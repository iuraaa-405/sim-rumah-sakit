Imports System.Data.SqlClient
Imports System.Threading

Namespace EMedrek
    Public Class clsS_DIGITAL_OK_LAPORANOPERASI
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public oCounter As Setting.clsCounter = Nothing
        Public sREFERENCE As String = ""
        Public sMODUL As String = ""
        Public sLASTNUMBER As Integer = 0

        Public Sub New()
            oConnection = New Setting.clsConnectionMain
            oError = New Setting.clsError
            oCounter = New Setting.clsCounter
            sMODUL = "LO"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_OK_LAPORANOPERASI
            If Not oConnection.GetConnectionRME Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_OK_LAPORANOPERASI
        End Function
        Public Function GetStructureDetailDiagnosaList() As List(Of S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA)
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailDiagnosaList = Nothing
            End If
            GetStructureDetailDiagnosaList = New List(Of S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA)
        End Function
        Public Function GetStructureDetailDiagnosa2List() As List(Of S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA_2)
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailDiagnosa2List = Nothing
            End If
            GetStructureDetailDiagnosa2List = New List(Of S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA_2)
        End Function
        Public Function GetStructureDetailProsedurList() As List(Of S_DIGITAL_OK_LAPORANOPERASI_PROSEDUR)
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailProsedurList = Nothing
            End If
            GetStructureDetailProsedurList = New List(Of S_DIGITAL_OK_LAPORANOPERASI_PROSEDUR)
        End Function
        Public Function GetStructureDetailDiagnosa() As S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailDiagnosa = Nothing
            End If
            GetStructureDetailDiagnosa = New S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA
        End Function
        Public Function GetStructureDetailDiagnosa2() As S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA_2
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailDiagnosa2 = Nothing
            End If
            GetStructureDetailDiagnosa2 = New S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA_2
        End Function
        Public Function GetStructureDetailProsedur() As S_DIGITAL_OK_LAPORANOPERASI_PROSEDUR
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailProsedur = Nothing
            End If
            GetStructureDetailProsedur = New S_DIGITAL_OK_LAPORANOPERASI_PROSEDUR
        End Function
        Public Function GetDataList() As List(Of S_DIGITAL_OK_LAPORANOPERASI)
            If Not oConnection.GetConnectionRME Then
                GetDataList = Nothing
                Exit Function
            End If
            GetDataList = oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASIs.OrderBy(Function(x) x.KDPENDAFTARAN).ToList()
        End Function
        Public Function GetDataByRMList(ByVal RM As String) As List(Of S_DIGITAL_OK_LAPORANOPERASI)
            If Not oConnection.GetConnectionRME Then
                GetDataByRMList = Nothing
                Exit Function
            End If
            GetDataByRMList = oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASIs.Where(Function(x) x.KDCUSTOMER = RM And x.ISDELETE = False).OrderBy(Function(x) x.KDPENDAFTARAN).ToList()
        End Function
        Public Function GetDataByKodePendaftaranList(ByVal sKDPENDAFTARAN As String) As List(Of S_DIGITAL_OK_LAPORANOPERASI)
            If Not oConnection.GetConnectionRME Then
                GetDataByKodePendaftaranList = Nothing
                Exit Function
            End If
            GetDataByKodePendaftaranList = oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASIs.Where(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN And x.ISDELETE = False).OrderBy(Function(x) x.KDPENDAFTARAN).ToList()
        End Function
        Public Function GetDataByKDPENDFATRAN(ByVal sParameter As String) As S_DIGITAL_OK_LAPORANOPERASI
            If Not oConnection.GetConnectionRME Then
                GetDataByKDPENDFATRAN = Nothing
                Exit Function
            End If
            GetDataByKDPENDFATRAN = oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASIs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sParameter And x.ISDELETE = False)
        End Function
        Public Function GetDataKode(ByVal sParameter As String) As S_DIGITAL_OK_LAPORANOPERASI
            If Not oConnection.GetConnectionRME Then
                GetDataKode = Nothing
                Exit Function
            End If
            GetDataKode = oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASIs.FirstOrDefault(Function(x) x.KDLAPORANOPERASI = sParameter And x.ISDELETE = False)
        End Function
        'Public Function GetDataKodeTindakan(ByVal sParameter As String) As S_DIGITAL_OK_LAPORANTINDAKAN
        '    If Not oConnection.GetConnectionRME Then
        '        GetDataKodeTindakan = Nothing
        '        Exit Function
        '    End If
        '    GetDataKodeTindakan = oConnection.dbRME.S_DIGITAL_OK_LAPORANTINDAKANs.FirstOrDefault(Function(x) x.KDLAPORANTINDAKAN = sParameter)
        'End Function
        Public Function GetDataByRM(ByVal kdcustomer As String) As List(Of S_DIGITAL_OK_LAPORANOPERASI)
            If Not oConnection.GetConnectionRME() Then
                GetDataByRM = Nothing
                Exit Function
            End If
            GetDataByRM = oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASIs.Where(Function(x) x.KDCUSTOMER = kdcustomer And x.ISDELETE = False).OrderByDescending(Function(x) x.KDPENDAFTARAN).ToList()
        End Function
        Public Function GetDataDetail(ByVal sParameter As String) As List(Of S_DIGITAL_OK_LAPORANOPERASI)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            'Dim KDPENDAFTARAN As String = oConnection.dbRME.R_IDENTITAS_PASIENs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sParameter).KDPENDAFTARAN
            GetDataDetail = oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASIs.Where(Function(x) x.KDPENDAFTARAN = sParameter).ToList()
        End Function
        Public Function GetDataDetailDiagnosa(ByVal sKDLAPORANOPERASI As String) As List(Of S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetailDiagnosa = Nothing
                Exit Function
            End If
            GetDataDetailDiagnosa = oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSAs.Where(Function(x) x.KDLAPORANOPERASI = sKDLAPORANOPERASI).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function GetDataDetailDiagnosa2(ByVal sKDLAPORANOPERASI As String) As List(Of S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA_2)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetailDiagnosa2 = Nothing
                Exit Function
            End If
            GetDataDetailDiagnosa2 = oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA_2s.Where(Function(x) x.KDLAPORANOPERASI = sKDLAPORANOPERASI).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function GetDataDetailProsedur(ByVal sKDLAPORANOPERASI As String) As List(Of S_DIGITAL_OK_LAPORANOPERASI_PROSEDUR)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetailProsedur = Nothing
                Exit Function
            End If
            GetDataDetailProsedur = oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_PROSEDURs.Where(Function(x) x.KDLAPORANOPERASI = sKDLAPORANOPERASI).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function GetSequence(ByVal sParameter As String) As Integer
            If Not oConnection.GetConnectionRME() Then
                GetSequence = Nothing
                Exit Function
            End If
            'Dim KDPENDAFTARAN As String = oConnection.dbRME.R_IDENTITAS_PASIENs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sParameter).KDPENDAFTARAN
            GetSequence = oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASIs.Where(Function(x) x.KDPENDAFTARAN = sParameter).OrderByDescending(Function(x) x.SEQ).FirstOrDefault.SEQ
        End Function
        Public Function IsExist(ByVal sParameter As String) As Boolean
            If Not oConnection.GetConnectionRME Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASIs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sParameter)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_OK_LAPORANOPERASI, ByVal entityDiagnosa As List(Of S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA), ByVal entityDiagnosa2 As List(Of S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA_2), ByVal entityProsedur As List(Of S_DIGITAL_OK_LAPORANOPERASI_PROSEDUR)) As String
            Try
                If Not oConnection.GetConnectionRME Then
                    InsertData = ""
                    Exit Function
                End If

                sREFERENCE = entity.KDPENDAFTARAN

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

                    entity.KDLAPORANOPERASI = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)

                    For Each iLoop In entityDiagnosa
                        iLoop.KDLAPORANOPERASI = entity.KDLAPORANOPERASI
                    Next

                    For Each iLoop In entityDiagnosa2
                        iLoop.KDLAPORANOPERASI = entity.KDLAPORANOPERASI
                    Next

                    For Each iLoop In entityProsedur
                        iLoop.KDLAPORANOPERASI = entity.KDLAPORANOPERASI
                    Next

                Catch ex As Exception
                    oError.InsertData(sMODUL, "S_DIGITAL_OK_LAPORANOPERASI", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASIs.InsertOnSubmit(entity)
                    If entityDiagnosa.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSAs.InsertAllOnSubmit(entityDiagnosa)
                    End If
                    If entityDiagnosa2.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA_2s.InsertAllOnSubmit(entityDiagnosa2)
                    End If
                    If entityProsedur.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_PROSEDURs.InsertAllOnSubmit(entityProsedur)
                    End If
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_OK_LAPORANOPERASI", "INSERTDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.dbRME.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_OK_LAPORANOPERASI", "INSERTDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATE), Year(entity.DATE))
                Catch ex As Exception
                    oError.InsertData(sMODUL, "INSERTDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                InsertData = entity.KDLAPORANOPERASI
            Catch ex As Exception
                InsertData = ""
                oError.InsertData("S_DIGITAL_OK_LAPORANOPERASI", "INSERTDATA", ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_DIGITAL_OK_LAPORANOPERASI, ByVal entityDiagnosa As List(Of S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA), ByVal entityDiagnosa2 As List(Of S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA_2), ByVal entityProsedur As List(Of S_DIGITAL_OK_LAPORANOPERASI_PROSEDUR)) As Boolean
            Try
                If Not oConnection.GetConnectionRME Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPENDAFTARAN

                Dim ds = oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASIs.FirstOrDefault(Function(x) x.KDLAPORANOPERASI = entity.KDLAPORANOPERASI) 'x.KDPENDAFTARAN = entity.KDPENDAFTARAN And x.SEQ = entity.SEQ
                Dim dsDiagnosa = oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSAs.Where(Function(x) x.KDLAPORANOPERASI = entity.KDLAPORANOPERASI)
                Dim dsDiagnosa2 = oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA_2s.Where(Function(x) x.KDLAPORANOPERASI = entity.KDLAPORANOPERASI)
                Dim dsProsedur = oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_PROSEDURs.Where(Function(x) x.KDLAPORANOPERASI = entity.KDLAPORANOPERASI)

                Try
                    oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASIs.DeleteOnSubmit(ds)
                    oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASIs.InsertOnSubmit(entity)

                    If dsDiagnosa.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSAs.DeleteAllOnSubmit(dsDiagnosa)
                    End If
                    If entityDiagnosa.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSAs.InsertAllOnSubmit(entityDiagnosa)
                    End If

                    If dsDiagnosa2.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA_2s.DeleteAllOnSubmit(dsDiagnosa2)
                    End If
                    If entityDiagnosa2.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA_2s.InsertAllOnSubmit(entityDiagnosa2)
                    End If

                    If dsProsedur.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_PROSEDURs.DeleteAllOnSubmit(dsProsedur)
                    End If
                    If entityProsedur.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_PROSEDURs.InsertAllOnSubmit(entityProsedur)
                    End If

                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_OK_LAPORANOPERASI", "UPDATEDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.dbRME.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_OK_LAPORANOPERASI", "UPDATEDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateData = True
            Catch ex As Exception
                UpdateData = False
                oError.InsertData("S_DIGITAL_OK_LAPORANOPERASI", "UPDATEDATA", ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function DeleteData(ByVal Parameter As Integer, ByVal sSEQ As Integer) As Boolean
            Try
                If Not oConnection.GetConnectionRME Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = Parameter


                Dim ds = oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASIs.FirstOrDefault(Function(x) x.KDLAPORANOPERASI = Parameter)
                Dim dsDiagnosa = oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSAs.Where(Function(x) x.KDLAPORANOPERASI = Parameter)
                Dim dsDiagnosa2 = oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA_2s.Where(Function(x) x.KDLAPORANOPERASI = Parameter)
                Dim dsProsedur = oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_PROSEDURs.Where(Function(x) x.KDLAPORANOPERASI = Parameter)

                Try
                    oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASIs.DeleteOnSubmit(ds)

                    If dsDiagnosa.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSAs.DeleteAllOnSubmit(dsDiagnosa)
                    End If

                    If dsDiagnosa2.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA_2s.DeleteAllOnSubmit(dsDiagnosa2)
                    End If

                    If dsProsedur.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_PROSEDURs.DeleteAllOnSubmit(dsProsedur)
                    End If
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_OK_LAPORANOPERASI", "DELETEDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.dbRME.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_OK_LAPORANOPERASI", "DELETEDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                oError.InsertData("S_DIGITAL_OK_LAPORANOPERASI", "DELETEDATA", ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateCetak(ByVal sKDPENDAFTARAN As String) As Boolean
            Try
                If Not oConnection.GetConnectionRME Then
                    UpdateCetak = False
                    Exit Function
                End If

                sREFERENCE = sKDPENDAFTARAN

                Try
                    Dim ds = oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASIs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN)

                    ds.CETAK += 1

                    oConnection.dbRME.SubmitChanges()

                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_OK_LAPORANOPERASI", "UPDATECETAK", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateCetak = True
            Catch ex As Exception
                UpdateCetak = False
                oError.InsertData("S_DIGITAL_OK_LAPORANOPERASI", "UPDATECETAK", ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateDeleteLaporanOperasi(ByVal KDLAPORANOPERASI As String, ByVal sUSER As String) As Boolean
            Try
                If Not oConnection.GetConnectionRME Then
                    UpdateDeleteLaporanOperasi = False
                    Exit Function
                End If

                sREFERENCE = KDLAPORANOPERASI

                Try
                    Dim ds = oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASIs.FirstOrDefault(Function(x) x.KDLAPORANOPERASI = KDLAPORANOPERASI)

                    ds.ISDELETE = 1
                    ds.DATEDELETE = Now
                    ds.USERDELETE = sUSER

                    oConnection.dbRME.SubmitChanges()

                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_OK_LAPORANOPERASI", "UPDATEDELETE", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateDeleteLaporanOperasi = True
            Catch ex As Exception
                UpdateDeleteLaporanOperasi = False
                oError.InsertData("S_DIGITAL_OK_LAPORANOPERASI", "UPDATEDELETE", ex.ToString, sREFERENCE)
                Throw ex
            End Try

        End Function
    End Class
End Namespace