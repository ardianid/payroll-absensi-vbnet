Imports System.Data
Imports System.Data.OleDb
Imports System.Security.Cryptography
Imports System.Text
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions

Public Class Clsmy

    Private ReadOnly FileConf As String = Application.StartupPath & "\autocon.dll"


    Public Shared Function open_conn() As OleDbConnection

        Dim mloc As String = ""
        Dim mdbase As String = ""
        Dim muser As String = ""
        Dim mpwd As String = ""

        Dim cn = New OleDbConnection

        Try

            Dim fileReader As StreamReader = New StreamReader(Application.StartupPath & "\autocon.dll")

            For i As Integer = 0 To 3

                Select Case i
                    Case 0
                        mloc = fileReader.ReadLine
                    Case 1
                        mdbase = fileReader.ReadLine
                    Case 2
                        muser = fileReader.ReadLine
                    Case 3
                        mpwd = fileReader.ReadLine
                End Select

            Next

            'If mloc.Trim.Length > 0 Then
            '    mloc = Decrypt(mloc)
            'End If

            'If mdbase.Trim.Length > 0 Then
            '    mdbase = Decrypt(mdbase)
            'End If

            'If muser.Trim.Length > 0 Then
            '    muser = Decrypt(muser)
            'End If

            'If mpwd.Trim.Length > 0 Then
            '    mpwd = Decrypt(mpwd)
            'End If

            fileReader.Close()

            Dim myconnectionstring As String = String.Format("Provider=SQLOLEDB;Server={0};Database={1};Uid={2};Pwd={3}; max pool size=500", mloc, mdbase, muser, mpwd)

            cn.ConnectionString = myconnectionstring
            cn.Open()

        Catch ex As OleDb.OleDbException
            Throw New Exception(ex.ToString)
        End Try

        Return cn

    End Function

    Public Shared Function open_dbase_att(ByVal lokasi As String) As OleDbConnection

        Dim mloc As String = ""
        Dim mdbase As String = ""
        Dim muser As String = ""
        Dim mpwd As String = ""

        Dim cn = New OleDbConnection

        Try

            'Dim myconnectionstring As String = String.Format("Provider=SQLOLEDB;Server={0};Database={1};Uid={2};Pwd={3}; max pool size=500", mloc, mdbase, muser, mpwd)
            '  Dim myconnectionstring As String = String.Format(String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Jet OLEDB:Database Password='';", lokasi))

            Dim myconnectionstring As String = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}", lokasi)

            cn.ConnectionString = myconnectionstring
            cn.Open()

        Catch ex As OleDb.OleDbException
            Throw New Exception(ex.ToString)
        End Try

        Return cn

    End Function


    Public Shared Function open_conn_test(ByVal lokasi As String, ByVal datab As String, ByVal suser As String, ByVal pawd As String) As OleDbConnection


        Dim cn = New OleDbConnection

        Try


            Dim myconnectionstring As String = String.Format("Provider=SQLOLEDB;Server={0};Database={1};Uid={2};Pwd={3}; max pool size=500", lokasi, datab, suser, pawd)

            cn.ConnectionString = myconnectionstring
            cn.Open()

        Catch ex As OleDb.OleDbException
            Throw New Exception(ex.ToString)
        End Try

        Return cn

    End Function

    Public Shared Function GetDataSet(ByVal SQL As String, ByVal cn As OleDbConnection) As DataSet

        Dim adapter As New OleDbDataAdapter(SQL, cn)
        Dim myData As New DataSet
        adapter.Fill(myData)

        adapter.Dispose()

        Return myData
    End Function

    Public Shared Function GetDataSet(ByVal Cmd As OleDbCommand, ByVal cn As OleDbConnection) As DataSet

        Dim adapter As New OleDbDataAdapter(Cmd)
        Dim myData As New DataSet
        adapter.Fill(myData)

        adapter.Dispose()

        Return myData
    End Function

    Private Function GenerateHash(ByVal SourceText As String) As String
        'Create an encoding object to ensure the encoding standard for the source text
        Dim Ue As New UnicodeEncoding()
        'Retrieve a byte array based on the source text
        Dim ByteSourceText() As Byte = Ue.GetBytes(SourceText)
        'Instantiate an MD5 Provider object
        Dim Md5 As New MD5CryptoServiceProvider()
        'Compute the hash value from the source
        Dim ByteHash() As Byte = Md5.ComputeHash(ByteSourceText)
        'And convert it to String format for return
        Return Convert.ToBase64String(ByteHash)
    End Function

    Public Shared Sub InsertToLog(ByVal cn As OleDbConnection, ByVal kodemenu As String, ByVal isinsert As Int32, _
                                  ByVal isupdate As Int32, ByVal isdelete As Int32, ByVal isprint As Int32, ByVal nobukti As String, _
                                  ByVal info2 As String, ByVal sqltrans As OleDbTransaction)

        Dim sinfo2 As String = ""
        If info2.Trim.Length > 0 Then
            If IsDate(info2) Then
                sinfo2 = convert_date_to_eng(info2)
            Else
                sinfo2 = info2
            End If
        End If

        Dim sql As String = String.Format("insert into ms_logact (userid,kodemenu,isinsert,isupdate,isdelete,isprint,nobukti,waktu,info2) values(" & _
                                    "'{0}','{1}',{2},{3},{4},{5},'{6}','{7}','{8}')", userprog, kodemenu, isinsert, isupdate, isdelete, isprint, nobukti, convert_datetime_to_eng(Date.Now), sinfo2)

        If IsNothing(sqltrans) Then
            Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
            comd.ExecuteNonQuery()
        Else
            Dim comd1 As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
            comd1.ExecuteNonQuery()
        End If


    End Sub

    'Private Sub ReadText()

    '    Dim fileReader As StreamReader

    '    fileReader = New StreamReader(FileConf)

    '    For i As Integer = 0 To 3

    '        Select Case i
    '            Case 0
    '                mloc = fileReader.ReadLine
    '            Case 1
    '                mdbase = fileReader.ReadLine
    '            Case 2
    '                muser = fileReader.ReadLine
    '            Case 3
    '                muser = fileReader.ReadLine
    '        End Select

    '    Next

    '    If mloc.Trim.Length > 0 Then
    '        mloc = Decrypt(mloc)
    '    End If

    '    If mdbase.Trim.Length > 0 Then
    '        mdbase = Decrypt(mdbase)
    '    End If

    '    If muser.Trim.Length > 0 Then
    '        muser = Decrypt(muser)
    '    End If

    '    If mpwd.Trim.Length > 0 Then
    '        mpwd = Decrypt(mpwd)
    '    End If

    '    fileReader.Close()

    'End Sub

    Public Shared Function WriteContent(ByVal content As String, ByVal sdbase As String, ByVal suser As String, ByVal spwd As String) As String
        Dim fileWriter As StreamWriter
        Try
            fileWriter = New StreamWriter(Application.StartupPath & "\autocon.dll")
            fileWriter.WriteLine(content)
            fileWriter.WriteLine(sdbase)
            fileWriter.WriteLine(suser)
            fileWriter.WriteLine(spwd)
            fileWriter.Close()

            Return "ok"

        Catch x As Exception
            Throw New Exception(x.ToString)
        End Try
    End Function

    

    Public Shared Function Encrypt(ByVal pass As String) As String

        Const input As String = "brave123"

        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim encrypted As String = ""
        Try
            Dim hash(31) As Byte
            Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(pass))
            Array.Copy(temp, 0, hash, 0, 16)
            Array.Copy(temp, 0, hash, 15, 16)
            AES.Key = hash
            AES.Mode = Security.Cryptography.CipherMode.ECB
            Dim DESEncrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
            Dim Buffer As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(input)
            encrypted = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Return encrypted
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Function

    'Public Shared Function Decrypt(ByVal CipherText As String) As String

    '    Const password As String = "brave123"

    '    Dim HashAlgorithm As String = "SHA1"
    '    Dim PasswordIterations As String = 2
    '    Dim InitialVector As String = "OFRna73m*aze01xY"
    '    Dim KeySize As Integer = 256

    '    If (String.IsNullOrEmpty(CipherText)) Then
    '        Return ""
    '    End If
    '    Dim InitialVectorBytes As Byte() = Encoding.ASCII.GetBytes(InitialVector)
    '    Dim SaltValueBytes As Byte() = Encoding.ASCII.GetBytes(salt)
    '    Dim CipherTextBytes As Byte() = Convert.FromBase64String(CipherText)
    '    Dim DerivedPassword As PasswordDeriveBytes = New PasswordDeriveBytes(password, SaltValueBytes, HashAlgorithm, PasswordIterations)
    '    Dim KeyBytes As Byte() = DerivedPassword.GetBytes(KeySize / 8)
    '    Dim SymmetricKey As RijndaelManaged = New RijndaelManaged()
    '    SymmetricKey.Mode = CipherMode.CBC
    '    Dim PlainTextBytes As Byte() = New Byte(CipherTextBytes.Length - 1) {}

    '    Dim ByteCount As Integer = 0

    '    Using Decryptor As ICryptoTransform = SymmetricKey.CreateDecryptor(KeyBytes, InitialVectorBytes)
    '        Using MemStream As MemoryStream = New MemoryStream(CipherTextBytes)
    '            Using CryptoStream As CryptoStream = New CryptoStream(MemStream, Decryptor, CryptoStreamMode.Read)
    '                ByteCount = CryptoStream.Read(PlainTextBytes, 0, PlainTextBytes.Length)
    '                MemStream.Close()
    '                CryptoStream.Close()
    '            End Using
    '        End Using
    '    End Using
    '    SymmetricKey.Clear()
    '    Return Encoding.UTF8.GetString(PlainTextBytes, 0, ByteCount)
    'End Function

End Class
