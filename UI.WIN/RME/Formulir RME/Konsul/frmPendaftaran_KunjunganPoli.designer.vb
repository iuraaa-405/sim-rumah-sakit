<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmPendaftaran_KunjunganPoli
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
        Me.txtPENJAMIN = New DevExpress.XtraEditors.TextEdit()
        Me.barManager = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barTop = New DevExpress.XtraBars.Bar()
        Me.btnSaveClose = New DevExpress.XtraBars.BarButtonItem()
        Me.btnClose = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.btnSaveNew = New DevExpress.XtraBars.BarButtonItem()
        Me.progressBarSave = New DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar()
        Me.progressSave = New DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar()
        Me.grdDOCTOR = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtCODE = New DevExpress.XtraEditors.TextEdit()
        Me.grdKDDEPARTMENT = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridView7 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.deDATE_MASUK = New DevExpress.XtraEditors.DateEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.lDATE_MASUK = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lKDDEPARTMENT = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lKDKUNJUNGAN_POLI = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lDOCTOR = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.layoutControl.SuspendLayout()
        CType(Me.txtPENJAMIN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdDOCTOR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCODE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDDEPARTMENT.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATE_MASUK.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATE_MASUK.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lDATE_MASUK, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKDDEPARTMENT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKDKUNJUNGAN_POLI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lDOCTOR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'layoutControl
        '
        Me.layoutControl.Controls.Add(Me.txtPENJAMIN)
        Me.layoutControl.Controls.Add(Me.grdDOCTOR)
        Me.layoutControl.Controls.Add(Me.txtCODE)
        Me.layoutControl.Controls.Add(Me.grdKDDEPARTMENT)
        Me.layoutControl.Controls.Add(Me.deDATE_MASUK)
        Me.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutControl.Location = New System.Drawing.Point(0, 0)
        Me.layoutControl.Name = "layoutControl"
        Me.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(774, 238, 250, 350)
        Me.layoutControl.Root = Me.LayoutControlGroup1
        Me.layoutControl.Size = New System.Drawing.Size(537, 172)
        Me.layoutControl.TabIndex = 0
        Me.layoutControl.Text = "LayoutControl1"
        '
        'txtPENJAMIN
        '
        Me.txtPENJAMIN.Location = New System.Drawing.Point(137, 36)
        Me.txtPENJAMIN.MenuManager = Me.barManager
        Me.txtPENJAMIN.Name = "txtPENJAMIN"
        Me.txtPENJAMIN.Properties.ReadOnly = True
        Me.txtPENJAMIN.Size = New System.Drawing.Size(388, 20)
        Me.txtPENJAMIN.StyleController = Me.layoutControl
        Me.txtPENJAMIN.TabIndex = 48
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
        Me.barDockControlTop.Size = New System.Drawing.Size(537, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 172)
        Me.barDockControlBottom.Size = New System.Drawing.Size(537, 22)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 172)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(537, 0)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 172)
        '
        'btnSaveNew
        '
        Me.btnSaveNew.Caption = "F2 - Save && New"
        Me.btnSaveNew.Id = 2
        Me.btnSaveNew.Name = "btnSaveNew"
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
        'grdDOCTOR
        '
        Me.grdDOCTOR.EnterMoveNextControl = True
        Me.grdDOCTOR.Location = New System.Drawing.Point(137, 108)
        Me.grdDOCTOR.MenuManager = Me.barManager
        Me.grdDOCTOR.Name = "grdDOCTOR"
        Me.grdDOCTOR.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdDOCTOR.Properties.NullText = ""
        Me.grdDOCTOR.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdDOCTOR.Properties.View = Me.GridView2
        Me.grdDOCTOR.Size = New System.Drawing.Size(388, 20)
        Me.grdDOCTOR.StyleController = Me.layoutControl
        Me.grdDOCTOR.TabIndex = 44
        '
        'GridView2
        '
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2})
        Me.GridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView2.OptionsView.ShowAutoFilterRow = True
        Me.GridView2.OptionsView.ShowGroupPanel = False
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Name Display"
        Me.GridColumn2.FieldName = "NAME_DISPLAY"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        '
        'txtCODE
        '
        Me.txtCODE.Location = New System.Drawing.Point(137, 12)
        Me.txtCODE.MenuManager = Me.barManager
        Me.txtCODE.Name = "txtCODE"
        Me.txtCODE.Properties.ReadOnly = True
        Me.txtCODE.Size = New System.Drawing.Size(388, 20)
        Me.txtCODE.StyleController = Me.layoutControl
        Me.txtCODE.TabIndex = 47
        '
        'grdKDDEPARTMENT
        '
        Me.grdKDDEPARTMENT.EnterMoveNextControl = True
        Me.grdKDDEPARTMENT.Location = New System.Drawing.Point(137, 84)
        Me.grdKDDEPARTMENT.MenuManager = Me.barManager
        Me.grdKDDEPARTMENT.Name = "grdKDDEPARTMENT"
        Me.grdKDDEPARTMENT.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDDEPARTMENT.Properties.NullText = ""
        Me.grdKDDEPARTMENT.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdKDDEPARTMENT.Properties.View = Me.GridView7
        Me.grdKDDEPARTMENT.Size = New System.Drawing.Size(388, 20)
        Me.grdKDDEPARTMENT.StyleController = Me.layoutControl
        Me.grdKDDEPARTMENT.TabIndex = 43
        '
        'GridView7
        '
        Me.GridView7.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn8})
        Me.GridView7.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView7.Name = "GridView7"
        Me.GridView7.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView7.OptionsView.ShowAutoFilterRow = True
        Me.GridView7.OptionsView.ShowGroupPanel = False
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Name Display"
        Me.GridColumn8.FieldName = "NAME_DISPLAY"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 0
        '
        'deDATE_MASUK
        '
        Me.deDATE_MASUK.EditValue = Nothing
        Me.deDATE_MASUK.EnterMoveNextControl = True
        Me.deDATE_MASUK.Location = New System.Drawing.Point(137, 60)
        Me.deDATE_MASUK.MenuManager = Me.barManager
        Me.deDATE_MASUK.Name = "deDATE_MASUK"
        Me.deDATE_MASUK.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deDATE_MASUK.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.deDATE_MASUK.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.deDATE_MASUK.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.deDATE_MASUK.Size = New System.Drawing.Size(388, 20)
        Me.deDATE_MASUK.StyleController = Me.layoutControl
        Me.deDATE_MASUK.TabIndex = 22
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lDATE_MASUK, Me.lKDDEPARTMENT, Me.lKDKUNJUNGAN_POLI, Me.lDOCTOR, Me.LayoutControlItem1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(537, 172)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'lDATE_MASUK
        '
        Me.lDATE_MASUK.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lDATE_MASUK.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lDATE_MASUK.Control = Me.deDATE_MASUK
        Me.lDATE_MASUK.Location = New System.Drawing.Point(0, 48)
        Me.lDATE_MASUK.Name = "lDATE_MASUK"
        Me.lDATE_MASUK.Size = New System.Drawing.Size(517, 24)
        Me.lDATE_MASUK.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lDATE_MASUK.TextSize = New System.Drawing.Size(120, 20)
        Me.lDATE_MASUK.TextToControlDistance = 5
        '
        'lKDDEPARTMENT
        '
        Me.lKDDEPARTMENT.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKDDEPARTMENT.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKDDEPARTMENT.Control = Me.grdKDDEPARTMENT
        Me.lKDDEPARTMENT.Location = New System.Drawing.Point(0, 72)
        Me.lKDDEPARTMENT.Name = "lKDDEPARTMENT"
        Me.lKDDEPARTMENT.Size = New System.Drawing.Size(517, 24)
        Me.lKDDEPARTMENT.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKDDEPARTMENT.TextSize = New System.Drawing.Size(120, 20)
        Me.lKDDEPARTMENT.TextToControlDistance = 5
        '
        'lKDKUNJUNGAN_POLI
        '
        Me.lKDKUNJUNGAN_POLI.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKDKUNJUNGAN_POLI.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKDKUNJUNGAN_POLI.Control = Me.txtCODE
        Me.lKDKUNJUNGAN_POLI.Location = New System.Drawing.Point(0, 0)
        Me.lKDKUNJUNGAN_POLI.Name = "lKDKUNJUNGAN_POLI"
        Me.lKDKUNJUNGAN_POLI.Size = New System.Drawing.Size(517, 24)
        Me.lKDKUNJUNGAN_POLI.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKDKUNJUNGAN_POLI.TextSize = New System.Drawing.Size(120, 20)
        Me.lKDKUNJUNGAN_POLI.TextToControlDistance = 5
        '
        'lDOCTOR
        '
        Me.lDOCTOR.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lDOCTOR.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lDOCTOR.Control = Me.grdDOCTOR
        Me.lDOCTOR.Location = New System.Drawing.Point(0, 96)
        Me.lDOCTOR.Name = "lDOCTOR"
        Me.lDOCTOR.Size = New System.Drawing.Size(517, 56)
        Me.lDOCTOR.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lDOCTOR.TextSize = New System.Drawing.Size(120, 20)
        Me.lDOCTOR.TextToControlDistance = 5
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem1.Control = Me.txtPENJAMIN
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(517, 24)
        Me.LayoutControlItem1.Text = "Penjamin :"
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(120, 20)
        Me.LayoutControlItem1.TextToControlDistance = 5
        '
        'frmPendaftaran_KunjunganPoli
        '
        Me.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(537, 194)
        Me.Controls.Add(Me.layoutControl)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.Name = "frmPendaftaran_KunjunganPoli"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.layoutControl.ResumeLayout(False)
        CType(Me.txtPENJAMIN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdDOCTOR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCODE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDDEPARTMENT.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATE_MASUK.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATE_MASUK.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lDATE_MASUK, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKDDEPARTMENT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKDKUNJUNGAN_POLI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lDOCTOR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents deDATE_MASUK As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lDATE_MASUK As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grdKDDEPARTMENT As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView7 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lKDDEPARTMENT As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtCODE As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lKDKUNJUNGAN_POLI As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grdDOCTOR As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lDOCTOR As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtPENJAMIN As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
End Class
