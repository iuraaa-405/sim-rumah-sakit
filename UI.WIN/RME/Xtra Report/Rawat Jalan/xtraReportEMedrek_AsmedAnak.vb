Imports QRCoder

Public Class xtraReportEMedrek_AsmedAnak
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
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

    Private Sub lPicture2_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles lPicture2.BeforePrint
        If sAttacment_1 IsNot Nothing Then
            Dim sCast = ByteArrayToImage(sAttacment_1.ToArray())

            'Dim img As Image = CType(ic.ConvertFrom(sCast), Image)
            lPicture2.Image = sCast
        End If
    End Sub
    Private Sub lPicture3_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles lPicture3.BeforePrint
        If sAttacment_2 IsNot Nothing Then
            Dim sCast = ByteArrayToImage(sAttacment_2.ToArray())

            'Dim img As Image = CType(ic.ConvertFrom(sCast), Image)
            lPicture3.Image = sCast
        End If
    End Sub
    Private Sub lPicture4_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles lPicture4.BeforePrint
        If sAttachGambarPerut IsNot Nothing Then
            Dim sCast = ByteArrayToImage(sAttachGambarPerut.ToArray())

            'Dim img As Image = CType(ic.ConvertFrom(sCast), Image)
            lPicture4.Image = sCast
        End If
    End Sub
    Private Sub picDokter_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles picDokter.BeforePrint
        Try
            Dim gen As New QRCodeGenerator
            Dim data = gen.CreateQrCode(sTandaTanganUser, QRCodeGenerator.ECCLevel.Q)
            Dim code As New QRCode(data)
            picDokter.Image = code.GetGraphic(6)
        Catch ex As Exception
            picDokter.Visible = False
        End Try
    End Sub
End Class