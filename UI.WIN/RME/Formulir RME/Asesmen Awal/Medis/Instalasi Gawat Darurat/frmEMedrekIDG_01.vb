Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmEMedrekIDG_01
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_IGD_01 As New Digital.clsDigital_IGD_01
    Private down As Boolean = False
    Private sKoneksi As String = String.Empty
    Private sKDDOCTOR As String = String.Empty

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String)
        oFormMode = FormMode

        Me.Text = EMedrekIGD_01.TITLE

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
            txtNamaPasien.Text = dsPendaftaran.NAMAPASIEN.ToString.Trim.ToUpper
            txtJenisKelamin.Text = dsPendaftaran.JENISKELAMIN
            txtTempatTglLahir.Text = dsPendaftaran.TEMPATLAHIR.Trim.ToUpper + ", " + dsPendaftaran.TANGGALLAHIR.ToString("dd-MM-yyyy")
            txtUmur.Text = dsPendaftaran.USIA
            txtPangkatNRP.Text = dsPendaftaran.PANGKAT.Trim.ToUpper + " / " + dsPendaftaran.NRP
            txtKesatuan.Text = dsPendaftaran.KESATUAN
            txtNik.Text = dsPendaftaran.NIK
            txtNoRegister.Text = dsPendaftaran.KDKUNJUNGAN
            txtTanggalDaftar.Text = dsPendaftaran.DATE.ToString("dd-MM-yyyy HH:mm:ss")
            txtJenisPeserta.Text = dsPendaftaran.PENJAMIN
            txtNoBPJS.Text = dsPendaftaran.KARTUBPJS
            txtAlamat.Text = dsPendaftaran.ALAMAT.Trim.ToUpper
            txtTujuan.Text = dsPendaftaran.TUJUAN
            txtDokter.Text = dsPendaftaran.DOKTER
           
            sKDDOCTOR = dsPendaftaran.KDDOKTER
            Dim oSetUser As New Setting.clsUser
            Dim dsSetUser = oSetUser.GetData(sUserID)
            If dsSetUser IsNot Nothing Then
                sKDDOCTOR = dsSetUser.KDDOCTOR
            End If


        Else
            txtNoPasien.ResetText()
            txtNamaPasien.ResetText()
            txtJenisKelamin.ResetText()
            txtTempatTglLahir.ResetText()
            txtUmur.ResetText()
            txtPangkatNRP.ResetText()
            txtKesatuan.ResetText()
            txtNik.ResetText()
            txtNoRegister.ResetText()
            txtTanggalDaftar.ResetText()
            txtJenisPeserta.ResetText()
            txtNoBPJS.ResetText()
            txtAlamat.ResetText()
            txtTujuan.ResetText()
            txtDokter.ResetText()

            sKDDOCTOR = String.Empty
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
        fn_NOIDUSER()
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
        btnDiagnosa.Enabled = Not Status
        btnReload.Enabled = Not Status

        chkTRIAGE_TRAUMA.Properties.ReadOnly = Status
        chkTRIAGE_NONTRAUMA.Properties.ReadOnly = Status
        chkTRIAGE_MATERNITY.Properties.ReadOnly = Status
        chkTRIAGE_RED.Properties.ReadOnly = Status
        chkTRIAGE_YELLOW.Properties.ReadOnly = Status
        chkTRIAGE_GREEN.Properties.ReadOnly = Status
        chkTRIAGE_BLACK.Properties.ReadOnly = Status
        chkTRIAGE_AIRWAY.Properties.ReadOnly = Status
        chkTRIAGE_BREATHING.Properties.ReadOnly = Status
        chkTRIAGE_CIRCULATION.Properties.ReadOnly = Status
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
        txtTINDAKLANJUT_TUJUAN.Properties.ReadOnly = Status

        grdKDDOCTOR.Properties.ReadOnly = Status
        txtKDDIAGNOSA.Properties.ReadOnly = Status
        chkKESIMPULAN_PERBAIKAN.Properties.ReadOnly = Status
        chkKESIMPULAN_STABIL.Properties.ReadOnly = Status
        chkKESIMPULAN_PERBURUKAN.Properties.ReadOnly = Status
        chkTINDAKLANJUT_RUJUK.Properties.ReadOnly = Status
        chkTINDAKLANJUT_RAWAT.Properties.ReadOnly = Status
        chkTINDAKLANJUT_PULANGPAKSA.Properties.ReadOnly = Status
        chkTINDAKLANJUT_PULANG.Properties.ReadOnly = Status
        txtLOKASI.Properties.ReadOnly = Status
        txtSAATPASIENPULANG_TEXT.Properties.ReadOnly = Status
        deDATEPUKUL.Properties.ReadOnly = Status
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
        txtDOKTER_.Properties.ReadOnly = Status
        txtOBATSAATPULANG.Properties.ReadOnly = Status
        grdPerawat.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        'txtCODE.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        chkTRIAGE_TRAUMA.Checked = False
        chkTRIAGE_NONTRAUMA.Checked = False
        chkTRIAGE_MATERNITY.Checked = False
        chkTRIAGE_RED.Checked = False
        chkTRIAGE_YELLOW.Checked = False
        chkTRIAGE_GREEN.Checked = False
        chkTRIAGE_BLACK.Checked = False
        chkTRIAGE_AIRWAY.Checked = False
        chkTRIAGE_BREATHING.Checked = False
        chkTRIAGE_CIRCULATION.Checked = False
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
        chkNORMAL_KEPALA.Checked = False
        chkTIDAK_NORMAL_KEPALA.Checked = False
        txtTidakNormalKepala.ResetText()
        chkNORMAL_MATA.Checked = False
        chkTIDAK_NORMAL_MATA.Checked = False
        txtTidakNormalMata.ResetText()
        chkNORMAL_LEHER.Checked = False
        chkTIDAK_NORMAL_LEHER.Checked = False
        txtTidakNormalLeher.ResetText()
        chkNORMAL_DADA.Checked = False
        chkTIDAK_NORMAL_DADA.Checked = False
        txtTidakNormalDada.ResetText()
        chkNORMAL_PERUT.Checked = False
        chkTIDAK_NORMAL_PERUT.Checked = False
        txtTidakNormalPerut.ResetText()
        chkNORMAL_ALATGERAK.Checked = False
        chkTIDAK_NORMAL_ALATGERAK.Checked = False
        txtTidakNormalAlatGerak.ResetText()
        txtTINDAKLANJUT_TUJUAN.ResetText()

        grdKDDOCTOR.ResetText()
        txtKDDIAGNOSA.ResetText()
        chkKESIMPULAN_PERBAIKAN.Checked = False
        chkKESIMPULAN_STABIL.Checked = False
        chkKESIMPULAN_PERBURUKAN.Checked = False
        chkTINDAKLANJUT_RUJUK.Checked = False
        chkTINDAKLANJUT_RAWAT.Checked = False
        chkTINDAKLANJUT_PULANGPAKSA.Checked = False
        chkTINDAKLANJUT_PULANG.Checked = False
        txtLOKASI.ResetText()
        txtSAATPASIENPULANG_TEXT.ResetText()
        deDATEPUKUL.DateTime = Now
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
        txtDOKTER_.ResetText()
        txtOBATSAATPULANG.ResetText()
        grdPerawat.ResetText()

        grdKDDOCTOR.Text = sKDDOCTOR
    End Sub
    Private Sub fn_LoadAdsemenPerawat()
        Dim oDigital As New Digital.clsDigital_IGD_02
        Dim dsDigital = oDigital.GetData(txtNoRegister.Text)
        If dsDigital IsNot Nothing Then
            chkTRIAGE_DATANGSENDIRI.Checked = dsDigital.BIT_4
            chkTRIAGE_RED.Checked = dsDigital.BIT_6
            chkTRIAGE_YELLOW.Checked = dsDigital.BIT_7
            chkTRIAGE_GREEN.Checked = dsDigital.BIT_8
            chkTRIAGE_BLACK.Checked = dsDigital.BIT_8
            grdPETUGASTRIAGE.Text = dsDigital.KDUSER
            cboTRIAGE_AIRWAY.Text = dsDigital.TEXT_3
            cboTRIAGE_BREATHING.Text = dsDigital.TEXT_4
            cboTRIAGE_CIRCULATION.Text = dsDigital.TEXT_5
            txtRIWAYAT.Text = dsDigital.TEXT_97
            txtGCS.Text = dsDigital.TEXT_63
            cboE.Text = dsDigital.TEXT_64
            cboM.Text = dsDigital.TEXT_65
            cboV.Text = dsDigital.TEXT_66
            txtBP.Text = dsDigital.TEXT_34 & "/" & dsDigital.TEXT_35
            txtHR.Text = dsDigital.TEXT_33
            txtT.Text = dsDigital.TEXT_36
            txtRR.Text = dsDigital.TEXT_19
            txtSPO2.Text = dsDigital.TEXT_20
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
        Else
            MsgBox("Asesmen Perawat belum di input", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_IGD_01.GetData(txtNoRegister.Text)

            With ds
                deDATE.DateTime = .DATEUPDATED
                chkTRIAGE_TRAUMA.Checked = .TRIAGE_TRAUMA
                chkTRIAGE_NONTRAUMA.Checked = .TRIAGE_NONTRAUMA
                chkTRIAGE_MATERNITY.Checked = .TRIAGE_MATERNITY
                chkTRIAGE_RED.Checked = .TRIAGE_RED
                chkTRIAGE_YELLOW.Checked = .TRIAGE_YELLOW
                chkTRIAGE_GREEN.Checked = .TRIAGE_GREEN
                chkTRIAGE_BLACK.Checked = .TRIAGE_BLACK
                chkTRIAGE_AIRWAY.Checked = .TRIAGE_AIRWAY
                chkTRIAGE_BREATHING.Checked = .TRIAGE_BREATHING
                chkTRIAGE_CIRCULATION.Checked = .TRIAGE_CIRCULATION
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
                txtTINDAKLANJUT_TUJUAN.Text = .TUJUAN

                Try
                    'Dim img = (From x In oS_DIGITAL_IGD_01.GetData
                    '           Where x.KDREG = txtNoRegister.Text
                    '           Select x.ATTACHMENT_2).Single

                    Dim img = oS_DIGITAL_IGD_01.GetData(txtNoRegister.Text).ATTACHMENT_1

                    picGAMBAR2.Image = ByteArrayToImage(img.ToArray())
                Catch oErr As Exception
                End Try

                grdKDDOCTOR.Text = .DOCTOR_KODE
                txtKDDIAGNOSA.Text = .KDDIAGNOSA
                chkKESIMPULAN_PERBAIKAN.Checked = .KESIMPULAN_PERBAIKAN
                chkKESIMPULAN_STABIL.Checked = .KESIMPULAN_STABIL
                chkKESIMPULAN_PERBURUKAN.Checked = .KESIMPULAN_PERBURUKAN
                chkTINDAKLANJUT_RUJUK.Checked = .TINDAKLANJUT_RUJUK
                chkTINDAKLANJUT_RAWAT.Checked = .TINDAKLANJUT_RAWAT
                chkTINDAKLANJUT_PULANGPAKSA.Checked = .TINDAKLANJUT_PULANGPAKSA
                chkTINDAKLANJUT_PULANG.Checked = .TINDAKLANJUT_PULANG
                txtLOKASI.Text = .TUJUAN
                txtSAATPASIENPULANG_TEXT.Text = .SAATPASIENPULANG_TEXT
                deDATEPUKUL.DateTime = .SAATPASIENPULANG_TIME
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
                txtDOKTER_.Text = .PERAWATANLANJUTAN_DOKTER
                txtOBATSAATPULANG.Text = .OBATSAATPULANG
                grdPerawat.Text = .PERAWATPENANGUNGJAWAB

                BindingSource.DataSource = oS_DIGITAL_IGD_01.GetDataDetail(txtNoRegister.Text).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = BindingSource
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
            If grdPETUGASTRIAGE.Text = String.Empty Then
                MsgBox("Dibutuhkan Petugas Triage", MsgBoxStyle.Exclamation, Me.Text)
                grdPETUGASTRIAGE.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdPerawat.Text = String.Empty Then
                MsgBox("Dibutuhkan Perawat", MsgBoxStyle.Exclamation, Me.Text)
                grdPerawat.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDOCTOR.Text = String.Empty Then
                MsgBox("Dibutuhkan Dokter", MsgBoxStyle.Exclamation, Me.Text)
                grdKDDOCTOR.Focus()
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
            Dim ds = oS_DIGITAL_IGD_01.GetStructureHeader
            With ds
                .KDKUNJUNGAN = txtNoRegister.Text
                Try
                    .DATECREATED = oS_DIGITAL_IGD_01.GetData(txtNoRegister.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try

                .DATEUPDATED = deDATE.DateTime
                .TRIAGE_TRAUMA = chkTRIAGE_TRAUMA.Checked
                .TRIAGE_NONTRAUMA = chkTRIAGE_NONTRAUMA.Checked
                .TRIAGE_MATERNITY = chkTRIAGE_MATERNITY.Checked
                .TRIAGE_RED = chkTRIAGE_RED.Checked
                .TRIAGE_YELLOW = chkTRIAGE_YELLOW.Checked
                .TRIAGE_GREEN = chkTRIAGE_GREEN.Checked
                .TRIAGE_BLACK = chkTRIAGE_BLACK.Checked
                .TRIAGE_AIRWAY = chkTRIAGE_AIRWAY.Checked
                .TRIAGE_BREATHING = chkTRIAGE_BREATHING.Checked
                .TRIAGE_CIRCULATION = chkTRIAGE_CIRCULATION.Checked
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
                .SURVEY_KEPALA_1 = ""
                .NORMAL_KEPALA = chkNORMAL_KEPALA.Checked
                .TIDAK_NORMAL_KEPALA = chkTIDAK_NORMAL_KEPALA.Checked
                .SURVEY_KEPALA_2 = txtTidakNormalKepala.Text
                .SURVEY_MATA_1 = ""
                .NORMAL_MATA = chkNORMAL_MATA.Checked
                .TIDAK_NORMAL_MATA = chkTIDAK_NORMAL_MATA.Checked
                .SURVEY_MATA_2 = txtTidakNormalMata.Text
                .SURVEY_LEHER_1 = ""
                .NORMAL_LEHER = chkNORMAL_LEHER.Checked
                .TIDAK_NORMAL_LEHER = chkTIDAK_NORMAL_LEHER.Checked
                .SURVEY_LEHER_2 = txtTidakNormalLeher.Text
                .SURVEY_DADA_1 = ""
                .NORMAL_DADA = chkNORMAL_DADA.Checked
                .TIDAK_NORMAL_DADA = chkTIDAK_NORMAL_DADA.Checked
                .SURVEY_DADA_2 = txtTidakNormalDada.Text
                .SURVEY_PERUT_1 = ""
                .NORMAL_PERUT = chkNORMAL_PERUT.Checked
                .TIDAK_NORMAL_PERUT = chkTIDAK_NORMAL_PERUT.Checked
                .SURVEY_PERUT_2 = txtTidakNormalPerut.Text
                .SURVEY_ALATGERAK_1 = ""
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
                        .ATTACHMENT_2 = oS_DIGITAL_IGD_01.GetData(txtNoRegister.Text).ATTACHMENT_2
                    Catch ex As Exception

                    End Try
                End Try

                .DOCTOR_KODE = grdKDDOCTOR.EditValue
                .DOCTOR_NAME_DISPLAY = grdKDDOCTOR.Text
                .KDDIAGNOSA = txtKDDIAGNOSA.Text
                .KESIMPULAN_PERBAIKAN = chkKESIMPULAN_PERBAIKAN.Checked
                .KESIMPULAN_STABIL = chkKESIMPULAN_STABIL.Checked
                .KESIMPULAN_PERBURUKAN = chkKESIMPULAN_PERBURUKAN.Checked
                .TINDAKLANJUT_RUJUK = chkTINDAKLANJUT_RUJUK.Checked
                .TINDAKLANJUT_RAWAT = chkTINDAKLANJUT_RAWAT.Checked
                .TINDAKLANJUT_PULANGPAKSA = chkTINDAKLANJUT_PULANGPAKSA.Checked
                .TINDAKLANJUT_PULANG = chkTINDAKLANJUT_PULANG.Checked
                .TUJUAN = txtTINDAKLANJUT_TUJUAN.Text
                .SAATPASIENPULANG_TEXT = txtSAATPASIENPULANG_TEXT.Text
                .SAATPASIENPULANG_TIME = deDATEPUKUL.DateTime
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
                .PERAWATANLANJUTAN_DOKTER = txtDOKTER_.Text
                .OBATSAATPULANG = txtOBATSAATPULANG.Text

                Try
                    .CETAK = oS_DIGITAL_IGD_01.GetData(txtNoRegister.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try
                .PERAWATPENANGUNGJAWAB = grdPerawat.EditValue
                .KDUSER = sUserID
                .KDUSER_SIGNATURE = sUserSIGNATURE
            End With

            ' ***** DETIL *****
            Dim arrDetail = oS_DIGITAL_IGD_01.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oS_DIGITAL_IGD_01.GetStructureDetail
                With dsDetail
                    .SEQ = i
                    .KDKUNJUNGAN = ds.KDKUNJUNGAN
                    .PUKUL = CDate(grvDetail.GetRowCellValue(i, colPUKUL))
                    .PENANGANAN = grvDetail.GetRowCellValue(i, colPENANGANAN)
                    .NAMA = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colNAMA)), "-", grvDetail.GetRowCellValue(i, colNAMA))
                    .PARAF = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colPARAF)), "-", grvDetail.GetRowCellValue(i, colPARAF))
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_IGD_01.InsertData(ds, arrDetail)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_IGD_01.UpdateData(ds, arrDetail)
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
            Case Keys.F6
                If btnDiagnosa.Enabled = True Then
                    btnDiagnosa_Click()
                End If
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
                listDiagnosa.Add(xloop.KATEGORI & " . " & xloop.REMARKS)
            Next

            Dim listProsedur As New List(Of String)
            For Each xloop In oMasterDiagnosa.GetDataDetail_(txtNoRegister.Text)
                listProsedur.Add(xloop.SEQ + 1 & ". " & xloop.REMARKS)
            Next

            txtKDDIAGNOSA.Text = "Diagnosa " & vbCrLf & String.Join(vbCrLf, listDiagnosa.ToArray) & IIf(String.Join(vbCrLf, listProsedur.ToArray) <> "", vbCrLf & " Prosedur " & vbCrLf & String.Join(vbCrLf, listProsedur.ToArray), "")
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
    Private Sub ResetGambarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResetGambarToolStripMenuItem.Click
        picGAMBAR2.Image = CType(My.Resources.ResourceManager.GetObject("gambar_2"), Image)
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_NOIDUSER()
        Dim oUser As New Setting.clsUser
        Try
            grdPETUGASTRIAGE.Properties.DataSource = oUser.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdPETUGASTRIAGE.Properties.ValueMember = "KDUSER"
            grdPETUGASTRIAGE.Properties.DisplayMember = "KDUSER"

            grdPerawat.Properties.DataSource = oUser.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdPerawat.Properties.ValueMember = "KDUSER"
            grdPerawat.Properties.DisplayMember = "KDUSER"

            grdUSER.DataSource = oUser.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdUSER.ValueMember = "KDUSER"
            grdUSER.DisplayMember = "KDUSER"
        Catch oErr As Exception
            MsgBox("Load User Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDDOCTOR()
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
    Private Sub cboE_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboE.SelectedIndexChanged, cboM.SelectedIndexChanged, cboV.SelectedIndexChanged
        If isLoad = True Then
            Dim sE As Decimal = 0
            Dim sM As Decimal = 0
            Dim sV As Decimal = 0

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
    End Sub
    Private Sub cboSAATPULANG_E_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSAATPULANG_E.SelectedIndexChanged, cboSAATPULANG_M.SelectedIndexChanged, cboSAATPULANG_V.SelectedIndexChanged
        If isLoad = True Then
            Dim sE As Decimal = 0
            Dim sM As Decimal = 0
            Dim sV As Decimal = 0

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
    Private Sub btnTambah_Click(sender As Object, e As EventArgs)
        Dim frmPopUpPenanganandanPenilaianUlang As New frmPopUpPenanganandanPenilaianUlang
        frmPopUpPenanganandanPenilaianUlang.fn_LoadMe(Now, "", "", "")
        frmPopUpPenanganandanPenilaianUlang.ShowDialog()

        If sFind1 <> String.Empty Then
            grvDetail.Focus()
            grvDetail.AddNewRow()
            grvDetail.SetFocusedRowCellValue(colPUKUL, sDATEFrom)
            grvDetail.SetFocusedRowCellValue(colPENANGANAN, sFind1)
            grvDetail.SetFocusedRowCellValue(colNAMA, sFind2)
            grvDetail.SetFocusedRowCellValue(colPARAF, sFind3)

            grvDetail.UpdateCurrentRow()
        End If

        sFind1 = String.Empty
        sFind2 = String.Empty
        sFind3 = String.Empty

        grdDetail.Focus()
    End Sub
    Private Sub EditToolStripMenuItem_Click(sender As Object, e As EventArgs) 
        Dim frmPopUpPenanganandanPenilaianUlang As New frmPopUpPenanganandanPenilaianUlang
        frmPopUpPenanganandanPenilaianUlang.fn_LoadMe(grvDetail.GetFocusedRowCellValue(colPUKUL), grvDetail.GetFocusedRowCellValue(colPENANGANAN), grvDetail.GetFocusedRowCellValue(colNAMA), grvDetail.GetFocusedRowCellValue(colPARAF))
        frmPopUpPenanganandanPenilaianUlang.ShowDialog()

        If sFind1 <> String.Empty Then
            grvDetail.SetFocusedRowCellValue(colPUKUL, sDATEFrom)
            grvDetail.SetFocusedRowCellValue(colPENANGANAN, sFind1)
            grvDetail.SetFocusedRowCellValue(colNAMA, sFind2)
            grvDetail.SetFocusedRowCellValue(colPARAF, sFind3)

            grvDetail.UpdateCurrentRow()
        End If

        sFind1 = String.Empty
        sFind2 = String.Empty
        sFind3 = String.Empty

        grdDetail.Focus()
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        grvDetail.DeleteSelectedRows()
    End Sub
    Private Sub btnEDITIMAGE_Click(sender As Object, e As EventArgs) Handles btnEDITIMAGE.Click
        Dim frmPopUp_Image As New frmPopUp_img
        frmPopUp_Image.fn_LoadMe(picGAMBAR2.Image)
        frmPopUp_Image.ShowDialog()

        If sPicture IsNot Nothing Then
            picGAMBAR2.Image = sPicture
        End If

        picGAMBAR2.Focus()
        sFind10 = String.Empty
        sPicture = Nothing
    End Sub
    Private Sub btnReload_Click(sender As Object, e As EventArgs) Handles btnReload.Click
        grvDetail.OptionsSelection.MultiSelect = True
        grvDetail.SelectAll()
        grvDetail.DeleteSelectedRows()
        grvDetail.OptionsSelection.MultiSelect = False

        Dim dsKunjungan = oS_DIGITAL_IGD_01.GetDataByKunjungan(txtNoRegister.Text)
        If dsKunjungan IsNot Nothing Then
            'Dim oOrderTindakan As New Inventory.clsOrderTindakan
            'Dim oOrderLab As New Inventory.clsOrderLab
            'Dim oOrderRad As New Inventory.clsOrderRad
            'Dim oKonsul As New Digital.clsKonsul
            'Dim oKonsulJawab As New Digital.clsJawabKonsul

            'Dim dsTindakan = From x In oOrderTindakan.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
            '                 Join y In oOrderTindakan.GetDataDetail()
            '                 On x.KDORDERTINDAKAN Equals y.KDORDERTINDAKAN
            '                 Select TANGGAL = x.DATE, ITEM = "Tindakan : " & y.TARIFKT & ", Catatan : " & y.REMARKS

            'Dim dsLab = From x In oOrderLab.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
            '            Join y In oOrderLab.GetDataDetail()
            '            On x.KDORDERLAB Equals y.KDORDERLAB
            '            Select TANGGAL = x.DATE, ITEM = "Lab : " & y.TARIFKT  & ", Catatan : " & y.REMARKS

            'Dim dsRad = From x In oOrderRad.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
            '            Join y In oOrderRad.GetDataDetail()
            '            On x.KDORDERRAD Equals y.KDORDERRAD
            '            Select TANGGAL = x.DATE, ITEM = "Rad : " & y.TARIFKT & ", Catatan : " & y.REMARKS

            'Dim dsKonsul = From x In oKonsul.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
            '               Select TANGGAL = x.DATE, ITEM = "Konsul : " & "Dari " & x.DOKTER_DARI & " Kepada " & x.DOKTER_KEPADA & " Isi " & x.MEMO & ", "

            'Dim dsJawabKonsul = From x In oKonsulJawab.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
            '                    Select TANGGAL = x.DATE, ITEM = "Jawab Konsul : " & "Dari " & x.DOKTER_DARI & " Kepada " & x.DOKTER_KEPADA & " Isi " & x.MEMO & ", "

            'Dim dsUnionAll = dsTindakan.Union(dsLab).Union(dsRad).Union(dsKonsul).Union(dsJawabKonsul)


            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\EMEDREK\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT TANGGAL = A.DATE, ITEM = CONCAT('Tindakan : ' , B.TARIFKT, ', Catatan : ' , B.REMARKS) FROM I_ORDERTINDAKAN_H A "
            SQL &= "INNER JOIN I_ORDERTINDAKAN_D B ON A.KDORDERTINDAKAN = B.KDORDERTINDAKAN "
            SQL &= "INNER JOIN R_IDENTITAS_PASIEN C ON C.KDKUNJUNGAN = A.KDKUNJUNGAN "
            SQL &= "WHERE C.KDPENDAFTARAN = '" & dsKunjungan.KDPENDAFTARAN & "' "

            SQL &= "UNION  "

            SQL &= "SELECT TANGGAL = A.DATE, ITEM = CONCAT('Lab : ' , B.TARIFKT, ', Catatan : ' , B.REMARKS) FROM I_ORDERLAB_H A  "
            SQL &= "INNER JOIN I_ORDERLAB_D B ON A.KDORDERLAB = B.KDORDERLAB "
            SQL &= "INNER JOIN R_IDENTITAS_PASIEN C ON C.KDKUNJUNGAN = A.KDKUNJUNGAN "
            SQL &= "WHERE C.KDPENDAFTARAN = '" & dsKunjungan.KDPENDAFTARAN & "' "

            SQL &= "UNION  "

            SQL &= "SELECT TANGGAL = A.DATE, ITEM = CONCAT('Rad : ' , B.TARIFKT, ', Catatan : ' , B.REMARKS) FROM I_ORDERRAD_H A  "
            SQL &= "INNER JOIN I_ORDERRAD_D B ON A.KDORDERRAD = B.KDORDERRAD "
            SQL &= "INNER JOIN R_IDENTITAS_PASIEN C ON C.KDKUNJUNGAN = A.KDKUNJUNGAN "
            SQL &= "WHERE C.KDPENDAFTARAN = '" & dsKunjungan.KDPENDAFTARAN & "' "

            SQL &= "UNION  "

            SQL &= "SELECT TANGGAL = A.DATE, ITEM = CONCAT('Konsul Dari : ' , A.DOKTER_DARI, ', Kepada : ' , A.DOKTER_KEPADA , ', Isi : ' , A.MEMO) FROM S_DIGITAL_KONSULTASI A  "
            SQL &= "INNER JOIN R_IDENTITAS_PASIEN C ON C.KDKUNJUNGAN = A.KDKUNJUNGAN "
            SQL &= "WHERE C.KDPENDAFTARAN = '" & dsKunjungan.KDPENDAFTARAN & "' "

            SQL &= "UNION  "

            SQL &= "SELECT TANGGAL = A.DATE, ITEM = CONCAT('Jawab Konsul Dari : ' , A.DOKTER_DARI, ', Kepada : ' , A.DOKTER_KEPADA , ', Isi : ' , A.MEMO) FROM S_DIGITAL_JAWABKONSUL A  "
            SQL &= "INNER JOIN R_IDENTITAS_PASIEN C ON C.KDKUNJUNGAN = A.KDKUNJUNGAN "
            SQL &= "WHERE C.KDPENDAFTARAN = '" & dsKunjungan.KDPENDAFTARAN & "' "


            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ASMEDIGD")

            For iLoop As Integer = 0 To ds.Tables("ASMEDIGD").Rows.Count - 1
                With ds.Tables("ASMEDIGD")
                    grvDetail.Focus()
                    grvDetail.AddNewRow()
                    grvDetail.SetFocusedRowCellValue(colPUKUL, .Rows(iLoop)("TANGGAL"))
                    grvDetail.SetFocusedRowCellValue(colPENANGANAN, .Rows(iLoop)("ITEM"))
                    grvDetail.SetFocusedRowCellValue(colNAMA, "")
                    grvDetail.SetFocusedRowCellValue(colPARAF, "")

                    grvDetail.UpdateCurrentRow()
                End With
            Next

        End If
    End Sub
#End Region
End Class