Imports QRCoder
Public Class xtraReportEMedrekRJ_26
    Private Sub picPerawat_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles picPerawat.BeforePrint
        Try
            Dim gen As New QRCodeGenerator
            Dim data = gen.CreateQrCode(sTandaTanganUser, QRCodeGenerator.ECCLevel.Q)
            Dim code As New QRCode(data)
            picPerawat.Image = code.GetGraphic(6)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub picDokter_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles picDokter.BeforePrint
        Try
            Dim gen As New QRCodeGenerator
            Dim data = gen.CreateQrCode(sTandaTanganDokterPelaksana, QRCodeGenerator.ECCLevel.Q)
            Dim code As New QRCode(data)
            picDokter.Image = code.GetGraphic(6)
        Catch ex As Exception

        End Try
    End Sub
End Class