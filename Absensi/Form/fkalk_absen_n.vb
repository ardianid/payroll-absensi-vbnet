Imports System.Data.OleDb
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Public Class fkalk_absen_n

    Dim realmasuk As DateTime
    Dim realkeluar As DateTime

    Dim namashift As String
    Dim hitungan_jam As Integer
    Dim jadwalmasuk As DateTime
    Dim jadwalmasuk_awl As DateTime
    Dim jadwalmasuk_akh As DateTime
    Dim jadwalmasuk_tol As DateTime
    Dim jadwalkeluar As DateTime
    Dim jadwalkeluar_awl As DateTime
    Dim jadwalkeluar_akh As DateTime
    Dim jadwalkeluar_tol As DateTime

    Dim idmesin As Integer
    Dim nnip As String
    Dim nnip_sebelum As String

    Dim gapok As Double
    Dim tunj_jabatan As Double
    Dim tunj_kehadiran As Double
    Dim tuangmakan As Double
    Dim tuangmakan_inap As Double
    Dim tamb_makan As Double
    Dim standby_uang As Double

    Dim jenisgol As String
    Dim jenislembur As String

    Dim sharian As Integer
    Dim laki1_uang As Double
    Dim perempuan_uang As Double

    Dim liburnormal As Integer

    Dim depart As String
    Dim jenisgaji As String
    Dim jniskelamin As String

    Dim lembur_perjam As Double

    Dim jenishitung_lembur As Integer = 0
    Dim awalshiftoto As String

    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private dvmanager2 As Data.DataViewManager
    Private dv2 As Data.DataView

    Private dvmanager3 As Data.DataViewManager
    Private dv3 As Data.DataView

    Private nama_hari_ht As String
    Private libur_ht As String
    Private jenislibur_hit As String

    Private isload As Boolean

    Dim bln_ As String
    Dim thn_ As String

    Private Sub loadGolongan()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = "select kode,nama from ms_golongan where tampilgroup=1 and saktif=1"

            Dim dsgol As New DataSet
            dsgol = Clsmy.GetDataSet(sql, cn)

            Dim dt As DataTable = dsgol.Tables(0)

            'Dim orow As DataRow = dt.NewRow
            'orow("kode") = "ALL"
            'orow("nama") = "ALL"

            'dt.Rows.InsertAt(orow, 0)

            cb_gol.Properties.DataSource = dt

            cb_gol.ItemIndex = 0

            'cb_gol.EditValue = "ALL"

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

    Private Sub loadDepartemen()

        cb_depart.Properties.DataSource = Nothing

        If cb_gol.EditValue = "" Then
            Return
        End If

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select * from ms_depart where nama in (select distinct depart from ms_karyawan where kdgol='{0}')", cb_gol.EditValue)

            Dim dsgol As New DataSet
            dsgol = Clsmy.GetDataSet(sql, cn)

            Dim dt As DataTable = dsgol.Tables(0)

            Dim orow As DataRow = dt.NewRow
            orow("nama") = "ALL"

            dt.Rows.InsertAt(orow, 0)

            cb_depart.Properties.DataSource = dt

            cb_depart.EditValue = "ALL"

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

    Private Sub load_pegawai()

        cbpeg.Properties.DataSource = Nothing

        Dim cn As OleDbConnection = Nothing

        Dim sql As String = "select nip,nama from ms_karyawan where shiden=0"

        If caktif.Checked = True Then
            sql = String.Format(" {0} and aktif=0", sql)

        Else
            sql = String.Format(" {0} and aktif=1", sql)

        End If


        sql = String.Format("{0} and kdgol='{1}'", sql, cb_gol.EditValue)

        If Not cb_depart.EditValue = "ALL" Then
            sql = String.Format("{0} and depart='{1}'", sql, cb_depart.EditValue)
        End If

        sql = String.Format("{0} order by nama", sql)

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim dtpegawai As DataTable
            dtpegawai = ds.Tables(0)

            Dim orow As DataRow = dtpegawai.NewRow
            orow("nip") = "ALL"
            orow("nama") = "ALL"
            dtpegawai.Rows.InsertAt(orow, 0)

            cbpeg.Properties.DataSource = dtpegawai

            cbpeg.ItemIndex = 0

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

        End Try

    End Sub

    Private Sub load_RJamKerja()

        Dim ds As DataSet
        Dim cn As OleDbConnection = Nothing

        Try

            Dim sql As String = "select kode,SUBSTRING(CONVERT(varchar(20),CONVERT(time(0),jammasuk),108),1,5) as jammasuk,SUBSTRING(CONVERT(varchar(20),CONVERT(time(0),jampulang),108),1,5) as jampulang from ms_jamkerja"

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            ds = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim dt As DataTable = ds.Tables(0)

            Dim orow As DataRow = dt.NewRow
            orow("kode") = "-"
            orow("jammasuk") = "-"
            orow("jampulang") = "-"

            dt.Rows.InsertAt(orow, 0)

            rshift.DataSource = Nothing
            rshift.DataSource = dt

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

    Private Sub cek_hari(ByVal for_obj As Boolean, ByVal tanggal As Date)

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select hari,case when libur=1 then 'LIBUR' else 'KERJA' end as 'libur',jenislibur from ms_kalender where tanggal='{0}'", convert_date_to_eng(tanggal))
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then

                If for_obj Then
                    thari.EditValue = drd("hari").ToString
                    tlibur.EditValue = drd("libur").ToString
                Else
                    nama_hari_ht = drd("hari").ToString
                    libur_ht = drd("libur").ToString
                    jenislibur_hit = drd("jenislibur").ToString

                    If jenislibur_hit.Trim.ToUpper = "LIBUR NORMAL" Then
                        jenislibur_hit = "LIBUR"
                    ElseIf jenislibur_hit.Trim.ToUpper = "LIBUR HARI BESAR" Then
                        jenislibur_hit = "LIBUR NASIONAL"
                    Else
                        jenislibur_hit = "KERJA"
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

    Private Sub load_grid()

        Dim sql As String = String.Format("select b.id,a.nip,a.nama,c.nama as namagol,b.tanggal,b.jammasuk,b.jampulang,b.skalk," _
            & "b.stelat,b.spulangcpat,b.stat,b.keterangan,b.jamlembur / 60 as jamlembur,b.lemburperjam,b.jadwalmasuk,b.jadwalpulang," _
            & "b.totlembur,b.hasilper,b.jmlhasil,b.tothasil,b.tambmakan,b.jamkerja,b.jam1,b.jam2,b.jam3,b.jam4,(b.jam1+b.jam2+b.jam3+b.jam4) as lemburdep," _
            & "b.jharian,a.kdgol,c.jnisrange,b.jmltelat,b.kdgol as kdgol_tr,b.tamblembur / 60 as tamblembur,b.tamb1,b.tamb2,b.kd_shift,d.hari,d.libur,b.uangmakan,b.stat_lmbr,b.stat_hari,b.tamb_istirahat as tambisti" _
            & " from ms_karyawan a inner join tr_hadir b on a.nip=b.nip" _
            & " inner join ms_golongan c on a.kdgol=c.kode" _
            & " inner join ms_kalender d on b.tanggal=d.tanggal" _
            & " where a.shiden=0 and b.tanggal >='{0}' and b.tanggal <='{1}' and c.kode='{2}'", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue), cb_gol.EditValue)

        If caktif.Checked = True Then
            sql = String.Format(" {0} and a.aktif=0", sql)

            For Each column As DevExpress.XtraGrid.Columns.GridColumn In GridView1.Columns
                column.OptionsColumn.[ReadOnly] = True
            Next

        Else
            sql = String.Format(" {0} and a.aktif=1", sql)

            For Each column As DevExpress.XtraGrid.Columns.GridColumn In GridView1.Columns
                column.OptionsColumn.[ReadOnly] = False
            Next

        End If

        If Not cb_depart.EditValue.ToString.Equals("ALL") Then
            sql = String.Format(" {0} and a.depart='{1}'", sql, cb_depart.EditValue)
        End If

        If Not cbpeg.EditValue.ToString.Equals("ALL") Then
            sql = String.Format(" {0} and a.nip='{1}'", sql, cbpeg.EditValue)
        End If


        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            dv1 = Nothing

            Dim ds As DataSet
            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager = New DataViewManager(ds)
            dv1 = dvmanager.CreateDataView(ds.Tables(0))

            grid1.DataSource = dv1

            load_kemungkinan_salah(cn)

            If caktif.Checked = True Then

                For Each column As DevExpress.XtraGrid.Columns.GridColumn In GridView1.Columns
                    column.OptionsColumn.[ReadOnly] = True
                Next

            Else

                For Each column As DevExpress.XtraGrid.Columns.GridColumn In GridView1.Columns
                    column.OptionsColumn.[ReadOnly] = False
                Next

            End If

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

    Private Sub load_kemungkinan_salah(ByVal cn As OleDbConnection)

        Dim sql As String = String.Format("select tr_hadir.tanggal,ms_karyawan.nama,'Jam Kerja >=12' as ket " & _
        "from tr_hadir inner join ms_karyawan on tr_hadir.nip=ms_karyawan.nip " & _
        "where tr_hadir.jamkerja >=12 and tr_hadir.tanggal>='{0}' and tr_hadir.tanggal<='{1}' and ms_karyawan.kdgol='{2}'", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue), cb_gol.EditValue)

        If Not cb_depart.EditValue = "ALL" Then
            sql = String.Format("{0} and ms_karyawan.depart='{1}'", sql, cb_depart.EditValue)
        End If

        If Not cbpeg.EditValue = "ALL" Then
            sql = String.Format("{0} and ms_karyawan.nip='{1}'", sql, cbpeg.EditValue)
        End If

        Dim sql2 As String = String.Format("select distinct CONVERT(datetime, CAST(CONVERT(date, ms_inout.checktime) AS varchar) + ' ' + CAST(LEFT(CONVERT(VARCHAR, CONVERT(time(0), ms_inout.checktime), 108), 5) AS varchar)) " & _
        "AS tanggal,ms_karyawan.nama,'Tidak TerHitung' " & _
        "from ms_inout inner join ms_karyawan on ms_inout.userid=ms_karyawan.idmesin " & _
        "where ms_inout.skalk=0 and CONVERT(date,ms_inout.checktime)>='{0}' and CONVERT(date,ms_inout.checktime)<='{1}' and ms_karyawan.kdgol='{2}'", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue), cb_gol.EditValue)

        If Not cb_depart.EditValue = "ALL" Then
            sql2 = String.Format("{0} and ms_karyawan.depart='{1}'", sql2, cb_depart.EditValue)
        End If

        If Not cbpeg.EditValue = "ALL" Then
            sql2 = String.Format("{0} and ms_karyawan.nip='{1}'", sql2, cbpeg.EditValue)
        End If

        Dim sqljadi As String = String.Format("{0} union all {1}", sql, sql2)

        dv3 = Nothing

        Dim ds As DataSet
        ds = New DataSet()
        ds = Clsmy.GetDataSet(sqljadi, cn)

        dvmanager3 = New DataViewManager(ds)
        dv3 = dvmanager3.CreateDataView(ds.Tables(0))

        gridtidak.DataSource = dv3

    End Sub

    Private Sub load_grid_pernip(ByVal idhadir As Integer)

        Dim sql As String = String.Format("select b.id,a.nip,a.nama,c.nama as namagol,b.tanggal,b.jammasuk,b.jampulang,b.skalk," _
            & "b.stelat,b.spulangcpat,b.stat,b.keterangan,b.jamlembur / 60 as jamlembur,b.lemburperjam,b.jadwalmasuk,b.jadwalpulang," _
            & "b.totlembur,b.hasilper,b.jmlhasil,b.tothasil,b.tambmakan,b.jamkerja,b.jam1,b.jam2,b.jam3,b.jam4,(b.jam1+b.jam2+b.jam3+b.jam4) as lemburdep," _
            & "b.jharian,a.kdgol,c.jnisrange,b.jmltelat,b.kdgol as kdgol_tr,b.tamblembur / 60 as tamblembur,b.tamb1,b.tamb2,b.kd_shift,d.hari,d.libur,b.uangmakan,b.stat_lmbr,b.stat_hari,b.tamb_istirahat as tambisti" _
            & " from ms_karyawan a inner join tr_hadir b on a.nip=b.nip" _
            & " inner join ms_golongan c on a.kdgol=c.kode" _
            & " inner join ms_kalender d on b.tanggal=d.tanggal" _
            & " where a.shiden=0 and b.id={0}", idhadir)

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim cmdload2 As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drdload2 As OleDbDataReader = cmdload2.ExecuteReader

            If drdload2.Read Then
                If IsNumeric(drdload2("id").ToString) Then

                    dv1(Me.BindingContext(dv1).Position)("kd_shift") = drdload2("kd_shift").ToString
                    dv1(Me.BindingContext(dv1).Position)("skalk") = drdload2("skalk").ToString
                    dv1(Me.BindingContext(dv1).Position)("stelat") = drdload2("stelat").ToString
                    dv1(Me.BindingContext(dv1).Position)("spulangcpat") = drdload2("spulangcpat").ToString
                    dv1(Me.BindingContext(dv1).Position)("stat") = drdload2("stat").ToString
                    dv1(Me.BindingContext(dv1).Position)("stat_lmbr") = drdload2("stat_lmbr").ToString
                    dv1(Me.BindingContext(dv1).Position)("jamlembur") = drdload2("jamlembur").ToString
                    dv1(Me.BindingContext(dv1).Position)("lemburperjam") = drdload2("lemburperjam").ToString
                    dv1(Me.BindingContext(dv1).Position)("jammasuk") = IIf(drdload2("jammasuk").ToString.Trim.Length = 0, DBNull.Value, drdload2("jammasuk").ToString)
                    dv1(Me.BindingContext(dv1).Position)("jampulang") = IIf(drdload2("jampulang").ToString.Trim.Length = 0, DBNull.Value, drdload2("jampulang").ToString)
                    dv1(Me.BindingContext(dv1).Position)("jadwalmasuk") = IIf(drdload2("jadwalmasuk").ToString.Trim.Length = 0, DBNull.Value, drdload2("jadwalmasuk").ToString)
                    dv1(Me.BindingContext(dv1).Position)("jadwalpulang") = IIf(drdload2("jadwalpulang").ToString.Trim.Length = 0, DBNull.Value, drdload2("jadwalpulang").ToString)
                    dv1(Me.BindingContext(dv1).Position)("totlembur") = drdload2("totlembur").ToString
                    dv1(Me.BindingContext(dv1).Position)("hasilper") = drdload2("hasilper").ToString
                    dv1(Me.BindingContext(dv1).Position)("jmlhasil") = drdload2("jmlhasil").ToString
                    dv1(Me.BindingContext(dv1).Position)("tothasil") = drdload2("tothasil").ToString
                    dv1(Me.BindingContext(dv1).Position)("uangmakan") = drdload2("uangmakan").ToString
                    dv1(Me.BindingContext(dv1).Position)("tambmakan") = drdload2("tambmakan").ToString
                    dv1(Me.BindingContext(dv1).Position)("jamkerja") = drdload2("jamkerja").ToString
                    dv1(Me.BindingContext(dv1).Position)("jam1") = drdload2("jam1").ToString
                    dv1(Me.BindingContext(dv1).Position)("jam2") = drdload2("jam2").ToString
                    dv1(Me.BindingContext(dv1).Position)("jam3") = drdload2("jam3").ToString
                    dv1(Me.BindingContext(dv1).Position)("jam4") = drdload2("jam4").ToString
                    dv1(Me.BindingContext(dv1).Position)("lemburdep") = drdload2("lemburdep").ToString
                    dv1(Me.BindingContext(dv1).Position)("jharian") = drdload2("jharian").ToString
                    dv1(Me.BindingContext(dv1).Position)("jmltelat") = drdload2("jmltelat").ToString
                    dv1(Me.BindingContext(dv1).Position)("tamblembur") = drdload2("tamblembur").ToString
                    dv1(Me.BindingContext(dv1).Position)("tamb1") = drdload2("tamb1").ToString
                    dv1(Me.BindingContext(dv1).Position)("tamb2") = drdload2("tamb2").ToString
                    dv1(Me.BindingContext(dv1).Position)("tambisti") = drdload2("tambisti").ToString

                    dv1(Me.BindingContext(dv1).Position)("hari") = drdload2("hari").ToString
                    dv1(Me.BindingContext(dv1).Position)("libur") = drdload2("libur").ToString

                End If
            End If
            drdload2.Close()

            load_kemungkinan_salah(cn)

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

    Private Sub mulai_hitung(ByVal tanggal_kalk As Date, ByVal tanggal_kalk2 As Date, ByVal nip_param As String, ByVal shift_param As String, id_param As Integer, ByVal masuksebelum As String, ByVal keluarsebelum As String, ByVal jd_masuksebelum As String, ByVal jd_keluarsebelum As String)

        Dim sql As String = "select ms_karyawan.idmesin,ms_karyawan.nip,ms_karyawan.nama, " & _
       "ms_karyawan.gapok, ms_karyawan.tunj_jabatan, ms_karyawan.tunj_kehadiran, ms_karyawan.tunj_makan,ms_karyawan.tunj_makan_inap, " & _
       "ms_karyawan.tamb_makanlembur, ms_karyawan.[standby],ms_karyawan.depart,ms_karyawan.kdgol," & _
       "ms_golongan.jnisgol,ms_golongan.jenislembur,ms_golongan.harian,ms_golongan.laki2,ms_golongan.perempuan,ms_karyawan.liburnormal,ms_golongan.jenisgaji,ms_karyawan.jniskelamin,ms_karyawan.lembur_perjam " & _
       "from ms_karyawan inner join ms_golongan on ms_karyawan.kdgol=ms_golongan.kode " & _
       "where ms_karyawan.aktif=1 and  ms_golongan.saktif=1 and ms_karyawan.shiden=0"

        If Not cb_gol.EditValue = "ALL" Then
            sql = String.Format("{0} and ms_karyawan.kdgol='{1}'", sql, cb_gol.EditValue)
        End If

        If Not cb_depart.EditValue = "ALL" Then
            sql = String.Format("{0} and ms_karyawan.depart='{1}'", sql, cb_depart.EditValue)
        End If

        If Not cbpeg.EditValue = "ALL" Then
            sql = String.Format("{0} and ms_karyawan.nip='{1}'", sql, cbpeg.EditValue)
        End If

        If nip_param.Trim.Length > 0 Then
            sql = String.Format("{0} and ms_karyawan.nip='{1}'", sql, nip_param)
        End If

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqlawal As String = "select * from ms_awalshift"
            Dim cmdawal As OleDbCommand = New OleDbCommand(sqlawal, cn)
            Dim drdawal As OleDbDataReader = cmdawal.ExecuteReader
            If drdawal.Read Then
                awalshiftoto = drdawal("namahari").ToString.ToUpper
                jenishitung_lembur = Integer.Parse(drdawal("jnis_hit_harian").ToString)
            End If
            drdawal.Close()

            '' loop hari
            Dim sqlhari As String = String.Format("select tanggal from ms_kalender where tanggal>='{0}' and tanggal<='{1}'", convert_date_to_eng(tanggal_kalk), convert_date_to_eng(tanggal_kalk2))
            Dim cmdhari As OleDbCommand = New OleDbCommand(sqlhari, cn)
            Dim drdhari As OleDbDataReader = cmdhari.ExecuteReader
            Dim dthari As DataTable = New DataTable
            dthari.Load(drdhari)

            If dthari.Rows.Count <= 0 Then
                MsgBox("Kalender kerja harus diisi dulu..", vbOKOnly + vbInformation, "Informasi")
                Return
            End If

            Dim cmd1 As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd1 As OleDbDataReader = cmd1.ExecuteReader
            Dim dtkaryawan As DataTable = New DataTable
            dtkaryawan.Load(drd1)

            Dim hasiledit_bygrid As Boolean = False
            Dim tgl_sebelum As String = ""
            Dim nip_sebelum As String = ""
            Dim depart_sebelum As String = ""
            Dim kdgol_sebelum As String = ""
            Dim sqlexec As String = ""

            For i As Integer = 0 To dthari.Rows.Count - 1

                Dim tanggal_kalen As Date = dthari(i)("tanggal").ToString

                If nip_param.Trim.Length = 0 Then

                    If Not (tgl_sebelum = "") Then

                        If DateValue(tgl_sebelum) <> DateValue(tanggal_kalen) Then

                            Dim sqlcek As String = String.Format("select id from tr_hadir where tanggal='{0}' and nip='{1}'", convert_date_to_eng(tgl_sebelum), nip_sebelum)
                            Dim cmdcek As OleDbCommand = New OleDbCommand(sqlcek, cn)
                            Dim drdcek As OleDbDataReader = cmdcek.ExecuteReader

                            Dim ada As Boolean = False
                            If drdcek.Read Then
                                If IsNumeric(drdcek(0).ToString) Then
                                    ada = True
                                End If
                            End If
                            drdcek.Close()

                            If ada = False Then

                                cek_hari(False, DateValue(tgl_sebelum))

                                sqlexec = String.Format("insert into tr_hadir (nip,tanggal,jadwalmasuk,jadwalpulang,jammasuk,jampulang,stat,kdgol,kd_shift,depart,step,stat_hari) values ('{0}','{1}',null,null,null,null,'{2}','{3}','{4}','{5}',0,'{6}')", _
                                                                    nip_sebelum, convert_date_to_eng(tgl_sebelum), "LAIN-LAIN", kdgol_sebelum, "-", depart_sebelum, jenislibur_hit)
                                Dim cmdexec As OleDbCommand = New OleDbCommand(sqlexec, cn)
                                cmdexec.ExecuteNonQuery()

                            End If

                        End If
                    End If

                End If

                tgl_sebelum = tanggal_kalen
                nip_sebelum = ""
                kdgol_sebelum = ""
                depart_sebelum = ""

                For n As Integer = 0 To dtkaryawan.Rows.Count - 1

                    SetWaitDialog(String.Format("{0}-{1}", convert_date_to_ind(tanggal_kalen), dtkaryawan(n)("nama").ToString))

                    idmesin = Integer.Parse(dtkaryawan(n)("idmesin").ToString)
                    nnip = dtkaryawan(n)("nip").ToString

                    Dim kdgol As String = dtkaryawan(n)("kdgol").ToString
                    kdgol_sebelum = kdgol

                    nnip_sebelum = ""

                    gapok = Double.Parse(dtkaryawan(n)("gapok").ToString)
                    tunj_jabatan = Double.Parse(dtkaryawan(n)("tunj_jabatan").ToString)
                    tunj_kehadiran = Double.Parse(dtkaryawan(n)("tunj_kehadiran").ToString)
                    tuangmakan = Double.Parse(dtkaryawan(n)("tunj_makan").ToString)
                    tuangmakan_inap = Double.Parse(dtkaryawan(n)("tunj_makan_inap").ToString)
                    tamb_makan = Double.Parse(dtkaryawan(n)("tamb_makanlembur").ToString)
                    standby_uang = Double.Parse(dtkaryawan(n)("standby").ToString)

                    jenisgol = dtkaryawan(n)("jnisgol").ToString
                    jenislembur = dtkaryawan(n)("jenislembur").ToString

                    sharian = Integer.Parse(dtkaryawan(n)("harian").ToString)
                    laki1_uang = Double.Parse(dtkaryawan(n)("laki2").ToString)
                    perempuan_uang = Double.Parse(dtkaryawan(n)("perempuan").ToString)

                    liburnormal = Integer.Parse(dtkaryawan(n)("liburnormal").ToString)

                    depart = dtkaryawan(n)("depart").ToString
                    depart_sebelum = depart

                    jenisgaji = dtkaryawan(n)("jenisgaji").ToString
                    jniskelamin = dtkaryawan(n)("jniskelamin").ToString

                    Dim nilharian As Double
                    If jniskelamin.Equals("Laki - Laki") Then
                        nilharian = laki1_uang
                    Else
                        nilharian = perempuan_uang
                    End If


                    '' cek di tr_hadir sudah ada perhitungan belom, kalau udah lewatin aja dah...
                    If nip_param.Trim.Length = 0 Then

                        If Not (nip_sebelum = "") Then

                            If nip_sebelum <> nnip Then

                                Dim sqlcek As String = String.Format("select id from tr_hadir where tanggal='{0}' and nip='{1}'", convert_date_to_eng(tanggal_kalen), nnip)
                                Dim cmdcek As OleDbCommand = New OleDbCommand(sqlcek, cn)
                                Dim drdcek As OleDbDataReader = cmdcek.ExecuteReader

                                Dim ada As Boolean = False
                                If drdcek.Read Then
                                    If IsNumeric(drdcek(0).ToString) Then
                                        ada = True
                                    End If
                                End If
                                drdcek.Close()

                                If ada = False Then

                                    cek_hari(False, DateValue(tanggal_kalen))

                                    sqlexec = String.Format("insert into tr_hadir (nip,tanggal,jadwalmasuk,jadwalpulang,jammasuk,jampulang,stat,kdgol,kd_shift,depart,step,stat_hari) values ('{0}','{1}',null,null,null,null,'{2}','{3}','{4}','{5}',0,'{6}')", _
                                                                        nnip, convert_date_to_eng(tanggal_kalen), "LAIN-LAIN", kdgol, "-", depart, jenislibur_hit)
                                    Dim cmdexec As OleDbCommand = New OleDbCommand(sqlexec, cn)
                                    cmdexec.ExecuteNonQuery()

                                End If

                            End If

                        End If

                        Dim sqlcd As String = String.Format("select COUNT(nip) as jml from tr_hadir where nip='{0}' and tanggal='{1}'", nnip, convert_date_to_eng(tanggal_kalen))
                        Dim cmdcd As OleDbCommand = New OleDbCommand(sqlcd, cn)
                        Dim drdcd As OleDbDataReader = cmdcd.ExecuteReader

                        If drdcd.Read Then
                            If Int32.Parse(drdcd(0).ToString()) > 0 Then
                                GoTo lanjut_karyawan_laen
                            End If
                        End If
                        drdcd.Close()

                    End If

                    '' akhir cek tr_hadir

                    nip_sebelum = dtkaryawan(n)("nip").ToString

                    '' balikin ms_inout

                    If nip_param.Trim.Length = 0 And DateValue(tanggal_kalen) = DateValue(tanggal_kalk) Then
                        putar_inout2_(cn, tanggal_kalen, tanggal_kalk2, Nothing, Nothing, cb_gol.EditValue, cb_depart.EditValue, nnip)
                    End If

                    '' akhir balikin ms inout

                    If Double.Parse(dtkaryawan(n)("lembur_perjam").ToString) = 0 Then
                        lembur_perjam = lembur_perjam_cek(sharian, jenislembur, gapok, jenishitung_lembur, tunj_jabatan, depart, nilharian)
                    Else
                        lembur_perjam = Double.Parse(dtkaryawan(n)("lembur_perjam").ToString)
                    End If




                    Dim sql_inout As String = String.Format("select distinct * from V_InOut3 where skalk=0 and tgl_kalk is null and CONVERT(date,tanggal)='{0}' and userid={1} order by tanggal", convert_date_to_eng(tanggal_kalen), idmesin)

                    If shift_param.Trim.Length > 0 Then

                        Dim tgla As String = ""
                        Dim tglb As String = ""

                        Select Case shift_param.Trim
                            Case "SHIFT-A"

                                tgla = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "04:00:00")
                                tglb = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "21:00:00")

                                sql_inout = String.Format("select userid,MIN(tanggal) as masuk,MAX(tanggal) as pulang from V_InOut3 where skalk=0 and tgl_kalk is null and CONVERT(date,tanggal)='{0}' and userid={1}", convert_date_to_eng(tanggal_kalen), idmesin)
                                sql_inout = String.Format("{0} and tanggal>='{1}' and tanggal<='{2}'", sql_inout, tgla, tglb)
                                sql_inout = String.Format("{0} group by userid", sql_inout)
                                sql_inout = String.Format("{0} order by MIN(tanggal)", sql_inout)

                            Case "SHIFT-B"

                                tgla = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "15:00:00")
                                tglb = String.Format("{0} {1}", convert_date_to_eng(DateValue(tanggal_kalen).AddDays(1)), "09:00:00")

                                sql_inout = String.Format("select userid,MIN(tanggal) as masuk,MAX(tanggal) as pulang from V_InOut3 where skalk=0 and tgl_kalk is null and CONVERT(date,tanggal)>='{0}' and CONVERT(date,tanggal)<='{1}' and userid={2}", convert_date_to_eng(tanggal_kalen), convert_date_to_eng(tanggal_kalen.AddDays(1)), idmesin)
                                sql_inout = String.Format("{0} and tanggal>='{1}' and tanggal<='{2}'", sql_inout, tgla, tglb)
                                sql_inout = String.Format("{0} group by userid", sql_inout)
                                sql_inout = String.Format("{0} order by MIN(tanggal)", sql_inout)

                            Case "SHIFT-I"

                                tgla = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "04:00:00")
                                tglb = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "18:05:00")

                                sql_inout = String.Format("select userid,MIN(tanggal) as masuk,MAX(tanggal) as pulang from V_InOut3 where skalk=0 and tgl_kalk is null and CONVERT(date,tanggal)='{0}' and userid={1}", convert_date_to_eng(tanggal_kalen), idmesin)
                                sql_inout = String.Format("{0} and tanggal>='{1}' and tanggal<='{2}'", sql_inout, tgla, tglb)
                                sql_inout = String.Format("{0} group by userid", sql_inout)
                                sql_inout = String.Format("{0} order by MIN(tanggal)", sql_inout)

                            Case "SHIFT-II"

                                tgla = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "09:35:00")
                                tglb = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "23:59:59")

                                sql_inout = String.Format("select userid,MIN(tanggal) as masuk,MAX(tanggal) as pulang from V_InOut3 where skalk=0 and tgl_kalk is null and CONVERT(date,tanggal)='{0}' and userid={1}", convert_date_to_eng(tanggal_kalen), idmesin)
                                sql_inout = String.Format("{0} and tanggal>='{1}' and tanggal<='{2}'", sql_inout, tgla, tglb)
                                sql_inout = String.Format("{0} group by userid", sql_inout)
                                sql_inout = String.Format("{0} order by MIN(tanggal)", sql_inout)

                            Case "SHIFT-III"

                                tgla = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "18:35:00")
                                tglb = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen.AddDays(1)), "09:00:00")

                                sql_inout = String.Format("select userid,MIN(tanggal) as masuk,MAX(tanggal) as pulang from V_InOut3 where skalk=0 and tgl_kalk is null and CONVERT(date,tanggal)>='{0}' and CONVERT(date,tanggal)<='{1}' and userid={2}", convert_date_to_eng(tanggal_kalen), convert_date_to_eng(tanggal_kalen.AddDays(1)), idmesin)
                                sql_inout = String.Format("{0} and tanggal>='{1}' and tanggal<='{2}'", sql_inout, tgla, tglb)
                                sql_inout = String.Format("{0} group by userid", sql_inout)
                                sql_inout = String.Format("{0} order by MIN(tanggal)", sql_inout)

                        End Select

                    End If

                    Dim cmd_inout As OleDbCommand = New OleDbCommand(sql_inout, cn)
                    Dim drd_inout As OleDbDataReader = cmd_inout.ExecuteReader

                    Dim jammasuk As DateTime = Nothing
                    Dim jampulang As DateTime = Nothing
                    Dim jamaja As DateTime = Nothing
                    ' Dim sqlexec As String = Nothing

                    Dim jamsebelumnya As DateTime = Nothing

                    '' kalau rubah shift langsung di grid
                    If shift_param.Trim.Length > 0 Then

                        If drd_inout.Read Then

                            Dim ja As String = ""
                            Dim jb As String = ""

                            Dim jd1 As String = ""
                            Dim jd2 As String = ""

                            Select Case shift_param.Trim
                                Case "SHIFT-A"

                                    jd1 = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "07:00:00")
                                    jd2 = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "16:00:00")

                                    If TimeValue(drd_inout("masuk").ToString) >= TimeValue("04:00:00") And TimeValue(drd_inout("masuk").ToString) <= TimeValue("09:00:00") Then
                                        ja = convert_datetime_to_eng(drd_inout("masuk").ToString)
                                    End If

                                    If TimeValue(drd_inout("pulang").ToString) >= TimeValue("15:00:00") And TimeValue(drd_inout("pulang").ToString) <= TimeValue("21:00:00") Then
                                        jb = convert_datetime_to_eng(drd_inout("pulang").ToString)
                                    End If

                                Case "SHIFT-B"

                                    jd1 = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "19:00:00")
                                    jd2 = String.Format("{0} {1}", convert_date_to_eng(DateValue(tanggal_kalen).AddDays(1)), "04:00:00")

                                    If TimeValue(drd_inout("masuk").ToString) >= TimeValue("15:00:00") And TimeValue(drd_inout("masuk").ToString) <= TimeValue("21:00:00") Then
                                        ja = convert_datetime_to_eng(drd_inout("masuk").ToString)
                                    End If

                                    If TimeValue(drd_inout("pulang").ToString) >= TimeValue("02:00:00") And TimeValue(drd_inout("pulang").ToString) <= TimeValue("09:00:00") Then
                                        jb = convert_datetime_to_eng(drd_inout("pulang").ToString)
                                    End If

                                Case "SHIFT-I"

                                    If TimeValue(drd_inout("masuk").ToString) <= "07:16:00" Then
                                        jd1 = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "07:00:00")
                                        jd2 = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "15:00:00")
                                    Else
                                        jd1 = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "08:00:00")
                                        jd2 = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "16:00:00")
                                    End If

                                    If TimeValue(drd_inout("masuk").ToString) >= TimeValue("04:00:00") And TimeValue(drd_inout("masuk").ToString) <= TimeValue("09:00:00") Then
                                        ja = convert_datetime_to_eng(drd_inout("masuk").ToString)
                                    End If

                                    If TimeValue(drd_inout("pulang").ToString) >= TimeValue("09:35:00") And TimeValue(drd_inout("pulang").ToString) <= TimeValue("18:05:00") Then
                                        jb = convert_datetime_to_eng(drd_inout("pulang").ToString)
                                    End If

                                Case "SHIFT-II"

                                    jd1 = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "15:00:00")
                                    jd2 = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "23:00:00")

                                    If TimeValue(drd_inout("masuk").ToString) >= TimeValue("09:35:00") And TimeValue(drd_inout("masuk").ToString) <= TimeValue("18:05:00") Then
                                        ja = convert_datetime_to_eng(drd_inout("masuk").ToString)
                                    End If

                                    If TimeValue(drd_inout("pulang").ToString) >= TimeValue("19:35:00") And TimeValue(drd_inout("pulang").ToString) <= TimeValue("23:59:59") Then
                                        jb = convert_datetime_to_eng(drd_inout("pulang").ToString)
                                    Else

                                        Dim tgla As String = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen.AddDays(1)), "01:00:00")
                                        Dim tglb As String = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen.AddDays(1)), "09:00:00")

                                        sql_inout = String.Format("select userid,MAX(tanggal) as pulang from V_InOut3 where skalk=0 and tgl_kalk is null and CONVERT(date,tanggal)>='{0}' and CONVERT(date,tanggal)<='{1}' and userid={2}", convert_date_to_eng(tanggal_kalen), convert_date_to_eng(tanggal_kalen.AddDays(1)), idmesin)
                                        sql_inout = String.Format("{0} and tanggal>='{1}' and tanggal<='{2}'", sql_inout, tgla, tglb)
                                        sql_inout = String.Format("{0} group by userid", sql_inout)
                                        sql_inout = String.Format("{0} order by MIN(tanggal)", sql_inout)

                                        Dim cmd_inout2 As OleDbCommand = New OleDbCommand(sql_inout, cn)
                                        Dim drd_inout2 As OleDbDataReader = cmd_inout2.ExecuteReader

                                        If drd_inout2.Read Then

                                            If TimeValue(drd_inout2("pulang").ToString) >= TimeValue("01:00:00") And TimeValue(drd_inout2("pulang").ToString) <= TimeValue("09:00:00") Then
                                                jb = convert_datetime_to_eng(drd_inout2("pulang").ToString)
                                            End If

                                        End If
                                        drd_inout2.Close()

                                    End If

                                Case "SHIFT-III"

                                    jd1 = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "23:00:00")
                                    jd2 = String.Format("{0} {1}", convert_date_to_eng(DateValue(tanggal_kalen).AddDays(1)), "07:00:00")

                                    If TimeValue(drd_inout("masuk").ToString) >= TimeValue("20:00:00") And TimeValue(drd_inout("masuk").ToString) <= TimeValue("23:59:59") Then
                                        ja = convert_datetime_to_eng(drd_inout("masuk").ToString)
                                    End If

                                    If TimeValue(drd_inout("pulang").ToString) >= TimeValue("02:00:00") And TimeValue(drd_inout("pulang").ToString) <= TimeValue("09:00:00") Then
                                        jb = convert_datetime_to_eng(drd_inout("pulang").ToString)
                                    End If

                            End Select

                            Dim sql_up1 As String = String.Format("update tr_hadir set jammasuk=null,jampulang=null,stat='LAIN-LAIN',stelat=0,spulangcpat=0, " & _
                                "jmltelat=0,skalk=0,jamkerja=0,jamlembur=0,jmlhasil=0,lemburperjam=0,totlembur=0, " & _
                                "jam1=0,jam2=0,jam3=0,jam4=0,hasilper=0,tothasil=0,tambmakan=0,step=0,jharian=0,tamb_istirahat=0,jnisabsen=1, " & _
                                "tamblembur=0,tamb1=0,tamb2=0,kd_shift='-',uangmakan=0,stat_lmbr='-' " & _
                                "where id={0}", id_param)
                            Using cmd_up1 As OleDbCommand = New OleDbCommand(sql_up1, cn)
                                cmd_up1.ExecuteNonQuery()
                            End Using

                            Dim sqlm As String = String.Format("update tr_hadir set skalk=1,stat_hari='{0}' where id={1}", jenislibur_hit, id_param)
                            Using cmdm As OleDbCommand = New OleDbCommand(sqlm, cn)
                                cmdm.ExecuteNonQuery()
                            End Using

                            If ja.Trim.Length > 0 Or jb.Trim.Length > 0 Then

                                Dim sqlu As String = String.Format("update tr_hadir set jadwalmasuk='{0}',jadwalpulang='{1}',kd_shift='{2}' where id={3}", jd1, jd2, shift_param, id_param)
                                Using cmdu As OleDbCommand = New OleDbCommand(sqlu, cn)
                                    cmdu.ExecuteNonQuery()
                                End Using

                            End If

                            If ja.Trim.Length > 0 Then

                                Dim sql_up2 As String = String.Format("update tr_hadir set jammasuk='{0}' where id={1}", ja, id_param)
                                Using cmd_up2 As OleDbCommand = New OleDbCommand(sql_up2, cn)
                                    cmd_up2.ExecuteNonQuery()
                                End Using

                            End If

                            If jb.Trim.Length > 0 Then

                                If Not (ja = jb) Then
                                    Dim sql_up2 As String = String.Format("update tr_hadir set jampulang='{0}' where id={1}", jb, id_param)
                                    Using cmd_up2 As OleDbCommand = New OleDbCommand(sql_up2, cn)
                                        cmd_up2.ExecuteNonQuery()
                                    End Using
                                End If

                            End If

                            If ja.Trim.Length > 0 And jb.Trim.Length > 0 Then

                                Dim sql_up2 As String = String.Format("update tr_hadir set stat='HADIR',step=1 where id={0}", id_param)
                                Using cmd_up2 As OleDbCommand = New OleDbCommand(sql_up2, cn)
                                    cmd_up2.ExecuteNonQuery()
                                End Using

                            End If

                            If ja.Trim.Length > 0 And jb.Trim.Length > 0 Then

                                '' cek hari dulu''
                                If nip_param.Trim.Length = 0 Then
                                    cek_hari(False, DateValue(tanggal_kalen))
                                Else
                                    If jenislibur_hit.Trim.Length = 0 Then
                                        cek_hari(False, DateValue(tanggal_kalen))
                                    End If
                                End If
                                '' end of cek hari

                                mulai_hitung2(id_param, shift_param, jd1, jd2, ja, jb, "HADIR", cn, 0.0)
                            End If

                            If ja.Trim.Length > 0 Then
                                inout_rubah(drd_inout("userid").ToString, tanggal_kalen, ja, jd2, cn)
                            End If

                            If jb.Trim.Length > 0 Then
                                inout_rubah(drd_inout("userid").ToString, tanggal_kalen, jd1, jb, cn)
                            End If


                            hasiledit_bygrid = True

                            GoTo langsung_keluar

                        End If

                    End If
                    '' end langsung rubah shift

                    If drd_inout.HasRows Then

                        While drd_inout.Read

                            Dim statcek_sebelum As Integer = 0
                            Dim kd_shift As String = Nothing
                            Dim jd_masuk As DateTime = Nothing
                            Dim jd_pulang As DateTime = Nothing
                            Dim jm_masuk As DateTime = Nothing
                            Dim jm_pulang As DateTime = Nothing
                            Dim st_hadir As String = Nothing

                            Dim jamhasil As DateTime = drd_inout("tanggal").ToString
                            Dim userid_inout As Integer = drd_inout("userid").ToString

                            If Not (jamsebelumnya = Nothing) Then

                                Dim selisih_tgl As Integer = DateDiff(DateInterval.Day, DateValue(jamhasil), DateValue(jamsebelumnya))


                                Dim selisih_menit As TimeSpan = DateTime.Parse(jamhasil) - DateTime.Parse(jamsebelumnya)


                                If selisih_tgl = 0 Then
                                    If selisih_menit.Hours = 0 And selisih_menit.Minutes <= 60 Then
                                        ' inout_rubah(userid_inout, tanggal_kalen, jamhasil, cn)
                                        GoTo _ke_absen_selanjutnya
                                    End If
                                End If


                            End If

                            jamsebelumnya = jamhasil



                            Select Case Trim(depart)
                                Case "PRODUKSI"

                                    If TimeValue(jamhasil) >= "04:00:00" And TimeValue(jamhasil) <= "09:00:00" Then

                                        Dim sqlcek_sebelum As String = String.Format("select id,kd_shift,jadwalmasuk,jadwalpulang,jammasuk,stat from tr_hadir where tanggal='{0}' and nip='{1}' and jampulang is null and stat='HADIR'", convert_date_to_eng(DateValue(tanggal_kalen).AddDays(-1)), nnip)
                                        Dim cmdcek_sebelum As OleDbCommand = New OleDbCommand(sqlcek_sebelum, cn)
                                        Dim drdcek_sebelum As OleDbDataReader = cmdcek_sebelum.ExecuteReader

                                        If drdcek_sebelum.HasRows Then
                                            If drdcek_sebelum.Read Then
                                                If Integer.Parse(drdcek_sebelum(0).ToString) > 0 Then
                                                    statcek_sebelum = Integer.Parse(drdcek_sebelum(0).ToString)
                                                    kd_shift = drdcek_sebelum("kd_shift").ToString
                                                    jd_masuk = drdcek_sebelum("jadwalmasuk").ToString
                                                    jd_pulang = drdcek_sebelum("jadwalpulang").ToString
                                                    jm_masuk = drdcek_sebelum("jammasuk").ToString
                                                    st_hadir = drdcek_sebelum("stat").ToString
                                                End If
                                            End If
                                        End If
                                        drdcek_sebelum.Close()

                                        If statcek_sebelum = 0 Then

                                            '' masuk ''

                                            Dim jmasuk As String = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "07:00:00")
                                            Dim jpulang As String = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "16:00:00")
                                            Dim jamhasil2 As String = convert_datetime_to_eng(jamhasil)

                                            Dim idx As Boolean = False
                                            If id_param > 0 And nip_param.Trim.Length > 0 And shift_param.Trim.Length = 0 Then

                                                Dim sqlx As String = String.Format("select id from tr_hadir where id={0} and jadwalmasuk is null and jadwalpulang is null and jammasuk is null and jampulang is null", _
                                                                                     id_param)
                                                Dim cmdx As OleDbCommand = New OleDbCommand(sqlx, cn)
                                                Dim drdx As OleDbDataReader = cmdx.ExecuteReader

                                                If drdx.Read Then
                                                    If drdx("id").ToString <> "" Then
                                                        idx = True
                                                    End If
                                                End If
                                                drdx.Close()


                                            End If

                                            If idx = False Then
                                                sqlexec = String.Format("insert into tr_hadir (nip,tanggal,jadwalmasuk,jadwalpulang,jammasuk,jampulang,stat,kdgol,kd_shift,depart,step,stat_hari) values ('{0}','{1}','{2}','{3}','{4}',null,'{5}','{6}','{7}','{8}',0,'{9}')", _
                                                                    nnip, convert_date_to_eng(tanggal_kalen), jmasuk, jpulang, jamhasil2, "HADIR", kdgol, "SHIFT-A", depart, jenislibur_hit)

                                            Else
                                                sqlexec = String.Format("update tr_hadir set jadwalmasuk='{0}',jadwalpulang='{1}',jammasuk='{2}',stat='{3}',kdgol='{4}',kd_shift='{5}',depart='{6}',stat_hari='{7}' where id={8}", _
                                                                      jmasuk, jpulang, jamhasil2, "HADIR", kdgol, "SHIFT-A", depart, jenislibur_hit, id_param)

                                            End If


                                            Dim cmdexec As OleDbCommand = New OleDbCommand(sqlexec, cn)
                                            cmdexec.ExecuteNonQuery()

                                            Dim xjpulang As DateTime = jpulang
                                            If TimeValue(jpulang) >= "02:00:00" And TimeValue(xjpulang) <= "05:00:00" Then
                                                xjpulang = xjpulang.AddHours(-1)
                                            Else
                                                xjpulang = xjpulang.AddHours(-5)
                                            End If

                                            inout_rubah(userid_inout, tanggal_kalen, jamhasil2, xjpulang, cn)



                                        Else

                                            Dim jamhasil2 As String = convert_datetime_to_eng(jamhasil)

                                            sqlexec = String.Format("update tr_hadir set jampulang='{0}',step=1 where id={1}", jamhasil2, statcek_sebelum)
                                            Dim cmdexec As OleDbCommand = New OleDbCommand(sqlexec, cn)
                                            cmdexec.ExecuteNonQuery()

                                            '' cek hari dulu''
                                            If nip_param.Trim.Length = 0 Then
                                                cek_hari(False, DateValue(tanggal_kalen).AddDays(-1))
                                            Else
                                                If jenislibur_hit.Trim.Length = 0 Then
                                                    cek_hari(False, DateValue(tanggal_kalen).AddDays(-1))
                                                End If
                                            End If
                                            '' end of cek hari

                                            mulai_hitung2(statcek_sebelum, kd_shift, jd_masuk, jd_pulang, jm_masuk, jamhasil2, st_hadir, cn, 0.0)

                                            inout_rubah(userid_inout, tanggal_kalen.AddDays(-1), jd_masuk, jamhasil2, cn)

                                        End If


                                    ElseIf TimeValue(jamhasil) >= "09:35:00" And TimeValue(jamhasil) <= "16:10:00" Then

                                        Dim sqlcek_sebelum As String = String.Format("select id,kd_shift,jadwalmasuk,jadwalpulang,jammasuk,jampulang,stat from tr_hadir where tanggal='{0}' and nip='{1}' and jampulang is null and stat='HADIR'", convert_date_to_eng(tanggal_kalen), nnip)
                                        Dim cmdcek_sebelum As OleDbCommand = New OleDbCommand(sqlcek_sebelum, cn)
                                        Dim drdcek_sebelum As OleDbDataReader = cmdcek_sebelum.ExecuteReader

                                        If drdcek_sebelum.HasRows Then
                                            If drdcek_sebelum.Read Then
                                                If Integer.Parse(drdcek_sebelum(0).ToString) > 0 Then
                                                    statcek_sebelum = Integer.Parse(drdcek_sebelum(0).ToString)
                                                    kd_shift = drdcek_sebelum("kd_shift").ToString
                                                    jd_masuk = drdcek_sebelum("jadwalmasuk").ToString
                                                    jd_pulang = drdcek_sebelum("jadwalpulang").ToString
                                                    jm_masuk = drdcek_sebelum("jammasuk").ToString
                                                    st_hadir = drdcek_sebelum("stat").ToString
                                                End If
                                            End If
                                        End If
                                        drdcek_sebelum.Close()

                                        If statcek_sebelum > 0 Then

                                            Dim jamhasil2 As String = convert_datetime_to_eng(jamhasil)

                                            sqlexec = String.Format("update tr_hadir set jampulang='{0}',step=1 where id={1}", jamhasil2, statcek_sebelum)
                                            Dim cmdexec As OleDbCommand = New OleDbCommand(sqlexec, cn)
                                            cmdexec.ExecuteNonQuery()

                                            '' cek hari dulu''
                                            If nip_param.Trim.Length = 0 Then
                                                cek_hari(False, DateValue(tanggal_kalen))
                                            Else
                                                If jenislibur_hit.Trim.Length = 0 Then
                                                    cek_hari(False, DateValue(tanggal_kalen))
                                                End If
                                            End If
                                            '' end of cek hari

                                            mulai_hitung2(statcek_sebelum, kd_shift, jd_masuk, jd_pulang, jm_masuk, jamhasil2, st_hadir, cn, 0.0)

                                            inout_rubah(userid_inout, tanggal_kalen, jd_masuk, jamhasil2, cn)

                                        Else

                                            '' masuk ''

                                            Dim jmasuk As String = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "15:00:00")
                                            Dim jpulang As String = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "23:00:00")
                                            Dim jamhasil2 As String = convert_datetime_to_eng(jamhasil)

                                            Dim idx As Boolean = False
                                            If id_param > 0 And nip_param.Trim.Length > 0 And shift_param.Trim.Length = 0 Then

                                                Dim sqlx As String = String.Format("select id from tr_hadir where id={0} and jadwalmasuk is null and jadwalpulang is null and jammasuk is null and jampulang is null", _
                                                                                     id_param)
                                                Dim cmdx As OleDbCommand = New OleDbCommand(sqlx, cn)
                                                Dim drdx As OleDbDataReader = cmdx.ExecuteReader

                                                If drdx.Read Then
                                                    If drdx("id").ToString <> "" Then
                                                        idx = True
                                                    End If
                                                End If
                                                drdx.Close()

                                            End If

                                            If idx = False Then
                                                sqlexec = String.Format("insert into tr_hadir (nip,tanggal,jadwalmasuk,jadwalpulang,jammasuk,jampulang,stat,kdgol,kd_shift,depart,step,stat_hari) values ('{0}','{1}','{2}','{3}','{4}',null,'{5}','{6}','{7}','{8}',0,'{9}')", _
                                                                        nnip, convert_date_to_eng(tanggal_kalen), jmasuk, jpulang, jamhasil2, "HADIR", kdgol, "SHIFT-II", depart, jenislibur_hit)
                                            Else
                                                sqlexec = String.Format("update tr_hadir set jadwalmasuk='{0}',jadwalpulang='{1}',jammasuk='{2}',stat='{3}',kdgol='{4}',kd_shift='{5}',depart='{6}',stat_hari='{7}' where id={8}", _
                                                                      jmasuk, jpulang, jamhasil2, "HADIR", kdgol, "SHIFT-II", depart, jenislibur_hit, id_param)
                                            End If


                                            Dim cmdexec As OleDbCommand = New OleDbCommand(sqlexec, cn)
                                            cmdexec.ExecuteNonQuery()


                                            Dim xjpulang As DateTime = jpulang
                                            If TimeValue(jpulang) >= "02:00:00" And TimeValue(xjpulang) <= "05:00:00" Then
                                                xjpulang = xjpulang.AddHours(-1)
                                            Else
                                                xjpulang = xjpulang.AddHours(-5)
                                            End If

                                            inout_rubah(userid_inout, tanggal_kalen, jamhasil2, xjpulang, cn)


                                        End If

                                    ElseIf TimeValue(jamhasil) >= "16:15:00" And TimeValue(jamhasil) <= "19:30:00" Then

                                        Dim sqlcek_sebelum As String = String.Format("select id,kd_shift,jadwalmasuk,jadwalpulang,jammasuk,jampulang,stat from tr_hadir where tanggal='{0}' and nip='{1}' and jampulang is null and stat='HADIR'", convert_date_to_eng(tanggal_kalen), nnip)
                                        Dim cmdcek_sebelum As OleDbCommand = New OleDbCommand(sqlcek_sebelum, cn)
                                        Dim drdcek_sebelum As OleDbDataReader = cmdcek_sebelum.ExecuteReader

                                        If drdcek_sebelum.HasRows Then
                                            If drdcek_sebelum.Read Then
                                                If Integer.Parse(drdcek_sebelum(0).ToString) > 0 Then
                                                    statcek_sebelum = Integer.Parse(drdcek_sebelum(0).ToString)
                                                    kd_shift = drdcek_sebelum("kd_shift").ToString
                                                    jd_masuk = drdcek_sebelum("jadwalmasuk").ToString
                                                    jd_pulang = drdcek_sebelum("jadwalpulang").ToString
                                                    jm_masuk = drdcek_sebelum("jammasuk").ToString
                                                    st_hadir = drdcek_sebelum("stat").ToString
                                                End If
                                            End If
                                        End If
                                        drdcek_sebelum.Close()

                                        If statcek_sebelum > 0 Then

                                            Dim jamhasil2 As String = convert_datetime_to_eng(jamhasil)

                                            sqlexec = String.Format("update tr_hadir set jampulang='{0}',step=1 where id={1}", jamhasil2, statcek_sebelum)
                                            Dim cmdexec As OleDbCommand = New OleDbCommand(sqlexec, cn)
                                            cmdexec.ExecuteNonQuery()

                                            '' cek hari dulu''
                                            If nip_param.Trim.Length = 0 Then
                                                cek_hari(False, DateValue(tanggal_kalen))
                                            Else
                                                If jenislibur_hit.Trim.Length = 0 Then
                                                    cek_hari(False, DateValue(tanggal_kalen))
                                                End If
                                            End If
                                            '' end of cek hari

                                            mulai_hitung2(statcek_sebelum, kd_shift, jd_masuk, jd_pulang, jm_masuk, jamhasil2, st_hadir, cn, 0.0)

                                            inout_rubah(userid_inout, tanggal_kalen, jd_masuk, jamhasil2, cn)

                                        Else

                                            '' masuk ''

                                            Dim jmasuk As String = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "19:00:00")
                                            Dim jpulang As String = String.Format("{0} {1}", convert_date_to_eng(DateValue(tanggal_kalen).AddDays(1)), "04:00:00")
                                            Dim jamhasil2 As String = convert_datetime_to_eng(jamhasil)

                                            Dim idx As Boolean = False
                                            If id_param > 0 And nip_param.Trim.Length > 0 And shift_param.Trim.Length = 0 Then

                                                Dim sqlx As String = String.Format("select id from tr_hadir where id={0} and jadwalmasuk is null and jadwalpulang is null and jammasuk is null and jampulang is null", _
                                                                                     id_param)
                                                Dim cmdx As OleDbCommand = New OleDbCommand(sqlx, cn)
                                                Dim drdx As OleDbDataReader = cmdx.ExecuteReader

                                                If drdx.Read Then
                                                    If drdx("id").ToString <> "" Then
                                                        idx = True
                                                    End If
                                                End If
                                                drdx.Close()


                                            End If

                                            If idx = False Then
                                                sqlexec = String.Format("insert into tr_hadir (nip,tanggal,jadwalmasuk,jadwalpulang,jammasuk,jampulang,stat,kdgol,kd_shift,depart,step,stat_hari) values ('{0}','{1}','{2}','{3}','{4}',null,'{5}','{6}','{7}','{8}',0,'{9}')", _
                                                                    nnip, convert_date_to_eng(tanggal_kalen), jmasuk, jpulang, jamhasil2, "HADIR", kdgol, "SHIFT-B", depart, jenislibur_hit)

                                            Else
                                                sqlexec = String.Format("update tr_hadir set jadwalmasuk='{0}',jadwalpulang='{1}',jammasuk='{2}',stat='{3}',kdgol='{4}',kd_shift='{5}',depart='{6}',stat_hari='{7}' where id={8}", _
                                                                      jmasuk, jpulang, jamhasil2, "HADIR", kdgol, "SHIFT-B", depart, jenislibur_hit, id_param)
                                            End If

                                            Dim cmdexec As OleDbCommand = New OleDbCommand(sqlexec, cn)
                                            cmdexec.ExecuteNonQuery()

                                            Dim xjpulang As DateTime = jpulang
                                            If TimeValue(jpulang) >= "02:00:00" And TimeValue(xjpulang) <= "05:00:00" Then
                                                xjpulang = xjpulang.AddHours(-1)
                                            Else
                                                xjpulang = xjpulang.AddHours(-5)
                                            End If

                                            inout_rubah(userid_inout, tanggal_kalen, jamhasil2, xjpulang, cn)

                                        End If

                                    ElseIf DateValue(String.Format("{0} {1}", Microsoft.VisualBasic.Left(tanggal_kalen, 10), Microsoft.VisualBasic.Right(jamhasil, 8))) >= DateValue(String.Format("{0} {1}", Microsoft.VisualBasic.Left(tanggal_kalen, 10), "19:35:00")) And _
                                        DateValue(String.Format("{0} {1}", Microsoft.VisualBasic.Left(tanggal_kalen, 10), Microsoft.VisualBasic.Right(jamhasil, 8))) <= DateValue(String.Format("{0} {1}", Microsoft.VisualBasic.Left(tanggal_kalen, 10), "23:59:59")) Then

                                        Dim sqlcek_sebelum As String = String.Format("select id,kd_shift,jadwalmasuk,jadwalpulang,jammasuk,jampulang,stat from tr_hadir where tanggal='{0}' and nip='{1}' and jampulang is null and stat='HADIR'", convert_date_to_eng(tanggal_kalen), nnip)

                                        If TimeValue(jamhasil) > "00:00:00" And TimeValue(jamhasil) <= "03:00:00" Then
                                            sqlcek_sebelum = String.Format("select id,kd_shift,jadwalmasuk,jadwalpulang,jammasuk,jampulang,stat from tr_hadir where tanggal='{0}' and nip='{1}' and jampulang is null and stat='HADIR'", convert_date_to_eng(DateValue(tanggal_kalen).AddDays(-1)), nnip)
                                        End If

                                        Dim cmdcek_sebelum As OleDbCommand = New OleDbCommand(sqlcek_sebelum, cn)
                                        Dim drdcek_sebelum As OleDbDataReader = cmdcek_sebelum.ExecuteReader


                                        If drdcek_sebelum.HasRows Then
                                            If drdcek_sebelum.Read Then
                                                If Integer.Parse(drdcek_sebelum(0).ToString) > 0 Then
                                                    statcek_sebelum = Integer.Parse(drdcek_sebelum(0).ToString)
                                                    kd_shift = drdcek_sebelum("kd_shift").ToString
                                                    jd_masuk = drdcek_sebelum("jadwalmasuk").ToString
                                                    jd_pulang = drdcek_sebelum("jadwalpulang").ToString
                                                    jm_masuk = drdcek_sebelum("jammasuk").ToString
                                                    st_hadir = drdcek_sebelum("stat").ToString
                                                End If
                                            End If
                                        End If
                                        drdcek_sebelum.Close()

                                        If statcek_sebelum > 0 Then

                                            Dim jamhasil2 As String = convert_datetime_to_eng(jamhasil)

                                            sqlexec = String.Format("update tr_hadir set jampulang='{0}',step=1 where id={1}", jamhasil2, statcek_sebelum)
                                            Dim cmdexec As OleDbCommand = New OleDbCommand(sqlexec, cn)
                                            cmdexec.ExecuteNonQuery()

                                            '' cek hari dulu''
                                            If nip_param.Trim.Length = 0 Then
                                                If TimeValue(jamhasil) > "00:00:00" And TimeValue(jamhasil) <= "03:00:00" Then
                                                    cek_hari(False, DateValue(tanggal_kalen).AddDays(-1))
                                                Else
                                                    cek_hari(False, DateValue(tanggal_kalen))
                                                End If
                                            Else
                                                If jenislibur_hit.Trim.Length = 0 Then
                                                    If TimeValue(jamhasil) > "00:00:00" And TimeValue(jamhasil) <= "03:00:00" Then
                                                        cek_hari(False, DateValue(tanggal_kalen).AddDays(-1))
                                                    Else
                                                        cek_hari(False, DateValue(tanggal_kalen))
                                                    End If
                                                End If
                                            End If
                                            '' end of cek hari

                                            mulai_hitung2(statcek_sebelum, kd_shift, jd_masuk, jd_pulang, jm_masuk, jamhasil2, st_hadir, cn, 0.0)

                                            inout_rubah(userid_inout, tanggal_kalen, jd_masuk, jamhasil2, cn)

                                        Else

                                            '' masuk ''

                                            Dim jmasuk As String = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "23:00:00")
                                            Dim jpulang As String = String.Format("{0} {1}", convert_date_to_eng(DateValue(tanggal_kalen).AddDays(1)), "07:00:00")
                                            Dim jamhasil2 As String = convert_datetime_to_eng(jamhasil)

                                            Dim idx As Boolean = False
                                            If id_param > 0 And nip_param.Trim.Length > 0 And shift_param.Trim.Length = 0 Then

                                                Dim sqlx As String = String.Format("select id from tr_hadir where id={0} and jadwalmasuk is null and jadwalpulang is null and jammasuk is null and jampulang is null", _
                                                                                     id_param)
                                                Dim cmdx As OleDbCommand = New OleDbCommand(sqlx, cn)
                                                Dim drdx As OleDbDataReader = cmdx.ExecuteReader

                                                If drdx.Read Then
                                                    If drdx("id").ToString <> "" Then
                                                        idx = True
                                                    End If
                                                End If
                                                drdx.Close()


                                            End If

                                            If idx = False Then
                                                sqlexec = String.Format("insert into tr_hadir (nip,tanggal,jadwalmasuk,jadwalpulang,jammasuk,jampulang,stat,kdgol,kd_shift,depart,step,stat_hari) values ('{0}','{1}','{2}','{3}','{4}',null,'{5}','{6}','{7}','{8}',0,'{9}')", _
                                                                       nnip, convert_date_to_eng(tanggal_kalen), jmasuk, jpulang, jamhasil2, "HADIR", kdgol, "SHIFT-III", depart, jenislibur_hit)
                                            Else
                                                sqlexec = String.Format("update tr_hadir set jadwalmasuk='{0}',jadwalpulang='{1}',jammasuk='{2}',stat='{3}',kdgol='{4}',kd_shift='{5}',depart='{6}',stat_hari='{7}' where id={8}", _
                                                                      jmasuk, jpulang, jamhasil2, "HADIR", kdgol, "SHIFT-III", depart, jenislibur_hit, id_param)
                                            End If


                                            Dim cmdexec As OleDbCommand = New OleDbCommand(sqlexec, cn)
                                            cmdexec.ExecuteNonQuery()


                                            Dim xjpulang As DateTime = jpulang
                                            If TimeValue(jpulang) >= "02:00:00" And TimeValue(xjpulang) <= "05:00:00" Then
                                                xjpulang = xjpulang.AddHours(-1)
                                            Else
                                                xjpulang = xjpulang.AddHours(-5)
                                            End If

                                            inout_rubah(userid_inout, tanggal_kalen, jamhasil2, xjpulang, cn)


                                        End If


                                    End If


                                Case "LOOM", "LOOM BORONGAN", "CUTTING", "PRINTING", "SECURITY", "QA / QC", "CUTTING JUMBO", "METAL DETECTOR", "INNER BLOWING", "BORONGAN JUMBO BAG"

                                    If TimeValue(jamhasil) >= "04:00:00" And TimeValue(jamhasil) <= "09:00:00" Then

                                        Dim tglm_a1 As String = String.Format("{0} {1}", Microsoft.VisualBasic.Left(convert_date_to_eng(DateValue(tanggal_kalen).AddDays(-1)), 10), "09:35:00")
                                        Dim tglm_a2 As String = String.Format("{0} {1}", Microsoft.VisualBasic.Left(convert_date_to_eng(DateValue(tanggal_kalen).AddDays(-1)), 10), "23:59:59")

                                        Dim sqlcek_sebelum As String = String.Format("select id,kd_shift,jadwalmasuk,jadwalpulang,jammasuk,jampulang,stat from tr_hadir where tanggal='{0}' and nip='{1}' and jampulang is null and stat='HADIR' and jammasuk>='{2}' and jammasuk<='{3}'", _
                                                                                     convert_date_to_eng(DateValue(tanggal_kalen).AddDays(-1)), nnip, tglm_a1, tglm_a2)

                                        Dim cmdcek_sebelum As OleDbCommand = New OleDbCommand(sqlcek_sebelum, cn)
                                        Dim drdcek_sebelum As OleDbDataReader = cmdcek_sebelum.ExecuteReader


                                        If drdcek_sebelum.HasRows Then
                                            If drdcek_sebelum.Read Then
                                                If Integer.Parse(drdcek_sebelum(0).ToString) > 0 Then
                                                    statcek_sebelum = Integer.Parse(drdcek_sebelum(0).ToString)
                                                    kd_shift = drdcek_sebelum("kd_shift").ToString
                                                    jd_masuk = drdcek_sebelum("jadwalmasuk").ToString
                                                    jd_pulang = drdcek_sebelum("jadwalpulang").ToString
                                                    jm_masuk = drdcek_sebelum("jammasuk").ToString
                                                    st_hadir = drdcek_sebelum("stat").ToString
                                                End If
                                            End If
                                        End If
                                        drdcek_sebelum.Close()

                                        If statcek_sebelum = 0 Then

                                            '' masuk ''

                                            Dim jmasuk As String = Nothing
                                            Dim jpulang As String = Nothing

                                            If TimeValue(jamhasil) <= "07:16:00" Then
                                                jmasuk = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "07:00:00")
                                                jpulang = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "15:00:00")
                                            Else
                                                jmasuk = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "08:00:00")
                                                jpulang = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "16:00:00")
                                            End If

                                            Dim jamhasil2 As String = convert_datetime_to_eng(jamhasil)

                                            Dim idx As Boolean = False
                                            If id_param > 0 And nip_param.Trim.Length > 0 And shift_param.Trim.Length = 0 Then

                                                Dim sqlx As String = String.Format("select id from tr_hadir where id={0} and jadwalmasuk is null and jadwalpulang is null and jammasuk is null and jampulang is null", _
                                                                                     id_param)
                                                Dim cmdx As OleDbCommand = New OleDbCommand(sqlx, cn)
                                                Dim drdx As OleDbDataReader = cmdx.ExecuteReader

                                                If drdx.Read Then
                                                    If drdx("id").ToString <> "" Then
                                                        idx = True
                                                    End If
                                                End If
                                                drdx.Close()


                                            End If

                                            If idx = False Then
                                                sqlexec = String.Format("insert into tr_hadir (nip,tanggal,jadwalmasuk,jadwalpulang,jammasuk,jampulang,stat,kdgol,kd_shift,depart,step,stat_hari) values ('{0}','{1}','{2}','{3}','{4}',null,'{5}','{6}','{7}','{8}',0,'{9}')", _
                                                                    nnip, convert_date_to_eng(tanggal_kalen), jmasuk, jpulang, jamhasil2, "HADIR", kdgol, "SHIFT-I", depart, jenislibur_hit)
                                            Else
                                                sqlexec = String.Format("update tr_hadir set jadwalmasuk='{0}',jadwalpulang='{1}',jammasuk='{2}',stat='{3}',kdgol='{4}',kd_shift='{5}',depart='{6}',stat_hari='{7}' where id={8}", _
                                                                      jmasuk, jpulang, jamhasil2, "HADIR", kdgol, "SHIFT-I", depart, jenislibur_hit, id_param)
                                            End If

                                            Dim cmdexec As OleDbCommand = New OleDbCommand(sqlexec, cn)
                                            cmdexec.ExecuteNonQuery()

                                            Dim xjpulang As DateTime = jpulang
                                            If TimeValue(jpulang) >= "02:00:00" And TimeValue(xjpulang) <= "05:00:00" Then
                                                xjpulang = xjpulang.AddHours(-1)
                                            Else
                                                xjpulang = xjpulang.AddHours(-5)
                                            End If

                                            inout_rubah(userid_inout, tanggal_kalen, jamhasil2, xjpulang, cn)

                                        Else

                                            Dim jamhasil2 As String = convert_datetime_to_eng(jamhasil)

                                            sqlexec = String.Format("update tr_hadir set jampulang='{0}',step=1 where id={1}", jamhasil2, statcek_sebelum)
                                            Dim cmdexec As OleDbCommand = New OleDbCommand(sqlexec, cn)
                                            cmdexec.ExecuteNonQuery()

                                            '' cek hari dulu''
                                            If nip_param.Trim.Length = 0 Then
                                                cek_hari(False, DateValue(tanggal_kalen).AddDays(-1))
                                            Else
                                                If jenislibur_hit.Trim.Length = 0 Then
                                                    cek_hari(False, DateValue(tanggal_kalen).AddDays(-1))
                                                End If
                                            End If
                                            '' end of cek hari

                                            mulai_hitung2(statcek_sebelum, kd_shift, jd_masuk, jd_pulang, jm_masuk, jamhasil2, st_hadir, cn, 0.0)

                                            inout_rubah(userid_inout, tanggal_kalen.AddDays(-1), jd_masuk, jamhasil2, cn)

                                        End If

                                    ElseIf TimeValue(jamhasil) >= "09:35:00" And TimeValue(jamhasil) <= "18:05:00" Then

                                        Dim sqlcek_sebelum As String = String.Format("select id,kd_shift,jadwalmasuk,jadwalpulang,jammasuk,jampulang,stat from tr_hadir where tanggal='{0}' and nip='{1}' and jampulang is null and stat='HADIR'", convert_date_to_eng(tanggal_kalen), nnip)
                                        Dim cmdcek_sebelum As OleDbCommand = New OleDbCommand(sqlcek_sebelum, cn)
                                        Dim drdcek_sebelum As OleDbDataReader = cmdcek_sebelum.ExecuteReader

                                        If drdcek_sebelum.HasRows Then
                                            If drdcek_sebelum.Read Then
                                                If Integer.Parse(drdcek_sebelum(0).ToString) > 0 Then
                                                    statcek_sebelum = Integer.Parse(drdcek_sebelum(0).ToString)
                                                    kd_shift = drdcek_sebelum("kd_shift").ToString
                                                    jd_masuk = drdcek_sebelum("jadwalmasuk").ToString
                                                    jd_pulang = drdcek_sebelum("jadwalpulang").ToString
                                                    jm_masuk = drdcek_sebelum("jammasuk").ToString
                                                    st_hadir = drdcek_sebelum("stat").ToString
                                                End If
                                            End If
                                        End If
                                        drdcek_sebelum.Close()

                                        If statcek_sebelum > 0 Then

                                            Dim jamhasil2 As String = convert_datetime_to_eng(jamhasil)

                                            sqlexec = String.Format("update tr_hadir set jampulang='{0}',step=1 where id={1}", jamhasil2, statcek_sebelum)
                                            Dim cmdexec As OleDbCommand = New OleDbCommand(sqlexec, cn)
                                            cmdexec.ExecuteNonQuery()

                                            '' cek hari dulu''
                                            If nip_param.Trim.Length = 0 Then
                                                cek_hari(False, DateValue(tanggal_kalen))
                                            Else
                                                If jenislibur_hit.Trim.Length = 0 Then
                                                    cek_hari(False, DateValue(tanggal_kalen))
                                                End If
                                            End If
                                            '' end of cek hari

                                            mulai_hitung2(statcek_sebelum, kd_shift, jd_masuk, jd_pulang, jm_masuk, jamhasil2, st_hadir, cn, 0.0)

                                            inout_rubah(userid_inout, tanggal_kalen, jd_masuk, jamhasil2, cn)

                                        Else

                                            '' masuk ''

                                            Dim jmasuk As String = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "15:00:00")
                                            Dim jpulang As String = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "23:00:00")
                                            Dim jamhasil2 As String = convert_datetime_to_eng(jamhasil)

                                            Dim idx As Boolean = False
                                            If id_param > 0 And nip_param.Trim.Length > 0 And shift_param.Trim.Length = 0 Then

                                                Dim sqlx As String = String.Format("select id from tr_hadir where id={0} and jadwalmasuk is null and jadwalpulang is null and jammasuk is null and jampulang is null", _
                                                                                     id_param)
                                                Dim cmdx As OleDbCommand = New OleDbCommand(sqlx, cn)
                                                Dim drdx As OleDbDataReader = cmdx.ExecuteReader

                                                If drdx.Read Then
                                                    If drdx("id").ToString <> "" Then
                                                        idx = True
                                                    End If
                                                End If
                                                drdx.Close()

                                            End If

                                            If idx = False Then
                                                sqlexec = String.Format("insert into tr_hadir (nip,tanggal,jadwalmasuk,jadwalpulang,jammasuk,jampulang,stat,kdgol,kd_shift,depart,step,stat_hari) values ('{0}','{1}','{2}','{3}','{4}',null,'{5}','{6}','{7}','{8}',0,'{9}')", _
                                                                        nnip, convert_date_to_eng(tanggal_kalen), jmasuk, jpulang, jamhasil2, "HADIR", kdgol, "SHIFT-II", depart, jenislibur_hit)
                                            Else
                                                sqlexec = String.Format("update tr_hadir set jadwalmasuk='{0}',jadwalpulang='{1}',jammasuk='{2}',stat='{3}',kdgol='{4}',kd_shift='{5}',depart='{6}',stat_hari='{7}' where id={8}", _
                                                                      jmasuk, jpulang, jamhasil2, "HADIR", kdgol, "SHIFT-II", depart, jenislibur_hit, id_param)
                                            End If


                                            Dim cmdexec As OleDbCommand = New OleDbCommand(sqlexec, cn)
                                            cmdexec.ExecuteNonQuery()


                                            Dim xjpulang As DateTime = jpulang
                                            If TimeValue(jpulang) >= "02:00:00" And TimeValue(xjpulang) <= "05:00:00" Then
                                                xjpulang = xjpulang.AddHours(-1)
                                            Else
                                                xjpulang = xjpulang.AddHours(-5)
                                            End If

                                            inout_rubah(userid_inout, tanggal_kalen, jamhasil2, xjpulang, cn)


                                        End If

                                    ElseIf DateValue(String.Format("{0} {1}", Microsoft.VisualBasic.Left(tanggal_kalen, 10), Microsoft.VisualBasic.Right(jamhasil, 8))) >= DateValue(String.Format("{0} {1}", Microsoft.VisualBasic.Left(tanggal_kalen, 10), "18:35:00")) And _
                                        DateValue(String.Format("{0} {1}", Microsoft.VisualBasic.Left(tanggal_kalen, 10), Microsoft.VisualBasic.Right(jamhasil, 8))) <= DateValue(String.Format("{0} {1}", Microsoft.VisualBasic.Left(tanggal_kalen, 10), "23:59:59")) Then


                                        Dim sqlcek_sebelum As String = String.Format("select id,kd_shift,jadwalmasuk,jadwalpulang,jammasuk,jampulang,stat from tr_hadir where tanggal='{0}' and nip='{1}' and jampulang is null and stat='HADIR'", convert_date_to_eng(tanggal_kalen), nnip)

                                        If TimeValue(jamhasil) > "00:00:00" And TimeValue(jamhasil) <= "03:00:00" Then
                                            sqlcek_sebelum = String.Format("select id,kd_shift,jadwalmasuk,jadwalpulang,jammasuk,jampulang,stat from tr_hadir where tanggal='{0}' and nip='{1}' and jampulang is null and stat='HADIR'", convert_date_to_eng(DateValue(tanggal_kalen).AddDays(-1)), nnip)
                                        End If

                                        Dim cmdcek_sebelum As OleDbCommand = New OleDbCommand(sqlcek_sebelum, cn)
                                        Dim drdcek_sebelum As OleDbDataReader = cmdcek_sebelum.ExecuteReader


                                        If drdcek_sebelum.HasRows Then
                                            If drdcek_sebelum.Read Then
                                                If Integer.Parse(drdcek_sebelum(0).ToString) > 0 Then
                                                    statcek_sebelum = Integer.Parse(drdcek_sebelum(0).ToString)
                                                    kd_shift = drdcek_sebelum("kd_shift").ToString
                                                    jd_masuk = drdcek_sebelum("jadwalmasuk").ToString
                                                    jd_pulang = drdcek_sebelum("jadwalpulang").ToString
                                                    jm_masuk = drdcek_sebelum("jammasuk").ToString
                                                    st_hadir = drdcek_sebelum("stat").ToString
                                                End If
                                            End If
                                        End If
                                        drdcek_sebelum.Close()

                                        If statcek_sebelum > 0 Then

                                            Dim jamhasil2 As String = convert_datetime_to_eng(jamhasil)

                                            sqlexec = String.Format("update tr_hadir set jampulang='{0}',step=1 where id={1}", jamhasil2, statcek_sebelum)
                                            Dim cmdexec As OleDbCommand = New OleDbCommand(sqlexec, cn)
                                            cmdexec.ExecuteNonQuery()

                                            '' cek hari dulu''
                                            If nip_param.Trim.Length = 0 Then
                                                If TimeValue(jamhasil) > "00:00:00" And TimeValue(jamhasil) <= "03:00:00" Then
                                                    cek_hari(False, DateValue(tanggal_kalen).AddDays(-1))
                                                Else
                                                    cek_hari(False, DateValue(tanggal_kalen))
                                                End If
                                            Else
                                                If jenislibur_hit.Trim.Length = 0 Then
                                                    If TimeValue(jamhasil) > "00:00:00" And TimeValue(jamhasil) <= "03:00:00" Then
                                                        cek_hari(False, DateValue(tanggal_kalen).AddDays(-1))
                                                    Else
                                                        cek_hari(False, DateValue(tanggal_kalen))
                                                    End If
                                                End If
                                            End If
                                            '' end of cek hari

                                            mulai_hitung2(statcek_sebelum, kd_shift, jd_masuk, jd_pulang, jm_masuk, jamhasil2, st_hadir, cn, 0.0)

                                            inout_rubah(userid_inout, tanggal_kalen, jd_masuk, jamhasil2, cn)

                                        Else

                                            '' masuk ''

                                            Dim jmasuk As String = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "23:00:00")
                                            Dim jpulang As String = String.Format("{0} {1}", convert_date_to_eng(DateValue(tanggal_kalen).AddDays(1)), "07:00:00")
                                            Dim jamhasil2 As String = convert_datetime_to_eng(jamhasil)

                                            Dim idx As Boolean = False
                                            If id_param > 0 And nip_param.Trim.Length > 0 And shift_param.Trim.Length = 0 Then

                                                Dim sqlx As String = String.Format("select id from tr_hadir where id={0} and jadwalmasuk is null and jadwalpulang is null and jammasuk is null and jampulang is null", _
                                                                                     id_param)
                                                Dim cmdx As OleDbCommand = New OleDbCommand(sqlx, cn)
                                                Dim drdx As OleDbDataReader = cmdx.ExecuteReader

                                                If drdx.Read Then
                                                    If drdx("id").ToString <> "" Then
                                                        idx = True
                                                    End If
                                                End If
                                                drdx.Close()


                                            End If

                                            If idx = False Then
                                                sqlexec = String.Format("insert into tr_hadir (nip,tanggal,jadwalmasuk,jadwalpulang,jammasuk,jampulang,stat,kdgol,kd_shift,depart,step,stat_hari) values ('{0}','{1}','{2}','{3}','{4}',null,'{5}','{6}','{7}','{8}',0,'{9}')", _
                                                                       nnip, convert_date_to_eng(tanggal_kalen), jmasuk, jpulang, jamhasil2, "HADIR", kdgol, "SHIFT-III", depart, jenislibur_hit)
                                            Else
                                                sqlexec = String.Format("update tr_hadir set jadwalmasuk='{0}',jadwalpulang='{1}',jammasuk='{2}',stat='{3}',kdgol='{4}',kd_shift='{5}',depart='{6}',stat_hari='{7}' where id={8}", _
                                                                      jmasuk, jpulang, jamhasil2, "HADIR", kdgol, "SHIFT-III", depart, jenislibur_hit, id_param)
                                            End If


                                            Dim cmdexec As OleDbCommand = New OleDbCommand(sqlexec, cn)
                                            cmdexec.ExecuteNonQuery()


                                            Dim xjpulang As DateTime = jpulang
                                            If TimeValue(jpulang) >= "02:00:00" And TimeValue(xjpulang) <= "05:00:00" Then
                                                xjpulang = xjpulang.AddHours(-1)
                                            Else
                                                xjpulang = xjpulang.AddHours(-5)
                                            End If

                                            inout_rubah(userid_inout, tanggal_kalen, jamhasil2, xjpulang, cn)


                                        End If


                                    End If

                                Case "MAINTENCE", "BORONGAN SCRUB", "YARN"

                                    If TimeValue(jamhasil) >= "04:00:00" And TimeValue(jamhasil) <= "09:00:00" Then

                                        Dim tglm_a1 As String = String.Format("{0} {1}", Microsoft.VisualBasic.Left(convert_date_to_eng(DateValue(tanggal_kalen).AddDays(-1)), 10), "09:35:00")
                                        Dim tglm_a2 As String = String.Format("{0} {1}", Microsoft.VisualBasic.Left(convert_date_to_eng(DateValue(tanggal_kalen).AddDays(-1)), 10), "23:59:59")

                                        Dim sqlcek_sebelum As String = String.Format("select id,kd_shift,jadwalmasuk,jadwalpulang,jammasuk,jampulang,stat from tr_hadir where tanggal='{0}' and nip='{1}' and jampulang is null and stat='HADIR' and jammasuk>='{2}' and jammasuk<='{3}'", _
                                                                                     convert_date_to_eng(DateValue(tanggal_kalen).AddDays(-1)), nnip, tglm_a1, tglm_a2)

                                        Dim cmdcek_sebelum As OleDbCommand = New OleDbCommand(sqlcek_sebelum, cn)
                                        Dim drdcek_sebelum As OleDbDataReader = cmdcek_sebelum.ExecuteReader


                                        If drdcek_sebelum.HasRows Then
                                            If drdcek_sebelum.Read Then
                                                If Integer.Parse(drdcek_sebelum(0).ToString) > 0 Then
                                                    statcek_sebelum = Integer.Parse(drdcek_sebelum(0).ToString)
                                                    kd_shift = drdcek_sebelum("kd_shift").ToString
                                                    jd_masuk = drdcek_sebelum("jadwalmasuk").ToString
                                                    jd_pulang = drdcek_sebelum("jadwalpulang").ToString
                                                    jm_masuk = drdcek_sebelum("jammasuk").ToString
                                                    st_hadir = drdcek_sebelum("stat").ToString
                                                End If
                                            End If
                                        End If
                                        drdcek_sebelum.Close()

                                        If statcek_sebelum = 0 Then

                                            '' masuk ''

                                            Dim jmasuk As String = Nothing
                                            Dim jpulang As String = Nothing

                                            If TimeValue(jamhasil) <= "07:16:00" Then
                                                jmasuk = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "07:00:00")
                                                jpulang = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "15:00:00")
                                            Else
                                                jmasuk = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "08:00:00")
                                                jpulang = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "16:00:00")
                                            End If

                                            Dim jamhasil2 As String = convert_datetime_to_eng(jamhasil)

                                            Dim idx As Boolean = False
                                            If id_param > 0 And nip_param.Trim.Length > 0 And shift_param.Trim.Length = 0 Then

                                                Dim sqlx As String = String.Format("select id from tr_hadir where id={0} and jadwalmasuk is null and jadwalpulang is null and jammasuk is null and jampulang is null", _
                                                                                     id_param)
                                                Dim cmdx As OleDbCommand = New OleDbCommand(sqlx, cn)
                                                Dim drdx As OleDbDataReader = cmdx.ExecuteReader

                                                If drdx.Read Then
                                                    If drdx("id").ToString <> "" Then
                                                        idx = True
                                                    End If
                                                End If
                                                drdx.Close()


                                            End If

                                            If idx = False Then
                                                sqlexec = String.Format("insert into tr_hadir (nip,tanggal,jadwalmasuk,jadwalpulang,jammasuk,jampulang,stat,kdgol,kd_shift,depart,step,stat_hari) values ('{0}','{1}','{2}','{3}','{4}',null,'{5}','{6}','{7}','{8}',0,'{9}')", _
                                                                    nnip, convert_date_to_eng(tanggal_kalen), jmasuk, jpulang, jamhasil2, "HADIR", kdgol, "SHIFT-I", depart, jenislibur_hit)
                                            Else
                                                sqlexec = String.Format("update tr_hadir set jadwalmasuk='{0}',jadwalpulang='{1}',jammasuk='{2}',stat='{3}',kdgol='{4}',kd_shift='{5}',depart='{6}',stat_hari='{7}' where id={8}", _
                                                                      jmasuk, jpulang, jamhasil2, "HADIR", kdgol, "SHIFT-I", depart, jenislibur_hit, id_param)
                                            End If


                                            Dim cmdexec As OleDbCommand = New OleDbCommand(sqlexec, cn)
                                            cmdexec.ExecuteNonQuery()


                                            Dim xjpulang As DateTime = jpulang
                                            If TimeValue(jpulang) >= "02:00:00" And TimeValue(xjpulang) <= "05:00:00" Then
                                                xjpulang = xjpulang.AddHours(-1)
                                            Else
                                                xjpulang = xjpulang.AddHours(-5)
                                            End If

                                            inout_rubah(userid_inout, tanggal_kalen, jamhasil2, xjpulang, cn)


                                        Else

                                            Dim jamhasil2 As String = convert_datetime_to_eng(jamhasil)

                                            sqlexec = String.Format("update tr_hadir set jampulang='{0}',step=1 where id={1}", jamhasil2, statcek_sebelum)
                                            Dim cmdexec As OleDbCommand = New OleDbCommand(sqlexec, cn)
                                            cmdexec.ExecuteNonQuery()

                                            '' cek hari dulu''
                                            If nip_param.Trim.Length = 0 Then
                                                cek_hari(False, DateValue(tanggal_kalen).AddDays(-1))
                                            Else
                                                If jenislibur_hit.Trim.Length = 0 Then
                                                    cek_hari(False, DateValue(tanggal_kalen).AddDays(-1))
                                                End If
                                            End If
                                            '' end of cek hari

                                            mulai_hitung2(statcek_sebelum, kd_shift, jd_masuk, jd_pulang, jm_masuk, jamhasil2, st_hadir, cn, 0.0)

                                            inout_rubah(userid_inout, tanggal_kalen.AddDays(-1), jd_masuk, jamhasil2, cn)

                                        End If

                                    ElseIf TimeValue(jamhasil) >= "09:35:00" And TimeValue(jamhasil) <= "18:05:00" Then

                                        Dim sqlcek_sebelum As String = String.Format("select id,kd_shift,jadwalmasuk,jadwalpulang,jammasuk,jampulang,stat from tr_hadir where tanggal='{0}' and nip='{1}' and jampulang is null and stat='HADIR'", convert_date_to_eng(tanggal_kalen), nnip)
                                        Dim cmdcek_sebelum As OleDbCommand = New OleDbCommand(sqlcek_sebelum, cn)
                                        Dim drdcek_sebelum As OleDbDataReader = cmdcek_sebelum.ExecuteReader

                                        If drdcek_sebelum.HasRows Then
                                            If drdcek_sebelum.Read Then
                                                If Integer.Parse(drdcek_sebelum(0).ToString) > 0 Then
                                                    statcek_sebelum = Integer.Parse(drdcek_sebelum(0).ToString)
                                                    kd_shift = drdcek_sebelum("kd_shift").ToString
                                                    jd_masuk = drdcek_sebelum("jadwalmasuk").ToString
                                                    jd_pulang = drdcek_sebelum("jadwalpulang").ToString
                                                    jm_masuk = drdcek_sebelum("jammasuk").ToString
                                                    st_hadir = drdcek_sebelum("stat").ToString
                                                End If
                                            End If
                                        End If
                                        drdcek_sebelum.Close()

                                        If statcek_sebelum > 0 Then

                                            Dim jamhasil2 As String = convert_datetime_to_eng(jamhasil)

                                            sqlexec = String.Format("update tr_hadir set jampulang='{0}',step=1 where id={1}", jamhasil2, statcek_sebelum)
                                            Dim cmdexec As OleDbCommand = New OleDbCommand(sqlexec, cn)
                                            cmdexec.ExecuteNonQuery()

                                            '' cek hari dulu''
                                            If nip_param.Trim.Length = 0 Then
                                                cek_hari(False, DateValue(tanggal_kalen))
                                            Else
                                                If jenislibur_hit.Trim.Length = 0 Then
                                                    cek_hari(False, DateValue(tanggal_kalen))
                                                End If
                                            End If
                                            '' end of cek hari

                                            mulai_hitung2(statcek_sebelum, kd_shift, jd_masuk, jd_pulang, jm_masuk, jamhasil2, st_hadir, cn, 0.0)

                                            inout_rubah(userid_inout, tanggal_kalen, jd_masuk, jamhasil2, cn)

                                        Else

                                            '' masuk ''

                                            Dim jmasuk As String = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "15:00:00")
                                            Dim jpulang As String = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "23:00:00")
                                            Dim jamhasil2 As String = convert_datetime_to_eng(jamhasil)

                                            Dim idx As Boolean = False
                                            If id_param > 0 And nip_param.Trim.Length > 0 And shift_param.Trim.Length = 0 Then

                                                Dim sqlx As String = String.Format("select id from tr_hadir where id={0} and jadwalmasuk is null and jadwalpulang is null and jammasuk is null and jampulang is null", _
                                                                                     id_param)
                                                Dim cmdx As OleDbCommand = New OleDbCommand(sqlx, cn)
                                                Dim drdx As OleDbDataReader = cmdx.ExecuteReader

                                                If drdx.Read Then
                                                    If drdx("id").ToString <> "" Then
                                                        idx = True
                                                    End If
                                                End If
                                                drdx.Close()


                                            End If

                                            If idx = False Then
                                                sqlexec = String.Format("insert into tr_hadir (nip,tanggal,jadwalmasuk,jadwalpulang,jammasuk,jampulang,stat,kdgol,kd_shift,depart,step,stat_hari) values ('{0}','{1}','{2}','{3}','{4}',null,'{5}','{6}','{7}','{8}',0,'{9}')", _
                                                                    nnip, convert_date_to_eng(tanggal_kalen), jmasuk, jpulang, jamhasil2, "HADIR", kdgol, "SHIFT-II", depart, jenislibur_hit)
                                            Else
                                                sqlexec = String.Format("update tr_hadir set jadwalmasuk='{0}',jadwalpulang='{1}',jammasuk='{2}',stat='{3}',kdgol='{4}',kd_shift='{5}',depart='{6}',stat_hari='{7}' where id={8}", _
                                                                      jmasuk, jpulang, jamhasil2, "HADIR", kdgol, "SHIFT-II", depart, jenislibur_hit, id_param)
                                            End If

                                            Dim cmdexec As OleDbCommand = New OleDbCommand(sqlexec, cn)
                                            cmdexec.ExecuteNonQuery()

                                            Dim xjpulang As DateTime = jpulang
                                            If TimeValue(jpulang) >= "02:00:00" And TimeValue(xjpulang) <= "05:00:00" Then
                                                xjpulang = xjpulang.AddHours(-1)
                                            Else
                                                xjpulang = xjpulang.AddHours(-5)
                                            End If

                                            inout_rubah(userid_inout, tanggal_kalen, jamhasil2, xjpulang, cn)



                                        End If

                                    ElseIf TimeValue(jamhasil) >= "18:06:00" And TimeValue(jamhasil) <= "20:00:00" Then

                                        Dim sqlcek_sebelum As String = String.Format("select id,kd_shift,jadwalmasuk,jadwalpulang,jammasuk,jampulang,stat from tr_hadir where tanggal='{0}' and nip='{1}' and jampulang is null and stat='HADIR'", convert_date_to_eng(tanggal_kalen), nnip)
                                        Dim cmdcek_sebelum As OleDbCommand = New OleDbCommand(sqlcek_sebelum, cn)
                                        Dim drdcek_sebelum As OleDbDataReader = cmdcek_sebelum.ExecuteReader

                                        If drdcek_sebelum.HasRows Then
                                            If drdcek_sebelum.Read Then
                                                If Integer.Parse(drdcek_sebelum(0).ToString) > 0 Then
                                                    statcek_sebelum = Integer.Parse(drdcek_sebelum(0).ToString)
                                                    kd_shift = drdcek_sebelum("kd_shift").ToString
                                                    jd_masuk = drdcek_sebelum("jadwalmasuk").ToString
                                                    jd_pulang = drdcek_sebelum("jadwalpulang").ToString
                                                    jm_masuk = drdcek_sebelum("jammasuk").ToString
                                                    st_hadir = drdcek_sebelum("stat").ToString
                                                End If
                                            End If
                                        End If
                                        drdcek_sebelum.Close()

                                        If statcek_sebelum > 0 Then

                                            Dim jamhasil2 As String = convert_datetime_to_eng(jamhasil)

                                            sqlexec = String.Format("update tr_hadir set jampulang='{0}',step=1 where id={1}", jamhasil2, statcek_sebelum)
                                            Dim cmdexec As OleDbCommand = New OleDbCommand(sqlexec, cn)
                                            cmdexec.ExecuteNonQuery()

                                            '' cek hari dulu''
                                            If nip_param.Trim.Length = 0 Then
                                                cek_hari(False, DateValue(tanggal_kalen))
                                            Else
                                                If jenislibur_hit.Trim.Length = 0 Then
                                                    cek_hari(False, DateValue(tanggal_kalen))
                                                End If
                                            End If
                                            '' end of cek hari

                                            mulai_hitung2(statcek_sebelum, kd_shift, jd_masuk, jd_pulang, jm_masuk, jamhasil2, st_hadir, cn, 0.0)

                                            inout_rubah(userid_inout, tanggal_kalen, jd_masuk, jamhasil2, cn)

                                        Else

                                            '' masuk ''

                                            Dim jmasuk As String = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "19:00:00")
                                            Dim jpulang As String = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "04:00:00")
                                            Dim jamhasil2 As String = convert_datetime_to_eng(jamhasil)

                                            Dim idx As Boolean = False
                                            If id_param > 0 And nip_param.Trim.Length > 0 And shift_param.Trim.Length = 0 Then

                                                Dim sqlx As String = String.Format("select id from tr_hadir where id={0} and jadwalmasuk is null and jadwalpulang is null and jammasuk is null and jampulang is null", _
                                                                                     id_param)
                                                Dim cmdx As OleDbCommand = New OleDbCommand(sqlx, cn)
                                                Dim drdx As OleDbDataReader = cmdx.ExecuteReader

                                                If drdx.Read Then
                                                    If drdx("id").ToString <> "" Then
                                                        idx = True
                                                    End If
                                                End If
                                                drdx.Close()


                                            End If

                                            If idx = False Then
                                                sqlexec = String.Format("insert into tr_hadir (nip,tanggal,jadwalmasuk,jadwalpulang,jammasuk,jampulang,stat,kdgol,kd_shift,depart,step,stat_hari) values ('{0}','{1}','{2}','{3}','{4}',null,'{5}','{6}','{7}','{8}',0,'{9}')", _
                                                                    nnip, convert_date_to_eng(tanggal_kalen), jmasuk, jpulang, jamhasil2, "HADIR", kdgol, "SHIFT-B", depart, jenislibur_hit)
                                            Else
                                                sqlexec = String.Format("update tr_hadir set jadwalmasuk='{0}',jadwalpulang='{1}',jammasuk='{2}',stat='{3}',kdgol='{4}',kd_shift='{5}',depart='{6}',stat_hari='{7}' where id={8}", _
                                                                      jmasuk, jpulang, jamhasil2, "HADIR", kdgol, "SHIFT-B", depart, jenislibur_hit, id_param)
                                            End If

                                            Dim cmdexec As OleDbCommand = New OleDbCommand(sqlexec, cn)
                                            cmdexec.ExecuteNonQuery()

                                            Dim xjpulang As DateTime = jpulang
                                            If TimeValue(jpulang) >= "02:00:00" And TimeValue(xjpulang) <= "05:00:00" Then
                                                xjpulang = xjpulang.AddHours(-1)
                                            Else
                                                xjpulang = xjpulang.AddHours(-5)
                                            End If

                                            inout_rubah(userid_inout, tanggal_kalen, jamhasil2, xjpulang, cn)



                                        End If


                                    ElseIf DateValue(String.Format("{0} {1}", Microsoft.VisualBasic.Left(tanggal_kalen, 10), Microsoft.VisualBasic.Right(jamhasil, 8))) >= DateValue(String.Format("{0} {1}", Microsoft.VisualBasic.Left(tanggal_kalen, 10), "20:05:00")) And _
                                        DateValue(String.Format("{0} {1}", Microsoft.VisualBasic.Left(tanggal_kalen, 10), Microsoft.VisualBasic.Right(jamhasil, 8))) <= DateValue(String.Format("{0} {1}", Microsoft.VisualBasic.Left(tanggal_kalen, 10), "23:59:59")) Then


                                        Dim sqlcek_sebelum As String = String.Format("select id,kd_shift,jadwalmasuk,jadwalpulang,jammasuk,jampulang,stat from tr_hadir where tanggal='{0}' and nip='{1}' and jampulang is null and stat='HADIR'", convert_date_to_eng(tanggal_kalen), nnip)

                                        If TimeValue(jamhasil) > "00:00:00" And TimeValue(jamhasil) <= "03:00:00" Then
                                            sqlcek_sebelum = String.Format("select id,kd_shift,jadwalmasuk,jadwalpulang,jammasuk,jampulang,stat from tr_hadir where tanggal='{0}' and nip='{1}' and jampulang is null and stat='HADIR'", convert_date_to_eng(DateValue(tanggal_kalen).AddDays(-1)), nnip)
                                        End If

                                        Dim cmdcek_sebelum As OleDbCommand = New OleDbCommand(sqlcek_sebelum, cn)
                                        Dim drdcek_sebelum As OleDbDataReader = cmdcek_sebelum.ExecuteReader


                                        If drdcek_sebelum.HasRows Then
                                            If drdcek_sebelum.Read Then
                                                If Integer.Parse(drdcek_sebelum(0).ToString) > 0 Then
                                                    statcek_sebelum = Integer.Parse(drdcek_sebelum(0).ToString)
                                                    kd_shift = drdcek_sebelum("kd_shift").ToString
                                                    jd_masuk = drdcek_sebelum("jadwalmasuk").ToString
                                                    jd_pulang = drdcek_sebelum("jadwalpulang").ToString
                                                    jm_masuk = drdcek_sebelum("jammasuk").ToString
                                                    st_hadir = drdcek_sebelum("stat").ToString
                                                End If
                                            End If
                                        End If
                                        drdcek_sebelum.Close()

                                        If statcek_sebelum > 0 Then

                                            Dim jamhasil2 As String = convert_datetime_to_eng(jamhasil)

                                            sqlexec = String.Format("update tr_hadir set jampulang='{0}',step=1 where id={1}", jamhasil2, statcek_sebelum)
                                            Dim cmdexec As OleDbCommand = New OleDbCommand(sqlexec, cn)
                                            cmdexec.ExecuteNonQuery()

                                            '' cek hari dulu''
                                            If nip_param.Trim.Length = 0 Then
                                                If TimeValue(jamhasil) > "00:00:00" And TimeValue(jamhasil) <= "03:00:00" Then
                                                    cek_hari(False, DateValue(tanggal_kalen).AddDays(-1))
                                                Else
                                                    cek_hari(False, DateValue(tanggal_kalen))
                                                End If
                                            Else
                                                If jenislibur_hit.Trim.Length = 0 Then
                                                    If TimeValue(jamhasil) > "00:00:00" And TimeValue(jamhasil) <= "03:00:00" Then
                                                        cek_hari(False, DateValue(tanggal_kalen).AddDays(-1))
                                                    Else
                                                        cek_hari(False, DateValue(tanggal_kalen))
                                                    End If
                                                End If
                                            End If
                                            '' end of cek hari


                                            mulai_hitung2(statcek_sebelum, kd_shift, jd_masuk, jd_pulang, jm_masuk, jamhasil2, st_hadir, cn, 0.0)

                                            inout_rubah(userid_inout, tanggal_kalen, jd_masuk, jamhasil2, cn)

                                        Else

                                            '' masuk ''

                                            Dim jmasuk As String = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "23:00:00")
                                            Dim jpulang As String = String.Format("{0} {1}", convert_date_to_eng(DateValue(tanggal_kalen).AddDays(1)), "07:00:00")
                                            Dim jamhasil2 As String = convert_datetime_to_eng(jamhasil)

                                            Dim idx As Boolean = False
                                            If id_param > 0 And nip_param.Trim.Length > 0 And shift_param.Trim.Length = 0 Then

                                                Dim sqlx As String = String.Format("select id from tr_hadir where id={0} and jadwalmasuk is null and jadwalpulang is null and jammasuk is null and jampulang is null", _
                                                                                     id_param)
                                                Dim cmdx As OleDbCommand = New OleDbCommand(sqlx, cn)
                                                Dim drdx As OleDbDataReader = cmdx.ExecuteReader

                                                If drdx.Read Then
                                                    If drdx("id").ToString <> "" Then
                                                        idx = True
                                                    End If
                                                End If
                                                drdx.Close()


                                            End If

                                            If idx = False Then
                                                sqlexec = String.Format("insert into tr_hadir (nip,tanggal,jadwalmasuk,jadwalpulang,jammasuk,jampulang,stat,kdgol,kd_shift,depart,step,stat_hari) values ('{0}','{1}','{2}','{3}','{4}',null,'{5}','{6}','{7}','{8}',0,'{9}')", _
                                                                    nnip, convert_date_to_eng(tanggal_kalen), jmasuk, jpulang, jamhasil2, "HADIR", kdgol, "SHIFT-III", depart, jenislibur_hit)
                                            Else
                                                sqlexec = String.Format("update tr_hadir set jadwalmasuk='{0}',jadwalpulang='{1}',jammasuk='{2}',stat='{3}',kdgol='{4}',kd_shift='{5}',depart='{6}',stat_hari='{7}' where id={8}", _
                                                                      jmasuk, jpulang, jamhasil2, "HADIR", kdgol, "SHIFT-III", depart, jenislibur_hit, id_param)
                                            End If


                                            Dim cmdexec As OleDbCommand = New OleDbCommand(sqlexec, cn)
                                            cmdexec.ExecuteNonQuery()


                                            Dim xjpulang As DateTime = jpulang
                                            If TimeValue(jpulang) >= "02:00:00" And TimeValue(xjpulang) <= "05:00:00" Then
                                                xjpulang = xjpulang.AddHours(-1)
                                            Else
                                                xjpulang = xjpulang.AddHours(-5)
                                            End If

                                            inout_rubah(userid_inout, tanggal_kalen, jamhasil2, xjpulang, cn)


                                        End If


                                    End If

                                Case "JAIT JUMBO BAG", "QC JUMBO BAG", "SETTING INER", "PAKING", "QC INNER  SETTING"

                                    If TimeValue(jamhasil) >= "04:00:00" And TimeValue(jamhasil) <= "09:00:00" Then

                                        '' masuk ''

                                        Dim jmasuk As String = Nothing
                                        Dim jpulang As String = Nothing

                                        If TimeValue(jamhasil) <= "07:16:00" Then
                                            jmasuk = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "07:00:00")
                                            jpulang = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "15:00:00")
                                        Else
                                            jmasuk = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "08:00:00")
                                            jpulang = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "16:00:00")
                                        End If

                                        Dim jamhasil2 As String = convert_datetime_to_eng(jamhasil)

                                        Dim idx As Boolean = False
                                        If id_param > 0 And nip_param.Trim.Length > 0 And shift_param.Trim.Length = 0 Then

                                            Dim sqlx As String = String.Format("select id from tr_hadir where id={0} and jadwalmasuk is null and jadwalpulang is null and jammasuk is null and jampulang is null", _
                                                                                 convert_date_to_eng(DateValue(tanggal_kalen)), nnip)
                                            Dim cmdx As OleDbCommand = New OleDbCommand(sqlx, cn)
                                            Dim drdx As OleDbDataReader = cmdx.ExecuteReader

                                            If drdx.Read Then
                                                If drdx("id").ToString <> "" Then
                                                    idx = True
                                                End If
                                            End If
                                            drdx.Close()


                                        End If

                                        If idx = False Then
                                            sqlexec = String.Format("insert into tr_hadir (nip,tanggal,jadwalmasuk,jadwalpulang,jammasuk,jampulang,stat,kdgol,kd_shift,depart,step,stat_hari) values ('{0}','{1}','{2}','{3}','{4}',null,'{5}','{6}','{7}','{8}',0,'{9}')", _
                                                                    nnip, convert_date_to_eng(tanggal_kalen), jmasuk, jpulang, jamhasil2, "HADIR", kdgol, "SHIFT-I", depart, jenislibur_hit)
                                        Else
                                            sqlexec = String.Format("update tr_hadir set jadwalmasuk='{0}',jadwalpulang='{1}',jammasuk='{2}',stat='{3}',kdgol='{4}',kd_shift='{5}',depart='{6}',stat_hari='{7}' where id={8}", _
                                                                  jmasuk, jpulang, jamhasil2, "HADIR", kdgol, "SHIFT-I", depart, jenislibur_hit, id_param)
                                        End If

                                        Dim cmdexec As OleDbCommand = New OleDbCommand(sqlexec, cn)
                                        cmdexec.ExecuteNonQuery()


                                        Dim xjpulang As DateTime = jpulang
                                        If TimeValue(jpulang) >= "02:00:00" And TimeValue(xjpulang) <= "05:00:00" Then
                                            xjpulang = xjpulang.AddHours(-1)
                                        Else
                                            xjpulang = xjpulang.AddHours(-5)
                                        End If

                                        inout_rubah(userid_inout, tanggal_kalen, jamhasil2, xjpulang, cn)


                                    ElseIf TimeValue(jamhasil) >= "09:35:00" And TimeValue(jamhasil) <= "18:05:00" Then

                                        Dim sqlcek_sebelum As String = String.Format("select id,kd_shift,jadwalmasuk,jadwalpulang,jammasuk,jampulang,stat from tr_hadir where tanggal='{0}' and nip='{1}' and jampulang is null and stat='HADIR'", convert_date_to_eng(tanggal_kalen), nnip)
                                        Dim cmdcek_sebelum As OleDbCommand = New OleDbCommand(sqlcek_sebelum, cn)
                                        Dim drdcek_sebelum As OleDbDataReader = cmdcek_sebelum.ExecuteReader

                                        If drdcek_sebelum.HasRows Then
                                            If drdcek_sebelum.Read Then
                                                If Integer.Parse(drdcek_sebelum(0).ToString) > 0 Then
                                                    statcek_sebelum = Integer.Parse(drdcek_sebelum(0).ToString)
                                                    kd_shift = drdcek_sebelum("kd_shift").ToString
                                                    jd_masuk = drdcek_sebelum("jadwalmasuk").ToString
                                                    jd_pulang = drdcek_sebelum("jadwalpulang").ToString
                                                    jm_masuk = drdcek_sebelum("jammasuk").ToString
                                                    st_hadir = drdcek_sebelum("stat").ToString
                                                End If
                                            End If
                                        End If
                                        drdcek_sebelum.Close()

                                        If statcek_sebelum > 0 Then

                                            Dim jamhasil2 As String = convert_datetime_to_eng(jamhasil)

                                            sqlexec = String.Format("update tr_hadir set jampulang='{0}',step=1 where id={1}", jamhasil2, statcek_sebelum)
                                            Dim cmdexec As OleDbCommand = New OleDbCommand(sqlexec, cn)
                                            cmdexec.ExecuteNonQuery()

                                            '' cek hari dulu''
                                            If nip_param.Trim.Length = 0 Then
                                                cek_hari(False, DateValue(tanggal_kalen))
                                            Else
                                                If jenislibur_hit.Trim.Length = 0 Then
                                                    cek_hari(False, DateValue(tanggal_kalen))
                                                End If
                                            End If
                                            '' end of cek hari

                                            mulai_hitung2(statcek_sebelum, kd_shift, jd_masuk, jd_pulang, jm_masuk, jamhasil2, st_hadir, cn, 0.0)

                                            inout_rubah(userid_inout, tanggal_kalen, jd_masuk, jamhasil2, cn)

                                        Else

                                            '' masuk ''

                                            Dim jmasuk As String = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "15:00:00")
                                            Dim jpulang As String = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "23:00:00")
                                            Dim jamhasil2 As String = convert_datetime_to_eng(jamhasil)

                                            Dim idx As Boolean = False
                                            If id_param > 0 And nip_param.Trim.Length > 0 And shift_param.Trim.Length = 0 Then

                                                Dim sqlx As String = String.Format("select id from tr_hadir where id={0} and jadwalmasuk is null and jadwalpulang is null and jammasuk is null and jampulang is null", _
                                                                                     id_param)
                                                Dim cmdx As OleDbCommand = New OleDbCommand(sqlx, cn)
                                                Dim drdx As OleDbDataReader = cmdx.ExecuteReader

                                                If drdx.Read Then
                                                    If drdx("id").ToString <> "" Then
                                                        idx = True
                                                    End If
                                                End If
                                                drdx.Close()


                                            End If

                                            If idx = False Then
                                                sqlexec = String.Format("insert into tr_hadir (nip,tanggal,jadwalmasuk,jadwalpulang,jammasuk,jampulang,stat,kdgol,kd_shift,depart,step,stat_hari) values ('{0}','{1}','{2}','{3}','{4}',null,'{5}','{6}','{7}','{8}',0,'{9}')", _
                                                                    nnip, convert_date_to_eng(tanggal_kalen), jmasuk, jpulang, jamhasil2, "HADIR", kdgol, "SHIFT-II", depart, jenislibur_hit)
                                            Else
                                                sqlexec = String.Format("update tr_hadir set jadwalmasuk='{0}',jadwalpulang='{1}',jammasuk='{2}',stat='{3}',kdgol='{4}',kd_shift='{5}',depart='{6}',stat_hari='{7}' where id={8}", _
                                                                      jmasuk, jpulang, jamhasil2, "HADIR", kdgol, "SHIFT-II", depart, jenislibur_hit, id_param)
                                            End If

                                            Dim cmdexec As OleDbCommand = New OleDbCommand(sqlexec, cn)
                                            cmdexec.ExecuteNonQuery()


                                            Dim xjpulang As DateTime = jpulang
                                            If TimeValue(jpulang) >= "02:00:00" And TimeValue(xjpulang) <= "05:00:00" Then
                                                xjpulang = xjpulang.AddHours(-1)
                                            Else
                                                xjpulang = xjpulang.AddHours(-5)
                                            End If

                                            inout_rubah(userid_inout, tanggal_kalen, jamhasil2, xjpulang, cn)


                                        End If

                                    ElseIf DateValue(String.Format("{0} {1}", Microsoft.VisualBasic.Left(tanggal_kalen, 10), Microsoft.VisualBasic.Right(jamhasil, 8))) >= DateValue(String.Format("{0} {1}", Microsoft.VisualBasic.Left(tanggal_kalen, 10), "18:35:00")) And _
                                        DateValue(String.Format("{0} {1}", Microsoft.VisualBasic.Left(tanggal_kalen, 10), Microsoft.VisualBasic.Right(jamhasil, 8))) <= DateValue(String.Format("{0} {1}", Microsoft.VisualBasic.Left(tanggal_kalen, 10), "23:59:59")) Then


                                        Dim sqlcek_sebelum As String = String.Format("select id,kd_shift,jadwalmasuk,jadwalpulang,jammasuk,jampulang,stat from tr_hadir where tanggal='{0}' and nip='{1}' and jampulang is null and stat='HADIR'", convert_date_to_eng(tanggal_kalen), nnip)

                                        If TimeValue(jamhasil) > "00:00:00" And TimeValue(jamhasil) <= "03:00:00" Then
                                            sqlcek_sebelum = String.Format("select id,kd_shift,jadwalmasuk,jadwalpulang,jammasuk,jampulang,stat from tr_hadir where tanggal='{0}' and nip='{1}' and jampulang is null and stat='HADIR'", convert_date_to_eng(DateValue(tanggal_kalen).AddDays(-1)), nnip)
                                        End If

                                        Dim cmdcek_sebelum As OleDbCommand = New OleDbCommand(sqlcek_sebelum, cn)
                                        Dim drdcek_sebelum As OleDbDataReader = cmdcek_sebelum.ExecuteReader

                                        If drdcek_sebelum.HasRows Then
                                            If drdcek_sebelum.Read Then
                                                If Integer.Parse(drdcek_sebelum(0).ToString) > 0 Then
                                                    statcek_sebelum = Integer.Parse(drdcek_sebelum(0).ToString)
                                                    kd_shift = drdcek_sebelum("kd_shift").ToString
                                                    jd_masuk = drdcek_sebelum("jadwalmasuk").ToString
                                                    jd_pulang = drdcek_sebelum("jadwalpulang").ToString
                                                    jm_masuk = drdcek_sebelum("jammasuk").ToString
                                                    st_hadir = drdcek_sebelum("stat").ToString
                                                End If
                                            End If
                                        End If
                                        drdcek_sebelum.Close()

                                        If statcek_sebelum > 0 Then

                                            Dim jamhasil2 As String = convert_datetime_to_eng(jamhasil)

                                            sqlexec = String.Format("update tr_hadir set jampulang='{0}',step=1 where id={1}", jamhasil2, statcek_sebelum)
                                            Dim cmdexec As OleDbCommand = New OleDbCommand(sqlexec, cn)
                                            cmdexec.ExecuteNonQuery()

                                            '' cek hari dulu''
                                            If nip_param.Trim.Length = 0 Then
                                                If TimeValue(jamhasil) > "00:00:00" And TimeValue(jamhasil) <= "03:00:00" Then
                                                    cek_hari(False, DateValue(tanggal_kalen).AddDays(-1))
                                                Else
                                                    cek_hari(False, DateValue(tanggal_kalen))
                                                End If
                                            Else
                                                If jenislibur_hit.Trim.Length = 0 Then
                                                    If TimeValue(jamhasil) > "00:00:00" And TimeValue(jamhasil) <= "03:00:00" Then
                                                        cek_hari(False, DateValue(tanggal_kalen).AddDays(-1))
                                                    Else
                                                        cek_hari(False, DateValue(tanggal_kalen))
                                                    End If
                                                End If
                                            End If
                                            '' end of cek hari

                                            mulai_hitung2(statcek_sebelum, kd_shift, jd_masuk, jd_pulang, jm_masuk, jamhasil2, st_hadir, cn, 0.0)

                                            inout_rubah(userid_inout, tanggal_kalen, jd_masuk, jamhasil2, cn)

                                        End If



                                    End If

                                Case "WAREHOUSE", "CUTTING JUMBO", "STAFF"

                                    If TimeValue(jamhasil) >= "04:00:00" And TimeValue(jamhasil) <= "09:00:00" Then

                                        '' masuk ''

                                        Dim jmasuk As String = Nothing
                                        Dim jpulang As String = Nothing

                                        If TimeValue(jamhasil) <= "07:16:00" Then
                                            jmasuk = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "07:00:00")
                                            jpulang = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "15:00:00")
                                        Else
                                            jmasuk = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "08:00:00")
                                            jpulang = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), "16:00:00")
                                        End If

                                        Dim jamhasil2 As String = convert_datetime_to_eng(jamhasil)

                                        Dim idx As Boolean = False
                                        If id_param > 0 And nip_param.Trim.Length > 0 And shift_param.Trim.Length = 0 Then

                                            Dim sqlx As String = String.Format("select id from tr_hadir where id={0} and jadwalmasuk is null and jadwalpulang is null and jammasuk is null and jampulang is null", _
                                                                                 id_param)
                                            Dim cmdx As OleDbCommand = New OleDbCommand(sqlx, cn)
                                            Dim drdx As OleDbDataReader = cmdx.ExecuteReader

                                            If drdx.Read Then
                                                If drdx("id").ToString <> "" Then
                                                    idx = True
                                                End If
                                            End If
                                            drdx.Close()


                                        End If

                                        If idx = False Then
                                            sqlexec = String.Format("insert into tr_hadir (nip,tanggal,jadwalmasuk,jadwalpulang,jammasuk,jampulang,stat,kdgol,kd_shift,depart,step,stat_hari) values ('{0}','{1}','{2}','{3}','{4}',null,'{5}','{6}','{7}','{8}',0,'{9}')", _
                                                                    nnip, convert_date_to_eng(tanggal_kalen), jmasuk, jpulang, jamhasil2, "HADIR", kdgol, "SHIFT-I", depart, jenislibur_hit)
                                        Else
                                            sqlexec = String.Format("update tr_hadir set jadwalmasuk='{0}',jadwalpulang='{1}',jammasuk='{2}',stat='{3}',kdgol='{4}',kd_shift='{5}',depart='{6}',stat_hari='{7}' where id={8}", _
                                                                  jmasuk, jpulang, jamhasil2, "HADIR", kdgol, "SHIFT-I", depart, jenislibur_hit, id_param)
                                        End If

                                        Dim cmdexec As OleDbCommand = New OleDbCommand(sqlexec, cn)
                                        cmdexec.ExecuteNonQuery()


                                        Dim xjpulang As DateTime = jpulang
                                        If TimeValue(jpulang) >= "02:00:00" And TimeValue(xjpulang) <= "05:00:00" Then
                                            xjpulang = xjpulang.AddHours(-1)
                                        Else
                                            xjpulang = xjpulang.AddHours(-5)
                                        End If

                                        inout_rubah(userid_inout, tanggal_kalen, jamhasil2, xjpulang, cn)


                                    ElseIf TimeValue(jamhasil) >= "09:35:00" And TimeValue(jamhasil) <= "18:15:00" Then

                                        Dim sqlcek_sebelum As String = String.Format("select id,kd_shift,jadwalmasuk,jadwalpulang,jammasuk,jampulang,stat from tr_hadir where tanggal='{0}' and nip='{1}' and jampulang is null and stat='HADIR'", convert_date_to_eng(tanggal_kalen), nnip)
                                        Dim cmdcek_sebelum As OleDbCommand = New OleDbCommand(sqlcek_sebelum, cn)
                                        Dim drdcek_sebelum As OleDbDataReader = cmdcek_sebelum.ExecuteReader



                                        If drdcek_sebelum.HasRows Then
                                            If drdcek_sebelum.Read Then
                                                If Integer.Parse(drdcek_sebelum(0).ToString) > 0 Then
                                                    statcek_sebelum = Integer.Parse(drdcek_sebelum(0).ToString)
                                                    kd_shift = drdcek_sebelum("kd_shift").ToString
                                                    jd_masuk = drdcek_sebelum("jadwalmasuk").ToString
                                                    jd_pulang = drdcek_sebelum("jadwalpulang").ToString
                                                    jm_masuk = drdcek_sebelum("jammasuk").ToString
                                                    st_hadir = drdcek_sebelum("stat").ToString
                                                End If
                                            End If
                                        End If
                                        drdcek_sebelum.Close()

                                        If statcek_sebelum > 0 Then

                                            Dim jamhasil2 As String = convert_datetime_to_eng(jamhasil)

                                            sqlexec = String.Format("update tr_hadir set jampulang='{0}',step=1 where id={1}", jamhasil2, statcek_sebelum)
                                            Dim cmdexec As OleDbCommand = New OleDbCommand(sqlexec, cn)
                                            cmdexec.ExecuteNonQuery()

                                            '' cek hari dulu''
                                            If nip_param.Trim.Length = 0 Then
                                                cek_hari(False, DateValue(tanggal_kalen))
                                            Else
                                                If jenislibur_hit.Trim.Length = 0 Then
                                                    cek_hari(False, DateValue(tanggal_kalen))
                                                End If
                                            End If
                                            '' end of cek hari

                                            mulai_hitung2(statcek_sebelum, kd_shift, jd_masuk, jd_pulang, jm_masuk, jamhasil2, st_hadir, cn, 0.0)

                                            inout_rubah(userid_inout, tanggal_kalen, jd_masuk, jamhasil2, cn)

                                        End If

                                    End If

                            End Select

