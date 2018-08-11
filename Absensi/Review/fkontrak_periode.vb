Public Class fkontrak_periode

    Private Sub btload_Click(sender As Object, e As EventArgs) Handles btload.Click

        Dim sql As String = String.Format("select nip,nama,depart,tgl_mulai,tgl_kon11 as kon_akhir,tgl_angkat,'KONTRAK I' as stat  from ms_karyawan " & _
        "where aktif = 1 " & _
        "and tgl_kon11>='{0}' and tgl_kon11<='{1}' " & _
        "and not(tgl_kon11 is null) " & _
        "and tgl_kon2 is null and tgl_kon3 is null " & _
        "union all " & _
        "select nip,nama,depart,tgl_mulai,tgl_kon22 as kon_akhir,tgl_angkat,'KONTRAK II'  from ms_karyawan " & _
        "where aktif=1 " & _
        "and tgl_kon22>='{0}' and tgl_kon22<='{1}' " & _
        "and not(tgl_kon22 is null) " & _
        "and tgl_kon3 is null " & _
        "union all " & _
        "select nip,nama,depart,tgl_mulai,tgl_kon33 as kon_akhir,tgl_angkat,'KONTRAK III'  from ms_karyawan " & _
        "where aktif=1 " & _
        "and tgl_kon33>='{0}' and tgl_kon33<='{1}' " & _
        "and not(tgl_kon33 is null)", convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue))

        Dim periode As String = String.Format("Periode : {0} s.d {1}", convert_date_to_ind(ttgl1.EditValue), convert_date_to_ind(ttgl2.EditValue))

        Dim fs As New fkontrak_periode2 With {.WindowState = FormWindowState.Maximized, .sql = sql, .periode = periode}
        fs.ShowDialog()

    End Sub

    Private Sub fkontrak_periode_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        ttgl1.Focus()
    End Sub

    Private Sub fkontrak_periode_Load(sender As Object, e As EventArgs) Handles Me.Load
        ttgl1.EditValue = convert_date_to_ind(Date.Now)
        ttgl2.EditValue = convert_date_to_ind(Date.Now)
    End Sub

    Private Sub btcancel_Click(sender As Object, e As EventArgs) Handles btcancel.Click
        Me.Close()
    End Sub
End Class