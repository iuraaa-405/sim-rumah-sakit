<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMIDIAntrianDustira
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMIDIAntrianDustira))
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.picAntrianManual = New System.Windows.Forms.PictureBox()
        Me.picAntrianSKDOnline = New System.Windows.Forms.PictureBox()
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.picAntrianManual, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picAntrianSKDOnline, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 0)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(976, 97)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.picAntrianManual, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.picAntrianSKDOnline, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.SimpleButton1, 0, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1020, 806)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'picAntrianManual
        '
        Me.picAntrianManual.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picAntrianManual.Image = CType(resources.GetObject("picAntrianManual.Image"), System.Drawing.Image)
        Me.picAntrianManual.Location = New System.Drawing.Point(3, 396)
        Me.picAntrianManual.Name = "picAntrianManual"
        Me.picAntrianManual.Size = New System.Drawing.Size(504, 387)
        Me.picAntrianManual.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picAntrianManual.TabIndex = 4
        Me.picAntrianManual.TabStop = False
        '
        'picAntrianSKDOnline
        '
        Me.picAntrianSKDOnline.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picAntrianSKDOnline.Image = CType(resources.GetObject("picAntrianSKDOnline.Image"), System.Drawing.Image)
        Me.picAntrianSKDOnline.Location = New System.Drawing.Point(3, 3)
        Me.picAntrianSKDOnline.Name = "picAntrianSKDOnline"
        Me.picAntrianSKDOnline.Size = New System.Drawing.Size(504, 387)
        Me.picAntrianSKDOnline.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picAntrianSKDOnline.TabIndex = 3
        Me.picAntrianSKDOnline.TabStop = False
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Location = New System.Drawing.Point(3, 789)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(33, 14)
        Me.SimpleButton1.TabIndex = 5
        Me.SimpleButton1.Text = "x"
        '
        'frmMIDIAntrianDustira
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1020, 806)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.IsMdiContainer = True
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMIDIAntrianDustira"
        Me.ShowIcon = False
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.picAntrianManual, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picAntrianSKDOnline, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Timer1 As Timer
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents picAntrianSKDOnline As PictureBox
    Friend WithEvents picAntrianManual As PictureBox
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
End Class
