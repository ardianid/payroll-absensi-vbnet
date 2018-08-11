Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Public Class fkurangabsen1

    Private Sub load_data()

        Dim sql As String = String.Format("select ms_karyawan.depart,ms_karyawan.nip,ms_karyawan.nama,ms_tinout.tanggal,ms_tinout.jam,ms_tinout.alasan " & _
        "from ms_tinout inner join ms_karyawan on ms_tinout.nip=ms_karyawan.nip " & _
        "where ms_karyawan.aktif=1 and ms_tinout.tanggal>='{0}' and ms_tinout.tanggal<='{1}'", convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue))

        Dim sql2 As String = String.Format("select ms_karyawan.depart,ms_karyawan.nip,ms_karyawan.nama,ms_absenman.tanggal,ms_absenman.jammasuk,ms_absenman.jampulang,ms_absenman.keterangan " & _
        "from ms_absenman inner join ms_karyawan on ms_absenman.nip=ms_karyawan.nip " & _
        "where ms_karyawan.aktif=1 and ms_absenman.tanggal>='{0}' and ms_absenman.tanggal<='{1}'", convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue))

        Dim periode As String = String.Format("Periode : {0} s.d {1}", convert_date_to_ind(ttgl1.EditValue), convert_date_to_ind(ttgl2.EditValue))

        Cursor = Cursors.WaitCursor

        Dim fs As New fkurangabsen11 With {.WindowState = FormWindowState.Maximized, .sql = sql, .sql2 = sql2, .periode = periode}
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