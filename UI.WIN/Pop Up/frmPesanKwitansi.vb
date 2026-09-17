Public Class frmPesanKwitansi
    Private Sub btnBayar_Click(sender As Object, e As EventArgs) Handles btnBayar.Click
        sBayar = True
        Me.Close()

    End Sub
    Private Sub btnBayarTidak_Click(sender As Object, e As EventArgs) Handles btnBayarTidak.Click
        sBayar = False
        Me.Close()

    End Sub
End Class