Public Class fpr_jamkerja

    Private Sub fpr_totlembur_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        ttgl1.Focus()
    End Sub

    Private Sub fpr_totlembur_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ttgl1.EditValue = DateValue(Date.Now)
        ttgl2.EditValue = DateValue(Date.Now)

        tjenislap.SelectedIndex = 0

    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub btload_Click(sender As System.Object, e As System.EventArgs) Handles btload.Click

        'Dim sql As String = String.Format("select depart,COUNT(nip) as jmlkary,0 as jmllembur from ms_karyawan where aktif=1 group by depart " & _
        '" union all " & _
        '"select ms_karyawan.depart,0,SUM(tr_hadir.jamlembur) + SUM(tr_hadir.tamblembur) " & _
        '"from tr_hadir inner join ms_karyawan on tr_hadir.nip=ms_karyawan.nip " & _
        '"where ms_karyawan.aktif=1 and tr_hadir.tanggal>='{0}' and tr_hadir.tanggal<='{1}' " & _
        '"and not(ms_karyawan.kdgol in (select ms_golongan.kode from ms_golongan inner join ms_golongan1 on ms_golongan.kode=ms_golongan1.kode)) " & _
        '"group by ms_karyawan.depart", convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue))

        Dim sqldetail As String = String.Format("select ms_karyawan.nip,ms_karyawan.nama,tr_hadir.depart, " & _
        "tr_hadir.tanggal, tr_hadir.jammasuk,tr_hadir.jampulang,convert(time,tr_hadir.jammasuk) as jammasuk1,convert(time,tr_hadir.jampulang) as jampulang1, tr_hadir.jamkerja " & _
        "from tr_hadir inner join ms_karyawan  " & _
        "on tr_hadir.nip=ms_karyawan.nip  " & _
        "where ms_karyawan.aktif = 1 and jamkerja>0 " & _
        "and tr_hadir.tanggal>='{0}' and tr_hadir.tanggal<='{1}'", convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue))

        Cursor = Cursors.WaitCursor

        Dim jenislap As Integer = 1
        Dim sqlhasil As String = sqldetail
        If tjenislap.EditValue.ToString.Equals("LAP DETAIL") Then
            jenislap = 2
            'sqlhasil = sqldetail
        End If


        Dim fs As New fpr_jamkerja2 With {.WindowState = FormWindowState.Maximized, .tgl1 = ttgl1.EditValue, .tgl2 = ttgl2.EditValue, .sql = sqlhasil, .jenislap = jenislap}
        fs.ShowDialog()

        Cursor = Cursors.Default

    End Sub


End Class