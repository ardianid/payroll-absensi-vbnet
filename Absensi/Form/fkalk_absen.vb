Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy
Imports DevExpress.XtraGrid.Views.Grid

Public Class fkalk_absen

    Private bs1 As BindingSource
    Private ds As DataSet
    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private dvmanager2 As Data.DataViewManager
    Private dv2 As Data.DataView

    Private dvmanager_pv As Data.DataViewManager
    Private dv_pv As Data.DataView

    Private dvmanager_all As Data.DataViewManager
    Private dv_all As Data.DataView

    Private dvmanager_tdk As Data.DataViewManager
    Private dv_tdk As Data.DataView

    'Private dtgol2 As DataTable
    Private dtgol_umum As DataTable
    Private statkalkhasil As Boolean
    Private dtgol_per As DataTable

    Dim tgl1 As String
    Dim tgl2 As String
    Dim depart As String
    Dim gol As String
    Dim pegawai As String
    Dim stat As Boolean
    Dim skalk As Boolean

    Dim stataksesform As Boolean

    Dim kdshift As String
    Dim jammasuk, jampulang As String
    Dim tolmasuk, tolpulang As String
    Dim awalmasuk, awalkeluar As String
    Dim akhirmasuk, akhirkeluar As String
    Dim tengahmalam As Integer = 0
    Dim hitlembur_libur As Integer = 0
    Dim sjeniskelamin As String = ""

    Dim awalabsen As String = ""
    Dim akhirabsen As String = ""

    Dim hhit_lembur As Integer = 0

    Dim tambmakan As Double = 0
    Dim tamblembur As Double = 0

    Dim nnip_ksebelum As String = ""
    Dim ttgl_ksebelum As String = ""

    Private jenishitung_lembur As Integer = 0

    Private Sub loadGolongan()

        Dim cn As OleDbConnection = Nothing

        Try

            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = "select kode,nama from ms_golongan where tampilgroup=1 and saktif=1"

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

    Private Sub load_Rgolongan()

        Dim ds As DataSet
        Dim cn As OleDbConnection = Nothing

        Try

            Dim sql As String = "select kode,nama from ms_golongan where jnisgol='Manual' and saktif=1"
            Dim sql2 As String = "select * from ms_golongan2 where kode in ( select kode from ms_golongan where jnisgol='Manual' and saktif=1)"

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            ds = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim dt As DataTable = ds.Tables(0)

            rgolongan.DataSource = Nothing
            rgolongan.DataSource = dt

            Dim ds2 As DataSet
            ds2 = New DataSet
            ds2 = Clsmy.GetDataSet(sql2, cn)

            dtgol_umum = New DataTable
            dtgol_umum = ds2.Tables(0)

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

    Private Sub Get_Aksesform()

        Dim rows() As DataRow = dtmenu.Select(String.Format("NAMAFORM='{0}'", Me.Name.ToUpper.ToString))

        If Convert.ToInt16(rows(0)("t_add")) = 1 Then
            tsedit.Enabled = True
            stataksesform = True
        Else
            tsedit.Enabled = False
            stataksesform = False
        End If

        'If Convert.ToInt16(rows(0)("t_edit")) = 1 Then
        '    tsedit.Enabled = True
        'Else
        '    tsedit.Enabled = False
        'End If

        'If Convert.ToInt16(rows(0)("t_del")) = 1 Then
        '    tsdel.Enabled = True
        'Else
        '    tsdel.Enabled = False
        'End If

        'If Convert.ToInt16(rows(0)("t_lap")) = 1 Then

        'Else

        'End If

    End Sub

    Private Function cekhari(ByVal cn As OleDbConnection, ByVal tgl As String) As Boolean

        Dim hasil As Boolean
        hasil = False

        Dim sql As String = String.Format("select hari from ms_kalender where tanggal='{0}'", convert_date_to_eng(tgl))
        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        If drd.Read Then
            If Not drd(0).ToString.Equals("") Then

                Dim sql1 As String = String.Format("select namahari from ms_awalshift where namahari='{0}'", drd(0).ToString)
                Dim cmd1 As OleDbCommand = New OleDbCommand(sql1, cn)
                Dim drd1 As OleDbDataReader = cmd1.ExecuteReader

                If drd1.Read Then
                    If Not drd1(0).ToString.Equals("") Then
                        hasil = True
                    End If
                End If
                drd1.Close()

            End If
        End If
        drd.Close()


        Return hasil

    End Function

    Private Function cekhari_sabtu(ByVal cn As OleDbConnection, ByVal tgl As String) As Boolean

        Dim hasil As Boolean
        hasil = False

        Dim sql As String = String.Format("select hari from ms_kalender where tanggal='{0}'", convert_date_to_eng(tgl))
        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        If drd.Read Then
            If Not drd(0).ToString.Equals("") Then

                If drd(0).ToString.Equals("SABTU") Then
                    hasil = True
                End If

            End If
        End If
        drd.Close()


        Return hasil

    End Function

    Private Function cek_jml_sebelumnya(ByVal cn As OleDbConnection, ByVal tgl1 As String, ByVal tgl2 As String) As Integer

        Dim sql As String = String.Format("select COUNT(*) from V_InOut3 where tgl_kalk is null and convert(date,tanggal)>='{0}' and  convert(date,tanggal)<='{1}'", convert_date_to_eng(tgl1), convert_date_to_eng(tgl2))
        sql = String.Format("{0} and userid in (select idmesin from ms_karyawan where kdgol='{1}'", sql, dv2(Me.BindingContext(dv2).Position)("kode").ToString)


        If Not pegawai.ToLower.Equals("all") Then
            sql = String.Format("{0} and nip='{1}')", sql, cbpeg.EditValue)
        Else
            sql = String.Format("{0})", sql)
        End If

        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        Dim hasil As Integer = 0

        If drd.Read Then
            If IsNumeric(drd(0).ToString) Then
                hasil = Integer.Parse(drd(0).ToString)
            End If
        End If
        drd.Close()

        Return hasil

    End Function

    Private Sub upadate_msinout(ByVal cn As OleDbConnection, ByVal tgl1 As String, ByVal tgl2 As String)

        Dim sql As String = ""
        Dim adacek As Boolean = False

        Dim sql_cr As String = String.Format("select count(*) from ms_inout where tgl_kalk is null and convert(date,checktime) >='{0}' and convert(date,checktime) <='{1}'", convert_date_to_eng(tgl1), convert_date_to_eng(tgl2))
        sql_cr = String.Format("{0} and userid in (select idmesin from ms_karyawan where kdgol='{1}'", sql_cr, dv2(Me.BindingContext(dv2).Position)("kode").ToString)

        If Not cbpeg.EditValue.Equals("All") Then
            sql_cr = String.Format("{0} and nip='{1}')", sql_cr, cbpeg.EditValue)
        Else
            sql_cr = String.Format("{0})", sql_cr)
        End If

        Dim cmd_cr As OleDbCommand = New OleDbCommand(sql_cr, cn)
        Dim dr_cr As OleDbDataReader = cmd_cr.ExecuteReader
        If dr_cr.Read Then
            If IsNumeric(dr_cr(0).ToString) Then

                If Integer.Parse(dr_cr(0).ToString) > 0 Then
                    adacek = True
                    sql = String.Format("update ms_inout set skalk=0,tgl_kalk=NULL where convert(date,checktime) >='{0}' and convert(date,checktime) <='{1}'", convert_date_to_eng(tgl1), convert_date_to_eng(tgl2))
                End If
                    
            End If
        End If
            dr_cr.Close()

            If adacek = False Then
                sql = String.Format("update ms_inout set skalk=0,tgl_kalk=NULL where tgl_kalk >='{0}' and tgl_kalk <='{1}'", convert_date_to_eng(tgl1), convert_date_to_eng(tgl2))
            End If

            sql = String.Format("{0} and userid in (select idmesin from ms_karyawan where kdgol='{1}'", sql, dv2(Me.BindingContext(dv2).Position)("kode").ToString)

            If Not cbpeg.EditValue.Equals("All") Then
                sql = String.Format("{0} and nip='{1}')", sql, cbpeg.EditValue)
            Else
                sql = String.Format("{0})", sql)
            End If

            '----------------------------------

            Dim sqldel_kosong As String = String.Format("delete from tr_hadir where tanggal>='{0}' and tanggal<='{1}' and jammasuk is null and jampulang is null and jadwalmasuk is null and jadwalpulang is null and stat='LAIN-LAIN'", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))

            If Not cbpeg.EditValue.ToString.Equals("All") Then
                sql = String.Format(" {0} and userid in (select idmesin from ms_karyawan where nip='{1}')", sql, cbpeg.EditValue)
                sqldel_kosong = String.Format(" {0} and nip='{1}'", sqldel_kosong, pegawai)
            End If

            Using cmd As OleDbCommand = New OleDbCommand(sql, cn)
                cmd.ExecuteNonQuery()
            End Using

            Using cmd_kosong As OleDbCommand = New OleDbCommand(sqldel_kosong, cn)
                cmd_kosong.ExecuteNonQuery()
            End Using

    End Sub

    'Private Sub cek_jenis_lembur(ByVal cn As OleDbConnection)

    '    Dim sql As String = "select * from ms_awalshift"
    '    Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
    '    Dim drd As OleDbDataReader = cmd.ExecuteReader

    '    jenishitung_lembur = 0

    '    If drd.Read Then
    '        If IsNumeric(drd("jnis_hit_harian").ToString) Then
    '            jenishitung_lembur = drd("jnis_hit_harian").ToString
    '        End If
    '    End If
    '    drd.Close()

    'End Sub

    Private Sub Get_Kalk1(ByVal rubahshift As Boolean, ByVal addlembur As Boolean)

        Dim cn As OleDbConnection = Nothing

        Try

            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn


            Dim tgl1 As String = ttgl.EditValue
            Dim tgl2 As String = ttgl2.EditValue
            Dim golongan As String = dv2(Me.BindingContext(dv2).Position)("kode").ToString
            Dim pegawai As String = cbpeg.EditValue
            Dim tglawalshift As String = ""

            If rubahshift Then
                tgl1 = dv1(bs1.Position)("tanggal")
                tgl2 = dv1(bs1.Position)("tanggal")
                pegawai = dv1(bs1.Position)("nip")
                tglawalshift = tgl1

                GoTo langsung_kesini

            End If

            Dim tglsebelum As String = DateAdd(DateInterval.Day, -1, DateValue(tgl1))


            ' cek apakah awal shift
            Dim sqlcekawal As String = "select * from ms_awalshift"
            Dim cmdcekawal As OleDbCommand = New OleDbCommand(sqlcekawal, cn)
            Dim drcekawal As OleDbDataReader = cmdcekawal.ExecuteReader

            Dim mulaiawal As Boolean = False

            If drcekawal.Read Then
                If IsNumeric(drcekawal("noid")) Then

                    Dim namahariawal As String = drcekawal("namahari").ToString
                    jenishitung_lembur = Integer.Parse(drcekawal("jnis_hit_harian").ToString)

                    Dim tglperiodeawalcek As String = DateAdd(DateInterval.Day, -15, DateValue(tgl1))
                    Dim sqlkalender As String = String.Format("select tanggal,hari from ms_kalender where tanggal>='{0}' and tanggal<='{1}' order by tanggal desc", convert_date_to_eng(tglperiodeawalcek), convert_date_to_eng(tgl1))
                    Dim cmdkalender As OleDbCommand = New OleDbCommand(sqlkalender, cn)
                    Dim drkalender As OleDbDataReader = cmdkalender.ExecuteReader

                    Dim hasilkalender As Integer = 0

                    While drkalender.Read
                        hasilkalender = hasilkalender + 1


                        If drkalender("hari").ToString.Equals(namahariawal) Then
                            mulaiawal = True
                            tglawalshift = drkalender("tanggal").ToString
                            Exit While
                        End If

                    End While
                    drkalender.Close()

                    If hasilkalender = 0 Then
                        MsgBox("isi dulu data kalender kerja..", vbOKOnly + vbExclamation, "Informasi")
                        Return
                    End If

                Else
                    drcekawal.Close()
                    MsgBox("isi dulu awal mula shift", vbOKOnly + vbExclamation, "Informasi")
                    Return
                End If
            End If
            drcekawal.Close()
            '' ------------------------------------------------------------------------------------------------------------------------ ''

            'cek kalau ada penambahan------------------------
            Dim hasilsebelumnya As Integer = cek_jml_sebelumnya(cn, tglawalshift, tgl1)
            If hasilsebelumnya <= 0 Then
                mulaiawal = False
                tglawalshift = tgl1
            End If
            '------------------------------------------

            ' clear data in ms_inout
            upadate_msinout(cn, tglawalshift, tgl2)
            '------------------------------------------

