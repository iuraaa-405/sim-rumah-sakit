Imports QRCoder
Public Class xtraReportLembarUjiFungsi
    Private Sub xtraReportLembarUjiFungsi_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles MyBase.BeforePrint
        Try
            Dim gen As New QRCodeGenerator
            Dim data = gen.CreateQrCode(sTandaTanganUser, QRCodeGenerator.ECCLevel.Q)
            Dim code As New QRCode(data)
            picTerapis.Image = code.GetGraphic(6)
        Catch ex As Exception
            picTerapis.Visible = False
        End Try
    End Sub
End Class