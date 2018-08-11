Imports System.Data
Imports System.Data.OleDb

Imports DevExpress.XtraReports.UI
Public Class frp_karyawanbaru2

    Public sql As String
    Public tgl As String

    Private Sub load_print()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            Dim ds As DataSet = New ds_karyawanbaru
            ds = Clsmy.GetDataSet(sql, cn)

            Dim ops As New System.Drawing.Printing.PrinterSettings
            Dim rrekap As New r_karyawanbaru() With {.DataSource = ds.Tables(0)}
            'rrekap.xuser.Text = String.Format("User : {0} | Tgl : {1}", userprog, Date.Now)
            rrekap.xperiode.Text = tgl
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

    Private Sub frp_karyawanbaru2_Load(sender As Object, e As EventArgs) Handles Me.Load
        load_print()
    End Sub

End Class