'Imports iPOS.DA
Imports QRCoder

Public Class xtraReportEMedrek_Konsul
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        Try
            'Konsul
            Try
                'Dim gen As New QRCodeGenerator
                'Dim data = gen.CreateQrCode("https://rsdustira.com/Verifikasi/" & sKDDOCTOR_KONSUL, QRCodeGenerator.ECCLevel.Q)
                'Dim code As New QRCode(data)
                'picKonsul.Image = code.GetGraphic(6)
                'lblCatatan.Text = "Ditandatangani secara elektronik"

                picKonsul.Image = Image.FromFile("\\172.165.115.200\Software\SCAN_TTD_DOKTER\" & sKDDOCTOR_KONSUL & ".jpg")
            Catch ex As Exception
                picKonsul.Visible = False
            End Try


            'Jawab Konsul
            Try
                'Dim genJ As New QRCodeGenerator
                'Dim dataJ = genJ.CreateQrCode("https://rsdustira.com/Verifikasi/" & sKDDOCTOR_JAWABKONSUL, QRCodeGenerator.ECCLevel.Q)
                'Dim codeJ As New QRCode(dataJ)
                'picJawabKonsul.Image = codeJ.GetGraphic(6)

                picJawabKonsul.Image = Image.FromFile("\\172.165.115.200\Software\SCAN_TTD_DOKTER\" & sKDDOCTOR_JAWABKONSUL & ".jpg")
            Catch ex As Exception
                picJawabKonsul.Visible = False
            End Try


        Catch oErr As Exception
            MsgBox("Print QR : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
End Class