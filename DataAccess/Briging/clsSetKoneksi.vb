Imports DataAccess.My.Resources
Imports System.Security.Cryptography
Imports System.Text
Imports LZStringVBNet
Imports System.IO
Imports System.IO.Compression

Namespace Brigging
    Public Class clsSetKoneksi

#Region "Function"
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        'Public sMODUL As String = ""
        'Public sREFERENCE As String = ""
        'Public sSTATUS As String = ""
        'Public sLASTNUMBER As Integer = 0

        Public Sub New(Optional ByVal sConnection As String = "")
            If sConnection = "" Then
                oConnection = New Setting.clsConnectionMain
                oError = New Setting.clsError
            Else
                oConnection = New Setting.clsConnectionMain("TAX")
                oError = New Setting.clsError("TAX")
            End If

            'sMODUL = "SET_KONEKSI"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetData() As List(Of SET_KONEKSI)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.SET_KONEKSIs.OrderBy(Function(x) x.KDKONEKSI).ToList()
        End Function

        'Public Function GetStructureHeader() As SET_KONEKSI
        '    If Not oConnection.GetConnection() Then
        '        GetStructureHeader = Nothing
        '    End If
        '    GetStructureHeader = New SET_KONEKSI
        'End Function
        'Public Function GetData() As List(Of SET_KONEKSI)
        '    If Not oConnection.GetConnection() Then
        '        GetData = Nothing
        '        Exit Function
        '    End If
        '    GetData = oConnection.db.SET_KONEKSIs.OrderBy(Function(x) x.KDKONEKSI).ToList()
        'End Function
        'Public Function GetData(ByVal sKDKONEKSI As String) As SET_KONEKSI
        '    If Not oConnection.GetConnection() Then
        '        GetData = Nothing
        '        Exit Function
        '    End If
        '    GetData = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.KDKONEKSI = sKDKONEKSI)
        'End Function
        'Public Function GetDataAktiveByNameDisplay(ByVal Parameter As String) As SET_KONEKSI
        '    If Not oConnection.GetConnection() Then
        '        GetDataAktiveByNameDisplay = Nothing
        '        Exit Function
        '    End If
        '    GetDataAktiveByNameDisplay = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = Parameter And x.ISACTIVE = True)
        'End Function
        Public Function GetDataFooter(ByVal SKDREPORT As String) As SET_REPORT
            If Not oConnection.GetConnection() Then
                GetDataFooter = Nothing
                Exit Function
            End If
            GetDataFooter = oConnection.db.SET_REPORTs.FirstOrDefault(Function(x) x.KDREPORT = SKDREPORT)
        End Function
        'Public Function IsExist(ByVal sNAME_DISPLAY As String) As Boolean
        '    If Not oConnection.GetConnection() Then
        '        IsExist = False
        '        Exit Function
        '    End If

        '    Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY)

        '    If ds IsNot Nothing Then
        '        IsExist = True
        '    Else
        '        IsExist = False
        '    End If
        'End Function
        'Public Function InsertData(ByVal entity As SET_KONEKSI) As Boolean
        '    Try
        '        If Not oConnection.GetConnection() Then
        '            InsertData = False
        '            Exit Function
        '        End If

        '        sREFERENCE = entity.KDKONEKSI
        '        sSTATUS = "INSERT"

        '        'Generate Auto Number
        '        Try
        '            sLASTNUMBER = CInt(oConnection.db.SET_KONEKSIs.OrderByDescending(Function(x) x.KDKONEKSI).FirstOrDefault().KDKONEKSI.Remove(0, (sMODUL & " _ ").Length)) + 1
        '        Catch ex As Exception
        '            sLASTNUMBER = 1
        '        End Try
        '        'End Generate

        '        Try
        '            entity.KDKONEKSI = sMODUL & "_" & AutoNumberCode(sLASTNUMBER)
        '            oConnection.db.SET_KONEKSIs.InsertOnSubmit(entity)
        '        Catch ex As Exception
        '            
        '            Throw ex
        '        End Try
        '        Try
        '            oConnection.db.SubmitChanges()
        '        Catch ex As Exception
        '            
        '            Throw ex
        '        End Try

        '        InsertData = True
        '    Catch ex As Exception
        '        InsertData = False
        '        
        '        Throw ex
        '    End Try
        'End Function
        'Public Function UpdateData(ByVal entity As SET_KONEKSI) As Boolean
        '    Try
        '        If Not oConnection.GetConnection() Then
        '            UpdateData = False
        '            Exit Function
        '        End If

        '        sREFERENCE = entity.KDKONEKSI
        '        sSTATUS = "UPDATE"

        '        Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.KDKONEKSI = entity.KDKONEKSI)

        '        Try
        '            oConnection.db.SET_KONEKSIs.DeleteOnSubmit(ds)
        '            oConnection.db.SET_KONEKSIs.InsertOnSubmit(entity)
        '        Catch ex As Exception
        '            
        '            Throw ex
        '        End Try
        '        Try
        '            oConnection.db.SubmitChanges()
        '        Catch ex As Exception
        '            
        '            Throw ex
        '        End Try

        '        UpdateData = True
        '    Catch ex As Exception
        '        UpdateData = False
        '        
        '        Throw ex
        '    End Try
        'End Function
        'Public Function DeleteData(ByVal sKDKONEKSI As String) As Boolean
        '    Try
        '        If Not oConnection.GetConnection() Then
        '            DeleteData = False
        '            Exit Function
        '        End If

        '        sREFERENCE = sKDKONEKSI
        '        sSTATUS = "DELETE"

        '        Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.KDKONEKSI = sKDKONEKSI)

        '        Try
        '            oConnection.db.SET_KONEKSIs.DeleteOnSubmit(ds)
        '        Catch ex As Exception
        '            
        '            Throw ex
        '        End Try
        '        Try
        '            oConnection.db.SubmitChanges()
        '        Catch ex As Exception
        '            
        '            Throw ex
        '        End Try

        '        DeleteData = True
        '    Catch ex As Exception
        '        DeleteData = False
        '        
        '        Throw ex
        '    End Try
        'End Function
#End Region
#Region "DECRYPTE"
        Public Function Decrypt(ByVal data As String, ByVal key As String) As String
            Dim decData As String = Nothing
            Dim keys As Byte()() = GetHashKeys(key)

            Try
                decData = LZString.DecompressFromEncodedUriComponent(DecryptStringFromBytes_Aes(data, keys(0), keys(1)))
            Catch oErr As Exception
                'MsgBox("Load Petugas Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
            Return decData
        End Function
        Private Shared Function DecryptStringFromBytes_Aes(ByVal cipherTextString As String, ByVal Key As Byte(), ByVal IV As Byte()) As String
            Dim cipherText As Byte() = Convert.FromBase64String(cipherTextString)
            If cipherText Is Nothing OrElse cipherText.Length <= 0 Then Throw New ArgumentNullException("cipherText")
            If Key Is Nothing OrElse Key.Length <= 0 Then Throw New ArgumentNullException("Key")
            If IV Is Nothing OrElse IV.Length <= 0 Then Throw New ArgumentNullException("IV")
            Dim plaintext As String = Nothing

            Using aesAlg As Aes = Aes.Create()
                aesAlg.Key = Key
                aesAlg.IV = IV
                Dim decryptor As ICryptoTransform = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV)

                Using msDecrypt As IO.MemoryStream = New IO.MemoryStream(cipherText)

                    Using csDecrypt As CryptoStream = New CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)

                        Using srDecrypt As IO.StreamReader = New IO.StreamReader(csDecrypt)
                            plaintext = srDecrypt.ReadToEnd()
                        End Using
                    End Using
                End Using
            End Using

            Return plaintext
        End Function
        Private Function GetHashKeys(ByVal key As String) As Byte()()
            Dim result As Byte()() = New Byte(1)() {}
            Dim enc As Encoding = Encoding.UTF8
            Dim sha2 As SHA256 = New SHA256CryptoServiceProvider()
            Dim rawKey As Byte() = enc.GetBytes(key)
            Dim rawIV As Byte() = enc.GetBytes(key)
            Dim hashKey As Byte() = sha2.ComputeHash(rawKey)
            Dim hashIV As Byte() = sha2.ComputeHash(rawIV)
            Array.Resize(hashIV, 16)
            result(0) = hashKey
            result(1) = hashIV
            Return result
        End Function
