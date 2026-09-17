Imports DataAccess
Imports QRCoder

Public Class xtraRingkasanKeluarRawatInap
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        Dim oDoctor As New Reference.clsDoctor
        Try
            If sPictureLogo IsNot Nothing Then
                XrPictureBox1.Image = sPictureLogo
            End If

            Dim value2 As Object = GetCurrentColumnValue("DOKTERKEDUA")
            Dim value3 As Object = GetCurrentColumnValue("DOKTERKETIGA")
            Dim value4 As Object = GetCurrentColumnValue("DOKTERKEEMPAT")
            Dim value5 As Object = GetCurrentColumnValue("DOKTERKELIMA")

            If value2 IsNot Nothing Then
                If value2.ToString() <> "" Then
                    Dim ds = oDoctor.GetData(value2.ToString())
                    If ds IsNot Nothing Then
                        lblDokter2.Text = ds.NAME_DISPLAY
                    End If
                End If
            End If
            If value3 IsNot Nothing Then
                If value3.ToString() <> "" Then
                    Dim ds = oDoctor.GetData(value3.ToString())
                    If ds IsNot Nothing Then
                        lblDokter3.Text = ds.NAME_DISPLAY
                    End If
                End If
            End If
            If value4 IsNot Nothing Then
                If value4.ToString() <> "" Then
                    Dim ds = oDoctor.GetData(value4.ToString())
                    If ds IsNot Nothing Then
                        lblDokter4.Text = ds.NAME_DISPLAY
                    End If
                End If
            End If
            If value5 IsNot Nothing Then
                If value5.ToString() <> "" Then
                    Dim ds = oDoctor.GetData(value5.ToString())
                    If ds IsNot Nothing Then
                        lblDokter5.Text = ds.NAME_DISPLAY
                    End If
                End If
            End If

        Catch ex As Exception
            'MsgBox("Alamat Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
        End Try

        If menggunkanQR = False Then
            Try
                picTTD.Visible = True
                picQR.Visible = False

                Dim value1 As Object = GetCurrentColumnValue("KDDOCTOR")

                If value1 IsNot Nothing Then

                    Dim dsDoctor = oDoctor.GetData(value1.ToString())
                    If dsDoctor IsNot Nothing Then
                        If dsDoctor.KODETTD <> "" Then
                            Dim Alamat As String = sALAMATTTD & dsDoctor.KODETTD & ".jpg"
                            If FileIO.FileSystem.FileExists(Alamat) Then
                                picTTD.Image = Image.FromFile(Alamat)
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception

            End Try
        Else
            Try
                picTTD.Visible = False
                picQR.Visible = True

                Dim valueKDREG As Object = GetCurrentColumnValue("KDREG")

                If valueKDREG IsNot Nothing Then
                    Dim oData As New EMedrek.clsRingkasanKeluar
                    Dim dsData = oData.GetData(valueKDREG.ToString())
                    If dsData IsNot Nothing Then
                        ' Teks yang akan dimasukkan ke QR Code
                        Dim teksDokumen As String = "Nama Dokumen : RINGKASAN KELUAR " & sCompany.ToUpper & vbCrLf &
                                                    "Tanggal Buat : " & dsData.DATECREATED.ToString("dd-MM-yyyy HH:mm:ss") & "" & vbCrLf &
                                                    "Tanggal Update : " & dsData.DATEUPDATED.ToString("dd-MM-yyyy HH:mm:ss") & "" & vbCrLf &
                                                    "Oleh : " & dsData.DOKTERUTAMA

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
                    End If
                End If



            Catch ex As Exception
                MessageBox.Show("Error membuat QR Code: " & ex.Message)
            End Try
        End If

    End Sub
End Class