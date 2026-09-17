<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReportBalanceSheet
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReportBalanceSheet))
        Me.mnuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MasterColumnChooserToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.cboMONTH = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cboYEAR = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.lMONTH = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lYEAR = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.grd = New DevExpress.XtraGrid.GridControl()
        Me.grv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.panelMenu = New DevExpress.XtraEditors.PanelControl()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.picRefresh = New DevExpress.XtraEditors.PictureEdit()
        Me.picPrint = New DevExpress.XtraEditors.PictureEdit()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.printSystem = New DevExpress.XtraPrinting.PrintingSystem(Me.components)
        Me.printableComponentLink = New DevExpress.XtraPrinting.PrintableComponentLink(Me.components)
        Me.mnuStrip.SuspendLayout()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboMONTH.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboYEAR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lMONTH, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lYEAR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.panelMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelMenu.SuspendLayout()
        CType(Me.picRefresh.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picPrint.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.printSystem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'mnuStrip
        '
        Me.mnuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MasterColumnChooserToolStripMenuItem})
        Me.mnuStrip.Name = "mnuStrip"
        Me.mnuStrip.Size = New System.Drawing.Size(204, 26)
        '
        'MasterColumnChooserToolStripMenuItem
        '
        Me.MasterColumnChooserToolStripMenuItem.Name = "MasterColumnChooserToolStripMenuItem"
        Me.MasterColumnChooserToolStripMenuItem.Size = New System.Drawing.Size(203, 22)
        Me.MasterColumnChooserToolStripMenuItem.Text = "Master Column Chooser"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.LayoutControl2)
        Me.LayoutControl1.Controls.Add(Me.cboMONTH)
        Me.LayoutControl1.Controls.Add(Me.cboYEAR)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(2, 20)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(640, 96)
        Me.LayoutControl1.TabIndex = 2
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'LayoutControl2
        '
        Me.LayoutControl2.Location = New System.Drawing.Point(234, 12)
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.Root = Me.Root
        Me.LayoutControl2.Size = New System.Drawing.Size(394, 72)
        Me.LayoutControl2.TabIndex = 4
        Me.LayoutControl2.Text = "LayoutControl2"
        '
        'Root
        '
        Me.Root.CustomizationFormText = "Root"
        Me.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.Root.GroupBordersVisible = False
        Me.Root.Location = New System.Drawing.Point(0, 0)
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(394, 72)
        Me.Root.TextVisible = False
        '
        'cboMONTH
        '
        Me.cboMONTH.Location = New System.Drawing.Point(117, 12)
        Me.cboMONTH.Name = "cboMONTH"
        Me.cboMONTH.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboMONTH.Properties.DisplayFormat.FormatString = "d"
        Me.cboMONTH.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.cboMONTH.Properties.EditFormat.FormatString = "d"
        Me.cboMONTH.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.cboMONTH.Size = New System.Drawing.Size(113, 20)
        Me.cboMONTH.StyleController = Me.LayoutControl1
        Me.cboMONTH.TabIndex = 0
        '
        'cboYEAR
        '
        Me.cboYEAR.Location = New System.Drawing.Point(117, 36)
        Me.cboYEAR.Name = "cboYEAR"
        Me.cboYEAR.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboYEAR.Properties.DisplayFormat.FormatString = "d"
        Me.cboYEAR.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.cboYEAR.Properties.EditFormat.FormatString = "d"
        Me.cboYEAR.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.cboYEAR.Size = New System.Drawing.Size(113, 20)
        Me.cboYEAR.StyleController = Me.LayoutControl1
        Me.cboYEAR.TabIndex = 1
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lMONTH, Me.lYEAR, Me.LayoutControlItem3})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(640, 96)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'lMONTH
        '
        Me.lMONTH.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lMONTH.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lMONTH.Control = Me.cboMONTH
        Me.lMONTH.CustomizationFormText = "From Date :"
        Me.lMONTH.Location = New System.Drawing.Point(0, 0)
        Me.lMONTH.Name = "lMONTH"
        Me.lMONTH.Size = New System.Drawing.Size(222, 24)
        Me.lMONTH.Text = "Month :"
        Me.lMONTH.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lMONTH.TextSize = New System.Drawing.Size(100, 20)
        Me.lMONTH.TextToControlDistance = 5
        '
        'lYEAR
        '
        Me.lYEAR.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lYEAR.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lYEAR.Control = Me.cboYEAR
        Me.lYEAR.CustomizationFormText = "To Date :"
        Me.lYEAR.Location = New System.Drawing.Point(0, 24)
        Me.lYEAR.Name = "lYEAR"
        Me.lYEAR.Size = New System.Drawing.Size(222, 52)
        Me.lYEAR.Text = "Year :"
        Me.lYEAR.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lYEAR.TextSize = New System.Drawing.Size(100, 20)
        Me.lYEAR.TextToControlDistance = 5
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.LayoutControl2
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(222, 0)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(398, 76)
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.grd)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 207)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(644, 365)
        Me.GroupControl1.TabIndex = 4
        Me.GroupControl1.Text = "Preview"
        '
        'grd
        '
        Me.grd.ContextMenuStrip = Me.mnuStrip
        Me.grd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grd.Location = New System.Drawing.Point(2, 20)
        Me.grd.MainView = Me.grv
        Me.grd.Name = "grd"
        Me.grd.ShowOnlyPredefinedDetails = True
        Me.grd.Size = New System.Drawing.Size(640, 343)
        Me.grd.TabIndex = 4
        Me.grd.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grv})
        '
        'grv
        '
        Me.grv.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.EvenRow.BackColor2 = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.EvenRow.BorderColor = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.EvenRow.Options.UseBackColor = True
        Me.grv.AppearancePrint.EvenRow.Options.UseBorderColor = True
        Me.grv.AppearancePrint.FilterPanel.BackColor = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.FilterPanel.BackColor2 = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.FilterPanel.BorderColor = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.FilterPanel.Options.UseBackColor = True
        Me.grv.AppearancePrint.FilterPanel.Options.UseBorderColor = True
        Me.grv.AppearancePrint.FooterPanel.BackColor = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.FooterPanel.BackColor2 = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.FooterPanel.BorderColor = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.FooterPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.grv.AppearancePrint.FooterPanel.Options.UseBackColor = True
        Me.grv.AppearancePrint.FooterPanel.Options.UseBorderColor = True
        Me.grv.AppearancePrint.FooterPanel.Options.UseFont = True
        Me.grv.AppearancePrint.GroupFooter.BackColor = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.GroupFooter.BackColor2 = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.GroupFooter.BorderColor = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.GroupFooter.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.grv.AppearancePrint.GroupFooter.Options.UseBackColor = True
        Me.grv.AppearancePrint.GroupFooter.Options.UseBorderColor = True
        Me.grv.AppearancePrint.GroupFooter.Options.UseFont = True
        Me.grv.AppearancePrint.GroupRow.BackColor = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.GroupRow.BackColor2 = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.GroupRow.BorderColor = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.GroupRow.Options.UseBackColor = True
        Me.grv.AppearancePrint.GroupRow.Options.UseBorderColor = True
        Me.grv.AppearancePrint.HeaderPanel.BackColor = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.HeaderPanel.BackColor2 = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.HeaderPanel.BorderColor = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle))
        Me.grv.AppearancePrint.HeaderPanel.Options.UseBackColor = True
        Me.grv.AppearancePrint.HeaderPanel.Options.UseBorderColor = True
        Me.grv.AppearancePrint.HeaderPanel.Options.UseFont = True
        Me.grv.AppearancePrint.HeaderPanel.Options.UseTextOptions = True
        Me.grv.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.grv.AppearancePrint.Lines.BackColor = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.Lines.BackColor2 = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.Lines.BorderColor = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.Lines.Options.UseBackColor = True
        Me.grv.AppearancePrint.Lines.Options.UseBorderColor = True
        Me.grv.AppearancePrint.OddRow.BackColor = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.OddRow.BackColor2 = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.OddRow.BorderColor = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.OddRow.Options.UseBackColor = True
        Me.grv.AppearancePrint.OddRow.Options.UseBorderColor = True
        Me.grv.AppearancePrint.Preview.BackColor = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.Preview.BackColor2 = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.Preview.BorderColor = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.Preview.Options.UseBackColor = True
        Me.grv.AppearancePrint.Preview.Options.UseBorderColor = True
        Me.grv.AppearancePrint.Row.BackColor = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.Row.BackColor2 = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.Row.BorderColor = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.Row.Options.UseBackColor = True
        Me.grv.AppearancePrint.Row.Options.UseBorderColor = True
        Me.grv.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grv.GridControl = Me.grd
        Me.grv.Name = "grv"
        Me.grv.OptionsBehavior.Editable = False
        Me.grv.OptionsBehavior.ReadOnly = True
        Me.grv.OptionsDetail.SmartDetailHeight = True
        Me.grv.OptionsPrint.EnableAppearanceEvenRow = True
        Me.grv.OptionsPrint.EnableAppearanceOddRow = True
        Me.grv.OptionsPrint.ExpandAllDetails = True
        Me.grv.OptionsPrint.PrintDetails = True
        Me.grv.OptionsPrint.PrintFilterInfo = True
        Me.grv.OptionsPrint.PrintHorzLines = False
        Me.grv.OptionsPrint.PrintVertLines = False
        Me.grv.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grv.OptionsView.AllowCellMerge = True
        Me.grv.OptionsView.ShowAutoFilterRow = True
        Me.grv.OptionsView.ShowFooter = True
        '
        'panelMenu
        '
        Me.panelMenu.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.panelMenu.Appearance.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.panelMenu.Appearance.Options.UseBackColor = True
        Me.panelMenu.Controls.Add(Me.LabelControl6)
        Me.panelMenu.Controls.Add(Me.LabelControl7)
        Me.panelMenu.Controls.Add(Me.picRefresh)
        Me.panelMenu.Controls.Add(Me.picPrint)
        Me.panelMenu.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelMenu.Location = New System.Drawing.Point(0, 0)
        Me.panelMenu.Name = "panelMenu"
        Me.panelMenu.Size = New System.Drawing.Size(644, 89)
        Me.panelMenu.TabIndex = 7
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl6.Location = New System.Drawing.Point(68, 62)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(44, 13)
        Me.LabelControl6.TabIndex = 17
        Me.LabelControl6.Text = "&Refresh"
        '
        'LabelControl7
        '
        Me.LabelControl7.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl7.Location = New System.Drawing.Point(23, 62)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(27, 13)
        Me.LabelControl7.TabIndex = 15
        Me.LabelControl7.Text = "&Print"
        '
        'picRefresh
        '
        Me.picRefresh.EditValue = CType(resources.GetObject("picRefresh.EditValue"), Object)
        Me.picRefresh.Location = New System.Drawing.Point(66, 12)
        Me.picRefresh.Name = "picRefresh"
        Me.picRefresh.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picRefresh.Properties.Appearance.Options.UseBackColor = True
        Me.picRefresh.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picRefresh.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picRefresh.Size = New System.Drawing.Size(48, 48)
        Me.picRefresh.TabIndex = 16
        '
        'picPrint
        '
        Me.picPrint.EditValue = CType(resources.GetObject("picPrint.EditValue"), Object)
        Me.picPrint.Location = New System.Drawing.Point(12, 12)
        Me.picPrint.Name = "picPrint"
        Me.picPrint.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picPrint.Properties.Appearance.Options.UseBackColor = True
        Me.picPrint.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picPrint.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picPrint.Size = New System.Drawing.Size(48, 48)
        Me.picPrint.TabIndex = 14
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.LayoutControl1)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl2.Location = New System.Drawing.Point(0, 89)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(644, 118)
        Me.GroupControl2.TabIndex = 4
        Me.GroupControl2.Text = "Filter"
        '
        'printSystem
        '
        Me.printSystem.Links.AddRange(New Object() {Me.printableComponentLink})
        '
        'printableComponentLink
        '
        Me.printableComponentLink.Landscape = True
        Me.printableComponentLink.PaperKind = System.Drawing.Printing.PaperKind.Custom
        Me.printableComponentLink.PrintingSystemBase = Me.printSystem
        '
        'frmReportBalanceSheet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(644, 572)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.GroupControl2)
        Me.Controls.Add(Me.panelMenu)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmReportBalanceSheet"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.mnuStrip.ResumeLayout(False)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboMONTH.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboYEAR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lMONTH, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lYEAR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.panelMenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelMenu.ResumeLayout(False)
        Me.panelMenu.PerformLayout()
        CType(Me.picRefresh.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picPrint.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.printSystem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents panelMenu As DevExpress.XtraEditors.PanelControl
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents printSystem As DevExpress.XtraPrinting.PrintingSystem
    Friend WithEvents printableComponentLink As DevExpress.XtraPrinting.PrintableComponentLink
    Friend WithEvents mnuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents MasterColumnChooserToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents picRefresh As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents picPrint As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents lMONTH As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lYEAR As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents cboMONTH As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cboYEAR As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents grd As DevExpress.XtraGrid.GridControl
    Friend WithEvents grv As DevExpress.XtraGrid.Views.Grid.GridView
End Class
