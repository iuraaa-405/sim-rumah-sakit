Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Imports DevExpress.XtraSplashScreen
Imports System.Text.RegularExpressions
Imports System.Net
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class frmSalesOrderTransaksi
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oSalesOrderTransaksi As New Sales.clsSalesOrderTransaksi
    Private sPopUP As Boolean = False
    Private sCategoryBilling As Integer = 0
    Private sKDCPPT As String = String.Empty
    Private sParamaeterLoadBilling1 As String = String.Empty
    Private sParamaeterLoadBilling2 As String = String.Empty
#End Region
#Region "Function"
    Public Sub fn_loadKunjungan(ByVal Parameter1 As String, ByVal Parameter2 As String, ByVal paramater As Integer)
        sParamaeterLoadBilling1 = Parameter1
        sParamaeterLoadBilling2 = Parameter2
        sCategoryBilling = paramater
    End Sub
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal CategoryBilling As Integer, Optional ByVal NoId As String = "")
        sBayar = False

        oFormMode = FormMode
        sNoId = NoId
        sCategoryBilling = CategoryBilling

        lBHP.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If sCategoryBilling = 0 Or sCategoryBilling = 1 Then
            grdKDDOCTOR_H.Properties.ReadOnly = False
        Else
            grdKDDOCTOR_H.Properties.ReadOnly = True
        End If

        If sCategoryBilling = 4 Then
            btnAdjusment.Enabled = True
            lKDITEMALL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lKDKELASRAWAT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lKDWAREHOUSE.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lSEARCHALL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            'colKDDOCTOR.VisibleIndex = -1
            colKDDEPARTMENT.VisibleIndex = -1
            colKDUSER.VisibleIndex = -1
            colDATECREATED.VisibleIndex = -1
            colISCETAKETIKET.VisibleIndex = 0
            colKDITEM.VisibleIndex = 1
            chkBHP.Checked = True
            lSEARCHITEM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

            colKDITEM.OptionsColumn.TabStop = True
            colKDITEM.OptionsColumn.AllowEdit = True
            colKDITEM.OptionsColumn.AllowFocus = True
            colKDITEM.OptionsColumn.ReadOnly = False
            lKDDOCTOR.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lTUSLAH.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            colREMARKS.Caption = "Cara Pakai / Signa"
            colKDDOCTOR.VisibleIndex = -1
            colREMARKS.VisibleIndex = 7
        Else
            colREMARKS.Caption = "Remarks"

            btnAdjusment.Enabled = False
            btnAdjusment.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

            If sCategoryBilling = 2 Or sCategoryBilling = 3 Or sCategoryBilling = 5 Then
                lSEARCHALL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lKDDOCTOR.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                lSEARCHALL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lKDDOCTOR.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If

            lKDITEMALL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lKDKELASRAWAT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lKDWAREHOUSE.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            colKDDOCTOR.VisibleIndex = 4
            colKDDEPARTMENT.VisibleIndex = -1
            colKDUSER.VisibleIndex = 5
            colQTY.VisibleIndex = 6
            colDATECREATED.VisibleIndex = 0
            colISCETAKETIKET.VisibleIndex = -1
            colGROUPRACIK.VisibleIndex = -1
            chkBHP.Checked = False
            lSEARCHITEM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lTUSLAH.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

            colKDITEM.OptionsColumn.TabStop = False
            colKDITEM.OptionsColumn.AllowEdit = False
            colKDITEM.OptionsColumn.AllowFocus = False
            colKDITEM.OptionsColumn.ReadOnly = True
        End If

        If oFormMode = FORM_MODE.FORM_MODE_EDIT Then
            UpdateDokterTool.Visible = True
        Else
            UpdateDokterTool.Visible = False
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        PdfViewerCPPTRawatJalan.CloseDocument()
        fn_ChangeFormState()
        fn_LoadLanguage()
        fn_Hidden()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = SalesOrderTransaksi.TITLE & " - " & IIf(sCategoryBilling = 0, "Rawat Jalan", IIf(sCategoryBilling = 1, "Rawat Inap", IIf(sCategoryBilling = 2, "Laboratorium", IIf(sCategoryBilling = 3, "Radiologi", IIf(sCategoryBilling = 4, "Farmasi", "BDRS")))))

            lNAMAPASIEN.Text = Customer.TITLE
            lKDDEPARTMENT.Text = Department.TITLE
            lKDDOCTOR_H.Text = Doctor.TITLE & " DPJP"

            If sCategoryBilling = 4 Then
                lKDDOCTOR.Text = Doctor.TITLE
            Else
                lKDDOCTOR.Text = Doctor.TITLE & " Pengirim"
            End If


            lKDITEM_L2.Text = sItem_L2
            lKDKELASRAWAT.Text = Caption.ReferenceKelasRawat
            lKDSOTRANSAKSI.Text = SalesOrderTransaksi.KDSOTRANSAKSI
            lDATE.Text = SalesOrderTransaksi.TANGGAL
            lKDKUNJUNGAN.Text = SalesOrderTransaksi.KDKUNJUNGAN & " *"
            lSUBTOTAL.Text = SalesOrderTransaksi.SUBTOTAL
            lDISCOUNT.Text = SalesOrderTransaksi.DISCOUNT
            lTAX.Text = SalesOrderTransaksi.TAX
            lGRANDTOTAL.Text = SalesOrderTransaksi.GRANDTOTAL
            lKDWAREHOUSE.Text = Caption.ReferenceWarehouse

            lTUSLAH.Text = "Total Tuslah"

            tab1.Text = SalesOrderTransaksi.TAB_DETAIL
            'tab2.Text = SalesOrderTransaksi.TAB_MEMO


            grvDetail.Columns("KDUOM").Caption = SalesOrderTransaksi.DETAIL_KDUOM
            grvDetail.Columns("QTY").Caption = SalesOrderTransaksi.DETAIL_QTY
            grvDetail.Columns("PRICE").Caption = SalesOrderTransaksi.DETAIL_PRICE
            grvDetail.Columns("SUBTOTAL").Caption = SalesOrderTransaksi.DETAIL_SUBTOTAL
            If sCategoryBilling = 4 Then
                grvDetail.Columns("DISCOUNT").Caption = "Tuslah"
                lTAX.Text = "Admin Racik"
                grvDetail.Columns("KDITEM").Caption = "Nama Obat"
                grvKDITEM.Columns("NMITEM2").Caption = "Nama Obat"
            Else
                grvDetail.Columns("KDITEM").Caption = SalesOrderTransaksi.DETAIL_KDITEM
                grvKDITEM.Columns("NMITEM2").Caption = Item.NMITEM2_1
                grvDetail.Columns("DISCOUNT").Caption = SalesOrderTransaksi.DETAIL_DISCOUNT
                lTAX.Text = "Admin"
            End If

            grvDetail.Columns("GRANDTOTAL").Caption = SalesOrderTransaksi.DETAIL_GRANDTOTAL
            grvDetail.Columns("KDDOCTOR").Caption = SalesOrderTransaksi.DETAIL_KDDOCTOR
            grvDetail.Columns("KDDEPARTMENT").Caption = SalesOrderTransaksi.DETAIL_KDDEPARTMENT
            grvDetail.Columns("REMARKS").Caption = SalesOrderTransaksi.DETAIL_REMARKS


            grvKDDOCTOR.Columns("NAME_DISPLAY").Caption = Doctor.NAME_DISPLAY
            grvKDDEPARTMENT.Columns("NAME_DISPLAY").Caption = Department.NAME_DISPLAY

            grvKDUOM.Columns("MEMO").Caption = UOM.MEMO

            If sCategoryBilling = 4 Then
                btnSaveNew.Caption = "Simpan"
                btnSaveClose.Caption = "Simpan & Cetak"
            Else
                btnSaveClose.Caption = "Simpan"
            End If

            'btnSaveNew.Caption = Caption.FormSaveNew
            'btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDSOTRANSAKSI.Text.Trim.ToUpper
        PdfViewerCPPTRawatJalan.CloseDocument()
    End Sub
    Private Sub fn_Hidden()
        If chkBHP.Checked = False Then
            'colKDUOM.VisibleIndex = -1
            colKDUOM.Caption = "Harga Kelas"
            'colKDSIGNA.VisibleIndex = -1
            colSTOK.VisibleIndex = -1
            colISRACIK.VisibleIndex = -1
        Else
            colKDUOM.Caption = "Satuan"
            colISRACIK.VisibleIndex = 2
            'colKDSIGNA.VisibleIndex = -1
            colSTOK.VisibleIndex = 1
        End If
        If sCategoryBilling = 4 Then
            colKDSIGNA.VisibleIndex = 6
            colKDCARAPAKAI.VisibleIndex = 7
        Else
            colKDSIGNA.VisibleIndex = -1
            colKDCARAPAKAI.VisibleIndex = -1
        End If
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDKELASRAWAT()
        fn_LoadKDDOCTOR()
        fn_LoadKDDEPARTMENT()
        fn_LoadKDUSER()
        fn_LoadITEM_L2()
        fn_LoadKDITEM()
        fn_LoadKDUOM()
        fn_LoadKDSIGNA()
        fn_LoadKDCARAPAKAI()
        fn_LoadKDWAREHOUSE()
        If sCategoryBilling <> 4 Then
            fn_LoadKDITEMSearch()
        End If

        Select Case oFormMode
            Case FORM_MODE.FORM_MODE_VIEW
                fn_ViewMode(True)
                fn_LoadData()
            Case FORM_MODE.FORM_MODE_ADD
                fn_ViewMode(False)
                fn_EmptyMe()
            Case FORM_MODE.FORM_MODE_EDIT
                fn_ViewMode(False)
                fn_LoadData()
            Case Else
                fn_ViewMode(True)
        End Select
    End Sub
    Private Sub fn_ViewMode(ByVal Status As Boolean)
        btnSaveNew.Enabled = Not Status
        btnSaveClose.Enabled = Not Status
        btnDiagnosa.Enabled = Not Status
        btnOrder.Enabled = Not Status

        deDATE.Properties.ReadOnly = Status
        grdKDKUNJUNGAN.Properties.ReadOnly = Status
        grdKDDOCTOR_H.Properties.ReadOnly = Status

        txtMEMO.Properties.ReadOnly = Status
        txtSUBTOTAL.Properties.ReadOnly = Status
        txtDISCOUNT.Properties.ReadOnly = Status
        txtTAX.Properties.ReadOnly = Status
        txtGRANDTOTAL.Properties.ReadOnly = Status
        grdDOCTOR.Properties.ReadOnly = Status
        grvDetail.OptionsBehavior.ReadOnly = Status

        If sCategoryBilling = 4 Then
            btnSaveNew.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        Else
            btnSaveNew.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If
    End Sub
    Private Sub fn_EmptyMe()
        txtKDSOTRANSAKSI.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        grdKDKUNJUNGAN.ResetText()

        txtMEMO.ResetText()
        grdDOCTOR.ResetText()

        Dim oGrouperDataCppt As New Grouper.clsR_CPPT
        Dim oItem As New Reference.clsItem

        If sParamaeterLoadBilling1 <> "" Then
            fn_LoadKDKUNJUNGAN(sParamaeterLoadBilling1, 3)
            grdKDKUNJUNGAN.Text = sParamaeterLoadBilling1

            fn_LoadDataIdentitas(sParamaeterLoadBilling1)

            grdDOCTOR.Text = sParamaeterLoadBilling2
        End If

        If sCategoryBilling = 5 Then
            Dim oOrderDarah As New Order.clsOrderDarah

            Dim dsOrderdarah = oOrderDarah.GetData(sFind2)
            If dsOrderdarah IsNot Nothing Then
                fn_LoadKDKUNJUNGAN(dsOrderdarah.A_IDENTITASPASIEN_LIST.KDKUNJUNGAN, 3)
                grdKDKUNJUNGAN.Text = dsOrderdarah.A_IDENTITASPASIEN_LIST.KDKUNJUNGAN

                fn_LoadDataIdentitas(dsOrderdarah.A_IDENTITASPASIEN_LIST.KDKUNJUNGAN)

                grdDOCTOR.Text = dsOrderdarah.A_IDENTITASPASIEN_LIST.KDDOCTOR

                For Each xloop In oOrderDarah.GetDataDetail(dsOrderdarah.KDORDERDARAH)
                    grvDetail.Focus()
                    grvDetail.AddNewRow()
                    grvDetail.SetFocusedRowCellValue(colKDITEM, xloop.KDITEM)
                    grvDetail.SetFocusedRowCellValue(colQTY, xloop.JUMLAH)
                    grvDetail.SetFocusedRowCellValue(colKDCARAPAKAI, oItem.DefaultItem_CaraPakai)
                    grvDetail.SetFocusedRowCellValue(colREMARKS, xloop.MEMO)
                    grvDetail.UpdateCurrentRow()
                Next
            End If

            Calculate()
        Else
            If sFind1 <> "" Then
                sKDCPPT = sFind1

                If sCategoryBilling = 2 Then
                    If sFind1 <> "" Then
                        Dim dsCPPT = oGrouperDataCppt.GetData(sFind1)
                        If dsCPPT IsNot Nothing Then
                            fn_LoadKDKUNJUNGAN(dsCPPT.A_IDENTITASPASIEN_LIST.KDKUNJUNGAN, 3)
                            grdKDKUNJUNGAN.Text = dsCPPT.A_IDENTITASPASIEN_LIST.KDKUNJUNGAN

                            fn_LoadDataIdentitas(dsCPPT.A_IDENTITASPASIEN_LIST.KDKUNJUNGAN)

                            grdDOCTOR.Text = dsCPPT.A_IDENTITASPASIEN_LIST.KDDOCTOR

                            For Each xloop In oGrouperDataCppt.GetDataDetailTindakan(dsCPPT.KDCPPT)
                                If xloop.ISPERAWAT = False Then
                                    Dim dsItem = oItem.GetData(xloop.KDITEM)
                                    If dsItem IsNot Nothing Then
                                        If dsItem.M_ITEM_L3.MEMO = "LABORATORIUM" Then
                                            grvDetail.Focus()
                                            grvDetail.AddNewRow()
                                            grvDetail.SetFocusedRowCellValue(colKDITEM, xloop.KDITEM)
                                            grvDetail.SetFocusedRowCellValue(colQTY, xloop.JUMLAH)
                                            grvDetail.SetFocusedRowCellValue(colKDCARAPAKAI, oItem.DefaultItem_CaraPakai)
                                            grvDetail.SetFocusedRowCellValue(colREMARKS, xloop.MEMO)
                                            grvDetail.UpdateCurrentRow()
                                        End If
                                    End If
                                End If
                            Next
                        End If
                    End If
                ElseIf sCategoryBilling = 3 Then
                    If sFind1 <> "" Then
                        Dim dsCPPT = oGrouperDataCppt.GetData(sFind1)
                        If dsCPPT IsNot Nothing Then
                            fn_LoadKDKUNJUNGAN(dsCPPT.A_IDENTITASPASIEN_LIST.KDKUNJUNGAN, 3)
                            grdKDKUNJUNGAN.Text = dsCPPT.A_IDENTITASPASIEN_LIST.KDKUNJUNGAN

                            fn_LoadDataIdentitas(dsCPPT.A_IDENTITASPASIEN_LIST.KDKUNJUNGAN)
                            grdDOCTOR.Text = dsCPPT.A_IDENTITASPASIEN_LIST.KDDOCTOR

                            For Each xloop In oGrouperDataCppt.GetDataDetailTindakan(dsCPPT.KDCPPT)
                                If xloop.ISPERAWAT = False Then
                                    Dim dsItem = oItem.GetData(xloop.KDITEM)
                                    If dsItem IsNot Nothing Then
                                        If dsItem.M_ITEM_L3.MEMO = "RADIOLOGI" Then
                                            grvDetail.Focus()
                                            grvDetail.AddNewRow()
                                            grvDetail.SetFocusedRowCellValue(colKDITEM, xloop.KDITEM)
                                            grvDetail.SetFocusedRowCellValue(colQTY, xloop.JUMLAH)
                                            grvDetail.SetFocusedRowCellValue(colKDCARAPAKAI, oItem.DefaultItem_CaraPakai)
                                            grvDetail.SetFocusedRowCellValue(colREMARKS, xloop.MEMO)
                                            grvDetail.UpdateCurrentRow()
                                        End If
                                    End If
                                End If
                            Next

                            Dim listDiagnosaSekunder As New List(Of String)
                            For Each xloop In oGrouperDataCppt.GetDataDetailDiagnosa(sFind1)
                                listDiagnosaSekunder.Add(xloop.MEMO)
                            Next

                            If listDiagnosaSekunder.Count > 0 Then
                                txtMEMO.Text = String.Join(vbCrLf, listDiagnosaSekunder.ToArray)
                            Else
                                txtMEMO.Text = dsCPPT.ASSEMENT_TEXT.Trim
                            End If
                        End If
                    End If
                ElseIf sCategoryBilling = 4 Then
                    If sFind1 <> "" Then
                        Dim dsCPPT = oGrouperDataCppt.GetData(sFind1)
                        If dsCPPT IsNot Nothing Then
                            fn_LoadKDKUNJUNGAN(dsCPPT.A_IDENTITASPASIEN_LIST.KDKUNJUNGAN, 3)
                            grdKDKUNJUNGAN.Text = dsCPPT.A_IDENTITASPASIEN_LIST.KDKUNJUNGAN

                            fn_LoadDataIdentitas(dsCPPT.A_IDENTITASPASIEN_LIST.KDKUNJUNGAN)
                            grdDOCTOR.Text = dsCPPT.A_IDENTITASPASIEN_LIST.KDDOCTOR

                            For Each xloop In oGrouperDataCppt.GetDataDetailNonRacikan(dsCPPT.KDCPPT)
                                grvDetail.Focus()
                                grvDetail.AddNewRow()
                                grvDetail.SetFocusedRowCellValue(colKDITEM, xloop.KDITEM)
                                grvDetail.SetFocusedRowCellValue(colISRACIK, False)
                                grvDetail.SetFocusedRowCellValue(colQTY, xloop.JUMLAH)
                                grvDetail.SetFocusedRowCellValue(colKDSIGNA, xloop.KDSIGNA)
                                grvDetail.SetFocusedRowCellValue(colKDCARAPAKAI, xloop.KDCARAPAKAI)
                                'If sCetakEtiketFarmasi = 1 Then
                                '    Dim dsCaraPakai = oCaraPakai.GetDataNama(xloop.CARAPAKAI)
                                '    If dsCaraPakai IsNot Nothing Then
                                '        If dsCaraPakai.MEMO = "-" Then
                                '            Try
                                '                grvDetail.SetFocusedRowCellValue(colKDCARAPAKAI, "CP_0000000016")
                                '            Catch ex As Exception
                                '                grvDetail.SetFocusedRowCellValue(colKDCARAPAKAI, dsCaraPakai.KDCARAPAKAI)
                                '            End Try
                                '        Else
                                '            grvDetail.SetFocusedRowCellValue(colKDCARAPAKAI, dsCaraPakai.KDCARAPAKAI)
                                '        End If
                                '    Else
                                '        grvDetail.SetFocusedRowCellValue(colKDCARAPAKAI, xloop.KDCARAPAKAI)
                                '    End If
                                'Else
                                '    grvDetail.SetFocusedRowCellValue(colKDCARAPAKAI, xloop.KDCARAPAKAI)
                                'End If

                                grvDetail.SetFocusedRowCellValue(colREMARKS, xloop.REMARKS_DOKTER)
                                grvDetail.SetFocusedRowCellValue(colGROUPRACIK, 0)

                                grvDetail.UpdateCurrentRow()
                            Next

                            Dim Racik As Integer = 0

                            For Each xloop In oGrouperDataCppt.GetDataDetailRacikan(dsCPPT.KDCPPT)
                                grvDetail.Focus()
                                grvDetail.AddNewRow()
                                grvDetail.SetFocusedRowCellValue(colKDITEM, xloop.KDITEM)
                                If xloop.KDITEM = "R999999" Then
                                    grvDetail.SetFocusedRowCellValue(colISRACIK, True)
                                    Racik += 1
                                Else
                                    grvDetail.SetFocusedRowCellValue(colISRACIK, False)
                                End If
                                grvDetail.SetFocusedRowCellValue(colQTY, xloop.JUMLAH)
                                grvDetail.SetFocusedRowCellValue(colKDSIGNA, oItem.DefaultItem_Signa)
                                grvDetail.SetFocusedRowCellValue(colKDCARAPAKAI, oItem.DefaultItem_CaraPakai)
                                grvDetail.SetFocusedRowCellValue(colREMARKS, xloop.SIGNA & " " & xloop.PERMINTAAN)

                                grvDetail.SetFocusedRowCellValue(colGROUPRACIK, Racik)

                                grvDetail.UpdateCurrentRow()
                            Next
                        End If
                    End If
                End If

                Calculate()

            Else
                Dim oOrder As New Digital.clsR_Order
                Dim dsOrder = oOrder.GetData(sFind2)
                If dsOrder IsNot Nothing Then
                    fn_LoadKDKUNJUNGAN(dsOrder.A_IDENTITASPASIEN_LIST.KDKUNJUNGAN, 3)
                    grdKDKUNJUNGAN.Text = dsOrder.A_IDENTITASPASIEN_LIST.KDKUNJUNGAN

                    fn_LoadDataIdentitas(dsOrder.A_IDENTITASPASIEN_LIST.KDKUNJUNGAN)

                    grdDOCTOR.Text = dsOrder.A_IDENTITASPASIEN_LIST.KDDOCTOR

                    If sCategoryBilling = 2 Then
                        Dim oOrderlab As New Order.clsOrderRanapLab

                        For Each xloop In oOrderlab.GetDataDetail(sFind2)
                            If xloop.ISPERAWAT = False Then
                                Dim dsItem = oItem.GetData(xloop.KDITEM)
                                If dsItem IsNot Nothing Then
                                    If dsItem.M_ITEM_L3.MEMO = "LABORATORIUM" Then
                                        grvDetail.Focus()
                                        grvDetail.AddNewRow()
                                        grvDetail.SetFocusedRowCellValue(colKDITEM, xloop.KDITEM)
                                        grvDetail.SetFocusedRowCellValue(colQTY, xloop.JUMLAH)
                                        grvDetail.SetFocusedRowCellValue(colKDCARAPAKAI, oItem.DefaultItem_CaraPakai)
                                        grvDetail.SetFocusedRowCellValue(colREMARKS, xloop.MEMO)
                                        grvDetail.UpdateCurrentRow()
                                    End If
                                End If
                            End If
                        Next

                    ElseIf sCategoryBilling = 3
                        Dim oOrderlab As New Order.clsOrderRanapRad

                        For Each xloop In oOrderlab.GetDataDetail(sFind2)
                            If xloop.ISPERAWAT = False Then
                                Dim dsItem = oItem.GetData(xloop.KDITEM)
                                If dsItem IsNot Nothing Then
                                    If dsItem.M_ITEM_L3.MEMO = "RADIOLOGI" Then
                                        grvDetail.Focus()
                                        grvDetail.AddNewRow()
                                        grvDetail.SetFocusedRowCellValue(colKDITEM, xloop.KDITEM)
                                        grvDetail.SetFocusedRowCellValue(colQTY, xloop.JUMLAH)
                                        grvDetail.SetFocusedRowCellValue(colKDCARAPAKAI, oItem.DefaultItem_CaraPakai)
                                        grvDetail.SetFocusedRowCellValue(colREMARKS, xloop.MEMO)
                                        grvDetail.UpdateCurrentRow()
                                    End If
                                End If
                            End If
                        Next

                    ElseIf sCategoryBilling = 4 Then
                        Dim oOrderlab As New Order.clsOrderRanapNonRacikan

                        For Each xloop In oOrderlab.GetDataDetail(sFind2)
                            grvDetail.Focus()
                            grvDetail.AddNewRow()
                            grvDetail.SetFocusedRowCellValue(colKDITEM, xloop.KDITEM)
                            grvDetail.SetFocusedRowCellValue(colISRACIK, False)
                            grvDetail.SetFocusedRowCellValue(colQTY, xloop.JUMLAH)
                            grvDetail.SetFocusedRowCellValue(colKDSIGNA, xloop.KDSIGNA)
                            grvDetail.SetFocusedRowCellValue(colKDCARAPAKAI, xloop.KDCARAPAKAI)
                            grvDetail.SetFocusedRowCellValue(colREMARKS, xloop.REMARKS_DOKTER)
                            grvDetail.SetFocusedRowCellValue(colGROUPRACIK, 0)

                            grvDetail.UpdateCurrentRow()
                        Next
                    End If

                    Calculate()
                End If
            End If
        End If

    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oSalesOrderTransaksi.GetData(sNoId)

            With ds
                txtKDSOTRANSAKSI.Text = .KDSOTRANSAKSI
                deDATE.DateTime = .DATE
                grdDOCTOR.Text = .KDDOCTOR

                fn_LoadKDKUNJUNGAN(.KDKUNJUNGAN, 3)
                grdKDKUNJUNGAN.Text = .KDKUNJUNGAN

                Dim oKunjungan As New Admission.clsPendaftaran
                Dim dsKunjungan = oKunjungan.GetDataKunjungan(grdKDKUNJUNGAN.EditValue)
                If dsKunjungan IsNot Nothing Then
                    txtNAMAPASIEN.Text = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY & " / " & dsKunjungan.S_PENDAFTARAN_H.KDCUSTOMER
                    txtTANGGALLAHIR.Text = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy")

                    grdKDDEPARTMENT_H.Text = dsKunjungan.KDDEPARTMENT
                    grdKDDOCTOR_H.Text = dsKunjungan.KDDOCTOR
                    If dsKunjungan.S_PENDAFTARAN_H.M_DAFTAR_L1.MEMO = "BPJS" Then
                        chkBPJS.Checked = True
                    Else
                        chkBPJS.Checked = False
                    End If

                    If dsKunjungan.S_PENDAFTARAN_H.CATEGORY = 1 Then
                        If dsKunjungan.S_PENDAFTARAN_H.NAIKRANAP <> "" Then
                            Dim oKelas As New Reference.clsKelasRawat
                            Dim dsKelasByName = oKelas.GetDataByMemo(dsKunjungan.S_PENDAFTARAN_H.NAIKRANAP)
                            If dsKelasByName IsNot Nothing Then
                                grdKDKELASRAWAT.Text = dsKelasByName.KDKELASRAWAT
                            Else
                                If dsKunjungan.M_DEPARTMENT.M_KELASRAWAT.KELOMPOKKELAS = "Non Kelas" Then
                                    grdKDKELASRAWAT.Text = dsKunjungan.S_PENDAFTARAN_H.KDKELASRAWAT
                                ElseIf dsKunjungan.M_DEPARTMENT.M_KELASRAWAT.KELOMPOKKELAS = ""
                                    grdKDKELASRAWAT.Text = dsKunjungan.S_PENDAFTARAN_H.KDKELASRAWAT
                                Else
                                    grdKDKELASRAWAT.Text = dsKunjungan.M_DEPARTMENT.KDKELASRAWAT
                                End If
                            End If
                        Else
                            If dsKunjungan.M_DEPARTMENT.M_KELASRAWAT.KELOMPOKKELAS = "Non Kelas" Then
                                grdKDKELASRAWAT.Text = dsKunjungan.S_PENDAFTARAN_H.KDKELASRAWAT
                            ElseIf dsKunjungan.M_DEPARTMENT.M_KELASRAWAT.KELOMPOKKELAS = ""
                                grdKDKELASRAWAT.Text = dsKunjungan.S_PENDAFTARAN_H.KDKELASRAWAT
                            Else
                                grdKDKELASRAWAT.Text = dsKunjungan.M_DEPARTMENT.KDKELASRAWAT
                            End If
                        End If
                    Else
                        If dsKunjungan.M_DEPARTMENT.M_KELASRAWAT.KELOMPOKKELAS = "Non Kelas" Then
                            grdKDKELASRAWAT.Text = dsKunjungan.S_PENDAFTARAN_H.KDKELASRAWAT
                        ElseIf dsKunjungan.M_DEPARTMENT.M_KELASRAWAT.KELOMPOKKELAS = ""
                            grdKDKELASRAWAT.Text = dsKunjungan.S_PENDAFTARAN_H.KDKELASRAWAT
                        Else
                            grdKDKELASRAWAT.Text = dsKunjungan.M_DEPARTMENT.KDKELASRAWAT
                        End If
                    End If

                    txtJenisPasien.Text = dsKunjungan.M_KESATUAN.GOL
                    Dim a, b, c As String
                    a = Year(dsKunjungan.DATE)
                    b = Year(dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)
                    c = a - b

                    txtUmur.Text = c & " tahun"
                    txtALAMAT.Text = dsKunjungan.ALAMAT
                    txtDiganosa.Text = dsKunjungan.S_PENDAFTARAN_H.M_DIAGNOSA.MEMO
                Else
                    txtNAMAPASIEN.ResetText()
                    txtTANGGALLAHIR.ResetText()
                    grdKDDEPARTMENT_H.ResetText()
                    grdKDDOCTOR_H.ResetText()
                    grdKDKELASRAWAT.ResetText()
                    txtJenisPasien.ResetText()
                    txtUmur.ResetText()
                    txtALAMAT.ResetText()
                    txtDiganosa.ResetText()
                End If

                grdKDWAREHOUSE.Text = .KDWAREHOUSE
                txtSUBTOTAL.Text = .SUBTOTAL
                txtDISCOUNT.Text = .DISCOUNT
                txtTAX.Text = .TAX
                txtGRANDTOTAL.Text = .GRANDTOTAL
                txtMEMO.Text = .MEMO
                txtTUSLAH.Text = .TUSLAH

                sKDCPPT = .KDCPPT

                fn_LoadObat()

                'bindingSource.DataSource = oSalesOrderTransaksi.GetDataDetail(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                'grdDetail.DataSource = bindingSource

                Dim oGrouperDataCppt As New Grouper.clsR_CPPT
                Dim oDoctor As New Reference.clsDoctor
                Dim oItem As New Reference.clsItem

                If sCategoryBilling = 2 Then
                    If sFind1 <> "" Then
                        Dim dsCPPT = oGrouperDataCppt.GetData(sFind1)
                        If dsCPPT IsNot Nothing Then
                            fn_LoadKDKUNJUNGAN(dsCPPT.A_IDENTITASPASIEN_LIST.KDKUNJUNGAN, 3)
                            grdKDKUNJUNGAN.Text = dsCPPT.A_IDENTITASPASIEN_LIST.KDKUNJUNGAN

                            fn_LoadDataIdentitas(dsCPPT.A_IDENTITASPASIEN_LIST.KDKUNJUNGAN)

                            grdDOCTOR.Text = dsCPPT.A_IDENTITASPASIEN_LIST.KDDOCTOR

                            For Each xloop In oGrouperDataCppt.GetDataDetailTindakan(dsCPPT.KDCPPT)
                                If xloop.ISPERAWAT = False Then
                                    Dim dsItem = oItem.GetData(xloop.KDITEM)
                                    If dsItem IsNot Nothing Then
                                        If dsItem.M_ITEM_L3.MEMO = "LABORATORIUM" Then
                                            grvDetail.Focus()
                                            grvDetail.AddNewRow()
                                            grvDetail.SetFocusedRowCellValue(colKDITEM, xloop.KDITEM)
                                            grvDetail.SetFocusedRowCellValue(colQTY, xloop.JUMLAH)
                                            grvDetail.SetFocusedRowCellValue(colKDCARAPAKAI, oItem.DefaultItem_CaraPakai)
                                            grvDetail.SetFocusedRowCellValue(colREMARKS, xloop.MEMO)
                                            grvDetail.UpdateCurrentRow()
                                        End If
                                    End If
                                End If

                            Next
                        End If
                    Else
                        bindingSource.DataSource = oSalesOrderTransaksi.GetDataDetail(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                        grdDetail.DataSource = bindingSource
                    End If
                ElseIf sCategoryBilling = 3 Then
                    If sFind1 <> "" Then
                        Dim dsCPPT = oGrouperDataCppt.GetData(sFind1)
                        If dsCPPT IsNot Nothing Then
                            fn_LoadKDKUNJUNGAN(dsCPPT.A_IDENTITASPASIEN_LIST.KDKUNJUNGAN, 3)
                            grdKDKUNJUNGAN.Text = dsCPPT.A_IDENTITASPASIEN_LIST.KDKUNJUNGAN

                            fn_LoadDataIdentitas(dsCPPT.A_IDENTITASPASIEN_LIST.KDKUNJUNGAN)
                            grdDOCTOR.Text = dsCPPT.A_IDENTITASPASIEN_LIST.KDDOCTOR

                            For Each xloop In oGrouperDataCppt.GetDataDetailTindakan(dsCPPT.KDCPPT)
                                If xloop.ISPERAWAT = False Then
                                    Dim dsItem = oItem.GetData(xloop.KDITEM)
                                    If dsItem IsNot Nothing Then
                                        If dsItem.M_ITEM_L3.MEMO = "RADIOLOGI" Then
                                            grvDetail.Focus()
                                            grvDetail.AddNewRow()
                                            grvDetail.SetFocusedRowCellValue(colKDITEM, xloop.KDITEM)
                                            grvDetail.SetFocusedRowCellValue(colQTY, xloop.JUMLAH)
                                            grvDetail.SetFocusedRowCellValue(colKDCARAPAKAI, oItem.DefaultItem_CaraPakai)
                                            grvDetail.SetFocusedRowCellValue(colREMARKS, xloop.MEMO)
                                            grvDetail.UpdateCurrentRow()
                                        End If
                                    End If
                                End If

                            Next

                            Dim listDiagnosaSekunder As New List(Of String)
                            For Each xloop In oGrouperDataCppt.GetDataDetailDiagnosa(sFind1)
                                listDiagnosaSekunder.Add(xloop.MEMO)
                            Next

                            If listDiagnosaSekunder.Count > 0 Then
                                txtMEMO.Text = String.Join(vbCrLf, listDiagnosaSekunder.ToArray)
                            Else
                                txtMEMO.Text = dsCPPT.ASSEMENT_TEXT.Trim
                            End If
                        End If
                    Else
                        bindingSource.DataSource = oSalesOrderTransaksi.GetDataDetail(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                        grdDetail.DataSource = bindingSource
                    End If
                ElseIf sCategoryBilling = 4 Then
                    If sFind1 <> "" Then
                        Dim dsCPPT = oGrouperDataCppt.GetData(sFind1)
                        If dsCPPT IsNot Nothing Then
                            fn_LoadKDKUNJUNGAN(dsCPPT.A_IDENTITASPASIEN_LIST.KDKUNJUNGAN, 3)
                            grdKDKUNJUNGAN.Text = dsCPPT.A_IDENTITASPASIEN_LIST.KDKUNJUNGAN

                            fn_LoadDataIdentitas(dsCPPT.A_IDENTITASPASIEN_LIST.KDKUNJUNGAN)
                            grdDOCTOR.Text = dsCPPT.A_IDENTITASPASIEN_LIST.KDDOCTOR

                            For Each xloop In oGrouperDataCppt.GetDataDetailNonRacikan(dsCPPT.KDCPPT)
                                grvDetail.Focus()
                                grvDetail.AddNewRow()
                                grvDetail.SetFocusedRowCellValue(colKDITEM, xloop.KDITEM)
                                grvDetail.SetFocusedRowCellValue(colQTY, xloop.JUMLAH)
                                grvDetail.SetFocusedRowCellValue(colKDSIGNA, xloop.KDSIGNA)
                                grvDetail.SetFocusedRowCellValue(colKDCARAPAKAI, xloop.KDCARAPAKAI)
                                'grvDetail.SetFocusedRowCellValue(colREMARKS, xloop.REMARKS_DOKTER)
                                Dim dsCekItem = oSalesOrderTransaksi.GetDataKDITEM(ds.KDSOTRANSAKSI, xloop.KDITEM)
                                If dsCekItem IsNot Nothing Then
                                    grvDetail.SetFocusedRowCellValue(colREMARKS, xloop.REMARKS_DOKTER & " Sudah diterima")
                                Else
                                    grvDetail.SetFocusedRowCellValue(colREMARKS, xloop.REMARKS_DOKTER)
                                End If
                                grvDetail.UpdateCurrentRow()
                            Next

                            Dim Racik As Integer = 0

                            For Each xloop In oGrouperDataCppt.GetDataDetailRacikan(dsCPPT.KDCPPT)
                                grvDetail.Focus()
                                grvDetail.AddNewRow()
                                grvDetail.SetFocusedRowCellValue(colKDITEM, xloop.KDITEM)
                                If xloop.KDITEM = "R999999" Then
                                    grvDetail.SetFocusedRowCellValue(colISRACIK, True)
                                    Racik += 1
                                Else
                                    grvDetail.SetFocusedRowCellValue(colISRACIK, False)
                                End If
                                grvDetail.SetFocusedRowCellValue(colQTY, xloop.JUMLAH)
                                grvDetail.SetFocusedRowCellValue(colKDSIGNA, oItem.DefaultItem_Signa)
                                grvDetail.SetFocusedRowCellValue(colKDCARAPAKAI, oItem.DefaultItem_CaraPakai)
                                'grvDetail.SetFocusedRowCellValue(colREMARKS, xloop.SIGNA & " " & xloop.PERMINTAAN)

                                Dim dsCekItem = oSalesOrderTransaksi.GetDataKDITEM(ds.KDSOTRANSAKSI, xloop.KDITEM)
                                If dsCekItem IsNot Nothing Then
                                    grvDetail.SetFocusedRowCellValue(colREMARKS, xloop.SIGNA & " " & xloop.PERMINTAAN & " Sudah diterima")
                                Else
                                    grvDetail.SetFocusedRowCellValue(colREMARKS, xloop.SIGNA & " " & xloop.PERMINTAAN)
                                End If

                                grvDetail.SetFocusedRowCellValue(colGROUPRACIK, Racik)

                                grvDetail.UpdateCurrentRow()
                            Next
                            'For Each xloop In oGrouperDataCppt.GetDataDetailRacikan(dsCPPT.KDCPPT)
                            '    grvDetail.Focus()
                            '    grvDetail.AddNewRow()
                            '    grvDetail.SetFocusedRowCellValue(colKDITEM, xloop.KDITEM)
                            '    grvDetail.SetFocusedRowCellValue(colQTY, xloop.JUMLAH)
                            '    grvDetail.SetFocusedRowCellValue(colKDSIGNA, oItem.DefaultItem_Signa)
                            '    grvDetail.SetFocusedRowCellValue(colKDCARAPAKAI, oItem.DefaultItem_CaraPakai)
                            '    grvDetail.SetFocusedRowCellValue(colREMARKS, xloop.SIGNA & " " & xloop.PERMINTAAN)
                            '    grvDetail.UpdateCurrentRow()
                            'Next
                        End If
                    Else

                        bindingSource.DataSource = oSalesOrderTransaksi.GetDataDetail(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                        grdDetail.DataSource = bindingSource


                    End If
                Else
                    bindingSource.DataSource = oSalesOrderTransaksi.GetDataDetail(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                    grdDetail.DataSource = bindingSource
                End If
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If grdDOCTOR.Text = String.Empty Then
                grdDOCTOR.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdDOCTOR.ErrorText = Statement.ErrorRequired

                grdDOCTOR.Focus()
                fn_Validate = False
                Exit Function
            End If

            If grdKDKUNJUNGAN.Text = String.Empty Then
                grdKDKUNJUNGAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDKUNJUNGAN.ErrorText = Statement.ErrorRequired

                grdKDKUNJUNGAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtNAMAPASIEN.Text = String.Empty Then
                txtNAMAPASIEN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtNAMAPASIEN.ErrorText = Statement.ErrorRequired

                txtNAMAPASIEN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If chkBHP.Checked = True Then
                If grdKDWAREHOUSE.Text = String.Empty Then
                    grdKDWAREHOUSE.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    grdKDWAREHOUSE.ErrorText = Statement.ErrorRequired

                    grdKDWAREHOUSE.Focus()
                    fn_Validate = False
                    Exit Function
                End If
            End If

            If sCategoryBilling = 3 Then
                If txtMEMO.Text = "" Then
                    MsgBox("Isi Catatan Terlebih dahulu untuk diagnosa", MsgBoxStyle.Exclamation, Me.Text)

                    fn_Validate = False
                    Exit Function
                End If
            End If

            grvDetail.UpdateCurrentRow()

            If grvDetail.RowCount < 2 Then
                MsgBox(Statement.ErrorDetail, MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End If

            Dim sSubTotal As Decimal = 0
            For i As Integer = 0 To grvDetail.RowCount - 2
                sSubTotal += CDec(grvDetail.GetRowCellValue(i, colGRANDTOTAL))
            Next

            If sSubTotal <> CDec(txtSUBTOTAL.Text) Then
                MsgBox("Subtotal tidak sama dengan yang Grand Total", MsgBoxStyle.Exclamation, Me.Text)

                fn_Validate = False
                Exit Function
            End If

            Dim JumlahInput As Integer = 0
            Dim JumlahOrder As Integer = 0

            For i As Integer = 0 To grvDetail.RowCount - 2
                JumlahInput += 1
            Next

            If sKDCPPT <> "" Then
                Try
                    Dim oConn As New SqlConnection
                    Dim oComm As New SqlCommand
                    Dim da As SqlDataAdapter
                    Dim ds As New DataSet
                    Dim SQL As String

                    Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\RME\", "Database", "").ToString())

                    oConn = New SqlConnection(sConn)
                    If oConn.State = ConnectionState.Closed Then
                        oConn.Open()
                    End If

                    SQL = "EXEC ORDERCEK_ITEM "
                    SQL &= " @CATEGORY = '" & sCategoryBilling & "' "
                    SQL &= ", @KODE = '" & sKDCPPT & "' "

                    oComm.Connection = oConn
                    oComm.CommandText = SQL
                    oComm.CommandTimeout = 120
                    oComm.CommandType = CommandType.Text

                    da = New SqlDataAdapter(oComm)
                    da.Fill(ds, "ORDERPENUNJANG")

                    If oConn.State = ConnectionState.Open Then
                        oConn.Close()
                    End If

                    For iLoop As Integer = 0 To ds.Tables("ORDERPENUNJANG").Rows.Count - 1
                        JumlahOrder += 1
                    Next

                    If JumlahOrder <> 0 Then
                        If JumlahOrder <> JumlahInput Then
                            MsgBox("Jumlah Order Tidak sama dengan Jumlah yg di Input", MsgBoxStyle.Exclamation, Me.Text)

                            If MsgBox("Tekan Yes melanjutkan, Tekan No Memperbaiki", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then
                                fn_Validate = False
                                Exit Function
                            End If

                        End If
                    End If

                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_SaveCashin(ByVal sNOINVOICE As String, ByVal sCATEGORY As Integer) As Boolean
        If sBayar = False Then Exit Function

        Try
            Dim sNoIdCashin As String = ""
            Dim oCashin As New Finance.clsCashIn

            Dim dsNoidCashin = oCashin.GetDataDetailFirst(sNOINVOICE)
            If dsNoidCashin IsNot Nothing Then
                sNoIdCashin = dsNoidCashin.KDCASHIN
            End If
            ' ***** HEADER *****
            Dim ds = oCashin.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oCashin.GetData(sNoIdCashin).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDCASHIN = sNoIdCashin
                .CATEGORY = sCATEGORY
                .DATE = deDATE.DateTime
                .KDPENDAFTARAN = oSalesOrderTransaksi.GetData(sNOINVOICE).S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.KDPENDAFTARAN
                .KDPAYMENTTYPE = "PAYMENTTYPE_0000000001"
                .MEMO = "INVOICE NO. : "
                .SUBTOTAL = CDec(txtSUBTOTAL.Text)
                'Discount = Admin
                .ADMIN = CDec(0)
                'Round = Tax (Biaya racik)
                .ROUND = CDec(txtTAX.Text)
                .COSTSHARE = CDec(0)
                .DEPOSIT = CDec(0)
                .GRANDTOTAL = CDec(txtGRANDTOTAL.Text)
                Try
                    .ISSETOR = oCashin.GetData(sNoId).ISSETOR
                Catch oErr As Exception
                    .ISSETOR = False
                End Try
                Try
                    .KDUSER = oCashin.GetData(sNoId).KDUSER
                Catch ex As Exception
                    .KDUSER = sUserID
                End Try
                .KDSHIFT = sSHIFT
            End With

            '' ***** DETIL *****
            'Dim arrDetail = oCashin.GetStructureDetailList
            'For i As Integer = 0 To grvDetail.RowCount - 2
            '    Dim dsDetail = oCashin.GetStructureDetail
            '    With dsDetail
            '        Try
            '            .DATECREATED = oCashin.GetData(sNoIdCashin).DATECREATED
            '        Catch oErr As Exception
            '            .DATECREATED = Now
            '        End Try
            '        .DATEUPDATED = Now

            '        .SEQ = i
            '        .KDCASHIN = ds.KDCASHIN
            '        .NOINVOICE = sNOINVOICE

            '        ds.MEMO &= .NOINVOICE & ", "
            '        .AMOUNTORIGINAL = CDec(txtGRANDTOTAL.Text)
            '        .AMOUNTDUE = CDec(0)
            '        .AMOUNTPAYMENT = CDec(txtGRANDTOTAL.Text)
            '        .REMARKS = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colREMARKS)), "-", grvDetail.GetRowCellValue(i, colREMARKS))
            '    End With
            '    arrDetail.Add(dsDetail)
            'Next

            Dim arrDetail = oCashin.GetStructureDetailList
            Dim dsDetail = oCashin.GetStructureDetail
            With dsDetail
                Try
                    .DATECREATED = oCashin.GetData(sNoIdCashin).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .SEQ = i
                .KDCASHIN = ds.KDCASHIN
                .NOINVOICE = sNOINVOICE

                ds.MEMO &= .NOINVOICE & ", "
                .AMOUNTORIGINAL = CDec(txtGRANDTOTAL.Text)
                .AMOUNTDUE = CDec(0)
                .AMOUNTPAYMENT = CDec(txtGRANDTOTAL.Text)
                .REMARKS = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colREMARKS)), "-", grvDetail.GetRowCellValue(i, colREMARKS))
            End With
            arrDetail.Add(dsDetail)

            If sNoIdCashin = "" Then
                Try
                    Dim sKDCASHIN As String = ""
                    sKDCASHIN = oCashin.InsertData(ds, arrDetail)
                    If sKDCASHIN = "" Then
                        fn_SaveCashin = False
                    Else
                        fn_SaveCashin = True
                        fn_CetakKwitansi(sKDCASHIN)
                    End If
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf sNoIdCashin <> "" Then
                Try
                    fn_SaveCashin = oCashin.UpdateData(ds, arrDetail)
                    fn_CetakKwitansi(sNoIdCashin)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveCashin = False
        End Try
    End Function
    Private Sub fn_CetakKwitansi(ByVal sKDCASIN As String)
        Try
            If sKDCASIN = String.Empty Then Exit Sub
            Dim oPendaftaran As New Admission.clsPendaftaran
            Dim oPulang As New Admission.clsUpdate_Tanggal_Pulang
            Dim oCashin As New Finance.clsCashIn

            Dim dsCashin = oCashin.GetData(sKDCASIN)

            Dim dsDaftar = oPendaftaran.GetData(dsCashin.KDPENDAFTARAN)

            If dsDaftar.CATEGORY = 1 Then
                Dim dsPulang = oPulang.GetDatabyKD(dsDaftar.KDPENDAFTARAN)
                If dsPulang Is Nothing Then
                    'MsgBox("Pasien belum Pulang", MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                Else
                    sPrintGrandTotal = dsCashin.GRANDTOTAL
                End If
            Else
                sPrintGrandTotal = dsCashin.GRANDTOTAL
            End If

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

            SQL = " SELECT  "
            SQL &= " A.KDCASHIN "
            SQL &= " ,CATEGORY = (SELECT CATEGORY FROM S_PENDAFTARAN_H WHERE A.KDPENDAFTARAN = KDPENDAFTARAN) "
            SQL &= " ,KDPENDAFTARAN = (SELECT AA.KDPENDAFTARAN FROM S_PENDAFTARAN_H AA INNER JOIN S_PENDAFTARAN_KUNJUNGAN BB ON AA.KDPENDAFTARAN = BB.KDPENDAFTARAN WHERE C.KDKUNJUNGAN = BB.KDKUNJUNGAN) "
            SQL &= " ,TUJUAN = (SELECT BB.NAME_DISPLAY FROM S_PENDAFTARAN_H AA INNER JOIN M_DEPARTMENT BB ON AA.KDDEPARTMENT = BB.KDDEPARTMENT WHERE A.KDPENDAFTARAN = AA.KDPENDAFTARAN)  "
            SQL &= " ,DPJP = (SELECT BB.NAME_DISPLAY FROM S_PENDAFTARAN_H AA INNER JOIN M_DOCTOR BB ON AA.KDDOCTOR = BB.KDDOCTOR WHERE A.KDPENDAFTARAN = AA.KDPENDAFTARAN) "
            SQL &= " ,KDCUSTOMER = (SELECT KDCUSTOMER FROM S_PENDAFTARAN_H WHERE A.KDPENDAFTARAN = KDPENDAFTARAN) "
            SQL &= " ,PASIEN = (SELECT BB.NAME_DISPLAY FROM S_PENDAFTARAN_H AA INNER JOIN M_CUSTOMER BB ON AA.KDCUSTOMER = BB.KDCUSTOMER WHERE A.KDPENDAFTARAN = AA.KDPENDAFTARAN)  "
            SQL &= " ,ALAMAT = (SELECT TOP 1 ALAMAT FROM S_PENDAFTARAN_KUNJUNGAN WHERE A.KDPENDAFTARAN = KDPENDAFTARAN) "
            SQL &= " ,KELAS = (SELECT BB.MEMO FROM S_PENDAFTARAN_H AA INNER JOIN M_KELASRAWAT BB ON AA.KDKELASRAWAT = BB.KDKELASRAWAT WHERE A.KDPENDAFTARAN = AA.KDPENDAFTARAN)  "
            SQL &= " ,TANGGAL_DATANG = (SELECT DATE FROM S_PENDAFTARAN_H WHERE A.KDPENDAFTARAN = KDPENDAFTARAN) "
            SQL &= " ,TANGGAL_PULANG = ISNULL((SELECT DATE FROM T_UPDATE_TANGGAL_PULANG WHERE A.KDPENDAFTARAN = KDPENDAFTARAN), GETDATE()) "
            'SQL &= " ,B.NOINVOICE "
            SQL &= " ,A.SUBTOTAL "
            SQL &= " ,A.COSTSHARE "
            SQL &= " ,A.DEPOSIT "
            SQL &= " ,A.ADMIN "
            SQL &= " ,A.ROUND "
            SQL &= " ,A.GRANDTOTAL "
            SQL &= " ,ITEM_GROUP = F.MEMO "
            If dsDaftar.M_DAFTAR_L1.MEMO = "BPJS" Then
                SQL &= " ,ITEM = E.NMITEM2 "
            Else
                SQL &= " ,ITEM = E.NMITEM2  + ' (' + (SELECT NAME_DISPLAY FROM M_DOCTOR WHERE D.KDDOCTOR = KDDOCTOR) + ')' "
            End If
            SQL &= " ,QTY = SUM(D.QTY) "
            SQL &= " ,GRANDTOTAL_DETAIL = SUM(D.GRANDTOTAL)  "
            SQL &= " FROM F_CASHIN_H A  "
            SQL &= " INNER JOIN F_CASHIN_D B "
            SQL &= " ON A.KDCASHIN = B.KDCASHIN "
            SQL &= " INNER JOIN S_SO_TRANSAKSI_H C  "
            SQL &= " ON B.NOINVOICE = C.KDSOTRANSAKSI "
            SQL &= " INNER JOIN S_SO_TRANSAKSI_D D "
            SQL &= " ON C.KDSOTRANSAKSI = D.KDSOTRANSAKSI "
            SQL &= " INNER JOIN M_ITEM E "
            SQL &= " ON D.KDITEM = E.KDITEM "
            SQL &= " INNER JOIN M_ITEM_L2 F "
            SQL &= " ON E.KDITEM_L2 = F.KDITEM_L2  "
            SQL &= " WHERE "
            SQL &= " A.KDCASHIN = '" & sKDCASIN & "' "
            SQL &= "GROUP BY "
            SQL &= " A.KDCASHIN "
            SQL &= " ,A.KDPENDAFTARAN "
            SQL &= " ,C.KDKUNJUNGAN "
            SQL &= " ,A.KDPENDAFTARAN "
            'SQL &= " ,B.NOINVOICE "
            SQL &= " ,A.SUBTOTAL "
            SQL &= " ,A.COSTSHARE "
            SQL &= " ,A.DEPOSIT "
            SQL &= " ,A.ADMIN "
            SQL &= " ,A.ROUND "
            SQL &= " ,A.GRANDTOTAL "
            SQL &= " ,F.MEMO "
            SQL &= " ,E.NMITEM2 "
            SQL &= " ,A.DATE "
            If dsDaftar.M_DAFTAR_L1.MEMO = "BPJS" Then
                'SQL &= " ,ITEM = E.NMITEM2 "
            Else
                SQL &= " ,D.KDDOCTOR "
            End If
            SQL &= " ORDER BY A.DATE "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

            Dim listTranskasi As New List(Of R_CASHIN)

            For iLoop As Integer = 0 To ds.Tables("ALL").Rows.Count - 1
                Dim dsRekap As New DataAccess.R_CASHIN

                With ds.Tables("ALL")
                    dsRekap.KDCASHIN = .Rows(iLoop)("KDCASHIN")
                    dsRekap.CATEGORY = .Rows(iLoop)("CATEGORY")
                    dsRekap.KDPENDAFTARAN = .Rows(iLoop)("KDPENDAFTARAN")
                    dsRekap.KDCUSTOMER = .Rows(iLoop)("KDCUSTOMER")
                    dsRekap.TUJUAN = .Rows(iLoop)("TUJUAN")
                    dsRekap.DPJP = .Rows(iLoop)("DPJP")
                    dsRekap.PASIEN = .Rows(iLoop)("PASIEN")
                    dsRekap.ALAMAT = .Rows(iLoop)("ALAMAT")
                    dsRekap.KELAS = .Rows(iLoop)("KELAS")
                    dsRekap.TANGGAL_DATANG = .Rows(iLoop)("TANGGAL_DATANG")
                    dsRekap.TANGGAL_PULANG = .Rows(iLoop)("TANGGAL_PULANG")
                    dsRekap.NOINVOICE = ""
                    dsRekap.SUBTOTAL = .Rows(iLoop)("SUBTOTAL")
                    dsRekap.COSTSHARE = .Rows(iLoop)("COSTSHARE")
                    dsRekap.DEPOSIT = .Rows(iLoop)("DEPOSIT")
                    dsRekap.ADMIN = .Rows(iLoop)("ADMIN")
                    dsRekap.ROUND = .Rows(iLoop)("ROUND")
                    dsRekap.GRANDTOTAL = .Rows(iLoop)("GRANDTOTAL")
                    dsRekap.ITEM_GROUP = .Rows(iLoop)("ITEM_GROUP")
                    dsRekap.ITEM = .Rows(iLoop)("ITEM")
                    dsRekap.ITEM_GROUP = .Rows(iLoop)("ITEM_GROUP")
                    dsRekap.QTY = .Rows(iLoop)("QTY")
                    dsRekap.GRANDTOTAL_DETAIL = .Rows(iLoop)("GRANDTOTAL_DETAIL")

                    listTranskasi.Add(dsRekap)
                End With
            Next

            If sCategoryBilling = 2 Or sCategoryBilling = 3 Then
                Dim rpt As New xtraCashIn_Kecil

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.bindingSource.DataSource = listTranskasi
                Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)

            Else
                Dim rpt As New xtraCashIn

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.bindingSource.DataSource = listTranskasi
                Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)

            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Save(ByVal iscetak As Boolean) As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oSalesOrderTransaksi.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oSalesOrderTransaksi.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDSOTRANSAKSI = sNoId
                .CATEGORY = sCategoryBilling
                .DATE = deDATE.DateTime
                .KDKUNJUNGAN = IIf(String.IsNullOrEmpty(grdKDKUNJUNGAN.EditValue), String.Empty, grdKDKUNJUNGAN.EditValue)
                .KDWAREHOUSE = IIf(String.IsNullOrEmpty(grdKDWAREHOUSE.EditValue), String.Empty, grdKDWAREHOUSE.EditValue)
                .ISBHP = chkBHP.Checked
                .SUBTOTAL = CDec(txtSUBTOTAL.Text)
                .DISCOUNT = CDec(txtDISCOUNT.Text)
                .TAX = CDec(txtTAX.Text)
                .GRANDTOTAL = CDec(txtGRANDTOTAL.Text)
                Try
                    .PAYAMOUNT = oSalesOrderTransaksi.GetData(sNoId).PAYAMOUNT
                Catch oErr As Exception
                    .PAYAMOUNT = CDec(0)
                End Try
                .MEMO = txtMEMO.Text.Trim.ToUpper
                Try
                    .KDUSER = oSalesOrderTransaksi.GetData(sNoId).KDUSER
                Catch ex As Exception
                    .KDUSER = sUserID
                End Try
                '.KDSHIFT = sSHIFT
                .KDSHIFT = sSHIFT
                .KDDOCTOR = grdDOCTOR.EditValue
                .TUSLAH = CDec(txtTUSLAH.Text)

                Try
                    .KDCPPT = oSalesOrderTransaksi.GetData(sNoId).KDCPPT
                Catch ex As Exception
                    .KDCPPT = sKDCPPT
                End Try
                Try
                    .KDORDER = oSalesOrderTransaksi.GetData(sNoId).KDORDER
                Catch ex As Exception
                    .KDORDER = sFind2
                End Try
            End With

            Dim oItem As New Reference.clsItem

            ' ***** DETIL *****
            Dim arrDetail = oSalesOrderTransaksi.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oSalesOrderTransaksi.GetStructureDetail
                With dsDetail
                    .DATECREATED = deDATE.DateTime
                    .DATEUPDATED = Now
                    .SEQ = i
                    .KDSOTRANSAKSI = ds.KDSOTRANSAKSI
                    .KDITEM = grvDetail.GetRowCellValue(i, colKDITEM)
                    .KDUOM = grvDetail.GetRowCellValue(i, colKDUOM)
                    .KDSIGNA = grvDetail.GetRowCellValue(i, colKDSIGNA)
                    .KDCARAPAKAI = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colKDCARAPAKAI)), oItem.DefaultItem_CaraPakai, grvDetail.GetRowCellValue(i, colKDCARAPAKAI))
                    .QTY = CDec(grvDetail.GetRowCellValue(i, colQTY))
                    .ISRACIK = CBool(grvDetail.GetRowCellValue(i, colISRACIK))
                    .PRICE = CDec(grvDetail.GetRowCellValue(i, colPRICE))
                    .SUBTOTAL = CDec(grvDetail.GetRowCellValue(i, colSUBTOTAL))
                    .DISCOUNT = CDec(grvDetail.GetRowCellValue(i, colDISCOUNT))
                    .GRANDTOTAL = CDec(grvDetail.GetRowCellValue(i, colGRANDTOTAL))
                    .KDDOCTOR = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colKDDOCTOR)), grdKDDOCTOR_H.EditValue, IIf(sCategoryBilling = 4, grdKDDOCTOR_H.EditValue, grvDetail.GetRowCellValue(i, colKDDOCTOR)))
                    .KDDEPARTMENT = grvDetail.GetRowCellValue(i, colKDDEPARTMENT)
                    .REMARKS = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colREMARKS)), "-", grvDetail.GetRowCellValue(i, colREMARKS))
                    .ISCETAKETIKET = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colISCETAKETIKET)), True, grvDetail.GetRowCellValue(i, colISCETAKETIKET))
                    .GROUPRACIK = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colGROUPRACIK)), 0, grvDetail.GetRowCellValue(i, colGROUPRACIK))
                    .KDUSER = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colKDUSER)), sUserID, grvDetail.GetRowCellValue(i, colKDUSER))
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    Dim sKDSOTRANSAKSI = oSalesOrderTransaksi.InsertData(ds, arrDetail)
                    If sKDSOTRANSAKSI = "" Then
                        fn_Save = False
                    Else
                        fn_Save = True

                        txtKDSOTRANSAKSI.Text = sKDSOTRANSAKSI

                        oSalesOrderTransaksi.UpdateDPJP(oSalesOrderTransaksi.GetData(txtKDSOTRANSAKSI.Text).S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.KDPENDAFTARAN, grdKDDOCTOR_H.EditValue)

                        If chkBPJS.Checked = False Then
                            If sCategoryBilling = 0 Then
                                Dim oPoli As New Reference.clsDepartment
                                Dim dsPoli = oPoli.GetData(grdKDDEPARTMENT_H.EditValue)

                                If dsPoli.VCLAIM_KODEPOLI = "IGD" Then

                                Else
                                    fn_SaveCashin(txtKDSOTRANSAKSI.Text.ToString, 0)
                                End If
                            ElseIf sCategoryBilling = 2 Then
                                If ds.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.CATEGORY = 0 Then
                                    fn_SaveCashin(txtKDSOTRANSAKSI.Text.ToString, 0)
                                End If
                            ElseIf sCategoryBilling = 3 Then
                                If ds.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.CATEGORY = 0 Then
                                    fn_SaveCashin(txtKDSOTRANSAKSI.Text.ToString, 0)
                                End If
                            ElseIf sCategoryBilling = 4 Then
                                fn_SaveCashin(txtKDSOTRANSAKSI.Text.ToString, 2)
                            End If
                        End If
                    End If
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oSalesOrderTransaksi.UpdateData(ds, arrDetail)

                    oSalesOrderTransaksi.UpdateDPJP(oSalesOrderTransaksi.GetData(txtKDSOTRANSAKSI.Text).S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.KDPENDAFTARAN, grdKDDOCTOR_H.EditValue)

                    If chkBPJS.Checked = False Then
                        If sCategoryBilling = 0 Then
                            fn_SaveCashin(txtKDSOTRANSAKSI.Text.ToString, 0)
                        ElseIf sCategoryBilling = 2 Then
                            If grdKDKUNJUNGAN.Text.ToString.Contains("RJ") Then
                                fn_SaveCashin(txtKDSOTRANSAKSI.Text.ToString, 0)
                            End If
                        ElseIf sCategoryBilling = 3 Then
                            If grdKDKUNJUNGAN.Text.ToString.Contains("RJ") Then
                                fn_SaveCashin(txtKDSOTRANSAKSI.Text.ToString, 0)
                            End If
                        ElseIf sCategoryBilling = 4 Then
                            fn_SaveCashin(txtKDSOTRANSAKSI.Text.ToString, 2)
                        End If
                    End If

                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

            If fn_Save = True Then
                sFind3 = txtKDSOTRANSAKSI.Text

                If sKDCPPT <> "" Then
                    Dim oCPPT As New Grouper.clsR_CPPT

                    If sCategoryBilling = 2 Then
                        For Each xloop In oCPPT.GetDataDetailTindakan(sKDCPPT)
                            Dim dsItem = oItem.GetData(xloop.KDITEM)
                            If dsItem IsNot Nothing Then
                                If dsItem.M_ITEM_L3.MEMO = "LABORATORIUM" Then
                                    oCPPT.UpdateIsBaca(xloop.KDCPPT, xloop.SEQ)
                                End If
                            End If
                        Next
                    End If
                    If sCategoryBilling = 3 Then
                        For Each xloop In oCPPT.GetDataDetailTindakan(sKDCPPT)
                            Dim dsItem = oItem.GetData(xloop.KDITEM)
                            If dsItem IsNot Nothing Then
                                If dsItem.M_ITEM_L3.MEMO = "RADIOLOGI" Then
                                    oCPPT.UpdateIsBaca(xloop.KDCPPT, xloop.SEQ)
                                End If
                            End If
                        Next
                    End If
                    If sCategoryBilling = 4 Then
                        For Each xloop In oCPPT.GetDataDetailNonRacikan(sKDCPPT)
                            Dim dsItem = oItem.GetData(xloop.KDITEM)
                            If dsItem IsNot Nothing Then
                                oCPPT.UpdateIsBacaNonRacikan(xloop.KDCPPT, xloop.SEQ)
                            End If
                        Next
                        For Each xloop In oCPPT.GetDataDetailRacikan(sKDCPPT)
                            Dim dsItem = oItem.GetData(xloop.KDITEM)
                            If dsItem IsNot Nothing Then
                                oCPPT.UpdateIsBacaRacikan(xloop.KDCPPT, xloop.SEQ)
                            End If
                        Next
                    End If
                End If

                If sCategoryBilling = 4 Then
                    Dim oOpname As New Inventory.clsOpname
                    Dim oPI As New Purchasing.clsPurchaseInvoice
                    Dim oRME As New RME.clsRME

                    If iscetak = True Then
                        Dim RI As Integer = 0
                        Dim AdaRacikan As Integer = 0

                        Dim arrDetailEtiket = oSalesOrderTransaksi.GetStructureDetailEtiketList

                        For Each xloop In oSalesOrderTransaksi.GetDataDetail(ds.KDSOTRANSAKSI)
                            If xloop.GROUPRACIK = 0 Then
                                If xloop.M_ITEM.M_ITEM_L3.MEMO <> "BHP" Then
                                    Dim dsEtiket = oSalesOrderTransaksi.GetStructureHeaderEtiket
                                    With dsEtiket
                                        RI = xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.CATEGORY

                                        If xloop.ISCETAKETIKET = True Then
                                            If sCetakEtiketFarmasi = 1 Then
                                                .DESCRIPTION = xloop.KDSOTRANSAKSI & "   " & xloop.S_SO_TRANSAKSI_H.DATE.ToString("dd-MM-yyyy")
                                                .DOKTER = xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.M_DEPARTMENT.NAME_DISPLAY & " / " & xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_DAFTAR_L1.MEMO
                                                .EXPIREOBAT = xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY & " / " & xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.KDCUSTOMER
                                                .NAMA = xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy") & " / " & oRME.GetUmurPasien(xloop.S_SO_TRANSAKSI_H.DATE, xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)
                                                .NORESEP = xloop.M_CARAPAKAI.MEMO
                                                .NAMAOBAT = (xloop.M_ITEM.NMITEM2 & " / " & "Jml:" & FormatNumber(xloop.QTY, 0)).ToString.Trim
                                                .RUANGAN = (IIf(xloop.M_SIGNA.MEMO = "-", "", xloop.M_SIGNA.MEMO) & " " & xloop.REMARKS).ToString.Trim

                                                Dim rpt As New xtraEtiket1
                                                rpt.ShowPrintMarginsWarning = False
                                                rpt.Watermark.Text = sWATERMARK
                                                rpt.BindingSource.DataSource = dsEtiket
                                                Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                                                printTool.Print()
                                            ElseIf sCetakEtiketFarmasi = 2 Then
                                                .DESCRIPTION = xloop.KDSOTRANSAKSI & "   " & xloop.S_SO_TRANSAKSI_H.DATE.ToString("dd-MM-yyyy")
                                                .DOKTER = xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.M_DEPARTMENT.NAME_DISPLAY & " / " & xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_DAFTAR_L1.MEMO
                                                .EXPIREOBAT = xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY & " / " & xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.KDCUSTOMER
                                                .NAMA = xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy") & " / " & oRME.GetUmurPasien(xloop.S_SO_TRANSAKSI_H.DATE, xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)
                                                .NORESEP = xloop.M_CARAPAKAI.MEMO
                                                .NAMAOBAT = (xloop.M_ITEM.NMITEM2 & " / " & "Jml:" & FormatNumber(xloop.QTY, 0)).ToString.Trim
                                                .RUANGAN = (IIf(xloop.M_SIGNA.MEMO = "-", "", xloop.M_SIGNA.MEMO) & " " & xloop.REMARKS).ToString.Trim

                                                Dim rpt As New xtraEtiket2
                                                rpt.ShowPrintMarginsWarning = False
                                                rpt.Watermark.Text = sWATERMARK
                                                rpt.BindingSource.DataSource = dsEtiket
                                                Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                                                printTool.Print()
                                            Else
                                                .DESCRIPTION = xloop.KDSOTRANSAKSI & "   " & xloop.S_SO_TRANSAKSI_H.DATE.ToString("dd-MM-yyyy")
                                                .DOKTER = xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.M_DEPARTMENT.NAME_DISPLAY & " / " & xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_DAFTAR_L1.MEMO
                                                .EXPIREOBAT = xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY & " / " & xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.KDCUSTOMER
                                                .NAMA = xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy") & " / " & oRME.GetUmurPasien(xloop.S_SO_TRANSAKSI_H.DATE, xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)
                                                .NORESEP = xloop.M_ITEM.M_ITEM_L6.MEMO & "          " & "Sebelum / Sesudah Makan"
                                                .NAMAOBAT = (xloop.M_ITEM.NMITEM2 & " / " & "Jml:" & FormatNumber(xloop.QTY, 0)).ToString.Trim
                                                .RUANGAN = (IIf(xloop.M_SIGNA.MEMO = "-", "", xloop.M_SIGNA.MEMO) & " " & IIf(xloop.M_CARAPAKAI.MEMO = "-", "", xloop.M_CARAPAKAI.MEMO) & " " & xloop.REMARKS).ToString.Trim

                                                Dim rpt As New xtraEtiket
                                                rpt.ShowPrintMarginsWarning = False
                                                rpt.Watermark.Text = sWATERMARK
                                                rpt.BindingSource.DataSource = dsEtiket
                                                Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                                                printTool.Print()
                                            End If

                                        End If
                                    End With
                                Else

                                    Dim dsDetailEtiket = oSalesOrderTransaksi.GetStructureDetailEtiket
                                    With dsDetailEtiket
                                        .DESCRIPTION = xloop.KDSOTRANSAKSI & "   " & xloop.S_SO_TRANSAKSI_H.DATE.ToString("dd-MM-yyyy")
                                        .DOKTER = xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.M_DEPARTMENT.NAME_DISPLAY & " / " & xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_DAFTAR_L1.MEMO
                                        .EXPIREOBAT = xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY & " / " & xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.KDCUSTOMER
                                        .NAMA = xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy") & " / " & oRME.GetUmurPasien(xloop.S_SO_TRANSAKSI_H.DATE, xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)
                                        .NORESEP = xloop.M_CARAPAKAI.MEMO
                                        .NAMAOBAT = (xloop.M_ITEM.NMITEM2 & " / " & "Jml:" & FormatNumber(xloop.QTY, 0)).ToString.Trim
                                        .RUANGAN = (IIf(xloop.M_SIGNA.MEMO = "-", "", xloop.M_SIGNA.MEMO) & " " & xloop.REMARKS).ToString.Trim
                                    End With
                                    arrDetailEtiket.Add(dsDetailEtiket)
                                End If
                            Else
                                AdaRacikan = xloop.GROUPRACIK
                            End If
                        Next

                        If arrDetailEtiket.Count > 0 Then
                            Dim rptList As New xtraEtiketBHPList
                            rptList.ShowPrintMarginsWarning = False
                            rptList.Watermark.Text = sWATERMARK
                            rptList.BindingSource.DataSource = arrDetailEtiket
                            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rptList)
                            printTool.Print()
                        End If

                        If AdaRacikan > 0 Then
                            For i As Integer = 0 To AdaRacikan
                                Dim dsEtiket = oSalesOrderTransaksi.GetStructureHeaderEtiket
                                With dsEtiket
                                    Dim listItem As New List(Of String)

                                    For Each xloop In oSalesOrderTransaksi.GetDataDetail(ds.KDSOTRANSAKSI, i)
                                        If xloop.ISRACIK = True And xloop.GROUPRACIK = i Then
                                            listItem.Add("Racik" & i & " " & xloop.REMARKS)
                                        End If

                                        listItem.Add(xloop.M_ITEM.NMITEM2)

                                    Next

                                    .DESCRIPTION = String.Join(vbCrLf, listItem.ToArray)
                                    .DOKTER = ""
                                    .EXPIREOBAT = ""
                                    .NAMA = ds.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy") & " / " & oRME.GetUmurPasien(ds.DATE, ds.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)
                                    .NORESEP = ""
                                    .NAMAOBAT = ""
                                    .RUANGAN = ""

                                    Dim rpt As New xtraEtiketRacikan
                                    rpt.ShowPrintMarginsWarning = False
                                    rpt.Watermark.Text = sWATERMARK
                                    rpt.BindingSource.DataSource = dsEtiket
                                    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                                    printTool.Print()
                                End With
                            Next
                        End If

                        If RI = 1 Then
                            If MsgBox("Apakah Akan Cetak List Obat?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.Yes Then
                                Dim rpt As New xtraEtiketList
                                rpt.ShowPrintMarginsWarning = False
                                rpt.Watermark.Text = sWATERMARK
                                rpt.BindingSource.DataSource = ds
                                Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                                printTool.Print()
                            End If
                        End If
                    End If

                    Dim dsCekTelaahResep = oSalesOrderTransaksi.GetDataTelaahResep(ds.KDSOTRANSAKSI)
                    If dsCekTelaahResep Is Nothing Then
                        Dim frmTelaahResep As New frmTelaahResep
                        Try
                            frmTelaahResep.LoadMe(FORM_MODE.FORM_MODE_ADD, ds.KDSOTRANSAKSI)
                            frmTelaahResep.ShowDialog(Me)
                        Catch oErr As Exception
                            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                        Finally
                            If Not frmTelaahResep Is Nothing Then frmTelaahResep.Dispose()
                            frmTelaahResep = Nothing
                        End Try
                    Else
                        Dim frmTelaahResep As New frmTelaahResep
                        Try
                            frmTelaahResep.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsCekTelaahResep.KDSOTRANSAKSI)
                            frmTelaahResep.ShowDialog(Me)
                        Catch oErr As Exception
                            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                        Finally
                            If Not frmTelaahResep Is Nothing Then frmTelaahResep.Dispose()
                            frmTelaahResep = Nothing
                        End Try
                    End If
                End If

                If sFind2 <> "" Then
                    Dim oDigitalOrder As New Digital.clsR_Order
                    oDigitalOrder.UpdateStatus(sFind2, "DITERIMA")
                End If
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Sub fn_LoadHistoryPasienCPPT(ByVal sKDCUSTOMER As String, ByVal Category As Integer, ByVal Tanggallahir As String)
        Try
            PdfViewerCPPTRawatJalan.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data CPPT.....")

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\RME\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            'GetDataByRekamMedis = oConnection.dbRME.R_CPPTs.Where(Function(x) x.A_IDENTITASPASIEN_LIST.KDCUSTOMER = snoRm And x.ISDELETE = False).OrderByDescending(Function(x) x.DATE).ToList()

            SQL = "SELECT "
            SQL &= "B.* "
            SQL &= ",PROFESI = ISNULL((SELECT MEMO FROM R_CPPT_M_PROFESI WHERE B.KDPROFESI = KDPROFESI), '') "
            SQL &= "FROM "
            SQL &= "A_IDENTITASPASIEN_LIST A "
            SQL &= "INNER JOIN R_CPPT B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE A.KDCUSTOMER = '" & sKDCUSTOMER & "' "
            SQL &= "AND B.ISDELETE = 0 "
            SQL &= "AND A.CATEGORY = " & Category & " "
            SQL &= "AND ISNULL((SELECT MEMO FROM R_CPPT_M_PROFESI WHERE B.KDPROFESI = KDPROFESI), '') LIKE '%DOKTER%' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "HISTORY_CPPT")

            Dim listCPPT As New List(Of DataAccess.R_CPPT)
            Dim Identitas As Integer = 0

            For iLoop As Integer = 0 To ds.Tables("HISTORY_CPPT").Rows.Count - 1
                Dim dsRekap As New DataAccess.R_CPPT
                With ds.Tables("HISTORY_CPPT")
                    Identitas = .Rows(iLoop)("KDIDENTITAS")
                    dsRekap.DATECREATED = .Rows(iLoop)("DATECREATED")
                    dsRekap.DATEUPDATED = .Rows(iLoop)("DATEUPDATED")
                    dsRekap.DATE = .Rows(iLoop)("DATE")
                    dsRekap.KDIDENTITAS = .Rows(iLoop)("KDIDENTITAS")
                    dsRekap.KDCPPT = .Rows(iLoop)("KDCPPT")
                    dsRekap.KDPROFESI = .Rows(iLoop)("PROFESI")
                    dsRekap.SUBJEKTIF_KELUHANUTAMA = .Rows(iLoop)("SUBJEKTIF_KELUHANUTAMA")
                    dsRekap.SUBJEKTIF_ALERGI_TIDAK = .Rows(iLoop)("SUBJEKTIF_ALERGI_TIDAK")
                    dsRekap.SUBJEKTIF_ALERGI_YA = .Rows(iLoop)("SUBJEKTIF_ALERGI_YA")
                    dsRekap.SUBJEKTIF_ALERGI_YA_TEXT = .Rows(iLoop)("SUBJEKTIF_ALERGI_YA_TEXT")
                    dsRekap.SUBJEKTIF_TEXT = .Rows(iLoop)("SUBJEKTIF_TEXT")
                    dsRekap.OBJEKTIF_KESADARAN = .Rows(iLoop)("OBJEKTIF_KESADARAN")
                    dsRekap.OBJEKTIF_GCS = .Rows(iLoop)("OBJEKTIF_GCS")
                    dsRekap.OBJEKTIF_TAMPAKSAKIT = .Rows(iLoop)("OBJEKTIF_TAMPAKSAKIT")
                    dsRekap.OBJEKTIF_VISUALANALOGSCORE = .Rows(iLoop)("OBJEKTIF_VISUALANALOGSCORE")
                    dsRekap.OBJEKTIF_BERATBADAN = .Rows(iLoop)("OBJEKTIF_BERATBADAN")
                    dsRekap.OBJEKTIF_TINGGIBADAN = .Rows(iLoop)("OBJEKTIF_TINGGIBADAN")
                    dsRekap.OBJEKTIF_SPO2 = .Rows(iLoop)("OBJEKTIF_SPO2")
                    dsRekap.OBJEKTIF_SISTOLE = .Rows(iLoop)("OBJEKTIF_SISTOLE")
                    dsRekap.OBJEKTIF_DIASTOLE = .Rows(iLoop)("OBJEKTIF_DIASTOLE")
                    dsRekap.OBJEKTIF_HR = .Rows(iLoop)("OBJEKTIF_HR")
                    dsRekap.OBJEKTIF_RR = .Rows(iLoop)("OBJEKTIF_RR")
                    dsRekap.OBJEKTIF_SUHU = .Rows(iLoop)("OBJEKTIF_SUHU")
                    dsRekap.OBJEKTIF_PEMERIKSAAN = .Rows(iLoop)("OBJEKTIF_PEMERIKSAAN")
                    dsRekap.OBJEKTIF_ALAMATGAMBARPEMERIKSAAN = .Rows(iLoop)("OBJEKTIF_ALAMATGAMBARPEMERIKSAAN")
                    dsRekap.OBJEKTIF_TEXT = .Rows(iLoop)("OBJEKTIF_TEXT")
                    dsRekap.ASSEMENT_INDIKASI = .Rows(iLoop)("ASSEMENT_INDIKASI")
                    dsRekap.ASSEMENT_TEXT = .Rows(iLoop)("ASSEMENT_TEXT")
                    dsRekap.PLANNING_ISTINDAKLANJUT_PULANG = .Rows(iLoop)("PLANNING_ISTINDAKLANJUT_PULANG")
                    dsRekap.PLANNING_ISTINDAKLANJUT_RAWAT = .Rows(iLoop)("PLANNING_ISTINDAKLANJUT_RAWAT")
                    dsRekap.PLANNING_ISTINDAKLANJUT_KONSUL = .Rows(iLoop)("PLANNING_ISTINDAKLANJUT_KONSUL")
                    dsRekap.PLANNING_ISTINDAKLANJUT_KONSUL_TEXT = .Rows(iLoop)("PLANNING_ISTINDAKLANJUT_KONSUL_TEXT")
                    dsRekap.PLANNING_ISTINDAKLANJUT_RUJUK = .Rows(iLoop)("PLANNING_ISTINDAKLANJUT_RUJUK")
                    dsRekap.PLANNING_ISTINDAKLANJUT_RUJUK_TEXT = .Rows(iLoop)("PLANNING_ISTINDAKLANJUT_RUJUK_TEXT")
                    dsRekap.PLANNING_ALASAN = .Rows(iLoop)("PLANNING_ALASAN")
                    dsRekap.PLANNING_TEXT = .Rows(iLoop)("PLANNING_TEXT")
                    dsRekap.CATATAN = .Rows(iLoop)("CATATAN")
                    dsRekap.KDUSER = .Rows(iLoop)("KDUSER")
                    dsRekap.ISDELETE = .Rows(iLoop)("ISDELETE")
                    dsRekap.DATEDELETE = .Rows(iLoop)("DATEDELETE")
                    dsRekap.USERDELETE = .Rows(iLoop)("USERDELETE")

                    listCPPT.Add(dsRekap)
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            Try

                Dim oConnMySql As New MySql.Data.MySqlClient.MySqlConnection
                Dim oCommMySql As New MySql.Data.MySqlClient.MySqlCommand
                Dim daMySql As MySql.Data.MySqlClient.MySqlDataAdapter
                Dim dsMySql As New DataSet
                Dim MYSQL As String
                dsMySql = New DataSet

                oConnMySql = New MySqlConnection(sMySQL_Url)

                If oConnMySql.State = ConnectionState.Closed Then
                    oConnMySql.Open()
                End If

                MYSQL = "SELECT "
                MYSQL &= "a.KUNJUNGAN "
                MYSQL &= ",a.TANGGAL as tanggal "
                MYSQL &= ",a.SUBYEKTIF "
                MYSQL &= ",a.OBYEKTIF "
                MYSQL &= ",a.ASSESMENT "
                MYSQL &= ",a.PLANNING "
                MYSQL &= ",a.INSTRUKSI "
                MYSQL &= ",d.NAMA "
                MYSQL &= "From medicalrecord.cppt as a "
                MYSQL &= "INNER Join pendaftaran.kunjungan as b "
                MYSQL &= "On a.KUNJUNGAN = b.NOMOR "
                MYSQL &= "INNER Join pendaftaran.pendaftaran as c "
                MYSQL &= "On b.NOPEN = c.NOMOR "
                MYSQL &= "INNER JOIN aplikasi.pengguna as d "
                MYSQL &= "On a.OLEH = d.ID "
                MYSQL &= "WHERE c.NORM = '" & CInt(sKDCUSTOMER) & "' "

                oCommMySql.Connection = oConnMySql
                oCommMySql.CommandText = MYSQL
                oCommMySql.CommandTimeout = 120
                oCommMySql.CommandType = CommandType.Text

                daMySql = New MySql.Data.MySqlClient.MySqlDataAdapter(oCommMySql)
                daMySql.Fill(dsMySql, "medicalrecordcppt")

                If oConnMySql.State = ConnectionState.Open Then
                    oConnMySql.Close()
                End If

                For iLoop As Integer = 0 To dsMySql.Tables("medicalrecordcppt").Rows.Count - 1
                    Dim dsRekap As New DataAccess.R_CPPT
                    With dsMySql.Tables("medicalrecordcppt")
                        dsRekap.DATECREATED = CDate(.Rows(iLoop)("tanggal"))
                        dsRekap.DATEUPDATED = CDate(.Rows(iLoop)("tanggal"))
                        dsRekap.DATE = CDate(.Rows(iLoop)("tanggal"))
                        dsRekap.KDIDENTITAS = Identitas
                        dsRekap.KDCPPT = .Rows(iLoop)("KUNJUNGAN")
                        dsRekap.KDPROFESI = "DOKTER"
                        dsRekap.SUBJEKTIF_KELUHANUTAMA = 0
                        dsRekap.SUBJEKTIF_ALERGI_TIDAK = 0
                        dsRekap.SUBJEKTIF_ALERGI_YA = 0
                        dsRekap.SUBJEKTIF_ALERGI_YA_TEXT = ""
                        dsRekap.SUBJEKTIF_TEXT = CleanHtmlToPlainText(.Rows(iLoop)("SUBYEKTIF"))
                        dsRekap.OBJEKTIF_KESADARAN = 0
                        dsRekap.OBJEKTIF_GCS = 0
                        dsRekap.OBJEKTIF_TAMPAKSAKIT = 0
                        dsRekap.OBJEKTIF_VISUALANALOGSCORE = 0
                        dsRekap.OBJEKTIF_BERATBADAN = 0
                        dsRekap.OBJEKTIF_TINGGIBADAN = 0
                        dsRekap.OBJEKTIF_SPO2 = 0
                        dsRekap.OBJEKTIF_SISTOLE = 0
                        dsRekap.OBJEKTIF_DIASTOLE = 0
                        dsRekap.OBJEKTIF_HR = 0
                        dsRekap.OBJEKTIF_RR = 0
                        dsRekap.OBJEKTIF_SUHU = 0
                        dsRekap.OBJEKTIF_PEMERIKSAAN = 0
                        dsRekap.OBJEKTIF_ALAMATGAMBARPEMERIKSAAN = 0
                        dsRekap.OBJEKTIF_TEXT = CleanHtmlToPlainText(.Rows(iLoop)("OBYEKTIF"))
                        dsRekap.ASSEMENT_INDIKASI = 0
                        dsRekap.ASSEMENT_TEXT = CleanHtmlToPlainText(.Rows(iLoop)("ASSESMENT"))
                        dsRekap.PLANNING_ISTINDAKLANJUT_PULANG = 0
                        dsRekap.PLANNING_ISTINDAKLANJUT_RAWAT = 0
                        dsRekap.PLANNING_ISTINDAKLANJUT_KONSUL = 0
                        dsRekap.PLANNING_ISTINDAKLANJUT_KONSUL_TEXT = 0
                        dsRekap.PLANNING_ISTINDAKLANJUT_RUJUK = 0
                        dsRekap.PLANNING_ISTINDAKLANJUT_RUJUK_TEXT = 0
                        dsRekap.PLANNING_ALASAN = 0
                        dsRekap.PLANNING_TEXT = CleanHtmlToPlainText(.Rows(iLoop)("PLANNING")) & vbCrLf & CleanHtmlToPlainText(.Rows(iLoop)("INSTRUKSI"))
                        dsRekap.CATATAN = 0
                        dsRekap.KDUSER = .Rows(iLoop)("NAMA")
                        dsRekap.ISDELETE = 0
                        dsRekap.DATEDELETE = CDate(.Rows(iLoop)("TANGGAL"))
                        dsRekap.USERDELETE = 0

                        listCPPT.Add(dsRekap)
                    End With
                Next
            Catch oErr As Exception
                SplashScreenManager.CloseForm(False)
                MsgBox("Koneksi CPPT Aplikasi Lama" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try

            sFind1_cppt = String.Empty
            sFind2_cppt = String.Empty
            sFind3_cppt = String.Empty

            If listCPPT.Count > 0 Then
                sFind1_cppt = sKDCUSTOMER
                sFind2_cppt = txtNAMAPASIEN.Text
                sFind3_cppt = Tanggallahir

                Dim FolderSimpan = "C:/SIMRS/CPPT/"

                If Not IO.Directory.Exists(FolderSimpan) Then
                    IO.Directory.CreateDirectory(FolderSimpan)
                Else
                    DeleteDirectory(FolderSimpan)
                    IO.Directory.CreateDirectory(FolderSimpan)
                End If

                Dim AlamatCPPT As String = FolderSimpan & sKDCUSTOMER & Now.ToString("yyyyMMddHHmmss") & ".pdf"

                Dim rpt As New xtraDigital_CPPT_01_QR

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.bindingSource.DataSource = listCPPT.OrderByDescending(Function(x) x.DATE)
                rpt.ExportToPdf(AlamatCPPT)

                If FileIO.FileSystem.FileExists(AlamatCPPT) Then
                    PdfViewerCPPTRawatJalan.LoadDocument(AlamatCPPT)
                End If

            End If

            sFind1_cppt = String.Empty
            sFind2_cppt = String.Empty
            sFind3_cppt = String.Empty

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Load Report CPPT" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub DeleteDirectory(path As String)
        If IO.Directory.Exists(path) Then
            If IO.Directory.Exists(path) Then
                'Delete all files from the Directory
                For Each filepath As String In IO.Directory.GetFiles(path)
                    IO.File.Delete(filepath)
                Next
                'Delete all child Directories
                For Each dir As String In IO.Directory.GetDirectories(path)
                    DeleteDirectory(dir)
                Next
                'Delete a Directory
                IO.Directory.Delete(path)
            End If

        End If
    End Sub
    Private Function CleanHtmlToPlainText(html As String) As String
        Try
            If String.IsNullOrWhiteSpace(html) Then Return String.Empty

            Dim s As String = html

            ' 1) Remove script/style blocks
            s = Regex.Replace(s, "(?is)<(script|style)\b.*?>.*?</\1>", String.Empty)

            ' 2) Replace common block tags with newlines
            s = Regex.Replace(s, "(?i)</?(div|p|h[1-6]|section|article)[^>]*>", vbCrLf)

            ' 3) Replace <br> and <br/> with newline
            s = Regex.Replace(s, "(?i)<br\s*/?>", vbCrLf)

            ' 4) Replace <li> with bullet
            s = Regex.Replace(s, "(?i)<li[^>]*>", vbCrLf & "- ")
            s = Regex.Replace(s, "(?i)</li>", String.Empty)

            ' 5) Remove all remaining tags
            s = Regex.Replace(s, "<[^>]+>", String.Empty)

            ' 6) Decode HTML entities
            s = WebUtility.HtmlDecode(s)

            ' 7) Normalize whitespace: collapse multiple newlines and spaces
            s = Regex.Replace(s, "\r\n[\s\r\n]+", vbCrLf)           ' collapse blank lines
            s = Regex.Replace(s, "[ \t]{2,}", " ")                 ' collapse repeated spaces
            s = Regex.Replace(s, "(?:\r\n){3,}", vbCrLf & vbCrLf)   ' limit successive newlines

            ' Trim
            s = s.Trim()

            Return s
        Catch ex As Exception
            CleanHtmlToPlainText = html
        End Try
    End Function
#End Region
#Region "Grid Method"
    Private Sub OnValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDISCOUNT.EditValueChanged, txtTAX.EditValueChanged, grvDetail.FocusedRowChanged
        If isLoad Then
            Calculate()
        End If
    End Sub
    Private Sub Calculate()
        Dim sSubTotal As Decimal = 0
        Dim sTuslah As Decimal = 0
        For i As Integer = 0 To grvDetail.RowCount - 2
            sSubTotal += CDec(grvDetail.GetRowCellValue(i, colGRANDTOTAL))
            sTuslah += CDec(grvDetail.GetRowCellValue(i, colDISCOUNT))
        Next

        txtTUSLAH.Text = sTuslah
        txtSUBTOTAL.Text = sSubTotal
        txtGRANDTOTAL.Text = CDec(txtSUBTOTAL.Text) - CDec(txtDISCOUNT.Text) + CDec(txtTAX.Text)
    End Sub
    Private Sub grvDetail_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDetail.CellValueChanged
        If e.Column.Name = colKDITEM.Name Then
            Dim oItem As New Reference.clsItem
            Dim oSlaesOrder As New Sales.clsSalesOrderTransaksi

            Try
                If grvDetail.GetFocusedRowCellValue(colKDITEM) IsNot Nothing Then
                    Dim ds = oItem.GetDataDetail_UOM(grvDetail.GetFocusedRowCellValue(colKDITEM))

                    If ds IsNot Nothing Then
                        If grdKDKUNJUNGAN.Text <> "" Then
                            Dim dsSales = oSalesOrderTransaksi.GetDataByKDkunjungan(grdKDKUNJUNGAN.EditValue)
                            If dsSales IsNot Nothing Then
                                If dsSales.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.CATEGORY = 0 Then
                                    Dim dsSalesDetail = oSalesOrderTransaksi.GetDataByGroupTarif(dsSales.KDKUNJUNGAN)
                                    If dsSalesDetail IsNot Nothing Then
                                        Dim dsTes = dsSalesDetail.FirstOrDefault(Function(x) x.KDITEM = grvDetail.GetFocusedRowCellValue(colKDITEM))
                                        If dsTes IsNot Nothing Then
                                            MsgBox("Nama Tarif " & oItem.GetData(grvDetail.GetFocusedRowCellValue(colKDITEM)).NMITEM2 & " yang sama sudah di Input dengan No. Billing : " & dsSales.KDSOTRANSAKSI, MsgBoxStyle.Exclamation, Me.Text)
                                        End If
                                    End If
                                End If
                            End If
                        End If

                        'If sCategoryBilling = 1 Then
                        '    If grdKDKELASRAWAT.Text = "" Then
                        '        grvDetail.SetFocusedRowCellValue(colKDUOM, ds.FirstOrDefault(Function(x) x.RATE = 1).KDUOM)
                        '    Else
                        '        Try
                        '            grvDetail.SetFocusedRowCellValue(colKDUOM, ds.FirstOrDefault(Function(x) x.M_UOM.KDKELASRAWAT = grdKDKELASRAWAT.EditValue).KDUOM)
                        '        Catch ex As Exception
                        '            grvDetail.SetFocusedRowCellValue(colKDUOM, ds.FirstOrDefault(Function(x) x.RATE = 1).KDUOM)
                        '        End Try

                        '    End If

                        'Else
                        '    grvDetail.SetFocusedRowCellValue(colKDUOM, ds.FirstOrDefault(Function(x) x.RATE = 1).KDUOM)
                        'End If

                        Try
                            grvDetail.SetFocusedRowCellValue(colKDUOM, ds.FirstOrDefault(Function(x) x.M_UOM.KDKELASRAWAT = grdKDKELASRAWAT.EditValue).KDUOM)
                        Catch ex As Exception
                            grvDetail.SetFocusedRowCellValue(colKDUOM, ds.FirstOrDefault(Function(x) x.RATE = 1).KDUOM)
                        End Try

                        grvDetail.SetFocusedRowCellValue(colQTY, 1)
                        grvDetail.SetFocusedRowCellValue(colKDSIGNA, oItem.DefaultItem_Signa)
                        grvDetail.SetFocusedRowCellValue(colISRACIK, False)
                        grvDetail.SetFocusedRowCellValue(colDATECREATED, deDATE.DateTime)
                        grvDetail.SetFocusedRowCellValue(colISCETAKETIKET, True)
                        grvDetail.SetFocusedRowCellValue(colKDUSER, sUserID)

                        If sCategoryBilling = 2 Then
                            Dim oDoctor As New Reference.clsDoctor
                            Dim oDepartment As New Reference.clsDepartment
                            Dim dsDoctor = oDoctor.GetDataByStatus("LABORATORIUM")
                            If dsDoctor IsNot Nothing Then
                                grvDetail.SetFocusedRowCellValue(colKDDOCTOR, dsDoctor.KDDOCTOR)
                            Else
                                grvDetail.SetFocusedRowCellValue(colKDDOCTOR, grdKDDOCTOR_H.EditValue)
                            End If

                            Dim dsdepartment = oDepartment.GetDataByName("Laboratorium")
                            If dsdepartment IsNot Nothing Then
                                grvDetail.SetFocusedRowCellValue(colKDDEPARTMENT, dsdepartment.KDDEPARTMENT)
                            Else
                                grvDetail.SetFocusedRowCellValue(colKDDEPARTMENT, grdKDDEPARTMENT_H.EditValue)
                            End If
                        ElseIf sCategoryBilling = 3 Then
                            Dim oDoctor As New Reference.clsDoctor
                            Dim oDepartment As New Reference.clsDepartment
                            Dim dsDoctor = oDoctor.GetDataByStatus("RADIOLOGI")
                            If dsDoctor IsNot Nothing Then
                                grvDetail.SetFocusedRowCellValue(colKDDOCTOR, dsDoctor.KDDOCTOR)
                            Else
                                grvDetail.SetFocusedRowCellValue(colKDDOCTOR, grdKDDOCTOR_H.EditValue)
                            End If

                            Dim dsdepartment = oDepartment.GetDataByName("Radiologi")
                            If dsdepartment IsNot Nothing Then
                                grvDetail.SetFocusedRowCellValue(colKDDEPARTMENT, dsdepartment.KDDEPARTMENT)
                            Else
                                grvDetail.SetFocusedRowCellValue(colKDDEPARTMENT, grdKDDEPARTMENT_H.EditValue)
                            End If
                        ElseIf sCategoryBilling = 4 Then
                            grvDetail.SetFocusedRowCellValue(colKDDEPARTMENT, grdKDDEPARTMENT_H.EditValue)
                            grvDetail.SetFocusedRowCellValue(colKDDOCTOR, grdDOCTOR.EditValue)
                            grvDetail.SetFocusedRowCellValue(colGROUPRACIK, 0)
                        Else
                            If sCategoryBilling = 0 Then
                                grvDetail.SetFocusedRowCellValue(colKDDEPARTMENT, grdKDDEPARTMENT_H.EditValue)
                                grvDetail.SetFocusedRowCellValue(colKDDOCTOR, grdKDDOCTOR_H.EditValue)
                            Else
                                grvDetail.SetFocusedRowCellValue(colKDDEPARTMENT, grdKDDEPARTMENT_H.EditValue)
                                grvDetail.SetFocusedRowCellValue(colKDDOCTOR, "XXX")
                            End If
                        End If
                    Else
                        MsgBox(Statement.ErrorUOM, MsgBoxStyle.Exclamation, Me.Text)

                        grvDetail.CancelUpdateCurrentRow()
                    End If
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf e.Column.Name = colKDUOM.Name Then
            Dim oItem As New Reference.clsItem
            Try
                If grvDetail.GetFocusedRowCellValue(colKDITEM) IsNot Nothing And grvDetail.GetFocusedRowCellValue(colKDUOM) IsNot Nothing Then
                    Dim ds = oItem.GetDataDetail_UOM(grvDetail.GetFocusedRowCellValue(colKDITEM), grvDetail.GetFocusedRowCellValue(colKDUOM))

                    If ds IsNot Nothing Then
                        If sCategoryBilling = 4 Then
                            If sHargaApotik = True Then
                                If chkBPJS.Checked = False Then
                                    grvDetail.SetFocusedRowCellValue(colPRICE, ds.PRICESALESSTANDARD)
                                Else
                                    grvDetail.SetFocusedRowCellValue(colPRICE, ds.PRICESALESTERMIN)
                                End If
                            Else
                                grvDetail.SetFocusedRowCellValue(colPRICE, IIf(ds.M_ITEM.M_ITEM_L1.MEMO.Contains("PROGRAM NOL"), 0, ds.PRICEPURCHASESTANDARD))

                                If CDec(grvDetail.GetFocusedRowCellValue(colPRICE)) > 0 Then
                                    Dim sPriceSetelahPPN = 0 + CDec(grvDetail.GetFocusedRowCellValue(colPRICE))
                                    Dim sPriceTermin = buletin(CDec(sPriceSetelahPPN * (IIf(chkBPJS.Checked = False, (ds.MARGIN / 100), (ds.PRICESALESTERMIN / 100))) + CDec(sPriceSetelahPPN)), 100)

                                    grvDetail.SetFocusedRowCellValue(colPRICE, sPriceTermin)
                                Else
                                    grvDetail.SetFocusedRowCellValue(colPRICE, 0)
                                End If
                            End If
                        Else
                            grvDetail.SetFocusedRowCellValue(colPRICE, ds.PRICESALESSTANDARD)
                        End If
                    Else
                        MsgBox(Statement.ErrorUOM, MsgBoxStyle.Exclamation, Me.Text)

                        Dim sItem = grvDetail.GetFocusedRowCellValue(colKDITEM)
                        grvDetail.CancelUpdateCurrentRow()

                        grvDetail.AddNewRow()
                        grvDetail.SetFocusedRowCellValue(colKDITEM, sItem)
                    End If
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf e.Column.Name = colQTY.Name Or e.Column.Name = colPRICE.Name Then
            Dim sSubTotal As Decimal = CDec(grvDetail.GetFocusedRowCellValue(colQTY)) * CDec(grvDetail.GetFocusedRowCellValue(colPRICE))

            grvDetail.SetFocusedRowCellValue(colSUBTOTAL, sSubTotal)

        ElseIf e.Column.Name = colISRACIK.Name Then
            If sCategoryBilling = 4 Then
                Dim oTuslah As New Reference.clsTuslah

                If CBool(grvDetail.GetFocusedRowCellValue(colISRACIK)) = True Then
                    Dim dsTuslah = oTuslah.GetData("RACIKAN")

                    If dsTuslah IsNot Nothing Then
                        grvDetail.SetFocusedRowCellValue(colDISCOUNT, dsTuslah.HARGA)
                    Else
                        grvDetail.SetFocusedRowCellValue(colDISCOUNT, 0)
                    End If
                Else
                    Dim dsTuslah = oTuslah.GetData("NONRACIKAN")

                    If dsTuslah IsNot Nothing Then
                        grvDetail.SetFocusedRowCellValue(colDISCOUNT, dsTuslah.HARGA)
                    Else
                        grvDetail.SetFocusedRowCellValue(colDISCOUNT, 0)
                    End If
                End If
            Else
                grvDetail.SetFocusedRowCellValue(colDISCOUNT, 0)
            End If
        ElseIf e.Column.Name = colSUBTOTAL.Name Or e.Column.Name = colDISCOUNT.Name Then
            If sCategoryBilling = 4 Then

                Dim sGrandTotal As Decimal = CDec(grvDetail.GetFocusedRowCellValue(colSUBTOTAL)) + CDec(grvDetail.GetFocusedRowCellValue(colDISCOUNT))

                grvDetail.SetFocusedRowCellValue(colGRANDTOTAL, sGrandTotal)
            Else
                Dim sGrandTotal As Decimal = CDec(grvDetail.GetFocusedRowCellValue(colSUBTOTAL)) - CDec(grvDetail.GetFocusedRowCellValue(colDISCOUNT))

                grvDetail.SetFocusedRowCellValue(colGRANDTOTAL, sGrandTotal)
            End If

        End If
    End Sub
    Private Sub UpdateDokterTool_Click(sender As Object, e As EventArgs) Handles UpdateDokterTool.Click
        If sNoId <> String.Empty Then
            Dim dsSO = oSalesOrderTransaksi.GetData(sNoId)

            If dsSO IsNot Nothing Then
                For i As Integer = 0 To grvDetail.RowCount - 2
                    Dim dsDetail = oSalesOrderTransaksi.GetDataDetail(dsSO.KDSOTRANSAKSI, grvDetail.GetRowCellValue(i, colKDITEM), grvDetail.GetRowCellValue(i, colKDUOM), i)

                    If dsDetail IsNot Nothing Then
                        oSalesOrderTransaksi.UpdateDokter(dsSO.KDSOTRANSAKSI, grvDetail.GetRowCellValue(i, colKDITEM), grvDetail.GetRowCellValue(i, colKDUOM), i, grvDetail.GetRowCellValue(i, colKDDOCTOR))
                    End If
                Next

                MsgBox("Selesai Berhasil di Perbaharui", MsgBoxStyle.Information, Me.Text)
            End If

        End If

    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub

        Dim frmPesanDelete As New frmPesanDelete
        frmPesanDelete.ShowDialog(Me)

        If sPesanHapus <> "XXXXXBATALXXXXX" Then
            Dim oDelete As New Setting.clsDelete

            If oDelete.InsertData("UPDATEASESMENAWALMEDIS", sUserID, sFind1 & ", " & sFind2 & " " & sPesanHapus & " " & Now.ToString("dd-MM-yyyy HH:mm:ss"), "BILLING DETIL") = False Then
                MsgBox("Maap Insert Delete Gagal", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            grvDetail.DeleteSelectedRows()

        Else
            Exit Sub
        End If

    End Sub
    Private Function buletin(ByVal Number As Double, Optional ByVal Range As Integer = 10) As Decimal

        buletin = Math.Round(Number / Range, 0) * Range

    End Function
#End Region
#Region "Command Button"
    Private Sub frmSalesOrder_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClose_Click()
            Case Keys.F2
                If btnSaveNew.Enabled = True Then
                    btnSaveNew_Click()
                End If
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
            'Case Keys.F5
            '    If btnDiagnosa.Enabled = True Then
            '        btnDiagnosa_Click()
            '    End If
            Case Keys.F6
                If sCategoryBilling = 4 Then
                    If btnAdjusment.Enabled = True Then
                        btnAdjusment_Click()
                    End If
                End If
            'Case Keys.F7
            '    If btnOrder.Enabled = True Then
            '        btnOrder_Click()
            '    End If
            Case Keys.F9
                If btnFocus.Enabled = True Then
                    btnFocus_Click()
                End If
        End Select
    End Sub
    Private Sub btnOrder_Click() Handles btnOrder.ItemClick
        'Dim oGrouperDataCppt As New Grouper.clsR_Identitas_Grouper_CPPT
        'Dim oDoctor As New Reference.clsDoctor

        'frmBrowseOrderPenunjang.LoadMe(sCategoryBilling)
        'frmBrowseOrderPenunjang.ShowDialog(Me)

        'sKDCPPT = sFind1

        'If sCategoryBilling = 2 Then
        '    If sFind1 <> "" Then
        '        Dim dsCPPT = oGrouperDataCppt.GetData(sFind1)
        '        If dsCPPT IsNot Nothing Then
        '            Dim dsKunjungan = oGrouperDataCppt.GetDataKunjunganFirst(dsCPPT.R_IDENTITAS_GROUPER.norec)
        '            If dsKunjungan IsNot Nothing Then
        '                fn_LoadKDKUNJUNGAN(dsKunjungan.KDKUNJUNGAN, 3)
        '                grdKDKUNJUNGAN.Text = dsKunjungan.KDKUNJUNGAN

        '                fn_LoadDataIdentitas(dsKunjungan.KDKUNJUNGAN)
        '                Dim dsDoctor = oDoctor.GetDataByKodeVclaim(dsCPPT.R_IDENTITAS_GROUPER.dpjpkodevclaim)
        '                If dsDoctor IsNot Nothing Then
        '                    grdDOCTOR.Text = dsDoctor.KDDOCTOR
        '                End If
        '            Else
        '                MsgBox("Maap Registrasi Tidak di Temukan", MsgBoxStyle.Exclamation, Me.Text)
        '            End If

        '            For Each xloop In oGrouperDataCppt.GetDataDetailTindakan(dsCPPT.KDCPPT)
        '                If xloop.M_ITEM.M_ITEM_L3.MEMO = "LABORATORIUM" And xloop.ISBACA = 0 Then
        '                    grvDetail.Focus()
        '                    grvDetail.AddNewRow()
        '                    grvDetail.SetFocusedRowCellValue(colKDITEM, xloop.KDITEM)
        '                    grvDetail.SetFocusedRowCellValue(colQTY, xloop.JUMLAH)
        '                    grvDetail.UpdateCurrentRow()
        '                End If
        '            Next
        '        End If
        '    End If
        'ElseIf sCategoryBilling = 3 Then
        '    If sFind1 <> "" Then
        '        Dim dsCPPT = oGrouperDataCppt.GetData(sFind1)
        '        If dsCPPT IsNot Nothing Then
        '            Dim dsKunjungan = oGrouperDataCppt.GetDataKunjunganFirst(dsCPPT.R_IDENTITAS_GROUPER.norec)
        '            If dsKunjungan IsNot Nothing Then
        '                fn_LoadKDKUNJUNGAN(dsKunjungan.KDKUNJUNGAN, 3)
        '                grdKDKUNJUNGAN.Text = dsKunjungan.KDKUNJUNGAN

        '                fn_LoadDataIdentitas(dsKunjungan.KDKUNJUNGAN)
        '                Dim dsDoctor = oDoctor.GetDataByKodeVclaim(dsCPPT.R_IDENTITAS_GROUPER.dpjpkodevclaim)
        '                If dsDoctor IsNot Nothing Then
        '                    grdDOCTOR.Text = dsDoctor.KDDOCTOR
        '                End If
        '            Else
        '                MsgBox("Maap Registrasi Tidak di Temukan", MsgBoxStyle.Exclamation, Me.Text)
        '            End If

        '            For Each xloop In oGrouperDataCppt.GetDataDetailTindakan(dsCPPT.KDCPPT)
        '                If xloop.M_ITEM.M_ITEM_L3.MEMO = "RADIOLOGI" And xloop.ISBACA = 0 Then
        '                    grvDetail.Focus()
        '                    grvDetail.AddNewRow()
        '                    grvDetail.SetFocusedRowCellValue(colKDITEM, xloop.KDITEM)
        '                    grvDetail.SetFocusedRowCellValue(colQTY, xloop.JUMLAH)
        '                    grvDetail.UpdateCurrentRow()
        '                End If
        '            Next
        '        End If
        '    End If
        'ElseIf sCategoryBilling = 4 Then
        '    Dim oItem As New Reference.clsItem

        '    If sFind1 <> "" Then
        '        Dim dsCPPT = oGrouperDataCppt.GetData(sFind1)
        '        If dsCPPT IsNot Nothing Then
        '            Dim dsKunjungan = oGrouperDataCppt.GetDataKunjunganFirst(dsCPPT.R_IDENTITAS_GROUPER.norec)
        '            If dsKunjungan IsNot Nothing Then
        '                fn_LoadKDKUNJUNGAN(dsKunjungan.KDKUNJUNGAN, 3)
        '                grdKDKUNJUNGAN.Text = dsKunjungan.KDKUNJUNGAN

        '                fn_LoadDataIdentitas(dsKunjungan.KDKUNJUNGAN)
        '                Dim dsDoctor = oDoctor.GetDataByKodeVclaim(dsCPPT.R_IDENTITAS_GROUPER.dpjpkodevclaim)
        '                If dsDoctor IsNot Nothing Then
        '                    grdDOCTOR.Text = dsDoctor.KDDOCTOR
        '                End If
        '            Else
        '                MsgBox("Maap Registrasi Tidak di Temukan", MsgBoxStyle.Exclamation, Me.Text)
        '            End If

        '            For Each xloop In oGrouperDataCppt.GetDataDetailNonRacikan(dsCPPT.KDCPPT)
        '                If xloop.ISBACA = 0 Then
        '                    grvDetail.Focus()
        '                    grvDetail.AddNewRow()
        '                    grvDetail.SetFocusedRowCellValue(colKDITEM, xloop.KDITEM)
        '                    grvDetail.SetFocusedRowCellValue(colQTY, xloop.JUMLAH)
        '                    grvDetail.SetFocusedRowCellValue(colKDSIGNA, xloop.KDSIGNA)
        '                    grvDetail.SetFocusedRowCellValue(colREMARKS, xloop.M_CARAPAKAI.MEMO)
        '                    grvDetail.UpdateCurrentRow()
        '                End If
        '            Next

        '            For Each xloop In oGrouperDataCppt.GetDataDetailRacikan(dsCPPT.KDCPPT)
        '                If xloop.ISBACA = 0 Then
        '                    grvDetail.Focus()
        '                    grvDetail.AddNewRow()
        '                    grvDetail.SetFocusedRowCellValue(colKDITEM, xloop.KDITEM)
        '                    grvDetail.SetFocusedRowCellValue(colQTY, xloop.JUMLAH)
        '                    grvDetail.SetFocusedRowCellValue(colKDSIGNA, oItem.DefaultItem_Signa)
        '                    grvDetail.SetFocusedRowCellValue(colREMARKS, xloop.SIGNA & " " & xloop.PERMINTAAN)
        '                    grvDetail.UpdateCurrentRow()
        '                End If
        '            Next
        '        End If
        '    End If
        'End If
    End Sub
    Private Sub btnFocus_Click() Handles btnFocus.ItemClick
        grdKDITEMSearch.Focus()
        grdKDITEMSearch.ShowPopup()
    End Sub
    Private Sub btnAdjusment_Click() Handles btnAdjusment.ItemClick
        If grdKDKUNJUNGAN.Text <> String.Empty And grdKDWAREHOUSE.Text <> String.Empty Then
            frmAdjustment.LoadMe(FORM_MODE.FORM_MODE_ADD, grdKDKUNJUNGAN.Text, grdKDWAREHOUSE.EditValue)
            frmAdjustment.ShowDialog(Me)
            fn_LoadKDITEM()
        End If
    End Sub
    Private Sub btnDiagnosa_Click() Handles btnDiagnosa.ItemClick
        If grdKDKUNJUNGAN.Text = String.Empty Then
            MsgBox("Kode Kunjungan Kosong", MsgBoxStyle.Exclamation, Me.Text)
        Else
            Dim dsKunjungan = oSalesOrderTransaksi.GetDataKunjunganByKD(grdKDKUNJUNGAN.EditValue)

            If dsKunjungan IsNot Nothing Then
                Dim dsMasterDiagnosa = oSalesOrderTransaksi.GetDataMasterDiagnosaByKD(dsKunjungan.KDPENDAFTARAN)
                If dsMasterDiagnosa IsNot Nothing Then
                    frmDiagnosaMaster.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsMasterDiagnosa.KDPENDAFTARAN, dsMasterDiagnosa.KDDIAGNOSAMASTER)
                    frmDiagnosaMaster.ShowDialog(Me)
                Else
                    frmDiagnosaMaster.LoadMe(FORM_MODE.FORM_MODE_ADD, dsKunjungan.KDPENDAFTARAN)
                    frmDiagnosaMaster.ShowDialog(Me)
                End If

                fn_LoadObat()

            Else
                MsgBox("Pendafatran Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
            End If
        End If
    End Sub
    Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
        If fn_Validate() = False Then Exit Sub
        'If oFormMode = FORM_MODE.FORM_MODE_ADD Then
        '    If chkBPJS.Checked = False Then
        '        If sCategoryBilling = 0 Then
        '            Dim frmPesanKwitansi As New frmPesanKwitansi
        '            frmPesanKwitansi.ShowDialog(Me)
        '        ElseIf sCategoryBilling = 2 Then
        '            Dim frmPesanKwitansi As New frmPesanKwitansi
        '            frmPesanKwitansi.ShowDialog(Me)
        '        ElseIf sCategoryBilling = 3 Then
        '            Dim frmPesanKwitansi As New frmPesanKwitansi
        '            frmPesanKwitansi.ShowDialog(Me)
        '        ElseIf sCategoryBilling = 4 Then
        '            Dim frmPesanKwitansi As New frmPesanKwitansi
        '            frmPesanKwitansi.ShowDialog(Me)
        '        Else
        '            If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        '        End If

        '    Else
        '        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        '    End If
        'End If

        If fn_Save(False) = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
            'sStatusSave = "NEW"
            Me.Close()
        End If
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        'If oFormMode = FORM_MODE.FORM_MODE_ADD Then
        '    If chkBPJS.Checked = False Then
        '        If sCategoryBilling = 0 Then
        '            Dim frmPesanKwitansi As New frmPesanKwitansi
        '            frmPesanKwitansi.ShowDialog(Me)
        '        ElseIf sCategoryBilling = 2 Then
        '            Dim frmPesanKwitansi As New frmPesanKwitansi
        '            frmPesanKwitansi.ShowDialog(Me)
        '        ElseIf sCategoryBilling = 3 Then
        '            Dim frmPesanKwitansi As New frmPesanKwitansi
        '            frmPesanKwitansi.ShowDialog(Me)
        '        ElseIf sCategoryBilling = 4 Then
        '            Dim frmPesanKwitansi As New frmPesanKwitansi
        '            frmPesanKwitansi.ShowDialog(Me)
        '        Else
        '            If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        '        End If

        '    Else
        '        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        '    End If
        'End If

        If fn_Save(True) = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            If sCategoryBilling = 4 Then
                Dim dsKunjungan = oSalesOrderTransaksi.GetDataByKDkunjungan(grdKDKUNJUNGAN.EditValue)
                If dsKunjungan IsNot Nothing Then
                    Dim oSet_Antrian_Simpan As New SettingAntrian.clsSetAntrian
                    Dim dsAntrian = oSet_Antrian_Simpan.GetDataByRMTanggal(dsKunjungan.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.KDCUSTOMER, Now)
                    If dsAntrian IsNot Nothing Then
                        oSet_Antrian_Simpan.UpdateDataIsCheked(dsAntrian.KODEBOOKING, 6, "")
                        'Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                        'Dim oUpdateWaktuAntrian As New SettingAntrian_UI.clsSetAntrian_UI
                        'Dim JsonRequest As String = oUpdateWaktuAntrian.fn_RequestUpdateWaktuAntrean(dsAntrian.KODEBOOKING, 6, uTime)
                        'If JsonRequest <> "" Then
                        '    MsgBox("Update Waktu Ke BPJS" & vbCrLf & oUpdateWaktuAntrian.fn_UpdateWaktuAntrean(JsonRequest, uTime), MsgBoxStyle.Information, Me.Text)
                        'End If
                    End If
                End If
            ElseIf sCategoryBilling = 0 Then
                MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)

                Dim dsKunjungan = oSalesOrderTransaksi.GetDataByKDkunjungan(grdKDKUNJUNGAN.EditValue)
                If dsKunjungan IsNot Nothing Then
                    Dim oSet_Antrian_Simpan As New SettingAntrian.clsSetAntrian
                    Dim dsAntrian = oSet_Antrian_Simpan.GetDataByRMTanggal(dsKunjungan.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.KDCUSTOMER, Now)
                    If dsAntrian IsNot Nothing Then
                        oSet_Antrian_Simpan.UpdateDataIsCheked(dsAntrian.KODEBOOKING, 5, "")
                        'Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                        'Dim oUpdateWaktuAntrian As New SettingAntrian_UI.clsSetAntrian_UI
                        'Dim JsonRequest As String = oUpdateWaktuAntrian.fn_RequestUpdateWaktuAntrean(dsAntrian.KODEBOOKING, 5, uTime)
                        'If JsonRequest <> "" Then
                        '    MsgBox("Update Waktu Ke BPJS" & vbCrLf & oUpdateWaktuAntrian.fn_UpdateWaktuAntrean(JsonRequest, uTime), MsgBoxStyle.Information, Me.Text)
                        'End If
                    End If
                End If
            Else
                MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
            End If

            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadITEM_L2()
        Dim oItem_L2 As New Reference.clsItem_L2
        If sCategoryBilling <> 4 Then
            Try
                grdITEM_L2.Properties.DataSource = oItem_L2.GetData.Where(Function(x) x.ISACTIVE = True And Not x.MEMO.Contains("FARMASI")).ToList()
                grdITEM_L2.Properties.ValueMember = "KDITEM_L2"
                grdITEM_L2.Properties.DisplayMember = "MEMO"

                If sCategoryBilling = 2 Then
                    grdITEM_L2.Text = "TARIF LABORATORIUM"
                ElseIf sCategoryBilling = 3 Then
                    grdITEM_L2.Text = "TARIF RADIOLOGI"
                Else
                    grdITEM_L2.Text = oItem_L2.DefaultItem_L2
                End If

                fn_loadItem()

            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Else
            Try
                grdITEM_L2.Properties.DataSource = oItem_L2.GetData.Where(Function(x) x.ISACTIVE = True And x.MEMO.Contains("FARMASI")).ToList()
                grdITEM_L2.Properties.ValueMember = "KDITEM_L2"
                grdITEM_L2.Properties.DisplayMember = "MEMO"

                grdITEM_L2.Text = oItem_L2.DefaultItem_L2

            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If

    End Sub
    Private Sub fn_LoadKDITEMSearch()
        Try
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
            SQL &= "A.KDITEM "
            SQL &= ",NMITEM2 = (SELECT CASE D.MEMO WHEN 'NON KELAS' THEN A.NMITEM2 ELSE A.NMITEM2 + ' ' + D.MEMO END) "
            SQL &= ",KELOMPOK = B.MEMO "
            SQL &= ",PRICE = C.PRICESALESSTANDARD "
            SQL &= "FROM "
            SQL &= "M_ITEM A "
            SQL &= "INNER JOIN M_ITEM_L2 B "
            SQL &= "ON A.KDITEM_L2 = B.KDITEM_L2 "
            SQL &= "INNER JOIN M_ITEM_UOM C "
            SQL &= "ON A.KDITEM = C.KDITEM "
            SQL &= "INNER JOIN M_UOM D "
            SQL &= "ON C.KDUOM = D.KDUOM "
            SQL &= "WHERE A.ISSTOK = 0 "
            SQL &= "AND A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ITEM_ALL")

            grdKDITEMSearch.Properties.DataSource = ds.Tables("ITEM_ALL")
            grdKDITEMSearch.Properties.ValueMember = "KDITEM"
            grdKDITEMSearch.Properties.DisplayMember = "NMITEM2"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDITEM()
        Try
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

            If chkBHP.Checked = False Then
                SQL = "SELECT "
                SQL &= "A.KDITEM "
                SQL &= ",A.NMITEM2 "
                SQL &= "FROM "
                SQL &= "M_ITEM A "
                SQL &= "WHERE A.ISSTOK = 0 "
                'SQL &= "AND A.ISACTIVE = 1 "
            Else
                SQL = "SELECT "
                SQL &= "A.KDITEM "
                SQL &= ",A.NMITEM2 "
                SQL &= ",STOK = B.AMOUNT "
                SQL &= "FROM "
                SQL &= "M_ITEM A "
                SQL &= "INNER JOIN M_ITEM_WAREHOUSE B "
                SQL &= "ON A.KDITEM = B.KDITEM "
                SQL &= "INNER JOIN M_ITEM_UOM C "
                SQL &= "ON A.KDITEM = C.KDITEM AND B.KDITEM = C.KDITEM "
                SQL &= "WHERE C.RATE = 1 "
                'SQL &= "AND A.ISACTIVE = 1 "
                SQL &= "AND A.ISSTOK = 1 "
                SQL &= "AND B.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
                'SQL &= "AND B.AMOUNT > 0 "
            End If

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ITEM")

            grdKDITEM.DataSource = ds.Tables("ITEM")
            grdKDITEM.ValueMember = "KDITEM"
            grdKDITEM.DisplayMember = "NMITEM2"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadSearchKDITEM()
        Try
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

            If chkBHP.Checked = False Then
                SQL = "SELECT "
                SQL &= "KELOMPOK = (SELECT MEMO FROM M_ITEM_L6 WHERE A.KDITEM_L6 = KDITEM_L6) "
                SQL &= ",A.KDITEM "
                SQL &= ",A.NMITEM2 "
                SQL &= ",STOK = ISNULL((SELECT AMOUNT FROM M_ITEM_WAREHOUSE WHERE A.KDITEM = KDITEM AND KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "'), 0 ) "
                SQL &= ",KDPILIH = CONVERT(BIT, 0) "
                SQL &= "FROM "
                SQL &= "M_ITEM A "
                SQL &= "WHERE A.ISSTOK = 0 "
                SQL &= "AND A.ISACTIVE = 1 "
                SQL &= "AND A.KDITEM_L2 = '" & grdITEM_L2.EditValue & "' "
            Else
                SQL = "SELECT "
                SQL &= "KELOMPOK = (SELECT MEMO FROM M_ITEM_L6 WHERE A.KDITEM_L6 = KDITEM_L6) "
                SQL &= ",A.KDITEM "
                SQL &= ",A.NMITEM2 "
                SQL &= ",KDPILIH = CONVERT(BIT, 0) "
                SQL &= "FROM "
                SQL &= "M_ITEM A "
                SQL &= "WHERE A.ISSTOK = 1 "
                SQL &= "AND A.ISACTIVE = 1 "
            End If

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ITEM")

            grdKDITEMALL.DataSource = ds.Tables("ITEM")

            grvKDITEMALL.UpdateCurrentRow()
            grvKDITEMALL.RefreshRow(grvKDITEMALL.GetFocusedDataSourceRowIndex())

            grvKDITEMALL.OptionsBehavior.Editable = False
            grvKDITEMALL.OptionsBehavior.ReadOnly = True

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grvKDITEMALL.Columns("KELOMPOK").Group()
            grvKDITEMALL.ExpandAllGroups()

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDUOM()
        Dim oUOM As New Reference.clsUOM
        Try
            grdKDUOM.DataSource = oUOM.GetData.Where(Function(x) x.ISACTIVE = True And IIf(sCategoryBilling = 4, x.KDKELASRAWAT = "", x.KDKELASRAWAT <> "")).ToList()
            grdKDUOM.ValueMember = "KDUOM"
            grdKDUOM.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDSIGNA()
        Dim oSigna As New Reference.clsSigna
        Try
            grdKDSIGNA.DataSource = oSigna.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDSIGNA.ValueMember = "KDSIGNA"
            grdKDSIGNA.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDCARAPAKAI()
        Dim oCaraPakai As New Reference.clsCaraPakai
        Try
            grdKDCARAPAKAI.DataSource = oCaraPakai.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDCARAPAKAI.ValueMember = "KDCARAPAKAI"
            grdKDCARAPAKAI.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDKELASRAWAT()
        Dim oKELAS As New Reference.clsKelasRawat
        Try
            grdKDKELASRAWAT.Properties.DataSource = oKELAS.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDKELASRAWAT.Properties.ValueMember = "KDKELASRAWAT"
            grdKDKELASRAWAT.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDWAREHOUSE()
        Dim oWarehouse As New Reference.clsWarehouse

        Try
            Dim dsWarehouse = From x In oWarehouse.GetData
                              Join y In oWarehouse.GetDataWarehouseUser
                              On x.KDWAREHOUSE Equals y.KDWAREHOUSE
                              Where y.KDUSER = sUserID And x.ISACTIVE = True
                              Select x.KDWAREHOUSE, x.NAME_DISPLAY, y.ISDEFAULT

            grdKDWAREHOUSE.Properties.DataSource = dsWarehouse.ToList()
            grdKDWAREHOUSE.Properties.ValueMember = "KDWAREHOUSE"
            grdKDWAREHOUSE.Properties.DisplayMember = "NAME_DISPLAY"

            If sCategoryBilling = 4 Then
                Dim dsFisrt = dsWarehouse.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If dsFisrt IsNot Nothing Then
                    grdKDWAREHOUSE.EditValue = dsFisrt.KDWAREHOUSE
                End If
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDDOCTOR()
        Dim oDoctor As New Reference.clsDoctor
        Try
            Dim ds = oDoctor.GetData.Where(Function(x) x.ISACTIVE = True).ToList()

            grdKDDOCTOR.DataSource = ds
            grdKDDOCTOR.ValueMember = "KDDOCTOR"
            grdKDDOCTOR.DisplayMember = "NAME_DISPLAY"

            grdKDDOCTOR_H.Properties.DataSource = ds
            grdKDDOCTOR_H.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR_H.Properties.DisplayMember = "NAME_DISPLAY"

            grdDOCTOR.Properties.DataSource = ds
            grdDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDDEPARTMENT()
        Dim oDepartment As New Reference.clsDepartment
        Try
            Dim ds = oDepartment.GetData.Where(Function(x) x.ISACTIVE = True).ToList()

            grdKDDEPARTMENT.DataSource = ds
            grdKDDEPARTMENT.ValueMember = "KDDEPARTMENT"
            grdKDDEPARTMENT.DisplayMember = "NAME_DISPLAY"

            grdKDDEPARTMENT_H.Properties.DataSource = ds
            grdKDDEPARTMENT_H.Properties.ValueMember = "KDDEPARTMENT"
            grdKDDEPARTMENT_H.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDUSER()
        Try
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
            SQL &= "A.KDUSER "
            SQL &= "FROM "
            SQL &= "[USER]..SET_USER A "
            SQL &= "WHERE A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "USERUNIT")

            grdKDUSER.DataSource = ds.Tables("USERUNIT")
            grdKDUSER.ValueMember = "KDUSER"
            grdKDUSER.DisplayMember = "KDUSER"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDKUNJUNGAN(ByVal sParameter As String, ByVal sCari As Integer)
        Try
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
            SQL &= "FROM ( "
            SQL &= "SELECT "
            SQL &= "A.KDKUNJUNGAN "
            SQL &= ",KATEGORI = (SELECT CASE D.CATEGORY WHEN 0 THEN 'RJ' ELSE 'RI' END) "
            SQL &= ",D.KDCUSTOMER "
            SQL &= ",JENISDAFTAR = F.MEMO "
            SQL &= ",PASIEN = G.GOL "
            SQL &= ",A.KDPENDAFTARAN "
            SQL &= ",E.NAME_DISPLAY "
            SQL &= ",A.DATE "
            SQL &= ",TUJUAN = B.NAME_DISPLAY  "
            SQL &= ",DPJP = C.NAME_DISPLAY "
            SQL &= ",PULANG = ISNULL((SELECT CONVERT(BIT, 1) FROM T_UPDATE_TANGGAL_PULANG WHERE A.KDPENDAFTARAN = KDPENDAFTARAN AND CARAPULANG <> 5), ISNULL((SELECT CONVERT(BIT, 1) FROM T_UPDATE_TANGGAL_PULANG WHERE D.KDPENDAFTARAN_AWAL = KDPENDAFTARAN AND CARAPULANG <> 5), CONVERT(BIT, 0)))  "
            SQL &= ",RANAP = (SELECT CASE WHEN KDPENDAFTARAN_AWAL <> '' THEN CONVERT(BIT, 1) ELSE CONVERT(BIT, 0) END) "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_KUNJUNGAN A "
            SQL &= "INNER JOIN M_DEPARTMENT B "
            SQL &= "ON A.KDDEPARTMENT = B.KDDEPARTMENT "
            SQL &= "INNER JOIN M_DOCTOR C "
            SQL &= "ON A.KDDOCTOR = C.KDDOCTOR "
            SQL &= "INNER JOIN S_PENDAFTARAN_H D "
            SQL &= "ON A.KDPENDAFTARAN = D.KDPENDAFTARAN "
            SQL &= "INNER JOIN M_CUSTOMER E "
            SQL &= "ON D.KDCUSTOMER = E.KDCUSTOMER "
            SQL &= "INNER JOIN M_DAFTAR_L1 F "
            SQL &= "ON D.KDDAFTAR_L1 = F.KDDAFTAR_L1 "
            SQL &= "INNER JOIN M_KESATUAN G "
            SQL &= "ON A.KDKESATUAN = G.KDKESATUAN "
            If sCari = 0 Then
                SQL &= "WHERE D.KDCUSTOMER LIKE '%" & sParameter & "%' "
            ElseIf sCari = 1 Then
                SQL &= "WHERE E.NAME_DISPLAY LIKE '%" & sParameter & "%' "
            ElseIf sCari = 2 Then
                SQL &= "WHERE A.KDPENDAFTARAN LIKE '%" & sParameter & "%' "
            Else
                SQL &= "WHERE A.KDKUNJUNGAN = '" & sParameter & "' "
            End If

            If sCategoryBilling = 0 Then
                SQL &= "AND D.CATEGORY = 0 "
            ElseIf sCategoryBilling = 1 Then
                SQL &= "AND D.CATEGORY = 1 "
            End If
            SQL &= ") Z "
            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                SQL &= "WHERE Z.PULANG = 0 "
            End If
            SQL &= "ORDER BY Z.DATE DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

            grdKDKUNJUNGAN.Properties.DataSource = ds.Tables("ALL")
            grdKDKUNJUNGAN.Properties.ValueMember = "KDKUNJUNGAN"
            grdKDKUNJUNGAN.Properties.DisplayMember = "KDKUNJUNGAN"

            If sPopUP = True Then
                grdKDKUNJUNGAN.ShowPopup()
            End If

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub txtCARI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCARI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            sPopUP = True
            If txtCARI.Text = String.Empty Then Exit Sub
            fn_LoadKDKUNJUNGAN(IIf(cboCARI.SelectedIndex = 0, txtCARI.Text.ToString.Trim, txtCARI.Text.ToString.Trim), cboCARI.SelectedIndex)
        End If
    End Sub
    Private Sub fn_LoadDataIdentitas(ByVal KDKUNJUNGAN As String)
        Dim oKunjungan As New Admission.clsPendaftaran
        Dim dsKunjungan = oKunjungan.GetDataKunjungan(KDKUNJUNGAN)
        If dsKunjungan IsNot Nothing Then
            If dsKunjungan.S_PENDAFTARAN_H.STATUSDAFTAR <> 0 Then
                MsgBox("Status Pasien Batal", MsgBoxStyle.Exclamation, Me.Text)
                txtNAMAPASIEN.ResetText()
                grdKDDEPARTMENT_H.ResetText()
                grdKDDOCTOR_H.ResetText()
                Exit Sub
            End If
            txtNAMAPASIEN.Text = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY & " / " & dsKunjungan.S_PENDAFTARAN_H.KDCUSTOMER
            txtTANGGALLAHIR.Text = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy")
            grdKDDEPARTMENT_H.Text = dsKunjungan.KDDEPARTMENT
            grdKDDOCTOR_H.Text = dsKunjungan.KDDOCTOR

            If dsKunjungan.S_PENDAFTARAN_H.M_DAFTAR_L1.MEMO = "BPJS" Then
                chkBPJS.Checked = True
            Else
                chkBPJS.Checked = False
            End If

            If dsKunjungan.S_PENDAFTARAN_H.CATEGORY = 1 Then
                If dsKunjungan.S_PENDAFTARAN_H.NAIKRANAP <> "" Then
                    Dim oKelas As New Reference.clsKelasRawat
                    Dim dsKelasByName = oKelas.GetDataByMemo(dsKunjungan.S_PENDAFTARAN_H.NAIKRANAP)
                    If dsKelasByName IsNot Nothing Then
                        grdKDKELASRAWAT.Text = dsKelasByName.KDKELASRAWAT
                    Else
                        If dsKunjungan.M_DEPARTMENT.M_KELASRAWAT.KELOMPOKKELAS = "Non Kelas" Then
                            grdKDKELASRAWAT.Text = dsKunjungan.S_PENDAFTARAN_H.KDKELASRAWAT
                        ElseIf dsKunjungan.M_DEPARTMENT.M_KELASRAWAT.KELOMPOKKELAS = ""
                            grdKDKELASRAWAT.Text = dsKunjungan.S_PENDAFTARAN_H.KDKELASRAWAT
                        Else
                            grdKDKELASRAWAT.Text = dsKunjungan.M_DEPARTMENT.KDKELASRAWAT
                        End If
                    End If
                Else
                    If dsKunjungan.M_DEPARTMENT.M_KELASRAWAT.KELOMPOKKELAS = "Non Kelas" Then
                        grdKDKELASRAWAT.Text = dsKunjungan.S_PENDAFTARAN_H.KDKELASRAWAT
                    ElseIf dsKunjungan.M_DEPARTMENT.M_KELASRAWAT.KELOMPOKKELAS = ""
                        grdKDKELASRAWAT.Text = dsKunjungan.S_PENDAFTARAN_H.KDKELASRAWAT
                    Else
                        grdKDKELASRAWAT.Text = dsKunjungan.M_DEPARTMENT.KDKELASRAWAT
                    End If
                End If
            Else
                If dsKunjungan.M_DEPARTMENT.M_KELASRAWAT.KELOMPOKKELAS = "Non Kelas" Then
                    grdKDKELASRAWAT.Text = dsKunjungan.S_PENDAFTARAN_H.KDKELASRAWAT
                ElseIf dsKunjungan.M_DEPARTMENT.M_KELASRAWAT.KELOMPOKKELAS = ""
                    grdKDKELASRAWAT.Text = dsKunjungan.S_PENDAFTARAN_H.KDKELASRAWAT
                Else
                    grdKDKELASRAWAT.Text = dsKunjungan.M_DEPARTMENT.KDKELASRAWAT
                End If
            End If

            txtJenisPasien.Text = dsKunjungan.M_KESATUAN.GOL
            Dim a, b, c As String
            a = Year(dsKunjungan.DATE)
            b = Year(dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)
            c = a - b

            txtUmur.Text = c & " tahun"
            txtALAMAT.Text = dsKunjungan.ALAMAT
            txtDiganosa.Text = dsKunjungan.S_PENDAFTARAN_H.M_DIAGNOSA.MEMO

            If sCategoryBilling <> 4 Then
                fn_LoadKDITEM()
                fn_LoadKDUOM()
                fn_LoadSearchKDITEM()

            Else
                grdDetail.Focus()

            End If

            Dim dsItem = oSalesOrderTransaksi.GetDataItem(sUserID)

            If dsItem.Count > 0 Then
                grvDetail.OptionsSelection.MultiSelect = True
                grvDetail.SelectAll()
                grvDetail.DeleteSelectedRows()
                grvDetail.OptionsSelection.MultiSelect = False

                For Each xloop In dsItem.OrderBy(Function(x) x.SEQ)
                    grvDetail.Focus()
                    grvDetail.AddNewRow()
                    grvDetail.SetFocusedRowCellValue(colKDITEM, xloop.KDITEM)
                    grvDetail.UpdateCurrentRow()
                Next
            End If

            If sCategoryBilling = 0 Or sCategoryBilling = 1 Then
                grdDOCTOR.Text = dsKunjungan.KDDOCTOR
            Else
                grdDOCTOR.ResetText()
            End If

            fn_LoadObat()

        Else
            txtNAMAPASIEN.ResetText()
            txtTANGGALLAHIR.ResetText()
            grdKDDEPARTMENT_H.ResetText()
            grdKDDOCTOR_H.ResetText()
            grdKDKELASRAWAT.ResetText()
            txtJenisPasien.ResetText()
            txtUmur.ResetText()
            txtALAMAT.ResetText()
            txtDiganosa.ResetText()
        End If
    End Sub
    Private Sub grdKDKUNJUNGAN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdKDKUNJUNGAN.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If grdKDKUNJUNGAN.Text <> "" Then
                fn_LoadDataIdentitas(grdKDKUNJUNGAN.EditValue)
            End If
        End If
    End Sub
    Private Sub chkBHP_CheckedChanged(sender As Object, e As EventArgs) Handles chkBHP.CheckedChanged
        If sCategoryBilling <> 4 Then
            If grdKDWAREHOUSE.Text <> String.Empty Then
                fn_LoadKDUOM()
                fn_LoadKDITEM()
            Else
                chkBHP.Checked = False
            End If

            fn_LoadSearchKDITEM()

        End If
        fn_Hidden()
    End Sub
    Private Sub grdKDWAREHOUSE_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDWAREHOUSE.EditValueChanged
        fn_LoadKDUOM()
        fn_LoadKDITEM()
    End Sub
    Private Sub fn_loadItem()
        fn_LoadKDITEM()
        fn_LoadKDUOM()
        fn_LoadSearchKDITEM()
    End Sub
    Private Sub grdITEM_L2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdITEM_L2.KeyPress
        If Asc(e.KeyChar) = 13 Then
            fn_loadItem()
        End If
    End Sub
    Private Sub grdKDITEMSearch_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdKDITEMSearch.KeyPress
        If Asc(e.KeyChar) = 13 Then
            grvDetail.Focus()
            grvDetail.AddNewRow()
            grvDetail.SetFocusedRowCellValue(colKDITEM, grdKDITEMSearch.EditValue)
            grvDetail.SetFocusedRowCellValue(colKDDOCTOR, grdKDDOCTOR_H.EditValue)
            grvDetail.UpdateCurrentRow()
        End If
    End Sub
    Private Sub grdKDITEMALL_DoubleClick(sender As Object, e As EventArgs) Handles grdKDITEMALL.DoubleClick
        grvDetail.Focus()
        grvDetail.AddNewRow()
        grvDetail.SetFocusedRowCellValue(colKDITEM, grvKDITEMALL.GetFocusedRowCellValue("KDITEM"))
        grvDetail.SetFocusedRowCellValue(colKDDOCTOR, grdKDDOCTOR_H.EditValue)
        grvDetail.UpdateCurrentRow()
        grvKDITEMALL.SetFocusedRowCellValue(colPilih, False)
    End Sub
    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        For iLoop As Integer = 0 To grvKDITEMALL.RowCount - 1
            'If grvKDITEMALL.IsRowSelected(iLoop) = True Then
            '    grvDetail.Focus()
            '    grvDetail.AddNewRow()
            '    grvDetail.SetFocusedRowCellValue(colKDITEM, grvKDITEMALL.GetRowCellValue(iLoop, "KDITEM"))
            '    grvDetail.UpdateCurrentRow()
            'End If
            If CBool(grvKDITEMALL.GetRowCellValue(iLoop, "KDPILIH")) = True Then
                grvDetail.Focus()
                grvDetail.AddNewRow()
                grvDetail.SetFocusedRowCellValue(colKDITEM, grvKDITEMALL.GetRowCellValue(iLoop, "KDITEM"))
                grvDetail.SetFocusedRowCellValue(colKDDOCTOR, grdKDDOCTOR_H.EditValue)
                grvDetail.UpdateCurrentRow()
            End If

        Next

        fn_LoadSearchKDITEM()

    End Sub
    Private Sub grdKDITEMALL_Click(sender As Object, e As EventArgs) Handles grdKDITEMALL.Click
        If CBool(grvKDITEMALL.GetFocusedRowCellValue(colPilih)) = True Then
            grvKDITEMALL.SetFocusedRowCellValue(colPilih, False)
        Else
            grvKDITEMALL.SetFocusedRowCellValue(colPilih, True)
        End If
    End Sub
    Private Sub txtKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtKode.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Dim oItem As New Reference.clsItem
            Dim dsItem = oItem.GetDataNMITEM3(txtKode.Text.ToString)

            If txtKode.Text = "-" Or txtKode.Text = "" Then Exit Sub

            If dsItem IsNot Nothing Then
                grvDetail.Focus()
                grvDetail.AddNewRow()
                grvDetail.SetFocusedRowCellValue(colKDITEM, dsItem.KDITEM)
                grvDetail.SetFocusedRowCellValue(colKDDOCTOR, grdKDDOCTOR_H.EditValue)
                grvDetail.UpdateCurrentRow()

                txtKode.ResetText()
                txtKode.Focus()

            End If
        End If

    End Sub
    Private Sub fn_LoadObat()
        Dim oPendaftaran As New Admission.clsPendaftaran

        Dim dsPendaftaranKunjungan = oPendaftaran.GetDataKunjungan(grdKDKUNJUNGAN.EditValue)
        If dsPendaftaranKunjungan IsNot Nothing Then
            Dim dsPendaftaran = oPendaftaran.GetData(dsPendaftaranKunjungan.KDPENDAFTARAN)
            If dsPendaftaran IsNot Nothing Then
                txtOBAT.Text = oSalesOrderTransaksi.GetTotalObat(dsPendaftaran.KDPENDAFTARAN, dsPendaftaran.KDPENDAFTARAN_AWAL)
            Else
                txtOBAT.Text = CDec(0)
            End If

            fn_LoadInacbg(dsPendaftaranKunjungan.KDPENDAFTARAN, dsPendaftaranKunjungan.S_PENDAFTARAN_H.M_KELASRAWAT.KODE_VCLAIM)

        Else
            txtOBAT.Text = CDec(0)
        End If

    End Sub
    Private Sub fn_LoadInacbg(ByVal KDPENDAFTARAN As String, ByVal kelasrawat As String)
        Dim dsInacbg = oSalesOrderTransaksi.GetDataHargaINACBG(KDPENDAFTARAN)
        If dsInacbg IsNot Nothing Then
            If kelasrawat = 3 Then
                txtINACBG.Text = dsInacbg.KLSI
            ElseIf kelasrawat = 2 Then
                txtINACBG.Text = dsInacbg.KLS2
            ElseIf kelasrawat = 1 Then
                txtINACBG.Text = dsInacbg.KLS3
            Else
                txtINACBG.Text = 0
            End If
        Else
            txtINACBG.Text = CDec(0)
        End If
    End Sub

    Private Sub OnValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs)

    End Sub
    Private Sub fn_LoadDataUpload(ByVal RM As String)
        Try
            grvUpload.Columns.Clear()
            grdUpload.DataSource = Nothing
            grvUpload.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Processing data.....")

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\RME\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "EXEC PENUNJANG_REKAMMEDIS @KDCUSTOMER = '" & RM & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_SO_TRANSAKSI_D_HASIL_LABORATORIUM")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grdUpload.MainView = grvUpload
            grdUpload.DataSource = ds.Tables("S_SO_TRANSAKSI_D_HASIL_LABORATORIUM")
            grdUpload.ForceInitialize()

            SplashScreenManager.CloseForm(False)

            QyeryHasilLaboratorium_LoadFormatData()

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Load Hasil Penunjang" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub QyeryHasilLaboratorium_LoadFormatData()
        For iLoop As Integer = 0 To grvUpload.Columns.Count - 1
            If grvUpload.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvUpload.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvUpload.Columns(iLoop).DisplayFormat.FormatString = "{0:n0}"
                grvUpload.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvUpload.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvUpload.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvUpload.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm}"
            End If
        Next

        grvUpload.Columns("KDSOTRANSAKSI").Visible = False
        grvUpload.Columns("KDSOTRANSAKSI").OptionsColumn.ShowInCustomizationForm = False
        grvUpload.Columns("KDITEM").Visible = False
        grvUpload.Columns("KDITEM").OptionsColumn.ShowInCustomizationForm = False
        grvUpload.BestFitColumns()
    End Sub
    Private Sub grvUpload_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvUpload.FocusedRowChanged
        If grvUpload.GetFocusedRowCellValue("KDSOTRANSAKSI") Is Nothing Then
            PdfViewerHasilPenunjang.CloseDocument()
            Exit Sub
        End If

        If grvUpload.GetFocusedRowCellValue("KATEGORI") = "LABORATORIUM" Then
            xtraReportHasilLaboratorium(grvUpload.GetFocusedRowCellValue("KDSOTRANSAKSI"))
        ElseIf grvUpload.GetFocusedRowCellValue("KATEGORI") = "RADIOLOGI" Then
            xtraReportHasilExpertise(grvUpload.GetFocusedRowCellValue("KDSOTRANSAKSI"), grvUpload.GetFocusedRowCellValue("KDITEM"))
        Else
            xtraReportHasilUpload(grvUpload.GetFocusedRowCellValue("KDITEM"), grvUpload.GetFocusedRowCellValue("KATEGORI"))
        End If
    End Sub
    Private Sub xtraReportHasilLaboratorium(ByVal KDSOTRANSAKSI As String)
        Try
            PdfViewerHasilPenunjang.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Hasil Laboratorium.....")

            Dim oRME As New RME.clsRME
            Dim oSalesOrder As New Sales.clsSalesOrderTransaksi

            Dim ds = oSalesOrder.GetData(KDSOTRANSAKSI)
            If ds IsNot Nothing Then
                Dim FolderSimpan = "C:\PDF\RME\LABORATORIUM"
                If Not Directory.Exists(FolderSimpan) Then
                    Directory.CreateDirectory(FolderSimpan)
                Else
                    Try
                        DeleteDirectory(FolderSimpan)
                        Directory.CreateDirectory(FolderSimpan)
                    Catch ex As Exception

                    End Try
                End If

                If sHargaApotik = False Then
                    Dim rpt As New xtraHasilLabSementara

                    sSIPNIP = ds.S_SO_TRANSAKSI_D_HASIL_LABORATORIUMs.FirstOrDefault.M_DOCTOR.SIP
                    sUSIA = oRME.GetUmurPasien(ds.DATE, ds.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK
                    rpt.bindingSource.DataSource = ds

                    Dim alamatlab As String = FolderSimpan & "ZLAB" & ds.KDSOTRANSAKSI & Now.ToString("yyyyMMddHHmmss") & ".pdf"

                    rpt.ExportToPdf(alamatlab)

                    If FileIO.FileSystem.FileExists(alamatlab) Then
                        PdfViewerHasilPenunjang.LoadDocument(alamatlab)
                    End If
                Else
                    Dim rpt As New xtraHasilLabSementaraVersi2

                    sSIPNIP = ds.S_SO_TRANSAKSI_D_HASIL_LABORATORIUMs.FirstOrDefault.M_DOCTOR.SIP
                    sUSIA = oRME.GetUmurPasien(ds.DATE, ds.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK
                    rpt.bindingSource.DataSource = ds

                    Dim alamatlab As String = FolderSimpan & "ZLAB" & ds.KDSOTRANSAKSI & Now.ToString("yyyyMMddHHmmss") & ".pdf"

                    rpt.ExportToPdf(alamatlab)

                    If FileIO.FileSystem.FileExists(alamatlab) Then
                        PdfViewerHasilPenunjang.LoadDocument(alamatlab)
                    End If
                End If

            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Hasil Laboratorium" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub xtraReportHasilExpertise(ByVal KDSOTRANSAKSI As String, ByVal SEQ_SO As Integer)
        Try
            Dim oExpertise As New Grouper.clsExpertise
            Dim oRME As New RME.clsRME

            PdfViewerHasilPenunjang.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Hasil Expertise.....")

            Dim dataList As New List(Of Byte())
            Dim ds = oExpertise.GetData(KDSOTRANSAKSI, SEQ_SO)

            If ds IsNot Nothing Then
                Dim FolderSimpan = "C:\PDF\RME\RADIOLOGI"
                If Not Directory.Exists(FolderSimpan) Then
                    Directory.CreateDirectory(FolderSimpan)
                Else
                    Try
                        DeleteDirectory(FolderSimpan)
                        Directory.CreateDirectory(FolderSimpan)
                    Catch ex As Exception

                    End Try
                End If

                Dim Alamat As String = FolderSimpan & KDSOTRANSAKSI & SEQ_SO & Now.ToString("yyyyMMddHHmmss") & ".pdf"

                sUSIA = oRME.GetUmurPasien(ds.DATE, ds.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)

                Dim rpt As New xtraExpertise

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.bindingSource.DataSource = ds
                rpt.ExportToPdf(Alamat)

                If FileIO.FileSystem.FileExists(Alamat) Then
                    PdfViewerHasilPenunjang.LoadDocument(Alamat)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Hasil Expertise" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub xtraReportHasilUpload(ByVal alamat As String, ByVal Type As String)
        Try
            PdfViewerHasilPenunjang.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Hasil Expertise.....")

            If Type.Contains(".pdf") Then
                If IO.File.Exists(alamat) Then
                    PdfViewerHasilPenunjang.LoadDocument(alamat)
                End If
            Else
                Dim FolderSimpan = "C:/SIMRS/UPLOAD/"

                Try
                    If Not Directory.Exists(FolderSimpan) Then
                        Directory.CreateDirectory(FolderSimpan)
                    Else
                        DeleteDirectory(FolderSimpan)
                        Directory.CreateDirectory(FolderSimpan)
                    End If
                Catch ex As Exception

                End Try

                Dim Simpan As String = Now.ToString("yyyyMMddHHmmss") & "Upload.pdf"

                ConvertImageToPDF(alamat, FolderSimpan & Simpan)

                If IO.File.Exists(FolderSimpan & Simpan) Then
                    PdfViewerHasilPenunjang.LoadDocument(FolderSimpan & Simpan)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Hasil Upload" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub ConvertImageToPDF(ByVal alamat As String, ByVal outputPDF As String)
        If Not File.Exists(alamat) Then
            MessageBox.Show("File gambar tidak ditemukan: " & alamat)
            Exit Sub
        End If

        ' Buat dokumen PDF baru
        Dim doc As New Document(PageSize.A4)
        PdfWriter.GetInstance(doc, New FileStream(outputPDF, FileMode.Create))

        doc.Open()

        ' Tambahkan gambar dari file
        Dim img As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(alamat)

        ' Atur ukuran agar pas halaman (opsional)
        img.Alignment = Element.ALIGN_CENTER
        img.ScaleToFit(PageSize.A4.Width - 40, PageSize.A4.Height - 40)

        ' Tambahkan gambar ke halaman
        doc.Add(img)

        doc.Close()
        'MessageBox.Show("Berhasil membuat PDF di: " & outputPDF)
    End Sub
    Private Sub tabControl_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles tabControl.SelectedPageChanged
        If isLoad = True Then
            If tabControl.SelectedTabPageIndex = 0 Then

            ElseIf tabControl.SelectedTabPageIndex = 1 Then
                If sKDCPPT <> "" Then
                    Try
                        grvOrder.Columns.Clear()
                        grdOrder.DataSource = Nothing
                        grvOrder.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

                        Dim oConn As New SqlConnection
                        Dim oComm As New SqlCommand
                        Dim da As SqlDataAdapter
                        Dim ds As New DataSet
                        Dim SQL As String

                        Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\RME\", "Database", "").ToString())

                        oConn = New SqlConnection(sConn)
                        If oConn.State = ConnectionState.Closed Then
                            oConn.Open()
                        End If

                        SQL = "EXEC ORDERCEK_ITEM "
                        SQL &= " @CATEGORY = '" & sCategoryBilling & "' "
                        SQL &= ", @KODE = '" & sKDCPPT & "' "

                        oComm.Connection = oConn
                        oComm.CommandText = SQL
                        oComm.CommandTimeout = 120
                        oComm.CommandType = CommandType.Text

                        da = New SqlDataAdapter(oComm)
                        da.Fill(ds, "ORDERCEK")

                        If oConn.State = ConnectionState.Open Then
                            oConn.Close()
                        End If

                        grdOrder.MainView = grvOrder
                        grdOrder.DataSource = ds.Tables("ORDERCEK")
                        grdOrder.ForceInitialize()

                        grvOrder.BestFitColumns()
                    Catch oErr As Exception
                        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                End If
            ElseIf tabControl.SelectedTabPageIndex = 3 Then
                If grdKDKUNJUNGAN.Text <> "" Then
                    Dim dsKunjungan = oSalesOrderTransaksi.GetDataKunjunganByKD(grdKDKUNJUNGAN.EditValue)
                    If dsKunjungan IsNot Nothing Then
                        fn_LoadHistoryPasienCPPT(dsKunjungan.S_PENDAFTARAN_H.KDCUSTOMER, dsKunjungan.S_PENDAFTARAN_H.CATEGORY, dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)
                    End If
                End If
            ElseIf tabControl.SelectedTabPageIndex = 4 Then
                If grdKDKUNJUNGAN.Text <> "" Then
                    Dim dsKunjungan = oSalesOrderTransaksi.GetDataKunjunganByKD(grdKDKUNJUNGAN.EditValue)
                    If dsKunjungan IsNot Nothing Then
                        fn_LoadDataUpload(dsKunjungan.S_PENDAFTARAN_H.KDCUSTOMER)
                    End If
                End If
            End If
        End If
    End Sub
#End Region
End Class