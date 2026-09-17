Imports System.Threading

Namespace SuratInfokes
    Public Class clsSuratKeteranganPerawatan
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0

        Public oCounter As Setting.clsCounter = Nothing

        Public sKDITEM As New List(Of String)

        Public Sub New()
            oConnection = New Setting.clsConnectionMain
            oError = New Setting.clsError
            sMODUL = "INFOKES_SKP"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_SURATKETERANGANPERAWATAN
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_SURATKETERANGANPERAWATAN
        End Function
        Public Function GetDataKunjungan(ByVal sKDKUNJUNGAN As String) As R_IDENTITAS_PASIEN
            If Not oConnection.GetConnection() Then
                GetDataKunjungan = Nothing
                Exit Function
            End If

            GetDataKunjungan = oConnection.db.R_IDENTITAS_PASIENs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = sKDKUNJUNGAN)
        End Function
        Public Function GetData(ByVal sKDSURATKETERANGANPERAWATAN As String) As S_DIGITAL_SURATKETERANGANPERAWATAN
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If

            GetData = oConnection.db.S_DIGITAL_SURATKETERANGANPERAWATANs.FirstOrDefault(Function(x) x.KDSURATKETERANGANPERAWATAN = sKDSURATKETERANGANPERAWATAN)
        End Function
        Public Function GetDataKunjunganPasien(ByVal sKDKUNJUNGAN As String) As S_DIGITAL_SURATKETERANGANPERAWATAN
            If Not oConnection.GetConnection() Then
                GetDataKunjunganPasien = Nothing
                Exit Function
            End If

            GetDataKunjunganPasien = oConnection.db.S_DIGITAL_SURATKETERANGANPERAWATANs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = sKDKUNJUNGAN)
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
        Public Function InsertData(ByVal entity As S_DIGITAL_SURATKETERANGANPERAWATAN) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = ""
                    Exit Function
                End If

                sREFERENCE = entity.KDSURATKETERANGANPERAWATAN
                sSTATUS = "UPDATE"

                Dim sLASTNUMBER_SURAT As String = ""
                Dim sMODUL_SURAT As String = "INFOKES_SKP_S"

                Try
                    sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                    If sLASTNUMBER = 0 Then
                        Try
                            oCounter.InsertData(sMODUL, entity.DATE)
                            sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                        Catch ex As Exception
                            sLASTNUMBER = 0
                        End Try
                    End If

                    entity.KDSURATKETERANGANPERAWATAN = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)

                    sLASTNUMBER_SURAT = oCounter.GetLastNumberTahun(sMODUL_SURAT, entity.DATE)

                    If sLASTNUMBER_SURAT = 0 Then
                        Try
                            oCounter.InsertData(sMODUL_SURAT, entity.DATE)
                            sLASTNUMBER_SURAT = oCounter.GetLastNumberTahun(sMODUL_SURAT, entity.DATE)
                        Catch ex As Exception
                            sLASTNUMBER_SURAT = 0
                        End Try
                    End If

                    entity.NOMORSURAT = "B / " & sLASTNUMBER_SURAT + 1 & " / " & IntegerToRoman(CInt(Month(entity.DATE))) & " / " & Year(entity.DATE)

                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.S_DIGITAL_SURATKETERANGANPERAWATANs.InsertOnSubmit(entity)
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
                    oCounter.UpdateDataYear(sMODUL_SURAT, sLASTNUMBER_SURAT + 1, Year(entity.DATE))
                Catch ex As Exception
                    oError.InsertData(sMODUL_SURAT, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try


                InsertData = entity.KDSURATKETERANGANPERAWATAN
            Catch ex As Exception
                InsertData = ""
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_DIGITAL_SURATKETERANGANPERAWATAN) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDSURATKETERANGANPERAWATAN
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_DIGITAL_SURATKETERANGANPERAWATANs.FirstOrDefault(Function(x) x.KDSURATKETERANGANPERAWATAN = entity.KDSURATKETERANGANPERAWATAN)

                Try
                    oConnection.db.S_DIGITAL_SURATKETERANGANPERAWATANs.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_SURATKETERANGANPERAWATANs.InsertOnSubmit(entity)
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
        Public Function UpdateMemo(ByVal sKDSURATKETERANGANPERAWATAN As String, ByVal sMemo As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateMemo = False
                    Exit Function
                End If

                Dim ds = oConnection.db.S_DIGITAL_SURATKETERANGANPERAWATANs.FirstOrDefault(Function(x) x.KDSURATKETERANGANPERAWATAN = sKDSURATKETERANGANPERAWATAN)

                If ds IsNot Nothing Then
                    ds.MEMO = sMemo
                    oConnection.db.SubmitChanges()
                End If

                UpdateMemo = True
            Catch ex As Exception
                UpdateMemo = False
                oError.InsertData("S_DIGITAL_BERITAMASUKPERAWATAN", "UPDATECETAK", ex.ToString, sKDSURATKETERANGANPERAWATAN)
                Throw ex
            End Try
        End Function
        Public Function UpdateMemo(ByVal sKDSURATKETERANGANPERAWATAN As String, ByVal sMemo As String, ByVal sKDUSER_KEPALA As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateMemo = False
                    Exit Function
                End If

                Dim ds = oConnection.db.S_DIGITAL_SURATKETERANGANPERAWATANs.FirstOrDefault(Function(x) x.KDSURATKETERANGANPERAWATAN = sKDSURATKETERANGANPERAWATAN)

                If ds IsNot Nothing Then
                    ds.MEMO = sMemo
                    ds.KDUSER_KEPALA = sKDUSER_KEPALA
                    oConnection.db.SubmitChanges()
                End If

                UpdateMemo = True
            Catch ex As Exception
                UpdateMemo = False
                oError.InsertData("S_DIGITAL_BERITAMASUKPERAWATAN", "UPDATECETAK", ex.ToString, sKDSURATKETERANGANPERAWATAN)
                Throw ex
            End Try
        End Function
    End Class
End Namespace