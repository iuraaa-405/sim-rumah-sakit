Imports DataAccess.My.Resources

Namespace Reference
    Public Class clsBentukMakanan
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

            sMODUL = "BM"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As M_BENTUKMAKANAN
            If Not oConnection.GetConnectionRME() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New M_BENTUKMAKANAN
        End Function
        Public Function GetData() As List(Of M_BENTUKMAKANAN)
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.M_BENTUKMAKANANs.OrderBy(Function(x) x.MEMO).ToList()
        End Function
        Public Function GetData(ByVal sKDBENTUKMAKANAN As String) As M_BENTUKMAKANAN
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.M_BENTUKMAKANANs.FirstOrDefault(Function(x) x.KDBENTUKMAKANAN = sKDBENTUKMAKANAN)
        End Function
        Public Function GetDataSync() As List(Of M_BENTUKMAKANAN)
            If Not oConnection.GetConnectionRME() Then
                GetDataSync = Nothing
                Exit Function
            End If
            GetDataSync = oConnection.dbRME.M_BENTUKMAKANANs.OrderBy(Function(x) x.MEMO).ToList()
        End Function
        Public Function IsExist(ByVal sMEMO As String) As Boolean
            If Not oConnection.GetConnectionRME() Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.dbRME.M_BENTUKMAKANANs.FirstOrDefault(Function(x) x.MEMO = sMEMO)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        Public Function InsertData(ByVal entity As M_BENTUKMAKANAN) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDBENTUKMAKANAN
                sSTATUS = "INSERT"

                If CheckDefault(0, entity.ISDEFAULT) = False Then
                    InsertData = False
                    Exit Function
                End If

                'Generate Auto Number
                Try
                    sLASTNUMBER = CInt(oConnection.dbRME.M_BENTUKMAKANANs.OrderByDescending(Function(x) x.KDBENTUKMAKANAN).FirstOrDefault().KDBENTUKMAKANAN.Remove(0, (sMODUL & " _ ").Length)) + 1
                Catch ex As Exception
                    sLASTNUMBER = 1
                End Try
                'End Generate

                Try
                    entity.KDBENTUKMAKANAN = sMODUL & "_" & AutoNumberCode(sLASTNUMBER)
                    oConnection.dbRME.M_BENTUKMAKANANs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.dbRME.SubmitChanges()
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
        Public Function UpdateData(ByVal entity As M_BENTUKMAKANAN) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDBENTUKMAKANAN
                sSTATUS = "UPDATE"

                If CheckDefault(0, entity.ISDEFAULT) = False Then
                    UpdateData = False
                    Exit Function
                End If

                Dim ds = oConnection.dbRME.M_BENTUKMAKANANs.FirstOrDefault(Function(x) x.KDBENTUKMAKANAN = entity.KDBENTUKMAKANAN)

                Try
                    oConnection.dbRME.M_BENTUKMAKANANs.DeleteOnSubmit(ds)
                    oConnection.dbRME.M_BENTUKMAKANANs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.dbRME.SubmitChanges()
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
        Public Function DeleteData(ByVal sKDBENTUKMAKANAN As String) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDBENTUKMAKANAN
                sSTATUS = "DELETE"

                If CheckDefault(1, 0) = False Then
                    DeleteData = False
                    Exit Function
                End If

                Dim ds = oConnection.dbRME.M_BENTUKMAKANANs.FirstOrDefault(Function(x) x.KDBENTUKMAKANAN = sKDBENTUKMAKANAN)

                Try
                    oConnection.dbRME.M_BENTUKMAKANANs.DeleteOnSubmit(ds)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.dbRME.SubmitChanges()
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
        Public Function CheckDefault(ByVal sState As Integer, ByVal sDefault As Boolean) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    CheckDefault = False
                    Exit Function
                End If


                If sState = 0 Then
                    Dim ds = oConnection.dbRME.M_BENTUKMAKANANs.Where(Function(x) x.ISDEFAULT = True)
                    If sDefault = True Then
                        For Each iLoop In ds
                            iLoop.ISDEFAULT = False
                        Next
                    Else
                        If ds.Count < 1 Then
                            MsgBox(Statement.CheckDefault, MsgBoxStyle.Exclamation)
                            CheckDefault = False
                            Exit Function
                        End If
                    End If
                Else
                    Dim ds = oConnection.dbRME.M_BENTUKMAKANANs.Where(Function(x) x.ISDEFAULT = True)

                    If ds.Count < 1 Then
                        MsgBox(Statement.CheckDefault, MsgBoxStyle.Exclamation)
                        CheckDefault = False
                        Exit Function
                    End If
                End If
                CheckDefault = True
            Catch ex As Exception
                CheckDefault = False
                Throw ex
            End Try
        End Function
        Public Function AmbilDefault() As String
            Try
                If Not oConnection.GetConnectionRME() Then
                    AmbilDefault = ""
                    Exit Function
                End If

                Dim ds = oConnection.dbRME.M_BENTUKMAKANANs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    AmbilDefault = ds.KDBENTUKMAKANAN
                Else
                    AmbilDefault = String.Empty
                End If
            Catch ex As Exception
                AmbilDefault = String.Empty
                Throw ex
            End Try
        End Function
    End Class
End Namespace