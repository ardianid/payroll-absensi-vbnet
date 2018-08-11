Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Imports DevExpress.XtraReports.UI

Public Class fpr_gajiborongan

    Public tgl1 As String
    Public tgl2 As String
    Public kdgol As String

    Private Sub load_print()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            Dim sql As String = String.Format("select b.nip,b.nama, " & _
"	case when d.hari='SENIN' then " & _
" 	a.tothasil " & _
"	else 0 " & _
"	end as 'SENIN', " & _
"	case when d.hari='SELASA' then " & _
"	a.tothasil " & _
"	else 0 " & _
"	end as 'SELASA', " & _
"	case when d.hari='RABU' then " & _
"	a.tothasil " & _
"	else 0 " & _
"	end as 'RABU', " & _
"	case when d.hari='KAMIS' then " & _
"	a.tothasil " & _
"	else 0 " & _
"	end as 'KAMIS', " & _
"	case when d.hari='JUMAT' then " & _
"	a.tothasil " & _
"	else 0 " & _
"	end as 'JUMAT', " & _
"	case when d.hari='SABTU' then " & _
"	a.tothasil " & _
"	else 0 " & _
"	end as 'SABTU', " & _
"	case when d.hari='MINGGU' then " & _
"	a.tothasil " & _
"	else 0 " & _
"	end as 'MINGGU' from tr_hadir a inner join ms_karyawan b on a.nip=b.nip inner join ms_golongan c on b.kdgol=c.kode " & _
"	inner join ms_kalender d on a.tanggal=d.tanggal " & _
"	where c.jenisgaji='Mingguan' and b.aktif=1 and a.step=2 and c.harian=0 " & _
"	and a.skalk=1 and a.tanggal>='{0}' and a.tanggal<='{1}'", convert_date_to_eng(tgl1), convert_date_to_eng(tgl2))

            If Not kdgol.Equals("") And Not kdgol.Equals("All") Then
                sql = String.Format("{0} and c.kode='{1}'", sql, kdgol)
            End If

            Dim ds As DataSet = New ds_gajiborongan
            ds = Clsmy.GetDataSet(sql, cn)

            Dim ops As New System.Drawing.Printing.PrinterSettings
            Dim rrekap As New rgaji_borongan() With {.DataSource = ds.Tables(0)}
            rrekap.xtanggal.Text = String.Format("{0} s.d {1}", convert_date_to_ind(tgl1), convert_date_to_ind(tgl2))
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

    Private Sub fpr_gajiborongan_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        load_print()
    End Sub

End Class