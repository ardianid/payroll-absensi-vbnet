Public Class frp_karyawanbaru 

    Private Sub btclose_Click(sender As Object, e As EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub frp_karyawanbaru_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        ttgl.Focus()
    End Sub

    Private Sub frp_karyawanbaru_Load(sender As Object, e As EventArgs) Handles Me.Load
        ttgl.EditValue = DateValue(Date.Now)
        ttgl2.EditValue = DateValue(Date.Now)
    End Sub

    Private Sub btload_Click(sender As Object, e As EventArgs) Handles btload.Click

        Cursor = Cursors.WaitCursor

        Dim sql As String = String.Format("select nip,nama,depart,tgl_mulai from ms_karyawan " & _
        "where aktif=1 and tgl_mulai>='{0}' and tgl_mulai<='{1}'", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))

        Dim periode As String = String.Format("Periode : {0} s.d {1}", convert_date_to_ind(ttgl.EditValue), convert_date_to_ind(ttgl2.EditValue))

        Dim fs As New frp_karyawanbaru2 With {.WindowState = FormWindowState.Maximized, .sql = sql, .tgl = periode}
        fs.ShowDialog()

        Cursor = Cursors.Default

    End Sub

End Class