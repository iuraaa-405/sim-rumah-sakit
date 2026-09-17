Namespace Sales
    Public Class clsSalesOrder
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public oCounter As Setting.clsCounter = Nothing

        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0

        Public Sub New(Optional ByVal sConnection As String = "")
            If sConnection = "" Then
                oConnection = New Setting.clsConnectionMain
                oError = New Setting.clsError
                oCounter = New Setting.clsCounter
            Else
                oConnection = New Setting.clsConnectionMain("TAX")
                oError = New Setting.clsError("TAX")
                oCounter = New Setting.clsCounter("TAX")
            End If

            sMODUL = "SO"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_SO_H
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_SO_H
        End Function
        Public Function GetStructureDetail() As S_SO_D
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New S_SO_D
        End Function
        Public Function GetStructureDetailList() As List(Of S_SO_D)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of S_SO_D)
        End Function
        Public Function GetData() As List(Of S_SO_H)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_SO_Hs.OrderByDescending(Function(x) x.KDSO).ToList()
        End Function
        Public Function GetData(ByVal sKDSO As String) As S_SO_H
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_SO_Hs.FirstOrDefault(Function(x) x.KDSO = sKDSO)
        End Function
        Public Function GetDataDetail(ByVal sKDSO As String, ByVal sKDITEM As String, ByVal sKDUOM As String, ByVal sSEQ As Integer) As S_SO_D
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_SO_Ds.FirstOrDefault(Function(x) x.KDSO = sKDSO And x.KDITEM = sKDITEM And x.KDUOM = sKDUOM And sSEQ = sSEQ)
        End Function
        Public Function GetDataDetail() As List(Of S_SO_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_SO_Ds.ToList()
        End Function
        Public Function GetDataDetail(ByVal sKDSO As String) As List(Of S_SO_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_SO_Ds.Where(Function(x) x.KDSO = sKDSO).ToList()
        End Function
        Public Function InsertData(ByVal entity As S_SO_H, ByVal entityDetail As List(Of S_SO_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDSO
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

                    entity.KDSO = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)
                    For Each iLoop In entityDetail
                        iLoop.KDSO = entity.KDSO
                    Next
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.S_SO_Hs.InsertOnSubmit(entity)
                    oConnection.db.S_SO_Ds.InsertAllOnSubmit(entityDetail)
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
            Finally
                oConnection.db.Dispose()

                oConnection = Nothing
                oError = Nothing
                oCounter = Nothing
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_SO_H, ByVal entityDetail As List(Of S_SO_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDSO
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_SO_Hs.FirstOrDefault(Function(x) x.KDSO = entity.KDSO)

                Try
                    oConnection.db.S_SO_Hs.DeleteOnSubmit(ds)
                    oConnection.db.S_SO_Hs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail = oConnection.db.S_SO_Ds.Where(Function(x) x.KDSO = entity.KDSO)

                Try
                    oConnection.db.S_SO_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.S_SO_Ds.InsertAllOnSubmit(entityDetail)
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

                UpdateData = True
            Catch ex As Exception
                UpdateData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            Finally
                oConnection.db.Dispose()

                oConnection = Nothing
                oError = Nothing
                oCounter = Nothing
            End Try
        End Function
        Public Function DeleteData(ByVal sKDSO As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDSO
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.S_SO_Hs.FirstOrDefault(Function(x) x.KDSO = sKDSO)
                Dim dsDetail = oConnection.db.S_SO_Ds.Where(Function(x) x.KDSO = sKDSO)

                Try
                    'ds.ISDELETE = True

                    oConnection.db.S_SO_Hs.DeleteOnSubmit(ds)
                    oConnection.db.S_SO_Ds.DeleteAllOnSubmit(dsDetail)
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
            Finally
                oConnection.db.Dispose()

                oConnection = Nothing
                oError = Nothing
                oCounter = Nothing
            End Try
        End Function
    End Class
End Namespace