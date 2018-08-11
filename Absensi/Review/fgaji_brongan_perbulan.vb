Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Imports DevExpress.XtraReports.UI
Public Class fgaji_brongan_perbulan

    Private Sub loadGolongan()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = "select kode,nama from ms_golongan where jenisgaji='Mingguan' and tampilgroup=1 and saktif=1 and kode in (select kode from ms_golongan1)"

            Dim dsgol As New DataSet
            dsgol = Clsmy.GetDataSet(sql, cn)

            Dim dtgol As DataTable = dsgol.Tables(0)

            cbgol.Properties.DataSource = dtgol

            If dtgol.Rows.Count > 0 Then
                cbgol.ItemIndex = 0
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

    Private Sub loadDepartemen()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = "select * from ms_depart order by nama"

            Dim dsdepart As New DataSet
            dsdepart = Clsmy.GetDataSet(sql, cn)

            Dim dtdepart As DataTable = dsdepart.Tables(0)

            Dim rows As DataRow = dtdepart.NewRow
            rows(0) = "ALL"
            dtdepart.Rows.InsertAt(rows, 0)

            cbdepart.Properties.DataSource = dtdepart

            If dtdepart.Rows.Count > 0 Then
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

    Private Sub load_report()

        Dim sql As String = String.Format("select kary.nip,kary.nama,hdr.depart, " & _
        "case when MONTH(hdr.tanggal)=1 then " & _
        "SUM(hdr.tothasil) else 0 end as '1b', " & _
        "case when MONTH(hdr.tanggal)=2 then " & _
        "SUM(hdr.tothasil) else 0 end as '2b', " & _
        "case when MONTH(hdr.tanggal)=3 then " & _
        "SUM(hdr.tothasil) else 0 end as '3b', " & _
        "case when MONTH(hdr.tanggal)=4 then " & _
        "SUM(hdr.tothasil) else 0 end as '4b', " & _
        "case when MONTH(hdr.tanggal)=5 then " & _
        "SUM(hdr.tothasil) else 0 end as '5b', " & _
        "case when MONTH(hdr.tanggal)=6 then " & _
        "SUM(hdr.tothasil) else 0 end as '6b', " & _
        "case when MONTH(hdr.tanggal)=7 then " & _
        "SUM(hdr.tothasil) else 0 end as '7b', " & _
        "case when MONTH(hdr.tanggal)=8 then " & _
        "SUM(hdr.tothasil) else 0 end as '8b', " & _
        "case when MONTH(hdr.tanggal)=9 then " & _
        "SUM(hdr.tothasil) else 0 end as '9b', " & _
        "case when MONTH(hdr.tanggal)=10 then " & _
        "SUM(hdr.tothasil) else 0 end as '10b', " & _
        "case when MONTH(hdr.tanggal)=11 then " & _
        "SUM(hdr.tothasil) else 0 end as '11b', " & _
        "case when MONTH(hdr.tanggal)=12 then " & _
        "SUM(hdr.tothasil) else 0 end as '12b' " & _
        " from tr_hadir hdr inner join ms_karyawan kary on hdr.nip=kary.nip " & _
        "where hdr.skalk=1 and hdr.step=2 and YEAR(hdr.tanggal)={0} AND hdr.kdgol='{1}'", ttahun.EditValue, cbgol.EditValue)

        If cbdepart.EditValue = "ALL" Then
        Else
            sql = String.Format("{0} and hdr.depart='{1}'", sql, cbdepart.EditValue)
        End If

        sql = String.Format("{0} group by kary.nip,kary.nama,hdr.depart,MONTH(hdr.tanggal)", sql)

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet = New ds_borongan_perbln
            ds = Clsmy.GetDataSet(sql, cn)

            Dim ops As New System.Drawing.Printing.PrinterSettings
            Dim rrekap As New r_gaji_borongan_perbulan() With {.DataSource = ds.Tables(0)}
            rrekap.xtahun.Text = String.Format("Tahun : {0}", ttahun.EditValue)
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

    Private Sub fgaji_brongan_perbulan_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        ttahun.Focus()
    End Sub

    Private Sub fgaji_brongan_perbulan_Load(sender As Object, e As EventArgs) Handles Me.Load

        loadGolongan()
        loadDepartemen()

        ttahun.EditValue = Year(Date.Now)

    End Sub

    Private Sub btload_Click(sender As Object, e As EventArgs) Handles btload.Click

        If ttahun.EditValue = 0 Then
            MsgBox("Tahun harus diisi", vbOKOnly + vbInformation, "Informasi")
            ttahun.Focus()
            Return
        End If

        load_report()
    End Sub

End Class