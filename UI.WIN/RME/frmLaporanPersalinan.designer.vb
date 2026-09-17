<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmLaporanPersalinan
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.deDATE = New DevExpress.XtraEditors.DateEdit()
        Me.grdCPPT_KDDOCTOR = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.grvDPJP = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn22 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LabelControl29 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.txtRUANGAN = New DevExpress.XtraEditors.TextEdit()
        Me.barManager = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar3 = New DevExpress.XtraBars.Bar()
        Me.btnSaveClosee = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.btnSaveNew = New DevExpress.XtraBars.BarButtonItem()
        Me.btnClose = New DevExpress.XtraBars.BarButtonItem()
        Me.btnSaveClose = New DevExpress.XtraBars.BarButtonItem()
        Me.btnClosee = New DevExpress.XtraBars.BarButtonItem()
        Me.progressBarSave = New DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar()
        Me.progressSave = New DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar()
        Me.txtCATATANLAPORAN = New DevExpress.XtraEditors.MemoEdit()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtKDIDENTITAS = New DevExpress.XtraEditors.TextEdit()
        Me.txtKDLAPORANPERSALINAN = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.Panel1.SuspendLayout()
        CType(Me.deDATE.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCPPT_KDDOCTOR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvDPJP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRUANGAN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCATATANLAPORAN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtKDIDENTITAS.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtKDLAPORANPERSALINAN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.txtKDIDENTITAS)
        Me.Panel1.Controls.Add(Me.txtKDLAPORANPERSALINAN)
        Me.Panel1.Controls.Add(Me.LabelControl2)
        Me.Panel1.Controls.Add(Me.LabelControl4)
        Me.Panel1.Controls.Add(Me.deDATE)
        Me.Panel1.Controls.Add(Me.grdCPPT_KDDOCTOR)
        Me.Panel1.Controls.Add(Me.LabelControl29)
        Me.Panel1.Controls.Add(Me.LabelControl1)
        Me.Panel1.Controls.Add(Me.txtRUANGAN)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(780, 104)
        Me.Panel1.TabIndex = 56
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(55, 61)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(54, 13)
        Me.LabelControl4.TabIndex = 51
        Me.LabelControl4.Text = "Tanggal * :"
        '
        'deDATE
        '
        Me.deDATE.EditValue = Nothing
        Me.deDATE.EnterMoveNextControl = True
        Me.deDATE.Location = New System.Drawing.Point(131, 59)
        Me.deDATE.Name = "deDATE"
        Me.deDATE.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deDATE.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.deDATE.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.deDATE.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.deDATE.Size = New System.Drawing.Size(312, 20)
        Me.deDATE.TabIndex = 48
        '
        'grdCPPT_KDDOCTOR
        '
        Me.grdCPPT_KDDOCTOR.EnterMoveNextControl = True
        Me.grdCPPT_KDDOCTOR.Location = New System.Drawing.Point(131, 35)
        Me.grdCPPT_KDDOCTOR.Name = "grdCPPT_KDDOCTOR"
        Me.grdCPPT_KDDOCTOR.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdCPPT_KDDOCTOR.Properties.NullText = ""
        Me.grdCPPT_KDDOCTOR.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdCPPT_KDDOCTOR.Properties.ReadOnly = True
        Me.grdCPPT_KDDOCTOR.Properties.View = Me.grvDPJP
        Me.grdCPPT_KDDOCTOR.Size = New System.Drawing.Size(312, 20)
        Me.grdCPPT_KDDOCTOR.TabIndex = 47
        '
        'grvDPJP
        '
        Me.grvDPJP.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn22})
        Me.grvDPJP.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvDPJP.Name = "grvDPJP"
        Me.grvDPJP.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvDPJP.OptionsView.ShowAutoFilterRow = True
        Me.grvDPJP.OptionsView.ShowGroupPanel = False
        '
        'GridColumn22
        '
        Me.GridColumn22.Caption = "Name Display"
        Me.GridColumn22.FieldName = "NAME_DISPLAY"
        Me.GridColumn22.Name = "GridColumn22"
        Me.GridColumn22.Visible = True
        Me.GridColumn22.VisibleIndex = 0
        '
        'LabelControl29
        '
        Me.LabelControl29.Location = New System.Drawing.Point(61, 38)
        Me.LabelControl29.Name = "LabelControl29"
        Me.LabelControl29.Size = New System.Drawing.Size(48, 13)
        Me.LabelControl29.TabIndex = 50
        Me.LabelControl29.Text = "Dokter * :"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(30, 14)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(79, 13)
        Me.LabelControl1.TabIndex = 49
        Me.LabelControl1.Text = "Poli/Ruangan * :"
        '
        'txtRUANGAN
        '
        Me.txtRUANGAN.Location = New System.Drawing.Point(131, 12)
        Me.txtRUANGAN.MenuManager = Me.barManager
        Me.txtRUANGAN.Name = "txtRUANGAN"
        Me.txtRUANGAN.Properties.ReadOnly = True
        Me.txtRUANGAN.Size = New System.Drawing.Size(312, 20)
        Me.txtRUANGAN.TabIndex = 46
        '
        'barManager
        '
        Me.barManager.AllowQuickCustomization = False
        Me.barManager.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar3})
        Me.barManager.DockControls.Add(Me.barDockControlTop)
        Me.barManager.DockControls.Add(Me.barDockControlBottom)
        Me.barManager.DockControls.Add(Me.barDockControlLeft)
        Me.barManager.DockControls.Add(Me.barDockControlRight)
        Me.barManager.Form = Me
        Me.barManager.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.btnSaveNew, Me.btnClose, Me.btnSaveClose, Me.btnSaveClosee, Me.btnClosee})
        Me.barManager.MainMenu = Me.Bar3
        Me.barManager.MaxItemId = 10
        Me.barManager.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.progressBarSave, Me.progressSave})
        '
        'Bar3
        '
        Me.Bar3.BarName = "Custom 2"
        Me.Bar3.DockCol = 0
        Me.Bar3.DockRow = 0
        Me.Bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom
        Me.Bar3.FloatLocation = New System.Drawing.Point(46, 709)
        Me.Bar3.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.btnSaveClosee)})
        Me.Bar3.OptionsBar.MultiLine = True
        Me.Bar3.OptionsBar.UseWholeRow = True
        Me.Bar3.Text = "Custom 2"
        '
        'btnSaveClosee
        '
        Me.btnSaveClosee.Caption = "Simpan"
        Me.btnSaveClosee.Id = 8
        Me.btnSaveClosee.Name = "btnSaveClosee"
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(780, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 535)
        Me.barDockControlBottom.Size = New System.Drawing.Size(780, 22)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 535)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(780, 0)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 535)
        '
        'btnSaveNew
        '
        Me.btnSaveNew.Caption = "F2 - Save && New"
        Me.btnSaveNew.Id = 2
        Me.btnSaveNew.Name = "btnSaveNew"
        '
        'btnClose
        '
        Me.btnClose.Caption = "F12 - Close"
        Me.btnClose.Id = 3
        Me.btnClose.Name = "btnClose"
        '
        'btnSaveClose
        '
        Me.btnSaveClose.Caption = "F3 - Save && Close"
        Me.btnSaveClose.Id = 5
        Me.btnSaveClose.Name = "btnSaveClose"
        '
        'btnClosee
        '
        Me.btnClosee.Caption = "F12 - Close"
        Me.btnClosee.Id = 9
        Me.btnClosee.Name = "btnClosee"
        '
        'progressBarSave
        '
        Me.progressBarSave.Name = "progressBarSave"
        Me.progressBarSave.Stopped = True
        '
        'progressSave
        '
        Me.progressSave.Name = "progressSave"
        Me.progressSave.Paused = True
        '
        'txtCATATANLAPORAN
        '
        Me.txtCATATANLAPORAN.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCATATANLAPORAN.Location = New System.Drawing.Point(0, 104)
        Me.txtCATATANLAPORAN.MenuManager = Me.barManager
        Me.txtCATATANLAPORAN.Name = "txtCATATANLAPORAN"
        Me.txtCATATANLAPORAN.Size = New System.Drawing.Size(780, 431)
        Me.txtCATATANLAPORAN.TabIndex = 61
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(452, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 13)
        Me.Label2.TabIndex = 54
        Me.Label2.Text = "Kode Identitas :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(498, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 55
        Me.Label1.Text = "Kode :"
        '
        'txtKDIDENTITAS
        '
        Me.txtKDIDENTITAS.Location = New System.Drawing.Point(542, 38)
        Me.txtKDIDENTITAS.Name = "txtKDIDENTITAS"
        Me.txtKDIDENTITAS.Properties.ReadOnly = True
        Me.txtKDIDENTITAS.Size = New System.Drawing.Size(171, 20)
        Me.txtKDIDENTITAS.TabIndex = 53
        '
        'txtKDLAPORANPERSALINAN
        '
        Me.txtKDLAPORANPERSALINAN.Location = New System.Drawing.Point(542, 12)
        Me.txtKDLAPORANPERSALINAN.Name = "txtKDLAPORANPERSALINAN"
        Me.txtKDLAPORANPERSALINAN.Properties.ReadOnly = True
        Me.txtKDLAPORANPERSALINAN.Size = New System.Drawing.Size(171, 20)
        Me.txtKDLAPORANPERSALINAN.TabIndex = 52
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(12, 83)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(97, 13)
        Me.LabelControl2.TabIndex = 51
        Me.LabelControl2.Text = "Catatan Laporan * :"
        '
        'frmLaporanPersalinan
        '
        Me.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(780, 557)
        Me.Controls.Add(Me.txtCATATANLAPORAN)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.Name = "frmLaporanPersalinan"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.deDATE.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCPPT_KDDOCTOR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvDPJP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRUANGAN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCATATANLAPORAN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtKDIDENTITAS.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtKDLAPORANPERSALINAN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As Panel
    Friend WithEvents barManager As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar3 As DevExpress.XtraBars.Bar
    Friend WithEvents btnSaveClosee As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnClosee As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents btnSaveNew As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnClose As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnSaveClose As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents progressBarSave As DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar
    Friend WithEvents progressSave As DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar
    Friend WithEvents txtCATATANLAPORAN As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents deDATE As DevExpress.XtraEditors.DateEdit
    Friend WithEvents grdCPPT_KDDOCTOR As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents grvDPJP As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn22 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LabelControl29 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtRUANGAN As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents txtKDIDENTITAS As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtKDLAPORANPERSALINAN As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
End Class
