Imports DataAccess.My.Resources

Namespace Reference
    Public Class clsItem
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing

        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0

        Public oCounter As Setting.clsCounter = Nothing

        Public Sub New(Optional ByVal sConnection As String = "")
            If sConnection = "" Then
                oConnection = New Setting.clsConnectionMain
                oError = New Setting.clsError
                oCounter = New Setting.clsCounter
            Else
                oConnection = New Setting.clsConnectionMain("TAX")
                oError = New Setting.clsError("TAX")
                oCounter = New Setting.clsCounter("TAX")
            End If

            sMODUL = "ITEM"

        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As M_ITEM
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New M_ITEM
        End Function
        Public Function GetStructureHeaderSatuSehat() As M_ITEM_SATUSEHAT
            If Not oConnection.GetConnection() Then
                GetStructureHeaderSatuSehat = Nothing
            End If
            GetStructureHeaderSatuSehat = New M_ITEM_SATUSEHAT
        End Function
        Public Function GetStructureDetail_UOM() As M_ITEM_UOM
            If Not oConnection.GetConnection() Then
                GetStructureDetail_UOM = Nothing
            End If
            GetStructureDetail_UOM = New M_ITEM_UOM
        End Function
        Public Function GetStructureDetail_JASA() As M_ITEM_JASA
            If Not oConnection.GetConnection() Then
                GetStructureDetail_JASA = Nothing
            End If
            GetStructureDetail_JASA = New M_ITEM_JASA
        End Function
        Public Function GetStructureDetail_TemplatLab() As M_ITEM_TEMPALTE_1LAB
            If Not oConnection.GetConnection() Then
                GetStructureDetail_TemplatLab = Nothing
            End If
            GetStructureDetail_TemplatLab = New M_ITEM_TEMPALTE_1LAB
        End Function
        Public Function GetStructureDetail_UOMList() As List(Of M_ITEM_UOM)
            If Not oConnection.GetConnection() Then
                GetStructureDetail_UOMList = Nothing
            End If
            GetStructureDetail_UOMList = New List(Of M_ITEM_UOM)
        End Function
        Public Function GetStructureDetail_JASAList() As List(Of M_ITEM_JASA)
            If Not oConnection.GetConnection() Then
                GetStructureDetail_JASAList = Nothing
            End If
            GetStructureDetail_JASAList = New List(Of M_ITEM_JASA)
        End Function
        Public Function GetStructureDetail_TemplatLabList() As List(Of M_ITEM_TEMPALTE_1LAB)
            If Not oConnection.GetConnection() Then
                GetStructureDetail_TemplatLabList = Nothing
            End If
            GetStructureDetail_TemplatLabList = New List(Of M_ITEM_TEMPALTE_1LAB)
        End Function
        Public Function GetStructureDetail_WAREHOUSE() As M_ITEM_WAREHOUSE
            If Not oConnection.GetConnection() Then
                GetStructureDetail_WAREHOUSE = Nothing
            End If
            GetStructureDetail_WAREHOUSE = New M_ITEM_WAREHOUSE
        End Function
        Public Function GetStructureDetail_WAREHOUSEList() As List(Of M_ITEM_WAREHOUSE)
            If Not oConnection.GetConnection() Then
                GetStructureDetail_WAREHOUSEList = Nothing
            End If
            GetStructureDetail_WAREHOUSEList = New List(Of M_ITEM_WAREHOUSE)
        End Function
        Public Function GetData() As List(Of M_ITEM)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_ITEMs.OrderBy(Function(x) x.NMITEM1).ToList()
        End Function
        Public Function GetDataItemOutomatis() As List(Of M_ITEM)
            If Not oConnection.GetConnection() Then
                GetDataItemOutomatis = Nothing
                Exit Function
            End If
            GetDataItemOutomatis = oConnection.db.M_ITEMs.Where(Function(x) x.M_ITEM_L6.MEMO.Contains("DAFTARAUTOMATIS")).OrderBy(Function(x) x.NMITEM1).ToList()
        End Function
        Public Function GetDataLItem6Description(ByVal sMEMO As String) As M_ITEM
            If Not oConnection.GetConnection() Then
                GetDataLItem6Description = Nothing
                Exit Function
            End If
            GetDataLItem6Description = oConnection.db.M_ITEMs.FirstOrDefault(Function(x) x.M_ITEM_L6.MEMO = sMEMO)
        End Function
        Public Function GetData(ByVal sKDITEM As String) As M_ITEM
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_ITEMs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM)
        End Function
        Public Function GetDataSatuSehat(ByVal sKDITEM As String) As M_ITEM_SATUSEHAT
            If Not oConnection.GetConnection() Then
                GetDataSatuSehat = Nothing
                Exit Function
            End If
            GetDataSatuSehat = oConnection.db.M_ITEM_SATUSEHATs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM)
        End Function
        Public Function GetIDSATUSEHAT(ByVal sKDITEM As String) As String
            If Not oConnection.GetConnection() Then
                GetIDSATUSEHAT = Nothing
                Exit Function
            End If
            Dim data = oConnection.db.M_ITEM_SATUSEHATs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM)
            Return If(data IsNot Nothing, data.IDSATUSEHAT, String.Empty)
        End Function
        Public Function GetDataByAutodiCPPT() As M_ITEM
            If Not oConnection.GetConnection() Then
                GetDataByAutodiCPPT = Nothing
                Exit Function
            End If
            GetDataByAutodiCPPT = oConnection.db.M_ITEMs.FirstOrDefault(Function(x) x.M_ITEM_L6.MEMO = "CPPTAUTO")
        End Function
        Public Function GetDataNMITEM3(ByVal sKDITEM3 As String) As M_ITEM
            If Not oConnection.GetConnection() Then
                GetDataNMITEM3 = Nothing
                Exit Function
            End If
            GetDataNMITEM3 = oConnection.db.M_ITEMs.FirstOrDefault(Function(x) x.NMITEM3 = sKDITEM3 And x.ISSTOK = False)
        End Function
        Public Function GetDataByName(ByVal sKDITEM As String) As M_ITEM
            If Not oConnection.GetConnection() Then
                GetDataByName = Nothing
                Exit Function
            End If
            GetDataByName = oConnection.db.M_ITEMs.FirstOrDefault(Function(x) x.NMITEM2 = sKDITEM)
        End Function
        Public Function GetDataDetail_UOM() As List(Of M_ITEM_UOM)
            If Not oConnection.GetConnection() Then
                GetDataDetail_UOM = Nothing
                Exit Function
            End If
            GetDataDetail_UOM = oConnection.db.M_ITEM_UOMs.ToList()
        End Function
        Public Function GetDataDetail_UOM(ByVal sKDITEM As String) As List(Of M_ITEM_UOM)
            If Not oConnection.GetConnection() Then
                GetDataDetail_UOM = Nothing
                Exit Function
            End If
            GetDataDetail_UOM = oConnection.db.M_ITEM_UOMs.Where(Function(x) x.KDITEM = sKDITEM).ToList()
        End Function
        Public Function GetDataDetail_UOM_ByKelas(ByVal sKDITEM As String, ByVal sKelas As String) As List(Of M_ITEM_UOM)
            If Not oConnection.GetConnection() Then
                GetDataDetail_UOM_ByKelas = Nothing
                Exit Function
            End If
            GetDataDetail_UOM_ByKelas = oConnection.db.M_ITEM_UOMs.Where(Function(x) x.KDITEM = sKDITEM And x.M_UOM.KDKELASRAWAT = sKelas).ToList()
        End Function
        Public Function GetDataDetail_UOM(ByVal sKDITEM As String, ByVal sKDUOM As String) As M_ITEM_UOM
            If Not oConnection.GetConnection() Then
                GetDataDetail_UOM = Nothing
                Exit Function
            End If
            GetDataDetail_UOM = oConnection.db.M_ITEM_UOMs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM And x.KDUOM = sKDUOM)
        End Function
        Public Function GetDataDetail_WAROUSE(ByVal sKDITEM As String, ByVal sKDUOM As String, ByVal sKDWAREHOUSE As String) As M_ITEM_WAREHOUSE
            If Not oConnection.GetConnection() Then
                GetDataDetail_WAROUSE = Nothing
                Exit Function
            End If
            GetDataDetail_WAROUSE = oConnection.db.M_ITEM_WAREHOUSEs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM And x.KDUOM = sKDUOM And x.KDWAREHOUSE = sKDWAREHOUSE)
        End Function
        Public Function GetDataDetail_JASA() As List(Of M_ITEM_JASA)
            If Not oConnection.GetConnection() Then
                GetDataDetail_JASA = Nothing
                Exit Function
            End If
            GetDataDetail_JASA = oConnection.db.M_ITEM_JASAs.ToList()
        End Function
        Public Function GetDataDetail_JASA(ByVal sKDITEM As String) As List(Of M_ITEM_JASA)
            If Not oConnection.GetConnection() Then
                GetDataDetail_JASA = Nothing
                Exit Function
            End If
            GetDataDetail_JASA = oConnection.db.M_ITEM_JASAs.Where(Function(x) x.KDITEM = sKDITEM).ToList()
        End Function
        Public Function GetDataDetail_TemplateLab(ByVal sKDITEM As String) As List(Of M_ITEM_TEMPALTE_1LAB)
            If Not oConnection.GetConnection() Then
                GetDataDetail_TemplateLab = Nothing
                Exit Function
            End If
            GetDataDetail_TemplateLab = oConnection.db.M_ITEM_TEMPALTE_1LABs.Where(Function(x) x.KDITEM = sKDITEM).ToList()
        End Function
        Public Function GetDataDetail_WAREHOUSE() As List(Of M_ITEM_WAREHOUSE)
            If Not oConnection.GetConnection() Then
                GetDataDetail_WAREHOUSE = Nothing
                Exit Function
            End If
            GetDataDetail_WAREHOUSE = oConnection.db.M_ITEM_WAREHOUSEs.ToList()
        End Function
        Public Function GetDataDetail_WAREHOUSEList(ByVal sKDWAREHOUSE As String) As List(Of M_ITEM_WAREHOUSE)
            If Not oConnection.GetConnection() Then
                GetDataDetail_WAREHOUSEList = Nothing
                Exit Function
            End If
            GetDataDetail_WAREHOUSEList = oConnection.db.M_ITEM_WAREHOUSEs.Where(Function(x) x.KDWAREHOUSE = sKDWAREHOUSE And x.AMOUNT <> 0).ToList()
        End Function
        Public Function GetDataDetail_WAREHOUSE(ByVal sKDITEM As String) As List(Of M_ITEM_WAREHOUSE)
            If Not oConnection.GetConnection() Then
                GetDataDetail_WAREHOUSE = Nothing
                Exit Function
            End If
            GetDataDetail_WAREHOUSE = oConnection.db.M_ITEM_WAREHOUSEs.Where(Function(x) x.KDITEM = sKDITEM).ToList
        End Function
        Public Function GetDataDetail_WAREHOUSE(ByVal sKDITEM As String, ByVal sKDWAREHOUSE As String) As List(Of M_ITEM_WAREHOUSE)
            If Not oConnection.GetConnection() Then
                GetDataDetail_WAREHOUSE = Nothing
                Exit Function
            End If
            GetDataDetail_WAREHOUSE = oConnection.db.M_ITEM_WAREHOUSEs.Where(Function(x) x.KDITEM = sKDITEM And x.KDWAREHOUSE = sKDWAREHOUSE).ToList
        End Function
        Public Function GetDataDetail_WAREHOUSE(ByVal sKDITEM As String, ByVal sKDWAREHOUSE As String, ByVal sKDUOM As String) As M_ITEM_WAREHOUSE
            If Not oConnection.GetConnection() Then
                GetDataDetail_WAREHOUSE = Nothing
                Exit Function
            End If
            GetDataDetail_WAREHOUSE = oConnection.db.M_ITEM_WAREHOUSEs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM And x.KDWAREHOUSE = sKDWAREHOUSE And x.KDUOM = sKDUOM)
        End Function
        Public Function GetDataRate(ByVal sKDITEM As String, ByVal sKDUOM As String) As Decimal
            If Not oConnection.GetConnection() Then
                GetDataRate = Nothing
                Exit Function
            End If
            GetDataRate = oConnection.db.M_ITEM_UOMs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM And x.KDUOM = sKDUOM).RATE
        End Function
        Public Function IsExist(ByVal sNMITEM2 As String) As Boolean
            If Not oConnection.GetConnection() Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.db.M_ITEMs.FirstOrDefault(Function(x) x.NMITEM2 = sNMITEM2)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        Public Function InsertData(ByVal entity As M_ITEM, ByVal entityDetail_UOM As List(Of M_ITEM_UOM), ByVal entityDetail_JASA As List(Of M_ITEM_JASA), ByVal entityDetail_TemplateLab As List(Of M_ITEM_TEMPALTE_1LAB), ByVal entitySatuSehat As M_ITEM_SATUSEHAT) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDITEM
                sSTATUS = "INSERT"

                If entity.KDITEM = "<--AUTO-->" Then
                    ''Generate Auto Number
                    'Try
                    '    sLASTNUMBER = CInt(oConnection.db.M_ITEMs.Where(Function(x) x.KDITEM.Contains(sMODUL)).OrderByDescending(Function(x) x.KDITEM).FirstOrDefault().KDITEM.Remove(0, (sMODUL & " _ ").Length)) + 1
                    'Catch ex As Exception
                    '    sLASTNUMBER = 1
                    'End Try
                    ''End Generate

                    sLASTNUMBER = oCounter.GetLastNumber(sMODUL)

                    If sLASTNUMBER = 0 Then
                        Try
                            oCounter.InsertData(sMODUL, entity.DATECREATED)
                            sLASTNUMBER = oCounter.GetLastNumber(sMODUL)
                        Catch ex As Exception
                            sLASTNUMBER = 0
                        End Try
                    End If

                    Try
                        entity.KDITEM = sMODUL & "_" & AutoNumberCode(sLASTNUMBER + 1)
                        oConnection.db.M_ITEMs.InsertOnSubmit(entity)

                        If entitySatuSehat IsNot Nothing Then
                            entitySatuSehat.KDITEM = sMODUL & "_" & AutoNumberCode(sLASTNUMBER + 1)
                            oConnection.db.M_ITEM_SATUSEHATs.InsertOnSubmit(entitySatuSehat)
                        End If

                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try

                    Try
                        If entityDetail_UOM IsNot Nothing Then
                            For Each iLoop In entityDetail_UOM
                                iLoop.KDITEM = sMODUL & "_" & AutoNumberCode(sLASTNUMBER + 1)
                            Next

                            oConnection.db.M_ITEM_UOMs.InsertAllOnSubmit(entityDetail_UOM)
                        End If
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try

                    Try
                        If entityDetail_JASA IsNot Nothing Then
                            For Each iLoop In entityDetail_JASA
                                iLoop.KDITEM = sMODUL & "_" & AutoNumberCode(sLASTNUMBER + 1)
                            Next

                            oConnection.db.M_ITEM_JASAs.InsertAllOnSubmit(entityDetail_JASA)
                        End If
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try

                    Try
                        If entityDetail_TemplateLab.Count > 0 Then
                            For Each iLoop In entityDetail_TemplateLab
                                iLoop.KDITEM = sMODUL & "_" & AutoNumberCode(sLASTNUMBER + 1)
                            Next

                            oConnection.db.M_ITEM_TEMPALTE_1LABs.InsertAllOnSubmit(entityDetail_TemplateLab)
                        End If
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try
                Else
                    Try
                        oConnection.db.M_ITEMs.InsertOnSubmit(entity)
                        oConnection.db.M_ITEM_UOMs.InsertAllOnSubmit(entityDetail_UOM)
                        oConnection.db.M_ITEM_JASAs.InsertAllOnSubmit(entityDetail_JASA)
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try

                End If

                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1)
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
        Public Function UpdateData(ByVal entity As M_ITEM, ByVal entityDetail_UOM As List(Of M_ITEM_UOM), ByVal entityDetail_JASA As List(Of M_ITEM_JASA), ByVal entityDetail_TemplateLab As List(Of M_ITEM_TEMPALTE_1LAB), ByVal entitySatuSehat As M_ITEM_SATUSEHAT) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDITEM
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.M_ITEMs.FirstOrDefault(Function(x) x.KDITEM = entity.KDITEM)
                Dim dsSatuSehat = oConnection.db.M_ITEM_SATUSEHATs.FirstOrDefault(Function(x) x.KDITEM = entity.KDITEM)
                Try
                    oConnection.db.M_ITEMs.DeleteOnSubmit(ds)
                    oConnection.db.M_ITEMs.InsertOnSubmit(entity)
                    If dsSatuSehat IsNot Nothing Then
                        oConnection.db.M_ITEM_SATUSEHATs.DeleteOnSubmit(dsSatuSehat)
                    End If
                    If entitySatuSehat IsNot Nothing Then
                        oConnection.db.M_ITEM_SATUSEHATs.InsertOnSubmit(entitySatuSehat)
                    End If
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail_UOM = oConnection.db.M_ITEM_UOMs.Where(Function(x) x.KDITEM = entity.KDITEM)

                Try
                    oConnection.db.M_ITEM_UOMs.DeleteAllOnSubmit(dsDetail_UOM)
                    oConnection.db.M_ITEM_UOMs.InsertAllOnSubmit(entityDetail_UOM)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail_JASA = oConnection.db.M_ITEM_JASAs.Where(Function(x) x.KDITEM = entity.KDITEM)

                Try
                    oConnection.db.M_ITEM_JASAs.DeleteAllOnSubmit(dsDetail_JASA)
                    oConnection.db.M_ITEM_JASAs.InsertAllOnSubmit(entityDetail_JASA)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail_TemplateLab = oConnection.db.M_ITEM_TEMPALTE_1LABs.Where(Function(x) x.KDITEM = entity.KDITEM)

                Try
                    If dsDetail_TemplateLab.Count > 0 Then
                        oConnection.db.M_ITEM_TEMPALTE_1LABs.DeleteAllOnSubmit(dsDetail_TemplateLab)
                    End If
                    If entityDetail_TemplateLab.Count > 0 Then
                        oConnection.db.M_ITEM_TEMPALTE_1LABs.InsertAllOnSubmit(entityDetail_TemplateLab)
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

                UpdateData = True
            Catch ex As Exception
                UpdateData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateDataItemLab(ByVal entityDetail_TemplateLab As List(Of M_ITEM_TEMPALTE_1LAB)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateDataItemLab = False
                    Exit Function
                End If

                sREFERENCE = entityDetail_TemplateLab.FirstOrDefault.KDITEM
                sSTATUS = "UPDATE"

                Dim dsDetail_TemplateLab = oConnection.db.M_ITEM_TEMPALTE_1LABs.Where(Function(x) x.KDITEM = entityDetail_TemplateLab.FirstOrDefault.KDITEM)

                Try
                    If dsDetail_TemplateLab.Count > 0 Then
                        oConnection.db.M_ITEM_TEMPALTE_1LABs.DeleteAllOnSubmit(dsDetail_TemplateLab)
                    End If
                    If entityDetail_TemplateLab.Count > 0 Then
                        oConnection.db.M_ITEM_TEMPALTE_1LABs.InsertAllOnSubmit(entityDetail_TemplateLab)
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

                UpdateDataItemLab = True
            Catch ex As Exception
                UpdateDataItemLab = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function DeleteData(ByVal sKDITEM As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDITEM
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.M_ITEMs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM)
                Dim dsSatuSehat = oConnection.db.M_ITEM_SATUSEHATs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM)
                Dim dsDetail_UOM = oConnection.db.M_ITEM_UOMs.Where(Function(x) x.KDITEM = sKDITEM)
                Dim dsDetail_JASA = oConnection.db.M_ITEM_JASAs.Where(Function(x) x.KDITEM = sKDITEM)

                Try
                    oConnection.db.M_ITEMs.DeleteOnSubmit(ds)
                    If dsSatuSehat IsNot Nothing Then
                        oConnection.db.M_ITEM_SATUSEHATs.DeleteOnSubmit(dsSatuSehat)
                    End If
                    oConnection.db.M_ITEM_UOMs.DeleteAllOnSubmit(dsDetail_UOM)
                    oConnection.db.M_ITEM_JASAs.DeleteAllOnSubmit(dsDetail_JASA)
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
        Public Function AccountInventoryDefault() As String
            Try
                If Not oConnection.GetConnection() Then
                    AccountInventoryDefault = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_SETTINGs.FirstOrDefault().COA_ITEM_INVENTORY
                Try
                    AccountInventoryDefault = ds
                Catch ex As Exception
                    AccountInventoryDefault = String.Empty
                End Try
            Catch ex As Exception
                AccountInventoryDefault = String.Empty
                Throw ex
            End Try
        End Function
        Public Function AccountSalesDefault() As String
            Try
                If Not oConnection.GetConnection() Then
                    AccountSalesDefault = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_SETTINGs.FirstOrDefault().COA_ITEM_SALES
                Try
                    AccountSalesDefault = ds
                Catch ex As Exception
                    AccountSalesDefault = String.Empty
                End Try
            Catch ex As Exception
                AccountSalesDefault = String.Empty
                Throw ex
            End Try
        End Function
        Public Function AccountCostDefault() As String
            Try
                If Not oConnection.GetConnection() Then
                    AccountCostDefault = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_SETTINGs.FirstOrDefault().COA_ITEM_HPP
                Try
                    AccountCostDefault = ds
                Catch ex As Exception
                    AccountCostDefault = String.Empty
                End Try
            Catch ex As Exception
                AccountCostDefault = String.Empty
                Throw ex
            End Try
        End Function
        Public Function DefaultItem_L1() As String
            Try
                If Not oConnection.GetConnection() Then
                    DefaultItem_L1 = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_ITEM_L1s.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                Try
                    DefaultItem_L1 = ds.KDITEM_L1
                Catch ex As Exception
                    DefaultItem_L1 = String.Empty
                End Try
            Catch ex As Exception
                DefaultItem_L1 = String.Empty
                Throw ex
            End Try
        End Function
        Public Function DefaultItem_L2() As String
            Try
                If Not oConnection.GetConnection() Then
                    DefaultItem_L2 = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_ITEM_L2s.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                Try
                    DefaultItem_L2 = ds.KDITEM_L2
                Catch ex As Exception
                    DefaultItem_L2 = String.Empty
                End Try
            Catch ex As Exception
                DefaultItem_L2 = String.Empty
                Throw ex
            End Try
        End Function
        Public Function DefaultItem_L2_() As String
            Try
                If Not oConnection.GetConnection() Then
                    DefaultItem_L2_ = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_ITEM_L2s.FirstOrDefault(Function(x) x.MEMO = "FARMASI")
                Try
                    DefaultItem_L2_ = ds.KDITEM_L2
                Catch ex As Exception
                    DefaultItem_L2_ = String.Empty
                End Try
            Catch ex As Exception
                DefaultItem_L2_ = String.Empty
                Throw ex
            End Try
        End Function
        Public Function DefaultItem_L3() As String
            Try
                If Not oConnection.GetConnection() Then
                    DefaultItem_L3 = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_ITEM_L3s.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                Try
                    DefaultItem_L3 = ds.KDITEM_L3
                Catch ex As Exception
                    DefaultItem_L3 = String.Empty
                End Try
            Catch ex As Exception
                DefaultItem_L3 = String.Empty
                Throw ex
            End Try
        End Function
        Public Function DefaultItem_L4() As String
            Try
                If Not oConnection.GetConnection() Then
                    DefaultItem_L4 = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_ITEM_L4s.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                Try
                    DefaultItem_L4 = ds.KDITEM_L4
                Catch ex As Exception
                    DefaultItem_L4 = String.Empty
                End Try
            Catch ex As Exception
                DefaultItem_L4 = String.Empty
                Throw ex
            End Try
        End Function
        Public Function DefaultItem_L5() As String
            Try
                If Not oConnection.GetConnection() Then
                    DefaultItem_L5 = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_ITEM_L5s.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                Try
                    DefaultItem_L5 = ds.KDITEM_L5
                Catch ex As Exception
                    DefaultItem_L5 = String.Empty
                End Try
            Catch ex As Exception
                DefaultItem_L5 = String.Empty
                Throw ex
            End Try
        End Function
        Public Function DefaultItem_L6() As String
            Try
                If Not oConnection.GetConnection() Then
                    DefaultItem_L6 = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_ITEM_L6s.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                Try
                    DefaultItem_L6 = ds.KDITEM_L6
                Catch ex As Exception
                    DefaultItem_L6 = String.Empty
                End Try
            Catch ex As Exception
                DefaultItem_L6 = String.Empty
                Throw ex
            End Try
        End Function
        Public Function DefaultItem_Signa() As String
            Try
                If Not oConnection.GetConnection() Then
                    DefaultItem_Signa = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_SIGNAs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                Try
                    DefaultItem_Signa = ds.KDSIGNA
                Catch ex As Exception
                    DefaultItem_Signa = String.Empty
                End Try
            Catch ex As Exception
                DefaultItem_Signa = String.Empty
                Throw ex
            End Try
        End Function
        Public Function DefaultItem_CaraPakai() As String
            Try
                If Not oConnection.GetConnection() Then
                    DefaultItem_CaraPakai = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_CARAPAKAIs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                Try
                    DefaultItem_CaraPakai = ds.KDCARAPAKAI
                Catch ex As Exception
                    DefaultItem_CaraPakai = String.Empty
                End Try
            Catch ex As Exception
                DefaultItem_CaraPakai = String.Empty
                Throw ex
            End Try
        End Function
        Public Function UpdateDataKDITEM_L3(ByVal sKDITEM As String, ByVal sKDITEM_L3 As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateDataKDITEM_L3 = False
                    Exit Function
                End If

                UpdateDataKDITEM_L3 = True

                Dim ds = oConnection.db.M_ITEMs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM)

                ds.KDITEM_L3 = sKDITEM_L3

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdateDataKDITEM_L3 = False
                Throw ex
            End Try
        End Function
    End Class
End Namespace