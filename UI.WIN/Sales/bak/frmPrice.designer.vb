<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrice
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.txtQuantity = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.txtItemName = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.btnSubmit = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.txtPassword = New DevExpress.XtraEditors.TextEdit()
        Me.txtUsername = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.txtQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtItemName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPassword.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUsername.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtQuantity
        '
        Me.txtQuantity.EditValue = "1"
        Me.txtQuantity.EnterMoveNextControl = True
        Me.txtQuantity.Location = New System.Drawing.Point(94, 48)
        Me.txtQuantity.Name = "txtQuantity"
        Me.txtQuantity.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.txtQuantity.Properties.Appearance.Options.UseFont = True
        Me.txtQuantity.Properties.Appearance.Options.UseTextOptions = True
        Me.txtQuantity.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtQuantity.Properties.Mask.EditMask = "n0"
        Me.txtQuantity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtQuantity.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtQuantity.Properties.NullText = "0"
        Me.txtQuantity.Size = New System.Drawing.Size(213, 26)
        Me.txtQuantity.TabIndex = 1
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.LabelControl1.Location = New System.Drawing.Point(43, 51)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(45, 19)
        Me.LabelControl1.TabIndex = 1
        Me.LabelControl1.Text = "Price :"
        '
        'txtItemName
        '
        Me.txtItemName.Location = New System.Drawing.Point(94, 16)
        Me.txtItemName.Name = "txtItemName"
        Me.txtItemName.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.txtItemName.Properties.Appearance.Options.UseFont = True
        Me.txtItemName.Properties.ReadOnly = True
        Me.txtItemName.Size = New System.Drawing.Size(213, 26)
        Me.txtItemName.TabIndex = 0
        Me.txtItemName.TabStop = False
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.LabelControl2.Location = New System.Drawing.Point(44, 19)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(44, 19)
        Me.LabelControl2.TabIndex = 1
        Me.LabelControl2.Text = "Item :"
        '
        'btnSubmit
        '
        Me.btnSubmit.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.btnSubmit.Appearance.Options.UseFont = True
        Me.btnSubmit.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnSubmit.Location = New System.Drawing.Point(232, 144)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(75, 27)
        Me.btnSubmit.TabIndex = 4
        Me.btnSubmit.Text = "Change"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.LabelControl3.Location = New System.Drawing.Point(10, 113)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(78, 19)
        Me.LabelControl3.TabIndex = 1
        Me.LabelControl3.Text = "Password :"
        '
        'txtPassword
        '
        Me.txtPassword.EditValue = ""
        Me.txtPassword.EnterMoveNextControl = True
        Me.txtPassword.Location = New System.Drawing.Point(94, 112)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.txtPassword.Properties.Appearance.Options.UseFont = True
        Me.txtPassword.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(213, 26)
        Me.txtPassword.TabIndex = 3
        '
        'txtUsername
        '
        Me.txtUsername.EnterMoveNextControl = True
        Me.txtUsername.Location = New System.Drawing.Point(94, 80)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.txtUsername.Properties.Appearance.Options.UseFont = True
        Me.txtUsername.Size = New System.Drawing.Size(213, 26)
        Me.txtUsername.TabIndex = 2
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.LabelControl4.Location = New System.Drawing.Point(6, 83)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(82, 19)
        Me.LabelControl4.TabIndex = 5
        Me.LabelControl4.Text = "Username :"
        '
        'frmPrice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(325, 181)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.txtUsername)
        Me.Controls.Add(Me.btnSubmit)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.txtItemName)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.txtQuantity)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.Name = "frmPrice"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Change Price"
        CType(Me.txtQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtItemName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPassword.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUsername.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtQuantity As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtItemName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnSubmit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtPassword As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtUsername As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
End Class
