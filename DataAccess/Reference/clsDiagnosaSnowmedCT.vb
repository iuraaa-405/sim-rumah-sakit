Namespace Reference
    Public Class clsDiagnosaSnowmedCT
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

            sMODUL = "DIAGNOSA"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As M_DIAGNOSA_SNOMED_CT
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New M_DIAGNOSA_SNOMED_CT
        End Function
        Public Function GetData() As List(Of M_DIAGNOSA_SNOMED_CT)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_DIAGNOSA_SNOMED_CTs.OrderBy(Function(x) x.KDDIAGNOSA).ToList()
        End Function
        Public Function GetData(ByVal sKDDIAGNOSA As String) As M_DIAGNOSA_SNOMED_CT
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_DIAGNOSA_SNOMED_CTs.FirstOrDefault(Function(x) x.KDDIAGNOSA = sKDDIAGNOSA)
        End Function
        Public Function GetDataSnowmed(ByVal sKDDIAGNOSA As String) As M_DIAGNOSA_SNOMED_CT
            If Not oConnection.GetConnection() Then
                GetDataSnowmed = Nothing
                Exit Function
            End If
            GetDataSnowmed = oConnection.db.M_DIAGNOSA_SNOMED_CTs.FirstOrDefault(Function(x) x.KDSNOMED_CT = sKDDIAGNOSA)
        End Function
        Public Function GetDataSync() As List(Of M_DIAGNOSA_SNOMED_CT)
            If Not oConnection.GetConnection() Then
                GetDataSync = Nothing
                Exit Function
            End If
            GetDataSync = oConnection.db.M_DIAGNOSA_SNOMED_CTs.OrderBy(Function(x) x.KDDIAGNOSA).ToList()
        End Function
        Public Function IsExist(ByVal sKDDIAGNOSA As String) As Boolean
            If Not oConnection.GetConnection() Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.db.M_DIAGNOSA_SNOMED_CTs.FirstOrDefault(Function(x) x.KDDIAGNOSA = sKDDIAGNOSA)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        Public Function InsertData(ByVal entity As M_DIAGNOSA_SNOMED_CT) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDDIAGNOSA
                sSTATUS = "INSERT"

                Try
                    oConnection.db.M_DIAGNOSA_SNOMED_CTs.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal entity As M_DIAGNOSA_SNOMED_CT) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDDIAGNOSA
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.M_DIAGNOSA_SNOMED_CTs.FirstOrDefault(Function(x) x.KDDIAGNOSA = entity.KDDIAGNOSA)

                Try
                    oConnection.db.M_DIAGNOSA_SNOMED_CTs.DeleteOnSubmit(ds)
                    oConnection.db.M_DIAGNOSA_SNOMED_CTs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal sKDDIAGNOSA As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDDIAGNOSA
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.M_DIAGNOSA_SNOMED_CTs.FirstOrDefault(Function(x) x.KDDIAGNOSA = sKDDIAGNOSA)

                Try
                    oConnection.db.M_DIAGNOSA_SNOMED_CTs.DeleteOnSubmit(ds)
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