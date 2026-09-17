Imports QRCoder
Public Class xtraReportEMedrekRJ_41
    Private Sub picKeluarga_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles picKeluarga.BeforePrint
        Try
            Dim gen As New QRCodeGenerator
            Dim data = gen.CreateQrCode(sTandaTanganPasien, QRCodeGenerator.ECCLevel.Q)
            Dim code As New QRCode(data)
            picKeluarga.Image = code.GetGraphic(6)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub picPetugas_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles picPetugas.BeforePrint
        Try
            Dim gen As New QRCodeGenerator
            Dim data = gen.CreateQrCode(sTandaTanganUser, QRCodeGenerator.ECCLevel.Q)
            Dim code As New QRCode(data)
            picPetugas.Image = code.GetGraphic(6)
        Catch ex As Exception

        End Try
    End Sub
End Class