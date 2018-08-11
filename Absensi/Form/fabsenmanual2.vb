Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Public Class fabsenmanual2
    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private idmesin As String
    Private in_old As String
    Private out_old As String

    Private in_lawas As String
    Private out_lawas As String

    Public tanggal_s As String
    Public nnip_s As String
    Private get_okadd As Boolean = False

    Public ReadOnly Property get_ok As String
        Get

            Return get_okadd

        End Get
    End Property

    Private Sub kosongkan()
        tnip.Text = ""
        tnama.Text = ""
        tket.Text = ""
        idmesin = ""
    End Sub

    Private Sub isi()
        tnip.Text = dv(position)("nip").ToString
        tnama.Text = dv(position)("nama").ToString
        ttgl1.EditValue = DateValue(dv(position)("tanggal").ToString)
        ttgl2.EditValue = DateValue(dv(position)("tanggal").ToString)
        tjam1.EditValue = TimeValue(dv(position)("jammasuk").ToString)
        tjam2.EditValue = TimeValue(dv(position)("jampulang").ToString)
        tket.Text = dv(position)("keterangan").ToString
        idmesin = dv(position)("idmesin").ToString

        Dim tanggal As String = DateValue(ttgl1.EditValue)
        Dim jam1 As String = TimeValue(tjam1.EditValue)
        Dim jam2 As String = TimeValue(tjam2.EditValue)

        in_lawas = jam1
        out_lawas = jam2

        in_old = String.Format("{0} {1}", tanggal, jam1)
        out_old = String.Format("{0} {1}", tanggal, jam2)

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


        get_okadd = False

        open_wait()
        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim dtkalender As DataTable = loadsced(cn)

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction

            Dim jammasuk As String = TimeValue(tjam1.EditValue)
            Dim jampulang As String = TimeValue(tjam2.EditValue)

            Dim sqlinout As String = ""
            
            Dim tgal As Date
            Dim tgl1 As String = DateValue(ttgl1.EditValue)
            Dim tgal2 As String = DateValue(ttgl2.EditValue)
            Dim rdate_in As String = String.Format("{0} {1}", tgl1, jammasuk)
            Dim rdate_out As String = String.Format("{0} {1}", tgal2, jampulang)


            If addstat = True Then

                If dtkalender.Rows.Count = 0 Then
                    MsgBox("Isi dahulu kalender kerja...", vbInformation + vbOKOnly, "Informasi")
                    close_wait()
                    Return
                End If

                Dim sql As String = ""

                If DateValue(ttgl1.EditValue) = DateValue(ttgl2.EditValue) Then

                    tgal = ttgl1.EditValue


                    If addstat = True Then

                        If harikerja(dtkalender, tgal) = False Then
                            close_wait()
                            If MsgBox("Tanggal yang anda masukkan adalah hari libur, tetap ingin melanjutkan ?", vbInformation + vbYesNo, "Informasi") = vbNo Then
                                Return
                            Else
                                open_wait()
                            End If

                        End If

                        Dim sqlcek As String = String.Format("select a.tanggal from V_InOut2 a inner join ms_karyawan b on a.userid=b.idmesin  where a.tanggal='{0}' and b.nip='{1}'", convert_date_to_eng(tgal), tnip.Text.Trim)
                        Dim cmdcek As OleDbCommand = New OleDbCommand(sqlcek, cn, sqltrans)
                        Dim drcek As OleDbDataReader = cmdcek.ExecuteReader

                        If drcek.Read Then

                            If Not (drcek(0).ToString.Equals("")) Then

                                close_wait()

                                MsgBox("Sudah ada data absen dimesin pada tanggal ini...", vbOKOnly + vbExclamation, "konfirmasi")

                                Return

                            End If

                        End If

                        drcek.Close()

                        

                    End If

                    Dim sqls_inout As String = String.Format("select count(*) from ms_inout where userid={0} and (checktime='{1}' or checktime='{2}')", idmesin, convert_datetime_to_eng(rdate_in), convert_datetime_to_eng(rdate_out))
                    Dim comds As OleDbCommand = New OleDbCommand(sqls_inout, cn, sqltrans)
                    Dim dred As OleDbDataReader = comds.ExecuteReader

                    If dred.HasRows Then
                        If dred.Read Then
                            Dim xhasil As Integer = Convert.ToInt32(dred(0).ToString)

                            If xhasil > 0 Then
                                close_wait()
                                MsgBox("Jam absen sudah ada...", vbOKOnly + vbInformation, "Informasi")
                                Return
                            End If

                        End If
                    End If

                    dred.Close()


                    sql = String.Format("insert into ms_absenman (nip,tanggal,jammasuk,jampulang,keterangan) values('{0}','{1}','{2}','{3}','{4}')", _
                                                tnip.Text.Trim, convert_date_to_eng(tgal), jammasuk, jampulang, tket.Text.Trim)

                    sqlinout = String.Format("insert into ms_inout (userid,checktime,verifymode,inoutmode,workcode,skalk) values({0},'{1}',0,0,0,0)", idmesin, convert_datetime_to_eng(rdate_in))

                    Dim cmdins_inout1 As OleDbCommand = New OleDbCommand(sqlinout, cn, sqltrans)
                    cmdins_inout1.ExecuteNonQuery()

                    sqlinout = String.Format("insert into ms_inout (userid,checktime,verifymode,inoutmode,workcode,skalk) values({0},'{1}',0,0,0,0)", idmesin, convert_datetime_to_eng(rdate_out))

                    Dim cmdins_inout2 As OleDbCommand = New OleDbCommand(sqlinout, cn, sqltrans)
                    cmdins_inout2.ExecuteNonQuery()

                    Dim cmdins As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                    cmdins.ExecuteNonQuery()

                    Clsmy.InsertToLog(cn, "btgajiminggu", 1, 0, 0, 0, tnip.Text.Trim, convert_date_to_eng(tgal), sqltrans)

                    If nnip_s.Length = 0 Then
                        insertview(tgal)
                    End If


                Else

                    'Dim jmldata As Integer = DateDiff(DateInterval.DayOfYear, ttgl1.EditValue, ttgl2.EditValue)

                    'For i As Integer = 0 To jmldata

                    '    If i = 0 Then
                    '        tgal = ttgl1.EditValue
                    '    Else
                    '        tgal = DateAdd(DateInterval.Day, 1, tgal)
                    '    End If

                    '    If harikerja(dtkalender, tgal) = True Then

                    '        tglin = String.Format("{0} {1}", tgal, jammasuk)
                    '        tglout = String.Format("{0} {1}", tgal, jampulang)

                    '        sqlinout = String.Format("insert into ms_inout (userid,checktime,verifymode,inoutmode,workcode,skalk) values({0},'{1}',0,0,0,0)", idmesin, convert_datetime_to_eng(tglin))

                    '        Dim cmdins_inout1 As OleDbCommand = New OleDbCommand(sqlinout, cn, sqltrans)
                    '        cmdins_inout1.ExecuteNonQuery()

                    '        sqlinout = String.Format("insert into ms_inout (userid,checktime,verifymode,inoutmode,workcode,skalk) values({0},'{1}',0,0,0,0)", idmesin, convert_datetime_to_eng(tglout))

                    '        Dim cmdins_inout2 As OleDbCommand = New OleDbCommand(sqlinout, cn, sqltrans)
                    '        cmdins_inout2.ExecuteNonQuery()

                    '        sql = String.Format("insert into tr_hadir (nip,tanggal,jammasuk,jampulang,stat,keterangan,skalk,stelat,spulangcpat,jnisabsen) values('{0}','{1}','{2}','{3}','HADIR','{4}',1,{5},{6},3)", _
                    '                            tnip.Text.Trim, convert_date_to_eng(tgal), jammasuk, jampulang, tket.Text.Trim, 0, 0)
                    '        Dim cmdins As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                    '        cmdins.ExecuteNonQuery()

                    '        Clsmy.InsertToLog(cn, "btgajiminggu", 1, 0, 0, 0, tnip.Text.Trim, convert_date_to_eng(tgal), sqltrans)

                    '        insertview(tgal)

                    '    End If

                    'Next

                End If

            Else

                '   tgal = ttgl1.EditValue
                '  tglin = String.Format("{0} {1}", tgal, jammasuk)
                ' tglout = String.Format("{0} {1}", tgal2, jampulang)


                sqlinout = String.Format("update ms_inout set checktime='{0}' where userid={1} and checktime='{2}'", convert_datetime_to_eng(rdate_in), idmesin, convert_datetime_to_eng(in_old))

                Dim cmdins_inout1 As OleDbCommand = New OleDbCommand(sqlinout, cn, sqltrans)
                cmdins_inout1.ExecuteNonQuery()

                sqlinout = String.Format("update ms_inout set checktime='{0}' where userid={1} and checktime='{2}'", convert_datetime_to_eng(rdate_out), idmesin, convert_datetime_to_eng(out_old))

                Dim cmdins_inout2 As OleDbCommand = New OleDbCommand(sqlinout, cn, sqltrans)
                cmdins_inout2.ExecuteNonQuery()

                Dim sqlup As String = String.Format("update ms_absenman set jammasuk='{0}',jampulang='{1}',keterangan='{2}' where nip='{3}' and tanggal='{4}' and jammasuk='{5}' and jampulang='{6}'", _
                                                    jammasuk, jampulang, tket.Text.Trim, tnip.Text.Trim, convert_date_to_eng(ttgl1.EditValue), in_lawas, out_lawas)

                Dim cmdup As OleDbCommand = New OleDbCommand(sqlup, cn, sqltrans)
                cmdup.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btgajiminggu", 0, 1, 0, 0, tnip.Text.Trim, convert_date_to_eng(ttgl1.EditValue), sqltrans)

                editview()

            End If

            sqltrans.Commit()

            close_wait()


            If nnip_s.Length = 0 Then

                MsgBox("Data Disimpan..", vbOKOnly + vbInformation, "Informasi")

                If addstat = True Then
                    kosongkan()
                    tnip.Focus()
                Else
                    Me.Close()
                End If

            Else

                get_okadd = True

                Me.Close()
            End If


        Catch ex As Exception
            get_okadd = False
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
        orow("tanggal") = tanggal
        orow("keterangan") = tket.Text.Trim
        orow("jammasuk") = tjam1.EditValue
        orow("jampulang") = tjam2.EditValue
        orow("idmesin") = idmesin
        dv.EndInit()
    End Sub

    Private Sub editview()
        dv(position)("nama") = tnama.Text.Trim
        dv(position)("tanggal") = ttgl1.EditValue
        dv(position)("keterangan") = tket.Text.Trim
        dv(position)("jammasuk") = tjam1.EditValue
        dv(position)("jampulang") = tjam2.EditValue
        dv(position)("idmesin") = idmesin
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

        If tket.Text.Trim.Length = 0 Then
            MsgBox("Keterangan alasan tidak absen harus diisi...", vbOKOnly + vbInformation, "Informasi")
            tket.Focus()
            Return
        End If

        simpan()

    End Sub

    Private Sub btselesai_Click(sender As System.Object, e As System.EventArgs) Handles btselesai.Click
        get_okadd = False
        Me.Close()
    End Sub

    Private Sub fstathadir2_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        If addstat = True Then

            If Not nnip_s.Length = 0 Then
                tjam1.Focus()
            Else
                tnip.Focus()
            End If

        Else
            tket.Focus()
        End If
    End Sub

    Private Sub fstathadir2_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        get_okadd = False

        If addstat = True Then
            kosongkan()

            ttgl1.EditValue = Date.Now
            tnip.Enabled = True
            btcnip.Enabled = True
            ttgl1.Enabled = True

            If Not nnip_s.Length = 0 Then
                tnip.EditValue = nnip_s
                tnip_LostFocus(sender, Nothing)
                ttgl1.EditValue = convert_date_to_ind(tanggal_s)
            End If


        Else

            isi()
            tnip.Enabled = False
            btcnip.Enabled = False

            ttgl1.Enabled = False

        End If
    End Sub

    Private Sub btcnip_Click(sender As System.Object, e As System.EventArgs) Handles btcnip.Click
        Using fskary As New fskary With {.StartPosition = FormStartPosition.CenterParent}
            fskary.ShowDialog(Me)

            tnip.Text = fskary.get_Nip
            tnama.Text = fskary.get_nama
            idmesin = fskary.get_idmesin

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
                        idmesin = drd(3).ToString
                    Else
                        MsgBox("Nip tidak ditemukan...", vbOKOnly + vbInformation, "Informasi")
                        tnip.Text = ""
                        tnama.Text = ""
                        idmesin = ""
                    End If
                Else
                    MsgBox("Nip tidak ditemukan...", vbOKOnly + vbInformation, "Informasi")
                    tnip.Text = ""
                    tnama.Text = ""
                    idmesin = ""
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

    Private Sub ttgl1_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles ttgl1.EditValueChanged
        ttgl2.EditValue = ttgl1.EditValue
    End Sub

    Private Sub tnip_EditValueChanged(sender As Object, e As EventArgs) Handles tnip.EditValueChanged

    End Sub
End Class