Imports System.Data
Imports System.Data.OleDb

Imports DevExpress.XtraReports.UI
Public Class fpr_karyawan_perjenis2

    Public sql As String
    Public tgl As String

    Private Sub load_print()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            Dim ds As DataSet = New ds_kary_perjenis
            ds = Clsmy.GetDataSet("select depart,upper(jniskelamin) as jniskelamin,COUNT(nip) as jml from ms_karyawan where aktif=1 group by depart,jniskelamin", cn)

            Dim ops As New System.Drawing.Printing.PrinterSettings
            Dim rrekap As New r_karyawan_perjenis() With {.DataSource = ds.Tables(0)}
            ' rrekap.xuser.Text = String.Format("User : {0} | Tgl : {1}", userprog, Date.Now)
            'rrekap.xperiode.Text = tgl
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

    Private Sub fpr_rekapaktur_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        load_print()
    End Sub

End Class