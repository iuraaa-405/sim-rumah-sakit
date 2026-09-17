<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmFingerController
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
        Me.btnBack = New System.Windows.Forms.Button()
        Me.txtIdentify = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'btnBack
        '
        Me.btnBack.Location = New System.Drawing.Point(276, 229)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(75, 23)
        Me.btnBack.TabIndex = 4
        Me.btnBack.Text = "OK"
        '
        'txtIdentify
        '
        Me.txtIdentify.Location = New System.Drawing.Point(12, 10)
        Me.txtIdentify.Multiline = True
        Me.txtIdentify.Name = "txtIdentify"
        Me.txtIdentify.Size = New System.Drawing.Size(339, 213)
        Me.txtIdentify.TabIndex = 3
        '
        'frmFingerController
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(354, 269)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.txtIdentify)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(374, 312)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(374, 312)
        Me.Name = "frmFingerController"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Identification"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents txtIdentify As System.Windows.Forms.TextBox
End Class
