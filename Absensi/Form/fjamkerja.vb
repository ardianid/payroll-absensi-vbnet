Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraEditors

Public Class fjamkerja

    Private bs1 As BindingSource
    Private ds As DataSet
    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private Sub opendata()

        Const sql As String = "select * from ms_jamkerja"
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

    Private Sub hapus()

        Dim sql As String = String.Format("delete from ms_jamkerja where kode='{0}'", dv1(Me.BindingContext(bs1).Position)("kode").ToString)

        Dim cn As OleDbConnection = Nothing
        Dim comd As OleDbCommand = Nothing

        Try

            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction

            comd = New OleDbCommand(sql, cn, sqltrans)
            comd.ExecuteNonQuery()

            Clsmy.InsertToLog(cn, "btjamkerja", 0, 0, 1, 0, dv1(Me.BindingContext(bs1).Position)("kode").ToString, dv1(Me.BindingContext(bs1).Position)("nama").ToString, sqltrans)

            sqltrans.Commit()

            close_wait()

            MsgBox("Data telah dihapus...", vbOKOnly + vbInformation, "Informasi")

            opendata()

        Catch ex As Exception
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

    Private Sub Get_Aksesform()

        Dim rows() As DataRow = dtmenu.Select(String.Format("NAMAFORM='{0}'", Me.Name.ToUpper.ToString))

        If Convert.ToInt16(rows(0)("t_add")) = 1 Then
            tsadd.Enabled = True
        Else
            tsadd.Enabled = False
        End If

        If Convert.ToInt16(rows(0)("t_edit")) = 1 Then
            tsedit.Enabled = True
        Else
            tsedit.Enabled = False
        End If

        If Convert.ToInt16(rows(0)("t_del")) = 1 Then
            tsdel.Enabled = True
        Else
            tsdel.Enabled = False
        End If

        'If Convert.ToInt16(rows(0)("t_lap")) = 1 Then

        'Else

        'End If

    End Sub

    Private Sub fjamkerja_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        tcbofind.SelectedIndex = 0

        Get_Aksesform()

        opendata()
    End Sub

    Private Sub tsref_Click(sender As System.Object, e As System.EventArgs) Handles tsref.Click
        tsfind.Text = ""
        opendata()
    End Sub

    Private Sub tsdel_Click(sender As System.Object, e As System.EventArgs) Handles tsdel.Click

        If dv1.Count < 1 Then
            Exit Sub
        End If

        If MsgBox("Yakin akan dihapus ?", vbYesNo + vbQuestion, "Konfirmasi") = vbNo Then
            Exit Sub
        End If

        hapus()

    End Sub


    Private Sub tsedit_Click(sender As System.Object, e As System.EventArgs) Handles tsedit.Click

        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If

        Using fjamker2 As New fjamkerja2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position}
            fjamker2.ShowDialog()
        End Using

    End Sub

    Private Sub tsadd_Click(sender As System.Object, e As System.EventArgs) Handles tsadd.Click
        Using fjamker2 As New fjamkerja2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = True, .position = 0}
            fjamker2.ShowDialog()
        End Using
    End Sub

    Private Sub tcbofind_Click(sender As System.Object, e As System.EventArgs) Handles tcbofind.Click

        

    End Sub

    Private Function ShowSaveFileDialog(ByVal title As String, ByVal filter As String) As String
        Dim dlg As New SaveFileDialog()
        Dim name As String = Application.ProductName
        Dim n As Integer = name.LastIndexOf(".") + 1
        If n > 0 Then
            name = name.Substring(n, name.Length - n)
        End If
        dlg.Title = "Export To " & title
        dlg.FileName = name
        dlg.Filter = filter
        If dlg.ShowDialog() = DialogResult.OK Then
            Return dlg.FileName
        End If
        Return ""
    End Function

    Private Sub OpenFile(ByVal fileName As String)
        If XtraMessageBox.Show("Anda ingin membuka file ?", "Export To...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                Dim process As New System.Diagnostics.Process()
                process.StartInfo.FileName = fileName
                process.StartInfo.Verb = "Open"
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal
                process.Start()
            Catch
                DevExpress.XtraEditors.XtraMessageBox.Show(Me, "Data tidak ditemukan", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
        '   progressBarControl1.Position = 0
    End Sub

    Private Sub Excel2007ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles Excel2007ToolStripMenuItem.Click

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If


                Dim fileName As String = ShowSaveFileDialog("Excel 2007", "Microsoft Excel|*.xlsx")

                If fileName = String.Empty Then
                    Return
                End If

                GridView1.ExportToXlsx(fileName)
                OpenFile(fileName)

    End Sub

    Private Sub ExcelToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ExcelToolStripMenuItem.Click

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        Dim fileName As String = ShowSaveFileDialog("Excel", "Microsoft Excel|*.xls")

        If fileName = String.Empty Then
            Return
        End If

        GridView1.ExportToXls(fileName)
        OpenFile(fileName)

    End Sub


End Class