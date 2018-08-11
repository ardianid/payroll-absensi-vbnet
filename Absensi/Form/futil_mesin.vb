Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Public Class futil_mesin

    Public axCZKEM1 As New zkemkeeper.CZKEM

    Private bIsConnected = False
    Private iMachineNumber As Integer

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
                        tid1.Text = dread("nomor").ToString
                        tip.Text = dread("noip").ToString
                        tport.Text = dread("noport").ToString
                    ElseIf i = 2 Then
                        tid2.Text = dread("nomor").ToString
                        tip2.Text = dread("noip").ToString
                        tport2.Text = dread("noport").ToString
                    ElseIf i = 3 Then
                        tid3.Text = dread("nomor").ToString
                        tip3.Text = dread("noip").ToString
                        tport3.Text = dread("noport").ToString
                    End If

                End While

                If i = 0 Then

                    tid1.Text = 0
                    tip.Text = "0.0.0.0"
                    tport.Text = 0

                    tid2.Text = 0
                    tip2.Text = "0.0.0.0"
                    tport2.Text = 0

                    tid3.Text = 0
                    tip3.Text = "0.0.0.0"
                    tport3.Text = 0

                ElseIf i = 1 Then

                    tid2.Text = 222
                    tip2.Text = "0.0.0.0"
                    tport2.Text = 0

                    tid3.Text = 222
                    tip3.Text = "0.0.0.0"
                    tport3.Text = 0

                ElseIf i = 2 Then

                    tid3.Text = 222
                    tip3.Text = "0.0.0.0"
                    tport3.Text = 0

                End If

            Else

                tid1.Text = 0
                tip.Text = "0.0.0.0"
                tport.Text = 0

                tid2.Text = 0
                tip2.Text = "0.0.0.0"
                tport2.Text = 0

                tid3.Text = 0
                tip3.Text = "0.0.0.0"
                tport3.Text = 0

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

    Private Sub simpan()

        Dim cn As OleDbConnection = Nothing

        Dim comd As OleDbCommand
        Dim comd2 As OleDbCommand
        Dim comd3 As OleDbCommand

        Dim comds As OleDbCommand


        Dim dread As OleDbDataReader

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction

            Dim sqlinsert As String = String.Format("insert into ms_device (noip,noport) values('{0}',{1})", tip.Text.Trim, tport.EditValue)
            Dim sqlinsert2 As String = String.Format("insert into ms_device (noip,noport) values('{0}',{1})", tip2.Text.Trim, tport2.EditValue)
            Dim sqlinsert3 As String = String.Format("insert into ms_device (noip,noport) values('{0}',{1})", tip3.Text.Trim, tport3.EditValue)

            Dim sqlupdate As String = String.Format("update ms_device set noip='{0}',noport={1} where nomor={2}", tip.Text.Trim, tport.EditValue, tid1.Text.Trim)
            Dim sqlupdate2 As String = String.Format("update ms_device set noip='{0}',noport={1} where nomor={2}", tip2.Text.Trim, tport2.EditValue, tid2.Text.Trim)
            Dim sqlupdate3 As String = String.Format("update ms_device set noip='{0}',noport={1} where nomor={2}", tip3.Text.Trim, tport3.EditValue, tid3.Text.Trim)


            Dim sqls As String = "select * from ms_device"

            comds = New OleDbCommand(sqls, cn, sqltrans)
            dread = comds.ExecuteReader

            If dread.HasRows Then
                If dread.Read Then

                    comd = New OleDbCommand(sqlupdate, cn, sqltrans)
                    comd.ExecuteNonQuery()

                    If Not (tip2.Text = "0.0.0.0") And tid2.Text = 222 Then
                        comd2 = New OleDbCommand(sqlinsert2, cn, sqltrans)
                        comd2.ExecuteNonQuery()
                    Else
                        comd2 = New OleDbCommand(sqlupdate2, cn, sqltrans)
                        comd2.ExecuteNonQuery()
                    End If


                    If Not (tip3.Text = "0.0.0.0") And tid3.Text = 222 Then
                        comd3 = New OleDbCommand(sqlinsert3, cn, sqltrans)
                        comd3.ExecuteNonQuery()
                    Else
                        comd3 = New OleDbCommand(sqlupdate3, cn, sqltrans)
                        comd3.ExecuteNonQuery()
                    End If
                    

                    Clsmy.InsertToLog(cn, "btutilmesin", 0, 1, 0, 0, tip.Text.Trim, tport.EditValue, sqltrans)

                Else

                    comd = New OleDbCommand(sqlinsert, cn, sqltrans)
                    comd.ExecuteNonQuery()

                    comd2 = New OleDbCommand(sqlinsert2, cn, sqltrans)
                    comd2.ExecuteNonQuery()

                    comd3 = New OleDbCommand(sqlinsert3, cn, sqltrans)
                    comd3.ExecuteNonQuery()

                    Clsmy.InsertToLog(cn, "btutilmesin", 1, 0, 0, 0, tip.Text.Trim, tport.EditValue, sqltrans)

                End If
                Else

                    comd = New OleDbCommand(sqlinsert, cn, sqltrans)
                    comd.ExecuteNonQuery()

                    comd2 = New OleDbCommand(sqlinsert2, cn, sqltrans)
                    comd2.ExecuteNonQuery()

                    comd3 = New OleDbCommand(sqlinsert3, cn, sqltrans)
                    comd3.ExecuteNonQuery()

                End If

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

    Private Sub btconn_Click(sender As System.Object, e As System.EventArgs) Handles btconn.Click

        If tip.Text.Trim() = "" Or tport.Text.Trim() = "" Then
            MsgBox("IP dan Port tidak boleh kosong", MsgBoxStyle.Exclamation, "Error")
            Return
        End If
        Dim idwErrorCode As Integer
        open_wait()
        Cursor = Cursors.WaitCursor
        If bIsConnected Then
            axCZKEM1.Disconnect()
            bIsConnected = False
            'btconn.Text = "&Connect"
            ' lblState.Text = "Current State:Disconnected"
            '  Cursor = Cursors.Default
            ' close_wait()
            'Return
        End If

        bIsConnected = axCZKEM1.Connect_Net(tip.Text.Trim(), Convert.ToInt32(tport.Text.Trim()))
        If bIsConnected = True Then
            'btconn.Text = "Disconnect"
            'btconn.Refresh()
            ' lblState.Text = "Current State:Connected"
            iMachineNumber = 1 'In fact,when you are using the tcp/ip communication,this parameter will be ignored,that is any integer will all right.Here we use 1.
            axCZKEM1.RegEvent(iMachineNumber, 65535) 'Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
        Else
            axCZKEM1.GetLastError(idwErrorCode)
            MsgBox("Unable to connect the device,ErrorCode=" & idwErrorCode, MsgBoxStyle.Exclamation, "Error")
        End If

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btconn2_Click(sender As System.Object, e As System.EventArgs) Handles btconn2.Click

        If tip2.Text.Trim() = "" Or tport2.Text.Trim() = "" Then
            MsgBox("IP dan Port tidak boleh kosong", MsgBoxStyle.Exclamation, "Error")
            Return
        End If
        Dim idwErrorCode As Integer
        open_wait()
        Cursor = Cursors.WaitCursor
        If bIsConnected Then
            axCZKEM1.Disconnect()
            bIsConnected = False
            'btconn.Text = "&Connect"
            ' lblState.Text = "Current State:Disconnected"
            'Cursor = Cursors.Default
            'close_wait()
            'Return
        End If

        bIsConnected = axCZKEM1.Connect_Net(tip2.Text.Trim(), Convert.ToInt32(tport2.Text.Trim()))
        If bIsConnected = True Then
            'btconn.Text = "Disconnect"
            'btconn.Refresh()
            ' lblState.Text = "Current State:Connected"
            iMachineNumber = 1 'In fact,when you are using the tcp/ip communication,this parameter will be ignored,that is any integer will all right.Here we use 1.
            axCZKEM1.RegEvent(iMachineNumber, 65535) 'Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
        Else
            axCZKEM1.GetLastError(idwErrorCode)
            MsgBox("Unable to connect the device,ErrorCode=" & idwErrorCode, MsgBoxStyle.Exclamation, "Error")
        End If

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btconn3_Click(sender As System.Object, e As System.EventArgs) Handles btconn3.Click

        If tip3.Text.Trim() = "" Or tport3.Text.Trim() = "" Then
            MsgBox("IP dan Port tidak boleh kosong", MsgBoxStyle.Exclamation, "Error")
            Return
        End If
        Dim idwErrorCode As Integer
        open_wait()
        Cursor = Cursors.WaitCursor
        If bIsConnected Then
            axCZKEM1.Disconnect()
            bIsConnected = False
            'btconn.Text = "&Connect"
            ' lblState.Text = "Current State:Disconnected"
            'Cursor = Cursors.Default
            'close_wait()
            'Return
        End If

        bIsConnected = axCZKEM1.Connect_Net(tip3.Text.Trim(), Convert.ToInt32(tport3.Text.Trim()))
        If bIsConnected = True Then
            'btconn.Text = "Disconnect"
            'btconn.Refresh()
            ' lblState.Text = "Current State:Connected"
            iMachineNumber = 1 'In fact,when you are using the tcp/ip communication,this parameter will be ignored,that is any integer will all right.Here we use 1.
            axCZKEM1.RegEvent(iMachineNumber, 65535) 'Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
        Else
            axCZKEM1.GetLastError(idwErrorCode)
            MsgBox("Unable to connect the device,ErrorCode=" & idwErrorCode, MsgBoxStyle.Exclamation, "Error")
        End If

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btget_Click(sender As System.Object, e As System.EventArgs) Handles btget.Click
        If bIsConnected = False Then
            MsgBox("Please connect the device first", MsgBoxStyle.Exclamation, "Error")
            Return
        End If
        Dim idwErrorCode As Integer

        Dim idwYear As Integer
        Dim idwMonth As Integer
        Dim idwDay As Integer
        Dim idwHour As Integer
        Dim idwMinute As Integer
        Dim idwSecond As Integer

        open_wait()
        Cursor = Cursors.WaitCursor
        If axCZKEM1.GetDeviceTime(iMachineNumber, idwYear, idwMonth, idwDay, idwHour, idwMinute, idwSecond) = True Then
            axCZKEM1.RefreshData(iMachineNumber) 'the data in the device should be refreshed
            ' txtGetDeviceTime.Text = idwYear.ToString() + "-" + idwMonth.ToString() + "-" + idwDay.ToString() + " " + idwHour.ToString() + ":" + idwMinute.ToString() + ":" + idwSecond.ToString()
            ttgl.EditValue = String.Format("{0}/{1}/{2}", idwDay.ToString, idwMonth.ToString, idwYear.ToString)
            ttime.EditValue = String.Format("{0}:{1}:{2}", idwHour.ToString, idwMinute.ToString, idwSecond.ToString)

        Else
            axCZKEM1.GetLastError(idwErrorCode)
            MsgBox("Operation failed,ErrorCode=" & idwErrorCode.ToString(), MsgBoxStyle.Exclamation, "Error")
        End If

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btset_Click(sender As System.Object, e As System.EventArgs) Handles btset.Click

        If bIsConnected = False Then
            MsgBox("Please connect the device first", MsgBoxStyle.Exclamation, "Error")
            Return
        End If
        Dim idwErrorCode As Integer

        Dim djam As String = TimeValue(ttime.EditValue)
        Dim dtanggal As String = DateValue(ttgl.EditValue)

        Dim tanggal As DateTime = String.Format("{0} {1}", dtanggal, djam)

        Dim idwYear As Integer = Convert.ToInt32(tanggal.Year)
        Dim idwMonth As Integer = Convert.ToInt32(tanggal.Month)
        Dim idwDay As Integer = Convert.ToInt32(tanggal.Day)
        Dim idwHour As Integer = Convert.ToInt32(tanggal.Hour)
        Dim idwMinute As Integer = Convert.ToInt32(tanggal.Minute)
        Dim idwSecond As Integer = Convert.ToInt32(tanggal.Second)


        Cursor = Cursors.WaitCursor
        open_wait()
        If axCZKEM1.SetDeviceTime2(iMachineNumber, idwYear, idwMonth, idwDay, idwHour, idwMinute, idwSecond) = True Then
            axCZKEM1.RefreshData(iMachineNumber) 'the data in the device should be refreshed
            MsgBox("Tanggal dan jam telah berhasil dirubah !!!", MsgBoxStyle.Information, "Success")
        Else
            axCZKEM1.GetLastError(idwErrorCode)
            MsgBox("Operation failed,ErrorCode=" & idwErrorCode.ToString(), MsgBoxStyle.Exclamation, "Error")
        End If
        Cursor = Cursors.Default
        close_wait()

    End Sub


    Private Sub btrest_Click(sender As System.Object, e As System.EventArgs) Handles btrest.Click
        If bIsConnected = False Then
            MsgBox("Please connect the device first", MsgBoxStyle.Exclamation, "Error")
            Return
        End If
        'Dim idwErrorCode As Integer

        Cursor = Cursors.WaitCursor
        open_wait()

        Try
            axCZKEM1.RestartDevice(iMachineNumber)

            Cursor = Cursors.Default
            close_wait()

        Catch ex As Exception

            MsgBox(ex.ToString, vbOKOnly + vbExclamation, "Informasi")

            Cursor = Cursors.Default
            close_wait()
        End Try



        'If axCZKEM1.RestartDevice(iMachineNumber) = True Then
        '    MsgBox("The device will restart!", MsgBoxStyle.Information, "Success")
        'Else
        '    axCZKEM1.GetLastError(idwErrorCode)
        '    MsgBox("Operation failed,ErrorCode=" & idwErrorCode.ToString(), MsgBoxStyle.Exclamation, "Error")
        'End If


        'Cursor = Cursors.Default
        'close_wait()
    End Sub

    Private Sub btoff_Click(sender As System.Object, e As System.EventArgs) Handles btoff.Click
        If bIsConnected = False Then
            MsgBox("Please connect the device first", MsgBoxStyle.Exclamation, "Error")
            Return
        End If

        'Dim idwErrorCode As Integer

        Cursor = Cursors.WaitCursor
        open_wait()

        Try
            axCZKEM1.PowerOffDevice(iMachineNumber)

            Cursor = Cursors.Default
            close_wait()

        Catch ex As Exception
            MsgBox(ex.ToString, vbOKOnly + vbExclamation, "Informasi")

            Cursor = Cursors.Default
            close_wait()
        End Try

        'If axCZKEM1.PowerOffDevice(iMachineNumber) = True Then
        '    MsgBox("PowerOffDevice", MsgBoxStyle.Information, "Success")
        'Else
        '    axCZKEM1.GetLastError(idwErrorCode)
        '    MsgBox("Operation failed,ErrorCode=" & idwErrorCode.ToString(), MsgBoxStyle.Exclamation, "Error")
        'End If
        'Cursor = Cursors.Default
        'close_wait()
    End Sub

    Private Sub futil_mesin_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        simpan()

        If bIsConnected Then
            axCZKEM1.Disconnect()
            bIsConnected = False
        End If
    End Sub

    Private Sub futil_mesin_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        loadip()
        '  ttgl.EditValue = Date.Now
    End Sub




End Class