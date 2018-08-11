Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Public Class fgaji_mingguan

    Private bs1 As BindingSource
    Private ds As DataSet
    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private statedit As Boolean
    Private sqlopen As String
    Private kbulan As Integer

    Private dvmanager2 As Data.DataViewManager
    Private dv2 As Data.DataView

    Private jenishitung_lembur As Integer = 0

    Private Sub loadGolongan()

        Dim cn As OleDbConnection = Nothing

        Try

            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = "select kode,nama,jnisgol,harian from ms_golongan where jenisgaji='Mingguan' and tampilgroup=1"

            Dim dsgol As New DataSet
            dsgol = Clsmy.GetDataSet(sql, cn)

            dvmanager = New DataViewManager(dsgol)
            dv2 = dvmanager.CreateDataView(dsgol.Tables(0))

            grid2.DataSource = dv2

            close_wait()

        Catch ex As Exception
            close_wait()
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

    End Sub

    Private Sub Get_Aksesform()

        Dim rows() As DataRow = dtmenu.Select(String.Format("NAMAFORM='{0}'", Me.Name.ToUpper.ToString))
        Dim rows2() As DataRow = dtmenu2.Select(String.Format("NAMAFORM='{0}'", Me.Name.ToUpper.ToString))

        If Convert.ToInt16(rows(0)("t_add")) = 1 Then
            tsadd.Enabled = True
        Else
            tsadd.Enabled = False
        End If

        If Convert.ToInt16(rows(0)("t_edit")) = 1 Then
            statedit = True
        Else
            statedit = False
        End If

        If Convert.ToInt16(rows2(0)("t_lap")) = 1 Then
            tsprint.Enabled = True
        Else
            tsprint.Enabled = False
        End If

    End Sub

    Private Sub opendata()


        If IsNothing(dv2) Then
            Return
        End If

        If dv2.Count <= 0 Then
            Return
        End If


        Dim jnisgol As String = dv2(Me.BindingContext(dv2).Position)("jnisgol").ToString
        Dim hariank As Integer = Integer.Parse(dv2(Me.BindingContext(dv2).Position)("harian").ToString)

        If jnisgol.Equals("Manual") And hariank = 0 Then

            XtraTabPage2.PageVisible = True

        Else

            XtraTabPage2.PageVisible = False

        End If


            If IsNothing(ttahun.Text) Then
                Return
            End If

            If ttahun.Text = "" Then
                Return
            End If

            If ttahun.Text.Trim.Length = 0 Then
                Return
            End If

            If Not IsNumeric(ttahun.Text.Trim) Then
                Return
            End If

            If tcbmgu.Text.Trim = "" Then
                Return
            End If

            If ttgl1.Text.Trim.Length = 0 Or ttgl2.Text.Trim.Length = 0 Then
                Return
            End If

        If Not IsDate(ttgl1.Text.Trim) Then
            Return
        End If

        If Not IsDate(ttgl2.Text.Trim) Then
            Return
        End If

        sqlopen = "select a.nip,b.nama,c.nama as golongan,a.gapok,a.tunj_jab,a.tunj_hadir,a.jmlhadir,a.tunj_akomod,a.tunj_makan," & _
             "a.tunj_makanlembur,a.jmllembur as jmllembur,a.gaji_lembur,a.jml_hasil,a.tot_hasil,a.keterangan,a.tot_harian,a.id,a.tahun,a.minggu, " & _
             "(a.gapok + a.tunj_jab + a.tunj_hadir +  a.tunj_akomod + a.tunj_makan + a.tunj_makanlembur + a.gaji_lembur  + a.tot_harian) as tot_gaji " & _
             "from tr_gaji a inner join ms_karyawan b on a.nip=b.nip " & _
             "inner join ms_golongan c on b.kdgol=c.kode"

        sqlopen = String.Format("{0} where a.tahun={1} and a.minggu={2} and a.bulan={3} and b.kdgol='{4}'", sqlopen, ttahun.EditValue, tcbmgu.EditValue, kbulan, dv2(Me.BindingContext(dv2).Position)("kode").ToString)

            Dim cn As OleDbConnection = Nothing

            grid1.DataSource = Nothing

            Try

                open_wait()

                dv1 = Nothing

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                setColumn(cn)

                ds = New DataSet()
                ds = Clsmy.GetDataSet(sqlopen, cn)

                dvmanager = New DataViewManager(ds)
                dv1 = dvmanager.CreateDataView(ds.Tables(0))

                bs1 = New BindingSource
                bs1.DataSource = dv1
            bn1.BindingSource = bs1

                grid1.DataSource = bs1

                close_wait()


            Catch ex As OleDb.OleDbException
                close_wait()
                MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
            Finally


                If Not cn Is Nothing Then
                    If cn.State = ConnectionState.Open Then
                        cn.Close()
                    End If
                End If

            End Try

    End Sub

    Private Sub setColumn(ByVal cn As OleDbConnection)

        GridView1.Columns("nip").Visible = True
        GridView1.Columns("nip").VisibleIndex = 0

        GridView1.Columns("nama").Visible = True
        GridView1.Columns("nama").VisibleIndex = 1

        Dim sqlutil As String = String.Format("select * from sutil_gaji where kd_gol='{0}'", dv2(Me.BindingContext(dv2).Position)("kode").ToString)
        Dim cmdutil As OleDbCommand = New OleDbCommand(sqlutil, cn)
        Dim drutil As OleDbDataReader = cmdutil.ExecuteReader

        If drutil.Read Then

            Dim vindex As Integer = 1

            If IsNumeric(drutil("sjmlhadir").ToString) Then
                If drutil("sjmlhadir") = 1 Then

                    vindex = vindex + 1

                    GridView1.Columns("jmlhadir").Visible = True
                    GridView1.Columns("jmlhadir").VisibleIndex = vindex
                Else
                    GridView1.Columns("jmlhadir").Visible = False
                    GridView1.Columns("jmlhadir").VisibleIndex = -1
                End If
            End If

            If IsNumeric(drutil("sgapok").ToString) Then
                If drutil("sgapok") = 1 Then

                    If xform = 0 Then
                        vindex = vindex + 1

                        GridView1.Columns("gapok").Visible = True
                        GridView1.Columns("gapok").VisibleIndex = vindex
                    Else

                        If xgapok = 1 Then
                            vindex = vindex + 1

                            GridView1.Columns("gapok").Visible = True
                            GridView1.Columns("gapok").VisibleIndex = vindex
                        Else
                            GridView1.Columns("gapok").Visible = False
                            GridView1.Columns("gapok").VisibleIndex = -1
                        End If

                    End If




                Else
                    GridView1.Columns("gapok").Visible = False
                    GridView1.Columns("gapok").VisibleIndex = -1
                End If
            End If

            If IsNumeric(drutil("stunj_jab").ToString) Then
                If drutil("stunj_jab") = 1 Then

                    If xform = 0 Then
                        vindex = vindex + 1

                        GridView1.Columns("tunj_jab").Visible = True
                        GridView1.Columns("tunj_jab").VisibleIndex = vindex

                    Else

                        If xtunj_jab = 1 Then

                            vindex = vindex + 1

                            GridView1.Columns("tunj_jab").Visible = True
                            GridView1.Columns("tunj_jab").VisibleIndex = vindex

                        Else

                            GridView1.Columns("tunj_jab").Visible = False
                            GridView1.Columns("tunj_jab").VisibleIndex = -1

                        End If

                    End If

                Else
                    GridView1.Columns("tunj_jab").Visible = False
                    GridView1.Columns("tunj_jab").VisibleIndex = -1
                End If
            End If

            If IsNumeric(drutil("sjmllembur").ToString) Then
                If drutil("sjmllembur") = 1 Then


                    vindex = vindex + 1

                    GridView1.Columns("jmllembur").Visible = True
                    GridView1.Columns("jmllembur").VisibleIndex = vindex
                Else

                    GridView1.Columns("jmllembur").Visible = False
                    GridView1.Columns("jmllembur").VisibleIndex = -1
                End If
            End If

            If IsNumeric(drutil("sgajilembur").ToString) Then
                If drutil("sgajilembur") = 1 Then

                    vindex = vindex + 1

                    GridView1.Columns("gaji_lembur").Visible = True
                    GridView1.Columns("gaji_lembur").VisibleIndex = vindex
                Else
                    GridView1.Columns("gaji_lembur").Visible = False
                    GridView1.Columns("gaji_lembur").VisibleIndex = -1
                End If
            End If

            If IsNumeric(drutil("stunjhadir").ToString) Then
                If drutil("stunjhadir") = 1 Then


                    If xform = 0 Then
                        vindex = vindex + 1

                        GridView1.Columns("tunj_hadir").Visible = True
                        GridView1.Columns("tunj_hadir").VisibleIndex = vindex

                    Else

                        If xtunj_hdr = 1 Then

                            vindex = vindex + 1

                            GridView1.Columns("tunj_hadir").Visible = True
                            GridView1.Columns("tunj_hadir").VisibleIndex = vindex

                        Else

                            GridView1.Columns("tunj_hadir").Visible = False
                            GridView1.Columns("tunj_hadir").VisibleIndex = -1

                        End If

                    End If

                Else
                    GridView1.Columns("tunj_hadir").Visible = False
                    GridView1.Columns("tunj_hadir").VisibleIndex = -1
                End If
            End If

            If IsNumeric(drutil("stunjakomod").ToString) Then
                If drutil("stunjakomod") = 1 Then

                    If xform = 0 Then

                        vindex = vindex + 1

                        GridView1.Columns("tunj_akomod").Visible = True
                        GridView1.Columns("tunj_akomod").VisibleIndex = vindex

                    Else

                        If xtunj_trans = 1 Then

                            vindex = vindex + 1

                            GridView1.Columns("tunj_akomod").Visible = True
                            GridView1.Columns("tunj_akomod").VisibleIndex = vindex

                        Else

                            GridView1.Columns("tunj_akomod").Visible = False
                            GridView1.Columns("tunj_akomod").VisibleIndex = -1

                        End If

                    End If


                Else

                    GridView1.Columns("tunj_akomod").Visible = False
                    GridView1.Columns("tunj_akomod").VisibleIndex = -1

                End If
            End If

            If IsNumeric(drutil("stunjmakan").ToString) Then
                If drutil("stunjmakan") = 1 Then

                    If xform = 0 Then

                        vindex = vindex + 1

                        GridView1.Columns("tunj_makan").Visible = True
                        GridView1.Columns("tunj_makan").VisibleIndex = vindex

                    Else

                        If xtunj_makan = 1 Then

                            vindex = vindex + 1

                            GridView1.Columns("tunj_makan").Visible = True
                            GridView1.Columns("tunj_makan").VisibleIndex = vindex

                        Else

                            GridView1.Columns("tunj_makan").Visible = False
                            GridView1.Columns("tunj_makan").VisibleIndex = -1

                        End If

                    End If


                Else

                    GridView1.Columns("tunj_makan").Visible = False
                    GridView1.Columns("tunj_makan").VisibleIndex = -1


                End If
            End If

            If IsNumeric(drutil("stambmakan").ToString) Then
                If drutil("stambmakan") = 1 Then

                    If xform = 0 Then

                        vindex = vindex + 1

                        GridView1.Columns("tunj_makanlembur").Visible = True
                        GridView1.Columns("tunj_makanlembur").VisibleIndex = vindex

                    Else

                        If xtamb_makan = 1 Then

                            vindex = vindex + 1

                            GridView1.Columns("tunj_makanlembur").Visible = True
                            GridView1.Columns("tunj_makanlembur").VisibleIndex = vindex

                        Else
                            GridView1.Columns("tunj_makanlembur").Visible = False
                            GridView1.Columns("tunj_makanlembur").VisibleIndex = -1

                        End If


                    End If


                Else
                    GridView1.Columns("tunj_makanlembur").Visible = False
                    GridView1.Columns("tunj_makanlembur").VisibleIndex = -1
                End If
            End If

            If IsNumeric(drutil("sgajihar").ToString) Then
                If drutil("sgajihar") = 1 Then

                    vindex = vindex + 1

                    GridView1.Columns("tot_harian").Visible = True
                    GridView1.Columns("tot_harian").VisibleIndex = vindex
                Else
                    GridView1.Columns("tot_harian").Visible = False
                    GridView1.Columns("tot_harian").VisibleIndex = -1
                End If
            End If

            If IsNumeric(drutil("sjmlhasil").ToString) Then
                If drutil("sjmlhasil") = 1 Then

                    vindex = vindex + 1

                    GridView1.Columns("jml_hasil").Visible = True
                    GridView1.Columns("jml_hasil").VisibleIndex = vindex
                Else
                    GridView1.Columns("jml_hasil").Visible = False
                    GridView1.Columns("jml_hasil").VisibleIndex = -1
                End If
            End If

            If IsNumeric(drutil("sgajihasil").ToString) Then
                If drutil("sgajihasil") = 1 Then


                    vindex = vindex + 1

                    GridView1.Columns("tot_hasil").Visible = True
                    GridView1.Columns("tot_hasil").VisibleIndex = vindex
                Else

                    GridView1.Columns("tot_hasil").Visible = False
                    GridView1.Columns("tot_hasil").VisibleIndex = -1

                End If
            End If

            Dim jnisgol As String = dv2(Me.BindingContext(dv2).Position)("jnisgol").ToString
            Dim hariank As Integer = Integer.Parse(dv2(Me.BindingContext(dv2).Position)("harian").ToString)

            If jnisgol.Equals("Manual") And hariank = 0 Then

                GridView1.Columns("tot_gaji").Visible = False
                GridView1.Columns("tot_gaji").VisibleIndex = -1

            Else

                vindex = vindex + 1

                GridView1.Columns("tot_gaji").Visible = True
                GridView1.Columns("tot_gaji").VisibleIndex = vindex

            End If

            If IsNumeric(drutil("sket").ToString) Then
                If drutil("sket") = 1 Then

                    vindex = vindex + 1

                    GridView1.Columns("keterangan").Visible = True
                    GridView1.Columns("keterangan").VisibleIndex = vindex
                Else
                    GridView1.Columns("keterangan").Visible = False
                    GridView1.Columns("keterangan").VisibleIndex = -1
                End If
            End If

        End If

        drutil.Close()

    End Sub

    Private Sub cek_jenis_lembur(ByVal cn As OleDbConnection)

        Dim sql As String = "select * from ms_awalshift"
        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        jenishitung_lembur = 0

        If drd.Read Then
            If IsNumeric(drd("jnis_hit_harian").ToString) Then
                jenishitung_lembur = drd("jnis_hit_harian").ToString
            End If
        End If
        drd.Close()

    End Sub

    Private Sub open_minggu()

        tcbmgu.Properties.Items.Clear()
        ttgl1.Text = ""
        ttgl2.Text = ""

        If ttahun.Text.Trim.Length = 0 Then
            Return
        End If

        If Not IsNumeric(ttahun.Text.Trim) Then
            Return
        End If

        Dim sql As String = String.Format("select MAX(minggu) as minggu from ms_kalender where year(tanggal)={0}", ttahun.EditValue)

        Dim cn As OleDbConnection = Nothing


        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.HasRows Then

                If drd.Read Then

                    If drd(0).ToString.Equals("") Then
                        Return
                    End If

                    For i As Integer = 1 To Convert.ToInt32(drd(0).ToString)
                        tcbmgu.Properties.Items.Add(i)
                    Next

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

            If tcbmgu.Properties.Items.Count > 0 Then
                tcbmgu.SelectedIndex = 0
            End If

        End Try

    End Sub

    Private Sub getTanggal()

        ttgl1.Text = ""
        ttgl2.Text = ""

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqlbulan As String = String.Format("select MONTH(tanggal) as bln from ms_kalender where YEAR(tanggal)={0} and minggu={1}", ttahun.Text.Trim, tcbmgu.Text.Trim)

            Dim sqlcek As String = String.Format("select tanggal1,tanggal2 from tr_gaji where minggu={0}", tcbmgu.Text.Trim)
            Dim cmdcek As OleDbCommand = New OleDbCommand(sqlcek, cn)
            Dim drcek As OleDbDataReader = cmdcek.ExecuteReader

            If drcek.Read Then

                If Not drcek(0).ToString.Equals("") Then

                    ttgl1.Text = convert_date_to_ind(drcek(0).ToString)
                    ttgl2.Text = convert_date_to_ind(drcek(1).ToString)

                    drcek.Close()

                    GoTo cek_bulan

                End If

            End If

            drcek.Close()

            Dim sql As String = String.Format("select MIN(tanggal) as tglmin,MAX(tanggal) as tglmax from ms_kalender where YEAR(tanggal)={0} and minggu={1}", ttahun.Text.Trim, tcbmgu.Text.Trim)


            Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = comd.ExecuteReader

            If drd.HasRows Then
                If drd.Read Then
                    ttgl1.Text = convert_date_to_ind(drd(0).ToString)
                    ttgl2.Text = convert_date_to_ind(drd(1).ToString)
                End If
            End If

            drd.Close()

