<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAntrianDepanManual
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAntrianDepanManual))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnBatal = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDinas = New DevExpress.XtraEditors.SimpleButton()
        Me.btnNonDinas = New DevExpress.XtraEditors.SimpleButton()
        Me.btnUmum = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.SimpleButton0 = New DevExpress.XtraEditors.SimpleButton()
        Me.grdDokter = New DevExpress.XtraGrid.GridControl()
        Me.grvDokter = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemButtonEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdPOLI = New DevExpress.XtraGrid.GridControl()
        Me.grvPoli = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemButtonEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.lPOLI = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lDOKTER = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lButton = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lPILIHULANGPOLI = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.grdDokter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvDokter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemButtonEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdPOLI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvPoli, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemButtonEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lPOLI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lDOKTER, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lButton, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lPILIHULANGPOLI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Number"
        Me.GridColumn6.FieldName = "KDSO"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 0
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.btnBatal, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.btnDinas, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btnNonDinas, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.btnUmum, 0, 2)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(704, 2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(246, 596)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'btnBatal
        '
        Me.btnBatal.Appearance.Font = New System.Drawing.Font("Tahoma", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBatal.Appearance.Options.UseFont = True
        Me.btnBatal.Appearance.Options.UseTextOptions = True
        Me.btnBatal.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.btnBatal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnBatal.Location = New System.Drawing.Point(3, 537)
        Me.btnBatal.LookAndFeel.SkinMaskColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnBatal.LookAndFeel.UseDefaultLookAndFeel = False
        Me.btnBatal.Name = "btnBatal"
        Me.btnBatal.Size = New System.Drawing.Size(240, 56)
        Me.btnBatal.TabIndex = 69
        Me.btnBatal.Text = "BATAL"
        '
        'btnDinas
        '
        Me.btnDinas.Appearance.Font = New System.Drawing.Font("Tahoma", 48.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDinas.Appearance.Options.UseFont = True
        Me.btnDinas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDinas.Location = New System.Drawing.Point(3, 3)
        Me.btnDinas.LookAndFeel.SkinMaskColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnDinas.LookAndFeel.UseDefaultLookAndFeel = False
        Me.btnDinas.Name = "btnDinas"
        Me.btnDinas.Size = New System.Drawing.Size(240, 172)
        Me.btnDinas.TabIndex = 66
        Me.btnDinas.Text = "DINAS"
        '
        'btnNonDinas
        '
        Me.btnNonDinas.Appearance.Font = New System.Drawing.Font("Tahoma", 48.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNonDinas.Appearance.Options.UseFont = True
        Me.btnNonDinas.Appearance.Options.UseTextOptions = True
        Me.btnNonDinas.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.btnNonDinas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnNonDinas.Location = New System.Drawing.Point(3, 181)
        Me.btnNonDinas.LookAndFeel.SkinMaskColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnNonDinas.LookAndFeel.UseDefaultLookAndFeel = False
        Me.btnNonDinas.Name = "btnNonDinas"
        Me.btnNonDinas.Size = New System.Drawing.Size(240, 172)
        Me.btnNonDinas.TabIndex = 68
        Me.btnNonDinas.Text = "NON DINAS (JKN)"
        '
        'btnUmum
        '
        Me.btnUmum.Appearance.Font = New System.Drawing.Font("Tahoma", 48.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUmum.Appearance.Options.UseFont = True
        Me.btnUmum.Appearance.Options.UseTextOptions = True
        Me.btnUmum.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.btnUmum.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnUmum.Location = New System.Drawing.Point(3, 359)
        Me.btnUmum.LookAndFeel.SkinMaskColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnUmum.LookAndFeel.UseDefaultLookAndFeel = False
        Me.btnUmum.Name = "btnUmum"
        Me.btnUmum.Size = New System.Drawing.Size(240, 172)
        Me.btnUmum.TabIndex = 67
        Me.btnUmum.Text = "UMUM"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.SimpleButton1)
        Me.LayoutControl1.Controls.Add(Me.SimpleButton0)
        Me.LayoutControl1.Controls.Add(Me.grdDokter)
        Me.LayoutControl1.Controls.Add(Me.grdPOLI)
        Me.LayoutControl1.Controls.Add(Me.TableLayoutPanel1)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(921, 417, 250, 350)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(952, 626)
        Me.LayoutControl1.TabIndex = 2
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Location = New System.Drawing.Point(2, 602)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(29, 22)
        Me.SimpleButton1.StyleController = Me.LayoutControl1
        Me.SimpleButton1.TabIndex = 81
        Me.SimpleButton1.Text = "X"
        '
        'SimpleButton0
        '
        Me.SimpleButton0.Appearance.Font = New System.Drawing.Font("Tahoma", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SimpleButton0.Appearance.Options.UseFont = True
        Me.SimpleButton0.Location = New System.Drawing.Point(373, 552)
        Me.SimpleButton0.Name = "SimpleButton0"
        Me.SimpleButton0.Size = New System.Drawing.Size(327, 46)
        Me.SimpleButton0.StyleController = Me.LayoutControl1
        Me.SimpleButton0.TabIndex = 80
        Me.SimpleButton0.Text = "PILIH ULANG POLI"
        '
        'grdDokter
        '
        Me.grdDokter.Location = New System.Drawing.Point(373, 2)
        Me.grdDokter.MainView = Me.grvDokter
        Me.grdDokter.Name = "grdDokter"
        Me.grdDokter.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemButtonEdit2})
        Me.grdDokter.Size = New System.Drawing.Size(327, 546)
        Me.grdDokter.TabIndex = 20
        Me.grdDokter.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvDokter})
        '
        'grvDokter
        '
        Me.grvDokter.Appearance.Row.FontSizeDelta = 15
        Me.grvDokter.Appearance.Row.Options.UseFont = True
        Me.grvDokter.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2, Me.GridColumn4, Me.GridColumn7, Me.GridColumn8, Me.GridColumn9})
        Me.grvDokter.GridControl = Me.grdDokter
        Me.grvDokter.Name = "grvDokter"
        Me.grvDokter.OptionsCustomization.AllowColumnMoving = False
        Me.grvDokter.OptionsCustomization.AllowFilter = False
        Me.grvDokter.OptionsCustomization.AllowGroup = False
        Me.grvDokter.OptionsCustomization.AllowQuickHideColumns = False
        Me.grvDokter.OptionsCustomization.AllowSort = False
        Me.grvDokter.OptionsDetail.EnableMasterViewMode = False
        Me.grvDokter.OptionsFind.AllowFindPanel = False
        Me.grvDokter.OptionsLayout.StoreAllOptions = True
        Me.grvDokter.OptionsLayout.StoreAppearance = True
        Me.grvDokter.OptionsMenu.EnableColumnMenu = False
        Me.grvDokter.OptionsNavigation.AutoFocusNewRow = True
        Me.grvDokter.OptionsNavigation.EnterMoveNextColumn = True
        Me.grvDokter.OptionsView.EnableAppearanceEvenRow = True
        Me.grvDokter.OptionsView.EnableAppearanceOddRow = True
        Me.grvDokter.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.grvDokter.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.grvDokter.OptionsView.ShowGroupPanel = False
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Dokter"
        Me.GridColumn2.FieldName = "NAME_DISPLAY"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        Me.GridColumn2.Width = 541
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Pilih"
        Me.GridColumn4.ColumnEdit = Me.RepositoryItemButtonEdit2
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 1
        Me.GridColumn4.Width = 136
        '
        'RepositoryItemButtonEdit2
        '
        Me.RepositoryItemButtonEdit2.AutoHeight = False
        Me.RepositoryItemButtonEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, CType(resources.GetObject("RepositoryItemButtonEdit2.Buttons"), System.Drawing.Image), New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.RepositoryItemButtonEdit2.Name = "RepositoryItemButtonEdit2"
        Me.RepositoryItemButtonEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "GridColumn7"
        Me.GridColumn7.FieldName = "KDDOCTOR"
        Me.GridColumn7.Name = "GridColumn7"
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "GridColumn8"
        Me.GridColumn8.FieldName = "KDJADWALDOKTER"
        Me.GridColumn8.Name = "GridColumn8"
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "GridColumn9"
        Me.GridColumn9.FieldName = "SEQ"
        Me.GridColumn9.Name = "GridColumn9"
        '
        'grdPOLI
        '
        Me.grdPOLI.Location = New System.Drawing.Point(2, 2)
        Me.grdPOLI.MainView = Me.grvPoli
        Me.grdPOLI.Name = "grdPOLI"
        Me.grdPOLI.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemButtonEdit1})
        Me.grdPOLI.Size = New System.Drawing.Size(367, 596)
        Me.grdPOLI.TabIndex = 19
        Me.grdPOLI.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvPoli})
        '
        'grvPoli
        '
        Me.grvPoli.Appearance.Row.FontSizeDelta = 15
        Me.grvPoli.Appearance.Row.Options.UseFont = True
        Me.grvPoli.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn3, Me.GridColumn5})
        Me.grvPoli.GridControl = Me.grdPOLI
        Me.grvPoli.Name = "grvPoli"
        Me.grvPoli.OptionsCustomization.AllowColumnMoving = False
        Me.grvPoli.OptionsCustomization.AllowFilter = False
        Me.grvPoli.OptionsCustomization.AllowGroup = False
        Me.grvPoli.OptionsCustomization.AllowQuickHideColumns = False
        Me.grvPoli.OptionsCustomization.AllowSort = False
        Me.grvPoli.OptionsDetail.EnableMasterViewMode = False
        Me.grvPoli.OptionsFind.AllowFindPanel = False
        Me.grvPoli.OptionsLayout.StoreAllOptions = True
        Me.grvPoli.OptionsLayout.StoreAppearance = True
        Me.grvPoli.OptionsMenu.EnableColumnMenu = False
        Me.grvPoli.OptionsNavigation.AutoFocusNewRow = True
        Me.grvPoli.OptionsNavigation.EnterMoveNextColumn = True
        Me.grvPoli.OptionsView.EnableAppearanceEvenRow = True
        Me.grvPoli.OptionsView.EnableAppearanceOddRow = True
        Me.grvPoli.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.grvPoli.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.grvPoli.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Poli"
        Me.GridColumn1.FieldName = "Poli"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 542
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Pilih"
        Me.GridColumn3.ColumnEdit = Me.RepositoryItemButtonEdit1
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 1
        Me.GridColumn3.Width = 135
        '
        'RepositoryItemButtonEdit1
        '
        Me.RepositoryItemButtonEdit1.AutoHeight = False
        Me.RepositoryItemButtonEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, CType(resources.GetObject("RepositoryItemButtonEdit1.Buttons"), System.Drawing.Image), New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", Nothing, Nothing, True)})
        Me.RepositoryItemButtonEdit1.Name = "RepositoryItemButtonEdit1"
        Me.RepositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "GridColumn5"
        Me.GridColumn5.FieldName = "KDDEPARTMENT"
        Me.GridColumn5.Name = "GridColumn5"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lPOLI, Me.lDOKTER, Me.lButton, Me.lPILIHULANGPOLI, Me.LayoutControlItem1, Me.EmptySpaceItem1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(952, 626)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'lPOLI
        '
        Me.lPOLI.Control = Me.grdPOLI
        Me.lPOLI.Location = New System.Drawing.Point(0, 0)
        Me.lPOLI.Name = "lPOLI"
        Me.lPOLI.Size = New System.Drawing.Size(371, 600)
        Me.lPOLI.TextSize = New System.Drawing.Size(0, 0)
        Me.lPOLI.TextVisible = False
        '
        'lDOKTER
        '
        Me.lDOKTER.Control = Me.grdDokter
        Me.lDOKTER.Location = New System.Drawing.Point(371, 0)
        Me.lDOKTER.Name = "lDOKTER"
        Me.lDOKTER.Size = New System.Drawing.Size(331, 550)
        Me.lDOKTER.TextSize = New System.Drawing.Size(0, 0)
        Me.lDOKTER.TextVisible = False
        '
        'lButton
        '
        Me.lButton.Control = Me.TableLayoutPanel1
        Me.lButton.Location = New System.Drawing.Point(702, 0)
        Me.lButton.Name = "lButton"
        Me.lButton.Size = New System.Drawing.Size(250, 600)
        Me.lButton.TextSize = New System.Drawing.Size(0, 0)
        Me.lButton.TextVisible = False
        '
        'lPILIHULANGPOLI
        '
        Me.lPILIHULANGPOLI.Control = Me.SimpleButton0
        Me.lPILIHULANGPOLI.Location = New System.Drawing.Point(371, 550)
        Me.lPILIHULANGPOLI.Name = "lPILIHULANGPOLI"
        Me.lPILIHULANGPOLI.Size = New System.Drawing.Size(331, 50)
        Me.lPILIHULANGPOLI.TextSize = New System.Drawing.Size(0, 0)
        Me.lPILIHULANGPOLI.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.SimpleButton1
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 600)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(33, 26)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(33, 600)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(919, 26)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'frmAntrianDepanManual
        '
        Me.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(952, 626)
        Me.Controls.Add(Me.LayoutControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Name = "frmAntrianDepanManual"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.grdDokter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvDokter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemButtonEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdPOLI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvPoli, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemButtonEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lPOLI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lDOKTER, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lButton, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lPILIHULANGPOLI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents btnUmum As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDinas As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnNonDinas As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents lButton As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grdDokter As DevExpress.XtraGrid.GridControl
    Friend WithEvents grvDokter As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grdPOLI As DevExpress.XtraGrid.GridControl
    Friend WithEvents grvPoli As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lPOLI As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lDOKTER As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemButtonEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemButtonEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SimpleButton0 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lPILIHULANGPOLI As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents btnBatal As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
End Class
