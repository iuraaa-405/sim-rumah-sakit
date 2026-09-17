Imports QRCoder
Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class xtraReportEMedrekRI_36
    Private oResumeRawatJalan As New Digital.clsResumeRawatJalan
    Private oResumeRawatInap As New Digital.clsResumeRawatInap

    Private Sub pic1_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles pic1.BeforePrint

        If sKODEBOOKING_KODE = "RAWATJALAN" Then
            Dim oSignature As New Identitas.clsSet_Signature
            Dim dsSignature = oSignature.GetDatSignatureRM(sKODEBOOKING_RM, "LBP")
            If dsSignature IsNot Nothing Then
                If dsSignature.ALAMAT_URL <> "" Then
                    Dim DownloadIamge1 As String = "http://172.165.115.222:86/" & dsSignature.ALAMAT_URL
                    Dim img As Image = GetImageFromURL(DownloadIamge1)
                    pic1.Image = GetImageFromURL(DownloadIamge1)
                End If
            Else
                Dim gen As New QRCodeGenerator
                Dim data = gen.CreateQrCode(sKARTUBPJS, QRCodeGenerator.ECCLevel.Q)
                Dim code As New QRCode(data)
                pic1.Image = code.GetGraphic(6)
            End If
        ElseIf sKODEBOOKING_KODE = "RAWATINAP" Then
            Dim oSet_Signature As New Identitas.clsSet_Signature
            Dim dsTandaTangan = oSet_Signature.GetDatSignatureRM(sKODEBOOKING_RM, "RINGKASANKELUAR")
            If dsTandaTangan IsNot Nothing Then
                Dim DownloadIamge1 As String = "http://172.165.115.222:86/" & dsTandaTangan.ALAMAT_URL
                Dim img As Image = GetImageFromURL(DownloadIamge1)
                pic1.Image = GetImageFromURL(DownloadIamge1)
            Else
                Dim gen As New QRCodeGenerator
                Dim data = gen.CreateQrCode(sKARTUBPJS, QRCodeGenerator.ECCLevel.Q)
                Dim code As New QRCode(data)
                pic1.Image = code.GetGraphic(6)
            End If
        Else
            If sISUPLOAD = False Then
                MsgBox("Pasien Belum Tanda Tangan", MsgBoxStyle.Information, Me.Text)
            End If
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

    Private Sub pic2_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles pic2.BeforePrint
        Dim gen As New QRCodeGenerator
        Dim data = gen.CreateQrCode(DetailReport.GetCurrentColumnValue("NAMA_1"), QRCodeGenerator.ECCLevel.Q)
        Dim code As New QRCode(data)
        pic2.Image = code.GetGraphic(6)
    End Sub
End Class