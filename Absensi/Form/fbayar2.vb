﻿Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Public Class fbayar2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Dim nip_old As String
    Dim jumlah_old As Double

    Private Sub kosongkan()
        tbukti.Text = ""
        tnip.Text = ""
        tnama.Text = ""
        tjumlah.EditValue = 0
        tket.Text = ""
    End Sub

    Private Sub isi()

        tbukti.Text = dv(position)("nobukti").ToString
        ttgl.EditValue = DateValue(dv(position)("tanggal").ToString)

        tnip.Text = dv(position)("nip").ToString
        tnama.Text = dv(position)("nama").ToString

        nip_old = tnip.Text.Trim

        tjumlah.EditValue = dv(position)("jumlah").ToString

        jumlah_old = dv(position)("jumlah").ToString

        tket.Text = dv(position)("ket").ToString

    End Sub

    Private Function isi_nobukti(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As String

        Dim bulan As String
        bulan = Month(Date.Now)

        If bulan.Length = 1 Then
            bulan = "0" & bulan
        End If

        Dim nobukti_awl = "BBN." & bulan & Year(Date.Now).ToString.Substring(2, 2)

        Dim sql As String = String.Format("select MAX(nobukti) from ms_angsur where nobukti like '{0}%'", nobukti_awl)
        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        Dim noawal As String = 0

        If drd.Read Then
            If Not drd(0).ToString.Equals("") Then
                noawal = drd(0).ToString.Substring(8, 4)
            End If
        End If
        drd.Close()

        Dim noawal1 As Integer = 0
        If IsNumeric(noawal) Then
            noawal1 = Integer.Parse(noawal)
        End If

        noawal1 = noawal1 + 1
        If noawal1.ToString.Length = 1 Then
            noawal = "000" & noawal1
        ElseIf noawal1.ToString.Length = 2 Then
            noawal = "00" & noawal1
        ElseIf noawal1.ToString.Length = 3 Then
            noawal = "0" & noawal1
        Else
            noawal = noawal1
        End If

        noawal = nobukti_awl & noawal

        Return noawal

    End Function

    Private Sub simpan()

        open_wait()
        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn


            Dim sqltrans As OleDbTransaction = cn.BeginTransaction

            tbukti.Text = isi_nobukti(cn, sqltrans)

            Dim sqlins As String = String.Format("insert into ms_angsur (nobukti,tanggal,nip,jumlah,ket) values('{0}','{1}','{2}',{3},'{4}')", tbukti.Text.Trim, convert_date_to_eng(ttgl.EditValue), tnip.Text.Trim, Replace(tjumlah.EditValue, ",", "."), tket.Text.Trim)
            Dim sqlup As String = String.Format("update ms_angsur set tanggal='{0}',nip='{1}',jumlah={2},ket='{3}' where nobukti='{4}'", convert_date_to_eng(ttgl.EditValue), tnip.Text.Trim, Replace(tjumlah.EditValue, ",", "."), tket.Text.Trim, tbukti.Text.Trim)

            If addstat = True Then

                Using cmdins As OleDbCommand = New OleDbCommand(sqlins, cn, sqltrans)
                    cmdins.ExecuteNonQuery()
                End Using

                Dim sqlbon As String = String.Format("update ms_karyawan set jmlbon=jmlbon - {0} where nip='{1}'", Replace(tjumlah.EditValue, ",", "."), tnip.Text.Trim)
                Using cmdbon As OleDbCommand = New OleDbCommand(sqlbon, cn, sqltrans)
                    cmdbon.ExecuteNonQuery()
                End Using

                Clsmy.InsertToLog(cn, "btbon", 1, 0, 0, 0, tbukti.Text.Trim, tnip.Text.Trim, sqltrans)

                insertview()

            Else

                Using cmdup As OleDbCommand = New OleDbCommand(sqlup, cn, sqltrans)
                    cmdup.ExecuteNonQuery()
                End Using

                Dim sqlbon1 As String = String.Format("update ms_karyawan set jmlbon=jmlbon + {0} where nip='{1}'", Replace(jumlah_old, ",", "."), nip_old)
                Using cmdbon1 As OleDbCommand = New OleDbCommand(sqlbon1, cn, sqltrans)
                    cmdbon1.ExecuteNonQuery()
                End Using

                Dim sqlbon As String = String.Format("update ms_karyawan set jmlbon=jmlbon - {0} where nip='{1}'", Replace(tjumlah.EditValue, ",", "."), tnip.Text.Trim)
                Using cmdbon As OleDbCommand = New OleDbCommand(sqlbon, cn, sqltrans)
                    cmdbon.ExecuteNonQuery()
                End Using

                editview()

            End If

            sqltrans.Commit()

            close_wait()

            MsgBox("Data Disimpan..", vbOKOnly + vbInformation, "Informasi")

            If addstat = True Then
                kosongkan()
                tnip.Focus()
            Else
                Me.Close()
            End If


        Catch ex As Exception
            close_wait()
            MsgBox(ex.ToString)
        Finally
            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

    End Sub

    Private Sub insertview()
        Dim orow As DataRowView = dv.AddNew
        orow("nobukti") = tbukti.Text.Trim
        orow("tanggal") = ttgl.EditValue

        orow("nip") = tnip.Text.Trim
        orow("nama") = tnama.Text.Trim

        orow("ket") = tket.Text.Trim
        orow("jumlah") = tjumlah.EditValue

        dv.EndInit()

    End Sub

    Private Sub editview()

        dv(position)("nobukti") = tbukti.Text.Trim
        dv(position)("tanggal") = ttgl.EditValue

        dv(position)("nip") = tnip.Text.Trim
        dv(position)("nama") = tnama.Text.Trim

        dv(position)("ket") = tket.Text.Trim
        dv(position)("jumlah") = tjumlah.EditValue

    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click

        If tnip.Text.Trim.Length = 0 Then
            MsgBox("Nip tidak boleh kosong...", vbOKOnly + vbInformation, "Informasi")
            tnip.Focus()
            Return
        End If

        If tjumlah.EditValue = 0 Then
            MsgBox("Jumlah tidak boleh 0", vbOKOnly + vbInformation, "Infromasi")
            tjumlah.Focus()
            Return
        End If

        simpan()

    End Sub

    Private Sub btselesai_Click(sender As System.Object, e As System.EventArgs) Handles btselesai.Click
        Me.Close()
    End Sub

    Private Sub fstathadir2_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        If addstat = True Then
            ttgl.Focus()
        Else
            tjumlah.Focus()
        End If
    End Sub

    Private Sub fbon2_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If addstat = True Then
            kosongkan()

            ttgl.EditValue = DateValue(Date.Now)

        Else
            isi()
        End If
    End Sub

    Private Sub btcnip_Click(sender As System.Object, e As System.EventArgs) Handles btcnip.Click
        Using fskary As New fskary With {.StartPosition = FormStartPosition.CenterParent}
            fskary.ShowDialog(Me)

            tnip.Text = fskary.get_Nip
            tnama.Text = fskary.get_nama

        End Using
    End Sub

    Private Sub tnip_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tnip.KeyDown
        If e.KeyCode = Keys.F4 Then
            btcnip_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tnip_LostFocus(sender As Object, e As System.EventArgs) Handles tnip.LostFocus

        If tnip.Text.Trim.Length > 0 Then

            Dim cn As OleDbConnection = Nothing

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim sql As String = String.Format("select nip,nama,alamat,idmesin from ms_karyawan where shiden=0 and aktif=1 and nip='{0}'", tnip.Text.Trim)
                Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim drd As OleDbDataReader = cmd.ExecuteReader

                If drd.HasRows Then
                    If drd.Read Then
                        tnip.Text = drd(0).ToString
                        tnama.Text = drd(1).ToString

                    Else
                        MsgBox("Nip tidak ditemukan...", vbOKOnly + vbInformation, "Informasi")
                        tnip.Text = ""
                        tnama.Text = ""

                    End If
                Else
                    MsgBox("Nip tidak ditemukan...", vbOKOnly + vbInformation, "Informasi")
                    tnip.Text = ""
                    tnama.Text = ""

                End If


            Catch ex As Exception
                MsgBox(ex.ToString)
            Finally
                If Not cn Is Nothing Then
                    If cn.State = ConnectionState.Open Then
                        cn.Close()
                    End If
                End If
            End Try

        End If

    End Sub

    Private Sub tnip_EditValueChanged(sender As Object, e As EventArgs) Handles tnip.EditValueChanged

    End Sub
End Class