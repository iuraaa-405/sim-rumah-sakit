Public Class xtraKontraBon
    Private Sub xtraCashIn_BeforePrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        Try
            If sPictureLogo IsNot Nothing Then
                XrPictureBox1.Image = sPictureLogo
            End If

            'lblUMUR.Text = sUmurPasienDiCPPT
        Catch ex As Exception

        End Try

        lblJudul1.Text = sCompany.ToUpper()
        lblJudul2.Text = sAddress
    End Sub
End Class