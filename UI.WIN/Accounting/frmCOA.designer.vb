<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCOA
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
        Me.grdPARENTKDCOA = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.barManager = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barTop = New DevExpress.XtraBars.Bar()
        Me.btnSaveNew = New DevExpress.XtraBars.BarButtonItem()
        Me.btnSaveClose = New DevExpress.XtraBars.BarButtonItem()
        Me.btnClose = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.grvPARENTKDCOA = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colKDCOA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colNMCOA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cboGROUPS = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.rbTYPE = New DevExpress.XtraEditors.RadioGroup()
        Me.txtNMCOA = New DevExpress.XtraEditors.TextEdit()
        Me.txtKDCOA = New DevExpress.XtraEditors.TextEdit()
        Me.cboDC = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.lKDCOA = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lNMCOA = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lDC = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lTYPE = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lGROUPS = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lPARENTKDCOA = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.layoutControl.SuspendLayout()
        CType(Me.grdPARENTKDCOA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvPARENTKDCOA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboGROUPS.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbTYPE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNMCOA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtKDCOA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboDC.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKDCOA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lNMCOA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lDC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lTYPE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lGROUPS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lPARENTKDCOA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'layoutControl
        '
        Me.layoutControl.Controls.Add(Me.grdPARENTKDCOA)
        Me.layoutControl.Controls.Add(Me.cboGROUPS)
        Me.layoutControl.Controls.Add(Me.rbTYPE)
        Me.layoutControl.Controls.Add(Me.txtNMCOA)
        Me.layoutControl.Controls.Add(Me.txtKDCOA)
        Me.layoutControl.Controls.Add(Me.cboDC)
        Me.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutControl.Location = New System.Drawing.Point(0, 0)
        Me.layoutControl.Name = "layoutControl"
        Me.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(774, 238, 250, 350)
        Me.layoutControl.Root = Me.LayoutControlGroup1
        Me.layoutControl.Size = New System.Drawing.Size(411, 181)
        Me.layoutControl.TabIndex = 0
        Me.layoutControl.Text = "LayoutControl1"
        '
        'grdPARENTKDCOA
        '
        Me.grdPARENTKDCOA.EditValue = ""
        Me.grdPARENTKDCOA.EnterMoveNextControl = True
        Me.grdPARENTKDCOA.Location = New System.Drawing.Point(117, 101)
        Me.grdPARENTKDCOA.MenuManager = Me.barManager
        Me.grdPARENTKDCOA.Name = "grdPARENTKDCOA"
        Me.grdPARENTKDCOA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdPARENTKDCOA.Properties.NullText = ""
        Me.grdPARENTKDCOA.Properties.PopupFormMinSize = New System.Drawing.Size(500, 250)
        Me.grdPARENTKDCOA.Properties.View = Me.grvPARENTKDCOA
        Me.grdPARENTKDCOA.Size = New System.Drawing.Size(282, 20)
        Me.grdPARENTKDCOA.StyleController = Me.layoutControl
        Me.grdPARENTKDCOA.TabIndex = 16
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
        Me.barManager.MaxItemId = 6
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
        Me.barDockControlTop.Size = New System.Drawing.Size(411, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 181)
        Me.barDockControlBottom.Size = New System.Drawing.Size(411, 22)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 181)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(411, 0)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 181)
        '
        'grvPARENTKDCOA
        '
        Me.grvPARENTKDCOA.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colKDCOA, Me.colNMCOA})
        Me.grvPARENTKDCOA.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvPARENTKDCOA.Name = "grvPARENTKDCOA"
        Me.grvPARENTKDCOA.OptionsFind.AlwaysVisible = True
        Me.grvPARENTKDCOA.OptionsFind.ShowFindButton = False
        Me.grvPARENTKDCOA.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvPARENTKDCOA.OptionsView.ShowAutoFilterRow = True
        Me.grvPARENTKDCOA.OptionsView.ShowGroupPanel = False
        '
        'colKDCOA
        '
        Me.colKDCOA.Caption = "Akun"
        Me.colKDCOA.FieldName = "KDCOA"
        Me.colKDCOA.Name = "colKDCOA"
        Me.colKDCOA.Visible = True
        Me.colKDCOA.VisibleIndex = 0
        '
        'colNMCOA
        '
        Me.colNMCOA.Caption = "Name"
        Me.colNMCOA.FieldName = "NMCOA"
        Me.colNMCOA.Name = "colNMCOA"
        Me.colNMCOA.Visible = True
        Me.colNMCOA.VisibleIndex = 1
        '
        'cboGROUPS
        '
        Me.cboGROUPS.EditValue = "ASET"
        Me.cboGROUPS.EnterMoveNextControl = True
        Me.cboGROUPS.Location = New System.Drawing.Point(117, 125)
        Me.cboGROUPS.MenuManager = Me.barManager
        Me.cboGROUPS.Name = "cboGROUPS"
        Me.cboGROUPS.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboGROUPS.Properties.Items.AddRange(New Object() {"ASET", "KEWAJIBAN", "MODAL", "PENDAPATAN", "PENGELUARAN", "LAIN - LAIN"})
        Me.cboGROUPS.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cboGROUPS.Size = New System.Drawing.Size(282, 20)
        Me.cboGROUPS.StyleController = Me.layoutControl
        Me.cboGROUPS.TabIndex = 15
        '
        'rbTYPE
        '
        Me.rbTYPE.EnterMoveNextControl = True
        Me.rbTYPE.Location = New System.Drawing.Point(117, 60)
        Me.rbTYPE.MenuManager = Me.barManager
        Me.rbTYPE.Name = "rbTYPE"
        Me.rbTYPE.Properties.Columns = 2
        Me.rbTYPE.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(Nothing, "Header"), New DevExpress.XtraEditors.Controls.RadioGroupItem(Nothing, "Detil")})
        Me.rbTYPE.Size = New System.Drawing.Size(282, 37)
        Me.rbTYPE.StyleController = Me.layoutControl
        Me.rbTYPE.TabIndex = 14
        '
        'txtNMCOA
        '
        Me.txtNMCOA.EnterMoveNextControl = True
        Me.txtNMCOA.Location = New System.Drawing.Point(117, 36)
        Me.txtNMCOA.Name = "txtNMCOA"
        Me.txtNMCOA.Size = New System.Drawing.Size(282, 20)
        Me.txtNMCOA.StyleController = Me.layoutControl
        Me.txtNMCOA.TabIndex = 8
        '
        'txtKDCOA
        '
        Me.txtKDCOA.EnterMoveNextControl = True
        Me.txtKDCOA.Location = New System.Drawing.Point(117, 12)
        Me.txtKDCOA.Name = "txtKDCOA"
        Me.txtKDCOA.Size = New System.Drawing.Size(282, 20)
        Me.txtKDCOA.StyleController = Me.layoutControl
        Me.txtKDCOA.TabIndex = 6
        '
        'cboDC
        '
        Me.cboDC.EditValue = "D"
        Me.cboDC.Location = New System.Drawing.Point(117, 149)
        Me.cboDC.Name = "cboDC"
        Me.cboDC.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboDC.Properties.Items.AddRange(New Object() {"D", "C"})
        Me.cboDC.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cboDC.Size = New System.Drawing.Size(282, 20)
        Me.cboDC.StyleController = Me.layoutControl
        Me.cboDC.TabIndex = 10
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lKDCOA, Me.lNMCOA, Me.lDC, Me.lTYPE, Me.lGROUPS, Me.lPARENTKDCOA})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(411, 181)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'lKDCOA
        '
        Me.lKDCOA.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKDCOA.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKDCOA.Control = Me.txtKDCOA
        Me.lKDCOA.CustomizationFormText = "Middle :"
        Me.lKDCOA.Location = New System.Drawing.Point(0, 0)
        Me.lKDCOA.Name = "lKDCOA"
        Me.lKDCOA.Size = New System.Drawing.Size(391, 24)
        Me.lKDCOA.Text = "Account * :"
        Me.lKDCOA.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKDCOA.TextSize = New System.Drawing.Size(100, 20)
        Me.lKDCOA.TextToControlDistance = 5
        '
        'lNMCOA
        '
        Me.lNMCOA.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lNMCOA.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lNMCOA.Control = Me.txtNMCOA
        Me.lNMCOA.CustomizationFormText = "Company :"
        Me.lNMCOA.Location = New System.Drawing.Point(0, 24)
        Me.lNMCOA.Name = "lNMCOA"
        Me.lNMCOA.Size = New System.Drawing.Size(391, 24)
        Me.lNMCOA.Text = "Name :"
        Me.lNMCOA.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lNMCOA.TextSize = New System.Drawing.Size(100, 20)
        Me.lNMCOA.TextToControlDistance = 5
        '
        'lDC
        '
        Me.lDC.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lDC.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lDC.Control = Me.cboDC
        Me.lDC.CustomizationFormText = "Display on Print :"
        Me.lDC.Location = New System.Drawing.Point(0, 137)
        Me.lDC.Name = "lDC"
        Me.lDC.Size = New System.Drawing.Size(391, 24)
        Me.lDC.Text = "Debit / Credit :"
        Me.lDC.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lDC.TextSize = New System.Drawing.Size(100, 20)
        Me.lDC.TextToControlDistance = 5
        '
        'lTYPE
        '
        Me.lTYPE.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lTYPE.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lTYPE.Control = Me.rbTYPE
        Me.lTYPE.CustomizationFormText = "Type :"
        Me.lTYPE.Location = New System.Drawing.Point(0, 48)
        Me.lTYPE.Name = "lTYPE"
        Me.lTYPE.Size = New System.Drawing.Size(391, 41)
        Me.lTYPE.Text = "Type :"
        Me.lTYPE.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lTYPE.TextSize = New System.Drawing.Size(100, 20)
        Me.lTYPE.TextToControlDistance = 5
        '
        'lGROUPS
        '
        Me.lGROUPS.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lGROUPS.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lGROUPS.Control = Me.cboGROUPS
        Me.lGROUPS.CustomizationFormText = "Group :"
        Me.lGROUPS.Location = New System.Drawing.Point(0, 113)
        Me.lGROUPS.Name = "lGROUPS"
        Me.lGROUPS.Size = New System.Drawing.Size(391, 24)
        Me.lGROUPS.Text = "Group :"
        Me.lGROUPS.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lGROUPS.TextSize = New System.Drawing.Size(100, 20)
        Me.lGROUPS.TextToControlDistance = 5
        '
        'lPARENTKDCOA
        '
        Me.lPARENTKDCOA.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lPARENTKDCOA.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lPARENTKDCOA.Control = Me.grdPARENTKDCOA
        Me.lPARENTKDCOA.CustomizationFormText = "Parent Account :"
        Me.lPARENTKDCOA.Location = New System.Drawing.Point(0, 89)
        Me.lPARENTKDCOA.Name = "lPARENTKDCOA"
        Me.lPARENTKDCOA.Size = New System.Drawing.Size(391, 24)
        Me.lPARENTKDCOA.Text = "Parent Account :"
        Me.lPARENTKDCOA.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lPARENTKDCOA.TextSize = New System.Drawing.Size(100, 20)
        Me.lPARENTKDCOA.TextToControlDistance = 5
        '
        'frmCOA
        '
        Me.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(411, 203)
        Me.Controls.Add(Me.layoutControl)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.Name = "frmCOA"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Akun - Edit Form"
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.layoutControl.ResumeLayout(False)
        CType(Me.grdPARENTKDCOA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvPARENTKDCOA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboGROUPS.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbTYPE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNMCOA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtKDCOA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboDC.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKDCOA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lNMCOA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lDC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lTYPE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lGROUPS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lPARENTKDCOA, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents layoutControl As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents txtNMCOA As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtKDCOA As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lKDCOA As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lNMCOA As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lDC As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents barManager As DevExpress.XtraBars.BarManager
    Friend WithEvents barTop As DevExpress.XtraBars.Bar
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents btnSaveNew As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnClose As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnSaveClose As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents rbTYPE As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents lTYPE As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents cboDC As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cboGROUPS As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents lGROUPS As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grdPARENTKDCOA As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents grvPARENTKDCOA As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lPARENTKDCOA As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents colKDCOA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colNMCOA As DevExpress.XtraGrid.Columns.GridColumn
End Class
