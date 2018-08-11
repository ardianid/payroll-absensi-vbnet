Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Public Class fgaji_bulanan

    Private bs1 As BindingSource
    Private ds As DataSet
    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private dvmanager2 As Data.DataViewManager
    Private dv2 As Data.DataView

    Private statedit As Boolean
    Private sqlopen As String

    Private jenishitung_lembur As Integer = 0

    Private Sub loadGolongan()

        Dim cn As OleDbConnection = Nothing

        Try

            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = "select kode,nama from ms_golongan where jenisgaji='Bulanan'"

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
            tsprint_sel.Enabled = True
            tsprintall.Enabled = True
        Else
            tsprint_sel.Enabled = False
            tsprintall.Enabled = False
        End If

    End Sub

    Private Sub opendata()

        If IsNothing(dv2) Then
            Return
        End If

        If dv2.Count <= 0 Then
            Return
        End If

        sqlopen = "select a.nip,e.nip,b.nama,c.nama as golongan, " & _
            "case when e.nip=b.nip then " & _
            "0 else a.gapok end as gapok, " & _
            "case when e.nip=b.nip then " & _
            "0 else a.kurangi_gapok end as kurangi_gapok, " & _
            "a.tunj_jab,a.tunj_hadir,a.jmlhadir,a.tunj_akomod,a.tunj_makan,a.tunj_makanlembur ,a.jmllembur, " & _
            "a.gaji_lembur,a.jml_hasil,a.tot_hasil,a.keterangan,a.tot_harian,a.id,a.tahun,a.bulan,d.total as jamsos  " & _
            "from tr_gaji a inner join ms_karyawan b on a.nip=b.nip  " & _
            "inner join ms_golongan c on b.kdgol=c.kode " & _
            "left join ms_usersys4 e on b.nip=e.nip " & _
            "left join tr_iuran_jamsos d on a.tahun=d.tahun and a.bulan=d.bulan and a.nip=d.nip "

        sqlopen = String.Format("{0} where a.calc_by='Bulanan' and a.tahun={1} and a.bulan={2} and b.kdgol='{3}'", sqlopen, ttahun.Text.Trim, tcbbulan.SelectedIndex + 1, dv2(Me.BindingContext(dv2).Position)("kode").ToString)
        '  sqlopen = String.Format("{0} and a.nip not in (select nip from ms_usersys4 where namauser='{1}')", sqlopen, userprog)

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

                        If xnonkary = 1 Then
                            GoTo disable_gapok
                        End If

                        vindex = vindex + 1

                        GridView1.Columns("gapok").Visible = True
                        GridView1.Columns("gapok").VisibleIndex = vindex

                        vindex = vindex + 1

                        GridView1.Columns("kurangi_gapok").Visible = True
                        GridView1.Columns("kurangi_gapok").VisibleIndex = vindex

                    Else

                        If xgapok = 1 Then

                            If xnonkary = 1 Then
                                GoTo disable_gapok
                            End If

                            vindex = vindex + 1

                            GridView1.Columns("gapok").Visible = True
                            GridView1.Columns("gapok").VisibleIndex = vindex

                            vindex = vindex + 1

                            GridView1.Columns("kurangi_gapok").Visible = True
                            GridView1.Columns("kurangi_gapok").VisibleIndex = vindex

                        Else
                            GridView1.Columns("gapok").Visible = False
                            GridView1.Columns("gapok").VisibleIndex = -1

                            GridView1.Columns("kurangi_gapok").Visible = False
                            GridView1.Columns("kurangi_gapok").VisibleIndex = -1

                        End If

                    End If

                Else

disable_gapok:

                    GridView1.Columns("gapok").Visible = False
                    GridView1.Columns("gapok").VisibleIndex = -1

                    GridView1.Columns("kurangi_gapok").Visible = False
                    GridView1.Columns("kurangi_gapok").VisibleIndex = -1

                End If
            End If

            If IsNumeric(drutil("stunj_jab").ToString) Then
                If drutil("stunj_jab") = 1 Then

                    If xform = 0 Then

                        If xnonkary = 1 Then
                            GoTo disable_tunjjab
                        End If

                        vindex = vindex + 1

                        GridView1.Columns("tunj_jab").Visible = True
                        GridView1.Columns("tunj_jab").VisibleIndex = vindex

                    Else

                        If xtunj_jab = 1 Then

                            If xnonkary = 1 Then
                                GoTo disable_tunjjab
                            End If

                            vindex = vindex + 1

                            GridView1.Columns("tunj_jab").Visible = True
                            GridView1.Columns("tunj_jab").VisibleIndex = vindex

                        Else

                            GridView1.Columns("tunj_jab").Visible = False
                            GridView1.Columns("tunj_jab").VisibleIndex = -1

                        End If

                    End If

                Else

