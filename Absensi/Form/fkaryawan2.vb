Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Public Class fkaryawan2
    Private bs1 As BindingSource
    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private bs2 As BindingSource
    Private dvmanager2 As Data.DataViewManager
    Private dv2 As Data.DataView

    Private bs3 As BindingSource
    Private dvmanager3 As Data.DataViewManager
    Private dv3 As Data.DataView

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private pathfoto As String
    Private jenislembur As String
    Private jenisharian As Integer

    Private dtgolongan As DataTable

    Private Function getmax_idmesin() As Integer

        Const sql As String = "select MAX(idmesin) as maxid from ms_karyawan"

        Dim cn As OleDbConnection = Nothing
        Dim dread As OleDbDataReader
        Dim comd As OleDbCommand

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            comd = New OleDbCommand(sql, cn)
            dread = comd.ExecuteReader

            If dread.HasRows Then
                If dread.Read Then

                    Dim hasil As String = dread(0).ToString

                    If IsNumeric(hasil) Then
                        Return Convert.ToInt32(hasil)
                    Else
                        Return 0
                    End If

                Else
                    Return 0
                End If
            Else
                Return 0
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
            Return -1
        Finally
            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

    End Function

    Private Sub kosongkan()
        caktif.Checked = True
        tidmesin.EditValue = 0
        tnip.EditValue = ""
        tnama.EditValue = ""
        talamat.EditValue = ""
        ttelp.EditValue = ""
        thp.EditValue = ""
        tjabatan.EditValue = ""

        ttgl_kon1.Text = "__/__/____"
        ttgl_kon2.Text = "__/__/____"
        ttgl_kon3.Text = "__/__/____"

        ttgl_kon11.Text = "__/__/____"
        ttgl_kon22.Text = "__/__/____"
        ttgl_kon33.Text = "__/__/____"

        tstat_kon1.Text = ""
        tstat_kon2.Text = ""
        tstat_kon3.Text = ""

        tfoto.Image = Nothing
        pathfoto = ""

        tbiasa.EditValue = 0
        tlibur.EditValue = 0
        traya.EditValue = 0
        tgapok.EditValue = 0
        tpot_jamsos.EditValue = 0
        ttunj_jab.EditValue = 0
        ttunj_hadir.EditValue = 0
        ttunj_akomod.EditValue = 0
        ttunj_makan.EditValue = 0
        ttamb_makan.EditValue = 0
        tlemb_perjam.EditValue = 0
        tgapok2.EditValue = 0
        tstandby.EditValue = 0
        tmakan_inap.EditValue = 0
        tsewa_kend.EditValue = 0
        tnojamsos.EditValue = ""
        tnorek.EditValue = ""
        tlemb_perjam.EditValue = 0

        tbon.EditValue = 0

        thit_kerja_lib.EditValue = 0
        thit_lmbur_lib.EditValue = 0

        ttgl_naik1.Text = convert_date_to_ind(Date.Now)
        ttgl_naik2.Text = convert_date_to_ind(Date.Now)
        ttgl_naik3.Text = convert_date_to_ind(Date.Now)
        ttgl_naik4.Text = convert_date_to_ind(Date.Now)

        tnaik1.EditValue = 0
        tnaik2.EditValue = 0
        tnaik3.EditValue = 0
        tnaik4.EditValue = 0

        If xform = 1 Then

            If xgapok = 0 Then
                tgapok.Enabled = False
                tlemb_perjam.Enabled = False
            Else
                tgapok.Enabled = True
                tlemb_perjam.Enabled = True
            End If

            If xgapok_jmsos = 0 Then
                tgapok2.Enabled = False
                tpot_jamsos.Enabled = False
            Else
                tgapok2.Enabled = True
                tpot_jamsos.Enabled = True
            End If

            If xtunj_jab = 0 Then
                ttunj_jab.Enabled = False
            Else
                ttunj_jab.Enabled = True
            End If

            If xtunj_hdr = 0 Then
                ttunj_hadir.Enabled = False
            Else
                ttunj_hadir.Enabled = True
            End If

            If xtunj_trans = 0 Then
                ttunj_akomod.Enabled = False
                tsewa_kend.Enabled = False
            Else
                ttunj_akomod.Enabled = True
                tsewa_kend.Enabled = True
            End If

            If xtunj_makan = 0 Then
                ttunj_makan.Enabled = False
                tmakan_inap.Enabled = False
            Else
                ttunj_makan.Enabled = True
                tmakan_inap.Enabled = True
            End If

            If xtamb_makan = 0 Then
                ttamb_makan.Enabled = False
            Else
                ttamb_makan.Enabled = True
            End If

        End If

    End Sub

    Private Function cek_bolehliat(ByVal cn As OleDbConnection, ByVal nnip As String) As Boolean

        Dim sql As String = String.Format("select noid from ms_usersys4 where namauser='{0}' and nip='{1}'", userprog, nnip)
        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        Dim hasil As Boolean = False

        If drd.Read Then
            If drd(0) > 0 Then
                hasil = True
            End If
        End If
        drd.Close()

        Return hasil

    End Function

    Private Sub isi()

        Dim sql As String = String.Format("select a.*,b.nama as namagol from ms_karyawan a " _
         + "inner join ms_golongan b on a.kdgol=b.kode  where a.nip='{0}'", dv(position)("nip").ToString)

        Dim cn As OleDbConnection = Nothing
        Dim comd As OleDbCommand
        Dim dred As OleDbDataReader = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            comd = New OleDbCommand(sql, cn)
            dred = comd.ExecuteReader

            If dred.HasRows Then
                If dred.Read Then

                    If dred("aktif").ToString.Equals("1") Then
                        caktif.Checked = True
                    Else
                        caktif.Checked = False
                    End If

                    If dred("liburnormal").ToString.Equals("1") Then
                        clibur.Checked = True
                    Else
                        clibur.Checked = False
                    End If

                    tidmesin.EditValue = Convert.ToInt32(dred("idmesin").ToString)
                    tnip.EditValue = dred("nip").ToString
                    tnama.EditValue = dred("nama").ToString
                    tnojamsos.EditValue = dred("nojamsos").ToString

                    ttgl.Text = convert_date_to_ind(dred("tgllahir").ToString)

                    Dim tglmulai As String = dred("tgl_mulai").ToString
                    Dim tglangkat As String = dred("tgl_angkat").ToString

                    If IsDate(tglmulai) Then
                        ttgl_mulai.EditValue = convert_date_to_ind(tglmulai)
                    Else
                        ttgl_mulai.EditValue = Nothing
                    End If

                    If IsDate(tglangkat) Then
                        ttgl_angkat.EditValue = convert_date_to_ind(tglangkat)
                    Else
                        ttgl_angkat.EditValue = Nothing
                    End If

                    talamat.EditValue = dred("alamat").ToString
                    cbjkel.EditValue = dred("jniskelamin").ToString
                    cbstat.EditValue = dred("status_kawin").ToString
                    ttelp.EditValue = dred("notelp").ToString
                    thp.EditValue = dred("nohp").ToString
                    tnote.EditValue = dred("note").ToString
                    tjabatan.EditValue = dred("jabatan").ToString

                    Dim tglkon1 As String = dred("tgl_kon1").ToString
                    If IsDate(tglkon1) Then
                        ttgl_kon1.Text = convert_date_to_ind(tglkon1)
                    Else
                        ttgl_kon1.Text = "__/__/____"
                    End If

                    Dim tglkon11 As String = dred("tgl_kon11").ToString
                    If IsDate(tglkon11) Then
                        ttgl_kon11.Text = convert_date_to_ind(tglkon11)
                    Else
                        ttgl_kon11.Text = "__/__/____"
                    End If

                    Dim tglkon2 As String = dred("tgl_kon2").ToString
                    If IsDate(tglkon2) Then
                        ttgl_kon2.Text = convert_date_to_ind(tglkon2)
                    Else
                        ttgl_kon2.Text = "__/__/____"
                    End If

                    Dim tglkon22 As String = dred("tgl_kon22").ToString
                    If IsDate(tglkon22) Then
                        ttgl_kon22.Text = convert_date_to_ind(tglkon22)
                    Else
                        ttgl_kon22.Text = "__/__/____"
                    End If

                    Dim tglkon3 As String = dred("tgl_kon3").ToString
                    If IsDate(tglkon3) Then
                        ttgl_kon3.Text = convert_date_to_ind(tglkon3)
                    Else
                        ttgl_kon3.Text = "__/__/____"
                    End If

                    Dim tglkon33 As String = dred("tgl_kon33").ToString
                    If IsDate(tglkon33) Then
                        ttgl_kon33.Text = convert_date_to_ind(tglkon33)
                    Else
                        ttgl_kon33.Text = "__/__/____"
                    End If

                    tstat_kon1.Text = dred("stat_kon1").ToString
                    tstat_kon2.Text = dred("stat_kon2").ToString
                    tstat_kon3.Text = dred("stat_kon3").ToString


                    If dred("pathfoto").ToString.Trim.Length > 0 Then
                        loadfoto(dred("pathfoto").ToString)
                    End If


