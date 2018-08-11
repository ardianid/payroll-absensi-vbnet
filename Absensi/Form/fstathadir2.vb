Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Public Class fstathadir2
    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private Sub kosongkan()
        tnip.Text = ""
        tnama.Text = ""
        tket.Text = ""
        cstat.Checked = False
    End Sub

    Private Sub isi()
        tnip.Text = dv(position)("nip").ToString
        tnama.Text = dv(position)("nama").ToString
        ttgl1.EditValue = DateValue(dv(position)("tanggal").ToString)
        ttgl2.EditValue = DateValue(dv(position)("tanggal").ToString)
        tjam1.EditValue = TimeValue(dv(position)("jammasuk").ToString)
        tjam2.EditValue = TimeValue(dv(position)("jampulang").ToString)
        tcbostat.EditValue = dv(position)("stat").ToString
        tket.Text = dv(position)("keterangan").ToString
        cstat.EditValue = IIf(dv(position)("skalk").ToString.Equals("1"), True, False)
    End Sub

    Private Function loadsced(ByVal cn As OleDbConnection) As DataTable

        Dim sql As String = String.Format("select * from ms_kalender where year(tanggal) in ({0},{1})", Year(ttgl1.EditValue), Year(ttgl2.EditValue))

        Dim ds As DataSet = New DataSet
        ds = Clsmy.GetDataSet(sql, cn)

        Return ds.Tables(0)

    End Function

    Private Function harikerja(ByVal dt As DataTable, ByVal tgal As Date) As Boolean

        Dim dt2 As DataTable = dt.Copy
        Dim results() As DataRow = dt2.Select(String.Format("tanggal='{0}'", convert_date_to_eng(tgal)))

        Dim hasil As Boolean = False

        For Each row As DataRow In results
            If row(3).ToString.Equals("0") Then
                hasil = True
            End If
        Next

        Return hasil

    End Function

    Private Sub simpan()

        open_wait()
        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim dtkalender As DataTable = loadsced(cn)

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction

            Dim chadir As Integer
            If cstat.Checked = True Then
                chadir = 1
            Else
                chadir = 0
            End If

            Dim jammasuk As String = TimeValue(tjam1.EditValue)
            Dim jampulang As String = TimeValue(tjam2.EditValue)

            If addstat = True Then

                If dtkalender.Rows.Count = 0 Then
                    MsgBox("Isi dahulu kalender kerja...", vbInformation + vbOKOnly, "Informasi")
                    close_wait()
                    Return
                End If

                Dim sql As String = ""
                Dim tgal As Date

                If DateValue(ttgl1.EditValue) = DateValue(ttgl2.EditValue) Then

                    tgal = ttgl1.EditValue

                    If harikerja(dtkalender, tgal) = False Then
                        MsgBox("Tanggal yang anda masukkan adalah hari libur", vbInformation + vbOKOnly, "Informasi")
                        close_wait()
                        Return
                    End If

                    sql = String.Format("insert into tr_hadir (nip,tanggal,jammasuk,jampulang,stat,keterangan,skalk,stelat,spulangcpat,jnisabsen) values('{0}','{1}','{2}','{3}','{4}','{5}',{6},{7},{8},2)", _
                                                tnip.Text.Trim, convert_date_to_eng(tgal), jammasuk, jampulang, tcbostat.EditValue, tket.Text.Trim, chadir, 0, 0)
                    Dim cmdins As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                    cmdins.ExecuteNonQuery()

                    Clsmy.InsertToLog(cn, "btstatabs", 1, 0, 0, 0, tnip.Text.Trim, convert_date_to_eng(tgal), sqltrans)

                    insertview(tgal)

                Else

                    Dim jmldata As Integer = DateDiff(DateInterval.DayOfYear, ttgl1.EditValue, ttgl2.EditValue)

                    For i As Integer = 0 To jmldata

                        If i = 0 Then
                            tgal = ttgl1.EditValue
                        Else
                            tgal = DateAdd(DateInterval.Day, 1, tgal)
                        End If

                        If harikerja(dtkalender, tgal) = True Then

                            sql = String.Format("insert into tr_hadir (nip,tanggal,jammasuk,jampulang,stat,keterangan,skalk,stelat,spulangcpat,jnisabsen) values('{0}','{1}','{2}','{3}','{4}','{5}',{6},{7},{8},2)", _
                                                    tnip.Text.Trim, convert_date_to_eng(tgal), jammasuk, jampulang, tcbostat.EditValue, tket.Text.Trim, chadir, 0, 0)
                            Dim cmdins As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                            cmdins.ExecuteNonQuery()

                            Clsmy.InsertToLog(cn, "btstatabs", 1, 0, 0, 0, tnip.Text.Trim, convert_date_to_eng(tgal), sqltrans)

                            insertview(tgal)

                        End If

                    Next

                End If

            Else

                Dim sqlup As String = String.Format("update tr_hadir set jammasuk='{0}',jampulang='{1}',stat='{2}',keterangan='{3}',skalk={4} where nip='{5}' and tanggal='{6}'", _
                                                    jammasuk, jampulang, tcbostat.EditValue, tket.Text.Trim, chadir, tnip.Text.Trim, convert_date_to_eng(ttgl1.EditValue))

                Dim cmdup As OleDbCommand = New OleDbCommand(sqlup, cn, sqltrans)
                cmdup.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btstatabs", 0, 1, 0, 0, tnip.Text.Trim, convert_date_to_eng(ttgl1.EditValue), sqltrans)

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

    Private Sub insertview(ByVal tanggal As String)
        Dim orow As DataRowView = dv.AddNew
        orow("nip") = tnip.Text.Trim
        orow("nama") = tnama.Text.Trim
        orow("skalk") = IIf(cstat.Checked, 1, 0)
        orow("tanggal") = tanggal
        orow("stat") = tcbostat.EditValue
        orow("keterangan") = tket.Text.Trim
        orow("jammasuk") = tjam1.EditValue
        orow("jampulang") = tjam2.EditValue
        dv.EndInit()
    End Sub

    Private Sub editview()
        dv(position)("nama") = tnama.Text.Trim
        dv(position)("skalk") = IIf(cstat.Checked, 1, 0)
        dv(position)("tanggal") = ttgl1.EditValue
        dv(position)("stat") = tcbostat.EditValue
        dv(position)("keterangan") = tket.Text.Trim
        dv(position)("jammasuk") = tjam1.EditValue
        dv(position)("jampulang") = tjam2.EditValue
    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click

        If tnip.Text.Trim.Length = 0 Then
            MsgBox("Nip tidak boleh kosong...", vbOKOnly + vbInformation, "Informasi")
            tnip.Focus()
            Return
        End If

        If DateValue(ttgl2.EditValue) < DateValue(ttgl1.EditValue) Then
            MsgBox("Tanggal2 tidak boleh lebih kecil dari tanggal1", vbOKOnly + vbInformation, "Informasi")
            ttgl2.Focus()
            Return
        End If

        If TimeValue(tjam2.EditValue) < TimeValue(tjam1.EditValue) Then
            MsgBox("Jam2 tidak boleh lebih kecil dari jam1", vbOKOnly + vbInformation, "Informasi")
            tjam2.Focus()
            Return
        End If

        If tcbostat.EditValue.ToString.Length = 0 Then
            MsgBox("Status tidak boleh kosong...", vbOKOnly + vbInformation, "Informasi")
            tcbostat.Focus()
            Return
        End If

        simpan()

    End Sub

    Private Sub btselesai_Click(sender As System.Object, e As System.EventArgs) Handles btselesai.Click
        Me.Close()
    End Sub

    Private Sub fstathadir2_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        If addstat = True Then
            tnip.Enabled = True
        Else
            tcbostat.Focus()
        End If
    End Sub

    Private Sub fstathadir2_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If addstat = True Then
            kosongkan()

            ttgl1.EditValue = Date.Now
            ttgl2.EditValue = Date.Now

            tnip.Enabled = True
            btcnip.Enabled = True

            ttgl1.Enabled = True
            ttgl2.Enabled = True

        Else

            isi()
            tnip.Enabled = False
            btcnip.Enabled = False

            ttgl1.Enabled = False
            ttgl2.Enabled = False

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

                Dim sql As String = String.Format("select top 100 nip,nama,alamat from ms_karyawan where shiden=0 and aktif=1 and nip='{0}'", tnip.Text.Trim)
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

            End Try

        End If

    End Sub

    Private Sub tnip_EditValueChanged(sender As Object, e As EventArgs) Handles tnip.EditValueChanged

    End Sub
End Class