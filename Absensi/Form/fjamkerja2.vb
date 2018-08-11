Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Public Class fjamkerja2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

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

        tistirahat1.EditValue = "00:00"
        tistirahat2.EditValue = "00:00"

        tjam_istirahat.EditValue = 0
        tjamkerja.EditValue = 0

        'tjam_plg_sb.EditValue = "00:00"
        'ttol_pulang_sb.EditValue = "00:00"
        'tawal_pulang_sb.EditValue = "00:00"
        'takhir_pulang_sb.EditValue = "00:00"

    End Sub

    Private Sub isi()
        tkode.Text = dv(position)("kode").ToString
        tnama.Text = dv(position)("nama").ToString
        tjam_masuk.EditValue = TimeValue(dv(position)("jammasuk").ToString)
        tjam_plg.EditValue = TimeValue(dv(position)("jampulang").ToString)
        ttol_terlambat.EditValue = TimeValue(dv(position)("tolmasuk").ToString)
        ttol_pulang.EditValue = TimeValue(dv(position)("tolpulang").ToString)
        tawal_masuk.EditValue = TimeValue(dv(position)("awalmasuk").ToString)
        takhir_masuk.EditValue = TimeValue(dv(position)("akhirmasuk").ToString)
        tawal_pulang.EditValue = TimeValue(dv(position)("awalkeluar").ToString)
        takhir_pulang.EditValue = TimeValue(dv(position)("akhirkeluar").ToString)

        'tistirahat1.EditValue = TimeValue(dv(position)("jamistirahat1").ToString)
        'tistirahat2.EditValue = TimeValue(dv(position)("jamistirahat2").ToString)

        'tjam_istirahat.EditValue = dv(position)("jam_istirahat").ToString
        'tjamkerja.EditValue = dv(position)("jam_kerja").ToString

        'If dv(position)("jampulang_sb").ToString.Equals("") Then
        '    tjam_plg_sb.EditValue = "00:00"
        'Else
        '    tjam_plg_sb.EditValue = TimeValue(dv(position)("jampulang_sb").ToString)
        'End If

        'If dv(position)("tolpulang_sb").ToString.Equals("") Then
        '    ttol_pulang_sb.EditValue = "00:00"
        'Else
        '    ttol_pulang_sb.EditValue = TimeValue(dv(position)("tolpulang_sb").ToString)
        'End If

        'If dv(position)("awalkeluar_sb").ToString.Equals("") Then
        '    tawal_pulang_sb.EditValue = "00:00"
        'Else
        '    tawal_pulang_sb.EditValue = TimeValue(dv(position)("awalkeluar_sb").ToString)
        'End If

        'If dv(position)("akhirkeluar").ToString.Equals("") Then
        '    takhir_pulang_sb.EditValue = "00:00"
        'Else
        '    takhir_pulang_sb.EditValue = TimeValue(dv(position)("akhirkeluar_sb").ToString)
        'End If

    End Sub

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

            'Dim jampulang_sb As String = TimeValue(tjam_plg_sb.EditValue)

            'If jampulang_sb.Substring(jampulang_sb.Length - 2, 2) < 59 Then
            '    jampulang_sb = TimeValue(jampulang_sb).AddSeconds(59)
            'End If

            Dim tolterlambat As String = TimeValue(ttol_terlambat.EditValue)

            If tolterlambat.Substring(tolterlambat.Length - 2, 2) < 59 Then
                tolterlambat = TimeValue(tolterlambat).AddSeconds(59)
            End If

            Dim tolpulang As String = TimeValue(ttol_pulang.EditValue)

            If tolpulang.Substring(tolpulang.Length - 2, 2) < 59 Then
                tolpulang = TimeValue(tolpulang).AddSeconds(59)
            End If

            'Dim tolpulang_sb As String = TimeValue(ttol_pulang_sb.EditValue)

            'If tolpulang_sb.Substring(tolpulang_sb.Length - 2, 2) < 59 Then
            '    tolpulang_sb = TimeValue(tolpulang_sb).AddSeconds(59)
            'End If

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

            'Dim awalpulang_sb As String = TimeValue(tawal_pulang_sb.EditValue)

            'If awalpulang_sb.Substring(awalpulang_sb.Length - 2, 2) < 59 Then
            '    awalpulang_sb = TimeValue(awalpulang_sb).AddSeconds(59)
            'End If

            Dim akhirpulang As String = TimeValue(takhir_pulang.EditValue)

            If akhirpulang.Substring(akhirpulang.Length - 2, 2) < 59 Then
                akhirpulang = TimeValue(akhirpulang).AddSeconds(59)
            End If

            'Dim akhirpulang_sb As String = TimeValue(takhir_pulang_sb.EditValue)

            'If akhirpulang_sb.Substring(akhirpulang_sb.Length - 2, 2) < 59 Then
            '    akhirpulang_sb = TimeValue(akhirpulang_sb).AddSeconds(59)
            'End If

            Dim jamisti1 As String = TimeValue(tistirahat1.EditValue)
            Dim jamisti2 As String = TimeValue(tistirahat2.EditValue)
           
            Dim stattengah As Integer
            stattengah = cek_lewat_tngahmalem(TimeValue(jammasuk), TimeValue(jampulang))

            Dim sqlinsert As String = String.Format("insert into ms_jamkerja (kode,nama,jammasuk,jampulang,tolmasuk,tolpulang,awalmasuk,akhirmasuk,awalkeluar,akhirkeluar,tengahmalam) values(" & _
                                        "'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10})", tkode.Text.Trim, tnama.Text.Trim, _
                                            jammasuk, jampulang, tolterlambat, tolpulang, awalmasuk, akhirmasuk, awalpulang, akhirpulang, stattengah)
            Dim sqlupdate As String = String.Format("update ms_jamkerja set nama='{0}',jammasuk='{1}',jampulang='{2}',tolmasuk='{3}',tolpulang='{4}',awalmasuk='{5}',akhirmasuk='{6}',awalkeluar='{7}',akhirkeluar='{8}',tengahmalam={9} where kode='{10}'", tnama.Text.Trim, _
                                        jammasuk, jampulang, tolterlambat, tolpulang, awalmasuk, akhirmasuk, awalpulang, akhirpulang, stattengah, tkode.Text.Trim)

            Dim comd As OleDbCommand

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction

            If addstat = True Then

                comd = New OleDbCommand(sqlinsert, cn, sqltrans)
                comd.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btjamkerja", 1, 0, 0, 0, tkode.Text.Trim, tnama.Text.Trim, sqltrans)

                insertview()

            Else

                comd = New OleDbCommand(sqlupdate, cn, sqltrans)
                comd.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btjamkerja", 0, 1, 0, 0, tkode.Text.Trim, tnama.Text.Trim, sqltrans)

                updateview()

            End If

            sqltrans.Commit()

            MsgBox("Data disimpan...")

            If addstat = True Then
                kosongkan()
                tkode.Focus()
            Else
                Me.Close()
            End If

            close_wait()

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

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew
        orow("kode") = tkode.Text.Trim
        orow("nama") = tnama.Text.Trim
        orow("jammasuk") = tjam_masuk.EditValue
        orow("jampulang") = tjam_plg.EditValue
        orow("tolmasuk") = ttol_terlambat.EditValue
        orow("tolpulang") = ttol_pulang.EditValue
        orow("awalmasuk") = tawal_masuk.EditValue
        orow("akhirmasuk") = takhir_masuk.EditValue
        orow("awalkeluar") = tawal_pulang.EditValue
        orow("akhirkeluar") = takhir_pulang.EditValue
        'orow("jamistirahat1") = tistirahat1.EditValue
        'orow("jamistirahat2") = tistirahat2.EditValue

        'orow("jam_istirahat") = tjam_istirahat.EditValue
        'orow("jam_kerja") = tjamkerja.EditValue

        dv.EndInit()

    End Sub

    Private Sub updateview()

        dv(position)("nama") = tnama.Text.Trim
        dv(position)("jammasuk") = tjam_masuk.EditValue
        dv(position)("jampulang") = tjam_plg.EditValue
        dv(position)("tolmasuk") = ttol_terlambat.EditValue
        dv(position)("tolpulang") = ttol_pulang.EditValue
        dv(position)("awalmasuk") = tawal_masuk.EditValue
        dv(position)("akhirmasuk") = takhir_masuk.EditValue
        dv(position)("awalkeluar") = tawal_pulang.EditValue
        dv(position)("akhirkeluar") = takhir_pulang.EditValue

        'dv(position)("jamistirahat1") = tistirahat1.EditValue
        'dv(position)("jamistirahat2") = tistirahat2.EditValue

        'dv(position)("jam_istirahat") = tjam_istirahat.EditValue
        'dv(position)("jam_kerja") = tjamkerja.EditValue

    End Sub

    Private Sub btselesai_Click(sender As System.Object, e As System.EventArgs) Handles btselesai.Click
        Me.Close()
    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click

        If tkode.Text.Trim.Length = 0 Then
            MsgBox("Kode tidak boleh kosong...", vbOKOnly + vbInformation, "Informasi")
            tkode.Focus()
            Return
        End If

        If tnama.Text.Trim.Length = 0 Then
            MsgBox("Nama tidak boleh kosong...", vbOKOnly + vbInformation, "Informasi")
            tnama.Focus()
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
            tkode.Focus()
        Else
            tnama.Focus()
        End If
    End Sub

    Private Sub fjamkerja2_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If addstat = True Then
            kosongkan()
            tkode.Enabled = True
        Else
            isi()
            tkode.Enabled = False
        End If
    End Sub

    Private Sub tjam_masuk_Validated(sender As System.Object, e As System.EventArgs) Handles tjam_masuk.Validated
        If addstat Then
            ttol_terlambat.EditValue = TimeValue(DateAdd("n", 15, tjam_masuk.EditValue))
            tawal_masuk.EditValue = TimeValue(DateAdd("n", -40, tjam_masuk.EditValue))
            takhir_masuk.EditValue = TimeValue(DateAdd("n", 15, tjam_masuk.EditValue))
        End If

        'If Not tjam_plg.EditValue.ToString.Equals("00:00:00") And Not tjam_masuk.EditValue.ToString.Equals("00:00:00") Then
        '    tjamkerja.EditValue = hitung_selisih_jam(TimeValue(tjam_masuk.EditValue), TimeValue(tjam_plg.EditValue), "h")

        '    If Not tistirahat1.EditValue.ToString.Equals("00:00:00") And Not tistirahat2.EditValue.ToString.Equals("00:00:00") Then
        '        tjam_istirahat.EditValue = hitung_selisih_jam(TimeValue(tistirahat1.EditValue), TimeValue(tistirahat2.EditValue), "h")
        '    End If

        '    tjamkerja.EditValue = tjamkerja.EditValue - tjam_istirahat.EditValue

        'End If

    End Sub

    Private Sub tjam_plg_Validated(sender As System.Object, e As System.EventArgs) Handles tjam_plg.Validated
        If addstat Then
            ttol_pulang.EditValue = TimeValue(DateAdd("n", -2, tjam_plg.EditValue))
            tawal_pulang.EditValue = TimeValue(DateAdd("n", -30, tjam_plg.EditValue))
            takhir_pulang.EditValue = TimeValue(DateAdd("h", 5, tjam_plg.EditValue))
        End If

        'If Not tjam_plg.EditValue.ToString.Equals("00:00:00") And Not tjam_masuk.EditValue.ToString.Equals("00:00:00") Then
        '    tjamkerja.EditValue = hitung_selisih_jam(TimeValue(tjam_masuk.EditValue), TimeValue(tjam_plg.EditValue), "h")

        '    If Not tistirahat1.EditValue.ToString.Equals("00:00:00") And Not tistirahat2.EditValue.ToString.Equals("00:00:00") Then
        '        tjam_istirahat.EditValue = hitung_selisih_jam(TimeValue(tistirahat1.EditValue), TimeValue(tistirahat2.EditValue), "h")
        '    End If

        '    tjamkerja.EditValue = tjamkerja.EditValue - tjam_istirahat.EditValue

        'End If

    End Sub

    Private Sub tistirahat1_Validated(sender As Object, e As EventArgs) Handles tistirahat1.Validated, tistirahat2.Validated

        If Not tistirahat1.EditValue.ToString.Equals("00:00:00") And Not tistirahat2.EditValue.ToString.Equals("00:00:00") Then
            tjam_istirahat.EditValue = hitung_selisih_jam(TimeValue(tistirahat1.EditValue), TimeValue(tistirahat2.EditValue), "h")

            If Not tjam_plg.EditValue.ToString.Equals("00:00:00") And Not tjam_masuk.EditValue.ToString.Equals("00:00:00") Then
                tjamkerja.EditValue = hitung_selisih_jam(TimeValue(tjam_masuk.EditValue), TimeValue(tjam_plg.EditValue), "h")
            End If

            tjamkerja.EditValue = tjamkerja.EditValue - tjam_istirahat.EditValue

        End If

    End Sub


End Class