Imports System.Data
Imports System.Data.OleDb
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports Absensi.Clsmy

Public Class fprtobank

    Public sql As String

    Dim crReportDocument As rrekaptobank

    Private Sub loaddata()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As New DataSet
            ds = GetDataSet(sql, cn)

            Dim ds1 As New dsrekaptobank
            ds1.Clear()
            ds1.Tables(0).Merge(ds.Tables(0))

            crReportDocument = New rrekaptobank

            crReportDocument.SetDataSource(ds1)

            Dim crParameterFieldDefinitions As ParameterFieldDefinitions
            Dim crParameterFieldDefinition As ParameterFieldDefinition
            Dim crParameterValues As New ParameterValues
            Dim crParameterDiscreteValue As New ParameterDiscreteValue

            crParameterDiscreteValue.Value = "User Print : " & userprog & " | Tgl : " & Date.Now
            crParameterFieldDefinitions = crReportDocument.DataDefinition.ParameterFields
            crParameterFieldDefinition = crParameterFieldDefinitions.Item("muser")
            crParameterValues = crParameterFieldDefinition.CurrentValues

            crParameterValues.Clear()
            crParameterValues.Add(crParameterDiscreteValue)
            crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

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

    Private Sub fprtobank_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        close_wait()
        loaddata()
    End Sub

End Class