Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Public Class fdownloadlog

    Public axCZKEM1 As New zkemkeeper.CZKEM

    Private bIsConnected = False
    Private iMachineNumber As Integer

    Private bIsConnected2 = False
    Private iMachineNumber2 As Integer

    Private bIsConnected3 = False
    Private iMachineNumber3 As Integer

    Private dvmanager_down As Data.DataViewManager
    Private dv_down As Data.DataView

    Private dvmanager_down2 As Data.DataViewManager
    Private dv_down2 As Data.DataView

    Private dvmanager_down3 As Data.DataViewManager
    Private dv_down3 As Data.DataView

    Dim dtdown As DataTable
    Dim dtdown2 As DataTable
    Dim dtdown3 As DataTable

    Dim no_mesin1 As Integer = 0
    Dim no_mesin2 As Integer = 0
    Dim no_mesin3 As Integer = 0

    Dim tgl_akhir1 As String = ""
    Dim tgl_akhir2 As String = ""
    Dim tgl_akhir3 As String = ""

    Private Sub loadip()

        Dim cn As OleDbConnection = Nothing
        Dim comd As IDbCommand
        Dim dread As OleDbDataReader

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Const sql As String = "select * from ms_device order by nomor"
            comd = New OleDbCommand(sql, cn)
            dread = comd.ExecuteReader

            If dread.HasRows Then

                Dim i As Integer = 0
                While dread.Read

                    i = i + 1

                    If i = 1 Then
                        tip.Text = dread("noip").ToString
                        tport.Text = dread("noport").ToString
                        no_mesin1 = dread("nomor").ToString

                        If Not (dread("tgl_terakhir").ToString = "") Then
                            tgl_akhir1 = convert_date_to_eng(dread("tgl_terakhir").ToString)
                        End If

                    ElseIf i = 2 Then
                        tip2.Text = dread("noip").ToString
                        tport2.Text = dread("noport").ToString
                        no_mesin2 = dread("nomor").ToString

                        If Not (dread("tgl_terakhir").ToString = "") Then
                            tgl_akhir2 = convert_date_to_eng(dread("tgl_terakhir").ToString)
                        End If

                    ElseIf i = 3 Then
                        tip3.Text = dread("noip").ToString
                        tport3.Text = dread("noport").ToString
                        no_mesin3 = dread("nomor").ToString

                        If Not (dread("tgl_terakhir").ToString = "") Then
                            tgl_akhir3 = convert_date_to_eng(dread("tgl_terakhir").ToString)
                        End If

                    End If

                End While

                If i = 1 Then

                    tip2.Text = "0.0.0.0"
                    tport2.Text = 0

                    no_mesin2 = 0
                    tgl_akhir2 = ""

                    tip3.Text = "0.0.0.0"
                    tport3.Text = 0

                    no_mesin3 = 0
                    tgl_akhir3 = ""

                ElseIf i = 2 Then
                    tip3.Text = "0.0.0.0"
                    tport3.Text = 0

                    no_mesin3 = 0
                    tgl_akhir3 = ""

                End If

            Else
                tip.Text = "0.0.0.0"
                tport.Text = 0

                no_mesin1 = 0
                tgl_akhir1 = ""

                tip2.Text = "0.0.0.0"
                tport2.Text = 0

                no_mesin2 = 0
                tgl_akhir2 = ""

                tip3.Text = "0.0.0.0"
                tport3.Text = 0

                no_mesin3 = 0
                tgl_akhir3 = ""

            End If


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

    Private Sub loaddown()

        Const sql As String = "select 0 as noid,userid,'' as checktime1,verifymode,inoutmode,workcode from ms_inout where userid=000"

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet

        '   grid1.DataSource = Nothing

        Try

            open_wait()

            dv_down = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            'dvmanager_down = New DataViewManager(ds)
            'dv_down = dvmanager_down.CreateDataView(ds.Tables(0))

            dtdown = New DataTable
            dtdown = ds.Tables(0)

            griddown.DataSource = dtdown

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

    Private Sub loaddown2()

        Const sql As String = "select 0 as noid,userid,'' as checktime1,verifymode,inoutmode,workcode from ms_inout where userid=000"

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet

        '   grid1.DataSource = Nothing

        Try

            open_wait()

            dv_down2 = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            'dvmanager_down = New DataViewManager(ds)
            'dv_down = dvmanager_down.CreateDataView(ds.Tables(0))

            dtdown2 = New DataTable
            dtdown2 = ds.Tables(0)

            griddown2.DataSource = dtdown2

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

    Private Sub loaddown3()

        Const sql As String = "select 0 as noid,userid,'' as checktime1,verifymode,inoutmode,workcode from ms_inout where userid=000"

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet

        '   grid1.DataSource = Nothing

        Try

            open_wait()

            dv_down3 = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            'dvmanager_down = New DataViewManager(ds)
            'dv_down = dvmanager_down.CreateDataView(ds.Tables(0))

            dtdown3 = New DataTable
            dtdown3 = ds.Tables(0)

            griddown3.DataSource = dtdown3

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


    Private Function buka_mesin1() As Boolean

        If tip.Text.Trim() = "" Or tport.Text.Trim() = "" Then
            MsgBox("IP dan Port tidak boleh kosong", MsgBoxStyle.Exclamation, "Error")
            bIsConnected = False
            Return False
        End If

        loaddown()


        ' cek koneksi 2
        If bIsConnected2 Then
            axCZKEM1.Disconnect()
            bIsConnected2 = False
        End If

        ' cek koneksi 3
        If bIsConnected3 Then
            axCZKEM1.Disconnect()
            bIsConnected3 = False
        End If

        Dim idwErrorCode As Integer

        open_wait()
        Cursor = Cursors.WaitCursor
        If bIsConnected Then
            axCZKEM1.Disconnect()
            bIsConnected = False
            'btconn.Text = "&Connect"
            ' lblState.Text = "Current State:Disconnected"
            Cursor = Cursors.Default
            close_wait()
            Return False
        End If

        bIsConnected = axCZKEM1.Connect_Net(tip.Text.Trim(), Convert.ToInt32(tport.Text.Trim()))
        If bIsConnected = True Then
            '  btconn.Text = "Disconnect"
            '  btconn.Refresh()
            ' lblState.Text = "Current State:Connected"
            iMachineNumber = 1 'In fact,when you are using the tcp/ip communication,this parameter will be ignored,that is any integer will all right.Here we use 1.
            axCZKEM1.RegEvent(iMachineNumber, 65535) 'Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)


            'loaddown()
            '      showpegawai()

        Else
            axCZKEM1.GetLastError(idwErrorCode)
            MsgBox("Unable to connect the device,ErrorCode=" & idwErrorCode, MsgBoxStyle.Exclamation, "Error")
            Return False
        End If

        Cursor = Cursors.Default
        close_wait()

        Return True

    End Function

    Private Function buka_mesin2() As Boolean

        If tip2.Text.Trim() = "" Or tport2.Text.Trim() = "" Then
            MsgBox("IP dan Port tidak boleh kosong", MsgBoxStyle.Exclamation, "Error")
            bIsConnected2 = False
            Return False
        End If

        loaddown2()

        ' cek koneksi 1
        If bIsConnected Then
            axCZKEM1.Disconnect()
            bIsConnected = False
        End If

        ' cek koneksi 3
        If bIsConnected3 Then
            axCZKEM1.Disconnect()
            bIsConnected3 = False
        End If

        Dim idwErrorCode As Integer

        open_wait()
        Cursor = Cursors.WaitCursor
        If bIsConnected2 Then
            axCZKEM1.Disconnect()
            bIsConnected2 = False
            'btconn.Text = "&Connect"
            ' lblState.Text = "Current State:Disconnected"
            Cursor = Cursors.Default
            close_wait()
            Return False
        End If

        bIsConnected2 = axCZKEM1.Connect_Net(tip2.Text.Trim(), Convert.ToInt32(tport2.Text.Trim()))
        If bIsConnected2 = True Then
            '  btconn.Text = "Disconnect"
            '  btconn.Refresh()
            ' lblState.Text = "Current State:Connected"
            iMachineNumber2 = 1 'In fact,when you are using the tcp/ip communication,this parameter will be ignored,that is any integer will all right.Here we use 1.
            axCZKEM1.RegEvent(iMachineNumber, 65535) 'Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)


            'loaddown()
            '      showpegawai()

        Else
            axCZKEM1.GetLastError(idwErrorCode)
            MsgBox("Unable to connect the device,ErrorCode=" & idwErrorCode, MsgBoxStyle.Exclamation, "Error")
            Return False
        End If

        Cursor = Cursors.Default
        close_wait()

        Return True

    End Function

    Private Function buka_mesin3() As Boolean

        If tip3.Text.Trim() = "" Or tport3.Text.Trim() = "" Then
            MsgBox("IP dan Port tidak boleh kosong", MsgBoxStyle.Exclamation, "Error")
            bIsConnected3 = False
            Return False
        End If

        loaddown3()

        ' cek koneksi 2
        If bIsConnected2 Then
            axCZKEM1.Disconnect()
            bIsConnected2 = False
        End If

        ' cek koneksi 1
        If bIsConnected Then
            axCZKEM1.Disconnect()
            bIsConnected = False
        End If

        Dim idwErrorCode As Integer

        open_wait()
        Cursor = Cursors.WaitCursor
        If bIsConnected3 Then
            axCZKEM1.Disconnect()
            bIsConnected3 = False
            'btconn.Text = "&Connect"
            ' lblState.Text = "Current State:Disconnected"
            Cursor = Cursors.Default
            close_wait()
            Return False
        End If

        bIsConnected3 = axCZKEM1.Connect_Net(tip3.Text.Trim(), Convert.ToInt32(tport3.Text.Trim()))
        If bIsConnected3 = True Then
            '  btconn.Text = "Disconnect"
            '  btconn.Refresh()
            ' lblState.Text = "Current State:Connected"
            iMachineNumber3 = 1 'In fact,when you are using the tcp/ip communication,this parameter will be ignored,that is any integer will all right.Here we use 1.
            axCZKEM1.RegEvent(iMachineNumber, 65535) 'Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)


            'loaddown()
            '      showpegawai()

        Else
            axCZKEM1.GetLastError(idwErrorCode)
            MsgBox("Unable to connect the device,ErrorCode=" & idwErrorCode, MsgBoxStyle.Exclamation, "Error")
            Return False
        End If

        Cursor = Cursors.Default
        close_wait()

        Return True

    End Function

    Private Sub tutup_mesin()

        ' cek koneksi
        If bIsConnected Then
            axCZKEM1.Disconnect()
            bIsConnected = False
        End If

        ' cek koneksi 2
        If bIsConnected2 Then
            axCZKEM1.Disconnect()
            bIsConnected2 = False
        End If

        ' cek koneksi 3
        If bIsConnected3 Then
            axCZKEM1.Disconnect()
            bIsConnected3 = False
        End If

    End Sub

    Private Sub fdownloadlog_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        ' cek koneksi 1
        If bIsConnected Then
            axCZKEM1.Disconnect()
            bIsConnected = False
        End If

        ' cek koneksi 2
        If bIsConnected2 Then
            axCZKEM1.Disconnect()
            bIsConnected2 = False
        End If

        ' cek koneksi 3
        If bIsConnected3 Then
            axCZKEM1.Disconnect()
            bIsConnected3 = False
        End If

    End Sub


    Private Sub fdownloadlog_Load(sender As Object, e As EventArgs) Handles Me.Load

        '  cbopsi.SelectedIndex = 0

        loadip()
        ' XtraTabControl1.Visible = False

        lstat.Text = ""


    End Sub

    Private Sub btproses_Click(sender As Object, e As EventArgs) Handles btproses.Click

        If bIsConnected = False Then
            If buka_mesin1() = False Then
                Return
            End If
        End If

        Dim sdwEnrollNumber As String = ""
        Dim idwVerifyMode As Integer
        Dim idwInOutMode As Integer
        Dim idwYear As Integer
        Dim idwMonth As Integer
        Dim idwDay As Integer
        Dim idwHour As Integer
        Dim idwMinute As Integer
        Dim idwSecond As Integer
        Dim idwWorkcode As Integer

        Dim idwErrorCode As Integer
        Dim iGLCount = 0
        '  Dim lvItem As New ListViewItem("Items", 0)

        Cursor = Cursors.WaitCursor

        loaddown()
        ' Dim tanggalakhir As String = cek_tgl_terakhir()

        axCZKEM1.EnableDevice(iMachineNumber, False) 'disable the device
        If axCZKEM1.ReadGeneralLogData(iMachineNumber) Then 'read all the attendance records to the memory
            'get records from the memory

            lstat.Text = "[MESIN 1] Proses Download..."
            '     open_wait()
            Dim cn As OleDbConnection = Nothing

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim comds As OleDbCommand = Nothing
                Dim comd As OleDbCommand
                Dim dread As OleDbDataReader = Nothing

                Dim sqltrans As OleDbTransaction = cn.BeginTransaction

                Dim tgl_banding As String = ""

                While axCZKEM1.SSR_GetGeneralLogData(iMachineNumber, sdwEnrollNumber, idwVerifyMode, idwInOutMode, idwYear, idwMonth, idwDay, idwHour, idwMinute, idwSecond, idwWorkcode)
                    iGLCount += 1

                    Dim tgl_eng As String = String.Format("{0}-{1}-{2} {3}:{4}:{5}", idwYear, idwMonth, idwDay, idwHour, idwMinute, idwSecond)


                    Dim sqls As String = String.Format("select userid from ms_inout where userid={0} and checktime='{1}'", sdwEnrollNumber, tgl_eng)
                    Dim sqlinsert As String = String.Format("insert into ms_inout (userid,checktime,verifymode,inoutmode,workcode) values({0},'{1}',{2},{3},{4})", sdwEnrollNumber, tgl_eng, Convert.ToInt32(idwVerifyMode.ToString()), Convert.ToInt32(idwInOutMode.ToString()), Convert.ToInt32(idwWorkcode))
                    Dim sqlupdate As String = String.Format("update ms_inout set verifymode={0},inoutmode={1},workcode={2} where userid={3} and checktime='{4}'", Convert.ToInt32(idwVerifyMode.ToString()), Convert.ToInt32(idwInOutMode.ToString()), Convert.ToInt32(idwWorkcode), sdwEnrollNumber, tgl_eng)

                    Dim sql_gab As String = String.Format("if not exists ({0}) begin {1} end", sqls, sqlinsert)

                    If IsNumeric(sdwEnrollNumber) Then

                        tgl_banding = String.Format("{0}/{1}/{2}", idwDay, idwMonth, idwYear)
                        tgl_banding = convert_date_to_eng(tgl_banding)

                        If Not (tgl_akhir1 = "") Then
                            If Not (DateValue(tgl_banding) < DateValue(tgl_akhir1)) Then

                                comd = New OleDbCommand(sql_gab, cn, sqltrans)
                                comd.ExecuteNonQuery()

                            End If
                        Else

                            comd = New OleDbCommand(sql_gab, cn, sqltrans)
                            comd.ExecuteNonQuery()

                        End If

                    Dim orow As DataRow = dtdown.NewRow
                    orow("noid") = iGLCount
                    orow("userid") = sdwEnrollNumber
                    orow("checktime1") = String.Format("{0}-{1}-{2} {3}:{4}:{5}", idwDay, idwMonth, idwYear, idwHour, idwMinute, idwSecond)
                    orow("verifymode") = idwVerifyMode.ToString()
                    orow("inoutmode") = idwInOutMode.ToString()
                    orow("workcode") = idwWorkcode

                    dtdown.Rows.Add(orow)

                        'GridView1.FocusedRowHandle = iGLCount
                        'GridView1.SelectRow(iGLCount)

                    Application.DoEvents()
                    ' System.Threading.Thread.Sleep(500)

                    'lvItem = lvLogs.Items.Add(iGLCount.ToString())
                    'lvItem.SubItems.Add(sdwEnrollNumber)
                    'lvItem.SubItems.Add(idwVerifyMode.ToString())
                    'lvItem.SubItems.Add(idwInOutMode.ToString())
                    'lvItem.SubItems.Add(idwYear.ToString() & "-" + idwMonth.ToString() & "-" & idwDay.ToString() & " " & idwHour.ToString() & ":" & idwMinute.ToString() & ":" & idwSecond.ToString())
                    'lvItem.SubItems.Add(idwWorkcode.ToString())
                    End If
                End While

                If Not (tgl_banding = "") Then

                    Dim sqlup_ak As String = String.Format("update ms_device set tgl_terakhir='{0}' where nomor='{1}'", tgl_banding, no_mesin1)
                    Using cmdup_ak As OleDbCommand = New OleDbCommand(sqlup_ak, cn, sqltrans)
                        cmdup_ak.ExecuteNonQuery()
                    End Using

                End If
                

                sqltrans.Commit()

                lstat.Text = "[MESIN 1] Download Log Selesai..."

                ' close_wait()
                ' MsgBox("Download Data selesai...")


            Catch ex As Exception
                '  close_wait()
                lstat.Text = "[MESIN 1] Proses Download error.."
                If Not cn Is Nothing Then
                    If cn.State = ConnectionState.Open Then
                        cn.Close()
                    End If
                End If
            End Try


        Else
            Cursor = Cursors.Default
            axCZKEM1.GetLastError(idwErrorCode)
            If idwErrorCode <> 0 Then
                MsgBox("Reading data from terminal failed,ErrorCode: " & idwErrorCode, MsgBoxStyle.Exclamation, "Error")
            Else
                MsgBox("No data from terminal returns!", MsgBoxStyle.Exclamation, "Error")
            End If
        End If

        axCZKEM1.EnableDevice(iMachineNumber, True) 'enable the device

        tutup_mesin()

        Cursor = Cursors.Default

        loadip()

    End Sub

    Private Sub btproses2_Click(sender As Object, e As EventArgs) Handles btproses2.Click

        If bIsConnected2 = False Then
            If buka_mesin2() = False Then
                Return
            End If
        End If

        Dim sdwEnrollNumber As String = ""
        Dim idwVerifyMode As Integer
        Dim idwInOutMode As Integer
        Dim idwYear As Integer
        Dim idwMonth As Integer
        Dim idwDay As Integer
        Dim idwHour As Integer
        Dim idwMinute As Integer
        Dim idwSecond As Integer
        Dim idwWorkcode As Integer

        Dim idwErrorCode As Integer
        Dim iGLCount = 0
        '  Dim lvItem As New ListViewItem("Items", 0)

        Cursor = Cursors.WaitCursor

        loaddown2()
        ' Dim tanggalakhir As String = cek_tgl_terakhir()

        axCZKEM1.EnableDevice(iMachineNumber2, False) 'disable the device
        If axCZKEM1.ReadGeneralLogData(iMachineNumber2) Then 'read all the attendance records to the memory
            'get records from the memory

            lstat.Text = "[MESIN 2] Proses Download..."
            '     open_wait()
            Dim cn As OleDbConnection = Nothing

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim comds As OleDbCommand = Nothing
                Dim comd As OleDbCommand
                Dim dread As OleDbDataReader = Nothing

                Dim sqltrans As OleDbTransaction = cn.BeginTransaction

                Dim tgl_banding As String = ""

                While axCZKEM1.SSR_GetGeneralLogData(iMachineNumber, sdwEnrollNumber, idwVerifyMode, idwInOutMode, idwYear, idwMonth, idwDay, idwHour, idwMinute, idwSecond, idwWorkcode)
                    iGLCount += 1

                    Dim tgl_eng As String = String.Format("{0}-{1}-{2} {3}:{4}:{5}", idwYear, idwMonth, idwDay, idwHour, idwMinute, idwSecond)

                    Dim sqls As String = String.Format("select userid from ms_inout where userid={0} and checktime='{1}'", sdwEnrollNumber, tgl_eng)
                    Dim sqlinsert As String = String.Format("insert into ms_inout (userid,checktime,verifymode,inoutmode,workcode) values({0},'{1}',{2},{3},{4})", sdwEnrollNumber, tgl_eng, Convert.ToInt32(idwVerifyMode.ToString()), Convert.ToInt32(idwInOutMode.ToString()), Convert.ToInt32(idwWorkcode))
                    Dim sqlupdate As String = String.Format("update ms_inout set verifymode={0},inoutmode={1},workcode={2} where userid={3} and checktime='{4}'", Convert.ToInt32(idwVerifyMode.ToString()), Convert.ToInt32(idwInOutMode.ToString()), Convert.ToInt32(idwWorkcode), sdwEnrollNumber, tgl_eng)

                    Dim sql_gab As String = String.Format("if not exists ({0}) begin {1} end", sqls, sqlinsert)

                    If IsNumeric(sdwEnrollNumber) Then

                        tgl_banding = String.Format("{0}/{1}/{2}", idwDay, idwMonth, idwYear)
                        tgl_banding = convert_date_to_eng(tgl_banding)

                        If Not (tgl_akhir2 = "") Then
                            If Not (DateValue(tgl_banding) < DateValue(tgl_akhir2)) Then

                                comd = New OleDbCommand(sql_gab, cn, sqltrans)
                                comd.ExecuteNonQuery()

                            End If
                        Else

                            comd = New OleDbCommand(sql_gab, cn, sqltrans)
                            comd.ExecuteNonQuery()

                        End If




                        Dim orow As DataRow = dtdown2.NewRow
                        orow("noid") = iGLCount
                        orow("userid") = sdwEnrollNumber
                        orow("checktime1") = String.Format("{0}-{1}-{2} {3}:{4}:{5}", idwDay, idwMonth, idwYear, idwHour, idwMinute, idwSecond)
                        orow("verifymode") = idwVerifyMode.ToString()
                        orow("inoutmode") = idwInOutMode.ToString()
                        orow("workcode") = idwWorkcode

                        dtdown2.Rows.Add(orow)

                        'GridView2.FocusedRowHandle = iGLCount
                        'GridView2.SelectRow(iGLCount)

                        Application.DoEvents()
                        ' System.Threading.Thread.Sleep(500)

                        'lvItem = lvLogs.Items.Add(iGLCount.ToString())
                        'lvItem.SubItems.Add(sdwEnrollNumber)
                        'lvItem.SubItems.Add(idwVerifyMode.ToString())
                        'lvItem.SubItems.Add(idwInOutMode.ToString())
                        'lvItem.SubItems.Add(idwYear.ToString() & "-" + idwMonth.ToString() & "-" & idwDay.ToString() & " " & idwHour.ToString() & ":" & idwMinute.ToString() & ":" & idwSecond.ToString())
                        'lvItem.SubItems.Add(idwWorkcode.ToString())
                    End If
                End While

                If Not (tgl_banding = "") Then

                    Dim sqlup_ak As String = String.Format("update ms_device set tgl_terakhir='{0}' where nomor='{1}'", tgl_banding, no_mesin2)
                    Using cmdup_ak As OleDbCommand = New OleDbCommand(sqlup_ak, cn, sqltrans)
                        cmdup_ak.ExecuteNonQuery()
                    End Using

                End If

                sqltrans.Commit()

                lstat.Text = "[MESIN 2] Download Log Selesai..."

                ' close_wait()
                ' MsgBox("Download Data selesai...")


            Catch ex As Exception
                '  close_wait()
                lstat.Text = "[MESIN 2] Proses Download error.."
                If Not cn Is Nothing Then
                    If cn.State = ConnectionState.Open Then
                        cn.Close()
                    End If
                End If
            End Try


        Else
            Cursor = Cursors.Default
            axCZKEM1.GetLastError(idwErrorCode)
            If idwErrorCode <> 0 Then
                MsgBox("Reading data from terminal failed,ErrorCode: " & idwErrorCode, MsgBoxStyle.Exclamation, "Error")
            Else
                MsgBox("No data from terminal returns!", MsgBoxStyle.Exclamation, "Error")
            End If
        End If

        axCZKEM1.EnableDevice(iMachineNumber2, True) 'enable the device

        tutup_mesin()

        Cursor = Cursors.Default

        loadip()

    End Sub

    Private Sub btproses3_Click(sender As Object, e As EventArgs) Handles btproses3.Click

        If bIsConnected3 = False Then
            If buka_mesin3() = False Then
                Return
            End If
        End If

        Dim sdwEnrollNumber As String = ""
        Dim idwVerifyMode As Integer
        Dim idwInOutMode As Integer
        Dim idwYear As Integer
        Dim idwMonth As Integer
        Dim idwDay As Integer
        Dim idwHour As Integer
        Dim idwMinute As Integer
        Dim idwSecond As Integer
        Dim idwWorkcode As Integer

        Dim idwErrorCode As Integer
        Dim iGLCount = 0
        '  Dim lvItem As New ListViewItem("Items", 0)

        Cursor = Cursors.WaitCursor

        loaddown3()
        ' Dim tanggalakhir As String = cek_tgl_terakhir()

        axCZKEM1.EnableDevice(iMachineNumber3, False) 'disable the device
        If axCZKEM1.ReadGeneralLogData(iMachineNumber3) Then 'read all the attendance records to the memory
            'get records from the memory

            lstat.Text = "[MESIN 3] Proses Download..."
            '     open_wait()
            Dim cn As OleDbConnection = Nothing

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim comds As OleDbCommand = Nothing
                Dim comd As OleDbCommand
                Dim dread As OleDbDataReader = Nothing

                Dim sqltrans As OleDbTransaction = cn.BeginTransaction

                Dim tgl_banding As String = ""

                While axCZKEM1.SSR_GetGeneralLogData(iMachineNumber, sdwEnrollNumber, idwVerifyMode, idwInOutMode, idwYear, idwMonth, idwDay, idwHour, idwMinute, idwSecond, idwWorkcode)
                    iGLCount += 1

                    Dim tgl_eng As String = String.Format("{0}-{1}-{2} {3}:{4}:{5}", idwYear, idwMonth, idwDay, idwHour, idwMinute, idwSecond)

                    Dim sqls As String = String.Format("select userid from ms_inout where userid={0} and checktime='{1}'", sdwEnrollNumber, tgl_eng)
                    Dim sqlinsert As String = String.Format("insert into ms_inout (userid,checktime,verifymode,inoutmode,workcode) values({0},'{1}',{2},{3},{4})", sdwEnrollNumber, tgl_eng, Convert.ToInt32(idwVerifyMode.ToString()), Convert.ToInt32(idwInOutMode.ToString()), Convert.ToInt32(idwWorkcode))
                    Dim sqlupdate As String = String.Format("update ms_inout set verifymode={0},inoutmode={1},workcode={2} where userid={3} and checktime='{4}'", Convert.ToInt32(idwVerifyMode.ToString()), Convert.ToInt32(idwInOutMode.ToString()), Convert.ToInt32(idwWorkcode), sdwEnrollNumber, tgl_eng)

                    Dim sql_gab As String = String.Format("if not exists ({0}) begin {1} end", sqls, sqlinsert)

                    If IsNumeric(sdwEnrollNumber) Then

                        tgl_banding = String.Format("{0}/{1}/{2}", idwDay, idwMonth, idwYear)
                        tgl_banding = convert_date_to_eng(tgl_banding)

                        If Not (tgl_akhir3 = "") Then
                            If Not (DateValue(tgl_banding) < DateValue(tgl_akhir3)) Then

                                comd = New OleDbCommand(sql_gab, cn, sqltrans)
                                comd.ExecuteNonQuery()

                            End If
                        Else

                            comd = New OleDbCommand(sql_gab, cn, sqltrans)
                            comd.ExecuteNonQuery()

                        End If

                        Dim orow As DataRow = dtdown3.NewRow
                        orow("noid") = iGLCount
                        orow("userid") = sdwEnrollNumber
                        orow("checktime1") = String.Format("{0}-{1}-{2} {3}:{4}:{5}", idwDay, idwMonth, idwYear, idwHour, idwMinute, idwSecond)
                        orow("verifymode") = idwVerifyMode.ToString()
                        orow("inoutmode") = idwInOutMode.ToString()
                        orow("workcode") = idwWorkcode

                        dtdown3.Rows.Add(orow)

                        'GridView3.FocusedRowHandle = iGLCount
                        'GridView3.SelectRow(iGLCount)

                        Application.DoEvents()
                        ' System.Threading.Thread.Sleep(500)

                        'lvItem = lvLogs.Items.Add(iGLCount.ToString())
                        'lvItem.SubItems.Add(sdwEnrollNumber)
                        'lvItem.SubItems.Add(idwVerifyMode.ToString())
                        'lvItem.SubItems.Add(idwInOutMode.ToString())
                        'lvItem.SubItems.Add(idwYear.ToString() & "-" + idwMonth.ToString() & "-" & idwDay.ToString() & " " & idwHour.ToString() & ":" & idwMinute.ToString() & ":" & idwSecond.ToString())
                        'lvItem.SubItems.Add(idwWorkcode.ToString())
                    End If
                End While

                If Not (tgl_banding = "") Then

                    Dim sqlup_ak As String = String.Format("update ms_device set tgl_terakhir='{0}' where nomor='{1}'", tgl_banding, no_mesin3)
                    Using cmdup_ak As OleDbCommand = New OleDbCommand(sqlup_ak, cn, sqltrans)
                        cmdup_ak.ExecuteNonQuery()
                    End Using

                End If

                sqltrans.Commit()

                lstat.Text = "[MESIN 3] Download Log Selesai..."

                ' close_wait()
                ' MsgBox("Download Data selesai...")


            Catch ex As Exception
                '  close_wait()
                lstat.Text = "[MESIN 3] Proses Download error.."
                If Not cn Is Nothing Then
                    If cn.State = ConnectionState.Open Then
                        cn.Close()
                    End If
                End If
            End Try


        Else
            Cursor = Cursors.Default
            axCZKEM1.GetLastError(idwErrorCode)
            If idwErrorCode <> 0 Then
                MsgBox("Reading data from terminal failed,ErrorCode: " & idwErrorCode, MsgBoxStyle.Exclamation, "Error")
            Else
                MsgBox("No data from terminal returns!", MsgBoxStyle.Exclamation, "Error")
            End If
        End If

        axCZKEM1.EnableDevice(iMachineNumber3, True) 'enable the device

        tutup_mesin()

        Cursor = Cursors.Default

        loadip()

    End Sub

    Public Function cek_tgl_terakhir() As String

        Dim hasil As String = ""

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim cmd As OleDbCommand = New OleDbCommand("select max(convert(date,checktime)) from ms_inout ", cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If drd(0).ToString.Trim.Length > 0 Then
                    hasil = drd(0).ToString
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

        Return hasil

    End Function

    Private Sub btclear_Click(sender As Object, e As EventArgs) Handles btclear.Click
        If bIsConnected = False Then
            If buka_mesin1() = False Then
                Return
            End If
        End If

        If MsgBox("Yakin akan dihapus semua log absen dari mesin ???", vbYesNo + vbQuestion, "Konfirmasi") = vbNo Then
            Return
        End If

        Dim idwErrorCode As Integer

        ' lvLogs.Items.Clear()
        axCZKEM1.EnableDevice(iMachineNumber, False) 'disable the device
        If axCZKEM1.ClearGLog(iMachineNumber) = True Then
            axCZKEM1.RefreshData(iMachineNumber) 'the data in the device should be refreshed
            MsgBox("Semua data log absensi sudah dihapus dari mesin!", MsgBoxStyle.Information, "Success")
        Else
            axCZKEM1.GetLastError(idwErrorCode)
            MsgBox("Operation failed,ErrorCode=" & idwErrorCode, MsgBoxStyle.Exclamation, "Error")
        End If

        axCZKEM1.EnableDevice(iMachineNumber, True) 'enable the device
    End Sub

    Private Sub btclear2_Click(sender As Object, e As EventArgs) Handles btclear2.Click
        If bIsConnected2 = False Then
            If buka_mesin2() = False Then
                Return
            End If
        End If

        If MsgBox("Yakin akan dihapus semua log absen dari mesin ???", vbYesNo + vbQuestion, "Konfirmasi") = vbNo Then
            Return
        End If

        Dim idwErrorCode As Integer

        ' lvLogs.Items.Clear()
        axCZKEM1.EnableDevice(iMachineNumber2, False) 'disable the device
        If axCZKEM1.ClearGLog(iMachineNumber2) = True Then
            axCZKEM1.RefreshData(iMachineNumber2) 'the data in the device should be refreshed
            MsgBox("Semua data log absensi sudah dihapus dari mesin!", MsgBoxStyle.Information, "Success")
        Else
            axCZKEM1.GetLastError(idwErrorCode)
            MsgBox("Operation failed,ErrorCode=" & idwErrorCode, MsgBoxStyle.Exclamation, "Error")
        End If

        axCZKEM1.EnableDevice(iMachineNumber2, True) 'enable the device
    End Sub

    Private Sub btclear3_Click(sender As Object, e As EventArgs) Handles btclear3.Click
        If bIsConnected3 = False Then
            If buka_mesin3() = False Then
                Return
            End If
        End If

        If MsgBox("Yakin akan dihapus semua log absen dari mesin ???", vbYesNo + vbQuestion, "Konfirmasi") = vbNo Then
            Return
        End If

        Dim idwErrorCode As Integer

        ' lvLogs.Items.Clear()
        axCZKEM1.EnableDevice(iMachineNumber3, False) 'disable the device
        If axCZKEM1.ClearGLog(iMachineNumber3) = True Then
            axCZKEM1.RefreshData(iMachineNumber3) 'the data in the device should be refreshed
            MsgBox("Semua data log absensi sudah dihapus dari mesin!", MsgBoxStyle.Information, "Success")
        Else
            axCZKEM1.GetLastError(idwErrorCode)
            MsgBox("Operation failed,ErrorCode=" & idwErrorCode, MsgBoxStyle.Exclamation, "Error")
        End If

        axCZKEM1.EnableDevice(iMachineNumber3, True) 'enable the device
    End Sub


End Class