Public Class frmAntrianJenisKunjungan
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sPOSTRAWAT = False
        sASALRUJUKAN_ANTRIAN = "0"
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
    End Sub
    Private Sub btnBayar_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        sASALRUJUKAN_ANTRIAN = "0"
        Me.Close()
    End Sub
    Private Sub btnBayarTidak_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        sASALRUJUKAN_ANTRIAN = "1"
        Me.Close()
    End Sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        sPOSTRAWAT = True
        Me.Close()
    End Sub
End Class