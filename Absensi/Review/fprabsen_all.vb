Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraEditors


Public Class fprabsen_all

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

        Const sql As String = "select nip,nama from ms_karyawan where aktif=1"

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

            sql = "select b.nip,b.nama,c.nama as golongan,a.tanggal,a.jammasuk,a.jampulang," & _
            "case when a.stat='HADIR' then 1 else 0 end as xhadir," & _
            "case when a.stat='IZIN' then 1 else 0 end as xizin," & _
            "case when a.stat='SAKIT' then 1 else 0 end as xsakit," & _
            "case when a.stat='CUTI' then 1 else 0 end as xcuti," & _
            "case when a.stat='LAIN-LAIN' then 1 else 0 end as xlain2," & _
            "case when a.stat='ALPHA' or a.stat='ALPHA ABSEN' then 1 else 0 end as xalpha," & _
            "a.stelat, a.spulangcpat, a.jmltelat, a.jamkerja, a.jamlembur " & _
            "from tr_hadir a inner join ms_karyawan b on a.nip=b.nip inner join ms_golongan c on a.kdgol=c.kode " & _
            "where b.aktif = 1"

            If Not cbgolongan.EditValue = "All" Then
                sql = String.Format("{0} and c.kode='{1}'", sql, cbgolongan.EditValue)
            End If

            If Not cbpeg.EditValue = "All" Then
                sql = String.Format("{0} and b.nip='{1}'", sql, cbpeg.EditValue)
            End If

            sql = String.Format("{0} and a.tanggal>='{1}' and a.tanggal<='{2}'", sql, convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue))

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

    Private Sub btexex2007_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btoexcel2007.ItemClick

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

    Private Sub btexex_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btoexcel.ItemClick

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

    Private Sub btexhtml_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btohtml.ItemClick
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


    Private Sub btexrtf_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btortf.ItemClick
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

    Private Sub btexpdf_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btopdf.ItemClick
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

    Private Sub btextext_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btotext.ItemClick
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


End Class