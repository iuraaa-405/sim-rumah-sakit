<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmGrouperPDF
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGrouperPDF))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.layoutControl = New DevExpress.XtraLayout.LayoutControl()
        Me.LayoutControl5 = New DevExpress.XtraLayout.LayoutControl()
        Me.LayoutControlGroup5 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.tabControl = New DevExpress.XtraTab.XtraTabControl()
        Me.tab1 = New DevExpress.XtraTab.XtraTabPage()
        Me.grdDetail = New DevExpress.XtraGrid.GridControl()
        Me.mnuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.bindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.grvDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colKDPDF = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdKDPDF = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.grvKDPDF = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colREMARKS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colALAMAT_AWAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colALAMAT_UPLOAD = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTYPEFILE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colSIZE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colAksi = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.btnBrowsePdf = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.colISCHEKED = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDATECREATED = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPACS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.btnUpload = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
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
        Me.txtkodegrouper = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.lKDADJUSTMENT = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.layoutControl.SuspendLayout()
        CType(Me.LayoutControl5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tabControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabControl.SuspendLayout()
        Me.tab1.SuspendLayout()
        CType(Me.grdDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuStrip.SuspendLayout()
        CType(Me.bindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDPDF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvKDPDF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBrowsePdf, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUpload, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtkodegrouper.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKDADJUSTMENT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'layoutControl
        '
        Me.layoutControl.Controls.Add(Me.LayoutControl5)
        Me.layoutControl.Controls.Add(Me.tabControl)
        Me.layoutControl.Controls.Add(Me.txtkodegrouper)
        Me.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutControl.Location = New System.Drawing.Point(0, 0)
        Me.layoutControl.Name = "layoutControl"
        Me.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(652, 156, 250, 350)
        Me.layoutControl.Root = Me.LayoutControlGroup1
        Me.layoutControl.Size = New System.Drawing.Size(790, 549)
        Me.layoutControl.TabIndex = 0
        Me.layoutControl.Text = "LayoutControl1"
        '
        'LayoutControl5
        '
        Me.LayoutControl5.Location = New System.Drawing.Point(496, 12)
        Me.LayoutControl5.Name = "LayoutControl5"
        Me.LayoutControl5.Root = Me.LayoutControlGroup5
        Me.LayoutControl5.Size = New System.Drawing.Size(282, 30)
        Me.LayoutControl5.TabIndex = 21
        Me.LayoutControl5.Text = "LayoutControl5"
        '
        'LayoutControlGroup5
        '
        Me.LayoutControlGroup5.CustomizationFormText = "LayoutControlGroup5"
        Me.LayoutControlGroup5.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup5.GroupBordersVisible = False
        Me.LayoutControlGroup5.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.EmptySpaceItem1})
        Me.LayoutControlGroup5.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup5.Name = "LayoutControlGroup5"
        Me.LayoutControlGroup5.Size = New System.Drawing.Size(282, 30)
        Me.LayoutControlGroup5.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 0)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(262, 10)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'tabControl
        '
        Me.tabControl.Location = New System.Drawing.Point(12, 46)
        Me.tabControl.Name = "tabControl"
        Me.tabControl.SelectedTabPage = Me.tab1
        Me.tabControl.Size = New System.Drawing.Size(766, 491)
        Me.tabControl.TabIndex = 18
        Me.tabControl.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tab1})
        '
        'tab1
        '
        Me.tab1.Controls.Add(Me.grdDetail)
        Me.tab1.Name = "tab1"
        Me.tab1.Size = New System.Drawing.Size(760, 463)
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
        Me.grdDetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.btnBrowsePdf, Me.grdKDPDF, Me.btnUpload})
        Me.grdDetail.Size = New System.Drawing.Size(760, 463)
        Me.grdDetail.TabIndex = 18
        Me.grdDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvDetail})
        '
        'mnuStrip
        '
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
        Me.bindingSource.DataSource = GetType(DataAccess.S_PENDAFTARAN_PDF)
        '
        'grvDetail
        '
        Me.grvDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colKDPDF, Me.colREMARKS, Me.colALAMAT_AWAL, Me.colALAMAT_UPLOAD, Me.colTYPEFILE, Me.colSIZE, Me.colAksi, Me.colISCHEKED, Me.colDATECREATED, Me.colPACS})
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
        'colKDPDF
        '
        Me.colKDPDF.Caption = "Nama PDF"
        Me.colKDPDF.ColumnEdit = Me.grdKDPDF
        Me.colKDPDF.FieldName = "KDPDF"
        Me.colKDPDF.Name = "colKDPDF"
        Me.colKDPDF.Visible = True
        Me.colKDPDF.VisibleIndex = 0
        Me.colKDPDF.Width = 222
        '
        'grdKDPDF
        '
        Me.grdKDPDF.AutoHeight = False
        Me.grdKDPDF.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDPDF.Name = "grdKDPDF"
        Me.grdKDPDF.NullText = ""
        Me.grdKDPDF.View = Me.grvKDPDF
        '
        'grvKDPDF
        '
        Me.grvKDPDF.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1})
        Me.grvKDPDF.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvKDPDF.Name = "grvKDPDF"
        Me.grvKDPDF.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvKDPDF.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Nama PDF"
        Me.GridColumn1.FieldName = "MEMO"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'colREMARKS
        '
        Me.colREMARKS.Caption = "Catatan"
        Me.colREMARKS.FieldName = "REMARKS"
        Me.colREMARKS.Name = "colREMARKS"
        Me.colREMARKS.Visible = True
        Me.colREMARKS.VisibleIndex = 1
        Me.colREMARKS.Width = 139
        '
        'colALAMAT_AWAL
        '
        Me.colALAMAT_AWAL.Caption = "Alamat Awal"
        Me.colALAMAT_AWAL.FieldName = "ALAMAT_AWAL"
        Me.colALAMAT_AWAL.Name = "colALAMAT_AWAL"
        Me.colALAMAT_AWAL.OptionsColumn.AllowEdit = False
        Me.colALAMAT_AWAL.OptionsColumn.AllowFocus = False
        Me.colALAMAT_AWAL.OptionsColumn.ReadOnly = True
        Me.colALAMAT_AWAL.OptionsColumn.TabStop = False
        Me.colALAMAT_AWAL.Visible = True
        Me.colALAMAT_AWAL.VisibleIndex = 2
        Me.colALAMAT_AWAL.Width = 171
        '
        'colALAMAT_UPLOAD
        '
        Me.colALAMAT_UPLOAD.Caption = "Alamat Upload"
        Me.colALAMAT_UPLOAD.FieldName = "ALAMAT_UPLOAD"
        Me.colALAMAT_UPLOAD.Name = "colALAMAT_UPLOAD"
        Me.colALAMAT_UPLOAD.OptionsColumn.AllowEdit = False
        Me.colALAMAT_UPLOAD.OptionsColumn.AllowFocus = False
        Me.colALAMAT_UPLOAD.OptionsColumn.ReadOnly = True
        Me.colALAMAT_UPLOAD.OptionsColumn.TabStop = False
        Me.colALAMAT_UPLOAD.Visible = True
        Me.colALAMAT_UPLOAD.VisibleIndex = 3
        Me.colALAMAT_UPLOAD.Width = 154
        '
        'colTYPEFILE
        '
        Me.colTYPEFILE.Caption = "Type"
        Me.colTYPEFILE.FieldName = "TYPEFILE"
        Me.colTYPEFILE.Name = "colTYPEFILE"
        Me.colTYPEFILE.OptionsColumn.AllowEdit = False
        Me.colTYPEFILE.OptionsColumn.AllowFocus = False
        Me.colTYPEFILE.OptionsColumn.ReadOnly = True
        Me.colTYPEFILE.OptionsColumn.TabStop = False
        Me.colTYPEFILE.Visible = True
        Me.colTYPEFILE.VisibleIndex = 4
        '
        'colSIZE
        '
        Me.colSIZE.Caption = "Ukuran"
        Me.colSIZE.FieldName = "SIZE"
        Me.colSIZE.Name = "colSIZE"
        Me.colSIZE.OptionsColumn.AllowEdit = False
        Me.colSIZE.OptionsColumn.AllowFocus = False
        Me.colSIZE.OptionsColumn.ReadOnly = True
        Me.colSIZE.OptionsColumn.TabStop = False
        Me.colSIZE.Visible = True
        Me.colSIZE.VisibleIndex = 5
        Me.colSIZE.Width = 53
        '
        'colAksi
        '
        Me.colAksi.Caption = "Aksi"
        Me.colAksi.ColumnEdit = Me.btnBrowsePdf
        Me.colAksi.Name = "colAksi"
        Me.colAksi.Visible = True
        Me.colAksi.VisibleIndex = 6
        Me.colAksi.Width = 37
        '
        'btnBrowsePdf
        '
        Me.btnBrowsePdf.AutoHeight = False
        Me.btnBrowsePdf.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, CType(resources.GetObject("btnBrowsePdf.Buttons"), System.Drawing.Image), New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.btnBrowsePdf.Name = "btnBrowsePdf"
        Me.btnBrowsePdf.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'colISCHEKED
        '
        Me.colISCHEKED.Caption = "Batal"
        Me.colISCHEKED.FieldName = "ISCHEKED"
        Me.colISCHEKED.Name = "colISCHEKED"
        '
        'colDATECREATED
        '
        Me.colDATECREATED.Caption = "Tanggl Buat"
        Me.colDATECREATED.FieldName = "DATECREATED"
        Me.colDATECREATED.Name = "colDATECREATED"
        '
        'colPACS
        '
        Me.colPACS.Caption = "PACS"
        Me.colPACS.ColumnEdit = Me.btnUpload
        Me.colPACS.Name = "colPACS"
        '
        'btnUpload
        '
        Me.btnUpload.AutoHeight = False
        Me.btnUpload.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, CType(resources.GetObject("btnUpload.Buttons"), System.Drawing.Image), New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", Nothing, Nothing, True)})
        Me.btnUpload.Name = "btnUpload"
        Me.btnUpload.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
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
        'txtkodegrouper
        '
        Me.txtkodegrouper.EditValue = ""
        Me.txtkodegrouper.EnterMoveNextControl = True
        Me.txtkodegrouper.Location = New System.Drawing.Point(117, 12)
        Me.txtkodegrouper.Name = "txtkodegrouper"
        Me.txtkodegrouper.Properties.ReadOnly = True
        Me.txtkodegrouper.Size = New System.Drawing.Size(375, 20)
        Me.txtkodegrouper.StyleController = Me.layoutControl
        Me.txtkodegrouper.TabIndex = 9
        Me.txtkodegrouper.TabStop = False
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lKDADJUSTMENT, Me.LayoutControlItem5, Me.LayoutControlItem3})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(790, 549)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'lKDADJUSTMENT
        '
        Me.lKDADJUSTMENT.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKDADJUSTMENT.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKDADJUSTMENT.Control = Me.txtkodegrouper
        Me.lKDADJUSTMENT.CustomizationFormText = "Display Name * :"
        Me.lKDADJUSTMENT.Location = New System.Drawing.Point(0, 0)
        Me.lKDADJUSTMENT.Name = "lKDADJUSTMENT"
        Me.lKDADJUSTMENT.Size = New System.Drawing.Size(484, 34)
        Me.lKDADJUSTMENT.Text = "Number * :"
        Me.lKDADJUSTMENT.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKDADJUSTMENT.TextSize = New System.Drawing.Size(100, 20)
        Me.lKDADJUSTMENT.TextToControlDistance = 5
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.tabControl
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 34)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(770, 495)
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.LayoutControl5
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(484, 0)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(286, 34)
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Number"
        Me.GridColumn6.FieldName = "KDSO"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 0
        '
        'frmGrouperPDF
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
        Me.Name = "frmGrouperPDF"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.layoutControl.ResumeLayout(False)
        CType(Me.LayoutControl5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tabControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabControl.ResumeLayout(False)
        Me.tab1.ResumeLayout(False)
        CType(Me.grdDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuStrip.ResumeLayout(False)
        CType(Me.bindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDPDF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvKDPDF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBrowsePdf, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUpload, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtkodegrouper.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKDADJUSTMENT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents txtkodegrouper As DevExpress.XtraEditors.TextEdit
    Friend WithEvents bindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents tabControl As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents tab1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grdDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents grvDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControl5 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup5 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents mnuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colAksi As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btnBrowsePdf As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents colKDPDF As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colREMARKS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdKDPDF As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents grvKDPDF As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colALAMAT_AWAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colALAMAT_UPLOAD As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colSIZE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colISCHEKED As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDATECREATED As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTYPEFILE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPACS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btnUpload As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
End Class
