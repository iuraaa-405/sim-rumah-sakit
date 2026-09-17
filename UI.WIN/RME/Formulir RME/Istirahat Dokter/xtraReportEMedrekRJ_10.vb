Imports QRCoder

Public Class xtraReportEMedrekRJ_10
    Private Sub picDokter_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles picDokter.BeforePrint
        Try
            Dim gen As New QRCodeGenerator
            Dim data = gen.CreateQrCode(sTandaTanganUserSIMRS, QRCodeGenerator.ECCLevel.Q)
            Dim code As New QRCode(data)
            picDokter.Image = code.GetGraphic(6)
        Catch ex As Exception

        End Try
    End Sub
End Class