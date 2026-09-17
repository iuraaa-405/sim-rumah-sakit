'Imports QRCoder
Imports DataAccess

Public Class xtraReportEMedrekRI_38
    Private sStatusFormSkriningGizi As String = "2024-07-29 00:00:00"
    Dim sDateCreated As Date = Now()

    Private Sub xtraReportEMedrekRI_38_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles MyBase.BeforePrint
        If sPictureLogo IsNot Nothing Then
            XrPictureBox1.Image = sPictureLogo
        End If

        Dim value1 As Object = GetCurrentColumnValue("KDKUNJUNGAN")
        Dim sKDPENDAFTARAN As String = String.Empty

        If value1 IsNot Nothing Then
            sKDPENDAFTARAN = value1.ToString()
        End If

        'lblJudul.Text = sJudulAsesmen

        Dim oPendaftaran As New Admission.clsPendaftaran
        Dim dsPendaftaran = oPendaftaran.GetData(sKDPENDAFTARAN)

        If dsPendaftaran IsNot Nothing Then
            lblNama.Text = dsPendaftaran.M_CUSTOMER.NAME_DISPLAY
            lblNIK.Text = dsPendaftaran.M_CUSTOMER.KTP
            lblJenisKelamin.Text = IIf(dsPendaftaran.M_CUSTOMER.KDJENISKELAMIN = 1, "LAKI-LAKI", "PEREMPUAN")
            lblUmur.Text = oPendaftaran.GetUmurPasien(dsPendaftaran.DATE, dsPendaftaran.M_CUSTOMER.TANGGALLAHIR)
            'lblAlamat.Text = dsPendaftaran.M_CUSTOMER.ALAMAT
            lblTanggalLahir.Text = dsPendaftaran.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy")
            'lblTanggalDaftar.Text = dsPendaftaran.DATE.ToString("dd-MM-yyyy")
            XrTableCell654.Text = dsPendaftaran.M_DEPARTMENT.NAME_DISPLAY
            lblAGAMA.Text = dsPendaftaran.M_CUSTOMER.M_AGAMA.MEMO
        End If

        If Report.GetCurrentColumnValue("ASESMEN_01") IsNot Nothing Then sDateCreated = Report.GetCurrentColumnValue("ASESMEN_01")

        If sDateCreated >= CDate(sStatusFormSkriningGizi) Then
            SubBand2.Visible = True
        Else
            SubBand1.Visible = True
        End If
    End Sub

    Private Sub picPerawat_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles picPerawat.BeforePrint
        'Try
        '    Dim gen As New QRCodeGenerator
        '    Dim data = gen.CreateQrCode(Report.GetCurrentColumnValue("DOKTER2_NAMEDISPLAY"), QRCodeGenerator.ECCLevel.Q)
        '    Dim code As New QRCode(data)
        '    picPerawat.Image = code.GetGraphic(6)
        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub picPasien_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles picPasien.BeforePrint
        'Try
        '    Dim gen As New QRCodeGenerator
        '    Dim data = gen.CreateQrCode(Report.GetCurrentColumnValue("PASIEN_KELUARGA"), QRCodeGenerator.ECCLevel.Q)
        '    Dim code As New QRCode(data)
        '    picPasien.Image = code.GetGraphic(6)
        'Catch ex As Exception

        'End Try
    End Sub
End Class