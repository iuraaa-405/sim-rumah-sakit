Imports System.Threading

Namespace Order
    Public Class clsOrderRanapRad
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
            sMODUL = "ORDERRANAPRAD"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As R_ORDER
            If Not oConnection.GetConnectionRME() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New R_ORDER
        End Function
        Public Function GetStructureHeaderLainnya() As R_ORDER_LAINNYA
            If Not oConnection.GetConnectionRME() Then
                GetStructureHeaderLainnya = Nothing
            End If
            GetStructureHeaderLainnya = New R_ORDER_LAINNYA
        End Function
        Public Function GetStructureDetail() As R_ORDER_RANAPRAD
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New R_ORDER_RANAPRAD
        End Function
        Public Function GetStructureDetailList() As List(Of R_ORDER_RANAPRAD)
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of R_ORDER_RANAPRAD)
        End Function
        Public Function GetData() As List(Of R_ORDER)
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.R_ORDERs.OrderByDescending(Function(x) x.KDORDER).ToList()
        End Function
        Public Function GetDataLainnya(ByVal sKDORDER As String) As R_ORDER_LAINNYA
            If Not oConnection.GetConnectionRME() Then
                GetDataLainnya = Nothing
                Exit Function
            End If
            GetDataLainnya = oConnection.dbRME.R_ORDER_LAINNYAs.FirstOrDefault(Function(x) x.KDORDER = sKDORDER)
        End Function
        Public Function GetData(ByVal sKDORDER As String) As R_ORDER
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.R_ORDERs.FirstOrDefault(Function(x) x.KDORDER = sKDORDER)
        End Function
        Public Function GetDataDetail() As List(Of R_ORDER_RANAPRAD)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.dbRME.R_ORDER_RANAPRADs.ToList()
        End Function
        Public Function GetDataDetail(ByVal sKDORDER As String) As List(Of R_ORDER_RANAPRAD)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.dbRME.R_ORDER_RANAPRADs.Where(Function(x) x.KDORDER = sKDORDER).ToList()
        End Function
        Public Function InsertData(ByVal entity As R_ORDER, ByVal entityDetail As List(Of R_ORDER_RANAPRAD), ByVal entityLainnya As R_ORDER_LAINNYA) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDORDER
                sSTATUS = "INSERT"

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
                    entityLainnya.KDORDER = entity.KDORDER

                    For Each iLoop In entityDetail
                        iLoop.KDORDER = entity.KDORDER
                    Next
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.dbRME.R_ORDERs.InsertOnSubmit(entity)
                    oConnection.dbRME.R_ORDER_LAINNYAs.InsertOnSubmit(entityLainnya)
                    oConnection.dbRME.R_ORDER_RANAPRADs.InsertAllOnSubmit(entityDetail)
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.dbRME.SubmitChanges()
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATECREATED), Year(entity.DATECREATED))
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                InsertData = True
            Catch ex As Exception
                InsertData = False
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As R_ORDER, ByVal entityDetail As List(Of R_ORDER_RANAPRAD), ByVal entityLainnya As R_ORDER_LAINNYA) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDORDER
                sSTATUS = "UPDATE"

                Dim ds = oConnection.dbRME.R_ORDERs.FirstOrDefault(Function(x) x.KDORDER = entity.KDORDER)

                Try
                    oConnection.dbRME.R_ORDERs.DeleteOnSubmit(ds)
                    oConnection.dbRME.R_ORDERs.InsertOnSubmit(entity)
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsLainnya = oConnection.dbRME.R_ORDER_LAINNYAs.FirstOrDefault(Function(x) x.KDORDER = entity.KDORDER)

                Try
                    oConnection.dbRME.R_ORDER_LAINNYAs.DeleteOnSubmit(dsLainnya)
                    oConnection.dbRME.R_ORDER_LAINNYAs.InsertOnSubmit(entityLainnya)
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail = oConnection.dbRME.R_ORDER_RANAPRADs.Where(Function(x) x.KDORDER = entity.KDORDER)

                Try
                    oConnection.dbRME.R_ORDER_RANAPRADs.DeleteAllOnSubmit(dsDetail)
                    oConnection.dbRME.R_ORDER_RANAPRADs.InsertAllOnSubmit(entityDetail)
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.dbRME.SubmitChanges()
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateData = True
            Catch ex As Exception
                UpdateData = False
                Throw ex
            End Try
        End Function
        Public Function DeleteData(ByVal sKDORDER As String) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDORDER
                sSTATUS = "DELETE"

                Dim ds = oConnection.dbRME.R_ORDERs.FirstOrDefault(Function(x) x.KDORDER = sKDORDER)
                Dim dsLainnya = oConnection.dbRME.R_ORDER_LAINNYAs.FirstOrDefault(Function(x) x.KDORDER = sKDORDER)
                Dim dsDetail = oConnection.dbRME.R_ORDER_RANAPRADs.Where(Function(x) x.KDORDER = sKDORDER)

                Try
                    oConnection.dbRME.R_ORDERs.DeleteOnSubmit(ds)
                    oConnection.dbRME.R_ORDER_LAINNYAs.DeleteOnSubmit(dsLainnya)
                    oConnection.dbRME.R_ORDER_RANAPRADs.DeleteAllOnSubmit(dsDetail)

                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.dbRME.SubmitChanges()
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                Throw ex
            End Try
        End Function
    End Class
End Namespace