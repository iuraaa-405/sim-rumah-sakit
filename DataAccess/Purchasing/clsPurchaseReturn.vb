Imports System.Threading

Namespace Purchasing
    Public Class clsPurchaseReturn
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public oItem As Reference.clsItem = Nothing
        Public oVendor As Reference.clsVendor = Nothing
        Public oAverage As Accounting.clsStockCard = Nothing
        Public oCounter As Setting.clsCounter = Nothing
        Public oJournal As Accounting.clsJournal = Nothing

        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0
        Public sKDITEM As New List(Of String)

        Public Sub New(Optional ByVal sConnection As String = "")
            If sConnection = "" Then
                oConnection = New Setting.clsConnectionMain
                oError = New Setting.clsError
                oItem = New Reference.clsItem
                oVendor = New Reference.clsVendor
                oJournal = New Accounting.clsJournal
                oAverage = New Accounting.clsStockCard
                oCounter = New Setting.clsCounter
            Else
                oConnection = New Setting.clsConnectionMain("TAX")
                oError = New Setting.clsError("TAX")
                oItem = New Reference.clsItem("TAX")
                oVendor = New Reference.clsVendor("TAX")
                oJournal = New Accounting.clsJournal("TAX")
                oAverage = New Accounting.clsStockCard("TAX")
                oCounter = New Setting.clsCounter("TAX")
            End If

            sMODUL = "PR"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As P_PR_H
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New P_PR_H
        End Function
        Public Function GetStructureDetail() As P_PR_D
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New P_PR_D
        End Function
        Public Function GetStructureDetailList() As List(Of P_PR_D)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of P_PR_D)
        End Function
        Public Function GetData() As List(Of P_PR_H)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.P_PR_Hs.OrderByDescending(Function(x) x.KDPR).ToList()
        End Function
        Public Function GetData(ByVal sKDPR As String, Optional ByVal isTAX As Boolean = False) As P_PR_H
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            If isTAX = False Then
                GetData = oConnection.db.P_PR_Hs.FirstOrDefault(Function(x) x.KDPR = sKDPR)
            Else
                GetData = oConnection.db.P_PR_Hs.FirstOrDefault(Function(x) x.MEMO = sKDPR)
            End If
        End Function
        Public Function GetDataDetail() As List(Of P_PR_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.P_PR_Ds.ToList()
        End Function
        Public Function GetDataDetail(ByVal sKDPR As String) As List(Of P_PR_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.P_PR_Ds.Where(Function(x) x.KDPR = sKDPR).ToList()
        End Function
        Public Function GetDataLastPrice(ByVal sKDVENDOR As String, ByVal sKDITEM As String, ByVal sKDUOM As String) As Decimal
            If Not oConnection.GetConnection() Then
                GetDataLastPrice = 0
                Exit Function
            End If
            Try
                GetDataLastPrice = (From x In (From x In oConnection.db.P_PR_Hs _
                                   Join y In oConnection.db.P_PR_Ds _
                                   On x.KDPR Equals y.KDPR _
                                   Where x.KDVENDOR = sKDVENDOR And y.KDITEM = sKDITEM And y.KDUOM = sKDUOM _
                                   Select x.KDPR, y.PRICE _
                                   Order By KDPR Descending) _
                                   Select x.PRICE).FirstOrDefault
            Catch ex As Exception
                GetDataLastPrice = 0
            End Try
        End Function
        Public Function InsertData(ByVal entity As P_PR_H, ByVal entityDetail As List(Of P_PR_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPR
                sSTATUS = "INSERT"

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

                    entity.KDPR = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)
                    For Each iLoop In entityDetail
                        iLoop.KDPR = entity.KDPR
                    Next
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.P_PR_Hs.InsertOnSubmit(entity)
                    oConnection.db.P_PR_Ds.InsertAllOnSubmit(entityDetail)
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
                    For Each iLoop In entityDetail
                        Dim sKDITEM = iLoop.KDITEM
                        Dim sKDUOM = iLoop.KDUOM

                        If oItem.GetDataDetail_WAREHOUSE(sKDITEM, entity.KDWAREHOUSE, sKDUOM) IsNot Nothing Then
                            Dim dsStock = oConnection.db.M_ITEM_WAREHOUSEs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM And x.KDWAREHOUSE = entity.KDWAREHOUSE And x.KDUOM = sKDUOM)
                            dsStock.AMOUNT -= iLoop.QTY

                            oConnection.db.SubmitChanges()
                        Else
                            Dim dsStock As New M_ITEM_WAREHOUSE
                            With dsStock
                                .DATECREATED = entity.DATECREATED
                                .DATEUPDATED = entity.DATEUPDATED
                                .KDWAREHOUSE = entity.KDWAREHOUSE
                                .KDITEM = sKDITEM
                                .KDUOM = sKDUOM
                                .AMOUNT = -iLoop.QTY
                            End With

                            oConnection.db.M_ITEM_WAREHOUSEs.InsertOnSubmit(dsStock)
                            oConnection.db.SubmitChanges()
                        End If
                    Next
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                'Try
                '    For Each iLoop In entityDetail
                '        sKDITEM.Add(iLoop.KDITEM)
                '    Next
                '    Dim oAverage As New Accounting.clsStockCard
                '    oAverage.PostingAverage(sKDITEM)
                'Catch ex As Exception
                '    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                '    Throw ex
                'End Try
                'Try
                '    If Not AutoJournal(entity, entityDetail, True) Then
                '        Return False
                '    End If
                'Catch ex As Exception
                '    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                '    Throw ex
                'End Try
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
            Finally
                oConnection.db.Dispose()
            End Try
        End Function
        Public Function UpdateData(ByVal entity As P_PR_H, ByVal entityDetail As List(Of P_PR_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPR
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.P_PR_Hs.FirstOrDefault(Function(x) x.KDPR = entity.KDPR)

                Try
                    oConnection.db.P_PR_Hs.DeleteOnSubmit(ds)
                    oConnection.db.P_PR_Hs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail = oConnection.db.P_PR_Ds.Where(Function(x) x.KDPR = entity.KDPR)

                Try
                    For Each iLoop In dsDetail
                        Dim sKDITEM = iLoop.KDITEM
                        Dim sKDUOM = iLoop.KDUOM

                        If oItem.GetDataDetail_WAREHOUSE(sKDITEM, ds.KDWAREHOUSE, sKDUOM) IsNot Nothing Then
                            Dim dsStock = oConnection.db.M_ITEM_WAREHOUSEs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM And x.KDWAREHOUSE = ds.KDWAREHOUSE And x.KDUOM = sKDUOM)
                            dsStock.AMOUNT += iLoop.QTY

                            oConnection.db.SubmitChanges()
                        Else
                            Dim dsStock As New M_ITEM_WAREHOUSE
                            With dsStock
                                .DATECREATED = entity.DATECREATED
                                .DATEUPDATED = entity.DATEUPDATED
                                .KDWAREHOUSE = ds.KDWAREHOUSE
                                .KDITEM = sKDITEM
                                .KDUOM = sKDUOM
                                .AMOUNT = iLoop.QTY
                            End With

                            oConnection.db.M_ITEM_WAREHOUSEs.InsertOnSubmit(dsStock)
                            oConnection.db.SubmitChanges()
                        End If
                    Next
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    For Each iLoop In dsDetail
                        sKDITEM.Add(iLoop.KDITEM)
                    Next
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.P_PR_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.P_PR_Ds.InsertAllOnSubmit(entityDetail)
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
                    For Each iLoop In entityDetail
                        Dim sKDITEM = iLoop.KDITEM
                        Dim sKDUOM = iLoop.KDUOM

                        If oItem.GetDataDetail_WAREHOUSE(sKDITEM, entity.KDWAREHOUSE, sKDUOM) IsNot Nothing Then
                            Dim dsStock = oConnection.db.M_ITEM_WAREHOUSEs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM And x.KDWAREHOUSE = entity.KDWAREHOUSE And x.KDUOM = sKDUOM)
                            dsStock.AMOUNT -= iLoop.QTY

                            oConnection.db.SubmitChanges()
                        Else
                            Dim dsStock As New M_ITEM_WAREHOUSE
                            With dsStock
                                .DATECREATED = entity.DATECREATED
                                .DATEUPDATED = entity.DATEUPDATED
                                .KDWAREHOUSE = entity.KDWAREHOUSE
                                .KDITEM = sKDITEM
                                .KDUOM = sKDUOM
                                .AMOUNT = -iLoop.QTY
                            End With

                            oConnection.db.M_ITEM_WAREHOUSEs.InsertOnSubmit(dsStock)
                            oConnection.db.SubmitChanges()
                        End If
                    Next
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                'Try
                '    For Each iLoop In entityDetail
                '        sKDITEM.Add(iLoop.KDITEM)
                '    Next
                '    Dim oAverage As New Accounting.clsStockCard
                '    oAverage.PostingAverage(sKDITEM)
                'Catch ex As Exception
                '    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                '    Throw ex
                'End Try
                'Try
                '    If Not AutoJournal(entity, entityDetail, False) Then
                '        Return False
                '    End If
                'Catch ex As Exception
                '    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                '    Throw ex
                'End Try

                UpdateData = True
            Catch ex As Exception
                UpdateData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            Finally
                oConnection.db.Dispose()
            End Try
        End Function
        Public Function DeleteData(ByVal sKDPR As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDPR
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.P_PR_Hs.FirstOrDefault(Function(x) x.KDPR = sKDPR)
                Dim dsDetail = oConnection.db.P_PR_Ds.Where(Function(x) x.KDPR = sKDPR)

                Try
                    oConnection.db.P_PR_Hs.DeleteOnSubmit(ds)
                    oConnection.db.P_PR_Ds.DeleteAllOnSubmit(dsDetail)

                    'ds.ISDELETE = True
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    For Each iLoop In dsDetail
                        Dim sKDITEM = iLoop.KDITEM
                        Dim sKDUOM = iLoop.KDUOM

                        Dim dsStock = oConnection.db.M_ITEM_WAREHOUSEs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM And x.KDWAREHOUSE = ds.KDWAREHOUSE And x.KDUOM = sKDUOM)

                        dsStock.AMOUNT += iLoop.QTY
                    Next
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    For Each iLoop In dsDetail
                        sKDITEM.Add(iLoop.KDITEM)
                    Next
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
                'Try
                '    Dim oAverage As New Accounting.clsStockCard
                '    oAverage.PostingAverage(sKDITEM)
                'Catch ex As Exception
                '    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                '    Throw ex
                'End Try
                'Try
                '    Dim oJournal As New Accounting.clsJournal
                '    oJournal.DeleteData(sKDPR)
                'Catch ex As Exception
                '    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                '    Throw ex
                'End Try

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            Finally
                oConnection.db.Dispose()
            End Try
        End Function
        Public Function AutoJournal(ByVal entity As P_PR_H, ByVal entityDetail As List(Of P_PR_D), ByVal IsNew As Boolean) As Boolean
            Try
                sREFERENCE = entity.KDPR
                sSTATUS = "AUTOJOURNAL"

                Dim dsVendor = oVendor.GetData(entity.KDVENDOR)

                Dim dsJournal_H As New A_JOURNAL_H
                With dsJournal_H
                    .DATECREATED = entity.DATECREATED
                    .DATEUPDATED = entity.DATEUPDATED
                    .KDJOURNAL = entity.KDPR
                    .DATE = entity.DATE
                    .MEMO = "INVOICE NO. : " & entity.KDPR & ", VENDOR : " & dsVendor.NAME_DISPLAY
                    .ISAUTO = True
                    .KDUSER = entity.KDUSER
                End With

                Dim sSeq As Integer = 0
                Dim sQtyTotal = entityDetail.Sum(Function(x) x.QTY)

                Dim arrJournal_D As New List(Of A_JOURNAL_D)
                For Each iLoop In entityDetail
                    Try
                        Dim dsJournal_D1 As New A_JOURNAL_D
                        Dim dsItem = oItem.GetData(iLoop.KDITEM)

                        With dsJournal_D1
                            .DATECREATED = entity.DATECREATED
                            .DATEUPDATED = entity.DATEUPDATED
                            .KDJOURNAL = dsJournal_H.KDJOURNAL
                            .KDCOA = dsItem.KDCOA_INVENTORY
                            .DEBIT = 0
                            .CREDIT = iLoop.GRANDTOTAL - ((entity.DISCOUNT / sQtyTotal) * iLoop.QTY) + ((entity.TAX / sQtyTotal) * iLoop.QTY)
                            .SEQ = sSeq
                        End With

                        arrJournal_D.Add(dsJournal_D1)
                        sSeq += 1
                    Catch ex As Exception
                        Throw ex
                    End Try
                Next

                Dim dsJournal_D2 As New A_JOURNAL_D
                With dsJournal_D2
                    .DATECREATED = entity.DATECREATED
                    .DATEUPDATED = entity.DATEUPDATED
                    .KDJOURNAL = dsJournal_H.KDJOURNAL
                    .KDCOA = dsVendor.KDCOA
                    .DEBIT = entity.GRANDTOTAL
                    .CREDIT = 0
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

                Dim entity = oConnection.db.P_PR_Hs

                For Each iLoop In entity
                    Dim sKDPR = iLoop.KDPR
                    Dim entityDetail = oConnection.db.P_PR_Ds.Where(Function(x) x.KDPR = sKDPR)

                    Dim dsDetail = oConnection.db.P_PR_Ds.Where(Function(x) x.QTY = 0)

                    Try
                        oConnection.db.P_PR_Ds.DeleteAllOnSubmit(dsDetail)
                        oConnection.db.SubmitChanges()
                    Catch ex As Exception
                        MsgBox(ex)
                    End Try

                    Try
                        If Not AutoJournal(iLoop, entityDetail.ToList, isNew) Then
                            Return False
                        End If
                    Catch ex As Exception
                        MsgBox(ex)
                    End Try

                    Thread.Sleep(100)
                Next

                UpdateDataFix = True
            Catch ex As Exception
                UpdateDataFix = False
                MsgBox(ex)
            Finally
                oConnection.db.Dispose()
            End Try
        End Function
    End Class
End Namespace