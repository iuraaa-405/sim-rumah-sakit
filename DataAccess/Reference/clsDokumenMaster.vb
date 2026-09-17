Imports DataAccess.My.Resources

Namespace Reference
    Public Class clsDokumenMaster
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

            sMODUL = "DOKUMENMASTER"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As A_DOKUMEN_MASTER
            If Not oConnection.GetConnectionRME() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New A_DOKUMEN_MASTER
        End Function
        Public Function GetData() As List(Of A_DOKUMEN_MASTER)
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.A_DOKUMEN_MASTERs.OrderBy(Function(x) x.MEMO).ToList()
        End Function
        Public Function GetData(ByVal sKDDKUMEN As String) As A_DOKUMEN_MASTER
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.A_DOKUMEN_MASTERs.FirstOrDefault(Function(x) x.KDDKUMEN = sKDDKUMEN)
        End Function
        Public Function InsertData(ByVal entity As A_DOKUMEN_MASTER) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDDKUMEN
                sSTATUS = "INSERT"

                'If CheckDefault(0, entity.ISDEFAULT) = False Then
                '    InsertData = False
                '    Exit Function
                'End If

                'Generate Auto Number
                'Try
                '    sLASTNUMBER = CInt(oConnection.dbRME.A_DOKUMEN_MASTERs.OrderByDescending(Function(x) x.KDDKUMEN).FirstOrDefault().KDDKUMEN.Remove(0, (sMODUL & " _ ").Length)) + 1
                'Catch ex As Exception
                '    sLASTNUMBER = 1
                'End Try
                'End Generate

                Try
                    'entity.KDDKUMEN = sMODUL & "_" & AutoNumberCode(sLASTNUMBER)
                    oConnection.dbRME.A_DOKUMEN_MASTERs.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal entity As A_DOKUMEN_MASTER) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDDKUMEN
                sSTATUS = "UPDATE"

                'If CheckDefault(0, entity.ISDEFAULT) = False Then
                '    UpdateData = False
                '    Exit Function
                'End If

                Dim ds = oConnection.dbRME.A_DOKUMEN_MASTERs.FirstOrDefault(Function(x) x.KDDKUMEN = entity.KDDKUMEN)

                Try
                    oConnection.dbRME.A_DOKUMEN_MASTERs.DeleteOnSubmit(ds)
                    oConnection.dbRME.A_DOKUMEN_MASTERs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal sKDDKUMEN As String) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDDKUMEN
                sSTATUS = "DELETE"

                'If CheckDefault(1, 0) = False Then
                '    DeleteData = False
                '    Exit Function
                'End If

                Dim ds = oConnection.dbRME.A_DOKUMEN_MASTERs.FirstOrDefault(Function(x) x.KDDKUMEN = sKDDKUMEN)

                Try
                    oConnection.dbRME.A_DOKUMEN_MASTERs.DeleteOnSubmit(ds)
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
                    Dim ds = oConnection.dbRME.A_DOKUMEN_MASTERs.Where(Function(x) x.ISDEFAULT = True)
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
                    Dim ds = oConnection.dbRME.A_DOKUMEN_MASTERs.Where(Function(x) x.ISDEFAULT = True)

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
    End Class
End Namespace