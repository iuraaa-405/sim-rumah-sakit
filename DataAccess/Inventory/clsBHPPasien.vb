Imports System.Threading

Namespace Inventory
    Public Class clsBHPPasien
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
            sMODUL = "BHPP"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As I_BHPPASIEN_H
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New I_BHPPASIEN_H
        End Function
        Public Function GetStructureDetail() As I_BHPPASIEN_D
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New I_BHPPASIEN_D
        End Function
        Public Function GetStructureDetailList() As List(Of I_BHPPASIEN_D)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of I_BHPPASIEN_D)
        End Function
        Public Function GetData() As List(Of I_BHPPASIEN_H)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.I_BHPPASIEN_Hs.OrderByDescending(Function(x) x.KDBHPPASIEN).ToList()
        End Function
        Public Function GetData(ByVal sKDBHPPASIEN As String) As I_BHPPASIEN_H
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.I_BHPPASIEN_Hs.FirstOrDefault(Function(x) x.KDBHPPASIEN = sKDBHPPASIEN)
        End Function
        Public Function GetDataDetail() As List(Of I_BHPPASIEN_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.I_BHPPASIEN_Ds.ToList()
        End Function
        Public Function GetDataDetail(ByVal sKDBHPPASIEN As String) As List(Of I_BHPPASIEN_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.I_BHPPASIEN_Ds.Where(Function(x) x.KDBHPPASIEN = sKDBHPPASIEN).ToList()
        End Function
        Public Function GetDataByRmTerakhir(ByVal Parameter1 As String) As I_BHPPASIEN_D
            If Not oConnection.GetConnection() Then
                GetDataByRmTerakhir = Nothing
                Exit Function
            End If
            GetDataByRmTerakhir = oConnection.db.I_BHPPASIEN_Ds.Where(Function(x) x.I_BHPPASIEN_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.KDCUSTOMER = Parameter1).OrderByDescending(Function(x) x.DATECREATED).FirstOrDefault()
        End Function
        Public Function GetDataIdentitas(ByVal sKDIDENTITAS As String) As A_IDENTITASPASIEN_LIST
            If Not oConnection.GetConnectionRME() Then
                GetDataIdentitas = Nothing
                Exit Function
            End If
            GetDataIdentitas = oConnection.dbRME.A_IDENTITASPASIEN_LISTs.FirstOrDefault(Function(x) x.KDIDENTITAS = sKDIDENTITAS)
        End Function
        Public Function InsertData(ByVal entity As I_BHPPASIEN_H, ByVal entityDetail As List(Of I_BHPPASIEN_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDBHPPASIEN
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

                    entity.KDBHPPASIEN = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)
                    For Each iLoop In entityDetail
                        iLoop.KDBHPPASIEN = entity.KDBHPPASIEN
                    Next
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.I_BHPPASIEN_Hs.InsertOnSubmit(entity)
                    oConnection.db.I_BHPPASIEN_Ds.InsertAllOnSubmit(entityDetail)
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
                '    For Each iLoop In entityDetail
                '        Dim oItem As New Reference.clsItem

                '        If oItem.GetDataDetail_WAREHOUSE(iLoop.KDITEM, entity.KDWAREHOUSE, iLoop.KDUOM) IsNot Nothing Then
                '            Dim dsStock = oConnection.db.M_ITEM_WAREHOUSEs.FirstOrDefault(Function(x) x.KDITEM = iLoop.KDITEM And x.KDWAREHOUSE = entity.KDWAREHOUSE And x.KDUOM = iLoop.KDUOM)

                '            dsStock.AMOUNT -= iLoop.JUMLAH

                '            oConnection.db.SubmitChanges()
                '        Else
                '            Dim dsStock As New M_ITEM_WAREHOUSE
                '            With dsStock
                '                .DATECREATED = entity.DATECREATED
                '                .DATEUPDATED = entity.DATEUPDATED
                '                .KDWAREHOUSE = entity.KDWAREHOUSE
                '                .KDITEM = iLoop.KDITEM
                '                .KDUOM = iLoop.KDUOM
                '                .AMOUNT = -iLoop.JUMLAH
                '            End With

                '            oConnection.db.M_ITEM_WAREHOUSEs.InsertOnSubmit(dsStock)
                '            oConnection.db.SubmitChanges()
                '        End If
                '    Next

                'Catch ex As Exception
                '    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                '    Throw ex
                'End Try
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
            End Try
        End Function
        Public Function UpdateData(ByVal entity As I_BHPPASIEN_H, ByVal entityDetail As List(Of I_BHPPASIEN_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDBHPPASIEN
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.I_BHPPASIEN_Hs.FirstOrDefault(Function(x) x.KDBHPPASIEN = entity.KDBHPPASIEN)

                Try
                    oConnection.db.I_BHPPASIEN_Hs.DeleteOnSubmit(ds)
                    oConnection.db.I_BHPPASIEN_Hs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail = oConnection.db.I_BHPPASIEN_Ds.Where(Function(x) x.KDBHPPASIEN = entity.KDBHPPASIEN)

                'Try
                '    For Each iLoop In dsDetail
                '        Dim oItem As New Reference.clsItem

                '        If oItem.GetDataDetail_WAREHOUSE(iLoop.KDITEM, entity.KDWAREHOUSE, iLoop.KDUOM) IsNot Nothing Then
                '            Dim dsStock = oConnection.db.M_ITEM_WAREHOUSEs.FirstOrDefault(Function(x) x.KDITEM = iLoop.KDITEM And x.KDWAREHOUSE = ds.KDWAREHOUSE And x.KDUOM = iLoop.KDUOM)
                '            dsStock.AMOUNT += iLoop.JUMLAH

                '            oConnection.db.SubmitChanges()
                '        Else
                '            Dim dsStock As New M_ITEM_WAREHOUSE
                '            With dsStock
                '                .DATECREATED = entity.DATECREATED
                '                .DATEUPDATED = entity.DATEUPDATED
                '                .KDWAREHOUSE = entity.KDWAREHOUSE
                '                .KDITEM = iLoop.KDITEM
                '                .KDUOM = iLoop.KDUOM
                '                .AMOUNT = iLoop.JUMLAH
                '            End With

                '            oConnection.db.M_ITEM_WAREHOUSEs.InsertOnSubmit(dsStock)
                '            oConnection.db.SubmitChanges()
                '        End If
                '    Next

                'Catch ex As Exception
                '    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                '    Throw ex
                'End Try
                'Try
                '    For Each iLoop In dsDetail
                '        sKDITEM.Add(iLoop.KDITEM)
                '    Next
                'Catch ex As Exception
                '    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                '    Throw ex
                'End Try
                Try
                    oConnection.db.I_BHPPASIEN_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.I_BHPPASIEN_Ds.InsertAllOnSubmit(entityDetail)
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
                'Try
                '    For Each iLoop In entityDetail
                '        Dim oItem As New Reference.clsItem

                '        If oItem.GetDataDetail_WAREHOUSE(iLoop.KDITEM, entity.KDWAREHOUSE, iLoop.KDUOM) IsNot Nothing Then
                '            Dim dsStock = oConnection.db.M_ITEM_WAREHOUSEs.FirstOrDefault(Function(x) x.KDITEM = iLoop.KDITEM And x.KDWAREHOUSE = entity.KDWAREHOUSE And x.KDUOM = iLoop.KDUOM)
                '            dsStock.AMOUNT -= iLoop.JUMLAH

                '            oConnection.db.SubmitChanges()
                '        Else
                '            Dim dsStock As New M_ITEM_WAREHOUSE
                '            With dsStock
                '                .DATECREATED = entity.DATECREATED
                '                .DATEUPDATED = entity.DATEUPDATED
                '                .KDWAREHOUSE = entity.KDWAREHOUSE
                '                .KDITEM = iLoop.KDITEM
                '                .KDUOM = iLoop.KDUOM
                '                .AMOUNT = -iLoop.JUMLAH
                '            End With

                '            oConnection.db.M_ITEM_WAREHOUSEs.InsertOnSubmit(dsStock)
                '            oConnection.db.SubmitChanges()
                '        End If
                '    Next
                'Catch ex As Exception
                '    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                '    Throw ex
                'End Try
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
        Public Function DeleteData(ByVal sKDBHPPASIEN As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDBHPPASIEN
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.I_BHPPASIEN_Hs.FirstOrDefault(Function(x) x.KDBHPPASIEN = sKDBHPPASIEN)
                Dim dsDetail = oConnection.db.I_BHPPASIEN_Ds.Where(Function(x) x.KDBHPPASIEN = sKDBHPPASIEN)

                Try
                    oConnection.db.I_BHPPASIEN_Hs.DeleteOnSubmit(ds)
                    oConnection.db.I_BHPPASIEN_Ds.DeleteAllOnSubmit(dsDetail)

                    'ds.ISDELETE = True
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                'Try
                '    For Each iLoop In dsDetail
                '        Dim sKDITEM = iLoop.KDITEM
                '        Dim sKDUOM = iLoop.KDUOM

                '        Dim dsStock = oConnection.db.M_ITEM_WAREHOUSEs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM And x.KDWAREHOUSE = ds.KDWAREHOUSE And x.KDUOM = sKDUOM)

                '        dsStock.AMOUNT += iLoop.JUMLAH
                '    Next
                'Catch ex As Exception
                '    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                '    Throw ex
                'End Try
                'Try
                '    For Each iLoop In dsDetail
                '        sKDITEM.Add(iLoop.KDITEM)
                '    Next
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
                'Try
                '    Dim oJournal As New Accounting.clsJournal
                '    oJournal.DeleteData(sKDBHPPASIEN)
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
        'Public Function AutoJournal(ByVal entity As I_BHPPASIEN_H, ByVal entityDetail As List(Of I_BHPPASIEN_D), ByVal IsNew As Boolean) As Boolean
        '    Try
        '        sREFERENCE = entity.KDBHPPASIEN
        '        sSTATUS = "AUTOJOURNAL"

        '        Dim oJournal As New Accounting.clsJournal
        '        Dim oItem As New Reference.clsItem

        '        Dim dsJournal_H As New A_JOURNAL_H
        '        With dsJournal_H
        '            .DATECREATED = entity.DATECREATED
        '            .DATEUPDATED = entity.DATEUPDATED
        '            .KDJOURNAL = entity.KDBHPPASIEN
        '            .DATE = entity.DATE
        '            .MEMO = "NO. : " & entity.KDBHPPASIEN & ", USER : " & entity.KDUSER
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
        '                sPrice = oStockCard.GetData(iLoop.KDITEM, iLoop.KDUOM).OrderByDescending(Function(x) x.SEQ).FirstOrDefault(Function(x) x.NOREFERENCE = entity.KDBHPPASIEN And x.SEQREF = sSEQREF).HPPAVERAGE
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


                Dim entity = oConnection.db.I_BHPPASIEN_Hs

                For Each iLoop In entity
                    Dim sKDBHPPASIEN = iLoop.KDBHPPASIEN
                    Dim entityDetail = oConnection.db.I_BHPPASIEN_Ds.Where(Function(x) x.KDBHPPASIEN = sKDBHPPASIEN)

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
    End Class
End Namespace