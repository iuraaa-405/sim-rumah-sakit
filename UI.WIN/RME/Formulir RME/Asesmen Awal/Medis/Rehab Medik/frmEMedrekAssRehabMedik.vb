Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmEMedrekAssRehabMedik
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_RJ_ASSREHABMEDIK As New Digital.clsDigital_RJ_ASSREHABMEDIK
    Private down As Boolean = False
    Private sKODEDOKTER As String
    Private sNAMADOKTER  As String
    Private sIsOtority As Boolean = False

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String)
        oFormMode = FormMode

        Dim oPendaftaran As New Identitas.clsIdentitasPasien
        Dim dsPendaftaran = oPendaftaran.GetData(KDREG)

        txtNoRegister.Text = KDREG

        If dsPendaftaran IsNot Nothing Then
            txtNamaPasien.Text = dsPendaftaran.NAMAPASIEN
            txtNIK.Text = dsPendaftaran.NIK
            txtTmpTglLahir.Text = dsPendaftaran.TEMPATLAHIR.Trim & ", " & dsPendaftaran.TANGGALLAHIR
            txtAgama.Text = dspendaftaran.AGAMA
            txtPangkatGol.Text = dsPendaftaran.PANGKAT
            txtNRPNIP.Text = dsPendaftaran.NRP
            txtKesatuan.Text = dsPendaftaran.KESATUAN
            txtJK.Text = dsPendaftaran.JENISKELAMIN
            txtUmur.Text = dsPendaftaran.USIA

            txtNoRegister.Text = dsPendaftaran.KDKUNJUNGAN
            txtNoRM.Text = dsPendaftaran.KDCUSTOMER
            txtNoTelepon.Text = dsPendaftaran.NOMORTELEPON
            txtPendidikan.Text = dsPendaftaran.PENDIDIKAN
            txtSukuBangsa.Text = dspendaftaran.SUKU
            txtTanggalMasuk.Text = dsPendaftaran.DATE.ToString("dd-MM-yyyy")
            txtJam.Text = dsPendaftaran.DATE.ToString("HH:mm:ss")
            txtAlamat.Text = dsPendaftaran.ALAMAT
            sKODEDOKTER = dsPendaftaran.KDDOKTER
            sNAMADOKTER = dsPendaftaran.DOKTER

        Else
            txtNamaPasien.ResetText()
            txtNIK.ResetText()
            txtTmpTglLahir.ResetText()
            txtAgama.ResetText()
            txtPangkatGol.ResetText()
            txtNRPNIP.ResetText()
            txtKesatuan.ResetText()
            txtJK.ResetText()
            txtUmur.ResetText()
            txtNoRegister.ResetText()
            txtNoRM.ResetText()
            txtNoTelepon.ResetText()
            txtPendidikan.ResetText()
            txtSukuBangsa.ResetText()
            txtTanggalMasuk.ResetText()
            txtJam.ResetText()
            txtAlamat.ResetText()
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = EMedrekRJ_AssRehabMedik.TITLE
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
        sCode = txtNoRegister.Text.Trim.ToUpper
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_ChangeFormState()
        fn_Doctor()
        Select Case oFormMode
            Case FORM_MODE.FORM_MODE_VIEW
                fn_ViewMode(True)
                fn_LoadData()
            Case FORM_MODE.FORM_MODE_ADD
                fn_ViewMode(False)
                fn_EmptyMe()
            Case FORM_MODE.FORM_MODE_EDIT
                fn_ViewMode(False)
                fn_LoadData()
            Case Else
                fn_ViewMode(True)
        End Select
    End Sub
    Private Sub fn_ViewMode(ByVal Status As Boolean)
        btnSaveNew.Enabled = Not Status
        btnSaveClose.Enabled = Not Status

        chkAUTO.Properties.ReadOnly = Status
        chkALLO.Properties.ReadOnly = Status
        txtKELUHANUTAMA.Properties.ReadOnly = Status
        txtRIWAYATSEKARANG.Properties.ReadOnly = Status
        chkRIWAYATDULU_TIDAK.Properties.ReadOnly = Status
        chkRIWAYATDULU_ADA.Properties.ReadOnly = Status
        txtRIWAYATDULU.Properties.ReadOnly = Status
        chkRIWAYATALERGI_TIDAK.Properties.ReadOnly = Status
        chkRIWAYATALERGI_ADA.Properties.ReadOnly = Status
        txtRIWAYATALERGI.Properties.ReadOnly = Status
        chkSKALANYERI1.Properties.ReadOnly = Status
        chkSKALANYERI2.Properties.ReadOnly = Status
        chkSKALANYERI3.Properties.ReadOnly = Status
        chkSKALANYERI4.Properties.ReadOnly = Status
        chkRESIKOJATUH_TIDAK.Properties.ReadOnly = Status
        chkRESIKOJATUH_ADA.Properties.ReadOnly = Status
        txtRESIKOJATUH.Properties.ReadOnly = Status
        chkAFEKDANEMOSI_RENDAH.Properties.ReadOnly = Status
        chkAFEKDANEMOSI_NORMAL.Properties.ReadOnly = Status
        chkAFEKDANEMOSI_TINGGI.Properties.ReadOnly = Status
        txtAFEKDANEMOSI.Properties.ReadOnly = Status
        chkMOTIVASI_SIAP.Properties.ReadOnly = Status
        chkMOTIVASI_RAGU.Properties.ReadOnly = Status
        chkMOTIVASI_TIDAKSIAP.Properties.ReadOnly = Status
        chkMOTIVASI_TIDAKMAU.Properties.ReadOnly = Status
        txtMOTIVASI.Properties.ReadOnly = Status
        chkPENDENGARAN_TIDAK.Properties.ReadOnly = Status
        chkPENDENGARAN_ADA.Properties.ReadOnly = Status
        txtPENDENGARAN.Properties.ReadOnly = Status
        chkBICARA_TIDAK.Properties.ReadOnly = Status
        chkBICARA_ADA.Properties.ReadOnly = Status
        txtBICARA.Properties.ReadOnly = Status
        chkPENGLIHATAN_TIDAK.Properties.ReadOnly = Status
        chkPENGLIHATAN_ADA.Properties.ReadOnly = Status
        txtPENGLIHATAN.Properties.ReadOnly = Status
        chkHAMBATANKOGNITIF_TIDAK.Properties.ReadOnly = Status
        chkHAMBATANKOGNITIF_ADA.Properties.ReadOnly = Status
        txtHAMBATANKOGNITIF.Properties.ReadOnly = Status
        chkBAHASAINDO.Properties.ReadOnly = Status
        txtBAHASA.Properties.ReadOnly = Status
        txtKEYAKINAN.Properties.ReadOnly = Status
        txtKEADAANUMUM.Properties.ReadOnly = Status
        txtKESADARAN.Properties.ReadOnly = Status
        txtTEKANANDARAH.Properties.ReadOnly = Status
        txtTINGGIBADAN.Properties.ReadOnly = Status
        txtNADI.Properties.ReadOnly = Status
        txtBERATBADAN.Properties.ReadOnly = Status
        txtRESPIRASI.Properties.ReadOnly = Status
        chkSTATUSGIZI_BAIK.Properties.ReadOnly = Status
        chkSTATUSGIZI_SEDANG.Properties.ReadOnly = Status
        chkSTATUSGIZI_BURUK.Properties.ReadOnly = Status
        txtSUHU.Properties.ReadOnly = Status
        'picGAMBAR.ReadOnly = Status
        txtSTATUSLOKASI.Properties.ReadOnly = Status
        txtUJIFUNGSI1.Properties.ReadOnly = Status
        txtUJIFUNGSI2.Properties.ReadOnly = Status
        txtUJIFUNGSI3.Properties.ReadOnly = Status
        txtUJIFUNGSI4.Properties.ReadOnly = Status
        txtUJIFUNGSI5.Properties.ReadOnly = Status
        txtUJIFUNGSI6.Properties.ReadOnly = Status
        txtUJIFUNGSI7.Properties.ReadOnly = Status
        txtUJIFUNGSI8.Properties.ReadOnly = Status
        txtUJIFUNGSI9.Properties.ReadOnly = Status
        txtUJIFUNGSI10.Properties.ReadOnly = Status
        txtUJIFUNGSI11.Properties.ReadOnly = Status
        txtUJIFUNGSI12.Properties.ReadOnly = Status
        txtLABORATORIUM.Properties.ReadOnly = Status
        txtRADIOLOGI.Properties.ReadOnly = Status
        txtLAINLAIN.Properties.ReadOnly = Status
        txtDIAGNOSISKLINIS.Properties.ReadOnly = Status
        txtDIAGNOSISFUNGSI.Properties.ReadOnly = Status
        txtGOAL.Properties.ReadOnly = Status
        txtPROGRAMDANTINDAKAN.Properties.ReadOnly = Status
        txtEVALUASI.Properties.ReadOnly = Status
        grdDOKTER.Properties.ReadOnly = Status
        grdTERAPIS.Properties.ReadOnly = Status

        Dim oSetUser As New Setting.clsUser
        Dim dsUser = oSetUser.GetData(sUserID)
        If dsUser IsNot Nothing Then
            If dsUser.ISOTORTY = True Then
                lTanggal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                sIsOtority = True
            Else
                lTanggal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                sIsOtority = False
            End If
        End If
    End Sub
    Private Sub fn_EmptyMe()
        'txtCODE.Text = "<--- AUTO --->"

        chkAUTO.Checked=False
        chkALLO.Checked=False
        txtKELUHANUTAMA.ResetText()
        txtRIWAYATSEKARANG.ResetText()
        chkRIWAYATDULU_TIDAK.Checked=False
        chkRIWAYATDULU_ADA.Checked=False
        txtRIWAYATDULU.ResetText()
        chkRIWAYATALERGI_TIDAK.Checked=False
        chkRIWAYATALERGI_ADA.Checked=False
        txtRIWAYATALERGI.ResetText()
        chkSKALANYERI1.Checked=False
        chkSKALANYERI2.Checked=False
        chkSKALANYERI3.Checked=False
        chkSKALANYERI4.Checked=False
        chkRESIKOJATUH_TIDAK.Checked=False
        chkRESIKOJATUH_ADA.Checked=False
        txtRESIKOJATUH.ResetText()
        chkAFEKDANEMOSI_RENDAH.Checked=False
        chkAFEKDANEMOSI_NORMAL.Checked=False
        chkAFEKDANEMOSI_TINGGI.Checked=False
        txtAFEKDANEMOSI.ResetText()
        chkMOTIVASI_SIAP.Checked=False
        chkMOTIVASI_RAGU.Checked=False
        chkMOTIVASI_TIDAKSIAP.Checked=False
        chkMOTIVASI_TIDAKMAU.Checked=False
        txtMOTIVASI.ResetText()
        chkPENDENGARAN_TIDAK.Checked=False
        chkPENDENGARAN_ADA.Checked=False
        txtPENDENGARAN.ResetText()
        chkBICARA_TIDAK.Checked=False
        chkBICARA_ADA.Checked=False
        txtBICARA.ResetText()
        chkPENGLIHATAN_TIDAK.Checked=False
        chkPENGLIHATAN_ADA.Checked=False
        txtPENGLIHATAN.ResetText()
        chkHAMBATANKOGNITIF_TIDAK.Checked=False
        chkHAMBATANKOGNITIF_ADA.Checked=False
        txtHAMBATANKOGNITIF.ResetText()
        chkBAHASAINDO.Checked=False
        txtBAHASA.ResetText()
        txtKEYAKINAN.ResetText()
        txtKEADAANUMUM.ResetText()
        txtKESADARAN.ResetText()
        txtTEKANANDARAH.ResetText()
        txtTINGGIBADAN.ResetText()
        txtNADI.ResetText()
        txtBERATBADAN.ResetText()
        txtRESPIRASI.ResetText()
        chkSTATUSGIZI_BAIK.Checked=False
        chkSTATUSGIZI_SEDANG.Checked=False
        chkSTATUSGIZI_BURUK.Checked=False
        txtSUHU.ResetText()
        'picGAMBAR.Checked=False
        txtSTATUSLOKASI.ResetText()
        txtUJIFUNGSI1.ResetText()
        txtUJIFUNGSI2.ResetText()
        txtUJIFUNGSI3.ResetText()
        txtUJIFUNGSI4.ResetText()
        txtUJIFUNGSI5.ResetText()
        txtUJIFUNGSI6.ResetText()
        txtUJIFUNGSI7.ResetText()
        txtUJIFUNGSI8.ResetText()
        txtUJIFUNGSI9.ResetText()
        txtUJIFUNGSI10.ResetText()
        txtUJIFUNGSI11.ResetText()
        txtUJIFUNGSI12.ResetText()
        txtLABORATORIUM.ResetText()
        txtRADIOLOGI.ResetText()
        txtLAINLAIN.ResetText()
        txtDIAGNOSISKLINIS.ResetText()
        txtDIAGNOSISFUNGSI.ResetText()
        txtGOAL.ResetText()
        txtPROGRAMDANTINDAKAN.ResetText()
        txtEVALUASI.ResetText()
        deDATE.DateTime = Now
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RJ_ASSREHABMEDIK.GetData(txtNoRegister.Text)

            With ds
                chkAUTO.Checked = .SDIGITAL1
                chkALLO.Checked = .SDIGITAL2
                txtKELUHANUTAMA.Text = .SDIGITAL3
                txtRIWAYATSEKARANG.Text = .SDIGITAL4
                chkRIWAYATDULU_TIDAK.Checked = .SDIGITAL5
                chkRIWAYATDULU_ADA.Checked = .SDIGITAL6
                txtRIWAYATDULU.Text = .SDIGITAL7
                chkRIWAYATALERGI_TIDAK.Checked = .SDIGITAL8
                chkRIWAYATALERGI_ADA.Checked = .SDIGITAL9
                txtRIWAYATALERGI.Text = .SDIGITAL10
                chkSKALANYERI1.Checked = .SDIGITAL11
                chkSKALANYERI2.Checked = .SDIGITAL12
                chkSKALANYERI3.Checked = .SDIGITAL13
                chkSKALANYERI4.Checked = .SDIGITAL14
                chkRESIKOJATUH_TIDAK.Checked = .SDIGITAL15
                chkRESIKOJATUH_ADA.Checked = .SDIGITAL16
                txtRESIKOJATUH.Text = .SDIGITAL17
                chkAFEKDANEMOSI_RENDAH.Checked = .SDIGITAL18
                chkAFEKDANEMOSI_NORMAL.Checked = .SDIGITAL19
                chkAFEKDANEMOSI_TINGGI.Checked = .SDIGITAL20
                txtAFEKDANEMOSI.Text = .SDIGITAL21
                chkMOTIVASI_SIAP.Checked = .SDIGITAL22
                chkMOTIVASI_RAGU.Checked = .SDIGITAL23
                chkMOTIVASI_TIDAKSIAP.Checked = .SDIGITAL24
                chkMOTIVASI_TIDAKMAU.Checked = .SDIGITAL25
                txtMOTIVASI.Text = .SDIGITAL26
                chkPENDENGARAN_TIDAK.Checked = .SDIGITAL27
                chkPENDENGARAN_ADA.Checked = .SDIGITAL28
                txtPENDENGARAN.Text = .SDIGITAL29
                chkBICARA_TIDAK.Checked = .SDIGITAL30
                chkBICARA_ADA.Checked = .SDIGITAL31
                txtBICARA.Text = .SDIGITAL32
                chkPENGLIHATAN_TIDAK.Checked = .SDIGITAL33
                chkPENGLIHATAN_ADA.Checked = .SDIGITAL34
                txtPENGLIHATAN.Text = .SDIGITAL35
                chkHAMBATANKOGNITIF_TIDAK.Checked = .SDIGITAL36
                chkHAMBATANKOGNITIF_ADA.Checked = .SDIGITAL37
                txtHAMBATANKOGNITIF.Text = .SDIGITAL38
                chkBAHASAINDO.Checked = .SDIGITAL39
                txtBAHASA.Text = .SDIGITAL40
                txtKEYAKINAN.Text = .SDIGITAL41
                txtKEADAANUMUM.Text = .SDIGITAL42
                txtKESADARAN.Text = .SDIGITAL43
                txtTEKANANDARAH.Text = .SDIGITAL44
                txtTINGGIBADAN.Text = .SDIGITAL45
                txtNADI.Text = .SDIGITAL46
                txtBERATBADAN.Text = .SDIGITAL47
                txtRESPIRASI.Text = .SDIGITAL48
                chkSTATUSGIZI_BAIK.Checked = .SDIGITAL49
                chkSTATUSGIZI_SEDANG.Checked = .SDIGITAL50
                chkSTATUSGIZI_BURUK.Checked = .SDIGITAL51
                txtSUHU.Text = .SDIGITAL52
                'picGAMBAR.Checked = .SDIGITAL53

                Try
                    Dim img = oS_DIGITAL_RJ_ASSREHABMEDIK.GetData(txtNoRegister.Text).SDIGITAL53

                    picGAMBAR.Image = ByteArrayToImage(img.ToArray())
                Catch oErr As Exception
                     MsgBox("Load Data Gambar tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
                End Try

                txtSTATUSLOKASI.Text = .SDIGITAL54
                txtUJIFUNGSI1.Text = .SDIGITAL55
                txtUJIFUNGSI2.Text = .SDIGITAL56
                txtUJIFUNGSI3.Text = .SDIGITAL57
                txtUJIFUNGSI4.Text = .SDIGITAL58
                txtUJIFUNGSI5.Text = .SDIGITAL59
                txtUJIFUNGSI6.Text = .SDIGITAL60
                txtUJIFUNGSI7.Text = .SDIGITAL61
                txtUJIFUNGSI8.Text = .SDIGITAL62
                txtUJIFUNGSI9.Text = .SDIGITAL63
                txtUJIFUNGSI10.Text = .SDIGITAL64
                txtUJIFUNGSI11.Text = .SDIGITAL65
                txtUJIFUNGSI12.Text = .SDIGITAL66
                txtLABORATORIUM.Text = .SDIGITAL67
                txtRADIOLOGI.Text = .SDIGITAL68
                txtLAINLAIN.Text = .SDIGITAL69
                txtDIAGNOSISKLINIS.Text = .SDIGITAL70
                txtDIAGNOSISFUNGSI.Text = .SDIGITAL71
                txtGOAL.Text = .SDIGITAL72
                txtPROGRAMDANTINDAKAN.Text = .SDIGITAL73
                txtEVALUASI.Text = .SDIGITAL74
                grdDOKTER.EditValue = .KDDOKTER
                grdTERAPIS.EditValue = .KDTERAPIS

                deDATE.DateTime = .DATE
            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
  
    Private Function ByteArrayToImage(ByVal byteArrayIn() As Byte) As Image
        Using ms As New System.IO.MemoryStream(byteArrayIn)
            Dim returnImage = Image.FromStream(ms)
            Return returnImage
        End Using
    End Function
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtNoRegister.Text = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                txtNoRegister.Focus()
                fn_Validate = False
                Exit Function
            End If
            'If grdNOIDUSER.Text = String.Empty Then
            '    MsgBox("Dibutuhkan Petugas Triage", MsgBoxStyle.Exclamation, Me.Text)
            '    grdNOIDUSER.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If
        Catch oErr As Exception
            MsgBox("Validate Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RJ_ASSREHABMEDIK.GetStructureHeader
            With ds
                .KDKUNJUNGAN = txtNoRegister.Text
                Try
                    .DATECREATED = oS_DIGITAL_RJ_ASSREHABMEDIK.GetData(txtNoRegister.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .DATE = deDATE.DateTime
                
                .SDIGITAL1 = chkAUTO.Checked
                .SDIGITAL2 = chkALLO.Checked
                .SDIGITAL3 = txtKELUHANUTAMA.Text
                .SDIGITAL4 = txtRIWAYATSEKARANG.Text
                .SDIGITAL5 = chkRIWAYATDULU_TIDAK.Checked
                .SDIGITAL6 = chkRIWAYATDULU_ADA.Checked
                .SDIGITAL7 = txtRIWAYATDULU.Text
                .SDIGITAL8 = chkRIWAYATALERGI_TIDAK.Checked
                .SDIGITAL9 = chkRIWAYATALERGI_ADA.Checked
                .SDIGITAL10 = txtRIWAYATALERGI.Text
                .SDIGITAL11 = chkSKALANYERI1.Checked
                .SDIGITAL12 = chkSKALANYERI2.Checked
                .SDIGITAL13 = chkSKALANYERI3.Checked
                .SDIGITAL14 = chkSKALANYERI4.Checked
                .SDIGITAL15 = chkRESIKOJATUH_TIDAK.Checked
                .SDIGITAL16 = chkRESIKOJATUH_ADA.Checked
                .SDIGITAL17 = txtRESIKOJATUH.Text
                .SDIGITAL18 = chkAFEKDANEMOSI_RENDAH.Checked
                .SDIGITAL19 = chkAFEKDANEMOSI_NORMAL.Checked
                .SDIGITAL20 = chkAFEKDANEMOSI_TINGGI.Checked
                .SDIGITAL21 = txtAFEKDANEMOSI.Text
                .SDIGITAL22 = chkMOTIVASI_SIAP.Checked
                .SDIGITAL23 = chkMOTIVASI_RAGU.Checked
                .SDIGITAL24 = chkMOTIVASI_TIDAKSIAP.Checked
                .SDIGITAL25 = chkMOTIVASI_TIDAKMAU.Checked
                .SDIGITAL26 = txtMOTIVASI.Text
                .SDIGITAL27 = chkPENDENGARAN_TIDAK.Checked
                .SDIGITAL28 = chkPENDENGARAN_ADA.Checked
                .SDIGITAL29 = txtPENDENGARAN.Text
                .SDIGITAL30 = chkBICARA_TIDAK.Checked
                .SDIGITAL31 = chkBICARA_ADA.Checked
                .SDIGITAL32 = txtBICARA.Text
                .SDIGITAL33 = chkPENGLIHATAN_TIDAK.Checked
                .SDIGITAL34 = chkPENGLIHATAN_ADA.Checked
                .SDIGITAL35 = txtPENGLIHATAN.Text
                .SDIGITAL36 = chkHAMBATANKOGNITIF_TIDAK.Checked
                .SDIGITAL37 = chkHAMBATANKOGNITIF_ADA.Checked
                .SDIGITAL38 = txtHAMBATANKOGNITIF.Text
                .SDIGITAL39 = chkBAHASAINDO.Checked
                .SDIGITAL40 = txtBAHASA.Text
                .SDIGITAL41 = txtKEYAKINAN.Text
                .SDIGITAL42 = txtKEADAANUMUM.Text
                .SDIGITAL43 = txtKESADARAN.Text
                .SDIGITAL44 = txtTEKANANDARAH.Text
                .SDIGITAL45 = txtTINGGIBADAN.Text
                .SDIGITAL46 = txtNADI.Text
                .SDIGITAL47 = txtBERATBADAN.Text
                .SDIGITAL48 = txtRESPIRASI.Text
                .SDIGITAL49 = chkSTATUSGIZI_BAIK.Checked
                .SDIGITAL50 = chkSTATUSGIZI_SEDANG.Checked
                .SDIGITAL51 = chkSTATUSGIZI_BURUK.Checked
                .SDIGITAL52 = txtSUHU.Text
                '.SDIGITAL53 = picGAMBAR.Checked
                Try
                    Dim ms As New IO.MemoryStream()
                    picGAMBAR.Image.Save(ms, picGAMBAR.Image.RawFormat)
                    Dim data As Byte() = ms.GetBuffer()
                    .SDIGITAL53 = data
                Catch oErr As Exception
                    Try
                        .SDIGITAL53 = oS_DIGITAL_RJ_ASSREHABMEDIK.GetData(txtNoRegister.Text).SDIGITAL53
                    Catch ex As Exception
                    End Try
                End Try

                .SDIGITAL54 = txtSTATUSLOKASI.Text
                .SDIGITAL55 = txtUJIFUNGSI1.Text
                .SDIGITAL56 = txtUJIFUNGSI2.Text
                .SDIGITAL57 = txtUJIFUNGSI3.Text
                .SDIGITAL58 = txtUJIFUNGSI4.Text
                .SDIGITAL59 = txtUJIFUNGSI5.Text
                .SDIGITAL60 = txtUJIFUNGSI6.Text
                .SDIGITAL61 = txtUJIFUNGSI7.Text
                .SDIGITAL62 = txtUJIFUNGSI8.Text
                .SDIGITAL63 = txtUJIFUNGSI9.Text
                .SDIGITAL64 = txtUJIFUNGSI10.Text
                .SDIGITAL65 = txtUJIFUNGSI11.Text
                .SDIGITAL66 = txtUJIFUNGSI12.Text
                .SDIGITAL67 = txtLABORATORIUM.Text
                .SDIGITAL68 = txtRADIOLOGI.Text
                .SDIGITAL69 = txtLAINLAIN.Text
                .SDIGITAL70 = txtDIAGNOSISKLINIS.Text
                .SDIGITAL71 = txtDIAGNOSISFUNGSI.Text
                .SDIGITAL72 = txtGOAL.Text
                .SDIGITAL73 = txtPROGRAMDANTINDAKAN.Text
                .SDIGITAL74 = txtEVALUASI.Text

                .KDDOKTER = grdDOKTER.EditValue
                .NAMADOKTER = grdDOKTER.Text

                .KDTERAPIS = grdTERAPIS.EditValue
                .NAMATERAPIS = grdTERAPIS.Text

                Try
                    .CETAK = oS_DIGITAL_RJ_ASSREHABMEDIK.GetData(txtNoRegister.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                Try
                    If sIsOtority = True Then
                        .KDUSER = oS_DIGITAL_RJ_ASSREHABMEDIK.GetData(txtNoRegister.Text).KDUSER
                        .KDUSER_SIGNATURE = oS_DIGITAL_RJ_ASSREHABMEDIK.GetData(txtNoRegister.Text).KDUSER_SIGNATURE
                    Else
                        .KDUSER = sUserID
                        .KDUSER_SIGNATURE = sUserSIGNATURE
                    End If
                Catch ex As Exception
                    .KDUSER = sUserID
                    .KDUSER_SIGNATURE = sUserSIGNATURE
                End Try
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_RJ_ASSREHABMEDIK.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_RJ_ASSREHABMEDIK.UpdateData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function

    Private _Previous As System.Nullable(Of Point) = Nothing
    Private Sub pictureBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles picGAMBAR.MouseDown
        _Previous = e.Location
        pictureBox1_MouseMove(sender, e)
    End Sub
    Private Sub pictureBox1_MouseUp(sender As Object, e As MouseEventArgs) Handles picGAMBAR.MouseUp
        _Previous = Nothing
    End Sub

    Private Sub pictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles picGAMBAR.MouseMove
        If _Previous IsNot Nothing Then
            Dim GridColor As Color = Color.Red
            Dim GridPen As New Pen(GridColor)
            GridPen.Width = 2

            Using g As Graphics = Graphics.FromImage(picGAMBAR.Image)
                g.DrawLine(GridPen, _Previous.Value, e.Location)
            End Using
            picGAMBAR.Invalidate()
            _Previous = e.Location
        End If
    End Sub
    Private Sub ResetGambarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResetGambarToolStripMenuItem.Click
        picGAMBAR.Image = CType(My.Resources.ResourceManager.GetObject("IRM1"), Image)
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClose_Click()
            Case Keys.F5
                If btnReload.Enabled = True Then
                    btnReload_Click()
                End If
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
            Case Keys.F6
                If btnDiagnosa.Enabled = True Then
                    btnDiagnosa_Click()
                End If
            Case Keys.PageUp
	            fn_ScrollPage(True)
            Case Keys.PageDown
	            fn_ScrollPage(False)
        End Select
    End Sub

     Private Sub btnDiagnosa_Click() Handles btnDiagnosa.ItemClick
        Dim oMasterDiagnosa As New Diagnosa.clsMasterDiagnosa
        Dim dsMasterDiagnosa = oMasterDiagnosa.GetData(txtNoRegister.Text)
        If dsMasterDiagnosa IsNot Nothing Then
            Dim frmMasterDiagnosa As New frmMasterDiagnosa
            frmMasterDiagnosa.LoadMe(FORM_MODE.FORM_MODE_EDIT, txtNoRegister.Text)
            frmMasterDiagnosa.ShowDialog(Me)
        Else
            Dim frmMasterDiagnosa As New frmMasterDiagnosa
            frmMasterDiagnosa.LoadMe(FORM_MODE.FORM_MODE_ADD, txtNoRegister.Text)
            frmMasterDiagnosa.ShowDialog(Me)
        End If

        If sCode = "Berhasil" Then
            Dim listDiagnosa As New List(Of String)
            For Each xloop In oMasterDiagnosa.GetDataDetail(txtNoRegister.Text)
                listDiagnosa.Add(xloop.KATEGORI & " : " & xloop.REMARKS)
            Next

            Dim listProsedur As New List(Of String)
            For Each xloop In oMasterDiagnosa.GetDataDetail_(txtNoRegister.Text)
                listProsedur.Add(xloop.SEQ + 1 & ". " & xloop.REMARKS)
            Next
            'MemoEdit4.Text = String.Join(vbCrLf, listProsedur.ToArray)
            txtDIAGNOSISKLINIS.Text = String.Join(vbCrLf, listDiagnosa.ToArray)
        End If
    End Sub
    Private Sub btnReload_Click() Handles btnReload.ItemClick
        Dim dsKunjungan = oS_DIGITAL_RJ_ASSREHABMEDIK.GetDataByKunjungan(txtNoRegister.Text)

        Dim listPenunjang As New List(Of String)
        Dim listTindakanPengobatan As New List(Of String)

        If dsKunjungan IsNot Nothing Then
            Dim oOrderTindakan As New Inventory.clsOrderTindakan
            Dim oOrderLab As New Inventory.clsOrderLab
            Dim oOrderRad As New Inventory.clsOrderRad
            Dim oKonsul As New Digital.clsKonsul
            Dim oKonsulJawab As New Digital.clsJawabKonsul

            Dim dsTindakan = From x In oOrderTindakan.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
                             Join y In oOrderTindakan.GetDataDetail()
                             On x.KDORDERTINDAKAN Equals y.KDORDERTINDAKAN
                             Select TANGGAL = x.DATE, ITEM = "Tindakan : " & y.TARIFKT & " " & y.REMARKS & ", "

            Dim dsLab = From x In oOrderLab.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
                        Join y In oOrderLab.GetDataDetail()
                        On x.KDORDERLAB Equals y.KDORDERLAB
                        Select TANGGAL = x.DATE, ITEM = "Lab : " & y.TARIFKT & " " & y.REMARKS & ", "

            Dim dsRad = From x In oOrderRad.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
                        Join y In oOrderRad.GetDataDetail()
                        On x.KDORDERRAD Equals y.KDORDERRAD
                        Select TANGGAL = x.DATE, ITEM = "Rad : " & y.TARIFKT & " " & y.REMARKS & ", "

            Dim dsKonsul = From x In oKonsul.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
                           Select TANGGAL = x.DATE, ITEM = "Konsul : " & "Dari " & x.DOKTER_DARI & " Kepada " & x.DOKTER_KEPADA & " Isi " & x.MEMO & ", "

            Dim dsJawabKonsul = From x In oKonsulJawab.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
                                Select TANGGAL = x.DATE, ITEM = "Jawab Konsul : " & "Dari " & x.DOKTER_DARI & " Kepada " & x.DOKTER_KEPADA & " Isi " & x.MEMO & ", "

            Dim dsUnionPenunjang = dsLab.Union(dsRad)

            For Each xloop In dsUnionPenunjang.OrderBy(Function(x) x.TANGGAL)
                listPenunjang.Add("*) " & xloop.TANGGAL.ToString("dd-MM-yyy HH:mm:ss") & " " & xloop.ITEM)
            Next

            Dim dsUnionTindakanKonsul = dsTindakan.Union(dsKonsul).Union(dsJawabKonsul)

            For Each xloop In dsUnionTindakanKonsul.OrderBy(Function(x) x.TANGGAL)
                listTindakanPengobatan.Add("*) " & xloop.TANGGAL.ToString("dd-MM-yyy HH:mm:ss") & " " & xloop.ITEM)
            Next

            Dim oResep As New Inventory.clsOrderResep

            Dim dsResep = oResep.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)

            For Each xloop In dsResep
                For Each yloop In oResep.GetDataDetail(xloop.KDORDERRESEP)
                    listTindakanPengobatan.Add(yloop.TARIFKT & " " & yloop.SIGNA)
                Next
            Next

            txtLABORATORIUM.Text = String.Join(", ", listPenunjang.ToArray)
            txtPROGRAMDANTINDAKAN.Text = String.Join(", ", listTindakanPengobatan.ToArray)
        End If
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save asesmen " & txtNamaPasien.Text.Trim.ToUpper & vbCrLf & "Dengan User : " & sUserID & " ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtNoRegister.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_Doctor()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = "Data Source=172.165.115.150;Initial Catalog=DUSTIRA_FARMASI;Persist Security Info=True;User ID=sa;Password=dust1r@@"
            oConn = New SqlConnection(sConn)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "M_STAFF A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "
            SQL &= "AND KELOMPOKIPK = 'NAKES'"
            SQL &= "ORDER BY NAME_DISPLAY ASC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "DOKTER")

            grdDOKTER.Properties.DataSource = ds.Tables("DOKTER")
            grdDOKTER.Properties.ValueMember = "KDSTAFF"
            grdDOKTER.Properties.DisplayMember = "NAME_DISPLAY"

            grdTERAPIS.Properties.DataSource = ds.Tables("DOKTER")
            grdTERAPIS.Properties.ValueMember = "KDSTAFF"
            grdTERAPIS.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub frmEMedrekAssRehabMedik_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
	    If e.Delta > 0 Then
		    'up
		    fn_ScrollPage(True)
	    Else
		    'down
		    fn_ScrollPage(False)
	    End If
    End Sub

    Private Sub fn_ScrollPage(ByVal isUp As Boolean)
	    Dim myView As Point = Me.Panel2.AutoScrollPosition
	    Dim scrollchange As Integer = 50

	    If isUp Then
		    'up
		    myView.X = -myView.X
		    myView.y = -scrollchange - myView.Y
	    Else
		    'down
		    myView.X = -myView.X
		    myView.y = scrollchange - myView.Y
	    End If

	    Me.Panel2.AutoScrollPosition = myView
    End Sub
#End Region
End Class