<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmKonsul
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
        Me.chkISALIHLEADER = New DevExpress.XtraEditors.CheckEdit()
        Me.barManager = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barTop = New DevExpress.XtraBars.Bar()
        Me.btnSaveClose = New DevExpress.XtraBars.BarButtonItem()
        Me.btnClose = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.btnSaveNew = New DevExpress.XtraBars.BarButtonItem()
        Me.progressBarSave = New DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar()
        Me.progressSave = New DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar()
        Me.chkISSEWAKTU = New DevExpress.XtraEditors.CheckEdit()
        Me.chkISRUBBER = New DevExpress.XtraEditors.CheckEdit()
        Me.chkISPERIKSAPOLI = New DevExpress.XtraEditors.CheckEdit()
        Me.grdKDOCTOR_FROM = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.grvKDDOCTORFROM = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.deDATE = New DevExpress.XtraEditors.DateEdit()
        Me.grdKDOCTOR_TO = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.grvKDDOKTERTO = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPOLI = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colKDDEPARTMENT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtKONSUL = New DevExpress.XtraEditors.MemoEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.lMEMO = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lRABER = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lKONSULSEWAKTU = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lALIHLEADER = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.layoutControl,System.ComponentModel.ISupportInitialize).BeginInit
        Me.layoutControl.SuspendLayout
        CType(Me.chkISALIHLEADER.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.barManager,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.progressBarSave,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.progressSave,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.chkISSEWAKTU.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.chkISRUBBER.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.chkISPERIKSAPOLI.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.grdKDOCTOR_FROM.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.grvKDDOCTORFROM,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.deDATE.Properties.CalendarTimeProperties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.deDATE.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.grdKDOCTOR_TO.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.grvKDDOKTERTO,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtKONSUL.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.lMEMO,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem2,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.EmptySpaceItem1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem3,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem4,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.lRABER,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.lKONSULSEWAKTU,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.lALIHLEADER,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'layoutControl
        '
        Me.layoutControl.Controls.Add(Me.chkISALIHLEADER)
        Me.layoutControl.Controls.Add(Me.chkISSEWAKTU)
        Me.layoutControl.Controls.Add(Me.chkISRUBBER)
        Me.layoutControl.Controls.Add(Me.chkISPERIKSAPOLI)
        Me.layoutControl.Controls.Add(Me.grdKDOCTOR_FROM)
        Me.layoutControl.Controls.Add(Me.deDATE)
        Me.layoutControl.Controls.Add(Me.grdKDOCTOR_TO)
        Me.layoutControl.Controls.Add(Me.txtKONSUL)
        Me.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutControl.Location = New System.Drawing.Point(0, 0)
        Me.layoutControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.layoutControl.Name = "layoutControl"
        Me.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(774, 238, 250, 350)
        Me.layoutControl.Root = Me.LayoutControlGroup1
        Me.layoutControl.Size = New System.Drawing.Size(913, 631)
        Me.layoutControl.TabIndex = 0
        Me.layoutControl.Text = "LayoutControl1"
        '
        'chkISALIHLEADER
        '
        Me.chkISALIHLEADER.Location = New System.Drawing.Point(525, 42)
        Me.chkISALIHLEADER.MenuManager = Me.barManager
        Me.chkISALIHLEADER.Name = "chkISALIHLEADER"
        Me.chkISALIHLEADER.Properties.Caption = "Alih Leader ?"
        Me.chkISALIHLEADER.Properties.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        Me.chkISALIHLEADER.Size = New System.Drawing.Size(372, 20)
        Me.chkISALIHLEADER.StyleController = Me.layoutControl
        Me.chkISALIHLEADER.TabIndex = 28
        '
        'barManager
        '
        Me.barManager.AllowQuickCustomization = false
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
        Me.barTop.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.btnSaveClose), New DevExpress.XtraBars.LinkPersistInfo(Me.btnClose)})
        Me.barTop.OptionsBar.DrawDragBorder = false
        Me.barTop.OptionsBar.MultiLine = true
        Me.barTop.OptionsBar.UseWholeRow = true
        Me.barTop.Text = "Main menu"
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
        Me.barDockControlTop.CausesValidation = false
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.barDockControlTop.Size = New System.Drawing.Size(913, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = false
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 631)
        Me.barDockControlBottom.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.barDockControlBottom.Size = New System.Drawing.Size(913, 29)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = false
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlLeft.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 631)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = false
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(913, 0)
        Me.barDockControlRight.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 631)
        '
        'btnSaveNew
        '
        Me.btnSaveNew.Caption = "F2 - Save && New"
        Me.btnSaveNew.Id = 2
        Me.btnSaveNew.Name = "btnSaveNew"
        '
        'progressBarSave
        '
        Me.progressBarSave.Name = "progressBarSave"
        Me.progressBarSave.Stopped = true
        '
        'progressSave
        '
        Me.progressSave.Name = "progressSave"
        Me.progressSave.Paused = true
        '
        'chkISSEWAKTU
        '
        Me.chkISSEWAKTU.Location = New System.Drawing.Point(653, 16)
        Me.chkISSEWAKTU.MenuManager = Me.barManager
        Me.chkISSEWAKTU.Name = "chkISSEWAKTU"
        Me.chkISSEWAKTU.Properties.Caption = "Konsul Sewaktu ?"
        Me.chkISSEWAKTU.Properties.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        Me.chkISSEWAKTU.Size = New System.Drawing.Size(244, 20)
        Me.chkISSEWAKTU.StyleController = Me.layoutControl
        Me.chkISSEWAKTU.TabIndex = 27
        '
        'chkISRUBBER
        '
        Me.chkISRUBBER.Location = New System.Drawing.Point(525, 16)
        Me.chkISRUBBER.MenuManager = Me.barManager
        Me.chkISRUBBER.Name = "chkISRUBBER"
        Me.chkISRUBBER.Properties.Caption = "Rawat Bersama ?"
        Me.chkISRUBBER.Properties.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        Me.chkISRUBBER.Size = New System.Drawing.Size(122, 20)
        Me.chkISRUBBER.StyleController = Me.layoutControl
        Me.chkISRUBBER.TabIndex = 26
        '
        'chkISPERIKSAPOLI
        '
        Me.chkISPERIKSAPOLI.Location = New System.Drawing.Point(424, 16)
        Me.chkISPERIKSAPOLI.MenuManager = Me.barManager
        Me.chkISPERIKSAPOLI.Name = "chkISPERIKSAPOLI"
        Me.chkISPERIKSAPOLI.Properties.Caption = "Periksa Poli"
        Me.chkISPERIKSAPOLI.Properties.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        Me.chkISPERIKSAPOLI.Size = New System.Drawing.Size(95, 20)
        Me.chkISPERIKSAPOLI.StyleController = Me.layoutControl
        Me.chkISPERIKSAPOLI.TabIndex = 25
        '
        'grdKDOCTOR_FROM
        '
        Me.grdKDOCTOR_FROM.EditValue = ""
        Me.grdKDOCTOR_FROM.EnterMoveNextControl = true
        Me.grdKDOCTOR_FROM.Location = New System.Drawing.Point(121, 44)
        Me.grdKDOCTOR_FROM.MenuManager = Me.barManager
        Me.grdKDOCTOR_FROM.Name = "grdKDOCTOR_FROM"
        Me.grdKDOCTOR_FROM.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDOCTOR_FROM.Properties.NullText = ""
        Me.grdKDOCTOR_FROM.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdKDOCTOR_FROM.Properties.View = Me.grvKDDOCTORFROM
        Me.grdKDOCTOR_FROM.Size = New System.Drawing.Size(398, 22)
        Me.grdKDOCTOR_FROM.StyleController = Me.layoutControl
        Me.grdKDOCTOR_FROM.TabIndex = 24
        '
        'grvKDDOCTORFROM
        '
        Me.grvKDDOCTORFROM.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1})
        Me.grvKDDOCTORFROM.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvKDDOCTORFROM.Name = "grvKDDOCTORFROM"
        Me.grvKDDOCTORFROM.OptionsSelection.EnableAppearanceFocusedCell = false
        Me.grvKDDOCTORFROM.OptionsView.ShowAutoFilterRow = true
        Me.grvKDDOCTORFROM.OptionsView.ShowGroupPanel = false
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Dokter"
        Me.GridColumn1.FieldName = "NAME_DISPLAY"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = true
        Me.GridColumn1.VisibleIndex = 0
        '
        'deDATE
        '
        Me.deDATE.EditValue = Nothing
        Me.deDATE.EnterMoveNextControl = true
        Me.deDATE.Location = New System.Drawing.Point(121, 16)
        Me.deDATE.MenuManager = Me.barManager
        Me.deDATE.Name = "deDATE"
        Me.deDATE.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deDATE.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.deDATE.Properties.Mask.EditMask = "dd/MM/yyyy HH:mm"
        Me.deDATE.Properties.Mask.UseMaskAsDisplayFormat = true
        Me.deDATE.Size = New System.Drawing.Size(297, 22)
        Me.deDATE.StyleController = Me.layoutControl
        Me.deDATE.TabIndex = 21
        '
        'grdKDOCTOR_TO
        '
        Me.grdKDOCTOR_TO.EditValue = ""
        Me.grdKDOCTOR_TO.EnterMoveNextControl = true
        Me.grdKDOCTOR_TO.Location = New System.Drawing.Point(121, 72)
        Me.grdKDOCTOR_TO.MenuManager = Me.barManager
        Me.grdKDOCTOR_TO.Name = "grdKDOCTOR_TO"
        Me.grdKDOCTOR_TO.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDOCTOR_TO.Properties.NullText = ""
        Me.grdKDOCTOR_TO.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdKDOCTOR_TO.Properties.View = Me.grvKDDOKTERTO
        Me.grdKDOCTOR_TO.Size = New System.Drawing.Size(398, 22)
        Me.grdKDOCTOR_TO.StyleController = Me.layoutControl
        Me.grdKDOCTOR_TO.TabIndex = 23
        '
        'grvKDDOKTERTO
        '
        Me.grvKDDOKTERTO.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.colPOLI, Me.colKDDEPARTMENT})
        Me.grvKDDOKTERTO.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvKDDOKTERTO.Name = "grvKDDOKTERTO"
        Me.grvKDDOKTERTO.OptionsSelection.EnableAppearanceFocusedCell = false
        Me.grvKDDOKTERTO.OptionsView.ShowAutoFilterRow = true
        Me.grvKDDOKTERTO.OptionsView.ShowGroupPanel = false
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Dokter"
        Me.GridColumn3.FieldName = "NAME_DISPLAY"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = true
        Me.GridColumn3.VisibleIndex = 0
        '
        'colPOLI
        '
        Me.colPOLI.Caption = "Poli"
        Me.colPOLI.FieldName = "POLI"
        Me.colPOLI.Name = "colPOLI"
        Me.colPOLI.Visible = true
        Me.colPOLI.VisibleIndex = 1
        '
        'colKDDEPARTMENT
        '
        Me.colKDDEPARTMENT.Caption = "KDDEPARTMENT"
        Me.colKDDEPARTMENT.FieldName = "KDDEPARTMENT"
        Me.colKDDEPARTMENT.Name = "colKDDEPARTMENT"
        '
        'txtKONSUL
        '
        Me.txtKONSUL.Location = New System.Drawing.Point(121, 100)
        Me.txtKONSUL.MenuManager = Me.barManager
        Me.txtKONSUL.Name = "txtKONSUL"
        Me.txtKONSUL.Size = New System.Drawing.Size(776, 515)
        Me.txtKONSUL.StyleController = Me.layoutControl
        Me.txtKONSUL.TabIndex = 15
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = false
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lMEMO, Me.LayoutControlItem1, Me.LayoutControlItem2, Me.EmptySpaceItem1, Me.LayoutControlItem3, Me.LayoutControlItem4, Me.lRABER, Me.lKONSULSEWAKTU, Me.lALIHLEADER})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(913, 631)
        Me.LayoutControlGroup1.TextVisible = false
        '
        'lMEMO
        '
        Me.lMEMO.AppearanceItemCaption.Options.UseTextOptions = true
        Me.lMEMO.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lMEMO.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.lMEMO.Control = Me.txtKONSUL
        Me.lMEMO.Location = New System.Drawing.Point(0, 84)
        Me.lMEMO.Name = "lMEMO"
        Me.lMEMO.Size = New System.Drawing.Size(887, 521)
        Me.lMEMO.Text = "Isi Konsul :"
        Me.lMEMO.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lMEMO.TextSize = New System.Drawing.Size(100, 20)
        Me.lMEMO.TextToControlDistance = 5
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.AppearanceItemCaption.Options.UseTextOptions = true
        Me.LayoutControlItem1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem1.Control = Me.grdKDOCTOR_TO
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 56)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(509, 28)
        Me.LayoutControlItem1.Text = "Kepada Dokter :"
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(100, 20)
        Me.LayoutControlItem1.TextToControlDistance = 5
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.AppearanceItemCaption.Options.UseTextOptions = true
        Me.LayoutControlItem2.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem2.Control = Me.deDATE
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(408, 28)
        Me.LayoutControlItem2.Text = "Tanggal :"
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(100, 20)
        Me.LayoutControlItem2.TextToControlDistance = 5
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = false
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(509, 52)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(378, 32)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.AppearanceItemCaption.Options.UseTextOptions = true
        Me.LayoutControlItem3.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem3.Control = Me.grdKDOCTOR_FROM
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 28)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(509, 28)
        Me.LayoutControlItem3.Text = "Dari Dokter :"
        Me.LayoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(100, 20)
        Me.LayoutControlItem3.TextToControlDistance = 5
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.chkISPERIKSAPOLI
        Me.LayoutControlItem4.Location = New System.Drawing.Point(408, 0)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(101, 28)
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextVisible = false
        '
        'lRABER
        '
        Me.lRABER.Control = Me.chkISRUBBER
        Me.lRABER.Location = New System.Drawing.Point(509, 0)
        Me.lRABER.Name = "lRABER"
        Me.lRABER.Size = New System.Drawing.Size(128, 26)
        Me.lRABER.TextSize = New System.Drawing.Size(0, 0)
        Me.lRABER.TextVisible = false
        '
        'lKONSULSEWAKTU
        '
        Me.lKONSULSEWAKTU.Control = Me.chkISSEWAKTU
        Me.lKONSULSEWAKTU.Location = New System.Drawing.Point(637, 0)
        Me.lKONSULSEWAKTU.Name = "lKONSULSEWAKTU"
        Me.lKONSULSEWAKTU.Size = New System.Drawing.Size(250, 26)
        Me.lKONSULSEWAKTU.TextSize = New System.Drawing.Size(0, 0)
        Me.lKONSULSEWAKTU.TextVisible = false
        '
        'lALIHLEADER
        '
        Me.lALIHLEADER.Control = Me.chkISALIHLEADER
        Me.lALIHLEADER.Location = New System.Drawing.Point(509, 26)
        Me.lALIHLEADER.Name = "lALIHLEADER"
        Me.lALIHLEADER.Size = New System.Drawing.Size(378, 26)
        Me.lALIHLEADER.TextSize = New System.Drawing.Size(0, 0)
        Me.lALIHLEADER.TextVisible = false
        '
        'frmKonsul
        '
        Me.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(235,Byte),Integer), CType(CType(236,Byte),Integer), CType(CType(239,Byte),Integer))
        Me.Appearance.Options.UseBackColor = true
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7!, 16!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(913, 660)
        Me.Controls.Add(Me.layoutControl)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = true
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmKonsul"
        Me.ShowIcon = false
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.layoutControl,System.ComponentModel.ISupportInitialize).EndInit
        Me.layoutControl.ResumeLayout(false)
        CType(Me.chkISALIHLEADER.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.barManager,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.progressBarSave,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.progressSave,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.chkISSEWAKTU.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.chkISRUBBER.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.chkISPERIKSAPOLI.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.grdKDOCTOR_FROM.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.grvKDDOCTORFROM,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.deDATE.Properties.CalendarTimeProperties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.deDATE.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.grdKDOCTOR_TO.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.grvKDDOKTERTO,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtKONSUL.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.lMEMO,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.EmptySpaceItem1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem3,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem4,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.lRABER,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.lKONSULSEWAKTU,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.lALIHLEADER,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents layoutControl As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
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
    Friend WithEvents txtKONSUL As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents lMEMO As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grdKDOCTOR_TO As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents grvKDDOKTERTO As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grdKDOCTOR_FROM As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents grvKDDOCTORFROM As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents deDATE As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents chkISPERIKSAPOLI As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPOLI As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colKDDEPARTMENT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents chkISSEWAKTU As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkISRUBBER As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents lRABER As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lKONSULSEWAKTU As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents chkISALIHLEADER As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents lALIHLEADER As DevExpress.XtraLayout.LayoutControlItem
End Class
