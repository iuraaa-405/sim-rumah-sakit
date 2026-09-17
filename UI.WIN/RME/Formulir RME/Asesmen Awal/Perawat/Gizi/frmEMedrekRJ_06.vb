Imports System.Data.SqlClient
Imports DataAccess
Imports UI.WIN.MAIN.My.Resources

Public Class frmEMedrekRJ_06
#Region "Declaration"
    'GIZI
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_RJ_06 As New Digital.clsDigital_RJ_06
    Private down As Boolean = False
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String)
        oFormMode = FormMode

        Dim oPendaftaran As New Identitas.clsIdentitasPasien
        Dim dsPendaftaran = oPendaftaran.GetData(KDREG)

        txtNoRegister.Text = KDREG

        If dsPendaftaran IsNot Nothing Then
            txtNoPasien.Text = dsPendaftaran.KDCUSTOMER
            txtNamaPasien.Text = dsPendaftaran.NAMAPASIEN.ToString.Trim.ToUpper
            txtUmur.Text = dsPendaftaran.USIA
            txtTanggalDaftar.Text = dsPendaftaran.DATE.ToString("dd-MM-yyyy HH:mm:ss")
            txtNoRegister.Text = dsPendaftaran.KDKUNJUNGAN
            txtTujuan.Text = dsPendaftaran.TUJUAN
            txtDokter.Text = dsPendaftaran.DOKTER
        Else
            txtNoPasien.ResetText()
            txtNamaPasien.ResetText()
            txtUmur.ResetText()
            txtTanggalDaftar.ResetText()
            txtNoRegister.ResetText()
            txtTujuan.ResetText()
            txtDokter.ResetText()
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = EMedrekRJ_06.TITLE
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
        fn_LoadPerawat()
        fn_LoadKDDOCTOR()
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

        deDATE.Properties.ReadOnly = Status
        txtDIAGNOSA_MEDIS_TEXT.Properties.ReadOnly = Status
        txtANTROPOMETRI_BB.Properties.ReadOnly = Status
        txtANTROPOMETRI_TB.Properties.ReadOnly = Status
        txtANTROPOMETRI_IMT.Properties.ReadOnly = Status
        txtANTROPOMETRI_LINGKAR_LENGAN.Properties.ReadOnly = Status
        txtANTROPOMETRI_BBI.Properties.ReadOnly = Status
        txtSTATUS_GIZI.Properties.ReadOnly = Status
        txtBIOKIMIA.Properties.ReadOnly = Status
        txtPEMERIKSAAN_FISIK_KLINIS_TENSI.Properties.ReadOnly = Status
        txtPEMERIKSAAN_FISIK_KLINIS_SUHU.Properties.ReadOnly = Status
        chkPEMERIKSAAN_FISIK_KLINIS_OEDEMA_01.Properties.ReadOnly = Status
        chkPEMERIKSAAN_FISIK_KLINIS_OEDEMA_02.Properties.ReadOnly = Status
        chkPEMERIKSAAN_FISIK_KLINIS_ASITES_01.Properties.ReadOnly = Status
        chkPEMERIKSAAN_FISIK_KLINIS_ASITES_02.Properties.ReadOnly = Status
        txtPEMERIKSAAN_FISIK_KLINIS_NADI.Properties.ReadOnly = Status
        txtPEMERIKSAAN_FISIK_KLINIS_RESP.Properties.ReadOnly = Status
        chkRIWAYAT_GIZI_ALERGI_MAKANAN_01.Properties.ReadOnly = Status
        txtRIWAYAT_GIZI_ALERGI_MAKANAN_01_TEXT.Properties.ReadOnly = Status
        chkRIWAYAT_GIZI_ALERGI_MAKANAN_02.Properties.ReadOnly = Status
        chkRIWAYAT_GIZI_ALERGI_OBAT_01.Properties.ReadOnly = Status
        txtRIWAYAT_GIZI_ALERGI_OBAT_01_TEXT.Properties.ReadOnly = Status
        chkRIWAYAT_GIZI_ALERGI_OBAT_02.Properties.ReadOnly = Status
        txtPOLA_MAKAN.Properties.ReadOnly = Status
        txtRIWAYAT_PERSONAL.Properties.ReadOnly = Status
        chkSKINING_RESIKO_JATUH_01_1.Properties.ReadOnly = Status
        chkSKINING_RESIKO_JATUH_01_2.Properties.ReadOnly = Status
        chkSKINING_RESIKO_JATUH_02_1.Properties.ReadOnly = Status
        chkSKINING_RESIKO_JATUH_02_2.Properties.ReadOnly = Status

        chkRESIKO_JATUH_01.Properties.ReadOnly = Status
        chkRESIKO_JATUH_02.Properties.ReadOnly = Status
        chkRESIKO_JATUH_03.Properties.ReadOnly = Status

        chkTINDAKAN_01_YA.Properties.ReadOnly = Status
        chkTINDAKAN_02_YA.Properties.ReadOnly = Status
        chkTINDAKAN_03_YA.Properties.ReadOnly = Status
        chkTINDAKAN_04_YA.Properties.ReadOnly = Status
        chkTINDAKAN_01_TIDAK.Properties.ReadOnly = Status
        chkTINDAKAN_02_TIDAK.Properties.ReadOnly = Status
        chkTINDAKAN_03_TIDAK.Properties.ReadOnly = Status
        chkTINDAKAN_04_TIDAK.Properties.ReadOnly = Status

        grdPerawat1.Properties.ReadOnly = Status
        grdPerawat2.Properties.ReadOnly = Status
        grdPerawat3.Properties.ReadOnly = Status
        grdPerawat4.Properties.ReadOnly = Status

        txtPASIENKELUARGA1.ReadOnly = Status
        txtPASIENKELUARGA2.ReadOnly = Status
        txtPASIENKELUARGA3.ReadOnly = Status
        txtPASIENKELUARGA4.ReadOnly = Status

        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_AYAH.Properties.ReadOnly = Status
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_IBU.Properties.ReadOnly = Status
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_KELUARGA.Properties.ReadOnly = Status
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_LAINNYA.Properties.ReadOnly = Status
        txtKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_LAINNYA_TEXT.Properties.ReadOnly = Status
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01_1.Properties.ReadOnly = Status
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01_2.Properties.ReadOnly = Status
        txtKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01_2_TEXT.Properties.ReadOnly = Status
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02_1.Properties.ReadOnly = Status
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02_2.Properties.ReadOnly = Status
        txtKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02_2_TEXT.Properties.ReadOnly = Status
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_1.Properties.ReadOnly = Status
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_1_1.Properties.ReadOnly = Status
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_1_2.Properties.ReadOnly = Status
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_2.Properties.ReadOnly = Status
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_2_1.Properties.ReadOnly = Status
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_2_2.Properties.ReadOnly = Status
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_3.Properties.ReadOnly = Status
        txtKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_3_TEXT.Properties.ReadOnly = Status
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04_1.Properties.ReadOnly = Status
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04_2.Properties.ReadOnly = Status
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_05_1.Properties.ReadOnly = Status
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_05_2.Properties.ReadOnly = Status
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_1.Properties.ReadOnly = Status
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_2.Properties.ReadOnly = Status
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_3.Properties.ReadOnly = Status
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_4.Properties.ReadOnly = Status
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_5.Properties.ReadOnly = Status
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_6.Properties.ReadOnly = Status
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_7.Properties.ReadOnly = Status
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_8.Properties.ReadOnly = Status
        chkPARTISIPASI_KELUARGA_1.Properties.ReadOnly = Status
        chkPARTISIPASI_KELUARGA_2.Properties.ReadOnly = Status
        chkCARA_EDUKASI_1.Properties.ReadOnly = Status
        chkCARA_EDUKASI_2.Properties.ReadOnly = Status
        chkCARA_EDUKASI_3.Properties.ReadOnly = Status
        chkCARA_EDUKASI_4.Properties.ReadOnly = Status
        chkCARA_EDUKASI_5.Properties.ReadOnly = Status
        chkCARA_EDUKASI_6.Properties.ReadOnly = Status
        chkCARA_EDUKASI_7.Properties.ReadOnly = Status
        txtCARA_EDUKASI_7_TEXT.Properties.ReadOnly = Status
        chkKEBUTUHAN_EDUKASI_1.Properties.ReadOnly = Status
        chkKEBUTUHAN_EDUKASI_2.Properties.ReadOnly = Status
        chkKEBUTUHAN_EDUKASI_3.Properties.ReadOnly = Status
        chkKEBUTUHAN_EDUKASI_4.Properties.ReadOnly = Status
        txtKEBUTUHAN_EDUKASI_4_TEXT.Properties.ReadOnly = Status
        txtDIAGNOSA_GIZI_TEXT.Properties.ReadOnly = Status
        txtINTERVENSI_GIZI_TEXT.Properties.ReadOnly = Status
        txtMONITORING_DAN_EVALUASI_TEXT.Properties.ReadOnly = Status
        chkHASIL_PENANGANAN_1.Properties.ReadOnly = Status
        chkHASIL_PENANGANAN_2.Properties.ReadOnly = Status
        chkHASIL_PENANGANAN_3.Properties.ReadOnly = Status
        chkHASIL_PENANGANAN_4.Properties.ReadOnly = Status
        chkHASIL_PENANGANAN_5.Properties.ReadOnly = Status
        txtDIFERENSIAL_DIAGNOSIS_TEXT.Properties.ReadOnly = Status
        txtDIAGNOSIS_KERJA_TEXT.Properties.ReadOnly = Status
        txtPENGOBATAN_DAN_TINDAKAN_TEXT.Properties.ReadOnly = Status
        txtREKONSILIASI_OBAT_TEXT.Properties.ReadOnly = Status
        txtDISCHARGE_PLANNING_TEXT.Properties.ReadOnly = Status
        txtDIETITIAN_TEXT.Properties.ReadOnly = Status
        grdKDDOCTOR.Properties.ReadOnly = Status
        txtRIWAYAT_GIZI.Properties.ReadOnly = Status

    End Sub
    Private Sub fn_EmptyMe()
        'txtCODE.Text = "<--- AUTO --->"

        deDATE.DateTime = Now
        txtDIAGNOSA_MEDIS_TEXT.ResetText()
        txtANTROPOMETRI_BB.ResetText()
        txtANTROPOMETRI_TB.ResetText()
        txtANTROPOMETRI_IMT.ResetText()
        txtANTROPOMETRI_LINGKAR_LENGAN.ResetText()
        txtANTROPOMETRI_BBI.ResetText()
        txtSTATUS_GIZI.ResetText()
        txtBIOKIMIA.ResetText()
        txtPEMERIKSAAN_FISIK_KLINIS_TENSI.ResetText()
        txtPEMERIKSAAN_FISIK_KLINIS_SUHU.ResetText()
        chkPEMERIKSAAN_FISIK_KLINIS_OEDEMA_01.Checked = False
        chkPEMERIKSAAN_FISIK_KLINIS_OEDEMA_02.Checked = False
        chkPEMERIKSAAN_FISIK_KLINIS_ASITES_01.Checked = False
        chkPEMERIKSAAN_FISIK_KLINIS_ASITES_02.Checked = False
        txtPEMERIKSAAN_FISIK_KLINIS_NADI.ResetText()
        txtPEMERIKSAAN_FISIK_KLINIS_RESP.ResetText()
        chkRIWAYAT_GIZI_ALERGI_MAKANAN_01.Checked = False
        txtRIWAYAT_GIZI_ALERGI_MAKANAN_01_TEXT.ResetText()
        chkRIWAYAT_GIZI_ALERGI_MAKANAN_02.Checked = False
        chkRIWAYAT_GIZI_ALERGI_OBAT_01.Checked = False
        txtRIWAYAT_GIZI_ALERGI_OBAT_01_TEXT.ResetText()
        chkRIWAYAT_GIZI_ALERGI_OBAT_02.Checked = False
        txtPOLA_MAKAN.ResetText()
        txtRIWAYAT_PERSONAL.ResetText()
        chkSKINING_RESIKO_JATUH_01_1.Checked = False
        chkSKINING_RESIKO_JATUH_01_2.Checked = False
        chkSKINING_RESIKO_JATUH_02_1.Checked = False
        chkSKINING_RESIKO_JATUH_02_2.Checked = False

        chkRESIKO_JATUH_01.Checked = False
        chkRESIKO_JATUH_02.Checked = False
        chkRESIKO_JATUH_03.Checked = False

        chkTINDAKAN_01_YA.Checked = False
        chkTINDAKAN_02_YA.Checked = False
        chkTINDAKAN_03_YA.Checked = False
        chkTINDAKAN_04_YA.Checked = False
        chkTINDAKAN_01_TIDAK.Checked = False
        chkTINDAKAN_02_TIDAK.Checked = False
        chkTINDAKAN_03_TIDAK.Checked = False
        chkTINDAKAN_04_TIDAK.Checked = False
        
        grdPerawat1.ResetText()
        grdPerawat2.ResetText()
        grdPerawat3.ResetText()
        grdPerawat4.ResetText()

        txtPASIENKELUARGA1.ResetText()
        txtPASIENKELUARGA2.ResetText()
        txtPASIENKELUARGA3.ResetText()
        txtPASIENKELUARGA4.ResetText()

        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_AYAH.Checked = False
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_IBU.Checked = False
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_KELUARGA.Checked = False
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_LAINNYA.Checked = False
        txtKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_LAINNYA_TEXT.ResetText()
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01_1.Checked = False
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01_2.Checked = False
        txtKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01_2_TEXT.ResetText()
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02_1.Checked = False
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02_2.Checked = False
        txtKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02_2_TEXT.ResetText()
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_1.Checked = False
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_1_1.Checked = False
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_1_2.Checked = False
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_2.Checked = False
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_2_1.Checked = False
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_2_2.Checked = False
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_3.Checked = False
        txtKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_3_TEXT.ResetText()
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04_1.Checked = False
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04_2.Checked = False
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_05_1.Checked = False
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_05_2.Checked = False
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_1.Checked = False
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_2.Checked = False
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_3.Checked = False
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_4.Checked = False
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_5.Checked = False
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_6.Checked = False
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_7.Checked = False
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_8.Checked = False
        chkPARTISIPASI_KELUARGA_1.Checked = False
        chkPARTISIPASI_KELUARGA_2.Checked = False
        chkCARA_EDUKASI_1.Checked = False
        chkCARA_EDUKASI_2.Checked = False
        chkCARA_EDUKASI_3.Checked = False
        chkCARA_EDUKASI_4.Checked = False
        chkCARA_EDUKASI_5.Checked = False
        chkCARA_EDUKASI_6.Checked = False
        chkCARA_EDUKASI_7.Checked = False
        txtCARA_EDUKASI_7_TEXT.ResetText()
        chkKEBUTUHAN_EDUKASI_1.Checked = False
        chkKEBUTUHAN_EDUKASI_2.Checked = False
        chkKEBUTUHAN_EDUKASI_3.Checked = False
        chkKEBUTUHAN_EDUKASI_4.Checked = False
        txtKEBUTUHAN_EDUKASI_4_TEXT.ResetText()
        txtDIAGNOSA_GIZI_TEXT.ResetText()
        txtINTERVENSI_GIZI_TEXT.ResetText()
        txtMONITORING_DAN_EVALUASI_TEXT.ResetText()
        chkHASIL_PENANGANAN_1.Checked = False
        chkHASIL_PENANGANAN_2.Checked = False
        chkHASIL_PENANGANAN_3.Checked = False
        chkHASIL_PENANGANAN_4.Checked = False
        chkHASIL_PENANGANAN_5.Checked = False
        txtDIFERENSIAL_DIAGNOSIS_TEXT.ResetText()
        txtDIAGNOSIS_KERJA_TEXT.ResetText()
        txtPENGOBATAN_DAN_TINDAKAN_TEXT.ResetText()
        txtREKONSILIASI_OBAT_TEXT.ResetText()
        txtDISCHARGE_PLANNING_TEXT.ResetText()
        txtDIETITIAN_TEXT.ResetText()
        grdKDDOCTOR.ResetText()
        txtRIWAYAT_GIZI.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RJ_06.GetData(txtNoRegister.Text)

            With ds
                deDATE.DateTime = .DATE
                txtDIAGNOSA_MEDIS_TEXT.Text = .DIAGNOSA_MEDIS_TEXT
                txtANTROPOMETRI_BB.Text = .ANTROPOMETRI_BB
                txtANTROPOMETRI_TB.Text = .ANTROPOMETRI_TB
                txtANTROPOMETRI_IMT.Text = .ANTROPOMETRI_IMT
                txtANTROPOMETRI_LINGKAR_LENGAN.Text = .ANTROPOMETRI_LINGKAR_LENGAN
                txtANTROPOMETRI_BBI.Text = .ANTROPOMETRI_BBI
                txtSTATUS_GIZI.Text = .STATUS_GIZI
                txtBIOKIMIA.Text = .BIOKIMIA
                txtPEMERIKSAAN_FISIK_KLINIS_TENSI.Text = .PEMERIKSAAN_FISIK_KLINIS_TENSI
                txtPEMERIKSAAN_FISIK_KLINIS_SUHU.Text = .PEMERIKSAAN_FISIK_KLINIS_SUHU
                chkPEMERIKSAAN_FISIK_KLINIS_OEDEMA_01.Checked = .PEMERIKSAAN_FISIK_KLINIS_OEDEMA_01
                chkPEMERIKSAAN_FISIK_KLINIS_OEDEMA_02.Checked = .PEMERIKSAAN_FISIK_KLINIS_OEDEMA_02
                chkPEMERIKSAAN_FISIK_KLINIS_ASITES_01.Checked = .PEMERIKSAAN_FISIK_KLINIS_ASITES_01
                chkPEMERIKSAAN_FISIK_KLINIS_ASITES_02.Checked = .PEMERIKSAAN_FISIK_KLINIS_ASITES_02
                txtPEMERIKSAAN_FISIK_KLINIS_NADI.Text = .PEMERIKSAAN_FISIK_KLINIS_NADI
                txtPEMERIKSAAN_FISIK_KLINIS_RESP.Text = .PEMERIKSAAN_FISIK_KLINIS_RESP
                chkRIWAYAT_GIZI_ALERGI_MAKANAN_01.Checked = .RIWAYAT_GIZI_ALERGI_MAKANAN_01
                txtRIWAYAT_GIZI_ALERGI_MAKANAN_01_TEXT.Text = .RIWAYAT_GIZI_ALERGI_MAKANAN_01_TEXT
                chkRIWAYAT_GIZI_ALERGI_MAKANAN_02.Checked = .RIWAYAT_GIZI_ALERGI_MAKANAN_02
                chkRIWAYAT_GIZI_ALERGI_OBAT_01.Checked = .RIWAYAT_GIZI_ALERGI_OBAT_01
                txtRIWAYAT_GIZI_ALERGI_OBAT_01_TEXT.Text = .RIWAYAT_GIZI_ALERGI_OBAT_01_TEXT
                chkRIWAYAT_GIZI_ALERGI_OBAT_02.Checked = .RIWAYAT_GIZI_ALERGI_OBAT_02
                txtPOLA_MAKAN.Text = .POLA_MAKAN
                txtRIWAYAT_PERSONAL.Text = .RIWAYAT_PERSONAL
                chkSKINING_RESIKO_JATUH_01_1.Checked = .SKINING_RESIKO_JATUH_01_1
                chkSKINING_RESIKO_JATUH_01_2.Checked = .SKINING_RESIKO_JATUH_01_2
                chkSKINING_RESIKO_JATUH_02_1.Checked = .SKINING_RESIKO_JATUH_02_1
                chkSKINING_RESIKO_JATUH_02_2.Checked = .SKINING_RESIKO_JATUH_02_2
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_AYAH.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_AYAH
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_IBU.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_IBU
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_KELUARGA.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_KELUARGA
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_LAINNYA.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_LAINNYA
                txtKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_LAINNYA_TEXT.Text = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_LAINNYA_TEXT
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01_1.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01_1
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01_2.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01_2
                txtKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01_2_TEXT.Text = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01_2_TEXT
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02_1.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02_1
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02_2.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02_2
                txtKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02_2_TEXT.Text = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02_2_TEXT
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_1.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_1
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_1_1.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_1_1
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_1_2.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_1_2
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_2.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_2
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_2_1.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_2_1
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_2_2.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_2_2
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_3.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_3
                txtKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_3_TEXT.Text = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_3_TEXT
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04_1.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04_1
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04_2.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04_2
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_05_1.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_05_1
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_05_2.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_05_2
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_1.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_1
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_2.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_2
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_3.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_3
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_4.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_4
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_5.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_5
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_6.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_6
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_7.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_7
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_8.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_8
                chkPARTISIPASI_KELUARGA_1.Checked = .PARTISIPASI_KELUARGA_1
                chkPARTISIPASI_KELUARGA_2.Checked = .PARTISIPASI_KELUARGA_2
                chkCARA_EDUKASI_1.Checked = .CARA_EDUKASI_1
                chkCARA_EDUKASI_2.Checked = .CARA_EDUKASI_2
                chkCARA_EDUKASI_3.Checked = .CARA_EDUKASI_3
                chkCARA_EDUKASI_4.Checked = .CARA_EDUKASI_4
                chkCARA_EDUKASI_5.Checked = .CARA_EDUKASI_5
                chkCARA_EDUKASI_6.Checked = .CARA_EDUKASI_6
                chkCARA_EDUKASI_7.Checked = .CARA_EDUKASI_7
                txtCARA_EDUKASI_7_TEXT.Text = .CARA_EDUKASI_7_TEXT
                chkKEBUTUHAN_EDUKASI_1.Checked = .KEBUTUHAN_EDUKASI_1
                chkKEBUTUHAN_EDUKASI_2.Checked = .KEBUTUHAN_EDUKASI_2
                chkKEBUTUHAN_EDUKASI_3.Checked = .KEBUTUHAN_EDUKASI_3
                chkKEBUTUHAN_EDUKASI_4.Checked = .KEBUTUHAN_EDUKASI_4
                txtKEBUTUHAN_EDUKASI_4_TEXT.Text = .KEBUTUHAN_EDUKASI_4_TEXT
                txtDIAGNOSA_GIZI_TEXT.Text = .DIAGNOSA_GIZI_TEXT
                txtINTERVENSI_GIZI_TEXT.Text = .INTERVENSI_GIZI_TEXT
                txtMONITORING_DAN_EVALUASI_TEXT.Text = .MONITORING_DAN_EVALUASI_TEXT
                chkHASIL_PENANGANAN_1.Checked = .HASIL_PENANGANAN_1
                chkHASIL_PENANGANAN_2.Checked = .HASIL_PENANGANAN_2
                chkHASIL_PENANGANAN_3.Checked = .HASIL_PENANGANAN_3
                chkHASIL_PENANGANAN_4.Checked = .HASIL_PENANGANAN_4
                chkHASIL_PENANGANAN_5.Checked = .HASIL_PENANGANAN_5
                txtDIFERENSIAL_DIAGNOSIS_TEXT.Text = .DIFERENSIAL_DIAGNOSIS_TEXT
                txtDIAGNOSIS_KERJA_TEXT.Text = .DIAGNOSIS_KERJA_TEXT
                txtPENGOBATAN_DAN_TINDAKAN_TEXT.Text = .PENGOBATAN_DAN_TINDAKAN_TEXT
                txtREKONSILIASI_OBAT_TEXT.Text = .REKONSILIASI_OBAT_TEXT
                txtDISCHARGE_PLANNING_TEXT.Text = .DISCHARGE_PLANNING_TEXT
                txtDIETITIAN_TEXT.Text = .DIETITIAN_TEXT
                grdKDDOCTOR.Text = .DOKTER_KODE
                txtRIWAYAT_GIZI.Text = .RIWAYAT_GIZI

                chkRESIKO_JATUH_01.Checked = .RESIKO_JATUH_01
                chkRESIKO_JATUH_02.Checked = .RESIKO_JATUH_02
                chkRESIKO_JATUH_03.Checked = .RESIKO_JATUH_03

                chkTINDAKAN_01_YA.Checked = .TINDAKAN_01_YA
                chkTINDAKAN_02_YA.Checked = .TINDAKAN_02_YA
                chkTINDAKAN_03_YA.Checked = .TINDAKAN_03_YA
                chkTINDAKAN_04_YA.Checked = .TINDAKAN_04_YA
                chkTINDAKAN_01_TIDAK.Checked = .TINDAKAN_01_TIDAK
                chkTINDAKAN_02_TIDAK.Checked = .TINDAKAN_02_TIDAK
                chkTINDAKAN_03_TIDAK.Checked = .TINDAKAN_03_TIDAK
                chkTINDAKAN_04_TIDAK.Checked = .TINDAKAN_04_TIDAK

                grdPerawat1.EditValue = .KDSTAFFPERAWAT1
                grdPerawat2.EditValue = .KDSTAFFPERAWAT2
                grdPerawat3.EditValue = .KDSTAFFPERAWAT3
                grdPerawat4.EditValue = .KDSTAFFPERAWAT4

                txtPASIENKELUARGA1.Text = .PASIENKELUARGA1
                txtPASIENKELUARGA2.Text = .PASIENKELUARGA2
                txtPASIENKELUARGA3.Text = .PASIENKELUARGA3
                txtPASIENKELUARGA4.Text = .PASIENKELUARGA4

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
        Catch oErr As Exception
            MsgBox("Validate Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RJ_06.GetStructureHeader
            With ds
                .KDKUNJUNGAN = txtNoRegister.Text
                Try
                    .DATECREATED = oS_DIGITAL_RJ_06.GetData(txtNoRegister.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .DATE = deDATE.DateTime
                .DIAGNOSA_MEDIS_TEXT = txtDIAGNOSA_MEDIS_TEXT.Text
                .ANTROPOMETRI_BB = txtANTROPOMETRI_BB.Text
                .ANTROPOMETRI_TB = txtANTROPOMETRI_TB.Text
                .ANTROPOMETRI_IMT = txtANTROPOMETRI_IMT.Text
                .ANTROPOMETRI_LINGKAR_LENGAN = txtANTROPOMETRI_LINGKAR_LENGAN.Text
                .ANTROPOMETRI_BBI = txtANTROPOMETRI_BBI.Text
                .STATUS_GIZI = txtSTATUS_GIZI.Text
                .BIOKIMIA = txtBIOKIMIA.Text
                .PEMERIKSAAN_FISIK_KLINIS_TENSI = txtPEMERIKSAAN_FISIK_KLINIS_TENSI.Text
                .PEMERIKSAAN_FISIK_KLINIS_SUHU = txtPEMERIKSAAN_FISIK_KLINIS_SUHU.Text
                .PEMERIKSAAN_FISIK_KLINIS_OEDEMA_01 = chkPEMERIKSAAN_FISIK_KLINIS_OEDEMA_01.Checked
                .PEMERIKSAAN_FISIK_KLINIS_OEDEMA_02 = chkPEMERIKSAAN_FISIK_KLINIS_OEDEMA_02.Checked
                .PEMERIKSAAN_FISIK_KLINIS_ASITES_01 = chkPEMERIKSAAN_FISIK_KLINIS_ASITES_01.Checked
                .PEMERIKSAAN_FISIK_KLINIS_ASITES_02 = chkPEMERIKSAAN_FISIK_KLINIS_ASITES_02.Checked
                .PEMERIKSAAN_FISIK_KLINIS_NADI = txtPEMERIKSAAN_FISIK_KLINIS_NADI.Text
                .PEMERIKSAAN_FISIK_KLINIS_RESP = txtPEMERIKSAAN_FISIK_KLINIS_RESP.Text
                .RIWAYAT_GIZI_ALERGI_MAKANAN_01 = chkRIWAYAT_GIZI_ALERGI_MAKANAN_01.Checked
                .RIWAYAT_GIZI_ALERGI_MAKANAN_01_TEXT = txtRIWAYAT_GIZI_ALERGI_MAKANAN_01_TEXT.Text
                .RIWAYAT_GIZI_ALERGI_MAKANAN_02 = chkRIWAYAT_GIZI_ALERGI_MAKANAN_02.Checked
                .RIWAYAT_GIZI_ALERGI_OBAT_01 = chkRIWAYAT_GIZI_ALERGI_OBAT_01.Checked
                .RIWAYAT_GIZI_ALERGI_OBAT_01_TEXT = txtRIWAYAT_GIZI_ALERGI_OBAT_01_TEXT.Text
                .RIWAYAT_GIZI_ALERGI_OBAT_02 = chkRIWAYAT_GIZI_ALERGI_OBAT_02.Checked
                .POLA_MAKAN = txtPOLA_MAKAN.Text
                .RIWAYAT_PERSONAL = txtRIWAYAT_PERSONAL.Text
                .SKINING_RESIKO_JATUH_01_1 = chkSKINING_RESIKO_JATUH_01_1.Checked
                .SKINING_RESIKO_JATUH_01_2 = chkSKINING_RESIKO_JATUH_01_2.Checked
                .SKINING_RESIKO_JATUH_02_1 = chkSKINING_RESIKO_JATUH_02_1.Checked
                .SKINING_RESIKO_JATUH_02_2 = chkSKINING_RESIKO_JATUH_02_2.Checked
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_AYAH = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_AYAH.Checked
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_IBU = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_IBU.Checked
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_KELUARGA = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_KELUARGA.Checked
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_LAINNYA = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_LAINNYA.Checked
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_LAINNYA_TEXT = txtKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_LAINNYA_TEXT.Text
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01_1 = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01_1.Checked
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01_2 = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01_2.Checked
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01_2_TEXT = txtKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01_2_TEXT.Text
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02_1 = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02_1.Checked
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02_2 = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02_2.Checked
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02_2_TEXT = txtKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02_2_TEXT.Text
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_1 = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_1.Checked
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_1_1 = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_1_1.Checked
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_1_2 = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_1_2.Checked
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_2 = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_2.Checked
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_2_1 = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_2_1.Checked
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_2_2 = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_2_2.Checked
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_3 = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_3.Checked
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_3_TEXT = txtKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03_3_TEXT.Text
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04_1 = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04_1.Checked
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04_2 = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04_2.Checked
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_05_1 = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_05_1.Checked
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_05_2 = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_05_2.Checked
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_1 = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_1.Checked
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_2 = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_2.Checked
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_3 = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_3.Checked
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_4 = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_4.Checked
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_5 = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_5.Checked
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_6 = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_6.Checked
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_7 = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_7.Checked
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_8 = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_06_8.Checked
                .PARTISIPASI_KELUARGA_1 = chkPARTISIPASI_KELUARGA_1.Checked
                .PARTISIPASI_KELUARGA_2 = chkPARTISIPASI_KELUARGA_2.Checked
                .CARA_EDUKASI_1 = chkCARA_EDUKASI_1.Checked
                .CARA_EDUKASI_2 = chkCARA_EDUKASI_2.Checked
                .CARA_EDUKASI_3 = chkCARA_EDUKASI_3.Checked
                .CARA_EDUKASI_4 = chkCARA_EDUKASI_4.Checked
                .CARA_EDUKASI_5 = chkCARA_EDUKASI_5.Checked
                .CARA_EDUKASI_6 = chkCARA_EDUKASI_6.Checked
                .CARA_EDUKASI_7 = chkCARA_EDUKASI_7.Checked
                .CARA_EDUKASI_7_TEXT = txtCARA_EDUKASI_7_TEXT.Text
                .KEBUTUHAN_EDUKASI_1 = chkKEBUTUHAN_EDUKASI_1.Checked
                .KEBUTUHAN_EDUKASI_2 = chkKEBUTUHAN_EDUKASI_2.Checked
                .KEBUTUHAN_EDUKASI_3 = chkKEBUTUHAN_EDUKASI_3.Checked
                .KEBUTUHAN_EDUKASI_4 = chkKEBUTUHAN_EDUKASI_4.Checked
                .KEBUTUHAN_EDUKASI_4_TEXT = txtKEBUTUHAN_EDUKASI_4_TEXT.Text
                .DIAGNOSA_GIZI_TEXT = txtDIAGNOSA_GIZI_TEXT.Text
                .INTERVENSI_GIZI_TEXT = txtINTERVENSI_GIZI_TEXT.Text
                .MONITORING_DAN_EVALUASI_TEXT = txtMONITORING_DAN_EVALUASI_TEXT.Text
                .HASIL_PENANGANAN_1 = chkHASIL_PENANGANAN_1.Checked
                .HASIL_PENANGANAN_2 = chkHASIL_PENANGANAN_2.Checked
                .HASIL_PENANGANAN_3 = chkHASIL_PENANGANAN_3.Checked
                .HASIL_PENANGANAN_4 = chkHASIL_PENANGANAN_4.Checked
                .HASIL_PENANGANAN_5 = chkHASIL_PENANGANAN_5.Checked
                .DIFERENSIAL_DIAGNOSIS_TEXT = txtDIFERENSIAL_DIAGNOSIS_TEXT.Text
                .DIAGNOSIS_KERJA_TEXT = txtDIAGNOSIS_KERJA_TEXT.Text
                .PENGOBATAN_DAN_TINDAKAN_TEXT = txtPENGOBATAN_DAN_TINDAKAN_TEXT.Text
                .REKONSILIASI_OBAT_TEXT = txtREKONSILIASI_OBAT_TEXT.Text
                .DISCHARGE_PLANNING_TEXT = txtDISCHARGE_PLANNING_TEXT.Text
                .DIETITIAN_TEXT = txtDIETITIAN_TEXT.Text
                .DOKTER_KODE = grdKDDOCTOR.EditValue
                .DOKTER_NAMEDISPLAY = grdKDDOCTOR.Text

                .RESIKO_JATUH_01 = chkRESIKO_JATUH_01.Checked
                .RESIKO_JATUH_02 = chkRESIKO_JATUH_02.Checked
                .RESIKO_JATUH_03 = chkRESIKO_JATUH_03.Checked

                .TINDAKAN_01_YA = chkTINDAKAN_01_YA.Checked
                .TINDAKAN_02_YA = chkTINDAKAN_02_YA.Checked
                .TINDAKAN_03_YA = chkTINDAKAN_03_YA.Checked
                .TINDAKAN_04_YA = chkTINDAKAN_04_YA.Checked
                .TINDAKAN_01_TIDAK = chkTINDAKAN_01_TIDAK.Checked
                .TINDAKAN_02_TIDAK = chkTINDAKAN_02_TIDAK.Checked
                .TINDAKAN_03_TIDAK = chkTINDAKAN_03_TIDAK.Checked
                .TINDAKAN_04_TIDAK = chkTINDAKAN_04_TIDAK.Checked

                .KDSTAFFPERAWAT1 = grdPerawat1.EditValue
                .NAMAPERAWAT1 = grdPerawat1.Text
                .KDSTAFFPERAWAT2 = grdPerawat2.EditValue
                .NAMAPERAWAT2 = grdPerawat2.Text
                .KDSTAFFPERAWAT3 = grdPerawat3.EditValue
                .NAMAPERAWAT3 = grdPerawat3.Text
                .KDSTAFFPERAWAT4 = grdPerawat4.EditValue
                .NAMAPERAWAT4 = grdPerawat4.Text

                .PASIENKELUARGA1 = txtPASIENKELUARGA1.Text
                .PASIENKELUARGA2 = txtPASIENKELUARGA2.Text
                .PASIENKELUARGA3 = txtPASIENKELUARGA3.Text
                .PASIENKELUARGA4 = txtPASIENKELUARGA4.Text

                Try
                    .CETAK = oS_DIGITAL_RJ_06.GetData(txtNoRegister.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                Try
                    Dim oSetUser As New Setting.clsUser
                    Dim dsUser = oSetUser.GetData(sUserID)
                    If dsUser.ISOTORTY = True Then
                        .KDUSER = oS_DIGITAL_RJ_06.GetData(txtNoRegister.Text).KDUSER
                        .KDUSER_SIGNATURE = oS_DIGITAL_RJ_06.GetData(txtNoRegister.Text).KDUSER_SIGNATURE
                    Else
                        .KDUSER = sUserID
                        .KDUSER_SIGNATURE = sUserSIGNATURE
                    End If
                Catch ex As Exception
                    .KDUSER = sUserID
                    .KDUSER_SIGNATURE = sUserSIGNATURE
                End Try


                .RIWAYAT_GIZI = txtRIWAYAT_GIZI.Text

            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_RJ_06.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_RJ_06.UpdateData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
#End Region
#Region "Command Button"
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClose_Click()
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
            Case Keys.PageUp
	            fn_ScrollPage(True)
            Case Keys.PageDown
	            fn_ScrollPage(False)
        End Select
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & txtNoRegister.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
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
    Private Sub fn_NOIDUSER()
        'Dim oUser As New Setting.clsUser
        'Try
        '    grdNOIDUSER.Properties.DataSource = oUser.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
        '    grdNOIDUSER.Properties.ValueMember = "NOIDUSER"
        '    grdNOIDUSER.Properties.DisplayMember = "NOIDUSER"
        'Catch oErr As Exception
        '    MsgBox("Load Sub Spesialis Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub
    Private Sub fn_LoadKDDOCTOR()
        Try

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String


            Dim sConn As String = ""

            Dim oSetKoneksi As New Setting.clsSetKoneksi
            Dim dsSetKoneksi = oSetKoneksi.GetData()
            If dsSetKoneksi IsNot Nothing Then
                sConn = dsSetKoneksi.KONEKSI
            End If

            oConn = New SqlConnection(sConn)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "M_DOCTOR A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "DOKTER")

            grdKDDOCTOR.Properties.DataSource = ds.Tables("DOKTER")
            grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub fn_LoadPerawat()
        Try
            Dim sKoneksiOld As String = String.Empty
            Dim oSetKoneksi As New Setting.clsSetKoneksi
            Dim dsSetKoneksi = oSetKoneksi.GetData()
            If dsSetKoneksi IsNot Nothing Then
                sKoneksiOld = dsSetKoneksi.GENERATE_ECLAIM
            End If

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = sKoneksiOld
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
            SQL &= "AND KELOMPOKIPK = 'NAKES' "
            SQL &= "ORDER BY NAME_DISPLAY ASC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "STAFF")

            grdPerawat1.Properties.DataSource = ds.Tables("STAFF")
            grdPerawat1.Properties.ValueMember = "KDSTAFF"
            grdPerawat1.Properties.DisplayMember = "NAME_DISPLAY"

            grdPerawat2.Properties.DataSource = ds.Tables("STAFF")
            grdPerawat2.Properties.ValueMember = "KDSTAFF"
            grdPerawat2.Properties.DisplayMember = "NAME_DISPLAY"

            grdPerawat3.Properties.DataSource = ds.Tables("STAFF")
            grdPerawat3.Properties.ValueMember = "KDSTAFF"
            grdPerawat3.Properties.DisplayMember = "NAME_DISPLAY"

            grdPerawat4.Properties.DataSource = ds.Tables("STAFF")
            grdPerawat4.Properties.ValueMember = "KDSTAFF"
            grdPerawat4.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub frmEMedrekRJ_06_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
	    If e.Delta > 0 Then
		    'up
		    fn_ScrollPage(True)
	    Else
		    'down
		    fn_ScrollPage(False)
	    End If
    End Sub

    Private Sub fn_ScrollPage(ByVal isUp As Boolean)
	    Dim myView As Point = Me.Panel3.AutoScrollPosition
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

	    Me.Panel3.AutoScrollPosition = myView
    End Sub
#End Region
End Class