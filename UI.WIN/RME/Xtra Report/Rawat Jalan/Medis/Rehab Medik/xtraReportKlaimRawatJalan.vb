Imports QRCoder
Imports DataAccess
Imports System.Text.RegularExpressions
Imports System.Data.SqlClient

Public Class xtraReportKlaimRawatJalan
    Private oResumeRawatJalan As New Digital.clsResumeRawatJalan
    Public sCountKunjungan As Integer = 0

    Private Sub xtraReportKlaimRawatJalan_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles MyBase.BeforePrint
        If sCountKunjungan Mod 5 = 0 AndAlso sCountKunjungan <> 0 Then
            lblReassesment.Visible = True
        Else
            lblReassesment.Visible = False
        End If
    End Sub

    Private Sub ReportFooter_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles ReportFooter.BeforePrint
        Try
            lNAMADOK.Text = sNAMADOKTER
            lNOSIP.Text = sNOSIPDOKTER

            picPetugas.Image = Nothing

            If sKDDOCTOR_DPJP <> "" Then
                Dim oUser As New Setting.clsUser
                Dim dsUser = oUser.GetDataByDokter(sKDDOCTOR_DPJP)
                If dsUser IsNot Nothing Then
                    If sTextLBP <> "" Then
                        Dim gen As New QRCodeGenerator
                        Dim data = gen.CreateQrCode(sTextLBP, QRCodeGenerator.ECCLevel.Q)
                        Dim code As New QRCode(data)
                        picPetugas.Image = code.GetGraphic(6)
                    Else
                        picPetugas.Image = Image.FromFile("\\172.165.115.200\Software\SCAN_TTD_DOKTER\" & sKDDOCTOR_DPJP & ".jpg")
                    End If
                End If
            End If

        Catch ex As Exception
            picPetugas.Visible = False
        End Try

        'ttd pasien
        If sKODEBOOKING_KODE = "RAWATJALAN" Then
            Dim oSignature As New Identitas.clsSet_Signature
            Dim dsSignature = oSignature.GetDatSignatureRM(sKODEBOOKING_RM, "LBP")
            If dsSignature IsNot Nothing Then
                If dsSignature.ALAMAT_URL <> "" Then
                    Dim DownloadIamge1 As String = "http://172.165.115.222:86/" & dsSignature.ALAMAT_URL
                    Dim img As Image = GetImageFromURL(DownloadIamge1)
                    picPasien.Image = GetImageFromURL(DownloadIamge1)
                End If
            Else
                Dim gen As New QRCodeGenerator
                Dim data = gen.CreateQrCode(sKARTUBPJS, QRCodeGenerator.ECCLevel.Q)
                Dim code As New QRCode(data)
                picPasien.Image = code.GetGraphic(6)
            End If
        ElseIf sKODEBOOKING_KODE = "RAWATINAP" Then
            Dim oSet_Signature As New Identitas.clsSet_Signature
            Dim dsTandaTangan = oSet_Signature.GetDatSignatureRM(sKODEBOOKING_RM, "RINGKASANKELUAR")
            If dsTandaTangan IsNot Nothing Then
                Dim DownloadIamge1 As String = "http://172.165.115.222:86/" & dsTandaTangan.ALAMAT_URL
                Dim img As Image = GetImageFromURL(DownloadIamge1)
                picPasien.Image = GetImageFromURL(DownloadIamge1)
            Else
                Dim gen As New QRCodeGenerator
                Dim data = gen.CreateQrCode(sKARTUBPJS, QRCodeGenerator.ECCLevel.Q)
                Dim code As New QRCode(data)
                picPasien.Image = code.GetGraphic(6)
            End If
        Else
            'If sISUPLOAD = False Then
            '    MsgBox("Pasien Belum Tanda Tangan", MsgBoxStyle.Information, Me.Text)
            'End If
        End If
    End Sub

    Private Function GetImageFromURL(ByVal url As String) As Image
        Dim retVal As Image = Nothing

        If Not String.IsNullOrWhiteSpace(url) Then
            Dim req As System.Net.WebRequest = System.Net.WebRequest.Create(url.Trim)

            Using request As System.Net.WebResponse = req.GetResponse
                Using stream As System.IO.Stream = request.GetResponseStream
                    retVal = New Bitmap(System.Drawing.Image.FromStream(stream))
                End Using
            End Using
        End If

        Return retVal

    End Function

    Private Sub txtAnjuranTeks_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles txtAnjuranTeks.BeforePrint
        Dim text As String = GetCurrentColumnValue("SDIGITAL73").ToString
        Dim pattern As String = "RUTIN\s+\w+\s+\d+[Xx]/\w+"
        Dim match As Match = Regex.Match(text, pattern)

        txtAnjuranTeks.Text = match.Value

    End Sub
End Class