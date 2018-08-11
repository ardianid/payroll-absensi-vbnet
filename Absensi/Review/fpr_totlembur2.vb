Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Imports DevExpress.XtraReports.UI

Public Class fpr_totlembur2

    Public tgl1 As String
    Public tgl2 As String
    Public sql As String
    Public jenislap As Integer = 0


    Private Sub load1(ByVal cn As OleDbConnection)

        Dim ds As DataSet = New dstotlembur_perdepart
        ds = Clsmy.GetDataSet(sql, cn)

        Dim ops As New System.Drawing.Printing.PrinterSettings
        Dim rrekap As New rtotlembur_perdepart() With {.DataSource = ds.Tables(0)}
        rrekap.xperiode.Text = String.Format("Periode : {0} s.d {1}", convert_date_to_ind(tgl1), convert_date_to_ind(tgl2))
        rrekap.xuser.Text = String.Format("{0} {1}", userprog, DateTime.Now)
        rrekap.DataMember = rrekap.DataMember
        rrekap.PrinterName = ops.PrinterName
        rrekap.CreateDocument(True)

        PrintControl1.PrintingSystem = rrekap.PrintingSystem

    End Sub

    Private Sub load2(ByVal cn As OleDbConnection)

        Dim ds As DataSet = New dstotlembur_perdepart2
        ds = Clsmy.GetDataSet(sql, cn)

        Dim ops As New System.Drawing.Printing.PrinterSettings
        Dim rrekap As New rtotlembur_perdepart2() With {.DataSource = ds.Tables(0)}
        rrekap.xperiode.Text = String.Format("Periode : {0} s.d {1}", convert_date_to_ind(tgl1), convert_date_to_ind(tgl2))
        rrekap.xuser.Text = String.Format("{0} {1}", userprog, DateTime.Now)
        rrekap.DataMember = rrekap.DataMember
        rrekap.PrinterName = ops.PrinterName
        rrekap.CreateDocument(True)

        PrintControl1.PrintingSystem = rrekap.PrintingSystem

    End Sub

    Private Sub load_print()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            If jenislap = 1 Then
                load1(cn)
            Else
                load2(cn)
            End If

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