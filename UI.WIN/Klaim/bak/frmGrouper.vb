Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq
Imports System.Data
Imports System.Data.SqlClient

Public Class frmGrouper
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oKlaim As New EClaim.clsKlaim
    Private oSetKoneksi As New Brigging.clsSetKoneksi
    Private oPendaftaran As New Admission.clsPendaftaran
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
        txtCARI.Focus()
    End Sub
    Public Sub fn_LoadLanguage()
        Try
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
        fn_LoadDPJP()
        fn_LoadCOB()
        fn_LoadCARAKELUAR()
        fn_LoadKODETARIF()
        fn_LoadKDICD_X()
        fn_LoadKDICD_IX()

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
        btnUpdatePasien.Enabled = Not Status
        cboCaraBayar.Properties.ReadOnly = Status
        txtNoPeserta.Properties.ReadOnly = Status
        txtNoSEP.Properties.ReadOnly = Status
        grdCOB.Properties.ReadOnly = Status
        chkKelasEksekutif.Properties.ReadOnly = Status
        chkNaikKelas.Properties.ReadOnly = Status
        chkAdaRawat.Properties.ReadOnly = Status
        deDATEMASUK.Properties.ReadOnly = Status
        deDATEPULANG.Properties.ReadOnly = Status
        rbKELASPELAYANAN.ReadOnly = Status
        txtLAMA.Properties.ReadOnly = Status
        txtLOS.Properties.ReadOnly = Status
        txtADLScore_SubAcute.Properties.ReadOnly = True
        txtADLScore_Chronic.Properties.ReadOnly = True
        rbKELASHAK.Properties.ReadOnly = Status
        txtUmur.Properties.ReadOnly = Status
        txtBeratBadan.Properties.ReadOnly = Status
        grdCaraKeluar.Properties.ReadOnly = Status
        grdJenisTarif.Properties.ReadOnly = Status
        txttarifRumahSakit.Properties.ReadOnly = Status
        txtTarifEksekutif.Properties.ReadOnly = Status
        txtProsedurNonBedah.Properties.ReadOnly = Status
        txtTenagaAhli.Properties.ReadOnly = Status
        txtRadiologi.Properties.ReadOnly = Status
        txtRehabilitasi.Properties.ReadOnly = Status
        txtObat.Properties.ReadOnly = Status
        txtAlkes.Properties.ReadOnly = Status
        txtProsedurBedah.Properties.ReadOnly = Status
        txtKeperawatan.Properties.ReadOnly = Status
        txtLaboratorium.Properties.ReadOnly = Status
        txtKamarAkomodasi.Properties.ReadOnly = Status
        txtObatKronis.Properties.ReadOnly = Status
        txtBMHP.Properties.ReadOnly = Status
        txtKonsultasi.Properties.ReadOnly = Status
        txtPenunjang.Properties.ReadOnly = Status
        txtPelayananDarah.Properties.ReadOnly = Status
        txtRawatIntensif.Properties.ReadOnly = Status
        txtObatKemoTerapi.Properties.ReadOnly = Status
        txtSewaAlat.Properties.ReadOnly = Status
        txtICD_X.Properties.ReadOnly = Status
        txtICCD_IX.Properties.ReadOnly = Status
        txtRAWATINTENSIF_HARI.Properties.ReadOnly = Status
        txtVENTILATOR.Properties.ReadOnly = Status
        If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            txtCARI.Properties.ReadOnly = False
            grdKDCASHIN.Properties.ReadOnly = False
            rbCategory.Properties.ReadOnly = False
        Else
            txtCARI.Properties.ReadOnly = True
            grdKDCASHIN.Properties.ReadOnly = True
            rbCategory.Properties.ReadOnly = True
        End If
        rbCATEGORY_SelectedIndexChanged()
    End Sub
    Private Sub fn_EmptyMe()
        txtCODE.Text = "<--- AUTO --->"
        chkKelasEksekutif.Checked = False
        cboCaraBayar.SelectedIndex = 0
        txtNoPeserta.ResetText()
        txtNoSEP.ResetText()
        grdJenisTarif.Text = oKlaim.KodeTarifKlaim_Default
        grdCOB.Text = oKlaim.COB_Default
        chkKelasEksekutif.Checked = False
        chkNaikKelas.Checked = False
        chkAdaRawat.Checked = False
        deDATEMASUK.DateTime = Now
        deDATEPULANG.DateTime = Now
        rbKELASPELAYANAN.SelectedIndex = 0
        txtLAMA.Text = 0
        txtLOS.Text = 1
        txtADLScore_SubAcute.Text = "-"
        txtADLScore_Chronic.Text = "-"
        txtHAKKELAS.Text = "-"
        rbKELASHAK.SelectedIndex = 0
        txtUmur.ResetText()
        txtBeratBadan.ResetText()
        grdCaraKeluar.Text = oKlaim.CaraKeluar_Default
        txttarifRumahSakit.ResetText()
        txtTarifEksekutif.ResetText()
        txtProsedurNonBedah.ResetText()
        txtTenagaAhli.ResetText()
        txtRadiologi.ResetText()
        txtRehabilitasi.ResetText()
        txtObat.ResetText()
        txtAlkes.ResetText()
        txtProsedurBedah.ResetText()
        txtKeperawatan.ResetText()
        txtLaboratorium.ResetText()
        txtKamarAkomodasi.ResetText()
        txtObatKronis.ResetText()
        txtBMHP.ResetText()
        txtKonsultasi.ResetText()
        txtPenunjang.ResetText()
        txtPelayananDarah.ResetText()
        txtRawatIntensif.ResetText()
        txtObatKemoTerapi.ResetText()
        txtSewaAlat.ResetText()
        txtICD_X.ResetText()
        txtICCD_IX.ResetText()
        txtRAWATINTENSIF_HARI.Text = 0
        txtVENTILATOR.Text = 0
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oKlaim.GetData(sNoId)

            With ds
                txtCODE.Text = .KDPENDAFTARAN
                cboCaraBayar.SelectedIndex = .CARABAYAR
                txtNoPeserta.Text = .NOPESERTA
                txtNoSEP.Text = .NOSEP
                grdCOB.Text = .KDCOB
                rbCategory.SelectedIndex = .CATEGORY
                chkKelasEksekutif.Checked = .ISKELASEKSEKUTIF
                chkNaikKelas.Checked = .ISNAIKTURUNKELAS
                chkAdaRawat.Checked = .ISADARAWATINTENSIF
                deDATEMASUK.DateTime = .DATE_MASUK
                deDATEPULANG.DateTime = .DATE_KELUAR
                rbKELASPELAYANAN.SelectedIndex = .KELASPELAYANAN
                txtLAMA.Text = .LAMA
                txtLOS.Text = .LOS
                txtADLScore_SubAcute.Text = .ADLSCORE_SUBACUTE
                txtADLScore_Chronic.Text = .ADLSCORE_CHRONIC
                grdDPJP.Text = .DPJP
                txtHAKKELAS.Text = "-"
                rbKELASHAK.SelectedIndex = .HAKKELAS
                txtUmur.Text = .UMUR
                txtBeratBadan.Text = .BERATBADAN
                grdCaraKeluar.Text = .KDCARAKELUAR
                grdJenisTarif.Text = .JENISTARIF
                txttarifRumahSakit.Text = .TARIFRUMAHSAKIT
                txtTarifEksekutif.Text = .TARIFEKSEKUTIF
                txtProsedurNonBedah.Text = .PROSEDURNONBEDAH
                txtTenagaAhli.Text = .TENAGAAHLI
                txtRadiologi.Text = .RADIOLOGI
                txtRehabilitasi.Text = .REHABILITASI
                txtObat.Text = .OBAT
                txtAlkes.Text = .ALKES
                txtProsedurBedah.Text = .PROSEDURBEDAH
                txtKeperawatan.Text = .KEPERAWATAN
                txtLaboratorium.Text = .LABORATORIUM
                txtKamarAkomodasi.Text = .KAMARAKOMODASI
                txtObatKronis.Text = .OBATKRONIS
                txtBMHP.Text = .BMHP
                txtKonsultasi.Text = .KONSULTASI
                txtPenunjang.Text = .PENUNJANG
                txtPelayananDarah.Text = .PELAYANANDARAH
                txtRawatIntensif.Text = .RAWATINTENSIF
                txtObatKemoTerapi.Text = .OBATKEMOTERAPI
                txtSewaAlat.Text = .SEWAALAT
                txtICD_X.Text = .ICD_10
                txtICCD_IX.Text = .ICD_9
                txtRAWATINTENSIF_HARI.Text = .RAWATINTENSIF_HARI
                txtVENTILATOR.Text = .VENTILATOR
                'If .CATEGORY = 0 Then
                '    fn_LoadDataRJ(.KDPENDAFTARAN)
                'Else
                '    Dim oPendaftaran As New Admission.clsPendaftaran
                '    fn_LoadDataRI(.KDPENDAFTARAN, oPendaftaran.GetData(.KDPENDAFTARAN).KDPENDAFTARAN_AWAL)
                'End If
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If txtCODE.Text = String.Empty Then
                txtCODE.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtCODE.ErrorText = Statement.ErrorRequired

                txtCODE.Focus()
                fn_Validate = False
                Exit Function
            End If

            If grdDPJP.Text = String.Empty Then
                grdDPJP.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdDPJP.ErrorText = Statement.ErrorRequired

                grdDPJP.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtNoPeserta.Text = String.Empty Then
                txtNoPeserta.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtNoPeserta.ErrorText = Statement.ErrorRequired

                txtNoPeserta.Focus()
                fn_Validate = False
                Exit Function
            End If

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Dim dsRegister = oKlaim.GetData(txtCODE.Text)
                If dsRegister IsNot Nothing Then
                    MsgBox("No Pendaftaran " & dsRegister.KDPENDAFTARAN & " Sudah di Grouper", MsgBoxStyle.Exclamation, Me.Text)
                    fn_Validate = False
                    Exit Function
                End If
            End If

        Catch oErr As Exception
            fn_Validate = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** ECLAIM *****
            If fn_Membuatklaimbaru() = True Then
                If fn_MengisiUpdateDataKlaim() = False Then
                    fn_Save = False
                    Exit Function
                End If
            Else
                fn_Save = False
                Exit Function
            End If

            ' ***** HEADER *****
            Dim ds = oKlaim.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oKlaim.GetData(txtCODE.Text).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDPENDAFTARAN = txtCODE.Text
                .CARABAYAR = cboCaraBayar.SelectedIndex
                .NOPESERTA = txtNoPeserta.Text
                .NOSEP = txtNoSEP.Text
                .KDCOB = grdCOB.EditValue
                .CATEGORY = rbCategory.SelectedIndex
                .ISKELASEKSEKUTIF = chkKelasEksekutif.Checked
                .ISNAIKTURUNKELAS = chkNaikKelas.Checked
                .ISADARAWATINTENSIF = chkAdaRawat.Checked
                .DATE_MASUK = deDATEMASUK.DateTime
                .DATE_KELUAR = deDATEPULANG.DateTime
                .KELASPELAYANAN = rbKELASPELAYANAN.SelectedIndex
                .RAWATINTENSIF_HARI = CInt(txtRAWATINTENSIF_HARI.Text)
                .LOS = CInt(txtLOS.Text)
                .ADLSCORE_SUBACUTE = txtADLScore_SubAcute.Text
                .ADLSCORE_CHRONIC = txtADLScore_Chronic.Text
                .DPJP = grdDPJP.EditValue
                .HAKKELAS = rbKELASHAK.SelectedIndex
                .UMUR = txtUmur.Text
                .LAMA = txtLAMA.Text
                .VENTILATOR = CInt(txtVENTILATOR.Text)
                .BERATBADAN = CInt(txtBeratBadan.Text)
                .KDCARAKELUAR = grdCaraKeluar.EditValue
                .JENISTARIF = grdJenisTarif.EditValue
                .TARIFRUMAHSAKIT = txttarifRumahSakit.Text
                .TARIFEKSEKUTIF = txtTarifEksekutif.Text
                .PROSEDURNONBEDAH = txtProsedurNonBedah.Text
                .TENAGAAHLI = txtTenagaAhli.Text
                .RADIOLOGI = txtRadiologi.Text
                .REHABILITASI = txtRehabilitasi.Text
                .OBAT = txtObat.Text
                .ALKES = txtAlkes.Text
                .PROSEDURBEDAH = txtProsedurBedah.Text
                .KEPERAWATAN = txtKeperawatan.Text
                .LABORATORIUM = txtLaboratorium.Text
                .KAMARAKOMODASI = txtKamarAkomodasi.Text
                .OBATKRONIS = txtObatKronis.Text
                .BMHP = txtBMHP.Text
                .KONSULTASI = txtKonsultasi.Text
                .PENUNJANG = txtPenunjang.Text
                .PELAYANANDARAH = txtPelayananDarah.Text
                .RAWATINTENSIF = txtRawatIntensif.Text
                .OBATKEMOTERAPI = txtObatKemoTerapi.Text
                .SEWAALAT = txtSewaAlat.Text
                .ICD_10 = txtICD_X.Text
                .ICD_9 = txtICCD_IX.Text
                .MEMO = ""
                .KDUSER = sUserID
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oKlaim.InsertData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oKlaim.UpdateData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Function fn_Membuatklaimbaru() As Boolean
        Try
            Dim dsPendaftaran = oPendaftaran.GetData(txtCODE.Text)

            Dim dsMembuatKlaimBaru = oSetKoneksi.fn_Membuatklaimbaru("VCLAIM2", "{" & """metadata"": {" & """method"": " & """new_claim""    }," & """data"": {" & """nomor_kartu"": """ & txtNoPeserta.Text & """, " & """nomor_sep"": """ & txtNoSEP.Text & """, " & """nomor_rm"": """ & dsPendaftaran.KDCUSTOMER & """, " & """nama_pasien"": """ & dsPendaftaran.M_CUSTOMER.NAME_DISPLAY & """, " & """tgl_lahir"": """ & dsPendaftaran.M_CUSTOMER.TANGGALLAHIR.ToString("yyyy-MM-dd HH:mm:ss") & """, " & """gender"": """ & IIf(dsPendaftaran.M_CUSTOMER.KDJENISKELAMIN = 0, "1", "2") & """   } } ")

            If dsMembuatKlaimBaru <> "" Then
                Dim jsonDecode = JObject.Parse(dsMembuatKlaimBaru)
                Dim code = jsonDecode("metadata")("code").ToString
                Dim message = jsonDecode("metadata")("message").ToString

                If code = "200" Then
                    fn_Membuatklaimbaru = True
                ElseIf code = "400" Then
                    fn_Membuatklaimbaru = True
                    If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                        MsgBox(Statement.ErrorStatement & vbCrLf & code & " - " & message, MsgBoxStyle.Information, Me.Text)
                    End If
                Else
                    fn_Membuatklaimbaru = False
                    MsgBox(Statement.ErrorStatement & vbCrLf & dsMembuatKlaimBaru, MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
                fn_Membuatklaimbaru = False
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Membuatklaimbaru = False
        End Try
    End Function
    Private Function fn_MengisiUpdateDataKlaim() As Boolean
        Try
            Dim dsPendaftaran = oPendaftaran.GetData(txtCODE.Text)
            Dim oSetUser As New Setting.clsUser

            Dim dsMembuatKlaimBaru = oSetKoneksi.fn_MengisiUpdateDataKlaim("VCLAIM2", "{" & """metadata"": { " & """method"": " & """set_claim_data"",    " & """nomor_sep"": """ & txtNoSEP.Text & """  },  " & """data"": {    " & """nomor_sep"": """ & txtNoSEP.Text & """,    " & """nomor_kartu"": """ & txtNoPeserta.Text & """,    " & """tgl_masuk"": """ & deDATEMASUK.DateTime.ToString("yyyy-MM-dd HH:mm:ss") & """,    " & """tgl_pulang"": """ & deDATEPULANG.DateTime.ToString("yyyy-MM-dd HH:mm:ss") & """,    " & """jenis_rawat"": """ & "2" & """,    " & """kelas_rawat"": """ & IIf(chkKelasEksekutif.Checked = False, "3", "1") & """,    " & """adl_sub_acute"": """ & txtADLScore_SubAcute.Text & """,    " & """adl_chronic"": """ & txtADLScore_Chronic.Text & """,    " & """icu_indikator"": """ & "0" & """,    " & """icu_los"": """ & "0" & """,    " & """ventilator_hour"": """ & "0" & """,    " & """upgrade_class_ind"": """ & "0" & """,    " & """upgrade_class_class"": """ & "" & """,    " & """upgrade_class_los"": """ & 0 & """,    " & """add_payment_pct"": """ & 0 & """,    " & """birth_weight"": """ & txtBeratBadan.Text & """,    " & """discharge_status"": """ & grdCaraKeluar.EditValue & """,    " & """diagnosa"": """ & txtICD_X.Text & """,    " & """procedure"": """ & txtICCD_IX.Text & """,    " & """tarif_rs"": {      " & """prosedur_non_bedah"": """ & CInt(txtProsedurNonBedah.Text) & """,      " & """prosedur_bedah"": """ & CInt(txtProsedurBedah.Text) & """,      " & """konsultasi"": """ & CInt(txtKonsultasi.Text) & """,      " & """tenaga_ahli"": """ & CInt(txtTenagaAhli.Text) & """,      " & """keperawatan"": """ & CInt(txtKeperawatan.Text) & """,      " & """penunjang"": """ & CInt(txtPenunjang.Text) & """,      " & """radiologi"": """ & CInt(txtRadiologi.Text) & """,      " & """laboratorium"": """ & CInt(txtLaboratorium.Text) & """,      " & """pelayanan_darah"": """ & CInt(txtPelayananDarah.Text) & """,      " & """rehabilitasi"": """ & CInt(txtRehabilitasi.Text) & """,      " & """kamar"": """ & CInt(txtKamarAkomodasi.Text) & """,      " & """rawat_intensif"": """ & CInt(txtRawatIntensif.Text) & """,   " & """obat"": """ & CInt(txtObat.Text) & """,   " & """obat_kronis"": """ & CInt(txtObatKronis.Text) & """, " & """obat_kemoterapi"": """ & CInt(txtObatKemoTerapi.Text) & """,      " & """alkes"": """ & CInt(txtAlkes.Text) & """,      " & """bmhp"": """ & CInt(txtBMHP.Text) & """,      " & """sewa_alat"": """ & CInt(txtSewaAlat.Text) & """    },    " & """tarif_poli_eks"": """ & CInt(txtTarifEksekutif.Text) & """,    " & """nama_dokter"": """ & grdDPJP.Text & """,    " & """kode_tarif"": """ & grdJenisTarif.EditValue & """,    " & """payor_id"": """ & IIf(cboCaraBayar.Text = "JKN", "3", IIf(cboCaraBayar.Text = "JAMKESDA", "5", IIf(cboCaraBayar.Text = "JAMKESOS", "6", "1"))) & """,    " & """payor_cd"": """ & cboCaraBayar.Text & """,    " & """cob_cd"": """ & IIf(grdCOB.Text = "-", "#", grdCOB.EditValue) & """,    " & """coder_nik"": """ & oSetUser.GetData(sUserID).NIK & """  } } ")

            Dim jsonDecode = JObject.Parse(dsMembuatKlaimBaru)
            Dim code = jsonDecode("metadata")("code").ToString
            Dim message = jsonDecode("metadata")("message").ToString

            If code = "200" Then
                fn_MengisiUpdateDataKlaim = True
            ElseIf code = "400" Then
                fn_MengisiUpdateDataKlaim = False
                MsgBox(Statement.ErrorStatement & vbCrLf & code & " - " & message, MsgBoxStyle.Information, Me.Text)
            Else
                fn_MengisiUpdateDataKlaim = False
                MsgBox(Statement.ErrorStatement & vbCrLf & dsMembuatKlaimBaru, MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_MengisiUpdateDataKlaim = False
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
            Case Keys.F6
                If btnUpdatePasien.Enabled = True Then
                    btnUpdatePasien_Click()
                End If
        End Select
    End Sub
    Private Sub btnUpdatePasien_Click() Handles btnUpdatePasien.ItemClick
        Try
            Dim dsPendaftaran = oPendaftaran.GetData(txtCODE.Text)
            Dim Request As String = ""

            Dim dsMembuatKlaimBaru = oSetKoneksi.fn_UpdateDataPasien("VCLAIM2", "{" & """metadata"": {" & """method"": " & """update_patient""    }," & """data"": {" & """nomor_kartu"": """ & txtNoPeserta.Text & """, " & """nomor_rm"": """ & dsPendaftaran.KDCUSTOMER & """, " & """nama_pasien"": """ & dsPendaftaran.M_CUSTOMER.NAME_DISPLAY & """, " & """tgl_lahir"": """ & dsPendaftaran.M_CUSTOMER.TANGGALLAHIR.ToString("yyyy-MM-dd HH:mm:ss") & """, " & """gender"": """ & IIf(dsPendaftaran.M_CUSTOMER.KDJENISKELAMIN = 0, "1", "2") & """   } } ")

            Dim jsonDecode = JObject.Parse(dsMembuatKlaimBaru)
            Dim code = jsonDecode("metadata")("code").ToString
            Dim message = jsonDecode("metadata")("message").ToString

            If code = "200" Then
                MsgBox(Statement.ErrorStatement & vbCrLf & code & " - " & message, MsgBoxStyle.Information, Me.Text)
            ElseIf code = "400" Then
                MsgBox(Statement.ErrorStatement & vbCrLf & code & " - " & message, MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox(Statement.ErrorStatement & vbCrLf & dsMembuatKlaimBaru, MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
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
    Private Sub rbCATEGORY_SelectedIndexChanged() Handles rbCategory.SelectedIndexChanged
        If rbCategory.SelectedIndex = 0 Then
            lKELASEKSEKUTIF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lNAIKKELAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lADARAWAT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lKELASHAKRJ.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lKELASHAKRI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            If chkKelasEksekutif.Checked = False Then
                lTARIFEKSEKUTIF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                txtTarifEksekutif.Text = "0"
            Else
                lTARIFEKSEKUTIF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
            lKELASPELAYANAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            LKELASPELAYANAN_RB.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lLAMA_LBL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lLAMA.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lRAWATINTENSIF_HARI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lRAWATINTENSIF_HARI_TEXT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lVENTILATOR.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lVENTILATOR_TEXT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            chkNaikKelas.Checked = False
            chkAdaRawat.Checked = False
            rbKELASHAK.SelectedIndex = 0
            rbKELASPELAYANAN.SelectedIndex = 0
            txtLAMA.Text = 0
            txtRAWATINTENSIF_HARI.Text = 0
            txtVENTILATOR.Text = 0
        Else
            lKELASEKSEKUTIF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lNAIKKELAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lADARAWAT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lKELASHAKRJ.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lKELASHAKRI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lTARIFEKSEKUTIF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            If chkNaikKelas.Checked = False Then
                lLAMA_LBL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lLAMA.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lKELASPELAYANAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                LKELASPELAYANAN_RB.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                txtLAMA.Text = "0"
                rbKELASPELAYANAN.SelectedIndex = 0
            Else
                lLAMA_LBL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lLAMA.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lKELASPELAYANAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                LKELASPELAYANAN_RB.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
            If chkAdaRawat.Checked = False Then
                lRAWATINTENSIF_HARI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lRAWATINTENSIF_HARI_TEXT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lVENTILATOR.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lVENTILATOR_TEXT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                txtRAWATINTENSIF_HARI.Text = 0
                txtVENTILATOR.Text = 0
            Else
                lRAWATINTENSIF_HARI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lRAWATINTENSIF_HARI_TEXT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lVENTILATOR.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lVENTILATOR_TEXT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
            txtTarifEksekutif.Text = "0"
            chkKelasEksekutif.Checked = False
        End If
    End Sub
    Private Sub chkKelasEksekutif_CheckedChanged(sender As Object, e As EventArgs) Handles chkKelasEksekutif.CheckedChanged
        If chkKelasEksekutif.Checked = False Then
            lTARIFEKSEKUTIF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            txtTarifEksekutif.Text = "0"
        Else
            lTARIFEKSEKUTIF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            txtTarifEksekutif.Text = "0"
        End If
    End Sub
    Private Sub chkNaikKelas_CheckedChanged(sender As Object, e As EventArgs) Handles chkNaikKelas.CheckedChanged
        If chkNaikKelas.Checked = False Then
            lLAMA_LBL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lLAMA.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lKELASPELAYANAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            LKELASPELAYANAN_RB.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            txtLAMA.Text = "0"
            rbKELASPELAYANAN.SelectedIndex = 0
        Else
            lLAMA_LBL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lLAMA.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lKELASPELAYANAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            LKELASPELAYANAN_RB.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            txtLAMA.Text = "0"
            rbKELASPELAYANAN.SelectedIndex = 0
        End If
    End Sub
    Private Sub chkAdaRawat_CheckedChanged(sender As Object, e As EventArgs) Handles chkAdaRawat.CheckedChanged
        If chkAdaRawat.Checked = False Then
            lRAWATINTENSIF_HARI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lRAWATINTENSIF_HARI_TEXT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lVENTILATOR.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lVENTILATOR_TEXT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            txtRAWATINTENSIF_HARI.Text = 0
            txtVENTILATOR.Text = 0
        Else
            lRAWATINTENSIF_HARI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lRAWATINTENSIF_HARI_TEXT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lVENTILATOR.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lVENTILATOR_TEXT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            txtRAWATINTENSIF_HARI.Text = 0
            txtVENTILATOR.Text = 0
        End If
    End Sub
    Private Sub fn_LoadKDICD_X()
        Dim oICD_X As New Reference.clsDiagnosa
        Try
            grdICD_X.Properties.DataSource = oICD_X.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdICD_X.Properties.ValueMember = "KDDIAGNOSA"
            grdICD_X.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDICD_IX()
        Dim oICD_IX As New Reference.clsProsedur
        Try
            grdICD_IX.Properties.DataSource = oICD_IX.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdICD_IX.Properties.ValueMember = "KDPROSEDUR"
            grdICD_IX.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKODETARIF()
        Dim oKodeTarif As New Reference.clsKodeTarif_Klaim
        Try
            grdJenisTarif.Properties.DataSource = oKodeTarif.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdJenisTarif.Properties.ValueMember = "KODETARIF_KLAIM"
            grdJenisTarif.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDPJP()
        Dim oDPJP As New Reference.clsDoctor
        Try
            grdDPJP.Properties.DataSource = oDPJP.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdDPJP.Properties.ValueMember = "KDDOCTOR"
            grdDPJP.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadCOB()
        Dim oCOB As New Reference.clsCOB
        Try
            grdCOB.Properties.DataSource = oCOB.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdCOB.Properties.ValueMember = "KDCOB"
            grdCOB.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadCARAKELUAR()
        Dim oCARAPULANG As New Reference.clsCaraKeluar
        Try
            grdCaraKeluar.Properties.DataSource = oCARAPULANG.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdCaraKeluar.Properties.ValueMember = "KDCARAKELUAR"
            grdCaraKeluar.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub txtCARI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCARI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If txtCARI.Text <> "" Then
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
                    SQL &= "A.KDPENDAFTARAN "
                    SQL &= ",TANGGAL = CONVERT(datetime, A.DATE) "
                    SQL &= ",A.KDCUSTOMER "
                    SQL &= ",PASIEN = C.NAME_DISPLAY "
                    SQL &= ",TUJUAN = B.NAME_DISPLAY "
                    SQL &= "FROM S_PENDAFTARAN_H A "
                    SQL &= "INNER JOIN M_DEPARTMENT B "
                    SQL &= "ON A.KDDEPARTMENT = B.KDDEPARTMENT "
                    SQL &= "INNER JOIN M_CUSTOMER C "
                    SQL &= "ON A.KDCUSTOMER = C.KDCUSTOMER "
                    If rbCategory.SelectedIndex = 0 Then
                        SQL &= "WHERE A.CATEGORY = 0 "
                    Else
                        SQL &= "WHERE A.CATEGORY = 1 "
                    End If
                    If cboCARI.SelectedIndex = 0 Then
                        SQL &= "AND A.KDCUSTOMER = '" & txtCARI.Text & "' "
                    Else
                        SQL &= "AND A.KDPENDAFTARAN = '" & txtCARI.Text & "' "
                    End If

                    oComm.Connection = oConn
                    oComm.CommandText = SQL
                    oComm.CommandTimeout = 120
                    oComm.CommandType = CommandType.Text

                    da = New SqlDataAdapter(oComm)
                    da.Fill(ds, "REGISTRASI")

                    grdKDCASHIN.Properties.DataSource = ds.Tables("REGISTRASI")
                    grdKDCASHIN.Properties.ValueMember = "KDPENDAFTARAN"
                    grdKDCASHIN.Properties.DisplayMember = "PASIEN"
                    grdKDCASHIN.ForceInitialize()

                    If oConn.State = ConnectionState.Open Then
                        oConn.Close()
                    End If

                    grvKDCASHIN.BestFitColumns()
                    grdKDCASHIN.ShowPopup()

                Catch oErr As Exception
                    MsgBox("Load Data" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        End If
    End Sub
    Private Sub grdKDCASHIN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdKDCASHIN.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtCARI.ResetText()
            Dim oPendaftaran As New Admission.clsPendaftaran
            Dim dsPendaftaran = oPendaftaran.GetData(grdKDCASHIN.EditValue)
            If dsPendaftaran IsNot Nothing Then
                txtCODE.Text = dsPendaftaran.KDPENDAFTARAN
                txtNoPeserta.Text = dsPendaftaran.KARTUBPJS
                deDATEMASUK.DateTime = dsPendaftaran.DATE
                Try
                    deDATEPULANG.DateTime = dsPendaftaran.T_UPDATE_TANGGAL_PULANGs.FirstOrDefault.DATE
                Catch ex As Exception
                    deDATEPULANG.DateTime = dsPendaftaran.DATE
                End Try
                grdDPJP.Text = dsPendaftaran.KDDOCTOR

                If rbCategory.SelectedIndex = 1 Then
                    Dim numWeekdays As Integer
                    Dim totalDays As Integer
                    Dim WeekendDays As Integer
                    numWeekdays = 0
                    WeekendDays = 0

                    totalDays = DateDiff(DateInterval.Day, deDATEMASUK.DateTime, deDATEPULANG.DateTime) + 1

                    For i As Integer = 1 To totalDays

                        If DatePart(DateInterval.Weekday, deDATEMASUK.DateTime) = 1 Then
                            WeekendDays = WeekendDays + 1
                        End If
                        If DatePart(DateInterval.Weekday, deDATEMASUK.DateTime) = 7 Then
                            WeekendDays = WeekendDays + 1
                        End If
                    Next

                    numWeekdays = totalDays - WeekendDays

                    txtLOS.Text = numWeekdays
                End If

                txtUmur.Text = oPendaftaran.GetUmurPasien(dsPendaftaran.DATE, dsPendaftaran.M_CUSTOMER.TANGGALLAHIR)

                If rbCategory.SelectedIndex = 0 Then
                    fn_LoadDataRJ(dsPendaftaran.KDPENDAFTARAN)
                Else
                    fn_LoadDataRI(dsPendaftaran.KDPENDAFTARAN, dsPendaftaran.KDPENDAFTARAN_AWAL)
                End If

            Else
                fn_EmptyMe()
            End If
        End If
    End Sub
    Private Sub fn_LoadDataRJ(ByVal Paramater As String)
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

            SQL = "SELECT 	"
            SQL &= "Transaksi = (SELECT CASE A.CATEGORY WHEN 0 THEN 'Rawat Jalan' ELSE 'Rawat Inap' END)	"
            SQL &= ",Register = A.KDPENDAFTARAN	"
            SQL &= ",TanggalTindakan = D.DATECREATED	"
            SQL &= ",NamaTindakan = E.NMITEM2	"
            SQL &= ",Jumlah = D.QTY	"
            SQL &= ",Harga = D.PRICE	"
            SQL &= ",Total = D.GRANDTOTAL	"
            SQL &= "FROM	"
            SQL &= "S_PENDAFTARAN_H A	"
            SQL &= "INNER JOIN S_PENDAFTARAN_KUNJUNGAN B	"
            SQL &= "ON A.KDPENDAFTARAN = B.KDPENDAFTARAN	"
            SQL &= "INNER JOIN S_SO_TRANSAKSI_H C	"
            SQL &= "ON B.KDKUNJUNGAN = C.KDKUNJUNGAN 	"
            SQL &= "INNER JOIN S_SO_TRANSAKSI_D D	"
            SQL &= "ON C.KDSOTRANSAKSI = D.KDSOTRANSAKSI	"
            SQL &= "INNER JOIN M_ITEM E	"
            SQL &= "ON D.KDITEM =  E.KDITEM	"
            SQL &= "INNER JOIN M_ITEM_L3 F	"
            SQL &= "ON E.KDITEM_L3 = F.KDITEM_L3	"
            SQL &= "WHERE A.KDPENDAFTARAN = '" & Paramater & "'	"

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_SI_D")

            grd.MainView = grv
            grd.DataSource = ds.Tables("S_SI_D")
            grd.ForceInitialize()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            fn_LoadFormatData()
            grv.BestFitColumns()

            Dim sProsedurNonBedah As Decimal = 0
            Dim sProsedurBedah As Decimal = 0
            Dim sKonsultasi As Decimal = 0
            Dim sTenagaAhli As Decimal = 0
            Dim sKeperawatan As Decimal = 0
            Dim sPenunjang As Decimal = 0
            Dim sRadiologi As Decimal = 0
            Dim sLaboratorium As Decimal = 0
            Dim sPelayananDarah As Decimal = 0
            Dim sRehabilitasi As Decimal = 0
            Dim sKamarAkomodasi As Decimal = 0
            Dim sRawatIntensif As Decimal = 0
            Dim sObat As Decimal = 0
            Dim sBMHP As Decimal = 0
            Dim sAlatMedis As Decimal = 0
            Dim sAlkes As Decimal = 0
            Dim sObatPRB As Decimal = 0
            Dim sObatKemoterafi As Decimal = 0

            For iLoop As Integer = 0 To ds.Tables("S_SI_D").Rows.Count - 1
                With ds.Tables("S_SI_D")
                    If .Rows(iLoop)("Kategori") = "-" Then
                        MsgBox("Tindakan " & .Rows(iLoop)("NamaTindakan") & " Belum Masuk Kategori", MsgBoxStyle.Exclamation, Me.Text)
                    End If
                    If .Rows(iLoop)("Kategori") = "PROSEDUR NON-BEDAH" Then
                        sProsedurNonBedah += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "PROSEDUR BEDAH" Then
                        sProsedurBedah += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "KONSULTASI" Then
                        sKonsultasi += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "TENAGA AHLI" Then
                        sTenagaAhli += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "KEPERAWATAN" Then
                        sKeperawatan += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "PENUNJANG" Then
                        sPenunjang += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "RADIOLOGI" Then
                        sRadiologi += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "LABORATORIUM" Then
                        sLaboratorium += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "PELAYANAN DARAH" Then
                        sPelayananDarah += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "REHABILITASI" Then
                        sRehabilitasi += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "ALKES" Then
                        sAlkes += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "OBAT" Then
                        sObat += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "KAMAR / AKOMODASI" Then
                        sKamarAkomodasi += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "OBAT KRONIS" Then
                        sObatPRB += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "BMHP" Then
                        sBMHP += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "RAWAT INTENSIF" Then
                        sRawatIntensif += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "OBAT KEMOTERAPI" Then
                        sObatKemoterafi += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "SEWA ALAT" Then
                        sAlatMedis += .Rows(iLoop)("TotalTarifMargin")
                    End If
                End With
            Next

            txtProsedurNonBedah.Text = sProsedurNonBedah
            txtProsedurBedah.Text = sProsedurBedah
            txtKonsultasi.Text = sKonsultasi
            txtTenagaAhli.Text = sTenagaAhli
            txtKeperawatan.Text = sKeperawatan
            txtPenunjang.Text = sPenunjang
            txtRadiologi.Text = sRadiologi
            txtLaboratorium.Text = sLaboratorium
            txtPelayananDarah.Text = sPelayananDarah
            txtRehabilitasi.Text = sRehabilitasi
            txtKamarAkomodasi.Text = sKamarAkomodasi
            txtRawatIntensif.Text = sRawatIntensif
            txtObat.Text = sObat
            txtBMHP.Text = sBMHP
            txtSewaAlat.Text = sAlatMedis
            txtAlkes.Text = sAlkes
            txtObatKronis.Text = sObatPRB
            txtObatKemoTerapi.Text = sObatKemoterafi
        Catch oErr As Exception
            MsgBox("Load Data" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDataRI(ByVal Paramater1 As String, ByVal Paramater2 As String)
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

            SQL = "SELECT 	"
            SQL &= "Transaksi = (SELECT CASE A.CATEGORY WHEN 0 THEN 'Rawat Jalan' ELSE 'Rawat Inap' END)	"
            SQL &= ",Register = A.KDPENDAFTARAN	"
            SQL &= ",TanggalTindakan = D.DATECREATED	"
            SQL &= ",NamaTindakan = E.NMITEM2	"
            SQL &= ",Jumlah = D.QTY	"
            SQL &= ",Harga = D.PRICE	"
            SQL &= ",Total = D.GRANDTOTAL	"
            SQL &= "FROM	"
            SQL &= "S_PENDAFTARAN_H A	"
            SQL &= "INNER JOIN S_PENDAFTARAN_KUNJUNGAN B	"
            SQL &= "ON A.KDPENDAFTARAN = B.KDPENDAFTARAN	"
            SQL &= "INNER JOIN S_SO_TRANSAKSI_H C	"
            SQL &= "ON B.KDKUNJUNGAN = C.KDKUNJUNGAN 	"
            SQL &= "INNER JOIN S_SO_TRANSAKSI_D D	"
            SQL &= "ON C.KDSOTRANSAKSI = D.KDSOTRANSAKSI	"
            SQL &= "INNER JOIN M_ITEM E	"
            SQL &= "ON D.KDITEM =  E.KDITEM	"
            SQL &= "INNER JOIN M_ITEM_L3 F	"
            SQL &= "ON E.KDITEM_L3 = F.KDITEM_L3	"
            SQL &= "WHERE A.KDPENDAFTARAN = '" & Paramater1 & "'	"
            SQL &= "OR A.KDPENDAFTARAN_AWAL = '" & Paramater2 & "'	"

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_SI_D")

            grd.MainView = grv
            grd.DataSource = ds.Tables("S_SI_D")
            grd.ForceInitialize()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            fn_LoadFormatData()
            grv.BestFitColumns()

            Dim sProsedurNonBedah As Decimal = 0
            Dim sProsedurBedah As Decimal = 0
            Dim sKonsultasi As Decimal = 0
            Dim sTenagaAhli As Decimal = 0
            Dim sKeperawatan As Decimal = 0
            Dim sPenunjang As Decimal = 0
            Dim sRadiologi As Decimal = 0
            Dim sLaboratorium As Decimal = 0
            Dim sPelayananDarah As Decimal = 0
            Dim sRehabilitasi As Decimal = 0
            Dim sKamarAkomodasi As Decimal = 0
            Dim sRawatIntensif As Decimal = 0
            Dim sObat As Decimal = 0
            Dim sBMHP As Decimal = 0
            Dim sAlatMedis As Decimal = 0
            Dim sAlkes As Decimal = 0
            Dim sObatPRB As Decimal = 0
            Dim sObatKemoterafi As Decimal = 0

            For iLoop As Integer = 0 To ds.Tables("S_SI_D").Rows.Count - 1
                With ds.Tables("S_SI_D")
                    If .Rows(iLoop)("Kategori") = "-" Then
                        MsgBox("Tindakan " & .Rows(iLoop)("NamaTindakan") & " Belum Masuk Kategori", MsgBoxStyle.Exclamation, Me.Text)
                    End If
                    If .Rows(iLoop)("Kategori") = "PROSEDUR NON-BEDAH" Then
                        sProsedurNonBedah += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "PROSEDUR BEDAH" Then
                        sProsedurBedah += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "KONSULTASI" Then
                        sKonsultasi += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "TENAGA AHLI" Then
                        sTenagaAhli += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "KEPERAWATAN" Then
                        sKeperawatan += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "PENUNJANG" Then
                        sPenunjang += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "RADIOLOGI" Then
                        sRadiologi += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "LABORATORIUM" Then
                        sLaboratorium += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "PELAYANAN DARAH" Then
                        sPelayananDarah += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "REHABILITASI" Then
                        sRehabilitasi += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "ALKES" Then
                        sAlkes += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "OBAT" Then
                        sObat += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "KAMAR / AKOMODASI" Then
                        sKamarAkomodasi += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "OBAT KRONIS" Then
                        sObatPRB += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "BMHP" Then
                        sBMHP += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "RAWAT INTENSIF" Then
                        sRawatIntensif += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "OBAT KEMOTERAPI" Then
                        sObatKemoterafi += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "SEWA ALAT" Then
                        sAlatMedis += .Rows(iLoop)("TotalTarifMargin")
                    End If
                End With
            Next

            txtProsedurNonBedah.Text = sProsedurNonBedah
            txtProsedurBedah.Text = sProsedurBedah
            txtKonsultasi.Text = sKonsultasi
            txtTenagaAhli.Text = sTenagaAhli
            txtKeperawatan.Text = sKeperawatan
            txtPenunjang.Text = sPenunjang
            txtRadiologi.Text = sRadiologi
            txtLaboratorium.Text = sLaboratorium
            txtPelayananDarah.Text = sPelayananDarah
            txtRehabilitasi.Text = sRehabilitasi
            txtKamarAkomodasi.Text = sKamarAkomodasi
            txtRawatIntensif.Text = sRawatIntensif
            txtObat.Text = sObat
            txtBMHP.Text = sBMHP
            txtSewaAlat.Text = sAlatMedis
            txtAlkes.Text = sAlkes
            txtObatKronis.Text = sObatPRB
            txtObatKemoTerapi.Text = sObatKemoterafi
        Catch oErr As Exception
            MsgBox("Load Data" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatData()
        For iLoop As Integer = 0 To grv.Columns.Count - 1
            If grv.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                grv.Columns(iLoop).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                grv.Columns(iLoop).SummaryItem.DisplayFormat = "{0:n2}"
            ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

        'For iLoop As Integer = 0 To grv1.Columns.Count - 1
        '    If grv1.Columns(iLoop).ColumnType.Name = "Decimal" Then
        '        grv1.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        '        grv1.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
        '        grv1.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        '        grv1.Columns(iLoop).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        '        grv1.Columns(iLoop).SummaryItem.DisplayFormat = "{0:n2}"
        '    ElseIf grv1.Columns(iLoop).ColumnType.Name = "DateTime" Then
        '        grv1.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        '        grv1.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
        '    End If
        'Next

        'grv1.Columns("No_Reg").VisibleIndex = -1

        grv.Columns("Transaksi").Group()
        grv.ExpandAllGroups()

        'grv1.Columns("TotalTarifMargin").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        'grv1.Columns("TotalTarifMargin").DisplayFormat.FormatString = "{0:n0}"
        'grv1.Columns("TotalTarifMargin").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        'grv1.Columns("TotalTarifMargin").SummaryItem.FieldName = "TotalTarifMargin"
        'grv1.Columns("TotalTarifMargin").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        'grv1.Columns("TotalTarifMargin").SummaryItem.DisplayFormat = "{0:n0}"

    End Sub
    Private Sub OnValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtProsedurNonBedah.EditValueChanged, txtTenagaAhli.EditValueChanged, txtRadiologi.EditValueChanged, txtRehabilitasi.EditValueChanged _
                                      , txtObat.EditValueChanged, txtAlkes.EditValueChanged, txtProsedurBedah.EditValueChanged, txtKeperawatan.EditValueChanged, txtLaboratorium.EditValueChanged _
                                      , txtKamarAkomodasi.EditValueChanged, txtObatKronis.EditValueChanged, txtBMHP.EditValueChanged, txtKonsultasi.EditValueChanged _
                                      , txtPenunjang.EditValueChanged, txtPelayananDarah.EditValueChanged, txtRawatIntensif.EditValueChanged, txtObatKemoTerapi.EditValueChanged, txtSewaAlat.EditValueChanged
        If isLoad Then
            Calculate()
        End If
    End Sub
    Private Sub Calculate()
        Dim sSubTotal = 0

        sSubTotal = CDec(txtProsedurNonBedah.Text) + CDec(txtTenagaAhli.Text) + CDec(txtRadiologi.Text) + CDec(txtRehabilitasi.Text) _
                                  + CDec(txtObat.Text) + CDec(txtAlkes.Text) + CDec(txtProsedurBedah.Text) + CDec(txtKeperawatan.Text) + CDec(txtLaboratorium.Text) _
                                  + CDec(txtKamarAkomodasi.Text) + CDec(txtObatKronis.Text) + CDec(txtBMHP.Text) + CDec(txtKonsultasi.Text) _
                                  + CDec(txtPenunjang.Text) + CDec(txtPelayananDarah.Text) + CDec(txtRawatIntensif.Text) + CDec(txtObatKemoTerapi.Text) + CDec(txtSewaAlat.Text)

        txttarifRumahSakit.Text = sSubTotal

    End Sub
    Private Sub grdICD_X_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdICD_X.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If grdICD_X.Text <> "" Then
                If txtICD_X.Text = "" Then
                    txtICD_X.Text = grdICD_X.EditValue
                Else
                    txtICD_X.Text = txtICD_X.Text & "#" & grdICD_X.EditValue
                End If
            End If
        End If
    End Sub
    Private Sub grdICD_IX_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdICD_IX.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If grdICD_IX.Text <> "" Then
                If txtICCD_IX.Text = "" Then
                    txtICCD_IX.Text = grdICD_IX.EditValue
                Else
                    txtICCD_IX.Text = txtICCD_IX.Text & "#" & grdICD_IX.EditValue
                End If
            End If
        End If
    End Sub

#End Region
End Class