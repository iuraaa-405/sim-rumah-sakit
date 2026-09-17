Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmEMedrek_AsmedAnak
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIDIGITAL_ASMEDANAK As New EMedrek.clsDigital_RJ_ASMEDANAK
    Private down As Boolean = False
    Private sNoid As String
    Private sKodeIdentitas As Integer
    Private sKDDOCTOR As String

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDDOCTOR As String, ByVal KodeIdentitas As Integer, ByVal NoId As String)
        oFormMode = FormMode

        sKodeIdentitas = KodeIdentitas
        sNoid = NoId
        sKDDOCTOR = KDDOCTOR
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "ASESMEN AWAL MEDIS PASIEN ANAK"
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_ChangeFormState()
        fn_DOCTOR()

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
        txt1.Properties.ReadOnly = Status
        txt2.Properties.ReadOnly = Status
        txt3.Properties.ReadOnly = Status
        'txt4.Properties.ReadOnly = Status
        'txt5.Properties.ReadOnly = Status
        'txt6.Properties.ReadOnly = Status
        'txt7.Properties.ReadOnly = Status
        'txt8.Properties.ReadOnly = Status
        'txt9.Properties.ReadOnly = Status
        'txtNYERI_AKUT_L.Properties.ReadOnly = Status
        'txtINTENSITAS_A.Properties.ReadOnly = Status
        'txtINTENSITAS_I_K.Properties.ReadOnly = Status
        'txtR_I_BCG_DASAR.Properties.ReadOnly = Status
        'txtNYERI_KRONIS_L.Properties.ReadOnly = Status
        'txtR_I_DASAR_NAMA6.Properties.ReadOnly = Status
        'txtINTENSITAS_A_A.Properties.ReadOnly = Status
        'txtINTENSITAS_I_A.Properties.ReadOnly = Status
        'txtR_I_POLIO_DASAR.Properties.ReadOnly = Status
        'txtR_I_DPT_DASAR.Properties.ReadOnly = Status
        'txtR_I_HEPATITIS_DASAR.Properties.ReadOnly = Status
        'txtR_I_CAMPAK_DASAR.Properties.ReadOnly = Status
        'txtR_I_DASAR_NAMA1.Properties.ReadOnly = Status
        'MemoEdit4.Properties.ReadOnly = Status
        'MemoEdit2.Properties.ReadOnly = Status
        'MemoEdit3.Properties.ReadOnly = Status
        'MemoEdit5.Properties.ReadOnly = Status
        'MemoEdit6.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        '[KDKUNJUNGAN] [nvarchar](50) Not NULL,
        '[KDASMEDANAK] [nvarchar](50) Not NULL,
        '[DATECREATED] [smalldatetime] Not NULL,
        '[DATEUPDATED] [smalldatetime] Not NULL,
        '[DateTime] [smalldatetime] Not NULL,

        grdDOCTOR.Text = sKDDOCTOR
        txt1.ResetText()
        txt2.ResetText()
        txt3.ResetText()

        chkR_A_YA.Checked = False
        chkR_A_TDK.Checked = False
        txtR_A_OBAT.ResetText()
        txtR_A_MAKANAN.ResetText()
        txtR_A_LAIN.ResetText()
        txtR_A_O_REAKSI.ResetText()
        txtR_A_M_REAKSI.ResetText()
        txtR_A_L_REAKSI.ResetText()
        chkRESIKOCIDERA_JATUH_T.Checked = False
        chkRESIKOCIDERA_JATUH_Y.Checked = False
        chkRESIKOCIDERA_JATUH_Y_J.Checked = False
        chkSKALA_NYERI_0.Checked = False
        chkSKALA_NYERI_1.Checked = False
        chkSKALA_NYERI_2.Checked = False
        chkSKALA_NYERI_3.Checked = False
        chkSKALA_NYERI_4.Checked = False
        chkSKALA_NYERI_5.Checked = False
        chkSKALA_NYERI_6.Checked = False
        chkSKALA_NYERI_7.Checked = False
        chkSKALA_NYERI_8.Checked = False
        chkSKALA_NYERI_9.Checked = False
        chkSKALA_NYERI_10.Checked = False
        chkNYERI_KRONIS.Checked = False

        txtNYERI_KRONIS_L.ResetText()
        chkNYERI_AKUT.Checked = False
        txtNYERI_AKUT_L.ResetText()
        txtINTENSITAS_I_K.ResetText()
        txtINTENSITAS_A.ResetText()
        txtINTENSITAS_I_A.ResetText()
        txtINTENSITAS_A_A.ResetText()
        chkNYERI_H_MO.Checked = False
        chkNYERI_H_I.Checked = False
        chkNYERI_H_MM.Checked = False
        chkNYERI_H_BPT.Checked = False

        txtNYERI_H_LAIN.ResetText()
        txtRIWAYAT_IMUNISASI.ResetText()
        txtR_I_NAMA1.ResetText()
        txtR_I_NAMA2.ResetText()
        txtR_I_NAMA3.ResetText()
        txtR_I_NAMA4.ResetText()
        txtR_I_NAMA5.ResetText()
        txtR_I_NAMA6.ResetText()
        txtR_I_BCG_DASAR.ResetText()
        txtR_I_POLIO_DASAR.ResetText()
        txtR_I_DPT_DASAR.ResetText()
        txtR_I_CAMPAK_DASAR.ResetText()
        txtR_I_HEPATITIS_DASAR.ResetText()
        txtR_I_DASAR_NAMA1.ResetText()
        txtR_I_DASAR_NAMA2.ResetText()
        txtR_I_DASAR_NAMA3.ResetText()
        txtR_I_DASAR_NAMA4.ResetText()
        txtR_I_DASAR_NAMA5.ResetText()
        txtR_I_DASAR_NAMA6.ResetText()
        txtR_I_BCG_ULANG.ResetText()
        txtR_I_POLIO_ULANG.ResetText()
        txtR_I_DPT_ULANG.ResetText()
        txtR_I_CAMPAK_ULANG.ResetText()
        txtR_I_HEPATITIS_ULANG.ResetText()
        txtR_I_ULANG_NAMA1.ResetText()
        txtR_I_ULANG_NAMA2.ResetText()
        txtR_I_ULANG_NAMA3.ResetText()
        txtR_I_ULANG_NAMA4.ResetText()
        txtR_I_ULANG_NAMA5.ResetText()
        txtR_I_ULANG_NAMA6.ResetText()

        txtK_K_AYAH.ResetText()
        txtK_K_IBU.ResetText()
        txtK_K_SAUDARA.ResetText()
        txtK_K_SERUMAH.ResetText()
        txtPERKEMBANGAN.ResetText()
        txtP_BERBALIK.ResetText()
        txtP_DUDUK_T_B.ResetText()
        txtP_DUDUK_T_P.ResetText()
        txtP_BERJALAN1.ResetText()
        txtP_BERJALAN2.ResetText()
        txtP_LAIN.ResetText()
        txtP_BICARA_KATA.ResetText()
        txtP_BICARA_KALIMAT.ResetText()
        txtP_MEMBACA.ResetText()
        txtP_MENULIS.ResetText()
        txtP_SEKOLAH.ResetText()
        txtGIGI_GELIGI.ResetText()
        txtGG_PERTAMA.ResetText()
        txtGG_SEKARANG.ResetText()

        chkGS_KI_A_V.Checked = False
        chkGS_KI_A_IV.Checked = False
        chkGS_KI_A_III.Checked = False
        chkGS_KI_A_II.Checked = False
        chkGS_KI_A_I.Checked = False
        chkGS_KA_A_V.Checked = False
        chkGS_KA_A_IV.Checked = False
        chkGS_KA_A_III.Checked = False
        chkGS_KA_A_II.Checked = False
        chkGS_KA_A_I.Checked = False
        chkGS_KI_B_V.Checked = False
        chkGS_KI_B_IV.Checked = False
        chkGS_KI_B_III.Checked = False
        chkGS_KI_B_II.Checked = False
        chkGS_KI_B_I.Checked = False
        chkGS_KA_B_V.Checked = False
        chkGS_KA_B_IV.Checked = False
        chkGS_KA_B_III.Checked = False
        chkGS_KA_B_II.Checked = False
        chkGS_KA_B_I.Checked = False
        chkGT_KI_A_8.Checked = False
        chkGT_KI_A_7.Checked = False
        chkGT_KI_A_6.Checked = False
        chkGT_KI_A_5.Checked = False
        chkGT_KI_A_4.Checked = False
        chkGT_KI_A_3.Checked = False
        chkGT_KI_A_2.Checked = False
        chkGT_KI_A_1.Checked = False
        chkGT_KI_B_8.Checked = False
        chkGT_KI_B_7.Checked = False
        chkGT_KI_B_6.Checked = False
        chkGT_KI_B_5.Checked = False
        chkGT_KI_B_4.Checked = False
        chkGT_KI_B_3.Checked = False
        chkGT_KI_B_2.Checked = False
        chkGT_KI_B_1.Checked = False
        chkGT_KA_A_1.Checked = False
        chkGT_KA_A_2.Checked = False
        chkGT_KA_A_3.Checked = False
        chkGT_KA_A_4.Checked = False
        chkGT_KA_A_5.Checked = False
        chkGT_KA_A_6.Checked = False
        chkGT_KA_A_7.Checked = False
        chkGT_KA_A_8.Checked = False
        chkGT_KA_B_1.Checked = False
        chkGT_KA_B_2.Checked = False
        chkGT_KA_B_3.Checked = False
        chkGT_KA_B_4.Checked = False
        chkGT_KA_B_5.Checked = False
        chkGT_KA_B_6.Checked = False
        chkGT_KA_B_7.Checked = False
        chkGT_KA_B_8.Checked = False

        txtM_JENIS_MAKAN1.ResetText()
        txtM_JENIS_MAKAN2.ResetText()
        txtM_JENIS_MAKAN3.ResetText()
        txtM_JENIS_MAKAN4.ResetText()
        txtM_JENIS_MAKAN5.ResetText()
        txtM_KUANTITAS1.ResetText()
        txtM_KUANTITAS2.ResetText()
        txtM_KUANTITAS3.ResetText()
        txtM_KUANTITAS4.ResetText()
        txtM_KUANTITAS5.ResetText()
        txtM_KUALITAS1.ResetText()
        txtM_KUALITAS2.ResetText()
        txtM_KUALITAS3.ResetText()
        txtM_KUALITAS4.ResetText()
        txtM_KUALITAS5.ResetText()

        chkPENYAKIT_CAMPAK.Checked = False
        chkPENYAKIT_BATUK.Checked = False
        chkPENYAKIT_TBC.Checked = False
        chkPENYAKIT_DIFTERI.Checked = False
        chkPENYAKIT_TETANUS.Checked = False
        chkPENYAKIT_DIARE.Checked = False
        chkPENYAKIT_DEMAM.Checked = False
        chkPENYAKIT_KUNING.Checked = False
        chkPENYAKIT_CACING.Checked = False
        chkPENYAKIT_KEJANG.Checked = False
        chkPENYAKIT_BENGEK.Checked = False
        chkPENYAKIT_EKSIM.Checked = False
        chkPENYAKIT_KALIGATA.Checked = False
        chkPENYAKIT_TENGGOROKAN.Checked = False

        txtPENYAKIT_LAIN.ResetText()
        txtPENGUKURAN.ResetText()
        txtP_UMUR.ResetText()
        txtP_BERATBADAN.ResetText()
        txtP_TINGGI_BADAN.ResetText()
        txtP_LINGKARKEPALA.ResetText()
        txtP_LINGKARDADA.ResetText()
        txtP_LENGAN_ATAS.ResetText()
        txtP_STATUSGIZI_BBU.ResetText()
        txtP_STATUSGIZI_TBU.ResetText()
        txtP_STATUSGIZI_BBTB.ResetText()
        txtP_STATUSGIZI_BMIU.ResetText()
        txtTV.ResetText()
        txtTV_LAJUNAFAS.ResetText()
        txtTV_LAJUNAFAS_TIPE.ResetText()
        txtTV_TEKANAN_DARAH.ResetText()

        chkTV_TEKANAN_DARAH_SISTOLIK.Checked = False
        chkTV_TEKANAN_DARAH_DIASTOLIK.Checked = False

        txtTV_SUHU.ResetText()
        txtTV_LAJUNADI.ResetText()
        txtTV_LAJUNADI_KUALITAS.ResetText()

        chkTV_LAJUNADI_REGULAR.Checked = False
        chkTV_LAJUNADI_IREGULER.Checked = False

        txtTV_LAJUNADI_REGULER_IREGULER_TXT.ResetText()
        txtTV_LAJUNADI_ISI.ResetText()
        txtK_U.ResetText()

        chkKU_KEADAAN_SAKIT_TIDAK.Checked = False
        chkKU_KEADAAN_SAKIT_RINGAN.Checked = False
        chkKU_KEADAAN_SAKIT_SEDANG.Checked = False
        chkKU_KEADAAN_SAKIT_BERAT.Checked = False

        txtKU_KESADARAN_KUANTITATIF.ResetText()
        txtKU_KESADARAN_KUANTITATIF_E.ResetText()
        txtKU_KESADARAN_KUANTITATIF_V.ResetText()
        txtKU_KESADARAN_KUANTITATIF_M.ResetText()
        txtKU_KESADARAN_KUALITATIF.ResetText()


        chkKU_SESAK_P.Checked = False
        chkKU_SESAK_M.Checked = False

        txtKU_SESAK_PCH.ResetText()
        txtKU_SESAK_RETRAKSI.ResetText()
        txtKU_SESAK_RETRAKSI2.ResetText()

        chkKU_SIANOSIS_S.Checked = False
        chkKU_SIANOSIS_P.Checked = False

        txtKU_SIANOSIS_S_P_TXT.ResetText()
        txtKU_IKTERUS_KRAMER.ResetText()
        txtKU_IKTERUS_ANAK.ResetText()

        txtKU_EDEMA.ResetText()
        txtKU_EDEMA_ANASARKA.ResetText()
        txtKU_DEHIDRASI.ResetText()
        chkKU_DEHIDRASI_R.Checked = False
        chkKU_DEHIDRASI_S.Checked = False
        txtKU_DEHIDRASI_BERAT.ResetText()
        txtKU_DEHIDRASI_WHO.ResetText()
        txtKU_ANEMI.ResetText()
        txtKU_KEJANG.ResetText()
        chkKU_KEJANG_L.Checked = False
        chkKU_KEJANG_U.Checked = False
        txtKU_KEJANG_TK.ResetText()
        chkKU_KEJANG_T.Checked = False
        chkKU_KEJANG_K.Checked = False
        txtKU_LETAK_PAKSA.ResetText()
        txtPK.ResetText()
        txtPK_RAMBUT.ResetText()
        txtPK_KUKU.ResetText()
        txtPK_KULIT.ResetText()
        txtPK_GETAH_BENING.ResetText()
        txtPK_KEPALA.ResetText()
        txtPK_MATA.ResetText()
        txtPK_UBUN.ResetText()
        txtPK_PUPIL.ResetText()
        txtPK_THT_TELINGA.ResetText()
        txtPK_THT_HIDUNG.ResetText()
        txtPK_THT_TENGGOROKAN.ResetText()
        txtPK_THT_TONSIL.ResetText()
        txtPK_THT_FARINGS.ResetText()
        txtPK_BIBIR.ResetText()
        txtPK_MULUT.ResetText()
        txtPK_GUSI.ResetText()
        txtPK_GIGI.ResetText()
        txtPK_LANGIT.ResetText()
        txtPK_LIDAH.ResetText()
        txtPK_LEHER.ResetText()
        txtPK_TEKANAN_VENA.ResetText()
        txtPK_KAKU_KUDUK.ResetText()
        txtPK_KELENJAR_GETAH_BENING.ResetText()
        txtPK_LAIN1.ResetText()
        txtPK_LAIN2.ResetText()
        'DINDING_DADA_PARU_IMG
        txtDINDING_DADA_PARU_INSPEKSI.ResetText()
        txtDINDING_DADA_PARU_PALPASI.ResetText()
        txtDINDING_DADA_PARU_PERKUSI.ResetText()
        txtDINDING_DADA_PARU_AUSKULTASI.ResetText()
        'JANTUNG_IMG
        txtJANTUNG_INSPEKSI.ResetText()
        txtJANTUNG_PALPASI.ResetText()
        txtJANTUNG_PERKUSI.ResetText()
        txtJANTUNG_AUSKULTASI.ResetText()
        'txtPERUT_IMG
        txtPERUT_INSPEKSI.ResetText()
        txtPERUT_PALPASI.ResetText()
        txtPERUT_PALPASI_HEPAR.ResetText()
        txtPERUT_PALPASI_LIEN.ResetText()
        txtPERUT_PALPASI_GINJAL.ResetText()
        txtPERUT_PERKUSI.ResetText()
        txtPERUT_AUSKULTASI.ResetText()
        txtGENITALIA.ResetText()
        txtKELAINAN.ResetText()
        txtMATURITAS_SEKS.ResetText()
        txtANGGOTA_GERAK.ResetText()
        txtANGGOTA_GERAK_ATAS.ResetText()
        txtANGGOTA_GERAK_ATAS_SENDI.ResetText()
        txtANGGOTA_GERAK_ATAS_OTOT.ResetText()
        txtANGGOTA_GERAK_BAWAH.ResetText()
        txtANGGOTA_GERAK_BAWAH_SENDI.ResetText()
        txtANGGOTA_GERAK_BAWAH_OTOT.ResetText()
        txtSUSUNAN_SARAF.ResetText()
        txtSUSUNAN_SARAF_R_C_P_TXT.ResetText()

        chkSUSUNAN_SARAF_R_CAHAYA.Checked = False
        chkSUSUNAN_SARAF_R_PUPIL.Checked = False
        txtSUSUNAN_SARAF_R_OKULOSEFALIK.ResetText()
        txtSUSUNAN_SARAF_R_KORNEA.ResetText()
        txtKAKU_KUDUK.ResetText()
        chkBRUDZKINSKY_I.Checked = False
        chkBRUDZKINSKY_II.Checked = False
        chkBRUDZKINSKY_III.Checked = False
        txtBRUDZKINSKY_TXT.ResetText()
        txtKERNIG.ResetText()
        txtLASEQUE.ResetText()
        txtSARAF_OTAK.ResetText()
        txtMOTORIK.ResetText()
        txtSENSORIK.ResetText()
        txtVEGETATIF.ResetText()
        txtAPR.ResetText()
        txtKRP.ResetText()
        txtBABNISKY.ResetText()
        txtCHADDOCK.ResetText()
        txtGORDON.ResetText()
        txtOPPENHEIM.ResetText()
        mmPEMERIKSAAN_PENUNJANG.ResetText()
        mmDIAGNOSIS_BANDING.ResetText()
        mmDIAGNOSIS_KERJA.ResetText()
        mmPENGOBATAN_DAN_TINDAKAN.ResetText()
        mmREKONSILIASI_OBAT.ResetText()
        mmDISCHARGE_PLANNING.ResetText()
        '[CETAK] [Int] Not NULL,
        '[KDUSER] [nvarchar](50) Not NULL,
        '[KDUSER_SIGNATURE] [nvarchar](50) Not NULL,


        ''txtCODE.Text = "<--- AUTO --->"
        'txt1.ResetText()
        'txt2.ResetText()
        'txt3.ResetText()
        ''txt4.ResetText()
        ''txt5.ResetText()
        ''txt6.ResetText()
        ''txt7.ResetText()
        ''txt8.ResetText()
        ''txt9.ResetText()
        'txtNYERI_AKUT_L.ResetText()
        'txtINTENSITAS_A.ResetText()
        'txtINTENSITAS_I_K.ResetText()
        'txtR_I_BCG_DASAR.ResetText()
        'txtNYERI_KRONIS_L.ResetText()
        'txtR_I_DASAR_NAMA6.ResetText()
        'txtINTENSITAS_A_A.ResetText()
        'txtINTENSITAS_I_A.ResetText()
        'txtR_I_POLIO_DASAR.ResetText()
        'txtR_I_DPT_DASAR.ResetText()
        'txtR_I_HEPATITIS_DASAR.ResetText()
        'txtR_I_CAMPAK_DASAR.ResetText()
        'txtR_I_DASAR_NAMA1.ResetText()
        ''MemoEdit4.ResetText()
        ''MemoEdit2.ResetText()
        ''MemoEdit3.ResetText()
        ''MemoEdit5.ResetText()
        ''MemoEdit6.ResetText()
        'fn_LoadAsessmenRawatJalan(sNoId)

        deDATE.DateTime = Now
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIDIGITAL_ASMEDANAK.GetData(sNoid)

            With ds
                grdDOCTOR.Text = .KDUSER_SIGNATURE

                '-------------MULAI--------------'
                .DATEUPDATED = Now
                .DATEUPDATED = Now
                .DATE = Now
                .KDASMEDANAK = ""
                .ANAMNESIS = ""

                deDATE.DateTime = .DATE
                txt1.Text = .KU
                txt2.Text = .RIWAYAT_SEKARANG
                txt3.Text = .RIWAYAT_DAHULU
                chkR_A_YA.Checked = .R_A_YA
                chkR_A_TDK.Checked = .R_A_TDK
                txtR_A_OBAT.Text = .R_A_OBAT
                txtR_A_MAKANAN.Text = .R_A_MAKANAN
                txtR_A_LAIN.Text = .R_A_LAIN
                txtR_A_O_REAKSI.Text = .R_A_O_REAKSI
                txtR_A_M_REAKSI.Text = .R_A_M_REAKSI
                txtR_A_L_REAKSI.Text = .R_A_L_REAKSI
                chkRESIKOCIDERA_JATUH_T.Checked = .RESIKOCIDERA_JATUH_T
                chkRESIKOCIDERA_JATUH_Y.Checked = .RESIKOCIDERA_JATUH_Y
                chkRESIKOCIDERA_JATUH_Y_J.Checked = .RESIKOCIDERA_JATUH_Y_J
                chkSKALA_NYERI_0.Checked = .SKALA_NYERI_0
                chkSKALA_NYERI_1.Checked = .SKALA_NYERI_1
                chkSKALA_NYERI_2.Checked = .SKALA_NYERI_2
                chkSKALA_NYERI_3.Checked = .SKALA_NYERI_3
                chkSKALA_NYERI_4.Checked = .SKALA_NYERI_4
                chkSKALA_NYERI_5.Checked = .SKALA_NYERI_5
                chkSKALA_NYERI_6.Checked = .SKALA_NYERI_6
                chkSKALA_NYERI_7.Checked = .SKALA_NYERI_7
                chkSKALA_NYERI_8.Checked = .SKALA_NYERI_8
                chkSKALA_NYERI_9.Checked = .SKALA_NYERI_9
                chkSKALA_NYERI_10.Checked = .SKALA_NYERI_10
                chkNYERI_KRONIS.Checked = .NYERI_KRONIS
                txtNYERI_KRONIS_L.Text = .NYERI_KRONIS_L
                chkNYERI_AKUT.Checked = .NYERI_AKUT
                txtNYERI_AKUT_L.Text = .NYERI_AKUT_L
                txtINTENSITAS_I_K.Text = .INTENSITAS_I_K
                txtINTENSITAS_A.Text = .INTENSITAS_A
                txtINTENSITAS_I_A.Text = .INTENSITAS_I_A
                txtINTENSITAS_A_A.Text = .INTENSITAS_A_A
                chkNYERI_H_MO.Checked = .NYERI_H_MO
                chkNYERI_H_I.Checked = .NYERI_H_I
                chkNYERI_H_MM.Checked = .NYERI_H_MM
                chkNYERI_H_BPT.Checked = .NYERI_H_BPT
                txtNYERI_H_LAIN.Text = .NYERI_H_LAIN
                txtRIWAYAT_IMUNISASI.Text = .RIWAYAT_IMUNISASI
                txtR_I_NAMA1.Text = .R_I_NAMA1
                txtR_I_NAMA2.Text = .R_I_NAMA2
                txtR_I_NAMA3.Text = .R_I_NAMA3
                txtR_I_NAMA4.Text = .R_I_NAMA4
                txtR_I_NAMA5.Text = .R_I_NAMA5
                txtR_I_NAMA6.Text = .R_I_NAMA6
                txtR_I_BCG_DASAR.Text = .R_I_BCG_DASAR
                txtR_I_POLIO_DASAR.Text = .R_I_POLIO_DASAR
                txtR_I_DPT_DASAR.Text = .R_I_DPT_DASAR
                txtR_I_CAMPAK_DASAR.Text = .R_I_CAMPAK_DASAR
                txtR_I_HEPATITIS_DASAR.Text = .R_I_HEPATITIS_DASAR
                txtR_I_DASAR_NAMA1.Text = .R_I_DASAR_NAMA1
                txtR_I_DASAR_NAMA2.Text = .R_I_DASAR_NAMA2
                txtR_I_DASAR_NAMA3.Text = .R_I_DASAR_NAMA3
                txtR_I_DASAR_NAMA4.Text = .R_I_DASAR_NAMA4
                txtR_I_DASAR_NAMA5.Text = .R_I_DASAR_NAMA5
                txtR_I_DASAR_NAMA6.Text = .R_I_DASAR_NAMA6
                txtR_I_BCG_ULANG.Text = .R_I_BCG_ULANG
                txtR_I_POLIO_ULANG.Text = .R_I_POLIO_ULANG
                txtR_I_DPT_ULANG.Text = .R_I_DPT_ULANG
                txtR_I_CAMPAK_ULANG.Text = .R_I_CAMPAK_ULANG
                txtR_I_HEPATITIS_ULANG.Text = .R_I_HEPATITIS_ULANG
                txtR_I_ULANG_NAMA1.Text = .R_I_ULANG_NAMA1
                txtR_I_ULANG_NAMA2.Text = .R_I_ULANG_NAMA2
                txtR_I_ULANG_NAMA3.Text = .R_I_ULANG_NAMA3
                txtR_I_ULANG_NAMA4.Text = .R_I_ULANG_NAMA4
                txtR_I_ULANG_NAMA5.Text = .R_I_ULANG_NAMA5
                txtR_I_ULANG_NAMA6.Text = .R_I_ULANG_NAMA6
                txtK_K_AYAH.Text = .K_K_AYAH
                txtK_K_IBU.Text = .K_K_IBU
                txtK_K_SAUDARA.Text = .K_K_SAUDARA
                txtK_K_SERUMAH.Text = .K_K_SERUMAH
                txtPERKEMBANGAN.Text = .PERKEMBANGAN
                txtP_BERBALIK.Text = .P_BERBALIK
                txtP_DUDUK_T_B.Text = .P_DUDUK_T_B
                txtP_DUDUK_T_P.Text = .P_DUDUK_T_P
                txtP_BERJALAN1.Text = .P_BERJALAN1
                txtP_BERJALAN2.Text = .P_BERJALAN2
                txtP_LAIN.Text = .P_LAIN
                txtP_BICARA_KATA.Text = .P_BICARA_KATA
                txtP_BICARA_KALIMAT.Text = .P_BICARA_KALIMAT
                txtP_MEMBACA.Text = .P_MEMBACA
                txtP_MENULIS.Text = .P_MENULIS
                txtP_SEKOLAH.Text = .P_SEKOLAH
                txtGIGI_GELIGI.Text = .GIGI_GELIGI
                txtGG_PERTAMA.Text = .GG_PERTAMA
                txtGG_SEKARANG.Text = .GG_SEKARANG
                chkGS_KI_A_V.Checked = .GS_KI_A_V
                chkGS_KI_A_IV.Checked = .GS_KI_A_IV
                chkGS_KI_A_III.Checked = .GS_KI_A_III
                chkGS_KI_A_II.Checked = .GS_KI_A_II
                chkGS_KI_A_I.Checked = .GS_KI_A_I
                chkGS_KA_A_V.Checked = .GS_KA_A_V
                chkGS_KA_A_IV.Checked = .GS_KA_A_IV
                chkGS_KA_A_III.Checked = .GS_KA_A_III
                chkGS_KA_A_II.Checked = .GS_KA_A_II
                chkGS_KA_A_I.Checked = .GS_KA_A_I
                chkGS_KI_B_V.Checked = .GS_KI_B_V
                chkGS_KI_B_IV.Checked = .GS_KI_B_IV
                chkGS_KI_B_III.Checked = .GS_KI_B_III
                chkGS_KI_B_II.Checked = .GS_KI_B_II
                chkGS_KI_B_I.Checked = .GS_KI_B_I
                chkGS_KA_B_V.Checked = .GS_KA_B_V
                chkGS_KA_B_IV.Checked = .GS_KA_B_IV
                chkGS_KA_B_III.Checked = .GS_KA_B_III
                chkGS_KA_B_II.Checked = .GS_KA_B_II
                chkGS_KA_B_I.Checked = .GS_KA_B_I
                chkGT_KI_A_8.Checked = .GT_KI_A_8
                chkGT_KI_A_7.Checked = .GT_KI_A_7
                chkGT_KI_A_6.Checked = .GT_KI_A_6
                chkGT_KI_A_5.Checked = .GT_KI_A_5
                chkGT_KI_A_4.Checked = .GT_KI_A_4
                chkGT_KI_A_3.Checked = .GT_KI_A_3
                chkGT_KI_A_2.Checked = .GT_KI_A_2
                chkGT_KI_A_1.Checked = .GT_KI_A_1
                chkGT_KI_B_8.Checked = .GT_KI_B_8
                chkGT_KI_B_7.Checked = .GT_KI_B_7
                chkGT_KI_B_6.Checked = .GT_KI_B_6
                chkGT_KI_B_5.Checked = .GT_KI_B_5
                chkGT_KI_B_4.Checked = .GT_KI_B_4
                chkGT_KI_B_3.Checked = .GT_KI_B_3
                chkGT_KI_B_2.Checked = .GT_KI_B_2
                chkGT_KI_B_1.Checked = .GT_KI_B_1
                chkGT_KA_A_1.Checked = .GT_KA_A_1
                chkGT_KA_A_2.Checked = .GT_KA_A_2
                chkGT_KA_A_3.Checked = .GT_KA_A_3
                chkGT_KA_A_4.Checked = .GT_KA_A_4
                chkGT_KA_A_5.Checked = .GT_KA_A_5
                chkGT_KA_A_6.Checked = .GT_KA_A_6
                chkGT_KA_A_7.Checked = .GT_KA_A_7
                chkGT_KA_A_8.Checked = .GT_KA_A_8
                chkGT_KA_B_1.Checked = .GT_KA_B_1
                chkGT_KA_B_2.Checked = .GT_KA_B_2
                chkGT_KA_B_3.Checked = .GT_KA_B_3
                chkGT_KA_B_4.Checked = .GT_KA_B_4
                chkGT_KA_B_5.Checked = .GT_KA_B_5
                chkGT_KA_B_6.Checked = .GT_KA_B_6
                chkGT_KA_B_7.Checked = .GT_KA_B_7
                chkGT_KA_B_8.Checked = .GT_KA_B_8
                txtM_JENIS_MAKAN1.Text = .M_JENIS_MAKAN1
                txtM_JENIS_MAKAN2.Text = .M_JENIS_MAKAN2
                txtM_JENIS_MAKAN3.Text = .M_JENIS_MAKAN3
                txtM_JENIS_MAKAN4.Text = .M_JENIS_MAKAN4
                txtM_JENIS_MAKAN5.Text = .M_JENIS_MAKAN5
                txtM_KUANTITAS1.Text = .M_KUANTITAS1
                txtM_KUANTITAS2.Text = .M_KUANTITAS2
                txtM_KUANTITAS3.Text = .M_KUANTITAS3
                txtM_KUANTITAS4.Text = .M_KUANTITAS4
                txtM_KUANTITAS5.Text = .M_KUANTITAS5
                txtM_KUALITAS1.Text = .M_KUALITAS1
                txtM_KUALITAS2.Text = .M_KUALITAS2
                txtM_KUALITAS3.Text = .M_KUALITAS3
                txtM_KUALITAS4.Text = .M_KUALITAS4
                txtM_KUALITAS5.Text = .M_KUALITAS5
                chkPENYAKIT_CAMPAK.Checked = .PENYAKIT_CAMPAK
                chkPENYAKIT_BATUK.Checked = .PENYAKIT_BATUK
                chkPENYAKIT_TBC.Checked = .PENYAKIT_TBC
                chkPENYAKIT_DIFTERI.Checked = .PENYAKIT_DIFTERI
                chkPENYAKIT_TETANUS.Checked = .PENYAKIT_TETANUS
                chkPENYAKIT_DIARE.Checked = .PENYAKIT_DIARE
                chkPENYAKIT_DEMAM.Checked = .PENYAKIT_DEMAM
                chkPENYAKIT_KUNING.Checked = .PENYAKIT_KUNING
                chkPENYAKIT_CACING.Checked = .PENYAKIT_CACING
                chkPENYAKIT_KEJANG.Checked = .PENYAKIT_KEJANG
                chkPENYAKIT_BENGEK.Checked = .PENYAKIT_BENGEK
                chkPENYAKIT_EKSIM.Checked = .PENYAKIT_EKSIM
                chkPENYAKIT_KALIGATA.Checked = .PENYAKIT_KALIGATA
                chkPENYAKIT_TENGGOROKAN.Checked = .PENYAKIT_TENGGOROKAN
                txtPENYAKIT_LAIN.Text = .PENYAKIT_LAIN
                txtPENGUKURAN.Text = .PENGUKURAN
                txtP_UMUR.Text = .P_UMUR
                txtP_BERATBADAN.Text = .P_BERATBADAN
                txtP_TINGGI_BADAN.Text = .P_TINGGI_BADAN
                txtP_LINGKARKEPALA.Text = .P_LINGKARKEPALA
                txtP_LINGKARDADA.Text = .P_LINGKARDADA
                txtP_LENGAN_ATAS.Text = .P_LENGAN_ATAS
                txtP_STATUSGIZI_BBU.Text = .P_STATUSGIZI_BBU
                txtP_STATUSGIZI_TBU.Text = .P_STATUSGIZI_TBU
                txtP_STATUSGIZI_BBTB.Text = .P_STATUSGIZI_BBTB
                txtP_STATUSGIZI_BMIU.Text = .P_STATUSGIZI_BMIU
                txtTV.Text = .TV
                txtTV_LAJUNAFAS.Text = .TV_LAJUNAFAS
                txtTV_LAJUNAFAS_TIPE.Text = .TV_LAJUNAFAS_TIPE
                txtTV_TEKANAN_DARAH.Text = .TV_TEKANAN_DARAH
                chkTV_TEKANAN_DARAH_SISTOLIK.Checked = .TV_TEKANAN_DARAH_SISTOLIK
                chkTV_TEKANAN_DARAH_DIASTOLIK.Checked = .TV_TEKANAN_DARAH_DIASTOLIK
                txtTV_SUHU.Text = .TV_SUHU
                txtTV_LAJUNADI.Text = .TV_LAJUNADI
                txtTV_LAJUNADI_KUALITAS.Text = .TV_LAJUNADI_KUALITAS
                chkTV_LAJUNADI_REGULAR.Checked = .TV_LAJUNADI_REGULAR
                chkTV_LAJUNADI_IREGULER.Checked = .TV_LAJUNADI_IREGULER
                txtTV_LAJUNADI_REGULER_IREGULER_TXT.Text = .TV_LAJUNADI_REGULER_IREGULER_TXT
                txtTV_LAJUNADI_ISI.Text = .TV_LAJUNADI_ISI
                txtK_U.Text = .K_U
                chkKU_KEADAAN_SAKIT_TIDAK.Checked = .KU_KEADAAN_SAKIT_TIDAK
                chkKU_KEADAAN_SAKIT_RINGAN.Checked = .KU_KEADAAN_SAKIT_RINGAN
                chkKU_KEADAAN_SAKIT_SEDANG.Checked = .KU_KEADAAN_SAKIT_SEDANG
                chkKU_KEADAAN_SAKIT_BERAT.Checked = .KU_KEADAAN_SAKIT_BERAT
                txtKU_KESADARAN_KUANTITATIF.Text = .KU_KESADARAN_KUANTITATIF
                txtKU_KESADARAN_KUANTITATIF_E.Text = .KU_KESADARAN_KUANTITATIF_E
                txtKU_KESADARAN_KUANTITATIF_V.Text = .KU_KESADARAN_KUANTITATIF_V
                txtKU_KESADARAN_KUANTITATIF_M.Text = .KU_KESADARAN_KUANTITATIF_M
                txtKU_KESADARAN_KUALITATIF.Text = .KU_KESADARAN_KUALITATIF
                chkKU_SESAK_P.Checked = .KU_SESAK_P
                chkKU_SESAK_M.Checked = .KU_SESAK_M
                txtKU_SESAK_PCH.Text = .KU_SESAK_PCH
                txtKU_SESAK_RETRAKSI.Text = .KU_SESAK_RETRAKSI
                txtKU_SESAK_RETRAKSI2.Text = .KU_SESAK_RETRAKSI2
                chkKU_SIANOSIS_S.Checked = .KU_SIANOSIS_S
                chkKU_SIANOSIS_P.Checked = .KU_SIANOSIS_P
                txtKU_SIANOSIS_S_P_TXT.Text = .KU_SIANOSIS_S_P_TXT
                txtKU_IKTERUS_KRAMER.Text = .KU_IKTERUS_KRAMER
                txtKU_IKTERUS_ANAK.Text = .KU_IKTERUS_ANAK
                txtKU_EDEMA.Text = .KU_EDEMA
                txtKU_EDEMA_ANASARKA.Text = .KU_EDEMA_ANASARKA
                txtKU_DEHIDRASI.Text = .KU_DEHIDRASI
                chkKU_DEHIDRASI_R.Checked = .KU_DEHIDRASI_R
                chkKU_DEHIDRASI_S.Checked = .KU_DEHIDRASI_S
                txtKU_DEHIDRASI_BERAT.Text = .KU_DEHIDRASI_BERAT
                txtKU_DEHIDRASI_WHO.Text = .KU_DEHIDRASI_WHO
                txtKU_ANEMI.Text = .KU_ANEMI
                txtKU_KEJANG.Text = .KU_KEJANG
                chkKU_KEJANG_L.Checked = .KU_KEJANG_L
                chkKU_KEJANG_U.Checked = .KU_KEJANG_U
                txtKU_KEJANG_TK.Text = .KU_KEJANG_TK
                chkKU_KEJANG_T.Checked = .KU_KEJANG_T
                chkKU_KEJANG_K.Checked = .KU_KEJANG_K
                txtKU_LETAK_PAKSA.Text = .KU_LETAK_PAKSA
                txtPK.Text = .PK
                txtPK_RAMBUT.Text = .PK_RAMBUT
                txtPK_KUKU.Text = .PK_KUKU
                txtPK_KULIT.Text = .PK_KULIT
                txtPK_GETAH_BENING.Text = .PK_GETAH_BENING
                txtPK_KEPALA.Text = .PK_KEPALA
                txtPK_MATA.Text = .PK_MATA
                txtPK_UBUN.Text = .PK_UBUN
                txtPK_PUPIL.Text = .PK_PUPIL
                txtPK_THT_TELINGA.Text = .PK_THT_TELINGA
                txtPK_THT_HIDUNG.Text = .PK_THT_HIDUNG
                txtPK_THT_TENGGOROKAN.Text = .PK_THT_TENGGOROKAN
                txtPK_THT_TONSIL.Text = .PK_THT_TONSIL
                txtPK_THT_FARINGS.Text = .PK_THT_FARINGS
                txtPK_BIBIR.Text = .PK_BIBIR
                txtPK_MULUT.Text = .PK_MULUT
                txtPK_GUSI.Text = .PK_GUSI
                txtPK_GIGI.Text = .PK_GIGI
                txtPK_LANGIT.Text = .PK_LANGIT
                txtPK_LIDAH.Text = .PK_LIDAH
                txtPK_LEHER.Text = .PK_LEHER
                txtPK_TEKANAN_VENA.Text = .PK_TEKANAN_VENA
                txtPK_KAKU_KUDUK.Text = .PK_KAKU_KUDUK
                txtPK_KELENJAR_GETAH_BENING.Text = .PK_KELENJAR_GETAH_BENING
                txtPK_LAIN1.Text = .PK_LAIN1
                txtPK_LAIN2.Text = .PK_LAIN2
                '.DINDING_DADA_PARU_IMG = ""
                Try
                    Dim img = oS_DIDIGITAL_ASMEDANAK.GetData(sNoid).DINDING_DADA_PARU_IMG

                    PicGambar1.Image = ByteArrayToImage(img.ToArray())
                Catch oErr As Exception
                End Try
                txtDINDING_DADA_PARU_INSPEKSI.Text = .DINDING_DADA_PARU_INSPEKSI
                txtDINDING_DADA_PARU_PALPASI.Text = .DINDING_DADA_PARU_PALPASI
                txtDINDING_DADA_PARU_PERKUSI.Text = .DINDING_DADA_PARU_PERKUSI
                txtDINDING_DADA_PARU_AUSKULTASI.Text = .DINDING_DADA_PARU_AUSKULTASI
                '.JANTUNG_IMG = ""
                Try
                    Dim img = oS_DIDIGITAL_ASMEDANAK.GetData(sNoid).JANTUNG_IMG

                    PicGambar2.Image = ByteArrayToImage(img.ToArray())
                Catch oErr As Exception
                End Try

                txtJANTUNG_INSPEKSI.Text = .JANTUNG_INSPEKSI
                txtJANTUNG_PALPASI.Text = .JANTUNG_PALPASI
                txtJANTUNG_PERKUSI.Text = .JANTUNG_PERKUSI
                txtJANTUNG_AUSKULTASI.Text = .JANTUNG_AUSKULTASI
                '.PERUT_IMG = ""
                Try
                    Dim img = oS_DIDIGITAL_ASMEDANAK.GetData(sNoid).PERUT_IMG

                    PicGambar3.Image = ByteArrayToImage(img.ToArray())
                Catch oErr As Exception
                End Try
                txtPERUT_INSPEKSI.Text = .PERUT_INSPEKSI
                txtPERUT_PALPASI.Text = .PERUT_PALPASI
                txtPERUT_PALPASI_HEPAR.Text = .PERUT_PALPASI_HEPAR
                txtPERUT_PALPASI_LIEN.Text = .PERUT_PALPASI_LIEN
                txtPERUT_PALPASI_GINJAL.Text = .PERUT_PALPASI_GINJAL
                txtPERUT_PERKUSI.Text = .PERUT_PERKUSI
                txtPERUT_AUSKULTASI.Text = .PERUT_AUSKULTASI
                txtGENITALIA.Text = .GENITALIA
                txtKELAINAN.Text = .KELAINAN
                txtMATURITAS_SEKS.Text = .MATURITAS_SEKS
                txtANGGOTA_GERAK.Text = .ANGGOTA_GERAK
                txtANGGOTA_GERAK_ATAS.Text = .ANGGOTA_GERAK_ATAS
                txtANGGOTA_GERAK_ATAS_SENDI.Text = .ANGGOTA_GERAK_ATAS_SENDI
                txtANGGOTA_GERAK_ATAS_OTOT.Text = .ANGGOTA_GERAK_ATAS_OTOT
                txtANGGOTA_GERAK_BAWAH.Text = .ANGGOTA_GERAK_BAWAH
                txtANGGOTA_GERAK_BAWAH_SENDI.Text = .ANGGOTA_GERAK_BAWAH_SENDI
                txtANGGOTA_GERAK_BAWAH_OTOT.Text = .ANGGOTA_GERAK_BAWAH_OTOT
                txtSUSUNAN_SARAF.Text = .SUSUNAN_SARAF
                txtSUSUNAN_SARAF_R_C_P_TXT.Text = .SUSUNAN_SARAF_R_C_P_TXT
                chkSUSUNAN_SARAF_R_CAHAYA.Checked = .SUSUNAN_SARAF_R_CAHAYA
                chkSUSUNAN_SARAF_R_PUPIL.Checked = .SUSUNAN_SARAF_R_PUPIL
                txtSUSUNAN_SARAF_R_OKULOSEFALIK.Text = .SUSUNAN_SARAF_R_OKULOSEFALIK
                txtSUSUNAN_SARAF_R_KORNEA.Text = .SUSUNAN_SARAF_R_KORNEA
                txtKAKU_KUDUK.Text = .KAKU_KUDUK
                chkBRUDZKINSKY_I.Checked = .BRUDZKINSKY_I
                chkBRUDZKINSKY_II.Checked = .BRUDZKINSKY_II
                chkBRUDZKINSKY_III.Checked = .BRUDZKINSKY_III
                txtBRUDZKINSKY_TXT.Text = .BRUDZKINSKY_TXT
                txtKERNIG.Text = .KERNIG
                txtLASEQUE.Text = .LASEQUE
                txtSARAF_OTAK.Text = .SARAF_OTAK
                txtMOTORIK.Text = .MOTORIK
                txtSENSORIK.Text = .SENSORIK
                txtVEGETATIF.Text = .VEGETATIF
                txtAPR.Text = .APR
                txtKRP.Text = .KRP
                txtBABNISKY.Text = .BABNISKY
                txtCHADDOCK.Text = .CHADDOCK
                txtGORDON.Text = .GORDON
                txtOPPENHEIM.Text = .OPPENHEIM
                mmPEMERIKSAAN_PENUNJANG.Text = .PEMERIKSAAN_PENUNJANG
                mmDIAGNOSIS_BANDING.Text = .DIAGNOSIS_BANDING
                mmDIAGNOSIS_KERJA.Text = .DIAGNOSIS_KERJA
                mmPENGOBATAN_DAN_TINDAKAN.Text = .PENGOBATAN_DAN_TINDAKAN
                mmREKONSILIASI_OBAT.Text = .REKONSILIASI_OBAT
                mmDISCHARGE_PLANNING.Text = .DISCHARGE_PLANNING

            End With
            '-------------SELESAI-------------'





            'txt1.Text = .SDIGITALRJ36_1
            'txt2.Text = .SDIGITALRJ36_2
            'txt3.Text = .SDIGITALRJ36_3
            'txt4.Text = .SDIGITALRJ36_4
            'txt5.Text = .SDIGITALRJ36_5
            'txt6.Text = .SDIGITALRJ36_6
            'txt7.Text = .SDIGITALRJ36_7
            'txt8.Text = .SDIGITALRJ36_8
            'txt9.Text = .SDIGITALRJ36_9
            'txtNYERI_AKUT_L.Text = .SDIGITALRJ36_10
            'txtINTENSITAS_A.Text = .SDIGITALRJ36_11
            'txtINTENSITAS_I_K.Text = .SDIGITALRJ36_12
            'txtR_I_BCG_DASAR.Text = .SDIGITALRJ36_13
            'chkPENYAKIT_TETANUS.Checked = .SDIGITALRJ36_14
            'txtNYERI_KRONIS_L.Text = .SDIGITALRJ36_15
            'chkNYERI_KRONIS.Checked = .SDIGITALRJ36_16
            'txtR_I_DASAR_NAMA6.Text = .SDIGITALRJ36_17
            'chkNYERI_AKUT.Checked = .SDIGITALRJ36_18
            'txtINTENSITAS_A_A.Text = .SDIGITALRJ36_19
            'chkPENYAKIT_TBC.Checked = .SDIGITALRJ36_20
            'chkPENYAKIT_DIFTERI.Checked = .SDIGITALRJ36_21
            'chkGS_KI_B_IV.Checked = .SDIGITALRJ36_22
            'chkPENYAKIT_DIARE.Checked = .SDIGITALRJ36_23
            'txtINTENSITAS_I_A.Text = .SDIGITALRJ36_24
            'Try
            '    Dim img = oS_DIDIGITAL_ASMEDANAK.GetData(sNoId).SDIGITALRJ36_25

            '    picGAMBAR2.Image = ByteArrayToImage(img.ToArray())
            'Catch oErr As Exception
            'End Try
            'chkGS_KI_B_II.Checked = .SDIGITALRJ36_26
            'chkGS_KI_B_III.Checked = .SDIGITALRJ36_27
            'chkGS_KI_B_I.Checked = .SDIGITALRJ36_28
            'txtR_I_POLIO_DASAR.Text = .SDIGITALRJ36_29
            'txtR_I_DPT_DASAR.Text = .SDIGITALRJ36_30
            'chkGS_KA_A_III.Checked = .SDIGITALRJ36_31
            'txtR_I_HEPATITIS_DASAR.Text = .SDIGITALRJ36_32
            'chkGS_KA_A_II.Checked = .SDIGITALRJ36_33
            'txtR_I_CAMPAK_DASAR.Text = .SDIGITALRJ36_34
            'chkGS_KA_A_I.Checked = .SDIGITALRJ36_35
            'txtR_I_DASAR_NAMA1.Text = .SDIGITALRJ36_36
            'MemoEdit4.Text = .SDIGITALRJ36_37
            'MemoEdit2.Text = .SDIGITALRJ36_38
            'MemoEdit3.Text = .SDIGITALRJ36_39
            'MemoEdit5.Text = .SDIGITALRJ36_40
            'MemoEdit6.Text = .SDIGITALRJ36_41

        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadAsessmenRawatJalan(ByVal Parameter As String)
        Dim oAsessmenRawatJalan As New EMedrek.clsDigital_RJ_08
        Dim dsAsessmenRawatJalan = oAsessmenRawatJalan.GetData(Parameter)

        If dsAsessmenRawatJalan IsNot Nothing Then
            'txtSOAP_O.Text = "Tensi : " & dsAsessmenRawatJalan.TANDA_VITAL_01 & " mmHg" & vbCrLf & vbCrLf & "Nadi : " & dsAsessmenRawatJalan.TANDA_VITAL_02 & " x/m" & vbCrLf & vbCrLf & "Respirasi : " & dsAsessmenRawatJalan.TANDA_VITAL_04 & " x/m" & vbCrLf & vbCrLf & "Suhu : " & dsAsessmenRawatJalan.TANDA_VITAL_03 & " °C" & vbCrLf & vbCrLf & "Berat Badan : " & dsAsessmenRawatJalan.TANDA_VITAL_05 & " Kg" & vbCrLf & vbCrLf & "Tinggi Badan : " & dsAsessmenRawatJalan.TANDA_VITAL_06 & " Cm"
            'txt1.Text = dsAsessmenRawatJalan.KELUHAN_UTAMA
            'txt7.Text = dsAsessmenRawatJalan.TANDA_VITAL_05
            'txt8.Text = dsAsessmenRawatJalan.TANDA_VITAL_06
            'txtNYERI_AKUT_L.Text = dsAsessmenRawatJalan.TANDA_VITAL_02
            'txtINTENSITAS_A.Text = dsAsessmenRawatJalan.TANDA_VITAL_01
            'txtINTENSITAS_I_K.Text = dsAsessmenRawatJalan.TANDA_VITAL_03
            'txtR_I_BCG_DASAR.Text = dsAsessmenRawatJalan.TANDA_VITAL_04
            'txt5.Text = dsAsessmenRawatJalan.KESADARAN_UMUM
            'txt4.Text = dsAsessmenRawatJalan.ALERGI_02_02_TEXT
        Else
            'MsgBox("Assemen Awal Keperawatan rawat jalan belum di input", MsgBoxStyle.Exclamation, Me.Text)
        End If
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
            If sNoid = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                deDATE.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdDOCTOR.Text = String.Empty Then
                MsgBox("Dibutuhkan Dokter", MsgBoxStyle.Exclamation, Me.Text)
                grdDOCTOR.Focus()
                fn_Validate = False
                Exit Function
            End If
            'If grdDEPARTMENT.Text = String.Empty Then
            '    MsgBox("Dibutuhkan Poliklinik", MsgBoxStyle.Exclamation, Me.Text)
            '    grdDEPARTMENT.Focus()
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
            Dim ds = oS_DIDIGITAL_ASMEDANAK.GetStructureHeader
            With ds
                .KDIDENTITAS = sKodeIdentitas
                .KDKUNJUNGAN = sNoid
                Try
                    .DATECREATED = oS_DIDIGITAL_ASMEDANAK.GetData(sNoid).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .KDASMEDANAK = ""

                .ANAMNESIS = ""
                .KU = txt1.Text
                .RIWAYAT_SEKARANG = txt2.Text
                .RIWAYAT_DAHULU = txt3.Text
                .R_A_YA = chkR_A_YA.Checked
                .R_A_TDK = chkR_A_TDK.Checked
                .R_A_OBAT = txtR_A_OBAT.Text
                .R_A_MAKANAN = txtR_A_MAKANAN.Text
                .R_A_LAIN = txtR_A_LAIN.Text
                .R_A_O_REAKSI = txtR_A_O_REAKSI.Text
                .R_A_M_REAKSI = txtR_A_M_REAKSI.Text
                .R_A_L_REAKSI = txtR_A_L_REAKSI.Text
                .RESIKOCIDERA_JATUH_T = chkRESIKOCIDERA_JATUH_T.Checked
                .RESIKOCIDERA_JATUH_Y = chkRESIKOCIDERA_JATUH_Y.Checked
                .RESIKOCIDERA_JATUH_Y_J = chkRESIKOCIDERA_JATUH_Y_J.Checked
                .SKALA_NYERI_0 = chkSKALA_NYERI_0.Checked
                .SKALA_NYERI_1 = chkSKALA_NYERI_1.Checked
                .SKALA_NYERI_2 = chkSKALA_NYERI_2.Checked
                .SKALA_NYERI_3 = chkSKALA_NYERI_3.Checked
                .SKALA_NYERI_4 = chkSKALA_NYERI_4.Checked
                .SKALA_NYERI_5 = chkSKALA_NYERI_5.Checked
                .SKALA_NYERI_6 = chkSKALA_NYERI_6.Checked
                .SKALA_NYERI_7 = chkSKALA_NYERI_7.Checked
                .SKALA_NYERI_8 = chkSKALA_NYERI_8.Checked
                .SKALA_NYERI_9 = chkSKALA_NYERI_9.Checked
                .SKALA_NYERI_10 = chkSKALA_NYERI_10.Checked
                .NYERI_KRONIS = chkNYERI_KRONIS.Checked

                .NYERI_KRONIS_L = txtNYERI_KRONIS_L.Text
                .NYERI_AKUT = chkNYERI_AKUT.Checked
                .NYERI_AKUT_L = txtNYERI_AKUT_L.Text
                .INTENSITAS_I_K = txtINTENSITAS_I_K.Text
                .INTENSITAS_A = txtINTENSITAS_A.Text
                .INTENSITAS_I_A = txtINTENSITAS_I_A.Text
                .INTENSITAS_A_A = txtINTENSITAS_A_A.Text
                .NYERI_H_MO = chkNYERI_H_MO.Checked
                .NYERI_H_I = chkNYERI_H_I.Checked
                .NYERI_H_MM = chkNYERI_H_MM.Checked
                .NYERI_H_BPT = chkNYERI_H_BPT.Checked

                .NYERI_H_LAIN = txtNYERI_H_LAIN.Text
                .RIWAYAT_IMUNISASI = txtRIWAYAT_IMUNISASI.Text
                .R_I_NAMA1 = txtR_I_NAMA1.Text
                .R_I_NAMA2 = txtR_I_NAMA2.Text
                .R_I_NAMA3 = txtR_I_NAMA3.Text
                .R_I_NAMA4 = txtR_I_NAMA4.Text
                .R_I_NAMA5 = txtR_I_NAMA5.Text
                .R_I_NAMA6 = txtR_I_NAMA6.Text
                .R_I_BCG_DASAR = txtR_I_BCG_DASAR.Text
                .R_I_POLIO_DASAR = txtR_I_POLIO_DASAR.Text
                .R_I_DPT_DASAR = txtR_I_DPT_DASAR.Text
                .R_I_CAMPAK_DASAR = txtR_I_CAMPAK_DASAR.Text
                .R_I_HEPATITIS_DASAR = txtR_I_HEPATITIS_DASAR.Text
                .R_I_DASAR_NAMA1 = txtR_I_DASAR_NAMA1.Text
                .R_I_DASAR_NAMA2 = txtR_I_DASAR_NAMA2.Text
                .R_I_DASAR_NAMA3 = txtR_I_DASAR_NAMA3.Text
                .R_I_DASAR_NAMA4 = txtR_I_DASAR_NAMA4.Text
                .R_I_DASAR_NAMA5 = txtR_I_DASAR_NAMA5.Text
                .R_I_DASAR_NAMA6 = txtR_I_DASAR_NAMA6.Text
                .R_I_BCG_ULANG = txtR_I_BCG_ULANG.Text
                .R_I_POLIO_ULANG = txtR_I_POLIO_ULANG.Text
                .R_I_DPT_ULANG = txtR_I_DPT_ULANG.Text
                .R_I_CAMPAK_ULANG = txtR_I_CAMPAK_ULANG.Text
                .R_I_HEPATITIS_ULANG = txtR_I_HEPATITIS_ULANG.Text
                .R_I_ULANG_NAMA1 = txtR_I_ULANG_NAMA1.Text
                .R_I_ULANG_NAMA2 = txtR_I_ULANG_NAMA2.Text
                .R_I_ULANG_NAMA3 = txtR_I_ULANG_NAMA3.Text
                .R_I_ULANG_NAMA4 = txtR_I_ULANG_NAMA4.Text
                .R_I_ULANG_NAMA5 = txtR_I_ULANG_NAMA5.Text
                .R_I_ULANG_NAMA6 = txtR_I_ULANG_NAMA6.Text

                .K_K_AYAH = txtK_K_AYAH.Text
                .K_K_IBU = txtK_K_IBU.Text
                .K_K_SAUDARA = txtK_K_SAUDARA.Text
                .K_K_SERUMAH = txtK_K_SERUMAH.Text
                .PERKEMBANGAN = txtPERKEMBANGAN.Text
                .P_BERBALIK = txtP_BERBALIK.Text
                .P_DUDUK_T_B = txtP_DUDUK_T_B.Text
                .P_DUDUK_T_P = txtP_DUDUK_T_P.Text
                .P_BERJALAN1 = txtP_BERJALAN1.Text
                .P_BERJALAN2 = txtP_BERJALAN2.Text
                .P_LAIN = txtP_LAIN.Text
                .P_BICARA_KATA = txtP_BICARA_KATA.Text
                .P_BICARA_KALIMAT = txtP_BICARA_KALIMAT.Text
                .P_MEMBACA = txtP_MEMBACA.Text
                .P_MENULIS = txtP_MENULIS.Text
                .P_SEKOLAH = txtP_SEKOLAH.Text
                .GIGI_GELIGI = txtGIGI_GELIGI.Text
                .GG_PERTAMA = txtGG_PERTAMA.Text
                .GG_SEKARANG = txtGG_SEKARANG.Text

                .GS_KI_A_V = chkGS_KI_A_V.Checked
                .GS_KI_A_IV = chkGS_KI_A_IV.Checked
                .GS_KI_A_III = chkGS_KI_A_III.Checked
                .GS_KI_A_II = chkGS_KI_A_II.Checked
                .GS_KI_A_I = chkGS_KI_A_I.Checked
                .GS_KA_A_V = chkGS_KA_A_V.Checked
                .GS_KA_A_IV = chkGS_KA_A_IV.Checked
                .GS_KA_A_III = chkGS_KA_A_III.Checked
                .GS_KA_A_II = chkGS_KA_A_II.Checked
                .GS_KA_A_I = chkGS_KA_A_I.Checked
                .GS_KI_B_V = chkGS_KI_B_V.Checked
                .GS_KI_B_IV = chkGS_KI_B_IV.Checked
                .GS_KI_B_III = chkGS_KI_B_III.Checked
                .GS_KI_B_II = chkGS_KI_B_II.Checked
                .GS_KI_B_I = chkGS_KI_B_I.Checked
                .GS_KA_B_V = chkGS_KA_B_V.Checked
                .GS_KA_B_IV = chkGS_KA_B_IV.Checked
                .GS_KA_B_III = chkGS_KA_B_III.Checked
                .GS_KA_B_II = chkGS_KA_B_II.Checked
                .GS_KA_B_I = chkGS_KA_B_I.Checked
                .GT_KI_A_8 = chkGT_KI_A_8.Checked
                .GT_KI_A_7 = chkGT_KI_A_7.Checked
                .GT_KI_A_6 = chkGT_KI_A_6.Checked
                .GT_KI_A_5 = chkGT_KI_A_5.Checked
                .GT_KI_A_4 = chkGT_KI_A_4.Checked
                .GT_KI_A_3 = chkGT_KI_A_3.Checked
                .GT_KI_A_2 = chkGT_KI_A_2.Checked
                .GT_KI_A_1 = chkGT_KI_A_1.Checked
                .GT_KI_B_8 = chkGT_KI_B_8.Checked
                .GT_KI_B_7 = chkGT_KI_B_7.Checked
                .GT_KI_B_6 = chkGT_KI_B_6.Checked
                .GT_KI_B_5 = chkGT_KI_B_5.Checked
                .GT_KI_B_4 = chkGT_KI_B_4.Checked
                .GT_KI_B_3 = chkGT_KI_B_3.Checked
                .GT_KI_B_2 = chkGT_KI_B_2.Checked
                .GT_KI_B_1 = chkGT_KI_B_1.Checked
                .GT_KA_A_1 = chkGT_KA_A_1.Checked
                .GT_KA_A_2 = chkGT_KA_A_2.Checked
                .GT_KA_A_3 = chkGT_KA_A_3.Checked
                .GT_KA_A_4 = chkGT_KA_A_4.Checked
                .GT_KA_A_5 = chkGT_KA_A_5.Checked
                .GT_KA_A_6 = chkGT_KA_A_6.Checked
                .GT_KA_A_7 = chkGT_KA_A_7.Checked
                .GT_KA_A_8 = chkGT_KA_A_8.Checked
                .GT_KA_B_1 = chkGT_KA_B_1.Checked
                .GT_KA_B_2 = chkGT_KA_B_2.Checked
                .GT_KA_B_3 = chkGT_KA_B_3.Checked
                .GT_KA_B_4 = chkGT_KA_B_4.Checked
                .GT_KA_B_5 = chkGT_KA_B_5.Checked
                .GT_KA_B_6 = chkGT_KA_B_6.Checked
                .GT_KA_B_7 = chkGT_KA_B_7.Checked
                .GT_KA_B_8 = chkGT_KA_B_8.Checked

                .M_JENIS_MAKAN1 = txtM_JENIS_MAKAN1.Text
                .M_JENIS_MAKAN2 = txtM_JENIS_MAKAN2.Text
                .M_JENIS_MAKAN3 = txtM_JENIS_MAKAN3.Text
                .M_JENIS_MAKAN4 = txtM_JENIS_MAKAN4.Text
                .M_JENIS_MAKAN5 = txtM_JENIS_MAKAN5.Text
                .M_KUANTITAS1 = txtM_KUANTITAS1.Text
                .M_KUANTITAS2 = txtM_KUANTITAS2.Text
                .M_KUANTITAS3 = txtM_KUANTITAS3.Text
                .M_KUANTITAS4 = txtM_KUANTITAS4.Text
                .M_KUANTITAS5 = txtM_KUANTITAS5.Text
                .M_KUALITAS1 = txtM_KUALITAS1.Text
                .M_KUALITAS2 = txtM_KUALITAS2.Text
                .M_KUALITAS3 = txtM_KUALITAS3.Text
                .M_KUALITAS4 = txtM_KUALITAS4.Text
                .M_KUALITAS5 = txtM_KUALITAS5.Text

                .PENYAKIT_CAMPAK = chkPENYAKIT_CAMPAK.Checked
                .PENYAKIT_BATUK = chkPENYAKIT_BATUK.Checked
                .PENYAKIT_TBC = chkPENYAKIT_TBC.Checked
                .PENYAKIT_DIFTERI = chkPENYAKIT_DIFTERI.Checked
                .PENYAKIT_TETANUS = chkPENYAKIT_TETANUS.Checked
                .PENYAKIT_DIARE = chkPENYAKIT_DIARE.Checked
                .PENYAKIT_DEMAM = chkPENYAKIT_DEMAM.Checked
                .PENYAKIT_KUNING = chkPENYAKIT_KUNING.Checked
                .PENYAKIT_CACING = chkPENYAKIT_CACING.Checked
                .PENYAKIT_KEJANG = chkPENYAKIT_KEJANG.Checked
                .PENYAKIT_BENGEK = chkPENYAKIT_BENGEK.Checked
                .PENYAKIT_EKSIM = chkPENYAKIT_EKSIM.Checked
                .PENYAKIT_KALIGATA = chkPENYAKIT_KALIGATA.Checked
                .PENYAKIT_TENGGOROKAN = chkPENYAKIT_TENGGOROKAN.Checked

                .PENYAKIT_LAIN = txtPENYAKIT_LAIN.Text
                .PENGUKURAN = txtPENGUKURAN.Text
                .P_UMUR = txtP_UMUR.Text
                .P_BERATBADAN = txtP_BERATBADAN.Text
                .P_TINGGI_BADAN = txtP_TINGGI_BADAN.Text
                .P_LINGKARKEPALA = txtP_LINGKARKEPALA.Text
                .P_LINGKARDADA = txtP_LINGKARDADA.Text
                .P_LENGAN_ATAS = txtP_LENGAN_ATAS.Text
                .P_STATUSGIZI_BBU = txtP_STATUSGIZI_BBU.Text
                .P_STATUSGIZI_TBU = txtP_STATUSGIZI_TBU.Text
                .P_STATUSGIZI_BBTB = txtP_STATUSGIZI_BBTB.Text
                .P_STATUSGIZI_BMIU = txtP_STATUSGIZI_BMIU.Text
                .TV = txtTV.Text
                .TV_LAJUNAFAS = txtTV_LAJUNAFAS.Text
                .TV_LAJUNAFAS_TIPE = txtTV_LAJUNAFAS_TIPE.Text
                .TV_TEKANAN_DARAH = txtTV_TEKANAN_DARAH.Text

                .TV_TEKANAN_DARAH_SISTOLIK = chkTV_TEKANAN_DARAH_SISTOLIK.Checked
                .TV_TEKANAN_DARAH_DIASTOLIK = chkTV_TEKANAN_DARAH_DIASTOLIK.Checked

                .TV_SUHU = txtTV_SUHU.Text
                .TV_LAJUNADI = txtTV_LAJUNADI.Text
                .TV_LAJUNADI_KUALITAS = txtTV_LAJUNADI_KUALITAS.Text

                .TV_LAJUNADI_REGULAR = chkTV_LAJUNADI_REGULAR.Checked
                .TV_LAJUNADI_IREGULER = chkTV_LAJUNADI_IREGULER.Checked

                .TV_LAJUNADI_REGULER_IREGULER_TXT = txtTV_LAJUNADI_REGULER_IREGULER_TXT.Text
                .TV_LAJUNADI_ISI = txtTV_LAJUNADI_ISI.Text
                .K_U = txtK_U.Text

                .KU_KEADAAN_SAKIT_TIDAK = chkKU_KEADAAN_SAKIT_TIDAK.Checked
                .KU_KEADAAN_SAKIT_RINGAN = chkKU_KEADAAN_SAKIT_RINGAN.Checked
                .KU_KEADAAN_SAKIT_SEDANG = chkKU_KEADAAN_SAKIT_SEDANG.Checked
                .KU_KEADAAN_SAKIT_BERAT = chkKU_KEADAAN_SAKIT_BERAT.Checked

                .KU_KESADARAN_KUANTITATIF = txtKU_KESADARAN_KUANTITATIF.Text
                .KU_KESADARAN_KUANTITATIF_E = txtKU_KESADARAN_KUANTITATIF_E.Text
                .KU_KESADARAN_KUANTITATIF_V = txtKU_KESADARAN_KUANTITATIF_V.Text
                .KU_KESADARAN_KUANTITATIF_M = txtKU_KESADARAN_KUANTITATIF_M.Text
                .KU_KESADARAN_KUALITATIF = txtKU_KESADARAN_KUALITATIF.Text


                .KU_SESAK_P = chkKU_SESAK_P.Checked
                .KU_SESAK_M = chkKU_SESAK_M.Checked

                .KU_SESAK_PCH = txtKU_SESAK_PCH.Text
                .KU_SESAK_RETRAKSI = txtKU_SESAK_RETRAKSI.Text
                .KU_SESAK_RETRAKSI2 = txtKU_SESAK_RETRAKSI2.Text

                .KU_SIANOSIS_S = chkKU_SIANOSIS_S.Checked
                .KU_SIANOSIS_P = chkKU_SIANOSIS_P.Checked

                .KU_SIANOSIS_S_P_TXT = txtKU_SIANOSIS_S_P_TXT.Text
                .KU_IKTERUS_KRAMER = txtKU_IKTERUS_KRAMER.Text
                .KU_IKTERUS_ANAK = txtKU_IKTERUS_ANAK.Text

                .KU_EDEMA = txtKU_EDEMA.Text
                .KU_EDEMA_ANASARKA = txtKU_EDEMA_ANASARKA.Text
                .KU_DEHIDRASI = txtKU_DEHIDRASI.Text
                .KU_DEHIDRASI_R = chkKU_DEHIDRASI_R.Checked
                .KU_DEHIDRASI_S = chkKU_DEHIDRASI_S.Checked
                .KU_DEHIDRASI_BERAT = txtKU_DEHIDRASI_BERAT.Text
                .KU_DEHIDRASI_WHO = txtKU_DEHIDRASI_WHO.Text
                .KU_ANEMI = txtKU_ANEMI.Text
                .KU_KEJANG = txtKU_KEJANG.Text
                .KU_KEJANG_L = chkKU_KEJANG_L.Checked
                .KU_KEJANG_U = chkKU_KEJANG_U.Checked
                .KU_KEJANG_TK = txtKU_KEJANG_TK.Text
                .KU_KEJANG_T = chkKU_KEJANG_T.Checked
                .KU_KEJANG_K = chkKU_KEJANG_K.Checked
                .KU_LETAK_PAKSA = txtKU_LETAK_PAKSA.Text
                .PK = txtPK.Text
                .PK_RAMBUT = txtPK_RAMBUT.Text
                .PK_KUKU = txtPK_KUKU.Text
                .PK_KULIT = txtPK_KULIT.Text
                .PK_GETAH_BENING = txtPK_GETAH_BENING.Text
                .PK_KEPALA = txtPK_KEPALA.Text
                .PK_MATA = txtPK_MATA.Text
                .PK_UBUN = txtPK_UBUN.Text
                .PK_PUPIL = txtPK_PUPIL.Text
                .PK_THT_TELINGA = txtPK_THT_TELINGA.Text
                .PK_THT_HIDUNG = txtPK_THT_HIDUNG.Text
                .PK_THT_TENGGOROKAN = txtPK_THT_TENGGOROKAN.Text
                .PK_THT_TONSIL = txtPK_THT_TONSIL.Text
                .PK_THT_FARINGS = txtPK_THT_FARINGS.Text
                .PK_BIBIR = txtPK_BIBIR.Text
                .PK_MULUT = txtPK_MULUT.Text
                .PK_GUSI = txtPK_GUSI.Text
                .PK_GIGI = txtPK_GIGI.Text
                .PK_LANGIT = txtPK_LANGIT.Text
                .PK_LIDAH = txtPK_LIDAH.Text
                .PK_LEHER = txtPK_LEHER.Text
                .PK_TEKANAN_VENA = txtPK_TEKANAN_VENA.Text
                .PK_KAKU_KUDUK = txtPK_KAKU_KUDUK.Text
                .PK_KELENJAR_GETAH_BENING = txtPK_KELENJAR_GETAH_BENING.Text
                .PK_LAIN1 = txtPK_LAIN1.Text
                .PK_LAIN2 = txtPK_LAIN2.Text

                '.DINDING_DADA_PARU_IMG = ""
                Try
                    Dim ms As New IO.MemoryStream()
                    PicGambar1.Image.Save(ms, PicGambar1.Image.RawFormat)

                    Dim data As Byte() = ms.GetBuffer()

                    .DINDING_DADA_PARU_IMG = data
                Catch oErr As Exception
                    Try
                        .DINDING_DADA_PARU_IMG = oS_DIDIGITAL_ASMEDANAK.GetData(sNoid).DINDING_DADA_PARU_IMG
                    Catch ex As Exception

                    End Try
                End Try
                .DINDING_DADA_PARU_INSPEKSI = txtDINDING_DADA_PARU_INSPEKSI.Text
                .DINDING_DADA_PARU_PALPASI = txtDINDING_DADA_PARU_PALPASI.Text
                .DINDING_DADA_PARU_PERKUSI = txtDINDING_DADA_PARU_PERKUSI.Text
                .DINDING_DADA_PARU_AUSKULTASI = txtDINDING_DADA_PARU_AUSKULTASI.Text
                '.JANTUNG_IMG = ""
                Try
                    Dim ms As New IO.MemoryStream()
                    PicGambar2.Image.Save(ms, PicGambar2.Image.RawFormat)

                    Dim data As Byte() = ms.GetBuffer()

                    .JANTUNG_IMG = data
                Catch oErr As Exception
                    Try
                        .JANTUNG_IMG = oS_DIDIGITAL_ASMEDANAK.GetData(sNoid).JANTUNG_IMG
                    Catch ex As Exception

                    End Try
                End Try
                .JANTUNG_INSPEKSI = txtJANTUNG_INSPEKSI.Text
                .JANTUNG_PALPASI = txtJANTUNG_PALPASI.Text
                .JANTUNG_PERKUSI = txtJANTUNG_PERKUSI.Text
                .JANTUNG_AUSKULTASI = txtJANTUNG_AUSKULTASI.Text
                '.PERUT_IMG = ""
                Try
                    Dim ms As New IO.MemoryStream()
                    PicGambar3.Image.Save(ms, PicGambar3.Image.RawFormat)

                    Dim data As Byte() = ms.GetBuffer()

                    .PERUT_IMG = data
                Catch oErr As Exception
                    Try
                        .PERUT_IMG = oS_DIDIGITAL_ASMEDANAK.GetData(sNoid).PERUT_IMG
                    Catch ex As Exception

                    End Try
                End Try
                .PERUT_INSPEKSI = txtPERUT_INSPEKSI.Text
                .PERUT_PALPASI = txtPERUT_PALPASI.Text
                .PERUT_PALPASI_HEPAR = txtPERUT_PALPASI_HEPAR.Text
                .PERUT_PALPASI_LIEN = txtPERUT_PALPASI_LIEN.Text
                .PERUT_PALPASI_GINJAL = txtPERUT_PALPASI_GINJAL.Text
                .PERUT_PERKUSI = txtPERUT_PERKUSI.Text
                .PERUT_AUSKULTASI = txtPERUT_AUSKULTASI.Text
                .GENITALIA = txtGENITALIA.Text
                .KELAINAN = txtKELAINAN.Text
                .MATURITAS_SEKS = txtMATURITAS_SEKS.Text
                .ANGGOTA_GERAK = txtANGGOTA_GERAK.Text
                .ANGGOTA_GERAK_ATAS = txtANGGOTA_GERAK_ATAS.Text
                .ANGGOTA_GERAK_ATAS_SENDI = txtANGGOTA_GERAK_ATAS_SENDI.Text
                .ANGGOTA_GERAK_ATAS_OTOT = txtANGGOTA_GERAK_ATAS_OTOT.Text
                .ANGGOTA_GERAK_BAWAH = txtANGGOTA_GERAK_BAWAH.Text
                .ANGGOTA_GERAK_BAWAH_SENDI = txtANGGOTA_GERAK_BAWAH_SENDI.Text
                .ANGGOTA_GERAK_BAWAH_OTOT = txtANGGOTA_GERAK_BAWAH_OTOT.Text
                .SUSUNAN_SARAF = txtSUSUNAN_SARAF.Text
                .SUSUNAN_SARAF_R_C_P_TXT = txtSUSUNAN_SARAF_R_C_P_TXT.Text

                .SUSUNAN_SARAF_R_CAHAYA = chkSUSUNAN_SARAF_R_CAHAYA.Checked
                .SUSUNAN_SARAF_R_PUPIL = chkSUSUNAN_SARAF_R_PUPIL.Checked
                .SUSUNAN_SARAF_R_OKULOSEFALIK = txtSUSUNAN_SARAF_R_OKULOSEFALIK.Text
                .SUSUNAN_SARAF_R_KORNEA = txtSUSUNAN_SARAF_R_KORNEA.Text
                .KAKU_KUDUK = txtKAKU_KUDUK.Text
                .BRUDZKINSKY_I = chkBRUDZKINSKY_I.Checked
                .BRUDZKINSKY_II = chkBRUDZKINSKY_II.Checked
                .BRUDZKINSKY_III = chkBRUDZKINSKY_III.Checked
                .BRUDZKINSKY_TXT = txtBRUDZKINSKY_TXT.Text
                .KERNIG = txtKERNIG.Text
                .LASEQUE = txtLASEQUE.Text
                .SARAF_OTAK = txtSARAF_OTAK.Text
                .MOTORIK = txtMOTORIK.Text
                .SENSORIK = txtSENSORIK.Text
                .VEGETATIF = txtVEGETATIF.Text
                .APR = txtAPR.Text
                .KRP = txtKRP.Text
                .BABNISKY = txtBABNISKY.Text
                .CHADDOCK = txtCHADDOCK.Text
                .GORDON = txtGORDON.Text
                .OPPENHEIM = txtOPPENHEIM.Text
                .PEMERIKSAAN_PENUNJANG = mmPEMERIKSAAN_PENUNJANG.Text
                .DIAGNOSIS_BANDING = mmDIAGNOSIS_BANDING.Text
                .DIAGNOSIS_KERJA = mmDIAGNOSIS_KERJA.Text
                .PENGOBATAN_DAN_TINDAKAN = mmPENGOBATAN_DAN_TINDAKAN.Text
                .REKONSILIASI_OBAT = mmREKONSILIASI_OBAT.Text
                .DISCHARGE_PLANNING = mmDISCHARGE_PLANNING.Text


                '.DOKTER_KODE = sKODEDOKTER
                '.DOKTER_NAMEDISPLAY = sNAMADOKTER
                '.SDIGITALRJ36_1 = txt1.Text
                '.SDIGITALRJ36_2 = txt2.Text
                '.SDIGITALRJ36_3 = txt3.Text
                '.SDIGITALRJ36_4 = txt4.Text
                '.SDIGITALRJ36_5 = txt5.Text
                '.SDIGITALRJ36_6 = txt6.Text
                '.SDIGITALRJ36_7 = txt7.Text
                '.SDIGITALRJ36_8 = txt8.Text
                '.SDIGITALRJ36_9 = txt9.Text
                '.SDIGITALRJ36_10 = txtNYERI_AKUT_L.Text
                '.SDIGITALRJ36_11 = txtINTENSITAS_A.Text
                '.SDIGITALRJ36_12 = txtINTENSITAS_I_K.Text
                '.SDIGITALRJ36_13 = txtR_I_BCG_DASAR.Text
                '.SDIGITALRJ36_14 = chkPENYAKIT_TETANUS.Checked
                '.SDIGITALRJ36_15 = txtNYERI_KRONIS_L.Text
                '.SDIGITALRJ36_16 = chkNYERI_KRONIS.Checked
                '.SDIGITALRJ36_17 = txtR_I_DASAR_NAMA6.Text
                '.SDIGITALRJ36_18 = chkNYERI_AKUT.Checked
                '.SDIGITALRJ36_19 = txtINTENSITAS_A_A.Text
                '.SDIGITALRJ36_20 = chkPENYAKIT_TBC.Checked
                '.SDIGITALRJ36_21 = chkPENYAKIT_DIFTERI.Checked
                '.SDIGITALRJ36_22 = chkGS_KI_B_IV.Checked
                '.SDIGITALRJ36_23 = chkPENYAKIT_DIARE.Checked
                '.SDIGITALRJ36_24 = txtINTENSITAS_I_A.Text
                Try
                    Dim ms As New IO.MemoryStream()
                    PicGambar1.Image.Save(ms, PicGambar1.Image.RawFormat)

                    Dim data As Byte() = ms.GetBuffer()

                    .DINDING_DADA_PARU_IMG = data
                Catch oErr As Exception
                    Try
                        .DINDING_DADA_PARU_IMG = oS_DIDIGITAL_ASMEDANAK.GetData(sNoid).DINDING_DADA_PARU_IMG
                    Catch ex As Exception

                    End Try
                End Try
                '.SDIGITALRJ36_26 = chkGS_KI_B_II.Checked
                '.SDIGITALRJ36_27 = chkGS_KI_B_III.Checked
                '.SDIGITALRJ36_28 = chkGS_KI_B_I.Checked
                '.SDIGITALRJ36_29 = txtR_I_POLIO_DASAR.Text
                '.SDIGITALRJ36_30 = txtR_I_DPT_DASAR.Text
                '.SDIGITALRJ36_31 = chkGS_KA_A_III.Checked
                '.SDIGITALRJ36_32 = txtR_I_HEPATITIS_DASAR.Text
                '.SDIGITALRJ36_33 = chkGS_KA_A_II.Checked
                '.SDIGITALRJ36_34 = txtR_I_CAMPAK_DASAR.Text
                '.SDIGITALRJ36_35 = chkGS_KA_A_I.Checked
                '.SDIGITALRJ36_36 = txtR_I_DASAR_NAMA1.Text
                '.SDIGITALRJ36_37 = MemoEdit4.Text
                '.SDIGITALRJ36_38 = MemoEdit2.Text
                '.SDIGITALRJ36_39 = MemoEdit3.Text
                '.SDIGITALRJ36_40 = MemoEdit5.Text
                '.SDIGITALRJ36_41 = MemoEdit6.Text

                '.DOKTER_KODE = grdDOCTOR.EditValue
                '.DOKTER_NAMEDISPLAY = grdDOCTOR.Text
                Try
                    .CETAK = oS_DIDIGITAL_ASMEDANAK.GetData(sNoid).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                .KDUSER = sUserID
                .KDUSER_SIGNATURE = grdDOCTOR.EditValue
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIDIGITAL_ASMEDANAK.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIDIGITAL_ASMEDANAK.UpdateData(ds)
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
            Case Keys.F5
                If btnReload.Enabled = True Then
                    btnReload_Click()
                End If
            Case Keys.F6
                If btnReload.Enabled = True Then
                    btnReload_Click()
                End If
            Case Keys.PageUp
                fn_ScrollPage(True)
            Case Keys.PageDown
                fn_ScrollPage(False)
        End Select
    End Sub
    Private Sub btnDiagnosa_Click() Handles btnDiagnosa.ItemClick
        'Dim oMasterDiagnosa As New Diagnosa.clsMasterDiagnosa
        'Dim dsMasterDiagnosa = oMasterDiagnosa.GetData(sNoid)
        'If dsMasterDiagnosa IsNot Nothing Then
        '    Dim frmMasterDiagnosa As New frmMasterDiagnosa
        '    frmMasterDiagnosa.LoadMe(FORM_MODE.FORM_MODE_EDIT, sNoid)
        '    frmMasterDiagnosa.ShowDialog(Me)
        'Else
        '    Dim frmMasterDiagnosa As New frmMasterDiagnosa
        '    frmMasterDiagnosa.LoadMe(FORM_MODE.FORM_MODE_ADD, sNoid)
        '    frmMasterDiagnosa.ShowDialog(Me)
        'End If

        'If sCode = "Berhasil" Then
        '    Dim listDiagnosa As New List(Of String)
        '    For Each xloop In oMasterDiagnosa.GetDataDetail(sNoid)
        '        listDiagnosa.Add(xloop.KATEGORI & " : " & xloop.REMARKS)
        '    Next

        '    Dim listProsedur As New List(Of String)
        '    For Each xloop In oMasterDiagnosa.GetDataDetail_(sNoid)
        '        listProsedur.Add(xloop.SEQ + 1 & ". " & xloop.REMARKS)
        '    Next
        '    'MemoEdit4.Text = String.Join(vbCrLf, listProsedur.ToArray)
        '    'MemoEdit2.Text = String.Join(vbCrLf, listDiagnosa.ToArray)
        'End If
    End Sub
    Private Sub btnReload_Click() Handles btnReload.ItemClick
        'Dim dsKunjungan = oS_DIDIGITAL_ASMEDANAK.GetDataByKunjungan(sNoid)

        'Dim listPenunjang As New List(Of String)
        'Dim listTindakanPengobatan As New List(Of String)

        'If dsKunjungan IsNot Nothing Then
        '    Dim oOrderTindakan As New Inventory.clsOrderTindakan
        '    Dim oOrderLab As New Inventory.clsOrderLab
        '    Dim oOrderRad As New Inventory.clsOrderRad
        '    Dim oKonsul As New Digital.clsKonsul
        '    Dim oKonsulJawab As New Digital.clsJawabKonsul

        '    Dim dsTindakan = From x In oOrderTindakan.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
        '                     Join y In oOrderTindakan.GetDataDetail()
        '                     On x.KDORDERTINDAKAN Equals y.KDORDERTINDAKAN
        '                     Select TANGGAL = x.DATE, ITEM = "Tindakan : " & y.TARIFKT & " " & y.REMARKS & ", "

        '    Dim dsLab = From x In oOrderLab.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
        '                Join y In oOrderLab.GetDataDetail()
        '                On x.KDORDERLAB Equals y.KDORDERLAB
        '                Select TANGGAL = x.DATE, ITEM = "Lab : " & y.TARIFKT & " " & y.REMARKS & ", "

        '    Dim dsRad = From x In oOrderRad.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
        '                Join y In oOrderRad.GetDataDetail()
        '                On x.KDORDERRAD Equals y.KDORDERRAD
        '                Select TANGGAL = x.DATE, ITEM = "Rad : " & y.TARIFKT & " " & y.REMARKS & ", "

        '    Dim dsKonsul = From x In oKonsul.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
        '                   Select TANGGAL = x.DATE, ITEM = "Konsul : " & "Dari " & x.DOKTER_DARI & " Kepada " & x.DOKTER_KEPADA & " Isi " & x.MEMO & ", "

        '    Dim dsJawabKonsul = From x In oKonsulJawab.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
        '                        Select TANGGAL = x.DATE, ITEM = "Jawab Konsul : " & "Dari " & x.DOKTER_DARI & " Kepada " & x.DOKTER_KEPADA & " Isi " & x.MEMO & ", "

        '    Dim dsUnionPenunjang = dsLab.Union(dsRad)

        '    For Each xloop In dsUnionPenunjang.OrderBy(Function(x) x.TANGGAL)
        '        listPenunjang.Add("*) " & xloop.TANGGAL.ToString("dd-MM-yyy HH:mm:ss") & " " & xloop.ITEM)
        '    Next

        '    Dim dsUnionTindakanKonsul = dsTindakan.Union(dsKonsul).Union(dsJawabKonsul)

        '    For Each xloop In dsUnionTindakanKonsul.OrderBy(Function(x) x.TANGGAL)
        '        listTindakanPengobatan.Add("*) " & xloop.TANGGAL.ToString("dd-MM-yyy HH:mm:ss") & " " & xloop.ITEM)
        '    Next

        '    Dim oResep As New Inventory.clsOrderResep

        '    Dim dsResep = oResep.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)

        '    For Each xloop In dsResep
        '        For Each yloop In oResep.GetDataDetail(xloop.KDORDERRESEP)
        '            listTindakanPengobatan.Add(yloop.TARIFKT & " " & yloop.SIGNA)
        '        Next
        '    Next

        '    'MemoEdit7.Text = String.Join(", ", listPenunjang.ToArray)
        '    ''MemoEdit3.Text = String.Join(", ", listTindakanPengobatan.ToArray)
        'End If
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & sNoid.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & sNoid.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub

