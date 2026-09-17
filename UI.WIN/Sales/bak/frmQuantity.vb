Public Class frmQuantity
    Public iQuantity As Decimal

    Public Sub fn_Load(ByVal sItem As String)
        txtItemName.Text = sItem
    End Sub

    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        iQuantity = CDbl(txtQuantity.Text)
    End Sub

    Private Sub frmQuantity_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
End Class