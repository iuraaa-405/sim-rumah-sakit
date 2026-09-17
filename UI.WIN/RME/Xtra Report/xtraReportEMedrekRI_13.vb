Imports DataAccess
Imports QRCoder

Public Class xtraReportEMedrekRI_13
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        If sPictureLogo IsNot Nothing Then
            XrPictureBox1.Image = sPictureLogo
        End If

        Dim sKDREG As String = String.Empty

        Dim value1 As Object = GetCurrentColumnValue("KDREG")
        Dim value2 As Object = GetCurrentColumnValue("DOCTOR_KODE")

        Try
            If value1 IsNot Nothing Then
                sKDREG = value1.ToString()
            End If

            Dim oData As New Admission.clsPendaftaran
            Dim ds = oData.GetData(sKDREG)
            If ds IsNot Nothing Then
                txtNAMA.Text = ds.M_CUSTOMER.NAME_DISPLAY
                txtTANGGALLAHIR.Text = ds.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy")
                txtJENISKELAMIN.Text = IIf(ds.M_CUSTOMER.KDJENISKELAMIN = 1, "LAKI-LAKI", "PEREMPUAN")
            End If
        Catch ex As Exception

        End Try


        If menggunkanQR = False Then
            Try
                picTTDOperator.Visible = True
                picQROperator.Visible = False

                If value2 IsNot Nothing Then
                    Dim oDoctor As New Reference.clsDoctor

                    Dim dsDoctor = oDoctor.GetData(value2.ToString())
                    If dsDoctor IsNot Nothing Then
                        If dsDoctor.KODETTD <> "" Then
                            Dim Alamat As String = sALAMATTTD & dsDoctor.KODETTD & ".jpg"
                            If FileIO.FileSystem.FileExists(Alamat) Then
                                picTTDOperator.Image = Image.FromFile(Alamat)
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception

            End Try
        Else
            Try
                picTTDOperator.Visible = False
                picQROperator.Visible = True

                Dim valueKDREG As Object = GetCurrentColumnValue("KDREG")

                If valueKDREG IsNot Nothing Then
                    Dim oData As New Transaksi.clsDigital_DischargePlanning
                    Dim dsData = oData.GetData(valueKDREG.ToString())
                    If dsData IsNot Nothing Then
                        ' Teks yang akan dimasukkan ke QR Code
                        Dim teksDokumen As String = "Nama Dokumen : ASESMEN AWAL MEDIS RAWAT INAP " & sCompany.ToUpper & vbCrLf &
                                                    "Tanggal Buat : " & dsData.DATECREATED.ToString("dd-MM-yyyy HH:mm:ss") & "" & vbCrLf &
                                                    "Tanggal Update : " & dsData.DATEUPDATED.ToString("dd-MM-yyyy HH:mm:ss") & "" & vbCrLf &
                                                    "Oleh : " & dsData.DOCTOR_NAME_DISPLAY

                        ' Buat QR Code
                        Dim qrGenerator As New QRCodeGenerator()
                        Dim qrCodeData As QRCodeData = qrGenerator.CreateQrCode(teksDokumen, QRCodeGenerator.ECCLevel.Q)
                        Dim qrCode As New QRCode(qrCodeData)

                        ' Hasilkan gambar (ukuran 20 pixel per modul, dengan border putih)
                        Dim qrCodeImage As Bitmap = qrCode.GetGraphic(20, Color.Black, Color.White, True)

                        ' Tampilkan di XrPictureBox3 (DevExpress)
                        ' XrPictureBox3 menerima gambar dalam bentuk Image atau byte array
                        picQROperator.Image = qrCodeImage

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