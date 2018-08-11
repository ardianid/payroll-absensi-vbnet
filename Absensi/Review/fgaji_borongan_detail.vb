Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Public Class fgaji_borongan_detail

    Private Sub loadGolongan()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = "select kode,nama from ms_golongan where jenisgaji='Mingguan' and tampilgroup=1 and saktif=1 and kode in (select kode from ms_golongan1)"

            Dim dsgol As New DataSet
            dsgol = Clsmy.GetDataSet(sql, cn)

            Dim dtgol As DataTable = dsgol.Tables(0)

            cbgol.Properties.DataSource = dtgol

            If dtgol.Rows.Count > 0 Then
                cbgol.ItemIndex = 0
            End If

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

    End Sub

    Private Sub loadPegawai()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = "select nip,nama from ms_karyawan where aktif=1"

            Dim dspeg As New DataSet
            dspeg = Clsmy.GetDataSet(sql, cn)

            Dim orow As DataRow = dspeg.Tables(0).NewRow
            orow("nip") = "All"
            orow("nama") = "All"
            dspeg.Tables(0).Rows.InsertAt(orow, 0)

            Dim dtpeg As DataTable = dspeg.Tables(0)

            cbpegawai.Properties.DataSource = dtpeg

            If dtpeg.Rows.Count > 0 Then
                cbpegawai.ItemIndex = 0
            End If

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

    End Sub

    Private Sub fgaji_borongan_detail_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        cbgol.Focus()
    End Sub

    Private Sub fgaji_borongan_detail_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        loadPegawai()
        loadGolongan()

        ttgl1.EditValue = Date.Now
        ttgl2.EditValue = Date.Now

    End Sub

    Private Sub load_data()

        Dim sql As String = String.Format("SELECT ms_karyawan.depart,ms_karyawan.nip,ms_karyawan.nama, tr_hadir.tanggal, tr_hadir.kd_shift, tr_hadir2.namaborongan as nama_borongan, tr_hadir2.jmlhasil, tr_hadir2.price, tr_hadir2.tothasil as tot_hasil,tr_hadir2.tambahan,tr_hadir2.total,tr_hadir2.ket,tr_hadir.jammasuk as jadwalmasuk,tr_hadir.jampulang as jadwalpulang " & _
        "FROM  tr_hadir INNER JOIN " & _
        "tr_hadir2 ON tr_hadir.id = tr_hadir2.id2 INNER JOIN " & _
        "ms_karyawan ON tr_hadir.nip = ms_karyawan.nip " & _
        "WHERE tr_hadir.kdgol='{0}' and tr_hadir.tanggal>='{1}' and tr_hadir.tanggal<='{2}'", cbgol.EditValue, convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue))

        If Not cbpegawai.EditValue.Equals("All") Then
            sql = String.Format(" {0} and ms_karyawan.nip='{1}'", sql, cbpegawai.EditValue)
        End If

        Dim periode As String = String.Format("Periode : {0} s.d {1}", convert_date_to_ind(ttgl1.EditValue), convert_date_to_ind(ttgl2.EditValue))

        Cursor = Cursors.WaitCursor

        Dim fs As New fgaji_borongan_detail2 With {.WindowState = FormWindowState.Maximized, .sql = sql, .periode = periode}
        fs.ShowDialog()

        Cursor = Cursors.Default

    End Sub

    Private Sub btcancel_Click(sender As System.Object, e As System.EventArgs) Handles btcancel.Click
        Me.Close()
    End Sub

    Private Sub btload_Click(sender As System.Object, e As System.EventArgs) Handles btload.Click
        load_data()
    End Sub

End Class