Imports DataAccess.My.Resources

Namespace Digital
    Public Class clsJawabKonsul
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

            sMODUL = "JAWABKONSUL"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_JAWABKONSUL
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_JAWABKONSUL
        End Function
        Public Function GetData(ByVal sKDJAWABKONSUL As String) As S_DIGITAL_JAWABKONSUL
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_JAWABKONSULs.FirstOrDefault(Function(x) x.KDJAWABKONSUL = sKDJAWABKONSUL)
        End Function
        Public Function GetDataList(ByVal sKDKONSUL As String) As List(Of S_DIGITAL_JAWABKONSUL)
            If Not oConnection.GetConnection() Then
                GetDataList = Nothing
                Exit Function
            End If
            GetDataList = oConnection.db.S_DIGITAL_JAWABKONSULs.Where(Function(x) x.KDKONSULTASI = sKDKONSUL).ToList()
        End Function
        Public Function GetDataKonsultasi(ByVal sKDKONSULTASI As String) As S_DIGITAL_KONSULTASI
            If Not oConnection.GetConnection() Then
                GetDataKonsultasi = Nothing
                Exit Function
            End If
            GetDataKonsultasi = oConnection.db.S_DIGITAL_KONSULTASIs.FirstOrDefault(Function(x) x.KDKONSULTASI = sKDKONSULTASI)
        End Function
        Public Function GetDataByKonsultasi(ByVal sKDKONSULTASI As String) As S_DIGITAL_JAWABKONSUL
            If Not oConnection.GetConnection() Then
                GetDataByKonsultasi = Nothing
                Exit Function
            End If
            GetDataByKonsultasi = oConnection.db.S_DIGITAL_JAWABKONSULs.FirstOrDefault(Function(x) x.KDKONSULTASI = sKDKONSULTASI)
        End Function

        Public Function GetDataCategory(ByVal sKDKUNJUNGAN As String) As R_IDENTITAS_PASIEN
            If Not oConnection.GetConnection() Then
                GetDataCategory = Nothing
                Exit Function
            End If
            GetDataCategory = oConnection.db.R_IDENTITAS_PASIENs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = sKDKUNJUNGAN)
        End Function
        Public Function GetDataKunjungan(ByVal sKDKUNJUNGAN As String) As R_IDENTITAS_PASIEN
            If Not oConnection.GetConnection() Then
                GetDataKunjungan = Nothing
                Exit Function
            End If
            GetDataKunjungan = oConnection.db.R_IDENTITAS_PASIENs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = sKDKUNJUNGAN)
        End Function
        Public Function GetDataPendaftaranList(ByVal sKDPENDAFTARAN As String) As List(Of S_DIGITAL_JAWABKONSUL)
            If Not oConnection.GetConnection() Then
                GetDataPendaftaranList = Nothing
                Exit Function
            End If
            GetDataPendaftaranList = oConnection.db.S_DIGITAL_JAWABKONSULs.Where(Function(x) x.R_IDENTITAS_PASIEN.KDPENDAFTARAN = sKDPENDAFTARAN).ToList()
        End Function
        Public Function GetDataPendaftaranList(ByVal sKDPENDAFTARAN As String, ByVal sDATE As String) As List(Of S_DIGITAL_JAWABKONSUL)
            If Not oConnection.GetConnection() Then
                GetDataPendaftaranList = Nothing
                Exit Function
            End If
            GetDataPendaftaranList = oConnection.db.S_DIGITAL_JAWABKONSULs.Where(Function(x) x.R_IDENTITAS_PASIEN.KDPENDAFTARAN = sKDPENDAFTARAN And x.DATE.Year = Year(sDATE) And x.DATE.Month = Month(sDATE) And x.DATE.Day = Day(sDATE)).ToList()
        End Function
        Public Function GetDataPendaftaranKunjunganList(ByVal sKDKUNJUNGAN As String) As List(Of S_DIGITAL_JAWABKONSUL)
            If Not oConnection.GetConnection() Then
                GetDataPendaftaranKunjunganList = Nothing
                Exit Function
            End If
            GetDataPendaftaranKunjunganList = oConnection.db.S_DIGITAL_JAWABKONSULs.Where(Function(x) x.KDKUNJUNGAN = sKDKUNJUNGAN).ToList()
        End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_JAWABKONSUL) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDJAWABKONSUL
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

                entity.KDJAWABKONSUL = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)

                Try
                    oConnection.db.S_DIGITAL_JAWABKONSULs.InsertOnSubmit(entity)
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

                UpdateIschek(entity.KDKONSULTASI)

                InsertData = True
            Catch ex As Exception
                InsertData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_DIGITAL_JAWABKONSUL) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDJAWABKONSUL
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_DIGITAL_JAWABKONSULs.FirstOrDefault(Function(x) x.KDJAWABKONSUL = entity.KDJAWABKONSUL)

                Try
                    oConnection.db.S_DIGITAL_JAWABKONSULs.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_JAWABKONSULs.InsertOnSubmit(entity)
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

                UpdateIschek(entity.KDKONSULTASI)
            Catch ex As Exception
                UpdateData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateIschek(ByVal sKDKONSULTASI As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateIschek = False
                    Exit Function
                End If

                UpdateIschek = True

                Dim ds = oConnection.db.S_DIGITAL_KONSULTASIs.FirstOrDefault(Function(x) x.KDKONSULTASI = sKDKONSULTASI)

                ds.ISCHEKED = True

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdateIschek = False
                Throw ex
            End Try
        End Function
        Public Function UpdateIschekDelete(ByVal sKDKONSULTASI As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateIschekDelete = False
                    Exit Function
                End If

                UpdateIschekDelete = True

                Dim ds = oConnection.db.S_DIGITAL_KONSULTASIs.FirstOrDefault(Function(x) x.KDKONSULTASI = sKDKONSULTASI)

                ds.ISCHEKED = False

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdateIschekDelete = False
                Throw ex
            End Try
        End Function
        Public Function DeleteData(ByVal sKDJAWABKONSUL As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDJAWABKONSUL
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.S_DIGITAL_JAWABKONSULs.FirstOrDefault(Function(x) x.KDJAWABKONSUL = sKDJAWABKONSUL)

                Try
                    oConnection.db.S_DIGITAL_JAWABKONSULs.DeleteOnSubmit(ds)

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
                UpdateIschekDelete(ds.KDKONSULTASI)
            Catch ex As Exception
                DeleteData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
    End Class
End Namespace