Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Public Class fkaryawan_nonakt

    Public snip, snama, sdepart As String
    Private statok As Boolean = False
    Private nipbaru As String

    Public ReadOnly Property get_Nip As String
        Get

            If statok = False Then
                Return ""
                Exit Property
            End If

            Return nipbaru

        End Get
    End Property

    Private Function cek_nipakhir(ByVal cn As OleDbConnection) As String

        Dim hasil As String = ""

        Dim nobukti As String = "N"
        nobukti = String.Format("{0}{1}", nobukti, Microsoft.VisualBasic.Right(Year(Date.Now), 2))

        Dim sql As String = String.Format("select MAX(nip) from ms_karyawan where aktif=0 and nip like '%{0}%'", nobukti)
        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        Dim noakhir As Integer = 0
        If drd.Read Then
            If Not drd(0).ToString.Equals("") Then
                noakhir = Integer.Parse(Microsoft.VisualBasic.Right(drd(0).ToString, 4))
            End If
        End If
        drd.Close()

        noakhir = noakhir + 1

        Dim noakhir1 As String = noakhir

        If Len(noakhir1) = 1 Then
            noakhir1 = String.Format("000{0}", noakhir1)
        ElseIf Len(noakhir1) = 2 Then
            noakhir1 = String.Format("00{0}", noakhir1)
        ElseIf Len(noakhir1) = 3 Then
            noakhir1 = String.Format("0{0}", noakhir1)
        End If

        hasil = String.Format("{0}{1}", nobukti, noakhir1)

        Return hasil

    End Function

    Private Sub simpan()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            nipbaru = cek_nipakhir(cn)
            Dim niplama As String = tnip.Text.Trim

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction

            '1. update gaji
            Dim sqlgaji As String = String.Format("update tr_gaji set nip='{0}' where nip='{1}'", nipbaru, niplama)
            Using cmdgaji As OleDbCommand = New OleDbCommand(sqlgaji, cn, sqltrans)
                cmdgaji.ExecuteNonQuery()
            End Using

            '2. update kehadiran
            Dim sqlhadir As String = String.Format("update tr_hadir set nip='{0}' where nip='{1}'", nipbaru, niplama)
            Using cmdhadir As OleDbCommand = New OleDbCommand(sqlhadir, cn, sqltrans)
                cmdhadir.ExecuteNonQuery()
            End Using

            '3. update absen manual
            Dim sqlabsen_manual As String = String.Format("update ms_absenman set nip='{0}' where nip='{1}'", nipbaru, niplama)
            Using cmdabsen_manual As OleDbCommand = New OleDbCommand(sqlabsen_manual, cn, sqltrans)
                cmdabsen_manual.ExecuteNonQuery()
            End Using

            '4. update angsuran
            Dim sql_ansuran As String = String.Format("update ms_angsur set nip='{0}' where nip='{1}'", nipbaru, niplama)
            Using cmdangsuran As OleDbCommand = New OleDbCommand(sql_ansuran, cn, sqltrans)
                cmdangsuran.ExecuteNonQuery()
            End Using

            '5. update ms_bon
            Dim sqlbon As String = String.Format("update ms_bon set nip='{0}' where nip='{1}'", nipbaru, niplama)
            Using cmdbon As OleDbCommand = New OleDbCommand(sqlbon, cn, sqltrans)
                cmdbon.ExecuteNonQuery()
            End Using

            '6. update ms_goltt
            Dim sqlgol_tt As String = String.Format("update ms_gol_tt2 set nip='{0}' where nip='{1}'", nipbaru, niplama)
            Using cmdgol_tt As OleDbCommand = New OleDbCommand(sqlgol_tt, cn, sqltrans)
                cmdgol_tt.ExecuteNonQuery()
            End Using

            '7. update ms_inout
            'Dim sqlmsinout As String = String.Format("update ms_inout set userid={0} where userid={1}", tidmes_n.EditValue, tidmes.EditValue)
            'Using cmdinout As OleDbCommand = New OleDbCommand(sqlmsinout, cn, sqltrans)
            '    cmdinout.ExecuteNonQuery()
            'End Using

            '8. update jakerja_lain
            Dim sqljamlain As String = String.Format("update ms_jamkerjalain2 set nip='{0}' where nip='{1}'", nipbaru, niplama)
            Using cmdjamlain As OleDbCommand = New OleDbCommand(sqljamlain, cn, sqltrans)
                cmdjamlain.ExecuteNonQuery()
            End Using

            '9. update karyawan
            Dim sqlkaryawan As String = String.Format("update ms_karyawan set nip='{0}',aktif=0,tgl_keluar='{1}',alasan_keluar='{2}' where nip='{3}'", nipbaru, convert_date_to_eng(ttgl.EditValue), talasan.Text.Trim, niplama)
            Using cmdkary As OleDbCommand = New OleDbCommand(sqlkaryawan, cn, sqltrans)
                cmdkary.ExecuteNonQuery()
            End Using

            '10. update karyawan2
            Dim sqlkaryawan2 As String = String.Format("update ms_karyawan2 set nip='{0}' where nip='{1}'", nipbaru, niplama)
            Using cmdkary2 As OleDbCommand = New OleDbCommand(sqlkaryawan2, cn, sqltrans)
                cmdkary2.ExecuteNonQuery()
            End Using

            '11. update karyawan3
            Dim sqlkaryawan3 As String = String.Format("update ms_karyawan3 set nip='{0}' where nip='{1}'", nipbaru, niplama)
            Using cmdkary3 As OleDbCommand = New OleDbCommand(sqlkaryawan3, cn, sqltrans)
                cmdkary3.ExecuteNonQuery()
            End Using

            '12. update tinout
            Dim sqltinout As String = String.Format("update ms_tinout set nip='{0}' where nip='{1}'", nipbaru, niplama)
            Using cmdtinout As OleDbCommand = New OleDbCommand(sqltinout, cn, sqltrans)
                cmdtinout.ExecuteNonQuery()
            End Using

            '13. update iuran jamsos
            Dim sqliuran_jamsos As String = String.Format("update tr_iuran_jamsos set nip='{0}' where nip='{1}'", nipbaru, niplama)
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

    Private Sub fkaryawan_nonakt_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        ttgl.Focus()
    End Sub

    Private Sub fkaryawan_nonakt_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        ttgl.EditValue = Date.Now

        tnip.Text = snip
        tnama.Text = snama
        tdepart.Text = sdepart

    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        statok = False
        Me.Close()
    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click

        statok = False

        If Not IsDate(ttgl.EditValue) Then
            MsgBox("ada kesalahan tanggal...", vbOKOnly + vbInformation, "Informasi")
            ttgl.Focus()
            Return
        End If

        If talasan.Text.Trim.Length = 0 Then
            MsgBox("Alasan harus diisi..", vbOKOnly + vbInformation, "Informasi")
            talasan.Focus()
            Return
        End If

        If MsgBox("Yakin akan dinonaktifkan ?", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.Yes Then
            simpan()
        End If

    End Sub


End Class