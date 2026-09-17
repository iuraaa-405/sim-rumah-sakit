
Public Class xtraReportEMedrekRJ_41
    Private Sub picKeluarga_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles picKeluarga.BeforePrint
        If sPictureLogo IsNot Nothing Then
            XrPictureBox1.Image = sPictureLogo
        End If
    End Sub
End Class