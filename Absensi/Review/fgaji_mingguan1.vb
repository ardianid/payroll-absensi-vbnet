Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Imports DevExpress.XtraReports.UI

Public Class fgaji_mingguan1

    Dim jnis_hit As Integer = 0

    Private Function cek_periode_ongaji() As Boolean

        Dim hasil As Boolean = True

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select distinct tanggal1,tanggal2 from tr_gaji where calc_by='Mingguan' and tanggal1>='{0}' and tanggal2<='{1}'", convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue))
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

            Dim sql As String = String.Format("select distinct tahun,bulan,minggu from tr_gaji where calc_by='Mingguan' and tanggal1>='{0}' and tanggal2<='{1}'", convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue))
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            Dim jtahun As Integer = 0
            Dim jbln As Integer = 0
            Dim jmgu As Integer = 0

            While drd.Read

                If Integer.Parse(ttahun.EditValue) = Integer.Parse(drd("tahun").ToString) Then
                    jtahun = jtahun + 1
                End If

                If Integer.Parse(tbln.EditValue) = Integer.Parse(drd("bulan").ToString) Then
                    jbln = jbln + 1
                End If

                If Integer.Parse(tmgu.EditValue) = Integer.Parse(drd("minggu").ToString) Then
                    jmgu = jmgu + 1
                End If

            End While
            drd.Close()

            If jtahun > 1 Or jbln > 1 Or jmgu > 1 Then
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

            Dim sql As String = "select kode,nama from ms_golongan where jenisgaji='Mingguan' and tampilgroup=1 and saktif=1"

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

        Dim sql As String = String.Format("delete from tr_gaji where calc_by='Mingguan' and tanggal1='{0}' and tanggal2='{1}' and kdgol='{2}'", convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue), cbgol.EditValue)
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

            Dim jenishitung_lembur As Integer = 0

            Dim sqlawal As String = "select * from ms_awalshift"
            Dim cmdawal As OleDbCommand = New OleDbCommand(sqlawal, cn)
            Dim drdawal As OleDbDataReader = cmdawal.ExecuteReader
            If drdawal.Read Then
                jenishitung_lembur = Integer.Parse(drdawal("jnis_hit_harian").ToString)
            End If
            drdawal.Close()

            Dim sql As String = String.Format("SELECT ms_karyawan.nip,ms_karyawan.nama,ms_karyawan.depart,SUM(tr_hadir.jam1) + SUM(tr_hadir.jam2) + SUM(tr_hadir.jam3) + SUM(tr_hadir.jam4) as jmllembur, " & _
            "sum(tr_hadir.lemburperjam) as totlembur,sum(tr_hadir.tothasil) as tothasil, sum(tr_hadir.jharian) as tot_harian,SUM(tr_hadir.uangmakan) as uangmakan, SUM(tr_hadir.tambmakan) as tambmakanlembur,SUM(jmlhasil) as jmlhasil,SUM(tamb_istirahat) as tamb_istirahat, " & _
            "ms_karyawan.tunj_jabatan, ms_karyawan.tunj_kehadiran, ms_karyawan.tunj_akomodasi, ms_karyawan.tunj_makan,ms_karyawan.sewa_kend " & _
            "FROM         tr_hadir INNER JOIN ms_karyawan ON tr_hadir.nip = ms_karyawan.nip " & _
            "WHERE ms_karyawan.aktif=1 and tr_hadir.kdgol='{0}' and tr_hadir.tanggal>='{1}' and tr_hadir.tanggal<='{2}' " & _
            "GROUP BY ms_karyawan.nip,ms_karyawan.nama,ms_karyawan.depart,ms_karyawan.tunj_jabatan, " & _
            "ms_karyawan.tunj_kehadiran,ms_karyawan.tunj_akomodasi,ms_karyawan.tunj_makan,ms_karyawan.sewa_kend", cbgol.EditValue, convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue))

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
                Dim totuangmakan As Double = Double.Parse(dtsel(i)("uangmakan").ToString)
                Dim sewakend As Double = Double.Parse(dtsel(i)("sewa_kend").ToString)
                Dim tambisti As Double = Double.Parse(dtsel(i)("tamb_istirahat").ToString)

                Dim jmlhari As Integer = 0
                ' Dim sqlhariaktif As String = String.Format("select COUNT(distinct tr_hadir.tanggal) as jmlmasuk from tr_hadir inner join ms_kalender on tr_hadir.tanggal=ms_kalender.tanggal where ms_kalender.libur=0 and tr_hadir.stat in ('HADIR','MENGINAP') and tr_hadir.step=2 and tr_hadir.tanggal>='{0}' and tr_hadir.tanggal <='{1}' and tr_hadir.nip='{2}'", convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue), nnip)

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
                    Dim sqlhariaktif_mk As String = String.Format("select COUNT(distinct tr_hadir.tanggal) as jmlmasuk from tr_hadir inner join ms_kalender on tr_hadir.tanggal=ms_kalender.tanggal where ms_kalender.libur=0 and tr_hadir.uangmakan=0 and tr_hadir.stat in ('HADIR','MENGINAP') and tr_hadir.step=2 and tr_hadir.tanggal>='{0}' and tr_hadir.tanggal <='{1}' and tr_hadir.nip='{2}'", convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue), nnip)
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

                If jmlhari >= hariaktif_kalender1 Then

                    If jenishitung_lembur = 3 Then
                        If jmlhari < 6 Then
                            tunjkehadiran = 0
                        End If
                    End If

                Else
                    tunjkehadiran = 0
                End If

                Dim sqlin As String = String.Format("insert into tr_gaji(nip,calc_by,tahun,bulan,minggu,tunj_jab,tunj_hadir,jmlhadir,tunj_akomod,tunj_makan,tunj_makanlembur,jmllembur,gaji_lembur,jml_hasil,tot_hasil,tot_harian,tanggal1,tanggal2,tunj_sewakend,kdgol,depart,tamb_istirahat) " & _
                "values('{0}','Mingguan',{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},'{15}','{16}',{17},'{18}','{19}',{20})", _
                nnip, ttahun.EditValue, tbln.EditValue, tmgu.EditValue, Replace(tunjjabatan, ",", "."), Replace(tunjkehadiran, ",", "."), Replace(jmlhari, ",", "."), Replace(tunjakomodasi, ",", "."), Replace(tunjmakan, ",", "."), Replace(tambmakanlembur, ",", "."), _
                 Replace(jmllembur, ",", "."), Replace(totlembur, ",", "."), Replace(jmlhasil, ",", "."), Replace(tothasil, ",", "."), Replace(totharian, ",", "."), convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue), Replace(sewakend, ",", "."), cbgol.EditValue, depart, Replace(tambisti, ",", "."))

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
            ' If MsgBox("Anda ingin langsung melihat hasilnya ?", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.Yes Then
            load_data()
            'End If
        End If

    End Sub

    Private Sub load_data()

        Dim sql As String = String.Format("select ms_karyawan.depart,tr_gaji.nip,ms_karyawan.nama,tr_gaji.jmllembur,tr_gaji.gaji_lembur as totlembur, " & _
        "tr_gaji.tot_harian + tr_gaji.tot_hasil as totperhari, tr_gaji.tunj_jab as tunj_jabatan, " & _
        "tr_gaji.tunj_hadir as tunj_kehadiran,tr_gaji.tunj_akomod as tunj_akomodasi, " & _
        "tr_gaji.tunj_makan + tr_gaji.tunj_makanlembur as tunj_makan,tr_gaji.jmlhadir,tr_gaji.tunj_sewakend,tr_gaji.tamb_istirahat " & _
        "from tr_gaji inner join ms_karyawan on tr_gaji.nip=ms_karyawan.nip " & _
        "where tr_gaji.calc_by='Mingguan' and tr_gaji.tanggal1='{0}' and tr_gaji.tanggal2='{1}' and ms_karyawan.kdgol='{2}'", convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue), cbgol.EditValue)

        Dim periode As String = String.Format("Periode : {0} s.d {1}", convert_date_to_ind(ttgl1.EditValue), convert_date_to_ind(ttgl2.EditValue))

        Cursor = Cursors.WaitCursor

        load_print(sql, periode)

        'Dim fs As New fgaji_mingguan11 With {.WindowState = FormWindowState.Maximized, .sql = sql, .periode = periode}
        'fs.ShowDialog()

        Cursor = Cursors.Default

    End Sub

    Private Sub load_print(ByVal sql As String, ByVal periode As String)

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            Dim ds As DataSet = New ds_gajimingguan1
            ds = Clsmy.GetDataSet(Sql, cn)

            Dim ops As New System.Drawing.Printing.PrinterSettings
            Dim rrekap As New r_gajimingguan() With {.DataSource = ds.Tables(0)}
            rrekap.xperiode.Text = periode
            rrekap.xuser.Text = String.Format("{0} {1}", userprog, DateTime.Now)
            rrekap.DataMember = rrekap.DataMember
            rrekap.PrinterName = ops.PrinterName
            rrekap.CreateDocument(True)

            PrintControl1.PrintingSystem = rrekap.PrintingSystem

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

    Private Function cekminggu() As Integer

        Dim mingguke As Integer = 0
        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select minggu,tanggal from ms_kalender where MONTH(tanggal)={0} and YEAR(tanggal)={1} order by minggu", Month(ttgl1.EditValue), Year(ttgl1.EditValue))
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            Dim minggusebelum As Integer = 0

            While drd.Read
                If mingguke = 0 Then
                    mingguke = 1
                    minggusebelum = Integer.Parse(drd(0).ToString)
                Else

                    If minggusebelum <> Integer.Parse(drd(0).ToString) Then
                        minggusebelum = Integer.Parse(drd(0).ToString)
                        mingguke = mingguke + 1
                    End If

                    If DateValue(drd(1).ToString) = DateValue(ttgl1.EditValue) Then
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

        Return mingguke

    End Function

    Private Sub btkalk_Click(sender As System.Object, e As System.EventArgs) Handles btkalk.Click

        If tmgu.EditValue = 0 Then
            MsgBox("Minggu harus diisi...", vbOKOnly + vbInformation, "Informasi")
            tmgu.Focus()
            Return
        End If

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
            tmgu.Focus()
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

        cek_jnis_hit()
        loadGolongan()

        ttgl1.EditValue = Date.Now
        ttgl2.EditValue = Date.Now

    End Sub

    Private Sub ttgl1_Validated(sender As Object, e As System.EventArgs) Handles ttgl1.Validated
        If IsDate(ttgl1.EditValue) Then
            tmgu.EditValue = cekminggu()
            tbln.EditValue = Month(ttgl1.EditValue)
            ttahun.EditValue = Year(ttgl1.EditValue)
        End If
    End Sub

End Class