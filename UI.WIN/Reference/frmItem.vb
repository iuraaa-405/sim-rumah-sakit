Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmItem
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oItem As New Reference.clsItem
    Private sKodeItem As String = String.Empty
    Private sNamaItem As String = String.Empty

#End Region
#Region "Function"
    Public Sub fn_LoadItemData(ByVal Kode As String, ByVal Nama As String)
        sKodeItem = Kode
        sNamaItem = Nama
    End Sub
    Public Sub LoadMe(ByVal FormMode As Integer, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = IIf(sStok = False, Item.TITLE_1, Item.TITLE_2)

            colRATE.VisibleIndex = -1

            If SStok = False Then
                grvDetail_UOM.Columns("PRICEPURCHASESTANDARD").VisibleIndex = -1
                grvDetail_UOM.Columns("PRICESALESTERMIN").VisibleIndex = -1
                grvDetail_UOM.Columns("MARGIN").VisibleIndex = -1
                grvDetail_UOM.Columns("PRICESALESSTANDARD").Caption = "Harga"
            Else
                lNMITEM3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                If sHargaApotik = False Then
                    grvDetail_UOM.Columns("PRICESALESSTANDARD").VisibleIndex = -1
                    grvDetail_UOM.Columns("MARGIN").Caption = "Persen (%) Umum"
                    grvDetail_UOM.Columns("PRICESALESTERMIN").Caption = "Persen (%) BPJS"
                Else
                    grvDetail_UOM.Columns("MARGIN").VisibleIndex = -1
                    grvDetail_UOM.Columns("PRICESALESSTANDARD").Caption = "Harga Jual Umum"
                    grvDetail_UOM.Columns("PRICESALESTERMIN").Caption = "Harga Jual BPJS"
                End If
            End If

            lNMITEM1.Text = IIf(sStok = False, Item.NMITEM1_1, Item.NMITEM1_2)
            lNMITEM2.Text = IIf(sStok = False, Item.NMITEM2_1 & " *", Item.NMITEM2_2 & " *")
            lNMITEM3.Text = IIf(sStok = False, Item.NMITEM3_1, Item.NMITEM3_2)

            lKDITEM_L1.Text = Item_L1.TITLE & " *"
            lKDITEM_L2.Text = Item_L2.TITLE & " *"
            lKDITEM_L3.Text = Item_L3.TITLE & " *"
            lKDITEM_L4.Text = Item_L4.TITLE & " *"
            lKDITEM_L5.Text = Item_L5.TITLE & " *"
            lKDITEM_L6.Text = Item_L6.TITLE & " *"

            lKDCOA_INVENTORY.Text = Item.KDCOA_INVENTORY & " *"
            lKDCOA_SALES.Text = Item.KDCOA_SALES & " *"
            lKDCOA_COST.Text = Item.KDCOA_COST & " *"

            grvDetail_UOM.Columns("KDUOM").Caption = Item.DETAILUOM_KDUOM
            grvDetail_UOM.Columns("RATE").Caption = Item.DETAILUOM_RATE
            'grvDetail_UOM.Columns("PRICEPURCHASESTANDARD").Caption = Item.DETAILUOM_PRICEPURCHASESTANDARD
            'grvDetail_UOM.Columns("PRICESALESSTANDARD").Caption = Item.DETAILUOM_PRICESALESSTANDARD
            'grvDetail_UOM.Columns("MARGIN").Caption = Item.DETAILUOM_MARGIN

            tab1.Text = Item.TAB_SATUAN
            tab2.Text = Item.TAB_JASA
            tab3.Text = Item.TAB_SPESIFICATION
            tab4.Text = Item.TAB_ACCOUNT
            tab5.Text = Item.TAB_ATTACHMENT

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtNMITEM1.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadITEM_L1()
        fn_LoadITEM_L2()
        fn_LoadITEM_L3()
        fn_LoadITEM_L4()
        fn_LoadITEM_L5()
        fn_LoadITEM_L6()
        fn_LoadUOM()
        fn_LoadJASA()
        fn_LoadKDCOA_COST()
        fn_LoadKDCOA_INVENTORY()
        fn_LoadKDCOA_SALES()
        fn_LoadKDLOINCDANKFA(sStok)

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

        If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            txtKDITEM.Properties.ReadOnly = False
        Else
            txtKDITEM.Properties.ReadOnly = True
        End If

        txtNMITEM1.Properties.ReadOnly = Status
        txtNMITEM2.Properties.ReadOnly = Status
        chkISACTIVE.Properties.ReadOnly = Status

        grdITEM_L1.Properties.ReadOnly = Status
        grdITEM_L2.Properties.ReadOnly = Status
        grdITEM_L3.Properties.ReadOnly = Status
        grdITEM_L4.Properties.ReadOnly = Status
        grdITEM_L5.Properties.ReadOnly = Status
        grdITEM_L6.Properties.ReadOnly = Status

        grdCOA_COST.Properties.ReadOnly = Status
        grdCOA_INVENTORY.Properties.ReadOnly = Status
        grdCOA_SALES.Properties.ReadOnly = Status

        grvDetail_UOM.OptionsBehavior.ReadOnly = Status
        grvDetail_Jasa.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDITEM.Text = "<--AUTO-->"
        txtNMITEM1.Text = "-"
        txtNMITEM2.ResetText()
        txtNMITEM3.Text = "-"
        chkISACTIVE.Checked = True

        Try
            grdCOA_COST.Text = oItem.AccountCostDefault
        Catch oErr As Exception

        End Try
        Try
            grdCOA_INVENTORY.Text = oItem.AccountInventoryDefault
        Catch oErr As Exception

        End Try
        Try
            grdCOA_SALES.Text = oItem.AccountSalesDefault
        Catch oErr As Exception

        End Try
        Try
            grdITEM_L1.Text = oItem.DefaultItem_L1
        Catch oErr As Exception

        End Try
        Try
            If sStok = False Then
                grdITEM_L2.Text = oItem.DefaultItem_L2
            Else
                grdITEM_L2.Text = oItem.DefaultItem_L2_
            End If

        Catch oErr As Exception

        End Try
        Try
            grdITEM_L3.Text = oItem.DefaultItem_L3
        Catch oErr As Exception

        End Try
        Try
            grdITEM_L4.Text = oItem.DefaultItem_L4
        Catch oErr As Exception

        End Try
        Try
            grdITEM_L5.Text = oItem.DefaultItem_L5
        Catch oErr As Exception

        End Try
        Try
            grdITEM_L6.Text = oItem.DefaultItem_L6
        Catch oErr As Exception

        End Try

        Dim oUOM As New Reference.clsUOM

        If SStok = False Then
            grvDetail_UOM.AddNewRow()
            grvDetail_UOM.SetFocusedRowCellValue(colKDUOM, oUOM.GetData().FirstOrDefault(Function(x) x.KDKELASRAWAT = "KELASRAWAT_0000000001").KDUOM)
            grvDetail_UOM.SetFocusedRowCellValue(colRATE, 1)
            grvDetail_UOM.UpdateCurrentRow()
        Else
            grvDetail_UOM.AddNewRow()
            grvDetail_UOM.SetFocusedRowCellValue(colKDUOM, oUOM.GetData().FirstOrDefault(Function(x) x.ISDEFAULT = True).KDUOM)
            grvDetail_UOM.SetFocusedRowCellValue(colRATE, 1)
            grvDetail_UOM.UpdateCurrentRow()
        End If

        If sKodeItem <> "" Then
            txtKDITEM.Text = sKodeItem
            txtNMITEM2.Text = sNamaItem
        End If
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oItem.GetData(sNoId)

            With ds

                Dim dsSatuSehat = oItem.GetDataSatuSehat(.KDITEM)
                If dsSatuSehat IsNot Nothing Then
                    grdKDLOINDANKFA.Text = dsSatuSehat.IDSATUSEHAT
                End If

                txtKDITEM.Text = .KDITEM
                txtNMITEM1.Text = .NMITEM1
                txtNMITEM2.Text = .NMITEM2
                txtNMITEM3.Text = .NMITEM3
                chkISACTIVE.Checked = .ISACTIVE

                grdITEM_L1.Text = .KDITEM_L1
                grdITEM_L2.Text = .KDITEM_L2
                grdITEM_L3.Text = .KDITEM_L3
                grdITEM_L4.Text = .KDITEM_L4
                grdITEM_L5.Text = .KDITEM_L5
                grdITEM_L6.Text = .KDITEM_L6

                grdCOA_COST.Text = .KDCOA_COST
                grdCOA_INVENTORY.Text = .KDCOA_INVENTORY
                grdCOA_SALES.Text = .KDCOA_SALES

                bindingSource_UOM.DataSource = oItem.GetDataDetail_UOM.Where(Function(x) x.KDITEM = sNoId).OrderBy(Function(x) x.M_UOM.MEMO).ToList()
                grdDetail_UOM.DataSource = bindingSource_UOM

                BindingSource_Jasa.DataSource = oItem.GetDataDetail_JASA.Where(Function(x) x.KDITEM = sNoId).OrderBy(Function(x) x.M_JASA.MEMO).ToList()
                grdDetail_UOM.DataSource = bindingSource_UOM

                BindingSourceTemplatelab.DataSource = oItem.GetDataDetail_TemplateLab(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdTemplate.DataSource = BindingSourceTemplatelab

                'IMAGE
                Try
                    Dim img = (From x In oItem.GetData
                               Where x.KDITEM = sNoId
                               Select x.ATTACHMENT).Single

                    picGAMBAR.Image = ByteArrayToImage(img.ToArray())
                Catch oErr As Exception
                    'MsgBox("Load Image : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try

                tabControl.SelectedTabPage = tab6
                tabControl.SelectedTabPage = tab5
                tabControl.SelectedTabPage = tab4
                tabControl.SelectedTabPage = tab3
                tabControl.SelectedTabPage = tab2
                tabControl.SelectedTabPage = tab1
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function ByteArrayToImage(ByVal byteArrayIn() As Byte) As Image
        Using ms As New System.IO.MemoryStream(byteArrayIn)
            Dim returnImage = Image.FromStream(ms)
            Return returnImage
        End Using
    End Function
    Private Function ImageToByteArray(ByVal ImageIn As System.Drawing.Image) As Byte()

        Using ms As New System.IO.MemoryStream
            ImageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif)
            Return ms.ToArray
        End Using
    End Function
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If txtNMITEM2.Text = String.Empty Then
                txtNMITEM2.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtNMITEM2.ErrorText = Statement.ErrorRequired
                txtNMITEM2.Focus()

                txtNMITEM2.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdITEM_L1.Text = String.Empty Then
                tabControl.SelectedTabPage = tab3

                grdITEM_L1.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdITEM_L1.ErrorText = Statement.ErrorRequired
                grdITEM_L1.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdITEM_L2.Text = String.Empty Then
                tabControl.SelectedTabPage = tab3

                grdITEM_L2.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdITEM_L2.ErrorText = Statement.ErrorRequired
                grdITEM_L2.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdITEM_L3.Text = String.Empty Then
                tabControl.SelectedTabPage = tab3

                grdITEM_L3.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdITEM_L3.ErrorText = Statement.ErrorRequired
                grdITEM_L3.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdITEM_L4.Text = String.Empty Then
                tabControl.SelectedTabPage = tab3

                grdITEM_L4.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdITEM_L4.ErrorText = Statement.ErrorRequired
                grdITEM_L4.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdITEM_L5.Text = String.Empty Then
                tabControl.SelectedTabPage = tab3

                grdITEM_L5.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdITEM_L5.ErrorText = Statement.ErrorRequired
                grdITEM_L5.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdITEM_L6.Text = String.Empty Then
                tabControl.SelectedTabPage = tab3

                grdITEM_L6.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdITEM_L6.ErrorText = Statement.ErrorRequired
                grdITEM_L6.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdCOA_COST.Text = String.Empty Then
                tabControl.SelectedTabPage = tab4

                grdCOA_COST.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdCOA_COST.ErrorText = Statement.ErrorRequired
                grdCOA_COST.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdCOA_INVENTORY.Text = String.Empty Then
                tabControl.SelectedTabPage = tab4

                grdCOA_INVENTORY.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdCOA_INVENTORY.ErrorText = Statement.ErrorRequired
                grdCOA_INVENTORY.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdCOA_SALES.Text = String.Empty Then
                tabControl.SelectedTabPage = tab4

                grdCOA_SALES.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdCOA_SALES.ErrorText = Statement.ErrorRequired
                grdCOA_SALES.Focus()
                fn_Validate = False
                Exit Function
            End If

            'Dim isRate As Boolean = False
            'For iLoop As Integer = 0 To grvDetail_UOM.RowCount - 2
            '    If grvDetail_UOM.GetRowCellValue(iLoop, colRATE) = 1 Then
            '        isRate = True
            '        Exit For
            '    Else
            '        isRate = False
            '    End If
            'Next

            'If isRate = True Then
            '    fn_Validate = True
            'Else
            '    MsgBox(Statement.ErrorRequiredRate, MsgBoxStyle.Exclamation, Me.Text)
            '    fn_Validate = False
            '    Exit Function
            'End If

            'If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            '    If oItem.IsExist(txtNMITEM2.Text.ToUpper.Trim) = True Then
            '        txtNMITEM2.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '        txtNMITEM2.ErrorText = Statement.ErrorRegistered
            '        txtNMITEM2.Focus()
            '        fn_Validate = False
            '        Exit Function
            '    End If
            'Else
            '    If txtNMITEM2.Text.Trim.ToUpper <> oItem.GetData(sNoId).NMITEM2 Then
            '        If oItem.IsExist(txtNMITEM2.Text.ToUpper.Trim) = True Then
            '            txtNMITEM2.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '            txtNMITEM2.ErrorText = Statement.ErrorRegistered
            '            txtNMITEM2.Focus()
            '            fn_Validate = False
            '            Exit Function
            '        End If
            '    End If
            'End If

            grvDetail_UOM.UpdateCurrentRow()
            grvDetail_Jasa.UpdateCurrentRow()

            Dim jasa As Boolean = False
            Dim Jumlahjasa1 As Integer = 0
            Dim Jumlahjasa2 As Integer = 0

            For i As Integer = 0 To grvDetail_Jasa.RowCount - 2
                jasa = True
                Jumlahjasa1 += grvDetail_Jasa.GetRowCellValue(i, colRATE_NONUMUM)
                Jumlahjasa2 += grvDetail_Jasa.GetRowCellValue(i, colRATE_UMUM)
            Next

            If jasa = False Then
                tabControl.SelectedTabPage = tab2

                MsgBox("Silhakan isi jasa", MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End If

            If Jumlahjasa1 <> 100 Then
                tabControl.SelectedTabPage = tab2

                MsgBox("Total Kolom 1 harus 100 %", MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End If
            If Jumlahjasa2 <> 100 Then
                tabControl.SelectedTabPage = tab2

                MsgBox("Total Kolom 2 harus 100 %", MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try

            ' ***** HEADER *****
            Dim ds = oItem.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oItem.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDITEM = txtKDITEM.Text
                .NMITEM1 = txtNMITEM1.Text.Trim.ToUpper
                .NMITEM2 = txtNMITEM2.Text.Trim.ToUpper
                .NMITEM3 = IIf(String.IsNullOrEmpty(txtNMITEM3.Text), String.Empty, txtNMITEM3.Text)
                .ISACTIVE = chkISACTIVE.Checked
                .KDITEM_L1 = grdITEM_L1.EditValue.ToString.Trim.ToUpper
                .KDITEM_L2 = grdITEM_L2.EditValue.ToString.Trim.ToUpper
                If sStok = False Then
                    .KDITEM_L3 = IIf(grdITEM_L3.Text = "-", grdITEM_L2.EditValue.ToString.Trim.ToUpper, grdITEM_L3.EditValue.ToString.Trim.ToUpper)
                Else
                    .KDITEM_L3 = grdITEM_L3.EditValue.ToString.Trim.ToUpper
                End If
                .KDITEM_L4 = grdITEM_L4.EditValue.ToString.Trim.ToUpper
                .KDITEM_L5 = grdITEM_L5.EditValue.ToString.Trim.ToUpper
                .KDITEM_L6 = grdITEM_L6.EditValue.ToString.Trim.ToUpper
                .KDCOA_COST = grdCOA_COST.EditValue.ToString.Trim.ToUpper
                .KDCOA_INVENTORY = grdCOA_INVENTORY.EditValue.ToString.Trim.ToUpper
                .KDCOA_SALES = grdCOA_SALES.EditValue.ToString.Trim.ToUpper
                .QTYMAXIMUM = 0
                .QTYMINIMUM = 0
                .ISTAX = True
                .ISSTOK = sStok

                'IMAGE
                Try
                    Dim data As Byte() = System.IO.File.ReadAllBytes(picGAMBAR.ImageLocation)
                    .ATTACHMENT = data
                Catch oErr As Exception
                    If oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                        Try
                            Dim img = (From x In oItem.GetData
                                       Where x.KDITEM = sNoId
                                       Select x.ATTACHMENT).Single

                            .ATTACHMENT = img
                        Catch ex1 As Exception

                        End Try
                    End If
                End Try
                Try
                    .KDOBATDPHO = oItem.GetData(sNoId).KDOBATDPHO
                Catch oErr As Exception
                    .KDOBATDPHO = ""
                End Try
            End With


            Dim dsItemSatuSehat = oItem.GetStructureHeaderSatuSehat

            If grdKDLOINDANKFA.Text <> "" Then
                With dsItemSatuSehat
                    .IDSATUSEHAT = grdKDLOINDANKFA.EditValue
                    .KDITEM = ds.KDITEM
                    .KDUSER = sUserID
                End With
            Else
                dsItemSatuSehat = Nothing
            End If

            ' ***** Satuan *****
            Dim arrDetail_UOM = oItem.GetStructureDetail_UOMList
            For i As Integer = 0 To grvDetail_UOM.RowCount - 2
                Dim dsDetail_UOM = oItem.GetStructureDetail_UOM
                With dsDetail_UOM
                    Try
                        .DATECREATED = oItem.GetData(sNoId).DATECREATED
                    Catch oErr As Exception
                        .DATECREATED = Now
                    End Try
                    .DATEUPDATED = Now
                    .KDITEM = ds.KDITEM
                    .KDUOM = grvDetail_UOM.GetRowCellValue(i, colKDUOM)
                    .RATE = IIf(CDec(grvDetail_UOM.GetRowCellValue(i, colRATE)) = 0, 1, CDec(grvDetail_UOM.GetRowCellValue(i, colRATE)))
                    .PRICEPURCHASESTANDARD = CDec(grvDetail_UOM.GetRowCellValue(i, colPRICEPURCHASESTANDARD))
                    .PRICESALESSTANDARD = CDec(grvDetail_UOM.GetRowCellValue(i, colPRICESALESSTANDARD))
                    .MARGIN = CDec(grvDetail_UOM.GetRowCellValue(i, colMARGIN))
                    .PRICESALESTERMIN = CDec(grvDetail_UOM.GetRowCellValue(i, colPRICESALESTERMIN))
                End With
                arrDetail_UOM.Add(dsDetail_UOM)
            Next

            ' ***** Jasa *****
            Dim arrDetail_JASA = oItem.GetStructureDetail_JASAList
            For i As Integer = 0 To grvDetail_Jasa.RowCount - 2
                Dim dsDetail_JASA = oItem.GetStructureDetail_JASA
                With dsDetail_JASA
                    Try
                        .DATECREATED = oItem.GetData(sNoId).DATECREATED
                    Catch oErr As Exception
                        .DATECREATED = Now
                    End Try
                    .DATEUPDATED = Now
                    .KDITEM = ds.KDITEM
                    .KDJASA = grvDetail_Jasa.GetRowCellValue(i, colKDJASA)
                    .RATE_NONUMUM = CDec(grvDetail_Jasa.GetRowCellValue(i, colRATE_NONUMUM))
                    .RATE_UMUM = CDec(grvDetail_Jasa.GetRowCellValue(i, colRATE_UMUM))
                End With
                arrDetail_JASA.Add(dsDetail_JASA)
            Next

            ' ***** Template *****
            Dim arrDetail_Template = oItem.GetStructureDetail_TemplatLabList
            For i As Integer = 0 To grvTemplate.RowCount - 2
                Dim dsDetail_Tempalte = oItem.GetStructureDetail_TemplatLab
                With dsDetail_Tempalte
                    Try
                        .DATECREATED = oItem.GetData(sNoId).DATECREATED
                    Catch oErr As Exception
                        .DATECREATED = Now
                    End Try
                    .DATEUPDATED = Now
                    .KDITEM = ds.KDITEM
                    .SEQ = i
                    .PEMERIKSAAN = IIf(String.IsNullOrEmpty(grvTemplate.GetRowCellValue(i, colPEMERIKSAAN)), String.Empty, grvTemplate.GetRowCellValue(i, colPEMERIKSAAN))
                    .HASIL = IIf(String.IsNullOrEmpty(grvTemplate.GetRowCellValue(i, colHASIL)), String.Empty, grvTemplate.GetRowCellValue(i, colHASIL))
                    .NILAIRUJUKAN = IIf(String.IsNullOrEmpty(grvTemplate.GetRowCellValue(i, colNILAIRUJUKAN)), String.Empty, grvTemplate.GetRowCellValue(i, colNILAIRUJUKAN))
                    .NILAIRUJUKAN_1 = IIf(String.IsNullOrEmpty(grvTemplate.GetRowCellValue(i, colNILAIRUJUKAN_1)), 0, grvTemplate.GetRowCellValue(i, colNILAIRUJUKAN_1))
                    .NILAIRUJUKAN_2 = IIf(String.IsNullOrEmpty(grvTemplate.GetRowCellValue(i, colNILAIRUJUKAN_2)), 0, grvTemplate.GetRowCellValue(i, colNILAIRUJUKAN_2))
                    .SATUAN = IIf(String.IsNullOrEmpty(grvTemplate.GetRowCellValue(i, colSATUAN)), String.Empty, grvTemplate.GetRowCellValue(i, colSATUAN))
                    .KETERANGAN = IIf(String.IsNullOrEmpty(grvTemplate.GetRowCellValue(i, colKETERANGAN)), String.Empty, grvTemplate.GetRowCellValue(i, colKETERANGAN))
                    .ISACTIVE = True
                End With
                arrDetail_Template.Add(dsDetail_Tempalte)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oItem.InsertData(ds, arrDetail_UOM, arrDetail_JASA, arrDetail_Template, dsItemSatuSehat)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oItem.UpdateData(ds, arrDetail_UOM, arrDetail_JASA, arrDetail_Template, dsItemSatuSehat)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
#End Region
#Region "Grid Method"
    Private Sub grvDetail_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDetail_UOM.CellValueChanged
        If e.Column.Name = colKDUOM.Name Then
            Try
                If grvDetail_UOM.GetFocusedRowCellValue(colKDUOM) IsNot Nothing Then
                    For i As Integer = 0 To grvDetail_UOM.RowCount - 2
                        If grvDetail_UOM.GetFocusedRowCellValue(colKDUOM) = grvDetail_UOM.GetRowCellValue(i, colKDUOM) Then
                            MsgBox(Statement.ErrorRegistered, MsgBoxStyle.Exclamation, Me.Text)
                            grvDetail_UOM.CancelUpdateCurrentRow()
                            Exit Sub
                        End If
                    Next
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf e.Column.Name = colPRICEPURCHASESTANDARD.Name Or e.Column.Name = colMARGIN.Name
            Try
                If CDec(grvDetail_UOM.GetFocusedRowCellValue(colMARGIN)) > 0 Then

                    Dim sSubTotal As Decimal = CDec(grvDetail_UOM.GetFocusedRowCellValue(colPRICEPURCHASESTANDARD)) * (CDec(grvDetail_UOM.GetFocusedRowCellValue(colMARGIN)) / 100)

                    Dim sPRICESALES = CDec(grvDetail_UOM.GetFocusedRowCellValue(colPRICEPURCHASESTANDARD)) + sSubTotal

                    grvDetail_UOM.SetFocusedRowCellValue(colPRICESALESSTANDARD, sPRICESALES)

                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)

            End Try
        End If
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail_UOM.DeleteSelectedRows()
    End Sub
    Private Sub DeleteToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem1.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvTemplate.DeleteSelectedRows()
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
        End Select
    End Sub
    Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick

        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
            sStatusSave = "NEW"
            Me.Close()
        End If
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadUOM()
        Dim oUOM As New Reference.clsUOM
        Try
            grdUOM.DataSource = oUOM.GetData.Where(Function(x) x.ISACTIVE = True And IIf(SStok = False, x.KDKELASRAWAT <> "", x.KDKELASRAWAT = "")).ToList()
            grdUOM.ValueMember = "KDUOM"
            grdUOM.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadJASA()
        Dim oJASA As New Reference.clsItemJasa
        Try
            grdKDJASA.DataSource = oJASA.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDJASA.ValueMember = "KDJASA"
            grdKDJASA.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadITEM_L1()
        Dim oItem_L1 As New Reference.clsItem_L1
        Try
            grdITEM_L1.Properties.DataSource = oItem_L1.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdITEM_L1.Properties.ValueMember = "KDITEM_L1"
            grdITEM_L1.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdITEM_L1_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdITEM_L1.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdITEM_L1.ResetText()
        End If
    End Sub
    Private Sub fn_LoadITEM_L2()
        Dim oItem_L2 As New Reference.clsItem_L2
        Try
            grdITEM_L2.Properties.DataSource = oItem_L2.GetData.Where(Function(x) x.ISACTIVE = True And IIf(sStok = False, x.MEMO <> "XFARMASI", x.MEMO = "XFARMASI")).ToList()
            grdITEM_L2.Properties.ValueMember = "KDITEM_L2"
            grdITEM_L2.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdITEM_L2_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdITEM_L2.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdITEM_L2.ResetText()
        End If
    End Sub
    Private Sub fn_LoadITEM_L3()
        Dim oItem_L3 As New Reference.clsItem_L3
        Try
            grdITEM_L3.Properties.DataSource = oItem_L3.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdITEM_L3.Properties.ValueMember = "KDITEM_L3"
            grdITEM_L3.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdITEM_L3_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdITEM_L3.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdITEM_L3.ResetText()
        End If
    End Sub
    Private Sub fn_LoadITEM_L4()
        Dim oItem_L4 As New Reference.clsItem_L4
        Try
            grdITEM_L4.Properties.DataSource = oItem_L4.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdITEM_L4.Properties.ValueMember = "KDITEM_L4"
            grdITEM_L4.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdITEM_L4_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdITEM_L4.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdITEM_L4.ResetText()
        End If
    End Sub
    Private Sub fn_LoadITEM_L5()
        Dim oItem_L5 As New Reference.clsItem_L5
        Try
            grdITEM_L5.Properties.DataSource = oItem_L5.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdITEM_L5.Properties.ValueMember = "KDITEM_L5"
            grdITEM_L5.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdITEM_L5_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdITEM_L5.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdITEM_L5.ResetText()
        End If
    End Sub
    Private Sub fn_LoadITEM_L6()
        Dim oItem_L6 As New Reference.clsItem_L6
        Try
            grdITEM_L6.Properties.DataSource = oItem_L6.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdITEM_L6.Properties.ValueMember = "KDITEM_L6"
            grdITEM_L6.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdITEM_L6_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdITEM_L6.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdITEM_L6.ResetText()
        End If
    End Sub
    Private Sub fn_LoadKDCOA_COST()
        Dim oCOA As New Accounting.clsCOA
        Try
            grdCOA_COST.Properties.DataSource = oCOA.GetData.Where(Function(x) x.TYPE = 1).ToList()
            grdCOA_COST.Properties.ValueMember = "KDCOA"
            grdCOA_COST.Properties.DisplayMember = "NMCOA"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdCOA_COST_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdCOA_COST.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdCOA_COST.ResetText()
        End If
    End Sub
    Private Sub fn_LoadKDCOA_INVENTORY()
        Dim oCOA As New Accounting.clsCOA
        Try
            grdCOA_INVENTORY.Properties.DataSource = oCOA.GetData.Where(Function(x) x.TYPE = 1).ToList()
            grdCOA_INVENTORY.Properties.ValueMember = "KDCOA"
            grdCOA_INVENTORY.Properties.DisplayMember = "NMCOA"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDLOINCDANKFA(ByVal isDefault As Boolean)
        If isDefault = False Then
            Dim oLoincdanKFA As New Reference.clsLoincdanKfa
            Try
                grdKDLOINDANKFA.Properties.DataSource = oLoincdanKFA.GetData.Where(Function(x) x.ISACTIVE = True And x.ISDEFAULT = False).ToList()
                grdKDLOINDANKFA.Properties.ValueMember = "KDLOINCDANKFA"
                grdKDLOINDANKFA.Properties.DisplayMember = "MEMO"
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Else
            'Dim oLoincdanKFA As New Reference.clsLoincdanKfa
            'Try
            '    grdKDLOINDANKFA.Properties.DataSource = oLoincdanKFA.GetData.Where(Function(x) x.ISACTIVE = True And x.ISDEFAULT = True).ToList()
            '    grdKDLOINDANKFA.Properties.ValueMember = "KDLOINCDANKFA"
            '    grdKDLOINDANKFA.Properties.DisplayMember = "MEMO"
            'Catch oErr As Exception
            '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            'End Try

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
                SQL &= "KDLOINCDANKFA = A.kodegrouper "
                SQL &= ",MEMO = A.Display_Name "
                SQL &= ",MEMO1 = A.Product_Template_Display_Name "
                SQL &= ",MEMO2 = A.Bahan_Baku_Aktif_Zat_Aktif_Display_Name + ' ' + A.Bahan_Baku_Aktif_Numerator + ' ' + A.Bahan_Baku_Aktif_Satuan_N "
                SQL &= "FROM M_KFA A "

                oComm.Connection = oConn
                oComm.CommandText = SQL
                oComm.CommandTimeout = 120
                oComm.CommandType = CommandType.Text

                da = New SqlDataAdapter(oComm)
                da.Fill(ds, "M_LOINCDANKFA")

                If oConn.State = ConnectionState.Open Then
                    oConn.Close()
                End If

                grdKDLOINDANKFA.Properties.DataSource = ds.Tables("M_LOINCDANKFA")
                grdKDLOINDANKFA.Properties.ValueMember = "KDLOINCDANKFA"
                grdKDLOINDANKFA.Properties.DisplayMember = "MEMO"

            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If

    End Sub
    Private Sub grdCOA_INVENTORY_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdCOA_INVENTORY.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdCOA_INVENTORY.ResetText()
        End If
    End Sub
    Private Sub fn_LoadKDCOA_SALES()
        Dim oCOA As New Accounting.clsCOA
        Try
            grdCOA_SALES.Properties.DataSource = oCOA.GetData.Where(Function(x) x.TYPE = 1).ToList()
            grdCOA_SALES.Properties.ValueMember = "KDCOA"
            grdCOA_SALES.Properties.DisplayMember = "NMCOA"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdCOA_SALES_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdCOA_SALES.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdCOA_SALES.ResetText()
        End If
    End Sub
    Private Sub picGAMBAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picGAMBAR.Click
        If fileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            picGAMBAR.Load(fileDialog.FileName)
            picGAMBAR.Update()
        End If
    End Sub


#End Region
End Class