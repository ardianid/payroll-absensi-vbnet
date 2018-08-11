Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy


Public Class fkalk_jamsostek

    Const vjht_tk As Double = 0.02
    Const vjht_prs As Double = 0.036999999999999998
    Const vjkm As Double = 0.0030000000000000001
    Const vjkk As Double = 0.0088999999999999999
    Const vlajang As Double = 0.029999999999999999
    Const vkawin As Double = 0.059999999999999998


    Private Sub kalkulasi()

        Dim sql As String = "select a.nip,a.nama,a.gapok_jamsos,a.status_kawin,a.jniskelamin,a.nojamsos from ms_karyawan a inner join  ms_golongan b on a.kdgol=b.kode where b.jenisgaji='Bulanan'"
        Dim sqls As String = String.Format("select count(*) as jml from tr_iuran_jamsos where tahun={0} and bulan={1}", ttahun.Text.Trim, cbbulan.SelectedIndex + 1)
        Dim sqldel As String = String.Format("delete from tr_iuran_jamsos  where tahun={0} and bulan={1}", ttahun.Text.Trim, cbbulan.SelectedIndex + 1)

        Dim cn As OleDbConnection = Nothing

        open_wait()
        SetWaitDialog("Cek Kalkulasi sebelumnya..")

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim cmds As OleDbCommand = New OleDbCommand(sqls, cn)
            Dim drd As OleDbDataReader = cmds.ExecuteReader

            If drd.HasRows Then
                If drd.Read Then
                    Dim jml As String = drd(0).ToString
                    Dim jml1 As Integer = Convert.ToInt32(jml)

                    If jml1 > 0 Then
                        If MsgBox("Kalkulasi sebelumnya sudah ada, ingin kalkulasi ulang ?", vbYesNo + vbQuestion, "Konfirmasi") = vbNo Then
                            close_wait()
                            GoTo goout
                        Else
                            Dim sqltransdel As OleDbTransaction = cn.BeginTransaction
                            Dim cmdclear As OleDbCommand = New OleDbCommand(sqldel, cn, sqltransdel)
                            cmdclear.ExecuteNonQuery()
                            sqltransdel.Commit()

                            sqltransdel.Dispose()

                        End If

                    End If

                End If
            End If
            drd.Close()

            Dim ds As DataSet
            ds = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim dt As DataTable = ds.Tables(0)

            If dt.Rows.Count >= 0 Then

                SetWaitDialog("Kalkulasi, Silahkan tunggu...")
                Dim sqltrans As OleDbTransaction = cn.BeginTransaction

                Dim vnip As String
                Dim vnama As String
                Dim vgapok As Double
                Dim vstat As String
                Dim vjeniskel As String
                Dim vnojamsos As String

                Dim kjht_tk As Double
                Dim kjht_prs As Double
                Dim kjkm As Double
                Dim kjkk As Double
                Dim klajang As Double
                Dim kkawin As Double
                Dim ktotal As Double

                Dim sqlinsert As String = ""
                Dim cmdins As OleDbCommand

                For i As Integer = 0 To dt.Rows.Count - 1

                    vnip = dt.Rows(i)("nip").ToString
                    vnama = dt.Rows(i)("nama").ToString
                    vgapok = Convert.ToDouble(dt.Rows(i)("gapok_jamsos").ToString)
                    vstat = dt.Rows(i)("status_kawin").ToString
                    vjeniskel = dt.Rows(i)("jniskelamin").ToString
                    vnojamsos = dt.Rows(i)("nojamsos").ToString

                    kjht_tk = vgapok * vjht_tk
                    kjht_prs = vgapok * vjht_prs
                    kjkm = vgapok * vjkm
                    kjkk = vgapok * vjkk

                    If vstat.Equals("KAWIN") Then
                        klajang = 0
                        kkawin = vgapok * vkawin
                    Else
                        kkawin = 0
                        klajang = vgapok * vlajang
                    End If

                    ktotal = kjht_tk + kjht_prs + kjkm + kjkk + klajang + kkawin

                    sqlinsert = String.Format("insert into tr_iuran_jamsos (tahun,bulan,nip,stat_kawin,gapok,jht_tk,jht_prs,jkm,jkk,jpk_l,jpk_k,total) values(" & _
                                "{0},{1},'{2}','{3}',{4},{5},{6},{7},{8},{9},{10},{11})", ttahun.Text.Trim, cbbulan.SelectedIndex + 1, vnip, vstat, Replace(vgapok, ",", "."), Replace(kjht_tk, ",", "."), Replace(kjht_prs, ",", "."), Replace(kjkm, ",", "."), Replace(kjkk, ",", "."), Replace(klajang, ",", "."), Replace(kkawin, ",", "."), Replace(ktotal, ",", "."))

                    cmdins = New OleDbCommand(sqlinsert, cn, sqltrans)
                    cmdins.ExecuteNonQuery()

                Next

                sqltrans.Commit()
                close_wait()
                MsgBox("Kalkulasi selesai..", vbOKOnly + vbInformation, "Informasi")
                Me.Close()

            Else
                MsgBox("Data karyawan tidak ditemukan", vbOKOnly + vbInformation, "Informasi")
                close_wait()
            End If


goout:
        Catch ex As Exception
            close_wait()
            MsgBox(ex.ToString)
        Finally
            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try


    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub fkalk_jamsostek_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        ttahun.Focus()
    End Sub

    Private Sub fkalk_jamsostek_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ttahun.EditValue = Year(Now)
        cbbulan.SelectedIndex = 0
    End Sub

    Private Sub btcalc_Click(sender As System.Object, e As System.EventArgs) Handles btcalc.Click

        If ttahun.Text <= 0 Then
            MsgBox("Tahun tidak boleh kosong", vbOKOnly + vbInformation, "Informasi")
            ttahun.Focus()
            Return
        End If

        kalkulasi()

    End Sub
End Class