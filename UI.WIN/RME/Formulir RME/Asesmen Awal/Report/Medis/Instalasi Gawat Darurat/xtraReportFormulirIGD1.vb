Imports DataAccess

Public Class xtraReportFormulirIGD1
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        Try
            'lPicture2.Image = Image.FromFile(sASESEMEN_IGD)

            Try
                If sPictureLogo IsNot Nothing Then
                    XrPictureBox1.Image = sPictureLogo
                End If
            Catch ex As Exception

            End Try

            Dim sKDCUSTOMER As String = String.Empty

            Dim value1 As Object = GetCurrentColumnValue("KDCUSTOMER")

            If value1 IsNot Nothing Then
                sKDCUSTOMER = value1.ToString()
            End If

            Dim oCustomer As New Reference.clsCustomer
            Dim dsCustomer = oCustomer.GetData(sKDCUSTOMER)
            If dsCustomer IsNot Nothing Then
                Nama.Text = dsCustomer.NAME_DISPLAY
                JenisKelamin.Text = IIf(dsCustomer.KDJENISKELAMIN = 1, "LAKI-LAKI", "PEREMPUAN")
                TanggalLahir.Text = dsCustomer.TANGGALLAHIR
            End If

            lPicture2.Image = CType(My.Resources.ResourceManager.GetObject("image1"), Image)

            Dim value2 As Object = GetCurrentColumnValue("DOCTOR_KODE")
            Dim value3 As Object = GetCurrentColumnValue("PETUGASTRIAGE")

            Try
                If value2 IsNot Nothing Then
                    Dim oDoctor As New Reference.clsDoctor

                    Dim dsDoctor = oDoctor.GetData(value2.ToString())
                    If dsDoctor IsNot Nothing Then
                        If dsDoctor.KODETTD <> "" Then
                            Dim Alamat As String = sALAMATTTD & dsDoctor.KODETTD & ".jpg"
                            If FileIO.FileSystem.FileExists(Alamat) Then
                                XrPictureBox3.Image = Image.FromFile(Alamat)
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception

            End Try

            Try
                If value3 IsNot Nothing Then
                    Dim Alamat As String = sALAMATTTD & value3.ToString() & ".jpg"
                    If FileIO.FileSystem.FileExists(Alamat) Then
                        XrPictureBox4.Image = Image.FromFile(Alamat)
                    End If
                End If
            Catch ex As Exception

            End Try

        Catch ex As Exception
            'MsgBox("Load List Data Gambar tidak ditemukan dialamat : " & sASESEMEN_IGD, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function ByteArrayToImage(ByVal byteArrayIn() As Byte) As Image
        Using ms As New System.IO.MemoryStream(byteArrayIn)
            Dim returnImage = Image.FromStream(ms)
            Return returnImage
        End Using
    End Function
    'Private Sub xtraReportFormulirIGD1_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles MyBase.BeforePrint
    '    Dim ic As New ImageConverter()
    '    Dim sCast = TryCast(sAttacment_1, Byte())

    '    Dim img As Image = CType(ic.ConvertFrom(TryCast(sAttacment_1, Byte())), Image)
    '    lPicture2.Image = img
    'End Sub
End Class