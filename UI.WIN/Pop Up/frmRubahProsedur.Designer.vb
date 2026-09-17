<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmRubahProsedur
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.mnuStripDiagnosa = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BindingSourceDiagnosa = New System.Windows.Forms.BindingSource(Me.components)
        Me.btnOk = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.grdDiagnosa = New DevExpress.XtraGrid.GridControl()
        Me.grvDiagnosa = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colmemo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colkdprpsedur = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.coljumlah = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdKDITEM = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.grvKDITEM = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdKDUOM = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.grvKDUOM = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtREMARKS = New DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit()
        Me.RepositoryItemComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.grdCariDiagnosa = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.grvCariDiagnosaiDRG = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn16 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn17 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.btnBatal = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lCari = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lGrid = New DevExpress.XtraLayout.LayoutControlItem()
        Me.mnuStripDiagnosa.SuspendLayout()
        CType(Me.BindingSourceDiagnosa, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.grdDiagnosa, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvDiagnosa, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDITEM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvKDITEM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDUOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvKDUOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtREMARKS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCariDiagnosa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvCariDiagnosaiDRG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lCari, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'mnuStripDiagnosa
        '
        Me.mnuStripDiagnosa.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DeleteToolStripMenuItem})
        Me.mnuStripDiagnosa.Name = "mnuStripDiagnosa"
        Me.mnuStripDiagnosa.Size = New System.Drawing.Size(108, 26)
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(107, 22)
        Me.DeleteToolStripMenuItem.Text = "Delete"
        '
        'BindingSourceDiagnosa
        '
        Me.BindingSourceDiagnosa.DataSource = GetType(DataAccess.R_IDENTITAS_GROUPER_DATA_PROSEDURIDRG)
        '
        'btnOk
        '
        Me.btnOk.Location = New System.Drawing.Point(2, 335)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(185, 22)
        Me.btnOk.StyleController = Me.LayoutControl1
        Me.btnOk.TabIndex = 22
        Me.btnOk.Text = "Edit Prosedur"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.grdDiagnosa)
        Me.LayoutControl1.Controls.Add(Me.grdCariDiagnosa)
        Me.LayoutControl1.Controls.Add(Me.btnBatal)
        Me.LayoutControl1.Controls.Add(Me.btnOk)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(378, 359)
        Me.LayoutControl1.TabIndex = 26
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'grdDiagnosa
        '
        Me.grdDiagnosa.ContextMenuStrip = Me.mnuStripDiagnosa
        Me.grdDiagnosa.DataSource = Me.BindingSourceDiagnosa
        Me.grdDiagnosa.Location = New System.Drawing.Point(2, 26)
        Me.grdDiagnosa.MainView = Me.grvDiagnosa
        Me.grdDiagnosa.Name = "grdDiagnosa"
        Me.grdDiagnosa.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.grdKDITEM, Me.grdKDUOM, Me.txtREMARKS, Me.RepositoryItemComboBox1})
        Me.grdDiagnosa.Size = New System.Drawing.Size(374, 305)
        Me.grdDiagnosa.TabIndex = 27
        Me.grdDiagnosa.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvDiagnosa})
        '
        'grvDiagnosa
        '
        Me.grvDiagnosa.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colmemo, Me.colkdprpsedur, Me.coljumlah})
        Me.grvDiagnosa.GridControl = Me.grdDiagnosa
        Me.grvDiagnosa.Name = "grvDiagnosa"
        Me.grvDiagnosa.OptionsCustomization.AllowColumnMoving = False
        Me.grvDiagnosa.OptionsCustomization.AllowFilter = False
        Me.grvDiagnosa.OptionsCustomization.AllowGroup = False
        Me.grvDiagnosa.OptionsCustomization.AllowQuickHideColumns = False
        Me.grvDiagnosa.OptionsCustomization.AllowSort = False
        Me.grvDiagnosa.OptionsDetail.EnableMasterViewMode = False
        Me.grvDiagnosa.OptionsFind.AllowFindPanel = False
        Me.grvDiagnosa.OptionsLayout.StoreAllOptions = True
        Me.grvDiagnosa.OptionsLayout.StoreAppearance = True
        Me.grvDiagnosa.OptionsMenu.EnableColumnMenu = False
        Me.grvDiagnosa.OptionsNavigation.AutoFocusNewRow = True
        Me.grvDiagnosa.OptionsNavigation.EnterMoveNextColumn = True
        Me.grvDiagnosa.OptionsView.EnableAppearanceEvenRow = True
        Me.grvDiagnosa.OptionsView.EnableAppearanceOddRow = True
        Me.grvDiagnosa.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.grvDiagnosa.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.grvDiagnosa.OptionsView.ShowFooter = True
        Me.grvDiagnosa.OptionsView.ShowGroupPanel = False
        '
        'colmemo
        '
        Me.colmemo.Caption = "Nama Prosedur"
        Me.colmemo.FieldName = "memo"
        Me.colmemo.Name = "colmemo"
        Me.colmemo.OptionsColumn.AllowEdit = False
        Me.colmemo.OptionsColumn.AllowFocus = False
        Me.colmemo.OptionsColumn.ReadOnly = True
        Me.colmemo.OptionsColumn.TabStop = False
        Me.colmemo.Visible = True
        Me.colmemo.VisibleIndex = 0
        Me.colmemo.Width = 386
        '
        'colkdprpsedur
        '
        Me.colkdprpsedur.Caption = "Kode"
        Me.colkdprpsedur.FieldName = "kdprpsedur"
        Me.colkdprpsedur.Name = "colkdprpsedur"
        Me.colkdprpsedur.OptionsColumn.AllowEdit = False
        Me.colkdprpsedur.OptionsColumn.AllowFocus = False
        Me.colkdprpsedur.OptionsColumn.ReadOnly = True
        Me.colkdprpsedur.OptionsColumn.TabStop = False
        Me.colkdprpsedur.Visible = True
        Me.colkdprpsedur.VisibleIndex = 1
        Me.colkdprpsedur.Width = 120
        '
        'coljumlah
        '
        Me.coljumlah.Caption = "Jumlah"
        Me.coljumlah.FieldName = "jumlah"
        Me.coljumlah.Name = "coljumlah"
        Me.coljumlah.Visible = True
        Me.coljumlah.VisibleIndex = 2
        Me.coljumlah.Width = 141
        '
        'grdKDITEM
        '
        Me.grdKDITEM.AutoHeight = False
        Me.grdKDITEM.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDITEM.Name = "grdKDITEM"
        Me.grdKDITEM.NullText = ""
        Me.grdKDITEM.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdKDITEM.View = Me.grvKDITEM
        '
        'grvKDITEM
        '
        Me.grvKDITEM.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2, Me.GridColumn1})
        Me.grvKDITEM.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvKDITEM.Name = "grvKDITEM"
        Me.grvKDITEM.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvKDITEM.OptionsView.ShowAutoFilterRow = True
        Me.grvKDITEM.OptionsView.ShowGroupPanel = False
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Item Name #2"
        Me.GridColumn2.FieldName = "NMITEM2"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Stok"
        Me.GridColumn1.DisplayFormat.FormatString = "{0:n2}"
        Me.GridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn1.FieldName = "STOK"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 1
        '
        'grdKDUOM
        '
        Me.grdKDUOM.AutoHeight = False
        Me.grdKDUOM.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDUOM.Name = "grdKDUOM"
        Me.grdKDUOM.NullText = ""
        Me.grdKDUOM.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdKDUOM.View = Me.grvKDUOM
        '
        'grvKDUOM
        '
        Me.grvKDUOM.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn4})
        Me.grvKDUOM.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvKDUOM.Name = "grvKDUOM"
        Me.grvKDUOM.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvKDUOM.OptionsView.ShowAutoFilterRow = True
        Me.grvKDUOM.OptionsView.ShowGroupPanel = False
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Description"
        Me.GridColumn4.FieldName = "MEMO"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 0
        '
        'txtREMARKS
        '
        Me.txtREMARKS.AutoHeight = False
        Me.txtREMARKS.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtREMARKS.Name = "txtREMARKS"
        '
        'RepositoryItemComboBox1
        '
        Me.RepositoryItemComboBox1.AutoHeight = False
        Me.RepositoryItemComboBox1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemComboBox1.Items.AddRange(New Object() {"Primary", "Secondary"})
        Me.RepositoryItemComboBox1.Name = "RepositoryItemComboBox1"
        Me.RepositoryItemComboBox1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        '
        'grdCariDiagnosa
        '
        Me.grdCariDiagnosa.Location = New System.Drawing.Point(127, 2)
        Me.grdCariDiagnosa.Name = "grdCariDiagnosa"
        Me.grdCariDiagnosa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdCariDiagnosa.Properties.NullText = ""
        Me.grdCariDiagnosa.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdCariDiagnosa.Properties.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.RepositoryItemCheckEdit2})
        Me.grdCariDiagnosa.Properties.View = Me.grvCariDiagnosaiDRG
        Me.grdCariDiagnosa.Size = New System.Drawing.Size(249, 20)
        Me.grdCariDiagnosa.StyleController = Me.LayoutControl1
        Me.grdCariDiagnosa.TabIndex = 48
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        '
        'RepositoryItemCheckEdit2
        '
        Me.RepositoryItemCheckEdit2.AutoHeight = False
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
        '
        'grvCariDiagnosaiDRG
        '
        Me.grvCariDiagnosaiDRG.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn16, Me.GridColumn17, Me.GridColumn3, Me.GridColumn5})
        Me.grvCariDiagnosaiDRG.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvCariDiagnosaiDRG.Name = "grvCariDiagnosaiDRG"
        Me.grvCariDiagnosaiDRG.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvCariDiagnosaiDRG.OptionsView.ShowAutoFilterRow = True
        Me.grvCariDiagnosaiDRG.OptionsView.ShowGroupPanel = False
        '
        'GridColumn16
        '
        Me.GridColumn16.Caption = "Kode"
        Me.GridColumn16.FieldName = "KDPROSEDUR"
        Me.GridColumn16.Name = "GridColumn16"
        Me.GridColumn16.Visible = True
        Me.GridColumn16.VisibleIndex = 0
        Me.GridColumn16.Width = 116
        '
        'GridColumn17
        '
        Me.GridColumn17.Caption = "Nama Diagnosa ICD-10"
        Me.GridColumn17.FieldName = "MEMO"
        Me.GridColumn17.Name = "GridColumn17"
        Me.GridColumn17.Visible = True
        Me.GridColumn17.VisibleIndex = 1
        Me.GridColumn17.Width = 454
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Valid Code"
        Me.GridColumn3.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.GridColumn3.FieldName = "ISDEFAULT"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 77
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Kode Primery"
        Me.GridColumn5.ColumnEdit = Me.RepositoryItemCheckEdit2
        Me.GridColumn5.FieldName = "ISACTIVE"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 3
        '
        'btnBatal
        '
        Me.btnBatal.Location = New System.Drawing.Point(191, 335)
        Me.btnBatal.Name = "btnBatal"
        Me.btnBatal.Size = New System.Drawing.Size(185, 22)
        Me.btnBatal.StyleController = Me.LayoutControl1
        Me.btnBatal.TabIndex = 23
        Me.btnBatal.Text = "Batal"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2, Me.LayoutControlItem3, Me.lCari, Me.lGrid})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(378, 359)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.btnOk
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 333)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(189, 26)
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.btnBatal
        Me.LayoutControlItem3.Location = New System.Drawing.Point(189, 333)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(189, 26)
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'lCari
        '
        Me.lCari.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lCari.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lCari.Control = Me.grdCariDiagnosa
        Me.lCari.Location = New System.Drawing.Point(0, 0)
        Me.lCari.Name = "lCari"
        Me.lCari.Size = New System.Drawing.Size(378, 24)
        Me.lCari.Text = "Cari Prosedur :"
        Me.lCari.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lCari.TextSize = New System.Drawing.Size(120, 20)
        Me.lCari.TextToControlDistance = 5
        Me.lCari.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'lGrid
        '
        Me.lGrid.Control = Me.grdDiagnosa
        Me.lGrid.Location = New System.Drawing.Point(0, 24)
        Me.lGrid.Name = "lGrid"
        Me.lGrid.Size = New System.Drawing.Size(378, 309)
        Me.lGrid.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lGrid.TextSize = New System.Drawing.Size(0, 0)
        Me.lGrid.TextToControlDistance = 0
        Me.lGrid.TextVisible = False
        Me.lGrid.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'frmRubahProsedur
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(378, 359)
        Me.Controls.Add(Me.LayoutControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmRubahProsedur"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.mnuStripDiagnosa.ResumeLayout(False)
        CType(Me.BindingSourceDiagnosa, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.grdDiagnosa, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvDiagnosa, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDITEM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvKDITEM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDUOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvKDUOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtREMARKS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCariDiagnosa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvCariDiagnosaiDRG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lCari, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnOk As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents btnBatal As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents mnuStripDiagnosa As ContextMenuStrip
    Friend WithEvents DeleteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BindingSourceDiagnosa As BindingSource
    Friend WithEvents grdCariDiagnosa As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents grvCariDiagnosaiDRG As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn17 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lCari As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grdDiagnosa As DevExpress.XtraGrid.GridControl
    Friend WithEvents grvDiagnosa As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colmemo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colkdprpsedur As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents coljumlah As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdKDITEM As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents grvKDITEM As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdKDUOM As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents grvKDUOM As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents txtREMARKS As DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit
    Friend WithEvents lGrid As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
End Class
