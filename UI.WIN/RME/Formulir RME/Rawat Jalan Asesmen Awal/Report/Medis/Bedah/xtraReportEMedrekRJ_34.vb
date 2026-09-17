Imports QRCoder

Public Class xtraReportEMedrekRJ_34
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        If sAttacment_1 IsNot Nothing Then
            Dim sCast = ByteArrayToImage(sAttacment_1.ToArray())

            'Dim img As Image = CType(ic.ConvertFrom(sCast), Image)
            lPicture2.Image = sCast
        End If

        Try
            Dim gen As New QRCodeGenerator
            Dim data = gen.CreateQrCode(sTandaTanganUser, QRCodeGenerator.ECCLevel.Q)
            Dim code As New QRCode(data)
            picPetugas.Image = code.GetGraphic(6)
        Catch ex As Exception

        End Try
    End Sub
    Private Function ByteArrayToImage(ByVal byteArrayIn() As Byte) As Image
        Using ms As New System.IO.MemoryStream(byteArrayIn)
            Dim returnImage = Image.FromStream(ms)
            Return returnImage
        End Using
    End Function
End Class