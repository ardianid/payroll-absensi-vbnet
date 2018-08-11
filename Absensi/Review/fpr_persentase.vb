Imports System.Data
Imports System.Data.OleDb
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports Absensi.Clsmy

Public Class fpr_persentase

    Dim crReportDocument As rr_persentase

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

    Private Sub loaddata()

        Dim sql As String = ""
        Dim cn As OleDbConnection = Nothing

        Try

            sql = String.Format("select a.tanggal,a.stat as kehadiran from tr_hadir a inner join ms_karyawan b on a.nip=b.nip where tanggal>='{0}' and tanggal<='{1}' and not(a.stat in ('LAIN-LAIN','OFF'))", convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue))

            If Not cbgolongan.EditValue = "All" Then
                sql = String.Format("{0} and b.kdgol='{1}'", sql, cbgolongan.EditValue)
            End If

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As New DataSet
            ds = GetDataSet(sql, cn)

            Dim ds1 As New dsrekap_absen
            ds1.Clear()
            ds1.Tables(0).Merge(ds.Tables(0))

            crReportDocument = New rr_persentase
            crReportDocument.SetDataSource(ds1)

            Dim crParameterFieldDefinitions As ParameterFieldDefinitions
            Dim crParameterFieldDefinition As ParameterFieldDefinition
            Dim crParameterValues As New ParameterValues
            Dim crParameterDiscreteValue As New ParameterDiscreteValue

            Dim crParameterFieldDefinitions2 As ParameterFieldDefinitions
            Dim crParameterFieldDefinition2 As ParameterFieldDefinition
            Dim crParameterValues2 As New ParameterValues
            Dim crParameterDiscreteValue2 As New ParameterDiscreteValue

            crParameterDiscreteValue.Value = String.Format("{0} s.d {1}", ttgl1.Text, ttgl2.Text)
            crParameterFieldDefinitions = crReportDocument.DataDefinition.ParameterFields
            crParameterFieldDefinition = crParameterFieldDefinitions.Item("xtanggal1")
            crParameterValues = crParameterFieldDefinition.CurrentValues

            crParameterValues.Clear()
            crParameterValues.Add(crParameterDiscreteValue)
            crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

            crParameterDiscreteValue2.Value = cbgolongan.Text
            crParameterFieldDefinitions2 = crReportDocument.DataDefinition.ParameterFields
            crParameterFieldDefinition2 = crParameterFieldDefinitions2.Item("xtanggal2")
            crParameterValues2 = crParameterFieldDefinition2.CurrentValues

            crParameterValues2.Clear()
            crParameterValues2.Add(crParameterDiscreteValue2)
            crParameterFieldDefinition2.ApplyCurrentValues(crParameterValues2)

            CrystalReportViewer1.ReportSource = crReportDocument
            CrystalReportViewer1.Refresh()

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

    End Sub

    Private Sub fpr_persentase_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        ttgl1.Focus()
    End Sub

    Private Sub fpr_persentase_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        ttgl1.EditValue = Date.Now
        ttgl2.EditValue = Date.Now

        load_golongan()
    End Sub

    Private Sub btload_Click(sender As System.Object, e As System.EventArgs) Handles btload.Click
        loaddata()
    End Sub

End Class