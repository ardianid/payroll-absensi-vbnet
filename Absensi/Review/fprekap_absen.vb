Imports System.Data
Imports System.Data.OleDb
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports Absensi.Clsmy

Public Class fprekap_absen

    Public sql As String
    Public tgl1 As String
    Public tgl2 As String

    Dim crReportDocument As rrekap_absen

    Private Sub loaddata()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As New DataSet
            ds = GetDataSet(sql, cn)

            Dim ds1 As New dsrekap_absen
            ds1.Clear()
            ds1.Tables(0).Merge(ds.Tables(0))

            crReportDocument = New rrekap_absen

            crReportDocument.SetDataSource(ds1)

            Dim crParameterFieldDefinitions As ParameterFieldDefinitions
            Dim crParameterFieldDefinition As ParameterFieldDefinition
            Dim crParameterValues As New ParameterValues
            Dim crParameterDiscreteValue As New ParameterDiscreteValue

            Dim crParameterFieldDefinitions2 As ParameterFieldDefinitions
            Dim crParameterFieldDefinition2 As ParameterFieldDefinition
            Dim crParameterValues2 As New ParameterValues
            Dim crParameterDiscreteValue2 As New ParameterDiscreteValue

            Dim crParameterFieldDefinitions3 As ParameterFieldDefinitions
            Dim crParameterFieldDefinition3 As ParameterFieldDefinition
            Dim crParameterValues3 As New ParameterValues
            Dim crParameterDiscreteValue3 As New ParameterDiscreteValue

            crParameterDiscreteValue.Value = "User Print : " & userprog & " | Tgl : " & Date.Now
            crParameterFieldDefinitions = crReportDocument.DataDefinition.ParameterFields
            crParameterFieldDefinition = crParameterFieldDefinitions.Item("muser")
            crParameterValues = crParameterFieldDefinition.CurrentValues

            crParameterValues.Clear()
            crParameterValues.Add(crParameterDiscreteValue)
            crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

            crParameterDiscreteValue2.Value = tgl1
            crParameterFieldDefinitions2 = crReportDocument.DataDefinition.ParameterFields
            crParameterFieldDefinition2 = crParameterFieldDefinitions2.Item("tgl1")
            crParameterValues2 = crParameterFieldDefinition2.CurrentValues

            crParameterValues2.Clear()
            crParameterValues2.Add(crParameterDiscreteValue2)
            crParameterFieldDefinition2.ApplyCurrentValues(crParameterValues2)

            crParameterDiscreteValue3.Value = tgl2
            crParameterFieldDefinitions3 = crReportDocument.DataDefinition.ParameterFields
            crParameterFieldDefinition3 = crParameterFieldDefinitions3.Item("tgl2")
            crParameterValues3 = crParameterFieldDefinition3.CurrentValues

            crParameterValues3.Clear()
            crParameterValues3.Add(crParameterDiscreteValue3)
            crParameterFieldDefinition3.ApplyCurrentValues(crParameterValues3)

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

    Private Sub fprrekap_gaji_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        close_wait()
        loaddata()
    End Sub

End Class