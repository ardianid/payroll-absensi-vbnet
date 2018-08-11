Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Public Class fjamkerja_oby_gol3

    Public dv3 As DataView
    Public jenis As String

    Private Sub addto()



        Dim sql As String = String.Format("select a.nip,a.nama,b.nama as golongan from ms_karyawan a inner join ms_golongan b on a.kdgol=b.kode where a.aktif=1 and a.kdgol='{0}'", cbgol.EditValue)
        Dim sql2 As String = String.Format("select a.nip,a.nama,b.nama as golongan from ms_karyawan a inner join ms_golongan b on a.kdgol=b.kode where a.aktif=1 and a.nip='{0}'", cbkary.EditValue)
        Dim sql3 As String = String.Format("select a.nip,a.nama,b.nama as golongan from ms_karyawan a inner join ms_golongan b on a.kdgol=b.kode where a.aktif=1 and a.depart='{0}'", cbdepart.EditValue)
        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet = New DataSet()

            If cbjenis.SelectedIndex = 0 Then
                ds = Clsmy.GetDataSet(sql, cn)
            ElseIf cbjenis.SelectedIndex = 1 Then
                ds = Clsmy.GetDataSet(sql3, cn)
            Else
                ds = Clsmy.GetDataSet(sql2, cn)
            End If

            Dim dt As New DataTable
            dt = ds.Tables(0)


            For i As Integer = 0 To dt.Rows.Count - 1

                Dim orow As DataRowView = dv3.AddNew

                orow("nip") = dt.Rows(i)("nip").ToString
                orow("nama") = dt.Rows(i)("nama").ToString

                If jenis.Equals("jamkerja") Then
                    orow("golongan") = dt.Rows(i)("golongan").ToString
                End If


                dv3.EndInit()
            Next






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

    Private Sub isikary()

        Dim ds As DataSet
        Dim cn As OleDbConnection = Nothing

        Try

            Dim sql As String = "select a.nip,a.nama,b.nama as golongan from ms_karyawan a inner join ms_golongan b on a.kdgol=b.kode where a.aktif=1"

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            ds = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim dt As DataTable = ds.Tables(0)

            cbkary.Properties.DataSource = Nothing
            cbkary.Properties.DataSource = dt


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

    Private Sub isidepart()

        Dim ds As DataSet
        Dim cn As OleDbConnection = Nothing

        Try

            Dim sql As String = "select * from ms_depart"

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            ds = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim dt As DataTable = ds.Tables(0)

            cbdepart.Properties.DataSource = Nothing
            cbdepart.Properties.DataSource = dt


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

    Private Sub fjamkerja_oby_gol3_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        cbjenis.Focus()
    End Sub

    Private Sub fjamkerja_oby_gol3_Load(sender As Object, e As System.EventArgs) Handles Me.Load


        If jenis.Equals("jamkerja") Then
            Me.Text = "Pegawai (Jam Kerja Diluar Standar)"
        Else
            Me.Text = "Pegawai (Golongan Per-Periode"
        End If

        isikary()
        isigolongan()
        isidepart()

        cbjenis.SelectedIndex = 0

    End Sub

    Private Sub cbjenis_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbjenis.SelectedIndexChanged
        If cbjenis.SelectedIndex = 0 Then
            cbgol.Visible = True
            cbkary.Visible = False
            cbdepart.Visible = False
        ElseIf cbjenis.SelectedIndex = 1 Then
            cbdepart.Visible = True
            cbgol.Visible = False
            cbkary.Visible = False
        Else
            cbdepart.Visible = False
            cbgol.Visible = False
            cbkary.Visible = True
        End If
    End Sub

    Private Sub cbgol_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles cbgol.KeyDown, cbkary.KeyDown
        If e.KeyCode = 13 Then
            addto()
        End If
    End Sub

 
    Private Sub btok_Click(sender As System.Object, e As System.EventArgs) Handles btok.Click
        addto()
    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

End Class