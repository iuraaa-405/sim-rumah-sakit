Imports DataAccess.My.Resources

Namespace Reference
    Public Class clsLoincdanKfa
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

            sMODUL = "LOINCDANKFA"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As M_LOINCDANKFA
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New M_LOINCDANKFA
        End Function
        Public Function GetData() As List(Of M_LOINCDANKFA)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_LOINCDANKFAs.OrderBy(Function(x) x.MEMO).ToList()
        End Function
        Public Function GetDataKFA() As List(Of M_KFA)
            If Not oConnection.GetConnection() Then
                GetDataKFA = Nothing
                Exit Function
            End If
            GetDataKFA = oConnection.db.M_KFAs.OrderBy(Function(x) x.kodegrouper).ToList()
        End Function
        Public Function GetDataKfa(ByVal sKode As Integer) As M_KFA
            If Not oConnection.GetConnection() Then
                GetDataKfa = Nothing
                Exit Function
            End If
            GetDataKfa = oConnection.db.M_KFAs.FirstOrDefault(Function(x) x.kodegrouper = sKode)
        End Function
        Public Function GetData(ByVal sKDLOINCDANKFA As String) As M_LOINCDANKFA
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_LOINCDANKFAs.FirstOrDefault(Function(x) x.KDLOINCDANKFA = sKDLOINCDANKFA)
        End Function
        Public Function GetDataSync() As List(Of M_LOINCDANKFA)
            If Not oConnection.GetConnection() Then
                GetDataSync = Nothing
                Exit Function
            End If
            GetDataSync = oConnection.db.M_LOINCDANKFAs.OrderBy(Function(x) x.MEMO).ToList()
        End Function
        Public Function InsertData(ByVal entity As M_LOINCDANKFA) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDLOINCDANKFA
                sSTATUS = "INSERT"

                Try
                    oConnection.db.M_LOINCDANKFAs.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal entity As M_LOINCDANKFA) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDLOINCDANKFA
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.M_LOINCDANKFAs.FirstOrDefault(Function(x) x.KDLOINCDANKFA = entity.KDLOINCDANKFA)

                Try
                    oConnection.db.M_LOINCDANKFAs.DeleteOnSubmit(ds)
                    oConnection.db.M_LOINCDANKFAs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal sKDLOINCDANKFA As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDLOINCDANKFA
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.M_LOINCDANKFAs.FirstOrDefault(Function(x) x.KDLOINCDANKFA = sKDLOINCDANKFA)

                Try
                    oConnection.db.M_LOINCDANKFAs.DeleteOnSubmit(ds)
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