_ke_absen_selanjutnya:
                        End While

                    Else ' kalau gak ada absen

                        If nip_param.Trim.Length = 0 Then

                            sqlexec = String.Format("insert into tr_hadir (nip,tanggal,jadwalmasuk,jadwalpulang,jammasuk,jampulang,stat,kdgol,kd_shift,depart,step,stat_hari) values ('{0}','{1}',null,null,null,null,'{2}','{3}','{4}','{5}',0,'{6}')", _
                                                                    nnip, convert_date_to_eng(tanggal_kalen), "LAIN-LAIN", kdgol, "-", depart, jenislibur_hit)
                            Dim cmdexec As OleDbCommand = New OleDbCommand(sqlexec, cn)
                            cmdexec.ExecuteNonQuery()

                        End If

                    End If

lanjut_karyawan_laen:

                    'If nip_param.Trim.Length = 0 Then ''And DateValue(tanggal_kalen) = DateValue(tanggal_kalk2) Then

                    '    Dim sqlcek As String = String.Format("select id from tr_hadir where tanggal='{0}' and nip='{1}'", tanggal_kalen, nnip)
                    '    Dim cmdcek As OleDbCommand = New OleDbCommand(sqlcek, cn)
                    '    Dim drdcek As OleDbDataReader = cmdcek.ExecuteReader

                    '    Dim ada As Boolean = False
                    '    If drdcek.Read Then
                    '        If IsNumeric(drdcek(0).ToString) Then
                    '            ada = True
                    '        End If
                    '    End If
                    '    drdcek.Close()

                    '    If ada = False Then

                    '        sqlexec = String.Format("insert into tr_hadir (nip,tanggal,jadwalmasuk,jadwalpulang,jammasuk,jampulang,stat,kdgol,kd_shift,depart,step,stat_hari) values ('{0}','{1}',null,null,null,null,'{2}','{3}','{4}','{5}',0,'{6}')", _
                    '                                            nnip, convert_date_to_eng(tanggal_kalen), "LAIN-LAIN", kdgol, "-", depart, jenislibur_hit)
                    '        Dim cmdexec As OleDbCommand = New OleDbCommand(sqlexec, cn)
                    '        cmdexec.ExecuteNonQuery()

                    '    End If
                    'End If

                Next