lanjut:

                    tnorek.EditValue = dred("norek").ToString
                    cbbank.EditValue = dred("namabank").ToString

                    cbdepart.EditValue = dred("depart").ToString
                    cbgol.EditValue = dred("kdgol").ToString

                    '  load_jenislembur(dred("kdgol").ToString)

                    tbiasa.EditValue = Convert.ToInt32(dred("tamlembur").ToString)
                    tlibur.EditValue = Convert.ToInt32(dred("tamlibur").ToString)
                    traya.EditValue = Convert.ToInt32(dred("tamhariraya").ToString)

                    thit_kerja_lib.EditValue = Integer.Parse(dred("xkerja").ToString)
                    thit_lmbur_lib.EditValue = Integer.Parse(dred("xlembur").ToString)

                    tbon.EditValue = Double.Parse(dred("jmlbon").ToString)

                    Dim tglnaik1 As String = dred("naik1").ToString
                    Dim tglnaik2 As String = dred("naik2").ToString
                    Dim tglnaik3 As String = dred("naik3").ToString
                    Dim tglnaik4 As String = dred("naik4").ToString

                    Dim naik1 As String = dred("gaji1").ToString
                    Dim naik2 As String = dred("gaji2").ToString
                    Dim naik3 As String = dred("gaji3").ToString
                    Dim naik4 As String = dred("gaji4").ToString

                    If xform = 0 Then
                        tgapok.EditValue = Convert.ToDouble(dred("gapok").ToString)
                        tgapok2.EditValue = Convert.ToDouble(dred("gapok_jamsos").ToString)
                        tpot_jamsos.EditValue = Convert.ToDouble(dred("pot_jamsos").ToString)
                        ttunj_jab.EditValue = Convert.ToDouble(dred("tunj_jabatan").ToString)
                        ttunj_hadir.EditValue = Convert.ToDouble(dred("tunj_kehadiran").ToString)
                        ttunj_akomod.EditValue = Convert.ToDouble(dred("tunj_akomodasi").ToString)
                        ttunj_makan.EditValue = Convert.ToDouble(dred("tunj_makan").ToString)
                        ttamb_makan.EditValue = Convert.ToDouble(dred("tamb_makanlembur").ToString)
                        tstandby.EditValue = Convert.ToDouble(dred("standby").ToString)
                        tmakan_inap.EditValue = Convert.ToDouble(dred("tunj_makan_inap").ToString)
                        tsewa_kend.EditValue = Convert.ToDouble(dred("sewa_kend").ToString)

                        tlemb_perjam.EditValue = Convert.ToDouble(dred("lembur_perjam").ToString)

                        tgapok.Enabled = True
                        tgapok2.Enabled = True
                        tpot_jamsos.Enabled = True
                        ttunj_jab.Enabled = True
                        ttunj_hadir.Enabled = True
                        ttunj_akomod.Enabled = True
                        ttunj_makan.Enabled = True
                        ttamb_makan.Enabled = True
                        tpot_jamsos.Enabled = True
                        tstandby.Enabled = True
                        tmakan_inap.Enabled = True
                        tsewa_kend.Enabled = True
                        tlemb_perjam.Enabled = True

                        If IsDate(tglnaik1) Then
                            ttgl_naik1.Text = convert_date_to_ind(tglnaik1)
                        Else
                            ttgl_naik1.Text = convert_date_to_ind(Date.Now)
                        End If

                        If IsDate(tglnaik2) Then
                            ttgl_naik2.Text = convert_date_to_ind(tglnaik2)
                        Else
                            ttgl_naik2.Text = convert_date_to_ind(Date.Now)
                        End If

                        If IsDate(tglnaik3) Then
                            ttgl_naik3.Text = convert_date_to_ind(tglnaik3)
                        Else
                            ttgl_naik3.Text = convert_date_to_ind(Date.Now)
                        End If

                        If IsDate(tglnaik4) Then
                            ttgl_naik4.Text = convert_date_to_ind(tglnaik4)
                        Else
                            ttgl_naik4.Text = convert_date_to_ind(Date.Now)
                        End If

                        If IsNumeric(naik1) Then
                            tnaik1.EditValue = naik1
                        Else
                            tnaik1.EditValue = 0
                        End If

                        If IsNumeric(naik2) Then
                            tnaik2.EditValue = naik2
                        Else
                            tnaik2.EditValue = 0
                        End If

                        If IsNumeric(naik3) Then
                            tnaik3.EditValue = naik3
                        Else
                            tnaik3.EditValue = 0
                        End If

                        If IsNumeric(naik4) Then
                            tnaik4.EditValue = naik4
                        Else
                            tnaik4.EditValue = 0
                        End If

                    Else

                        If xnonkary = 0 Then

                            If cek_bolehliat(cn, tnip.Text.Trim) = True Then
                                GoTo masuk_janganliat
                            End If

                        Else

