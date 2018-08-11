Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Public Class fpr_totborongan

    Private Sub btcnip_Click(sender As System.Object, e As System.EventArgs) Handles btcnip.Click
        Using fskary As New fskary With {.StartPosition = FormStartPosition.CenterParent}
            fskary.ShowDialog(Me)

            tnip.Text = fskary.get_Nip
            tnama.Text = fskary.get_nama

        End Using
    End Sub

    Private Sub tnip_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tnip.KeyDown
        If e.KeyCode = Keys.F4 Then
            btcnip_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tnip_LostFocus(sender As Object, e As System.EventArgs) Handles tnip.LostFocus

        If tnip.Text.Trim.Length > 0 Then

            Dim cn As OleDbConnection = Nothing

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim sql As String = String.Format("select nip,nama,alamat,idmesin from ms_karyawan where aktif=1 and nip='{0}'", tnip.Text.Trim)
                Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim drd As OleDbDataReader = cmd.ExecuteReader

                If drd.HasRows Then
                    If drd.Read Then
                        tnip.Text = drd(0).ToString
                        tnama.Text = drd(1).ToString

                    Else
                        MsgBox("Nip tidak ditemukan...", vbOKOnly + vbInformation, "Informasi")
                        tnip.Text = ""
                        tnama.Text = ""

                    End If
                Else
                    MsgBox("Nip tidak ditemukan...", vbOKOnly + vbInformation, "Informasi")
                    tnip.Text = ""
                    tnama.Text = ""

                End If


            Catch ex As Exception
                MsgBox(ex.ToString)
            Finally
                If Not cn Is Nothing Then
                    If cn.State = ConnectionState.Open Then
                        cn.Close()
                    End If
                End If
            End Try

        Else
            tnama.Text = ""
        End If

    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub fpr_gajiborongan0_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        ttgl1.Focus()
    End Sub

    Private Sub fpr_gajiborongan0_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ttgl1.EditValue = DateValue(Now)
        ttgl2.EditValue = DateValue(Now)

    End Sub


    Private Sub btload_Click(sender As System.Object, e As System.EventArgs) Handles btload.Click

        Dim sql As String = String.Format("SELECT ms_karyawan.nip, ms_karyawan.nama,  tr_hadir2.kode, tr_hadir2.namaborongan, sum(tr_hadir2.jmlhasil) as jmlhasil " & _
        "FROM tr_hadir INNER JOIN tr_hadir2 ON tr_hadir.id = tr_hadir2.id2 INNER JOIN " & _
        "ms_karyawan ON tr_hadir.nip = ms_karyawan.nip " & _
        "where tr_hadir.tanggal>='{0}' and tr_hadir.tanggal<='{1}'", convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue))

        If tnip.Text.Trim.Length > 0 Then
            sql = String.Format(" {0} and ms_karyawan.nip='{1}'", sql, tnip.Text.Trim)
        End If

        sql = String.Format(" {0} group by ms_karyawan.nip, ms_karyawan.nama,  tr_hadir2.kode, tr_hadir2.namaborongan", sql)

        Cursor = Cursors.WaitCursor

        Dim fs As New fpr_totborongan2 With {.WindowState = FormWindowState.Maximized, .tgl1 = ttgl1.EditValue, .tgl2 = ttgl2.EditValue, .sql = sql}
        fs.ShowDialog()

        Cursor = Cursors.Default

    End Sub


End Class