Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Drawing.Imaging

Public Class xtraBeritaMasukPerawatan
    Function Base64ToImage(ByVal base64string As String) As System.Drawing.Image
        'Setup image and get data stream together
        Dim img As System.Drawing.Image
        Dim MS As System.IO.MemoryStream = New System.IO.MemoryStream
        Dim b64 As String = base64string.Replace(" ", "+")
        Dim b() As Byte

        'Converts the base64 encoded msg to image data
        b = Convert.FromBase64String(b64)
        MS = New System.IO.MemoryStream(b)

        'creates image
        img = System.Drawing.Image.FromStream(MS)

        Return img
    End Function
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
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        '        lblDATE.Text = "Cimahi, " & Now.ToString("dd-MM-yyyy") & " 
        '" & vbCrLf & " a.n. Kepala Rumah Sakit Dustira
        '" & vbCrLf & "Wakil Kepala
        '" & vbCrLf & "u.b
        '" & vbCrLf & "Kaur Infokes"

        Try
            If sBase64TTe <> "" Then
                Dim sCast = Base64ToImage(sBase64TTe)

                picDokter.Image = sCast

                'lblTTE.Text = "Dokumen ini telah ditandatangani secara elektronik menggunakan sertifikat yg diterbitkan oleh PSrE IOtentik -Badan Riset dan Inovasi Nasional (BRIN)"
                ' lblKetTTE.Text = "Sartifikat Tanda Tangan Eletronik (TTE) dikeluarkan oleh PSrE iOTENTIK – BRIN / RS TK II DUSTIRA"
                lblKetTTE.Text = "Dokumen ini telah ditandatangani secara elektronik menggunakan sertifikat elektronik yang diterbitkan oleh Balai Sertifikasi Elektronik (BSrE), Badan Siber dan Sandi Negara"
                lCode.Text = "CODE: " & sTTEID
                lPin.Text = "PIN: " & sTTEPIN
            Else
                Dim downloadimagette As String = "http://172.165.115.222:86/websrv/assets/BARCODEIOTENTIK/" & sBarcodeTTE
                Dim imgtte As Image = GetImageFromURL(downloadimagette)

                picDokter.Image = imgtte
                lblKetTTE.Text = "Dokumen ini telah ditandatangani secara elektronik menggunakan sertifikat elektronik yang diterbitkan oleh Balai Sertifikasi Elektronik (BSrE), Badan Siber dan Sandi Negara"
            End If
        Catch ex As Exception

        End Try


        lblDATE.Text = "Cimahi, " & Now.ToString("dd-MM-yyyy HH:mm:ss")

        Dim oDokumen As New Setting.clsUser
        Dim dsDokumen = oDokumen.GetDataDokumen(sCODE_DOKUMEN)
        If dsDokumen IsNot Nothing Then
            lblNAMA.Text = dsDokumen.NAMA
            lblPANGKAT.Text = dsDokumen.PANGKAT
            lblNRP.Text = dsDokumen.IDENTITAS
            lblJABATAN.Text = dsDokumen.JABATAN
            lblKESATUAN.Text = dsDokumen.KESATUAN

            'If sTTE = True Then
            '    'IMAGE
            '    Try
            '        Dim sCast = ByteArrayToImage(dsDokumen.ATTACHMENT.ToArray)

            '        picGAMBAR.Image = sCast
            '    Catch oErr As Exception
            '        'MsgBox("Load Image : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            '    End Try
            'End If

        Else
            lblNAMA.Text = "-"
            lblPANGKAT.Text = "-"
            lblNRP.Text = "-"
            lblJABATAN.Text = "-"
            lblKESATUAN.Text = "-"
        End If

        'If sTTE = True Then
        '    lblTTE.Text = sDokumenTTE
        'End If

    End Sub
    Private Function ByteArrayToImage(ByVal byteArrayIn() As Byte) As Image
        Using ms As New System.IO.MemoryStream(byteArrayIn)
            Dim returnImage = Image.FromStream(ms)
            Return returnImage
        End Using
    End Function
End Class