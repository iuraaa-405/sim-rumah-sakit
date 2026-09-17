Public Class xtraReportKARTUANESTESI_A
    Public sAttacment_1
    Public sAttacment_2

    Private Sub XrPictureBox1_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles XrPictureBox1.BeforePrint
        If sAttacment_1 IsNot Nothing Then
            Dim sCast = ByteArrayToImage(sAttacment_1.ToArray())
            XrPictureBox1.Image = sCast
        End If
    End Sub

    Private Sub XrPictureBox2_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles XrPictureBox2.BeforePrint
        If sAttacment_2 IsNot Nothing Then
            Dim sCast = ByteArrayToImage(sAttacment_2.ToArray())
            XrPictureBox2.Image = sCast
        End If
    End Sub

    Private Function ByteArrayToImage(ByVal byteArrayIn() As Byte) As Image
        Using ms As New System.IO.MemoryStream(byteArrayIn)
            Dim returnImage = Image.FromStream(ms)
            Return returnImage
        End Using
    End Function
End Class