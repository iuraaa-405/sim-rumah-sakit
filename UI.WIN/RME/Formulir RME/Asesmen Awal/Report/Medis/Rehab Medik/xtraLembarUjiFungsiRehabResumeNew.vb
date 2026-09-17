Imports DataAccess
Imports QRCoder

Public Class xtraLembarUjiFungsiRehabResumeNew
    Private Sub xtraCashIn_BeforePrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        Try
            Dim oDoctor As New Reference.clsDoctor

            If sPictureLogo IsNot Nothing Then
                XrPictureBox2.Image = sPictureLogo
            End If

            sJUDUL1.Text = sCompany
            sJUDUL2.Text = sPhone & " " & sNPWP
            sJUDUL3.Text = sAddress

            Dim sKDCPPT As Object = GetCurrentColumnValue("KDCPPT")

            If sKDCPPT IsNot Nothing Then
                Dim oGrouperDataCppt As New Grouper.clsR_CPPT
                Dim ds = oGrouperDataCppt.GetData(sKDCPPT)
                If ds IsNot Nothing Then
                    lblTANGGAL.Text = sTempatTTD & ", " & ds.DATE.ToString("dd-MM-yyyy")

                    If menggunkanQR = False Then
                        Try
                            picTTD.Visible = True
                            picQR.Visible = False

                            Dim dsDoctor = oDoctor.GetData(ds.A_IDENTITASPASIEN_LIST.KDDOCTOR)
                            If dsDoctor IsNot Nothing Then
                                If dsDoctor.KODETTD <> "" Then
                                    Dim Alamat As String = sALAMATTTD & dsDoctor.KODETTD & ".jpg"
                                    If FileIO.FileSystem.FileExists(Alamat) Then
                                        picTTD.Image = Image.FromFile(Alamat)
                                    End If
                                End If
                            End If
                        Catch ex As Exception

                        End Try
                    Else
                        Try
                            picTTD.Visible = False
                            picQR.Visible = True

                            ' Teks yang akan dimasukkan ke QR Code
                            Dim teksDokumen As String = "Nama Dokumen : FORMULIR REHABILITASI MEDIK " & sCompany.ToUpper & vbCrLf &
                                                        "Tanggal Buat : " & ds.DATECREATED.ToString("dd-MM-yyyy HH:mm:ss") & "" & vbCrLf &
                                                        "Tanggal Update : " & ds.DATEUPDATED.ToString("dd-MM-yyyy HH:mm:ss") & "" & vbCrLf &
                                                        "Oleh : " & ds.KDUSER

                            ' Buat QR Code
                            Dim qrGenerator As New QRCodeGenerator()
                            Dim qrCodeData As QRCodeData = qrGenerator.CreateQrCode(teksDokumen, QRCodeGenerator.ECCLevel.Q)
                            Dim qrCode As New QRCode(qrCodeData)

                            ' Hasilkan gambar (ukuran 20 pixel per modul, dengan border putih)
                            Dim qrCodeImage As Bitmap = qrCode.GetGraphic(20, Color.Black, Color.White, True)

                            ' Tampilkan di XrPictureBox3 (DevExpress)
                            ' XrPictureBox3 menerima gambar dalam bentuk Image atau byte array
                            picQR.Image = qrCodeImage

                            ' Atau jika Anda ingin menggunakan byte array (alternatif):
                            ' Using ms As New MemoryStream()
                            '     qrCodeImage.Save(ms, ImageFormat.Png)
                            '     XrPictureBox3.Image = Image.FromStream(ms)
                            ' End Using

                            'Console.WriteLine("QR Code berhasil dibuat dan ditampilkan!")
                        Catch ex As Exception
                            MessageBox.Show("Error membuat QR Code: " & ex.Message)
                        End Try
                    End If
                End If
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
End Class