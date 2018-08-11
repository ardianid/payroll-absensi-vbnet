Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Imports DevExpress.XtraReports.UI

Public Class fgaji_blnan1

    Dim jnis_hit As Integer = 0

    Dim sql As String
    Dim sqlhari As String
    Dim statall As Boolean
    Dim periode As String

    Private Function cek_periode_ongaji() As Boolean

        Dim hasil As Boolean = True

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select distinct tanggal1,tanggal2 from tr_gaji where not(calc_by='Mingguan') and tanggal1>='{0}' and tanggal2<='{1}'", convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue))
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            Dim ctgl1 As Boolean = True
            Dim ctgl2 As Boolean = True

            While drd.Read

                If DateValue(drd(0).ToString) <> DateValue(ttgl1.EditValue) Then
                    ctgl1 = False
                    Exit While
                End If

                If DateValue(drd(1).ToString) <> DateValue(ttgl2.EditValue) Then
                    ctgl2 = False
                    Exit While
                End If

            End While
            drd.Close()

            If ctgl1 = False Or ctgl2 = False Then
                hasil = False
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

        Return hasil

    End Function

    Private Function cek_tahunbulanminggu() As Boolean

        Dim hasil As Boolean = True

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select distinct tahun,bulan,minggu from tr_gaji where not(calc_by='Mingguan') and tanggal1>='{0}' and tanggal2<='{1}'", convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue))
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            Dim jtahun As Integer = 0
            Dim jbln As Integer = 0

            While drd.Read

                If Integer.Parse(ttahun.EditValue) = Integer.Parse(drd("tahun").ToString) Then
                    jtahun = jtahun + 1
                End If

                If Integer.Parse(tbln.EditValue) = Integer.Parse(drd("bulan").ToString) Then
                    jbln = jbln + 1
                End If

            End While
            drd.Close()

            If jtahun > 1 Or jbln > 1 Then
                hasil = False
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

        Return hasil

    End Function

    Private Sub loadGolongan()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = "select kode,nama from ms_golongan where jenisgaji='Bulanan' and tampilgroup=1 and saktif=1"

            Dim dsgol As New DataSet
            dsgol = Clsmy.GetDataSet(sql, cn)

            Dim dtgol As DataTable = dsgol.Tables(0)

            cbgol.Properties.DataSource = dtgol

            If dtgol.Rows.Count > 0 Then
                cbgol.ItemIndex = 0
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

    Private Sub delete_trgajisebelumnya(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction)

        Dim sql As String = String.Format("delete from tr_gaji where not(calc_by='Mingguan') and tanggal1='{0}' and tanggal2='{1}' and kdgol='{2}'", convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue), cbgol.EditValue)
        Using cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
            cmd.ExecuteNonQuery()
        End Using

    End Sub

    Private Function hariaktif_kalender(ByVal cn As OleDbConnection) As Integer

        Dim jmlhari As Integer = 0

        Dim sqlhari As String = String.Format("select COUNT(tanggal) as jml from ms_kalender where libur=0 and tanggal >='{0}' and tanggal <='{1}'", convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue))
        Dim cmd As OleDbCommand = New OleDbCommand(sqlhari, cn)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        If drd.Read Then
            If IsNumeric(drd(0).ToString) Then
                jmlhari = Integer.Parse(drd(0).ToString)
            End If
        End If
        drd.Close()

        Return jmlhari

    End Function

    Private Sub cek_jnis_hit()

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqlhit As String = "select jnis_hit_harian from ms_awalshift"
            Dim cmdhit As OleDbCommand = New OleDbCommand(sqlhit, cn)
            Dim drdhit As OleDbDataReader = cmdhit.ExecuteReader

            If drdhit.Read Then
                If IsNumeric(drdhit(0).ToString) Then
                    jnis_hit = Integer.Parse(drdhit(0).ToString)
                End If
            End If
            drdhit.Close()

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

    Private Sub simpanto_trgaji()

        open_wait()

        Dim hasil As Integer = 1
        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim hariaktif_kalender1 As Integer = hariaktif_kalender(cn)
            If hariaktif_kalender1 = 0 Then
                MsgBox("Isi dulu kalender kerja...", vbOKOnly + vbInformation, "Informasi")
                Return
            End If

            Dim sql As String = String.Format("SELECT ms_karyawan.nip,ms_karyawan.nama,ms_karyawan.depart,SUM(tr_hadir.jam1) + SUM(tr_hadir.jam2) + SUM(tr_hadir.jam3) + SUM(tr_hadir.jam4) as jmllembur, " & _
            "sum(tr_hadir.lemburperjam) as totlembur,0 as tothasil, 0 as tot_harian,SUM(tr_hadir.uangmakan) as uangmakan, SUM(tr_hadir.tambmakan) as tambmakanlembur,0 as jmlhasil, " & _
            "ms_karyawan.tunj_jabatan, ms_karyawan.tunj_kehadiran, ms_karyawan.tunj_akomodasi, ms_karyawan.tunj_makan,ms_karyawan.gapok,ms_karyawan.tgl_mulai,ms_karyawan.standby,ms_karyawan.pot_jamsos,ms_karyawan.sewa_kend " & _
            "FROM  tr_hadir INNER JOIN ms_karyawan ON tr_hadir.nip = ms_karyawan.nip " & _
            "WHERE ms_karyawan.aktif=1 and tr_hadir.kdgol='{0}' and tr_hadir.tanggal>='{1}' and tr_hadir.tanggal<='{2}' " & _
            "GROUP BY ms_karyawan.nip,ms_karyawan.nama,ms_karyawan.depart,ms_karyawan.tunj_jabatan, " & _
            "ms_karyawan.tunj_kehadiran,ms_karyawan.tunj_akomodasi,ms_karyawan.tunj_makan,ms_karyawan.gapok,ms_karyawan.tgl_mulai,ms_karyawan.standby,ms_karyawan.pot_jamsos,ms_karyawan.sewa_kend", cbgol.EditValue, convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue))

            Dim dssel As DataSet = New DataSet
            dssel = Clsmy.GetDataSet(sql, cn)
            Dim dtsel As DataTable = dssel.Tables(0)

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction()

            delete_trgajisebelumnya(cn, sqltrans)

            For i As Integer = 0 To dtsel.Rows.Count - 1

                Dim nnip As String = dtsel(i)("nip").ToString
                Dim nama As String = dtsel(i)("nama").ToString
                Dim depart As String = dtsel(i)("depart").ToString
                Dim jmllembur As Double = Double.Parse(dtsel(i)("jmllembur").ToString)
                Dim totlembur As Double = Double.Parse(dtsel(i)("totlembur").ToString)
                Dim tothasil As Double = Double.Parse(dtsel(i)("tothasil").ToString)
                Dim totharian As Double = Double.Parse(dtsel(i)("tot_harian").ToString)
                Dim tambmakanlembur As Double = Double.Parse(dtsel(i)("tambmakanlembur").ToString)
                Dim tunjjabatan As Double = Double.Parse(dtsel(i)("tunj_jabatan").ToString)
                Dim tunjkehadiran As Double = Double.Parse(dtsel(i)("tunj_kehadiran").ToString)
                Dim tunjakomodasi As Double = Double.Parse(dtsel(i)("tunj_akomodasi").ToString)
                Dim tunjmakan As Double = Double.Parse(dtsel(i)("tunj_makan").ToString)
                Dim jmlhasil As Integer = Integer.Parse(dtsel(i)("jmlhasil").ToString)
                Dim sgapok As Double = Double.Parse(dtsel(i)("gapok").ToString)
                Dim standby As Double = Double.Parse(dtsel(i)("standby").ToString)
                Dim pot_jamsos As Double = Double.Parse(dtsel(i)("pot_jamsos").ToString)
                Dim totuangmakan As Double = Double.Parse(dtsel(i)("uangmakan").ToString)
                Dim sewakend As Double = Double.Parse(dtsel(i)("sewa_kend").ToString)

                '' cek hari minggu ''

                Dim sqlmgu As String = String.Format("select COUNT(tr_hadir.tanggal) as jml from tr_hadir inner join ms_kalender on tr_hadir.tanggal=ms_kalender.tanggal " & _
                "where ms_kalender.hari='MINGGU' and not(ms_kalender.jenislibur='Libur Hari Besar') and tr_hadir.tanggal>='{0}' and tr_hadir.tanggal<='{1}' and tr_hadir.nip='{2}' and tr_hadir.kdgol='{3}'", _
                convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue), nnip, cbgol.EditValue)

                Dim cmdmgu As OleDbCommand = New OleDbCommand(sqlmgu, cn, sqltrans)
                Dim drdmgu As OleDbDataReader = cmdmgu.ExecuteReader

                Dim jmlgu As Integer = 0
                If drdmgu.Read Then
                    If IsNumeric(drdmgu(0).ToString) Then
                        jmlgu = Integer.Parse(drdmgu(0).ToString)
                    End If
                End If
                drdmgu.Close()

                Dim totstandby As Double = 0
                If jmlgu > 0 And standby > 0 Then
                    totstandby = jmlgu * standby
                End If

                ''-----

                '' cek angsuran bon
                Dim sqlbon As String = String.Format("select SUM(jumlah) as jmlbayar from ms_angsur where tanggal>='{0}' and tanggal<='{1}' and nip='{2}'", convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue), nnip)
                Dim cmdbon As OleDbCommand = New OleDbCommand(sqlbon, cn, sqltrans)
                Dim drdbon As OleDbDataReader = cmdbon.ExecuteReader

                Dim jmlbon As Double = 0
                If drdbon.Read Then
                    If IsNumeric(drdbon(0).ToString) Then
                        jmlbon = Double.Parse(drdbon(0).ToString)
                    End If
                End If
                drdbon.Close()

                '-----


                Dim jmlhari As Integer = 0

                Dim sqlhariaktif As String = ""

                If jnis_hit = 4 Then ' kalau global
                    sqlhariaktif = String.Format("select COUNT(distinct tr_hadir.tanggal) as jmlmasuk from tr_hadir inner join ms_kalender on tr_hadir.tanggal=ms_kalender.tanggal where  tr_hadir.stat in ('HADIR','MENGINAP') and tr_hadir.step=2 and tr_hadir.skalk=1 and tr_hadir.tanggal>='{0}' and tr_hadir.tanggal <='{1}' and tr_hadir.nip='{2}'", convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue), nnip)
                Else
                    sqlhariaktif = String.Format("select COUNT(distinct tr_hadir.tanggal) as jmlmasuk from tr_hadir inner join ms_kalender on tr_hadir.tanggal=ms_kalender.tanggal where  tr_hadir.stat in ('HADIR','SAKIT','CUTI','MENGINAP') and tr_hadir.step=2 and tr_hadir.skalk=1 and tr_hadir.tanggal>='{0}' and tr_hadir.tanggal <='{1}' and tr_hadir.nip='{2}'", convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue), nnip)
                End If

                Dim cmd_hr As OleDbCommand = New OleDbCommand(sqlhariaktif, cn, sqltrans)
                Dim dr_hr As OleDbDataReader = cmd_hr.ExecuteReader
                If dr_hr.Read Then
                    If IsNumeric(dr_hr(0).ToString) Then
                        jmlhari = Integer.Parse(dr_hr(0).ToString)
                    End If
                End If
                dr_hr.Close()


                Dim jmlhari_mk As Integer = 0
                If DateValue(ttgl1.EditValue) <= DateValue("28/03/2015") Then
                    Dim sqlhariaktif_mk As String = String.Format("select COUNT(distinct tr_hadir.tanggal) as jmlmasuk from tr_hadir inner join ms_kalender on tr_hadir.tanggal=ms_kalender.tanggal where tr_hadir.uangmakan=0 and tr_hadir.stat in ('HADIR','SAKIT','CUTI','MENGINAP') and tr_hadir.step=2 and tr_hadir.tanggal>='{0}' and tr_hadir.tanggal <='{1}' and tr_hadir.nip='{2}'", convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue), nnip)
                    Dim cmd_hr_mk As OleDbCommand = New OleDbCommand(sqlhariaktif_mk, cn, sqltrans)
                    Dim dr_hr_mk As OleDbDataReader = cmd_hr_mk.ExecuteReader
                    If dr_hr_mk.Read Then
                        If IsNumeric(dr_hr_mk(0).ToString) Then
                            jmlhari_mk = Integer.Parse(dr_hr_mk(0).ToString)
                        End If
                    End If
                    dr_hr_mk.Close()
                End If

                tunjmakan = (tunjmakan * jmlhari_mk) + totuangmakan

                Dim jmlalpha As Integer = 0
                Dim sqlalpha As String = String.Format("select COUNT(distinct tr_hadir.tanggal) as jmlalp from tr_hadir where tr_hadir.stat like 'ALPHA%' and tr_hadir.step=2 and tr_hadir.tanggal>='{0}' and tr_hadir.tanggal <='{1}' and tr_hadir.nip='{2}'", convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue), nnip)
                Dim cmdalpha As OleDbCommand = New OleDbCommand(sqlalpha, cn, sqltrans)
                Dim drdalpha As OleDbDataReader = cmdalpha.ExecuteReader
                If drdalpha.Read Then
                    If IsNumeric(drdalpha(0).ToString) Then
                        jmlalpha = Integer.Parse(drdalpha(0).ToString)
                    End If
                End If


                If jmlhari >= hariaktif_kalender1 Then
                Else
                    tunjkehadiran = 0
                End If

                Dim potongan_gaji As Double = 0

                If jnis_hit = 1 Then '' visi
                    If jmlalpha > 0 Then
                        potongan_gaji = ((sgapok + tunjjabatan) / 25) * jmlalpha
                    Else
                        potongan_gaji = 0
                    End If
                End If


                Dim jml_potcuti As Double = 0

                Dim sql_potcuti As String = String.Format("select SUM(jumlah) as jumlah from tr_potcuti where tanggal>='{0}' and tanggal<='{1}' and nip='{2}'", convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue), nnip)
                Dim cmd_potcuti As OleDbCommand = New OleDbCommand(sql_potcuti, cn, sqltrans)
                Dim drd_potcuti As OleDbDataReader = cmd_potcuti.ExecuteReader

                If drd_potcuti.Read Then
                    If IsNumeric(drd_potcuti(0).ToString) Then
                        jml_potcuti = Double.Parse(drd_potcuti(0).ToString)
                    End If
                End If
                drd_potcuti.Close()

                Dim tglmulai As String = dtsel(i)("tgl_mulai").ToString
                Dim sbln As String = tbln.EditValue.ToString
                If sbln.Length = 1 Then
                    sbln = String.Format("0{0}", sbln)
                End If
                Dim tglawalbulan = String.Format("01/{0}/{1}", sbln, ttahun.Text.Trim)

                If IsDate(tglmulai) Then
                    If DateValue(tglmulai) > DateValue(tglawalbulan) Then
                        sgapok = (sgapok + tunjjabatan) / 25
                        sgapok = sgapok * jmlhari
                        potongan_gaji = 0
                    End If
                End If

                Dim sqlin As String = String.Format("insert into tr_gaji(nip,calc_by,tahun,bulan,minggu,tunj_jab,tunj_hadir,jmlhadir,tunj_akomod,tunj_makan,tunj_makanlembur,jmllembur,gaji_lembur,jml_hasil,tot_hasil,tot_harian,tanggal1,tanggal2,gapok,kurangi_gapok,tot_standby,angsuran_bon,pot_jamsos,pot_cuti,tunj_sewakend,kdgol,depart) " & _
                "values('{0}','Bulanan',{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},'{15}','{16}',{17},{18},{19},{20},{21},{22},{23},'{24}','{25}')", _
                nnip, ttahun.EditValue, tbln.EditValue, 0, Replace(tunjjabatan, ",", "."), Replace(tunjkehadiran, ",", "."), Replace(jmlhari, ",", "."), Replace(tunjakomodasi, ",", "."), Replace(tunjmakan, ",", "."), Replace(tambmakanlembur, ",", "."), _
                 Replace(jmllembur, ",", "."), Replace(totlembur, ",", "."), Replace(jmlhasil, ",", "."), Replace(tothasil, ",", "."), Replace(totharian, ",", "."), convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue), Replace(sgapok, ",", "."), Replace(potongan_gaji, ",", "."), Replace(totstandby, ",", "."), Replace(jmlbon, ",", "."), Replace(pot_jamsos, ",", "."), Replace(jml_potcuti, ",", "."), Replace(sewakend, ",", "."), cbgol.EditValue, depart)

                Using cmd1 As OleDbCommand = New OleDbCommand(sqlin, cn, sqltrans)
                    cmd1.ExecuteNonQuery()
                End Using

            Next

            sqltrans.Commit()
            close_wait()

        Catch ex As Exception
            close_wait()
            hasil = 0
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

        If hasil = 1 Then
            '   If MsgBox("Anda ingin langsung melihat hasilnya ?", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.Yes Then
            load_data()
            'End If
        End If

    End Sub

    Private Function visible_on(ByVal sql As String) As String

        Dim hasil As String = sql

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql2 As String = String.Format("select snonkary from ms_usersys where namauser='{0}'", userprog)
            Dim cmd As OleDbCommand = New OleDbCommand(sql2, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If Integer.Parse(drd(0).ToString) = 1 Then
                    hasil = sql.Replace("'' as statin", "'all' as statin")
                End If
            End If
            drd.Close()

        Catch ex As Exception
            hasil = "error"
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

    Private Function add_jmlhari(ByVal sql As String) As String

        Dim sqlhasil As String = sql

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim hariaktif_kalender1 As Integer = hariaktif_kalender(cn)

            sqlhasil = sql.Replace("0 as jmlhari", String.Format("{0} as jmlhari", hariaktif_kalender1))
          
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

        Return sqlhasil

    End Function

    Private Sub load_data()

        statall = False

        sql = String.Format("select ms_karyawan.depart,tr_gaji.nip,ms_karyawan.nama,tr_gaji.jmllembur,tr_gaji.gaji_lembur as totlembur, " & _
        "tr_gaji.tunj_jab as tunj_jabatan, tr_gaji.tunj_hadir as tunj_kehadiran,tr_gaji.tunj_akomod as tunj_akomodasi, " & _
        "tr_gaji.tunj_makan + tr_gaji.tunj_makanlembur as tunj_makan,tr_gaji.jmlhadir,tr_gaji.gapok,tr_gaji.kurangi_gapok,'' as statin,tr_gaji.pot_cuti,tr_gaji.tunj_sewakend,0 as jmlhari,tr_gaji.pot_jamsos " & _
        "from tr_gaji inner join ms_karyawan on tr_gaji.nip=ms_karyawan.nip " & _
        "where tr_gaji.calc_by='Bulanan' and tr_gaji.tanggal1='{0}' and tr_gaji.tanggal2='{1}' and ms_karyawan.kdgol='{2}'", convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue), cbgol.EditValue)

        Dim sql3 As String = String.Format("select ms_karyawan.depart,tr_gaji.nip,ms_karyawan.nama,tr_gaji.jmllembur,tr_gaji.gaji_lembur as totlembur, " & _
        "tr_gaji.tunj_jab as tunj_jabatan, tr_gaji.tunj_hadir as tunj_kehadiran,tr_gaji.tunj_akomod as tunj_akomodasi, " & _
        "tr_gaji.tunj_makan + tr_gaji.tunj_makanlembur as tunj_makan,tr_gaji.jmlhadir,tr_gaji.gapok, " & _
        "tr_gaji.kurangi_gapok,'' as statin,tr_gaji.tot_standby,tr_gaji.angsuran_bon,tr_gaji.pot_jamsos,tr_gaji.pot_cuti,tr_gaji.tunj_sewakend " & _
        "from tr_gaji inner join ms_karyawan on tr_gaji.nip=ms_karyawan.nip " & _
        "where tr_gaji.calc_by='Bulanan' and tr_gaji.tanggal1='{0}' and tr_gaji.tanggal2='{1}' and ms_karyawan.kdgol='{2}'", convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue), cbgol.EditValue)

        Dim sql1 As String = ""

        If jnis_hit = 1 Or jnis_hit = 2 Then
            '' sql1 = add_jmlhari(sql)
            sql1 = visible_on(sql)
        ElseIf jnis_hit = 3 Or jnis_hit = 4 Then
            sql1 = visible_on(sql3)
        End If

        If sql1.Equals("error") Then
            Return
        End If

        If Not sql1.Equals(sql) Then
            statall = True
        End If

        sql = sql1

        sqlhari = String.Format("select COUNT(tanggal) from ms_kalender where tanggal>='{0}' and tanggal<='{1}' and libur=0", convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue))

        periode = String.Format("Periode : {0} s.d {1}", convert_date_to_ind(ttgl1.EditValue), convert_date_to_ind(ttgl2.EditValue))

        Cursor = Cursors.WaitCursor

        load_print()

        'Dim fs As New fgaji_blnan11 With {.WindowState = FormWindowState.Maximized, .sql = sql, .periode = periode, .statall = statall, .jenislap = jnis_hit, .sqlhari = sqlhari}
        'fs.ShowDialog()

        Cursor = Cursors.Default

    End Sub

    Private Sub load_print1(ByVal cn As OleDbConnection)

        Dim ds As DataSet = New ds_gajibulanan1
        ds = Clsmy.GetDataSet(sql, cn)

        If statall = False Then
            Dim sqlvi As String = String.Format("select nip from ms_usersys4 where namauser='{0}'", userprog)
            Dim cmd As OleDbCommand = New OleDbCommand(sqlvi, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            While drd.Read
                Dim rows() As DataRow = ds.Tables(0).Select(String.Format("nip='{0}'", drd("nip").ToString))

                If rows.Length > 0 Then
                    If rows(0)("nip").ToString.Equals(drd("nip").ToString) Then
                        rows(0)("statin") = "off"
                    End If
                End If

            End While
            drd.Close()
        End If

        Dim sqlhari As String = String.Format("select nip,COUNT(distinct tanggal) as jml from tr_hadir where stat like 'ALPHA%' and tanggal>='{0}' and tanggal<='{1}' group by nip", convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue))
        Dim dshari As DataSet = New DataSet
        dshari = Clsmy.GetDataSet(sqlhari, cn)
        Dim dthari As DataTable = dshari.Tables(0)

        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            Dim nip As String = ds.Tables(0)(i)("nip").ToString

            Dim orows() As DataRow = dthari.Select(String.Format("nip='{0}'", nip))
            If orows.Length > 0 Then
                ds.Tables(0)(i)("jmlhari") = orows(0)("jml").ToString
            End If

        Next

        Dim ops As New System.Drawing.Printing.PrinterSettings
        Dim rrekap As New r_gajibulanan() With {.DataSource = ds.Tables(0)}
        rrekap.xperiode.Text = periode
        rrekap.xuser.Text = String.Format("{0} {1}", userprog, DateTime.Now)
        rrekap.DataMember = rrekap.DataMember
        rrekap.PrinterName = ops.PrinterName
        rrekap.CreateDocument(True)

        PrintControl1.PrintingSystem = rrekap.PrintingSystem


    End Sub

    Private Sub load_print3(ByVal cn As OleDbConnection)

        Dim jmlhari As Integer = 0
        Dim cmdhari As OleDbCommand = New OleDbCommand(sqlhari, cn)
        Dim drdhari As OleDbDataReader = cmdhari.ExecuteReader
        If drdhari.Read Then
            If IsNumeric(drdhari(0).ToString) Then
                jmlhari = Integer.Parse(drdhari(0).ToString)
            End If
        End If
        drdhari.Close()

        Dim ds As DataSet = New ds_gajibulanan3
        ds = Clsmy.GetDataSet(sql, cn)

        If statall = False Then
            Dim sqlvi As String = String.Format("select nip from ms_usersys4 where namauser='{0}'", userprog)
            Dim cmd As OleDbCommand = New OleDbCommand(sqlvi, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            While drd.Read
                Dim rows() As DataRow = ds.Tables(0).Select(String.Format("nip='{0}'", drd("nip").ToString))

                If rows.Length > 0 Then
                    If rows(0)("nip").ToString.Equals(drd("nip").ToString) Then
                        rows(0)("statin") = "off"
                    End If
                End If

            End While
            drd.Close()
        End If

        Dim ops As New System.Drawing.Printing.PrinterSettings
        Dim rrekap As New r_gajibulanan3() With {.DataSource = ds.Tables(0)}
        rrekap.xperiode.Text = periode
        rrekap.xuser.Text = String.Format("{0} {1}", userprog, DateTime.Now)
        rrekap.xjmlhari.Text = jmlhari
        rrekap.DataMember = rrekap.DataMember
        rrekap.PrinterName = ops.PrinterName
        rrekap.CreateDocument(True)

        PrintControl1.PrintingSystem = rrekap.PrintingSystem

    End Sub

    Private Sub load_print4(ByVal cn As OleDbConnection)

        Dim jmlhari As Integer = 0
        Dim cmdhari As OleDbCommand = New OleDbCommand(sqlhari, cn)
        Dim drdhari As OleDbDataReader = cmdhari.ExecuteReader
        If drdhari.Read Then
            If IsNumeric(drdhari(0).ToString) Then
                jmlhari = Integer.Parse(drdhari(0).ToString)
            End If
        End If
        drdhari.Close()

        Dim ds As DataSet = New ds_gajibulanan3
        ds = Clsmy.GetDataSet(sql, cn)

        If statall = False Then
            Dim sqlvi As String = String.Format("select nip from ms_usersys4 where namauser='{0}'", userprog)
            Dim cmd As OleDbCommand = New OleDbCommand(sqlvi, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            While drd.Read
                Dim rows() As DataRow = ds.Tables(0).Select(String.Format("nip='{0}'", drd("nip").ToString))

                If rows.Length > 0 Then
                    If rows(0)("nip").ToString.Equals(drd("nip").ToString) Then
                        rows(0)("statin") = "off"
                    End If
                End If

            End While
            drd.Close()
        End If

        Dim ops As New System.Drawing.Printing.PrinterSettings
        Dim rrekap As New r_gajibulanan4() With {.DataSource = ds.Tables(0)}
        rrekap.xperiode.Text = periode
        rrekap.xuser.Text = String.Format("{0} {1}", userprog, DateTime.Now)
        rrekap.xjmlhari.Text = jmlhari
        rrekap.DataMember = rrekap.DataMember
        rrekap.PrinterName = ops.PrinterName
        rrekap.CreateDocument(True)

        PrintControl1.PrintingSystem = rrekap.PrintingSystem

    End Sub

    Private Sub load_print()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            If jnis_hit = 1 Or jnis_hit = 2 Then
                load_print1(cn)
            ElseIf jnis_hit = 3 Then
                load_print3(cn)
            ElseIf jnis_hit = 4 Then
                load_print4(cn)
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


    Private Sub btkalk_Click(sender As System.Object, e As System.EventArgs) Handles btkalk.Click

        If tbln.EditValue = 0 Then
            MsgBox("Bulan harus diisi...", vbOKOnly + vbInformation, "Informasi")
            tbln.Focus()
            Return
        End If

        If ttahun.EditValue = 0 Then
            MsgBox("Tahun harus diisi...", vbOKOnly + vbInformation, "Informasi")
            ttahun.Focus()
            Return
        End If

        If cek_periode_ongaji() = False Then
            MsgBox("Tanggal tidak sesuai dengan yang sudah dihitung sebelumnya,cek kembali...", vbOKOnly + vbInformation, "Informasi")
            ttgl1.Focus()
            Return
        End If

        If cek_tahunbulanminggu() = False Then
            MsgBox("Minggu,Bulan atau tahun tidak sesuai dengan data yang sudah dihitung sebelumnya,cek kembali...", vbOKOnly + vbInformation, "Informasi")
            tbln.Focus()
            Return
        End If

        simpanto_trgaji()

    End Sub

    Private Sub btload_Click(sender As System.Object, e As System.EventArgs) Handles btload.Click
        load_data()
    End Sub

    Private Sub fgaji_mingguan1_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        cbgol.Focus()
    End Sub

    Private Sub fgaji_mingguan1_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        loadGolongan()
        cek_jnis_hit()

        ttgl1.EditValue = Date.Now
        ttgl2.EditValue = Date.Now

    End Sub

    Private Sub ttgl1_Validated(sender As Object, e As System.EventArgs) Handles ttgl1.Validated
        If IsDate(ttgl1.EditValue) Then
            tbln.EditValue = Month(ttgl1.EditValue)
            ttahun.EditValue = Year(ttgl1.EditValue)
        End If
    End Sub

    Private Sub btbayar_Click(sender As Object, e As EventArgs) Handles btbayar.Click
        Using fkar2 As New fpot_cuti2 With {.StartPosition = FormStartPosition.CenterParent, .dv = Nothing, .addstat = True, .position = 0, .via_bagigaji = True}
            fkar2.ShowDialog()
        End Using
    End Sub

    Private Sub cbgol_EditValueChanged(sender As Object, e As EventArgs) Handles cbgol.EditValueChanged

    End Sub
End Class