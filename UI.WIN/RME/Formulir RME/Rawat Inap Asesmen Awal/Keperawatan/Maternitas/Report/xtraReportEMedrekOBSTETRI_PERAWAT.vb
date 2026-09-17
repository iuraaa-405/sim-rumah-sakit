Imports QRCoder
Public Class xtraReportEMedrekOBSTETRI_PERAWAT
    Private Sub picPerawat_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles picPerawat.BeforePrint
        Try
            Dim gen As New QRCodeGenerator
            Dim data = gen.CreateQrCode(sTandaTanganPerawat, QRCodeGenerator.ECCLevel.Q)
            Dim code As New QRCode(data)
            picPerawat.Image = code.GetGraphic(6)
        Catch ex As Exception
            picPerawat.Visible = False
        End Try
    End Sub
End Class