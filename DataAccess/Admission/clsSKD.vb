Imports System.Threading

Namespace Admission
    Public Class clsSKD
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0

        Public oCounter As Setting.clsCounter = Nothing

        Public Sub New()
            oConnection = New Setting.clsConnectionMain
            oError = New Setting.clsError
            sMODUL = "SKD"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_PENDAFTARAN_SKD
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_PENDAFTARAN_SKD
        End Function
        Public Function GetData() As List(Of S_PENDAFTARAN_SKD)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_PENDAFTARAN_SKDs.OrderByDescending(Function(x) x.KDSKD).ToList()
        End Function
        Public Function GetDataByKDREG(ByVal sKDPENDAFTARAN As String) As List(Of S_PENDAFTARAN_SKD)
            If Not oConnection.GetConnection() Then
                GetDataByKDREG = Nothing
                Exit Function
            End If
            GetDataByKDREG = oConnection.db.S_PENDAFTARAN_SKDs.Where(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN).OrderByDescending(Function(x) x.KDSKD).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As S_PENDAFTARAN_SKD
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_PENDAFTARAN_SKDs.FirstOrDefault(Function(x) x.KDSKD = Parameter And x.ISONLINE = True)
        End Function
        Public Function GetDataKonsul(ByVal Parameter As String) As S_PENDAFTARAN_SKD
            If Not oConnection.GetConnection() Then
                GetDataKonsul = Nothing
                Exit Function
            End If
            GetDataKonsul = oConnection.db.S_PENDAFTARAN_SKDs.FirstOrDefault(Function(x) x.KDSKD = Parameter And x.ISONLINE = True And x.ALASAN = "KONTROL")
        End Function
        Public Function GetDataNotOnline(ByVal Parameter As String) As S_PENDAFTARAN_SKD
            If Not oConnection.GetConnection() Then
                GetDataNotOnline = Nothing
                Exit Function
            End If
            GetDataNotOnline = oConnection.db.S_PENDAFTARAN_SKDs.FirstOrDefault(Function(x) x.KDSKD = Parameter)
        End Function
        Public Function GetDataTindakLanjut(ByVal Parameter1 As String, ByVal Parameter2 As String) As S_PENDAFTARAN_SKD
            If Not oConnection.GetConnection() Then
                GetDataTindakLanjut = Nothing
                Exit Function
            End If
            GetDataTindakLanjut = oConnection.db.S_PENDAFTARAN_SKDs.FirstOrDefault(Function(x) x.KDSKD = Parameter1 And x.TINDAKLANJUT = Parameter2)
        End Function
        Public Function GetDataOfline(ByVal Parameter As String) As S_PENDAFTARAN_SKD
            If Not oConnection.GetConnection() Then
                GetDataOfline = Nothing
                Exit Function
            End If
            GetDataOfline = oConnection.db.S_PENDAFTARAN_SKDs.FirstOrDefault(Function(x) x.KDSKD = Parameter)
        End Function
        Public Function GetDataPendaftaran(ByVal Parameter As String) As S_PENDAFTARAN_SKD
            If Not oConnection.GetConnection() Then
                GetDataPendaftaran = Nothing
                Exit Function
            End If
            GetDataPendaftaran = oConnection.db.S_PENDAFTARAN_SKDs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter)
        End Function
        Public Function GetDataByDate(ByVal Parameter As String) As S_PENDAFTARAN_SKD
            If Not oConnection.GetConnection() Then
                GetDataByDate = Nothing
                Exit Function
            End If
            GetDataByDate = oConnection.db.S_PENDAFTARAN_SKDs.FirstOrDefault(Function(x) x.S_PENDAFTARAN_H.KDCUSTOMER = Parameter And x.TANGGALPERIKSA_TEXT = Now.ToString("ddMMyyyy"))
        End Function
        Public Function GetDataPendaftaranByKoderegistrasi(ByVal Parameter As String) As S_PENDAFTARAN_H
            If Not oConnection.GetConnection() Then
                GetDataPendaftaranByKoderegistrasi = Nothing
                Exit Function
            End If
            GetDataPendaftaranByKoderegistrasi = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter)
        End Function
        Public Function GetDataByRM(ByVal Parameter As String) As S_PENDAFTARAN_SKD
            If Not oConnection.GetConnection() Then
                GetDataByRM = Nothing
                Exit Function
            End If
            'GetDataByRM = oConnection.db.S_PENDAFTARAN_SKDs.OrderByDescending(Function(x) x.DATE).FirstOrDefault(Function(x) x.S_PENDAFTARAN_H.KDCUSTOMER = Parameter And x.ISCATEGORY = sCategory)
            GetDataByRM = oConnection.db.S_PENDAFTARAN_SKDs.FirstOrDefault(Function(x) x.S_PENDAFTARAN_H.KDCUSTOMER = Parameter And x.ALASAN = "KONTROL")
        End Function
        'Public Function GetDataByKartuBPJSTanggal(ByVal sNOMORKARTU As String, ByVal sDATE As DateTime) As S_PENDAFTARAN_SKD
        '    If Not oConnection.GetConnection() Then
        '        GetDataByKartuBPJSTanggal = Nothing
        '        Exit Function
        '    End If
        '    GetDataByKartuBPJSTanggal = oConnection.db.S_PENDAFTARAN_SKDs.FirstOrDefault(Function(x) x.S_PENDAFTARAN_H.KARTUBPJS = sNOMORKARTU And x.TANGGALPERIKSA_TEXT = sDATE.ToString("ddMMyyyy"))
        'End Function
        'Public Function GetDataByRMTanggal(ByVal sNORM As String, ByVal sDATE As DateTime) As S_PENDAFTARAN_SKD
        '    If Not oConnection.GetConnection() Then
        '        GetDataByRMTanggal = Nothing
        '        Exit Function
        '    End If
        '    GetDataByRMTanggal = oConnection.db.S_PENDAFTARAN_SKDs.FirstOrDefault(Function(x) x.S_PENDAFTARAN_H.KDCUSTOMER = sNORM And x.TANGGALPERIKSA_TEXT = sDATE.ToString("ddMMyyyy"))
        'End Function
        Public Function GetDataByRMTanggalKonsul(ByVal sNORM As String, ByVal sDATE As DateTime) As S_PENDAFTARAN_SKD
            If Not oConnection.GetConnection() Then
                GetDataByRMTanggalKonsul = Nothing
                Exit Function
            End If
            GetDataByRMTanggalKonsul = oConnection.db.S_PENDAFTARAN_SKDs.FirstOrDefault(Function(x) x.S_PENDAFTARAN_H.KDCUSTOMER = sNORM And x.TANGGALPERIKSA_TEXT = sDATE.ToString("ddMMyyyy") And x.ALASAN = "KONTROL")
        End Function
        Public Function GetDataByKartuBPJSTanggalKonsul(ByVal sNOMORKARTU As String, ByVal sDATE As DateTime) As S_PENDAFTARAN_SKD
            If Not oConnection.GetConnection() Then
                GetDataByKartuBPJSTanggalKonsul = Nothing
                Exit Function
            End If
            GetDataByKartuBPJSTanggalKonsul = oConnection.db.S_PENDAFTARAN_SKDs.FirstOrDefault(Function(x) x.S_PENDAFTARAN_H.KARTUBPJS = sNOMORKARTU And x.TANGGALPERIKSA_TEXT = sDATE.ToString("ddMMyyyy") And x.ALASAN = "KONTROL")
        End Function
        Public Function InsertData(ByVal entity As S_PENDAFTARAN_SKD, ByVal sKDSKD As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = ""
                    Exit Function
                End If

                sREFERENCE = entity.KDSKD
                sSTATUS = "INSERT"

                If entity.ISCATEGORY = 0 Then
                    sMODUL = "SKD-RJ"
                Else
                    sMODUL = "SKD-RI"
                End If

                If sKDSKD = "" Then
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

                        entity.KDSKD = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)
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
                End If

                Try
                    oConnection.db.S_PENDAFTARAN_SKDs.InsertOnSubmit(entity)
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

                InsertData = entity.KDSKD
            Catch ex As Exception
                InsertData = ""
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_PENDAFTARAN_SKD) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDSKD
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_PENDAFTARAN_SKDs.FirstOrDefault(Function(x) x.KDSKD = entity.KDSKD)

                Try
                    oConnection.db.S_PENDAFTARAN_SKDs.DeleteOnSubmit(ds)
                    oConnection.db.S_PENDAFTARAN_SKDs.InsertOnSubmit(entity)
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
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = Parameter
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.S_PENDAFTARAN_SKDs.FirstOrDefault(Function(x) x.KDSKD.Contains(Parameter))

                If ds IsNot Nothing Then
                    Try
                        oConnection.db.S_PENDAFTARAN_SKDs.DeleteOnSubmit(ds)
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try

                End If

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
        Public Function UpdateDataFix(ByVal kdskd As String, ByVal isCek As Boolean) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateDataFix = False
                    Exit Function
                End If

                UpdateDataFix = True

                Dim ds = oConnection.db.S_PENDAFTARAN_SKDs.FirstOrDefault(Function(x) x.KDSKD = kdskd)

                ds.ISCHEKED = isCek

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdateDataFix = False
                Throw ex
            End Try
        End Function
        'Public Function UpdateDataRequsetResponse(ByVal sKDSKD As String, ByVal sRequset As String, ByVal sReponse As String) As Boolean
        '    Try
        '        If Not oConnection.GetConnection Then
        '            UpdateDataRequsetResponse = False
        '            Exit Function
        '        End If

        '        UpdateDataRequsetResponse = True

        '        Dim ds = oConnection.db.S_PENDAFTARAN_SKDs.FirstOrDefault(Function(x) x.KDSKD = sKDSKD)

        '        ds.REQUEST = sRequset
        '        ds.RESPONSE = sReponse

        '        oConnection.db.SubmitChanges()

        '    Catch ex As Exception
        '        UpdateDataRequsetResponse = False
        '        Throw ex
        '    End Try
        'End Function
    End Class
End Namespace