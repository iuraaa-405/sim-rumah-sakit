Public Class frmPesertaBPJS
    Public Sub LoadMe(ByVal Judul As String, ByVal Pasien As String, ByVal JenisPeserta As String, ByVal NoKartuBPJS As String, ByVal NoRMRS As String, ByVal NoRMBPJS As String, ByVal JenisKelamin As String, ByVal TanggalLahir As String, ByVal Umur As String, ByVal Faskes As String, ByVal Message As String)
        lblME.Text = IIf(NoRMRS <> NoRMBPJS, Judul & " (No RM Tidak Sama)", Judul)
        txtPasien.Text = Pasien
        txtJenisPeserta.Text = JenisPeserta
        txtKARTUBPJS.Text = NoKartuBPJS
        txtNoRMRS.Text = NoRMRS
        txtNoRMBPJS.Text = NoRMBPJS
        txtJenisKelamin.Text = JenisKelamin
        txtTanggalLahir.Text = TanggalLahir
        txtUmur.Text = Umur
        txtFaskes.Text = Faskes
        txtMEMO.Text = Message
        txtPasien.Focus()
    End Sub
    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub
    Private Sub txtPasien_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPasien.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Me.Close()
        End If
    End Sub
End Class