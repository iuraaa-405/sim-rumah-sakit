<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmPanggilOnlineJKN
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPanggilOnlineJKN))
        Me.grv1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.grd = New DevExpress.XtraGrid.GridControl()
        Me.grv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.deDATEFrom = New DevExpress.XtraEditors.DateEdit()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.chkPanggil = New DevExpress.XtraEditors.CheckEdit()
        Me.btnSemua = New DevExpress.XtraEditors.SimpleButton()
        Me.grdKDDEPARTMENT = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.lDATEFROM = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lPoli = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
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
        CType(Me.grv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATEFrom.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATEFrom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.chkPanggil.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDDEPARTMENT.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lDATEFROM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lPoli, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.grd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grd.Location = New System.Drawing.Point(2, 20)
        Me.grd.MainView = Me.grv
        Me.grd.Name = "grd"
        Me.grd.ShowOnlyPredefinedDetails = True
        Me.grd.Size = New System.Drawing.Size(640, 368)
        Me.grd.TabIndex = 3
        Me.grd.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grv, Me.grv1})
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
        Me.grv.OptionsView.ShowAutoFilterRow = True
        Me.grv.OptionsView.ShowFooter = True
        Me.grv.OptionsView.ShowGroupPanel = False
        '
        'deDATEFrom
        '
        Me.deDATEFrom.EditValue = Nothing
        Me.deDATEFrom.EnterMoveNextControl = True
        Me.deDATEFrom.Location = New System.Drawing.Point(117, 12)
        Me.deDATEFrom.Name = "deDATEFrom"
        Me.deDATEFrom.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deDATEFrom.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.deDATEFrom.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.deDATEFrom.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.deDATEFrom.Size = New System.Drawing.Size(137, 20)
        Me.deDATEFrom.StyleController = Me.LayoutControl1
        Me.deDATEFrom.TabIndex = 0
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.chkPanggil)
        Me.LayoutControl1.Controls.Add(Me.btnSemua)
        Me.LayoutControl1.Controls.Add(Me.grdKDDEPARTMENT)
        Me.LayoutControl1.Controls.Add(Me.LayoutControl2)
        Me.LayoutControl1.Controls.Add(Me.deDATEFrom)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(2, 20)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(640, 71)
        Me.LayoutControl1.TabIndex = 2
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'chkPanggil
        '
        Me.chkPanggil.Location = New System.Drawing.Point(258, 12)
        Me.chkPanggil.Name = "chkPanggil"
        Me.chkPanggil.Properties.Caption = "Belum di Panggil?"
        Me.chkPanggil.Size = New System.Drawing.Size(105, 19)
        Me.chkPanggil.StyleController = Me.LayoutControl1
        Me.chkPanggil.TabIndex = 45
        '
        'btnSemua
        '
        Me.btnSemua.Location = New System.Drawing.Point(258, 36)
        Me.btnSemua.Name = "btnSemua"
        Me.btnSemua.Size = New System.Drawing.Size(105, 22)
        Me.btnSemua.StyleController = Me.LayoutControl1
        Me.btnSemua.TabIndex = 44
        Me.btnSemua.Text = "Semua"
        '
        'grdKDDEPARTMENT
        '
        Me.grdKDDEPARTMENT.Location = New System.Drawing.Point(117, 36)
        Me.grdKDDEPARTMENT.Name = "grdKDDEPARTMENT"
        Me.grdKDDEPARTMENT.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDDEPARTMENT.Properties.NullText = ""
        Me.grdKDDEPARTMENT.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdKDDEPARTMENT.Properties.View = Me.SearchLookUpEdit1View
        Me.grdKDDEPARTMENT.Size = New System.Drawing.Size(137, 20)
        Me.grdKDDEPARTMENT.StyleController = Me.LayoutControl1
        Me.grdKDDEPARTMENT.TabIndex = 43
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn4})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Name Display"
        Me.GridColumn4.FieldName = "NAME_DISPLAY"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 0
        '
        'LayoutControl2
        '
        Me.LayoutControl2.Location = New System.Drawing.Point(367, 12)
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.Root = Me.Root
        Me.LayoutControl2.Size = New System.Drawing.Size(261, 47)
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
        Me.Root.Size = New System.Drawing.Size(261, 47)
        Me.Root.TextVisible = False
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lDATEFROM, Me.LayoutControlItem3, Me.lPoli, Me.LayoutControlItem1, Me.LayoutControlItem2})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(640, 71)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'lDATEFROM
        '
        Me.lDATEFROM.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lDATEFROM.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lDATEFROM.Control = Me.deDATEFrom
        Me.lDATEFROM.CustomizationFormText = "From Date :"
        Me.lDATEFROM.Location = New System.Drawing.Point(0, 0)
        Me.lDATEFROM.Name = "lDATEFROM"
        Me.lDATEFROM.Size = New System.Drawing.Size(246, 24)
        Me.lDATEFROM.Text = "From Date :"
        Me.lDATEFROM.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lDATEFROM.TextSize = New System.Drawing.Size(100, 20)
        Me.lDATEFROM.TextToControlDistance = 5
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.LayoutControl2
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(355, 0)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(265, 51)
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'lPoli
        '
        Me.lPoli.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lPoli.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lPoli.Control = Me.grdKDDEPARTMENT
        Me.lPoli.Location = New System.Drawing.Point(0, 24)
        Me.lPoli.Name = "lPoli"
        Me.lPoli.Size = New System.Drawing.Size(246, 27)
        Me.lPoli.Text = "Poli :"
        Me.lPoli.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lPoli.TextSize = New System.Drawing.Size(100, 20)
        Me.lPoli.TextToControlDistance = 5
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.btnSemua
        Me.LayoutControlItem1.Location = New System.Drawing.Point(246, 24)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(109, 27)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.chkPanggil
        Me.LayoutControlItem2.Location = New System.Drawing.Point(246, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(109, 24)
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.grd)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 182)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(644, 390)
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
        Me.GroupControl2.Size = New System.Drawing.Size(644, 93)
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
        'frmPanggilOnlineJKN
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(644, 572)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.GroupControl2)
        Me.Controls.Add(Me.panelMenu)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.Name = "frmPanggilOnlineJKN"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.grv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATEFrom.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATEFrom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.chkPanggil.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDDEPARTMENT.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lDATEFROM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lPoli, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents panelMenu As DevExpress.XtraEditors.PanelControl
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents printSystem As DevExpress.XtraPrinting.PrintingSystem
    Friend WithEvents printableComponentLink As DevExpress.XtraPrinting.PrintableComponentLink
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents picRefresh As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents picPrint As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents lDATEFROM As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grd As DevExpress.XtraGrid.GridControl
    Friend WithEvents grv1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grdKDDEPARTMENT As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lPoli As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents btnSemua As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents chkPanggil As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
End Class
