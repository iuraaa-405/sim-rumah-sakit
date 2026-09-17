Imports QRCoder
Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class xtraReportLembarKontrol
    Private Sub xtraReportLembarKontroll_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles MyBase.BeforePrint
        Try
            picDokter.Image = Nothing

            If sKDDOCTOR_DPJP <> "" Then
                Dim oUser As New Setting.clsUser
                Dim dsUser = oUser.GetDataByDokter(sKDDOCTOR_DPJP)
                If dsUser IsNot Nothing Then
                    If sTextLBP <> "" Then 
                        Dim gen As New QRCodeGenerator
                        Dim data = gen.CreateQrCode(sTextLBP, QRCodeGenerator.ECCLevel.Q)
                        Dim code As New QRCode(data)
                        picDokter.Image = code.GetGraphic(6)
                    Else
                        picDokter.Image = Image.FromFile("\\172.165.115.200\Software\SCAN_TTD_DOKTER\" & sKDDOCTOR_DPJP & ".jpg")
                    End If
                End If
            End If

        Catch ex As Exception
            picDokter.Visible = False
        End Try
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

    Private Sub Detail1_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles Detail1.BeforePrint
        Try
            Dim oSignature As New Identitas.clsSet_Signature
            Dim dsSignature = oSignature.GetData(DetailReport.GetCurrentColumnValue("KDKUNJUNGAN") & "LBP")
            If dsSignature IsNot Nothing Then
                If dsSignature.ALAMAT_URL <> "" Then

                    Dim DownloadIamge1 As String = "http://172.165.115.222:86/" & dsSignature.ALAMAT_URL
                    Dim img As Image = GetImageFromURL(DownloadIamge1)
                    picTTDPASIEN.Image = img
                Else
                    If sISUPLOAD = False Then
                        MsgBox("Pasien Belum Tanda Tangan", MsgBoxStyle.Information, Me.Text)
                    End If
                End If
            Else
                If sISUPLOAD = False Then
                    MsgBox("Pasien Belum Tanda Tangan", MsgBoxStyle.Information, Me.Text)
                End If
            End If
            
        Catch ex As Exception
            picTTDPASIEN.Visible = False
        End Try

        Try
            Dim sttdokter As String = DetailReport.GetCurrentColumnValue("NMDOCTOR")
            Dim gen As New QRCodeGenerator
            Dim data = gen.CreateQrCode(sttdokter, QRCodeGenerator.ECCLevel.Q)
            Dim code As New QRCode(data)
            picTTDDOKTER.Image = code.GetGraphic(6)

        Catch ex As Exception
            picTTDDOKTER.Visible = False
        End Try

        Try
            Dim sttdterapis As String = DetailReport.GetCurrentColumnValue("NMSTAFF")
            Dim gen As New QRCodeGenerator
            Dim data = gen.CreateQrCode(sttdterapis, QRCodeGenerator.ECCLevel.Q)
            Dim code As New QRCode(data)
            picTTDTERAPIS.Image = code.GetGraphic(6)
        Catch ex As Exception
            picTTDTERAPIS.Visible = False
        End Try
    End Sub
End Class