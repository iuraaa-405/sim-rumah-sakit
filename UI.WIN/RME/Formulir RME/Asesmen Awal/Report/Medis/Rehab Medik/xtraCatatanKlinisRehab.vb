Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq
Imports System.Data.SqlClient

Public Class xtraCatatanKlinisRehab
    Private Sub xtraCashIn_BeforePrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        'Try
        '    If sPictureLogo IsNot Nothing Then
        '        XrPictureBox4.Image = sPictureLogo
        '    End If

        '    Dim value1 As Object = GetCurrentColumnValue("DATE")
        '    Dim value2 As Object = GetCurrentColumnValue("KDKUNJUNGAN")
        '    Dim value3 As Object = GetCurrentColumnValue("KDDOCTOR")
        '    'Dim value4 As Object = GetCurrentColumnValue("ALAMATSIMPANGAMBAR")
        '    Dim oRehabMedik As New EMedrek.clsFisioterafi_1
        '    Dim oDoctor As New Reference.clsDoctor
        '    Dim oDAFTAR As New Admission.clsPendaftaran

        '    If value1 IsNot Nothing Then
        '        lblTANGGAL.Text = "Garut, " & CDate(value1.ToString()).ToString("dd-MM-yyyy")
        '    End If
        '    If value2 IsNot Nothing Then
        '        Dim dsKunjungan = oRehabMedik.GetDatabykodeKunjungan(value2.ToString())
        '        If dsKunjungan IsNot Nothing Then
        '            lblNIK.Text = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.KTP
        '            lblNamaPasien.Text = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY
        '            JenisKelamin.Text = IIf(dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.KDJENISKELAMIN = 1, "LAKI-LAKI", "PEREMPUAN")
        '            TanggalLahir.Text = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy")
        '            lblUsia.Text = oDAFTAR.GetUmurPasien(dsKunjungan.DATE, dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)
        '            lblAGAMA.Text = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.M_AGAMA.MEMO
        '            lblRuangan.Text = dsKunjungan.M_DEPARTMENT.NAME_DISPLAY
        '        End If
        '    End If
        '    If value3 IsNot Nothing Then
        '        Dim dsDoctor = oDoctor.GetData(value3.ToString())
        '        If dsDoctor IsNot Nothing Then
        '            lblNAMADOKTERATAS.Text = dsDoctor.NAME_DISPLAY
        '            lblNAMADOKTERDIBAWAH.Text = dsDoctor.NAME_DISPLAY

        '            If dsDoctor.KODETTD <> "" Then
        '                Dim Alamat As String = sALAMATTTD & dsDoctor.KODETTD & ".jpg"
        '                If FileIO.FileSystem.FileExists(Alamat) Then
        '                    XrPictureBox1.Image = Image.FromFile(Alamat)
        '                End If
        '            End If
        '        End If
        '    End If
        'Catch ex As Exception

        'End Try
    End Sub
    Private Function ByteArrayToImage(ByVal byteArrayIn() As Byte) As Image
        Using ms As New System.IO.MemoryStream(byteArrayIn)
            Dim returnImage = Image.FromStream(ms)
            Return returnImage
        End Using
    End Function
End Class