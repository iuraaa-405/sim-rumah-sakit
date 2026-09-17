<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBrowseWilayah
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
        Me.grd = New DevExpress.XtraGrid.GridControl()
        Me.grv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colKDKELURAHAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colMEMO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Bawah = New DevExpress.XtraEditors.PanelControl()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.grdKDPROPINSI = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdKDKABUPATEN = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdKDKECAMATAN = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Bawah, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDPROPINSI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDKABUPATEN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDKECAMATAN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grd
        '
        Me.grd.EmbeddedNavigator.Buttons.Append.Enabled = False
        Me.grd.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.grd.EmbeddedNavigator.Buttons.CancelEdit.Enabled = False
        Me.grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Me.grd.EmbeddedNavigator.Buttons.Edit.Enabled = False
        Me.grd.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.grd.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        Me.grd.EmbeddedNavigator.Buttons.Remove.Visible = False
        Me.grd.Location = New System.Drawing.Point(2, 74)
        Me.grd.MainView = Me.grv
        Me.grd.Name = "grd"
        Me.grd.Size = New System.Drawing.Size(490, 471)
        Me.grd.TabIndex = 0
        Me.grd.UseEmbeddedNavigator = True
        Me.grd.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grv})
        '
        'grv
        '
        Me.grv.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.Silver
        Me.grv.Appearance.ColumnFilterButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.grv.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.Silver
        Me.grv.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Gray
        Me.grv.Appearance.ColumnFilterButton.Options.UseBackColor = True
        Me.grv.Appearance.ColumnFilterButton.Options.UseBorderColor = True
        Me.grv.Appearance.ColumnFilterButton.Options.UseForeColor = True
        Me.grv.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.grv.Appearance.ColumnFilterButtonActive.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.grv.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.grv.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Blue
        Me.grv.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.grv.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
        Me.grv.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
        Me.grv.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.grv.Appearance.Empty.Options.UseBackColor = True
        Me.grv.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.grv.Appearance.EvenRow.BackColor2 = System.Drawing.Color.GhostWhite
        Me.grv.Appearance.EvenRow.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.grv.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
        Me.grv.Appearance.EvenRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.grv.Appearance.EvenRow.Options.UseBackColor = True
        Me.grv.Appearance.EvenRow.Options.UseFont = True
        Me.grv.Appearance.EvenRow.Options.UseForeColor = True
        Me.grv.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.grv.Appearance.FilterCloseButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(118, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.grv.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.grv.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
        Me.grv.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.grv.Appearance.FilterCloseButton.Options.UseBackColor = True
        Me.grv.Appearance.FilterCloseButton.Options.UseBorderColor = True
        Me.grv.Appearance.FilterCloseButton.Options.UseForeColor = True
        Me.grv.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(135, Byte), Integer))
        Me.grv.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.grv.Appearance.FilterPanel.ForeColor = System.Drawing.Color.White
        Me.grv.Appearance.FilterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.grv.Appearance.FilterPanel.Options.UseBackColor = True
        Me.grv.Appearance.FilterPanel.Options.UseForeColor = True
        Me.grv.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.grv.Appearance.FixedLine.Options.UseBackColor = True
        Me.grv.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.grv.Appearance.FocusedCell.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.grv.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black
        Me.grv.Appearance.FocusedCell.Options.UseBackColor = True
        Me.grv.Appearance.FocusedCell.Options.UseFont = True
        Me.grv.Appearance.FocusedCell.Options.UseForeColor = True
        Me.grv.Appearance.FocusedRow.BackColor = System.Drawing.Color.Navy
        Me.grv.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(178, Byte), Integer))
        Me.grv.Appearance.FocusedRow.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.grv.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
        Me.grv.Appearance.FocusedRow.Options.UseBackColor = True
        Me.grv.Appearance.FocusedRow.Options.UseFont = True
        Me.grv.Appearance.FocusedRow.Options.UseForeColor = True
        Me.grv.Appearance.FooterPanel.BackColor = System.Drawing.Color.Silver
        Me.grv.Appearance.FooterPanel.BorderColor = System.Drawing.Color.Silver
        Me.grv.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.grv.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
        Me.grv.Appearance.FooterPanel.Options.UseBackColor = True
        Me.grv.Appearance.FooterPanel.Options.UseBorderColor = True
        Me.grv.Appearance.FooterPanel.Options.UseFont = True
        Me.grv.Appearance.FooterPanel.Options.UseForeColor = True
        Me.grv.Appearance.GroupButton.BackColor = System.Drawing.Color.Silver
        Me.grv.Appearance.GroupButton.BorderColor = System.Drawing.Color.Silver
        Me.grv.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black
        Me.grv.Appearance.GroupButton.Options.UseBackColor = True
        Me.grv.Appearance.GroupButton.Options.UseBorderColor = True
        Me.grv.Appearance.GroupButton.Options.UseForeColor = True
        Me.grv.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.grv.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.grv.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
        Me.grv.Appearance.GroupFooter.Options.UseBackColor = True
        Me.grv.Appearance.GroupFooter.Options.UseBorderColor = True
        Me.grv.Appearance.GroupFooter.Options.UseForeColor = True
        Me.grv.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.grv.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
        Me.grv.Appearance.GroupPanel.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.grv.Appearance.GroupPanel.ForeColor = System.Drawing.Color.White
        Me.grv.Appearance.GroupPanel.Options.UseBackColor = True
        Me.grv.Appearance.GroupPanel.Options.UseFont = True
        Me.grv.Appearance.GroupPanel.Options.UseForeColor = True
        Me.grv.Appearance.GroupRow.BackColor = System.Drawing.Color.Gray
        Me.grv.Appearance.GroupRow.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.grv.Appearance.GroupRow.ForeColor = System.Drawing.Color.Silver
        Me.grv.Appearance.GroupRow.Options.UseBackColor = True
        Me.grv.Appearance.GroupRow.Options.UseFont = True
        Me.grv.Appearance.GroupRow.Options.UseForeColor = True
        Me.grv.Appearance.HeaderPanel.BackColor = System.Drawing.Color.Silver
        Me.grv.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.Silver
        Me.grv.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.grv.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
        Me.grv.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.grv.Appearance.HeaderPanel.Options.UseBorderColor = True
        Me.grv.Appearance.HeaderPanel.Options.UseFont = True
        Me.grv.Appearance.HeaderPanel.Options.UseForeColor = True
        Me.grv.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.Gray
        Me.grv.Appearance.HideSelectionRow.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.grv.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.grv.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.grv.Appearance.HideSelectionRow.Options.UseFont = True
        Me.grv.Appearance.HideSelectionRow.Options.UseForeColor = True
        Me.grv.Appearance.HorzLine.BackColor = System.Drawing.Color.Silver
        Me.grv.Appearance.HorzLine.Options.UseBackColor = True
        Me.grv.Appearance.OddRow.BackColor = System.Drawing.Color.White
        Me.grv.Appearance.OddRow.BackColor2 = System.Drawing.Color.White
        Me.grv.Appearance.OddRow.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.grv.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
        Me.grv.Appearance.OddRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal
        Me.grv.Appearance.OddRow.Options.UseBackColor = True
        Me.grv.Appearance.OddRow.Options.UseFont = True
        Me.grv.Appearance.OddRow.Options.UseForeColor = True
        Me.grv.Appearance.Preview.BackColor = System.Drawing.Color.White
        Me.grv.Appearance.Preview.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.grv.Appearance.Preview.ForeColor = System.Drawing.Color.Navy
        Me.grv.Appearance.Preview.Options.UseBackColor = True
        Me.grv.Appearance.Preview.Options.UseFont = True
        Me.grv.Appearance.Preview.Options.UseForeColor = True
        Me.grv.Appearance.Row.BackColor = System.Drawing.Color.White
        Me.grv.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.grv.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.grv.Appearance.Row.Options.UseBackColor = True
        Me.grv.Appearance.Row.Options.UseFont = True
        Me.grv.Appearance.Row.Options.UseForeColor = True
        Me.grv.Appearance.RowSeparator.BackColor = System.Drawing.Color.White
        Me.grv.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.grv.Appearance.RowSeparator.Options.UseBackColor = True
        Me.grv.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.grv.Appearance.SelectedRow.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.grv.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White
        Me.grv.Appearance.SelectedRow.Options.UseBackColor = True
        Me.grv.Appearance.SelectedRow.Options.UseFont = True
        Me.grv.Appearance.SelectedRow.Options.UseForeColor = True
        Me.grv.Appearance.TopNewRow.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.grv.Appearance.TopNewRow.Options.UseFont = True
        Me.grv.Appearance.VertLine.BackColor = System.Drawing.Color.Silver
        Me.grv.Appearance.VertLine.Options.UseBackColor = True
        Me.grv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colKDKELURAHAN, Me.colMEMO})
        Me.grv.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grv.GridControl = Me.grd
        Me.grv.Name = "grv"
        Me.grv.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.grv.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.grv.OptionsBehavior.AllowIncrementalSearch = True
        Me.grv.OptionsBehavior.AutoExpandAllGroups = True
        Me.grv.OptionsBehavior.Editable = False
        Me.grv.OptionsDetail.EnableMasterViewMode = False
        Me.grv.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grv.OptionsView.EnableAppearanceEvenRow = True
        Me.grv.OptionsView.EnableAppearanceOddRow = True
        Me.grv.OptionsView.ShowAutoFilterRow = True
        Me.grv.OptionsView.ShowChildrenInGroupPanel = True
        Me.grv.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways
        Me.grv.OptionsView.ShowGroupPanel = False
        '
        'colKDKELURAHAN
        '
        Me.colKDKELURAHAN.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.colKDKELURAHAN.AppearanceCell.Options.UseFont = True
        Me.colKDKELURAHAN.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.colKDKELURAHAN.AppearanceHeader.Options.UseFont = True
        Me.colKDKELURAHAN.AppearanceHeader.Options.UseTextOptions = True
        Me.colKDKELURAHAN.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.colKDKELURAHAN.Caption = "Code"
        Me.colKDKELURAHAN.FieldName = "KDKELURAHAN"
        Me.colKDKELURAHAN.Name = "colKDKELURAHAN"
        Me.colKDKELURAHAN.Width = 147
        '
        'colMEMO
        '
        Me.colMEMO.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.colMEMO.AppearanceCell.Options.UseFont = True
        Me.colMEMO.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.colMEMO.AppearanceHeader.Options.UseFont = True
        Me.colMEMO.AppearanceHeader.Options.UseTextOptions = True
        Me.colMEMO.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.colMEMO.Caption = "Nama Kelurahan / Desa"
        Me.colMEMO.FieldName = "MEMO"
        Me.colMEMO.Name = "colMEMO"
        Me.colMEMO.Visible = True
        Me.colMEMO.VisibleIndex = 0
        Me.colMEMO.Width = 246
        '
        'Bawah
        '
        Me.Bawah.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Bawah.Location = New System.Drawing.Point(0, 547)
        Me.Bawah.Margin = New System.Windows.Forms.Padding(4)
        Me.Bawah.Name = "Bawah"
        Me.Bawah.Size = New System.Drawing.Size(494, 25)
        Me.Bawah.TabIndex = 29
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.grd)
        Me.LayoutControl1.Controls.Add(Me.grdKDPROPINSI)
        Me.LayoutControl1.Controls.Add(Me.grdKDKABUPATEN)
        Me.LayoutControl1.Controls.Add(Me.grdKDKECAMATAN)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(494, 547)
        Me.LayoutControl1.TabIndex = 30
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.LayoutControlItem4})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(494, 547)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.grd
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(494, 475)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem2.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem2.Control = Me.grdKDPROPINSI
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(494, 24)
        Me.LayoutControlItem2.Text = "Propinsi :"
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(100, 20)
        Me.LayoutControlItem2.TextToControlDistance = 5
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem3.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem3.Control = Me.grdKDKABUPATEN
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(494, 24)
        Me.LayoutControlItem3.Text = "Kabupaten/Kota :"
        Me.LayoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(100, 20)
        Me.LayoutControlItem3.TextToControlDistance = 5
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem4.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem4.Control = Me.grdKDKECAMATAN
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(494, 24)
        Me.LayoutControlItem4.Text = "Kecamatan :"
        Me.LayoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(100, 20)
        Me.LayoutControlItem4.TextToControlDistance = 5
        '
        'grdKDPROPINSI
        '
        Me.grdKDPROPINSI.EditValue = ""
        Me.grdKDPROPINSI.Location = New System.Drawing.Point(107, 2)
        Me.grdKDPROPINSI.Name = "grdKDPROPINSI"
        Me.grdKDPROPINSI.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDPROPINSI.Properties.NullText = ""
        Me.grdKDPROPINSI.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdKDPROPINSI.Properties.View = Me.SearchLookUpEdit1View
        Me.grdKDPROPINSI.Size = New System.Drawing.Size(385, 20)
        Me.grdKDPROPINSI.StyleController = Me.LayoutControl1
        Me.grdKDPROPINSI.TabIndex = 31
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Nama Propinsi"
        Me.GridColumn3.FieldName = "MEMO"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        '
        'grdKDKABUPATEN
        '
        Me.grdKDKABUPATEN.EditValue = ""
        Me.grdKDKABUPATEN.Location = New System.Drawing.Point(107, 26)
        Me.grdKDKABUPATEN.Name = "grdKDKABUPATEN"
        Me.grdKDKABUPATEN.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDKABUPATEN.Properties.NullText = ""
        Me.grdKDKABUPATEN.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdKDKABUPATEN.Properties.View = Me.GridView1
        Me.grdKDKABUPATEN.Size = New System.Drawing.Size(385, 20)
        Me.grdKDKABUPATEN.StyleController = Me.LayoutControl1
        Me.grdKDKABUPATEN.TabIndex = 31
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn4})
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Nama Kabupaten/Kota"
        Me.GridColumn4.FieldName = "MEMO"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 0
        '
        'grdKDKECAMATAN
        '
        Me.grdKDKECAMATAN.EditValue = ""
        Me.grdKDKECAMATAN.Location = New System.Drawing.Point(107, 50)
        Me.grdKDKECAMATAN.Name = "grdKDKECAMATAN"
        Me.grdKDKECAMATAN.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDKECAMATAN.Properties.NullText = ""
        Me.grdKDKECAMATAN.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdKDKECAMATAN.Properties.View = Me.GridView2
        Me.grdKDKECAMATAN.Size = New System.Drawing.Size(385, 20)
        Me.grdKDKECAMATAN.StyleController = Me.LayoutControl1
        Me.grdKDKECAMATAN.TabIndex = 31
        '
        'GridView2
        '
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1})
        Me.GridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView2.OptionsView.ShowAutoFilterRow = True
        Me.GridView2.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Nama Kecamatan"
        Me.GridColumn1.FieldName = "MEMO"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'frmBrowseWilayah
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(494, 572)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Controls.Add(Me.Bawah)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Name = "frmBrowseWilayah"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Browse Wilayah"
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Bawah, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDPROPINSI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDKABUPATEN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDKECAMATAN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grd As DevExpress.XtraGrid.GridControl
    Friend WithEvents grv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colKDKELURAHAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colMEMO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Bawah As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grdKDPROPINSI As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdKDKABUPATEN As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdKDKECAMATAN As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
End Class
