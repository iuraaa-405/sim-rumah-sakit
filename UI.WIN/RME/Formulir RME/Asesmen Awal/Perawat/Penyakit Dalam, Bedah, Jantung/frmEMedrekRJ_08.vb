Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Data.SqlClient

Public Class frmEMedrekRJ_08
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_RJ_08 As New Digital.clsDigital_RJ_08
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
        Me.Text = EMedrekRJ_08.TITLE
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
        sCode = txtNoRegister.Text.Trim.ToUpper
        sCPPTAKTIVE = ""
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_ChangeFormState()
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
        chkALERGI_01.Properties.ReadOnly = Status
        chkALERGI_02.Properties.ReadOnly = Status
        txtALERGI_02_TEXT.Properties.ReadOnly = Status
        chkAUTO.Properties.ReadOnly = Status
        chkALLO.Properties.ReadOnly = Status
        txtALLO_TEXT.Properties.ReadOnly = Status
        txtKELUHAN_UTAMA.Properties.ReadOnly = Status
        txtKESADARAN_UMUM.Properties.ReadOnly = Status
        txtTANDA_VITAL_01.Properties.ReadOnly = Status
        txtTANDA_VITAL_02.Properties.ReadOnly = Status
        txtTANDA_VITAL_03.Properties.ReadOnly = Status
        txtTANDA_VITAL_04.Properties.ReadOnly = Status
        txtTANDA_VITAL_05.Properties.ReadOnly = Status
        txtTANDA_VITAL_06.Properties.ReadOnly = Status
        txtTANDA_VITAL_07.Properties.ReadOnly = Status
        txtTANDA_VITAL_08.Properties.ReadOnly = Status
        txtTANDA_VITAL_09.Properties.ReadOnly = Status
        chkTANDA_VITAL_REGULER.Properties.ReadOnly = Status
        chkTANDA_VITAL_IREGULER.Properties.ReadOnly = Status
        chkSTATUS_PSIKOLOGIS_01.Properties.ReadOnly = Status
        chkSTATUS_PSIKOLOGIS_02.Properties.ReadOnly = Status
        chkSTATUS_PSIKOLOGIS_03.Properties.ReadOnly = Status
        chkSTATUS_PSIKOLOGIS_04.Properties.ReadOnly = Status
        chkSTATUS_PSIKOLOGIS_05.Properties.ReadOnly = Status
        chkSTATUS_PSIKOLOGIS_06.Properties.ReadOnly = Status
        txtSTATUS_PSIKOLOGIS_06_TEXT.Properties.ReadOnly = Status
        chkSTATUS_MENTAL_01.Properties.ReadOnly = Status
        chkSTATUS_MENTAL_02.Properties.ReadOnly = Status
        txtSTATUS_MENTAL_02_TEXT.Properties.ReadOnly = Status
        chkSTATUS_MENTAL_03.Properties.ReadOnly = Status
        txtSTATUS_MENTAL_03_TEXT.Properties.ReadOnly = Status
        chkSOSIAL_01_1.Properties.ReadOnly = Status
        chkSOSIAL_01_2.Properties.ReadOnly = Status
        chkSOSIAL_02_1.Properties.ReadOnly = Status
        chkSOSIAL_02_2.Properties.ReadOnly = Status
        chkSOSIAL_02_3.Properties.ReadOnly = Status
        chkSOSIAL_02_4.Properties.ReadOnly = Status
        txtSOSIAL_02_4_TEXT.Properties.ReadOnly = Status
        txtSOSIAL_03_1_TEXT.Properties.ReadOnly = Status
        txtSOSIAL_03_2_TEXT.Properties.ReadOnly = Status
        txtSOSIAL_03_3_TEXT.Properties.ReadOnly = Status
        txtSTATUS_SPIRITUAL_01_TEXT.Properties.ReadOnly = Status
        txtSTATUS_SPIRITUAL_02_TEXT.Properties.ReadOnly = Status
        chkPERSYARAFAN_01.Properties.ReadOnly = Status
        chkPERSYARAFAN_02.Properties.ReadOnly = Status
        chkPERSYARAFAN_03.Properties.ReadOnly = Status
        chkPERSYARAFAN_04.Properties.ReadOnly = Status
        chkPERSYARAFAN_04_1.Properties.ReadOnly = Status
        PERSYARAFAN_04_2.Properties.ReadOnly = Status
        chkPERSYARAFAN_05.Properties.ReadOnly = Status
        txtPERSYARAFAN_05_TEXT.Properties.ReadOnly = Status
        chkPERNAPASAN_01.Properties.ReadOnly = Status
        chkPERNAPASAN_02.Properties.ReadOnly = Status
        chkPERNAPASAN_03.Properties.ReadOnly = Status
        chkPERNAPASAN_04.Properties.ReadOnly = Status
        txtPERNAPASAN_04_TEXT.Properties.ReadOnly = Status
        chkPENCERNAAN_01.Properties.ReadOnly = Status
        chkPENCERNAAN_02.Properties.ReadOnly = Status
        chkPENCERNAAN_02_1.Properties.ReadOnly = Status
        chkPENCERNAAN_02_2.Properties.ReadOnly = Status
        chkPENCERNAAN_03.Properties.ReadOnly = Status
        chkPENCERNAAN_04.Properties.ReadOnly = Status
        chkPENCERNAAN_05.Properties.ReadOnly = Status
        txtPENCERNAAN_05_TEXT.Properties.ReadOnly = Status
        chkENDROKIN_01.Properties.ReadOnly = Status
        chkENDROKIN_02.Properties.ReadOnly = Status
        chkENDROKIN_03.Properties.ReadOnly = Status
        chkENDROKIN_04.Properties.ReadOnly = Status
        chkENDROKIN_05.Properties.ReadOnly = Status
        txtENDROKIN_05_TEXT.Properties.ReadOnly = Status
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
        chkREPRODUKSI_01_1.Properties.ReadOnly = Status
        chkREPRODUKSI_01_2.Properties.ReadOnly = Status
        chkREPRODUKSI_02.Properties.ReadOnly = Status
        chkREPRODUKSI_02_1.Properties.ReadOnly = Status
        txtREPRODUKSI_02_1_TEXT.Properties.ReadOnly = Status
        chkREPRODUKSI_02_2.Properties.ReadOnly = Status
        chkREPRODUKSI_03.Properties.ReadOnly = Status
        chkREPRODUKSI_03_1.Properties.ReadOnly = Status
        txtREPRODUKSI_03_1_TEXT.Properties.ReadOnly = Status
        chkREPRODUKSI_03_2.Properties.ReadOnly = Status
        chkREPRODUKSI_04.Properties.ReadOnly = Status
        txtREPRODUKSI_04_TEXT.Properties.ReadOnly = Status
        chkREPRODUKSI_05.Properties.ReadOnly = Status
        txtREPRODUKSI_05_TEXT.Properties.ReadOnly = Status
        txtREPRODUKSI_06_TEXT.Properties.ReadOnly = Status
        txtREPRODUKSI_07_TEXT.Properties.ReadOnly = Status
        txtREPRODUKSI_08_TEXT.Properties.ReadOnly = Status
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
        txtURINARIA_04_TEXT.Properties.ReadOnly = Status
        chkKEADAAN_EMOSIONAL_01.Properties.ReadOnly = Status
        chkKEADAAN_EMOSIONAL_02.Properties.ReadOnly = Status
        chkKEADAAN_EMOSIONAL_03.Properties.ReadOnly = Status
        chkKEADAAN_EMOSIONAL_04.Properties.ReadOnly = Status
        chkKEADAAN_EMOSIONAL_05.Properties.ReadOnly = Status
        txtKEADAAN_EMOSIONAL_05_TEXT.Properties.ReadOnly = Status
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
        chkHIDUNG_MUKA_01.Properties.ReadOnly = Status
        chkHIDUNG_MUKA_02.Properties.ReadOnly = Status
        txtHIDUNG_MUKA_02_TEXT.Properties.ReadOnly = Status
        chkHIDUNG_MUKA_03.Properties.ReadOnly = Status
        txtHIDUNG_MUKA_03_TEXT.Properties.ReadOnly = Status
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
        chkSKALANEYRI_AKUT_01.Properties.ReadOnly = Status
        txtSKALANEYRI_AKUT_LOKASI_TEXT.Properties.ReadOnly = Status
        txtSKALANEYRI_AKUT_ISTIRAHAT_TEXT.Properties.ReadOnly = Status
        txtSKALANEYRI_AKUT_AKTIVITAS_TEXT.Properties.ReadOnly = Status
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01.Properties.ReadOnly = Status
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02.Properties.ReadOnly = Status
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03.Properties.ReadOnly = Status
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04.Properties.ReadOnly = Status
        txtKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04_TEXT.Properties.ReadOnly = Status
        chkKEBUTUHAN_EDUKASI_01.Properties.ReadOnly = Status
        chkKEBUTUHAN_EDUKASI_02.Properties.ReadOnly = Status
        chkKEBUTUHAN_EDUKASI_03.Properties.ReadOnly = Status
        chkKEBUTUHAN_EDUKASI_04.Properties.ReadOnly = Status
        txtKEBUTUHAN_EDUKASI_04_TEXT.Properties.ReadOnly = Status
        'chkALERGI_02_01.Properties.ReadOnly = Status
        'chkALERGI_02_02.Properties.ReadOnly = Status
        'txtALERGI_02_02_TEXT.Properties.ReadOnly = Status
        txtKEADAAN.Properties.ReadOnly = Status
        txtPEMERIKSAAN_PENUNJANG_01.Properties.ReadOnly = Status
        chkPEMERIKSAAN_PENUNJANG_02.Properties.ReadOnly = Status
        chkPEMERIKSAAN_PENUNJANG_03.Properties.ReadOnly = Status
        chkPEMERIKSAAN_PENUNJANG_04.Properties.ReadOnly = Status
        txtPEMERIKSAAN_PENUNJANG_04_TEXT.Properties.ReadOnly = Status
        txtRIWAYAT_PENYAKIT.Properties.ReadOnly = Status
        chkRIWAYAT_IMUNISASI_01.Properties.ReadOnly = Status
        txtRIWAYAT_IMUNISASI_01_TEXT.Properties.ReadOnly = Status
        chkRIWAYAT_IMUNISASI_02.Properties.ReadOnly = Status
        txtRIWAYAT_IMUNISASI_02_TEXT.Properties.ReadOnly = Status
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
        txtPERENCANAAN_PASIEN_PULANG_01.Properties.ReadOnly = Status
        chkPERENCANAAN_PASIEN_PULANG_02_1.Properties.ReadOnly = Status
        chkPERENCANAAN_PASIEN_PULANG_02_2.Properties.ReadOnly = Status
        chkPERENCANAAN_PASIEN_PULANG_02_3.Properties.ReadOnly = Status
        chkPERENCANAAN_PASIEN_PULANG_02_4.Properties.ReadOnly = Status
        txtPERENCANAAN_PASIEN_PULANG_02_4_TEXT.Properties.ReadOnly = Status
        chkPERENCANAAN_PASIEN_PULANG_03_1.Properties.ReadOnly = Status
        chkPERENCANAAN_PASIEN_PULANG_03_1_1.Properties.ReadOnly = Status
        chkPERENCANAAN_PASIEN_PULANG_03_1_2.Properties.ReadOnly = Status
        chkPERENCANAAN_PASIEN_PULANG_03_2.Properties.ReadOnly = Status
        chkPERENCANAAN_PASIEN_PULANG_03_3.Properties.ReadOnly = Status
        chkPERENCANAAN_PASIEN_PULANG_03_4.Properties.ReadOnly = Status
        chkPERENCANAAN_PASIEN_PULANG_03_5.Properties.ReadOnly = Status
        chkPERENCANAAN_PASIEN_PULANG_03_5_1.Properties.ReadOnly = Status
        chkPERENCANAAN_PASIEN_PULANG_03_5_2.Properties.ReadOnly = Status
        chkPERENCANAAN_PASIEN_PULANG_03_6.Properties.ReadOnly = Status
        chkPERENCANAAN_PASIEN_PULANG_03_7.Properties.ReadOnly = Status
        'grdNOIDUSER.Properties.ReadOnly = Status
        'txtPASIEN_KELUARGA.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        'txtCODE.Text = "<--- AUTO --->"

        deDATE.DateTime = Now
        chkALERGI_01.Checked = False
        chkALERGI_02.Checked = False
        txtALERGI_02_TEXT.ResetText()
        chkAUTO.Checked = False
        chkALLO.Checked = False
        txtALLO_TEXT.ResetText()
        txtKELUHAN_UTAMA.ResetText()
        txtKESADARAN_UMUM.ResetText()
        txtTANDA_VITAL_01.ResetText()
        txtTANDA_VITAL_02.ResetText()
        txtTANDA_VITAL_03.ResetText()
        txtTANDA_VITAL_04.ResetText()
        txtTANDA_VITAL_05.ResetText()
        txtTANDA_VITAL_06.ResetText()
        txtTANDA_VITAL_07.ResetText()
        txtTANDA_VITAL_08.ResetText()
        txtTANDA_VITAL_09.ResetText()
        chkTANDA_VITAL_REGULER.Checked = False
        chkTANDA_VITAL_IREGULER.Checked = False
        chkSTATUS_PSIKOLOGIS_01.Checked = False
        chkSTATUS_PSIKOLOGIS_02.Checked = False
        chkSTATUS_PSIKOLOGIS_03.Checked = False
        chkSTATUS_PSIKOLOGIS_04.Checked = False
        chkSTATUS_PSIKOLOGIS_05.Checked = False
        chkSTATUS_PSIKOLOGIS_06.Checked = False
        txtSTATUS_PSIKOLOGIS_06_TEXT.ResetText()
        chkSTATUS_MENTAL_01.Checked = False
        chkSTATUS_MENTAL_02.Checked = False
        txtSTATUS_MENTAL_02_TEXT.ResetText()
        chkSTATUS_MENTAL_03.Checked = False
        txtSTATUS_MENTAL_03_TEXT.ResetText()
        chkSOSIAL_01_1.Checked = False
        chkSOSIAL_01_2.Checked = False
        chkSOSIAL_02_1.Checked = False
        chkSOSIAL_02_2.Checked = False
        chkSOSIAL_02_3.Checked = False
        chkSOSIAL_02_4.Checked = False
        txtSOSIAL_02_4_TEXT.ResetText()
        txtSOSIAL_03_1_TEXT.ResetText()
        txtSOSIAL_03_2_TEXT.ResetText()
        txtSOSIAL_03_3_TEXT.ResetText()
        txtSTATUS_SPIRITUAL_01_TEXT.ResetText()
        txtSTATUS_SPIRITUAL_02_TEXT.ResetText()
        chkPERSYARAFAN_01.Checked = False
        chkPERSYARAFAN_02.Checked = False
        chkPERSYARAFAN_03.Checked = False
        chkPERSYARAFAN_04.Checked = False
        chkPERSYARAFAN_04_1.Checked = False
        PERSYARAFAN_04_2.Checked = False
        chkPERSYARAFAN_05.Checked = False
        txtPERSYARAFAN_05_TEXT.ResetText()
        chkPERNAPASAN_01.Checked = False
        chkPERNAPASAN_02.Checked = False
        chkPERNAPASAN_03.Checked = False
        chkPERNAPASAN_04.Checked = False
        txtPERNAPASAN_04_TEXT.ResetText()
        chkPENCERNAAN_01.Checked = False
        chkPENCERNAAN_02.Checked = False
        chkPENCERNAAN_02_1.Checked = False
        chkPENCERNAAN_02_2.Checked = False
        chkPENCERNAAN_03.Checked = False
        chkPENCERNAAN_04.Checked = False
        chkPENCERNAAN_05.Checked = False
        txtPENCERNAAN_05_TEXT.ResetText()
        chkENDROKIN_01.Checked = False
        chkENDROKIN_02.Checked = False
        chkENDROKIN_03.Checked = False
        chkENDROKIN_04.Checked = False
        chkENDROKIN_05.Checked = False
        txtENDROKIN_05_TEXT.ResetText()
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
        chkREPRODUKSI_01_1.Checked = False
        chkREPRODUKSI_01_2.Checked = False
        chkREPRODUKSI_02.Checked = False
        chkREPRODUKSI_02_1.Checked = False
        txtREPRODUKSI_02_1_TEXT.ResetText()
        chkREPRODUKSI_02_2.Checked = False
        chkREPRODUKSI_03.Checked = False
        chkREPRODUKSI_03_1.Checked = False
        txtREPRODUKSI_03_1_TEXT.ResetText()
        chkREPRODUKSI_03_2.Checked = False
        chkREPRODUKSI_04.Checked = False
        txtREPRODUKSI_04_TEXT.ResetText()
        chkREPRODUKSI_05.Checked = False
        txtREPRODUKSI_05_TEXT.ResetText()
        txtREPRODUKSI_06_TEXT.ResetText()
        txtREPRODUKSI_07_TEXT.ResetText()
        txtREPRODUKSI_08_TEXT.ResetText()
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
        txtURINARIA_04_TEXT.ResetText()
        chkKEADAAN_EMOSIONAL_01.Checked = False
        chkKEADAAN_EMOSIONAL_02.Checked = False
        chkKEADAAN_EMOSIONAL_03.Checked = False
        chkKEADAAN_EMOSIONAL_04.Checked = False
        chkKEADAAN_EMOSIONAL_05.Checked = False
        txtKEADAAN_EMOSIONAL_05_TEXT.ResetText()
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
        chkHIDUNG_MUKA_01.Checked = False
        chkHIDUNG_MUKA_02.Checked = False
        txtHIDUNG_MUKA_02_TEXT.ResetText()
        chkHIDUNG_MUKA_03.Checked = False
        txtHIDUNG_MUKA_03_TEXT.ResetText()
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
        chkSKALANEYRI_AKUT_01.Checked = False
        txtSKALANEYRI_AKUT_LOKASI_TEXT.ResetText()
        txtSKALANEYRI_AKUT_ISTIRAHAT_TEXT.ResetText()
        txtSKALANEYRI_AKUT_AKTIVITAS_TEXT.ResetText()
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01.Checked = False
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02.Checked = False
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03.Checked = False
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04.Checked = False
        txtKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04_TEXT.ResetText()
        chkKEBUTUHAN_EDUKASI_01.Checked = False
        chkKEBUTUHAN_EDUKASI_02.Checked = False
        chkKEBUTUHAN_EDUKASI_03.Checked = False
        chkKEBUTUHAN_EDUKASI_04.Checked = False
        txtKEBUTUHAN_EDUKASI_04_TEXT.ResetText()
        'chkALERGI_02_01.Checked = False
        'chkALERGI_02_02.Checked = False
        'txtALERGI_02_02_TEXT.ResetText()
        txtKEADAAN.ResetText()
        txtPEMERIKSAAN_PENUNJANG_01.ResetText()
        chkPEMERIKSAAN_PENUNJANG_02.Checked = False
        chkPEMERIKSAAN_PENUNJANG_03.Checked = False
        chkPEMERIKSAAN_PENUNJANG_04.Checked = False
        txtPEMERIKSAAN_PENUNJANG_04_TEXT.ResetText()
        txtRIWAYAT_PENYAKIT.ResetText()
        chkRIWAYAT_IMUNISASI_01.Checked = False
        txtRIWAYAT_IMUNISASI_01_TEXT.ResetText()
        chkRIWAYAT_IMUNISASI_02.Checked = False
        txtRIWAYAT_IMUNISASI_02_TEXT.ResetText()
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
        txtPERENCANAAN_PASIEN_PULANG_01.ResetText()
        chkPERENCANAAN_PASIEN_PULANG_02_1.Checked = False
        chkPERENCANAAN_PASIEN_PULANG_02_2.Checked = False
        chkPERENCANAAN_PASIEN_PULANG_02_3.Checked = False
        chkPERENCANAAN_PASIEN_PULANG_02_4.Checked = False
        txtPERENCANAAN_PASIEN_PULANG_02_4_TEXT.ResetText()
        chkPERENCANAAN_PASIEN_PULANG_03_1.Checked = False
        chkPERENCANAAN_PASIEN_PULANG_03_1_1.Checked = False
        chkPERENCANAAN_PASIEN_PULANG_03_1_2.Checked = False
        chkPERENCANAAN_PASIEN_PULANG_03_2.Checked = False
        chkPERENCANAAN_PASIEN_PULANG_03_3.Checked = False
        chkPERENCANAAN_PASIEN_PULANG_03_4.Checked = False
        chkPERENCANAAN_PASIEN_PULANG_03_5.Checked = False
        chkPERENCANAAN_PASIEN_PULANG_03_5_1.Checked = False
        chkPERENCANAAN_PASIEN_PULANG_03_5_2.Checked = False
        chkPERENCANAAN_PASIEN_PULANG_03_6.Checked = False
        chkPERENCANAAN_PASIEN_PULANG_03_7.Checked = False
        'txtPASIEN_KELUARGA.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RJ_08.GetData(txtNoRegister.Text)

            With ds

                deDATE.DateTime = .DATE
                chkALERGI_01.Checked = .ALERGI_01
                chkALERGI_02.Checked = .ALERGI_02
                txtALERGI_02_TEXT.Text = .ALERGI_02_TEXT
                chkAUTO.Checked = .AUTO
                chkALLO.Checked = .ALLO
                txtALLO_TEXT.Text = .ALLO_TEXT
                txtKELUHAN_UTAMA.Text = .KELUHAN_UTAMA
                txtKESADARAN_UMUM.Text = .KESADARAN_UMUM
                txtTANDA_VITAL_01.Text = .TANDA_VITAL_01
                txtTANDA_VITAL_02.Text = .TANDA_VITAL_02
                txtTANDA_VITAL_03.Text = .TANDA_VITAL_03
                txtTANDA_VITAL_04.Text = .TANDA_VITAL_04
                txtTANDA_VITAL_05.Text = .TANDA_VITAL_05
                txtTANDA_VITAL_06.Text = .TANDA_VITAL_06
                txtTANDA_VITAL_07.Text = .TANDA_VITAL_07
                txtTANDA_VITAL_08.Text = .TANDA_VITAL_08
                txtTANDA_VITAL_09.Text = .TANDA_VITAL_09
                chkTANDA_VITAL_REGULER.Checked = .TANDA_VITAL_REGULER
                chkTANDA_VITAL_IREGULER.Checked = .TANDA_VITAL_IREGULER
                chkSTATUS_PSIKOLOGIS_01.Checked = .STATUS_PSIKOLOGIS_01
                chkSTATUS_PSIKOLOGIS_02.Checked = .STATUS_PSIKOLOGIS_02
                chkSTATUS_PSIKOLOGIS_03.Checked = .STATUS_PSIKOLOGIS_03
                chkSTATUS_PSIKOLOGIS_04.Checked = .STATUS_PSIKOLOGIS_04
                chkSTATUS_PSIKOLOGIS_05.Checked = .STATUS_PSIKOLOGIS_05
                chkSTATUS_PSIKOLOGIS_06.Checked = .STATUS_PSIKOLOGIS_06
                txtSTATUS_PSIKOLOGIS_06_TEXT.Text = .STATUS_PSIKOLOGIS_06_TEXT
                chkSTATUS_MENTAL_01.Checked = .STATUS_MENTAL_01
                chkSTATUS_MENTAL_02.Checked = .STATUS_MENTAL_02
                txtSTATUS_MENTAL_02_TEXT.Text = .STATUS_MENTAL_02_TEXT
                chkSTATUS_MENTAL_03.Checked = .STATUS_MENTAL_03
                txtSTATUS_MENTAL_03_TEXT.Text = .STATUS_MENTAL_03_TEXT
                chkSOSIAL_01_1.Checked = .SOSIAL_01_1
                chkSOSIAL_01_2.Checked = .SOSIAL_01_2
                chkSOSIAL_02_1.Checked = .SOSIAL_02_1
                chkSOSIAL_02_2.Checked = .SOSIAL_02_2
                chkSOSIAL_02_3.Checked = .SOSIAL_02_3
                chkSOSIAL_02_4.Checked = .SOSIAL_02_4
                txtSOSIAL_02_4_TEXT.Text = .SOSIAL_02_4_TEXT
                txtSOSIAL_03_1_TEXT.Text = .SOSIAL_03_1_TEXT
                txtSOSIAL_03_2_TEXT.Text = .SOSIAL_03_2_TEXT
                txtSOSIAL_03_3_TEXT.Text = .SOSIAL_03_3_TEXT
                txtSTATUS_SPIRITUAL_01_TEXT.Text = .STATUS_SPIRITUAL_01_TEXT
                txtSTATUS_SPIRITUAL_02_TEXT.Text = .STATUS_SPIRITUAL_02_TEXT
                chkPERSYARAFAN_01.Checked = .PERSYARAFAN_01
                chkPERSYARAFAN_02.Checked = .PERSYARAFAN_02
                chkPERSYARAFAN_03.Checked = .PERSYARAFAN_03
                chkPERSYARAFAN_04.Checked = .PERSYARAFAN_04
                chkPERSYARAFAN_04_1.Checked = .PERSYARAFAN_04_1
                PERSYARAFAN_04_2.Checked = .PERSYARAFAN_04_2
                chkPERSYARAFAN_05.Checked = .PERSYARAFAN_05
                txtPERSYARAFAN_05_TEXT.Text = .PERSYARAFAN_05_TEXT
                chkPERNAPASAN_01.Checked = .PERNAPASAN_01
                chkPERNAPASAN_02.Checked = .PERNAPASAN_02
                chkPERNAPASAN_03.Checked = .PERNAPASAN_03
                chkPERNAPASAN_04.Checked = .PERNAPASAN_04
                txtPERNAPASAN_04_TEXT.Text = .PERNAPASAN_04_TEXT
                chkPENCERNAAN_01.Checked = .PENCERNAAN_01
                chkPENCERNAAN_02.Checked = .PENCERNAAN_02
                chkPENCERNAAN_02_1.Checked = .PENCERNAAN_02_1
                chkPENCERNAAN_02_2.Checked = .PENCERNAAN_02_2
                chkPENCERNAAN_03.Checked = .PENCERNAAN_03
                chkPENCERNAAN_04.Checked = .PENCERNAAN_04
                chkPENCERNAAN_05.Checked = .PENCERNAAN_05
                txtPENCERNAAN_05_TEXT.Text = .PENCERNAAN_05_TEXT
                chkENDROKIN_01.Checked = .ENDROKIN_01
                chkENDROKIN_02.Checked = .ENDROKIN_02
                chkENDROKIN_03.Checked = .ENDROKIN_03
                chkENDROKIN_04.Checked = .ENDROKIN_04
                chkENDROKIN_05.Checked = .ENDROKIN_05
                txtENDROKIN_05_TEXT.Text = .ENDROKIN_05_TEXT
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
                chkREPRODUKSI_01_1.Checked = .REPRODUKSI_01_1
                chkREPRODUKSI_01_2.Checked = .REPRODUKSI_01_2
                chkREPRODUKSI_02.Checked = .REPRODUKSI_02
                chkREPRODUKSI_02_1.Checked = .REPRODUKSI_02_1
                txtREPRODUKSI_02_1_TEXT.Text = .REPRODUKSI_02_1_TEXT
                chkREPRODUKSI_02_2.Checked = .REPRODUKSI_02_2
                chkREPRODUKSI_03.Checked = .REPRODUKSI_03
                chkREPRODUKSI_03_1.Checked = .REPRODUKSI_03_1
                txtREPRODUKSI_03_1_TEXT.Text = .REPRODUKSI_03_1_TEXT
                chkREPRODUKSI_03_2.Checked = .REPRODUKSI_03_2
                chkREPRODUKSI_04.Checked = .REPRODUKSI_04
                txtREPRODUKSI_04_TEXT.Text = .REPRODUKSI_04_TEXT
                chkREPRODUKSI_05.Checked = .REPRODUKSI_05
                txtREPRODUKSI_05_TEXT.Text = .REPRODUKSI_05_TEXT
                txtREPRODUKSI_06_TEXT.Text = .REPRODUKSI_06_TEXT
                txtREPRODUKSI_07_TEXT.Text = .REPRODUKSI_07_TEXT
                txtREPRODUKSI_08_TEXT.Text = .REPRODUKSI_08_1_TEXT
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
                txtURINARIA_04_TEXT.Text = .URINARIA_04_TEXT
                chkKEADAAN_EMOSIONAL_01.Checked = .KEADAAN_EMOSIONAL_01
                chkKEADAAN_EMOSIONAL_02.Checked = .KEADAAN_EMOSIONAL_02
                chkKEADAAN_EMOSIONAL_03.Checked = .KEADAAN_EMOSIONAL_03
                chkKEADAAN_EMOSIONAL_04.Checked = .KEADAAN_EMOSIONAL_04
                chkKEADAAN_EMOSIONAL_05.Checked = .KEADAAN_EMOSIONAL_05
                txtKEADAAN_EMOSIONAL_05_TEXT.Text = .KEADAAN_EMOSIONAL_05_TEXT
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
                chkMULUT_01.Checked = .GIGI_01
                chkMULUT_02.Checked = .GIGI_02
                txtMULUT_02_TEXT.Text = .GIGI_02_TEXT
                chkMULUT_03.Checked = .GIGI_03
                txtMULUT_03_TEXT.Text = .GIGI_03_TEXT
                chkHIDUNG_MUKA_01.Checked = .HIDUNG_MUKA_01
                chkHIDUNG_MUKA_02.Checked = .HIDUNG_MUKA_02
                txtHIDUNG_MUKA_02_TEXT.Text = .HIDUNG_MUKA_02_TEXT
                chkHIDUNG_MUKA_03.Checked = .HIDUNG_MUKA_03
                txtHIDUNG_MUKA_03_TEXT.Text = .HIDUNG_MUKA_03_TEXT
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
                chkSKALANEYRI_AKUT_01.Checked = .SKALANEYRI_AKUT_01
                txtSKALANEYRI_AKUT_LOKASI_TEXT.Text = .SKALANEYRI_AKUT_LOKASI_TEXT
                txtSKALANEYRI_AKUT_ISTIRAHAT_TEXT.Text = .SKALANEYRI_AKUT_ISTIRAHAT_TEXT
                txtSKALANEYRI_AKUT_AKTIVITAS_TEXT.Text = .SKALANEYRI_AKUT_AKTIVITAS_TEXT
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04
                txtKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04_TEXT.Text = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04_TEXT
                chkKEBUTUHAN_EDUKASI_01.Checked = .KEBUTUHAN_EDUKASI_01
                chkKEBUTUHAN_EDUKASI_02.Checked = .KEBUTUHAN_EDUKASI_02
                chkKEBUTUHAN_EDUKASI_03.Checked = .KEBUTUHAN_EDUKASI_03
                chkKEBUTUHAN_EDUKASI_04.Checked = .KEBUTUHAN_EDUKASI_04
                txtKEBUTUHAN_EDUKASI_04_TEXT.Text = .KEBUTUHAN_EDUKASI_04_TEXT
                txtKEADAAN.Text = .ALERGI_02_02_TEXT
                txtPEMERIKSAAN_PENUNJANG_01.Text = .PEMERIKSAAN_PENUNJANG_01
                chkPEMERIKSAAN_PENUNJANG_02.Checked = .PEMERIKSAAN_PENUNJANG_02
                chkPEMERIKSAAN_PENUNJANG_03.Checked = .PEMERIKSAAN_PENUNJANG_03
                chkPEMERIKSAAN_PENUNJANG_04.Checked = .PEMERIKSAAN_PENUNJANG_04
                txtPEMERIKSAAN_PENUNJANG_04_TEXT.Text = .PEMERIKSAAN_PENUNJANG_04_TEXT
                txtRIWAYAT_PENYAKIT.Text = .RIWAYAT_PENYAKIT
                chkRIWAYAT_IMUNISASI_01.Checked = .RIWAYAT_IMUNISASI_01
                txtRIWAYAT_IMUNISASI_01_TEXT.Text = .RIWAYAT_IMUNISASI_01_TEXT
                chkRIWAYAT_IMUNISASI_02.Checked = .RIWAYAT_IMUNISASI_02
                txtRIWAYAT_IMUNISASI_02_TEXT.Text = .RIWAYAT_IMUNISASI_02_TEXT
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
                txtPERENCANAAN_PASIEN_PULANG_01.Text = .PERENCANAAN_PASIEN_PULANG_01
                chkPERENCANAAN_PASIEN_PULANG_02_1.Checked = .PERENCANAAN_PASIEN_PULANG_02_1
                chkPERENCANAAN_PASIEN_PULANG_02_2.Checked = .PERENCANAAN_PASIEN_PULANG_02_2
                chkPERENCANAAN_PASIEN_PULANG_02_3.Checked = .PERENCANAAN_PASIEN_PULANG_02_3
                chkPERENCANAAN_PASIEN_PULANG_02_4.Checked = .PERENCANAAN_PASIEN_PULANG_02_4
                txtPERENCANAAN_PASIEN_PULANG_02_4_TEXT.Text = .PERENCANAAN_PASIEN_PULANG_02_4_TEXT
                chkPERENCANAAN_PASIEN_PULANG_03_1.Checked = .PERENCANAAN_PASIEN_PULANG_03_1
                chkPERENCANAAN_PASIEN_PULANG_03_1_1.Checked = .PERENCANAAN_PASIEN_PULANG_03_1_1
                chkPERENCANAAN_PASIEN_PULANG_03_1_2.Checked = .PERENCANAAN_PASIEN_PULANG_03_1_2
                chkPERENCANAAN_PASIEN_PULANG_03_2.Checked = .PERENCANAAN_PASIEN_PULANG_03_2
                chkPERENCANAAN_PASIEN_PULANG_03_3.Checked = .PERENCANAAN_PASIEN_PULANG_03_3
                chkPERENCANAAN_PASIEN_PULANG_03_4.Checked = .PERENCANAAN_PASIEN_PULANG_03_4
                chkPERENCANAAN_PASIEN_PULANG_03_5.Checked = .PERENCANAAN_PASIEN_PULANG_03_5
                chkPERENCANAAN_PASIEN_PULANG_03_5_1.Checked = .PERENCANAAN_PASIEN_PULANG_03_5_1
                chkPERENCANAAN_PASIEN_PULANG_03_5_2.Checked = .PERENCANAAN_PASIEN_PULANG_03_5_2
                chkPERENCANAAN_PASIEN_PULANG_03_6.Checked = .PERENCANAAN_PASIEN_PULANG_03_6
                chkPERENCANAAN_PASIEN_PULANG_03_7.Checked = .PERENCANAAN_PASIEN_PULANG_03_7
                'grdNOIDUSER.Text = .DIASESMEN_OLEH
                'txtPASIEN_KELUARGA.Text = .PASIEN_KELUARGA

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
            '    MsgBox("Dibutuhkan Nama Assesmen", MsgBoxStyle.Exclamation, Me.Text)
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
            Dim ds = oS_DIGITAL_RJ_08.GetStructureHeader
            With ds
                .KDKUNJUNGAN = txtNoRegister.Text
                Try
                    .DATECREATED = oS_DIGITAL_RJ_08.GetData(txtNoRegister.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .ALERGI_01 = chkALERGI_01.Checked
                .ALERGI_02 = chkALERGI_02.Checked
                .ALERGI_02_TEXT = txtALERGI_02_TEXT.Text
                .AUTO = chkAUTO.Checked
                .ALLO = chkALLO.Checked
                .ALLO_TEXT = txtALLO_TEXT.Text
                .KELUHAN_UTAMA = txtKELUHAN_UTAMA.Text
                .KESADARAN_UMUM = txtKESADARAN_UMUM.Text
                .TANDA_VITAL_01 = txtTANDA_VITAL_01.Text
                .TANDA_VITAL_02 = txtTANDA_VITAL_02.Text
                .TANDA_VITAL_03 = txtTANDA_VITAL_03.Text
                .TANDA_VITAL_04 = txtTANDA_VITAL_04.Text
                .TANDA_VITAL_05 = txtTANDA_VITAL_05.Text
                .TANDA_VITAL_06 = txtTANDA_VITAL_06.Text
                .TANDA_VITAL_07 = txtTANDA_VITAL_07.Text
                .TANDA_VITAL_08 = txtTANDA_VITAL_08.Text
                .TANDA_VITAL_09 = txtTANDA_VITAL_09.Text
                .TANDA_VITAL_REGULER = chkTANDA_VITAL_REGULER.Checked
                .TANDA_VITAL_IREGULER = chkTANDA_VITAL_IREGULER.Checked
                .STATUS_PSIKOLOGIS_01 = chkSTATUS_PSIKOLOGIS_01.Checked
                .STATUS_PSIKOLOGIS_02 = chkSTATUS_PSIKOLOGIS_02.Checked
                .STATUS_PSIKOLOGIS_03 = chkSTATUS_PSIKOLOGIS_03.Checked
                .STATUS_PSIKOLOGIS_04 = chkSTATUS_PSIKOLOGIS_04.Checked
                .STATUS_PSIKOLOGIS_05 = chkSTATUS_PSIKOLOGIS_05.Checked
                .STATUS_PSIKOLOGIS_06 = chkSTATUS_PSIKOLOGIS_06.Checked
                .STATUS_PSIKOLOGIS_06_TEXT = txtSTATUS_PSIKOLOGIS_06_TEXT.Text
                .STATUS_MENTAL_01 = chkSTATUS_MENTAL_01.Checked
                .STATUS_MENTAL_02 = chkSTATUS_MENTAL_02.Checked
                .STATUS_MENTAL_02_TEXT = txtSTATUS_MENTAL_02_TEXT.Text
                .STATUS_MENTAL_03 = chkSTATUS_MENTAL_03.Checked
                .STATUS_MENTAL_03_TEXT = txtSTATUS_MENTAL_03_TEXT.Text
                .SOSIAL_01_1 = chkSOSIAL_01_1.Checked
                .SOSIAL_01_2 = chkSOSIAL_01_2.Checked
                .SOSIAL_02_1 = chkSOSIAL_02_1.Checked
                .SOSIAL_02_2 = chkSOSIAL_02_2.Checked
                .SOSIAL_02_3 = chkSOSIAL_02_3.Checked
                .SOSIAL_02_4 = chkSOSIAL_02_4.Checked
                .SOSIAL_02_4_TEXT = txtSOSIAL_02_4_TEXT.Text
                .SOSIAL_03_1_TEXT = txtSOSIAL_03_1_TEXT.Text
                .SOSIAL_03_2_TEXT = txtSOSIAL_03_2_TEXT.Text
                .SOSIAL_03_3_TEXT = txtSOSIAL_03_3_TEXT.Text
                .STATUS_SPIRITUAL_01_TEXT = txtSTATUS_SPIRITUAL_01_TEXT.Text
                .STATUS_SPIRITUAL_02_TEXT = txtSTATUS_SPIRITUAL_02_TEXT.Text
                .PERSYARAFAN_01 = chkPERSYARAFAN_01.Checked
                .PERSYARAFAN_02 = chkPERSYARAFAN_02.Checked
                .PERSYARAFAN_03 = chkPERSYARAFAN_03.Checked
                .PERSYARAFAN_04 = chkPERSYARAFAN_04.Checked
                .PERSYARAFAN_04_1 = chkPERSYARAFAN_04_1.Checked
                .PERSYARAFAN_04_2 = PERSYARAFAN_04_2.Checked
                .PERSYARAFAN_05 = chkPERSYARAFAN_05.Checked
                .PERSYARAFAN_05_TEXT = txtPERSYARAFAN_05_TEXT.Text
                .PERNAPASAN_01 = chkPERNAPASAN_01.Checked
                .PERNAPASAN_02 = chkPERNAPASAN_02.Checked
                .PERNAPASAN_03 = chkPERNAPASAN_03.Checked
                .PERNAPASAN_04 = chkPERNAPASAN_04.Checked
                .PERNAPASAN_04_TEXT = txtPERNAPASAN_04_TEXT.Text
                .PENCERNAAN_01 = chkPENCERNAAN_01.Checked
                .PENCERNAAN_02 = chkPENCERNAAN_02.Checked
                .PENCERNAAN_02_1 = chkPENCERNAAN_02_1.Checked
                .PENCERNAAN_02_2 = chkPENCERNAAN_02_2.Checked
                .PENCERNAAN_03 = chkPENCERNAAN_03.Checked
                .PENCERNAAN_04 = chkPENCERNAAN_04.Checked
                .PENCERNAAN_05 = chkPENCERNAAN_05.Checked
                .PENCERNAAN_05_TEXT = txtPENCERNAAN_05_TEXT.Text
                .ENDROKIN_01 = chkENDROKIN_01.Checked
                .ENDROKIN_02 = chkENDROKIN_02.Checked
                .ENDROKIN_03 = chkENDROKIN_03.Checked
                .ENDROKIN_04 = chkENDROKIN_04.Checked
                .ENDROKIN_05 = chkENDROKIN_05.Checked
                .ENDROKIN_05_TEXT = txtENDROKIN_05_TEXT.Text
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
                .REPRODUKSI_01_1 = chkREPRODUKSI_01_1.Checked
                .REPRODUKSI_01_2 = chkREPRODUKSI_01_2.Checked
                .REPRODUKSI_02 = chkREPRODUKSI_02.Checked
                .REPRODUKSI_02_1 = chkREPRODUKSI_02_1.Checked
                .REPRODUKSI_02_1_TEXT = txtREPRODUKSI_02_1_TEXT.Text
                .REPRODUKSI_02_2 = chkREPRODUKSI_02_2.Checked
                .REPRODUKSI_03 = chkREPRODUKSI_03.Checked
                .REPRODUKSI_03_1 = chkREPRODUKSI_03_1.Checked
                .REPRODUKSI_03_1_TEXT = txtREPRODUKSI_03_1_TEXT.Text
                .REPRODUKSI_03_2 = chkREPRODUKSI_03_2.Checked
                .REPRODUKSI_04 = chkREPRODUKSI_04.Checked
                .REPRODUKSI_04_TEXT = txtREPRODUKSI_04_TEXT.Text
                .REPRODUKSI_05 = chkREPRODUKSI_05.Checked
                .REPRODUKSI_05_TEXT = txtREPRODUKSI_05_TEXT.Text
                .REPRODUKSI_06_TEXT = txtREPRODUKSI_06_TEXT.Text
                .REPRODUKSI_07_TEXT = txtREPRODUKSI_07_TEXT.Text
                .REPRODUKSI_08_1_TEXT = txtREPRODUKSI_08_TEXT.Text
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
                .URINARIA_04_TEXT = txtURINARIA_04_TEXT.Text
                .KEADAAN_EMOSIONAL_01 = chkKEADAAN_EMOSIONAL_01.Checked
                .KEADAAN_EMOSIONAL_02 = chkKEADAAN_EMOSIONAL_02.Checked
                .KEADAAN_EMOSIONAL_03 = chkKEADAAN_EMOSIONAL_03.Checked
                .KEADAAN_EMOSIONAL_04 = chkKEADAAN_EMOSIONAL_04.Checked
                .KEADAAN_EMOSIONAL_05 = chkKEADAAN_EMOSIONAL_05.Checked
                .KEADAAN_EMOSIONAL_05_TEXT = txtKEADAAN_EMOSIONAL_05_TEXT.Text
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
                .GIGI_01 = chkMULUT_01.Checked
                .GIGI_02 = chkMULUT_02.Checked
                .GIGI_02_TEXT = txtMULUT_02_TEXT.Text
                .GIGI_03 = chkMULUT_03.Checked
                .GIGI_03_TEXT = txtMULUT_03_TEXT.Text
                .HIDUNG_MUKA_01 = chkHIDUNG_MUKA_01.Checked
                .HIDUNG_MUKA_02 = chkHIDUNG_MUKA_02.Checked
                .HIDUNG_MUKA_02_TEXT = txtHIDUNG_MUKA_02_TEXT.Text
                .HIDUNG_MUKA_03 = chkHIDUNG_MUKA_03.Checked
                .HIDUNG_MUKA_03_TEXT = txtHIDUNG_MUKA_03_TEXT.Text
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
                .SKALANEYRI_AKUT_01 = chkSKALANEYRI_AKUT_01.Checked
                .SKALANEYRI_AKUT_LOKASI_TEXT = txtSKALANEYRI_AKUT_LOKASI_TEXT.Text
                .SKALANEYRI_AKUT_ISTIRAHAT_TEXT = txtSKALANEYRI_AKUT_ISTIRAHAT_TEXT.Text
                .SKALANEYRI_AKUT_AKTIVITAS_TEXT = txtSKALANEYRI_AKUT_AKTIVITAS_TEXT.Text
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01 = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01.Checked
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02 = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02.Checked
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03 = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03.Checked
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04 = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04.Checked
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04_TEXT = txtKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04_TEXT.Text
                .KEBUTUHAN_EDUKASI_01 = chkKEBUTUHAN_EDUKASI_01.Checked
                .KEBUTUHAN_EDUKASI_02 = chkKEBUTUHAN_EDUKASI_02.Checked
                .KEBUTUHAN_EDUKASI_03 = chkKEBUTUHAN_EDUKASI_03.Checked
                .KEBUTUHAN_EDUKASI_04 = chkKEBUTUHAN_EDUKASI_04.Checked
                .KEBUTUHAN_EDUKASI_04_TEXT = txtKEBUTUHAN_EDUKASI_04_TEXT.Text
                .ALERGI_02_01 = False
                .ALERGI_02_02 = False
                .ALERGI_02_02_TEXT = txtKEADAAN.Text
                .PEMERIKSAAN_PENUNJANG_01 = txtPEMERIKSAAN_PENUNJANG_01.Text
                .PEMERIKSAAN_PENUNJANG_02 = chkPEMERIKSAAN_PENUNJANG_02.Checked
                .PEMERIKSAAN_PENUNJANG_03 = chkPEMERIKSAAN_PENUNJANG_03.Checked
                .PEMERIKSAAN_PENUNJANG_04 = chkPEMERIKSAAN_PENUNJANG_04.Checked
                .PEMERIKSAAN_PENUNJANG_04_TEXT = txtPEMERIKSAAN_PENUNJANG_04_TEXT.Text
                .RIWAYAT_PENYAKIT = txtRIWAYAT_PENYAKIT.Text
                .RIWAYAT_IMUNISASI_01 = chkRIWAYAT_IMUNISASI_01.Checked
                .RIWAYAT_IMUNISASI_01_TEXT = txtRIWAYAT_IMUNISASI_01_TEXT.Text
                .RIWAYAT_IMUNISASI_02 = chkRIWAYAT_IMUNISASI_02.Checked
                .RIWAYAT_IMUNISASI_02_TEXT = txtRIWAYAT_IMUNISASI_02_TEXT.Text
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
                .PERENCANAAN_PASIEN_PULANG_01 = txtPERENCANAAN_PASIEN_PULANG_01.Text
                .PERENCANAAN_PASIEN_PULANG_02_1 = chkPERENCANAAN_PASIEN_PULANG_02_1.Checked
                .PERENCANAAN_PASIEN_PULANG_02_2 = chkPERENCANAAN_PASIEN_PULANG_02_2.Checked
                .PERENCANAAN_PASIEN_PULANG_02_3 = chkPERENCANAAN_PASIEN_PULANG_02_3.Checked
                .PERENCANAAN_PASIEN_PULANG_02_4 = chkPERENCANAAN_PASIEN_PULANG_02_4.Checked
                .PERENCANAAN_PASIEN_PULANG_02_4_TEXT = txtPERENCANAAN_PASIEN_PULANG_02_4_TEXT.Text
                .PERENCANAAN_PASIEN_PULANG_03_1 = chkPERENCANAAN_PASIEN_PULANG_03_1.Checked
                .PERENCANAAN_PASIEN_PULANG_03_1_1 = chkPERENCANAAN_PASIEN_PULANG_03_1_1.Checked
                .PERENCANAAN_PASIEN_PULANG_03_1_2 = chkPERENCANAAN_PASIEN_PULANG_03_1_2.Checked
                .PERENCANAAN_PASIEN_PULANG_03_2 = chkPERENCANAAN_PASIEN_PULANG_03_2.Checked
                .PERENCANAAN_PASIEN_PULANG_03_3 = chkPERENCANAAN_PASIEN_PULANG_03_3.Checked
                .PERENCANAAN_PASIEN_PULANG_03_4 = chkPERENCANAAN_PASIEN_PULANG_03_4.Checked
                .PERENCANAAN_PASIEN_PULANG_03_5 = chkPERENCANAAN_PASIEN_PULANG_03_5.Checked
                .PERENCANAAN_PASIEN_PULANG_03_5_1 = chkPERENCANAAN_PASIEN_PULANG_03_5_1.Checked
                .PERENCANAAN_PASIEN_PULANG_03_5_2 = chkPERENCANAAN_PASIEN_PULANG_03_5_2.Checked
                .PERENCANAAN_PASIEN_PULANG_03_6 = chkPERENCANAAN_PASIEN_PULANG_03_6.Checked
                .PERENCANAAN_PASIEN_PULANG_03_7 = chkPERENCANAAN_PASIEN_PULANG_03_7.Checked
                .DIASESMEN_OLEH = ""
                .PASIEN_KELUARGA = txtNoPasien.Text

                Try
                    .CETAK = oS_DIGITAL_RJ_08.GetData(txtNoRegister.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                Try
                    Dim oSetUser As New Setting.clsUser
                    Dim dsUser = oSetUser.GetData(sUserID)
                    If dsUser.ISOTORTY = True Then
                        .KDUSER = oS_DIGITAL_RJ_08.GetData(txtNoRegister.Text).KDUSER
                        .KDUSER_SIGNATURE = oS_DIGITAL_RJ_08.GetData(txtNoRegister.Text).KDUSER_SIGNATURE
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
                    fn_Save = oS_DIGITAL_RJ_08.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_RJ_08.UpdateData(ds)
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
    Private Sub txtTANDA_VITAL_01_EditValueChanged(sender As Object, e As EventArgs) Handles txtTANDA_VITAL_01.EditValueChanged
        If isLoad = True Then
            txtINTERVENSI_IMPLEMENTASI_01_TD.Text = txtTANDA_VITAL_01.Text
        End If
    End Sub
    Private Sub txtTANDA_VITAL_03_EditValueChanged(sender As Object, e As EventArgs) Handles txtTANDA_VITAL_03.EditValueChanged
        If isLoad = True Then
            txtINTERVENSI_IMPLEMENTASI_01_S.Text = txtTANDA_VITAL_03.Text
        End If
    End Sub
    Private Sub txtTANDA_VITAL_02_EditValueChanged(sender As Object, e As EventArgs) Handles txtTANDA_VITAL_02.EditValueChanged
        If isLoad = True Then
            txtINTERVENSI_IMPLEMENTASI_01_N.Text = txtTANDA_VITAL_02.Text
        End If
    End Sub
    Private Sub txtTANDA_VITAL_04_EditValueChanged(sender As Object, e As EventArgs) Handles txtTANDA_VITAL_04.EditValueChanged
        If isLoad = True Then
            txtINTERVENSI_IMPLEMENTASI_01_R.Text = txtTANDA_VITAL_04.Text
        End If
    End Sub
#End Region
End Class