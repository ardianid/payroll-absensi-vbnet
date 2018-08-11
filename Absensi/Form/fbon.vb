Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Public Class fbon

    Private bs1 As BindingSource
    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private Sub opendata()

        Const sql As String = "select top 100 a.nobukti,a.tanggal,b.nip,b.nama,a.jumlah,a.ket from ms_bon a inner join ms_karyawan b on a.nip=b.nip where b.shiden=0 order by tanggal,nobukti desc"

        Dim cn As OleDbConnection = Nothing

        grid1.DataSource = Nothing

        Try

            open_wait()

            dv1 = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet
            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager = New DataViewManager(ds)
            dv1 = dvmanager.CreateDataView(ds.Tables(0))

            bs1 = New BindingSource
            bs1.DataSource = dv1
            bn1.BindingSource = bs1

            grid1.DataSource = bs1

            close_wait()


        Catch ex As OleDb.OleDbException
            close_wait()
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally


            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

        End Try

    End Sub

    Private Sub cari()

        'bs1.DataSource = Nothing
        grid1.DataSource = Nothing
        Dim cn As OleDbConnection = Nothing

        Dim sql As String = "select top 5000 a.nobukti,a.tanggal,b.nip,b.nama,a.jumlah,a.ket from ms_bon a inner join ms_karyawan b on a.nip=b.nip where shiden=0"

        Dim scbo As Integer = tcbofind.SelectedIndex

        Select Case scbo
            Case 0 ' nobukti
                sql = String.Format("{0} and a.nobukti='{1}'", sql, tfind.Text.Trim)
            Case 1
                If Not IsDate(tfind.Text.Trim) Then

                    MsgBox("Format tanggal yang anda masukkan salah", MsgBoxStyle.Information, "Informasi")
                    tfind.Focus()
                    Exit Sub
                End If

                sql = String.Format("{0} and a.tanggal='{1}'", sql, tfind.Text.Trim)
            Case 2
                sql = String.Format("{0} and b.nip='{1}'", sql, tfind.Text.Trim)
            Case 3 ' nama
                sql = String.Format("{0} and b.nama like '%{1}%'", sql, tfind.Text.Trim)
            Case 4 ' tgl lahir

                sql = String.Format("{0} and a.ket like '%{1}%'", sql, tfind.Text.Trim)

        End Select

        ' sql = String.Format("{0} order by a.tanggal.a.nobukti desc", sql)

        Try

            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet
            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager = New DataViewManager(ds)
            dv1 = dvmanager.CreateDataView(ds.Tables(0))

            bs1.DataSource = dv1
            bn1.BindingSource = bs1

            grid1.DataSource = bs1

            close_wait()

        Catch ex As Exception
            close_wait()
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally
            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

    End Sub

    Private Sub hapus()

        Dim cn As OleDbConnection = Nothing

        Try

            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction

            Dim sql As String = String.Format("update ms_karyawan set jmlbon=jmlbon-{0} where nip='{1}'", Replace(dv1(bs1.Position)("jumlah"), ",", "."), dv1(bs1.Position)("nip"))
            Using cmd1 As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd1.ExecuteNonQuery()
            End Using

            Dim sql2 As String = String.Format("delete from ms_bon where nobukti='{0}'", dv1(bs1.Position)("nobukti"))
            Using cmd2 As OleDbCommand = New OleDbCommand(sql2, cn, sqltrans)
                cmd2.ExecuteNonQuery()
            End Using

            Clsmy.InsertToLog(cn, "btbon", 0, 0, 1, 0, dv1(Me.BindingContext(bs1).Position)("nobukti").ToString, dv1(Me.BindingContext(bs1).Position)("nip").ToString, sqltrans)

            sqltrans.Commit()

            dv1.Delete(bs1.Position)

            close_wait()
            MsgBox("Data dihapus..", vbOKOnly + vbInformation, "Informasi")


        Catch ex As Exception
            close_wait()
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

    End Sub

    Private Sub Get_Aksesform()

        Dim rows() As DataRow = dtmenu.Select(String.Format("NAMAFORM='{0}'", Me.Name.ToUpper.ToString))

        If Convert.ToInt16(rows(0)("t_add")) = 1 Then
            tsadd.Enabled = True
        Else
            tsadd.Enabled = False
        End If

        If Convert.ToInt16(rows(0)("t_edit")) = 1 Then
            tsedit.Enabled = True
        Else
            tsedit.Enabled = False
        End If

        If Convert.ToInt16(rows(0)("t_del")) = 1 Then
            tsdel.Enabled = True
        Else
            tsdel.Enabled = False
        End If

        'If Convert.ToInt16(rows(0)("t_lap")) = 1 Then

        'Else

        'End If

    End Sub

    Private Sub fstathadir_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        tcbofind.SelectedIndex = 0

        Get_Aksesform()

        opendata()
    End Sub

    Private Sub tsfind_Click(sender As System.Object, e As System.EventArgs) Handles tsfind.Click
        cari()
    End Sub

    Private Sub tfind_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles tfind.KeyDown
        If e.KeyCode = 13 Then
            cari()
        End If
    End Sub

    Private Sub tsref_Click(sender As System.Object, e As System.EventArgs) Handles tsref.Click
        tfind.Text = ""
        opendata()
    End Sub

    Private Sub tsdel_Click(sender As System.Object, e As System.EventArgs) Handles tsdel.Click

        If dv1.Count < 1 Then
            Exit Sub
        End If

        If MsgBox("Yakin akan dihapus ?", vbYesNo + vbQuestion, "Konfirmasi") = vbNo Then
            Exit Sub
        End If

        hapus()

    End Sub

    Private Sub tsadd_Click(sender As System.Object, e As System.EventArgs) Handles tsadd.Click
        Using fkar2 As New fbon2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = True, .position = 0}
            fkar2.ShowDialog()
        End Using
    End Sub

    Private Sub tsedit_Click(sender As System.Object, e As System.EventArgs) Handles tsedit.Click
        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If

        Using fkar2 As New fbon2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position}
            fkar2.ShowDialog()
        End Using
    End Sub


End Class