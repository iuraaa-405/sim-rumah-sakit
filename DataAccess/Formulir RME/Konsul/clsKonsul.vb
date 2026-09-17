Imports DataAccess.My.Resources

Namespace Digital
    Public Class clsKonsul
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

            sMODUL = "KONSUL"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_KONSULTASI
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_KONSULTASI
        End Function
        Public Function GetData(ByVal sKDKONSULTASI As String) As S_DIGITAL_KONSULTASI
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_KONSULTASIs.FirstOrDefault(Function(x) x.KDKONSULTASI = sKDKONSULTASI)
        End Function
        Public Function GetDataList(ByVal sKDKONSULTASI As String) As List(Of S_DIGITAL_KONSULTASI)
            If Not oConnection.GetConnection() Then
                GetDataList = Nothing
                Exit Function
            End If
            GetDataList = oConnection.db.S_DIGITAL_KONSULTASIs.Where(Function(x) x.KDKONSULTASI = sKDKONSULTASI).ToList()
        End Function
        Public Function GetDataDokterKepadaRubberAlihLeader(ByVal sKDKUNJUNGAN As String, ByVal sKDDOCTOR As String) As S_DIGITAL_KONSULTASI
            If Not oConnection.GetConnection() Then
                GetDataDokterKepadaRubberAlihLeader = Nothing
                Exit Function
            End If
            GetDataDokterKepadaRubberAlihLeader = oConnection.db.S_DIGITAL_KONSULTASIs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = sKDKUNJUNGAN And x.KDDOKTER_KEPADA = sKDDOCTOR And x.ISRUBBER = True Or x.KDKUNJUNGAN = sKDKUNJUNGAN And x.KDDOKTER_KEPADA = sKDDOCTOR And x.ISALIHLEADER = True)
        End Function
        Public Function GetDataByKDPENDAFTARAN(ByVal sKDPENDAFTARAN As String) As S_DIGITAL_KONSULTASI
            If Not oConnection.GetConnection() Then
                GetDataByKDPENDAFTARAN = Nothing
                Exit Function
            End If
            GetDataByKDPENDAFTARAN = oConnection.db.S_DIGITAL_KONSULTASIs.FirstOrDefault(Function(x) x.R_IDENTITAS_PASIEN.KDPENDAFTARAN = sKDPENDAFTARAN)
        End Function
        Public Function GetDataKunjungan(ByVal sKDKUNJUNGAN As String) As R_IDENTITAS_PASIEN
            If Not oConnection.GetConnection() Then
                GetDataKunjungan = Nothing
                Exit Function
            End If
            GetDataKunjungan = oConnection.db.R_IDENTITAS_PASIENs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = sKDKUNJUNGAN)
        End Function
        Public Function GetDataKonsulRubberList(ByVal sKDPENDAFTARAN As String) As List(Of S_DIGITAL_KONSULTASI)
            If Not oConnection.GetConnection() Then
                GetDataKonsulRubberList = Nothing
                Exit Function
            End If
            GetDataKonsulRubberList = oConnection.db.S_DIGITAL_KONSULTASIs.Where(Function(x) x.R_IDENTITAS_PASIEN.KDPENDAFTARAN = sKDPENDAFTARAN And (x.ISRUBBER = True Or x.ISSEWAKTU = True Or x.ISALIHLEADER = True)).ToList()
        End Function
        Public Function GetDataPendaftaranList(ByVal sKDPENDAFTARAN As String) As List(Of S_DIGITAL_KONSULTASI)
            If Not oConnection.GetConnection() Then
                GetDataPendaftaranList = Nothing
                Exit Function
            End If
            GetDataPendaftaranList = oConnection.db.S_DIGITAL_KONSULTASIs.Where(Function(x) x.R_IDENTITAS_PASIEN.KDPENDAFTARAN = sKDPENDAFTARAN).ToList()
        End Function
        Public Function GetDataRabberAwal(ByVal sKDPENDAFTARAN As String, ByVal sDPJP As String) As List(Of S_DIGITAL_KONSULTASI)
            If Not oConnection.GetConnection() Then
                GetDataRabberAwal = Nothing
                Exit Function
            End If
            GetDataRabberAwal = oConnection.db.S_DIGITAL_KONSULTASIs.Where(Function(x) x.R_IDENTITAS_PASIEN.KDPENDAFTARAN = sKDPENDAFTARAN And x.KDDOKTER_KEPADA <> sDPJP).ToList()
        End Function
        Public Function GetDataPendaftaranList(ByVal sKDPENDAFTARAN As String, ByVal sDATE As DateTime) As List(Of S_DIGITAL_KONSULTASI)
            If Not oConnection.GetConnection() Then
                GetDataPendaftaranList = Nothing
                Exit Function
            End If
            GetDataPendaftaranList = oConnection.db.S_DIGITAL_KONSULTASIs.Where(Function(x) x.R_IDENTITAS_PASIEN.KDPENDAFTARAN = sKDPENDAFTARAN And x.DATE.Year = Year(sDATE) And x.DATE.Month = Month(sDATE) And x.DATE.Day = Day(sDATE)).ToList()
        End Function
        Public Function GetDataPendaftaranKunjunganList(ByVal sKDKUNJUNGAN As String) As List(Of S_DIGITAL_KONSULTASI)
            If Not oConnection.GetConnection() Then
                GetDataPendaftaranKunjunganList = Nothing
                Exit Function
            End If
            GetDataPendaftaranKunjunganList = oConnection.db.S_DIGITAL_KONSULTASIs.Where(Function(x) x.KDKUNJUNGAN = sKDKUNJUNGAN).ToList()
        End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_KONSULTASI, ByVal sKDKUNJUNGAN_POLI As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = ""
                    Exit Function
                End If

                sREFERENCE = entity.KDKONSULTASI
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

                entity.KDKUNJUNGAN_POLI = sKDKUNJUNGAN_POLI
                entity.KDKONSULTASI = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)

                Try
                    oConnection.db.S_DIGITAL_KONSULTASIs.InsertOnSubmit(entity)
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
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                InsertData = entity.KDKONSULTASI
            Catch ex As Exception
                InsertData = ""
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_DIGITAL_KONSULTASI) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDKONSULTASI
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_DIGITAL_KONSULTASIs.FirstOrDefault(Function(x) x.KDKONSULTASI = entity.KDKONSULTASI)

                Try
                    oConnection.db.S_DIGITAL_KONSULTASIs.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_KONSULTASIs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal sKDKONSULTASI As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDKONSULTASI
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.S_DIGITAL_KONSULTASIs.FirstOrDefault(Function(x) x.KDKONSULTASI = sKDKONSULTASI)

                Try
                    oConnection.db.S_DIGITAL_KONSULTASIs.DeleteOnSubmit(ds)

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
        Public Function RawatBersama(ByVal Parameter1 As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    RawatBersama = False
                    Exit Function
                End If

                RawatBersama = True

                Dim ds = oConnection.db.S_DIGITAL_KONSULTASIs.FirstOrDefault(Function(x) x.KDKONSULTASI = Parameter1)

                ds.ISRUBBER = True

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                RawatBersama = False
                Throw ex
            End Try
        End Function
        Public Function RawataliLeader(ByVal Parameter1 As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    RawataliLeader = False
                    Exit Function
                End If

                RawataliLeader = True

                Dim ds = oConnection.db.S_DIGITAL_KONSULTASIs.FirstOrDefault(Function(x) x.KDKONSULTASI = Parameter1)

                ds.ISALIHLEADER = True

                oConnection.db.SubmitChanges()
            Catch ex As Exception
                RawataliLeader = False
                Throw ex
            End Try
        End Function
    End Class
End Namespace