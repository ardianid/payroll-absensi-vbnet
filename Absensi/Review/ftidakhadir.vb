Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Public Class ftidakhadir

    Private Sub load_data()

        Dim sql As String = String.Format("select tr_hadir.tanggal,ms_karyawan.depart,ms_karyawan.nip,ms_karyawan.nama,tr_hadir.stat,tr_hadir.keterangan,tr_hadir.kd_shift " & _
        "from tr_hadir inner join ms_karyawan on tr_hadir.nip=ms_karyawan.nip " & _
        "where ms_karyawan.aktif=1 and not(tr_hadir.stat in ('HADIR','LAIN-LAIN','OFF')) " & _
        "and tr_hadir.tanggal>='{0}' and tr_hadir.tanggal<='{1}'", convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue))

        Dim periode As String = String.Format("Periode : {0} s.d {1}", convert_date_to_ind(ttgl1.EditValue), convert_date_to_ind(ttgl2.EditValue))

        Cursor = Cursors.WaitCursor

        Dim fs As New ftidakhadir11 With {.WindowState = FormWindowState.Maximized, .sql = sql, .periode = periode}
        fs.ShowDialog()

        Cursor = Cursors.Default

    End Sub

    Private Sub btcancel_Click(sender As System.Object, e As System.EventArgs) Handles btcancel.Click
        Me.Close()
    End Sub

    Private Sub btload_Click(sender As System.Object, e As System.EventArgs) Handles btload.Click
        load_data()
    End Sub

    Private Sub fkurangabsen1_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        ttgl1.Focus()
    End Sub

    Private Sub fkurangabsen1_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ttgl1.EditValue = Date.Now
        ttgl2.EditValue = Date.Now
    End Sub

End Class