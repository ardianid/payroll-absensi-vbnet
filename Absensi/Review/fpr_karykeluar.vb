Public Class fpr_karykeluar 

    Private Sub btclose_Click(sender As Object, e As EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub fpr_karykeluar_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        ttgl.Focus()
    End Sub

    Private Sub fpr_karykeluar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ttgl.EditValue = DateValue(Date.Now)
        ttgl2.EditValue = DateValue(Date.Now)
    End Sub

    Private Sub btload_Click(sender As Object, e As EventArgs) Handles btload.Click

        Cursor = Cursors.WaitCursor

        Dim sql As String = String.Format("select nama,alamat,depart,tgl_keluar,alasan_keluar,tgl_mulai from ms_karyawan where aktif=0 " & _
        "and tgl_keluar>='{0}' and tgl_keluar<='{1}'", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))

        Dim periode As String = String.Format("Periode : {0} s.d {1}", convert_date_to_ind(ttgl.EditValue), convert_date_to_ind(ttgl2.EditValue))

        Dim fs As New fpr_karykeluar2 With {.WindowState = FormWindowState.Maximized, .sql = sql, .tgl = periode}
        fs.ShowDialog()

        Cursor = Cursors.Default

    End Sub

End Class