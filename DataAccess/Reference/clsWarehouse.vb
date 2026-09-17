Namespace Reference
    Public Class clsWarehouse
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing

        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0

        Public Sub New(Optional ByVal sConnection As String = "")
            If sConnection = "" Then
                oConnection = New Setting.clsConnectionMain
                oError = New Setting.clsError
            Else
                oConnection = New Setting.clsConnectionMain("TAX")
                oError = New Setting.clsError("TAX")
            End If

            sMODUL = "WAREHOUSE"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As M_WAREHOUSE
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New M_WAREHOUSE
        End Function
        Public Function GetData() As List(Of M_WAREHOUSE)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_WAREHOUSEs.OrderBy(Function(x) x.NAME_DISPLAY).ToList()
        End Function
        Public Function GetDataWarehouseUser() As List(Of M_WAREHOUSE_USER)
            If Not oConnection.GetConnection() Then
                GetDataWarehouseUser = Nothing
                Exit Function
            End If
            GetDataWarehouseUser = oConnection.db.M_WAREHOUSE_USERs.OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function GetData(ByVal sKDWAREHOUSE As String) As M_WAREHOUSE
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_WAREHOUSEs.FirstOrDefault(Function(x) x.KDWAREHOUSE = sKDWAREHOUSE)
        End Function
        Public Function GetDataSync() As List(Of M_WAREHOUSE)
            If Not oConnection.GetConnection() Then
                GetDataSync = Nothing
                Exit Function
            End If
            GetDataSync = oConnection.db.M_WAREHOUSEs.OrderBy(Function(x) x.NAME_DISPLAY).ToList()
        End Function
        Public Function IsExist(ByVal sNAME_DISPLAY As String) As Boolean
            If Not oConnection.GetConnection() Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.db.M_WAREHOUSEs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        Public Function GetDataDetail_WAREHOUSE() As List(Of M_WAREHOUSE_USER)
            If Not oConnection.GetConnection() Then
                GetDataDetail_WAREHOUSE = Nothing
                Exit Function
            End If
            GetDataDetail_WAREHOUSE = oConnection.db.M_WAREHOUSE_USERs.ToList()
        End Function
        Public Function GetDataDetail_ITEM() As List(Of M_ITEM_USER)
            If Not oConnection.GetConnection() Then
                GetDataDetail_ITEM = Nothing
                Exit Function
            End If
            GetDataDetail_ITEM = oConnection.db.M_ITEM_USERs.ToList()
        End Function
        Public Function GetStructureDetail_WAREHOUSEList() As List(Of M_WAREHOUSE_USER)
            If Not oConnection.GetConnection() Then
                GetStructureDetail_WAREHOUSEList = Nothing
            End If
            GetStructureDetail_WAREHOUSEList = New List(Of M_WAREHOUSE_USER)
        End Function
        Public Function GetStructureDetail_WAREHOUSE() As M_WAREHOUSE_USER
            If Not oConnection.GetConnection() Then
                GetStructureDetail_WAREHOUSE = Nothing
            End If
            GetStructureDetail_WAREHOUSE = New M_WAREHOUSE_USER
        End Function
        Public Function GetStructureDetail_ITEMList() As List(Of M_ITEM_USER)
            If Not oConnection.GetConnection() Then
                GetStructureDetail_ITEMList = Nothing
            End If
            GetStructureDetail_ITEMList = New List(Of M_ITEM_USER)
        End Function
        Public Function GetStructureDetail_ITEM() As M_ITEM_USER
            If Not oConnection.GetConnection() Then
                GetStructureDetail_ITEM = Nothing
            End If
            GetStructureDetail_ITEM = New M_ITEM_USER
        End Function
        Public Function InsertData(ByVal entity As M_WAREHOUSE) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDWAREHOUSE
                sSTATUS = "INSERT"

                'Generate Auto Number
                Try
                    sLASTNUMBER = CInt(oConnection.db.M_WAREHOUSEs.OrderByDescending(Function(x) x.KDWAREHOUSE).FirstOrDefault().KDWAREHOUSE.Remove(0, (sMODUL & " _ ").Length)) + 1
                Catch ex As Exception
                    sLASTNUMBER = 1
                End Try
                'End Generate

                Try
                    entity.KDWAREHOUSE = sMODUL & "_" & AutoNumberCode(sLASTNUMBER)
                    oConnection.db.M_WAREHOUSEs.InsertOnSubmit(entity)
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

                InsertData = True
            Catch ex As Exception
                InsertData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As M_WAREHOUSE) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDWAREHOUSE
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.M_WAREHOUSEs.FirstOrDefault(Function(x) x.KDWAREHOUSE = entity.KDWAREHOUSE)

                Try
                    oConnection.db.M_WAREHOUSEs.DeleteOnSubmit(ds)
                    oConnection.db.M_WAREHOUSEs.InsertOnSubmit(entity)
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
            End Try
        End Function
        Public Function DeleteData(ByVal sKDWAREHOUSE As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDWAREHOUSE
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.M_WAREHOUSEs.FirstOrDefault(Function(x) x.KDWAREHOUSE = sKDWAREHOUSE)

                Try
                    oConnection.db.M_WAREHOUSEs.DeleteOnSubmit(ds)
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
        Public Function InsertDataWarehouseUser(ByVal entity As List(Of M_WAREHOUSE_USER)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertDataWarehouseUser = False
                    Exit Function
                End If

                sSTATUS = "INSERT"

                Try
                    oConnection.db.M_WAREHOUSE_USERs.InsertAllOnSubmit(entity)
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

                InsertDataWarehouseUser = True
            Catch ex As Exception
                InsertDataWarehouseUser = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateDataWarehouseUser(ByVal entity As List(Of M_WAREHOUSE_USER), ByVal KDUSER As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateDataWarehouseUser = False
                    Exit Function
                End If

                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.M_WAREHOUSE_USERs.Where(Function(x) x.KDUSER = KDUSER)

                Try
                    oConnection.db.M_WAREHOUSE_USERs.DeleteAllOnSubmit(ds)
                    oConnection.db.M_WAREHOUSE_USERs.InsertAllOnSubmit(entity)
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

                UpdateDataWarehouseUser = True
            Catch ex As Exception
                UpdateDataWarehouseUser = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function InsertDataItemUser(ByVal entity As List(Of M_ITEM_USER)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertDataItemUser = False
                    Exit Function
                End If

                sSTATUS = "INSERT"

                Try
                    oConnection.db.M_ITEM_USERs.InsertAllOnSubmit(entity)
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

                InsertDataItemUser = True
            Catch ex As Exception
                InsertDataItemUser = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateDataItemUser(ByVal entity As List(Of M_ITEM_USER), ByVal KDUSER As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateDataItemUser = False
                    Exit Function
                End If

                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.M_ITEM_USERs.Where(Function(x) x.KDUSER = KDUSER)

                Try
                    oConnection.db.M_ITEM_USERs.DeleteAllOnSubmit(ds)
                    oConnection.db.M_ITEM_USERs.InsertAllOnSubmit(entity)
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

                UpdateDataItemUser = True
            Catch ex As Exception
                UpdateDataItemUser = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
    End Class
End Namespace