#End Region
#Region "VCLAIM2"
#Region "Referensi"
        Public Function GetDataVClaimReferensiDiagnosa(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiDiagnosa = ""
                    Exit Function
                End If

                'Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim HasilKey As String = ""
                Dim result As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "referensi/diagnosa/" & Parameter & ""

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                result = req.ResponseText

                GetDataVClaimReferensiDiagnosa = result

            Catch ex As Exception
                GetDataVClaimReferensiDiagnosa = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimReferensiPoli(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiPoli = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""
                Dim result As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "referensi/poli/" & Parameter

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                result = req.ResponseText

                GetDataVClaimReferensiPoli = result

            Catch ex As Exception
                GetDataVClaimReferensiPoli = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimReferensiFasilitasKesehatan(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter1 As String, ByVal Parameter2 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiFasilitasKesehatan = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""
                Dim result As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "referensi/faskes/" & Parameter1 & "/" & Parameter2

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                result = req.ResponseText

                GetDataVClaimReferensiFasilitasKesehatan = result

            Catch ex As Exception
                GetDataVClaimReferensiFasilitasKesehatan = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimReferensiDokterDPJP(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter1 As String, ByVal Parameter2 As String, ByVal Parameter3 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiDokterDPJP = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""
                Dim result As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "referensi/dokter/pelayanan/" & Parameter1 & "/tglPelayanan/" & Parameter2 & "/Spesialis/" & Parameter3

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                result = req.ResponseText

                GetDataVClaimReferensiDokterDPJP = result

            Catch ex As Exception
                GetDataVClaimReferensiDokterDPJP = ex.ToString

                Throw ex
            End Try

        End Function
        Public Function GetDataVClaimReferensiPropinsi(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiPropinsi = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""
                Dim result As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "referensi/propinsi"

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                result = req.ResponseText

                GetDataVClaimReferensiPropinsi = result

            Catch ex As Exception
                GetDataVClaimReferensiPropinsi = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimReferensiKabupaten(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiKabupaten = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""
                Dim result As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "referensi/kabupaten/propinsi/" & Parameter

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                result = req.ResponseText

                GetDataVClaimReferensiKabupaten = result

            Catch ex As Exception
                GetDataVClaimReferensiKabupaten = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimReferensiKecamatan(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiKecamatan = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""
                Dim result As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "referensi/kecamatan/kabupaten/" & Parameter

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                result = req.ResponseText

                GetDataVClaimReferensiKecamatan = result

            Catch ex As Exception
                GetDataVClaimReferensiKecamatan = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimReferensiDiagnosa_PRB(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiDiagnosa_PRB = ""
                    Exit Function
                End If

                'Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim HasilKey As String = ""
                Dim result As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "referensi/diagnosaprb"

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                result = req.ResponseText

                GetDataVClaimReferensiDiagnosa_PRB = result

            Catch ex As Exception
                GetDataVClaimReferensiDiagnosa_PRB = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimReferensiObatGenerikProgramPRB(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiObatGenerikProgramPRB = ""
                    Exit Function
                End If

                'Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim HasilKey As String = ""
                Dim result As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "referensi/obatprb/" & Parameter

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                result = req.ResponseText

                GetDataVClaimReferensiObatGenerikProgramPRB = result

            Catch ex As Exception
                GetDataVClaimReferensiObatGenerikProgramPRB = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimReferensiProcedure(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiProcedure = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""
                Dim result As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "referensi/procedure/" & Parameter

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                result = req.ResponseText

                GetDataVClaimReferensiProcedure = result

            Catch ex As Exception
                GetDataVClaimReferensiProcedure = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimReferensiKelasRawat(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiKelasRawat = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""
                Dim result As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "referensi/kelasrawat"

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                result = req.ResponseText

                GetDataVClaimReferensiKelasRawat = result

            Catch ex As Exception
                GetDataVClaimReferensiKelasRawat = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimReferensiDokter(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiDokter = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""
                Dim result As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "referensi/dokter/" & Parameter

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                result = req.ResponseText

                GetDataVClaimReferensiDokter = result

            Catch ex As Exception
                GetDataVClaimReferensiDokter = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimReferensiSpesialistik(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiSpesialistik = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""
                Dim result As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "referensi/spesialistik"

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                result = req.ResponseText

                GetDataVClaimReferensiSpesialistik = result

            Catch ex As Exception
                GetDataVClaimReferensiSpesialistik = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimReferensiRuangRawat(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiRuangRawat = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""
                Dim result As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "referensi/ruangrawat"

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                result = req.ResponseText

                GetDataVClaimReferensiRuangRawat = result

            Catch ex As Exception
                GetDataVClaimReferensiRuangRawat = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimReferensiCaraKeluar(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiCaraKeluar = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""
                Dim result As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "referensi/carakeluar"

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                result = req.ResponseText

                GetDataVClaimReferensiCaraKeluar = result

            Catch ex As Exception
                GetDataVClaimReferensiCaraKeluar = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimReferensipascapulang(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensipascapulang = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""
                Dim result As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "referensi/pascapulang"

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                result = req.ResponseText

                GetDataVClaimReferensipascapulang = result

            Catch ex As Exception
                GetDataVClaimReferensipascapulang = ex.ToString

                Throw ex
            End Try
        End Function
#End Region
#Region "Peserta"
        Public Function GetDataVClaimPesertaNoKartuBPJS(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter1 As String, ByVal Parameter2 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimPesertaNoKartuBPJS = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "Peserta/nokartu/" & Parameter1 & "/tglSEP/" & Parameter2

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                GetDataVClaimPesertaNoKartuBPJS = req.ResponseText

            Catch ex As Exception
                GetDataVClaimPesertaNoKartuBPJS = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimPesertaNIK(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter1 As String, ByVal Parameter2 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimPesertaNIK = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "Peserta/nik/" & Parameter1 & "/tglSEP/" & Parameter2

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                GetDataVClaimPesertaNIK = req.ResponseText

            Catch ex As Exception
                GetDataVClaimPesertaNIK = ex.ToString

                Throw ex
            End Try
        End Function
#End Region
#Region "PRB"
        Public Function InsertPRB(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertPRB = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "PRB/insert"

                req = New WinHttp.WinHttpRequest
                req.Open("POST", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                InsertPRB = req.ResponseText

            Catch ex As Exception
                InsertPRB = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function UpdatePRB(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    UpdatePRB = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "PRB/Update"

                req = New WinHttp.WinHttpRequest
                req.Open("PUT", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                UpdatePRB = req.ResponseText

            Catch ex As Exception
                UpdatePRB = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function HapusPRB(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    HapusPRB = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "PRB/Delete"

                req = New WinHttp.WinHttpRequest
                req.Open("DELETE", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                HapusPRB = req.ResponseText

            Catch ex As Exception
                HapusPRB = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function NomorSRB(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter1 As String, ByVal Parameter2 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    NomorSRB = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "prb/" & Parameter1 & "/nosep/" & Parameter2

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                NomorSRB = req.ResponseText

            Catch ex As Exception
                NomorSRB = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function TanggalSRB(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter1 As String, ByVal Parameter2 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    TanggalSRB = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "prb/tglMulai/" & Parameter1 & "/tglAkhir/" & Parameter2

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                TanggalSRB = req.ResponseText

            Catch ex As Exception
                TanggalSRB = ex.ToString

                Throw ex
            End Try
        End Function
#End Region
#Region "SEP"
#Region "Pembuatan SEP"
        Public Function InsertSEP(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertSEP = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "SEP/1.1/insert"

                req = New WinHttp.WinHttpRequest
                req.Open("POST", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                InsertSEP = req.ResponseText

            Catch ex As Exception
                InsertSEP = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function UpdateSEP(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    UpdateSEP = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "SEP/1.1/Update"

                req = New WinHttp.WinHttpRequest
                req.Open("PUT", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                UpdateSEP = req.ResponseText

            Catch ex As Exception
                UpdateSEP = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function HapusSEP(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    HapusSEP = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "SEP/Delete"

                req = New WinHttp.WinHttpRequest
                req.Open("DELETE", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                HapusSEP = req.ResponseText

            Catch ex As Exception
                HapusSEP = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function CariSEP(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    CariSEP = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "SEP/" & Parameter

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send()

                CariSEP = req.ResponseText

            Catch ex As Exception
                CariSEP = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function InsertSEPv2(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertSEPv2 = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "SEP/2.0/insert"

                req = New WinHttp.WinHttpRequest
                req.Open("POST", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")
                '
                req.Send(Request)

                InsertSEPv2 = req.ResponseText

            Catch ex As Exception
                InsertSEPv2 = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function UpdateSEPv2(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    UpdateSEPv2 = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "SEP/2.0/update"

                req = New WinHttp.WinHttpRequest
                req.Open("PUT", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                UpdateSEPv2 = req.ResponseText

            Catch ex As Exception
                UpdateSEPv2 = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function HapusSEPv2(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    HapusSEPv2 = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "SEP/2.0/delete"

                req = New WinHttp.WinHttpRequest
                req.Open("DELETE", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                HapusSEPv2 = req.ResponseText

            Catch ex As Exception
                HapusSEPv2 = ex.ToString

                Throw ex
            End Try
        End Function
#End Region
#Region "Potensi Suplesi Jasa Raharja"
        Public Function GetDataSuplesiJasaRaharja(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter1 As String, ByVal Parameter2 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataSuplesiJasaRaharja = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "sep/JasaRaharja/Suplesi/" & Parameter1 & "/tglPelayanan/" & Parameter2

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                GetDataSuplesiJasaRaharja = req.ResponseText

            Catch ex As Exception
                GetDataSuplesiJasaRaharja = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function GetDataDataIndukKecelakaan(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter1 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataDataIndukKecelakaan = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "sep/KllInduk/List/" & Parameter1

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                GetDataDataIndukKecelakaan = req.ResponseText

            Catch ex As Exception
                GetDataDataIndukKecelakaan = ex.ToString

                Throw ex
            End Try
        End Function
#End Region
#Region "Approval Penjaminan SEP"
        Public Function Approval_Pengajuan(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    Approval_Pengajuan = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "Sep/pengajuanSEP"

                req = New WinHttp.WinHttpRequest
                req.Open("POST", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                Approval_Pengajuan = req.ResponseText

            Catch ex As Exception
                Approval_Pengajuan = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function Approval_ApprovalPengajuanSEP(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    Approval_ApprovalPengajuanSEP = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "Sep/aprovalSEP"

                req = New WinHttp.WinHttpRequest
                req.Open("POST", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                Approval_ApprovalPengajuanSEP = req.ResponseText

            Catch ex As Exception
                Approval_ApprovalPengajuanSEP = ex.ToString

                Throw ex
            End Try
        End Function
#End Region
#Region "Update Tgl Pulang SEP"
        'Public Function UpdateTanggalPulang(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
        '    Try
        '        If Not oConnection.GetConnection() Then
        '            UpdateTanggalPulang = ""
        '            Exit Function
        '        End If

        '        Dim HasilKey As String = ""

        '        ' Initialize the keyed hash object using the secret key as the key
        '        Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
        '        ' Computes the signature by hashing the salt with the secret key as the key
        '        Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
        '        ' Base 64 Encode
        '        HasilKey = Convert.ToBase64String(signature)

        '        Dim req As WinHttp.WinHttpRequest

        '        Dim Url As String = ALAMATWEB & "Sep/updtglplg"

        '        req = New WinHttp.WinHttpRequest
        '        req.Open("PUT", Url, False)
        '        req.SetRequestHeader("X-cons-ID", CONSID)
        '        req.SetRequestHeader("X-timestamp", uTime)
        '        req.SetRequestHeader("X-signature", HasilKey)
        '        req.SetRequestHeader("user_key", USERKEY)
        '        req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

        '        req.Send(Request)

        '        UpdateTanggalPulang = req.ResponseText

        '    Catch ex As Exception
        '        UpdateTanggalPulang = ex.ToString

        '        Throw ex
        '    End Try
        'End Function
        Public Function UpdateTanggalPulangv2(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    UpdateTanggalPulangv2 = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "SEP/2.0/updtglplgg"

                req = New WinHttp.WinHttpRequest
                req.Open("PUT", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                UpdateTanggalPulangv2 = req.ResponseText

            Catch ex As Exception
                UpdateTanggalPulangv2 = ex.ToString

                Throw ex
            End Try
        End Function
#End Region
#Region "Integrasi SEP dan Inacbg"
        Public Function IntegrasiSEPdenganInacbg(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    IntegrasiSEPdenganInacbg = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "/sep/cbg/" & Parameter

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send()

                IntegrasiSEPdenganInacbg = req.ResponseText

            Catch ex As Exception
                IntegrasiSEPdenganInacbg = ex.ToString

                Throw ex
            End Try
        End Function
#End Region
#Region "LPK"
        Public Function InsertLPK(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertLPK = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "LPK/insert"

                req = New WinHttp.WinHttpRequest
                req.Open("POST", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                InsertLPK = req.ResponseText

            Catch ex As Exception
                InsertLPK = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function UpdateLPK(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    UpdateLPK = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "LPK/update"

                req = New WinHttp.WinHttpRequest
                req.Open("PUT", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                UpdateLPK = req.ResponseText

            Catch ex As Exception
                UpdateLPK = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function DeleteLPK(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    DeleteLPK = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "LPK/delete"

                req = New WinHttp.WinHttpRequest
                req.Open("DELETE", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                DeleteLPK = req.ResponseText

            Catch ex As Exception
                DeleteLPK = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function DataLembarPengajuanKlaim(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter1 As String, ByVal Parameter2 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    DataLembarPengajuanKlaim = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "LPK/TglMasuk/" & Parameter1 & "/JnsPelayanan/" & Parameter2

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send()

                DataLembarPengajuanKlaim = req.ResponseText

            Catch ex As Exception
                DataLembarPengajuanKlaim = ex.ToString

                Throw ex
            End Try
        End Function
#End Region
#Region "SEP Internal"
        Public Function DataSEPInternal(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    DataSEPInternal = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "SEP/Internal/" & Parameter

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send()

                DataSEPInternal = req.ResponseText

            Catch ex As Exception
                DataSEPInternal = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function HapusSEPInternal(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    HapusSEPInternal = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "SEP/Internal/delete"

                req = New WinHttp.WinHttpRequest
                req.Open("DELETE", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                HapusSEPInternal = req.ResponseText

            Catch ex As Exception
                HapusSEPInternal = ex.ToString

                Throw ex
            End Try
        End Function
#End Region
#Region "Get Finger Print"
        Public Function GetFingerPrint(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter1 As String, ByVal Parameter2 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetFingerPrint = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "SEP/FingerPrint/Peserta/" & Parameter1 & "/TglPelayanan/" & Parameter2

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send()

                GetFingerPrint = req.ResponseText

            Catch ex As Exception
                GetFingerPrint = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function GetListFingerPrint(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetListFingerPrint = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "SEP/FingerPrint/List/Peserta/TglPelayanan/" & Parameter

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send()

                GetListFingerPrint = req.ResponseText

            Catch ex As Exception
                GetListFingerPrint = ex.ToString

                Throw ex
            End Try
        End Function
#End Region
#End Region
#Region "Rujukan"
#Region "Cari Rujukan"
        Public Function CariRujukan(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter As String, ByVal JenisRujukan As Integer) As String
            Try
                If Not oConnection.GetConnection() Then
                    CariRujukan = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & IIf(JenisRujukan = 0, "Rujukan/", "Rujukan/RS/") & Parameter

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                CariRujukan = req.ResponseText

            Catch ex As Exception
                CariRujukan = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function CariRujukanKartuSatuRecord(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter As String, ByVal JenisRujukan As Integer) As String
            Try
                If Not oConnection.GetConnection() Then
                    CariRujukanKartuSatuRecord = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & IIf(JenisRujukan = 0, "Rujukan/Peserta/", "Rujukan/RS/Peserta/") & Parameter

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                CariRujukanKartuSatuRecord = req.ResponseText

            Catch ex As Exception
                CariRujukanKartuSatuRecord = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function CariRujukanKartuMultiRecord(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter As String, ByVal JenisRujukan As Integer) As String
            Try
                If Not oConnection.GetConnection() Then
                    CariRujukanKartuMultiRecord = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & IIf(JenisRujukan = 0, "Rujukan/List/Peserta/", "Rujukan/RS/List/Peserta/") & Parameter

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                CariRujukanKartuMultiRecord = req.ResponseText

            Catch ex As Exception
                CariRujukanKartuMultiRecord = ex.ToString

                Throw ex
            End Try
        End Function
#End Region
#Region "Pembuatan Rujukan"
        Public Function InsertRujukan(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertRujukan = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "Rujukan/insert"

                req = New WinHttp.WinHttpRequest
                req.Open("POST", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                InsertRujukan = req.ResponseText

            Catch ex As Exception
                InsertRujukan = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function UpdateRujukan(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    UpdateRujukan = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "Rujukan/update"

                req = New WinHttp.WinHttpRequest
                req.Open("PUT", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                UpdateRujukan = req.ResponseText

            Catch ex As Exception
                UpdateRujukan = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function DeleteRujukan(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    DeleteRujukan = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "Rujukan/delete"

                req = New WinHttp.WinHttpRequest
                req.Open("DELETE", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                DeleteRujukan = req.ResponseText

            Catch ex As Exception
                DeleteRujukan = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function InsertRujukanV2(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertRujukanV2 = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "Rujukan/2.0/insert"

                req = New WinHttp.WinHttpRequest
                req.Open("POST", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                InsertRujukanV2 = req.ResponseText

            Catch ex As Exception
                InsertRujukanV2 = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function UpdateRujukanV2(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    UpdateRujukanV2 = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "Rujukan/2.0/Update"

                req = New WinHttp.WinHttpRequest
                req.Open("PUT", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                UpdateRujukanV2 = req.ResponseText

            Catch ex As Exception
                UpdateRujukanV2 = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function InsertRujukanKhusus(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertRujukanKhusus = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "Rujukan/Khusus/insert"

                req = New WinHttp.WinHttpRequest
                req.Open("POST", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                InsertRujukanKhusus = req.ResponseText

            Catch ex As Exception
                InsertRujukanKhusus = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function DeleteRujukanKhusus(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    DeleteRujukanKhusus = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "Rujukan/Khusus/delete"

                req = New WinHttp.WinHttpRequest
                req.Open("DELETE", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                DeleteRujukanKhusus = req.ResponseText

            Catch ex As Exception
                DeleteRujukanKhusus = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimListRujukanKhusus(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Paramater1 As String, ByVal Parameter2 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimListRujukanKhusus = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "Rujukan/Khusus/List/Bulan/" & Paramater1 & "/Tahun/" & Parameter2

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                GetDataVClaimListRujukanKhusus = req.ResponseText

            Catch ex As Exception
                GetDataVClaimListRujukanKhusus = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimListSpesialistikRujukan(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Paramater1 As String, ByVal Parameter2 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimListSpesialistikRujukan = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "Rujukan/ListSpesialistik/PPKRujukan/" & Paramater1 & "/TglRujukan/" & Parameter2

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                GetDataVClaimListSpesialistikRujukan = req.ResponseText

            Catch ex As Exception
                GetDataVClaimListSpesialistikRujukan = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimListSarana(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Paramater As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimListSarana = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "Rujukan/ListSarana/PPKRujukan/" & Paramater

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                GetDataVClaimListSarana = req.ResponseText

            Catch ex As Exception
                GetDataVClaimListSarana = ex.ToString

                Throw ex
            End Try
        End Function
#End Region
#End Region
#Region "Monitoring"
        Public Function GetDataVClaimMonitoringDataKunjungan(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Paramater1 As String, ByVal Parameter2 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimMonitoringDataKunjungan = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "Monitoring/Kunjungan/Tanggal/" & Paramater1 & "/JnsPelayanan/" & Parameter2

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                GetDataVClaimMonitoringDataKunjungan = req.ResponseText

            Catch ex As Exception
                GetDataVClaimMonitoringDataKunjungan = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimMonitoringDataKlaim(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Paramater1 As String, ByVal Parameter2 As String, ByVal Parameter3 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimMonitoringDataKlaim = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "Monitoring/Klaim/Tanggal/" & Paramater1 & "/JnsPelayanan/" & Parameter2 & "/Status/" & Parameter3

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                GetDataVClaimMonitoringDataKlaim = req.ResponseText

            Catch ex As Exception
                GetDataVClaimMonitoringDataKlaim = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimMonitoringDataHistoriPelayananPeserta(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Paramater1 As String, ByVal Parameter2 As String, ByVal Parameter3 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimMonitoringDataHistoriPelayananPeserta = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "monitoring/HistoriPelayanan/NoKartu/" & Paramater1 & "/tglAwal/" & Parameter2 & "/tglAkhir/" & Parameter3

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                GetDataVClaimMonitoringDataHistoriPelayananPeserta = req.ResponseText

            Catch ex As Exception
                GetDataVClaimMonitoringDataHistoriPelayananPeserta = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimMonitoringDataKlaimJaminanJasaRaharja(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter1 As String, ByVal Parameter2 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimMonitoringDataKlaimJaminanJasaRaharja = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "monitoring/JasaRaharja/tglMulai/" & Parameter1 & "/tglAkhir/" & Parameter2

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                GetDataVClaimMonitoringDataKlaimJaminanJasaRaharja = req.ResponseText

            Catch ex As Exception
                GetDataVClaimMonitoringDataKlaimJaminanJasaRaharja = ex.ToString

                Throw ex
            End Try
        End Function
#End Region
#Region "Rencana Kontrol"
        Public Function InsertRencanaKontrol(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertRencanaKontrol = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "RencanaKontrol/insert"

                req = New WinHttp.WinHttpRequest
                req.Open("POST", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                InsertRencanaKontrol = req.ResponseText

            Catch ex As Exception
                InsertRencanaKontrol = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function UpdateRencanaKontrol(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    UpdateRencanaKontrol = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "RencanaKontrol/Update"

                req = New WinHttp.WinHttpRequest
                req.Open("PUT", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                UpdateRencanaKontrol = req.ResponseText

            Catch ex As Exception
                UpdateRencanaKontrol = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function HapusRencanaKontrol(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    HapusRencanaKontrol = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "RencanaKontrol/Delete"

                req = New WinHttp.WinHttpRequest
                req.Open("DELETE", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                HapusRencanaKontrol = req.ResponseText

            Catch ex As Exception
                HapusRencanaKontrol = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function InsertSPRI(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertSPRI = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "RencanaKontrol/InsertSPRI"

                req = New WinHttp.WinHttpRequest
                req.Open("POST", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                InsertSPRI = req.ResponseText

            Catch ex As Exception
                InsertSPRI = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function UpdateSPRI(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    UpdateSPRI = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "RencanaKontrol/UpdateSPRI"

                req = New WinHttp.WinHttpRequest
                req.Open("PUT", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                UpdateSPRI = req.ResponseText

            Catch ex As Exception
                UpdateSPRI = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function CariSEPSuratKontrolSPRI(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    CariSEPSuratKontrolSPRI = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "RencanaKontrol/nosep/" & Parameter

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send()

                CariSEPSuratKontrolSPRI = req.ResponseText

            Catch ex As Exception
                CariSEPSuratKontrolSPRI = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function CariNomorRencanaKontrol(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    CariNomorRencanaKontrol = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "RencanaKontrol/noSuratKontrol/" & Parameter

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send()

                CariNomorRencanaKontrol = req.ResponseText

            Catch ex As Exception
                CariNomorRencanaKontrol = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function DataNomorRencanaKontrol(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter1 As String, ByVal Parameter2 As String, ByVal Parameter3 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    DataNomorRencanaKontrol = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "RencanaKontrol/ListRencanaKontrol/tglAwal/" & Parameter1 & "/tglAkhir/" & Parameter2 & "/filter/" & Parameter3

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send()

                DataNomorRencanaKontrol = req.ResponseText

            Catch ex As Exception
                DataNomorRencanaKontrol = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function DataPoliSpesialistik(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter1 As String, ByVal Parameter2 As String, ByVal Parameter3 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    DataPoliSpesialistik = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "RencanaKontrol/ListSpesialistik/JnsKontrol/" & Parameter1 & "/nomor/" & Parameter2 & "/TglRencanaKontrol/" & Parameter3

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send()

                DataPoliSpesialistik = req.ResponseText

            Catch ex As Exception
                DataPoliSpesialistik = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function DataDokter(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter1 As String, ByVal Parameter2 As String, ByVal Parameter3 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    DataDokter = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "RencanaKontrol/JadwalPraktekDokter/JnsKontrol/" & Parameter1 & "/KdPoli/" & Parameter2 & "/TglRencanaKontrol/" & Parameter3

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send()

                DataDokter = req.ResponseText

            Catch ex As Exception
                DataDokter = ex.ToString

                Throw ex
            End Try
        End Function
#End Region
#End Region
#Region "APLICARE"
        Public Function GetDataAplicareReferensiKelasRawat(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataAplicareReferensiKelasRawat = ""
                    Exit Function
                End If

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "rest/ref/kelas"

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-Cons-ID", CONSID)
                req.SetRequestHeader("X-Timestamp", uTime)
                req.SetRequestHeader("X-Signature", HasilKey)
                req.SetRequestHeader("Content-Type", "application/json")

                req.Send()

                result = req.ResponseText

                GetDataAplicareReferensiKelasRawat = result

            Catch ex As Exception
                GetDataAplicareReferensiKelasRawat = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function UpdateKetersediaanTempatTidurNew(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal PPKPELAYANAN As String, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    UpdateKetersediaanTempatTidurNew = ""
                    Exit Function
                End If

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "rest/bed/update/" & PPKPELAYANAN

                req = New WinHttp.WinHttpRequest
                req.Open("POST", Url, False)
                req.SetRequestHeader("X-Cons-ID", CONSID)
                req.SetRequestHeader("X-Timestamp", uTime)
                req.SetRequestHeader("X-Signature", HasilKey)
                req.SetRequestHeader("Content-Type", "application/json")
                req.Send(Request)
                result = req.ResponseText

                UpdateKetersediaanTempatTidurNew = result

            Catch ex As Exception
                UpdateKetersediaanTempatTidurNew = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function RuanganBaruNew(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal PPKPELAYANAN As String, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    RuanganBaruNew = ""
                    Exit Function
                End If

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "rest/bed/create/" & PPKPELAYANAN

                req = New WinHttp.WinHttpRequest
                req.Open("POST", Url, False)
                req.SetRequestHeader("X-Cons-ID", CONSID)
                req.SetRequestHeader("X-Timestamp", uTime)
                req.SetRequestHeader("X-Signature", HasilKey)
                req.SetRequestHeader("Content-Type", "application/json")
                req.Send(Request)
                result = req.ResponseText

                RuanganBaruNew = result

            Catch ex As Exception
                RuanganBaruNew = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function RuanganHapusNew(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal PPKPELAYANAN As String, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    RuanganHapusNew = ""
                    Exit Function
                End If


                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "rest/bed/delete/" & PPKPELAYANAN

                req = New WinHttp.WinHttpRequest
                req.Open("POST", Url, False)
                req.SetRequestHeader("X-Cons-ID", CONSID)
                req.SetRequestHeader("X-Timestamp", uTime)
                req.SetRequestHeader("X-Signature", HasilKey)
                req.SetRequestHeader("Content-Type", "application/json")
                req.Send(Request)
                result = req.ResponseText

                RuanganHapusNew = result

            Catch ex As Exception
                RuanganHapusNew = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function KeterersediaanKamar(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal PPKPELAYANAN As String, ByVal Start As Integer, ByVal Limit As Integer) As String
            Try
                If Not oConnection.GetConnection() Then
                    KeterersediaanKamar = ""
                    Exit Function
                End If

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "rest/bed/read/" & PPKPELAYANAN & "/" & Start & "/" & Limit & ""

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-Cons-ID", CONSID)
                req.SetRequestHeader("X-Timestamp", uTime)
                req.SetRequestHeader("X-Signature", HasilKey)
                req.SetRequestHeader("Content-Type", "application/json")

                req.Send()

                result = req.ResponseText

                KeterersediaanKamar = result

            Catch ex As Exception
                KeterersediaanKamar = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function Agents(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal PPKPELAYANAN As String, ByVal Request As String, ByVal Start As Integer, ByVal Limit As Integer) As String
            Try
                If Not oConnection.GetConnection() Then
                    Agents = ""
                    Exit Function
                End If

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "rest/bed/read/" & PPKPELAYANAN & "/" & Start & "/" & Limit

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-Cons-ID", CONSID)
                req.SetRequestHeader("X-Timestamp", uTime)
                req.SetRequestHeader("X-Signature", HasilKey)
                req.SetRequestHeader("Content-Type", "application/json")
                req.Send(Request)
                result = req.ResponseText

                Agents = result

            Catch ex As Exception
                Agents = ex.ToString

                Throw ex
            End Try
        End Function
#End Region
#Region "CALCULATE PEMETAAN"
        Public Function UpdatePemetaan(ByVal isCalculate As Boolean, ByVal sKDPENDAFTARAN As String, ByVal sKDUPDATE_APLICARE As String, ByVal sKDJENISKELAMIN As Integer) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdatePemetaan = False
                    Exit Function
                End If

                UpdatePemetaan = True

                If sKDUPDATE_APLICARE <> "" Then
                    Dim dsPulang = oConnection.db.T_UPDATE_TANGGAL_PULANGs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN)

                    If dsPulang Is Nothing Then
                        Dim dsPemetaan = oConnection.db.M_KELASAPLICARE_DEPARTMENTs.FirstOrDefault(Function(x) x.KDUPDATE_APLICARE = sKDUPDATE_APLICARE)

                        If dsPemetaan IsNot Nothing Then
                            If dsPemetaan.TERSEDIA_LAKIPEREMPUAN > 0 Then
                                dsPemetaan.TERSEDIA_LAKIPEREMPUAN = IIf(isCalculate = False, dsPemetaan.TERSEDIA_LAKIPEREMPUAN - 1, dsPemetaan.TERSEDIA_LAKIPEREMPUAN + 1)
                            Else
                                If sKDJENISKELAMIN = 0 Then
                                    dsPemetaan.TERSEDIA_PEREMPUAN = IIf(isCalculate = False, dsPemetaan.TERSEDIA_PEREMPUAN - 1, dsPemetaan.TERSEDIA_PEREMPUAN + 1)
                                Else
                                    dsPemetaan.TERSEDIA_LAKI = IIf(isCalculate = False, dsPemetaan.TERSEDIA_LAKI - 1, dsPemetaan.TERSEDIA_LAKI + 1)
                                End If
                            End If

                            dsPemetaan.TERSEDIA = dsPemetaan.TERSEDIA_LAKI + dsPemetaan.TERSEDIA_PEREMPUAN + dsPemetaan.TERSEDIA_LAKIPEREMPUAN

                            If dsPemetaan.TERSEDIA > 0 Then
                                If dsPemetaan.KAPASITAS >= dsPemetaan.TERSEDIA Then
                                    oConnection.db.SubmitChanges()
                                End If
                            End If

                            Try
                                Dim oUser As New Setting.clsUser
                                Dim oKelasAplicare As New Reference.clsKelasAplicare

                                Dim dsAPlicare = oUser.GetDataKoneksiBPJS("APLICARE")
                                If dsAPlicare IsNot Nothing Then
                                    Dim ds = oKelasAplicare.GetDataDetail_KelasDepartment(dsPemetaan.KDKELASAPLICARE, dsPemetaan.KDDEPARTMENT)
                                    If ds IsNot Nothing Then
                                        Dim jsonRequest As String = String.Empty

                                        jsonRequest = "{ "
                                        jsonRequest &= """kodekelas"": """ & dsPemetaan.KDKELASAPLICARE & ""","
                                        jsonRequest &= """koderuang"": """ & ds.KDUPDATE_APLICARE & ""","
                                        jsonRequest &= """namaruang"": """ & dsPemetaan.M_DEPARTMENT.NAME_DISPLAY & ""","
                                        jsonRequest &= """kapasitas"": """ & dsPemetaan.KAPASITAS & ""","
                                        jsonRequest &= """tersedia"": """ & dsPemetaan.TERSEDIA & ""","
                                        jsonRequest &= """tersediapria"": """ & dsPemetaan.TERSEDIA_LAKI & ""","
                                        jsonRequest &= """tersediawanita"": """ & dsPemetaan.TERSEDIA_PEREMPUAN & ""","
                                        jsonRequest &= """tersediapriawanita"": """ & dsPemetaan.TERSEDIA_LAKIPEREMPUAN & """"
                                        jsonRequest &= "} "

                                        UpdateKetersediaanTempatTidurNew(dsAPlicare.ALAMATWEB, dsAPlicare.CONSID, dsAPlicare.SECREATKEY, dsAPlicare.REMARKS, dsAPlicare.PPKPELAYANAN, jsonRequest)
                                    End If
                                Else
                                    'UpdatePemetaan = False
                                End If
                            Catch ex As Exception

                            End Try

                        Else
                            UpdatePemetaan = False
                        End If

                        Dim dsPendaftaran = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN)

                        If dsPendaftaran IsNot Nothing Then
                            If isCalculate = False Then
                                UpdateKDUPDATE_APLICARE(dsPendaftaran.KDPENDAFTARAN, dsPemetaan.KDUPDATE_APLICARE, dsPemetaan.KDDEPARTMENT)
                            End If
                        End If
                    Else
                        UpdatePemetaan = False
                    End If
                Else
                    UpdatePemetaan = False
                End If

            Catch ex As Exception
                UpdatePemetaan = False
                Throw ex
            End Try
        End Function
        Public Function UpdateKDUPDATE_APLICARE(ByVal sKDPENDAFTARAN As String, ByVal sKDUPDATE_APLICARE As String, ByVal sKDDEPARTMENT As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateKDUPDATE_APLICARE = False
                    Exit Function
                End If

                UpdateKDUPDATE_APLICARE = True

                Dim ds = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN)

                ds.KDUPDATE_APLICARE = sKDUPDATE_APLICARE
                ds.KDDEPARTMENT = sKDDEPARTMENT

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdateKDUPDATE_APLICARE = False
                Throw ex
            End Try
        End Function
        Public Function AntreanPerTanggal(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal uTime As Integer, ByVal USERKEY As String, ByVal Parameter1 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    AntreanPerTanggal = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "antrean/pendaftaran/tanggal/" & Parameter1

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("x-cons-id", CONSID)
                req.SetRequestHeader("x-timestamp", uTime)
                req.SetRequestHeader("x-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)

                req.Send()

                AntreanPerTanggal = req.ResponseText

            Catch ex As Exception
                AntreanPerTanggal = ex.ToString

                Throw ex
            End Try
        End Function
#End Region
#Region "E-Claim"
        Public Function fn_BriggingEKlaim(ByVal ALAMATWEB As String, ByVal REMARKS As String, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    fn_BriggingEKlaim = ""
                    Exit Function
                End If

                Dim result As String = String.Empty
                Dim req As WinHttp.WinHttpRequest
                Dim jsonEncode As String = String.Empty
                Dim JsonEncrypt As String = String.Empty
                Dim JsonDecrypt As String = String.Empty

                jsonEncode = Request

                JsonEncrypt = inacbg_encrypt(jsonEncode, REMARKS)

                req = New WinHttp.WinHttpRequest
                req.Open("POST", ALAMATWEB, False)
                req.Send(JsonEncrypt)

                If req.Status = "200" Then
                    result = req.ResponseText

                    Dim HasilResult As String

                    HasilResult = result.Replace("----BEGIN ENCRYPTED DATA----", "")

                    HasilResult = HasilResult.Replace("----END ENCRYPTED DATA----", "")

                    fn_BriggingEKlaim = inacbg_decrypt(HasilResult, REMARKS)
                Else
                    fn_BriggingEKlaim = ""
                End If
            Catch ex As Exception
                fn_BriggingEKlaim = "ERORSIMRS" & ex.ToString
                ''oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function

        ' ENCRYPT
        Public Function inacbg_encrypt(text As String, key As String) As String
            Dim keys = Encoding.[Default].GetBytes(hex2bin(key))
            Dim aes As New AesCryptoServiceProvider()
            aes.BlockSize = 128
            aes.KeySize = 256
            aes.GenerateIV()
            Dim iv = aes.IV
            aes.Key = keys
            aes.Mode = CipherMode.CBC
            aes.Padding = PaddingMode.PKCS7
            Dim src As Byte() = Encoding.[Default].GetBytes(text)

            Using enc As ICryptoTransform = aes.CreateEncryptor()
                Dim data As Byte() = enc.TransformFinalBlock(src, 0, src.Length)
                Dim hashObject As New HMACSHA256(keys)
                Dim hash_sign = hashObject.ComputeHash(data)
                Dim signature As Byte() = New Byte(9) {}
                Array.Copy(hash_sign, 0, signature, 0, 10)
                Dim ret As Byte() = New Byte(signature.Length + iv.Length + (data.Length - 1)) {}
                Array.Copy(signature, 0, ret, 0, signature.Length)
                Array.Copy(iv, 0, ret, signature.Length, iv.Length)
                Array.Copy(data, 0, ret, signature.Length + iv.Length, data.Length)
                Return Convert.ToBase64String(ret)
            End Using

        End Function
        ' DECRYPT   
        Public Function inacbg_decrypt(strencrypt As String, key As String) As String
            Dim encoded_str As String = strencrypt
            Dim chiper As Byte() = Convert.FromBase64String(encoded_str)
            Dim length = chiper.Length
            Dim new_byte_iv As Byte() = New Byte(15) {}
            Dim new_byte_msg As Byte() = New Byte(length - 27) {}
            Array.Copy(chiper, 10, new_byte_iv, 0, 16)
            Array.Copy(chiper, 26, new_byte_msg, 0, length - 26)
            Dim byte_key As Byte() = Encoding.[Default].GetBytes(hex2bin(key))
            Dim aes As New RijndaelManaged()
            aes.KeySize = 256
            aes.BlockSize = 128
            aes.Padding = PaddingMode.PKCS7
            aes.Mode = CipherMode.CBC
            aes.Key = byte_key
            aes.IV = new_byte_iv
            Dim AESDecrypt As ICryptoTransform = aes.CreateDecryptor(aes.Key, aes.IV)
            Return Encoding.[Default].GetString(AESDecrypt.TransformFinalBlock(new_byte_msg, 0, new_byte_msg.Length))
        End Function
        Private Shared Function hex2bin(input As String) As String
            input = input.Replace("-", "")
            Dim raw As Byte() = New Byte(input.Length / 2 - 1) {}
            For i As Integer = 0 To raw.Length - 1
                raw(i) = Convert.ToByte(input.Substring(i * 2, 2), 16)
            Next
            Return Encoding.[Default].GetString(raw)
        End Function
        Public Function fn_Membuatklaimbaru(ByVal ALAMATWEB As String, ByVal REMARKS As String, ByVal txtJENISKELAMIN As String, ByVal txtNoPeserta As String, ByVal txtNoSEP As String, ByVal txtNORM As String, ByVal txtNAMAPASIEN As String, ByVal txtTGLLAHIR As String) As String
            Try
                Dim req As WinHttp.WinHttpRequest

                Dim jsonEncode As String
                Dim JsonEncrypt As String
                Dim JsonDecrypt As String
                Dim Result As String = String.Empty
                Dim gender As String = String.Empty
                Dim nomor_kartu As String = String.Empty

                If txtJENISKELAMIN = "L" Then
                    gender = "1"
                Else
                    gender = "2"
                End If

                jsonEncode = "{" & """metadata"": {" & """method"": " & """new_claim""    }," & """data"": {" & """nomor_kartu"": """ & txtNoPeserta & """, " & """nomor_sep"": """ & txtNoSEP & """, " & """nomor_rm"": """ & txtNORM & """, " & """nama_pasien"": """ & txtNAMAPASIEN & """, " & """tgl_lahir"": """ & txtTGLLAHIR & """, " & """gender"": """ & gender & """   } } "

                JsonEncrypt = inacbg_encrypt(jsonEncode, REMARKS)

                req = New WinHttp.WinHttpRequest
                req.Open("POST", ALAMATWEB, False)
                req.Send(JsonEncrypt)

                If req.Status = "200" Then
                    Result = req.ResponseText

                    Dim HasilResult As String

                    HasilResult = Result.Replace("----BEGIN ENCRYPTED DATA----", "")

                    HasilResult = HasilResult.Replace("----END ENCRYPTED DATA----", "")

                    JsonDecrypt = inacbg_decrypt(HasilResult, REMARKS)

                    fn_Membuatklaimbaru = JsonDecrypt
                Else
                    fn_Membuatklaimbaru = "Error Membuat Klaim Baru, Status Tidak 200" & vbCrLf & req.Status & "-" & req.StatusText
                End If
            Catch oErr As Exception
                fn_Membuatklaimbaru = "Membuat Klaim Baru Gagal: " & vbCrLf & oErr.Message
            End Try
        End Function
        Public Function fn_UpdateDataPasien(ByVal ALAMATWEB As String, ByVal REMARKS As String, ByVal sNAME_DISPLAY As String, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    fn_UpdateDataPasien = ""
                    Exit Function
                End If

                Dim result As String = String.Empty
                Dim req As WinHttp.WinHttpRequest
                Dim jsonEncode As String = String.Empty
                Dim JsonEncrypt As String = String.Empty
                Dim JsonDecrypt As String = String.Empty

                jsonEncode = Request

                JsonEncrypt = inacbg_encrypt(jsonEncode, REMARKS)

                req = New WinHttp.WinHttpRequest
                req.Open("POST", ALAMATWEB, False)
                req.Send(JsonEncrypt)

                If req.Status = "200" Then
                    result = req.ResponseText

                    Dim HasilResult As String

                    HasilResult = result.Replace("----BEGIN ENCRYPTED DATA----", "")

                    HasilResult = HasilResult.Replace("----END ENCRYPTED DATA----", "")

                    fn_UpdateDataPasien = inacbg_decrypt(HasilResult, REMARKS)
                Else
                    fn_UpdateDataPasien = ""
                End If

            Catch ex As Exception
                fn_UpdateDataPasien = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function fn_MengisiUpdateDataKlaim(ByVal ALAMATWEB As String, ByVal REMARKS As String, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    fn_MengisiUpdateDataKlaim = ""
                    Exit Function
                End If

                Dim result As String = String.Empty
                Dim req As WinHttp.WinHttpRequest
                Dim jsonEncode As String = String.Empty
                Dim JsonEncrypt As String = String.Empty
                Dim JsonDecrypt As String = String.Empty

                jsonEncode = Request

                JsonEncrypt = inacbg_encrypt(jsonEncode, REMARKS)

                req = New WinHttp.WinHttpRequest
                req.Open("POST", ALAMATWEB, False)
                req.Send(JsonEncrypt)

                If req.Status = "200" Then
                    result = req.ResponseText

                    Dim HasilResult As String

                    HasilResult = result.Replace("----BEGIN ENCRYPTED DATA----", "")

                    HasilResult = HasilResult.Replace("----END ENCRYPTED DATA----", "")

                    fn_MengisiUpdateDataKlaim = inacbg_decrypt(HasilResult, REMARKS)
                Else
                    fn_MengisiUpdateDataKlaim = ""
                End If
            Catch ex As Exception
                fn_MengisiUpdateDataKlaim = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function fn_GroupingStage1(ByVal ALAMATWEB As String, ByVal REMARKS As String, ByVal Nosep As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    fn_GroupingStage1 = ""
                    Exit Function
                End If

                Dim result As String = String.Empty
                Dim req As WinHttp.WinHttpRequest
                Dim jsonEncode As String = String.Empty
                Dim JsonEncrypt As String = String.Empty
                Dim JsonDecrypt As String = String.Empty

                jsonEncode = "{" & """metadata"": {      " & """method"":" & """grouper"",      " & """stage"":""" & 1 & """   },   " & """data"": {      " & """nomor_sep"":""" & Nosep & """   } } "

                JsonEncrypt = inacbg_encrypt(jsonEncode, REMARKS)

                req = New WinHttp.WinHttpRequest
                req.Open("POST", ALAMATWEB, False)
                req.Send(JsonEncrypt)

                If req.Status = "200" Then
                    result = req.ResponseText

                    Dim HasilResult As String

                    HasilResult = result.Replace("----BEGIN ENCRYPTED DATA----", "")

                    HasilResult = HasilResult.Replace("----END ENCRYPTED DATA----", "")

                    fn_GroupingStage1 = inacbg_decrypt(HasilResult, REMARKS)
                Else
                    fn_GroupingStage1 = ""
                End If
            Catch ex As Exception
                fn_GroupingStage1 = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function fn_UntukFinalisasiKlaim(ByVal ALAMATWEB As String, ByVal REMARKS As String, ByVal Nosep As String, ByVal sCoder As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    fn_UntukFinalisasiKlaim = ""
                    Exit Function
                End If

                Dim result As String = String.Empty
                Dim req As WinHttp.WinHttpRequest
                Dim jsonEncode As String = String.Empty
                Dim JsonEncrypt As String = String.Empty
                Dim JsonDecrypt As String = String.Empty

                jsonEncode = "{   " & """metadata"": { " & """method"":" & """claim_final""   },   " & """data"": {      " & """nomor_sep"":""" & Nosep & """,      " & """coder_nik"": """ & sCoder & """   } } "

                JsonEncrypt = inacbg_encrypt(jsonEncode, REMARKS)

                req = New WinHttp.WinHttpRequest
                req.Open("POST", ALAMATWEB, False)
                req.Send(JsonEncrypt)

                If req.Status = "200" Then
                    result = req.ResponseText

                    Dim HasilResult As String

                    HasilResult = result.Replace("----BEGIN ENCRYPTED DATA----", "")

                    HasilResult = HasilResult.Replace("----END ENCRYPTED DATA----", "")

                    fn_UntukFinalisasiKlaim = inacbg_decrypt(HasilResult, REMARKS)
                Else
                    fn_UntukFinalisasiKlaim = ""
                End If
            Catch ex As Exception
                fn_UntukFinalisasiKlaim = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function fn_UntukMenghapusKlaim(ByVal ALAMATWEB As String, ByVal REMARKS As String, ByVal Nosep As String, ByVal sCoder As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    fn_UntukMenghapusKlaim = ""
                    Exit Function
                End If

                Dim req As WinHttp.WinHttpRequest
                Dim jsonEncode As String
                Dim JsonEncrypt As String
                Dim JsonDecrypt As String
                Dim Result As String = String.Empty

                jsonEncode = "{   " & """metadata"": {      " & """method"":" & """delete_claim""   },   " & """data"": {      " & """nomor_sep"":""" & Nosep & """,      " & """coder_nik"":""" & sCoder & """   } } "

                JsonEncrypt = inacbg_encrypt(jsonEncode, REMARKS)

                req = New WinHttp.WinHttpRequest
                req.Open("POST", ALAMATWEB, False)
                req.Send(JsonEncrypt)

                If req.Status = "200" Then
                    Result = req.ResponseText

                    Dim HasilResult As String

                    HasilResult = Result.Replace("----BEGIN ENCRYPTED DATA----", "")

                    HasilResult = HasilResult.Replace("----END ENCRYPTED DATA----", "")

                    JsonDecrypt = inacbg_decrypt(HasilResult, REMARKS)

                    fn_UntukMenghapusKlaim = JsonDecrypt
                Else
                    fn_UntukMenghapusKlaim = "Untuk Menghapus Klaim Status Tidak 200" & vbCrLf & req.Status & "-" & req.StatusText
                End If
            Catch ex As Exception
                fn_UntukMenghapusKlaim = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function fn_MengirimKlaimIndividualKeDataCenter(ByVal ALAMATWEB As String, ByVal REMARKS As String, ByVal Nosep As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    fn_MengirimKlaimIndividualKeDataCenter = ""
                    Exit Function
                End If

                Dim req As WinHttp.WinHttpRequest
                Dim jsonEncode As String
                Dim JsonEncrypt As String
                Dim JsonDecrypt As String
                Dim Result As String = String.Empty

                jsonEncode = "{   " & """metadata"": {      " & """method"":" & """send_claim_individual""   },   " & """data"": {      " & """nomor_sep"":""" & Nosep & """   } } "

                JsonEncrypt = inacbg_encrypt(jsonEncode, REMARKS)

                req = New WinHttp.WinHttpRequest
                req.Open("POST", ALAMATWEB, False)
                req.Send(JsonEncrypt)

                If req.Status = "200" Then
                    Result = req.ResponseText

                    Dim HasilResult As String

                    HasilResult = Result.Replace("----BEGIN ENCRYPTED DATA----", "")

                    HasilResult = HasilResult.Replace("----END ENCRYPTED DATA----", "")

                    JsonDecrypt = inacbg_decrypt(HasilResult, REMARKS)

                    fn_MengirimKlaimIndividualKeDataCenter = JsonDecrypt
                Else
                    fn_MengirimKlaimIndividualKeDataCenter = "Untuk Menghapus Klaim Status Tidak 200" & vbCrLf & req.Status & "-" & req.StatusText
                End If
            Catch ex As Exception
                fn_MengirimKlaimIndividualKeDataCenter = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function fn_UntukMengeditUlangKlaim(ByVal ALAMATWEB As String, ByVal REMARKS As String, ByVal Nosep As String) As String
            Try
                Dim req As WinHttp.WinHttpRequest
                Dim jsonEncode As String
                Dim JsonEncrypt As String
                Dim JsonDecrypt As String
                Dim Result As String = String.Empty

                jsonEncode = "{   " & """metadata"": {      " & """method"":" & """reedit_claim""   },   " & """data"": {      " & """nomor_sep"":""" & Nosep & """  } } "

                JsonEncrypt = inacbg_encrypt(jsonEncode, REMARKS)

                req = New WinHttp.WinHttpRequest
                req.Open("POST", ALAMATWEB, False)
                req.Send(JsonEncrypt)

                If req.Status = "200" Then
                    Result = req.ResponseText

                    Dim HasilResult As String

                    HasilResult = Result.Replace("----BEGIN ENCRYPTED DATA----", "")

                    HasilResult = HasilResult.Replace("----END ENCRYPTED DATA----", "")

                    JsonDecrypt = inacbg_decrypt(HasilResult, REMARKS)

                    fn_UntukMengeditUlangKlaim = JsonDecrypt

                Else
                    fn_UntukMengeditUlangKlaim = "Edit Ulang Klaim Status Tidak 200" & vbCrLf & req.Status & "-" & req.StatusText
                End If
            Catch oErr As Exception
                fn_UntukMengeditUlangKlaim = "Edit Ulang Klaim Gagal : " & vbCrLf & oErr.Message
            End Try
        End Function
        Public Function fn_Cetakklaim(ByVal ALAMATWEB As String, ByVal REMARKS As String, ByVal Nosep As String) As String
            Try
                Dim req As WinHttp.WinHttpRequest
                Dim jsonEncode As String
                Dim JsonEncrypt As String
                Dim JsonDecrypt As String
                Dim Result As String = String.Empty

                jsonEncode = "{   " & """metadata"": {      " & """method"": " & """claim_print""   },   " & """data"": {     " & """nomor_sep"": """ & Nosep & """   } } "

                JsonEncrypt = inacbg_encrypt(jsonEncode, REMARKS)

                req = New WinHttp.WinHttpRequest
                req.Open("GET", ALAMATWEB, False)
                req.Send(JsonEncrypt)

                If req.Status = "200" Then
                    Result = req.ResponseText

                    Dim HasilResult As String

                    HasilResult = Result.Replace("----BEGIN ENCRYPTED DATA----", "")

                    HasilResult = HasilResult.Replace("----END ENCRYPTED DATA----", "")

                    JsonDecrypt = inacbg_decrypt(HasilResult, REMARKS)

                    fn_Cetakklaim = JsonDecrypt

                Else
                    fn_Cetakklaim = "Cetak Klaim, Status Tidak 200" & vbCrLf & req.Status & "-" & req.StatusText
                End If
            Catch oErr As Exception
                fn_Cetakklaim = "Cetak Klaim Gagal : " & vbCrLf & oErr.Message
            End Try
        End Function
        Public Function fn_Pencariandiagnosa(ByVal ALAMATWEB As String, ByVal REMARKS As String, ByVal keyword As String) As String
            Try
                Dim result As String = String.Empty
                Dim req As WinHttp.WinHttpRequest
                Dim jsonEncode As String = String.Empty
                Dim JsonEncrypt As String = String.Empty
                Dim JsonDecrypt As String = String.Empty

                Dim Request As String = "{" & """metadata"": {" & """method"": " & """search_diagnosis""}," & """data"": {" & """keyword"": """ & keyword & """ } } "

                jsonEncode = Request

                JsonEncrypt = inacbg_encrypt(jsonEncode, REMARKS)

                req = New WinHttp.WinHttpRequest
                req.Open("POST", ALAMATWEB, False)
                req.Send(JsonEncrypt)

                If req.Status = "200" Then
                    result = req.ResponseText

                    Dim HasilResult As String

                    HasilResult = result.Replace("----BEGIN ENCRYPTED DATA----", "")

                    HasilResult = HasilResult.Replace("----END ENCRYPTED DATA----", "")

                    fn_Pencariandiagnosa = inacbg_decrypt(HasilResult, REMARKS)
                Else
                    fn_Pencariandiagnosa = ""
                End If
            Catch ex As Exception
                fn_Pencariandiagnosa = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function fn_PencarianProsedur(ByVal ALAMATWEB As String, ByVal REMARKS As String, ByVal keyword As String) As String
            Try
                Dim result As String = String.Empty

                Dim req As WinHttp.WinHttpRequest
                Dim jsonEncode As String = String.Empty
                Dim JsonEncrypt As String = String.Empty
                Dim JsonDecrypt As String = String.Empty

                Dim Request As String = "{" & """metadata"": {" & """method"": " & """search_procedures""}," & """data"": {" & """keyword"": """ & keyword & """ } } "

                jsonEncode = Request

                JsonEncrypt = inacbg_encrypt(jsonEncode, REMARKS)

                req = New WinHttp.WinHttpRequest
                req.Open("POST", ALAMATWEB, False)
                req.Send(JsonEncrypt)

                If req.Status = "200" Then
                    result = req.ResponseText

                    Dim HasilResult As String

                    HasilResult = result.Replace("----BEGIN ENCRYPTED DATA----", "")

                    HasilResult = HasilResult.Replace("----END ENCRYPTED DATA----", "")

                    fn_PencarianProsedur = inacbg_decrypt(HasilResult, REMARKS)
                Else
                    fn_PencarianProsedur = ""
                End If
            Catch ex As Exception
                fn_PencarianProsedur = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function fn_GroupingStage2(ByVal ALAMATWEB As String, ByVal REMARKS As String, ByVal special_cmg As String, ByVal Nosep As String) As String
            Try
                Dim req As WinHttp.WinHttpRequest
                Dim jsonEncode As String
                Dim JsonEncrypt As String
                Dim JsonDecrypt As String
                Dim Result As String = String.Empty

                jsonEncode = "{" & """metadata"": {      " & """method"":" & """grouper"",      " & """stage"":""" & 2 & """   },   " & """data"": {      " & """nomor_sep"":""" & Nosep & """, " & """special_cmg"": """ & special_cmg & """    } } "

                JsonEncrypt = inacbg_encrypt(jsonEncode, REMARKS)

                req = New WinHttp.WinHttpRequest
                req.Open("POST", ALAMATWEB, False)
                req.Send(JsonEncrypt)

                If req.Status = "200" Then
                    Result = req.ResponseText

                    Dim HasilResult As String

                    HasilResult = Result.Replace("----BEGIN ENCRYPTED DATA----", "")

                    HasilResult = HasilResult.Replace("----END ENCRYPTED DATA----", "")

                    JsonDecrypt = inacbg_decrypt(HasilResult, REMARKS)

                    fn_GroupingStage2 = JsonDecrypt

                Else
                    fn_GroupingStage2 = "Grouping Statge1, Status Tidak 200" & vbCrLf & req.Status & "-" & req.StatusText
                End If
            Catch oErr As Exception
                fn_GroupingStage2 = "Grouping Stage 1 Gagal : " & vbCrLf & oErr.Message
            End Try
        End Function
#End Region
#Region "Web Service Antrean - BPJS"
        Public Function ReferensiPoli(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal uTime As Integer, ByVal USERKEY As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    ReferensiPoli = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "ref/poli"

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-Cons-ID", CONSID)
                req.SetRequestHeader("X-Timestamp", uTime)
                req.SetRequestHeader("X-Signature", HasilKey)
                req.SetRequestHeader("user-key", USERKEY)

                req.Send()

                ReferensiPoli = req.ResponseText

            Catch ex As Exception
                ReferensiPoli = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function ReferensiDokter(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal uTime As Integer, ByVal USERKEY As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    ReferensiDokter = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "ref/dokter"

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("x-cons-id", CONSID)
                req.SetRequestHeader("x-timestamp", uTime)
                req.SetRequestHeader("x-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)

                req.Send()

                ReferensiDokter = req.ResponseText

            Catch ex As Exception
                ReferensiDokter = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function ReferensiJadwalDokter(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal uTime As Integer, ByVal USERKEY As String, ByVal Parameter1 As String, ByVal Parameter2 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    ReferensiJadwalDokter = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "jadwaldokter/kodepoli/" & Parameter1 & "/tanggal/" & Parameter2

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("x-cons-id", CONSID)
                req.SetRequestHeader("x-timestamp", uTime)
                req.SetRequestHeader("x-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)

                req.Send()

                ReferensiJadwalDokter = req.ResponseText

            Catch ex As Exception
                ReferensiJadwalDokter = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function UpdateJadwalDokter(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    UpdateJadwalDokter = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "jadwaldokter/updatejadwaldokter"

                req = New WinHttp.WinHttpRequest
                req.Open("POST", Url, False)
                req.SetRequestHeader("x-cons-id", CONSID)
                req.SetRequestHeader("x-timestamp", uTime)
                req.SetRequestHeader("x-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)

                req.Send(Request)

                UpdateJadwalDokter = req.ResponseText

            Catch ex As Exception
                UpdateJadwalDokter = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function TambahAntrean(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    TambahAntrean = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "antrean/add"

                req = New WinHttp.WinHttpRequest
                req.Open("POST", Url, False)
                req.SetRequestHeader("x-cons-id", CONSID)
                req.SetRequestHeader("x-timestamp", uTime)
                req.SetRequestHeader("x-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)

                req.Send(Request)

                TambahAntrean = req.ResponseText

            Catch ex As Exception
                TambahAntrean = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function UpdateWaktuAntrean(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    UpdateWaktuAntrean = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "antrean/updatewaktu"

                req = New WinHttp.WinHttpRequest
                req.Open("POST", Url, False)
                req.SetRequestHeader("x-cons-id", CONSID)
                req.SetRequestHeader("x-timestamp", uTime)
                req.SetRequestHeader("x-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)

                req.Send(Request)

                UpdateWaktuAntrean = req.ResponseText

            Catch ex As Exception
                UpdateWaktuAntrean = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function BatalAntrean(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    BatalAntrean = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "antrean/batal"

                req = New WinHttp.WinHttpRequest
                req.Open("POST", Url, False)
                req.SetRequestHeader("x-cons-id", CONSID)
                req.SetRequestHeader("x-timestamp", uTime)
                req.SetRequestHeader("x-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)

                req.Send(Request)

                BatalAntrean = req.ResponseText

            Catch ex As Exception
                BatalAntrean = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function ListWaktuTaskId(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    ListWaktuTaskId = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "antrean/getlisttask"

                req = New WinHttp.WinHttpRequest
                req.Open("POST", Url, False)
                req.SetRequestHeader("x-cons-id", CONSID)
                req.SetRequestHeader("x-timestamp", uTime)
                req.SetRequestHeader("x-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)

                req.Send(Request)

                ListWaktuTaskId = req.ResponseText

            Catch ex As Exception
                ListWaktuTaskId = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function DashboardPerTanggal(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal uTime As Integer, ByVal USERKEY As String, ByVal Parameter1 As String, ByVal Parameter2 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    DashboardPerTanggal = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "dashboard/waktutunggu/tanggal/" & Parameter1 & "/waktu/" & Parameter2

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("x-cons-id", CONSID)
                req.SetRequestHeader("x-timestamp", uTime)
                req.SetRequestHeader("x-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)

                req.Send()

                DashboardPerTanggal = req.ResponseText

            Catch ex As Exception
                DashboardPerTanggal = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function DashboardPerBulan(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal uTime As Integer, ByVal USERKEY As String, ByVal Parameter1 As String, ByVal Parameter2 As String, ByVal Parameter3 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    DashboardPerBulan = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "dashboard/waktutunggu/bulan/" & Parameter1 & "/tahun/" & Parameter2 & "/waktu/" & Parameter3

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("x-cons-id", CONSID)
                req.SetRequestHeader("x-timestamp", uTime)
                req.SetRequestHeader("x-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)

                req.Send()

                DashboardPerBulan = req.ResponseText

            Catch ex As Exception
                DashboardPerBulan = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function fn_ValidasiNomorRegisterSITB(ByVal ALAMATWEB As String, ByVal REMARKS As String, ByVal nomor_register_sitb As String, ByVal txtNoSEP As String) As String
            Try
                Dim req As WinHttp.WinHttpRequest

                Dim jsonEncode As String
                Dim JsonEncrypt As String
                Dim JsonDecrypt As String
                Dim Result As String = String.Empty


                jsonEncode = "{" & """metadata"": {" & """method"": " & """sitb_validate""    }," & """data"": {" & """nomor_sep"": """ & txtNoSEP & """, " & """nomor_register_sitb"": """ & nomor_register_sitb & """   } } "

                JsonEncrypt = inacbg_encrypt(jsonEncode, REMARKS)

                req = New WinHttp.WinHttpRequest
                req.Open("POST", ALAMATWEB, False)
                req.Send(JsonEncrypt)

                If req.Status = "200" Then
                    Result = req.ResponseText

                    Dim HasilResult As String

                    HasilResult = Result.Replace("----BEGIN ENCRYPTED DATA----", "")

                    HasilResult = HasilResult.Replace("----END ENCRYPTED DATA----", "")

                    JsonDecrypt = inacbg_decrypt(HasilResult, REMARKS)

                    fn_ValidasiNomorRegisterSITB = JsonDecrypt
                Else
                    fn_ValidasiNomorRegisterSITB = "Error Validasi NomorRegister SITB, Status Tidak 200" & vbCrLf & req.Status & "-" & req.StatusText
                End If
            Catch oErr As Exception
                fn_ValidasiNomorRegisterSITB = "Validasi NomorRegister SITB Gagal: " & vbCrLf & oErr.Message
            End Try
        End Function
#End Region
#Region "Riwayat BPJS"
        Public Function GetDataIcareDevelop(ByVal NoKartuBPJS As String, ByVal kodedokter As Integer, ByVal uTime As Integer, ByVal cosid As String, ByVal secretKey As String, ByVal userkey As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataIcareDevelop = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""
                Dim data = cosid & "&" & uTime

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(secretKey))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(data))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = "https://apijkn-dev.bpjs-kesehatan.go.id/ihs_dev/api/rs/validate"

                req = New WinHttp.WinHttpRequest
                req.Open("POST", Url, False)
                req.SetRequestHeader("X-cons-id", cosid)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", userkey)
                req.SetRequestHeader("Content-Type", "application/json")

                Dim jsonRequest As String = String.Empty

                jsonRequest = " { "
                jsonRequest &= """param"": """ & NoKartuBPJS & ""","
                jsonRequest &= """kodedokter"": " & kodedokter & " "
                jsonRequest &= "}  "

                req.Send(jsonRequest)

                GetDataIcareDevelop = req.ResponseText

            Catch ex As Exception
                GetDataIcareDevelop = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function GetDataIcare(ByVal NoKartuBPJS As String, ByVal kodedokter As Integer, ByVal uTime As Integer, ByVal cosid As String, ByVal secretKey As String, ByVal userkey As String, ByVal url As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataIcare = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""
                Dim data = cosid & "&" & uTime

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(secretKey))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(data))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                'Dim Url As String = "https://apijkn.bpjs-kesehatan.go.id/wsihs/api/rs/validate"

                req = New WinHttp.WinHttpRequest
                req.Open("POST", Url, False)
                req.SetRequestHeader("X-cons-id", cosid)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", userkey)
                req.SetRequestHeader("Content-Type", "application/json")

                Dim jsonRequest As String = String.Empty

                jsonRequest = " { "
                jsonRequest &= """param"": """ & NoKartuBPJS & ""","
                jsonRequest &= """kodedokter"": " & kodedokter & " "
                jsonRequest &= "}  "

                req.Send(jsonRequest)

                GetDataIcare = req.ResponseText

            Catch ex As Exception
                GetDataIcare = ex.ToString

                Throw ex
            End Try
        End Function
#End Region
#Region "Apotek Online"
#Region "Katalog Referensi"
        Public Function Referensi_DPHO(ByVal uTime As Integer, ByVal UrlApotekOnline As String, ByVal ConsumerSecret As String, ByVal ConsumerID As String, ByVal UserKey As String) As String
            Try
                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ConsumerSecret))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ConsumerID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                req = New WinHttp.WinHttpRequest
                req.Open("GET", UrlApotekOnline & "referensi/dpho", False)
                req.SetRequestHeader("X-cons-ID", ConsumerID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", UserKey)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                Referensi_DPHO = req.ResponseText

            Catch oErr As Exception
                Referensi_DPHO = "E500" & vbCrLf & oErr.Message
            End Try
        End Function
        Public Function Referensi_Poli(ByVal uTime As Integer, ByVal UrlApotekOnline As String, ByVal ConsumerSecret As String, ByVal ConsumerID As String, ByVal UserKey As String, ByVal Parameter As String) As String
            Try
                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ConsumerSecret))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ConsumerID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                req = New WinHttp.WinHttpRequest
                req.Open("GET", UrlApotekOnline & "referensi/poli/" & Parameter, False)
                req.SetRequestHeader("X-cons-ID", ConsumerID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", UserKey)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                Referensi_Poli = req.ResponseText

            Catch oErr As Exception
                Referensi_Poli = "E500" & vbCrLf & oErr.Message
            End Try
        End Function
        Public Function Referensi_FasilitasKesehatan(ByVal uTime As Integer, ByVal UrlApotekOnline As String, ByVal ConsumerSecret As String, ByVal ConsumerID As String, ByVal UserKey As String, ByVal Parameter As String, ByVal Parameter_Text As String) As String
            Try
                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ConsumerSecret))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ConsumerID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                req = New WinHttp.WinHttpRequest
                req.Open("GET", UrlApotekOnline & "referensi/ppk/" & Parameter & "/" & Parameter_Text, False)
                req.SetRequestHeader("X-cons-ID", ConsumerID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", UserKey)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                Referensi_FasilitasKesehatan = req.ResponseText

            Catch oErr As Exception
                Referensi_FasilitasKesehatan = "E500" & vbCrLf & oErr.Message
            End Try
        End Function
        Public Function Referensi_SettingApotek(ByVal uTime As Integer, ByVal UrlApotekOnline As String, ByVal ConsumerSecret As String, ByVal ConsumerID As String, ByVal UserKey As String, ByVal Parameter As String) As String
            Try
                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ConsumerSecret))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ConsumerID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                req = New WinHttp.WinHttpRequest
                req.Open("GET", UrlApotekOnline & "referensi/settingppk/read/" & Parameter, False)
                req.SetRequestHeader("X-cons-ID", ConsumerID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", UserKey)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                Referensi_SettingApotek = req.ResponseText

            Catch oErr As Exception
                Referensi_SettingApotek = "E500" & vbCrLf & oErr.Message
            End Try
        End Function
        Public Function Referensi_spesialistik(ByVal uTime As Integer, ByVal UrlApotekOnline As String, ByVal ConsumerSecret As String, ByVal ConsumerID As String, ByVal UserKey As String) As String
            Try
                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ConsumerSecret))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ConsumerID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                req = New WinHttp.WinHttpRequest
                req.Open("GET", UrlApotekOnline & "referensi/spesialistik", False)
                req.SetRequestHeader("X-cons-ID", ConsumerID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", UserKey)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                Referensi_spesialistik = req.ResponseText

            Catch oErr As Exception
                Referensi_spesialistik = "E500" & vbCrLf & oErr.Message
            End Try
        End Function
        Public Function Referensi_Obat(ByVal uTime As Integer, ByVal UrlApotekOnline As String, ByVal ConsumerSecret As String, ByVal ConsumerID As String, ByVal UserKey As String, ByVal Parameter1 As String, ByVal Parameter2 As String, ByVal Parameter3 As String) As String
            Try
                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ConsumerSecret))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ConsumerID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                req = New WinHttp.WinHttpRequest
                req.Open("GET", UrlApotekOnline & "referensi/obat/" & Parameter1 & "/" & Parameter2 & "/" & Parameter3, False)
                req.SetRequestHeader("X-cons-ID", ConsumerID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", UserKey)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                Referensi_Obat = req.ResponseText

            Catch oErr As Exception
                Referensi_Obat = "E500" & vbCrLf & oErr.Message
            End Try
        End Function
#End Region
#Region "Obat"
        Public Function NonRacikan(ByVal uTime As Integer, ByVal UrlApotekOnline As String, ByVal ConsumerSecret As String, ByVal ConsumerID As String, ByVal UserKey As String, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    NonRacikan = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ConsumerSecret))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ConsumerID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = UrlApotekOnline & "obatnonracikan/v3/insert"

                req = New WinHttp.WinHttpRequest
                req.Open("POST", Url, False)
                req.SetRequestHeader("X-cons-ID", ConsumerID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", UserKey)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                NonRacikan = req.ResponseText

            Catch ex As Exception
                NonRacikan = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function Racikan(ByVal uTime As Integer, ByVal UrlApotekOnline As String, ByVal ConsumerSecret As String, ByVal ConsumerID As String, ByVal UserKey As String, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    Racikan = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ConsumerSecret))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ConsumerID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = UrlApotekOnline & "obatracikan/v3/insert"

                req = New WinHttp.WinHttpRequest
                req.Open("POST", Url, False)
                req.SetRequestHeader("X-cons-ID", ConsumerID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", UserKey)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                Racikan = req.ResponseText

            Catch ex As Exception
                Racikan = ex.ToString

                Throw ex
            End Try
        End Function
#End Region
#Region "Resep"
        Public Function SimpanResep(ByVal uTime As Integer, ByVal UrlApotekOnline As String, ByVal ConsumerSecret As String, ByVal ConsumerID As String, ByVal UserKey As String, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    SimpanResep = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ConsumerSecret))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ConsumerID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = UrlApotekOnline & "sjpresep/v3/insert"

                req = New WinHttp.WinHttpRequest
                req.Open("POST", Url, False)
                req.SetRequestHeader("X-cons-ID", ConsumerID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", UserKey)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                SimpanResep = req.ResponseText

            Catch ex As Exception
                SimpanResep = ex.ToString

                Throw ex
            End Try
        End Function
        Public Function HapusResep(ByVal uTime As Integer, ByVal UrlApotekOnline As String, ByVal ConsumerSecret As String, ByVal ConsumerID As String, ByVal UserKey As String, ByVal Request As String) As String
            Try
                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ConsumerSecret))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ConsumerID & "&" & uTime))
                ' Base 64 Encode
                Dim HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                req = New WinHttp.WinHttpRequest
                req.Open("DELETE", UrlApotekOnline & "hapusresep", False)
                req.SetRequestHeader("X-cons-id", ConsumerID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", UserKey)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")
                req.Send(Request)

                HapusResep = req.ResponseText
            Catch oErr As Exception
                HapusResep = "E500" & vbCrLf & oErr.Message
            End Try
        End Function
        Public Function DaftarResep(ByVal uTime As Integer, ByVal UrlApotekOnline As String, ByVal ConsumerSecret As String, ByVal ConsumerID As String, ByVal UserKey As String, ByVal Request As String) As String
            Try
                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ConsumerSecret))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ConsumerID & "&" & uTime))
                ' Base 64 Encode
                Dim HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                req = New WinHttp.WinHttpRequest
                req.Open("POST", UrlApotekOnline & "daftarresep", False)
                req.SetRequestHeader("X-cons-id", ConsumerID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", UserKey)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")
                req.Send(Request)

                DaftarResep = req.ResponseText
            Catch oErr As Exception
                DaftarResep = "E500" & vbCrLf & oErr.Message
            End Try
        End Function
#End Region
#Region "Pelayanan Obat"
        Public Function HapusPelayananObatNonRacikan(ByVal uTime As Integer, ByVal UrlApotekOnline As String, ByVal ConsumerSecret As String, ByVal ConsumerID As String, ByVal UserKey As String, ByVal Request As String) As String
            Try
                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ConsumerSecret))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ConsumerID & "&" & uTime))
                ' Base 64 Encode
                Dim HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                req = New WinHttp.WinHttpRequest
                req.Open("DELETE", UrlApotekOnline & "pelayanan/obat/hapus", False)
                req.SetRequestHeader("X-cons-id", ConsumerID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", UserKey)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")
                req.Send(Request)

                HapusPelayananObatNonRacikan = req.ResponseText
            Catch oErr As Exception
                HapusPelayananObatNonRacikan = "E500" & vbCrLf & oErr.Message
            End Try
        End Function
        Public Function DaftarPelayananObat(ByVal uTime As Integer, ByVal UrlApotekOnline As String, ByVal ConsumerSecret As String, ByVal ConsumerID As String, ByVal UserKey As String, ByVal Parameter As String) As String
            Try
                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ConsumerSecret))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ConsumerID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                req = New WinHttp.WinHttpRequest
                req.Open("GET", UrlApotekOnline & "obat/daftar/" & Parameter, False)
                req.SetRequestHeader("X-cons-ID", ConsumerID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", UserKey)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                DaftarPelayananObat = req.ResponseText

            Catch oErr As Exception
                DaftarPelayananObat = "E500" & vbCrLf & oErr.Message
            End Try
        End Function
        Public Function RiwayatPelayananObat(ByVal uTime As Integer, ByVal UrlApotekOnline As String, ByVal ConsumerSecret As String, ByVal ConsumerID As String, ByVal UserKey As String, ByVal Parameter1 As String, ByVal Parameter2 As String, ByVal Parameter3 As String) As String
            Try
                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ConsumerSecret))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ConsumerID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                req = New WinHttp.WinHttpRequest
                req.Open("GET", UrlApotekOnline & "riwayatobat/" & Parameter1 & "/" & Parameter2 & "/" & Parameter3, False)
                req.SetRequestHeader("X-cons-ID", ConsumerID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", UserKey)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                RiwayatPelayananObat = req.ResponseText

            Catch oErr As Exception
                RiwayatPelayananObat = "E500" & vbCrLf & oErr.Message
            End Try
        End Function
#End Region
#Region "SEP"
        Public Function CariNoKunjungan(ByVal uTime As Integer, ByVal UrlApotekOnline As String, ByVal ConsumerSecret As String, ByVal ConsumerID As String, ByVal UserKey As String, ByVal Parameter As String) As String
            Try
                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ConsumerSecret))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ConsumerID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                req = New WinHttp.WinHttpRequest
                req.Open("GET", UrlApotekOnline & "sep/" & Parameter, False)
                req.SetRequestHeader("X-cons-ID", ConsumerID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", UserKey)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                CariNoKunjungan = req.ResponseText

            Catch oErr As Exception
                CariNoKunjungan = "E500" & vbCrLf & oErr.Message
            End Try
        End Function
#End Region
#Region "Monitoring"
        Public Function DataKlaim(ByVal uTime As Integer, ByVal UrlApotekOnline As String, ByVal ConsumerSecret As String, ByVal ConsumerID As String, ByVal UserKey As String, ByVal Parameter1 As String, ByVal Parameter2 As String, ByVal Parameter3 As String, ByVal Parameter4 As String) As String
            Try
                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ConsumerSecret))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ConsumerID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                req = New WinHttp.WinHttpRequest
                req.Open("GET", UrlApotekOnline & "monitoring/klaim/" & Parameter1 & "/" & Parameter2 & "/" & Parameter3 & "/" & Parameter4, False)
                req.SetRequestHeader("X-cons-ID", ConsumerID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", UserKey)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                DataKlaim = req.ResponseText

            Catch oErr As Exception
                DataKlaim = "E500" & vbCrLf & oErr.Message
            End Try
        End Function
        Public Function UploadShareFolderLocal(ByVal ALAMATWEB As String, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    UploadShareFolderLocal = ""
                    Exit Function
                End If

                'Dim url As String = "http://localhost:8080/upload-api/api.php?action=upload"

                Dim result As String = String.Empty
                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB

                req = New WinHttp.WinHttpRequest
                req.Open("POST", Url, False)
                req.SetRequestHeader("Content-Type", "application/json")
                req.Send(Request)
                result = req.ResponseText

                UploadShareFolderLocal = result

            Catch ex As Exception
                UploadShareFolderLocal = ex.ToString
                Throw ex
            End Try
        End Function
#End Region
#End Region
#Region "RME - BPJS"
        'Public Function CompressGZip(input As String, Optional encoding As Encoding = Nothing) As Byte()
        '    encoding = If(encoding, Encoding.Unicode)
        '    Dim bytes As Byte() = encoding.GetBytes(input)
        '    Using stream As New MemoryStream()
        '        Using zipStream As New GZipStream(stream, CompressionMode.Compress)
        '            zipStream.Write(bytes, 0, bytes.Length)
        '            Return stream.ToArray()
        '        End Using
        '    End Using
        'End Function
        'Public Function Encrypt(consid As String, conspwd As String, kodefaskes As String, data As String) As String

        '    Dim key As String = consid & conspwd & kodefaskes

        '    Dim encData As String = Nothing
        '    Dim keys As Byte()() = GetHashKeys(key)

        '    Try
        '        encData = EncryptStringToBytes_Aes(data, keys(0), keys(1))
        '    Catch ex As CryptographicException
        '        ' handle error jika perlu
        '    Catch ex As ArgumentNullException
        '        ' handle error jika perlu
        '    End Try

        '    Return encData

        'End Function
        'Public Function EncryptStringToBytes_Aes(plainText As String, Key As Byte(), IV As Byte()) As String

        '    If plainText Is Nothing OrElse plainText.Length <= 0 Then
        '        Throw New ArgumentNullException(NameOf(plainText))
        '    End If

        '    If Key Is Nothing OrElse Key.Length = 0 Then
        '        Throw New ArgumentNullException(NameOf(Key))
        '    End If

        '    If IV Is Nothing OrElse IV.Length = 0 Then
        '        Throw New ArgumentNullException(NameOf(IV))
        '    End If

        '    Dim encrypted As Byte()

        '    Using aesAlg As Aes = Aes.Create()
        '        aesAlg.Key = Key
        '        aesAlg.IV = IV
        '        aesAlg.Mode = CipherMode.CBC
        '        aesAlg.Padding = PaddingMode.PKCS7

        '        Dim encryptor As ICryptoTransform = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV)

        '        Using msEncrypt As New MemoryStream()
        '            Using csEncrypt As New CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)
        '                Using swEncrypt As New StreamWriter(csEncrypt)
        '                    swEncrypt.Write(plainText)
        '                End Using
        '                encrypted = msEncrypt.ToArray()
        '            End Using
        '        End Using
        '    End Using

        '    ' hasil dalam Base64 (umumnya dipakai untuk API)
        '    Return Convert.ToBase64String(encrypted)

        'End Function
        'Public Function CompressThenEncrypt(data As String, consid As String, conspwd As String, kodefaskes As String) As String
        '    ' Langkah 1: Compress data dengan GZip
        '    Dim compressedBytes As Byte() = CompressGZip(data, Encoding.UTF8)

        '    ' Langkah 2: Convert compressed bytes ke string Base64 untuk dienkripsi
        '    Dim compressedBase64 As String = Convert.ToBase64String(compressedBytes)

        '    ' Langkah 3: Encrypt hasil compress
        '    Dim encryptedResult As String = Encrypt(consid, conspwd, kodefaskes, compressedBase64)

        '    Return encryptedResult
        'End Function
        ''Public Function CompressToGzip(jsonString As String) As Byte()
        ''    Dim inputBytes As Byte() = Encoding.UTF8.GetBytes(jsonString)

        ''    Using outputStream As New MemoryStream()
        ''        Using gzip As New GZipStream(outputStream, CompressionMode.Compress)
        ''            gzip.Write(inputBytes, 0, inputBytes.Length)
        ''        End Using
        ''        Return outputStream.ToArray()
        ''    End Using
        ''End Function
        ''Public Function DecompressGzip(gzipBytes As Byte()) As String
        ''    Using inputStream As New MemoryStream(gzipBytes)
        ''        Using gzip As New GZipStream(inputStream, CompressionMode.Decompress)
        ''            Using reader As New StreamReader(gzip)
        ''                Return reader.ReadToEnd()
        ''            End Using
        ''        End Using
        ''    End Using
        ''End Function

        '' Compress string to gzip byte array
        'Public Shared Function CompressString(text As String) As Byte()
        '    If String.IsNullOrEmpty(text) Then
        '        Return New Byte() {}
        '    End If

        '    Dim bytes As Byte() = Encoding.UTF8.GetBytes(text)

        '    Using compressedStream As New MemoryStream()
        '        Using gzipStream As New GZipStream(compressedStream, CompressionMode.Compress)
        '            gzipStream.Write(bytes, 0, bytes.Length)
        '        End Using
        '        Return compressedStream.ToArray()
        '    End Using
        'End Function

        '' Decompress gzip byte array to string
        'Public Shared Function DecompressString(compressedBytes As Byte()) As String
        '    If compressedBytes Is Nothing OrElse compressedBytes.Length = 0 Then
        '        Return String.Empty
        '    End If

        '    Using compressedStream As New MemoryStream(compressedBytes)
        '        Using gzipStream As New GZipStream(compressedStream, CompressionMode.Decompress)
        '            Using reader As New StreamReader(gzipStream, Encoding.UTF8)
        '                Return reader.ReadToEnd()
        '            End Using
        '        End Using
        '    End Using
        'End Function
        '' Async version
        'Public Shared Async Function DecompressStringAsync(compressedBytes As Byte()) As Task(Of String)
        '    If compressedBytes Is Nothing OrElse compressedBytes.Length = 0 Then
        '        Return String.Empty
        '    End If

        '    Using compressedStream As New MemoryStream(compressedBytes)
        '        Using gzipStream As New GZipStream(compressedStream, CompressionMode.Decompress)
        '            Using reader As New StreamReader(gzipStream, Encoding.UTF8)
        '                Return Await reader.ReadToEndAsync()
        '            End Using
        '        End Using
        '    End Using
        'End Function
        Public Shared Function Encrypt(ByVal data As String, ByVal consid As String, ByVal secretKey As String, ByVal koders As String) As String
            ' Kompres data dengan GZip (setara dengan gzencode dengan level 9)
            Dim compressedData As Byte() = CompressData(data)

            ' Metode enkripsi
            Dim encryptMethod As String = "AES-256-CBC"
            Dim encryptKey As String = consid & secretKey & koders

            ' Buat key hash dengan SHA256
            Using sha256 As SHA256 = SHA256.Create()
                Dim keyBytes As Byte() = Encoding.UTF8.GetBytes(encryptKey)
                Dim keyHash As Byte() = sha256.ComputeHash(keyBytes)

                ' IV diambil dari 16 byte pertama key hash
                Dim iv As Byte() = New Byte(15) {}
                Array.Copy(keyHash, iv, 16)

                ' Enkripsi dengan AES-256-CBC
                Using aes As Aes = Aes.Create()
                    aes.KeySize = 256
                    aes.BlockSize = 128
                    aes.Key = keyHash
                    aes.IV = iv
                    aes.Mode = CipherMode.CBC
                    aes.Padding = PaddingMode.PKCS7

                    Using encryptor As ICryptoTransform = aes.CreateEncryptor()
                        Dim encryptedData As Byte() = encryptor.TransformFinalBlock(compressedData, 0, compressedData.Length)
                        ' Return Base64 encoded result
                        Return Convert.ToBase64String(encryptedData)
                    End Using
                End Using
            End Using
        End Function
        Public Shared Function CompressData(ByVal data As String) As Byte()
            Dim inputBytes As Byte() = Encoding.UTF8.GetBytes(data)

            Using outputStream As New MemoryStream()
                Using gzipStream As New GZipStream(outputStream, CompressionLevel.Optimal)
                    gzipStream.Write(inputBytes, 0, inputBytes.Length)
                End Using
                Return outputStream.ToArray()
            End Using
        End Function
        Public Function CompressGZip(input As String, Optional encoding As Encoding = Nothing) As Byte()
            encoding = If(encoding, Encoding.Unicode)
            Dim bytes As Byte() = encoding.GetBytes(input)
            Using stream As New MemoryStream()
                Using zipStream As New GZipStream(stream, CompressionMode.Compress)
                    zipStream.Write(bytes, 0, bytes.Length)
                    Return stream.ToArray()
                End Using
            End Using
        End Function
        Public Function EncryptBPJS(ByVal consid As String, ByVal conspwd As String, ByVal kodefaskes As String, ByVal data As String) As String
            Try
                ' Combine consid, conspwd, and kodefaskes to create the data to hash
                Dim keyData As String = consid & conspwd & kodefaskes

                ' Use HMACSHA256 to generate a hash-based key from the password
                ' Note: Typically the key for HMAC should be the password, and data to hash is the combined string
                Using hmac As New HMACSHA256(Encoding.UTF8.GetBytes(conspwd))
                    ' Compute hash of the combined data
                    Dim hashBytes As Byte() = hmac.ComputeHash(Encoding.UTF8.GetBytes(keyData))

                    ' Use first 16 bytes for AES key (or 32 bytes depending on AES mode)
                    ' For AES-256, we need 32 bytes; for AES-128, 16 bytes
                    ' Adjust according to your EncryptStringToBytes_Aes implementation
                    Dim aesKey(31) As Byte ' 32 bytes for AES-256
                    Dim aesIV(15) As Byte  ' 16 bytes for IV

                    ' Copy hash bytes to key and IV
                    ' Option 1: Use different parts of hash for key and IV
                    Array.Copy(hashBytes, 0, aesKey, 0, Math.Min(32, hashBytes.Length))
                    If hashBytes.Length >= 48 Then
                        Array.Copy(hashBytes, 32, aesIV, 0, 16)
                    Else
                        ' If hash is shorter, pad or use another method for IV
                        Array.Copy(hashBytes, 0, aesIV, 0, Math.Min(16, hashBytes.Length))
                    End If

                    ' Encrypt the data
                    Return EncryptStringToBytes_Aes(data, aesKey, aesIV)
                End Using

            Catch ex As CryptographicException
                ' Log error if needed
                Return String.Empty
            Catch ex As ArgumentNullException
                ' Log error if needed
                Return String.Empty
            End Try
        End Function
        Private Function EncryptStringToBytes_Aes(ByVal plainText As String, ByVal key As Byte(), ByVal iv As Byte()) As String
            If plainText Is Nothing OrElse plainText.Length <= 0 Then
                Throw New ArgumentNullException("plainText")
            End If
            If key Is Nothing OrElse key.Length <= 0 Then
                Throw New ArgumentNullException("key")
            End If
            If iv Is Nothing OrElse iv.Length <= 0 Then
                Throw New ArgumentNullException("iv")
            End If

            Dim encrypted As Byte() = Nothing

            Using aesAlg As Aes = Aes.Create()
                aesAlg.Key = key
                aesAlg.IV = iv

                Dim encryptor As ICryptoTransform = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV)

                Using msEncrypt As New System.IO.MemoryStream()
                    Using csEncrypt As New CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)
                        Using swEncrypt As New System.IO.StreamWriter(csEncrypt)
                            swEncrypt.Write(plainText)
                        End Using
                        encrypted = msEncrypt.ToArray()
                    End Using
                End Using
            End Using

            Return Convert.ToBase64String(encrypted)
        End Function
        Public Function InsertMedicalRecord(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertMedicalRecord = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "eclaim/rekammedis/insert"

                req = New WinHttp.WinHttpRequest
                req.Open("POST", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "text/plain")
                'req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")
                'req.SetRequestHeader("Content-Type", "application/json")

                req.Send(Request)

                InsertMedicalRecord = req.ResponseText

                If InsertMedicalRecord = "" Then
                    InsertMedicalRecord = req.Status & " " & req.StatusText
                End If
            Catch ex As Exception
                InsertMedicalRecord = ex.ToString

                Throw ex
            End Try
        End Function
        'Public Function GenerateKey(keyString As String) As Byte()
        '    Using sha256 As SHA256 = SHA256.Create()
        '        Return sha256.ComputeHash(Encoding.UTF8.GetBytes(keyString))
        '    End Using
        'End Function
        'Public Function EncryptAES(plainText As String, keyString As String) As String
        '    Dim keyBytes As Byte() = GenerateKey(keyString)
        '    Dim iv(15) As Byte ' 16 byte IV (default 0)

        '    Using aes As Aes = Aes.Create()
        '        aes.Key = keyBytes
        '        aes.IV = iv
        '        aes.Mode = CipherMode.CBC
        '        aes.Padding = PaddingMode.PKCS7

        '        Using ms As New MemoryStream()
        '            Using cs As New CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write)
        '                Using sw As New StreamWriter(cs)
        '                    sw.Write(plainText)
        '                End Using
        '            End Using
        '            Return Convert.ToBase64String(ms.ToArray())
        '        End Using
        '    End Using
        'End Function
#End Region
#Region "RME BPJS 2"
        Public Function CompressGZipBPJS(input As String, Optional encoding As Encoding = Nothing) As Byte()
            encoding = If(encoding, Encoding.Unicode)
            Dim bytes As Byte() = encoding.GetBytes(input)
            Using stream As New MemoryStream()
                Using zipStream As New GZipStream(stream, CompressionMode.Compress)
                    zipStream.Write(bytes, 0, bytes.Length)
                    Return stream.ToArray()
                End Using
            End Using
        End Function
        ' Fungsi Encrypt utama
        Public Function EncryptBPJS2(ByVal consid As String, ByVal secretkey As String, ByVal kodefaskes As String, ByVal data As String) As String

            ' Gabungkan key
            'CONSID & "&" & uTime
            Dim key As String = consid & secretkey & kodefaskes

            Dim encData As String = Nothing

            Try
                ' Dapatkan hash keys
                Dim keys As Byte()() = GetHashKeys(key)

                ' Enkripsi data
                encData = EncryptStringToBytes_Aes2(data, keys(0), keys(1))

            Catch ex As CryptographicException
                ' Log error jika diperlukan
                encData = String.Empty
            Catch ex As ArgumentNullException
                ' Log error jika diperlukan
                encData = String.Empty
            End Try

            Return encData
        End Function
        Private Function EncryptStringToBytes_Aes2(ByVal plainText As String, ByVal Key As Byte(), ByVal IV As Byte()) As String
            If plainText Is Nothing OrElse plainText.Length <= 0 Then
                Throw New ArgumentNullException("plainText")
            End If
            If Key Is Nothing OrElse Key.Length <= 0 Then
                Throw New ArgumentNullException("Key")
            End If
            If IV Is Nothing OrElse IV.Length <= 0 Then
                Throw New ArgumentNullException("IV")
            End If

            Dim encrypted As Byte() = Nothing

            Using aesAlg As Aes = Aes.Create()
                aesAlg.Key = Key
                aesAlg.IV = IV
                aesAlg.Mode = CipherMode.CBC
                aesAlg.Padding = PaddingMode.PKCS7

                Dim encryptor As ICryptoTransform = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV)

                Using msEncrypt As New MemoryStream()
                    Using csEncrypt As New CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)
                        Using swEncrypt As New StreamWriter(csEncrypt)
                            swEncrypt.Write(plainText)
                        End Using
                        encrypted = msEncrypt.ToArray()
                    End Using
                End Using
            End Using

            Return Convert.ToBase64String(encrypted)
        End Function
#End Region
        '#Region "Satu Sehat"
        '        Public Function GetAccessToken(ByVal url As String, ByVal client_id As String, ByVal client_secret As String) As String
        '            Try
        '                If Not oConnection.GetConnection() Then
        '                    GetAccessToken = ""
        '                    Exit Function
        '                End If

        '                Dim postData As String
        '                Dim http As New WinHttp.WinHttpRequest

        '                postData = "client_id=" & client_id & "&client_secret=" & client_secret

        '                http.Open("POST", url, False)
        '                http.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded")

        '                http.Send(postData)

        '                GetAccessToken = http.ResponseText

        '            Catch ex As Exception
        '                GetAccessToken = ex.ToString
        '                Throw ex
        '            End Try
        '        End Function
        '        Public Function PatientBayiSearchNIKIbu(ByVal url As String, ByVal client_id As String, ByVal client_secret As String, ByVal NIK As String, ByVal birthdate As String) As String
        '            Try
        '                If Not oConnection.GetConnection() Then
        '                    PatientBayiSearchNIKIbu = ""
        '                    Exit Function
        '                End If

        '                url = url & "/Patient?identifier=https://fhir.kemkes.go.id/id/nik-ibu|" & NIK & "&birthdate=" & birthdate & ""

        '                Dim postData As String
        '                Dim http As New WinHttp.WinHttpRequest

        '                postData = "client_id=" & client_id & "&client_secret=" & client_secret

        '                http.Open("POST", url, False)
        '                http.SetRequestHeader("Content-Type", "application/json")

        '                http.Send(postData)

        '                PatientBayiSearchNIKIbu = http.ResponseText

        '            Catch ex As Exception
        '                PatientBayiSearchNIKIbu = ex.ToString
        '                Throw ex
        '            End Try
        '        End Function
        '#End Region
    End Class
End Namespace