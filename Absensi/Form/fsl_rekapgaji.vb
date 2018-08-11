Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Public Class fsl_rekapgaji


    Private Sub load_golongan()

        Dim cn As OleDbConnection = Nothing
        Dim dtgol As DataTable

        Const sql As String = "select kode,nama from ms_golongan"

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            dtgol = New DataTable

            Dim ds As DataSet = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            dtgol = ds.Tables(0)

            Dim orow As DataRow = dtgol.NewRow
            orow("kode") = "All"
            orow("nama") = "All"
            dtgol.Rows.InsertAt(orow, 0)

            cbgolongan.Properties.DataSource = dtgol

            cbgolongan.ItemIndex = 0

        Catch ex As Exception

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

        End Try
    End Sub

    Private Sub load_kary()

        Dim cn As OleDbConnection = Nothing
        Dim dtgol As DataTable

        Const sql As String = "select nip,nama from ms_karyawan where aktif=1"

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            dtgol = New DataTable

            Dim ds As DataSet = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            dtgol = ds.Tables(0)

            Dim orow As DataRow = dtgol.NewRow
            orow("nip") = "All"
            orow("nama") = "All"
            dtgol.Rows.InsertAt(orow, 0)

            cbpeg.Properties.DataSource = dtgol

            cbpeg.ItemIndex = 0

        Catch ex As Exception

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

        End Try
    End Sub

    Private Sub loaddata()

        If ttahun.EditValue = 0 Or ttahun.Text = "" Then
            MsgBox("Tahun tidak boleh kosong", vbOKOnly + vbInformation, "Informasi")
            ttahun.Focus()
            Return
        End If

        Dim sql As String = ""

        sql = "select a.nip,e.nip as nipuser,a.nama,b.nama as golongan,c.tahun,c.bulan,c.minggu,c.gapok,c.tunj_jab," & _
            "c.tunj_hadir,c.tunj_akomod,c.tunj_makan,c.tunj_makanlembur,c.jmllembur,c.gaji_lembur," & _
            "c.jml_hasil,c.tot_hasil,c.tot_harian,c.keterangan,case when d.total IS NULL then 0 else d.total end as jamsos,c.kurangi_gapok from ms_karyawan a " & _
            "inner join ms_golongan b on a.kdgol=b.kode " & _
         "inner join tr_gaji c on a.nip=c.nip " & _
         "left join ms_usersys4 e on a.nip=e.nip " & _
         "left join tr_iuran_jamsos d on (a.nip=d.nip and c.tahun=d.tahun and c.bulan=d.bulan) where a.aktif=1"

        If Not cbgolongan.EditValue = "All" Then
            sql = String.Format("{0} and b.kode='{1}'", sql, cbgolongan.EditValue)
        End If

        If Not cbpeg.EditValue = "All" Then
            sql = String.Format("{0} and a.nip='{1}'", sql, cbpeg.EditValue)
        End If

        sql = String.Format("{0} and c.tahun={1} and c.bulan={2}", sql, ttahun.EditValue, cbbulan.SelectedIndex + 1)

        Using frekapgaji As New fprrekap_gaji With {.WindowState = FormWindowState.Maximized, .sql = sql}
            open_wait()
            frekapgaji.ShowDialog(Me)
            '  close_wait()
        End Using

    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub fsl_rekapgaji_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ttahun.EditValue = Year(Now)
        cbbulan.SelectedIndex = 0

        load_golongan()
        load_kary()

    End Sub

    Private Sub btprev_Click(sender As System.Object, e As System.EventArgs) Handles btprev.Click
        loaddata()
    End Sub
End Class