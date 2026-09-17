<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmItemLaboratorium
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
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
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
        Me.grdTemplate = New DevExpress.XtraGrid.GridControl()
        Me.mnuStripTemplate = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DeleteToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.BindingSourceTemplatelab = New System.Windows.Forms.BindingSource(Me.components)
        Me.grvTemplate = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colPEMERIKSAAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colHASIL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colNILAIRUJUKAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colNILAIRUJUKAN_1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colNILAIRUJUKAN_2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colSATUAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colKETERANGAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.txtNMITEM1 = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.layoutControl.SuspendLayout()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuStripTemplate.SuspendLayout()
        CType(Me.BindingSourceTemplatelab, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNMITEM1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'layoutControl
        '
        Me.layoutControl.Controls.Add(Me.txtNMITEM1)
        Me.layoutControl.Controls.Add(Me.grdTemplate)
        Me.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutControl.Location = New System.Drawing.Point(0, 0)
        Me.layoutControl.Name = "layoutControl"
        Me.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(774, 238, 250, 350)
        Me.layoutControl.Root = Me.LayoutControlGroup1
        Me.layoutControl.Size = New System.Drawing.Size(590, 401)
        Me.layoutControl.TabIndex = 0
        Me.layoutControl.Text = "LayoutControl1"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(590, 401)
        Me.LayoutControlGroup1.TextVisible = False
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
        'grdTemplate
        '
        Me.grdTemplate.ContextMenuStrip = Me.mnuStripTemplate
        Me.grdTemplate.DataSource = Me.BindingSourceTemplatelab
        Me.grdTemplate.Location = New System.Drawing.Point(2, 26)
        Me.grdTemplate.MainView = Me.grvTemplate
        Me.grdTemplate.MenuManager = Me.barManager
        Me.grdTemplate.Name = "grdTemplate"
        Me.grdTemplate.Size = New System.Drawing.Size(586, 373)
        Me.grdTemplate.TabIndex = 21
        Me.grdTemplate.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvTemplate})
        '
        'mnuStripTemplate
        '
        Me.mnuStripTemplate.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DeleteToolStripMenuItem1})
        Me.mnuStripTemplate.Name = "mnuStripTemplate"
        Me.mnuStripTemplate.Size = New System.Drawing.Size(108, 26)
        '
        'DeleteToolStripMenuItem1
        '
        Me.DeleteToolStripMenuItem1.Name = "DeleteToolStripMenuItem1"
        Me.DeleteToolStripMenuItem1.Size = New System.Drawing.Size(107, 22)
        Me.DeleteToolStripMenuItem1.Text = "Delete"
        '
        'BindingSourceTemplatelab
        '
        Me.BindingSourceTemplatelab.DataSource = GetType(DataAccess.M_ITEM_TEMPALTE_1LAB)
        '
        'grvTemplate
        '
        Me.grvTemplate.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colPEMERIKSAAN, Me.colHASIL, Me.colNILAIRUJUKAN, Me.colNILAIRUJUKAN_1, Me.colNILAIRUJUKAN_2, Me.colSATUAN, Me.colKETERANGAN})
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
        'colPEMERIKSAAN
        '
        Me.colPEMERIKSAAN.Caption = "Pemeriksaan"
        Me.colPEMERIKSAAN.FieldName = "PEMERIKSAAN"
        Me.colPEMERIKSAAN.Name = "colPEMERIKSAAN"
        Me.colPEMERIKSAAN.Visible = True
        Me.colPEMERIKSAAN.VisibleIndex = 0
        '
        'colHASIL
        '
        Me.colHASIL.Caption = "Hasil"
        Me.colHASIL.FieldName = "HASIL"
        Me.colHASIL.Name = "colHASIL"
        Me.colHASIL.Visible = True
        Me.colHASIL.VisibleIndex = 1
        '
        'colNILAIRUJUKAN
        '
        Me.colNILAIRUJUKAN.Caption = "Nilai Rujukan"
        Me.colNILAIRUJUKAN.FieldName = "NILAIRUJUKAN"
        Me.colNILAIRUJUKAN.Name = "colNILAIRUJUKAN"
        Me.colNILAIRUJUKAN.Visible = True
        Me.colNILAIRUJUKAN.VisibleIndex = 2
        '
        'colNILAIRUJUKAN_1
        '
        Me.colNILAIRUJUKAN_1.Caption = "Nilai 1"
        Me.colNILAIRUJUKAN_1.DisplayFormat.FormatString = "{0:n2}"
        Me.colNILAIRUJUKAN_1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colNILAIRUJUKAN_1.FieldName = "NILAIRUJUKAN_1"
        Me.colNILAIRUJUKAN_1.Name = "colNILAIRUJUKAN_1"
        '
        'colNILAIRUJUKAN_2
        '
        Me.colNILAIRUJUKAN_2.Caption = "Nilai 2"
        Me.colNILAIRUJUKAN_2.DisplayFormat.FormatString = "{0:n2}"
        Me.colNILAIRUJUKAN_2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colNILAIRUJUKAN_2.FieldName = "NILAIRUJUKAN_2"
        Me.colNILAIRUJUKAN_2.Name = "colNILAIRUJUKAN_2"
        '
        'colSATUAN
        '
        Me.colSATUAN.Caption = "Satuan"
        Me.colSATUAN.FieldName = "SATUAN"
        Me.colSATUAN.Name = "colSATUAN"
        Me.colSATUAN.Visible = True
        Me.colSATUAN.VisibleIndex = 3
        '
        'colKETERANGAN
        '
        Me.colKETERANGAN.Caption = "Keterangan"
        Me.colKETERANGAN.FieldName = "KETERANGAN"
        Me.colKETERANGAN.Name = "colKETERANGAN"
        Me.colKETERANGAN.Visible = True
        Me.colKETERANGAN.VisibleIndex = 4
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.grdTemplate
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(590, 377)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'txtNMITEM1
        '
        Me.txtNMITEM1.Location = New System.Drawing.Point(107, 2)
        Me.txtNMITEM1.MenuManager = Me.barManager
        Me.txtNMITEM1.Name = "txtNMITEM1"
        Me.txtNMITEM1.Size = New System.Drawing.Size(481, 20)
        Me.txtNMITEM1.StyleController = Me.layoutControl
        Me.txtNMITEM1.TabIndex = 22
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem2.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem2.Control = Me.txtNMITEM1
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(590, 24)
        Me.LayoutControlItem2.Text = "Kode :"
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(100, 20)
        Me.LayoutControlItem2.TextToControlDistance = 5
        Me.LayoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'frmItemLaboratorium
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
        Me.Name = "frmItemLaboratorium"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.layoutControl.ResumeLayout(False)
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuStripTemplate.ResumeLayout(False)
        CType(Me.BindingSourceTemplatelab, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNMITEM1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents grdTemplate As DevExpress.XtraGrid.GridControl
    Friend WithEvents grvTemplate As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colPEMERIKSAAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colHASIL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colNILAIRUJUKAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colNILAIRUJUKAN_1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colNILAIRUJUKAN_2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colSATUAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colKETERANGAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BindingSourceTemplatelab As BindingSource
    Friend WithEvents mnuStripTemplate As ContextMenuStrip
    Friend WithEvents DeleteToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents txtNMITEM1 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
End Class
