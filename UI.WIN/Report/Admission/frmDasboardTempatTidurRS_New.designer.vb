<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDasboardTempatTidurRS_New
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
        Me.mnuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MasterColumnChooserToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DetailColumnChooserToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.printSystem = New DevExpress.XtraPrinting.PrintingSystem(Me.components)
        Me.printableComponentLink = New DevExpress.XtraPrinting.PrintableComponentLink(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel17 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl30 = New DevExpress.XtraEditors.LabelControl()
        Me.lblNONKELAS = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel16 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl28 = New DevExpress.XtraEditors.LabelControl()
        Me.lblISOLASI = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel15 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl26 = New DevExpress.XtraEditors.LabelControl()
        Me.lblHCU = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel14 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl24 = New DevExpress.XtraEditors.LabelControl()
        Me.lblBERSALIN = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel13 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl22 = New DevExpress.XtraEditors.LabelControl()
        Me.lblUGD = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel12 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl20 = New DevExpress.XtraEditors.LabelControl()
        Me.lblIGD = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel11 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl18 = New DevExpress.XtraEditors.LabelControl()
        Me.lblPICU = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel10 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl16 = New DevExpress.XtraEditors.LabelControl()
        Me.lblNICU = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel9 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl14 = New DevExpress.XtraEditors.LabelControl()
        Me.lblICCU = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel8 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl12 = New DevExpress.XtraEditors.LabelControl()
        Me.lblICU = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel7 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl10 = New DevExpress.XtraEditors.LabelControl()
        Me.lblKELASIII = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.lblKELASII = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.lblKELASI = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.lblUTAMA = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.lblVIP = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.lblVVIP = New DevExpress.XtraEditors.LabelControl()
        Me.mnuStrip.SuspendLayout()
        CType(Me.printSystem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel17.SuspendLayout()
        Me.TableLayoutPanel16.SuspendLayout()
        Me.TableLayoutPanel15.SuspendLayout()
        Me.TableLayoutPanel14.SuspendLayout()
        Me.TableLayoutPanel13.SuspendLayout()
        Me.TableLayoutPanel12.SuspendLayout()
        Me.TableLayoutPanel11.SuspendLayout()
        Me.TableLayoutPanel10.SuspendLayout()
        Me.TableLayoutPanel9.SuspendLayout()
        Me.TableLayoutPanel8.SuspendLayout()
        Me.TableLayoutPanel7.SuspendLayout()
        Me.TableLayoutPanel6.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'mnuStrip
        '
        Me.mnuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MasterColumnChooserToolStripMenuItem, Me.DetailColumnChooserToolStripMenuItem})
        Me.mnuStrip.Name = "mnuStrip"
        Me.mnuStrip.Size = New System.Drawing.Size(204, 48)
        '
        'MasterColumnChooserToolStripMenuItem
        '
        Me.MasterColumnChooserToolStripMenuItem.Name = "MasterColumnChooserToolStripMenuItem"
        Me.MasterColumnChooserToolStripMenuItem.Size = New System.Drawing.Size(203, 22)
        Me.MasterColumnChooserToolStripMenuItem.Text = "Master Column Chooser"
        '
        'DetailColumnChooserToolStripMenuItem
        '
        Me.DetailColumnChooserToolStripMenuItem.Name = "DetailColumnChooserToolStripMenuItem"
        Me.DetailColumnChooserToolStripMenuItem.Size = New System.Drawing.Size(203, 22)
        Me.DetailColumnChooserToolStripMenuItem.Text = "Detail Column Chooser"
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
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.Blue
        Me.TableLayoutPanel1.ColumnCount = 8
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel17, 7, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel16, 6, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel15, 5, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel14, 4, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel13, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel12, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel11, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel10, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel9, 7, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel8, 6, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel7, 5, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel6, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel5, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel4, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel3, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(951, 586)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'TableLayoutPanel17
        '
        Me.TableLayoutPanel17.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel17.ColumnCount = 1
        Me.TableLayoutPanel17.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel17.Controls.Add(Me.LabelControl30, 0, 0)
        Me.TableLayoutPanel17.Controls.Add(Me.lblNONKELAS, 0, 1)
        Me.TableLayoutPanel17.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel17.Location = New System.Drawing.Point(829, 296)
        Me.TableLayoutPanel17.Name = "TableLayoutPanel17"
        Me.TableLayoutPanel17.RowCount = 2
        Me.TableLayoutPanel17.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.95652!))
        Me.TableLayoutPanel17.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.04348!))
        Me.TableLayoutPanel17.Size = New System.Drawing.Size(119, 287)
        Me.TableLayoutPanel17.TabIndex = 15
        '
        'LabelControl30
        '
        Me.LabelControl30.Appearance.BackColor = System.Drawing.Color.Green
        Me.LabelControl30.Appearance.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl30.Appearance.ForeColor = System.Drawing.Color.White
        Me.LabelControl30.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl30.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl30.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl30.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl30.Name = "LabelControl30"
        Me.LabelControl30.Size = New System.Drawing.Size(113, 100)
        Me.LabelControl30.TabIndex = 0
        Me.LabelControl30.Text = "NON KELAS"
        '
        'lblNONKELAS
        '
        Me.lblNONKELAS.Appearance.BackColor = System.Drawing.Color.Gray
        Me.lblNONKELAS.Appearance.Font = New System.Drawing.Font("Tahoma", 36.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNONKELAS.Appearance.ForeColor = System.Drawing.Color.White
        Me.lblNONKELAS.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblNONKELAS.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblNONKELAS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblNONKELAS.Location = New System.Drawing.Point(3, 109)
        Me.lblNONKELAS.Name = "lblNONKELAS"
        Me.lblNONKELAS.Size = New System.Drawing.Size(113, 175)
        Me.lblNONKELAS.TabIndex = 1
        Me.lblNONKELAS.Text = "0"
        '
        'TableLayoutPanel16
        '
        Me.TableLayoutPanel16.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel16.ColumnCount = 1
        Me.TableLayoutPanel16.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel16.Controls.Add(Me.LabelControl28, 0, 0)
        Me.TableLayoutPanel16.Controls.Add(Me.lblISOLASI, 0, 1)
        Me.TableLayoutPanel16.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel16.Location = New System.Drawing.Point(711, 296)
        Me.TableLayoutPanel16.Name = "TableLayoutPanel16"
        Me.TableLayoutPanel16.RowCount = 2
        Me.TableLayoutPanel16.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.95652!))
        Me.TableLayoutPanel16.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.04348!))
        Me.TableLayoutPanel16.Size = New System.Drawing.Size(112, 287)
        Me.TableLayoutPanel16.TabIndex = 14
        '
        'LabelControl28
        '
        Me.LabelControl28.Appearance.BackColor = System.Drawing.Color.Green
        Me.LabelControl28.Appearance.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl28.Appearance.ForeColor = System.Drawing.Color.White
        Me.LabelControl28.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl28.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl28.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl28.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl28.Name = "LabelControl28"
        Me.LabelControl28.Size = New System.Drawing.Size(106, 100)
        Me.LabelControl28.TabIndex = 0
        Me.LabelControl28.Text = "ISOLASI"
        '
        'lblISOLASI
        '
        Me.lblISOLASI.Appearance.BackColor = System.Drawing.Color.Gray
        Me.lblISOLASI.Appearance.Font = New System.Drawing.Font("Tahoma", 36.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblISOLASI.Appearance.ForeColor = System.Drawing.Color.White
        Me.lblISOLASI.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblISOLASI.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblISOLASI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblISOLASI.Location = New System.Drawing.Point(3, 109)
        Me.lblISOLASI.Name = "lblISOLASI"
        Me.lblISOLASI.Size = New System.Drawing.Size(106, 175)
        Me.lblISOLASI.TabIndex = 1
        Me.lblISOLASI.Text = "0"
        '
        'TableLayoutPanel15
        '
        Me.TableLayoutPanel15.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel15.ColumnCount = 1
        Me.TableLayoutPanel15.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel15.Controls.Add(Me.LabelControl26, 0, 0)
        Me.TableLayoutPanel15.Controls.Add(Me.lblHCU, 0, 1)
        Me.TableLayoutPanel15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel15.Location = New System.Drawing.Point(593, 296)
        Me.TableLayoutPanel15.Name = "TableLayoutPanel15"
        Me.TableLayoutPanel15.RowCount = 2
        Me.TableLayoutPanel15.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.95652!))
        Me.TableLayoutPanel15.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.04348!))
        Me.TableLayoutPanel15.Size = New System.Drawing.Size(112, 287)
        Me.TableLayoutPanel15.TabIndex = 13
        '
        'LabelControl26
        '
        Me.LabelControl26.Appearance.BackColor = System.Drawing.Color.Green
        Me.LabelControl26.Appearance.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl26.Appearance.ForeColor = System.Drawing.Color.White
        Me.LabelControl26.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl26.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl26.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl26.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl26.Name = "LabelControl26"
        Me.LabelControl26.Size = New System.Drawing.Size(106, 100)
        Me.LabelControl26.TabIndex = 0
        Me.LabelControl26.Text = "HCU"
        '
        'lblHCU
        '
        Me.lblHCU.Appearance.BackColor = System.Drawing.Color.Gray
        Me.lblHCU.Appearance.Font = New System.Drawing.Font("Tahoma", 36.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHCU.Appearance.ForeColor = System.Drawing.Color.White
        Me.lblHCU.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblHCU.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblHCU.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblHCU.Location = New System.Drawing.Point(3, 109)
        Me.lblHCU.Name = "lblHCU"
        Me.lblHCU.Size = New System.Drawing.Size(106, 175)
        Me.lblHCU.TabIndex = 1
        Me.lblHCU.Text = "0"
        '
        'TableLayoutPanel14
        '
        Me.TableLayoutPanel14.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel14.ColumnCount = 1
        Me.TableLayoutPanel14.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel14.Controls.Add(Me.LabelControl24, 0, 0)
        Me.TableLayoutPanel14.Controls.Add(Me.lblBERSALIN, 0, 1)
        Me.TableLayoutPanel14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel14.Location = New System.Drawing.Point(475, 296)
        Me.TableLayoutPanel14.Name = "TableLayoutPanel14"
        Me.TableLayoutPanel14.RowCount = 2
        Me.TableLayoutPanel14.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.95652!))
        Me.TableLayoutPanel14.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.04348!))
        Me.TableLayoutPanel14.Size = New System.Drawing.Size(112, 287)
        Me.TableLayoutPanel14.TabIndex = 12
        '
        'LabelControl24
        '
        Me.LabelControl24.Appearance.BackColor = System.Drawing.Color.Green
        Me.LabelControl24.Appearance.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl24.Appearance.ForeColor = System.Drawing.Color.White
        Me.LabelControl24.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl24.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl24.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl24.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl24.Name = "LabelControl24"
        Me.LabelControl24.Size = New System.Drawing.Size(106, 100)
        Me.LabelControl24.TabIndex = 0
        Me.LabelControl24.Text = "BERSALIN"
        '
        'lblBERSALIN
        '
        Me.lblBERSALIN.Appearance.BackColor = System.Drawing.Color.Gray
        Me.lblBERSALIN.Appearance.Font = New System.Drawing.Font("Tahoma", 36.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBERSALIN.Appearance.ForeColor = System.Drawing.Color.White
        Me.lblBERSALIN.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblBERSALIN.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblBERSALIN.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblBERSALIN.Location = New System.Drawing.Point(3, 109)
        Me.lblBERSALIN.Name = "lblBERSALIN"
        Me.lblBERSALIN.Size = New System.Drawing.Size(106, 175)
        Me.lblBERSALIN.TabIndex = 1
        Me.lblBERSALIN.Text = "0"
        '
        'TableLayoutPanel13
        '
        Me.TableLayoutPanel13.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel13.ColumnCount = 1
        Me.TableLayoutPanel13.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel13.Controls.Add(Me.LabelControl22, 0, 0)
        Me.TableLayoutPanel13.Controls.Add(Me.lblUGD, 0, 1)
        Me.TableLayoutPanel13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel13.Location = New System.Drawing.Point(357, 296)
        Me.TableLayoutPanel13.Name = "TableLayoutPanel13"
        Me.TableLayoutPanel13.RowCount = 2
        Me.TableLayoutPanel13.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.95652!))
        Me.TableLayoutPanel13.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.04348!))
        Me.TableLayoutPanel13.Size = New System.Drawing.Size(112, 287)
        Me.TableLayoutPanel13.TabIndex = 11
        '
        'LabelControl22
        '
        Me.LabelControl22.Appearance.BackColor = System.Drawing.Color.Green
        Me.LabelControl22.Appearance.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl22.Appearance.ForeColor = System.Drawing.Color.White
        Me.LabelControl22.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl22.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl22.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl22.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl22.Name = "LabelControl22"
        Me.LabelControl22.Size = New System.Drawing.Size(106, 100)
        Me.LabelControl22.TabIndex = 0
        Me.LabelControl22.Text = "UGD"
        '
        'lblUGD
        '
        Me.lblUGD.Appearance.BackColor = System.Drawing.Color.Gray
        Me.lblUGD.Appearance.Font = New System.Drawing.Font("Tahoma", 36.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUGD.Appearance.ForeColor = System.Drawing.Color.White
        Me.lblUGD.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblUGD.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblUGD.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblUGD.Location = New System.Drawing.Point(3, 109)
        Me.lblUGD.Name = "lblUGD"
        Me.lblUGD.Size = New System.Drawing.Size(106, 175)
        Me.lblUGD.TabIndex = 1
        Me.lblUGD.Text = "0"
        '
        'TableLayoutPanel12
        '
        Me.TableLayoutPanel12.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel12.ColumnCount = 1
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel12.Controls.Add(Me.LabelControl20, 0, 0)
        Me.TableLayoutPanel12.Controls.Add(Me.lblIGD, 0, 1)
        Me.TableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel12.Location = New System.Drawing.Point(239, 296)
        Me.TableLayoutPanel12.Name = "TableLayoutPanel12"
        Me.TableLayoutPanel12.RowCount = 2
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.95652!))
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.04348!))
        Me.TableLayoutPanel12.Size = New System.Drawing.Size(112, 287)
        Me.TableLayoutPanel12.TabIndex = 10
        '
        'LabelControl20
        '
        Me.LabelControl20.Appearance.BackColor = System.Drawing.Color.Green
        Me.LabelControl20.Appearance.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl20.Appearance.ForeColor = System.Drawing.Color.White
        Me.LabelControl20.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl20.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl20.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl20.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl20.Name = "LabelControl20"
        Me.LabelControl20.Size = New System.Drawing.Size(106, 100)
        Me.LabelControl20.TabIndex = 0
        Me.LabelControl20.Text = "IGD"
        '
        'lblIGD
        '
        Me.lblIGD.Appearance.BackColor = System.Drawing.Color.Gray
        Me.lblIGD.Appearance.Font = New System.Drawing.Font("Tahoma", 36.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIGD.Appearance.ForeColor = System.Drawing.Color.White
        Me.lblIGD.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblIGD.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblIGD.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblIGD.Location = New System.Drawing.Point(3, 109)
        Me.lblIGD.Name = "lblIGD"
        Me.lblIGD.Size = New System.Drawing.Size(106, 175)
        Me.lblIGD.TabIndex = 1
        Me.lblIGD.Text = "0"
        '
        'TableLayoutPanel11
        '
        Me.TableLayoutPanel11.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel11.ColumnCount = 1
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel11.Controls.Add(Me.LabelControl18, 0, 0)
        Me.TableLayoutPanel11.Controls.Add(Me.lblPICU, 0, 1)
        Me.TableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel11.Location = New System.Drawing.Point(121, 296)
        Me.TableLayoutPanel11.Name = "TableLayoutPanel11"
        Me.TableLayoutPanel11.RowCount = 2
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.95652!))
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.04348!))
        Me.TableLayoutPanel11.Size = New System.Drawing.Size(112, 287)
        Me.TableLayoutPanel11.TabIndex = 9
        '
        'LabelControl18
        '
        Me.LabelControl18.Appearance.BackColor = System.Drawing.Color.Green
        Me.LabelControl18.Appearance.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl18.Appearance.ForeColor = System.Drawing.Color.White
        Me.LabelControl18.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl18.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl18.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl18.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl18.Name = "LabelControl18"
        Me.LabelControl18.Size = New System.Drawing.Size(106, 100)
        Me.LabelControl18.TabIndex = 0
        Me.LabelControl18.Text = "PICU"
        '
        'lblPICU
        '
        Me.lblPICU.Appearance.BackColor = System.Drawing.Color.Gray
        Me.lblPICU.Appearance.Font = New System.Drawing.Font("Tahoma", 36.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPICU.Appearance.ForeColor = System.Drawing.Color.White
        Me.lblPICU.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblPICU.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblPICU.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPICU.Location = New System.Drawing.Point(3, 109)
        Me.lblPICU.Name = "lblPICU"
        Me.lblPICU.Size = New System.Drawing.Size(106, 175)
        Me.lblPICU.TabIndex = 1
        Me.lblPICU.Text = "0"
        '
        'TableLayoutPanel10
        '
        Me.TableLayoutPanel10.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel10.ColumnCount = 1
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel10.Controls.Add(Me.LabelControl16, 0, 0)
        Me.TableLayoutPanel10.Controls.Add(Me.lblNICU, 0, 1)
        Me.TableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel10.Location = New System.Drawing.Point(3, 296)
        Me.TableLayoutPanel10.Name = "TableLayoutPanel10"
        Me.TableLayoutPanel10.RowCount = 2
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.95652!))
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.04348!))
        Me.TableLayoutPanel10.Size = New System.Drawing.Size(112, 287)
        Me.TableLayoutPanel10.TabIndex = 8
        '
        'LabelControl16
        '
        Me.LabelControl16.Appearance.BackColor = System.Drawing.Color.Green
        Me.LabelControl16.Appearance.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl16.Appearance.ForeColor = System.Drawing.Color.White
        Me.LabelControl16.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl16.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl16.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl16.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl16.Name = "LabelControl16"
        Me.LabelControl16.Size = New System.Drawing.Size(106, 100)
        Me.LabelControl16.TabIndex = 0
        Me.LabelControl16.Text = "NICU"
        '
        'lblNICU
        '
        Me.lblNICU.Appearance.BackColor = System.Drawing.Color.Gray
        Me.lblNICU.Appearance.Font = New System.Drawing.Font("Tahoma", 36.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNICU.Appearance.ForeColor = System.Drawing.Color.White
        Me.lblNICU.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblNICU.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblNICU.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblNICU.Location = New System.Drawing.Point(3, 109)
        Me.lblNICU.Name = "lblNICU"
        Me.lblNICU.Size = New System.Drawing.Size(106, 175)
        Me.lblNICU.TabIndex = 1
        Me.lblNICU.Text = "0"
        '
        'TableLayoutPanel9
        '
        Me.TableLayoutPanel9.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel9.ColumnCount = 1
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel9.Controls.Add(Me.LabelControl14, 0, 0)
        Me.TableLayoutPanel9.Controls.Add(Me.lblICCU, 0, 1)
        Me.TableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel9.Location = New System.Drawing.Point(829, 3)
        Me.TableLayoutPanel9.Name = "TableLayoutPanel9"
        Me.TableLayoutPanel9.RowCount = 2
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.95652!))
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.04348!))
        Me.TableLayoutPanel9.Size = New System.Drawing.Size(119, 287)
        Me.TableLayoutPanel9.TabIndex = 7
        '
        'LabelControl14
        '
        Me.LabelControl14.Appearance.BackColor = System.Drawing.Color.Green
        Me.LabelControl14.Appearance.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl14.Appearance.ForeColor = System.Drawing.Color.White
        Me.LabelControl14.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl14.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl14.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl14.Name = "LabelControl14"
        Me.LabelControl14.Size = New System.Drawing.Size(113, 100)
        Me.LabelControl14.TabIndex = 0
        Me.LabelControl14.Text = "ICCU"
        '
        'lblICCU
        '
        Me.lblICCU.Appearance.BackColor = System.Drawing.Color.Gray
        Me.lblICCU.Appearance.Font = New System.Drawing.Font("Tahoma", 36.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblICCU.Appearance.ForeColor = System.Drawing.Color.White
        Me.lblICCU.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblICCU.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblICCU.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblICCU.Location = New System.Drawing.Point(3, 109)
        Me.lblICCU.Name = "lblICCU"
        Me.lblICCU.Size = New System.Drawing.Size(113, 175)
        Me.lblICCU.TabIndex = 1
        Me.lblICCU.Text = "0"
        '
        'TableLayoutPanel8
        '
        Me.TableLayoutPanel8.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel8.ColumnCount = 1
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel8.Controls.Add(Me.LabelControl12, 0, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.lblICU, 0, 1)
        Me.TableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel8.Location = New System.Drawing.Point(711, 3)
        Me.TableLayoutPanel8.Name = "TableLayoutPanel8"
        Me.TableLayoutPanel8.RowCount = 2
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.95652!))
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.04348!))
        Me.TableLayoutPanel8.Size = New System.Drawing.Size(112, 287)
        Me.TableLayoutPanel8.TabIndex = 6
        '
        'LabelControl12
        '
        Me.LabelControl12.Appearance.BackColor = System.Drawing.Color.Green
        Me.LabelControl12.Appearance.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl12.Appearance.ForeColor = System.Drawing.Color.White
        Me.LabelControl12.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl12.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl12.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl12.Name = "LabelControl12"
        Me.LabelControl12.Size = New System.Drawing.Size(106, 100)
        Me.LabelControl12.TabIndex = 0
        Me.LabelControl12.Text = "ICU"
        '
        'lblICU
        '
        Me.lblICU.Appearance.BackColor = System.Drawing.Color.Gray
        Me.lblICU.Appearance.Font = New System.Drawing.Font("Tahoma", 36.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblICU.Appearance.ForeColor = System.Drawing.Color.White
        Me.lblICU.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblICU.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblICU.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblICU.Location = New System.Drawing.Point(3, 109)
        Me.lblICU.Name = "lblICU"
        Me.lblICU.Size = New System.Drawing.Size(106, 175)
        Me.lblICU.TabIndex = 1
        Me.lblICU.Text = "0"
        '
        'TableLayoutPanel7
        '
        Me.TableLayoutPanel7.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel7.ColumnCount = 1
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.Controls.Add(Me.LabelControl10, 0, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.lblKELASIII, 0, 1)
        Me.TableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel7.Location = New System.Drawing.Point(593, 3)
        Me.TableLayoutPanel7.Name = "TableLayoutPanel7"
        Me.TableLayoutPanel7.RowCount = 2
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.95652!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.04348!))
        Me.TableLayoutPanel7.Size = New System.Drawing.Size(112, 287)
        Me.TableLayoutPanel7.TabIndex = 5
        '
        'LabelControl10
        '
        Me.LabelControl10.Appearance.BackColor = System.Drawing.Color.Green
        Me.LabelControl10.Appearance.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl10.Appearance.ForeColor = System.Drawing.Color.White
        Me.LabelControl10.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl10.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl10.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl10.Name = "LabelControl10"
        Me.LabelControl10.Size = New System.Drawing.Size(106, 100)
        Me.LabelControl10.TabIndex = 0
        Me.LabelControl10.Text = "KELAS 3"
        '
        'lblKELASIII
        '
        Me.lblKELASIII.Appearance.BackColor = System.Drawing.Color.Gray
        Me.lblKELASIII.Appearance.Font = New System.Drawing.Font("Tahoma", 36.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblKELASIII.Appearance.ForeColor = System.Drawing.Color.White
        Me.lblKELASIII.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblKELASIII.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblKELASIII.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblKELASIII.Location = New System.Drawing.Point(3, 109)
        Me.lblKELASIII.Name = "lblKELASIII"
        Me.lblKELASIII.Size = New System.Drawing.Size(106, 175)
        Me.lblKELASIII.TabIndex = 1
        Me.lblKELASIII.Text = "0"
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel6.ColumnCount = 1
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.Controls.Add(Me.LabelControl8, 0, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.lblKELASII, 0, 1)
        Me.TableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(475, 3)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 2
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.95652!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.04348!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(112, 287)
        Me.TableLayoutPanel6.TabIndex = 4
        '
        'LabelControl8
        '
        Me.LabelControl8.Appearance.BackColor = System.Drawing.Color.Green
        Me.LabelControl8.Appearance.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl8.Appearance.ForeColor = System.Drawing.Color.White
        Me.LabelControl8.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl8.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl8.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl8.Name = "LabelControl8"
        Me.LabelControl8.Size = New System.Drawing.Size(106, 100)
        Me.LabelControl8.TabIndex = 0
        Me.LabelControl8.Text = "KELAS 2"
        '
        'lblKELASII
        '
        Me.lblKELASII.Appearance.BackColor = System.Drawing.Color.Gray
        Me.lblKELASII.Appearance.Font = New System.Drawing.Font("Tahoma", 36.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblKELASII.Appearance.ForeColor = System.Drawing.Color.White
        Me.lblKELASII.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblKELASII.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblKELASII.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblKELASII.Location = New System.Drawing.Point(3, 109)
        Me.lblKELASII.Name = "lblKELASII"
        Me.lblKELASII.Size = New System.Drawing.Size(106, 175)
        Me.lblKELASII.TabIndex = 1
        Me.lblKELASII.Text = "0"
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel5.ColumnCount = 1
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.LabelControl6, 0, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.lblKELASI, 0, 1)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(357, 3)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 2
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.95652!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.04348!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(112, 287)
        Me.TableLayoutPanel5.TabIndex = 3
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.BackColor = System.Drawing.Color.Green
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl6.Appearance.ForeColor = System.Drawing.Color.White
        Me.LabelControl6.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl6.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl6.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(106, 100)
        Me.LabelControl6.TabIndex = 0
        Me.LabelControl6.Text = "KELAS 1"
        '
        'lblKELASI
        '
        Me.lblKELASI.Appearance.BackColor = System.Drawing.Color.Gray
        Me.lblKELASI.Appearance.Font = New System.Drawing.Font("Tahoma", 36.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblKELASI.Appearance.ForeColor = System.Drawing.Color.White
        Me.lblKELASI.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblKELASI.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblKELASI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblKELASI.Location = New System.Drawing.Point(3, 109)
        Me.lblKELASI.Name = "lblKELASI"
        Me.lblKELASI.Size = New System.Drawing.Size(106, 175)
        Me.lblKELASI.TabIndex = 1
        Me.lblKELASI.Text = "0"
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel4.ColumnCount = 1
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.LabelControl4, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.lblUTAMA, 0, 1)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(239, 3)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 2
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.95652!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.04348!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(112, 287)
        Me.TableLayoutPanel4.TabIndex = 2
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.BackColor = System.Drawing.Color.Green
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl4.Appearance.ForeColor = System.Drawing.Color.White
        Me.LabelControl4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl4.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(106, 100)
        Me.LabelControl4.TabIndex = 0
        Me.LabelControl4.Text = "UTAMA"
        '
        'lblUTAMA
        '
        Me.lblUTAMA.Appearance.BackColor = System.Drawing.Color.Gray
        Me.lblUTAMA.Appearance.Font = New System.Drawing.Font("Tahoma", 36.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUTAMA.Appearance.ForeColor = System.Drawing.Color.White
        Me.lblUTAMA.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblUTAMA.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblUTAMA.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblUTAMA.Location = New System.Drawing.Point(3, 109)
        Me.lblUTAMA.Name = "lblUTAMA"
        Me.lblUTAMA.Size = New System.Drawing.Size(106, 175)
        Me.lblUTAMA.TabIndex = 1
        Me.lblUTAMA.Text = "0"
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.LabelControl2, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.lblVIP, 0, 1)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(121, 3)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 2
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.95652!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.04348!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(112, 287)
        Me.TableLayoutPanel3.TabIndex = 1
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.BackColor = System.Drawing.Color.Green
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Appearance.ForeColor = System.Drawing.Color.White
        Me.LabelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl2.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(106, 100)
        Me.LabelControl2.TabIndex = 0
        Me.LabelControl2.Text = "VIP"
        '
        'lblVIP
        '
        Me.lblVIP.Appearance.BackColor = System.Drawing.Color.Gray
        Me.lblVIP.Appearance.Font = New System.Drawing.Font("Tahoma", 36.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVIP.Appearance.ForeColor = System.Drawing.Color.White
        Me.lblVIP.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblVIP.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblVIP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblVIP.Location = New System.Drawing.Point(3, 109)
        Me.lblVIP.Name = "lblVIP"
        Me.lblVIP.Size = New System.Drawing.Size(106, 175)
        Me.lblVIP.TabIndex = 1
        Me.lblVIP.Text = "0"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.LabelControl1, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.lblVVIP, 0, 1)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.95652!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.04348!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(112, 287)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.BackColor = System.Drawing.Color.Green
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.White
        Me.LabelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(106, 100)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "VVIP"
        '
        'lblVVIP
        '
        Me.lblVVIP.Appearance.BackColor = System.Drawing.Color.Gray
        Me.lblVVIP.Appearance.Font = New System.Drawing.Font("Tahoma", 36.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVVIP.Appearance.ForeColor = System.Drawing.Color.White
        Me.lblVVIP.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblVVIP.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblVVIP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblVVIP.Location = New System.Drawing.Point(3, 109)
        Me.lblVVIP.Name = "lblVVIP"
        Me.lblVVIP.Size = New System.Drawing.Size(106, 175)
        Me.lblVVIP.TabIndex = 1
        Me.lblVVIP.Text = "0"
        '
        'frmDasboardTempatTidurRS_New
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(951, 586)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDasboardTempatTidurRS_New"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.mnuStrip.ResumeLayout(False)
        CType(Me.printSystem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel17.ResumeLayout(False)
        Me.TableLayoutPanel16.ResumeLayout(False)
        Me.TableLayoutPanel15.ResumeLayout(False)
        Me.TableLayoutPanel14.ResumeLayout(False)
        Me.TableLayoutPanel13.ResumeLayout(False)
        Me.TableLayoutPanel12.ResumeLayout(False)
        Me.TableLayoutPanel11.ResumeLayout(False)
        Me.TableLayoutPanel10.ResumeLayout(False)
        Me.TableLayoutPanel9.ResumeLayout(False)
        Me.TableLayoutPanel8.ResumeLayout(False)
        Me.TableLayoutPanel7.ResumeLayout(False)
        Me.TableLayoutPanel6.ResumeLayout(False)
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents printSystem As DevExpress.XtraPrinting.PrintingSystem
    Friend WithEvents printableComponentLink As DevExpress.XtraPrinting.PrintableComponentLink
    Friend WithEvents mnuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents MasterColumnChooserToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DetailColumnChooserToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Timer1 As Timer
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel17 As TableLayoutPanel
    Friend WithEvents LabelControl30 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblNONKELAS As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel16 As TableLayoutPanel
    Friend WithEvents LabelControl28 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblISOLASI As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel15 As TableLayoutPanel
    Friend WithEvents LabelControl26 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblHCU As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel14 As TableLayoutPanel
    Friend WithEvents LabelControl24 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblBERSALIN As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel13 As TableLayoutPanel
    Friend WithEvents LabelControl22 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblUGD As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel12 As TableLayoutPanel
    Friend WithEvents LabelControl20 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblIGD As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel11 As TableLayoutPanel
    Friend WithEvents LabelControl18 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblPICU As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel10 As TableLayoutPanel
    Friend WithEvents LabelControl16 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblNICU As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel9 As TableLayoutPanel
    Friend WithEvents LabelControl14 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblICCU As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel8 As TableLayoutPanel
    Friend WithEvents LabelControl12 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblICU As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel7 As TableLayoutPanel
    Friend WithEvents LabelControl10 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblKELASIII As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel6 As TableLayoutPanel
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblKELASII As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel5 As TableLayoutPanel
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblKELASI As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblUTAMA As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblVIP As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblVVIP As DevExpress.XtraEditors.LabelControl
End Class
