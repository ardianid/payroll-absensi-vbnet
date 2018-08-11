Imports Absensi.Clsmy
Imports System.Data
Imports System.Data.OleDb
Imports DevExpress.XtraBars

Public Class login

    Private Sub login_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        tuser.Focus()
    End Sub

    Private Sub btbatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btbatal.Click
        Application.Exit()
    End Sub

    Private Sub btmasuk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btmasuk.Click

        If tuser.Text = "" Then
            MsgBox("Nama user harus diisi", MsgBoxStyle.Information, "Informasi")
            tuser.Focus()
            Exit Sub
        End If

        If tpwd.Text = "" Then
            MsgBox("Password harus diisi", MsgBoxStyle.Information, "Informasi")
            tpwd.Focus()
            Exit Sub
        End If

        open_wait()

        Dim cn As OleDbConnection = New OleDbConnection
        userprog = tuser.Text.Trim
        pwd = tpwd.Text.Trim

        Try


            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select nonaktif,jenisuser,sgapok,sgapok_jamsos,stunj_jab,stunj_hdr,stunj_transport,stunj_makan,stamb_makan,fform,flap,snonkary from ms_usersys where namauser='{0}' and pwd=HASHBYTES('md5','{1}')", tuser.Text.Trim, tpwd.Text.Trim)
            Dim comd = New OleDbCommand(sql, cn)
            Dim dre As OleDbDataReader = comd.ExecuteReader

            If dre.Read Then

                If dre(0).ToString = "1" Then
                    close_wait()
                    MsgBox("User anda sudah tidak aktif, hubungi admin", vbOKOnly + vbInformation, "Informasi")
                Else

                    xjenisuser = dre("jenisuser").ToString

                    xgapok = dre("sgapok").ToString
                    xgapok_jmsos = dre("sgapok_jamsos").ToString
                    xtunj_jab = dre("stunj_jab").ToString
                    xtunj_hdr = dre("stunj_hdr").ToString
                    xtunj_trans = dre("stunj_transport").ToString
                    xtunj_makan = dre("stunj_makan").ToString
                    xtamb_makan = dre("stamb_makan").ToString
                    xform = dre("fform").ToString
                    xlap = dre("flap").ToString

                    xnonkary = dre("snonkary").ToString

                    setmenu()
                    setmenu2()
                    futama.tuserakt.Caption = "User : " & userprog.Trim

                    Me.Close()

                    Dim sqlcek_tgl As String = String.Format("select COUNT(*) as jml from ms_kalender where YEAR(tanggal)=2015", Year(Date.Now))
                    Dim cmdcek_tgl As OleDbCommand = New OleDbCommand(sqlcek_tgl, cn)
                    Dim drdcek_tgl As OleDbDataReader = cmdcek_tgl.ExecuteReader

                    Dim jmlhari As Integer = 0
                    If drdcek_tgl.Read Then
                        If IsNumeric(drdcek_tgl(0).ToString) Then
                            jmlhari = Integer.Parse(drdcek_tgl(0).ToString)
                        End If
                    End If
                    drdcek_tgl.Close()

                    If jmlhari = 0 Then
                        close_wait()
                        MsgBox("Set kalender kerja dulu..", vbInformation + MsgBoxStyle.Information, "Informasi")
                    End If


                End If

            Else
                close_wait()
                MsgBox("User/Password tidak ditemukan...", vbOKOnly + vbInformation, "Informasi")
                tuser.Focus()
            End If

            close_wait()

        Catch ex As Exception

            close_wait()

            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Informasi")

        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                    cn.Dispose()
                End If
            End If

        End Try


    End Sub

    Public Sub setmenu()

        Dim ds As DataSet
        Dim sql As String = String.Format("select a.kodemenu,a.t_active,a.t_add,a.t_edit,a.t_del,a.t_lap,b.namaform,b.submenu2,b.submenu1 from ms_usersys2 a inner join ms_menu b on a.kodemenu=b.kodemenu where a.namauser='{0}'", userprog.Trim)


        Dim cn2 As New OleDbConnection


        Dim fm1 As Integer = 0
        Dim fm2 As Integer = 0
        Dim fm3 As Integer = 0
        Dim fm4 As Integer = 0
        Dim fm5 As Integer = 0
        Dim fm6 As Integer = 0
        Dim fm7 As Integer = 0

        Try

            cn2 = Clsmy.open_conn

            ds = New DataSet
            ds = Clsmy.GetDataSet(sql, cn2)

            dtmenu = New DataTable
            dtmenu.Clear()

            dtmenu = ds.Tables(0)

            If ds.Tables(0).Rows.Count > 0 Then


                Dim i As Integer = 0
                For i = 0 To futama.RibbonControl.Items.Count - 1
                    If TypeOf futama.RibbonControl.Items(i) Is BarButtonItem Then

                        Dim btnbar As BarButtonItem = CType(futama.RibbonControl.Items(i), BarButtonItem)

                        If btnbar.Name.ToString.Trim.Length > 0 Then

                            If btnbar.Name.Substring(0, 2).ToUpper = "NO" Or btnbar.Name.Substring(0, 1).ToUpper = "X" Or btnbar.Name.Substring(0, 3).ToUpper = "LAP" Then
                                btnbar.Enabled = True
                            Else


                                Dim rows() As DataRow = dtmenu.Select(String.Format("kodemenu='{0}'", btnbar.Name.Trim))
                                Dim i2 As Integer = 0
                                For i2 = 0 To rows.GetUpperBound(0)

                                    If Convert.ToInt16(rows(i2)("t_active")) = 1 Then

                                        btnbar.Enabled = True

                                        If rows(i2)("submenu2").ToString.Equals("RibbonPageGroup1") Then
                                            fm1 = 1
                                        ElseIf rows(i2)("submenu2").ToString.Equals("RibbonPageGroup2") Then
                                            fm2 = 1
                                        ElseIf rows(i2)("submenu2").ToString.Equals("RibbonPageGroup3") Then
                                            fm3 = 1
                                        ElseIf rows(i2)("submenu2").ToString.Equals("RibbonPageGroup4") Then
                                            fm4 = 1
                                        ElseIf rows(i2)("submenu2").ToString.Equals("RibbonPageGroup5") Then
                                            fm5 = 1
                                            'ElseIf rows(i2)("submenu2").ToString.Equals("RibbonPageGroup6") Then
                                            '    fm6 = 1
                                        ElseIf rows(i2)("submenu2").ToString.Equals("RibbonPageGroup7") Then
                                            fm7 = 1
                                        End If

                                    Else
                                        btnbar.Visibility = BarItemVisibility.Never
                                        btnbar.Enabled = False

                                    End If

                                Next


                                

                            End If

                        End If


                    End If
                Next


                'If fm1 = 1 Then
                futama.RibbonPageGroup1.Visible = True
                'Else
                '    futama.RibbonPageGroup1.Visible = False
                'End If

                If fm2 = 1 Then
                    futama.RibbonPageGroup2.Visible = True
                Else
                    futama.RibbonPageGroup2.Visible = False
                End If

                If fm3 = 1 Then
                    futama.RibbonPageGroup3.Visible = True
                Else
                    futama.RibbonPageGroup3.Visible = False
                End If

                If fm4 = 1 Then
                    futama.RibbonPageGroup4.Visible = True
                Else
                    futama.RibbonPageGroup4.Visible = False
                End If

                If fm5 = 1 Then
                    futama.RibbonPageGroup5.Visible = True
                Else
                    futama.RibbonPageGroup5.Visible = False
                End If

                If fm7 = 1 Then
                    futama.RibbonPageGroup7.Visible = True
                Else
                    futama.RibbonPageGroup7.Visible = False
                End If

                futama.RibbonControl.Minimized = False

                futama.RibbonPageGroup6.Visible = True


                If Not cn2 Is Nothing Then
                    If cn2.State = ConnectionState.Open Then
                        cn2.Close()
                        cn2.Dispose()
                    End If
                End If


            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Informasi")
        Finally
            If Not cn2 Is Nothing Then
                If cn2.State = ConnectionState.Open Then
                    cn2.Close()
                    cn2.Dispose()
                End If
            End If
        End Try

    End Sub

    Public Sub setmenu2()

        Dim ds As DataSet
        Dim sql As String = String.Format("select a.kodemenu,a.t_lap,b.namaform from ms_usersys3 a inner join ms_menu b on a.kodemenu=b.kodemenu where a.namauser='{0}'", userprog.Trim)


        Dim cn2 As New OleDbConnection

        Try

            cn2 = Clsmy.open_conn

            ds = New DataSet
            ds = Clsmy.GetDataSet(sql, cn2)

            dtmenu2 = New DataTable
            dtmenu2.Clear()

            dtmenu2 = ds.Tables(0)

                If Not cn2 Is Nothing Then
                    If cn2.State = ConnectionState.Open Then
                        cn2.Close()
                        cn2.Dispose()
                    End If
                End If


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Informasi")
        Finally
            If Not cn2 Is Nothing Then
                If cn2.State = ConnectionState.Open Then
                    cn2.Close()
                    cn2.Dispose()
                End If
            End If
        End Try

    End Sub

    Private Sub tuser_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tuser.KeyDown
        If e.KeyCode = 13 Then
            tpwd.Focus()
        End If
    End Sub

    Private Sub tpwd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tpwd.KeyDown
        If e.KeyCode = 13 Then
            btmasuk.PerformClick()
        End If
    End Sub



End Class