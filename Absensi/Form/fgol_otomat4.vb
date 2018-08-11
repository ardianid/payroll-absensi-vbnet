Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Public Class fgol_otomat4

    Public dv As DataView
    Private addaja As Boolean

    Public ReadOnly Property get_statada As String
        Get

            Return addaja

        End Get
    End Property

    Private Sub tambah()

        dv.Sort = "kode2"
        Dim hsil As Integer = dv.Find(tkode.Text.Trim)

        If hsil = -1 Then

            Dim cn As OleDbConnection = Nothing
            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Try

                Dim sql As String = String.Format("select kode2 from ms_golongan1 where kode2='{0}'", tkode.Text.Trim)
                Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim drd As OleDbDataReader = cmd.ExecuteReader

                Dim ada As Boolean = False

                If drd.Read Then
                    If Not (drd(0).ToString.Equals("")) Then
                        ada = True
                    End If
                End If
                drd.Close()

                If ada = True Then
                    MsgBox("Data sudah ada...", vbOKOnly + vbInformation, "Informasi")
                    tkode.Focus()
                    Return
                End If

            Catch ex As Exception
                MsgBox(ex.ToString)
            Finally

                If Not cn Is Nothing Then
                    If cn.State = ConnectionState.Open Then
                        cn.Close()
                    End If
                End If
            End Try


            Dim orow As DataRowView = dv.AddNew
            orow("id") = 0
            orow("kode2") = tkode.Text.Trim
            orow("nama") = tnama.Text.Trim

            dv.EndInit()

            tkode.Text = ""
            tnama.Text = ""

            tkode.Focus()

            addaja = True

        Else
            MsgBox("Data sudah ada...", vbOKOnly + vbInformation, "Informasi")
        End If


    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub bttambah_Click(sender As Object, e As System.EventArgs) Handles bttambah.Click

        If tkode.Text.Trim.Length = 0 Then
            MsgBox("Kode tidak boleh kosong...", vbOKOnly + vbInformation, "Informasi")
            tkode.Focus()
            Return
        End If

        If tnama.Text.Trim.Length = 0 Then
            MsgBox("Nama tidak boleh kosong...", vbOKOnly + vbInformation, "Informasi")
            tnama.Focus()
            Return
        End If

        tambah()

    End Sub

    Private Sub fgol_otomat4_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated

        addaja = False

        tkode.Focus()
    End Sub

End Class