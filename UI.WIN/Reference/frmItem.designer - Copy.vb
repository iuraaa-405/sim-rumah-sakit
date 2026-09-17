<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmItem
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
        Me.txtCODE = New DevExpress.XtraEditors.TextEdit()
        Me.barManager = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barTop = New DevExpress.XtraBars.Bar()
        Me.btnSaveNew = New DevExpress.XtraBars.BarButtonItem()
        Me.btnSaveClose = New DevExpress.XtraBars.BarButtonItem()
        Me.btnClose = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.progressBarSave = New DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar()
        Me.progressSave = New DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar()
        Me.txtPRICE = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.tabControl = New DevExpress.XtraTab.XtraTabControl()
        Me.tab1 = New DevExpress.XtraTab.XtraTabPage()
        Me.grdDetail_UOM = New DevExpress.XtraGrid.GridControl()
        Me.mnuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.bindingSource_UOM = New System.Windows.Forms.BindingSource(Me.components)
        Me.grvDetail_UOM = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colKDUOM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdUOM = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.grvUOM = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colRATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPRICEPURCHASESTANDARD = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPRICESALESSTANDARD = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colMARGIN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.tab3 = New DevExpress.XtraTab.XtraTabPage()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.grdITEM_L6 = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.grvItem_L6 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdITEM_L5 = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.grvItem_L5 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdITEM_L4 = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.grvItem_L4 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdITEM_L3 = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.grvItem_L3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdITEM_L2 = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.grvItem_L2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdITEM_L1 = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.grvItem_L1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.lKDITEM_L1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lKDITEM_L2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lKDITEM_L3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lKDITEM_L4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lKDITEM_L5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lKDITEM_L6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.tab4 = New DevExpress.XtraTab.XtraTabPage()
        Me.LayoutControl3 = New DevExpress.XtraLayout.LayoutControl()
        Me.grdCOA_SALES = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.grvCOASALES = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdCOA_INVENTORY = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.grvCOAITEM = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdCOA_COST = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.grvCOAHPP = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.lKDCOA_COST = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lKDCOA_INVENTORY = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lKDCOA_SALES = New DevExpress.XtraLayout.LayoutControlItem()
        Me.tab5 = New DevExpress.XtraTab.XtraTabPage()
        Me.picGAMBAR = New System.Windows.Forms.PictureBox()
        Me.txtNMITEM2 = New DevExpress.XtraEditors.TextEdit()
        Me.chkISACTIVE = New DevExpress.XtraEditors.CheckEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lNMITEM2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lPRICE = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lCODE = New DevExpress.XtraLayout.LayoutControlItem()
        Me.fileDialog = New System.Windows.Forms.OpenFileDialog()
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.layoutControl.SuspendLayout()
        CType(Me.txtCODE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPRICE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tabControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabControl.SuspendLayout()
        Me.tab1.SuspendLayout()
        CType(Me.grdDetail_UOM, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuStrip.SuspendLayout()
        CType(Me.bindingSource_UOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvDetail_UOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdUOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvUOM, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tab3.SuspendLayout()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.grdITEM_L6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvItem_L6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdITEM_L5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvItem_L5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdITEM_L4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvItem_L4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdITEM_L3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvItem_L3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdITEM_L2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvItem_L2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdITEM_L1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvItem_L1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKDITEM_L1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKDITEM_L2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKDITEM_L3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKDITEM_L4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKDITEM_L5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKDITEM_L6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tab4.SuspendLayout()
        CType(Me.LayoutControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl3.SuspendLayout()
        CType(Me.grdCOA_SALES.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvCOASALES, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCOA_INVENTORY.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvCOAITEM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCOA_COST.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvCOAHPP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKDCOA_COST, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKDCOA_INVENTORY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKDCOA_SALES, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tab5.SuspendLayout()
        CType(Me.picGAMBAR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNMITEM2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkISACTIVE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lNMITEM2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lPRICE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lCODE, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'layoutControl
        '
        Me.layoutControl.Controls.Add(Me.txtCODE)
        Me.layoutControl.Controls.Add(Me.txtPRICE)
        Me.layoutControl.Controls.Add(Me.LayoutControl2)
        Me.layoutControl.Controls.Add(Me.tabControl)
        Me.layoutControl.Controls.Add(Me.txtNMITEM2)
        Me.layoutControl.Controls.Add(Me.chkISACTIVE)
        Me.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutControl.Location = New System.Drawing.Point(0, 0)
        Me.layoutControl.Name = "layoutControl"
        Me.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(774, 238, 250, 350)
        Me.layoutControl.Root = Me.LayoutControlGroup1
        Me.layoutControl.Size = New System.Drawing.Size(590, 401)
        Me.layoutControl.TabIndex = 0
        Me.layoutControl.Text = "LayoutControl1"
        '
        'txtCODE
        '
        Me.txtCODE.EnterMoveNextControl = True
        Me.txtCODE.Location = New System.Drawing.Point(117, 12)
        Me.txtCODE.MenuManager = Me.barManager
        Me.txtCODE.Name = "txtCODE"
        Me.txtCODE.Properties.ReadOnly = True
        Me.txtCODE.Size = New System.Drawing.Size(301, 20)
        Me.txtCODE.StyleController = Me.layoutControl
        Me.txtCODE.TabIndex = 16
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
        Me.barManager.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.btnSaveNew, Me.btnClose, Me.btnSaveClose})
        Me.barManager.MainMenu = Me.barTop
        Me.barManager.MaxItemId = 8
        Me.barManager.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.progressBarSave, Me.progressSave})
        '
        'barTop
        '
        Me.barTop.BarName = "Main menu"
        Me.barTop.DockCol = 0
        Me.barTop.DockRow = 0
        Me.barTop.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom
        Me.barTop.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.btnSaveNew), New DevExpress.XtraBars.LinkPersistInfo(Me.btnSaveClose), New DevExpress.XtraBars.LinkPersistInfo(Me.btnClose)})
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
        Me.barDockControlTop.Size = New System.Drawing.Size(590, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 401)
        Me.barDockControlBottom.Size = New System.Drawing.Size(590, 22)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 401)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(590, 0)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 401)
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
        'txtPRICE
        '
        Me.txtPRICE.EnterMoveNextControl = True
        Me.txtPRICE.Location = New System.Drawing.Point(117, 60)
        Me.txtPRICE.MenuManager = Me.barManager
        Me.txtPRICE.Name = "txtPRICE"
        Me.txtPRICE.Properties.Appearance.Options.UseTextOptions = True
        Me.txtPRICE.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtPRICE.Properties.Mask.EditMask = "n2"
        Me.txtPRICE.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtPRICE.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtPRICE.Properties.NullText = "0.00"
        Me.txtPRICE.Size = New System.Drawing.Size(301, 20)
        Me.txtPRICE.StyleController = Me.layoutControl
        Me.txtPRICE.TabIndex = 34
        '
        'LayoutControl2
        '
        Me.LayoutControl2.Location = New System.Drawing.Point(12, 84)
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.Root = Me.Root
        Me.LayoutControl2.Size = New System.Drawing.Size(566, 20)
        Me.LayoutControl2.TabIndex = 19
        Me.LayoutControl2.Text = "LayoutControl2"
        '
        'Root
        '
        Me.Root.CustomizationFormText = "Root"
        Me.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.Root.GroupBordersVisible = False
        Me.Root.Location = New System.Drawing.Point(0, 0)
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(566, 20)
        Me.Root.TextVisible = False
        '
        'tabControl
        '
        Me.tabControl.Location = New System.Drawing.Point(12, 108)
        Me.tabControl.Name = "tabControl"
        Me.tabControl.SelectedTabPage = Me.tab1
        Me.tabControl.Size = New System.Drawing.Size(566, 281)
        Me.tabControl.TabIndex = 18
        Me.tabControl.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tab1, Me.tab3, Me.tab4, Me.tab5})
        '
        'tab1
        '
        Me.tab1.Controls.Add(Me.grdDetail_UOM)
        Me.tab1.Name = "tab1"
        Me.tab1.Size = New System.Drawing.Size(560, 253)
        Me.tab1.Text = "Informasi Satuan"
        '
        'grdDetail_UOM
        '
        Me.grdDetail_UOM.ContextMenuStrip = Me.mnuStrip
        Me.grdDetail_UOM.DataSource = Me.bindingSource_UOM
        Me.grdDetail_UOM.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdDetail_UOM.Location = New System.Drawing.Point(0, 0)
        Me.grdDetail_UOM.MainView = Me.grvDetail_UOM
        Me.grdDetail_UOM.MenuManager = Me.barManager
        Me.grdDetail_UOM.Name = "grdDetail_UOM"
        Me.grdDetail_UOM.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.grdUOM})
        Me.grdDetail_UOM.Size = New System.Drawing.Size(560, 253)
        Me.grdDetail_UOM.TabIndex = 18
        Me.grdDetail_UOM.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvDetail_UOM})
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
        'bindingSource_UOM
        '
        Me.bindingSource_UOM.DataSource = GetType(DataAccess.M_ITEM_UOM)
        '
        'grvDetail_UOM
        '
        Me.grvDetail_UOM.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colKDUOM, Me.colRATE, Me.colPRICEPURCHASESTANDARD, Me.colPRICESALESSTANDARD, Me.colMARGIN})
        Me.grvDetail_UOM.GridControl = Me.grdDetail_UOM
        Me.grvDetail_UOM.Name = "grvDetail_UOM"
        Me.grvDetail_UOM.OptionsCustomization.AllowColumnMoving = False
        Me.grvDetail_UOM.OptionsCustomization.AllowFilter = False
        Me.grvDetail_UOM.OptionsCustomization.AllowGroup = False
        Me.grvDetail_UOM.OptionsCustomization.AllowQuickHideColumns = False
        Me.grvDetail_UOM.OptionsCustomization.AllowSort = False
        Me.grvDetail_UOM.OptionsDetail.EnableMasterViewMode = False
        Me.grvDetail_UOM.OptionsFind.AllowFindPanel = False
        Me.grvDetail_UOM.OptionsMenu.EnableColumnMenu = False
        Me.grvDetail_UOM.OptionsNavigation.AutoFocusNewRow = True
        Me.grvDetail_UOM.OptionsNavigation.EnterMoveNextColumn = True
        Me.grvDetail_UOM.OptionsView.EnableAppearanceEvenRow = True
        Me.grvDetail_UOM.OptionsView.EnableAppearanceOddRow = True
        Me.grvDetail_UOM.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.grvDetail_UOM.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.grvDetail_UOM.OptionsView.ShowFooter = True
        Me.grvDetail_UOM.OptionsView.ShowGroupPanel = False
        '
        'colKDUOM
        '
        Me.colKDUOM.Caption = "Satuan"
        Me.colKDUOM.ColumnEdit = Me.grdUOM
        Me.colKDUOM.FieldName = "KDUOM"
        Me.colKDUOM.Name = "colKDUOM"
        Me.colKDUOM.Visible = True
        Me.colKDUOM.VisibleIndex = 0
        '
        'grdUOM
        '
        Me.grdUOM.AutoHeight = False
        Me.grdUOM.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdUOM.Name = "grdUOM"
        Me.grdUOM.NullText = ""
        Me.grdUOM.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdUOM.View = Me.grvUOM
        '
        'grvUOM
        '
        Me.grvUOM.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn4})
        Me.grvUOM.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvUOM.Name = "grvUOM"
        Me.grvUOM.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvUOM.OptionsView.ShowAutoFilterRow = True
        Me.grvUOM.OptionsView.ShowGroupPanel = False
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Keterangan"
        Me.GridColumn4.FieldName = "MEMO"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 0
        '
        'colRATE
        '
        Me.colRATE.AppearanceCell.Options.UseTextOptions = True
        Me.colRATE.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.colRATE.Caption = "Persen"
        Me.colRATE.DisplayFormat.FormatString = "{0:n2}"
        Me.colRATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colRATE.FieldName = "RATE"
        Me.colRATE.Name = "colRATE"
        Me.colRATE.Visible = True
        Me.colRATE.VisibleIndex = 1
        '
        'colPRICEPURCHASESTANDARD
        '
        Me.colPRICEPURCHASESTANDARD.AppearanceCell.Options.UseTextOptions = True
        Me.colPRICEPURCHASESTANDARD.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.colPRICEPURCHASESTANDARD.Caption = "Harga Beli"
        Me.colPRICEPURCHASESTANDARD.DisplayFormat.FormatString = "{0:n2}"
        Me.colPRICEPURCHASESTANDARD.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colPRICEPURCHASESTANDARD.FieldName = "PRICEPURCHASESTANDARD"
        Me.colPRICEPURCHASESTANDARD.Name = "colPRICEPURCHASESTANDARD"
        '
        'colPRICESALESSTANDARD
        '
        Me.colPRICESALESSTANDARD.AppearanceCell.Options.UseTextOptions = True
        Me.colPRICESALESSTANDARD.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.colPRICESALESSTANDARD.Caption = "Harga Jual"
        Me.colPRICESALESSTANDARD.DisplayFormat.FormatString = "{0:n2}"
        Me.colPRICESALESSTANDARD.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colPRICESALESSTANDARD.FieldName = "PRICESALESSTANDARD"
        Me.colPRICESALESSTANDARD.Name = "colPRICESALESSTANDARD"
        '
        'colMARGIN
        '
        Me.colMARGIN.AppearanceCell.Options.UseTextOptions = True
        Me.colMARGIN.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.colMARGIN.Caption = "Keuntungan"
        Me.colMARGIN.DisplayFormat.FormatString = "{0:n2}"
        Me.colMARGIN.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colMARGIN.FieldName = "MARGIN"
        Me.colMARGIN.Name = "colMARGIN"
        Me.colMARGIN.Visible = True
        Me.colMARGIN.VisibleIndex = 2
        '
        'tab3
        '
        Me.tab3.Controls.Add(Me.LayoutControl1)
        Me.tab3.Name = "tab3"
        Me.tab3.Size = New System.Drawing.Size(560, 253)
        Me.tab3.Text = "Informasi Spesifikasi"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.grdITEM_L6)
        Me.LayoutControl1.Controls.Add(Me.grdITEM_L5)
        Me.LayoutControl1.Controls.Add(Me.grdITEM_L4)
        Me.LayoutControl1.Controls.Add(Me.grdITEM_L3)
        Me.LayoutControl1.Controls.Add(Me.grdITEM_L2)
        Me.LayoutControl1.Controls.Add(Me.grdITEM_L1)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup2
        Me.LayoutControl1.Size = New System.Drawing.Size(560, 253)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'grdITEM_L6
        '
        Me.grdITEM_L6.EnterMoveNextControl = True
        Me.grdITEM_L6.Location = New System.Drawing.Point(117, 132)
        Me.grdITEM_L6.MenuManager = Me.barManager
        Me.grdITEM_L6.Name = "grdITEM_L6"
        Me.grdITEM_L6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdITEM_L6.Properties.NullText = ""
        Me.grdITEM_L6.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdITEM_L6.Properties.View = Me.grvItem_L6
        Me.grdITEM_L6.Size = New System.Drawing.Size(431, 20)
        Me.grdITEM_L6.StyleController = Me.LayoutControl1
        Me.grdITEM_L6.TabIndex = 9
        '
        'grvItem_L6
        '
        Me.grvItem_L6.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn8})
        Me.grvItem_L6.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvItem_L6.Name = "grvItem_L6"
        Me.grvItem_L6.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvItem_L6.OptionsView.ShowAutoFilterRow = True
        Me.grvItem_L6.OptionsView.ShowGroupPanel = False
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Keterangan"
        Me.GridColumn8.FieldName = "MEMO"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 0
        '
        'grdITEM_L5
        '
        Me.grdITEM_L5.EnterMoveNextControl = True
        Me.grdITEM_L5.Location = New System.Drawing.Point(117, 108)
        Me.grdITEM_L5.MenuManager = Me.barManager
        Me.grdITEM_L5.Name = "grdITEM_L5"
        Me.grdITEM_L5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdITEM_L5.Properties.NullText = ""
        Me.grdITEM_L5.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdITEM_L5.Properties.View = Me.grvItem_L5
        Me.grdITEM_L5.Size = New System.Drawing.Size(431, 20)
        Me.grdITEM_L5.StyleController = Me.LayoutControl1
        Me.grdITEM_L5.TabIndex = 8
        '
        'grvItem_L5
        '
        Me.grvItem_L5.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn7})
        Me.grvItem_L5.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvItem_L5.Name = "grvItem_L5"
        Me.grvItem_L5.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvItem_L5.OptionsView.ShowAutoFilterRow = True
        Me.grvItem_L5.OptionsView.ShowGroupPanel = False
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Keterangan"
        Me.GridColumn7.FieldName = "MEMO"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 0
        '
        'grdITEM_L4
        '
        Me.grdITEM_L4.EnterMoveNextControl = True
        Me.grdITEM_L4.Location = New System.Drawing.Point(117, 84)
        Me.grdITEM_L4.MenuManager = Me.barManager
        Me.grdITEM_L4.Name = "grdITEM_L4"
        Me.grdITEM_L4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdITEM_L4.Properties.NullText = ""
        Me.grdITEM_L4.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdITEM_L4.Properties.View = Me.grvItem_L4
        Me.grdITEM_L4.Size = New System.Drawing.Size(431, 20)
        Me.grdITEM_L4.StyleController = Me.LayoutControl1
        Me.grdITEM_L4.TabIndex = 7
        '
        'grvItem_L4
        '
        Me.grvItem_L4.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn6})
        Me.grvItem_L4.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvItem_L4.Name = "grvItem_L4"
        Me.grvItem_L4.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvItem_L4.OptionsView.ShowAutoFilterRow = True
        Me.grvItem_L4.OptionsView.ShowGroupPanel = False
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Keterangan"
        Me.GridColumn6.FieldName = "MEMO"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 0
        '
        'grdITEM_L3
        '
        Me.grdITEM_L3.EnterMoveNextControl = True
        Me.grdITEM_L3.Location = New System.Drawing.Point(117, 60)
        Me.grdITEM_L3.MenuManager = Me.barManager
        Me.grdITEM_L3.Name = "grdITEM_L3"
        Me.grdITEM_L3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdITEM_L3.Properties.NullText = ""
        Me.grdITEM_L3.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdITEM_L3.Properties.View = Me.grvItem_L3
        Me.grdITEM_L3.Size = New System.Drawing.Size(431, 20)
        Me.grdITEM_L3.StyleController = Me.LayoutControl1
        Me.grdITEM_L3.TabIndex = 6
        '
        'grvItem_L3
        '
        Me.grvItem_L3.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn5})
        Me.grvItem_L3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvItem_L3.Name = "grvItem_L3"
        Me.grvItem_L3.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvItem_L3.OptionsView.ShowAutoFilterRow = True
        Me.grvItem_L3.OptionsView.ShowGroupPanel = False
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Keterangan"
        Me.GridColumn5.FieldName = "MEMO"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 0
        '
        'grdITEM_L2
        '
        Me.grdITEM_L2.EnterMoveNextControl = True
        Me.grdITEM_L2.Location = New System.Drawing.Point(117, 36)
        Me.grdITEM_L2.MenuManager = Me.barManager
        Me.grdITEM_L2.Name = "grdITEM_L2"
        Me.grdITEM_L2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdITEM_L2.Properties.NullText = ""
        Me.grdITEM_L2.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdITEM_L2.Properties.View = Me.grvItem_L2
        Me.grdITEM_L2.Size = New System.Drawing.Size(431, 20)
        Me.grdITEM_L2.StyleController = Me.LayoutControl1
        Me.grdITEM_L2.TabIndex = 5
        '
        'grvItem_L2
        '
        Me.grvItem_L2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2})
        Me.grvItem_L2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvItem_L2.Name = "grvItem_L2"
        Me.grvItem_L2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvItem_L2.OptionsView.ShowAutoFilterRow = True
        Me.grvItem_L2.OptionsView.ShowGroupPanel = False
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Keterangan"
        Me.GridColumn2.FieldName = "MEMO"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        '
        'grdITEM_L1
        '
        Me.grdITEM_L1.EnterMoveNextControl = True
        Me.grdITEM_L1.Location = New System.Drawing.Point(117, 12)
        Me.grdITEM_L1.MenuManager = Me.barManager
        Me.grdITEM_L1.Name = "grdITEM_L1"
        Me.grdITEM_L1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdITEM_L1.Properties.NullText = ""
        Me.grdITEM_L1.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdITEM_L1.Properties.View = Me.grvItem_L1
        Me.grdITEM_L1.Size = New System.Drawing.Size(431, 20)
        Me.grdITEM_L1.StyleController = Me.LayoutControl1
        Me.grdITEM_L1.TabIndex = 4
        '
        'grvItem_L1
        '
        Me.grvItem_L1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3})
        Me.grvItem_L1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvItem_L1.Name = "grvItem_L1"
        Me.grvItem_L1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvItem_L1.OptionsView.ShowAutoFilterRow = True
        Me.grvItem_L1.OptionsView.ShowGroupPanel = False
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Keterangan"
        Me.GridColumn3.FieldName = "MEMO"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.CustomizationFormText = "LayoutControlGroup2"
        Me.LayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup2.GroupBordersVisible = False
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lKDITEM_L1, Me.lKDITEM_L2, Me.lKDITEM_L3, Me.lKDITEM_L4, Me.lKDITEM_L5, Me.lKDITEM_L6})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(560, 253)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'lKDITEM_L1
        '
        Me.lKDITEM_L1.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKDITEM_L1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKDITEM_L1.Control = Me.grdITEM_L1
        Me.lKDITEM_L1.CustomizationFormText = "Item_L1 :"
        Me.lKDITEM_L1.Location = New System.Drawing.Point(0, 0)
        Me.lKDITEM_L1.Name = "lKDITEM_L1"
        Me.lKDITEM_L1.Size = New System.Drawing.Size(540, 24)
        Me.lKDITEM_L1.Text = "Item_L1 :"
        Me.lKDITEM_L1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKDITEM_L1.TextSize = New System.Drawing.Size(100, 20)
        Me.lKDITEM_L1.TextToControlDistance = 5
        '
        'lKDITEM_L2
        '
        Me.lKDITEM_L2.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKDITEM_L2.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKDITEM_L2.Control = Me.grdITEM_L2
        Me.lKDITEM_L2.CustomizationFormText = "Item_L2 :"
        Me.lKDITEM_L2.Location = New System.Drawing.Point(0, 24)
        Me.lKDITEM_L2.Name = "lKDITEM_L2"
        Me.lKDITEM_L2.Size = New System.Drawing.Size(540, 24)
        Me.lKDITEM_L2.Text = "Item_L2 :"
        Me.lKDITEM_L2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKDITEM_L2.TextSize = New System.Drawing.Size(100, 20)
        Me.lKDITEM_L2.TextToControlDistance = 5
        '
        'lKDITEM_L3
        '
        Me.lKDITEM_L3.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKDITEM_L3.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKDITEM_L3.Control = Me.grdITEM_L3
        Me.lKDITEM_L3.CustomizationFormText = "Item_L3 :"
        Me.lKDITEM_L3.Location = New System.Drawing.Point(0, 48)
        Me.lKDITEM_L3.Name = "lKDITEM_L3"
        Me.lKDITEM_L3.Size = New System.Drawing.Size(540, 24)
        Me.lKDITEM_L3.Text = "Item_L3 :"
        Me.lKDITEM_L3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKDITEM_L3.TextSize = New System.Drawing.Size(100, 20)
        Me.lKDITEM_L3.TextToControlDistance = 5
        '
        'lKDITEM_L4
        '
        Me.lKDITEM_L4.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKDITEM_L4.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKDITEM_L4.Control = Me.grdITEM_L4
        Me.lKDITEM_L4.CustomizationFormText = "Item_L4 :"
        Me.lKDITEM_L4.Location = New System.Drawing.Point(0, 72)
        Me.lKDITEM_L4.Name = "lKDITEM_L4"
        Me.lKDITEM_L4.Size = New System.Drawing.Size(540, 24)
        Me.lKDITEM_L4.Text = "Item_L4 :"
        Me.lKDITEM_L4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKDITEM_L4.TextSize = New System.Drawing.Size(100, 20)
        Me.lKDITEM_L4.TextToControlDistance = 5
        '
        'lKDITEM_L5
        '
        Me.lKDITEM_L5.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKDITEM_L5.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKDITEM_L5.Control = Me.grdITEM_L5
        Me.lKDITEM_L5.CustomizationFormText = "Item_L5 :"
        Me.lKDITEM_L5.Location = New System.Drawing.Point(0, 96)
        Me.lKDITEM_L5.Name = "lKDITEM_L5"
        Me.lKDITEM_L5.Size = New System.Drawing.Size(540, 24)
        Me.lKDITEM_L5.Text = "Item_L5 :"
        Me.lKDITEM_L5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKDITEM_L5.TextSize = New System.Drawing.Size(100, 20)
        Me.lKDITEM_L5.TextToControlDistance = 5
        '
        'lKDITEM_L6
        '
        Me.lKDITEM_L6.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKDITEM_L6.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKDITEM_L6.Control = Me.grdITEM_L6
        Me.lKDITEM_L6.CustomizationFormText = "Item_L6 :"
        Me.lKDITEM_L6.Location = New System.Drawing.Point(0, 120)
        Me.lKDITEM_L6.Name = "lKDITEM_L6"
        Me.lKDITEM_L6.Size = New System.Drawing.Size(540, 113)
        Me.lKDITEM_L6.Text = "Item_L6 :"
        Me.lKDITEM_L6.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKDITEM_L6.TextSize = New System.Drawing.Size(100, 20)
        Me.lKDITEM_L6.TextToControlDistance = 5
        '
        'tab4
        '
        Me.tab4.Controls.Add(Me.LayoutControl3)
        Me.tab4.Name = "tab4"
        Me.tab4.Size = New System.Drawing.Size(560, 253)
        Me.tab4.Text = "Account Information"
        '
        'LayoutControl3
        '
        Me.LayoutControl3.Controls.Add(Me.grdCOA_SALES)
        Me.LayoutControl3.Controls.Add(Me.grdCOA_INVENTORY)
        Me.LayoutControl3.Controls.Add(Me.grdCOA_COST)
        Me.LayoutControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl3.Name = "LayoutControl3"
        Me.LayoutControl3.Root = Me.LayoutControlGroup3
        Me.LayoutControl3.Size = New System.Drawing.Size(560, 253)
        Me.LayoutControl3.TabIndex = 0
        Me.LayoutControl3.Text = "LayoutControl3"
        '
        'grdCOA_SALES
        '
        Me.grdCOA_SALES.EnterMoveNextControl = True
        Me.grdCOA_SALES.Location = New System.Drawing.Point(117, 60)
        Me.grdCOA_SALES.MenuManager = Me.barManager
        Me.grdCOA_SALES.Name = "grdCOA_SALES"
        Me.grdCOA_SALES.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdCOA_SALES.Properties.NullText = ""
        Me.grdCOA_SALES.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdCOA_SALES.Properties.View = Me.grvCOASALES
        Me.grdCOA_SALES.Size = New System.Drawing.Size(431, 20)
        Me.grdCOA_SALES.StyleController = Me.LayoutControl3
        Me.grdCOA_SALES.TabIndex = 6
        '
        'grvCOASALES
        '
        Me.grvCOASALES.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn13, Me.GridColumn14})
        Me.grvCOASALES.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvCOASALES.Name = "grvCOASALES"
        Me.grvCOASALES.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvCOASALES.OptionsView.ShowAutoFilterRow = True
        Me.grvCOASALES.OptionsView.ShowGroupPanel = False
        '
        'GridColumn13
        '
        Me.GridColumn13.Caption = "Akun Code"
        Me.GridColumn13.FieldName = "KDCOA"
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.Visible = True
        Me.GridColumn13.VisibleIndex = 0
        '
        'GridColumn14
        '
        Me.GridColumn14.Caption = "Akun Name"
        Me.GridColumn14.FieldName = "NMCOA"
        Me.GridColumn14.Name = "GridColumn14"
        Me.GridColumn14.Visible = True
        Me.GridColumn14.VisibleIndex = 1
        '
        'grdCOA_INVENTORY
        '
        Me.grdCOA_INVENTORY.EnterMoveNextControl = True
        Me.grdCOA_INVENTORY.Location = New System.Drawing.Point(117, 36)
        Me.grdCOA_INVENTORY.MenuManager = Me.barManager
        Me.grdCOA_INVENTORY.Name = "grdCOA_INVENTORY"
        Me.grdCOA_INVENTORY.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdCOA_INVENTORY.Properties.NullText = ""
        Me.grdCOA_INVENTORY.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdCOA_INVENTORY.Properties.View = Me.grvCOAITEM
        Me.grdCOA_INVENTORY.Size = New System.Drawing.Size(431, 20)
        Me.grdCOA_INVENTORY.StyleController = Me.LayoutControl3
        Me.grdCOA_INVENTORY.TabIndex = 5
        '
        'grvCOAITEM
        '
        Me.grvCOAITEM.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn11, Me.GridColumn12})
        Me.grvCOAITEM.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvCOAITEM.Name = "grvCOAITEM"
        Me.grvCOAITEM.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvCOAITEM.OptionsView.ShowAutoFilterRow = True
        Me.grvCOAITEM.OptionsView.ShowGroupPanel = False
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Akun Code"
        Me.GridColumn11.FieldName = "KDCOA"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 0
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "Akun Name"
        Me.GridColumn12.FieldName = "NMCOA"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 1
        '
        'grdCOA_COST
        '
        Me.grdCOA_COST.EnterMoveNextControl = True
        Me.grdCOA_COST.Location = New System.Drawing.Point(117, 12)
        Me.grdCOA_COST.MenuManager = Me.barManager
        Me.grdCOA_COST.Name = "grdCOA_COST"
        Me.grdCOA_COST.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdCOA_COST.Properties.NullText = ""
        Me.grdCOA_COST.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdCOA_COST.Properties.View = Me.grvCOAHPP
        Me.grdCOA_COST.Size = New System.Drawing.Size(431, 20)
        Me.grdCOA_COST.StyleController = Me.LayoutControl3
        Me.grdCOA_COST.TabIndex = 4
        '
        'grvCOAHPP
        '
        Me.grvCOAHPP.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn9, Me.GridColumn10})
        Me.grvCOAHPP.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvCOAHPP.Name = "grvCOAHPP"
        Me.grvCOAHPP.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvCOAHPP.OptionsView.ShowAutoFilterRow = True
        Me.grvCOAHPP.OptionsView.ShowGroupPanel = False
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Akun Code"
        Me.GridColumn9.FieldName = "KDCOA"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 0
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Akun Name"
        Me.GridColumn10.FieldName = "NMCOA"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 1
        '
        'LayoutControlGroup3
        '
        Me.LayoutControlGroup3.CustomizationFormText = "LayoutControlGroup3"
        Me.LayoutControlGroup3.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup3.GroupBordersVisible = False
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lKDCOA_COST, Me.lKDCOA_INVENTORY, Me.lKDCOA_SALES})
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup3.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(560, 253)
        Me.LayoutControlGroup3.TextVisible = False
        '
        'lKDCOA_COST
        '
        Me.lKDCOA_COST.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKDCOA_COST.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKDCOA_COST.Control = Me.grdCOA_COST
        Me.lKDCOA_COST.CustomizationFormText = "Akun HPP :"
        Me.lKDCOA_COST.Location = New System.Drawing.Point(0, 0)
        Me.lKDCOA_COST.Name = "lKDCOA_COST"
        Me.lKDCOA_COST.Size = New System.Drawing.Size(540, 24)
        Me.lKDCOA_COST.Text = "Akun HPP * :"
        Me.lKDCOA_COST.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKDCOA_COST.TextSize = New System.Drawing.Size(100, 20)
        Me.lKDCOA_COST.TextToControlDistance = 5
        '
        'lKDCOA_INVENTORY
        '
        Me.lKDCOA_INVENTORY.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKDCOA_INVENTORY.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKDCOA_INVENTORY.Control = Me.grdCOA_INVENTORY
        Me.lKDCOA_INVENTORY.CustomizationFormText = "Akun Barang :"
        Me.lKDCOA_INVENTORY.Location = New System.Drawing.Point(0, 24)
        Me.lKDCOA_INVENTORY.Name = "lKDCOA_INVENTORY"
        Me.lKDCOA_INVENTORY.Size = New System.Drawing.Size(540, 24)
        Me.lKDCOA_INVENTORY.Text = "Akun Barang * :"
        Me.lKDCOA_INVENTORY.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKDCOA_INVENTORY.TextSize = New System.Drawing.Size(100, 20)
        Me.lKDCOA_INVENTORY.TextToControlDistance = 5
        '
        'lKDCOA_SALES
        '
        Me.lKDCOA_SALES.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKDCOA_SALES.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKDCOA_SALES.Control = Me.grdCOA_SALES
        Me.lKDCOA_SALES.CustomizationFormText = "Akun Penjualan :"
        Me.lKDCOA_SALES.Location = New System.Drawing.Point(0, 48)
        Me.lKDCOA_SALES.Name = "lKDCOA_SALES"
        Me.lKDCOA_SALES.Size = New System.Drawing.Size(540, 185)
        Me.lKDCOA_SALES.Text = "Akun Penjualan * :"
        Me.lKDCOA_SALES.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKDCOA_SALES.TextSize = New System.Drawing.Size(100, 20)
        Me.lKDCOA_SALES.TextToControlDistance = 5
        '
        'tab5
        '
        Me.tab5.Controls.Add(Me.picGAMBAR)
        Me.tab5.Name = "tab5"
        Me.tab5.Size = New System.Drawing.Size(560, 253)
        Me.tab5.Text = "Informasi Gambar"
        '
        'picGAMBAR
        '
        Me.picGAMBAR.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picGAMBAR.Location = New System.Drawing.Point(0, 0)
        Me.picGAMBAR.Name = "picGAMBAR"
        Me.picGAMBAR.Size = New System.Drawing.Size(560, 253)
        Me.picGAMBAR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picGAMBAR.TabIndex = 0
        Me.picGAMBAR.TabStop = False
        '
        'txtNMITEM2
        '
        Me.txtNMITEM2.EnterMoveNextControl = True
        Me.txtNMITEM2.Location = New System.Drawing.Point(117, 36)
        Me.txtNMITEM2.MenuManager = Me.barManager
        Me.txtNMITEM2.Name = "txtNMITEM2"
        Me.txtNMITEM2.Size = New System.Drawing.Size(301, 20)
        Me.txtNMITEM2.StyleController = Me.layoutControl
        Me.txtNMITEM2.TabIndex = 15
        '
        'chkISACTIVE
        '
        Me.chkISACTIVE.Location = New System.Drawing.Point(422, 12)
        Me.chkISACTIVE.MenuManager = Me.barManager
        Me.chkISACTIVE.Name = "chkISACTIVE"
        Me.chkISACTIVE.Properties.Caption = "Active?"
        Me.chkISACTIVE.Properties.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        Me.chkISACTIVE.Size = New System.Drawing.Size(156, 19)
        Me.chkISACTIVE.StyleController = Me.layoutControl
        Me.chkISACTIVE.TabIndex = 14
        Me.chkISACTIVE.TabStop = False
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem5, Me.LayoutControlItem4, Me.lNMITEM2, Me.lPRICE, Me.lCODE})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(590, 401)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.chkISACTIVE
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(410, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(160, 72)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.tabControl
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 96)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(570, 285)
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextVisible = False
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.LayoutControl2
        Me.LayoutControlItem4.CustomizationFormText = "LayoutControlItem4"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(570, 24)
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextVisible = False
        '
        'lNMITEM2
        '
        Me.lNMITEM2.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lNMITEM2.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lNMITEM2.Control = Me.txtNMITEM2
        Me.lNMITEM2.CustomizationFormText = "Nomer Barcode * :"
        Me.lNMITEM2.Location = New System.Drawing.Point(0, 24)
        Me.lNMITEM2.Name = "lNMITEM2"
        Me.lNMITEM2.Size = New System.Drawing.Size(410, 24)
        Me.lNMITEM2.Text = "Nama Barang #2 * :"
        Me.lNMITEM2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lNMITEM2.TextSize = New System.Drawing.Size(100, 20)
        Me.lNMITEM2.TextToControlDistance = 5
        '
        'lPRICE
        '
        Me.lPRICE.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lPRICE.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lPRICE.Control = Me.txtPRICE
        Me.lPRICE.Location = New System.Drawing.Point(0, 48)
        Me.lPRICE.Name = "lPRICE"
        Me.lPRICE.Size = New System.Drawing.Size(410, 24)
        Me.lPRICE.Text = "Price :"
        Me.lPRICE.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lPRICE.TextSize = New System.Drawing.Size(100, 20)
        Me.lPRICE.TextToControlDistance = 5
        '
        'lCODE
        '
        Me.lCODE.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lCODE.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lCODE.Control = Me.txtCODE
        Me.lCODE.Location = New System.Drawing.Point(0, 0)
        Me.lCODE.Name = "lCODE"
        Me.lCODE.Size = New System.Drawing.Size(410, 24)
        Me.lCODE.Text = "Code :"
        Me.lCODE.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lCODE.TextSize = New System.Drawing.Size(100, 20)
        Me.lCODE.TextToControlDistance = 5
        '
        'fileDialog
        '
        Me.fileDialog.Filter = "All files|*.*"
        '
        'frmItem
        '
        Me.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(590, 423)
        Me.Controls.Add(Me.layoutControl)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.Name = "frmItem"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.layoutControl.ResumeLayout(False)
        CType(Me.txtCODE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPRICE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tabControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabControl.ResumeLayout(False)
        Me.tab1.ResumeLayout(False)
        CType(Me.grdDetail_UOM, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuStrip.ResumeLayout(False)
        CType(Me.bindingSource_UOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvDetail_UOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdUOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvUOM, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tab3.ResumeLayout(False)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.grdITEM_L6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvItem_L6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdITEM_L5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvItem_L5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdITEM_L4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvItem_L4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdITEM_L3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvItem_L3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdITEM_L2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvItem_L2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdITEM_L1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvItem_L1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKDITEM_L1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKDITEM_L2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKDITEM_L3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKDITEM_L4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKDITEM_L5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKDITEM_L6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tab4.ResumeLayout(False)
        CType(Me.LayoutControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl3.ResumeLayout(False)
        CType(Me.grdCOA_SALES.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvCOASALES, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCOA_INVENTORY.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvCOAITEM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCOA_COST.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvCOAHPP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKDCOA_COST, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKDCOA_INVENTORY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKDCOA_SALES, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tab5.ResumeLayout(False)
        CType(Me.picGAMBAR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNMITEM2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkISACTIVE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lNMITEM2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lPRICE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lCODE, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

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
    Friend WithEvents chkISACTIVE As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtNMITEM2 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lNMITEM2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents bindingSource_UOM As System.Windows.Forms.BindingSource
    Friend WithEvents tabControl As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents tab1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tab3 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grdDetail_UOM As DevExpress.XtraGrid.GridControl
    Friend WithEvents grvDetail_UOM As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colKDUOM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdUOM As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents grvUOM As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colRATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grdITEM_L6 As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents grvItem_L6 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grdITEM_L5 As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents grvItem_L5 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grdITEM_L4 As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents grvItem_L4 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grdITEM_L3 As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents grvItem_L3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grdITEM_L2 As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents grvItem_L2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grdITEM_L1 As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents grvItem_L1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lKDITEM_L1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lKDITEM_L2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lKDITEM_L3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lKDITEM_L4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lKDITEM_L5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lKDITEM_L6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents tab4 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents LayoutControl3 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents grdCOA_SALES As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents grvCOASALES As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grdCOA_INVENTORY As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents grvCOAITEM As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grdCOA_COST As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents grvCOAHPP As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lKDCOA_COST As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lKDCOA_INVENTORY As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lKDCOA_SALES As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPRICEPURCHASESTANDARD As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPRICESALESSTANDARD As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mnuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents colMARGIN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents tab5 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents picGAMBAR As System.Windows.Forms.PictureBox
    Friend WithEvents fileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents txtPRICE As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lPRICE As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtCODE As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lCODE As DevExpress.XtraLayout.LayoutControlItem
End Class