langsung_keluar:

                If nip_param.Trim.Length > 0 Then
                    If hasiledit_bygrid = False Then

                        If masuksebelum.Trim.Length > 0 Then
                            inout_rubah(idmesin, tanggal_kalen, masuksebelum, IIf(jd_keluarsebelum = "0:00:00", masuksebelum, jd_keluarsebelum), cn)
                        End If

                        If keluarsebelum.Trim.Length > 0 Then
                            inout_rubah(idmesin, tanggal_kalen, IIf(jd_masuksebelum = "0:00:00", keluarsebelum, jd_masuksebelum), keluarsebelum, cn)
                        End If

                    End If
                End If

            Next



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
    Private Sub mulai_hitung2(ByVal idhadir As Integer, ByVal nama_shift As String, ByVal jadwalmasuk As DateTime, ByVal jadwalpulang As DateTime, ByVal jammasuk As DateTime, ByVal jampulang As DateTime, ByVal stathadir As String, ByVal cn As OleDbConnection, ByVal tamb_lembur As Double)

        Dim stelat As Integer = 0
        Dim jmltelat As Integer = 0

        Dim selisihjam As TimeSpan = jammasuk - jadwalmasuk
        If selisihjam.Minutes > 0 Then
            stelat = 1
            jmltelat = IIf(selisihjam.Hours > 0, selisihjam.Hours / 60, 0) + selisihjam.Minutes
        Else
            jmltelat = 0
        End If

        Dim spulangcepat As Integer = 0
        Dim selisihpulang As TimeSpan = jadwalpulang - jampulang
        If selisihpulang.Minutes > 0 Then
            spulangcepat = 1
        End If

        Dim jamkerja As TimeSpan
        'Dim selisihmasuk As TimeSpan = jadwalmasuk - jammasuk
        If selisihjam.TotalMinutes <= -30 Then
            jamkerja = jampulang - jammasuk
        Else
            If selisihjam.TotalMinutes <= 60 Then
                jamkerja = jampulang - jadwalmasuk
            Else
                jamkerja = jampulang - jammasuk
            End If

        End If

        Dim realjamkerja As Integer = jamkerja.Hours
        Dim jmllembur As Integer = 0

        Dim libur_ok As Integer = 0
        Dim stat_lembur As String
        If libur_ht.Equals("LIBUR") Then
            stat_lembur = "TDK TERJADWAL"
            libur_ok = 1
        Else
            stat_lembur = "TERJADWAL"
        End If

        Dim tambyarn As Boolean = False

        Select Case Trim(depart)
            Case "YARN"

                If jenislibur_hit = "LIBUR NASIONAL" Then

                    If realjamkerja < 8 Then

                        'jmllembur = realjamkerja

                        'If nama_shift = "SHIFT-A" Then

                        '    If TimeValue(jampulang) >= "13:00:00" Then
                        '        jmllembur = jmllembur - 1
                        '    End If

                        'ElseIf nama_shift = "SHIFT-B" Then

                        '    If TimeValue(jampulang) >= "04:00:00" Then
                        '        jmllembur = jmllembur - 1
                        '    End If

                        'End If

                    Else
                        'jmllembur = realjamkerja - 2
                        tambyarn = True
                    End If

                    'stat_lembur = "TDK TERJADWAL"
                    'libur_ok = 1
                Else

                    If realjamkerja < 8 Then
                        'jmllembur = 0
                    Else
                        jmllembur = realjamkerja - 8
                        tambyarn = True
                    End If

                    'If jmllembur > 0 Then
                    '    stat_lembur = "TERJADWAL"
                    'Else
                    '    stat_lembur = "-"
                    'End If

                    'libur_ok = 0

                End If

                GoTo masuk_perhitungan_normal


            Case "PRODUKSI"

                If jenisgaji.Equals("Bulanan") Then
                    If nama_hari_ht.Equals("SABTU") Then

                        If realjamkerja < 6 Then

                            If jenislibur_hit = "LIBUR" Or jenislibur_hit = "LIBUR NASIONAL" Then

                                jmllembur = realjamkerja

                                If nama_shift = "SHIFT-A" Then

                                    If TimeValue(jampulang) >= "13:00:00" Then
                                        jmllembur = jmllembur - 1
                                    End If

                                ElseIf nama_shift = "SHIFT-B" Then

                                    If TimeValue(jampulang) >= "04:00:00" Then
                                        jmllembur = jmllembur - 1
                                    End If

                                End If

                                stat_lembur = "TDK TERJADWAL"
                                libur_ok = 1

                            Else

                                jmllembur = 0
                                stat_lembur = "-"
                                libur_ok = 0

                            End If

                        Else

                            If jenislibur_hit = "LIBUR" Or jenislibur_hit = "LIBUR NASIONAL" Then

                                jmllembur = realjamkerja

                                If nama_shift = "SHIFT-A" Then

                                    If TimeValue(jampulang) >= "13:00:00" Then
                                        jmllembur = jmllembur - 1
                                    End If

                                ElseIf nama_shift = "SHIFT-B" Then

                                    If TimeValue(jampulang) >= "04:00:00" Then
                                        jmllembur = jmllembur - 1
                                    End If

                                End If

                                stat_lembur = "TDK TERJADWAL"
                                libur_ok = 1

                            Else
                                jmllembur = realjamkerja - 6

                                If jmllembur > 0 Then
                                    stat_lembur = "TERJADWAL"
                                Else
                                    stat_lembur = "-"
                                End If

                                libur_ok = 0
                            End If

                        End If


                    Else

                        If realjamkerja < 8 Then

                            If jenislibur_hit = "LIBUR" Or jenislibur_hit = "LIBUR NASIONAL" Then

                                jmllembur = realjamkerja

                                If nama_shift = "SHIFT-A" Then

                                    If TimeValue(jampulang) >= "13:00:00" Then
                                        jmllembur = jmllembur - 1
                                    End If

                                ElseIf nama_shift = "SHIFT-B" Then

                                    If TimeValue(jampulang) >= "04:00:00" Then
                                        jmllembur = jmllembur - 1
                                    End If

                                End If

                                stat_lembur = "TDK TERJADWAL"
                                libur_ok = 1

                            Else

                                jmllembur = 0
                                stat_lembur = "-"
                                libur_ok = 0

                            End If

                        Else


                            If jenislibur_hit = "LIBUR" Or jenislibur_hit = "LIBUR NASIONAL" Then

                                jmllembur = realjamkerja

                                If nama_shift = "SHIFT-A" Then

                                    If TimeValue(jampulang) >= "13:00:00" Then
                                        jmllembur = jmllembur - 1
                                    End If

                                ElseIf nama_shift = "SHIFT-B" Then

                                    If TimeValue(jampulang) >= "04:00:00" Then
                                        jmllembur = jmllembur - 1
                                    End If

                                End If

                                stat_lembur = "TDK TERJADWAL"
                                libur_ok = 1

                            Else

                                jmllembur = realjamkerja - 8

                                If jmllembur > 0 Then
                                    stat_lembur = "TERJADWAL"
                                Else
                                    stat_lembur = "-"
                                End If

                                libur_ok = 0

                            End If
                        End If


                    End If

                Else

                    '' kalau mingguan

                    If realjamkerja < 8 Then

                        If jenislibur_hit = "LIBUR" Or jenislibur_hit = "LIBUR NASIONAL" Then

                            jmllembur = realjamkerja

                            If nama_shift = "SHIFT-A" Then

                                If TimeValue(jampulang) >= "13:00:00" Then
                                    jmllembur = jmllembur - 1
                                End If

                            ElseIf nama_shift = "SHIFT-B" Then

                                If TimeValue(jampulang) >= "04:00:00" Then
                                    jmllembur = jmllembur - 1
                                End If

                            End If

                            stat_lembur = "TDK TERJADWAL"
                            libur_ok = 1

                        Else

                            jmllembur = 0
                            stat_lembur = "-"
                            libur_ok = 0

                        End If

                    Else


                        If jenislibur_hit = "LIBUR" Or jenislibur_hit = "LIBUR NASIONAL" Then

                            jmllembur = realjamkerja

                            If nama_shift = "SHIFT-A" Then

                                If TimeValue(jampulang) >= "13:00:00" Then
                                    jmllembur = jmllembur - 1
                                End If

                            ElseIf nama_shift = "SHIFT-B" Then

                                If TimeValue(jampulang) >= "04:00:00" Then
                                    jmllembur = jmllembur - 1
                                End If

                            End If

                            stat_lembur = "TDK TERJADWAL"
                            libur_ok = 1

                        Else

                            jmllembur = realjamkerja - 8

                            If jmllembur > 0 Then
                                stat_lembur = "TERJADWAL"
                            Else
                                stat_lembur = "-"
                            End If

                            libur_ok = 0

                        End If
                    End If

                End If

                GoTo lompati_perhitungan_normal

            Case "LOOM", "LOOM BORONGAN", "CUTTING", "PRINTING", "SECURITY", "MAINTENCE", "QA / QC", "CUTTING JUMBO", "METAL DETECTOR", "BORONGAN SCRUB", "INNER BLOWING", "BORONGAN JUMBO BAG"

                If cb_gol.EditValue = "BRNG KHS" Then
                    jmllembur = 0
                    libur_ok = 0
                    stat_lembur = "-"

                    GoTo lompati_perhitungan_normal

                Else

                    GoTo masuk_perhitungan_normal

                End If

            Case "JAIT JUMBO BAG", "QC JUMBO BAG"

                If cb_gol.EditValue = "BRNG KHS" Then
                    jmllembur = 0
                    libur_ok = 0
                    stat_lembur = "-"

                    GoTo lompati_perhitungan_normal

                Else

                    GoTo masuk_perhitungan_normal

                End If

            Case "WAREHOUSE", "SETTING INER", "CUTTING JUMBO", "PAKING", "QC INNER  SETTING"

                If cb_gol.EditValue = "BRNG KHS" Then
                    jmllembur = 0
                    libur_ok = 0
                    stat_lembur = "-"

                    GoTo lompati_perhitungan_normal
                Else

                    GoTo masuk_perhitungan_normal

                End If

            Case "STAFF"

                GoTo masuk_perhitungan_normal

        End Select