#End Region
#Region "Lookup / Event"
    Private Sub fn_DOCTOR()
        Dim oDPJP As New Reference.clsDoctor
        Try
            grdDOCTOR.Properties.DataSource = oDPJP.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    'Private Sub fn_NOIDUSER()
    'Dim oUser As New Setting.clsUser
    'Try
    '    grdNOIDUSER.Properties.DataSource = oUser.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
    '    grdNOIDUSER.Properties.ValueMember = "NOIDUSER"
    '    grdNOIDUSER.Properties.DisplayMember = "NOIDUSER"
    'Catch oErr As Exception
    '    MsgBox("Load Sub Spesialis Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    'End Try
    'End Sub
    'Private Sub fn_DOCTOR()
    '    'Dim oDOCTOR As New Master.clsDoctor
    '    'Try
    '    '    grdDOCTOR.Properties.DataSource = oDOCTOR.GetData.Where(Function(x) x.ISACTIVE = True And x.ADDRESS_COUNTRY <> "").ToList()
    '    '    grdDOCTOR.Properties.ValueMember = "KDDOCTOR"
    '    '    grdDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"
    '    'Catch oErr As Exception
    '    '    MsgBox("Load Dokter DPJP Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    'End Try
    'End Sub
    'Private Sub fn_DEPARTMENT()
    '    'Dim oDEPARTMENT As New Master.clsDepartment
    '    'Try
    '    '    grdDEPARTMENT.Properties.DataSource = oDEPARTMENT.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
    '    '    grdDEPARTMENT.Properties.ValueMember = "KDDEPARTMENT"
    '    '    grdDEPARTMENT.Properties.DisplayMember = "NAME_DISPLAY"
    '    'Catch oErr As Exception
    '    '    MsgBox("Load Department Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    'End Try
    'End Sub

    Private Sub btnEDITIMAGE_Click(sender As Object, e As EventArgs) Handles btnEDITIMAGE.Click ', btnEDITIMAGE2.Click
        Dim frmPopUp_Image As New frmPopUp_AsmedAnak
        frmPopUp_Image.fn_LoadMe(PicGambar1.Image)
        frmPopUp_Image.ShowDialog()

        If sPicture IsNot Nothing Then
            PicGambar1.Image = sPicture
        End If

        PicGambar1.Focus()
        sFind10 = String.Empty
        sPicture = Nothing
    End Sub
    Private Sub btnEDITIMAGE2_Click(sender As Object, e As EventArgs) Handles btnEDITIMAGE2.Click ', btnEDITIMAGE3.Click , btnEDITIMAGE2.Click
        Dim frmPopUp_Image As New frmPopUp_AsmedAnak
        frmPopUp_Image.fn_LoadMe(PicGambar2.Image)
        frmPopUp_Image.ShowDialog()

        If sPicture IsNot Nothing Then
            PicGambar2.Image = sPicture
        End If

        PicGambar2.Focus()
        sFind10 = String.Empty
        sPicture = Nothing
    End Sub
    Private Sub btnEDITIMAGE3_Click(sender As Object, e As EventArgs) Handles btnEDITIMAGE3.Click ', btnEDITIMAGE3.Click , btnEDITIMAGE2.Click
        Dim frmPopUp_Image As New frmPopUp_AsmedAnak
        frmPopUp_Image.fn_LoadMe(PicGambar3.Image)
        frmPopUp_Image.ShowDialog()

        If sPicture IsNot Nothing Then
            PicGambar3.Image = sPicture
        End If

        PicGambar3.Focus()
        sFind10 = String.Empty
        sPicture = Nothing
    End Sub

    Private Sub frmEMedrek_AsmedAnak_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
	    If e.Delta > 0 Then
		    'up
		    fn_ScrollPage(True)
	    Else
		    'down
		    fn_ScrollPage(False)
	    End If
    End Sub

    Private Sub fn_ScrollPage(ByVal isUp As Boolean)
        Dim myView As Point = Me.Panel1.AutoScrollPosition
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

        Me.Panel1.AutoScrollPosition = myView
    End Sub
#End Region
End Class