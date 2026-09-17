Imports System.Threading

Namespace Transaksi
    Public Class clsDigital_IGD_01
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0
        Private oData As New Grouper.clsR_Identitas_Grouper_Data
        Public oCounter As Setting.clsCounter = Nothing

        Public Sub New()
            oConnection = New Setting.clsConnectionMain
            oError = New Setting.clsError
            sMODUL = "AMIGD"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_IGD_01
            If Not oConnection.GetConnectionRME() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_IGD_01
        End Function
        Public Function GetStructureDetail() As S_DIGITAL_IGD_01_DIAGNOSA
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New S_DIGITAL_IGD_01_DIAGNOSA
        End Function
        Public Function GetStructureDetailList() As List(Of S_DIGITAL_IGD_01_DIAGNOSA)
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of S_DIGITAL_IGD_01_DIAGNOSA)
        End Function
        Public Function GetStructureDetailTindakan() As S_DIGITAL_IGD_01_TINDAKAN
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailTindakan = Nothing
            End If
            GetStructureDetailTindakan = New S_DIGITAL_IGD_01_TINDAKAN
        End Function
        Public Function GetStructureDetailTindakanList() As List(Of S_DIGITAL_IGD_01_TINDAKAN)
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailTindakanList = Nothing
            End If
            GetStructureDetailTindakanList = New List(Of S_DIGITAL_IGD_01_TINDAKAN)
        End Function
        Public Function GetStructureDetailObat() As S_DIGITAL_IGD_01_RECIPE
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailObat = Nothing
            End If
            GetStructureDetailObat = New S_DIGITAL_IGD_01_RECIPE
        End Function
        Public Function GetStructureDetailObatList() As List(Of S_DIGITAL_IGD_01_RECIPE)
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailObatList = Nothing
            End If
            GetStructureDetailObatList = New List(Of S_DIGITAL_IGD_01_RECIPE)
        End Function
        Public Function GetStructureDetailTindakanPoli() As S_DIGITAL_IGD_01_TINDAKANPOLI
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailTindakanPoli = Nothing
            End If
            GetStructureDetailTindakanPoli = New S_DIGITAL_IGD_01_TINDAKANPOLI
        End Function
        Public Function GetStructureDetailTindakanPoliList() As List(Of S_DIGITAL_IGD_01_TINDAKANPOLI)
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailTindakanPoliList = Nothing
            End If
            GetStructureDetailTindakanPoliList = New List(Of S_DIGITAL_IGD_01_TINDAKANPOLI)
        End Function
        Public Function GetStructureDetailPenunjang() As S_DIGITAL_IGD_01_PENUNJANG
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailPenunjang = Nothing
            End If
            GetStructureDetailPenunjang = New S_DIGITAL_IGD_01_PENUNJANG
        End Function
        Public Function GetStructureDetailPenunjangList() As List(Of S_DIGITAL_IGD_01_PENUNJANG)
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailPenunjangList = Nothing
            End If
            GetStructureDetailPenunjangList = New List(Of S_DIGITAL_IGD_01_PENUNJANG)
        End Function
        Public Function GetDataByKodeList() As List(Of S_DIGITAL_IGD_01)
            If Not oConnection.GetConnectionRME() Then
                GetDataByKodeList = Nothing
                Exit Function
            End If
            GetDataByKodeList = oConnection.dbRME.S_DIGITAL_IGD_01s.OrderByDescending(Function(x) x.KODE).ToList()
        End Function
        Public Function GetDataByPendaftaran(ByVal Parameter As String) As S_DIGITAL_IGD_01
            If Not oConnection.GetConnectionRME() Then
                GetDataByPendaftaran = Nothing
                Exit Function
            End If
            GetDataByPendaftaran = oConnection.dbRME.S_DIGITAL_IGD_01s.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter And x.ISDELETE = False)
        End Function
        Public Function GetDataByKodeIGD(ByVal Parameter As String) As S_DIGITAL_IGD_01
            If Not oConnection.GetConnectionRME() Then
                GetDataByKodeIGD = Nothing
                Exit Function
            End If
            GetDataByKodeIGD = oConnection.dbRME.S_DIGITAL_IGD_01s.FirstOrDefault(Function(x) x.KODE = Parameter And x.ISDELETE = False)
        End Function
        Public Function GetDataDetail() As List(Of S_DIGITAL_IGD_01_DIAGNOSA)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.dbRME.S_DIGITAL_IGD_01_DIAGNOSAs.ToList()
        End Function
        Public Function GetDataDetail(ByVal sKODE As String) As List(Of S_DIGITAL_IGD_01_DIAGNOSA)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.dbRME.S_DIGITAL_IGD_01_DIAGNOSAs.Where(Function(x) x.KODE = sKODE).ToList()
        End Function
        Public Function GetDataDetailTindakanPoli(ByVal sKODE As String) As List(Of S_DIGITAL_IGD_01_TINDAKANPOLI)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetailTindakanPoli = Nothing
                Exit Function
            End If
            GetDataDetailTindakanPoli = oConnection.dbRME.S_DIGITAL_IGD_01_TINDAKANPOLIs.Where(Function(x) x.KODE = sKODE).ToList()
        End Function
        Public Function GetDataDetailTindakan(ByVal sKODE As String) As List(Of S_DIGITAL_IGD_01_TINDAKAN)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetailTindakan = Nothing
                Exit Function
            End If
            GetDataDetailTindakan = oConnection.dbRME.S_DIGITAL_IGD_01_TINDAKANs.Where(Function(x) x.KODE = sKODE).ToList()
        End Function
        Public Function GetDataDetailTindakanByItem(ByVal sKDPENDAFTARAN As String, ByVal sITEM As String) As S_DIGITAL_IGD_01_TINDAKAN
            If Not oConnection.GetConnectionRME() Then
                GetDataDetailTindakanByItem = Nothing
                Exit Function
            End If
            GetDataDetailTindakanByItem = oConnection.dbRME.S_DIGITAL_IGD_01_TINDAKANs.FirstOrDefault(Function(x) x.S_DIGITAL_IGD_01.KDPENDAFTARAN = sKDPENDAFTARAN And x.KDITEM = sITEM)
        End Function
        Public Function GetDataDetailPenunjang(ByVal sKODE As String) As List(Of S_DIGITAL_IGD_01_PENUNJANG)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetailPenunjang = Nothing
                Exit Function
            End If
            GetDataDetailPenunjang = oConnection.dbRME.S_DIGITAL_IGD_01_PENUNJANGs.Where(Function(x) x.KODE = sKODE).ToList()
        End Function
        Public Function GetDataDetailResepSelamaIGD(ByVal sKODE As String) As List(Of S_DIGITAL_IGD_01_RECIPE)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetailResepSelamaIGD = Nothing
                Exit Function
            End If
            GetDataDetailResepSelamaIGD = oConnection.dbRME.S_DIGITAL_IGD_01_RECIPEs.Where(Function(x) x.KODE = sKODE And x.SEQ < 100).ToList()
        End Function
        Public Function GetDataDetailResepObatRanap(ByVal sKODE As String) As List(Of S_DIGITAL_IGD_01_RECIPE)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetailResepObatRanap = Nothing
                Exit Function
            End If
            GetDataDetailResepObatRanap = oConnection.dbRME.S_DIGITAL_IGD_01_RECIPEs.Where(Function(x) x.KODE = sKODE And x.SEQ >= 100 And x.SEQ < 200).ToList()
        End Function
        Public Function GetDataDetailResepObatPulang(ByVal sKODE As String) As List(Of S_DIGITAL_IGD_01_RECIPE)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetailResepObatPulang = Nothing
                Exit Function
            End If
            GetDataDetailResepObatPulang = oConnection.dbRME.S_DIGITAL_IGD_01_RECIPEs.Where(Function(x) x.KODE = sKODE And x.SEQ >= 200).ToList()
        End Function
        Public Function GetDataByRMTerakhir(ByVal sKDCUSTOMER As String) As S_DIGITAL_IGD_01
            If Not oConnection.GetConnectionRME() Then
                GetDataByRMTerakhir = Nothing
                Exit Function
            End If
            GetDataByRMTerakhir = oConnection.dbRME.S_DIGITAL_IGD_01s.Where(Function(x) x.KDCUSTOMER = sKDCUSTOMER And x.ISDELETE = False).OrderByDescending(Function(x) x.KDPENDAFTARAN).FirstOrDefault()
        End Function
        Public Function GetDataDetailbynorec(ByVal sKDPENDAFTARAN As String) As List(Of S_DIGITAL_IGD_01)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetailbynorec = Nothing
                Exit Function
            End If
            GetDataDetailbynorec = oConnection.dbRME.S_DIGITAL_IGD_01s.Where(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN And x.ISDELETE = False).ToList()
        End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_IGD_01, ByVal entityDetailDiagnosa As List(Of S_DIGITAL_IGD_01_DIAGNOSA), ByVal entityDetailResep As List(Of S_DIGITAL_IGD_01_RECIPE), ByVal entityDetailTindakan As List(Of S_DIGITAL_IGD_01_TINDAKANPOLI), ByVal entityDetailPenunjang As List(Of S_DIGITAL_IGD_01_PENUNJANG), ByVal entityDetail As List(Of S_DIGITAL_IGD_01_TINDAKAN)) As String
            Try
                If Not oConnection.GetConnectionRME() Then
                    InsertData = ""
                    Exit Function
                End If

                sREFERENCE = entity.KODE
                sSTATUS = "INSERT"

                Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()
                entity.DATECREATED = WaktuServer
                entity.DATEUPDATED = WaktuServer

                Try
                    sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATECREATED)
                    If sLASTNUMBER = 0 Then
                        Try
                            oCounter.InsertData(sMODUL, entity.DATECREATED)
                            sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATECREATED)
                        Catch ex As Exception
                            sLASTNUMBER = 0
                        End Try
                    End If

                    entity.KODE = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATECREATED)

                    For Each iLoop In entityDetailDiagnosa
                        iLoop.KODE = entity.KODE
                    Next
                    For Each iLoop In entityDetailResep
                        iLoop.KODE = entity.KODE
                    Next
                    For Each iLoop In entityDetailTindakan
                        iLoop.KODE = entity.KODE
                    Next
                    For Each iLoop In entityDetailPenunjang
                        iLoop.KODE = entity.KODE
                    Next
                    For Each iLoop In entityDetail
                        iLoop.KODE = entity.KODE
                    Next

                    oConnection.dbRME.S_DIGITAL_IGD_01s.InsertOnSubmit(entity)

                    If entityDetailDiagnosa IsNot Nothing Then
                        oConnection.dbRME.S_DIGITAL_IGD_01_DIAGNOSAs.InsertAllOnSubmit(entityDetailDiagnosa)
                    End If
                    If entityDetailResep IsNot Nothing Then
                        oConnection.dbRME.S_DIGITAL_IGD_01_RECIPEs.InsertAllOnSubmit(entityDetailResep)
                    End If
                    If entityDetailTindakan IsNot Nothing Then
                        oConnection.dbRME.S_DIGITAL_IGD_01_TINDAKANPOLIs.InsertAllOnSubmit(entityDetailTindakan)
                    End If
                    If entityDetailPenunjang IsNot Nothing Then
                        oConnection.dbRME.S_DIGITAL_IGD_01_PENUNJANGs.InsertAllOnSubmit(entityDetailPenunjang)
                    End If
                    If entityDetail IsNot Nothing Then
                        oConnection.dbRME.S_DIGITAL_IGD_01_TINDAKANs.InsertAllOnSubmit(entityDetail)
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

                oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATECREATED), Year(entity.DATECREATED))

                InsertData = entity.KODE

                'Try
                '    Dim dsFormulir = oDataFormulir.GetStructureHeader
                '    With dsFormulir
                '        .DATECREATED = entity.DATECREATED
                '        .DATEUPDATED = entity.DATEUPDATED
                '        .KODE = 0
                '        .kodegrouper = entity.kodegrouper
                '        .KDFORMULIR = entity.KODE
                '        .NAMAFORMULIR = "ASESMEN MEDIS"
                '        .AKSI = "ADD"
                '        .DESCRIPTION = "ASESMEN AWAL MEDIS IGD"
                '        .KDUSER = entity.KDUSER
                '    End With

                '    oDataFormulir.InsertData(dsFormulir)
                'Catch ex As Exception

                'End Try
            Catch ex As Exception
                InsertData = ""
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_DIGITAL_IGD_01, ByVal entityDetailDiagnosa As List(Of S_DIGITAL_IGD_01_DIAGNOSA), ByVal entityDetailResep As List(Of S_DIGITAL_IGD_01_RECIPE), ByVal entityDetailTindakan As List(Of S_DIGITAL_IGD_01_TINDAKANPOLI), ByVal entityDetailPenunjang As List(Of S_DIGITAL_IGD_01_PENUNJANG), ByVal entityDetail As List(Of S_DIGITAL_IGD_01_TINDAKAN)) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KODE
                sSTATUS = "UPDATE"

                Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()
                entity.DATEUPDATED = WaktuServer

                Dim ds = oConnection.dbRME.S_DIGITAL_IGD_01s.FirstOrDefault(Function(x) x.KODE = entity.KODE)
                Dim dsDiagnosa = oConnection.dbRME.S_DIGITAL_IGD_01_DIAGNOSAs.Where(Function(x) x.KODE = entity.KODE)
                Dim dsResep = oConnection.dbRME.S_DIGITAL_IGD_01_RECIPEs.Where(Function(x) x.KODE = entity.KODE)
                Dim dsTindakan = oConnection.dbRME.S_DIGITAL_IGD_01_TINDAKANPOLIs.Where(Function(x) x.KODE = entity.KODE)
                Dim dsPenunjang = oConnection.dbRME.S_DIGITAL_IGD_01_PENUNJANGs.Where(Function(x) x.KODE = entity.KODE)
                Dim dsTindakanAll = oConnection.dbRME.S_DIGITAL_IGD_01_TINDAKANs.Where(Function(x) x.KODE = entity.KODE)

                Try
                    oConnection.dbRME.S_DIGITAL_IGD_01s.DeleteOnSubmit(ds)
                    oConnection.dbRME.S_DIGITAL_IGD_01s.InsertOnSubmit(entity)

                    If dsDiagnosa.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_IGD_01_DIAGNOSAs.DeleteAllOnSubmit(dsDiagnosa)
                    End If
                    If entityDetailDiagnosa.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_IGD_01_DIAGNOSAs.InsertAllOnSubmit(entityDetailDiagnosa)
                    End If
                    If dsResep.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_IGD_01_RECIPEs.DeleteAllOnSubmit(dsResep)
                    End If
                    If entityDetailResep.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_IGD_01_RECIPEs.InsertAllOnSubmit(entityDetailResep)
                    End If
                    If dsTindakan.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_IGD_01_TINDAKANPOLIs.DeleteAllOnSubmit(dsTindakan)
                    End If
                    If entityDetailTindakan.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_IGD_01_TINDAKANPOLIs.InsertAllOnSubmit(entityDetailTindakan)
                    End If
                    If dsPenunjang.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_IGD_01_PENUNJANGs.DeleteAllOnSubmit(dsPenunjang)
                    End If
                    If entityDetailPenunjang.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_IGD_01_PENUNJANGs.InsertAllOnSubmit(entityDetailPenunjang)
                    End If
                    If dsTindakanAll.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_IGD_01_TINDAKANs.DeleteAllOnSubmit(dsTindakanAll)
                    End If
                    If entityDetail.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_IGD_01_TINDAKANs.InsertAllOnSubmit(entityDetail)
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
        Public Function DeleteData(ByVal Parameter As String, ByVal sAlasanDelete As String) As Boolean
            Try
                If Not oConnection.GetConnectionRME Then
                    DeleteData = False
                    Exit Function
                End If

                DeleteData = True

                Dim ds = oConnection.dbRME.S_DIGITAL_IGD_01s.FirstOrDefault(Function(x) x.KODE = Parameter)

                If ds IsNot Nothing Then
                    Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()
                    ds.DATEUPDATED = WaktuServer
                    ds.ISDELETE = True
                    ds.CATATAN = sAlasanDelete

                    oConnection.dbRME.SubmitChanges()

                End If

            Catch ex As Exception
                DeleteData = False
                Throw ex
            End Try
        End Function
    End Class
End Namespace