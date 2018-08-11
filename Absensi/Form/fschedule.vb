Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Public Class fschedule
    Private bs1 As BindingSource
    Private ds As DataSet
    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private okok As Boolean
    Private updateok As Boolean
    Private stattrans As String

    Private Sub Get_Aksesform()

        Dim rows() As DataRow = dtmenu.Select(String.Format("NAMAFORM='{0}'", Me.Name.ToUpper.ToString))

        If Convert.ToInt16(rows(0)("t_add")) = 1 Then
            tsload.Enabled = True
            '   tssimpan.Enabled = True
            okok = True
        Else
            tsload.Enabled = False
            '   tsload.Enabled = False
            okok = False
        End If

        If Convert.ToInt16(rows(0)("t_edit")) = 1 Then
            updateok = True
        Else
            updateok = False
        End If

        'If Convert.ToInt16(rows(0)("t_del")) = 1 Then
        '    tsdel.Enabled = True
        'Else
        '    tsdel.Enabled = False
        'End If

        'If Convert.ToInt16(rows(0)("t_lap")) = 1 Then

        'Else

        'End If

    End Sub

    Private Sub addyears()

        Dim yearnow As Integer = Year(Now)
        Dim x As Integer = 0
        Dim pos As Integer = 0

        For i As Integer = 2010 To 2035
            cbtahun.Items.Add(i)

            If yearnow = i Then
                pos = x
            End If

            x = x + 1

        Next

        cbtahun.SelectedIndex = pos

    End Sub

    Private Sub loadsched()
        Dim sql As String = String.Format("select * from ms_kalender where year(tanggal)='{0}'", cbtahun.Text.Trim)
        Dim cn As OleDbConnection = Nothing

        grid1.DataSource = Nothing

        Try

            open_wait()

            dv1 = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager = New DataViewManager(ds)
            dv1 = dvmanager.CreateDataView(ds.Tables(0))

            bs1 = New BindingSource
            bs1.DataSource = dv1
            bn1.BindingSource = bs1

            grid1.DataSource = bs1

            close_wait()

            If dv1.Count = 0 Then

                If okok = True Then
                    tsload.Enabled = True
                Else
                    tsload.Enabled = False
                End If

            Else
                tsload.Enabled = False
            End If

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

    Private Sub createschedule()

        Try

            open_wait()

            loadsched()

            Dim tgl As String = "01/01/" & cbtahun.Text.Trim
            Dim time As New DateTime((cbtahun.Text.Trim + 1), 1, 1)
            time = time.AddDays(-1)

            Dim tgl1 As Date = convert_date_to_ind(tgl)
            Dim tgl2 As Date = convert_date_to_ind(time)
            Dim jmlhari As Integer = tgl2.DayOfYear


            Dim tgladd As Date

            Dim weekis As Integer = 0
            Dim hari As String

            For i As Integer = 1 To jmlhari

                If i = 1 Then
                    tgladd = tgl1
                Else
                    tgladd = convert_date_to_ind(DateAdd(DateInterval.Day, 1, tgladd))
                End If

                weekis = DatePart(DateInterval.WeekOfYear, tgladd)
                hari = DatePart(DateInterval.Weekday, tgladd)

                Select Case hari
                    Case 1
                        hari = "MINGGU"
                    Case 2
                        hari = "SENIN"
                    Case 3
                        hari = "SELASA"
                    Case 4
                        hari = "RABU"
                    Case 5
                        hari = "KAMIS"
                    Case 6
                        hari = "JUMAT"
                    Case 7
                        hari = "SABTU"
                End Select

                Dim orow As DataRowView = dv1.AddNew
                orow("tanggal") = tgladd
                orow("minggu") = weekis
                orow("hari") = hari

                If hari = "MINGGU" Then
                    orow("libur") = 1
                    orow("jenislibur") = "Libur Normal"
                Else
                    orow("libur") = 0
                    orow("jenislibur") = "-"
                End If


                orow("ket") = ""
                dv1.EndInit()

            Next


            close_wait()

        Catch ex As Exception
            close_wait()
            MsgBox(ex.ToString)
        End Try

    End Sub

    Private Sub simpan()

        Dim sql As String = ""
        Dim cn As OleDbConnection = Nothing
        Dim comd As OleDbCommand

        Try

            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction

            For i As Integer = 0 To dv1.Count - 1

                Dim jenislib As String = ""
                If dv1(i)("libur").ToString.Equals("0") Then
                    jenislib = "-"
                    dv1(i)("jenislibur") = "-"
                Else
                    jenislib = dv1(i)("jenislibur").ToString
                End If

                If stattrans = "add" Then
                    sql = String.Format("insert into ms_kalender (tanggal,minggu,hari,libur,ket,jenislibur) values('{0}',{1},'{2}',{3},'{4}','{5}')", _
                    convert_date_to_eng(dv1(i)("tanggal").ToString), Convert.ToInt32(dv1(i)("minggu").ToString), dv1(i)("hari").ToString, Convert.ToInt32(dv1(i)("libur").ToString), dv1(i)("ket").ToString, jenislib)

                    comd = New OleDbCommand(sql, cn, sqltrans)
                    comd.ExecuteNonQuery()

                    Clsmy.InsertToLog(cn, "btsched", 1, 0, 0, 0, cbtahun.Text.Trim, "", sqltrans)

                Else

                    sql = String.Format("update ms_kalender set libur={0},ket='{1}',jenislibur='{2}' where tanggal='{3}'", Convert.ToInt32(dv1(i)("libur").ToString), dv1(i)("ket").ToString, jenislib, convert_date_to_eng(dv1(i)("tanggal").ToString))

                    comd = New OleDbCommand(sql, cn, sqltrans)
                    comd.ExecuteNonQuery()

                    Clsmy.InsertToLog(cn, "btsched", 0, 1, 0, 0, cbtahun.Text.Trim, "", sqltrans)

                End If

            Next

            sqltrans.Commit()

            close_wait()

            MsgBox("Data disimpan...", vbOKOnly + vbInformation, "Informasi")


            tsload.Enabled = False
            stattrans = "edit"

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

    Private Sub cbtahun_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cbtahun.SelectedIndexChanged
        loadsched()

        If okok = False Then
            Exit Sub
        End If

        If dv1.Count = 0 Then

            If okok = True Then
                tsload.Enabled = True
            Else
                tsload.Enabled = False
            End If

        Else
            tsload.Enabled = False
            '  tssimpan.Enabled = True
        End If

    End Sub

    Private Sub tsload_Click(sender As System.Object, e As System.EventArgs) Handles tsload.Click
        createschedule()
        stattrans = "add"
    End Sub

    Private Sub tssimpan_Click(sender As System.Object, e As System.EventArgs) Handles tssimpan.Click

        If dv1.Count = 0 Then
            Exit Sub
        End If

        If stattrans = "add" Then

            If okok = False Then
                MsgBox("Anda tidak berhak untuk menambah hari kerja, hub admin", vbOKOnly + vbInformation, "Informasi")
                Exit Sub
            End If

        Else

            If updateok = False Then
                MsgBox("Anda tidak berhak untuk merubah hari kerja, hub admin", vbOKOnly + vbInformation, "Informasi")
                Exit Sub
            End If

        End If

        simpan()
    End Sub

    Private Sub fschedule_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        cbtahun.Focus()
    End Sub

    Private Sub fschedule_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Get_Aksesform()
        addyears()

        stattrans = "edit"

    End Sub
End Class