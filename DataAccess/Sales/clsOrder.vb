Namespace Sales
    Public Class clsOrder
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public oCounter As Setting.clsCounter = Nothing

        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0
        Public oItem As Reference.clsItem = Nothing
        Public sKDITEM As New List(Of String)

        Public Sub New(Optional ByVal sConnection As String = "")
            If sConnection = "" Then
                oConnection = New Setting.clsConnectionMain
                oError = New Setting.clsError
                oCounter = New Setting.clsCounter
                oItem = New Reference.clsItem
            Else
                oConnection = New Setting.clsConnectionMain("TAX")
                oError = New Setting.clsError("TAX")
                oCounter = New Setting.clsCounter("TAX")
                oItem = New Reference.clsItem("TAX")
            End If

            sMODUL = "ORD"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As R_ORDER_H
            If Not oConnection.GetConnectionRME() Then
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
        Public Function GetData() As List(Of R_ORDER_H)
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.R_ORDER_Hs.OrderByDescending(Function(x) x.KDORDER).ToList()
        End Function
        Public Function GetData(ByVal sKDORDER As String) As R_ORDER_H
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.R_ORDER_Hs.FirstOrDefault(Function(x) x.KDORDER = sKDORDER)
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
        Public Function InsertData(ByVal entity As R_ORDER_H, ByVal entityDetail As List(Of R_ORDER_D)) As String
            Try
                If Not oConnection.GetConnectionRME() Then
                    InsertData = ""
                    Exit Function
                End If

                sREFERENCE = entity.KDORDER
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

                    entity.KDORDER = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)
                    For Each iLoop In entityDetail
                        iLoop.KDORDER = entity.KDORDER
                    Next
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.dbRME.R_ORDER_Hs.InsertOnSubmit(entity)
                    oConnection.dbRME.R_ORDER_Ds.InsertAllOnSubmit(entityDetail)
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
                    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATE), Year(entity.DATE))
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                InsertData = entity.KDORDER
            Catch ex As Exception
                InsertData = ""
                'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As R_ORDER_H, ByVal entityDetail As List(Of R_ORDER_D)) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDORDER
                sSTATUS = "UPDATE"

                Dim ds = oConnection.dbRME.R_ORDER_Hs.FirstOrDefault(Function(x) x.KDORDER = entity.KDORDER)

                Try
                    oConnection.dbRME.R_ORDER_Hs.DeleteOnSubmit(ds)
                    oConnection.dbRME.R_ORDER_Hs.InsertOnSubmit(entity)
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail = oConnection.dbRME.R_ORDER_Ds.Where(Function(x) x.KDORDER = entity.KDORDER)
                Try
                    oConnection.dbRME.R_ORDER_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.dbRME.R_ORDER_Ds.InsertAllOnSubmit(entityDetail)
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
                'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            Finally
                'oConnection.dbRME.Dispose()

                'oConnection = Nothing
                'oError = Nothing
                'oCounter = Nothing
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

                Dim ds = oConnection.dbRME.R_ORDER_Hs.FirstOrDefault(Function(x) x.KDORDER = sKDORDER)
                Dim dsDetail = oConnection.dbRME.R_ORDER_Ds.Where(Function(x) x.KDORDER = sKDORDER)

                Try
                    oConnection.dbRME.R_ORDER_Hs.DeleteOnSubmit(ds)
                    oConnection.dbRME.R_ORDER_Ds.DeleteAllOnSubmit(dsDetail)
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
                'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
    End Class
End Namespace