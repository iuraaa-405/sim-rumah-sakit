Imports System.Threading

Namespace Finance
    Public Class clsSetor
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
            sMODUL = "SETOR"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As F_SETOR_H
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New F_SETOR_H
        End Function
        Public Function GetStructureDetail() As F_SETOR_D
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New F_SETOR_D
        End Function
        Public Function GetStructureDetailList() As List(Of F_SETOR_D)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of F_SETOR_D)
        End Function
        Public Function GetData() As List(Of F_SETOR_H)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.F_SETOR_Hs.OrderByDescending(Function(x) x.KDSETOR).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As F_SETOR_H
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.F_SETOR_Hs.FirstOrDefault(Function(x) x.KDSETOR = Parameter)
        End Function
        Public Function GetDataDetail() As List(Of F_SETOR_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.F_SETOR_Ds.ToList()
        End Function
        Public Function GetDataDetail(ByVal Parameter As String) As List(Of F_SETOR_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.F_SETOR_Ds.Where(Function(x) x.KDSETOR = Parameter).ToList()
        End Function
        Public Function InsertData(ByVal entity As F_SETOR_H, ByVal entityDetail As List(Of F_SETOR_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDSETOR
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

                    entity.KDSETOR = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)
                    For Each iLoop In entityDetail
                        iLoop.KDSETOR = entity.KDSETOR
                    Next
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.F_SETOR_Hs.InsertOnSubmit(entity)
                    oConnection.db.F_SETOR_Ds.InsertAllOnSubmit(entityDetail)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    For Each iLoop In entityDetail
                        Dim sINVOICE = iLoop.NOINVOICE

                        Dim dsInvoice = oConnection.db.F_CASHIN_Hs.FirstOrDefault(Function(x) x.KDCASHIN = sINVOICE)

                        If dsInvoice IsNot Nothing Then
                            dsInvoice.ISSETOR = True
                        End If

                        Dim dsInvoiceTanpaResep = oConnection.db.S_SO_TANPARESEP_Hs.FirstOrDefault(Function(x) x.KDSOTANPARESEP = sINVOICE)

                        If dsInvoiceTanpaResep IsNot Nothing Then
                            dsInvoiceTanpaResep.PAYAMOUNT += entity.GRANDTOTAL
                        End If
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
        Public Function UpdateData(ByVal entity As F_SETOR_H, ByVal entityDetail As List(Of F_SETOR_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDSETOR
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.F_SETOR_Hs.FirstOrDefault(Function(x) x.KDSETOR = entity.KDSETOR)

                Try
                    oConnection.db.F_SETOR_Hs.DeleteOnSubmit(ds)
                    oConnection.db.F_SETOR_Hs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail = oConnection.db.F_SETOR_Ds.Where(Function(x) x.KDSETOR = entity.KDSETOR)

                Try
                    For Each iLoop In dsDetail
                        Dim sINVOICE = iLoop.NOINVOICE

                        Dim dsInvoice = oConnection.db.F_CASHIN_Hs.FirstOrDefault(Function(x) x.KDCASHIN = sINVOICE)

                        If dsInvoice IsNot Nothing Then
                            dsInvoice.ISSETOR = False
                        End If

                        Dim dsInvoiceTanpaResep = oConnection.db.S_SO_TANPARESEP_Hs.FirstOrDefault(Function(x) x.KDSOTANPARESEP = sINVOICE)

                        If dsInvoiceTanpaResep IsNot Nothing Then
                            dsInvoiceTanpaResep.PAYAMOUNT -= ds.GRANDTOTAL
                        End If

                    Next
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.F_SETOR_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.F_SETOR_Ds.InsertAllOnSubmit(entityDetail)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    For Each iLoop In entityDetail
                        Dim sINVOICE = iLoop.NOINVOICE

                        Dim dsInvoice = oConnection.db.F_CASHIN_Hs.FirstOrDefault(Function(x) x.KDCASHIN = sINVOICE)

                        If dsInvoice IsNot Nothing Then
                            dsInvoice.ISSETOR = True
                        End If

                        Dim dsInvoiceTanpaResep = oConnection.db.S_SO_TANPARESEP_Hs.FirstOrDefault(Function(x) x.KDSOTANPARESEP = sINVOICE)

                        If dsInvoiceTanpaResep IsNot Nothing Then
                            dsInvoiceTanpaResep.PAYAMOUNT += entity.GRANDTOTAL
                        End If

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

                Dim ds = oConnection.db.F_SETOR_Hs.Where(Function(x) x.KDSETOR.Contains(Parameter))

                For Each xLoop In ds
                    Dim sNOCASH = xLoop.KDSETOR

                    Dim dsDetail = oConnection.db.F_SETOR_Ds.Where(Function(x) x.KDSETOR = sNOCASH)

                    Try
                        For Each iLoop In dsDetail
                            Dim sINVOICE = iLoop.NOINVOICE

                            Dim dsInvoice = oConnection.db.F_CASHIN_Hs.FirstOrDefault(Function(x) x.KDCASHIN = sINVOICE)

                            If dsInvoice IsNot Nothing Then
                                dsInvoice.ISSETOR = False
                            End If

                            Dim dsInvoiceTanpaResep = oConnection.db.S_SO_TANPARESEP_Hs.FirstOrDefault(Function(x) x.KDSOTANPARESEP = sINVOICE)

                            If dsInvoiceTanpaResep IsNot Nothing Then
                                dsInvoiceTanpaResep.PAYAMOUNT -= iLoop.AMOUNTPAYMENT
                            End If

                        Next
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try

                    Try
                        oConnection.db.F_SETOR_Hs.DeleteOnSubmit(xLoop)
                        oConnection.db.F_SETOR_Ds.DeleteAllOnSubmit(dsDetail)
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

                Next

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function

    End Class
End Namespace