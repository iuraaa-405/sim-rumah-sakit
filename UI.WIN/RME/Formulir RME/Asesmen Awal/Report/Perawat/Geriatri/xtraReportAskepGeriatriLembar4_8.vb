Imports DataAccess
Imports QRCoder

Public Class xtraReportAskepGeriatriLembar4_8
    Private Sub XrPictureBox2_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles XrPictureBox2.BeforePrint
        Try
            Dim gen As New QRCodeGenerator
            Dim data = gen.CreateQrCode(sTandaTanganUser, QRCodeGenerator.ECCLevel.Q)
            Dim code As New QRCode(data)
            XrPictureBox2.Image = code.GetGraphic(6)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub XrPictureBox1_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles XrPictureBox1.BeforePrint
        Try
            Dim sConn As String = String.Empty

            Dim oSetKoneksi As New Setting.clsSetKoneksi
            Dim dsSetKoneksi = oSetKoneksi.GetData()
            Dim NamaPasien As String = String.Empty

            If dsSetKoneksi IsNot Nothing Then
                sConn = dsSetKoneksi.KONEKSI
            End If

            sKDORDERRESEP_TANDATANGANPASIEN = sKDORDERRESEP_TANDATANGANPASIEN & "RINGKASANKELUAR"

            Dim oSet_Signature As New Identitas.clsSet_Signature
            Dim dsTandaTangan = oSet_Signature.GetData(sKDORDERRESEP_TANDATANGANPASIEN)

            If dsTandaTangan IsNot Nothing Then
                If dsTandaTangan.ISREAD = False Then
                    If MsgBox("Formulir Ringkasan Keluar Belum ditanda tangan pasien, Apakah akan lanjut cetak?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then
                        Me.Dispose()
                    End If

                    'lblNAMA.Text = "(                                )"
                Else
                    lblNAMA.Text = dsTandaTangan.NAMA

                    Dim DownloadIamge1 As String = "http://172.165.115.222:86/assets/images/signature_image/" & sKDORDERRESEP_TANDATANGANPASIEN & ".png"
                    Dim img As Image = GetImageFromURL(DownloadIamge1)

                    XrPictureBox2.Image = GetImageFromURL(DownloadIamge1)

                End If
            Else
                If MsgBox("Formulir Ringkasan Keluar Belum ditanda tangan pasien, Apakah akan lanjut cetak?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then
                    Me.Dispose()
                End If

                'lblNAMA.Text = "(                                )"

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function ByteArrayToImage(ByVal byteArrayIn() As Byte) As Image
        Using ms As New System.IO.MemoryStream(byteArrayIn)
            Dim returnImage = Image.FromStream(ms)
            Return returnImage
        End Using
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
End Class