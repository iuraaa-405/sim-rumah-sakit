<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmHandoverAlkes
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.layoutControl = New DevExpress.XtraLayout.LayoutControl()
        Me.grdCARI = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.barManager = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barTop = New DevExpress.XtraBars.Bar()
        Me.btnSaveClose = New DevExpress.XtraBars.BarButtonItem()
        Me.btnClose = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.btnSaveNew = New DevExpress.XtraBars.BarButtonItem()
        Me.btnFocus = New DevExpress.XtraBars.BarButtonItem()
        Me.progressBarSave = New DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar()
        Me.progressSave = New DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar()
        Me.grvCARI = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.txtRM = New DevExpress.XtraEditors.TextEdit()
        Me.btnCariPasien = New DevExpress.XtraEditors.SimpleButton()
        Me.grdPOLIRUANGAN = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.grvKDPOLIRUANGAN = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.deDATE = New DevExpress.XtraEditors.DateEdit()
        Me.tabControl = New DevExpress.XtraTab.XtraTabControl()
        Me.tab1 = New DevExpress.XtraTab.XtraTabPage()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.grdDetail = New DevExpress.XtraGrid.GridControl()
        Me.mnuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.grvDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDATECREATED = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDATEUPDATED = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colKDKUNJUNGAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colNAME_DISPLAY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colSTETOSKOP_ADA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colSTETOSKOP_BERFUNGSI = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colSPHYGMOMANOMETER_ADA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colSPHYGMOMANOMETER_BERFUNGSI = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTERMOMETER_ADA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTERMOMETER_BERFUNGSI = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colOXYMETER_ADA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colOXYMETER_BERFUNGSI = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colEKG_ADA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colEKG_BERFUNGSI = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colUSER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colKDRUANGAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colNAMARUANGAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colSESI = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtREMARKS = New DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit()
        Me.grdMACAMDIET = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.grvMACAMDIET = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.chkISBANYAK = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.chkISBIASA = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.chkISTKTP = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.chkISRG = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.chkISRL = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.chkISRS = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.chkISRPROT = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.chkISRP = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.chkISDM = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.chkISRK = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.chkISRKAL = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.chkISPUASA = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lDATE = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.layoutControl,System.ComponentModel.ISupportInitialize).BeginInit
        Me.layoutControl.SuspendLayout
        CType(Me.grdCARI.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.barManager,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.progressBarSave,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.progressSave,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.grvCARI,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtRM.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.grdPOLIRUANGAN.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.grvKDPOLIRUANGAN,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.deDATE.Properties.CalendarTimeProperties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.deDATE.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.tabControl,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tabControl.SuspendLayout
        Me.tab1.SuspendLayout
        CType(Me.LayoutControl1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.LayoutControl1.SuspendLayout
        CType(Me.grdDetail,System.ComponentModel.ISupportInitialize).BeginInit
        Me.mnuStrip.SuspendLayout
        CType(Me.BindingSource1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.grvDetail,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtREMARKS,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.grdMACAMDIET,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.grvMACAMDIET,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.chkISBANYAK,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.chkISBIASA,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.chkISTKTP,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.chkISRG,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.chkISRL,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.chkISRS,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.chkISRPROT,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.chkISRP,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.chkISDM,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.chkISRK,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.chkISRKAL,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.chkISPUASA,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup2,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem6,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem5,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.lDATE,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem10,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.EmptySpaceItem1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem2,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem3,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'layoutControl
        '
        Me.layoutControl.Controls.Add(Me.grdCARI)
        Me.layoutControl.Controls.Add(Me.txtRM)
        Me.layoutControl.Controls.Add(Me.btnCariPasien)
        Me.layoutControl.Controls.Add(Me.grdPOLIRUANGAN)
        Me.layoutControl.Controls.Add(Me.deDATE)
        Me.layoutControl.Controls.Add(Me.tabControl)
        Me.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutControl.Location = New System.Drawing.Point(0, 0)
        Me.layoutControl.Name = "layoutControl"
        Me.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(652, 156, 250, 350)
        Me.layoutControl.Root = Me.LayoutControlGroup1
        Me.layoutControl.Size = New System.Drawing.Size(760, 585)
        Me.layoutControl.TabIndex = 0
        Me.layoutControl.Text = "LayoutControl1"
        '
        'grdCARI
        '
        Me.grdCARI.EditValue = ""
        Me.grdCARI.Location = New System.Drawing.Point(511, 38)
        Me.grdCARI.MenuManager = Me.barManager
        Me.grdCARI.Name = "grdCARI"
        Me.grdCARI.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdCARI.Properties.NullText = ""
        Me.grdCARI.Properties.PopupFormSize = New System.Drawing.Size(700, 300)
        Me.grdCARI.Properties.View = Me.grvCARI
        Me.grdCARI.Size = New System.Drawing.Size(237, 20)
        Me.grdCARI.StyleController = Me.layoutControl
        Me.grdCARI.TabIndex = 34
        '
        'barManager
        '
        Me.barManager.AllowQuickCustomization = false
        Me.barManager.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.barTop})
        Me.barManager.DockControls.Add(Me.barDockControlTop)
        Me.barManager.DockControls.Add(Me.barDockControlBottom)
        Me.barManager.DockControls.Add(Me.barDockControlLeft)
        Me.barManager.DockControls.Add(Me.barDockControlRight)
        Me.barManager.Form = Me
        Me.barManager.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.btnSaveNew, Me.btnClose, Me.btnSaveClose, Me.btnFocus})
        Me.barManager.MainMenu = Me.barTop
        Me.barManager.MaxItemId = 9
        Me.barManager.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.progressBarSave, Me.progressSave})
        '
        'barTop
        '
        Me.barTop.BarName = "Main menu"
        Me.barTop.DockCol = 0
        Me.barTop.DockRow = 0
        Me.barTop.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom
        Me.barTop.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.btnSaveClose), New DevExpress.XtraBars.LinkPersistInfo(Me.btnClose)})
        Me.barTop.OptionsBar.DrawDragBorder = false
        Me.barTop.OptionsBar.MultiLine = true
        Me.barTop.OptionsBar.UseWholeRow = true
        Me.barTop.Text = "Main menu"
        '
        'btnSaveClose
        '
        Me.btnSaveClose.Caption = "F3 - Save && Close"
        Me.btnSaveClose.Id = 5
        Me.btnSaveClose.Name = "btnSaveClose"
        '
        'btnClose
        '
        Me.btnClose.Caption = "F12 - Close"
        Me.btnClose.Id = 3
        Me.btnClose.Name = "btnClose"
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = false
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(760, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = false
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 585)
        Me.barDockControlBottom.Size = New System.Drawing.Size(760, 22)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = false
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 585)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = false
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(760, 0)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 585)
        '
        'btnSaveNew
        '
        Me.btnSaveNew.Caption = "F2 - Save && New"
        Me.btnSaveNew.Id = 2
        Me.btnSaveNew.Name = "btnSaveNew"
        '
        'btnFocus
        '
        Me.btnFocus.Caption = "F6 - Focus Cari Obat"
        Me.btnFocus.Id = 8
        Me.btnFocus.Name = "btnFocus"
        '
        'progressBarSave
        '
        Me.progressBarSave.Name = "progressBarSave"
        Me.progressBarSave.Stopped = true
        '
        'progressSave
        '
        Me.progressSave.Name = "progressSave"
        Me.progressSave.Paused = true
        '
        'grvCARI
        '
        Me.grvCARI.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvCARI.Name = "grvCARI"
        Me.grvCARI.OptionsSelection.EnableAppearanceFocusedCell = false
        Me.grvCARI.OptionsView.ShowAutoFilterRow = true
        Me.grvCARI.OptionsView.ShowGroupPanel = false
        '
        'txtRM
        '
        Me.txtRM.Location = New System.Drawing.Point(511, 12)
        Me.txtRM.MenuManager = Me.barManager
        Me.txtRM.Name = "txtRM"
        Me.txtRM.Size = New System.Drawing.Size(106, 20)
        Me.txtRM.StyleController = Me.layoutControl
        Me.txtRM.TabIndex = 33
        '
        'btnCariPasien
        '
        Me.btnCariPasien.Location = New System.Drawing.Point(621, 12)
        Me.btnCariPasien.Name = "btnCariPasien"
        Me.btnCariPasien.Size = New System.Drawing.Size(127, 22)
        Me.btnCariPasien.StyleController = Me.layoutControl
        Me.btnCariPasien.TabIndex = 32
        Me.btnCariPasien.Text = "Cari Data Pasien"
        '
        'grdPOLIRUANGAN
        '
        Me.grdPOLIRUANGAN.EditValue = ""
        Me.grdPOLIRUANGAN.EnterMoveNextControl = true
        Me.grdPOLIRUANGAN.Location = New System.Drawing.Point(147, 36)
        Me.grdPOLIRUANGAN.MenuManager = Me.barManager
        Me.grdPOLIRUANGAN.Name = "grdPOLIRUANGAN"
        Me.grdPOLIRUANGAN.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdPOLIRUANGAN.Properties.NullText = ""
        Me.grdPOLIRUANGAN.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdPOLIRUANGAN.Properties.ReadOnly = true
        Me.grdPOLIRUANGAN.Properties.View = Me.grvKDPOLIRUANGAN
        Me.grdPOLIRUANGAN.Size = New System.Drawing.Size(116, 20)
        Me.grdPOLIRUANGAN.StyleController = Me.layoutControl
        Me.grdPOLIRUANGAN.TabIndex = 23
        '
        'grvKDPOLIRUANGAN
        '
        Me.grvKDPOLIRUANGAN.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn8})
        Me.grvKDPOLIRUANGAN.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvKDPOLIRUANGAN.Name = "grvKDPOLIRUANGAN"
        Me.grvKDPOLIRUANGAN.OptionsSelection.EnableAppearanceFocusedCell = false
        Me.grvKDPOLIRUANGAN.OptionsView.ShowAutoFilterRow = true
        Me.grvKDPOLIRUANGAN.OptionsView.ShowGroupPanel = false
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Tampilan Nama"
        Me.GridColumn8.FieldName = "NAME_DISPLAY"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Visible = true
        Me.GridColumn8.VisibleIndex = 0
        '
        'deDATE
        '
        Me.deDATE.EditValue = Nothing
        Me.deDATE.EnterMoveNextControl = true
        Me.deDATE.Location = New System.Drawing.Point(147, 12)
        Me.deDATE.MenuManager = Me.barManager
        Me.deDATE.Name = "deDATE"
        Me.deDATE.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deDATE.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.deDATE.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.deDATE.Properties.Mask.UseMaskAsDisplayFormat = true
        Me.deDATE.Size = New System.Drawing.Size(116, 20)
        Me.deDATE.StyleController = Me.layoutControl
        Me.deDATE.TabIndex = 20
        '
        'tabControl
        '
        Me.tabControl.Location = New System.Drawing.Point(12, 62)
        Me.tabControl.Name = "tabControl"
        Me.tabControl.SelectedTabPage = Me.tab1
        Me.tabControl.Size = New System.Drawing.Size(736, 511)
        Me.tabControl.TabIndex = 18
        Me.tabControl.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tab1})
        '
        'tab1
        '
        Me.tab1.Controls.Add(Me.LayoutControl1)
        Me.tab1.Name = "tab1"
        Me.tab1.Size = New System.Drawing.Size(730, 483)
        Me.tab1.Text = "Informasi Pasien"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.grdDetail)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup2
        Me.LayoutControl1.Size = New System.Drawing.Size(730, 483)
        Me.LayoutControl1.TabIndex = 19
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'grdDetail
        '
        Me.grdDetail.ContextMenuStrip = Me.mnuStrip
        Me.grdDetail.DataSource = Me.BindingSource1
        Me.grdDetail.Location = New System.Drawing.Point(12, 12)
        Me.grdDetail.MainView = Me.grvDetail
        Me.grdDetail.MenuManager = Me.barManager
        Me.grdDetail.Name = "grdDetail"
        Me.grdDetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.txtREMARKS, Me.grdMACAMDIET, Me.chkISBANYAK, Me.chkISBIASA, Me.chkISTKTP, Me.chkISRG, Me.chkISRL, Me.chkISRS, Me.chkISRPROT, Me.chkISRP, Me.chkISDM, Me.chkISRK, Me.chkISRKAL, Me.chkISPUASA})
        Me.grdDetail.Size = New System.Drawing.Size(706, 459)
        Me.grdDetail.TabIndex = 19
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
        'BindingSource1
        '
        Me.BindingSource1.DataSource = GetType(DataAccess.S_DIGITAL_HANDOVERALKE)
        '
        'grvDetail
        '
        Me.grvDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colID, Me.colDATECREATED, Me.colDATEUPDATED, Me.colKDKUNJUNGAN, Me.colNAME_DISPLAY, Me.colSTETOSKOP_ADA, Me.colSTETOSKOP_BERFUNGSI, Me.colSPHYGMOMANOMETER_ADA, Me.colSPHYGMOMANOMETER_BERFUNGSI, Me.colTERMOMETER_ADA, Me.colTERMOMETER_BERFUNGSI, Me.colOXYMETER_ADA, Me.colOXYMETER_BERFUNGSI, Me.colEKG_ADA, Me.colEKG_BERFUNGSI, Me.colUSER, Me.colKDRUANGAN, Me.colNAMARUANGAN, Me.colSESI})
        Me.grvDetail.GridControl = Me.grdDetail
        Me.grvDetail.Name = "grvDetail"
        Me.grvDetail.OptionsCustomization.AllowColumnMoving = false
        Me.grvDetail.OptionsCustomization.AllowFilter = false
        Me.grvDetail.OptionsCustomization.AllowGroup = false
        Me.grvDetail.OptionsCustomization.AllowQuickHideColumns = false
        Me.grvDetail.OptionsCustomization.AllowSort = false
        Me.grvDetail.OptionsDetail.EnableMasterViewMode = false
        Me.grvDetail.OptionsFind.AllowFindPanel = false
        Me.grvDetail.OptionsLayout.StoreAllOptions = true
        Me.grvDetail.OptionsLayout.StoreAppearance = true
        Me.grvDetail.OptionsMenu.EnableColumnMenu = false
        Me.grvDetail.OptionsNavigation.AutoFocusNewRow = true
        Me.grvDetail.OptionsNavigation.EnterMoveNextColumn = true
        Me.grvDetail.OptionsView.EnableAppearanceEvenRow = true
        Me.grvDetail.OptionsView.EnableAppearanceOddRow = true
        Me.grvDetail.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.grvDetail.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.grvDetail.OptionsView.ShowFooter = true
        Me.grvDetail.OptionsView.ShowGroupPanel = false
        '
        'colID
        '
        Me.colID.FieldName = "ID"
        Me.colID.Name = "colID"
        '
        'colDATECREATED
        '
        Me.colDATECREATED.FieldName = "DATECREATED"
        Me.colDATECREATED.Name = "colDATECREATED"
        Me.colDATECREATED.Visible = true
        Me.colDATECREATED.VisibleIndex = 0
        '
        'colDATEUPDATED
        '
        Me.colDATEUPDATED.FieldName = "DATEUPDATED"
        Me.colDATEUPDATED.Name = "colDATEUPDATED"
        Me.colDATEUPDATED.Visible = true
        Me.colDATEUPDATED.VisibleIndex = 1
        '
        'colKDKUNJUNGAN
        '
        Me.colKDKUNJUNGAN.FieldName = "KDKUNJUNGAN"
        Me.colKDKUNJUNGAN.Name = "colKDKUNJUNGAN"
        Me.colKDKUNJUNGAN.OptionsColumn.AllowEdit = false
        Me.colKDKUNJUNGAN.Visible = true
        Me.colKDKUNJUNGAN.VisibleIndex = 2
        '
        'colNAME_DISPLAY
        '
        Me.colNAME_DISPLAY.Caption = "Nama Pasien"
        Me.colNAME_DISPLAY.FieldName = "NAME_DISPLAY"
        Me.colNAME_DISPLAY.Name = "colNAME_DISPLAY"
        Me.colNAME_DISPLAY.OptionsColumn.AllowEdit = false
        Me.colNAME_DISPLAY.Visible = true
        Me.colNAME_DISPLAY.VisibleIndex = 3
        '
        'colSTETOSKOP_ADA
        '
        Me.colSTETOSKOP_ADA.Caption = "Stetoskop Tersedia"
        Me.colSTETOSKOP_ADA.FieldName = "STETOSKOP_ADA"
        Me.colSTETOSKOP_ADA.Name = "colSTETOSKOP_ADA"
        Me.colSTETOSKOP_ADA.Visible = true
        Me.colSTETOSKOP_ADA.VisibleIndex = 4
        '
        'colSTETOSKOP_BERFUNGSI
        '
        Me.colSTETOSKOP_BERFUNGSI.Caption = "Stetoskop Berfungsi"
        Me.colSTETOSKOP_BERFUNGSI.FieldName = "STETOSKOP_BERFUNGSI"
        Me.colSTETOSKOP_BERFUNGSI.Name = "colSTETOSKOP_BERFUNGSI"
        Me.colSTETOSKOP_BERFUNGSI.Visible = true
        Me.colSTETOSKOP_BERFUNGSI.VisibleIndex = 5
        '
        'colSPHYGMOMANOMETER_ADA
        '
        Me.colSPHYGMOMANOMETER_ADA.Caption = "Sphygmomanometer Tersedia"
        Me.colSPHYGMOMANOMETER_ADA.FieldName = "SPHYGMOMANOMETER_ADA"
        Me.colSPHYGMOMANOMETER_ADA.Name = "colSPHYGMOMANOMETER_ADA"
        Me.colSPHYGMOMANOMETER_ADA.Visible = true
        Me.colSPHYGMOMANOMETER_ADA.VisibleIndex = 6
        '
        'colSPHYGMOMANOMETER_BERFUNGSI
        '
        Me.colSPHYGMOMANOMETER_BERFUNGSI.Caption = "Sphygmomanometer Berfungsi"
        Me.colSPHYGMOMANOMETER_BERFUNGSI.FieldName = "SPHYGMOMANOMETER_BERFUNGSI"
        Me.colSPHYGMOMANOMETER_BERFUNGSI.Name = "colSPHYGMOMANOMETER_BERFUNGSI"
        Me.colSPHYGMOMANOMETER_BERFUNGSI.Visible = true
        Me.colSPHYGMOMANOMETER_BERFUNGSI.VisibleIndex = 7
        '
        'colTERMOMETER_ADA
        '
        Me.colTERMOMETER_ADA.Caption = "Termometer Tersedia"
        Me.colTERMOMETER_ADA.FieldName = "TERMOMETER_ADA"
        Me.colTERMOMETER_ADA.Name = "colTERMOMETER_ADA"
        Me.colTERMOMETER_ADA.Visible = true
        Me.colTERMOMETER_ADA.VisibleIndex = 8
        '
        'colTERMOMETER_BERFUNGSI
        '
        Me.colTERMOMETER_BERFUNGSI.Caption = "Termometer Berfungsi"
        Me.colTERMOMETER_BERFUNGSI.FieldName = "TERMOMETER_BERFUNGSI"
        Me.colTERMOMETER_BERFUNGSI.Name = "colTERMOMETER_BERFUNGSI"
        Me.colTERMOMETER_BERFUNGSI.Visible = true
        Me.colTERMOMETER_BERFUNGSI.VisibleIndex = 9
        '
        'colOXYMETER_ADA
        '
        Me.colOXYMETER_ADA.Caption = "Oxymeter Tersedia"
        Me.colOXYMETER_ADA.FieldName = "OXYMETER_ADA"
        Me.colOXYMETER_ADA.Name = "colOXYMETER_ADA"
        Me.colOXYMETER_ADA.Visible = true
        Me.colOXYMETER_ADA.VisibleIndex = 10
        '
        'colOXYMETER_BERFUNGSI
        '
        Me.colOXYMETER_BERFUNGSI.Caption = "Oxymeter Berfungsi"
        Me.colOXYMETER_BERFUNGSI.FieldName = "OXYMETER_BERFUNGSI"
        Me.colOXYMETER_BERFUNGSI.Name = "colOXYMETER_BERFUNGSI"
        Me.colOXYMETER_BERFUNGSI.Visible = true
        Me.colOXYMETER_BERFUNGSI.VisibleIndex = 11
        '
        'colEKG_ADA
        '
        Me.colEKG_ADA.Caption = "EKG Tersedia"
        Me.colEKG_ADA.FieldName = "EKG_ADA"
        Me.colEKG_ADA.Name = "colEKG_ADA"
        Me.colEKG_ADA.Visible = true
        Me.colEKG_ADA.VisibleIndex = 12
        '
        'colEKG_BERFUNGSI
        '
        Me.colEKG_BERFUNGSI.Caption = "EKG Berfungsi"
        Me.colEKG_BERFUNGSI.FieldName = "EKG_BERFUNGSI"
        Me.colEKG_BERFUNGSI.Name = "colEKG_BERFUNGSI"
        Me.colEKG_BERFUNGSI.Visible = true
        Me.colEKG_BERFUNGSI.VisibleIndex = 13
        '
        'colUSER
        '
        Me.colUSER.Caption = "User"
        Me.colUSER.FieldName = "KDUSER"
        Me.colUSER.Name = "colUSER"
        Me.colUSER.OptionsColumn.AllowEdit = false
        Me.colUSER.Visible = true
        Me.colUSER.VisibleIndex = 14
        '
        'colKDRUANGAN
        '
        Me.colKDRUANGAN.Caption = "KDRUANGAN"
        Me.colKDRUANGAN.FieldName = "KDRUANGAN"
        Me.colKDRUANGAN.Name = "colKDRUANGAN"
        '
        'colNAMARUANGAN
        '
        Me.colNAMARUANGAN.Caption = "NAMARUANGAN"
        Me.colNAMARUANGAN.FieldName = "NAMARUANGAN"
        Me.colNAMARUANGAN.Name = "colNAMARUANGAN"
        '
        'colSESI
        '
        Me.colSESI.Caption = "SESI"
        Me.colSESI.FieldName = "SESI"
        Me.colSESI.Name = "colSESI"
        '
        'txtREMARKS
        '
        Me.txtREMARKS.AutoHeight = false
        Me.txtREMARKS.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtREMARKS.Name = "txtREMARKS"
        '
        'grdMACAMDIET
        '
        Me.grdMACAMDIET.AutoHeight = false
        Me.grdMACAMDIET.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdMACAMDIET.Name = "grdMACAMDIET"
        Me.grdMACAMDIET.NullText = ""
        Me.grdMACAMDIET.View = Me.grvMACAMDIET
        '
        'grvMACAMDIET
        '
        Me.grvMACAMDIET.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2})
        Me.grvMACAMDIET.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvMACAMDIET.Name = "grvMACAMDIET"
        Me.grvMACAMDIET.OptionsSelection.EnableAppearanceFocusedCell = false
        Me.grvMACAMDIET.OptionsView.ShowGroupPanel = false
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nama"
        Me.GridColumn2.FieldName = "NAMA"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = true
        Me.GridColumn2.VisibleIndex = 0
        '
        'chkISBANYAK
        '
        Me.chkISBANYAK.AutoHeight = false
        Me.chkISBANYAK.Name = "chkISBANYAK"
        Me.chkISBANYAK.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'chkISBIASA
        '
        Me.chkISBIASA.AutoHeight = false
        Me.chkISBIASA.Name = "chkISBIASA"
        Me.chkISBIASA.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'chkISTKTP
        '
        Me.chkISTKTP.AutoHeight = false
        Me.chkISTKTP.Name = "chkISTKTP"
        Me.chkISTKTP.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'chkISRG
        '
        Me.chkISRG.AutoHeight = false
        Me.chkISRG.Name = "chkISRG"
        Me.chkISRG.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'chkISRL
        '
        Me.chkISRL.AutoHeight = false
        Me.chkISRL.Name = "chkISRL"
        Me.chkISRL.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'chkISRS
        '
        Me.chkISRS.AutoHeight = false
        Me.chkISRS.Name = "chkISRS"
        Me.chkISRS.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'chkISRPROT
        '
        Me.chkISRPROT.AutoHeight = false
        Me.chkISRPROT.Name = "chkISRPROT"
        Me.chkISRPROT.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'chkISRP
        '
        Me.chkISRP.AutoHeight = false
        Me.chkISRP.Name = "chkISRP"
        Me.chkISRP.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'chkISDM
        '
        Me.chkISDM.AutoHeight = false
        Me.chkISDM.Name = "chkISDM"
        Me.chkISDM.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'chkISRK
        '
        Me.chkISRK.AutoHeight = false
        Me.chkISRK.Name = "chkISRK"
        Me.chkISRK.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'chkISRKAL
        '
        Me.chkISRKAL.AutoHeight = false
        Me.chkISRKAL.Name = "chkISRKAL"
        Me.chkISRKAL.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'chkISPUASA
        '
        Me.chkISPUASA.AutoHeight = false
        Me.chkISPUASA.Name = "chkISPUASA"
        Me.chkISPUASA.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup2.GroupBordersVisible = false
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem6})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(730, 483)
        Me.LayoutControlGroup2.TextVisible = false
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.grdDetail
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(710, 463)
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem6.TextVisible = false
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = false
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem5, Me.lDATE, Me.LayoutControlItem1, Me.LayoutControlItem10, Me.EmptySpaceItem1, Me.LayoutControlItem2, Me.LayoutControlItem3})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(760, 585)
        Me.LayoutControlGroup1.TextVisible = false
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.tabControl
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 50)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(740, 515)
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextVisible = false
        '
        'lDATE
        '
        Me.lDATE.AppearanceItemCaption.Options.UseTextOptions = true
        Me.lDATE.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lDATE.Control = Me.deDATE
        Me.lDATE.CustomizationFormText = "Date :"
        Me.lDATE.Location = New System.Drawing.Point(0, 0)
        Me.lDATE.Name = "lDATE"
        Me.lDATE.Size = New System.Drawing.Size(255, 24)
        Me.lDATE.Text = "Tanggal Input :"
        Me.lDATE.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lDATE.TextSize = New System.Drawing.Size(130, 20)
        Me.lDATE.TextToControlDistance = 5
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.AppearanceItemCaption.Options.UseTextOptions = true
        Me.LayoutControlItem1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem1.Control = Me.grdPOLIRUANGAN
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(255, 26)
        Me.LayoutControlItem1.Text = "Ruang * :"
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(130, 20)
        Me.LayoutControlItem1.TextToControlDistance = 5
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.Control = Me.btnCariPasien
        Me.LayoutControlItem10.Location = New System.Drawing.Point(609, 0)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Size = New System.Drawing.Size(131, 26)
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem10.TextVisible = false
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = false
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(255, 0)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(202, 50)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.txtRM
        Me.LayoutControlItem2.Location = New System.Drawing.Point(457, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(152, 26)
        Me.LayoutControlItem2.Text = "No.RM :"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(39, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.grdCARI
        Me.LayoutControlItem3.Location = New System.Drawing.Point(457, 26)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(283, 24)
        Me.LayoutControlItem3.Text = "Hasil :"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(39, 13)
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Number"
        Me.GridColumn6.FieldName = "KDSO"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = true
        Me.GridColumn6.VisibleIndex = 0
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Name Display"
        Me.GridColumn3.FieldName = "NAME_DISPLAY"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = true
        Me.GridColumn3.VisibleIndex = 0
        '
        'frmHandoverAlkes
        '
        Me.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(235,Byte),Integer), CType(CType(236,Byte),Integer), CType(CType(239,Byte),Integer))
        Me.Appearance.Options.UseBackColor = true
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(760, 607)
        Me.Controls.Add(Me.layoutControl)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = true
        Me.Name = "frmHandoverAlkes"
        Me.ShowIcon = false
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.layoutControl,System.ComponentModel.ISupportInitialize).EndInit
        Me.layoutControl.ResumeLayout(false)
        CType(Me.grdCARI.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.barManager,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.progressBarSave,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.progressSave,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.grvCARI,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtRM.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.grdPOLIRUANGAN.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.grvKDPOLIRUANGAN,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.deDATE.Properties.CalendarTimeProperties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.deDATE.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.tabControl,System.ComponentModel.ISupportInitialize).EndInit
        Me.tabControl.ResumeLayout(false)
        Me.tab1.ResumeLayout(false)
        CType(Me.LayoutControl1,System.ComponentModel.ISupportInitialize).EndInit
        Me.LayoutControl1.ResumeLayout(false)
        CType(Me.grdDetail,System.ComponentModel.ISupportInitialize).EndInit
        Me.mnuStrip.ResumeLayout(false)
        CType(Me.BindingSource1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.grvDetail,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtREMARKS,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.grdMACAMDIET,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.grvMACAMDIET,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.chkISBANYAK,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.chkISBIASA,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.chkISTKTP,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.chkISRG,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.chkISRL,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.chkISRS,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.chkISRPROT,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.chkISRP,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.chkISDM,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.chkISRK,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.chkISRKAL,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.chkISPUASA,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem6,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem5,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.lDATE,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem10,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.EmptySpaceItem1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem3,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents layoutControl As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
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
    Friend WithEvents tabControl As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents tab1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents deDATE As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lDATE As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents mnuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdPOLIRUANGAN As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents grvKDPOLIRUANGAN As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btnFocus As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents grdDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents grvDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grdMACAMDIET As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents grvMACAMDIET As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents chkISBANYAK As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents txtREMARKS As DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents chkISBIASA As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents chkISTKTP As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents chkISRG As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents chkISRL As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents chkISRS As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents chkISRPROT As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents chkISRP As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents chkISDM As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents chkISRK As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents chkISRKAL As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents chkISPUASA As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents btnCariPasien As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents BindingSource1 As BindingSource
    Friend WithEvents colID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDATECREATED As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDATEUPDATED As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colKDKUNJUNGAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colNAME_DISPLAY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colSTETOSKOP_ADA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colSTETOSKOP_BERFUNGSI As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colSPHYGMOMANOMETER_ADA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colSPHYGMOMANOMETER_BERFUNGSI As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTERMOMETER_ADA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTERMOMETER_BERFUNGSI As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colOXYMETER_ADA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colOXYMETER_BERFUNGSI As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colEKG_ADA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colEKG_BERFUNGSI As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colUSER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colKDRUANGAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colNAMARUANGAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colSESI As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents txtRM As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grdCARI As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents grvCARI As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
End Class
