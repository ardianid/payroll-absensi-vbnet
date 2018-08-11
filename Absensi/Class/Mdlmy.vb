Imports DevExpress.XtraSplashScreen
Imports System.Globalization

Imports System.Windows.Forms
Imports System.Reflection

Module Mdlmy

    Public userprog, pwd, xjenisuser As String
    Public dtmenu As DataTable
    Public dtmenu2 As DataTable

    Public xgapok As Integer
    Public xgapok_jmsos As Integer
    Public xtunj_jab As Integer
    Public xtunj_hdr As Integer
    Public xtunj_trans As Integer
    Public xtunj_makan As Integer
    Public xtamb_makan As Integer
    Public xform As Integer
    Public xlap As Integer
    Public xnonkary As Integer

    '    Private Dlg As DevExpress.Utils.WaitDialogForm = Nothing

    Public Class ObjectFinder
        Public Shared Function CreateObjectInstance(ByVal objectName As String) As Object
            ' Creates and returns an instance of any object in the assembly by its type name.

            Dim obj As Object

            Try
                If objectName.LastIndexOf(".") = -1 Then
                    'Appends the root namespace if not specified.
                    objectName = String.Format("{0}.{1}", [Assembly].GetEntryAssembly.GetName.Name, objectName)
                End If

                obj = [Assembly].GetEntryAssembly.CreateInstance(objectName)

            Catch ex As Exception
                obj = Nothing
            End Try
            Return obj

        End Function

        Public Shared Function CreateForm(ByVal formName As String) As Form
            ' Return the instance of the form by specifying its name.
            Return DirectCast(CreateObjectInstance(formName), Form)
        End Function

    End Class

    Public Sub open_wait()

        SplashScreenManager.ShowForm(futama, GetType(waitf), True, True, False)
        ' SplashScreenManager.Default.SetWaitFormDescription("lagi ngetest")

        '  Dlg = New DevExpress.Utils.WaitDialogForm("Loading Components...")
    End Sub

    Public Sub open_wait(ByVal capt As String)

        SplashScreenManager.ShowForm(futama, GetType(waitf), True, True, False)
        SplashScreenManager.Default.SetWaitFormDescription(capt)

        '  Dlg = New DevExpress.Utils.WaitDialogForm("Loading Components...")
    End Sub

    Public Sub SetWaitDialog(ByVal capt As String)

        ' SplashScreenManager.ShowForm(futama, GetType(waitf), True, True, False)
        SplashScreenManager.Default.SetWaitFormDescription(capt)

        '  Dlg = New DevExpress.Utils.WaitDialogForm("Loading Components...")
    End Sub

    Public Sub open_wait2(ByVal formm As Form)
        SplashScreenManager.ShowForm(formm, GetType(waitf), True, True, False)
    End Sub

    Public Sub close_wait()
        SplashScreenManager.CloseForm(False)
    End Sub

    Public Function convert_date_to_eng(ByVal valdate As String) As String

        If valdate = "" Then
            Return ""
        End If

        If valdate.Equals("__/__/____") Then
            Return ""
        End If

        If valdate.Equals("  /  /") Then
            Return ""
        End If

        valdate = CType(valdate, DateTime).ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US"))

        Return valdate

    End Function

    Public Function convert_date_to_eng_att(ByVal valdate As String) As String

        If valdate = "" Then
            Return ""
        End If

        valdate = CType(valdate, DateTime).ToString("MM/dd/yyyy", CultureInfo.CreateSpecificCulture("en-US"))

        Return valdate

    End Function

    Public Function convert_datetime_to_eng(ByVal valdate As String) As String

        If valdate = "" Then
            Return ""
        End If

        If valdate.Equals("__/__/____") Then
            Return ""
        End If

        If valdate.Equals("  /  /") Then
            Return ""
        End If

        valdate = CType(valdate, DateTime).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))

        Return valdate

    End Function


    Public Function convert_date_to_ind(ByVal valdate As String) As String

        If valdate = "" Then
            Return ""
        End If

        valdate = CType(valdate, Date).ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("id-ID"))

        Return valdate

    End Function

    Public Function hitung_selisih_jam(ByVal jamawal As String, ByVal jamakhir As String, ByVal hasilkeluar As String) As Integer

        jamawal = TimeValue(jamawal).ToString("HH:mm") ' String.Format("{0}:00", Microsoft.VisualBasic.Left(jamawal, 5))
        jamakhir = TimeValue(jamakhir).ToString("HH:mm")  'String.Format("{0}:00", Microsoft.VisualBasic.Left(jamakhir, 5))

        Dim hasil As Integer = 0

        Dim jam1 As Date
        Dim jam2 As Date

        Dim tengahmalem As Integer = 0

        If TimeValue("23:59:59") > TimeValue(jamawal) And TimeValue("15:00:00") < TimeValue(jamawal) And _
            TimeValue("00:00:00") < TimeValue(jamakhir) And TimeValue("11:00:00") > TimeValue(jamakhir) Then

            tengahmalem = 1

        End If

        jam1 = Convert.ToDateTime(String.Format("12/12/2012 {0}", jamawal))

        If tengahmalem = 1 Then
            jam2 = Convert.ToDateTime(String.Format("13/12/2012 {0}", jamakhir))

            Dim jamtengah1 As Date = Convert.ToDateTime("12/12/2012 23:59:59")
            Dim jamtengah2 As Date = Convert.ToDateTime("13/12/2012 00:00:00")

            Dim hasil1 As TimeSpan = jamtengah1.Subtract(jam1)
            Dim hasil2 As TimeSpan = jam2.Subtract(jamtengah2)

            hasil = hasil1.TotalSeconds + hasil2.TotalSeconds + 1

        Else
            jam2 = Convert.ToDateTime(String.Format("12/12/2012 {0}", jamakhir))

            Dim hasil1 As TimeSpan = jam2.Subtract(jam1)

            hasil = hasil1.TotalSeconds

        End If

        If hasilkeluar.Equals("h") Then
            hasil = hasil / 60
            hasil = hasil / 60
        ElseIf hasilkeluar.Equals("m") Then
            hasil = hasil / 60
        ElseIf hasilkeluar.Equals("s") Then
            hasil = hasil
        End If

        Return hasil

    End Function

    Public Function cek_lewat_tngahmalem(ByVal jamawal As String, ByVal jamakhir As String) As Integer
        Dim hasil As Integer = 0

        jamawal = TimeValue(jamawal).ToString("HH:mm") ' String.Format("{0}:00", Microsoft.VisualBasic.Left(jamawal, 5))
        jamakhir = TimeValue(jamakhir).ToString("HH:mm")  'String.Format("{0}:00", Microsoft.VisualBasic.Left(jamakhir, 5))

        If TimeValue("23:59:59") > TimeValue(jamawal) And TimeValue("15:00:00") < TimeValue(jamawal) And _
            TimeValue("00:00:00") < TimeValue(jamakhir) And TimeValue("11:00:00") > TimeValue(jamakhir) Then

            hasil = 1

        End If

        Return hasil
    End Function

End Module
