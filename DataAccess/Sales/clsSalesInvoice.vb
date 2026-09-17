Imports System.Threading
Imports System.Linq

Namespace Sales
    Public Class clsSalesInvoice
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public oItem As Reference.clsItem = Nothing
        Public oCustomer As Reference.clsCustomer = Nothing
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
                oCustomer = New Reference.clsCustomer
                oJournal = New Accounting.clsJournal
                oAverage = New Accounting.clsStockCard
                oCounter = New Setting.clsCounter
            Else
                oConnection = New Setting.clsConnectionMain("TAX")
                oError = New Setting.clsError("TAX")
                oItem = New Reference.clsItem("TAX")
                oCustomer = New Reference.clsCustomer("TAX")
                oJournal = New Accounting.clsJournal("TAX")
                oAverage = New Accounting.clsStockCard("TAX")
                oCounter = New Setting.clsCounter("TAX")
            End If

            sMODUL = "SI"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_SI_H
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_SI_H
        End Function
        Public Function GetStructureDetail() As S_SI_D
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New S_SI_D
        End Function
        Public Function GetStructureDetailList() As List(Of S_SI_D)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of S_SI_D)
        End Function
        Public Function GetData() As List(Of S_SI_H)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_SI_Hs.OrderByDescending(Function(x) x.KDSI).ToList()
        End Function
        Public Function GetData(ByVal sKDSI As String, Optional ByVal isTAX As Boolean = False) As S_SI_H
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            If isTAX = False Then
                GetData = oConnection.db.S_SI_Hs.FirstOrDefault(Function(x) x.KDSI = sKDSI)
            Else
                GetData = oConnection.db.S_SI_Hs.FirstOrDefault(Function(x) x.MEMO = sKDSI)
            End If
        End Function
        Public Function GetDataDetail() As List(Of S_SI_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_SI_Ds.ToList()
        End Function
        Public Function GetDataDetail(ByVal sKDSI As String) As List(Of S_SI_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_SI_Ds.Where(Function(x) x.KDSI = sKDSI).ToList()
        End Function
        Public Function GetDataPayment() As List(Of S_SI_PAYMENT)
            If Not oConnection.GetConnection Then
                GetDataPayment = Nothing
                Exit Function
            End If
            GetDataPayment = oConnection.db.S_SI_PAYMENTs.ToList()
        End Function
        Public Function GetDataLastPrice(ByVal sKDCUSTOMER As String, ByVal sKDITEM As String, ByVal sKDUOM As String) As Decimal
            If Not oConnection.GetConnection() Then
                GetDataLastPrice = 0
                Exit Function
            End If
            Try
                GetDataLastPrice = (From x In (From x In oConnection.db.S_SI_Hs _
                                   Join y In oConnection.db.S_SI_Ds _
                                   On x.KDSI Equals y.KDSI _
                                   Where x.KDCUSTOMER = sKDCUSTOMER And y.KDITEM = sKDITEM And y.KDUOM = sKDUOM _
                                   Select x.KDSI, y.PRICE _
                                   Order By KDSI Descending) _
                                   Select x.PRICE).FirstOrDefault
            Catch ex As Exception
                GetDataLastPrice = 0
            End Try
        End Function
        Public Function InsertData(ByVal entity As S_SI_H, ByVal entityDetail As List(Of S_SI_D), Optional ByVal entityPayment As List(Of S_SI_PAYMENT) = Nothing) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDSI
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

                    If oConnection.db.S_SI_Hs.Where(Function(x) x.KDSI = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)).Count > 0 Then
                        sLASTNUMBER = sLASTNUMBER + 1
                    End If

                    entity.KDSI = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)



                    For Each iLoop In entityDetail
                        iLoop.KDSI = entity.KDSI
                    Next
                    If entityPayment IsNot Nothing Then
                        For Each iLoop In entityPayment
                            iLoop.KDSI = entity.KDSI
                        Next
                    End If
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.S_SI_Hs.InsertOnSubmit(entity)
                    oConnection.db.S_SI_Ds.InsertAllOnSubmit(entityDetail)

                    If entityPayment IsNot Nothing Then
                        oConnection.db.S_SI_PAYMENTs.InsertAllOnSubmit(entityPayment)
                    End If
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
                Try
                    For Each iLoop In entityDetail
                        sKDITEM.Add(iLoop.KDITEM)
                    Next
                    Dim oAverage As New Accounting.clsStockCard
                    oAverage.PostingAverage(sKDITEM)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    If Not AutoJournal(entity, entityDetail, True) Then
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
            Finally
                oConnection.db.Dispose()
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_SI_H, ByVal entityDetail As List(Of S_SI_D), Optional ByVal entityPayment As List(Of S_SI_PAYMENT) = Nothing) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDSI
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_SI_Hs.FirstOrDefault(Function(x) x.KDSI = entity.KDSI)

                Try
                    oConnection.db.S_SI_Hs.DeleteOnSubmit(ds)
                    oConnection.db.S_SI_Hs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail = oConnection.db.S_SI_Ds.Where(Function(x) x.KDSI = entity.KDSI)

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
                    oConnection.db.S_SI_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.S_SI_Ds.InsertAllOnSubmit(entityDetail)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsPayment = oConnection.db.S_SI_PAYMENTs.Where(Function(x) x.KDSI = entity.KDSI)

                Try
                    oConnection.db.S_SI_PAYMENTs.DeleteAllOnSubmit(dsPayment)

                    If entityPayment IsNot Nothing Then
                        oConnection.db.S_SI_PAYMENTs.InsertAllOnSubmit(entityPayment)
                    End If
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
                Try
                    For Each iLoop In entityDetail
                        sKDITEM.Add(iLoop.KDITEM)
                    Next
                    Dim oAverage As New Accounting.clsStockCard
                    oAverage.PostingAverage(sKDITEM)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    If Not AutoJournal(entity, entityDetail, False) Then
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
            Finally
                oConnection.db.Dispose()
            End Try
        End Function
        Public Function DeleteData(ByVal sKDSI As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDSI
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.S_SI_Hs.FirstOrDefault(Function(x) x.KDSI = sKDSI)
                Dim dsDetail = oConnection.db.S_SI_Ds.Where(Function(x) x.KDSI = sKDSI)
                Dim dsDetailPayment = oConnection.db.S_SI_PAYMENTs.Where(Function(x) x.KDSI = sKDSI)

                Try
                    oConnection.db.S_SI_Hs.DeleteOnSubmit(ds)
                    oConnection.db.S_SI_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.S_SI_PAYMENTs.DeleteAllOnSubmit(dsDetailPayment)

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
                Try
                    Dim oAverage As New Accounting.clsStockCard
                    oAverage.PostingAverage(sKDITEM)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    Dim oJournal As New Accounting.clsJournal
                    oJournal.DeleteData(sKDSI)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            Finally
                oConnection.db.Dispose()
            End Try
        End Function
        Public Function AutoJournal(ByVal entity As S_SI_H, ByVal entityDetail As List(Of S_SI_D), ByVal IsNew As Boolean) As Boolean
            Try
                sREFERENCE = entity.KDSI
                sSTATUS = "AUTOJOURNAL"

                Dim dsCustomer = oCustomer.GetData(entity.KDCUSTOMER)

                Dim dsJournal_H As New A_JOURNAL_H
                With dsJournal_H
                    .DATECREATED = entity.DATECREATED
                    .DATEUPDATED = entity.DATEUPDATED
                    .KDJOURNAL = entity.KDSI
                    .DATE = entity.DATE
                    .MEMO = "INVOICE NO. : " & entity.KDSI & ", CUSTOMER : " & dsCustomer.NAME_DISPLAY
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
                            .KDCOA = dsItem.KDCOA_SALES
                            .DEBIT = 0
                            .CREDIT = iLoop.GRANDTOTAL
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
                    .KDCOA = dsCustomer.KDCOA
                    .DEBIT = entity.GRANDTOTAL
                    .CREDIT = 0
                    .SEQ = sSeq
                End With

                arrJournal_D.Add(dsJournal_D2)
                sSeq += 1

                If entity.DISCOUNT <> 0 Then
                    Dim dsJournal_D3 As New A_JOURNAL_D
                    With dsJournal_D3
                        .DATECREATED = entity.DATECREATED
                        .DATEUPDATED = entity.DATEUPDATED
                        .KDJOURNAL = dsJournal_H.KDJOURNAL
                        .KDCOA = oConnection.db.SET_SETTINGs.FirstOrDefault().COA_SALES_DISCOUNT
                        .DEBIT = entity.DISCOUNT
                        .CREDIT = 0
                        .SEQ = sSeq
                    End With

                    arrJournal_D.Add(dsJournal_D3)
                    sSeq += 1
                End If

                For Each iLoop In entityDetail
                    Dim dsItem = oItem.GetData(iLoop.KDITEM)
                    Dim sRATE = oItem.GetDataRate(iLoop.KDITEM, iLoop.KDUOM)

                    Dim sSEQREF = iLoop.SEQ
                    Dim sPrice As Decimal = 0

                    Dim oStockCard As New Accounting.clsStockCard

                    Try
                        sPrice = oStockCard.GetData(iLoop.KDITEM, iLoop.KDUOM).OrderByDescending(Function(x) x.SEQ).FirstOrDefault(Function(x) x.NOREFERENCE = entity.KDSI And x.SEQREF = sSEQREF).HPPAVERAGE
                    Catch ex As Exception
                        sPrice = 0
                    End Try

                    Dim dsJournal_D4 As New A_JOURNAL_D
                    With dsJournal_D4
                        .DATECREATED = entity.DATECREATED
                        .DATEUPDATED = entity.DATEUPDATED
                        .KDJOURNAL = dsJournal_H.KDJOURNAL
                        .KDCOA = dsItem.KDCOA_COST
                        .DEBIT = CDec((iLoop.QTY * sRATE) * sPrice)
                        .CREDIT = 0
                        .SEQ = sSeq
                    End With

                    arrJournal_D.Add(dsJournal_D4)
                    sSeq += 1

                    Dim dsJournal_D5 As New A_JOURNAL_D
                    With dsJournal_D5
                        .DATECREATED = entity.DATECREATED
                        .DATEUPDATED = entity.DATEUPDATED
                        .KDJOURNAL = dsJournal_H.KDJOURNAL
                        .KDCOA = dsItem.KDCOA_INVENTORY
                        .DEBIT = 0
                        .CREDIT = CDec((iLoop.QTY * sRATE) * sPrice)
                        .SEQ = sSeq
                    End With

                    arrJournal_D.Add(dsJournal_D5)
                    sSeq += 1
                Next

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

                Dim entity = oConnection.db.S_SI_Hs.OrderBy(Function(x) x.KDSI).Where(Function(x) x.DATE.Year >= 2015)

                For Each iLoop In entity
                    Dim sKDSI = iLoop.KDSI
                    Dim entityDetail = oConnection.db.S_SI_Ds.Where(Function(x) x.KDSI = sKDSI)

                    Dim dsDetail = oConnection.db.S_SI_Ds.Where(Function(x) x.QTY = 0)

                    Try
                        oConnection.db.S_SI_Ds.DeleteAllOnSubmit(dsDetail)
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