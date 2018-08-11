Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Imports DevExpress.XtraReports.UI

Public Class fuangmakan1

    Private Sub loaddepartemen()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = "select * from ms_depart"

            Dim dsgol As New DataSet
            dsgol = Clsmy.GetDataSet(sql, cn)

            Dim dtgol As DataTable = dsgol.Tables(0)

            Dim orow As DataRow = dtgol.NewRow
            orow("nama") = "ALL"
            dtgol.Rows.InsertAt(orow, 0)

            cbdepart.Properties.DataSource = dtgol

            If dtgol.Rows.Count > 0 Then
                cbdepart.ItemIndex = 0
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

    Private Sub load_data()

        Dim sql As String = String.Format("SELECT ms_karyawan.nip,ms_karyawan.nama,ms_karyawan.depart,SUM(tr_hadir.uangmakan) as uangmakan from tr_hadir inner join ms_karyawan on tr_hadir.nip=ms_karyawan.nip " & _
        "where tr_hadir.step=2 and tanggal>='{0}' and tanggal<='{1}'", convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue))

        If Not (cbdepart.EditValue = "ALL") Then
            sql = String.Format("{0} and ms_karyawan.depart='{1}'", sql, cbdepart.EditValue)
        End If

        sql = String.Format("{0} group by ms_karyawan.nip,ms_karyawan.nama,ms_karyawan.depart", sql)

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet = New ds_uangmakan
            ds = Clsmy.GetDataSet(sql, cn)

            Dim periode As String = ""
            periode = String.Format("Periode : {0} s.d {1}", convert_date_to_ind(ttgl1.EditValue), convert_date_to_ind(ttgl2.EditValue))

            Dim ops As New System.Drawing.Printing.PrinterSettings
            Dim rrekap As New r_uangmakan() With {.DataSource = ds.Tables(0)}
            rrekap.xperiode.Text = periode
            rrekap.xuser.Text = String.Format("{0} {1}", userprog, DateTime.Now)
            rrekap.DataMember = rrekap.DataMember
            rrekap.PrinterName = ops.PrinterName
            rrekap.CreateDocument(True)

            PrintControl1.PrintingSystem = rrekap.PrintingSystem

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

    Private Sub fuangmakan1_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        ttgl1.Focus()
    End Sub

    Private Sub fuangmakan1_Load(sender As Object, e As EventArgs) Handles Me.Load

        ttgl1.EditValue = DateValue(Date.Now)
        ttgl2.EditValue = DateValue(Date.Now)

        loaddepartemen()

    End Sub

    Private Sub btload_Click(sender As Object, e As EventArgs) Handles btload.Click
        load_data()
    End Sub


End Class