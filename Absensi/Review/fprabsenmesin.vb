Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraEditors

Public Class fprabsenmesin

    Private dt As DataTable

    Private Sub load_golongan()

        Dim cn As OleDbConnection = Nothing
        Dim dtgol As DataTable

        Const sql As String = "select kode,nama from ms_golongan where saktif=1"

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            dtgol = New DataTable

            Dim ds As DataSet = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            dtgol = ds.Tables(0)

            Dim orow As DataRow = dtgol.NewRow
            orow("kode") = "All"
            orow("nama") = "All"
            dtgol.Rows.InsertAt(orow, 0)

            cbgolongan.Properties.DataSource = dtgol

            cbgolongan.ItemIndex = 0

        Catch ex As Exception

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

        End Try
    End Sub

    Private Sub load_kary()

        Dim cn As OleDbConnection = Nothing
        Dim dtgol As DataTable

        Const sql As String = "select nip,nama from ms_karyawan"

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            dtgol = New DataTable

            Dim ds As DataSet = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            dtgol = ds.Tables(0)

            Dim orow As DataRow = dtgol.NewRow
            orow("nip") = "All"
            orow("nama") = "All"
            dtgol.Rows.InsertAt(orow, 0)

            cbpeg.Properties.DataSource = dtgol

            cbpeg.ItemIndex = 0

        Catch ex As Exception

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

        End Try
    End Sub

    Private Sub loaddata()

        Dim sql As String = ""

        open_wait()

        Dim cn As OleDbConnection = Nothing

        Try

            'sql = "select b.nip,b.nama,c.nama as golongan,a.checktime as tanggal,a.checktime as jam,a.skalk from ms_inout a inner join ms_karyawan b on a.userid=b.idmesin" & _
            ' " inner join ms_golongan c on b.kdgol=c.kode"

            sql = String.Format("SELECT DISTINCT ms_karyawan.nama, CONVERT(varchar(10),checktime,103) as tanggal,SUBSTRING(CONVERT(varchar(20),CONVERT(time(0),checktime),108),1,5) as jam, ms_inout.skalk " & _
            "FROM         ms_karyawan INNER JOIN ms_inout ON ms_karyawan.idmesin = ms_inout.userid " & _
            "WHERE ms_karyawan.aktif=1 and convert(date,ms_inout.checktime) >='{0}' and convert(date,ms_inout.checktime) <='{1}'", convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue))

            If Not cbgolongan.EditValue = "All" Then
                sql = String.Format("{0} and ms_karyawan.kdgol='{1}'", sql, cbgolongan.EditValue)
            End If

            If Not cbpeg.EditValue = "All" Then
                sql = String.Format("{0} and ms_karyawan.nip='{1}'", sql, cbpeg.EditValue)
            End If

            '  sql = String.Format("{0} and convert(date,a.checktime)>='{1}' and convert(date,a.checktime)<='{2}'", sql, convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue))

            grid1.DataSource = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            dt = New DataTable
            dt = ds.Tables(0)

            grid1.DataSource = dt

            close_wait()

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

    Private Sub fprabsen_all_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        ttgl1.Focus()
    End Sub

    Private Sub fprabsen_all_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ttgl1.EditValue = Date.Now
        ttgl2.EditValue = Date.Now

        load_golongan()
        load_kary()

    End Sub

    Private Sub btload_Click(sender As System.Object, e As System.EventArgs) Handles btload.Click
        loaddata()
    End Sub

    Private Sub btexex2007_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btexcel2007.ItemClick

        If IsNothing(dt) Then
            Return
        End If

        If dt.Rows.Count <= 0 Then
            Return
        End If

        Dim fileName As String = ShowSaveFileDialog("Excel 2007", "Microsoft Excel|*.xlsx")

        If fileName = String.Empty Then
            Return
        End If

        GridView1.ExportToXlsx(fileName)
        OpenFile(fileName)

    End Sub

    Private Sub btexex_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btexcel.ItemClick

        If IsNothing(dt) Then
            Return
        End If

        If dt.Rows.Count <= 0 Then
            Return
        End If

        Dim fileName As String = ShowSaveFileDialog("Excel", "Microsoft Excel|*.xls")

        If fileName = String.Empty Then
            Return
        End If

        GridView1.ExportToXls(fileName)
        OpenFile(fileName)
    End Sub

    Private Sub btexhtml_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bthtml.ItemClick
        If IsNothing(dt) Then
            Return
        End If

        If dt.Rows.Count <= 0 Then
            Return
        End If


        Dim fileName As String = ShowSaveFileDialog("HTML", "HTML Documents|*.html")

        If fileName = String.Empty Then
            Return
        End If

        GridView1.ExportToHtml(fileName)
        OpenFile(fileName)
    End Sub

    Private Sub btexrtf_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btrtf.ItemClick
        If IsNothing(dt) Then
            Return
        End If

        If dt.Rows.Count <= 0 Then
            Return
        End If

        Dim fileName As String = ShowSaveFileDialog("RTF", "RTF Files|*.rtf")

        If fileName = String.Empty Then
            Return
        End If

        GridView1.ExportToRtf(fileName)
        OpenFile(fileName)
    End Sub

    Private Sub btexpdf_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btpdf.ItemClick
        If IsNothing(dt) Then
            Return
        End If

        If dt.Rows.Count <= 0 Then
            Return
        End If

        Dim fileName As String = ShowSaveFileDialog("PDF", "PDF Files|*.pdf")

        If fileName = String.Empty Then
            Return
        End If

        GridView1.ExportToPdf(fileName)
        OpenFile(fileName)
    End Sub

    Private Sub btextext_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bttext.ItemClick
        If IsNothing(dt) Then
            Return
        End If

        If dt.Rows.Count <= 0 Then
            Return
        End If

        Dim fileName As String = ShowSaveFileDialog("Text Files", "Text Files|*.txt")

        If fileName = String.Empty Then
            Return
        End If

        GridView1.ExportToText(fileName)
        OpenFile(fileName)
    End Sub

    Private Sub DropDownButton1_Click(sender As System.Object, e As System.EventArgs) Handles DropDownButton1.Click

    End Sub
End Class