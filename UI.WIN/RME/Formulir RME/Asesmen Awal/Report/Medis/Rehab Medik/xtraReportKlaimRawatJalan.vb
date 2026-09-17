Imports QRCoder

Public Class xtraReportKlaimRawatJalan
    Private Sub ReportFooter_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles ReportFooter.BeforePrint
        Try
            Dim gen As New QRCodeGenerator
            Dim data = gen.CreateQrCode(sTandaTanganUserSIMRS, QRCodeGenerator.ECCLevel.Q)
            Dim code As New QRCode(data)
            picPetugas.Image = code.GetGraphic(6)
        Catch ex As Exception
            picPetugas.Visible = False
        End Try

        If sTandaTanganPasien = "" Then
            picPasien.Visible = False
        Else
            Try
                Dim gen As New QRCodeGenerator
                Dim data = gen.CreateQrCode(sTandaTanganPasien, QRCodeGenerator.ECCLevel.Q)
                Dim code As New QRCode(data)
                picPasien.Image = code.GetGraphic(6)
            Catch ex As Exception
                picPasien.Visible = False
            End Try
        End If
    End Sub
End Class