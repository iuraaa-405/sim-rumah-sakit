Imports System.Threading

Namespace Inventory
    Public Class clsOpname
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0

        Public oCounter As Setting.clsCounter = Nothing

        Public sKDITEM As New List(Of String)

        Public Sub New()
            oConnection = New Setting.clsConnectionMain
            oError = New Setting.clsError
            sMODUL = "OPM"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As I_OPNAME_H
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New I_OPNAME_H
        End Function
        Public Function GetStructureDetail() As I_OPNAME_D
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New I_OPNAME_D
        End Function
        Public Function GetStructureDetailList() As List(Of I_OPNAME_D)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of I_OPNAME_D)
        End Function
        Public Function GetData() As List(Of I_OPNAME_H)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.I_OPNAME_Hs.OrderByDescending(Function(x) x.KDOPNAME).ToList()
        End Function
        Public Function GetData(ByVal sKDOPNAME As String) As I_OPNAME_H
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.I_OPNAME_Hs.FirstOrDefault(Function(x) x.KDOPNAME = sKDOPNAME)
        End Function
        Public Function GetDataDetail() As List(Of I_OPNAME_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.I_OPNAME_Ds.ToList()
        End Function
        Public Function GetDataDetail(ByVal sKDOPNAME As String) As List(Of I_OPNAME_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.I_OPNAME_Ds.Where(Function(x) x.KDOPNAME = sKDOPNAME).ToList()
        End Function
        Public Function GetDataOpnameTerakhir(ByVal sKDITEM As String, ByVal sKDUOM As String) As I_OPNAME_D
            If Not oConnection.GetConnection() Then
                GetDataOpnameTerakhir = Nothing
                Exit Function
            End If
            GetDataOpnameTerakhir = oConnection.db.I_OPNAME_Ds.Where(Function(x) x.KDITEM = sKDITEM And x.KDUOM = sKDUOM).OrderByDescending(Function(x) x.TANGGALEXPIRE).FirstOrDefault()
        End Function
        Public Function InsertData(ByVal entity As I_OPNAME_H, ByVal entityDetail As List(Of I_OPNAME_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDOPNAME
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

                    entity.KDOPNAME = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)
                    For Each iLoop In entityDetail
                        iLoop.KDOPNAME = entity.KDOPNAME
                    Next
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.I_OPNAME_Hs.InsertOnSubmit(entity)
                    oConnection.db.I_OPNAME_Ds.InsertAllOnSubmit(entityDetail)
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
                    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATE), Year(entity.DATE))
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try


                Try
                    For Each iLoop In entityDetail
                        Dim oItem As New Reference.clsItem
                        Dim sKDITEM = iLoop.KDITEM
                        Dim sKDUOM = iLoop.KDUOM

                        If oItem.GetDataDetail_WAREHOUSE(sKDITEM, entity.KDWAREHOUSE, sKDUOM) IsNot Nothing Then
                            Dim dsStock = oConnection.db.M_ITEM_WAREHOUSEs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM And x.KDWAREHOUSE = entity.KDWAREHOUSE And x.KDUOM = sKDUOM)
                            If iLoop.QTY > 0 Then
                                dsStock.AMOUNT += Math.Abs(iLoop.QTY)
                            Else
                                dsStock.AMOUNT -= Math.Abs(iLoop.QTY)
                            End If


                            oConnection.db.SubmitChanges()
                        Else
                            Dim dsStock As New M_ITEM_WAREHOUSE
                            With dsStock
                                .DATECREATED = entity.DATECREATED
                                .DATEUPDATED = entity.DATEUPDATED
                                .KDWAREHOUSE = entity.KDWAREHOUSE
                                .KDITEM = sKDITEM
                                .KDUOM = sKDUOM
                                If iLoop.QTY > 0 Then
                                    .AMOUNT = Math.Abs(iLoop.QTY)
                                Else
                                    .AMOUNT = -Math.Abs(iLoop.QTY)
                                End If
                            End With

                            oConnection.db.M_ITEM_WAREHOUSEs.InsertOnSubmit(dsStock)
                            oConnection.db.SubmitChanges()
                        End If
                    Next
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
        Public Function UpdateData(ByVal entity As I_OPNAME_H, ByVal entityDetail As List(Of I_OPNAME_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDOPNAME
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.I_OPNAME_Hs.FirstOrDefault(Function(x) x.KDOPNAME = entity.KDOPNAME)

                Try
                    oConnection.db.I_OPNAME_Hs.DeleteOnSubmit(ds)
                    oConnection.db.I_OPNAME_Hs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail = oConnection.db.I_OPNAME_Ds.Where(Function(x) x.KDOPNAME = entity.KDOPNAME)

                Try
                    For Each iLoop In dsDetail
                        Dim oItem As New Reference.clsItem
                        Dim sKDITEM = iLoop.KDITEM
                        Dim sKDUOM = iLoop.KDUOM

                        If oItem.GetDataDetail_WAREHOUSE(sKDITEM, ds.KDWAREHOUSE, sKDUOM) IsNot Nothing Then
                            Dim dsStock = oConnection.db.M_ITEM_WAREHOUSEs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM And x.KDWAREHOUSE = ds.KDWAREHOUSE And x.KDUOM = sKDUOM)
                            If iLoop.QTY > 0 Then
                                dsStock.AMOUNT -= Math.Abs(iLoop.QTY)
                            Else
                                dsStock.AMOUNT += Math.Abs(iLoop.QTY)
                            End If


                            oConnection.db.SubmitChanges()
                        Else
                            Dim dsStock As New M_ITEM_WAREHOUSE
                            With dsStock
                                .DATECREATED = entity.DATECREATED
                                .DATEUPDATED = entity.DATEUPDATED
                                .KDWAREHOUSE = ds.KDWAREHOUSE
                                .KDITEM = sKDITEM
                                .KDUOM = sKDUOM
                                If iLoop.QTY > 0 Then
                                    .AMOUNT = -Math.Abs(iLoop.QTY)
                                Else
                                    .AMOUNT = Math.Abs(iLoop.QTY)
                                End If

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
                    oConnection.db.I_OPNAME_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.I_OPNAME_Ds.InsertAllOnSubmit(entityDetail)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                'Try
                '    If Not AutoJournal(entity, entityDetail, False) Then
                '        Return False
                '    End If
                'Catch ex As Exception
                '    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                '    Throw ex
                'End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    For Each iLoop In entityDetail
                        Dim oItem As New Reference.clsItem
                        Dim sKDITEM = iLoop.KDITEM
                        Dim sKDUOM = iLoop.KDUOM

                        If oItem.GetDataDetail_WAREHOUSE(sKDITEM, entity.KDWAREHOUSE, sKDUOM) IsNot Nothing Then
                            Dim dsStock = oConnection.db.M_ITEM_WAREHOUSEs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM And x.KDWAREHOUSE = entity.KDWAREHOUSE And x.KDUOM = sKDUOM)

                            If iLoop.QTY > 0 Then
                                dsStock.AMOUNT += Math.Abs(iLoop.QTY)
                            Else
                                dsStock.AMOUNT -= Math.Abs(iLoop.QTY)
                            End If


                            oConnection.db.SubmitChanges()
                        Else
                            Dim dsStock As New M_ITEM_WAREHOUSE
                            With dsStock
                                .DATECREATED = entity.DATECREATED
                                .DATEUPDATED = entity.DATEUPDATED
                                .KDWAREHOUSE = entity.KDWAREHOUSE
                                .KDITEM = sKDITEM
                                .KDUOM = sKDUOM
                                If iLoop.QTY > 0 Then
                                    .AMOUNT = Math.Abs(iLoop.QTY)
                                Else
                                    .AMOUNT = -Math.Abs(iLoop.QTY)
                                End If
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

                UpdateData = True
            Catch ex As Exception
                UpdateData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function DeleteData(ByVal sKDOPNAME As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDOPNAME
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.I_OPNAME_Hs.FirstOrDefault(Function(x) x.KDOPNAME = sKDOPNAME)
                Dim dsDetail = oConnection.db.I_OPNAME_Ds.Where(Function(x) x.KDOPNAME = sKDOPNAME)

                Try
                    oConnection.db.I_OPNAME_Hs.DeleteOnSubmit(ds)
                    oConnection.db.I_OPNAME_Ds.DeleteAllOnSubmit(dsDetail)

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

                        If iLoop.QTY > 0 Then
                            dsStock.AMOUNT -= Math.Abs(iLoop.QTY)
                        Else
                            dsStock.AMOUNT += Math.Abs(iLoop.QTY)
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
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                'Try
                '    Dim oJournal As New Accounting.clsJournal
                '    oJournal.DeleteData(sKDOPNAME)
                'Catch ex As Exception
                '    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                '    Throw ex
                'End Try
                'Try
                '    Dim oAverage As New Accounting.clsStockCard
                '    oAverage.PostingAverage(sKDITEM)
                'Catch ex As Exception
                '    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                '    Throw ex
                'End Try

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        'Public Function AutoJournal(ByVal entity As I_OPNAME_H, ByVal entityDetail As List(Of I_OPNAME_D), ByVal IsNew As Boolean) As Boolean
        '    Try
        '        sREFERENCE = entity.KDOPNAME
        '        sSTATUS = "AUTOJOURNAL"

        '        Dim oJournal As New Accounting.clsJournal
        '        Dim oItem As New Reference.clsItem

        '        Dim dsJournal_H As New A_JOURNAL_H
        '        With dsJournal_H
        '            .DATECREATED = entity.DATECREATED
        '            .DATEUPDATED = entity.DATEUPDATED
        '            .KDJOURNAL = entity.KDOPNAME
        '            .DATE = entity.DATE
        '            .MEMO = "NO. : " & entity.KDOPNAME & ", USER : " & entity.KDUSER
        '            .ISAUTO = True
        '            .KDUSER = entity.KDUSER
        '        End With

        '        Dim sSeq As Integer = 0

        '        Dim arrJournal_D As New List(Of A_JOURNAL_D)

        '        For Each iLoop In entityDetail
        '            Dim dsItem = oItem.GetData(iLoop.KDITEM)
        '            Dim sRATE = oItem.GetDataRate(iLoop.KDITEM, iLoop.KDUOM)

        '            Dim sSEQREF = iLoop.SEQ
        '            Dim sPrice As Decimal = 0

        '            Dim oStockCard As New Accounting.clsStockCard

        '            Try
        '                sPrice = oStockCard.GetData(iLoop.KDITEM, iLoop.KDUOM).OrderByDescending(Function(x) x.SEQ).FirstOrDefault(Function(x) x.NOREFERENCE = entity.KDOPNAME And x.SEQREF = sSEQREF).HPPAVERAGE
        '            Catch ex As Exception
        '                sPrice = 0
        '            End Try

        '            Dim dsJournal_D1 As New A_JOURNAL_D
        '            With dsJournal_D1
        '                .DATECREATED = entity.DATECREATED
        '                .DATEUPDATED = entity.DATEUPDATED
        '                .KDJOURNAL = dsJournal_H.KDJOURNAL
        '                .KDCOA = oConnection.db.SET_SETTINGs.FirstOrDefault().COA_CORRECTION
        '                If iLoop.QTY > 0 Then
        '                    .DEBIT = 0
        '                    .CREDIT = CDec((Math.Abs(iLoop.QTY) * sRATE) * sPrice)
        '                Else
        '                    .DEBIT = CDec((Math.Abs(iLoop.QTY) * sRATE) * sPrice)
        '                    .CREDIT = 0
        '                End If

        '                .SEQ = sSeq
        '            End With

        '            arrJournal_D.Add(dsJournal_D1)
        '            sSeq += 1

        '            Dim dsJournal_D2 As New A_JOURNAL_D
        '            With dsJournal_D2
        '                .DATECREATED = entity.DATECREATED
        '                .DATEUPDATED = entity.DATEUPDATED
        '                .KDJOURNAL = dsJournal_H.KDJOURNAL
        '                .KDCOA = dsItem.KDCOA_INVENTORY
        '                If iLoop.QTY > 0 Then
        '                    .DEBIT = CDec((Math.Abs(iLoop.QTY) * sRATE) * sPrice)
        '                    .CREDIT = 0
        '                Else
        '                    .DEBIT = 0
        '                    .CREDIT = CDec((Math.Abs(iLoop.QTY) * sRATE) * sPrice)
        '                End If
        '                .SEQ = sSeq
        '            End With

        '            arrJournal_D.Add(dsJournal_D2)
        '            sSeq += 1
        '        Next

        '        If IsNew = True Then
        '            oJournal.InsertData(dsJournal_H, arrJournal_D, True)
        '        Else
        '            oJournal.UpdateData(dsJournal_H, arrJournal_D)
        '        End If

        '        AutoJournal = True
        '    Catch ex As Exception
        '        AutoJournal = False
        '        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
        '        Throw ex
        '    End Try
        'End Function
        Public Function UpdateDataFix(ByVal isNew As Boolean) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateDataFix = False
                    Exit Function
                End If


                Dim entity = oConnection.db.I_OPNAME_Hs

                For Each iLoop In entity
                    Dim sKDOPNAME = iLoop.KDOPNAME
                    Dim entityDetail = oConnection.db.I_OPNAME_Ds.Where(Function(x) x.KDOPNAME = sKDOPNAME)

                    'Try
                    '    If Not AutoJournal(iLoop, entityDetail.ToList, isNew) Then
                    '        Return False
                    '    End If
                    'Catch ex As Exception
                    '    MsgBox(ex)
                    'End Try

                    Thread.Sleep(100)
                Next

                UpdateDataFix = True
            Catch ex As Exception
                UpdateDataFix = False
                MsgBox(ex)
            End Try
        End Function
        Public Function UpdateAmount(ByVal sKDITEM As String, ByVal sKDUOM As String, ByVal sKDWAREHOUSE As String, ByVal sAMOUNT As Decimal) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateAmount = False
                    Exit Function
                End If

                UpdateAmount = True

                Dim ds = oConnection.db.M_ITEM_WAREHOUSEs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM And x.KDUOM = sKDUOM And x.KDWAREHOUSE = sKDWAREHOUSE)

                If ds IsNot Nothing Then
                    ds.AMOUNT = sAMOUNT
                End If

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdateAmount = False
                Throw ex
            End Try
        End Function
    End Class
End Namespace