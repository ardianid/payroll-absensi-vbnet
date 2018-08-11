Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Imports DevExpress.XtraReports.UI

Public Class fpr_karybydepart

    Private Sub load_print()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn()


            Dim ds As DataSet = New dskary_bydepart3
            ds = Clsmy.GetDataSet("select ms_karyawan.depart,ms_karyawan.nip,ms_karyawan.nama,ms_karyawan.tgl_mulai,ms_golongan.nama as nama_golongan,ms_karyawan.jabatan,ms_karyawan.tgl_kon1,ms_karyawan.tgl_kon11,ms_karyawan.tgl_kon2,ms_karyawan.tgl_kon22,ms_karyawan.tgl_kon3,ms_karyawan.tgl_kon33 " & _
            "from ms_karyawan inner join ms_golongan on ms_karyawan.kdgol=ms_golongan.kode " & _
            "where ms_karyawan.aktif=1", cn)

            Dim ops As New System.Drawing.Printing.PrinterSettings
            Dim rrekap As New rkary_bydepart3() With {.DataSource = ds.Tables(0)}
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