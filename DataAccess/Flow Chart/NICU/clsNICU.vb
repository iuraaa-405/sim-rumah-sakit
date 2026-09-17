Imports System.Threading

Namespace Flowchart
    Public Class clsNICU
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
            sMODUL = "FWN"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_FLOWCHART_NICU_H
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_FLOWCHART_NICU_H
        End Function
        Public Function GetStructureDetail_1() As S_DIGITAL_FLOWCHART_NICU_TANDAVITAL
            If Not oConnection.GetConnection() Then
                GetStructureDetail_1 = Nothing
            End If
            GetStructureDetail_1 = New S_DIGITAL_FLOWCHART_NICU_TANDAVITAL
        End Function
        Public Function GetStructureDetailList_1() As List(Of S_DIGITAL_FLOWCHART_NICU_TANDAVITAL)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList_1 = Nothing
            End If
            GetStructureDetailList_1 = New List(Of S_DIGITAL_FLOWCHART_NICU_TANDAVITAL)
        End Function
        Public Function GetStructureDetail_2() As S_DIGITAL_FLOWCHART_NICU_KESADARAN
            If Not oConnection.GetConnection() Then
                GetStructureDetail_2 = Nothing
            End If
            GetStructureDetail_2 = New S_DIGITAL_FLOWCHART_NICU_KESADARAN
        End Function
        Public Function GetStructureDetailList_2() As List(Of S_DIGITAL_FLOWCHART_NICU_KESADARAN)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList_2 = Nothing
            End If
            GetStructureDetailList_2 = New List(Of S_DIGITAL_FLOWCHART_NICU_KESADARAN)
        End Function
        Public Function GetStructureDetail_3() As S_DIGITAL_FLOWCHART_NICU_HEMODINAMIK
            If Not oConnection.GetConnection() Then
                GetStructureDetail_3 = Nothing
            End If
            GetStructureDetail_3 = New S_DIGITAL_FLOWCHART_NICU_HEMODINAMIK
        End Function
        Public Function GetStructureDetailList_3() As List(Of S_DIGITAL_FLOWCHART_NICU_HEMODINAMIK)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList_3 = Nothing
            End If
            GetStructureDetailList_3 = New List(Of S_DIGITAL_FLOWCHART_NICU_HEMODINAMIK)
        End Function
        Public Function GetStructureDetail_4() As S_DIGITAL_FLOWCHART_NICU_VENTILASI
            If Not oConnection.GetConnection() Then
                GetStructureDetail_4 = Nothing
            End If
            GetStructureDetail_4 = New S_DIGITAL_FLOWCHART_NICU_VENTILASI
        End Function
        Public Function GetStructureDetailList_4() As List(Of S_DIGITAL_FLOWCHART_NICU_VENTILASI)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList_4 = Nothing
            End If
            GetStructureDetailList_4 = New List(Of S_DIGITAL_FLOWCHART_NICU_VENTILASI)
        End Function
        Public Function GetData() As List(Of S_DIGITAL_FLOWCHART_NICU_H)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_FLOWCHART_NICU_Hs.OrderByDescending(Function(x) x.KDFLOWCHARTNICU).ToList()
        End Function
        Public Function GetData(ByVal sKDFLOWCHARTNICU As String) As S_DIGITAL_FLOWCHART_NICU_H
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_FLOWCHART_NICU_Hs.FirstOrDefault(Function(x) x.KDFLOWCHARTNICU = sKDFLOWCHARTNICU)
        End Function
        Public Function GetDataDetail_1(ByVal sKDFLOWCHARTNICU As String) As List(Of S_DIGITAL_FLOWCHART_NICU_TANDAVITAL)
            If Not oConnection.GetConnection() Then
                GetDataDetail_1 = Nothing
                Exit Function
            End If
            GetDataDetail_1 = oConnection.db.S_DIGITAL_FLOWCHART_NICU_TANDAVITALs.Where(Function(x) x.KDFLOWCHARTNICU = sKDFLOWCHARTNICU).ToList()
        End Function
        Public Function GetDataDetail_2(ByVal sKDFLOWCHARTNICU As String) As List(Of S_DIGITAL_FLOWCHART_NICU_KESADARAN)
            If Not oConnection.GetConnection() Then
                GetDataDetail_2 = Nothing
                Exit Function
            End If
            GetDataDetail_2 = oConnection.db.S_DIGITAL_FLOWCHART_NICU_KESADARANs.Where(Function(x) x.KDFLOWCHARTNICU = sKDFLOWCHARTNICU).ToList()
        End Function
        Public Function GetDataDetail_3(ByVal sKDFLOWCHARTNICU As String) As List(Of S_DIGITAL_FLOWCHART_NICU_HEMODINAMIK)
            If Not oConnection.GetConnection() Then
                GetDataDetail_3 = Nothing
                Exit Function
            End If
            GetDataDetail_3 = oConnection.db.S_DIGITAL_FLOWCHART_NICU_HEMODINAMIKs.Where(Function(x) x.KDFLOWCHARTNICU = sKDFLOWCHARTNICU).ToList()
        End Function
        Public Function GetDataDetail_4(ByVal sKDFLOWCHARTNICU As String) As List(Of S_DIGITAL_FLOWCHART_NICU_VENTILASI)
            If Not oConnection.GetConnection() Then
                GetDataDetail_4 = Nothing
                Exit Function
            End If
            GetDataDetail_4 = oConnection.db.S_DIGITAL_FLOWCHART_NICU_VENTILASIs.Where(Function(x) x.KDFLOWCHARTNICU = sKDFLOWCHARTNICU).ToList()
        End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_FLOWCHART_NICU_H, Optional ByVal entityDetail_1 As List(Of S_DIGITAL_FLOWCHART_NICU_TANDAVITAL) = Nothing, Optional ByVal entityDetail_2 As List(Of S_DIGITAL_FLOWCHART_NICU_KESADARAN) = Nothing, Optional ByVal entityDetail_3 As List(Of S_DIGITAL_FLOWCHART_NICU_HEMODINAMIK) = Nothing, Optional ByVal entityDetail_4 As List(Of S_DIGITAL_FLOWCHART_NICU_VENTILASI) = Nothing) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDFLOWCHARTNICU
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

                    entity.KDFLOWCHARTNICU = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)

                    For Each iLoop In entityDetail_1
                        iLoop.KDFLOWCHARTNICU = entity.KDFLOWCHARTNICU
                    Next
                    For Each iLoop In entityDetail_2
                        iLoop.KDFLOWCHARTNICU = entity.KDFLOWCHARTNICU
                    Next
                    For Each iLoop In entityDetail_3
                        iLoop.KDFLOWCHARTNICU = entity.KDFLOWCHARTNICU
                    Next
                    For Each iLoop In entityDetail_4
                        iLoop.KDFLOWCHARTNICU = entity.KDFLOWCHARTNICU
                    Next
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.S_DIGITAL_FLOWCHART_NICU_Hs.InsertOnSubmit(entity)
                    If entityDetail_1 IsNot Nothing Then
                        oConnection.db.S_DIGITAL_FLOWCHART_NICU_TANDAVITALs.InsertAllOnSubmit(entityDetail_1)
                    End If
                    If entityDetail_2 IsNot Nothing Then
                        oConnection.db.S_DIGITAL_FLOWCHART_NICU_KESADARANs.InsertAllOnSubmit(entityDetail_2)
                    End If
                    If entityDetail_3 IsNot Nothing Then
                        oConnection.db.S_DIGITAL_FLOWCHART_NICU_HEMODINAMIKs.InsertAllOnSubmit(entityDetail_3)
                    End If
                    If entityDetail_4 IsNot Nothing Then
                        oConnection.db.S_DIGITAL_FLOWCHART_NICU_VENTILASIs.InsertAllOnSubmit(entityDetail_4)
                    End If
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

                InsertData = True
            Catch ex As Exception
                InsertData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_DIGITAL_FLOWCHART_NICU_H, Optional ByVal entityDetail_1 As List(Of S_DIGITAL_FLOWCHART_NICU_TANDAVITAL) = Nothing, Optional ByVal entityDetail_2 As List(Of S_DIGITAL_FLOWCHART_NICU_KESADARAN) = Nothing, Optional ByVal entityDetail_3 As List(Of S_DIGITAL_FLOWCHART_NICU_HEMODINAMIK) = Nothing, Optional ByVal entityDetail_4 As List(Of S_DIGITAL_FLOWCHART_NICU_VENTILASI) = Nothing) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDFLOWCHARTNICU
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_DIGITAL_FLOWCHART_NICU_Hs.FirstOrDefault(Function(x) x.KDFLOWCHARTNICU = entity.KDFLOWCHARTNICU)

                Try
                    oConnection.db.S_DIGITAL_FLOWCHART_NICU_Hs.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_FLOWCHART_NICU_Hs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail_1 = oConnection.db.S_DIGITAL_FLOWCHART_NICU_TANDAVITALs.Where(Function(x) x.KDFLOWCHARTNICU = entity.KDFLOWCHARTNICU)
                If dsDetail_1 IsNot Nothing Then
                    oConnection.db.S_DIGITAL_FLOWCHART_NICU_TANDAVITALs.DeleteAllOnSubmit(dsDetail_1)
                End If
                If entityDetail_1 IsNot Nothing Then
                    oConnection.db.S_DIGITAL_FLOWCHART_NICU_TANDAVITALs.InsertAllOnSubmit(entityDetail_1)
                End If

                Dim dsDetail_2 = oConnection.db.S_DIGITAL_FLOWCHART_NICU_KESADARANs.Where(Function(x) x.KDFLOWCHARTNICU = entity.KDFLOWCHARTNICU)
                If dsDetail_2 IsNot Nothing Then
                    oConnection.db.S_DIGITAL_FLOWCHART_NICU_KESADARANs.DeleteAllOnSubmit(dsDetail_2)
                End If
                If entityDetail_2 IsNot Nothing Then
                    oConnection.db.S_DIGITAL_FLOWCHART_NICU_KESADARANs.InsertAllOnSubmit(entityDetail_2)
                End If

                Dim dsDetail_3 = oConnection.db.S_DIGITAL_FLOWCHART_NICU_HEMODINAMIKs.Where(Function(x) x.KDFLOWCHARTNICU = entity.KDFLOWCHARTNICU)
                If dsDetail_3 IsNot Nothing Then
                    oConnection.db.S_DIGITAL_FLOWCHART_NICU_HEMODINAMIKs.DeleteAllOnSubmit(dsDetail_3)
                End If
                If entityDetail_3 IsNot Nothing Then
                    oConnection.db.S_DIGITAL_FLOWCHART_NICU_HEMODINAMIKs.InsertAllOnSubmit(entityDetail_3)
                End If

                Dim dsDetail_4 = oConnection.db.S_DIGITAL_FLOWCHART_NICU_VENTILASIs.Where(Function(x) x.KDFLOWCHARTNICU = entity.KDFLOWCHARTNICU)
                If dsDetail_4 IsNot Nothing Then
                    oConnection.db.S_DIGITAL_FLOWCHART_NICU_VENTILASIs.DeleteAllOnSubmit(dsDetail_4)
                End If
                If entityDetail_4 IsNot Nothing Then
                    oConnection.db.S_DIGITAL_FLOWCHART_NICU_VENTILASIs.InsertAllOnSubmit(entityDetail_4)
                End If

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
        Public Function DeleteData(ByVal sKDFLOWCHARTNICU As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDFLOWCHARTNICU
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.S_DIGITAL_FLOWCHART_NICU_Hs.FirstOrDefault(Function(x) x.KDFLOWCHARTNICU = sKDFLOWCHARTNICU)
                Dim dsDetail_1 = oConnection.db.S_DIGITAL_FLOWCHART_NICU_TANDAVITALs.Where(Function(x) x.KDFLOWCHARTNICU = sKDFLOWCHARTNICU)
                Dim dsDetail_2 = oConnection.db.S_DIGITAL_FLOWCHART_NICU_KESADARANs.Where(Function(x) x.KDFLOWCHARTNICU = sKDFLOWCHARTNICU)
                Dim dsDetail_3 = oConnection.db.S_DIGITAL_FLOWCHART_NICU_HEMODINAMIKs.Where(Function(x) x.KDFLOWCHARTNICU = sKDFLOWCHARTNICU)
                Dim dsDetail_4 = oConnection.db.S_DIGITAL_FLOWCHART_NICU_VENTILASIs.Where(Function(x) x.KDFLOWCHARTNICU = sKDFLOWCHARTNICU)

                Try
                    oConnection.db.S_DIGITAL_FLOWCHART_NICU_Hs.DeleteOnSubmit(ds)
                    If dsDetail_1 IsNot Nothing Then
                        oConnection.db.S_DIGITAL_FLOWCHART_NICU_TANDAVITALs.DeleteAllOnSubmit(dsDetail_1)
                    End If
                    If dsDetail_2 IsNot Nothing Then
                        oConnection.db.S_DIGITAL_FLOWCHART_NICU_KESADARANs.DeleteAllOnSubmit(dsDetail_2)
                    End If
                    If dsDetail_3 IsNot Nothing Then
                        oConnection.db.S_DIGITAL_FLOWCHART_NICU_HEMODINAMIKs.DeleteAllOnSubmit(dsDetail_3)
                    End If
                    If dsDetail_4 IsNot Nothing Then
                        oConnection.db.S_DIGITAL_FLOWCHART_NICU_VENTILASIs.DeleteAllOnSubmit(dsDetail_4)
                    End If
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
    End Class
End Namespace