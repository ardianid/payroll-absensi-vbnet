Imports System.Data.OleDb
Public Class fshift2

    Public dv As DataView

    Private Sub simpan()

        If dv.Count > 0 Then

            Dim dt1 As DataTable = dv.ToTable
            Dim rs As DataRow() = dt1.Select(String.Format("nama_shift='{0}'", tnama.EditValue))

            If rs.Length > 0 Then
                MsgBox("Shift sudah ada..", vbOKOnly + vbInformation, "Informasi")
                Return
            End If

        End If

        open_wait()

        Dim ok As Boolean = True
        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("insert into ms_shift (nama_shift,periode,perputaran) values('{0}',{1},'{2}')", tnama.EditValue, tperiod.EditValue, tunit.EditValue)
            Using cmd As OleDbCommand = New OleDbCommand(sql, cn)
                cmd.ExecuteNonQuery()
            End Using

        Catch ex As Exception
            close_wait()
            ok = False
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

        If ok = True Then
            insertview()
            close_wait()
        End If

        Me.Close()

    End Sub

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew
        orow("nama_shift") = tnama.EditValue
        ' orow("tgl_mulai") = ttgl.EditValue

        orow("periode") = tperiod.EditValue
        orow("perputaran") = tunit.EditValue

        dv.EndInit()

    End Sub

    Private Sub btselesai_Click(sender As Object, e As EventArgs) Handles btselesai.Click
        Me.Close()
    End Sub

    Private Sub btsimpan_Click(sender As Object, e As EventArgs) Handles btsimpan.Click

        If tnama.Text.Trim.Length = 0 Then
            MsgBox("Nama shift tidak boleh kosong..", vbOKOnly + vbInformation, "Informasi")
            tnama.Focus()
            Return
        End If

        If tperiod.EditValue = 0 Then
            MsgBox("Periode tidak boleh 0", vbOKOnly + vbInformation, "Infromasi")
            tperiod.Focus()
            Return
        End If

        simpan()
    End Sub

    Private Sub cektanggal()

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select * from ms_kalender where tanggal<='{0}' order by tanggal desc", convert_date_to_eng(Date.Now))
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            While drd.Read

                If tunit.EditValue.Equals("MINGGU") Then
                    If drd("hari").ToString.Equals("SENIN") Then
                        ttgl.EditValue = convert_date_to_ind(drd("tanggal").ToString)
                        Exit While
                    End If
                Else
                    If DateValue(drd("tanggal").ToString).Day = 1 Then
                        ttgl.EditValue = convert_date_to_ind(drd("tanggal").ToString)
                        Exit While
                    End If
                End If

            End While
            drd.Close()

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

        

    End Sub

    Private Sub cektanggal2()

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select * from ms_kalender where tanggal='{0}'", convert_date_to_eng(ttgl.EditValue))
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If tunit.EditValue.Equals("MINGGU") Then
                    If Not drd("hari").ToString.Equals("SENIN") Then
                        MsgBox("Hari bukan senin..", vbOKOnly + vbInformation, "Informasi")
                        '  cektanggal()
                        ttgl.Focus()
                    End If
                Else
                    If Not DateValue(drd("tanggal").ToString).Day = 1 Then
                        MsgBox("Tanggal bukan 1", vbOKOnly + vbInformation, "Informasi")
                        '   cektanggal()
                        ttgl.Focus()
                    End If
                End If
            End If
            drd.Close()

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

    End Sub

    Private Sub fshift2_Load(sender As Object, e As EventArgs) Handles Me.Load
        tunit.SelectedIndex = 0
        tperiod.Text = 0
        ttgl.EditValue = DateValue(Date.Now)

        tunit_Validated(sender, Nothing)

    End Sub

    Private Sub tunit_Validated(sender As Object, e As EventArgs) Handles tunit.Validated
        cektanggal()
    End Sub

    Private Sub ttgl_Validated(sender As Object, e As EventArgs) Handles ttgl.Validated
        cektanggal2()
    End Sub

End Class