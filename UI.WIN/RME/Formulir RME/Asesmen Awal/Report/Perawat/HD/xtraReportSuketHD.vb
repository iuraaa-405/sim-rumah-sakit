'Imports QRCoder
'Imports DataAccess
Public Class xtraReportSuketHD
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        Try
            If sPictureLogo IsNot Nothing Then
                XrPictureBox1.Image = sPictureLogo
            End If
        Catch ex As Exception

        End Try
    End Sub
    'Private Sub ReportFooter_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles ReportFooter.BeforePrint
    '    Try
    '        pic1.Image = Nothing

    '        If sTextLBP <> "" Then
    '            Dim gen As New QRCodeGenerator
    '            Dim data = gen.CreateQrCode(sTextLBP, QRCodeGenerator.ECCLevel.Q)
    '            Dim code As New QRCode(data)
    '            pic1.Image = code.GetGraphic(6)
    '        End If
    '    Catch ex As Exception
    '        pic1.Visible = False
    '    End Try

    '    '    Try
    '    '        picDokterPelaksana.Image = Nothing

    '    '        'If sKDDOCTOR_DPJP <> "" Then
    '    '        '    Dim oUser As New Setting.clsUser
    '    '        '    Dim dsUser = oUser.GetDataByDokter(sKDDOCTOR_DPJP)
    '    '        '    If dsUser IsNot Nothing Then
    '    '        '        Dim dsCEK = oUser.GetDataSignatuerUser(dsUser.KDUSER)
    '    '        '        If dsCEK <> "" Then
    '    '        '            Dim gen As New QRCodeGenerator
    '    '        '            Dim data = gen.CreateQrCode("https://rsdustira.co.id/Verifikasi/" & oUser.GetDataSignatuerUser(dsUser.KDUSER) & "/" & Now.ToString("dd-MM-yyyy HH:mm") & "/LBP", QRCodeGenerator.ECCLevel.Q)
    '    '        '            Dim code As New QRCode(data)
    '    '        '            picDokterPelaksana.Image = code.GetGraphic(6)
    '    '        '        Else
    '    '        '            picDokterPelaksana.Image = Image.FromFile("\\172.165.115.200\Software\SCAN_TTD_DOKTER\" & sKDDOCTOR_DPJP & ".jpg")
    '    '        '        End If
    '    '        '    End If
    '    '        'End If

    '    '        If sKDDOCTOR_DPJP <> "" Then
    '    '            Dim oUser As New Setting.clsUser
    '    '            Dim dsUser = oUser.GetDataByDokter(sKDDOCTOR_DPJP)
    '    '            If dsUser IsNot Nothing Then
    '    '                If sTextLBP <> "" Then
    '    '                    Dim gen As New QRCodeGenerator
    '    '                    Dim data = gen.CreateQrCode(sTextLBP, QRCodeGenerator.ECCLevel.Q)
    '    '                    Dim code As New QRCode(data)
    '    '                    picDokterPelaksana.Image = code.GetGraphic(6)
    '    '                Else
    '    '                    picDokterPelaksana.Image = Image.FromFile("\\172.165.115.200\Software\SCAN_TTD_DOKTER\" & sKDDOCTOR_DPJP & ".jpg")
    '    '                End If
    '    '            End If
    '    '        End If

    '    '    Catch ex As Exception
    '    '        picDokterPelaksana.Visible = False
    '    '    End Try

    'End Sub
End Class