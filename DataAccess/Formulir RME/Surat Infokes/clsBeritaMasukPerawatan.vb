Imports System.Threading

Namespace SuratInfokes
    Public Class clsBeritaMasukPerawatan
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
            sMODUL = "INFOKES_BMP"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_BERITAMASUKPERAWATAN
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_BERITAMASUKPERAWATAN
        End Function
        Public Function GetDataKunjungan(ByVal sKDKUNJUNGAN As String) As R_IDENTITAS_PASIEN
            If Not oConnection.GetConnection() Then
                GetDataKunjungan = Nothing
                Exit Function
            End If

            GetDataKunjungan = oConnection.db.R_IDENTITAS_PASIENs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = sKDKUNJUNGAN)
        End Function
        Public Function GetData(ByVal sKDBERITAMASUKPERAWATAN As String) As S_DIGITAL_BERITAMASUKPERAWATAN
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If

            GetData = oConnection.db.S_DIGITAL_BERITAMASUKPERAWATANs.FirstOrDefault(Function(x) x.KDBERITAMASUKPERAWATAN = sKDBERITAMASUKPERAWATAN)
        End Function
        Public Function GetDataKunjunganPasien(ByVal sKDKUNJUNGAN As String) As S_DIGITAL_BERITAMASUKPERAWATAN
            If Not oConnection.GetConnection() Then
                GetDataKunjunganPasien = Nothing
                Exit Function
            End If

            GetDataKunjunganPasien = oConnection.db.S_DIGITAL_BERITAMASUKPERAWATANs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = sKDKUNJUNGAN)
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
        Public Function InsertData(ByVal entity As S_DIGITAL_BERITAMASUKPERAWATAN) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = ""
                    Exit Function
                End If

                sREFERENCE = entity.KDBERITAMASUKPERAWATAN
                sSTATUS = "UPDATE"

                Dim sLASTNUMBER_SURAT As String = ""
                Dim sMODUL_SURAT As String = "INFOKES_BMP_S"

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

                    entity.KDBERITAMASUKPERAWATAN = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)

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
                    oConnection.db.S_DIGITAL_BERITAMASUKPERAWATANs.InsertOnSubmit(entity)
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

                InsertData = entity.KDBERITAMASUKPERAWATAN
            Catch ex As Exception
                InsertData = ""
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_DIGITAL_BERITAMASUKPERAWATAN) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDBERITAMASUKPERAWATAN
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_DIGITAL_BERITAMASUKPERAWATANs.FirstOrDefault(Function(x) x.KDBERITAMASUKPERAWATAN = entity.KDBERITAMASUKPERAWATAN)

                Try
                    oConnection.db.S_DIGITAL_BERITAMASUKPERAWATANs.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_BERITAMASUKPERAWATANs.InsertOnSubmit(entity)
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
        Public Function UpdateMemo(ByVal sKDBERITAMASUKPERAWATAN As String, ByVal sMemo As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateMemo = False
                    Exit Function
                End If

                Dim ds = oConnection.db.S_DIGITAL_BERITAMASUKPERAWATANs.FirstOrDefault(Function(x) x.KDBERITAMASUKPERAWATAN = sKDBERITAMASUKPERAWATAN)

                If ds IsNot Nothing Then
                    ds.MEMO = sMemo
                    oConnection.db.SubmitChanges()
                End If

                UpdateMemo = True
            Catch ex As Exception
                UpdateMemo = False
                oError.InsertData("S_DIGITAL_BERITAMASUKPERAWATAN", "UPDATECETAK", ex.ToString, sKDBERITAMASUKPERAWATAN)
                Throw ex
            End Try
        End Function
        Public Function UpdateMemo(ByVal sKDBERITAMASUKPERAWATAN As String, ByVal sMemo As String, ByVal sKDUSER_KEPALA As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateMemo = False
                    Exit Function
                End If

                Dim ds = oConnection.db.S_DIGITAL_BERITAMASUKPERAWATANs.FirstOrDefault(Function(x) x.KDBERITAMASUKPERAWATAN = sKDBERITAMASUKPERAWATAN)

                If ds IsNot Nothing Then
                    ds.MEMO = sMemo
                    ds.KDUSER_KEPALA = sKDUSER_KEPALA
                    oConnection.db.SubmitChanges()
                End If

                UpdateMemo = True
            Catch ex As Exception
                UpdateMemo = False
                oError.InsertData("S_DIGITAL_BERITAMASUKPERAWATAN", "UPDATECETAK", ex.ToString, sKDBERITAMASUKPERAWATAN)
                Throw ex
            End Try
        End Function
    End Class
End Namespace