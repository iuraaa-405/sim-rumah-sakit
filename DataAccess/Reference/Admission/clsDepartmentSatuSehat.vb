Namespace Reference
    Public Class clsDepartmentSatuSehat
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

            sMODUL = "DEPART"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As M_DEPARTMENT_SATUSEHAT
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New M_DEPARTMENT_SATUSEHAT
        End Function
        Public Function GetDataList() As List(Of M_DEPARTMENT_SATUSEHAT)
            If Not oConnection.GetConnection() Then
                GetDataList = Nothing
                Exit Function
            End If
            GetDataList = oConnection.db.M_DEPARTMENT_SATUSEHATs.OrderBy(Function(x) x.DATECREATED).ToList()
        End Function
        Public Function GetDataSatuSehatOrganisasiByKddepartment(ByVal sKDDEPARTMENT As String) As M_DEPARTMENT_SATUSEHAT
            If Not oConnection.GetConnection() Then
                GetDataSatuSehatOrganisasiByKddepartment = Nothing
                Exit Function
            End If
            GetDataSatuSehatOrganisasiByKddepartment = oConnection.db.M_DEPARTMENT_SATUSEHATs.FirstOrDefault(Function(x) x.KDDEPARTMENT = sKDDEPARTMENT)
        End Function
        Public Function GetDataSatuSehatOrganisasiIdSatuSehat(ByVal sIDSATUSEHAT As String) As M_DEPARTMENT_SATUSEHAT
            If Not oConnection.GetConnection() Then
                GetDataSatuSehatOrganisasiIdSatuSehat = Nothing
                Exit Function
            End If
            GetDataSatuSehatOrganisasiIdSatuSehat = oConnection.db.M_DEPARTMENT_SATUSEHATs.FirstOrDefault(Function(x) x.IDSATUSEHAT = sIDSATUSEHAT)
        End Function
        'Public Function IsExist(ByVal sKDDEPARTMENT As String) As Boolean
        '    If Not oConnection.GetConnection() Then
        '        IsExist = False
        '        Exit Function
        '    End If

        '    Dim ds = oConnection.db.M_DEPARTMENT_SATUSEHATs.FirstOrDefault(Function(x) x.KDDEPARTMENT = sKDDEPARTMENT)

        '    If ds IsNot Nothing Then
        '        IsExist = True
        '    Else
        '        IsExist = False
        '    End If
        'End Function
        Public Function InsertData(ByVal entity As M_DEPARTMENT_SATUSEHAT) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDDEPARTMENT
                sSTATUS = "INSERT"

                Try
                    oConnection.db.M_DEPARTMENT_SATUSEHATs.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal entity As M_DEPARTMENT_SATUSEHAT) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDDEPARTMENT
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.M_DEPARTMENT_SATUSEHATs.FirstOrDefault(Function(x) x.KDDEPARTMENT = entity.KDDEPARTMENT)

                Try
                    oConnection.db.M_DEPARTMENT_SATUSEHATs.DeleteOnSubmit(ds)
                    oConnection.db.M_DEPARTMENT_SATUSEHATs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal sKDDEPARTMENT As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDDEPARTMENT
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.M_DEPARTMENT_SATUSEHATs.FirstOrDefault(Function(x) x.KDDEPARTMENT = sKDDEPARTMENT)

                Try
                    oConnection.db.M_DEPARTMENT_SATUSEHATs.DeleteOnSubmit(ds)
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