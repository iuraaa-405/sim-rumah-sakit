Imports System.Drawing.Printing
Imports DataAccess
Imports QRCoder

Public Class xtraReportLAPORANOPERASI
    'Private oSignatureURL As New TandaTangan.clsTandaTangan
    'Dim sTTDUserSKRumkit As String

    Private Sub xtraReportLAPORANOPERASI_BeforePrint(sender As Object, e As PrintEventArgs) Handles MyBase.BeforePrint
        Try
            lblTTDTempat.Text = sTempatTTD & ", "

            If sPictureLogo IsNot Nothing Then
                XrPictureBox1.Image = sPictureLogo
            End If
        Catch ex As Exception

        End Try


        Try
            Dim value1 As Object = GetCurrentColumnValue("KDCUSTOMER")
            Dim value2 As Object = GetCurrentColumnValue("DATE")
            Dim value3 As Object = GetCurrentColumnValue("TextEdit19")

            If value1 IsNot Nothing Then
                Dim oCustomer As New Reference.clsCustomer
                Dim oRME As New RME.clsRME
                Dim Tanggal As DateTime = value2

                Dim dsCustomer = oCustomer.GetData(value1.ToString())
                If dsCustomer IsNot Nothing Then
                    txtNAMA.Text = dsCustomer.NAME_DISPLAY & " (" & IIf(dsCustomer.KDJENISKELAMIN = 1, "L", "P") & ")"
                    txtTANGGALLAHIR.Text = dsCustomer.TANGGALLAHIR.ToString("dd-MM-yyyy")
                    lblUsia.Text = oRME.GetUmurPasien(Tanggal, dsCustomer.TANGGALLAHIR)
                End If
            End If
            Try
                If value3 IsNot Nothing Then
                    Dim oDoctor As New Reference.clsDoctor
                    Dim dsDoctor = oDoctor.GetData(value3.ToString())
                    If dsDoctor IsNot Nothing Then
                        Dim Alamat As String = sALAMATTTD & dsDoctor.KODETTD & ".jpg"
                        If FileIO.FileSystem.FileExists(Alamat) Then
                            'XrPictureBox2.Image = Image.FromFile(Alamat)
                            'XrPictureBox3.Image = Image.FromFile(Alamat)
                        End If
                    End If
                End If
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try

        If menggunkanQR = False Then
            Try
                Dim oDoctor As New Reference.clsDoctor

                picTTDOperator.Visible = True
                picQROperator.Visible = False

                picTTDPembuatLaporan.Visible = True
                picQRPembuatLaporan.Visible = False


                Dim operatordokter As Object = GetCurrentColumnValue("TextEdit19")
                Dim pembuatlaporan As Object = GetCurrentColumnValue("TextEdit18")

                If operatordokter IsNot Nothing Then

                    Dim dsDoctor = oDoctor.GetData(operatordokter.ToString())
                    If dsDoctor IsNot Nothing Then
                        If dsDoctor.KODETTD <> "" Then
                            Dim Alamat As String = sALAMATTTD & dsDoctor.KODETTD & ".jpg"
                            If FileIO.FileSystem.FileExists(Alamat) Then
                                picTTDOperator.Image = Image.FromFile(Alamat)
                            End If
                        End If
                    End If
                End If

                If pembuatlaporan IsNot Nothing Then

                    Dim dsDoctor = oDoctor.GetData(pembuatlaporan.ToString())
                    If dsDoctor IsNot Nothing Then
                        If dsDoctor.KODETTD <> "" Then
                            Dim Alamat As String = sALAMATTTD & dsDoctor.KODETTD & ".jpg"
                            If FileIO.FileSystem.FileExists(Alamat) Then
                                picTTDPembuatLaporan.Image = Image.FromFile(Alamat)
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

                picTTDPembuatLaporan.Visible = False
                picQRPembuatLaporan.Visible = True

                Dim valueKDREG As Object = GetCurrentColumnValue("KDLAPORANOPERASI")

                If valueKDREG IsNot Nothing Then
                    Dim oData As New EMedrek.clsS_DIGITAL_OK_LAPORANOPERASI
                    Dim dsData = oData.GetDataKode(valueKDREG.ToString())
                    If dsData IsNot Nothing Then
                        ' Teks yang akan dimasukkan ke QR Code
                        Dim teksDokumen As String = "Nama Dokumen : LAPORAN OPERASI " & sCompany.ToUpper & vbCrLf &
                                                    "Tanggal Buat : " & dsData.DATECREATED.ToString("dd-MM-yyyy HH:mm:ss") & "" & vbCrLf &
                                                    "Tanggal Update : " & dsData.DATEUPDATED.ToString("dd-MM-yyyy HH:mm:ss") & "" & vbCrLf &
                                                    "Oleh : " & dsData.TextEdit8

                        Dim teksDokumen2 As String = "Nama Dokumen : LAPORAN OPERASI " & sCompany.ToUpper & vbCrLf &
                                                    "Tanggal Buat : " & dsData.DATECREATED.ToString("dd-MM-yyyy HH:mm:ss") & "" & vbCrLf &
                                                    "Tanggal Update : " & dsData.DATEUPDATED.ToString("dd-MM-yyyy HH:mm:ss") & "" & vbCrLf &
                                                    "Oleh : " & dsData.TextEdit10

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


                        ' Buat QR Code
                        Dim qrGenerator2 As New QRCodeGenerator()
                        Dim qrCodeData2 As QRCodeData = qrGenerator2.CreateQrCode(teksDokumen2, QRCodeGenerator.ECCLevel.Q)
                        Dim qrCode2 As New QRCode(qrCodeData2)

                        ' Hasilkan gambar (ukuran 20 pixel per modul, dengan border putih)
                        Dim qrCodeImage2 As Bitmap = qrCode2.GetGraphic(20, Color.Black, Color.White, True)

                        ' Tampilkan di XrPictureBox3 (DevExpress)
                        ' XrPictureBox3 menerima gambar dalam bentuk Image atau byte array
                        picQRPembuatLaporan.Image = qrCodeImage2

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
    Private Sub BarcodeImplan_BeforePrint(sender As Object, e As PrintEventArgs)
        'Try
        '    Dim bm As New Bitmap(sBarcodeImplan)
        '    BarcodeImplan.Image = bm

        'Catch ex As Exception
        '    BarcodeImplan.Visible = False
        'End Try
    End Sub
    Private Sub picUser_BeforePrint(sender As Object, e As Printing.PrintEventArgs)
        'Try
        '    Dim gen As New QRCodeGenerator
        '    Dim data = gen.CreateQrCode(sTTDUserSKRumkit, QRCodeGenerator.ECCLevel.Q)
        '    Dim code As New QRCode(data)
        '    picUser.Image = code.GetGraphic(6)


        'Catch ex As Exception
        'End Try
    End Sub
    Private Sub picDokter_BeforePrint(sender As Object, e As PrintEventArgs)
        'Try
        '    Dim gen As New QRCodeGenerator
        '    Dim data = gen.CreateQrCode(sTTDUserSKRumkit, QRCodeGenerator.ECCLevel.Q)
        '    Dim code As New QRCode(data)
        '    picDokter.Image = code.GetGraphic(6)
        'Catch ex As Exception

        'End Try
    End Sub
End Class