langsung_kesini:

            ' hitung harus berapa x loop
            Dim jmlhari As Integer = DateDiff(DateInterval.Day, DateValue(tglawalshift), DateValue(tgl2))

            ' cek karyawan
            Dim sqlkaryawan As String = String.Format("select idmesin,nip,nama,gapok,tunj_makan,tamb_makanlembur,liburnormal from ms_karyawan where aktif=1 and kdgol='{0}'", golongan)

            If Not pegawai.ToLower.Equals("all") Then
                sqlkaryawan = String.Format("{0} and nip='{1}'", sqlkaryawan, pegawai)
            Else

                If rubahshift Then
                    sqlkaryawan = String.Format("{0} and nip='{1}'", sqlkaryawan, dv1(bs1.Position)("nip").ToString)
                End If

            End If

            'Dim dskaryawan As DataSet = New DataSet
            'dskaryawan = Clsmy.GetDataSet(sqlkaryawan, cn)

            Dim cmd_karyawan As OleDbCommand = Nothing
            cmd_karyawan = New OleDbCommand(sqlkaryawan, cn)
            Dim drd_karyawan As OleDbDataReader = cmd_karyawan.ExecuteReader
            Dim dtkaryawan As DataTable = New DataTable
            dtkaryawan.Load(drd_karyawan)

            '--------------------------------------------------------

            ' siapkan golongan2 yang diperlukan
            Dim sqlgolongan As String = String.Format("select distinct c.nip,a.jnisgol,a.jenislembur,a.harian,a.laki2,a.perempuan,a.jnisrange,a.jenisgaji, " & _
            "b.jmin,b.jmax,b.price,b.perkalian,c.gapok,c.tunj_jabatan,c.depart from ms_golongan a left join ms_golongan2 b on a.kode=b.kode " & _
            "inner join ms_karyawan c on a.kode=c.kdgol " & _
            "where c.aktif=1 and c.nip in (select distinct b.nip from V_InOut2 a inner join ms_karyawan b on a.userid=b.idmesin " & _
            "where b.kdgol='{0}')", golongan)

            If Not pegawai.ToLower.Equals("all") Then
                sqlgolongan = String.Format("{0} and c.nip='{1}'", sqlgolongan, pegawai)
            Else

                If rubahshift Then
                    sqlgolongan = String.Format("{0} and c.nip='{1}'", sqlgolongan, dv1(bs1.Position)("nip").ToString)
                End If

            End If

            'Dim dsgol As DataSet = New DataSet
            'dsgol = Clsmy.GetDataSet(sqlgolongan, cn)
            Dim cmdgol As OleDbCommand = New OleDbCommand(sqlgolongan, cn)
            Dim drgol As OleDbDataReader = cmdgol.ExecuteReader
            Dim dtgolongan As DataTable = New DataTable
            dtgolongan.Load(drgol)

            ' --------------------------------------------------

            ' siapkan libur
            Dim sqllibur As String = String.Format("select * from ms_karyawan2 where nip in (select distinct b.nip from V_InOut3 a inner join ms_karyawan b on a.userid=b.idmesin inner join ms_golongan c on b.kdgol=c.kode " & _
            "where b.aktif=1 and b.liburnormal=0 and c.kode='{0}'", golongan)

            If Not pegawai.ToLower.Equals("all") Then
                sqllibur = String.Format(" {0} and b.nip='{1}')", sqllibur, pegawai)
            Else

                If rubahshift Then
                    sqllibur = String.Format(" {0} and b.nip='{1}')", sqllibur, dv1(bs1.Position)("nip").ToString)
                Else
                    sqllibur = String.Format("{0} )", sqllibur)
                End If

            End If

            'Dim dslibur As DataSet = New DataSet
            'dslibur = Clsmy.GetDataSet(sqllibur, cn)

            Dim cmdlibur As OleDbCommand = New OleDbCommand(sqllibur, cn)
            Dim drlibur As OleDbDataReader = cmdlibur.ExecuteReader
            Dim dtlibur As DataTable = New DataTable
            dtlibur.Load(drlibur)

            ' ----------------------------------------------------

            'Dim sql_jamlain As String = ""
            Dim sql_jam As String = ""

            If rubahshift Then
                sql_jam = "select LEFT (CONVERT (VARCHAR, CONVERT (time(0), awalmasuk), 108), 5) as awalmasuk," & _
                "LEFT (CONVERT (VARCHAR, CONVERT (time(0), jammasuk), 108), 5) as jammasuk, " & _
                "LEFT (CONVERT (VARCHAR, CONVERT (time(0), tolmasuk), 108), 5) as tolmasuk, " & _
                "LEFT (CONVERT (VARCHAR, CONVERT (time(0), akhirmasuk), 108), 5) as akhirmasuk, " & _
                "LEFT (CONVERT (VARCHAR, CONVERT (time(0), awalkeluar), 108), 5) as awalpulang, " & _
                "LEFT (CONVERT (VARCHAR, CONVERT (time(0), jampulang), 108), 5) as jampulang, " & _
                "LEFT (CONVERT (VARCHAR, CONVERT (time(0), tolpulang), 108), 5) as tolpulang, " & _
                "LEFT (CONVERT (VARCHAR, CONVERT (time(0), akhirkeluar), 108), 5) as akhirpulang,tengahmalam,0 as hitung_lembur,ms_jamkerja.kode as kdshift from ms_jamkerja "
                sql_jam = String.Format("{0} where ms_jamkerja.kode='{1}'", sql_jam, dv1(bs1.Position)("kd_shift"))
            Else

                'sql_jamlain = String.Format("select LEFT (CONVERT (VARCHAR, CONVERT (time(0), awalmasuk), 108), 5) as awalmasuk, " & _
                '"LEFT (CONVERT (VARCHAR, CONVERT (time(0), jammasuk), 108), 5) as jammasuk, " & _
                '"LEFT (CONVERT (VARCHAR, CONVERT (time(0), tolmasuk), 108), 5) as tolmasuk, " & _
                '"LEFT (CONVERT (VARCHAR, CONVERT (time(0), akhirmasuk), 108), 5) as akhirmasuk, " & _
                '"LEFT (CONVERT (VARCHAR, CONVERT (time(0), awalpulang), 108), 5) as awalpulang, " & _
                '"LEFT (CONVERT (VARCHAR, CONVERT (time(0), jampulang), 108), 5) as jampulang, " & _
                '"LEFT (CONVERT (VARCHAR, CONVERT (time(0), tolpulang), 108), 5) as tolpulang, " & _
                '"LEFT (CONVERT (VARCHAR, CONVERT (time(0), akhirpulang), 108), 5) as akhirpulang,tengahmalam,hitung_lembur from ms_jamkerjalain " & _
                '" where tanggal1>='{0}' and tanggal2<='{1}'", convert_date_to_eng(tglawalshift), convert_date_to_eng(tgl2))

                sql_jam = "select LEFT (CONVERT (VARCHAR, CONVERT (time(0), awalmasuk), 108), 5) as awalmasuk," & _
                "LEFT (CONVERT (VARCHAR, CONVERT (time(0), jammasuk), 108), 5) as jammasuk, " & _
                "LEFT (CONVERT (VARCHAR, CONVERT (time(0), tolmasuk), 108), 5) as tolmasuk, " & _
                "LEFT (CONVERT (VARCHAR, CONVERT (time(0), akhirmasuk), 108), 5) as akhirmasuk, " & _
                "LEFT (CONVERT (VARCHAR, CONVERT (time(0), awalkeluar), 108), 5) as awalpulang, " & _
                "LEFT (CONVERT (VARCHAR, CONVERT (time(0), jampulang), 108), 5) as jampulang, " & _
                "LEFT (CONVERT (VARCHAR, CONVERT (time(0), tolpulang), 108), 5) as tolpulang, " & _
                "LEFT (CONVERT (VARCHAR, CONVERT (time(0), akhirkeluar), 108), 5) as akhirpulang,tengahmalam,0 as hitung_lembur,ms_jamkerja.kode as kdshift from ms_jamkerja inner join ms_karyawan3 on ms_jamkerja.kode=ms_karyawan3.kodejam"

            End If


            'mulai loop tanggal
            For i As Integer = 0 To jmlhari

                If dtkaryawan.Rows.Count > 0 Then
                    For x As Integer = 0 To dtkaryawan.Rows.Count - 1

                        ' While drd_karyawan.Read

                        nnip_ksebelum = ""
                        ttgl_ksebelum = ""

                        Dim idmesin As String = dtkaryawan(x)("idmesin").ToString
                        Dim nnip As String = dtkaryawan(x)("nip").ToString
                        Dim nnama As String = dtkaryawan(x)("nama").ToString
                        Dim ggapok As Double = Double.Parse(dtkaryawan(x)("gapok").ToString)
                        Dim gmakan As Double = Double.Parse(dtkaryawan(x)("tunj_makan").ToString)
                        Dim gmakan_lembur As Double = Double.Parse(dtkaryawan(x)("tamb_makanlembur").ToString)
                        Dim gliburnormal As Integer = Integer.Parse(dtkaryawan(x)("liburnormal").ToString)

                        tambmakan = gmakan_lembur

                        'If rubahshift Then
                        '    GoTo langsung_jamnormal
                        'End If

                        'Dim sql_jam2 As String = String.Format("{0} and kode in (select kode from ms_jamkerjalain2 where nip='{1}')", sql_jamlain, nnip)
                        'cmdjam = New OleDbCommand(sql_jam2, cn)
                        'drdjam = cmdjam.ExecuteReader

                        'If drdjam.Read Then

                        '    If Not drdjam("awalmasuk").ToString.Equals("") Then

                        '        adajam = True

                        '        kdshift = "-"

                        '        awalmasuk = drdjam("awalmasuk").ToString
                        '        jammasuk = drdjam("jammasuk").ToString
                        '        tolmasuk = drdjam("tolmasuk").ToString
                        '        akhirmasuk = drdjam("akhirmasuk").ToString
                        '        awalkeluar = drdjam("awalpulang").ToString
                        '        jampulang = drdjam("jampulang").ToString
                        '        tolpulang = drdjam("tolpulang").ToString
                        '        akhirkeluar = drdjam("akhirpulang").ToString
                        '        tengahmalam = Integer.Parse(drdjam("tengahmalam").ToString)

                        '        hhit_lembur = Integer.Parse(drdjam("hitung_lembur").ToString)

                        '        ' mulai loop tanggal
                        '        ' For i As Integer = 0 To jmlhari

                        '        Dim realtanggal As Date = DateAdd(DateInterval.Day, i, DateValue(tglawalshift))

                        '        SetWaitDialog(String.Format("{0}-{1}", convert_date_to_ind(realtanggal), nnama))

                        '        get_kalk2(cn, tglawalshift, realtanggal, ggapok, gmakan, gmakan_lembur, gliburnormal, hhit_lembur, idmesin, nnip, _
                        '                  dtgolongan, dtlibur, False)

                        '        ' Next
                        '        ' akhir loop tanggal

                        '    End If

                        'Else

                        '    drdjam.Close()

                        'langsung_jamnormal:

                        Dim sql_jam2 As String = ""
                        Dim adajam As Boolean = False

                        If rubahshift Then
                            sql_jam2 = String.Format("{0} order by awalmasuk", sql_jam)
                        Else
                            sql_jam2 = String.Format("{0} where nip='{1}' order by awalmasuk", sql_jam, nnip)
                        End If

                        Dim cmdjam As OleDbCommand = Nothing
                        Dim drdjam As OleDbDataReader = Nothing
                        cmdjam = New OleDbCommand(sql_jam2, cn)
                        drdjam = cmdjam.ExecuteReader

                        If drdjam.HasRows Then
                            While drdjam.Read

                                If Not drdjam("awalmasuk").ToString.Equals("") Then

                                    adajam = True

                                    kdshift = drdjam("kdshift").ToString

                                    awalmasuk = drdjam("awalmasuk").ToString
                                    jammasuk = drdjam("jammasuk").ToString
                                    tolmasuk = drdjam("tolmasuk").ToString
                                    akhirmasuk = drdjam("akhirmasuk").ToString
                                    awalkeluar = drdjam("awalpulang").ToString
                                    jampulang = drdjam("jampulang").ToString
                                    tolpulang = drdjam("tolpulang").ToString
                                    akhirkeluar = drdjam("akhirpulang").ToString
                                    tengahmalam = Integer.Parse(drdjam("tengahmalam").ToString)

                                    hhit_lembur = Integer.Parse(drdjam("hitung_lembur").ToString)

                                    If rubahshift = True And addlembur = True Then
                                        awalabsen = TimeValue(dv1(bs1.Position)("jammasuk").ToString)
                                        akhirabsen = TimeValue(dv1(bs1.Position)("jampulang").ToString)

                                        kalk3(tgl1, nnip, dtlibur, dtgolongan, cn, rubahshift, gliburnormal, hhit_lembur)

                                    Else

                                        Dim realtanggal As Date = DateAdd(DateInterval.Day, i, DateValue(tglawalshift))

                                        SetWaitDialog(String.Format("{0}-{1}", convert_date_to_ind(realtanggal), nnama))

                                        get_kalk2(cn, realtanggal, tgl1, ggapok, gmakan, gmakan_lembur, gliburnormal, hhit_lembur, idmesin, nnip, _
                                                  dtgolongan, dtlibur, rubahshift)

                                    End If



                                End If

                            End While
                        End If
                        drdjam.Close()

                        'End While
                        '        drd_karyawan.Close()

                    Next
                End If
            Next


                    If Not (rubahshift) Then

                        Dim sqldel_hadir2 As String = "delete from tr_hadir2 where id2 not in (select id from tr_hadir)"
                        Using cmddel_hadir2 As OleDbCommand = New OleDbCommand(sqldel_hadir2, cn)
                            cmddel_hadir2.ExecuteNonQuery()
                        End Using

                        SetWaitDialog("Loading Grid...")
                        load_grid(tgl1, tgl2, "", golongan, pegawai, cn)

                    End If

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

    Private Sub get_kalk2(ByVal cn As OleDbConnection, ByVal tglawal As String, ByVal tglreal As String, _
                          ByVal ggapok As Double, ByVal gmakan As Double, ByVal gmakan_lembur As Double, ByVal gliburnormal As Integer, _
                          ByVal ghitunglembur As String, _
                          ByVal idmesin As String, ByVal nnip As String, ByVal dtgolongan As DataTable, ByVal dtlibur As DataTable, _
                          ByVal rubahshift As Boolean)

        '' persiapkan

        Dim tgl_next As String = DateAdd(DateInterval.Day, 1, DateValue(tglawal))

        Dim tgl_awalmasuk As DateTime = String.Format("{0} {1}", tglawal, awalmasuk)
        Dim tgl_akhirmasuk As DateTime

        If TimeValue(akhirmasuk) >= TimeValue("00:00") And TimeValue(akhirmasuk) <= TimeValue("04:00") And TimeValue(jammasuk) > TimeValue("12:00") Then
            tgl_akhirmasuk = String.Format("{0} {1}", tgl_next, akhirmasuk)
        Else
            tgl_akhirmasuk = String.Format("{0} {1}", tglawal, akhirmasuk)
        End If

        Dim tgl_awalkeluar As DateTime
        Dim tgl_akhirkeluar As DateTime

        If TimeValue(awalkeluar) >= TimeValue("00:00") And TimeValue(awalkeluar) <= TimeValue("12:00") And TimeValue(jammasuk) > TimeValue("12:00") Then
            tgl_awalkeluar = String.Format("{0} {1}", tgl_next, awalkeluar)
        Else
            tgl_awalkeluar = String.Format("{0} {1}", tglawal, awalkeluar)
        End If

        If TimeValue(akhirkeluar) >= TimeValue("00:00") And TimeValue(akhirkeluar) <= TimeValue("12:00") Then
            tgl_akhirkeluar = String.Format("{0} {1}", tgl_next, akhirkeluar)
        Else
            tgl_akhirkeluar = String.Format("{0} {1}", tglawal, akhirkeluar)
        End If

        '---------------------------------------------

        Dim sqlc As String = ""
        Dim sqlsebelum As String = ""


        sqlsebelum = String.Format("select min(CONVERT(time,tanggal)) as jam from V_InOut3 where " & _
        "tanggal>='{0}' and tanggal<='{1}' and  userid='{2}' ", convert_datetime_to_eng(tgl_awalmasuk), convert_datetime_to_eng(tgl_akhirmasuk), idmesin)

        If rubahshift Then
        Else
            sqlsebelum = String.Format("{0} and tgl_kalk is null and skalk=0", sqlsebelum)
        End If

        sqlsebelum = String.Format(" {0} group by CONVERT(time,tanggal)", sqlsebelum)

        sqlc = String.Format("select distinct tanggal,convert(time,tanggal) as jam from V_InOut3 where " & _
        "tanggal>='{0}' and tanggal<='{1}' and  userid='{2}' ", convert_datetime_to_eng(tgl_awalkeluar), convert_datetime_to_eng(tgl_akhirkeluar), idmesin)

        If TimeValue(akhirkeluar) >= TimeValue("00:00") And TimeValue(akhirkeluar) <= TimeValue("12:00") Then
        Else

            If rubahshift Then
            Else
                sqlc = String.Format("{0} and tgl_kalk is null and skalk=0", sqlc)
            End If

        End If

        sqlc = String.Format("{0} order by tanggal", sqlc)

        awalabsen = ""
        akhirabsen = ""
        Dim cmdawal As OleDbCommand = New OleDbCommand(sqlsebelum, cn)
        Dim drdawal As OleDbDataReader = cmdawal.ExecuteReader

        If drdawal.Read Then
            If Not (drdawal("jam").ToString.Equals("")) Then
                awalabsen = drdawal("jam").ToString

                Dim jamakhirmasuk As String = TimeValue(akhirmasuk).AddSeconds(59)
                Dim sqlupdate As String = String.Format("update ms_inout set skalk=1,tgl_kalk='{0}' where CONVERT(time, checktime)>='{1}' and CONVERT(time, checktime)<='{2}' and CONVERT(date, checktime)='{3}' " & _
                        "and userid in (select idmesin from ms_karyawan where nip='{4}')", convert_date_to_eng(tglawal), awalmasuk, jamakhirmasuk, convert_date_to_eng(tglawal), nnip)

                Using cmdup As OleDbCommand = New OleDbCommand(sqlupdate, cn)
                    cmdup.ExecuteNonQuery()
                End Using

            End If
        End If
        drdawal.Close()

        If awalabsen.ToString.Equals("") Then
            GoTo langsung_cek
        End If

        Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn)
        Dim drdc As OleDbDataReader = cmdc.ExecuteReader

        Dim adacek As Boolean = False
        Dim tgljam_abs As DateTime

        If drdc.HasRows Then
            While drdc.Read

                tgljam_abs = drdc("tanggal").ToString 'String.Format("{0} {1}", drdc("tanggal").ToString, drdc("jam").ToString)

                Dim sqlupdate2 As String = String.Format("update ms_inout set tgl_kalk='{0}' where CONVERT(time, checktime)='{1}'  and CONVERT(date, checktime)='{2}' " & _
                    "and userid in (select idmesin from ms_karyawan where nip='{3}')", convert_date_to_eng(tglawal), drdc("jam").ToString, convert_date_to_eng(tglawal), nnip)

                Using cmdup As OleDbCommand = New OleDbCommand(sqlupdate2, cn)
                    cmdup.ExecuteNonQuery()
                End Using


                'akhir absen

                tgljam_abs = drdc("tanggal").ToString 'String.Format("{0} {1}", drdc("tanggal").ToString, drdc("jam").ToString)

                If tgljam_abs >= tgl_awalkeluar And tgljam_abs <= tgl_akhirkeluar Then

                    If DateValue(drdc("tanggal").ToString) = DateValue(tgl_next) Then
                        If TimeValue(drdc("jam").ToString) <= TimeValue("10:00") Then

                            If akhirabsen.Equals("") Then
                                GoTo langsung_lainhari
                            End If

                            If TimeValue(drdc("jam").ToString) >= TimeValue(akhirabsen) Then

