<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChangePassword
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmChangePassword))
        Me.txtCurrent = New DevExpress.XtraEditors.TextEdit()
        Me.txtNew = New DevExpress.XtraEditors.TextEdit()
        Me.lblUsername = New DevExpress.XtraEditors.LabelControl()
        Me.lblPassword = New DevExpress.XtraEditors.LabelControl()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.PictureEdit1 = New DevExpress.XtraEditors.PictureEdit()
        Me.txtConfirm = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.btnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.txtCurrent.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNew.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtConfirm.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtCurrent
        '
        Me.txtCurrent.EnterMoveNextControl = True
        Me.txtCurrent.Location = New System.Drawing.Point(390, 23)
        Me.txtCurrent.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtCurrent.Name = "txtCurrent"
        Me.txtCurrent.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.txtCurrent.Properties.Appearance.Options.UseFont = True
        Me.txtCurrent.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtCurrent.Size = New System.Drawing.Size(216, 36)
        Me.txtCurrent.TabIndex = 0
        '
        'txtNew
        '
        Me.txtNew.EnterMoveNextControl = True
        Me.txtNew.Location = New System.Drawing.Point(390, 70)
        Me.txtNew.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtNew.Name = "txtNew"
        Me.txtNew.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.txtNew.Properties.Appearance.Options.UseFont = True
        Me.txtNew.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtNew.Size = New System.Drawing.Size(216, 36)
        Me.txtNew.TabIndex = 1
        '
        'lblUsername
        '
        Me.lblUsername.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.lblUsername.Location = New System.Drawing.Point(177, 28)
        Me.lblUsername.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.lblUsername.Name = "lblUsername"
        Me.lblUsername.Size = New System.Drawing.Size(204, 29)
        Me.lblUsername.TabIndex = 1
        Me.lblUsername.Text = "Current Password :"
        '
        'lblPassword
        '
        Me.lblPassword.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.lblPassword.Location = New System.Drawing.Point(210, 75)
        Me.lblPassword.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(172, 29)
        Me.lblPassword.TabIndex = 1
        Me.lblPassword.Text = "New Password :"
        '
        'btnCancel
        '
        Me.btnCancel.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.btnCancel.Appearance.Options.UseFont = True
        Me.btnCancel.Location = New System.Drawing.Point(494, 165)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(112, 51)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "&Cancel"
        '
        'PictureEdit1
        '
        Me.PictureEdit1.EditValue = CType(resources.GetObject("PictureEdit1.EditValue"), Object)
        Me.PictureEdit1.Location = New System.Drawing.Point(18, 19)
        Me.PictureEdit1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.PictureEdit1.Name = "PictureEdit1"
        Me.PictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Silver
        Me.PictureEdit1.Properties.Appearance.Options.UseBackColor = True
        Me.PictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.PictureEdit1.Size = New System.Drawing.Size(150, 140)
        Me.PictureEdit1.TabIndex = 4
        '
        'txtConfirm
        '
        Me.txtConfirm.EnterMoveNextControl = True
        Me.txtConfirm.Location = New System.Drawing.Point(390, 117)
        Me.txtConfirm.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtConfirm.Name = "txtConfirm"
        Me.txtConfirm.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.txtConfirm.Properties.Appearance.Options.UseFont = True
        Me.txtConfirm.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtConfirm.Size = New System.Drawing.Size(216, 36)
        Me.txtConfirm.TabIndex = 2
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.LabelControl1.Location = New System.Drawing.Point(171, 121)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(207, 29)
        Me.LabelControl1.TabIndex = 1
        Me.LabelControl1.Text = "Confirm Password :"
        '
        'btnSave
        '
        Me.btnSave.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.btnSave.Appearance.Options.UseFont = True
        Me.btnSave.Location = New System.Drawing.Point(372, 165)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(112, 51)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "&Save"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(31, 265)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(440, 76)
        Me.LabelControl2.TabIndex = 5
        Me.LabelControl2.Text = "- Password minimal 10 karakter" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "- Password mengandung huruf besar (A-Z)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "- Passwo" &
    "rd mengandung huruf kecil (a-z)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "- Password mengandung spesial karakter (!, @ , " &
    "#, %, ^, &, *)"
        '
        'frmChangePassword
        '
        Me.Appearance.BackColor = System.Drawing.Color.Silver
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(639, 376)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.PictureEdit1)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.lblPassword)
        Me.Controls.Add(Me.lblUsername)
        Me.Controls.Add(Me.txtConfirm)
        Me.Controls.Add(Me.txtNew)
        Me.Controls.Add(Me.txtCurrent)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmChangePassword"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Change Password"
        Me.TopMost = True
        CType(Me.txtCurrent.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNew.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtConfirm.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtCurrent As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNew As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblUsername As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblPassword As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents PictureEdit1 As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents txtConfirm As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
End Class
