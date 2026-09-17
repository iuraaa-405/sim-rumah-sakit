Public Class frmQRKartuBerobat
    Private Sub btnBayar_Click(sender As Object, e As EventArgs)
        sBayar = True
        Me.Close()

    End Sub
    Private Sub btnBayarTidak_Click(sender As Object, e As EventArgs)
        sBayar = False
        Me.Close()

    End Sub
End Class