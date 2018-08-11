Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Public Class fkalk_absen2

    Public kd_gol As String

    Private dtdepart As DataTable
    Private dtpegawai As DataTable

    Private okkalk As Boolean = False

    Private Sub load_depart()

        Dim cn As OleDbConnection = Nothing

        Const sql As String = "select * from ms_depart"

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            dtdepart = New DataTable

            Dim ds As DataSet = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            dtdepart = ds.Tables(0)

            Dim orow As DataRow = dtdepart.NewRow
            orow("nama") = "All"
            dtdepart.Rows.InsertAt(orow, 0)

            cbdepart.Properties.DataSource = dtdepart

            cbdepart.ItemIndex = 0

        Catch ex As Exception

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

        End Try

    End Sub

    Private Sub load_pegawai()

        Dim cn As OleDbConnection = Nothing

        Dim sql As String = String.Format("select nip,nama from ms_karyawan where kdgol='{0}' and aktif=1 order by nama", kd_gol)

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)


            dtpegawai = ds.Tables(0)

            Dim orow As DataRow = dtpegawai.NewRow
            orow("nip") = "All"
            orow("nama") = "All"
            dtpegawai.Rows.InsertAt(orow, 0)

            cbpeg.Properties.DataSource = dtpegawai

            cbpeg.ItemIndex = 0

        Catch ex As Exception

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

        End Try

    End Sub

    Public ReadOnly Property get_tgl As String
        Get

            'If IsNothing(dv1) Then
            '    Return ""
            '    Exit Property
            'End If

            'If dv1.Count <= 0 Then
            '    Return ""
            '    Exit Property
            'End If
            Return ttgl.EditValue
        End Get
    End Property

    Public ReadOnly Property get_tgl2 As String
        Get

            'If IsNothing(dv1) Then
            '    Return ""
            '    Exit Property
            'End If

            'If dv1.Count <= 0 Then
            '    Return ""
            '    Exit Property
            'End If
            Return ttgl2.EditValue
        End Get
    End Property

    Public ReadOnly Property get_depart As String
        Get

            'If IsNothing(dtdepart) Then
            '    Return ""
            '    Exit Property
            'End If

            'If dtdepart.Rows.Count <= 0 Then
            '    Return ""
            '    Exit Property
            'End If
            Return "all" 'cbdepart.EditValue
        End Get
    End Property

    Public ReadOnly Property get_gol As String
        Get

            'If IsNothing(dtdepart) Then
            '    Return ""
            '    Exit Property
            'End If

            'If dtdepart.Rows.Count <= 0 Then
            '    Return ""
            '    Exit Property
            'End If
            Return cbgol.Text.Trim
        End Get
    End Property

    Public ReadOnly Property get_peg As String
        Get

            If IsNothing(dtpegawai) Then
                Return ""
                Exit Property
            End If

            If dtpegawai.Rows.Count <= 0 Then
                Return ""
                Exit Property
            End If
            Return cbpeg.EditValue
        End Get
    End Property

    Public ReadOnly Property get_stat As String
        Get

            'If IsNothing(dtpegawai) Then
            '    Return ""
            '    Exit Property
            'End If

            'If dtpegawai.Rows.Count <= 0 Then
            '    Return ""
            '    Exit Property
            'End If
            Return okkalk
        End Get
    End Property

    Public ReadOnly Property get_kalk As String
        Get

            If ckalk.Checked = True Then
                Return True
            Else
                Return False
            End If

        End Get
    End Property

    Private Sub btcancel_Click(sender As System.Object, e As System.EventArgs) Handles btcancel.Click

        okkalk = False
        Me.Close()

    End Sub

    Private Sub fkalk_absen2_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        ttgl.Focus()
    End Sub

    Private Sub fkalk_absen2_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        ttgl.EditValue = Date.Now
        ttgl2.EditValue = Date.Now

        load_depart()
        load_pegawai()

        cbgol.SelectedIndex = 0

        okkalk = False

    End Sub

    Private Sub btok_Click(sender As System.Object, e As System.EventArgs) Handles btok.Click

        If ckalk.Checked Then

            If MsgBox("Yakin akan kalkulasi ulang ?karna data yang sebelumnya telah dikalkulasi akan diperhitungkan ulang..", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.No Then
                okkalk = False
                Exit Sub
            End If

        End If

        okkalk = True
        Me.Close()

    End Sub

End Class