Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Public Class fskary

    Private ds As DataSet
    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Public hanyabulanan As Boolean = False

    Private Sub cari()

        Dim sql As String = ""
        Dim sqlkriteria As String = ""

        sql = "select top 100 nip,nama,alamat,idmesin from ms_karyawan where aktif=1 and shiden=0"

        If hanyabulanan Then
            sql = String.Format("{0} and kdgol in (select kode from ms_golongan where jenisgaji='Bulanan')", sql)
        End If

        If tfind.Text.Trim.Length = 0 Then
        Else

            Select Case cbofind.SelectedIndex
                Case 0
                    sqlkriteria = String.Format("nip like '%{0}%'", tfind.Text.Trim)
                Case 1
                    sqlkriteria = String.Format("nama like '%{0}%'", tfind.Text.Trim)
                Case 2
                    sqlkriteria = String.Format("alamat like '%{0}%'", tfind.Text.Trim)
            End Select

            sql = String.Format("{0} and {1}", sql, sqlkriteria)

        End If

        Dim cn As OleDbConnection = Nothing

        Try

            dv1 = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager = New DataViewManager(ds)
            dv1 = dvmanager.CreateDataView(ds.Tables(0))

            grid1.DataSource = dv1

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

    Public ReadOnly Property get_Nip As String
        Get

            If IsNothing(dv1) Then
                Return ""
                Exit Property
            End If

            If dv1.Count <= 0 Then
                Return ""
                Exit Property
            End If
            Return dv1(Me.BindingContext(dv1).Position)("nip").ToString
        End Get
    End Property

    Public ReadOnly Property get_nama As String
        Get

            If IsNothing(dv1) Then
                Return ""
                Exit Property
            End If

            If dv1.Count <= 0 Then
                Return ""
                Exit Property
            End If
            Return dv1(Me.BindingContext(dv1).Position)("nama").ToString
        End Get
    End Property

    Public ReadOnly Property get_idmesin As String
        Get

            If IsNothing(dv1) Then
                Return ""
                Exit Property
            End If

            If dv1.Count <= 0 Then
                Return ""
                Exit Property
            End If
            Return dv1(Me.BindingContext(dv1).Position)("idmesin").ToString
        End Get
    End Property

    Private Sub fskary_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        cbofind.SelectedIndex = 1
        cari()
    End Sub

    Private Sub tfind_EditValueChanged(sender As Object, e As System.EventArgs) Handles tfind.EditValueChanged
        cari()
    End Sub

    Private Sub tfind_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tfind.KeyDown
        If e.KeyCode = 13 Then
            Me.Close()
        End If
    End Sub


    Private Sub grid1_DoubleClick(sender As Object, e As System.EventArgs) Handles grid1.DoubleClick
        Me.Close()
    End Sub
End Class