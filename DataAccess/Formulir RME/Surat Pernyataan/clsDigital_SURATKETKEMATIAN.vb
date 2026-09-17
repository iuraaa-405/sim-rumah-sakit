Imports DataAccess.My.Resources

Namespace Digital
    Public Class clsDigital_SURATKETKEMATIAN
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing

        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0
        Public oCounter As Setting.clsCounter = Nothing

        Public Sub New(Optional ByVal sConnection As String = "")
            If sConnection = "" Then
                oConnection = New Setting.clsConnectionMain
                oError = New Setting.clsError
                oCounter = New Setting.clsCounter
            Else
                oConnection = New Setting.clsConnectionMain("TAX")
                oError = New Setting.clsError("TAX")
                oCounter = New Setting.clsCounter
            End If

            sMODUL = "KETERANGANKEMATIAN"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_KETERANGANKEMATIAN
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_KETERANGANKEMATIAN
        End Function
        Public Function GetData(ByVal sKDKUNJUNGAN As String) As S_DIGITAL_KETERANGANKEMATIAN
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_KETERANGANKEMATIANs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = sKDKUNJUNGAN)
        End Function
        Public Function GetDataByKunjungan(ByVal sKDKUNJUNGAN As String) As R_IDENTITAS_PASIEN
            If Not oConnection.GetConnection() Then
                GetDataByKunjungan = Nothing
                Exit Function
            End If
            GetDataByKunjungan = oConnection.db.R_IDENTITAS_PASIENs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = sKDKUNJUNGAN)
        End Function
        Public Function GetDataDetailByPendaftaranList(ByVal sParameter As String) As List(Of S_DIGITAL_KETERANGANKEMATIAN)
            If Not oConnection.GetConnection() Then
                GetDataDetailByPendaftaranList = Nothing
                Exit Function
            End If
            GetDataDetailByPendaftaranList = oConnection.db.S_DIGITAL_KETERANGANKEMATIANs.Where(Function(x) x.R_IDENTITAS_PASIEN.KDPENDAFTARAN = sParameter).ToList()
        End Function
        Public Function GetDatabyKDREG(ByVal sParameter As String) As List(Of S_DIGITAL_KETERANGANKEMATIAN)
            If Not oConnection.GetConnection Then
                GetDatabyKDREG = Nothing
                Exit Function
            End If
            GetDatabyKDREG = oConnection.db.S_DIGITAL_KETERANGANKEMATIANs.Where(Function(x) x.R_IDENTITAS_PASIEN.KDPENDAFTARAN = sParameter).OrderBy(Function(x) x.DATE).ToList()
        End Function
        Private Function IntegerToRoman(IntNumberValue As Integer) As String
            Dim RomanNumbers As New Dictionary(Of String, Integer)()
            RomanNumbers.Add("M", 1000)
            RomanNumbers.Add("CM", 900)
            RomanNumbers.Add("D", 500)
            RomanNumbers.Add("CD", 400)
            RomanNumbers.Add("C", 100)
            RomanNumbers.Add("XC", 90)
            RomanNumbers.Add("L", 50)
            RomanNumbers.Add("XL", 40)
            RomanNumbers.Add("X", 10)
            RomanNumbers.Add("IX", 9)
            RomanNumbers.Add("V", 5)
            RomanNumbers.Add("IV", 4)
            RomanNumbers.Add("I", 1)

            Dim result As String = ""

            For Each pair As KeyValuePair(Of String, Integer) In RomanNumbers
                While IntNumberValue >= pair.Value
                    IntNumberValue -= pair.Value
                    result += pair.Key
                End While
            Next
            Return result
        End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_KETERANGANKEMATIAN) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDKUNJUNGAN
                sSTATUS = "INSERT"

                If entity.NOMOR = "" Then
                    sLASTNUMBER = oCounter.GetLastNumberTahun(sMODUL, entity.DATE)

                    If sLASTNUMBER = 0 Then
                        Try
                            oCounter.InsertData(sMODUL, entity.DATE)
                            sLASTNUMBER = oCounter.GetLastNumberTahun(sMODUL, entity.DATE)
                        Catch ex As Exception
                            sLASTNUMBER = 0
                        End Try
                    End If

                    entity.NOMOR = sLASTNUMBER + 1 & " / " & IntegerToRoman(CInt(Month(entity.DATE))) & " / " & Year(entity.DATE)

                End If


                Try
                    oConnection.db.S_DIGITAL_KETERANGANKEMATIANs.InsertOnSubmit(entity)
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

                If entity.NOMOR = "" Then
                    Try
                        oCounter.UpdateDataYear(sMODUL, sLASTNUMBER + 1, Year(entity.DATE))
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try
                End If

                InsertData = True
            Catch ex As Exception
                InsertData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_DIGITAL_KETERANGANKEMATIAN) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDKUNJUNGAN
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_DIGITAL_KETERANGANKEMATIANs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = entity.KDKUNJUNGAN)

                Try
                    oConnection.db.S_DIGITAL_KETERANGANKEMATIANs.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_KETERANGANKEMATIANs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal Parameter As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    DeleteData = False
                    Exit Function
                End If
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.S_DIGITAL_KETERANGANKEMATIANs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = Parameter)

                Try
                    oConnection.db.S_DIGITAL_KETERANGANKEMATIANs.DeleteOnSubmit(ds)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, Parameter)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, Parameter)
                    Throw ex
                End Try

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, Parameter)
                Throw ex
            End Try
        End Function
    End Class
End Namespace