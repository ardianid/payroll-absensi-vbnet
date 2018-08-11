Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Public Class fpr_gajiborongan0

    Private Sub load_golongan()

        Dim cn As OleDbConnection = Nothing
        Dim dtgol As DataTable

        Const sql As String = "select kode,nama from ms_golongan where jenisgaji='Mingguan' and harian=0 and saktif=1"

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

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub fpr_gajiborongan0_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        ttgl1.Focus()
    End Sub


    Private Sub fpr_gajiborongan0_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ttgl1.EditValue = DateValue(Now)
        ttgl2.EditValue = DateValue(Now)

        load_golongan()

    End Sub


    Private Sub btload_Click(sender As System.Object, e As System.EventArgs) Handles btload.Click

        Cursor = Cursors.WaitCursor

        Dim fs As New fpr_gajiborongan With {.WindowState = FormWindowState.Maximized, .tgl1 = ttgl1.EditValue, .tgl2 = ttgl2.EditValue, .kdgol = cbgolongan.EditValue}
        fs.ShowDialog()

        Cursor = Cursors.Default

    End Sub
End Class