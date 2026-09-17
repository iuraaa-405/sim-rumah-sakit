Imports DataAccess.My.Resources
Imports System.Security.Cryptography
Imports System.Text

Namespace Brigging
    Public Class clsSetKoneksi_bak
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing

        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0

        Public Sub New(Optional ByVal sConnection As String = "")
            If sConnection = "" Then
                oConnection = New Setting.clsConnectionMain
                oError = New Setting.clsError
            Else
                oConnection = New Setting.clsConnectionMain("TAX")
                oError = New Setting.clsError("TAX")
            End If

            sMODUL = "SET_KONEKSI"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As SET_KONEKSI
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New SET_KONEKSI
        End Function
        Public Function GetData() As List(Of SET_KONEKSI)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.SET_KONEKSIs.OrderBy(Function(x) x.KDKONEKSI).ToList()
        End Function
        Public Function GetData(ByVal sKDKONEKSI As String) As SET_KONEKSI
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.KDKONEKSI = sKDKONEKSI)
        End Function
        Public Function GetDataAktive() As SET_KONEKSI
            If Not oConnection.GetConnection() Then
                GetDataAktive = Nothing
                Exit Function
            End If
            GetDataAktive = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = "VCLAIM2" And x.ISACTIVE = True)
        End Function
        Public Function GetDataFooter(ByVal SKDREPORT As String) As SET_REPORT
            If Not oConnection.GetConnection() Then
                GetDataFooter = Nothing
                Exit Function
            End If
            GetDataFooter = oConnection.db.SET_REPORTs.FirstOrDefault(Function(x) x.KDREPORT = SKDREPORT)
        End Function
        Public Function IsExist(ByVal sNAME_DISPLAY As String) As Boolean
            If Not oConnection.GetConnection() Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
