<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCashIn
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.layoutControl = New DevExpress.XtraLayout.LayoutControl()
        Me.txtKDPENDAFTARAN_AWAL = New DevExpress.XtraEditors.TextEdit()
        Me.barManager = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barTop = New DevExpress.XtraBars.Bar()
        Me.btnSaveNew = New DevExpress.XtraBars.BarButtonItem()
        Me.btnSaveClose = New DevExpress.XtraBars.BarButtonItem()
        Me.btnPasienPulang = New DevExpress.XtraBars.BarButtonItem()
        Me.btnClose = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.progressBarSave = New DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar()
        Me.progressSave = New DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar()
        Me.txtCOSTSHARING = New DevExpress.XtraEditors.TextEdit()
        Me.txtDEPOSIT = New DevExpress.XtraEditors.TextEdit()
        Me.grdKDDOCTOR_H = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView6 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn19 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdKDPENDAFTARAN = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn17 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn18 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtCARI = New DevExpress.XtraEditors.TextEdit()
        Me.cboCARI = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtGRANDTOTAL = New DevExpress.XtraEditors.TextEdit()
        Me.txtROUND = New DevExpress.XtraEditors.TextEdit()
        Me.txtADMIN = New DevExpress.XtraEditors.TextEdit()
        Me.txtSUBTOTAL = New DevExpress.XtraEditors.TextEdit()
        Me.grdKDPAYMENTTYPE = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.grvKDPAYMENTTYPE = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.deDATE = New DevExpress.XtraEditors.DateEdit()
        Me.tabControl = New DevExpress.XtraTab.XtraTabControl()
        Me.tab1 = New DevExpress.XtraTab.XtraTabPage()
        Me.grdDetail = New DevExpress.XtraGrid.GridControl()
        Me.mnuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.bindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.grvDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colNOINVOICE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colAMOUNTORIGINAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colAMOUNTDUE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colAMOUNTPAYMENT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colREMARKS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtREMARKS = New DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit()
        Me.grdNOINVOICE = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.grvNOINVOICE = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.tab7 = New DevExpress.XtraTab.XtraTabPage()
        Me.grdDetail_R = New DevExpress.XtraGrid.GridControl()
        Me.bindingSource_R = New System.Windows.Forms.BindingSource(Me.components)
        Me.grvDetail_R = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colNOINVOICE_R = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colAMOUNTORIGINAL_UB_R = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colAMOUNTDUE_UB_R = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colAMOUNTPAYMENT_R = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colREMARKS_R = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtREMARKS_R = New DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit()
        Me.RepositoryItemGridLookUpEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.GridView10 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn36 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn37 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn39 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdNOINVOICE_R = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.grvNOINVOICE_R = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn30 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn32 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn34 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn35 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.tab2 = New DevExpress.XtraTab.XtraTabPage()
        Me.grdDetailRincian = New DevExpress.XtraGrid.GridControl()
        Me.BindingSourceRincian = New System.Windows.Forms.BindingSource(Me.components)
        Me.grvDeatilRincian = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colKDSOTRANSAKSI = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDATECREATED = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.deDateCretaed = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.colCATEGORYI = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colKDITEM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdKDITEM = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.grvKDITEM = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colSTOK = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colQTY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colKDUOM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdKDUOM = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.grvKDUOM = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn15 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colKDDOCTOR = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdKDDOCTOR = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.grvKDDOCTOR = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn16 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colKDDEPARTMENT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdKDDEPARTMENT = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.grvKDDEPARTMENT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn21 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPRICE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colSUBTOTAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDISCOUNT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colGRANDTOTAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemMemoExEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit()
        Me.grdKDSIGNA = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.grvKDSIGNA = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.MEMO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.chkISRACIK = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.tab4 = New DevExpress.XtraTab.XtraTabPage()
        Me.grdDetailObat = New DevExpress.XtraGrid.GridControl()
        Me.BindingSourceObat = New System.Windows.Forms.BindingSource(Me.components)
        Me.grvDetailObat = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colKDSOTRANSKASI_OBAT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDATECREATED_OBAT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemDateEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.colKDITEM_OBAT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdKDITEM_OBAT = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn25 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn26 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colQTY_OBAT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colKDUOM_OBAT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdKDUOM_OBAT = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.GridView4 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn29 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colKDDOCTOR_OBAT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdKDDOCTOR_OBAT = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.GridView5 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn31 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colKDDEPARTMENT_OBAT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdKDDEPARTMENT_OBAT = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.GridView7 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn33 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPRICE_OBAT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colSUBTOTAL_OBAT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDISCOUNT_OBAT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colGRANDTORAL_OBAT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemMemoExEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit()
        Me.RepositoryItemGridLookUpEdit5 = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.GridView8 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn38 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.tab5 = New DevExpress.XtraTab.XtraTabPage()
        Me.grdDetailItem = New DevExpress.XtraGrid.GridControl()
        Me.BindingSourceItem = New System.Windows.Forms.BindingSource(Me.components)
        Me.grvDetailItem = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colKDITEM_ITEM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdKDITEM_Item = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.RepositoryItemGridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn22 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colQTY_ITEM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.tab6 = New DevExpress.XtraTab.XtraTabPage()
        Me.grdHistoryPasien = New DevExpress.XtraGrid.GridControl()
        Me.mnuStrip_ = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MutasiPasienToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditMutasiPasienToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.grvHistoryPasien = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.RepositoryItemGridLookUpEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.GridView9 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn23 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn24 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn27 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemGridLookUpEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn28 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemMemoExEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit()
        Me.tab3 = New DevExpress.XtraTab.XtraTabPage()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.txtMEMO = New DevExpress.XtraEditors.MemoEdit()
        Me.s = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.txtKDCASHIN = New DevExpress.XtraEditors.TextEdit()
        Me.txtNAMAPASIEN = New DevExpress.XtraEditors.TextEdit()
        Me.grdKDDEPARTMENT_H = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn20 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.lKDCASH = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lDATE = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lSUBTOTAL = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lADMIN = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lROUND = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lGRANDTOTAL = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lCARI = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lKDPENDAFTARAN = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lNAMAPASIEN = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lKDDEPARTMENT = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lKDDOCTOR = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lKDPAYMENTTYPE = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lCOSTSHARING = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lDEPOSIT = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lKDPENDAFTARAN_AWAL = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cboSHIFT = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.layoutControl.SuspendLayout()
        CType(Me.txtKDPENDAFTARAN_AWAL.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCOSTSHARING.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDEPOSIT.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDDOCTOR_H.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDPENDAFTARAN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCARI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboCARI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGRANDTOTAL.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtROUND.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtADMIN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSUBTOTAL.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDPAYMENTTYPE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvKDPAYMENTTYPE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATE.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tabControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabControl.SuspendLayout()
        Me.tab1.SuspendLayout()
        CType(Me.grdDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuStrip.SuspendLayout()
        CType(Me.bindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtREMARKS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdNOINVOICE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvNOINVOICE, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tab7.SuspendLayout()
        CType(Me.grdDetail_R, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.bindingSource_R, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvDetail_R, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtREMARKS_R, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemGridLookUpEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdNOINVOICE_R, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvNOINVOICE_R, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tab2.SuspendLayout()
        CType(Me.grdDetailRincian, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSourceRincian, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvDeatilRincian, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDateCretaed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDateCretaed.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDITEM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvKDITEM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDUOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvKDUOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDDOCTOR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvKDDOCTOR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDDEPARTMENT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvKDDEPARTMENT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemMemoExEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDSIGNA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvKDSIGNA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkISRACIK, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tab4.SuspendLayout()
        CType(Me.grdDetailObat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSourceObat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvDetailObat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDITEM_OBAT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDUOM_OBAT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDDOCTOR_OBAT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDDEPARTMENT_OBAT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemMemoExEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemGridLookUpEdit5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tab5.SuspendLayout()
        CType(Me.grdDetailItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSourceItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvDetailItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDITEM_Item, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tab6.SuspendLayout()
        CType(Me.grdHistoryPasien, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuStrip_.SuspendLayout()
        CType(Me.grvHistoryPasien, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemGridLookUpEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemGridLookUpEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemMemoExEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tab3.SuspendLayout()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.txtMEMO.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.s, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtKDCASHIN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNAMAPASIEN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDDEPARTMENT_H.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKDCASH, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lDATE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lSUBTOTAL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lADMIN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lROUND, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lGRANDTOTAL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lCARI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKDPENDAFTARAN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lNAMAPASIEN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKDDEPARTMENT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKDDOCTOR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKDPAYMENTTYPE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lCOSTSHARING, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lDEPOSIT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKDPENDAFTARAN_AWAL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboSHIFT.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'layoutControl
        '
        Me.layoutControl.Controls.Add(Me.cboSHIFT)
        Me.layoutControl.Controls.Add(Me.txtKDPENDAFTARAN_AWAL)
        Me.layoutControl.Controls.Add(Me.txtCOSTSHARING)
        Me.layoutControl.Controls.Add(Me.txtDEPOSIT)
        Me.layoutControl.Controls.Add(Me.grdKDDOCTOR_H)
        Me.layoutControl.Controls.Add(Me.grdKDPENDAFTARAN)
        Me.layoutControl.Controls.Add(Me.txtCARI)
        Me.layoutControl.Controls.Add(Me.cboCARI)
        Me.layoutControl.Controls.Add(Me.txtGRANDTOTAL)
        Me.layoutControl.Controls.Add(Me.txtROUND)
        Me.layoutControl.Controls.Add(Me.txtADMIN)
        Me.layoutControl.Controls.Add(Me.txtSUBTOTAL)
        Me.layoutControl.Controls.Add(Me.grdKDPAYMENTTYPE)
        Me.layoutControl.Controls.Add(Me.deDATE)
        Me.layoutControl.Controls.Add(Me.tabControl)
        Me.layoutControl.Controls.Add(Me.txtKDCASHIN)
        Me.layoutControl.Controls.Add(Me.txtNAMAPASIEN)
        Me.layoutControl.Controls.Add(Me.grdKDDEPARTMENT_H)
        Me.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutControl.Location = New System.Drawing.Point(0, 0)
        Me.layoutControl.Name = "layoutControl"
        Me.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(652, 156, 250, 350)
        Me.layoutControl.Root = Me.LayoutControlGroup1
        Me.layoutControl.Size = New System.Drawing.Size(790, 549)
        Me.layoutControl.TabIndex = 0
        Me.layoutControl.Text = "LayoutControl1"
        '
        'txtKDPENDAFTARAN_AWAL
        '
        Me.txtKDPENDAFTARAN_AWAL.Location = New System.Drawing.Point(289, 84)
        Me.txtKDPENDAFTARAN_AWAL.MenuManager = Me.barManager
        Me.txtKDPENDAFTARAN_AWAL.Name = "txtKDPENDAFTARAN_AWAL"
        Me.txtKDPENDAFTARAN_AWAL.Properties.ReadOnly = True
        Me.txtKDPENDAFTARAN_AWAL.Size = New System.Drawing.Size(120, 20)
        Me.txtKDPENDAFTARAN_AWAL.StyleController = Me.layoutControl
        Me.txtKDPENDAFTARAN_AWAL.TabIndex = 51
        '
        'barManager
        '
        Me.barManager.AllowQuickCustomization = False
        Me.barManager.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.barTop})
        Me.barManager.DockControls.Add(Me.barDockControlTop)
        Me.barManager.DockControls.Add(Me.barDockControlBottom)
        Me.barManager.DockControls.Add(Me.barDockControlLeft)
        Me.barManager.DockControls.Add(Me.barDockControlRight)
        Me.barManager.Form = Me
        Me.barManager.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.btnSaveNew, Me.btnClose, Me.btnSaveClose, Me.btnPasienPulang, Me.BarButtonItem1, Me.BarButtonItem2})
        Me.barManager.MainMenu = Me.barTop
        Me.barManager.MaxItemId = 11
        Me.barManager.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.progressBarSave, Me.progressSave})
        '
        'barTop
        '
        Me.barTop.BarName = "Main menu"
        Me.barTop.DockCol = 0
        Me.barTop.DockRow = 0
        Me.barTop.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom
        Me.barTop.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.btnSaveNew), New DevExpress.XtraBars.LinkPersistInfo(Me.btnSaveClose), New DevExpress.XtraBars.LinkPersistInfo(Me.btnPasienPulang), New DevExpress.XtraBars.LinkPersistInfo(Me.btnClose)})
        Me.barTop.OptionsBar.DrawDragBorder = False
        Me.barTop.OptionsBar.MultiLine = True
        Me.barTop.OptionsBar.UseWholeRow = True
        Me.barTop.Text = "Main menu"
        '
        'btnSaveNew
        '
        Me.btnSaveNew.Caption = "F2 - Save && New"
        Me.btnSaveNew.Id = 2
        Me.btnSaveNew.Name = "btnSaveNew"
        '
        'btnSaveClose
        '
        Me.btnSaveClose.Caption = "F3 - Save && Close"
        Me.btnSaveClose.Id = 5
        Me.btnSaveClose.Name = "btnSaveClose"
        '
        'btnPasienPulang
        '
        Me.btnPasienPulang.Caption = "F5 - Pasien Pulang"
        Me.btnPasienPulang.Id = 8
        Me.btnPasienPulang.Name = "btnPasienPulang"
        '
        'btnClose
        '
        Me.btnClose.Caption = "F12 - Close"
        Me.btnClose.Id = 3
        Me.btnClose.Name = "btnClose"
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(790, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 549)
        Me.barDockControlBottom.Size = New System.Drawing.Size(790, 22)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 549)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(790, 0)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 549)
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Id = 9
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.Id = 10
        Me.BarButtonItem2.Name = "BarButtonItem2"
        '
        'progressBarSave
        '
        Me.progressBarSave.Name = "progressBarSave"
        Me.progressBarSave.Stopped = True
        '
        'progressSave
        '
        Me.progressSave.Name = "progressSave"
        Me.progressSave.Paused = True
        '
        'txtCOSTSHARING
        '
        Me.txtCOSTSHARING.EnterMoveNextControl = True
        Me.txtCOSTSHARING.Location = New System.Drawing.Point(563, 421)
        Me.txtCOSTSHARING.MenuManager = Me.barManager
        Me.txtCOSTSHARING.Name = "txtCOSTSHARING"
        Me.txtCOSTSHARING.Properties.Appearance.Options.UseTextOptions = True
        Me.txtCOSTSHARING.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtCOSTSHARING.Properties.Mask.EditMask = "n2"
        Me.txtCOSTSHARING.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtCOSTSHARING.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtCOSTSHARING.Properties.NullText = "0.00"
        Me.txtCOSTSHARING.Size = New System.Drawing.Size(215, 20)
        Me.txtCOSTSHARING.StyleController = Me.layoutControl
        Me.txtCOSTSHARING.TabIndex = 35
        '
        'txtDEPOSIT
        '
        Me.txtDEPOSIT.EnterMoveNextControl = True
        Me.txtDEPOSIT.Location = New System.Drawing.Point(563, 445)
        Me.txtDEPOSIT.MenuManager = Me.barManager
        Me.txtDEPOSIT.Name = "txtDEPOSIT"
        Me.txtDEPOSIT.Properties.Appearance.Options.UseTextOptions = True
        Me.txtDEPOSIT.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtDEPOSIT.Properties.Mask.EditMask = "n2"
        Me.txtDEPOSIT.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtDEPOSIT.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtDEPOSIT.Properties.NullText = "0.00"
        Me.txtDEPOSIT.Properties.ReadOnly = True
        Me.txtDEPOSIT.Size = New System.Drawing.Size(215, 20)
        Me.txtDEPOSIT.StyleController = Me.layoutControl
        Me.txtDEPOSIT.TabIndex = 35
        '
        'grdKDDOCTOR_H
        '
        Me.grdKDDOCTOR_H.Location = New System.Drawing.Point(568, 60)
        Me.grdKDDOCTOR_H.MenuManager = Me.barManager
        Me.grdKDDOCTOR_H.Name = "grdKDDOCTOR_H"
        Me.grdKDDOCTOR_H.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDDOCTOR_H.Properties.NullText = ""
        Me.grdKDDOCTOR_H.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdKDDOCTOR_H.Properties.ReadOnly = True
        Me.grdKDDOCTOR_H.Properties.View = Me.GridView6
        Me.grdKDDOCTOR_H.Size = New System.Drawing.Size(210, 20)
        Me.grdKDDOCTOR_H.StyleController = Me.layoutControl
        Me.grdKDDOCTOR_H.TabIndex = 48
        '
        'GridView6
        '
        Me.GridView6.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn19})
        Me.GridView6.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView6.Name = "GridView6"
        Me.GridView6.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView6.OptionsView.ShowAutoFilterRow = True
        Me.GridView6.OptionsView.ShowGroupPanel = False
        '
        'GridColumn19
        '
        Me.GridColumn19.Caption = "Name Display"
        Me.GridColumn19.FieldName = "NAME_DISPLAY"
        Me.GridColumn19.Name = "GridColumn19"
        Me.GridColumn19.Visible = True
        Me.GridColumn19.VisibleIndex = 0
        '
        'grdKDPENDAFTARAN
        '
        Me.grdKDPENDAFTARAN.EditValue = ""
        Me.grdKDPENDAFTARAN.EnterMoveNextControl = True
        Me.grdKDPENDAFTARAN.Location = New System.Drawing.Point(167, 84)
        Me.grdKDPENDAFTARAN.MenuManager = Me.barManager
        Me.grdKDPENDAFTARAN.Name = "grdKDPENDAFTARAN"
        Me.grdKDPENDAFTARAN.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDPENDAFTARAN.Properties.NullText = ""
        Me.grdKDPENDAFTARAN.Properties.PopupFormMinSize = New System.Drawing.Size(810, 300)
        Me.grdKDPENDAFTARAN.Properties.View = Me.GridView1
        Me.grdKDPENDAFTARAN.Size = New System.Drawing.Size(118, 20)
        Me.grdKDPENDAFTARAN.StyleController = Me.layoutControl
        Me.grdKDPENDAFTARAN.TabIndex = 24
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn10, Me.GridColumn11, Me.GridColumn3, Me.GridColumn12, Me.GridColumn13, Me.GridColumn17, Me.GridColumn18, Me.GridColumn7, Me.GridColumn8})
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Jenis Daftar"
        Me.GridColumn10.FieldName = "JENISDAFTAR"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 0
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Pasien"
        Me.GridColumn11.FieldName = "PASIEN"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 1
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Nomor"
        Me.GridColumn3.FieldName = "KDPENDAFTARAN"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "Name Display"
        Me.GridColumn12.FieldName = "NAME_DISPLAY"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 3
        '
        'GridColumn13
        '
        Me.GridColumn13.Caption = "Tanggal"
        Me.GridColumn13.DisplayFormat.FormatString = "dd-MM-yyyy"
        Me.GridColumn13.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn13.FieldName = "DATE"
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.Visible = True
        Me.GridColumn13.VisibleIndex = 4
        '
        'GridColumn17
        '
        Me.GridColumn17.Caption = "Tujuan"
        Me.GridColumn17.FieldName = "TUJUAN"
        Me.GridColumn17.Name = "GridColumn17"
        Me.GridColumn17.Visible = True
        Me.GridColumn17.VisibleIndex = 5
        '
        'GridColumn18
        '
        Me.GridColumn18.Caption = "DPJP"
        Me.GridColumn18.FieldName = "DPJP"
        Me.GridColumn18.Name = "GridColumn18"
        Me.GridColumn18.Visible = True
        Me.GridColumn18.VisibleIndex = 6
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Pulang"
        Me.GridColumn7.FieldName = "PULANG"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 7
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Ranap"
        Me.GridColumn8.FieldName = "RANAP"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 8
        '
        'txtCARI
        '
        Me.txtCARI.Location = New System.Drawing.Point(289, 60)
        Me.txtCARI.MenuManager = Me.barManager
        Me.txtCARI.Name = "txtCARI"
        Me.txtCARI.Size = New System.Drawing.Size(120, 20)
        Me.txtCARI.StyleController = Me.layoutControl
        Me.txtCARI.TabIndex = 48
        '
        'cboCARI
        '
        Me.cboCARI.EditValue = "REKAM MEDIS"
        Me.cboCARI.Location = New System.Drawing.Point(167, 60)
        Me.cboCARI.MenuManager = Me.barManager
        Me.cboCARI.Name = "cboCARI"
        Me.cboCARI.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboCARI.Properties.Items.AddRange(New Object() {"REKAM MEDIS", "NAMA PASIEN", "NOMOR PENDAFTARAN"})
        Me.cboCARI.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cboCARI.Size = New System.Drawing.Size(118, 20)
        Me.cboCARI.StyleController = Me.layoutControl
        Me.cboCARI.TabIndex = 47
        '
        'txtGRANDTOTAL
        '
        Me.txtGRANDTOTAL.EnterMoveNextControl = True
        Me.txtGRANDTOTAL.Location = New System.Drawing.Point(563, 517)
        Me.txtGRANDTOTAL.MenuManager = Me.barManager
        Me.txtGRANDTOTAL.Name = "txtGRANDTOTAL"
        Me.txtGRANDTOTAL.Properties.Appearance.Options.UseTextOptions = True
        Me.txtGRANDTOTAL.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtGRANDTOTAL.Properties.Mask.EditMask = "n2"
        Me.txtGRANDTOTAL.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtGRANDTOTAL.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtGRANDTOTAL.Properties.NullText = "0.00"
        Me.txtGRANDTOTAL.Properties.ReadOnly = True
        Me.txtGRANDTOTAL.Size = New System.Drawing.Size(215, 20)
        Me.txtGRANDTOTAL.StyleController = Me.layoutControl
        Me.txtGRANDTOTAL.TabIndex = 35
        Me.txtGRANDTOTAL.TabStop = False
        '
        'txtROUND
        '
        Me.txtROUND.EnterMoveNextControl = True
        Me.txtROUND.Location = New System.Drawing.Point(563, 493)
        Me.txtROUND.MenuManager = Me.barManager
        Me.txtROUND.Name = "txtROUND"
        Me.txtROUND.Properties.Appearance.Options.UseTextOptions = True
        Me.txtROUND.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtROUND.Properties.Mask.EditMask = "n2"
        Me.txtROUND.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtROUND.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtROUND.Properties.NullText = "0.00"
        Me.txtROUND.Size = New System.Drawing.Size(215, 20)
        Me.txtROUND.StyleController = Me.layoutControl
        Me.txtROUND.TabIndex = 34
        '
        'txtADMIN
        '
        Me.txtADMIN.EnterMoveNextControl = True
        Me.txtADMIN.Location = New System.Drawing.Point(563, 469)
        Me.txtADMIN.MenuManager = Me.barManager
        Me.txtADMIN.Name = "txtADMIN"
        Me.txtADMIN.Properties.Appearance.Options.UseTextOptions = True
        Me.txtADMIN.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtADMIN.Properties.Mask.EditMask = "n2"
        Me.txtADMIN.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtADMIN.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtADMIN.Properties.NullText = "0.00"
        Me.txtADMIN.Size = New System.Drawing.Size(215, 20)
        Me.txtADMIN.StyleController = Me.layoutControl
        Me.txtADMIN.TabIndex = 33
        '
        'txtSUBTOTAL
        '
        Me.txtSUBTOTAL.EnterMoveNextControl = True
        Me.txtSUBTOTAL.Location = New System.Drawing.Point(563, 397)
        Me.txtSUBTOTAL.MenuManager = Me.barManager
        Me.txtSUBTOTAL.Name = "txtSUBTOTAL"
        Me.txtSUBTOTAL.Properties.Appearance.Options.UseTextOptions = True
        Me.txtSUBTOTAL.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtSUBTOTAL.Properties.Mask.EditMask = "n2"
        Me.txtSUBTOTAL.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtSUBTOTAL.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtSUBTOTAL.Properties.NullText = "0.00"
        Me.txtSUBTOTAL.Properties.ReadOnly = True
        Me.txtSUBTOTAL.Size = New System.Drawing.Size(215, 20)
        Me.txtSUBTOTAL.StyleController = Me.layoutControl
        Me.txtSUBTOTAL.TabIndex = 32
        Me.txtSUBTOTAL.TabStop = False
        '
        'grdKDPAYMENTTYPE
        '
        Me.grdKDPAYMENTTYPE.EnterMoveNextControl = True
        Me.grdKDPAYMENTTYPE.Location = New System.Drawing.Point(568, 84)
        Me.grdKDPAYMENTTYPE.MenuManager = Me.barManager
        Me.grdKDPAYMENTTYPE.Name = "grdKDPAYMENTTYPE"
        Me.grdKDPAYMENTTYPE.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDPAYMENTTYPE.Properties.NullText = ""
        Me.grdKDPAYMENTTYPE.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdKDPAYMENTTYPE.Properties.View = Me.grvKDPAYMENTTYPE
        Me.grdKDPAYMENTTYPE.Size = New System.Drawing.Size(210, 20)
        Me.grdKDPAYMENTTYPE.StyleController = Me.layoutControl
        Me.grdKDPAYMENTTYPE.TabIndex = 31
        '
        'grvKDPAYMENTTYPE
        '
        Me.grvKDPAYMENTTYPE.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn9})
        Me.grvKDPAYMENTTYPE.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvKDPAYMENTTYPE.Name = "grvKDPAYMENTTYPE"
        Me.grvKDPAYMENTTYPE.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvKDPAYMENTTYPE.OptionsView.ShowAutoFilterRow = True
        Me.grvKDPAYMENTTYPE.OptionsView.ShowGroupPanel = False
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Display Name"
        Me.GridColumn9.FieldName = "MEMO"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 0
        '
        'deDATE
        '
        Me.deDATE.EditValue = Nothing
        Me.deDATE.EnterMoveNextControl = True
        Me.deDATE.Location = New System.Drawing.Point(167, 36)
        Me.deDATE.MenuManager = Me.barManager
        Me.deDATE.Name = "deDATE"
        Me.deDATE.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deDATE.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.deDATE.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.deDATE.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.deDATE.Size = New System.Drawing.Size(242, 20)
        Me.deDATE.StyleController = Me.layoutControl
        Me.deDATE.TabIndex = 20
        '
        'tabControl
        '
        Me.tabControl.Location = New System.Drawing.Point(12, 108)
        Me.tabControl.Name = "tabControl"
        Me.tabControl.SelectedTabPage = Me.tab1
        Me.tabControl.Size = New System.Drawing.Size(766, 285)
        Me.tabControl.TabIndex = 18
        Me.tabControl.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tab1, Me.tab7, Me.tab2, Me.tab4, Me.tab5, Me.tab6, Me.tab3})
        '
        'tab1
        '
        Me.tab1.Controls.Add(Me.grdDetail)
        Me.tab1.Name = "tab1"
        Me.tab1.Size = New System.Drawing.Size(760, 257)
        Me.tab1.Text = "Detail Information"
        '
        'grdDetail
        '
        Me.grdDetail.ContextMenuStrip = Me.mnuStrip
        Me.grdDetail.DataSource = Me.bindingSource
        Me.grdDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdDetail.Location = New System.Drawing.Point(0, 0)
        Me.grdDetail.MainView = Me.grvDetail
        Me.grdDetail.MenuManager = Me.barManager
        Me.grdDetail.Name = "grdDetail"
        Me.grdDetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.grdNOINVOICE, Me.txtREMARKS})
        Me.grdDetail.Size = New System.Drawing.Size(760, 257)
        Me.grdDetail.TabIndex = 18
        Me.grdDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvDetail})
        '
        'mnuStrip
        '
        Me.mnuStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.mnuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DeleteToolStripMenuItem})
        Me.mnuStrip.Name = "mnuStrip"
        Me.mnuStrip.Size = New System.Drawing.Size(108, 26)
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(107, 22)
        Me.DeleteToolStripMenuItem.Text = "Delete"
        '
        'bindingSource
        '
        Me.bindingSource.DataSource = GetType(DataAccess.F_CASHIN_D)
        '
        'grvDetail
        '
        Me.grvDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colNOINVOICE, Me.colAMOUNTORIGINAL, Me.colAMOUNTDUE, Me.colAMOUNTPAYMENT, Me.colREMARKS})
        Me.grvDetail.GridControl = Me.grdDetail
        Me.grvDetail.Name = "grvDetail"
        Me.grvDetail.OptionsCustomization.AllowColumnMoving = False
        Me.grvDetail.OptionsCustomization.AllowFilter = False
        Me.grvDetail.OptionsCustomization.AllowGroup = False
        Me.grvDetail.OptionsCustomization.AllowQuickHideColumns = False
        Me.grvDetail.OptionsCustomization.AllowSort = False
        Me.grvDetail.OptionsDetail.EnableMasterViewMode = False
        Me.grvDetail.OptionsFind.AllowFindPanel = False
        Me.grvDetail.OptionsLayout.StoreAllOptions = True
        Me.grvDetail.OptionsLayout.StoreAppearance = True
        Me.grvDetail.OptionsMenu.EnableColumnMenu = False
        Me.grvDetail.OptionsNavigation.AutoFocusNewRow = True
        Me.grvDetail.OptionsNavigation.EnterMoveNextColumn = True
        Me.grvDetail.OptionsView.EnableAppearanceEvenRow = True
        Me.grvDetail.OptionsView.EnableAppearanceOddRow = True
        Me.grvDetail.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.grvDetail.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.grvDetail.OptionsView.ShowFooter = True
        Me.grvDetail.OptionsView.ShowGroupPanel = False
        '
        'colNOINVOICE
        '
        Me.colNOINVOICE.Caption = "Invoice No."
        Me.colNOINVOICE.FieldName = "NOINVOICE"
        Me.colNOINVOICE.Name = "colNOINVOICE"
        Me.colNOINVOICE.OptionsColumn.AllowEdit = False
        Me.colNOINVOICE.OptionsColumn.AllowFocus = False
        Me.colNOINVOICE.OptionsColumn.ReadOnly = True
        Me.colNOINVOICE.OptionsColumn.TabStop = False
        Me.colNOINVOICE.Visible = True
        Me.colNOINVOICE.VisibleIndex = 0
        '
        'colAMOUNTORIGINAL
        '
        Me.colAMOUNTORIGINAL.Caption = "Amount Original"
        Me.colAMOUNTORIGINAL.DisplayFormat.FormatString = "{0:n2}"
        Me.colAMOUNTORIGINAL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colAMOUNTORIGINAL.FieldName = "AMOUNTORIGINAL"
        Me.colAMOUNTORIGINAL.Name = "colAMOUNTORIGINAL"
        Me.colAMOUNTORIGINAL.OptionsColumn.AllowEdit = False
        Me.colAMOUNTORIGINAL.OptionsColumn.AllowFocus = False
        Me.colAMOUNTORIGINAL.OptionsColumn.ReadOnly = True
        Me.colAMOUNTORIGINAL.OptionsColumn.TabStop = False
        Me.colAMOUNTORIGINAL.UnboundType = DevExpress.Data.UnboundColumnType.[Decimal]
        Me.colAMOUNTORIGINAL.Visible = True
        Me.colAMOUNTORIGINAL.VisibleIndex = 1
        '
        'colAMOUNTDUE
        '
        Me.colAMOUNTDUE.Caption = "Amount Due"
        Me.colAMOUNTDUE.DisplayFormat.FormatString = "{0:n2}"
        Me.colAMOUNTDUE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colAMOUNTDUE.FieldName = "AMOUNTDUE"
        Me.colAMOUNTDUE.Name = "colAMOUNTDUE"
        Me.colAMOUNTDUE.OptionsColumn.AllowEdit = False
        Me.colAMOUNTDUE.OptionsColumn.AllowFocus = False
        Me.colAMOUNTDUE.OptionsColumn.ReadOnly = True
        Me.colAMOUNTDUE.OptionsColumn.TabStop = False
        Me.colAMOUNTDUE.UnboundType = DevExpress.Data.UnboundColumnType.[Decimal]
        Me.colAMOUNTDUE.Visible = True
        Me.colAMOUNTDUE.VisibleIndex = 2
        '
        'colAMOUNTPAYMENT
        '
        Me.colAMOUNTPAYMENT.AppearanceCell.Options.UseTextOptions = True
        Me.colAMOUNTPAYMENT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.colAMOUNTPAYMENT.Caption = "Amount Payment"
        Me.colAMOUNTPAYMENT.DisplayFormat.FormatString = "{0:n2}"
        Me.colAMOUNTPAYMENT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colAMOUNTPAYMENT.FieldName = "AMOUNTPAYMENT"
        Me.colAMOUNTPAYMENT.Name = "colAMOUNTPAYMENT"
        Me.colAMOUNTPAYMENT.Visible = True
        Me.colAMOUNTPAYMENT.VisibleIndex = 3
        '
        'colREMARKS
        '
        Me.colREMARKS.AppearanceCell.Options.UseTextOptions = True
        Me.colREMARKS.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.colREMARKS.Caption = "Remarks"
        Me.colREMARKS.ColumnEdit = Me.txtREMARKS
        Me.colREMARKS.FieldName = "REMARKS"
        Me.colREMARKS.Name = "colREMARKS"
        Me.colREMARKS.Visible = True
        Me.colREMARKS.VisibleIndex = 4
        '
        'txtREMARKS
        '
        Me.txtREMARKS.AutoHeight = False
        Me.txtREMARKS.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtREMARKS.Name = "txtREMARKS"
        '
        'grdNOINVOICE
        '
        Me.grdNOINVOICE.AutoHeight = False
        Me.grdNOINVOICE.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdNOINVOICE.Name = "grdNOINVOICE"
        Me.grdNOINVOICE.NullText = ""
        Me.grdNOINVOICE.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdNOINVOICE.View = Me.grvNOINVOICE
        '
        'grvNOINVOICE
        '
        Me.grvNOINVOICE.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn4, Me.GridColumn1, Me.GridColumn5, Me.GridColumn2})
        Me.grvNOINVOICE.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvNOINVOICE.Name = "grvNOINVOICE"
        Me.grvNOINVOICE.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvNOINVOICE.OptionsView.ShowAutoFilterRow = True
        Me.grvNOINVOICE.OptionsView.ShowGroupPanel = False
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Number"
        Me.GridColumn4.FieldName = "NOINVOICE"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 0
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Tanggal"
        Me.GridColumn1.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn1.FieldName = "DATE"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 1
        '
        'GridColumn5
        '
        Me.GridColumn5.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn5.Caption = "Total"
        Me.GridColumn5.DisplayFormat.FormatString = "{0:n2}"
        Me.GridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn5.FieldName = "GRANDTOTAL"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 2
        '
        'GridColumn2
        '
        Me.GridColumn2.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn2.Caption = "Telah Dibayar"
        Me.GridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn2.FieldName = "PAYAMOUNT"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 3
        '
        'tab7
        '
        Me.tab7.Controls.Add(Me.grdDetail_R)
        Me.tab7.Name = "tab7"
        Me.tab7.Size = New System.Drawing.Size(760, 257)
        Me.tab7.Text = "Retur Resep"
        '
        'grdDetail_R
        '
        Me.grdDetail_R.DataSource = Me.bindingSource_R
        Me.grdDetail_R.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdDetail_R.Location = New System.Drawing.Point(0, 0)
        Me.grdDetail_R.MainView = Me.grvDetail_R
        Me.grdDetail_R.MenuManager = Me.barManager
        Me.grdDetail_R.Name = "grdDetail_R"
        Me.grdDetail_R.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemGridLookUpEdit3, Me.grdNOINVOICE_R, Me.txtREMARKS_R})
        Me.grdDetail_R.Size = New System.Drawing.Size(760, 257)
        Me.grdDetail_R.TabIndex = 21
        Me.grdDetail_R.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvDetail_R})
        '
        'bindingSource_R
        '
        Me.bindingSource_R.DataSource = GetType(DataAccess.F_CASHIN_D)
        '
        'grvDetail_R
        '
        Me.grvDetail_R.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colNOINVOICE_R, Me.colAMOUNTORIGINAL_UB_R, Me.colAMOUNTDUE_UB_R, Me.colAMOUNTPAYMENT_R, Me.colREMARKS_R})
        Me.grvDetail_R.GridControl = Me.grdDetail_R
        Me.grvDetail_R.Name = "grvDetail_R"
        Me.grvDetail_R.OptionsCustomization.AllowColumnMoving = False
        Me.grvDetail_R.OptionsCustomization.AllowFilter = False
        Me.grvDetail_R.OptionsCustomization.AllowGroup = False
        Me.grvDetail_R.OptionsCustomization.AllowQuickHideColumns = False
        Me.grvDetail_R.OptionsCustomization.AllowSort = False
        Me.grvDetail_R.OptionsDetail.EnableMasterViewMode = False
        Me.grvDetail_R.OptionsFind.AllowFindPanel = False
        Me.grvDetail_R.OptionsLayout.StoreAllOptions = True
        Me.grvDetail_R.OptionsLayout.StoreAppearance = True
        Me.grvDetail_R.OptionsMenu.EnableColumnMenu = False
        Me.grvDetail_R.OptionsNavigation.AutoFocusNewRow = True
        Me.grvDetail_R.OptionsNavigation.EnterMoveNextColumn = True
        Me.grvDetail_R.OptionsView.EnableAppearanceEvenRow = True
        Me.grvDetail_R.OptionsView.EnableAppearanceOddRow = True
        Me.grvDetail_R.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.grvDetail_R.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.grvDetail_R.OptionsView.ShowFooter = True
        Me.grvDetail_R.OptionsView.ShowGroupPanel = False
        '
        'colNOINVOICE_R
        '
        Me.colNOINVOICE_R.Caption = "No. Retur"
        Me.colNOINVOICE_R.FieldName = "NOINVOICE"
        Me.colNOINVOICE_R.Name = "colNOINVOICE_R"
        Me.colNOINVOICE_R.Visible = True
        Me.colNOINVOICE_R.VisibleIndex = 0
        '
        'colAMOUNTORIGINAL_UB_R
        '
        Me.colAMOUNTORIGINAL_UB_R.Caption = "Amount Original"
        Me.colAMOUNTORIGINAL_UB_R.DisplayFormat.FormatString = "{0:n2}"
        Me.colAMOUNTORIGINAL_UB_R.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colAMOUNTORIGINAL_UB_R.FieldName = "AMOUNTORIGINAL"
        Me.colAMOUNTORIGINAL_UB_R.Name = "colAMOUNTORIGINAL_UB_R"
        Me.colAMOUNTORIGINAL_UB_R.OptionsColumn.AllowEdit = False
        Me.colAMOUNTORIGINAL_UB_R.OptionsColumn.AllowFocus = False
        Me.colAMOUNTORIGINAL_UB_R.OptionsColumn.ReadOnly = True
        Me.colAMOUNTORIGINAL_UB_R.OptionsColumn.TabStop = False
        Me.colAMOUNTORIGINAL_UB_R.UnboundType = DevExpress.Data.UnboundColumnType.[Decimal]
        Me.colAMOUNTORIGINAL_UB_R.Visible = True
        Me.colAMOUNTORIGINAL_UB_R.VisibleIndex = 1
        '
        'colAMOUNTDUE_UB_R
        '
        Me.colAMOUNTDUE_UB_R.Caption = "Amount Due"
        Me.colAMOUNTDUE_UB_R.DisplayFormat.FormatString = "{0:n2}"
        Me.colAMOUNTDUE_UB_R.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colAMOUNTDUE_UB_R.FieldName = "AMOUNTDUE"
        Me.colAMOUNTDUE_UB_R.Name = "colAMOUNTDUE_UB_R"
        Me.colAMOUNTDUE_UB_R.OptionsColumn.AllowEdit = False
        Me.colAMOUNTDUE_UB_R.OptionsColumn.AllowFocus = False
        Me.colAMOUNTDUE_UB_R.OptionsColumn.ReadOnly = True
        Me.colAMOUNTDUE_UB_R.OptionsColumn.TabStop = False
        Me.colAMOUNTDUE_UB_R.UnboundType = DevExpress.Data.UnboundColumnType.[Decimal]
        Me.colAMOUNTDUE_UB_R.Visible = True
        Me.colAMOUNTDUE_UB_R.VisibleIndex = 2
        '
        'colAMOUNTPAYMENT_R
        '
        Me.colAMOUNTPAYMENT_R.AppearanceCell.Options.UseTextOptions = True
        Me.colAMOUNTPAYMENT_R.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.colAMOUNTPAYMENT_R.Caption = "Amount Payment"
        Me.colAMOUNTPAYMENT_R.DisplayFormat.FormatString = "{0:n2}"
        Me.colAMOUNTPAYMENT_R.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colAMOUNTPAYMENT_R.FieldName = "AMOUNTPAYMENT"
        Me.colAMOUNTPAYMENT_R.Name = "colAMOUNTPAYMENT_R"
        Me.colAMOUNTPAYMENT_R.Visible = True
        Me.colAMOUNTPAYMENT_R.VisibleIndex = 3
        '
        'colREMARKS_R
        '
        Me.colREMARKS_R.AppearanceCell.Options.UseTextOptions = True
        Me.colREMARKS_R.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.colREMARKS_R.Caption = "Remarks"
        Me.colREMARKS_R.ColumnEdit = Me.txtREMARKS_R
        Me.colREMARKS_R.FieldName = "REMARKS"
        Me.colREMARKS_R.Name = "colREMARKS_R"
        Me.colREMARKS_R.Visible = True
        Me.colREMARKS_R.VisibleIndex = 4
        '
        'txtREMARKS_R
        '
        Me.txtREMARKS_R.AutoHeight = False
        Me.txtREMARKS_R.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtREMARKS_R.Name = "txtREMARKS_R"
        '
        'RepositoryItemGridLookUpEdit3
        '
        Me.RepositoryItemGridLookUpEdit3.AutoHeight = False
        Me.RepositoryItemGridLookUpEdit3.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemGridLookUpEdit3.Name = "RepositoryItemGridLookUpEdit3"
        Me.RepositoryItemGridLookUpEdit3.NullText = ""
        Me.RepositoryItemGridLookUpEdit3.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.RepositoryItemGridLookUpEdit3.View = Me.GridView10
        '
        'GridView10
        '
        Me.GridView10.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn36, Me.GridColumn37, Me.GridColumn39})
        Me.GridView10.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView10.Name = "GridView10"
        Me.GridView10.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView10.OptionsView.ShowAutoFilterRow = True
        Me.GridView10.OptionsView.ShowGroupPanel = False
        '
        'GridColumn36
        '
        Me.GridColumn36.Caption = "Item Name #1"
        Me.GridColumn36.FieldName = "NMITEM1"
        Me.GridColumn36.Name = "GridColumn36"
        Me.GridColumn36.Visible = True
        Me.GridColumn36.VisibleIndex = 0
        '
        'GridColumn37
        '
        Me.GridColumn37.Caption = "Item Name #2"
        Me.GridColumn37.FieldName = "NMITEM2"
        Me.GridColumn37.Name = "GridColumn37"
        Me.GridColumn37.Visible = True
        Me.GridColumn37.VisibleIndex = 1
        '
        'GridColumn39
        '
        Me.GridColumn39.Caption = "Item Name #3"
        Me.GridColumn39.FieldName = "NMITEM3"
        Me.GridColumn39.Name = "GridColumn39"
        Me.GridColumn39.Visible = True
        Me.GridColumn39.VisibleIndex = 2
        '
        'grdNOINVOICE_R
        '
        Me.grdNOINVOICE_R.AutoHeight = False
        Me.grdNOINVOICE_R.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdNOINVOICE_R.Name = "grdNOINVOICE_R"
        Me.grdNOINVOICE_R.NullText = ""
        Me.grdNOINVOICE_R.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdNOINVOICE_R.View = Me.grvNOINVOICE_R
        '
        'grvNOINVOICE_R
        '
        Me.grvNOINVOICE_R.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn30, Me.GridColumn32, Me.GridColumn34, Me.GridColumn35})
        Me.grvNOINVOICE_R.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvNOINVOICE_R.Name = "grvNOINVOICE_R"
        Me.grvNOINVOICE_R.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvNOINVOICE_R.OptionsView.ShowAutoFilterRow = True
        Me.grvNOINVOICE_R.OptionsView.ShowGroupPanel = False
        '
        'GridColumn30
        '
        Me.GridColumn30.Caption = "Number"
        Me.GridColumn30.FieldName = "NOINVOICE"
        Me.GridColumn30.Name = "GridColumn30"
        Me.GridColumn30.Visible = True
        Me.GridColumn30.VisibleIndex = 0
        '
        'GridColumn32
        '
        Me.GridColumn32.Caption = "Tanggal"
        Me.GridColumn32.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GridColumn32.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn32.FieldName = "DATE"
        Me.GridColumn32.Name = "GridColumn32"
        Me.GridColumn32.Visible = True
        Me.GridColumn32.VisibleIndex = 1
        '
        'GridColumn34
        '
        Me.GridColumn34.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn34.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn34.Caption = "Total"
        Me.GridColumn34.DisplayFormat.FormatString = "{0:n2}"
        Me.GridColumn34.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn34.FieldName = "GRANDTOTAL"
        Me.GridColumn34.Name = "GridColumn34"
        Me.GridColumn34.Visible = True
        Me.GridColumn34.VisibleIndex = 2
        '
        'GridColumn35
        '
        Me.GridColumn35.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn35.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn35.Caption = "Telah Dibayar"
        Me.GridColumn35.DisplayFormat.FormatString = "{0:n2}"
        Me.GridColumn35.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn35.FieldName = "PAYAMOUNT"
        Me.GridColumn35.Name = "GridColumn35"
        Me.GridColumn35.Visible = True
        Me.GridColumn35.VisibleIndex = 3
        '
        'tab2
        '
        Me.tab2.Controls.Add(Me.grdDetailRincian)
        Me.tab2.Name = "tab2"
        Me.tab2.Size = New System.Drawing.Size(760, 257)
        Me.tab2.Text = "Informasi Rincian"
        '
        'grdDetailRincian
        '
        Me.grdDetailRincian.ContextMenuStrip = Me.mnuStrip
        Me.grdDetailRincian.DataSource = Me.BindingSourceRincian
        Me.grdDetailRincian.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdDetailRincian.Location = New System.Drawing.Point(0, 0)
        Me.grdDetailRincian.MainView = Me.grvDeatilRincian
        Me.grdDetailRincian.MenuManager = Me.barManager
        Me.grdDetailRincian.Name = "grdDetailRincian"
        Me.grdDetailRincian.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.grdKDITEM, Me.grdKDUOM, Me.RepositoryItemMemoExEdit1, Me.grdKDDOCTOR, Me.grdKDDEPARTMENT, Me.grdKDSIGNA, Me.chkISRACIK, Me.deDateCretaed})
        Me.grdDetailRincian.Size = New System.Drawing.Size(760, 257)
        Me.grdDetailRincian.TabIndex = 19
        Me.grdDetailRincian.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvDeatilRincian})
        '
        'BindingSourceRincian
        '
        Me.BindingSourceRincian.DataSource = GetType(DataAccess.S_SO_TRANSAKSI_D)
        '
        'grvDeatilRincian
        '
        Me.grvDeatilRincian.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colKDSOTRANSAKSI, Me.colDATECREATED, Me.colCATEGORYI, Me.colKDITEM, Me.colQTY, Me.colKDUOM, Me.colKDDOCTOR, Me.colKDDEPARTMENT, Me.colPRICE, Me.colSUBTOTAL, Me.colDISCOUNT, Me.colGRANDTOTAL})
        Me.grvDeatilRincian.GridControl = Me.grdDetailRincian
        Me.grvDeatilRincian.Name = "grvDeatilRincian"
        Me.grvDeatilRincian.OptionsCustomization.AllowColumnMoving = False
        Me.grvDeatilRincian.OptionsCustomization.AllowFilter = False
        Me.grvDeatilRincian.OptionsCustomization.AllowGroup = False
        Me.grvDeatilRincian.OptionsCustomization.AllowQuickHideColumns = False
        Me.grvDeatilRincian.OptionsCustomization.AllowSort = False
        Me.grvDeatilRincian.OptionsDetail.EnableMasterViewMode = False
        Me.grvDeatilRincian.OptionsFind.AllowFindPanel = False
        Me.grvDeatilRincian.OptionsLayout.StoreAllOptions = True
        Me.grvDeatilRincian.OptionsLayout.StoreAppearance = True
        Me.grvDeatilRincian.OptionsMenu.EnableColumnMenu = False
        Me.grvDeatilRincian.OptionsNavigation.AutoFocusNewRow = True
        Me.grvDeatilRincian.OptionsNavigation.EnterMoveNextColumn = True
        Me.grvDeatilRincian.OptionsView.EnableAppearanceEvenRow = True
        Me.grvDeatilRincian.OptionsView.EnableAppearanceOddRow = True
        Me.grvDeatilRincian.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.grvDeatilRincian.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.grvDeatilRincian.OptionsView.ShowFooter = True
        Me.grvDeatilRincian.OptionsView.ShowGroupPanel = False
        '
        'colKDSOTRANSAKSI
        '
        Me.colKDSOTRANSAKSI.Caption = "No Transaksi"
        Me.colKDSOTRANSAKSI.FieldName = "KDSOTRANSAKSI"
        Me.colKDSOTRANSAKSI.Name = "colKDSOTRANSAKSI"
        Me.colKDSOTRANSAKSI.OptionsColumn.AllowEdit = False
        Me.colKDSOTRANSAKSI.OptionsColumn.AllowFocus = False
        Me.colKDSOTRANSAKSI.OptionsColumn.ReadOnly = True
        Me.colKDSOTRANSAKSI.OptionsColumn.TabStop = False
        '
        'colDATECREATED
        '
        Me.colDATECREATED.Caption = "Input"
        Me.colDATECREATED.ColumnEdit = Me.deDateCretaed
        Me.colDATECREATED.DisplayFormat.FormatString = "dd-MM-yyyy"
        Me.colDATECREATED.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.colDATECREATED.FieldName = "DATECREATED"
        Me.colDATECREATED.Name = "colDATECREATED"
        Me.colDATECREATED.OptionsColumn.AllowEdit = False
        Me.colDATECREATED.OptionsColumn.AllowFocus = False
        Me.colDATECREATED.OptionsColumn.ReadOnly = True
        Me.colDATECREATED.OptionsColumn.TabStop = False
        Me.colDATECREATED.Visible = True
        Me.colDATECREATED.VisibleIndex = 0
        '
        'deDateCretaed
        '
        Me.deDateCretaed.AutoHeight = False
        Me.deDateCretaed.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deDateCretaed.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deDateCretaed.DisplayFormat.FormatString = "dd-MM-yyyy"
        Me.deDateCretaed.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.deDateCretaed.EditFormat.FormatString = "dd-MM-yyyy"
        Me.deDateCretaed.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.deDateCretaed.Mask.EditMask = "dd-MM-yyyy"
        Me.deDateCretaed.Name = "deDateCretaed"
        Me.deDateCretaed.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        '
        'colCATEGORYI
        '
        Me.colCATEGORYI.Caption = "Transaksi"
        Me.colCATEGORYI.FieldName = "REMARKS"
        Me.colCATEGORYI.Name = "colCATEGORYI"
        Me.colCATEGORYI.OptionsColumn.AllowEdit = False
        Me.colCATEGORYI.OptionsColumn.AllowFocus = False
        Me.colCATEGORYI.OptionsColumn.ReadOnly = True
        Me.colCATEGORYI.OptionsColumn.TabStop = False
        Me.colCATEGORYI.Visible = True
        Me.colCATEGORYI.VisibleIndex = 1
        '
        'colKDITEM
        '
        Me.colKDITEM.Caption = "Tarif"
        Me.colKDITEM.ColumnEdit = Me.grdKDITEM
        Me.colKDITEM.FieldName = "KDITEM"
        Me.colKDITEM.Name = "colKDITEM"
        Me.colKDITEM.OptionsColumn.AllowEdit = False
        Me.colKDITEM.OptionsColumn.AllowFocus = False
        Me.colKDITEM.OptionsColumn.ReadOnly = True
        Me.colKDITEM.Visible = True
        Me.colKDITEM.VisibleIndex = 2
        '
        'grdKDITEM
        '
        Me.grdKDITEM.AutoHeight = False
        Me.grdKDITEM.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDITEM.Name = "grdKDITEM"
        Me.grdKDITEM.NullText = ""
        Me.grdKDITEM.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdKDITEM.View = Me.grvKDITEM
        '
        'grvKDITEM
        '
        Me.grvKDITEM.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn14, Me.colSTOK})
        Me.grvKDITEM.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvKDITEM.Name = "grvKDITEM"
        Me.grvKDITEM.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvKDITEM.OptionsView.ShowAutoFilterRow = True
        Me.grvKDITEM.OptionsView.ShowGroupPanel = False
        '
        'GridColumn14
        '
        Me.GridColumn14.Caption = "Item Name #2"
        Me.GridColumn14.FieldName = "NMITEM2"
        Me.GridColumn14.Name = "GridColumn14"
        Me.GridColumn14.Visible = True
        Me.GridColumn14.VisibleIndex = 0
        '
        'colSTOK
        '
        Me.colSTOK.Caption = "Stok"
        Me.colSTOK.DisplayFormat.FormatString = "{0:n2}"
        Me.colSTOK.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colSTOK.FieldName = "STOK"
        Me.colSTOK.Name = "colSTOK"
        Me.colSTOK.Visible = True
        Me.colSTOK.VisibleIndex = 1
        '
        'colQTY
        '
        Me.colQTY.AppearanceCell.Options.UseTextOptions = True
        Me.colQTY.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.colQTY.Caption = "Jumlah"
        Me.colQTY.DisplayFormat.FormatString = "{0:n2}"
        Me.colQTY.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colQTY.FieldName = "QTY"
        Me.colQTY.Name = "colQTY"
        Me.colQTY.OptionsColumn.AllowEdit = False
        Me.colQTY.OptionsColumn.AllowFocus = False
        Me.colQTY.OptionsColumn.ReadOnly = True
        Me.colQTY.OptionsColumn.TabStop = False
        Me.colQTY.Visible = True
        Me.colQTY.VisibleIndex = 3
        '
        'colKDUOM
        '
        Me.colKDUOM.Caption = "Kelas"
        Me.colKDUOM.ColumnEdit = Me.grdKDUOM
        Me.colKDUOM.FieldName = "KDUOM"
        Me.colKDUOM.Name = "colKDUOM"
        Me.colKDUOM.OptionsColumn.AllowEdit = False
        Me.colKDUOM.OptionsColumn.AllowFocus = False
        Me.colKDUOM.OptionsColumn.ReadOnly = True
        Me.colKDUOM.OptionsColumn.TabStop = False
        Me.colKDUOM.Visible = True
        Me.colKDUOM.VisibleIndex = 4
        '
        'grdKDUOM
        '
        Me.grdKDUOM.AutoHeight = False
        Me.grdKDUOM.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDUOM.Name = "grdKDUOM"
        Me.grdKDUOM.NullText = ""
        Me.grdKDUOM.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdKDUOM.View = Me.grvKDUOM
        '
        'grvKDUOM
        '
        Me.grvKDUOM.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn15})
        Me.grvKDUOM.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvKDUOM.Name = "grvKDUOM"
        Me.grvKDUOM.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvKDUOM.OptionsView.ShowAutoFilterRow = True
        Me.grvKDUOM.OptionsView.ShowGroupPanel = False
        '
        'GridColumn15
        '
        Me.GridColumn15.Caption = "Memo"
        Me.GridColumn15.FieldName = "MEMO"
        Me.GridColumn15.Name = "GridColumn15"
        Me.GridColumn15.Visible = True
        Me.GridColumn15.VisibleIndex = 0
        '
        'colKDDOCTOR
        '
        Me.colKDDOCTOR.Caption = "Dokter"
        Me.colKDDOCTOR.ColumnEdit = Me.grdKDDOCTOR
        Me.colKDDOCTOR.FieldName = "KDDOCTOR"
        Me.colKDDOCTOR.Name = "colKDDOCTOR"
        Me.colKDDOCTOR.OptionsColumn.AllowEdit = False
        Me.colKDDOCTOR.OptionsColumn.AllowFocus = False
        Me.colKDDOCTOR.OptionsColumn.ReadOnly = True
        Me.colKDDOCTOR.OptionsColumn.TabStop = False
        Me.colKDDOCTOR.Visible = True
        Me.colKDDOCTOR.VisibleIndex = 5
        '
        'grdKDDOCTOR
        '
        Me.grdKDDOCTOR.AutoHeight = False
        Me.grdKDDOCTOR.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDDOCTOR.Name = "grdKDDOCTOR"
        Me.grdKDDOCTOR.NullText = ""
        Me.grdKDDOCTOR.View = Me.grvKDDOCTOR
        '
        'grvKDDOCTOR
        '
        Me.grvKDDOCTOR.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn16})
        Me.grvKDDOCTOR.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvKDDOCTOR.Name = "grvKDDOCTOR"
        Me.grvKDDOCTOR.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvKDDOCTOR.OptionsView.ShowGroupPanel = False
        '
        'GridColumn16
        '
        Me.GridColumn16.Caption = "Name Display"
        Me.GridColumn16.FieldName = "NAME_DISPLAY"
        Me.GridColumn16.Name = "GridColumn16"
        Me.GridColumn16.Visible = True
        Me.GridColumn16.VisibleIndex = 0
        '
        'colKDDEPARTMENT
        '
        Me.colKDDEPARTMENT.Caption = "Unit"
        Me.colKDDEPARTMENT.ColumnEdit = Me.grdKDDEPARTMENT
        Me.colKDDEPARTMENT.FieldName = "KDDEPARTMENT"
        Me.colKDDEPARTMENT.Name = "colKDDEPARTMENT"
        Me.colKDDEPARTMENT.OptionsColumn.AllowEdit = False
        Me.colKDDEPARTMENT.OptionsColumn.AllowFocus = False
        Me.colKDDEPARTMENT.OptionsColumn.ReadOnly = True
        Me.colKDDEPARTMENT.OptionsColumn.TabStop = False
        Me.colKDDEPARTMENT.Visible = True
        Me.colKDDEPARTMENT.VisibleIndex = 6
        '
        'grdKDDEPARTMENT
        '
        Me.grdKDDEPARTMENT.AutoHeight = False
        Me.grdKDDEPARTMENT.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDDEPARTMENT.Name = "grdKDDEPARTMENT"
        Me.grdKDDEPARTMENT.NullText = ""
        Me.grdKDDEPARTMENT.View = Me.grvKDDEPARTMENT
        '
        'grvKDDEPARTMENT
        '
        Me.grvKDDEPARTMENT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn21})
        Me.grvKDDEPARTMENT.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvKDDEPARTMENT.Name = "grvKDDEPARTMENT"
        Me.grvKDDEPARTMENT.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvKDDEPARTMENT.OptionsView.ShowGroupPanel = False
        '
        'GridColumn21
        '
        Me.GridColumn21.Caption = "Name Display"
        Me.GridColumn21.FieldName = "NAME_DISPLAY"
        Me.GridColumn21.Name = "GridColumn21"
        Me.GridColumn21.Visible = True
        Me.GridColumn21.VisibleIndex = 0
        '
        'colPRICE
        '
        Me.colPRICE.Caption = "Harga"
        Me.colPRICE.DisplayFormat.FormatString = "{0:n2}"
        Me.colPRICE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colPRICE.FieldName = "PRICE"
        Me.colPRICE.Name = "colPRICE"
        Me.colPRICE.OptionsColumn.AllowEdit = False
        Me.colPRICE.OptionsColumn.AllowFocus = False
        Me.colPRICE.OptionsColumn.ReadOnly = True
        Me.colPRICE.OptionsColumn.TabStop = False
        Me.colPRICE.Visible = True
        Me.colPRICE.VisibleIndex = 7
        '
        'colSUBTOTAL
        '
        Me.colSUBTOTAL.Caption = "Subtotal"
        Me.colSUBTOTAL.DisplayFormat.FormatString = "{0:n2}"
        Me.colSUBTOTAL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colSUBTOTAL.FieldName = "SUBTOTAL"
        Me.colSUBTOTAL.Name = "colSUBTOTAL"
        Me.colSUBTOTAL.OptionsColumn.AllowEdit = False
        Me.colSUBTOTAL.OptionsColumn.AllowFocus = False
        Me.colSUBTOTAL.OptionsColumn.ReadOnly = True
        Me.colSUBTOTAL.OptionsColumn.TabStop = False
        Me.colSUBTOTAL.Visible = True
        Me.colSUBTOTAL.VisibleIndex = 8
        '
        'colDISCOUNT
        '
        Me.colDISCOUNT.Caption = "Diskon"
        Me.colDISCOUNT.DisplayFormat.FormatString = "{0:n2}"
        Me.colDISCOUNT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colDISCOUNT.FieldName = "DISCOUNT"
        Me.colDISCOUNT.Name = "colDISCOUNT"
        Me.colDISCOUNT.OptionsColumn.AllowEdit = False
        Me.colDISCOUNT.OptionsColumn.AllowFocus = False
        Me.colDISCOUNT.OptionsColumn.ReadOnly = True
        Me.colDISCOUNT.OptionsColumn.TabStop = False
        Me.colDISCOUNT.Visible = True
        Me.colDISCOUNT.VisibleIndex = 9
        '
        'colGRANDTOTAL
        '
        Me.colGRANDTOTAL.Caption = "Grand Total"
        Me.colGRANDTOTAL.DisplayFormat.FormatString = "{0:n2}"
        Me.colGRANDTOTAL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colGRANDTOTAL.FieldName = "GRANDTOTAL"
        Me.colGRANDTOTAL.Name = "colGRANDTOTAL"
        Me.colGRANDTOTAL.OptionsColumn.AllowEdit = False
        Me.colGRANDTOTAL.OptionsColumn.AllowFocus = False
        Me.colGRANDTOTAL.OptionsColumn.ReadOnly = True
        Me.colGRANDTOTAL.OptionsColumn.TabStop = False
        Me.colGRANDTOTAL.Visible = True
        Me.colGRANDTOTAL.VisibleIndex = 10
        '
        'RepositoryItemMemoExEdit1
        '
        Me.RepositoryItemMemoExEdit1.AutoHeight = False
        Me.RepositoryItemMemoExEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemMemoExEdit1.Name = "RepositoryItemMemoExEdit1"
        '
        'grdKDSIGNA
        '
        Me.grdKDSIGNA.AutoHeight = False
        Me.grdKDSIGNA.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDSIGNA.Name = "grdKDSIGNA"
        Me.grdKDSIGNA.NullText = ""
        Me.grdKDSIGNA.View = Me.grvKDSIGNA
        '
        'grvKDSIGNA
        '
        Me.grvKDSIGNA.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.MEMO})
        Me.grvKDSIGNA.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvKDSIGNA.Name = "grvKDSIGNA"
        Me.grvKDSIGNA.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvKDSIGNA.OptionsView.ShowGroupPanel = False
        '
        'MEMO
        '
        Me.MEMO.Caption = "Memo"
        Me.MEMO.Name = "MEMO"
        Me.MEMO.Visible = True
        Me.MEMO.VisibleIndex = 0
        '
        'chkISRACIK
        '
        Me.chkISRACIK.AutoHeight = False
        Me.chkISRACIK.Name = "chkISRACIK"
        Me.chkISRACIK.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'tab4
        '
        Me.tab4.Controls.Add(Me.grdDetailObat)
        Me.tab4.Name = "tab4"
        Me.tab4.Size = New System.Drawing.Size(760, 257)
        Me.tab4.Text = "Informasi Obat"
        '
        'grdDetailObat
        '
        Me.grdDetailObat.ContextMenuStrip = Me.mnuStrip
        Me.grdDetailObat.DataSource = Me.BindingSourceObat
        Me.grdDetailObat.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdDetailObat.Location = New System.Drawing.Point(0, 0)
        Me.grdDetailObat.MainView = Me.grvDetailObat
        Me.grdDetailObat.MenuManager = Me.barManager
        Me.grdDetailObat.Name = "grdDetailObat"
        Me.grdDetailObat.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.grdKDITEM_OBAT, Me.grdKDUOM_OBAT, Me.RepositoryItemMemoExEdit2, Me.grdKDDOCTOR_OBAT, Me.grdKDDEPARTMENT_OBAT, Me.RepositoryItemGridLookUpEdit5, Me.RepositoryItemCheckEdit1, Me.RepositoryItemDateEdit1})
        Me.grdDetailObat.Size = New System.Drawing.Size(760, 257)
        Me.grdDetailObat.TabIndex = 20
        Me.grdDetailObat.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvDetailObat})
        '
        'BindingSourceObat
        '
        Me.BindingSourceObat.DataSource = GetType(DataAccess.S_SO_TRANSAKSI_D)
        '
        'grvDetailObat
        '
        Me.grvDetailObat.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colKDSOTRANSKASI_OBAT, Me.colDATECREATED_OBAT, Me.colKDITEM_OBAT, Me.colQTY_OBAT, Me.colKDUOM_OBAT, Me.colKDDOCTOR_OBAT, Me.colKDDEPARTMENT_OBAT, Me.colPRICE_OBAT, Me.colSUBTOTAL_OBAT, Me.colDISCOUNT_OBAT, Me.colGRANDTORAL_OBAT})
        Me.grvDetailObat.GridControl = Me.grdDetailObat
        Me.grvDetailObat.Name = "grvDetailObat"
        Me.grvDetailObat.OptionsCustomization.AllowColumnMoving = False
        Me.grvDetailObat.OptionsCustomization.AllowFilter = False
        Me.grvDetailObat.OptionsCustomization.AllowGroup = False
        Me.grvDetailObat.OptionsCustomization.AllowQuickHideColumns = False
        Me.grvDetailObat.OptionsCustomization.AllowSort = False
        Me.grvDetailObat.OptionsDetail.EnableMasterViewMode = False
        Me.grvDetailObat.OptionsFind.AllowFindPanel = False
        Me.grvDetailObat.OptionsLayout.StoreAllOptions = True
        Me.grvDetailObat.OptionsLayout.StoreAppearance = True
        Me.grvDetailObat.OptionsMenu.EnableColumnMenu = False
        Me.grvDetailObat.OptionsNavigation.AutoFocusNewRow = True
        Me.grvDetailObat.OptionsNavigation.EnterMoveNextColumn = True
        Me.grvDetailObat.OptionsView.EnableAppearanceEvenRow = True
        Me.grvDetailObat.OptionsView.EnableAppearanceOddRow = True
        Me.grvDetailObat.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.grvDetailObat.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.grvDetailObat.OptionsView.ShowFooter = True
        Me.grvDetailObat.OptionsView.ShowGroupPanel = False
        '
        'colKDSOTRANSKASI_OBAT
        '
        Me.colKDSOTRANSKASI_OBAT.Caption = "No Transaksi"
        Me.colKDSOTRANSKASI_OBAT.FieldName = "KDSOTRANSAKSI"
        Me.colKDSOTRANSKASI_OBAT.Name = "colKDSOTRANSKASI_OBAT"
        Me.colKDSOTRANSKASI_OBAT.OptionsColumn.AllowEdit = False
        Me.colKDSOTRANSKASI_OBAT.OptionsColumn.AllowFocus = False
        Me.colKDSOTRANSKASI_OBAT.OptionsColumn.ReadOnly = True
        Me.colKDSOTRANSKASI_OBAT.OptionsColumn.TabStop = False
        Me.colKDSOTRANSKASI_OBAT.Visible = True
        Me.colKDSOTRANSKASI_OBAT.VisibleIndex = 0
        '
        'colDATECREATED_OBAT
        '
        Me.colDATECREATED_OBAT.Caption = "Input"
        Me.colDATECREATED_OBAT.ColumnEdit = Me.RepositoryItemDateEdit1
        Me.colDATECREATED_OBAT.DisplayFormat.FormatString = "d"
        Me.colDATECREATED_OBAT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.colDATECREATED_OBAT.FieldName = "DATECREATED"
        Me.colDATECREATED_OBAT.Name = "colDATECREATED_OBAT"
        Me.colDATECREATED_OBAT.OptionsColumn.AllowEdit = False
        Me.colDATECREATED_OBAT.OptionsColumn.AllowFocus = False
        Me.colDATECREATED_OBAT.OptionsColumn.ReadOnly = True
        Me.colDATECREATED_OBAT.OptionsColumn.TabStop = False
        Me.colDATECREATED_OBAT.Visible = True
        Me.colDATECREATED_OBAT.VisibleIndex = 1
        '
        'RepositoryItemDateEdit1
        '
        Me.RepositoryItemDateEdit1.AutoHeight = False
        Me.RepositoryItemDateEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateEdit1.DisplayFormat.FormatString = "dd-MM-yyyy"
        Me.RepositoryItemDateEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemDateEdit1.EditFormat.FormatString = "dd-MM-yyyy"
        Me.RepositoryItemDateEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemDateEdit1.Mask.EditMask = "dd-MM-yyyy"
        Me.RepositoryItemDateEdit1.Name = "RepositoryItemDateEdit1"
        Me.RepositoryItemDateEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        '
        'colKDITEM_OBAT
        '
        Me.colKDITEM_OBAT.Caption = "Obat"
        Me.colKDITEM_OBAT.ColumnEdit = Me.grdKDITEM_OBAT
        Me.colKDITEM_OBAT.FieldName = "KDITEM"
        Me.colKDITEM_OBAT.Name = "colKDITEM_OBAT"
        Me.colKDITEM_OBAT.OptionsColumn.AllowEdit = False
        Me.colKDITEM_OBAT.OptionsColumn.AllowFocus = False
        Me.colKDITEM_OBAT.OptionsColumn.ReadOnly = True
        Me.colKDITEM_OBAT.Visible = True
        Me.colKDITEM_OBAT.VisibleIndex = 2
        '
        'grdKDITEM_OBAT
        '
        Me.grdKDITEM_OBAT.AutoHeight = False
        Me.grdKDITEM_OBAT.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDITEM_OBAT.Name = "grdKDITEM_OBAT"
        Me.grdKDITEM_OBAT.NullText = ""
        Me.grdKDITEM_OBAT.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdKDITEM_OBAT.View = Me.GridView3
        '
        'GridView3
        '
        Me.GridView3.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn25, Me.GridColumn26})
        Me.GridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView3.Name = "GridView3"
        Me.GridView3.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView3.OptionsView.ShowAutoFilterRow = True
        Me.GridView3.OptionsView.ShowGroupPanel = False
        '
        'GridColumn25
        '
        Me.GridColumn25.Caption = "Item Name #2"
        Me.GridColumn25.FieldName = "NMITEM2"
        Me.GridColumn25.Name = "GridColumn25"
        Me.GridColumn25.Visible = True
        Me.GridColumn25.VisibleIndex = 0
        '
        'GridColumn26
        '
        Me.GridColumn26.Caption = "Stok"
        Me.GridColumn26.DisplayFormat.FormatString = "{0:n2}"
        Me.GridColumn26.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn26.FieldName = "STOK"
        Me.GridColumn26.Name = "GridColumn26"
        Me.GridColumn26.Visible = True
        Me.GridColumn26.VisibleIndex = 1
        '
        'colQTY_OBAT
        '
        Me.colQTY_OBAT.AppearanceCell.Options.UseTextOptions = True
        Me.colQTY_OBAT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.colQTY_OBAT.Caption = "Jumlah"
        Me.colQTY_OBAT.DisplayFormat.FormatString = "{0:n2}"
        Me.colQTY_OBAT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colQTY_OBAT.FieldName = "QTY"
        Me.colQTY_OBAT.Name = "colQTY_OBAT"
        Me.colQTY_OBAT.OptionsColumn.AllowEdit = False
        Me.colQTY_OBAT.OptionsColumn.AllowFocus = False
        Me.colQTY_OBAT.OptionsColumn.ReadOnly = True
        Me.colQTY_OBAT.OptionsColumn.TabStop = False
        Me.colQTY_OBAT.Visible = True
        Me.colQTY_OBAT.VisibleIndex = 3
        '
        'colKDUOM_OBAT
        '
        Me.colKDUOM_OBAT.Caption = "Satuan"
        Me.colKDUOM_OBAT.ColumnEdit = Me.grdKDUOM_OBAT
        Me.colKDUOM_OBAT.FieldName = "KDUOM"
        Me.colKDUOM_OBAT.Name = "colKDUOM_OBAT"
        Me.colKDUOM_OBAT.OptionsColumn.AllowEdit = False
        Me.colKDUOM_OBAT.OptionsColumn.AllowFocus = False
        Me.colKDUOM_OBAT.OptionsColumn.ReadOnly = True
        Me.colKDUOM_OBAT.OptionsColumn.TabStop = False
        Me.colKDUOM_OBAT.Visible = True
        Me.colKDUOM_OBAT.VisibleIndex = 4
        '
        'grdKDUOM_OBAT
        '
        Me.grdKDUOM_OBAT.AutoHeight = False
        Me.grdKDUOM_OBAT.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDUOM_OBAT.Name = "grdKDUOM_OBAT"
        Me.grdKDUOM_OBAT.NullText = ""
        Me.grdKDUOM_OBAT.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdKDUOM_OBAT.View = Me.GridView4
        '
        'GridView4
        '
        Me.GridView4.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn29})
        Me.GridView4.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView4.Name = "GridView4"
        Me.GridView4.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView4.OptionsView.ShowAutoFilterRow = True
        Me.GridView4.OptionsView.ShowGroupPanel = False
        '
        'GridColumn29
        '
        Me.GridColumn29.Caption = "Memo"
        Me.GridColumn29.FieldName = "MEMO"
        Me.GridColumn29.Name = "GridColumn29"
        Me.GridColumn29.Visible = True
        Me.GridColumn29.VisibleIndex = 0
        '
        'colKDDOCTOR_OBAT
        '
        Me.colKDDOCTOR_OBAT.Caption = "Dokter"
        Me.colKDDOCTOR_OBAT.ColumnEdit = Me.grdKDDOCTOR_OBAT
        Me.colKDDOCTOR_OBAT.FieldName = "KDDOCTOR"
        Me.colKDDOCTOR_OBAT.Name = "colKDDOCTOR_OBAT"
        Me.colKDDOCTOR_OBAT.OptionsColumn.AllowEdit = False
        Me.colKDDOCTOR_OBAT.OptionsColumn.AllowFocus = False
        Me.colKDDOCTOR_OBAT.OptionsColumn.ReadOnly = True
        Me.colKDDOCTOR_OBAT.OptionsColumn.TabStop = False
        Me.colKDDOCTOR_OBAT.Visible = True
        Me.colKDDOCTOR_OBAT.VisibleIndex = 5
        '
        'grdKDDOCTOR_OBAT
        '
        Me.grdKDDOCTOR_OBAT.AutoHeight = False
        Me.grdKDDOCTOR_OBAT.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDDOCTOR_OBAT.Name = "grdKDDOCTOR_OBAT"
        Me.grdKDDOCTOR_OBAT.NullText = ""
        Me.grdKDDOCTOR_OBAT.View = Me.GridView5
        '
        'GridView5
        '
        Me.GridView5.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn31})
        Me.GridView5.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView5.Name = "GridView5"
        Me.GridView5.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView5.OptionsView.ShowGroupPanel = False
        '
        'GridColumn31
        '
        Me.GridColumn31.Caption = "Name Display"
        Me.GridColumn31.FieldName = "NAME_DISPLAY"
        Me.GridColumn31.Name = "GridColumn31"
        Me.GridColumn31.Visible = True
        Me.GridColumn31.VisibleIndex = 0
        '
        'colKDDEPARTMENT_OBAT
        '
        Me.colKDDEPARTMENT_OBAT.Caption = "Unit"
        Me.colKDDEPARTMENT_OBAT.ColumnEdit = Me.grdKDDEPARTMENT_OBAT
        Me.colKDDEPARTMENT_OBAT.FieldName = "KDDEPARTMENT"
        Me.colKDDEPARTMENT_OBAT.Name = "colKDDEPARTMENT_OBAT"
        Me.colKDDEPARTMENT_OBAT.OptionsColumn.AllowEdit = False
        Me.colKDDEPARTMENT_OBAT.OptionsColumn.AllowFocus = False
        Me.colKDDEPARTMENT_OBAT.OptionsColumn.ReadOnly = True
        Me.colKDDEPARTMENT_OBAT.OptionsColumn.TabStop = False
        Me.colKDDEPARTMENT_OBAT.Visible = True
        Me.colKDDEPARTMENT_OBAT.VisibleIndex = 6
        '
        'grdKDDEPARTMENT_OBAT
        '
        Me.grdKDDEPARTMENT_OBAT.AutoHeight = False
        Me.grdKDDEPARTMENT_OBAT.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDDEPARTMENT_OBAT.Name = "grdKDDEPARTMENT_OBAT"
        Me.grdKDDEPARTMENT_OBAT.NullText = ""
        Me.grdKDDEPARTMENT_OBAT.View = Me.GridView7
        '
        'GridView7
        '
        Me.GridView7.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn33})
        Me.GridView7.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView7.Name = "GridView7"
        Me.GridView7.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView7.OptionsView.ShowGroupPanel = False
        '
        'GridColumn33
        '
        Me.GridColumn33.Caption = "Name Display"
        Me.GridColumn33.FieldName = "NAME_DISPLAY"
        Me.GridColumn33.Name = "GridColumn33"
        Me.GridColumn33.Visible = True
        Me.GridColumn33.VisibleIndex = 0
        '
        'colPRICE_OBAT
        '
        Me.colPRICE_OBAT.Caption = "Harga"
        Me.colPRICE_OBAT.DisplayFormat.FormatString = "{0:n2}"
        Me.colPRICE_OBAT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colPRICE_OBAT.FieldName = "PRICE"
        Me.colPRICE_OBAT.Name = "colPRICE_OBAT"
        Me.colPRICE_OBAT.OptionsColumn.AllowEdit = False
        Me.colPRICE_OBAT.OptionsColumn.AllowFocus = False
        Me.colPRICE_OBAT.OptionsColumn.ReadOnly = True
        Me.colPRICE_OBAT.OptionsColumn.TabStop = False
        Me.colPRICE_OBAT.Visible = True
        Me.colPRICE_OBAT.VisibleIndex = 7
        '
        'colSUBTOTAL_OBAT
        '
        Me.colSUBTOTAL_OBAT.Caption = "Subtotal"
        Me.colSUBTOTAL_OBAT.DisplayFormat.FormatString = "{0:n2}"
        Me.colSUBTOTAL_OBAT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colSUBTOTAL_OBAT.FieldName = "SUBTOTAL"
        Me.colSUBTOTAL_OBAT.Name = "colSUBTOTAL_OBAT"
        Me.colSUBTOTAL_OBAT.OptionsColumn.AllowEdit = False
        Me.colSUBTOTAL_OBAT.OptionsColumn.AllowFocus = False
        Me.colSUBTOTAL_OBAT.OptionsColumn.ReadOnly = True
        Me.colSUBTOTAL_OBAT.OptionsColumn.TabStop = False
        Me.colSUBTOTAL_OBAT.Visible = True
        Me.colSUBTOTAL_OBAT.VisibleIndex = 8
        '
        'colDISCOUNT_OBAT
        '
        Me.colDISCOUNT_OBAT.Caption = "Diskon"
        Me.colDISCOUNT_OBAT.DisplayFormat.FormatString = "{0:n2}"
        Me.colDISCOUNT_OBAT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colDISCOUNT_OBAT.FieldName = "DISCOUNT"
        Me.colDISCOUNT_OBAT.Name = "colDISCOUNT_OBAT"
        Me.colDISCOUNT_OBAT.OptionsColumn.AllowEdit = False
        Me.colDISCOUNT_OBAT.OptionsColumn.AllowFocus = False
        Me.colDISCOUNT_OBAT.OptionsColumn.ReadOnly = True
        Me.colDISCOUNT_OBAT.OptionsColumn.TabStop = False
        Me.colDISCOUNT_OBAT.Visible = True
        Me.colDISCOUNT_OBAT.VisibleIndex = 9
        '
        'colGRANDTORAL_OBAT
        '
        Me.colGRANDTORAL_OBAT.Caption = "Grand Total"
        Me.colGRANDTORAL_OBAT.DisplayFormat.FormatString = "{0:n2}"
        Me.colGRANDTORAL_OBAT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colGRANDTORAL_OBAT.FieldName = "GRANDTOTAL"
        Me.colGRANDTORAL_OBAT.Name = "colGRANDTORAL_OBAT"
        Me.colGRANDTORAL_OBAT.OptionsColumn.AllowEdit = False
        Me.colGRANDTORAL_OBAT.OptionsColumn.AllowFocus = False
        Me.colGRANDTORAL_OBAT.OptionsColumn.ReadOnly = True
        Me.colGRANDTORAL_OBAT.OptionsColumn.TabStop = False
        Me.colGRANDTORAL_OBAT.Visible = True
        Me.colGRANDTORAL_OBAT.VisibleIndex = 10
        '
        'RepositoryItemMemoExEdit2
        '
        Me.RepositoryItemMemoExEdit2.AutoHeight = False
        Me.RepositoryItemMemoExEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemMemoExEdit2.Name = "RepositoryItemMemoExEdit2"
        '
        'RepositoryItemGridLookUpEdit5
        '
        Me.RepositoryItemGridLookUpEdit5.AutoHeight = False
        Me.RepositoryItemGridLookUpEdit5.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemGridLookUpEdit5.Name = "RepositoryItemGridLookUpEdit5"
        Me.RepositoryItemGridLookUpEdit5.NullText = ""
        Me.RepositoryItemGridLookUpEdit5.View = Me.GridView8
        '
        'GridView8
        '
        Me.GridView8.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn38})
        Me.GridView8.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView8.Name = "GridView8"
        Me.GridView8.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView8.OptionsView.ShowGroupPanel = False
        '
        'GridColumn38
        '
        Me.GridColumn38.Caption = "Memo"
        Me.GridColumn38.Name = "GridColumn38"
        Me.GridColumn38.Visible = True
        Me.GridColumn38.VisibleIndex = 0
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'tab5
        '
        Me.tab5.Controls.Add(Me.grdDetailItem)
        Me.tab5.Name = "tab5"
        Me.tab5.Size = New System.Drawing.Size(760, 257)
        Me.tab5.Text = "Informasi Rekap"
        '
        'grdDetailItem
        '
        Me.grdDetailItem.ContextMenuStrip = Me.mnuStrip
        Me.grdDetailItem.DataSource = Me.BindingSourceItem
        Me.grdDetailItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdDetailItem.Location = New System.Drawing.Point(0, 0)
        Me.grdDetailItem.MainView = Me.grvDetailItem
        Me.grdDetailItem.MenuManager = Me.barManager
        Me.grdDetailItem.Name = "grdDetailItem"
        Me.grdDetailItem.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.grdKDITEM_Item})
        Me.grdDetailItem.Size = New System.Drawing.Size(760, 257)
        Me.grdDetailItem.TabIndex = 20
        Me.grdDetailItem.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvDetailItem})
        '
        'BindingSourceItem
        '
        Me.BindingSourceItem.DataSource = GetType(DataAccess.S_SO_TRANSAKSI_D)
        '
        'grvDetailItem
        '
        Me.grvDetailItem.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colKDITEM_ITEM, Me.colQTY_ITEM})
        Me.grvDetailItem.GridControl = Me.grdDetailItem
        Me.grvDetailItem.Name = "grvDetailItem"
        Me.grvDetailItem.OptionsCustomization.AllowColumnMoving = False
        Me.grvDetailItem.OptionsCustomization.AllowFilter = False
        Me.grvDetailItem.OptionsCustomization.AllowGroup = False
        Me.grvDetailItem.OptionsCustomization.AllowQuickHideColumns = False
        Me.grvDetailItem.OptionsCustomization.AllowSort = False
        Me.grvDetailItem.OptionsDetail.EnableMasterViewMode = False
        Me.grvDetailItem.OptionsFind.AllowFindPanel = False
        Me.grvDetailItem.OptionsLayout.StoreAllOptions = True
        Me.grvDetailItem.OptionsLayout.StoreAppearance = True
        Me.grvDetailItem.OptionsMenu.EnableColumnMenu = False
        Me.grvDetailItem.OptionsNavigation.AutoFocusNewRow = True
        Me.grvDetailItem.OptionsNavigation.EnterMoveNextColumn = True
        Me.grvDetailItem.OptionsView.EnableAppearanceEvenRow = True
        Me.grvDetailItem.OptionsView.EnableAppearanceOddRow = True
        Me.grvDetailItem.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.grvDetailItem.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.grvDetailItem.OptionsView.ShowFooter = True
        Me.grvDetailItem.OptionsView.ShowGroupPanel = False
        '
        'colKDITEM_ITEM
        '
        Me.colKDITEM_ITEM.Caption = "Tarif"
        Me.colKDITEM_ITEM.ColumnEdit = Me.grdKDITEM_Item
        Me.colKDITEM_ITEM.FieldName = "KDITEM"
        Me.colKDITEM_ITEM.Name = "colKDITEM_ITEM"
        Me.colKDITEM_ITEM.OptionsColumn.AllowEdit = False
        Me.colKDITEM_ITEM.OptionsColumn.AllowFocus = False
        Me.colKDITEM_ITEM.OptionsColumn.ReadOnly = True
        Me.colKDITEM_ITEM.Visible = True
        Me.colKDITEM_ITEM.VisibleIndex = 0
        '
        'grdKDITEM_Item
        '
        Me.grdKDITEM_Item.AutoHeight = False
        Me.grdKDITEM_Item.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDITEM_Item.Name = "grdKDITEM_Item"
        Me.grdKDITEM_Item.NullText = ""
        Me.grdKDITEM_Item.View = Me.RepositoryItemGridLookUpEdit1View
        '
        'RepositoryItemGridLookUpEdit1View
        '
        Me.RepositoryItemGridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn22})
        Me.RepositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.RepositoryItemGridLookUpEdit1View.Name = "RepositoryItemGridLookUpEdit1View"
        Me.RepositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'GridColumn22
        '
        Me.GridColumn22.Caption = "Name"
        Me.GridColumn22.FieldName = "NMITEM"
        Me.GridColumn22.Name = "GridColumn22"
        Me.GridColumn22.Visible = True
        Me.GridColumn22.VisibleIndex = 0
        '
        'colQTY_ITEM
        '
        Me.colQTY_ITEM.AppearanceCell.Options.UseTextOptions = True
        Me.colQTY_ITEM.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.colQTY_ITEM.Caption = "Jumlah"
        Me.colQTY_ITEM.DisplayFormat.FormatString = "{0:n2}"
        Me.colQTY_ITEM.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colQTY_ITEM.FieldName = "QTY"
        Me.colQTY_ITEM.Name = "colQTY_ITEM"
        Me.colQTY_ITEM.OptionsColumn.AllowEdit = False
        Me.colQTY_ITEM.OptionsColumn.AllowFocus = False
        Me.colQTY_ITEM.OptionsColumn.ReadOnly = True
        Me.colQTY_ITEM.OptionsColumn.TabStop = False
        Me.colQTY_ITEM.Visible = True
        Me.colQTY_ITEM.VisibleIndex = 1
        '
        'tab6
        '
        Me.tab6.Controls.Add(Me.grdHistoryPasien)
        Me.tab6.Name = "tab6"
        Me.tab6.Size = New System.Drawing.Size(760, 257)
        Me.tab6.Text = "Mutasi Pasien"
        '
        'grdHistoryPasien
        '
        Me.grdHistoryPasien.ContextMenuStrip = Me.mnuStrip_
        Me.grdHistoryPasien.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdHistoryPasien.Location = New System.Drawing.Point(0, 0)
        Me.grdHistoryPasien.MainView = Me.grvHistoryPasien
        Me.grdHistoryPasien.Name = "grdHistoryPasien"
        Me.grdHistoryPasien.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemGridLookUpEdit1, Me.RepositoryItemGridLookUpEdit2, Me.RepositoryItemMemoExEdit3})
        Me.grdHistoryPasien.Size = New System.Drawing.Size(760, 257)
        Me.grdHistoryPasien.TabIndex = 21
        Me.grdHistoryPasien.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvHistoryPasien})
        '
        'mnuStrip_
        '
        Me.mnuStrip_.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.mnuStrip_.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MutasiPasienToolStripMenuItem, Me.EditMutasiPasienToolStripMenuItem})
        Me.mnuStrip_.Name = "mnuStrip"
        Me.mnuStrip_.Size = New System.Drawing.Size(173, 48)
        '
        'MutasiPasienToolStripMenuItem
        '
        Me.MutasiPasienToolStripMenuItem.Name = "MutasiPasienToolStripMenuItem"
        Me.MutasiPasienToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.MutasiPasienToolStripMenuItem.Text = "Add Mutasi Pasien"
        '
        'EditMutasiPasienToolStripMenuItem
        '
        Me.EditMutasiPasienToolStripMenuItem.Name = "EditMutasiPasienToolStripMenuItem"
        Me.EditMutasiPasienToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.EditMutasiPasienToolStripMenuItem.Text = "Edit Mutasi Pasien"
        '
        'grvHistoryPasien
        '
        Me.grvHistoryPasien.GridControl = Me.grdHistoryPasien
        Me.grvHistoryPasien.Name = "grvHistoryPasien"
        Me.grvHistoryPasien.OptionsBehavior.Editable = False
        Me.grvHistoryPasien.OptionsCustomization.AllowColumnMoving = False
        Me.grvHistoryPasien.OptionsCustomization.AllowFilter = False
        Me.grvHistoryPasien.OptionsCustomization.AllowGroup = False
        Me.grvHistoryPasien.OptionsCustomization.AllowQuickHideColumns = False
        Me.grvHistoryPasien.OptionsCustomization.AllowSort = False
        Me.grvHistoryPasien.OptionsDetail.EnableMasterViewMode = False
        Me.grvHistoryPasien.OptionsFind.AllowFindPanel = False
        Me.grvHistoryPasien.OptionsLayout.StoreAllOptions = True
        Me.grvHistoryPasien.OptionsLayout.StoreAppearance = True
        Me.grvHistoryPasien.OptionsMenu.EnableColumnMenu = False
        Me.grvHistoryPasien.OptionsNavigation.AutoFocusNewRow = True
        Me.grvHistoryPasien.OptionsNavigation.EnterMoveNextColumn = True
        Me.grvHistoryPasien.OptionsView.EnableAppearanceEvenRow = True
        Me.grvHistoryPasien.OptionsView.EnableAppearanceOddRow = True
        Me.grvHistoryPasien.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.grvHistoryPasien.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.grvHistoryPasien.OptionsView.ShowFooter = True
        Me.grvHistoryPasien.OptionsView.ShowGroupPanel = False
        '
        'RepositoryItemGridLookUpEdit1
        '
        Me.RepositoryItemGridLookUpEdit1.AutoHeight = False
        Me.RepositoryItemGridLookUpEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemGridLookUpEdit1.Name = "RepositoryItemGridLookUpEdit1"
        Me.RepositoryItemGridLookUpEdit1.NullText = ""
        Me.RepositoryItemGridLookUpEdit1.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.RepositoryItemGridLookUpEdit1.View = Me.GridView9
        '
        'GridView9
        '
        Me.GridView9.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn23, Me.GridColumn24, Me.GridColumn27})
        Me.GridView9.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView9.Name = "GridView9"
        Me.GridView9.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView9.OptionsView.ShowAutoFilterRow = True
        Me.GridView9.OptionsView.ShowGroupPanel = False
        '
        'GridColumn23
        '
        Me.GridColumn23.Caption = "Item Name #1"
        Me.GridColumn23.FieldName = "NMITEM1"
        Me.GridColumn23.Name = "GridColumn23"
        Me.GridColumn23.Visible = True
        Me.GridColumn23.VisibleIndex = 0
        '
        'GridColumn24
        '
        Me.GridColumn24.Caption = "Item Name #2"
        Me.GridColumn24.FieldName = "NMITEM2"
        Me.GridColumn24.Name = "GridColumn24"
        Me.GridColumn24.Visible = True
        Me.GridColumn24.VisibleIndex = 1
        '
        'GridColumn27
        '
        Me.GridColumn27.Caption = "Item Name #3"
        Me.GridColumn27.FieldName = "NMITEM3"
        Me.GridColumn27.Name = "GridColumn27"
        Me.GridColumn27.Visible = True
        Me.GridColumn27.VisibleIndex = 2
        '
        'RepositoryItemGridLookUpEdit2
        '
        Me.RepositoryItemGridLookUpEdit2.AutoHeight = False
        Me.RepositoryItemGridLookUpEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemGridLookUpEdit2.Name = "RepositoryItemGridLookUpEdit2"
        Me.RepositoryItemGridLookUpEdit2.NullText = ""
        Me.RepositoryItemGridLookUpEdit2.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.RepositoryItemGridLookUpEdit2.View = Me.GridView2
        '
        'GridView2
        '
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn28})
        Me.GridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView2.OptionsView.ShowAutoFilterRow = True
        Me.GridView2.OptionsView.ShowGroupPanel = False
        '
        'GridColumn28
        '
        Me.GridColumn28.Caption = "Description"
        Me.GridColumn28.FieldName = "MEMO"
        Me.GridColumn28.Name = "GridColumn28"
        Me.GridColumn28.Visible = True
        Me.GridColumn28.VisibleIndex = 0
        '
        'RepositoryItemMemoExEdit3
        '
        Me.RepositoryItemMemoExEdit3.AutoHeight = False
        Me.RepositoryItemMemoExEdit3.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemMemoExEdit3.Name = "RepositoryItemMemoExEdit3"
        '
        'tab3
        '
        Me.tab3.Controls.Add(Me.LayoutControl1)
        Me.tab3.Name = "tab3"
        Me.tab3.Size = New System.Drawing.Size(760, 257)
        Me.tab3.Text = "Memo Information"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.txtMEMO)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.s
        Me.LayoutControl1.Size = New System.Drawing.Size(760, 257)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'txtMEMO
        '
        Me.txtMEMO.EnterMoveNextControl = True
        Me.txtMEMO.Location = New System.Drawing.Point(12, 12)
        Me.txtMEMO.MenuManager = Me.barManager
        Me.txtMEMO.Name = "txtMEMO"
        Me.txtMEMO.Size = New System.Drawing.Size(736, 233)
        Me.txtMEMO.StyleController = Me.LayoutControl1
        Me.txtMEMO.TabIndex = 4
        '
        's
        '
        Me.s.CustomizationFormText = "s"
        Me.s.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.s.GroupBordersVisible = False
        Me.s.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem7})
        Me.s.Location = New System.Drawing.Point(0, 0)
        Me.s.Name = "s"
        Me.s.Size = New System.Drawing.Size(760, 257)
        Me.s.TextVisible = False
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.txtMEMO
        Me.LayoutControlItem7.CustomizationFormText = "LayoutControlItem7"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(740, 237)
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem7.TextVisible = False
        '
        'txtKDCASHIN
        '
        Me.txtKDCASHIN.EditValue = ""
        Me.txtKDCASHIN.EnterMoveNextControl = True
        Me.txtKDCASHIN.Location = New System.Drawing.Point(167, 12)
        Me.txtKDCASHIN.Name = "txtKDCASHIN"
        Me.txtKDCASHIN.Properties.ReadOnly = True
        Me.txtKDCASHIN.Size = New System.Drawing.Size(242, 20)
        Me.txtKDCASHIN.StyleController = Me.layoutControl
        Me.txtKDCASHIN.TabIndex = 9
        Me.txtKDCASHIN.TabStop = False
        '
        'txtNAMAPASIEN
        '
        Me.txtNAMAPASIEN.Location = New System.Drawing.Point(568, 12)
        Me.txtNAMAPASIEN.MenuManager = Me.barManager
        Me.txtNAMAPASIEN.Name = "txtNAMAPASIEN"
        Me.txtNAMAPASIEN.Properties.ReadOnly = True
        Me.txtNAMAPASIEN.Size = New System.Drawing.Size(210, 20)
        Me.txtNAMAPASIEN.StyleController = Me.layoutControl
        Me.txtNAMAPASIEN.TabIndex = 50
        '
        'grdKDDEPARTMENT_H
        '
        Me.grdKDDEPARTMENT_H.Location = New System.Drawing.Point(568, 36)
        Me.grdKDDEPARTMENT_H.MenuManager = Me.barManager
        Me.grdKDDEPARTMENT_H.Name = "grdKDDEPARTMENT_H"
        Me.grdKDDEPARTMENT_H.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDDEPARTMENT_H.Properties.NullText = ""
        Me.grdKDDEPARTMENT_H.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdKDDEPARTMENT_H.Properties.ReadOnly = True
        Me.grdKDDEPARTMENT_H.Properties.View = Me.SearchLookUpEdit1View
        Me.grdKDDEPARTMENT_H.Size = New System.Drawing.Size(210, 20)
        Me.grdKDDEPARTMENT_H.StyleController = Me.layoutControl
        Me.grdKDDEPARTMENT_H.TabIndex = 49
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn20})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'GridColumn20
        '
        Me.GridColumn20.Caption = "Name Display"
        Me.GridColumn20.FieldName = "NAME_DISPLAY"
        Me.GridColumn20.Name = "GridColumn20"
        Me.GridColumn20.Visible = True
        Me.GridColumn20.VisibleIndex = 0
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lKDCASH, Me.LayoutControlItem5, Me.lDATE, Me.lSUBTOTAL, Me.lADMIN, Me.lROUND, Me.lGRANDTOTAL, Me.lCARI, Me.LayoutControlItem2, Me.lKDPENDAFTARAN, Me.lNAMAPASIEN, Me.lKDDEPARTMENT, Me.lKDDOCTOR, Me.lKDPAYMENTTYPE, Me.lCOSTSHARING, Me.lDEPOSIT, Me.lKDPENDAFTARAN_AWAL, Me.EmptySpaceItem1, Me.LayoutControlItem1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(790, 549)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'lKDCASH
        '
        Me.lKDCASH.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKDCASH.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKDCASH.Control = Me.txtKDCASHIN
        Me.lKDCASH.CustomizationFormText = "Display Name * :"
        Me.lKDCASH.Location = New System.Drawing.Point(0, 0)
        Me.lKDCASH.Name = "lKDCASH"
        Me.lKDCASH.Size = New System.Drawing.Size(401, 24)
        Me.lKDCASH.Text = "Number * :"
        Me.lKDCASH.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKDCASH.TextSize = New System.Drawing.Size(150, 20)
        Me.lKDCASH.TextToControlDistance = 5
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.tabControl
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 96)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(770, 289)
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextVisible = False
        '
        'lDATE
        '
        Me.lDATE.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lDATE.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lDATE.Control = Me.deDATE
        Me.lDATE.CustomizationFormText = "Date :"
        Me.lDATE.Location = New System.Drawing.Point(0, 24)
        Me.lDATE.Name = "lDATE"
        Me.lDATE.Size = New System.Drawing.Size(401, 24)
        Me.lDATE.Text = "Date :"
        Me.lDATE.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lDATE.TextSize = New System.Drawing.Size(150, 20)
        Me.lDATE.TextToControlDistance = 5
        '
        'lSUBTOTAL
        '
        Me.lSUBTOTAL.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lSUBTOTAL.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lSUBTOTAL.Control = Me.txtSUBTOTAL
        Me.lSUBTOTAL.CustomizationFormText = "Sub Total :"
        Me.lSUBTOTAL.Location = New System.Drawing.Point(446, 385)
        Me.lSUBTOTAL.Name = "lSUBTOTAL"
        Me.lSUBTOTAL.Size = New System.Drawing.Size(324, 24)
        Me.lSUBTOTAL.Text = "Sub Total :"
        Me.lSUBTOTAL.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lSUBTOTAL.TextSize = New System.Drawing.Size(100, 20)
        Me.lSUBTOTAL.TextToControlDistance = 5
        '
        'lADMIN
        '
        Me.lADMIN.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lADMIN.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lADMIN.Control = Me.txtADMIN
        Me.lADMIN.CustomizationFormText = "Admin :"
        Me.lADMIN.Location = New System.Drawing.Point(446, 457)
        Me.lADMIN.Name = "lADMIN"
        Me.lADMIN.Size = New System.Drawing.Size(324, 24)
        Me.lADMIN.Text = "Admin :"
        Me.lADMIN.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lADMIN.TextSize = New System.Drawing.Size(100, 20)
        Me.lADMIN.TextToControlDistance = 5
        '
        'lROUND
        '
        Me.lROUND.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lROUND.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lROUND.Control = Me.txtROUND
        Me.lROUND.CustomizationFormText = "Round :"
        Me.lROUND.Location = New System.Drawing.Point(446, 481)
        Me.lROUND.Name = "lROUND"
        Me.lROUND.Size = New System.Drawing.Size(324, 24)
        Me.lROUND.Text = "Round :"
        Me.lROUND.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lROUND.TextSize = New System.Drawing.Size(100, 20)
        Me.lROUND.TextToControlDistance = 5
        Me.lROUND.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'lGRANDTOTAL
        '
        Me.lGRANDTOTAL.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lGRANDTOTAL.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lGRANDTOTAL.Control = Me.txtGRANDTOTAL
        Me.lGRANDTOTAL.CustomizationFormText = "Grand Total :"
        Me.lGRANDTOTAL.Location = New System.Drawing.Point(446, 505)
        Me.lGRANDTOTAL.Name = "lGRANDTOTAL"
        Me.lGRANDTOTAL.Size = New System.Drawing.Size(324, 24)
        Me.lGRANDTOTAL.Text = "Grand Total :"
        Me.lGRANDTOTAL.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lGRANDTOTAL.TextSize = New System.Drawing.Size(100, 20)
        Me.lGRANDTOTAL.TextToControlDistance = 5
        '
        'lCARI
        '
        Me.lCARI.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lCARI.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lCARI.Control = Me.cboCARI
        Me.lCARI.Location = New System.Drawing.Point(0, 48)
        Me.lCARI.Name = "lCARI"
        Me.lCARI.Size = New System.Drawing.Size(277, 24)
        Me.lCARI.Text = "Cari :"
        Me.lCARI.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lCARI.TextSize = New System.Drawing.Size(150, 20)
        Me.lCARI.TextToControlDistance = 5
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.txtCARI
        Me.LayoutControlItem2.Location = New System.Drawing.Point(277, 48)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(124, 24)
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'lKDPENDAFTARAN
        '
        Me.lKDPENDAFTARAN.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKDPENDAFTARAN.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKDPENDAFTARAN.Control = Me.grdKDPENDAFTARAN
        Me.lKDPENDAFTARAN.Location = New System.Drawing.Point(0, 72)
        Me.lKDPENDAFTARAN.Name = "lKDPENDAFTARAN"
        Me.lKDPENDAFTARAN.Size = New System.Drawing.Size(277, 24)
        Me.lKDPENDAFTARAN.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKDPENDAFTARAN.TextSize = New System.Drawing.Size(150, 20)
        Me.lKDPENDAFTARAN.TextToControlDistance = 5
        '
        'lNAMAPASIEN
        '
        Me.lNAMAPASIEN.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lNAMAPASIEN.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lNAMAPASIEN.Control = Me.txtNAMAPASIEN
        Me.lNAMAPASIEN.Location = New System.Drawing.Point(401, 0)
        Me.lNAMAPASIEN.Name = "lNAMAPASIEN"
        Me.lNAMAPASIEN.Size = New System.Drawing.Size(369, 24)
        Me.lNAMAPASIEN.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lNAMAPASIEN.TextSize = New System.Drawing.Size(150, 20)
        Me.lNAMAPASIEN.TextToControlDistance = 5
        '
        'lKDDEPARTMENT
        '
        Me.lKDDEPARTMENT.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKDDEPARTMENT.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKDDEPARTMENT.Control = Me.grdKDDEPARTMENT_H
        Me.lKDDEPARTMENT.Location = New System.Drawing.Point(401, 24)
        Me.lKDDEPARTMENT.Name = "lKDDEPARTMENT"
        Me.lKDDEPARTMENT.Size = New System.Drawing.Size(369, 24)
        Me.lKDDEPARTMENT.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKDDEPARTMENT.TextSize = New System.Drawing.Size(150, 20)
        Me.lKDDEPARTMENT.TextToControlDistance = 5
        '
        'lKDDOCTOR
        '
        Me.lKDDOCTOR.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKDDOCTOR.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKDDOCTOR.Control = Me.grdKDDOCTOR_H
        Me.lKDDOCTOR.Location = New System.Drawing.Point(401, 48)
        Me.lKDDOCTOR.Name = "lKDDOCTOR"
        Me.lKDDOCTOR.Size = New System.Drawing.Size(369, 24)
        Me.lKDDOCTOR.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKDDOCTOR.TextSize = New System.Drawing.Size(150, 20)
        Me.lKDDOCTOR.TextToControlDistance = 5
        '
        'lKDPAYMENTTYPE
        '
        Me.lKDPAYMENTTYPE.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKDPAYMENTTYPE.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKDPAYMENTTYPE.Control = Me.grdKDPAYMENTTYPE
        Me.lKDPAYMENTTYPE.CustomizationFormText = "Warehouse * :"
        Me.lKDPAYMENTTYPE.Location = New System.Drawing.Point(401, 72)
        Me.lKDPAYMENTTYPE.Name = "lKDPAYMENTTYPE"
        Me.lKDPAYMENTTYPE.Size = New System.Drawing.Size(369, 24)
        Me.lKDPAYMENTTYPE.Text = "Payment Type * :"
        Me.lKDPAYMENTTYPE.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKDPAYMENTTYPE.TextSize = New System.Drawing.Size(150, 20)
        Me.lKDPAYMENTTYPE.TextToControlDistance = 5
        '
        'lCOSTSHARING
        '
        Me.lCOSTSHARING.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lCOSTSHARING.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lCOSTSHARING.Control = Me.txtCOSTSHARING
        Me.lCOSTSHARING.Location = New System.Drawing.Point(446, 409)
        Me.lCOSTSHARING.Name = "lCOSTSHARING"
        Me.lCOSTSHARING.Size = New System.Drawing.Size(324, 24)
        Me.lCOSTSHARING.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lCOSTSHARING.TextSize = New System.Drawing.Size(100, 20)
        Me.lCOSTSHARING.TextToControlDistance = 5
        '
        'lDEPOSIT
        '
        Me.lDEPOSIT.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lDEPOSIT.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lDEPOSIT.Control = Me.txtDEPOSIT
        Me.lDEPOSIT.Location = New System.Drawing.Point(446, 433)
        Me.lDEPOSIT.Name = "lDEPOSIT"
        Me.lDEPOSIT.Size = New System.Drawing.Size(324, 24)
        Me.lDEPOSIT.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lDEPOSIT.TextSize = New System.Drawing.Size(100, 20)
        Me.lDEPOSIT.TextToControlDistance = 5
        '
        'lKDPENDAFTARAN_AWAL
        '
        Me.lKDPENDAFTARAN_AWAL.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKDPENDAFTARAN_AWAL.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKDPENDAFTARAN_AWAL.Control = Me.txtKDPENDAFTARAN_AWAL
        Me.lKDPENDAFTARAN_AWAL.Location = New System.Drawing.Point(277, 72)
        Me.lKDPENDAFTARAN_AWAL.Name = "lKDPENDAFTARAN_AWAL"
        Me.lKDPENDAFTARAN_AWAL.Size = New System.Drawing.Size(124, 24)
        Me.lKDPENDAFTARAN_AWAL.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKDPENDAFTARAN_AWAL.TextSize = New System.Drawing.Size(0, 0)
        Me.lKDPENDAFTARAN_AWAL.TextToControlDistance = 0
        Me.lKDPENDAFTARAN_AWAL.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 457)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(446, 72)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Number"
        Me.GridColumn6.FieldName = "KDSO"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 0
        '
        'cboSHIFT
        '
        Me.cboSHIFT.Location = New System.Drawing.Point(117, 397)
        Me.cboSHIFT.MenuManager = Me.barManager
        Me.cboSHIFT.Name = "cboSHIFT"
        Me.cboSHIFT.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboSHIFT.Properties.Items.AddRange(New Object() {"PAGI", "SIANG", "SORE"})
        Me.cboSHIFT.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cboSHIFT.Size = New System.Drawing.Size(337, 20)
        Me.cboSHIFT.StyleController = Me.layoutControl
        Me.cboSHIFT.TabIndex = 52
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem1.Control = Me.cboSHIFT
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 385)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(446, 72)
        Me.LayoutControlItem1.Text = "Kasir :"
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(100, 20)
        Me.LayoutControlItem1.TextToControlDistance = 5
        '
        'frmCashIn
        '
        Me.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(790, 571)
        Me.Controls.Add(Me.layoutControl)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.Name = "frmCashIn"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.layoutControl.ResumeLayout(False)
        CType(Me.txtKDPENDAFTARAN_AWAL.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCOSTSHARING.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDEPOSIT.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDDOCTOR_H.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDPENDAFTARAN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCARI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboCARI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGRANDTOTAL.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtROUND.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtADMIN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSUBTOTAL.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDPAYMENTTYPE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvKDPAYMENTTYPE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATE.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tabControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabControl.ResumeLayout(False)
        Me.tab1.ResumeLayout(False)
        CType(Me.grdDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuStrip.ResumeLayout(False)
        CType(Me.bindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtREMARKS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdNOINVOICE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvNOINVOICE, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tab7.ResumeLayout(False)
        CType(Me.grdDetail_R, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.bindingSource_R, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvDetail_R, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtREMARKS_R, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemGridLookUpEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdNOINVOICE_R, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvNOINVOICE_R, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tab2.ResumeLayout(False)
        CType(Me.grdDetailRincian, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingSourceRincian, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvDeatilRincian, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDateCretaed.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDateCretaed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDITEM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvKDITEM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDUOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvKDUOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDDOCTOR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvKDDOCTOR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDDEPARTMENT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvKDDEPARTMENT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemMemoExEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDSIGNA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvKDSIGNA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkISRACIK, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tab4.ResumeLayout(False)
        CType(Me.grdDetailObat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingSourceObat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvDetailObat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit1.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDITEM_OBAT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDUOM_OBAT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDDOCTOR_OBAT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDDEPARTMENT_OBAT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemMemoExEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemGridLookUpEdit5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tab5.ResumeLayout(False)
        CType(Me.grdDetailItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingSourceItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvDetailItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDITEM_Item, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tab6.ResumeLayout(False)
        CType(Me.grdHistoryPasien, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuStrip_.ResumeLayout(False)
        CType(Me.grvHistoryPasien, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemGridLookUpEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemGridLookUpEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemMemoExEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tab3.ResumeLayout(False)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.txtMEMO.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.s, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtKDCASHIN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNAMAPASIEN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDDEPARTMENT_H.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKDCASH, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lDATE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lSUBTOTAL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lADMIN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lROUND, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lGRANDTOTAL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lCARI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKDPENDAFTARAN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lNAMAPASIEN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKDDEPARTMENT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKDDOCTOR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKDPAYMENTTYPE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lCOSTSHARING, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lDEPOSIT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKDPENDAFTARAN_AWAL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboSHIFT.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents layoutControl As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents lKDCASH As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents barManager As DevExpress.XtraBars.BarManager
    Friend WithEvents barTop As DevExpress.XtraBars.Bar
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents btnSaveNew As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnClose As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnSaveClose As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents progressBarSave As DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar
    Friend WithEvents progressSave As DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar
    Friend WithEvents txtKDCASHIN As DevExpress.XtraEditors.TextEdit
    Friend WithEvents bindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents tabControl As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents tab1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grdDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents grvDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents deDATE As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lDATE As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents tab3 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents s As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents txtMEMO As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents colAMOUNTPAYMENT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colREMARKS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdNOINVOICE As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents grvNOINVOICE As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mnuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents colNOINVOICE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents txtREMARKS As DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdKDPAYMENTTYPE As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents grvKDPAYMENTTYPE As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lKDPAYMENTTYPE As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents colAMOUNTORIGINAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colAMOUNTDUE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents txtGRANDTOTAL As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtROUND As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtADMIN As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtSUBTOTAL As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lSUBTOTAL As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lADMIN As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lROUND As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lGRANDTOTAL As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents txtCARI As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cboCARI As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents lCARI As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grdKDPENDAFTARAN As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn17 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn18 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lKDPENDAFTARAN As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grdKDDOCTOR_H As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView6 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn19 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents txtNAMAPASIEN As DevExpress.XtraEditors.TextEdit
    Friend WithEvents grdKDDEPARTMENT_H As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn20 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lNAMAPASIEN As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lKDDEPARTMENT As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lKDDOCTOR As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtCOSTSHARING As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtDEPOSIT As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lCOSTSHARING As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lDEPOSIT As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtKDPENDAFTARAN_AWAL As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lKDPENDAFTARAN_AWAL As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents tab2 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents grdDetailRincian As DevExpress.XtraGrid.GridControl
    Friend WithEvents BindingSourceRincian As BindingSource
    Friend WithEvents grvDeatilRincian As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colDATECREATED As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents deDateCretaed As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents colKDITEM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdKDITEM As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents grvKDITEM As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colSTOK As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colQTY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents chkISRACIK As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents colKDUOM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdKDUOM As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents grvKDUOM As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colKDDOCTOR As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdKDDOCTOR As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents grvKDDOCTOR As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colKDDEPARTMENT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdKDDEPARTMENT As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents grvKDDEPARTMENT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn21 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdKDSIGNA As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents grvKDSIGNA As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents MEMO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPRICE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colSUBTOTAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDISCOUNT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colGRANDTOTAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemMemoExEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit
    Friend WithEvents colKDSOTRANSAKSI As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents tab4 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents grdDetailObat As DevExpress.XtraGrid.GridControl
    Friend WithEvents BindingSourceObat As BindingSource
    Friend WithEvents grvDetailObat As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colKDSOTRANSKASI_OBAT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDATECREATED_OBAT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemDateEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents colKDITEM_OBAT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdKDITEM_OBAT As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn25 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn26 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colQTY_OBAT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colKDUOM_OBAT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdKDUOM_OBAT As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents GridView4 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn29 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colKDDOCTOR_OBAT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdKDDOCTOR_OBAT As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents GridView5 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn31 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colKDDEPARTMENT_OBAT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdKDDEPARTMENT_OBAT As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents GridView7 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn33 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPRICE_OBAT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colSUBTOTAL_OBAT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDISCOUNT_OBAT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colGRANDTORAL_OBAT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemMemoExEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit
    Friend WithEvents RepositoryItemGridLookUpEdit5 As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents GridView8 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn38 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents btnPasienPulang As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents tab5 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents grdDetailItem As DevExpress.XtraGrid.GridControl
    Friend WithEvents grvDetailItem As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colKDITEM_ITEM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdKDITEM_Item As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents RepositoryItemGridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn22 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colQTY_ITEM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BindingSourceItem As BindingSource
    Friend WithEvents colCATEGORYI As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents tab6 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents grdHistoryPasien As DevExpress.XtraGrid.GridControl
    Friend WithEvents grvHistoryPasien As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemGridLookUpEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents GridView9 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn23 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn24 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn27 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemGridLookUpEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn28 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemMemoExEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit
    Friend WithEvents mnuStrip_ As ContextMenuStrip
    Friend WithEvents MutasiPasienToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EditMutasiPasienToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents tab7 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents grdDetail_R As DevExpress.XtraGrid.GridControl
    Friend WithEvents grvDetail_R As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grdNOINVOICE_R As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents grvNOINVOICE_R As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn30 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn32 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn34 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn35 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colAMOUNTORIGINAL_UB_R As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colAMOUNTDUE_UB_R As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colAMOUNTPAYMENT_R As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colREMARKS_R As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents txtREMARKS_R As DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit
    Friend WithEvents RepositoryItemGridLookUpEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents GridView10 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn36 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn37 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn39 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents bindingSource_R As BindingSource
    Friend WithEvents colNOINVOICE_R As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cboSHIFT As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
End Class
