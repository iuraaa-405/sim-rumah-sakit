Namespace Accounting
    Public Class clsCOA
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing

        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""

        Public Sub New(Optional ByVal sConnection As String = "")
            If sConnection = "" Then
                oConnection = New Setting.clsConnectionMain
                oError = New Setting.clsError
            Else
                oConnection = New Setting.clsConnectionMain("TAX")
                oError = New Setting.clsError("TAX")
            End If

            sMODUL = "COA"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As A_COA
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New A_COA
        End Function
        Public Function GetData() As List(Of A_COA)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.A_COAs.OrderBy(Function(x) x.KDCOA).ToList()
        End Function
        Public Function GetData(ByVal sKDCOA As String) As A_COA
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.A_COAs.FirstOrDefault(Function(x) x.KDCOA = sKDCOA)
        End Function
        Public Function GetDataBalance() As List(Of A_COA_BALANCE)
            If Not oConnection.GetConnection() Then
                GetDataBalance = Nothing
                Exit Function
            End If
            GetDataBalance = oConnection.db.A_COA_BALANCEs.ToList()
        End Function
        Public Function GetDataBalance(ByVal sKDCOA As String, ByVal sKDCOA1 As String) As A_COA_BALANCE
            If Not oConnection.GetConnection() Then
                GetDataBalance = Nothing
                Exit Function
            End If
            GetDataBalance = oConnection.db.A_COA_BALANCEs.FirstOrDefault(Function(x) x.KDCOA = sKDCOA And x.YEAR = sKDCOA1)
        End Function
        Public Function GetProfitLossCurrent() As A_COA
            If Not oConnection.GetConnection() Then
                GetProfitLossCurrent = Nothing
                Exit Function
            End If
            Dim sKDCOA = oConnection.db.SET_SETTINGs.FirstOrDefault().COA_MONTH
            GetProfitLossCurrent = oConnection.db.A_COAs.FirstOrDefault(Function(x) x.KDCOA = sKDCOA)
        End Function
        Public Function GetProfitLossRetained() As A_COA
            If Not oConnection.GetConnection() Then
                GetProfitLossRetained = Nothing
                Exit Function
            End If
            Dim sKDCOA = oConnection.db.SET_SETTINGs.FirstOrDefault().COA_YEAR
            GetProfitLossRetained = oConnection.db.A_COAs.FirstOrDefault(Function(x) x.KDCOA = sKDCOA)
        End Function
        Public Function IsExist(ByVal sKDCOA As String) As Boolean
            If Not oConnection.GetConnection() Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.db.A_COAs.FirstOrDefault(Function(x) x.KDCOA = sKDCOA)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        Public Function InsertData(ByVal entity As A_COA) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDCOA
                sSTATUS = "INSERT"

                Try
                    oConnection.db.A_COAs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsBalance As New A_COA_BALANCE

                Try
                    With dsBalance
                        .DATECREATED = entity.DATECREATED
                        .DATEUPDATED = entity.DATEUPDATED
                        .KDCOA = entity.KDCOA
                        .BBALANCE = 0
                        .EBALANCE = 0
                        .SALDO01 = 0
                        .SALDO02 = 0
                        .SALDO03 = 0
                        .SALDO04 = 0
                        .SALDO05 = 0
                        .SALDO06 = 0
                        .SALDO07 = 0
                        .SALDO08 = 0
                        .SALDO09 = 0
                        .SALDO10 = 0
                        .SALDO11 = 0
                        .SALDO12 = 0
                        .YEAR = Year(Now)
                    End With
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.A_COA_BALANCEs.InsertOnSubmit(dsBalance)
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
        Public Function UpdateData(ByVal entity As A_COA) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDCOA
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.A_COAs.FirstOrDefault(Function(x) x.KDCOA = entity.KDCOA)

                Try
                    oConnection.db.A_COAs.DeleteOnSubmit(ds)
                    oConnection.db.A_COAs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal sKDCOA As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDCOA
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.A_COAs.FirstOrDefault(Function(x) x.KDCOA = sKDCOA)

                Try
                    oConnection.db.A_COAs.DeleteOnSubmit(ds)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsBalance = oConnection.db.A_COA_BALANCEs.Where(Function(x) x.KDCOA = sKDCOA)

                Try
                    oConnection.db.A_COA_BALANCEs.DeleteAllOnSubmit(dsBalance)
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
        Public Function PostingYear() As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    PostingYear = False
                    Exit Function
                End If

                Dim ds = oConnection.db.A_COA_BALANCEs.Where(Function(x) x.A_COA.GROUPS <> 3 And x.A_COA.GROUPS <> 4 And x.A_COA.GROUPS <> 5).OrderBy(Function(x) x.YEAR)
                Dim sCOABulanan = GetProfitLossCurrent().KDCOA
                Dim sCOATahunan = GetProfitLossRetained.KDCOA

                For Each iLoop In ds
                    If iLoop.YEAR - Year(Now) > 3 Then
                        Exit For
                    End If

                    Dim sYear = iLoop.YEAR + 1
                    With iLoop
                        .EBALANCE = .BBALANCE + .SALDO01 + .SALDO02 + .SALDO03 + .SALDO04 + .SALDO05 + .SALDO06 + .SALDO07 + .SALDO08 + .SALDO09 + .SALDO10 + .SALDO11 + .SALDO12

                        If .KDCOA <> sCOABulanan And .KDCOA <> sCOATahunan Then
                            Dim dsBalance = oConnection.db.A_COA_BALANCEs.FirstOrDefault(Function(x) x.KDCOA = .KDCOA And x.YEAR = sYear)

                            If dsBalance Is Nothing Then
                                Dim dsBalanceNew As New A_COA_BALANCE
                                With dsBalanceNew
                                    .KDCOA = iLoop.KDCOA
                                    .BBALANCE = iLoop.EBALANCE
                                    .EBALANCE = 0
                                    .SALDO01 = 0
                                    .SALDO02 = 0
                                    .SALDO03 = 0
                                    .SALDO04 = 0
                                    .SALDO05 = 0
                                    .SALDO06 = 0
                                    .SALDO07 = 0
                                    .SALDO08 = 0
                                    .SALDO09 = 0
                                    .SALDO10 = 0
                                    .SALDO11 = 0
                                    .SALDO12 = 0
                                    .YEAR = sYear
                                End With

                                oConnection.db.A_COA_BALANCEs.InsertOnSubmit(dsBalanceNew)
                            Else
                                dsBalance.BBALANCE = iLoop.EBALANCE
                            End If
                        ElseIf .KDCOA = sCOABulanan Then
                            Dim dsBalance = oConnection.db.A_COA_BALANCEs.FirstOrDefault(Function(x) x.KDCOA = sCOATahunan And x.YEAR = sYear)

                            If dsBalance Is Nothing Then
                                Dim dsBalanceNew As New A_COA_BALANCE
                                With dsBalanceNew
                                    .KDCOA = sCOATahunan
                                    .BBALANCE = iLoop.EBALANCE
                                    .EBALANCE = 0
                                    .SALDO01 = 0
                                    .SALDO02 = 0
                                    .SALDO03 = 0
                                    .SALDO04 = 0
                                    .SALDO05 = 0
                                    .SALDO06 = 0
                                    .SALDO07 = 0
                                    .SALDO08 = 0
                                    .SALDO09 = 0
                                    .SALDO10 = 0
                                    .SALDO11 = 0
                                    .SALDO12 = 0
                                    .YEAR = sYear
                                End With

                                oConnection.db.A_COA_BALANCEs.InsertOnSubmit(dsBalanceNew)
                            Else
                                dsBalance.BBALANCE = iLoop.EBALANCE
                            End If
                        End If
                    End With
                Next

                oConnection.db.SubmitChanges()
                PostingYear = True
            Catch ex As Exception
                PostingYear = False
                Throw ex
            End Try
        End Function
    End Class
End Namespace