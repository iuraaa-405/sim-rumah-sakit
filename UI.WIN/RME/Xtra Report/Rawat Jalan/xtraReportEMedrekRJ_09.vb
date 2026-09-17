Imports QRCoder

Public Class xtraReportEMedrekRJ_09
    Private Sub picPerawat_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles picPerawat.BeforePrint
        Try
            Dim gen As New QRCodeGenerator
            Dim data = gen.CreateQrCode(sTandaTanganUser, QRCodeGenerator.ECCLevel.Q)
            Dim code As New QRCode(data)
            picperawat.Image = code.GetGraphic(6)
        Catch ex As Exception

        End Try
    End Sub
End Class