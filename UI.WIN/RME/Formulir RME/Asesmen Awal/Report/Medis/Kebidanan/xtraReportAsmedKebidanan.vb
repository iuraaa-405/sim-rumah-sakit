'Imports QRCoder

Public Class xtraReportAsmedKebidanan
    Private Sub xtraReportAsmedKebidanan_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles MyBase.BeforePrint
        If sPictureLogo IsNot Nothing Then
            XrPictureBox1.Image = sPictureLogo
        End If
        If sAttacment_1 IsNot Nothing Then
            Dim sCast = ByteArrayToImage(sAttacment_1.ToArray())

            'Dim img As Image = CType(ic.ConvertFrom(sCast), Image)
            picPerut.Image = sCast
        End If
    End Sub
    Private Function ByteArrayToImage(ByVal byteArrayIn() As Byte) As Image
        Using ms As New System.IO.MemoryStream(byteArrayIn)
            Dim returnImage = Image.FromStream(ms)
            Return returnImage
        End Using
    End Function
End Class