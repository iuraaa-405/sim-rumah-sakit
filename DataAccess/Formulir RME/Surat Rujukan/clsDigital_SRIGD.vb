Imports DataAccess.My.Resources

Namespace Digital
    Public Class clsDigital_SRIGD
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

            sMODUL = "SRIGD"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_SURATRUJUKAN_IGD
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_SURATRUJUKAN_IGD
        End Function
        Public Function GetData(ByVal sKODE As String) As S_DIGITAL_SURATRUJUKAN_IGD
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_SURATRUJUKAN_IGDs.FirstOrDefault(Function(x) x.KDSR_IGD = sKODE)
        End Function
        Public Function GetDataKunjungan(ByVal sKDKUNJUNGAN As String) As S_DIGITAL_SURATRUJUKAN_IGD
            If Not oConnection.GetConnection() Then
                GetDataKunjungan = Nothing
                Exit Function
            End If
            GetDataKunjungan = oConnection.db.S_DIGITAL_SURATRUJUKAN_IGDs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = sKDKUNJUNGAN)
        End Function
        Public Function GetDatabyKDREG(ByVal sParameter As String) As List(Of S_DIGITAL_SURATRUJUKAN_IGD)
            If Not oConnection.GetConnection Then
                GetDatabyKDREG = Nothing
                Exit Function
            End If
            GetDatabyKDREG = oConnection.db.S_DIGITAL_SURATRUJUKAN_IGDs.Where(Function(x) x.R_IDENTITAS_PASIEN.KDPENDAFTARAN = sParameter).OrderBy(Function(x) x.DATECREATED).ToList()
        End Function
        Public Function GetDataByKunjungan(ByVal sKDKUNJUNGAN As String) As R_IDENTITAS_PASIEN
            If Not oConnection.GetConnection() Then
                GetDataByKunjungan = Nothing
                Exit Function
            End If
            GetDataByKunjungan = oConnection.db.R_IDENTITAS_PASIENs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = sKDKUNJUNGAN)
        End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_SURATRUJUKAN_IGD) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDKUNJUNGAN
                sSTATUS = "INSERT"

                sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)

                If sLASTNUMBER = 0 Then
                    Try
                        oCounter.InsertData(sMODUL, entity.DATE)
                        sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                    Catch ex As Exception
                        sLASTNUMBER = 0
                    End Try
                End If

                entity.KDSR_IGD = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)

                Dim sMODULNOMOR As String = "RUJUKAN_IGD"
                Dim sLASTNUMBERNOMOR As Integer = 0

                sLASTNUMBERNOMOR = oCounter.GetLastNumber(sMODULNOMOR, entity.DATE)

                If sLASTNUMBERNOMOR = 0 Then
                    Try
                        oCounter.InsertData(sMODULNOMOR, entity.DATE)
                        sLASTNUMBERNOMOR = oCounter.GetLastNumber(sMODULNOMOR, entity.DATE)
                    Catch ex As Exception
                        sLASTNUMBERNOMOR = 0
                    End Try
                End If

                entity.NOMOR = sLASTNUMBERNOMOR + 1 & " / 1001 / " & IntegerToRoman(CInt(Month(entity.DATE))) & " / " & Year(entity.DATE)


                Try
                    oConnection.db.S_DIGITAL_SURATRUJUKAN_IGDs.InsertOnSubmit(entity)
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

                Try
                    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATE), Year(entity.DATE))
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oCounter.UpdateData(sMODULNOMOR, sLASTNUMBERNOMOR + 1, Month(entity.DATE), Year(entity.DATE))
                Catch ex As Exception
                    oError.InsertData(sMODULNOMOR, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                InsertData = True
            Catch ex As Exception
                InsertData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_DIGITAL_SURATRUJUKAN_IGD) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDKUNJUNGAN
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_DIGITAL_SURATRUJUKAN_IGDs.FirstOrDefault(Function(x) x.KDSR_IGD = entity.KDSR_IGD)

                Try
                    oConnection.db.S_DIGITAL_SURATRUJUKAN_IGDs.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_SURATRUJUKAN_IGDs.InsertOnSubmit(entity)
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

                Dim ds = oConnection.db.S_DIGITAL_SURATRUJUKAN_IGDs.FirstOrDefault(Function(x) x.KDSR_IGD = Parameter)

                Try
                    oConnection.db.S_DIGITAL_SURATRUJUKAN_IGDs.DeleteOnSubmit(ds)
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
        Public Function IntegerToRoman(IntNumberValue As Integer) As String
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
    End Class
End Namespace