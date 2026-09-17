<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmReferensiApotekOnline
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReferensiApotekOnline))
        Me.grv1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.grd = New DevExpress.XtraGrid.GridControl()
        Me.mnuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.UpdateKodeObatDPHOToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.grv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.panelMenu = New DevExpress.XtraEditors.PanelControl()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.picRefresh = New DevExpress.XtraEditors.PictureEdit()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.btnHapus = New DevExpress.XtraEditors.SimpleButton()
        Me.btnTidakAdaHarga = New DevExpress.XtraEditors.SimpleButton()
        Me.txtBiayaSetujui = New DevExpress.XtraEditors.TextEdit()
        Me.txtBiayaPengajuan = New DevExpress.XtraEditors.TextEdit()
        Me.txtJumlahData = New DevExpress.XtraEditors.TextEdit()
        Me.cboJENISRESEP = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.grdKDITEM = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.grvKDITEM = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.lbltotalbiayasetujui = New DevExpress.XtraEditors.LabelControl()
        Me.lbltotalbiayapengajuan = New DevExpress.XtraEditors.LabelControl()
        Me.lblJumlahData = New DevExpress.XtraEditors.LabelControl()
        Me.lblItem = New DevExpress.XtraEditors.LabelControl()
        Me.txtCARI = New DevExpress.XtraEditors.TextEdit()
        Me.lDateTo = New DevExpress.XtraEditors.LabelControl()
        Me.lblDateFrom = New DevExpress.XtraEditors.LabelControl()
        Me.cboType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.deDateTo = New DevExpress.XtraEditors.DateEdit()
        Me.deDATEFrom = New DevExpress.XtraEditors.DateEdit()
        Me.lblJENISOBAT = New DevExpress.XtraEditors.LabelControl()
        Me.lblCari = New DevExpress.XtraEditors.LabelControl()
        Me.lblTipe = New DevExpress.XtraEditors.LabelControl()
        Me.printSystem = New DevExpress.XtraPrinting.PrintingSystem(Me.components)
        Me.printableComponentLink = New DevExpress.XtraPrinting.PrintableComponentLink(Me.components)
        CType(Me.grv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuStrip.SuspendLayout()
        CType(Me.grv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.panelMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelMenu.SuspendLayout()
        CType(Me.picRefresh.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.txtBiayaSetujui.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBiayaPengajuan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtJumlahData.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboJENISRESEP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDITEM.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvKDITEM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCARI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDateTo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDateTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATEFrom.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATEFrom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.printSystem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grv1
        '
        Me.grv1.GridControl = Me.grd
        Me.grv1.Name = "grv1"
        Me.grv1.OptionsBehavior.Editable = False
        Me.grv1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grv1.OptionsView.ShowAutoFilterRow = True
        '
        'grd
        '
        Me.grd.ContextMenuStrip = Me.mnuStrip
        Me.grd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grd.Location = New System.Drawing.Point(2, 20)
        Me.grd.MainView = Me.grv
        Me.grd.Name = "grd"
        Me.grd.ShowOnlyPredefinedDetails = True
        Me.grd.Size = New System.Drawing.Size(766, 312)
        Me.grd.TabIndex = 0
        Me.grd.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grv, Me.grv1})
        '
        'mnuStrip
        '
        Me.mnuStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.mnuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UpdateKodeObatDPHOToolStripMenuItem})
        Me.mnuStrip.Name = "mnuStrip"
        Me.mnuStrip.Size = New System.Drawing.Size(208, 26)
        '
        'UpdateKodeObatDPHOToolStripMenuItem
        '
        Me.UpdateKodeObatDPHOToolStripMenuItem.Name = "UpdateKodeObatDPHOToolStripMenuItem"
        Me.UpdateKodeObatDPHOToolStripMenuItem.Size = New System.Drawing.Size(207, 22)
        Me.UpdateKodeObatDPHOToolStripMenuItem.Text = "Update Kode Obat DPHO"
        '
        'grv
        '
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
        Me.grv.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grv.OptionsSelection.MultiSelect = True
        Me.grv.OptionsView.ShowAutoFilterRow = True
        Me.grv.OptionsView.ShowFooter = True
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.grd)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 248)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(770, 334)
        Me.GroupControl1.TabIndex = 4
        Me.GroupControl1.Text = "Preview"
        '
        'panelMenu
        '
        Me.panelMenu.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.panelMenu.Appearance.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.panelMenu.Appearance.Options.UseBackColor = True
        Me.panelMenu.Controls.Add(Me.LabelControl6)
        Me.panelMenu.Controls.Add(Me.picRefresh)
        Me.panelMenu.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelMenu.Location = New System.Drawing.Point(0, 0)
        Me.panelMenu.Name = "panelMenu"
        Me.panelMenu.Size = New System.Drawing.Size(770, 91)
        Me.panelMenu.TabIndex = 7
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl6.Location = New System.Drawing.Point(14, 66)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(44, 16)
        Me.LabelControl6.TabIndex = 3
        Me.LabelControl6.Text = "&Refresh"
        '
        'picRefresh
        '
        Me.picRefresh.EditValue = CType(resources.GetObject("picRefresh.EditValue"), Object)
        Me.picRefresh.Location = New System.Drawing.Point(12, 12)
        Me.picRefresh.Name = "picRefresh"
        Me.picRefresh.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picRefresh.Properties.Appearance.Options.UseBackColor = True
        Me.picRefresh.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picRefresh.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picRefresh.Size = New System.Drawing.Size(50, 50)
        Me.picRefresh.TabIndex = 2
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.btnHapus)
        Me.GroupControl2.Controls.Add(Me.btnTidakAdaHarga)
        Me.GroupControl2.Controls.Add(Me.txtBiayaSetujui)
        Me.GroupControl2.Controls.Add(Me.txtBiayaPengajuan)
        Me.GroupControl2.Controls.Add(Me.txtJumlahData)
        Me.GroupControl2.Controls.Add(Me.cboJENISRESEP)
        Me.GroupControl2.Controls.Add(Me.grdKDITEM)
        Me.GroupControl2.Controls.Add(Me.lbltotalbiayasetujui)
        Me.GroupControl2.Controls.Add(Me.lbltotalbiayapengajuan)
        Me.GroupControl2.Controls.Add(Me.lblJumlahData)
        Me.GroupControl2.Controls.Add(Me.lblItem)
        Me.GroupControl2.Controls.Add(Me.txtCARI)
        Me.GroupControl2.Controls.Add(Me.lDateTo)
        Me.GroupControl2.Controls.Add(Me.lblDateFrom)
        Me.GroupControl2.Controls.Add(Me.cboType)
        Me.GroupControl2.Controls.Add(Me.deDateTo)
        Me.GroupControl2.Controls.Add(Me.deDATEFrom)
        Me.GroupControl2.Controls.Add(Me.lblJENISOBAT)
        Me.GroupControl2.Controls.Add(Me.lblCari)
        Me.GroupControl2.Controls.Add(Me.lblTipe)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl2.Location = New System.Drawing.Point(0, 91)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(770, 157)
        Me.GroupControl2.TabIndex = 4
        Me.GroupControl2.Text = "Filter"
        '
        'btnHapus
        '
        Me.btnHapus.Location = New System.Drawing.Point(503, 40)
        Me.btnHapus.Name = "btnHapus"
        Me.btnHapus.Size = New System.Drawing.Size(94, 23)
        Me.btnHapus.TabIndex = 57
        Me.btnHapus.Text = "Hapus Mapping"
        '
        'btnTidakAdaHarga
        '
        Me.btnTidakAdaHarga.Location = New System.Drawing.Point(403, 41)
        Me.btnTidakAdaHarga.Name = "btnTidakAdaHarga"
        Me.btnTidakAdaHarga.Size = New System.Drawing.Size(94, 23)
        Me.btnTidakAdaHarga.TabIndex = 57
        Me.btnTidakAdaHarga.Text = "Aktif/Non Aktif"
        '
        'txtBiayaSetujui
        '
        Me.txtBiayaSetujui.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtBiayaSetujui.EditValue = "0"
        Me.txtBiayaSetujui.Location = New System.Drawing.Point(612, 132)
        Me.txtBiayaSetujui.Name = "txtBiayaSetujui"
        Me.txtBiayaSetujui.Properties.Appearance.Options.UseTextOptions = True
        Me.txtBiayaSetujui.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtBiayaSetujui.Properties.Mask.EditMask = "n0"
        Me.txtBiayaSetujui.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtBiayaSetujui.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtBiayaSetujui.Properties.ReadOnly = True
        Me.txtBiayaSetujui.Size = New System.Drawing.Size(146, 20)
        Me.txtBiayaSetujui.TabIndex = 55
        Me.txtBiayaSetujui.Visible = False
        '
        'txtBiayaPengajuan
        '
        Me.txtBiayaPengajuan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtBiayaPengajuan.EditValue = "0"
        Me.txtBiayaPengajuan.Location = New System.Drawing.Point(612, 88)
        Me.txtBiayaPengajuan.Name = "txtBiayaPengajuan"
        Me.txtBiayaPengajuan.Properties.Appearance.Options.UseTextOptions = True
        Me.txtBiayaPengajuan.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtBiayaPengajuan.Properties.Mask.EditMask = "n0"
        Me.txtBiayaPengajuan.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtBiayaPengajuan.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtBiayaPengajuan.Properties.ReadOnly = True
        Me.txtBiayaPengajuan.Size = New System.Drawing.Size(146, 20)
        Me.txtBiayaPengajuan.TabIndex = 55
        Me.txtBiayaPengajuan.Visible = False
        '
        'txtJumlahData
        '
        Me.txtJumlahData.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtJumlahData.EditValue = "0"
        Me.txtJumlahData.Location = New System.Drawing.Point(612, 43)
        Me.txtJumlahData.Name = "txtJumlahData"
        Me.txtJumlahData.Properties.Appearance.Options.UseTextOptions = True
        Me.txtJumlahData.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtJumlahData.Properties.Mask.EditMask = "n0"
        Me.txtJumlahData.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtJumlahData.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtJumlahData.Properties.ReadOnly = True
        Me.txtJumlahData.Size = New System.Drawing.Size(146, 20)
        Me.txtJumlahData.TabIndex = 55
        Me.txtJumlahData.Visible = False
        '
        'cboJENISRESEP
        '
        Me.cboJENISRESEP.EditValue = "Obat PRB"
        Me.cboJENISRESEP.Location = New System.Drawing.Point(14, 88)
        Me.cboJENISRESEP.Name = "cboJENISRESEP"
        Me.cboJENISRESEP.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboJENISRESEP.Properties.Items.AddRange(New Object() {"Obat PRB", "Obat Kronis Belum Stabil", "Obat Kemoterapi"})
        Me.cboJENISRESEP.Properties.ReadOnly = True
        Me.cboJENISRESEP.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cboJENISRESEP.Size = New System.Drawing.Size(164, 20)
        Me.cboJENISRESEP.TabIndex = 56
        '
        'grdKDITEM
        '
        Me.grdKDITEM.EditValue = ""
        Me.grdKDITEM.EnterMoveNextControl = True
        Me.grdKDITEM.Location = New System.Drawing.Point(184, 43)
        Me.grdKDITEM.Name = "grdKDITEM"
        Me.grdKDITEM.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDITEM.Properties.NullText = ""
        Me.grdKDITEM.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdKDITEM.Properties.View = Me.grvKDITEM
        Me.grdKDITEM.Size = New System.Drawing.Size(212, 20)
        Me.grdKDITEM.TabIndex = 25
        '
        'grvKDITEM
        '
        Me.grvKDITEM.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn9, Me.GridColumn2})
        Me.grvKDITEM.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvKDITEM.Name = "grvKDITEM"
        Me.grvKDITEM.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvKDITEM.OptionsView.ShowAutoFilterRow = True
        Me.grvKDITEM.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Kode Obat DPHO"
        Me.GridColumn1.FieldName = "KDOBATDPHO"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 119
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Tampilan Nama"
        Me.GridColumn9.FieldName = "NMITEM2"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 1
        Me.GridColumn9.Width = 396
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Keterangan"
        Me.GridColumn2.FieldName = "CEKDPHO"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 2
        Me.GridColumn2.Width = 132
        '
        'lbltotalbiayasetujui
        '
        Me.lbltotalbiayasetujui.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbltotalbiayasetujui.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lbltotalbiayasetujui.Location = New System.Drawing.Point(612, 112)
        Me.lbltotalbiayasetujui.Name = "lbltotalbiayasetujui"
        Me.lbltotalbiayasetujui.Size = New System.Drawing.Size(112, 13)
        Me.lbltotalbiayasetujui.TabIndex = 1
        Me.lbltotalbiayasetujui.Text = "Total Biaya Setujui :"
        Me.lbltotalbiayasetujui.Visible = False
        '
        'lbltotalbiayapengajuan
        '
        Me.lbltotalbiayapengajuan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbltotalbiayapengajuan.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lbltotalbiayapengajuan.Location = New System.Drawing.Point(612, 69)
        Me.lbltotalbiayapengajuan.Name = "lbltotalbiayapengajuan"
        Me.lbltotalbiayapengajuan.Size = New System.Drawing.Size(132, 13)
        Me.lbltotalbiayapengajuan.TabIndex = 1
        Me.lbltotalbiayapengajuan.Text = "Total Biaya Pengajuan :"
        Me.lbltotalbiayapengajuan.Visible = False
        '
        'lblJumlahData
        '
        Me.lblJumlahData.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblJumlahData.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblJumlahData.Location = New System.Drawing.Point(612, 24)
        Me.lblJumlahData.Name = "lblJumlahData"
        Me.lblJumlahData.Size = New System.Drawing.Size(77, 13)
        Me.lblJumlahData.TabIndex = 1
        Me.lblJumlahData.Text = "Jumlah Data :"
        Me.lblJumlahData.Visible = False
        '
        'lblItem
        '
        Me.lblItem.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblItem.Location = New System.Drawing.Point(184, 24)
        Me.lblItem.Name = "lblItem"
        Me.lblItem.Size = New System.Drawing.Size(34, 13)
        Me.lblItem.TabIndex = 1
        Me.lblItem.Text = "Item :"
        '
        'txtCARI
        '
        Me.txtCARI.Location = New System.Drawing.Point(184, 88)
        Me.txtCARI.Name = "txtCARI"
        Me.txtCARI.Properties.ReadOnly = True
        Me.txtCARI.Size = New System.Drawing.Size(313, 20)
        Me.txtCARI.TabIndex = 12
        '
        'lDateTo
        '
        Me.lDateTo.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lDateTo.Location = New System.Drawing.Point(184, 112)
        Me.lDateTo.Name = "lDateTo"
        Me.lDateTo.Size = New System.Drawing.Size(84, 13)
        Me.lDateTo.TabIndex = 9
        Me.lDateTo.Text = "Tanggal Akhir :"
        '
        'lblDateFrom
        '
        Me.lblDateFrom.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblDateFrom.Location = New System.Drawing.Point(14, 113)
        Me.lblDateFrom.Name = "lblDateFrom"
        Me.lblDateFrom.Size = New System.Drawing.Size(81, 13)
        Me.lblDateFrom.TabIndex = 9
        Me.lblDateFrom.Text = "Tanggal Awal :"
        '
        'cboType
        '
        Me.cboType.EditValue = "Referensi DPHO"
        Me.cboType.EnterMoveNextControl = True
        Me.cboType.Location = New System.Drawing.Point(14, 43)
        Me.cboType.Name = "cboType"
        Me.cboType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboType.Properties.Items.AddRange(New Object() {"Referensi DPHO", "Referensi Poli", "Referensi Fasilitas Kesehatan Faskes 1", "Referensi Fasilitas Kesehatan Faskes 2/RS", "Referensi Setting Apotek", "Referensi Spesialistik", "Referensi Obat", "Daftar Pelayanan Obat", "Riwayat Pelayanan Obat", "Cari No Kunjungan/SEP", "Daftar Resep By TGLPELSJP", "Daftar Resep By TGLRSP", "Data Klaim Belum diverifikasi", "Data Klaim Sudah Verifikasi"})
        Me.cboType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cboType.Size = New System.Drawing.Size(164, 20)
        Me.cboType.TabIndex = 2
        '
        'deDateTo
        '
        Me.deDateTo.EditValue = Nothing
        Me.deDateTo.EnterMoveNextControl = True
        Me.deDateTo.Location = New System.Drawing.Point(184, 131)
        Me.deDateTo.Name = "deDateTo"
        Me.deDateTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deDateTo.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.deDateTo.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.deDateTo.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.deDateTo.Properties.ReadOnly = True
        Me.deDateTo.Size = New System.Drawing.Size(212, 20)
        Me.deDateTo.TabIndex = 8
        '
        'deDATEFrom
        '
        Me.deDATEFrom.EditValue = Nothing
        Me.deDATEFrom.EnterMoveNextControl = True
        Me.deDATEFrom.Location = New System.Drawing.Point(14, 132)
        Me.deDATEFrom.Name = "deDATEFrom"
        Me.deDATEFrom.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deDATEFrom.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.deDATEFrom.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.deDATEFrom.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.deDATEFrom.Properties.ReadOnly = True
        Me.deDATEFrom.Size = New System.Drawing.Size(164, 20)
        Me.deDATEFrom.TabIndex = 8
        '
        'lblJENISOBAT
        '
        Me.lblJENISOBAT.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblJENISOBAT.Location = New System.Drawing.Point(14, 69)
        Me.lblJENISOBAT.Name = "lblJENISOBAT"
        Me.lblJENISOBAT.Size = New System.Drawing.Size(96, 13)
        Me.lblJENISOBAT.TabIndex = 1
        Me.lblJENISOBAT.Text = "Kode Jenis Obat :"
        '
        'lblCari
        '
        Me.lblCari.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblCari.Location = New System.Drawing.Point(187, 69)
        Me.lblCari.Name = "lblCari"
        Me.lblCari.Size = New System.Drawing.Size(28, 13)
        Me.lblCari.TabIndex = 1
        Me.lblCari.Text = "Cari :"
        '
        'lblTipe
        '
        Me.lblTipe.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblTipe.Location = New System.Drawing.Point(14, 24)
        Me.lblTipe.Name = "lblTipe"
        Me.lblTipe.Size = New System.Drawing.Size(30, 13)
        Me.lblTipe.TabIndex = 1
        Me.lblTipe.Text = "Tipe :"
        '
        'printSystem
        '
        Me.printSystem.Links.AddRange(New Object() {Me.printableComponentLink})
        '
        'printableComponentLink
        '
        Me.printableComponentLink.Component = Me.grd
        Me.printableComponentLink.Landscape = True
        Me.printableComponentLink.PaperKind = System.Drawing.Printing.PaperKind.Custom
        Me.printableComponentLink.PrintingSystemBase = Me.printSystem
        '
        'frmReferensiApotekOnline
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(770, 582)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.GroupControl2)
        Me.Controls.Add(Me.panelMenu)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.Name = "frmReferensiApotekOnline"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Brigging Apotek Online"
        CType(Me.grv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuStrip.ResumeLayout(False)
        CType(Me.grv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.panelMenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelMenu.ResumeLayout(False)
        Me.panelMenu.PerformLayout()
        CType(Me.picRefresh.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.GroupControl2.PerformLayout()
        CType(Me.txtBiayaSetujui.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBiayaPengajuan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtJumlahData.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboJENISRESEP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDITEM.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvKDITEM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCARI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDateTo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDateTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATEFrom.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATEFrom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.printSystem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grd As DevExpress.XtraGrid.GridControl
    Friend WithEvents grv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents panelMenu As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents picRefresh As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblTipe As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents grv1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents printSystem As DevExpress.XtraPrinting.PrintingSystem
    Friend WithEvents printableComponentLink As DevExpress.XtraPrinting.PrintableComponentLink
    Friend WithEvents lblDateFrom As DevExpress.XtraEditors.LabelControl
    Friend WithEvents deDATEFrom As DevExpress.XtraEditors.DateEdit
    Friend WithEvents txtCARI As DevExpress.XtraEditors.TextEdit
    Friend WithEvents grdKDITEM As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents grvKDITEM As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lblItem As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblCari As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboJENISRESEP As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents lblJENISOBAT As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lDateTo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents deDateTo As DevExpress.XtraEditors.DateEdit
    Friend WithEvents txtBiayaSetujui As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtBiayaPengajuan As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtJumlahData As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lbltotalbiayasetujui As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lbltotalbiayapengajuan As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblJumlahData As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btnTidakAdaHarga As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents mnuStrip As ContextMenuStrip
    Friend WithEvents UpdateKodeObatDPHOToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents btnHapus As DevExpress.XtraEditors.SimpleButton
End Class
