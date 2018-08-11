Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Public Class fslrekap_telat

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

        Dim sql As String = ""

        sql = "select b.depart,b.nip,b.nama,c.nama as golongan,a.tanggal,a.jammasuk,a.jadwalmasuk, " & _
        "datediff(MINUTE,a.jadwalmasuk, a.jammasuk) as jmltelat ,a.keterangan " & _
        "from tr_hadir a inner join ms_karyawan b on a.nip=b.nip " & _
        "inner join ms_golongan c on b.kdgol=c.kode where a.stelat=1 and b.aktif=1"

        If Not cbgolongan.EditValue = "All" Then
            sql = String.Format("{0} and c.kode='{1}'", sql, cbgolongan.EditValue)
        End If

        If Not cbpeg.EditValue = "All" Then
            sql = String.Format("{0} and a.nip='{1}'", sql, cbpeg.EditValue)
        End If

        sql = String.Format("{0} and a.tanggal>='{1}' and a.tanggal<='{2}'", sql, convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue))

        Dim periode As String = String.Format("Periode : {0} s.d {1}", convert_date_to_ind(ttgl1.EditValue), convert_date_to_ind(ttgl2.EditValue))

        Using frekapgaji As New fpr_rekaptelat With {.WindowState = FormWindowState.Maximized, .sql = sql, .periode = periode}
            '  open_wait()
            frekapgaji.ShowDialog(Me)
            '  close_wait()
        End Using

    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub fslrekap_telat_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        ttgl1.Focus()
    End Sub

    Private Sub fsl_rekapgaji_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        ttgl1.EditValue = Date.Now
        ttgl2.EditValue = Date.Now

        load_golongan()
        load_kary()

    End Sub

    Private Sub btprev_Click(sender As System.Object, e As System.EventArgs) Handles btprev.Click
        loaddata()
    End Sub

End Class