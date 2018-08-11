Imports System.Data.OleDb
Public Class fkaryawan5

    Public dvshift As DataView
    Dim dtshift As DataTable

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

        'Dim rowshift As DataRow() = dtshift.Select(String.Format("kd_jam='{0}'", cbjamkerja.EditValue))

        'If rowshift.Length > 0 Then

        Dim orow As DataRowView = dvshift.AddNew
        orow("id") = 0
        orow("kd_jam") = cbjamkerja.EditValue
        orow("hit_jam") = thit.EditValue
        orow("tgl_mulai") = DateValue(ttgl.EditValue)
        orow("tgl_selesai") = DateValue(ttgl2.EditValue)
        dvshift.EndInit()

        ttgl.Focus()

        'End If

    End Sub

    Private Sub cek_hari()
        Dim cn As OleDbConnection = Nothing
        Dim sql As String = String.Format("select hari from ms_kalender where tanggal='{0}'", convert_date_to_eng(ttgl.EditValue))

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            Dim namahari As String = ""
            If drd.Read Then
                namahari = drd(0).ToString
            Else
                MsgBox("Kalender kosong")
                Me.Close()
            End If
            drd.Close()

            If Not (namahari = "SENIN") Then

                Dim sqlsenin As String = String.Format("select top 1 tanggal from ms_kalender where hari='SENIN' and tanggal<='{0}' order by tanggal desc", convert_date_to_eng(ttgl.EditValue))
                Dim cmdsenin As OleDbCommand = New OleDbCommand(sqlsenin, cn)
                Dim drdsenin As OleDbDataReader = cmdsenin.ExecuteReader

                If drdsenin.Read Then
                    ttgl.EditValue = DateValue(drdsenin(0).ToString)
                Else
                    MsgBox("Kalender Kosong")
                    Me.Close()
                End If
                drdsenin.Close()

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

    Private Sub fkaryawan4_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        ttgl.Focus()
    End Sub

    Private Sub fkaryawan4_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        ttgl.EditValue = DateValue(Date.Now)
        ttgl2.EditValue = DateValue(Date.Now)

        thit.EditValue = 7

        loadjamkerja()
    End Sub

    Private Sub btselesai_Click(sender As System.Object, e As System.EventArgs) Handles btselesai.Click
        Me.Close()
    End Sub

    Private Sub btok_Click(sender As System.Object, e As System.EventArgs) Handles btok.Click

        If Not (IsNumeric(thit.EditValue)) Then
            MsgBox("Hitungan jam kerja tidak boleh 0 atau kosong..", vbOKOnly + vbInformation, "Informasi")
            thit.Focus()
            Return
        End If

        If thit.EditValue = 0 Then
            MsgBox("Hitungan jam kerja tidak boleh 0 atau kosong..", vbOKOnly + vbInformation, "Informasi")
            thit.Focus()
            Return
        End If

        simpan()
    End Sub

    Private Sub cbhari_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = 13 Then
            simpan()
        End If
    End Sub

    Private Sub ttgl_Validated(sender As Object, e As EventArgs) Handles ttgl.Validated
        'cek_hari()
    End Sub

End Class