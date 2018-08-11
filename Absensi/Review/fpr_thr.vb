Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Public Class fpr_thr

    Private Sub loaddepart()
        Dim cn As OleDbConnection = Nothing
        Dim sql As String = "select * from ms_depart"

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim orow As DataRow = ds.Tables(0).NewRow
            orow("nama") = "All"
            ds.Tables(0).Rows.InsertAt(orow, 0)

            cbdepart.Properties.DataSource = ds.Tables(0)

            cbdepart.ItemIndex = 0

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


    Private Sub fpr_thr_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loaddepart()
    End Sub

    Private Sub btload_Click(sender As System.Object, e As System.EventArgs) Handles btload.Click

        Dim sql As String = ""

        If xnonkary = 1 Then
            sql = "select ms_karyawan.nip,ms_karyawan.nama,0 as gapok,ms_karyawan.tgl_mulai,ms_karyawan.depart, "
        Else
            sql = "select ms_karyawan.nip,ms_karyawan.nama,case when ms_karyawan.nip=ms_usersys4.nip then 0 else ms_karyawan.gapok end as gapok,ms_karyawan.tgl_mulai,ms_karyawan.depart, "
        End If

        sql = String.Format(" {0} case when isdate(tgl_mulai)=1 then " & _
        "DateDiff(Year, ms_karyawan.tgl_mulai, GETDATE()) " & _
        "              - CASE WHEN MONTH(GETDATE())*100+DAY(GETDATE())<MONTH(ms_karyawan.tgl_mulai)*100+DAY(ms_karyawan.tgl_mulai) " & _
        "              THEN 1 ELSE 0 END " & _
        " else 0 end as tahun, " & _
        "case when isdate(tgl_mulai)=1 then " & _
        "(DATEDIFF(month,ms_karyawan.tgl_mulai,GETDATE()) " & _
        "                  - CASE WHEN DAY(GETDATE())<DAY(ms_karyawan.tgl_mulai) THEN 1 ELSE 0 END) % 12 else 0 end as bulan " & _
        "from ms_karyawan inner join ms_golongan on ms_karyawan.kdgol=ms_golongan.kode " & _
        "left join ms_usersys4 on ms_karyawan.nip=ms_usersys4.nip " & _
        "where ms_karyawan.aktif=1 and ms_golongan.jenisgaji='Bulanan' ", sql)

        If Not cbdepart.EditValue = "All" Then
            sql = String.Format(" {0} and ms_karyawan.depart='{1}'", sql, cbdepart.EditValue)
        End If

        If tnip.Text.Trim.Length > 0 Then
            sql = String.Format(" {0} and ms_karyawan.nip='{1}'", sql, tnip.Text.Trim)
        End If

        Cursor = Cursors.WaitCursor

        Dim fs As New fprthr2 With {.WindowState = FormWindowState.Maximized, .sql = sql}
        fs.ShowDialog()

        Cursor = Cursors.Default

    End Sub


End Class