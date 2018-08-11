Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Public Class fjamkerja_oby_gol2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private bs1 As BindingSource
    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private Sub kosongkan()
        tkode.Text = ""
        tnama.Text = ""
        tjam_masuk.EditValue = "00:00"
        tjam_plg.EditValue = "00:00"
        ttol_terlambat.EditValue = "00:00"
        ttol_pulang.EditValue = "00:00"
        tawal_masuk.EditValue = "00:00"
        takhir_masuk.EditValue = "00:00"
        tawal_pulang.EditValue = "00:00"
        takhir_pulang.EditValue = "00:00"
        ttgl.EditValue = Date.Now
        ttgl1.EditValue = Date.Now
        ttgl2.EditValue = Date.Now

    End Sub

    Private Sub isi()
        tkode.Text = dv(position)("kode").ToString
        tnama.Text = dv(position)("ket").ToString
        tjam_masuk.EditValue = TimeValue(dv(position)("jammasuk").ToString)
        tjam_plg.EditValue = TimeValue(dv(position)("jampulang").ToString)
        ttol_terlambat.EditValue = TimeValue(dv(position)("tolmasuk").ToString)
        ttol_pulang.EditValue = TimeValue(dv(position)("tolpulang").ToString)
        tawal_masuk.EditValue = TimeValue(dv(position)("awalmasuk").ToString)
        takhir_masuk.EditValue = TimeValue(dv(position)("akhirmasuk").ToString)
        tawal_pulang.EditValue = TimeValue(dv(position)("awalpulang").ToString)
        takhir_pulang.EditValue = TimeValue(dv(position)("akhirpulang").ToString)
        ttgl.EditValue = DateValue(dv(position)("tanggal").ToString)
        ttgl1.EditValue = DateValue(dv(position)("tanggal1").ToString)
        ttgl2.EditValue = DateValue(dv(position)("tanggal2").ToString)
        cbhitung.EditValue = dv(position)("hitung_lembur").ToString
    End Sub

    Private Sub opendata()

        Dim sql As String = String.Format("select a.kode,a.nip,b.nama,c.nama as golongan from	ms_jamkerjalain2 a inner join ms_karyawan b on a.nip=b.nip " & _
             "inner join ms_golongan c on  b.kdgol=c.kode where a.kode='{0}'", tkode.Text.Trim)
        Dim cn As OleDbConnection = Nothing

        grid1.DataSource = Nothing

        Try

            open_wait()

            Dim ds As DataSet

            dv1 = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager = New DataViewManager(ds)
            dv1 = dvmanager.CreateDataView(ds.Tables(0))


            grid1.DataSource = dv1

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

    Private Function isi_nobukti(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As String

        Dim sql As String = String.Format("select MAX(kode) from ms_jamkerjalain where MONTH(tanggal)='{0}' and YEAR(tanggal)='{1}'", Month(Date.Now), Year(Date.Now))
        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        Dim noawal As String = 0

        If drd.Read Then
            If Not drd(0).ToString.Equals("") Then
                noawal = drd(0).ToString.Substring(8, 4)
            End If
        End If
        drd.Close()

        Dim noawal1 As Integer = 0
        If IsNumeric(noawal) Then
            noawal1 = Integer.Parse(noawal)
        End If

        noawal1 = noawal1 + 1
        If noawal1.ToString.Length = 1 Then
            noawal = "000" & noawal1
        ElseIf noawal1.ToString.Length = 2 Then
            noawal = "00" & noawal1
        ElseIf noawal1.ToString.Length = 3 Then
            noawal = "0" & noawal1
        Else
            noawal = noawal1
        End If

        Dim bulan As String
        bulan = Month(Date.Now)

        If bulan.Length = 1 Then
            bulan = "0" & bulan
        End If

        noawal = "FJK." & bulan & Year(Date.Now).ToString.Substring(2, 2) & noawal

        Return noawal

    End Function

    Private Sub simpan()

        Dim cn As OleDbConnection = Nothing
        open_wait()

        Try

            Dim jammasuk As String = TimeValue(tjam_masuk.EditValue)

            If jammasuk.Substring(jammasuk.Length - 2, 2) < 59 Then
                jammasuk = TimeValue(jammasuk).AddSeconds(59)
            End If

            Dim jampulang As String = TimeValue(tjam_plg.EditValue)

            If jampulang.Substring(jampulang.Length - 2, 2) < 59 Then
                jampulang = TimeValue(jampulang).AddSeconds(59)
            End If

            Dim tolterlambat As String = TimeValue(ttol_terlambat.EditValue)

            If tolterlambat.Substring(tolterlambat.Length - 2, 2) < 59 Then
                tolterlambat = TimeValue(tolterlambat).AddSeconds(59)
            End If

            Dim tolpulang As String = TimeValue(ttol_pulang.EditValue)

            If tolpulang.Substring(tolpulang.Length - 2, 2) < 59 Then
                tolpulang = TimeValue(tolpulang).AddSeconds(59)
            End If

            Dim awalmasuk As String = TimeValue(tawal_masuk.EditValue)

            If awalmasuk.Substring(awalmasuk.Length - 2, 2) < 59 Then
                awalmasuk = TimeValue(awalmasuk).AddSeconds(59)
            End If

            Dim akhirmasuk As String = TimeValue(takhir_masuk.EditValue)

            If akhirmasuk.Substring(akhirmasuk.Length - 2, 2) < 59 Then
                akhirmasuk = TimeValue(akhirmasuk).AddSeconds(59)
            End If

            Dim awalpulang As String = TimeValue(tawal_pulang.EditValue)

            If awalpulang.Substring(awalpulang.Length - 2, 2) < 59 Then
                awalpulang = TimeValue(awalpulang).AddSeconds(59)
            End If

            Dim akhirpulang As String = TimeValue(takhir_pulang.EditValue)

            If akhirpulang.Substring(akhirpulang.Length - 2, 2) < 59 Then
                akhirpulang = TimeValue(akhirpulang).AddSeconds(59)
            End If

            Dim jammasuk2 As Date = Convert.ToDateTime(jammasuk)
            Dim jampulang2 As Date = Convert.ToDateTime(jampulang)

            Dim slisihkerja As TimeSpan = jampulang2.Subtract(jammasuk2)
            Dim stattengah As Integer

            Dim kjmlpulang As Integer = slisihkerja.TotalMinutes

            If kjmlpulang < 0 Then
                stattengah = 1
            Else
                stattengah = 0
            End If

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction

            tkode.Text = isi_nobukti(cn, sqltrans)

            Dim sqlinsert As String = String.Format("insert into ms_jamkerjalain (kode,ket,jammasuk,jampulang,tolmasuk,tolpulang,awalmasuk,akhirmasuk,awalpulang,akhirpulang,tanggal,tanggal1,tanggal2,hitung_lembur,tengahmalam) values(" & _
                                        "'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}',{14})", tkode.Text.Trim, tnama.Text.Trim, _
                                            jammasuk, jampulang, tolterlambat, tolpulang, awalmasuk, akhirmasuk, awalpulang, akhirpulang, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue), cbhitung.EditValue, stattengah)
            Dim sqlupdate As String = String.Format("update ms_jamkerjalain set ket='{0}',jammasuk='{1}',jampulang='{2}',tolmasuk='{3}',tolpulang='{4}',awalmasuk='{5}',akhirmasuk='{6}',awalpulang='{7}',akhirpulang='{8}',tanggal='{9}',tanggal1='{10}',tanggal2='{11}',hitung_lembur='{12}',tengahmalam={13} where kode='{14}'", tnama.Text.Trim, _
                                        jammasuk, jampulang, tolterlambat, tolpulang, awalmasuk, akhirmasuk, awalpulang, akhirpulang, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue), cbhitung.EditValue, stattengah, tkode.Text.Trim)

            Dim comd As OleDbCommand

            If addstat = True Then



                comd = New OleDbCommand(sqlinsert, cn, sqltrans)
                comd.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btjamkerjalain", 1, 0, 0, 0, tkode.Text.Trim, tnama.Text.Trim, sqltrans)

                insertview()

            Else

                comd = New OleDbCommand(sqlupdate, cn, sqltrans)
                comd.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btjamkerjalain", 0, 1, 0, 0, tkode.Text.Trim, tnama.Text.Trim, sqltrans)

                updateview()

            End If

            simpan2(cn, sqltrans)

            sqltrans.Commit()

            MsgBox("Data disimpan...")

            If addstat = True Then
                kosongkan()
                tkode.Focus()
            Else
                Me.Close()
            End If

            close_wait()

            Me.Close()

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

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        For i As Integer = 0 To dv1.Count - 1

            Dim sql As String = String.Format("insert into ms_jamkerjalain2 (kode,nip) values('{0}','{1}')", tkode.Text.Trim, dv1(i)("nip").ToString)
            Dim sqlse As String = String.Format("select nip from ms_jamkerjalain2 where kode='{0}' and nip='{1}'", tkode.Text.Trim, dv1(i)("nip").ToString)

            Dim comd2 As OleDbCommand = New OleDbCommand(sqlse, cn, sqltrans)
            Dim dre As OleDbDataReader = comd2.ExecuteReader

            If dre.HasRows Then
                If dre.Read Then
                    If dre(0).ToString.Length = 0 Then
                        Dim comd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                        comd.ExecuteReader()
                    End If

                Else
                    Dim comd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                    comd.ExecuteReader()
                End If
            Else
                Dim comd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                comd.ExecuteReader()
            End If


            

        Next

    End Sub

    Private Sub hapus()

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        Dim sql As String = String.Format("delete from ms_jamkerjalain2 where kode='{0}' and nip='{1}'", tkode.Text.Trim, dv1(Me.BindingContext(dv1).Position)("nip").ToString)

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction

            Dim comd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
            comd.ExecuteNonQuery()

            dv1.Delete(Me.BindingContext(dv1).Position)

            sqltrans.Commit()

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

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew
        orow("kode") = tkode.Text.Trim
        orow("ket") = tnama.Text.Trim
        orow("jammasuk") = tjam_masuk.EditValue
        orow("jampulang") = tjam_plg.EditValue
        orow("tolmasuk") = ttol_terlambat.EditValue
        orow("tolpulang") = ttol_pulang.EditValue
        orow("awalmasuk") = tawal_masuk.EditValue
        orow("akhirmasuk") = takhir_masuk.EditValue
        orow("awalpulang") = tawal_pulang.EditValue
        orow("akhirpulang") = takhir_pulang.EditValue
        orow("tanggal") = ttgl.EditValue
        orow("tanggal1") = ttgl1.EditValue
        orow("tanggal2") = ttgl2.EditValue
        orow("hitung_lembur") = cbhitung.EditValue

        dv.EndInit()

    End Sub

    Private Sub updateview()

        dv(position)("ket") = tnama.Text.Trim
        dv(position)("jammasuk") = tjam_masuk.EditValue
        dv(position)("jampulang") = tjam_plg.EditValue
        dv(position)("tolmasuk") = ttol_terlambat.EditValue
        dv(position)("tolpulang") = ttol_pulang.EditValue
        dv(position)("awalmasuk") = tawal_masuk.EditValue
        dv(position)("akhirmasuk") = takhir_masuk.EditValue
        dv(position)("awalpulang") = tawal_pulang.EditValue
        dv(position)("akhirpulang") = takhir_pulang.EditValue
        dv(position)("tanggal") = ttgl.EditValue
        dv(position)("tanggal1") = ttgl1.EditValue
        dv(position)("tanggal2") = ttgl2.EditValue
        dv(position)("hitung_lembur") = cbhitung.EditValue

    End Sub

    Private Sub btselesai_Click(sender As System.Object, e As System.EventArgs) Handles btselesai.Click
        Me.Close()
    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click

        'If tkode.Text.Trim.Length = 0 Then
        '    MsgBox("Kode tidak boleh kosong...", vbOKOnly + vbInformation, "Informasi")
        '    tkode.Focus()
        '    Return
        'End If

        If tnama.Text.Trim.Length = 0 Then
            MsgBox("Ket tidak boleh kosong...", vbOKOnly + vbInformation, "Informasi")
            tnama.Focus()
            Return
        End If

        If cbhitung.EditValue = "" Then
            MsgBox("Jenis Perhitungan lembur tidak boleh kosong...", vbOKOnly + vbInformation, "Informasi")
            cbhitung.Focus()
            Return
        End If

        If tjam_masuk.EditValue = "00:00" Then
            MsgBox("Jam masuk tidak boleh kosong...", vbOKOnly + vbInformation, "Informasi")
            tjam_masuk.Focus()
            Return
        End If

        If tjam_plg.EditValue = "00:00" Then
            MsgBox("Jam pulang tidak boleh kosong...", vbOKOnly + vbInformation, "Informasi")
            tjam_plg.Focus()
            Return
        End If

        If ttol_terlambat.EditValue = "00:00" Then
            MsgBox("Toleransi keterlambatan tidak boleh kosong...", vbOKOnly + vbInformation, "Informasi")
            ttol_terlambat.Focus()
            Return
        End If

        If ttol_pulang.EditValue = "00:00" Then
            MsgBox("Toleransi pulang cepat tidak boleh kosong...", vbOKOnly + vbInformation, "Informasi")
            ttol_pulang.Focus()
            Return
        End If

        If takhir_masuk.EditValue = "00:00" Then
            MsgBox("Akhir masuk tidak boleh kosong...", vbOKOnly + vbInformation, "Informasi")
            takhir_masuk.Focus()
            Return
        End If

        If tawal_pulang.EditValue = "00:00" Then
            MsgBox("Awal pulang tidak boleh kosong...", vbOKOnly + vbInformation, "Informasi")
            tawal_pulang.Focus()
            Return
        End If

        simpan()



    End Sub

    Private Sub fjamkerja2_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        If addstat = True Then
            tnama.Focus()
        Else
            ttgl1.Focus()
        End If
    End Sub

    Private Sub fjamkerja2_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If addstat = True Then
            kosongkan()
            'tkode.Enabled = True
        Else
            isi()
            'tkode.Enabled = False
            ttgl.Enabled = False

        End If

        opendata()

    End Sub

    Private Sub btdel_Click(sender As System.Object, e As System.EventArgs) Handles btdel.Click
        hapus()
    End Sub

    Private Sub btadd_Click(sender As System.Object, e As System.EventArgs) Handles btadd.Click
        Using fjamker2 As New fjamkerja_oby_gol3 With {.StartPosition = FormStartPosition.CenterParent, .dv3 = dv1, .jenis = "jamkerja"}
            fjamker2.ShowDialog()
        End Using
    End Sub


    Private Sub tjam_masuk_Validated(sender As System.Object, e As System.EventArgs) Handles tjam_masuk.Validated

        If addstat Then
            ttol_terlambat.EditValue = TimeValue(DateAdd("n", 15, tjam_masuk.EditValue))
            tawal_masuk.EditValue = TimeValue(DateAdd("n", -40, tjam_masuk.EditValue))
            takhir_masuk.EditValue = TimeValue(DateAdd("n", 15, tjam_masuk.EditValue))
        End If

    End Sub

    Private Sub tjam_plg_Validated(sender As System.Object, e As System.EventArgs) Handles tjam_plg.Validated
        If addstat Then
            ttol_pulang.EditValue = TimeValue(DateAdd("n", -2, tjam_plg.EditValue))
            tawal_pulang.EditValue = TimeValue(DateAdd("n", -30, tjam_plg.EditValue))
            takhir_pulang.EditValue = TimeValue(DateAdd("h", 5, tjam_plg.EditValue))
        End If
    End Sub

End Class