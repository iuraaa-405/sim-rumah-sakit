Imports System.Threading

Namespace Transaksi
    Public Class clsS_DIGITAL_OK_LAPORANOPERASITemplate
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
            sMODUL = "HNO"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE
            If Not oConnection.GetConnectionRME() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE
        End Function
        Public Function GetStructureDetailDiagnosaList() As List(Of S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE_DIAGNOSA)
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailDiagnosaList = Nothing
            End If
            GetStructureDetailDiagnosaList = New List(Of S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE_DIAGNOSA)
        End Function
        Public Function GetStructureDetailDiagnosa() As S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE_DIAGNOSA
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailDiagnosa = Nothing
            End If
            GetStructureDetailDiagnosa = New S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE_DIAGNOSA
        End Function
        Public Function GetStructureDetailDiagnosa2List() As List(Of S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE_DIAGNOSA_2)
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailDiagnosa2List = Nothing
            End If
            GetStructureDetailDiagnosa2List = New List(Of S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE_DIAGNOSA_2)
        End Function
        Public Function GetStructureDetailDiagnosa2() As S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE_DIAGNOSA_2
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailDiagnosa2 = Nothing
            End If
            GetStructureDetailDiagnosa2 = New S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE_DIAGNOSA_2
        End Function
        Public Function GetStructureDetailProsedurList() As List(Of S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE_PROSEDUR)
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailProsedurList = Nothing
            End If
            GetStructureDetailProsedurList = New List(Of S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE_PROSEDUR)
        End Function
        Public Function GetStructureDetailProsedur() As S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE_PROSEDUR
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailProsedur = Nothing
            End If
            GetStructureDetailProsedur = New S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE_PROSEDUR
        End Function
        Public Function GetData() As List(Of S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE)
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_TEMPLATEs.OrderByDescending(Function(x) x.DATECREATED).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_TEMPLATEs.FirstOrDefault(Function(x) x.KDJUDUL = Parameter)
        End Function
        Public Function GetDataDetailDiagnosa(ByVal sKDJUDUL As String) As List(Of S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE_DIAGNOSA)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetailDiagnosa = Nothing
                Exit Function
            End If
            GetDataDetailDiagnosa = oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE_DIAGNOSAs.Where(Function(x) x.KDJUDUL = sKDJUDUL).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function GetDataDetailDiagnosa2(ByVal sKDJUDUL As String) As List(Of S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE_DIAGNOSA_2)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetailDiagnosa2 = Nothing
                Exit Function
            End If
            GetDataDetailDiagnosa2 = oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE_DIAGNOSA_2s.Where(Function(x) x.KDJUDUL = sKDJUDUL).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function GetDataDetailProsedur(ByVal sKDJUDUL As String) As List(Of S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE_PROSEDUR)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetailProsedur = Nothing
                Exit Function
            End If
            GetDataDetailProsedur = oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE_PROSEDURs.Where(Function(x) x.KDJUDUL = sKDJUDUL).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE, ByVal entityDiagnosa As List(Of S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE_DIAGNOSA), ByVal entityDiagnosa2 As List(Of S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE_DIAGNOSA_2), ByVal entityProsedur As List(Of S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE_PROSEDUR)) As String
            Try
                If Not oConnection.GetConnectionRME() Then
                    InsertData = ""
                    Exit Function
                End If

                sREFERENCE = entity.KDJUDUL
                sSTATUS = "INSERT"

                Try
                    oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_TEMPLATEs.InsertOnSubmit(entity)

                    If entityDiagnosa.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE_DIAGNOSAs.InsertAllOnSubmit(entityDiagnosa)
                    End If
                    If entityDiagnosa2.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE_DIAGNOSA_2s.InsertAllOnSubmit(entityDiagnosa2)
                    End If
                    If entityProsedur.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE_PROSEDURs.InsertAllOnSubmit(entityProsedur)
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

                InsertData = entity.KDJUDUL
            Catch ex As Exception
                InsertData = ""
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE, ByVal entityDiagnosa As List(Of S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE_DIAGNOSA), ByVal entityDiagnosa2 As List(Of S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE_DIAGNOSA_2), ByVal entityProsedur As List(Of S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE_PROSEDUR)) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDJUDUL
                sSTATUS = "UPDATE"

                Dim ds = oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_TEMPLATEs.FirstOrDefault(Function(x) x.KDJUDUL = entity.KDJUDUL)
                Dim dsDiagnosa = oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE_DIAGNOSAs.Where(Function(x) x.KDJUDUL = entity.KDJUDUL)
                Dim dsDiagnosa2 = oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE_DIAGNOSA_2s.Where(Function(x) x.KDJUDUL = entity.KDJUDUL)
                Dim dsProsedur = oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE_PROSEDURs.Where(Function(x) x.KDJUDUL = entity.KDJUDUL)

                Try
                    oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_TEMPLATEs.DeleteOnSubmit(ds)
                    oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_TEMPLATEs.InsertOnSubmit(entity)

                    If dsDiagnosa.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE_DIAGNOSAs.DeleteAllOnSubmit(dsDiagnosa)
                    End If
                    If entityDiagnosa.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE_DIAGNOSAs.InsertAllOnSubmit(entityDiagnosa)
                    End If

                    If dsDiagnosa2.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE_DIAGNOSA_2s.DeleteAllOnSubmit(dsDiagnosa2)
                    End If
                    If entityDiagnosa2.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE_DIAGNOSA_2s.InsertAllOnSubmit(entityDiagnosa2)
                    End If

                    If dsProsedur.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE_PROSEDURs.DeleteAllOnSubmit(dsProsedur)
                    End If
                    If entityProsedur.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE_PROSEDURs.InsertAllOnSubmit(entityProsedur)
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
        Public Function DeleteData(ByVal Parameter As String) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = Parameter
                sSTATUS = "DELETE"

                Dim ds = oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_TEMPLATEs.FirstOrDefault(Function(x) x.KDJUDUL = Parameter)

                Try
                    oConnection.dbRME.S_DIGITAL_OK_LAPORANOPERASI_TEMPLATEs.DeleteOnSubmit(ds)
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
                'Finally
                '    oConnection.dbRME.Dispose()
            End Try
        End Function
    End Class
End Namespace