cek_bulan:

            Dim comd2 As OleDbCommand = New OleDbCommand(sqlbulan, cn)
            Dim drd2 As OleDbDataReader = comd2.ExecuteReader

            kbulan = 0
            If drd2.HasRows Then
                If drd2.Read Then
                    kbulan = Convert.ToInt32(drd2(0).ToString)
                End If
            End If

            drd2.Close()


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

    Private Function cek_periode_on_gaji(ByVal cn As OleDbConnection) As Boolean

        Dim hasil As Boolean = False

        Dim sql As String = String.Format("select distinct tanggal1,tanggal2,minggu from tr_gaji where calc_by='Mingguan' and tanggal1>='{0}'", convert_date_to_eng(ttgl1.EditValue))

        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        While drd.Read
            If DateValue(drd(1).ToString) <= DateValue(ttgl2.EditValue) Then

                If DateValue(ttgl1.EditValue) <> DateValue(drd(0).ToString) Then
                    hasil = True
                    Exit While
                End If

                If DateValue(ttgl2.EditValue) <> DateValue(drd(1).ToString) Then
                    hasil = True
                    Exit While
                End If

                If Integer.Parse(tcbmgu.EditValue) <> Integer.Parse(drd(2).ToString) Then
                    hasil = True
                    Exit While
                End If

            End If

        End While
        drd.Close()

            Return hasil

    End Function

    Private Sub kalkulasi()

        If ttahun.Text.Trim.Length > 0 Then
            If Not IsNumeric(ttahun.Text.Trim) Then
                '  MsgBox("Tahun salah..", vbOKOnly + vbInformation, "Informasi")
                Return
            End If
        Else
            Return
        End If

        If ttahun.Text.Trim = "" Then
            Return
        End If

        If tcbmgu.Text.Trim = "" Then
            Return
        End If

        If ttgl1.Text.Trim.Length = 0 Or ttgl2.Text.Trim.Length = 0 Then
            Return
        End If


        Dim sql As String = String.Format("select a.nip,a.nama,b.nama as golongan,a.gapok,a.tunj_jabatan,a.tunj_kehadiran,a.tunj_akomodasi,a.tunj_makan,b.jenislembur,b.Harian,b.laki2,b.perempuan,a.jniskelamin" & _
                " from ms_karyawan a inner join ms_golongan b on a.kdgol=b.kode where a.aktif=1 and b.jenisgaji='Mingguan' and a.kdgol='{0}'", dv2(Me.BindingContext(dv2).Position)("kode").ToString)

        Dim sqltr As String = String.Format("select a.nip,COUNT(stat) as jmlhadir,SUM(jamlembur) + SUM(tamblembur) as jmlembur,(SUM(jam1) + SUM(jam2) + SUM(jam3) + SUM(jam4) + SUM(tamb1) + SUM(tamb2)) as jmlemburdep," & _
            "SUM(jmlhasil) as jmlhasil,SUM(tothasil) as tothasil,SUM(tambmakan) as tambmakan,SUM(jharian) as harian " & _
            "from tr_hadir a inner join ms_karyawan b on a.nip=b.nip  where stat='HADIR' and step=2 and a.skalk=1 " & _
         "and b.aktif=1 and a.tanggal >='{0}' and a.tanggal <='{1}' and b.kdgol='{2}' and (a.tothasil > 0 or a.jharian>0) group by a.nip", convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue), dv2(Me.BindingContext(dv2).Position)("kode").ToString)

        Dim sqlhari As String = String.Format("select COUNT(tanggal) as jml from ms_kalender where libur=0 and YEAR(tanggal)={0} and tanggal >='{1}' and tanggal <='{2}'", ttahun.EditValue, convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue))
        Dim sqltransak As String = String.Format("select COUNT(ms_karyawan.nip) as jml from tr_gaji inner join ms_karyawan on tr_gaji.nip=ms_karyawan.nip where tr_gaji.tahun={0} and tr_gaji.minggu={1} and ms_karyawan.kdgol='{2}' and calc_by='Mingguan'", ttahun.EditValue, tcbmgu.EditValue, dv2(Me.BindingContext(dv2).Position)("kode").ToString)

        Dim cn As OleDbConnection = Nothing

        Dim ds1 As DataSet
        Dim ds2 As DataSet

        Dim dt1 As DataTable
        Dim dt2 As DataTable

        Dim cmdhari As OleDbCommand
        Dim dre As OleDbDataReader

        Dim cmdtrans As OleDbCommand
        Dim drtrans As OleDbDataReader

        Dim kharikerja As Integer
        Dim timpa As Boolean

        open_wait()

        Try



            cn = New OleDbConnection
            cn = Clsmy.open_conn

            cek_jenis_lembur(cn)
            If jenishitung_lembur = 0 Then
                MsgBox("Jenis perhitungan lembur harus diisi...", vbOKOnly + vbInformation, "Informasi")
                close_wait()
                Return
            End If

            If cek_periode_on_gaji(cn) = True Then

                close_wait()

                MsgBox("Periksa dulu tanggal awal dan akhir, karna sudah ada didalam kalkulasi lainnya", vbOKOnly + vbInformation, "Informasi")
                ttgl1.Focus()
                Return

            End If

            SetWaitDialog("Cek kalender kerja...")

            cmdhari = New OleDbCommand(sqlhari, cn)
            dre = cmdhari.ExecuteReader

            If dre.HasRows Then
                If dre.Read Then

                    If Convert.ToInt32(dre(0).ToString) = 0 Then
                        MsgBox("Isi dulu kalender kerja..", vbOKOnly + vbInformation, "Informasi")

                        dre.Close()
                        cn.Close()

                        close_wait()

                        Return
                    Else
                        kharikerja = Convert.ToInt32(dre(0).ToString)
                    End If
                Else
                    MsgBox("Isi dulu kalender kerja..", vbOKOnly + vbInformation, "Informasi")

                    dre.Close()
                    cn.Close()

                    close_wait()

                    Return
                End If
            Else
                MsgBox("Isi dulu kalender kerja..", vbOKOnly + vbInformation, "Informasi")

                dre.Close()
                cn.Close()

                close_wait()

                Return
            End If

            dre.Close()


            SetWaitDialog("Cek transaksi sebelumnya..")

            cmdtrans = New OleDbCommand(sqltransak, cn)
            drtrans = cmdtrans.ExecuteReader

            If drtrans.HasRows Then
                If drtrans.Read Then
                    If Convert.ToInt32(drtrans(0).ToString) > 0 Then
                        close_wait()
                        If MsgBox("Terdapat data yang telah dikalkulasi sebelumnya, ingin dikalkulasi ulang?", vbYesNo + vbQuestion, "Konfirmasi") = vbYes Then
                            timpa = True
                        Else
                            timpa = False
                        End If

                        open_wait()

                    Else
                        timpa = False
                    End If
                Else
                    timpa = False
                End If
            Else
                timpa = False
            End If



            ds1 = New DataSet
            ds1 = Clsmy.GetDataSet(sql, cn)
            dt1 = New DataTable
            dt1 = ds1.Tables(0)

            ds2 = New DataSet
            ds2 = Clsmy.GetDataSet(sqltr, cn)
            dt2 = New DataTable
            dt2 = ds2.Tables(0)

            Dim snip As String
            Dim sgapok As Double
            Dim stunj_jab As Double
            Dim stunj_kehadiran As Double
            Dim stunj_akomod As Double
            Dim stunj_makan As Double
            Dim sjenis_lembur As String

            Dim jmlhadir As Integer
            Dim tunj_makanlembur As Double
            Dim jmllembur As Double
            Dim jmllembur_dep As Double
            Dim gajilembur As Double
            Dim jmlhasil As Double
            Dim tothasil As Double
            Dim totharian As Double

            Dim charian As Integer = 0
            Dim jlaki As Double = 0
            Dim jperempuan As Double = 0

            Dim jniskelamin As String
            Dim ggajiharian As Double

            Dim cmd As OleDbCommand
            Dim cmds As OleDbCommand
            Dim drs As OleDbDataReader

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction

            SetWaitDialog("Kalkulasi gaji....")

            For i As Integer = 0 To dt1.Rows.Count - 1

                snip = dt1.Rows(i)("nip").ToString

                Dim hariaktif As Integer = 0

                Dim sqlhariaktif As String = String.Format("select COUNT(distinct tr_hadir.tanggal) as jmlmasuk from tr_hadir inner join ms_kalender on tr_hadir.tanggal=ms_kalender.tanggal where tr_hadir.stat in ('HADIR','SAKIT','CUTI') and tr_hadir.step=2 and tr_hadir.tanggal>='{0}' and tr_hadir.tanggal <='{1}' and tr_hadir.nip='{2}'", convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue), snip)
                Dim cmd_hr As OleDbCommand = New OleDbCommand(sqlhariaktif, cn, sqltrans)
                Dim dr_hr As OleDbDataReader = cmd_hr.ExecuteReader
                If dr_hr.Read Then
                    If IsNumeric(dr_hr(0).ToString) Then
                        hariaktif = Integer.Parse(dr_hr(0).ToString)
                    End If
                End If
                dr_hr.Close()

                sgapok = Convert.ToDouble(dt1.Rows(i)("gapok").ToString)
                stunj_jab = Convert.ToDouble(dt1.Rows(i)("tunj_jabatan").ToString)
                stunj_kehadiran = Convert.ToDouble(dt1.Rows(i)("tunj_kehadiran").ToString)
                stunj_akomod = Convert.ToDouble(dt1.Rows(i)("tunj_akomodasi").ToString)
                stunj_makan = Convert.ToDouble(dt1.Rows(i)("tunj_makan").ToString)
                sjenis_lembur = dt1.Rows(i)("jenislembur").ToString

                charian = dt1.Rows(i)("harian").ToString
                jlaki = dt1.Rows(i)("laki2").ToString
                jperempuan = dt1(i)("perempuan").ToString

                jniskelamin = dt1(i)("jniskelamin").ToString

                If jniskelamin.Equals("Laki - Laki") Then
                    ggajiharian = jlaki
                Else
                    ggajiharian = jperempuan
                End If

                Dim row As DataRow = dt2.Select(String.Format("nip ='{0}'", dt1.Rows(i)("nip").ToString)).FirstOrDefault()
                If Not row Is Nothing Then

                    jmlhadir = Convert.ToInt32(row.Item("jmlhadir").ToString)
                    tunj_makanlembur = Convert.ToDouble(row.Item("tambmakan").ToString)
                    jmllembur = Convert.ToDouble(row.Item("jmlembur").ToString)

                    If IsNumeric(jmlhadir) Then
                        If IsNumeric(stunj_makan) Then
                            stunj_makan = stunj_makan * hariaktif
                        End If
                    End If

                    jmllembur_dep = Convert.ToDouble(row.Item("jmlemburdep").ToString)
                    jmlhasil = Convert.ToDouble(row.Item("jmlhasil").ToString)
                    tothasil = Convert.ToDouble(row.Item("tothasil").ToString)
                    totharian = Convert.ToDouble(row.Item("harian").ToString)

                    If charian = 1 Then

                        If sjenis_lembur.Equals("Depnaker") Then

                            If sgapok > 0 Then

                                If jenishitung_lembur = 1 Then ' visi
                                    gajilembur = (sgapok / 7) * jmllembur_dep
                                ElseIf jenishitung_lembur = 2 Then ' grand
                                    gajilembur = (((sgapok + stunj_jab) * 3) / 20) * jmllembur_dep
                                Else ' dsa
                                    gajilembur = sgapok * 25
                                    gajilembur = (gajilembur / 173)
                                    gajilembur = gajilembur * jmllembur_dep
                                End If


                            Else

                                If jenishitung_lembur = 1 Then ' visi
                                    gajilembur = (ggajiharian / 7) * jmllembur_dep
                                ElseIf jenishitung_lembur = 2 Then ' grand
                                    gajilembur = (((ggajiharian + stunj_jab) * 3) / 20) * jmllembur_dep
                                Else ' dsa
                                    gajilembur = ggajiharian * 25
                                    gajilembur = (gajilembur / 173)
                                    gajilembur = gajilembur * jmllembur_dep
                                End If


                            End If


                        ElseIf sjenis_lembur.Equals("Jam Mati") Then

                            If sgapok > 0 Then

                                If jenishitung_lembur = 1 Then ' visi
                                    gajilembur = (sgapok / 7) * (jmllembur / 60)

                                ElseIf jenishitung_lembur = 2 Then ' grand
                                    gajilembur = (((sgapok + stunj_jab) * 3) / 20) * (jmllembur / 60)
                                Else ' dsa
                                    gajilembur = sgapok * 25 ' (((sgapok * 25) / 173) * 100) * (jmllembur / 60)
                                    gajilembur = (gajilembur / 173)
                                    gajilembur = gajilembur * (jmllembur / 60)
                                End If


                            Else

                                If jenishitung_lembur = 1 Then ' visi
                                    gajilembur = (ggajiharian / 7) * (jmllembur / 60)
                                ElseIf jenishitung_lembur = 2 Then ' grand
                                    gajilembur = (((ggajiharian + stunj_jab) * 3) / 20) * (jmllembur / 60)
                                Else ' dsa
                                    gajilembur = ggajiharian * 25 ' (((sgapok * 25) / 173) * 100) * (jmllembur / 60)
                                    gajilembur = (gajilembur / 173)
                                    gajilembur = gajilembur * (jmllembur / 60)
                                End If


                            End If


                        Else
                            gajilembur = 0
                        End If

                    Else

                        If sjenis_lembur.Equals("Depnaker") Then
                            gajilembur = ((sgapok + stunj_jab) / 173) * jmllembur_dep
                        ElseIf sjenis_lembur.Equals("Jam Mati") Then
                            gajilembur = ((sgapok + stunj_jab) / 25 / 7) * (jmllembur / 60)
                        Else
                            gajilembur = 0
                        End If

                    End If

                    


                Else

                    jmlhadir = 0 : tunj_makanlembur = 0 : jmllembur = 0 : jmllembur_dep = 0
                    jmlhasil = 0 : tothasil = 0 : totharian = 0 : gajilembur = 0

                End If

                If jmlhadir > 0 Then
                    If jmlhadir >= kharikerja Then
                    Else
                        stunj_kehadiran = 0
                    End If
                Else
                    stunj_kehadiran = 0
                End If

                Dim sqlins As String = String.Format("insert into tr_gaji (nip,calc_by,tahun,bulan,minggu,gapok,tunj_jab,tunj_hadir,jmlhadir,tunj_akomod,tunj_makan,tunj_makanlembur,jmllembur,gaji_lembur,jml_hasil,tot_hasil,tot_harian,keterangan,tanggal1,tanggal2)" & _
                                                     "values('{0}','{1}',{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},'{17}','{18}','{19}')", _
                                                     snip, "Mingguan", ttahun.EditValue, kbulan, tcbmgu.EditValue, sgapok, stunj_jab, stunj_kehadiran, jmlhadir, stunj_akomod, stunj_makan, tunj_makanlembur, Replace(jmllembur_dep, ",", "."), Replace(gajilembur, ",", "."), jmlhasil, tothasil, totharian, "", convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue))
                Dim sqlup As String = String.Format("update tr_gaji set gapok={0},tunj_jab={1},tunj_hadir={2},jmlhadir={3},tunj_akomod={4},tunj_makan={5}," & _
                            "tunj_makanlembur={6},jmllembur={7},gaji_lembur={8},jml_hasil={9},tot_hasil={10},tot_harian={11},keterangan='{12}',tanggal1='{13}',tanggal2='{14}' " & _
                            "where nip='{15}' and calc_by='Mingguan' and tahun={16} and minggu={17} and bulan={18}", sgapok, stunj_jab, stunj_kehadiran, jmlhadir, stunj_akomod, stunj_makan, tunj_makanlembur, Replace(jmllembur_dep, ",", "."), Replace(gajilembur, ",", "."), jmlhasil, tothasil, totharian, "", _
                            convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue), snip, ttahun.EditValue, tcbmgu.EditValue, kbulan)
                Dim sqls2 As String = String.Format("select id from tr_gaji where tahun={0} and minggu={1} and nip='{2}' and calc_by='Mingguan'", ttahun.EditValue, tcbmgu.EditValue, snip)

                cmds = New OleDbCommand(sqls2, cn, sqltrans)
                drs = cmds.ExecuteReader

                If drs.HasRows Then
                    If drs.Read Then
                        If timpa Then
                            cmd = New OleDbCommand(sqlup, cn, sqltrans)
                            cmd.ExecuteNonQuery()
                        End If
                    Else
                        cmd = New OleDbCommand(sqlins, cn, sqltrans)
                        cmd.ExecuteNonQuery()
                    End If

                Else
                    cmd = New OleDbCommand(sqlins, cn, sqltrans)
                    cmd.ExecuteNonQuery()
                End If

            Next

            sqltrans.Commit()

            SetWaitDialog("Load data...")
            opendata()

        Catch ex As Exception
            close_wait()
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

    End Sub

    Private Sub fgaji_mingguan_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Get_Aksesform()

        ttgl1.EditValue = Date.Now
        ttgl2.EditValue = Date.Now

        ttahun.EditValue = Year(Now)
        open_minggu()

        loadGolongan()

    End Sub

    Private Sub tsadd_Click(sender As System.Object, e As System.EventArgs) Handles tsadd.Click
        kalkulasi()
    End Sub

    Private Sub ttahun_LostFocus(sender As Object, e As System.EventArgs)

        If ttahun.Text.Trim.Length > 0 Then
            If Not IsNumeric(ttahun.Text.Trim) Then
                '  MsgBox("Tahun salah..", vbOKOnly + vbInformation, "Informasi")
                Return
            End If
        Else
            Return
        End If

        open_minggu()

        opendata()

    End Sub

    Private Sub tsref_Click(sender As System.Object, e As System.EventArgs) Handles tsref.Click
        kalkulasi()
    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        If e.Column.AbsoluteIndex = 13 Then

            If statedit = False Then
                Return
            End If

            Dim ket As String = e.Value
            Dim cid = dv1(bs1.Position)("id").ToString

            Dim sql As String = String.Format("update tr_gaji set keterangan='{0}' where id={1}", ket, cid)

            Dim cn As OleDbConnection = Nothing

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim sqltrans As OleDbTransaction = cn.BeginTransaction

                Dim comd As New OleDbCommand(sql, cn, sqltrans)
                comd.ExecuteNonQuery()

                sqltrans.Commit()

            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
            Finally


                If Not cn Is Nothing Then
                    If cn.State = ConnectionState.Open Then
                        cn.Close()
                    End If
                End If
            End Try

        End If
    End Sub

    Private Sub ttahun_Validating(sender As System.Object, e As System.ComponentModel.CancelEventArgs)
        If Not IsNumeric(ttahun.Text.Trim) Then
            e.Cancel = True
        End If
    End Sub

    Private Sub tcbbulan_SelectedIndexChanged(sender As Object, e As System.EventArgs)

        '  getTanggal()

        opendata()

    End Sub

    Private Sub tsprint_Click(sender As System.Object, e As System.EventArgs) Handles tsprint.Click

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If


        If XtraTabControl1.SelectedTabPage.Name.Equals("XtraTabPage2") Then
            grid3.ShowPrintPreview()
        Else
            grid1.ShowPrintPreview()
        End If

        
        'Using fslipgaji As New fprgaji_mingguan With {.WindowState = FormWindowState.Maximized, .sql = sqlopen}
        '    open_wait()
        '    fslipgaji.ShowDialog(Me)
        '    '  close_wait()
        'End Using

    End Sub

    Private Sub GridView2_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView2.FocusedRowChanged
        opendata()
    End Sub

    Private Sub GridView2_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles GridView2.RowCellClick
        GridView2_FocusedRowChanged(sender, Nothing)
    End Sub

    Private Sub ttgl1_Validated(sender As System.Object, e As System.EventArgs) Handles ttgl1.LostFocus

        If ttgl1.Text.Trim.Length = 0 Then
            Return
        End If

        If ttgl1.Text.Trim.Length < 10 Then
            MsgBox("Tanggal salah...", vbOKOnly + vbInformation, "Informasi")
            ttgl1.Focus()
            Return
        End If

        If Not IsDate(ttgl1.Text.Trim) Then

            MsgBox("Tanggal salah...", vbOKOnly + vbInformation, "Informasi")
            ttgl1.Focus()
            Return

        End If

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select minggu,YEAR(tanggal) from ms_kalender where tanggal='{0}'", convert_date_to_eng(ttgl1.EditValue))

            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If IsNumeric(drd(0).ToString) Then
                    tcbmgu.SelectedIndex = Integer.Parse(drd(0).ToString) - 1
                    ' tcbmgu.Text = drd(0).ToString
                    ttahun.EditValue = drd(1).ToString
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

        tcbbulan_SelectedIndexChanged(sender, Nothing)

    End Sub

    Private Sub ttgl2_Validated(sender As Object, e As System.EventArgs) Handles ttgl2.LostFocus

        If ttgl2.Text.Trim.Length = 0 Then
            Return
        End If

        If ttgl2.Text.Trim.Length < 10 Then
            MsgBox("Tanggal salah...", vbOKOnly + vbInformation, "Informasi")
            ttgl2.Focus()
            Return
        End If

        If Not IsDate(ttgl2.Text.Trim) Then

            MsgBox("Tanggal salah...", vbOKOnly + vbInformation, "Informasi")
            ttgl2.Focus()
            Return

        End If

        tcbbulan_SelectedIndexChanged(sender, Nothing)

    End Sub

    Private Sub XtraTabControl1_Selected(sender As System.Object, e As DevExpress.XtraTab.TabPageEventArgs) Handles XtraTabControl1.Selected

        If e.PageIndex = 1 Then
            buka_detail()
        End If

    End Sub

    Private Sub buka_detail()

        Dim cn As OleDbConnection = Nothing

        Try

            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim kodegol As String = dv2(Me.BindingContext(dv2).Position)("kode").ToString

            Dim sql As String = String.Format("SELECT    ms_karyawan.nama, tr_hadir.tanggal, tr_hadir.kd_shift, tr_hadir2.namaborongan, tr_hadir2.jmlhasil, tr_hadir2.price, tr_hadir2.tothasil,tr_hadir2.tambahan,tr_hadir2.total,tr_hadir2.ket " & _
                "FROM         tr_hadir INNER JOIN " & _
                      "tr_hadir2 ON tr_hadir.id = tr_hadir2.id2 INNER JOIN " & _
                      "ms_karyawan ON tr_hadir.nip = ms_karyawan.nip " & _
                    "WHERE  tr_hadir.step=2 and tr_hadir.tanggal>='{0}' and tr_hadir.tanggal<='{1}' and tr_hadir.kdgol='{2}' and (tr_hadir.tothasil> 0 or tr_hadir.jharian>0) order by ms_karyawan.nama,tr_hadir.tanggal asc", convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue), kodegol)

            Dim ds As DataSet = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim dt As DataTable = ds.Tables(0)
            grid3.DataSource = Nothing
            grid3.DataSource = dt

            close_wait()

        Catch ex As Exception
            close_wait()
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally


            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

    End Sub

End Class