Imports QRCoder

Public Class xtraReportAsmedKebidanan
    Private Sub xtraReportAsmedKebidanan_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles MyBase.BeforePrint
        If sAttacment_1 IsNot Nothing Then
            Dim sCast = ByteArrayToImage(sAttacment_1.ToArray())

            'Dim img As Image = CType(ic.ConvertFrom(sCast), Image)
            picPerut.Image = sCast
        End If

        Try
            Dim gen As New QRCodeGenerator
            Dim data = gen.CreateQrCode(sTandaTanganUser, QRCodeGenerator.ECCLevel.Q)
            Dim code As New QRCode(data)
            picDokter.Image = code.GetGraphic(6)
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