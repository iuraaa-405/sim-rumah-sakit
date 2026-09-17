Imports QRCoder

Public Class xtraReportEMedrekRJ_23
    Private Function ByteArrayToImage(ByVal byteArrayIn() As Byte) As Image
        Using ms As New System.IO.MemoryStream(byteArrayIn)
            Dim returnImage = Image.FromStream(ms)
            Return returnImage
        End Using
    End Function

    Private Sub XrTableCell6_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles XrTableCell6.BeforePrint
        Try
            Dim gen As New QRCodeGenerator
            Dim data = gen.CreateQrCode(sTandaTanganPasien, QRCodeGenerator.ECCLevel.Q)
            Dim code As New QRCode(data)
            picPasien.Image = code.GetGraphic(6)
        Catch ex As Exception

        End Try
        Try
            Dim gen As New QRCodeGenerator
            Dim data = gen.CreateQrCode(sTandaTanganUser, QRCodeGenerator.ECCLevel.Q)
            Dim code As New QRCode(data)
            picPetugas.Image = code.GetGraphic(6)
        Catch ex As Exception

        End Try
    End Sub
End Class