masuk_perhitungan_normal:

        '' perhitungan lembur dengan perhitungan normal
        If jenisgaji.Equals("Bulanan") Then
            If nama_hari_ht.Equals("SABTU") Then

                If realjamkerja < 6 Then
                    If jenislibur_hit = "LIBUR" Or jenislibur_hit = "LIBUR NASIONAL" Then

                        jmllembur = realjamkerja

                        If nama_shift = "SHIFT-I" Then
                            If TimeValue(jampulang) >= "13:00:00" Then
                                jmllembur = jmllembur - 1
                            End If
                        End If

                        If nama_shift = "SHIFT-II" Then
                            If TimeValue(jampulang) >= "19:00:00" Then
                                jmllembur = jmllembur - 1
                            End If
                        End If

                        If nama_shift = "SHIFT-III" Or nama_shift = "SHIFT-B" Then
                            If TimeValue(jampulang) >= "04:00:00" Then
                                jmllembur = jmllembur - 1
                            End If
                        End If

                        stat_lembur = "TDK TERJADWAL"
                        libur_ok = 1

                    Else
                        jmllembur = 0
                        stat_lembur = "-"
                        libur_ok = 0
                    End If

                Else

                    If jenislibur_hit = "LIBUR" Or jenislibur_hit = "LIBUR NASIONAL" Then

                        jmllembur = realjamkerja

                        If nama_shift = "SHIFT-I" Then
                            If TimeValue(jampulang) >= "13:00:00" Then
                                jmllembur = jmllembur - 1
                            End If
                        End If

                        If nama_shift = "SHIFT-II" Then
                            If TimeValue(jampulang) >= "19:00:00" Then
                                jmllembur = jmllembur - 1
                            End If
                        End If

                        If nama_shift = "SHIFT-III" Or nama_shift = "SHIFT-B" Then
                            If TimeValue(jampulang) >= "04:00:00" Then
                                jmllembur = jmllembur - 1
                            End If
                        End If

                        stat_lembur = "TDK TERJADWAL"
                        libur_ok = 1

                    Else
                        jmllembur = realjamkerja - 6

                        If jmllembur > 0 Then
                            stat_lembur = "TERJADWAL"
                        Else
                            stat_lembur = "-"
                        End If

                        libur_ok = 0
                    End If

                End If

            Else

                If realjamkerja < 8 Then
                    If jenislibur_hit = "LIBUR" Or jenislibur_hit = "LIBUR NASIONAL" Then

                        jmllembur = realjamkerja

                        If nama_shift = "SHIFT-I" Then
                            If TimeValue(jampulang) >= "13:00:00" Then
                                jmllembur = jmllembur - 1
                            End If
                        End If

                        If nama_shift = "SHIFT-II" Then
                            If TimeValue(jampulang) >= "19:00:00" Then
                                jmllembur = jmllembur - 1
                            End If
                        End If

                        If nama_shift = "SHIFT-III" Or nama_shift = "SHIFT-B" Then
                            If TimeValue(jampulang) >= "04:00:00" Then
                                jmllembur = jmllembur - 1
                            End If
                        End If

                        stat_lembur = "TDK TERJADWAL"
                        libur_ok = 1

                    Else
                        jmllembur = 0
                        stat_lembur = "-"
                        libur_ok = 0
                    End If

                Else

                    If jenislibur_hit = "LIBUR" Or jenislibur_hit = "LIBUR NASIONAL" Then

                        jmllembur = realjamkerja

                        If nama_shift = "SHIFT-I" Then
                            If TimeValue(jampulang) >= "13:00:00" Then
                                jmllembur = jmllembur - 1
                            End If
                        End If

                        If nama_shift = "SHIFT-II" Then
                            If TimeValue(jampulang) >= "19:00:00" Then
                                jmllembur = jmllembur - 1
                            End If
                        End If

                        If nama_shift = "SHIFT-III" Or nama_shift = "SHIFT-B" Then
                            If TimeValue(jampulang) >= "04:00:00" Then
                                jmllembur = jmllembur - 1
                            End If
                        End If

                        stat_lembur = "TDK TERJADWAL"
                        libur_ok = 1

                    Else

                        jmllembur = realjamkerja - 8

                        If nama_shift = "SHIFT-II" Then
                            If TimeValue(jampulang) >= "02:00:00" And TimeValue(jampulang) <= "09:00:00" Then
                                jmllembur = jmllembur - 1
                            End If
                        End If


                        If jmllembur > 0 Then
                            stat_lembur = "TERJADWAL"
                        Else
                            stat_lembur = "-"
                        End If

                        libur_ok = 0
                    End If

                End If

            End If

        Else
            '' KALAU MINGGUAN

            If realjamkerja < 8 Then
                If jenislibur_hit = "LIBUR" Or jenislibur_hit = "LIBUR NASIONAL" Then

                    jmllembur = realjamkerja

                    If nama_shift = "SHIFT-I" Then
                        If TimeValue(jampulang) >= "13:00:00" Then
                            jmllembur = jmllembur - 1
                        End If
                    End If

                    If nama_shift = "SHIFT-II" Then
                        If TimeValue(jampulang) >= "19:00:00" Then
                            jmllembur = jmllembur - 1
                        End If
                    End If

                    If nama_shift = "SHIFT-III" Or nama_shift = "SHIFT-B" Then
                        If TimeValue(jampulang) >= "04:00:00" Then
                            jmllembur = jmllembur - 1
                        End If
                    End If

                    stat_lembur = "TDK TERJADWAL"
                    libur_ok = 1

                Else
                    jmllembur = 0
                    stat_lembur = "-"
                    libur_ok = 0
                End If

            Else

                If jenislibur_hit = "LIBUR" Or jenislibur_hit = "LIBUR NASIONAL" Then

                    jmllembur = realjamkerja

                    If nama_shift = "SHIFT-I" Then
                        If TimeValue(jampulang) >= "13:00:00" Then
                            jmllembur = jmllembur - 1
                        End If
                    End If

                    If nama_shift = "SHIFT-II" Then
                        If TimeValue(jampulang) >= "19:00:00" Then
                            jmllembur = jmllembur - 1
                        End If
                    End If

                    If nama_shift = "SHIFT-III" Or nama_shift = "SHIFT-B" Then
                        If TimeValue(jampulang) >= "04:00:00" Then
                            jmllembur = jmllembur - 1
                        End If
                    End If

                    stat_lembur = "TDK TERJADWAL"
                    libur_ok = 1

                Else

                    
                    jmllembur = realjamkerja - 8

                    If nama_shift = "SHIFT-II" Then
                        If TimeValue(jampulang) >= "02:00:00" And TimeValue(jampulang) <= "09:00:00" Then
                            jmllembur = jmllembur - 1
                        End If
                    End If

                    'If nama_shift = "SHIFT-I" Then
                    '    If TimeValue(jampulang) >= "13:00:00" Then
                    '        jmllembur = jmllembur - 1
                    '    End If
                    'ElseIf nama_shift = "SHIFT-II" Then
                    '    If TimeValue(jampulang) >= "19:00:00" Then
                    '        jmllembur = jmllembur - 1
                    '    ElseIf TimeValue(jampulang) >= "02:00:00" And TimeValue(jampulang) <= "09:00:00" Then
                    '        jmllembur = jmllembur - 1
                    '    End If
                    'ElseIf nama_shift = "SHIFT-III" Or nama_shift = "SHIFT-B" Then
                    '    If TimeValue(jampulang) >= "04:00:00" Then
                    '        jmllembur = jmllembur - 1
                    '    End If
                    'End If

                    If jmllembur > 0 Then
                        stat_lembur = "TERJADWAL"
                    Else
                        stat_lembur = "-"
                    End If

                    libur_ok = 0
                End If

            End If

        End If
        '' akhir perhitungan lembur dengan perhitungan normal

