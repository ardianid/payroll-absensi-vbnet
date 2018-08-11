Imports System.Data
Imports System.Data.OleDb
Imports Absensi.Clsmy

Public Class fgol_otomat3

    Public dt1 As DataTable
    Public dv As DataView
    Public jenisrange As String
    Public jeniskerja As String
    Public kode0 As String

    Private addaja As Boolean

    Public ReadOnly Property get_statada As String
        Get

            Return addaja

        End Get
    End Property

    Private Sub tambah()

        dv.Sort = "jmin"
        Dim hsil As Integer = dv.Find(tmin.EditValue.ToString)


        If hsil = -1 Then

            Dim orow As DataRowView = dv.AddNew
            orow("id") = 0
            orow("kode") = kode0
            orow("jmin") = tmin.EditValue
            orow("jmax") = tmax.EditValue
            orow("price") = tupah.EditValue
            orow("perkalian") = IIf(CheckEdit1.Checked, 1, 0)
            dv.EndInit()

            'Dim orow2 As DataRow = dt1.Rows.Add
            'orow2("id") = 0
            'orow2("kode") = kode0
            'orow2("jmin") = tmin.EditValue
            'orow2("jmax") = tmax.EditValue
            'orow2("price") = tupah.EditValue
            'orow2("perkalian") = IIf(CheckEdit1.Checked, 1, 0)
            'dt1.EndInit()


            tmin.EditValue = 0
            tmax.EditValue = 0
            tupah.EditValue = 0

            tmin.Focus()

            addaja = True

        Else
            MsgBox("Data sudah ada...", vbOKOnly + vbInformation, "Informasi")
        End If


    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub bttambah_Click(sender As System.Object, e As System.EventArgs) Handles bttambah.Click

        Dim jmin, jmax As Double
        jmin = tmin.EditValue
        jmax = tmax.EditValue

        If jmin = 0 Then
            MsgBox("Range min tidak boleh kosong..", vbOKOnly + vbInformation, "Informasi")
            tmin.Focus()
            Exit Sub
        End If

        If jmax = 0 Then
            MsgBox("Range max tidak boleh kosong..", vbOKOnly + vbInformation, "Informasi")
            tmax.Focus()
            Exit Sub
        End If

        If tupah.EditValue = 0.0 Then
            MsgBox("Upah tidak boleh kosong..", vbOKOnly + vbInformation, "Informasi")
            tupah.Focus()
            Exit Sub
        End If

        If jmin > jmax Then
            MsgBox("Range tidak sesuai..", vbOKOnly + vbInformation, "Informasi")
            tmin.Focus()
            Exit Sub
        End If

        tambah()

    End Sub

    Private Sub fgol_otomat3_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated

        addaja = False

        If jenisrange.Equals("Harian") Then
            tupah.Focus()
        Else
            tmin.Focus()
        End If
    End Sub

    Private Sub fgol_otomat3_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        addaja = False

        tjenis.Text = jeniskerja

        If jenisrange.Equals("Harian") Then
            tmin.EditValue = 1
            tmax.EditValue = 1
            tmin.Enabled = False
            tmax.Enabled = False
        Else
            tmin.EditValue = 0
            tmax.EditValue = 0
            tmin.Enabled = True
            tmax.Enabled = True
        End If

        
        tupah.EditValue = 0.0

    End Sub

End Class