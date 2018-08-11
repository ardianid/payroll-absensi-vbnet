Public Class fkaryawan3 

    Public dvme As DataView

    Private Sub simpan()
        Dim orow As DataRowView = dvme.AddNew
        orow("id") = 0
        orow("namahari") = cbhari.Text.Trim
        orow("tanggal1") = ttgl1.EditValue
        orow("tanggal2") = ttgl2.EditValue
        dvme.EndInit()
    End Sub

    Private Sub btselesai_Click(sender As System.Object, e As System.EventArgs) Handles btselesai.Click
        Me.Close()
    End Sub

    Private Sub btok_Click(sender As System.Object, e As System.EventArgs) Handles btok.Click
        simpan()

    End Sub

    Private Sub cbhari_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles cbhari.KeyDown
        If e.KeyCode = 13 Then
            simpan()
        End If
    End Sub

    Private Sub fkaryawan3_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        ttgl1.Focus()
    End Sub


    Private Sub fkaryawan3_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ttgl1.EditValue = Date.Now
        ttgl2.EditValue = Date.Now
    End Sub
End Class