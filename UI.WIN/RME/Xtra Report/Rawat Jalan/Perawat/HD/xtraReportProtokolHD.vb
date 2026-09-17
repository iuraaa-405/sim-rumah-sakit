Imports QRCoder
Imports DataAccess

Public Class xtraReportProtokolHD
    Private Sub ReportFooter_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles ReportFooter.BeforePrint
        Try
            If sTextLBP_PJ <> "" Then
                Dim gen As New QRCodeGenerator
                Dim data = gen.CreateQrCode(sTextLBP_PJ, QRCodeGenerator.ECCLevel.Q)
                Dim code As New QRCode(data)
                pic1.Image = code.GetGraphic(6)
            End If
        Catch ex As Exception
            pic1.Visible = False
        End Try

        Try
            Dim gen As New QRCodeGenerator
            Dim data = gen.CreateQrCode(sTextLBP_PELAKSANA, QRCodeGenerator.ECCLevel.Q)
            Dim code As New QRCode(data)
            pic2.Image = code.GetGraphic(6)
        Catch ex As Exception
            pic2.Visible = False
        End Try

        Try
            Dim gen As New QRCodeGenerator
            Dim data = gen.CreateQrCode(sTextLBP_SUVER, QRCodeGenerator.ECCLevel.Q)
            Dim code As New QRCode(data)
            pic3.Image = code.GetGraphic(6)
        Catch ex As Exception
            pic3.Visible = False
        End Try
    End Sub
End Class