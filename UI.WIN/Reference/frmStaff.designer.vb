<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmStaff
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
        Me.barManager = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barTop = New DevExpress.XtraBars.Bar()
        Me.btnSaveNew = New DevExpress.XtraBars.BarButtonItem()
        Me.btnSaveClose = New DevExpress.XtraBars.BarButtonItem()
        Me.btnUpload = New DevExpress.XtraBars.BarButtonItem()
        Me.btnClose = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.progressBarSave = New DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar()
        Me.progressSave = New DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar()
        Me.txtNAME_DISPLAY = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.grdKDUSER = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtMessage = New System.Windows.Forms.TextBox()
        Me.picFinger = New System.Windows.Forms.PictureBox()
        Me.grdKDSTAFFPENDIDIKAN = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdKDSTAFFBAGIAN = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.picGAMBAR = New System.Windows.Forms.PictureBox()
        Me.txtDESCRIPTION = New DevExpress.XtraEditors.TextEdit()
        Me.deDATETMTKERJA = New DevExpress.XtraEditors.DateEdit()
        Me.cboStatus = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.grdKDSTAFJABATAN = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.grvKDSTAFFJABATAN = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn31 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colMEMOJABATAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdKDSTAFPANGKAT = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.grvKDSTAFFPANGKAT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colMEMOPANGKAT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtKDSTAFF = New DevExpress.XtraEditors.TextEdit()
        Me.txtEMAIL = New DevExpress.XtraEditors.TextEdit()
        Me.txtNOMOR_HP2 = New DevExpress.XtraEditors.TextEdit()
        Me.txtNOMOR_HP1 = New DevExpress.XtraEditors.TextEdit()
        Me.cboAGAMA = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtNOMOR_KTP = New DevExpress.XtraEditors.TextEdit()
        Me.chkISACTIVE = New DevExpress.XtraEditors.CheckEdit()
        Me.txtNOMOR_NIP = New DevExpress.XtraEditors.TextEdit()
        Me.cboJENISKELAMIN = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtTEMPATLAHIR = New DevExpress.XtraEditors.TextEdit()
        Me.deDATETANGGALLAHIR = New DevExpress.XtraEditors.DateEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.lJENISKELAMIN = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lAGAMA = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lNOMOR_HP1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lNOMOR_HP2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lEMAIL = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lKDSTAFF = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lISACTIVE = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lSTATUS = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lKDSTAFFBAGIAN = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lKDSTAFFPANGKAT = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lKDSTAFFJABATAN = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lKDSTAFFPENDIDIKAN = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lNAME_DISPLAY = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lNOMOR_NIP = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lTMT = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lNOMOR_KTP = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lTEMPATLAHIR = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lTANGGALLAHIR = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lDESCRIPTION = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lATTACMENT = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LabelControl13 = New DevExpress.XtraEditors.LabelControl()
        Me.BarDockControl1 = New DevExpress.XtraBars.BarDockControl()
        Me.fileDialog = New System.Windows.Forms.OpenFileDialog()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNAME_DISPLAY.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.grdKDUSER.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picFinger, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDSTAFFPENDIDIKAN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDSTAFFBAGIAN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picGAMBAR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDESCRIPTION.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATETMTKERJA.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATETMTKERJA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboStatus.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDSTAFJABATAN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvKDSTAFFJABATAN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDSTAFPANGKAT.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvKDSTAFFPANGKAT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtKDSTAFF.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEMAIL.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNOMOR_HP2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNOMOR_HP1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboAGAMA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNOMOR_KTP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkISACTIVE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNOMOR_NIP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboJENISKELAMIN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTEMPATLAHIR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATETANGGALLAHIR.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATETANGGALLAHIR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lJENISKELAMIN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lAGAMA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lNOMOR_HP1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lNOMOR_HP2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lEMAIL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKDSTAFF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lISACTIVE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lSTATUS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKDSTAFFBAGIAN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKDSTAFFPANGKAT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKDSTAFFJABATAN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKDSTAFFPENDIDIKAN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lNAME_DISPLAY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lNOMOR_NIP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lTMT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lNOMOR_KTP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lTEMPATLAHIR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lTANGGALLAHIR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lDESCRIPTION, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lATTACMENT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'barManager
        '
        Me.barManager.AllowQuickCustomization = False
        Me.barManager.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.barTop})
        Me.barManager.DockControls.Add(Me.barDockControlTop)
        Me.barManager.DockControls.Add(Me.barDockControlBottom)
        Me.barManager.DockControls.Add(Me.barDockControlLeft)
        Me.barManager.DockControls.Add(Me.barDockControlRight)
        Me.barManager.Form = Me
        Me.barManager.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.btnSaveNew, Me.btnClose, Me.btnSaveClose, Me.btnUpload})
        Me.barManager.MainMenu = Me.barTop
        Me.barManager.MaxItemId = 9
        Me.barManager.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.progressBarSave, Me.progressSave})
        '
        'barTop
        '
        Me.barTop.BarName = "Main menu"
        Me.barTop.DockCol = 0
        Me.barTop.DockRow = 0
        Me.barTop.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom
        Me.barTop.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.btnSaveNew), New DevExpress.XtraBars.LinkPersistInfo(Me.btnSaveClose), New DevExpress.XtraBars.LinkPersistInfo(Me.btnUpload), New DevExpress.XtraBars.LinkPersistInfo(Me.btnClose)})
        Me.barTop.OptionsBar.DrawDragBorder = False
        Me.barTop.OptionsBar.MultiLine = True
        Me.barTop.OptionsBar.UseWholeRow = True
        Me.barTop.Text = "Main menu"
        '
        'btnSaveNew
        '
        Me.btnSaveNew.Caption = "F2 - Save && New"
        Me.btnSaveNew.Id = 2
        Me.btnSaveNew.Name = "btnSaveNew"
        '
        'btnSaveClose
        '
        Me.btnSaveClose.Caption = "F3 - Save && Close"
        Me.btnSaveClose.Id = 5
        Me.btnSaveClose.Name = "btnSaveClose"
        '
        'btnUpload
        '
        Me.btnUpload.Caption = "F5 - Upload"
        Me.btnUpload.Id = 8
        Me.btnUpload.Name = "btnUpload"
        '
        'btnClose
        '
        Me.btnClose.Caption = "F12 - Close"
        Me.btnClose.Id = 3
        Me.btnClose.Name = "btnClose"
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(934, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 592)
        Me.barDockControlBottom.Size = New System.Drawing.Size(934, 22)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 592)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(934, 0)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 592)
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
        'txtNAME_DISPLAY
        '
        Me.txtNAME_DISPLAY.Location = New System.Drawing.Point(152, 296)
        Me.txtNAME_DISPLAY.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNAME_DISPLAY.MenuManager = Me.barManager
        Me.txtNAME_DISPLAY.Name = "txtNAME_DISPLAY"
        Me.txtNAME_DISPLAY.Size = New System.Drawing.Size(235, 20)
        Me.txtNAME_DISPLAY.StyleController = Me.LayoutControl1
        Me.txtNAME_DISPLAY.TabIndex = 1
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.grdKDUSER)
        Me.LayoutControl1.Controls.Add(Me.txtMessage)
        Me.LayoutControl1.Controls.Add(Me.picFinger)
        Me.LayoutControl1.Controls.Add(Me.grdKDSTAFFPENDIDIKAN)
        Me.LayoutControl1.Controls.Add(Me.grdKDSTAFFBAGIAN)
        Me.LayoutControl1.Controls.Add(Me.picGAMBAR)
        Me.LayoutControl1.Controls.Add(Me.txtDESCRIPTION)
        Me.LayoutControl1.Controls.Add(Me.deDATETMTKERJA)
        Me.LayoutControl1.Controls.Add(Me.cboStatus)
        Me.LayoutControl1.Controls.Add(Me.grdKDSTAFJABATAN)
        Me.LayoutControl1.Controls.Add(Me.grdKDSTAFPANGKAT)
        Me.LayoutControl1.Controls.Add(Me.txtKDSTAFF)
        Me.LayoutControl1.Controls.Add(Me.txtEMAIL)
        Me.LayoutControl1.Controls.Add(Me.txtNOMOR_HP2)
        Me.LayoutControl1.Controls.Add(Me.txtNOMOR_HP1)
        Me.LayoutControl1.Controls.Add(Me.cboAGAMA)
        Me.LayoutControl1.Controls.Add(Me.txtNOMOR_KTP)
        Me.LayoutControl1.Controls.Add(Me.chkISACTIVE)
        Me.LayoutControl1.Controls.Add(Me.txtNOMOR_NIP)
        Me.LayoutControl1.Controls.Add(Me.cboJENISKELAMIN)
        Me.LayoutControl1.Controls.Add(Me.txtTEMPATLAHIR)
        Me.LayoutControl1.Controls.Add(Me.deDATETANGGALLAHIR)
        Me.LayoutControl1.Controls.Add(Me.txtNAME_DISPLAY)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Right
        Me.LayoutControl1.Location = New System.Drawing.Point(535, 0)
        Me.LayoutControl1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(198, 197, 250, 350)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(399, 592)
        Me.LayoutControl1.TabIndex = 158
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'grdKDUSER
        '
        Me.grdKDUSER.EnterMoveNextControl = True
        Me.grdKDUSER.Location = New System.Drawing.Point(152, 152)
        Me.grdKDUSER.MenuManager = Me.barManager
        Me.grdKDUSER.Name = "grdKDUSER"
        Me.grdKDUSER.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDUSER.Properties.NullText = ""
        Me.grdKDUSER.Properties.PopupFormMinSize = New System.Drawing.Size(300, 300)
        Me.grdKDUSER.Properties.View = Me.GridView3
        Me.grdKDUSER.Size = New System.Drawing.Size(235, 20)
        Me.grdKDUSER.StyleController = Me.LayoutControl1
        Me.grdKDUSER.TabIndex = 166
        '
        'GridView3
        '
        Me.GridView3.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3})
        Me.GridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView3.Name = "GridView3"
        Me.GridView3.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView3.OptionsView.ShowAutoFilterRow = True
        Me.GridView3.OptionsView.ShowGroupPanel = False
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Id User"
        Me.GridColumn3.FieldName = "KDUSER"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        '
        'txtMessage
        '
        Me.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMessage.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtMessage.Location = New System.Drawing.Point(12, 12)
        Me.txtMessage.Multiline = True
        Me.txtMessage.Name = "txtMessage"
        Me.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMessage.Size = New System.Drawing.Size(147, 112)
        Me.txtMessage.TabIndex = 11
        Me.txtMessage.TabStop = False
        '
        'picFinger
        '
        Me.picFinger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picFinger.Location = New System.Drawing.Point(163, 12)
        Me.picFinger.Name = "picFinger"
        Me.picFinger.Size = New System.Drawing.Size(106, 112)
        Me.picFinger.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picFinger.TabIndex = 12
        Me.picFinger.TabStop = False
        '
        'grdKDSTAFFPENDIDIKAN
        '
        Me.grdKDSTAFFPENDIDIKAN.EnterMoveNextControl = True
        Me.grdKDSTAFFPENDIDIKAN.Location = New System.Drawing.Point(152, 272)
        Me.grdKDSTAFFPENDIDIKAN.MenuManager = Me.barManager
        Me.grdKDSTAFFPENDIDIKAN.Name = "grdKDSTAFFPENDIDIKAN"
        Me.grdKDSTAFFPENDIDIKAN.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDSTAFFPENDIDIKAN.Properties.NullText = ""
        Me.grdKDSTAFFPENDIDIKAN.Properties.PopupFormMinSize = New System.Drawing.Size(300, 300)
        Me.grdKDSTAFFPENDIDIKAN.Properties.View = Me.GridView2
        Me.grdKDSTAFFPENDIDIKAN.Size = New System.Drawing.Size(235, 20)
        Me.grdKDSTAFFPENDIDIKAN.StyleController = Me.LayoutControl1
        Me.grdKDSTAFFPENDIDIKAN.TabIndex = 165
        '
        'GridView2
        '
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2})
        Me.GridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView2.OptionsView.ShowAutoFilterRow = True
        Me.GridView2.OptionsView.ShowGroupPanel = False
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Display Name"
        Me.GridColumn2.FieldName = "MEMO"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        '
        'grdKDSTAFFBAGIAN
        '
        Me.grdKDSTAFFBAGIAN.EnterMoveNextControl = True
        Me.grdKDSTAFFBAGIAN.Location = New System.Drawing.Point(152, 200)
        Me.grdKDSTAFFBAGIAN.MenuManager = Me.barManager
        Me.grdKDSTAFFBAGIAN.Name = "grdKDSTAFFBAGIAN"
        Me.grdKDSTAFFBAGIAN.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDSTAFFBAGIAN.Properties.NullText = ""
        Me.grdKDSTAFFBAGIAN.Properties.PopupFormMinSize = New System.Drawing.Size(300, 300)
        Me.grdKDSTAFFBAGIAN.Properties.View = Me.GridView1
        Me.grdKDSTAFFBAGIAN.Size = New System.Drawing.Size(235, 20)
        Me.grdKDSTAFFBAGIAN.StyleController = Me.LayoutControl1
        Me.grdKDSTAFFBAGIAN.TabIndex = 165
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1})
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Display Name"
        Me.GridColumn1.FieldName = "MEMO"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'picGAMBAR
        '
        Me.picGAMBAR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picGAMBAR.Location = New System.Drawing.Point(273, 12)
        Me.picGAMBAR.Name = "picGAMBAR"
        Me.picGAMBAR.Size = New System.Drawing.Size(114, 112)
        Me.picGAMBAR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picGAMBAR.TabIndex = 171
        Me.picGAMBAR.TabStop = False
        '
        'txtDESCRIPTION
        '
        Me.txtDESCRIPTION.Location = New System.Drawing.Point(152, 560)
        Me.txtDESCRIPTION.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtDESCRIPTION.MenuManager = Me.barManager
        Me.txtDESCRIPTION.Name = "txtDESCRIPTION"
        Me.txtDESCRIPTION.Size = New System.Drawing.Size(235, 20)
        Me.txtDESCRIPTION.StyleController = Me.LayoutControl1
        Me.txtDESCRIPTION.TabIndex = 170
        '
        'deDATETMTKERJA
        '
        Me.deDATETMTKERJA.EditValue = Nothing
        Me.deDATETMTKERJA.EnterMoveNextControl = True
        Me.deDATETMTKERJA.Location = New System.Drawing.Point(152, 344)
        Me.deDATETMTKERJA.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.deDATETMTKERJA.Name = "deDATETMTKERJA"
        Me.deDATETMTKERJA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deDATETMTKERJA.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.deDATETMTKERJA.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.deDATETMTKERJA.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.deDATETMTKERJA.Size = New System.Drawing.Size(235, 20)
        Me.deDATETMTKERJA.StyleController = Me.LayoutControl1
        Me.deDATETMTKERJA.TabIndex = 170
        '
        'cboStatus
        '
        Me.cboStatus.EditValue = "KAWIN"
        Me.cboStatus.Location = New System.Drawing.Point(152, 536)
        Me.cboStatus.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboStatus.Properties.Items.AddRange(New Object() {"KAWIN", "TIDAK KAWIN"})
        Me.cboStatus.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cboStatus.Size = New System.Drawing.Size(235, 20)
        Me.cboStatus.StyleController = Me.LayoutControl1
        Me.cboStatus.TabIndex = 170
        '
        'grdKDSTAFJABATAN
        '
        Me.grdKDSTAFJABATAN.EnterMoveNextControl = True
        Me.grdKDSTAFJABATAN.Location = New System.Drawing.Point(152, 248)
        Me.grdKDSTAFJABATAN.MenuManager = Me.barManager
        Me.grdKDSTAFJABATAN.Name = "grdKDSTAFJABATAN"
        Me.grdKDSTAFJABATAN.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDSTAFJABATAN.Properties.NullText = ""
        Me.grdKDSTAFJABATAN.Properties.PopupFormMinSize = New System.Drawing.Size(300, 300)
        Me.grdKDSTAFJABATAN.Properties.View = Me.grvKDSTAFFJABATAN
        Me.grdKDSTAFJABATAN.Size = New System.Drawing.Size(235, 20)
        Me.grdKDSTAFJABATAN.StyleController = Me.LayoutControl1
        Me.grdKDSTAFJABATAN.TabIndex = 164
        '
        'grvKDSTAFFJABATAN
        '
        Me.grvKDSTAFFJABATAN.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn31, Me.colMEMOJABATAN})
        Me.grvKDSTAFFJABATAN.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvKDSTAFFJABATAN.Name = "grvKDSTAFFJABATAN"
        Me.grvKDSTAFFJABATAN.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvKDSTAFFJABATAN.OptionsView.ShowAutoFilterRow = True
        Me.grvKDSTAFFJABATAN.OptionsView.ShowGroupPanel = False
        '
        'GridColumn31
        '
        Me.GridColumn31.Caption = "Group Lapor"
        Me.GridColumn31.FieldName = "KELOMPOKLAPOR"
        Me.GridColumn31.Name = "GridColumn31"
        Me.GridColumn31.Visible = True
        Me.GridColumn31.VisibleIndex = 0
        '
        'colMEMOJABATAN
        '
        Me.colMEMOJABATAN.Caption = "Display Name"
        Me.colMEMOJABATAN.FieldName = "MEMO"
        Me.colMEMOJABATAN.Name = "colMEMOJABATAN"
        Me.colMEMOJABATAN.Visible = True
        Me.colMEMOJABATAN.VisibleIndex = 1
        '
        'grdKDSTAFPANGKAT
        '
        Me.grdKDSTAFPANGKAT.EnterMoveNextControl = True
        Me.grdKDSTAFPANGKAT.Location = New System.Drawing.Point(152, 224)
        Me.grdKDSTAFPANGKAT.MenuManager = Me.barManager
        Me.grdKDSTAFPANGKAT.Name = "grdKDSTAFPANGKAT"
        Me.grdKDSTAFPANGKAT.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDSTAFPANGKAT.Properties.NullText = ""
        Me.grdKDSTAFPANGKAT.Properties.PopupFormMinSize = New System.Drawing.Size(300, 300)
        Me.grdKDSTAFPANGKAT.Properties.View = Me.grvKDSTAFFPANGKAT
        Me.grdKDSTAFPANGKAT.Size = New System.Drawing.Size(235, 20)
        Me.grdKDSTAFPANGKAT.StyleController = Me.LayoutControl1
        Me.grdKDSTAFPANGKAT.TabIndex = 164
        '
        'grvKDSTAFFPANGKAT
        '
        Me.grvKDSTAFFPANGKAT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colMEMOPANGKAT})
        Me.grvKDSTAFFPANGKAT.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvKDSTAFFPANGKAT.Name = "grvKDSTAFFPANGKAT"
        Me.grvKDSTAFFPANGKAT.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvKDSTAFFPANGKAT.OptionsView.ShowAutoFilterRow = True
        Me.grvKDSTAFFPANGKAT.OptionsView.ShowGroupPanel = False
        '
        'colMEMOPANGKAT
        '
        Me.colMEMOPANGKAT.Caption = "Display Name"
        Me.colMEMOPANGKAT.FieldName = "MEMO"
        Me.colMEMOPANGKAT.Name = "colMEMOPANGKAT"
        Me.colMEMOPANGKAT.Visible = True
        Me.colMEMOPANGKAT.VisibleIndex = 0
        '
        'txtKDSTAFF
        '
        Me.txtKDSTAFF.Location = New System.Drawing.Point(152, 128)
        Me.txtKDSTAFF.MenuManager = Me.barManager
        Me.txtKDSTAFF.Name = "txtKDSTAFF"
        Me.txtKDSTAFF.Properties.ReadOnly = True
        Me.txtKDSTAFF.Size = New System.Drawing.Size(174, 20)
        Me.txtKDSTAFF.StyleController = Me.LayoutControl1
        Me.txtKDSTAFF.TabIndex = 168
        '
        'txtEMAIL
        '
        Me.txtEMAIL.Location = New System.Drawing.Point(152, 512)
        Me.txtEMAIL.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtEMAIL.MenuManager = Me.barManager
        Me.txtEMAIL.Name = "txtEMAIL"
        Me.txtEMAIL.Size = New System.Drawing.Size(235, 20)
        Me.txtEMAIL.StyleController = Me.LayoutControl1
        Me.txtEMAIL.TabIndex = 164
        '
        'txtNOMOR_HP2
        '
        Me.txtNOMOR_HP2.Location = New System.Drawing.Point(152, 488)
        Me.txtNOMOR_HP2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNOMOR_HP2.MenuManager = Me.barManager
        Me.txtNOMOR_HP2.Name = "txtNOMOR_HP2"
        Me.txtNOMOR_HP2.Size = New System.Drawing.Size(235, 20)
        Me.txtNOMOR_HP2.StyleController = Me.LayoutControl1
        Me.txtNOMOR_HP2.TabIndex = 164
        '
        'txtNOMOR_HP1
        '
        Me.txtNOMOR_HP1.Location = New System.Drawing.Point(152, 464)
        Me.txtNOMOR_HP1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNOMOR_HP1.MenuManager = Me.barManager
        Me.txtNOMOR_HP1.Name = "txtNOMOR_HP1"
        Me.txtNOMOR_HP1.Size = New System.Drawing.Size(235, 20)
        Me.txtNOMOR_HP1.StyleController = Me.LayoutControl1
        Me.txtNOMOR_HP1.TabIndex = 164
        '
        'cboAGAMA
        '
        Me.cboAGAMA.EditValue = "ISLAM"
        Me.cboAGAMA.Location = New System.Drawing.Point(152, 440)
        Me.cboAGAMA.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cboAGAMA.Name = "cboAGAMA"
        Me.cboAGAMA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboAGAMA.Properties.Items.AddRange(New Object() {"ISLAM", "PROTESTAN", "KATHOLIK", "HINDU", "BUDHA", "DLL"})
        Me.cboAGAMA.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cboAGAMA.Size = New System.Drawing.Size(235, 20)
        Me.cboAGAMA.StyleController = Me.LayoutControl1
        Me.cboAGAMA.TabIndex = 159
        '
        'txtNOMOR_KTP
        '
        Me.txtNOMOR_KTP.Location = New System.Drawing.Point(152, 176)
        Me.txtNOMOR_KTP.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNOMOR_KTP.MenuManager = Me.barManager
        Me.txtNOMOR_KTP.Name = "txtNOMOR_KTP"
        Me.txtNOMOR_KTP.Size = New System.Drawing.Size(235, 20)
        Me.txtNOMOR_KTP.StyleController = Me.LayoutControl1
        Me.txtNOMOR_KTP.TabIndex = 159
        '
        'chkISACTIVE
        '
        Me.chkISACTIVE.Location = New System.Drawing.Point(330, 128)
        Me.chkISACTIVE.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkISACTIVE.MenuManager = Me.barManager
        Me.chkISACTIVE.Name = "chkISACTIVE"
        Me.chkISACTIVE.Properties.Caption = "Active?"
        Me.chkISACTIVE.Properties.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        Me.chkISACTIVE.Size = New System.Drawing.Size(57, 19)
        Me.chkISACTIVE.StyleController = Me.LayoutControl1
        Me.chkISACTIVE.TabIndex = 104
        Me.chkISACTIVE.TabStop = False
        '
        'txtNOMOR_NIP
        '
        Me.txtNOMOR_NIP.Location = New System.Drawing.Point(152, 320)
        Me.txtNOMOR_NIP.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNOMOR_NIP.MenuManager = Me.barManager
        Me.txtNOMOR_NIP.Name = "txtNOMOR_NIP"
        Me.txtNOMOR_NIP.Size = New System.Drawing.Size(235, 20)
        Me.txtNOMOR_NIP.StyleController = Me.LayoutControl1
        Me.txtNOMOR_NIP.TabIndex = 144
        '
        'cboJENISKELAMIN
        '
        Me.cboJENISKELAMIN.EditValue = "LAKI-LAKI"
        Me.cboJENISKELAMIN.Location = New System.Drawing.Point(152, 416)
        Me.cboJENISKELAMIN.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cboJENISKELAMIN.Name = "cboJENISKELAMIN"
        Me.cboJENISKELAMIN.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboJENISKELAMIN.Properties.Items.AddRange(New Object() {"LAKI-LAKI", "PEREMPUAN"})
        Me.cboJENISKELAMIN.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cboJENISKELAMIN.Size = New System.Drawing.Size(235, 20)
        Me.cboJENISKELAMIN.StyleController = Me.LayoutControl1
        Me.cboJENISKELAMIN.TabIndex = 67
        '
        'txtTEMPATLAHIR
        '
        Me.txtTEMPATLAHIR.Location = New System.Drawing.Point(152, 368)
        Me.txtTEMPATLAHIR.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtTEMPATLAHIR.MenuManager = Me.barManager
        Me.txtTEMPATLAHIR.Name = "txtTEMPATLAHIR"
        Me.txtTEMPATLAHIR.Size = New System.Drawing.Size(235, 20)
        Me.txtTEMPATLAHIR.StyleController = Me.LayoutControl1
        Me.txtTEMPATLAHIR.TabIndex = 72
        '
        'deDATETANGGALLAHIR
        '
        Me.deDATETANGGALLAHIR.EditValue = Nothing
        Me.deDATETANGGALLAHIR.EnterMoveNextControl = True
        Me.deDATETANGGALLAHIR.Location = New System.Drawing.Point(152, 392)
        Me.deDATETANGGALLAHIR.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.deDATETANGGALLAHIR.Name = "deDATETANGGALLAHIR"
        Me.deDATETANGGALLAHIR.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deDATETANGGALLAHIR.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.deDATETANGGALLAHIR.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.deDATETANGGALLAHIR.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.deDATETANGGALLAHIR.Size = New System.Drawing.Size(235, 20)
        Me.deDATETANGGALLAHIR.StyleController = Me.LayoutControl1
        Me.deDATETANGGALLAHIR.TabIndex = 68
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lJENISKELAMIN, Me.lAGAMA, Me.lNOMOR_HP1, Me.lNOMOR_HP2, Me.lEMAIL, Me.lKDSTAFF, Me.lISACTIVE, Me.lSTATUS, Me.lKDSTAFFBAGIAN, Me.lKDSTAFFPANGKAT, Me.lKDSTAFFJABATAN, Me.lKDSTAFFPENDIDIKAN, Me.lNAME_DISPLAY, Me.lNOMOR_NIP, Me.lTMT, Me.lNOMOR_KTP, Me.lTEMPATLAHIR, Me.lTANGGALLAHIR, Me.lDESCRIPTION, Me.LayoutControlItem1, Me.LayoutControlItem2, Me.lATTACMENT, Me.LayoutControlItem3})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.OptionsItemText.TextToControlDistance = 4
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(399, 592)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'lJENISKELAMIN
        '
        Me.lJENISKELAMIN.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lJENISKELAMIN.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lJENISKELAMIN.Control = Me.cboJENISKELAMIN
        Me.lJENISKELAMIN.Location = New System.Drawing.Point(0, 404)
        Me.lJENISKELAMIN.Name = "lJENISKELAMIN"
        Me.lJENISKELAMIN.Size = New System.Drawing.Size(379, 24)
        Me.lJENISKELAMIN.Text = "Jenis Kelamin :"
        Me.lJENISKELAMIN.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lJENISKELAMIN.TextSize = New System.Drawing.Size(135, 20)
        Me.lJENISKELAMIN.TextToControlDistance = 5
        '
        'lAGAMA
        '
        Me.lAGAMA.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lAGAMA.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lAGAMA.Control = Me.cboAGAMA
        Me.lAGAMA.Location = New System.Drawing.Point(0, 428)
        Me.lAGAMA.Name = "lAGAMA"
        Me.lAGAMA.Size = New System.Drawing.Size(379, 24)
        Me.lAGAMA.Text = "Agama :"
        Me.lAGAMA.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lAGAMA.TextSize = New System.Drawing.Size(135, 20)
        Me.lAGAMA.TextToControlDistance = 5
        '
        'lNOMOR_HP1
        '
        Me.lNOMOR_HP1.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lNOMOR_HP1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lNOMOR_HP1.Control = Me.txtNOMOR_HP1
        Me.lNOMOR_HP1.Location = New System.Drawing.Point(0, 452)
        Me.lNOMOR_HP1.Name = "lNOMOR_HP1"
        Me.lNOMOR_HP1.Size = New System.Drawing.Size(379, 24)
        Me.lNOMOR_HP1.Text = "No HP 1 :"
        Me.lNOMOR_HP1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lNOMOR_HP1.TextSize = New System.Drawing.Size(135, 20)
        Me.lNOMOR_HP1.TextToControlDistance = 5
        '
        'lNOMOR_HP2
        '
        Me.lNOMOR_HP2.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lNOMOR_HP2.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lNOMOR_HP2.Control = Me.txtNOMOR_HP2
        Me.lNOMOR_HP2.Location = New System.Drawing.Point(0, 476)
        Me.lNOMOR_HP2.Name = "lNOMOR_HP2"
        Me.lNOMOR_HP2.Size = New System.Drawing.Size(379, 24)
        Me.lNOMOR_HP2.Text = "No HP 2 :"
        Me.lNOMOR_HP2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lNOMOR_HP2.TextSize = New System.Drawing.Size(135, 20)
        Me.lNOMOR_HP2.TextToControlDistance = 5
        '
        'lEMAIL
        '
        Me.lEMAIL.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lEMAIL.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lEMAIL.Control = Me.txtEMAIL
        Me.lEMAIL.Location = New System.Drawing.Point(0, 500)
        Me.lEMAIL.Name = "lEMAIL"
        Me.lEMAIL.Size = New System.Drawing.Size(379, 24)
        Me.lEMAIL.Text = "Email :"
        Me.lEMAIL.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lEMAIL.TextSize = New System.Drawing.Size(135, 20)
        Me.lEMAIL.TextToControlDistance = 5
        '
        'lKDSTAFF
        '
        Me.lKDSTAFF.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKDSTAFF.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKDSTAFF.Control = Me.txtKDSTAFF
        Me.lKDSTAFF.Location = New System.Drawing.Point(0, 116)
        Me.lKDSTAFF.Name = "lKDSTAFF"
        Me.lKDSTAFF.Size = New System.Drawing.Size(318, 24)
        Me.lKDSTAFF.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKDSTAFF.TextSize = New System.Drawing.Size(135, 20)
        Me.lKDSTAFF.TextToControlDistance = 5
        '
        'lISACTIVE
        '
        Me.lISACTIVE.Control = Me.chkISACTIVE
        Me.lISACTIVE.Location = New System.Drawing.Point(318, 116)
        Me.lISACTIVE.Name = "lISACTIVE"
        Me.lISACTIVE.Size = New System.Drawing.Size(61, 24)
        Me.lISACTIVE.TextSize = New System.Drawing.Size(0, 0)
        Me.lISACTIVE.TextVisible = False
        '
        'lSTATUS
        '
        Me.lSTATUS.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lSTATUS.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lSTATUS.Control = Me.cboStatus
        Me.lSTATUS.Location = New System.Drawing.Point(0, 524)
        Me.lSTATUS.Name = "lSTATUS"
        Me.lSTATUS.Size = New System.Drawing.Size(379, 24)
        Me.lSTATUS.Text = "Status :"
        Me.lSTATUS.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lSTATUS.TextSize = New System.Drawing.Size(135, 20)
        Me.lSTATUS.TextToControlDistance = 5
        '
        'lKDSTAFFBAGIAN
        '
        Me.lKDSTAFFBAGIAN.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKDSTAFFBAGIAN.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKDSTAFFBAGIAN.Control = Me.grdKDSTAFFBAGIAN
        Me.lKDSTAFFBAGIAN.Location = New System.Drawing.Point(0, 188)
        Me.lKDSTAFFBAGIAN.Name = "lKDSTAFFBAGIAN"
        Me.lKDSTAFFBAGIAN.Size = New System.Drawing.Size(379, 24)
        Me.lKDSTAFFBAGIAN.Text = "Bagian :"
        Me.lKDSTAFFBAGIAN.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKDSTAFFBAGIAN.TextSize = New System.Drawing.Size(135, 20)
        Me.lKDSTAFFBAGIAN.TextToControlDistance = 5
        '
        'lKDSTAFFPANGKAT
        '
        Me.lKDSTAFFPANGKAT.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKDSTAFFPANGKAT.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKDSTAFFPANGKAT.Control = Me.grdKDSTAFPANGKAT
        Me.lKDSTAFFPANGKAT.Location = New System.Drawing.Point(0, 212)
        Me.lKDSTAFFPANGKAT.Name = "lKDSTAFFPANGKAT"
        Me.lKDSTAFFPANGKAT.Size = New System.Drawing.Size(379, 24)
        Me.lKDSTAFFPANGKAT.Text = "Pangkat :"
        Me.lKDSTAFFPANGKAT.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKDSTAFFPANGKAT.TextSize = New System.Drawing.Size(135, 20)
        Me.lKDSTAFFPANGKAT.TextToControlDistance = 5
        '
        'lKDSTAFFJABATAN
        '
        Me.lKDSTAFFJABATAN.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKDSTAFFJABATAN.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKDSTAFFJABATAN.Control = Me.grdKDSTAFJABATAN
        Me.lKDSTAFFJABATAN.Location = New System.Drawing.Point(0, 236)
        Me.lKDSTAFFJABATAN.Name = "lKDSTAFFJABATAN"
        Me.lKDSTAFFJABATAN.Size = New System.Drawing.Size(379, 24)
        Me.lKDSTAFFJABATAN.Text = "Jabatan :"
        Me.lKDSTAFFJABATAN.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKDSTAFFJABATAN.TextSize = New System.Drawing.Size(135, 20)
        Me.lKDSTAFFJABATAN.TextToControlDistance = 5
        '
        'lKDSTAFFPENDIDIKAN
        '
        Me.lKDSTAFFPENDIDIKAN.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKDSTAFFPENDIDIKAN.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKDSTAFFPENDIDIKAN.Control = Me.grdKDSTAFFPENDIDIKAN
        Me.lKDSTAFFPENDIDIKAN.Location = New System.Drawing.Point(0, 260)
        Me.lKDSTAFFPENDIDIKAN.Name = "lKDSTAFFPENDIDIKAN"
        Me.lKDSTAFFPENDIDIKAN.Size = New System.Drawing.Size(379, 24)
        Me.lKDSTAFFPENDIDIKAN.Text = "Pendidikan :"
        Me.lKDSTAFFPENDIDIKAN.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKDSTAFFPENDIDIKAN.TextSize = New System.Drawing.Size(135, 20)
        Me.lKDSTAFFPENDIDIKAN.TextToControlDistance = 5
        '
        'lNAME_DISPLAY
        '
        Me.lNAME_DISPLAY.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lNAME_DISPLAY.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lNAME_DISPLAY.Control = Me.txtNAME_DISPLAY
        Me.lNAME_DISPLAY.Location = New System.Drawing.Point(0, 284)
        Me.lNAME_DISPLAY.Name = "lNAME_DISPLAY"
        Me.lNAME_DISPLAY.Size = New System.Drawing.Size(379, 24)
        Me.lNAME_DISPLAY.Text = "Nama :"
        Me.lNAME_DISPLAY.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lNAME_DISPLAY.TextSize = New System.Drawing.Size(135, 20)
        Me.lNAME_DISPLAY.TextToControlDistance = 5
        '
        'lNOMOR_NIP
        '
        Me.lNOMOR_NIP.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lNOMOR_NIP.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lNOMOR_NIP.Control = Me.txtNOMOR_NIP
        Me.lNOMOR_NIP.Location = New System.Drawing.Point(0, 308)
        Me.lNOMOR_NIP.Name = "lNOMOR_NIP"
        Me.lNOMOR_NIP.Size = New System.Drawing.Size(379, 24)
        Me.lNOMOR_NIP.Text = "Nomor NIK/NIP :"
        Me.lNOMOR_NIP.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lNOMOR_NIP.TextSize = New System.Drawing.Size(135, 20)
        Me.lNOMOR_NIP.TextToControlDistance = 5
        '
        'lTMT
        '
        Me.lTMT.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lTMT.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lTMT.Control = Me.deDATETMTKERJA
        Me.lTMT.Location = New System.Drawing.Point(0, 332)
        Me.lTMT.Name = "lTMT"
        Me.lTMT.Size = New System.Drawing.Size(379, 24)
        Me.lTMT.Text = "TMT Kerja :"
        Me.lTMT.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lTMT.TextSize = New System.Drawing.Size(135, 20)
        Me.lTMT.TextToControlDistance = 5
        '
        'lNOMOR_KTP
        '
        Me.lNOMOR_KTP.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lNOMOR_KTP.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lNOMOR_KTP.Control = Me.txtNOMOR_KTP
        Me.lNOMOR_KTP.Location = New System.Drawing.Point(0, 164)
        Me.lNOMOR_KTP.Name = "lNOMOR_KTP"
        Me.lNOMOR_KTP.Size = New System.Drawing.Size(379, 24)
        Me.lNOMOR_KTP.Text = "Nomor KTP :"
        Me.lNOMOR_KTP.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lNOMOR_KTP.TextSize = New System.Drawing.Size(135, 20)
        Me.lNOMOR_KTP.TextToControlDistance = 5
        '
        'lTEMPATLAHIR
        '
        Me.lTEMPATLAHIR.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lTEMPATLAHIR.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lTEMPATLAHIR.Control = Me.txtTEMPATLAHIR
        Me.lTEMPATLAHIR.Location = New System.Drawing.Point(0, 356)
        Me.lTEMPATLAHIR.Name = "lTEMPATLAHIR"
        Me.lTEMPATLAHIR.Size = New System.Drawing.Size(379, 24)
        Me.lTEMPATLAHIR.Text = "Tmp. Tgl. Lahir :"
        Me.lTEMPATLAHIR.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lTEMPATLAHIR.TextSize = New System.Drawing.Size(135, 20)
        Me.lTEMPATLAHIR.TextToControlDistance = 5
        '
        'lTANGGALLAHIR
        '
        Me.lTANGGALLAHIR.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lTANGGALLAHIR.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lTANGGALLAHIR.Control = Me.deDATETANGGALLAHIR
        Me.lTANGGALLAHIR.Location = New System.Drawing.Point(0, 380)
        Me.lTANGGALLAHIR.Name = "lTANGGALLAHIR"
        Me.lTANGGALLAHIR.Size = New System.Drawing.Size(379, 24)
        Me.lTANGGALLAHIR.Text = "Tanggal Lahir :"
        Me.lTANGGALLAHIR.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lTANGGALLAHIR.TextSize = New System.Drawing.Size(135, 20)
        Me.lTANGGALLAHIR.TextToControlDistance = 5
        '
        'lDESCRIPTION
        '
        Me.lDESCRIPTION.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lDESCRIPTION.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lDESCRIPTION.Control = Me.txtDESCRIPTION
        Me.lDESCRIPTION.Location = New System.Drawing.Point(0, 548)
        Me.lDESCRIPTION.Name = "lDESCRIPTION"
        Me.lDESCRIPTION.Size = New System.Drawing.Size(379, 24)
        Me.lDESCRIPTION.Text = "Deskripsi :"
        Me.lDESCRIPTION.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lDESCRIPTION.TextSize = New System.Drawing.Size(135, 20)
        Me.lDESCRIPTION.TextToControlDistance = 5
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.txtMessage
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(151, 116)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.picFinger
        Me.LayoutControlItem2.Location = New System.Drawing.Point(151, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(110, 116)
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'lATTACMENT
        '
        Me.lATTACMENT.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lATTACMENT.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lATTACMENT.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.lATTACMENT.Control = Me.picGAMBAR
        Me.lATTACMENT.Location = New System.Drawing.Point(261, 0)
        Me.lATTACMENT.Name = "lATTACMENT"
        Me.lATTACMENT.Size = New System.Drawing.Size(118, 116)
        Me.lATTACMENT.Text = "Foto :"
        Me.lATTACMENT.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lATTACMENT.TextSize = New System.Drawing.Size(0, 0)
        Me.lATTACMENT.TextToControlDistance = 0
        Me.lATTACMENT.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem3.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem3.Control = Me.grdKDUSER
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 140)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(379, 24)
        Me.LayoutControlItem3.Text = "User Login :"
        Me.LayoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(135, 20)
        Me.LayoutControlItem3.TextToControlDistance = 5
        '
        'LabelControl13
        '
        Me.LabelControl13.Location = New System.Drawing.Point(725, 91)
        Me.LabelControl13.Name = "LabelControl13"
        Me.LabelControl13.Size = New System.Drawing.Size(4, 13)
        Me.LabelControl13.TabIndex = 73
        Me.LabelControl13.Text = "."
        '
        'BarDockControl1
        '
        Me.BarDockControl1.CausesValidation = False
        Me.BarDockControl1.Dock = System.Windows.Forms.DockStyle.Right
        Me.BarDockControl1.Location = New System.Drawing.Point(934, 0)
        Me.BarDockControl1.Size = New System.Drawing.Size(0, 592)
        '
        'fileDialog
        '
        Me.fileDialog.Filter = "All files|*.*"
        '
        'frmStaff
        '
        Me.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(934, 614)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Controls.Add(Me.LabelControl13)
        Me.Controls.Add(Me.BarDockControl1)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Name = "frmStaff"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNAME_DISPLAY.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.grdKDUSER.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picFinger, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDSTAFFPENDIDIKAN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDSTAFFBAGIAN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picGAMBAR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDESCRIPTION.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATETMTKERJA.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATETMTKERJA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboStatus.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDSTAFJABATAN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvKDSTAFFJABATAN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDSTAFPANGKAT.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvKDSTAFFPANGKAT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtKDSTAFF.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEMAIL.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNOMOR_HP2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNOMOR_HP1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboAGAMA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNOMOR_KTP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkISACTIVE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNOMOR_NIP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboJENISKELAMIN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTEMPATLAHIR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATETANGGALLAHIR.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATETANGGALLAHIR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lJENISKELAMIN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lAGAMA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lNOMOR_HP1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lNOMOR_HP2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lEMAIL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKDSTAFF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lISACTIVE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lSTATUS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKDSTAFFBAGIAN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKDSTAFFPANGKAT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKDSTAFFJABATAN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKDSTAFFPENDIDIKAN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lNAME_DISPLAY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lNOMOR_NIP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lTMT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lNOMOR_KTP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lTEMPATLAHIR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lTANGGALLAHIR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lDESCRIPTION, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lATTACMENT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents barManager As DevExpress.XtraBars.BarManager
    Friend WithEvents barTop As DevExpress.XtraBars.Bar
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents btnSaveNew As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnClose As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnSaveClose As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents progressBarSave As DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar
    Friend WithEvents progressSave As DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar
    Friend WithEvents txtNAME_DISPLAY As DevExpress.XtraEditors.TextEdit
    Private WithEvents txtMessage As System.Windows.Forms.TextBox
    Private WithEvents picFinger As System.Windows.Forms.PictureBox
    Friend WithEvents LabelControl13 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtTEMPATLAHIR As DevExpress.XtraEditors.TextEdit
    Friend WithEvents deDATETANGGALLAHIR As DevExpress.XtraEditors.DateEdit
    Friend WithEvents cboJENISKELAMIN As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents chkISACTIVE As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents BarDockControl1 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents txtNOMOR_NIP As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents lJENISKELAMIN As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lTEMPATLAHIR As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lTANGGALLAHIR As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lNAME_DISPLAY As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lNOMOR_NIP As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lISACTIVE As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents cboAGAMA As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents txtNOMOR_KTP As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lNOMOR_KTP As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lAGAMA As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtEMAIL As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNOMOR_HP2 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNOMOR_HP1 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lNOMOR_HP1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lNOMOR_HP2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lEMAIL As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtKDSTAFF As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lKDSTAFF As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grdKDSTAFJABATAN As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents grvKDSTAFFJABATAN As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colMEMOJABATAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdKDSTAFPANGKAT As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents grvKDSTAFFPANGKAT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colMEMOPANGKAT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lKDSTAFFPANGKAT As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lKDSTAFFJABATAN As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents fileDialog As OpenFileDialog
    Friend WithEvents cboStatus As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents lSTATUS As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents deDATETMTKERJA As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lTMT As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn31 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents txtDESCRIPTION As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lDESCRIPTION As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents btnUpload As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents picGAMBAR As PictureBox
    Friend WithEvents lATTACMENT As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grdKDSTAFFPENDIDIKAN As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdKDSTAFFBAGIAN As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lKDSTAFFBAGIAN As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lKDSTAFFPENDIDIKAN As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grdKDUSER As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
End Class
