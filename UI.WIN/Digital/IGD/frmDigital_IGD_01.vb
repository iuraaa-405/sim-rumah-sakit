Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmDigital_IGD_01
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oDigital_IGD_01 As New Digital.clsDigital_IGD_01
    Private down As Boolean = False
    Private PopUP As Boolean = False

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = Digital_IGD_01.TITLE

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtCODE.Text.Trim.ToUpper
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
        chkTRIAGE_Trauma.Properties.ReadOnly = Status
        chkTRIAGE_NonTrauma.Properties.ReadOnly = Status
        chkTRIAGE_Circulation.Properties.ReadOnly = Status
        chkTRIAGE_Red.Properties.ReadOnly = Status
        chkTRIAGE_Yellow.Properties.ReadOnly = Status
        chkTRIAGE_Green.Properties.ReadOnly = Status
        chkTRIAGE_Black.Properties.ReadOnly = Status
        chkTRIAGE_Airway.Properties.ReadOnly = Status
        chkTRIAGE_Breathing.Properties.ReadOnly = Status
        chkTRIAGE_Circulation.Properties.ReadOnly = Status
        cboTRIAGE_Airway.Properties.ReadOnly = Status
        cboTRIAGE_Breathing.Properties.ReadOnly = Status
        cboTRIAGE_Circulation.Properties.ReadOnly = Status
        chkTRIAGE_PasienDatangSendiri.Properties.ReadOnly = Status
        chkTRIAGE_PasienDiantar.Properties.ReadOnly = Status
        cboTRIAGE_PasienDiantar.Properties.ReadOnly = Status
        chkPENGKAJIAN_AutoAnamnesa.Properties.ReadOnly = Status
        chkPENGKAJIAN_AlloAnamnesa.Properties.ReadOnly = Status
        dePENGKAJIAN_PukulPeriksa.Properties.ReadOnly = Status
        dePENKAJIAN_PukulRawat.Properties.ReadOnly = Status
        grdTRIAGE_Petugas.Properties.ReadOnly = Status
        txtRiwayat.Properties.ReadOnly = Status
        txtAllergi.Properties.ReadOnly = Status
        txtRiwayatPenyakitDahulu.Properties.ReadOnly = Status
        cboTingkatKesadaran.Properties.ReadOnly = Status
        cboKeadaanUmum.Properties.ReadOnly = Status
        txtBeratBadan.Properties.ReadOnly = Status
        txtTinggiBadan.Properties.ReadOnly = Status
        cboGCS.Properties.ReadOnly = Status
        cboE.Properties.ReadOnly = Status
        cboM.Properties.ReadOnly = Status
        cboV.Properties.ReadOnly = Status
        txtTensi.Properties.ReadOnly = Status
        cboNadi.Properties.ReadOnly = Status
        cboRespirasi.Properties.ReadOnly = Status
        txtSuhu.Properties.ReadOnly = Status
        cboSpO2.Properties.ReadOnly = Status
        chk_SKALANYERI_00.Properties.ReadOnly = Status
        chk_SKALANYERI_01.Properties.ReadOnly = Status
        chk_SKALANYERI_02.Properties.ReadOnly = Status
        chk_SKALANYERI_03.Properties.ReadOnly = Status
        chk_SKALANYERI_04.Properties.ReadOnly = Status
        chk_SKALANYERI_05.Properties.ReadOnly = Status
        chk_SKALANYERI_06.Properties.ReadOnly = Status
        chk_SKALANYERI_07.Properties.ReadOnly = Status
        chk_SKALANYERI_08.Properties.ReadOnly = Status
        chk_SKALANYERI_09.Properties.ReadOnly = Status
        chk_SKALANYERI_10.Properties.ReadOnly = Status
        chkResikiJatuh_Tidak.Properties.ReadOnly = Status
        chkResikoJatuh_Ya.Properties.ReadOnly = Status
        chkStatusFungsional_Mandiri.Properties.ReadOnly = Status
        chkStatusFungsional_PerluBantuan.Properties.ReadOnly = Status
        txtStatusFungsional_PerluBantuan.Properties.ReadOnly = Status
        chkStatusFungsional_AlatBantu.Properties.ReadOnly = Status
        txtStatusFungsional_AlatBantu.Properties.ReadOnly = Status
        cboSurveySkunder_Kepala.Properties.ReadOnly = Status
        txtSurveySkunder_Kepala.Properties.ReadOnly = Status
        cboSurveySkunder_Mata.Properties.ReadOnly = Status
        txtSurveySkunder_Mata.Properties.ReadOnly = Status
        cboSurveySkunder_Leher.Properties.ReadOnly = Status
        txtSurveySkunder_Leher.Properties.ReadOnly = Status
        cboSurveySkunder_Dada.Properties.ReadOnly = Status
        txtSurveySkunder_Dada.Properties.ReadOnly = Status
        cboSurveySkunder_Perut.Properties.ReadOnly = Status
        txtSurveySkunder_Perut.Properties.ReadOnly = Status
        cboSurveySkunder_AlatGerak.Properties.ReadOnly = Status
        txtSurveySkunder_AlatGerak.Properties.ReadOnly = Status

        grdKDDOCTOR.Properties.ReadOnly = Status
        grdKDPERAWAT.Properties.ReadOnly = Status
        txtDIAGNOSA.Properties.ReadOnly = Status
        chkPerbaikan.Properties.ReadOnly = Status
        chkStabil.Properties.ReadOnly = Status
        chkPerburukan.Properties.ReadOnly = Status
        chkRujuk.Properties.ReadOnly = Status
        chkRawat.Properties.ReadOnly = Status
        chkPulaangPaksa.Properties.ReadOnly = Status
        chkPulang.Properties.ReadOnly = Status
        txtTujuan.Properties.ReadOnly = Status
        txtKondisi.Properties.ReadOnly = Status
        dePukul.Properties.ReadOnly = Status
        cboTingkaKesadaran_2.Properties.ReadOnly = Status
        cboGCS_2.Properties.ReadOnly = Status
        cboE_2.Properties.ReadOnly = Status
        cboM_2.Properties.ReadOnly = Status
        cboV_2.Properties.ReadOnly = Status
        txtBP.Properties.ReadOnly = Status
        cboHR.Properties.ReadOnly = Status
        cboRR.Properties.ReadOnly = Status
        cboSpO2.Properties.ReadOnly = Status
        txtT.Properties.ReadOnly = Status
        txtInstruksiLanjutan.Properties.ReadOnly = Status
        txtLokasi.Properties.ReadOnly = Status
        deWaktu.Properties.ReadOnly = Status
        grdKDDOCTOR_2.Properties.ReadOnly = Status
        txtObatSaatPulang.Properties.ReadOnly = Status
        grvDetail.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        deDATE.DateTime = Now
        dePENGKAJIAN_PukulPeriksa.DateTime = Now
        dePENKAJIAN_PukulRawat.DateTime = Now
        dePukul.DateTime = Now
        deWaktu.DateTime = Now

        'txtMEMO.ResetText()

        'chkISACTIVE.Checked = True
        'chkISDEFAULT.Checked = False

        'txtMEMO.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oDigital_IGD_01.GetData(sNoId)

            With ds
                txtCODE.Text = sNoId

                cboCARI.SelectedIndex = 2

                fn_LoadKDKUNJUNGAN(.KDKUNJUNGAN, 3)
                grdKDKUNJUNGAN.Text = .KDKUNJUNGAN
                deDATE.DateTime = .DATE
                chkTRIAGE_Trauma.Checked = .TRIAGE_TRAUMA
                chkTRIAGE_NonTrauma.Checked = .TRIAGE_NONTRAUMA
                chkTRIAGE_Martenity.Checked = .TRIAGE_MATERNITY
                chkTRIAGE_Red.Checked = .TRIAGE_RED
                chkTRIAGE_Yellow.Checked = .TRIAGE_YELLOW
                chkTRIAGE_Green.Checked = .TRIAGE_GREEN
                chkTRIAGE_Black.Checked = .TRIAGE_BLACK
                chkTRIAGE_Airway.Checked = .TRIAGE_AIRWAY
                chkTRIAGE_Breathing.Checked = .TRIAGE_BREATHING
                chkTRIAGE_Circulation.Checked = .TRIAGE_CIRCULATION
                cboTRIAGE_Airway.Text = .TRIAGE_AIRWAY_TEX
                cboTRIAGE_Breathing.Text = .TRIAGE_BREATHING_TEXT
                cboTRIAGE_Circulation.Text = .TRIAGE_CIRCULATION_TEXT
                chkTRIAGE_PasienDatangSendiri.Checked = .TRIAGE_DATANGSENDIRI
                chkTRIAGE_PasienDiantar.Checked = .TRIAGE_DIANTAR
                cboTRIAGE_PasienDiantar.Text = .TRIAGE_DIANTAR_TEXT
                chkPENGKAJIAN_AutoAnamnesa.Checked = .PENGKAJIAN_AUTOANAMNESA
                chkPENGKAJIAN_AlloAnamnesa.Checked = .PENGKAJIAN_ALOANAMNESA
                dePENGKAJIAN_PukulPeriksa.DateTime = .PUKULPERIKSA
                dePENKAJIAN_PukulRawat.DateTime = .PUKULRAWAT
                grdTRIAGE_Petugas.Text = .PETUGASTRIAGE
                txtRiwayat.Text = .RIWAYAT
                txtAllergi.Text = .RIWAYAT_ALERGI
                txtRiwayatPenyakitDahulu.Text = .RIWAYAT_PENYAKITDAHULU
                cboTingkatKesadaran.Text = .TINGKATKESADARAN
                cboKeadaanUmum.Text = .KEADAANUMUM
                txtBeratBadan.Text = .BERATBADAN
                txtTinggiBadan.Text = .TINGGIBADAN
                cboGCS.Text = .GCS
                cboE.Text = .E
                cboM.Text = .M
                cboV.Text = .V
                txtTensi.Text = .BP
                cboNadi.Text = .HR
                cboRespirasi.Text = .RR
                txtSuhu.Text = .T
                cboSpO2.Text = .SP02
                chk_SKALANYERI_00.Checked = .SKALANEYRI_00
                chk_SKALANYERI_01.Checked = .SKALANEYRI_01
                chk_SKALANYERI_02.Checked = .SKALANEYRI_02
                chk_SKALANYERI_03.Checked = .SKALANEYRI_03
                chk_SKALANYERI_04.Checked = .SKALANEYRI_04
                chk_SKALANYERI_05.Checked = .SKALANEYRI_05
                chk_SKALANYERI_06.Checked = .SKALANEYRI_06
                chk_SKALANYERI_07.Checked = .SKALANEYRI_07
                chk_SKALANYERI_08.Checked = .SKALANEYRI_08
                chk_SKALANYERI_09.Checked = .SKALANEYRI_09
                chk_SKALANYERI_10.Checked = .SKALANEYRI_10
                chkResikiJatuh_Tidak.Checked = .RESIKO_TIDAK
                chkResikoJatuh_Ya.Checked = .RESIKO_YA
                chkStatusFungsional_Mandiri.Checked = .FUNGSIONAL_MANDIRI
                chkStatusFungsional_PerluBantuan.Checked = .FUNGSIONAL_PERLUBANTUAN
                txtStatusFungsional_PerluBantuan.Text = .FUNGSIONAL_2_PERLUBANTUAN_TEXT
                chkStatusFungsional_AlatBantu.Checked = .FUNGSIONAL_ALATBANTU
                txtStatusFungsional_AlatBantu.Text = .FUNGSIONAL_3_ALATBANTU_TEXT
                cboSurveySkunder_Kepala.Text = .SURVEY_KEPALA_1
                txtSurveySkunder_Kepala.Text = .SURVEY_KEPALA_2
                cboSurveySkunder_Mata.Text = .SURVEY_MATA_1
                txtSurveySkunder_Mata.Text = .SURVEY_MATA_2
                cboSurveySkunder_Leher.Text = .SURVEY_LEHER_1
                txtSurveySkunder_Leher.Text = .SURVEY_LEHER_2
                cboSurveySkunder_Dada.Text = .SURVEY_DADA_1
                txtSurveySkunder_Dada.Text = .SURVEY_DADA_2
                cboSurveySkunder_Perut.Text = .SURVEY_PERUT_1
                txtSurveySkunder_Perut.Text = .SURVEY_PERUT_2
                cboSurveySkunder_AlatGerak.Text = .SURVEY_ALATGERAK_1
                txtSurveySkunder_AlatGerak.Text = .SURVEY_ALATGERAK_2
                Try
                    Dim img = (From x In oDigital_IGD_01.GetData
                               Where x.KDDIGITAL_IGD_01 = sNoId
                               Select x.ATTACHMENT_2).Single

                    picGAMBAR2.Image = ByteArrayToImage(img.ToArray())
                Catch oErr As Exception
                    'MsgBox("Load Image : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try

                grdKDDOCTOR.Text = .KDDOCTOR
                grdKDPERAWAT.Text = .KDPERAWAT
                txtDIAGNOSA.Text = .KDDIAGNOSA
                chkPerbaikan.Checked = .PERBAIKAN
                chkStabil.Checked = .STABIL
                chkPerburukan.Checked = .PERBURUKAN
                chkRujuk.Checked = .RUJUK
                chkRawat.Checked = .RAWAT
                chkPulaangPaksa.Checked = .PULANGPAKSA
                chkPulang.Checked = .PULANG
                txtTujuan.Text = .TUJUAN
                txtKondisi.Text = .KONDISI
                dePukul.DateTime = .PUKUL
                cboTingkaKesadaran_2.Text = .TINGKATKESADARAN_2
                cboGCS_2.Text = .GCS_2
                cboE_2.Text = .E_2
                cboM_2.Text = .M_2
                cboV_2.Text = .V_2
                txtBP.Text = .BP_2
                cboHR.Text = .HR_2
                cboRR.Text = .RR_2
                cboSpO2.Text = .SP02_2
                txtT.Text = .T
                txtInstruksiLanjutan.Text = .INTRUKSILANJUTAN
                txtLokasi.Text = .LOKASI
                deWaktu.DateTime = .WAKTU
                grdKDDOCTOR_2.Text = .DOKTER
                txtObatSaatPulang.Text = .OBATSAATPULANG

                BindingSource.DataSource = oDigital_IGD_01.GetDataDetail.Where(Function(x) x.KDDIGITAL_IGD_01 = sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = BindingSource

            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
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

            If grdKDKUNJUNGAN.Text = String.Empty Then
                grdKDKUNJUNGAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDKUNJUNGAN.ErrorText = Statement.ErrorRequired

                grdKDKUNJUNGAN.Focus()
                fn_Validate = False
                Exit Function
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oDigital_IGD_01.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oDigital_IGD_01.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = Now

                .KDDIGITAL_IGD_01 = sNoId
                .KDKUNJUNGAN = grdKDKUNJUNGAN.EditValue

                .TRIAGE_TRAUMA = chkTRIAGE_Trauma.Checked
                .TRIAGE_NONTRAUMA = chkTRIAGE_NonTrauma.Checked
                .TRIAGE_MATERNITY = chkTRIAGE_Martenity.Checked
                .TRIAGE_RED = chkTRIAGE_Red.Checked
                .TRIAGE_YELLOW = chkTRIAGE_Yellow.Checked
                .TRIAGE_GREEN = chkTRIAGE_Green.Checked
                .TRIAGE_BLACK = chkTRIAGE_Black.Checked
                .TRIAGE_AIRWAY = chkTRIAGE_Airway.Checked
                .TRIAGE_BREATHING = chkTRIAGE_Breathing.Checked
                .TRIAGE_CIRCULATION = chkTRIAGE_Circulation.Checked
                .TRIAGE_AIRWAY_TEX = cboTRIAGE_Airway.Text
                .TRIAGE_BREATHING_TEXT = cboTRIAGE_Breathing.Text
                .TRIAGE_CIRCULATION_TEXT = cboTRIAGE_Circulation.Text
                .TRIAGE_DATANGSENDIRI = chkTRIAGE_PasienDatangSendiri.Checked
                .TRIAGE_DIANTAR = chkTRIAGE_PasienDiantar.Checked
                .TRIAGE_DIANTAR_TEXT = cboTRIAGE_PasienDiantar.Text
                .PENGKAJIAN_AUTOANAMNESA = chkPENGKAJIAN_AutoAnamnesa.Checked
                .PENGKAJIAN_ALOANAMNESA = chkPENGKAJIAN_AlloAnamnesa.Checked
                .PUKULPERIKSA = dePENGKAJIAN_PukulPeriksa.DateTime
                .PUKULRAWAT = dePENKAJIAN_PukulRawat.DateTime
                .PETUGASTRIAGE = grdTRIAGE_Petugas.Text
                .RIWAYAT = txtRiwayat.Text
                .RIWAYAT_ALERGI = txtAllergi.Text
                .RIWAYAT_PENYAKITDAHULU = txtRiwayatPenyakitDahulu.Text
                .TINGKATKESADARAN = cboTingkatKesadaran.Text
                .KEADAANUMUM = cboKeadaanUmum.Text
                .BERATBADAN = CDec(txtBeratBadan.Text)
                .TINGGIBADAN = CDec(txtTinggiBadan.Text)
                .GCS = cboGCS.Text
                .E = cboE.Text
                .M = cboM.Text
                .V = cboV.Text
                .BP = txtTensi.Text
                .HR = cboNadi.Text
                .RR = cboRespirasi.Text
                .T = CDec(txtSuhu.Text)
                .SP02 = cboSpO2.Text
                .SKALANEYRI_00 = chk_SKALANYERI_00.Checked
                .SKALANEYRI_01 = chk_SKALANYERI_01.Checked
                .SKALANEYRI_02 = chk_SKALANYERI_02.Checked
                .SKALANEYRI_03 = chk_SKALANYERI_03.Checked
                .SKALANEYRI_04 = chk_SKALANYERI_04.Checked
                .SKALANEYRI_05 = chk_SKALANYERI_05.Checked
                .SKALANEYRI_06 = chk_SKALANYERI_06.Checked
                .SKALANEYRI_07 = chk_SKALANYERI_07.Checked
                .SKALANEYRI_08 = chk_SKALANYERI_08.Checked
                .SKALANEYRI_09 = chk_SKALANYERI_09.Checked
                .SKALANEYRI_10 = chk_SKALANYERI_10.Checked
                .RESIKO_TIDAK = chkResikiJatuh_Tidak.Checked
                .RESIKO_YA = chkResikoJatuh_Ya.Checked
                .FUNGSIONAL_MANDIRI = chkStatusFungsional_Mandiri.Checked
                .FUNGSIONAL_PERLUBANTUAN = chkStatusFungsional_PerluBantuan.Checked
                .FUNGSIONAL_2_PERLUBANTUAN_TEXT = txtStatusFungsional_PerluBantuan.Text
                .FUNGSIONAL_ALATBANTU = chkStatusFungsional_AlatBantu.Checked
                .FUNGSIONAL_3_ALATBANTU_TEXT = txtStatusFungsional_AlatBantu.Text
                .SURVEY_KEPALA_1 = cboSurveySkunder_Kepala.Text
                .SURVEY_KEPALA_2 = txtSurveySkunder_Kepala.Text
                .SURVEY_MATA_1 = cboSurveySkunder_Mata.Text
                .SURVEY_MATA_2 = txtSurveySkunder_Mata.Text
                .SURVEY_LEHER_1 = cboSurveySkunder_Leher.Text
                .SURVEY_LEHER_2 = txtSurveySkunder_Leher.Text
                .SURVEY_DADA_1 = cboSurveySkunder_Dada.Text
                .SURVEY_DADA_2 = txtSurveySkunder_Dada.Text
                .SURVEY_PERUT_1 = cboSurveySkunder_Perut.Text
                .SURVEY_PERUT_2 = txtSurveySkunder_Perut.Text
                .SURVEY_ALATGERAK_1 = cboSurveySkunder_AlatGerak.Text
                .SURVEY_ALATGERAK_2 = txtSurveySkunder_AlatGerak.Text
                Try
                    Dim ms As New IO.MemoryStream()
                    picGAMBAR2.Image.Save(ms, picGAMBAR2.Image.RawFormat)

                    Dim data As Byte() = ms.GetBuffer()

                    .ATTACHMENT_2 = data
                Catch oErr As Exception

                End Try
                .KDUSER = sUserID
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    Dim KDDIGITAL As String = String.Empty
                    KDDIGITAL = oDigital_IGD_01.InsertData(ds)
                    If KDDIGITAL = "" Then
                        fn_Save = False
                    Else
                        fn_Save = True
                    End If
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oDigital_IGD_01.UpdateData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
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
        End Select
    End Sub
    Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
            sStatusSave = "NEW"
            Me.Close()
        End If
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub

#End Region
#Region "Lookup / Event"
    Private Sub OnClick_01(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTRIAGE_Trauma.Click, chkTRIAGE_NonTrauma.Click, chkTRIAGE_Martenity.Click
        chkTRIAGE_Trauma.Checked = False
        chkTRIAGE_NonTrauma.Checked = False
        chkTRIAGE_Martenity.Checked = False
    End Sub
    Private Sub OnClick_02(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTRIAGE_Red.Click, chkTRIAGE_Yellow.Click, chkTRIAGE_Green.Click, chkTRIAGE_Black.Click
        chkTRIAGE_Red.Checked = False
        chkTRIAGE_Yellow.Checked = False
        chkTRIAGE_Green.Checked = False
        chkTRIAGE_Black.Checked = False
    End Sub
    Private Sub OnClick_03(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTRIAGE_PasienDatangSendiri.Click, chkTRIAGE_PasienDiantar.Click
        chkTRIAGE_PasienDatangSendiri.Checked = False
        chkTRIAGE_PasienDiantar.Checked = False
    End Sub
    Private Sub OnClick_04(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPENGKAJIAN_AlloAnamnesa.Click, chkPENGKAJIAN_AutoAnamnesa.Click
        chkPENGKAJIAN_AutoAnamnesa.Checked = False
        chkPENGKAJIAN_AlloAnamnesa.Checked = False
    End Sub
    Private Sub OnClick_chkSkalaNyeri(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_SKALANYERI_00.Click, chk_SKALANYERI_01.Click, chk_SKALANYERI_02.Click, chk_SKALANYERI_03.Click, chk_SKALANYERI_04.Click, chk_SKALANYERI_05.Click, chk_SKALANYERI_06.Click, chk_SKALANYERI_07.Click, chk_SKALANYERI_08.Click, chk_SKALANYERI_09.Click, chk_SKALANYERI_10.Click
        chk_SKALANYERI_00.Checked = False
        chk_SKALANYERI_01.Checked = False
        chk_SKALANYERI_02.Checked = False
        chk_SKALANYERI_03.Checked = False
        chk_SKALANYERI_04.Checked = False
        chk_SKALANYERI_05.Checked = False
        chk_SKALANYERI_06.Checked = False
        chk_SKALANYERI_07.Checked = False
        chk_SKALANYERI_08.Checked = False
        chk_SKALANYERI_09.Checked = False
        chk_SKALANYERI_10.Checked = False
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
    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        picGAMBAR2.Image = CType(My.Resources.ResourceManager.GetObject("gambar_2"), Image)
    End Sub
    Private Sub fn_LoadKDKUNJUNGAN(ByVal sParameter As String, ByVal sCari As Integer)
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
            SQL &= "A.KDKUNJUNGAN "
            SQL &= ",A.KDPENDAFTARAN "
            SQL &= ",D.KDCUSTOMER "
            SQL &= ",E.NAME_DISPLAY "
            SQL &= ",A.DATE "
            SQL &= ",TUJUAN = B.NAME_DISPLAY  "
            SQL &= ",DPJP = C.NAME_DISPLAY "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_KUNJUNGAN A "
            SQL &= "INNER JOIN M_DEPARTMENT B "
            SQL &= "ON A.KDDEPARTMENT = B.KDDEPARTMENT "
            SQL &= "INNER JOIN M_DOCTOR C "
            SQL &= "ON A.KDDOCTOR = C.KDDOCTOR "
            SQL &= "INNER JOIN S_PENDAFTARAN_H D "
            SQL &= "ON A.KDPENDAFTARAN = D.KDPENDAFTARAN "
            SQL &= "INNER JOIN M_CUSTOMER E "
            SQL &= "ON D.KDCUSTOMER = E.KDCUSTOMER "
            If sCari = 0 Then
                SQL &= "WHERE D.KDCUSTOMER = '" & sParameter & "' "
            ElseIf sCari = 1 Then
                SQL &= "WHERE E.NAME_DISPLAY LIKE '%" & sParameter & "%' "
            ElseIf sCari = 2 Then
                SQL &= "WHERE A.KDPENDAFTARAN LIKE '%" & sParameter & "%' "
            Else
                SQL &= "WHERE A.KDKUNJUNGAN = '" & sParameter & "' "
            End If

            SQL &= "AND D.CATEGORY = 0 "

            SQL &= "ORDER BY A.DATE DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

            grdKDKUNJUNGAN.Properties.DataSource = ds.Tables("ALL")
            grdKDKUNJUNGAN.Properties.ValueMember = "KDKUNJUNGAN"
            grdKDKUNJUNGAN.Properties.DisplayMember = "KDKUNJUNGAN"

            If PopUP = True Then
                grdKDKUNJUNGAN.ShowPopup()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub txtCARI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCARI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            PopUP = True
            If txtCARI.Text = String.Empty Then Exit Sub
            fn_LoadKDKUNJUNGAN(IIf(cboCARI.SelectedIndex = 0, txtCARI.Text.ToString.Trim.PadLeft(9, "0"), txtCARI.Text.ToString.Trim), cboCARI.SelectedIndex)
        End If
    End Sub
    Private Sub grdKDKUNJUNGAN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdKDKUNJUNGAN.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Dim oKunjungan As New Admission.clsPendaftaran
            Dim dsKunjungan = oKunjungan.GetDataKunjungan(grdKDKUNJUNGAN.EditValue)
            If dsKunjungan IsNot Nothing Then
                txtNAMAPASIEN.Text = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY & " / " & dsKunjungan.S_PENDAFTARAN_H.KDCUSTOMER
                txtKTP.Text = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.KTP
                txtTEMPATLAHIR.Text = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.TEMPATLAHIR & " / " & dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy")
                txtALAMAT.Text = dsKunjungan.ALAMAT
            Else
                txtNAMAPASIEN.ResetText()
                txtKTP.ResetText()
                txtTEMPATLAHIR.ResetText()
                txtALAMAT.ResetText()
            End If
        End If
    End Sub

#End Region
End Class