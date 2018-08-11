Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Imports DevExpress.XtraReports.UI
Public Class fgaji_perdepart_pergol

    Private Sub loaddepartemen()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = "select * from ms_depart"

            Dim dsgol As New DataSet
            dsgol = Clsmy.GetDataSet(sql, cn)

            Dim dtgol As DataTable = dsgol.Tables(0)

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

        Dim sql As String = String.Format("SELECT ms_karyawan.nip, ms_karyawan.nama,ms_karyawan.jabatan, ms_golongan.nama AS namagol,tr_gaji.depart, sum(tr_gaji.gapok) as gapok, " & _
        "sum(tr_gaji.tunj_jab) as tunj_jab,  " & _
        "sum(tr_gaji.tunj_hadir) as tunj_hadir,  " & _
        "sum(tr_gaji.jmlhadir) as jmlhadir, " & _
        "sum(tr_gaji.tunj_akomod) as tunj_akomod,  " & _
        "sum(tr_gaji.tunj_makan) as tunj_makan, " & _
        "sum(tr_gaji.tunj_makanlembur) as tunj_makanlembur, " & _
        "sum(tr_gaji.jmllembur) as jmllembur, " & _
        "sum(tr_gaji.gaji_lembur) as gaji_lembur, " & _
        "sum(tr_gaji.tot_hasil) as tot_hasil, " & _
        "sum(tr_gaji.tot_harian) as tot_harian, " & _
        "sum(tr_gaji.kurangi_gapok) as kurangi_gapok,  " & _
        " sum(tr_gaji.tot_standby) as tot_standby, " & _
        "sum(tr_gaji.angsuran_bon) as angsuran_bon, " & _
        "sum(tr_gaji.pot_jamsos) as pot_jamsos, " & _
        "sum(tr_gaji.pot_cuti) as pot_cuti " & _
        "FROM ms_karyawan INNER JOIN " & _
        "tr_gaji ON ms_karyawan.nip = tr_gaji.nip INNER JOIN " & _
        "ms_golongan ON tr_gaji.kdgol = ms_golongan.kode " & _
        "WHERE tr_gaji.depart='{0}'", cbdepart.EditValue)

        If ttahun.EditValue <> 0 Then
            sql = String.Format("{0} and tr_gaji.tahun={1}", sql, ttahun.EditValue)
        End If

        If tbln.EditValue <> 0 Then
            sql = String.Format("{0} and tr_gaji.bulan={1}", sql, tbln.EditValue)
        End If

        sql = String.Format("{0} group by ms_karyawan.nip, ms_karyawan.nama,ms_karyawan.jabatan, ms_golongan.nama,tr_gaji.depart", sql)

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet = New ds_gaji_goldep
            ds = Clsmy.GetDataSet(sql, cn)

            Dim periode As String = ""
            periode = String.Format("Periode : {0}/{1}", tbln.EditValue, ttahun.EditValue)

            Dim ops As New System.Drawing.Printing.PrinterSettings
            Dim rrekap As New r_gaji_goldep() With {.DataSource = ds.Tables(0)}
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

    Private Sub fgaji_perdepart_pergol_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        cbdepart.Focus()
    End Sub

    Private Sub fgaji_perdepart_pergol_Load(sender As Object, e As EventArgs) Handles Me.Load

        loaddepartemen()
        ttahun.EditValue = Year(Date.Now)
        tbln.EditValue = Month(Date.Now)

    End Sub

    Private Sub btload_Click(sender As Object, e As EventArgs) Handles btload.Click

        If ttahun.EditValue = 0 Then
            MsgBox("Tahun harus diisi..", vbOKOnly + vbInformation, "Informasi")
            ttahun.Focus()
            Return
        End If

        If tbln.EditValue = 0 Then
            MsgBox("Bln harus diisi..", vbOKOnly + vbInformation, "Informasi")
            tbln.Focus()
            Return
        End If

        load_data()

    End Sub

End Class