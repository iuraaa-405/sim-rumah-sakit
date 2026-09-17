Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmMain
#Region "Declaration"
    Private isRestart As Boolean = False
#End Region
#Region "Function"
    Private Sub frmMain_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        frmDashboard.MdiParent = Me
        frmDashboard.Show()

        fn_LoadLanguage("id")
        fn_LoadLogin()

    End Sub
    Private Sub frmMain_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If (MessageBox.Show(Statement.ExitApplication, Caption.Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) = Windows.Forms.DialogResult.Yes Then
            For Each iLoop In Me.MdiChildren
                iLoop.Close()
            Next

            Environment.Exit(1)
        Else
            e.Cancel = True
        End If
    End Sub
    Private Sub fn_LoadLogin()
        frmLogin.ShowDialog()

        statusKDUSER.Caption = Caption.User & " : " & sUserID & " Versi " & 107
        statusDATE.Caption = Caption.Tanggal & " : " & Now.ToString("dd/MM/yyyy")

        fn_LoadNameModuel()

        fn_LoadDiagnosa()
        fn_LoadProsedur()
        fn_LoadDiagnosaINACBG()
        fn_LoadProsedurINACBG()
    End Sub
    Private Sub fn_LoadLanguage(ByVal sCulture As String)
        Try
            If sCulture = "id" Then
                'System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("id-ID")
                System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("id-ID")

                statusLANGUAGE.Caption = "Bahasa : Indonesia"
            Else
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.InstalledUICulture

                statusLANGUAGE.Caption = "Language : English"
            End If

            My.Settings.Save()

            For Each iLoop In Me.MdiChildren
                Dim child As ILanguage = TryCast(iLoop, ILanguage)

                If child IsNot Nothing Then
                    child.fn_LoadLanguage()
                End If
            Next

        Catch oErr As Exception

        End Try

        'File
        mnuFileLanguage.Caption = Caption.FileLanguage
        mnuFileLanguageIndonesian.Caption = Caption.FileLanguageIndonesian
        mnuFileLanguageEnglish.Caption = Caption.FileLanguageEnglish
        mnuFileExit.Caption = Caption.FileExit
        mnuFileChangePassword.Caption = Caption.FileChangePassword

        'Kasir
        mnuKasir.Caption = Caption.Kasir
        mnuKasirPembayaran.Caption = Caption.KasirPembayaran
        mnuKasirSetor.Caption = Caption.KasirSetor
        mnuKasirLaporan.Caption = Caption.Report
        mnuKasirReportPembayaran.Caption = Caption.KasirReportPembayaran
        mnuKasirReportSetor.Caption = Caption.KasirReportSetor

        'Reference
        mnuReference.Caption = Caption.Reference
        mnuReferenceVendor.Caption = Caption.ReferenceVendor
        mnuReferenceCustomer.Caption = Caption.ReferenceCustomer
        mnuReferenceWarehouse.Caption = Caption.ReferenceWarehouse
        mnuReferenceCOA.Caption = Caption.ReferenceCOA
        mnuReferenceUOM.Caption = Caption.ReferenceUOM
        mnuReferenceItem_L1.Caption = Caption.ReferenceItem_L1
        mnuReferenceItem_L2.Caption = Caption.ReferenceItem_L2
        mnuReferenceItem_L3.Caption = Caption.ReferenceItem_L3
        mnuReferenceItem_L4.Caption = Caption.ReferenceItem_L4
        mnuReferenceItem_L5.Caption = Caption.ReferenceItem_L5
        mnuReferenceItem_L6.Caption = Caption.ReferenceItem_L6
        mnuReferenceItem_1.Caption = Caption.ReferenceItem_1
        mnuReferenceItem_2.Caption = Caption.ReferenceItem_2
        mnuReferencePaymentType.Caption = Caption.ReferencePaymentType
        mnuReferenceEmploye.Caption = Caption.ReferenceItem_Employee
        mnuReferenceKesatuan.Caption = Caption.ReferenceKesatuan
        mnuReferencePangkat.Caption = Caption.ReferencePangkat
        mnuReferenceGolongan.Caption = Caption.ReferenceGolongan
        mnuReferencePendidikan.Caption = Caption.ReferencePendidikan
        mnuReferencePekerjaan.Caption = Caption.ReferencePekerjaan
        mnuReferencePenjamin.Caption = Caption.ReferencePenjamin
        mnuReferenceAgama.Caption = Caption.ReferenceAgama
        mnuReferenceSuku.Caption = Caption.ReferenceSuku
        mnuReferenceStatusKeluarga.Caption = Caption.ReferenceStatusKeluarga
        mnuReferencePropinsi.Caption = Caption.ReferencePropinsi
        mnuReferenceKabupaten.Caption = Caption.ReferenceKabupaten
        mnuReferenceKecamatan.Caption = Caption.ReferenceKecamatan
        mnuReferenceKelurahan.Caption = Caption.ReferenceKelurahan


        mnuReferenceItemSigna.Caption = Caption.ReferenceSigna

        mnuSettingUserKoneksi.Caption = Caption.ReferenceSetKoneksi
        mnuReferenceSpesialistik.Caption = Caption.ReferenceSpesialistik
        mnuReferenceDiagnosa.Caption = Caption.ReferenceDiagnosa
        mnuReferenceCOB.Caption = Caption.ReferenceCOB
        mnuReferencePPK.Caption = Caption.ReferencePPK

        'Purchasing
        mnuPurchasing.Caption = Caption.Purchasing
        mnuPurchasingPurchaseOrder.Caption = Caption.PurchasingPurchaseOrder
        mnuPurchasingPurchaseInvoice.Caption = Caption.PurchasingPurchaseInvoice
        mnuPurchasingPurchaseReturn.Caption = Caption.PurchasingPurchaseReturn
        mnuPurchasingReport.Caption = Caption.PurchasingReport
        mnuPurchasingReportPurchaseOrder.Caption = Caption.PurchasingPurchaseOrder
        mnuPurchasingReportPurchaseInvoice.Caption = Caption.PurchasingPurchaseInvoice
        mnuPurchasingReportPurchaseReturn.Caption = Caption.PurchasingPurchaseReturn

        'Sales
        mnuSales.Caption = Caption.Sales
        mnuSalesSalesOrder.Caption = Caption.SalesSalesOrder
        mnuSalesSalesInvoice.Caption = Caption.SalesSalesInvoice
        mnuSalesSalesReturn.Caption = Caption.SalesSalesReturn
        mnuSalesReport.Caption = Caption.SalesReport
        mnuSalesReportSalesOrder.Caption = Caption.SalesSalesOrder
        mnuSalesReportSalesInvoice.Caption = Caption.SalesSalesInvoice
        mnuSalesReportSalesReturn.Caption = Caption.SalesSalesReturn

        'Inventory
        mnuInventory.Caption = Caption.Inventory
        mnuInventoryMutation.Caption = Caption.InventoryMutation
        mnuInventoryOpname.Caption = Caption.InventoryOpname
        mnuInventoryReport.Caption = Caption.InventoryReport
        mnuInventoryReportMutation.Caption = Caption.InventoryMutation
        mnuInventoryReportOpname.Caption = Caption.InventoryOpname
        mnuInventoryReportMonitoringItem.Caption = Caption.InventoryReportMonitoringItem
        mnuInventoryDefecta.Caption = Caption.InventoryDefecta
        mnuInventoryPenerimaan.Caption = Caption.InventoryPenerimaan
        mnuInventoryTransferBarang.Caption = Caption.InventortyTransfer

        'Finance
        mnuFinance.Caption = Caption.Finance
        mnuFinanceCashIn.Caption = Caption.FinanceCashIn
        mnuFinanceCashOut.Caption = Caption.FinanceCashOut
        mnuFinanceReport.Caption = Caption.FinanceReport
        mnuFinanceReportCashIn.Caption = Caption.FinanceCashIn
        mnuFinanceReportCashOut.Caption = Caption.FinanceCashOut
        mnuFinanceReportReceiveables.Caption = Caption.FinanceReportReceiveables
        mnuFinanceReportPayables.Caption = Caption.FinanceReportPayables
        mnuFinanceKontraBon.Caption = Caption.KontraBon

        'Accounting
        mnuAccounting.Caption = Caption.Accounting
        mnuAccountingJournal.Caption = Caption.AccountingJournal
        mnuAccountingReport.Caption = Caption.AccountingReport
        mnuAccountingReportJournal.Caption = Caption.AccountingJournal
        mnuAccountingReportLedger.Caption = Caption.AccountingReportLedger
        mnuAccountingReportBalanceSheet.Caption = Caption.AccountingReportBalanceSheet
        mnuAccountingReportProfitLossStatement.Caption = Caption.AccountingReportProfitLossStatement

        'Admissioon
        mnuAdmission.Caption = Caption.Admission
        mnuAdmissionPendaftaran.Caption = Caption.AdmissionPendaftaran
        mnuAdmissionSKDP.Caption = Caption.AdmissionSKD
        mnuAdmissionApprovalPenajminSEP.Caption = Caption.AdmissionApprovalPenjaminanSEP
        mnuAdmissionUpdateTanggalPulang.Caption = Caption.AdmissionUpdateTanggalPulang
        mnuAdmissionRujukan.Caption = Caption.AdmissionRujukan
        mnuAdmissionOperasi.Caption = Caption.AdmisisionOperasi

        'Setting
        mnuSetting.Caption = Caption.Setting
        mnuSettingDatabase.Caption = Caption.SettingDatabase
        mnuSettingDatabaseBackup.Caption = Caption.SettingDatabaseBackup
        mnuSettingDatabaseRestore.Caption = Caption.SettingDatabaseRestore
        mnuSettingDatabaseConnection.Caption = Caption.SettingDatabaseConnection
        mnuSettingUser.Caption = Caption.SettingUser
        mnuSettingUserUser.Caption = Caption.SettingUserUser
        mnuSettingUserOtority.Caption = Caption.SettingUserOtority
        mnuSettingDatabaseConnectionUser.Caption = Caption.SettingDatabaseConnectionUser

        'Billing
        mnuBillingTambahDeposit.Caption = Caption.BillingTambahDeposit
        mnuBillingCostShare.Caption = Caption.BilliingCostShare
    End Sub
    Private Sub fn_LoadNameModuel()
        Dim oNameModul As New Setting.clsCounter
        Dim dsModul = oNameModul.GetDataNameModul("DEMO")
        If dsModul IsNot Nothing Then
            sDaftar_L1 = dsModul.KDDAFTAR_L1
            sDaftar_L2 = dsModul.KDDAFTAR_L2
            sDaftar_L3 = dsModul.KDDAFTAR_L3
            sDaftar_L4 = dsModul.KDDAFTAR_L4
            sDaftar_L5 = dsModul.KDDAFTAR_L5
            sDaftar_L6 = dsModul.KDDAFTAR_L6

            sItem_L1 = dsModul.KDITEM_L1
            sItem_L2 = dsModul.KDITEM_L2
            sItem_L3 = dsModul.KDITEM_L3
            sItem_L4 = dsModul.KDITEM_L4
            sItem_L5 = dsModul.KDITEM_L5
            sItem_L6 = dsModul.KDITEM_L6

            mnuReferenceAdmissionDaftar_1.Caption = sDaftar_L1
            mnuReferenceAdmissionDaftar_2.Caption = sDaftar_L2
            mnuReferenceAdmissionDaftar_3.Caption = sDaftar_L3
            mnuReferenceAdmissionDaftar_4.Caption = sDaftar_L4
            mnuReferenceAdmissionDaftar_5.Caption = sDaftar_L5
            mnuReferenceAdmissionDaftar_6.Caption = sDaftar_L6

            mnuReferenceItem_L1.Caption = sItem_L1
            mnuReferenceItem_L2.Caption = sItem_L2
            mnuReferenceItem_L3.Caption = sItem_L3
            mnuReferenceItem_L4.Caption = sItem_L4
            mnuReferenceItem_L5.Caption = sItem_L5
            mnuReferenceItem_L6.Caption = sItem_L6
        End If

        If sANTRIAN = True Then
            mnuDokter.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuPerawat.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuERM.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuReference.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuAdmission.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuBilling.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuKasir.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuBillingIstalasiFarmasi.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuLaboratorium.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuRadiologi.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuFinance.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuKlaim.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuJasa.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuAccounting.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuDigital.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuSetting.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuFileChangePassword.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuFileLanguage.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

            Dim frmMIDIAntrianDustira As New frmMIDIAntrianDustira
            frmMIDIAntrianDustira.fn_LoadMeAuto(True)
            frmMIDIAntrianDustira.WindowState = FormWindowState.Maximized
            frmMIDIAntrianDustira.ShowDialog()
        Else
            mnuDokter.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            mnuPerawat.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            mnuERM.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            mnuReference.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            mnuAdmission.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            mnuBilling.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            mnuKasir.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            mnuBillingIstalasiFarmasi.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            mnuLaboratorium.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            mnuRadiologi.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            mnuFinance.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            mnuKlaim.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            mnuJasa.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            mnuAccounting.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            mnuDigital.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuSetting.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            mnuFileChangePassword.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            mnuFileLanguage.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If

        Dim oOtority As New Setting.clsOtority
        Dim oUser As New Setting.clsUser

        Dim dsOtorityModul = (From x In oOtority.GetDataDetail
                              Join y In oUser.GetData
                              On x.KDOTORITY Equals y.KDOTORITY
                              Where x.MODUL = "DEMO2" _
                              And y.KDUSER = sUserID
                              Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

        If dsOtorityModul IsNot Nothing Then
            mnuBilling.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuRadiologi.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuLaboratorium.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuBillingIstalasiFarmasi.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuKasir.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuKlaim.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuFinance.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuJasa.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuAntrian.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            sModulBoolean = True
        End If
    End Sub
    Private Sub mnuFileLogOut_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuFileLogOut.ItemClick
        For Each iLoop In Me.MdiChildren
            iLoop.Close()
        Next

        frmDashboard.MdiParent = Me
        frmDashboard.Show()

        fn_LoadLogin()
    End Sub
#End Region
#Region "File"
    Private Sub mnuFileLanguageIndonesian_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuFileLanguageIndonesian.ItemClick
        fn_LoadLanguage("id")
    End Sub
    Private Sub mnuFileLanguageEnglish_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuFileLanguageEnglish.ItemClick
        fn_LoadLanguage("en")
    End Sub
    Private Sub mnuFileExit_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuFileExit.ItemClick
        isRestart = False
        Application.Exit()
    End Sub
#End Region
#Region "Reference"
    Private Sub mnuReferenceBentukMakanan_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceBentukMakanan.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmBentukMakananList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmBentukMakananList.MdiParent = Me
        frmBentukMakananList.Show()
    End Sub
    Private Sub mnuReferenceJenisDiet_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceJenisDiet.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmJenisDietList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmJenisDietList.MdiParent = Me
        frmJenisDietList.Show()
    End Sub
    Private Sub mnuReferencePDF_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferencePDF.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmPDFList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmPDFList.MdiParent = Me
        frmPDFList.Show()
    End Sub
    Private Sub mnuReferenceVendor_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceVendor.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmVendorList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmVendorList.MdiParent = Me
        frmVendorList.Show()
    End Sub
    Private Sub mnuReferenceCustomer_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceCustomer.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmCustomerList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmCustomerList.MdiParent = Me
        frmCustomerList.Show()
    End Sub
    Private Sub mnuReferenceWarehouse_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceWarehouse.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmWarehouseList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmWarehouseList.MdiParent = Me
        frmWarehouseList.Show()
    End Sub
    Private Sub mnuReferenceWarehouseDepartment_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceWarehouseDepartment.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmWarehouseDepartmentList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmWarehouseDepartmentList.MdiParent = Me
        frmWarehouseDepartmentList.Show()
    End Sub
    Private Sub mnuReferenceCOA_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceCOA.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmCOAList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmCOAList.MdiParent = Me
        frmCOAList.Show()
    End Sub
    Private Sub mnuReferenceUOM_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceUOM.ItemClick
        If MsgBox("Tekan Yes Satuan Obat, Tekan No Satuan Tarif", MsgBoxStyle.Information + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then sStokUom = False Else sStokUom = True

        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmUOMList.Name Then
                iLoop.Close()
            End If
        Next

        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmUOMList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmUOMList.MdiParent = Me
        frmUOMList.Show()
    End Sub
    Private Sub mnuReferenceItem_L1_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceItem_L1.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmItem_L1List.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmItem_L1List.MdiParent = Me
        frmItem_L1List.Show()
    End Sub
    Private Sub mnuReferenceItem_L2_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceItem_L2.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmItem_L2List.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmItem_L2List.MdiParent = Me
        frmItem_L2List.Show()
    End Sub
    Private Sub mnuReferenceItem_L3_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceItem_L3.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmItem_L3List.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmItem_L3List.MdiParent = Me
        frmItem_L3List.Show()
    End Sub
    Private Sub mnuReferenceItem_L4_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceItem_L4.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmItem_L4List.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmItem_L4List.MdiParent = Me
        frmItem_L4List.Show()
    End Sub
    Private Sub mnuReferenceItem_L5_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceItem_L5.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmItem_L5List.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmItem_L5List.MdiParent = Me
        frmItem_L5List.Show()
    End Sub
    Private Sub mnuReferenceItem_L6_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceItem_L6.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmItem_L6List.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmItem_L6List.MdiParent = Me
        frmItem_L6List.Show()
    End Sub
    Private Sub mnuReferenceItemSigna_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceItemSigna.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmSignaList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmSignaList.MdiParent = Me
        frmSignaList.Show()
    End Sub
    Private Sub mnuReferenceItemCaraPakai_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceItemCaraPakai.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmCaraPakaiList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmCaraPakaiList.MdiParent = Me
        frmCaraPakaiList.Show()
    End Sub
    Private Sub mnuReferenceItem_1_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceItem_1.ItemClick
        sStok = False
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmItemList.Name Then
                iLoop.Close()
            End If
        Next
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmItemList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmItemList.MdiParent = Me
        frmItemList.Show()
    End Sub
    Private Sub mnuReferenceItem_2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceItem_2.ItemClick
        sStok = True
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmItemList.Name Then
                iLoop.Close()
            End If
        Next
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmItemList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmItemList.MdiParent = Me
        frmItemList.Show()
    End Sub
    Private Sub mnuReferencePaymentType_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferencePaymentType.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmPaymentTypeList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmPaymentTypeList.MdiParent = Me
        frmPaymentTypeList.Show()
    End Sub
    Private Sub mnuReferenceSpesialistik_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceSpesialistik.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmSpesialistikList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmSpesialistikList.MdiParent = Me
        frmSpesialistikList.Show()
    End Sub
    Private Sub mnuReferenceItemJasa2_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceItemJasa2.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmItemJasaList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmItemJasaList.MdiParent = Me
        frmItemJasaList.Show()
    End Sub
    Private Sub mnuReferenceDokter_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceDokter.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmDoctorList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmDoctorList.MdiParent = Me
        frmDoctorList.Show()
    End Sub
    Private Sub mnuReferenceKelasRawat_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceKelasRawat.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmKelasRawatList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmKelasRawatList.MdiParent = Me
        frmKelasRawatList.Show()
    End Sub
    Private Sub mnuReferenceDiagnosa_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceDiagnosa.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmDiagnosaList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmDiagnosaList.MdiParent = Me
        frmDiagnosaList.Show()
    End Sub
    Private Sub mnuReferenceDiagnosaSnomedCT_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceDiagnosaSnomedCT.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmDiagnosaSnowmedCTList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmDiagnosaSnowmedCTList.MdiParent = Me
        frmDiagnosaSnowmedCTList.Show()
    End Sub
    Private Sub mnuReferenceDiagnosaPRB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceDiagnosaPRB.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmDiagnosa_PRBList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmDiagnosa_PRBList.MdiParent = Me
        frmDiagnosa_PRBList.Show()
    End Sub
    Private Sub mnuReferenceProsedur_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceProsedur.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmProsedurList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmProsedurList.MdiParent = Me
        frmProsedurList.Show()
    End Sub
    Private Sub mnuReferenceCaraKeluar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceCaraKeluar.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmCaraKeluarList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmCaraKeluarList.MdiParent = Me
        frmCaraKeluarList.Show()
    End Sub
    Private Sub mnuReferencePascaPulang_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferencePascaPulang.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmPascaPulangList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmPascaPulangList.MdiParent = Me
        frmPascaPulangList.Show()
    End Sub
    Private Sub mnuReferenceCOB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceCOB.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmCOBList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmCOBList.MdiParent = Me
        frmCOBList.Show()
    End Sub
    Private Sub mnuReferencePPK_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferencePPK.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmPPKList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmPPKList.MdiParent = Me
        frmPPKList.Show()
    End Sub
    Private Sub mnuReferenceUnit_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceUnit.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmDepartmentList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmDepartmentList.MdiParent = Me
        frmDepartmentList.Show()
    End Sub
    Private Sub mnuReferenceKesatuan_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceKesatuan.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmKesatuanList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmKesatuanList.MdiParent = Me
        frmKesatuanList.Show()
    End Sub
    Private Sub mnuReferencePangkat_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferencePangkat.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmPangkatList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmPangkatList.MdiParent = Me
        frmPangkatList.Show()
    End Sub
    Private Sub mnuReferenceGolongan_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceGolongan.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmGolonganList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmGolonganList.MdiParent = Me
        frmGolonganList.Show()
    End Sub
    Private Sub mnuReferencePendidikan_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferencePendidikan.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmPendidikanList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmPendidikanList.MdiParent = Me
        frmPendidikanList.Show()
    End Sub
    Private Sub mnuReferencePekerjaan_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferencePekerjaan.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmPekerjaanList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmPekerjaanList.MdiParent = Me
        frmPekerjaanList.Show()
    End Sub
    Private Sub mnuReferencePenjamin_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferencePenjamin.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmPenjaminList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmPenjaminList.MdiParent = Me
        frmPenjaminList.Show()
    End Sub
    Private Sub mnuReferencePerusahaan_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferencePerusahaan.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmPerusahaanList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmPerusahaanList.MdiParent = Me
        frmPerusahaanList.Show()
    End Sub
    Private Sub mnuReferenceAgama_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceAgama.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmAgamaList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmAgamaList.MdiParent = Me
        frmAgamaList.Show()
    End Sub
    Private Sub mnuReferenceSuku_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceSuku.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmSukuList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmSukuList.MdiParent = Me
        frmSukuList.Show()
    End Sub
    Private Sub mnuReferenceStatusKeluarga_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceStatusKeluarga.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmStatusKeluargaList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmStatusKeluargaList.MdiParent = Me
        frmStatusKeluargaList.Show()
    End Sub
    Private Sub mnuReferencePropinsi_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferencePropinsi.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmPropinsiList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmPropinsiList.MdiParent = Me
        frmPropinsiList.Show()
    End Sub
    Private Sub mnuReferenceKabupaten_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceKabupaten.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmKabupatenList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmKabupatenList.MdiParent = Me
        frmKabupatenList.Show()
    End Sub
    Private Sub mnuReferenceKecamatan_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceKecamatan.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmKecamatanList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmKecamatanList.MdiParent = Me
        frmKecamatanList.Show()
    End Sub
    Private Sub mnuReferenceKelurahan_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceKelurahan.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmKelurahanList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmKelurahanList.MdiParent = Me
        frmKelurahanList.Show()
    End Sub
    Private Sub mnuReferenceAdmissionDaftar_1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceAdmissionDaftar_1.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmDaftar_L1List.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmDaftar_L1List.MdiParent = Me
        frmDaftar_L1List.Show()
    End Sub
    Private Sub mnuReferenceAdmissionDaftar_2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceAdmissionDaftar_2.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmDaftar_L2List.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmDaftar_L2List.MdiParent = Me
        frmDaftar_L2List.Show()
    End Sub
    Private Sub mnuReferenceAdmissionDaftar_3_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceAdmissionDaftar_3.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmDaftar_L3List.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmDaftar_L3List.MdiParent = Me
        frmDaftar_L3List.Show()
    End Sub
    Private Sub mnuReferenceAdmissionDaftar_4_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceAdmissionDaftar_4.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmDaftar_L4List.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmDaftar_L4List.MdiParent = Me
        frmDaftar_L4List.Show()
    End Sub
    Private Sub mnuReferenceAdmissionDaftar_5_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceAdmissionDaftar_5.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmDaftar_L5List.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmDaftar_L5List.MdiParent = Me
        frmDaftar_L5List.Show()
    End Sub
    Private Sub mnuReferenceAdmissionDaftar_6_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceAdmissionDaftar_6.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmDaftar_L6List.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmDaftar_L6List.MdiParent = Me
        frmDaftar_L6List.Show()
    End Sub
    Private Sub mnuReferenceKelasrawatAplicare_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceKelasrawatAplicare.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmKelasAplicareList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmKelasAplicareList.MdiParent = Me
        frmKelasAplicareList.Show()
    End Sub
    Private Sub mnuReferenceStaffBagian_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceStaffBagian.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmStaffBagianList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmStaffBagianList.MdiParent = Me
        frmStaffBagianList.Show()
    End Sub
    Private Sub mnuReferenceStaffPangkat_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceStaffPangkat.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmStaffPangkatList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmStaffPangkatList.MdiParent = Me
        frmStaffPangkatList.Show()
    End Sub
    Private Sub mnuReferenceStaffJabatan_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceStaffJabatan.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmStaffJabatanList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmStaffJabatanList.MdiParent = Me
        frmStaffJabatanList.Show()
    End Sub
    Private Sub mnuReferenceStaffPendidikan_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceStaffPendidikan.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmStaffPendidikanList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmStaffPendidikanList.MdiParent = Me
        frmStaffPendidikanList.Show()
    End Sub
    Private Sub mnuReferenceStaffAnggota_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceStaffAnggota.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmStaffList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmStaffList.MdiParent = Me
        frmStaffList.Show()
    End Sub
#End Region
#Region "Purchasing"
    Private Sub mnuPurchasingPurchaseOrder_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPurchasingPurchaseOrder.ItemClick
        sStok = True

        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmPurchaseOrderList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmPurchaseOrderList.MdiParent = Me
        frmPurchaseOrderList.Show()
    End Sub
    Private Sub mnuPurchasingPurchaseInvoice_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPurchasingPurchaseInvoice.ItemClick
        sStok = True

        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmPurchaseInvoiceList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmPurchaseInvoiceList.MdiParent = Me
        frmPurchaseInvoiceList.Show()
    End Sub
    Private Sub mnuPurchasingPurchaseReturn_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPurchasingPurchaseReturn.ItemClick
        sStok = True

        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmPurchaseReturnList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmPurchaseReturnList.MdiParent = Me
        frmPurchaseReturnList.Show()
    End Sub
#End Region
#Region "Report Purchasing"
    Private Sub mnuPurchasingReportPurchaseOrder_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPurchasingReportPurchaseOrder.ItemClick
        sStok = True

        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmReportPurchaseOrder.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmReportPurchaseOrder.MdiParent = Me
        frmReportPurchaseOrder.Show()
    End Sub
    Private Sub mnuPurchasingReportPurchaseInvoice_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPurchasingReportPurchaseInvoice.ItemClick
        sStok = True

        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmReportPurchaseInvoice.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmReportPurchaseInvoice.MdiParent = Me
        frmReportPurchaseInvoice.Show()
    End Sub
    Private Sub mnuPurchasingReportPurchaseReturn_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPurchasingReportPurchaseReturn.ItemClick
        sStok = True

        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmReportPurchaseReturn.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmReportPurchaseReturn.MdiParent = Me
        frmReportPurchaseReturn.Show()
    End Sub
#End Region
#Region "Sales"
    'Private Sub mnuSalesSalesOrder_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSalesSalesOrder.ItemClick
    '    For Each iLoop In Me.MdiChildren
    '        If iLoop.Name = frmSalesOrderList.Name Then
    '            iLoop.Activate()
    '            Exit Sub
    '        End If
    '    Next

    '    frmSalesOrderList.MdiParent = Me
    '    frmSalesOrderList.Show()
    'End Sub
    'Private Sub mnuSalesSalesInvoice_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSalesSalesInvoice.ItemClick
    '    For Each iLoop In Me.MdiChildren
    '        If iLoop.Name = frmSalesInvoiceList.Name Then
    '            iLoop.Activate()
    '            Exit Sub
    '        End If
    '    Next

    '    frmSalesInvoiceList.MdiParent = Me
    '    frmSalesInvoiceList.Show()
    'End Sub
    'Private Sub mnuSalesSalesReturn_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSalesSalesReturn.ItemClick
    '    For Each iLoop In Me.MdiChildren
    '        If iLoop.Name = frmSalesReturnList.Name Then
    '            iLoop.Activate()
    '            Exit Sub
    '        End If
    '    Next

    '    frmSalesReturnList.MdiParent = Me
    '    frmSalesReturnList.Show()
    'End Sub
#End Region
#Region "Report Sales"
#End Region
#Region "Inventory"
    Private Sub mnuInventoryMutation_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuInventoryMutation.ItemClick
        sStok = True

        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmMutationList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmMutationList.MdiParent = Me
        frmMutationList.Show()
    End Sub
    Private Sub mnuInventoryAdjusment_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuInventoryAdjusment.ItemClick
        sStok = True

        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmAdjustmnetList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmAdjustmnetList.MdiParent = Me
        frmAdjustmnetList.Show()
    End Sub
    Private Sub mnuInventoryReportPenerimaan_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuInventoryReportPenerimaan.ItemClick
        sStok = True

        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmReportPenerimaan.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmReportPenerimaan.MdiParent = Me
        frmReportPenerimaan.Show()
    End Sub
    Private Sub mnuInventoryOpname_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuInventoryOpname.ItemClick
        sStok = True

        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmOpnameList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmOpnameList.MdiParent = Me
        frmOpnameList.Show()
    End Sub
    Private Sub mnuInventoryDefecta_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuInventoryDefecta.ItemClick
        sStok = True

        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmDefectaList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmDefectaList.MdiParent = Me
        frmDefectaList.Show()
    End Sub
    Private Sub mnuInventoryReportAdjustment_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuInventoryReportAdjustment.ItemClick
        sStok = True

        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmReportAdjusment.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmReportAdjusment.MdiParent = Me
        frmReportAdjusment.Show()
    End Sub
    Private Sub mnuInventoryPenerimaan_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuInventoryPenerimaan.ItemClick
        sStok = True

        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmTerimaList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmTerimaList.MdiParent = Me
        frmTerimaList.Show()
    End Sub
#End Region
#Region "Report Inventory"
    Private Sub mnuInventoryReportDefecta_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuInventoryReportDefecta.ItemClick
        sStok = True

        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmReportDefecta.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmReportDefecta.MdiParent = Me
        frmReportDefecta.Show()
    End Sub
    Private Sub mnuInventoryReportMutation_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuInventoryReportMutation.ItemClick
        sStok = True

        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmReportMutation.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmReportMutation.MdiParent = Me
        frmReportMutation.Show()
    End Sub
    Private Sub mnuInventoryReportOpname_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuInventoryReportOpname.ItemClick
        sStok = True

        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmReportOpname.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmReportOpname.MdiParent = Me
        frmReportOpname.Show()
    End Sub
    Private Sub mnuInventoryReportMonitoringItem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuInventoryReportMonitoringItem.ItemClick
        sStok = True

        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmReportMonitoringItem.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmReportMonitoringItem.MdiParent = Me
        frmReportMonitoringItem.Show()
    End Sub
#End Region
#Region "Finance"
    Private Sub mnuFinanceCashBank_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuFinanceCashBank.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmCashList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmCashList.MdiParent = Me
        frmCashList.Show()
    End Sub
    Private Sub mnuFinanceCashIn_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuFinanceCashIn.ItemClick
        'For Each iLoop In Me.MdiChildren
        '    If iLoop.Name = frmCashInList.Name Then
        '        iLoop.Activate()
        '        Exit Sub
        '    End If
        'Next

        'frmCashInList.MdiParent = Me
        'frmCashInList.Show()

        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmSetorList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmSetorList.MdiParent = Me
        frmSetorList.Show()
    End Sub
    Private Sub mnuFinanceCashOut_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuFinanceCashOut.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmCashOutList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmCashOutList.MdiParent = Me
        frmCashOutList.Show()
    End Sub
    Private Sub mnuFinanceKontraBon_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuFinanceKontraBon.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmKontraBonList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmKontraBonList.MdiParent = Me
        frmKontraBonList.Show()
    End Sub
#End Region
#Region "Report Finance"
    Private Sub mnuFinanceReportCashBank_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuFinanceReportCashBank.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmReportCash.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmReportCash.MdiParent = Me
        frmReportCash.Show()
    End Sub
    Private Sub mnuFinanceReportCashIn_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuFinanceReportCashIn.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmReportCashIn.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmReportCashIn.MdiParent = Me
        frmReportCashIn.Show()
    End Sub
    Private Sub mnuFinanceReportCashOut_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuFinanceReportCashOut.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmReportCashOut.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmReportCashOut.MdiParent = Me
        frmReportCashOut.Show()
    End Sub
    Private Sub mnuFinanceReportKontraBon_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuFinanceReportKontraBon.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmReportKontraBon.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmReportKontraBon.MdiParent = Me
        frmReportKontraBon.Show()
    End Sub
    Private Sub mnuFinanceReportReceiveables_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuFinanceReportReceiveables.ItemClick
        'For Each iLoop In Me.MdiChildren
        '    If iLoop.Name = frmReportReceiveables.Name Then
        '        iLoop.Activate()
        '        Exit Sub
        '    End If
        'Next

        'frmReportReceiveables.MdiParent = Me
        'frmReportReceiveables.Show()
    End Sub
    Private Sub mnuFinanceReportPayables_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuFinanceReportPayables.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmReportPayables.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmReportPayables.MdiParent = Me
        frmReportPayables.Show()
    End Sub
#End Region
#Region "Accounting"
    Private Sub mnuAccountingJournal_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAccountingJournal.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmJournalList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmJournalList.MdiParent = Me
        frmJournalList.Show()
    End Sub
#End Region
#Region "Report Accounting"
    Private Sub mnuAccountingReportLedger_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAccountingReportLedger.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmReportLedger.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmReportLedger.MdiParent = Me
        frmReportLedger.Show()
    End Sub
    Private Sub mnuAccountingReportBalanceSheet_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAccountingReportBalanceSheet.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmReportBalanceSheet.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmReportBalanceSheet.MdiParent = Me
        frmReportBalanceSheet.Show()
    End Sub
    Private Sub mnuAccountingReportProfitLossStatement_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAccountingReportProfitLossStatement.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmGrouper2List.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmGrouper2List.MdiParent = Me
        frmGrouper2List.Show()
    End Sub
#End Region
#Region "Admission"
    Private Sub mnuAdmissionPendaftaran_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAdmissionPendaftaran.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmPendaftaranList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmPendaftaranList.MdiParent = Me
        frmPendaftaranList.Show()
    End Sub
    Private Sub mnuAdmissionSKDP_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAdmissionSKDP.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmSKDList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmSKDList.MdiParent = Me
        frmSKDList.Show()
    End Sub
    Private Sub mnuAdmissionApprovalPenajminSEP_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAdmissionApprovalPenajminSEP.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmApproval_Penjaminan_SEPList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmApproval_Penjaminan_SEPList.MdiParent = Me
        frmApproval_Penjaminan_SEPList.Show()
    End Sub
    Private Sub mnuAdmissionUpdateTanggalPulang_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAdmissionUpdateTanggalPulang.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmUpdate_Tanggal_PulangList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmUpdate_Tanggal_PulangList.MdiParent = Me
        frmUpdate_Tanggal_PulangList.Show()
    End Sub
    Private Sub mnuAdmissionRujukan_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAdmissionRujukan.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmRujukanList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmRujukanList.MdiParent = Me
        frmRujukanList.Show()
    End Sub
    Private Sub mnuAdmissionRujukanPRB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAdmissionRujukanPRB.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmPendaftaran_PRBList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmPendaftaran_PRBList.MdiParent = Me
        frmPendaftaran_PRBList.Show()
    End Sub
    Private Sub mnuBillingTambahDeposit_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuBillingTambahDeposit.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmDepositList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmDepositList.MdiParent = Me
        frmDepositList.Show()
    End Sub
    Private Sub mnuBillingCostShare_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuBillingCostShare.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmCostShareList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmCostShareList.MdiParent = Me
        frmCostShareList.Show()
    End Sub
    Private Sub mnuAdmissionPemetaanRuangan_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAdmissionPemetaanRuangan.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmPemetaanRuangan.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmPemetaanRuangan.MdiParent = Me
        frmPemetaanRuangan.Show()
    End Sub
    Private Sub mnusAdmissionMutasiPasien_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnusAdmissionMutasiPasien.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmPendaftaran_KunjunganList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmPendaftaran_KunjunganList.MdiParent = Me
        frmPendaftaran_KunjunganList.Show()
    End Sub
    Private Sub mnuAdmissionPendaftaranGabung_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAdmissionPendaftaranGabung.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmPendaftaranGabungList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmPendaftaranGabungList.MdiParent = Me
        frmPendaftaranGabungList.Show()
    End Sub
    Private Sub mnuAdmissionReportDataKunjunganBPJS_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAdmissionReportDataKunjunganBPJS.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmMonitoringBPJS.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmMonitoringBPJS.MdiParent = Me
        frmMonitoringBPJS.Show()
    End Sub
    Private Sub mnuAdmissionLembarPengajuanKlaim_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAdmissionLembarPengajuanKlaim.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmLPKList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmLPKList.MdiParent = Me
        frmLPKList.Show()
    End Sub
    Private Sub mnuAdmissionReportPendaftaran_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAdmissionReportPendaftaran.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmReportAdmission.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmReportAdmission.MdiParent = Me
        frmReportAdmission.Show()
    End Sub
    Private Sub mnuAdmissionReportPendaftaranRI_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAdmissionReportPendaftaranRI.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmReportAdmissionRI.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmReportAdmissionRI.MdiParent = Me
        frmReportAdmissionRI.Show()
    End Sub
#End Region
#Region "Billing"
    Private Sub mnuBillingRawatJalan_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuBillingRawatJalan.ItemClick
        sStok = False
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmSalesOrderTransaksiList.Name Then
                iLoop.Close()
            End If
        Next
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmSalesOrderTransaksiList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmSalesOrderTransaksiList.MdiParent = Me
        frmSalesOrderTransaksiList.LoadMe(0)
        frmSalesOrderTransaksiList.Show()
    End Sub
    Private Sub mnuBillingRawatInap_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuBillingRawatInap.ItemClick
        sStok = False
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmSalesOrderTransaksiList.Name Then
                iLoop.Close()
            End If
        Next
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmSalesOrderTransaksiList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmSalesOrderTransaksiList.MdiParent = Me
        frmSalesOrderTransaksiList.LoadMe(1)
        frmSalesOrderTransaksiList.Show()
    End Sub
    Private Sub mnuBillingLaboratorium_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuBillingLaboratorium.ItemClick
        sStok = False
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmSalesOrderTransaksiList.Name Then
                iLoop.Close()
            End If
        Next
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmSalesOrderTransaksiList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmSalesOrderTransaksiList.MdiParent = Me
        frmSalesOrderTransaksiList.LoadMe(2)
        frmSalesOrderTransaksiList.Show()
    End Sub
    Private Sub mnuBilingLaboratoriumHasilPCR_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuBilingLaboratoriumHasilPCR.ItemClick
        sStok = False
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmSalesOrderTransaksiPCRList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmSalesOrderTransaksiPCRList.MdiParent = Me
        frmSalesOrderTransaksiPCRList.Show()
    End Sub
    Private Sub mnuBillingRadiologi_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuBillingRadiologi.ItemClick
        sStok = False
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmSalesOrderTransaksiList.Name Then
                iLoop.Close()
            End If
        Next
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmSalesOrderTransaksiList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmSalesOrderTransaksiList.MdiParent = Me
        frmSalesOrderTransaksiList.LoadMe(3)
        frmSalesOrderTransaksiList.Show()
    End Sub
    Private Sub mnuInstalasiFarmasiResep_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuInstalasiFarmasiResep.ItemClick
        sStok = True
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmSalesOrderTransaksiList.Name Then
                iLoop.Close()
            End If
        Next
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmSalesOrderTransaksiList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmSalesOrderTransaksiList.MdiParent = Me
        frmSalesOrderTransaksiList.LoadMe(4)
        frmSalesOrderTransaksiList.Show()
    End Sub
    Private Sub mnuInstalasiFarmasiTanpaResep_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuInstalasiFarmasiTanpaResep.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmSalesOrderTanpaResepList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmSalesOrderTanpaResepList.MdiParent = Me
        frmSalesOrderTanpaResepList.Show()
    End Sub
    Private Sub mnuBillingLaporanFarmasi_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuBillingLaporanFarmasi.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmReportBillingFarmasi.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmReportBillingFarmasi.MdiParent = Me
        frmReportBillingFarmasi.Show()
    End Sub
    Private Sub mnuBillingLaporanBilling_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuBillingLaporanBilling.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmReportSalesOrderTransaksi.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmReportSalesOrderTransaksi.MdiParent = Me
        frmReportSalesOrderTransaksi.Show()
    End Sub
#End Region
#Region "Setting"
    Private Sub mnuSettingDatabaseBackup_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSettingDatabaseBackup.ItemClick
        Dim oConnection As New Setting.clsConnectionMain

        Try
            Dim arrMain() As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString()).Split(";")

            For iLoop As Integer = 0 To arrMain.Length - 1
                Dim arrMainResult() As String = arrMain(iLoop).Split("=")

                For xLoop As Integer = 0 To arrMainResult.Length - 1
                    If arrMainResult(xLoop) = "Initial Catalog" Then
                        If fileDialogSave.ShowDialog() = Windows.Forms.DialogResult.Cancel Then

                        Else
                            If fileDialogSave.FileName <> String.Empty Then
                                Try
                                    If oConnection.BackupData(arrMainResult(xLoop + 1), fileDialogSave.FileName) = True Then
                                        MsgBox(Statement.BackupSuccess, MsgBoxStyle.Information, Caption.Title)
                                    Else
                                        MsgBox(Statement.BackupFail, MsgBoxStyle.Exclamation, Caption.Title)
                                    End If
                                Catch oErr As Exception
                                    MsgBox(Statement.BackupFail, MsgBoxStyle.Exclamation, Caption.Title)
                                End Try
                            End If
                        End If
                    End If
                Next
            Next
        Catch oErr As Exception
            MsgBox(Statement.BackupFail, MsgBoxStyle.Exclamation, Caption.Title)
        End Try
    End Sub
    Private Sub mnuSettingDatabaseRestore_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSettingDatabaseRestore.ItemClick
        Dim oConnection As New Setting.clsConnectionMain

        Try
            Dim arrMain() As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString()).Split(";")

            For iLoop As Integer = 0 To arrMain.Length - 1
                Dim arrMainResult() As String = arrMain(iLoop).Split("=")

                For xLoop As Integer = 0 To arrMainResult.Length - 1
                    If arrMainResult(xLoop) = "Initial Catalog" Then
                        If fileDialogOpen.ShowDialog() = Windows.Forms.DialogResult.Cancel Then

                        Else
                            If fileDialogOpen.FileName <> String.Empty Then
                                Try
                                    If oConnection.RestoreData(arrMainResult(xLoop + 1), fileDialogOpen.FileName) = True Then
                                        MsgBox(Statement.RestoreSuccess, MsgBoxStyle.Information, Caption.Title)

                                        Application.Exit()
                                    Else
                                        MsgBox(Statement.RestoreFail, MsgBoxStyle.Exclamation, Caption.Title)
                                    End If
                                Catch oErr As Exception
                                    MsgBox(Statement.RestoreFail, MsgBoxStyle.Exclamation, Caption.Title)
                                End Try
                            End If
                        End If
                    End If
                Next
            Next
        Catch oErr As Exception
            MsgBox(Statement.RestoreFail, MsgBoxStyle.Exclamation, Caption.Title)
        End Try
    End Sub
    Private Sub mnuSettingDatabaseConnectionUser_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSettingDatabaseConnectionUser.ItemClick
        frmDatabaseUser.ShowDialog(Me)
    End Sub
    Private Sub mnuSettingUserKoneksiSatuSehat_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSettingUserKoneksiSatuSehat.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmSatuSehatKoneksiList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmSatuSehatKoneksiList.MdiParent = Me
        frmSatuSehatKoneksiList.Show()
    End Sub
    Private Sub mnuSettingUserKoneksiSatuSehatToken_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSettingUserKoneksiSatuSehatToken.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmSatuSehatKoneksiTokenList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmSatuSehatKoneksiTokenList.MdiParent = Me
        frmSatuSehatKoneksiTokenList.Show()
    End Sub
    Private Sub mnuSettingUserOtority_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSettingUserOtority.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmOtorityList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmOtorityList.MdiParent = Me
        frmOtorityList.Show()
    End Sub
    Private Sub mnuSettingUserKoneksi_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSettingUserKoneksi.ItemClick
        'For Each iLoop In Me.MdiChildren
        '    If iLoop.Name = frmSetKoneksiList.Name Then
        '        iLoop.Activate()
        '        Exit Sub
        '    End If
        'Next

        'frmSetKoneksiList.MdiParent = Me
        'frmSetKoneksiList.Show()
    End Sub
    Private Sub mnuSettingUserUser_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSettingUserUser.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmUserList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmUserList.MdiParent = Me
        frmUserList.Show()
    End Sub
    Private Sub mnuSettingUserUserFinger_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSettingUserUserFinger.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmUser_FingerList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmUser_FingerList.MdiParent = Me
        frmUser_FingerList.Show()
    End Sub
    Private Sub mnuAccountingRecalculate_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAccountingRecalculate.ItemClick
        Dim oPurchaseInvoice As New Purchasing.clsPurchaseInvoice
        Dim oPurchaseReturn As New Purchasing.clsPurchaseReturn

        'Dim oSalesInvoice As New Sales.clsSalesInvoice
        'Dim oSalesReturn As New Sales.clsSalesReturn

        'Dim oCashIn As New Finance.clsCashIn
        Dim oCashOut As New Finance.clsCashOut

        Dim oCash As New Finance.clsCash

        Dim oOpname As New Inventory.clsOpname
        Dim oItem As New Reference.clsItem

        Dim oJournal As New Accounting.clsJournal

        'If MessageBox.Show("Recalculate?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then

        '    Try
        '        SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

        '        Dim sKDITEM As New List(Of String)

        '        Try
        '            For Each xLoop In oItem.GetData
        '                sKDITEM.Add(xLoop.KDITEM)
        '            Next
        '            Dim oAverage As New Accounting.clsStockCard
        '            oAverage.PostingAverage(sKDITEM)
        '        Catch ex As Exception
        '            MsgBox(ex)
        '        End Try

        '        Try
        '            oPurchaseInvoice.UpdateDataFix(False)
        '        Catch ex As Exception
        '            MsgBox(ex)
        '        End Try
        '        Try
        '            oPurchaseReturn.UpdateDataFix(False)
        '        Catch ex As Exception
        '            MsgBox(ex)
        '        End Try
        '        'Try
        '        '    oSalesInvoice.UpdateDataFix(False)
        '        'Catch ex As Exception
        '        '    MsgBox(ex)
        '        'End Try
        '        'Try
        '        '    oSalesReturn.UpdateDataFix(False)
        '        'Catch ex As Exception
        '        '    MsgBox(ex)
        '        'End Try
        '        'Try
        '        '    oCashIn.UpdateDataFix(False)
        '        'Catch ex As Exception
        '        '    MsgBox(ex)
        '        'End Try
        '        Try
        '            oCashOut.UpdateDataFix(False)
        '        Catch ex As Exception
        '            MsgBox(ex)
        '        End Try
        '        Try
        '            oCash.UpdateDataFix(False)
        '        Catch ex As Exception
        '            MsgBox(ex)
        '        End Try
        '        Try
        '            oOpname.UpdateDataFix(False)
        '        Catch ex As Exception
        '            MsgBox(ex)
        '        End Try
        '        Try
        '            oJournal.UpdateDataFix(False)
        '        Catch ex As Exception
        '            MsgBox(ex)
        '        End Try
        '    Catch ex As Exception

        '    Finally
        '        Thread.Sleep(1000)
        '        SplashScreenManager.CloseForm(False)

        '        MsgBox("Recalculate Done!")
        '    End Try
        'End If
    End Sub
    Private Sub mnuFileChangePassword_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuFileChangePassword.ItemClick
        frmChangePassword.ShowDialog()
    End Sub
    Private Sub mnuSettingImage_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSettingImage.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmImageList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmImageList.MdiParent = Me
        frmImageList.Show()
    End Sub
#End Region
#Region "Jasa"
    Private Sub mnuPenjasaanTransaksi_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPenjasaanTransaksi.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmJasaList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmJasaList.MdiParent = Me
        frmJasaList.Show()
    End Sub
    Private Sub mnuPenjasaanTransaksiBPJS_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPenjasaanTransaksiBPJS.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmJasaList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmJasaList.MdiParent = Me
        frmJasaList.Show()
    End Sub
    Private Sub mnuPenjasaanTransaksiUmum_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPenjasaanTransaksiUmum.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmJasaList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmJasaList.MdiParent = Me
        frmJasaList.Show()
    End Sub
    Private Sub BarButtonItem10_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuJasaTransaksiReport.ItemClick
        'For Each iLoop In Me.MdiChildren
        '    If iLoop.Name = frmReportTransaksiJasa.Name Then
        '        iLoop.Activate()
        '        Exit Sub
        '    End If
        'Next

        'frmReportTransaksiJasa.MdiParent = Me
        'frmReportTransaksiJasa.Show()
    End Sub
    Private Sub mnuJasaUmpanBalikBPJSJudul_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuJasaUmpanBalikBPJSJudul.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmJudulJasaList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmJudulJasaList.MdiParent = Me
        frmJudulJasaList.Show()
    End Sub
    Private Sub mnuJasaUmpanBalikUploadTXT_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuJasaUmpanBalikUploadTXT.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmCahsinBPJSList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmCahsinBPJSList.MdiParent = Me
        frmCahsinBPJSList.Show()
    End Sub
    Private Sub mnuJasaUmpanBalikUploadPemabayaran_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuJasaUmpanBalikUploadPemabayaran.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmCahsinBPJSBayarList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmCahsinBPJSBayarList.MdiParent = Me
        frmCahsinBPJSBayarList.Show()
    End Sub
    Private Sub mnuJasaUmpanBalikReport_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuJasaUmpanBalikReport.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmReportCashinBPJSBayar.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmReportCashinBPJSBayar.MdiParent = Me
        frmReportCashinBPJSBayar.Show()
    End Sub
    Private Sub BarButtonItem9_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem9.ItemClick
        'For Each iLoop In Me.MdiChildren
        '    If iLoop.Name = frmReportUmpanBalikJasa_New.Name Then
        '        iLoop.Activate()
        '        Exit Sub
        '    End If
        'Next

        'frmReportUmpanBalikJasa_New.MdiParent = Me
        'frmReportUmpanBalikJasa_New.Show()
    End Sub
#End Region
#Region "Digital"
    'Private Sub mnuDigitalInstlasiGawatDaruratAsesmenAwalIGD_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuDigitalInstlasiGawatDaruratAsesmenAwalIGD.ItemClick
    '    For Each iLoop In Me.MdiChildren
    '        If iLoop.Name = frmDigital_IGD_01List.Name Then
    '            iLoop.Activate()
    '            Exit Sub
    '        End If
    '    Next

    '    frmDigital_IGD_01List.MdiParent = Me
    '    frmDigital_IGD_01List.Show()
    'End Sub
    'Private Sub mnuDigitalInstlasiGawatDaruratAsesmenKeperawatanGawatDarurat_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuDigitalInstlasiGawatDaruratAsesmenKeperawatanGawatDarurat.ItemClick
    '    For Each iLoop In Me.MdiChildren
    '        If iLoop.Name = frmDigital_IGD_02List.Name Then
    '            iLoop.Activate()
    '            Exit Sub
    '        End If
    '    Next

    '    frmDigital_IGD_02List.MdiParent = Me
    '    frmDigital_IGD_02List.Show()
    'End Sub
    'Private Sub mnuDigitalInstlasiGawatDaruratAsuhanKebidananGawatDarurat_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuDigitalInstlasiGawatDaruratAsuhanKebidananGawatDarurat.ItemClick
    'For Each iLoop In Me.MdiChildren
    '        If iLoop.Name = frmDigital_IGD_03List.Name Then
    '            iLoop.Activate()
    '            Exit Sub
    '        End If
    '    Next

    '    frmDigital_IGD_03List.MdiParent = Me
    '    frmDigital_IGD_03List.Show()
    'End Sub
#End Region
#Region "Kasir"
    Private Sub mnuKasirPembayaran_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuKasirPembayaran.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmCashInList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmCashInList.MdiParent = Me
        frmCashInList.Show()
    End Sub
    Private Sub mnuKasirSetor_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuKasirSetor.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmSetorList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmSetorList.MdiParent = Me
        frmSetorList.Show()
    End Sub
    Private Sub mnuFinanceReportSetoran_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuFinanceReportSetoran.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmReportSetor.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmReportSetor.MdiParent = Me
        frmReportSetor.Show()
    End Sub
    Private Sub mnuInventoryTransferBarang_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuInventoryTransferBarang.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmTransferBarang.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmTransferBarang.MdiParent = Me
        frmTransferBarang.Show()
    End Sub
    Private Sub mnuKasirReportSetor_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuKasirReportSetor.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmReportSetor.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmReportSetor.MdiParent = Me
        frmReportSetor.Show()
    End Sub
    Private Sub mnuKasirReportPembayaran_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuKasirReportPembayaran.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmReportPembayaran.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmReportPembayaran.MdiParent = Me
        frmReportPembayaran.Show()
    End Sub
    Private Sub mnuAdmissionLaporanAplicares_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAdmissionLaporanAplicares.ItemClick
        'For Each iLoop In Me.MdiChildren
        '    If iLoop.Name = frmReportAplicare.Name Then
        '        iLoop.Activate()
        '        Exit Sub
        '    End If
        'Next

        'frmReportAplicare.MdiParent = Me
        'frmReportAplicare.Show()

        Dim frmDasboardTempatTidurRS As New frmDasboardTempatTidurRS
        frmDasboardTempatTidurRS.Location = Screen.AllScreens(UBound(Screen.AllScreens)).Bounds.Location + New Point(100, 100)
        frmDasboardTempatTidurRS.Show()

    End Sub
    Private Sub mnuAdmissionPemetaanRuanganDasboardRS_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAdmissionPemetaanRuanganDasboardRS.ItemClick
        Dim frmDasboardTempatTidurRS_New As New frmDasboardTempatTidurRS_New
        frmDasboardTempatTidurRS_New.Location = Screen.AllScreens(UBound(Screen.AllScreens)).Bounds.Location + New Point(100, 100)
        frmDasboardTempatTidurRS_New.Show()
    End Sub
    Private Sub mnuAdmissionLaporanDataAplicares_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAdmissionLaporanDataAplicares.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmReportAplicares.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmReportAplicares.MdiParent = Me
        frmReportAplicares.Show()
    End Sub

#End Region
#Region "Web Service"
    Private Sub mnuAdmissionOperasi_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAdmissionOperasi.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmSET_BOOKING_JADWALOPERASIList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmSET_BOOKING_JADWALOPERASIList.MdiParent = Me
        frmSET_BOOKING_JADWALOPERASIList.Show()
    End Sub
    Private Sub mnuAdmissionAntrianOnlineJKN_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAdmissionAntrianOnlineJKN.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmReportAntrianOnlineJKN.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmReportAntrianOnlineJKN.MdiParent = Me
        frmReportAntrianOnlineJKN.Show()
    End Sub
    Private Sub mnuReferenceAdmissionDiagnosaHarga_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceAdmissionDiagnosaHarga.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmDiagnosaMasterPriceList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmDiagnosaMasterPriceList.MdiParent = Me
        frmDiagnosaMasterPriceList.Show()
    End Sub
    Private Sub mnuAdmissionAntrianLayarOperasi_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAdmissionAntrianLayarOperasi.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmReportBookingOperasi.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmReportBookingOperasi.MdiParent = Me
        frmReportBookingOperasi.Show()
    End Sub
    Private Sub mnuBillingReturResepObat_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuBillingReturResepObat.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmSalesReturnList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmSalesReturnList.MdiParent = Me
        frmSalesReturnList.Show()
    End Sub
    Private Sub mnuReferenceAdmissionJadwalDokter_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceAdmissionJadwalDokter.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmJadwalDokterList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmJadwalDokterList.MdiParent = Me
        frmJadwalDokterList.Show()
    End Sub
    Private Sub mnuAdmissionAntrianLayar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAdmissionAntrianLayar.ItemClick
        Dim frmMIDIAntrianDustira As New frmMIDIAntrianDustira
        frmMIDIAntrianDustira.fn_LoadMeAuto(True)
        frmMIDIAntrianDustira.WindowState = FormWindowState.Maximized
        frmMIDIAntrianDustira.ShowDialog()
    End Sub
    Private Sub BarButtonItem32_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem32.ItemClick
        Dim frmLayarAntrian As New frmLayarAntrian
        frmLayarAntrian.WindowState = FormWindowState.Maximized
        frmLayarAntrian.ShowDialog()
    End Sub
    Private Sub mnuSettingAbsensi_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSettingAbsensi.ItemClick
        Dim frmStaffAbsensi As New frmStaffAbsensi
        frmStaffAbsensi.WindowState = FormWindowState.Maximized
        frmStaffAbsensi.ShowDialog()
    End Sub
    Private Sub mnuAdmissionReportDataRencanaKontrol_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAdmissionReportDataRencanaKontrol.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmReportRencaKontrol.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmReportRencaKontrol.MdiParent = Me
        frmReportRencaKontrol.Show()
    End Sub
    Private Sub mnuAdmissionReportPRB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAdmissionReportPRB.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmReportPRB.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmReportPRB.MdiParent = Me
        frmReportPRB.Show()
    End Sub
    Private Sub mnuAdmissionReportDataRujukanBPJS_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAdmissionReportDataRujukanBPJS.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmReportRujukanBPJS.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmReportRujukanBPJS.MdiParent = Me
        frmReportRujukanBPJS.Show()
    End Sub
    Private Sub mnuReferenceRuangRawat_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceRuangRawat.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmRuangRawatList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmRuangRawatList.MdiParent = Me
        frmRuangRawatList.Show()
    End Sub
    Private Sub mnuAdmissionReportDataLPK_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAdmissionReportDataLPK.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmReportDataLembarPengajuanKlaim.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmReportDataLembarPengajuanKlaim.MdiParent = Me
        frmReportDataLembarPengajuanKlaim.Show()
    End Sub
    Private Sub BarButtonItem34_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem34.ItemClick
        Dim frmMIDIAntrianDustira As New frmMIDIAntrianDustira
        frmMIDIAntrianDustira.fn_LoadMeAuto(False)
        frmMIDIAntrianDustira.WindowState = FormWindowState.Maximized
        frmMIDIAntrianDustira.ShowDialog()
    End Sub
    Private Sub mnuKlaimGrouperInacbg_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuKlaimGrouperInacbg.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmIncbgList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmIncbgList.MdiParent = Me
        frmIncbgList.Show()
    End Sub
    Private Sub btnERM_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuERM.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmErmList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmErmList.MdiParent = Me
        frmErmList.Show()
    End Sub
    Private Sub mnuReferenceProfesi_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceProfesi.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmProfesiList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmProfesiList.MdiParent = Me
        frmProfesiList.Show()
    End Sub
    Private Sub mnuReferenceDiagnosaPerawat_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceDiagnosaPerawat.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmItemDiagnosaPerawatList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmItemDiagnosaPerawatList.MdiParent = Me
        frmItemDiagnosaPerawatList.Show()
    End Sub
    Private Sub mnuBillingRequest_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuBillingRequest.ItemClick
        sStok = True
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmRequestResep.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmRequestResep.MdiParent = Me
        frmRequestResep.Show()
    End Sub
    Private Sub BarButtonItem37_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem37.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmRequestLaboratorium.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmRequestLaboratorium.MdiParent = Me
        frmRequestLaboratorium.Show()
    End Sub
    Private Sub mnuLaboratoriumReference_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuLaboratoriumReference.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmHasilLabMasterList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmHasilLabMasterList.MdiParent = Me
        frmHasilLabMasterList.Show()
    End Sub
    Private Sub BarButtonItem42_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem42.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmRequestRadiologi.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmRequestRadiologi.MdiParent = Me
        frmRequestRadiologi.Show()
    End Sub
    Private Sub BarButtonItem43_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem43.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmtEvaluasiKlaim.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmtEvaluasiKlaim.MdiParent = Me
        frmtEvaluasiKlaim.Show()
    End Sub
    Private Sub btnRMEIGD_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnRMEIGD.ItemClick
        'For Each iLoop In Me.MdiChildren
        '    If iLoop.Name = frmErmList.Name Then
        '        iLoop.Activate()
        '        Exit Sub
        '    End If
        'Next

        'frmErmList.MdiParent = Me
        'frmErmList.Show()
    End Sub
    Private Sub btnRMERawatJalan_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnRMERawatJalan.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmMedrekRawatNew2JalanList.Name Then
                iLoop.Close()
            End If
        Next
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmMedrekRawatNew2JalanList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmMedrekRawatNew2JalanList.fn_LoadMe(False)
        frmMedrekRawatNew2JalanList.MdiParent = Me
        frmMedrekRawatNew2JalanList.Show()
    End Sub
    Private Sub btnRMERawatInap_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnRMERawatInap.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmErmList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmErmList.MdiParent = Me
        frmErmList.Show()
    End Sub
    Private Sub btnRMEIGD_Dokter_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnRMEIGD_Dokter.ItemClick

    End Sub
    Private Sub btnRMERawatJalan_Dokter_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnRMERawatJalan_Dokter.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmMedrekRawatNew2JalanList.Name Then
                iLoop.Close()
            End If
        Next
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmMedrekRawatNew2JalanList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmMedrekRawatNew2JalanList.fn_LoadMe(True)
        frmMedrekRawatNew2JalanList.MdiParent = Me
        frmMedrekRawatNew2JalanList.Show()
    End Sub
    Private Sub btnUpload_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnUpload.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmGrouperPDFList.Name Then
                iLoop.Close()
            End If
        Next
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmGrouperPDFList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmGrouperPDFList.MdiParent = Me
        frmGrouperPDFList.Show()
    End Sub
    Private Sub fn_LoadDiagnosa()
        Try
            ListDiagnosaiDRG.Clear()

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "DATABASERS..M_CODE_SYSTEM_IDRG A "
            SQL &= "WHERE "
            SQL &= "A.SYSTEM LIKE '%ICD_10%' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "M_DIAGNOSA")

            For iLoop As Integer = 0 To ds.Tables("M_DIAGNOSA").Rows.Count - 1
                Dim dsRekap As New DataAccess.M_DIAGNOSA
                With ds.Tables("M_DIAGNOSA")
                    dsRekap.DATECREATED = Now
                    dsRekap.DATEUPDATED = Now
                    dsRekap.KDDIAGNOSA = .Rows(iLoop)("CODE")
                    dsRekap.MEMO = .Rows(iLoop)("DESCRIPTION")
                    dsRekap.ISDEFAULT = IIf(.Rows(iLoop)("VALIDCODE") = "0", False, True)
                    dsRekap.ISACTIVE = IIf(.Rows(iLoop)("ACCPDX") = "N", False, True)

                    ListDiagnosaiDRG.Add(dsRekap)
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadProsedur()
        Try
            ListProseduriDRG.Clear()

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "DATABASERS..M_CODE_SYSTEM_IDRG A "
            SQL &= "WHERE "
            SQL &= "A.SYSTEM NOT LIKE '%ICD_10%' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "M_PROSEDUR")

            For iLoop As Integer = 0 To ds.Tables("M_PROSEDUR").Rows.Count - 1
                Dim dsRekap As New DataAccess.M_PROSEDUR
                With ds.Tables("M_PROSEDUR")
                    dsRekap.DATECREATED = Now
                    dsRekap.DATEUPDATED = Now
                    dsRekap.KDPROSEDUR = .Rows(iLoop)("CODE")
                    dsRekap.MEMO = .Rows(iLoop)("DESCRIPTION")
                    dsRekap.ISDEFAULT = IIf(.Rows(iLoop)("VALIDCODE") = "0", False, True)
                    dsRekap.ISACTIVE = IIf(.Rows(iLoop)("ACCPDX") = "N", False, True)

                    ListProseduriDRG.Add(dsRekap)
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDiagnosaINACBG()
        Try
            ListDiagnosaiNACBG.Clear()

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "DATABASERS..M_CODE_SYSTEM_INACBG A "
            SQL &= "WHERE "
            SQL &= "A.SYSTEM LIKE '%ICD_10%' "
            SQL &= "AND A.VALIDCODE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "M_DIAGNOSA")

            For iLoop As Integer = 0 To ds.Tables("M_DIAGNOSA").Rows.Count - 1
                Dim dsRekap As New DataAccess.M_DIAGNOSA
                With ds.Tables("M_DIAGNOSA")
                    dsRekap.DATECREATED = Now
                    dsRekap.DATEUPDATED = Now
                    dsRekap.KDDIAGNOSA = .Rows(iLoop)("CODE")
                    dsRekap.MEMO = .Rows(iLoop)("DESCRIPTION")
                    dsRekap.ISDEFAULT = IIf(.Rows(iLoop)("VALIDCODE") = "0", False, True)
                    dsRekap.ISACTIVE = True

                    ListDiagnosaiNACBG.Add(dsRekap)
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadProsedurINACBG()
        Try
            ListProseduriNACBG.Clear()

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "DATABASERS..M_CODE_SYSTEM_INACBG A "
            SQL &= "WHERE "
            SQL &= "A.SYSTEM NOT LIKE '%ICD_10%' "
            SQL &= "AND A.VALIDCODE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "M_PROSEDUR")

            For iLoop As Integer = 0 To ds.Tables("M_PROSEDUR").Rows.Count - 1
                Dim dsRekap As New DataAccess.M_PROSEDUR
                With ds.Tables("M_PROSEDUR")
                    dsRekap.DATECREATED = Now
                    dsRekap.DATEUPDATED = Now
                    dsRekap.KDPROSEDUR = .Rows(iLoop)("CODE")
                    dsRekap.MEMO = .Rows(iLoop)("DESCRIPTION")
                    dsRekap.ISDEFAULT = IIf(.Rows(iLoop)("VALIDCODE") = "0", False, True)
                    dsRekap.ISACTIVE = True
                    ListProseduriNACBG.Add(dsRekap)
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub mnuRadiologiLaporan_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuRadiologiLaporan.ItemClick

    End Sub
    Private Sub BarButtonItem46_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem46.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmReportLaboratorium.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmReportLaboratorium.MdiParent = Me
        frmReportLaboratorium.Show()
    End Sub

    Private Sub BarButtonItem47_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem47.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmReportDiagnosa.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmReportDiagnosa.MdiParent = Me
        frmReportDiagnosa.Show()
    End Sub
    Private Sub BarButtonItem51_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnAdmissionLaporanKirimRME.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmReportKirimRMEBPJS.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmReportKirimRMEBPJS.MdiParent = Me
        frmReportKirimRMEBPJS.Show()
    End Sub

    Private Sub BarButtonItem51_ItemClick_1(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem51.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmDokumenMasterList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmDokumenMasterList.MdiParent = Me
        frmDokumenMasterList.Show()
    End Sub

    Private Sub mnuReferenceLoincdanKfa_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceLoincdanKfa.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmLoincdanKfaList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmLoincdanKfaList.MdiParent = Me
        frmLoincdanKfaList.Show()
    End Sub

    Private Sub BarButtonItem53_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem53.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmReportRME.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmReportRME.MdiParent = Me
        frmReportRME.Show()
    End Sub

    Private Sub mnuFinanceReportKeuangan_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuFinanceReportKeuangan.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmReportKeuangan.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmReportKeuangan.MdiParent = Me
        frmReportKeuangan.Show()
    End Sub
    Private Sub mnuGiziListDiet_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGiziListDiet.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmDietList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmDietList.MdiParent = Me
        frmDietList.Show()
    End Sub
    Private Sub BarButtonItem54_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem54.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmRequestBDRS.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmRequestBDRS.MdiParent = Me
        frmRequestBDRS.Show()
    End Sub
    Private Sub BarButtonItem55_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem55.ItemClick
        sStok = False
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmSalesOrderTransaksiList.Name Then
                iLoop.Close()
            End If
        Next
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmSalesOrderTransaksiList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmSalesOrderTransaksiList.MdiParent = Me
        frmSalesOrderTransaksiList.LoadMe(5)
        frmSalesOrderTransaksiList.Show()
    End Sub
#End Region
End Class