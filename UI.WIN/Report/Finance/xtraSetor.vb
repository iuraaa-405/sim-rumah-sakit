Public Class xtraSetor
    Private Sub xtraCash_BeforePrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        'lblJudul1.Text = sNPWP
        'lblJudul2.Text = sCompany

        Try
            If sPictureLogo IsNot Nothing Then
                XrPictureBox1.Image = sPictureLogo
            End If

            'lblUMUR.Text = sUmurPasienDiCPPT
        Catch ex As Exception

        End Try

        lblJudul1.Text = sCompany.ToUpper()
        lblJudul2.Text = sAddress
        'lblCATEGORY.Text = sCATEGORYSETOR
    End Sub
End Class