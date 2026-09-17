Imports System.Threading

Namespace Order
    Public Class clsOrderDarah
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
            sMODUL = "ORDERDARAH"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As R_ORDER_DARAH
            If Not oConnection.GetConnectionRME() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New R_ORDER_DARAH
        End Function
        Public Function GetStructureDetail() As R_ORDER_DARAH_D
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New R_ORDER_DARAH_D
        End Function
        Public Function GetStructureDetailList() As List(Of R_ORDER_DARAH_D)
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of R_ORDER_DARAH_D)
        End Function
        Public Function GetData() As List(Of R_ORDER_DARAH)
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.R_ORDER_DARAHs.OrderByDescending(Function(x) x.KDORDERDARAH).ToList()
        End Function
        Public Function GetData(ByVal sKDORDERDARAH As String) As R_ORDER_DARAH
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.R_ORDER_DARAHs.FirstOrDefault(Function(x) x.KDORDERDARAH = sKDORDERDARAH)
        End Function
        Public Function GetDataDetail() As List(Of R_ORDER_DARAH_D)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.dbRME.R_ORDER_DARAH_Ds.ToList()
        End Function
        Public Function GetDataDetail(ByVal sKDORDERDARAH As String) As List(Of R_ORDER_DARAH_D)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.dbRME.R_ORDER_DARAH_Ds.Where(Function(x) x.KDORDERDARAH = sKDORDERDARAH).ToList()
        End Function
        Public Function InsertData(ByVal entity As R_ORDER_DARAH, ByVal entityDetail As List(Of R_ORDER_DARAH_D)) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDORDERDARAH
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

                    entity.KDORDERDARAH = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATECREATED)

                    For Each iLoop In entityDetail
                        iLoop.KDORDERDARAH = entity.KDORDERDARAH
                    Next
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.dbRME.R_ORDER_DARAHs.InsertOnSubmit(entity)
                    oConnection.dbRME.R_ORDER_DARAH_Ds.InsertAllOnSubmit(entityDetail)
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
        Public Function UpdateData(ByVal entity As R_ORDER_DARAH, ByVal entityDetail As List(Of R_ORDER_DARAH_D)) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDORDERDARAH
                sSTATUS = "UPDATE"

                Dim ds = oConnection.dbRME.R_ORDER_DARAHs.FirstOrDefault(Function(x) x.KDORDERDARAH = entity.KDORDERDARAH)

                Try
                    oConnection.dbRME.R_ORDER_DARAHs.DeleteOnSubmit(ds)
                    oConnection.dbRME.R_ORDER_DARAHs.InsertOnSubmit(entity)
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail = oConnection.dbRME.R_ORDER_DARAH_Ds.Where(Function(x) x.KDORDERDARAH = entity.KDORDERDARAH)

                Try
                    oConnection.dbRME.R_ORDER_DARAH_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.dbRME.R_ORDER_DARAH_Ds.InsertAllOnSubmit(entityDetail)
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
        Public Function DeleteData(ByVal sKDORDERDARAH As String) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDORDERDARAH
                sSTATUS = "DELETE"

                Dim ds = oConnection.dbRME.R_ORDER_DARAHs.FirstOrDefault(Function(x) x.KDORDERDARAH = sKDORDERDARAH)
                Dim dsDetail = oConnection.dbRME.R_ORDER_DARAH_Ds.Where(Function(x) x.KDORDERDARAH = sKDORDERDARAH)

                Try
                    oConnection.dbRME.R_ORDER_DARAHs.DeleteOnSubmit(ds)
                    oConnection.dbRME.R_ORDER_DARAH_Ds.DeleteAllOnSubmit(dsDetail)
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