Public Class fkontrak_fail 

    Private Sub load()

        Dim sql As String = "select nip,nama,depart,tgl_kon1,tgl_kon11,stat_kon1,tgl_kon2,tgl_kon22,stat_kon2,tgl_kon3,tgl_kon33,stat_kon3 " & _
        "from ms_karyawan where DATEDIFF(DAY,CONVERT(date, tgl_kon11),CONVERT(date,getdate())) >=90 " & _
        "or DATEDIFF(DAY,CONVERT(date, tgl_kon22),CONVERT(date,getdate())) >=90 " & _
        "or DATEDIFF(DAY,CONVERT(date, tgl_kon33),CONVERT(date,getdate())) >=90"


    End Sub

End Class