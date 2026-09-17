<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmReqAwalPemeriksaanList
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReqAwalPemeriksaanList))
        Me.grd = New DevExpress.XtraGrid.GridControl()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.HandOverToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PemberiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PenerimaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NilaiKritisToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PemberiToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.PenerimaToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.SBARToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PemberiToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.PenerimaToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.VerifikasiDPJPToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.grv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.mnuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ColumnChooserToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.picAdd = New DevExpress.XtraEditors.PictureEdit()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.picRefresh = New DevExpress.XtraEditors.PictureEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.picUpdate = New DevExpress.XtraEditors.PictureEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.picDelete = New DevExpress.XtraEditors.PictureEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.picPrint = New DevExpress.XtraEditors.PictureEdit()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem3 = New DevExpress.XtraBars.BarButtonItem()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.grv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuStrip.SuspendLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.picAdd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picRefresh.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picUpdate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picDelete.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picPrint.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grd
        '
        Me.grd.ContextMenuStrip = Me.ContextMenuStrip1
        Me.grd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grd.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Me.grd.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.grd.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        Me.grd.EmbeddedNavigator.Buttons.Remove.Visible = False
        Me.grd.Location = New System.Drawing.Point(0, 82)
        Me.grd.MainView = Me.grv
        Me.grd.Name = "grd"
        Me.grd.Size = New System.Drawing.Size(839, 409)
        Me.grd.TabIndex = 0
        Me.grd.UseEmbeddedNavigator = True
        Me.grd.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grv})
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HandOverToolStripMenuItem, Me.NilaiKritisToolStripMenuItem, Me.SBARToolStripMenuItem, Me.VerifikasiDPJPToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(132, 92)
        '
        'HandOverToolStripMenuItem
        '
        Me.HandOverToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PemberiToolStripMenuItem, Me.PenerimaToolStripMenuItem})
        Me.HandOverToolStripMenuItem.Name = "HandOverToolStripMenuItem"
        Me.HandOverToolStripMenuItem.Size = New System.Drawing.Size(131, 22)
        Me.HandOverToolStripMenuItem.Text = "Hand Over"
        '
        'PemberiToolStripMenuItem
        '
        Me.PemberiToolStripMenuItem.Name = "PemberiToolStripMenuItem"
        Me.PemberiToolStripMenuItem.Size = New System.Drawing.Size(124, 22)
        Me.PemberiToolStripMenuItem.Text = "Pemberi"
        '
        'PenerimaToolStripMenuItem
        '
        Me.PenerimaToolStripMenuItem.Name = "PenerimaToolStripMenuItem"
        Me.PenerimaToolStripMenuItem.Size = New System.Drawing.Size(124, 22)
        Me.PenerimaToolStripMenuItem.Text = "Penerima"
        '
        'NilaiKritisToolStripMenuItem
        '
        Me.NilaiKritisToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PemberiToolStripMenuItem1, Me.PenerimaToolStripMenuItem1})
        Me.NilaiKritisToolStripMenuItem.Name = "NilaiKritisToolStripMenuItem"
        Me.NilaiKritisToolStripMenuItem.Size = New System.Drawing.Size(131, 22)
        Me.NilaiKritisToolStripMenuItem.Text = "Nilai Kritis"
        '
        'PemberiToolStripMenuItem1
        '
        Me.PemberiToolStripMenuItem1.Name = "PemberiToolStripMenuItem1"
        Me.PemberiToolStripMenuItem1.Size = New System.Drawing.Size(124, 22)
        Me.PemberiToolStripMenuItem1.Text = "Pemberi"
        '
        'PenerimaToolStripMenuItem1
        '
        Me.PenerimaToolStripMenuItem1.Name = "PenerimaToolStripMenuItem1"
        Me.PenerimaToolStripMenuItem1.Size = New System.Drawing.Size(124, 22)
        Me.PenerimaToolStripMenuItem1.Text = "Penerima"
        '
        'SBARToolStripMenuItem
        '
        Me.SBARToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PemberiToolStripMenuItem2, Me.PenerimaToolStripMenuItem2})
        Me.SBARToolStripMenuItem.Name = "SBARToolStripMenuItem"
        Me.SBARToolStripMenuItem.Size = New System.Drawing.Size(131, 22)
        Me.SBARToolStripMenuItem.Text = "SBAR"
        '
        'PemberiToolStripMenuItem2
        '
        Me.PemberiToolStripMenuItem2.Name = "PemberiToolStripMenuItem2"
        Me.PemberiToolStripMenuItem2.Size = New System.Drawing.Size(124, 22)
        Me.PemberiToolStripMenuItem2.Text = "Pemberi"
        '
        'PenerimaToolStripMenuItem2
        '
        Me.PenerimaToolStripMenuItem2.Name = "PenerimaToolStripMenuItem2"
        Me.PenerimaToolStripMenuItem2.Size = New System.Drawing.Size(124, 22)
        Me.PenerimaToolStripMenuItem2.Text = "Penerima"
        '
        'VerifikasiDPJPToolStripMenuItem
        '
        Me.VerifikasiDPJPToolStripMenuItem.Name = "VerifikasiDPJPToolStripMenuItem"
        Me.VerifikasiDPJPToolStripMenuItem.Size = New System.Drawing.Size(131, 22)
        Me.VerifikasiDPJPToolStripMenuItem.Text = "Verifikasi"
        '
        'grv
        '
        Me.grv.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grv.GridControl = Me.grd
        Me.grv.Name = "grv"
        Me.grv.OptionsBehavior.Editable = False
        Me.grv.OptionsBehavior.ReadOnly = True
        Me.grv.OptionsFind.AlwaysVisible = True
        Me.grv.OptionsFind.ShowFindButton = False
        Me.grv.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grv.OptionsView.ShowAutoFilterRow = True
        Me.grv.OptionsView.ShowGroupPanel = False
        '
        'mnuStrip
        '
        Me.mnuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ColumnChooserToolStripMenuItem})
        Me.mnuStrip.Name = "mnuStrip"
        Me.mnuStrip.Size = New System.Drawing.Size(165, 26)
        '
        'ColumnChooserToolStripMenuItem
        '
        Me.ColumnChooserToolStripMenuItem.Name = "ColumnChooserToolStripMenuItem"
        Me.ColumnChooserToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.ColumnChooserToolStripMenuItem.Text = "Column Chooser"
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.LabelControl6)
        Me.PanelControl1.Controls.Add(Me.picAdd)
        Me.PanelControl1.Controls.Add(Me.LabelControl7)
        Me.PanelControl1.Controls.Add(Me.picRefresh)
        Me.PanelControl1.Controls.Add(Me.LabelControl4)
        Me.PanelControl1.Controls.Add(Me.picUpdate)
        Me.PanelControl1.Controls.Add(Me.LabelControl3)
        Me.PanelControl1.Controls.Add(Me.picDelete)
        Me.PanelControl1.Controls.Add(Me.LabelControl1)
        Me.PanelControl1.Controls.Add(Me.picPrint)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(839, 82)
        Me.PanelControl1.TabIndex = 1
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl6.Location = New System.Drawing.Point(182, 62)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(44, 13)
        Me.LabelControl6.TabIndex = 13
        Me.LabelControl6.Text = "&Refresh"
        '
        'picAdd
        '
        Me.picAdd.EditValue = CType(resources.GetObject("picAdd.EditValue"), Object)
        Me.picAdd.Location = New System.Drawing.Point(18, 10)
        Me.picAdd.Name = "picAdd"
        Me.picAdd.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picAdd.Properties.Appearance.Options.UseBackColor = True
        Me.picAdd.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picAdd.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picAdd.Size = New System.Drawing.Size(48, 48)
        Me.picAdd.TabIndex = 8
        '
        'LabelControl7
        '
        Me.LabelControl7.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl7.Location = New System.Drawing.Point(299, 62)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(27, 13)
        Me.LabelControl7.TabIndex = 10
        Me.LabelControl7.Text = "&Print"
        Me.LabelControl7.Visible = False
        '
        'picRefresh
        '
        Me.picRefresh.EditValue = CType(resources.GetObject("picRefresh.EditValue"), Object)
        Me.picRefresh.Location = New System.Drawing.Point(180, 12)
        Me.picRefresh.Name = "picRefresh"
        Me.picRefresh.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picRefresh.Properties.Appearance.Options.UseBackColor = True
        Me.picRefresh.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picRefresh.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picRefresh.Size = New System.Drawing.Size(48, 48)
        Me.picRefresh.TabIndex = 12
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl4.Location = New System.Drawing.Point(130, 62)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(37, 13)
        Me.LabelControl4.TabIndex = 11
        Me.LabelControl4.Text = "&Delete"
        '
        'picUpdate
        '
        Me.picUpdate.EditValue = CType(resources.GetObject("picUpdate.EditValue"), Object)
        Me.picUpdate.Location = New System.Drawing.Point(72, 10)
        Me.picUpdate.Name = "picUpdate"
        Me.picUpdate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picUpdate.Properties.Appearance.Options.UseBackColor = True
        Me.picUpdate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picUpdate.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picUpdate.Size = New System.Drawing.Size(48, 48)
        Me.picUpdate.TabIndex = 6
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl3.Location = New System.Drawing.Point(86, 60)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(21, 13)
        Me.LabelControl3.TabIndex = 9
        Me.LabelControl3.Text = "&Edit"
        '
        'picDelete
        '
        Me.picDelete.EditValue = CType(resources.GetObject("picDelete.EditValue"), Object)
        Me.picDelete.Location = New System.Drawing.Point(126, 12)
        Me.picDelete.Name = "picDelete"
        Me.picDelete.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picDelete.Properties.Appearance.Options.UseBackColor = True
        Me.picDelete.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picDelete.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picDelete.Size = New System.Drawing.Size(48, 48)
        Me.picDelete.TabIndex = 7
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl1.Location = New System.Drawing.Point(31, 60)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(22, 13)
        Me.LabelControl1.TabIndex = 5
        Me.LabelControl1.Text = "&Add"
        '
        'picPrint
        '
        Me.picPrint.EditValue = CType(resources.GetObject("picPrint.EditValue"), Object)
        Me.picPrint.Location = New System.Drawing.Point(288, 12)
        Me.picPrint.Name = "picPrint"
        Me.picPrint.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picPrint.Properties.Appearance.Options.UseBackColor = True
        Me.picPrint.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picPrint.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picPrint.Size = New System.Drawing.Size(48, 48)
        Me.picPrint.TabIndex = 4
        Me.picPrint.Visible = False
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "New"
        Me.BarButtonItem1.Id = 0
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.Caption = "Edit"
        Me.BarButtonItem2.Id = 1
        Me.BarButtonItem2.Name = "BarButtonItem2"
        '
        'BarButtonItem3
        '
        Me.BarButtonItem3.Caption = "Delete"
        Me.BarButtonItem3.Id = 2
        Me.BarButtonItem3.Name = "BarButtonItem3"
        '
        'frmReqAwalPemeriksaanList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(839, 491)
        Me.Controls.Add(Me.grd)
        Me.Controls.Add(Me.PanelControl1)
        Me.KeyPreview = True
        Me.Name = "frmReqAwalPemeriksaanList"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.grv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuStrip.ResumeLayout(False)
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.picAdd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picRefresh.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picUpdate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picDelete.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picPrint.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grd As DevExpress.XtraGrid.GridControl
    Friend WithEvents grv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem3 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents picRefresh As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents picPrint As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents picDelete As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents picUpdate As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents picAdd As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents mnuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ColumnChooserToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents HandOverToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PemberiToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PenerimaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NilaiKritisToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PemberiToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents PenerimaToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents SBARToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PemberiToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents PenerimaToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents VerifikasiDPJPToolStripMenuItem As ToolStripMenuItem
End Class