disable_tunjjab:

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

                        If xnonkary = 1 Then
                            GoTo disable_tunjhadir
                        End If

                        vindex = vindex + 1

                        GridView1.Columns("tunj_hadir").Visible = True
                        GridView1.Columns("tunj_hadir").VisibleIndex = vindex

                    Else

                        If xtunj_hdr = 1 Then

                            If xnonkary = 1 Then
                                GoTo disable_tunjhadir
                            End If

                            vindex = vindex + 1

                            GridView1.Columns("tunj_hadir").Visible = True
                            GridView1.Columns("tunj_hadir").VisibleIndex = vindex

                        Else

                            GridView1.Columns("tunj_hadir").Visible = False
                            GridView1.Columns("tunj_hadir").VisibleIndex = -1

                        End If

                    End If

                Else

disable_tunjhadir:

                    GridView1.Columns("tunj_hadir").Visible = False
                    GridView1.Columns("tunj_hadir").VisibleIndex = -1
                End If
            End If

            If IsNumeric(drutil("stunjakomod").ToString) Then
                If drutil("stunjakomod") = 1 Then

                    If xform = 0 Then

                        If xnonkary = 1 Then
                            GoTo disable_tunjakomod
                        End If

                        vindex = vindex + 1

                        GridView1.Columns("tunj_akomod").Visible = True
                        GridView1.Columns("tunj_akomod").VisibleIndex = vindex

                    Else

                        If xtunj_trans = 1 Then

                            If xnonkary = 1 Then
                                GoTo disable_tunjakomod
                            End If

                            vindex = vindex + 1

                            GridView1.Columns("tunj_akomod").Visible = True
                            GridView1.Columns("tunj_akomod").VisibleIndex = vindex

                        Else

                            GridView1.Columns("tunj_akomod").Visible = False
                            GridView1.Columns("tunj_akomod").VisibleIndex = -1

                        End If

                    End If


                Else

disable_tunjakomod:

                    GridView1.Columns("tunj_akomod").Visible = False
                    GridView1.Columns("tunj_akomod").VisibleIndex = -1

                End If
            End If

            If IsNumeric(drutil("stunjmakan").ToString) Then
                If drutil("stunjmakan") = 1 Then

                    If xform = 0 Then

                        If xnonkary = 1 Then
                            GoTo disable_tunjmakan
                        End If

                        vindex = vindex + 1

                        GridView1.Columns("tunj_makan").Visible = True
                        GridView1.Columns("tunj_makan").VisibleIndex = vindex

                    Else

                        If xtunj_makan = 1 Then

                            If xnonkary = 1 Then
                                GoTo disable_tunjmakan
                            End If

                            vindex = vindex + 1

                            GridView1.Columns("tunj_makan").Visible = True
                            GridView1.Columns("tunj_makan").VisibleIndex = vindex

                        Else

                            GridView1.Columns("tunj_makan").Visible = False
                            GridView1.Columns("tunj_makan").VisibleIndex = -1

                        End If

                    End If


                Else

disable_tunjmakan:

                    GridView1.Columns("tunj_makan").Visible = False
                    GridView1.Columns("tunj_makan").VisibleIndex = -1


                End If
            End If

            If IsNumeric(drutil("stambmakan").ToString) Then
                If drutil("stambmakan") = 1 Then


                    If xform = 0 Then

                        If xnonkary = 1 Then
                            GoTo disable_tamblembur
                        End If

                        vindex = vindex + 1

                        GridView1.Columns("tunj_makanlembur").Visible = True
                        GridView1.Columns("tunj_makanlembur").VisibleIndex = vindex

                    Else

                        If xtamb_makan = 1 Then

                            If xnonkary = 1 Then
                                GoTo disable_tamblembur
                            End If

                            vindex = vindex + 1

                            GridView1.Columns("tunj_makanlembur").Visible = True
                            GridView1.Columns("tunj_makanlembur").VisibleIndex = vindex

                        Else
                            GridView1.Columns("tunj_makanlembur").Visible = False
                            GridView1.Columns("tunj_makanlembur").VisibleIndex = -1

                        End If


                    End If

                Else

