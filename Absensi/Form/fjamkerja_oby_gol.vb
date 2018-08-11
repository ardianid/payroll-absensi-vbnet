Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Public Class fjamkerja_oby_gol

    Private bs1 As BindingSource
    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private dvmanager2 As Data.DataViewManager
    Private dv2 As Data.DataView

    Private Sub opendata()

        Const sql As String = "select top 100 * from ms_jamkerjalain"
        Dim cn As OleDbConnection = Nothing

        grid1.DataSource = Nothing

        Try

            open_wait()

            Dim ds As DataSet

            dv1 = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager = New DataViewManager(ds)
            dv1 = dvmanager.CreateDataView(ds.Tables(0))

            bs1 = New BindingSource
            bs1.DataSource = dv1
            bn1.BindingSource = bs1

            grid1.DataSource = bs1

            If dv1.Count > 0 Then
                GridView1.SelectRow(0)
            Else
                opendata2("xxxx")
            End If

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

    Private Sub opendata2(ByVal vkode As String)

        Dim sql As String = String.Format("select a.kode,a.nip,b.nama,c.nama as golongan from ms_jamkerjalain2 a inner join ms_karyawan b on a.nip=b.nip " & _
         "inner join ms_golongan c on  b.kdgol=c.kode where a.kode='{0}'", vkode)

        Dim cn As OleDbConnection = Nothing

        grid2.DataSource = Nothing

        Try

            Dim ds As DataSet

            dv2 = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
            ds = Clsmy.GetDataSet(Sql, cn)

            dvmanager2 = New DataViewManager(ds)
            dv2 = dvmanager.CreateDataView(ds.Tables(0))


            grid2.DataSource = dv2


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

        If tfind.Text.Trim.Length = 0 Then
            Return
        End If

        Dim sql2 As String = "select top 300 a.* from ms_jamkerjalain a"

        Select Case tcbofind.SelectedIndex
            Case 0 ' kode
                sql2 = String.Format("{0} where a.kode like '%{1}%'", sql2, tfind.Text.Trim)
            Case 1 ' tanggal
                If Not (IsDate(tfind.Text.Trim)) Then
                    MsgBox("Tanggal salah...", vbOKOnly + vbInformation, "Informasi")
                    tfind.Focus()
                    Return
                End If

                sql2 = String.Format("{0} where a.tanggal='{1}'", sql2, convert_date_to_eng(tfind.Text.Trim))

            Case 2 ' ket
                sql2 = String.Format("{0} where a.ket like '%{1}%'", sql2, tfind.Text.Trim)
            Case 3 ' nip
                sql2 = "select top 300 a.* from ms_jamkerjalain a where a.kode in (select b.kode from  ms_jamkerjalain2 b"
                sql2 = String.Format("{0} where b.NIP='{1}')", sql2, tfind.Text.Trim)
        End Select

        Dim cn As OleDbConnection = Nothing

        grid1.DataSource = Nothing

        Try

            open_wait()

            Dim ds As DataSet

            dv1 = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql2, cn)

            dvmanager = New DataViewManager(ds)
            dv1 = dvmanager.CreateDataView(ds.Tables(0))

            bs1 = New BindingSource
            bs1.DataSource = dv1
            bn1.BindingSource = bs1

            grid1.DataSource = bs1

            If dv1.Count > 0 Then
                GridView1.SelectRow(0)
            Else
                opendata2("xxxxx")
            End If

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

    Private Sub hapus()

        Dim sql As String = String.Format("delete from ms_jamkerjalain where kode='{0}'", dv1(Me.BindingContext(bs1).Position)("kode").ToString)
        Dim sql2 As String = String.Format("delete from ms_jamkerjalain2 where kode='{0}'", dv1(Me.BindingContext(bs1).Position)("kode").ToString)

        Dim cn As OleDbConnection = Nothing
        Dim comd As OleDbCommand = Nothing

        Try

            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction

            comd = New OleDbCommand(sql2, cn, sqltrans)
            comd.ExecuteNonQuery()

            comd = New OleDbCommand(sql, cn, sqltrans)
            comd.ExecuteNonQuery()

            Clsmy.InsertToLog(cn, "btjamkerjalain", 0, 0, 1, 0, dv1(Me.BindingContext(bs1).Position)("kode").ToString, dv1(Me.BindingContext(bs1).Position)("ket").ToString, sqltrans)

            sqltrans.Commit()

            close_wait()

            MsgBox("Data telah dihapus...", vbOKOnly + vbInformation, "Informasi")

            opendata()

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

    Private Sub fjamkerja_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        tcbofind.SelectedIndex = 0

        Get_Aksesform()

        opendata()
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

    Private Sub tfind_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles tfind.KeyDown
        If e.KeyCode = 13 Then
            cari()
        End If
    End Sub

    Private Sub tsfind_Click(sender As System.Object, e As System.EventArgs) Handles tsfind.Click
        cari()
    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        Dim kode As String = dv1(bs1.Position)("kode").ToString

        opendata2(kode)

    End Sub

    Private Sub tsedit_Click(sender As System.Object, e As System.EventArgs) Handles tsedit.Click

        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If

        Using fjamker2 As New fjamkerja_oby_gol2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position}
            fjamker2.ShowDialog()
        End Using

    End Sub

    Private Sub tsadd_Click(sender As System.Object, e As System.EventArgs) Handles tsadd.Click
        Using fjamker2 As New fjamkerja_oby_gol2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = True, .position = 0}
            fjamker2.ShowDialog()
        End Using
    End Sub

End Class