masuk_janganliat:

                            tgapok.EditValue = 0
                            tgapok.Enabled = False

                            tgapok2.EditValue = 0
                            tgapok2.Enabled = False

                            tpot_jamsos.EditValue = 0
                            tpot_jamsos.Enabled = False

                            tstandby.EditValue = 0
                            tstandby.Enabled = False

                            ttunj_jab.EditValue = 0
                            ttunj_jab.Enabled = False

                            ttunj_hadir.EditValue = 0
                            ttunj_hadir.Enabled = False

                            ttunj_akomod.EditValue = 0
                            ttunj_akomod.Enabled = False

                            ttunj_makan.EditValue = 0
                            ttunj_makan.Enabled = False

                            ttamb_makan.EditValue = 0
                            ttamb_makan.Enabled = False

                            tstandby.EditValue = 0
                            tstandby.Enabled = False

                            tmakan_inap.EditValue = 0
                            tmakan_inap.Enabled = False

                            tsewa_kend.EditValue = 0
                            tsewa_kend.Enabled = False

                            tlemb_perjam.EditValue = 0
                            tlemb_perjam.Enabled = False
                            

                            GoTo langsungsini

                        End If

                        tstandby.EditValue = Convert.ToDouble(dred("standby").ToString)
                        tstandby.Enabled = True


                        If xgapok_jmsos = 0 Then
                            tgapok2.EditValue = 0
                            tgapok2.Enabled = False

                            tpot_jamsos.EditValue = 0
                            tpot_jamsos.Enabled = False

                        Else
                            tgapok2.EditValue = Convert.ToDouble(dred("gapok_jamsos").ToString)
                            tgapok2.Enabled = True

                            tpot_jamsos.EditValue = Convert.ToDouble(dred("pot_jamsos").ToString)
                            tpot_jamsos.Enabled = True

                        End If

                        If xtunj_jab = 0 Then
                            ttunj_jab.EditValue = 0
                            ttunj_jab.Enabled = False
                        Else
                            ttunj_jab.EditValue = Convert.ToDouble(dred("tunj_jabatan").ToString)
                            ttunj_jab.Enabled = True
                        End If

                        If xgapok = 0 Then
                            tgapok.EditValue = 0
                            tgapok.Enabled = False

                            tlemb_perjam.EditValue = 0
                            tlemb_perjam.Enabled = False

                            ttgl_naik1.Text = convert_date_to_ind(Date.Now)
                            ttgl_naik2.Text = convert_date_to_ind(Date.Now)
                            ttgl_naik3.Text = convert_date_to_ind(Date.Now)
                            ttgl_naik4.Text = convert_date_to_ind(Date.Now)

                            tnaik1.EditValue = 0
                            tnaik2.EditValue = 0
                            tnaik3.EditValue = 0
                            tnaik4.EditValue = 0

                        Else
                            tgapok.EditValue = Convert.ToDouble(dred("gapok").ToString)
                            tgapok.Enabled = True

                            tlemb_perjam.EditValue = Convert.ToDouble(dred("lembur_perjam").ToString)
                            tlemb_perjam.Enabled = True

                            If IsDate(tglnaik1) Then
                                ttgl_naik1.Text = convert_date_to_ind(tglnaik1)
                            Else
                                ttgl_naik1.Text = convert_date_to_ind(Date.Now)
                            End If

                            If IsDate(tglnaik2) Then
                                ttgl_naik2.Text = convert_date_to_ind(tglnaik2)
                            Else
                                ttgl_naik2.Text = convert_date_to_ind(Date.Now)
                            End If

                            If IsDate(tglnaik3) Then
                                ttgl_naik3.Text = convert_date_to_ind(tglnaik3)
                            Else
                                ttgl_naik3.Text = convert_date_to_ind(Date.Now)
                            End If

                            If IsDate(tglnaik4) Then
                                ttgl_naik4.Text = convert_date_to_ind(tglnaik4)
                            Else
                                ttgl_naik4.Text = convert_date_to_ind(Date.Now)
                            End If

                            If IsNumeric(naik1) Then
                                tnaik1.EditValue = naik1
                            Else
                                tnaik1.EditValue = 0
                            End If

                            If IsNumeric(naik2) Then
                                tnaik2.EditValue = naik2
                            Else
                                tnaik2.EditValue = 0
                            End If

                            If IsNumeric(naik3) Then
                                tnaik3.EditValue = naik3
                            Else
                                tnaik3.EditValue = 0
                            End If

                            If IsNumeric(naik4) Then
                                tnaik4.EditValue = naik4
                            Else
                                tnaik4.EditValue = 0
                            End If

                        End If

                        

                        If xtunj_hdr = 0 Then
                            ttunj_hadir.EditValue = 0
                            ttunj_hadir.Enabled = False
                        Else
                            ttunj_hadir.EditValue = Convert.ToDouble(dred("tunj_kehadiran").ToString)
                            ttunj_hadir.Enabled = True
                        End If

                        If xtunj_trans = 0 Then
                            ttunj_akomod.EditValue = 0
                            ttunj_akomod.Enabled = False

                            tsewa_kend.EditValue = 0
                            tsewa_kend.Enabled = False

                        Else
                            ttunj_akomod.EditValue = Convert.ToDouble(dred("tunj_akomodasi").ToString)
                            ttunj_akomod.Enabled = True

                            tsewa_kend.EditValue = Convert.ToDouble(dred("sewa_kend").ToString)
                            tsewa_kend.Enabled = True

                        End If

                        If xtunj_makan = 0 Then
                            ttunj_makan.EditValue = 0
                            ttunj_makan.Enabled = False

                            tmakan_inap.EditValue = 0
                            tmakan_inap.Enabled = False

                        Else
                            ttunj_makan.EditValue = Convert.ToDouble(dred("tunj_makan").ToString)
                            ttunj_makan.Enabled = True

                            tmakan_inap.EditValue = Convert.ToDouble(dred("tunj_makan_inap").ToString)
                            tmakan_inap.Enabled = True

                        End If

                        If xtamb_makan = 0 Then
                            ttamb_makan.EditValue = 0
                            ttamb_makan.Enabled = False
                        Else
                            ttamb_makan.EditValue = Convert.ToDouble(dred("tamb_makanlembur").ToString)
                            ttamb_makan.Enabled = True
                        End If

                    End If

