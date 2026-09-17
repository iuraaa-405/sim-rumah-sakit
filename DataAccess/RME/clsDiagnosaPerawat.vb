Imports System.Threading

Namespace Digital
    Public Class clsDiagnosaPerawat
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
            sMODUL = "DPER"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_DIAGNOSAPERAWAT_H
            If Not oConnection.GetConnectionRME() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_DIAGNOSAPERAWAT_H
        End Function
        Public Function GetStructureDetail1() As S_DIGITAL_DIAGNOSAPERAWAT_D_1
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetail1 = Nothing
            End If
            GetStructureDetail1 = New S_DIGITAL_DIAGNOSAPERAWAT_D_1
        End Function
        Public Function GetStructureDetailList1() As List(Of S_DIGITAL_DIAGNOSAPERAWAT_D_1)
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailList1 = Nothing
            End If
            GetStructureDetailList1 = New List(Of S_DIGITAL_DIAGNOSAPERAWAT_D_1)
        End Function
        Public Function GetStructureDetail2() As S_DIGITAL_DIAGNOSAPERAWAT_D_2
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetail2 = Nothing
            End If
            GetStructureDetail2 = New S_DIGITAL_DIAGNOSAPERAWAT_D_2
        End Function
        Public Function GetStructureDetailList2() As List(Of S_DIGITAL_DIAGNOSAPERAWAT_D_2)
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailList2 = Nothing
            End If
            GetStructureDetailList2 = New List(Of S_DIGITAL_DIAGNOSAPERAWAT_D_2)
        End Function
        Public Function GetStructureDetail3() As S_DIGITAL_DIAGNOSAPERAWAT_D_3
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetail3 = Nothing
            End If
            GetStructureDetail3 = New S_DIGITAL_DIAGNOSAPERAWAT_D_3
        End Function
        Public Function GetStructureDetailList3() As List(Of S_DIGITAL_DIAGNOSAPERAWAT_D_3)
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailList3 = Nothing
            End If
            GetStructureDetailList3 = New List(Of S_DIGITAL_DIAGNOSAPERAWAT_D_3)
        End Function
        Public Function GetStructureDetail4() As S_DIGITAL_DIAGNOSAPERAWAT_D_4
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetail4 = Nothing
            End If
            GetStructureDetail4 = New S_DIGITAL_DIAGNOSAPERAWAT_D_4
        End Function
        Public Function GetStructureDetailList4() As List(Of S_DIGITAL_DIAGNOSAPERAWAT_D_4)
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailList4 = Nothing
            End If
            GetStructureDetailList4 = New List(Of S_DIGITAL_DIAGNOSAPERAWAT_D_4)
        End Function
        Public Function GetStructureDetail5() As S_DIGITAL_DIAGNOSAPERAWAT_D_5
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetail5 = Nothing
            End If
            GetStructureDetail5 = New S_DIGITAL_DIAGNOSAPERAWAT_D_5
        End Function
        Public Function GetStructureDetailList5() As List(Of S_DIGITAL_DIAGNOSAPERAWAT_D_5)
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailList5 = Nothing
            End If
            GetStructureDetailList5 = New List(Of S_DIGITAL_DIAGNOSAPERAWAT_D_5)
        End Function
        Public Function GetStructureDetail6() As S_DIGITAL_DIAGNOSAPERAWAT_D_6
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetail6 = Nothing
            End If
            GetStructureDetail6 = New S_DIGITAL_DIAGNOSAPERAWAT_D_6
        End Function
        Public Function GetStructureDetailList6() As List(Of S_DIGITAL_DIAGNOSAPERAWAT_D_6)
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailList6 = Nothing
            End If
            GetStructureDetailList6 = New List(Of S_DIGITAL_DIAGNOSAPERAWAT_D_6)
        End Function
        Public Function GetDataItem(ByVal Parameter As Boolean) As List(Of M_ITEM_DIAGNOSA_PERAWAT_H)
            If Not oConnection.GetConnectionRME() Then
                GetDataItem = Nothing
                Exit Function
            End If
            GetDataItem = oConnection.dbRME.M_ITEM_DIAGNOSA_PERAWAT_Hs.Where(Function(x) x.ISACTIVE = Parameter).OrderBy(Function(x) x.DESCRIPTION).ToList()
        End Function
        Public Function GetDataItemAll() As List(Of M_ITEM_DIAGNOSA_PERAWAT_H)
            If Not oConnection.GetConnectionRME() Then
                GetDataItemAll = Nothing
                Exit Function
            End If
            GetDataItemAll = oConnection.dbRME.M_ITEM_DIAGNOSA_PERAWAT_Hs.OrderBy(Function(x) x.DESCRIPTION).ToList()
        End Function
        Public Function GetDataItemDetil(ByVal Parameter As String) As List(Of M_ITEM_DIAGNOSA_PERAWAT_D)
            If Not oConnection.GetConnectionRME() Then
                GetDataItemDetil = Nothing
                Exit Function
            End If
            GetDataItemDetil = oConnection.dbRME.M_ITEM_DIAGNOSA_PERAWAT_Ds.Where(Function(x) x.KDITEMDIAGNOSAPERAWAT = Parameter).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function GetData() As List(Of S_DIGITAL_DIAGNOSAPERAWAT_H)
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_Hs.OrderByDescending(Function(x) x.KDDIAGNOSAPERAWAT).ToList()
        End Function
        Public Function GetData(ByVal sKDDIAGNOSAPERAWAT As String) As S_DIGITAL_DIAGNOSAPERAWAT_H
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_Hs.FirstOrDefault(Function(x) x.KDDIAGNOSAPERAWAT = sKDDIAGNOSAPERAWAT)
        End Function
        Public Function GetData1(ByVal sKDDIAGNOSAPERAWAT As String, ByVal sSEQ As Integer) As S_DIGITAL_DIAGNOSAPERAWAT_D_1
            If Not oConnection.GetConnectionRME() Then
                GetData1 = Nothing
                Exit Function
            End If
            GetData1 = oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_1s.FirstOrDefault(Function(x) x.KDDIAGNOSAPERAWAT = sKDDIAGNOSAPERAWAT And x.SEQ = sSEQ)
        End Function
        Public Function GetDataDetail1() As List(Of S_DIGITAL_DIAGNOSAPERAWAT_D_1)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetail1 = Nothing
                Exit Function
            End If
            GetDataDetail1 = oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_1s.ToList()
        End Function
        Public Function GetDataDetail1(ByVal sKDDIAGNOSAPERAWAT As String) As List(Of S_DIGITAL_DIAGNOSAPERAWAT_D_1)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetail1 = Nothing
                Exit Function
            End If
            GetDataDetail1 = oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_1s.Where(Function(x) x.KDDIAGNOSAPERAWAT = sKDDIAGNOSAPERAWAT).ToList()
        End Function
        Public Function GetData2(ByVal sKDDIAGNOSAPERAWAT As String, ByVal sSEQ As Integer) As S_DIGITAL_DIAGNOSAPERAWAT_D_2
            If Not oConnection.GetConnectionRME() Then
                GetData2 = Nothing
                Exit Function
            End If
            GetData2 = oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_2s.FirstOrDefault(Function(x) x.KDDIAGNOSAPERAWAT = sKDDIAGNOSAPERAWAT And x.SEQ = sSEQ)
        End Function
        Public Function GetDataDetail2() As List(Of S_DIGITAL_DIAGNOSAPERAWAT_D_2)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetail2 = Nothing
                Exit Function
            End If
            GetDataDetail2 = oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_2s.ToList()
        End Function
        Public Function GetDataDetail2(ByVal sKDDIAGNOSAPERAWAT As String) As List(Of S_DIGITAL_DIAGNOSAPERAWAT_D_2)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetail2 = Nothing
                Exit Function
            End If
            GetDataDetail2 = oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_2s.Where(Function(x) x.KDDIAGNOSAPERAWAT = sKDDIAGNOSAPERAWAT).ToList()
        End Function
        Public Function GetData3(ByVal sKDDIAGNOSAPERAWAT As String, ByVal sSEQ As Integer) As S_DIGITAL_DIAGNOSAPERAWAT_D_3
            If Not oConnection.GetConnectionRME() Then
                GetData3 = Nothing
                Exit Function
            End If
            GetData3 = oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_3s.FirstOrDefault(Function(x) x.KDDIAGNOSAPERAWAT = sKDDIAGNOSAPERAWAT And x.SEQ = sSEQ)
        End Function
        Public Function GetDataDetail3() As List(Of S_DIGITAL_DIAGNOSAPERAWAT_D_3)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetail3 = Nothing
                Exit Function
            End If
            GetDataDetail3 = oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_3s.ToList()
        End Function
        Public Function GetDataDetail3(ByVal sKDDIAGNOSAPERAWAT As String) As List(Of S_DIGITAL_DIAGNOSAPERAWAT_D_3)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetail3 = Nothing
                Exit Function
            End If
            GetDataDetail3 = oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_3s.Where(Function(x) x.KDDIAGNOSAPERAWAT = sKDDIAGNOSAPERAWAT).ToList()
        End Function
        Public Function GetData4(ByVal sKDDIAGNOSAPERAWAT As String, ByVal sSEQ As Integer) As S_DIGITAL_DIAGNOSAPERAWAT_D_4
            If Not oConnection.GetConnectionRME() Then
                GetData4 = Nothing
                Exit Function
            End If
            GetData4 = oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_4s.FirstOrDefault(Function(x) x.KDDIAGNOSAPERAWAT = sKDDIAGNOSAPERAWAT And x.SEQ = sSEQ)
        End Function
        Public Function GetDataDetail4() As List(Of S_DIGITAL_DIAGNOSAPERAWAT_D_4)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetail4 = Nothing
                Exit Function
            End If
            GetDataDetail4 = oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_4s.ToList()
        End Function
        Public Function GetDataDetail4(ByVal sKDDIAGNOSAPERAWAT As String) As List(Of S_DIGITAL_DIAGNOSAPERAWAT_D_4)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetail4 = Nothing
                Exit Function
            End If
            GetDataDetail4 = oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_4s.Where(Function(x) x.KDDIAGNOSAPERAWAT = sKDDIAGNOSAPERAWAT).ToList()
        End Function
        Public Function GetData5(ByVal sKDDIAGNOSAPERAWAT As String, ByVal sSEQ As Integer) As S_DIGITAL_DIAGNOSAPERAWAT_D_5
            If Not oConnection.GetConnectionRME() Then
                GetData5 = Nothing
                Exit Function
            End If
            GetData5 = oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_5s.FirstOrDefault(Function(x) x.KDDIAGNOSAPERAWAT = sKDDIAGNOSAPERAWAT And x.SEQ = sSEQ)
        End Function
        Public Function GetDataDetail5() As List(Of S_DIGITAL_DIAGNOSAPERAWAT_D_5)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetail5 = Nothing
                Exit Function
            End If
            GetDataDetail5 = oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_5s.ToList()
        End Function
        Public Function GetDataDetail5(ByVal sKDDIAGNOSAPERAWAT As String) As List(Of S_DIGITAL_DIAGNOSAPERAWAT_D_5)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetail5 = Nothing
                Exit Function
            End If
            GetDataDetail5 = oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_5s.Where(Function(x) x.KDDIAGNOSAPERAWAT = sKDDIAGNOSAPERAWAT).ToList()
        End Function
        Public Function GetDataDetailByKodeKunjunganDetail_5(ByVal sKDDIAGNOSAPERAWAT As String) As List(Of S_DIGITAL_DIAGNOSAPERAWAT_D_5)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetailByKodeKunjunganDetail_5 = Nothing
                Exit Function
            End If
            GetDataDetailByKodeKunjunganDetail_5 = oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_5s.Where(Function(x) x.KDDIAGNOSAPERAWAT = sKDDIAGNOSAPERAWAT).ToList()
        End Function
        Public Function GetData6(ByVal sKDDIAGNOSAPERAWAT As String, ByVal sSEQ As Integer) As S_DIGITAL_DIAGNOSAPERAWAT_D_6
            If Not oConnection.GetConnectionRME() Then
                GetData6 = Nothing
                Exit Function
            End If
            GetData6 = oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_6s.FirstOrDefault(Function(x) x.KDDIAGNOSAPERAWAT = sKDDIAGNOSAPERAWAT And x.SEQ = sSEQ)
        End Function
        Public Function GetDataDetail6() As List(Of S_DIGITAL_DIAGNOSAPERAWAT_D_6)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetail6 = Nothing
                Exit Function
            End If
            GetDataDetail6 = oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_6s.ToList()
        End Function
        Public Function GetDataDetail6(ByVal sKDDIAGNOSAPERAWAT As String) As List(Of S_DIGITAL_DIAGNOSAPERAWAT_D_6)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetail6 = Nothing
                Exit Function
            End If
            GetDataDetail6 = oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_6s.Where(Function(x) x.KDDIAGNOSAPERAWAT = sKDDIAGNOSAPERAWAT).ToList()
        End Function
        Public Function GetDataKunjunganAndkditem(ByVal sKDPENDAFTARAN As String, ByVal sKDITEMDIAGNOSAPERAWAT As String) As S_DIGITAL_DIAGNOSAPERAWAT_H
            If Not oConnection.GetConnectionRME() Then
                GetDataKunjunganAndkditem = Nothing
                Exit Function
            End If
            GetDataKunjunganAndkditem = oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN And x.KDITEMDIAGNOSAPERAWAT = sKDITEMDIAGNOSAPERAWAT)
        End Function
        Public Function InsertData(
                                   ByVal entity As S_DIGITAL_DIAGNOSAPERAWAT_H,
                                   ByVal entityDetail1 As List(Of S_DIGITAL_DIAGNOSAPERAWAT_D_1),
                                   ByVal entityDetail2 As List(Of S_DIGITAL_DIAGNOSAPERAWAT_D_2),
                                   ByVal entityDetail3 As List(Of S_DIGITAL_DIAGNOSAPERAWAT_D_3),
                                   ByVal entityDetail4 As List(Of S_DIGITAL_DIAGNOSAPERAWAT_D_4),
                                   ByVal entityDetail5 As List(Of S_DIGITAL_DIAGNOSAPERAWAT_D_5),
                                   ByVal entityDetail6 As List(Of S_DIGITAL_DIAGNOSAPERAWAT_D_6)) As String
            Try
                If Not oConnection.GetConnectionRME() Then
                    InsertData = ""
                    Exit Function
                End If

                sREFERENCE = entity.KDDIAGNOSAPERAWAT
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

                    entity.KDDIAGNOSAPERAWAT = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)
                    For Each iLoop In entityDetail1
                        iLoop.KDDIAGNOSAPERAWAT = entity.KDDIAGNOSAPERAWAT
                    Next
                    For Each iLoop In entityDetail2
                        iLoop.KDDIAGNOSAPERAWAT = entity.KDDIAGNOSAPERAWAT
                    Next
                    For Each iLoop In entityDetail3
                        iLoop.KDDIAGNOSAPERAWAT = entity.KDDIAGNOSAPERAWAT
                    Next
                    For Each iLoop In entityDetail4
                        iLoop.KDDIAGNOSAPERAWAT = entity.KDDIAGNOSAPERAWAT
                    Next
                    For Each iLoop In entityDetail5
                        iLoop.KDDIAGNOSAPERAWAT = entity.KDDIAGNOSAPERAWAT
                    Next
                    For Each iLoop In entityDetail6
                        iLoop.KDDIAGNOSAPERAWAT = entity.KDDIAGNOSAPERAWAT
                    Next
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_Hs.InsertOnSubmit(entity)
                    oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_1s.InsertAllOnSubmit(entityDetail1)
                    oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_2s.InsertAllOnSubmit(entityDetail2)
                    oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_3s.InsertAllOnSubmit(entityDetail3)
                    oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_4s.InsertAllOnSubmit(entityDetail4)
                    oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_5s.InsertAllOnSubmit(entityDetail5)
                    oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_6s.InsertAllOnSubmit(entityDetail6)
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

                InsertData = entity.KDDIAGNOSAPERAWAT
            Catch ex As Exception
                InsertData = ""
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_DIGITAL_DIAGNOSAPERAWAT_H,
                                   ByVal entityDetail1 As List(Of S_DIGITAL_DIAGNOSAPERAWAT_D_1),
                                   ByVal entityDetail2 As List(Of S_DIGITAL_DIAGNOSAPERAWAT_D_2),
                                   ByVal entityDetail3 As List(Of S_DIGITAL_DIAGNOSAPERAWAT_D_3),
                                   ByVal entityDetail4 As List(Of S_DIGITAL_DIAGNOSAPERAWAT_D_4),
                                   ByVal entityDetail5 As List(Of S_DIGITAL_DIAGNOSAPERAWAT_D_5),
                                   ByVal entityDetail6 As List(Of S_DIGITAL_DIAGNOSAPERAWAT_D_6)) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDDIAGNOSAPERAWAT
                sSTATUS = "UPDATE"

                Dim ds = oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_Hs.FirstOrDefault(Function(x) x.KDDIAGNOSAPERAWAT = entity.KDDIAGNOSAPERAWAT)

                Try
                    oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_Hs.DeleteOnSubmit(ds)
                    oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_Hs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail1 = oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_1s.Where(Function(x) x.KDDIAGNOSAPERAWAT = entity.KDDIAGNOSAPERAWAT)

                Try
                    oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_1s.DeleteAllOnSubmit(dsDetail1)
                    oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_1s.InsertAllOnSubmit(entityDetail1)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail2 = oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_2s.Where(Function(x) x.KDDIAGNOSAPERAWAT = entity.KDDIAGNOSAPERAWAT)

                Try
                    oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_2s.DeleteAllOnSubmit(dsDetail2)
                    oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_2s.InsertAllOnSubmit(entityDetail2)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail3 = oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_3s.Where(Function(x) x.KDDIAGNOSAPERAWAT = entity.KDDIAGNOSAPERAWAT)

                Try
                    oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_3s.DeleteAllOnSubmit(dsDetail3)
                    oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_3s.InsertAllOnSubmit(entityDetail3)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try


                Dim dsDetail4 = oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_4s.Where(Function(x) x.KDDIAGNOSAPERAWAT = entity.KDDIAGNOSAPERAWAT)

                Try
                    oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_4s.DeleteAllOnSubmit(dsDetail4)
                    oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_4s.InsertAllOnSubmit(entityDetail4)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail5 = oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_5s.Where(Function(x) x.KDDIAGNOSAPERAWAT = entity.KDDIAGNOSAPERAWAT)

                Try
                    oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_5s.DeleteAllOnSubmit(dsDetail5)
                    oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_5s.InsertAllOnSubmit(entityDetail5)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail6 = oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_6s.Where(Function(x) x.KDDIAGNOSAPERAWAT = entity.KDDIAGNOSAPERAWAT)

                Try
                    oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_6s.DeleteAllOnSubmit(dsDetail6)
                    oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_6s.InsertAllOnSubmit(entityDetail6)
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
        Public Function DeleteData(ByVal sKDDIAGNOSAPERAWAT As String) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDDIAGNOSAPERAWAT
                sSTATUS = "DELETE"

                Dim ds = oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_Hs.FirstOrDefault(Function(x) x.KDDIAGNOSAPERAWAT = sKDDIAGNOSAPERAWAT)
                Dim dsDetail1 = oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_1s.Where(Function(x) x.KDDIAGNOSAPERAWAT = sKDDIAGNOSAPERAWAT)
                Dim dsDetail2 = oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_2s.Where(Function(x) x.KDDIAGNOSAPERAWAT = sKDDIAGNOSAPERAWAT)
                Dim dsDetail3 = oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_3s.Where(Function(x) x.KDDIAGNOSAPERAWAT = sKDDIAGNOSAPERAWAT)
                Dim dsDetail4 = oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_4s.Where(Function(x) x.KDDIAGNOSAPERAWAT = sKDDIAGNOSAPERAWAT)
                Dim dsDetail5 = oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_5s.Where(Function(x) x.KDDIAGNOSAPERAWAT = sKDDIAGNOSAPERAWAT)
                Dim dsDetail6 = oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_6s.Where(Function(x) x.KDDIAGNOSAPERAWAT = sKDDIAGNOSAPERAWAT)

                Try
                    oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_Hs.DeleteOnSubmit(ds)
                    If dsDetail1.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_1s.DeleteAllOnSubmit(dsDetail1)
                    End If
                    If dsDetail2.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_2s.DeleteAllOnSubmit(dsDetail2)
                    End If
                    If dsDetail3.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_3s.DeleteAllOnSubmit(dsDetail3)
                    End If
                    If dsDetail4.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_4s.DeleteAllOnSubmit(dsDetail4)
                    End If
                    If dsDetail5.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_5s.DeleteAllOnSubmit(dsDetail5)
                    End If
                    If dsDetail6.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_D_6s.DeleteAllOnSubmit(dsDetail6)
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
        Public Function UpdateDataDelete(ByVal sKDDIAGNOSAPERAWAT As String) As Boolean
            Try
                If Not oConnection.GetConnectionRME Then
                    UpdateDataDelete = False
                    Exit Function
                End If

                UpdateDataDelete = True

                Dim ds = oConnection.dbRME.S_DIGITAL_DIAGNOSAPERAWAT_Hs.FirstOrDefault(Function(x) x.KDDIAGNOSAPERAWAT = sKDDIAGNOSAPERAWAT)

                ds.ISACTIVE = 1

                oConnection.dbRME.SubmitChanges()

            Catch ex As Exception
                UpdateDataDelete = False
                Throw ex
            End Try
        End Function
    End Class
End Namespace