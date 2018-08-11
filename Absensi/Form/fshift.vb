Imports System.Data.OleDb
Public Class fshift

    Private dvmanager1 As Data.DataViewManager
    Private dv1 As Data.DataView

    Private dvmanager2 As Data.DataViewManager
    Private dv2 As Data.DataView

    Dim dtjam_kerja As DataTable
    Dim statproses As Boolean = False

    Private Sub load_jamkerja()

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select kode,LEFT (CONVERT (VARCHAR, CONVERT (time(0), jammasuk), 108), 5) as jammasuk, " & _
            "LEFT (CONVERT (VARCHAR, CONVERT (time(0), jampulang), 108), 5) as jampulang from ms_jamkerja")

            Dim dsjam As DataSet = New DataSet
            dsjam = Clsmy.GetDataSet(sql, cn)

            Dim orow As DataRow = dsjam.Tables(0).NewRow
            orow("kode") = ""
            orow("jammasuk") = "00:00"
            orow("jampulang") = "00:00"
            dsjam.Tables(0).Rows.InsertAt(orow, 0)

            dtjam_kerja = dsjam.Tables(0)

            rjam_kerja.DataSource = dtjam_kerja

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

    Private Sub load_grid()

        Dim sql As String = "select * from ms_shift"

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager1 = New DataViewManager(ds)
            dv1 = dvmanager1.CreateDataView(ds.Tables(0))

            grid1.DataSource = dv1

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

    Private Sub load_detail()

        grid2.DataSource = Nothing

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        Dim namashift As String = dv1(Me.BindingContext(dv1).Position)("nama_shift").ToString

        Dim sql As String = String.Format("select ms_shift2.noid,ms_shift2.nama_shift,ms_shift2.nama_periode,ms_shift2.kd_jam,LEFT (CONVERT (VARCHAR, CONVERT (time(0), ms_jamkerja.jammasuk), 108), 5) as jammasuk, " & _
        "LEFT (CONVERT (VARCHAR, CONVERT (time(0), ms_jamkerja.jampulang), 108), 5) as jampulang,ms_shift2.num_shift " & _
        "from ms_shift2 left join ms_jamkerja on ms_shift2.kd_jam=ms_jamkerja.kode " & _
        "inner join ms_shift on ms_shift.nama_shift=ms_shift2.nama_shift where ms_shift.nama_shift='{0}'", namashift)

        Dim cn As OleDbConnection = Nothing
        Try
            cn = New OleDbConnection
            cn = Clsmy.open_conn


            Dim ds As New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager2 = New DataViewManager(ds)
            dv2 = dvmanager2.CreateDataView(ds.Tables(0))

            grid2.DataSource = dv2


            Dim sqlcek As String = String.Format("select COUNT(nama_periode) as jml from ms_shift2 where nama_shift='{0}'", namashift)
            Dim cmdcek As OleDbCommand = New OleDbCommand(sqlcek, cn)
            Dim drdcek As OleDbDataReader = cmdcek.ExecuteReader

            Dim adacek As Boolean = False
            If drdcek.Read Then
                If IsNumeric(drdcek(0).ToString) Then
                    If Integer.Parse(drdcek(0).ToString) > 0 Then
                        adacek = True
                    End If
                End If
            End If
            drdcek.Close()

            If adacek = False And Not (namashift.Equals("")) Then
                load_detail_null()
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

    Private Sub load_detail_null()

        If dv2.Count <= 0 Then

            Dim namashift As String = dv1(Me.BindingContext(dv1).Position)("nama_shift").ToString
            Dim perputaran As String = dv1(Me.BindingContext(dv1).Position)("perputaran").ToString
            Dim periode As String = dv1(Me.BindingContext(dv1).Position)("periode").ToString

            If perputaran.Equals("MINGGU") Then

                Dim num_s As Integer = 0

                For i As Integer = 1 To Integer.Parse(periode)
                    For x As Integer = 1 To 7

                        num_s = num_s + 1

                        Dim orow As DataRowView = dv2.AddNew

                        Dim namahari As String
                        Select Case x
                            Case 1
                                namahari = "SENIN"
                            Case 2
                                namahari = "SELASA"
                            Case 3
                                namahari = "RABU"
                            Case 4
                                namahari = "KAMIS"
                            Case 5
                                namahari = "JUMAT"
                            Case 6
                                namahari = "SABTU"
                            Case Else
                                namahari = "MINGGU"
                        End Select

                        orow("noid") = 0
                        orow("nama_shift") = namashift
                        orow("nama_periode") = namahari
                        orow("kd_jam") = ""
                        orow("jammasuk") = "00:00"
                        orow("jampulang") = "00:00"
                        orow("num_shift") = num_s

                        dv2.EndInit()

                    Next
                Next
            Else

                Dim num_s As Integer = 0

                For i As Integer = 1 To Integer.Parse(periode)
                    For x As Integer = 1 To 31

                        num_s = num_s + 1

                        Dim orow As DataRowView = dv2.AddNew

                        Dim namahari As String = String.Format("Hari {0}", x)

                        orow("noid") = 0
                        orow("nama_shift") = namashift
                        orow("nama_periode") = namahari
                        orow("kd_jam") = ""
                        orow("jammasuk") = "00:00"
                        orow("jampulang") = "00:00"
                        orow("num_shift") = num_s

                        dv2.EndInit()

                    Next
                Next

            End If

            simpan_detail()

        End If

    End Sub

    Private Sub simpan_detail()

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            sqltrans = cn.BeginTransaction

            Dim nama_shift As String = dv1(Me.BindingContext(dv1).Position)("nama_shift").ToString

            For i As Integer = 0 To dv2.Count - 1

                Dim noid As Integer = Integer.Parse(dv2(i)("noid").ToString)
                Dim namaperiode As String = dv2(i)("nama_periode").ToString
                Dim kdjam As String = dv2(i)("kd_jam").ToString

                Dim sql As String = ""
                If noid = 0 Then
                    sql = String.Format("insert into ms_shift2 (nama_shift,nama_periode,num_shift,kd_jam) values('{0}','{1}','{2}','{3}')", nama_shift, namaperiode, i + 1, kdjam)
                Else
                    sql = String.Format("update ms_shift2 set kd_jam='{0}' where noid={1}", kdjam, noid)
                End If

                Using cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                    cmd.ExecuteNonQuery()
                End Using

            Next

            sqltrans.Commit()

        Catch ex As Exception

            If Not IsNothing(sqltrans) Then
                sqltrans.Rollback()
            End If

            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

    End Sub

    Private Sub GridView1_Click(sender As Object, e As EventArgs) Handles GridView1.Click
        load_detail()
    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged

        If statproses Then
            Return
        End If

        load_detail()
    End Sub

    Private Sub GridView1_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles GridView1.SelectionChanged
        load_detail()
    End Sub

    Private Sub fshift_Load(sender As Object, e As EventArgs) Handles Me.Load
        load_jamkerja()
        load_grid()
    End Sub

    Private Sub btadd_Click(sender As Object, e As EventArgs) Handles btadd.Click

        statproses = True

        Using fkar2 As New fshift2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1}
            fkar2.ShowDialog()
        End Using

        statproses = False

    End Sub

    Private Sub btad2_Click(sender As Object, e As EventArgs) Handles btad2.Click

        If dv1.Count <= 0 Then
            Return
        End If

        Using fkar2 As New fshift3 With {.StartPosition = FormStartPosition.CenterParent, .dv_periode = dv2}
            fkar2.ShowDialog()

            simpan_detail()

        End Using

    End Sub

    Private Sub btdel_Click(sender As Object, e As EventArgs) Handles btdel.Click

        If dv1.Count <= 0 Then
            Return
        End If

        open_wait()

        statproses = True

        Dim namashift As String = dv1(Me.BindingContext(dv1).Position)("nama_shift").ToString

        Dim sql As String = String.Format("delete from ms_shift where nama_shift='{0}'", namashift)
        Dim sql2 As String = String.Format("delete from ms_shift2 where nama_shift='{0}'", namashift)

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            sqltrans = cn.BeginTransaction

            Using cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd.ExecuteNonQuery()
            End Using

            Using cmd1 As OleDbCommand = New OleDbCommand(sql2, cn, sqltrans)
                cmd1.ExecuteNonQuery()
            End Using

            dv1.Delete(Me.BindingContext(dv1).Position)

            sqltrans.Commit()

            close_wait()
            statproses = False


        Catch ex As Exception

            statproses = False

            close_wait()

            If Not IsNothing(sqltrans) Then
                sqltrans.Rollback()
            End If

            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

    End Sub

    Private Sub GridView2_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView2.CellValueChanged

        If dv2.Count <= 0 Then
            Return
        End If

        If e.Column.FieldName.Equals("kd_jam") Then

            Dim rowv As DataRow() = dtjam_kerja.Select(String.Format("kode='{0}'", e.Value))
            If rowv.Length > 0 Then
                dv2(Me.BindingContext(dv2).Position)("jammasuk") = rowv(0)("jammasuk").ToString
                dv2(Me.BindingContext(dv2).Position)("jampulang") = rowv(0)("jampulang").ToString

                Dim cn As OleDbConnection = Nothing
                Try

                    cn = New OleDbConnection
                    cn = Clsmy.open_conn

                    Dim sql As String = String.Format("update ms_shift2 set kd_jam='{0}' where noid={1}", e.Value, dv2(Me.BindingContext(dv2).Position)("noid").ToString)
                    Using cmd As OleDbCommand = New OleDbCommand(sql, cn)
                        cmd.ExecuteNonQuery()
                    End Using

                Catch ex As Exception
                    MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
                Finally

                    If Not cn Is Nothing Then
                        If cn.State = ConnectionState.Open Then
                            cn.Close()
                        End If
                    End If
                End Try

            End If

        End If

    End Sub

    Private Sub btdel2_Click(sender As Object, e As EventArgs) Handles btdel2.Click

        Dim cn As OleDbConnection = Nothing
        Try

            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            For i As Integer = 0 To dv2.Count - 1

                Dim sql As String = String.Format("update ms_shift2 set kd_jam='' where noid={0}", dv2(i)("noid").ToString)
                Using cmd As OleDbCommand = New OleDbCommand(sql, cn)
                    cmd.ExecuteNonQuery()
                End Using

                dv2(i)("kd_jam") = ""
                dv2(i)("jammasuk") = "00:00"
                dv2(i)("jampulang") = "00:00"

            Next

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

End Class