langsung_lainhari:
                                akhirabsen = drdc("jam").ToString

                                Dim jamakhir As String = TimeValue(akhirabsen).AddSeconds(59)
                                jamakhir = String.Format("{0} {1}", tgl_next, jamakhir)
                                Dim sqlupdate As String = String.Format("update ms_inout set skalk=1,tgl_kalk='{0}' where checktime>='{1}' and checktime<='{2}' " & _
                                    "and userid in (select idmesin from ms_karyawan where nip='{3}')", convert_date_to_eng(tglawal), convert_datetime_to_eng(tgl_awalkeluar), convert_datetime_to_eng(jamakhir), nnip)

                                Using cmdup As OleDbCommand = New OleDbCommand(sqlupdate, cn)
                                    cmdup.ExecuteNonQuery()
                                End Using

                            End If

                        End If
                    Else

                        If akhirabsen.Equals("") Then
                            GoTo langsung_masuk
                        End If

                        If TimeValue(drdc("jam").ToString) >= TimeValue(akhirabsen) Then

langsung_masuk:

                            akhirabsen = drdc("jam").ToString

                            Dim jamakhir As String = TimeValue(akhirabsen).AddSeconds(59)
                            Dim sqlupdate As String = String.Format("update ms_inout set skalk=1,tgl_kalk='{0}' where CONVERT(time, checktime)>='{1}' and CONVERT(time, checktime)<='{2}' and CONVERT(date, checktime)='{3}' " & _
                                "and userid in (select idmesin from ms_karyawan where nip='{4}')", convert_date_to_eng(tglawal), awalkeluar, jamakhir, convert_date_to_eng(tglawal), nnip)

                            Using cmdup As OleDbCommand = New OleDbCommand(sqlupdate, cn)
                                cmdup.ExecuteNonQuery()
                            End Using

                        End If

                    End If


                End If

            End While
        End If

        If Not awalabsen.Equals("") And Not akhirabsen.Equals("") Then

            If DateValue(tglawal) >= DateValue(tglreal) Then
                kalk3(tglawal, nnip, dtlibur, dtgolongan, cn, rubahshift, gliburnormal, ghitunglembur)
            End If

            adacek = True
            awalabsen = "" : akhirabsen = ""
        End If


langsung_cek:

        If adacek = False Then

            If rubahshift Then
                If awalabsen.Equals("") Or akhirabsen.Equals("") Then

                    dv1(bs1.Position)("skalk") = 0
                    dv1(bs1.Position)("stelat") = 0
                    dv1(bs1.Position)("spulangcpat") = 0
                    dv1(bs1.Position)("stat") = "LAIN-LAIN"
                    dv1(bs1.Position)("jamlembur") = 0
                    dv1(bs1.Position)("lemburperjam") = 0
                    dv1(bs1.Position)("jammasuk") = DBNull.Value
                    dv1(bs1.Position)("jampulang") = DBNull.Value
                    dv1(bs1.Position)("jadwalmasuk") = DBNull.Value
                    dv1(bs1.Position)("jadwalpulang") = DBNull.Value
                    dv1(bs1.Position)("totlembur") = 0
                    dv1(bs1.Position)("hasilper") = 0
                    dv1(bs1.Position)("jmlhasil") = 0
                    dv1(bs1.Position)("tothasil") = 0
                    dv1(bs1.Position)("tambmakan") = 0
                    dv1(bs1.Position)("jamkerja") = 0
                    dv1(bs1.Position)("jam1") = 0
                    dv1(bs1.Position)("jam2") = 0
                    dv1(bs1.Position)("jam3") = 0
                    dv1(bs1.Position)("jam4") = 0
                    dv1(bs1.Position)("lemburdep") = 0
                    dv1(bs1.Position)("jharian") = 0
                    dv1(bs1.Position)("jmltelat") = 0
                    dv1(bs1.Position)("tamblembur") = 0
                    dv1(bs1.Position)("tamb1") = 0
                    dv1(bs1.Position)("tamb2") = 0

                End If
            End If

            If DateValue(tglawal) >= DateValue(tglreal) Then

                Dim sqlcek As String = String.Format("select count(*) from tr_hadir where nip='{0}' and tanggal='{1}'", nnip, convert_date_to_eng(tglawal))
                Dim cmdcekada As OleDbCommand = New OleDbCommand(sqlcek, cn)
                Dim drcekada As OleDbDataReader = cmdcekada.ExecuteReader

                Dim ceksebelum As Boolean = False
                If drcekada.Read Then
                    If IsNumeric(drcekada(0).ToString) Then

                        If Integer.Parse(drcekada(0).ToString) > 0 Then

                            ceksebelum = True
                        End If

                    End If
                End If
                drcekada.Close()

                If ceksebelum = False Then
                    Dim sqlinsert As String = String.Format("insert into tr_hadir (nip,tanggal,stat,kdgol) values('{0}','{1}','LAIN-LAIN','{2}')", _
                               nnip, convert_date_to_eng(tglawal), dv2(Me.BindingContext(dv2).Position)("kode").ToString)

                    Using cmdin_non As OleDbCommand = New OleDbCommand(sqlinsert, cn)
                        cmdin_non.ExecuteNonQuery()
                    End Using
                End If


            End If

        End If

    End Sub


    Private Sub kalk3(ByVal tanggal As String, ByVal nnip As String, ByVal dtlibur As DataTable, _
                      ByVal dtgolongan As DataTable, ByVal cn As OleDbConnection, _
                      ByVal rubahshift As Boolean, ByVal liburnormal As String, ByVal hitlembur As String)


        Dim stelat As Integer = 0
        Dim spulangcpt As Integer = 0
        Dim jmltelat As Integer = 0
        Dim jamkerja As Double = 0.0
        Dim jamlembur As Double = 0.0

        Dim jmlhasil As Integer = 0
        Dim totlembur As Double = 0

        Dim jam1 As Double = 0
        Dim jam2 As Double = 0
        Dim jam3 As Double = 0
        Dim jam4 As Double = 0

        Dim hasilper As Double = 0
        Dim tothasil As Double = 0
        Dim jharian As Double = 0

        Dim sjenislembur As String = ""
        Dim sjenisgaji As String = ""
        Dim sharian As Integer = 0
        Dim sperempuan As Double = 0
        Dim slaki2 As Double = 0
        Dim sgapok As Double = 0
        Dim stunj_jab As Double = 0
        Dim sdepart As String = ""

        Dim kharian As Double = 0

        Dim row3() As DataRow = dtgolongan.Select(String.Format("nip='{0}'", nnip))
        If row3.Length > 0 Then
            sjenislembur = row3(0)("jenislembur").ToString
            sjenisgaji = row3(0)("jenisgaji").ToString
            sharian = Integer.Parse(row3(0)("harian").ToString)
            sgapok = Double.Parse(row3(0)("gapok").ToString)
            sperempuan = Double.Parse(row3(0)("perempuan").ToString)
            slaki2 = Double.Parse(row3(0)("laki2").ToString)
            stunj_jab = Double.Parse(row3(0)("tunj_jabatan").ToString)
            sdepart = row3(0)("depart").ToString
        End If

        If jammasuk.Length > 0 And tolmasuk.Length > 0 Then

            If TimeValue(awalabsen) > TimeValue(tolmasuk) Then

                If TimeValue(awalabsen) > TimeValue(tolmasuk) Then

                    stelat = 1

                    Dim jmmasuk As Date = Convert.ToDateTime(jammasuk)
                    Dim jmtolmasuk As Date = Convert.ToDateTime(tolmasuk)

                    Dim hsiltelat As TimeSpan = jmmasuk.Subtract(jmtolmasuk)

                    jmltelat = hsiltelat.TotalMinutes

                Else
                    stelat = 0
                    jmltelat = 0
                End If
            Else
                stelat = 0
                jmltelat = 0
            End If

        End If

