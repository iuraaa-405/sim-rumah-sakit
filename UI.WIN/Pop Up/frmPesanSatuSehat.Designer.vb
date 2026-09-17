<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmPesanSatuSehat
    Inherits System.Windows.Forms.Form

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
        Me.btnOK = New DevExpress.XtraEditors.SimpleButton()
        Me.txtJson = New DevExpress.XtraEditors.MemoEdit()
        Me.lblKodeRespon = New DevExpress.XtraEditors.LabelControl()
        CType(Me.txtJson.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOK.Appearance.Options.UseFont = True
        Me.btnOK.Location = New System.Drawing.Point(23, 487)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(396, 41)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "OK"
        '
        'txtJson
        '
        Me.txtJson.Location = New System.Drawing.Point(23, 41)
        Me.txtJson.Name = "txtJson"
        Me.txtJson.Size = New System.Drawing.Size(396, 440)
        Me.txtJson.TabIndex = 3
        '
        'lblKodeRespon
        '
        Me.lblKodeRespon.Location = New System.Drawing.Point(23, 21)
        Me.lblKodeRespon.Name = "lblKodeRespon"
        Me.lblKodeRespon.Size = New System.Drawing.Size(18, 13)
        Me.lblKodeRespon.TabIndex = 4
        Me.lblKodeRespon.Text = "000"
        '
        'frmPesanSatuSehat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(442, 540)
        Me.Controls.Add(Me.lblKodeRespon)
        Me.Controls.Add(Me.txtJson)
        Me.Controls.Add(Me.btnOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPesanSatuSehat"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.txtJson.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnOK As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtJson As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents lblKodeRespon As DevExpress.XtraEditors.LabelControl
End Class