lompati_perhitungan_normal:


        If jmllembur > 0 Then
            jmllembur = jmllembur * 60
        Else
            jmllembur = 0
        End If

        '' tambahan lembur
        If tamb_lembur > 0.0 Then
            jmllembur = jmllembur + (tamb_lembur * 60)
            tamb_lembur = tamb_lembur * 60
        ElseIf tamb_lembur < 0.0 Then
            jmllembur = jmllembur + (tamb_lembur * 60)
            tamb_lembur = tamb_lembur * 60
        Else
            jmllembur = jmllembur + tamb_lembur
        End If


        '' uang makan dan tambahan
        Dim uangmakan_ As Double
        If stathadir.Equals("HADIR") Then

            If libur_ok = 0 Then
                uangmakan_ = tuangmakan
            Else
                uangmakan_ = 0
            End If

        ElseIf stathadir.Equals("MENGINAP") Then
            uangmakan_ = tuangmakan_inap
        End If

        Dim tambmakan As Double = 0
        If (jenislembur.Equals("Depnaker") Or jenislembur.Equals("Jam Mati")) And jenisgaji.Equals("Bulanan") Then
            If (jmllembur / 60) >= 3 Then

                If libur_ok = 0 Then
                    tambmakan = tamb_makan
                End If

            Else
                tambmakan = 0
            End If
        Else
            tambmakan = 0
        End If
        ' akhir uang makan


        '' rubah jika tidak ada lembur
        If jmllembur = 0 Then
            stat_lembur = "-"
        End If
        '' akhir jika tidak ada lembur

        Dim jam1 As Double = 0.0
        Dim jam2 As Double = 0.0
        Dim jam3 As Double = 0.0
        Dim jam4 As Double = 0.0

        If jmllembur <> 0 Then

            If jenislembur.Equals("Depnaker") Then

                jam1 = 1.5
                jam2 = ((jmllembur / 60) - 1) * 2
                If libur_ok = 1 Then
                    jam1 = 0
                    jam2 = ((jmllembur / 60)) * 2
                End If

                jam3 = 0
                jam4 = 0

            ElseIf jenislembur.Equals("Jam Mati") Then
                jam1 = 0

                If libur_ok = 1 Then

                    If jenishitung_lembur = 1 Then
                        jam2 = (jmllembur / 60) * 2
                    End If

                Else
                    jam2 = (jmllembur / 60) * 1
                End If

                jam3 = 0
                jam4 = 0

            End If

        End If

        Dim jmllembur_rp As Double = 0

        If jam1 + jam2 + jam3 + jam4 = 0 Then
            jmllembur_rp = 0
        Else

            If (jam1 + jam2 + jam3 + jam4) = 1 Then
                jmllembur_rp = Math.Truncate(lembur_perjam) * (jam1 + jam2 + jam3 + jam4)
            Else
                jmllembur_rp = lembur_perjam * (jam1 + jam2 + jam3 + jam4)
            End If

        End If

        Dim tambahan_istirahat As Double = 0
        If tambyarn = True Then
            'tambahan_istirahat = Math.Truncate(lembur_perjam / 2)
        End If

        If jmllembur_rp < 1 Then
            jmllembur_rp = 0
        End If

        Dim jharian As Integer = 0

        If sharian = 1 Then

            If libur_ok = 1 Then
                jharian = 0
            Else

                Dim realjamkerja2 As Integer = realjamkerja
                If nama_shift = "SHIFT I" Or nama_shift = "SHIFT-A" Then
                    If TimeValue(jampulang) >= "13:00:00" Then
                        realjamkerja2 = realjamkerja2 - 1
                    End If
                ElseIf nama_shift = "SHIFT II" Then
                    If TimeValue(jampulang) >= "19:00:00" Then
                        realjamkerja = realjamkerja2 - 1
                    End If
                ElseIf nama_shift = "SHIFT III" Or nama_shift = "SHIFT-B" Then
                    If TimeValue(jampulang) >= "04:00:00" Then
                        realjamkerja = realjamkerja2 - 1
                    End If
                End If

                If jniskelamin.Equals("Perempuan") Then

                    If gapok = 0 Then

                        If realjamkerja2 < 6 Then
                            jharian = (perempuan_uang / 7) * realjamkerja2 * 1
                        Else
                            jharian = perempuan_uang * 1
                        End If

                    Else

                        If realjamkerja2 < 6 Then
                            jharian = (gapok / 7) * realjamkerja2 * 1
                        Else
                            jharian = gapok * 1
                        End If

                    End If

                Else

                    If gapok = 0 Then

                        If realjamkerja2 < 6 Then
                            jharian = (laki1_uang / 7) * realjamkerja2 * 1
                        Else
                            jharian = laki1_uang * 1
                        End If

                    Else

                        If realjamkerja2 < 6 Then
                            jharian = (gapok / 7) * realjamkerja2 * 1
                        Else
                            jharian = gapok * 1
                        End If

                    End If

                End If
            End If

        Else
            jharian = 0

            If jniskelamin.Equals("Perempuan") Then

                If gapok = 0 Then

                    If jenishitung_lembur = 3 Then
                        jharian = perempuan_uang * 1
                    End If

                Else

                    If jenishitung_lembur = 3 Then
                        jharian = gapok
                    End If

                End If

            Else

                If gapok = 0 Then

                    If jenishitung_lembur = 3 Then
                        jharian = laki1_uang * 1
                    End If

                Else

                    If jenishitung_lembur = 3 Then
                        jharian = gapok
                    End If

                End If

            End If

        End If



        Dim sqlup As String = String.Format("update tr_hadir set stelat={0},spulangcpat={1},jmltelat={2},skalk=1,jamkerja={3},jamlembur={4},lemburperjam={5},jam1={6},jam2={7},jam3={8},jam4={9},tambmakan={10},jharian={11},jnisabsen=1,uangmakan={12},stat_lmbr='{13}',tamblembur={14},stat_hari='{15}',tamb_istirahat={16},step=2 where id={17}", _
                                            stelat, spulangcepat, jmltelat, jamkerja.Hours, Replace(jmllembur, ",", "."), Replace(jmllembur_rp, ",", "."), Replace(jam1, ",", "."), Replace(jam2, ",", "."), Replace(jam3, ",", "."), Replace(jam4, ",", "."), Replace(tambmakan, ",", "."), _
                                            Replace(jharian, ",", "."), Replace(uangmakan_, ",", "."), stat_lembur, Replace(tamb_lembur, ",", "."), jenislibur_hit, Replace(tambahan_istirahat, ",", "."), idhadir)

        Using cmdins As OleDbCommand = New OleDbCommand(sqlup, cn)
            cmdins.ExecuteNonQuery()
        End Using

    End Sub


    Private Sub mulai_kalk(ByVal id_param As Integer, ByVal nip_param As String, ByVal jam1_param As DateTime, _
                           ByVal jam2_param As DateTime, ByVal skaktothasil As Boolean, _
                           ByVal tanggal_kalk As Date, ByVal tanggal_kalk2 As Date, ByVal shift_param As String)

        realmasuk = "12/12/2007 11:11:11"
        realkeluar = "12/12/2007 11:11:11"

        jadwalmasuk = "12/12/2007 11:11:11"
        jadwalmasuk_awl = "12/12/2007 11:11:11"
        jadwalmasuk_akh = "12/12/2007 11:11:11"
        jadwalmasuk_tol = "12/12/2007 11:11:11"
        jadwalkeluar = "12/12/2007 11:11:11"
        jadwalkeluar_awl = "12/12/2007 11:11:11"
        jadwalkeluar_akh = "12/12/2007 11:11:11"
        jadwalkeluar_tol = "12/12/2007 11:11:11"

        Dim sql As String = "select ms_karyawan.idmesin,ms_karyawan.nip,ms_karyawan.nama, " & _
        "ms_karyawan.gapok, ms_karyawan.tunj_jabatan, ms_karyawan.tunj_kehadiran, ms_karyawan.tunj_makan,ms_karyawan.tunj_makan_inap, " & _
        "ms_karyawan.tamb_makanlembur, ms_karyawan.[standby],ms_karyawan.depart, " & _
        "ms_golongan.jnisgol,ms_golongan.jenislembur,ms_golongan.harian,ms_golongan.laki2,ms_golongan.perempuan,ms_karyawan.liburnormal,ms_golongan.jenisgaji,ms_karyawan.jniskelamin " & _
        "from ms_karyawan inner join ms_golongan on ms_karyawan.kdgol=ms_golongan.kode " & _
        "where ms_karyawan.aktif=1 and ms_golongan.saktif=1 and ms_karyawan.shiden=0"

        If Not cb_gol.EditValue = "ALL" Then
            sql = String.Format("{0} and ms_karyawan.kdgol='{1}'", sql, cb_gol.EditValue)
        End If

        If Not cb_depart.EditValue = "ALL" Then
            sql = String.Format("{0} and ms_karyawan.depart='{1}'", sql, cb_depart.EditValue)
        End If

        If Not cbpeg.EditValue = "ALL" Then
            sql = String.Format("{0} and ms_karyawan.nip='{1}'", sql, cbpeg.EditValue)
        End If

        If nip_param.Trim.Length > 0 Then
            sql = String.Format("{0} and ms_karyawan.nip='{1}'", sql, nip_param)
        End If

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqlawal As String = "select * from ms_awalshift"
            Dim cmdawal As OleDbCommand = New OleDbCommand(sqlawal, cn)
            Dim drdawal As OleDbDataReader = cmdawal.ExecuteReader
            If drdawal.Read Then
                awalshiftoto = drdawal("namahari").ToString.ToUpper
                jenishitung_lembur = Integer.Parse(drdawal("jnis_hit_harian").ToString)
            End If
            drdawal.Close()


            '' loop hari
            Dim sqlhari As String = String.Format("select tanggal from ms_kalender where tanggal>='{0}' and tanggal<='{1}'", convert_date_to_eng(tanggal_kalk), convert_date_to_eng(tanggal_kalk2))
            Dim cmdhari As OleDbCommand = New OleDbCommand(sqlhari, cn)
            Dim drdhari As OleDbDataReader = cmdhari.ExecuteReader
            Dim dthari As DataTable = New DataTable
            dthari.Load(drdhari)

            If dthari.Rows.Count <= 0 Then
                MsgBox("Kalender kerja harus diisi dulu..", vbOKOnly + vbInformation, "Informasi")
                Return
            End If

            Dim cmd1 As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd1 As OleDbDataReader = cmd1.ExecuteReader
            Dim dtkaryawan As DataTable = New DataTable
            dtkaryawan.Load(drd1)

            For i As Integer = 0 To dthari.Rows.Count - 1

                Dim tanggal_kalen As Date = dthari(i)("tanggal").ToString

                '' balikin ms_inout

                If jam1_param = Nothing And DateValue(tanggal_kalen) = DateValue(tanggal_kalk) Then
                    putar_inout2_(cn, tanggal_kalen, tanggal_kalk2, jam1_param, jam2_param, cb_gol.EditValue, cb_depart.EditValue, cbpeg.EditValue)
                End If

                '' akhir balikin ms inout

                '' cek hari dulu''

                cek_hari(False, tanggal_kalen)

                '' end of cek hari


                For n As Integer = 0 To dtkaryawan.Rows.Count - 1

                    SetWaitDialog(String.Format("{0}-{1}", convert_date_to_ind(tanggal_kalen), dtkaryawan(n)("nama").ToString))

                    idmesin = Integer.Parse(dtkaryawan(n)("idmesin").ToString)
                    nnip = dtkaryawan(n)("nip").ToString

                    nnip_sebelum = ""

                    gapok = Double.Parse(dtkaryawan(n)("gapok").ToString)
                    tunj_jabatan = Double.Parse(dtkaryawan(n)("tunj_jabatan").ToString)
                    tunj_kehadiran = Double.Parse(dtkaryawan(n)("tunj_kehadiran").ToString)
                    tuangmakan = Double.Parse(dtkaryawan(n)("tunj_makan").ToString)
                    tuangmakan_inap = Double.Parse(dtkaryawan(n)("tunj_makan_inap").ToString)
                    tamb_makan = Double.Parse(dtkaryawan(n)("tamb_makanlembur").ToString)
                    standby_uang = Double.Parse(dtkaryawan(n)("standby").ToString)

                    jenisgol = dtkaryawan(n)("jnisgol").ToString
                    jenislembur = dtkaryawan(n)("jenislembur").ToString

                    sharian = Integer.Parse(dtkaryawan(n)("harian").ToString)
                    laki1_uang = Double.Parse(dtkaryawan(n)("laki2").ToString)
                    perempuan_uang = Double.Parse(dtkaryawan(n)("perempuan").ToString)

                    liburnormal = Integer.Parse(dtkaryawan(n)("liburnormal").ToString)

                    depart = dtkaryawan(n)("depart").ToString
                    jenisgaji = dtkaryawan(n)("jenisgaji").ToString
                    jniskelamin = dtkaryawan(n)("jniskelamin").ToString

                    Dim nilharian As Double
                    If jniskelamin.Equals("Laki - Laki") Then
                        nilharian = laki1_uang
                    Else
                        nilharian = perempuan_uang
                    End If

                    lembur_perjam = lembur_perjam_cek(sharian, jenislembur, gapok, jenishitung_lembur, tunj_jabatan, depart, nilharian)

                    Dim adashift As Boolean = False
                    Dim shiftok As Boolean = False

                    '' kalau nip parameter ada langsung lompat aja ke yang otomatis cari jam
                    If nip_param.Length > 0 And Not (jam1_param = Nothing) Then GoTo langsung_kalau_nip_param_ada
                    '' end of nip

                    '' cek dulu di ms_shift

                    Dim sqlshift As String = String.Format("select ms_karyawan4.hit_jam,ms_karyawan4.kd_jam,ms_karyawan4.tgl_mulai,ms_karyawan4.tgl_selesai,ms_jamkerja.kode as namajamkerja, " & _
                    "LEFT (CONVERT (VARCHAR, CONVERT (time(0), ms_jamkerja.awalmasuk), 108), 5) as awalmasuk, " & _
                    "LEFT (CONVERT (VARCHAR, CONVERT (time(0), ms_jamkerja.akhirmasuk), 108), 5) as akhirmasuk, " & _
                    "LEFT (CONVERT (VARCHAR, CONVERT (time(0), ms_jamkerja.jammasuk), 108), 5) as jammasuk, " & _
                    "LEFT (CONVERT (VARCHAR, CONVERT (time(0), ms_jamkerja.tolmasuk), 108), 5) as tolmasuk, " & _
                    "LEFT (CONVERT (VARCHAR, CONVERT (time(0), ms_jamkerja.awalkeluar), 108), 5) as awalkeluar, " & _
                    "LEFT (CONVERT (VARCHAR, CONVERT (time(0), ms_jamkerja.akhirkeluar), 108), 5) as akhirkeluar, " & _
                    "LEFT (CONVERT (VARCHAR, CONVERT (time(0), ms_jamkerja.jampulang), 108), 5) as jamkeluar, " & _
                    "LEFT (CONVERT (VARCHAR, CONVERT (time(0), ms_jamkerja.tolpulang), 108), 5) as tolkeluar " & _
                    "from ms_karyawan4 " & _
                    "inner join ms_jamkerja on ms_karyawan4.kd_jam=ms_jamkerja.kode " & _
                    "where ms_karyawan4.nip='{0}' " & _
                    "and ms_karyawan4.tgl_mulai<='{1}' and ms_karyawan4.tgl_selesai>='{1}' " & _
                    "order by ms_jamkerja.awalmasuk", nnip, convert_date_to_eng(tanggal_kalen))

                    Dim cmdshift As OleDbCommand = New OleDbCommand(sqlshift, cn)
                    Dim drdshift As OleDbDataReader = cmdshift.ExecuteReader
                    While drdshift.Read

                        realmasuk = "12/12/2007 11:11:11"
                        realkeluar = "12/12/2007 11:11:11"

                        jadwalmasuk = "12/12/2007 11:11:11"
                        jadwalmasuk_awl = "12/12/2007 11:11:11"
                        jadwalmasuk_akh = "12/12/2007 11:11:11"
                        jadwalmasuk_tol = "12/12/2007 11:11:11"
                        jadwalkeluar = "12/12/2007 11:11:11"
                        jadwalkeluar_awl = "12/12/2007 11:11:11"
                        jadwalkeluar_akh = "12/12/2007 11:11:11"
                        jadwalkeluar_tol = "12/12/2007 11:11:11"

                        '' kalau harinya sama
                        If IsNumeric(drdshift("hit_jam").ToString) Then

                            namashift = drdshift("namajamkerja").ToString
                            hitungan_jam = drdshift("hit_jam").ToString

                            If TimeValue(drdshift("awalmasuk").ToString) >= "19:00" And TimeValue(drdshift("awalmasuk").ToString) <= "23:59" And TimeValue(drdshift("jammasuk").ToString) >= "00:00" And TimeValue(drdshift("jammasuk").ToString) <= "03:00" Then
                                jadwalmasuk_awl = String.Format("{0} {1}", convert_date_to_eng(DateAdd(DateInterval.Day, -1, tanggal_kalen)), drdshift("awalmasuk").ToString)
                            ElseIf TimeValue(drdshift("awalmasuk").ToString) >= "00:00" And TimeValue(drdshift("awalmasuk").ToString) <= "04:00" And TimeValue(drdshift("jammasuk").ToString) >= "19:00" And TimeValue(drdshift("jammasuk").ToString) <= "23:59" Then
                                jadwalmasuk_awl = String.Format("{0} {1}", convert_date_to_eng(DateAdd(DateInterval.Day, 1, tanggal_kalen)), drdshift("awalmasuk").ToString)
                            Else
                                jadwalmasuk_awl = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), drdshift("awalmasuk").ToString)
                            End If

                            If TimeValue(drdshift("akhirmasuk").ToString) >= "00:00" And TimeValue(drdshift("akhirmasuk").ToString) <= "05:00" And TimeValue(drdshift("jammasuk").ToString) >= "19:00" And TimeValue(drdshift("jammasuk").ToString) <= "23:59" Then
                                jadwalmasuk_akh = String.Format("{0} {1}", convert_date_to_eng(DateAdd(DateInterval.Day, 1, tanggal_kalen)), drdshift("akhirmasuk").ToString)
                            ElseIf TimeValue(drdshift("akhirmasuk").ToString) >= "19:00" And TimeValue(drdshift("akhirmasuk").ToString) <= "23:59" And TimeValue(drdshift("jammasuk").ToString) >= "00:00" And TimeValue(drdshift("jammasuk").ToString) <= "04:00" Then
                                jadwalmasuk_akh = String.Format("{0} {1}", convert_date_to_eng(DateAdd(DateInterval.Day, -1, tanggal_kalen)), drdshift("akhirmasuk").ToString)
                            Else
                                jadwalmasuk_akh = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), drdshift("akhirmasuk").ToString)
                            End If

                            'If TimeValue(drdshift("jammasuk").ToString) >= "19:00" And TimeValue(drdshift("jammasuk").ToString) <= "23:59" And num_shift <> 1 Then
                            '    jadwalmasuk = String.Format("{0} {1}", convert_date_to_eng(DateAdd(DateInterval.Day, -1, tanggal_kalen)), drdshift("jammasuk").ToString)
                            'Else
                            jadwalmasuk = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), drdshift("jammasuk").ToString)
                            'End If

                            If TimeValue(drdshift("tolmasuk").ToString) >= "00:00" And TimeValue(drdshift("tolmasuk").ToString) <= "04:00" And TimeValue(drdshift("jammasuk").ToString) >= "19:00" And TimeValue(drdshift("jammasuk").ToString) <= "23:59" Then
                                jadwalmasuk_tol = String.Format("{0} {1}", convert_date_to_eng(DateAdd(DateInterval.Day, 1, tanggal_kalen)), drdshift("tolmasuk").ToString)
                            ElseIf TimeValue(drdshift("tolmasuk").ToString) >= "19:00" And TimeValue(drdshift("tolmasuk").ToString) <= "23:59" And TimeValue(drdshift("jammasuk").ToString) >= "00:00" And TimeValue(drdshift("jammasuk").ToString) <= "04:00" Then
                                jadwalmasuk_tol = String.Format("{0} {1}", convert_date_to_eng(DateAdd(DateInterval.Day, -1, tanggal_kalen)), drdshift("tolmasuk").ToString)
                            Else
                                jadwalmasuk_tol = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), drdshift("tolmasuk").ToString)
                            End If

                            If TimeValue(drdshift("awalkeluar").ToString) >= "19:00" And TimeValue(drdshift("awalkeluar").ToString) <= "23:59" And TimeValue(drdshift("jamkeluar").ToString) >= "00:00" And TimeValue(drdshift("jamkeluar").ToString) <= "12:00" Then
                                jadwalkeluar_awl = String.Format("{0} {1}", convert_date_to_eng(DateAdd(DateInterval.Day, -1, tanggal_kalen)), drdshift("awalkeluar").ToString)
                            ElseIf TimeValue(drdshift("awalkeluar").ToString) >= "00:00" And TimeValue(drdshift("awalkeluar").ToString) <= "07:00" Then
                                jadwalkeluar_awl = String.Format("{0} {1}", convert_date_to_eng(DateAdd(DateInterval.Day, 1, tanggal_kalen)), drdshift("awalkeluar").ToString)
                            Else
                                jadwalkeluar_awl = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), drdshift("awalkeluar").ToString)
                            End If

                            If TimeValue(drdshift("akhirkeluar").ToString) >= "00:00" And TimeValue(drdshift("akhirkeluar").ToString) <= "12:00" And TimeValue(drdshift("jammasuk").ToString) >= "15:00" And TimeValue(drdshift("jammasuk").ToString) <= "23:59" Then
                                jadwalkeluar_akh = String.Format("{0} {1}", convert_date_to_eng(DateAdd(DateInterval.Day, 1, tanggal_kalen)), drdshift("akhirkeluar").ToString)
                            Else
                                jadwalkeluar_akh = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), drdshift("akhirkeluar").ToString)
                            End If

                            If TimeValue(drdshift("jamkeluar").ToString) >= "00:00" And TimeValue(drdshift("jamkeluar").ToString) <= "12:00" And TimeValue(drdshift("jammasuk").ToString) >= "15:00" And TimeValue(drdshift("jammasuk").ToString) <= "23:59" Then
                                jadwalkeluar = String.Format("{0} {1}", convert_date_to_eng(DateAdd(DateInterval.Day, 1, tanggal_kalen)), drdshift("jamkeluar").ToString)
                            Else
                                jadwalkeluar = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), drdshift("jamkeluar").ToString)
                            End If

                            If TimeValue(drdshift("tolkeluar").ToString) >= "00:00" And TimeValue(drdshift("tolkeluar").ToString) <= "12:00" And TimeValue(drdshift("jammasuk").ToString) >= "15:00" And TimeValue(drdshift("jammasuk").ToString) <= "23:59" Then
                                jadwalkeluar_tol = String.Format("{0} {1}", convert_date_to_eng(DateAdd(DateInterval.Day, 1, tanggal_kalen)), drdshift("tolkeluar").ToString)
                            Else
                                jadwalkeluar_tol = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), drdshift("tolkeluar").ToString)
                            End If

                            '' cek awal masuk
                            Dim sqljamawal As String = String.Format("select MIN(tanggal) as tanggal from V_InOut3 where userid={0} and tanggal>='{1}' and tanggal<='{2}' ", idmesin, convert_datetime_to_eng(jadwalmasuk_awl), convert_datetime_to_eng(jadwalmasuk_akh))

                            If jam1_param = Nothing And id_param = 0 Then
                                sqljamawal = String.Format("{0} and skalk=0 and tgl_kalk is null", sqljamawal)
                            End If

                            Dim cmdjamawal As OleDbCommand = New OleDbCommand(sqljamawal, cn)
                            Dim drdjamawal As OleDbDataReader = cmdjamawal.ExecuteReader

                            If drdjamawal.Read Then
                                If drdjamawal(0).ToString.Trim.Length > 0 Then
                                    realmasuk = drdjamawal(0).ToString
                                End If
                            End If
                            drdjamawal.Close()
                            '' akhir cek awal masuk

                            '' cek akhir masuk
                            Dim sqljamakhir As String = String.Format("select distinct tanggal from V_InOut3 where userid={0} and tanggal>='{1}' and tanggal<='{2}'", idmesin, convert_datetime_to_eng(jadwalkeluar_awl), convert_datetime_to_eng(jadwalkeluar_akh))

                            'Dim sqljamakhir As String = String.Format("select distinct CONVERT(datetime, CAST(CONVERT(date, ms_inout.checktime) AS varchar) + ' ' + CAST(LEFT(CONVERT(VARCHAR, CONVERT(time(0), ms_inout.checktime), 108), 5) AS varchar)) " & _
                            '"AS tanggal from ms_inout " & _
                            '"where ms_inout.userid={0} and convert(date,ms_inout.checktime)>='{1}' and convert(date,ms_inout.checktime)<='{2}'", idmesin, convert_datetime_to_eng(jadwalkeluar_awl), convert_datetime_to_eng(jadwalkeluar_akh))

                            If jam1_param = Nothing And id_param = 0 Then
                                sqljamakhir = String.Format("{0} and skalk=0 and tgl_kalk is null", sqljamakhir)
                            End If

                            sqljamakhir = String.Format(" {0} order by tanggal", sqljamakhir)

                            Dim cmdjamakhir As OleDbCommand = New OleDbCommand(sqljamakhir, cn)
                            Dim drdjamakhir As OleDbDataReader = cmdjamakhir.ExecuteReader

                            Dim real_keluar_10 As DateTime = "12/12/2007 11:11:11"

                            While drdjamakhir.Read

                                If real_keluar_10 = "12/12/2007 11:11:11" Then
                                    realkeluar = drdjamakhir(0).ToString
                                    real_keluar_10 = DateAdd(DateInterval.Minute, 11, realkeluar)
                                Else

                                    Dim reals As DateTime = drdjamakhir(0).ToString
                                    Dim selisihminakhir As Integer = DateDiff(DateInterval.Second, reals, real_keluar_10)
                                    If selisihminakhir > 0 Then
                                        realkeluar = reals
                                    End If

                                End If

                            End While
                            drdjamakhir.Close()
                            '' akhir cek akhir masuk

                            If Not realmasuk = "12/12/2007 11:11:11" And Not realkeluar = "12/12/2007 11:11:11" Then

                                adashift = True

                                If Not (jam1_param = Nothing) Then
                                    putar_inout2_(cn, tanggal_kalk, tanggal_kalk, jam1_param, jam2_param, cb_gol.EditValue, cb_depart.EditValue, nnip)
                                End If

                                mulai_kalk2(cn, id_param, skaktothasil, tanggal_kalen)
                                shiftok = True

                                Dim sql_update_shift As String = String.Format("update ms_inout set skalk=1,tgl_kalk='{0}' where checktime>='{1}' and checktime<='{2}' and userid={3}", _
                                                                             convert_date_to_eng(tanggal_kalen), convert_datetime_to_eng(realmasuk), convert_datetime_to_eng(DateAdd(DateInterval.Second, 59, realkeluar)), idmesin)
                                Using cmd_update_shift As OleDbCommand = New OleDbCommand(sql_update_shift, cn)
                                    cmd_update_shift.ExecuteNonQuery()
                                End Using

                                If jam1_param = Nothing Then
                                    realmasuk = "12/12/2007 11:11:11"
                                    realkeluar = "12/12/2007 11:11:11"

                                    jadwalmasuk = "12/12/2007 11:11:11"
                                    jadwalmasuk_awl = "12/12/2007 11:11:11"
                                    jadwalmasuk_akh = "12/12/2007 11:11:11"
                                    jadwalmasuk_tol = "12/12/2007 11:11:11"
                                    jadwalkeluar = "12/12/2007 11:11:11"
                                    jadwalkeluar_awl = "12/12/2007 11:11:11"
                                    jadwalkeluar_akh = "12/12/2007 11:11:11"
                                    jadwalkeluar_tol = "12/12/2007 11:11:11"
                                End If

                            End If

                        End If
                        '' akhir kalau harinya sama

                    End While
                    drdshift.Close()

                    '' akhir cek ms_shift

                    ' -------------------------------------------------------------------------------

                    '' kalau bukan shift

                    Dim otomat_shift As Boolean = False

                    If adashift = False Then

langsung_kalau_nip_param_ada:

                        realmasuk = "12/12/2007 11:11:11"
                        realkeluar = "12/12/2007 11:11:11"

                        jadwalmasuk = "12/12/2007 11:11:11"
                        jadwalmasuk_awl = "12/12/2007 11:11:11"
                        jadwalmasuk_akh = "12/12/2007 11:11:11"
                        jadwalmasuk_tol = "12/12/2007 11:11:11"
                        jadwalkeluar = "12/12/2007 11:11:11"
                        jadwalkeluar_awl = "12/12/2007 11:11:11"
                        jadwalkeluar_akh = "12/12/2007 11:11:11"
                        jadwalkeluar_tol = "12/12/2007 11:11:11"

                        hitungan_jam = 0

                        Dim sqlmesin As String = String.Format("select distinct tanggal from V_InOut3 where userid={0}", idmesin)

                        If nip_param.Length = 0 Then
                            sqlmesin = String.Format(" {0} and skalk=0 and tgl_kalk is null and  tanggal>='{1} 00:00:00' and tanggal<='{2} 12:00:00'", sqlmesin, convert_date_to_eng(tanggal_kalen), convert_date_to_eng(DateAdd(DateInterval.Day, 1, tanggal_kalen)))
                        ElseIf nip_param.Length > 0 And jam1_param = Nothing Then
                            sqlmesin = String.Format(" {0} and skalk=0 and tgl_kalk is null and tanggal>='{1} 00:00:00' and tanggal<='{2} 12:00:00'", sqlmesin, convert_date_to_eng(tanggal_kalen), convert_date_to_eng(DateAdd(DateInterval.Day, 1, tanggal_kalen)))
                        Else
                            sqlmesin = String.Format(" {0} and tanggal>='{1}' and tanggal<='{2}'", sqlmesin, convert_datetime_to_eng(jam1_param), convert_datetime_to_eng(jam2_param))
                        End If

                        sqlmesin = String.Format("{0} order by tanggal", sqlmesin)

                        Dim cmdmesin As OleDbCommand = New OleDbCommand(sqlmesin, cn)
                        Dim drdmesin As OleDbDataReader = cmdmesin.ExecuteReader

                        Dim masukawal_a As Boolean = False
                        Dim realkeluar_awl As DateTime = "12/12/2007 11:11:11"


                        While drdmesin.Read

                            Dim masuksementara As DateTime = drdmesin("tanggal").ToString
                            masukawal_a = False

                            '' kalau dia 2x absen

                            If Not realmasuk = "12/12/2007 11:11:11" And Not realkeluar = "12/12/2007 11:11:11" Then
                                If masuksementara >= realkeluar_awl And realkeluar_awl >= jadwalkeluar_awl Then

                                    If Not (jam1_param = Nothing) Then
                                        putar_inout2_(cn, tanggal_kalen, tanggal_kalen, jam1_param, jam2_param, cb_gol.EditValue, cb_depart.EditValue, nnip)
                                    End If

                                    mulai_kalk2(cn, id_param, skaktothasil, tanggal_kalen)

                                    otomat_shift = True

                                    Dim sqlup_msShift As String = String.Format("update ms_inout set skalk=1,tgl_kalk='{0}' where checktime>='{1}' and checktime<='{2}' and userid={3}", _
                                                                              convert_date_to_eng(tanggal_kalen), convert_datetime_to_eng(realmasuk), convert_datetime_to_eng(DateAdd(DateInterval.Second, 59, realkeluar)), idmesin)
                                    Using cmdup_msShift As OleDbCommand = New OleDbCommand(sqlup_msShift, cn)
                                        cmdup_msShift.ExecuteNonQuery()
                                    End Using

                                    If jam1_param = Nothing Then
                                        realmasuk = "12/12/2007 11:11:11"
                                        realkeluar = "12/12/2007 11:11:11"

                                        realkeluar_awl = "12/12/2007 11:11:11"

                                        jadwalmasuk = "12/12/2007 11:11:11"
                                        jadwalmasuk_awl = "12/12/2007 11:11:11"
                                        jadwalmasuk_akh = "12/12/2007 11:11:11"
                                        jadwalmasuk_tol = "12/12/2007 11:11:11"
                                        jadwalkeluar = "12/12/2007 11:11:11"
                                        jadwalkeluar_awl = "12/12/2007 11:11:11"
                                        jadwalkeluar_akh = "12/12/2007 11:11:11"
                                        jadwalkeluar_tol = "12/12/2007 11:11:11"
                                    End If

                                End If
                            End If
                            '' akhir kalau 2x absen

                            '' apakah benar awal ?

                            Dim sqlcek_a As String = String.Format("SELECT ms_jamkerja.kode as namajamkerja,LEFT (CONVERT (VARCHAR, CONVERT (time(0), ms_jamkerja.awalmasuk), 108), 5) as awalmasuk, " & _
                            "LEFT (CONVERT (VARCHAR, CONVERT (time(0), ms_jamkerja.akhirmasuk), 108), 5) as akhirmasuk, " & _
                            "LEFT (CONVERT (VARCHAR, CONVERT (time(0), ms_jamkerja.jammasuk), 108), 5) as jammasuk, " & _
                            "LEFT (CONVERT (VARCHAR, CONVERT (time(0), ms_jamkerja.tolmasuk), 108), 5) as tolmasuk, " & _
                            "LEFT (CONVERT (VARCHAR, CONVERT (time(0), ms_jamkerja.awalkeluar), 108), 5) as awalkeluar, " & _
                            "LEFT (CONVERT (VARCHAR, CONVERT (time(0), ms_jamkerja.akhirkeluar), 108), 5) as akhirkeluar, " & _
                            "LEFT (CONVERT (VARCHAR, CONVERT (time(0), ms_jamkerja.jampulang), 108), 5) as jamkeluar, " & _
                            "LEFT (CONVERT (VARCHAR, CONVERT (time(0), ms_jamkerja.tolpulang), 108), 5) as tolkeluar " & _
                            "from ms_jamkerja where " & _
                            "LEFT (CONVERT (VARCHAR, CONVERT (time(0), ms_jamkerja.awalmasuk), 108), 5)<=convert(time,'{0}') and " & _
                            "LEFT (CONVERT (VARCHAR, CONVERT (time(0), ms_jamkerja.akhirmasuk), 108), 5) >=convert(time,'{0}') and " & _
                            "ms_jamkerja.kode in (select kodejam from ms_karyawan3 where nip='{1}')", convert_datetime_to_eng(masuksementara), nnip)

                            If Not jam1_param = Nothing Then
                                sqlcek_a = String.Format(" {0} and ms_jamkerja.kode='{1}'", sqlcek_a, shift_param)
                            End If

                            If jam1_param = Nothing Then

                                sqlcek_a = String.Format(" {0} and ms_jamkerja.kode not in (select kd_jam from ms_karyawan4 where nip='{1}' and tgl_mulai>='{2}' and tgl_selesai<='{2}')", sqlcek_a, nnip, convert_date_to_eng(tanggal_kalen))

                            End If

                            sqlcek_a = String.Format("{0} order by awalmasuk", sqlcek_a)

                            Dim cmdcek_a As OleDbCommand = New OleDbCommand(sqlcek_a, cn)
                            Dim drdcek_a As OleDbDataReader = cmdcek_a.ExecuteReader
                            If drdcek_a.Read Then

                                If jadwalmasuk_awl = "12/12/2007 11:11:11" Then

                                    masukawal_a = True

                                    realmasuk = masuksementara

                                    namashift = drdcek_a("namajamkerja").ToString

                                    If TimeValue(drdcek_a("awalmasuk").ToString) >= "19:00" And TimeValue(drdcek_a("awalmasuk").ToString) <= "23:59" And TimeValue(drdcek_a("jammasuk").ToString) >= "00:00" And TimeValue(drdcek_a("jammasuk").ToString) <= "03:00" Then
                                        jadwalmasuk_awl = String.Format("{0} {1}", convert_date_to_eng(DateAdd(DateInterval.Day, -1, tanggal_kalen)), drdcek_a("awalmasuk").ToString)
                                    ElseIf TimeValue(drdcek_a("awalmasuk").ToString) >= "00:00" And TimeValue(drdcek_a("awalmasuk").ToString) <= "04:00" And TimeValue(drdcek_a("jammasuk").ToString) >= "19:00" And TimeValue(drdcek_a("jammasuk").ToString) <= "23:59" Then
                                        jadwalmasuk_awl = String.Format("{0} {1}", convert_date_to_eng(DateAdd(DateInterval.Day, 1, tanggal_kalen)), drdcek_a("awalmasuk").ToString)
                                    Else
                                        jadwalmasuk_awl = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), drdcek_a("awalmasuk").ToString)
                                    End If

                                    If TimeValue(drdcek_a("akhirmasuk").ToString) >= "00:00" And TimeValue(drdcek_a("akhirmasuk").ToString) <= "05:00" And TimeValue(drdcek_a("jammasuk").ToString) >= "19:00" And TimeValue(drdcek_a("jammasuk").ToString) <= "23:59" Then
                                        jadwalmasuk_akh = String.Format("{0} {1}", convert_date_to_eng(DateAdd(DateInterval.Day, 1, tanggal_kalen)), drdcek_a("akhirmasuk").ToString)
                                    ElseIf TimeValue(drdcek_a("akhirmasuk").ToString) >= "19:00" And TimeValue(drdcek_a("akhirmasuk").ToString) <= "23:59" And TimeValue(drdcek_a("jammasuk").ToString) >= "00:00" And TimeValue(drdcek_a("jammasuk").ToString) <= "04:00" Then
                                        jadwalmasuk_akh = String.Format("{0} {1}", convert_date_to_eng(DateAdd(DateInterval.Day, -1, tanggal_kalen)), drdcek_a("akhirmasuk").ToString)
                                    Else
                                        jadwalmasuk_akh = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), drdcek_a("akhirmasuk").ToString)
                                    End If

                                    'If TimeValue(drdcek_a("jammasuk").ToString) >= "19:00" And TimeValue(drdcek_a("jammasuk").ToString) <= "23:59" and  Then
                                    '    jadwalmasuk = String.Format("{0} {1}", convert_date_to_eng(DateAdd(DateInterval.Day, -1, ttgl.EditValue)), drdcek_a("jammasuk").ToString)
                                    'Else
                                    jadwalmasuk = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), drdcek_a("jammasuk").ToString)
                                    'End If

                                    If TimeValue(drdcek_a("tolmasuk").ToString) >= "00:00" And TimeValue(drdcek_a("tolmasuk").ToString) <= "04:00" And TimeValue(drdcek_a("jammasuk").ToString) >= "19:00" And TimeValue(drdcek_a("jammasuk").ToString) <= "23:59" Then
                                        jadwalmasuk_tol = String.Format("{0} {1}", convert_date_to_eng(DateAdd(DateInterval.Day, 1, tanggal_kalen)), drdcek_a("tolmasuk").ToString)
                                    ElseIf TimeValue(drdcek_a("tolmasuk").ToString) >= "19:00" And TimeValue(drdcek_a("tolmasuk").ToString) <= "23:59" And TimeValue(drdcek_a("jammasuk").ToString) >= "00:00" And TimeValue(drdcek_a("jammasuk").ToString) <= "04:00" Then
                                        jadwalmasuk_tol = String.Format("{0} {1}", convert_date_to_eng(DateAdd(DateInterval.Day, -1, tanggal_kalen)), drdcek_a("tolmasuk").ToString)
                                    Else
                                        jadwalmasuk_tol = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), drdcek_a("tolmasuk").ToString)
                                    End If

                                    'If TimeValue(drdcek_a("awalkeluar").ToString) >= "19:00" And TimeValue(drdcek_a("awalkeluar").ToString) <= "23:59" And TimeValue(drdcek_a("jamkeluar").ToString) >= "00:00" And TimeValue(drdcek_a("jamkeluar").ToString) <= "12:00" Then
                                    '    jadwalkeluar_awl = String.Format("{0} {1}", convert_date_to_eng(DateAdd(DateInterval.Day, -1, tanggal_kalen)), drdcek_a("awalkeluar").ToString)
                                    If TimeValue(drdcek_a("awalkeluar").ToString) >= "00:00" And TimeValue(drdcek_a("awalkeluar").ToString) <= "07:00" Then
                                        jadwalkeluar_awl = String.Format("{0} {1}", convert_date_to_eng(DateAdd(DateInterval.Day, 1, tanggal_kalen)), drdcek_a("awalkeluar").ToString)
                                    Else
                                        jadwalkeluar_awl = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), drdcek_a("awalkeluar").ToString)
                                    End If

                                    If TimeValue(drdcek_a("akhirkeluar").ToString) >= "00:00" And TimeValue(drdcek_a("akhirkeluar").ToString) <= "12:00" And TimeValue(drdcek_a("jammasuk").ToString) >= "15:00" And TimeValue(drdcek_a("jammasuk").ToString) <= "23:59" Then
                                        jadwalkeluar_akh = String.Format("{0} {1}", convert_date_to_eng(DateAdd(DateInterval.Day, 1, tanggal_kalen)), drdcek_a("akhirkeluar").ToString)
                                    Else
                                        jadwalkeluar_akh = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), drdcek_a("akhirkeluar").ToString)
                                    End If

                                    If TimeValue(drdcek_a("jamkeluar").ToString) >= "00:00" And TimeValue(drdcek_a("jamkeluar").ToString) <= "12:00" And TimeValue(drdcek_a("jammasuk").ToString) >= "15:00" And TimeValue(drdcek_a("jammasuk").ToString) <= "23:59" Then
                                        jadwalkeluar = String.Format("{0} {1}", convert_date_to_eng(DateAdd(DateInterval.Day, 1, tanggal_kalen)), drdcek_a("jamkeluar").ToString)
                                    Else
                                        jadwalkeluar = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), drdcek_a("jamkeluar").ToString)
                                    End If

                                    If TimeValue(drdcek_a("tolkeluar").ToString) >= "00:00" And TimeValue(drdcek_a("tolkeluar").ToString) <= "12:00" And TimeValue(drdcek_a("jammasuk").ToString) >= "15:00" And TimeValue(drdcek_a("jammasuk").ToString) <= "23:59" Then
                                        jadwalkeluar_tol = String.Format("{0} {1}", convert_date_to_eng(DateAdd(DateInterval.Day, 1, tanggal_kalen)), drdcek_a("tolkeluar").ToString)
                                    Else
                                        jadwalkeluar_tol = String.Format("{0} {1}", convert_date_to_eng(tanggal_kalen), drdcek_a("tolkeluar").ToString)
                                    End If

                                End If

                            End If
                            drdcek_a.Close()

                            '' apakah benar awal - end

                            If Not (jadwalmasuk_awl = "12/12/2007 11:11:11") And masukawal_a = False Then

                                If jadwalkeluar_awl <= masuksementara And jadwalkeluar_akh >= masuksementara Then
                                    realkeluar = masuksementara

                                    If realkeluar_awl = "12/12/2007 11:11:11" Then
                                        realkeluar_awl = DateAdd(DateInterval.Minute, 11, realkeluar)
                                    End If

                                ElseIf jadwalkeluar_akh = "12/12/2007 11:11:11" Then
                                    realkeluar = masuksementara

                                    If realkeluar_awl = "12/12/2007 11:11:11" Then
                                        realkeluar_awl = DateAdd(DateInterval.Minute, 11, realkeluar)
                                    End If

                                End If

                            End If

lanjut_next:
                        End While
                        drdmesin.Close()

                        If Not realmasuk = "12/12/2007 11:11:11" And Not realkeluar = "12/12/2007 11:11:11" Then

                            If Not (jam1_param = Nothing) Then
                                putar_inout2_(cn, tanggal_kalen, tanggal_kalen, jam1_param, jam2_param, cb_gol.EditValue, cb_depart.EditValue, nnip)
                            End If

                            mulai_kalk2(cn, id_param, skaktothasil, tanggal_kalen)

                            otomat_shift = True

                            Dim sqlup_msShift As String = String.Format("update ms_inout set skalk=1,tgl_kalk='{0}' where checktime>='{1}' and checktime<='{2}' and userid={3}", _
                                                                             convert_date_to_eng(tanggal_kalen), convert_datetime_to_eng(realmasuk), convert_datetime_to_eng(DateAdd(DateInterval.Second, 59, realkeluar)), idmesin)
                            Using cmdup_msShift As OleDbCommand = New OleDbCommand(sqlup_msShift, cn)
                                cmdup_msShift.ExecuteNonQuery()
                            End Using

                            If jam1_param = Nothing Then
                                realmasuk = "12/12/2007 11:11:11"
                                realkeluar = "12/12/2007 11:11:11"

                                jadwalmasuk = "12/12/2007 11:11:11"
                                jadwalmasuk_awl = "12/12/2007 11:11:11"
                                jadwalmasuk_akh = "12/12/2007 11:11:11"
                                jadwalmasuk_tol = "12/12/2007 11:11:11"
                                jadwalkeluar = "12/12/2007 11:11:11"
                                jadwalkeluar_awl = "12/12/2007 11:11:11"
                                jadwalkeluar_akh = "12/12/2007 11:11:11"
                                jadwalkeluar_tol = "12/12/2007 11:11:11"
                            End If

                        End If

                        'If masukada = False Then
                        '    insert_to_hadir_zerro(cn, id_param, tanggal_kalk)
                        'End If

                    End If
                    '' akhir kalau bukan shift

                    If shiftok = False And otomat_shift = False Then
                        insert_to_hadir_zerro(cn, id_param, tanggal_kalen)
                    End If

                Next


            Next
            '' akhir loop hari

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

    Private Sub mulai_kalk2(ByVal cn As OleDbConnection, ByVal id_param As Integer, ByVal skalktothasil As Boolean, ByVal tanggal_kalk As Date)

        Dim stelat As Integer = 0
        Dim jmltelat As Integer = 0
        jmltelat = DateDiff(DateInterval.Minute, jadwalmasuk_tol, realmasuk)
        If jmltelat > 0 Then
            stelat = 1
        Else
            jmltelat = 0
        End If

        Dim spulangcepat As Integer = 0
        If DateDiff(DateInterval.Minute, realkeluar, jadwalkeluar_tol) > 0 Then
            spulangcepat = 1
        End If

        Dim realjamkerja As Integer = DateDiff(DateInterval.Hour, realmasuk, realkeluar)

        Dim jamistirahat1 As DateTime = "12/12/2007 11:11:11"
        Dim jamistirahat2 As DateTime = "12/12/2007 11:11:11"
        Dim jamistirahat3 As DateTime = "12/12/2007 11:11:11"

        If TimeValue(realmasuk) <= "12:00" And TimeValue(realkeluar) >= "13:00" Then
            jamistirahat1 = String.Format("{0} 12:00", convert_date_to_eng(DateValue(realmasuk)))
        End If

        If TimeValue(realmasuk) <= "16:00" And (TimeValue(realkeluar) >= "17:00" Or (TimeValue(realkeluar) >= "00:00" And TimeValue(realkeluar) <= "12:00")) Then
            jamistirahat2 = String.Format("{0} 18:00", convert_date_to_eng(DateValue(realmasuk)))
        End If

        If DateValue(realmasuk) <> DateValue(realkeluar) Then
            jamistirahat3 = String.Format("{0} 03:00", convert_date_to_eng(DateValue(realkeluar)))
        End If

        Dim selisih_istirahat1 As Integer = 0
        Dim selisih_istirahat2 As Integer = 0
        Dim selisih_istirahat3 As Integer = 0

        If Not jamistirahat1 = "12/12/2007 11:11:11" Then
            selisih_istirahat1 = DateDiff(DateInterval.Hour, jamistirahat1, realkeluar)
        End If

        If Not jamistirahat2 = "12/12/2007 11:11:11" Then
            selisih_istirahat2 = DateDiff(DateInterval.Hour, jamistirahat2, realkeluar)
        End If

        If Not jamistirahat3 = "12/12/2007 11:11:11" Then
            selisih_istirahat3 = DateDiff(DateInterval.Hour, jamistirahat3, realkeluar)
        End If

        Dim selisihmasuk As Integer = DateDiff(DateInterval.Minute, realmasuk, jadwalmasuk)

        Dim jjam1 As DateTime

        '' kalau bukan visi gak ada lembur diawal
        If jenishitung_lembur <> 1 Then
            jjam1 = String.Format("{0} {1}", convert_date_to_eng(DateValue(realmasuk)), String.Format("{0}:00:00", jadwalmasuk.Hour))
        Else

            If selisihmasuk >= 60 Then
                jjam1 = String.Format("{0} {1}", convert_date_to_eng(DateValue(realmasuk)), String.Format("{0}:00:00", realmasuk.Hour))
            ElseIf selisihmasuk <= -6 Then

                If jenishitung_lembur = 1 Then

                    If realmasuk.Hour <> 23 Then
                        jjam1 = String.Format("{0} {1}", convert_date_to_eng(DateValue(realmasuk)), String.Format("{0}:00:00", realmasuk.Hour + 1))
                    Else
                        jjam1 = String.Format("{0} {1}", convert_date_to_eng(DateAdd(DateInterval.Day, 1, DateValue(realmasuk))), String.Format("{0}:00:00", "00"))
                    End If

                Else
                    GoTo jam_masuk_biasa
                End If

            Else

jam_masuk_biasa:

                jjam1 = String.Format("{0} {1}", convert_date_to_eng(DateValue(realmasuk)), String.Format("{0}:00:00", jadwalmasuk.Hour))
            End If

        End If

        Dim jjam2 As DateTime = String.Format("{0} {1}", convert_date_to_eng(DateValue(realkeluar)), String.Format("{0}:00:00", realkeluar.Hour))

        Dim jamkerja_jam As Integer = DateDiff(DateInterval.Hour, jjam1, jjam2)

        If selisih_istirahat1 >= 1 Then
            jamkerja_jam = jamkerja_jam - 1

            If jenishitung_lembur = 3 Then
                GoTo lompati_kurangjam
            End If

        End If

        If selisih_istirahat2 >= 1 Then
            jamkerja_jam = jamkerja_jam - 1

            If jenishitung_lembur = 3 Then
                GoTo lompati_kurangjam
            End If

        End If

        If selisih_istirahat3 >= 1 Then
            jamkerja_jam = jamkerja_jam - 1
        End If

