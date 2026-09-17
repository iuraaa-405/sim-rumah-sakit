Imports System.Threading

Namespace EMedrek
    Public Class clsGeneralICU
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
            sMODUL = "GENERALICU"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_GENERALICU_H
            If Not oConnection.GetConnectionRME() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_GENERALICU_H
        End Function
        Public Function GetStructureDetailTandaVitalList() As List(Of S_GENERALICU_TANDAVITAL)
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailTandaVitalList = Nothing
            End If
            GetStructureDetailTandaVitalList = New List(Of S_GENERALICU_TANDAVITAL)
        End Function
        Public Function GetStructureDetailTandaVital() As S_GENERALICU_TANDAVITAL
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailTandaVital = Nothing
            End If
            GetStructureDetailTandaVital = New S_GENERALICU_TANDAVITAL
        End Function
        Public Function GetStructureDetaiTandaVital2List() As List(Of S_GENERALICU_TANDAVITAL2)
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetaiTandaVital2List = Nothing
            End If
            GetStructureDetaiTandaVital2List = New List(Of S_GENERALICU_TANDAVITAL2)
        End Function
        Public Function GetStructureDetailTandaVital2() As S_GENERALICU_TANDAVITAL2
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailTandaVital2 = Nothing
            End If
            GetStructureDetailTandaVital2 = New S_GENERALICU_TANDAVITAL2
        End Function
        Public Function GetStructureDetaiTandaVital3List() As List(Of S_GENERALICU_TANDAVITAL3)
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetaiTandaVital3List = Nothing
            End If
            GetStructureDetaiTandaVital3List = New List(Of S_GENERALICU_TANDAVITAL3)
        End Function
        Public Function GetStructureDetailTandaVital3() As S_GENERALICU_TANDAVITAL3
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailTandaVital3 = Nothing
            End If
            GetStructureDetailTandaVital3 = New S_GENERALICU_TANDAVITAL3
        End Function
        Public Function GetStructureDetaiTandaVital4List() As List(Of S_GENERALICU_TANDAVITAL4)
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetaiTandaVital4List = Nothing
            End If
            GetStructureDetaiTandaVital4List = New List(Of S_GENERALICU_TANDAVITAL4)
        End Function
        Public Function GetStructureDetailTandaVital4() As S_GENERALICU_TANDAVITAL4
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailTandaVital4 = Nothing
            End If
            GetStructureDetailTandaVital4 = New S_GENERALICU_TANDAVITAL4
        End Function
        Public Function GetStructureDetaiTandaVital5List() As List(Of S_GENERALICU_TANDAVITAL5)
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetaiTandaVital5List = Nothing
            End If
            GetStructureDetaiTandaVital5List = New List(Of S_GENERALICU_TANDAVITAL5)
        End Function
        Public Function GetStructureDetailTandaVital5() As S_GENERALICU_TANDAVITAL5
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailTandaVital5 = Nothing
            End If
            GetStructureDetailTandaVital5 = New S_GENERALICU_TANDAVITAL5
        End Function
        Public Function GetStructureDetaiTandaVital6List() As List(Of S_GENERALICU_TANDAVITAL6)
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetaiTandaVital6List = Nothing
            End If
            GetStructureDetaiTandaVital6List = New List(Of S_GENERALICU_TANDAVITAL6)
        End Function
        Public Function GetStructureDetailTandaVital6() As S_GENERALICU_TANDAVITAL6
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailTandaVital6 = Nothing
            End If
            GetStructureDetailTandaVital6 = New S_GENERALICU_TANDAVITAL6
        End Function
        Public Function GetStructureDetailHemodiamikList() As List(Of S_GENERALICU_HEMODINAMIK)
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailHemodiamikList = Nothing
            End If
            GetStructureDetailHemodiamikList = New List(Of S_GENERALICU_HEMODINAMIK)
        End Function
        Public Function GetStructureDetailHemodiamik() As S_GENERALICU_HEMODINAMIK
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailHemodiamik = Nothing
            End If
            GetStructureDetailHemodiamik = New S_GENERALICU_HEMODINAMIK
        End Function
        Public Function GetData() As List(Of S_GENERALICU_H)
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.S_GENERALICU_Hs.OrderByDescending(Function(x) x.KDGENERALICU).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As S_GENERALICU_H
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.S_GENERALICU_Hs.FirstOrDefault(Function(x) x.KDGENERALICU = Parameter)
        End Function
        Public Function GetDataDetailTandaVital(ByVal sKDGENERALICU As String) As List(Of S_GENERALICU_TANDAVITAL)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetailTandaVital = Nothing
                Exit Function
            End If
            GetDataDetailTandaVital = oConnection.dbRME.S_GENERALICU_TANDAVITALs.Where(Function(x) x.KDGENERALICU = sKDGENERALICU).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function GetDataDetailTandaVital2(ByVal sKDGENERALICU As String) As List(Of S_GENERALICU_TANDAVITAL2)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetailTandaVital2 = Nothing
                Exit Function
            End If
            GetDataDetailTandaVital2 = oConnection.dbRME.S_GENERALICU_TANDAVITAL2s.Where(Function(x) x.KDGENERALICU = sKDGENERALICU).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function GetDataDetailTandaVital3(ByVal sKDGENERALICU As String) As List(Of S_GENERALICU_TANDAVITAL3)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetailTandaVital3 = Nothing
                Exit Function
            End If
            GetDataDetailTandaVital3 = oConnection.dbRME.S_GENERALICU_TANDAVITAL3s.Where(Function(x) x.KDGENERALICU = sKDGENERALICU).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function GetDataDetailTandaVital4(ByVal sKDGENERALICU As String) As List(Of S_GENERALICU_TANDAVITAL4)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetailTandaVital4 = Nothing
                Exit Function
            End If
            GetDataDetailTandaVital4 = oConnection.dbRME.S_GENERALICU_TANDAVITAL4s.Where(Function(x) x.KDGENERALICU = sKDGENERALICU).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function GetDataDetailTandaVital5(ByVal sKDGENERALICU As String) As List(Of S_GENERALICU_TANDAVITAL5)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetailTandaVital5 = Nothing
                Exit Function
            End If
            GetDataDetailTandaVital5 = oConnection.dbRME.S_GENERALICU_TANDAVITAL5s.Where(Function(x) x.KDGENERALICU = sKDGENERALICU).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function GetDataDetailTandaVital6(ByVal sKDGENERALICU As String) As List(Of S_GENERALICU_TANDAVITAL6)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetailTandaVital6 = Nothing
                Exit Function
            End If
            GetDataDetailTandaVital6 = oConnection.dbRME.S_GENERALICU_TANDAVITAL6s.Where(Function(x) x.KDGENERALICU = sKDGENERALICU).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function GetDataDetailHemodiamik(ByVal sKDGENERALICU As String) As List(Of S_GENERALICU_HEMODINAMIK)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetailHemodiamik = Nothing
                Exit Function
            End If
            GetDataDetailHemodiamik = oConnection.dbRME.S_GENERALICU_HEMODINAMIKs.Where(Function(x) x.KDGENERALICU = sKDGENERALICU).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function InsertData(ByVal entity As S_GENERALICU_H, ByVal entityDetail As List(Of S_GENERALICU_TANDAVITAL), ByVal entityDetail2 As List(Of S_GENERALICU_TANDAVITAL2), ByVal entityDetailHemodiamik As List(Of S_GENERALICU_HEMODINAMIK), ByVal entityDetail3 As List(Of S_GENERALICU_TANDAVITAL3), ByVal entityDetail4 As List(Of S_GENERALICU_TANDAVITAL4), ByVal entityDetail5 As List(Of S_GENERALICU_TANDAVITAL5), ByVal entityDetail6 As List(Of S_GENERALICU_TANDAVITAL6)) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDGENERALICU
                sSTATUS = "INSERT"

                Try
                    Dim oData As New Grouper.clsR_Identitas_Grouper_Data
                    Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()
                    entity.DATECREATED = WaktuServer
                    entity.DATEUPDATED = WaktuServer

                    sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATECREATED)
                    If sLASTNUMBER = 0 Then
                        Try
                            oCounter.InsertData(sMODUL, entity.DATECREATED)
                            sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATECREATED)
                        Catch ex As Exception
                            sLASTNUMBER = 0
                        End Try
                    End If

                    entity.KDGENERALICU = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATECREATED)

                    For Each iLoop In entityDetail
                        iLoop.KDGENERALICU = entity.KDGENERALICU
                    Next
                    For Each iLoop In entityDetail2
                        iLoop.KDGENERALICU = entity.KDGENERALICU
                    Next
                    For Each iLoop In entityDetailHemodiamik
                        iLoop.KDGENERALICU = entity.KDGENERALICU
                    Next
                    For Each iLoop In entityDetail3
                        iLoop.KDGENERALICU = entity.KDGENERALICU
                    Next
                    For Each iLoop In entityDetail4
                        iLoop.KDGENERALICU = entity.KDGENERALICU
                    Next
                    For Each iLoop In entityDetail5
                        iLoop.KDGENERALICU = entity.KDGENERALICU
                    Next
                    For Each iLoop In entityDetail6
                        iLoop.KDGENERALICU = entity.KDGENERALICU
                    Next
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATECREATED), Year(entity.DATECREATED))
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.dbRME.S_GENERALICU_Hs.InsertOnSubmit(entity)
                    oConnection.dbRME.S_GENERALICU_TANDAVITALs.InsertAllOnSubmit(entityDetail)
                    oConnection.dbRME.S_GENERALICU_TANDAVITAL2s.InsertAllOnSubmit(entityDetail2)
                    oConnection.dbRME.S_GENERALICU_HEMODINAMIKs.InsertAllOnSubmit(entityDetailHemodiamik)
                    oConnection.dbRME.S_GENERALICU_TANDAVITAL3s.InsertAllOnSubmit(entityDetail3)
                    oConnection.dbRME.S_GENERALICU_TANDAVITAL4s.InsertAllOnSubmit(entityDetail4)
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

                InsertData = True
            Catch ex As Exception
                InsertData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_GENERALICU_H, ByVal entityDetail As List(Of S_GENERALICU_TANDAVITAL), ByVal entityDetail2 As List(Of S_GENERALICU_TANDAVITAL2), ByVal entityDetailHemodiamik As List(Of S_GENERALICU_HEMODINAMIK), ByVal entityDetail3 As List(Of S_GENERALICU_TANDAVITAL3), ByVal entityDetail4 As List(Of S_GENERALICU_TANDAVITAL4), ByVal entityDetail5 As List(Of S_GENERALICU_TANDAVITAL5), ByVal entityDetail6 As List(Of S_GENERALICU_TANDAVITAL6)) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDGENERALICU
                sSTATUS = "UPDATE"

                Dim oData As New Grouper.clsR_Identitas_Grouper_Data
                Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()
                entity.DATEUPDATED = WaktuServer

                Dim ds = oConnection.dbRME.S_GENERALICU_Hs.FirstOrDefault(Function(x) x.KDGENERALICU = entity.KDGENERALICU)
                Dim dsDetail = oConnection.dbRME.S_GENERALICU_TANDAVITALs.Where(Function(x) x.KDGENERALICU = entity.KDGENERALICU)
                Dim dsDetail2 = oConnection.dbRME.S_GENERALICU_TANDAVITAL2s.Where(Function(x) x.KDGENERALICU = entity.KDGENERALICU)
                Dim dsDetailHemodiamik = oConnection.dbRME.S_GENERALICU_HEMODINAMIKs.Where(Function(x) x.KDGENERALICU = entity.KDGENERALICU)
                Dim dsDetail3 = oConnection.dbRME.S_GENERALICU_TANDAVITAL3s.Where(Function(x) x.KDGENERALICU = entity.KDGENERALICU)
                Dim dsDetail4 = oConnection.dbRME.S_GENERALICU_TANDAVITAL4s.Where(Function(x) x.KDGENERALICU = entity.KDGENERALICU)
                Dim dsDetail5 = oConnection.dbRME.S_GENERALICU_TANDAVITAL5s.Where(Function(x) x.KDGENERALICU = entity.KDGENERALICU)
                Dim dsDetail6 = oConnection.dbRME.S_GENERALICU_TANDAVITAL6s.Where(Function(x) x.KDGENERALICU = entity.KDGENERALICU)

                Try
                    oConnection.dbRME.S_GENERALICU_Hs.DeleteOnSubmit(ds)
                    oConnection.dbRME.S_GENERALICU_Hs.InsertOnSubmit(entity)

                    If dsDetail IsNot Nothing Then
                        oConnection.dbRME.S_GENERALICU_TANDAVITALs.DeleteAllOnSubmit(dsDetail)
                    End If

                    If entityDetail IsNot Nothing Then
                        oConnection.dbRME.S_GENERALICU_TANDAVITALs.InsertAllOnSubmit(entityDetail)
                    End If

                    If dsDetail2 IsNot Nothing Then
                        oConnection.dbRME.S_GENERALICU_TANDAVITAL2s.DeleteAllOnSubmit(dsDetail2)
                    End If

                    If entityDetail2 IsNot Nothing Then
                        oConnection.dbRME.S_GENERALICU_TANDAVITAL2s.InsertAllOnSubmit(entityDetail2)
                    End If

                    If dsDetailHemodiamik IsNot Nothing Then
                        oConnection.dbRME.S_GENERALICU_HEMODINAMIKs.DeleteAllOnSubmit(dsDetailHemodiamik)
                    End If

                    If entityDetailHemodiamik IsNot Nothing Then
                        oConnection.dbRME.S_GENERALICU_HEMODINAMIKs.InsertAllOnSubmit(entityDetailHemodiamik)
                    End If

                    If dsDetail3 IsNot Nothing Then
                        oConnection.dbRME.S_GENERALICU_TANDAVITAL3s.DeleteAllOnSubmit(dsDetail3)
                    End If

                    If entityDetail3 IsNot Nothing Then
                        oConnection.dbRME.S_GENERALICU_TANDAVITAL3s.InsertAllOnSubmit(entityDetail3)
                    End If

                    If dsDetail4 IsNot Nothing Then
                        oConnection.dbRME.S_GENERALICU_TANDAVITAL4s.DeleteAllOnSubmit(dsDetail4)
                    End If

                    If entityDetail4 IsNot Nothing Then
                        oConnection.dbRME.S_GENERALICU_TANDAVITAL4s.InsertAllOnSubmit(entityDetail4)
                    End If

                    If dsDetail5 IsNot Nothing Then
                        oConnection.dbRME.S_GENERALICU_TANDAVITAL5s.DeleteAllOnSubmit(dsDetail5)
                    End If

                    If entityDetail5 IsNot Nothing Then
                        oConnection.dbRME.S_GENERALICU_TANDAVITAL5s.InsertAllOnSubmit(entityDetail5)
                    End If

                    If dsDetail6 IsNot Nothing Then
                        oConnection.dbRME.S_GENERALICU_TANDAVITAL6s.DeleteAllOnSubmit(dsDetail6)
                    End If

                    If entityDetail6 IsNot Nothing Then
                        oConnection.dbRME.S_GENERALICU_TANDAVITAL6s.InsertAllOnSubmit(entityDetail6)
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
        Public Function DeleteData(ByVal sKDGENERALICU As String) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDGENERALICU
                sSTATUS = "DELETE"

                Dim ds = oConnection.dbRME.S_GENERALICU_Hs.FirstOrDefault(Function(x) x.KDGENERALICU = sKDGENERALICU)
                Dim dsTandaVital = oConnection.dbRME.S_GENERALICU_TANDAVITALs.Where(Function(x) x.KDGENERALICU = sKDGENERALICU)
                Dim dsTandaVital2 = oConnection.dbRME.S_GENERALICU_TANDAVITAL2s.Where(Function(x) x.KDGENERALICU = sKDGENERALICU)
                Dim dsTandaVital3 = oConnection.dbRME.S_GENERALICU_TANDAVITAL3s.Where(Function(x) x.KDGENERALICU = sKDGENERALICU)
                Dim dsTandaVital4 = oConnection.dbRME.S_GENERALICU_TANDAVITAL4s.Where(Function(x) x.KDGENERALICU = sKDGENERALICU)
                Dim dsTandaVital5 = oConnection.dbRME.S_GENERALICU_TANDAVITAL5s.Where(Function(x) x.KDGENERALICU = sKDGENERALICU)
                Dim dsTandaVital6 = oConnection.dbRME.S_GENERALICU_TANDAVITAL6s.Where(Function(x) x.KDGENERALICU = sKDGENERALICU)
                Dim dsHemodiamik = oConnection.dbRME.S_GENERALICU_HEMODINAMIKs.Where(Function(x) x.KDGENERALICU = sKDGENERALICU)


                Try
                    oConnection.dbRME.S_GENERALICU_Hs.DeleteOnSubmit(ds)
                    If dsTandaVital.Count > 0 Then
                        oConnection.dbRME.S_GENERALICU_TANDAVITALs.InsertAllOnSubmit(dsTandaVital)
                    End If
                    If dsTandaVital2.Count > 0 Then
                        oConnection.dbRME.S_GENERALICU_TANDAVITAL2s.InsertAllOnSubmit(dsTandaVital2)
                    End If
                    If dsHemodiamik.Count > 0 Then
                        oConnection.dbRME.S_GENERALICU_HEMODINAMIKs.InsertAllOnSubmit(dsHemodiamik)
                    End If
                    If dsTandaVital3.Count > 0 Then
                        oConnection.dbRME.S_GENERALICU_TANDAVITAL3s.InsertAllOnSubmit(dsTandaVital3)
                    End If
                    If dsTandaVital4.Count > 0 Then
                        oConnection.dbRME.S_GENERALICU_TANDAVITAL4s.InsertAllOnSubmit(dsTandaVital4)
                    End If
                    If dsTandaVital5.Count > 0 Then
                        oConnection.dbRME.S_GENERALICU_TANDAVITAL5s.InsertAllOnSubmit(dsTandaVital5)
                    End If
                    If dsTandaVital6.Count > 0 Then
                        oConnection.dbRME.S_GENERALICU_TANDAVITAL6s.InsertAllOnSubmit(dsTandaVital6)
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