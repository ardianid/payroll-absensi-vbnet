Imports DevExpress.XtraBars
Imports System.Data.OleDb
Imports Absensi.Clsmy

Public Class futama

    Private Sub disable_bar()

        Dim i As Integer = 0
        For i = 0 To RibbonControl.Items.Count - 1
            If TypeOf RibbonControl.Items(i) Is BarButtonItem Then

                Dim btn As BarButtonItem = CType(RibbonControl.Items(i), BarButtonItem)

                Dim xA As String = btn.Name.ToString

                If xA.Length > 0 Then
                    btn.Visibility = BarItemVisibility.Always
                    btn.Enabled = False
                End If


            End If
        Next

        RibbonControl.Minimized = True

        RibbonPageGroup1.Visible = False
        RibbonPageGroup2.Visible = False
        RibbonPageGroup3.Visible = False
        RibbonPageGroup4.Visible = False
        RibbonPageGroup5.Visible = False
        RibbonPageGroup6.Visible = False
        RibbonPageGroup7.Visible = False

    End Sub

    Public Sub LoadOtherForm(ByVal fname As Form)

        open_wait()
        Cursor = Cursors.WaitCursor

       
        fname.MdiParent = Me
        fname.Show()


        Cursor = Cursors.Default
        close_wait()

    End Sub


    Private Sub btnuser_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnuser.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        fuser.MdiParent = Me
        fuser.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub futama_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Application.Exit()
    End Sub


    Private Sub futama_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        disable_bar()


        Try
            Dim cn As OleDbConnection

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            cn.Close()
            cn.Dispose()

            LoadLOgin()

        Catch ex As Exception

            fsettdbase.MdiParent = Me
            fsettdbase.Show()

            'If ex.Message.ToString.Equals("Object reference not set to an instance of an object.") Then
            '    MsgBox("ok")
            'End If

        End Try


        

    End Sub

    Public Sub LoadLOgin()
        Dim fmlogin As New login With {.MdiParent = Me, .WindowState = FormWindowState.Maximized}
        fmlogin.Show()
    End Sub

    Private Sub NO_logof_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles NO_logof.ItemClick

        For Each f As Form In Me.MdiChildren
            f.Close()
        Next

        disable_bar()

        userprog = ""
        pwd = ""

        tuserakt.Caption = "User : "

        Dim fmlogin As New login With {.MdiParent = Me, .WindowState = FormWindowState.Maximized}
        fmlogin.Show()

    End Sub


    Private Sub btdepart_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btdepart.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        fdepart.MdiParent = Me
        fdepart.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btgol_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btgol.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        fgol_otomat.MdiParent = Me
        fgol_otomat.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btkary_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btkary.ItemClick
        open_wait()
        Cursor = Cursors.WaitCursor

        fkaryawan.MdiParent = Me
        fkaryawan.Show()

        Cursor = Cursors.Default
        close_wait()
    End Sub

    Private Sub btsched_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btsched.ItemClick
        open_wait()
        Cursor = Cursors.WaitCursor

        fschedule.MdiParent = Me
        fschedule.Show()

        Cursor = Cursors.Default
        close_wait()
    End Sub

    Private Sub btutilmesin_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btutilmesin.ItemClick
        ' open_wait()
        Cursor = Cursors.WaitCursor

        '  futil_mesin.StartPosition = FormStartPosition.CenterParent
        futil_mesin.ShowDialog(Me)

        Cursor = Cursors.Default
        ' close_wait()
    End Sub

    Private Sub btdown_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btdown.ItemClick
        ' open_wait()
        Cursor = Cursors.WaitCursor

        fdownloadlog.StartPosition = FormStartPosition.CenterScreen
        fdownloadlog.Show()

        Cursor = Cursors.Default
        ' close_wait()
    End Sub

    Private Sub btjamkerja_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btjamkerja.ItemClick
        open_wait()
        Cursor = Cursors.WaitCursor

        fjamkerja.MdiParent = Me
        fjamkerja.Show()

        Cursor = Cursors.Default
        close_wait()
    End Sub

 
    Private Sub btstatabs_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btstatabs.ItemClick
        open_wait()
        Cursor = Cursors.WaitCursor

        fstathadir.MdiParent = Me
        fstathadir.Show()

        Cursor = Cursors.Default
        close_wait()
    End Sub

    Private Sub btkalkpresen_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btkalkpresen.ItemClick
        open_wait()
        Cursor = Cursors.WaitCursor

        fkalk_absen_n.MdiParent = Me
        fkalk_absen_n.Show()

        Cursor = Cursors.Default
        close_wait()
    End Sub

    Private Sub btgajibulan_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btgajibulan.ItemClick
        open_wait()
        Cursor = Cursors.WaitCursor

        'fgaji_bulanan.MdiParent = Me
        'fgaji_bulanan.Show()

        Cursor = Cursors.Default
        close_wait()
    End Sub

    Private Sub btgajiminggu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btgajiminggu.ItemClick
        open_wait()
        Cursor = Cursors.WaitCursor

        'fgaji_mingguan.MdiParent = Me
        'fgaji_mingguan.Show()

        Cursor = Cursors.Default
        close_wait()
    End Sub

    Private Sub btmanabs_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btmanabs.ItemClick
        open_wait()
        Cursor = Cursors.WaitCursor

        fabsenmanual.MdiParent = Me
        fabsenmanual.Show()

        Cursor = Cursors.Default
        close_wait()
    End Sub

    Private Sub btjamkerjalain_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btjamkerjalain.ItemClick
        open_wait()
        Cursor = Cursors.WaitCursor

        'fjamkerja_oby_gol.MdiParent = Me
        'fjamkerja_oby_gol.Show()

        Cursor = Cursors.Default
        close_wait()
    End Sub

    Private Sub btkalkjamsos_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btkalkjamsos.ItemClick
        '     open_wait()
        Cursor = Cursors.WaitCursor

        ' fjamkerja_oby_gol.MdiParent = Me
        'fkalk_jamsostek.ShowDialog(Me)

        Cursor = Cursors.Default
        '   close_wait()
    End Sub

    Private Sub btreportall_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btreportall.ItemClick
        open_wait()
        Cursor = Cursors.WaitCursor

        fload_reports.MdiParent = Me
        fload_reports.Show()

        Cursor = Cursors.Default
        close_wait()
    End Sub

    Private Sub btmachuser_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)

        Cursor = Cursors.WaitCursor

        fuserabsen.StartPosition = FormStartPosition.CenterParent
        fuserabsen.ShowDialog(Me)

        Cursor = Cursors.Default

    End Sub

    Private Sub btgol_per_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)

        open_wait()
        Cursor = Cursors.WaitCursor

        'fgol_per.MdiParent = Me
        'fgol_per.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btkurabsen_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btkurabsen.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        ftinout.MdiParent = Me
        ftinout.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btnaik_gj_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnaik_gj.ItemClick

        'open_wait()
        Cursor = Cursors.WaitCursor

        fnaik_gaji.StartPosition = FormStartPosition.CenterScreen
        fnaik_gaji.ShowDialog()

        Cursor = Cursors.Default
        ' close_wait()

    End Sub

    Private Sub btbon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btbon.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        fbon.MdiParent = Me
        fbon.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btangsur_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btangsur.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        fbayar.MdiParent = Me
        fbayar.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub NO_ch_pwd_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles NO_ch_pwd.ItemClick
        frubah_pwd.ShowDialog(Me)
    End Sub

    Private Sub bt_shift_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bt_shift.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        fshift.MdiParent = Me
        fshift.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btpotcuti_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btpotcuti.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        fpot_cuti.MdiParent = Me
        fpot_cuti.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btndow_att_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btndow_att.ItemClick

        Cursor = Cursors.WaitCursor

        fdownload_fromatt.StartPosition = FormStartPosition.CenterScreen
        fdownload_fromatt.Show()

        Cursor = Cursors.Default

    End Sub

End Class