hitung_jamkerja:

        Dim ijammas As Date = Nothing

        Dim awalabsen_p As DateTime = TimeValue(awalabsen)
        Dim jammasuk_p As DateTime = TimeValue(jammasuk)

        Dim jmlawal As TimeSpan = jammasuk_p.Subtract(awalabsen_p)

        If jmlawal.TotalMinutes >= 60 Then
            ijammas = Convert.ToDateTime(awalabsen)
        Else
            ijammas = Convert.ToDateTime(jammasuk)
        End If

        Dim ijampula As Date = Convert.ToDateTime(akhirabsen)
        Dim hjamkerja As TimeSpan = ijampula.Subtract(ijammas)

        jamkerja = hjamkerja.TotalMinutes
        jamkerja = jamkerja / 60
        jamkerja = Math.Floor(jamkerja)

        If jamkerja < 0 Then

            Dim jamakhir As Date = Convert.ToDateTime("23:59:59")
            Dim jamawal As Date = Convert.ToDateTime("00:00:00")

            '  Dim jamtest As Date = Date.MinValue

            Dim hsilsebelum = jamakhir.Subtract(ijammas)
            Dim hasilsesudah = ijampula.Subtract(jamawal)

            Dim thsilsebelum = hsilsebelum.TotalSeconds + 1
            thsilsebelum = thsilsebelum / 60
            Dim thsilsesudah = hasilsesudah.TotalMinutes

            'jamkerja = (thsilsebelum + thsilsesudah)
            jamkerja = (thsilsebelum + thsilsesudah) / 60
            jamkerja = Math.Floor(jamkerja)

        End If

        Dim apakahsabtu As Boolean = cekhari_sabtu(cn, tanggal)

        Dim sjenislibur As String = "-"
        Dim shari As String = ""

        Dim sqlc As String = String.Format("select jenislibur,hari from ms_kalender where tanggal='{0}'", convert_date_to_eng(tanggal))
        Dim comd As OleDbCommand = New OleDbCommand(sqlc, cn)
        Dim drd As OleDbDataReader = comd.ExecuteReader

        If drd.Read Then
            sjenislibur = drd(0).ToString
            shari = drd(1).ToString
        End If
        drd.Close()

        Dim apakahlibur As Boolean = False

        If liburnormal = 1 Then

            If Not sjenislibur.Trim.Equals("-") Then
                apakahlibur = True
            End If

        Else

            Dim rowliburkah As DataRow = dtlibur.Select(String.Format("nip ='{0}' and namahari ='{1}' and tanggal1<='{2}' and tanggal2>='{2}'", nnip, shari, tanggal)).FirstOrDefault()
            If Not rowliburkah Is Nothing Then
                apakahlibur = True
            Else
                If sjenislibur.Trim.Equals("Libur Hari Besar") Then
                    apakahlibur = True
                End If
            End If

        End If


        ' ---------------------------

        Dim jamawal2 As DateTime = String.Format("{0} {1}", "2000/11/11", awalabsen)
        Dim jamakhir2 As DateTime = String.Format("{0} {1}", "2000/11/11", akhirabsen)

        Dim jamistirahat1 As DateTime = String.Format("{0} {1}", "2000/11/11", "12:00")
        Dim jamistirahat2 As DateTime = String.Format("{0} {1}", "2000/11/11", "18:00")
        Dim jamistirahat3 As DateTime = String.Format("{0} {1}", "2000/12/11", "03:00")
        Dim jammalam As DateTime = String.Format("{0} {1}", "2000/12/11", "00:00")

        Dim v_kurang1 As Integer = 0
        Dim v_kurang2 As Integer = 0
        Dim v_kurang3 As Integer = 0

        If TimeValue(jamakhir2) > TimeValue(jammalam) And TimeValue(jamakhir2) < TimeValue(jamistirahat1) Then
            jamakhir2 = String.Format("{0} {1}", "2000/12/11", akhirabsen)
        End If

        Dim cek1 As TimeSpan = jamakhir2.Subtract(jamistirahat1)
        Dim cek2 As TimeSpan = jamakhir2.Subtract(jamistirahat2)
        Dim cek3 As TimeSpan = jamakhir2.Subtract(jamistirahat3)

        Dim cek1awal As TimeSpan = jamistirahat1.Subtract(jamawal2)
        Dim cekawal2 As TimeSpan = jamistirahat2.Subtract(jamawal2)

        If cek1.TotalMinutes >= 90 And cek1awal.TotalMinutes >= 100 Then
            v_kurang1 = 1
        End If

        If cek2.TotalMinutes >= 90 And cekawal2.TotalMinutes >= 100 Then
            v_kurang2 = 1
        End If

        If cek3.TotalMinutes >= 90 Then
            v_kurang3 = 1
        End If

        ' --------------------------

        If hitlembur.Equals("HARI LIBUR") Then
            apakahlibur = True
        End If

        If apakahlibur = True Then

            If sjenislembur.Equals("Tidak Ada") Then
                GoTo masuk_lembur_biasa
            End If

            If sjenisgaji.Equals("Mingguan") Then
                GoTo masuk_lembur_biasa
            End If

            jamlembur = jamkerja - (v_kurang1 + v_kurang2 + v_kurang3)

            If jamlembur < 0 Then
                jamlembur = 0
            Else
                jamlembur = jamlembur * 60
            End If

            tambmakan = 0

        Else

            If apakahsabtu = True Then

                If sjenisgaji.Equals("Mingguan") Then
                    GoTo masuk_lembur_biasa
                End If

                jamlembur = jamkerja - (v_kurang1 + v_kurang2 + v_kurang3)

                If jamlembur >= 5 Then
                    jamlembur = jamlembur - 5
                Else
                    jamlembur = 0
                End If

                If jamlembur <= 0 Then
                    jamlembur = 0
                Else
                    jamlembur = jamlembur * 60
                End If

                If (sjenislembur.Equals("Depnaker") Or sjenislembur.Equals("Jam Mati")) And sjenisgaji.Equals("Bulanan") Then
                    If (jamlembur / 60) >= 3 Then
                        tambmakan = tambmakan
                    Else
                        tambmakan = 0
                    End If
                Else
                    tambmakan = 0
                End If


            Else

masuk_lembur_biasa:

                jamlembur = jamkerja - (v_kurang1 + v_kurang2 + v_kurang3)

                If jamlembur >= 7 And apakahlibur = False Then
                    jamlembur = jamlembur - 7
                ElseIf apakahlibur = False And jamlembur < 7 Then
                    jamlembur = 0
                End If

                If jamlembur <= 0 Then
                    jamlembur = 0
                Else
                    jamlembur = jamlembur * 60
                End If

                If (sjenislembur.Equals("Depnaker") Or sjenislembur.Equals("Jam Mati")) And sjenisgaji.Equals("Bulanan") Then
                    If (jamlembur / 60) >= 3 Then
                        tambmakan = tambmakan
                    Else
                        tambmakan = 0
                    End If
                Else
                    tambmakan = 0
                End If

            End If

        End If

        ' --------------------------------------------------------------

        If jamkerja > 8 Then
            spulangcpt = 0
        Else

            If TimeValue(akhirabsen) < TimeValue(jampulang) Then
                spulangcpt = 1
            Else
                spulangcpt = 0
            End If

        End If

        '' tambahin tmbahan lemburnya

        Dim tamblembur As Double = 0.0
        Dim jamlemburreal As Integer = jamlembur

        If rubahshift Then

            If IsNumeric(dv1(bs1.Position)("tamblembur").ToString) Then
                If Double.Parse(dv1(bs1.Position)("tamblembur").ToString) >= 1.0 Then
                    tamblembur = Double.Parse(dv1(bs1.Position)("tamblembur").ToString) * 60
                    jamlembur = jamlembur + tamblembur
                Else
                    tamblembur = Double.Parse(dv1(bs1.Position)("tamblembur").ToString)
                End If

            End If

        End If

            '------------------------------------------------------------

            If jamlembur > 0 Then

                Dim ktamblembur As Integer = tamblembur

                If liburnormal = 1 Then

                If Not sjenislibur.Trim.Equals("-") Then

                    If sjenislembur.Equals("Tidak Ada") Or sjenislembur.Equals("Jam Mati") Then
                        GoTo lembur_biasa
                    End If

                    GoTo masuk_sini
                Else
                    GoTo lembur_biasa
                End If

                Else

                    Dim row2 As DataRow = dtlibur.Select(String.Format("nip ='{0}' and namahari ='{1}' and tanggal1<='{2}' and tanggal2>='{2}'", nnip, shari, tanggal)).FirstOrDefault()
                    If Not row2 Is Nothing Then

                        If (sjenislembur.Equals("Tidak Ada") Or sjenislembur.Equals("Jam Mati")) And sjenisgaji.Equals("Mingguan") Then
                            GoTo lembur_biasa
                        End If

                        GoTo masuk_sini
                    Else
                        If sjenislibur.Trim.Equals("Libur Hari Besar") Then

                            If (sjenislembur.Equals("Tidak Ada") Or sjenislembur.Equals("Jam Mati")) And sjenisgaji.Equals("Mingguan") Then
                                GoTo lembur_biasa
                            End If

                            GoTo masuk_sini
                        End If
                    End If

                End If


                GoTo lembur_biasa

lembur_biasa:

                Dim hjamlembur = jamlembur / 60

                If hjamlembur = 0 Then
                    GoTo kosong_semualembur
                End If

                If sjenislembur.Equals("Depnaker") Then
                jam1 = 1.5
                jam2 = (hjamlembur - 1) * 2
                jam3 = 0

                If Not (IsNothing(dv1)) Then
                    If bs1.Position >= 0 Then
                        If IsNumeric(dv1(bs1.Position)("tamblembur").ToString) Then
                            If Double.Parse(dv1(bs1.Position)("tamblembur").ToString) < 1.0 Then
                                jam3 = Double.Parse(dv1(bs1.Position)("tamblembur").ToString)
                            End If
                        End If
                    End If
                End If

                jam4 = 0
            ElseIf sjenislembur.Equals("Jam Mati") Then

                If sharian.Equals(1) Then
                    jam1 = 0
                    jam2 = hjamlembur * 1
                    jam3 = 0

                    If Not (IsNothing(dv1)) Then
                        If bs1.Position >= 0 Then
                            If IsNumeric(dv1(bs1.Position)("tamblembur").ToString) Then
                                If Double.Parse(dv1(bs1.Position)("tamblembur").ToString) < 1.0 Then
                                    jam3 = Double.Parse(dv1(bs1.Position)("tamblembur").ToString)
                                End If
                            End If
                        End If
                    End If

                    jam4 = 0
                Else
                    jam1 = 0
                    jam2 = hjamlembur * 1
                    jam3 = 0

                    If Not (IsNothing(dv1)) Then
                        If bs1.Position >= 0 Then
                            If IsNumeric(dv1(bs1.Position)("tamblembur").ToString) Then
                                If Double.Parse(dv1(bs1.Position)("tamblembur").ToString) < 1.0 Then
                                    jam3 = Double.Parse(dv1(bs1.Position)("tamblembur").ToString)
                                End If
                            End If
                        End If
                    End If

                    jam4 = 0
                    End If
                End If

                GoTo masuk_harian


