Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Imports DevExpress.XtraReports.UI

Public Class fkurangabsen11

    Public sql As String
    Public sql2 As String
    Public periode As String

    Private Sub load_print()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            Dim ds As DataSet = New ds_krangabsen
            ds = Clsmy.GetDataSet(sql, cn)

            Dim ds2 As DataSet = New ds_mnualabsen
            ds2 = Clsmy.GetDataSet(sql2, cn)

            Dim ops As New System.Drawing.Printing.PrinterSettings
            Dim rrekap As New r_krangabsen() With {.DataSource = ds.Tables(0)}

            rrekap.XrSubreport1.ReportSource = New r_absenmnual
            rrekap.XrSubreport1.ReportSource.DataSource = ds2.Tables(0)
            rrekap.XrSubreport1.ReportSource.DataMember = rrekap.XrSubreport1.ReportSource.DataMember

            rrekap.xrperiode.Text = periode
            rrekap.xuser.Text = String.Format("{0} {1}", userprog, DateTime.Now)
            rrekap.DataMember = rrekap.DataMember
            rrekap.PrinterName = ops.PrinterName
            rrekap.CreateDocument(True)

            PrintControl1.PrintingSystem = rrekap.PrintingSystem

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

    Private Sub fpr_gajiborongan_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        load_print()
    End Sub

End Class