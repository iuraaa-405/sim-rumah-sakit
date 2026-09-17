<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAntrianDepanPilih
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAntrianDepanPilih))
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.picAntrianManual = New System.Windows.Forms.PictureBox()
        Me.picAntrianOnline = New System.Windows.Forms.PictureBox()
        Me.btnClose = New DevExpress.XtraEditors.SimpleButton()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.picAntrianManual, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picAntrianOnline, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.LawnGreen
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.LabelControl1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.btnClose, 0, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(760, 615)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.BackColor = System.Drawing.Color.LawnGreen
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.Green
        Me.LabelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(754, 117)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "SELAMAT DATANG DI RS TNI GUNTUR - KAB. GARUT"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.picAntrianManual, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.picAntrianOnline, 1, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 126)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 455.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(754, 455)
        Me.TableLayoutPanel2.TabIndex = 1
        '
        'picAntrianManual
        '
        Me.picAntrianManual.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picAntrianManual.Image = CType(resources.GetObject("picAntrianManual.Image"), System.Drawing.Image)
        Me.picAntrianManual.Location = New System.Drawing.Point(3, 3)
        Me.picAntrianManual.Name = "picAntrianManual"
        Me.picAntrianManual.Size = New System.Drawing.Size(371, 449)
        Me.picAntrianManual.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picAntrianManual.TabIndex = 0
        Me.picAntrianManual.TabStop = False
        '
        'picAntrianOnline
        '
        Me.picAntrianOnline.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picAntrianOnline.Image = CType(resources.GetObject("picAntrianOnline.Image"), System.Drawing.Image)
        Me.picAntrianOnline.Location = New System.Drawing.Point(380, 3)
        Me.picAntrianOnline.Name = "picAntrianOnline"
        Me.picAntrianOnline.Size = New System.Drawing.Size(371, 449)
        Me.picAntrianOnline.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picAntrianOnline.TabIndex = 1
        Me.picAntrianOnline.TabStop = False
        '
        'btnClose
        '
        Me.btnClose.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnClose.Location = New System.Drawing.Point(3, 587)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(39, 25)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "X"
        '
        'frmAntrianDepanPilih
        '
        Me.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(760, 615)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Name = "frmAntrianDepanPilih"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        CType(Me.picAntrianManual, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picAntrianOnline, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents picAntrianManual As PictureBox
    Friend WithEvents picAntrianOnline As PictureBox
    Friend WithEvents btnClose As DevExpress.XtraEditors.SimpleButton
End Class