langsungsini:

                    'tlemb_perjam.EditValue = Convert.ToDouble(dred("lembur_perjam").ToString)

                Else
                    kosongkan()
                End If
            Else
                kosongkan()
            End If

        Catch ex As Exception

            If ex.Message.ToString.ToLower.Equals("the path is not of a legal form.") Then

                If MsgBox("Path foto tidak ditemukan, akan dilajutkan ?", vbYesNo + vbInformation, "Informasi") = MsgBoxResult.Yes Then
                    GoTo lanjut
                Else

                    If Not cn Is Nothing Then
                        If cn.State = ConnectionState.Open Then
                            cn.Close()
                        End If
                    End If

                    Me.Close()
                    Exit Sub

                End If

            End If

            MsgBox(ex.Message.ToString, MsgBoxStyle.Information, "Informasi")
            
        Finally
            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try
        
    End Sub

    Private Sub loadfoto(ByVal path As String)
        pathfoto = path
        tfoto.Image = Image.FromFile(path)
    End Sub

    Private Sub load_harilibur(ByVal kode As String)

        Dim sql As String = String.Format("select * from ms_karyawan2 where nip='{0}'", kode)
        Dim cn As OleDbConnection = Nothing

        grid1.DataSource = Nothing

        Try

            '   open_wait()

            dv1 = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet
            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager = New DataViewManager(ds)
            dv1 = dvmanager.CreateDataView(ds.Tables(0))

            bs1 = New BindingSource
            bs1.DataSource = dv1
            bn1.BindingSource = bs1

            grid1.DataSource = bs1

            ' close_wait()


        Catch ex As OleDb.OleDbException
            '  close_wait()
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally


            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

        End Try

    End Sub

    Private Sub load_jamkerja(ByVal kode As String)

        Dim sql As String = String.Format("select ms_jamkerja.kode,ms_jamkerja.nama,ms_karyawan3.id from ms_jamkerja inner join ms_karyawan3 on ms_jamkerja.kode=ms_karyawan3.kodejam where ms_karyawan3.nip='{0}'", kode)
        Dim cn As OleDbConnection = Nothing

        grid2.DataSource = Nothing

        Try

            '   open_wait()

            dv2 = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet
            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager2 = New DataViewManager(ds)
            dv2 = dvmanager2.CreateDataView(ds.Tables(0))

            bs2 = New BindingSource
            bs2.DataSource = dv2
            bn2.BindingSource = bs2

            grid2.DataSource = bs2

            ' close_wait()

            '  kalkulasi_jamlemburPerjam()

        Catch ex As OleDb.OleDbException
            '  close_wait()
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally


            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

        End Try

    End Sub

    Private Sub load_shift(ByVal kode As String)

        Dim sql As String = String.Format("select id,tgl_mulai,tgl_selesai,kd_jam,hit_jam from ms_karyawan4 where nip='{0}'", kode)

        Dim cn As OleDbConnection = Nothing

        grid3.DataSource = Nothing

        Try

            '   open_wait()

            dv3 = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet
            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager3 = New DataViewManager(ds)
            dv3 = dvmanager3.CreateDataView(ds.Tables(0))

            bs3 = New BindingSource
            bs3.DataSource = dv3
            bn3.BindingSource = bs3

            grid3.DataSource = bs3

        Catch ex As OleDb.OleDbException
            '  close_wait()
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally


            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

        End Try

    End Sub


    Private Sub loaddepart()
        Dim cn As OleDbConnection = Nothing
        Dim sql As String = "select * from ms_depart"

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            cbdepart.Properties.DataSource = ds.Tables(0)

            'cbdepart.ItemIndex = 1

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

    Private Sub loadgolongan()
        Dim cn As OleDbConnection = Nothing
        Const sql As String = "select kode,nama,harian,jenisgaji from ms_golongan where saktif=1"

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            dtgolongan = ds.Tables(0)

            cbgol.Properties.DataSource = dtgolongan

            'cbgol.ItemIndex = 1

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

    Private Sub hapushari()

        Dim cn As OleDbConnection = Nothing
        Dim comd As OleDbCommand

        Dim noid As String = dv1(Me.BindingContext(bs1).Position)("id").ToString
        If noid.Equals("") Then
            noid = 0
        End If

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction

            Dim sql As String = String.Format("delete from ms_karyawan2 where id={0}", noid)
            comd = New OleDbCommand(sql, cn, sqltrans)
            comd.ExecuteNonQuery()

            sqltrans.Commit()
            dv1.Delete(bs1.Position)

            '' isi_detail(tkode.Text.Trim)

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

    Private Sub hapusjamkerja()

        Dim cn As OleDbConnection = Nothing
        Dim comd As OleDbCommand

        Dim noid As String = dv2(Me.BindingContext(bs2).Position)("id").ToString
        If noid.Equals("") Then
            noid = 0
        End If

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction

            Dim sql As String = String.Format("delete from ms_karyawan3 where id={0}", noid)
            comd = New OleDbCommand(sql, cn, sqltrans)
            comd.ExecuteNonQuery()

            sqltrans.Commit()
            dv2.Delete(bs2.Position)

            '' isi_detail(tkode.Text.Trim)

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

    Private Sub hapusShift()

        Dim cn As OleDbConnection = Nothing
        Dim comd As OleDbCommand

        Dim noid As String = dv3(Me.BindingContext(bs3).Position)("id").ToString
        If noid.Equals("") Then
            noid = 0
        End If

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction

            Dim sql As String = String.Format("delete from ms_karyawan4 where id={0}", noid)
            comd = New OleDbCommand(sql, cn, sqltrans)
            comd.ExecuteNonQuery()

            sqltrans.Commit()
            dv3.Delete(bs3.Position)

            '' isi_detail(tkode.Text.Trim)

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


    Private Sub load_jenislembur(ByVal kdgol As String)

        Dim cn As OleDbConnection = Nothing
        Try
            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select jenislembur,harian from ms_golongan where kode='{0}'", kdgol)

            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim dred As OleDbDataReader = cmd.ExecuteReader

            If dred.HasRows Then
                If dred.Read Then
                    jenislembur = dred(0).ToString
                    jenisharian = dred(1).ToString
                Else
                    jenislembur = ""
                    jenisharian = 0
                End If
            Else
                jenislembur = ""
                jenisharian = 0
            End If

        Catch ex As Exception

            jenislembur = ""
            jenisharian = 0

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

    End Sub

    Private Sub kalkulasi_jamlemburPerjam()

        Dim gapok As Double = 0
        If IsNothing(tgapok.EditValue) Then
            gapok = 0
        Else
            gapok = tgapok.EditValue
        End If

        Dim tunj_jabatan As Double = 0
        If IsNothing(ttunj_jab.EditValue) Then
            tunj_jabatan = 0
        Else
            tunj_jabatan = ttunj_jab.EditValue
        End If

        Dim lembur_perjam As Double = 0
        Dim sharian As Integer = 0
        Dim jenislembur2 As String = ""
        Dim nilharian As Double = 0

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqlgol As String = String.Format("select jenislembur,harian,jnisrange,jenisgaji,laki2,perempuan from ms_golongan where saktif=1 and kode='{0}'", cbgol.EditValue)
            Dim cmdgol As OleDbCommand = New OleDbCommand(sqlgol, cn)
            Dim drdgol As OleDbDataReader = cmdgol.ExecuteReader

            If drdgol.Read Then
                sharian = drdgol("harian").ToString
                jenislembur2 = drdgol("jenislembur").ToString

                If tgapok.EditValue > 0 Then
                    nilharian = gapok
                Else
                    If cbjkel.EditValue = "Laki - Laki" Then
                        nilharian = drdgol("laki2").ToString
                    Else
                        nilharian = drdgol("perempuan").ToString
                    End If

                    If nilharian = 0 Then
                        nilharian = gapok
                    End If
                End If

            End If
                drdgol.Close()


        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

        If sharian = 1 Then

            If jenislembur2.Equals("Depnaker") Then

                If gapok > 0 Then
                    lembur_perjam = ((gapok * 25) / 173)
                Else
                    lembur_perjam = ((nilharian * 25) / 173)
                End If

            ElseIf jenislembur2.Equals("Jam Mati") Then

                If gapok > 0 Then
                    lembur_perjam = ((gapok * 25) / 173)
                Else
                    lembur_perjam = ((nilharian * 25) / 173)
                End If

            Else
                lembur_perjam = 0
            End If

        Else

            If jenislembur2.Equals("Depnaker") Then
                lembur_perjam = ((gapok + tunj_jabatan) / 173)
            ElseIf jenislembur2.Equals("Jam Mati") Then
                lembur_perjam = ((gapok + tunj_jabatan) / 25 / 7)
            Else
                lembur_perjam = 0
            End If

        End If

        tlemb_perjam.EditValue = lembur_perjam

    End Sub

    Private Sub simpandbase()

        Dim aktif As Integer
        If caktif.Checked = True Then
            aktif = 1
        Else
            aktif = 0
        End If

        Dim liburnormal As Integer
        If clibur.Checked = True Then
            liburnormal = 1
        Else
            liburnormal = 0
        End If

        Dim sqlsearch As String = String.Format("select nip from ms_karyawan where nip='{0}'", tnip.Text.Trim)
        'Dim sqlinsert As String = String.Format("insert into ms_karyawan (aktif,nip,idmesin,nama,alamat,jniskelamin,nohp,notelp," _
        '            + "tgllahir,note,tamlembur,tamhariraya,tamlibur,gapok,tunj_jabatan,tunj_kehadiran,tunj_akomodasi,tunj_makan,tamb_makanlembur," _
        '            + "pathfoto,depart,kdgol,lembur_perjam,status_kawin,gapok_jamsos,nojamsos,norek,namabank) values({0},'{1}',{2},'{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},{11},{12},{13},{14},{15},{16},{17},{18},'{19}','{20}','{21}',{22},'{23}',{24},'{25}','{26}','{27}')", aktif,
        '            tnip.Text.Trim, tidmesin.EditValue, tnama.Text.Trim, talamat.Text.Trim, cbjkel.EditValue, thp.Text.Trim, ttelp.Text.Trim, _
        '            convert_date_to_eng(ttgl.Text), tnote.Text.Trim, Replace(tbiasa.EditValue, ",", "."), Replace(traya.EditValue, ",", "."), _
        '            Replace(tlibur.EditValue, ",", "."), Replace(tgapok.EditValue, ",", "."), Replace(ttunj_jab.EditValue, ",", "."), Replace(ttunj_hadir.EditValue, ",", "."), _
        '            Replace(ttunj_akomod.EditValue, ",", "."), Replace(ttunj_makan.EditValue, ",", "."), Replace(ttamb_makan.EditValue, ",", "."), pathfoto, cbdepart.EditValue, cbgol.EditValue, Replace(tlemb_perjam.EditValue, ",", "."), cbstat.EditValue, Replace(tgapok2.EditValue, ",", "."), tnojamsos.EditValue.ToString.Trim, tnorek.EditValue.ToString.Trim, cbbank.EditValue)
        'Dim sqlupdate As String = String.Format("update ms_karyawan set aktif={0},idmesin={1},nama='{2}',alamat='{3}',jniskelamin='{4}'," _
        '                                        + "nohp='{5}',notelp='{6}',tgllahir='{7}',note='{8}',tamlembur={9},tamhariraya={10},tamlibur={11},gapok={12}," _
        '                                        + "tunj_jabatan={13},tunj_kehadiran={14},tunj_akomodasi={15},tunj_makan={16},tamb_makanlembur={17}," _
        '                                        + "pathfoto='{18}',depart='{19}',kdgol='{20}',lembur_perjam={21},status_kawin='{22}',gapok_jamsos={23},nojamsos='{24}',norek='{25}',namabank='{26}' where nip='{27}'", aktif, tidmesin.EditValue, tnama.Text.Trim, _
        '                                        talamat.Text.Trim, cbjkel.EditValue, thp.Text.Trim, ttelp.Text.Trim, _
        '           convert_date_to_eng(ttgl.Text), tnote.Text.Trim, Replace(tbiasa.EditValue, ",", "."), Replace(traya.EditValue, ",", "."), _
        '           Replace(tlibur.EditValue, ",", "."), Replace(tgapok.EditValue, ",", "."), Replace(ttunj_jab.EditValue, ",", "."), Replace(ttunj_hadir.EditValue, ",", "."), _
        '           Replace(ttunj_akomod.EditValue, ",", "."), Replace(ttunj_makan.EditValue, ",", "."), Replace(ttamb_makan.EditValue, ",", "."), pathfoto, cbdepart.EditValue, cbgol.EditValue, Replace(tlemb_perjam.EditValue, ",", "."), cbstat.EditValue, Replace(tgapok2.EditValue, ",", "."), tnojamsos.EditValue.ToString.Trim, tnorek.EditValue.ToString.Trim, cbbank.EditValue, tnip.Text.Trim)

        Dim sqlinsert As String = String.Format("insert into ms_karyawan (aktif,nip,idmesin,nama,alamat,jniskelamin,nohp,notelp," _
                    + "tgllahir,note,tamlembur,tamhariraya,tamlibur," _
                    + "pathfoto,depart,kdgol,lembur_perjam,status_kawin,nojamsos,norek,namabank,gapok,gapok_jamsos,tunj_jabatan,tunj_kehadiran,tunj_akomodasi,tunj_makan,tamb_makanlembur,xkerja,xlembur,liburnormal,jabatan,tgl_kon1,tgl_kon2,tgl_kon3,stat_kon1,stat_kon2,stat_kon3,tgl_kon11,tgl_kon22,tgl_kon33) values({0},'{1}',{2},'{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},{11},{12},'{13}','{14}','{15}',{16},'{17}','{18}','{19}','{20}',0,0,0,0,0,0,0,{21},{22},{23},'{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}')", aktif,
                    tnip.Text.Trim, tidmesin.EditValue, tnama.Text.Trim, talamat.Text.Trim, cbjkel.EditValue, thp.Text.Trim, ttelp.Text.Trim, _
                    convert_date_to_eng(ttgl.Text), tnote.Text.Trim, Replace(tbiasa.EditValue, ",", "."), Replace(traya.EditValue, ",", "."), _
                    Replace(tlibur.EditValue, ",", "."), _
                    pathfoto, cbdepart.EditValue, cbgol.EditValue, Replace(tlemb_perjam.EditValue, ",", "."), cbstat.EditValue, tnojamsos.EditValue.ToString.Trim, tnorek.EditValue.ToString.Trim, cbbank.EditValue, thit_kerja_lib.EditValue, thit_lmbur_lib.EditValue, liburnormal, tjabatan.EditValue, _
                    IIf(IsDate(ttgl_kon1.Text), convert_date_to_eng(ttgl_kon1.Text), "NULL"), IIf(IsDate(ttgl_kon2.Text), convert_date_to_eng(ttgl_kon2.Text), "NULL"), IIf(IsDate(ttgl_kon3.Text), convert_date_to_eng(ttgl_kon3.Text), "NULL"), tstat_kon1.Text.Trim, tstat_kon2.Text.Trim, tstat_kon3.Text.Trim, IIf(IsDate(ttgl_kon11.Text), convert_date_to_eng(ttgl_kon11.Text), "NULL"), IIf(IsDate(ttgl_kon22.Text), convert_date_to_eng(ttgl_kon22.Text), "NULL"), IIf(IsDate(ttgl_kon33.Text), convert_date_to_eng(ttgl_kon33.Text), "NULL"))

        sqlinsert = sqlinsert.Replace("'NULL'", "NULL")

        Dim sqlupdate As String = String.Format("update ms_karyawan set aktif={0},idmesin={1},nama='{2}',alamat='{3}',jniskelamin='{4}'," _
                                                + "nohp='{5}',notelp='{6}',tgllahir='{7}',note='{8}',tamlembur={9},tamhariraya={10},tamlibur={11}," _
                                                + "pathfoto='{12}',depart='{13}',kdgol='{14}',lembur_perjam={15},status_kawin='{16}',nojamsos='{17}',norek='{18}',namabank='{19}',xkerja={20},xlembur={21},liburnormal={22},jabatan='{23}',tgl_kon1='{24}',tgl_kon2='{25}',tgl_kon3='{26}',stat_kon1='{27}',stat_kon2='{28}',stat_kon3='{29}',tgl_kon11='{30}',tgl_kon22='{31}',tgl_kon33='{32}' where nip='{33}'", aktif, tidmesin.EditValue, tnama.Text.Trim, _
                                                talamat.Text.Trim, cbjkel.EditValue, thp.Text.Trim, ttelp.Text.Trim, _
                   convert_date_to_eng(ttgl.Text), tnote.Text.Trim, Replace(tbiasa.EditValue, ",", "."), Replace(traya.EditValue, ",", "."), _
                   Replace(tlibur.EditValue, ",", "."), _
                   pathfoto, cbdepart.EditValue, cbgol.EditValue, Replace(tlemb_perjam.EditValue, ",", "."), cbstat.EditValue, tnojamsos.EditValue.ToString.Trim, tnorek.EditValue.ToString.Trim, cbbank.EditValue, thit_kerja_lib.EditValue, thit_lmbur_lib.EditValue, liburnormal, tjabatan.EditValue, _
                   IIf(IsDate(ttgl_kon1.Text), convert_date_to_eng(ttgl_kon1.Text), "NULL"), IIf(IsDate(ttgl_kon2.Text), convert_date_to_eng(ttgl_kon2.Text), "NULL"), IIf(IsDate(ttgl_kon3.Text), convert_date_to_eng(ttgl_kon3.Text), "NULL"), tstat_kon1.Text.Trim, tstat_kon2.Text.Trim, tstat_kon3.Text.Trim, IIf(IsDate(ttgl_kon11.Text), convert_date_to_eng(ttgl_kon11.Text), "NULL"), IIf(IsDate(ttgl_kon22.Text), convert_date_to_eng(ttgl_kon22.Text), "NULL"), IIf(IsDate(ttgl_kon33.Text), convert_date_to_eng(ttgl_kon33.Text), "NULL"), tnip.Text.Trim)


        sqlupdate = sqlupdate.Replace("'NULL'", "NULL")

        Dim tglmulai As String = ttgl_mulai.Text

        Dim sqlupdate2 As String = ""

        If Not tglmulai.Equals("") Then
            sqlupdate2 = String.Format("update ms_karyawan set naik1='{0}',naik2='{1}',naik3='{2}',naik4='{3}',gaji1={4},gaji2={5},gaji3={6},gaji4={7},tgl_mulai='{8}' where nip='{9}'", convert_date_to_eng(ttgl_naik1.Text), convert_date_to_eng(ttgl_naik2.Text), convert_date_to_eng(ttgl_naik3.Text), convert_date_to_eng(ttgl_naik4.Text), Replace(tnaik1.EditValue, ",", "."), Replace(tnaik2.EditValue, ",", "."), Replace(tnaik3.EditValue, ",", "."), Replace(tnaik4.EditValue, ",", "."), convert_date_to_eng(tglmulai), tnip.Text.Trim)
        Else
            sqlupdate2 = String.Format("update ms_karyawan set naik1='{0}',naik2='{1}',naik3='{2}',naik4='{3}',gaji1={4},gaji2={5},gaji3={6},gaji4={7},tgl_mulai=null where nip='{8}'", convert_date_to_eng(ttgl_naik1.Text), convert_date_to_eng(ttgl_naik2.Text), convert_date_to_eng(ttgl_naik3.Text), convert_date_to_eng(ttgl_naik4.Text), Replace(tnaik1.EditValue, ",", "."), Replace(tnaik2.EditValue, ",", "."), Replace(tnaik3.EditValue, ",", "."), Replace(tnaik4.EditValue, ",", "."), tnip.Text.Trim)
        End If

        Dim tglangkat As String = ttgl_angkat.Text
        Dim sqlupdate3 As String = ""

        If Not tglangkat.Equals("") Then
            sqlupdate3 = String.Format("update ms_karyawan set tgl_angkat='{0}' where nip='{1}'", convert_date_to_eng(tglangkat), tnip.Text.Trim)
        Else
            sqlupdate3 = String.Format("update ms_karyawan set tgl_angkat=null where nip='{0}'", tnip.Text.Trim)
        End If


        Dim cn As OleDbConnection = Nothing
        Dim dread As OleDbDataReader
        Dim comds As OleDbCommand
        Dim comd As OleDbCommand

        Try

            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction

            If addstat = True Then

                comds = New OleDbCommand(sqlsearch, cn, sqltrans)
                dread = comds.ExecuteReader

                If dread.HasRows Then

                    If dread.Read Then
                        MsgBox("NIP sudah ada...", vbOKOnly + vbInformation, "Informasi")
                        close_wait()
                        Exit Sub
                    Else
                        comd = New OleDbCommand(sqlinsert, cn, sqltrans)
                        comd.ExecuteNonQuery()

                        Clsmy.InsertToLog(cn, "btkary", 1, 0, 0, 0, tnip.Text.Trim, tnama.Text.Trim, sqltrans)

                        insertview()

                    End If

                Else
                    comd = New OleDbCommand(sqlinsert, cn, sqltrans)
                    comd.ExecuteNonQuery()

                    Clsmy.InsertToLog(cn, "btkary", 1, 0, 0, 0, tnip.Text.Trim, tnama.Text.Trim, sqltrans)

                    insertview()

                End If

            Else
                comd = New OleDbCommand(sqlupdate, cn, sqltrans)
                comd.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btkary", 0, 1, 0, 0, tnip.Text.Trim, tnama.Text.Trim, sqltrans)

                updateview()

            End If

            Using cmdup2 As OleDbCommand = New OleDbCommand(sqlupdate2, cn, sqltrans)
                cmdup2.ExecuteNonQuery()
            End Using

            Using cmdup3 As OleDbCommand = New OleDbCommand(sqlupdate3, cn, sqltrans)
                cmdup3.ExecuteNonQuery()
            End Using

            update_gapokdll(cn, sqltrans)

            simpan2(cn, sqltrans)
            simpan3(cn, sqltrans)
            simpan4(cn, sqltrans)

            sqltrans.Commit()

            close_wait()

            MsgBox("Data disimpan...", vbOKOnly + vbInformation, "Informasi")

            If addstat = True Then
                kosongkan()
                load_harilibur(tnip.Text.Trim)
                load_jamkerja(tnip.Text.Trim)
                load_shift(tnip.Text.Trim)

                setidmesin()

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

    Private Sub simpan2(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction)

        Dim comd As OleDbCommand

        For i As Integer = 0 To dv1.Count - 1
            If dv1(i)("id").ToString.Equals("0") Then

                Dim sql As String = String.Format("insert into ms_karyawan2 (nip,namahari,tanggal1,tanggal2) values('{0}','{1}','{2}','{3}')", tnip.Text.Trim, dv1(i)("namahari").ToString, convert_date_to_eng(dv1(i)("tanggal1").ToString), convert_date_to_eng(dv1(i)("tanggal2").ToString))
                comd = New OleDbCommand(sql, cn, sqltrans)
                comd.ExecuteNonQuery()

            End If
        Next

    End Sub

    Private Sub simpan3(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction)

        Dim comd As OleDbCommand

        For i As Integer = 0 To dv2.Count - 1
            If dv2(i)("id").ToString.Equals("0") Then

                Dim sql As String = String.Format("insert into ms_karyawan3 (nip,kodejam) values('{0}','{1}')", tnip.Text.Trim, dv2(i)("kode").ToString)
                comd = New OleDbCommand(sql, cn, sqltrans)
                comd.ExecuteNonQuery()

            End If
        Next

    End Sub

    Private Sub simpan4(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction)

        Dim comd As OleDbCommand

        For i As Integer = 0 To dv3.Count - 1
            If dv3(i)("id").ToString.Equals("0") Then

                Dim sql As String = String.Format("insert into ms_karyawan4 (nip,kd_jam,tgl_mulai,tgl_selesai,hit_jam) values('{0}','{1}','{2}','{3}',{4})", tnip.Text.Trim, dv3(i)("kd_jam").ToString, convert_date_to_eng(dv3(i)("tgl_mulai")), convert_date_to_eng(dv3(i)("tgl_selesai")), dv3(i)("hit_jam"))
                comd = New OleDbCommand(sql, cn, sqltrans)
                comd.ExecuteNonQuery()

            End If
        Next

    End Sub


    Private Sub update_gapokdll(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction)

        Dim sqlup As String = ""

        If tstandby.Enabled = True Then

            sqlup = String.Format("update ms_karyawan set standby={0} where nip='{1}'", Replace(tstandby.EditValue, ",", "."), tnip.Text.Trim)
            Using cmdup As OleDbCommand = New OleDbCommand(sqlup, cn, sqltrans)
                cmdup.ExecuteNonQuery()
            End Using

        End If

        If tgapok.Enabled = True Then

            sqlup = String.Format("update ms_karyawan set gapok={0} where nip='{1}'", Replace(tgapok.EditValue, ",", "."), tnip.Text.Trim)
            Using cmdup As OleDbCommand = New OleDbCommand(sqlup, cn, sqltrans)
                cmdup.ExecuteNonQuery()
            End Using

        End If

        If tgapok2.Enabled = True Then

            sqlup = String.Format("update ms_karyawan set gapok_jamsos={0},pot_jamsos={1} where nip='{2}'", Replace(tgapok2.EditValue, ",", "."), Replace(tpot_jamsos.EditValue, ",", "."), tnip.Text.Trim)
            Using cmdup As OleDbCommand = New OleDbCommand(sqlup, cn, sqltrans)
                cmdup.ExecuteNonQuery()
            End Using

        End If

        If ttunj_jab.Enabled = True Then

            sqlup = String.Format("update ms_karyawan set tunj_jabatan={0} where nip='{1}'", Replace(ttunj_jab.EditValue, ",", "."), tnip.Text.Trim)
            Using cmdup As OleDbCommand = New OleDbCommand(sqlup, cn, sqltrans)
                cmdup.ExecuteNonQuery()
            End Using

        End If

        If ttunj_hadir.Enabled = True Then

            sqlup = String.Format("update ms_karyawan set tunj_kehadiran={0} where nip='{1}'", Replace(ttunj_hadir.EditValue, ",", "."), tnip.Text.Trim)
            Using cmdup As OleDbCommand = New OleDbCommand(sqlup, cn, sqltrans)
                cmdup.ExecuteNonQuery()
            End Using

        End If

        If ttunj_akomod.Enabled = True Then

            sqlup = String.Format("update ms_karyawan set tunj_akomodasi={0} where nip='{1}'", Replace(ttunj_akomod.EditValue, ",", "."), tnip.Text.Trim)
            Using cmdup As OleDbCommand = New OleDbCommand(sqlup, cn, sqltrans)
                cmdup.ExecuteNonQuery()
            End Using

        End If

        If ttunj_makan.Enabled = True Then

            sqlup = String.Format("update ms_karyawan set tunj_makan={0} where nip='{1}'", Replace(ttunj_makan.EditValue, ",", "."), tnip.Text.Trim)
            Using cmdup As OleDbCommand = New OleDbCommand(sqlup, cn, sqltrans)
                cmdup.ExecuteNonQuery()
            End Using

        End If

        If tmakan_inap.Enabled = True Then

            sqlup = String.Format("update ms_karyawan set tunj_makan_inap={0} where nip='{1}'", Replace(tmakan_inap.EditValue, ",", "."), tnip.Text.Trim)
            Using cmdup As OleDbCommand = New OleDbCommand(sqlup, cn, sqltrans)
                cmdup.ExecuteNonQuery()
            End Using

        End If

        If tsewa_kend.Enabled = True Then

            sqlup = String.Format("update ms_karyawan set sewa_kend={0} where nip='{1}'", Replace(tsewa_kend.EditValue, ",", "."), tnip.Text.Trim)
            Using cmdup As OleDbCommand = New OleDbCommand(sqlup, cn, sqltrans)
                cmdup.ExecuteNonQuery()
            End Using

        End If


        If ttamb_makan.Enabled = True Then

            sqlup = String.Format("update ms_karyawan set tamb_makanlembur={0} where nip='{1}'", Replace(ttamb_makan.EditValue, ",", "."), tnip.Text.Trim)
            Using cmdup As OleDbCommand = New OleDbCommand(sqlup, cn, sqltrans)
                cmdup.ExecuteNonQuery()
            End Using

        End If


    End Sub

    Private Sub updateview()
        dv(position)("idmesin") = tidmesin.EditValue
        dv(position)("nama") = tnama.Text.Trim
        dv(position)("tgllahir") = ttgl.Text
        dv(position)("alamat") = talamat.Text.Trim
        dv(position)("jniskelamin") = cbjkel.Text.Trim
        dv(position)("notelp") = ttelp.Text.Trim
        dv(position)("nohp") = thp.Text.Trim
        'dv(position)("golongan") = cbgol.Text.Trim
        'dv(position)("kdgol") = cbgol.EditValue
        'dv(position)("depart") = cbdepart.Text.Trim
        dv(position)("jabatan") = tjabatan.EditValue
        dv(position)("aktif") = IIf(caktif.Checked = True, 1, 0)

        'dv(position)("naik1") = ttgl_naik1.EditValue
        'dv(position)("naik2") = ttgl_naik2.EditValue
        'dv(position)("naik3") = ttgl_naik3.EditValue
        'dv(position)("naik4") = ttgl_naik4.EditValue

        'dv(position)("gaji1") = tnaik1.EditValue
        'dv(position)("gaji2") = tnaik2.EditValue
        'dv(position)("gaji3") = tnaik3.EditValue
        'dv(position)("gaji4") = tnaik4.EditValue

    End Sub

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew
        orow("nip") = tnip.Text.Trim
        orow("idmesin") = tidmesin.EditValue
        orow("nama") = tnama.Text.Trim
        orow("tgllahir") = ttgl.Text
        orow("alamat") = talamat.Text.Trim
        orow("jniskelamin") = cbjkel.Text.Trim
        orow("notelp") = ttelp.Text.Trim
        orow("nohp") = thp.Text.Trim
        orow("golongan") = cbgol.Text.Trim
        orow("kdgol") = cbgol.EditValue
        orow("depart") = cbdepart.Text.Trim
        orow("jabatan") = tjabatan.EditValue
        orow("aktif") = IIf(caktif.Checked = True, 1, 0)

        'orow("naik1") = ttgl_naik1.EditValue
        'orow("naik2") = ttgl_naik2.EditValue
        'orow("naik3") = ttgl_naik3.EditValue
        'orow("naik4") = ttgl_naik4.EditValue

        'orow("gaji1") = tnaik1.EditValue
        'orow("gaji2") = tnaik2.EditValue
        'orow("gaji3") = tnaik3.EditValue
        'orow("gaji4") = tnaik4.EditValue

        dv.EndInit()

    End Sub

    Private Sub setidmesin()

        Dim maxid As Integer = getmax_idmesin()

        If maxid = -1 Then
            Exit Sub
        End If

        maxid = maxid + 1

        tidmesin.EditValue = maxid

    End Sub


    Private Sub btfoto_Click(sender As System.Object, e As System.EventArgs) Handles btfoto.Click

        Dim fd As OpenFileDialog = New OpenFileDialog()
        fd.Title = "Open File Dialog"
        ' fd.InitialDirectory = "C:\"
        fd.Filter = "Image Files (*.bmp;*.jpg;*.gif)|*.bmp;*.jpg;*.gif|" _
            & "All Files(*.*)|"
        ' fd.FilterIndex = 2
        fd.RestoreDirectory = True

        If fd.ShowDialog() = DialogResult.OK Then
            loadfoto(fd.FileName)
        End If

    End Sub

    Private Sub tsadd_Click(sender As System.Object, e As System.EventArgs) Handles tsadd.Click

        If tnip.Text.Trim.Length <= 0 Then
            MsgBox("Nip harus diisi dahulu...", vbOKOnly + vbInformation, "Informasi")
            Exit Sub
        End If

        Using fkary3 As New fkaryawan3 With {.StartPosition = FormStartPosition.CenterParent, .dvme = dv1}
            fkary3.ShowDialog()
        End Using

    End Sub

    Private Sub tsdel_Click(sender As System.Object, e As System.EventArgs) Handles tsdel.Click

        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count <= 0 Then
            Exit Sub
        End If

        hapushari()

    End Sub

    Private Sub tsref_Click(sender As System.Object, e As System.EventArgs) Handles tsref.Click
        load_harilibur(tnip.Text.Trim)
    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click

        If tidmesin.EditValue <= 0 Then
            MsgBox("IdMesin absen tidak boleh 0", vbOKOnly + vbInformation, "Informasi")
            tidmesin.Focus()
            Exit Sub
        End If

        If tnip.Text.Trim.Length = 0 Then
            MsgBox("NIP tidak boleh kosong...", vbOKOnly + vbInformation, "Informasi")
            tnip.Focus()
            Exit Sub
        End If

        If talamat.Text.Trim.Length = 0 Then
            MsgBox("Alamat tidak boleh kosong...", vbOKOnly + vbInformation, "Informasi")
            talamat.Focus()
            Exit Sub
        End If

        If Not IsDate(ttgl.Text.Trim) Then
            MsgBox("Tanggal Salah...", vbOKOnly + vbInformation, "Informasi")
            ttgl.Focus()
            Exit Sub
        End If

        If IsNothing(cbdepart.EditValue) Then
            MsgBox("Departemen tidak boleh kosong...", vbOKOnly + vbInformation, "Informasi")
            cbdepart.Focus()
            Exit Sub
        End If

        If IsNothing(cbgol.EditValue) Then
            MsgBox("Golongan tidak boleh kosong...", vbOKOnly + vbInformation, "Informasi")
            cbgol.Focus()
            Exit Sub
        End If

        If MsgBox("Yakin semua sudah benar ... ???", vbYesNo + vbQuestion, "Konfirmasi") = vbYes Then
            simpandbase()
        End If

    End Sub

    Private Sub fkaryawan2_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        If addstat = True Then
            tnip.Focus()
        Else
            tnama.Focus()
        End If
    End Sub

    Private Sub fkaryawan2_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        loaddepart()
        loadgolongan()

        cbjkel.SelectedIndex = 0
        caktif.Checked = True
        caktif.Enabled = False

        If addstat = True Then
            kosongkan()
            setidmesin()

            pathfoto = ""

            load_harilibur("xxx")
            load_jamkerja("xxx")
            load_shift("xxdian")

            tnip.Enabled = True

            ttgl_naik1.Text = convert_date_to_ind(Date.Now)
            ttgl_naik2.Text = convert_date_to_ind(Date.Now)
            ttgl_naik3.Text = convert_date_to_ind(Date.Now)
            ttgl_naik4.Text = convert_date_to_ind(Date.Now)

            ttgl_mulai.EditValue = convert_date_to_ind(Date.Now)
            ttgl_angkat.EditValue = convert_date_to_ind(Date.Now)


        Else

            isi()

            load_harilibur(tnip.Text.Trim)
            load_jamkerja(tnip.Text.Trim)
            load_shift(tnip.Text.Trim)

            tnip.Enabled = False

            cbdepart.Enabled = False
            cbgol.Enabled = False

        End If
    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub btclearfot_Click(sender As System.Object, e As System.EventArgs) Handles btclearfot.Click
        tfoto.Image = Nothing
        pathfoto = ""
    End Sub

    Private Sub tsref2_Click(sender As System.Object, e As System.EventArgs) Handles tsref2.Click
        load_jamkerja(tnip.Text.Trim)
    End Sub

    Private Sub tsdel2_Click(sender As System.Object, e As System.EventArgs) Handles tsdel2.Click

        If IsNothing(dv2) Then
            Exit Sub
        End If

        If dv2.Count <= 0 Then
            Exit Sub
        End If

        hapusjamkerja()
    End Sub

    Private Sub tsadd2_Click(sender As System.Object, e As System.EventArgs) Handles tsadd2.Click
        If tnip.Text.Trim.Length <= 0 Then
            MsgBox("Nip harus diisi dahulu...", vbOKOnly + vbInformation, "Informasi")
            Exit Sub
        End If

        Using fkary4 As New fkaryawan4 With {.StartPosition = FormStartPosition.CenterParent, .dvjam = dv2}
            fkary4.ShowDialog()
        End Using
    End Sub

    Private Sub cbgol_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbgol.EditValueChanged
        '   load_jamkerja(cbgol.EditValue)

        Dim hsrow As DataRow() = dtgolongan.Select(String.Format("kode='{0}'", cbgol.EditValue))

        If hsrow.Length > 0 Then

            Dim charian As Integer = Integer.Parse(hsrow(0)("harian").ToString)
            Dim cjenisgaji As String = hsrow(0)("jenisgaji").ToString

            If charian = 1 Or cjenisgaji.Equals("Mingguan") Then
                lblgapok.Text = "Gapok /Hr :"
            Else
                lblgapok.Text = "Gapok /Bln :"
            End If

        End If
        

    End Sub

    Private Sub tgapok_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tgapok.EditValueChanged

        If addstat = True Then

            If xform = 1 Then

                If xgapok = 0 Then
                    tnaik1.EditValue = 0
                Else

                    If IsNumeric(tgapok.EditValue) Then
                        tnaik1.EditValue = tgapok.EditValue
                    Else
                        tnaik1.EditValue = 0
                    End If

                End If
            End If

        Else

            If xform = 1 Then
                If xgapok = 0 Then
                    tnaik1.EditValue = 0
                Else

                    If tnaik2.EditValue = 0 Then
                        If IsNumeric(tgapok.EditValue) Then
                            tnaik1.EditValue = tgapok.EditValue
                        Else
                            tnaik1.EditValue = 0
                        End If
                    End If

                End If
            End If

        End If

    End Sub

    Private Sub ttunj_jab_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles ttunj_jab.EditValueChanged

    End Sub

    Private Sub clibur_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles clibur.CheckedChanged

        If clibur.Checked = True Then
            clibur.Text = "Ya"
            'SplitContainerControl4.Panel2.Visible = False
            SplitContainerControl4.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1
        Else
            clibur.Text = "Tidak"
            SplitContainerControl4.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both
            'SplitContainerControl4.Panel2.Visible = True
        End If

    End Sub
    Private Sub tsref3_Click(sender As Object, e As EventArgs) Handles tsref3.Click
        load_shift(tnip.Text.Trim)
    End Sub

    Private Sub tsadd3_Click(sender As Object, e As EventArgs) Handles tsadd3.Click

        If tnip.Text.Trim.Length <= 0 Then
            MsgBox("Nip harus diisi dahulu...", vbOKOnly + vbInformation, "Informasi")
            Exit Sub
        End If

        Using fkary5 As New fkaryawan5 With {.StartPosition = FormStartPosition.CenterParent, .dvshift = dv3}
            fkary5.ShowDialog()
        End Using

    End Sub

    Private Sub tsdel3_Click(sender As Object, e As EventArgs) Handles tsdel3.Click

        If IsNothing(dv3) Then
            Exit Sub
        End If

        If dv3.Count <= 0 Then
            Exit Sub
        End If

        hapusShift()

    End Sub

    Private Sub tnama_EditValueChanged(sender As Object, e As EventArgs) Handles tnama.EditValueChanged
        lnama.Text = tnama.EditValue
    End Sub

    Private Sub tgapok_Validated(sender As Object, e As EventArgs) Handles tgapok.Validated
        kalkulasi_jamlemburPerjam()
    End Sub

    Private Sub ttunj_jab_Validated(sender As Object, e As EventArgs) Handles ttunj_jab.Validated
        kalkulasi_jamlemburPerjam()
    End Sub

End Class