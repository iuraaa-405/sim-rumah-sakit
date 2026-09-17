Public Class xtraReportAskepGeriatriLembar1_4
    Private Sub XrPictureBox2_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles XrPictureBox2.BeforePrint
        sAttacment_1 = Me.GetCurrentColumnValue("picGambar")
        If sAttacment_1 IsNot Nothing Then
            Dim sCast = ByteArrayToImage(sAttacment_1.ToArray())
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