<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmKelasAplicarePerRuangan
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmKelasAplicarePerRuangan))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.layoutControl = New DevExpress.XtraLayout.LayoutControl()
        Me.tabControl = New DevExpress.XtraTab.XtraTabControl()
        Me.tab1 = New DevExpress.XtraTab.XtraTabPage()
        Me.grdDetail_UOM = New DevExpress.XtraGrid.GridControl()
        Me.mnuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.grvDetail_UOM = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colKDUPDATE_APLICARE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colKDKELASAPLICARE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colKDUOM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdUOM = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.grvUOM = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colKAPASITAS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTERSEDIA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTERSEDIA_LAKI = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTERSEDIA_PEREMPUAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTERSEDIA_LAKIPEREMPUAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.barManager = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barTop = New DevExpress.XtraBars.Bar()
        Me.btnSaveClose = New DevExpress.XtraBars.BarButtonItem()
        Me.btnClose = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.btnSaveNew = New DevExpress.XtraBars.BarButtonItem()
        Me.btnKetersediaanKamar = New DevExpress.XtraBars.BarButtonItem()
        Me.progressBarSave = New DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar()
        Me.progressSave = New DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar()
        Me.chkISAPLICARE = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemButtonEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.layoutControl.SuspendLayout()
        CType(Me.tabControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabControl.SuspendLayout()
        Me.tab1.SuspendLayout()
        CType(Me.grdDetail_UOM, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuStrip.SuspendLayout()
        CType(Me.BindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvDetail_UOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdUOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvUOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkISAPLICARE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemButtonEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'layoutControl
        '
        Me.layoutControl.Controls.Add(Me.tabControl)
        Me.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutControl.Location = New System.Drawing.Point(0, 0)
        Me.layoutControl.Name = "layoutControl"
        Me.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(774, 238, 250, 350)
        Me.layoutControl.Root = Me.LayoutControlGroup1
        Me.layoutControl.Size = New System.Drawing.Size(832, 568)
        Me.layoutControl.TabIndex = 0
        Me.layoutControl.Text = "LayoutControl1"
        '
        'tabControl
        '
        Me.tabControl.Location = New System.Drawing.Point(12, 12)
        Me.tabControl.Name = "tabControl"
        Me.tabControl.SelectedTabPage = Me.tab1
        Me.tabControl.Size = New System.Drawing.Size(808, 544)
        Me.tabControl.TabIndex = 19
        Me.tabControl.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tab1})
        '
        'tab1
        '
        Me.tab1.Controls.Add(Me.grdDetail_UOM)
        Me.tab1.Name = "tab1"
        Me.tab1.Size = New System.Drawing.Size(802, 516)
        Me.tab1.Text = "Informasi Satuan"
        '
        'grdDetail_UOM
        '
        Me.grdDetail_UOM.ContextMenuStrip = Me.mnuStrip
        Me.grdDetail_UOM.DataSource = Me.BindingSource
        Me.grdDetail_UOM.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdDetail_UOM.Location = New System.Drawing.Point(0, 0)
        Me.grdDetail_UOM.MainView = Me.grvDetail_UOM
        Me.grdDetail_UOM.MenuManager = Me.barManager
        Me.grdDetail_UOM.Name = "grdDetail_UOM"
        Me.grdDetail_UOM.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.grdUOM, Me.chkISAPLICARE, Me.RepositoryItemButtonEdit1})
        Me.grdDetail_UOM.Size = New System.Drawing.Size(802, 516)
        Me.grdDetail_UOM.TabIndex = 18
        Me.grdDetail_UOM.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvDetail_UOM})
        '
        'mnuStrip
        '
        Me.mnuStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.mnuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BedToolStripMenuItem})
        Me.mnuStrip.Name = "mnuStrip"
        Me.mnuStrip.Size = New System.Drawing.Size(95, 26)
        '
        'BedToolStripMenuItem
        '
        Me.BedToolStripMenuItem.Name = "BedToolStripMenuItem"
        Me.BedToolStripMenuItem.Size = New System.Drawing.Size(94, 22)
        Me.BedToolStripMenuItem.Text = "Bed"
        '
        'BindingSource
        '
        Me.BindingSource.DataSource = GetType(DataAccess.M_KELASAPLICARE_DEPARTMENT)
        '
        'grvDetail_UOM
        '
        Me.grvDetail_UOM.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colKDUPDATE_APLICARE, Me.colKDKELASAPLICARE, Me.colKDUOM, Me.colKAPASITAS, Me.colTERSEDIA, Me.colTERSEDIA_LAKI, Me.colTERSEDIA_PEREMPUAN, Me.colTERSEDIA_LAKIPEREMPUAN})
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
        'colKDUPDATE_APLICARE
        '
        Me.colKDUPDATE_APLICARE.Caption = "Kode"
        Me.colKDUPDATE_APLICARE.FieldName = "KDUPDATE_APLICARE"
        Me.colKDUPDATE_APLICARE.Name = "colKDUPDATE_APLICARE"
        Me.colKDUPDATE_APLICARE.OptionsColumn.AllowEdit = False
        Me.colKDUPDATE_APLICARE.OptionsColumn.AllowFocus = False
        Me.colKDUPDATE_APLICARE.OptionsColumn.ReadOnly = True
        Me.colKDUPDATE_APLICARE.OptionsColumn.TabStop = False
        Me.colKDUPDATE_APLICARE.Visible = True
        Me.colKDUPDATE_APLICARE.VisibleIndex = 0
        '
        'colKDKELASAPLICARE
        '
        Me.colKDKELASAPLICARE.Caption = "Kelas Aplicare"
        Me.colKDKELASAPLICARE.FieldName = "KDKELASAPLICARE"
        Me.colKDKELASAPLICARE.Name = "colKDKELASAPLICARE"
        Me.colKDKELASAPLICARE.OptionsColumn.AllowEdit = False
        Me.colKDKELASAPLICARE.OptionsColumn.AllowFocus = False
        Me.colKDKELASAPLICARE.OptionsColumn.ReadOnly = True
        Me.colKDKELASAPLICARE.OptionsColumn.TabStop = False
        Me.colKDKELASAPLICARE.Visible = True
        Me.colKDKELASAPLICARE.VisibleIndex = 1
        '
        'colKDUOM
        '
        Me.colKDUOM.Caption = "Ruangan"
        Me.colKDUOM.ColumnEdit = Me.grdUOM
        Me.colKDUOM.FieldName = "KDDEPARTMENT"
        Me.colKDUOM.Name = "colKDUOM"
        Me.colKDUOM.OptionsColumn.AllowEdit = False
        Me.colKDUOM.OptionsColumn.AllowFocus = False
        Me.colKDUOM.OptionsColumn.ReadOnly = True
        Me.colKDUOM.OptionsColumn.TabStop = False
        Me.colKDUOM.Visible = True
        Me.colKDUOM.VisibleIndex = 2
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
        Me.GridColumn4.Caption = "Name Display"
        Me.GridColumn4.FieldName = "NAME_DISPLAY"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 0
        '
        'colKAPASITAS
        '
        Me.colKAPASITAS.Caption = "Kapasitas"
        Me.colKAPASITAS.DisplayFormat.FormatString = "{0:n0}"
        Me.colKAPASITAS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colKAPASITAS.FieldName = "KAPASITAS"
        Me.colKAPASITAS.Name = "colKAPASITAS"
        Me.colKAPASITAS.OptionsColumn.AllowEdit = False
        Me.colKAPASITAS.OptionsColumn.AllowFocus = False
        Me.colKAPASITAS.OptionsColumn.ReadOnly = True
        Me.colKAPASITAS.OptionsColumn.TabStop = False
        Me.colKAPASITAS.Visible = True
        Me.colKAPASITAS.VisibleIndex = 3
        '
        'colTERSEDIA
        '
        Me.colTERSEDIA.Caption = "Tersedia"
        Me.colTERSEDIA.DisplayFormat.FormatString = "{0:n0}"
        Me.colTERSEDIA.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colTERSEDIA.FieldName = "TERSEDIA"
        Me.colTERSEDIA.Name = "colTERSEDIA"
        Me.colTERSEDIA.OptionsColumn.AllowEdit = False
        Me.colTERSEDIA.OptionsColumn.AllowFocus = False
        Me.colTERSEDIA.OptionsColumn.ReadOnly = True
        Me.colTERSEDIA.OptionsColumn.TabStop = False
        Me.colTERSEDIA.Visible = True
        Me.colTERSEDIA.VisibleIndex = 4
        '
        'colTERSEDIA_LAKI
        '
        Me.colTERSEDIA_LAKI.Caption = "Tersedia Pria"
        Me.colTERSEDIA_LAKI.DisplayFormat.FormatString = "{0:n0}"
        Me.colTERSEDIA_LAKI.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colTERSEDIA_LAKI.FieldName = "TERSEDIA_LAKI"
        Me.colTERSEDIA_LAKI.Name = "colTERSEDIA_LAKI"
        Me.colTERSEDIA_LAKI.Visible = True
        Me.colTERSEDIA_LAKI.VisibleIndex = 5
        '
        'colTERSEDIA_PEREMPUAN
        '
        Me.colTERSEDIA_PEREMPUAN.Caption = "Tersedia Perempuan"
        Me.colTERSEDIA_PEREMPUAN.DisplayFormat.FormatString = "{0:n0}"
        Me.colTERSEDIA_PEREMPUAN.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colTERSEDIA_PEREMPUAN.FieldName = "TERSEDIA_PEREMPUAN"
        Me.colTERSEDIA_PEREMPUAN.Name = "colTERSEDIA_PEREMPUAN"
        Me.colTERSEDIA_PEREMPUAN.Visible = True
        Me.colTERSEDIA_PEREMPUAN.VisibleIndex = 6
        '
        'colTERSEDIA_LAKIPEREMPUAN
        '
        Me.colTERSEDIA_LAKIPEREMPUAN.Caption = "Tersedia Pria Wanita"
        Me.colTERSEDIA_LAKIPEREMPUAN.DisplayFormat.FormatString = "{0:n0}"
        Me.colTERSEDIA_LAKIPEREMPUAN.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colTERSEDIA_LAKIPEREMPUAN.FieldName = "TERSEDIA_LAKIPEREMPUAN"
        Me.colTERSEDIA_LAKIPEREMPUAN.Name = "colTERSEDIA_LAKIPEREMPUAN"
        Me.colTERSEDIA_LAKIPEREMPUAN.Visible = True
        Me.colTERSEDIA_LAKIPEREMPUAN.VisibleIndex = 7
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
        Me.barManager.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.btnSaveNew, Me.btnClose, Me.btnSaveClose, Me.btnKetersediaanKamar})
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
        Me.barTop.OptionsBar.DrawDragBorder = False
        Me.barTop.OptionsBar.MultiLine = True
        Me.barTop.OptionsBar.UseWholeRow = True
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
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(832, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 568)
        Me.barDockControlBottom.Size = New System.Drawing.Size(832, 22)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 568)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(832, 0)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 568)
        '
        'btnSaveNew
        '
        Me.btnSaveNew.Caption = "F2 - Save && New"
        Me.btnSaveNew.Id = 2
        Me.btnSaveNew.Name = "btnSaveNew"
        '
        'btnKetersediaanKamar
        '
        Me.btnKetersediaanKamar.Caption = "F5 - Ketersediaan Kamar"
        Me.btnKetersediaanKamar.Id = 8
        Me.btnKetersediaanKamar.Name = "btnKetersediaanKamar"
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
        'chkISAPLICARE
        '
        Me.chkISAPLICARE.AutoHeight = False
        Me.chkISAPLICARE.Name = "chkISAPLICARE"
        Me.chkISAPLICARE.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'RepositoryItemButtonEdit1
        '
        Me.RepositoryItemButtonEdit1.AutoHeight = False
        Me.RepositoryItemButtonEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, CType(resources.GetObject("RepositoryItemButtonEdit1.Buttons"), System.Drawing.Image), New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.RepositoryItemButtonEdit1.Name = "RepositoryItemButtonEdit1"
        Me.RepositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem3})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(832, 568)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.tabControl
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(812, 548)
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'frmKelasAplicarePerRuangan
        '
        Me.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(832, 590)
        Me.Controls.Add(Me.layoutControl)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.Name = "frmKelasAplicarePerRuangan"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.layoutControl.ResumeLayout(False)
        CType(Me.tabControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabControl.ResumeLayout(False)
        Me.tab1.ResumeLayout(False)
        CType(Me.grdDetail_UOM, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuStrip.ResumeLayout(False)
        CType(Me.BindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvDetail_UOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdUOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvUOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkISAPLICARE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemButtonEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents tabControl As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents tab1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents grdDetail_UOM As DevExpress.XtraGrid.GridControl
    Friend WithEvents grvDetail_UOM As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colKDUOM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdUOM As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents grvUOM As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents BindingSource As BindingSource
    Friend WithEvents colKAPASITAS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTERSEDIA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTERSEDIA_LAKI As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTERSEDIA_PEREMPUAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mnuStrip As ContextMenuStrip
    Friend WithEvents chkISAPLICARE As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemButtonEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents colTERSEDIA_LAKIPEREMPUAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btnKetersediaanKamar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents colKDKELASAPLICARE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colKDUPDATE_APLICARE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BedToolStripMenuItem As ToolStripMenuItem
End Class
