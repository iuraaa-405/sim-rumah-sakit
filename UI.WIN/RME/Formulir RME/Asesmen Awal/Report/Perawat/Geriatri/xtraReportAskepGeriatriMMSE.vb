Public Class xtraReportAskepGeriatriMMSE
    Private Sub pic1_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles pic1.BeforePrint
        sAttacment_1 = Me.GetCurrentColumnValue("picGAMBAR1")
        If sAttacment_1 IsNot Nothing Then
            Dim sCast = ByteArrayToImage(sAttacment_1.ToArray())
            pic1.Image = sCast
        End If
    End Sub

    Private Sub picKalimat_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles picKalimat.BeforePrint
        sAttacment_1 = Me.GetCurrentColumnValue("picKALIMAT")
        If sAttacment_1 IsNot Nothing Then
            Dim sCast = ByteArrayToImage(sAttacment_1.ToArray())
            picKalimat.Image = sCast
        End If
    End Sub

    Private Sub pic2_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles pic2.BeforePrint
        sAttacment_1 = Me.GetCurrentColumnValue("picGAMBAR2")
        If sAttacment_1 IsNot Nothing Then
            Dim sCast = ByteArrayToImage(sAttacment_1.ToArray())
            pic2.Image = sCast
        End If
    End Sub

    Private Function ByteArrayToImage(ByVal byteArrayIn() As Byte) As Image
        Using ms As New System.IO.MemoryStream(byteArrayIn)
            Dim returnImage = Image.FromStream(ms)
            Return returnImage
        End Using
    End Function
End Class