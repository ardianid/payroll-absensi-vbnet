Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Public Class fsltobank

    Private Sub loaddata()

        If ttahun.EditValue = 0 Or ttahun.Text = "" Then
            MsgBox("Tahun tidak boleh kosong", vbOKOnly + vbInformation, "Informasi")
            ttahun.Focus()
            Return
        End If

        Dim sql As String = ""

        sql = "select a.norek,a.nama,a.namabank,b.tahun,b.bulan,b.gapok,b.tunj_jab," & _
            "b.tunj_hadir,b.tunj_akomod,b.tunj_makan,b.tunj_makanlembur,b.gaji_lembur," & _
            "b.tot_hasil,b.tot_harian,case when d.total IS NULL then 0 else d.total end as jamsos " & _
            "from ms_karyawan a inner join tr_gaji b on a.nip=b.nip " & _
         "inner join ms_golongan c on a.kdgol=c.kode " & _
         "left join tr_iuran_jamsos d on (b.nip=d.nip and b.tahun=d.tahun and b.bulan=d.bulan) " & _
            "where c.jenisgaji='Bulanan' and LEN(a.norek) > 0 and a.aktif=1"

        If Not cbbank.EditValue = "All" Then
            sql = String.Format("{0} and a.namabank='{1}'", sql, cbbank.EditValue)
        End If

        sql = String.Format("{0} and b.tahun={1} and b.bulan={2}", sql, ttahun.EditValue, cbbulan.SelectedIndex + 1)

        Using frekapgaji As New fprtobank With {.WindowState = FormWindowState.Maximized, .sql = sql}
            open_wait()
            frekapgaji.ShowDialog(Me)
            '  close_wait()
        End Using

    End Sub

    Private Sub fsltobank_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ttahun.EditValue = Year(Now)
        cbbulan.SelectedIndex = 0
        cbbank.SelectedIndex = 0
    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub btprev_Click(sender As System.Object, e As System.EventArgs) Handles btprev.Click
        loaddata()
    End Sub
End Class