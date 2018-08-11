Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Public Class fdepart2

    Public addstat As Boolean
    Public dv1 As DataView
    Public position As Integer

    Private Sub simpan()

        Dim cn As OleDbConnection = Nothing
        Dim comd As OleDbCommand
        Dim comdcari As OleDbCommand
        Dim dread As OleDbDataReader

        Try

            open_wait()

            Dim sql As String = ""
            Dim sqlcari As String = String.Format("select * from ms_depart where nama='{0}'", tnama.Text.Trim.ToUpper)

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction()

            comdcari = New OleDbCommand(sqlcari, cn, sqltrans)
            dread = comdcari.ExecuteReader

            If dread.HasRows Then

                If dread.Read Then
                    If dread(0).ToString.Length > 0 Then
                        close_wait()
                        MsgBox("Departemen sudah ada...", vbOKOnly + vbInformation, "Informasi")
                        tnama.Focus()
                        Exit Sub
                        'Else

                        '    sql = String.Format("update ms_depart set nama='{0}' where nam", tnama.Text.Trim.ToUpper)
                        '    comd = New OleDbCommand(sql, cn, sqltrans)
                        '    comd.ExecuteNonQuery()

                        '    dv1(position)("nama") = tnama.Text.Trim.ToUpper

                    End If
                End If

            Else
                sql = String.Format("insert into ms_depart (nama) values('{0}')", tnama.Text.Trim.ToUpper)
                comd = New OleDbCommand(sql, cn, sqltrans)
                comd.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btdepart", 1, 0, 0, 0, tnama.Text.Trim.ToUpper, "", sqltrans)

                Dim orow As DataRowView = dv1.AddNew
                orow("nama") = tnama.Text.Trim.ToUpper
                dv1.EndInit()

            End If

            sqltrans.Commit()

            close_wait()

            MsgBox("Data disimpan...", vbOKOnly + vbInformation, "Informasi")

            tnama.Text = ""
            tnama.Focus()

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

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click

        If tnama.Text.Trim.Length = 0 Then
            MsgBox("Nama Departemen tidak boleh kosong...", vbOKOnly + vbInformation, "Informasi")
            tnama.Focus()
            Exit Sub
        End If

        simpan()

    End Sub

    Private Sub tnama_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tnama.KeyDown
        If e.KeyCode = 13 Then
            btsimpan_Click(sender, Nothing)
        End If
    End Sub

    Private Sub fdepart2_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        tnama.Focus()
    End Sub

    Private Sub btkeluar_Click(sender As System.Object, e As System.EventArgs) Handles btkeluar.Click
        Me.Close()
    End Sub
End Class