masuk_sini:

                jam1 = 0
                Dim sjamlembur = jamlembur / 60

                If sjamlembur = 0 Then
                    jam2 = 0
                Else
                    jam2 = sjamlembur * 2
                End If

                jam3 = 0

            If Not (IsNothing(dv1)) Then
                If bs1.Position >= 0 Then
                    If IsNumeric(dv1(bs1.Position)("tamblembur").ToString) Then
                        If Double.Parse(dv1(bs1.Position)("tamblembur").ToString) < 1.0 Then
                            jam3 = Double.Parse(dv1(bs1.Position)("tamblembur").ToString)
                        End If
                    End If
                End If
            End If


            jam4 = 0

        Else

kosong_semualembur:

            jam1 = 0
            jam2 = 0
            jam3 = 0
            jam4 = 0
        End If


masuk_harian:

        If sharian = 1 And apakahlibur = False Then

            If sjeniskelamin.Equals("Perempuan") Then

                If sgapok = 0 Then

                    If jamkerja < 6 Then
                        jharian = (sperempuan / 7) * jamkerja * 1
                    Else
                        jharian = sperempuan * 1
                    End If

                    kharian = sperempuan
                Else

                    If jamkerja < 6 Then
                        jharian = (sgapok / 7) * jamkerja * 1
                    Else
                        jharian = sgapok * 1
                    End If

                    kharian = sgapok
                End If


            Else

                If sgapok = 0 Then

                    If jamkerja < 6 Then
                        jharian = (slaki2 / 7) * jamkerja * 1
                    Else
                        jharian = slaki2 * 1
                    End If

                    kharian = slaki2

                Else


                    If jamkerja < 6 Then
                        jharian = (sgapok / 7) * jamkerja * 1
                    Else
                        jharian = sgapok * 1
                    End If

                    kharian = sgapok

                End If


            End If

        Else
            jharian = 0

            If sjeniskelamin.Equals("Perempuan") Then

                If sgapok = 0 Then

                    If jenishitung_lembur = 3 Then
                        jharian = sperempuan * 1
                    End If
                    kharian = sperempuan * 1

                Else

                    If jenishitung_lembur = 3 Then
                        jharian = sgapok
                    End If

                    kharian = sgapok
                End If

            Else

                If sgapok = 0 Then

                    If jenishitung_lembur = 3 Then
                        jharian = slaki2 * 1
                    End If

                    kharian = slaki2 * 1
                Else

                    If jenishitung_lembur = 3 Then
                        jharian = sgapok
                    End If

                    kharian = sgapok
                End If

            End If

        End If


        Dim jrp_lembur As Double = 0
        ' hitunglembur
        If sharian = 1 Then

            If sjenislembur.Equals("Depnaker") Then

                If sgapok > 0 Then

                    If jenishitung_lembur = 1 Then ' visi
                        jrp_lembur = (sgapok / 7) * (jam1 + jam2 + jam3 + jam4)
                    ElseIf jenishitung_lembur = 2 Then ' grand
                        jrp_lembur = (((sgapok + stunj_jab) * 3) / 20) * (jam1 + jam2 + jam3 + jam4)
                    Else ' dsa

                        If sdepart.Equals("HARIAN TETAP") Or sdepart.Equals("ASS OPERATOR") Then
                            jrp_lembur = sgapok * 25
                            jrp_lembur = Math.Floor((jrp_lembur / 173))
                            jrp_lembur = jrp_lembur * (jam1 + jam2 + jam3 + jam4)
                        Else
                            jrp_lembur = sgapok / 7
                            '   jrp_lembur = Math.Floor((jrp_lembur / 173))
                            jrp_lembur = jrp_lembur * (jam1 + jam2 + jam3 + jam4)
                        End If

                        
                    End If


                Else

                    If jenishitung_lembur = 1 Then ' visi
                        jrp_lembur = (kharian / 7) * (jam1 + jam2 + jam3 + jam4)
                    ElseIf jenishitung_lembur = 2 Then ' grand
                        jrp_lembur = (((kharian + stunj_jab) * 3) / 20) * (jam1 + jam2 + jam3 + jam4)
                    Else ' dsa

                        If sdepart.Equals("HARIAN TETAP") Or sdepart.Equals("ASS OPERATOR") Then
                            jrp_lembur = kharian * 25
                            jrp_lembur = Math.Floor((jrp_lembur / 173))
                            jrp_lembur = jrp_lembur * (jam1 + jam2 + jam3 + jam4)
                        Else
                            jrp_lembur = kharian / 7
                            '  jrp_lembur = Math.Floor((jrp_lembur / 173))
                            jrp_lembur = jrp_lembur * (jam1 + jam2 + jam3 + jam4)
                        End If

                        
                    End If


                End If


            ElseIf sjenislembur.Equals("Jam Mati") Then

                If sgapok > 0 Then

                    If jenishitung_lembur = 1 Then ' visi
                        jrp_lembur = (sgapok / 7) * (jam1 + jam2 + jam3 + jam4)

                    ElseIf jenishitung_lembur = 2 Then ' grand
                        jrp_lembur = (((sgapok + stunj_jab) * 3) / 20) * (jam1 + jam2 + jam3 + jam4)
                    Else ' dsa

                        If sdepart.Equals("HARIAN TETAP") Or sdepart.Equals("ASS OPERATOR") Then
                            jrp_lembur = sgapok * 25
                            jrp_lembur = Math.Floor((jrp_lembur / 173))
                            jrp_lembur = jrp_lembur * (jam1 + jam2 + jam3 + jam4)
                        Else
                            jrp_lembur = sgapok / 7
                            '  jrp_lembur = Math.Floor((jrp_lembur / 173))
                            jrp_lembur = jrp_lembur * (jam1 + jam2 + jam3 + jam4)
                        End If

                        
                    End If

                Else

                    If jenishitung_lembur = 1 Then ' visi
                        jrp_lembur = (kharian / 7) * (jam1 + jam2 + jam3 + jam4)
                    ElseIf jenishitung_lembur = 2 Then ' grand
                        jrp_lembur = (((kharian + stunj_jab) * 3) / 20) * (jam1 + jam2 + jam3 + jam4)
                    Else ' dsa

                        If sdepart.Equals("HARIAN TETAP") Or sdepart.Equals("ASS OPERATOR") Then

                            jrp_lembur = kharian * 25
                            jrp_lembur = Math.Floor((jrp_lembur / 173))
                            jrp_lembur = jrp_lembur * (jam1 + jam2 + jam3 + jam4)

                        Else
                            jrp_lembur = kharian / 7
                            '  jrp_lembur = Math.Floor((jrp_lembur / 173))
                            jrp_lembur = jrp_lembur * (jam1 + jam2 + jam3 + jam4)
                        End If
                        
                    End If


                End If


            Else
                jrp_lembur = 0
            End If

        Else

            If sjenislembur.Equals("Depnaker") Then
                jrp_lembur = ((sgapok + stunj_jab) / 173) * (jam1 + jam2 + jam3 + jam4)
            ElseIf sjenislembur.Equals("Jam Mati") Then
                jrp_lembur = ((sgapok + stunj_jab) / 25 / 7) * (jam1 + jam2 + jam3 + jam4)
            Else
                jrp_lembur = 0
            End If

        End If


        If rubahshift = True Then

            dv1(bs1.Position)("jammasuk") = awalabsen
            dv1(bs1.Position)("jampulang") = akhirabsen

            dv1(bs1.Position)("skalk") = 1
            dv1(bs1.Position)("stelat") = stelat
            dv1(bs1.Position)("spulangcpat") = spulangcpt
            dv1(bs1.Position)("stat") = "HADIR"
            dv1(bs1.Position)("jamlembur") = jamlemburreal / 60
            dv1(bs1.Position)("lemburperjam") = 0
            dv1(bs1.Position)("jadwalmasuk") = jammasuk
            dv1(bs1.Position)("jadwalpulang") = jampulang
            dv1(bs1.Position)("totlembur") = 0
            dv1(bs1.Position)("tambmakan") = tambmakan
            dv1(bs1.Position)("jamkerja") = jamkerja
            dv1(bs1.Position)("jam1") = jam1
            dv1(bs1.Position)("jam2") = jam2
            dv1(bs1.Position)("jam3") = jam3
            dv1(bs1.Position)("jam4") = jam4
            dv1(bs1.Position)("lemburdep") = (jam1 + jam2 + jam3 + jam4)
            dv1(bs1.Position)("lemburperjam") = jrp_lembur

            If tamblembur >= 1 Then
                dv1(bs1.Position)("tamblembur") = tamblembur / 60
            Else
                dv1(bs1.Position)("tamblembur") = tamblembur
            End If

            dv1(bs1.Position)("jharian") = jharian
            dv1(bs1.Position)("jmltelat") = jmltelat

            Exit Sub

        End If


        jmlhasil = 0
        tothasil = 0
        hasilper = 0

        Dim kdgol As String = ""

        If apakahlibur = False Then
            If jamkerja < 4 Then
                jharian = 0
                jamlembur = 0

                jam1 = 0
                jam2 = 0
                jam3 = 0
                jam4 = 0

                spulangcpt = 0
                skalk = 0

            End If
        End If

        Dim sqlc_tampung As String = String.Format("select id,jmlhasil,tothasil,jharian,hasilper,kdgol,kd_shift from tr_hadir where jnisabsen=1 and tanggal='{0}' and nip='{1}' and kdgol='{2}'", convert_date_to_eng(tanggal), nnip, dv2(Me.BindingContext(dv2).Position)("kode").ToString)
        'Dim dstampung As DataSet = New DataSet
        'dstampung = Clsmy.GetDataSet(sqlc_tampung, cn)
        Dim cmdtampung As OleDbCommand = New OleDbCommand(sqlc_tampung, cn)
        Dim drdtampung As OleDbDataReader = cmdtampung.ExecuteReader
        Dim dttampung As DataTable = New DataTable
        dttampung.Load(drdtampung)

        Dim dthadir2 As DataTable = New DataTable


        If nnip_ksebelum = "" Then
            nnip_ksebelum = nnip
            ttgl_ksebelum = tanggal

            Dim sqlhadir2 As String = String.Format("select id2,nip,tanggal,kdgol,kd_shift from tr_hadir2 inner join tr_hadir on tr_hadir.id=tr_hadir2.id2 where jnisabsen=1 and tanggal='{0}' and nip='{1}' and kdgol='{2}'", convert_date_to_eng(tanggal), nnip, dv2(Me.BindingContext(dv2).Position)("kode").ToString)
            'Dim dshadir2 As DataSet = New DataSet
            'dshadir2 = Clsmy.GetDataSet(sqlhadir2, cn)
            Dim cmdhadir As OleDbCommand = New OleDbCommand(sqlhadir2, cn)
            Dim drdhadir2 As OleDbDataReader = cmdhadir.ExecuteReader
            dthadir2.Load(drdhadir2)

            Dim sqlcari As String = String.Format("delete from tr_hadir where jnisabsen=1 and tanggal='{0}' and nip='{1}' and kdgol='{2}'", convert_date_to_eng(tanggal), nnip, dv2(Me.BindingContext(dv2).Position)("kode").ToString)
            Using cmd As OleDbCommand = New OleDbCommand(sqlcari, cn)
                cmd.ExecuteNonQuery()
            End Using

        Else

            If nnip <> nnip_ksebelum And DateValue(ttgl_ksebelum) <> DateValue(tanggal) Then

                Dim sqlhadir2 As String = String.Format("select id2,nip,tanggal,kdgol,kd_shift from tr_hadir2 inner join tr_hadir on tr_hadir.id=tr_hadir2.id2 where jnisabsen=1 and tanggal='{0}' and nip='{1}' and kdgol='{2}'", convert_date_to_eng(tanggal), nnip, dv2(Me.BindingContext(dv2).Position)("kode").ToString)
                'Dim dshadir2 As DataSet = New DataSet
                Dim cmdhadir As OleDbCommand = New OleDbCommand(sqlhadir2, cn)
                Dim drdhadir2 As OleDbDataReader = cmdhadir.ExecuteReader
                dthadir2.Load(drdhadir2)

                Dim sqlcari As String = String.Format("delete from tr_hadir where jnisabsen=1 and tanggal='{0}' and nip='{1}' and kdgol='{2}'", convert_date_to_eng(tanggal), nnip, dv2(Me.BindingContext(dv2).Position)("kode").ToString)
                Using cmd As OleDbCommand = New OleDbCommand(sqlcari, cn)
                    cmd.ExecuteNonQuery()
                End Using

                nnip_ksebelum = nnip
                ttgl_ksebelum = tanggal

            End If

        End If


        For i As Integer = 0 To dttampung.Rows.Count - 1

            If kdshift.Equals(dttampung(i)("kd_shift")) Then

                jmlhasil = Integer.Parse(dttampung(i)("jmlhasil").ToString)
                tothasil = Double.Parse(dttampung(i)("tothasil").ToString)
                jharian = Double.Parse(dttampung(i)("jharian").ToString)
                hasilper = Double.Parse(dttampung(i)("hasilper").ToString)
                '     kdgol = dttampung(i)("kdgol").ToString

            End If

        Next

        '  If kdgol.Equals("") Then
        kdgol = dv2(Me.BindingContext(dv2).Position)("kode").ToString
        ' End If

        Dim awal1 As String = TimeValue(awalabsen)
        Dim akhir1 As String = TimeValue(akhirabsen)

        Dim uanglembur As Double = 0


        Dim sqlinsert As String = String.Format("insert into tr_hadir (nip,tanggal,jammasuk,jampulang,stat,keterangan,stelat,spulangcpat,skalk,jamlembur,jmlhasil,lemburperjam,totlembur,hasilper,tothasil,tambmakan,step,jamkerja,jam1,jam2,jam3,jam4,jharian,jmltelat,jadwalmasuk,jadwalpulang,kdgol,kd_shift) values(" _
                                   & "'{0}','{1}','{2}','{3}','{4}','{5}',{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},'{17}',{18},{19},{20},{21},{22},{23},'{24}','{25}','{26}','{27}')", _
                               nnip, convert_date_to_eng(tanggal), awal1, akhir1, "HADIR", "", stelat, spulangcpt, 1, jamlembur, Replace(jmlhasil, ",", "."), Replace(jrp_lembur, ",", "."), 0, Replace(hasilper, ",", "."), Replace(tothasil, ",", "."), Replace(tambmakan, ",", "."), 1, Replace(jamkerja, ",", "."), Replace(jam1, ",", "."), Replace(jam2, ",", "."), Replace(jam3, ",", "."), Replace(jam4, ",", "."), Replace(jharian, ",", "."), jmltelat, jammasuk, jampulang, kdgol, kdshift)

        Using cmdins As OleDbCommand = New OleDbCommand(sqlinsert, cn)
            cmdins.ExecuteNonQuery()
        End Using

        If Not IsNothing(dthadir2) Then

            For x As Integer = 0 To dthadir2.Rows.Count - 1

                Dim sqlhadir As String = String.Format("select id from tr_hadir where nip='{0}' and tanggal='{1}' and kdgol='{2}' and kd_shift='{3}' and kdgol='{4}'", nnip, convert_date_to_eng(tanggal), kdgol, kdshift, dv2(Me.BindingContext(dv2).Position)("kode").ToString)
                Dim cmdhadir As OleDbCommand = New OleDbCommand(sqlhadir, cn)
                Dim drhadir As OleDbDataReader = cmdhadir.ExecuteReader

                If drhadir.Read Then
                    If IsNumeric(drhadir(0).ToString) Then

                        Dim sqlupdate_hadir2 As String = String.Format("update tr_hadir2 set id2={0} where id2={1}", drhadir(0).ToString, dthadir2(x)("id2").ToString)
                        Using cmdupdate_hadir2 As OleDbCommand = New OleDbCommand(sqlupdate_hadir2, cn)
                            cmdupdate_hadir2.ExecuteNonQuery()
                        End Using

                    End If
                End If
                drhadir.Close()

            Next

        End If


        '   sqltrans.Commit()

    End Sub

    Private Sub load_pegawai()

        If IsNothing(dv2) Then
            Return
        End If

        If dv2.Count <= 0 Then
            Return
        End If

        Dim cn As OleDbConnection = Nothing

        Dim sql As String = String.Format("select nip,nama from ms_karyawan where kdgol='{0}' and aktif=1 order by nama", dv2(Me.BindingContext(dv2).Position)("kode").ToString)

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim dtpegawai As DataTable
            dtpegawai = ds.Tables(0)

            Dim orow As DataRow = dtpegawai.NewRow
            orow("nip") = "All"
            orow("nama") = "All"
            dtpegawai.Rows.InsertAt(orow, 0)

            cbpeg.Properties.DataSource = dtpegawai

            cbpeg.ItemIndex = 0

        Catch ex As Exception

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

        End Try

    End Sub

    Private Sub load_grid(ByVal tgl1 As String, ByVal tgl2 As String, ByVal depart As String, _
                        ByVal gol As String, ByVal peg As String, ByVal cn As OleDbConnection)


        ' setColumn(cn)

        Dim sql As String = String.Format("select b.id,a.nip,a.nama,c.nama as namagol,b.tanggal,b.jammasuk,b.jampulang,b.skalk," _
            & "b.stelat,b.spulangcpat,b.stat,b.keterangan,b.jamlembur / 60 as jamlembur,b.lemburperjam,b.jadwalmasuk,b.jadwalpulang," _
            & "b.totlembur,b.hasilper,b.jmlhasil,b.tothasil,b.tambmakan,b.jamkerja,b.jam1,b.jam2,b.jam3,b.jam4,(b.jam1+b.jam2+b.jam3+b.jam4) as lemburdep,b.jharian,a.kdgol,c.jnisrange,b.jmltelat,b.kdgol as kdgol_tr,b.tamblembur / 60 as tamblembur,b.tamb1,b.tamb2,b.kd_shift from ms_karyawan a inner join tr_hadir b on a.nip=b.nip" _
            & " inner join ms_golongan c on a.kdgol=c.kode" _
            & " where a.aktif=1 and b.tanggal >='{0}' and b.tanggal <='{1}' and c.kode='{2}'", convert_date_to_eng(tgl1), convert_date_to_eng(tgl2), gol)


        If Not peg.ToLower.Equals("all") Then
            sql = String.Format("{0} and a.nip='{1}'", sql, peg)
        End If

        dv1 = Nothing

        ds = New DataSet()
        ds = Clsmy.GetDataSet(sql, cn)

        dvmanager = New DataViewManager(ds)
        dv1 = dvmanager.CreateDataView(ds.Tables(0))

        ' cek_hasil_grid()

        bs1 = New BindingSource
        bs1.DataSource = dv1
        bn1.BindingSource = bs1

        grid1.DataSource = bs1

    End Sub

    Private Sub opendata_pv()


        If IsNothing(dv2) Then
            Return
        End If

        If dv2.Count <= 0 Then
            Return
        End If

        Dim sqlopen As String

        sqlopen = String.Format("SELECT  ms_karyawan.nama, tr_hadir.tanggal, (tr_hadir.tothasil + tr_hadir.tambmakan + tr_hadir.jharian) as tothasil, (tr_hadir.jamlembur + tr_hadir.tamblembur) / 60 as lembur " & _
        "FROM  tr_hadir INNER JOIN ms_karyawan ON tr_hadir.nip = ms_karyawan.nip " & _
        "WHERE ms_karyawan.aktif=1 and tr_hadir.stat='HADIR' and tr_hadir.skalk=1 and tr_hadir.tanggal >='{0}' and tr_hadir.tanggal <='{1}' and ms_karyawan.kdgol='{2}' and (tr_hadir.tothasil > 0 or tr_hadir.jharian>0)", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue), dv2(Me.BindingContext(dv2).Position)("kode").ToString)

        Dim cn As OleDbConnection = Nothing

        pvgrid1.DataSource = Nothing

        Try

            open_wait()

            dv_pv = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn


            ds = New DataSet()
            ds = Clsmy.GetDataSet(sqlopen, cn)

            dvmanager_pv = New DataViewManager(ds)
            dv_pv = dvmanager_pv.CreateDataView(ds.Tables(0))

            pvgrid1.DataSource = dv_pv

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

    Private Sub opendata_darimesin()


        If IsNothing(dv2) Then
            Return
        End If

        If dv2.Count <= 0 Then
            Return
        End If

        Dim sqlopen As String

        sqlopen = String.Format("SELECT DISTINCT ms_karyawan.nama, CONVERT(varchar(10),checktime,103) as tanggal,SUBSTRING(CONVERT(varchar(20),CONVERT(time(0),checktime),108),1,5) as jam, ms_inout.skalk " & _
        "FROM         ms_karyawan INNER JOIN ms_inout ON ms_karyawan.idmesin = ms_inout.userid " & _
        "WHERE ms_karyawan.aktif=1 and convert(date,ms_inout.checktime) >='{0}' and convert(date,ms_inout.checktime) <='{1}' and ms_karyawan.kdgol='{2}'", convert_date_to_eng(tgl1), convert_date_to_eng(tgl2), dv2(Me.BindingContext(dv2).Position)("kode").ToString)

        'sqlopen = String.Format("select distinct b.nama,CONVERT(varchar(10),a.tanggal,103) as tanggal,SUBSTRING(CONVERT(varchar(20),a.jam,108),1,5) as jam,a.skalk " & _
        '"from V_InOut2 a inner join ms_karyawan b on a.userid=b.idmesin " & _
        '"where b.aktif=1 and a.tanggal>='{0}' and a.tanggal<='{1}' and b.kdgol='{2}'", convert_date_to_eng(tgl1), convert_date_to_eng(tgl2), dv2(Me.BindingContext(dv2).Position)("kode").ToString)

        'Dim sqlltidak As String = String.Format("select distinct b.nama,CONVERT(varchar(10),a.tanggal,103) as tanggal,SUBSTRING(CONVERT(varchar(20),a.jam,108),1,5) as jam " & _
        '"from V_InOut2 a inner join ms_karyawan b on a.userid=b.idmesin " & _
        '"where b.aktif=1 and a.skalk=0 and a.tanggal>='{0}' and a.tanggal<='{1}' and b.kdgol='{2}'", convert_date_to_eng(tgl1), convert_date_to_eng(tgl2), dv2(Me.BindingContext(dv2).Position)("kode").ToString)

        Dim sqlltidak As String = String.Format("SELECT  DISTINCT  ms_karyawan.nama, CONVERT(varchar(10),checktime,103) as tanggal,SUBSTRING(CONVERT(varchar(20),CONVERT(time(0),checktime),108),1,5) as jam " & _
        "FROM         ms_karyawan INNER JOIN ms_inout ON ms_karyawan.idmesin = ms_inout.userid " & _
        "WHERE ms_karyawan.aktif=1 and ms_inout.skalk=0 and convert(date,ms_inout.checktime) >='{0}' and convert(date,ms_inout.checktime) <='{1}' and ms_karyawan.kdgol='{2}'", convert_date_to_eng(tgl1), convert_date_to_eng(tgl2), dv2(Me.BindingContext(dv2).Position)("kode").ToString)

        If Not IsNothing(pegawai) Then

            If Not pegawai.ToLower.Equals("all") Then

                sqlopen = String.Format("{0} and ms_karyawan.nip='{1}'", sqlopen, pegawai)
                sqlltidak = String.Format("{0} and ms_karyawan.nip='{1}'", sqlltidak, pegawai)

            End If

        End If

        Dim cn As OleDbConnection = Nothing

        gridall.DataSource = Nothing
        gridtidak.DataSource = Nothing

        Try

            open_wait()

            dv_all = Nothing
            dv_tdk = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn


            ds = New DataSet()
            ds = Clsmy.GetDataSet(sqlopen, cn)

            dvmanager_all = New DataViewManager(ds)
            dv_all = dvmanager_all.CreateDataView(ds.Tables(0))

            gridall.DataSource = dv_all

            ' tidak terkalkulasi

            Dim ds1 As DataSet
            ds1 = New DataSet
            ds1 = Clsmy.GetDataSet(sqlltidak, cn)

            dvmanager_tdk = New DataViewManager(ds1)
            dv_tdk = dvmanager_tdk.CreateDataView(ds1.Tables(0))

            gridtidak.DataSource = dv_tdk

            ' -----------------------------

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

    Private Sub cek_hasil_grid()

        If statkalkhasil = True Then

            If IsNothing(dv1) Then
                Return
            End If

            If dv1.Count <= 0 Then
                Return
            End If

            Dim konfir_f As Integer = 0

            For i As Integer = 0 To dv1.Count - 1

                Dim jmlhasil As Integer = Integer.Parse(dv1(i)("jmlhasil").ToString)
                Dim kdgol As String = dv1(i)("kdgol_tr").ToString
                Dim hargahasil As Double = Double.Parse(dv1(i)("hasilper").ToString)

                If kdgol.Length = 0 Then

                    dv1(i)("kdgol_tr") = dv1(i)("kdgol").ToString

                Else

                    Dim row As DataRow = dtgol_per.Select(String.Format("nip ='{0}'", dv1(i)("nip").ToString)).FirstOrDefault()

                    If Not row Is Nothing Then

                        Dim kdgol_per As String = row("kd_gol").ToString

                        If Not kdgol_per.Equals(kdgol) Then

                            If konfir_f = 0 Then
                                If MsgBox("Terdapat Golongan per-Periode,ingin dikalkulasi ulang ?", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.Yes Then

                                    hargahasil = Double.Parse(row("price").ToString)
                                    Dim totalhasil As Double = jmlhasil * hargahasil

                                    dv1(i)("kdgol_tr") = row("kd_gol").ToString
                                    dv1(i)("hasilper") = hargahasil
                                    dv1(i)("tothasil") = totalhasil

                                    konfir_f = konfir_f + 1

                                End If
                            End If

                        Else

                            Dim row2 As DataRow = dtgol_per.Select(String.Format("nip ='{0}' and jmin <={2} and jmax >={3}", dv1(i)("nip").ToString, jmlhasil, jmlhasil)).FirstOrDefault()

                            If Not row2 Is Nothing Then

                                Dim price As Double = Double.Parse(row2("price").ToString)

                                If hargahasil <> price Then

                                    If konfir_f = 0 Then
                                        If MsgBox("Terdapat Golongan per-Periode,ingin dikalkulasi ulang ?", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.Yes Then

                                            hargahasil = price
                                            Dim totalhasil As Double = jmlhasil * hargahasil

                                            dv1(i)("kdgol_tr") = row("kd_gol").ToString
                                            dv1(i)("hasilper") = hargahasil
                                            dv1(i)("tothasil") = totalhasil

                                            konfir_f = konfir_f + 1

                                        End If
                                    End If

                                End If

                            End If


                        End If


                    End If

                End If

            Next

        Else

            If IsNothing(dv1) Then
                Return
            End If

            If dv1.Count <= 0 Then
                Return
            End If


            For i As Integer = 0 To dv1.Count - 1

                dv1(i)("kdgol_tr") = dv1(i)("kdgol").ToString
            Next

        End If

    End Sub

    Private Sub setColumn(ByVal cn As OleDbConnection)

        statkalkhasil = False

        GridView1.Columns("nip").Visible = True
        GridView1.Columns("nip").VisibleIndex = 0

        GridView1.Columns("nama").Visible = True
        GridView1.Columns("nama").VisibleIndex = 1

        GridView1.Columns("skalk").Visible = True
        GridView1.Columns("skalk").VisibleIndex = 2

        Dim sqlutil As String = String.Format("select * from sutil_absen where kd_gol='{0}'", dv2(Me.BindingContext(dv2).Position)("kode").ToString)
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
                Else
                    GridView1.Columns("stat").Visible = False
                    GridView1.Columns("stat").VisibleIndex = -1
                End If
            End If

            If IsNumeric(drutil("stanggal").ToString) Then
                If drutil("stanggal") = 1 Then

                    vindex = vindex + 1

                    GridView1.Columns("tanggal").Visible = True
                    GridView1.Columns("tanggal").VisibleIndex = vindex
                Else
                    GridView1.Columns("tanggal").Visible = False
                    GridView1.Columns("tanggal").VisibleIndex = -1
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



                Else
                    GridView1.Columns("jamlembur").Visible = False
                    GridView1.Columns("jamlembur").VisibleIndex = -1

                    GridView1.Columns("lemburdep").Visible = False
                    GridView1.Columns("lemburdep").VisibleIndex = -1

                    GridView1.Columns("lemburperjam").Visible = False
                    GridView1.Columns("lemburperjam").VisibleIndex = -1

                    GridView1.Columns("tamblembur").Visible = False
                    GridView1.Columns("tamblembur").VisibleIndex = -1

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

                    statkalkhasil = True

                    vindex = vindex + 1

                    GridView1.Columns("tothasil").Visible = True
                    GridView1.Columns("tothasil").VisibleIndex = vindex
                Else

                    statkalkhasil = False

                    GridView1.Columns("tothasil").Visible = False
                    GridView1.Columns("tothasil").VisibleIndex = -1
                End If
            End If


            If IsNumeric(drutil("stambmakan").ToString) Then
                If drutil("stambmakan") = 1 Then

                    vindex = vindex + 1

                    GridView1.Columns("tambmakan").Visible = True
                    GridView1.Columns("tambmakan").VisibleIndex = vindex
                Else
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

    End Sub

    Private Sub tsadd_Click(sender As System.Object, e As System.EventArgs) Handles tsadd.Click


        If IsNothing(dv2) Then
            Return
        End If

        If dv2.Count <= 0 Then
            Return
        End If

        load_Rgolongan()

        Dim kdgol As String = dv2(Me.BindingContext(dv2).Position)("kode").ToString

        Using fkalk2 As New fkalk_absen2 With {.StartPosition = FormStartPosition.CenterParent, .kd_gol = kdgol}
            fkalk2.ShowDialog(Me)

            XtraTabControl1.SelectedTabPageIndex = 0

            With fkalk2
                tgl1 = .get_tgl
                tgl2 = .get_tgl2
                depart = .get_depart
                gol = .get_gol
                pegawai = .get_peg
                stat = .get_stat
                skalk = .get_kalk

                'If stat = True Then
                '    get_step1(tgl1, tgl2, pegawai, skalk)
                'End If

            End With



        End Using

    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged

        Dim statrh As Boolean = False

        If e.Column.FieldName.Equals("kdgol_tr") Then
            GoTo msuk_sini
        ElseIf e.Column.FieldName.Equals("stat") Then

            If e.Value = "HADIR" Then
                dv1(bs1.Position)("skalk") = 1
            Else
                dv1(bs1.Position)("skalk") = 0
            End If

            statrh = True
            GoTo langsung_sini

        ElseIf e.Column.FieldName.Equals("kd_shift") Then

            If e.Value = "-" Then

langsung_sini:

                Dim cn0 As OleDbConnection = Nothing
                Try

                    cn0 = New OleDbConnection
                    cn0 = Clsmy.open_conn

                    Dim sqldel2 As String = String.Format("delete from tr_hadir2 where id2={0}", dv1(bs1.Position)("id").ToString)
                    Using cmddel2 As OleDbCommand = New OleDbCommand(sqldel2, cn0)
                        cmddel2.ExecuteNonQuery()
                    End Using

                    dv1(bs1.Position)("kd_shift") = "-"
                    dv1(bs1.Position)("skalk") = 0
                    dv1(bs1.Position)("stelat") = 0
                    dv1(bs1.Position)("spulangcpat") = 0

                    If statrh = False Then
                        dv1(bs1.Position)("stat") = "LAIN-LAIN"
                    End If

                    dv1(bs1.Position)("jamlembur") = 0
                    dv1(bs1.Position)("lemburperjam") = 0
                    dv1(bs1.Position)("jammasuk") = DBNull.Value
                    dv1(bs1.Position)("jampulang") = DBNull.Value
                    dv1(bs1.Position)("jadwalmasuk") = DBNull.Value
                    dv1(bs1.Position)("jadwalpulang") = DBNull.Value
                    dv1(bs1.Position)("totlembur") = 0
                    dv1(bs1.Position)("hasilper") = 0
                    dv1(bs1.Position)("jmlhasil") = 0
                    dv1(bs1.Position)("tothasil") = 0
                    dv1(bs1.Position)("tambmakan") = 0
                    dv1(bs1.Position)("jamkerja") = 0
                    dv1(bs1.Position)("jam1") = 0
                    dv1(bs1.Position)("jam2") = 0
                    dv1(bs1.Position)("jam3") = 0
                    dv1(bs1.Position)("jam4") = 0
                    dv1(bs1.Position)("lemburdep") = 0
                    dv1(bs1.Position)("jharian") = 0
                    dv1(bs1.Position)("jmltelat") = 0
                    dv1(bs1.Position)("tamblembur") = 0
                    dv1(bs1.Position)("tamb1") = 0
                    dv1(bs1.Position)("tamb2") = 0



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

            Get_Kalk1(True, False)

        ElseIf e.Column.FieldName.Equals("tamblembur") Then

            Get_Kalk1(True, True)

        ElseIf e.Column.FieldName.Equals("jmlhasil") Then

msuk_sini:

            Dim kdgol As String = dv1(bs1.Position)("kdgol_tr").ToString
            Dim jnisra As String = dv1(bs1.Position)("jnisrange").ToString
            Dim nilaival As Double = dv1(bs1.Position)("jmlhasil").ToString

            Dim row As DataRow = dtgol_umum.Select(String.Format("kode ='{0}' and jmin <={1} and jmax >={2}", kdgol, nilaival, nilaival)).FirstOrDefault()
            If Not row Is Nothing Then
                Dim kalkulasi, hasilper As Double
                hasilper = Convert.ToDouble(row.Item("price").ToString)

                If jnisra.Equals("Tidak Ada") Then
                    kalkulasi = 0
                    hasilper = 0
                ElseIf jnisra.Equals("Harian") Then

                    Dim xkerja As Integer = Integer.Parse(row.Item("xkerja").ToString)
                    Dim xlembur As Integer = Integer.Parse(row.Item("xlembur").ToString)

                    kalkulasi = hasilper * xkerja
                Else
                    kalkulasi = nilaival * hasilper
                End If

                dv1(bs1.Position)("jmlhasil") = nilaival
                dv1(bs1.Position)("hasilper") = hasilper
                dv1(bs1.Position)("tothasil") = kalkulasi

            Else
                dv1(bs1.Position)("hasilper") = 0
                dv1(bs1.Position)("tothasil") = 0
            End If

        ElseIf e.Column.FieldName.Equals("hasilper") Then

            'Dim jmlhasil As Double = dv1(bs1.Position)("jmlhasil").ToString
            'Dim harga As Double = e.Value

            'Dim tot As Double = jmlhasil * harga

            'dv1(bs1.Position)("hasilper") = harga
            'dv1(bs1.Position)("tothasil") = tot


        End If

    End Sub

    Private Sub fkalk_absen_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        tgl1 = "01/01/2001"
        tgl2 = "01/01/2001"

        ttgl.EditValue = Date.Now
        ttgl2.EditValue = Date.Now

        cbjenis.SelectedIndex = 0
        cbjenis_SelectedIndexChanged(Nothing, Nothing)

        load_pegawai()

        loadGolongan()
        load_Rgolongan()
        load_RJamKerja()

        Get_Aksesform()

    End Sub

    Private Sub tsedit_Click(sender As System.Object, e As System.EventArgs) Handles tsedit.Click

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count < 1 Then
            Return
        End If

        If MsgBox("Yakin data sudah benar ?", vbYesNo + vbInformation, "Informasi") = vbNo Then
            Return
        End If

        Dim cn As OleDbConnection = Nothing
        Dim comd As OleDbCommand
        Dim sql As String

        Try

            Dim ktelat As Integer
            Dim kpulangcepat As Integer
            Dim kketerangan As String
            Dim kstat As String
            Dim kkalk As Integer
            Dim kjamkerja As Integer
            Dim kjamlembur As Integer
            Dim khasil As Integer
            Dim klemburperjam As Double
            Dim ktotlembur As Double
            Dim kjam1 As Double
            Dim kjam2 As Double
            Dim kjam3 As Double
            Dim kjam4 As Double
            Dim ktambmakan As Double
            Dim kharian As Double
            Dim knip As String
            Dim ktanggal As String
            Dim kjammasuk As String
            Dim kjampulang As String

            Dim khasilper As Double
            Dim ktothasil As Double

            Dim kdgol As String
            Dim kdshift2 As String

            Dim tamblembur As Double
            Dim tamb1, tamb2 As Double

            Dim jadwalmasuk As String
            Dim jadwalpulang As String

            'Dim stelat As String
            'Dim spulangcpat As String
            'Dim stat As String

            '  Dim jamkerja As Integer
            Dim jmltelat As Integer

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction

            For i As Integer = 0 To dv1.Count - 1

                ktelat = Convert.ToInt32(dv1(i)("stelat").ToString)
                kpulangcepat = Convert.ToInt32(dv1(i)("spulangcpat").ToString)
                kketerangan = dv1(i)("keterangan").ToString
                kstat = dv1(i)("stat").ToString

                If dv1(i)("skalk") = 1 Then
                    kkalk = 1
                Else
                    kkalk = 0
                End If

                kjamkerja = Convert.ToInt32(dv1(i)("jamkerja").ToString)
                kjamlembur = Convert.ToInt32(dv1(i)("jamlembur").ToString) * 60
                khasil = Convert.ToInt32(dv1(i)("jmlhasil").ToString)
                klemburperjam = Convert.ToDouble(dv1(i)("lemburperjam").ToString)
                ktotlembur = Convert.ToDouble(dv1(i)("totlembur").ToString)
                kjam1 = Convert.ToDouble(dv1(i)("jam1").ToString)
                kjam2 = Convert.ToDouble(dv1(i)("jam2").ToString)
                kjam3 = Convert.ToDouble(dv1(i)("jam3").ToString)
                kjam4 = Convert.ToDouble(dv1(i)("jam4").ToString)

                tamblembur = Convert.ToDouble(dv1(i)("tamblembur").ToString) * 60

                tamb1 = Convert.ToDouble(dv1(i)("tamb1").ToString)
                tamb2 = Convert.ToDouble(dv1(i)("tamb2").ToString)

                ktambmakan = Convert.ToDouble(dv1(i)("tambmakan").ToString)
                kharian = Convert.ToDouble(dv1(i)("jharian").ToString)

                khasilper = Convert.ToDouble(dv1(i)("hasilper").ToString)
                ktothasil = Convert.ToDouble(dv1(i)("tothasil").ToString)


                knip = dv1(i)("nip").ToString
                ktanggal = dv1(i)("tanggal").ToString

                If dv1(i)("jammasuk").ToString.Equals("") Then
                    kjammasuk = TimeValue("900-01-01 00:00:00.000")
                Else
                    kjammasuk = TimeValue(Convert.ToDateTime(dv1(i)("jammasuk").ToString))
                End If

                If dv1(i)("jampulang").ToString.Equals("") Then
                    kjampulang = TimeValue("900-01-01 00:00:00.000")
                Else
                    kjampulang = TimeValue(Convert.ToDateTime(dv1(i)("jampulang").ToString))
                End If

                If dv1(i)("jadwalmasuk").ToString.Equals("") Then
                    jadwalmasuk = TimeValue("900-01-01 00:00:00.000")
                Else
                    jadwalmasuk = TimeValue(Convert.ToDateTime(dv1(i)("jadwalmasuk").ToString))
                End If

                If dv1(i)("jadwalpulang").ToString.Equals("") Then
                    jadwalpulang = TimeValue("900-01-01 00:00:00.000")
                Else
                    jadwalpulang = TimeValue(Convert.ToDateTime(dv1(i)("jadwalpulang").ToString))
                End If

                kdgol = dv2(Me.BindingContext(dv2).Position)("kode").ToString 'dv1(i)("kdgol_tr").ToString
                kdshift2 = dv1(i)("kd_shift").ToString

                Dim noid As String = dv1(i)("id").ToString

                'stelat = dv1(i)("stelat").ToString
                'spulangcpat = dv1(i)("spulangcpat").ToString
                'stat = dv1(i)("stat").ToString

                '   jamkerja = Integer.Parse(dv1(i)("jamkerja").ToString)
                jmltelat = Integer.Parse(dv1(i)("jmltelat").ToString)

                sql = String.Format("update tr_hadir set stat='{0}',keterangan='{1}',stelat={2},spulangcpat={3},skalk={4},jamkerja={5},jamlembur={6},jmlhasil={7},lemburperjam={8},totlembur={9},jam1={10},jam2={11},jam3={12},jam4={13},hasilper={14},tothasil={15},tambmakan={16},step=2,jharian={17},kdgol='{18}',jammasuk='{19}',jampulang='{20}',tamblembur={21},tamb1={22},tamb2={23},kd_shift='{24}',jadwalmasuk='{25}',jadwalpulang='{26}', jmltelat={27} where id={28}",
                                                    kstat, kketerangan, ktelat, kpulangcepat, kkalk, kjamkerja, kjamlembur, khasil, Replace(klemburperjam, ",", "."), Replace(ktotlembur, ",", "."), Replace(kjam1, ",", "."), Replace(kjam2, ",", "."), Replace(kjam3, ",", "."), Replace(kjam4, ",", "."), Replace(khasilper, ",", "."), Replace(ktothasil, ",", "."), Replace(ktambmakan, ",", "."), Replace(kharian, ",", "."), kdgol, kjammasuk, kjampulang, Replace(tamblembur, ",", "."), Replace(tamb1, ",", "."), Replace(tamb2, ",", "."), kdshift2, jadwalmasuk, jadwalpulang, jmltelat, noid)

                comd = New OleDbCommand(sql, cn, sqltrans)
                comd.ExecuteNonQuery()

            Next

            sqltrans.Commit()
            MsgBox("Data disimpan...", vbOKOnly + vbInformation, "Informasi")

            'grid1.DataSource = Nothing

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

    Private Sub GridView2_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView2.FocusedRowChanged

        If IsNothing(dv2) Then
            Return
        End If

        If dv2.Count <= 0 Then
            Return
        End If

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            XtraTabControl2.SelectedTabPageIndex = 0
            XtraTabControl1.SelectedTabPageIndex = 0

            ' tgl1 = "01/01/2001"
            '  tgl2 = "01/01/2001"

            load_pegawai()
            load_Rgolongan()
            load_RJamKerja()

            If IsNothing(dv2(Me.BindingContext(dv2).Position)("kode").ToString) Then
                Return
            End If

            If IsNothing(cbpeg.EditValue) Then
                Return
            End If

            setColumn(cn)

            cbjenis.SelectedIndex = 0

            If statkalkhasil = True Then


                ' load golongan lain
                Dim sqlgol_laen As String = String.Format("select a.kd_gol,b.nip,x.jmin,x.jmax,x.perkalian,x.price,x.perkalian from ms_gol_tt a inner join ms_gol_tt2 b on a.kode=b.kode inner join ms_golongan2 x on a.kd_gol=x.kode " & _
                "where a.tanggal1 >='{0}' and a.tanggal2 <='{1}' and b.nip in (select distinct d.nip from V_InOut2 c inner join ms_karyawan d on c.userid=d.idmesin " & _
                "inner join ms_golongan e on d.kdgol=e.kode " & _
                "where d.aktif=1 and c.tanggal>='{0}' and c.tanggal<='{1}' and e.kode='{2}'", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue), dv2(Me.BindingContext(dv2).Position)("kode").ToString)

                If Not cbpeg.EditValue.ToString.Equals("all") Then
                    sqlgol_laen = String.Format("{0} and d.nip='{1}'", sqlgol_laen, cbpeg.EditValue)
                End If

                sqlgol_laen = String.Format("{0})", sqlgol_laen)

                Dim dshasil_per As DataSet = New DataSet
                dshasil_per = Clsmy.GetDataSet(sqlgol_laen, cn)

                dtgol_per = New DataTable
                dtgol_per = dshasil_per.Tables(0)

            End If


            load_grid(ttgl.EditValue, ttgl2.EditValue, "", dv2(Me.BindingContext(dv2).Position)("kode").ToString, cbpeg.EditValue, cn)

            ' get_step1(ttgl.EditValue, ttgl2.EditValue, cbpeg.EditValue, False)

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

    Private Sub GridView2_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles GridView2.RowCellClick
        GridView2_FocusedRowChanged(sender, Nothing)
    End Sub

    Private Sub XtraTabControl1_Selected(sender As System.Object, e As DevExpress.XtraTab.TabPageEventArgs) Handles XtraTabControl1.Selected

        If e.PageIndex = 1 Then
            opendata_darimesin()
        End If

    End Sub

    Private Function cek_doublegolongan() As Boolean

        Dim hasil As Boolean = False

        Dim cn As OleDbConnection = Nothing

        Try

            Dim sql As String = String.Format("select COUNT(*) as jml from tr_hadir inner join ms_karyawan on tr_hadir.nip=ms_karyawan.nip " & _
        "where ms_karyawan.aktif=1 and tr_hadir.tanggal>='{0}' and tr_hadir.tanggal<='{1}' and ms_karyawan.kdgol='{2}' and not(ms_karyawan.kdgol=tr_hadir.kdgol)", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue), dv2(Me.BindingContext(dv2).Position)("kode").ToString)

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If IsNumeric(drd(0).ToString) Then

                    If Integer.Parse(drd(0).ToString) > 0 Then
                        hasil = True
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

        Return hasil
    End Function

    Private Sub btok_Click(sender As System.Object, e As System.EventArgs) Handles btok.Click

        If IsNothing(dv2) Then
            Return
        End If

        If dv2.Count <= 0 Then
            Return
        End If

        If cek_doublegolongan() = True Then
            MsgBox("Terdapat golongan yang berbeda dengan golongan saat ini, perhitungan dibatalkan..", vbOKOnly + vbInformation, "Informasi")
            Return
        End If

        load_Rgolongan()
        load_RJamKerja()

        tgl1 = ttgl.EditValue
        tgl2 = ttgl2.EditValue
        depart = ""
        gol = dv2(Me.BindingContext(dv2).Position)("kode").ToString
        pegawai = cbpeg.EditValue
        stat = True

        skalk = True

   
        If cbjenis.SelectedIndex = 2 Then
            If MsgBox("Yakin akan dikalkulasi ulang perhitungan absen ?, karna data perhitungan sebelumnya akan dijadikan nilai standar atau 0 ?", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.No Then
                Return
            End If
        End If

        'If cbjenis.SelectedIndex = 0 Then
        '    load_grid(tgl1, tgl2, depart, gol, pegawai, Nothing)
        'Else

        XtraTabControl2.SelectedTabPageIndex = 0
        XtraTabControl1.SelectedTabPageIndex = 0

        '  get_step1(tgl1, tgl2, pegawai, skalk)
        'End If

        Get_Kalk1(False, False)

    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click
        tsedit_Click(sender, Nothing)
    End Sub

    Private Sub cbjenis_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbjenis.SelectedIndexChanged
        If cbjenis.SelectedIndex = 0 Then
            lnote.Text = "Note : 'None' -> Jenis perhitungan ini hanya menampilkan data yang telah dikalkulasi sebelumnya.."
        ElseIf cbjenis.SelectedIndex = 1 Then
            lnote.Text = "Note : 'Belum Terkalkulasi' -> Jenis perhitungan ini akan mencari data dari mesin absen yang belum pernah dikalkulasi/dihitung sebelumnya..."
        Else
            lnote.Text = "Note : 'Kalkulasi Semua' -> Jenis perhitungan ini akan mengkalkulasi/menghitung ulang data absen dari mesin,dan mengembalikan nilainya ke default. HATI... HATI..."
        End If
    End Sub

    Private Sub btview_Click(sender As System.Object, e As System.EventArgs) Handles btview.Click

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            XtraTabControl2.SelectedTabPageIndex = 0
            XtraTabControl1.SelectedTabPageIndex = 0

            load_Rgolongan()
            load_RJamKerja()

            tgl1 = ttgl.EditValue
            tgl2 = ttgl2.EditValue
            depart = ""
            gol = dv2(Me.BindingContext(dv2).Position)("kode").ToString
            pegawai = cbpeg.EditValue
            stat = True

            skalk = True

            If statkalkhasil = True Then


                ' load golongan lain
                Dim sqlgol_laen As String = String.Format("select a.kd_gol,b.nip,x.jmin,x.jmax,x.perkalian,x.price,x.perkalian from ms_gol_tt a inner join ms_gol_tt2 b on a.kode=b.kode inner join ms_golongan2 x on a.kd_gol=x.kode " & _
                "where a.tanggal1 >='{0}' and a.tanggal2 <='{1}' and b.nip in (select distinct d.nip from V_InOut2 c inner join ms_karyawan d on c.userid=d.idmesin " & _
                "inner join ms_golongan e on d.kdgol=e.kode " & _
                "where d.aktif=1 and c.tanggal>='{0}' and c.tanggal<='{1}' and e.kode='{2}'", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue), dv2(Me.BindingContext(dv2).Position)("kode").ToString)

                If Not cbpeg.EditValue.ToString.Equals("all") Then
                    sqlgol_laen = String.Format("{0} and d.nip='{1}'", sqlgol_laen, cbpeg.EditValue)
                End If

                sqlgol_laen = String.Format("{0})", sqlgol_laen)

                Dim dshasil_per As DataSet = New DataSet
                dshasil_per = Clsmy.GetDataSet(sqlgol_laen, cn)

                dtgol_per = New DataTable
                dtgol_per = dshasil_per.Tables(0)

            End If


            load_grid(ttgl.EditValue, ttgl2.EditValue, "", dv2(Me.BindingContext(dv2).Position)("kode").ToString, cbpeg.EditValue, cn)

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

    Private Sub XtraTabControl2_Selected(sender As Object, e As DevExpress.XtraTab.TabPageEventArgs) Handles XtraTabControl2.Selected
        If e.PageIndex = 1 Then
            opendata_pv()
        End If
    End Sub

    Private Sub rbutton_hsl_Click(sender As Object, e As System.EventArgs) Handles rbutton_hsl.ButtonClick

        Dim iddia As Integer = Integer.Parse(dv1(bs1.Position)("id").ToString)
        Dim nama As String = dv1(bs1.Position)("nama").ToString
        Dim tanggals As String = dv1(bs1.Position)("tanggal").ToString
        Dim shifts As String = dv1(bs1.Position)("kd_shift").ToString
        Dim kodegol As String = dv2(Me.BindingContext(dv2).Position)("kode").ToString

        Using fgol2 As New fkalk_absen1 With {.StartPosition = FormStartPosition.CenterParent, .iddia = iddia, .nama = nama, .nama_shift = shifts, .tanggal_shift = tanggals, .kodegol = kodegol}
            fgol2.ShowDialog(Me)

            Dim okedata As Boolean = fgol2.get_statdata
            Dim total As Double = fgol2.get_total

            If okedata = True Then
                dv1(bs1.Position)("tothasil") = total
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

End Class