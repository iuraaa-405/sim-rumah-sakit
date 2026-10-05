Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmAsesmenAwalMedisIGD2
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oDigitalIGD As New Transaksi.clsDigital_IGD_01
    Private down As Boolean = False
    Private sNoid As String = String.Empty
    Private sKDDOCTOR As String = String.Empty
    Private sKDCUSTOMER As String = String.Empty
    Private sNAMAPASIEN As String = String.Empty
    Private sKDKUNJUNGAN As String = String.Empty
    Private sTANGGALLAHIR As DateTime = Now
    Private listTindakanLab_LoadData As New List(Of String)
    Private listTindakanRad_LoadData As New List(Of String)
    Private oGrouperDataCppt As New Grouper.clsR_CPPT
    Private sKelas As String = String.Empty
    Private sPenjmain As String = String.Empty
    Private sKDIDENTIAS As Integer = 0
    Private sPenunjnag As String = String.Empty

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal OrderObat As Boolean, ByVal Kode As String, ByVal KDIDENTIAS As String, ByVal KDREG As String, ByVal kdkunjungan As String, ByVal KDDOCTOR As String, ByVal KDCUSTOMER As String, ByVal NAMAPASIEN As String, ByVal TANGGALLAHIR As DateTime)
        chkAutoOrderObat.Checked = OrderObat
        sNoid = Kode
        sKDDOCTOR = KDDOCTOR
        sKDCUSTOMER = KDCUSTOMER
        sNAMAPASIEN = NAMAPASIEN
        txtNoRegister.Text = KDREG
        sKDKUNJUNGAN = kdkunjungan
        sTANGGALLAHIR = TANGGALLAHIR
        sKDIDENTIAS = KDIDENTIAS

        If KDREG = "" Then
            MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien !!!", MsgBoxStyle.Exclamation, Me.Text)
            Me.Close()
        End If
        If KDREG = "Nul" Then
            MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien !!!", MsgBoxStyle.Exclamation, Me.Text)
            Me.Close()
        End If

        Dim ds = oDigitalIGD.GetDataByKodeIGD(sNoid)

        If ds Is Nothing Then
            oFormMode = FORM_MODE.FORM_MODE_ADD
        Else
            oFormMode = FORM_MODE.FORM_MODE_EDIT
        End If

        If chkAutoOrderObat.Checked = False Then
            lOrderResep.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            lOrderResep.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "Asesmen Awal Medis IGD"

            btnSaveClosee.Caption = "Simpan"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.Collect()
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_ChangeFormState()
        'fn_NOIDUSER()
        fn_LoadKDDOCTOR()
        fn_LoadITEM()
        fn_LoadUOM()
        fn_LoadSIGNA()
        fn_LoadCARAPAKAI()
        fn_ReoladResep()
        fn_LoadTEMPLATE()
        fn_LoadKDUSER()
        fn_LoadDiagnosa()
        fn_LoadKDITEMSearch()
        LoadPenunjang(txtNoRegister.Text)

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

        Dim oDaftar As New Admission.clsPendaftaran

        Dim dsDaftar = oDaftar.GetData(txtNoRegister.Text)
        If dsDaftar IsNot Nothing Then
            sKelas = dsDaftar.KDKELASRAWAT
            sPenjmain = dsDaftar.M_DAFTAR_L1.MEMO
        End If
    End Sub
    Private Sub fn_ViewMode(ByVal Status As Boolean)
        sPicture = Nothing

        btnSaveNew.Enabled = Not Status
        btnSaveClose.Enabled = Not Status
        'btnDiagnosa.Enabled = Not Status
        'btnReload.Enabled = Not Status

        deDATE.Properties.ReadOnly = Status
        chkTRIAGE_TRAUMA.Properties.ReadOnly = Status
        chkTRIAGE_NONTRAUMA.Properties.ReadOnly = Status
        chkTRIAGE_MATERNITY.Properties.ReadOnly = Status
        chkTRIAGE_RED.Properties.ReadOnly = Status
        chkTRIAGE_YELLOW.Properties.ReadOnly = Status
        chkTRIAGE_GREEN.Properties.ReadOnly = Status
        chkTRIAGE_BLACK.Properties.ReadOnly = Status
        'chkTRIAGE_AIRWAY.Properties.ReadOnly = Status
        'chkTRIAGE_BREATHING.Properties.ReadOnly = Status
        'chkTRIAGE_CIRCULATION.Properties.ReadOnly = Status
        cboTRIAGE_AIRWAY.Properties.ReadOnly = Status
        cboTRIAGE_BREATHING.Properties.ReadOnly = Status
        cboTRIAGE_CIRCULATION.Properties.ReadOnly = Status
        chkTRIAGE_DATANGSENDIRI.Properties.ReadOnly = Status
        chkTRIAGE_DIANTAR.Properties.ReadOnly = Status
        cboTRIAGE_DIANTAR.Properties.ReadOnly = Status
        chkAUTOANAMNESA.Properties.ReadOnly = Status
        chkALLOANAMNESA.Properties.ReadOnly = Status
        deDATEPUKULPERIKSA.Properties.ReadOnly = Status
        deDATEPUKULRAWAT.Properties.ReadOnly = Status
        grdPETUGASTRIAGE.Properties.ReadOnly = Status
        txtRIWAYAT.Properties.ReadOnly = Status
        txtRIWAYAT_ALERGI.Properties.ReadOnly = Status
        txtRIWAYAT_PENYAKITDAHULU.Properties.ReadOnly = Status
        cboTINGKATKESADARAN.Properties.ReadOnly = Status
        cboE.Properties.ReadOnly = Status
        cboM.Properties.ReadOnly = Status
        cboV.Properties.ReadOnly = Status
        txtKEADAANUMUM.Properties.ReadOnly = Status
        txtBERATBADAN.Properties.ReadOnly = Status
        txtTINGGIBADAN.Properties.ReadOnly = Status
        txtBP.Properties.ReadOnly = Status
        txtHR.Properties.ReadOnly = Status
        txtRR.Properties.ReadOnly = Status
        txtT.Properties.ReadOnly = Status
        txtSPO2.Properties.ReadOnly = Status
        chkRESIKO_1.Properties.ReadOnly = Status
        chkRESIKO_2.Properties.ReadOnly = Status
        chkFUNGSIONAL_1.Properties.ReadOnly = Status
        chkFUNGSIONAL_2.Properties.ReadOnly = Status
        chkFUNGSIONAL_3.Properties.ReadOnly = Status
        txtFUNGSIONAL_2.Properties.ReadOnly = Status
        txtFUNGSIONAL_3.Properties.ReadOnly = Status
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
        chkNORMAL_KEPALA.Properties.ReadOnly = Status
        chkTIDAK_NORMAL_KEPALA.Properties.ReadOnly = Status
        txtTidakNormalKepala.Properties.ReadOnly = Status
        chkNORMAL_MATA.Properties.ReadOnly = Status
        chkTIDAK_NORMAL_MATA.Properties.ReadOnly = Status
        txtTidakNormalMata.Properties.ReadOnly = Status
        chkNORMAL_LEHER.Properties.ReadOnly = Status
        chkTIDAK_NORMAL_LEHER.Properties.ReadOnly = Status
        txtTidakNormalLeher.Properties.ReadOnly = Status
        chkNORMAL_DADA.Properties.ReadOnly = Status
        chkTIDAK_NORMAL_DADA.Properties.ReadOnly = Status
        txtTidakNormalDada.Properties.ReadOnly = Status
        chkNORMAL_PERUT.Properties.ReadOnly = Status
        chkTIDAK_NORMAL_PERUT.Properties.ReadOnly = Status
        txtTidakNormalPerut.Properties.ReadOnly = Status
        chkNORMAL_ALATGERAK.Properties.ReadOnly = Status
        chkTIDAK_NORMAL_ALATGERAK.Properties.ReadOnly = Status
        txtTidakNormalAlatGerak.Properties.ReadOnly = Status
        txtTINDAKLANJUT.ReadOnly = Status

        grdKDDOCTOR.Properties.ReadOnly = Status
        txtKDDIAGNOSA.Properties.ReadOnly = Status
        chkKESIMPULAN_PERBAIKAN.Properties.ReadOnly = Status
        chkKESIMPULAN_STABIL.Properties.ReadOnly = Status
        chkKESIMPULAN_PERBURUKAN.Properties.ReadOnly = Status
        'chkTINDAKLANJUT_RUJUK.Properties.ReadOnly = True
        'chkTINDAKLANJUT_RAWAT.Properties.ReadOnly = True
        'chkTINDAKLANJUT_PULANGPAKSA.Properties.ReadOnly = True
        'chkTINDAKLANJUT_PULANG.Properties.ReadOnly = True
        txtLOKASI.Properties.ReadOnly = Status
        txtSAATPASIENPULANG_TEXT.Properties.ReadOnly = Status
        'deDATEPUKUL.Properties.ReadOnly = Status
        cboSAATPULANG_TINGKATKESDARAN.Properties.ReadOnly = Status
        cboSAATPULANG_E.Properties.ReadOnly = Status
        cboSAATPULANG_M.Properties.ReadOnly = Status
        cboSAATPULANG_V.Properties.ReadOnly = Status
        txtSAATPULANG_BP.Properties.ReadOnly = Status
        txtSAATPULANG_HR.Properties.ReadOnly = Status
        txtSAATPULANG_RR.Properties.ReadOnly = Status
        txtSAATPULANG_SPO2.Properties.ReadOnly = Status
        txtSAATPULANG_T.Properties.ReadOnly = Status
        txtINSTRUKSILANJUTAN.Properties.ReadOnly = Status
        txtLOKASI.Properties.ReadOnly = Status
        txtWAKTU.Properties.ReadOnly = Status
        'txtDOKTER_.Properties.ReadOnly = Status
        'txtOBATSAATPULANG.Properties.ReadOnly = True
        txtPERAWAT.Properties.ReadOnly = Status
        txtSURVEY_ALATGERAK_1.Properties.ReadOnly = Status
        CheckEdit1.Properties.ReadOnly = Status
        CheckEdit2.Properties.ReadOnly = Status
        CheckEdit3.Properties.ReadOnly = Status
        CheckEdit4.Properties.ReadOnly = Status
        CheckEdit5.Properties.ReadOnly = Status
        CheckEdit6.Properties.ReadOnly = Status
        txtSURVEY_MATA_1.Properties.ReadOnly = Status
        txtKELUHANUTAMA.Properties.ReadOnly = Status

        'If oFormMode = FORM_MODE.FORM_MODE_ADD Then
        '    lWAKTU.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        'Else
        '    lWAKTU.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        'End If
    End Sub
    Private Sub fn_EmptyMe()
        txtCODE.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        chkTRIAGE_TRAUMA.Checked = False
        chkTRIAGE_NONTRAUMA.Checked = False
        chkTRIAGE_MATERNITY.Checked = False
        chkTRIAGE_RED.Checked = False
        chkTRIAGE_YELLOW.Checked = False
        chkTRIAGE_GREEN.Checked = False
        chkTRIAGE_BLACK.Checked = False
        'chkTRIAGE_AIRWAY.Checked = False
        'chkTRIAGE_BREATHING.Checked = False
        'chkTRIAGE_CIRCULATION.Checked = False
        cboTRIAGE_AIRWAY.ResetText()
        cboTRIAGE_BREATHING.ResetText()
        cboTRIAGE_CIRCULATION.ResetText()
        chkTRIAGE_DATANGSENDIRI.Checked = False
        chkTRIAGE_DIANTAR.Checked = False
        cboTRIAGE_DIANTAR.ResetText()
        chkAUTOANAMNESA.Checked = False
        chkALLOANAMNESA.Checked = False
        deDATEPUKULPERIKSA.Time = Now
        deDATEPUKULRAWAT.Time = Now
        grdPETUGASTRIAGE.ResetText()
        txtRIWAYAT.ResetText()
        txtRIWAYAT_ALERGI.ResetText()
        txtRIWAYAT_PENYAKITDAHULU.ResetText()
        cboTINGKATKESADARAN.ResetText()
        txtGCS.ResetText()
        cboE.ResetText()
        cboM.ResetText()
        cboV.ResetText()
        txtKEADAANUMUM.ResetText()
        txtBERATBADAN.ResetText()
        txtTINGGIBADAN.ResetText()
        txtBP.ResetText()
        txtHR.ResetText()
        txtRR.ResetText()
        txtT.ResetText()
        txtSPO2.ResetText()
        chkRESIKO_1.Checked = False
        chkRESIKO_2.Checked = False
        chkFUNGSIONAL_1.Checked = False
        chkFUNGSIONAL_2.Checked = False
        chkFUNGSIONAL_3.Checked = False
        txtFUNGSIONAL_2.ResetText()
        txtFUNGSIONAL_3.ResetText()
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
        chkNORMAL_KEPALA.Checked = True
        chkTIDAK_NORMAL_KEPALA.Checked = False
        txtTidakNormalKepala.ResetText()
        chkNORMAL_MATA.Checked = True
        chkTIDAK_NORMAL_MATA.Checked = False
        txtTidakNormalMata.ResetText()
        chkNORMAL_LEHER.Checked = True
        chkTIDAK_NORMAL_LEHER.Checked = False
        txtTidakNormalLeher.ResetText()
        chkNORMAL_DADA.Checked = True
        chkTIDAK_NORMAL_DADA.Checked = False
        txtTidakNormalDada.ResetText()
        chkNORMAL_PERUT.Checked = True
        chkTIDAK_NORMAL_PERUT.Checked = False
        txtTidakNormalPerut.ResetText()
        chkNORMAL_ALATGERAK.Checked = True
        chkTIDAK_NORMAL_ALATGERAK.Checked = False
        txtTidakNormalAlatGerak.ResetText()
        'txtTINDAKLANJUT.ResetText()
        txtSURVEY_ALATGERAK_1.ResetText()

        grdKDDOCTOR.ResetText()
        txtKDDIAGNOSA.ResetText()
        chkKESIMPULAN_PERBAIKAN.Checked = False
        chkKESIMPULAN_STABIL.Checked = False
        chkKESIMPULAN_PERBURUKAN.Checked = False
        'chkTINDAKLANJUT_RUJUK.Checked = False
        'chkTINDAKLANJUT_RAWAT.Checked = False
        'chkTINDAKLANJUT_PULANGPAKSA.Checked = False
        'chkTINDAKLANJUT_PULANG.Checked = False
        txtLOKASI.ResetText()
        txtSAATPASIENPULANG_TEXT.ResetText()
        'deDATEPUKUL.DateTime = Now
        cboSAATPULANG_TINGKATKESDARAN.ResetText()
        txtSAATPULANG_GCS.ResetText()
        cboSAATPULANG_E.ResetText()
        cboSAATPULANG_M.ResetText()
        cboSAATPULANG_V.ResetText()
        txtSAATPULANG_BP.ResetText()
        txtSAATPULANG_HR.ResetText()
        txtSAATPULANG_RR.ResetText()
        txtSAATPULANG_SPO2.ResetText()
        txtSAATPULANG_T.ResetText()
        txtINSTRUKSILANJUTAN.ResetText()
        txtLOKASI.ResetText()
        txtWAKTU.ResetText()
        'txtDOKTER_.ResetText()
        'txtOBATSAATPULANG.ResetText()
        txtPERAWAT.ResetText()
        txtTINDAKLANJUT.ResetText()

        grdKDDOCTOR.Text = sKDDOCTOR

        txtRELOAD.Reset()
        CheckEdit1.Checked = False
        CheckEdit2.Checked = False
        CheckEdit3.Checked = False
        CheckEdit4.Checked = False
        CheckEdit5.Checked = False
        CheckEdit6.Checked = False

        grdPETUGASTRIAGE.Text = sUserID

        txtSURVEY_MATA_1.ResetText()
        txtKELUHANUTAMA.ResetText()

        grvCPPT_Tindakan.OptionsSelection.MultiSelect = True
        grvCPPT_Tindakan.SelectAll()
        grvCPPT_Tindakan.DeleteSelectedRows()
        grvCPPT_Tindakan.OptionsSelection.MultiSelect = False

        grvDIAGNOSA_CPPT.OptionsSelection.MultiSelect = True
        grvDIAGNOSA_CPPT.SelectAll()
        grvDIAGNOSA_CPPT.DeleteSelectedRows()
        grvDIAGNOSA_CPPT.OptionsSelection.MultiSelect = False

        fn_LoadAdsemenPerawat()


    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oDigitalIGD.GetDataByKodeIGD(sNoid)

            With ds
                txtCODE.Text = sNoid
                deDATE.DateTime = .DATE
                chkTRIAGE_TRAUMA.Checked = .TRIAGE_TRAUMA
                chkTRIAGE_NONTRAUMA.Checked = .TRIAGE_NONTRAUMA
                chkTRIAGE_MATERNITY.Checked = .TRIAGE_MATERNITY
                chkTRIAGE_RED.Checked = .TRIAGE_RED
                chkTRIAGE_YELLOW.Checked = .TRIAGE_YELLOW
                chkTRIAGE_GREEN.Checked = .TRIAGE_GREEN
                chkTRIAGE_BLACK.Checked = .TRIAGE_BLACK
                'chkTRIAGE_AIRWAY.Checked = .TRIAGE_AIRWAY
                'chkTRIAGE_BREATHING.Checked = .TRIAGE_BREATHING
                'chkTRIAGE_CIRCULATION.Checked = .TRIAGE_CIRCULATION
                cboTRIAGE_AIRWAY.Text = .TRIAGE_AIRWAY_TEX
                cboTRIAGE_BREATHING.Text = .TRIAGE_BREATHING_TEXT
                cboTRIAGE_CIRCULATION.Text = .TRIAGE_CIRCULATION_TEXT
                chkTRIAGE_DATANGSENDIRI.Checked = .TRIAGE_DATANGSENDIRI
                chkTRIAGE_DIANTAR.Checked = .TRIAGE_DIANTAR
                cboTRIAGE_DIANTAR.Text = .TRIAGE_DIANTAR_TEXT
                chkAUTOANAMNESA.Checked = .AUTOANAMNESA
                chkALLOANAMNESA.Checked = .ALOANAMNESA
                deDATEPUKULPERIKSA.Time = .PUKULPERIKSA
                deDATEPUKULRAWAT.Time = .PUKULRAWAT
                grdPETUGASTRIAGE.Text = .PETUGASTRIAGE
                txtRIWAYAT.Text = .RIWAYAT
                txtRIWAYAT_ALERGI.Text = .RIWAYAT_ALERGI
                txtRIWAYAT_PENYAKITDAHULU.Text = .RIWAYAT_PENYAKITDAHULU
                cboTINGKATKESADARAN.Text = .TINGKATKESADARAN
                txtGCS.Text = .GCS
                cboE.Text = .E
                cboM.Text = .M
                cboV.Text = .V
                txtKEADAANUMUM.Text = .KEADAANUMUM
                txtBERATBADAN.Text = .BERATBADAN
                txtTINGGIBADAN.Text = .TINGGIBADAN
                txtBP.Text = .BP
                txtHR.Text = .HR
                txtRR.Text = .RR
                txtT.Text = .T
                txtSPO2.Text = .SP02
                chkRESIKO_1.Checked = .RESIKO_1
                chkRESIKO_2.Checked = .RESIKO_2
                chkFUNGSIONAL_1.Checked = .FUNGSIONAL_1
                chkFUNGSIONAL_2.Checked = .FUNGSIONAL_2
                chkFUNGSIONAL_3.Checked = .FUNGSIONAL_3
                txtFUNGSIONAL_2.Text = .FUNGSIONAL_2_TEXT
                txtFUNGSIONAL_3.Text = .FUNGSIONAL_3_TEXT
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
                chkNORMAL_KEPALA.Checked = .NORMAL_KEPALA
                chkTIDAK_NORMAL_KEPALA.Checked = .TIDAK_NORMAL_KEPALA
                txtTidakNormalKepala.Text = .SURVEY_KEPALA_2
                chkNORMAL_MATA.Checked = .NORMAL_MATA
                chkTIDAK_NORMAL_MATA.Checked = .TIDAK_NORMAL_MATA
                txtTidakNormalMata.Text = .SURVEY_MATA_2
                chkNORMAL_LEHER.Checked = .NORMAL_LEHER
                chkTIDAK_NORMAL_LEHER.Checked = .TIDAK_NORMAL_LEHER
                txtTidakNormalLeher.Text = .SURVEY_LEHER_2
                chkNORMAL_DADA.Checked = .NORMAL_DADA
                chkTIDAK_NORMAL_DADA.Checked = .TIDAK_NORMAL_DADA
                txtTidakNormalDada.Text = .SURVEY_DADA_2
                chkNORMAL_PERUT.Checked = .NORMAL_PERUT
                chkTIDAK_NORMAL_PERUT.Checked = .TIDAK_NORMAL_PERUT
                txtTidakNormalPerut.Text = .SURVEY_PERUT_2
                chkNORMAL_ALATGERAK.Checked = .NORMAL_ALATGERAK
                chkTIDAK_NORMAL_ALATGERAK.Checked = .TIDAK_NORMAL_ALATGERAK
                txtTidakNormalAlatGerak.Text = .SURVEY_ALATGERAK_2
                'txtTINDAKLANJUT_TUJUAN.Text = .TUJUAN
                txtSURVEY_ALATGERAK_1.Text = .SURVEY_ALATGERAK_1
                txtSURVEY_MATA_1.Text = .SURVEY_MATA_1
                txtKELUHANUTAMA.Text = .SURVEY_LEHER_1
                Try
                    'Dim img = (From x In oS_DIGITAL_IGD_01.GetData
                    '           Where x.KDREG = txtNoRegister.Text
                    '           Select x.ATTACHMENT_2).Single

                    Dim img = oDigitalIGD.GetDataByKodeIGD(sNoid).ATTACHMENT_1

                    picGAMBAR2.Image = ByteArrayToImage(img.ToArray())
                Catch oErr As Exception
                End Try

                Try
                    Dim ImagePath As String = .SURVEY_PERUT_1
                    Dim img1 As Bitmap
                    Dim newImage As Image = Image.FromFile(.SURVEY_PERUT_1)

                    img1 = New Bitmap(ImagePath)
                    picGAMBAR2.ImageLocation = ImagePath

                    picGAMBAR2.Image = newImage
                Catch ex As Exception
                    sPicture = Nothing
                    'MsgBox("Load List Data Gambar tidak ditemukan dialamat : " & .SIMPANGAMBAR_1, MsgBoxStyle.Exclamation, Me.Text)
                End Try

                grdKDDOCTOR.Text = .DOCTOR_KODE
                txtKDDIAGNOSA.Text = .KDDIAGNOSA
                chkKESIMPULAN_PERBAIKAN.Checked = .KESIMPULAN_PERBAIKAN
                chkKESIMPULAN_STABIL.Checked = .KESIMPULAN_STABIL
                chkKESIMPULAN_PERBURUKAN.Checked = .KESIMPULAN_PERBURUKAN
                'chkTINDAKLANJUT_RUJUK.Checked = .TINDAKLANJUT_RUJUK
                'chkTINDAKLANJUT_RAWAT.Checked = .TINDAKLANJUT_RAWAT
                'chkTINDAKLANJUT_PULANGPAKSA.Checked = .TINDAKLANJUT_PULANGPAKSA
                'chkTINDAKLANJUT_PULANG.Checked = .TINDAKLANJUT_PULANG
                txtLOKASI.Text = .TUJUAN
                txtSAATPASIENPULANG_TEXT.Text = .SAATPASIENPULANG_TEXT
                'deDATEPUKUL.DateTime = .SAATPASIENPULANG_TIME
                cboSAATPULANG_TINGKATKESDARAN.Text = .SAATPASIENPULANG_TINGKATKESADARAN
                txtSAATPULANG_GCS.Text = .SAATPASIENPULANG_GCS
                cboSAATPULANG_E.Text = .SAATPASIENPULANG_E
                cboSAATPULANG_M.Text = .SAATPASIENPULANG_M
                cboSAATPULANG_V.Text = .SAATPASIENPULANG_V
                txtSAATPULANG_BP.Text = .SAATPASIENPULANG_BP
                txtSAATPULANG_HR.Text = .SAATPASIENPULANG_HR
                txtSAATPULANG_RR.Text = .SAATPASIENPULANG_RR
                txtSAATPULANG_SPO2.Text = .SAATPASIENPULANG_SPO2
                txtSAATPULANG_T.Text = .SAATPASIENPULANG_T
                txtINSTRUKSILANJUTAN.Text = .INTRUKSILANJUTAN
                txtLOKASI.Text = .PERAWATANLANJUTAN_LOKASI
                txtWAKTU.Text = .PERAWATANLANJUTAN_WAKTU
                'txtDOKTER_.Text = .PERAWATANLANJUTAN_DOKTER
                'txtOBATSAATPULANG.Text = .OBATSAATPULANG
                txtPERAWAT.Text = .PERAWATPENANGUNGJAWAB
                txtTINDAKLANJUT.Text = .SURVEY_KEPALA_1

                txtRELOAD.Text = .KDUSER_SIGNATURE

                CheckEdit1.Checked = .KRITERIA_01
                CheckEdit2.Checked = .KRITERIA_02
                CheckEdit3.Checked = .KRITERIA_03
                CheckEdit4.Checked = .KRITERIA_04
                CheckEdit5.Checked = .KRITERIA_05
                CheckEdit6.Checked = .KRITERIA_06

                BindingSourceObatPulang.DataSource = oDigitalIGD.GetDataDetailResepObatPulang(txtCODE.Text).OrderBy(Function(x) x.SEQ).ToList()
                grdDetailResep.DataSource = BindingSourceObatPulang

                BindingSourceObatIGD.DataSource = oDigitalIGD.GetDataDetailResepSelamaIGD(txtCODE.Text).OrderBy(Function(x) x.SEQ).ToList()
                grdObatIGD.DataSource = BindingSourceObatIGD

                BindingSourceObatRanap.DataSource = oDigitalIGD.GetDataDetailResepObatRanap(txtCODE.Text).OrderBy(Function(x) x.SEQ).ToList()
                grdObatRanap.DataSource = BindingSourceObatRanap


                'BindingSourceTindakanPoli.DataSource = oS_DIGITAL_IGD_01.GetDataDetailTindakanPoli(txtCODE.Text).OrderBy(Function(x) x.SEQ).ToList()
                'grdTindakanPoli.DataSource = BindingSourceTindakanPoli

                'BindingSourcePenunjang.DataSource = oS_DIGITAL_IGD_01.GetDataDetailPenunjang(txtCODE.Text).OrderBy(Function(x) x.SEQ).ToList()
                'grdTindakan.DataSource = BindingSourcePenunjang

                BindingSourceDiagnosa.DataSource = oDigitalIGD.GetDataDetail(txtCODE.Text).OrderBy(Function(x) x.SEQ).ToList()
                grdDIAGNOSA_CPPT.DataSource = BindingSourceDiagnosa

                BindingSourceTindakan.DataSource = oDigitalIGD.GetDataDetailTindakan(txtCODE.Text).OrderBy(Function(x) x.SEQ).ToList()
                grdCPPT_Tindakan.DataSource = BindingSourceTindakan

                listTindakanLab_LoadData.Clear()
                listTindakanRad_LoadData.Clear()

                Dim oItem As New Reference.clsItem

                For Each xloop In oGrouperDataCppt.GetDataDetailTindakan(ds.KODE)
                    If xloop.ISPERAWAT = False Then
                        Dim dsItem = oItem.GetData(xloop.KDITEM)

                        If dsItem IsNot Nothing Then
                            If dsItem.M_ITEM_L3.MEMO = "LABORATORIUM" Then
                                listTindakanLab_LoadData.Add(dsItem.NMITEM2)
                            ElseIf dsItem.M_ITEM_L3.MEMO = "BANK DARAH" Then
                                listTindakanLab_LoadData.Add(dsItem.NMITEM2)
                            ElseIf dsItem.M_ITEM_L3.MEMO = "RADIOLOGI"
                                listTindakanRad_LoadData.Add(dsItem.NMITEM2)
                            End If
                        End If
                    End If
                Next

            End With

            'Dim oReq_Recipe As New Transaksi.clsReq_Recipe
            'Dim dsResep = oReq_Recipe.GetData("IGD_" & txtNoRegister.Text)
            'If dsResep IsNot Nothing Then
            '    txtGRANDTOTAL.Text = dsResep.GRANDTOTAL
            'End If
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try

            fn_Validate = True
            If txtNoRegister.Text = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End If
            Dim sTriage As Boolean = False

            If chkTRIAGE_RED.Checked = True Then
                sTriage = True
            End If
            If chkTRIAGE_YELLOW.Checked = True Then
                sTriage = True
            End If
            If chkTRIAGE_GREEN.Checked = True Then
                sTriage = True
            End If
            If chkTRIAGE_BLACK.Checked = True Then
                sTriage = True
            End If
            If sTriage = False Then
                MsgBox("Dibutuhkan Trigae", MsgBoxStyle.Exclamation, Me.Text)
                chkTRIAGE_RED.Focus()
                fn_Validate = False
                Exit Function
            End If
            Dim Kriteria As Boolean = False

            Dim ABC As Boolean = False

            If cboTRIAGE_AIRWAY.Text = "-" Or cboTRIAGE_AIRWAY.Text = "" Then
            Else
                ABC = True
            End If

            If cboTRIAGE_BREATHING.Text = "-" Or cboTRIAGE_BREATHING.Text = "" Then
            Else
                ABC = True
            End If

            If cboTRIAGE_CIRCULATION.Text = "-" Or cboTRIAGE_CIRCULATION.Text = "" Then
            Else
                ABC = True
            End If

            If ABC = False Then
                MsgBox("Dibutuhkan Airway/Breathing/Circulation", MsgBoxStyle.Exclamation, Me.Text)
                cboTRIAGE_AIRWAY.Focus()
                fn_Validate = False
                Exit Function
            End If

            If CheckEdit1.Checked = True Then
                Kriteria = True
            End If
            If CheckEdit2.Checked = True Then
                Kriteria = True
            End If
            If CheckEdit3.Checked = True Then
                Kriteria = True
            End If
            If CheckEdit4.Checked = True Then
                Kriteria = True
            End If
            If CheckEdit5.Checked = True Then
                Kriteria = True
            End If
            If CheckEdit6.Checked = True Then
                Kriteria = True
            End If

            If Kriteria = False Then
                MsgBox("Dibutuhkan Kriteria", MsgBoxStyle.Exclamation, Me.Text)
                CheckEdit1.Focus()
                fn_Validate = False
                Exit Function
            End If

            If grdPETUGASTRIAGE.Text = String.Empty Then
                MsgBox("Dibutuhkan Petugas Triage", MsgBoxStyle.Exclamation, Me.Text)
                grdPETUGASTRIAGE.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtPERAWAT.Text = String.Empty Then
                If grdPETUGASTRIAGE.Text = String.Empty Then
                    MsgBox("Dibutuhkan Perawat", MsgBoxStyle.Exclamation, Me.Text)
                    txtPERAWAT.Focus()
                    fn_Validate = False
                    Exit Function
                Else
                    txtPERAWAT.Text = grdPETUGASTRIAGE.Text
                End If
            End If
            If grdKDDOCTOR.Text = String.Empty Then
                MsgBox("Dibutuhkan Dokter", MsgBoxStyle.Exclamation, Me.Text)
                grdKDDOCTOR.Focus()
                fn_Validate = False
                Exit Function
            End If
            'If txtRIWAYAT.Text = String.Empty Then
            '    MsgBox("Dibutuhkan Riwayat", MsgBoxStyle.Exclamation, Me.Text)
            '    txtRIWAYAT.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If
            If txtRIWAYAT_PENYAKITDAHULU.Text = String.Empty Then
                MsgBox("Dibutuhkan Anamnesis", MsgBoxStyle.Exclamation, Me.Text)
                txtRIWAYAT_PENYAKITDAHULU.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtBP.Text = String.Empty Then
                MsgBox("Dibutuhkan Tensi", MsgBoxStyle.Exclamation, Me.Text)
                txtBP.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtHR.Text = String.Empty Then
                MsgBox("Dibutuhkan Nadi", MsgBoxStyle.Exclamation, Me.Text)
                txtHR.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtRR.Text = String.Empty Then
                MsgBox("Dibutuhkan Respirasi", MsgBoxStyle.Exclamation, Me.Text)
                txtRR.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtT.Text = String.Empty Then
                MsgBox("Dibutuhkan Suhu", MsgBoxStyle.Exclamation, Me.Text)
                txtT.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtSPO2.Text = String.Empty Then
                MsgBox("Dibutuhkan SPO2", MsgBoxStyle.Exclamation, Me.Text)
                txtSPO2.Focus()
                fn_Validate = False
                Exit Function
            End If
            'If txtKDDIAGNOSA.Text = String.Empty Then
            '    MsgBox("Dibutuhkan Diagnosa", MsgBoxStyle.Exclamation, Me.Text)
            '    txtKDDIAGNOSA.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If
            ''If txtTINDAKLANJUT.Text = String.Empty Then
            ''    MsgBox("Dibutuhkan Tindak Lanjut", MsgBoxStyle.Exclamation, Me.Text)
            ''    txtTINDAKLANJUT.Focus()
            ''    fn_Validate = False
            ''    Exit Function
            ''End If

            'If txtKDDIAGNOSA.Text = String.Empty Then
            '    MsgBox("Dibutuhkan Diagnosa", MsgBoxStyle.Exclamation, Me.Text)
            '    txtKDDIAGNOSA.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If

            Dim listdiagnosa As New List(Of String)

            For i As Integer = 0 To grvDIAGNOSA_CPPT.RowCount - 2
                listdiagnosa.Add(IIf(grvDIAGNOSA_CPPT.GetRowCellValue(i, colKDDIAGNOSA_CPPT) = "", "", grvDIAGNOSA_CPPT.GetRowCellValue(i, colKDDIAGNOSA_CPPT) & "-") & grvDIAGNOSA_CPPT.GetRowCellValue(i, colNAMADIAGNOSA_CPPT))
            Next

            txtKDDIAGNOSA.Text = String.Join(", ", listdiagnosa.ToArray)

            If txtKDDIAGNOSA.Text = String.Empty Then
                MsgBox("Dibutuhkan Diagnosa", MsgBoxStyle.Exclamation, Me.Text)
                grdDIAGNOSA_CPPT.Focus()
                fn_Validate = False
                Exit Function
            End If

            If sHargaApotik = False Then
                Dim oPendaftaran As New Admission.clsPendaftaran

                Dim dsPendaftaran = oPendaftaran.GetData(txtNoRegister.Text)

                If dsPendaftaran.M_DAFTAR_L1.MEMO = "BPJS" Then
                    Dim oItem As New Reference.clsItem
                    Dim dsItemTindkan = oItem.GetData("ITEM_0000000058")
                    If dsItemTindkan IsNot Nothing Then
                        If dsItemTindkan.ISSTOK = False Then
                            Dim CekItem = oDigitalIGD.GetDataDetailTindakanByItem(txtNoRegister.Text, "ITEM_0000000058")

                            If CekItem Is Nothing Then
                                Dim tes As Boolean = False

                                For i As Integer = 0 To grvCPPT_Tindakan.RowCount - 2
                                    If grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_KDITEMTINDAKAN) = "ITEM_0000000058" Then
                                        tes = True
                                    End If
                                Next

                                If tes = False Then
                                    Dim dsItem = oItem.GetData("ITEM_0000000058")
                                    If dsItem IsNot Nothing Then
                                        grvCPPT_Tindakan.AddNewRow()
                                        grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN, dsItem.KDITEM)
                                        grvCPPT_Tindakan.UpdateCurrentRow()
                                    Else
                                        Dim dsItemCPPTAuto = oItem.GetDataByAutodiCPPT()
                                        If dsItemCPPTAuto IsNot Nothing Then
                                            grvCPPT_Tindakan.AddNewRow()
                                            grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN, dsItemCPPTAuto.KDITEM)
                                            grvCPPT_Tindakan.UpdateCurrentRow()
                                        End If
                                    End If
                                End If
                            End If
                        End If

                    End If
                End If
            End If
        Catch oErr As Exception
            MsgBox("Validate Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            Dim listObatPulang As New List(Of String)
            Dim listObatIGD As New List(Of String)
            Dim listObatRanap As New List(Of String)
            Dim oItem As New Reference.clsItem
            Dim oUom As New Reference.clsUOM
            Dim oSigna As New Reference.clsSigna
            Dim oCaraPakai As New Reference.clsCaraPakai

            For i As Integer = 0 To grvDetailResep.RowCount - 2
                Dim Obat As String = String.Empty
                Dim Uom As String = String.Empty
                Dim Signa As String = String.Empty
                Dim CaraPakai As String = String.Empty

                Dim dsObat = oItem.GetData(grvDetailResep.GetRowCellValue(i, colKDITEM))
                If dsObat IsNot Nothing Then
                    Obat = dsObat.NMITEM2
                End If

                Dim dsUom = oUom.GetData(grvDetailResep.GetRowCellValue(i, colKDUOM))
                If dsUom IsNot Nothing Then
                    Uom = dsUom.MEMO
                End If

                Dim dsSigna = oSigna.GetData(grvDetailResep.GetRowCellValue(i, colKDSIGNA))
                If dsSigna IsNot Nothing Then
                    Signa = dsSigna.MEMO
                End If

                Dim dsCaraPakai = oCaraPakai.GetData(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI))
                If dsCaraPakai IsNot Nothing Then
                    CaraPakai = dsCaraPakai.MEMO
                End If

                listObatPulang.Add(Obat & " No " & IntegerToRoman(CInt(grvDetailResep.GetRowCellValue(i, colQTY))) & " " & Uom & " " & IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDSIGNA)), "", Signa) & " " & IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)), "", CaraPakai) & " " & IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER)), "", grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER)))
            Next
            For i As Integer = 0 To grvObatIGD.RowCount - 2
                Dim Obat As String = String.Empty
                Dim Uom As String = String.Empty
                Dim Signa As String = String.Empty
                Dim CaraPakai As String = String.Empty

                Dim dsObat = oItem.GetData(grvObatIGD.GetRowCellValue(i, colKDITEM_ObatIGD))
                If dsObat IsNot Nothing Then
                    Obat = dsObat.NMITEM2
                End If

                Dim dsUom = oUom.GetData(grvObatIGD.GetRowCellValue(i, colKDUOM_ObatIGD))
                If dsUom IsNot Nothing Then
                    Uom = dsUom.MEMO
                End If

                Dim dsSigna = oSigna.GetData(grvObatIGD.GetRowCellValue(i, colKDSIGNA_ObatIGD))
                If dsSigna IsNot Nothing Then
                    Signa = dsSigna.MEMO
                End If

                Dim dsCaraPakai = oCaraPakai.GetData(grvObatIGD.GetRowCellValue(i, colKDCARAPAKAI_ObatIGD))
                If dsCaraPakai IsNot Nothing Then
                    CaraPakai = dsCaraPakai.MEMO
                End If

                listObatIGD.Add(Obat & " No " & IntegerToRoman(CInt(grvObatIGD.GetRowCellValue(i, colQTY_ObatIGD))) & " " & Uom & " " & IIf(String.IsNullOrEmpty(grvObatIGD.GetRowCellValue(i, colKDSIGNA_ObatIGD)), "", Signa) & " " & IIf(String.IsNullOrEmpty(grvObatIGD.GetRowCellValue(i, colKDCARAPAKAI_ObatIGD)), "", CaraPakai) & " " & IIf(String.IsNullOrEmpty(grvObatIGD.GetRowCellValue(i, colREMARKS_DOKTER_ObatIGD)), "", grvObatIGD.GetRowCellValue(i, colREMARKS_DOKTER_ObatIGD)))
            Next
            For i As Integer = 0 To grvObatRanap.RowCount - 2
                Dim Obat As String = String.Empty
                Dim Uom As String = String.Empty
                Dim Signa As String = String.Empty
                Dim CaraPakai As String = String.Empty

                Dim dsObat = oItem.GetData(grvObatRanap.GetRowCellValue(i, colKDITEM_ObatRanap))
                If dsObat IsNot Nothing Then
                    Obat = dsObat.NMITEM2
                End If

                Dim dsUom = oUom.GetData(grvObatRanap.GetRowCellValue(i, colKDUOM_ObatRanap))
                If dsUom IsNot Nothing Then
                    Uom = dsUom.MEMO
                End If

                Dim dsSigna = oSigna.GetData(grvObatRanap.GetRowCellValue(i, colKDSIGNA_ObatRanap))
                If dsSigna IsNot Nothing Then
                    Signa = dsSigna.MEMO
                End If

                Dim dsCaraPakai = oCaraPakai.GetData(grvObatRanap.GetRowCellValue(i, colKDCARAPAKAI_ObatRanap))
                If dsCaraPakai IsNot Nothing Then
                    CaraPakai = dsCaraPakai.MEMO
                End If

                listObatRanap.Add(Obat & " No " & IntegerToRoman(CInt(grvObatRanap.GetRowCellValue(i, colQTY_ObatRanap))) & " " & Uom & " " & IIf(String.IsNullOrEmpty(grvObatRanap.GetRowCellValue(i, colKDSIGNA_ObatRanap)), "", Signa) & " " & IIf(String.IsNullOrEmpty(grvObatRanap.GetRowCellValue(i, colKDCARAPAKAI_ObatRanap)), "", CaraPakai) & " " & IIf(String.IsNullOrEmpty(grvObatRanap.GetRowCellValue(i, colREMARKS_DOKTER_ObatRanap)), "", grvObatRanap.GetRowCellValue(i, colREMARKS_DOKTER_ObatRanap)))
            Next

            ' ***** HEADER *****
            Dim ds = oDigitalIGD.GetStructureHeader
            With ds
                .KODE = sNoid
                .KDCUSTOMER = sKDCUSTOMER
                .DATE = deDATE.DateTime
                .KDPENDAFTARAN = txtNoRegister.Text
                '.KDCUSTOMER = sKDCUSTOMER
                '.NAMAPASIEN = sNAMAPASIEN
                '.JENISKELAMIN = txtJENISKELAMIN.Text
                '.TANGGALLAHIR = deDATETANGGALLAHIR.DateTime.ToString("dd-MM-yyyy")
                '.ALAMAT = sALAMAT
                Try
                    .DATECREATED = oDigitalIGD.GetDataByKodeIGD(sNoid).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .TRIAGE_TRAUMA = chkTRIAGE_TRAUMA.Checked
                .TRIAGE_NONTRAUMA = chkTRIAGE_NONTRAUMA.Checked
                .TRIAGE_MATERNITY = chkTRIAGE_MATERNITY.Checked
                .TRIAGE_RED = chkTRIAGE_RED.Checked
                .TRIAGE_YELLOW = chkTRIAGE_YELLOW.Checked
                .TRIAGE_GREEN = chkTRIAGE_GREEN.Checked
                .TRIAGE_BLACK = chkTRIAGE_BLACK.Checked
                .TRIAGE_AIRWAY = False
                .TRIAGE_BREATHING = False
                .TRIAGE_CIRCULATION = False
                .TRIAGE_AIRWAY_TEX = cboTRIAGE_AIRWAY.Text
                .TRIAGE_BREATHING_TEXT = cboTRIAGE_BREATHING.Text
                .TRIAGE_CIRCULATION_TEXT = cboTRIAGE_CIRCULATION.Text
                .TRIAGE_DATANGSENDIRI = chkTRIAGE_DATANGSENDIRI.Checked
                .TRIAGE_DIANTAR = chkTRIAGE_DIANTAR.Checked
                .TRIAGE_DIANTAR_TEXT = cboTRIAGE_DIANTAR.Text
                .AUTOANAMNESA = chkAUTOANAMNESA.Checked
                .ALOANAMNESA = chkALLOANAMNESA.Checked
                .PUKULPERIKSA = deDATEPUKULPERIKSA.Time
                .PUKULRAWAT = deDATEPUKULRAWAT.Time
                .PETUGASTRIAGE = grdPETUGASTRIAGE.EditValue
                .RIWAYAT = txtRIWAYAT.Text
                .RIWAYAT_ALERGI = txtRIWAYAT_ALERGI.Text
                .RIWAYAT_PENYAKITDAHULU = txtRIWAYAT_PENYAKITDAHULU.Text
                .TINGKATKESADARAN = cboTINGKATKESADARAN.Text
                .GCS = txtGCS.Text
                .E = cboE.Text
                .M = cboM.Text
                .V = cboV.Text
                .KEADAANUMUM = txtKEADAANUMUM.Text
                .BERATBADAN = txtBERATBADAN.Text
                .TINGGIBADAN = txtTINGGIBADAN.Text
                .BP = txtBP.Text
                .HR = txtHR.Text
                .RR = txtRR.Text
                .T = txtT.Text
                .SP02 = txtSPO2.Text
                .RESIKO_1 = chkRESIKO_1.Checked
                .RESIKO_2 = chkRESIKO_2.Checked
                .FUNGSIONAL_1 = chkFUNGSIONAL_1.Checked
                .FUNGSIONAL_2 = chkFUNGSIONAL_2.Checked
                .FUNGSIONAL_3 = chkFUNGSIONAL_3.Checked
                .FUNGSIONAL_2_TEXT = txtFUNGSIONAL_2.Text
                .FUNGSIONAL_3_TEXT = txtFUNGSIONAL_3.Text
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
                .SURVEY_KEPALA_1 = txtTINDAKLANJUT.Text
                .NORMAL_KEPALA = chkNORMAL_KEPALA.Checked
                .TIDAK_NORMAL_KEPALA = chkTIDAK_NORMAL_KEPALA.Checked
                .SURVEY_KEPALA_2 = txtTidakNormalKepala.Text
                .SURVEY_MATA_1 = txtSURVEY_MATA_1.Text
                .NORMAL_MATA = chkNORMAL_MATA.Checked
                .TIDAK_NORMAL_MATA = chkTIDAK_NORMAL_MATA.Checked
                .SURVEY_MATA_2 = txtTidakNormalMata.Text
                .SURVEY_LEHER_1 = txtKELUHANUTAMA.Text
                .NORMAL_LEHER = chkNORMAL_LEHER.Checked
                .TIDAK_NORMAL_LEHER = chkTIDAK_NORMAL_LEHER.Checked
                .SURVEY_LEHER_2 = txtTidakNormalLeher.Text
                .SURVEY_DADA_1 = ""
                .NORMAL_DADA = chkNORMAL_DADA.Checked
                .TIDAK_NORMAL_DADA = chkTIDAK_NORMAL_DADA.Checked
                .SURVEY_DADA_2 = txtTidakNormalDada.Text

                If sPicture Is Nothing Then
                    Try
                        .SURVEY_PERUT_1 = oDigitalIGD.GetDataByKodeIGD(sNoid).SURVEY_PERUT_1
                    Catch ex As Exception
                        .SURVEY_PERUT_1 = ""
                    End Try
                Else
                    'Dim oSetKoneksi As New Brigging.clsSetKoneksi
                    'Dim sALAMATSIMPAN As String = String.Empty
                    'Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "SIMPANPDFCASMIX")
                    'If dsDataSetKoneksi IsNot Nothing Then
                    '    sALAMATSIMPAN = dsDataSetKoneksi.ALAMATWEB

                    '    If Not IO.Directory.Exists(sALAMATSIMPAN) Then
                    '        .SURVEY_PERUT_1 = ""
                    '    Else
                    '        Dim Alamat As String = sALAMATSIMPAN & Now.ToString("ddMMyyyyHHmm") & "." & sNoid & ".PNG"
                    '        picGAMBAR2.Image.Save(Alamat, System.Drawing.Imaging.ImageFormat.Jpeg)
                    '        .SURVEY_PERUT_1 = Alamat
                    '    End If
                    'Else
                    '    .SURVEY_PERUT_1 = ""
                    'End If
                    .SURVEY_PERUT_1 = ""
                    MsgBox("Belum Ada Almat Simpan Gambar")
                End If

                .NORMAL_PERUT = chkNORMAL_PERUT.Checked
                .TIDAK_NORMAL_PERUT = chkTIDAK_NORMAL_PERUT.Checked
                .SURVEY_PERUT_2 = txtTidakNormalPerut.Text
                .SURVEY_ALATGERAK_1 = txtSURVEY_ALATGERAK_1.Text
                .NORMAL_ALATGERAK = chkNORMAL_ALATGERAK.Checked
                .TIDAK_NORMAL_ALATGERAK = chkTIDAK_NORMAL_ALATGERAK.Checked
                .SURVEY_ALATGERAK_2 = txtTidakNormalAlatGerak.Text

                Try
                    Dim ms As New IO.MemoryStream()
                    picGAMBAR2.Image.Save(ms, picGAMBAR2.Image.RawFormat)

                    Dim data As Byte() = ms.GetBuffer()

                    .ATTACHMENT_2 = data
                Catch oErr As Exception
                    Try
                        .ATTACHMENT_2 = oDigitalIGD.GetDataByKodeIGD(sNoid).ATTACHMENT_2
                    Catch ex As Exception

                    End Try
                End Try

                .DOCTOR_KODE = grdKDDOCTOR.EditValue
                .DOCTOR_NAME_DISPLAY = grdKDDOCTOR.Text
                .KDDIAGNOSA = txtKDDIAGNOSA.Text
                .KESIMPULAN_PERBAIKAN = chkKESIMPULAN_PERBAIKAN.Checked
                .KESIMPULAN_STABIL = chkKESIMPULAN_STABIL.Checked
                .KESIMPULAN_PERBURUKAN = chkKESIMPULAN_PERBURUKAN.Checked
                .TINDAKLANJUT_RUJUK = False
                .TINDAKLANJUT_RAWAT = False
                .TINDAKLANJUT_PULANGPAKSA = False
                .TINDAKLANJUT_PULANG = False
                .TUJUAN = ""
                .SAATPASIENPULANG_TEXT = txtSAATPASIENPULANG_TEXT.Text
                '.SAATPASIENPULANG_TIME = deDATEPUKUL.DateTime
                If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                    .SAATPASIENPULANG_TIME = Now
                Else
                    .SAATPASIENPULANG_TIME = Now
                End If

                .SAATPASIENPULANG_TINGKATKESADARAN = cboSAATPULANG_TINGKATKESDARAN.Text
                .SAATPASIENPULANG_GCS = txtSAATPULANG_GCS.Text
                .SAATPASIENPULANG_E = cboSAATPULANG_E.Text
                .SAATPASIENPULANG_M = cboSAATPULANG_M.Text
                .SAATPASIENPULANG_V = cboSAATPULANG_V.Text
                .SAATPASIENPULANG_BP = txtSAATPULANG_BP.Text
                .SAATPASIENPULANG_HR = txtSAATPULANG_HR.Text
                .SAATPASIENPULANG_RR = txtSAATPULANG_RR.Text
                .SAATPASIENPULANG_SPO2 = txtSAATPULANG_SPO2.Text
                .SAATPASIENPULANG_T = txtSAATPULANG_T.Text
                .INTRUKSILANJUTAN = txtINSTRUKSILANJUTAN.Text
                .PERAWATANLANJUTAN_LOKASI = txtLOKASI.Text
                .PERAWATANLANJUTAN_WAKTU = txtWAKTU.Text
                .PERAWATANLANJUTAN_DOKTER = ""

                Try
                    Dim ObatPulang As String = ""
                    Dim ObatIGD As String = ""
                    Dim Obatranap As String = ""

                    If listObatIGD.Count > 0 Then
                        ObatIGD = "OBAT SELAMA IGD :" & vbCrLf & String.Join(", ", listObatIGD.ToArray)
                    End If
                    If listObatRanap.Count > 0 Then
                        Obatranap = "OBAT RANAP :" & vbCrLf & String.Join(", ", listObatRanap.ToArray)
                    End If
                    If listObatPulang.Count > 0 Then
                        ObatPulang = "OBAT PULANG :" & vbCrLf & String.Join(", ", listObatPulang.ToArray)
                    End If

                    .OBATSAATPULANG = ObatIGD & vbCrLf & Obatranap & vbCrLf & ObatPulang
                Catch ex As Exception
                    .OBATSAATPULANG = ""
                End Try
                '.OBATSAATPULANG = txtOBATSAATPULANG.Text

                Try
                    .CETAK = oDigitalIGD.GetDataByKodeIGD(sNoid).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try
                .PERAWATPENANGUNGJAWAB = txtPERAWAT.Text
                .KDUSER = sUserID
                .KDUSER_SIGNATURE = txtRELOAD.Text
                .KRITERIA_01 = CheckEdit1.Checked
                .KRITERIA_02 = CheckEdit2.Checked
                .KRITERIA_03 = CheckEdit3.Checked
                .KRITERIA_04 = CheckEdit4.Checked
                .KRITERIA_05 = CheckEdit5.Checked
                .KRITERIA_06 = CheckEdit6.Checked
                Try
                    .ISDELETE = oDigitalIGD.GetDataByKodeIGD(sNoid).ISDELETE
                Catch ex As Exception
                    .ISDELETE = False
                End Try
                Try
                    .CATATAN = oDigitalIGD.GetDataByKodeIGD(sNoid).CATATAN
                Catch ex As Exception
                    .CATATAN = ""
                End Try

            End With

            Dim arrDetail = oDigitalIGD.GetStructureDetailObatList

            Dim i_ObatIGD As Integer = 0
            For i As Integer = 0 To grvObatIGD.RowCount - 2
                Dim dsDetail = oDigitalIGD.GetStructureDetailObat
                With dsDetail
                    Dim Obat As String = String.Empty
                    Dim Uom As String = String.Empty
                    Dim Signa As String = String.Empty
                    Dim CaraPakai As String = String.Empty

                    Dim dsObat = oItem.GetData(grvObatIGD.GetRowCellValue(i, colKDITEM_ObatIGD))
                    If dsObat IsNot Nothing Then
                        Obat = dsObat.NMITEM2
                    End If

                    Dim dsUom = oUom.GetData(grvObatIGD.GetRowCellValue(i, colKDUOM_ObatIGD))
                    If dsUom IsNot Nothing Then
                        Uom = dsUom.MEMO
                    End If

                    Dim dsSigna = oSigna.GetData(grvObatIGD.GetRowCellValue(i, colKDSIGNA_ObatIGD))
                    If dsSigna IsNot Nothing Then
                        Signa = dsSigna.MEMO
                    End If

                    Dim dsCaraPakai = oCaraPakai.GetData(grvObatIGD.GetRowCellValue(i, colKDCARAPAKAI_ObatIGD))
                    If dsCaraPakai IsNot Nothing Then
                        CaraPakai = dsCaraPakai.MEMO
                    End If

                    .SEQ = i_ObatIGD
                    .KODE = ds.KODE
                    .NAMAOBAT = Obat
                    .SATUAN = Uom
                    .SIGNA = IIf(String.IsNullOrEmpty(grvObatIGD.GetRowCellValue(i, colKDSIGNA_ObatIGD)), "", Signa)
                    .CARAPAKAI = IIf(String.IsNullOrEmpty(grvObatIGD.GetRowCellValue(i, colKDCARAPAKAI_ObatIGD)), "", CaraPakai)
                    .QTY = CDec(grvObatIGD.GetRowCellValue(i, colQTY_ObatIGD))
                    .PRICE = CDec(grvObatIGD.GetRowCellValue(i, colPRICE_ObatIGD))
                    .GRANDTOTAL = CDec(grvObatIGD.GetRowCellValue(i, colGRANDTOTAL_ObatIGD))
                    .ROMAWI = IntegerToRoman(CInt(grvObatIGD.GetFocusedRowCellValue(colQTY_ObatIGD)))
                    .REMARKS_DOKTER = IIf(String.IsNullOrEmpty(grvObatIGD.GetRowCellValue(i, colREMARKS_DOKTER_ObatIGD)), "", grvObatIGD.GetRowCellValue(i, colREMARKS_DOKTER_ObatIGD))
                    .KDITEM = grvObatIGD.GetRowCellValue(i, colKDITEM_ObatIGD)
                    .KDUOM = grvObatIGD.GetRowCellValue(i, colKDUOM_ObatIGD)
                    .KDSIGNA = IIf(String.IsNullOrEmpty(grvObatIGD.GetRowCellValue(i, colKDSIGNA_ObatIGD)), oItem.DefaultItem_Signa, grvObatIGD.GetRowCellValue(i, colKDSIGNA_ObatIGD))
                    .KDCARAPAKAI = IIf(String.IsNullOrEmpty(grvObatIGD.GetRowCellValue(i, colKDCARAPAKAI_ObatIGD)), oItem.DefaultItem_CaraPakai, grvObatIGD.GetRowCellValue(i, colKDCARAPAKAI_ObatIGD))
                    .QTY_PERUBAHAN = grvObatIGD.GetRowCellValue(i, colQTY_ObatIGD)
                    .REMARKS_FARMASI = "OBAT IGD"

                    i_ObatIGD += 1
                End With

                If grvObatIGD.GetRowCellValue(i, colKDITEM_ObatIGD) IsNot Nothing Then
                    arrDetail.Add(dsDetail)
                End If
            Next

            Dim i_ObatRanap As Integer = 100
            For i As Integer = 0 To grvObatRanap.RowCount - 2
                Dim dsDetail = oDigitalIGD.GetStructureDetailObat
                With dsDetail
                    Dim Obat As String = String.Empty
                    Dim Uom As String = String.Empty
                    Dim Signa As String = String.Empty
                    Dim CaraPakai As String = String.Empty

                    Dim dsObat = oItem.GetData(grvObatRanap.GetRowCellValue(i, colKDITEM_ObatRanap))
                    If dsObat IsNot Nothing Then
                        Obat = dsObat.NMITEM2
                    End If

                    Dim dsUom = oUom.GetData(grvObatRanap.GetRowCellValue(i, colKDUOM_ObatRanap))
                    If dsUom IsNot Nothing Then
                        Uom = dsUom.MEMO
                    End If

                    Dim dsSigna = oSigna.GetData(grvObatRanap.GetRowCellValue(i, colKDSIGNA_ObatRanap))
                    If dsSigna IsNot Nothing Then
                        Signa = dsSigna.MEMO
                    End If

                    Dim dsCaraPakai = oCaraPakai.GetData(grvObatRanap.GetRowCellValue(i, colKDCARAPAKAI_ObatRanap))
                    If dsCaraPakai IsNot Nothing Then
                        CaraPakai = dsCaraPakai.MEMO
                    End If

                    .SEQ = i_ObatRanap
                    .KODE = ds.KODE
                    .NAMAOBAT = Obat
                    .SATUAN = Uom
                    .SIGNA = IIf(String.IsNullOrEmpty(grvObatRanap.GetRowCellValue(i, colKDSIGNA_ObatRanap)), "", Signa)
                    .CARAPAKAI = IIf(String.IsNullOrEmpty(grvObatRanap.GetRowCellValue(i, colKDCARAPAKAI_ObatRanap)), "", CaraPakai)
                    .QTY = CDec(grvObatRanap.GetRowCellValue(i, colQTY_ObatRanap))
                    .PRICE = CDec(grvObatRanap.GetRowCellValue(i, colPRICE_ObatRanap))
                    .GRANDTOTAL = CDec(grvObatRanap.GetRowCellValue(i, colGRANDTOTAL_ObatRanap))
                    .ROMAWI = IntegerToRoman(CInt(grvObatRanap.GetFocusedRowCellValue(colQTY_ObatRanap)))
                    .REMARKS_DOKTER = IIf(String.IsNullOrEmpty(grvObatRanap.GetRowCellValue(i, colREMARKS_DOKTER_ObatRanap)), "", grvObatRanap.GetRowCellValue(i, colREMARKS_DOKTER_ObatRanap))
                    .KDITEM = grvObatRanap.GetRowCellValue(i, colKDITEM_ObatRanap)
                    .KDUOM = grvObatRanap.GetRowCellValue(i, colKDUOM_ObatRanap)
                    .KDSIGNA = IIf(String.IsNullOrEmpty(grvObatRanap.GetRowCellValue(i, colKDSIGNA_ObatRanap)), oItem.DefaultItem_Signa, grvObatRanap.GetRowCellValue(i, colKDSIGNA_ObatRanap))
                    .KDCARAPAKAI = IIf(String.IsNullOrEmpty(grvObatRanap.GetRowCellValue(i, colKDCARAPAKAI_ObatRanap)), oItem.DefaultItem_CaraPakai, grvObatRanap.GetRowCellValue(i, colKDCARAPAKAI_ObatRanap))
                    .QTY_PERUBAHAN = grvObatRanap.GetRowCellValue(i, colQTY_ObatRanap)
                    .REMARKS_FARMASI = "OBAT RANAP"

                    i_ObatRanap += 1
                End With

                If grvObatRanap.GetRowCellValue(i, colKDITEM_ObatRanap) IsNot Nothing Then
                    arrDetail.Add(dsDetail)
                End If
            Next

            Dim i_ObatPulang As Integer = 200

            For i As Integer = 0 To grvDetailResep.RowCount - 2
                Dim dsDetail = oDigitalIGD.GetStructureDetailObat
                With dsDetail
                    Dim Obat As String = String.Empty
                    Dim Uom As String = String.Empty
                    Dim Signa As String = String.Empty
                    Dim CaraPakai As String = String.Empty

                    Dim dsObat = oItem.GetData(grvDetailResep.GetRowCellValue(i, colKDITEM))
                    If dsObat IsNot Nothing Then
                        Obat = dsObat.NMITEM2
                    End If

                    Dim dsUom = oUom.GetData(grvDetailResep.GetRowCellValue(i, colKDUOM))
                    If dsUom IsNot Nothing Then
                        Uom = dsUom.MEMO
                    End If

                    Dim dsSigna = oSigna.GetData(grvDetailResep.GetRowCellValue(i, colKDSIGNA))
                    If dsSigna IsNot Nothing Then
                        Signa = dsSigna.MEMO
                    End If

                    Dim dsCaraPakai = oCaraPakai.GetData(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI))
                    If dsCaraPakai IsNot Nothing Then
                        CaraPakai = dsCaraPakai.MEMO
                    End If

                    .SEQ = i_ObatPulang
                    .KODE = ds.KODE
                    .NAMAOBAT = Obat
                    .SATUAN = Uom
                    .SIGNA = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDSIGNA)), "", Signa)
                    .CARAPAKAI = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)), "", CaraPakai)
                    .QTY = CDec(grvDetailResep.GetRowCellValue(i, colQTY))
                    .PRICE = CDec(grvDetailResep.GetRowCellValue(i, colPRICE))
                    .GRANDTOTAL = CDec(grvDetailResep.GetRowCellValue(i, colGRANDTOTAL))
                    .ROMAWI = IntegerToRoman(CInt(grvDetailResep.GetFocusedRowCellValue(colQTY)))
                    .REMARKS_DOKTER = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER)), "", grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER))
                    .KDITEM = grvDetailResep.GetRowCellValue(i, colKDITEM)
                    .KDUOM = grvDetailResep.GetRowCellValue(i, colKDUOM)
                    .KDSIGNA = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDSIGNA)), oItem.DefaultItem_Signa, grvDetailResep.GetRowCellValue(i, colKDSIGNA))
                    .KDCARAPAKAI = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)), oItem.DefaultItem_CaraPakai, grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI))
                    .QTY_PERUBAHAN = grvDetailResep.GetRowCellValue(i, colQTY)
                    .REMARKS_FARMASI = "OBAT PULANG"

                    i_ObatPulang += 1
                End With

                If grvDetailResep.GetRowCellValue(i, colKDITEM) IsNot Nothing Then
                    arrDetail.Add(dsDetail)
                End If
            Next

            'DIAGNOSA
            Dim arrDetailDiagnosa = oDigitalIGD.GetStructureDetailList
            For i As Integer = 0 To grvDIAGNOSA_CPPT.RowCount - 2
                Dim dsDetail = oDigitalIGD.GetStructureDetail
                With dsDetail
                    .SEQ = i
                    .KODE = ds.KODE
                    .KATEGORI = IIf(String.IsNullOrEmpty(grvDIAGNOSA_CPPT.GetRowCellValue(i, colKATEGORI_CPPT)), "", grvDIAGNOSA_CPPT.GetRowCellValue(i, colKATEGORI_CPPT))
                    .NAMADIAGNOSA = IIf(String.IsNullOrEmpty(grvDIAGNOSA_CPPT.GetRowCellValue(i, colNAMADIAGNOSA_CPPT)), "", grvDIAGNOSA_CPPT.GetRowCellValue(i, colNAMADIAGNOSA_CPPT))
                    .KDDIAGNOSA = IIf(String.IsNullOrEmpty(grvDIAGNOSA_CPPT.GetRowCellValue(i, colKDDIAGNOSA_CPPT)), "", grvDIAGNOSA_CPPT.GetRowCellValue(i, colKDDIAGNOSA_CPPT))
                End With
                arrDetailDiagnosa.Add(dsDetail)
            Next

            Dim arrDetailTindakan = oDigitalIGD.GetStructureDetailTindakanList
            For i As Integer = 0 To grvCPPT_Tindakan.RowCount - 2
                Dim dsDetail = oDigitalIGD.GetStructureDetailTindakan
                With dsDetail
                    .DATECREATED = ds.DATECREATED
                    .DATEUPDATED = ds.DATEUPDATED
                    .KODE = ds.KODE
                    .SEQ = i
                    .KDITEM = grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_KDITEMTINDAKAN)
                    .KDUOM = grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_KDUOMTINDAKAN)
                    .JUMLAH = CDec(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_JUMLAHTINDAKAN))
                    .HARGA = CDec(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_HARGATINDAKAN))
                    .TOTAL = CDec(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_TOTALTINDAKAN))
                    .MEMO = IIf(String.IsNullOrEmpty(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)), "", grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN))
                    .KDUSER = sUserID
                    .ISBACA = IIf(String.IsNullOrEmpty(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_ISBACA)), "0", grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_ISBACA))
                    .ISPERAWAT = False
                End With
                arrDetailTindakan.Add(dsDetail)
            Next

            Dim SeqTindakan As Integer = 0
            Dim SeqPenunjang As Integer = 0

            Dim arrDetailTindakanPoli = oDigitalIGD.GetStructureDetailTindakanPoliList
            Dim arrDetailPenunjang = oDigitalIGD.GetStructureDetailPenunjangList

            For Each xloop In arrDetailTindakan
                Dim dsItem = oItem.GetData(xloop.KDITEM)
                If dsItem IsNot Nothing Then
                    If dsItem.M_ITEM_L3.MEMO = "LABORATORIUM" Or dsItem.M_ITEM_L3.MEMO = "BANK DARAH" Or dsItem.M_ITEM_L3.MEMO = "RADIOLOGI" Then
                        Dim dsDetail = oDigitalIGD.GetStructureDetailPenunjang
                        With dsDetail
                            .SEQ = SeqPenunjang
                            .KODE = ds.KODE
                            .PENANGANAN = dsItem.NMITEM2
                        End With
                        arrDetailPenunjang.Add(dsDetail)

                        SeqPenunjang += 1
                    Else
                        Dim dsDetailTindakanPoli = oDigitalIGD.GetStructureDetailTindakanPoli
                        With dsDetailTindakanPoli
                            .SEQ = SeqTindakan
                            .KODE = ds.KODE
                            .PENANGANAN = dsItem.NMITEM2
                        End With
                        arrDetailTindakanPoli.Add(dsDetailTindakanPoli)

                        SeqTindakan += 1
                    End If
                End If
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    sNoid = oDigitalIGD.InsertData(ds, arrDetailDiagnosa, arrDetail, arrDetailTindakanPoli, arrDetailPenunjang, arrDetailTindakan)

                    If sNoid = "" Then
                        fn_Save = False
                    Else
                        fn_Save = True
                    End If
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oDigitalIGD.UpdateData(ds, arrDetailDiagnosa, arrDetail, arrDetailTindakanPoli, arrDetailPenunjang, arrDetailTindakan)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

            If fn_Save = True Then
                Dim dsCPPT = oGrouperDataCppt.GetData(ds.KODE)
                If dsCPPT Is Nothing Then
                    fn_Save(ds.KODE, True)
                Else
                    fn_Save(ds.KODE, False)
                End If
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data : " & vbCrLf & txtNoRegister.Text & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Function fn_Save(ByVal sNoid As String, ByVal isAdd As Boolean) As Boolean
        Try
            Dim sCategory As Integer = 0
            Dim dsIdentitas = oGrouperDataCppt.GetDataByKdKunjungan(sKDKUNJUNGAN)
            Dim sKDIDENTITAS As Integer = 0
            If dsIdentitas IsNot Nothing Then
                sKDIDENTITAS = dsIdentitas.KDIDENTITAS
                sCategory = dsIdentitas.CATEGORY
            Else
                MsgBox("Identitas Kosong", MsgBoxStyle.Exclamation, Me.Text)
                'Exit Function
            End If

            Dim oItem As New Reference.clsItem
            Dim oUom As New Reference.clsUOM
            Dim oSiga As New Reference.clsSigna
            Dim oCaraPakai As New Reference.clsCaraPakai
            Dim Alasan As String = String.Empty

            ' ***** HEADER *****
            Dim ds = oGrouperDataCppt.GetStructureHeader
            With ds
                .DATECREATED = Now
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .KDCPPT = sNoid
                .KDIDENTITAS = sKDIDENTITAS
                .KDPROFESI = "PROFESI_0000000001"
                .SUBJEKTIF_KELUHANUTAMA = txtKELUHANUTAMA.Text
                .SUBJEKTIF_ALERGI_TIDAK = IIf(txtRIWAYAT_ALERGI.Text = "", True, False)
                .SUBJEKTIF_ALERGI_YA = IIf(txtRIWAYAT_ALERGI.Text <> "", True, False)
                .SUBJEKTIF_ALERGI_YA_TEXT = txtRIWAYAT_ALERGI.Text

                Dim listSubjektif As New List(Of String)

                listSubjektif.Add("Keluhan Utama " & .SUBJEKTIF_KELUHANUTAMA)

                Dim Alergi As String = String.Empty

                If .SUBJEKTIF_ALERGI_TIDAK = True Then
                    Alergi = "Alergi Obat: Tidak"
                End If
                If .SUBJEKTIF_ALERGI_YA = True Then
                    Alergi = "Alergi Obat: Ya" & ", " & .SUBJEKTIF_ALERGI_YA_TEXT
                End If

                'listSubjektif.Add((Alergi & " " & .SUBJEKTIF_ALERGI_YA_TEXT).Trim)

                .SUBJEKTIF_TEXT = String.Join(vbCrLf, listSubjektif.ToArray) & vbCrLf & Alergi

                .OBJEKTIF_KESADARAN = cboTINGKATKESADARAN.Text
                .OBJEKTIF_GCS = txtGCS.Text
                .OBJEKTIF_TAMPAKSAKIT = ""
                .OBJEKTIF_VISUALANALOGSCORE = ""
                .OBJEKTIF_BERATBADAN = txtBERATBADAN.Text
                .OBJEKTIF_TINGGIBADAN = txtTINGGIBADAN.Text
                .OBJEKTIF_SPO2 = txtSPO2.Text

                Try
                    Dim tekananDarah As String = txtBP.Text
                    Dim hasil() As String = tekananDarah.Split("/"c)

                    Dim sistole As Integer = Convert.ToInt32(hasil(0))
                    Dim diastole As Integer = Convert.ToInt32(hasil(1))
                    .OBJEKTIF_SISTOLE = sistole
                    .OBJEKTIF_DIASTOLE = diastole
                Catch ex As Exception
                    .OBJEKTIF_SISTOLE = 0
                    .OBJEKTIF_DIASTOLE = 0
                End Try


                .OBJEKTIF_HR = txtHR.Text
                .OBJEKTIF_RR = txtRR.Text
                .OBJEKTIF_SUHU = txtT.Text
                .OBJEKTIF_PEMERIKSAAN = txtSURVEY_ALATGERAK_1.Text

                'If sPicture Is Nothing Then
                '    .OBJEKTIF_ALAMATGAMBARPEMERIKSAAN = txtALAMATGAMBAR.Text
                'Else
                '    If sAlamatSimpanFolder <> "" Then
                '        If Directory.Exists(sAlamatSimpanFolder) Then
                '            Dim Alamat As String = sAlamatSimpanFolder & "CPPT_" & Now.ToString("ddMMyyyyHHmm") & "." & .KDIDENTITAS & ".Png"
                '            picGAMBAR2.Image.Save(Alamat, System.Drawing.Imaging.ImageFormat.Png)
                '            .OBJEKTIF_ALAMATGAMBARPEMERIKSAAN = Alamat
                '        Else
                '            MsgBox("Folder Simpan Gambar ke File " & sAlamatSimpanFolder & " Tidak dapat diakses")
                '            .OBJEKTIF_ALAMATGAMBARPEMERIKSAAN = txtALAMATGAMBAR.Text
                '        End If
                '    Else
                '        MsgBox("Belum Ada Simpan Gambar")
                '        .OBJEKTIF_ALAMATGAMBARPEMERIKSAAN = txtALAMATGAMBAR.Text
                '    End If
                'End If

                .OBJEKTIF_ALAMATGAMBARPEMERIKSAAN = ""

                Dim listOBjektif As New List(Of String)
                Dim oRME As New RME.clsRME

                If .OBJEKTIF_KESADARAN <> "" Then
                    listOBjektif.Add("Kesadaran : " & .OBJEKTIF_KESADARAN)
                End If

                listOBjektif.Add("Umur : " & oRME.GetUmurPasien(deDATE.DateTime, sTANGGALLAHIR))

                If .OBJEKTIF_GCS <> "" Then
                    listOBjektif.Add("IMT : " & .OBJEKTIF_GCS)
                End If
                If .OBJEKTIF_TAMPAKSAKIT <> "" Then
                    listOBjektif.Add("BB Ideal : " & .OBJEKTIF_TAMPAKSAKIT)
                End If
                If .OBJEKTIF_VISUALANALOGSCORE <> "" Then
                    listOBjektif.Add("BB Yang diturunkan : " & .OBJEKTIF_VISUALANALOGSCORE)
                End If
                If .OBJEKTIF_BERATBADAN <> "" Then
                    listOBjektif.Add("Berat Badan : " & .OBJEKTIF_BERATBADAN.Replace(",", ".") & " kg")
                End If
                If .OBJEKTIF_TINGGIBADAN <> "" Then
                    listOBjektif.Add("Tinggi Badan : " & .OBJEKTIF_TINGGIBADAN.Replace(",", ".") & " cm")
                End If
                If .OBJEKTIF_SPO2 <> "" Then
                    listOBjektif.Add("SpO2 : " & .OBJEKTIF_SPO2 & " %")
                End If
                listOBjektif.Add("Tekanan Darah : " & .OBJEKTIF_SISTOLE & "/" & .OBJEKTIF_DIASTOLE & " mmHg")
                If .OBJEKTIF_HR <> "" Then
                    listOBjektif.Add("HR : " & .OBJEKTIF_HR & " x/mnt")
                End If
                If .OBJEKTIF_RR <> "" Then
                    listOBjektif.Add("RR : " & .OBJEKTIF_RR & " x/mnt")
                End If
                If .OBJEKTIF_SUHU <> "" Then
                    listOBjektif.Add("Suhu : " & .OBJEKTIF_SUHU & " oC")
                End If
                If .OBJEKTIF_PEMERIKSAAN <> "" Then
                    listOBjektif.Add("Pemeriksaan : " & .OBJEKTIF_PEMERIKSAAN)
                End If

                .OBJEKTIF_TEXT = String.Join(vbCrLf, listOBjektif.ToArray)

                .ASSEMENT_INDIKASI = ""

                Dim listAsesment As New List(Of String)

                For i As Integer = 0 To grvDIAGNOSA_CPPT.RowCount - 2
                    listAsesment.Add((IIf(String.IsNullOrEmpty(grvDIAGNOSA_CPPT.GetRowCellValue(i, colKDDIAGNOSA_CPPT)), "", grvDIAGNOSA_CPPT.GetRowCellValue(i, colKDDIAGNOSA_CPPT) & " ")).ToString.Trim & grvDIAGNOSA_CPPT.GetRowCellValue(i, colNAMADIAGNOSA_CPPT))
                Next

                .ASSEMENT_TEXT = String.Join(vbCrLf, listAsesment.ToArray)

                .PLANNING_ISTINDAKLANJUT_PULANG = False
                .PLANNING_ISTINDAKLANJUT_RAWAT = False
                .PLANNING_ISTINDAKLANJUT_KONSUL = False
                .PLANNING_ISTINDAKLANJUT_RUJUK = False
                .PLANNING_ISTINDAKLANJUT_KONSUL_TEXT = "DAFTAR_L4_0000000001"
                .PLANNING_ISTINDAKLANJUT_RUJUK_TEXT = ""
                .PLANNING_ALASAN = txtTINDAKLANJUT.Text
                .PLANNING_TEXT = ""

                'Dim oSkd As New Admission.clsSKD
                'Dim oKunjungan As New Admission.clsPendaftaran_Kunjungan

                'Dim dsKunjungan = oKunjungan.GetData(sKDKUNJUNGAN )

                'If dsKunjungan IsNot Nothing Then
                '    Dim dsSKD = oSkd.GetDataPendaftaran(dsKunjungan.KDPENDAFTARAN)

                '    If dsSKD IsNot Nothing Then
                '        'txtTINDAKLANJUT.Text = "Kontrol Tanggal : " & dsSKD.DATEKONTROL.ToString("dd-MM-yyyy")
                '        'Alasan = dsSKD.DESCRIPTION & " " & dsSKD.ALASAN
                '        'KONSUL
                '        ds.PLANNING_ISTINDAKLANJUT_KONSUL_TEXT = "DAFTAR_L4_0000000007"
                '    End If
                'End If

                .CATATAN = "ASESMEN AWAL MEDIS IGD"
                .KDUSER = sUserID
                Try
                    .ISDELETE = oGrouperDataCppt.GetData(sNoid).ISDELETE
                Catch ex As Exception
                    .ISDELETE = False
                End Try
                Try
                    .DATEDELETE = oGrouperDataCppt.GetData(sNoid).DATEDELETE
                Catch ex As Exception
                    .DATEDELETE = ds.DATECREATED
                End Try
                Try
                    .USERDELETE = oGrouperDataCppt.GetData(sNoid).USERDELETE
                Catch ex As Exception
                    .USERDELETE = ""
                End Try

                Try
                    .GOALOFTREATMENT = oGrouperDataCppt.GetData(sNoid).GOALOFTREATMENT
                Catch ex As Exception
                    .GOALOFTREATMENT = ""
                End Try
                Try
                    .TINDAKANREHAB = oGrouperDataCppt.GetData(sNoid).TINDAKANREHAB
                Catch ex As Exception
                    .TINDAKANREHAB = ""
                End Try
                Try
                    .EDUKASI = oGrouperDataCppt.GetData(sNoid).EDUKASI
                Catch ex As Exception
                    .EDUKASI = ""
                End Try
                Try
                    .FREKUENSIKUNJUNGAN = oGrouperDataCppt.GetData(sNoid).FREKUENSIKUNJUNGAN
                Catch ex As Exception
                    .FREKUENSIKUNJUNGAN = ""
                End Try
            End With

            ' ***** DETIL *****
            Dim arrDetailDiagnosa = oGrouperDataCppt.GetStructureDetailDiagnosaList

            'Dim dsDetailPrimer = oGrouperDataCppt.GetStructureDetailDiagnosa
            'With dsDetailPrimer
            '    .DATECREATED = ds.DATECREATED
            '    .DATEUPDATED = ds.DATEUPDATED
            '    .KDCPPT = ds.KDCPPT
            '    .SEQ = 0
            '    .KATEGORI = "Primer"
            '    .KDDIAGNOSA = ""
            '    .MEMO = txtDIAGNOOSA.Text
            '    .KDUSER = sUserID
            'End With

            'arrDetailDiagnosa.Add(dsDetailPrimer)

            For i As Integer = 0 To grvDIAGNOSA_CPPT.RowCount - 2
                Dim dsDetail = oGrouperDataCppt.GetStructureDetailDiagnosa
                With dsDetail
                    .DATECREATED = ds.DATECREATED
                    .DATEUPDATED = ds.DATEUPDATED
                    .KDCPPT = ds.KDCPPT
                    .SEQ = i
                    .KATEGORI = IIf(String.IsNullOrEmpty(grvDIAGNOSA_CPPT.GetRowCellValue(i, colKATEGORI_CPPT)), IIf(i = 0, "Primary", "Secondary"), IIf(grvDIAGNOSA_CPPT.GetRowCellValue(i, colKATEGORI_CPPT) = "", IIf(i = 0, "Primary", "Secondary"), grvDIAGNOSA_CPPT.GetRowCellValue(i, colKATEGORI_CPPT)))
                    .KDDIAGNOSA = IIf(String.IsNullOrEmpty(grvDIAGNOSA_CPPT.GetRowCellValue(i, colKDDIAGNOSA_CPPT)), "", grvDIAGNOSA_CPPT.GetRowCellValue(i, colKDDIAGNOSA_CPPT))
                    .MEMO = IIf(String.IsNullOrEmpty(grvDIAGNOSA_CPPT.GetRowCellValue(i, colNAMADIAGNOSA_CPPT)), "", grvDIAGNOSA_CPPT.GetRowCellValue(i, colNAMADIAGNOSA_CPPT))
                    .KDUSER = sUserID
                End With
                arrDetailDiagnosa.Add(dsDetail)
            Next

            Dim arrDetailTindakan = oGrouperDataCppt.GetStructureDetailTindakanList
            For i As Integer = 0 To grvCPPT_Tindakan.RowCount - 2
                Dim dsDetail = oGrouperDataCppt.GetStructureDetailTindakan
                With dsDetail
                    .DATECREATED = ds.DATECREATED
                    .DATEUPDATED = ds.DATEUPDATED
                    .KDCPPT = ds.KDCPPT
                    .SEQ = i
                    .KDITEM = grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_KDITEMTINDAKAN)
                    .KDUOM = grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_KDUOMTINDAKAN)
                    .JUMLAH = CDec(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_JUMLAHTINDAKAN))
                    .HARGA = CDec(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_HARGATINDAKAN))
                    .TOTAL = CDec(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_TOTALTINDAKAN))
                    .MEMO = IIf(String.IsNullOrEmpty(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)), "", grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN))
                    .KDUSER = sUserID
                    .ISBACA = IIf(String.IsNullOrEmpty(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_ISBACA)), "0", grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_ISBACA))
                    .ISPERAWAT = False
                End With
                arrDetailTindakan.Add(dsDetail)
            Next

            Dim arrDetailTindakanTerjadwal = oGrouperDataCppt.GetStructureDetailTindakanTerjadwalList
            'For i As Integer = 0 To grvTindakanTerjadwal.RowCount - 2
            '    Dim dsDetail = oGrouperDataCppt.GetStructureDetailTindakanTerjadawal
            '    With dsDetail
            '        .DATECREATED = ds.DATECREATED
            '        .DATEUPDATED = ds.DATEUPDATED
            '        .TANGGALORDER = CDate(grvTindakanTerjadwal.GetRowCellValue(i, colTANGGALORDERTindakanTerjadwal))
            '        .JENISORDER = "TERJADWAL"
            '        .KDORDER = ds.KDCPPT
            '        .KDIDENTITAS = ds.KDIDENTITAS
            '        .NOMORREFERENCE = ds.KDCPPT
            '        .KDITEM = grvTindakanTerjadwal.GetRowCellValue(i, colKDITEMTindakanTerjadwal)
            '        Dim dsItem = oItem.GetData(grvTindakanTerjadwal.GetRowCellValue(i, colKDITEMTindakanTerjadwal))
            '        If dsItem IsNot Nothing Then
            '            .NMITEM2 = dsItem.NMITEM2
            '        Else
            '            .NMITEM2 = ""
            '        End If
            '        .USERORDER = sUserID
            '        .STATUS = ""
            '        .MEMO = IIf(String.IsNullOrEmpty(grvTindakanTerjadwal.GetRowCellValue(i, colREMARKSTindakanTerjadwal)), "", grvTindakanTerjadwal.GetRowCellValue(i, colREMARKSTindakanTerjadwal))
            '    End With
            '    arrDetailTindakanTerjadwal.Add(dsDetail)
            'Next


            Dim arrDetail = oGrouperDataCppt.GetStructureDetailNonRacikanList

            Dim i_ObatIGD As Integer = 0
            For i As Integer = 0 To grvObatIGD.RowCount - 2
                Dim dsDetail = oGrouperDataCppt.GetStructureDetailNonRacikan
                With dsDetail
                    .DATECREATED = ds.DATECREATED
                    .DATEUPDATED = ds.DATEUPDATED

                    Dim Obat As String = String.Empty
                    Dim Uom As String = String.Empty
                    Dim Signa As String = String.Empty
                    Dim CaraPakai As String = String.Empty

                    Dim dsObat = oItem.GetData(grvObatIGD.GetRowCellValue(i, colKDITEM_ObatIGD))
                    If dsObat IsNot Nothing Then
                        Obat = dsObat.NMITEM2
                    End If

                    Dim dsUom = oUom.GetData(grvObatIGD.GetRowCellValue(i, colKDUOM_ObatIGD))
                    If dsUom IsNot Nothing Then
                        Uom = dsUom.MEMO
                    End If

                    Dim dsSigna = oSiga.GetData(grvObatIGD.GetRowCellValue(i, colKDSIGNA_ObatIGD))
                    If dsSigna IsNot Nothing Then
                        Signa = dsSigna.MEMO
                    End If

                    Dim dsCaraPakai = oCaraPakai.GetData(grvObatIGD.GetRowCellValue(i, colKDCARAPAKAI_ObatIGD))
                    If dsCaraPakai IsNot Nothing Then
                        CaraPakai = dsCaraPakai.MEMO
                    End If

                    .SEQ = i_ObatIGD
                    .KDCPPT = ds.KDCPPT
                    .NAMAOBAT = Obat
                    .SATUAN = Uom
                    .SIGNA = IIf(String.IsNullOrEmpty(grvObatIGD.GetRowCellValue(i, colKDSIGNA_ObatIGD)), "", Signa)
                    .CARAPAKAI = IIf(String.IsNullOrEmpty(grvObatIGD.GetRowCellValue(i, colKDCARAPAKAI_ObatIGD)), "", CaraPakai)
                    .JUMLAH_NONPAKET = CDec(0)
                    .JUMLAH_PAKET = CDec(0)
                    .JUMLAH = CDec(grvObatIGD.GetRowCellValue(i, colQTY_ObatIGD))
                    .HARGA = CDec(grvObatIGD.GetRowCellValue(i, colPRICE_ObatIGD))
                    .TOTAL_PAKET = CDec(0)
                    .TOTAL_NONPAKET = CDec(0)
                    .TOTAL = CDec(grvObatIGD.GetRowCellValue(i, colGRANDTOTAL_ObatIGD))
                    .ROMAWI = IntegerToRoman(CInt(grvObatIGD.GetFocusedRowCellValue(colQTY_ObatIGD)))
                    .REMARKS_DOKTER = IIf(String.IsNullOrEmpty(grvObatIGD.GetRowCellValue(i, colREMARKS_DOKTER_ObatIGD)), "", grvObatIGD.GetRowCellValue(i, colREMARKS_DOKTER_ObatIGD))
                    .KDITEM = grvObatIGD.GetRowCellValue(i, colKDITEM_ObatIGD)
                    .ISKRONIS = False
                    .ISALKES = False
                    .KDUOM = grvObatIGD.GetRowCellValue(i, colKDUOM_ObatIGD)
                    .KDSIGNA = IIf(String.IsNullOrEmpty(grvObatIGD.GetRowCellValue(i, colKDSIGNA_ObatIGD)), oItem.DefaultItem_Signa, grvObatIGD.GetRowCellValue(i, colKDSIGNA_ObatIGD))
                    .KDCARAPAKAI = IIf(String.IsNullOrEmpty(grvObatIGD.GetRowCellValue(i, colKDCARAPAKAI_ObatIGD)), oItem.DefaultItem_CaraPakai, grvObatIGD.GetRowCellValue(i, colKDCARAPAKAI_ObatIGD))
                    .QTY_PERUBAHAN = grvObatIGD.GetRowCellValue(i, colQTY_ObatIGD)
                    .REMARKS_FARMASI = "OBAT IGD"
                    .KDUSER = sUserID
                    .ISBACA = "0"

                    i_ObatIGD += 1
                End With

                If grvObatIGD.GetRowCellValue(i, colKDITEM_ObatIGD) IsNot Nothing Then
                    arrDetail.Add(dsDetail)
                End If
            Next

            Dim i_ObatRanap As Integer = 100
            For i As Integer = 0 To grvObatRanap.RowCount - 2
                Dim dsDetail = oGrouperDataCppt.GetStructureDetailNonRacikan
                With dsDetail
                    .DATECREATED = ds.DATECREATED
                    .DATEUPDATED = ds.DATEUPDATED

                    Dim Obat As String = String.Empty
                    Dim Uom As String = String.Empty
                    Dim Signa As String = String.Empty
                    Dim CaraPakai As String = String.Empty

                    Dim dsObat = oItem.GetData(grvObatRanap.GetRowCellValue(i, colKDITEM_ObatRanap))
                    If dsObat IsNot Nothing Then
                        Obat = dsObat.NMITEM2
                    End If

                    Dim dsUom = oUom.GetData(grvObatRanap.GetRowCellValue(i, colKDUOM_ObatRanap))
                    If dsUom IsNot Nothing Then
                        Uom = dsUom.MEMO
                    End If

                    Dim dsSigna = oSiga.GetData(grvObatRanap.GetRowCellValue(i, colKDSIGNA_ObatRanap))
                    If dsSigna IsNot Nothing Then
                        Signa = dsSigna.MEMO
                    End If

                    Dim dsCaraPakai = oCaraPakai.GetData(grvObatRanap.GetRowCellValue(i, colKDCARAPAKAI_ObatRanap))
                    If dsCaraPakai IsNot Nothing Then
                        CaraPakai = dsCaraPakai.MEMO
                    End If

                    .SEQ = i_ObatRanap
                    .KDCPPT = ds.KDCPPT
                    .NAMAOBAT = Obat
                    .SATUAN = Uom
                    .SIGNA = IIf(String.IsNullOrEmpty(grvObatRanap.GetRowCellValue(i, colKDSIGNA_ObatRanap)), "", Signa)
                    .CARAPAKAI = IIf(String.IsNullOrEmpty(grvObatRanap.GetRowCellValue(i, colKDCARAPAKAI_ObatRanap)), "", CaraPakai)
                    .JUMLAH_NONPAKET = CDec(0)
                    .JUMLAH_PAKET = CDec(0)
                    .JUMLAH = CDec(grvObatRanap.GetRowCellValue(i, colQTY_ObatRanap))
                    .HARGA = CDec(grvObatRanap.GetRowCellValue(i, colPRICE_ObatRanap))
                    .TOTAL_PAKET = CDec(0)
                    .TOTAL_NONPAKET = CDec(0)
                    .TOTAL = CDec(grvObatRanap.GetRowCellValue(i, colGRANDTOTAL_ObatRanap))
                    .ROMAWI = IntegerToRoman(CInt(grvObatRanap.GetFocusedRowCellValue(colQTY_ObatRanap)))
                    .REMARKS_DOKTER = IIf(String.IsNullOrEmpty(grvObatRanap.GetRowCellValue(i, colREMARKS_DOKTER_ObatRanap)), "", grvObatRanap.GetRowCellValue(i, colREMARKS_DOKTER_ObatRanap))
                    .KDITEM = grvObatRanap.GetRowCellValue(i, colKDITEM_ObatRanap)
                    .ISKRONIS = False
                    .ISALKES = False
                    .KDUOM = grvObatRanap.GetRowCellValue(i, colKDUOM_ObatRanap)
                    .KDSIGNA = IIf(String.IsNullOrEmpty(grvObatRanap.GetRowCellValue(i, colKDSIGNA_ObatRanap)), oItem.DefaultItem_Signa, grvObatRanap.GetRowCellValue(i, colKDSIGNA_ObatRanap))
                    .KDCARAPAKAI = IIf(String.IsNullOrEmpty(grvObatRanap.GetRowCellValue(i, colKDCARAPAKAI_ObatRanap)), oItem.DefaultItem_CaraPakai, grvObatRanap.GetRowCellValue(i, colKDCARAPAKAI_ObatRanap))
                    .QTY_PERUBAHAN = grvObatRanap.GetRowCellValue(i, colQTY_ObatRanap)
                    .REMARKS_FARMASI = "OBAT RANAP"
                    .KDUSER = sUserID
                    .ISBACA = "0"

                    i_ObatRanap += 1
                End With

                If grvObatRanap.GetRowCellValue(i, colKDITEM_ObatRanap) IsNot Nothing Then
                    arrDetail.Add(dsDetail)
                End If
            Next

            Dim i_ObatPulang As Integer = 200

            For i As Integer = 0 To grvDetailResep.RowCount - 2
                Dim dsDetail = oGrouperDataCppt.GetStructureDetailNonRacikan
                With dsDetail
                    .DATECREATED = ds.DATECREATED
                    .DATEUPDATED = ds.DATEUPDATED
                    .SEQ = i_ObatPulang
                    .KDCPPT = ds.KDCPPT
                    Dim dsItem = oItem.GetData(grvDetailResep.GetRowCellValue(i, colKDITEM))
                    If dsItem IsNot Nothing Then
                        .NAMAOBAT = dsItem.NMITEM2
                    Else
                        .NAMAOBAT = ""
                    End If
                    Dim dsUom = oUom.GetData(grvDetailResep.GetRowCellValue(i, colKDUOM))
                    If dsUom IsNot Nothing Then
                        .SATUAN = dsUom.MEMO
                    Else
                        .SATUAN = ""
                    End If
                    Dim dsSigna = oSiga.GetData(grvDetailResep.GetRowCellValue(i, colKDSIGNA))
                    If dsSigna IsNot Nothing Then
                        .SIGNA = dsSigna.MEMO
                    Else
                        .SIGNA = ""
                    End If
                    Dim dsCaraPakai = oCaraPakai.GetData(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI))
                    If dsCaraPakai IsNot Nothing Then
                        .CARAPAKAI = dsCaraPakai.MEMO
                    Else
                        .CARAPAKAI = ""
                    End If
                    .KDCARAPAKAI = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)), oItem.DefaultItem_CaraPakai, grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI))
                    .JUMLAH_PAKET = CDec(0)
                    .JUMLAH_NONPAKET = CDec(0)
                    .JUMLAH = CDec(grvDetailResep.GetRowCellValue(i, colQTY))
                    .HARGA = CDec(grvDetailResep.GetRowCellValue(i, colPRICE))
                    .TOTAL_PAKET = CDec(0)
                    .TOTAL_NONPAKET = CDec(0)
                    .TOTAL = CDec(grvDetailResep.GetRowCellValue(i, colGRANDTOTAL))
                    .ROMAWI = IntegerToRoman(CInt(grvDetailResep.GetFocusedRowCellValue(colQTY)))
                    .REMARKS_DOKTER = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER)), "", grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER))
                    .KDITEM = grvDetailResep.GetRowCellValue(i, colKDITEM)
                    .ISKRONIS = False
                    .ISALKES = False
                    .KDUOM = grvDetailResep.GetRowCellValue(i, colKDUOM)
                    .KDSIGNA = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDSIGNA)), oItem.DefaultItem_Signa, grvDetailResep.GetRowCellValue(i, colKDSIGNA))
                    .QTY_PERUBAHAN = 0
                    .REMARKS_FARMASI = ""
                    .KDUSER = sUserID
                    .ISBACA = "0"

                    i_ObatPulang += 1
                End With

                arrDetail.Add(dsDetail)
            Next

            Dim arrDetailObatRacikan = oGrouperDataCppt.GetStructureDetailRacikanList
            'For i As Integer = 0 To grvOBATRACIKAN.RowCount - 2
            '    Dim dsDetail = oGrouperDataCppt.GetStructureDetailRacikan
            '    With dsDetail
            '        .DATECREATED = ds.DATECREATED
            '        .DATEUPDATED = ds.DATEUPDATED
            '        .KDCPPT = ds.KDCPPT
            '        .SEQ = i
            '        .KDITEM = grvOBATRACIKAN.GetRowCellValue(i, colKDITEMOBATRACIKAN)
            '        Dim dsItem = oItem.GetData(grvOBATRACIKAN.GetRowCellValue(i, colKDITEMOBATRACIKAN))
            '        If dsItem IsNot Nothing Then
            '            .NAMAOBAT = dsItem.NMITEM2
            '        Else
            '            .NAMAOBAT = "RACIKAN"
            '        End If
            '        .KDUOM = IIf(String.IsNullOrEmpty(grvOBATRACIKAN.GetRowCellValue(i, colKDUOMOBATRACIKAN)), "", grvOBATRACIKAN.GetRowCellValue(i, colKDUOMOBATRACIKAN))
            '        Dim dsUom = oUom.GetData(grvOBATRACIKAN.GetRowCellValue(i, colKDUOMOBATRACIKAN))
            '        If dsUom IsNot Nothing Then
            '            .SATUAN = dsUom.MEMO
            '        Else
            '            .SATUAN = ""
            '        End If
            '        .SIGNA = IIf(String.IsNullOrEmpty(grvOBATRACIKAN.GetRowCellValue(i, colSIGNAOBATRACIKAN)), "", grvOBATRACIKAN.GetRowCellValue(i, colSIGNAOBATRACIKAN))
            '        .PERMINTAAN = IIf(String.IsNullOrEmpty(grvOBATRACIKAN.GetRowCellValue(i, colPERMINTAANOBATRACIKAN)), "", grvOBATRACIKAN.GetRowCellValue(i, colPERMINTAANOBATRACIKAN))
            '        .JUMLAH = IIf(String.IsNullOrEmpty(grvOBATRACIKAN.GetRowCellValue(i, colQTYOBATRACIKAN)), 0, grvOBATRACIKAN.GetRowCellValue(i, colQTYOBATRACIKAN))
            '        .HARGA = CDec(0)
            '        .TOTAL = CDec(IIf(String.IsNullOrEmpty(grvOBATRACIKAN.GetRowCellValue(i, colQTYOBATRACIKAN)), 0, grvOBATRACIKAN.GetRowCellValue(i, colQTYOBATRACIKAN)))
            '        .REMARKS = IIf(String.IsNullOrEmpty(grvOBATRACIKAN.GetRowCellValue(i, colREMARKSRACIKAN)), "", grvOBATRACIKAN.GetRowCellValue(i, colREMARKSRACIKAN))
            '        .KDUSER = sUserID
            '        .ISBACA = 0
            '    End With
            '    arrDetailObatRacikan.Add(dsDetail)
            'Next

            'Dim listTindakanLab As New List(Of String)
            'Dim listTindakanRad As New List(Of String)
            Dim listObatNonRacik As New List(Of String)
            Dim listObatRacik As New List(Of String)
            Dim listPlanningTindakan As New List(Of String)
            Dim listPlanningTindakanPenunjang As New List(Of String)
            Dim listPlanningObat As New List(Of String)
            Dim listPlanningTindakanTerjadwal As New List(Of String)

            For i As Integer = 0 To grvCPPT_Tindakan.RowCount - 2
                Dim dsItem = oItem.GetData(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_KDITEMTINDAKAN))
                If dsItem IsNot Nothing Then
                    If dsItem.M_ITEM_L3.MEMO = "LABORATORIUM" Then
                        'If grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_ISPERAWAT) = False Then
                        '    'listTindakanLab.Add(dsItem.NMITEM2 & " " & IIf(String.IsNullOrEmpty(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)), "", grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)))
                        'End If
                        listPlanningTindakanPenunjang.Add(dsItem.NMITEM2 & " " & IIf(String.IsNullOrEmpty(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)), "", grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)))
                    ElseIf dsItem.M_ITEM_L3.MEMO = "BANK DARAH" Then
                        'If grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_ISPERAWAT) = False Then
                        '    'listTindakanLab.Add(dsItem.NMITEM2 & " " & IIf(String.IsNullOrEmpty(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)), "", grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)))
                        'End If
                        listPlanningTindakanPenunjang.Add(dsItem.NMITEM2 & " " & IIf(String.IsNullOrEmpty(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)), "", grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)))
                    ElseIf dsItem.M_ITEM_L3.MEMO = "RADIOLOGI" Then
                        'listTindakanRad.Add(dsItem.NMITEM2 & " " & IIf(String.IsNullOrEmpty(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)), "", grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)))
                        listPlanningTindakanPenunjang.Add(dsItem.NMITEM2 & " " & IIf(String.IsNullOrEmpty(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)), "", grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)))
                    Else
                        If dsItem.KDITEM_L1 = "ITEM_L1_0000000002" Then
                            listPlanningTindakan.Add(dsItem.NMITEM2 & " " & IIf(String.IsNullOrEmpty(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)), "", grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)))
                        End If
                    End If
                End If
            Next

            sPenunjnag = String.Empty

            LoadPenunjang(txtNoRegister.Text)

            If sPenunjnag <> "" Then
                listPlanningTindakanPenunjang.Add(sPenunjnag)
            End If

            For Each xloop In arrDetail
                listObatNonRacik.Add(xloop.NAMAOBAT)
                'listPlanningObat.Add(xloop.NAMAOBAT & " " & xloop.SATUAN & " " & xloop.SIGNA & " " & xloop.CARAPAKAI & " No " & xloop.ROMAWI & " " & xloop.REMARKS_DOKTER)
                listPlanningObat.Add(xloop.NAMAOBAT & " " & xloop.SATUAN & " " & xloop.SIGNA & " " & xloop.CARAPAKAI & " " & xloop.REMARKS_DOKTER)
            Next

            For Each xloop In arrDetailObatRacikan
                listObatRacik.Add(xloop.NAMAOBAT)
                listPlanningObat.Add(xloop.SEQ + 1 & ". " & xloop.NAMAOBAT & " " & xloop.SATUAN & " " & xloop.SIGNA & " " & xloop.PERMINTAAN & " " & xloop.REMARKS)
            Next

            For Each xloop In arrDetailTindakanTerjadwal
                listPlanningTindakanTerjadwal.Add("Tgl " & xloop.TANGGALORDER.ToString("dd-MM-yyy") & " Cek " & xloop.NMITEM2 & IIf(xloop.MEMO = "", "", xloop.MEMO))
            Next

            Dim TindakLanjut As String = IIf(txtTINDAKLANJUT.Text = "", "", "Tindak Lanjut : " & txtTINDAKLANJUT.Text)

            Dim Planning_Text As String = String.Empty
            If listPlanningTindakan.Count > 0 Then
                Planning_Text = Planning_Text & "Tindakan di Poli : " & String.Join(vbCrLf, listPlanningTindakan.ToArray)
            End If
            If listPlanningTindakanPenunjang.Count > 0 Then
                Planning_Text = Planning_Text & vbCrLf & vbCrLf & "Pemeriksaan Penunjang : " & String.Join(vbCrLf, listPlanningTindakanPenunjang.ToArray)
            End If
            If listPlanningObat.Count > 0 Then
                Planning_Text = Planning_Text & vbCrLf & vbCrLf & "Medikamentosa : " & String.Join(vbCrLf, listPlanningObat.ToArray)
            End If

            If TindakLanjut <> "" Then
                Planning_Text = Planning_Text & vbCrLf & vbCrLf & TindakLanjut
            End If

            If listPlanningTindakanTerjadwal.Count > 0 Then
                'Tindak lanjut Terjadwal
                Planning_Text = Planning_Text & vbCrLf & String.Join(vbCrLf, listPlanningTindakanTerjadwal.ToArray)
            End If

            ds.PLANNING_TEXT = Planning_Text.ToString.Trim

            Dim CPPT As Boolean = False

            If isAdd = True Then
                If oGrouperDataCppt.InsertDataAsesmen(ds, arrDetailDiagnosa, arrDetailTindakan, arrDetail, arrDetailObatRacikan, arrDetailTindakanTerjadwal) <> "" Then
                    CPPT = True
                    oGrouperDataCppt.UpdateDaftarL6(txtNoRegister.Text, "DAFTAR_L6_0000000003")
                End If
            Else
                If oGrouperDataCppt.UpdateData(ds, arrDetailDiagnosa, arrDetailTindakan, arrDetail, arrDetailObatRacikan, arrDetailTindakanTerjadwal) = True Then
                    CPPT = True
                End If
            End If

            If CPPT = True Then
                Dim oAdmision As New Admission.clsPendaftaran_Kunjungan
                Dim dsKunjungan = oAdmision.GetData(dsIdentitas.KDKUNJUNGAN)

                If dsKunjungan IsNot Nothing Then
                    If dsKunjungan.S_PENDAFTARAN_H.CATEGORY = 0 Then
                        fn_SaveTranskasiPoli(ds.KDCPPT, dsKunjungan.S_PENDAFTARAN_H.CATEGORY, dsKunjungan.KDKUNJUNGAN, dsKunjungan.KDDOCTOR, dsKunjungan.KDDEPARTMENT)
                    End If
                End If

                Dim oOrder As New Digital.clsR_Order

                If listObatNonRacik.Count > 0 Then
                    Try
                        Dim dsNomorRefrence = oOrder.GetDataNoReference(ds.KDCPPT, "ORDER FARMASI")
                        Dim NoOrder As String = String.Empty
                        Dim StatusOrder As String = String.Empty
                        Dim JenisOrder As String = String.Empty

                        If dsNomorRefrence IsNot Nothing Then
                            NoOrder = dsNomorRefrence.KDORDER
                            If dsNomorRefrence.STATUS = "TAMBAH" Then
                                StatusOrder = "UPDATE"
                            Else
                                StatusOrder = dsNomorRefrence.STATUS
                            End If

                            JenisOrder = dsNomorRefrence.JENISORDER
                        Else
                            Dim AdaObatRacik As Boolean = False

                            If listObatRacik.Count > 0 Then
                                AdaObatRacik = True
                            End If

                            StatusOrder = "TAMBAH"
                            If sCategory = 0 Then
                                If AdaObatRacik = False Then
                                    JenisOrder = "FARRJ"
                                Else
                                    JenisOrder = "RARRJ"
                                End If
                            Else
                                If AdaObatRacik = False Then
                                    JenisOrder = "FARRI"
                                Else
                                    JenisOrder = "RARRI"
                                End If
                            End If
                        End If

                        Dim dsOrder = oOrder.GetStructureHeader
                        With dsOrder
                            .DATECREATED = ds.DATECREATED
                            .DATEUPDATED = ds.DATEUPDATED
                            .TANGGALORDER = ds.DATE
                            .JENISORDER = JenisOrder
                            .KDORDER = NoOrder
                            .KDIDENTITAS = ds.KDIDENTITAS
                            .NOMORREFERENCE = ds.KDCPPT
                            .USERORDER = ds.KDUSER
                            .STATUS = StatusOrder
                            .MEMO = "ORDER FARMASI"
                        End With

                        If chkAutoOrderObat.Checked = True Then
                            If NoOrder = "" Then
                                oOrder.InsertData(dsOrder)
                            Else
                                oOrder.UpdateData(dsOrder)
                            End If
                        End If
                    Catch oErr As Exception
                        MsgBox("Order" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                End If

                'If listObatRacik.Count > 0 Then
                '    Try
                '        Dim dsNomorRefrence = oOrder.GetDataNoReference(ds.KDCPPT, "ORDER FARMASI")
                '        Dim NoOrder As String = String.Empty
                '        Dim StatusOrder As String = String.Empty
                '        Dim JenisOrder As String = String.Empty

                '        If dsNomorRefrence IsNot Nothing Then
                '            NoOrder = dsNomorRefrence.KDORDER
                '            If dsNomorRefrence.STATUS = "TAMBAH" Then
                '                StatusOrder = "UPDATE"
                '            Else
                '                StatusOrder = dsNomorRefrence.STATUS
                '            End If

                '            JenisOrder = dsNomorRefrence.JENISORDER
                '        Else
                '            StatusOrder = "TAMBAH"
                '            If sCategory = 0 Then
                '                JenisOrder = "RARRJ"
                '            Else
                '                JenisOrder = "RARRI"
                '            End If
                '        End If

                '        'If String.Join(", ", listTindakanLab.ToArray) <> String.Join(", ", ListLoadDataLaboratorium.ToArray) Then
                '        '    StatusOrder = "UPDATE"
                '        'End If

                '        Dim dsOrder = oOrder.GetStructureHeader
                '        With dsOrder
                '            .DATECREATED = ds.DATECREATED
                '            .DATEUPDATED = ds.DATEUPDATED
                '            .TANGGALORDER = ds.DATE
                '            .JENISORDER = JenisOrder
                '            .KDORDER = NoOrder
                '            .KDIDENTITAS = ds.KDIDENTITAS
                '            .NOMORREFERENCE = ds.KDCPPT
                '            .USERORDER = ds.KDUSER
                '            .STATUS = StatusOrder
                '            .MEMO = "ORDER FARMASI"
                '        End With

                '        If NoOrder = "" Then
                '            oOrder.InsertData(dsOrder)
                '        Else
                '            oOrder.UpdateData(dsOrder)
                '        End If
                '    Catch oErr As Exception
                '        MsgBox("Order" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                '    End Try
                'End If



                'Dim oItem As New Reference.clsItem
                'Dim oOrder As New Digital.clsR_Order
                Dim listTindakanLab As New List(Of String)
                Dim listTindakanRad As New List(Of String)

                For i As Integer = 0 To grvCPPT_Tindakan.RowCount - 2
                    Dim dsItem = oItem.GetData(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_KDITEMTINDAKAN))
                    If dsItem IsNot Nothing Then
                        If dsItem.M_ITEM_L3.MEMO = "LABORATORIUM" Then
                            listTindakanLab.Add(dsItem.NMITEM2)
                        ElseIf dsItem.M_ITEM_L3.MEMO = "BANK DARAH" Then
                            listTindakanLab.Add(dsItem.NMITEM2)
                        ElseIf dsItem.M_ITEM_L3.MEMO = "RADIOLOGI" Then
                            listTindakanRad.Add(dsItem.NMITEM2)
                        End If
                    End If
                Next

                'If listTindakanLab.Count > 0 Then
                '    Dim UpddateLab As Boolean = False
                '    Dim TambahHasil1 = String.Join(", ", listTindakanLab.ToArray)
                '    Dim TambahHasil2 = String.Join(", ", listTindakanLab_LoadData.ToArray)

                '    For Each yloop In listTindakanLab_LoadData
                '        If Not TambahHasil1.Contains(yloop) Then
                '            UpddateLab = True
                '        End If
                '    Next

                '    For Each yloop In listTindakanLab
                '        If Not TambahHasil2.Contains(yloop) Then
                '            UpddateLab = True
                '        End If
                '    Next

                '    Dim dsNomorRefrenceLab = oOrder.GetDataNoReference(ds.KDCPPT, "ORDER LABORATORIUM")
                '    If dsNomorRefrenceLab Is Nothing Then
                '        'Add

                '        Dim dsOrder = oOrder.GetStructureHeader
                '        With dsOrder
                '            .DATECREATED = ds.DATECREATED
                '            .DATEUPDATED = ds.DATEUPDATED
                '            .TANGGALORDER = ds.DATE
                '            .JENISORDER = IIf(sCategory = 0, "LABRJ", "LABRI")
                '            .KDORDER = ""
                '            .KDIDENTITAS = ds.KDIDENTITAS
                '            .NOMORREFERENCE = ds.KDCPPT
                '            .USERORDER = ds.KDUSER
                '            .STATUS = "TAMBAH"
                '            .MEMO = "ORDER LABORATORIUM"
                '        End With

                '        oOrder.InsertData(dsOrder)
                '    Else
                '        'Update

                '        If UpddateLab = True Then
                '            Dim dsOrder = oOrder.GetStructureHeader
                '            With dsOrder
                '                .DATECREATED = ds.DATECREATED
                '                .DATEUPDATED = ds.DATEUPDATED
                '                .TANGGALORDER = ds.DATE
                '                .JENISORDER = IIf(sCategory = 0, "LABRJ", "LABRI")
                '                .KDORDER = dsNomorRefrenceLab.KDORDER
                '                .KDIDENTITAS = ds.KDIDENTITAS
                '                .NOMORREFERENCE = ds.KDCPPT
                '                .USERORDER = ds.KDUSER
                '                .STATUS = "UPDATE"
                '                .MEMO = "ORDER LABORATORIUM"
                '            End With

                '            oOrder.UpdateData(dsOrder)
                '        Else
                '            If dsNomorRefrenceLab.STATUS = "DELETE" Then
                '                Dim dsOrder = oOrder.GetStructureHeader
                '                With dsOrder
                '                    .DATECREATED = ds.DATECREATED
                '                    .DATEUPDATED = ds.DATEUPDATED
                '                    .TANGGALORDER = ds.DATE
                '                    .JENISORDER = IIf(sCategory = 0, "LABRJ", "LABRI")
                '                    .KDORDER = dsNomorRefrenceLab.KDORDER
                '                    .KDIDENTITAS = ds.KDIDENTITAS
                '                    .NOMORREFERENCE = ds.KDCPPT
                '                    .USERORDER = ds.KDUSER
                '                    .STATUS = "TAMBAH"
                '                    .MEMO = "ORDER LABORATORIUM"
                '                End With

                '                oOrder.UpdateData(dsOrder)
                '            End If
                '        End If
                '    End If
                'Else
                '    Dim dsNomorRefrenceLab = oOrder.GetDataNoReference(ds.KDCPPT, "ORDER LABORATORIUM")
                '    If dsNomorRefrenceLab IsNot Nothing Then
                '        'delete
                '        Dim dsOrder = oOrder.GetStructureHeader
                '        With dsOrder
                '            .DATECREATED = ds.DATECREATED
                '            .DATEUPDATED = ds.DATEUPDATED
                '            .TANGGALORDER = ds.DATE
                '            .JENISORDER = IIf(sCategory = 0, "LABRJ", "LABRI")
                '            .KDORDER = dsNomorRefrenceLab.KDORDER
                '            .KDIDENTITAS = ds.KDIDENTITAS
                '            .NOMORREFERENCE = ds.KDCPPT
                '            .USERORDER = ds.KDUSER
                '            .STATUS = "DELETE"
                '            .MEMO = "ORDER LABORATORIUM"
                '        End With

                '        oOrder.UpdateData(dsOrder)
                '    End If
                'End If

                'If listTindakanRad.Count > 0 Then
                '    Dim UpddateLab As Boolean = False
                '    Dim TambahHasil1 = String.Join(", ", listTindakanRad.ToArray)
                '    Dim TambahHasil2 = String.Join(", ", listTindakanRad_LoadData.ToArray)

                '    For Each yloop In listTindakanRad_LoadData
                '        If Not TambahHasil1.Contains(yloop) Then
                '            UpddateLab = True
                '        End If
                '    Next

                '    For Each yloop In listTindakanRad
                '        If Not TambahHasil2.Contains(yloop) Then
                '            UpddateLab = True
                '        End If
                '    Next

                '    Dim dsNomorRefrenceRad = oOrder.GetDataNoReference(ds.KDCPPT, "ORDER RADIOLOGI")
                '    If dsNomorRefrenceRad Is Nothing Then
                '        'Add

                '        Dim dsOrder = oOrder.GetStructureHeader
                '        With dsOrder
                '            .DATECREATED = ds.DATECREATED
                '            .DATEUPDATED = ds.DATEUPDATED
                '            .TANGGALORDER = ds.DATE
                '            .JENISORDER = IIf(sCategory = 0, "RADRJ", "RADRI")
                '            .KDORDER = ""
                '            .KDIDENTITAS = ds.KDIDENTITAS
                '            .NOMORREFERENCE = ds.KDCPPT
                '            .USERORDER = ds.KDUSER
                '            .STATUS = "TAMBAH"
                '            .MEMO = "ORDER RADIOLOGI"
                '        End With

                '        oOrder.InsertData(dsOrder)
                '    Else
                '        'Update

                '        If UpddateLab = True Then
                '            Dim dsOrder = oOrder.GetStructureHeader
                '            With dsOrder
                '                .DATECREATED = ds.DATECREATED
                '                .DATEUPDATED = ds.DATEUPDATED
                '                .TANGGALORDER = ds.DATE
                '                .JENISORDER = IIf(sCategory = 0, "RADRJ", "RADRI")
                '                .KDORDER = dsNomorRefrenceRad.KDORDER
                '                .KDIDENTITAS = ds.KDIDENTITAS
                '                .NOMORREFERENCE = ds.KDCPPT
                '                .USERORDER = ds.KDUSER
                '                .STATUS = "UPDATE"
                '                .MEMO = "ORDER RADIOLOGI"
                '            End With

                '            oOrder.UpdateData(dsOrder)
                '        Else
                '            If dsNomorRefrenceRad.STATUS = "DELETE" Then
                '                Dim dsOrder = oOrder.GetStructureHeader
                '                With dsOrder
                '                    .DATECREATED = ds.DATECREATED
                '                    .DATEUPDATED = ds.DATEUPDATED
                '                    .TANGGALORDER = ds.DATE
                '                    .JENISORDER = IIf(sCategory = 0, "RADRJ", "RADRI")
                '                    .KDORDER = dsNomorRefrenceRad.KDORDER
                '                    .KDIDENTITAS = ds.KDIDENTITAS
                '                    .NOMORREFERENCE = ds.KDCPPT
                '                    .USERORDER = ds.KDUSER
                '                    .STATUS = "TAMBAH"
                '                    .MEMO = "ORDER RADIOLOGI"
                '                End With

                '                oOrder.UpdateData(dsOrder)
                '            End If
                '        End If
                '    End If
                'Else
                '    Dim dsNomorRefrenceRad = oOrder.GetDataNoReference(ds.KDCPPT, "ORDER RADIOLOGI")
                '    If dsNomorRefrenceRad IsNot Nothing Then
                '        'delete
                '        Dim dsOrder = oOrder.GetStructureHeader
                '        With dsOrder
                '            .DATECREATED = ds.DATECREATED
                '            .DATEUPDATED = ds.DATEUPDATED
                '            .TANGGALORDER = ds.DATE
                '            .JENISORDER = IIf(sCategory = 0, "RADRJ", "RADRI")
                '            .KDORDER = dsNomorRefrenceRad.KDORDER
                '            .KDIDENTITAS = ds.KDIDENTITAS
                '            .NOMORREFERENCE = ds.KDCPPT
                '            .USERORDER = ds.KDUSER
                '            .STATUS = "DELETE"
                '            .MEMO = "ORDER RADIOLOGI"
                '        End With

                '        oOrder.UpdateData(dsOrder)
                '    End If
                'End If
            End If

            'If fn_Save = True Then
            'oGrouperDataCppt.UpdateDaftarL6(txtNoRegister.Text, "DAFTAR_L6_0000000003")

            '    Dim SuaraFarmasi As Boolean = False

            '    If arrDetail.Count > 0 Then
            '        SuaraFarmasi = True
            '    End If
            '    If arrDetailObatRacikan.Count > 0 Then
            '        SuaraFarmasi = True
            '    End If

            '    If SuaraFarmasi = True Then
            '        Dim oSuara As New Digital.clsSuara
            '        oSuara.DeleteData(0)

            '        Dim dsOrder = oSuara.GetStructureHeader
            '        With dsOrder
            '            .DATECREATED = ds.DATECREATED
            '            .DATEUPDATED = ds.DATEUPDATED
            '            .KDSUARA = 0
            '            .MEMO = "SUARA FARMASI"
            '            .ISCHEKED = False
            '            .KDUSER = sUserID
            '        End With
            '    End If
            'End If
        Catch oErr As Exception
            fn_Save = False
            MsgBox("Simpan CPPT" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_SaveTranskasiPoli(ByVal sNoId As String, ByVal sCategoryBilling As Integer, ByVal KDKUNJUNGAN As String, ByVal KDDOCTOR As String, ByVal KDDEPARTMENT As String) As Boolean
        Try
            ' ***** HEADER *****
            Dim oSalesOrderTransaksi As New Sales.clsSalesOrderTransaksi
            Dim oItem As New Reference.clsItem
            Dim Total As Decimal = 0

            For i As Integer = 0 To grvCPPT_Tindakan.RowCount - 2
                Dim dsItem = oItem.GetData(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_KDITEMTINDAKAN))
                If dsItem IsNot Nothing Then
                    If dsItem.M_ITEM_L2.MEMO.Contains("RADIOLOGI") Then

                    ElseIf dsItem.M_ITEM_L2.MEMO.Contains("LABORATORIUM") Then

                    ElseIf dsItem.M_ITEM_L2.MEMO.Contains("BANK DARAH") Then

                    Else
                        Total += CDec(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_TOTALTINDAKAN))
                    End If
                End If
            Next

            Dim ds = oSalesOrderTransaksi.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oSalesOrderTransaksi.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDSOTRANSAKSI = sNoId
                .CATEGORY = sCategoryBilling
                .DATE = deDATE.DateTime
                .KDKUNJUNGAN = KDKUNJUNGAN
                .KDWAREHOUSE = ""
                .ISBHP = False
                .SUBTOTAL = CDec(Total)
                .DISCOUNT = CDec(0)
                .TAX = CDec(0)
                .GRANDTOTAL = CDec(Total)
                .PAYAMOUNT = CDec(0)
                .MEMO = "AUTO"
                Try
                    .KDUSER = oSalesOrderTransaksi.GetData(sNoId).KDUSER
                Catch ex As Exception
                    .KDUSER = sUserID
                End Try
                '.KDSHIFT = sSHIFT
                .KDSHIFT = sSHIFT
                .KDDOCTOR = KDDOCTOR
                .TUSLAH = CDec(0)

                Try
                    .KDCPPT = oSalesOrderTransaksi.GetData(sNoId).KDCPPT
                Catch ex As Exception
                    .KDCPPT = ""
                End Try
                Try
                    .KDORDER = oSalesOrderTransaksi.GetData(sNoId).KDORDER
                Catch ex As Exception
                    .KDORDER = ""
                End Try
            End With

            ' ***** DETIL *****
            Dim arrDetail = oSalesOrderTransaksi.GetStructureDetailList
            For i As Integer = 0 To grvCPPT_Tindakan.RowCount - 2
                Dim dsDetail = oSalesOrderTransaksi.GetStructureDetail
                With dsDetail
                    .DATECREATED = deDATE.DateTime
                    .DATEUPDATED = Now
                    .SEQ = i
                    .KDSOTRANSAKSI = ds.KDSOTRANSAKSI
                    .KDITEM = grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_KDITEMTINDAKAN)
                    .KDUOM = grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_KDUOMTINDAKAN)
                    .KDCARAPAKAI = oItem.DefaultItem_CaraPakai
                    .KDSIGNA = oItem.DefaultItem_Signa
                    .QTY = CDec(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_JUMLAHTINDAKAN))
                    .ISRACIK = CBool(False)
                    .PRICE = CDec(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_HARGATINDAKAN))
                    .SUBTOTAL = CDec(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_TOTALTINDAKAN))
                    .DISCOUNT = CDec(0)
                    .GRANDTOTAL = CDec(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_TOTALTINDAKAN))
                    .KDDOCTOR = KDDOCTOR
                    .KDDEPARTMENT = KDDEPARTMENT
                    .REMARKS = IIf(String.IsNullOrEmpty(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)), "-", grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN))
                    .ISCETAKETIKET = False
                    .KDUSER = sUserID
                    .GROUPRACIK = 0
                End With

                Dim dsItem = oItem.GetData(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_KDITEMTINDAKAN))
                If dsItem IsNot Nothing Then
                    If dsItem.M_ITEM_L3.MEMO = "RADIOLOGI" Then
                    ElseIf dsItem.M_ITEM_L3.MEMO = "LABORATORIUM" Then
                    ElseIf dsItem.M_ITEM_L3.MEMO = "BANK DARAH" Then
                    Else
                        arrDetail.Add(dsDetail)
                    End If
                End If
            Next

            If arrDetail.Count > 0 Then
                Dim dsCek = oSalesOrderTransaksi.GetData(sNoId)

                If dsCek Is Nothing Then
                    Try
                        Dim sKDSOTRANSAKSI = oSalesOrderTransaksi.InsertData(ds, arrDetail)
                        If sKDSOTRANSAKSI = "" Then
                            fn_SaveTranskasiPoli = False
                        Else
                            fn_SaveTranskasiPoli = True
                        End If
                    Catch oErr As Exception
                        MsgBox("Simpan Billing" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                Else
                    Try
                        fn_SaveTranskasiPoli = oSalesOrderTransaksi.UpdateData(ds, arrDetail)
                    Catch oErr As Exception
                        MsgBox("Simpan Billing" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                End If
            End If

        Catch oErr As Exception
            MsgBox("Simpan Billing" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveTranskasiPoli = False
        End Try
    End Function
    Private Function ByteArrayToImage(ByVal byteArrayIn() As Byte) As Image
        Using ms As New System.IO.MemoryStream(byteArrayIn)
            Dim returnImage = Image.FromStream(ms)
            Return returnImage
        End Using
    End Function
    Private Sub fn_LoadAdsemenPerawat()
        Dim oDigital As New Digital.clsDigital_IGD_02
        Dim dsDigital = oDigital.GetDataPendaftaran(txtNoRegister.Text)
        If dsDigital IsNot Nothing Then
            'If txtRIWAYAT_PENYAKITDAHULU.Text = "" Then
            '    txtRIWAYAT_PENYAKITDAHULU.Text = dsDigital.TEXT_6
            'End If
            txtKELUHANUTAMA.Text = dsDigital.TEXT_6
            cboSAATPULANG_TINGKATKESDARAN.Text = dsDigital.KESADARAN_UMUM

            chkTRIAGE_DATANGSENDIRI.Checked = dsDigital.BIT_4
            chkTRIAGE_RED.Checked = dsDigital.BIT_6
            chkTRIAGE_YELLOW.Checked = dsDigital.BIT_7
            chkTRIAGE_GREEN.Checked = dsDigital.BIT_8
            chkTRIAGE_BLACK.Checked = dsDigital.BIT_9
            'txtPERAWAT.Text = dsDigital.KDUSER
            txtPERAWAT.Text = dsDigital.KDUSER
            cboTRIAGE_AIRWAY.Text = dsDigital.TEXT_3
            cboTRIAGE_BREATHING.Text = dsDigital.TEXT_4
            cboTRIAGE_CIRCULATION.Text = dsDigital.TEXT_5
            'If txtRIWAYAT.Text = "" Then
            '    txtRIWAYAT.Text = dsDigital.TEXT_97
            'End If
            'txtGCS.Text = dsDigital.TEXT_63
            'If txtGCS.Text = "" Then
            '    cboE.Text = dsDigital.TEXT_64
            '    cboM.Text = dsDigital.TEXT_65
            '    cboV.Text = dsDigital.TEXT_66
            '    cboSAATPULANG_E.Text = dsDigital.TEXT_64
            '    cboSAATPULANG_M.Text = dsDigital.TEXT_65
            '    cboSAATPULANG_V.Text = dsDigital.TEXT_66
            'End If

            chkRESIKO_1.Checked = dsDigital.BIT_245
            chkRESIKO_2.Checked = dsDigital.BIT_244

            cboE.Text = dsDigital.TEXT_64
            cboM.Text = dsDigital.TEXT_65
            cboV.Text = dsDigital.TEXT_66

            txtBP.Text = dsDigital.TANDA_VITAL_01
            txtHR.Text = dsDigital.TANDA_VITAL_02
            txtT.Text = dsDigital.TANDA_VITAL_03
            txtRR.Text = dsDigital.TANDA_VITAL_04
            txtSPO2.Text = dsDigital.TANDA_VITAL_09

            cboSAATPULANG_E.Text = dsDigital.TEXT_64
            cboSAATPULANG_M.Text = dsDigital.TEXT_65
            cboSAATPULANG_V.Text = dsDigital.TEXT_66
            txtGCS.Text = dsDigital.TEXT_63

            txtSAATPULANG_BP.Text = dsDigital.TANDA_VITAL_01
            txtSAATPULANG_HR.Text = dsDigital.TANDA_VITAL_02
            txtSAATPULANG_T.Text = dsDigital.TANDA_VITAL_03
            txtSAATPULANG_RR.Text = dsDigital.TANDA_VITAL_04
            txtSAATPULANG_SPO2.Text = dsDigital.TANDA_VITAL_09

            cboTINGKATKESADARAN.Text = dsDigital.KESADARAN_UMUM
            txtKEADAANUMUM.Text = dsDigital.KEADAAN
            txtBERATBADAN.Text = dsDigital.TANDA_VITAL_05
            txtTINGGIBADAN.Text = dsDigital.TANDA_VITAL_06

            If dsDigital.TEXT_84 <> "" Then
                If dsDigital.TEXT_84 = 0 Then
                    chkSKALANYERI_00.Checked = True
                ElseIf dsDigital.TEXT_84 = 1 Then
                    chkSKALANYERI_01.Checked = True
                ElseIf dsDigital.TEXT_84 = 2 Then
                    chkSKALANYERI_02.Checked = True
                ElseIf dsDigital.TEXT_84 = 3 Then
                    chkSKALANYERI_03.Checked = True
                ElseIf dsDigital.TEXT_84 = 4 Then
                    chkSKALANYERI_04.Checked = True
                ElseIf dsDigital.TEXT_84 = 5 Then
                    chkSKALANYERI_05.Checked = True
                ElseIf dsDigital.TEXT_84 = 6 Then
                    chkSKALANYERI_06.Checked = True
                ElseIf dsDigital.TEXT_84 = 7 Then
                    chkSKALANYERI_07.Checked = True
                ElseIf dsDigital.TEXT_84 = 8 Then
                    chkSKALANYERI_08.Checked = True
                ElseIf dsDigital.TEXT_84 = 9 Then
                    chkSKALANYERI_09.Checked = True
                ElseIf dsDigital.TEXT_84 = 10 Then
                    chkSKALANYERI_10.Checked = True
                End If
            End If
        Else
            MsgBox("Asesmen Perawat belum di input", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub fn_LoadDataReloadIGD()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            Dim Tanggal As DateTime = deDATE.DateTime.AddDays(-1)

            SQL = "SELECT "
            SQL &= "KATEGORI = 'ASEMEN AWAL PERAWAT IGD' "
            SQL &= ",KDAWALASESMENIGD = A.KODE "
            SQL &= ",NAMAPASIEN = ISNULL((SELECT NAME_DISPLAY FROM DATABASERS..M_CUSTOMER WHERE A.KDCUSTOMER = KDCUSTOMER), '') "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "DATABASERME..S_DIGITAL_IGD_01 A "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & Tanggal.ToString("yyyyMMdd") & "' "
            'SQL &= "AND A.KDPENDAFTARAN = '' "
            SQL &= "ORDER BY A.DATE DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

            grdReloadIGD.Properties.DataSource = ds.Tables("ALL")
            grdReloadIGD.Properties.ValueMember = "KDAWALASESMENIGD"
            grdReloadIGD.Properties.DisplayMember = "NAMAPASIEN"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDataReloadIGDBidanPonek()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            Dim Tanggal As DateTime = deDATE.DateTime.AddDays(-1)

            SQL = "SELECT "
            SQL &= "KATEGORI = 'KEBIDANAN PONEK' "
            SQL &= ",KDAWALASESMENIGD = A.KDASESMEN "
            SQL &= ",NAMAPASIEN = ISNULL((SELECT NAME_DISPLAY FROM DATABASERS..M_CUSTOMER WHERE A.KDCUSTOMER = KDCUSTOMER), '') "
            SQL &= ",A.KDUSER "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= "FROM "
            SQL &= "DATABASERME..S_DIGITAL_IGD_03 A "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & Tanggal.ToString("yyyyMMdd") & "' "
            'SQL &= "WHERE A.KDCUSTOMER = '" & sKDCUSTOMER & "' "
            SQL &= "ORDER BY A.DATE DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

            grdReloadIGD.Properties.DataSource = ds.Tables("ALL")
            grdReloadIGD.Properties.ValueMember = "KDAWALASESMENIGD"
            grdReloadIGD.Properties.DisplayMember = "NAMAPASIEN"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_UpdateDataReloadIGD(ByVal Parameter As String)
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "UPDATE S_DIGITAL_IGD_01_AWAL SET KDPENDAFTARAN = '" & txtNoRegister.Text & "' WHERE KDAWALASESMENIGD = '" & Parameter & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "UPDATEALL")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
#Region "Command Button"
    Private Function IntegerToRoman(IntNumberValue As Integer) As String
        Dim RomanNumbers As New Dictionary(Of String, Integer)()
        RomanNumbers.Add("M", 1000)
        RomanNumbers.Add("CM", 900)
        RomanNumbers.Add("D", 500)
        RomanNumbers.Add("CD", 400)
        RomanNumbers.Add("C", 100)
        RomanNumbers.Add("XC", 90)
        RomanNumbers.Add("L", 50)
        RomanNumbers.Add("XL", 40)
        RomanNumbers.Add("X", 10)
        RomanNumbers.Add("IX", 9)
        RomanNumbers.Add("V", 5)
        RomanNumbers.Add("IV", 4)
        RomanNumbers.Add("I", 1)

        Dim result As String = ""

        For Each pair As KeyValuePair(Of String, Integer) In RomanNumbers
            While IntNumberValue >= pair.Value
                IntNumberValue -= pair.Value
                result += pair.Key
            End While
        Next
        Return result
    End Function
    Private Sub OnValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grvDetailResep.FocusedRowChanged, grvObatIGD.FocusedRowChanged, grvObatRanap.FocusedRowChanged
        If isLoad Then
            Calculate()
        End If
    End Sub
    Private Sub Calculate()
        Dim sSubTotal = 0
        Dim list As New List(Of String)

        For i As Integer = 0 To grvDetailResep.RowCount - 2
            sSubTotal += CDec(grvDetailResep.GetRowCellValue(i, colGRANDTOTAL))
        Next
        For i As Integer = 0 To grvObatIGD.RowCount - 2
            sSubTotal += CDec(grvObatIGD.GetRowCellValue(i, colGRANDTOTAL_ObatIGD))
        Next
        For i As Integer = 0 To grvObatRanap.RowCount - 2
            sSubTotal += CDec(grvObatRanap.GetRowCellValue(i, colGRANDTOTAL_ObatRanap))
        Next

        txtGRANDTOTAL.Text = sSubTotal
        'txtOBATSAATPULANG.Text = String.Join(", ", list.ToArray)
    End Sub
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        'Select Case e.KeyCode
        '    Case Keys.F12
        '        btnClose_Click()
        '    Case Keys.F3
        '        If btnSaveClose.Enabled = True Then
        '            btnSaveClose_Click()
        '        End If
        'End Select
    End Sub
    Private Sub btnTransferInternal_Click() Handles btnTransferInternal.ItemClick
        Dim oTransferInternal As New Transaksi.clsTransferInternal

        Dim dsCek = oTransferInternal.GetDataByregister(txtNoRegister.Text)
        If dsCek Is Nothing Then
            Dim frmTransferInternal As New frmTransferInternal
            Try
                frmTransferInternal.LoadMe(FORM_MODE.FORM_MODE_ADD, "", txtNoRegister.Text, grdKDDOCTOR.EditValue, sKDCUSTOMER, sNAMAPASIEN, "")
                frmTransferInternal.ShowDialog(Me)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmTransferInternal Is Nothing Then frmTransferInternal.Dispose()
                frmTransferInternal = Nothing
            End Try
        Else
            Dim frmTransferInternal As New frmTransferInternal
            Try
                frmTransferInternal.LoadMe(FORM_MODE.FORM_MODE_EDIT, "", txtNoRegister.Text, grdKDDOCTOR.EditValue, sKDCUSTOMER, sNAMAPASIEN, dsCek.KDTRANSFERINTERNAL)
                frmTransferInternal.ShowDialog(Me)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmTransferInternal Is Nothing Then frmTransferInternal.Dispose()
                frmTransferInternal = Nothing
            End Try
        End If
    End Sub
    Private Sub btnSaveClosee_Click() Handles btnSaveClosee.ItemClick
        If fn_Validate() = False Then Exit Sub
        If txtTINDAKLANJUT.Text = "" Then
            If MsgBox("Save Tanpa TINDAK LANJUT dengan Register " & txtNoRegister.Text & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        Else
            If MsgBox("Save " & txtNoRegister.Text & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        End If
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            oFormMode = FORM_MODE.FORM_MODE_EDIT
            MsgBox("Save " & txtNoRegister.Text & " success!", MsgBoxStyle.Information, Me.Text)
            'Me.Close()
        End If
    End Sub
    Private Sub btnTemplateObat_Click() Handles btnTemplateObat.ItemClick
        Dim frmTemplateNonRacikanList As New frmTemplateNonRacikanList
        Try
            frmTemplateNonRacikanList.fn_LoadRacikan(False)
            frmTemplateNonRacikanList.ShowDialog(Me)
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmTemplateNonRacikanList Is Nothing Then frmTemplateNonRacikanList.Dispose()
            frmTemplateNonRacikanList = Nothing

            fn_LoadTEMPLATE()
        End Try
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        'Me.Close()
    End Sub
    Private Sub chkTRIAGE_TRAUMA_CheckedChanged(sender As Object, e As EventArgs) Handles chkTRIAGE_TRAUMA.CheckedChanged
        If chkTRIAGE_TRAUMA.Checked = True Then
            chkTRIAGE_NONTRAUMA.Checked = False
            chkTRIAGE_MATERNITY.Checked = False
        End If
    End Sub
    Private Sub chkTRIAGE_NONTRAUMA_CheckedChanged(sender As Object, e As EventArgs) Handles chkTRIAGE_NONTRAUMA.CheckedChanged
        If chkTRIAGE_NONTRAUMA.Checked = True Then
            chkTRIAGE_TRAUMA.Checked = False
            chkTRIAGE_MATERNITY.Checked = False
        End If
    End Sub
    Private Sub chkTRIAGE_MATERNITY_CheckedChanged(sender As Object, e As EventArgs) Handles chkTRIAGE_MATERNITY.CheckedChanged
        If chkTRIAGE_MATERNITY.Checked = True Then
            chkTRIAGE_TRAUMA.Checked = False
            chkTRIAGE_NONTRAUMA.Checked = False
        End If
    End Sub
    Private Sub chkTRIAGE_RED_CheckedChanged(sender As Object, e As EventArgs) Handles chkTRIAGE_RED.CheckedChanged
        If chkTRIAGE_RED.Checked = True Then
            chkTRIAGE_YELLOW.Checked = False
            chkTRIAGE_GREEN.Checked = False
            chkTRIAGE_BLACK.Checked = False
        End If
    End Sub
    Private Sub chkTRIAGE_YELLOW_CheckedChanged(sender As Object, e As EventArgs) Handles chkTRIAGE_YELLOW.CheckedChanged
        If chkTRIAGE_YELLOW.Checked = True Then
            chkTRIAGE_RED.Checked = False
            chkTRIAGE_GREEN.Checked = False
            chkTRIAGE_BLACK.Checked = False
        End If
    End Sub
    Private Sub chkTRIAGE_GREEN_CheckedChanged(sender As Object, e As EventArgs) Handles chkTRIAGE_GREEN.CheckedChanged
        If chkTRIAGE_GREEN.Checked = True Then
            chkTRIAGE_RED.Checked = False
            chkTRIAGE_YELLOW.Checked = False
            chkTRIAGE_BLACK.Checked = False
        End If
    End Sub
    Private Sub chkTRIAGE_BLACK_CheckedChanged(sender As Object, e As EventArgs) Handles chkTRIAGE_BLACK.CheckedChanged
        If chkTRIAGE_BLACK.Checked = True Then
            chkTRIAGE_RED.Checked = False
            chkTRIAGE_YELLOW.Checked = False
            chkTRIAGE_GREEN.Checked = False
        End If
    End Sub
    Private Sub chkTRIAGE_DATANGSENDIRI_CheckedChanged(sender As Object, e As EventArgs) Handles chkTRIAGE_DATANGSENDIRI.CheckedChanged
        If chkTRIAGE_DATANGSENDIRI.Checked = True Then
            chkTRIAGE_DIANTAR.Checked = False
            cboTRIAGE_DIANTAR.SelectedIndex = 0
        End If
    End Sub
    Private Sub chkTRIAGE_DIANTAR_CheckedChanged(sender As Object, e As EventArgs) Handles chkTRIAGE_DIANTAR.CheckedChanged
        If chkTRIAGE_DIANTAR.Checked = True Then
            chkTRIAGE_DATANGSENDIRI.Checked = False
        End If
    End Sub
    Private Sub chkAUTOANAMNESA_CheckedChanged(sender As Object, e As EventArgs) Handles chkAUTOANAMNESA.CheckedChanged
        If chkAUTOANAMNESA.Checked = True Then
            chkALLOANAMNESA.Checked = False
        End If
    End Sub
    Private Sub chkALLOANAMNESA_CheckedChanged(sender As Object, e As EventArgs) Handles chkALLOANAMNESA.CheckedChanged
        If chkALLOANAMNESA.Checked = True Then
            chkAUTOANAMNESA.Checked = False
        End If
    End Sub
    Private Sub chkSKALANYERI_01_Click(sender As Object, e As EventArgs) Handles chkSKALANYERI_00.Click, chkSKALANYERI_01.Click, chkSKALANYERI_02.Click, chkSKALANYERI_03.Click, chkSKALANYERI_04.Click, chkSKALANYERI_05.Click, chkSKALANYERI_06.Click, chkSKALANYERI_07.Click, chkSKALANYERI_08.Click, chkSKALANYERI_09.Click, chkSKALANYERI_10.Click
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
    End Sub
    Private Sub chkRISIKO_Click(sender As Object, e As EventArgs) Handles chkRESIKO_1.Click, chkRESIKO_2.Click
        chkRESIKO_1.Checked = False
        chkRESIKO_2.Checked = False
    End Sub
    Private Sub chkFUNGSIONAL_Click(sender As Object, e As EventArgs) Handles chkFUNGSIONAL_1.Click, chkFUNGSIONAL_2.Click, chkFUNGSIONAL_3.Click
        chkFUNGSIONAL_1.Checked = False
        chkFUNGSIONAL_2.Checked = False
        chkFUNGSIONAL_3.Checked = False
    End Sub
    Private Sub picGAMBAR2_MouseDown(sender As Object, e As MouseEventArgs) Handles picGAMBAR2.MouseDown
        down = True
    End Sub
    Private Sub picGAMBAR2_MouseUp(sender As Object, e As MouseEventArgs) Handles picGAMBAR2.MouseUp
        down = False
    End Sub
    Private Sub picGAMBAR2_MouseMove(sender As Object, e As MouseEventArgs) Handles picGAMBAR2.MouseMove
        If down = True Then
            Dim ImageToDrawOn As Image
            Dim g As Graphics
            Dim Brush1 As New SolidBrush(Color.Black)
            g = picGAMBAR2.CreateGraphics()
            ImageToDrawOn = picGAMBAR2.Image
            g = Graphics.FromImage(ImageToDrawOn)
            g.FillEllipse(Brush1, e.X, e.Y, 5, 5)
            picGAMBAR2.Image = ImageToDrawOn
            g.Dispose()
            Brush1.Dispose()
        End If

    End Sub
    Private Sub ResetGambarToolStripMenuItem_Click(sender As Object, e As EventArgs)
        picGAMBAR2.Image = CType(My.Resources.ResourceManager.GetObject("gambar_2"), Image)
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadKDITEMSearch()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If


            SQL = "SELECT "
            SQL &= "A.KDITEM "
            'SQL &= ",NMITEM2 = (SELECT CASE D.MEMO WHEN 'NON KELAS' THEN A.NMITEM2 ELSE A.NMITEM2 + ' ' + D.MEMO END) "
            SQL &= ",A.NMITEM2 "
            SQL &= ",KELOMPOK = B.MEMO "
            'SQL &= ",PRICE = C.PRICESALESSTANDARD "
            SQL &= "FROM "
            SQL &= "M_ITEM A "
            SQL &= "INNER JOIN M_ITEM_L2 B "
            SQL &= "ON A.KDITEM_L2 = B.KDITEM_L2 "
            'SQL &= "INNER JOIN M_ITEM_UOM C "
            'SQL &= "ON A.KDITEM = C.KDITEM "
            'SQL &= "INNER JOIN M_UOM D "
            'SQL &= "ON C.KDUOM = D.KDUOM "
            SQL &= "WHERE A.ISSTOK = 0 "
            SQL &= "AND A.ISACTIVE = 1 "
            SQL &= "AND B.MEMO NOT IN ('LABORATORIUM', 'BANK DARAH', 'RADIOLOGI')  "

            'If sHargaApotik = False Then
            '    SQL &= "AND B.MEMO IN ('LABORATORIUM', 'BANK DARAH', 'RADIOLOGI', 'PENUNJANG', 'POLIKINIK', 'NON KATEGORI')  "
            'End If

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ITEM_ALL")

            'grdCPPT_KDITEMTINDAKAN.DataSource = ds.Tables("ITEM_ALL")
            'grdCPPT_KDITEMTINDAKAN.ValueMember = "KDITEM"
            'grdCPPT_KDITEMTINDAKAN.DisplayMember = "NMITEM2"

            grdTindakanHariIni.DataSource = ds.Tables("ITEM_ALL")
            grdTindakanHariIni.ValueMember = "KDITEM"
            grdTindakanHariIni.DisplayMember = "NMITEM2"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox("Load Tindakan" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grvDIAGNOSA_CPPT_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDIAGNOSA_CPPT.CellValueChanged
        If e.Column.Name = colNAMADIAGNOSA_CPPT.Name Then
            Dim CEK As Boolean = False

            For i As Integer = 0 To grvDIAGNOSA_CPPT.RowCount - 2
                If grvDIAGNOSA_CPPT.GetRowCellValue(i, colKATEGORI_CPPT) = "Primary" Then
                    CEK = True
                End If
            Next
            ' grvDIAGNOSA_CPPT.SetFocusedRowCellValue(colKATEGORI_CPPT, IIf(sCek = 0, "Primary", "Secondary"))
            If CEK = False Then
                grvDIAGNOSA_CPPT.SetFocusedRowCellValue(colKATEGORI_CPPT, "Primary")
            Else
                grvDIAGNOSA_CPPT.SetFocusedRowCellValue(colKATEGORI_CPPT, "Secondary")
            End If
        End If
    End Sub
    Private Sub fn_LoadCARAPAKAI()
        Dim oCaraPakai As New Reference.clsCaraPakai
        Try
            Dim dsList = oCaraPakai.GetData.Where(Function(x) x.ISACTIVE = True).ToList()

            grdCARAPAKAI.DataSource = dsList
            grdCARAPAKAI.ValueMember = "KDCARAPAKAI"
            grdCARAPAKAI.DisplayMember = "MEMO"

            grdCARAPAKAI_ObatIGD.DataSource = dsList
            grdCARAPAKAI_ObatIGD.ValueMember = "KDCARAPAKAI"
            grdCARAPAKAI_ObatIGD.DisplayMember = "MEMO"

            grdKDCARAPAKAI_ObatRanap.DataSource = dsList
            grdKDCARAPAKAI_ObatRanap.ValueMember = "KDCARAPAKAI"
            grdKDCARAPAKAI_ObatRanap.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox("Load Keterangan 2 Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadSIGNA()
        Dim oSigna As New Reference.clsSigna
        Try
            Dim dsList = oSigna.GetData.Where(Function(x) x.ISACTIVE = True).ToList()

            grdKDSIGNA.DataSource = dsList
            grdKDSIGNA.ValueMember = "KDSIGNA"
            grdKDSIGNA.DisplayMember = "MEMO"

            grdKDSIGNA_ObatIGD.DataSource = dsList
            grdKDSIGNA_ObatIGD.ValueMember = "KDSIGNA"
            grdKDSIGNA_ObatIGD.DisplayMember = "MEMO"

            grdKDSIGNA_ObatRanap.DataSource = dsList
            grdKDSIGNA_ObatRanap.ValueMember = "KDSIGNA"
            grdKDSIGNA_ObatRanap.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox("Load Signa Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadITEM()
        Dim oItem As New Reference.clsItem
        Try
            Dim dsList = oItem.GetData.Where(Function(x) x.ISACTIVE = True And x.ISSTOK = True).ToList()

            grdKDITEM.DataSource = dsList
            grdKDITEM.ValueMember = "KDITEM"
            grdKDITEM.DisplayMember = "NMITEM2"

            grdKDITEM_ObatIGD.DataSource = dsList
            grdKDITEM_ObatIGD.ValueMember = "KDITEM"
            grdKDITEM_ObatIGD.DisplayMember = "NMITEM2"

            grdKDITEM_ObatRanap.DataSource = dsList
            grdKDITEM_ObatRanap.ValueMember = "KDITEM"
            grdKDITEM_ObatRanap.DisplayMember = "NMITEM2"
        Catch oErr As Exception
            MsgBox("Load Obat Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadUOM()
        Try
            Dim oUom As New Reference.clsUOM
            Dim dsList = oUom.GetData.Where(Function(x) x.ISACTIVE = True).ToList()

            grdUOM.DataSource = dsList
            grdUOM.ValueMember = "KDUOM"
            grdUOM.DisplayMember = "MEMO"

            grdKDUOM_ObatIGD.DataSource = dsList
            grdKDUOM_ObatIGD.ValueMember = "KDUOM"
            grdKDUOM_ObatIGD.DisplayMember = "MEMO"

            grdKDUOM_ObatRanap.DataSource = dsList
            grdKDUOM_ObatRanap.ValueMember = "KDUOM"
            grdKDUOM_ObatRanap.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox("Load Satuan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDDOCTOR()
        Dim oDoctor As New Reference.clsDoctor
        Try
            grdKDDOCTOR.Properties.DataSource = oDoctor.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDUSER()
        Dim oUser As New Setting.clsUser
        Try
            grdPETUGASTRIAGE.Properties.DataSource = oUser.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdPETUGASTRIAGE.Properties.ValueMember = "KDUSER"
            grdPETUGASTRIAGE.Properties.DisplayMember = "KDUSER"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub cboE_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboE.SelectedIndexChanged, cboM.SelectedIndexChanged, cboV.SelectedIndexChanged
        If isLoad = True Then
            Dim sE As Decimal = 0
            Dim sM As Decimal = 0
            Dim sV As Decimal = 0

            If cboE.Text = "-" Or cboM.Text = "-" Or cboV.Text = "-" Then
                txtGCS.Text = "-"
            Else
                If cboE.Text <> String.Empty Then
                    sE = CDec(cboE.Text)
                End If

                If cboM.Text <> String.Empty Then
                    sM = CDec(cboM.Text)
                End If

                If cboV.Text <> String.Empty Then
                    sV = CDec(cboV.Text)
                End If

                txtGCS.Text = sE + sM + sV
            End If
        End If
    End Sub
    Private Sub cboSAATPULANG_E_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSAATPULANG_E.SelectedIndexChanged, cboSAATPULANG_M.SelectedIndexChanged, cboSAATPULANG_V.SelectedIndexChanged
        If isLoad = True Then
            Dim sE As Decimal = 0
            Dim sM As Decimal = 0
            Dim sV As Decimal = 0

            If cboSAATPULANG_E.Text = "-" Or cboSAATPULANG_M.Text = "-" Or cboSAATPULANG_V.Text = "-" Then
                txtSAATPULANG_GCS.Text = "-"
            Else
                If cboSAATPULANG_E.Text <> String.Empty Then
                    sE = CDec(cboSAATPULANG_E.Text)
                End If

                If cboSAATPULANG_M.Text <> String.Empty Then
                    sM = CDec(cboSAATPULANG_M.Text)
                End If

                If cboSAATPULANG_V.Text <> String.Empty Then
                    sV = CDec(cboSAATPULANG_V.Text)
                End If

                txtSAATPULANG_GCS.Text = sE + sM + sV
            End If

        End If
    End Sub
    Private Sub chkNORMAL_KEPALA_CheckedChanged(sender As Object, e As EventArgs) Handles chkNORMAL_KEPALA.Click, chkTIDAK_NORMAL_KEPALA.Click
        chkNORMAL_KEPALA.Checked = False
        chkTIDAK_NORMAL_KEPALA.Checked = False
    End Sub
    Private Sub chkNORMAL_MATA_CheckedChanged(sender As Object, e As EventArgs) Handles chkNORMAL_MATA.Click, chkTIDAK_NORMAL_MATA.Click
        chkNORMAL_MATA.Checked = False
        chkTIDAK_NORMAL_MATA.Checked = False
    End Sub
    Private Sub chkNORMAL_LEHER_CheckedChanged(sender As Object, e As EventArgs) Handles chkNORMAL_LEHER.Click, chkTIDAK_NORMAL_LEHER.Click
        chkNORMAL_LEHER.Checked = False
        chkTIDAK_NORMAL_LEHER.Checked = False
    End Sub
    Private Sub chkNORMAL_DADA_CheckedChanged(sender As Object, e As EventArgs) Handles chkNORMAL_DADA.Click, chkTIDAK_NORMAL_DADA.Click
        chkNORMAL_DADA.Checked = False
        chkTIDAK_NORMAL_DADA.Checked = False
    End Sub
    Private Sub chkNORMAL_PERUT_CheckedChanged(sender As Object, e As EventArgs) Handles chkNORMAL_PERUT.Click, chkTIDAK_NORMAL_PERUT.Click
        chkNORMAL_PERUT.Checked = False
        chkTIDAK_NORMAL_PERUT.Checked = False
    End Sub
    Private Sub chkNORMAL_ALATGERAK_CheckedChanged(sender As Object, e As EventArgs) Handles chkNORMAL_ALATGERAK.Click, chkTIDAK_NORMAL_ALATGERAK.Click
        chkNORMAL_ALATGERAK.Checked = False
        chkTIDAK_NORMAL_ALATGERAK.Checked = False
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        grvDIAGNOSA_CPPT.DeleteSelectedRows()
    End Sub
    Private Sub DeleteToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem1.Click
        grvObatIGD.DeleteSelectedRows()
    End Sub
    Private Sub DeleteToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem2.Click
        grvObatRanap.DeleteSelectedRows()
    End Sub
    Private Sub DeleteToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem3.Click
        grvDetailResep.DeleteSelectedRows()
    End Sub
    Private Sub DeleteToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem4.Click
        grvCPPT_Tindakan.DeleteSelectedRows()
    End Sub
    Private Sub btnEDITIMAGE_Click(sender As Object, e As EventArgs) Handles btnEDITIMAGE.Click
        Dim frmPopUp_Image As New frmPopUp_img
        frmPopUp_Image.fn_LoadMe(picGAMBAR2.Image)
        frmPopUp_Image.ShowDialog()

        If sPicture IsNot Nothing Then
            picGAMBAR2.Image = sPicture
        End If

        picGAMBAR2.Focus()
    End Sub
    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        fn_LoadDataReloadIGDBidanPonek()
        grdReloadIGD.ShowPopup()
    End Sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        fn_LoadAdsemenPerawat()
    End Sub
    Private Sub SimpleButton2_Click_1(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        fn_LoadDataReloadIGD()
        grdReloadIGD.ShowPopup()
    End Sub
    Private Sub OnValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvDetailResep.FocusedRowChanged, grvObatIGD.FocusedRowChanged, grvObatRanap.FocusedRowChanged

    End Sub
    Private Sub fn_ReoladResep()
        'Try
        '    grvObatIGD.OptionsSelection.MultiSelect = True
        '    grvObatIGD.SelectAll()
        '    grvObatIGD.DeleteSelectedRows()
        '    grvObatIGD.OptionsSelection.MultiSelect = False

        '    grvObatRanap.OptionsSelection.MultiSelect = True
        '    grvObatRanap.SelectAll()
        '    grvObatRanap.DeleteSelectedRows()
        '    grvObatRanap.OptionsSelection.MultiSelect = False

        '    Dim oConn As New SqlConnection
        '    Dim oComm As New SqlCommand
        '    Dim da As SqlDataAdapter
        '    Dim ds As New DataSet
        '    Dim SQL As String
        '    oConn = New SqlConnection(sConnOld)

        '    If oConn.State = ConnectionState.Closed Then
        '        oConn.Open()
        '    End If

        '    SQL = "SELECT "
        '    SQL &= "B.* "
        '    SQL &= ",KATEGOR_IGD = ISNULL((B.BATCH), '') "
        '    SQL &= "FROM "
        '    SQL &= "S_RECIPE_H A "
        '    SQL &= "INNER JOIN S_RECIPE_D B "
        '    SQL &= "ON A.KDRECIPE = B.KDRECIPE "
        '    SQL &= "WHERE "
        '    SQL &= "A.KDREG = '" & txtNoRegister.Text & "' "

        '    oComm.Connection = oConn
        '    oComm.CommandText = SQL
        '    oComm.CommandTimeout = 120
        '    oComm.CommandType = CommandType.Text

        '    da = New SqlDataAdapter(oComm)
        '    da.Fill(ds, "S_RECIPE_H")

        '    If oConn.State = ConnectionState.Open Then
        '        oConn.Close()
        '    End If

        '    For iLoop As Integer = 0 To ds.Tables("S_RECIPE_H").Rows.Count - 1
        '        With ds.Tables("S_RECIPE_H")
        '            If .Rows(iLoop)("KATEGOR_IGD") = "RESEP RAWAT INAP" Then
        '                grvObatRanap.Focus()
        '                grvObatRanap.AddNewRow()
        '                grvObatRanap.SetFocusedRowCellValue(colKDITEM_ObatRanap, .Rows(iLoop)("KDITEM"))
        '                grvObatRanap.SetFocusedRowCellValue(colKDSIGNA_ObatRanap, .Rows(iLoop)("KDSIGNA"))
        '                grvObatRanap.SetFocusedRowCellValue(colKDCARAPAKAI_ObatRanap, .Rows(iLoop)("KDCP"))
        '                grvObatRanap.SetFocusedRowCellValue(colQTY_ObatRanap, .Rows(iLoop)("QTY"))
        '                grvObatRanap.UpdateCurrentRow()
        '            Else
        '                grvObatIGD.Focus()
        '                grvObatIGD.AddNewRow()
        '                grvObatIGD.SetFocusedRowCellValue(colKDITEM_ObatIGD, .Rows(iLoop)("KDITEM"))
        '                grvObatIGD.SetFocusedRowCellValue(colKDSIGNA_ObatIGD, .Rows(iLoop)("KDSIGNA"))
        '                grvObatIGD.SetFocusedRowCellValue(colKDCARAPAKAI_ObatIGD, .Rows(iLoop)("KDCP"))
        '                grvObatIGD.SetFocusedRowCellValue(colQTY_ObatIGD, .Rows(iLoop)("QTY"))
        '                grvObatIGD.UpdateCurrentRow()
        '            End If
        '        End With
        '    Next
        'Catch oErr As Exception
        '    MsgBox("Reload Resep" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub
    Private Sub grdReloadIGD_EditValueChanged(sender As Object, e As EventArgs) Handles grdReloadIGD.EditValueChanged
        If isLoad = False Then Exit Sub

        If grvReloadIGD.GetFocusedRowCellValue("KATEGORI") Is Nothing Then
            MsgBox("Kategori Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If grvReloadIGD.GetFocusedRowCellValue("KATEGORI") = "ASEMEN AWAL PERAWAT IGD" Then
            If grdReloadIGD.Text <> "" Then
                Try
                    txtRELOAD.Text = grdReloadIGD.EditValue

                    'Dim oDIGITAL_IGD_01_AWAL As New Transaksi.clsDIGITAL_IGD_01_AWAL
                    ' ***** HEADER *****
                    Dim ds = oDigitalIGD.GetDataByKodeIGD(grdReloadIGD.EditValue)

                    With ds
                        txtCODE.Text = sNoid
                        'deDATE.DateTime = .DATE
                        chkTRIAGE_TRAUMA.Checked = .TRIAGE_TRAUMA
                        chkTRIAGE_NONTRAUMA.Checked = .TRIAGE_NONTRAUMA
                        chkTRIAGE_MATERNITY.Checked = .TRIAGE_MATERNITY
                        chkTRIAGE_RED.Checked = .TRIAGE_RED
                        chkTRIAGE_YELLOW.Checked = .TRIAGE_YELLOW
                        chkTRIAGE_GREEN.Checked = .TRIAGE_GREEN
                        chkTRIAGE_BLACK.Checked = .TRIAGE_BLACK
                        'chkTRIAGE_AIRWAY.Checked = .TRIAGE_AIRWAY
                        'chkTRIAGE_BREATHING.Checked = .TRIAGE_BREATHING
                        'chkTRIAGE_CIRCULATION.Checked = .TRIAGE_CIRCULATION
                        cboTRIAGE_AIRWAY.Text = .TRIAGE_AIRWAY_TEX
                        cboTRIAGE_BREATHING.Text = .TRIAGE_BREATHING_TEXT
                        cboTRIAGE_CIRCULATION.Text = .TRIAGE_CIRCULATION_TEXT
                        chkTRIAGE_DATANGSENDIRI.Checked = .TRIAGE_DATANGSENDIRI
                        chkTRIAGE_DIANTAR.Checked = .TRIAGE_DIANTAR
                        cboTRIAGE_DIANTAR.Text = .TRIAGE_DIANTAR_TEXT
                        chkAUTOANAMNESA.Checked = .AUTOANAMNESA
                        chkALLOANAMNESA.Checked = .ALOANAMNESA
                        deDATEPUKULPERIKSA.Time = .PUKULPERIKSA
                        deDATEPUKULRAWAT.Time = .PUKULRAWAT
                        grdPETUGASTRIAGE.Text = .PETUGASTRIAGE
                        txtRIWAYAT.Text = .RIWAYAT
                        txtRIWAYAT_ALERGI.Text = .RIWAYAT_ALERGI
                        txtRIWAYAT_PENYAKITDAHULU.Text = .RIWAYAT_PENYAKITDAHULU
                        cboTINGKATKESADARAN.Text = .TINGKATKESADARAN
                        txtGCS.Text = .GCS
                        cboE.Text = .E
                        cboM.Text = .M
                        cboV.Text = .V
                        txtKEADAANUMUM.Text = .KEADAANUMUM
                        txtBERATBADAN.Text = .BERATBADAN
                        txtTINGGIBADAN.Text = .TINGGIBADAN
                        txtBP.Text = .BP
                        txtHR.Text = .HR
                        txtRR.Text = .RR
                        txtT.Text = .T
                        txtSPO2.Text = .SP02
                        chkRESIKO_1.Checked = .RESIKO_1
                        chkRESIKO_2.Checked = .RESIKO_2
                        chkFUNGSIONAL_1.Checked = .FUNGSIONAL_1
                        chkFUNGSIONAL_2.Checked = .FUNGSIONAL_2
                        chkFUNGSIONAL_3.Checked = .FUNGSIONAL_3
                        txtFUNGSIONAL_2.Text = .FUNGSIONAL_2_TEXT
                        txtFUNGSIONAL_3.Text = .FUNGSIONAL_3_TEXT
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
                        chkNORMAL_KEPALA.Checked = .NORMAL_KEPALA
                        chkTIDAK_NORMAL_KEPALA.Checked = .TIDAK_NORMAL_KEPALA
                        txtTidakNormalKepala.Text = .SURVEY_KEPALA_2
                        chkNORMAL_MATA.Checked = .NORMAL_MATA
                        chkTIDAK_NORMAL_MATA.Checked = .TIDAK_NORMAL_MATA
                        txtTidakNormalMata.Text = .SURVEY_MATA_2
                        chkNORMAL_LEHER.Checked = .NORMAL_LEHER
                        chkTIDAK_NORMAL_LEHER.Checked = .TIDAK_NORMAL_LEHER
                        txtTidakNormalLeher.Text = .SURVEY_LEHER_2
                        chkNORMAL_DADA.Checked = .NORMAL_DADA
                        chkTIDAK_NORMAL_DADA.Checked = .TIDAK_NORMAL_DADA
                        txtTidakNormalDada.Text = .SURVEY_DADA_2
                        chkNORMAL_PERUT.Checked = .NORMAL_PERUT
                        chkTIDAK_NORMAL_PERUT.Checked = .TIDAK_NORMAL_PERUT
                        txtTidakNormalPerut.Text = .SURVEY_PERUT_2
                        chkNORMAL_ALATGERAK.Checked = .NORMAL_ALATGERAK
                        chkTIDAK_NORMAL_ALATGERAK.Checked = .TIDAK_NORMAL_ALATGERAK
                        txtTidakNormalAlatGerak.Text = .SURVEY_ALATGERAK_2
                        'txtTINDAKLANJUT_TUJUAN.Text = .TUJUAN
                        txtSURVEY_ALATGERAK_1.Text = .SURVEY_ALATGERAK_1
                        txtSURVEY_MATA_1.Text = .SURVEY_MATA_1
                        txtKELUHANUTAMA.Text = .SURVEY_LEHER_1
                        Try
                            'Dim img = (From x In oS_DIGITAL_IGD_01.GetData
                            '           Where x.KDREG = txtNoRegister.Text
                            '           Select x.ATTACHMENT_2).Single

                            Dim img = oDigitalIGD.GetDataByKodeIGD(grdReloadIGD.EditValue).ATTACHMENT_1

                            picGAMBAR2.Image = ByteArrayToImage(img.ToArray())
                        Catch oErr As Exception
                        End Try

                        Try
                            Dim ImagePath As String = .SURVEY_PERUT_1
                            Dim img1 As Bitmap
                            Dim newImage As Image = Image.FromFile(.SURVEY_PERUT_1)

                            img1 = New Bitmap(ImagePath)
                            picGAMBAR2.ImageLocation = ImagePath

                            picGAMBAR2.Image = newImage
                        Catch ex As Exception
                            sPicture = Nothing
                            'MsgBox("Load List Data Gambar tidak ditemukan dialamat : " & .SIMPANGAMBAR_1, MsgBoxStyle.Exclamation, Me.Text)
                        End Try

                        grdKDDOCTOR.Text = .DOCTOR_KODE
                        txtKDDIAGNOSA.Text = .KDDIAGNOSA
                        chkKESIMPULAN_PERBAIKAN.Checked = .KESIMPULAN_PERBAIKAN
                        chkKESIMPULAN_STABIL.Checked = .KESIMPULAN_STABIL
                        chkKESIMPULAN_PERBURUKAN.Checked = .KESIMPULAN_PERBURUKAN
                        'chkTINDAKLANJUT_RUJUK.Checked = .TINDAKLANJUT_RUJUK
                        'chkTINDAKLANJUT_RAWAT.Checked = .TINDAKLANJUT_RAWAT
                        'chkTINDAKLANJUT_PULANGPAKSA.Checked = .TINDAKLANJUT_PULANGPAKSA
                        'chkTINDAKLANJUT_PULANG.Checked = .TINDAKLANJUT_PULANG
                        txtLOKASI.Text = .TUJUAN
                        txtSAATPASIENPULANG_TEXT.Text = .SAATPASIENPULANG_TEXT
                        'deDATEPUKUL.DateTime = .SAATPASIENPULANG_TIME
                        cboSAATPULANG_TINGKATKESDARAN.Text = .SAATPASIENPULANG_TINGKATKESADARAN
                        txtSAATPULANG_GCS.Text = .SAATPASIENPULANG_GCS
                        cboSAATPULANG_E.Text = .SAATPASIENPULANG_E
                        cboSAATPULANG_M.Text = .SAATPASIENPULANG_M
                        cboSAATPULANG_V.Text = .SAATPASIENPULANG_V
                        txtSAATPULANG_BP.Text = .SAATPASIENPULANG_BP
                        txtSAATPULANG_HR.Text = .SAATPASIENPULANG_HR
                        txtSAATPULANG_RR.Text = .SAATPASIENPULANG_RR
                        txtSAATPULANG_SPO2.Text = .SAATPASIENPULANG_SPO2
                        txtSAATPULANG_T.Text = .SAATPASIENPULANG_T
                        txtINSTRUKSILANJUTAN.Text = .INTRUKSILANJUTAN
                        txtLOKASI.Text = .PERAWATANLANJUTAN_LOKASI
                        txtWAKTU.Text = .PERAWATANLANJUTAN_WAKTU
                        'txtDOKTER_.Text = .PERAWATANLANJUTAN_DOKTER
                        'txtOBATSAATPULANG.Text = .OBATSAATPULANG
                        txtPERAWAT.Text = .PERAWATPENANGUNGJAWAB
                        txtTINDAKLANJUT.Text = .SURVEY_KEPALA_1

                        txtRELOAD.Text = .KDUSER_SIGNATURE

                        CheckEdit1.Checked = .KRITERIA_01
                        CheckEdit2.Checked = .KRITERIA_02
                        CheckEdit3.Checked = .KRITERIA_03
                        CheckEdit4.Checked = .KRITERIA_04
                        CheckEdit5.Checked = .KRITERIA_05
                        CheckEdit6.Checked = .KRITERIA_06

                        BindingSourceObatIGD.DataSource = oDigitalIGD.GetDataDetailResepObatPulang(txtCODE.Text).OrderBy(Function(x) x.SEQ).ToList()
                        grdDetailResep.DataSource = BindingSourceObatIGD

                        BindingSourceObatIGD.DataSource = oDigitalIGD.GetDataDetailResepSelamaIGD(txtCODE.Text).OrderBy(Function(x) x.SEQ).ToList()
                        grdObatIGD.DataSource = BindingSourceObatIGD

                        BindingSourceObatRanap.DataSource = oDigitalIGD.GetDataDetailResepObatRanap(txtCODE.Text).OrderBy(Function(x) x.SEQ).ToList()
                        grdObatRanap.DataSource = BindingSourceObatRanap

                        'BindingSourceTindakanPoli.DataSource = oS_DIGITAL_IGD_01.GetDataDetailTindakanPoli(txtCODE.Text).OrderBy(Function(x) x.SEQ).ToList()
                        'grdTindakanPoli.DataSource = BindingSourceTindakanPoli

                        'BindingSourcePenunjang.DataSource = oS_DIGITAL_IGD_01.GetDataDetailPenunjang(txtCODE.Text).OrderBy(Function(x) x.SEQ).ToList()
                        'grdTindakan.DataSource = BindingSourcePenunjang

                        BindingSourceDiagnosa.DataSource = oDigitalIGD.GetDataDetail(txtCODE.Text).OrderBy(Function(x) x.SEQ).ToList()
                        grdDIAGNOSA_CPPT.DataSource = BindingSourceDiagnosa

                        BindingSourceTindakan.DataSource = oDigitalIGD.GetDataDetailTindakan(txtCODE.Text).OrderBy(Function(x) x.SEQ).ToList()
                        grdCPPT_Tindakan.DataSource = BindingSourceTindakan

                        'txtPERAWAT .Text = .KDUSER
                        'deDATE.DateTime = .DATE
                        ''txtNamaPasien.Text = .NAMAPASIEN
                        'txtRIWAYAT.Text = .RIWAYAT
                        'txtRIWAYAT_ALERGI.Text = .RIWAYAT_ALERGI
                        'txtRIWAYAT_PENYAKITDAHULU.Text = .RIWAYAT_PENYAKITDAHULU
                        'cboTINGKATKESADARAN.Text = .TINGKATKESADARAN
                        'txtGCS.Text = .GCS
                        'cboE.Text = .E
                        'cboM.Text = .M
                        'cboV.Text = .V
                        'txtKEADAANUMUM.Text = .KEADAANUMUM
                        'txtBERATBADAN.Text = .BERATBADAN
                        'txtTINGGIBADAN.Text = .TINGGIBADAN
                        'txtBP.Text = .BP
                        'txtHR.Text = .HR
                        'txtRR.Text = .RR
                        'txtT.Text = .T
                        'txtSPO2.Text = .SP02

                        'chkTRIAGE_RED.Checked = .TRIAGE_RED
                        'chkTRIAGE_YELLOW.Checked = .TRIAGE_YELLOW
                        'chkTRIAGE_GREEN.Checked = .TRIAGE_GREEN
                        'chkTRIAGE_BLACK.Checked = .TRIAGE_BLACK

                        ''txtSAATPULANG_GCS.Text = .GCS
                        ''cboSAATPULANG_E.Text = .GCS_E
                        ''cboSAATPULANG_M.Text = .GCS_M
                        ''cboSAATPULANG_V.Text = .GCS_V

                        'cboSAATPULANG_TINGKATKESDARAN.Text = .KEADAANUMUM
                        'txtSAATPULANG_BP.Text = .BP
                        'txtSAATPULANG_HR.Text = .HR
                        'txtSAATPULANG_T.Text = .T
                        'txtSAATPULANG_RR.Text = .RR
                        'txtSAATPULANG_SPO2.Text = .SP02

                        'cboTRIAGE_AIRWAY.Text = .TRIAGE_AIRWAY_TEX
                        'cboTRIAGE_BREATHING.Text = .TRIAGE_BREATHING_TEXT
                        'cboTRIAGE_CIRCULATION.Text = .TRIAGE_CIRCULATION_TEXT

                        ''BindingSourceTindakanPoli.DataSource = oS_DIGITAL_IGD_01.GetDataDetail(.KDAWALASESMENIGD).OrderBy(Function(x) x.SEQ).ToList()
                        ''grdTindakanPoli.DataSource = BindingSourceTindakanPoli

                        ''BindingSourcePenunjang.DataSource = oS_DIGITAL_IGD_01.GetDataDetail_P(.KDAWALASESMENIGD).OrderBy(Function(x) x.SEQ).ToList()
                        ''grdTindakan.DataSource = BindingSourcePenunjang

                        ''BindingSource.DataSource = oS_DIGITAL_IGD_01.GetDataDetail_ResepPulang(.KDAWALASESMENIGD).OrderBy(Function(x) x.SEQ).ToList()
                        ''grdDetailResep.DataSource = BindingSource

                        'deDATEPUKULPERIKSA.Time = .DATECREATED

                    End With
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        ElseIf grvReloadIGD.GetFocusedRowCellValue("KATEGORI") = "KEBIDANAN PONEK"
            Dim oDigital As New Digital.clsDigital_IGD_03
            Dim dsDigital = oDigital.GetData(grdReloadIGD.EditValue)
            If dsDigital IsNot Nothing Then
                txtRIWAYAT.Text = "Riwayat Menstruasi :" & vbCrLf & "HPHT : " & dsDigital.RIWAYATMENSTRUASI_TEXT_1 & vbCrLf _
                                  & "Dismenorea : " & dsDigital.RIWAYATMENSTRUASI_TEXT_2 & vbCrLf _
                                  & "Siklus : " & dsDigital.RIWAYATMENSTRUASI_TEXT_3 & vbCrLf _
                                  & "Banyaknya : " & dsDigital.RIWAYATMENSTRUASI_TEXT_4 & vbCrLf _
                                  & IIf(dsDigital.RIWAYATMENSTRUASI_6_1 = True, "Teratur", "Tidak Teratur") & vbCrLf _
                                  & "TP : " & dsDigital.RIWAYATMENSTRUASI_TEXT_7 & vbCrLf _
                                  & IIf(dsDigital.RIWAYATMENSTRUASI_8_1 = True, "Menorhagia", "") & vbCrLf _
                                  & IIf(dsDigital.RIWAYATMENSTRUASI_8_2 = True, "Metrorhagia", "")

                txtSURVEY_MATA_1.Text = "Pemeriksaan Dalam :" & vbCrLf & "Vulva Vagina : " & dsDigital.PEMERIKSAANDALAM_1_TEXT & vbCrLf _
                                        & "Portio : " & dsDigital.PEMERIKSAANDALAM_2_TEXT & vbCrLf _
                                        & "Ketuban : " & dsDigital.PEMERIKSAANDALAM_3_TEXT & vbCrLf _
                                        & "Pembukaan : " & dsDigital.PEMERIKSAANDALAM_4_TEXT & vbCrLf _
                                        & "Presentasi fetur : " & dsDigital.PEMERIKSAANDALAM_5_TEXT & vbCrLf _
                                        & "Hodge/Station : " & dsDigital.PEMERIKSAANDALAM_6_TEXT & vbCrLf
            Else
                MsgBox("Vonek Kebidan Tidak di Temukan kode " & grdReloadIGD.EditValue, MsgBoxStyle.Exclamation, Me.Text)
            End If
        End If
    End Sub
    Private Sub fn_LoadTEMPLATE()
        'Dim oTemplate As New Reference.clsTemplateResep
        'Try
        '    grdTEMPLATE.Properties.DataSource = oTemplate.GetData.Where(Function(x) x.NOIDUSER = sUserID).ToList()
        '    grdTEMPLATE.Properties.ValueMember = "KDTEMPLATE"
        '    grdTEMPLATE.Properties.DisplayMember = "DESCRIPTION"
        'Catch oErr As Exception
        '    MsgBox("Load Template Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
        Dim oTemplateNonRacikan As New EMedrek.clsTemplateNonRacikan
        Try
            'grdTEMPLATE.Properties.DataSource = oTemplate.GetDataDetailList().ToList()
            'grdTEMPLATE.Properties.ValueMember = "KDCPPTTEMPLATE"
            'grdTEMPLATE.Properties.DisplayMember = "REMARKS"

            Dim ds = From x In oTemplateNonRacikan.GetDataDetailList()
                     Join y In oTemplateNonRacikan.GetDataDetail()
                     On x.KDCPPTTEMPLATE Equals y.KDCPPTTEMPLATE
                     Where y.KDUSER = sUserID
                     Group y By x.KDCPPTTEMPLATE, x.REMARKS, y.KDUSER Into Total = Sum(y.TOTAL)
                     Select KDCPPTTEMPLATE, REMARKS

            grdTEMPLATE.Properties.DataSource = ds.ToList()
            grdTEMPLATE.Properties.ValueMember = "KDCPPTTEMPLATE"
            grdTEMPLATE.Properties.DisplayMember = "REMARKS"

            grdKDTEMPLATESELMAIGD.Properties.DataSource = ds.ToList()
            grdKDTEMPLATESELMAIGD.Properties.ValueMember = "KDCPPTTEMPLATE"
            grdKDTEMPLATESELMAIGD.Properties.DisplayMember = "REMARKS"

            grdKDTEMPLATERANAP.Properties.DataSource = ds.ToList()
            grdKDTEMPLATERANAP.Properties.ValueMember = "KDCPPTTEMPLATE"
            grdKDTEMPLATERANAP.Properties.DisplayMember = "REMARKS"

            'Dim dsRacikan = From x In oTemplateNonRacikan.GetDataDetailList()
            '                Join y In oTemplateNonRacikan.GetDataDetailRacikan()
            '                On x.KDCPPTTEMPLATE Equals y.KDCPPTTEMPLATE
            '                Where y.KDUSER = sUserID
            '                Group y By x.KDCPPTTEMPLATE, x.REMARKS, y.KDUSER Into Total = Sum(y.TOTAL)
            '                Select KDCPPTTEMPLATE, REMARKS

            'grdTEMPLATERACIKAN.Properties.DataSource = dsRacikan.ToList()
            'grdTEMPLATERACIKAN.Properties.ValueMember = "KDCPPTTEMPLATE"
            'grdTEMPLATERACIKAN.Properties.DisplayMember = "REMARKS"

        Catch oErr As Exception
            MsgBox("Load Template Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdTEMPLATE_EditValueChanged(sender As Object, e As EventArgs) Handles grdTEMPLATE.EditValueChanged
        If isLoad = True Then
            If isLoad = True Then
                If grdTEMPLATE.Text <> "" Then
                    Dim oTemplate As New EMedrek.clsTemplateNonRacikan

                    Dim dsTemplate = oTemplate.GetDataDetail(grdTEMPLATE.EditValue)

                    For Each iLoop In dsTemplate.OrderBy(Function(x) x.SEQ)
                        'txtDIAGNOOSA.Text = iLoop.S_REQ_RECIPE_TEMPLATE_H.DESCRIPTION
                        grvDetailResep.Focus()
                        grvDetailResep.AddNewRow()
                        grvDetailResep.SetFocusedRowCellValue(colKDITEM, iLoop.KDITEM)
                        grvDetailResep.SetFocusedRowCellValue(colKDUOM, iLoop.KDUOM)
                        grvDetailResep.SetFocusedRowCellValue(colKDSIGNA, iLoop.KDSIGNA)
                        grvDetailResep.SetFocusedRowCellValue(colKDCARAPAKAI, iLoop.KDCARAPAKAI)
                        grvDetailResep.SetFocusedRowCellValue(colQTY, iLoop.JUMLAH)
                        grvDetailResep.SetFocusedRowCellValue(colREMARKS_DOKTER, iLoop.REMARKS_DOKTER)
                        grvDetailResep.UpdateCurrentRow()
                    Next
                End If
            End If
        End If
    End Sub
    Private Sub grdCARIDIAGNOSA_CPPT_EditValueChanged(sender As Object, e As EventArgs) Handles grdCARIDIAGNOSA_CPPT.EditValueChanged
        If grdCARIDIAGNOSA_CPPT.Text <> "" Then
            Dim sCek As Integer = 0

            For i As Integer = 0 To grvDIAGNOSA_CPPT.RowCount - 2
                sCek += 1
            Next

            grvDIAGNOSA_CPPT.Focus()
            grvDIAGNOSA_CPPT.AddNewRow()
            'grvDIAGNOSA_CPPT.SetFocusedRowCellValue(colKATEGORI_CPPT, IIf(sCek = 0, "Primer", "Sekunder"))
            grvDIAGNOSA_CPPT.SetFocusedRowCellValue(colKATEGORI_CPPT, IIf(sCek = 0, "Primary", "Secondary"))
            grvDIAGNOSA_CPPT.SetFocusedRowCellValue(colKDDIAGNOSA_CPPT, grdCARIDIAGNOSA_CPPT.EditValue)
            grvDIAGNOSA_CPPT.SetFocusedRowCellValue(colNAMADIAGNOSA_CPPT, grdCARIDIAGNOSA_CPPT.Text)
            grvDIAGNOSA_CPPT.UpdateCurrentRow()

            'txtCARIDIAGNOSA_CPPT.Focus()
        End If
    End Sub
    Private Sub fn_LoadDiagnosa()
        Try
            grdCARIDIAGNOSA_CPPT.Properties.DataSource = ListDiagnosaiDRG
            grdCARIDIAGNOSA_CPPT.Properties.ValueMember = "KDDIAGNOSA"
            grdCARIDIAGNOSA_CPPT.Properties.DisplayMember = "MEMO"

            'grdCariDiagnosa.Properties.DataSource = ListDiagnosaiNACBG
            'grdCariDiagnosa.Properties.ValueMember = "KDDIAGNOSA"
            'grdCariDiagnosa.Properties.DisplayMember = "MEMO"

            'grdDIAGNOSA_V6.Properties.DataSource = ListDiagnosaiNACBG
            'grdDIAGNOSA_V6.Properties.ValueMember = "KDDIAGNOSA"
            'grdDIAGNOSA_V6.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox("Load Diagnosa" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
#Region "Grid Method"
    Private Sub grvDetailResep_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDetailResep.CellValueChanged
        If e.Column.Name = colKDITEM.Name Then
            Dim oItem As New Reference.clsItem
            Try
                If grvDetailResep.GetFocusedRowCellValue(colKDITEM) IsNot Nothing Then
                    Dim ds = oItem.GetDataDetail_UOM(grvDetailResep.GetFocusedRowCellValue(colKDITEM))

                    If ds IsNot Nothing Then
                        grvDetailResep.SetFocusedRowCellValue(colKDUOM, ds.FirstOrDefault(Function(x) x.RATE = 1).KDUOM)
                        grvDetailResep.SetFocusedRowCellValue(colREMARKS_DOKTER, "")
                        grvDetailResep.SetFocusedRowCellValue(colQTY, 0)
                    Else
                        MsgBox(Statement.ErrorUOM, MsgBoxStyle.Exclamation, Me.Text)

                        grvDetailResep.CancelUpdateCurrentRow()
                    End If
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf e.Column.Name = colREMARKS_DOKTER.Name Then
            'grvTindakan.Focus()
            'grvTindakan.AddNewRow()
            'grvTindakan.SetFocusedRowCellValue(colTERAPI, fn_LoadITEM(grvDetailResep.GetFocusedRowCellValue(colKDITEM)) & " " & fn_LoadSIGNA(grvDetailResep.GetFocusedRowCellValue(colKDSIGNA)))
            'grvTindakan.SetFocusedRowCellValue(colTINDAKAN, "")
            'grvTindakan.UpdateCurrentRow()
        ElseIf e.Column.Name = colKDUOM.Name Then
            'Dim oItem As New Reference.clsItem
            'Try
            '    If grvDetailResep.GetFocusedRowCellValue(colKDITEM) IsNot Nothing And grvDetailResep.GetFocusedRowCellValue(colKDUOM) IsNot Nothing Then
            '        Dim ds = oItem.GetDataDetail_UOM(grvDetailResep.GetFocusedRowCellValue(colKDITEM), grvDetailResep.GetFocusedRowCellValue(colKDUOM))

            '        If ds IsNot Nothing Then
            '            grvDetailResep.SetFocusedRowCellValue(colPRICE, ds.PRICESALESSTANDARD)
            '        Else
            '            MsgBox(Statement.ErrorUOM, MsgBoxStyle.Exclamation, Me.Text)

            '            Dim sItem = grvDetailResep.GetFocusedRowCellValue(colKDITEM)
            '            grvDetailResep.CancelUpdateCurrentRow()

            '            grvDetailResep.AddNewRow()
            '            grvDetailResep.SetFocusedRowCellValue(colKDITEM, sItem)
            '        End If
            '    End If
            'Catch oErr As Exception
            '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            'End Try
            Dim oItem As New Reference.clsItem
            Dim ds = oItem.GetDataDetail_UOM(grvDetailResep.GetFocusedRowCellValue(colKDITEM))

            'Dim hargajualawal As Decimal = fn_LoadUOMKDUOMHARGA(grvDetailResep.GetFocusedRowCellValue(colKDITEM), grvDetailResep.GetFocusedRowCellValue(colKDUOM))
            'Dim margin As Decimal = fn_LoadUOMKDUOMMARGIN(grvDetailResep.GetFocusedRowCellValue(colKDITEM), grvDetailResep.GetFocusedRowCellValue(colKDUOM))

            'Dim sPriceSales As Decimal = hargajualawal + (hargajualawal * (margin / 100))
            If sHargaApotik = True Then
                If sPenjmain <> "BPJS" Then
                    grvDetailResep.SetFocusedRowCellValue(colPRICE, ds.FirstOrDefault.PRICESALESSTANDARD)
                Else
                    grvDetailResep.SetFocusedRowCellValue(colPRICE, ds.FirstOrDefault.PRICESALESTERMIN)
                End If
            Else
                grvDetailResep.SetFocusedRowCellValue(colPRICE, ds.FirstOrDefault.PRICEPURCHASESTANDARD)

                If CDec(grvDetailResep.GetFocusedRowCellValue(colPRICE)) > 0 Then
                    Dim sPriceSetelahPPN = 0 + CDec(grvDetailResep.GetFocusedRowCellValue(colPRICE))
                    Dim sPriceTermin = buletin(CDec(sPriceSetelahPPN * (IIf(sPenjmain <> "BPJS", (ds.FirstOrDefault.MARGIN / 100), (ds.FirstOrDefault.PRICESALESTERMIN / 100))) + CDec(sPriceSetelahPPN)), 100)

                    grvDetailResep.SetFocusedRowCellValue(colPRICE, sPriceTermin)
                Else
                    grvDetailResep.SetFocusedRowCellValue(colPRICE, 0)
                End If
            End If

        ElseIf e.Column.Name = colQTY.Name Or e.Column.Name = colPRICE.Name Then
            Dim sSubTotal As Decimal = CDec(grvDetailResep.GetFocusedRowCellValue(colQTY)) * CDec(grvDetailResep.GetFocusedRowCellValue(colPRICE))
            grvDetailResep.SetFocusedRowCellValue(colGRANDTOTAL, sSubTotal)
        End If
    End Sub
    Private Sub grvObatIGD_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvObatIGD.CellValueChanged
        If e.Column.Name = colKDITEM_ObatIGD.Name Then
            Dim oItem As New Reference.clsItem
            Try
                If grvObatIGD.GetFocusedRowCellValue(colKDITEM_ObatIGD) IsNot Nothing Then
                    Dim ds = oItem.GetDataDetail_UOM(grvObatIGD.GetFocusedRowCellValue(colKDITEM_ObatIGD))

                    If ds IsNot Nothing Then
                        grvObatIGD.SetFocusedRowCellValue(colKDUOM_ObatIGD, ds.FirstOrDefault(Function(x) x.RATE = 1).KDUOM)
                        grvObatIGD.SetFocusedRowCellValue(colREMARKS_DOKTER_ObatIGD, "")
                        grvObatIGD.SetFocusedRowCellValue(colQTY_ObatIGD, 0)
                    Else
                        MsgBox(Statement.ErrorUOM, MsgBoxStyle.Exclamation, Me.Text)

                        grvObatIGD.CancelUpdateCurrentRow()
                    End If
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf e.Column.Name = colREMARKS_DOKTER_ObatIGD.Name Then
            'grvTindakan.Focus()
            'grvTindakan.AddNewRow()
            'grvTindakan.SetFocusedRowCellValue(colTERAPI, fn_LoadITEM(grvObatIGD.GetFocusedRowCellValue(colKDITEM_ObatIGD)) & " " & fn_LoadSIGNA(grvObatIGD.GetFocusedRowCellValue(colKDSIGNA)))
            'grvTindakan.SetFocusedRowCellValue(colTINDAKAN, "")
            'grvTindakan.UpdateCurrentRow()
        ElseIf e.Column.Name = colKDUOM_ObatIGD.Name Then
            'Dim oItem As New Reference.clsItem
            'Try
            '    If grvObatIGD.GetFocusedRowCellValue(colKDITEM_ObatIGD) IsNot Nothing And grvObatIGD.GetFocusedRowCellValue(colKDUOM_ObatIGD) IsNot Nothing Then
            '        Dim ds = oItem.GetDataDetail_UOM(grvObatIGD.GetFocusedRowCellValue(colKDITEM_ObatIGD), grvObatIGD.GetFocusedRowCellValue(colKDUOM_ObatIGD))

            '        If ds IsNot Nothing Then
            '            grvObatIGD.SetFocusedRowCellValue(colPRICE_ObatIGD, ds.PRICESALESSTANDARD)
            '        Else
            '            MsgBox(Statement.ErrorUOM, MsgBoxStyle.Exclamation, Me.Text)

            '            Dim sItem = grvObatIGD.GetFocusedRowCellValue(colKDITEM_ObatIGD)
            '            grvObatIGD.CancelUpdateCurrentRow()

            '            grvObatIGD.AddNewRow()
            '            grvObatIGD.SetFocusedRowCellValue(colKDITEM_ObatIGD, sItem)
            '        End If
            '    End If
            'Catch oErr As Exception
            '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            'End Try
            Dim oItem As New Reference.clsItem
            Dim ds = oItem.GetDataDetail_UOM(grvObatIGD.GetFocusedRowCellValue(colKDITEM_ObatIGD))

            'Dim hargajualawal As Decimal = fn_LoadUOMKDUOMHARGA(grvObatIGD.GetFocusedRowCellValue(colKDITEM), grvObatIGD.GetFocusedRowCellValue(colKDUOM))
            'Dim margin As Decimal = fn_LoadUOMKDUOMMARGIN(grvObatIGD.GetFocusedRowCellValue(colKDITEM), grvObatIGD.GetFocusedRowCellValue(colKDUOM))

            'Dim sPriceSales As Decimal = hargajualawal + (hargajualawal * (margin / 100))
            If sHargaApotik = True Then
                If sPenjmain <> "BPJS" Then
                    grvObatIGD.SetFocusedRowCellValue(colPRICE_ObatIGD, ds.FirstOrDefault.PRICESALESSTANDARD)
                Else
                    grvObatIGD.SetFocusedRowCellValue(colPRICE_ObatIGD, ds.FirstOrDefault.PRICESALESTERMIN)
                End If
            Else
                grvObatIGD.SetFocusedRowCellValue(colPRICE_ObatIGD, ds.FirstOrDefault.PRICEPURCHASESTANDARD)

                If CDec(grvObatIGD.GetFocusedRowCellValue(colPRICE_ObatIGD)) > 0 Then
                    Dim sPriceSetelahPPN = 0 + CDec(grvObatIGD.GetFocusedRowCellValue(colPRICE_ObatIGD))
                    Dim sPriceTermin = buletin(CDec(sPriceSetelahPPN * (IIf(sPenjmain <> "BPJS", (ds.FirstOrDefault.MARGIN / 100), (ds.FirstOrDefault.PRICESALESTERMIN / 100))) + CDec(sPriceSetelahPPN)), 100)

                    grvObatIGD.SetFocusedRowCellValue(colPRICE_ObatIGD, sPriceTermin)
                Else
                    grvObatIGD.SetFocusedRowCellValue(colPRICE_ObatIGD, 0)
                End If
            End If
        ElseIf e.Column.Name = colQTY_ObatIGD.Name Or e.Column.Name = colPRICE_ObatIGD.Name Then
            Dim sSubTotal As Decimal = CDec(grvObatIGD.GetFocusedRowCellValue(colQTY_ObatIGD)) * CDec(grvObatIGD.GetFocusedRowCellValue(colPRICE_ObatIGD))
            grvObatIGD.SetFocusedRowCellValue(colGRANDTOTAL_ObatIGD, sSubTotal)
        End If
    End Sub
    Private Sub grvObatRanap_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvObatRanap.CellValueChanged
        If e.Column.Name = colKDITEM_ObatRanap.Name Then
            Dim oItem As New Reference.clsItem
            Try
                If grvObatRanap.GetFocusedRowCellValue(colKDITEM_ObatRanap) IsNot Nothing Then
                    Dim ds = oItem.GetDataDetail_UOM(grvObatRanap.GetFocusedRowCellValue(colKDITEM_ObatRanap))

                    If ds IsNot Nothing Then
                        grvObatRanap.SetFocusedRowCellValue(colKDUOM_ObatRanap, ds.FirstOrDefault(Function(x) x.RATE = 1).KDUOM)
                        grvObatRanap.SetFocusedRowCellValue(colREMARKS_DOKTER_ObatRanap, "")
                        grvObatRanap.SetFocusedRowCellValue(colQTY_ObatRanap, 0)
                    Else
                        MsgBox(Statement.ErrorUOM, MsgBoxStyle.Exclamation, Me.Text)

                        grvObatRanap.CancelUpdateCurrentRow()
                    End If
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf e.Column.Name = colREMARKS_DOKTER_ObatRanap.Name Then
            'grvTindakan.Focus()
            'grvTindakan.AddNewRow()
            'grvTindakan.SetFocusedRowCellValue(colTERAPI, fn_LoadITEM(grvObatRanap.GetFocusedRowCellValue(colKDITEM_ObatRanap)) & " " & fn_LoadSIGNA(grvObatRanap.GetFocusedRowCellValue(colKDSIGNA)))
            'grvTindakan.SetFocusedRowCellValue(colTINDAKAN, "")
            'grvTindakan.UpdateCurrentRow()
        ElseIf e.Column.Name = colKDUOM_ObatRanap.Name Then
            'Dim oItem As New Reference.clsItem
            'Try
            '    If grvObatRanap.GetFocusedRowCellValue(colKDITEM_ObatRanap) IsNot Nothing And grvObatRanap.GetFocusedRowCellValue(colKDUOM_ObatRanap) IsNot Nothing Then
            '        Dim ds = oItem.GetDataDetail_UOM(grvObatRanap.GetFocusedRowCellValue(colKDITEM_ObatRanap), grvObatRanap.GetFocusedRowCellValue(colKDUOM_ObatRanap))

            '        If ds IsNot Nothing Then
            '            grvObatRanap.SetFocusedRowCellValue(colPRICE_ObatRanap, ds.PRICESALESSTANDARD)
            '        Else
            '            MsgBox(Statement.ErrorUOM, MsgBoxStyle.Exclamation, Me.Text)

            '            Dim sItem = grvObatRanap.GetFocusedRowCellValue(colKDITEM_ObatRanap)
            '            grvObatRanap.CancelUpdateCurrentRow()

            '            grvObatRanap.AddNewRow()
            '            grvObatRanap.SetFocusedRowCellValue(colKDITEM_ObatRanap, sItem)
            '        End If
            '    End If
            'Catch oErr As Exception
            '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            'End Try

            Dim oItem As New Reference.clsItem
            Dim ds = oItem.GetDataDetail_UOM(grvObatRanap.GetFocusedRowCellValue(colKDITEM_ObatRanap))

            'Dim hargajualawal As Decimal = fn_LoadUOMKDUOMHARGA(grvObatRanap.GetFocusedRowCellValue(colKDITEM), grvObatRanap.GetFocusedRowCellValue(colKDUOM))
            'Dim margin As Decimal = fn_LoadUOMKDUOMMARGIN(grvObatRanap.GetFocusedRowCellValue(colKDITEM), grvObatRanap.GetFocusedRowCellValue(colKDUOM))

            'Dim sPriceSales As Decimal = hargajualawal + (hargajualawal * (margin / 100))
            If sHargaApotik = True Then
                If sPenjmain <> "BPJS" Then
                    grvObatRanap.SetFocusedRowCellValue(colPRICE_ObatRanap, ds.FirstOrDefault.PRICESALESSTANDARD)
                Else
                    grvObatRanap.SetFocusedRowCellValue(colPRICE_ObatRanap, ds.FirstOrDefault.PRICESALESTERMIN)
                End If
            Else
                grvObatRanap.SetFocusedRowCellValue(colPRICE_ObatRanap, ds.FirstOrDefault.PRICEPURCHASESTANDARD)

                If CDec(grvObatRanap.GetFocusedRowCellValue(colPRICE_ObatRanap)) > 0 Then
                    Dim sPriceSetelahPPN = 0 + CDec(grvObatRanap.GetFocusedRowCellValue(colPRICE_ObatRanap))
                    Dim sPriceTermin = buletin(CDec(sPriceSetelahPPN * (IIf(sPenjmain <> "BPJS", (ds.FirstOrDefault.MARGIN / 100), (ds.FirstOrDefault.PRICESALESTERMIN / 100))) + CDec(sPriceSetelahPPN)), 100)

                    grvObatRanap.SetFocusedRowCellValue(colPRICE_ObatRanap, sPriceTermin)
                Else
                    grvObatRanap.SetFocusedRowCellValue(colPRICE_ObatRanap, 0)
                End If
            End If
        ElseIf e.Column.Name = colQTY_ObatRanap.Name Or e.Column.Name = colPRICE_ObatRanap.Name Then
            Dim sSubTotal As Decimal = CDec(grvObatRanap.GetFocusedRowCellValue(colQTY_ObatRanap)) * CDec(grvObatRanap.GetFocusedRowCellValue(colPRICE_ObatRanap))
            grvObatRanap.SetFocusedRowCellValue(colGRANDTOTAL_ObatRanap, sSubTotal)
        End If
    End Sub
    Private Sub grvCPPT_Tindakan_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvCPPT_Tindakan.CellValueChanged
        Dim oItem As New Reference.clsItem

        If e.Column.Name = colCPPT_KDITEMTINDAKAN.Name Then
            If grvCPPT_Tindakan.GetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN) IsNot Nothing Then
                Dim ds = oItem.GetDataDetail_UOM(grvCPPT_Tindakan.GetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN))

                If ds IsNot Nothing Then
                    Try
                        grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_KDUOMTINDAKAN, ds.FirstOrDefault(Function(x) x.M_UOM.KDKELASRAWAT = sKelas).KDUOM)
                    Catch ex As Exception
                        Try
                            grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_KDUOMTINDAKAN, ds.FirstOrDefault(Function(x) x.RATE = 1).KDUOM)
                        Catch ex2 As Exception

                        End Try
                    End Try
                End If
            End If
        ElseIf e.Column.Name = colCPPT_KDUOMTINDAKAN.Name Then
            Try
                If grvCPPT_Tindakan.GetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN) IsNot Nothing And grvCPPT_Tindakan.GetFocusedRowCellValue(colCPPT_KDUOMTINDAKAN) IsNot Nothing Then
                    Dim ds = oItem.GetDataDetail_UOM(grvCPPT_Tindakan.GetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN), grvCPPT_Tindakan.GetFocusedRowCellValue(colCPPT_KDUOMTINDAKAN))

                    If ds IsNot Nothing Then
                        grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_JUMLAHTINDAKAN, 1)
                        grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_HARGATINDAKAN, ds.PRICESALESSTANDARD)
                        grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_TOTALTINDAKAN, ds.PRICESALESSTANDARD)
                    Else
                        MsgBox("Load Data Uom", MsgBoxStyle.Exclamation, Me.Text)

                        Dim sItem = grvCPPT_Tindakan.GetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN)
                        grvCPPT_Tindakan.CancelUpdateCurrentRow()

                        grvCPPT_Tindakan.AddNewRow()
                        grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN, sItem)
                    End If
                End If
            Catch oErr As Exception
                MsgBox("Load Data Tindakan" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Function buletin(ByVal Number As Double, Optional ByVal Range As Integer = 10) As Decimal

        buletin = Math.Round(Number / Range, 0) * Range

    End Function
    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        If txtNoRegister.Text Is Nothing Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim Kode As String = fn_LoadTindakLanjutAlasanCek(txtNoRegister.Text, "RAWAT INAP")
        If Kode <> "" Then
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadNoPendaftaranPolidanDokter(1, "", "", sKDCUSTOMER, txtNoRegister.Text, "RAWAT INAP")
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, Kode)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSKD Is Nothing Then frmSKD.Dispose()
                frmSKD = Nothing
            End Try
        Else
            Dim frmSKD As New frmSKD
            Try
                'frmSKD.fn_LoadNoPendaftaran(txtKDKUNJUNGAN.Text, txtKDCUSTOMER.Text, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtNOMORSEP.Text, txtPenjamin.Text, txtNAMAPASIEN.Text, txtDIAGNOOSA.Text)
                'frmSKD.fn_LoadKategori(0)
                frmSKD.fn_LoadNoPendaftaranPolidanDokter(1, "", "", sKDCUSTOMER, txtNoRegister.Text, "RAWAT INAP")
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If

        fn_LoadTindakLanjutPertama(txtNoRegister.Text)

    End Sub
    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        If txtNoRegister.Text Is Nothing Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If MsgBox("Apakah Selesai Pengobatan?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        Dim Kode As String = fn_LoadTindakLanjutAlasanCek(txtNoRegister.Text, "SELESAI PENGOBATAN")

        If Kode <> "" Then
            fn_SaveSKD(False, Kode, "SELESAI PENGOBATAN", 0)
        Else
            fn_SaveSKD(True, "", "SELESAI PENGOBATAN", 0)
        End If

        fn_LoadTindakLanjutPertama(txtNoRegister.Text)
    End Sub
    Private Function fn_SaveSKD(ByVal isAdd As Boolean, ByVal NOIID As String, ByVal ALASAN As String, ByVal sISCATEGORY As Integer) As Boolean
        Try
            ' ***** HEADER *****
            Dim oSKD As New Admission.clsSKD

            Dim dsDaftar = oSKD.GetDataPendaftaranByKoderegistrasi(txtNoRegister.Text)

            Dim ds = oSKD.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oSKD.GetData(NOIID).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try

                .DATEUPDATED = Now
                .KDSKD = NOIID
                .KDPENDAFTARAN = dsDaftar.KDPENDAFTARAN
                .DATE = deDATE.DateTime
                .ISCATEGORY = sISCATEGORY
                .KDDEPARTMENT = dsDaftar.KDDEPARTMENT
                .KDDOCTOR = dsDaftar.KDDOCTOR
                .NOMORRUJUKAN = dsDaftar.NOMORRUJUKAN
                .DESCRIPTION = ""
                Try
                    .ISCHEKED = oSKD.GetData(NOIID).ISCHEKED
                Catch oErr As Exception
                    .ISCHEKED = False
                End Try
                .KDUSER = sUserID
                .DATEKONTROL = deDATE.DateTime
                .ALASAN = ALASAN
                .TINDAKLANJUT = ALASAN
                .TANGGALPERIKSA_TEXT = deDATE.DateTime.ToString("ddMMyyyy")
                .KDJADWALDOKTER = ""
                .SEQ = 0
                .REQUEST = ""
                .RESPONSE = ""
                .NOMORSEP = dsDaftar.NOMORSEP
                .ISONLINE = False
                .ISSKD = IIf(sISCATEGORY = 0, False, True)
            End With

            If isAdd = True Then
                Dim KDSKD As String = oSKD.InsertData(ds, NOIID)

                If KDSKD <> "" Then
                    fn_SaveSKD = True
                Else
                    fn_SaveSKD = False
                End If
            Else
                fn_SaveSKD = oSKD.UpdateData(ds)
            End If
        Catch oErr As Exception
            MsgBox("Save SKD" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveSKD = False
        End Try
    End Function
    Private Sub fn_LoadTindakLanjutPertama(ByVal KDREG As String)
        Dim oSkd As New Admission.clsSKD
        Dim oRujukan As New Admission.clsRujukan

        Dim Paramater As String = txtTINDAKLANJUT.Text
        txtTINDAKLANJUT.ResetText()

        Dim TindakLanjut As String = String.Empty

        For Each xloop In oSkd.GetDataByKDREG(KDREG)

            Dim dsSKD = oSkd.GetDataNotOnline(xloop.KDSKD)
            If dsSKD IsNot Nothing Then
                If dsSKD.ALASAN = "KONTROL" Then
                    Dim sTindakLanjut As String = "Tanggal Kontrol : " & dsSKD.DATEKONTROL.ToString("dd-MM-yyyy") & vbCrLf & "Alasan : " & dsSKD.ALASAN.ToString.Trim & vbCrLf & "Tindak Lanjut : " & dsSKD.TINDAKLANJUT.ToString.Trim & vbCrLf & "Rencana Pemeriksaan Saat Kontrol Selanjutnya : " & dsSKD.DESCRIPTION.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "RUJUKAN EKSTERNAL" Then
                    Dim sTindakLanjut As String = "Tanggal Kontrol : " & dsSKD.DATEKONTROL.ToString("dd-MM-yyyy") & vbCrLf & "Alasan : " & dsSKD.ALASAN.ToString.Trim & vbCrLf & "Alasan di rujuk : " & dsSKD.DESCRIPTION.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "RUJUKAN HABIS" Then
                    Dim sTindakLanjut As String = "Tanggal Kontrol : " & dsSKD.DATEKONTROL.ToString("dd-MM-yyyy") & vbCrLf & "Alasan : " & dsSKD.ALASAN.ToString.Trim & vbCrLf & "Tindak Lanjut : " & dsSKD.TINDAKLANJUT.ToString.Trim & vbCrLf & "Rencana Pemeriksaan : " & dsSKD.DESCRIPTION.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "KONSUL INTERNAL" Then
                    Dim sTindakLanjut As String = "Tanggal Kontrol : " & dsSKD.DATEKONTROL.ToString("dd-MM-yyyy") & vbCrLf & "Alasan : " & dsSKD.ALASAN.ToString.Trim & vbCrLf & "Tujuan Poli : " & dsSKD.M_DEPARTMENT.NAME_DISPLAY.ToString.Trim & vbCrLf & "Dokter yang di tuju: " & dsSKD.M_DOCTOR.NAME_DISPLAY.ToString.Trim & vbCrLf & "Alasan di konsul : " & dsSKD.DESCRIPTION.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "PRB" Then
                    Dim sTindakLanjut As String = "Tanggal Kontrol : " & dsSKD.DATEKONTROL.ToString("dd-MM-yyyy") & vbCrLf & "Alasan : " & dsSKD.ALASAN.ToString.Trim & vbCrLf & "Alasan di rujuk : " & dsSKD.DESCRIPTION.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "RUJUK BALIK" Then
                    Dim sTindakLanjut As String = "Tanggal Kontrol : " & dsSKD.DATEKONTROL.ToString("dd-MM-yyyy") & vbCrLf & "Alasan : " & dsSKD.ALASAN.ToString.Trim & vbCrLf & "Alasan di rujuk : " & dsSKD.DESCRIPTION.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "SELESAI PENGOBATAN" Then
                    Dim sTindakLanjut As String = "Keterangan : " & dsSKD.ALASAN.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "RAWAT INAP" Then
                    Dim sTindakLanjut As String = "Keterangan : " & dsSKD.ALASAN.ToString.Trim & vbCrLf & sRemarks_IntruksiDokter
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "PENJADWALAN OPERASI" Then
                    Dim sTindakLanjut As String = "Keterangan : " & dsSKD.ALASAN.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "PENUNJANG HARI INI" Then
                    Dim sTindakLanjut As String = "Keterangan : " & dsSKD.ALASAN.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "ALIH RAWAT" Then
                    Dim sTindakLanjut As String = "Tanggal Kontrol : " & dsSKD.DATEKONTROL.ToString("dd-MM-yyyy") & vbCrLf & "Alasan : " & dsSKD.ALASAN.ToString.Trim & vbCrLf & "Tujuan Poli : " & dsSKD.M_DEPARTMENT.NAME_DISPLAY.ToString.Trim & vbCrLf & "Dokter yang di tuju: " & dsSKD.M_DOCTOR.NAME_DISPLAY.ToString.Trim & vbCrLf & "Alasan di rujuk : " & dsSKD.DESCRIPTION.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "ITERASI1" Then
                    Dim sTindakLanjut As String = "Tanggal Iterasi Ke 1 : " & dsSKD.DATEKONTROL.ToString("dd-MM-yyyy") & vbCrLf & "Alasan : " & dsSKD.ALASAN.ToString.Trim & vbCrLf & "Asal Poli : " & dsSKD.M_DEPARTMENT.NAME_DISPLAY.ToString.Trim & vbCrLf & "Dokter Pemberi Iterasi: " & dsSKD.M_DOCTOR.NAME_DISPLAY.ToString.Trim & vbCrLf & "Alasan di Iterasi : " & dsSKD.DESCRIPTION.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "ITERASI2" Then
                    Dim sTindakLanjut As String = "Tanggal Iterasi Ke 1 : " & dsSKD.DATEKONTROL.ToString("dd-MM-yyyy") & vbCrLf & "Tanggal Iterasi Ke 2 : " & dsSKD.DATE.AddDays(30).ToString("dd-MM-yyyy") & vbCrLf & "Alasan : " & dsSKD.ALASAN.ToString.Trim & vbCrLf & "Asal Poli : " & dsSKD.M_DEPARTMENT.NAME_DISPLAY.ToString.Trim & vbCrLf & "Dokter Pemberi Iterasi: " & dsSKD.M_DOCTOR.NAME_DISPLAY.ToString.Trim & vbCrLf & "Alasan di Iterasi : " & dsSKD.DESCRIPTION.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
            End If

            TindakLanjut = txtTINDAKLANJUT.Text
        Next

        Dim dsRujukan = oRujukan.GetDataByNoRegister(txtNoRegister.Text)
        If dsRujukan IsNot Nothing Then
            TindakLanjut = (TindakLanjut & vbCrLf & vbCrLf & IIf(dsRujukan.TIPERUJUKAN.ToString.Trim = 0, "Rujukan Eksternal Tanggal Rencana Kunjungan : ", IIf(dsRujukan.TIPERUJUKAN.ToString.Trim = 1, "Rujukan Eksternal Tanggal Rencana Kunjungan : ", "Rujuk Balik Tanggal Rencana Kunjungan : ")) & dsRujukan.DATERENCANAKONTROL.ToString("dd-MM-yyyy") & vbCrLf & "Tipe Rujukan : " & IIf(dsRujukan.TIPERUJUKAN.ToString.Trim = 0, "Penuh", IIf(dsRujukan.TIPERUJUKAN.ToString.Trim = 1, "Partial", "Rujuk Balik")) & vbCrLf & "Faskes " & dsRujukan.M_PPK.MEMO & vbCrLf & "Ke Poli " & dsRujukan.POLI_NAME_DISPLAY & vbCrLf & "Catatan di rujuk : " & dsRujukan.CATATAN.ToString.Trim).ToString.Trim
        End If

        txtTINDAKLANJUT.Text = TindakLanjut

        'Dim oSET_BOOKING_JADWALOPERASI As New WebService.clsSET_BOOKING_JADWALOPERASI
        'Dim dsOperasi = oSET_BOOKING_JADWALOPERASI.GetDataByKD(txtKDKUNJUNGAN.Text)
        'If dsOperasi IsNot Nothing Then
        '    TindakLanjut = (TindakLanjut & vbCrLf & vbCrLf & "Jadwal Operasi Tanggal " & dsOperasi.TANGGALOPERASI.ToString("dd-MM-yyyy") & vbCrLf & "Jenis Operasi " & dsOperasi.JENISOPERASI & vbCrLf & "Jenis Tindakan " & dsOperasi.JENISTINDAKAN & vbCrLf & "Catatan " & dsOperasi.REMARKS).ToString.Trim
        'End If


        'Dim dsKoding = oGrouperDataCppt.GetDataByKdKunjunganCPPTDokter(txtKDKUNJUNGAN.Text)
        'If dsKoding Is Nothing Then
        '    If Paramater = "" Then
        '        txtTINDAKLANJUT.Text = TindakLanjut
        '    Else
        '        txtTINDAKLANJUT.Text = Paramater & vbCrLf & TindakLanjut
        '    End If
        'Else
        '    txtTINDAKLANJUT.Text = TindakLanjut
        'End If
    End Sub
    Private Function fn_LoadTindakLanjutAlasanCek(ByVal KDREG As String, ByVal ALASAN As String) As String
        fn_LoadTindakLanjutAlasanCek = ""

        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM S_PENDAFTARAN_SKD WHERE KDPENDAFTARAN = '" & KDREG & "' AND ALASAN = '" & ALASAN & "'"

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_PENDAFTARAN_SKD")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            For iLoop As Integer = 0 To ds.Tables("S_PENDAFTARAN_SKD").Rows.Count - 1
                With ds.Tables("S_PENDAFTARAN_SKD")
                    fn_LoadTindakLanjutAlasanCek = .Rows(iLoop)("KDSKD").ToString()
                    sRemarks_Ruangan = .Rows(iLoop)("REQUEST").ToString()
                    sRemarks_RencanaPembedahan = .Rows(iLoop)("RESPONSE").ToString()
                    sRemarks_IntruksiDokter = .Rows(iLoop)("DESCRIPTION").ToString()
                End With
            Next

        Catch ex As Exception
            MsgBox("Tindak Lanjut : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub btnOrderLaboratorium_Click(sender As Object, e As EventArgs) Handles btnOrderLaboratorium.Click
        Dim frmOrderRanapLab As New frmOrderRanapLab

        Try
            If txtBERATBADAN.Text = "" Then
                MsgBox("Dibutuhkan Berat Badan", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            If txtTINGGIBADAN.Text = "" Then
                MsgBox("Dibutuhkan Tinggi Badan", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            Dim listdiagnosa As New List(Of String)

            For i As Integer = 0 To grvDIAGNOSA_CPPT.RowCount - 2
                listdiagnosa.Add(grvDIAGNOSA_CPPT.GetRowCellValue(i, colNAMADIAGNOSA_CPPT))
            Next

            'If listdiagnosa.Count <= 0 Then
            '    MsgBox("Dibutuhkan Diagnosa", MsgBoxStyle.Exclamation, Me.Text)
            '    Exit Sub
            'End If

            frmOrderRanapLab.LoadMe(FORM_MODE.FORM_MODE_ADD, sKDIDENTIAS, String.Join(", ", listdiagnosa.ToArray), txtBERATBADAN.Text, txtTINGGIBADAN.Text)
            frmOrderRanapLab.ShowDialog(Me)

            LoadPenunjang(txtNoRegister.Text)

        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmOrderRanapLab Is Nothing Then frmOrderRanapLab.Dispose()
            frmOrderRanapLab = Nothing
        End Try
    End Sub
    Private Sub btnOrderRadiologi_Click(sender As Object, e As EventArgs) Handles btnOrderRadiologi.Click
        Dim frmOrderRanapRad As New frmOrderRanapRad

        Try
            If txtBERATBADAN.Text = "" Then
                MsgBox("Dibutuhkan Berat Badan", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            If txtTINGGIBADAN.Text = "" Then
                MsgBox("Dibutuhkan Tinggi Badan", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            Dim listdiagnosa As New List(Of String)

            For i As Integer = 0 To grvDIAGNOSA_CPPT.RowCount - 2
                listdiagnosa.Add(grvDIAGNOSA_CPPT.GetRowCellValue(i, colNAMADIAGNOSA_CPPT))
            Next

            'If listdiagnosa.Count <= 0 Then
            '    MsgBox("Dibutuhkan Diagnosa", MsgBoxStyle.Exclamation, Me.Text)
            '    Exit Sub
            'End If

            frmOrderRanapRad.LoadMe(FORM_MODE.FORM_MODE_ADD, sKDIDENTIAS, String.Join(", ", listdiagnosa.ToArray), txtBERATBADAN.Text, txtTINGGIBADAN.Text)
            frmOrderRanapRad.ShowDialog(Me)

            LoadPenunjang(txtNoRegister.Text)

        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmOrderRanapRad Is Nothing Then frmOrderRanapRad.Dispose()
            frmOrderRanapRad = Nothing
        End Try
    End Sub
    Private Sub LoadPenunjang(ByVal kdreg As String)
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\RME\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "KODE = B.KDORDER  "
            SQL &= ",TanggalOrder = B.TANGGALORDER "
            SQL &= ",Penunjang = B.MEMO "
            SQL &= ",JenisPemeriksaan = C.NAMATINDAKAN "
            SQL &= ",Dokter = B.USERORDER "
            SQL &= ",Keterangan = B.STATUS "
            SQL &= "FROM "
            SQL &= "A_IDENTITASPASIEN_LIST A "
            SQL &= "INNER JOIN R_ORDER B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "INNER JOIN R_ORDER_RANAPLAB C "
            SQL &= "ON B.KDORDER = C.KDORDER "
            SQL &= "WHERE "
            SQL &= "A.KDPENDAFTARAN = '" & kdreg & "' "
            SQL &= "AND B.MEMO = 'ORDER LABORATORIUM' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KODE = B.KDORDER  "
            SQL &= ",TanggalOrder = B.TANGGALORDER "
            SQL &= ",Penunjang = B.MEMO "
            SQL &= ",JenisPemeriksaan = C.NAMATINDAKAN "
            SQL &= ",Dokter = B.USERORDER "
            SQL &= ",Keterangan = B.STATUS "
            SQL &= "FROM "
            SQL &= "A_IDENTITASPASIEN_LIST A "
            SQL &= "INNER JOIN R_ORDER B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "INNER JOIN R_ORDER_RANAPRAD C "
            SQL &= "ON B.KDORDER = C.KDORDER "
            SQL &= "WHERE "
            SQL &= "A.KDPENDAFTARAN = '" & kdreg & "' "
            SQL &= "AND B.MEMO = 'ORDER RADIOLOGI' "

            'SQL &= "ORDER BY B.TANGGALORDER "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ORDERPENUNJANG")

            grdPenunjang.DataSource = ds.Tables("ORDERPENUNJANG")
            grdPenunjang.ForceInitialize()

            Dim listPlanningTindakanPenunjang As New List(Of String)

            For iLoop As Integer = 0 To ds.Tables("ORDERPENUNJANG").Rows.Count - 1
                With ds.Tables("ORDERPENUNJANG")
                    listPlanningTindakanPenunjang.Add(.Rows(iLoop)("JenisPemeriksaan").ToString())
                End With
            Next

            sPenunjnag = String.Join(vbCrLf, listPlanningTindakanPenunjang.ToArray)


            For iLoop As Integer = 0 To grvPenunjang.Columns.Count - 1
                If grvPenunjang.Columns(iLoop).ColumnType.Name = "Decimal" Then
                    grvPenunjang.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    grvPenunjang.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                    grvPenunjang.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ElseIf grvPenunjang.Columns(iLoop).ColumnType.Name = "DateTime" Then
                    grvPenunjang.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                    grvPenunjang.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm}"
                End If
            Next

            grvPenunjang.Columns("KODE").VisibleIndex = -1

            grvPenunjang.BestFitColumns()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If


        Catch oErr As Exception
            MsgBox("Load Penunjang: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub EditOrderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditOrderToolStripMenuItem.Click
        If grvPenunjang.GetFocusedRowCellValue("KODE") Is Nothing Then
            Exit Sub
        End If

        If grvPenunjang.GetFocusedRowCellValue("Keterangan") <> "TAMBAH" Then
            MsgBox("Status" & vbCrLf & grvPenunjang.GetFocusedRowCellValue("Keterangan"), MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If grvPenunjang.GetFocusedRowCellValue("Penunjang") = "ORDER LABORATORIUM" Then
            Dim frmOrderRanapLab As New frmOrderRanapLab
            Try
                frmOrderRanapLab.LoadMe(FORM_MODE.FORM_MODE_EDIT, sKDIDENTIAS, "", "", "", grvPenunjang.GetFocusedRowCellValue("KODE"))
                frmOrderRanapLab.ShowDialog(Me)
                LoadPenunjang(txtNoRegister.Text)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmOrderRanapLab Is Nothing Then frmOrderRanapLab.Dispose()
                frmOrderRanapLab = Nothing
            End Try
        ElseIf grvPenunjang.GetFocusedRowCellValue("Penunjang") = "ORDER RADIOLOGI"
            Dim frmOrderRanapRad As New frmOrderRanapRad
            Try

                frmOrderRanapRad.LoadMe(FORM_MODE.FORM_MODE_EDIT, sKDIDENTIAS, "", "", "", grvPenunjang.GetFocusedRowCellValue("KODE"))
                frmOrderRanapRad.ShowDialog(Me)
                LoadPenunjang(txtNoRegister.Text)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmOrderRanapRad Is Nothing Then frmOrderRanapRad.Dispose()
                frmOrderRanapRad = Nothing
            End Try
        Else
            MsgBox("Order Tidak di Temukan", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub ViewOrderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewOrderToolStripMenuItem.Click
        If grvPenunjang.GetFocusedRowCellValue("KODE") Is Nothing Then
            Exit Sub
        End If

        If grvPenunjang.GetFocusedRowCellValue("Penunjang") = "ORDER LABORATORIUM" Then
            Dim frmOrderRanapLab As New frmOrderRanapLab
            Try
                frmOrderRanapLab.LoadMe(FORM_MODE.FORM_MODE_VIEW, sKDIDENTIAS, "", "", "", grvPenunjang.GetFocusedRowCellValue("KODE"))
                frmOrderRanapLab.ShowDialog(Me)
                LoadPenunjang(txtNoRegister.Text)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmOrderRanapLab Is Nothing Then frmOrderRanapLab.Dispose()
                frmOrderRanapLab = Nothing
            End Try
        ElseIf grvPenunjang.GetFocusedRowCellValue("Penunjang") = "ORDER RADIOLOGI"
            Dim frmOrderRanapRad As New frmOrderRanapRad
            Try
                frmOrderRanapRad.LoadMe(FORM_MODE.FORM_MODE_VIEW, sKDIDENTIAS, "", "", "", grvPenunjang.GetFocusedRowCellValue("KODE"))
                frmOrderRanapRad.ShowDialog(Me)
                LoadPenunjang(txtNoRegister.Text)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmOrderRanapRad Is Nothing Then frmOrderRanapRad.Dispose()
                frmOrderRanapRad = Nothing
            End Try
        Else
            MsgBox("Order Tidak di Temukan", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub

    Private Sub grdKDTEMPLATESELMAIGD_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDTEMPLATESELMAIGD.EditValueChanged
        If isLoad = True Then
            If grdKDTEMPLATESELMAIGD.Text <> "" Then
                Dim oTemplate As New EMedrek.clsTemplateNonRacikan

                Dim dsTemplate = oTemplate.GetDataDetail(grdKDTEMPLATESELMAIGD.EditValue)

                For Each iLoop In dsTemplate.OrderBy(Function(x) x.SEQ)
                    'txtDIAGNOOSA.Text = iLoop.S_REQ_RECIPE_TEMPLATE_H.DESCRIPTION
                    grvObatIGD.Focus()
                    grvObatIGD.AddNewRow()
                    grvObatIGD.SetFocusedRowCellValue(colKDITEM_ObatIGD, iLoop.KDITEM)
                    grvObatIGD.SetFocusedRowCellValue(colKDUOM_ObatIGD, iLoop.KDUOM)
                    grvObatIGD.SetFocusedRowCellValue(colKDSIGNA_ObatIGD, iLoop.KDSIGNA)
                    grvObatIGD.SetFocusedRowCellValue(colKDCARAPAKAI_ObatIGD, iLoop.KDCARAPAKAI)
                    grvObatIGD.SetFocusedRowCellValue(colQTY_ObatIGD, iLoop.JUMLAH)
                    grvObatIGD.SetFocusedRowCellValue(colREMARKS_DOKTER_ObatIGD, iLoop.REMARKS_DOKTER)
                    grvObatIGD.UpdateCurrentRow()
                Next
            End If
        End If
    End Sub

    Private Sub grdKDTEMPLATERANAP_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDTEMPLATERANAP.EditValueChanged
        If isLoad = True Then
            If grdKDTEMPLATERANAP.Text <> "" Then
                Dim oTemplate As New EMedrek.clsTemplateNonRacikan

                Dim dsTemplate = oTemplate.GetDataDetail(grdKDTEMPLATERANAP.EditValue)

                For Each iLoop In dsTemplate.OrderBy(Function(x) x.SEQ)
                    'txtDIAGNOOSA.Text = iLoop.S_REQ_RECIPE_TEMPLATE_H.DESCRIPTION
                    grvObatRanap.Focus()
                    grvObatRanap.AddNewRow()
                    grvObatRanap.SetFocusedRowCellValue(colKDITEM_ObatRanap, iLoop.KDITEM)
                    grvObatRanap.SetFocusedRowCellValue(colKDUOM_ObatRanap, iLoop.KDUOM)
                    grvObatRanap.SetFocusedRowCellValue(colKDSIGNA_ObatRanap, iLoop.KDSIGNA)
                    grvObatRanap.SetFocusedRowCellValue(colKDCARAPAKAI_ObatRanap, iLoop.KDCARAPAKAI)
                    grvObatRanap.SetFocusedRowCellValue(colQTY_ObatRanap, iLoop.JUMLAH)
                    grvObatRanap.SetFocusedRowCellValue(colREMARKS_DOKTER_ObatRanap, iLoop.REMARKS_DOKTER)
                    grvObatRanap.UpdateCurrentRow()
                Next
            End If
        End If
    End Sub
    Private Sub SimpleButton6_Click(sender As Object, e As EventArgs) Handles SimpleButton6.Click
        Dim frmOrderRanapNonRacikanList As New frmOrderRanapNonRacikanList
        Try
            Dim oPendaftaran As New Admission.clsPendaftaran
            Dim listdiagnosa As New List(Of String)
            For i As Integer = 0 To grvDIAGNOSA_CPPT.RowCount - 2
                listdiagnosa.Add(grvDIAGNOSA_CPPT.GetRowCellValue(i, colNAMADIAGNOSA_CPPT))
            Next
            Dim dsDaftar = oPendaftaran.GetData(txtNoRegister.Text)
            If dsDaftar IsNot Nothing Then
                frmOrderRanapNonRacikanList.fn_LoadData(sKDIDENTIAS, txtNoRegister.Text, String.Join(", ", listdiagnosa.ToArray), txtBERATBADAN.Text, txtTINGGIBADAN.Text)
                frmOrderRanapNonRacikanList.ShowDialog(Me)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmOrderRanapNonRacikanList Is Nothing Then frmOrderRanapNonRacikanList.Dispose()
            frmOrderRanapNonRacikanList = Nothing

            grvObatIGD.OptionsSelection.MultiSelect = True
            grvObatIGD.SelectAll()
            grvObatIGD.DeleteSelectedRows()
            grvObatIGD.OptionsSelection.MultiSelect = False

            Dim oOrderRanapNonRacikan As New Order.clsOrderRanapNonRacikan
            For Each iLoop In oOrderRanapNonRacikan.GetDataDetailBykdpendaftaran(txtNoRegister.Text)
                grvObatIGD.Focus()
                grvObatIGD.AddNewRow()
                grvObatIGD.SetFocusedRowCellValue(colKDITEM_ObatIGD, iLoop.KDITEM)
                grvObatIGD.SetFocusedRowCellValue(colKDUOM_ObatIGD, iLoop.KDUOM)
                grvObatIGD.SetFocusedRowCellValue(colKDSIGNA_ObatIGD, iLoop.KDSIGNA)
                grvObatIGD.SetFocusedRowCellValue(colKDCARAPAKAI_ObatIGD, iLoop.KDCARAPAKAI)
                grvObatIGD.SetFocusedRowCellValue(colQTY_ObatIGD, iLoop.JUMLAH)
                grvObatIGD.SetFocusedRowCellValue(colREMARKS_DOKTER_ObatIGD, iLoop.REMARKS_DOKTER)
                grvObatIGD.UpdateCurrentRow()
            Next
        End Try
    End Sub
    Private Sub SimpleButton7_Click(sender As Object, e As EventArgs) Handles SimpleButton7.Click
        Dim frmBHPPasienList As New frmBHPPasienList
        Try
            frmBHPPasienList.fn_LoadData(sKDIDENTIAS, txtNoRegister.Text)
            frmBHPPasienList.ShowDialog(Me)

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmBHPPasienList Is Nothing Then frmBHPPasienList.Dispose()
            frmBHPPasienList = Nothing

        End Try
    End Sub
#End Region
End Class