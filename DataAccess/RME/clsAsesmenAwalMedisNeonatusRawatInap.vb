Imports DataAccess.My.Resources

Namespace Digital
    Public Class clsAsesmenAwalMedisNeonatusRawatInap
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

            sMODUL = "NEONATUS"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAP
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAP
        End Function
        Public Function GetStructureDetail() As S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAP_DIAGNOSAAKHIR
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAP_DIAGNOSAAKHIR
        End Function
        Public Function GetStructureDetail_() As S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAP_DIAGNOSASEMENTARA
            If Not oConnection.GetConnection() Then
                GetStructureDetail_ = Nothing
            End If
            GetStructureDetail_ = New S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAP_DIAGNOSASEMENTARA
        End Function
        Public Function GetStructureDetailList() As List(Of S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAP_DIAGNOSAAKHIR)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAP_DIAGNOSAAKHIR)
        End Function
        Public Function GetStructureDetailList_() As List(Of S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAP_DIAGNOSASEMENTARA)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList_ = Nothing
            End If
            GetStructureDetailList_ = New List(Of S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAP_DIAGNOSASEMENTARA)
        End Function
        Public Function GetData(ByVal sKDASESMENNEONATUS As String) As S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAP
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAPs.FirstOrDefault(Function(x) x.KDASESMENNEONATUS = sKDASESMENNEONATUS)
        End Function
        Public Function GetDataByKunjungan(ByVal sKDKUNJUNGAN As String) As S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAP
            If Not oConnection.GetConnection() Then
                GetDataByKunjungan = Nothing
                Exit Function
            End If
            GetDataByKunjungan = oConnection.dbRME.S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAPs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = sKDKUNJUNGAN)
        End Function
        Public Function GetDataDetail(ByVal sKDASESMENNEONATUS As String) As List(Of S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAP_DIAGNOSAAKHIR)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.dbRME.S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAP_DIAGNOSAAKHIRs.Where(Function(x) x.KDASESMENNEONATUS = sKDASESMENNEONATUS).ToList()
        End Function
        Public Function GetDataDetail_(ByVal sKDASESMENNEONATUS As String) As List(Of S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAP_DIAGNOSASEMENTARA)
            If Not oConnection.GetConnection() Then
                GetDataDetail_ = Nothing
                Exit Function
            End If
            GetDataDetail_ = oConnection.dbRME.S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAP_DIAGNOSASEMENTARAs.Where(Function(x) x.KDASESMENNEONATUS = sKDASESMENNEONATUS).ToList()
        End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAP, ByVal entityDetail As List(Of S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAP_DIAGNOSAAKHIR), ByVal entityDetail_ As List(Of S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAP_DIAGNOSASEMENTARA)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDKUNJUNGAN
                sSTATUS = "INSERT"

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

                    entity.KDASESMENNEONATUS = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)
                    For Each iLoop In entityDetail
                        iLoop.KDASESMENNEONATUS = entity.KDASESMENNEONATUS
                    Next
                    For Each iLoop In entityDetail_
                        iLoop.KDASESMENNEONATUS = entity.KDASESMENNEONATUS
                    Next
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.dbRME.S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAPs.InsertOnSubmit(entity)

                    If entityDetail.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAP_DIAGNOSAAKHIRs.InsertAllOnSubmit(entityDetail)
                    End If

                    If entityDetail_.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAP_DIAGNOSASEMENTARAs.InsertAllOnSubmit(entityDetail_)
                    End If

                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.dbRME.SubmitChanges()
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

                InsertData = True
            Catch ex As Exception
                InsertData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAP, ByVal entityDetail As List(Of S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAP_DIAGNOSAAKHIR), ByVal entityDetail_ As List(Of S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAP_DIAGNOSASEMENTARA)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDKUNJUNGAN
                sSTATUS = "UPDATE"

                Dim ds = oConnection.dbRME.S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAPs.FirstOrDefault(Function(x) x.KDASESMENNEONATUS = entity.KDASESMENNEONATUS)
                Dim dsDetail = oConnection.dbRME.S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAP_DIAGNOSAAKHIRs.Where(Function(x) x.KDASESMENNEONATUS = entity.KDASESMENNEONATUS)
                Dim dsDetail_ = oConnection.dbRME.S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAP_DIAGNOSASEMENTARAs.Where(Function(x) x.KDASESMENNEONATUS = entity.KDASESMENNEONATUS)

                Try
                    oConnection.dbRME.S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAPs.DeleteOnSubmit(ds)
                    oConnection.dbRME.S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAPs.InsertOnSubmit(entity)

                    If dsDetail IsNot Nothing Then
                        oConnection.dbRME.S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAP_DIAGNOSAAKHIRs.DeleteAllOnSubmit(dsDetail)
                    End If

                    If entityDetail.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAP_DIAGNOSAAKHIRs.InsertAllOnSubmit(entityDetail)
                    End If

                    If dsDetail_ IsNot Nothing Then
                        oConnection.dbRME.S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAP_DIAGNOSASEMENTARAs.DeleteAllOnSubmit(dsDetail_)
                    End If

                    If entityDetail_.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAP_DIAGNOSASEMENTARAs.InsertAllOnSubmit(entityDetail_)
                    End If
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.dbRME.SubmitChanges()
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
        Public Function DeleteData(ByVal sKDASESMENNEONATUS As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDASESMENNEONATUS
                sSTATUS = "DELETE"

                Dim ds = oConnection.dbRME.S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAPs.FirstOrDefault(Function(x) x.KDASESMENNEONATUS = sKDASESMENNEONATUS)
                Dim dsDetail = oConnection.dbRME.S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAP_DIAGNOSAAKHIRs.Where(Function(x) x.KDASESMENNEONATUS = sKDASESMENNEONATUS)
                Dim dsDetail_ = oConnection.dbRME.S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAP_DIAGNOSASEMENTARAs.Where(Function(x) x.KDASESMENNEONATUS = sKDASESMENNEONATUS)

                Try
                    oConnection.dbRME.S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAPs.DeleteOnSubmit(ds)
                    If dsDetail IsNot Nothing Then
                        oConnection.dbRME.S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAP_DIAGNOSAAKHIRs.DeleteAllOnSubmit(dsDetail)
                    End If
                    If dsDetail_ IsNot Nothing Then
                        oConnection.dbRME.S_DIGITAL_ASESMEN_AWAL_MEDIS_NEONATUS_RAWAT_INAP_DIAGNOSASEMENTARAs.DeleteAllOnSubmit(dsDetail_)
                    End If
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.dbRME.SubmitChanges()
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