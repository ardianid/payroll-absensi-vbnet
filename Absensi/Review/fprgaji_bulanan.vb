Imports System.Data
Imports System.Data.OleDb
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports Absensi.Clsmy

Public Class fprgaji_bulanan

    Public sql As String

    Dim crReportDocument As rgaji_bulanan


    Private Sub loaddata()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As New DataSet
            ds = GetDataSet(sql, cn)

            Dim ds1 As New dsgaji_bulanan
            ds1.Clear()
            ds1.Tables(0).Merge(ds.Tables(0))

            crReportDocument = New rgaji_bulanan()

            crReportDocument.SetDataSource(ds1)

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

    Private Sub fprgaji_bulanan_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        close_wait()
        loaddata()
    End Sub
End Class