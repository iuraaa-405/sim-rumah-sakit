Public Class xtraReportFormulirIGD1
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint

    End Sub
    Private Sub xrPictureBox1_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) 
        If sAttacment_1 IsNot Nothing Then
            Dim sCast = ByteArrayToImage(sAttacment_1.ToArray())

            'Dim img As Image = CType(ic.ConvertFrom(sCast), Image)
            lPicture2.Image = sCast
        End If
    End Sub
    Private Function ByteArrayToImage(ByVal byteArrayIn() As Byte) As Image
        Using ms As New System.IO.MemoryStream(byteArrayIn)
            Dim returnImage = Image.FromStream(ms)
            Return returnImage
        End Using
    End Function
    'Private Sub xtraReportFormulirIGD1_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles MyBase.BeforePrint
    '    Dim ic As New ImageConverter()
    '    Dim sCast = TryCast(sAttacment_1, Byte())

    '    Dim img As Image = CType(ic.ConvertFrom(TryCast(sAttacment_1, Byte())), Image)
    '    lPicture2.Image = img
    'End Sub
End Class