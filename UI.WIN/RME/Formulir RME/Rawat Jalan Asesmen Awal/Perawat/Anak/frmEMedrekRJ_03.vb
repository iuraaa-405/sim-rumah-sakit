Imports System.Data.SqlClient
Imports DataAccess
Imports UI.WIN.MAIN.My.Resources

Public Class frmEMedrekRJ_03
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_RJ_03 As New Digital.clsDigital_RJ_03
    Private down As Boolean = False
    Private sKODEDOKTER As String

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String)
        oFormMode = FormMode

        Dim oPendaftaran As New Identitas.clsIdentitasPasien
        Dim dsPendaftaran = oPendaftaran.GetData(KDREG)

        txtNoRegister.Text = KDREG

        If dsPendaftaran IsNot Nothing Then
            txtNoPasien.Text = dsPendaftaran.KDCUSTOMER
            txtNamaPasien.Text = dsPendaftaran.NAMAPASIEN
            txtUmur.Text = dsPendaftaran.USIA
            txtTanggalDaftar.Text = dsPendaftaran.DATE.ToString("dd-MM-yyyy HH:mm:ss")
            txtNoRegister.Text = dsPendaftaran.KDKUNJUNGAN
            txtTujuan.Text = dsPendaftaran.TUJUAN
            txtDokter.Text = dsPendaftaran.DOKTER
            sKODEDOKTER = dsPendaftaran.KDDOKTER
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
        Me.Text = EMedrekRJ_03.TITLE
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
        fn_NOIDUSER()

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
        chkAUTO.Properties.ReadOnly = Status
        chkALLO.Properties.ReadOnly = Status
        txtNAMAPENGKAJIAN.Properties.ReadOnly = Status
        txtKELUHAN_UTAMA.Properties.ReadOnly = Status
        txtKESADARAN_UMUM.Properties.ReadOnly = Status
        txt_GCS.Properties.ReadOnly = Status
        cbo_E.Properties.ReadOnly = Status
        cbo_M.Properties.ReadOnly = Status
        cbo_V.Properties.ReadOnly = Status
        chkKEADAAN_EMOSIONAL_01.Properties.ReadOnly = Status
        chkKEADAAN_EMOSIONAL_02.Properties.ReadOnly = Status
        chkKEADAAN_EMOSIONAL_03.Properties.ReadOnly = Status
        chkKEADAAN_EMOSIONAL_04.Properties.ReadOnly = Status
        txtKEADAAN_EMOSIONAL_04_TEXT.Properties.ReadOnly = Status
        txtTANDA_VITAL_01.Properties.ReadOnly = Status
        txtTANDA_VITAL_02.Properties.ReadOnly = Status
        txtTANDA_VITAL_03.Properties.ReadOnly = Status
        txtTANDA_VITAL_04.Properties.ReadOnly = Status
        txtTANDA_VITAL_05.Properties.ReadOnly = Status
        txtTANDA_VITAL_06.Properties.ReadOnly = Status
        txtTANDA_VITAL_07.Properties.ReadOnly = Status
        txtTANDA_VITAL_08.Properties.ReadOnly = Status
        chkPERSYARAFAN_01.Properties.ReadOnly = Status
        chkPERSYARAFAN_02.Properties.ReadOnly = Status
        chkPERSYARAFAN_03.Properties.ReadOnly = Status
        chkPERSYARAFAN_04.Properties.ReadOnly = Status
        chkPERSYARAFAN_05.Properties.ReadOnly = Status
        chkPERSYARAFAN_06.Properties.ReadOnly = Status
        txtPERSYARAFAN_06_TEXT.Properties.ReadOnly = Status
        chkPERNAPASAN_01.Properties.ReadOnly = Status
        chkPERNAPASAN_02.Properties.ReadOnly = Status
        chkPERNAPASAN_03.Properties.ReadOnly = Status
        chkPERNAPASAN_04.Properties.ReadOnly = Status
        txtPERNAPASAN_04_TEXT.Properties.ReadOnly = Status
        chkPENCERNAAN_01.Properties.ReadOnly = Status
        chkPENCERNAAN_02.Properties.ReadOnly = Status
        chkPENCERNAAN_03.Properties.ReadOnly = Status
        chkPENCERNAAN_04.Properties.ReadOnly = Status
        chkPENCERNAAN_05.Properties.ReadOnly = Status
        txtPENCERNAAN_05_TEXT.Properties.ReadOnly = Status
        chkENDROKIN_01.Properties.ReadOnly = Status
        chkENDROKIN_02.Properties.ReadOnly = Status
        chkENDROKIN_03.Properties.ReadOnly = Status
        chkENDROKIN_04.Properties.ReadOnly = Status
        txtENDROKIN_04_TEXT.Properties.ReadOnly = Status
        chkCARDIOVASKULER_01.Properties.ReadOnly = Status
        chkCARDIOVASKULER_02.Properties.ReadOnly = Status
        chkCARDIOVASKULER_03.Properties.ReadOnly = Status
        chkCARDIOVASKULER_04.Properties.ReadOnly = Status
        txtCARDIOVASKULER_04_TEXT.Properties.ReadOnly = Status
        chkABDOMEN_01.Properties.ReadOnly = Status
        chkABDOMEN_02.Properties.ReadOnly = Status
        chkABDOMEN_03.Properties.ReadOnly = Status
        chkABDOMEN_04.Properties.ReadOnly = Status
        chkABDOMEN_05.Properties.ReadOnly = Status
        txtABDOMEN_05_TEXT.Properties.ReadOnly = Status
        chkABDOMEN_06.Properties.ReadOnly = Status
        txtABDOMEN_06_TEXT.Properties.ReadOnly = Status
        chkABDOMEN_07.Properties.ReadOnly = Status
        txtABDOMEN_07_TEXT.Properties.ReadOnly = Status
        chkABDOMEN_08.Properties.ReadOnly = Status
        txtABDOMEN_08_TEXT.Properties.ReadOnly = Status
        chkABDOMEN_09.Properties.ReadOnly = Status
        txtABDOMEN_09_TEXT.Properties.ReadOnly = Status
        chkREPRODUKSI_01.Properties.ReadOnly = Status
        chkREPRODUKSI_02.Properties.ReadOnly = Status
        chkREPRODUKSI_03.Properties.ReadOnly = Status
        chkREPRODUKSI_04.Properties.ReadOnly = Status
        chkREPRODUKSI_05.Properties.ReadOnly = Status
        txtREPRODUKSI_01_TEXT.Properties.ReadOnly = Status
        txtREPRODUKSI_02_TEXT.Properties.ReadOnly = Status
        txtREPRODUKSI_03_TEXT.Properties.ReadOnly = Status
        txtREPRODUKSI_04_TEXT.Properties.ReadOnly = Status
        txtREPRODUKSI_05_TEXT.Properties.ReadOnly = Status
        txtREPRODUKSI_06_TEXT.Properties.ReadOnly = Status
        txtREPRODUKSI_07_TEXT.Properties.ReadOnly = Status
        chkKULIT_01.Properties.ReadOnly = Status
        chkKULIT_02.Properties.ReadOnly = Status
        txtKULIT_02_TEXT.Properties.ReadOnly = Status
        chkKULIT_03.Properties.ReadOnly = Status
        txtKULIT_03_TEXT.Properties.ReadOnly = Status
        chkKULIT_04.Properties.ReadOnly = Status
        chkKULIT_05.Properties.ReadOnly = Status
        chkKULIT_06.Properties.ReadOnly = Status
        txtKULIT_06_TEXT.Properties.ReadOnly = Status
        chkURINARIA_01.Properties.ReadOnly = Status
        chkURINARIA_02.Properties.ReadOnly = Status
        txtURINARIA_02_TEXT.Properties.ReadOnly = Status
        chkURINARIA_03.Properties.ReadOnly = Status
        txtURINARIA_03_TEXT.Properties.ReadOnly = Status
        chkURINARIA_04.Properties.ReadOnly = Status
        chkURINARIA_05.Properties.ReadOnly = Status
        txtURINARIA_05_TEXT.Properties.ReadOnly = Status
        chkMATA_01.Properties.ReadOnly = Status
        chkMATA_02.Properties.ReadOnly = Status
        chkMATA_03.Properties.ReadOnly = Status
        chkMATA_04.Properties.ReadOnly = Status
        txtMATA_04_TEXT.Properties.ReadOnly = Status
        chkOTOT_SENDI_TULANG_01.Properties.ReadOnly = Status
        chkOTOT_SENDI_TULANG_02.Properties.ReadOnly = Status
        chkOTOT_SENDI_TULANG_03.Properties.ReadOnly = Status
        chkOTOT_SENDI_TULANG_04.Properties.ReadOnly = Status
        txtOTOT_SENDI_TULANG_04_TEXT.Properties.ReadOnly = Status
        chkMULUT_01.Properties.ReadOnly = Status
        chkMULUT_02.Properties.ReadOnly = Status
        txtMULUT_02_TEXT.Properties.ReadOnly = Status
        chkMULUT_03.Properties.ReadOnly = Status
        txtMULUT_03_TEXT.Properties.ReadOnly = Status
        chkTELINGA_01.Properties.ReadOnly = Status
        chkTELINGA_02.Properties.ReadOnly = Status
        txtTELINGA_02_TEXT.Properties.ReadOnly = Status
        chkTELINGA_03.Properties.ReadOnly = Status
        txtTELINGA_03_TEXT.Properties.ReadOnly = Status
        chkTENGGOROKAN_01.Properties.ReadOnly = Status
        chkTENGGOROKAN_02.Properties.ReadOnly = Status
        txtTENGGOROKAN_02_TEXT.Properties.ReadOnly = Status
        chkTENGGOROKAN_03.Properties.ReadOnly = Status
        txtTENGGOROKAN_03_TEXT.Properties.ReadOnly = Status
        chkTENGGOROKAN_04.Properties.ReadOnly = Status
        chkTENGGOROKAN_05.Properties.ReadOnly = Status
        chkTENGGOROKAN_06.Properties.ReadOnly = Status
        txtTENGGOROKAN_06_TEXT.Properties.ReadOnly = Status
        chkTENGGOROKAN_07.Properties.ReadOnly = Status
        txtTENGGOROKAN_07_TEXT.Properties.ReadOnly = Status
        chkHIDUNG_MUKA_01.Properties.ReadOnly = Status
        chkHIDUNG_MUKA_02.Properties.ReadOnly = Status
        txtHIDUNG_MUKA_02_TEXT.Properties.ReadOnly = Status
        chkHIDUNG_MUKA_03.Properties.ReadOnly = Status
        txtHIDUNG_MUKA_03_TEXT.Properties.ReadOnly = Status
        chkTINGKAT_PERKEMBANGAN_01.Properties.ReadOnly = Status
        chkTINGKAT_PERKEMBANGAN_02.Properties.ReadOnly = Status
        chkTINGKAT_PERKEMBANGAN_03.Properties.ReadOnly = Status
        chkTINGKAT_PERKEMBANGAN_04.Properties.ReadOnly = Status
        chkRIWAYAT_KEHAMILAN_KELAHIRAN_01.Properties.ReadOnly = Status
        chkRIWAYAT_KEHAMILAN_KELAHIRAN_02.Properties.ReadOnly = Status
        txtRIWAYAT_KEHAMILAN_KELAHIRAN_02_TEXT.Properties.ReadOnly = Status
        chkRIWAYAT_KEHAMILAN_KELAHIRAN_03.Properties.ReadOnly = Status
        txtRIWAYAT_KEHAMILAN_KELAHIRAN_03_TEXT.Properties.ReadOnly = Status
        chkSKRINING_GIZI_01_1.Properties.ReadOnly = Status
        chkSKRINING_GIZI_01_2.Properties.ReadOnly = Status
        chkSKRINING_GIZI_02_1.Properties.ReadOnly = Status
        chkSKRINING_GIZI_02_2.Properties.ReadOnly = Status
        chkSKRINING_GIZI_03_1.Properties.ReadOnly = Status
        chkSKRINING_GIZI_03_2.Properties.ReadOnly = Status
        chkSKRINING_RESIKO_JATUH_01_1.Properties.ReadOnly = Status
        chkSKRINING_RESIKO_JATUH_01_2.Properties.ReadOnly = Status
        chkSKRINING_RESIKO_JATUH_02_1.Properties.ReadOnly = Status
        chkSKRINING_RESIKO_JATUH_02_2.Properties.ReadOnly = Status
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
        chkSKALANEYRI_KRONIS_01.Properties.ReadOnly = Status
        txtSKALANEYRI_LOKASI_TEXT.Properties.ReadOnly = Status
        txtSKALANEYRI_ISTIRAHAT_TEXT.Properties.ReadOnly = Status
        txtSKALANEYRI_AKTIVITAS_TEXT.Properties.ReadOnly = Status
        txtSKALANEYRI_AKUT_LOKASI_TEXT.Properties.ReadOnly = Status
        txtSKALANEYRI_AKUT_ISTIRAHAT_TEXT.Properties.ReadOnly = Status
        txtSKALANEYRI_AKUT_AKTIVITAS_TEXT.Properties.ReadOnly = Status
        txtKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN.Properties.ReadOnly = Status
        chkEDUKASI_01.Properties.ReadOnly = Status
        chkEDUKASI_02.Properties.ReadOnly = Status
        chkEDUKASI_03.Properties.ReadOnly = Status
        txtEDUKASI_03_TEXT.Properties.ReadOnly = Status
        chkBICARA_SEHARI2_01.Properties.ReadOnly = Status
        chkBICARA_SEHARI2_01_1.Properties.ReadOnly = Status
        chkBICARA_SEHARI2_01_2.Properties.ReadOnly = Status
        chkBICARA_SEHARI2_02.Properties.ReadOnly = Status
        chkBICARA_SEHARI2_02_1.Properties.ReadOnly = Status
        chkBICARA_SEHARI2_02_2.Properties.ReadOnly = Status
        chkBICARA_SEHARI2_03.Properties.ReadOnly = Status
        txtBICARA_SEHARI2_03_TEXT.Properties.ReadOnly = Status
        chkBICARA_SEHARI2_04.Properties.ReadOnly = Status
        txtBICARA_SEHARI2_04_TEXT.Properties.ReadOnly = Status
        chkPERLU_PENERJEMAH_01.Properties.ReadOnly = Status
        chkPERLU_PENERJEMAH_02.Properties.ReadOnly = Status
        chkBAHASA_ISYARAT_01.Properties.ReadOnly = Status
        chkBAHASA_ISYARAT_02.Properties.ReadOnly = Status
        chkHAMBATAN_EDUKASI_01.Properties.ReadOnly = Status
        chkHAMBATAN_EDUKASI_02.Properties.ReadOnly = Status
        chkHAMBATAN_EDUKASI_03.Properties.ReadOnly = Status
        chkHAMBATAN_EDUKASI_04.Properties.ReadOnly = Status
        chkHAMBATAN_EDUKASI_05.Properties.ReadOnly = Status
        chkHAMBATAN_EDUKASI_06.Properties.ReadOnly = Status
        chkHAMBATAN_EDUKASI_07.Properties.ReadOnly = Status
        chkHAMBATAN_EDUKASI_08.Properties.ReadOnly = Status
        chkHAMBATAN_EDUKASI_09.Properties.ReadOnly = Status
        chkHAMBATAN_EDUKASI_10.Properties.ReadOnly = Status
        chkHAMBATAN_EDUKASI_11.Properties.ReadOnly = Status
        chkHAMBATAN_EDUKASI_12.Properties.ReadOnly = Status
        txtUSIA_KEHAMILAN.Properties.ReadOnly = Status
        txtBERAT_BADAN_LAHIR.Properties.ReadOnly = Status
        chkPERSALINAN_01.Properties.ReadOnly = Status
        chkPERSALINAN_02.Properties.ReadOnly = Status
        chkPERSALINAN_03.Properties.ReadOnly = Status
        chkPERSALINAN_04.Properties.ReadOnly = Status
        chkMENANGIS_01.Properties.ReadOnly = Status
        chkMENANGIS_02.Properties.ReadOnly = Status
        txtMENANGIS_03_TEXT.Properties.ReadOnly = Status
        chkJAUNDICE_01.Properties.ReadOnly = Status
        chkJAUNDICE_02.Properties.ReadOnly = Status
        txtRIWAYAT_KELAHIRAN_LAIN_LAIN.Properties.ReadOnly = Status
        chkSOSIAL_01.Properties.ReadOnly = Status
        chkSOSIAL_02.Properties.ReadOnly = Status
        chkSOSIAL_03.Properties.ReadOnly = Status
        chkSOSIAL_04.Properties.ReadOnly = Status
        chkSOSIAL_05.Properties.ReadOnly = Status
        chkSOSIAL_06.Properties.ReadOnly = Status
        chkSOSIAL_07.Properties.ReadOnly = Status
        chkSOSIAL_08.Properties.ReadOnly = Status
        chkSOSIAL_09.Properties.ReadOnly = Status
        chkSOSIAL_10.Properties.ReadOnly = Status
        chkMOTORIK_HALUS_01.Properties.ReadOnly = Status
        chkMOTORIK_HALUS_02.Properties.ReadOnly = Status
        chkMOTORIK_HALUS_03.Properties.ReadOnly = Status
        chkMOTORIK_HALUS_04.Properties.ReadOnly = Status
        chkMOTORIK_HALUS_05.Properties.ReadOnly = Status
        chkMOTORIK_HALUS_06.Properties.ReadOnly = Status
        chkMOTORIK_HALUS_07.Properties.ReadOnly = Status
        chkMOTORIK_HALUS_08.Properties.ReadOnly = Status
        chkMOTORIK_HALUS_09.Properties.ReadOnly = Status
        chkMOTORIK_KASAR_01.Properties.ReadOnly = Status
        chkMOTORIK_KASAR_02.Properties.ReadOnly = Status
        chkMOTORIK_KASAR_03.Properties.ReadOnly = Status
        chkMOTORIK_KASAR_04.Properties.ReadOnly = Status
        chkMOTORIK_KASAR_05.Properties.ReadOnly = Status
        chkMOTORIK_KASAR_06.Properties.ReadOnly = Status
        chkMOTORIK_KASAR_07.Properties.ReadOnly = Status
        chkMOTORIK_KASAR_08.Properties.ReadOnly = Status
        chkMOTORIK_KASAR_09.Properties.ReadOnly = Status
        chkBAHASA_01.Properties.ReadOnly = Status
        chkBAHASA_02.Properties.ReadOnly = Status
        chkBAHASA_03.Properties.ReadOnly = Status
        chkBAHASA_04.Properties.ReadOnly = Status
        chkBAHASA_05.Properties.ReadOnly = Status
        chkBAHASA_06.Properties.ReadOnly = Status
        chkBAHASA_07.Properties.ReadOnly = Status
        chkBAHASA_08.Properties.ReadOnly = Status
        chkBAHASA_09.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_01.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_02.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_02_1.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_02_2.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_02_3.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_02_4.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_02_5.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_02_6.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_02_7.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_02_8.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_02_9.Properties.ReadOnly = Status
        txtMASALAH_KEPERAWATAN_02_9_TEXT.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_03.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_03_1.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_03_2.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_03_3.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_03_4.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_03_5.Properties.ReadOnly = Status
        txtMASALAH_KEPERAWATAN_03_5_TEXT.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_04.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_04_1.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_04_2.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_04_3.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_04_4.Properties.ReadOnly = Status
        txtMASALAH_KEPERAWATAN_04_4_TEXT.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_05.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_05_1.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_05_2.Properties.ReadOnly = Status
        txtMASALAH_KEPERAWATAN_05_2_TEXT.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_06.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_06_1.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_06_2.Properties.ReadOnly = Status
        txtMASALAH_KEPERAWATAN_06_2_TEXT.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_07.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_07_1.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_07_2.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_07_3.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_07_4.Properties.ReadOnly = Status
        txtMASALAH_KEPERAWATAN_07_4_TEXT.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_08.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_08_1.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_08_2.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_08_3.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_08_4.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_08_5.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_08_6.Properties.ReadOnly = Status
        txtMASALAH_KEPERAWATAN_08_6_TEXT.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_09.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_09_1.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_09_2.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_09_3.Properties.ReadOnly = Status
        txtMASALAH_KEPERAWATAN_09_3_TEXT.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_10.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_10_1.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_10_2.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_10_3.Properties.ReadOnly = Status
        txtMASALAH_KEPERAWATAN_10_3_TEXT.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_11.Properties.ReadOnly = Status
        txtMASALAH_KEPERAWATAN_11_TEXT.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_12_1.Properties.ReadOnly = Status
        txtMASALAH_KEPERAWATAN_12_1_TEXT.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_12_2.Properties.ReadOnly = Status
        txtMASALAH_KEPERAWATAN_12_2_TEXT.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_13.Properties.ReadOnly = Status
        txtMASALAH_KEPERAWATAN_13_TEXT.Properties.ReadOnly = Status
        txtCATATAN_KEPERAWATAN_JAM.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_01_1.Properties.ReadOnly = Status
        txtINTERVENSI_IMPLEMENTASI_01_TD.Properties.ReadOnly = Status
        txtINTERVENSI_IMPLEMENTASI_01_N.Properties.ReadOnly = Status
        txtINTERVENSI_IMPLEMENTASI_01_S.Properties.ReadOnly = Status
        txtINTERVENSI_IMPLEMENTASI_01_R.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_01_2.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_01_3.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_01_4.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_01_5.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_01_6.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_01_7.Properties.ReadOnly = Status
        txtINTERVENSI_IMPLEMENTASI_01_7_TEXT_1.Properties.ReadOnly = Status
        txtINTERVENSI_IMPLEMENTASI_01_7_TEXT_2.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_01_8.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_01_9.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_02_1.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_02_2.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_02_3.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_02_4.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_02_5.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_02_6.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_02_7.Properties.ReadOnly = Status
        txtINTERVENSI_IMPLEMENTASI_02_7_TEXT.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_03_1.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_03_2.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_03_3.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_03_4.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_03_5.Properties.ReadOnly = Status
        txtINTERVENSI_IMPLEMENTASI_03_5_TEXT.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_03_6.Properties.ReadOnly = Status
        txtINTERVENSI_IMPLEMENTASI_03_6_TEXT.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_04_1.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_04_2.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_04_3.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_04_4.Properties.ReadOnly = Status
        txtINTERVENSI_IMPLEMENTASI_04_4_TEXT.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_04_5.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_04_6.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_04_7.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_04_8.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_04_9.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_05_1.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_05_2.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_05_3.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_05_4.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_05_5.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_05_6.Properties.ReadOnly = Status
        txtINTERVENSI_IMPLEMENTASI_05_6_TEXT.Properties.ReadOnly = Status
        chkHASIL_PENANGANAN_01.Properties.ReadOnly = Status
        chkHASIL_PENANGANAN_02.Properties.ReadOnly = Status
        chkHASIL_PENANGANAN_03.Properties.ReadOnly = Status
        chkHASIL_PENANGANAN_04.Properties.ReadOnly = Status
        chkHASIL_PENANGANAN_05.Properties.ReadOnly = Status
        chkREPRODUKSI_01_1.Properties.ReadOnly = Status
        chkREPRODUKSI_01_2.Properties.ReadOnly = Status
        chkREPRODUKSI_02_1.Properties.ReadOnly = Status
        chkREPRODUKSI_02_2.Properties.ReadOnly = Status
        chkUMUR_01.Properties.ReadOnly = Status
        chkUMUR_02.Properties.ReadOnly = Status
        chkUMUR_03.Properties.ReadOnly = Status
        chkUMUR_04.Properties.ReadOnly = Status
        chkUMUR_05.Properties.ReadOnly = Status
        chkUMUR_06.Properties.ReadOnly = Status
        chkUMUR_07.Properties.ReadOnly = Status
        chkUMUR_08.Properties.ReadOnly = Status
        chkUMUR_09.Properties.ReadOnly = Status
        chkUMUR_10.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_01_AKTUAL.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_01_POTENSIAL.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_02_AKTUAL.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_02_POTENSIAL.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_03_AKTUAL.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_03_POTENSIAL.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_04_AKTUAL.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_04_POTENSIAL.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_05_AKTUAL.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_05_POTENSIAL.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_06_AKTUAL.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_06_POTENSIAL.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_07_AKTUAL.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_07_POTENSIAL.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_08_AKTUAL.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_08_POTENSIAL.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_09_AKTUAL.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_09_POTENSIAL.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_10_AKTUAL.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_10_POTENSIAL.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_10_RESIKO.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_08_NYERI.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_08_PUSING.Properties.ReadOnly = Status
        chkMASALAH_KEPERAWATAN_08_GATAL.Properties.ReadOnly = Status
        chkTANDA_VITAL_REGULER.Properties.ReadOnly = Status
        chkTANDA_VITAL_IREGULER.Properties.ReadOnly = Status
        chkREPRODUKSI_03_1.Properties.ReadOnly = Status
        chkREPRODUKSI_03_2.Properties.ReadOnly = Status
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01.Properties.ReadOnly = Status
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02.Properties.ReadOnly = Status
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03.Properties.ReadOnly = Status
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04.Properties.ReadOnly = Status

    End Sub
    Private Sub fn_EmptyMe()
        'txtCODE.Text = "<--- AUTO --->"

        deDATE.DateTime = Now
        chkAUTO.Checked = False
        chkALLO.Checked = False
        txtNAMAPENGKAJIAN.ResetText()
        txtKELUHAN_UTAMA.ResetText()
        txtKESADARAN_UMUM.ResetText()
        txt_GCS.ResetText()
        cbo_E.ResetText()
        cbo_M.ResetText()
        cbo_V.ResetText()
        chkKEADAAN_EMOSIONAL_01.Checked = False
        chkKEADAAN_EMOSIONAL_02.Checked = False
        chkKEADAAN_EMOSIONAL_03.Checked = False
        chkKEADAAN_EMOSIONAL_04.Checked = False
        txtKEADAAN_EMOSIONAL_04_TEXT.ResetText()
        txtTANDA_VITAL_01.ResetText()
        txtTANDA_VITAL_02.ResetText()
        txtTANDA_VITAL_03.ResetText()
        txtTANDA_VITAL_04.ResetText()
        txtTANDA_VITAL_05.ResetText()
        txtTANDA_VITAL_06.ResetText()
        txtTANDA_VITAL_07.ResetText()
        txtTANDA_VITAL_08.ResetText()
        chkPERSYARAFAN_01.Checked = False
        chkPERSYARAFAN_02.Checked = False
        chkPERSYARAFAN_03.Checked = False
        chkPERSYARAFAN_04.Checked = False
        chkPERSYARAFAN_05.Checked = False
        chkPERSYARAFAN_06.Checked = False
        txtPERSYARAFAN_06_TEXT.ResetText()
        chkPERNAPASAN_01.Checked = False
        chkPERNAPASAN_02.Checked = False
        chkPERNAPASAN_03.Checked = False
        chkPERNAPASAN_04.Checked = False
        txtPERNAPASAN_04_TEXT.ResetText()
        chkPENCERNAAN_01.Checked = False
        chkPENCERNAAN_02.Checked = False
        chkPENCERNAAN_03.Checked = False
        chkPENCERNAAN_04.Checked = False
        chkPENCERNAAN_05.Checked = False
        txtPENCERNAAN_05_TEXT.ResetText()
        chkENDROKIN_01.Checked = False
        chkENDROKIN_02.Checked = False
        chkENDROKIN_03.Checked = False
        chkENDROKIN_04.Checked = False
        txtENDROKIN_04_TEXT.ResetText()
        chkCARDIOVASKULER_01.Checked = False
        chkCARDIOVASKULER_02.Checked = False
        chkCARDIOVASKULER_03.Checked = False
        chkCARDIOVASKULER_04.Checked = False
        txtCARDIOVASKULER_04_TEXT.ResetText()
        chkABDOMEN_01.Checked = False
        chkABDOMEN_02.Checked = False
        chkABDOMEN_03.Checked = False
        chkABDOMEN_04.Checked = False
        chkABDOMEN_05.Checked = False
        txtABDOMEN_05_TEXT.ResetText()
        chkABDOMEN_06.Checked = False
        txtABDOMEN_06_TEXT.ResetText()
        chkABDOMEN_07.Checked = False
        txtABDOMEN_07_TEXT.ResetText()
        chkABDOMEN_08.Checked = False
        txtABDOMEN_08_TEXT.ResetText()
        chkABDOMEN_09.Checked = False
        txtABDOMEN_09_TEXT.ResetText()
        chkREPRODUKSI_01.Checked = False
        chkREPRODUKSI_02.Checked = False
        chkREPRODUKSI_03.Checked = False
        chkREPRODUKSI_04.Checked = False
        chkREPRODUKSI_05.Checked = False
        txtREPRODUKSI_01_TEXT.ResetText()
        txtREPRODUKSI_02_TEXT.ResetText()
        txtREPRODUKSI_03_TEXT.ResetText()
        txtREPRODUKSI_04_TEXT.ResetText()
        txtREPRODUKSI_05_TEXT.ResetText()
        txtREPRODUKSI_06_TEXT.ResetText()
        txtREPRODUKSI_07_TEXT.ResetText()
        chkKULIT_01.Checked = False
        chkKULIT_02.Checked = False
        txtKULIT_02_TEXT.ResetText()
        chkKULIT_03.Checked = False
        txtKULIT_03_TEXT.ResetText()
        chkKULIT_04.Checked = False
        chkKULIT_05.Checked = False
        chkKULIT_06.Checked = False
        txtKULIT_06_TEXT.ResetText()
        chkURINARIA_01.Checked = False
        chkURINARIA_02.Checked = False
        txtURINARIA_02_TEXT.ResetText()
        chkURINARIA_03.Checked = False
        txtURINARIA_03_TEXT.ResetText()
        chkURINARIA_04.Checked = False
        chkURINARIA_05.Checked = False
        txtURINARIA_05_TEXT.ResetText()
        chkMATA_01.Checked = False
        chkMATA_02.Checked = False
        chkMATA_03.Checked = False
        chkMATA_04.Checked = False
        txtMATA_04_TEXT.ResetText()
        chkOTOT_SENDI_TULANG_01.Checked = False
        chkOTOT_SENDI_TULANG_02.Checked = False
        chkOTOT_SENDI_TULANG_03.Checked = False
        chkOTOT_SENDI_TULANG_04.Checked = False
        txtOTOT_SENDI_TULANG_04_TEXT.ResetText()
        chkMULUT_01.Checked = False
        chkMULUT_02.Checked = False
        txtMULUT_02_TEXT.ResetText()
        chkMULUT_03.Checked = False
        txtMULUT_03_TEXT.ResetText()
        chkTELINGA_01.Checked = False
        chkTELINGA_02.Checked = False
        txtTELINGA_02_TEXT.ResetText()
        chkTELINGA_03.Checked = False
        txtTELINGA_03_TEXT.ResetText()
        chkTENGGOROKAN_01.Checked = False
        chkTENGGOROKAN_02.Checked = False
        txtTENGGOROKAN_02_TEXT.ResetText()
        chkTENGGOROKAN_03.Checked = False
        txtTENGGOROKAN_03_TEXT.ResetText()
        chkTENGGOROKAN_04.Checked = False
        chkTENGGOROKAN_05.Checked = False
        chkTENGGOROKAN_06.Checked = False
        txtTENGGOROKAN_06_TEXT.ResetText()
        chkTENGGOROKAN_07.Checked = False
        txtTENGGOROKAN_07_TEXT.ResetText()
        chkHIDUNG_MUKA_01.Checked = False
        chkHIDUNG_MUKA_02.Checked = False
        txtHIDUNG_MUKA_02_TEXT.ResetText()
        chkHIDUNG_MUKA_03.Checked = False
        txtHIDUNG_MUKA_03_TEXT.ResetText()
        chkTINGKAT_PERKEMBANGAN_01.Checked = False
        chkTINGKAT_PERKEMBANGAN_02.Checked = False
        chkTINGKAT_PERKEMBANGAN_03.Checked = False
        chkTINGKAT_PERKEMBANGAN_04.Checked = False
        chkRIWAYAT_KEHAMILAN_KELAHIRAN_01.Checked = False
        chkRIWAYAT_KEHAMILAN_KELAHIRAN_02.Checked = False
        txtRIWAYAT_KEHAMILAN_KELAHIRAN_02_TEXT.ResetText()
        chkRIWAYAT_KEHAMILAN_KELAHIRAN_03.Checked = False
        txtRIWAYAT_KEHAMILAN_KELAHIRAN_03_TEXT.ResetText()
        chkSKRINING_GIZI_01_1.Checked = False
        chkSKRINING_GIZI_01_2.Checked = False
        chkSKRINING_GIZI_02_1.Checked = False
        chkSKRINING_GIZI_02_2.Checked = False
        chkSKRINING_GIZI_03_1.Checked = False
        chkSKRINING_GIZI_03_2.Checked = False
        chkSKRINING_RESIKO_JATUH_01_1.Checked = False
        chkSKRINING_RESIKO_JATUH_01_2.Checked = False
        chkSKRINING_RESIKO_JATUH_02_1.Checked = False
        chkSKRINING_RESIKO_JATUH_02_2.Checked = False
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
        chkSKALANEYRI_KRONIS_01.Checked = False
        txtSKALANEYRI_LOKASI_TEXT.ResetText()
        txtSKALANEYRI_ISTIRAHAT_TEXT.ResetText()
        txtSKALANEYRI_AKTIVITAS_TEXT.ResetText()
        txtSKALANEYRI_AKUT_LOKASI_TEXT.ResetText()
        txtSKALANEYRI_AKUT_ISTIRAHAT_TEXT.ResetText()
        txtSKALANEYRI_AKUT_AKTIVITAS_TEXT.ResetText()
        txtKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN.ResetText()
        chkEDUKASI_01.Checked = False
        chkEDUKASI_02.Checked = False
        chkEDUKASI_03.Checked = False
        txtEDUKASI_03_TEXT.ResetText()
        chkBICARA_SEHARI2_01.Checked = False
        chkBICARA_SEHARI2_01_1.Checked = False
        chkBICARA_SEHARI2_01_2.Checked = False
        chkBICARA_SEHARI2_02.Checked = False
        chkBICARA_SEHARI2_02_1.Checked = False
        chkBICARA_SEHARI2_02_2.Checked = False
        chkBICARA_SEHARI2_03.Checked = False
        txtBICARA_SEHARI2_03_TEXT.ResetText()
        chkBICARA_SEHARI2_04.Checked = False
        txtBICARA_SEHARI2_04_TEXT.ResetText()
        chkPERLU_PENERJEMAH_01.Checked = False
        chkPERLU_PENERJEMAH_02.Checked = False
        chkBAHASA_ISYARAT_01.Checked = False
        chkBAHASA_ISYARAT_02.Checked = False
        chkHAMBATAN_EDUKASI_01.Checked = False
        chkHAMBATAN_EDUKASI_02.Checked = False
        chkHAMBATAN_EDUKASI_03.Checked = False
        chkHAMBATAN_EDUKASI_04.Checked = False
        chkHAMBATAN_EDUKASI_05.Checked = False
        chkHAMBATAN_EDUKASI_06.Checked = False
        chkHAMBATAN_EDUKASI_07.Checked = False
        chkHAMBATAN_EDUKASI_08.Checked = False
        chkHAMBATAN_EDUKASI_09.Checked = False
        chkHAMBATAN_EDUKASI_10.Checked = False
        chkHAMBATAN_EDUKASI_11.Checked = False
        chkHAMBATAN_EDUKASI_12.Checked = False
        txtUSIA_KEHAMILAN.ResetText()
        txtBERAT_BADAN_LAHIR.ResetText()
        chkPERSALINAN_01.Checked = False
        chkPERSALINAN_02.Checked = False
        chkPERSALINAN_03.Checked = False
        chkPERSALINAN_04.Checked = False
        chkMENANGIS_01.Checked = False
        chkMENANGIS_02.Checked = False
        txtMENANGIS_03_TEXT.ResetText()
        chkJAUNDICE_01.Checked = False
        chkJAUNDICE_02.Checked = False
        txtRIWAYAT_KELAHIRAN_LAIN_LAIN.ResetText()
        chkSOSIAL_01.Checked = False
        chkSOSIAL_02.Checked = False
        chkSOSIAL_03.Checked = False
        chkSOSIAL_04.Checked = False
        chkSOSIAL_05.Checked = False
        chkSOSIAL_06.Checked = False
        chkSOSIAL_07.Checked = False
        chkSOSIAL_08.Checked = False
        chkSOSIAL_09.Checked = False
        chkSOSIAL_10.Checked = False
        chkMOTORIK_HALUS_01.Checked = False
        chkMOTORIK_HALUS_02.Checked = False
        chkMOTORIK_HALUS_03.Checked = False
        chkMOTORIK_HALUS_04.Checked = False
        chkMOTORIK_HALUS_05.Checked = False
        chkMOTORIK_HALUS_06.Checked = False
        chkMOTORIK_HALUS_07.Checked = False
        chkMOTORIK_HALUS_08.Checked = False
        chkMOTORIK_HALUS_09.Checked = False
        chkMOTORIK_KASAR_01.Checked = False
        chkMOTORIK_KASAR_02.Checked = False
        chkMOTORIK_KASAR_03.Checked = False
        chkMOTORIK_KASAR_04.Checked = False
        chkMOTORIK_KASAR_05.Checked = False
        chkMOTORIK_KASAR_06.Checked = False
        chkMOTORIK_KASAR_07.Checked = False
        chkMOTORIK_KASAR_08.Checked = False
        chkMOTORIK_KASAR_09.Checked = False
        chkBAHASA_01.Checked = False
        chkBAHASA_02.Checked = False
        chkBAHASA_03.Checked = False
        chkBAHASA_04.Checked = False
        chkBAHASA_05.Checked = False
        chkBAHASA_06.Checked = False
        chkBAHASA_07.Checked = False
        chkBAHASA_08.Checked = False
        chkBAHASA_09.Checked = False
        chkMASALAH_KEPERAWATAN_01.Checked = False
        chkMASALAH_KEPERAWATAN_02.Checked = False
        chkMASALAH_KEPERAWATAN_02_1.Checked = False
        chkMASALAH_KEPERAWATAN_02_2.Checked = False
        chkMASALAH_KEPERAWATAN_02_3.Checked = False
        chkMASALAH_KEPERAWATAN_02_4.Checked = False
        chkMASALAH_KEPERAWATAN_02_5.Checked = False
        chkMASALAH_KEPERAWATAN_02_6.Checked = False
        chkMASALAH_KEPERAWATAN_02_7.Checked = False
        chkMASALAH_KEPERAWATAN_02_8.Checked = False
        chkMASALAH_KEPERAWATAN_02_9.Checked = False
        txtMASALAH_KEPERAWATAN_02_9_TEXT.ResetText()
        chkMASALAH_KEPERAWATAN_03.Checked = False
        chkMASALAH_KEPERAWATAN_03_1.Checked = False
        chkMASALAH_KEPERAWATAN_03_2.Checked = False
        chkMASALAH_KEPERAWATAN_03_3.Checked = False
        chkMASALAH_KEPERAWATAN_03_4.Checked = False
        chkMASALAH_KEPERAWATAN_03_5.Checked = False
        txtMASALAH_KEPERAWATAN_03_5_TEXT.ResetText()
        chkMASALAH_KEPERAWATAN_04.Checked = False
        chkMASALAH_KEPERAWATAN_04_1.Checked = False
        chkMASALAH_KEPERAWATAN_04_2.Checked = False
        chkMASALAH_KEPERAWATAN_04_3.Checked = False
        chkMASALAH_KEPERAWATAN_04_4.Checked = False
        txtMASALAH_KEPERAWATAN_04_4_TEXT.ResetText()
        chkMASALAH_KEPERAWATAN_05.Checked = False
        chkMASALAH_KEPERAWATAN_05_1.Checked = False
        chkMASALAH_KEPERAWATAN_05_2.Checked = False
        txtMASALAH_KEPERAWATAN_05_2_TEXT.ResetText()
        chkMASALAH_KEPERAWATAN_06.Checked = False
        chkMASALAH_KEPERAWATAN_06_1.Checked = False
        chkMASALAH_KEPERAWATAN_06_2.Checked = False
        txtMASALAH_KEPERAWATAN_06_2_TEXT.ResetText()
        chkMASALAH_KEPERAWATAN_07.Checked = False
        chkMASALAH_KEPERAWATAN_07_1.Checked = False
        chkMASALAH_KEPERAWATAN_07_2.Checked = False
        chkMASALAH_KEPERAWATAN_07_3.Checked = False
        chkMASALAH_KEPERAWATAN_07_4.Checked = False
        txtMASALAH_KEPERAWATAN_07_4_TEXT.ResetText()
        chkMASALAH_KEPERAWATAN_08.Checked = False
        chkMASALAH_KEPERAWATAN_08_1.Checked = False
        chkMASALAH_KEPERAWATAN_08_2.Checked = False
        chkMASALAH_KEPERAWATAN_08_3.Checked = False
        chkMASALAH_KEPERAWATAN_08_4.Checked = False
        chkMASALAH_KEPERAWATAN_08_5.Checked = False
        chkMASALAH_KEPERAWATAN_08_6.Checked = False
        txtMASALAH_KEPERAWATAN_08_6_TEXT.ResetText()
        chkMASALAH_KEPERAWATAN_09.Checked = False
        chkMASALAH_KEPERAWATAN_09_1.Checked = False
        chkMASALAH_KEPERAWATAN_09_2.Checked = False
        chkMASALAH_KEPERAWATAN_09_3.Checked = False
        txtMASALAH_KEPERAWATAN_09_3_TEXT.ResetText()
        chkMASALAH_KEPERAWATAN_10.Checked = False
        chkMASALAH_KEPERAWATAN_10_1.Checked = False
        chkMASALAH_KEPERAWATAN_10_2.Checked = False
        chkMASALAH_KEPERAWATAN_10_3.Checked = False
        txtMASALAH_KEPERAWATAN_10_3_TEXT.ResetText()
        chkMASALAH_KEPERAWATAN_11.Checked = False
        txtMASALAH_KEPERAWATAN_11_TEXT.ResetText()
        chkMASALAH_KEPERAWATAN_12_1.Checked = False
        txtMASALAH_KEPERAWATAN_12_1_TEXT.ResetText()
        chkMASALAH_KEPERAWATAN_12_2.Checked = False
        txtMASALAH_KEPERAWATAN_12_2_TEXT.ResetText()
        chkMASALAH_KEPERAWATAN_13.Checked = False
        txtMASALAH_KEPERAWATAN_13_TEXT.ResetText()
        txtCATATAN_KEPERAWATAN_JAM.ResetText()
        chkINTERVENSI_IMPLEMENTASI_01_1.Checked = False
        txtINTERVENSI_IMPLEMENTASI_01_TD.ResetText()
        txtINTERVENSI_IMPLEMENTASI_01_N.ResetText()
        txtINTERVENSI_IMPLEMENTASI_01_S.ResetText()
        txtINTERVENSI_IMPLEMENTASI_01_R.ResetText()
        chkINTERVENSI_IMPLEMENTASI_01_2.Checked = False
        chkINTERVENSI_IMPLEMENTASI_01_3.Checked = False
        chkINTERVENSI_IMPLEMENTASI_01_4.Checked = False
        chkINTERVENSI_IMPLEMENTASI_01_5.Checked = False
        chkINTERVENSI_IMPLEMENTASI_01_6.Checked = False
        chkINTERVENSI_IMPLEMENTASI_01_7.Checked = False
        txtINTERVENSI_IMPLEMENTASI_01_7_TEXT_1.ResetText()
        txtINTERVENSI_IMPLEMENTASI_01_7_TEXT_2.ResetText()
        chkINTERVENSI_IMPLEMENTASI_01_8.Checked = False
        chkINTERVENSI_IMPLEMENTASI_01_9.Checked = False
        chkINTERVENSI_IMPLEMENTASI_02_1.Checked = False
        chkINTERVENSI_IMPLEMENTASI_02_2.Checked = False
        chkINTERVENSI_IMPLEMENTASI_02_3.Checked = False
        chkINTERVENSI_IMPLEMENTASI_02_4.Checked = False
        chkINTERVENSI_IMPLEMENTASI_02_5.Checked = False
        chkINTERVENSI_IMPLEMENTASI_02_6.Checked = False
        chkINTERVENSI_IMPLEMENTASI_02_7.Checked = False
        txtINTERVENSI_IMPLEMENTASI_02_7_TEXT.ResetText()
        chkINTERVENSI_IMPLEMENTASI_03_1.Checked = False
        chkINTERVENSI_IMPLEMENTASI_03_2.Checked = False
        chkINTERVENSI_IMPLEMENTASI_03_3.Checked = False
        chkINTERVENSI_IMPLEMENTASI_03_4.Checked = False
        chkINTERVENSI_IMPLEMENTASI_03_5.Checked = False
        txtINTERVENSI_IMPLEMENTASI_03_5_TEXT.ResetText()
        chkINTERVENSI_IMPLEMENTASI_03_6.Checked = False
        txtINTERVENSI_IMPLEMENTASI_03_6_TEXT.ResetText()
        chkINTERVENSI_IMPLEMENTASI_04_1.Checked = False
        chkINTERVENSI_IMPLEMENTASI_04_2.Checked = False
        chkINTERVENSI_IMPLEMENTASI_04_3.Checked = False
        chkINTERVENSI_IMPLEMENTASI_04_4.Checked = False
        txtINTERVENSI_IMPLEMENTASI_04_4_TEXT.ResetText()
        chkINTERVENSI_IMPLEMENTASI_04_5.Checked = False
        chkINTERVENSI_IMPLEMENTASI_04_6.Checked = False
        chkINTERVENSI_IMPLEMENTASI_04_7.Checked = False
        chkINTERVENSI_IMPLEMENTASI_04_8.Checked = False
        chkINTERVENSI_IMPLEMENTASI_04_9.Checked = False
        chkINTERVENSI_IMPLEMENTASI_05_1.Checked = False
        chkINTERVENSI_IMPLEMENTASI_05_2.Checked = False
        chkINTERVENSI_IMPLEMENTASI_05_3.Checked = False
        chkINTERVENSI_IMPLEMENTASI_05_4.Checked = False
        chkINTERVENSI_IMPLEMENTASI_05_5.Checked = False
        chkINTERVENSI_IMPLEMENTASI_05_6.Checked = False
        txtINTERVENSI_IMPLEMENTASI_05_6_TEXT.ResetText()
        chkHASIL_PENANGANAN_01.Checked = False
        chkHASIL_PENANGANAN_02.Checked = False
        chkHASIL_PENANGANAN_03.Checked = False
        chkHASIL_PENANGANAN_04.Checked = False
        chkHASIL_PENANGANAN_05.Checked = False
        chkREPRODUKSI_01_1.Checked = False
        chkREPRODUKSI_01_2.Checked = False
        chkREPRODUKSI_02_1.Checked = False
        chkREPRODUKSI_02_2.Checked = False
        chkUMUR_01.Checked = False
        chkUMUR_02.Checked = False
        chkUMUR_03.Checked = False
        chkUMUR_04.Checked = False
        chkUMUR_05.Checked = False
        chkUMUR_06.Checked = False
        chkUMUR_07.Checked = False
        chkUMUR_08.Checked = False
        chkUMUR_09.Checked = False
        chkUMUR_10.Checked = False
        chkMASALAH_KEPERAWATAN_01_AKTUAL.Checked = False
        chkMASALAH_KEPERAWATAN_01_POTENSIAL.Checked = False
        chkMASALAH_KEPERAWATAN_02_AKTUAL.Checked = False
        chkMASALAH_KEPERAWATAN_02_POTENSIAL.Checked = False
        chkMASALAH_KEPERAWATAN_03_AKTUAL.Checked = False
        chkMASALAH_KEPERAWATAN_03_POTENSIAL.Checked = False
        chkMASALAH_KEPERAWATAN_04_AKTUAL.Checked = False
        chkMASALAH_KEPERAWATAN_04_POTENSIAL.Checked = False
        chkMASALAH_KEPERAWATAN_05_AKTUAL.Checked = False
        chkMASALAH_KEPERAWATAN_05_POTENSIAL.Checked = False
        chkMASALAH_KEPERAWATAN_06_AKTUAL.Checked = False
        chkMASALAH_KEPERAWATAN_06_POTENSIAL.Checked = False
        chkMASALAH_KEPERAWATAN_07_AKTUAL.Checked = False
        chkMASALAH_KEPERAWATAN_07_POTENSIAL.Checked = False
        chkMASALAH_KEPERAWATAN_08_AKTUAL.Checked = False
        chkMASALAH_KEPERAWATAN_08_POTENSIAL.Checked = False
        chkMASALAH_KEPERAWATAN_09_AKTUAL.Checked = False
        chkMASALAH_KEPERAWATAN_09_POTENSIAL.Checked = False
        chkMASALAH_KEPERAWATAN_10_AKTUAL.Checked = False
        chkMASALAH_KEPERAWATAN_10_POTENSIAL.Checked = False
        chkMASALAH_KEPERAWATAN_10_RESIKO.Checked = False
        chkMASALAH_KEPERAWATAN_08_NYERI.Checked = False
        chkMASALAH_KEPERAWATAN_08_PUSING.Checked = False
        chkMASALAH_KEPERAWATAN_08_GATAL.Checked = False
        chkTANDA_VITAL_REGULER.Checked = False
        chkTANDA_VITAL_IREGULER.Checked = False
        chkREPRODUKSI_03_1.Checked = False
        chkREPRODUKSI_03_2.Checked = False
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01.Checked = False
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02.Checked = False
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03.Checked = False
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04.Checked = False

    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RJ_03.GetData(txtNoRegister.Text)

            With ds

                deDATE.DateTime = .DATE
                chkAUTO.Checked = .AUTO
                chkALLO.Checked = .ALLO
                txtNAMAPENGKAJIAN.Text = .NAMA_PENGKAJIAN_FISIK_DAN_ANAMNESIS
                txtKELUHAN_UTAMA.Text = .KELUHAN_UTAMA
                txtKESADARAN_UMUM.Text = .KESADARAN_UMUM
                txt_GCS.Text = .GCS
                cbo_E.Text = .E
                cbo_M.Text = .M
                cbo_V.Text = .V
                chkKEADAAN_EMOSIONAL_01.Checked = .KEADAAN_EMOSIONAL_01
                chkKEADAAN_EMOSIONAL_02.Checked = .KEADAAN_EMOSIONAL_02
                chkKEADAAN_EMOSIONAL_03.Checked = .KEADAAN_EMOSIONAL_03
                chkKEADAAN_EMOSIONAL_04.Checked = .KEADAAN_EMOSIONAL_04
                txtKEADAAN_EMOSIONAL_04_TEXT.Text = .KEADAAN_EMOSIONAL_04_TEXT
                txtTANDA_VITAL_01.Text = .TANDA_VITAL_01
                txtTANDA_VITAL_02.Text = .TANDA_VITAL_02
                txtTANDA_VITAL_03.Text = .TANDA_VITAL_03
                txtTANDA_VITAL_04.Text = .TANDA_VITAL_04
                txtTANDA_VITAL_05.Text = .TANDA_VITAL_05
                txtTANDA_VITAL_06.Text = .TANDA_VITAL_06
                txtTANDA_VITAL_07.Text = .TANDA_VITAL_07
                txtTANDA_VITAL_08.Text = .TANDA_VITAL_08
                chkPERSYARAFAN_01.Checked = .PERSYARAFAN_01
                chkPERSYARAFAN_02.Checked = .PERSYARAFAN_02
                chkPERSYARAFAN_03.Checked = .PERSYARAFAN_03
                chkPERSYARAFAN_04.Checked = .PERSYARAFAN_04
                chkPERSYARAFAN_05.Checked = .PERSYARAFAN_05
                chkPERSYARAFAN_06.Checked = .PERSYARAFAN_06
                txtPERSYARAFAN_06_TEXT.Text = .PERSYARAFAN_06_TEXT
                chkPERNAPASAN_01.Checked = .PERNAPASAN_01
                chkPERNAPASAN_02.Checked = .PERNAPASAN_02
                chkPERNAPASAN_03.Checked = .PERNAPASAN_03
                chkPERNAPASAN_04.Checked = .PERNAPASAN_04
                txtPERNAPASAN_04_TEXT.Text = .PERNAPASAN_04_TEXT
                chkPENCERNAAN_01.Checked = .PENCERNAAN_01
                chkPENCERNAAN_02.Checked = .PENCERNAAN_02
                chkPENCERNAAN_03.Checked = .PENCERNAAN_03
                chkPENCERNAAN_04.Checked = .PENCERNAAN_04
                chkPENCERNAAN_05.Checked = .PENCERNAAN_05
                txtPENCERNAAN_05_TEXT.Text = .PENCERNAAN_05_TEXT
                chkENDROKIN_01.Checked = .ENDROKIN_01
                chkENDROKIN_02.Checked = .ENDROKIN_02
                chkENDROKIN_03.Checked = .ENDROKIN_03
                chkENDROKIN_04.Checked = .ENDROKIN_04
                txtENDROKIN_04_TEXT.Text = .ENDROKIN_04_TEXT
                chkCARDIOVASKULER_01.Checked = .CARDIOVASKULER_01
                chkCARDIOVASKULER_02.Checked = .CARDIOVASKULER_02
                chkCARDIOVASKULER_03.Checked = .CARDIOVASKULER_03
                chkCARDIOVASKULER_04.Checked = .CARDIOVASKULER_04
                txtCARDIOVASKULER_04_TEXT.Text = .CARDIOVASKULER_04_TEXT
                chkABDOMEN_01.Checked = .ABDOMEN_01
                chkABDOMEN_02.Checked = .ABDOMEN_02
                chkABDOMEN_03.Checked = .ABDOMEN_03
                chkABDOMEN_04.Checked = .ABDOMEN_04
                chkABDOMEN_05.Checked = .ABDOMEN_05
                txtABDOMEN_05_TEXT.Text = .ABDOMEN_05_TEXT
                chkABDOMEN_06.Checked = .ABDOMEN_06
                txtABDOMEN_06_TEXT.Text = .ABDOMEN_06_TEXT
                chkABDOMEN_07.Checked = .ABDOMEN_07
                txtABDOMEN_07_TEXT.Text = .ABDOMEN_07_TEXT
                chkABDOMEN_08.Checked = .ABDOMEN_08
                txtABDOMEN_08_TEXT.Text = .ABDOMEN_08_TEXT
                chkABDOMEN_09.Checked = .ABDOMEN_09
                txtABDOMEN_09_TEXT.Text = .ABDOMEN_09_TEXT
                chkREPRODUKSI_01.Checked = .REPRODUKSI_01
                chkREPRODUKSI_02.Checked = .REPRODUKSI_02
                chkREPRODUKSI_03.Checked = .REPRODUKSI_03
                chkREPRODUKSI_04.Checked = .REPRODUKSI_04
                chkREPRODUKSI_05.Checked = .REPRODUKSI_05
                txtREPRODUKSI_01_TEXT.Text = .REPRODUKSI_01_TEXT
                txtREPRODUKSI_02_TEXT.Text = .REPRODUKSI_02_TEXT
                txtREPRODUKSI_03_TEXT.Text = .REPRODUKSI_03_TEXT
                txtREPRODUKSI_04_TEXT.Text = .REPRODUKSI_04_TEXT
                txtREPRODUKSI_05_TEXT.Text = .REPRODUKSI_05_TEXT
                txtREPRODUKSI_06_TEXT.Text = .REPRODUKSI_06_TEXT
                txtREPRODUKSI_07_TEXT.Text = .REPRODUKSI_07_TEXT
                chkKULIT_01.Checked = .KULIT_01
                chkKULIT_02.Checked = .KULIT_02
                txtKULIT_02_TEXT.Text = .KULIT_02_TEXT
                chkKULIT_03.Checked = .KULIT_03
                txtKULIT_03_TEXT.Text = .KULIT_03_TEXT
                chkKULIT_04.Checked = .KULIT_04
                chkKULIT_05.Checked = .KULIT_05
                chkKULIT_06.Checked = .KULIT_06
                txtKULIT_06_TEXT.Text = .KULIT_06_TEXT
                chkURINARIA_01.Checked = .URINARIA_01
                chkURINARIA_02.Checked = .URINARIA_02
                txtURINARIA_02_TEXT.Text = .URINARIA_02_TEXT
                chkURINARIA_03.Checked = .URINARIA_03
                txtURINARIA_03_TEXT.Text = .URINARIA_03_TEXT
                chkURINARIA_04.Checked = .URINARIA_04
                chkURINARIA_05.Checked = .URINARIA_05
                txtURINARIA_05_TEXT.Text = .URINARIA_05_TEXT
                chkMATA_01.Checked = .MATA_01
                chkMATA_02.Checked = .MATA_02
                chkMATA_03.Checked = .MATA_03
                chkMATA_04.Checked = .MATA_04
                txtMATA_04_TEXT.Text = .MATA_04_TEXT
                chkOTOT_SENDI_TULANG_01.Checked = .OTOT_SENDI_TULANG_01
                chkOTOT_SENDI_TULANG_02.Checked = .OTOT_SENDI_TULANG_02
                chkOTOT_SENDI_TULANG_03.Checked = .OTOT_SENDI_TULANG_03
                chkOTOT_SENDI_TULANG_04.Checked = .OTOT_SENDI_TULANG_04
                txtOTOT_SENDI_TULANG_04_TEXT.Text = .OTOT_SENDI_TULANG_04_TEXT
                chkMULUT_01.Checked = .MULUT_01
                chkMULUT_02.Checked = .MULUT_02
                txtMULUT_02_TEXT.Text = .MULUT_02_TEXT
                chkMULUT_03.Checked = .MULUT_03
                txtMULUT_03_TEXT.Text = .MULUT_03_TEXT
                chkTELINGA_01.Checked = .TELINGA_01
                chkTELINGA_02.Checked = .TELINGA_02
                txtTELINGA_02_TEXT.Text = .TELINGA_02_TEXT
                chkTELINGA_03.Checked = .TELINGA_03
                txtTELINGA_03_TEXT.Text = .TELINGA_03_TEXT
                chkTENGGOROKAN_01.Checked = .TENGGOROKAN_01
                chkTENGGOROKAN_02.Checked = .TENGGOROKAN_02
                txtTENGGOROKAN_02_TEXT.Text = .TENGGOROKAN_02_TEXT
                chkTENGGOROKAN_03.Checked = .TENGGOROKAN_03
                txtTENGGOROKAN_03_TEXT.Text = .TENGGOROKAN_03_TEXT
                chkTENGGOROKAN_04.Checked = .TENGGOROKAN_04
                chkTENGGOROKAN_05.Checked = .TENGGOROKAN_05
                chkTENGGOROKAN_06.Checked = .TENGGOROKAN_06
                txtTENGGOROKAN_06_TEXT.Text = .TENGGOROKAN_06_TEXT
                chkTENGGOROKAN_07.Checked = .TENGGOROKAN_07
                txtTENGGOROKAN_07_TEXT.Text = .TENGGOROKAN_07_TEXT
                chkHIDUNG_MUKA_01.Checked = .HIDUNG_MUKA_01
                chkHIDUNG_MUKA_02.Checked = .HIDUNG_MUKA_02
                txtHIDUNG_MUKA_02_TEXT.Text = .HIDUNG_MUKA_02_TEXT
                chkHIDUNG_MUKA_03.Checked = .HIDUNG_MUKA_03
                txtHIDUNG_MUKA_03_TEXT.Text = .HIDUNG_MUKA_03_TEXT
                chkTINGKAT_PERKEMBANGAN_01.Checked = .TINGKAT_PERKEMBANGAN_01
                chkTINGKAT_PERKEMBANGAN_02.Checked = .TINGKAT_PERKEMBANGAN_02
                chkTINGKAT_PERKEMBANGAN_03.Checked = .TINGKAT_PERKEMBANGAN_03
                chkTINGKAT_PERKEMBANGAN_04.Checked = .TINGKAT_PERKEMBANGAN_04
                chkRIWAYAT_KEHAMILAN_KELAHIRAN_01.Checked = .RIWAYAT_KEHAMILAN_KELAHIRAN_01
                chkRIWAYAT_KEHAMILAN_KELAHIRAN_02.Checked = .RIWAYAT_KEHAMILAN_KELAHIRAN_02
                txtRIWAYAT_KEHAMILAN_KELAHIRAN_02_TEXT.Text = .RIWAYAT_KEHAMILAN_KELAHIRAN_02_TEXT
                chkRIWAYAT_KEHAMILAN_KELAHIRAN_03.Checked = .RIWAYAT_KEHAMILAN_KELAHIRAN_03
                txtRIWAYAT_KEHAMILAN_KELAHIRAN_03_TEXT.Text = .RIWAYAT_KEHAMILAN_KELAHIRAN_03_TEXT
                chkSKRINING_GIZI_01_1.Checked = .SKRINING_GIZI_01_1
                chkSKRINING_GIZI_01_2.Checked = .SKRINING_GIZI_01_2
                chkSKRINING_GIZI_02_1.Checked = .SKRINING_GIZI_02_1
                chkSKRINING_GIZI_02_2.Checked = .SKRINING_GIZI_02_2
                chkSKRINING_GIZI_03_1.Checked = .SKRINING_GIZI_03_1
                chkSKRINING_GIZI_03_2.Checked = .SKRINING_GIZI_03_2
                chkSKRINING_RESIKO_JATUH_01_1.Checked = .SKRINING_RESIKO_JATUH_01_1
                chkSKRINING_RESIKO_JATUH_01_2.Checked = .SKRINING_RESIKO_JATUH_01_2
                chkSKRINING_RESIKO_JATUH_02_1.Checked = .SKRINING_RESIKO_JATUH_02_1
                chkSKRINING_RESIKO_JATUH_02_2.Checked = .SKRINING_RESIKO_JATUH_02_2
                chkSKALANYERI_00.Checked = .SKALANEYRI_00
                chkSKALANYERI_01.Checked = .SKALANEYRI_01
                chkSKALANYERI_02.Checked = .SKALANEYRI_02
                chkSKALANYERI_03.Checked = .SKALANEYRI_03
                chkSKALANYERI_04.Checked = .SKALANEYRI_04
                chkSKALANYERI_05.Checked = .SKALANEYRI_05
                chkSKALANYERI_06.Checked = .SKALANEYRI_06
                chkSKALANYERI_07.Checked = .SKALANEYRI_07
                chkSKALANYERI_08.Checked = .SKALANEYRI_08
                chkSKALANYERI_09.Checked = .SKALANEYRI_09
                chkSKALANYERI_10.Checked = .SKALANEYRI_10
                chkSKALANEYRI_KRONIS_01.Checked = .SKALANEYRI_KRONIS_01
                txtSKALANEYRI_LOKASI_TEXT.Text = .SKALANEYRI_LOKASI_TEXT
                txtSKALANEYRI_ISTIRAHAT_TEXT.Text = .SKALANEYRI_ISTIRAHAT_TEXT
                txtSKALANEYRI_AKTIVITAS_TEXT.Text = .SKALANEYRI_AKTIVITAS_TEXT
                txtSKALANEYRI_AKUT_LOKASI_TEXT.Text = .SKALANEYRI_AKUT_LOKASI_TEXT
                txtSKALANEYRI_AKUT_ISTIRAHAT_TEXT.Text = .SKALANEYRI_AKUT_ISTIRAHAT_TEXT
                txtSKALANEYRI_AKUT_AKTIVITAS_TEXT.Text = .SKALANEYRI_AKUT_AKTIVITAS_TEXT
                txtKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN.Text = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN
                chkEDUKASI_01.Checked = .EDUKASI_01
                chkEDUKASI_02.Checked = .EDUKASI_02
                chkEDUKASI_03.Checked = .EDUKASI_03
                txtEDUKASI_03_TEXT.Text = .EDUKASI_03_TEXT
                chkBICARA_SEHARI2_01.Checked = .BICARA_SEHARI2_01
                chkBICARA_SEHARI2_01_1.Checked = .BICARA_SEHARI2_01_1
                chkBICARA_SEHARI2_01_2.Checked = .BICARA_SEHARI2_01_2
                chkBICARA_SEHARI2_02.Checked = .BICARA_SEHARI2_02
                chkBICARA_SEHARI2_02_1.Checked = .BICARA_SEHARI2_02_1
                chkBICARA_SEHARI2_02_2.Checked = .BICARA_SEHARI2_02_2
                chkBICARA_SEHARI2_03.Checked = .BICARA_SEHARI2_03
                txtBICARA_SEHARI2_03_TEXT.Text = .BICARA_SEHARI2_03_TEXT
                chkBICARA_SEHARI2_04.Checked = .BICARA_SEHARI2_04
                txtBICARA_SEHARI2_04_TEXT.Text = .BICARA_SEHARI2_04_TEXT
                chkPERLU_PENERJEMAH_01.Checked = .PERLU_PENERJEMAH_01
                chkPERLU_PENERJEMAH_02.Checked = .PERLU_PENERJEMAH_02
                chkBAHASA_ISYARAT_01.Checked = .BAHASA_ISYARAT_01
                chkBAHASA_ISYARAT_02.Checked = .BAHASA_ISYARAT_02
                chkHAMBATAN_EDUKASI_01.Checked = .HAMBATAN_EDUKASI_01
                chkHAMBATAN_EDUKASI_02.Checked = .HAMBATAN_EDUKASI_02
                chkHAMBATAN_EDUKASI_03.Checked = .HAMBATAN_EDUKASI_03
                chkHAMBATAN_EDUKASI_04.Checked = .HAMBATAN_EDUKASI_04
                chkHAMBATAN_EDUKASI_05.Checked = .HAMBATAN_EDUKASI_05
                chkHAMBATAN_EDUKASI_06.Checked = .HAMBATAN_EDUKASI_06
                chkHAMBATAN_EDUKASI_07.Checked = .HAMBATAN_EDUKASI_07
                chkHAMBATAN_EDUKASI_08.Checked = .HAMBATAN_EDUKASI_08
                chkHAMBATAN_EDUKASI_09.Checked = .HAMBATAN_EDUKASI_09
                chkHAMBATAN_EDUKASI_10.Checked = .HAMBATAN_EDUKASI_10
                chkHAMBATAN_EDUKASI_11.Checked = .HAMBATAN_EDUKASI_11
                chkHAMBATAN_EDUKASI_12.Checked = .HAMBATAN_EDUKASI_12
                txtUSIA_KEHAMILAN.Text = .USIA_KEHAMILAN
                txtBERAT_BADAN_LAHIR.Text = .BERAT_BADAN_LAHIR
                chkPERSALINAN_01.Checked = .PERSALINAN_01
                chkPERSALINAN_02.Checked = .PERSALINAN_02
                chkPERSALINAN_03.Checked = .PERSALINAN_03
                chkPERSALINAN_04.Checked = .PERSALINAN_04
                chkMENANGIS_01.Checked = .MENANGIS_01
                chkMENANGIS_02.Checked = .MENANGIS_02
                txtMENANGIS_03_TEXT.Text = .MENANGIS_03_TEXT
                chkJAUNDICE_01.Checked = .JAUNDICE_01
                chkJAUNDICE_02.Checked = .JAUNDICE_02
                txtRIWAYAT_KELAHIRAN_LAIN_LAIN.Text = .RIWAYAT_KELAHIRAN_LAIN_LAIN
                chkSOSIAL_01.Checked = .SOSIAL_01
                chkSOSIAL_02.Checked = .SOSIAL_02
                chkSOSIAL_03.Checked = .SOSIAL_03
                chkSOSIAL_04.Checked = .SOSIAL_04
                chkSOSIAL_05.Checked = .SOSIAL_05
                chkSOSIAL_06.Checked = .SOSIAL_06
                chkSOSIAL_07.Checked = .SOSIAL_07
                chkSOSIAL_08.Checked = .SOSIAL_08
                chkSOSIAL_09.Checked = .SOSIAL_09
                chkSOSIAL_10.Checked = .SOSIAL_10
                chkMOTORIK_HALUS_01.Checked = .MOTORIK_HALUS_01
                chkMOTORIK_HALUS_02.Checked = .MOTORIK_HALUS_02
                chkMOTORIK_HALUS_03.Checked = .MOTORIK_HALUS_03
                chkMOTORIK_HALUS_04.Checked = .MOTORIK_HALUS_04
                chkMOTORIK_HALUS_05.Checked = .MOTORIK_HALUS_05
                chkMOTORIK_HALUS_06.Checked = .MOTORIK_HALUS_06
                chkMOTORIK_HALUS_07.Checked = .MOTORIK_HALUS_07
                chkMOTORIK_HALUS_08.Checked = .MOTORIK_HALUS_08
                chkMOTORIK_HALUS_09.Checked = .MOTORIK_HALUS_09
                chkMOTORIK_KASAR_01.Checked = .MOTORIK_KASAR_01
                chkMOTORIK_KASAR_02.Checked = .MOTORIK_KASAR_02
                chkMOTORIK_KASAR_03.Checked = .MOTORIK_KASAR_03
                chkMOTORIK_KASAR_04.Checked = .MOTORIK_KASAR_04
                chkMOTORIK_KASAR_05.Checked = .MOTORIK_KASAR_05
                chkMOTORIK_KASAR_06.Checked = .MOTORIK_KASAR_06
                chkMOTORIK_KASAR_07.Checked = .MOTORIK_KASAR_07
                chkMOTORIK_KASAR_08.Checked = .MOTORIK_KASAR_08
                chkMOTORIK_KASAR_09.Checked = .MOTORIK_KASAR_09
                chkBAHASA_01.Checked = .BAHASA_01
                chkBAHASA_02.Checked = .BAHASA_02
                chkBAHASA_03.Checked = .BAHASA_03
                chkBAHASA_04.Checked = .BAHASA_04
                chkBAHASA_05.Checked = .BAHASA_05
                chkBAHASA_06.Checked = .BAHASA_06
                chkBAHASA_07.Checked = .BAHASA_07
                chkBAHASA_08.Checked = .BAHASA_08
                chkBAHASA_09.Checked = .BAHASA_09
                chkMASALAH_KEPERAWATAN_01.Checked = .MASALAH_KEPERAWATAN_01
                chkMASALAH_KEPERAWATAN_02.Checked = .MASALAH_KEPERAWATAN_02
                chkMASALAH_KEPERAWATAN_02_1.Checked = .MASALAH_KEPERAWATAN_02_1
                chkMASALAH_KEPERAWATAN_02_2.Checked = .MASALAH_KEPERAWATAN_02_2
                chkMASALAH_KEPERAWATAN_02_3.Checked = .MASALAH_KEPERAWATAN_02_3
                chkMASALAH_KEPERAWATAN_02_4.Checked = .MASALAH_KEPERAWATAN_02_4
                chkMASALAH_KEPERAWATAN_02_5.Checked = .MASALAH_KEPERAWATAN_02_5
                chkMASALAH_KEPERAWATAN_02_6.Checked = .MASALAH_KEPERAWATAN_02_6
                chkMASALAH_KEPERAWATAN_02_7.Checked = .MASALAH_KEPERAWATAN_02_7
                chkMASALAH_KEPERAWATAN_02_8.Checked = .MASALAH_KEPERAWATAN_02_8
                chkMASALAH_KEPERAWATAN_02_9.Checked = .MASALAH_KEPERAWATAN_02_9
                txtMASALAH_KEPERAWATAN_02_9_TEXT.Text = .MASALAH_KEPERAWATAN_02_9_TEXT
                chkMASALAH_KEPERAWATAN_03.Checked = .MASALAH_KEPERAWATAN_03
                chkMASALAH_KEPERAWATAN_03_1.Checked = .MASALAH_KEPERAWATAN_03_1
                chkMASALAH_KEPERAWATAN_03_2.Checked = .MASALAH_KEPERAWATAN_03_2
                chkMASALAH_KEPERAWATAN_03_3.Checked = .MASALAH_KEPERAWATAN_03_3
                chkMASALAH_KEPERAWATAN_03_4.Checked = .MASALAH_KEPERAWATAN_03_4
                chkMASALAH_KEPERAWATAN_03_5.Checked = .MASALAH_KEPERAWATAN_03_5
                txtMASALAH_KEPERAWATAN_03_5_TEXT.Text = .MASALAH_KEPERAWATAN_03_5_TEXT
                chkMASALAH_KEPERAWATAN_04.Checked = .MASALAH_KEPERAWATAN_04
                chkMASALAH_KEPERAWATAN_04_1.Checked = .MASALAH_KEPERAWATAN_04_1
                chkMASALAH_KEPERAWATAN_04_2.Checked = .MASALAH_KEPERAWATAN_04_2
                chkMASALAH_KEPERAWATAN_04_3.Checked = .MASALAH_KEPERAWATAN_04_3
                chkMASALAH_KEPERAWATAN_04_4.Checked = .MASALAH_KEPERAWATAN_04_4
                txtMASALAH_KEPERAWATAN_04_4_TEXT.Text = .MASALAH_KEPERAWATAN_04_4_TEXT
                chkMASALAH_KEPERAWATAN_05.Checked = .MASALAH_KEPERAWATAN_05
                chkMASALAH_KEPERAWATAN_05_1.Checked = .MASALAH_KEPERAWATAN_05_1
                chkMASALAH_KEPERAWATAN_05_2.Checked = .MASALAH_KEPERAWATAN_05_2
                txtMASALAH_KEPERAWATAN_05_2_TEXT.Text = .MASALAH_KEPERAWATAN_05_2_TEXT
                chkMASALAH_KEPERAWATAN_06.Checked = .MASALAH_KEPERAWATAN_06
                chkMASALAH_KEPERAWATAN_06_1.Checked = .MASALAH_KEPERAWATAN_06_1
                chkMASALAH_KEPERAWATAN_06_2.Checked = .MASALAH_KEPERAWATAN_06_2
                txtMASALAH_KEPERAWATAN_06_2_TEXT.Text = .MASALAH_KEPERAWATAN_06_2_TEXT
                chkMASALAH_KEPERAWATAN_07.Checked = .MASALAH_KEPERAWATAN_07
                chkMASALAH_KEPERAWATAN_07_1.Checked = .MASALAH_KEPERAWATAN_07_1
                chkMASALAH_KEPERAWATAN_07_2.Checked = .MASALAH_KEPERAWATAN_07_2
                chkMASALAH_KEPERAWATAN_07_3.Checked = .MASALAH_KEPERAWATAN_07_3
                chkMASALAH_KEPERAWATAN_07_4.Checked = .MASALAH_KEPERAWATAN_07_4
                txtMASALAH_KEPERAWATAN_07_4_TEXT.Text = .MASALAH_KEPERAWATAN_07_4_TEXT
                chkMASALAH_KEPERAWATAN_08.Checked = .MASALAH_KEPERAWATAN_08
                chkMASALAH_KEPERAWATAN_08_1.Checked = .MASALAH_KEPERAWATAN_08_1
                chkMASALAH_KEPERAWATAN_08_2.Checked = .MASALAH_KEPERAWATAN_08_2
                chkMASALAH_KEPERAWATAN_08_3.Checked = .MASALAH_KEPERAWATAN_08_3
                chkMASALAH_KEPERAWATAN_08_4.Checked = .MASALAH_KEPERAWATAN_08_4
                chkMASALAH_KEPERAWATAN_08_5.Checked = .MASALAH_KEPERAWATAN_08_5
                chkMASALAH_KEPERAWATAN_08_6.Checked = .MASALAH_KEPERAWATAN_08_6
                txtMASALAH_KEPERAWATAN_08_6_TEXT.Text = .MASALAH_KEPERAWATAN_08_6_TEXT
                chkMASALAH_KEPERAWATAN_09.Checked = .MASALAH_KEPERAWATAN_09
                chkMASALAH_KEPERAWATAN_09_1.Checked = .MASALAH_KEPERAWATAN_09_1
                chkMASALAH_KEPERAWATAN_09_2.Checked = .MASALAH_KEPERAWATAN_09_2
                chkMASALAH_KEPERAWATAN_09_3.Checked = .MASALAH_KEPERAWATAN_09_3
                txtMASALAH_KEPERAWATAN_09_3_TEXT.Text = .MASALAH_KEPERAWATAN_09_3_TEXT
                chkMASALAH_KEPERAWATAN_10.Checked = .MASALAH_KEPERAWATAN_10
                chkMASALAH_KEPERAWATAN_10_1.Checked = .MASALAH_KEPERAWATAN_10_1
                chkMASALAH_KEPERAWATAN_10_2.Checked = .MASALAH_KEPERAWATAN_10_2
                chkMASALAH_KEPERAWATAN_10_3.Checked = .MASALAH_KEPERAWATAN_10_3
                txtMASALAH_KEPERAWATAN_10_3_TEXT.Text = .MASALAH_KEPERAWATAN_10_3_TEXT
                chkMASALAH_KEPERAWATAN_11.Checked = .MASALAH_KEPERAWATAN_11
                txtMASALAH_KEPERAWATAN_11_TEXT.Text = .MASALAH_KEPERAWATAN_11_TEXT
                chkMASALAH_KEPERAWATAN_12_1.Checked = .MASALAH_KEPERAWATAN_12_1
                txtMASALAH_KEPERAWATAN_12_1_TEXT.Text = .MASALAH_KEPERAWATAN_12_1_TEXT
                chkMASALAH_KEPERAWATAN_12_2.Checked = .MASALAH_KEPERAWATAN_12_2
                txtMASALAH_KEPERAWATAN_12_2_TEXT.Text = .MASALAH_KEPERAWATAN_12_2_TEXT
                chkMASALAH_KEPERAWATAN_13.Checked = .MASALAH_KEPERAWATAN_13
                txtMASALAH_KEPERAWATAN_13_TEXT.Text = .MASALAH_KEPERAWATAN_13_TEXT
                txtCATATAN_KEPERAWATAN_JAM.Text = .CATATAN_KEPERAWATAN_JAM
                chkINTERVENSI_IMPLEMENTASI_01_1.Checked = .INTERVENSI_IMPLEMENTASI_01_1
                txtINTERVENSI_IMPLEMENTASI_01_TD.Text = .INTERVENSI_IMPLEMENTASI_01_TD
                txtINTERVENSI_IMPLEMENTASI_01_N.Text = .INTERVENSI_IMPLEMENTASI_01_N
                txtINTERVENSI_IMPLEMENTASI_01_S.Text = .INTERVENSI_IMPLEMENTASI_01_S
                txtINTERVENSI_IMPLEMENTASI_01_R.Text = .INTERVENSI_IMPLEMENTASI_01_R
                chkINTERVENSI_IMPLEMENTASI_01_2.Checked = .INTERVENSI_IMPLEMENTASI_01_2
                chkINTERVENSI_IMPLEMENTASI_01_3.Checked = .INTERVENSI_IMPLEMENTASI_01_3
                chkINTERVENSI_IMPLEMENTASI_01_4.Checked = .INTERVENSI_IMPLEMENTASI_01_4
                chkINTERVENSI_IMPLEMENTASI_01_5.Checked = .INTERVENSI_IMPLEMENTASI_01_5
                chkINTERVENSI_IMPLEMENTASI_01_6.Checked = .INTERVENSI_IMPLEMENTASI_01_6
                chkINTERVENSI_IMPLEMENTASI_01_7.Checked = .INTERVENSI_IMPLEMENTASI_01_7
                txtINTERVENSI_IMPLEMENTASI_01_7_TEXT_1.Text = .INTERVENSI_IMPLEMENTASI_01_7_TEXT_1
                txtINTERVENSI_IMPLEMENTASI_01_7_TEXT_2.Text = .INTERVENSI_IMPLEMENTASI_01_7_TEXT_2
                chkINTERVENSI_IMPLEMENTASI_01_8.Checked = .INTERVENSI_IMPLEMENTASI_01_8
                chkINTERVENSI_IMPLEMENTASI_01_9.Checked = .INTERVENSI_IMPLEMENTASI_01_9
                chkINTERVENSI_IMPLEMENTASI_02_1.Checked = .INTERVENSI_IMPLEMENTASI_02_1
                chkINTERVENSI_IMPLEMENTASI_02_2.Checked = .INTERVENSI_IMPLEMENTASI_02_2
                chkINTERVENSI_IMPLEMENTASI_02_3.Checked = .INTERVENSI_IMPLEMENTASI_02_3
                chkINTERVENSI_IMPLEMENTASI_02_4.Checked = .INTERVENSI_IMPLEMENTASI_02_4
                chkINTERVENSI_IMPLEMENTASI_02_5.Checked = .INTERVENSI_IMPLEMENTASI_02_5
                chkINTERVENSI_IMPLEMENTASI_02_6.Checked = .INTERVENSI_IMPLEMENTASI_02_6
                chkINTERVENSI_IMPLEMENTASI_02_7.Checked = .INTERVENSI_IMPLEMENTASI_02_7
                txtINTERVENSI_IMPLEMENTASI_02_7_TEXT.Text = .INTERVENSI_IMPLEMENTASI_02_7_TEXT
                chkINTERVENSI_IMPLEMENTASI_03_1.Checked = .INTERVENSI_IMPLEMENTASI_03_1
                chkINTERVENSI_IMPLEMENTASI_03_2.Checked = .INTERVENSI_IMPLEMENTASI_03_2
                chkINTERVENSI_IMPLEMENTASI_03_3.Checked = .INTERVENSI_IMPLEMENTASI_03_3
                chkINTERVENSI_IMPLEMENTASI_03_4.Checked = .INTERVENSI_IMPLEMENTASI_03_4
                chkINTERVENSI_IMPLEMENTASI_03_5.Checked = .INTERVENSI_IMPLEMENTASI_03_5
                txtINTERVENSI_IMPLEMENTASI_03_5_TEXT.Text = .INTERVENSI_IMPLEMENTASI_03_5_TEXT
                chkINTERVENSI_IMPLEMENTASI_03_6.Checked = .INTERVENSI_IMPLEMENTASI_03_6
                txtINTERVENSI_IMPLEMENTASI_03_6_TEXT.Text = .INTERVENSI_IMPLEMENTASI_03_6_TEXT
                chkINTERVENSI_IMPLEMENTASI_04_1.Checked = .INTERVENSI_IMPLEMENTASI_04_1
                chkINTERVENSI_IMPLEMENTASI_04_2.Checked = .INTERVENSI_IMPLEMENTASI_04_2
                chkINTERVENSI_IMPLEMENTASI_04_3.Checked = .INTERVENSI_IMPLEMENTASI_04_3
                chkINTERVENSI_IMPLEMENTASI_04_4.Checked = .INTERVENSI_IMPLEMENTASI_04_4
                txtINTERVENSI_IMPLEMENTASI_04_4_TEXT.Text = .INTERVENSI_IMPLEMENTASI_04_4_TEXT
                chkINTERVENSI_IMPLEMENTASI_04_5.Checked = .INTERVENSI_IMPLEMENTASI_04_5
                chkINTERVENSI_IMPLEMENTASI_04_6.Checked = .INTERVENSI_IMPLEMENTASI_04_6
                chkINTERVENSI_IMPLEMENTASI_04_7.Checked = .INTERVENSI_IMPLEMENTASI_04_7
                chkINTERVENSI_IMPLEMENTASI_04_8.Checked = .INTERVENSI_IMPLEMENTASI_04_8
                chkINTERVENSI_IMPLEMENTASI_04_9.Checked = .INTERVENSI_IMPLEMENTASI_04_9
                chkINTERVENSI_IMPLEMENTASI_05_1.Checked = .INTERVENSI_IMPLEMENTASI_05_1
                chkINTERVENSI_IMPLEMENTASI_05_2.Checked = .INTERVENSI_IMPLEMENTASI_05_2
                chkINTERVENSI_IMPLEMENTASI_05_3.Checked = .INTERVENSI_IMPLEMENTASI_05_3
                chkINTERVENSI_IMPLEMENTASI_05_4.Checked = .INTERVENSI_IMPLEMENTASI_05_4
                chkINTERVENSI_IMPLEMENTASI_05_5.Checked = .INTERVENSI_IMPLEMENTASI_05_5
                chkINTERVENSI_IMPLEMENTASI_05_6.Checked = .INTERVENSI_IMPLEMENTASI_05_6
                txtINTERVENSI_IMPLEMENTASI_05_6_TEXT.Text = .INTERVENSI_IMPLEMENTASI_05_6_TEXT
                chkHASIL_PENANGANAN_01.Checked = .HASIL_PENANGANAN_01
                chkHASIL_PENANGANAN_02.Checked = .HASIL_PENANGANAN_02
                chkHASIL_PENANGANAN_03.Checked = .HASIL_PENANGANAN_03
                chkHASIL_PENANGANAN_04.Checked = .HASIL_PENANGANAN_04
                chkHASIL_PENANGANAN_05.Checked = .HASIL_PENANGANAN_05
                chkREPRODUKSI_01_1.Checked = .REPRODUKSI_01_1
                chkREPRODUKSI_01_2.Checked = .REPRODUKSI_01_2
                chkREPRODUKSI_02_1.Checked = .REPRODUKSI_02_1
                chkREPRODUKSI_02_2.Checked = .REPRODUKSI_02_2
                chkUMUR_01.Checked = .UMUR_01
                chkUMUR_02.Checked = .UMUR_02
                chkUMUR_03.Checked = .UMUR_03
                chkUMUR_04.Checked = .UMUR_04
                chkUMUR_05.Checked = .UMUR_05
                chkUMUR_06.Checked = .UMUR_06
                chkUMUR_07.Checked = .UMUR_07
                chkUMUR_08.Checked = .UMUR_08
                chkUMUR_09.Checked = .UMUR_09
                chkUMUR_10.Checked = .UMUR_10
                chkMASALAH_KEPERAWATAN_01_AKTUAL.Checked = .MASALAH_KEPERAWATAN_01_AKTUAL
                chkMASALAH_KEPERAWATAN_01_POTENSIAL.Checked = .MASALAH_KEPERAWATAN_01_POTENSIAL
                chkMASALAH_KEPERAWATAN_02_AKTUAL.Checked = .MASALAH_KEPERAWATAN_02_AKTUAL
                chkMASALAH_KEPERAWATAN_02_POTENSIAL.Checked = .MASALAH_KEPERAWATAN_02_POTENSIAL
                chkMASALAH_KEPERAWATAN_03_AKTUAL.Checked = .MASALAH_KEPERAWATAN_03_AKTUAL
                chkMASALAH_KEPERAWATAN_03_POTENSIAL.Checked = .MASALAH_KEPERAWATAN_03_POTENSIAL
                chkMASALAH_KEPERAWATAN_04_AKTUAL.Checked = .MASALAH_KEPERAWATAN_04_AKTUAL
                chkMASALAH_KEPERAWATAN_04_POTENSIAL.Checked = .MASALAH_KEPERAWATAN_04_POTENSIAL
                chkMASALAH_KEPERAWATAN_05_AKTUAL.Checked = .MASALAH_KEPERAWATAN_05_AKTUAL
                chkMASALAH_KEPERAWATAN_05_POTENSIAL.Checked = .MASALAH_KEPERAWATAN_05_POTENSIAL
                chkMASALAH_KEPERAWATAN_06_AKTUAL.Checked = .MASALAH_KEPERAWATAN_06_AKTUAL
                chkMASALAH_KEPERAWATAN_06_POTENSIAL.Checked = .MASALAH_KEPERAWATAN_06_POTENSIAL
                chkMASALAH_KEPERAWATAN_07_AKTUAL.Checked = .MASALAH_KEPERAWATAN_07_AKTUAL
                chkMASALAH_KEPERAWATAN_07_POTENSIAL.Checked = .MASALAH_KEPERAWATAN_07_POTENSIAL
                chkMASALAH_KEPERAWATAN_08_AKTUAL.Checked = .MASALAH_KEPERAWATAN_08_AKTUAL
                chkMASALAH_KEPERAWATAN_08_POTENSIAL.Checked = .MASALAH_KEPERAWATAN_08_POTENSIAL
                chkMASALAH_KEPERAWATAN_09_AKTUAL.Checked = .MASALAH_KEPERAWATAN_09_AKTUAL
                chkMASALAH_KEPERAWATAN_09_POTENSIAL.Checked = .MASALAH_KEPERAWATAN_09_POTENSIAL
                chkMASALAH_KEPERAWATAN_10_AKTUAL.Checked = .MASALAH_KEPERAWATAN_10_AKTUAL
                chkMASALAH_KEPERAWATAN_10_POTENSIAL.Checked = .MASALAH_KEPERAWATAN_10_POTENSIAL
                chkMASALAH_KEPERAWATAN_10_RESIKO.Checked = .MASALAH_KEPERAWATAN_10_RESIKO
                chkMASALAH_KEPERAWATAN_08_NYERI.Checked = .MASALAH_KEPERAWATAN_08_NYERI
                chkMASALAH_KEPERAWATAN_08_PUSING.Checked = .MASALAH_KEPERAWATAN_08_PUSING
                chkMASALAH_KEPERAWATAN_08_GATAL.Checked = .MASALAH_KEPERAWATAN_08_GATAL
                chkTANDA_VITAL_REGULER.Checked = .TANDA_VITAL_REGULER
                chkTANDA_VITAL_IREGULER.Checked = .TANDA_VITAL_IREGULER
                chkREPRODUKSI_03_1.Checked = .REPRODUKSI_03_1
                chkREPRODUKSI_03_2.Checked = .REPRODUKSI_03_2
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04
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
            Dim ds = oS_DIGITAL_RJ_03.GetStructureHeader
            With ds
                .KDKUNJUNGAN = txtNoRegister.Text
                Try
                    .DATECREATED = oS_DIGITAL_RJ_03.GetData(txtNoRegister.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .AUTO = chkAUTO.Checked
                .ALLO = chkALLO.Checked
                .NAMA_PENGKAJIAN_FISIK_DAN_ANAMNESIS = txtNAMAPENGKAJIAN.Text
                .KELUHAN_UTAMA = txtKELUHAN_UTAMA.Text
                .KESADARAN_UMUM = txtKESADARAN_UMUM.Text
                .GCS = txt_GCS.Text
                .E = cbo_E.Text
                .M = cbo_M.Text
                .V = cbo_V.Text
                .KEADAAN_EMOSIONAL_01 = chkKEADAAN_EMOSIONAL_01.Checked
                .KEADAAN_EMOSIONAL_02 = chkKEADAAN_EMOSIONAL_02.Checked
                .KEADAAN_EMOSIONAL_03 = chkKEADAAN_EMOSIONAL_03.Checked
                .KEADAAN_EMOSIONAL_04 = chkKEADAAN_EMOSIONAL_04.Checked
                .KEADAAN_EMOSIONAL_04_TEXT = txtKEADAAN_EMOSIONAL_04_TEXT.Text
                .TANDA_VITAL_01 = txtTANDA_VITAL_01.Text
                .TANDA_VITAL_02 = txtTANDA_VITAL_02.Text
                .TANDA_VITAL_03 = txtTANDA_VITAL_03.Text
                .TANDA_VITAL_04 = txtTANDA_VITAL_04.Text
                .TANDA_VITAL_05 = txtTANDA_VITAL_05.Text
                .TANDA_VITAL_06 = txtTANDA_VITAL_06.Text
                .TANDA_VITAL_07 = txtTANDA_VITAL_07.Text
                .TANDA_VITAL_08 = txtTANDA_VITAL_08.Text
                .PERSYARAFAN_01 = chkPERSYARAFAN_01.Checked
                .PERSYARAFAN_02 = chkPERSYARAFAN_02.Checked
                .PERSYARAFAN_03 = chkPERSYARAFAN_03.Checked
                .PERSYARAFAN_04 = chkPERSYARAFAN_04.Checked
                .PERSYARAFAN_05 = chkPERSYARAFAN_05.Checked
                .PERSYARAFAN_06 = chkPERSYARAFAN_06.Checked
                .PERSYARAFAN_06_TEXT = txtPERSYARAFAN_06_TEXT.Text
                .PERNAPASAN_01 = chkPERNAPASAN_01.Checked
                .PERNAPASAN_02 = chkPERNAPASAN_02.Checked
                .PERNAPASAN_03 = chkPERNAPASAN_03.Checked
                .PERNAPASAN_04 = chkPERNAPASAN_04.Checked
                .PERNAPASAN_04_TEXT = txtPERNAPASAN_04_TEXT.Text
                .PENCERNAAN_01 = chkPENCERNAAN_01.Checked
                .PENCERNAAN_02 = chkPENCERNAAN_02.Checked
                .PENCERNAAN_03 = chkPENCERNAAN_03.Checked
                .PENCERNAAN_04 = chkPENCERNAAN_04.Checked
                .PENCERNAAN_05 = chkPENCERNAAN_05.Checked
                .PENCERNAAN_05_TEXT = txtPENCERNAAN_05_TEXT.Text
                .ENDROKIN_01 = chkENDROKIN_01.Checked
                .ENDROKIN_02 = chkENDROKIN_02.Checked
                .ENDROKIN_03 = chkENDROKIN_03.Checked
                .ENDROKIN_04 = chkENDROKIN_04.Checked
                .ENDROKIN_04_TEXT = txtENDROKIN_04_TEXT.Text
                .CARDIOVASKULER_01 = chkCARDIOVASKULER_01.Checked
                .CARDIOVASKULER_02 = chkCARDIOVASKULER_02.Checked
                .CARDIOVASKULER_03 = chkCARDIOVASKULER_03.Checked
                .CARDIOVASKULER_04 = chkCARDIOVASKULER_04.Checked
                .CARDIOVASKULER_04_TEXT = txtCARDIOVASKULER_04_TEXT.Text
                .ABDOMEN_01 = chkABDOMEN_01.Checked
                .ABDOMEN_02 = chkABDOMEN_02.Checked
                .ABDOMEN_03 = chkABDOMEN_03.Checked
                .ABDOMEN_04 = chkABDOMEN_04.Checked
                .ABDOMEN_05 = chkABDOMEN_05.Checked
                .ABDOMEN_05_TEXT = txtABDOMEN_05_TEXT.Text
                .ABDOMEN_06 = chkABDOMEN_06.Checked
                .ABDOMEN_06_TEXT = txtABDOMEN_06_TEXT.Text
                .ABDOMEN_07 = chkABDOMEN_07.Checked
                .ABDOMEN_07_TEXT = txtABDOMEN_07_TEXT.Text
                .ABDOMEN_08 = chkABDOMEN_08.Checked
                .ABDOMEN_08_TEXT = txtABDOMEN_08_TEXT.Text
                .ABDOMEN_09 = chkABDOMEN_09.Checked
                .ABDOMEN_09_TEXT = txtABDOMEN_09_TEXT.Text
                .REPRODUKSI_01 = chkREPRODUKSI_01.Checked
                .REPRODUKSI_02 = chkREPRODUKSI_02.Checked
                .REPRODUKSI_03 = chkREPRODUKSI_03.Checked
                .REPRODUKSI_04 = chkREPRODUKSI_04.Checked
                .REPRODUKSI_05 = chkREPRODUKSI_05.Checked
                .REPRODUKSI_01_TEXT = txtREPRODUKSI_01_TEXT.Text
                .REPRODUKSI_02_TEXT = txtREPRODUKSI_02_TEXT.Text
                .REPRODUKSI_03_TEXT = txtREPRODUKSI_03_TEXT.Text
                .REPRODUKSI_04_TEXT = txtREPRODUKSI_04_TEXT.Text
                .REPRODUKSI_05_TEXT = txtREPRODUKSI_05_TEXT.Text
                .REPRODUKSI_06_TEXT = txtREPRODUKSI_06_TEXT.Text
                .REPRODUKSI_07_TEXT = txtREPRODUKSI_07_TEXT.Text
                .KULIT_01 = chkKULIT_01.Checked
                .KULIT_02 = chkKULIT_02.Checked
                .KULIT_02_TEXT = txtKULIT_02_TEXT.Text
                .KULIT_03 = chkKULIT_03.Checked
                .KULIT_03_TEXT = txtKULIT_03_TEXT.Text
                .KULIT_04 = chkKULIT_04.Checked
                .KULIT_05 = chkKULIT_05.Checked
                .KULIT_06 = chkKULIT_06.Checked
                .KULIT_06_TEXT = txtKULIT_06_TEXT.Text
                .URINARIA_01 = chkURINARIA_01.Checked
                .URINARIA_02 = chkURINARIA_02.Checked
                .URINARIA_02_TEXT = txtURINARIA_02_TEXT.Text
                .URINARIA_03 = chkURINARIA_03.Checked
                .URINARIA_03_TEXT = txtURINARIA_03_TEXT.Text
                .URINARIA_04 = chkURINARIA_04.Checked
                .URINARIA_05 = chkURINARIA_05.Checked
                .URINARIA_05_TEXT = txtURINARIA_05_TEXT.Text
                .MATA_01 = chkMATA_01.Checked
                .MATA_02 = chkMATA_02.Checked
                .MATA_03 = chkMATA_03.Checked
                .MATA_04 = chkMATA_04.Checked
                .MATA_04_TEXT = txtMATA_04_TEXT.Text
                .OTOT_SENDI_TULANG_01 = chkOTOT_SENDI_TULANG_01.Checked
                .OTOT_SENDI_TULANG_02 = chkOTOT_SENDI_TULANG_02.Checked
                .OTOT_SENDI_TULANG_03 = chkOTOT_SENDI_TULANG_03.Checked
                .OTOT_SENDI_TULANG_04 = chkOTOT_SENDI_TULANG_04.Checked
                .OTOT_SENDI_TULANG_04_TEXT = txtOTOT_SENDI_TULANG_04_TEXT.Text
                .MULUT_01 = chkMULUT_01.Checked
                .MULUT_02 = chkMULUT_02.Checked
                .MULUT_02_TEXT = txtMULUT_02_TEXT.Text
                .MULUT_03 = chkMULUT_03.Checked
                .MULUT_03_TEXT = txtMULUT_03_TEXT.Text
                .TELINGA_01 = chkTELINGA_01.Checked
                .TELINGA_02 = chkTELINGA_02.Checked
                .TELINGA_02_TEXT = txtTELINGA_02_TEXT.Text
                .TELINGA_03 = chkTELINGA_03.Checked
                .TELINGA_03_TEXT = txtTELINGA_03_TEXT.Text
                .TENGGOROKAN_01 = chkTENGGOROKAN_01.Checked
                .TENGGOROKAN_02 = chkTENGGOROKAN_02.Checked
                .TENGGOROKAN_02_TEXT = txtTENGGOROKAN_02_TEXT.Text
                .TENGGOROKAN_03 = chkTENGGOROKAN_03.Checked
                .TENGGOROKAN_03_TEXT = txtTENGGOROKAN_03_TEXT.Text
                .TENGGOROKAN_04 = chkTENGGOROKAN_04.Checked
                .TENGGOROKAN_05 = chkTENGGOROKAN_05.Checked
                .TENGGOROKAN_06 = chkTENGGOROKAN_06.Checked
                .TENGGOROKAN_06_TEXT = txtTENGGOROKAN_06_TEXT.Text
                .TENGGOROKAN_07 = chkTENGGOROKAN_07.Checked
                .TENGGOROKAN_07_TEXT = txtTENGGOROKAN_07_TEXT.Text
                .HIDUNG_MUKA_01 = chkHIDUNG_MUKA_01.Checked
                .HIDUNG_MUKA_02 = chkHIDUNG_MUKA_02.Checked
                .HIDUNG_MUKA_02_TEXT = txtHIDUNG_MUKA_02_TEXT.Text
                .HIDUNG_MUKA_03 = chkHIDUNG_MUKA_03.Checked
                .HIDUNG_MUKA_03_TEXT = txtHIDUNG_MUKA_03_TEXT.Text
                .TINGKAT_PERKEMBANGAN_01 = chkTINGKAT_PERKEMBANGAN_01.Checked
                .TINGKAT_PERKEMBANGAN_02 = chkTINGKAT_PERKEMBANGAN_02.Checked
                .TINGKAT_PERKEMBANGAN_03 = chkTINGKAT_PERKEMBANGAN_03.Checked
                .TINGKAT_PERKEMBANGAN_04 = chkTINGKAT_PERKEMBANGAN_04.Checked
                .RIWAYAT_KEHAMILAN_KELAHIRAN_01 = chkRIWAYAT_KEHAMILAN_KELAHIRAN_01.Checked
                .RIWAYAT_KEHAMILAN_KELAHIRAN_02 = chkRIWAYAT_KEHAMILAN_KELAHIRAN_02.Checked
                .RIWAYAT_KEHAMILAN_KELAHIRAN_02_TEXT = txtRIWAYAT_KEHAMILAN_KELAHIRAN_02_TEXT.Text
                .RIWAYAT_KEHAMILAN_KELAHIRAN_03 = chkRIWAYAT_KEHAMILAN_KELAHIRAN_03.Checked
                .RIWAYAT_KEHAMILAN_KELAHIRAN_03_TEXT = txtRIWAYAT_KEHAMILAN_KELAHIRAN_03_TEXT.Text
                .SKRINING_GIZI_01_1 = chkSKRINING_GIZI_01_1.Checked
                .SKRINING_GIZI_01_2 = chkSKRINING_GIZI_01_2.Checked
                .SKRINING_GIZI_02_1 = chkSKRINING_GIZI_02_1.Checked
                .SKRINING_GIZI_02_2 = chkSKRINING_GIZI_02_2.Checked
                .SKRINING_GIZI_03_1 = chkSKRINING_GIZI_03_1.Checked
                .SKRINING_GIZI_03_2 = chkSKRINING_GIZI_03_2.Checked
                .SKRINING_RESIKO_JATUH_01_1 = chkSKRINING_RESIKO_JATUH_01_1.Checked
                .SKRINING_RESIKO_JATUH_01_2 = chkSKRINING_RESIKO_JATUH_01_2.Checked
                .SKRINING_RESIKO_JATUH_02_1 = chkSKRINING_RESIKO_JATUH_02_1.Checked
                .SKRINING_RESIKO_JATUH_02_2 = chkSKRINING_RESIKO_JATUH_02_2.Checked
                .SKALANEYRI_00 = chkSKALANYERI_00.Checked
                .SKALANEYRI_01 = chkSKALANYERI_01.Checked
                .SKALANEYRI_02 = chkSKALANYERI_02.Checked
                .SKALANEYRI_03 = chkSKALANYERI_03.Checked
                .SKALANEYRI_04 = chkSKALANYERI_04.Checked
                .SKALANEYRI_05 = chkSKALANYERI_05.Checked
                .SKALANEYRI_06 = chkSKALANYERI_06.Checked
                .SKALANEYRI_07 = chkSKALANYERI_07.Checked
                .SKALANEYRI_08 = chkSKALANYERI_08.Checked
                .SKALANEYRI_09 = chkSKALANYERI_09.Checked
                .SKALANEYRI_10 = chkSKALANYERI_10.Checked
                .SKALANEYRI_KRONIS_01 = chkSKALANEYRI_KRONIS_01.Checked
                .SKALANEYRI_LOKASI_TEXT = txtSKALANEYRI_LOKASI_TEXT.Text
                .SKALANEYRI_ISTIRAHAT_TEXT = txtSKALANEYRI_ISTIRAHAT_TEXT.Text
                .SKALANEYRI_AKTIVITAS_TEXT = txtSKALANEYRI_AKTIVITAS_TEXT.Text
                .SKALANEYRI_AKUT_LOKASI_TEXT = txtSKALANEYRI_AKUT_LOKASI_TEXT.Text
                .SKALANEYRI_AKUT_ISTIRAHAT_TEXT = txtSKALANEYRI_AKUT_ISTIRAHAT_TEXT.Text
                .SKALANEYRI_AKUT_AKTIVITAS_TEXT = txtSKALANEYRI_AKUT_AKTIVITAS_TEXT.Text
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN = txtKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN.Text
                .EDUKASI_01 = chkEDUKASI_01.Checked
                .EDUKASI_02 = chkEDUKASI_02.Checked
                .EDUKASI_03 = chkEDUKASI_03.Checked
                .EDUKASI_03_TEXT = txtEDUKASI_03_TEXT.Text
                .BICARA_SEHARI2_01 = chkBICARA_SEHARI2_01.Checked
                .BICARA_SEHARI2_01_1 = chkBICARA_SEHARI2_01_1.Checked
                .BICARA_SEHARI2_01_2 = chkBICARA_SEHARI2_01_2.Checked
                .BICARA_SEHARI2_02 = chkBICARA_SEHARI2_02.Checked
                .BICARA_SEHARI2_02_1 = chkBICARA_SEHARI2_02_1.Checked
                .BICARA_SEHARI2_02_2 = chkBICARA_SEHARI2_02_2.Checked
                .BICARA_SEHARI2_03 = chkBICARA_SEHARI2_03.Checked
                .BICARA_SEHARI2_03_TEXT = txtBICARA_SEHARI2_03_TEXT.Text
                .BICARA_SEHARI2_04 = chkBICARA_SEHARI2_04.Checked
                .BICARA_SEHARI2_04_TEXT = txtBICARA_SEHARI2_04_TEXT.Text
                .PERLU_PENERJEMAH_01 = chkPERLU_PENERJEMAH_01.Checked
                .PERLU_PENERJEMAH_02 = chkPERLU_PENERJEMAH_02.Checked
                .BAHASA_ISYARAT_01 = chkBAHASA_ISYARAT_01.Checked
                .BAHASA_ISYARAT_02 = chkBAHASA_ISYARAT_02.Checked
                .HAMBATAN_EDUKASI_01 = chkHAMBATAN_EDUKASI_01.Checked
                .HAMBATAN_EDUKASI_02 = chkHAMBATAN_EDUKASI_02.Checked
                .HAMBATAN_EDUKASI_03 = chkHAMBATAN_EDUKASI_03.Checked
                .HAMBATAN_EDUKASI_04 = chkHAMBATAN_EDUKASI_04.Checked
                .HAMBATAN_EDUKASI_05 = chkHAMBATAN_EDUKASI_05.Checked
                .HAMBATAN_EDUKASI_06 = chkHAMBATAN_EDUKASI_06.Checked
                .HAMBATAN_EDUKASI_07 = chkHAMBATAN_EDUKASI_07.Checked
                .HAMBATAN_EDUKASI_08 = chkHAMBATAN_EDUKASI_08.Checked
                .HAMBATAN_EDUKASI_09 = chkHAMBATAN_EDUKASI_09.Checked
                .HAMBATAN_EDUKASI_10 = chkHAMBATAN_EDUKASI_10.Checked
                .HAMBATAN_EDUKASI_11 = chkHAMBATAN_EDUKASI_11.Checked
                .HAMBATAN_EDUKASI_12 = chkHAMBATAN_EDUKASI_12.Checked
                .USIA_KEHAMILAN = txtUSIA_KEHAMILAN.Text
                .BERAT_BADAN_LAHIR = txtBERAT_BADAN_LAHIR.Text
                .PERSALINAN_01 = chkPERSALINAN_01.Checked
                .PERSALINAN_02 = chkPERSALINAN_02.Checked
                .PERSALINAN_03 = chkPERSALINAN_03.Checked
                .PERSALINAN_04 = chkPERSALINAN_04.Checked
                .MENANGIS_01 = chkMENANGIS_01.Checked
                .MENANGIS_02 = chkMENANGIS_02.Checked
                .MENANGIS_03_TEXT = txtMENANGIS_03_TEXT.Text
                .JAUNDICE_01 = chkJAUNDICE_01.Checked
                .JAUNDICE_02 = chkJAUNDICE_02.Checked
                .RIWAYAT_KELAHIRAN_LAIN_LAIN = txtRIWAYAT_KELAHIRAN_LAIN_LAIN.Text
                .SOSIAL_01 = chkSOSIAL_01.Checked
                .SOSIAL_02 = chkSOSIAL_02.Checked
                .SOSIAL_03 = chkSOSIAL_03.Checked
                .SOSIAL_04 = chkSOSIAL_04.Checked
                .SOSIAL_05 = chkSOSIAL_05.Checked
                .SOSIAL_06 = chkSOSIAL_06.Checked
                .SOSIAL_07 = chkSOSIAL_07.Checked
                .SOSIAL_08 = chkSOSIAL_08.Checked
                .SOSIAL_09 = chkSOSIAL_09.Checked
                .SOSIAL_10 = chkSOSIAL_10.Checked
                .MOTORIK_HALUS_01 = chkMOTORIK_HALUS_01.Checked
                .MOTORIK_HALUS_02 = chkMOTORIK_HALUS_02.Checked
                .MOTORIK_HALUS_03 = chkMOTORIK_HALUS_03.Checked
                .MOTORIK_HALUS_04 = chkMOTORIK_HALUS_04.Checked
                .MOTORIK_HALUS_05 = chkMOTORIK_HALUS_05.Checked
                .MOTORIK_HALUS_06 = chkMOTORIK_HALUS_06.Checked
                .MOTORIK_HALUS_07 = chkMOTORIK_HALUS_07.Checked
                .MOTORIK_HALUS_08 = chkMOTORIK_HALUS_08.Checked
                .MOTORIK_HALUS_09 = chkMOTORIK_HALUS_09.Checked
                .MOTORIK_KASAR_01 = chkMOTORIK_KASAR_01.Checked
                .MOTORIK_KASAR_02 = chkMOTORIK_KASAR_02.Checked
                .MOTORIK_KASAR_03 = chkMOTORIK_KASAR_03.Checked
                .MOTORIK_KASAR_04 = chkMOTORIK_KASAR_04.Checked
                .MOTORIK_KASAR_05 = chkMOTORIK_KASAR_05.Checked
                .MOTORIK_KASAR_06 = chkMOTORIK_KASAR_06.Checked
                .MOTORIK_KASAR_07 = chkMOTORIK_KASAR_07.Checked
                .MOTORIK_KASAR_08 = chkMOTORIK_KASAR_08.Checked
                .MOTORIK_KASAR_09 = chkMOTORIK_KASAR_09.Checked
                .BAHASA_01 = chkBAHASA_01.Checked
                .BAHASA_02 = chkBAHASA_02.Checked
                .BAHASA_03 = chkBAHASA_03.Checked
                .BAHASA_04 = chkBAHASA_04.Checked
                .BAHASA_05 = chkBAHASA_05.Checked
                .BAHASA_06 = chkBAHASA_06.Checked
                .BAHASA_07 = chkBAHASA_07.Checked
                .BAHASA_08 = chkBAHASA_08.Checked
                .BAHASA_09 = chkBAHASA_09.Checked
                .MASALAH_KEPERAWATAN_01 = chkMASALAH_KEPERAWATAN_01.Checked
                .MASALAH_KEPERAWATAN_02 = chkMASALAH_KEPERAWATAN_02.Checked
                .MASALAH_KEPERAWATAN_02_1 = chkMASALAH_KEPERAWATAN_02_1.Checked
                .MASALAH_KEPERAWATAN_02_2 = chkMASALAH_KEPERAWATAN_02_2.Checked
                .MASALAH_KEPERAWATAN_02_3 = chkMASALAH_KEPERAWATAN_02_3.Checked
                .MASALAH_KEPERAWATAN_02_4 = chkMASALAH_KEPERAWATAN_02_4.Checked
                .MASALAH_KEPERAWATAN_02_5 = chkMASALAH_KEPERAWATAN_02_5.Checked
                .MASALAH_KEPERAWATAN_02_6 = chkMASALAH_KEPERAWATAN_02_6.Checked
                .MASALAH_KEPERAWATAN_02_7 = chkMASALAH_KEPERAWATAN_02_7.Checked
                .MASALAH_KEPERAWATAN_02_8 = chkMASALAH_KEPERAWATAN_02_8.Checked
                .MASALAH_KEPERAWATAN_02_9 = chkMASALAH_KEPERAWATAN_02_9.Checked
                .MASALAH_KEPERAWATAN_02_9_TEXT = txtMASALAH_KEPERAWATAN_02_9_TEXT.Text
                .MASALAH_KEPERAWATAN_03 = chkMASALAH_KEPERAWATAN_03.Checked
                .MASALAH_KEPERAWATAN_03_1 = chkMASALAH_KEPERAWATAN_03_1.Checked
                .MASALAH_KEPERAWATAN_03_2 = chkMASALAH_KEPERAWATAN_03_2.Checked
                .MASALAH_KEPERAWATAN_03_3 = chkMASALAH_KEPERAWATAN_03_3.Checked
                .MASALAH_KEPERAWATAN_03_4 = chkMASALAH_KEPERAWATAN_03_4.Checked
                .MASALAH_KEPERAWATAN_03_5 = chkMASALAH_KEPERAWATAN_03_5.Checked
                .MASALAH_KEPERAWATAN_03_5_TEXT = txtMASALAH_KEPERAWATAN_03_5_TEXT.Text
                .MASALAH_KEPERAWATAN_04 = chkMASALAH_KEPERAWATAN_04.Checked
                .MASALAH_KEPERAWATAN_04_1 = chkMASALAH_KEPERAWATAN_04_1.Checked
                .MASALAH_KEPERAWATAN_04_2 = chkMASALAH_KEPERAWATAN_04_2.Checked
                .MASALAH_KEPERAWATAN_04_3 = chkMASALAH_KEPERAWATAN_04_3.Checked
                .MASALAH_KEPERAWATAN_04_4 = chkMASALAH_KEPERAWATAN_04_4.Checked
                .MASALAH_KEPERAWATAN_04_4_TEXT = txtMASALAH_KEPERAWATAN_04_4_TEXT.Text
                .MASALAH_KEPERAWATAN_05 = chkMASALAH_KEPERAWATAN_05.Checked
                .MASALAH_KEPERAWATAN_05_1 = chkMASALAH_KEPERAWATAN_05_1.Checked
                .MASALAH_KEPERAWATAN_05_2 = chkMASALAH_KEPERAWATAN_05_2.Checked
                .MASALAH_KEPERAWATAN_05_2_TEXT = txtMASALAH_KEPERAWATAN_05_2_TEXT.Text
                .MASALAH_KEPERAWATAN_06 = chkMASALAH_KEPERAWATAN_06.Checked
                .MASALAH_KEPERAWATAN_06_1 = chkMASALAH_KEPERAWATAN_06_1.Checked
                .MASALAH_KEPERAWATAN_06_2 = chkMASALAH_KEPERAWATAN_06_2.Checked
                .MASALAH_KEPERAWATAN_06_2_TEXT = txtMASALAH_KEPERAWATAN_06_2_TEXT.Text
                .MASALAH_KEPERAWATAN_07 = chkMASALAH_KEPERAWATAN_07.Checked
                .MASALAH_KEPERAWATAN_07_1 = chkMASALAH_KEPERAWATAN_07_1.Checked
                .MASALAH_KEPERAWATAN_07_2 = chkMASALAH_KEPERAWATAN_07_2.Checked
                .MASALAH_KEPERAWATAN_07_3 = chkMASALAH_KEPERAWATAN_07_3.Checked
                .MASALAH_KEPERAWATAN_07_4 = chkMASALAH_KEPERAWATAN_07_4.Checked
                .MASALAH_KEPERAWATAN_07_4_TEXT = txtMASALAH_KEPERAWATAN_07_4_TEXT.Text
                .MASALAH_KEPERAWATAN_08 = chkMASALAH_KEPERAWATAN_08.Checked
                .MASALAH_KEPERAWATAN_08_1 = chkMASALAH_KEPERAWATAN_08_1.Checked
                .MASALAH_KEPERAWATAN_08_2 = chkMASALAH_KEPERAWATAN_08_2.Checked
                .MASALAH_KEPERAWATAN_08_3 = chkMASALAH_KEPERAWATAN_08_3.Checked
                .MASALAH_KEPERAWATAN_08_4 = chkMASALAH_KEPERAWATAN_08_4.Checked
                .MASALAH_KEPERAWATAN_08_5 = chkMASALAH_KEPERAWATAN_08_5.Checked
                .MASALAH_KEPERAWATAN_08_6 = chkMASALAH_KEPERAWATAN_08_6.Checked
                .MASALAH_KEPERAWATAN_08_6_TEXT = txtMASALAH_KEPERAWATAN_08_6_TEXT.Text
                .MASALAH_KEPERAWATAN_09 = chkMASALAH_KEPERAWATAN_09.Checked
                .MASALAH_KEPERAWATAN_09_1 = chkMASALAH_KEPERAWATAN_09_1.Checked
                .MASALAH_KEPERAWATAN_09_2 = chkMASALAH_KEPERAWATAN_09_2.Checked
                .MASALAH_KEPERAWATAN_09_3 = chkMASALAH_KEPERAWATAN_09_3.Checked
                .MASALAH_KEPERAWATAN_09_3_TEXT = txtMASALAH_KEPERAWATAN_09_3_TEXT.Text
                .MASALAH_KEPERAWATAN_10 = chkMASALAH_KEPERAWATAN_10.Checked
                .MASALAH_KEPERAWATAN_10_1 = chkMASALAH_KEPERAWATAN_10_1.Checked
                .MASALAH_KEPERAWATAN_10_2 = chkMASALAH_KEPERAWATAN_10_2.Checked
                .MASALAH_KEPERAWATAN_10_3 = chkMASALAH_KEPERAWATAN_10_3.Checked
                .MASALAH_KEPERAWATAN_10_3_TEXT = txtMASALAH_KEPERAWATAN_10_3_TEXT.Text
                .MASALAH_KEPERAWATAN_11 = chkMASALAH_KEPERAWATAN_11.Checked
                .MASALAH_KEPERAWATAN_11_TEXT = txtMASALAH_KEPERAWATAN_11_TEXT.Text
                .MASALAH_KEPERAWATAN_12_1 = chkMASALAH_KEPERAWATAN_12_1.Checked
                .MASALAH_KEPERAWATAN_12_1_TEXT = txtMASALAH_KEPERAWATAN_12_1_TEXT.Text
                .MASALAH_KEPERAWATAN_12_2 = chkMASALAH_KEPERAWATAN_12_2.Checked
                .MASALAH_KEPERAWATAN_12_2_TEXT = txtMASALAH_KEPERAWATAN_12_2_TEXT.Text
                .MASALAH_KEPERAWATAN_13 = chkMASALAH_KEPERAWATAN_13.Checked
                .MASALAH_KEPERAWATAN_13_TEXT = txtMASALAH_KEPERAWATAN_13_TEXT.Text
                .CATATAN_KEPERAWATAN_JAM = txtCATATAN_KEPERAWATAN_JAM.Text
                .INTERVENSI_IMPLEMENTASI_01_1 = chkINTERVENSI_IMPLEMENTASI_01_1.Checked
                .INTERVENSI_IMPLEMENTASI_01_TD = txtINTERVENSI_IMPLEMENTASI_01_TD.Text
                .INTERVENSI_IMPLEMENTASI_01_N = txtINTERVENSI_IMPLEMENTASI_01_N.Text
                .INTERVENSI_IMPLEMENTASI_01_S = txtINTERVENSI_IMPLEMENTASI_01_S.Text
                .INTERVENSI_IMPLEMENTASI_01_R = txtINTERVENSI_IMPLEMENTASI_01_R.Text
                .INTERVENSI_IMPLEMENTASI_01_2 = chkINTERVENSI_IMPLEMENTASI_01_2.Checked
                .INTERVENSI_IMPLEMENTASI_01_3 = chkINTERVENSI_IMPLEMENTASI_01_3.Checked
                .INTERVENSI_IMPLEMENTASI_01_4 = chkINTERVENSI_IMPLEMENTASI_01_4.Checked
                .INTERVENSI_IMPLEMENTASI_01_5 = chkINTERVENSI_IMPLEMENTASI_01_5.Checked
                .INTERVENSI_IMPLEMENTASI_01_6 = chkINTERVENSI_IMPLEMENTASI_01_6.Checked
                .INTERVENSI_IMPLEMENTASI_01_7 = chkINTERVENSI_IMPLEMENTASI_01_7.Checked
                .INTERVENSI_IMPLEMENTASI_01_7_TEXT_1 = txtINTERVENSI_IMPLEMENTASI_01_7_TEXT_1.Text
                .INTERVENSI_IMPLEMENTASI_01_7_TEXT_2 = txtINTERVENSI_IMPLEMENTASI_01_7_TEXT_2.Text
                .INTERVENSI_IMPLEMENTASI_01_8 = chkINTERVENSI_IMPLEMENTASI_01_8.Checked
                .INTERVENSI_IMPLEMENTASI_01_9 = chkINTERVENSI_IMPLEMENTASI_01_9.Checked
                .INTERVENSI_IMPLEMENTASI_02_1 = chkINTERVENSI_IMPLEMENTASI_02_1.Checked
                .INTERVENSI_IMPLEMENTASI_02_2 = chkINTERVENSI_IMPLEMENTASI_02_2.Checked
                .INTERVENSI_IMPLEMENTASI_02_3 = chkINTERVENSI_IMPLEMENTASI_02_3.Checked
                .INTERVENSI_IMPLEMENTASI_02_4 = chkINTERVENSI_IMPLEMENTASI_02_4.Checked
                .INTERVENSI_IMPLEMENTASI_02_5 = chkINTERVENSI_IMPLEMENTASI_02_5.Checked
                .INTERVENSI_IMPLEMENTASI_02_6 = chkINTERVENSI_IMPLEMENTASI_02_6.Checked
                .INTERVENSI_IMPLEMENTASI_02_7 = chkINTERVENSI_IMPLEMENTASI_02_7.Checked
                .INTERVENSI_IMPLEMENTASI_02_7_TEXT = txtINTERVENSI_IMPLEMENTASI_02_7_TEXT.Text
                .INTERVENSI_IMPLEMENTASI_03_1 = chkINTERVENSI_IMPLEMENTASI_03_1.Checked
                .INTERVENSI_IMPLEMENTASI_03_2 = chkINTERVENSI_IMPLEMENTASI_03_2.Checked
                .INTERVENSI_IMPLEMENTASI_03_3 = chkINTERVENSI_IMPLEMENTASI_03_3.Checked
                .INTERVENSI_IMPLEMENTASI_03_4 = chkINTERVENSI_IMPLEMENTASI_03_4.Checked
                .INTERVENSI_IMPLEMENTASI_03_5 = chkINTERVENSI_IMPLEMENTASI_03_5.Checked
                .INTERVENSI_IMPLEMENTASI_03_5_TEXT = txtINTERVENSI_IMPLEMENTASI_03_5_TEXT.Text
                .INTERVENSI_IMPLEMENTASI_03_6 = chkINTERVENSI_IMPLEMENTASI_03_6.Checked
                .INTERVENSI_IMPLEMENTASI_03_6_TEXT = txtINTERVENSI_IMPLEMENTASI_03_6_TEXT.Text
                .INTERVENSI_IMPLEMENTASI_04_1 = chkINTERVENSI_IMPLEMENTASI_04_1.Checked
                .INTERVENSI_IMPLEMENTASI_04_2 = chkINTERVENSI_IMPLEMENTASI_04_2.Checked
                .INTERVENSI_IMPLEMENTASI_04_3 = chkINTERVENSI_IMPLEMENTASI_04_3.Checked
                .INTERVENSI_IMPLEMENTASI_04_4 = chkINTERVENSI_IMPLEMENTASI_04_4.Checked
                .INTERVENSI_IMPLEMENTASI_04_4_TEXT = txtINTERVENSI_IMPLEMENTASI_04_4_TEXT.Text
                .INTERVENSI_IMPLEMENTASI_04_5 = chkINTERVENSI_IMPLEMENTASI_04_5.Checked
                .INTERVENSI_IMPLEMENTASI_04_6 = chkINTERVENSI_IMPLEMENTASI_04_6.Checked
                .INTERVENSI_IMPLEMENTASI_04_7 = chkINTERVENSI_IMPLEMENTASI_04_7.Checked
                .INTERVENSI_IMPLEMENTASI_04_8 = chkINTERVENSI_IMPLEMENTASI_04_8.Checked
                .INTERVENSI_IMPLEMENTASI_04_9 = chkINTERVENSI_IMPLEMENTASI_04_9.Checked
                .INTERVENSI_IMPLEMENTASI_05_1 = chkINTERVENSI_IMPLEMENTASI_05_1.Checked
                .INTERVENSI_IMPLEMENTASI_05_2 = chkINTERVENSI_IMPLEMENTASI_05_2.Checked
                .INTERVENSI_IMPLEMENTASI_05_3 = chkINTERVENSI_IMPLEMENTASI_05_3.Checked
                .INTERVENSI_IMPLEMENTASI_05_4 = chkINTERVENSI_IMPLEMENTASI_05_4.Checked
                .INTERVENSI_IMPLEMENTASI_05_5 = chkINTERVENSI_IMPLEMENTASI_05_5.Checked
                .INTERVENSI_IMPLEMENTASI_05_6 = chkINTERVENSI_IMPLEMENTASI_05_6.Checked
                .INTERVENSI_IMPLEMENTASI_05_6_TEXT = txtINTERVENSI_IMPLEMENTASI_05_6_TEXT.Text
                .HASIL_PENANGANAN_01 = chkHASIL_PENANGANAN_01.Checked
                .HASIL_PENANGANAN_02 = chkHASIL_PENANGANAN_02.Checked
                .HASIL_PENANGANAN_03 = chkHASIL_PENANGANAN_03.Checked
                .HASIL_PENANGANAN_04 = chkHASIL_PENANGANAN_04.Checked
                .HASIL_PENANGANAN_05 = chkHASIL_PENANGANAN_05.Checked
                .REPRODUKSI_01_1 = chkREPRODUKSI_01_1.Checked
                .REPRODUKSI_01_2 = chkREPRODUKSI_01_2.Checked
                .REPRODUKSI_02_1 = chkREPRODUKSI_02_1.Checked
                .REPRODUKSI_02_2 = chkREPRODUKSI_02_2.Checked
                .UMUR_01 = chkUMUR_01.Checked
                .UMUR_02 = chkUMUR_02.Checked
                .UMUR_03 = chkUMUR_03.Checked
                .UMUR_04 = chkUMUR_04.Checked
                .UMUR_05 = chkUMUR_05.Checked
                .UMUR_06 = chkUMUR_06.Checked
                .UMUR_07 = chkUMUR_07.Checked
                .UMUR_08 = chkUMUR_08.Checked
                .UMUR_09 = chkUMUR_09.Checked
                .UMUR_10 = chkUMUR_10.Checked
                .MASALAH_KEPERAWATAN_01_AKTUAL = chkMASALAH_KEPERAWATAN_01_AKTUAL.Checked
                .MASALAH_KEPERAWATAN_01_POTENSIAL = chkMASALAH_KEPERAWATAN_01_POTENSIAL.Checked
                .MASALAH_KEPERAWATAN_02_AKTUAL = chkMASALAH_KEPERAWATAN_02_AKTUAL.Checked
                .MASALAH_KEPERAWATAN_02_POTENSIAL = chkMASALAH_KEPERAWATAN_02_POTENSIAL.Checked
                .MASALAH_KEPERAWATAN_03_AKTUAL = chkMASALAH_KEPERAWATAN_03_AKTUAL.Checked
                .MASALAH_KEPERAWATAN_03_POTENSIAL = chkMASALAH_KEPERAWATAN_03_POTENSIAL.Checked
                .MASALAH_KEPERAWATAN_04_AKTUAL = chkMASALAH_KEPERAWATAN_04_AKTUAL.Checked
                .MASALAH_KEPERAWATAN_04_POTENSIAL = chkMASALAH_KEPERAWATAN_04_POTENSIAL.Checked
                .MASALAH_KEPERAWATAN_05_AKTUAL = chkMASALAH_KEPERAWATAN_05_AKTUAL.Checked
                .MASALAH_KEPERAWATAN_05_POTENSIAL = chkMASALAH_KEPERAWATAN_05_POTENSIAL.Checked
                .MASALAH_KEPERAWATAN_06_AKTUAL = chkMASALAH_KEPERAWATAN_06_AKTUAL.Checked
                .MASALAH_KEPERAWATAN_06_POTENSIAL = chkMASALAH_KEPERAWATAN_06_POTENSIAL.Checked
                .MASALAH_KEPERAWATAN_07_AKTUAL = chkMASALAH_KEPERAWATAN_07_AKTUAL.Checked
                .MASALAH_KEPERAWATAN_07_POTENSIAL = chkMASALAH_KEPERAWATAN_07_POTENSIAL.Checked
                .MASALAH_KEPERAWATAN_08_AKTUAL = chkMASALAH_KEPERAWATAN_08_AKTUAL.Checked
                .MASALAH_KEPERAWATAN_08_POTENSIAL = chkMASALAH_KEPERAWATAN_08_POTENSIAL.Checked
                .MASALAH_KEPERAWATAN_09_AKTUAL = chkMASALAH_KEPERAWATAN_09_AKTUAL.Checked
                .MASALAH_KEPERAWATAN_09_POTENSIAL = chkMASALAH_KEPERAWATAN_09_POTENSIAL.Checked
                .MASALAH_KEPERAWATAN_10_AKTUAL = chkMASALAH_KEPERAWATAN_10_AKTUAL.Checked
                .MASALAH_KEPERAWATAN_10_POTENSIAL = chkMASALAH_KEPERAWATAN_10_POTENSIAL.Checked
                .MASALAH_KEPERAWATAN_10_RESIKO = chkMASALAH_KEPERAWATAN_10_RESIKO.Checked
                .MASALAH_KEPERAWATAN_08_NYERI = chkMASALAH_KEPERAWATAN_08_NYERI.Checked
                .MASALAH_KEPERAWATAN_08_PUSING = chkMASALAH_KEPERAWATAN_08_PUSING.Checked
                .MASALAH_KEPERAWATAN_08_GATAL = chkMASALAH_KEPERAWATAN_08_GATAL.Checked
                .TANDA_VITAL_REGULER = chkTANDA_VITAL_REGULER.Checked
                .TANDA_VITAL_IREGULER = chkTANDA_VITAL_IREGULER.Checked
                .REPRODUKSI_03_1 = chkREPRODUKSI_03_1.Checked
                .REPRODUKSI_03_2 = chkREPRODUKSI_03_2.Checked
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01 = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01.Checked
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02 = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02.Checked
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03 = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03.Checked
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04 = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04.Checked

                .DOKTER_KODE = sKODEDOKTER
                .DOKTER_NAMEDISPLAY = txtDokter.Text

                Try
                    .CETAK = oS_DIGITAL_RJ_03.GetData(txtNoRegister.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                Try
                    Dim oSetUser As New Setting.clsUser
                    Dim dsUser = oSetUser.GetData(sUserID)
                    If dsUser.ISOTORTY = True Then
                        .KDUSER = oS_DIGITAL_RJ_03.GetData(txtNoRegister.Text).KDUSER
                        .KDUSER_SIGNATURE = oS_DIGITAL_RJ_03.GetData(txtNoRegister.Text).KDUSER_SIGNATURE
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
                    fn_Save = oS_DIGITAL_RJ_03.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_RJ_03.UpdateData(ds)
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
    Private Sub cboSAATPULANG_E_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_E.SelectedIndexChanged, cbo_M.SelectedIndexChanged, cbo_V.SelectedIndexChanged
        If isLoad = True Then
            Dim sE As Decimal = 0
            Dim sM As Decimal = 0
            Dim sV As Decimal = 0

            If cbo_E.Text <> String.Empty Then
                sE = CDec(cbo_E.Text)
            End If

            If cbo_M.Text <> String.Empty Then
                sM = CDec(cbo_M.Text)
            End If

            If cbo_V.Text <> String.Empty Then
                sV = CDec(cbo_V.Text)
            End If

            txt_GCS.Text = sE + sM + sV

        End If
    End Sub

    Private Sub frmEMedrekRJ_03_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
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