Imports System.Data.SqlClient
Imports DataAccess
Imports UI.WIN.MAIN.My.Resources

Public Class frmEMedrekRJ_05
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_RJ_05 As New Digital.clsDigital_RJ_05
    Private down As Boolean = False
    Private sKoneksi As String = String.Empty
    Private sIsOtority As Boolean = False

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String)
        oFormMode = FormMode

        Dim oSetKoneksi As New Setting.clsSetKoneksi
        Dim dsSetKoneksi = oSetKoneksi.GetData()
        If dsSetKoneksi IsNot Nothing Then
            sKoneksi = dsSetKoneksi.KONEKSI
        End If

        Dim oPendaftaran As New Identitas.clsIdentitasPasien
        Dim dsPendaftaran = oPendaftaran.GetData(KDREG)

        txtNoRegister.Text = KDREG

        If dsPendaftaran IsNot Nothing Then
            txtNoPasien.Text = dsPendaftaran.KDCUSTOMER
            txtNamaPasien.Text = dsPendaftaran.NAMAPASIEN
            txtUmur.Text = dspendaftaran.USIA
            txtTanggalDaftar.Text = dsPendaftaran.DATE.ToString("dd-MM-yyyy HH:mm:ss")
            txtNoRegister.Text = dsPendaftaran.KDKUNJUNGAN
            txtTujuan.Text = dsPendaftaran.TUJUAN
            txtRUANG_KAMAR.Text = dsPendaftaran.TUJUAN
            txtDokter.Text = dsPendaftaran.DOKTER
        Else
            txtNoPasien.ResetText()
            txtNamaPasien.ResetText()
            txtUmur.ResetText()
            txtTanggalDaftar.ResetText()
            txtNoRegister.ResetText()
            txtTujuan.ResetText()
            txtRUANG_KAMAR.ResetText()
            txtDokter.ResetText()
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

        txtRUANG_KAMAR.ReadOnly = Status
        deJAM_PROSEDUR.ReadOnly = Status
        grdKDDOCTOR1.ReadOnly = Status
        grdKDDOCTOR2.ReadOnly = Status
        txtJENIS_TINDAKAN.ReadOnly = Status
        deTANGGAL_PROSEDUR.ReadOnly = Status
        txtJENIS_PROSEDUR.ReadOnly = Status
        deDATE.Properties.ReadOnly = Status
        txtDATA_SUBJEKTIF_01.Properties.ReadOnly = Status
        txtDATA_SUBJEKTIF_02.Properties.ReadOnly = Status
        txtDATA_SUBJEKTIF_03.Properties.ReadOnly = Status
        txtDATA_SUBJEKTIF_04.Properties.ReadOnly = Status
        txtDATA_SUBJEKTIF_06.Properties.ReadOnly = Status
        txtDATA_SUBJEKTIF_07.Properties.ReadOnly = Status
        chkDATA_OBJEKTIF_01_1.Properties.ReadOnly = Status
        chkDATA_OBJEKTIF_01_2.Properties.ReadOnly = Status
        chkDATA_OBJEKTIF_01_3.Properties.ReadOnly = Status
        chkDATA_OBJEKTIF_02_1.Properties.ReadOnly = Status
        chkDATA_OBJEKTIF_02_2.Properties.ReadOnly = Status
        chkDATA_OBJEKTIF_02_3.Properties.ReadOnly = Status
        chkDATA_OBJEKTIF_02_4.Properties.ReadOnly = Status
        chkDATA_OBJEKTIF_02_5.Properties.ReadOnly = Status
        txtDATA_OBJEKTIF_03_TD.Properties.ReadOnly = Status
        txtDATA_OBJEKTIF_03_P.Properties.ReadOnly = Status
        txtDATA_OBJEKTIF_03_N.Properties.ReadOnly = Status
        txtDATA_OBJEKTIF_03_S.Properties.ReadOnly = Status
        txtDATA_OBJEKTIF_03_SPO2.Properties.ReadOnly = Status
        chkDATA_OBJEKTIF_04_1_1.Properties.ReadOnly = Status
        chkDATA_OBJEKTIF_04_1_2.Properties.ReadOnly = Status
        chkDATA_OBJEKTIF_04_1_3.Properties.ReadOnly = Status
        chkDATA_OBJEKTIF_04_2_1.Properties.ReadOnly = Status
        chkDATA_OBJEKTIF_04_2_2.Properties.ReadOnly = Status
        chkDATA_OBJEKTIF_04_3_1.Properties.ReadOnly = Status
        chkDATA_OBJEKTIF_04_3_2.Properties.ReadOnly = Status
        chkRENCANA_TINDAKAN_01.Properties.ReadOnly = Status
        chkRENCANA_TINDAKAN_02.Properties.ReadOnly = Status
        chkRENCANA_TINDAKAN_03.Properties.ReadOnly = Status
        chkRENCANA_TINDAKAN_04.Properties.ReadOnly = Status
        chkRENCANA_TINDAKAN_05.Properties.ReadOnly = Status
        chkRENCANA_TINDAKAN_06.Properties.ReadOnly = Status
        chkRENCANA_TINDAKAN_07.Properties.ReadOnly = Status
        chkRENCANA_TINDAKAN_08.Properties.ReadOnly = Status
        chkRENCANA_TINDAKAN_09.Properties.ReadOnly = Status
        chkRENCANA_TINDAKAN_10.Properties.ReadOnly = Status
        chkRENCANA_TINDAKAN_11.Properties.ReadOnly = Status
        chkRENCANA_TINDAKAN_12.Properties.ReadOnly = Status
        txtRENCANA_TINDAKAN_12_TEXT.Properties.ReadOnly = Status
        chkDATA_PENUNJANG_LAB_01.Properties.ReadOnly = Status
        chkDATA_PENUNJANG_LAB_02.Properties.ReadOnly = Status
        chkDATA_PENUNJANG_LAB_03.Properties.ReadOnly = Status
        chkDATA_PENUNJANG_LAB_04.Properties.ReadOnly = Status
        chkDATA_PENUNJANG_RAD_01.Properties.ReadOnly = Status
        chkDATA_PENUNJANG_RAD_02.Properties.ReadOnly = Status
        chkDATA_PENUNJANG_RAD_03.Properties.ReadOnly = Status
        chkDATA_PENUNJANG_RAD_04.Properties.ReadOnly = Status
        chkDATA_PENUNJANG_GIZI_01.Properties.ReadOnly = Status
        chkDATA_PENUNJANG_GIZI_02.Properties.ReadOnly = Status
        chkDATA_PENUNJANG_GIZI_03.Properties.ReadOnly = Status
        chkDATA_PENUNJANG_GIZI_04.Properties.ReadOnly = Status
        chkDATA_PENUNJANG_KONSUL_01.Properties.ReadOnly = Status
        chkDATA_PENUNJANG_KONSUL_02.Properties.ReadOnly = Status
        chkDATA_PENUNJANG_KONSUL_03.Properties.ReadOnly = Status
        chkDATA_PENUNJANG_KONSUL_04.Properties.ReadOnly = Status
        txtDATA_PENUNJANG_KONSUL_01_TEXT.Properties.ReadOnly = Status
        txtDATA_PENUNJANG_KONSUL_02_TEXT.Properties.ReadOnly = Status
        txtDATA_PENUNJANG_KONSUL_03_TEXT.Properties.ReadOnly = Status
        txtDATA_PENUNJANG_KONSUL_04_TEXT.Properties.ReadOnly = Status
        chkPERSIAPAN_DILAKUKAN_01.Properties.ReadOnly = Status
        chkPERSIAPAN_DILAKUKAN_01_1.Properties.ReadOnly = Status
        chkPERSIAPAN_DILAKUKAN_01_2.Properties.ReadOnly = Status
        chkPERSIAPAN_DILAKUKAN_01_3.Properties.ReadOnly = Status
        chkPERSIAPAN_DILAKUKAN_02.Properties.ReadOnly = Status
        chkPERSIAPAN_DILAKUKAN_03.Properties.ReadOnly = Status
        chkPERSIAPAN_DILAKUKAN_04.Properties.ReadOnly = Status
        chkPERSIAPAN_DILAKUKAN_05.Properties.ReadOnly = Status
        chkPERSIAPAN_DILAKUKAN_06_1.Properties.ReadOnly = Status
        chkPERSIAPAN_DILAKUKAN_06_2.Properties.ReadOnly = Status
        chkPERSIAPAN_DILAKUKAN_06_3.Properties.ReadOnly = Status
        chkPERSIAPAN_DILAKUKAN_07.Properties.ReadOnly = Status
        chkPERSIAPAN_DILAKUKAN_08.Properties.ReadOnly = Status
        txtRESIKO_JATUH_01_TEXT.Properties.ReadOnly = Status
        txtRESIKO_JATUH_02_TEXT.Properties.ReadOnly = Status
        txtRESIKO_JATUH_03_TEXT.Properties.ReadOnly = Status
        txtRESIKO_JATUH_04_TEXT.Properties.ReadOnly = Status
        txtRESIKO_JATUH_05_TEXT.Properties.ReadOnly = Status
        txtRESIKO_JATUH_06_TEXT.Properties.ReadOnly = Status
        txtRESIKO_JATUH_JUMLAH_TEXT.Properties.ReadOnly = Status
        chkSKALANYERI_00.Properties.ReadOnly = Status
        chkSKALANYERI_01.Properties.ReadOnly = Status
        chkSKALANYERI_02.Properties.ReadOnly = Status
        chkSKALANYERI_03.Properties.ReadOnly = Status
        chkSKALANYERI_04.Properties.ReadOnly = Status
        chkSKALANYERI_05.Properties.ReadOnly = Status
        chkSKALANYERI_06.Properties.ReadOnly = Status
        chkSKALANYERI_07.Properties.ReadOnly = Status
        chkSKALANYERI_08.Properties.ReadOnly = Status
        chkSKALANYERI_09.Properties.ReadOnly = Status
        chkSKALANYERI_10.Properties.ReadOnly = Status
        txtSKALANYERI_KARAKTERISTIK.Properties.ReadOnly = Status
        txtSKALANYERI_LOKASI.Properties.ReadOnly = Status
        txtSKALANYERI_DURASI.Properties.ReadOnly = Status
        txtSKALANYERI_FREKUENSI.Properties.ReadOnly = Status
        txtRENCANA_ASUHAN_KEPERAWATAN_DIAGNOSA_KEPERAWATAN_03_TEXT.Properties.ReadOnly = Status
        txtRENCANA_ASUHAN_KEPERAWATAN_DIAGNOSA_KEPERAWATAN_06_TEXT.Properties.ReadOnly = Status
        txtRENCANA_ASUHAN_KEPERAWATAN_DIAGNOSA_KEPERAWATAN_07_TEXT.Properties.ReadOnly = Status
        chkRENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_01.Properties.ReadOnly = Status
        chkRENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_02.Properties.ReadOnly = Status
        chkRENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_03.Properties.ReadOnly = Status
        chkRENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_04.Properties.ReadOnly = Status
        chkRENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_05.Properties.ReadOnly = Status
        chkRENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_06.Properties.ReadOnly = Status
        chkRENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_07.Properties.ReadOnly = Status
        txtRENCANA_ASUHAN_KEPERAWATAN_TINDAKAN_TEXT.Properties.ReadOnly = Status
        txtRENCANA_ASUHAN_KEPERAWATAN_PERAWAT_TEXT.Properties.ReadOnly = Status
        chkPROSEDUR_PEMANTAUAN_01.Properties.ReadOnly = Status
        chkPROSEDUR_PEMANTAUAN_02.Properties.ReadOnly = Status
        chkPROSEDUR_PEMANTAUAN_03.Properties.ReadOnly = Status
        chkPROSEDUR_PEMANTAUAN_04.Properties.ReadOnly = Status
        txtPROSEDUR_PEMANTAUAN_04_TEXT.Properties.ReadOnly = Status
        chkPROSEDUR_PERSIAPAN_SEDASI_01.Properties.ReadOnly = Status
        chkPROSEDUR_PERSIAPAN_SEDASI_02.Properties.ReadOnly = Status
        chkPROSEDUR_PERSIAPAN_SEDASI_03.Properties.ReadOnly = Status
        chkPROSEDUR_PERSIAPAN_SEDASI_04.Properties.ReadOnly = Status
        chkPROSEDUR_PERSIAPAN_SEDASI_05.Properties.ReadOnly = Status
        chkPROSEDUR_PERSIAPAN_SEDASI_06.Properties.ReadOnly = Status
        txtPROSEDUR_PERSIAPAN_SEDASI_06_TEXT.Properties.ReadOnly = Status
        chkPROSEDUR_PENILAIAN_PRA_TINDAKAN_01.Properties.ReadOnly = Status
        txtPROSEDUR_PENILAIAN_PRA_TINDAKAN_01_TEXT.Properties.ReadOnly = Status
        chkPROSEDUR_PENILAIAN_PRA_TINDAKAN_02.Properties.ReadOnly = Status
        txtPROSEDUR_PENILAIAN_PRA_TINDAKAN_02_TEXT.Properties.ReadOnly = Status
        chkPROSEDUR_PENILAIAN_PRA_TINDAKAN_03.Properties.ReadOnly = Status
        txtPROSEDUR_PENILAIAN_PRA_TINDAKAN_03_TEXT.Properties.ReadOnly = Status
        chkPROSEDUR_PENILAIAN_PRA_TINDAKAN_04.Properties.ReadOnly = Status
        txtPROSEDUR_PENILAIAN_PRA_TINDAKAN_04_TEXT.Properties.ReadOnly = Status
        chkPROSEDUR_PENILAIAN_PRA_TINDAKAN_05.Properties.ReadOnly = Status
        txtPROSEDUR_PENILAIAN_PRA_TINDAKAN_05_TEXT.Properties.ReadOnly = Status
        chkPROSEDUR_PENILAIAN_PRA_TINDAKAN_06.Properties.ReadOnly = Status
        txtPROSEDUR_PENILAIAN_PRA_TINDAKAN_06_TEXT.Properties.ReadOnly = Status
        txtPROSEDUR_AKSES_INTRAVENA_TEXT.Properties.ReadOnly = Status
        chkPROSEDUR_MENGGUNAKAN_ALAT_01.Properties.ReadOnly = Status
        chkPROSEDUR_MENGGUNAKAN_ALAT_01_1.Properties.ReadOnly = Status
        chkPROSEDUR_MENGGUNAKAN_ALAT_01_2.Properties.ReadOnly = Status
        chkPROSEDUR_MENGGUNAKAN_ALAT_02.Properties.ReadOnly = Status
        chkPROSEDUR_MENGGUNAKAN_ALAT_02_1.Properties.ReadOnly = Status
        chkPROSEDUR_MENGGUNAKAN_ALAT_02_2.Properties.ReadOnly = Status
        chkPROSEDUR_MENGGUNAKAN_ALAT_03.Properties.ReadOnly = Status
        txtPROSEDUR_MENGGUNAKAN_ALAT_03_TEXT.Properties.ReadOnly = Status
        chkPROSEDUR_MENGGUNAKAN_ALAT_04.Properties.ReadOnly = Status
        txtPROSEDUR_MENGGUNAKAN_ALAT_04_TEXT.Properties.ReadOnly = Status
        chkPROSEDUR_MENGGUNAKAN_ALAT_05.Properties.ReadOnly = Status
        chkPROSEDUR_MENGGUNAKAN_ALAT_05_1.Properties.ReadOnly = Status
        chkPROSEDUR_MENGGUNAKAN_ALAT_05_2.Properties.ReadOnly = Status
        chkPROSEDUR_MENGGUNAKAN_ALAT_06.Properties.ReadOnly = Status
        chkPROSEDUR_MENGGUNAKAN_ALAT_07.Properties.ReadOnly = Status
        txtPROSEDUR_MENGGUNAKAN_ALAT_07_TEXT.Properties.ReadOnly = Status
        chkPROSEDUR_MENGGUNAKAN_ALAT_08.Properties.ReadOnly = Status
        txtPROSEDUR_MENGGUNAKAN_ALAT_08_TEXT.Properties.ReadOnly = Status
        txtRENCANA_ASUHAN_KEPERAWATAN_INTRA_DIAGNOSA_KEPERAWATAN_03_TEXT.Properties.ReadOnly = Status
        txtRENCANA_ASUHAN_KEPERAWATAN_INTRA_DIAGNOSA_KEPERAWATAN_06_TEXT.Properties.ReadOnly = Status
        txtRENCANA_ASUHAN_KEPERAWATAN_INTRA_DIAGNOSA_KEPERAWATAN_07_TEXT.Properties.ReadOnly = Status
        chkRENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_01.Properties.ReadOnly = Status
        chkRENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_02.Properties.ReadOnly = Status
        chkRENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_03.Properties.ReadOnly = Status
        chkRENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_04.Properties.ReadOnly = Status
        chkRENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_05.Properties.ReadOnly = Status
        chkRENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_06.Properties.ReadOnly = Status
        chkRENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_07.Properties.ReadOnly = Status
        txtRENCANA_ASUHAN_KEPERAWATAN_INTRA_TINDAKAN_TEXT.Properties.ReadOnly = Status
        txtRENCANA_ASUHAN_KEPERAWATAN_INTRA_PERAWAT_TEXT.Properties.ReadOnly = Status
        txtRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_DIAGNOSA_KEPERAWATAN_03_TEXT.Properties.ReadOnly = Status
        txtRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_DIAGNOSA_KEPERAWATAN_06_TEXT.Properties.ReadOnly = Status
        txtRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_DIAGNOSA_KEPERAWATAN_07_TEXT.Properties.ReadOnly = Status
        chkRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_01.Properties.ReadOnly = Status
        chkRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_02.Properties.ReadOnly = Status
        chkRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_03.Properties.ReadOnly = Status
        chkRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_04.Properties.ReadOnly = Status
        chkRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_05.Properties.ReadOnly = Status
        chkRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_06.Properties.ReadOnly = Status
        chkRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_07.Properties.ReadOnly = Status
        txtRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_TINDAKAN_TEXT.Properties.ReadOnly = Status
        txtRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_PERAWAT_TEXT.Properties.ReadOnly = Status
        chkDATA_SUBJEKTIF_05_1.Properties.ReadOnly = Status
        chkDATA_SUBJEKTIF_05_2.Properties.ReadOnly = Status
        txtDATA_PENUNJANG_LAB_03_TEXT.Properties.ReadOnly = Status
        txtDATA_PENUNJANG_LAB_04_TEXT.Properties.ReadOnly = Status
        txtDATA_PENUNJANG_RAD_04_TEXT.Properties.ReadOnly = Status
        txtDATA_PENUNJANG_GIZI_04_TEXT.Properties.ReadOnly = Status

        Dim oSetUser As New Setting.clsUser
        Dim dsUser = oSetUser.GetData(sUserID)
        If dsUser IsNot Nothing Then
            If dsUser.ISOTORTY = True Then
                sIsOtority = True
            Else
                sIsOtority = False
            End If
        End If
    End Sub
    Private Sub fn_EmptyMe()
        'txtCODE.Text = "<--- AUTO --->"
        deJAM_PROSEDUR.DateTime = Now
        grdKDDOCTOR1.ResetText()
        grdKDDOCTOR2.ResetText()
        txtJENIS_TINDAKAN.ResetText()
        deTANGGAL_PROSEDUR.DateTime = Now
        txtJENIS_PROSEDUR.ResetText()
        deDATE.DateTime = Now
        txtDATA_SUBJEKTIF_01.ResetText()
        txtDATA_SUBJEKTIF_02.ResetText()
        txtDATA_SUBJEKTIF_03.ResetText()
        txtDATA_SUBJEKTIF_04.ResetText()
        txtDATA_SUBJEKTIF_06.ResetText()
        txtDATA_SUBJEKTIF_07.ResetText()
        chkDATA_OBJEKTIF_01_1.Checked = False
        chkDATA_OBJEKTIF_01_2.Checked = False
        chkDATA_OBJEKTIF_01_3.Checked = False
        chkDATA_OBJEKTIF_02_1.Checked = False
        chkDATA_OBJEKTIF_02_2.Checked = False
        chkDATA_OBJEKTIF_02_3.Checked = False
        chkDATA_OBJEKTIF_02_4.Checked = False
        chkDATA_OBJEKTIF_02_5.Checked = False
        txtDATA_OBJEKTIF_03_TD.ResetText()
        txtDATA_OBJEKTIF_03_P.ResetText()
        txtDATA_OBJEKTIF_03_N.ResetText()
        txtDATA_OBJEKTIF_03_S.ResetText()
        txtDATA_OBJEKTIF_03_SPO2.ResetText()
        chkDATA_OBJEKTIF_04_1_1.Checked = False
        chkDATA_OBJEKTIF_04_1_2.Checked = False
        chkDATA_OBJEKTIF_04_1_3.Checked = False
        chkDATA_OBJEKTIF_04_2_1.Checked = False
        chkDATA_OBJEKTIF_04_2_2.Checked = False
        chkDATA_OBJEKTIF_04_3_1.Checked = False
        chkDATA_OBJEKTIF_04_3_2.Checked = False
        chkRENCANA_TINDAKAN_01.Checked = False
        chkRENCANA_TINDAKAN_02.Checked = False
        chkRENCANA_TINDAKAN_03.Checked = False
        chkRENCANA_TINDAKAN_04.Checked = False
        chkRENCANA_TINDAKAN_05.Checked = False
        chkRENCANA_TINDAKAN_06.Checked = False
        chkRENCANA_TINDAKAN_07.Checked = False
        chkRENCANA_TINDAKAN_08.Checked = False
        chkRENCANA_TINDAKAN_09.Checked = False
        chkRENCANA_TINDAKAN_10.Checked = False
        chkRENCANA_TINDAKAN_11.Checked = False
        chkRENCANA_TINDAKAN_12.Checked = False
        txtRENCANA_TINDAKAN_12_TEXT.ResetText()
        chkDATA_PENUNJANG_LAB_01.Checked = False
        chkDATA_PENUNJANG_LAB_02.Checked = False
        chkDATA_PENUNJANG_LAB_03.Checked = False
        chkDATA_PENUNJANG_LAB_04.Checked = False
        chkDATA_PENUNJANG_RAD_01.Checked = False
        chkDATA_PENUNJANG_RAD_02.Checked = False
        chkDATA_PENUNJANG_RAD_03.Checked = False
        chkDATA_PENUNJANG_RAD_04.Checked = False
        chkDATA_PENUNJANG_GIZI_01.Checked = False
        chkDATA_PENUNJANG_GIZI_02.Checked = False
        chkDATA_PENUNJANG_GIZI_03.Checked = False
        chkDATA_PENUNJANG_GIZI_04.Checked = False
        chkDATA_PENUNJANG_KONSUL_01.Checked = False
        chkDATA_PENUNJANG_KONSUL_02.Checked = False
        chkDATA_PENUNJANG_KONSUL_03.Checked = False
        chkDATA_PENUNJANG_KONSUL_04.Checked = False
        txtDATA_PENUNJANG_KONSUL_01_TEXT.ResetText()
        txtDATA_PENUNJANG_KONSUL_02_TEXT.ResetText()
        txtDATA_PENUNJANG_KONSUL_03_TEXT.ResetText()
        txtDATA_PENUNJANG_KONSUL_04_TEXT.ResetText()
        chkPERSIAPAN_DILAKUKAN_01.Checked = False
        chkPERSIAPAN_DILAKUKAN_01_1.Checked = False
        chkPERSIAPAN_DILAKUKAN_01_2.Checked = False
        chkPERSIAPAN_DILAKUKAN_01_3.Checked = False
        chkPERSIAPAN_DILAKUKAN_02.Checked = False
        chkPERSIAPAN_DILAKUKAN_03.Checked = False
        chkPERSIAPAN_DILAKUKAN_04.Checked = False
        chkPERSIAPAN_DILAKUKAN_05.Checked = False
        chkPERSIAPAN_DILAKUKAN_06_1.Checked = False
        chkPERSIAPAN_DILAKUKAN_06_2.Checked = False
        chkPERSIAPAN_DILAKUKAN_06_3.Checked = False
        chkPERSIAPAN_DILAKUKAN_07.Checked = False
        chkPERSIAPAN_DILAKUKAN_08.Checked = False
        txtRESIKO_JATUH_01_TEXT.ResetText()
        txtRESIKO_JATUH_02_TEXT.ResetText()
        txtRESIKO_JATUH_03_TEXT.ResetText()
        txtRESIKO_JATUH_04_TEXT.ResetText()
        txtRESIKO_JATUH_05_TEXT.ResetText()
        txtRESIKO_JATUH_06_TEXT.ResetText()
        txtRESIKO_JATUH_JUMLAH_TEXT.ResetText()
        chkSKALANYERI_00.Checked = False
        chkSKALANYERI_01.Checked = False
        chkSKALANYERI_02.Checked = False
        chkSKALANYERI_03.Checked = False
        chkSKALANYERI_04.Checked = False
        chkSKALANYERI_05.Checked = False
        chkSKALANYERI_06.Checked = False
        chkSKALANYERI_07.Checked = False
        chkSKALANYERI_08.Checked = False
        chkSKALANYERI_09.Checked = False
        chkSKALANYERI_10.Checked = False
        txtSKALANYERI_KARAKTERISTIK.ResetText()
        txtSKALANYERI_LOKASI.ResetText()
        txtSKALANYERI_DURASI.ResetText()
        txtSKALANYERI_FREKUENSI.ResetText()
        txtRENCANA_ASUHAN_KEPERAWATAN_DIAGNOSA_KEPERAWATAN_03_TEXT.ResetText()
        txtRENCANA_ASUHAN_KEPERAWATAN_DIAGNOSA_KEPERAWATAN_06_TEXT.ResetText()
        txtRENCANA_ASUHAN_KEPERAWATAN_DIAGNOSA_KEPERAWATAN_07_TEXT.ResetText()
        chkRENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_01.Checked = False
        chkRENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_02.Checked = False
        chkRENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_03.Checked = False
        chkRENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_04.Checked = False
        chkRENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_05.Checked = False
        chkRENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_06.Checked = False
        chkRENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_07.Checked = False
        txtRENCANA_ASUHAN_KEPERAWATAN_TINDAKAN_TEXT.ResetText()
        txtRENCANA_ASUHAN_KEPERAWATAN_PERAWAT_TEXT.ResetText()
        chkPROSEDUR_PEMANTAUAN_01.Checked = False
        chkPROSEDUR_PEMANTAUAN_02.Checked = False
        chkPROSEDUR_PEMANTAUAN_03.Checked = False
        chkPROSEDUR_PEMANTAUAN_04.Checked = False
        txtPROSEDUR_PEMANTAUAN_04_TEXT.ResetText()
        chkPROSEDUR_PERSIAPAN_SEDASI_01.Checked = False
        chkPROSEDUR_PERSIAPAN_SEDASI_02.Checked = False
        chkPROSEDUR_PERSIAPAN_SEDASI_03.Checked = False
        chkPROSEDUR_PERSIAPAN_SEDASI_04.Checked = False
        chkPROSEDUR_PERSIAPAN_SEDASI_05.Checked = False
        chkPROSEDUR_PERSIAPAN_SEDASI_06.Checked = False
        txtPROSEDUR_PERSIAPAN_SEDASI_06_TEXT.ResetText()
        chkPROSEDUR_PENILAIAN_PRA_TINDAKAN_01.Checked = False
        txtPROSEDUR_PENILAIAN_PRA_TINDAKAN_01_TEXT.ResetText()
        chkPROSEDUR_PENILAIAN_PRA_TINDAKAN_02.Checked = False
        txtPROSEDUR_PENILAIAN_PRA_TINDAKAN_02_TEXT.ResetText()
        chkPROSEDUR_PENILAIAN_PRA_TINDAKAN_03.Checked = False
        txtPROSEDUR_PENILAIAN_PRA_TINDAKAN_03_TEXT.ResetText()
        chkPROSEDUR_PENILAIAN_PRA_TINDAKAN_04.Checked = False
        txtPROSEDUR_PENILAIAN_PRA_TINDAKAN_04_TEXT.ResetText()
        chkPROSEDUR_PENILAIAN_PRA_TINDAKAN_05.Checked = False
        txtPROSEDUR_PENILAIAN_PRA_TINDAKAN_05_TEXT.ResetText()
        chkPROSEDUR_PENILAIAN_PRA_TINDAKAN_06.Checked = False
        txtPROSEDUR_PENILAIAN_PRA_TINDAKAN_06_TEXT.ResetText()
        txtPROSEDUR_AKSES_INTRAVENA_TEXT.ResetText()
        chkPROSEDUR_MENGGUNAKAN_ALAT_01.Checked = False
        chkPROSEDUR_MENGGUNAKAN_ALAT_01_1.Checked = False
        chkPROSEDUR_MENGGUNAKAN_ALAT_01_2.Checked = False
        chkPROSEDUR_MENGGUNAKAN_ALAT_02.Checked = False
        chkPROSEDUR_MENGGUNAKAN_ALAT_02_1.Checked = False
        chkPROSEDUR_MENGGUNAKAN_ALAT_02_2.Checked = False
        chkPROSEDUR_MENGGUNAKAN_ALAT_03.Checked = False
        txtPROSEDUR_MENGGUNAKAN_ALAT_03_TEXT.ResetText()
        chkPROSEDUR_MENGGUNAKAN_ALAT_04.Checked = False
        txtPROSEDUR_MENGGUNAKAN_ALAT_04_TEXT.ResetText()
        chkPROSEDUR_MENGGUNAKAN_ALAT_05.Checked = False
        chkPROSEDUR_MENGGUNAKAN_ALAT_05_1.Checked = False
        chkPROSEDUR_MENGGUNAKAN_ALAT_05_2.Checked = False
        chkPROSEDUR_MENGGUNAKAN_ALAT_06.Checked = False
        chkPROSEDUR_MENGGUNAKAN_ALAT_07.Checked = False
        txtPROSEDUR_MENGGUNAKAN_ALAT_07_TEXT.ResetText()
        chkPROSEDUR_MENGGUNAKAN_ALAT_08.Checked = False
        txtPROSEDUR_MENGGUNAKAN_ALAT_08_TEXT.ResetText()
        txtRENCANA_ASUHAN_KEPERAWATAN_INTRA_DIAGNOSA_KEPERAWATAN_03_TEXT.ResetText()
        txtRENCANA_ASUHAN_KEPERAWATAN_INTRA_DIAGNOSA_KEPERAWATAN_06_TEXT.ResetText()
        txtRENCANA_ASUHAN_KEPERAWATAN_INTRA_DIAGNOSA_KEPERAWATAN_07_TEXT.ResetText()
        chkRENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_01.Checked = False
        chkRENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_02.Checked = False
        chkRENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_03.Checked = False
        chkRENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_04.Checked = False
        chkRENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_05.Checked = False
        chkRENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_06.Checked = False
        chkRENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_07.Checked = False
        txtRENCANA_ASUHAN_KEPERAWATAN_INTRA_TINDAKAN_TEXT.ResetText()
        txtRENCANA_ASUHAN_KEPERAWATAN_INTRA_PERAWAT_TEXT.ResetText()
        txtRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_DIAGNOSA_KEPERAWATAN_03_TEXT.ResetText()
        txtRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_DIAGNOSA_KEPERAWATAN_06_TEXT.ResetText()
        txtRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_DIAGNOSA_KEPERAWATAN_07_TEXT.ResetText()
        chkRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_01.Checked = False
        chkRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_02.Checked = False
        chkRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_03.Checked = False
        chkRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_04.Checked = False
        chkRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_05.Checked = False
        chkRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_06.Checked = False
        chkRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_07.Checked = False
        txtRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_TINDAKAN_TEXT.ResetText()
        txtRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_PERAWAT_TEXT.ResetText()
        chkDATA_SUBJEKTIF_05_1.Checked = False
        chkDATA_SUBJEKTIF_05_2.Checked = False
        txtDATA_PENUNJANG_LAB_03_TEXT.ResetText()
        txtDATA_PENUNJANG_LAB_04_TEXT.ResetText()
        txtDATA_PENUNJANG_RAD_04_TEXT.ResetText()
        txtDATA_PENUNJANG_GIZI_04_TEXT.ResetText()

    End Sub

    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RJ_05.GetData(txtNoRegister.Text)

            With ds
                txtRUANG_KAMAR.Text = .RUANG_KAMAR
                deJAM_PROSEDUR.DateTime = .JAM_PROSEDUR
                grdKDDOCTOR1.Text = .DOKTER_1_KODE
                grdKDDOCTOR2.Text = .DOKTER_2_KODE
                txtJENIS_TINDAKAN.Text = .JENIS_TINDAKAN
                deTANGGAL_PROSEDUR.DateTime = .TANGGAL_PROSEDUR
                txtJENIS_PROSEDUR.Text = .JENIS_PROSEDUR
                deDATE.DateTime = .DATE
                txtDATA_SUBJEKTIF_01.Text = .DATA_SUBJEKTIF_01
                txtDATA_SUBJEKTIF_02.Text = .DATA_SUBJEKTIF_02
                txtDATA_SUBJEKTIF_03.Text = .DATA_SUBJEKTIF_03
                txtDATA_SUBJEKTIF_04.Text = .DATA_SUBJEKTIF_04
                txtDATA_SUBJEKTIF_06.Text = .DATA_SUBJEKTIF_06
                txtDATA_SUBJEKTIF_07.Text = .DATA_SUBJEKTIF_07
                chkDATA_OBJEKTIF_01_1.Checked = .DATA_OBJEKTIF_01_1
                chkDATA_OBJEKTIF_01_2.Checked = .DATA_OBJEKTIF_01_2
                chkDATA_OBJEKTIF_01_3.Checked = .DATA_OBJEKTIF_01_3
                chkDATA_OBJEKTIF_02_1.Checked = .DATA_OBJEKTIF_02_1
                chkDATA_OBJEKTIF_02_2.Checked = .DATA_OBJEKTIF_02_2
                chkDATA_OBJEKTIF_02_3.Checked = .DATA_OBJEKTIF_02_3
                chkDATA_OBJEKTIF_02_4.Checked = .DATA_OBJEKTIF_02_4
                chkDATA_OBJEKTIF_02_5.Checked = .DATA_OBJEKTIF_02_5
                txtDATA_OBJEKTIF_03_TD.Text = .DATA_OBJEKTIF_03_TD
                txtDATA_OBJEKTIF_03_P.Text = .DATA_OBJEKTIF_03_P
                txtDATA_OBJEKTIF_03_N.Text = .DATA_OBJEKTIF_03_N
                txtDATA_OBJEKTIF_03_S.Text = .DATA_OBJEKTIF_03_S
                txtDATA_OBJEKTIF_03_SPO2.Text = .DATA_OBJEKTIF_03_SPO2
                chkDATA_OBJEKTIF_04_1_1.Checked = .DATA_OBJEKTIF_04_1_1
                chkDATA_OBJEKTIF_04_1_2.Checked = .DATA_OBJEKTIF_04_1_2
                chkDATA_OBJEKTIF_04_1_3.Checked = .DATA_OBJEKTIF_04_1_3
                chkDATA_OBJEKTIF_04_2_1.Checked = .DATA_OBJEKTIF_04_2_1
                chkDATA_OBJEKTIF_04_2_2.Checked = .DATA_OBJEKTIF_04_2_2
                chkDATA_OBJEKTIF_04_3_1.Checked = .DATA_OBJEKTIF_04_3_1
                chkDATA_OBJEKTIF_04_3_2.Checked = .DATA_OBJEKTIF_04_3_2
                chkRENCANA_TINDAKAN_01.Checked = .RENCANA_TINDAKAN_01
                chkRENCANA_TINDAKAN_02.Checked = .RENCANA_TINDAKAN_02
                chkRENCANA_TINDAKAN_03.Checked = .RENCANA_TINDAKAN_03
                chkRENCANA_TINDAKAN_04.Checked = .RENCANA_TINDAKAN_04
                chkRENCANA_TINDAKAN_05.Checked = .RENCANA_TINDAKAN_05
                chkRENCANA_TINDAKAN_06.Checked = .RENCANA_TINDAKAN_06
                chkRENCANA_TINDAKAN_07.Checked = .RENCANA_TINDAKAN_07
                chkRENCANA_TINDAKAN_08.Checked = .RENCANA_TINDAKAN_08
                chkRENCANA_TINDAKAN_09.Checked = .RENCANA_TINDAKAN_09
                chkRENCANA_TINDAKAN_10.Checked = .RENCANA_TINDAKAN_10
                chkRENCANA_TINDAKAN_11.Checked = .RENCANA_TINDAKAN_11
                chkRENCANA_TINDAKAN_12.Checked = .RENCANA_TINDAKAN_12
                txtRENCANA_TINDAKAN_12_TEXT.Text = .RENCANA_TINDAKAN_12_TEXT
                chkDATA_PENUNJANG_LAB_01.Checked = .DATA_PENUNJANG_LAB_01
                chkDATA_PENUNJANG_LAB_02.Checked = .DATA_PENUNJANG_LAB_02
                chkDATA_PENUNJANG_LAB_03.Checked = .DATA_PENUNJANG_LAB_03
                chkDATA_PENUNJANG_LAB_04.Checked = .DATA_PENUNJANG_LAB_04
                chkDATA_PENUNJANG_RAD_01.Checked = .DATA_PENUNJANG_RAD_01
                chkDATA_PENUNJANG_RAD_02.Checked = .DATA_PENUNJANG_RAD_02
                chkDATA_PENUNJANG_RAD_03.Checked = .DATA_PENUNJANG_RAD_03
                chkDATA_PENUNJANG_RAD_04.Checked = .DATA_PENUNJANG_RAD_04
                chkDATA_PENUNJANG_GIZI_01.Checked = .DATA_PENUNJANG_GIZI_01
                chkDATA_PENUNJANG_GIZI_02.Checked = .DATA_PENUNJANG_GIZI_02
                chkDATA_PENUNJANG_GIZI_03.Checked = .DATA_PENUNJANG_GIZI_03
                chkDATA_PENUNJANG_GIZI_04.Checked = .DATA_PENUNJANG_GIZI_04
                chkDATA_PENUNJANG_KONSUL_01.Checked = .DATA_PENUNJANG_KONSUL_01
                chkDATA_PENUNJANG_KONSUL_02.Checked = .DATA_PENUNJANG_KONSUL_02
                chkDATA_PENUNJANG_KONSUL_03.Checked = .DATA_PENUNJANG_KONSUL_03
                chkDATA_PENUNJANG_KONSUL_04.Checked = .DATA_PENUNJANG_KONSUL_04
                txtDATA_PENUNJANG_KONSUL_01_TEXT.Text = .DATA_PENUNJANG_KONSUL_01_TEXT
                txtDATA_PENUNJANG_KONSUL_02_TEXT.Text = .DATA_PENUNJANG_KONSUL_02_TEXT
                txtDATA_PENUNJANG_KONSUL_03_TEXT.Text = .DATA_PENUNJANG_KONSUL_03_TEXT
                txtDATA_PENUNJANG_KONSUL_04_TEXT.Text = .DATA_PENUNJANG_KONSUL_04_TEXT
                chkPERSIAPAN_DILAKUKAN_01.Checked = .PERSIAPAN_DILAKUKAN_01
                chkPERSIAPAN_DILAKUKAN_01_1.Checked = .PERSIAPAN_DILAKUKAN_01_1
                chkPERSIAPAN_DILAKUKAN_01_2.Checked = .PERSIAPAN_DILAKUKAN_01_2
                chkPERSIAPAN_DILAKUKAN_01_3.Checked = .PERSIAPAN_DILAKUKAN_01_3
                chkPERSIAPAN_DILAKUKAN_02.Checked = .PERSIAPAN_DILAKUKAN_02
                chkPERSIAPAN_DILAKUKAN_03.Checked = .PERSIAPAN_DILAKUKAN_03
                chkPERSIAPAN_DILAKUKAN_04.Checked = .PERSIAPAN_DILAKUKAN_04
                chkPERSIAPAN_DILAKUKAN_05.Checked = .PERSIAPAN_DILAKUKAN_05
                chkPERSIAPAN_DILAKUKAN_06_1.Checked = .PERSIAPAN_DILAKUKAN_06_1
                chkPERSIAPAN_DILAKUKAN_06_2.Checked = .PERSIAPAN_DILAKUKAN_06_2
                chkPERSIAPAN_DILAKUKAN_06_3.Checked = .PERSIAPAN_DILAKUKAN_06_3
                chkPERSIAPAN_DILAKUKAN_07.Checked = .PERSIAPAN_DILAKUKAN_07
                chkPERSIAPAN_DILAKUKAN_08.Checked = .PERSIAPAN_DILAKUKAN_08
                txtRESIKO_JATUH_01_TEXT.Text = .RESIKO_JATUH_01_TEXT
                txtRESIKO_JATUH_02_TEXT.Text = .RESIKO_JATUH_02_TEXT
                txtRESIKO_JATUH_03_TEXT.Text = .RESIKO_JATUH_03_TEXT
                txtRESIKO_JATUH_04_TEXT.Text = .RESIKO_JATUH_04_TEXT
                txtRESIKO_JATUH_05_TEXT.Text = .RESIKO_JATUH_05_TEXT
                txtRESIKO_JATUH_06_TEXT.Text = .RESIKO_JATUH_06_TEXT
                txtRESIKO_JATUH_JUMLAH_TEXT.Text = .RESIKO_JATUH_JUMLAH_TEXT
                chkSKALANYERI_00.Checked = .SKALANYERI_00
                chkSKALANYERI_01.Checked = .SKALANYERI_01
                chkSKALANYERI_02.Checked = .SKALANYERI_02
                chkSKALANYERI_03.Checked = .SKALANYERI_03
                chkSKALANYERI_04.Checked = .SKALANYERI_04
                chkSKALANYERI_05.Checked = .SKALANYERI_05
                chkSKALANYERI_06.Checked = .SKALANYERI_06
                chkSKALANYERI_07.Checked = .SKALANYERI_07
                chkSKALANYERI_08.Checked = .SKALANYERI_08
                chkSKALANYERI_09.Checked = .SKALANYERI_09
                chkSKALANYERI_10.Checked = .SKALANYERI_10
                txtSKALANYERI_KARAKTERISTIK.Text = .SKALANYERI_KARAKTERISTIK
                txtSKALANYERI_LOKASI.Text = .SKALANYERI_LOKASI
                txtSKALANYERI_DURASI.Text = .SKALANYERI_DURASI
                txtSKALANYERI_FREKUENSI.Text = .SKALANYERI_FREKUENSI
                txtRENCANA_ASUHAN_KEPERAWATAN_DIAGNOSA_KEPERAWATAN_03_TEXT.Text = .RENCANA_ASUHAN_KEPERAWATAN_DIAGNOSA_KEPERAWATAN_03_TEXT
                txtRENCANA_ASUHAN_KEPERAWATAN_DIAGNOSA_KEPERAWATAN_06_TEXT.Text = .RENCANA_ASUHAN_KEPERAWATAN_DIAGNOSA_KEPERAWATAN_06_TEXT
                txtRENCANA_ASUHAN_KEPERAWATAN_DIAGNOSA_KEPERAWATAN_07_TEXT.Text = .RENCANA_ASUHAN_KEPERAWATAN_DIAGNOSA_KEPERAWATAN_07_TEXT
                chkRENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_01.Checked = .RENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_01
                chkRENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_02.Checked = .RENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_02
                chkRENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_03.Checked = .RENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_03
                chkRENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_04.Checked = .RENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_04
                chkRENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_05.Checked = .RENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_05
                chkRENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_06.Checked = .RENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_06
                chkRENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_07.Checked = .RENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_07
                txtRENCANA_ASUHAN_KEPERAWATAN_TINDAKAN_TEXT.Text = .RENCANA_ASUHAN_KEPERAWATAN_TINDAKAN_TEXT
                txtRENCANA_ASUHAN_KEPERAWATAN_PERAWAT_TEXT.Text = .RENCANA_ASUHAN_KEPERAWATAN_PERAWAT_TEXT
                chkPROSEDUR_PEMANTAUAN_01.Checked = .PROSEDUR_PEMANTAUAN_01
                chkPROSEDUR_PEMANTAUAN_02.Checked = .PROSEDUR_PEMANTAUAN_02
                chkPROSEDUR_PEMANTAUAN_03.Checked = .PROSEDUR_PEMANTAUAN_03
                chkPROSEDUR_PEMANTAUAN_04.Checked = .PROSEDUR_PEMANTAUAN_04
                txtPROSEDUR_PEMANTAUAN_04_TEXT.Text = .PROSEDUR_PEMANTAUAN_04_TEXT
                chkPROSEDUR_PERSIAPAN_SEDASI_01.Checked = .PROSEDUR_PERSIAPAN_SEDASI_01
                chkPROSEDUR_PERSIAPAN_SEDASI_02.Checked = .PROSEDUR_PERSIAPAN_SEDASI_02
                chkPROSEDUR_PERSIAPAN_SEDASI_03.Checked = .PROSEDUR_PERSIAPAN_SEDASI_03
                chkPROSEDUR_PERSIAPAN_SEDASI_04.Checked = .PROSEDUR_PERSIAPAN_SEDASI_04
                chkPROSEDUR_PERSIAPAN_SEDASI_05.Checked = .PROSEDUR_PERSIAPAN_SEDASI_05
                chkPROSEDUR_PERSIAPAN_SEDASI_06.Checked = .PROSEDUR_PERSIAPAN_SEDASI_06
                txtPROSEDUR_PERSIAPAN_SEDASI_06_TEXT.Text = .PROSEDUR_PERSIAPAN_SEDASI_06_TEXT
                chkPROSEDUR_PENILAIAN_PRA_TINDAKAN_01.Checked = .PROSEDUR_PENILAIAN_PRA_TINDAKAN_01
                txtPROSEDUR_PENILAIAN_PRA_TINDAKAN_01_TEXT.Text = .PROSEDUR_PENILAIAN_PRA_TINDAKAN_01_TEXT
                chkPROSEDUR_PENILAIAN_PRA_TINDAKAN_02.Checked = .PROSEDUR_PENILAIAN_PRA_TINDAKAN_02
                txtPROSEDUR_PENILAIAN_PRA_TINDAKAN_02_TEXT.Text = .PROSEDUR_PENILAIAN_PRA_TINDAKAN_02_TEXT
                chkPROSEDUR_PENILAIAN_PRA_TINDAKAN_03.Checked = .PROSEDUR_PENILAIAN_PRA_TINDAKAN_03
                txtPROSEDUR_PENILAIAN_PRA_TINDAKAN_03_TEXT.Text = .PROSEDUR_PENILAIAN_PRA_TINDAKAN_03_TEXT
                chkPROSEDUR_PENILAIAN_PRA_TINDAKAN_04.Checked = .PROSEDUR_PENILAIAN_PRA_TINDAKAN_04
                txtPROSEDUR_PENILAIAN_PRA_TINDAKAN_04_TEXT.Text = .PROSEDUR_PENILAIAN_PRA_TINDAKAN_04_TEXT
                chkPROSEDUR_PENILAIAN_PRA_TINDAKAN_05.Checked = .PROSEDUR_PENILAIAN_PRA_TINDAKAN_05
                txtPROSEDUR_PENILAIAN_PRA_TINDAKAN_05_TEXT.Text = .PROSEDUR_PENILAIAN_PRA_TINDAKAN_05_TEXT
                chkPROSEDUR_PENILAIAN_PRA_TINDAKAN_06.Checked = .PROSEDUR_PENILAIAN_PRA_TINDAKAN_06
                txtPROSEDUR_PENILAIAN_PRA_TINDAKAN_06_TEXT.Text = .PROSEDUR_PENILAIAN_PRA_TINDAKAN_06_TEXT
                txtPROSEDUR_AKSES_INTRAVENA_TEXT.Text = .PROSEDUR_AKSES_INTRAVENA_TEXT
                chkPROSEDUR_MENGGUNAKAN_ALAT_01.Checked = .PROSEDUR_MENGGUNAKAN_ALAT_01
                chkPROSEDUR_MENGGUNAKAN_ALAT_01_1.Checked = .PROSEDUR_MENGGUNAKAN_ALAT_01_1
                chkPROSEDUR_MENGGUNAKAN_ALAT_01_2.Checked = .PROSEDUR_MENGGUNAKAN_ALAT_01_2
                chkPROSEDUR_MENGGUNAKAN_ALAT_02.Checked = .PROSEDUR_MENGGUNAKAN_ALAT_02
                chkPROSEDUR_MENGGUNAKAN_ALAT_02_1.Checked = .PROSEDUR_MENGGUNAKAN_ALAT_02_1
                chkPROSEDUR_MENGGUNAKAN_ALAT_02_2.Checked = .PROSEDUR_MENGGUNAKAN_ALAT_02_2
                chkPROSEDUR_MENGGUNAKAN_ALAT_03.Checked = .PROSEDUR_MENGGUNAKAN_ALAT_03
                txtPROSEDUR_MENGGUNAKAN_ALAT_03_TEXT.Text = .PROSEDUR_MENGGUNAKAN_ALAT_03_TEXT
                chkPROSEDUR_MENGGUNAKAN_ALAT_04.Checked = .PROSEDUR_MENGGUNAKAN_ALAT_04
                txtPROSEDUR_MENGGUNAKAN_ALAT_04_TEXT.Text = .PROSEDUR_MENGGUNAKAN_ALAT_04_TEXT
                chkPROSEDUR_MENGGUNAKAN_ALAT_05.Checked = .PROSEDUR_MENGGUNAKAN_ALAT_05
                chkPROSEDUR_MENGGUNAKAN_ALAT_05_1.Checked = .PROSEDUR_MENGGUNAKAN_ALAT_05_1
                chkPROSEDUR_MENGGUNAKAN_ALAT_05_2.Checked = .PROSEDUR_MENGGUNAKAN_ALAT_05_2
                chkPROSEDUR_MENGGUNAKAN_ALAT_06.Checked = .PROSEDUR_MENGGUNAKAN_ALAT_06
                chkPROSEDUR_MENGGUNAKAN_ALAT_07.Checked = .PROSEDUR_MENGGUNAKAN_ALAT_07
                txtPROSEDUR_MENGGUNAKAN_ALAT_07_TEXT.Text = .PROSEDUR_MENGGUNAKAN_ALAT_07_TEXT
                chkPROSEDUR_MENGGUNAKAN_ALAT_08.Checked = .PROSEDUR_MENGGUNAKAN_ALAT_08
                txtPROSEDUR_MENGGUNAKAN_ALAT_08_TEXT.Text = .PROSEDUR_MENGGUNAKAN_ALAT_08_TEXT
                txtRENCANA_ASUHAN_KEPERAWATAN_INTRA_DIAGNOSA_KEPERAWATAN_03_TEXT.Text = .RENCANA_ASUHAN_KEPERAWATAN_INTRA_DIAGNOSA_KEPERAWATAN_03_TEXT
                txtRENCANA_ASUHAN_KEPERAWATAN_INTRA_DIAGNOSA_KEPERAWATAN_06_TEXT.Text = .RENCANA_ASUHAN_KEPERAWATAN_INTRA_DIAGNOSA_KEPERAWATAN_06_TEXT
                txtRENCANA_ASUHAN_KEPERAWATAN_INTRA_DIAGNOSA_KEPERAWATAN_07_TEXT.Text = .RENCANA_ASUHAN_KEPERAWATAN_INTRA_DIAGNOSA_KEPERAWATAN_07_TEXT
                chkRENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_01.Checked = .RENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_01
                chkRENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_02.Checked = .RENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_02
                chkRENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_03.Checked = .RENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_03
                chkRENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_04.Checked = .RENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_04
                chkRENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_05.Checked = .RENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_05
                chkRENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_06.Checked = .RENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_06
                chkRENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_07.Checked = .RENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_07
                txtRENCANA_ASUHAN_KEPERAWATAN_INTRA_TINDAKAN_TEXT.Text = .RENCANA_ASUHAN_KEPERAWATAN_INTRA_TINDAKAN_TEXT
                txtRENCANA_ASUHAN_KEPERAWATAN_INTRA_PERAWAT_TEXT.Text = .RENCANA_ASUHAN_KEPERAWATAN_INTRA_PERAWAT_TEXT
                txtRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_DIAGNOSA_KEPERAWATAN_03_TEXT.Text = .RENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_DIAGNOSA_KEPERAWATAN_03_TEXT
                txtRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_DIAGNOSA_KEPERAWATAN_06_TEXT.Text = .RENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_DIAGNOSA_KEPERAWATAN_06_TEXT
                txtRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_DIAGNOSA_KEPERAWATAN_07_TEXT.Text = .RENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_DIAGNOSA_KEPERAWATAN_07_TEXT
                chkRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_01.Checked = .RENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_01
                chkRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_02.Checked = .RENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_02
                chkRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_03.Checked = .RENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_03
                chkRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_04.Checked = .RENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_04
                chkRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_05.Checked = .RENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_05
                chkRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_06.Checked = .RENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_06
                chkRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_07.Checked = .RENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_07
                txtRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_TINDAKAN_TEXT.Text = .RENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_TINDAKAN_TEXT
                txtRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_PERAWAT_TEXT.Text = .RENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_PERAWAT_TEXT
                chkDATA_SUBJEKTIF_05_1.Checked = .DATA_SUBJEKTIF_05_1
                chkDATA_SUBJEKTIF_05_2.Checked = .DATA_SUBJEKTIF_05_2
                txtDATA_PENUNJANG_LAB_03_TEXT.Text = .DATA_PENUNJANG_LAB_03_TEXT
                txtDATA_PENUNJANG_LAB_04_TEXT.Text = .DATA_PENUNJANG_LAB_04_TEXT
                txtDATA_PENUNJANG_RAD_04_TEXT.Text = .DATA_PENUNJANG_RAD_04_TEXT
                txtDATA_PENUNJANG_GIZI_04_TEXT.Text = .DATA_PENUNJANG_GIZI_04_TEXT

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
            If grdKDDOCTOR1.Text = String.Empty Then
                MsgBox("Dokter Operator", MsgBoxStyle.Exclamation, Me.Text)
                grdKDDOCTOR1.Focus()
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
            Dim ds = oS_DIGITAL_RJ_05.GetStructureHeader


            With ds
                .KDKUNJUNGAN = txtNoRegister.Text
                Try
                    .DATECREATED = oS_DIGITAL_RJ_05.GetData(txtNoRegister.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .RUANG_KAMAR = txtRUANG_KAMAR.Text
                .JAM_PROSEDUR = deJAM_PROSEDUR.DateTime
                .DOKTER_1_KODE = grdKDDOCTOR1.EditValue
                .DOKTER_1_NAMEDISPLAY = grdKDDOCTOR1.Text
                .DOKTER_2_KODE = grdKDDOCTOR2.EditValue
                .DOKTER_2_NAMEDISPLAY = grdKDDOCTOR2.Text
                .JENIS_TINDAKAN = txtJENIS_TINDAKAN.Text
                .TANGGAL_PROSEDUR = deTANGGAL_PROSEDUR.DateTime
                .JENIS_PROSEDUR = txtJENIS_PROSEDUR.Text
                .DATE = deDATE.DateTime
                .DATA_SUBJEKTIF_01 = txtDATA_SUBJEKTIF_01.Text
                .DATA_SUBJEKTIF_02 = txtDATA_SUBJEKTIF_02.Text
                .DATA_SUBJEKTIF_03 = txtDATA_SUBJEKTIF_03.Text
                .DATA_SUBJEKTIF_04 = txtDATA_SUBJEKTIF_04.Text
                .DATA_SUBJEKTIF_06 = txtDATA_SUBJEKTIF_06.Text
                .DATA_SUBJEKTIF_07 = txtDATA_SUBJEKTIF_07.Text
                .DATA_OBJEKTIF_01_1 = chkDATA_OBJEKTIF_01_1.Checked
                .DATA_OBJEKTIF_01_2 = chkDATA_OBJEKTIF_01_2.Checked
                .DATA_OBJEKTIF_01_3 = chkDATA_OBJEKTIF_01_3.Checked
                .DATA_OBJEKTIF_02_1 = chkDATA_OBJEKTIF_02_1.Checked
                .DATA_OBJEKTIF_02_2 = chkDATA_OBJEKTIF_02_2.Checked
                .DATA_OBJEKTIF_02_3 = chkDATA_OBJEKTIF_02_3.Checked
                .DATA_OBJEKTIF_02_4 = chkDATA_OBJEKTIF_02_4.Checked
                .DATA_OBJEKTIF_02_5 = chkDATA_OBJEKTIF_02_5.Checked
                .DATA_OBJEKTIF_03_TD = txtDATA_OBJEKTIF_03_TD.Text
                .DATA_OBJEKTIF_03_P = txtDATA_OBJEKTIF_03_P.Text
                .DATA_OBJEKTIF_03_N = txtDATA_OBJEKTIF_03_N.Text
                .DATA_OBJEKTIF_03_S = txtDATA_OBJEKTIF_03_S.Text
                .DATA_OBJEKTIF_03_SPO2 = txtDATA_OBJEKTIF_03_SPO2.Text
                .DATA_OBJEKTIF_04_1_1 = chkDATA_OBJEKTIF_04_1_1.Checked
                .DATA_OBJEKTIF_04_1_2 = chkDATA_OBJEKTIF_04_1_2.Checked
                .DATA_OBJEKTIF_04_1_3 = chkDATA_OBJEKTIF_04_1_3.Checked
                .DATA_OBJEKTIF_04_2_1 = chkDATA_OBJEKTIF_04_2_1.Checked
                .DATA_OBJEKTIF_04_2_2 = chkDATA_OBJEKTIF_04_2_2.Checked
                .DATA_OBJEKTIF_04_3_1 = chkDATA_OBJEKTIF_04_3_1.Checked
                .DATA_OBJEKTIF_04_3_2 = chkDATA_OBJEKTIF_04_3_2.Checked
                .RENCANA_TINDAKAN_01 = chkRENCANA_TINDAKAN_01.Checked
                .RENCANA_TINDAKAN_02 = chkRENCANA_TINDAKAN_02.Checked
                .RENCANA_TINDAKAN_03 = chkRENCANA_TINDAKAN_03.Checked
                .RENCANA_TINDAKAN_04 = chkRENCANA_TINDAKAN_04.Checked
                .RENCANA_TINDAKAN_05 = chkRENCANA_TINDAKAN_05.Checked
                .RENCANA_TINDAKAN_06 = chkRENCANA_TINDAKAN_06.Checked
                .RENCANA_TINDAKAN_07 = chkRENCANA_TINDAKAN_07.Checked
                .RENCANA_TINDAKAN_08 = chkRENCANA_TINDAKAN_08.Checked
                .RENCANA_TINDAKAN_09 = chkRENCANA_TINDAKAN_09.Checked
                .RENCANA_TINDAKAN_10 = chkRENCANA_TINDAKAN_10.Checked
                .RENCANA_TINDAKAN_11 = chkRENCANA_TINDAKAN_11.Checked
                .RENCANA_TINDAKAN_12 = chkRENCANA_TINDAKAN_12.Checked
                .RENCANA_TINDAKAN_12_TEXT = txtRENCANA_TINDAKAN_12_TEXT.Text
                .DATA_PENUNJANG_LAB_01 = chkDATA_PENUNJANG_LAB_01.Checked
                .DATA_PENUNJANG_LAB_02 = chkDATA_PENUNJANG_LAB_02.Checked
                .DATA_PENUNJANG_LAB_03 = chkDATA_PENUNJANG_LAB_03.Checked
                .DATA_PENUNJANG_LAB_04 = chkDATA_PENUNJANG_LAB_04.Checked
                .DATA_PENUNJANG_RAD_01 = chkDATA_PENUNJANG_RAD_01.Checked
                .DATA_PENUNJANG_RAD_02 = chkDATA_PENUNJANG_RAD_02.Checked
                .DATA_PENUNJANG_RAD_03 = chkDATA_PENUNJANG_RAD_03.Checked
                .DATA_PENUNJANG_RAD_04 = chkDATA_PENUNJANG_RAD_04.Checked
                .DATA_PENUNJANG_GIZI_01 = chkDATA_PENUNJANG_GIZI_01.Checked
                .DATA_PENUNJANG_GIZI_02 = chkDATA_PENUNJANG_GIZI_02.Checked
                .DATA_PENUNJANG_GIZI_03 = chkDATA_PENUNJANG_GIZI_03.Checked
                .DATA_PENUNJANG_GIZI_04 = chkDATA_PENUNJANG_GIZI_04.Checked
                .DATA_PENUNJANG_KONSUL_01 = chkDATA_PENUNJANG_KONSUL_01.Checked
                .DATA_PENUNJANG_KONSUL_02 = chkDATA_PENUNJANG_KONSUL_02.Checked
                .DATA_PENUNJANG_KONSUL_03 = chkDATA_PENUNJANG_KONSUL_03.Checked
                .DATA_PENUNJANG_KONSUL_04 = chkDATA_PENUNJANG_KONSUL_04.Checked
                .DATA_PENUNJANG_KONSUL_01_TEXT = txtDATA_PENUNJANG_KONSUL_01_TEXT.Text
                .DATA_PENUNJANG_KONSUL_02_TEXT = txtDATA_PENUNJANG_KONSUL_02_TEXT.Text
                .DATA_PENUNJANG_KONSUL_03_TEXT = txtDATA_PENUNJANG_KONSUL_03_TEXT.Text
                .DATA_PENUNJANG_KONSUL_04_TEXT = txtDATA_PENUNJANG_KONSUL_04_TEXT.Text
                .PERSIAPAN_DILAKUKAN_01 = chkPERSIAPAN_DILAKUKAN_01.Checked
                .PERSIAPAN_DILAKUKAN_01_1 = chkPERSIAPAN_DILAKUKAN_01_1.Checked
                .PERSIAPAN_DILAKUKAN_01_2 = chkPERSIAPAN_DILAKUKAN_01_2.Checked
                .PERSIAPAN_DILAKUKAN_01_3 = chkPERSIAPAN_DILAKUKAN_01_3.Checked
                .PERSIAPAN_DILAKUKAN_02 = chkPERSIAPAN_DILAKUKAN_02.Checked
                .PERSIAPAN_DILAKUKAN_03 = chkPERSIAPAN_DILAKUKAN_03.Checked
                .PERSIAPAN_DILAKUKAN_04 = chkPERSIAPAN_DILAKUKAN_04.Checked
                .PERSIAPAN_DILAKUKAN_05 = chkPERSIAPAN_DILAKUKAN_05.Checked
                .PERSIAPAN_DILAKUKAN_06_1 = chkPERSIAPAN_DILAKUKAN_06_1.Checked
                .PERSIAPAN_DILAKUKAN_06_2 = chkPERSIAPAN_DILAKUKAN_06_2.Checked
                .PERSIAPAN_DILAKUKAN_06_3 = chkPERSIAPAN_DILAKUKAN_06_3.Checked
                .PERSIAPAN_DILAKUKAN_07 = chkPERSIAPAN_DILAKUKAN_07.Checked
                .PERSIAPAN_DILAKUKAN_08 = chkPERSIAPAN_DILAKUKAN_08.Checked
                .RESIKO_JATUH_01_TEXT = txtRESIKO_JATUH_01_TEXT.Text
                .RESIKO_JATUH_02_TEXT = txtRESIKO_JATUH_02_TEXT.Text
                .RESIKO_JATUH_03_TEXT = txtRESIKO_JATUH_03_TEXT.Text
                .RESIKO_JATUH_04_TEXT = txtRESIKO_JATUH_04_TEXT.Text
                .RESIKO_JATUH_05_TEXT = txtRESIKO_JATUH_05_TEXT.Text
                .RESIKO_JATUH_06_TEXT = txtRESIKO_JATUH_06_TEXT.Text
                .RESIKO_JATUH_JUMLAH_TEXT = txtRESIKO_JATUH_JUMLAH_TEXT.Text
                .SKALANYERI_00 = chkSKALANYERI_00.Checked
                .SKALANYERI_01 = chkSKALANYERI_01.Checked
                .SKALANYERI_02 = chkSKALANYERI_02.Checked
                .SKALANYERI_03 = chkSKALANYERI_03.Checked
                .SKALANYERI_04 = chkSKALANYERI_04.Checked
                .SKALANYERI_05 = chkSKALANYERI_05.Checked
                .SKALANYERI_06 = chkSKALANYERI_06.Checked
                .SKALANYERI_07 = chkSKALANYERI_07.Checked
                .SKALANYERI_08 = chkSKALANYERI_08.Checked
                .SKALANYERI_09 = chkSKALANYERI_09.Checked
                .SKALANYERI_10 = chkSKALANYERI_10.Checked
                .SKALANYERI_KARAKTERISTIK = txtSKALANYERI_KARAKTERISTIK.Text
                .SKALANYERI_LOKASI = txtSKALANYERI_LOKASI.Text
                .SKALANYERI_DURASI = txtSKALANYERI_DURASI.Text
                .SKALANYERI_FREKUENSI = txtSKALANYERI_FREKUENSI.Text
                .RENCANA_ASUHAN_KEPERAWATAN_DIAGNOSA_KEPERAWATAN_03_TEXT = txtRENCANA_ASUHAN_KEPERAWATAN_DIAGNOSA_KEPERAWATAN_03_TEXT.Text
                .RENCANA_ASUHAN_KEPERAWATAN_DIAGNOSA_KEPERAWATAN_06_TEXT = txtRENCANA_ASUHAN_KEPERAWATAN_DIAGNOSA_KEPERAWATAN_06_TEXT.Text
                .RENCANA_ASUHAN_KEPERAWATAN_DIAGNOSA_KEPERAWATAN_07_TEXT = txtRENCANA_ASUHAN_KEPERAWATAN_DIAGNOSA_KEPERAWATAN_07_TEXT.Text
                .RENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_01 = chkRENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_01.Checked
                .RENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_02 = chkRENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_02.Checked
                .RENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_03 = chkRENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_03.Checked
                .RENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_04 = chkRENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_04.Checked
                .RENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_05 = chkRENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_05.Checked
                .RENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_06 = chkRENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_06.Checked
                .RENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_07 = chkRENCANA_ASUHAN_KEPERAWATAN_INTERVENSI_07.Checked
                .RENCANA_ASUHAN_KEPERAWATAN_TINDAKAN_TEXT = txtRENCANA_ASUHAN_KEPERAWATAN_TINDAKAN_TEXT.Text
                .RENCANA_ASUHAN_KEPERAWATAN_PERAWAT_TEXT = txtRENCANA_ASUHAN_KEPERAWATAN_PERAWAT_TEXT.Text
                .PROSEDUR_PEMANTAUAN_01 = chkPROSEDUR_PEMANTAUAN_01.Checked
                .PROSEDUR_PEMANTAUAN_02 = chkPROSEDUR_PEMANTAUAN_02.Checked
                .PROSEDUR_PEMANTAUAN_03 = chkPROSEDUR_PEMANTAUAN_03.Checked
                .PROSEDUR_PEMANTAUAN_04 = chkPROSEDUR_PEMANTAUAN_04.Checked
                .PROSEDUR_PEMANTAUAN_04_TEXT = txtPROSEDUR_PEMANTAUAN_04_TEXT.Text
                .PROSEDUR_PERSIAPAN_SEDASI_01 = chkPROSEDUR_PERSIAPAN_SEDASI_01.Checked
                .PROSEDUR_PERSIAPAN_SEDASI_02 = chkPROSEDUR_PERSIAPAN_SEDASI_02.Checked
                .PROSEDUR_PERSIAPAN_SEDASI_03 = chkPROSEDUR_PERSIAPAN_SEDASI_03.Checked
                .PROSEDUR_PERSIAPAN_SEDASI_04 = chkPROSEDUR_PERSIAPAN_SEDASI_04.Checked
                .PROSEDUR_PERSIAPAN_SEDASI_05 = chkPROSEDUR_PERSIAPAN_SEDASI_05.Checked
                .PROSEDUR_PERSIAPAN_SEDASI_06 = chkPROSEDUR_PERSIAPAN_SEDASI_06.Checked
                .PROSEDUR_PERSIAPAN_SEDASI_06_TEXT = txtPROSEDUR_PERSIAPAN_SEDASI_06_TEXT.Text
                .PROSEDUR_PENILAIAN_PRA_TINDAKAN_01 = chkPROSEDUR_PENILAIAN_PRA_TINDAKAN_01.Checked
                .PROSEDUR_PENILAIAN_PRA_TINDAKAN_01_TEXT = txtPROSEDUR_PENILAIAN_PRA_TINDAKAN_01_TEXT.Text
                .PROSEDUR_PENILAIAN_PRA_TINDAKAN_02 = chkPROSEDUR_PENILAIAN_PRA_TINDAKAN_02.Checked
                .PROSEDUR_PENILAIAN_PRA_TINDAKAN_02_TEXT = txtPROSEDUR_PENILAIAN_PRA_TINDAKAN_02_TEXT.Text
                .PROSEDUR_PENILAIAN_PRA_TINDAKAN_03 = chkPROSEDUR_PENILAIAN_PRA_TINDAKAN_03.Checked
                .PROSEDUR_PENILAIAN_PRA_TINDAKAN_03_TEXT = txtPROSEDUR_PENILAIAN_PRA_TINDAKAN_03_TEXT.Text
                .PROSEDUR_PENILAIAN_PRA_TINDAKAN_04 = chkPROSEDUR_PENILAIAN_PRA_TINDAKAN_04.Checked
                .PROSEDUR_PENILAIAN_PRA_TINDAKAN_04_TEXT = txtPROSEDUR_PENILAIAN_PRA_TINDAKAN_04_TEXT.Text
                .PROSEDUR_PENILAIAN_PRA_TINDAKAN_05 = chkPROSEDUR_PENILAIAN_PRA_TINDAKAN_05.Checked
                .PROSEDUR_PENILAIAN_PRA_TINDAKAN_05_TEXT = txtPROSEDUR_PENILAIAN_PRA_TINDAKAN_05_TEXT.Text
                .PROSEDUR_PENILAIAN_PRA_TINDAKAN_06 = chkPROSEDUR_PENILAIAN_PRA_TINDAKAN_06.Checked
                .PROSEDUR_PENILAIAN_PRA_TINDAKAN_06_TEXT = txtPROSEDUR_PENILAIAN_PRA_TINDAKAN_06_TEXT.Text
                .PROSEDUR_AKSES_INTRAVENA_TEXT = txtPROSEDUR_AKSES_INTRAVENA_TEXT.Text
                .PROSEDUR_MENGGUNAKAN_ALAT_01 = chkPROSEDUR_MENGGUNAKAN_ALAT_01.Checked
                .PROSEDUR_MENGGUNAKAN_ALAT_01_1 = chkPROSEDUR_MENGGUNAKAN_ALAT_01_1.Checked
                .PROSEDUR_MENGGUNAKAN_ALAT_01_2 = chkPROSEDUR_MENGGUNAKAN_ALAT_01_2.Checked
                .PROSEDUR_MENGGUNAKAN_ALAT_02 = chkPROSEDUR_MENGGUNAKAN_ALAT_02.Checked
                .PROSEDUR_MENGGUNAKAN_ALAT_02_1 = chkPROSEDUR_MENGGUNAKAN_ALAT_02_1.Checked
                .PROSEDUR_MENGGUNAKAN_ALAT_02_2 = chkPROSEDUR_MENGGUNAKAN_ALAT_02_2.Checked
                .PROSEDUR_MENGGUNAKAN_ALAT_03 = chkPROSEDUR_MENGGUNAKAN_ALAT_03.Checked
                .PROSEDUR_MENGGUNAKAN_ALAT_03_TEXT = txtPROSEDUR_MENGGUNAKAN_ALAT_03_TEXT.Text
                .PROSEDUR_MENGGUNAKAN_ALAT_04 = chkPROSEDUR_MENGGUNAKAN_ALAT_04.Checked
                .PROSEDUR_MENGGUNAKAN_ALAT_04_TEXT = txtPROSEDUR_MENGGUNAKAN_ALAT_04_TEXT.Text
                .PROSEDUR_MENGGUNAKAN_ALAT_05 = chkPROSEDUR_MENGGUNAKAN_ALAT_05.Checked
                .PROSEDUR_MENGGUNAKAN_ALAT_05_1 = chkPROSEDUR_MENGGUNAKAN_ALAT_05_1.Checked
                .PROSEDUR_MENGGUNAKAN_ALAT_05_2 = chkPROSEDUR_MENGGUNAKAN_ALAT_05_2.Checked
                .PROSEDUR_MENGGUNAKAN_ALAT_06 = chkPROSEDUR_MENGGUNAKAN_ALAT_06.Checked
                .PROSEDUR_MENGGUNAKAN_ALAT_07 = chkPROSEDUR_MENGGUNAKAN_ALAT_07.Checked
                .PROSEDUR_MENGGUNAKAN_ALAT_07_TEXT = txtPROSEDUR_MENGGUNAKAN_ALAT_07_TEXT.Text
                .PROSEDUR_MENGGUNAKAN_ALAT_08 = chkPROSEDUR_MENGGUNAKAN_ALAT_08.Checked
                .PROSEDUR_MENGGUNAKAN_ALAT_08_TEXT = txtPROSEDUR_MENGGUNAKAN_ALAT_08_TEXT.Text
                .RENCANA_ASUHAN_KEPERAWATAN_INTRA_DIAGNOSA_KEPERAWATAN_03_TEXT = txtRENCANA_ASUHAN_KEPERAWATAN_INTRA_DIAGNOSA_KEPERAWATAN_03_TEXT.Text
                .RENCANA_ASUHAN_KEPERAWATAN_INTRA_DIAGNOSA_KEPERAWATAN_06_TEXT = txtRENCANA_ASUHAN_KEPERAWATAN_INTRA_DIAGNOSA_KEPERAWATAN_06_TEXT.Text
                .RENCANA_ASUHAN_KEPERAWATAN_INTRA_DIAGNOSA_KEPERAWATAN_07_TEXT = txtRENCANA_ASUHAN_KEPERAWATAN_INTRA_DIAGNOSA_KEPERAWATAN_07_TEXT.Text
                .RENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_01 = chkRENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_01.Checked
                .RENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_02 = chkRENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_02.Checked
                .RENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_03 = chkRENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_03.Checked
                .RENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_04 = chkRENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_04.Checked
                .RENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_05 = chkRENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_05.Checked
                .RENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_06 = chkRENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_06.Checked
                .RENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_07 = chkRENCANA_ASUHAN_KEPERAWATAN_INTRA_INTERVENSI_07.Checked
                .RENCANA_ASUHAN_KEPERAWATAN_INTRA_TINDAKAN_TEXT = txtRENCANA_ASUHAN_KEPERAWATAN_INTRA_TINDAKAN_TEXT.Text
                .RENCANA_ASUHAN_KEPERAWATAN_INTRA_PERAWAT_TEXT = txtRENCANA_ASUHAN_KEPERAWATAN_INTRA_PERAWAT_TEXT.Text
                .RENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_DIAGNOSA_KEPERAWATAN_03_TEXT = txtRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_DIAGNOSA_KEPERAWATAN_03_TEXT.Text
                .RENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_DIAGNOSA_KEPERAWATAN_06_TEXT = txtRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_DIAGNOSA_KEPERAWATAN_06_TEXT.Text
                .RENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_DIAGNOSA_KEPERAWATAN_07_TEXT = txtRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_DIAGNOSA_KEPERAWATAN_07_TEXT.Text
                .RENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_01 = chkRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_01.Checked
                .RENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_02 = chkRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_02.Checked
                .RENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_03 = chkRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_03.Checked
                .RENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_04 = chkRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_04.Checked
                .RENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_05 = chkRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_05.Checked
                .RENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_06 = chkRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_06.Checked
                .RENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_07 = chkRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_INTERVENSI_07.Checked
                .RENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_TINDAKAN_TEXT = txtRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_TINDAKAN_TEXT.Text
                .RENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_PERAWAT_TEXT = txtRENCANA_ASUHAN_KEPERAWATAN_PASCA_ENDOSKOPI_PERAWAT_TEXT.Text
                .DATA_SUBJEKTIF_05_1 = chkDATA_SUBJEKTIF_05_1.Checked
                .DATA_SUBJEKTIF_05_2 = chkDATA_SUBJEKTIF_05_2.Checked
                .DATA_PENUNJANG_LAB_03_TEXT = txtDATA_PENUNJANG_LAB_03_TEXT.Text
                .DATA_PENUNJANG_LAB_04_TEXT = txtDATA_PENUNJANG_LAB_04_TEXT.Text
                .DATA_PENUNJANG_RAD_04_TEXT = txtDATA_PENUNJANG_RAD_04_TEXT.Text
                .DATA_PENUNJANG_GIZI_04_TEXT = txtDATA_PENUNJANG_GIZI_04_TEXT.Text

                Try
                    .CETAK = oS_DIGITAL_RJ_05.GetData(txtNoRegister.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try
                
                Try
                    If sIsOtority = True Then
                        .KDUSER = oS_DIGITAL_RJ_05.GetData(txtNoRegister.Text).KDUSER
                        .KDUSER_SIGNATURE = oS_DIGITAL_RJ_05.GetData(txtNoRegister.Text).KDUSER_SIGNATURE
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
                    fn_Save = oS_DIGITAL_RJ_05.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_RJ_05.UpdateData(ds)
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
            Case Keys.F2
                If btnSaveNew.Enabled = True Then
                    btnSaveNew_Click()
                End If
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
    Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & txtNoRegister.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtNoRegister.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            'sStatusSave = "NEW"
            Me.Close()
        End If
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
    Private Sub fn_Doctor()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = sKoneksi
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

            grdKDDOCTOR1.Properties.DataSource = ds.Tables("DOKTER")
            grdKDDOCTOR1.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR1.Properties.DisplayMember = "NAME_DISPLAY"

            grdKDDOCTOR2.Properties.DataSource = ds.Tables("DOKTER")
            grdKDDOCTOR2.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR2.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub frmEMedrekRJ_05_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
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