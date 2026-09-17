Imports System.Data.SqlClient
Imports System.Threading

Namespace EMedrek
    Public Class clsOrderTerjadwal
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
            sMODUL = "ORDERTERJADWAL"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As R_ORDER_H
            If Not oConnection.GetConnectionRME Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New R_ORDER_H
        End Function
        Public Function GetStructureDetail() As R_ORDER_D
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New R_ORDER_D
        End Function
        Public Function GetStructureDetailList() As List(Of R_ORDER_D)
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of R_ORDER_D)
        End Function
        Public Function GetDataHeader(ByVal sKDORDER As String) As R_ORDER_H
            If Not oConnection.GetConnectionRME() Then
                GetDataHeader = Nothing
                Exit Function
            End If
            GetDataHeader = oConnection.dbRME.R_ORDER_Hs.FirstOrDefault(Function(x) x.KDORDER = sKDORDER)
        End Function
        Public Function GetDataDetail() As List(Of R_ORDER_D)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.dbRME.R_ORDER_Ds.ToList()
        End Function
        Public Function GetDataDetail(ByVal sKDORDER As String) As List(Of R_ORDER_D)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.dbRME.R_ORDER_Ds.Where(Function(x) x.KDORDER = sKDORDER).ToList()
        End Function
        Public Function GetDataDetailList() As List(Of R_ORDER_H)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetailList = Nothing
                Exit Function
            End If
            GetDataDetailList = oConnection.dbRME.R_ORDER_Hs().ToList()
        End Function
        Public Function InsertData(ByVal entity As R_ORDER_H, ByVal entityDetail As List(Of R_ORDER_D)) As String
            Try
                If Not oConnection.GetConnectionRME Then
                    InsertData = ""
                    Exit Function
                End If

                sREFERENCE = entity.KDORDER

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

                    entity.KDORDER = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATECREATED)

                    For Each iLoop In entityDetail
                        iLoop.KDORDER = entity.KDORDER
                    Next
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.dbRME.R_ORDER_Hs.InsertOnSubmit(entity)
                    If entityDetail.Count > 0 Then
                        oConnection.dbRME.R_ORDER_Ds.InsertAllOnSubmit(entityDetail)
                    End If
                Catch ex As Exception
                    oError.InsertData("R_ORDER_D", "INSERTDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.dbRME.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("R_ORDER_D", "INSERTDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATECREATED), Year(entity.DATECREATED))
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                InsertData = entity.KDORDER
            Catch ex As Exception
                InsertData = ""
                oError.InsertData("R_ORDER_D", "INSERTDATA", ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As R_ORDER_H, ByVal entityDetail As List(Of R_ORDER_D)) As Boolean
            Try
                If Not oConnection.GetConnectionRME Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDORDER

                Dim ds = oConnection.dbRME.R_ORDER_Hs.FirstOrDefault(Function(x) x.KDORDER = entity.KDORDER)
                Dim dsDetail = oConnection.dbRME.R_ORDER_Ds.Where(Function(x) x.KDORDER = entity.KDORDER)

                Try
                    oConnection.dbRME.R_ORDER_Hs.DeleteOnSubmit(ds)
                    oConnection.dbRME.R_ORDER_Hs.InsertOnSubmit(entity)

                    If dsDetail.Count > 0 Then
                        oConnection.dbRME.R_ORDER_Ds.DeleteAllOnSubmit(dsDetail)
                    End If
                    If entityDetail.Count > 0 Then
                        oConnection.dbRME.R_ORDER_Ds.InsertAllOnSubmit(entityDetail)
                    End If
                Catch ex As Exception
                    oError.InsertData("R_ORDER_D", "UPDATEDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.dbRME.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("R_ORDER_D", "UPDATEDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateData = True
            Catch ex As Exception
                UpdateData = False
                oError.InsertData("R_ORDER_D", "UPDATEDATA", ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function DeleteData(ByVal sKDITEM_L3 As String) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDITEM_L3
                'sSTATUS = "DELETE"
                Dim ds = oConnection.dbRME.R_ORDER_Hs.FirstOrDefault(Function(x) x.KDORDER = sKDITEM_L3)
                Dim dsDetail = oConnection.dbRME.R_ORDER_Ds.Where(Function(x) x.KDORDER = sKDITEM_L3)

                Try
                    oConnection.dbRME.R_ORDER_Hs.DeleteOnSubmit(ds)
                    oConnection.dbRME.R_ORDER_Ds.DeleteAllOnSubmit(dsDetail)
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
    End Class
End Namespace