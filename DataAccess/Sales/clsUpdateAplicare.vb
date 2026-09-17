Imports DataAccess.My.Resources

Namespace Sales
    Public Class clsUpdateAplicare
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

            sMODUL = "UPDATEAPLICARE"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As T_UPDATE_APLICARE
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New T_UPDATE_APLICARE
        End Function
        Public Function GetData() As List(Of T_UPDATE_APLICARE)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.T_UPDATE_APLICAREs.OrderBy(Function(x) x.KDUPDATE_APLICARE).ToList()
        End Function
        Public Function GetData(ByVal sKDUPDATE_APLICARE As String) As T_UPDATE_APLICARE
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.T_UPDATE_APLICAREs.FirstOrDefault(Function(x) x.KDUPDATE_APLICARE = sKDUPDATE_APLICARE)
        End Function
        Public Function GetDataByDepartment(ByVal sKDDEPARTMENT As String) As M_DEPARTMENT
            If Not oConnection.GetConnection() Then
                GetDataByDepartment = Nothing
                Exit Function
            End If
            GetDataByDepartment = oConnection.db.M_DEPARTMENTs.FirstOrDefault(Function(x) x.KDDEPARTMENT = sKDDEPARTMENT)
        End Function
        Public Function GetDataSync() As List(Of T_UPDATE_APLICARE)
            If Not oConnection.GetConnection() Then
                GetDataSync = Nothing
                Exit Function
            End If
            GetDataSync = oConnection.db.T_UPDATE_APLICAREs.OrderBy(Function(x) x.KDUPDATE_APLICARE).ToList()
        End Function
        Public Function InsertData(ByVal entity As T_UPDATE_APLICARE) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDUPDATE_APLICARE
                sSTATUS = "INSERT"

                Try
                    oConnection.db.T_UPDATE_APLICAREs.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal entity As T_UPDATE_APLICARE) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDUPDATE_APLICARE
                sSTATUS = "UPDATE"


                Dim ds = oConnection.db.T_UPDATE_APLICAREs.FirstOrDefault(Function(x) x.KDUPDATE_APLICARE = entity.KDUPDATE_APLICARE)

                Try
                    oConnection.db.T_UPDATE_APLICAREs.DeleteOnSubmit(ds)
                    oConnection.db.T_UPDATE_APLICAREs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal sKDUPDATE_APLICARE As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDUPDATE_APLICARE
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.T_UPDATE_APLICAREs.FirstOrDefault(Function(x) x.KDUPDATE_APLICARE = sKDUPDATE_APLICARE)

                Try
                    oConnection.db.T_UPDATE_APLICAREs.DeleteOnSubmit(ds)
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