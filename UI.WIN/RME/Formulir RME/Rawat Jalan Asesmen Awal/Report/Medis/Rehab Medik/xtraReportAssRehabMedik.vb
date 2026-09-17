Public Class xtraReportAssRehabMedik
    Private Sub picGambarWanita_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles picGambarWanita.BeforePrint
         If sAttachGambarWanita IsNot Nothing Then
            Dim sCast = ByteArrayToImage(sAttachGambarWanita.ToArray())
            picGambarWanita.Image = sCast
        End If
    End Sub

     Private Function ByteArrayToImage(ByVal byteArrayIn() As Byte) As Image
        Using ms As New System.IO.MemoryStream(byteArrayIn)
            Dim returnImage = Image.FromStream(ms)
            Return returnImage
        End Using
    End Function
End Class