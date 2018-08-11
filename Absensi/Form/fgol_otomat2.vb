Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Public Class fgol_otomat2

    Public dv As DataView
    Public position As Integer
    Public statadd As Boolean

    Private bs1 As BindingSource
    Private ds As DataSet
    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private bs0 As BindingSource
    Private ds0 As DataSet
    Private dvmanager0 As Data.DataViewManager
    Private dv0 As Data.DataView

    Private dtdetail As DataTable

    Private Sub SetNote()

        If cbjenisgol.SelectedIndex = 0 Then

            If cbjenislemb.SelectedIndex = 0 Then
                tnote.Text = "Perhitungan Lembur : -"
            ElseIf cbjenislemb.SelectedIndex = 1 Then
                tnote.Text = "Perhitungan Lembur : (Gapok + Tunj Jabatan) / 173"
            ElseIf cbjenislemb.SelectedIndex = 2 Then

                If charian.Checked = False Then
                    tnote.Text = "Perhitungan Lembur : (Gapok + Tunj Jabatan) / 25 / 7"
                Else
                    tnote.Text = "Perhitungan Lembur : (Gapok + Tunj Jabatan) / 7"
                End If

            Else

                If charian.Checked = False Then
                    tnote.Text = "Perhitungan Lembur : (Gapok + Tunj Jabatan) / 25 / 7"
                Else
                    tnote.Text = "Perhitungan Lembur : (Gapok + Tunj Jabatan) / 7"
                End If

            End If

        Else
            tnote.Text = "Perhitungan Lembur : -"
        End If

        If charian.Checked = True Then
            tnote.Text = String.Format("{0} | {1}", tnote.Text, "Note : Gaji harian disini berlaku hanya untuk karyawan-karyawan yang dimaster karyawan gaji diisi 0")
        End If

    End Sub

    Private Sub isi()

        Dim cn As OleDbConnection = Nothing
        Dim comd As OleDbCommand = Nothing
        Dim dread As OleDbDataReader = Nothing

        Try

            Dim sql As String = String.Format("select * from ms_golongan where kode='{0}'", dv(position)("kode").ToString)

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            comd = New OleDbCommand(sql, cn)
            dread = comd.ExecuteReader

            If dread.HasRows Then

                If dread.Read Then

                    tkode.Text = dread("kode").ToString
                    tnama.Text = dread("nama").ToString

                    If dread("jenisgaji").ToString.Equals("Bulanan") Then
                        cbgaji.SelectedIndex = 0
                    Else
                        cbgaji.SelectedIndex = 1
                    End If

                    If dread("jnisgol").ToString.Equals("Otomatis") Then
                        cbjenisgol.SelectedIndex = 0
                    Else
                        cbjenisgol.SelectedIndex = 1
                    End If

                    If dread("jenislembur").ToString.Equals("Tidak Ada") Then
                        cbjenislemb.SelectedIndex = 0
                    ElseIf dread("jenislembur").ToString.Equals("Depnaker") Then
                        cbjenislemb.SelectedIndex = 1
                    Else
                        cbjenislemb.SelectedIndex = 2
                    End If



                    cbjenislemb.Text = dread("jenislembur").ToString

                    If dread("harian").ToString.Equals("1") Then
                        charian.Checked = True
                    Else
                        charian.Checked = False
                    End If

                    If dread("tampilgroup").ToString.Equals("1") Then
                        ctampil.Checked = True
                    Else
                        ctampil.Checked = False
                    End If

                    charian_CheckedChanged(Nothing, Nothing)

                    tlaki.EditValue = Convert.ToDouble(dread("laki2").ToString)
                    tperempaun.EditValue = Convert.ToDouble(dread("perempuan").ToString)

                    If dread("jnisrange").ToString.Equals("Tidak Ada") Then
                        cbrange.SelectedIndex = 0
                    ElseIf dread("jnisrange").ToString.Equals("Harian") Then
                        cbrange.SelectedIndex = 1
                    Else
                        cbrange.SelectedIndex = 2
                    End If

                    SetNote()

                    load_option()

                Else
                    tkode.Text = ""
                    tnama.Text = ""
                    cbjenisgol.SelectedIndex = 0
                    cbjenislemb.SelectedIndex = 0
                    charian.Checked = False
                    tlaki.EditValue = 0
                    tperempaun.EditValue = 0
                    cbrange.SelectedIndex = 0
                    tnote.Text = "Perhitungan Lembur : -"

                    load_option()

                End If

            Else

                tkode.Text = ""
                tnama.Text = ""
                cbjenisgol.SelectedIndex = 0
                cbjenislemb.SelectedIndex = 0
                charian.Checked = False
                tlaki.EditValue = 0
                tperempaun.EditValue = 0
                cbrange.SelectedIndex = 0
                tnote.Text = "Perhitungan Lembur : -"

                load_option()

            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

            comd.Dispose()
            dread.Close()

        End Try

    End Sub

    Private Sub isi_detail0(ByVal kode As String)

        Dim sql As String = String.Format("select * from ms_golongan1 where saktif=1 and kode='{0}'", kode)
        Dim cn As OleDbConnection = Nothing

        grid0.DataSource = Nothing

        Try

            'open_wait()

            dv0 = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds0 = New DataSet()
            ds0 = Clsmy.GetDataSet(sql, cn)

            dvmanager0 = New DataViewManager(ds0)
            dv0 = dvmanager0.CreateDataView(ds0.Tables(0))

            ' dv1.Sort = "jmin"

            bs0 = New BindingSource
            bs0.DataSource = dv0
            bn0.BindingSource = bs0

            grid0.DataSource = bs0

            'If dv0.Count > 0 Then
            '    GridView2.SelectRow(0)
            '    GridView2_FocusedRowChanged(Nothing, Nothing)
            'End If

            'close_wait()


        Catch ex As OleDb.OleDbException
            'close_wait()
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally


            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

        End Try


    End Sub

    Private Sub isi_detail()

        grid1.DataSource = Nothing

        If IsNothing(dv0) Then
            Return
        End If

        If dv0.Count <= 0 Then
            Return
        End If

        Dim sql As String = String.Format("select * from ms_golongan2 where kode='{0}'", dv0(Me.BindingContext(bs0).Position)("kode2").ToString)
        Dim sqlfor As String = String.Format("select * from ms_golongan2 where kode in (select kode2 from ms_golongan1 where kode='{0}')", "<< New >>")

        Dim cn As OleDbConnection = Nothing

        Try

            'open_wait()

            dv1 = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds2 As DataSet
            ds2 = New DataSet()
            ds2 = Clsmy.GetDataSet(sql, cn)

            'If Not IsNothing(dtdetail) Then
            '    dtdetail.Clear()
            'End If

            'dtdetail = ds2.Tables(0)

            'ds = New DataSet()
            'ds = Clsmy.GetDataSet(sqlfor, cn)

            dvmanager = New DataViewManager(ds2)
            dv1 = dvmanager.CreateDataView(ds2.Tables(0))

            ' dv1.Sort = "jmin"

            bs1 = New BindingSource
            bs1.DataSource = dv1
            bn1.BindingSource = bs1

            grid1.DataSource = bs1

            'close_wait()


        Catch ex As OleDb.OleDbException
            'close_wait()
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally


            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

        End Try


    End Sub

    Private Sub kosongkan()
        tkode.Text = ""
        tnama.Text = ""
        cbjenisgol.SelectedIndex = 0
        cbjenislemb.SelectedIndex = 0
        charian.Checked = False
        charian_CheckedChanged(Nothing, Nothing)
        tlaki.EditValue = 0
        tperempaun.EditValue = 0
        cbrange.SelectedIndex = 0
        tnote.Text = "Perhitungan Lembur : -"

        isi_detail0(tkode.Text)
        isi_detail()

        load_option()

    End Sub

    Private Sub load_option()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = ""
            Dim cmd As OleDbCommand
            Dim drd As OleDbDataReader

            sql = String.Format("select * from sutil_absen where kd_gol='{0}'", tkode.Text.Trim)

            cmd = New OleDbCommand(sql, cn)
            drd = cmd.ExecuteReader


            stelat.Checked = False
            splang_cpat.Checked = False
            shadir.Checked = False
            stanggal.Checked = False
            sjam_msk.Checked = False
            sjam_plg.Checked = False
            sjml_tlat.Checked = False
            sjml_hdr.Checked = False
            sjml_lmbr.Checked = False
            sharian.Checked = False
            shasil.Checked = False
            smakan.Checked = False

            sgapok.Checked = False
            stunj_jab.Checked = False
            sjml_hdr2.Checked = False
            stunj_hdr.Checked = False
            stunj_akomod.Checked = False
            smakan2.Checked = False
            stamb_makan.Checked = False
            sgaji_lmbur.Checked = False
            sgaji_har.Checked = False
            sgaji_hsil.Checked = False


            If drd.Read Then
                If IsNumeric(drd("noid").ToString) Then

                    If drd("stelat").ToString.Equals("1") Then
                        stelat.Checked = True
                    Else
                        stelat.Checked = False
                    End If

                    If drd("spulang_cpt").ToString.Equals("1") Then
                        splang_cpat.Checked = True
                    Else
                        splang_cpat.Checked = False
                    End If

                    If drd("stat_hdr").ToString.Equals("1") Then
                        shadir.Checked = True
                    Else
                        shadir.Checked = False
                    End If

                    If drd("stanggal").ToString.Equals("1") Then
                        stanggal.Checked = True
                    Else
                        stanggal.Checked = False
                    End If

                    If drd("sjammasuk").ToString.Equals("1") Then
                        sjam_msk.Checked = True
                    Else
                        sjam_msk.Checked = False
                    End If

                    If drd("sjampulang").ToString.Equals("1") Then
                        sjam_plg.Checked = True
                    Else
                        sjam_plg.Checked = False
                    End If

                    If drd("sjmltelat").ToString.Equals("1") Then
                        sjml_tlat.Checked = True
                    Else
                        sjml_tlat.Checked = False
                    End If

                    If drd("sjmlkerja").ToString.Equals("1") Then
                        sjml_hdr.Checked = True
                    Else
                        sjml_hdr.Checked = False
                    End If

                    If drd("sjmllembur").ToString.Equals("1") Then
                        sjml_lmbr.Checked = True
                    Else
                        sjml_lmbr.Checked = False
                    End If

                    If drd("sharian").ToString.Equals("1") Then
                        sharian.Checked = True
                    Else
                        sharian.Checked = False
                    End If

                    If drd("shasil").ToString.Equals("1") Then
                        shasil.Checked = True
                    Else
                        shasil.Checked = False
                    End If

                    If drd("stambmakan").ToString.Equals("1") Then
                        smakan.Checked = True
                    Else
                        smakan.Checked = False
                    End If


                End If
            End If

            drd.Close()


            Dim sql2 As String = String.Format("select * from sutil_gaji where kd_gol='{0}'", tkode.Text.Trim)
            cmd = New OleDbCommand(sql2, cn)
            drd = cmd.ExecuteReader

            If drd.Read Then
                If IsNumeric(drd("noid").ToString) Then

                    If drd("sgapok").ToString.Equals("1") Then
                        sgapok.Checked = True
                    Else
                        sgapok.Checked = False
                    End If

                    If drd("stunj_jab").ToString.Equals("1") Then
                        stunj_jab.Checked = True
                    Else
                        stunj_jab.Checked = False
                    End If

                    If drd("sjmlhadir").ToString.Equals("1") Then
                        sjml_hdr2.Checked = True
                    Else
                        sjml_hdr2.Checked = False
                    End If

                    If drd("stunjhadir").ToString.Equals("1") Then
                        stunj_hdr.Checked = True
                    Else
                        stunj_hdr.Checked = False
                    End If

                    If drd("stunjakomod").ToString.Equals("1") Then
                        stunj_akomod.Checked = True
                    Else
                        stunj_akomod.Checked = False
                    End If

                    If drd("stunjmakan").ToString.Equals("1") Then
                        smakan2.Checked = True
                    Else
                        smakan2.Checked = False
                    End If

                    If drd("stambmakan").ToString.Equals("1") Then
                        stamb_makan.Checked = True
                    Else
                        stamb_makan.Checked = False
                    End If

                    If drd("sjmllembur").ToString.Equals("1") Then
                        sgaji_lmbur.Checked = True
                    Else
                        sgaji_lmbur.Checked = False
                    End If

                    If drd("sgajihar").ToString.Equals("1") Then
                        sgaji_har.Checked = True
                    Else
                        sgaji_har.Checked = False
                    End If

                    If drd("sgajihasil").ToString.Equals("1") Then
                        sgaji_hsil.Checked = True
                    Else
                        sgaji_hsil.Checked = False
                    End If

                End If
            End If

            drd.Close()

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

    Private Sub simpan()

        Dim sqlsearch As String = String.Format("select * from ms_golongan where kode='{0}'", tkode.Text.Trim)
        Dim sqlinsert As String = String.Format("insert into ms_golongan (kode,nama,jnisgol,jenislembur,harian,laki2,perempuan,jnisrange,jenisgaji,tampilgroup) values('{0}','{1}','{2}','{3}',{4},{5},{6},'{7}','{8}',{9})", _
                                                tkode.EditValue.ToString.Trim, tnama.EditValue.ToString.Trim, cbjenisgol.EditValue.ToString, cbjenislemb.EditValue.ToString, IIf(charian.Checked = True, 1, 0), Replace(tlaki.EditValue, ",", "."), Replace(tperempaun.EditValue, ",", "."), cbrange.EditValue.ToString, cbgaji.EditValue, IIf(ctampil.Checked = True, 1, 0))
        Dim sqlupdate As String = String.Format("update ms_golongan set nama='{0}',jnisgol='{1}',jenislembur='{2}',harian='{3}',laki2={4},perempuan={5},jnisrange='{6}',jenisgaji='{7}',tampilgroup={8} where kode='{9}'", _
                                                tnama.EditValue.ToString.Trim, cbjenisgol.EditValue.ToString, cbjenislemb.EditValue.ToString, IIf(charian.Checked = True, 1, 0), Replace(tlaki.EditValue, ",", "."), Replace(tperempaun.EditValue, ",", "."), cbrange.EditValue.ToString, cbgaji.EditValue, IIf(ctampil.Checked = True, 1, 0), tkode.EditValue.ToString.Trim)

        Dim cn As OleDbConnection = Nothing
        Dim dread As OleDbDataReader
        Dim comd_search As OleDbCommand
        Dim comd As OleDbCommand

        Try

            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction

            If statadd = True Then

                comd_search = New OleDbCommand(sqlsearch, cn, sqltrans)
                dread = comd_search.ExecuteReader

                If dread.HasRows Then
                    If dread.Read Then
                        close_wait()
                        MsgBox("Kode sudah ada..", vbOKOnly + vbInformation, "Informasi")
                        Exit Sub
                    Else

                        comd = New OleDbCommand(sqlinsert, cn, sqltrans)
                        comd.ExecuteNonQuery()

                        Clsmy.InsertToLog(cn, "btgol", 1, 0, 0, 0, tkode.EditValue.ToString.Trim, "", sqltrans)

                        insert_view()

                    End If
                Else
                    comd = New OleDbCommand(sqlinsert, cn, sqltrans)
                    comd.ExecuteNonQuery()

                    Clsmy.InsertToLog(cn, "btgol", 1, 0, 0, 0, tkode.EditValue.ToString.Trim, "", sqltrans)

                    insert_view()

                End If

            Else

                comd = New OleDbCommand(sqlupdate, cn, sqltrans)
                comd.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btgol", 0, 1, 0, 0, tkode.EditValue.ToString.Trim, "", sqltrans)

                update_view()

            End If

            simpan2(cn, sqltrans)
            save_option(cn, sqltrans)

            sqltrans.Commit()

            close_wait()

            MsgBox("Data disimpan", vbOKOnly + vbInformation, "Informasi")

            If statadd = True Then
                kosongkan()
                tkode.Focus()
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

            'comd_search.Dispose()
            ' comd.Dispose()
            ' dread.Close()

        End Try

    End Sub

    Private Sub simpan3(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction)

        Dim comd As OleDbCommand
        Dim minj, maxj, pric As Double
        Dim perkalian As Integer
        Dim kode0 As String

        For x = 0 To dv1.Count - 1
            If dv1(x)("id").Equals(0) Then

                minj = Convert.ToDouble(dv1(x)("jmin").ToString)
                maxj = Convert.ToDouble(dv1(x)("jmax").ToString)
                pric = Convert.ToDouble(dv1(x)("price").ToString)
                perkalian = Convert.ToDouble(dv1(x)("perkalian").ToString)

                kode0 = dv0(Me.BindingContext(bs0).Position)("kode2").ToString

                Dim sql As String = String.Format("insert into ms_golongan2 (kode,jmin,jmax,price,perkalian) values('{0}',{1},{2},{3},{4})", kode0, Replace(minj, ",", "."), Replace(maxj, ",", "."), Replace(pric, ",", "."), Replace(perkalian, ",", "."))

                comd = New OleDbCommand(sql, cn, sqltrans)
                comd.ExecuteNonQuery()

            End If
        Next

    End Sub

    Private Sub simpan2(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction)

        'Dim comd As OleDbCommand
        'Dim minj, maxj, pric As Double
        'Dim perkalian As Integer
        'Dim kode0 As String

        'For i = 0 To dtdetail.Rows.Count - 1

        '    If dtdetail.Rows(i)("id").Equals(0) Then

        '        minj = Convert.ToDouble(dtdetail.Rows(i)("jmin").ToString)
        '        maxj = Convert.ToDouble(dtdetail.Rows(i)("jmax").ToString)
        '        pric = Convert.ToDouble(dtdetail.Rows(i)("price").ToString)
        '        perkalian = Convert.ToDouble(dtdetail.Rows(i)("perkalian").ToString)

        '        kode0 = dtdetail.Rows(i)("kode").ToString

        '        Dim sql As String = String.Format("insert into ms_golongan2 (kode,jmin,jmax,price,perkalian) values('{0}',{1},{2},{3},{4})", kode0, Replace(minj, ",", "."), Replace(maxj, ",", "."), Replace(pric, ",", "."), Replace(perkalian, ",", "."))

        '        comd = New OleDbCommand(sql, cn, sqltrans)
        '        comd.ExecuteNonQuery()

        '    End If

        'Next

        Dim kode2 As String
        Dim nama2 As String

        For x = 0 To dv0.Count - 1

            If dv0(x)("id").Equals(0) Then

                kode2 = dv0(x)("kode2").ToString
                nama2 = dv0(x)("nama").ToString

                Dim sql As String = String.Format("insert into ms_golongan1 (kode,kode2,nama) values('{0}','{1}','{2}')", tkode.Text.Trim, kode2, nama2)
                Using cmd2 As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                    cmd2.ExecuteNonQuery()
                End Using

            End If

        Next

    End Sub

    Private Sub save_option(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction)

        Dim xtelat As Integer
        If stelat.Checked = True Then
            xtelat = 1
        Else
            xtelat = 0
        End If

        Dim xpulang_cpt As Integer
        If splang_cpat.Checked = True Then
            xpulang_cpt = 1
        Else
            xpulang_cpt = 0
        End If

        Dim xstat_hdr As Integer
        If shadir.Checked = True Then
            xstat_hdr = 1
        Else
            xstat_hdr = 0
        End If

        Dim xtanggal As Integer
        If stanggal.Checked = True Then
            xtanggal = 1
        Else
            xtanggal = 0
        End If

        Dim xjammasuk As Integer
        If sjam_msk.Checked = True Then
            xjammasuk = 1
        Else
            xjammasuk = 0
        End If

        Dim xjampulang As Integer
        If sjam_plg.Checked = True Then
            xjampulang = 1
        Else
            xjampulang = 0
        End If

        Dim xjmltelat As Integer
        If sjml_tlat.Checked = True Then
            xjmltelat = 1
        Else
            xjmltelat = 0
        End If

        Dim xjmlkerja As Integer
        If sjml_hdr.Checked = True Then
            xjmlkerja = 1
        Else
            xjmlkerja = 0
        End If

        Dim xjmllembur As Integer
        If sjml_lmbr.Checked = True Then
            xjmllembur = 1
        Else
            xjmllembur = 0
        End If

        Dim xharian As Integer
        If sharian.Checked = True Then
            xharian = 1
        Else
            xharian = 0
        End If

        Dim xhasil As Integer
        Dim xnilhasil As Integer
        Dim xtothasil As Integer

        If shasil.Checked = True Then

            xhasil = 1
            xnilhasil = 1
            xtothasil = 1

        Else

            xhasil = 0
            xnilhasil = 0
            xtothasil = 0

        End If

        Dim xtambmakanku As Integer
        If smakan.Checked = True Then
            xtambmakanku = 1
        Else
            xtambmakanku = 0
        End If


        Dim sqlin As String = String.Format("insert into sutil_absen (kd_gol,stelat,spulang_cpt,stat_hdr,stanggal,sjammasuk,sjampulang,sjmltelat,sjmlkerja,sjmllembur,sharian,shasil,snilhasil,stothasil,stambmakan,sket) values('{0}',{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15})", _
                                            tkode.Text.Trim, xtelat, xpulang_cpt, xstat_hdr, xtanggal, xjammasuk, xjampulang, xjmltelat, xjmlkerja, xjmllembur, xharian, xhasil, xnilhasil, xtothasil, xtambmakanku, 1)
        Dim sqlup As String = String.Format("update sutil_absen set stelat={0},spulang_cpt={1},stat_hdr={2},stanggal={3},sjammasuk={4},sjampulang={5},sjmltelat={6},sjmlkerja={7},sjmllembur={8},sharian={9},shasil={10},snilhasil={11},stothasil={12},stambmakan={13} where kd_gol='{14}'", _
                                            xtelat, xpulang_cpt, xstat_hdr, xtanggal, xjammasuk, xjampulang, xjmltelat, xjmlkerja, xjmllembur, xharian, xhasil, xnilhasil, xtothasil, xtambmakanku, tkode.Text.Trim)


        Dim sql As String = String.Format("select noid from sutil_absen where kd_gol='{0}'", tkode.Text.Trim)
        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        Dim hasil As Boolean = False

        If drd.Read Then
            If IsNumeric(drd(0).ToString) Then

                hasil = True

                Using cmdup As OleDbCommand = New OleDbCommand(sqlup, cn, sqltrans)
                    cmdup.ExecuteNonQuery()
                End Using

            End If
        End If

        drd.Close()

        If hasil = False Then
            Using cmdin As OleDbCommand = New OleDbCommand(sqlin, cn, sqltrans)
                cmdin.ExecuteNonQuery()
            End Using
        End If

        hasil = False

        Dim cgapok As Integer
        If sgapok.Checked = True Then
            cgapok = 1
        Else
            cgapok = 0
        End If

        Dim ctunj_jab As Integer
        If stunj_jab.Checked = True Then
            ctunj_jab = 1
        Else
            ctunj_jab = 0
        End If

        Dim cjmlhadir As Integer
        If sjml_hdr2.Checked = True Then
            cjmlhadir = 1
        Else
            cjmlhadir = 0
        End If

        Dim ctunj_hdr As Integer
        If stunj_hdr.Checked = True Then
            ctunj_hdr = 1
        Else
            ctunj_hdr = 0
        End If

        Dim ctunj_akomod As Integer
        If stunj_akomod.Checked = True Then
            ctunj_akomod = 1
        Else
            ctunj_akomod = 0
        End If

        Dim ctunj_makan As Integer
        If smakan2.Checked = True Then
            ctunj_makan = 1
        Else
            ctunj_makan = 0
        End If

        Dim ctamb_makan As Integer
        If stamb_makan.Checked = True Then
            ctamb_makan = 1
        Else
            ctamb_makan = 0
        End If

        Dim cjml_lmbur As Integer
        Dim cgaji_lmbur As Integer

        If sgaji_lmbur.Checked = True Then
            cjml_lmbur = 1
            cgaji_lmbur = 1
        Else
            cjml_lmbur = 0
            cgaji_lmbur = 0
        End If

        Dim cgaji_har As Integer
        If sgaji_har.Checked = True Then
            cgaji_har = 1
        Else
            cgaji_har = 0
        End If

        Dim cjml_hasil As Integer
        Dim cgaji_hsil As Integer

        If sgaji_hsil.Checked = True Then
            cjml_hasil = 1
            cgaji_hsil = 1
        Else
            cjml_hasil = 0
            cgaji_hsil = 0
        End If

        Dim sqlin_gaji As String = String.Format("insert into sutil_gaji (kd_gol,sgapok,stunj_jab,sjmlhadir,stunjhadir,stunjakomod,stunjmakan,stambmakan,sjmllembur,sgajilembur,sgajihar,sjmlhasil,sgajihasil,sket) values('{0}',{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},1)", _
                                                tkode.Text.Trim, cgapok, ctunj_jab, cjmlhadir, ctunj_hdr, ctunj_akomod, ctunj_makan, ctamb_makan, cjml_lmbur, cgaji_lmbur, cgaji_har, cjml_hasil, cgaji_hsil)
        Dim sqlup_gaji As String = String.Format("update sutil_gaji set sgapok={0},stunj_jab={1},sjmlhadir={2},stunjhadir={3},stunjakomod={4},stunjmakan={5},stambmakan={6},sjmllembur={7},sgajilembur={8},sgajihar={9},sjmlhasil={10},sgajihasil={11} where kd_gol='{12}'", _
                                                cgapok, ctunj_jab, cjmlhadir, ctunj_hdr, ctunj_akomod, ctunj_makan, ctamb_makan, cjml_lmbur, cgaji_lmbur, cgaji_har, cjml_hasil, cgaji_hsil, tkode.Text.Trim)

        Dim sql_gaji As String = String.Format("select noid from sutil_gaji where kd_gol='{0}'", tkode.Text.Trim)
        Dim cmd_gaji As OleDbCommand = New OleDbCommand(sql_gaji, cn, sqltrans)
        Dim drd_gaji As OleDbDataReader = cmd_gaji.ExecuteReader

        If drd_gaji.Read Then
            If IsNumeric(drd_gaji(0).ToString) Then

                hasil = True

                Using cmdup_gaji As OleDbCommand = New OleDbCommand(sqlup_gaji, cn, sqltrans)
                    cmdup_gaji.ExecuteNonQuery()
                End Using

            End If
        End If

        drd.Close()

        If hasil = False Then
            Using cmdin_gaji As OleDbCommand = New OleDbCommand(sqlin_gaji, cn, sqltrans)
                cmdin_gaji.ExecuteNonQuery()
            End Using
        End If


    End Sub

    Private Sub insert_view()

        Dim orow As DataRowView = dv.AddNew

        orow("kode") = tkode.Text.Trim
        orow("nama") = tnama.Text.Trim
        orow("jnisgol") = cbjenisgol.EditValue.ToString
        orow("jenislembur") = cbjenislemb.EditValue.ToString

        dv.EndInit()

    End Sub

    Private Sub update_view()

        dv(position)("nama") = tnama.Text.Trim
        dv(position)("jnisgol") = cbjenisgol.EditValue.ToString
        dv(position)("jenislembur") = cbjenislemb.EditValue.ToString

    End Sub

    Private Sub hapus_detail()

        Dim cn As OleDbConnection = Nothing
        Dim comd As OleDbCommand

        Dim noid As String = dv1(Me.BindingContext(bs1).Position)("id").ToString
        Dim jmin As Integer = Integer.Parse(dv1(Me.BindingContext(bs1).Position)("jmin").ToString)
        Dim jmax As Integer = Integer.Parse(dv1(Me.BindingContext(bs1).Position)("jmax").ToString)
        Dim price As Double = Double.Parse(dv1(Me.BindingContext(bs1).Position)("price").ToString)
        Dim perkalian As Integer = Integer.Parse(dv1(Me.BindingContext(bs1).Position)("perkalian").ToString)
        Dim kode As String = dv1(Me.BindingContext(bs1).Position)("kode").ToString

        If noid.Equals("") Then
            noid = 0
        End If

        Try

            'Dim rows() As DataRow = dtdetail.Select(String.Format("id={0} and jmin={1} and jmax={2} and price={3} and perkalian={4} and kode='{5}'", _
            '    noid, jmin, jmax, price, perkalian, kode))

            'If rows.Length > 0 Then
            '    rows(0).Delete()
            '    dtdetail.AcceptChanges()
            'End If


            'For i As Integer = 0 To dtdetail.Rows.Count - 1

            '    Dim noid2 As String = dtdetail.Rows(i)("id").ToString
            '    Dim jmin2 As Integer = Integer.Parse(dtdetail.Rows(i)("jmin").ToString)
            '    Dim jmax2 As Integer = Integer.Parse(dtdetail.Rows(i)("jmax").ToString)
            '    Dim price2 As Double = Double.Parse(dtdetail.Rows(i)("price").ToString)
            '    Dim perkalian2 As Integer = Integer.Parse(dtdetail.Rows(i)("perkalian").ToString)
            '    Dim kode2 As String = dtdetail.Rows(i)("kode").ToString

            '    If noid.Equals(noid2) And jmin = jmin2 And jmax = jmax2 And price = price2 And perkalian = perkalian2 And kode.Equals(kode2) Then
            '        dtdetail.Rows(i).Delete()
            '        Exit For
            '    End If

            'Next

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction

            Dim sql As String = String.Format("delete from ms_golongan2 where id={0}", noid)
            comd = New OleDbCommand(sql, cn, sqltrans)
            comd.ExecuteNonQuery()

            sqltrans.Commit()

            dv1.Delete(bs1.Position)

            isi_detail()

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

    Private Sub hapus_detail0()

        Dim cn As OleDbConnection = Nothing
        Dim comd As OleDbCommand

        Dim noid As String = dv0(Me.BindingContext(bs0).Position)("id").ToString
        If noid.Equals("") Then
            noid = 0
        End If

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction

            Dim sql As String = String.Format("update ms_golongan1 set saktif=0 where id={0}", noid)
            comd = New OleDbCommand(sql, cn, sqltrans)
            comd.ExecuteNonQuery()

            'Dim sql2 As String = String.Format("delete from ms_golongan2 where kode='{0}'", dv0(Me.BindingContext(bs0).Position)("kode2").ToString)
            'Using cmd2 As OleDbCommand = New OleDbCommand(sql2, cn, sqltrans)
            '    cmd2.ExecuteNonQuery()
            'End Using

            sqltrans.Commit()

            If noid = 0 Then

                For i As Integer = 0 To dtdetail.Rows.Count - 1

                    Dim kode2 As String = dtdetail.Rows(i)("kode").ToString

                    If dv0(Me.BindingContext(bs0).Position)("kode2").ToString.Equals(kode2) Then
                        dtdetail.Rows(i).Delete()
                    End If

                Next

                dv0.Delete(bs0.Position)

            Else
                isi_detail0(tkode.Text)
            End If

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

    Private Sub isi_griddetail(ByVal kode As String)

        If IsNothing(dv1) Then
            Return
        End If

        dv1.Table.Clear()

        Dim result() As DataRow = dtdetail.Select(String.Format("kode='{0}'", kode))

        For Each row As DataRow In result
            Dim orow As DataRowView = dv1.AddNew
            orow("id") = row("id").ToString
            orow("kode") = row("kode").ToString
            orow("jmin") = Integer.Parse(row("jmin").ToString)
            orow("jmax") = Integer.Parse(row("jmax").ToString)
            orow("price") = Double.Parse(row("price").ToString)
            orow("perkalian") = Integer.Parse(row("perkalian").ToString)

            dv1.EndInit()

        Next

        dv1.Sort = "jmin"

    End Sub

    Private Sub cbjenisgol_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbjenisgol.SelectedIndexChanged

        If cbjenisgol.SelectedIndex = 0 Then
            cbjenislemb.Enabled = True

            charian.Enabled = True
            charian_CheckedChanged(Nothing, Nothing)

            '  tlaki.Enabled = True
            '   tperempaun.Enabled = True
            cbrange.Enabled = False

            ' SplitContainerControl1.Panel2.Enabled = False

            SplitContainerControl1.Panel2.Enabled = False

        Else

            cbjenislemb.Enabled = False

            charian.Enabled = False
            charian.Checked = False
            charian_CheckedChanged(Nothing, Nothing)

            ' tlaki.Enabled = False
            tlaki.EditValue = 0
            '   tperempaun.Enabled = False
            tperempaun.EditValue = 0
            cbrange.Enabled = True

            ' SplitContainerControl1.Panel2.Enabled = True

            SplitContainerControl1.Panel2.Enabled = True

        End If

        isi_detail0(tkode.Text.Trim)
        ' isi_detail()

        SetNote()

    End Sub

    Private Sub charian_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles charian.CheckedChanged
        If charian.Checked = True Then
            tlaki.Enabled = True
            tperempaun.Enabled = True
            charian.Text = "Ya"
        Else
            tlaki.Enabled = False
            tperempaun.Enabled = False
            charian.Text = "Tidak"
        End If

        SetNote()

    End Sub

    Private Sub fgol_otomat2_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        If statadd = True Then
            tkode.Focus()
        Else
            tnama.Focus()
        End If
    End Sub

    Private Sub fgol_otomat2_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If statadd = True Then
            kosongkan()
            tkode.Enabled = True
            cbjenisgol.Enabled = True
        Else
            isi()

            tkode.Enabled = False
            cbjenisgol.Enabled = False

        End If

        isi_detail0(tkode.Text.Trim)
        isi_detail()

        If statadd = False Then

            If Not IsNothing(dv0) Then
                GridView2.SelectRow(0)
                GridView2_FocusedRowChanged(sender, Nothing)
            End If

        End If

    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click

        If tkode.Text.Trim.Length = 0 Then
            MsgBox("Kode harus diisi..", vbOKOnly + vbInformation, "Informasi")
            tkode.Focus()
            Exit Sub
        End If

        If tnama.Text.Trim.Length = 0 Then
            MsgBox("Nama harus diisi..", vbOKOnly + vbInformation, "Informasi")
            tnama.Focus()
            Exit Sub
        End If

        If cbjenisgol.SelectedIndex = 1 Then
            If dv1.Count = 0 Then
                MsgBox("Range kriteria harus diisi...", vbOKOnly + vbInformation, "Informasi")
                Exit Sub
            End If
        End If

        simpan()

    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub tsdel_Click(sender As System.Object, e As System.EventArgs) Handles tsdel.Click

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If

        'If MsgBox("Yakin ada dihapus ?", vbYesNo + vbQuestion, "Konfirmasi") = vbNo Then
        '    Exit Sub
        'End If

        hapus_detail()

    End Sub

    Private Sub tsdel0_Click(sender As System.Object, e As System.EventArgs) Handles tsdel0.Click

        If dv0.Count < 1 Then
            Exit Sub
        End If

        'If MsgBox("Yakin ada dihapus ?", vbYesNo + vbQuestion, "Konfirmasi") = vbNo Then
        '    Exit Sub
        'End If

        hapus_detail0()

    End Sub


    Private Sub tsadd_Click(sender As System.Object, e As System.EventArgs) Handles tsadd.Click

        If cbrange.SelectedIndex = 0 Then
            MsgBox("Jenis range harus ada...", vbOKOnly + vbInformation, "Informasi")
            cbrange.Focus()
            Exit Sub
        End If

        If IsNothing(dv0) Then
            Return
        End If

        If dv0.Count = 0 Then
            Return
        End If

        If cbrange.SelectedIndex = 1 Then
            If dv1.Count > 0 Then
                Exit Sub
            End If
        End If

        Using fdet As New fgol_otomat3 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .jenisrange = cbrange.Text.Trim.ToString, .jeniskerja = dv0(Me.BindingContext(bs0).Position)("nama").ToString, .kode0 = dv0(Me.BindingContext(bs0).Position)("kode2").ToString, .dt1 = dtdetail}
            fdet.ShowDialog(Me)

            If fdet.get_statada = True Then

                Dim cn As OleDbConnection = Nothing
                Dim sqltrans As OleDbTransaction = Nothing

                Try

                    cn = New OleDbConnection
                    cn = Clsmy.open_conn

                    sqltrans = cn.BeginTransaction

                    simpan3(cn, sqltrans)

                    sqltrans.Commit()

                    isi_detail()

                Catch ex As Exception
                    sqltrans.Rollback()
                    MsgBox(ex.ToString)
                Finally

                    If Not cn Is Nothing Then
                        If cn.State = ConnectionState.Open Then
                            cn.Close()
                        End If
                    End If
                End Try

            End If

        End Using

    End Sub

    Private Sub tsadd0_Click(sender As System.Object, e As System.EventArgs) Handles tsadd0.Click

        If cbrange.SelectedIndex = 0 Then
            MsgBox("Jenis range harus ada...", vbOKOnly + vbInformation, "Informasi")
            cbrange.Focus()
            Exit Sub
        End If

        If cbrange.SelectedIndex = 1 Then
            If dv1.Count > 0 Then
                Exit Sub
            End If
        End If

        Using fdet As New fgol_otomat4 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv0}
            fdet.ShowDialog(Me)

            If fdet.get_statada = True Then

                Dim cn As OleDbConnection = Nothing
                Dim sqltrans As OleDbTransaction = Nothing

                Try

                    cn = New OleDbConnection
                    cn = Clsmy.open_conn

                    sqltrans = cn.BeginTransaction

                    simpan2(cn, sqltrans)

                    sqltrans.Commit()

                    isi_detail0(tkode.Text)

                Catch ex As Exception
                    sqltrans.Rollback()
                    MsgBox(ex.ToString)
                Finally

                    If Not cn Is Nothing Then
                        If cn.State = ConnectionState.Open Then
                            cn.Close()
                        End If
                    End If
                End Try

            End If

        End Using

    End Sub

    Private Sub cbjenislemb_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbjenislemb.SelectedIndexChanged
        SetNote()
    End Sub

    Private Sub ctampil_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ctampil.CheckedChanged
        If ctampil.Checked = True Then
            ctampil.Text = "Ya"
        Else
            ctampil.Text = "Tidak"
        End If
    End Sub

    Private Sub GridView2_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView2.FocusedRowChanged

        If IsNothing(dv0) Then
            Return
        End If

        If dv0.Count = 0 Then
            Return
        End If

        Dim kode As String = dv0(Me.BindingContext(bs0).Position)("kode2").ToString

        isi_detail()

    End Sub

    Private Sub GridView2_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles GridView2.RowCellClick
        GridView2_FocusedRowChanged(sender, Nothing)
    End Sub

End Class