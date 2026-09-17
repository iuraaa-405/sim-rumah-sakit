Imports System.Threading

Namespace Finance
    Public Class clsCashIn
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
            sMODUL = "CI"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As F_CASHIN_H
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New F_CASHIN_H
        End Function
        Public Function GetStructureDetail() As F_CASHIN_D
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New F_CASHIN_D
        End Function
        Public Function GetStructureDetailList() As List(Of F_CASHIN_D)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of F_CASHIN_D)
        End Function
        Public Function GetData() As List(Of F_CASHIN_H)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.F_CASHIN_Hs.OrderByDescending(Function(x) x.KDCASHIN).ToList()
        End Function
        'Public Function GetDataUGD(ByVal Setor As Boolean) As List(Of F_CASHIN_H)
        '    If Not oConnection.GetConnection() Then
        '        GetDataUGD = Nothing
        '        Exit Function
        '    End If

        '    GetDataUGD = oConnection.db.F_CASHIN_Hs.Where(Function(x) x.ISSETOR = Setor And x.S_PENDAFTARAN_H.M_DEPARTMENT.VCLAIM_KODEPOLI = "IGD" And x.COSTSHARE = 0 And x.CATEGORY <> 2).OrderByDescending(Function(x) x.KDCASHIN).ToList()

        'End Function
        'Public Function GetDataPOLI(ByVal Setor As Boolean) As List(Of F_CASHIN_H)
        '    If Not oConnection.GetConnection() Then
        '        GetDataPOLI = Nothing
        '        Exit Function
        '    End If
        '    GetDataPOLI = oConnection.db.F_CASHIN_Hs.Where(Function(x) x.ISSETOR = Setor And x.S_PENDAFTARAN_H.M_DEPARTMENT.VCLAIM_KODEPOLI <> "IGD" And x.S_PENDAFTARAN_H.CATEGORY = 0 And x.COSTSHARE = 0 And x.CATEGORY <> 2).OrderByDescending(Function(x) x.KDCASHIN).ToList()
        'End Function
        'Public Function GetDataRAWATINAP(ByVal Setor As Boolean) As List(Of F_CASHIN_H)
        '    If Not oConnection.GetConnection() Then
        '        GetDataRAWATINAP = Nothing
        '        Exit Function
        '    End If
        '    GetDataRAWATINAP = oConnection.db.F_CASHIN_Hs.Where(Function(x) x.ISSETOR = Setor And x.S_PENDAFTARAN_H.M_DEPARTMENT.VCLAIM_KODEPOLI <> "IGD" And x.S_PENDAFTARAN_H.CATEGORY = 1 And x.COSTSHARE = 0 And x.CATEGORY <> 2).OrderByDescending(Function(x) x.KDCASHIN).ToList()
        'End Function
        'Public Function GetDataCOSTSHARE(ByVal Setor As Boolean) As List(Of F_CASHIN_H)
        '    If Not oConnection.GetConnection() Then
        '        GetDataCOSTSHARE = Nothing
        '        Exit Function
        '    End If
        '    GetDataCOSTSHARE = oConnection.db.F_CASHIN_Hs.Where(Function(x) x.ISSETOR = Setor And x.COSTSHARE <> 0).OrderByDescending(Function(x) x.KDCASHIN).ToList()
        'End Function
        'Public Function GetDataFARMASI(ByVal Setor As Boolean) As List(Of F_CASHIN_H)
        '    If Not oConnection.GetConnection() Then
        '        GetDataFARMASI = Nothing
        '        Exit Function
        '    End If
        '    GetDataFARMASI = oConnection.db.F_CASHIN_Hs.Where(Function(x) x.ISSETOR = Setor And x.CATEGORY = 2).OrderByDescending(Function(x) x.KDCASHIN).ToList()
        'End Function
        Public Function GetData(ByVal Parameter As String) As F_CASHIN_H
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.F_CASHIN_Hs.FirstOrDefault(Function(x) x.KDCASHIN = Parameter)
        End Function
        Public Function GetDataDetailBayar(ByVal Parameter As String) As F_CASHIN_D
            If Not oConnection.GetConnection() Then
                GetDataDetailBayar = Nothing
                Exit Function
            End If
            GetDataDetailBayar = oConnection.db.F_CASHIN_Ds.FirstOrDefault(Function(x) x.NOINVOICE = Parameter)
        End Function
        Public Function GetDataBykode(ByVal Parameter1 As String, ByVal Parameter2 As String) As F_CASHIN_H
            If Not oConnection.GetConnection() Then
                GetDataBykode = Nothing
                Exit Function
            End If
            GetDataBykode = oConnection.db.F_CASHIN_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter1 Or x.KDPENDAFTARAN = Parameter2)
        End Function
        Public Function GetDataBypendaftaran(ByVal Parameter1 As String) As F_CASHIN_H
            If Not oConnection.GetConnection() Then
                GetDataBypendaftaran = Nothing
                Exit Function
            End If
            GetDataBypendaftaran = oConnection.db.F_CASHIN_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter1)
        End Function
        Public Function GetDataBypendaftarankdpendaftaran(ByVal Parameter1 As String) As S_PENDAFTARAN_H
            If Not oConnection.GetConnection() Then
                GetDataBypendaftarankdpendaftaran = Nothing
                Exit Function
            End If
            GetDataBypendaftarankdpendaftaran = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter1)
        End Function
        Public Function GetDataDetail() As List(Of F_CASHIN_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.F_CASHIN_Ds.ToList()
        End Function
        Public Function GetDataDetail(ByVal Parameter As String) As List(Of F_CASHIN_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.F_CASHIN_Ds.Where(Function(x) x.KDCASHIN = Parameter).ToList()
        End Function
        Public Function GetDataDetailFirst(ByVal Parameter As String) As F_CASHIN_D
            If Not oConnection.GetConnection() Then
                GetDataDetailFirst = Nothing
                Exit Function
            End If
            GetDataDetailFirst = oConnection.db.F_CASHIN_Ds.FirstOrDefault(Function(x) x.KDCASHIN = Parameter)
        End Function
        Public Function GetDataByKdPendaftaran(ByVal Parameter As String) As F_CASHIN_H
            If Not oConnection.GetConnection() Then
                GetDataByKdPendaftaran = Nothing
                Exit Function
            End If
            GetDataByKdPendaftaran = oConnection.db.F_CASHIN_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter)
        End Function
        Public Function InsertData(ByVal entity As F_CASHIN_H, ByVal entityDetail As List(Of F_CASHIN_D), Optional ByVal entityDetail_R As List(Of F_CASHIN_D) = Nothing) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = ""
                    Exit Function
                End If

                sREFERENCE = entity.KDCASHIN
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

                    entity.KDCASHIN = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)

                    For Each iLoop In entityDetail
                        iLoop.KDCASHIN = entity.KDCASHIN
                    Next
                    If entityDetail_R IsNot Nothing Then
                        For Each iLoop In entityDetail_R
                            iLoop.KDCASHIN = entity.KDCASHIN
                        Next
                    End If
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.F_CASHIN_Hs.InsertOnSubmit(entity)
                    oConnection.db.F_CASHIN_Ds.InsertAllOnSubmit(entityDetail)
                    If entityDetail_R IsNot Nothing Then
                        oConnection.db.F_CASHIN_Ds.InsertAllOnSubmit(entityDetail_R)
                    End If
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    For Each iLoop In entityDetail
                        Dim sINVOICE = iLoop.NOINVOICE

                        Dim dsInvoice = oConnection.db.S_SO_TRANSAKSI_Hs.FirstOrDefault(Function(x) x.KDSOTRANSAKSI = sINVOICE)

                        If dsInvoice IsNot Nothing Then
                            dsInvoice.PAYAMOUNT += iLoop.AMOUNTPAYMENT
                        End If
                    Next
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    If entityDetail_R IsNot Nothing Then
                        For Each iLoop In entityDetail_R
                            Dim sINVOICE = iLoop.NOINVOICE

                            Dim dsInvoice = oConnection.db.S_SR_Hs.FirstOrDefault(Function(x) x.KDSR = sINVOICE)

                            If dsInvoice IsNot Nothing Then
                                dsInvoice.PAYAMOUNT += iLoop.AMOUNTPAYMENT
                            End If
                        Next
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
                InsertData = entity.KDCASHIN
            Catch ex As Exception
                InsertData = ""
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As F_CASHIN_H, ByVal entityDetail As List(Of F_CASHIN_D), Optional ByVal entityDetail_R As List(Of F_CASHIN_D) = Nothing) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDCASHIN
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.F_CASHIN_Hs.FirstOrDefault(Function(x) x.KDCASHIN = entity.KDCASHIN)

                Try
                    oConnection.db.F_CASHIN_Hs.DeleteOnSubmit(ds)
                    oConnection.db.F_CASHIN_Hs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail = oConnection.db.F_CASHIN_Ds.Where(Function(x) x.KDCASHIN = entity.KDCASHIN)

                Try
                    For Each iLoop In dsDetail
                        Dim sINVOICE = iLoop.NOINVOICE

                        Dim dsInvoice = oConnection.db.S_SO_TRANSAKSI_Hs.FirstOrDefault(Function(x) x.KDSOTRANSAKSI = sINVOICE)

                        If dsInvoice IsNot Nothing Then
                            dsInvoice.PAYAMOUNT -= iLoop.AMOUNTPAYMENT
                        End If
                    Next
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try


                Dim dsDetail_R = oConnection.db.F_CASHIN_Ds.Where(Function(x) x.KDCASHIN = entity.KDCASHIN And x.SEQ >= 100)

                Try
                    For Each iLoop In dsDetail_R
                        Dim sINVOICE = iLoop.NOINVOICE

                        Dim dsInvoice = oConnection.db.S_SR_Hs.FirstOrDefault(Function(x) x.KDSR = sINVOICE)

                        If dsInvoice IsNot Nothing Then
                            dsInvoice.PAYAMOUNT -= iLoop.AMOUNTPAYMENT
                        End If
                    Next
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.F_CASHIN_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.F_CASHIN_Ds.InsertAllOnSubmit(entityDetail)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.F_CASHIN_Ds.DeleteAllOnSubmit(dsDetail_R)
                    oConnection.db.F_CASHIN_Ds.InsertAllOnSubmit(entityDetail_R)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    For Each iLoop In entityDetail
                        Dim sINVOICE = iLoop.NOINVOICE

                        Dim dsInvoice = oConnection.db.S_SO_TRANSAKSI_Hs.FirstOrDefault(Function(x) x.KDSOTRANSAKSI = sINVOICE)

                        If dsInvoice IsNot Nothing Then
                            dsInvoice.PAYAMOUNT += iLoop.AMOUNTPAYMENT
                        End If
                    Next
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    For Each iLoop In entityDetail_R
                        Dim sINVOICE = iLoop.NOINVOICE

                        Dim dsInvoice = oConnection.db.S_SR_Hs.FirstOrDefault(Function(x) x.KDSR = sINVOICE)

                        If dsInvoice IsNot Nothing Then
                            dsInvoice.PAYAMOUNT += iLoop.AMOUNTPAYMENT
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

                Dim ds = oConnection.db.F_CASHIN_Hs.Where(Function(x) x.KDCASHIN.Contains(Parameter))

                For Each xLoop In ds
                    Dim sNOCASH = xLoop.KDCASHIN

                    Dim dsDetail = oConnection.db.F_CASHIN_Ds.Where(Function(x) x.KDCASHIN = sNOCASH)

                    Try
                        For Each iLoop In dsDetail
                            Dim sINVOICE = iLoop.NOINVOICE

                            Dim dsInvoice = oConnection.db.S_SO_TRANSAKSI_Hs.FirstOrDefault(Function(x) x.KDSOTRANSAKSI = sINVOICE)

                            If dsInvoice IsNot Nothing Then
                                dsInvoice.PAYAMOUNT -= iLoop.AMOUNTPAYMENT
                            End If
                        Next
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try

                    Dim dsDetail_R = oConnection.db.F_CASHIN_Ds.Where(Function(x) x.KDCASHIN = sNOCASH And x.SEQ >= 100)

                    Try
                        For Each iLoop In dsDetail_R
                            Dim sINVOICE = iLoop.NOINVOICE

                            Dim dsInvoice = oConnection.db.S_SR_Hs.FirstOrDefault(Function(x) x.KDSR = sINVOICE)

                            If dsInvoice IsNot Nothing Then
                                dsInvoice.PAYAMOUNT -= iLoop.AMOUNTPAYMENT
                            End If
                        Next
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try
                    Try
                        oConnection.db.F_CASHIN_Hs.DeleteOnSubmit(xLoop)
                        oConnection.db.F_CASHIN_Ds.DeleteAllOnSubmit(dsDetail)
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
                    '    oJournal.DeleteData(sNOCASH)
                    'Catch ex As Exception
                    '    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    '    Throw ex
                    'End Try
                Next

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function Daftar_Payment_Default() As String
            Try
                If Not oConnection.GetConnection() Then
                    Daftar_Payment_Default = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_PAYMENTTYPEs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    Daftar_Payment_Default = ds.KDPAYMENTTYPE
                Else
                    Daftar_Payment_Default = String.Empty
                End If
            Catch ex As Exception
                Daftar_Payment_Default = String.Empty
                Throw ex
            End Try
        End Function
    End Class
End Namespace