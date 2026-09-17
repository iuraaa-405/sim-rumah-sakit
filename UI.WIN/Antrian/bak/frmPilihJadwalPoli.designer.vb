<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmPilihJadwalPoli
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
        Me.layoutControl = New DevExpress.XtraLayout.LayoutControl()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.grdKDDEPARTMENT = New DevExpress.XtraGrid.GridControl()
        Me.grvKDDEPARTMENT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.grdKDDOCTOR = New DevExpress.XtraGrid.GridControl()
        Me.grvKDDOCTOR = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.layoutControl.SuspendLayout()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDDEPARTMENT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvKDDEPARTMENT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDDOCTOR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvKDDOCTOR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'layoutControl
        '
        Me.layoutControl.Controls.Add(Me.GroupControl2)
        Me.layoutControl.Controls.Add(Me.GroupControl1)
        Me.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutControl.Location = New System.Drawing.Point(0, 0)
        Me.layoutControl.Name = "layoutControl"
        Me.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(774, 238, 250, 350)
        Me.layoutControl.Root = Me.LayoutControlGroup1
        Me.layoutControl.Size = New System.Drawing.Size(894, 627)
        Me.layoutControl.TabIndex = 0
        Me.layoutControl.Text = "LayoutControl1"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem3, Me.LayoutControlItem1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(894, 627)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'grdKDDEPARTMENT
        '
        Me.grdKDDEPARTMENT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdKDDEPARTMENT.Location = New System.Drawing.Point(2, 26)
        Me.grdKDDEPARTMENT.MainView = Me.grvKDDEPARTMENT
        Me.grdKDDEPARTMENT.Name = "grdKDDEPARTMENT"
        Me.grdKDDEPARTMENT.Size = New System.Drawing.Size(429, 575)
        Me.grdKDDEPARTMENT.TabIndex = 19
        Me.grdKDDEPARTMENT.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvKDDEPARTMENT})
        '
        'grvKDDEPARTMENT
        '
        Me.grvKDDEPARTMENT.GridControl = Me.grdKDDEPARTMENT
        Me.grvKDDEPARTMENT.Name = "grvKDDEPARTMENT"
        Me.grvKDDEPARTMENT.OptionsCustomization.AllowColumnMoving = False
        Me.grvKDDEPARTMENT.OptionsCustomization.AllowFilter = False
        Me.grvKDDEPARTMENT.OptionsCustomization.AllowGroup = False
        Me.grvKDDEPARTMENT.OptionsCustomization.AllowQuickHideColumns = False
        Me.grvKDDEPARTMENT.OptionsCustomization.AllowSort = False
        Me.grvKDDEPARTMENT.OptionsDetail.EnableMasterViewMode = False
        Me.grvKDDEPARTMENT.OptionsFind.AllowFindPanel = False
        Me.grvKDDEPARTMENT.OptionsLayout.StoreAllOptions = True
        Me.grvKDDEPARTMENT.OptionsLayout.StoreAppearance = True
        Me.grvKDDEPARTMENT.OptionsMenu.EnableColumnMenu = False
        Me.grvKDDEPARTMENT.OptionsNavigation.AutoFocusNewRow = True
        Me.grvKDDEPARTMENT.OptionsNavigation.EnterMoveNextColumn = True
        Me.grvKDDEPARTMENT.OptionsView.EnableAppearanceEvenRow = True
        Me.grvKDDEPARTMENT.OptionsView.EnableAppearanceOddRow = True
        Me.grvKDDEPARTMENT.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.grvKDDEPARTMENT.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.grvKDDEPARTMENT.OptionsView.ShowFooter = True
        Me.grvKDDEPARTMENT.OptionsView.ShowGroupPanel = False
        '
        'grdKDDOCTOR
        '
        Me.grdKDDOCTOR.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdKDDOCTOR.Location = New System.Drawing.Point(2, 26)
        Me.grdKDDOCTOR.MainView = Me.grvKDDOCTOR
        Me.grdKDDOCTOR.Name = "grdKDDOCTOR"
        Me.grdKDDOCTOR.Size = New System.Drawing.Size(429, 575)
        Me.grdKDDOCTOR.TabIndex = 20
        Me.grdKDDOCTOR.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvKDDOCTOR})
        '
        'grvKDDOCTOR
        '
        Me.grvKDDOCTOR.GridControl = Me.grdKDDOCTOR
        Me.grvKDDOCTOR.Name = "grvKDDOCTOR"
        Me.grvKDDOCTOR.OptionsCustomization.AllowColumnMoving = False
        Me.grvKDDOCTOR.OptionsCustomization.AllowFilter = False
        Me.grvKDDOCTOR.OptionsCustomization.AllowGroup = False
        Me.grvKDDOCTOR.OptionsCustomization.AllowQuickHideColumns = False
        Me.grvKDDOCTOR.OptionsCustomization.AllowSort = False
        Me.grvKDDOCTOR.OptionsDetail.EnableMasterViewMode = False
        Me.grvKDDOCTOR.OptionsFind.AllowFindPanel = False
        Me.grvKDDOCTOR.OptionsLayout.StoreAllOptions = True
        Me.grvKDDOCTOR.OptionsLayout.StoreAppearance = True
        Me.grvKDDOCTOR.OptionsMenu.EnableColumnMenu = False
        Me.grvKDDOCTOR.OptionsNavigation.AutoFocusNewRow = True
        Me.grvKDDOCTOR.OptionsNavigation.EnterMoveNextColumn = True
        Me.grvKDDOCTOR.OptionsView.EnableAppearanceEvenRow = True
        Me.grvKDDOCTOR.OptionsView.EnableAppearanceOddRow = True
        Me.grvKDDOCTOR.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.grvKDDOCTOR.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.grvKDDOCTOR.OptionsView.ShowFooter = True
        Me.grvKDDOCTOR.OptionsView.ShowGroupPanel = False
        '
        'GroupControl1
        '
        Me.GroupControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl1.Appearance.Options.UseFont = True
        Me.GroupControl1.AppearanceCaption.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl1.AppearanceCaption.Options.UseFont = True
        Me.GroupControl1.Controls.Add(Me.grdKDDEPARTMENT)
        Me.GroupControl1.Location = New System.Drawing.Point(12, 12)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(433, 603)
        Me.GroupControl1.TabIndex = 21
        Me.GroupControl1.Text = "Poli"
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.GroupControl1
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(437, 607)
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'GroupControl2
        '
        Me.GroupControl2.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl2.Appearance.Options.UseFont = True
        Me.GroupControl2.AppearanceCaption.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl2.AppearanceCaption.Options.UseFont = True
        Me.GroupControl2.Controls.Add(Me.grdKDDOCTOR)
        Me.GroupControl2.Location = New System.Drawing.Point(449, 12)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(433, 603)
        Me.GroupControl2.TabIndex = 22
        Me.GroupControl2.Text = "Dokter"
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.GroupControl2
        Me.LayoutControlItem1.Location = New System.Drawing.Point(437, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(437, 607)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'frmPilihJadwalPoli
        '
        Me.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(894, 627)
        Me.Controls.Add(Me.layoutControl)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.Name = "frmPilihJadwalPoli"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.layoutControl.ResumeLayout(False)
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDDEPARTMENT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvKDDEPARTMENT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDDOCTOR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvKDDOCTOR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents layoutControl As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grdKDDOCTOR As DevExpress.XtraGrid.GridControl
    Friend WithEvents grvKDDOCTOR As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grdKDDEPARTMENT As DevExpress.XtraGrid.GridControl
    Friend WithEvents grvKDDEPARTMENT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
End Class
