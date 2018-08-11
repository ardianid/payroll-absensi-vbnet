Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Public Class fkaryawan4

    Public dvjam As DataView

    Private Sub loadjamkerja()
        Dim cn As OleDbConnection = Nothing
        Dim sql As String = "select * from ms_jamkerja"

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            cbjamkerja.Properties.DataSource = ds.Tables(0)

            '  cbdepart.ItemIndex = 1

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

    Private Sub simpan()
        Dim orow As DataRowView = dvjam.AddNew
        orow("id") = 0
        orow("kode") = cbjamkerja.EditValue
        orow("nama") = cbjamkerja.Text.Trim
        dvjam.EndInit()
    End Sub

    Private Sub fkaryawan4_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        cbjamkerja.Focus()
    End Sub

    Private Sub fkaryawan4_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        loadjamkerja()
    End Sub


    Private Sub btselesai_Click(sender As System.Object, e As System.EventArgs) Handles btselesai.Click
        Me.Close()
    End Sub

    Private Sub btok_Click(sender As System.Object, e As System.EventArgs) Handles btok.Click
        simpan()

    End Sub

    Private Sub cbhari_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles cbjamkerja.KeyDown
        If e.KeyCode = 13 Then
            simpan()
        End If
    End Sub

End Class