disable_tamblembur:

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

    Private Sub kalkulasi()

        If IsNothing(dv2) Then
            Return
        End If

        If dv2.Count <= 0 Then
            Return
        End If

        If ttahun.Text.Trim.Length > 0 Then
            If Not IsNumeric(ttahun.Text.Trim) Then
                '  MsgBox("Tahun salah..", vbOKOnly + vbInformation, "Informasi")
                Return
            End If
        Else
            Return
        End If

        Dim sql As String = String.Format("select a.nip,a.nama,b.nama as golongan,a.gapok,a.tunj_jabatan,a.tunj_kehadiran,a.tunj_akomodasi,a.tunj_makan,b.jenislembur,b.Harian,b.laki2,b.perempuan,a.jniskelamin,a.tgl_mulai" & _
                " from ms_karyawan a inner join ms_golongan b on a.kdgol=b.kode where a.aktif=1 and b.jenisgaji='Bulanan' and a.kdgol='{0}'", dv2(Me.BindingContext(dv2).Position)("kode").ToString)

        Dim sqltr As String = String.Format("select a.nip,COUNT(stat) as jmlhadir,SUM(jamlembur) + SUM(tamblembur) as jmlembur,(SUM(jam1) + SUM(jam2) + SUM(jam3) + SUM(jam4) + SUM(tamb1) + SUM(tamb2)) as jmlemburdep," & _
            "SUM(jmlhasil) as jmlhasil,SUM(tothasil) as tothasil,SUM(tambmakan) as tambmakan,SUM(jharian) as harian " & _
            "from tr_hadir a inner join ms_karyawan b on a.nip=b.nip  where stat='HADIR' and step=2 and a.skalk=1 " & _
         "and b.aktif=1 and YEAR(a.tanggal)={0} and MONTH(a.tanggal)={1} and b.kdgol='{2}' group by a.nip", ttahun.Text.Trim, tcbbulan.SelectedIndex + 1, dv2(Me.BindingContext(dv2).Position)("kode").ToString)

        Dim sqlhari As String = String.Format("select COUNT(tanggal) as jml from ms_kalender where libur=0 and YEAR(tanggal)={0} and MONTH(tanggal)={1}", ttahun.Text.Trim, tcbbulan.SelectedIndex + 1)
        Dim sqltransak As String = String.Format("select COUNT(ms_karyawan.nip) as jml from tr_gaji tr_gaji inner join ms_karyawan on tr_gaji.nip=ms_karyawan.nip where tr_gaji.tahun={0} and tr_gaji.bulan={1} and ms_karyawan.kdgol='{2}' and calc_by='Bulanan'", ttahun.Text.Trim, tcbbulan.SelectedIndex + 1, dv2(Me.BindingContext(dv2).Position)("kode").ToString)

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
                Dim hariaktifreal As Integer = 0

                Dim sqlhariaktif As String = String.Format("select COUNT(distinct tr_hadir.tanggal) as jmlmasuk from tr_hadir inner join ms_kalender on tr_hadir.tanggal=ms_kalender.tanggal where tr_hadir.stat in ('HADIR','SAKIT','CUTI') and tr_hadir.step=2 and MONTH(tr_hadir.tanggal)={0} and YEAR(tr_hadir.tanggal)={1} and tr_hadir.nip='{2}'", tcbbulan.SelectedIndex + 1, ttahun.Text.Trim, snip)
                Dim cmd_hr As OleDbCommand = New OleDbCommand(sqlhariaktif, cn, sqltrans)
                Dim dr_hr As OleDbDataReader = cmd_hr.ExecuteReader
                If dr_hr.Read Then
                    If IsNumeric(dr_hr(0).ToString) Then
                        hariaktif = Integer.Parse(dr_hr(0).ToString)
                    End If
                End If
                dr_hr.Close()

                Dim sqlhariaktif2 As String = String.Format("select COUNT(distinct tr_hadir.tanggal) as jmlmasuk from tr_hadir inner join ms_kalender on tr_hadir.tanggal=ms_kalender.tanggal where tr_hadir.skalk=1 and tr_hadir.stat in ('HADIR') and tr_hadir.step=2 and MONTH(tr_hadir.tanggal)={0} and YEAR(tr_hadir.tanggal)={1} and tr_hadir.nip='{2}'", tcbbulan.SelectedIndex + 1, ttahun.Text.Trim, snip)
                Dim cmd_hr2 As OleDbCommand = New OleDbCommand(sqlhariaktif2, cn, sqltrans)
                Dim dr_hr2 As OleDbDataReader = cmd_hr2.ExecuteReader
                If dr_hr2.Read Then
                    If IsNumeric(dr_hr2(0).ToString) Then
                        hariaktifreal = Integer.Parse(dr_hr2(0).ToString)
                    End If
                End If
                dr_hr2.Close()

                sgapok = Convert.ToDouble(dt1.Rows(i)("gapok").ToString)
                stunj_jab = Convert.ToDouble(dt1.Rows(i)("tunj_jabatan").ToString)

                Dim potongan_gaji As Double = 0
                If hariaktif < kharikerja Then
                    potongan_gaji = ((sgapok + stunj_jab) / 25) * (kharikerja - hariaktif)
                End If

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
                            stunj_makan = stunj_makan * hariaktifreal
                        End If
                    End If

                    jmllembur = jmllembur / 60

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
                                    gajilembur = (sgapok / 7) * jmllembur_dep
                                End If


                            Else

                                If jenishitung_lembur = 1 Then ' visi
                                    gajilembur = (ggajiharian / 7) * jmllembur_dep
                                ElseIf jenishitung_lembur = 2 Then ' grand
                                    gajilembur = (((ggajiharian + stunj_jab) * 3) / 20) * jmllembur_dep
                                Else
                                    gajilembur = (ggajiharian / 7) * jmllembur_dep
                                End If


                            End If


                        ElseIf sjenis_lembur.Equals("Jam Mati") Then

                            If sgapok > 0 Then

                                If jenishitung_lembur = 1 Then ' visi
                                    gajilembur = (sgapok / 7) * (jmllembur / 60)
                                ElseIf jenishitung_lembur = 2 Then ' grand
                                    gajilembur = (((sgapok + stunj_jab) * 3) / 20) * (jmllembur / 60)
                                Else ' dsa
                                    gajilembur = (sgapok / 7) * (jmllembur / 60)
                                End If


                            Else

                                If jenishitung_lembur = 1 Then ' visi
                                    gajilembur = (ggajiharian / 7) * (jmllembur / 60)
                                ElseIf jenishitung_lembur = 2 Then ' grand
                                    gajilembur = (((ggajiharian + stunj_jab) * 3) / 20) * (jmllembur / 60)
                                Else ' dsa
                                    gajilembur = (ggajiharian / 7) * (jmllembur / 60)
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

                    'If sjenis_lembur.Equals("Depnaker") Then
                    '    gajilembur = ((sgapok + stunj_jab) / 173) * jmllembur_dep
                    'ElseIf sjenis_lembur.Equals("Jam Mati") Then
                    '    gajilembur = ((sgapok + stunj_jab) / 25 / 7) * (jmllembur)
                    'Else
                    '    gajilembur = 0
                    'End If


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


                Dim tglmulai As String = dt1.Rows(i)("tgl_mulai").ToString
                Dim tbln As String = tcbbulan.SelectedIndex + 1
                If tbln.Length = 1 Then
                    tbln = String.Format("0{0}", tbln)
                End If
                Dim tglawalbulan = String.Format("01/{0}/{1}", tbln, ttahun.Text.Trim)

                If IsDate(tglmulai) Then
                    If DateValue(tglmulai) > DateValue(tglawalbulan) Then
                        sgapok = (sgapok + stunj_jab) / 25
                        sgapok = sgapok * hariaktifreal
                        potongan_gaji = 0
                    End If
                End If

                Dim sqlins As String = String.Format("insert into tr_gaji (nip,calc_by,tahun,bulan,minggu,gapok,tunj_jab,tunj_hadir,jmlhadir,tunj_akomod,tunj_makan,tunj_makanlembur,jmllembur,gaji_lembur,jml_hasil,tot_hasil,tot_harian,keterangan,kurangi_gapok)" & _
                                                     "values('{0}','{1}',{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},'{17}',{18})", _
                                                     snip, "Bulanan", ttahun.Text.Trim, tcbbulan.SelectedIndex + 1, 0, Replace(sgapok, ",", "."), stunj_jab, stunj_kehadiran, hariaktif, stunj_akomod, stunj_makan, tunj_makanlembur, Replace(jmllembur_dep, ",", "."), Replace(gajilembur, ",", "."), jmlhasil, tothasil, totharian, "", potongan_gaji)
                Dim sqlup As String = String.Format("update tr_gaji set gapok={0},tunj_jab={1},tunj_hadir={2},jmlhadir={3},tunj_akomod={4},tunj_makan={5}," & _
                            "tunj_makanlembur={6},jmllembur={7},gaji_lembur={8},jml_hasil={9},tot_hasil={10},tot_harian={11},keterangan='{12}',kurangi_gapok={13}" & _
                            "where nip='{14}' and calc_by='Bulanan' and tahun={15} and bulan={16} and minggu=0", Replace(sgapok, ",", "."), stunj_jab, stunj_kehadiran, hariaktif, stunj_akomod, stunj_makan, tunj_makanlembur, Replace(jmllembur_dep, ",", "."), Replace(gajilembur, ",", "."), jmlhasil, tothasil, totharian, "", potongan_gaji, _
                            snip, ttahun.Text.Trim, tcbbulan.SelectedIndex + 1)
                Dim sqls2 As String = String.Format("select id from tr_gaji where tahun={0} and bulan={1} and nip='{2}' and calc_by='Bulanan'", ttahun.Text.Trim, tcbbulan.SelectedIndex + 1, snip)

                Dim sqldel As String = String.Format("delete from tr_gaji where nip='{0}' and calc_by='Bulanan' and tahun={1} and bulan={2} and minggu=0", snip, ttahun.Text.Trim, tcbbulan.SelectedIndex + 1)

                cmds = New OleDbCommand(sqls2, cn, sqltrans)
                drs = cmds.ExecuteReader

                If drs.HasRows Then
                    If drs.Read Then
                        If timpa Then

                            Using cmddel As OleDbCommand = New OleDbCommand(sqldel, cn, sqltrans)
                                cmddel.ExecuteNonQuery()
                            End Using

                            cmd = New OleDbCommand(sqlins, cn, sqltrans)
                            cmd.ExecuteNonQuery()

                            'cmd = New OleDbCommand(sqlup, cn, sqltrans)
                            'cmd.ExecuteNonQuery()
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

    Private Sub fgaji_bulanan_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Get_Aksesform()

        ttahun.Text = Year(Now)

        Dim bln As Integer = Month(Date.Now)
        tcbbulan.SelectedIndex = bln - 1

        loadGolongan()

    End Sub

    Private Sub tsadd_Click(sender As System.Object, e As System.EventArgs) Handles tsadd.Click
        kalkulasi()
    End Sub

    Private Sub ttahun_LostFocus(sender As Object, e As System.EventArgs) Handles ttahun.LostFocus

        If ttahun.Text.Trim.Length > 0 Then
            If Not IsNumeric(ttahun.Text.Trim) Then
                '  MsgBox("Tahun salah..", vbOKOnly + vbInformation, "Informasi")
                Return
            End If
        Else
            Return
        End If

        opendata()

    End Sub

    Private Sub tsref_Click(sender As System.Object, e As System.EventArgs) Handles tsref.Click
        ttahun_LostFocus(sender, Nothing)
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

    Private Sub ttahun_Validating(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles ttahun.Validating
        If Not IsNumeric(ttahun.Text.Trim) Then
            e.Cancel = True
        End If
    End Sub

    Private Sub tcbbulan_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles tcbbulan.SelectedIndexChanged
        opendata()
    End Sub

    Private Sub tsprintall_Click(sender As System.Object, e As System.EventArgs) Handles tsprintall.Click

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If


        Using fslipgaji As New fprgaji_bulanan With {.WindowState = FormWindowState.Maximized, .sql = sqlopen}
            open_wait()
            fslipgaji.ShowDialog(Me)
            '  close_wait()
        End Using


    End Sub

    Private Sub tsprint_sel_Click(sender As System.Object, e As System.EventArgs) Handles tsprint_sel.Click


        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        Dim sql As String = sqlopen
        sql = String.Format("{0} and a.nip='{1}'", sql, dv1(bs1.Position)("nip").ToString)

        Using fslipgaji As New fprgaji_bulanan With {.StartPosition = FormStartPosition.CenterScreen, .sql = sql}
            open_wait()
            fslipgaji.ShowDialog(Me)
        End Using

    End Sub

    Private Sub GridView2_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView2.FocusedRowChanged
        opendata()
    End Sub

    Private Sub GridView2_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles GridView2.RowCellClick
        GridView2_FocusedRowChanged(sender, Nothing)
    End Sub

End Class