lompati_kurangjam:

        Dim jmllembur As Double = 0
        Dim libur_ok As Integer
        Dim stat_lembur As String

        If libur_ht.Equals("LIBUR") Then
            libur_ok = 1
            stat_lembur = "TDK TERJADWAL"
        Else
            libur_ok = 0
            stat_lembur = "TERJADWAL"
        End If

        If liburnormal = 1 Then

            If libur_ht.Equals("KERJA") Then
                If jamkerja_jam > IIf(hitungan_jam <> 0, hitungan_jam, 7) And Not (nama_hari_ht.Equals("SABTU")) Then
                    jmllembur = jamkerja_jam - IIf(hitungan_jam <> 0, hitungan_jam, 7)
                    jmllembur = jmllembur * 60
                End If


                '' kalau harian dsa
                If nama_hari_ht.Equals("SABTU") And sharian = 1 Then

                    If jenishitung_lembur = 3 Then

                        If jamkerja_jam > IIf(hitungan_jam <> 0, hitungan_jam, 7) Then
                            jmllembur = jamkerja_jam - IIf(hitungan_jam <> 0, hitungan_jam, 7)
                            jmllembur = jmllembur * 60
                        End If

                    Else

                        If jamkerja_jam > IIf(hitungan_jam <> 0, hitungan_jam, 7) Then
                            jmllembur = jamkerja_jam - IIf(hitungan_jam <> 0, hitungan_jam, 7)
                            jmllembur = jmllembur * 60
                        End If

                    End If
                    

                Else

                    If nama_hari_ht.Equals("SABTU") And jamkerja_jam > IIf(hitungan_jam <> 0, hitungan_jam, 5) Then
                        jmllembur = jamkerja_jam - IIf(hitungan_jam <> 0, hitungan_jam, 5)
                        jmllembur = jmllembur * 60
                    End If

                End If

            Else

                jmllembur = jamkerja_jam * 60

            End If

        Else

            Dim harilibur As String = ""

            Dim sqllibur As String = String.Format("select namahari from ms_karyawan2 where nip='{0}' and tanggal1<='{1}' and tanggal1>='{1}'", nnip, convert_date_to_eng(tanggal_kalk))
            Dim cmdlibur As OleDbCommand = New OleDbCommand(sqllibur, cn)
            Dim drdlibur As OleDbDataReader = cmdlibur.ExecuteReader

            If drdlibur.Read Then
                harilibur = drdlibur(0).ToString
            End If
            drdlibur.Close()

            Dim inilibur As Integer = 0
            If harilibur.Equals(nama_hari_ht) Then
                inilibur = 1
                libur_ok = 1
                stat_lembur = "TDK TERJADWAL"
            Else

                '' kalau visi
                If jenishitung_lembur = 1 Then

                    If depart.Equals("YARN") Then
                        'If libur_ht = "LIBUR" Then
                        '    If Not (jenislibur_hit.Equals("Libur Normal") Or jenislibur_hit.Equals("-")) Then

                        '        inilibur = 1
                        '        libur_ok = 1
                        '        stat_lembur = "TDK TERJADWAL"

                        '    End If
                        'Else
                        inilibur = 0
                        libur_ok = 0
                        stat_lembur = "TERJADWAL"
                        'End If
                    End If

                End If
                '' akhir kalau visi

            End If


            If inilibur = 1 Then
                jmllembur = jamkerja_jam * 60
            Else


                If nama_hari_ht.Equals("SABTU") And sharian = 1 Then

                    If jenishitung_lembur = 3 Then

                        If jamkerja_jam > IIf(hitungan_jam <> 0, hitungan_jam, 5) Then
                            jmllembur = jamkerja_jam - IIf(hitungan_jam <> 0, hitungan_jam, 5)
                            jmllembur = jmllembur * 60
                        End If

                    Else

                        If jamkerja_jam > IIf(hitungan_jam <> 0, hitungan_jam, 7) Then
                            jmllembur = jamkerja_jam - IIf(hitungan_jam <> 0, hitungan_jam, 7)
                            jmllembur = jmllembur * 60
                        End If

                    End If

                    

                    GoTo lewati_hitnormal

                End If


                If nama_hari_ht.Equals("SABTU") Then

                    If jenishitung_lembur = 3 Then
                        If jamkerja_jam > IIf(hitungan_jam <> 0, hitungan_jam, 5) Then
                            jmllembur = jamkerja_jam - IIf(hitungan_jam <> 0, hitungan_jam, 5)
                            jmllembur = jmllembur * 60
                        End If
                    Else
                        If jamkerja_jam > IIf(hitungan_jam <> 0, hitungan_jam, 7) Then
                            jmllembur = jamkerja_jam - IIf(hitungan_jam <> 0, hitungan_jam, 7)
                            jmllembur = jmllembur * 60
                        End If
                    End If

                ElseIf Not (nama_hari_ht.Equals("SABTU")) And jamkerja_jam > IIf(hitungan_jam <> 0, hitungan_jam, 7) Then
                    jmllembur = jamkerja_jam - IIf(hitungan_jam <> 0, hitungan_jam, 7)
                    jmllembur = jmllembur * 60
                End If

lewati_hitnormal:

            End If

        End If


        '' cek tambahan didatabase

        Dim jmllembur_real As Double = jmllembur

        Dim tamblembur As Double = 0.0
        Dim stathadir As String = "HADIR"
        Dim tothasil As Double = 0.0

        Dim sqlcek_tambahan As String = ""

        If id_param = 0 Then
            sqlcek_tambahan = String.Format("select tamblembur,tamb1,tamb2,totlembur,tothasil,stat from tr_hadir where tanggal='{0}' and nip='{1}' and kd_shift='{2}' and kdgol='{3}'", _
                                                      convert_date_to_eng(tanggal_kalk), nnip, namashift, cb_gol.EditValue)

            Dim cmdcek_tambahan As OleDbCommand = New OleDbCommand(sqlcek_tambahan, cn)
            Dim drdcek_tambahan As OleDbDataReader = cmdcek_tambahan.ExecuteReader
            If drdcek_tambahan.Read Then

                If Double.Parse(drdcek_tambahan("tamblembur").ToString) >= 60 Or Double.Parse(drdcek_tambahan("tamblembur").ToString) < 0.0 Then
                    tamblembur = Double.Parse(drdcek_tambahan("tamblembur").ToString)
                    jmllembur = jmllembur + tamblembur
                Else
                    tamblembur = Double.Parse(drdcek_tambahan("tamblembur").ToString) / 60
                End If

                tothasil = Double.Parse(drdcek_tambahan("tothasil").ToString)

                ' If drdcek_tambahan("stat").ToString.Equals("MENGINAP") Then
                stathadir = drdcek_tambahan("stat").ToString
                'End If

                tothasil = Double.Parse(drdcek_tambahan("tothasil").ToString)
            End If
            drdcek_tambahan.Close()

        Else

            If Double.Parse(dv1(Me.BindingContext(dv1).Position)("tamblembur").ToString) >= 1 Then
                tamblembur = Double.Parse(dv1(Me.BindingContext(dv1).Position)("tamblembur").ToString) * 60
                jmllembur = jmllembur + tamblembur
            Else
                tamblembur = Double.Parse(dv1(Me.BindingContext(dv1).Position)("tamblembur").ToString)
            End If

        End If


        '' akhir cek tambahan didatabase


        '' uang makan dan tambahan

        Dim uangmakan_ As Double
        If stathadir.Equals("HADIR") Then

            If libur_ok = 0 Then
                uangmakan_ = tuangmakan
            Else
                uangmakan_ = 0
            End If

        ElseIf stathadir.Equals("MENGINAP") Then
            uangmakan_ = tuangmakan_inap
        End If

        Dim tambmakan As Double = 0
        If (jenislembur.Equals("Depnaker") Or jenislembur.Equals("Jam Mati")) And jenisgaji.Equals("Bulanan") Then
            If (jmllembur / 60) >= 3 Then

                If libur_ok = 0 Then
                    tambmakan = tamb_makan
                End If

            Else
                tambmakan = 0
            End If
        Else
            tambmakan = 0
        End If
        ' akhir uang makan


        '' rubah jika tidak ada lembur
        If jmllembur = 0 Then
            stat_lembur = "-"
        End If
        '' akhir jika tidak ada lembur

        Dim jam1 As Double = 0.0
        Dim jam2 As Double = 0.0
        Dim jam3 As Double = 0.0
        Dim jam4 As Double = 0.0

        If jmllembur <> 0 Then

            If jenislembur.Equals("Depnaker") Then

                jam1 = 1.5
                jam2 = ((jmllembur / 60) - 1) * 2
                If libur_ok = 1 Then
                    jam1 = 0
                    jam2 = ((jmllembur / 60)) * 2
                End If

                jam3 = 0
                jam4 = 0

                If tamblembur > 0.0 And tamblembur < 1.0 Then
                    jam3 = tamblembur
                End If

            ElseIf jenislembur.Equals("Jam Mati") Then
                jam1 = 0

                If libur_ok = 1 Then

                    '' kalau dsa / khusus
                    If jenishitung_lembur = 3 Then

                        If depart.ToUpper.Equals("SATPAM") Then
                            If jenislibur_hit.ToUpper.Equals("LIBUR HARI BESAR") Then
                                jam2 = ((jmllembur) / 60) * 1
                            Else
                                jam2 = 0
                                jmllembur_real = 0
                                jmllembur = 0
                                tamblembur = 0
                            End If

                        Else

                            jam2 = ((jmllembur - tamblembur) / 60) * 2
                            If tamblembur >= 1.0 Then
                                jam2 = jam2 + (tamblembur / 60)
                            End If

                        End If

                        '' visi ikut2an
                    ElseIf jenishitung_lembur = 1 Then

                        If DateValue(tanggal_kalk) <= DateValue("01/05/2015") Then
                            GoTo hitung_normalsini
                        End If

                        jam2 = ((jmllembur - tamblembur) / 60) * 2
                        If tamblembur >= 1.0 Then
                            jam2 = jam2 + (tamblembur / 60)
                        End If

                    Else

