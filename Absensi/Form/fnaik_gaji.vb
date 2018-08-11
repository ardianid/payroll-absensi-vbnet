Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Public Class fnaik_gaji

    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private Sub loaddata()

        Dim sql As String = "select nip,nama,gapok,0 as gapok2,0 as hitlembur from ms_karyawan where nip='xxx111'"

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager = New DataViewManager(ds)
            dv1 = dvmanager.CreateDataView(ds.Tables(0))

            grid1.DataSource = dv1

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

    Private Sub simpan_data()

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction
            For i As Integer = 0 To dv1.Count - 1

                Dim sqlc As String = String.Format("select nip,gaji1,gaji2,gaji3,gaji4,kdgol,tunj_jabatan,jniskelamin from ms_karyawan where nip='{0}'", dv1(i)("nip").ToString)
                Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn, sqltrans)
                Dim drdc As OleDbDataReader = cmdc.ExecuteReader

                If drdc.Read Then

                    If Not drdc("nip").ToString.Equals("") Then

                        Dim gaji1 As Double = Double.Parse(drdc("gaji1").ToString)
                        Dim gaji2 As Double = Double.Parse(drdc("gaji2").ToString)
                        Dim gaji3 As Double = Double.Parse(drdc("gaji3").ToString)
                        Dim gaji4 As Double = Double.Parse(drdc("gaji4").ToString)

                        Dim sqlup As String = ""

                        If gaji1 = 0 And gaji2 = 0 And gaji3 = 0 And gaji4 = 0 Then
                            sqlup = String.Format("update ms_karyawan set naik1='{0}',gaji1={1},gapok={1} where nip='{2}'", convert_date_to_eng(ttgl.EditValue), Replace(dv1(i)("gapok2").ToString, ",", "."), dv1(i)("nip").ToString)
                        ElseIf gaji2 = 0 And gaji3 = 0 And gaji4 = 0 And gaji1 > 0 Then
                            sqlup = String.Format("update ms_karyawan set naik2='{0}',gaji2={1},gapok={1} where nip='{2}'", convert_date_to_eng(ttgl.EditValue), Replace(dv1(i)("gapok2").ToString, ",", "."), dv1(i)("nip").ToString)
                        ElseIf gaji3 = 0 And gaji4 = 0 And gaji1 > 0 And gaji2 > 0 Then
                            sqlup = String.Format("update ms_karyawan set naik3='{0}',gaji3={1},gapok={1} where nip='{2}'", convert_date_to_eng(ttgl.EditValue), Replace(dv1(i)("gapok2").ToString, ",", "."), dv1(i)("nip").ToString)
                        ElseIf gaji4 = 0 And gaji1 > 0 And gaji2 > 0 And gaji3 > 0 Then
                            sqlup = String.Format("update ms_karyawan set naik4='{0}',gaji4={1},gapok={1} where nip='{2}'", convert_date_to_eng(ttgl.EditValue), Replace(dv1(i)("gapok2").ToString, ",", "."), dv1(i)("nip").ToString)
                        ElseIf gaji4 > 0 And gaji3 > 0 And gaji2 > 0 And gaji1 > 0 Then

                            Dim sqlup1 As String = String.Format("update ms_karyawan set naik1=naik2,gaji1=gaji2 where nip='{0}'", dv1(i)("nip").ToString)
                            Using cmdup1 As OleDbCommand = New OleDbCommand(sqlup1, cn, sqltrans)
                                cmdup1.ExecuteNonQuery()
                            End Using

                            Dim sqlup2 As String = String.Format("update ms_karyawan set naik2=naik3,gaji2=gaji3 where nip='{0}'", dv1(i)("nip").ToString)
                            Using cmdup2 As OleDbCommand = New OleDbCommand(sqlup2, cn, sqltrans)
                                cmdup2.ExecuteNonQuery()
                            End Using

                            Dim sqlup3 As String = String.Format("update ms_karyawan set naik3=naik4,gaji3=gaji4 where nip='{0}'", dv1(i)("nip").ToString)
                            Using cmdup3 As OleDbCommand = New OleDbCommand(sqlup3, cn, sqltrans)
                                cmdup3.ExecuteNonQuery()
                            End Using

                            sqlup = String.Format("update ms_karyawan set naik4='{0}',gaji4={1},gapok={1} where nip='{2}'", convert_date_to_eng(ttgl.EditValue), Replace(dv1(i)("gapok2").ToString, ",", "."), dv1(i)("nip").ToString)

                        End If

                        Using cmdup As OleDbCommand = New OleDbCommand(sqlup, cn, sqltrans)
                            cmdup.ExecuteNonQuery()
                        End Using

                        Dim gpk = Replace(dv1(i)("gapok2").ToString, ",", ".")
                        Dim tunj_jabatan As Double = Double.Parse(drdc("tunj_jabatan").ToString)
                        Dim sharian As Integer = 0
                        Dim jenislembur2 As String = ""
                        Dim nilharian As Double = 0
                        Dim lembur_perjam As Double = 0

                        Dim sqlgol As String = String.Format("select jenislembur,harian,jnisrange,jenisgaji,laki2,perempuan from ms_golongan where saktif=1 and kode='{0}'", drdc("kdgol").ToString)
                        Dim cmdgol As OleDbCommand = New OleDbCommand(sqlgol, cn, sqltrans)
                        Dim drdgol As OleDbDataReader = cmdgol.ExecuteReader

                        If drdgol.Read Then
                            sharian = drdgol("harian").ToString
                            jenislembur2 = drdgol("jenislembur").ToString

                            'If drdc("jniskelamin").ToString = "Laki - Laki" Then
                            '    nilharian = drdgol("laki2").ToString
                            'Else
                            '    nilharian = drdgol("perempuan").ToString
                            'End If

                            'If nilharian = 0 Then
                            nilharian = gpk
                            'End If

                        End If
                        drdgol.Close()

                        If sharian = 1 Then

                            If jenislembur2.Equals("Depnaker") Then

                                If gpk > 0 Then
                                    lembur_perjam = ((gpk * 25) / 173)
                                Else
                                    lembur_perjam = ((nilharian * 25) / 173)
                                End If

                            ElseIf jenislembur2.Equals("Jam Mati") Then

                                If gpk > 0 Then
                                    lembur_perjam = ((gpk * 25) / 173)
                                Else
                                    lembur_perjam = ((nilharian * 25) / 173)
                                End If

                            Else
                                lembur_perjam = 0
                            End If

                            Dim sqlu As String = String.Format("update ms_karyawan set lembur_perjam={0} where nip='{1}'", Replace(lembur_perjam, ",", "."), dv1(i)("nip").ToString)
                            Using cmdu As OleDbCommand = New OleDbCommand(sqlu, cn, sqltrans)
                                cmdu.ExecuteNonQuery()
                            End Using

                        Else

                            If jenislembur2.Equals("Depnaker") Then
                                lembur_perjam = ((gpk + tunj_jabatan) / 173)
                            ElseIf jenislembur2.Equals("Jam Mati") Then
                                lembur_perjam = ((gpk + tunj_jabatan) / 25 / 7)
                            Else
                                lembur_perjam = 0
                            End If

                            Dim sqlu As String = String.Format("update ms_karyawan set lembur_perjam={0} where nip='{1}'", Replace(lembur_perjam, ",", "."), dv1(i)("nip").ToString)
                            Using cmdu As OleDbCommand = New OleDbCommand(sqlu, cn, sqltrans)
                                cmdu.ExecuteNonQuery()
                            End Using

                        End If

                    End If

                End If
                drdc.Close()

            Next

            sqltrans.Commit()
            MsgBox("Data disimpan...", vbOKOnly + vbInformation, "Informasi")

            loaddata()

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

    Private Sub fnaik_gaji_Load(sender As Object, e As System.EventArgs) Handles Me.load
        ttgl.EditValue = convert_date_to_ind(Date.Now)

        loaddata()

    End Sub

    Private Sub btclear_Click(sender As System.Object, e As System.EventArgs) Handles btclear.Click

        loaddata()

    End Sub

    Private Sub btdel_Click(sender As System.Object, e As System.EventArgs) Handles btdel.Click

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        dv1.Delete(Me.BindingContext(dv1).Position)
    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click

        If IsNothing(dv1) Then
            MsgBox("Tidak ada data yang akan disimpan...", vbOKOnly + vbInformation, "Informasi")
            Return
        End If

        If dv1.Count <= 0 Then
            MsgBox("Tidak ada data yang akan disimpan...", vbOKOnly + vbInformation, "Informasi")
            Return
        End If

        If MsgBox("Yakin data yang disimpan sudah benar ?", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.Yes Then
            simpan_data()
        End If

    End Sub

    Private Sub btadd_Click(sender As System.Object, e As System.EventArgs) Handles btadd.Click

        Using fjamker2 As New fnaik_gaji2 With {.StartPosition = FormStartPosition.CenterParent, .dv3 = dv1}
            fjamker2.ShowDialog()
        End Using

    End Sub


End Class