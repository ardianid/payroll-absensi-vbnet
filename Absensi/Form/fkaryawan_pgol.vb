Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Public Class fkaryawan_pgol

    Public snip, snama, sidmes, sgol, sjab, sdepart As String
    Public kdgol As String
    Private statok As Boolean = False

    Public ReadOnly Property get_statok As String
        Get

            Return statok

        End Get
    End Property

    Public ReadOnly Property get_Nip As String
        Get

            If statok = False Then
                Return ""
                Exit Property
            End If

            Return tnip_n.EditValue

        End Get
    End Property

    Public ReadOnly Property get_IDMesin As String
        Get

            If statok = False Then
                Return ""
                Exit Property
            End If

            Return tidmes_n.EditValue

        End Get
    End Property

    Public ReadOnly Property get_KDGolongan As String
        Get

            If statok = False Then
                Return ""
                Exit Property
            End If

            Return cbgol.EditValue

        End Get
    End Property

    Public ReadOnly Property get_NAMAGolongan As String
        Get

            If statok = False Then
                Return ""
                Exit Property
            End If

            Return cbgol.Text.Trim

        End Get
    End Property

    Public ReadOnly Property get_Depart As String
        Get

            If statok = False Then
                Return ""
                Exit Property
            End If

            Return cbdepart.EditValue

        End Get
    End Property

    Public ReadOnly Property get_jabatan As String
        Get

            If statok = False Then
                Return ""
                Exit Property
            End If

            Return tjab_n.EditValue

        End Get
    End Property



    Private Sub loaddepart()
        Dim cn As OleDbConnection = Nothing
        Dim sql As String = "select * from ms_depart"

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            cbdepart.Properties.DataSource = ds.Tables(0)

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

    Private Sub loadgolongan()
        Dim cn As OleDbConnection = Nothing
        Const sql As String = "select kode,nama,harian,jenisgaji from ms_golongan where saktif=1"

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim dtgolongan As DataTable
            dtgolongan = ds.Tables(0)

            cbgol.Properties.DataSource = dtgolongan

            '   cbgol.ItemIndex = 1

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

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            If tnip.EditValue <> tnip_n.EditValue Then
                If cek_nip_idmesin("nip", cn) = True Then
                    MsgBox("NIP sudah dipakai karyawan lain...", vbOKOnly + vbInformation, "Informasi")
                    tnip_n.Focus()
                    Return
                End If
            End If

            If tidmes.EditValue <> tidmes_n.EditValue Then
                If cek_nip_idmesin("idmesin", cn) = True Then
                    MsgBox("ID Mesin sudah dipakai karyawan lain...", vbOKOnly + vbInformation, "Informasi")
                    tidmes_n.Focus()
                    Return
                End If
            End If

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction

            '1. update gaji
            Dim sqlgaji As String = String.Format("update tr_gaji set nip='{0}' where nip='{1}'", tnip_n.EditValue, tnip.EditValue)
            Using cmdgaji As OleDbCommand = New OleDbCommand(sqlgaji, cn, sqltrans)
                cmdgaji.ExecuteNonQuery()
            End Using

            '2. update kehadiran
            Dim sqlhadir As String = String.Format("update tr_hadir set nip='{0}' where nip='{1}'", tnip_n.EditValue, tnip.EditValue)
            Using cmdhadir As OleDbCommand = New OleDbCommand(sqlhadir, cn, sqltrans)
                cmdhadir.ExecuteNonQuery()
            End Using

            '3. update absen manual
            Dim sqlabsen_manual As String = String.Format("update ms_absenman set nip='{0}' where nip='{1}'", tnip_n.EditValue, tnip.EditValue)
            Using cmdabsen_manual As OleDbCommand = New OleDbCommand(sqlabsen_manual, cn, sqltrans)
                cmdabsen_manual.ExecuteNonQuery()
            End Using

            '4. update angsuran
            Dim sql_ansuran As String = String.Format("update ms_angsur set nip='{0}' where nip='{1}'", tnip_n.EditValue, tnip.EditValue)
            Using cmdangsuran As OleDbCommand = New OleDbCommand(sql_ansuran, cn, sqltrans)
                cmdangsuran.ExecuteNonQuery()
            End Using

            '5. update ms_bon
            Dim sqlbon As String = String.Format("update ms_bon set nip='{0}' where nip='{1}'", tnip_n.EditValue, tnip.EditValue)
            Using cmdbon As OleDbCommand = New OleDbCommand(sqlbon, cn, sqltrans)
                cmdbon.ExecuteNonQuery()
            End Using

            '6. update ms_goltt
            Dim sqlgol_tt As String = String.Format("update ms_gol_tt2 set nip='{0}' where nip='{1}'", tnip_n.EditValue, tnip.EditValue)
            Using cmdgol_tt As OleDbCommand = New OleDbCommand(sqlgol_tt, cn, sqltrans)
                cmdgol_tt.ExecuteNonQuery()
            End Using

            '7. update ms_inout
            Dim sqlmsinout As String = String.Format("update ms_inout set userid={0} where userid={1}", tidmes_n.EditValue, tidmes.EditValue)
            Using cmdinout As OleDbCommand = New OleDbCommand(sqlmsinout, cn, sqltrans)
                cmdinout.ExecuteNonQuery()
            End Using

            '8. update jakerja_lain
            Dim sqljamlain As String = String.Format("update ms_jamkerjalain2 set nip='{0}' where nip='{1}'", tnip_n.EditValue, tnip.EditValue)
            Using cmdjamlain As OleDbCommand = New OleDbCommand(sqljamlain, cn, sqltrans)
                cmdjamlain.ExecuteNonQuery()
            End Using

            '9. update karyawan
            Dim sqlkaryawan As String = String.Format("update ms_karyawan set nip='{0}',depart='{1}',kdgol='{2}',jabatan='{3}',idmesin={4} where nip='{5}'", tnip_n.EditValue, cbdepart.EditValue, cbgol.EditValue, tjab_n.EditValue, tidmes_n.EditValue, tnip.EditValue)
            Using cmdkary As OleDbCommand = New OleDbCommand(sqlkaryawan, cn, sqltrans)
                cmdkary.ExecuteNonQuery()
            End Using

            '10. update karyawan2
            Dim sqlkaryawan2 As String = String.Format("update ms_karyawan2 set nip='{0}' where nip='{1}'", tnip_n.EditValue, tnip.EditValue)
            Using cmdkary2 As OleDbCommand = New OleDbCommand(sqlkaryawan2, cn, sqltrans)
                cmdkary2.ExecuteNonQuery()
            End Using

            '11. update karyawan3
            Dim sqlkaryawan3 As String = String.Format("update ms_karyawan3 set nip='{0}' where nip='{1}'", tnip_n.EditValue, tnip.EditValue)
            Using cmdkary3 As OleDbCommand = New OleDbCommand(sqlkaryawan3, cn, sqltrans)
                cmdkary3.ExecuteNonQuery()
            End Using

            '12. update tinout
            Dim sqltinout As String = String.Format("update ms_tinout set nip='{0}' where nip='{1}'", tnip_n.EditValue, tnip.EditValue)
            Using cmdtinout As OleDbCommand = New OleDbCommand(sqltinout, cn, sqltrans)
                cmdtinout.ExecuteNonQuery()
            End Using

            '13. update iuran jamsos
            Dim sqliuran_jamsos As String = String.Format("update tr_iuran_jamsos set nip='{0}' where nip='{1}'", tnip_n.EditValue, tnip.EditValue)
            Using cmdiuran_jamsos As OleDbCommand = New OleDbCommand(sqliuran_jamsos, cn, sqltrans)
                cmdiuran_jamsos.ExecuteNonQuery()
            End Using


            sqltrans.Commit()
            statok = True
            MsgBox("Data dirubah..", vbOKOnly + vbInformation, "Informasi")

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

    Private Function cek_nip_idmesin(ByVal stat As String, ByVal cn As OleDbConnection) As Boolean

        Dim hasil As Boolean = False

        Dim sql As String = ""
        If stat.Equals("nip") Then
            sql = String.Format("select nip from ms_karyawan where nip='{0}'", tnip_n.EditValue)
        Else
            sql = String.Format("select idmesin from ms_karyawan where idmesin={0}", tnip_n.EditValue)
        End If

        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        If drd.Read Then
            If stat.Equals("nip") Then

                If Not drd(0).ToString.Equals("") Then
                    hasil = True
                End If

            Else

                If IsNumeric((drd(0).ToString)) Then
                    hasil = True
                End If

            End If
        End If
        drd.Close()

        Return hasil


    End Function

    Private Sub fkaryawan_pgol_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        tnip_n.Focus()
    End Sub

    Private Sub fkaryawan_pgol_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        loaddepart()
        loadgolongan()

        tnama.Text = snama
        tnip.Text = snip
        tidmes.Text = sidmes
        tgol.Text = sgol
        tdepart.Text = sdepart
        tjab.Text = sjab

        tnip_n.EditValue = tnip.EditValue
        tidmes_n.EditValue = tidmes.EditValue

        cbdepart.EditValue = tdepart.EditValue
        cbgol.EditValue = kdgol
        tjab_n.EditValue = tjab.EditValue


    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        statok = False
        Me.Close()
    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click

        statok = False

        If tnip_n.EditValue.ToString.Trim.Length = 0 Or tnip_n.EditValue = 0 Then
            MsgBox("NIP harus diisi..", vbOKOnly + vbInformation, "Informasi")
            tnip_n.Focus()
            Return
        End If

        If tidmes_n.EditValue = 0 Then
            MsgBox("ID Mesin harus diisi..", vbOKOnly + vbInformation, "Informasi")
            tidmes_n.Focus()
            Return
        End If

        If MsgBox("Yakin semua data yang dimasukkan sudah benar ?", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.Yes Then
            simpan()
        End If

    End Sub

End Class