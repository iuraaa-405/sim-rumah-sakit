Imports QRCoder
Public Class xtraReportEMedrekRJ_38
    Private Sub xtraReportEMedrekRJ_38_BeforePrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        Try
            'Dim gen As New QRCodeGenerator
            'Dim data = gen.CreateQrCode(sTandaTanganUser, QRCodeGenerator.ECCLevel.Q)
            'Dim code As New QRCode(data)
            'picPetugas.Image = code.GetGraphic(6)

            picPetugas.Image = Image.FromFile("\\172.165.115.200\Software\SCAN_TTD_DOKTER\" & sKDDOCTOR_DPJP & ".jpg")
            XrPictureBox2.Image = Image.FromFile("\\172.165.115.200\Software\SCAN_TTD_DOKTER\" & sKDDOCTOR_DPJP & ".jpg")
        Catch ex As Exception
            picPetugas.Visible = False
            XrPictureBox2.Visible = False
        End Try
    End Sub
End Class