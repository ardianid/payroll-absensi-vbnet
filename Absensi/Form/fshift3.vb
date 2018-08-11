Imports System.Data.OleDb

Public Class fshift3

    Dim dtjam As DataTable

    Private dvmanager As Data.DataViewManager
    Private dv As Data.DataView

    Public dv_periode As DataView

    Private Sub load_jamkerja()

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select kode,nama,LEFT (CONVERT (VARCHAR, CONVERT (time(0), jammasuk), 108), 5) as jammasuk, " & _
            "LEFT (CONVERT (VARCHAR, CONVERT (time(0), jampulang), 108), 5) as jampulang from ms_jamkerja")

            Dim dsjam As DataSet = New DataSet
            dsjam = Clsmy.GetDataSet(sql, cn)

            dtjam = dsjam.Tables(0)

            tjamkerja.Properties.DataSource = dtjam

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

    Private Sub loadgrid()

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = "select 0 as pakai,nama_periode from ms_shift2 where nama_shift='dianaja'"

            Dim ds As New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager = New DataViewManager(ds)
            dv = dvmanager.CreateDataView(ds.Tables(0))

            grid1.DataSource = dv

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

        isi_grid()

    End Sub

    Private Sub isi_grid()

        For i As Integer = 0 To dv_periode.Count - 1

            Dim orow As DataRowView = dv.AddNew

            orow("nama_periode") = dv_periode(i)("nama_periode").ToString
            orow("pakai") = 0

            dv.EndInit()

        Next

    End Sub

    Private Sub btselesai_Click(sender As Object, e As EventArgs) Handles btselesai.Click
        Me.Close()
    End Sub

    Private Sub fshift3_Load(sender As Object, e As EventArgs) Handles Me.Load
        load_jamkerja()
        loadgrid()
    End Sub

    Private Sub tjamkerja_EditValueChanged(sender As Object, e As EventArgs) Handles tjamkerja.EditValueChanged
        tmasuk.EditValue = dtjam(tjamkerja.ItemIndex)("jammasuk").ToString
        tpulang.EditValue = dtjam(tjamkerja.ItemIndex)("jampulang").ToString
    End Sub

    Private Sub cbcekk_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbcekk.SelectedIndexChanged

        If dv.Count <= 0 Then
            Return
        End If

        If cbcekk.SelectedIndex = 0 Then
            For i As Integer = 0 To dv.Count - 1
                dv(i)("pakai") = 1
            Next
        Else
            For i As Integer = 0 To dv.Count - 1
                dv(i)("pakai") = 0
            Next
        End If

    End Sub

    Private Sub btsimpan_Click(sender As Object, e As EventArgs) Handles btsimpan.Click

        For i As Integer = 0 To dv.Count - 1
            If dv(i)("pakai") = 1 Then
                dv_periode(i)("jammasuk") = tmasuk.EditValue
                dv_periode(i)("jampulang") = tpulang.EditValue
                dv_periode(i)("kd_jam") = tjamkerja.EditValue
            End If
        Next

        Me.Close()

    End Sub

End Class