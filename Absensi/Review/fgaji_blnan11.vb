Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Imports DevExpress.XtraReports.UI

Public Class fgaji_blnan11

    Public sql As String
    Public periode As String
    Public sqlhari As String
    Public statall As Boolean
    Public jenislap As Integer

    Private Sub load_print1(ByVal cn As OleDbConnection)

        Dim ds As DataSet = New ds_gajibulanan1
        ds = Clsmy.GetDataSet(sql, cn)

        If statall = False Then
            Dim sqlvi As String = String.Format("select nip from ms_usersys4 where namauser='{0}'", userprog)
            Dim cmd As OleDbCommand = New OleDbCommand(sqlvi, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            While drd.Read
                Dim rows() As DataRow = ds.Tables(0).Select(String.Format("nip='{0}'", drd("nip").ToString))

                If rows.Length > 0 Then
                    If rows(0)("nip").ToString.Equals(drd("nip").ToString) Then
                        rows(0)("statin") = "off"
                    End If
                End If

            End While
            drd.Close()
        End If

        Dim ops As New System.Drawing.Printing.PrinterSettings
        Dim rrekap As New r_gajibulanan() With {.DataSource = ds.Tables(0)}
        rrekap.xperiode.Text = periode
        rrekap.xuser.Text = String.Format("{0} {1}", userprog, DateTime.Now)
        rrekap.DataMember = rrekap.DataMember
        rrekap.PrinterName = ops.PrinterName
        rrekap.CreateDocument(True)

        PrintControl1.PrintingSystem = rrekap.PrintingSystem


    End Sub

    Private Sub load_print3(ByVal cn As OleDbConnection)

        Dim jmlhari As Integer = 0
        Dim cmdhari As OleDbCommand = New OleDbCommand(sqlhari, cn)
        Dim drdhari As OleDbDataReader = cmdhari.ExecuteReader
        If drdhari.Read Then
            If IsNumeric(drdhari(0).ToString) Then
                jmlhari = Integer.Parse(drdhari(0).ToString)
            End If
        End If
        drdhari.Close()

        Dim ds As DataSet = New ds_gajibulanan3
        ds = Clsmy.GetDataSet(sql, cn)

        If statall = False Then
            Dim sqlvi As String = String.Format("select nip from ms_usersys4 where namauser='{0}'", userprog)
            Dim cmd As OleDbCommand = New OleDbCommand(sqlvi, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            While drd.Read
                Dim rows() As DataRow = ds.Tables(0).Select(String.Format("nip='{0}'", drd("nip").ToString))

                If rows.Length > 0 Then
                    If rows(0)("nip").ToString.Equals(drd("nip").ToString) Then
                        rows(0)("statin") = "off"
                    End If
                End If

            End While
            drd.Close()
        End If

        Dim ops As New System.Drawing.Printing.PrinterSettings
        Dim rrekap As New r_gajibulanan3() With {.DataSource = ds.Tables(0)}
        rrekap.xperiode.Text = periode
        rrekap.xuser.Text = String.Format("{0} {1}", userprog, DateTime.Now)
        rrekap.xjmlhari.Text = jmlhari
        rrekap.DataMember = rrekap.DataMember
        rrekap.PrinterName = ops.PrinterName
        rrekap.CreateDocument(True)

        PrintControl1.PrintingSystem = rrekap.PrintingSystem

    End Sub

    Private Sub load_print()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            If jenislap = 1 Or jenislap = 2 Then
                load_print1(cn)
            ElseIf jenislap = 3 Then
                load_print3(cn)
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

    Private Sub fpr_gajiborongan_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        load_print()
    End Sub

End Class