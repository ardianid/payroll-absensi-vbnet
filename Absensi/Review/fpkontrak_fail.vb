Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Imports DevExpress.XtraReports.UI

Public Class fpkontrak_fail

    Private Sub load_data()

        Dim sql As String = "select a.nip,a.nama,a.depart,a.tgl_kon1,a.tgl_kon11,a.stat_kon1,a.tgl_kon2,a.tgl_kon22,a.stat_kon2,a.tgl_kon3,a.tgl_kon33,a.stat_kon3 " & _
        "from ms_karyawan a inner join V_Sel_Hari b on a.nip=b.nip " & _
        "where aktif = 1 And b.selisih > 0 And b.selisih <= 40"

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet = New ds_kontrakfail
            ds = Clsmy.GetDataSet(sql, cn)

            Dim ops As New System.Drawing.Printing.PrinterSettings
            Dim rrekap As New r_kontrakfail() With {.DataSource = ds.Tables(0)}
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

    Private Sub fpkontrak_fail_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        load_data()
    End Sub

End Class