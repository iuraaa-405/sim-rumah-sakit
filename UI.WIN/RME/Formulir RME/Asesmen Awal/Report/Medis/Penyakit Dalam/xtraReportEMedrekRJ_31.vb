Public Class xtraReportEMedrekRJ_31
    Private Sub xtra_BeforePrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        If sPictureLogo IsNot Nothing Then
            XrPictureBox1.Image = sPictureLogo
        End If
    End Sub
End Class