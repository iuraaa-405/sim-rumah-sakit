Imports QRCoder

Public Class xtraReportEMedrekRJ_06
    Private Sub picDietitian_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles picDietitian.BeforePrint
        Try
            Dim gen As New QRCodeGenerator
            Dim data = gen.CreateQrCode(sTandaTanganDietitian, QRCodeGenerator.ECCLevel.Q)
            Dim code As New QRCode(data)
            picDietitian.Image = code.GetGraphic(6)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub picDokter_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles picDokter.BeforePrint
        Try
            Dim gen As New QRCodeGenerator
            Dim data = gen.CreateQrCode(sTandaTanganUser, QRCodeGenerator.ECCLevel.Q)
            Dim code As New QRCode(data)
            picDokter.Image = code.GetGraphic(6)
        Catch ex As Exception

        End Try
    End Sub
End Class