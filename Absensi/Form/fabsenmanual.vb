Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Public Class fabsenmanual
    Private bs1 As BindingSource
    Private ds As DataSet
    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private Sub opendata()

        Const sql As String = "SELECT  top 100 ms_karyawan.nip, ms_karyawan.nama, ms_absenman.tanggal, ms_absenman.keterangan, ms_absenman.jammasuk, ms_absenman.jampulang, ms_karyawan.idmesin " & _
        "FROM ms_absenman INNER JOIN ms_karyawan ON ms_absenman.nip = ms_karyawan.nip where ms_karyawan.shiden=0"

        Dim cn As OleDbConnection = Nothing

        grid1.DataSource = Nothing

        Try

            open_wait()

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

        Dim sql As String = "SELECT  top 5000 ms_karyawan.nip, ms_karyawan.nama, ms_absenman.tanggal, ms_absenman.keterangan, ms_absenman.jammasuk, ms_absenman.jampulang, ms_karyawan.idmesin " & _
        "FROM ms_absenman INNER JOIN ms_karyawan ON ms_absenman.nip = ms_karyawan.nip where ms_karyawan.shiden=0"

        Dim scbo As Integer = tcbofind.SelectedIndex

        Select Case scbo
            Case 0 ' nip
                sql = String.Format("{0} and ms_karyawan.nip='{1}'", sql, tfind.Text.Trim)
            Case 1 ' nama
                sql = String.Format("{0} and ms_karyawan.nama like '%{1}%'", sql, tfind.Text.Trim)
            Case 2 ' tgl lahir

                If Not IsDate(tfind.Text.Trim) Then

                    MsgBox("Format tanggal yang anda masukkan salah", MsgBoxStyle.Information, "Informasi")
                    tfind.Focus()
                    Exit Sub
                End If

                sql = String.Format("{0} and ms_absenman.tanggal='{1}'", sql, tfind.Text.Trim)

        End Select

        Try

            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

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

        Dim idmesin As String = dv1(Me.BindingContext(bs1).Position)("idmesin").ToString
        Dim tanggal As String = dv1(Me.BindingContext(bs1).Position)("tanggal").ToString
        Dim jammasuk As String = dv1(Me.BindingContext(bs1).Position)("jammasuk").ToString
        Dim jampulang As String = dv1(Me.BindingContext(bs1).Position)("jampulang").ToString

        tanggal = DateValue(tanggal)
        jammasuk = TimeValue(jammasuk)
        jampulang = TimeValue(jampulang)

        Dim tgl1 As String = String.Format("{0} {1}", tanggal, jammasuk)
        Dim tgl2 As String = String.Format("{0} {1}", tanggal, jampulang)

        Dim sql As String = String.Format("delete from ms_absenman where nip='{0}' and tanggal='{1}' and jammasuk='{2}' and jampulang='{3}'", _
                                          dv1(Me.BindingContext(bs1).Position)("nip").ToString, convert_date_to_eng(dv1(Me.BindingContext(bs1).Position)("tanggal").ToString), jammasuk, jampulang)

        Dim sql1 As String = String.Format("delete from ms_inout where userid={0} and checktime='{1}'", idmesin, convert_datetime_to_eng(tgl1))
        Dim sql2 As String = String.Format("delete from ms_inout where userid={0} and checktime='{1}'", idmesin, convert_datetime_to_eng(tgl2))

        Dim cn As OleDbConnection = Nothing
        Dim comd As OleDbCommand = Nothing

        Try

            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction

            Dim comd1 As OleDbCommand = New OleDbCommand(sql1, cn, sqltrans)
            comd1.ExecuteNonQuery()

            Dim comd2 As OleDbCommand = New OleDbCommand(sql2, cn, sqltrans)
            comd2.ExecuteNonQuery()

            comd = New OleDbCommand(sql, cn, sqltrans)
            comd.ExecuteNonQuery()

            Clsmy.InsertToLog(cn, "btgajiminggu", 0, 0, 1, 0, dv1(Me.BindingContext(bs1).Position)("nip").ToString, convert_date_to_eng(dv1(Me.BindingContext(bs1).Position)("tanggal").ToString), sqltrans)

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
        Using fkar2 As New fabsenmanual2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = True, .position = 0, .nnip_s = ""}
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

        Using fkar2 As New fabsenmanual2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position, .nnip_s = ""}
            fkar2.ShowDialog()
        End Using
    End Sub

End Class