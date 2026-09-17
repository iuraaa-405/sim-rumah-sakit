Namespace Finance
    Public Class clsCash
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
            sMODUL = "CS"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As F_CASH
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New F_CASH
        End Function
        Public Function GetData() As List(Of F_CASH)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.F_CASHes.OrderByDescending(Function(x) x.KDCASH).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As F_CASH
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.F_CASHes.FirstOrDefault(Function(x) x.KDCASH = Parameter)
        End Function
        Public Function InsertData(ByVal entity As F_CASH, Optional ByVal isAuto As Boolean = False) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDCASH
                sSTATUS = "INSERT"

                If isAuto <> True Then
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

                        entity.KDCASH = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try
                End If
                Try
                    oConnection.db.F_CASHes.InsertOnSubmit(entity)
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
                    If Not AutoJournal(entity, True) Then
                        Return False
                    End If
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
            End Try
        End Function
        Public Function UpdateData(ByVal entity As F_CASH) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDCASH
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.F_CASHes.FirstOrDefault(Function(x) x.KDCASH = entity.KDCASH)

                Try
                    oConnection.db.F_CASHes.DeleteOnSubmit(ds)
                    oConnection.db.F_CASHes.InsertOnSubmit(entity)
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
                    If Not AutoJournal(entity, False) Then
                        Return False
                    End If
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
        Public Function DeleteData(ByVal Parameter As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = Parameter
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.F_CASHes.Where(Function(x) x.KDCASH.Contains(Parameter))

                For Each xLoop In ds
                    Dim sNOCASH = xLoop.KDCASH

                    Try
                        oConnection.db.F_CASHes.DeleteOnSubmit(xLoop)
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
                        Dim oJournal As New Accounting.clsJournal
                        oJournal.DeleteData(sNOCASH)
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try
                Next

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function AutoJournal(ByVal entity As F_CASH, ByVal IsNew As Boolean) As Boolean
            Try
                sREFERENCE = entity.KDCASH
                sSTATUS = "AUTOJOURNAL"

                Dim oJournal As New Accounting.clsJournal
                Dim oPaymentType As New Reference.clsPaymentType

                Dim dsPaymentType = oPaymentType.GetData(entity.KDPAYMENTTYPE)

                Dim dsJournal_H As New A_JOURNAL_H
                With dsJournal_H
                    .DATECREATED = entity.DATECREATED
                    .DATEUPDATED = entity.DATEUPDATED
                    .KDJOURNAL = entity.KDCASH
                    .DATE = entity.DATE
                    .MEMO = "MEMO : " & entity.MEMO & ""
                    .ISAUTO = True
                    .KDUSER = entity.KDUSER
                End With

                Dim sSeq As Integer = 0

                Dim arrJournal_D As New List(Of A_JOURNAL_D)

                Dim dsJournal_D1 As New A_JOURNAL_D
                With dsJournal_D1
                    .DATECREATED = entity.DATECREATED
                    .DATEUPDATED = entity.DATEUPDATED
                    .KDJOURNAL = entity.KDCASH
                    .KDCOA = dsPaymentType.KDCOA

                    If entity.TOTAL > 0 Then
                        .DEBIT = 0
                        .CREDIT = Math.Abs(entity.TOTAL)
                    Else
                        .DEBIT = Math.Abs(entity.TOTAL)
                        .CREDIT = 0
                    End If

                    .SEQ = sSeq
                End With

                arrJournal_D.Add(dsJournal_D1)
                sSeq += 1

                Dim dsJournal_D2 As New A_JOURNAL_D
                With dsJournal_D2
                    .DATECREATED = entity.DATECREATED
                    .DATEUPDATED = entity.DATEUPDATED
                    .KDJOURNAL = entity.KDCASH

                    If entity.TOTAL > 0 Then
                        .KDCOA = entity.KDCOA
                        .DEBIT = Math.Abs(entity.TOTAL)
                        .CREDIT = 0
                    Else
                        .KDCOA = entity.KDCOA
                        .DEBIT = 0
                        .CREDIT = Math.Abs(entity.TOTAL)
                    End If

                    .SEQ = sSeq
                End With

                arrJournal_D.Add(dsJournal_D2)
                sSeq += 1

                If IsNew = True Then
                    oJournal.InsertData(dsJournal_H, arrJournal_D, True)
                Else
                    oJournal.UpdateData(dsJournal_H, arrJournal_D)
                End If

                AutoJournal = True
            Catch ex As Exception
                AutoJournal = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateDataFix(ByVal isNew As Boolean) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateDataFix = False
                    Exit Function
                End If


                Dim entity = oConnection.db.F_CASHes

                For Each iLoop In entity
                    Dim ds = oConnection.db.A_JOURNAL_Hs.Where(Function(x) x.KDJOURNAL = iLoop.KDCASH)

                    If ds Is Nothing Then
                        Dim sKDCASH = iLoop.KDCASH

                        Try
                            If Not AutoJournal(iLoop, isNew) Then
                                Return False
                            End If
                        Catch ex As Exception
                            Throw ex
                        End Try
                    End If
                Next

                UpdateDataFix = True
            Catch ex As Exception
                UpdateDataFix = False
                Throw ex
            End Try
        End Function
    End Class
End Namespace