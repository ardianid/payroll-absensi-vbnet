Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Public Class fkaryawan
    Private bs1 As BindingSource
    Private ds As DataSet
    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private Sub opendata()

        Dim sql As String = "select a.nip,idmesin,a.nama,a.tgllahir,a.alamat,a.jniskelamin,a.notelp,a.nohp,a.aktif,a.depart,b.nama as golongan,a.kdgol,a.jabatan from ms_karyawan a inner join ms_golongan b on a.kdgol=b.kode where a.shiden=0"

        If tsstat.SelectedIndex = 0 Then
            sql = String.Format(" {0} and a.aktif=1", sql)
        Else
            sql = String.Format(" {0} and a.aktif=0", sql)
        End If

        sql = String.Format(" {0} order by a.nip", sql)

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

        Dim sql As String = "select a.nip,idmesin,a.nama,a.tgllahir,a.alamat,a.jniskelamin,a.notelp,a.nohp,a.aktif,a.depart,b.nama as golongan,a.kdgol,a.jabatan from ms_karyawan a inner join ms_golongan b on a.kdgol=b.kode where a.shiden=0"

        If tsstat.SelectedIndex = 0 Then
            sql = String.Format(" {0} and a.aktif=1", sql)
        Else
            sql = String.Format(" {0} and a.aktif=0", sql)
        End If

        Dim scbo As Integer = tcbofind.SelectedIndex
        Select Case scbo
            Case 0 ' nip
                sql = String.Format(" {0} and a.nip='{1}'", sql, tfind.Text.Trim)
            Case 1 ' idmesin
                sql = String.Format(" {0} and a.idmesin={1}", sql, tfind.Text.Trim)
            Case 2 ' nama
                sql = String.Format(" {0} and a.nama like '%{1}%'", sql, tfind.Text.Trim)
            Case 3 ' tgl lahir

                If Not IsDate(tsfind.Text.Trim) Then

                    MsgBox("Format tanggal yang anda masukkan salah", MsgBoxStyle.Information, "Informasi")
                    tfind.Focus()
                    Exit Sub
                End If

                sql = String.Format(" {0} and a.tgllahir='{1}'", sql, convert_date_to_eng(tfind.Text.Trim))

            Case 4 ' alamat

                sql = String.Format(" {0} and a.alamat like '%{1}%'", sql, tfind.Text.Trim)
            Case 5 ' notelp
                sql = String.Format(" {0} and a.notelp like '%{1}%'", sql, tfind.Text.Trim)
            Case 6 'nohp
                sql = String.Format(" {0} and a.nohp like '%{1}%'", sql, tfind.Text.Trim)
        End Select

        sql = String.Format(" {0} order by a.nip", sql)

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

        Dim sql As String = String.Format("delete from ms_karyawan where nip='{0}'", dv1(Me.BindingContext(bs1).Position)("nip").ToString)
        Dim sql2 As String = String.Format("delete from ms_karyawan2 where nip='{0}'", dv1(Me.BindingContext(bs1).Position)("nip").ToString)
        Dim sql3 As String = String.Format("delete from ms_karyawan3 where nip='{0}'", dv1(Me.BindingContext(bs1).Position)("nip").ToString)

        Dim cn As OleDbConnection = Nothing
        Dim comd As OleDbCommand = Nothing

        Try

            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction

            comd = New OleDbCommand(sql3, cn, sqltrans)
            comd.ExecuteNonQuery()

            comd = New OleDbCommand(sql2, cn, sqltrans)
            comd.ExecuteNonQuery()

            comd = New OleDbCommand(sql, cn, sqltrans)
            comd.ExecuteNonQuery()

            Clsmy.InsertToLog(cn, "btkary", 0, 0, 1, 0, dv1(Me.BindingContext(bs1).Position)("nip").ToString, dv1(Me.BindingContext(bs1).Position)("nama").ToString, sqltrans)

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

    Private Sub ceknonaktif()

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select aktif from ms_karyawan where nip='{0}'", dv1(Me.BindingContext(bs1).Position)("nip").ToString)
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If IsNumeric(drd(0).ToString) Then
                    dv1(Me.BindingContext(bs1).Position)("aktif") = drd(0).ToString
                End If
            End If
            drd.Close()


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

    Private Sub Get_Aksesform()

        Dim rows() As DataRow = dtmenu.Select(String.Format("NAMAFORM='{0}'", Me.Name.ToUpper.ToString))

        If Convert.ToInt16(rows(0)("t_add")) = 1 Then
            tsadd.Enabled = True
        Else
            tsadd.Enabled = False
        End If

        If Convert.ToInt16(rows(0)("t_edit")) = 1 Then
            tsedit.Enabled = True
            tspindahgol.Enabled = True
            tsnonakt.Enabled = True
        Else
            tsedit.Enabled = False
            tspindahgol.Enabled = False
            tsnonakt.Enabled = False
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

    Private Sub fuser_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        tcbofind.SelectedIndex = 0

        Get_Aksesform()

        tsstat.SelectedIndex = 0

        'opendata()
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
        tsfind.Text = ""
        opendata()
    End Sub

    Private Sub tsdel_Click(sender As System.Object, e As System.EventArgs) Handles tsdel.Click

        If dv1.Count < 1 Then
            Exit Sub
        End If

        ceknonaktif()

        If Integer.Parse(dv1(Me.BindingContext(bs1).Position)("aktif").ToString) = 0 Then
            MsgBox("Karyawan tidak aktif, tidak bisa dihapus...", vbOKOnly + vbInformation, "Informasi")
            Return
        End If

        If MsgBox("Yakin akan dihapus ?", vbYesNo + vbQuestion, "Konfirmasi") = vbNo Then
            Exit Sub
        End If

        hapus()

    End Sub

    Private Sub tsadd_Click(sender As System.Object, e As System.EventArgs) Handles tsadd.Click
        Using fkar2 As New fkaryawan2 With {.StartPosition = FormStartPosition.CenterScreen, .dv = dv1, .addstat = True, .position = 0}
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

        ceknonaktif()

        Dim aktifsimpan As Boolean = False

        If Integer.Parse(dv1(Me.BindingContext(bs1).Position)("aktif").ToString) = 1 Then
            aktifsimpan = True
        End If

        Using fkar2 As New fkaryawan2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position}
            fkar2.btsimpan.Enabled = aktifsimpan
            fkar2.ShowDialog()
        End Using

    End Sub

    Private Sub tspindahgol_Click(sender As System.Object, e As System.EventArgs) Handles tspindahgol.Click

        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If

        ceknonaktif()

        If Integer.Parse(dv1(Me.BindingContext(bs1).Position)("aktif").ToString) = 0 Then
            MsgBox("Karyawan tidak aktif...", vbOKOnly + vbInformation, "Informasi")
            Return
        End If

        Dim snip As String = dv1(Me.BindingContext(bs1).Position)("nip").ToString
        Dim sidmesin As String = dv1(Me.BindingContext(bs1).Position)("idmesin").ToString
        Dim snama As String = dv1(Me.BindingContext(bs1).Position)("nama").ToString
        Dim skdgol As String = dv1(Me.BindingContext(bs1).Position)("kdgol").ToString
        Dim sgolongan As String = dv1(Me.BindingContext(bs1).Position)("golongan").ToString
        Dim sdepart As String = dv1(Me.BindingContext(bs1).Position)("depart").ToString
        Dim sjabatan As String = dv1(Me.BindingContext(bs1).Position)("jabatan").ToString

        Using fkar2 As New fkaryawan_pgol With {.StartPosition = FormStartPosition.CenterParent, .snip = snip, .snama = snama, _
                                                .sidmes = sidmesin, .kdgol = skdgol, .sgol = sgolongan, .sdepart = sdepart, .sjab = sjabatan}
            fkar2.ShowDialog()

            If fkar2.get_statok = True Then

                dv1(Me.BindingContext(bs1).Position)("nip") = fkar2.get_Nip
                dv1(Me.BindingContext(bs1).Position)("idmesin") = fkar2.get_IDMesin
                dv1(Me.BindingContext(bs1).Position)("kdgol") = fkar2.get_KDGolongan
                dv1(Me.BindingContext(bs1).Position)("golongan") = fkar2.get_NAMAGolongan
                dv1(Me.BindingContext(bs1).Position)("depart") = fkar2.get_Depart
                dv1(Me.BindingContext(bs1).Position)("jabatan") = fkar2.get_jabatan

                'grid1.Refresh()

            End If

        End Using

    End Sub

    Private Sub tsnonakt_Click(sender As System.Object, e As System.EventArgs) Handles tsnonakt.Click

        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If

        ceknonaktif()

        If Integer.Parse(dv1(Me.BindingContext(bs1).Position)("aktif").ToString) = 0 Then
            MsgBox("Karyawan sudah tidak aktif...", vbOKOnly + vbInformation, "Informasi")
            Return
        End If

        Dim snip As String = dv1(Me.BindingContext(bs1).Position)("nip").ToString
        Dim snama As String = dv1(Me.BindingContext(bs1).Position)("nama").ToString
        Dim sdepart As String = dv1(Me.BindingContext(bs1).Position)("depart").ToString

        Using fkar2 As New fkaryawan_nonakt With {.StartPosition = FormStartPosition.CenterParent, .snip = snip, .snama = snama, _
                                                    .sdepart = sdepart}
            fkar2.ShowDialog()

            Dim nipbaru As String = fkar2.get_Nip

            If Not nipbaru.Equals("") Then
                dv1(Me.BindingContext(bs1).Position)("nip") = nipbaru
                dv1(Me.BindingContext(bs1).Position)("aktif") = 0
            End If

        End Using

    End Sub

    Private Sub tsstat_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles tsstat.SelectedIndexChanged
        If tfind.Text.Trim.Length = 0 Then
            opendata()
        Else
            cari()
        End If
    End Sub


End Class