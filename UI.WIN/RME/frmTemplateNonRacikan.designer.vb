<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmTemplateNonRacikan
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
        Me.grdDetailResep = New DevExpress.XtraGrid.GridControl()
        Me.mnuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.grvDetailResep = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colKDITEM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdKDITEM = New DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit()
        Me.grvKDITEM = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn15 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn16 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn17 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colKDUOM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdUOM = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.grvUOM = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colQTY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colREMARKS_DOKTER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colKDSIGNA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdKDSIGNA = New DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit()
        Me.grvKDSIGNA = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colKDCARAPAKAI = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdCARAPAKAI = New DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit()
        Me.grvCARAPAKAI = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPRICE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colGRANDTOTAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.chk = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemMemoEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.txtREMARKS = New DevExpress.XtraEditors.TextEdit()
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
        Me.txtKDTEMPLATE = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.lKDLPK = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.layoutControl.SuspendLayout()
        CType(Me.grdDetailResep, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuStrip.SuspendLayout()
        CType(Me.BindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvDetailResep, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDITEM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvKDITEM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdUOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvUOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDSIGNA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvKDSIGNA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCARAPAKAI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvCARAPAKAI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemMemoEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtREMARKS.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtKDTEMPLATE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKDLPK, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'layoutControl
        '
        Me.layoutControl.Controls.Add(Me.grdDetailResep)
        Me.layoutControl.Controls.Add(Me.txtREMARKS)
        Me.layoutControl.Controls.Add(Me.txtKDTEMPLATE)
        Me.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutControl.Location = New System.Drawing.Point(0, 0)
        Me.layoutControl.Name = "layoutControl"
        Me.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(774, 238, 250, 350)
        Me.layoutControl.Root = Me.LayoutControlGroup1
        Me.layoutControl.Size = New System.Drawing.Size(666, 431)
        Me.layoutControl.TabIndex = 0
        Me.layoutControl.Text = "LayoutControl1"
        '
        'grdDetailResep
        '
        Me.grdDetailResep.ContextMenuStrip = Me.mnuStrip
        Me.grdDetailResep.DataSource = Me.BindingSource
        Me.grdDetailResep.Location = New System.Drawing.Point(12, 60)
        Me.grdDetailResep.MainView = Me.grvDetailResep
        Me.grdDetailResep.Name = "grdDetailResep"
        Me.grdDetailResep.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.grdCARAPAKAI, Me.grdUOM, Me.chk, Me.grdKDSIGNA, Me.RepositoryItemMemoEdit1, Me.grdKDITEM})
        Me.grdDetailResep.Size = New System.Drawing.Size(642, 359)
        Me.grdDetailResep.TabIndex = 6
        Me.grdDetailResep.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvDetailResep})
        '
        'mnuStrip
        '
        Me.mnuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DeleteToolStripMenuItem, Me.DeleteAllToolStripMenuItem})
        Me.mnuStrip.Name = "mnuStrip"
        Me.mnuStrip.Size = New System.Drawing.Size(125, 48)
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(124, 22)
        Me.DeleteToolStripMenuItem.Text = "Delete"
        '
        'DeleteAllToolStripMenuItem
        '
        Me.DeleteAllToolStripMenuItem.Name = "DeleteAllToolStripMenuItem"
        Me.DeleteAllToolStripMenuItem.Size = New System.Drawing.Size(124, 22)
        Me.DeleteAllToolStripMenuItem.Text = "Delete All"
        '
        'BindingSource
        '
        Me.BindingSource.DataSource = GetType(DataAccess.R_CPPT_NONRACIKAN)
        '
        'grvDetailResep
        '
        Me.grvDetailResep.Appearance.Row.Options.UseFont = True
        Me.grvDetailResep.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colKDITEM, Me.colKDUOM, Me.colQTY, Me.colREMARKS_DOKTER, Me.colKDSIGNA, Me.colKDCARAPAKAI, Me.colPRICE, Me.colGRANDTOTAL})
        Me.grvDetailResep.GridControl = Me.grdDetailResep
        Me.grvDetailResep.Name = "grvDetailResep"
        Me.grvDetailResep.OptionsDetail.EnableMasterViewMode = False
        Me.grvDetailResep.OptionsNavigation.AutoFocusNewRow = True
        Me.grvDetailResep.OptionsNavigation.EnterMoveNextColumn = True
        Me.grvDetailResep.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.grvDetailResep.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.grvDetailResep.OptionsView.ShowGroupPanel = False
        '
        'colKDITEM
        '
        Me.colKDITEM.Caption = "Obat"
        Me.colKDITEM.ColumnEdit = Me.grdKDITEM
        Me.colKDITEM.FieldName = "KDITEM"
        Me.colKDITEM.Name = "colKDITEM"
        Me.colKDITEM.Visible = True
        Me.colKDITEM.VisibleIndex = 0
        Me.colKDITEM.Width = 167
        '
        'grdKDITEM
        '
        Me.grdKDITEM.AutoHeight = False
        Me.grdKDITEM.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDITEM.Name = "grdKDITEM"
        Me.grdKDITEM.NullText = ""
        Me.grdKDITEM.PopupFormSize = New System.Drawing.Size(950, 500)
        Me.grdKDITEM.View = Me.grvKDITEM
        '
        'grvKDITEM
        '
        Me.grvKDITEM.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn3, Me.GridColumn15, Me.GridColumn16, Me.GridColumn17})
        Me.grvKDITEM.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvKDITEM.Name = "grvKDITEM"
        Me.grvKDITEM.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvKDITEM.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Generik"
        Me.GridColumn1.FieldName = "NMITEM1"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 249
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Dagang"
        Me.GridColumn3.FieldName = "NMITEM2"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 1
        Me.GridColumn3.Width = 359
        '
        'GridColumn15
        '
        Me.GridColumn15.Caption = "Satuan"
        Me.GridColumn15.FieldName = "SATUAN"
        Me.GridColumn15.Name = "GridColumn15"
        Me.GridColumn15.Visible = True
        Me.GridColumn15.VisibleIndex = 2
        Me.GridColumn15.Width = 139
        '
        'GridColumn16
        '
        Me.GridColumn16.Caption = "Harga"
        Me.GridColumn16.DisplayFormat.FormatString = "{0:n0}"
        Me.GridColumn16.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn16.FieldName = "HARGA"
        Me.GridColumn16.Name = "GridColumn16"
        Me.GridColumn16.Visible = True
        Me.GridColumn16.VisibleIndex = 3
        '
        'GridColumn17
        '
        Me.GridColumn17.Caption = "Stok"
        Me.GridColumn17.DisplayFormat.FormatString = "{0:n0}"
        Me.GridColumn17.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn17.FieldName = "STOK"
        Me.GridColumn17.Name = "GridColumn17"
        Me.GridColumn17.Visible = True
        Me.GridColumn17.VisibleIndex = 4
        '
        'colKDUOM
        '
        Me.colKDUOM.Caption = "Satuan"
        Me.colKDUOM.ColumnEdit = Me.grdUOM
        Me.colKDUOM.FieldName = "KDUOM"
        Me.colKDUOM.Name = "colKDUOM"
        Me.colKDUOM.OptionsColumn.AllowEdit = False
        Me.colKDUOM.OptionsColumn.AllowFocus = False
        Me.colKDUOM.OptionsColumn.ReadOnly = True
        Me.colKDUOM.OptionsColumn.TabStop = False
        Me.colKDUOM.Visible = True
        Me.colKDUOM.VisibleIndex = 1
        Me.colKDUOM.Width = 70
        '
        'grdUOM
        '
        Me.grdUOM.AutoHeight = False
        Me.grdUOM.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdUOM.Name = "grdUOM"
        Me.grdUOM.NullText = ""
        Me.grdUOM.View = Me.grvUOM
        '
        'grvUOM
        '
        Me.grvUOM.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn6})
        Me.grvUOM.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvUOM.Name = "grvUOM"
        Me.grvUOM.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvUOM.OptionsView.ShowGroupPanel = False
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Tampilan Nama"
        Me.GridColumn6.FieldName = "MEMO"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 0
        '
        'colQTY
        '
        Me.colQTY.Caption = "Jumlah"
        Me.colQTY.DisplayFormat.FormatString = "{0:n0}"
        Me.colQTY.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colQTY.FieldName = "JUMLAH"
        Me.colQTY.Name = "colQTY"
        Me.colQTY.Visible = True
        Me.colQTY.VisibleIndex = 2
        Me.colQTY.Width = 70
        '
        'colREMARKS_DOKTER
        '
        Me.colREMARKS_DOKTER.Caption = "Cara Pakai / Signa"
        Me.colREMARKS_DOKTER.FieldName = "REMARKS_DOKTER"
        Me.colREMARKS_DOKTER.Name = "colREMARKS_DOKTER"
        Me.colREMARKS_DOKTER.Visible = True
        Me.colREMARKS_DOKTER.VisibleIndex = 3
        Me.colREMARKS_DOKTER.Width = 70
        '
        'colKDSIGNA
        '
        Me.colKDSIGNA.Caption = "Keterangan 1"
        Me.colKDSIGNA.ColumnEdit = Me.grdKDSIGNA
        Me.colKDSIGNA.FieldName = "KDSIGNA"
        Me.colKDSIGNA.Name = "colKDSIGNA"
        Me.colKDSIGNA.OptionsColumn.TabStop = False
        Me.colKDSIGNA.Visible = True
        Me.colKDSIGNA.VisibleIndex = 4
        Me.colKDSIGNA.Width = 70
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
        Me.grvKDSIGNA.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn8})
        Me.grvKDSIGNA.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvKDSIGNA.Name = "grvKDSIGNA"
        Me.grvKDSIGNA.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvKDSIGNA.OptionsView.ShowGroupPanel = False
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Tampilan Nama"
        Me.GridColumn8.FieldName = "MEMO"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 0
        '
        'colKDCARAPAKAI
        '
        Me.colKDCARAPAKAI.Caption = "Keterangan 2"
        Me.colKDCARAPAKAI.ColumnEdit = Me.grdCARAPAKAI
        Me.colKDCARAPAKAI.FieldName = "KDCARAPAKAI"
        Me.colKDCARAPAKAI.Name = "colKDCARAPAKAI"
        Me.colKDCARAPAKAI.OptionsColumn.TabStop = False
        Me.colKDCARAPAKAI.Visible = True
        Me.colKDCARAPAKAI.VisibleIndex = 5
        Me.colKDCARAPAKAI.Width = 70
        '
        'grdCARAPAKAI
        '
        Me.grdCARAPAKAI.AutoHeight = False
        Me.grdCARAPAKAI.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdCARAPAKAI.Name = "grdCARAPAKAI"
        Me.grdCARAPAKAI.NullText = ""
        Me.grdCARAPAKAI.View = Me.grvCARAPAKAI
        '
        'grvCARAPAKAI
        '
        Me.grvCARAPAKAI.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn5})
        Me.grvCARAPAKAI.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvCARAPAKAI.Name = "grvCARAPAKAI"
        Me.grvCARAPAKAI.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvCARAPAKAI.OptionsView.ShowGroupPanel = False
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Keterangan"
        Me.GridColumn5.FieldName = "MEMO"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 0
        '
        'colPRICE
        '
        Me.colPRICE.Caption = "Harga"
        Me.colPRICE.DisplayFormat.FormatString = "{0:n0}"
        Me.colPRICE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colPRICE.FieldName = "HARGA"
        Me.colPRICE.Name = "colPRICE"
        Me.colPRICE.OptionsColumn.AllowEdit = False
        Me.colPRICE.OptionsColumn.AllowFocus = False
        Me.colPRICE.OptionsColumn.ReadOnly = True
        Me.colPRICE.OptionsColumn.TabStop = False
        Me.colPRICE.Width = 70
        '
        'colGRANDTOTAL
        '
        Me.colGRANDTOTAL.Caption = "Sub Total"
        Me.colGRANDTOTAL.DisplayFormat.FormatString = "{0:n0}"
        Me.colGRANDTOTAL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colGRANDTOTAL.FieldName = "TOTAL"
        Me.colGRANDTOTAL.Name = "colGRANDTOTAL"
        Me.colGRANDTOTAL.OptionsColumn.AllowEdit = False
        Me.colGRANDTOTAL.OptionsColumn.AllowFocus = False
        Me.colGRANDTOTAL.OptionsColumn.ReadOnly = True
        Me.colGRANDTOTAL.OptionsColumn.TabStop = False
        Me.colGRANDTOTAL.Width = 84
        '
        'chk
        '
        Me.chk.AutoHeight = False
        Me.chk.Name = "chk"
        Me.chk.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'RepositoryItemMemoEdit1
        '
        Me.RepositoryItemMemoEdit1.Name = "RepositoryItemMemoEdit1"
        '
        'txtREMARKS
        '
        Me.txtREMARKS.Location = New System.Drawing.Point(117, 36)
        Me.txtREMARKS.MenuManager = Me.barManager
        Me.txtREMARKS.Name = "txtREMARKS"
        Me.txtREMARKS.Size = New System.Drawing.Size(537, 20)
        Me.txtREMARKS.StyleController = Me.layoutControl
        Me.txtREMARKS.TabIndex = 48
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
        Me.barTop.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, False, Me.btnSaveNew, False), New DevExpress.XtraBars.LinkPersistInfo(Me.btnSaveClose), New DevExpress.XtraBars.LinkPersistInfo(Me.btnClose)})
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
        Me.barDockControlTop.Size = New System.Drawing.Size(666, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 431)
        Me.barDockControlBottom.Size = New System.Drawing.Size(666, 22)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 431)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(666, 0)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 431)
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
        'txtKDTEMPLATE
        '
        Me.txtKDTEMPLATE.Location = New System.Drawing.Point(117, 12)
        Me.txtKDTEMPLATE.MenuManager = Me.barManager
        Me.txtKDTEMPLATE.Name = "txtKDTEMPLATE"
        Me.txtKDTEMPLATE.Properties.ReadOnly = True
        Me.txtKDTEMPLATE.Size = New System.Drawing.Size(537, 20)
        Me.txtKDTEMPLATE.StyleController = Me.layoutControl
        Me.txtKDTEMPLATE.TabIndex = 47
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lKDLPK, Me.LayoutControlItem1, Me.LayoutControlItem2})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(666, 431)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'lKDLPK
        '
        Me.lKDLPK.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKDLPK.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKDLPK.Control = Me.txtKDTEMPLATE
        Me.lKDLPK.Location = New System.Drawing.Point(0, 0)
        Me.lKDLPK.Name = "lKDLPK"
        Me.lKDLPK.Size = New System.Drawing.Size(646, 24)
        Me.lKDLPK.Text = "Kode :"
        Me.lKDLPK.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKDLPK.TextSize = New System.Drawing.Size(100, 20)
        Me.lKDLPK.TextToControlDistance = 5
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem1.Control = Me.txtREMARKS
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(646, 24)
        Me.LayoutControlItem1.Text = "Judul :"
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(100, 20)
        Me.LayoutControlItem1.TextToControlDistance = 5
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.grdDetailResep
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(646, 363)
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'frmTemplateNonRacikan
        '
        Me.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(666, 453)
        Me.Controls.Add(Me.layoutControl)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.Name = "frmTemplateNonRacikan"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.layoutControl.ResumeLayout(False)
        CType(Me.grdDetailResep, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuStrip.ResumeLayout(False)
        CType(Me.BindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvDetailResep, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDITEM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvKDITEM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdUOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvUOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDSIGNA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvKDSIGNA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCARAPAKAI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvCARAPAKAI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemMemoEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtREMARKS.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtKDTEMPLATE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKDLPK, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents txtKDTEMPLATE As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lKDLPK As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtREMARKS As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grdDetailResep As DevExpress.XtraGrid.GridControl
    Friend WithEvents grvDetailResep As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colKDITEM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdKDITEM As DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit
    Friend WithEvents grvKDITEM As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn17 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colKDUOM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdUOM As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents grvUOM As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colQTY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colREMARKS_DOKTER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colKDSIGNA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdKDSIGNA As DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit
    Friend WithEvents grvKDSIGNA As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colKDCARAPAKAI As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdCARAPAKAI As DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit
    Friend WithEvents grvCARAPAKAI As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPRICE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colGRANDTOTAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents chk As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemMemoEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents mnuStrip As ContextMenuStrip
    Friend WithEvents DeleteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DeleteAllToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BindingSource As BindingSource
End Class
