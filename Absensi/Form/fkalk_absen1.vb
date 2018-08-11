Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Public Class fkalk_absen1

    Public iddia As Integer
    Public nama As String
    Public tanggal_shift As String
    Public nama_shift As String
    Public kodegol As String

    Private dt1 As DataTable
    Private totaldapat As Double = 0
    Private okdata As Boolean = False

    Public ReadOnly Property get_total As String
        Get

            If IsNothing(dt1) Then
                Return 0
                Exit Property
            End If

            If dt1.Rows.Count <= 0 Then
                Return 0
                Exit Property
            End If

            Return totaldapat

        End Get
    End Property

    Public ReadOnly Property get_statdata As String
        Get
            Return okdata
        End Get
    End Property

    Private Sub load_grid()

        Dim sql As String = String.Format("select kode2,nama,0 as jmlhasil,0.0 as price, 0.0 as tothasil,0.0 as total,0 as tambahan,'' as ket from ms_golongan1 where saktif=1 and kode='{0}' order by kode2 ", kodegol)

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            dt1 = ds.Tables(0)

            grid1.DataSource = Nothing
            grid1.DataSource = dt1

            load_bydetail(cn)

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

    Private Sub load_bydetail(ByVal cn As OleDbConnection)

        For i As Integer = 0 To dt1.Rows.Count - 1

            Dim kode2 As String = dt1(i)("kode2").ToString

            Dim sql As String = String.Format("select * from tr_hadir2 where id2={0} and kode='{1}'", iddia, kode2)
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            Dim ada As Boolean = False
            totaldapat = 0


            If drd.Read Then
                If IsNumeric(drd("id").ToString) Then

                    ada = True

                    dt1(i)("jmlhasil") = Integer.Parse(drd("jmlhasil").ToString)
                    dt1(i)("price") = Double.Parse(drd("price").ToString)
                    dt1(i)("tothasil") = Double.Parse(drd("tothasil").ToString)
                    dt1(i)("tambahan") = Double.Parse(drd("tambahan").ToString)
                    dt1(i)("total") = Double.Parse(drd("total").ToString)
                    dt1(i)("ket") = drd("ket").ToString

                    totaldapat = totaldapat + Double.Parse(drd("total").ToString)

                End If
            End If
            drd.Close()

            If ada = False Then
                dt1(i)("jmlhasil") = 0
                dt1(i)("price") = 0
                dt1(i)("tothasil") = 0
                dt1(i)("tambahan") = 0
                dt1(i)("total") = 0
                dt1(i)("ket") = ""
            End If

        Next

    End Sub

    Private Sub fkalk_absen1_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        GridView1.FocusedRowHandle = 0
    End Sub

    Private Sub fkalk_absen1_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        tnama.Text = nama
        ttanggal.Text = convert_date_to_ind(tanggal_shift)
        tshift.Text = nama_shift

        load_grid()

    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged

        If e.Column.FieldName.Equals("jmlhasil") Then

            If Not IsNumeric(e.Value) Then
                Return
            End If

            Dim cn As OleDbConnection = Nothing

            Dim kode2 As String = dt1(Me.BindingContext(dt1).Position)("kode2").ToString
            Dim jml As Integer = e.Value

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim sql As String = String.Format("select * from ms_golongan2 where kode='{0}' and jmin<={1} and jmax>={1}", kode2, jml)
                Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim drd As OleDbDataReader = cmd.ExecuteReader

                Dim hasilper As Double = 0
                Dim price As Double = 0

                If drd.Read Then
                    If IsNumeric(drd("id").ToString) Then

                        price = Double.Parse(drd("price").ToString)
                        Dim perkalian As Integer = Integer.Parse(drd("perkalian").ToString)

                        If perkalian = 1 Then
                            hasilper = price * jml
                        Else
                            hasilper = price
                        End If

                    End If
                End If
                drd.Close()

                Dim tambahan As Double = Double.Parse(dt1(Me.BindingContext(dt1).Position)("tambahan").ToString)
                Dim total As Double = tambahan + hasilper
                '    total = Math.Ceiling(total)

                dt1(Me.BindingContext(dt1).Position)("price") = price
                dt1(Me.BindingContext(dt1).Position)("tothasil") = hasilper
                dt1(Me.BindingContext(dt1).Position)("total") = total
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
            Finally

                If Not cn Is Nothing Then
                    If cn.State = ConnectionState.Open Then
                        cn.Close()
                    End If
                End If
            End Try


        ElseIf e.Column.FieldName.Equals("price") Then

            If Not IsNumeric(e.Value) Then
                Return
            End If

            Dim jml As Integer = Integer.Parse(dt1(Me.BindingContext(dt1).Position)("jmlhasil"))
            Dim price As Double = e.Value

            Dim hasil As Double = jml * price

            Dim tambahan As Double = Double.Parse(dt1(Me.BindingContext(dt1).Position)("tambahan").ToString)
            Dim total As Double = tambahan + hasil

            dt1(Me.BindingContext(dt1).Position)("tothasil") = hasil
            dt1(Me.BindingContext(dt1).Position)("total") = Math.Ceiling(total)

        ElseIf e.Column.FieldName.Equals("tambahan") Then

            Dim tambahan As Double = e.Value
            Dim tothasil As Double = Double.Parse(dt1(Me.BindingContext(dt1).Position)("tothasil"))

            Dim total As Double = tambahan + tothasil

            dt1(Me.BindingContext(dt1).Position)("total") = Math.Ceiling(total)

        End If

    End Sub

    Private Sub simpan()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction

            Dim sqldel As String = String.Format("delete from tr_hadir2 where id2={0}", iddia)
            Using cmddel As OleDbCommand = New OleDbCommand(sqldel, cn, sqltrans)
                cmddel.ExecuteNonQuery()
            End Using

            totaldapat = 0

            For i As Integer = 0 To dt1.Rows.Count - 1

                Dim kode2 As String = dt1(i)("kode2").ToString
                Dim namaborongan As String = dt1(i)("nama").ToString
                Dim jmlhasil As Integer
                If IsNumeric(dt1(i)("jmlhasil").ToString) Then
                    jmlhasil = Integer.Parse(dt1(i)("jmlhasil").ToString)
                Else
                    jmlhasil = 0
                End If

                Dim price As Double
                If IsNumeric(dt1(i)("price").ToString) Then
                    price = Double.Parse(dt1(i)("price").ToString)
                Else
                    price = 0
                End If

                Dim tothasil As Double
                If IsNumeric(dt1(i)("tothasil").ToString) Then
                    tothasil = Double.Parse(dt1(i)("tothasil").ToString)
                Else
                    tothasil = 0
                End If

                Dim tambahan As Double
                If IsNumeric(dt1(i)("tambahan").ToString) Then
                    tambahan = Double.Parse(dt1(i)("tambahan").ToString)
                Else
                    tambahan = 0
                End If

                Dim total As Double
                If IsNumeric(dt1(i)("total").ToString) Then
                    total = Double.Parse(dt1(i)("total").ToString)
                Else
                    total = 0
                End If

                Dim keterangan As String = dt1(i)("ket").ToString

                totaldapat = totaldapat + total

                If total > 0 Then

                    Dim sqlins As String = String.Format("insert into tr_hadir2 (id2,kode,namaborongan,jmlhasil,price,tothasil,tambahan,total,ket) values({0},'{1}','{2}',{3},{4},{5},{6},{7},'{8}')", iddia, kode2, namaborongan, jmlhasil, Replace(price, ",", "."), Replace(tothasil, ",", "."), Replace(tambahan, ",", "."), Replace(total, ",", "."), keterangan)
                    Using cmdins As OleDbCommand = New OleDbCommand(sqlins, cn, sqltrans)
                        cmdins.ExecuteNonQuery()
                    End Using

                End If

            Next

            Dim sqlupdate As String = String.Format("update tr_hadir set jmlhasil=0,hasilper=0,tothasil={0} where id={1}", Replace(totaldapat, ",", "."), iddia)
            Using cmd_upd As OleDbCommand = New OleDbCommand(sqlupdate, cn, sqltrans)
                cmd_upd.ExecuteNonQuery()
            End Using

            sqltrans.Commit()

            okdata = True
            Me.Close()

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

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        okdata = False
        Me.Close()
    End Sub

    Private Sub btok_Click(sender As System.Object, e As System.EventArgs) Handles btok.Click

        'If MsgBox("Yakin sudah benar ?", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.No Then
        '    Return
        'End If

        simpan()

    End Sub

    Private Sub grid1_ProcessGridKey(sender As Object, e As KeyEventArgs) Handles grid1.ProcessGridKey

        If GridView1.IsGroupRow(GridView1.FocusedRowHandle) Then Exit Sub

        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            btok_Click(sender, Nothing)
        ElseIf e.KeyCode = Keys.Escape Then
            btclose_Click(sender, Nothing)
        End If

    End Sub


End Class