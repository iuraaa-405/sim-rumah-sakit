Public Class xtraReportEMedrekRJ_35
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        If sPictureLogo IsNot Nothing Then
            XrPictureBox1.Image = sPictureLogo
        End If
    End Sub
    Private Sub lPicture5_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles lPicture5.BeforePrint
        If sAttacment_1 IsNot Nothing Then
            Dim sCast = ByteArrayToImage(sAttacment_1.ToArray())

            lPicture5.Image = sCast
        End If
    End Sub
    Private Function ByteArrayToImage(ByVal byteArrayIn() As Byte) As Image
        Using ms As New System.IO.MemoryStream(byteArrayIn)
            Dim returnImage = Image.FromStream(ms)
            Return returnImage
        End Using
    End Function
End Class