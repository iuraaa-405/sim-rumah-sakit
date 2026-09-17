<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmReportMutation
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
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReportMutation))
        Me.grv1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.grd = New DevExpress.XtraGrid.GridControl()
        Me.mnuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MasterColumnChooserToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DetailColumnChooserToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.grv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.deDATEFrom = New DevExpress.XtraEditors.DateEdit()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.chkALL = New DevExpress.XtraEditors.CheckEdit()
        Me.grdKDWAREHOUSE = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.grvKDWAREHOUSE = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cboTYPE = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.deDATETo = New DevExpress.XtraEditors.DateEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.lDATEFROM = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lDATETO = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lTYPE = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lWAREHOUSE = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lALL = New DevExpress.XtraLayout.LayoutControlItem()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.panelMenu = New DevExpress.XtraEditors.PanelControl()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.picRefresh = New DevExpress.XtraEditors.PictureEdit()
        Me.picPrint = New DevExpress.XtraEditors.PictureEdit()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.printSystem = New DevExpress.XtraPrinting.PrintingSystem(Me.components)
        Me.printableComponentLink = New DevExpress.XtraPrinting.PrintableComponentLink(Me.components)
        CType(Me.grv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuStrip.SuspendLayout()
        CType(Me.grv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATEFrom.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATEFrom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.chkALL.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDWAREHOUSE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvKDWAREHOUSE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboTYPE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATETo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATETo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lDATEFROM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lDATETO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lTYPE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lWAREHOUSE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lALL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.panelMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelMenu.SuspendLayout()
        CType(Me.picRefresh.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picPrint.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.printSystem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grv1
        '
        Me.grv1.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.EvenRow.BackColor2 = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.EvenRow.BorderColor = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.EvenRow.Options.UseBackColor = True
        Me.grv1.AppearancePrint.EvenRow.Options.UseBorderColor = True
        Me.grv1.AppearancePrint.FilterPanel.BackColor = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.FilterPanel.BackColor2 = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.FilterPanel.BorderColor = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.FilterPanel.Options.UseBackColor = True
        Me.grv1.AppearancePrint.FilterPanel.Options.UseBorderColor = True
        Me.grv1.AppearancePrint.FooterPanel.BackColor = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.FooterPanel.BackColor2 = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.FooterPanel.BorderColor = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.FooterPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.grv1.AppearancePrint.FooterPanel.Options.UseBackColor = True
        Me.grv1.AppearancePrint.FooterPanel.Options.UseBorderColor = True
        Me.grv1.AppearancePrint.FooterPanel.Options.UseFont = True
        Me.grv1.AppearancePrint.GroupFooter.BackColor = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.GroupFooter.BackColor2 = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.GroupFooter.BorderColor = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.GroupFooter.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.grv1.AppearancePrint.GroupFooter.Options.UseBackColor = True
        Me.grv1.AppearancePrint.GroupFooter.Options.UseBorderColor = True
        Me.grv1.AppearancePrint.GroupFooter.Options.UseFont = True
        Me.grv1.AppearancePrint.GroupRow.BackColor = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.GroupRow.BackColor2 = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.GroupRow.BorderColor = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.GroupRow.Options.UseBackColor = True
        Me.grv1.AppearancePrint.GroupRow.Options.UseBorderColor = True
        Me.grv1.AppearancePrint.HeaderPanel.BackColor = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.HeaderPanel.BackColor2 = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.HeaderPanel.BorderColor = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle))
        Me.grv1.AppearancePrint.HeaderPanel.Options.UseBackColor = True
        Me.grv1.AppearancePrint.HeaderPanel.Options.UseBorderColor = True
        Me.grv1.AppearancePrint.HeaderPanel.Options.UseFont = True
        Me.grv1.AppearancePrint.HeaderPanel.Options.UseTextOptions = True
        Me.grv1.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.grv1.AppearancePrint.Lines.BackColor = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.Lines.BackColor2 = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.Lines.BorderColor = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.Lines.Options.UseBackColor = True
        Me.grv1.AppearancePrint.Lines.Options.UseBorderColor = True
        Me.grv1.AppearancePrint.OddRow.BackColor = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.OddRow.BackColor2 = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.OddRow.BorderColor = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.OddRow.Options.UseBackColor = True
        Me.grv1.AppearancePrint.OddRow.Options.UseBorderColor = True
        Me.grv1.AppearancePrint.Preview.BackColor = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.Preview.BackColor2 = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.Preview.BorderColor = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.Preview.Options.UseBackColor = True
        Me.grv1.AppearancePrint.Preview.Options.UseBorderColor = True
        Me.grv1.AppearancePrint.Row.BackColor = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.Row.BackColor2 = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.Row.BorderColor = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.Row.Options.UseBackColor = True
        Me.grv1.AppearancePrint.Row.Options.UseBorderColor = True
        Me.grv1.GridControl = Me.grd
        Me.grv1.Name = "grv1"
        Me.grv1.OptionsBehavior.Editable = False
        Me.grv1.OptionsPrint.EnableAppearanceEvenRow = True
        Me.grv1.OptionsPrint.EnableAppearanceOddRow = True
        Me.grv1.OptionsPrint.PrintDetails = True
        Me.grv1.OptionsPrint.PrintFilterInfo = True
        Me.grv1.OptionsPrint.PrintHorzLines = False
        Me.grv1.OptionsPrint.PrintVertLines = False
        Me.grv1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grv1.OptionsView.ShowAutoFilterRow = True
        Me.grv1.OptionsView.ShowFooter = True
        '
        'grd
        '
        Me.grd.ContextMenuStrip = Me.mnuStrip
        Me.grd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grd.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        GridLevelNode1.LevelTemplate = Me.grv1
        GridLevelNode1.RelationName = "Level1"
        Me.grd.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.grd.Location = New System.Drawing.Point(2, 25)
        Me.grd.MainView = Me.grv
        Me.grd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grd.Name = "grd"
        Me.grd.ShowOnlyPredefinedDetails = True
        Me.grd.Size = New System.Drawing.Size(747, 396)
        Me.grd.TabIndex = 3
        Me.grd.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grv, Me.grv1})
        '
        'mnuStrip
        '
        Me.mnuStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.mnuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MasterColumnChooserToolStripMenuItem, Me.DetailColumnChooserToolStripMenuItem})
        Me.mnuStrip.Name = "mnuStrip"
        Me.mnuStrip.Size = New System.Drawing.Size(237, 52)
        '
        'MasterColumnChooserToolStripMenuItem
        '
        Me.MasterColumnChooserToolStripMenuItem.Name = "MasterColumnChooserToolStripMenuItem"
        Me.MasterColumnChooserToolStripMenuItem.Size = New System.Drawing.Size(236, 24)
        Me.MasterColumnChooserToolStripMenuItem.Text = "Master Column Chooser"
        '
        'DetailColumnChooserToolStripMenuItem
        '
        Me.DetailColumnChooserToolStripMenuItem.Name = "DetailColumnChooserToolStripMenuItem"
        Me.DetailColumnChooserToolStripMenuItem.Size = New System.Drawing.Size(236, 24)
        Me.DetailColumnChooserToolStripMenuItem.Text = "Detail Column Chooser"
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
        Me.grv.OptionsSelection.MultiSelect = True
        Me.grv.OptionsView.ShowAutoFilterRow = True
        Me.grv.OptionsView.ShowFooter = True
        '
        'deDATEFrom
        '
        Me.deDATEFrom.EditValue = Nothing
        Me.deDATEFrom.EnterMoveNextControl = True
        Me.deDATEFrom.Location = New System.Drawing.Point(121, 44)
        Me.deDATEFrom.Name = "deDATEFrom"
        Me.deDATEFrom.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deDATEFrom.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.deDATEFrom.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.deDATEFrom.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.deDATEFrom.Size = New System.Drawing.Size(147, 22)
        Me.deDATEFrom.StyleController = Me.LayoutControl1
        Me.deDATEFrom.TabIndex = 0
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.chkALL)
        Me.LayoutControl1.Controls.Add(Me.grdKDWAREHOUSE)
        Me.LayoutControl1.Controls.Add(Me.cboTYPE)
        Me.LayoutControl1.Controls.Add(Me.LayoutControl2)
        Me.LayoutControl1.Controls.Add(Me.deDATEFrom)
        Me.LayoutControl1.Controls.Add(Me.deDATETo)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(2, 25)
        Me.LayoutControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(747, 144)
        Me.LayoutControl1.TabIndex = 2
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'chkALL
        '
        Me.chkALL.Location = New System.Drawing.Point(215, 100)
        Me.chkALL.Name = "chkALL"
        Me.chkALL.Properties.Caption = "All ?"
        Me.chkALL.Size = New System.Drawing.Size(53, 20)
        Me.chkALL.StyleController = Me.LayoutControl1
        Me.chkALL.TabIndex = 41
        '
        'grdKDWAREHOUSE
        '
        Me.grdKDWAREHOUSE.Location = New System.Drawing.Point(121, 100)
        Me.grdKDWAREHOUSE.Name = "grdKDWAREHOUSE"
        Me.grdKDWAREHOUSE.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDWAREHOUSE.Properties.NullText = ""
        Me.grdKDWAREHOUSE.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdKDWAREHOUSE.Properties.View = Me.grvKDWAREHOUSE
        Me.grdKDWAREHOUSE.Size = New System.Drawing.Size(88, 22)
        Me.grdKDWAREHOUSE.StyleController = Me.LayoutControl1
        Me.grdKDWAREHOUSE.TabIndex = 40
        '
        'grvKDWAREHOUSE
        '
        Me.grvKDWAREHOUSE.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn7})
        Me.grvKDWAREHOUSE.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvKDWAREHOUSE.Name = "grvKDWAREHOUSE"
        Me.grvKDWAREHOUSE.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvKDWAREHOUSE.OptionsView.ShowAutoFilterRow = True
        Me.grvKDWAREHOUSE.OptionsView.ShowGroupPanel = False
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Name Display"
        Me.GridColumn7.FieldName = "NAME_DISPLAY"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 0
        '
        'cboTYPE
        '
        Me.cboTYPE.Location = New System.Drawing.Point(121, 16)
        Me.cboTYPE.Name = "cboTYPE"
        Me.cboTYPE.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboTYPE.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cboTYPE.Size = New System.Drawing.Size(147, 22)
        Me.cboTYPE.StyleController = Me.LayoutControl1
        Me.cboTYPE.TabIndex = 5
        '
        'LayoutControl2
        '
        Me.LayoutControl2.Location = New System.Drawing.Point(274, 16)
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.Root = Me.Root
        Me.LayoutControl2.Size = New System.Drawing.Size(457, 112)
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
        Me.Root.Size = New System.Drawing.Size(457, 112)
        Me.Root.TextVisible = False
        '
        'deDATETo
        '
        Me.deDATETo.EditValue = Nothing
        Me.deDATETo.EnterMoveNextControl = True
        Me.deDATETo.Location = New System.Drawing.Point(121, 72)
        Me.deDATETo.Name = "deDATETo"
        Me.deDATETo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deDATETo.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.deDATETo.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.deDATETo.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.deDATETo.Size = New System.Drawing.Size(147, 22)
        Me.deDATETo.StyleController = Me.LayoutControl1
        Me.deDATETo.TabIndex = 1
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lDATEFROM, Me.lDATETO, Me.LayoutControlItem3, Me.lTYPE, Me.lWAREHOUSE, Me.lALL})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(747, 144)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'lDATEFROM
        '
        Me.lDATEFROM.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lDATEFROM.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lDATEFROM.Control = Me.deDATEFrom
        Me.lDATEFROM.CustomizationFormText = "From Date :"
        Me.lDATEFROM.Location = New System.Drawing.Point(0, 28)
        Me.lDATEFROM.Name = "lDATEFROM"
        Me.lDATEFROM.Size = New System.Drawing.Size(258, 28)
        Me.lDATEFROM.Text = "From Date :"
        Me.lDATEFROM.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lDATEFROM.TextSize = New System.Drawing.Size(100, 20)
        Me.lDATEFROM.TextToControlDistance = 5
        '
        'lDATETO
        '
        Me.lDATETO.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lDATETO.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lDATETO.Control = Me.deDATETo
        Me.lDATETO.CustomizationFormText = "To Date :"
        Me.lDATETO.Location = New System.Drawing.Point(0, 56)
        Me.lDATETO.Name = "lDATETO"
        Me.lDATETO.Size = New System.Drawing.Size(258, 28)
        Me.lDATETO.Text = "To Date :"
        Me.lDATETO.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lDATETO.TextSize = New System.Drawing.Size(100, 20)
        Me.lDATETO.TextToControlDistance = 5
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.LayoutControl2
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(258, 0)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(463, 118)
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'lTYPE
        '
        Me.lTYPE.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lTYPE.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lTYPE.Control = Me.cboTYPE
        Me.lTYPE.CustomizationFormText = "Type :"
        Me.lTYPE.Location = New System.Drawing.Point(0, 0)
        Me.lTYPE.Name = "lTYPE"
        Me.lTYPE.Size = New System.Drawing.Size(258, 28)
        Me.lTYPE.Text = "Type :"
        Me.lTYPE.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lTYPE.TextSize = New System.Drawing.Size(100, 20)
        Me.lTYPE.TextToControlDistance = 5
        '
        'lWAREHOUSE
        '
        Me.lWAREHOUSE.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lWAREHOUSE.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lWAREHOUSE.Control = Me.grdKDWAREHOUSE
        Me.lWAREHOUSE.Location = New System.Drawing.Point(0, 84)
        Me.lWAREHOUSE.Name = "lWAREHOUSE"
        Me.lWAREHOUSE.Size = New System.Drawing.Size(199, 34)
        Me.lWAREHOUSE.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lWAREHOUSE.TextSize = New System.Drawing.Size(100, 20)
        Me.lWAREHOUSE.TextToControlDistance = 5
        '
        'lALL
        '
        Me.lALL.Control = Me.chkALL
        Me.lALL.Location = New System.Drawing.Point(199, 84)
        Me.lALL.Name = "lALL"
        Me.lALL.Size = New System.Drawing.Size(59, 34)
        Me.lALL.TextSize = New System.Drawing.Size(0, 0)
        Me.lALL.TextVisible = False
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.grd)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 281)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(751, 423)
        Me.GroupControl1.TabIndex = 4
        Me.GroupControl1.Text = "Preview"
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
        Me.panelMenu.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.panelMenu.Name = "panelMenu"
        Me.panelMenu.Size = New System.Drawing.Size(751, 110)
        Me.panelMenu.TabIndex = 7
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl6.Location = New System.Drawing.Point(79, 76)
        Me.LabelControl6.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(53, 17)
        Me.LabelControl6.TabIndex = 17
        Me.LabelControl6.Text = "&Refresh"
        '
        'LabelControl7
        '
        Me.LabelControl7.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl7.Location = New System.Drawing.Point(27, 76)
        Me.LabelControl7.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(33, 17)
        Me.LabelControl7.TabIndex = 15
        Me.LabelControl7.Text = "&Print"
        '
        'picRefresh
        '
        Me.picRefresh.EditValue = CType(resources.GetObject("picRefresh.EditValue"), Object)
        Me.picRefresh.Location = New System.Drawing.Point(77, 15)
        Me.picRefresh.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.picRefresh.Name = "picRefresh"
        Me.picRefresh.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picRefresh.Properties.Appearance.Options.UseBackColor = True
        Me.picRefresh.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picRefresh.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picRefresh.Size = New System.Drawing.Size(56, 59)
        Me.picRefresh.TabIndex = 16
        '
        'picPrint
        '
        Me.picPrint.EditValue = CType(resources.GetObject("picPrint.EditValue"), Object)
        Me.picPrint.Location = New System.Drawing.Point(14, 15)
        Me.picPrint.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.picPrint.Name = "picPrint"
        Me.picPrint.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picPrint.Properties.Appearance.Options.UseBackColor = True
        Me.picPrint.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picPrint.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picPrint.Size = New System.Drawing.Size(56, 59)
        Me.picPrint.TabIndex = 14
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.LayoutControl1)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl2.Location = New System.Drawing.Point(0, 110)
        Me.GroupControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(751, 171)
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
        'frmReportMutation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(751, 704)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.GroupControl2)
        Me.Controls.Add(Me.panelMenu)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmReportMutation"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.grv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuStrip.ResumeLayout(False)
        CType(Me.grv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATEFrom.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATEFrom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.chkALL.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDWAREHOUSE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvKDWAREHOUSE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboTYPE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATETo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATETo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lDATEFROM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lDATETO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lTYPE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lWAREHOUSE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lALL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
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
    Friend WithEvents deDATEFrom As DevExpress.XtraEditors.DateEdit
    Friend WithEvents deDATETo As DevExpress.XtraEditors.DateEdit
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents panelMenu As DevExpress.XtraEditors.PanelControl
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents printSystem As DevExpress.XtraPrinting.PrintingSystem
    Friend WithEvents printableComponentLink As DevExpress.XtraPrinting.PrintableComponentLink
    Friend WithEvents mnuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents MasterColumnChooserToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DetailColumnChooserToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents picRefresh As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents picPrint As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents lDATEFROM As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lDATETO As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents cboTYPE As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents lTYPE As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grd As DevExpress.XtraGrid.GridControl
    Friend WithEvents grv1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grdKDWAREHOUSE As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents grvKDWAREHOUSE As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lWAREHOUSE As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents chkALL As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents lALL As DevExpress.XtraLayout.LayoutControlItem
End Class
