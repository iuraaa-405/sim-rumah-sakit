Namespace Grouper
    Public Class clsStatusGrouper
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
            sMODUL = "STATUSGROUPER"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As R_IDENTITAS_GROUPER_STATUS
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New R_IDENTITAS_GROUPER_STATUS
        End Function
        Public Function GetData() As List(Of R_IDENTITAS_GROUPER_STATUS)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.R_IDENTITAS_GROUPER_STATUS.OrderByDescending(Function(x) x.kodegrouper).ToList()
        End Function
        Public Function GetData(ByVal Parameter As Integer) As R_IDENTITAS_GROUPER_STATUS
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.R_IDENTITAS_GROUPER_STATUS.FirstOrDefault(Function(x) x.kodegrouper = Parameter)
        End Function
        Public Function InsertData(ByVal entity As R_IDENTITAS_GROUPER_STATUS) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.kodegrouper
                sSTATUS = "INSERT"

                Try
                    oConnection.db.R_IDENTITAS_GROUPER_STATUS.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal entity As R_IDENTITAS_GROUPER_STATUS) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.kodegrouper
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.R_IDENTITAS_GROUPER_STATUS.FirstOrDefault(Function(x) x.kodegrouper = entity.kodegrouper)
                Try
                    oConnection.db.R_IDENTITAS_GROUPER_STATUS.DeleteOnSubmit(ds)
                    oConnection.db.R_IDENTITAS_GROUPER_STATUS.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal Parameter As Integer) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = Parameter
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.R_IDENTITAS_GROUPER_STATUS.FirstOrDefault(Function(x) x.kodegrouper = Parameter)

                If ds IsNot Nothing Then
                    Try
                        oConnection.db.R_IDENTITAS_GROUPER_STATUS.DeleteOnSubmit(ds)
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
                End If

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateStatus(ByVal Parameter As Integer, ByVal sSTATUS As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateStatus = False
                    Exit Function
                End If

                UpdateStatus = True

                Dim ds = oConnection.db.R_IDENTITAS_GROUPER_STATUS.FirstOrDefault(Function(x) x.kodegrouper = Parameter)

                If ds IsNot Nothing Then
                    ds.STATUS = sSTATUS
                    oConnection.db.SubmitChanges()
                End If
            Catch ex As Exception
                UpdateStatus = False
                Throw ex
            End Try
        End Function
    End Class
End Namespace