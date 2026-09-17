Public Class xtraReportAsmedRajal
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        Try
            If sPictureLogo IsNot Nothing Then
                XrPictureBox3.Image = sPictureLogo
            End If

            Dim alamatttd As Object = GetCurrentColumnValue("MemoEdit60")

            If alamatttd IsNot Nothing Then
                If IO.Directory.Exists(sAlamatSimpanFolder) Then
                    Dim img As System.Drawing.Image = System.Drawing.Image.FromFile(alamatttd.ToString())
                    XrPictureBox1.Image = img
                    sPicture = XrPictureBox1.Image
                Else
                    XrPictureBox1.Image = CType(My.Resources.ResourceManager.GetObject("image1"), Image)
                End If
            End If

            'lblUMUR.Text = sUmurPasienDiCPPT
        Catch ex As Exception

        End Try
    End Sub
End Class