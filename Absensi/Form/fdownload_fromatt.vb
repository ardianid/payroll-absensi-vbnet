Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy
Public Class fdownload_fromatt

    Private bs1 As BindingSource
    Private ds As DataSet
    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private Sub load_lokasi()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = "select * from ms_lokasi_att"
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                tlokasi.EditValue = drd(0).ToString
            End If
            drd.Close()

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
    Private Sub opendata()

        Dim sql As String = String.Format("SELECT distinct CHECKINOUT.CHECKTIME as tanggal, USERINFO.Badgenumber as idmesin , USERINFO.Name as nama " & _
        "FROM CHECKINOUT INNER JOIN USERINFO ON CHECKINOUT.USERID = USERINFO.USERID " & _
        "where FORMAT(CHECKINOUT.CHECKTIME,'MM/dd/yyyy') >='{0}' and FORMAT(CHECKINOUT.CHECKTIME,'MM/dd/yyyy') <='{1}' order by  USERINFO.Name,CHECKINOUT.CHECKTIME", convert_date_to_eng_att(ttgl.EditValue), convert_date_to_eng_att(ttgl2.EditValue))

        Dim cn As OleDbConnection = Nothing

        grid1.DataSource = Nothing

        Try

            open_wait()

            dv1 = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_dbase_att(tlokasi.EditValue)

            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager = New DataViewManager(ds)
            dv1 = dvmanager.CreateDataView(ds.Tables(0))

            bs1 = New BindingSource
            bs1.DataSource = dv1

            grid1.DataSource = bs1

            close_wait()

        Catch ex As OleDb.OleDbException
            close_wait()
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally


            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

        End Try

    End Sub

    Private Sub btload_Click(sender As Object, e As EventArgs) Handles btload.Click
        opendata()
    End Sub

    Private Sub fdownload_fromatt_Load(sender As Object, e As EventArgs) Handles Me.Load

        ttgl.EditValue = Date.Now
        ttgl2.EditValue = Date.Now

        load_lokasi()

    End Sub

    Private Sub btproses_Click(sender As Object, e As EventArgs) Handles btproses.Click

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        Dim cn As OleDbConnection = Nothing
        Try

            open_wait()
            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = ""

            For i As Integer = 0 To dv1.Count - 1

                sql = String.Format("if exists " & _
                "(SELECT idmesin FROM ms_karyawan where idmesin='{0}') and not exists (select userid from ms_inout where userid='{0}' and checktime='{1}') " & _
                "begin " & _
                "insert into ms_inout (userid,checktime,verifymode,inoutmode,workcode) values('{0}','{1}',1,0,0) end; ",  dv1(i)("idmesin").ToString, convert_datetime_to_eng(dv1(i)("tanggal").ToString))

                Using cmd As OleDbCommand = New OleDbCommand(sql, cn)
                    cmd.ExecuteNonQuery()
                End Using

            Next

            close_wait()
            MsgBox("Data telah diproses...", vbOKOnly + vbInformation, "Informasi")


        Catch ex As Exception
            close_wait()
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally


            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try



    End Sub

End Class