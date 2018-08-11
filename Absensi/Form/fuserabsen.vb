Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Public Class fuserabsen

    Public axCZKEM1 As New zkemkeeper.CZKEM

    Private bIsConnected = False
    Private iMachineNumber As Integer

    Private bs1 As BindingSource
    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private dvmanager2 As Data.DataViewManager
    Private dvload As DataView

    Private Sub showloaguser()

        Const sql As String = "select userid,'' as nama,'' as priv from ms_inout where userid='000'"

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet

        grid2.DataSource = Nothing

        Try

            open_wait()

            dvload = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager2 = New DataViewManager(ds)
            dvload = dvmanager2.CreateDataView(ds.Tables(0))

            grid2.DataSource = dvload

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

    Private Sub loadip()

        Dim cn As OleDbConnection = Nothing
        Dim comd As IDbCommand
        Dim dread As OleDbDataReader

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Const sql As String = "select * from ms_device"
            comd = New OleDbCommand(sql, cn)
            dread = comd.ExecuteReader

            If dread.HasRows Then
                If dread.Read Then
                    tip.Text = dread(0).ToString
                    tport.Text = dread(1).ToString

                Else
                    tip.Text = "0.0.0.0"
                    tport.Text = 0

                End If
            Else
                tip.Text = "0.0.0.0"
                tport.Text = 0

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

    Private Sub showpegawai()

        Const sql As String = "select nip,idmesin,nama,'' as stat,0 as transfer from ms_karyawan where aktif=1"

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet

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

    Private Sub btconn_Click(sender As System.Object, e As System.EventArgs) Handles btconn.Click

        If tip.Text.Trim() = "" Or tport.Text.Trim() = "" Then
            MsgBox("IP dan Port tidak boleh kosong", MsgBoxStyle.Exclamation, "Error")
            Return
        End If

        'PanelControl1.Visible = False
        'XtraTabControl1.Visible = True

        '   loaddown()
        showpegawai()
        showloaguser()

        'Exit Sub

        Dim idwErrorCode As Integer

        open_wait()
        Cursor = Cursors.WaitCursor
        If btconn.Text = "Disconnect" Then
            axCZKEM1.Disconnect()
            bIsConnected = False
            btconn.Text = "&Connect"
            ' lblState.Text = "Current State:Disconnected"
            Cursor = Cursors.Default
            close_wait()
            Return
        End If

        bIsConnected = axCZKEM1.Connect_Net(tip.Text.Trim(), Convert.ToInt32(tport.Text.Trim()))
        If bIsConnected = True Then
            '  btconn.Text = "Disconnect"
            '  btconn.Refresh()
            ' lblState.Text = "Current State:Connected"
            iMachineNumber = 1 'In fact,when you are using the tcp/ip communication,this parameter will be ignored,that is any integer will all right.Here we use 1.
            axCZKEM1.RegEvent(iMachineNumber, 65535) 'Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
            PanelControl1.Visible = False
            XtraTabControl1.Visible = True

            '    loaddown()
            'showpegawai()
            'showloaguser()

        Else
            axCZKEM1.GetLastError(idwErrorCode)
            MsgBox("Unable to connect the device,ErrorCode=" & idwErrorCode, MsgBoxStyle.Exclamation, "Error")
        End If

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub tsref_Click(sender As System.Object, e As System.EventArgs) Handles tsref.Click
        showpegawai()
    End Sub

    Private Sub cbopsi_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbopsi.SelectedIndexChanged

        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count <= 0 Then
            Exit Sub
        End If

        For i = 0 To dv1.Count - 1

            If cbopsi.SelectedIndex = 1 Then
                dv1(i)("transfer") = 1
            ElseIf cbopsi.SelectedIndex = 2 Then
                dv1(i)("transfer") = 0
            End If

        Next

    End Sub

    Private Sub trproses_Click(sender As System.Object, e As System.EventArgs) Handles trproses.Click

        If bIsConnected = False Then
            MsgBox("Please connect the device first", MsgBoxStyle.Exclamation, "Error")
            Return
        End If

        If dv1.Count = 0 Then
            Return
        End If

        open_wait()

        Try

            Dim idwErrorCode As Integer
            axCZKEM1.EnableDevice(iMachineNumber, False)


            For i As Integer = 0 To dv1.Count - 1
                If dv1(i)("transfer").Equals(1) Then

                    Dim sdwEnrollNumber As Integer = dv1(i)("idmesin").ToString
                    Dim sName As String = dv1(i)("nama").ToString
                    Dim sPassword As String = ""
                    Dim iPrivilege As Integer = 0
                    Dim sCardnumber As String = dv1(i)("idmesin").ToString
                    Dim bEnabled As Boolean = True

                    axCZKEM1.SetStrCardNumber(sCardnumber)
                    If axCZKEM1.SSR_SetUserInfo(iMachineNumber, sdwEnrollNumber, sName, sPassword, iPrivilege, bEnabled) = True Then 'upload the user's information(card number included)
                        '  MsgBox("SetUserInfo,UserID:" + sdwEnrollNumber.ToString() + " Privilege:" + iPrivilege.ToString() + " Cardnumber:" + sCardnumber + " Enabled:" + bEnabled.ToString(), MsgBoxStyle.Information, "Success")

                        dv1(i)("stat") = "OK"
                        Application.DoEvents()
                        System.Threading.Thread.Sleep(1500)

                    Else
                        axCZKEM1.GetLastError(idwErrorCode)
                        MsgBox("Operation failed,ErrorCode=" & idwErrorCode.ToString(), MsgBoxStyle.Exclamation, "Error")
                    End If


                End If
            Next

            axCZKEM1.RefreshData(iMachineNumber) 'the data in the device should be refreshed
            axCZKEM1.EnableDevice(iMachineNumber, True)

            close_wait()

            MsgBox("Data berhasil diupload..")

        Catch ex As Exception
            close_wait()
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        End Try

    End Sub

    Private Sub fdownloadlog_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        cbopsi.SelectedIndex = 0

        loadip()
        PanelControl1.Visible = True
        PanelControl1.Dock = DockStyle.Fill
        XtraTabControl1.Visible = False

    End Sub

    Private Sub XtraTabControl1_Click(sender As System.Object, e As System.EventArgs) Handles XtraTabControl1.Click
        '   tsref_Click(sender, Nothing)
    End Sub

    Private Sub SimpleButton1_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton1.Click

        showloaguser()

        If bIsConnected = False Then
            MsgBox("Please connect the device first", MsgBoxStyle.Exclamation, "Error")
            Return
        End If

        Dim idwEnrollNumber As Integer
        Dim sName As String = ""
        Dim sPassword As String = ""
        Dim iPrivilege As Integer
        Dim bEnabled As Boolean = False
        Dim sCardnumber As String = ""

        open_wait()

        Try

            axCZKEM1.EnableDevice(iMachineNumber, False) 'disable the device
            axCZKEM1.ReadAllUserID(iMachineNumber) 'read all the user information to the memory

            While axCZKEM1.SSR_GetAllUserInfo(iMachineNumber, idwEnrollNumber, sName, sPassword, iPrivilege, bEnabled) = True 'get user information from memory
                If axCZKEM1.GetStrCardNumber(sCardnumber) = True Then 'get the card number from the memory

                    Dim orow As DataRowView = dvload.AddNew
                    orow("userid") = idwEnrollNumber.ToString()
                    orow("nama") = sName
                    orow("priv") = iPrivilege.ToString()

                    dvload.EndInit()


                    Application.DoEvents()
                    '  System.Threading.Thread.Sleep(100)

                    'Dim list As New ListViewItem
                    'list.Text = idwEnrollNumber.ToString()
                    'list.SubItems.Add(sName)
                    'list.SubItems.Add(sCardnumber)
                    'list.SubItems.Add(iPrivilege.ToString())
                    'list.SubItems.Add(sPassword)
                    'If bEnabled = True Then
                    '    list.SubItems.Add("true")
                    'Else
                    '    list.SubItems.Add("false")
                    'End If

                End If
            End While

            axCZKEM1.EnableDevice(iMachineNumber, True) 'enable the device

            close_wait()

        Catch ex As Exception
            close_wait()
            MsgBox(ex.ToString)
        End Try

    End Sub

End Class