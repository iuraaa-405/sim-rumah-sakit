<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSetKoneksi
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
        Me.layoutControl = New DevExpress.XtraLayout.LayoutControl()
        Me.chkISACTIVEVERSI2 = New DevExpress.XtraEditors.CheckEdit()
        Me.barManager = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barTop = New DevExpress.XtraBars.Bar()
        Me.btnSaveNew = New DevExpress.XtraBars.BarButtonItem()
        Me.btnSaveClose = New DevExpress.XtraBars.BarButtonItem()
        Me.btnClose = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.progressBarSave = New DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar()
        Me.progressSave = New DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar()
        Me.cboNAME_DISPLAY = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtREMARKS = New DevExpress.XtraEditors.MemoEdit()
        Me.txtSCREATKEY = New DevExpress.XtraEditors.TextEdit()
        Me.txtCONSID = New DevExpress.XtraEditors.TextEdit()
        Me.txtPPKPELAYANAN = New DevExpress.XtraEditors.TextEdit()
        Me.chkISACTIVE = New DevExpress.XtraEditors.CheckEdit()
        Me.txtALALAMTWEB = New DevExpress.XtraEditors.MemoEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.lALAMATWEB = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lPPKPELAYANAN = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lCONSID = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lSCREATKEY = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lREMARKS = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lNAME_DISPLAY = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.layoutControl.SuspendLayout()
        CType(Me.chkISACTIVEVERSI2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboNAME_DISPLAY.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtREMARKS.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSCREATKEY.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCONSID.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPPKPELAYANAN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkISACTIVE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtALALAMTWEB.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lALAMATWEB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lPPKPELAYANAN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lCONSID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lSCREATKEY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lREMARKS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lNAME_DISPLAY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'layoutControl
        '
        Me.layoutControl.Controls.Add(Me.chkISACTIVEVERSI2)
        Me.layoutControl.Controls.Add(Me.cboNAME_DISPLAY)
        Me.layoutControl.Controls.Add(Me.txtREMARKS)
        Me.layoutControl.Controls.Add(Me.txtSCREATKEY)
        Me.layoutControl.Controls.Add(Me.txtCONSID)
        Me.layoutControl.Controls.Add(Me.txtPPKPELAYANAN)
        Me.layoutControl.Controls.Add(Me.chkISACTIVE)
        Me.layoutControl.Controls.Add(Me.txtALALAMTWEB)
        Me.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutControl.Location = New System.Drawing.Point(0, 0)
        Me.layoutControl.Name = "layoutControl"
        Me.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(774, 238, 250, 350)
        Me.layoutControl.Root = Me.LayoutControlGroup1
        Me.layoutControl.Size = New System.Drawing.Size(590, 401)
        Me.layoutControl.TabIndex = 0
        Me.layoutControl.Text = "LayoutControl1"
        '
        'chkISACTIVEVERSI2
        '
        Me.chkISACTIVEVERSI2.EnterMoveNextControl = True
        Me.chkISACTIVEVERSI2.Location = New System.Drawing.Point(491, 35)
        Me.chkISACTIVEVERSI2.MenuManager = Me.barManager
        Me.chkISACTIVEVERSI2.Name = "chkISACTIVEVERSI2"
        Me.chkISACTIVEVERSI2.Properties.Caption = "Versi2?"
        Me.chkISACTIVEVERSI2.Properties.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        Me.chkISACTIVEVERSI2.Size = New System.Drawing.Size(87, 19)
        Me.chkISACTIVEVERSI2.StyleController = Me.layoutControl
        Me.chkISACTIVEVERSI2.TabIndex = 15
        Me.chkISACTIVEVERSI2.TabStop = False
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
        Me.barManager.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.btnSaveNew, Me.btnClose, Me.btnSaveClose})
        Me.barManager.MainMenu = Me.barTop
        Me.barManager.MaxItemId = 8
        Me.barManager.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.progressBarSave, Me.progressSave})
        '
        'barTop
        '
        Me.barTop.BarName = "Main menu"
        Me.barTop.DockCol = 0
        Me.barTop.DockRow = 0
        Me.barTop.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom
        Me.barTop.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.btnSaveNew), New DevExpress.XtraBars.LinkPersistInfo(Me.btnSaveClose), New DevExpress.XtraBars.LinkPersistInfo(Me.btnClose)})
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
        Me.barDockControlTop.Size = New System.Drawing.Size(590, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 401)
        Me.barDockControlBottom.Size = New System.Drawing.Size(590, 22)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 401)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(590, 0)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 401)
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
        'cboNAME_DISPLAY
        '
        Me.cboNAME_DISPLAY.Location = New System.Drawing.Point(117, 12)
        Me.cboNAME_DISPLAY.MenuManager = Me.barManager
        Me.cboNAME_DISPLAY.Name = "cboNAME_DISPLAY"
        Me.cboNAME_DISPLAY.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboNAME_DISPLAY.Properties.Items.AddRange(New Object() {"VCLAIM2", "APLICARE", "ECLAIM", "DATABASE", "VCLAIM2", "ANTREAN"})
        Me.cboNAME_DISPLAY.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cboNAME_DISPLAY.Size = New System.Drawing.Size(370, 20)
        Me.cboNAME_DISPLAY.StyleController = Me.layoutControl
        Me.cboNAME_DISPLAY.TabIndex = 19
        '
        'txtREMARKS
        '
        Me.txtREMARKS.EditValue = ""
        Me.txtREMARKS.Location = New System.Drawing.Point(117, 250)
        Me.txtREMARKS.Name = "txtREMARKS"
        Me.txtREMARKS.Size = New System.Drawing.Size(370, 139)
        Me.txtREMARKS.StyleController = Me.layoutControl
        Me.txtREMARKS.TabIndex = 10
        '
        'txtSCREATKEY
        '
        Me.txtSCREATKEY.Location = New System.Drawing.Point(117, 84)
        Me.txtSCREATKEY.MenuManager = Me.barManager
        Me.txtSCREATKEY.Name = "txtSCREATKEY"
        Me.txtSCREATKEY.Size = New System.Drawing.Size(370, 20)
        Me.txtSCREATKEY.StyleController = Me.layoutControl
        Me.txtSCREATKEY.TabIndex = 18
        '
        'txtCONSID
        '
        Me.txtCONSID.Location = New System.Drawing.Point(117, 60)
        Me.txtCONSID.MenuManager = Me.barManager
        Me.txtCONSID.Name = "txtCONSID"
        Me.txtCONSID.Size = New System.Drawing.Size(370, 20)
        Me.txtCONSID.StyleController = Me.layoutControl
        Me.txtCONSID.TabIndex = 18
        '
        'txtPPKPELAYANAN
        '
        Me.txtPPKPELAYANAN.Location = New System.Drawing.Point(117, 36)
        Me.txtPPKPELAYANAN.MenuManager = Me.barManager
        Me.txtPPKPELAYANAN.Name = "txtPPKPELAYANAN"
        Me.txtPPKPELAYANAN.Size = New System.Drawing.Size(370, 20)
        Me.txtPPKPELAYANAN.StyleController = Me.layoutControl
        Me.txtPPKPELAYANAN.TabIndex = 17
        '
        'chkISACTIVE
        '
        Me.chkISACTIVE.EnterMoveNextControl = True
        Me.chkISACTIVE.Location = New System.Drawing.Point(491, 12)
        Me.chkISACTIVE.MenuManager = Me.barManager
        Me.chkISACTIVE.Name = "chkISACTIVE"
        Me.chkISACTIVE.Properties.Caption = "Active?"
        Me.chkISACTIVE.Properties.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        Me.chkISACTIVE.Size = New System.Drawing.Size(87, 19)
        Me.chkISACTIVE.StyleController = Me.layoutControl
        Me.chkISACTIVE.TabIndex = 14
        Me.chkISACTIVE.TabStop = False
        '
        'txtALALAMTWEB
        '
        Me.txtALALAMTWEB.EditValue = ""
        Me.txtALALAMTWEB.Location = New System.Drawing.Point(117, 108)
        Me.txtALALAMTWEB.Name = "txtALALAMTWEB"
        Me.txtALALAMTWEB.Size = New System.Drawing.Size(370, 138)
        Me.txtALALAMTWEB.StyleController = Me.layoutControl
        Me.txtALALAMTWEB.TabIndex = 9
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lALAMATWEB, Me.LayoutControlItem1, Me.lPPKPELAYANAN, Me.lCONSID, Me.lSCREATKEY, Me.lREMARKS, Me.lNAME_DISPLAY, Me.LayoutControlItem2})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(590, 401)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'lALAMATWEB
        '
        Me.lALAMATWEB.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lALAMATWEB.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lALAMATWEB.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.lALAMATWEB.Control = Me.txtALALAMTWEB
        Me.lALAMATWEB.CustomizationFormText = "Display Name * :"
        Me.lALAMATWEB.Location = New System.Drawing.Point(0, 96)
        Me.lALAMATWEB.Name = "lALAMATWEB"
        Me.lALAMATWEB.Size = New System.Drawing.Size(479, 142)
        Me.lALAMATWEB.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lALAMATWEB.TextSize = New System.Drawing.Size(100, 20)
        Me.lALAMATWEB.TextToControlDistance = 5
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.chkISACTIVE
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(479, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(91, 23)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'lPPKPELAYANAN
        '
        Me.lPPKPELAYANAN.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lPPKPELAYANAN.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lPPKPELAYANAN.Control = Me.txtPPKPELAYANAN
        Me.lPPKPELAYANAN.Location = New System.Drawing.Point(0, 24)
        Me.lPPKPELAYANAN.Name = "lPPKPELAYANAN"
        Me.lPPKPELAYANAN.Size = New System.Drawing.Size(479, 24)
        Me.lPPKPELAYANAN.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lPPKPELAYANAN.TextSize = New System.Drawing.Size(100, 20)
        Me.lPPKPELAYANAN.TextToControlDistance = 5
        '
        'lCONSID
        '
        Me.lCONSID.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lCONSID.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lCONSID.Control = Me.txtCONSID
        Me.lCONSID.Location = New System.Drawing.Point(0, 48)
        Me.lCONSID.Name = "lCONSID"
        Me.lCONSID.Size = New System.Drawing.Size(479, 24)
        Me.lCONSID.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lCONSID.TextSize = New System.Drawing.Size(100, 20)
        Me.lCONSID.TextToControlDistance = 5
        '
        'lSCREATKEY
        '
        Me.lSCREATKEY.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lSCREATKEY.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lSCREATKEY.Control = Me.txtSCREATKEY
        Me.lSCREATKEY.Location = New System.Drawing.Point(0, 72)
        Me.lSCREATKEY.Name = "lSCREATKEY"
        Me.lSCREATKEY.Size = New System.Drawing.Size(479, 24)
        Me.lSCREATKEY.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lSCREATKEY.TextSize = New System.Drawing.Size(100, 20)
        Me.lSCREATKEY.TextToControlDistance = 5
        '
        'lREMARKS
        '
        Me.lREMARKS.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lREMARKS.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lREMARKS.Control = Me.txtREMARKS
        Me.lREMARKS.Location = New System.Drawing.Point(0, 238)
        Me.lREMARKS.Name = "lREMARKS"
        Me.lREMARKS.Size = New System.Drawing.Size(479, 143)
        Me.lREMARKS.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lREMARKS.TextSize = New System.Drawing.Size(100, 20)
        Me.lREMARKS.TextToControlDistance = 5
        '
        'lNAME_DISPLAY
        '
        Me.lNAME_DISPLAY.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lNAME_DISPLAY.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lNAME_DISPLAY.Control = Me.cboNAME_DISPLAY
        Me.lNAME_DISPLAY.Location = New System.Drawing.Point(0, 0)
        Me.lNAME_DISPLAY.Name = "lNAME_DISPLAY"
        Me.lNAME_DISPLAY.Size = New System.Drawing.Size(479, 24)
        Me.lNAME_DISPLAY.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lNAME_DISPLAY.TextSize = New System.Drawing.Size(100, 20)
        Me.lNAME_DISPLAY.TextToControlDistance = 5
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.chkISACTIVEVERSI2
        Me.LayoutControlItem2.Location = New System.Drawing.Point(479, 23)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(91, 358)
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'frmSetKoneksi
        '
        Me.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(590, 423)
        Me.Controls.Add(Me.layoutControl)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.Name = "frmSetKoneksi"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.layoutControl.ResumeLayout(False)
        CType(Me.chkISACTIVEVERSI2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboNAME_DISPLAY.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtREMARKS.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSCREATKEY.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCONSID.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPPKPELAYANAN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkISACTIVE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtALALAMTWEB.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lALAMATWEB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lPPKPELAYANAN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lCONSID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lSCREATKEY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lREMARKS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lNAME_DISPLAY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents layoutControl As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents lALAMATWEB As DevExpress.XtraLayout.LayoutControlItem
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
    Friend WithEvents chkISACTIVE As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtALALAMTWEB As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents txtREMARKS As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents txtSCREATKEY As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCONSID As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtPPKPELAYANAN As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lPPKPELAYANAN As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lCONSID As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lSCREATKEY As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lREMARKS As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents cboNAME_DISPLAY As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents lNAME_DISPLAY As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents chkISACTIVEVERSI2 As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
End Class
