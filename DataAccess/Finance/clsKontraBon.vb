Imports System.Threading

Namespace Finance
    Public Class clsKontraBon
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
            sMODUL = "KB"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As F_KONTRABON_H
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New F_KONTRABON_H
        End Function
        Public Function GetStructureDetail() As F_KONTRABON_D
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New F_KONTRABON_D
        End Function
        Public Function GetStructureDetailList() As List(Of F_KONTRABON_D)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of F_KONTRABON_D)
        End Function
        Public Function GetData() As List(Of F_KONTRABON_H)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.F_KONTRABON_Hs.OrderByDescending(Function(x) x.KDKONTRABON).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As F_KONTRABON_H
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.F_KONTRABON_Hs.FirstOrDefault(Function(x) x.KDKONTRABON = Parameter)
        End Function
        Public Function GetDataDetail() As List(Of F_KONTRABON_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.F_KONTRABON_Ds.ToList()
        End Function
        Public Function GetDataDetail(ByVal Parameter As String) As List(Of F_KONTRABON_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.F_KONTRABON_Ds.Where(Function(x) x.KDKONTRABON = Parameter).ToList()
        End Function
        Public Function InsertData(ByVal entity As F_KONTRABON_H, ByVal entityDetail As List(Of F_KONTRABON_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDKONTRABON
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

                    entity.KDKONTRABON = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)
                    For Each iLoop In entityDetail
                        iLoop.KDKONTRABON = entity.KDKONTRABON
                    Next

                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.F_KONTRABON_Hs.InsertOnSubmit(entity)
                    oConnection.db.F_KONTRABON_Ds.InsertAllOnSubmit(entityDetail)
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
                '    If Not AutoJournal(entity, True) Then
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
        Public Function UpdateData(ByVal entity As F_KONTRABON_H, ByVal entityDetail As List(Of F_KONTRABON_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDKONTRABON
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.F_KONTRABON_Hs.FirstOrDefault(Function(x) x.KDKONTRABON = entity.KDKONTRABON)

                Try
                    oConnection.db.F_KONTRABON_Hs.DeleteOnSubmit(ds)
                    oConnection.db.F_KONTRABON_Hs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail = oConnection.db.F_KONTRABON_Ds.Where(Function(x) x.KDKONTRABON = entity.KDKONTRABON)

                Try
                    oConnection.db.F_KONTRABON_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.F_KONTRABON_Ds.InsertAllOnSubmit(entityDetail)
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
                '    If Not AutoJournal(entity, False) Then
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

                Dim ds = oConnection.db.F_KONTRABON_Hs.FirstOrDefault(Function(x) x.KDKONTRABON = Parameter)
                Dim dsDetail = oConnection.db.F_KONTRABON_Ds.Where(Function(x) x.KDKONTRABON = Parameter)

                Try
                    oConnection.db.F_KONTRABON_Hs.DeleteOnSubmit(ds)
                    oConnection.db.F_KONTRABON_Ds.DeleteAllOnSubmit(dsDetail)
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
                '    oJournal.DeleteData(Parameter)
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
        'Public Function AutoJournal(ByVal entity As F_KONTRABON_H, ByVal IsNew As Boolean) As Boolean
        '    Try
        '        sREFERENCE = entity.KDKONTRABON
        '        sSTATUS = "AUTOJOURNAL"

        '        Dim oJournal As New Accounting.clsJournal
        '        Dim oVendor As New Reference.clsVendor
        '        Dim oPaymentType As New Reference.clsPaymentType

        '        Dim dsVendor = oVendor.GetData(entity.KDVENDOR)
        '        Dim dsPaymentType = oPaymentType.GetData(entity.KDPAYMENTTYPE)

        '        Dim dsJournal_H As New A_JOURNAL_H
        '        With dsJournal_H
        '            .DATECREATED = entity.DATECREATED
        '            .DATEUPDATED = entity.DATEUPDATED
        '            .KDJOURNAL = entity.KDKONTRABON
        '            .DATE = entity.DATE
        '            .MEMO = "MEMO : " & entity.MEMO & ", VENDOR : " & dsVendor.NAME_DISPLAY
        '            .ISAUTO = True
        '            .KDUSER = entity.KDUSER
        '        End With

        '        Dim sSeq As Integer = 0

        '        Dim arrJournal_D As New List(Of A_JOURNAL_D)

        '        Dim dsJournal_D1 As New A_JOURNAL_D
        '        With dsJournal_D1
        '            .DATECREATED = entity.DATECREATED
        '            .DATEUPDATED = entity.DATEUPDATED
        '            .KDJOURNAL = entity.KDKONTRABON
        '            .KDCOA = dsPaymentType.KDCOA
        '            .DEBIT = 0
        '            .CREDIT = entity.GRANDTOTAL
        '            .SEQ = sSeq
        '        End With

        '        arrJournal_D.Add(dsJournal_D1)
        '        sSeq += 1

        '        Dim dsJournal_D2 As New A_JOURNAL_D
        '        With dsJournal_D2
        '            .DATECREATED = entity.DATECREATED
        '            .DATEUPDATED = entity.DATEUPDATED
        '            .KDJOURNAL = entity.KDKONTRABON
        '            .KDCOA = dsVendor.KDCOA
        '            .DEBIT = entity.GRANDTOTAL
        '            .CREDIT = 0
        '            .SEQ = sSeq
        '        End With

        '        arrJournal_D.Add(dsJournal_D2)
        '        sSeq += 1

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


                Dim entity = oConnection.db.F_KONTRABON_Hs

                For Each iLoop In entity
                    Dim sKDKONTRABON = iLoop.KDKONTRABON
                    Dim entityDetail = oConnection.db.F_KONTRABON_Ds.Where(Function(x) x.KDKONTRABON = sKDKONTRABON And x.SEQ < 100)
                    Dim entityDetail_R = oConnection.db.F_KONTRABON_Ds.Where(Function(x) x.KDKONTRABON = sKDKONTRABON And x.SEQ >= 100)

                    'Try
                    '    If Not AutoJournal(iLoop, isNew) Then
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