'Imports QRCoder
Imports DataAccess
Imports System.Linq

Public Class xtraReportAsesmenKeperawatanGD_New
    Private Dim AlamatGambar As String = String.Empty

    Public Sub New()
        InitializeComponent()
    End Sub
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        If sPictureLogo IsNot Nothing Then
            XrPictureBox3.Image = sPictureLogo
        End If

        lblJudul.Text = sCompany & vbCrLf & "INSTALASI GAWAT DARURAT"

        Dim sKDREG As String = String.Empty

        Dim value1 As Object = GetCurrentColumnValue("KDPENDAFTARAN")

        If value1 IsNot Nothing Then
            sKDREG = value1.ToString()
        End If

        lblNoRegister.Text = sKDREG

        Dim oData As New Admission.clsPendaftaran
        Dim ds = oData.GetData(sKDREG)
        If ds IsNot Nothing Then
            txtNAMA.Text = ds.M_CUSTOMER.NAME_DISPLAY
            txtTANGGALLAHIR.Text = ds.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy")
            txtJENISKELAMIN.Text = IIf(ds.M_CUSTOMER.KDJENISKELAMIN = 1, "LAKI-LAKI", "PEREMPUAN")
        End If

        'Try
        '    'picGambar_1.Image = Image.FromFile("\\172.165.115.250\budijayagroup\EMEDREK")
        '    AlamatGambar = Me.GetCurrentColumnValue("SIMPANGAMBAR_1")
        '    picGambar_1.Image = Image.FromFile(AlamatGambar)
        'Catch ex As Exception
        '    MsgBox("Load List Data Gambar tidak ditemukan dialamat : " & AlamatGambar & "", MsgBoxStyle.Exclamation, Me.Text)
        'End Try

        'Dim sUserTandatangan As String = String.Empty
        'Dim UserTandatangan As Object = Me.GetCurrentColumnValue("KDUSER")

        'If UserTandatangan IsNot Nothing Then
        '    sUserTandatangan = UserTandatangan.ToString()

        '    Try
        '        Dim gen As New QRCodeGenerator
        '        Dim data = gen.CreateQrCode(sTandaTanganUser & sUserTandatangan, QRCodeGenerator.ECCLevel.Q)
        '        Dim code As New QRCode(data)
        '        picPetugas_1.Image = code.GetGraphic(6)
        '    Catch ex As Exception
        '        picPetugas_1.Visible = False
        '    End Try
        'End If
    End Sub
    Private Function ByteArrayToImage(ByVal byteArrayIn() As Byte) As Image
        Using ms As New System.IO.MemoryStream(byteArrayIn)
            Dim returnImage = Image.FromStream(ms)
            Return returnImage
        End Using
    End Function
End Class