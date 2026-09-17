<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDiet
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
        Me.deDateFrom = New DevExpress.XtraEditors.DateEdit()
        Me.deDateTo = New DevExpress.XtraEditors.DateEdit()
        Me.cboKATEGORI = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.barManager = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barTop = New DevExpress.XtraBars.Bar()
        Me.btnSaveNew = New DevExpress.XtraBars.BarButtonItem()
        Me.btnSaveClose = New DevExpress.XtraBars.BarButtonItem()
        Me.btnReload = New DevExpress.XtraBars.BarButtonItem()
        Me.btnClose = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.progressBarSave = New DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar()
        Me.progressSave = New DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar()
        Me.chkPemetaan = New DevExpress.XtraEditors.CheckEdit()
        Me.deDATE = New DevExpress.XtraEditors.DateEdit()
        Me.tabControl = New DevExpress.XtraTab.XtraTabControl()
        Me.tab1 = New DevExpress.XtraTab.XtraTabPage()
        Me.grdDetail = New DevExpress.XtraGrid.GridControl()
        Me.mnuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CetakEtiketToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.grvDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colKDIDENTITAS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colNAMAPASIEN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colKDCUSTOMER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colBED = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colRUANGAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colKDDOCTOR = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdKDDOKTER = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.grvKDDOKTER = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colKDBENTUKMAKANAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdKDBENTUKMAKANAN = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.grVKDBENTUKMAKANAN = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colKDJENISDIET = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdKDJENISDIET = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.grvKDJENISDIET = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colWAKTUMAKAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colBATASMAKAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colREMARKS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtREMARKS = New DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit()
        Me.tab2 = New DevExpress.XtraTab.XtraTabPage()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.txtMEMO = New DevExpress.XtraEditors.MemoEdit()
        Me.s = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.txtKDDIET = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.lKDADJUSTMENT = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lDATE = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lDATEFROM = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lDATETO = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.layoutControl.SuspendLayout()
        CType(Me.deDateFrom.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDateFrom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDateTo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDateTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboKATEGORI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPemetaan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATE.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tabControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabControl.SuspendLayout()
        Me.tab1.SuspendLayout()
        CType(Me.grdDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuStrip.SuspendLayout()
        CType(Me.BindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDDOKTER, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvKDDOKTER, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDBENTUKMAKANAN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grVKDBENTUKMAKANAN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDJENISDIET, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvKDJENISDIET, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtREMARKS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tab2.SuspendLayout()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.txtMEMO.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.s, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtKDDIET.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKDADJUSTMENT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lDATE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lDATEFROM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lDATETO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'layoutControl
        '
        Me.layoutControl.Controls.Add(Me.deDateFrom)
        Me.layoutControl.Controls.Add(Me.deDateTo)
        Me.layoutControl.Controls.Add(Me.cboKATEGORI)
        Me.layoutControl.Controls.Add(Me.chkPemetaan)
        Me.layoutControl.Controls.Add(Me.deDATE)
        Me.layoutControl.Controls.Add(Me.tabControl)
        Me.layoutControl.Controls.Add(Me.txtKDDIET)
        Me.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutControl.Location = New System.Drawing.Point(0, 0)
        Me.layoutControl.Name = "layoutControl"
        Me.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(939, 191, 250, 350)
        Me.layoutControl.Root = Me.LayoutControlGroup1
        Me.layoutControl.Size = New System.Drawing.Size(790, 549)
        Me.layoutControl.TabIndex = 0
        Me.layoutControl.Text = "LayoutControl1"
        '
        'deDateFrom
        '
        Me.deDateFrom.EditValue = Nothing
        Me.deDateFrom.EnterMoveNextControl = True
        Me.deDateFrom.Location = New System.Drawing.Point(539, 35)
        Me.deDateFrom.Name = "deDateFrom"
        Me.deDateFrom.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deDateFrom.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.deDateFrom.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.deDateFrom.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.deDateFrom.Size = New System.Drawing.Size(239, 20)
        Me.deDateFrom.StyleController = Me.layoutControl
        Me.deDateFrom.TabIndex = 75
        '
        'deDateTo
        '
        Me.deDateTo.EditValue = Nothing
        Me.deDateTo.EnterMoveNextControl = True
        Me.deDateTo.Location = New System.Drawing.Point(539, 59)
        Me.deDateTo.Name = "deDateTo"
        Me.deDateTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deDateTo.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.deDateTo.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.deDateTo.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.deDateTo.Size = New System.Drawing.Size(239, 20)
        Me.deDateTo.StyleController = Me.layoutControl
        Me.deDateTo.TabIndex = 76
        Me.deDateTo.Visible = False
        '
        'cboKATEGORI
        '
        Me.cboKATEGORI.Location = New System.Drawing.Point(117, 60)
        Me.cboKATEGORI.MenuManager = Me.barManager
        Me.cboKATEGORI.Name = "cboKATEGORI"
        Me.cboKATEGORI.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboKATEGORI.Properties.Items.AddRange(New Object() {"Makan Pagi", "Makan Siang", "Makan Sore"})
        Me.cboKATEGORI.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cboKATEGORI.Size = New System.Drawing.Size(303, 20)
        Me.cboKATEGORI.StyleController = Me.layoutControl
        Me.cboKATEGORI.TabIndex = 22
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
        Me.barManager.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.btnSaveNew, Me.btnClose, Me.btnSaveClose, Me.btnReload})
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
        Me.barTop.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.btnSaveNew), New DevExpress.XtraBars.LinkPersistInfo(Me.btnSaveClose), New DevExpress.XtraBars.LinkPersistInfo(Me.btnReload), New DevExpress.XtraBars.LinkPersistInfo(Me.btnClose)})
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
        'btnReload
        '
        Me.btnReload.Caption = "F5 - Reload"
        Me.btnReload.Id = 8
        Me.btnReload.Name = "btnReload"
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
        'chkPemetaan
        '
        Me.chkPemetaan.Location = New System.Drawing.Point(434, 12)
        Me.chkPemetaan.MenuManager = Me.barManager
        Me.chkPemetaan.Name = "chkPemetaan"
        Me.chkPemetaan.Properties.Caption = "Pemetaan"
        Me.chkPemetaan.Size = New System.Drawing.Size(344, 19)
        Me.chkPemetaan.StyleController = Me.layoutControl
        Me.chkPemetaan.TabIndex = 4
        '
        'deDATE
        '
        Me.deDATE.EditValue = Nothing
        Me.deDATE.EnterMoveNextControl = True
        Me.deDATE.Location = New System.Drawing.Point(117, 36)
        Me.deDATE.MenuManager = Me.barManager
        Me.deDATE.Name = "deDATE"
        Me.deDATE.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deDATE.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.deDATE.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.deDATE.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.deDATE.Size = New System.Drawing.Size(303, 20)
        Me.deDATE.StyleController = Me.layoutControl
        Me.deDATE.TabIndex = 20
        '
        'tabControl
        '
        Me.tabControl.Location = New System.Drawing.Point(12, 84)
        Me.tabControl.Name = "tabControl"
        Me.tabControl.SelectedTabPage = Me.tab1
        Me.tabControl.Size = New System.Drawing.Size(766, 406)
        Me.tabControl.TabIndex = 18
        Me.tabControl.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tab1, Me.tab2})
        '
        'tab1
        '
        Me.tab1.Controls.Add(Me.grdDetail)
        Me.tab1.Name = "tab1"
        Me.tab1.Size = New System.Drawing.Size(760, 378)
        Me.tab1.Text = "Detail Information"
        '
        'grdDetail
        '
        Me.grdDetail.ContextMenuStrip = Me.mnuStrip
        Me.grdDetail.DataSource = Me.BindingSource
        Me.grdDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdDetail.Location = New System.Drawing.Point(0, 0)
        Me.grdDetail.MainView = Me.grvDetail
        Me.grdDetail.MenuManager = Me.barManager
        Me.grdDetail.Name = "grdDetail"
        Me.grdDetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.txtREMARKS, Me.grdKDDOKTER, Me.grdKDBENTUKMAKANAN, Me.grdKDJENISDIET})
        Me.grdDetail.Size = New System.Drawing.Size(760, 378)
        Me.grdDetail.TabIndex = 18
        Me.grdDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvDetail})
        '
        'mnuStrip
        '
        Me.mnuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DeleteToolStripMenuItem, Me.CetakEtiketToolStripMenuItem})
        Me.mnuStrip.Name = "mnuStrip"
        Me.mnuStrip.Size = New System.Drawing.Size(137, 48)
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(136, 22)
        Me.DeleteToolStripMenuItem.Text = "Delete"
        '
        'CetakEtiketToolStripMenuItem
        '
        Me.CetakEtiketToolStripMenuItem.Name = "CetakEtiketToolStripMenuItem"
        Me.CetakEtiketToolStripMenuItem.Size = New System.Drawing.Size(136, 22)
        Me.CetakEtiketToolStripMenuItem.Text = "Cetak Etiket"
        '
        'BindingSource
        '
        Me.BindingSource.DataSource = GetType(DataAccess.S_DIET_D)
        '
        'grvDetail
        '
        Me.grvDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colKDIDENTITAS, Me.colNAMAPASIEN, Me.colKDCUSTOMER, Me.colBED, Me.colRUANGAN, Me.colKDDOCTOR, Me.colKDBENTUKMAKANAN, Me.colKDJENISDIET, Me.colWAKTUMAKAN, Me.colBATASMAKAN, Me.colREMARKS})
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
        'colKDIDENTITAS
        '
        Me.colKDIDENTITAS.Caption = "Kode Identitas"
        Me.colKDIDENTITAS.FieldName = "KDIDENTITAS"
        Me.colKDIDENTITAS.Name = "colKDIDENTITAS"
        Me.colKDIDENTITAS.OptionsColumn.AllowEdit = False
        Me.colKDIDENTITAS.OptionsColumn.AllowFocus = False
        Me.colKDIDENTITAS.OptionsColumn.ReadOnly = True
        Me.colKDIDENTITAS.OptionsColumn.TabStop = False
        Me.colKDIDENTITAS.Visible = True
        Me.colKDIDENTITAS.VisibleIndex = 0
        '
        'colNAMAPASIEN
        '
        Me.colNAMAPASIEN.Caption = "Nama"
        Me.colNAMAPASIEN.FieldName = "NAMAPASIEN"
        Me.colNAMAPASIEN.Name = "colNAMAPASIEN"
        Me.colNAMAPASIEN.Visible = True
        Me.colNAMAPASIEN.VisibleIndex = 1
        '
        'colKDCUSTOMER
        '
        Me.colKDCUSTOMER.Caption = "No RM"
        Me.colKDCUSTOMER.FieldName = "KDCUSTOMER"
        Me.colKDCUSTOMER.Name = "colKDCUSTOMER"
        Me.colKDCUSTOMER.Visible = True
        Me.colKDCUSTOMER.VisibleIndex = 2
        '
        'colBED
        '
        Me.colBED.Caption = "BED"
        Me.colBED.FieldName = "BED"
        Me.colBED.Name = "colBED"
        Me.colBED.OptionsColumn.AllowEdit = False
        Me.colBED.OptionsColumn.AllowFocus = False
        Me.colBED.OptionsColumn.ReadOnly = True
        Me.colBED.OptionsColumn.TabStop = False
        Me.colBED.Visible = True
        Me.colBED.VisibleIndex = 3
        '
        'colRUANGAN
        '
        Me.colRUANGAN.Caption = "Gedung"
        Me.colRUANGAN.FieldName = "RUANGAN"
        Me.colRUANGAN.Name = "colRUANGAN"
        Me.colRUANGAN.OptionsColumn.AllowEdit = False
        Me.colRUANGAN.OptionsColumn.AllowFocus = False
        Me.colRUANGAN.OptionsColumn.ReadOnly = True
        Me.colRUANGAN.OptionsColumn.TabStop = False
        Me.colRUANGAN.Visible = True
        Me.colRUANGAN.VisibleIndex = 4
        '
        'colKDDOCTOR
        '
        Me.colKDDOCTOR.Caption = "Dokter"
        Me.colKDDOCTOR.ColumnEdit = Me.grdKDDOKTER
        Me.colKDDOCTOR.FieldName = "KDDOCTOR"
        Me.colKDDOCTOR.Name = "colKDDOCTOR"
        Me.colKDDOCTOR.Visible = True
        Me.colKDDOCTOR.VisibleIndex = 5
        Me.colKDDOCTOR.Width = 88
        '
        'grdKDDOKTER
        '
        Me.grdKDDOKTER.AutoHeight = False
        Me.grdKDDOKTER.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDDOKTER.Name = "grdKDDOKTER"
        Me.grdKDDOKTER.NullText = ""
        Me.grdKDDOKTER.View = Me.grvKDDOKTER
        '
        'grvKDDOKTER
        '
        Me.grvKDDOKTER.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1})
        Me.grvKDDOKTER.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvKDDOKTER.Name = "grvKDDOKTER"
        Me.grvKDDOKTER.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvKDDOKTER.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Nama"
        Me.GridColumn1.FieldName = "NAME_DISPLAY"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'colKDBENTUKMAKANAN
        '
        Me.colKDBENTUKMAKANAN.Caption = "Bentuk Makanan"
        Me.colKDBENTUKMAKANAN.ColumnEdit = Me.grdKDBENTUKMAKANAN
        Me.colKDBENTUKMAKANAN.FieldName = "KDBENTUKMAKANAN"
        Me.colKDBENTUKMAKANAN.Name = "colKDBENTUKMAKANAN"
        Me.colKDBENTUKMAKANAN.Visible = True
        Me.colKDBENTUKMAKANAN.VisibleIndex = 6
        Me.colKDBENTUKMAKANAN.Width = 98
        '
        'grdKDBENTUKMAKANAN
        '
        Me.grdKDBENTUKMAKANAN.AutoHeight = False
        Me.grdKDBENTUKMAKANAN.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDBENTUKMAKANAN.Name = "grdKDBENTUKMAKANAN"
        Me.grdKDBENTUKMAKANAN.NullText = ""
        Me.grdKDBENTUKMAKANAN.View = Me.grVKDBENTUKMAKANAN
        '
        'grVKDBENTUKMAKANAN
        '
        Me.grVKDBENTUKMAKANAN.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2})
        Me.grVKDBENTUKMAKANAN.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grVKDBENTUKMAKANAN.Name = "grVKDBENTUKMAKANAN"
        Me.grVKDBENTUKMAKANAN.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grVKDBENTUKMAKANAN.OptionsView.ShowGroupPanel = False
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nama"
        Me.GridColumn2.FieldName = "MEMO"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        '
        'colKDJENISDIET
        '
        Me.colKDJENISDIET.Caption = "Jenis Diet"
        Me.colKDJENISDIET.ColumnEdit = Me.grdKDJENISDIET
        Me.colKDJENISDIET.FieldName = "KDJENISDIET"
        Me.colKDJENISDIET.Name = "colKDJENISDIET"
        Me.colKDJENISDIET.Visible = True
        Me.colKDJENISDIET.VisibleIndex = 7
        Me.colKDJENISDIET.Width = 50
        '
        'grdKDJENISDIET
        '
        Me.grdKDJENISDIET.AutoHeight = False
        Me.grdKDJENISDIET.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDJENISDIET.Name = "grdKDJENISDIET"
        Me.grdKDJENISDIET.NullText = ""
        Me.grdKDJENISDIET.View = Me.grvKDJENISDIET
        '
        'grvKDJENISDIET
        '
        Me.grvKDJENISDIET.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn4})
        Me.grvKDJENISDIET.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvKDJENISDIET.Name = "grvKDJENISDIET"
        Me.grvKDJENISDIET.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvKDJENISDIET.OptionsView.ShowGroupPanel = False
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Nama"
        Me.GridColumn4.FieldName = "MEMO"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 0
        '
        'colWAKTUMAKAN
        '
        Me.colWAKTUMAKAN.Caption = "Waktu Makan"
        Me.colWAKTUMAKAN.FieldName = "WAKTUMAKAN"
        Me.colWAKTUMAKAN.Name = "colWAKTUMAKAN"
        Me.colWAKTUMAKAN.Visible = True
        Me.colWAKTUMAKAN.VisibleIndex = 8
        '
        'colBATASMAKAN
        '
        Me.colBATASMAKAN.Caption = "Kelas / Ruangan / Bed"
        Me.colBATASMAKAN.FieldName = "BATASMAKAN"
        Me.colBATASMAKAN.Name = "colBATASMAKAN"
        Me.colBATASMAKAN.Visible = True
        Me.colBATASMAKAN.VisibleIndex = 9
        '
        'colREMARKS
        '
        Me.colREMARKS.Caption = "Catatan"
        Me.colREMARKS.ColumnEdit = Me.txtREMARKS
        Me.colREMARKS.FieldName = "REMARKS"
        Me.colREMARKS.Name = "colREMARKS"
        Me.colREMARKS.Visible = True
        Me.colREMARKS.VisibleIndex = 10
        Me.colREMARKS.Width = 50
        '
        'txtREMARKS
        '
        Me.txtREMARKS.AutoHeight = False
        Me.txtREMARKS.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtREMARKS.Name = "txtREMARKS"
        '
        'tab2
        '
        Me.tab2.Controls.Add(Me.LayoutControl1)
        Me.tab2.Name = "tab2"
        Me.tab2.Size = New System.Drawing.Size(760, 378)
        Me.tab2.Text = "Memo Information"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.txtMEMO)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.s
        Me.LayoutControl1.Size = New System.Drawing.Size(760, 378)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'txtMEMO
        '
        Me.txtMEMO.EnterMoveNextControl = True
        Me.txtMEMO.Location = New System.Drawing.Point(12, 12)
        Me.txtMEMO.MenuManager = Me.barManager
        Me.txtMEMO.Name = "txtMEMO"
        Me.txtMEMO.Size = New System.Drawing.Size(736, 354)
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
        Me.s.Size = New System.Drawing.Size(760, 378)
        Me.s.TextVisible = False
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.txtMEMO
        Me.LayoutControlItem7.CustomizationFormText = "LayoutControlItem7"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(740, 358)
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem7.TextVisible = False
        '
        'txtKDDIET
        '
        Me.txtKDDIET.EditValue = ""
        Me.txtKDDIET.EnterMoveNextControl = True
        Me.txtKDDIET.Location = New System.Drawing.Point(117, 12)
        Me.txtKDDIET.Name = "txtKDDIET"
        Me.txtKDDIET.Properties.ReadOnly = True
        Me.txtKDDIET.Size = New System.Drawing.Size(303, 20)
        Me.txtKDDIET.StyleController = Me.layoutControl
        Me.txtKDDIET.TabIndex = 9
        Me.txtKDDIET.TabStop = False
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lKDADJUSTMENT, Me.LayoutControlItem5, Me.lDATE, Me.EmptySpaceItem2, Me.LayoutControlItem1, Me.LayoutControlItem2, Me.lDATEFROM, Me.lDATETO, Me.EmptySpaceItem1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(790, 549)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'lKDADJUSTMENT
        '
        Me.lKDADJUSTMENT.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKDADJUSTMENT.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKDADJUSTMENT.Control = Me.txtKDDIET
        Me.lKDADJUSTMENT.CustomizationFormText = "Display Name * :"
        Me.lKDADJUSTMENT.Location = New System.Drawing.Point(0, 0)
        Me.lKDADJUSTMENT.Name = "lKDADJUSTMENT"
        Me.lKDADJUSTMENT.Size = New System.Drawing.Size(412, 24)
        Me.lKDADJUSTMENT.Text = "Number * :"
        Me.lKDADJUSTMENT.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKDADJUSTMENT.TextSize = New System.Drawing.Size(100, 20)
        Me.lKDADJUSTMENT.TextToControlDistance = 5
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.tabControl
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(770, 410)
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
        Me.lDATE.Size = New System.Drawing.Size(412, 24)
        Me.lDATE.Text = "Tanggal * :"
        Me.lDATE.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lDATE.TextSize = New System.Drawing.Size(100, 20)
        Me.lDATE.TextToControlDistance = 5
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.AllowHotTrack = False
        Me.EmptySpaceItem2.CustomizationFormText = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(0, 482)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(770, 47)
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem1.Control = Me.cboKATEGORI
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(412, 24)
        Me.LayoutControlItem1.Text = "Waktu Makan * :"
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(100, 20)
        Me.LayoutControlItem1.TextToControlDistance = 5
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.chkPemetaan
        Me.LayoutControlItem2.Location = New System.Drawing.Point(422, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(348, 23)
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'lDATEFROM
        '
        Me.lDATEFROM.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lDATEFROM.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lDATEFROM.Control = Me.deDateFrom
        Me.lDATEFROM.Location = New System.Drawing.Point(422, 23)
        Me.lDATEFROM.Name = "lDATEFROM"
        Me.lDATEFROM.Size = New System.Drawing.Size(348, 24)
        Me.lDATEFROM.Text = "Dari Tgl :"
        Me.lDATEFROM.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lDATEFROM.TextSize = New System.Drawing.Size(100, 20)
        Me.lDATEFROM.TextToControlDistance = 5
        '
        'lDATETO
        '
        Me.lDATETO.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lDATETO.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lDATETO.Control = Me.deDateTo
        Me.lDATETO.Location = New System.Drawing.Point(422, 47)
        Me.lDATETO.Name = "lDATETO"
        Me.lDATETO.Size = New System.Drawing.Size(348, 25)
        Me.lDATETO.Text = "Sampai Tgl :"
        Me.lDATETO.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lDATETO.TextSize = New System.Drawing.Size(100, 20)
        Me.lDATETO.TextToControlDistance = 5
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(412, 0)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(10, 72)
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
        'frmDiet
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
        Me.Name = "frmDiet"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.layoutControl.ResumeLayout(False)
        CType(Me.deDateFrom.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDateFrom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDateTo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDateTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboKATEGORI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPemetaan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATE.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tabControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabControl.ResumeLayout(False)
        Me.tab1.ResumeLayout(False)
        CType(Me.grdDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuStrip.ResumeLayout(False)
        CType(Me.BindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDDOKTER, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvKDDOKTER, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDBENTUKMAKANAN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grVKDBENTUKMAKANAN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDJENISDIET, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvKDJENISDIET, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtREMARKS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tab2.ResumeLayout(False)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.txtMEMO.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.s, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtKDDIET.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKDADJUSTMENT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lDATE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lDATEFROM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lDATETO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents layoutControl As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents lKDADJUSTMENT As DevExpress.XtraLayout.LayoutControlItem
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
    Friend WithEvents txtKDDIET As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tabControl As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents tab1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grdDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents grvDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents deDATE As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lDATE As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents tab2 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents s As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents txtMEMO As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents mnuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtREMARKS As DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents BindingSource As BindingSource
    Friend WithEvents cboKATEGORI As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents colKDIDENTITAS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBED As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colRUANGAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colKDDOCTOR As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdKDDOKTER As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents grvKDDOKTER As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colKDBENTUKMAKANAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdKDBENTUKMAKANAN As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents grVKDBENTUKMAKANAN As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colKDJENISDIET As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdKDJENISDIET As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents grvKDJENISDIET As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colREMARKS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents btnReload As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents chkPemetaan As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents deDateFrom As DevExpress.XtraEditors.DateEdit
    Friend WithEvents deDateTo As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lDATEFROM As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lDATETO As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents colNAMAPASIEN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colKDCUSTOMER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colWAKTUMAKAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBATASMAKAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CetakEtiketToolStripMenuItem As ToolStripMenuItem
End Class