#Region "VCLAIM2"
        '****** Integrasi SEP dan Inacbg *******
        Public Function IntegrasiSEPdenganInacbg(ByVal sNAME_DISPLAY As String, ByVal Parameter As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    IntegrasiSEPdenganInacbg = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "/sep/cbg/" & Parameter & ""

                    req = New WinHttp.WinHttpRequest
                    req.Open("GET", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                    req.Send()

                    result = req.ResponseText

                End If

                IntegrasiSEPdenganInacbg = result

            Catch ex As Exception
                IntegrasiSEPdenganInacbg = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        '****** Potensi Suplesi Jasa Raharja *******
        '****** Referensi *******
        Public Function GetDataVClaimReferensiDiagnosa(ByVal sNAME_DISPLAY As String, ByVal Parameter As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiDiagnosa = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "referensi/diagnosa/" & Parameter & ""

                    req = New WinHttp.WinHttpRequest
                    req.Open("GET", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                    req.Send()

                    result = req.ResponseText

                End If

                GetDataVClaimReferensiDiagnosa = result

            Catch ex As Exception
                GetDataVClaimReferensiDiagnosa = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimReferensiPoli(ByVal sNAME_DISPLAY As String, ByVal Parameter As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiPoli = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "referensi/poli/" & Parameter

                    req = New WinHttp.WinHttpRequest
                    req.Open("GET", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                    req.Send()

                    result = req.ResponseText

                End If

                GetDataVClaimReferensiPoli = result

            Catch ex As Exception
                GetDataVClaimReferensiPoli = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimReferensiFasilitasKesehatan(ByVal sNAME_DISPLAY As String, ByVal Parameter1 As String, ByVal Parameter2 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiFasilitasKesehatan = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "referensi/faskes/" & Parameter1 & "/" & Parameter2

                    req = New WinHttp.WinHttpRequest
                    req.Open("GET", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                    req.Send()

                    result = req.ResponseText

                End If

                GetDataVClaimReferensiFasilitasKesehatan = result

            Catch ex As Exception
                GetDataVClaimReferensiFasilitasKesehatan = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimReferensiDokterDPJP(ByVal sNAME_DISPLAY As String, ByVal Parameter1 As String, ByVal Parameter2 As String, ByVal Parameter3 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiDokterDPJP = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "referensi/dokter/pelayanan/" & Parameter1 & "/tglPelayanan/" & Parameter2 & "/Spesialis/" & Parameter3

                    req = New WinHttp.WinHttpRequest
                    req.Open("GET", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                    req.Send()

                    result = req.ResponseText

                End If

                GetDataVClaimReferensiDokterDPJP = result

            Catch ex As Exception
                GetDataVClaimReferensiDokterDPJP = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimReferensiPropinsi(ByVal sNAME_DISPLAY As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiPropinsi = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "referensi/propinsi"

                    req = New WinHttp.WinHttpRequest
                    req.Open("GET", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                    req.Send()

                    result = req.ResponseText

                End If

                GetDataVClaimReferensiPropinsi = result

            Catch ex As Exception
                GetDataVClaimReferensiPropinsi = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimReferensiKabupaten(ByVal sNAME_DISPLAY As String, ByVal Parameter As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiKabupaten = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "referensi/kabupaten/propinsi/" & Parameter

                    req = New WinHttp.WinHttpRequest
                    req.Open("GET", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                    req.Send()

                    result = req.ResponseText

                End If

                GetDataVClaimReferensiKabupaten = result

            Catch ex As Exception
                GetDataVClaimReferensiKabupaten = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimReferensiKecamatan(ByVal sNAME_DISPLAY As String, ByVal Parameter As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiKecamatan = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "referensi/kecamatan/kabupaten/" & Parameter

                    req = New WinHttp.WinHttpRequest
                    req.Open("GET", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                    req.Send()

                    result = req.ResponseText

                End If

                GetDataVClaimReferensiKecamatan = result

            Catch ex As Exception
                GetDataVClaimReferensiKecamatan = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimReferensiProcedure(ByVal sNAME_DISPLAY As String, ByVal Parameter As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiProcedure = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "referensi/procedure/" & Parameter

                    req = New WinHttp.WinHttpRequest
                    req.Open("GET", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                    req.Send()

                    result = req.ResponseText

                End If

                GetDataVClaimReferensiProcedure = result

            Catch ex As Exception
                GetDataVClaimReferensiProcedure = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimReferensiKelasRawat(ByVal sNAME_DISPLAY As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiKelasRawat = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "referensi/kelasrawat"

                    req = New WinHttp.WinHttpRequest
                    req.Open("GET", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                    req.Send()

                    result = req.ResponseText

                End If

                GetDataVClaimReferensiKelasRawat = result

            Catch ex As Exception
                GetDataVClaimReferensiKelasRawat = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimReferensiDokter(ByVal sNAME_DISPLAY As String, ByVal Parameter As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiDokter = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "referensi/dokter/" & Parameter

                    req = New WinHttp.WinHttpRequest
                    req.Open("GET", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                    req.Send()

                    result = req.ResponseText

                End If

                GetDataVClaimReferensiDokter = result

            Catch ex As Exception
                GetDataVClaimReferensiDokter = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimReferensiSpesialistik(ByVal sNAME_DISPLAY As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiSpesialistik = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "referensi/spesialistik"

                    req = New WinHttp.WinHttpRequest
                    req.Open("GET", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                    req.Send()

                    result = req.ResponseText

                End If

                GetDataVClaimReferensiSpesialistik = result

            Catch ex As Exception
                GetDataVClaimReferensiSpesialistik = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimReferensiRuangRawat(ByVal sNAME_DISPLAY As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiRuangRawat = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "referensi/ruangrawat"

                    req = New WinHttp.WinHttpRequest
                    req.Open("GET", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                    req.Send()

                    result = req.ResponseText

                End If

                GetDataVClaimReferensiRuangRawat = result

            Catch ex As Exception
                GetDataVClaimReferensiRuangRawat = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimReferensiCaraKeluar(ByVal sNAME_DISPLAY As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiCaraKeluar = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "referensi/carakeluar"

                    req = New WinHttp.WinHttpRequest
                    req.Open("GET", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                    req.Send()

                    result = req.ResponseText

                End If

                GetDataVClaimReferensiCaraKeluar = result

            Catch ex As Exception
                GetDataVClaimReferensiCaraKeluar = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimReferensipascapulang(ByVal sNAME_DISPLAY As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensipascapulang = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "referensi/pascapulang"

                    req = New WinHttp.WinHttpRequest
                    req.Open("GET", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                    req.Send()

                    result = req.ResponseText

                End If

                GetDataVClaimReferensipascapulang = result

            Catch ex As Exception
                GetDataVClaimReferensipascapulang = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        '****** Peserta *******
        Public Function GetDataVClaimPesertaNoKartuBPJS(ByVal sNAME_DISPLAY As String, ByVal Parameter1 As String, ByVal Parameter2 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimPesertaNoKartuBPJS = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "Peserta/nokartu/" & Parameter1 & "/tglSEP/" & Parameter2

                    req = New WinHttp.WinHttpRequest
                    req.Open("GET", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                    req.Send()

                    result = req.ResponseText

                End If

                GetDataVClaimPesertaNoKartuBPJS = result

            Catch ex As Exception
                GetDataVClaimPesertaNoKartuBPJS = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimPesertaNIK(ByVal sNAME_DISPLAY As String, ByVal Parameter1 As String, ByVal Parameter2 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimPesertaNIK = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "Peserta/nik/" & Parameter1 & "/tglSEP/" & Parameter2

                    req = New WinHttp.WinHttpRequest
                    req.Open("GET", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                    req.Send()

                    result = req.ResponseText

                End If

                GetDataVClaimPesertaNIK = result

            Catch ex As Exception
                GetDataVClaimPesertaNIK = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        '****** SEP *******
        '****** Insert SEP *******
        Public Function InsertSEP(ByVal sNAME_DISPLAY As String, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertSEP = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "SEP/1.1/insert"

                    req = New WinHttp.WinHttpRequest
                    req.Open("POST", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                    req.Send(Request)

                    result = req.ResponseText

                End If

                InsertSEP = result

            Catch ex As Exception
                InsertSEP = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateSEP(ByVal sNAME_DISPLAY As String, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    UpdateSEP = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "SEP/1.1/Update"

                    req = New WinHttp.WinHttpRequest
                    req.Open("PUT", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                    req.Send(Request)

                    result = req.ResponseText

                End If

                UpdateSEP = result

            Catch ex As Exception
                UpdateSEP = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function HapusSEP(ByVal sNAME_DISPLAY As String, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    HapusSEP = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "SEP/Delete"

                    req = New WinHttp.WinHttpRequest
                    req.Open("DELETE", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                    req.Send(Request)

                    result = req.ResponseText

                End If

                HapusSEP = result

            Catch ex As Exception
                HapusSEP = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function CariSEP(ByVal sNAME_DISPLAY As String, ByVal Parameter As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    CariSEP = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "SEP/" & Parameter & ""

                    req = New WinHttp.WinHttpRequest
                    req.Open("GET", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                    req.Send()

                    result = req.ResponseText

                End If

                CariSEP = result

            Catch ex As Exception
                CariSEP = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function Approval_Pengajuan(ByVal sNAME_DISPLAY As String, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    Approval_Pengajuan = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "Sep/pengajuanSEP"

                    req = New WinHttp.WinHttpRequest
                    req.Open("POST", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                    req.Send(Request)

                    result = req.ResponseText

                End If

                Approval_Pengajuan = result

            Catch ex As Exception
                Approval_Pengajuan = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function Approval_ApprovalPengajuanSEP(ByVal sNAME_DISPLAY As String, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    Approval_ApprovalPengajuanSEP = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "Sep/aprovalSEP"

                    req = New WinHttp.WinHttpRequest
                    req.Open("POST", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                    req.Send(Request)

                    result = req.ResponseText

                End If

                Approval_ApprovalPengajuanSEP = result

            Catch ex As Exception
                Approval_ApprovalPengajuanSEP = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateTanggalPulang(ByVal sNAME_DISPLAY As String, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    UpdateTanggalPulang = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "Sep/updtglplg"

                    req = New WinHttp.WinHttpRequest
                    req.Open("PUT", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                    req.Send(Request)

                    result = req.ResponseText

                End If

                UpdateTanggalPulang = result

            Catch ex As Exception
                UpdateTanggalPulang = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        '****** Potensi Suplesi Jasa Raharja *******
        Public Function GetDataSuplesiJasaRaharja(ByVal sNAME_DISPLAY As String, ByVal Parameter1 As String, ByVal Parameter2 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataSuplesiJasaRaharja = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "sep/JasaRaharja/Suplesi/" & Parameter1 & "/tglPelayanan/" & Parameter2

                    req = New WinHttp.WinHttpRequest
                    req.Open("GET", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                    req.Send()

                    result = req.ResponseText

                End If

                GetDataSuplesiJasaRaharja = result

            Catch ex As Exception
                GetDataSuplesiJasaRaharja = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        '****** Cari Rujukan *******
        Public Function CariRujukan(ByVal sNAME_DISPLAY As String, ByVal Parameter As String, ByVal JenisRujukan As Integer) As String
            Try
                If Not oConnection.GetConnection() Then
                    CariRujukan = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & IIf(JenisRujukan = 0, "Rujukan/", "Rujukan/RS/") & Parameter

                    req = New WinHttp.WinHttpRequest
                    req.Open("GET", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                    req.Send()

                    result = req.ResponseText

                End If

                CariRujukan = result

            Catch ex As Exception
                CariRujukan = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function CariRujukanKartuSatuRecord(ByVal sNAME_DISPLAY As String, ByVal Parameter As String, ByVal JenisRujukan As Integer) As String
            Try
                If Not oConnection.GetConnection() Then
                    CariRujukanKartuSatuRecord = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & IIf(JenisRujukan = 0, "Rujukan/Peserta/", "Rujukan/RS/Peserta/") & Parameter

                    req = New WinHttp.WinHttpRequest
                    req.Open("GET", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                    req.Send()

                    result = req.ResponseText

                End If

                CariRujukanKartuSatuRecord = result

            Catch ex As Exception
                CariRujukanKartuSatuRecord = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function CariRujukanKartuMultiRecord(ByVal sNAME_DISPLAY As String, ByVal Parameter As String, ByVal JenisRujukan As Integer) As String
            Try
                If Not oConnection.GetConnection() Then
                    CariRujukanKartuMultiRecord = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & IIf(JenisRujukan = 0, "Rujukan/List/Peserta/", "Rujukan/RS/List/Peserta/") & Parameter

                    req = New WinHttp.WinHttpRequest
                    req.Open("GET", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                    req.Send()

                    result = req.ResponseText

                End If

                CariRujukanKartuMultiRecord = result

            Catch ex As Exception
                CariRujukanKartuMultiRecord = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        '******* Rujukan *********
        Public Function InsertRujukan(ByVal sNAME_DISPLAY As String, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertRujukan = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "Rujukan/insert"

                    req = New WinHttp.WinHttpRequest
                    req.Open("POST", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                    req.Send(Request)

                    result = req.ResponseText

                End If

                InsertRujukan = result

            Catch ex As Exception
                InsertRujukan = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateRujukan(ByVal sNAME_DISPLAY As String, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    UpdateRujukan = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "Rujukan/update"

                    req = New WinHttp.WinHttpRequest
                    req.Open("PUT", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                    req.Send(Request)

                    result = req.ResponseText

                End If

                UpdateRujukan = result

            Catch ex As Exception
                UpdateRujukan = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function DeleteRujukan(ByVal sNAME_DISPLAY As String, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    DeleteRujukan = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "Rujukan/delete"

                    req = New WinHttp.WinHttpRequest
                    req.Open("DELETE", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                    req.Send(Request)

                    result = req.ResponseText

                End If

                DeleteRujukan = result

            Catch ex As Exception
                DeleteRujukan = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        '******* Monitoring **********
        Public Function GetDataVClaimMonitoringDataKunjungan(ByVal sNAME_DISPLAY As String, ByVal Paramater1 As String, ByVal Parameter2 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimMonitoringDataKunjungan = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "Monitoring/Kunjungan/Tanggal/" & Paramater1 & "/JnsPelayanan/" & Parameter2 & ""

                    req = New WinHttp.WinHttpRequest
                    req.Open("GET", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                    req.Send()

                    result = req.ResponseText

                End If

                GetDataVClaimMonitoringDataKunjungan = result

            Catch ex As Exception
                GetDataVClaimMonitoringDataKunjungan = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimMonitoringDataKlaim(ByVal sNAME_DISPLAY As String, ByVal Paramater1 As String, ByVal Parameter2 As String, ByVal Parameter3 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimMonitoringDataKlaim = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "Monitoring/Klaim/Tanggal/" & Paramater1 & "/JnsPelayanan/" & Parameter2 & "/Status/" & Parameter3 & ""

                    req = New WinHttp.WinHttpRequest
                    req.Open("GET", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                    req.Send()

                    result = req.ResponseText

                End If

                GetDataVClaimMonitoringDataKlaim = result

            Catch ex As Exception
                GetDataVClaimMonitoringDataKlaim = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimMonitoringDataHistoriPelayananPeserta(ByVal sNAME_DISPLAY As String, ByVal Paramater1 As String, ByVal Parameter2 As String, ByVal Parameter3 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimMonitoringDataHistoriPelayananPeserta = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "monitoring/HistoriPelayanan/NoKartu/" & Paramater1 & "/tglAwal/" & Parameter2 & "/tglAkhir/" & Parameter3 & ""

                    req = New WinHttp.WinHttpRequest
                    req.Open("GET", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                    req.Send()

                    result = req.ResponseText

                End If

                GetDataVClaimMonitoringDataHistoriPelayananPeserta = result

            Catch ex As Exception
                GetDataVClaimMonitoringDataHistoriPelayananPeserta = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimMonitoringDataKlaimJaminanJasaRaharja(ByVal sNAME_DISPLAY As String, ByVal Paramater1 As String, ByVal Parameter2 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimMonitoringDataKlaimJaminanJasaRaharja = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "monitoring/JasaRaharja/tglMulai/" & Paramater1 & "/tglAkhir/" & Parameter2 & ""

                    req = New WinHttp.WinHttpRequest
                    req.Open("GET", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                    req.Send()

                    result = req.ResponseText

                End If

                GetDataVClaimMonitoringDataKlaimJaminanJasaRaharja = result

            Catch ex As Exception
                GetDataVClaimMonitoringDataKlaimJaminanJasaRaharja = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function InsertRencanaKontrol(ByVal sNAME_DISPLAY As String, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertRencanaKontrol = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "RencanaKontrol/insert"

                    req = New WinHttp.WinHttpRequest
                    req.Open("POST", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                    req.Send(Request)

                    result = req.ResponseText

                End If

                InsertRencanaKontrol = result

            Catch ex As Exception
                InsertRencanaKontrol = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateRencanaKontrol(ByVal sNAME_DISPLAY As String, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    UpdateRencanaKontrol = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "RencanaKontrol/Update"

                    req = New WinHttp.WinHttpRequest
                    req.Open("PUT", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                    req.Send(Request)

                    result = req.ResponseText

                End If

                UpdateRencanaKontrol = result

            Catch ex As Exception
                UpdateRencanaKontrol = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function HapusRencanaKontrol(ByVal sNAME_DISPLAY As String, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    HapusRencanaKontrol = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "RencanaKontrol/Delete"

                    req = New WinHttp.WinHttpRequest
                    req.Open("DELETE", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                    req.Send(Request)

                    result = req.ResponseText

                End If

                HapusRencanaKontrol = result

            Catch ex As Exception
                HapusRencanaKontrol = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function CariNomorRencanaKontrol(ByVal sNAME_DISPLAY As String, ByVal Parameter As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    CariNomorRencanaKontrol = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "RencanaKontrol/noSuratKontrol/" & Parameter

                    req = New WinHttp.WinHttpRequest
                    req.Open("GET", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                    req.Send()

                    result = req.ResponseText

                End If

                CariNomorRencanaKontrol = result

            Catch ex As Exception
                CariNomorRencanaKontrol = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function DataNomorRencanaKontrol(ByVal sNAME_DISPLAY As String, ByVal Parameter1 As String, ByVal Parameter2 As String, ByVal Parameter3 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    DataNomorRencanaKontrol = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "RencanaKontrol/ListRencanaKontrol/tglAwal/" & Parameter1 & "/tglAkhir/" & Parameter2 & "/filter/" & Parameter3

                    req = New WinHttp.WinHttpRequest
                    req.Open("GET", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                    req.Send()

                    result = req.ResponseText

                End If

                DataNomorRencanaKontrol = result

            Catch ex As Exception
                DataNomorRencanaKontrol = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function DataPoliSpesialistik(ByVal sNAME_DISPLAY As String, ByVal Parameter1 As String, ByVal Parameter2 As String, ByVal Parameter3 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    DataPoliSpesialistik = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "RencanaKontrol/ListSpesialistik/JnsKontrol/" & Parameter1 & "/nomor/" & Parameter2 & "/TglRencanaKontrol/" & Parameter3

                    req = New WinHttp.WinHttpRequest
                    req.Open("GET", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                    req.Send()

                    result = req.ResponseText

                End If

                DataPoliSpesialistik = result

            Catch ex As Exception
                DataPoliSpesialistik = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function DataDokter(ByVal sNAME_DISPLAY As String, ByVal Parameter1 As String, ByVal Parameter2 As String, ByVal Parameter3 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    DataDokter = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "RencanaKontrol/JadwalPraktekDokter/JnsKontrol/" & Parameter1 & "/KdPoli/" & Parameter2 & "/TglRencanaKontrol/" & Parameter3

                    req = New WinHttp.WinHttpRequest
                    req.Open("GET", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                    req.Send()

                    result = req.ResponseText

                End If

                DataDokter = result

            Catch ex As Exception
                DataDokter = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
#End Region
#Region "APLICARE"
        Public Function GetDataAplicareReferensiKelasRawat(ByVal sNAME_DISPLAY As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataAplicareReferensiKelasRawat = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "rest/ref/kelas"

                    req = New WinHttp.WinHttpRequest
                    req.Open("GET", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "application/json")

                    req.Send()

                    result = req.ResponseText

                End If

                GetDataAplicareReferensiKelasRawat = result

            Catch ex As Exception
                GetDataAplicareReferensiKelasRawat = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateKetersediaanTempatTidur(ByVal sNAME_DISPLAY As String, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    UpdateKetersediaanTempatTidur = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "rest/bed/update/" & ds.PPKPELAYANAN

                    req = New WinHttp.WinHttpRequest
                    req.Open("POST", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "application/json")
                    req.Send(Request)
                    result = req.ResponseText

                End If

                UpdateKetersediaanTempatTidur = result

            Catch ex As Exception
                UpdateKetersediaanTempatTidur = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function RuanganBaru(ByVal sNAME_DISPLAY As String, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    RuanganBaru = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "rest/bed/create/" & ds.PPKPELAYANAN

                    req = New WinHttp.WinHttpRequest
                    req.Open("POST", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "application/json")
                    req.Send(Request)
                    result = req.ResponseText

                End If

                RuanganBaru = result

            Catch ex As Exception
                RuanganBaru = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function RuanganHapus(ByVal sNAME_DISPLAY As String, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    RuanganHapus = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "rest/bed/delete/" & ds.PPKPELAYANAN

                    req = New WinHttp.WinHttpRequest
                    req.Open("POST", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "application/json")
                    req.Send(Request)
                    result = req.ResponseText

                End If

                RuanganHapus = result

            Catch ex As Exception
                RuanganHapus = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function KeterersediaanKamar(ByVal sNAME_DISPLAY As String, ByVal Start As Integer, ByVal Limit As Integer) As String
            Try
                If Not oConnection.GetConnection() Then
                    KeterersediaanKamar = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "rest/bed/read/" & ds.PPKPELAYANAN & "/" & Start & "/" & Limit & ""

                    req = New WinHttp.WinHttpRequest
                    req.Open("GET", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "application/json")

                    req.Send()

                    result = req.ResponseText

                End If

                KeterersediaanKamar = result

            Catch ex As Exception
                KeterersediaanKamar = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function Agents(ByVal sNAME_DISPLAY As String, ByVal Request As String, ByVal Start As Integer, ByVal Limit As Integer) As String
            Try
                If Not oConnection.GetConnection() Then
                    Agents = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim uTime As Integer = 0
                Dim HasilKey As String = ""
                Dim result As String = ""

                If ds IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    ' Initialize the keyed hash object using the secret key as the key
                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
                    ' Computes the signature by hashing the salt with the secret key as the key
                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
                    ' Base 64 Encode
                    HasilKey = Convert.ToBase64String(signature)

                    Dim req As WinHttp.WinHttpRequest

                    Dim Url As String = ds.ALAMATWEB & "rest/bed/read/" & ds.PPKPELAYANAN & "/" & Start & "/" & Limit

                    req = New WinHttp.WinHttpRequest
                    req.Open("GET", Url, False)
                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
                    req.SetRequestHeader("X-Timestamp", uTime)
                    req.SetRequestHeader("X-Signature", HasilKey)
                    req.SetRequestHeader("Content-Type", "application/json")
                    req.Send(Request)
                    result = req.ResponseText

                End If

                Agents = result

            Catch ex As Exception
                Agents = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
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
                            'dsPemetaan.TERSEDIA = IIf(isCalculate = False, dsPemetaan.TERSEDIA - 1, dsPemetaan.TERSEDIA + 1)

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

                            If dsPemetaan.ISAPLICARE = True Then
                                Dim jsonRequest As String = String.Empty

                                jsonRequest = "{ "
                                jsonRequest &= """kodekelas"": """ & dsPemetaan.KDKELASAPLICARE & ""","
                                jsonRequest &= """koderuang"": """ & dsPemetaan.KDDEPARTMENT & ""","
                                jsonRequest &= """namaruang"": """ & oConnection.db.M_DEPARTMENTs.FirstOrDefault(Function(x) x.KDDEPARTMENT = dsPemetaan.KDDEPARTMENT).NAME_DISPLAY & ""","
                                jsonRequest &= """kapasitas"": """ & dsPemetaan.KAPASITAS & ""","
                                jsonRequest &= """tersedia"": """ & dsPemetaan.TERSEDIA & ""","
                                jsonRequest &= """tersediapria"": """ & dsPemetaan.TERSEDIA_LAKI & ""","
                                jsonRequest &= """tersediawanita"": """ & dsPemetaan.TERSEDIA_PEREMPUAN & ""","
                                jsonRequest &= """tersediapriawanita"": """ & dsPemetaan.TERSEDIA_LAKIPEREMPUAN & """"
                                jsonRequest &= "} "

                                UpdateKetersediaanTempatTidur("APLICARE", jsonRequest)

                            End If
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
#End Region
#Region "E-Claim"
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
        Public Function fn_Membuatklaimbaru(ByVal sNAME_DISPLAY As String, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    fn_Membuatklaimbaru = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)
                Dim result As String = String.Empty

                If ds IsNot Nothing Then
                    Dim req As WinHttp.WinHttpRequest
                    Dim jsonEncode As String = String.Empty
                    Dim JsonEncrypt As String = String.Empty
                    Dim JsonDecrypt As String = String.Empty

                    jsonEncode = Request

                    JsonEncrypt = inacbg_encrypt(jsonEncode, ds.REMARKS)

                    req = New WinHttp.WinHttpRequest
                    req.Open("POST", ds.ALAMATWEB, False)
                    req.Send(JsonEncrypt)

                    If req.Status = "200" Then
                        result = req.ResponseText

                        Dim HasilResult As String

                        HasilResult = result.Replace("----BEGIN ENCRYPTED DATA----", "")

                        HasilResult = HasilResult.Replace("----END ENCRYPTED DATA----", "")

                        fn_Membuatklaimbaru = inacbg_decrypt(HasilResult, ds.REMARKS)
                    Else
                        fn_Membuatklaimbaru = ""
                    End If
                Else
                    fn_Membuatklaimbaru = ""
                End If

            Catch ex As Exception
                fn_Membuatklaimbaru = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function fn_UpdateDataPasien(ByVal sNAME_DISPLAY As String, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    fn_UpdateDataPasien = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)
                Dim result As String = String.Empty

                If ds IsNot Nothing Then
                    Dim req As WinHttp.WinHttpRequest
                    Dim jsonEncode As String = String.Empty
                    Dim JsonEncrypt As String = String.Empty
                    Dim JsonDecrypt As String = String.Empty

                    jsonEncode = Request

                    JsonEncrypt = inacbg_encrypt(jsonEncode, ds.REMARKS)

                    req = New WinHttp.WinHttpRequest
                    req.Open("POST", ds.ALAMATWEB, False)
                    req.Send(JsonEncrypt)

                    If req.Status = "200" Then
                        result = req.ResponseText

                        Dim HasilResult As String

                        HasilResult = result.Replace("----BEGIN ENCRYPTED DATA----", "")

                        HasilResult = HasilResult.Replace("----END ENCRYPTED DATA----", "")

                        fn_UpdateDataPasien = inacbg_decrypt(HasilResult, ds.REMARKS)
                    Else
                        fn_UpdateDataPasien = ""
                    End If
                Else
                    fn_UpdateDataPasien = ""
                End If

            Catch ex As Exception
                fn_UpdateDataPasien = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function fn_MengisiUpdateDataKlaim(ByVal sNAME_DISPLAY As String, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    fn_MengisiUpdateDataKlaim = ""
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)
                Dim result As String = String.Empty

                If ds IsNot Nothing Then
                    Dim req As WinHttp.WinHttpRequest
                    Dim jsonEncode As String = String.Empty
                    Dim JsonEncrypt As String = String.Empty
                    Dim JsonDecrypt As String = String.Empty

                    jsonEncode = Request

                    JsonEncrypt = inacbg_encrypt(jsonEncode, ds.REMARKS)

                    req = New WinHttp.WinHttpRequest
                    req.Open("POST", ds.ALAMATWEB, False)
                    req.Send(JsonEncrypt)

                    If req.Status = "200" Then
                        result = req.ResponseText

                        Dim HasilResult As String

                        HasilResult = result.Replace("----BEGIN ENCRYPTED DATA----", "")

                        HasilResult = HasilResult.Replace("----END ENCRYPTED DATA----", "")

                        fn_MengisiUpdateDataKlaim = inacbg_decrypt(HasilResult, ds.REMARKS)
                    Else
                        fn_MengisiUpdateDataKlaim = ""
                    End If
                Else
                    fn_MengisiUpdateDataKlaim = ""
                End If

            Catch ex As Exception
                fn_MengisiUpdateDataKlaim = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
#End Region
        Public Function InsertData(ByVal entity As SET_KONEKSI) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDKONEKSI
                sSTATUS = "INSERT"

                'Generate Auto Number
                Try
                    sLASTNUMBER = CInt(oConnection.db.SET_KONEKSIs.OrderByDescending(Function(x) x.KDKONEKSI).FirstOrDefault().KDKONEKSI.Remove(0, (sMODUL & " _ ").Length)) + 1
                Catch ex As Exception
                    sLASTNUMBER = 1
                End Try
                'End Generate

                Try
                    entity.KDKONEKSI = sMODUL & "_" & AutoNumberCode(sLASTNUMBER)
                    oConnection.db.SET_KONEKSIs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                InsertData = True
            Catch ex As Exception
                InsertData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As SET_KONEKSI) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDKONEKSI
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.KDKONEKSI = entity.KDKONEKSI)

                Try
                    oConnection.db.SET_KONEKSIs.DeleteOnSubmit(ds)
                    oConnection.db.SET_KONEKSIs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateData = True
            Catch ex As Exception
                UpdateData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function DeleteData(ByVal sKDKONEKSI As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDKONEKSI
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.KDKONEKSI = sKDKONEKSI)

                Try
                    oConnection.db.SET_KONEKSIs.DeleteOnSubmit(ds)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
    End Class
End Namespace