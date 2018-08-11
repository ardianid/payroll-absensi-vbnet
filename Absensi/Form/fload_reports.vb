Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy


Public Class fload_reports

    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView


    Private Sub loaddata()

        Dim sql As String = String.Format("select b1.namaform,b1.namamenu,b1.keterangan,b1.indialog,b1.namagroup from ms_usersys3 a1 inner join ms_menu2 b1 on a1.kodemenu=b1.kodemenu " & _
                "where b1.inform=0 and a1.t_lap=1 and a1.namauser='{0}'", userprog)

        Dim cn As OleDbConnection = Nothing

        grid1.DataSource = Nothing

        Try

            '  open_wait()

            dv1 = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet
            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager = New DataViewManager(ds)
            dv1 = dvmanager.CreateDataView(ds.Tables(0))

            grid1.DataSource = dv1

            '  close_wait()

        Catch ex As OleDb.OleDbException
            ' close_wait()
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally


            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

        End Try

    End Sub

    Private Sub fload_reports_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        loaddata()
    End Sub

    Private Sub SimpleButton2_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton2.Click
        Me.Close()
    End Sub

    Private Sub SimpleButton1_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton1.Click

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        Dim indialog As String = dv1(Me.BindingContext(dv1).Position)("indialog").ToString

        Dim sform As String = dv1(Me.BindingContext(dv1).Position)("namaform").ToString
        Dim frm As Form = ObjectFinder.CreateForm(sform)

        If indialog.Equals("1") Then
            frm.StartPosition = FormStartPosition.CenterScreen
            frm.ShowDialog(Me)
        Else
            futama.LoadOtherForm(frm)
        End If

        
    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As System.EventArgs) Handles GridView1.DoubleClick
        SimpleButton1_Click(sender, Nothing)
    End Sub

    Private Sub grid1_Click(sender As System.Object, e As System.EventArgs) Handles grid1.Click

    End Sub
End Class