hitung_normalsini:

                        jam2 = (jmllembur / 60) * 1
                        If tamblembur >= 1.0 Then
                            jam2 = jam2 + (tamblembur / 60)
                        End If

                    End If

                Else

                    jam2 = (jmllembur / 60) * 1

                    '' kalau dsa
                    If jenishitung_lembur = 3 Then
                        If depart.ToUpper.Equals("SATPAM") Then
                            jam2 = 0
                            jmllembur = 0
                            jmllembur_real = 0
                            tamblembur = 0
                        End If
                    End If


                End If

                jam3 = 0
                jam4 = 0

                If tamblembur > 0.0 And tamblembur < 1.0 Then

                    jam3 = tamblembur

                    ' kalau dsa
                    If jenishitung_lembur = 3 Then
                        If depart.ToUpper.Equals("SATPAM") Then
                            If Not (jenislibur_hit.ToUpper.Equals("LIBUR HARI BESAR")) Then
                                jam3 = 0
                            End If
                        End If
                    End If

                End If

            End If

        End If

        If tamblembur > 0.0 And tamblembur < 1.0 Then

            If jmllembur = 0 Then
                jam3 = tamblembur
            End If

            tamblembur = tamblembur * 60

            ' kalau dsa
            If jenishitung_lembur = 3 Then
                If depart.ToUpper.Equals("SATPAM") Then
                    If Not (jenislibur_hit.ToUpper.Equals("LIBUR HARI BESAR")) Then
                        jam3 = 0
                        tamblembur = 0
                    End If
                End If
            End If

        End If


        Dim jmllembur_rp As Double = 0

        If jam1 + jam2 + jam3 + jam4 = 0 Then
            jmllembur_rp = 0
        Else
            jmllembur_rp = lembur_perjam * (jam1 + jam2 + jam3 + jam4)
        End If

        If jmllembur_rp < 1 Then
            jmllembur_rp = 0
        End If

        Dim jharian As Integer = 0

        If sharian = 1 Then

            If libur_ok = 1 Then
                jharian = 0
            Else

                If jniskelamin.Equals("Perempuan") Then

                    If gapok = 0 Then

                        If jamkerja_jam < 6 Then
                            jharian = (perempuan_uang / 7) * jamkerja_jam * 1
                        Else
                            jharian = perempuan_uang * 1
                        End If

                    Else

                        If jamkerja_jam < 6 Then
                            jharian = (gapok / 7) * jamkerja_jam * 1
                        Else
                            jharian = gapok * 1
                        End If

                    End If

                Else

                    If gapok = 0 Then

                        If jamkerja_jam < 6 Then
                            jharian = (laki1_uang / 7) * jamkerja_jam * 1
                        Else
                            jharian = laki1_uang * 1
                        End If

                    Else

                        If jamkerja_jam < 6 Then
                            jharian = (gapok / 7) * jamkerja_jam * 1
                        Else
                            jharian = gapok * 1
                        End If

                    End If

                End If
            End If

        Else
            jharian = 0

            If jniskelamin.Equals("Perempuan") Then

                If gapok = 0 Then

                    If jenishitung_lembur = 3 Then
                        jharian = perempuan_uang * 1
                    End If

                Else

                    If jenishitung_lembur = 3 Then
                        jharian = gapok
                    End If

                End If

            Else

                If gapok = 0 Then

                    If jenishitung_lembur = 3 Then
                        jharian = laki1_uang * 1
                    End If

                Else

                    If jenishitung_lembur = 3 Then
                        jharian = gapok
                    End If

                End If

            End If

        End If

        Dim dthadir2 As DataTable = New DataTable

        If nnip_sebelum.ToString.Trim.Length = 0 Then

            Dim sqlhadir2 As String = ""

            If id_param = 0 Then
                sqlhadir2 = String.Format("select id2,nip,tanggal,kdgol,kd_shift,total from tr_hadir2 inner join tr_hadir on tr_hadir.id=tr_hadir2.id2 where jnisabsen=1 and tanggal='{0}' and nip='{1}' and kdgol='{2}'", convert_date_to_eng(tanggal_kalk), nnip, cb_gol.EditValue)
            Else
                sqlhadir2 = String.Format("select id2,nip,tanggal,kdgol,kd_shift,total from tr_hadir2 inner join tr_hadir on tr_hadir.id=tr_hadir2.id2 where tr_hadir.id={0}", id_param)
            End If

            Dim cmdhadir As OleDbCommand = New OleDbCommand(sqlhadir2, cn)
            Dim drdhadir2 As OleDbDataReader = cmdhadir.ExecuteReader
            dthadir2.Load(drdhadir2)

            Dim sqlcari As String = ""

            If id_param = 0 Then
                sqlcari = String.Format("delete from tr_hadir where jnisabsen=1 and tanggal='{0}' and nip='{1}' and kdgol='{2}'", convert_date_to_eng(tanggal_kalk), nnip, cb_gol.EditValue)

                Using cmd As OleDbCommand = New OleDbCommand(sqlcari, cn)
                    cmd.ExecuteNonQuery()
                End Using

                'Else
                '    sqlcari = String.Format("delete from tr_hadir where id={0}", id_param)
            End If

            nnip_sebelum = nnip

        End If


        Dim hasilper As Double = 0

        If id_param = 0 Then
            Dim sqlinsert As String = String.Format("insert into tr_hadir (nip,tanggal,jammasuk,jampulang,stat,keterangan,stelat,spulangcpat,skalk,jamlembur,jmlhasil,lemburperjam,totlembur,hasilper,tothasil,tambmakan,step,jamkerja,jam1,jam2,jam3,jam4,jharian,jmltelat,jadwalmasuk,jadwalpulang,kdgol,kd_shift,tamblembur,uangmakan,stat_lmbr,depart) values(" _
                                 & "'{0}','{1}','{2}','{3}','{4}','{5}',{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},'{17}',{18},{19},{20},{21},{22},{23},'{24}','{25}','{26}','{27}',{28},{29},'{30}','{31}')", _
                             nnip, convert_date_to_eng(tanggal_kalk), convert_datetime_to_eng(realmasuk), convert_datetime_to_eng(realkeluar), "HADIR", "", stelat, spulangcepat, 1, jmllembur_real, Replace(0, ",", "."), Replace(jmllembur_rp, ",", "."), 0, Replace(hasilper, ",", "."), Replace(tothasil, ",", "."), Replace(tambmakan, ",", "."), 2, Replace(realjamkerja, ",", "."), Replace(jam1, ",", "."), Replace(jam2, ",", "."), Replace(jam3, ",", "."), Replace(jam4, ",", "."), Replace(jharian, ",", "."), jmltelat, convert_datetime_to_eng(jadwalmasuk), convert_datetime_to_eng(jadwalkeluar), cb_gol.EditValue, namashift, Replace(tamblembur, ",", "."), Replace(uangmakan_, ",", "."), stat_lembur, depart)

            Using cmdins As OleDbCommand = New OleDbCommand(sqlinsert, cn)
                cmdins.ExecuteNonQuery()
            End Using


            If Not IsNothing(dthadir2) Then

                tothasil = 0

                For x As Integer = 0 To dthadir2.Rows.Count - 1

                    tothasil = tothasil + Double.Parse(dthadir2(x)("total").ToString)

                    Dim sqlhadir_upd As String = String.Format("select id from tr_hadir where nip='{0}' and tanggal='{1}' and kdgol='{2}' and kd_shift='{3}'", nnip, convert_date_to_eng(tanggal_kalk), cb_gol.EditValue, namashift)
                    Dim cmdhadir_upd As OleDbCommand = New OleDbCommand(sqlhadir_upd, cn)
                    Dim drhadir_upd As OleDbDataReader = cmdhadir_upd.ExecuteReader

                    If drhadir_upd.Read Then
                        If IsNumeric(drhadir_upd(0).ToString) Then

                            Dim sqlupdate_hadir2 As String = String.Format("update tr_hadir2 set id2={0} where id2={1}", drhadir_upd(0).ToString, dthadir2(x)("id2").ToString)
                            Using cmdupdate_hadir2 As OleDbCommand = New OleDbCommand(sqlupdate_hadir2, cn)
                                cmdupdate_hadir2.ExecuteNonQuery()
                            End Using

                        End If
                    End If
                    drhadir_upd.Close()

                Next

            End If

        Else

            save_editrow_(cn, stathadir, 1, stelat, spulangcepat, jmltelat, realjamkerja, jmllembur_real, jmllembur_rp, jam1, jam2, jam3, jam4, hasilper, tothasil, _
                              uangmakan_, tambmakan, jharian, tamblembur, namashift, id_param, False, stat_lembur)

        End If


    End Sub

    Private Sub save_editrow_(ByVal cn As OleDbConnection, _
                              ByVal stathadir As String, ByVal skalk As Integer, _
                              ByVal stelat As Integer, ByVal spulangcepat As Integer, ByVal jmltelat As Integer, ByVal realjamkerja As Double, _
                              ByVal jmllembur As Integer, ByVal jmllembur_rp As Double, ByVal jam1 As Integer, ByVal jam2 As Integer, _
                              ByVal jam3 As Integer, ByVal jam4 As Integer, ByVal hasilper As Double, ByVal tothasil As Double, ByVal uangmakan As Double, ByVal tambmakan As Double, _
                              ByVal jharian As Double, ByVal tamblembur As Double, ByVal namashift As String, ByVal id_param As Integer, ByVal stothasil_up As Boolean, ByVal stat_lmbr As String)

        If TimeValue(realmasuk) = TimeValue("00:00:00") Then

            If stathadir = "HADIR" Then
                stathadir = "LAIN-LAIN"
            End If
        Else
            If Not (stathadir = "HADIR" Or stathadir = "MENGINAP") Then
                stathadir = "HADIR"
            End If
        End If

        Dim sqlupdate As String = String.Format("update tr_hadir set jammasuk='{0}',jampulang='{1}',stat='{2}',stelat={3},spulangcpat={4}, " & _
                    "jmltelat={5},skalk={6},jamkerja={7},jamlembur={8},jmlhasil={9},lemburperjam={10},totlembur={11}, " & _
                    "jam1={12},jam2={13},jam3={14},jam4={15},hasilper={16},tambmakan={17},step=2,jharian={18}, " & _
                    "jadwalmasuk='{19}',jadwalpulang='{20}',tamblembur={21},tamb1={22},tamb2={23},kd_shift='{24}',uangmakan={25},stat_lmbr='{26}',depart='{27}' " & _
                    "where id={28}", convert_datetime_to_eng(realmasuk), convert_datetime_to_eng(realkeluar), stathadir, stelat, spulangcepat, jmltelat, 1, Replace(realjamkerja, ",", "."), _
                    jmllembur, 0, Replace(jmllembur_rp, ",", "."), 0, Replace(jam1, ",", "."), Replace(jam2, ",", "."), Replace(jam3, ",", "."), Replace(jam4, ",", "."), _
                    Replace(hasilper, ",", "."), Replace(tambmakan, ",", "."), Replace(jharian, ",", "."), convert_datetime_to_eng(jadwalmasuk), convert_datetime_to_eng(jadwalkeluar), _
                    Replace(tamblembur, ",", "."), 0, 0, namashift, uangmakan, stat_lmbr, depart, id_param)

        Using cmdupdate As OleDbCommand = New OleDbCommand(sqlupdate, cn)
            cmdupdate.ExecuteNonQuery()
        End Using

        If stothasil_up Then

            Dim sqltot As String = String.Format("update tr_hadir set tothasil={0} where id={1}", Replace(tothasil, ",", "."), id_param)
            Using cmdtot As OleDbCommand = New OleDbCommand(sqltot, cn)
                cmdtot.ExecuteNonQuery()
            End Using

        End If

    End Sub

    Private Sub insert_to_hadir_zerro(ByVal cn As OleDbConnection, ByVal id_param As Integer, ByVal tanggal_kalk As Date)

        If id_param = 0 Then

            Dim sql_stat As String = String.Format("select stat from tr_hadir where not(stat='HADIR') and jnisabsen=1 and tanggal='{0}' and nip='{1}' and kdgol='{2}'", convert_date_to_eng(tanggal_kalk), nnip, cb_gol.EditValue)
            Dim cmd_stat As OleDbCommand = New OleDbCommand(sql_stat, cn)
            Dim drd_stat As OleDbDataReader = cmd_stat.ExecuteReader

            Dim stathadir As String = ""

            If drd_stat.Read Then
                If Not (drd_stat(0).ToString.Equals("")) Then
                    stathadir = drd_stat(0).ToString
                End If
            End If
            drd_stat.Close()

            If stathadir = "HADIR" Then
                stathadir = "LAIN-LAIN"
            ElseIf stathadir.Length = 0 Then
                stathadir = "LAIN-LAIN"
            End If

            Dim sqlcari As String = String.Format("delete from tr_hadir where jnisabsen=1 and tanggal='{0}' and nip='{1}' and kdgol='{2}'", convert_date_to_eng(tanggal_kalk), nnip, cb_gol.EditValue)
            Using cmd As OleDbCommand = New OleDbCommand(sqlcari, cn)
                cmd.ExecuteNonQuery()
            End Using

            Dim sqlcari2 As String = String.Format("delete from tr_hadir2 where id2 in (select id from tr_hadir " & _
            "where jnisabsen=1 and tanggal='{0}' and nip='{1}' and kdgol='{2}')", convert_date_to_eng(tanggal_kalk), nnip, cb_gol.EditValue)
            Using cmdcar2 As OleDbCommand = New OleDbCommand(sqlcari2, cn)
                cmdcar2.ExecuteNonQuery()
            End Using

            Dim sqlinsert As String = String.Format("insert into tr_hadir (nip,tanggal,jammasuk,jampulang,stat,keterangan,stelat,spulangcpat,skalk,jamlembur,jmlhasil,lemburperjam,totlembur,hasilper,tothasil,tambmakan,step,jamkerja,jam1,jam2,jam3,jam4,jharian,jmltelat,jadwalmasuk,jadwalpulang,kdgol,kd_shift,uangmakan,stat_lmbr,depart) values(" _
                                  & "'{0}','{1}','{2}','{3}','{4}','{5}',{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},'{17}',{18},{19},{20},{21},{22},{23},'{24}','{25}','{26}','{27}',{28},'{29}','{30}')", _
                              nnip, convert_date_to_eng(tanggal_kalk), "00:00", "00:00", stathadir, "", 0, 0, 0, 0, Replace(0, ",", "."), Replace(0, ",", "."), 0, Replace(0, ",", "."), Replace(0, ",", "."), Replace(0, ",", "."), 2, Replace(0, ",", "."), Replace(0, ",", "."), Replace(0, ",", "."), Replace(0, ",", "."), Replace(0, ",", "."), Replace(0, ",", "."), 0, "00:00", "00:00", cb_gol.EditValue, "-", 0, "-", depart)

            Using cmdins As OleDbCommand = New OleDbCommand(sqlinsert, cn)
                cmdins.ExecuteNonQuery()
            End Using

        Else


            Dim sqlcari2 As String = String.Format("delete from tr_hadir2 where id2 in (select id from tr_hadir " & _
            "where id={0})", id_param)
            Using cmdcar2 As OleDbCommand = New OleDbCommand(sqlcari2, cn)
                cmdcar2.ExecuteNonQuery()
            End Using

            Dim sql_stat As String = String.Format("select stat from tr_hadir where id={0}", id_param)
            Dim cmd_stat As OleDbCommand = New OleDbCommand(sql_stat, cn)
            Dim drd_stat As OleDbDataReader = cmd_stat.ExecuteReader

            Dim stathadir As String = ""

            If drd_stat.Read Then
                If Not (drd_stat(0).ToString.Equals("")) Then
                    stathadir = drd_stat(0).ToString
                End If
            End If
            drd_stat.Close()

            If stathadir = "HADIR" Then
                stathadir = "LAIN-LAIN"
            End If

            Dim sqlinsert As String = String.Format("update tr_hadir set jammasuk='00:00',jampulang='00:00',stat='{0}',keterangan='',stelat=0,spulangcpat=0,skalk=0,jamlembur=0,jmlhasil=0,lemburperjam=0,totlembur=0,hasilper=0,tothasil=0,uangmakan=0,tambmakan=0,step=2,jamkerja=0,jam1=0,jam2=0,jam3=0,jam4=0,jharian=0,jmltelat=0,jadwalmasuk='00:00',jadwalpulang='00:00',depart='{1}',kdgol='{2}',kd_shift='-' where id={3}", stathadir, depart, cb_gol.EditValue, id_param)


            Using cmdins As OleDbCommand = New OleDbCommand(sqlinsert, cn)
                cmdins.ExecuteNonQuery()
            End Using

        End If



    End Sub

    Private Sub setColumn()

        GridView1.Columns("nip").Visible = True
        GridView1.Columns("nip").VisibleIndex = 0

        GridView1.Columns("nama").Visible = True
        GridView1.Columns("nama").VisibleIndex = 1

        GridView1.Columns("skalk").Visible = True
        GridView1.Columns("skalk").VisibleIndex = 2

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqlutil As String = String.Format("select * from sutil_absen where kd_gol='{0}'", cb_gol.EditValue)
            Dim cmdutil As OleDbCommand = New OleDbCommand(sqlutil, cn)
            Dim drutil As OleDbDataReader = cmdutil.ExecuteReader

            If drutil.Read Then

                Dim vindex As Integer = 2

                If IsNumeric(drutil("stelat").ToString) Then
                    If drutil("stelat") = 1 Then

                        vindex = vindex + 1

                        GridView1.Columns("stelat").Visible = True
                        GridView1.Columns("stelat").VisibleIndex = vindex
                    Else
                        GridView1.Columns("stelat").Visible = False
                        GridView1.Columns("stelat").VisibleIndex = -1
                    End If
                End If

                If IsNumeric(drutil("spulang_cpt").ToString) Then
                    If drutil("spulang_cpt") = 1 Then

                        vindex = vindex + 1

                        GridView1.Columns("spulangcpat").Visible = True
                        GridView1.Columns("spulangcpat").VisibleIndex = vindex

                    Else
                        GridView1.Columns("spulangcpat").Visible = False
                        GridView1.Columns("spulangcpat").VisibleIndex = -1
                    End If
                End If

                If IsNumeric(drutil("stat_hdr").ToString) Then
                    If drutil("stat_hdr") = 1 Then

                        vindex = vindex + 1

                        GridView1.Columns("stat").Visible = True
                        GridView1.Columns("stat").VisibleIndex = vindex


                        vindex = vindex + 1

                        GridView1.Columns("stat_hari").Visible = True
                        GridView1.Columns("stat_hari").VisibleIndex = vindex

                    Else
                        GridView1.Columns("stat").Visible = False
                        GridView1.Columns("stat").VisibleIndex = -1

                        GridView1.Columns("stat_hari").Visible = False
                        GridView1.Columns("stat_hari").VisibleIndex = -1

                    End If
                End If

                If IsNumeric(drutil("sjmllembur").ToString) Then
                    If drutil("sjmllembur") = 1 Then

                        vindex = vindex + 1

                        GridView1.Columns("stat_lmbr").Visible = True
                        GridView1.Columns("stat_lmbr").VisibleIndex = vindex
                    Else
                        GridView1.Columns("stat_lmbr").Visible = False
                        GridView1.Columns("stat_lmbr").VisibleIndex = -1
                    End If
                End If


                If IsNumeric(drutil("stanggal").ToString) Then
                    If drutil("stanggal") = 1 Then

                        vindex = vindex + 1

                        GridView1.Columns("tanggal").Visible = True
                        GridView1.Columns("tanggal").VisibleIndex = vindex

                        vindex = vindex + 1

                        GridView1.Columns("hari").Visible = True
                        GridView1.Columns("hari").VisibleIndex = vindex

                    Else
                        GridView1.Columns("tanggal").Visible = False
                        GridView1.Columns("tanggal").VisibleIndex = -1

                        GridView1.Columns("hari").Visible = False
                        GridView1.Columns("hari").VisibleIndex = -1

                    End If
                End If

                If IsNumeric(drutil("sjammasuk").ToString) Then
                    If drutil("sjammasuk") = 1 Then

                        vindex = vindex + 1

                        GridView1.Columns("kd_shift").Visible = True
                        GridView1.Columns("kd_shift").VisibleIndex = vindex

                        vindex = vindex + 1

                        GridView1.Columns("jadwalmasuk").Visible = True
                        GridView1.Columns("jadwalmasuk").VisibleIndex = vindex


                    Else

                        GridView1.Columns("kd_shift").Visible = False
                        GridView1.Columns("kd_shift").VisibleIndex = -1

                        GridView1.Columns("jadwalmasuk").Visible = False
                        GridView1.Columns("jadwalmasuk").VisibleIndex = -1

                    End If
                End If

                If IsNumeric(drutil("sjampulang").ToString) Then
                    If drutil("sjampulang") = 1 Then

                        vindex = vindex + 1

                        GridView1.Columns("jadwalpulang").Visible = True
                        GridView1.Columns("jadwalpulang").VisibleIndex = vindex


                    Else

                        GridView1.Columns("jadwalpulang").Visible = False
                        GridView1.Columns("jadwalpulang").VisibleIndex = -1


                    End If
                End If

                If IsNumeric(drutil("sjammasuk").ToString) Then
                    If drutil("sjammasuk") = 1 Then

                        vindex = vindex + 1

                        GridView1.Columns("jammasuk").Visible = True
                        GridView1.Columns("jammasuk").VisibleIndex = vindex


                    Else
                        GridView1.Columns("jammasuk").Visible = False
                        GridView1.Columns("jammasuk").VisibleIndex = -1

                    End If
                End If

                If IsNumeric(drutil("sjampulang").ToString) Then
                    If drutil("sjampulang") = 1 Then


                        vindex = vindex + 1

                        GridView1.Columns("jampulang").Visible = True
                        GridView1.Columns("jampulang").VisibleIndex = vindex
                    Else

                        GridView1.Columns("jampulang").Visible = False
                        GridView1.Columns("jampulang").VisibleIndex = -1
                    End If
                End If

                If IsNumeric(drutil("sjmltelat").ToString) Then
                    If drutil("sjmltelat") = 1 Then

                        vindex = vindex + 1

                        GridView1.Columns("jmltelat").Visible = True
                        GridView1.Columns("jmltelat").VisibleIndex = vindex
                    Else
                        GridView1.Columns("jmltelat").Visible = False
                        GridView1.Columns("jmltelat").VisibleIndex = -1
                    End If
                End If

                If IsNumeric(drutil("sjmlkerja").ToString) Then
                    If drutil("sjmlkerja") = 1 Then

                        vindex = vindex + 1

                        GridView1.Columns("jamkerja").Visible = True
                        GridView1.Columns("jamkerja").VisibleIndex = vindex
                    Else
                        GridView1.Columns("jamkerja").Visible = False
                        GridView1.Columns("jamkerja").VisibleIndex = -1
                    End If
                End If

                If IsNumeric(drutil("sjmllembur").ToString) Then
                    If drutil("sjmllembur") = 1 Then

                        vindex = vindex + 1

                        GridView1.Columns("jamlembur").Visible = True
                        GridView1.Columns("jamlembur").VisibleIndex = vindex


                        vindex = vindex + 1

                        GridView1.Columns("lemburdep").Visible = True
                        GridView1.Columns("lemburdep").VisibleIndex = vindex

                        vindex = vindex + 1

                        GridView1.Columns("lemburperjam").Visible = True
                        GridView1.Columns("lemburperjam").VisibleIndex = vindex

                        vindex = vindex + 1

                        GridView1.Columns("tamblembur").Visible = True
                        GridView1.Columns("tamblembur").VisibleIndex = vindex

                        vindex = vindex + 1

                        GridView1.Columns("tambisti").Visible = True
                        GridView1.Columns("tambisti").VisibleIndex = vindex

                    Else
                        GridView1.Columns("jamlembur").Visible = False
                        GridView1.Columns("jamlembur").VisibleIndex = -1

                        GridView1.Columns("lemburdep").Visible = False
                        GridView1.Columns("lemburdep").VisibleIndex = -1

                        GridView1.Columns("lemburperjam").Visible = False
                        GridView1.Columns("lemburperjam").VisibleIndex = -1

                        GridView1.Columns("tamblembur").Visible = False
                        GridView1.Columns("tamblembur").VisibleIndex = -1

                        GridView1.Columns("tambisti").Visible = False
                        GridView1.Columns("tambisti").VisibleIndex = -1

                    End If
                End If

                If IsNumeric(drutil("sharian").ToString) Then
                    If drutil("sharian") = 1 Then


                        vindex = vindex + 1

                        GridView1.Columns("jharian").Visible = True
                        GridView1.Columns("jharian").VisibleIndex = vindex
                    Else

                        GridView1.Columns("jharian").Visible = False
                        GridView1.Columns("jharian").VisibleIndex = -1

                    End If
                End If

                'If IsNumeric(drutil("shasil").ToString) Then
                '    If drutil("shasil") = 1 Then

                '        statkalkhasil = True

                '        vindex = vindex + 1

                '        GridView1.Columns("kdgol_tr").Visible = True
                '        GridView1.Columns("kdgol_tr").VisibleIndex = vindex

                '        vindex = vindex + 1

                '        GridView1.Columns("jmlhasil").Visible = True
                '        GridView1.Columns("jmlhasil").VisibleIndex = vindex
                '    Else

                '        statkalkhasil = False

                '        GridView1.Columns("kdgol_tr").Visible = False
                '        GridView1.Columns("kdgol_tr").VisibleIndex = -1

                '        GridView1.Columns("jmlhasil").Visible = False
                '        GridView1.Columns("jmlhasil").VisibleIndex = -1
                '    End If
                'End If

                'If IsNumeric(drutil("snilhasil").ToString) Then
                '    If drutil("snilhasil") = 1 Then

                '        vindex = vindex + 1

                '        GridView1.Columns("hasilper").Visible = True
                '        GridView1.Columns("hasilper").VisibleIndex = vindex
                '    Else
                '        GridView1.Columns("hasilper").Visible = False
                '        GridView1.Columns("hasilper").VisibleIndex = -1
                '    End If
                'End If

                If IsNumeric(drutil("stothasil").ToString) Then
                    If drutil("stothasil") = 1 Then

                        vindex = vindex + 1

                        GridView1.Columns("tothasil").Visible = True
                        GridView1.Columns("tothasil").VisibleIndex = vindex
                    Else
                        GridView1.Columns("tothasil").Visible = False
                        GridView1.Columns("tothasil").VisibleIndex = -1
                    End If
                End If


                If IsNumeric(drutil("stambmakan").ToString) Then
                    If drutil("stambmakan") = 1 Then

                        vindex = vindex + 1

                        GridView1.Columns("uangmakan").Visible = True
                        GridView1.Columns("uangmakan").VisibleIndex = vindex

                        vindex = vindex + 1

                        GridView1.Columns("tambmakan").Visible = True
                        GridView1.Columns("tambmakan").VisibleIndex = vindex
                    Else

                        GridView1.Columns("uangmakan").Visible = False
                        GridView1.Columns("uangmakan").VisibleIndex = -1

                        GridView1.Columns("tambmakan").Visible = False
                        GridView1.Columns("tambmakan").VisibleIndex = -1
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

    Private Function lembur_perjam_cek(ByVal sharian As Integer, ByVal jenislembur As String, ByVal gapok As Double, _
                                       ByVal jenishitung_lembur As Integer, ByVal tunj_jabatan As Double, ByVal depart As String, ByVal nilharian As Double) As Double

        Dim lembur_perjam As Double = 0

        If sharian = 1 Then

            If jenislembur.Equals("Depnaker") Then

                If gapok > 0 Then

                    If jenishitung_lembur = 1 Then ' visi
                        lembur_perjam = ((gapok * 25) / 173)
                    ElseIf jenishitung_lembur = 2 Then ' grand
                        lembur_perjam = ((gapok + tunj_jabatan) * 3) / 20
                    Else ' dsa

                        'If depart.Equals("HARIAN TETAP") Or depart.Equals("ASS OPERATOR") Then
                        lembur_perjam = gapok * 25
                        lembur_perjam = Math.Floor((lembur_perjam / 173))

                        '    Else
                        '    lembur_perjam = gapok / 7
                        'End If

                    End If


            Else

                If jenishitung_lembur = 1 Then ' visi
                        lembur_perjam = ((nilharian * 25) / 173)
                ElseIf jenishitung_lembur = 2 Then ' grand
                    lembur_perjam = ((nilharian + tunj_jabatan) * 3) / 20
                Else ' dsa

                        'If depart.Equals("HARIAN TETAP") Or depart.Equals("ASS OPERATOR") Then
                        lembur_perjam = nilharian * 25
                        lembur_perjam = Math.Floor((lembur_perjam / 173))
                        'Else
                        '    lembur_perjam = nilharian / 7
                        'End If

                    End If

                End If

        ElseIf jenislembur.Equals("Jam Mati") Then

            If gapok > 0 Then

                If jenishitung_lembur = 1 Then ' visi
                        lembur_perjam = ((gapok * 25) / 173)

                ElseIf jenishitung_lembur = 2 Then ' grand
                    lembur_perjam = (((gapok + tunj_jabatan) * 3) / 20)
                Else ' dsa

                        'If depart.Equals("HARIAN TETAP") Or depart.Equals("ASS OPERATOR") Then
                        lembur_perjam = gapok * 25
                        lembur_perjam = Math.Floor((lembur_perjam / 173))
                        'Else
                        '    lembur_perjam = gapok / 7
                        'End If

                    End If

            Else

                If jenishitung_lembur = 1 Then ' visi
                        lembur_perjam = ((nilharian * 25) / 173)
                ElseIf jenishitung_lembur = 2 Then ' grand
                    lembur_perjam = (((nilharian + tunj_jabatan) * 3) / 20)
                Else ' dsa

                        'If depart.Equals("HARIAN TETAP") Or depart.Equals("ASS OPERATOR") Then

                        lembur_perjam = nilharian * 25
                        lembur_perjam = Math.Floor((lembur_perjam / 173))

                        'Else
                        '    lembur_perjam = nilharian / 7
                        'End If

                    End If


            End If


        Else
            lembur_perjam = 0
        End If

        Else

            If jenislembur.Equals("Depnaker") Then
                lembur_perjam = ((gapok + tunj_jabatan) / 173)
            ElseIf jenislembur.Equals("Jam Mati") Then
                lembur_perjam = ((gapok + tunj_jabatan) / 25 / 7)
            Else
                lembur_perjam = 0
            End If

        End If

        Return lembur_perjam

    End Function

    Private Sub load_detailmesin()

        open_wait()

        'Dim sql As String = String.Format("select distinct ms_karyawan.nama,V_InOut3.tanggal, " & _
        '"case when V_InOut3.skalk=1 then " & _
        '"'Sesuai Jadwal' else 'Invalid/Error' end as skalk " & _
        '"from V_InOut3 inner join ms_karyawan on V_InOut3.userid=ms_karyawan.idmesin " & _
        '"where CONVERT(date,V_InOut3.tanggal)>='{0}' and CONVERT(date,V_InOut3.tanggal)<='{1}'  and ms_karyawan.kdgol='{2}'", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue), cb_gol.EditValue)

        Dim sql As String = String.Format("SELECT  distinct ms_karyawan.nama, " & _
        "CONVERT(datetime, CAST(CONVERT(date, ms_inout.checktime) AS varchar) + ' ' + CAST(LEFT(CONVERT(VARCHAR, CONVERT(time(0), ms_inout.checktime), 108), 5) AS varchar))  " & _
        "AS tanggal, case when ms_inout.skalk=1 then " & _
        "'Sesuai Jadwal' else 'Invalid/Error' end as skalk " & _
        "FROM ms_inout inner join ms_karyawan on ms_inout.userid=ms_karyawan.idmesin " & _
        "WHERE CONVERT(date,ms_inout.checktime)>='{0}' and CONVERT(date,ms_inout.checktime)<='{1}' and ms_karyawan.kdgol='{2}'", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue), cb_gol.EditValue)


        If Not cb_depart.EditValue = "ALL" Then
            sql = String.Format("{0} and ms_karyawan.depart='{1}'", sql, cb_depart.EditValue)
        End If

        If Not cbpeg.EditValue = "ALL" Then
            sql = String.Format("{0} and ms_karyawan.nip='{1}'", sql, cbpeg.EditValue)
        End If

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            dv2 = Nothing

            Dim ds As DataSet
            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager2 = New DataViewManager(ds)
            dv2 = dvmanager2.CreateDataView(ds.Tables(0))

            gridall.DataSource = dv2

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

    Private Function ShowSaveFileDialog(ByVal title As String, ByVal filter As String) As String
        Dim dlg As New SaveFileDialog()
        Dim name As String = Application.ProductName
        Dim n As Integer = name.LastIndexOf(".") + 1
        If n > 0 Then
            name = name.Substring(n, name.Length - n)
        End If
        dlg.Title = "Export To " & title
        dlg.FileName = name
        dlg.Filter = filter
        If dlg.ShowDialog() = DialogResult.OK Then
            Return dlg.FileName
        End If
        Return ""
    End Function

    Private Sub OpenFile(ByVal fileName As String)
        If XtraMessageBox.Show("Anda ingin membuka file ?", "Export To...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                Dim process As New System.Diagnostics.Process()
                process.StartInfo.FileName = fileName
                process.StartInfo.Verb = "Open"
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal
                process.Start()
            Catch
                DevExpress.XtraEditors.XtraMessageBox.Show(Me, "Data tidak ditemukan", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
        '   progressBarControl1.Position = 0
    End Sub


    Private Sub fkalk_absen_n_Load(sender As Object, e As EventArgs) Handles Me.Load

        isload = True

        loadGolongan()
        loadDepartemen()
        load_pegawai()
        '  load_RJamKerja()


        ttgl.EditValue = DateValue(Date.Now)
        ttgl2.EditValue = DateValue(Date.Now)

        isload = False

        setColumn()

    End Sub

    Private Sub cb_gol_EditValueChanged(sender As Object, e As EventArgs) Handles cb_gol.Validated

        If isload Then
            Return
        End If

        '   open_wait()

        loadDepartemen()
        load_pegawai()
        setColumn()

        ' load_grid()
        ' load_kemungkinan_salah()

        '   close_wait()

    End Sub

    Private Sub cb_depart_EditValueChanged(sender As Object, e As EventArgs) Handles cb_depart.Validated

        If isload Then
            Return
        End If

        '   open_wait()

        load_pegawai()

        ' load_grid()
        ' load_kemungkinan_salah()

        ' close_wait()

    End Sub

    Private Sub ttgl_Validated(sender As Object, e As EventArgs) Handles ttgl.Validated

        If IsDate(ttgl.EditValue) Then
            bln_ = Month(ttgl.EditValue)
            thn_ = Year(ttgl.EditValue)
        End If

        If isload Then
            Return
        End If

        '     open_wait()

        ' cek_hari(True, ttgl.EditValue)
        ' load_grid()
        'load_kemungkinan_salah()

        '  close_wait()

    End Sub

    Private Sub btview_Click(sender As Object, e As EventArgs) Handles btview.Click

        XtraTabControl1.SelectedTabPageIndex = 0
        LabelControl8.Enabled = True

        open_wait()
        SetWaitDialog("Proses Penghitungan..")



        If caktif.Checked = True Then
        Else
            mulai_hitung(ttgl.EditValue, ttgl2.EditValue, "", "", 0, "", "", "", "")
        End If


        ' mulai_kalk(0, "", Nothing, Nothing, True, ttgl.EditValue, ttgl2.EditValue, "")

        load_grid()

        '  load_kemungkinan_salah()

        close_wait()

    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        If e.Column.FieldName.Equals("skalk") Or e.Column.FieldName.Equals("stelat") Or e.Column.FieldName.Equals("spulangcpat") Then

            Dim idhadir As Integer = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("id").ToString)
            Dim skalk As Integer = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("skalk").ToString)
            Dim stelat As Integer = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("stelat").ToString)
            Dim spulangcpat As Integer = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("spulangcpat").ToString)

            Dim cns As OleDbConnection = Nothing
            Try

                cns = New OleDbConnection
                cns = Clsmy.open_conn

                Dim sqls As String = String.Format("update tr_hadir set skalk={0},stelat={1},spulangcpat={2} where id={3}", skalk, stelat, spulangcpat, idhadir)
                Using cmds As OleDbCommand = New OleDbCommand(sqls, cns)
                    cmds.ExecuteNonQuery()
                End Using

            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
            Finally
                If Not cns Is Nothing Then
                    If cns.State = ConnectionState.Open Then
                        cns.Close()
                    End If
                End If
            End Try

        ElseIf e.Column.FieldName.Equals("uangmakan") Then

            Dim statt = dv1(Me.BindingContext(dv1).Position)("stat").ToString()

            Dim cnu As OleDbConnection = Nothing
            Try

                cnu = New OleDbConnection
                cnu = Clsmy.open_conn

                Dim uuangmakan As Double = e.Value
                Dim idhadir As Integer = dv1(Me.BindingContext(dv1).Position)("id").ToString

                If Not (statt.Equals("HADIR") Or statt.Equals("SAKIT") Or statt.Equals("CUTI") Or statt.Equals("MENGINAP")) Then
                    uuangmakan = 0
                End If

                Dim sqlupmakan As String = String.Format("update tr_hadir set uangmakan={0} where id={1}", uuangmakan, idhadir)
                Using cmdupmakan As OleDbCommand = New OleDbCommand(sqlupmakan, cnu)
                    cmdupmakan.ExecuteNonQuery()
                End Using

                dv1(Me.BindingContext(dv1).Position)("uangmakan") = uuangmakan

            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
            Finally
                If Not cnu Is Nothing Then
                    If cnu.State = ConnectionState.Open Then
                        cnu.Close()
                    End If
                End If
            End Try

        ElseIf e.Column.FieldName.Equals("stat_lmbr") Then

            Dim cm_lm As OleDbConnection = Nothing
            Try
                cm_lm = New OleDbConnection
                cm_lm = Clsmy.open_conn

                Dim idhadir As Integer = dv1(Me.BindingContext(dv1).Position)("id").ToString
                Dim jmllembur_dep As Double = dv1(Me.BindingContext(dv1).Position)("lemburdep").ToString

                Dim stlembur As String = e.Value
                If jmllembur_dep = 0 Then
                    stlembur = "-"
                End If

                Dim sqlup_stlembur As String = String.Format("update tr_hadir set stat_lmbr='{0}' where id={1}", stlembur, idhadir)
                Using cmdup_stlembur As OleDbCommand = New OleDbCommand(sqlup_stlembur, cm_lm)
                    cmdup_stlembur.ExecuteNonQuery()
                End Using

                dv1(Me.BindingContext(dv1).Position)("stat_lmbr") = stlembur

            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
            Finally
                If Not cm_lm Is Nothing Then
                    If cm_lm.State = ConnectionState.Open Then
                        cm_lm.Close()
                    End If
                End If
            End Try


        ElseIf e.Column.FieldName.Equals("stat") Then

            If e.Value = "HADIR" Or e.Value = "MENGINAP" Then
                dv1(Me.BindingContext(dv1).Position)("skalk") = 1
            Else
                dv1(Me.BindingContext(dv1).Position)("skalk") = 0
            End If

            Dim cnm As OleDbConnection = Nothing
            Try
                cnm = New OleDbConnection
                cnm = Clsmy.open_conn

                Dim idhadir As Integer = dv1(Me.BindingContext(dv1).Position)("id").ToString
                Dim stkalk As Integer = dv1(Me.BindingContext(dv1).Position)("skalk").ToString
                Dim jmllembur_dep As Double = dv1(Me.BindingContext(dv1).Position)("lemburdep").ToString

                Dim jmlharian As Double = 0
                Dim liburnormal As Integer = 0

                Dim sqlcekharian As String = String.Format("select ms_karyawan.liburnormal,ms_golongan.jenisgaji,ms_golongan.harian,ms_golongan.laki2,ms_golongan.perempuan,ms_karyawan.gapok,ms_karyawan.jniskelamin,ms_karyawan.tunj_makan,ms_karyawan.tunj_makan_inap " & _
                "from ms_karyawan inner join ms_golongan on ms_karyawan.kdgol=ms_golongan.kode " & _
                "where nip='{0}'", dv1(Me.BindingContext(dv1).Position)("nip").ToString)
                Dim cmdharian As OleDbCommand = New OleDbCommand(sqlcekharian, cnm)
                Dim drdharian As OleDbDataReader = cmdharian.ExecuteReader

                If drdharian.Read Then
                    If IsNumeric(drdharian("harian").ToString) Then

                        liburnormal = Integer.Parse(drdharian("liburnormal").ToString)
                        jenisgaji = drdharian("jenisgaji").ToString

                        tuangmakan_inap = Double.Parse(drdharian("tunj_makan_inap").ToString)
                        tuangmakan = Double.Parse(drdharian("tunj_makan").ToString)

                        If Integer.Parse(drdharian("harian").ToString) = 1 Then


                            If drdharian("jniskelamin").ToString.Equals("Laki - Laki") Then
                                jmlharian = Double.Parse(drdharian("laki2").ToString)
                            Else
                                jmlharian = Double.Parse(drdharian("perempuan").ToString)
                            End If

                            If jmlharian = 0 Then
                                jmlharian = Double.Parse(drdharian("gapok").ToString)
                            End If

                        End If
                    End If
                End If
                drdharian.Close()

                If e.Value = "HADIR" Then

                    Dim sqlup As String = String.Format("update tr_hadir set uangmakan={0},skalk={1} where id={2}", tuangmakan, stkalk, idhadir)
                    Using cmdup As OleDbCommand = New OleDbCommand(sqlup, cnm)
                        cmdup.ExecuteNonQuery()
                    End Using

                    dv1(Me.BindingContext(dv1).Position)("uangmakan") = tuangmakan


                ElseIf e.Value = "SAKIT" Or e.Value = "CUTI" Then

                    If jenisgaji.ToString.ToUpper = "BULANAN" Then

                        Dim sqlup As String = String.Format("update tr_hadir set jammasuk='00:00',jampulang='00:00',stat='{0}',stelat=0,spulangcpat=0, " & _
                        "jmltelat=0,skalk={1},jamkerja=0,jamlembur=0,jmlhasil=0,lemburperjam=0,totlembur=0, " & _
                        "jam1=0,jam2=0,jam3=0,jam4=0,hasilper=0,tothasil=0,tambmakan=0,step=0,jharian=0,jnisabsen=1, " & _
                        "jadwalmasuk='00:00',jadwalpulang='00:00',tamblembur=0,tamb1=0,tamb2=0,tamb_istirahat=0,kd_shift='-',uangmakan={2},stat_lmbr='-' " & _
                        "where id={3}", e.Value, stkalk, tuangmakan, dv1(Me.BindingContext(dv1).Position)("id").ToString)

                        'Dim sqlup As String = String.Format("update tr_hadir set statlembur='-',tambmakan=0,uangmakan={0},skalk={1} where id={2}", tuangmakan, stkalk, idhadir)
                        Using cmdup As OleDbCommand = New OleDbCommand(sqlup, cnm)
                            cmdup.ExecuteNonQuery()
                        End Using

                        dv1(Me.BindingContext(dv1).Position)("uangmakan") = tuangmakan

                    Else

                        Dim sqlup As String = String.Format("update tr_hadir set jammasuk='00:00',jampulang='00:00',stat='{0}',stelat=0,spulangcpat=0, " & _
                        "jmltelat=0,skalk={1},jamkerja=0,jamlembur=0,jmlhasil=0,lemburperjam=0,totlembur=0, " & _
                        "jam1=0,jam2=0,jam3=0,jam4=0,hasilper=0,tothasil=0,tambmakan=0,step=0,jharian=0,jnisabsen=1, " & _
                        "jadwalmasuk='00:00',jadwalpulang='00:00',tamblembur=0,tamb1=0,tamb2=0,tamb_istirahat=0,kd_shift='-',uangmakan={2},stat_lmbr='-' " & _
                        "where id={3}", e.Value, stkalk, 0, dv1(Me.BindingContext(dv1).Position)("id").ToString)

                        'Dim sqlup As String = String.Format("update tr_hadir set statlembur='-',tambmakan=0,uangmakan={0},skalk={1} where id={2}", 0, stkalk, idhadir)
                        Using cmdup As OleDbCommand = New OleDbCommand(sqlup, cnm)
                            cmdup.ExecuteNonQuery()
                        End Using

                        dv1(Me.BindingContext(dv1).Position)("uangmakan") = 0

                    End If


                    dv1(Me.BindingContext(dv1).Position)("tambmakan") = 0
                    dv1(Me.BindingContext(dv1).Position)("kd_shift") = "-"
                    dv1(Me.BindingContext(dv1).Position)("stelat") = 0
                    dv1(Me.BindingContext(dv1).Position)("spulangcpat") = 0
                    dv1(Me.BindingContext(dv1).Position)("stat_lmbr") = "-"
                    dv1(Me.BindingContext(dv1).Position)("jamlembur") = 0
                    dv1(Me.BindingContext(dv1).Position)("lemburperjam") = 0
                    dv1(Me.BindingContext(dv1).Position)("jammasuk") = DBNull.Value
                    dv1(Me.BindingContext(dv1).Position)("jampulang") = DBNull.Value
                    dv1(Me.BindingContext(dv1).Position)("jadwalmasuk") = DBNull.Value
                    dv1(Me.BindingContext(dv1).Position)("jadwalpulang") = DBNull.Value
                    dv1(Me.BindingContext(dv1).Position)("totlembur") = 0
                    dv1(Me.BindingContext(dv1).Position)("hasilper") = 0
                    dv1(Me.BindingContext(dv1).Position)("jmlhasil") = 0
                    dv1(Me.BindingContext(dv1).Position)("tothasil") = 0
                    dv1(Me.BindingContext(dv1).Position)("jamkerja") = 0
                    dv1(Me.BindingContext(dv1).Position)("jam1") = 0
                    dv1(Me.BindingContext(dv1).Position)("jam2") = 0
                    dv1(Me.BindingContext(dv1).Position)("jam3") = 0
                    dv1(Me.BindingContext(dv1).Position)("jam4") = 0
                    dv1(Me.BindingContext(dv1).Position)("lemburdep") = 0
                    dv1(Me.BindingContext(dv1).Position)("jharian") = 0
                    dv1(Me.BindingContext(dv1).Position)("jmltelat") = 0
                    dv1(Me.BindingContext(dv1).Position)("tamblembur") = 0
                    dv1(Me.BindingContext(dv1).Position)("tamb1") = 0
                    dv1(Me.BindingContext(dv1).Position)("tamb2") = 0
                    dv1(Me.BindingContext(dv1).Position)("tambisti") = 0

                ElseIf e.Value = "ALPHA" Or e.Value = "ALPHA ABSEN" Or e.Value = "OFF" Or e.Value = "LAIN-LAIN" Or e.Value = "DISPENSASI" Or e.Value = "SKORSING" Or e.Value = "OFF PRODUKSI" Then

                    Dim sqlupdate1 As String = String.Format("update tr_hadir set jammasuk='00:00',jampulang='00:00',stat='{0}',stelat=0,spulangcpat=0, " & _
                        "jmltelat=0,skalk=0,jamkerja=0,jamlembur=0,jmlhasil=0,lemburperjam=0,totlembur=0, " & _
                        "jam1=0,jam2=0,jam3=0,jam4=0,hasilper=0,tothasil=0,tambmakan=0,step=0,jharian=0,jnisabsen=1, " & _
                        "jadwalmasuk='00:00',jadwalpulang='00:00',tamblembur=0,tamb1=0,tamb2=0,kd_shift='-',uangmakan=0,tamb_istirahat=0,stat_lmbr='-' " & _
                        "where id={1}", e.Value, dv1(Me.BindingContext(dv1).Position)("id").ToString)

                    Using cmdupdate1 As OleDbCommand = New OleDbCommand(sqlupdate1, cnm)
                        cmdupdate1.ExecuteNonQuery()
                    End Using

                    dv1(Me.BindingContext(dv1).Position)("kd_shift") = "-"
                    dv1(Me.BindingContext(dv1).Position)("skalk") = 0
                    dv1(Me.BindingContext(dv1).Position)("stelat") = 0
                    dv1(Me.BindingContext(dv1).Position)("spulangcpat") = 0
                    dv1(Me.BindingContext(dv1).Position)("stat") = e.Value
                    dv1(Me.BindingContext(dv1).Position)("stat_lmbr") = "-"
                    dv1(Me.BindingContext(dv1).Position)("jamlembur") = 0
                    dv1(Me.BindingContext(dv1).Position)("lemburperjam") = 0
                    dv1(Me.BindingContext(dv1).Position)("jammasuk") = DBNull.Value
                    dv1(Me.BindingContext(dv1).Position)("jampulang") = DBNull.Value
                    dv1(Me.BindingContext(dv1).Position)("jadwalmasuk") = DBNull.Value
                    dv1(Me.BindingContext(dv1).Position)("jadwalpulang") = DBNull.Value
                    dv1(Me.BindingContext(dv1).Position)("totlembur") = 0
                    dv1(Me.BindingContext(dv1).Position)("hasilper") = 0
                    dv1(Me.BindingContext(dv1).Position)("jmlhasil") = 0
                    dv1(Me.BindingContext(dv1).Position)("tothasil") = 0
                    dv1(Me.BindingContext(dv1).Position)("uangmakan") = 0
                    dv1(Me.BindingContext(dv1).Position)("tambmakan") = 0
                    dv1(Me.BindingContext(dv1).Position)("jamkerja") = 0
                    dv1(Me.BindingContext(dv1).Position)("jam1") = 0
                    dv1(Me.BindingContext(dv1).Position)("jam2") = 0
                    dv1(Me.BindingContext(dv1).Position)("jam3") = 0
                    dv1(Me.BindingContext(dv1).Position)("jam4") = 0
                    dv1(Me.BindingContext(dv1).Position)("lemburdep") = 0
                    dv1(Me.BindingContext(dv1).Position)("jharian") = 0
                    dv1(Me.BindingContext(dv1).Position)("jmltelat") = 0
                    dv1(Me.BindingContext(dv1).Position)("tamblembur") = 0
                    dv1(Me.BindingContext(dv1).Position)("tamb1") = 0
                    dv1(Me.BindingContext(dv1).Position)("tamb2") = 0
                    dv1(Me.BindingContext(dv1).Position)("tambisti") = 0

                ElseIf e.Value = "MENGINAP" Then

                    Dim tanggals As String = dv1(Me.BindingContext(dv1).Position)("tanggal").ToString
                    Dim sqls As String = String.Format("select userid from ms_inout where userid in (select idmesin from ms_karyawan where nip='{0}') " & _
                    "and CONVERT(date,checktime)='{1}'", dv1(Me.BindingContext(dv1).Position)("nip").ToString, convert_date_to_eng(tanggals))
                    Dim cmds As OleDbCommand = New OleDbCommand(sqls, cnm)
                    Dim drds As OleDbDataReader = cmds.ExecuteReader

                    If drds.Read Then
                        If IsNumeric(drds(0).ToString) Then
                            MsgBox("Ada absen dikaryawan tersebut, status tidak bisa dirubah ke menginap..", vbOKOnly + vbInformation, "Informasi")
                            load_grid_pernip(dv1(Me.BindingContext(dv1).Position)("id").ToString)
                            Return
                        End If
                    End If
                    drds.Close()

                    'Dim sqljamkerja As String = "select kode,LEFT (CONVERT (VARCHAR, CONVERT (time(0), ms_jamkerja.jammasuk), 108), 5) as jammasuk, " & _
                    '"LEFT (CONVERT (VARCHAR, CONVERT (time(0), ms_jamkerja.jampulang), 108), 5) as jampulang " & _
                    '"from ms_jamkerja where jammasuk>='07:57' and jammasuk<='08:05'"
                    'Dim cmdjamkerja As OleDbCommand = New OleDbCommand(sqljamkerja, cnm)
                    'Dim drdjamkerja As OleDbDataReader = cmdjamkerja.ExecuteReader
                    'If drdjamkerja.Read Then
                    '    If drdjamkerja(0).ToString.Length > 0 Then

                    '        jadwalmasuk = String.Format("{0} {1}", convert_date_to_eng(ttgl.EditValue), drdjamkerja("jammasuk").ToString)
                    '        jadwalkeluar = String.Format("{0} {1}", convert_date_to_eng(ttgl.EditValue), drdjamkerja("jampulang").ToString)

                    '        realmasuk = String.Format("{0} {1}", convert_date_to_eng(ttgl.EditValue), drdjamkerja("jammasuk").ToString)
                    '        realkeluar = String.Format("{0} {1}", convert_date_to_eng(ttgl.EditValue), drdjamkerja("jampulang").ToString)

                    '        namashift = drdjamkerja("kode").ToString

                    '    End If
                    'End If
                    'drdjamkerja.Close()

                    jadwalmasuk = String.Format("{0} {1}", convert_date_to_eng(tanggals), "08:00:00")
                    jadwalkeluar = String.Format("{0} {1}", convert_date_to_eng(tanggals), "16:00:00")

                    realmasuk = String.Format("{0} {1}", convert_date_to_eng(tanggals), "08:00:00")
                    realkeluar = String.Format("{0} {1}", convert_date_to_eng(tanggals), "16:00:00")

                    namashift = "SHIFT-I"

                    save_editrow_(cnm, "MENGINAP", dv1(Me.BindingContext(dv1).Position)("skalk"), 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, tuangmakan_inap, 0, jmlharian, 0, namashift, idhadir, False, "-")

                    load_grid_pernip(dv1(Me.BindingContext(dv1).Position)("id").ToString)

                Else

                    Dim sqlkal As String = String.Format("update tr_hadir set uangmakan=0,skalk={0} where id={1}", stkalk, idhadir)
                    Using cmdkal As OleDbCommand = New OleDbCommand(sqlkal, cnm)
                        cmdkal.ExecuteNonQuery()
                    End Using

                End If


                '' update status
                Dim sqlupstat As String = String.Format("update tr_hadir set stat='{0}' where id={1}", e.Value, idhadir)
                Using cmdupstat As OleDbCommand = New OleDbCommand(sqlupstat, cnm)
                    cmdupstat.ExecuteNonQuery()
                End Using


                '' update stat_lembur ''

                If e.Value = "HADIR" Or e.Value = "MENGINAP" Then
                    If jmllembur_dep <> 0.0 Then

                        '' cek tanggal
                        cek_hari(False, dv1(Me.BindingContext(dv1).Position)("tanggal").ToString)
                        ''

                        Dim libur_ok As Integer
                        If libur_ht.Equals("LIBUR") Then
                            libur_ok = 1
                        Else
                            libur_ok = 0
                        End If

                        If Not (liburnormal = 1) Then

                            Dim harilibur As String = ""

                            Dim sqllibur As String = String.Format("select namahari from ms_karyawan2 where nip='{0}' and tanggal1<='{1}' and tanggal1>='{1}'", dv1(Me.BindingContext(dv1).Position)("nip").ToString, convert_date_to_eng(dv1(Me.BindingContext(dv1).Position)("tanggal").ToString))
                            Dim cmdlibur As OleDbCommand = New OleDbCommand(sqllibur, cnm)
                            Dim drdlibur As OleDbDataReader = cmdlibur.ExecuteReader

                            If drdlibur.Read Then
                                harilibur = drdlibur(0).ToString
                            End If
                            drdlibur.Close()

                            Dim inilibur As Integer = 0
                            If harilibur.Equals(libur_ht) Then
                                libur_ok = 1
                            End If

                        End If

                        Dim stat_lembur As String = ""
                        If libur_ok = 1 Then
                            stat_lembur = "TDK TERJADWAL"
                        Else
                            stat_lembur = "TERJADWAL"
                        End If

                        Dim sqlup_lembur As String = String.Format("update tr_hadir set stat_lmbr='{0}' where id={1}", stat_lembur, idhadir)
                        Using cmdup_lembur As OleDbCommand = New OleDbCommand(sqlup_lembur, cnm)
                            cmdup_lembur.ExecuteNonQuery()
                        End Using

                        dv1(Me.BindingContext(dv1).Position)("stat_lmbr") = stat_lembur

                    Else

                        Dim sqlup_lembur2 As String = String.Format("update tr_hadir set stat_lmbr='{0}' where id={1}", "-", idhadir)
                        Using cmdup_lembur2 As OleDbCommand = New OleDbCommand(sqlup_lembur2, cnm)
                            cmdup_lembur2.ExecuteNonQuery()
                        End Using

                        dv1(Me.BindingContext(dv1).Position)("stat_lmbr") = "-"

                    End If

                Else

                    Dim sqlup_lembur2 As String = String.Format("update tr_hadir set stat_lmbr='{0}' where id={1}", "-", idhadir)
                    Using cmdup_lembur2 As OleDbCommand = New OleDbCommand(sqlup_lembur2, cnm)
                        cmdup_lembur2.ExecuteNonQuery()
                    End Using

                    dv1(Me.BindingContext(dv1).Position)("stat_lmbr") = "-"

                End If

                ''


            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
            Finally
                If Not cnm Is Nothing Then
                    If cnm.State = ConnectionState.Open Then
                        cnm.Close()
                    End If
                End If
            End Try

            '    load_kemungkinan_salah()


        ElseIf e.Column.FieldName.Equals("tamblembur") Then

            Dim stamblembur As Double = e.Value

            If Integer.Parse(dv1(Me.BindingContext(dv1).Position)("skalk").ToString) = 0 Then
                dv1(Me.BindingContext(dv1).Position)("tamblembur") = 0
                Return
            End If

            Dim cnt As OleDbConnection = Nothing
            Try

                cnt = New OleDbConnection
                cnt = Clsmy.open_conn

                Dim idhadir As Integer = dv1(Me.BindingContext(dv1).Position)("id").ToString
                Dim nip_cnt As String = dv1(Me.BindingContext(dv1).Position)("nip").ToString
                Dim kdshift As String = dv1(Me.BindingContext(dv1).Position)("kd_shift").ToString

                Dim jd1 As String = dv1(Me.BindingContext(dv1).Position)("jadwalmasuk").ToString
                Dim jd2 As String = dv1(Me.BindingContext(dv1).Position)("jadwalpulang").ToString

                Dim ja As String = dv1(Me.BindingContext(dv1).Position)("jammasuk").ToString
                Dim jb As String = dv1(Me.BindingContext(dv1).Position)("jampulang").ToString
                Dim stat As String = dv1(Me.BindingContext(dv1).Position)("stat").ToString

                Dim sqlawal As String = "select * from ms_awalshift"
                Dim cmdawal As OleDbCommand = New OleDbCommand(sqlawal, cnt)
                Dim drdawal As OleDbDataReader = cmdawal.ExecuteReader
                If drdawal.Read Then
                    awalshiftoto = drdawal("namahari").ToString.ToUpper
                    jenishitung_lembur = Integer.Parse(drdawal("jnis_hit_harian").ToString)
                End If
                drdawal.Close()

                Dim sqlh As String = String.Format("select ms_golongan.harian,ms_golongan.jenislembur, " & _
                "ms_karyawan.gapok, ms_karyawan.tunj_jabatan, ms_karyawan.depart, ms_golongan.laki2, " & _
                "ms_golongan.perempuan, tr_hadir.jamlembur, tr_hadir.totlembur, " & _
                "tr_hadir.jam1, tr_hadir.jam2, tr_hadir.jam3, tr_hadir.jam4,ms_karyawan.jniskelamin,ms_karyawan.liburnormal,tr_hadir.stat_hari,ms_karyawan.lembur_perjam,ms_karyawan.lembur_perjam " & _
                "from tr_hadir inner join ms_golongan on tr_hadir.kdgol=ms_golongan.kode " & _
                "inner join ms_karyawan on ms_karyawan.nip=tr_hadir.nip " & _
                "where tr_hadir.id={0}", idhadir)
                Dim cmdh As OleDbCommand = New OleDbCommand(sqlh, cnt)
                Dim drdh As OleDbDataReader = cmdh.ExecuteReader

                Dim sharian As Integer = 0
                Dim sjenislembur As String = ""
                Dim sgapok As Double = 0
                Dim stunj_jab As Double = 0
                Dim sdepart As String = ""
                Dim slaki2 As Double = 0
                Dim sperempuan As Double = 0
                Dim sjamlembur As Double = 0
                Dim stotlembur As Double = 0
                Dim sjam1 As Double = 0
                Dim sjam2 As Double = 0
                Dim sjam3 As Double = 0
                Dim sjam4 As Double = 0
                Dim sjeniskelamin As String

                If drdh.Read Then

                    sharian = Integer.Parse(drdh("harian").ToString)
                    sjenislembur = drdh("jenislembur").ToString
                    sgapok = Double.Parse(drdh("gapok").ToString)
                    stunj_jab = Double.Parse(drdh("tunj_jabatan").ToString)
                    sdepart = drdh("depart").ToString
                    sjeniskelamin = drdh("jniskelamin").ToString
                    slaki2 = Double.Parse(drdh("laki2").ToString)
                    sperempuan = Double.Parse(drdh("perempuan").ToString)
                    sjamlembur = Double.Parse(drdh("jamlembur").ToString)
                    stotlembur = Double.Parse(drdh("totlembur").ToString)
                    sjam1 = Double.Parse(drdh("jam1").ToString)
                    sjam2 = Double.Parse(drdh("jam2").ToString)
                    sjam3 = Double.Parse(drdh("jam3").ToString)
                    sjam4 = Double.Parse(drdh("jam4").ToString)
                    liburnormal = Integer.Parse(drdh("liburnormal").ToString)

                    Dim nilharian As Double
                    If sjeniskelamin.Equals("Laki - Laki") Then
                        nilharian = slaki2
                    Else
                        nilharian = sperempuan
                    End If

                    If Double.Parse(drdh("lembur_perjam").ToString) = 0 Then
                        lembur_perjam = lembur_perjam_cek(sharian, sjenislembur, sgapok, jenishitung_lembur, stunj_jab, sdepart, nilharian)
                    Else
                        lembur_perjam = Double.Parse(drdh("lembur_perjam").ToString)
                    End If


                    '' cek tanggal
                    cek_hari(False, dv1(Me.BindingContext(dv1).Position)("tanggal").ToString)
                    ''

                    If drdh("stat_hari").ToString.Length > 0 Then
                        jenislibur_hit = drdh("stat_hari").ToString
                    End If

                    mulai_hitung2(idhadir, kdshift, jd1, jd2, ja, jb, stat, cnt, e.Value)

                End If
                drdh.Close()

            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
            Finally
                If Not cnt Is Nothing Then
                    If cnt.State = ConnectionState.Open Then
                        cnt.Close()
                    End If
                End If
            End Try

            load_grid_pernip(dv1(Me.BindingContext(dv1).Position)("id").ToString)

        ElseIf e.Column.FieldName.Equals("kd_shift") Then

            If e.Value = "-" Then

                Dim cn0 As OleDbConnection = Nothing
                Try

                    cn0 = New OleDbConnection
                    cn0 = Clsmy.open_conn


                    '' balikin sebelumnya

                    Dim sqlsebelumnya As String = String.Format("select jammasuk,jampulang,jadwalmasuk,jadwalpulang from tr_hadir where id='{0}'", dv1(Me.BindingContext(dv1).Position)("id").ToString)
                    Dim cmdsebelumnya As OleDbCommand = New OleDbCommand(sqlsebelumnya, cn0)
                    Dim drdsebelumnya As OleDbDataReader = cmdsebelumnya.ExecuteReader

                    Dim masuksebelum2 As String = ""
                    Dim keluarsebelum2 As String = ""
                    Dim jadwalmasuk As String = ""
                    Dim jadwalpulang As String = ""

                    If drdsebelumnya.Read Then

                        If IsDate(drdsebelumnya("jammasuk").ToString) Then
                            masuksebelum2 = drdsebelumnya("jammasuk").ToString
                        End If

                        If IsDate(drdsebelumnya("jampulang").ToString) Then
                            keluarsebelum2 = drdsebelumnya("jampulang").ToString
                        End If

                        jadwalmasuk = drdsebelumnya("jadwalmasuk").ToString
                        jadwalpulang = drdsebelumnya("jadwalpulang").ToString

                    End If
                    drdsebelumnya.Close()

                    If masuksebelum2.Length > 0 Then

                        Dim sqlup_msShift As String = ""

                        sqlup_msShift = String.Format("update ms_inout set skalk=0,tgl_kalk=null where checktime>='{0}' and checktime<='{1}' and userid in (select idmesin from ms_karyawan where nip='{2}')", _
                                                                          convert_datetime_to_eng(masuksebelum2), convert_datetime_to_eng(jadwalpulang), dv1(Me.BindingContext(dv1).Position)("nip").ToString)

                        Using cmdup_msShift As OleDbCommand = New OleDbCommand(sqlup_msShift, cn0)
                            cmdup_msShift.ExecuteNonQuery()
                        End Using

                    End If

                    If keluarsebelum2.Length > 0 Then

                        Dim kls As DateTime = keluarsebelum2
                        If TimeValue(kls) < "23:59:59" Then
                            kls = kls.AddMinutes(1)
                        End If

                        Dim sqlup_msShift As String = String.Format("update ms_inout set skalk=0,tgl_kalk=null where checktime>='{0}' and checktime<='{1}' and userid in (select idmesin from ms_karyawan where nip='{2}')", _
                                                                          convert_datetime_to_eng(jadwalmasuk), convert_datetime_to_eng(kls), dv1(Me.BindingContext(dv1).Position)("nip").ToString)

                        Using cmdup_msShift As OleDbCommand = New OleDbCommand(sqlup_msShift, cn0)
                            cmdup_msShift.ExecuteNonQuery()
                        End Using

                    End If

                    ''

                    Dim sqldel2 As String = String.Format("delete from tr_hadir2 where id2={0}", dv1(Me.BindingContext(dv1).Position)("id").ToString)
                    Using cmddel2 As OleDbCommand = New OleDbCommand(sqldel2, cn0)
                        cmddel2.ExecuteNonQuery()
                    End Using

                    Dim sqlupdate1 As String = String.Format("update tr_hadir set jammasuk=null,jampulang=null,stat='LAIN-LAIN',stelat=0,spulangcpat=0, " & _
                    "jmltelat=0,skalk=0,jamkerja=0,jamlembur=0,jmlhasil=0,lemburperjam=0,totlembur=0, " & _
                    "jam1=0,jam2=0,jam3=0,jam4=0,hasilper=0,tothasil=0,tambmakan=0,step=0,jharian=0,jnisabsen=1, " & _
                    "jadwalmasuk=null,jadwalpulang=null,tamblembur=0,tamb1=0,tamb2=0,kd_shift='-',uangmakan=0,tamb_istirahat=0,stat_lmbr='-' " & _
                    "where id={0}", dv1(Me.BindingContext(dv1).Position)("id").ToString)

                    Using cmdupdate1 As OleDbCommand = New OleDbCommand(sqlupdate1, cn0)
                        cmdupdate1.ExecuteNonQuery()
                    End Using

                    dv1(Me.BindingContext(dv1).Position)("kd_shift") = "-"
                    dv1(Me.BindingContext(dv1).Position)("skalk") = 0
                    dv1(Me.BindingContext(dv1).Position)("stelat") = 0
                    dv1(Me.BindingContext(dv1).Position)("spulangcpat") = 0
                    dv1(Me.BindingContext(dv1).Position)("stat") = "LAIN-LAIN"
                    dv1(Me.BindingContext(dv1).Position)("stat_lmbr") = "-"
                    dv1(Me.BindingContext(dv1).Position)("jamlembur") = 0
                    dv1(Me.BindingContext(dv1).Position)("lemburperjam") = 0
                    dv1(Me.BindingContext(dv1).Position)("jammasuk") = DBNull.Value
                    dv1(Me.BindingContext(dv1).Position)("jampulang") = DBNull.Value
                    dv1(Me.BindingContext(dv1).Position)("jadwalmasuk") = DBNull.Value
                    dv1(Me.BindingContext(dv1).Position)("jadwalpulang") = DBNull.Value
                    dv1(Me.BindingContext(dv1).Position)("totlembur") = 0
                    dv1(Me.BindingContext(dv1).Position)("hasilper") = 0
                    dv1(Me.BindingContext(dv1).Position)("jmlhasil") = 0
                    dv1(Me.BindingContext(dv1).Position)("tothasil") = 0
                    dv1(Me.BindingContext(dv1).Position)("uangmakan") = 0
                    dv1(Me.BindingContext(dv1).Position)("tambmakan") = 0
                    dv1(Me.BindingContext(dv1).Position)("jamkerja") = 0
                    dv1(Me.BindingContext(dv1).Position)("jam1") = 0
                    dv1(Me.BindingContext(dv1).Position)("jam2") = 0
                    dv1(Me.BindingContext(dv1).Position)("jam3") = 0
                    dv1(Me.BindingContext(dv1).Position)("jam4") = 0
                    dv1(Me.BindingContext(dv1).Position)("lemburdep") = 0
                    dv1(Me.BindingContext(dv1).Position)("jharian") = 0
                    dv1(Me.BindingContext(dv1).Position)("jmltelat") = 0
                    dv1(Me.BindingContext(dv1).Position)("tamblembur") = 0
                    dv1(Me.BindingContext(dv1).Position)("tamb1") = 0
                    dv1(Me.BindingContext(dv1).Position)("tamb2") = 0
                    dv1(Me.BindingContext(dv1).Position)("tambisti") = 0

                    load_kemungkinan_salah(cn0)

                Catch ex As Exception
                    MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
                Finally
                    If Not cn0 Is Nothing Then
                        If cn0.State = ConnectionState.Open Then
                            cn0.Close()
                        End If
                    End If
                End Try


                Exit Sub

            End If

            Dim kd_shift As String = dv1(Me.BindingContext(dv1).Position)("kd_shift").ToString
            Dim noid As String = dv1(Me.BindingContext(dv1).Position)("id").ToString
            Dim nip As String = dv1(Me.BindingContext(dv1).Position)("nip").ToString
            Dim tanggal As String = dv1(Me.BindingContext(dv1).Position)("tanggal").ToString

            'Dim awal_cek As String = ""
            'Dim akhir_cek As String = ""

            Dim masuksebelum As String = "12/12/2007 11:11:11"
            Dim keluarsebelum As String = "12/12/2007 11:11:11"
            Dim jadwalmasuk_x As String = ""
            Dim jadwalpulang_x As String = ""
            Dim shift_sebelum As String = ""

            Dim cn As OleDbConnection = Nothing
            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn


                '' balikin sebelumnya

                Dim sqlsebelumnya As String = String.Format("select kd_shift,jammasuk,jampulang,stat_hari,jadwalmasuk,jadwalpulang from tr_hadir where id='{0}'", dv1(Me.BindingContext(dv1).Position)("id").ToString)
                Dim cmdsebelumnya As OleDbCommand = New OleDbCommand(sqlsebelumnya, cn)
                Dim drdsebelumnya As OleDbDataReader = cmdsebelumnya.ExecuteReader

                If drdsebelumnya.Read Then

                    If drdsebelumnya("kd_shift").ToString.Length = 0 Or drdsebelumnya("kd_shift").ToString.Equals("-") Then

                        masuksebelum = ""
                        keluarsebelum = ""
                        shift_sebelum = ""
                        jenislibur_hit = ""
                        jadwalmasuk_x = ""
                        jadwalpulang_x = ""

                    Else


                        masuksebelum = IIf(drdsebelumnya("jammasuk").ToString.Trim.Length = 0, "", drdsebelumnya("jammasuk").ToString)
                        keluarsebelum = IIf(drdsebelumnya("jampulang").ToString.Trim.Length = 0, "", drdsebelumnya("jampulang").ToString)
                        shift_sebelum = drdsebelumnya("kd_shift").ToString
                        jenislibur_hit = drdsebelumnya("stat_hari").ToString

                        If libur_ht = "" Then
                            If jenislibur_hit.Substring(0, 5).ToUpper = "LIBUR" Then
                                libur_ht = "LIBUR"
                            Else
                                libur_ht = "KERJA"
                            End If
                        End If

                        jadwalmasuk_x = drdsebelumnya("jadwalmasuk").ToString
                        jadwalpulang_x = drdsebelumnya("jadwalpulang").ToString

                        End If

                End If
                    drdsebelumnya.Close()

                    If masuksebelum.Length > 0 Then

                        Dim sqlup_msShift As String = ""

                        sqlup_msShift = String.Format("update ms_inout set skalk=0,tgl_kalk=null where checktime>='{0}' and checktime<='{1}' and userid in (select idmesin from ms_karyawan where nip='{2}')", _
                                                                          convert_datetime_to_eng(masuksebelum), convert_datetime_to_eng(jadwalpulang_x), dv1(Me.BindingContext(dv1).Position)("nip").ToString)

                        Using cmdup_msShift As OleDbCommand = New OleDbCommand(sqlup_msShift, cn)
                            cmdup_msShift.ExecuteNonQuery()
                        End Using

                    End If

                    If keluarsebelum.Length > 0 Then

                        Dim kls As DateTime = keluarsebelum
                        If TimeValue(kls) < "23:59:59" Then
                            kls = kls.AddMinutes(1)
                        End If

                        Dim sqlup_msShift As String = String.Format("update ms_inout set skalk=0,tgl_kalk=null where checktime>='{0}' and checktime<='{1}' and userid in (select idmesin from ms_karyawan where nip='{2}')", _
                                                                          convert_datetime_to_eng(jadwalmasuk_x), convert_datetime_to_eng(kls), dv1(Me.BindingContext(dv1).Position)("nip").ToString)

                        Using cmdup_msShift As OleDbCommand = New OleDbCommand(sqlup_msShift, cn)
                            cmdup_msShift.ExecuteNonQuery()
                        End Using

                    End If

            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
                Return
            Finally

                If Not cn Is Nothing Then
                    If cn.State = ConnectionState.Open Then
                        cn.Close()
                    End If
                End If
            End Try

            Dim tanggal_s As String = dv1(Me.BindingContext(dv1).Position)("tanggal").ToString

            open_wait()

            'putar_inout_(masuksebelum, keluarsebelum, noid, nip, shift_sebelum, kd_shift)

            mulai_hitung(tanggal_s, tanggal_s, nip, kd_shift, noid, masuksebelum, keluarsebelum, jadwalmasuk, jadwalkeluar)

            load_grid_pernip(noid)

            '' load_kemungkinan_salah()

            close_wait()

        ElseIf e.Column.FieldName.Equals("stat_hari") Then

            Dim kd_shift As String = dv1(Me.BindingContext(dv1).Position)("kd_shift").ToString
            Dim noid As String = dv1(Me.BindingContext(dv1).Position)("id").ToString
            Dim nip As String = dv1(Me.BindingContext(dv1).Position)("nip").ToString
            Dim tanggal As String = dv1(Me.BindingContext(dv1).Position)("tanggal").ToString

            Dim masuksebelum As String = ""
            Dim keluarsebelum As String = ""
            Dim shift_sebelum As String = ""
            Dim jadwalmasuk As String = ""
            Dim jadwalpulang As String = ""

            Dim cn As OleDbConnection = Nothing
            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn


                '' balikin sebelumnya

                Dim sqlsebelumnya As String = String.Format("select kd_shift,jammasuk,jampulang,stat_hari,jadwalmasuk,jadwalpulang from tr_hadir where id='{0}'", dv1(Me.BindingContext(dv1).Position)("id").ToString)
                Dim cmdsebelumnya As OleDbCommand = New OleDbCommand(sqlsebelumnya, cn)
                Dim drdsebelumnya As OleDbDataReader = cmdsebelumnya.ExecuteReader

                If drdsebelumnya.Read Then
                    masuksebelum = IIf(drdsebelumnya("jammasuk").ToString.Trim.Length = 0, "", drdsebelumnya("jammasuk").ToString)
                    keluarsebelum = IIf(drdsebelumnya("jampulang").ToString.Trim.Length = 0, "", drdsebelumnya("jampulang").ToString)
                    shift_sebelum = drdsebelumnya("kd_shift").ToString
                    jenislibur_hit = e.Value 'drdsebelumnya("stat_hari").ToString

                    jadwalmasuk = drdsebelumnya("jammasuk").ToString
                    jadwalpulang = drdsebelumnya("jampulang").ToString

                End If
                drdsebelumnya.Close()

                If masuksebelum.Trim.Length > 0 Then

                    Dim sqlup_msShift As String = String.Format("update ms_inout set skalk=0,tgl_kalk=null where checktime>='{0}' and checktime<='{1}' and userid in (select idmesin from ms_karyawan where nip='{2}')", _
                                                              convert_datetime_to_eng(masuksebelum), convert_datetime_to_eng(jadwalpulang), nip)


                    Using cmdup_msShift As OleDbCommand = New OleDbCommand(sqlup_msShift, cn)
                        cmdup_msShift.ExecuteNonQuery()
                    End Using

                End If

                If keluarsebelum.Trim.Length > 0 Then


                    Dim kls As DateTime = keluarsebelum

                    If TimeValue(kls) < "23:59:59" Then
                        kls = kls.AddMinutes(1)
                    End If

                    Dim sqlup_msShift As String = String.Format("update ms_inout set skalk=0,tgl_kalk=null where checktime>='{0}' and checktime<='{1}' and userid in (select idmesin from ms_karyawan where nip='{2}')", _
                                                              convert_datetime_to_eng(jadwalmasuk), convert_datetime_to_eng(kls), nip)

                    Using cmdup_msShift As OleDbCommand = New OleDbCommand(sqlup_msShift, cn)
                        cmdup_msShift.ExecuteNonQuery()
                    End Using

                End If

            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
                Return
            Finally

                If Not cn Is Nothing Then
                    If cn.State = ConnectionState.Open Then
                        cn.Close()
                    End If
                End If
            End Try

            Dim tanggal_s As String = dv1(Me.BindingContext(dv1).Position)("tanggal").ToString

            open_wait()

            mulai_hitung(tanggal_s, tanggal_s, nip, kd_shift, noid, masuksebelum, keluarsebelum, jadwalmasuk, jadwalpulang)

        
            load_grid_pernip(noid)

            close_wait()

        End If

    End Sub

    Private Sub inout_rubah(ByVal userid As Integer, ByVal tglkalk As Date, ByVal tglin As DateTime, ByVal tglinout As DateTime, ByVal cn As OleDbConnection)

        If TimeValue(tglinout) <= "23:59:59" Then
            tglinout = tglinout.AddMinutes(1)
        End If

        Dim sql As String = String.Format("update ms_inout set skalk=1,tgl_kalk='{0}' where userid={1} and checktime>='{2}' and checktime<='{3}' ", convert_date_to_eng(tglkalk), userid, convert_datetime_to_eng(tglin), convert_datetime_to_eng(tglinout))

        Using cmd As OleDbCommand = New OleDbCommand(sql, cn)
            cmd.ExecuteNonQuery()
        End Using

    End Sub
    Private Sub putar_inout_(ByVal masuksebelum As String, ByVal keluarsebelum As String, ByVal id As Integer, ByVal nip As String, ByVal kdshift_awal As String, ByVal kdshift_akhir As String)

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn



            If Not masuksebelum = "12/12/2007 11:11:11" And Not keluarsebelum = "12/12/2007 11:11:11" Then

                If kdshift_awal <> kdshift_akhir Then

                    'If masuksebelum < realmasuk Then
                    '    masuksebelum = realmasuk
                    'ElseIf masuksebelum < realkeluar And keluarsebelum > realkeluar Then
                    '    masuksebelum = realkeluar
                    'End If


                    'If keluarsebelum > realkeluar Then
                    '    keluarsebelum = realkeluar
                    'End If

                    If masuksebelum.Trim.Length > 0 And Not masuksebelum = "12/12/2007 11:11:11" Then

                        Dim sqlup_msShift As String = String.Format("update ms_inout set skalk=0,tgl_kalk=null where checktime>='{0}' and checktime<='{1}' and userid in (select idmesin from ms_karyawan where nip='{2}')", _
                                                                  convert_datetime_to_eng(DateValue(masuksebelum).AddMinutes(-3)), convert_datetime_to_eng(DateTime.Parse(masuksebelum).AddMinutes(3)), nip)

                        If TimeValue(masuksebelum) >= "23:57:00" Then
                            sqlup_msShift = String.Format("update ms_inout set skalk=0,tgl_kalk=null where checktime>='{0}' and checktime<='{1}' and userid in (select idmesin from ms_karyawan where nip='{2}')", _
                                                                  convert_datetime_to_eng(DateValue(masuksebelum).AddMinutes(-3)), convert_datetime_to_eng(DateTime.Parse(masuksebelum).AddMinutes(2)), nip)
                        End If


                        Using cmdup_msShift As OleDbCommand = New OleDbCommand(sqlup_msShift, cn)
                            cmdup_msShift.ExecuteNonQuery()
                        End Using

                    End If

                    If keluarsebelum.Trim.Length > 0 And Not keluarsebelum = "12/12/2007 11:11:11" Then

                        Dim sqlup_msShift As String = String.Format("update ms_inout set skalk=0,tgl_kalk=null where checktime>='{0}' and checktime<='{1}' and userid in (select idmesin from ms_karyawan where nip='{2}')", _
                                                                  convert_datetime_to_eng(DateValue(keluarsebelum).AddMinutes(-3)), convert_datetime_to_eng(DateTime.Parse(keluarsebelum).AddMinutes(3)), nip)

                        If TimeValue(keluarsebelum) >= "23:57:00" Then
                            sqlup_msShift = String.Format("update ms_inout set skalk=0,tgl_kalk=null where checktime>='{0}' and checktime<='{1}' and userid in (select idmesin from ms_karyawan where nip='{2}')", _
                                                                  convert_datetime_to_eng(DateValue(keluarsebelum).AddMinutes(-3)), convert_datetime_to_eng(DateTime.Parse(keluarsebelum).AddMinutes(2)), nip)
                        End If

                        Using cmdup_msShift As OleDbCommand = New OleDbCommand(sqlup_msShift, cn)
                            cmdup_msShift.ExecuteNonQuery()
                        End Using

                    End If

                End If

                'load_grid_pernip(id)

            Else

                'load_grid()

            End If

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

    Private Sub putar_inout2_(ByVal cn As OleDbConnection, ByVal tanggal_kalk As DateTime, ByVal tanggal_kalk2 As DateTime, ByVal jam1_param As DateTime, ByVal jam2_param As DateTime, _
                             ByVal kdgol As String, ByVal depart As String, ByVal nip As String)

        Dim sqlinout As String = String.Format("update ms_inout set skalk=0,tgl_kalk=null where userid in (select idmesin from ms_karyawan where kdgol='{0}'", kdgol)

        If Not depart = "ALL" Then
            sqlinout = String.Format("{0} and depart='{1}'", sqlinout, depart)
        End If

        If Not nip = "ALL" Then
            sqlinout = String.Format("{0} and nip='{1}'", sqlinout, nip)
        End If

        sqlinout = String.Format("{0})", sqlinout)

        Dim sqlinout2 As String = sqlinout

        If jam1_param = Nothing Then
            sqlinout = String.Format("{0} and tgl_kalk>='{1}' " & _
            "and tgl_kalk<='{2}'", sqlinout, _
            String.Format("{0}", convert_date_to_eng(tanggal_kalk)), _
            String.Format("{0}", convert_date_to_eng(tanggal_kalk2)))

            sqlinout2 = String.Format("{0} and convert(date,checktime)>='{1}' " & _
            "and convert(date,checktime)<='{2}' and tgl_kalk is null", sqlinout2, _
            String.Format("{0}", convert_date_to_eng(tanggal_kalk)), _
            String.Format("{0}", convert_date_to_eng(tanggal_kalk2)))

            Using cmd2 As OleDbCommand = New OleDbCommand(sqlinout2, cn)
                cmd2.ExecuteNonQuery()
            End Using

        Else

            sqlinout = String.Format("{0} and CONVERT(datetime, " & _
            "CONVERT(datetime, CAST(CONVERT(date, checktime)  " & _
            "AS varchar) + ' ' + CAST(LEFT(CONVERT(VARCHAR,  " & _
            "CONVERT(time(0), checktime), 108), 5) AS varchar)))>='{1}' " & _
            "and  " & _
            "CONVERT(datetime, " & _
            "CONVERT(datetime, CAST(CONVERT(date, checktime)  " & _
            "AS varchar) + ' ' + CAST(LEFT(CONVERT(VARCHAR,  " & _
            "CONVERT(time(0), checktime), 108), 5) AS varchar)))<='{2}'", sqlinout, _
            convert_datetime_to_eng(jam1_param), convert_datetime_to_eng(jam2_param))

        End If

        Using cmdinout As OleDbCommand = New OleDbCommand(sqlinout, cn)
            cmdinout.ExecuteNonQuery()
        End Using

    End Sub

    Private Sub XtraTabControl1_Selected(sender As Object, e As DevExpress.XtraTab.TabPageEventArgs) Handles XtraTabControl1.Selected
        If e.PageIndex = 1 Then
            LabelControl8.Enabled = False
            load_detailmesin()
        Else
            LabelControl8.Enabled = True
        End If
    End Sub

    Private Sub rbutton_hsl_Click(sender As Object, e As System.EventArgs) Handles rbutton_hsl.ButtonClick

        Dim iddia As Integer = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("id").ToString)
        Dim nama As String = dv1(Me.BindingContext(dv1).Position)("nama").ToString
        Dim tanggals As String = dv1(Me.BindingContext(dv1).Position)("tanggal").ToString
        Dim shifts As String = dv1(Me.BindingContext(dv1).Position)("kd_shift").ToString
        Dim kodegol As String = cb_gol.EditValue

        If shifts = "-" Then
            Return
        End If

        Using fgol2 As New fkalk_absen1 With {.StartPosition = FormStartPosition.CenterParent, .iddia = iddia, .nama = nama, .nama_shift = shifts, .tanggal_shift = tanggals, .kodegol = kodegol}
            fgol2.ShowDialog(Me)

            Dim okedata As Boolean = fgol2.get_statdata
            Dim total As Double = fgol2.get_total

            If okedata = True Then
                dv1(Me.BindingContext(dv1).Position)("tothasil") = total

                Dim cn As OleDbConnection = Nothing
                Try
                    cn = New OleDbConnection
                    cn = Clsmy.open_conn

                    Dim sqltot As String = String.Format("update tr_hadir set tothasil={0} where id={1}", Replace(total, ",", "."), iddia)
                    Using cmdtot As OleDbCommand = New OleDbCommand(sqltot, cn)
                        cmdtot.ExecuteNonQuery()
                    End Using

                Catch ex As Exception
                    MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
                Finally
                    If Not cn Is Nothing Then
                        If cn.State = ConnectionState.Open Then
                            cn.Close()
                        End If
                    End If
                End Try


            Else
                ' dv1(bs1.Position)("tothasil") = 0
            End If

        End Using

    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles GridView1.KeyDown

        If e.KeyCode = Keys.F4 Then

            Dim view As GridView = CType(sender, GridView)

            If view.FocusedColumn.FieldName.Equals("tothasil") Then
                rbutton_hsl_Click(sender, Nothing)
            End If

        End If

    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        Dim tanggal As String = dv1(Me.BindingContext(dv1).Position)("tanggal").ToString
        Dim nip As String = dv1(Me.BindingContext(dv1).Position)("nip").ToString
        Dim id As String = dv1(Me.BindingContext(dv1).Position)("id").ToString
        jenislibur_hit = ""

        Using fkar2 As New ftinout2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = True, .position = 0, .tanggal_s = tanggal, .nnip_s = nip}
            fkar2.ShowDialog()

            If fkar2.get_ok = True Then

                open_wait()

                ' mulai_kalk(id, nip, Nothing, Nothing, True, tanggal, tanggal, "")
                mulai_hitung(tanggal, tanggal, nip, "", id, "", "", "", "")

                Dim cn As OleDbConnection = Nothing
                Try

                    cn = New OleDbConnection
                    cn = Clsmy.open_conn

                    Dim sqlc As String = String.Format("select COUNT(jammasuk) as jint from tr_hadir where tanggal='{0}' and nip='{1}' and kdgol='{2}'", convert_date_to_eng(tanggal), nip, cb_gol.EditValue)
                    Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn)
                    Dim drdc As OleDbDataReader = cmdc.ExecuteReader

                    Dim jml1hari As Integer = 0
                    If drdc.Read Then
                        If IsNumeric(drdc(0).ToString) Then
                            jml1hari = Integer.Parse(drdc(0).ToString)
                        End If
                    End If
                    drdc.Close()

                    If jml1hari = 1 Then
                        load_grid_pernip(id)
                    Else
                        load_grid()
                    End If

                Catch ex As Exception
                    close_wait()
                    MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
                Finally

                    close_wait()

                    If Not cn Is Nothing Then
                        If cn.State = ConnectionState.Open Then
                            cn.Close()
                        End If
                    End If
                End Try

            End If

        End Using

    End Sub

    Private Sub cbpeg_Validated(sender As Object, e As EventArgs) Handles cbpeg.Validated

        If isload Then
            Return
        End If

        ' open_wait()

        ' load_grid()
        ' load_kemungkinan_salah()

        ' close_wait()

    End Sub

    Private Sub btlihat_Click(sender As Object, e As EventArgs) Handles btlihat.Click

        XtraTabControl1.SelectedTabPageIndex = 0
        LabelControl8.Enabled = True

        open_wait()

        load_grid()
        '  load_kemungkinan_salah()

        close_wait()

    End Sub

    Private Sub grid1_ProcessGridKey(sender As Object, e As KeyEventArgs) Handles grid1.ProcessGridKey

        If GridView1.IsGroupRow(GridView1.FocusedRowHandle) Then Exit Sub

        If e.KeyCode = Keys.F4 Then
            e.Handled = True
            rbutton_hsl_Click(sender, Nothing)
        End If

    End Sub
    Private Sub LabelControl9_Click(sender As Object, e As EventArgs) Handles LabelControl9.Click
        PopupMenu1.ShowPopup(Control.MousePosition)
    End Sub

    Private Sub LabelControl8_Click(sender As Object, e As EventArgs) Handles LabelControl8.Click

        If Not XtraTabControl1.SelectedTabPageIndex = 0 Then
            Return
        End If

        SimpleButton1_Click(sender, Nothing)
    End Sub

    Private Sub bar_exp_excl2007_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bar_exp_excl2007.ItemClick

        If XtraTabControl1.SelectedTabPageIndex = 0 Then

            If IsNothing(dv1) Then
                Return
            End If

            If dv1.Count <= 0 Then
                Return
            End If

        Else

            If IsNothing(dv2) Then
                Return
            End If

            If dv2.Count <= 0 Then
                Return
            End If

        End If

        Dim fileName As String = ShowSaveFileDialog("Excel 2007", "Microsoft Excel|*.xlsx")

        If fileName = String.Empty Then
            Return
        End If

        If XtraTabControl1.SelectedTabPageIndex = 0 Then
            GridView1.ExportToXlsx(fileName)
        Else
            GridView3.ExportToXlsx(fileName)
        End If



        OpenFile(fileName)

    End Sub

    Private Sub bar_exp_exc_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bar_exp_exc.ItemClick

        If XtraTabControl1.SelectedTabPageIndex = 0 Then

            If IsNothing(dv1) Then
                Return
            End If

            If dv1.Count <= 0 Then
                Return
            End If

        Else

            If IsNothing(dv2) Then
                Return
            End If

            If dv2.Count <= 0 Then
                Return
            End If

        End If

        Dim fileName As String = ShowSaveFileDialog("Excel", "Microsoft Excel|*.xls")

        If fileName = String.Empty Then
            Return
        End If

        If XtraTabControl1.SelectedTabPageIndex = 0 Then
            GridView1.ExportToXls(fileName)
        Else
            GridView3.ExportToXls(fileName)
        End If

        OpenFile(fileName)

    End Sub

    Private Sub bar_exp_htm_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bar_exp_htm.ItemClick

        If XtraTabControl1.SelectedTabPageIndex = 0 Then

            If IsNothing(dv1) Then
                Return
            End If

            If dv1.Count <= 0 Then
                Return
            End If

        Else

            If IsNothing(dv2) Then
                Return
            End If

            If dv2.Count <= 0 Then
                Return
            End If

        End If

        Dim fileName As String = ShowSaveFileDialog("HTML", "HTML Documents|*.html")

        If fileName = String.Empty Then
            Return
        End If

        If XtraTabControl1.SelectedTabPageIndex = 0 Then
            GridView1.ExportToHtml(fileName)
        Else
            GridView3.ExportToHtml(fileName)
        End If


        OpenFile(fileName)

    End Sub

    Private Sub bar_rtf_htm_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bar_exp_rtf.ItemClick

        If XtraTabControl1.SelectedTabPageIndex = 0 Then

            If IsNothing(dv1) Then
                Return
            End If

            If dv1.Count <= 0 Then
                Return
            End If

        Else

            If IsNothing(dv2) Then
                Return
            End If

            If dv2.Count <= 0 Then
                Return
            End If

        End If

        Dim fileName As String = ShowSaveFileDialog("RTF", "RTF Files|*.rtf")

        If fileName = String.Empty Then
            Return
        End If

        If XtraTabControl1.SelectedTabPageIndex = 0 Then
            GridView1.ExportToRtf(fileName)
        Else
            GridView3.ExportToRtf(fileName)
        End If

        OpenFile(fileName)

    End Sub

    Private Sub bar_pdf_htm_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bar_exp_pdf.ItemClick

        If XtraTabControl1.SelectedTabPageIndex = 0 Then

            If IsNothing(dv1) Then
                Return
            End If

            If dv1.Count <= 0 Then
                Return
            End If

        Else

            If IsNothing(dv2) Then
                Return
            End If

            If dv2.Count <= 0 Then
                Return
            End If

        End If

        Dim fileName As String = ShowSaveFileDialog("PDF", "PDF Files|*.pdf")

        If fileName = String.Empty Then
            Return
        End If

        If XtraTabControl1.SelectedTabPageIndex = 0 Then
            GridView1.ExportToPdf(fileName)
        Else
            GridView3.ExportToPdf(fileName)
        End If

        OpenFile(fileName)

    End Sub

    Private Sub bar_pdf_text_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bar_exp_text.ItemClick

        If XtraTabControl1.SelectedTabPageIndex = 0 Then

            If IsNothing(dv1) Then
                Return
            End If

            If dv1.Count <= 0 Then
                Return
            End If

        Else

            If IsNothing(dv2) Then
                Return
            End If

            If dv2.Count <= 0 Then
                Return
            End If

        End If

        Dim fileName As String = ShowSaveFileDialog("Text Files", "Text Files|*.txt")

        If fileName = String.Empty Then
            Return
        End If

        If XtraTabControl1.SelectedTabPageIndex = 0 Then
            GridView1.ExportToText(fileName)
        Else
            GridView3.ExportToText(fileName)
        End If

        OpenFile(fileName)

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub LabelControl10_Click(sender As Object, e As EventArgs) Handles LabelControl10.Click

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        Dim tanggal As String = dv1(Me.BindingContext(dv1).Position)("tanggal").ToString
        Dim nip As String = dv1(Me.BindingContext(dv1).Position)("nip").ToString
        Dim id As String = dv1(Me.BindingContext(dv1).Position)("id").ToString
        jenislibur_hit = ""

        Using fkar2 As New fabsenmanual2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = True, .position = 0, .tanggal_s = tanggal, .nnip_s = nip}
            fkar2.ShowDialog()

            If fkar2.get_ok = True Then

                open_wait()

                ' mulai_kalk(id, nip, Nothing, Nothing, True, tanggal, tanggal, "")
                mulai_hitung(tanggal, tanggal, nip, "", id, "", "", "", "")

                Dim cn As OleDbConnection = Nothing
                Try

                    cn = New OleDbConnection
                    cn = Clsmy.open_conn

                    Dim sqlc As String = String.Format("select COUNT(jammasuk) as jint from tr_hadir where tanggal='{0}' and nip='{1}' and kdgol='{2}'", convert_date_to_eng(tanggal), nip, cb_gol.EditValue)
                    Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn)
                    Dim drdc As OleDbDataReader = cmdc.ExecuteReader

                    Dim jml1hari As Integer = 0
                    If drdc.Read Then
                        If IsNumeric(drdc(0).ToString) Then
                            jml1hari = Integer.Parse(drdc(0).ToString)
                        End If
                    End If
                    drdc.Close()

                    If jml1hari = 1 Then
                        load_grid_pernip(id)
                    Else
                        load_grid()
                    End If

                Catch ex As Exception
                    close_wait()
                    MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
                Finally

                    close_wait()

                    If Not cn Is Nothing Then
                        If cn.State = ConnectionState.Open Then
                            cn.Close()
                        End If
                    End If
                End Try

            End If

        End Using

    End Sub

    Private Sub caktif_CheckedChanged(sender As Object, e As EventArgs) Handles caktif.CheckedChanged

        If isload Then
            Return
        End If

        load_pegawai()

    End Sub

    Private Function cek_periode_ongaji(ByVal cn As OleDbConnection) As Boolean

        Dim hasil As Boolean = True
        

        Dim sql As String = String.Format("select distinct tanggal1,tanggal2 from tr_gaji where calc_by='Mingguan' and tanggal1>='{0}' and tanggal2<='{1}'", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))

        If Not (cb_gol.EditValue = "ALL") Then
            sql = String.Format(" {0} and kdgol='{1}'", sql, cb_gol.EditValue)
        End If

        If Not (cb_depart.EditValue = "ALL") Then
            sql = String.Format(" {0} and depart='{1}'", sql, cb_depart.EditValue)
        End If


        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        Dim ctgl1 As Boolean = True
        Dim ctgl2 As Boolean = True

        While drd.Read

            If DateValue(drd(0).ToString) = DateValue(ttgl.EditValue) Then
                ctgl1 = False
                Exit While
            End If

            If DateValue(drd(1).ToString) = DateValue(ttgl2.EditValue) Then
                ctgl2 = False
                Exit While
            End If

        End While
        drd.Close()

        If ctgl1 = False Or ctgl2 = False Then
            hasil = False
        End If

        Return hasil

    End Function

    Private Function cek_tahunbulanminggu(ByVal cn As OleDbConnection) As Boolean

        Dim hasil As Boolean = True

        Dim sql As String = String.Format("select distinct tahun,bulan,minggu from tr_gaji where not(calc_by='Mingguan') and tanggal1>='{0}' and tanggal2<='{1}'", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))

        If Not (cb_gol.EditValue = "ALL") Then
            sql = String.Format(" {0} and kdgol='{1}'", sql, cb_gol.EditValue)
        End If

        If Not (cb_depart.EditValue = "ALL") Then
            sql = String.Format(" {0} and depart='{1}'", sql, cb_depart.EditValue)
        End If


            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            Dim jtahun As Integer = 0
            Dim jbln As Integer = 0

            While drd.Read

            If Integer.Parse(thn_) = Integer.Parse(drd("tahun").ToString) Then
                jtahun = jtahun + 1
            End If

            If Integer.Parse(bln_) = Integer.Parse(drd("bulan").ToString) Then
                jbln = jbln + 1
            End If

            End While
            drd.Close()

            If jtahun > 1 Or jbln > 1 Then
                hasil = False
            End If

        Return hasil

    End Function


    Private Sub LabelControl11_Click(sender As Object, e As EventArgs) Handles LabelControl11.Click

        If caktif.Checked = True Then Return

        Dim adaerr As Boolean = False

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim hsl_mgu As Boolean = cek_periode_ongaji(cn)

            If hsl_mgu = False Then
                MsgBox("Clean data tidak bisa dilakukan karna periode tesebut sudah masuk ke penggajian", vbOKOnly + vbInformation, "Informasi")
                adaerr = True
                Return
            End If

            Dim hsl_bln As Boolean = cek_tahunbulanminggu(cn)
            If hsl_bln = False Then
                MsgBox("Clean data tidak bisa dilakukan karna periode tesebut sudah masuk ke penggajian", vbOKOnly + vbInformation, "Informasi")
                adaerr = True
                Return
            End If

            If MsgBox("Yakin akan dihapus perhitungan sebelumnya ?proses ini akan mengembalikan perhitungan ke standar program", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.No Then
                adaerr = True
                Return
            End If

            Dim sql_hdr2 As String = String.Format("delete from tr_hadir2 where id2 in (select id from tr_hadir where CONVERT(date,tanggal)>='{0}' and CONVERT(date,tanggal)<='{1}'", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))
            Dim sql_hdr As String = String.Format("delete from tr_hadir where CONVERT(date,tanggal)>='{0}' and CONVERT(date,tanggal)<='{1}'", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))
            Dim sql_sel_hdr As String = String.Format("select nip,convert(date,tanggal) as tanggal,jammasuk,jampulang from tr_hadir where CONVERT(date,tanggal)>='{0}' and CONVERT(date,tanggal)<='{1}'", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))

            If Not (cb_depart.EditValue = "ALL") Then
                sql_hdr2 = String.Format(" {0} and depart='{1}'", sql_hdr2, cb_depart.EditValue)
                sql_hdr = String.Format(" {0} and depart='{1}'", sql_hdr, cb_depart.EditValue)
                sql_sel_hdr = String.Format(" {0} and depart='{1}'", sql_sel_hdr, cb_depart.EditValue)
            End If

            If Not (cbpeg.EditValue = "ALL") Then
                sql_hdr2 = String.Format(" {0} and nip='{1}'", sql_hdr2, cbpeg.EditValue)
                sql_hdr = String.Format(" {0} and nip='{1}'", sql_hdr, cbpeg.EditValue)
                sql_sel_hdr = String.Format(" {0} and nip='{1}'", sql_sel_hdr, cbpeg.EditValue)
            End If

            sql_hdr2 = String.Format(" {0})", sql_hdr2)

            Dim cmd_sel As OleDbCommand = New OleDbCommand(sql_sel_hdr, cn)
            Dim drd_sel As OleDbDataReader = cmd_sel.ExecuteReader

            Dim nip_sel As String
            Dim tgl_sel As String
            Dim jm1_sel As String
            Dim jm2_sel As String

            Dim tgl_a As DateTime

            Dim sql_tglaa As String

            While drd_sel.Read

                nip_sel = drd_sel("nip").ToString
                tgl_sel = drd_sel("tanggal").ToString
                jm1_sel = drd_sel("jammasuk").ToString
                jm2_sel = drd_sel("jampulang").ToString

                If jm2_sel.Length > 0 Then

                    tgl_a = convert_datetime_to_eng(jm2_sel)

                    If TimeValue(tgl_a) <= "23:59:59" Then
                        tgl_a = tgl_a.AddMinutes(1)
                    End If

                    sql_tglaa = String.Format("update ms_inout set skalk=0,tgl_kalk=null " & _
                    "where skalk=1 and checktime>='{0}' and checktime<='{1}' " & _
                    "and userid in (select idmesin from ms_karyawan where nip='{2}')", convert_datetime_to_eng(jm1_sel), convert_datetime_to_eng(tgl_a), nip_sel)

                    Using cmd_ex As OleDbCommand = New OleDbCommand(sql_tglaa, cn)
                        cmd_ex.ExecuteNonQuery()
                    End Using


                Else

                    If jm1_sel.Length > 0 Then

                        tgl_a = convert_datetime_to_eng(jm1_sel)

                        If TimeValue(tgl_a) <= "23:59:59" Then
                            tgl_a = tgl_a.AddMinutes(1)
                        End If

                        sql_tglaa = String.Format("update ms_inout set skalk=0,tgl_kalk=null " & _
                        "where skalk=1 and checktime>='{0}' and checktime<='{1}' " & _
                        "and userid in (select idmesin from ms_karyawan where nip='{2}')", convert_datetime_to_eng(jm1_sel), convert_datetime_to_eng(tgl_a), nip_sel)

                        Using cmd_ex As OleDbCommand = New OleDbCommand(sql_tglaa, cn)
                            cmd_ex.ExecuteNonQuery()
                        End Using

                    End If
                    

                End If


            End While
            drd_sel.Close()

            Using cmd_hdr2 As OleDbCommand = New OleDbCommand(sql_hdr2, cn)
                cmd_hdr2.ExecuteNonQuery()
            End Using

            Using cmd_hdr As OleDbCommand = New OleDbCommand(sql_hdr, cn)
                cmd_hdr.ExecuteNonQuery()
            End Using

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
            adaerr = True
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If


            If adaerr = False Then btview_Click(sender, Nothing)

        End Try

    End Sub

End Class