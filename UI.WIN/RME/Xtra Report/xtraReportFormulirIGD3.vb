Public Class xtraReportFormulirIGD3
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        If sPictureLogo IsNot Nothing Then
            XrPictureBox3.Image = sPictureLogo
        End If

        lblJudul.Text = sCompany & vbCrLf & "INSTALASI GAWAT DARURAT"
    End Sub
End Class