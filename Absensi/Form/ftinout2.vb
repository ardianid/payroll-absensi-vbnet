Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Public Class ftinout2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private idmesin As String
    Private in_old As String
    Private jamold As String

    Public tanggal_s As String
    Public nnip_s As String
    Private get_okadd As Boolean = False

    Public ReadOnly Property get_ok As String
        Get

            Return get_okadd

        End Get
    End Property


    Private Sub kosongkan()
        tnip.Text = ""
        tnama.Text = ""
        tket.Text = ""
        idmesin = ""
    End Sub

    Private Sub isi()
        tnip.Text = dv(position)("nip").ToString
        tnama.Text = dv(position)("nama").ToString
        ttgl1.EditValue = DateValue(dv(position)("tanggal").ToString)
        tjam1.EditValue = TimeValue(dv(position)("jam").ToString)
        tket.Text = dv(position)("alasan").ToString
        idmesin = dv(position)("idmesin").ToString

        Dim tanggal As String = DateValue(ttgl1.EditValue)
        Dim jam1 As String = TimeValue(tjam1.EditValue)

        jamold = jam1

        in_old = String.Format("{0} {1}", tanggal, jam1)

    End Sub

    Private Sub simpan()

        get_okadd = False

        open_wait()
        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            '  Dim dtkalender As DataTable = loadsced(cn)

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction

            Dim jammasuk As String = TimeValue(tjam1.EditValue)

            Dim sqlinout As String = ""
            
            Dim tgal As Date
            Dim tgl1 As String = DateValue(ttgl1.EditValue)

            Dim rdate_in As String = String.Format("{0} {1}", tgl1, jammasuk)



            If addstat = True Then

                Dim sql As String = ""


                tgal = ttgl1.EditValue

                Dim sqls_inout As String = String.Format("select count(*) from ms_inout where userid={0} and checktime='{1}'", idmesin, convert_datetime_to_eng(rdate_in))
                Dim comds As OleDbCommand = New OleDbCommand(sqls_inout, cn, sqltrans)
                Dim dred As OleDbDataReader = comds.ExecuteReader

                If dred.HasRows Then
                    If dred.Read Then
                        Dim xhasil As Integer = Convert.ToInt32(dred(0).ToString)

                        If xhasil > 0 Then
                            MsgBox("Jam absen sudah ada...", vbOKOnly + vbInformation, "Informasi")
                            close_wait()
                            Return
                        End If

                    End If
                End If


                sql = String.Format("insert into ms_tinout (nip,tanggal,jam,alasan) values('{0}','{1}','{2}','{3}')", _
                                            tnip.Text.Trim, convert_date_to_eng(tgal), jammasuk, tket.Text.Trim)

                sqlinout = String.Format("insert into ms_inout (userid,checktime,verifymode,inoutmode,workcode,skalk) values({0},'{1}',0,0,0,0)", idmesin, convert_datetime_to_eng(rdate_in))

                Dim cmdins_inout1 As OleDbCommand = New OleDbCommand(sqlinout, cn, sqltrans)
                cmdins_inout1.ExecuteNonQuery()

                Dim cmdins As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmdins.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btkurabsen", 1, 0, 0, 0, tnip.Text.Trim, convert_date_to_eng(tgal), sqltrans)

                If nnip_s.Length = 0 Then
                    insertview(tgal)
                End If




            Else


                sqlinout = String.Format("update ms_inout set checktime='{0}' where userid={1} and checktime='{2}'", convert_datetime_to_eng(rdate_in), idmesin, convert_datetime_to_eng(in_old))

                Dim cmdins_inout1 As OleDbCommand = New OleDbCommand(sqlinout, cn, sqltrans)
                cmdins_inout1.ExecuteNonQuery()

                Dim sqlup As String = String.Format("update ms_tinout set jam='{0}',alasan='{1}' where nip='{2}' and tanggal='{3}' and jam='{4}'", _
                                                    jammasuk, tket.Text.Trim, tnip.Text.Trim, convert_date_to_eng(ttgl1.EditValue), jamold)

                Dim cmdup As OleDbCommand = New OleDbCommand(sqlup, cn, sqltrans)
                cmdup.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btkurabsen", 0, 1, 0, 0, tnip.Text.Trim, convert_date_to_eng(ttgl1.EditValue), sqltrans)

                If nnip_s.Length = 0 Then
                    editview()
                End If


            End If

            sqltrans.Commit()

            close_wait()

            If nnip_s.Length = 0 Then

                MsgBox("Data Disimpan..", vbOKOnly + vbInformation, "Informasi")

                If addstat = True Then
                    kosongkan()
                    tnip.Focus()
                Else
                    Me.Close()
                End If

            Else

                get_okadd = True

                Me.Close()
            End If
            


        Catch ex As Exception
            get_okadd = False
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

    Private Sub insertview(ByVal tanggal As String)
        Dim orow As DataRowView = dv.AddNew
        orow("nip") = tnip.Text.Trim
        orow("nama") = tnama.Text.Trim
        orow("tanggal") = tanggal
        orow("alasan") = tket.Text.Trim
        orow("jam") = tjam1.EditValue
        orow("idmesin") = idmesin
        dv.EndInit()
    End Sub

    Private Sub editview()
        dv(position)("nama") = tnama.Text.Trim
        dv(position)("tanggal") = ttgl1.EditValue
        dv(position)("alasan") = tket.Text.Trim
        dv(position)("jam") = tjam1.EditValue
        dv(position)("idmesin") = idmesin
    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click

        If tnip.Text.Trim.Length = 0 Then
            MsgBox("Nip tidak boleh kosong...", vbOKOnly + vbInformation, "Informasi")
            tnip.Focus()
            Return
        End If

        If tket.Text.Trim.Length = 0 Then
            MsgBox("Alasan harus diisi...", vbOKOnly + vbInformation, "Informasi")
            tket.Focus()
            Return
        End If

        simpan()

    End Sub

    Private Sub btselesai_Click(sender As System.Object, e As System.EventArgs) Handles btselesai.Click
        get_okadd = False
        Me.Close()
    End Sub

    Private Sub fstathadir2_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        If addstat = True Then

            If Not nnip_s.Length = 0 Then
                tjam1.Focus()
            Else
                tnip.Focus()
            End If

        Else
            tket.Focus()
        End If
    End Sub

    Private Sub fstathadir2_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        get_okadd = False

        If addstat = True Then
            kosongkan()

            ttgl1.EditValue = Date.Now

            tnip.Enabled = True
            btcnip.Enabled = True

            ttgl1.Enabled = True

            If Not nnip_s.Length = 0 Then
                tnip.EditValue = nnip_s
                tnip_LostFocus(sender, Nothing)
                ttgl1.EditValue = convert_date_to_ind(tanggal_s)
            End If

        Else

            isi()
            tnip.Enabled = False
            btcnip.Enabled = False

            ttgl1.Enabled = False

        End If
    End Sub

    Private Sub btcnip_Click(sender As System.Object, e As System.EventArgs) Handles btcnip.Click
        Using fskary As New fskary With {.StartPosition = FormStartPosition.CenterParent}
            fskary.ShowDialog(Me)

            tnip.Text = fskary.get_Nip
            tnama.Text = fskary.get_nama
            idmesin = fskary.get_idmesin

        End Using
    End Sub

    Private Sub tnip_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tnip.KeyDown
        If e.KeyCode = Keys.F4 Then
            btcnip_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tnip_LostFocus(sender As Object, e As System.EventArgs) Handles tnip.LostFocus

        If tnip.Text.Trim.Length > 0 Then

            Dim cn As OleDbConnection = Nothing

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim sql As String = String.Format("select nip,nama,alamat,idmesin from ms_karyawan where shiden=0 and aktif=1 and nip='{0}'", tnip.Text.Trim)
                Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim drd As OleDbDataReader = cmd.ExecuteReader

                If drd.HasRows Then
                    If drd.Read Then
                        tnip.Text = drd(0).ToString
                        tnama.Text = drd(1).ToString
                        idmesin = drd(3).ToString
                    Else
                        MsgBox("Nip tidak ditemukan...", vbOKOnly + vbInformation, "Informasi")
                        tnip.Text = ""
                        tnama.Text = ""
                        idmesin = ""
                    End If
                Else
                    MsgBox("Nip tidak ditemukan...", vbOKOnly + vbInformation, "Informasi")
                    tnip.Text = ""
                    tnama.Text = ""
                    idmesin = ""
                End If


            Catch ex As Exception

            End Try

        End If

    End Sub

    Private Sub tnip_EditValueChanged(sender As Object, e As EventArgs) Handles tnip.EditValueChanged

    End Sub
End Class