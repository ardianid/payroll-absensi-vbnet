Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Imports DevExpress.XtraReports.UI

Public Class fpr_karybydepart2

    Private Sub load_print()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn()


            Dim ds As DataSet = New dskary_bydepart2
            ds = Clsmy.GetDataSet("select depart,COUNT(nip) as jml from ms_karyawan where aktif=1 group by depart", cn)

            Dim ops As New System.Drawing.Printing.PrinterSettings
            Dim rrekap As New rkary_bydepart2() With {.DataSource = ds.Tables(0)}
            '  rrekap.xperiode.Text = String.Format("Periode : {0} s.d {1}", convert_date_to_ind(tgl1), convert_date_to_ind(tgl2))
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