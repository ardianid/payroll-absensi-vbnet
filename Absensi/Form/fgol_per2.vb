Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Public Class fgol_per2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private bs1 As BindingSource
    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private Sub kosongkan()
        tkode.Text = ""
        tnama.Text = ""
        
        ttgl.EditValue = Date.Now
        ttgl1.EditValue = Date.Now
        ttgl2.EditValue = Date.Now

    End Sub

    Private Sub isi()
        tkode.Text = dv(position)("kode").ToString
        tnama.Text = dv(position)("ket").ToString
        ttgl.EditValue = DateValue(dv(position)("tanggal").ToString)
        ttgl1.EditValue = DateValue(dv(position)("tanggal1").ToString)
        ttgl2.EditValue = DateValue(dv(position)("tanggal2").ToString)
        cbgol.EditValue = dv(position)("kd_gol").ToString
    End Sub

    Private Sub opendata()

        Dim sql As String = String.Format("select a.kode,a.nip,b.nama from	ms_gol_tt2 a inner join ms_karyawan b on a.nip=b.nip " & _
             "where a.kode='{0}'", tkode.Text.Trim)
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


            grid1.DataSource = dv1

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

    Private Sub simpan()

        Dim cn As OleDbConnection = Nothing
        open_wait()

        Try


            Dim sqlinsert As String = String.Format("insert into ms_gol_tt (kode,ket,tanggal,tanggal1,tanggal2,kd_gol) values('{0}','{1}','{2}','{3}','{4}','{5}')", tkode.Text.Trim, tnama.Text.Trim, _
                                             convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue), cbgol.EditValue)
            Dim sqlupdate As String = String.Format("update ms_gol_tt set ket='{0}',tanggal='{1}',tanggal1='{2}',tanggal2='{3}',kd_gol='{4}' where kode='{5}'", tnama.Text.Trim, _
                                             convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue), cbgol.EditValue, tkode.Text.Trim)

            Dim comd As OleDbCommand


            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction

            If addstat = True Then

                comd = New OleDbCommand(sqlinsert, cn, sqltrans)
                comd.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btgol_per", 1, 0, 0, 0, tkode.Text.Trim, tnama.Text.Trim, sqltrans)

                insertview()

            Else

                comd = New OleDbCommand(sqlupdate, cn, sqltrans)
                comd.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btgol_per", 0, 1, 0, 0, tkode.Text.Trim, tnama.Text.Trim, sqltrans)

                updateview()

            End If

            simpan2(cn, sqltrans)

            sqltrans.Commit()

            MsgBox("Data disimpan...")

            If addstat = True Then
                kosongkan()
                tkode.Focus()
            Else
                Me.Close()
            End If

            close_wait()

            Me.Close()

        Catch ex As Exception
            close_wait()
            MsgBox(ex.ToString)
        Finally
            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

    End Sub

    Private Sub simpan2(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction)

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        For i As Integer = 0 To dv1.Count - 1

            Dim sql As String = String.Format("insert into ms_gol_tt2 (kode,nip) values('{0}','{1}')", tkode.Text.Trim, dv1(i)("nip").ToString)
            Dim sqlse As String = String.Format("select nip from ms_gol_tt2 where kode='{0}' and nip='{1}'", tkode.Text.Trim, dv1(i)("nip").ToString)

            Dim comd2 As OleDbCommand = New OleDbCommand(sqlse, cn, sqltrans)
            Dim dre As OleDbDataReader = comd2.ExecuteReader

            If dre.HasRows Then
                If dre.Read Then
                    If dre(0).ToString.Length = 0 Then
                        Dim comd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                        comd.ExecuteReader()
                    End If

                Else
                    Dim comd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                    comd.ExecuteReader()
                End If
            Else
                Dim comd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                comd.ExecuteReader()
            End If




        Next

    End Sub

    Private Sub hapus()

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        Dim sql As String = String.Format("delete from ms_gol_tt2 where kode='{0}' and nip='{1}'", tkode.Text.Trim, dv1(Me.BindingContext(dv1).Position)("nip").ToString)

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction

            Dim comd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
            comd.ExecuteNonQuery()

            dv1.Delete(Me.BindingContext(dv1).Position)

            sqltrans.Commit()

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

    End Sub

    Private Sub isigolongan()

        Dim ds As DataSet
        Dim cn As OleDbConnection = Nothing

        Try

            Dim sql As String = "select kode,nama from ms_golongan"

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            ds = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim dt As DataTable = ds.Tables(0)

            cbgol.Properties.DataSource = Nothing
            cbgol.Properties.DataSource = dt


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

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew
        orow("kode") = tkode.Text.Trim
        orow("ket") = tnama.Text.Trim
        
        orow("tanggal") = ttgl.EditValue
        orow("tanggal1") = ttgl1.EditValue
        orow("tanggal2") = ttgl2.EditValue

        orow("kd_gol") = cbgol.EditValue
        orow("nama") = cbgol.Text.Trim

        dv.EndInit()

    End Sub

    Private Sub updateview()

        dv(position)("kode") = tkode.Text.Trim
        dv(position)("ket") = tnama.Text.Trim

        dv(position)("tanggal") = ttgl.EditValue
        dv(position)("tanggal1") = ttgl1.EditValue
        dv(position)("tanggal2") = ttgl2.EditValue

        dv(position)("kd_gol") = cbgol.EditValue
        dv(position)("nama") = cbgol.Text.Trim

    End Sub

    Private Sub btselesai_Click(sender As System.Object, e As System.EventArgs) Handles btselesai.Click
        Me.Close()
    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click

        If tkode.Text.Trim.Length = 0 Then
            MsgBox("Kode tidak boleh kosong...", vbOKOnly + vbInformation, "Informasi")
            tkode.Focus()
            Return
        End If

        If tnama.Text.Trim.Length = 0 Then
            MsgBox("Ket tidak boleh kosong...", vbOKOnly + vbInformation, "Informasi")
            tnama.Focus()
            Return
        End If

        If cbgol.EditValue = "" Then
            MsgBox("Golongan tidak boleh kosong...", vbOKOnly + vbInformation, "Informasi")
            cbgol.Focus()
            Return
        End If

        simpan()

    End Sub

    Private Sub fjamkerja2_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        If addstat = True Then
            tkode.Focus()
        Else
            ttgl1.Focus()
        End If
    End Sub

    Private Sub fjamkerja2_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        isigolongan()

        If addstat = True Then
            kosongkan()
            tkode.Enabled = True
        Else
            isi()
            tkode.Enabled = False
            ttgl.Enabled = False

        End If

        opendata()

    End Sub

    Private Sub btdel_Click(sender As System.Object, e As System.EventArgs) Handles btdel.Click
        hapus()
    End Sub

    Private Sub btadd_Click(sender As System.Object, e As System.EventArgs) Handles btadd.Click
        Using fjamker2 As New fjamkerja_oby_gol3 With {.StartPosition = FormStartPosition.CenterParent, .dv3 = dv1, .jenis = "golongan"}
            fjamker2.ShowDialog()
        End Using
    End Sub


End Class