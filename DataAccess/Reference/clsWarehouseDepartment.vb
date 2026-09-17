Imports DataAccess.My.Resources

Namespace Reference
    Public Class clsWarehouseDepartment
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

            sMODUL = "WAREHOUSEDEPARTMENT"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As M_WAREHOUSE_DEPARTMENT
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New M_WAREHOUSE_DEPARTMENT
        End Function
        Public Function GetData() As List(Of M_WAREHOUSE_DEPARTMENT)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_WAREHOUSE_DEPARTMENTs.OrderBy(Function(x) x.MEMO).ToList()
        End Function
        Public Function GetData(ByVal sKDWAREHOUSE As String, ByVal sKDDEPARTMENT As String) As M_WAREHOUSE_DEPARTMENT
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_WAREHOUSE_DEPARTMENTs.FirstOrDefault(Function(x) x.KDWAREHOUSE = sKDWAREHOUSE And x.KDDEPARTMENT = sKDDEPARTMENT)
        End Function
        Public Function GetDataWarehouse(ByVal sKDWAREHOUSE As String) As M_WAREHOUSE_DEPARTMENT
            If Not oConnection.GetConnection() Then
                GetDataWarehouse = Nothing
                Exit Function
            End If
            GetDataWarehouse = oConnection.db.M_WAREHOUSE_DEPARTMENTs.FirstOrDefault(Function(x) x.KDWAREHOUSE = sKDWAREHOUSE)
        End Function
        Public Function GetDataDepartment(ByVal sKDDEPARTMENT As String) As M_WAREHOUSE_DEPARTMENT
            If Not oConnection.GetConnection() Then
                GetDataDepartment = Nothing
                Exit Function
            End If
            GetDataDepartment = oConnection.db.M_WAREHOUSE_DEPARTMENTs.FirstOrDefault(Function(x) x.KDDEPARTMENT = sKDDEPARTMENT)
        End Function
        Public Function InsertData(ByVal entity As M_WAREHOUSE_DEPARTMENT) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                Try
                    oConnection.db.M_WAREHOUSE_DEPARTMENTs.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal entity As M_WAREHOUSE_DEPARTMENT) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                Dim ds = oConnection.db.M_WAREHOUSE_DEPARTMENTs.FirstOrDefault(Function(x) x.KDWAREHOUSE = entity.KDWAREHOUSE And x.KDDEPARTMENT = x.KDDEPARTMENT)

                Try
                    oConnection.db.M_WAREHOUSE_DEPARTMENTs.DeleteOnSubmit(ds)
                    oConnection.db.M_WAREHOUSE_DEPARTMENTs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal sKDWAREHOUSE As String, ByVal sKDDEPARTMENT As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sSTATUS = "DELETE"

                Dim ds = oConnection.db.M_WAREHOUSE_DEPARTMENTs.FirstOrDefault(Function(x) x.KDWAREHOUSE = sKDWAREHOUSE And x.KDDEPARTMENT = sKDDEPARTMENT)

                Try
                    oConnection.db.M_WAREHOUSE_DEPARTMENTs.DeleteOnSubmit(ds)
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