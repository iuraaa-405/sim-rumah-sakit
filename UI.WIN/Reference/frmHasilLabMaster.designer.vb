<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmHasilLabMaster
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
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.grdKDITEM = New DevExpress.XtraEditors.GridLookUpEdit()
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
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdTemplate = New DevExpress.XtraGrid.GridControl()
        Me.BindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.grvTemplate = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colISGROUP = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPEMERIKSAAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colHASIL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colNILAIRUJUKAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colNILAI1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colNILAI2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colSATUAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colKETERANGAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.chkISACTIVE = New DevExpress.XtraEditors.CheckEdit()
        Me.txtJUDUL = New DevExpress.XtraEditors.MemoEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.lMEMO = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lGrid = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.layoutControl.SuspendLayout()
        CType(Me.grdKDITEM.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkISACTIVE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtJUDUL.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lMEMO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'layoutControl
        '
        Me.layoutControl.Controls.Add(Me.SimpleButton1)
        Me.layoutControl.Controls.Add(Me.grdKDITEM)
        Me.layoutControl.Controls.Add(Me.grdTemplate)
        Me.layoutControl.Controls.Add(Me.chkISACTIVE)
        Me.layoutControl.Controls.Add(Me.txtJUDUL)
        Me.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutControl.Location = New System.Drawing.Point(0, 0)
        Me.layoutControl.Name = "layoutControl"
        Me.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(774, 238, 250, 350)
        Me.layoutControl.Root = Me.LayoutControlGroup1
        Me.layoutControl.Size = New System.Drawing.Size(590, 401)
        Me.layoutControl.TabIndex = 0
        Me.layoutControl.Text = "LayoutControl1"
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Location = New System.Drawing.Point(438, 12)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(79, 22)
        Me.SimpleButton1.StyleController = Me.layoutControl
        Me.SimpleButton1.TabIndex = 46
        Me.SimpleButton1.Text = "Kosongkan"
        '
        'grdKDITEM
        '
        Me.grdKDITEM.EnterMoveNextControl = True
        Me.grdKDITEM.Location = New System.Drawing.Point(117, 12)
        Me.grdKDITEM.MenuManager = Me.barManager
        Me.grdKDITEM.Name = "grdKDITEM"
        Me.grdKDITEM.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDITEM.Properties.NullText = ""
        Me.grdKDITEM.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdKDITEM.Properties.View = Me.GridView2
        Me.grdKDITEM.Size = New System.Drawing.Size(317, 20)
        Me.grdKDITEM.StyleController = Me.layoutControl
        Me.grdKDITEM.TabIndex = 45
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
        'GridView2
        '
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn5})
        Me.GridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView2.OptionsView.ShowAutoFilterRow = True
        Me.GridView2.OptionsView.ShowGroupPanel = False
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Name Display"
        Me.GridColumn5.FieldName = "NMITEM2"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 0
        '
        'grdTemplate
        '
        Me.grdTemplate.ContextMenuStrip = Me.ContextMenuStrip
        Me.grdTemplate.DataSource = Me.BindingSource
        Me.grdTemplate.Location = New System.Drawing.Point(12, 62)
        Me.grdTemplate.MainView = Me.grvTemplate
        Me.grdTemplate.MenuManager = Me.barManager
        Me.grdTemplate.Name = "grdTemplate"
        Me.grdTemplate.Size = New System.Drawing.Size(566, 327)
        Me.grdTemplate.TabIndex = 22
        Me.grdTemplate.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvTemplate})
        '
        'BindingSource
        '
        Me.BindingSource.DataSource = GetType(DataAccess.M_HASILLAB_D)
        '
        'grvTemplate
        '
        Me.grvTemplate.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colISGROUP, Me.colPEMERIKSAAN, Me.colHASIL, Me.colNILAIRUJUKAN, Me.colNILAI1, Me.colNILAI2, Me.colSATUAN, Me.colKETERANGAN})
        Me.grvTemplate.GridControl = Me.grdTemplate
        Me.grvTemplate.Name = "grvTemplate"
        Me.grvTemplate.OptionsCustomization.AllowColumnMoving = False
        Me.grvTemplate.OptionsCustomization.AllowFilter = False
        Me.grvTemplate.OptionsCustomization.AllowGroup = False
        Me.grvTemplate.OptionsCustomization.AllowQuickHideColumns = False
        Me.grvTemplate.OptionsCustomization.AllowSort = False
        Me.grvTemplate.OptionsDetail.EnableMasterViewMode = False
        Me.grvTemplate.OptionsFind.AllowFindPanel = False
        Me.grvTemplate.OptionsMenu.EnableColumnMenu = False
        Me.grvTemplate.OptionsNavigation.AutoFocusNewRow = True
        Me.grvTemplate.OptionsNavigation.EnterMoveNextColumn = True
        Me.grvTemplate.OptionsView.EnableAppearanceEvenRow = True
        Me.grvTemplate.OptionsView.EnableAppearanceOddRow = True
        Me.grvTemplate.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.grvTemplate.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.grvTemplate.OptionsView.ShowFooter = True
        Me.grvTemplate.OptionsView.ShowGroupPanel = False
        '
        'colISGROUP
        '
        Me.colISGROUP.Caption = "Header"
        Me.colISGROUP.FieldName = "ISGROUP"
        Me.colISGROUP.Name = "colISGROUP"
        Me.colISGROUP.Visible = True
        Me.colISGROUP.VisibleIndex = 0
        Me.colISGROUP.Width = 46
        '
        'colPEMERIKSAAN
        '
        Me.colPEMERIKSAAN.Caption = "Pemeriksaan"
        Me.colPEMERIKSAAN.FieldName = "PEMERIKSAAN"
        Me.colPEMERIKSAAN.Name = "colPEMERIKSAAN"
        Me.colPEMERIKSAAN.Visible = True
        Me.colPEMERIKSAAN.VisibleIndex = 1
        '
        'colHASIL
        '
        Me.colHASIL.Caption = "Hasil"
        Me.colHASIL.FieldName = "HASIL"
        Me.colHASIL.Name = "colHASIL"
        Me.colHASIL.Visible = True
        Me.colHASIL.VisibleIndex = 2
        '
        'colNILAIRUJUKAN
        '
        Me.colNILAIRUJUKAN.Caption = "Nilai Rujukan"
        Me.colNILAIRUJUKAN.FieldName = "NILAIRUJUKAN"
        Me.colNILAIRUJUKAN.Name = "colNILAIRUJUKAN"
        Me.colNILAIRUJUKAN.Visible = True
        Me.colNILAIRUJUKAN.VisibleIndex = 3
        '
        'colNILAI1
        '
        Me.colNILAI1.Caption = "Nilai 1"
        Me.colNILAI1.DisplayFormat.FormatString = "{0:n2}"
        Me.colNILAI1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colNILAI1.FieldName = "NILAI1"
        Me.colNILAI1.Name = "colNILAI1"
        Me.colNILAI1.Visible = True
        Me.colNILAI1.VisibleIndex = 4
        '
        'colNILAI2
        '
        Me.colNILAI2.Caption = "Nilai 2"
        Me.colNILAI2.DisplayFormat.FormatString = "{0:n2}"
        Me.colNILAI2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colNILAI2.FieldName = "NILAI2"
        Me.colNILAI2.Name = "colNILAI2"
        Me.colNILAI2.Visible = True
        Me.colNILAI2.VisibleIndex = 5
        '
        'colSATUAN
        '
        Me.colSATUAN.Caption = "Satuan"
        Me.colSATUAN.FieldName = "SATUAN"
        Me.colSATUAN.Name = "colSATUAN"
        Me.colSATUAN.Visible = True
        Me.colSATUAN.VisibleIndex = 6
        '
        'colKETERANGAN
        '
        Me.colKETERANGAN.Caption = "Keterangan"
        Me.colKETERANGAN.FieldName = "KETERANGAN"
        Me.colKETERANGAN.Name = "colKETERANGAN"
        Me.colKETERANGAN.Visible = True
        Me.colKETERANGAN.VisibleIndex = 7
        '
        'chkISACTIVE
        '
        Me.chkISACTIVE.EnterMoveNextControl = True
        Me.chkISACTIVE.Location = New System.Drawing.Point(521, 12)
        Me.chkISACTIVE.MenuManager = Me.barManager
        Me.chkISACTIVE.Name = "chkISACTIVE"
        Me.chkISACTIVE.Properties.Caption = "Active?"
        Me.chkISACTIVE.Properties.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        Me.chkISACTIVE.Size = New System.Drawing.Size(57, 19)
        Me.chkISACTIVE.StyleController = Me.layoutControl
        Me.chkISACTIVE.TabIndex = 14
        Me.chkISACTIVE.TabStop = False
        '
        'txtJUDUL
        '
        Me.txtJUDUL.EditValue = ""
        Me.txtJUDUL.Location = New System.Drawing.Point(117, 38)
        Me.txtJUDUL.Name = "txtJUDUL"
        Me.txtJUDUL.Size = New System.Drawing.Size(400, 20)
        Me.txtJUDUL.StyleController = Me.layoutControl
        Me.txtJUDUL.TabIndex = 9
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lMEMO, Me.LayoutControlItem1, Me.lGrid, Me.LayoutControlItem2, Me.LayoutControlItem3})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(590, 401)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'lMEMO
        '
        Me.lMEMO.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lMEMO.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lMEMO.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.lMEMO.Control = Me.txtJUDUL
        Me.lMEMO.CustomizationFormText = "Judul * :"
        Me.lMEMO.Location = New System.Drawing.Point(0, 26)
        Me.lMEMO.Name = "lMEMO"
        Me.lMEMO.Size = New System.Drawing.Size(509, 24)
        Me.lMEMO.Text = "Judul * :"
        Me.lMEMO.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lMEMO.TextSize = New System.Drawing.Size(100, 20)
        Me.lMEMO.TextToControlDistance = 5
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.chkISACTIVE
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(509, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(61, 50)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'lGrid
        '
        Me.lGrid.Control = Me.grdTemplate
        Me.lGrid.Location = New System.Drawing.Point(0, 50)
        Me.lGrid.Name = "lGrid"
        Me.lGrid.Size = New System.Drawing.Size(570, 331)
        Me.lGrid.TextSize = New System.Drawing.Size(0, 0)
        Me.lGrid.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem2.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem2.Control = Me.grdKDITEM
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(426, 26)
        Me.LayoutControlItem2.Text = "Tindakan :"
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(100, 20)
        Me.LayoutControlItem2.TextToControlDistance = 5
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.SimpleButton1
        Me.LayoutControlItem3.Location = New System.Drawing.Point(426, 0)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(83, 26)
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(107, 22)
        Me.DeleteToolStripMenuItem.Text = "Delete"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DeleteToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(108, 26)
        '
        'frmHasilLabMaster
        '
        Me.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(590, 423)
        Me.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Controls.Add(Me.layoutControl)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.Name = "frmHasilLabMaster"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.layoutControl.ResumeLayout(False)
        CType(Me.grdKDITEM.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkISACTIVE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtJUDUL.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lMEMO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents layoutControl As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents lMEMO As DevExpress.XtraLayout.LayoutControlItem
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
    Friend WithEvents txtJUDUL As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents grdTemplate As DevExpress.XtraGrid.GridControl
    Friend WithEvents grvTemplate As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colPEMERIKSAAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colHASIL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colNILAIRUJUKAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colSATUAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colKETERANGAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lGrid As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents BindingSource As BindingSource
    'Friend WithEvents ContextMenuStrip As ContextMenuStrip
    Friend WithEvents DeleteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents colISGROUP As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents colNILAI1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colNILAI2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdKDITEM As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
End Class
