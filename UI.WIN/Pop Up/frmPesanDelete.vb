Public Class frmPesanDelete
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sPesanHapus = "XXXXXBATALXXXXX"
    End Sub
    Private Sub btnBayar_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        If txtHapus.Text = "" Then
            MsgBox("Alasan Kosong", MsgBoxStyle.Exclamation, Me.Text)
        Else
            sPesanHapus = txtHapus.Text

            Me.Close()
        End If
    End Sub
    Private Sub btnBayarTidak_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        sPesanHapus = "XXXXXBATALXXXXX"
        Me.Close()
    End Sub
End Class