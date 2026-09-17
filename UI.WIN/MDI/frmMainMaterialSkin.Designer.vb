<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMainMaterialSkin
    'Inherits System.Windows.Forms.Form
    Inherits MaterialSkin.Controls.MaterialForm

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMainMaterialSkin))
        Me.MaterialTabControl1 = New MaterialSkin.Controls.MaterialTabControl()
        Me.TabPageHome = New System.Windows.Forms.TabPage()
        Me.TabPageMaster = New System.Windows.Forms.TabPage()
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnMasterCaraPakai = New System.Windows.Forms.Button()
        Me.btnMasterSigna = New System.Windows.Forms.Button()
        Me.btnMasterSatuan = New System.Windows.Forms.Button()
        Me.btnMasterObat = New System.Windows.Forms.Button()
        Me.TabPageAdmisi = New System.Windows.Forms.TabPage()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.PanelMaster = New System.Windows.Forms.Panel()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.MaterialTabControl1.SuspendLayout()
        Me.TabPageMaster.SuspendLayout()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MaterialTabControl1
        '
        Me.MaterialTabControl1.Controls.Add(Me.TabPageHome)
        Me.MaterialTabControl1.Controls.Add(Me.TabPageMaster)
        Me.MaterialTabControl1.Controls.Add(Me.TabPageAdmisi)
        Me.MaterialTabControl1.Controls.Add(Me.TabPage1)
        Me.MaterialTabControl1.Depth = 0
        Me.MaterialTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MaterialTabControl1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MaterialTabControl1.ImageList = Me.ImageList1
        Me.MaterialTabControl1.Location = New System.Drawing.Point(3, 64)
        Me.MaterialTabControl1.MouseState = MaterialSkin.MouseState.HOVER
        Me.MaterialTabControl1.Multiline = True
        Me.MaterialTabControl1.Name = "MaterialTabControl1"
        Me.MaterialTabControl1.Padding = New System.Drawing.Point(10, 3)
        Me.MaterialTabControl1.SelectedIndex = 0
        Me.MaterialTabControl1.Size = New System.Drawing.Size(904, 589)
        Me.MaterialTabControl1.TabIndex = 0
        '
        'TabPageHome
        '
        Me.TabPageHome.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.TabPageHome.ImageKey = "home-button_icon-icons.com_72700.png"
        Me.TabPageHome.Location = New System.Drawing.Point(4, 39)
        Me.TabPageHome.Name = "TabPageHome"
        Me.TabPageHome.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageHome.Size = New System.Drawing.Size(896, 546)
        Me.TabPageHome.TabIndex = 0
        Me.TabPageHome.Text = "Home"
        Me.TabPageHome.UseVisualStyleBackColor = True
        '
        'TabPageMaster
        '
        Me.TabPageMaster.Controls.Add(Me.SplitContainerControl1)
        Me.TabPageMaster.ImageKey = "reference_notebook_icon_216616.png"
        Me.TabPageMaster.Location = New System.Drawing.Point(4, 39)
        Me.TabPageMaster.Name = "TabPageMaster"
        Me.TabPageMaster.Size = New System.Drawing.Size(896, 546)
        Me.TabPageMaster.TabIndex = 1
        Me.TabPageMaster.Text = "Master"
        Me.TabPageMaster.UseVisualStyleBackColor = True
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.Panel1)
        Me.SplitContainerControl1.Panel1.Text = "Panel1"
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.PanelMaster)
        Me.SplitContainerControl1.Size = New System.Drawing.Size(896, 546)
        Me.SplitContainerControl1.SplitterPosition = 141
        Me.SplitContainerControl1.TabIndex = 0
        Me.SplitContainerControl1.Text = "SplitContainerControl1"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnMasterCaraPakai)
        Me.Panel1.Controls.Add(Me.btnMasterSigna)
        Me.Panel1.Controls.Add(Me.btnMasterSatuan)
        Me.Panel1.Controls.Add(Me.btnMasterObat)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(141, 546)
        Me.Panel1.TabIndex = 0
        '
        'btnMasterCaraPakai
        '
        Me.btnMasterCaraPakai.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnMasterCaraPakai.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnMasterCaraPakai.FlatAppearance.BorderSize = 0
        Me.btnMasterCaraPakai.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMasterCaraPakai.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMasterCaraPakai.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMasterCaraPakai.Location = New System.Drawing.Point(0, 90)
        Me.btnMasterCaraPakai.Name = "btnMasterCaraPakai"
        Me.btnMasterCaraPakai.Size = New System.Drawing.Size(141, 30)
        Me.btnMasterCaraPakai.TabIndex = 3
        Me.btnMasterCaraPakai.Text = "Cara Pakai"
        Me.btnMasterCaraPakai.UseVisualStyleBackColor = False
        '
        'btnMasterSigna
        '
        Me.btnMasterSigna.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnMasterSigna.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnMasterSigna.FlatAppearance.BorderSize = 0
        Me.btnMasterSigna.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMasterSigna.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMasterSigna.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMasterSigna.Location = New System.Drawing.Point(0, 60)
        Me.btnMasterSigna.Name = "btnMasterSigna"
        Me.btnMasterSigna.Size = New System.Drawing.Size(141, 30)
        Me.btnMasterSigna.TabIndex = 2
        Me.btnMasterSigna.Text = "Signa"
        Me.btnMasterSigna.UseVisualStyleBackColor = False
        '
        'btnMasterSatuan
        '
        Me.btnMasterSatuan.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnMasterSatuan.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnMasterSatuan.FlatAppearance.BorderSize = 0
        Me.btnMasterSatuan.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMasterSatuan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMasterSatuan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMasterSatuan.Location = New System.Drawing.Point(0, 30)
        Me.btnMasterSatuan.Name = "btnMasterSatuan"
        Me.btnMasterSatuan.Size = New System.Drawing.Size(141, 30)
        Me.btnMasterSatuan.TabIndex = 1
        Me.btnMasterSatuan.Text = "Satuan"
        Me.btnMasterSatuan.UseVisualStyleBackColor = False
        '
        'btnMasterObat
        '
        Me.btnMasterObat.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnMasterObat.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnMasterObat.FlatAppearance.BorderSize = 0
        Me.btnMasterObat.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMasterObat.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMasterObat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMasterObat.Location = New System.Drawing.Point(0, 0)
        Me.btnMasterObat.Name = "btnMasterObat"
        Me.btnMasterObat.Size = New System.Drawing.Size(141, 30)
        Me.btnMasterObat.TabIndex = 0
        Me.btnMasterObat.Text = "Obat"
        Me.btnMasterObat.UseVisualStyleBackColor = False
        '
        'TabPageAdmisi
        '
        Me.TabPageAdmisi.ImageKey = "queue-play-next_118752.png"
        Me.TabPageAdmisi.Location = New System.Drawing.Point(4, 39)
        Me.TabPageAdmisi.Name = "TabPageAdmisi"
        Me.TabPageAdmisi.Size = New System.Drawing.Size(896, 546)
        Me.TabPageAdmisi.TabIndex = 2
        Me.TabPageAdmisi.Text = "Admisi"
        Me.TabPageAdmisi.UseVisualStyleBackColor = True
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "home-button_icon-icons.com_72700.png")
        Me.ImageList1.Images.SetKeyName(1, "poweroff_icon_237442.png")
        Me.ImageList1.Images.SetKeyName(2, "queue-play-next_118752.png")
        Me.ImageList1.Images.SetKeyName(3, "reference_notebook_icon_216616.png")
        '
        'PanelMaster
        '
        Me.PanelMaster.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelMaster.Location = New System.Drawing.Point(0, 0)
        Me.PanelMaster.Name = "PanelMaster"
        Me.PanelMaster.Size = New System.Drawing.Size(750, 546)
        Me.PanelMaster.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Location = New System.Drawing.Point(4, 39)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(896, 546)
        Me.TabPage1.TabIndex = 3
        Me.TabPage1.Text = "TabPage1"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'frmMainMaterialSkin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(910, 656)
        Me.Controls.Add(Me.MaterialTabControl1)
        Me.DrawerTabControl = Me.MaterialTabControl1
        Me.Name = "frmMainMaterialSkin"
        Me.Text = "Menu"
        Me.MaterialTabControl1.ResumeLayout(False)
        Me.TabPageMaster.ResumeLayout(False)
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents MaterialTabControl1 As MaterialSkin.Controls.MaterialTabControl
    Friend WithEvents TabPageHome As TabPage
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents TabPageMaster As TabPage
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents Panel1 As Panel
    Friend WithEvents btnMasterObat As Button
    Friend WithEvents TabPageAdmisi As TabPage
    Friend WithEvents btnMasterCaraPakai As Button
    Friend WithEvents btnMasterSigna As Button
    Friend WithEvents btnMasterSatuan As Button
    Friend WithEvents PanelMaster As Panel
    Friend WithEvents TabPage1 As TabPage
End Class
