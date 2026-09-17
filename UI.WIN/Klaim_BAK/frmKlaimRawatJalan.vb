Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Security.Cryptography
Imports System.Text
Imports Newtonsoft.Json.Linq
Imports System.Data
Imports System.Data.SqlClient

Public Class frmKlaimRawatJalan
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
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "Grouper Rawat Jalan"

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
        fn_LoadCARAPULANG()
        fn_LoadKodeTarif()

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
        btnICD.Enabled = Not Status
        btnUpdatePasien.Enabled = Not Status
        btnProsedur.Enabled = Not Status

        txtKDPENDAFTARAN.Properties.ReadOnly = True
        cboCaraBayar.Properties.ReadOnly = Status
        txtNoPeserta.Properties.ReadOnly = Status
        txtNoSEP.Properties.ReadOnly = Status
        grdCOB.Properties.ReadOnly = Status
        chkKelasEksekutif.Properties.ReadOnly = Status
        'chkNaikTurunKelas.Properties.ReadOnly = Status
        'chkAdaRawatKelas.Properties.ReadOnly = Status
        deDATEMASUK.Properties.ReadOnly = Status
        deDATEPULANG.Properties.ReadOnly = Status
        'rbKelasPelayanan.Properties.ReadOnly = Status
        'txtRawatIntensif_hari.Properties.ReadOnly = Status
        txtLOS.Properties.ReadOnly = Status
        txtADLScore_SubAcute.Properties.ReadOnly = Status
        txtADLScore_Chronic.Properties.ReadOnly = Status
        'rbKelasHak.Properties.ReadOnly = Status
        txtUmur.Properties.ReadOnly = Status
        'txtLama.Properties.ReadOnly = Status
        'txtVentilator.Properties.ReadOnly = Status
        txtBeratBadan.Properties.ReadOnly = Status
        grdCaraPulang.Properties.ReadOnly = Status
        grdJenisTarif.Properties.ReadOnly = Status
        txttarifRumahSakit.Properties.ReadOnly = Status
        If chkKelasEksekutif.Checked = False Then
            txtTarifEksekutif.Properties.ReadOnly = True
        Else
            txtTarifEksekutif.Properties.ReadOnly = False
        End If
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
        txtICD_X.Properties.ReadOnly = True
        txtICCD_IX.Properties.ReadOnly = True
    End Sub
    Private Sub fn_EmptyMe()
        txtCODE.ResetText()
        chkKelasEksekutif.Checked = False
        'chkNaikTurunKelas.Checked = False

        txtKDPENDAFTARAN.ResetText()
        cboCaraBayar.SelectedIndex = 0
        txtNoPeserta.ResetText()
        txtNoSEP.ResetText()
        grdJenisTarif.Text = oKlaim.KodeTarif_Default
        grdCOB.Text = oKlaim.COB_Default
        chkKelasEksekutif.Checked = False
        'chkNaikTurunKelas.Checked = False
        'chkAdaRawatKelas.Checked = False
        deDATEMASUK.DateTime = Now
        deDATEPULANG.DateTime = Now
        'rbKelasPelayanan.SelectedIndex = 0
        'txtRawatIntensif_hari.ResetText()
        txtLOS.Text = 1
        txtADLScore_SubAcute.Text = "-"
        txtADLScore_Chronic.Text = "-"
        txtHAKKELAS.Text = "-"
        txtUmur.ResetText()
        'txtLama.ResetText()
        'txtVentilator.ResetText()
        txtBeratBadan.ResetText()
        grdCaraPulang.Text = oKlaim.CaraPulang_Default
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
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oKlaim.GetData(sNoId)

            With ds
                txtCODE.Text = .KDCASHIN
                txtKDPENDAFTARAN.Text = .KDPENDAFTARAN
                cboCaraBayar.SelectedIndex = .CARABAYAR
                txtNoPeserta.Text = .NOPESERTA
                txtNoSEP.Text = .NOSEP
                grdCOB.Text = .KDCOB
                chkKelasEksekutif.Checked = .ISKELASEKSEKUTIF
                'chkNaikTurunKelas.Checked = .ISNAIKTURUNKELAS
                'chkAdaRawatKelas.Checked = .ISADARAWATINTENSIF
                deDATEMASUK.DateTime = .DATE_MASUK
                deDATEPULANG.DateTime = .DATE_KELUAR
                'rbKelasPelayanan.SelectedIndex = .KELASPELAYANAN
                'txtRawatIntensif_hari.Text = .RAWATINTENSIF_HARI
                txtLOS.Text = .LOS
                txtADLScore_SubAcute.Text = .ADLSCORE_SUBACUTE
                txtADLScore_Chronic.Text = .ADLSCORE_CHRONIC
                grdDPJP.Text = .DPJP
                txtHAKKELAS.Text = "-"
                txtUmur.Text = .UMUR
                'txtLama.Text = .LAMA
                'txtVentilator.Text = .VENTILATOR
                txtBeratBadan.Text = .BERATBADAN
                grdCaraPulang.Text = .KDCARAKELUAR
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
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtKDPENDAFTARAN.Text = String.Empty Then
                txtKDPENDAFTARAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDPENDAFTARAN.ErrorText = Statement.ErrorRequired

                txtKDPENDAFTARAN.Focus()
                fn_Validate = False
                Exit Function
            End If

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Dim dsRegister = oKlaim.GetDatabykdpendaftaran(txtKDPENDAFTARAN.Text)
                If dsRegister IsNot Nothing Then
                    MsgBox("No Pendaftaran Sudah di Grouper dengan Kode Grouper " & dsRegister.KDCASHIN, MsgBoxStyle.Exclamation, Me.Text)
                    fn_Validate = False
                    Exit Function
                End If
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** ECLAIM *****
            If fn_Membuatklaimbaru() = True Then
                fn_MengisiUpdateDataKlaim()
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

                .KDCASHIN = txtCODE.Text
                .KDPENDAFTARAN = txtKDPENDAFTARAN.Text
                .CARABAYAR = cboCaraBayar.SelectedIndex
                .NOPESERTA = txtNoPeserta.Text
                .NOSEP = txtNoSEP.Text
                .KDCOB = grdCOB.EditValue
                .CATEGORY = CInt(0)
                .ISKELASEKSEKUTIF = chkKelasEksekutif.Checked
                .ISNAIKTURUNKELAS = False
                .ISADARAWATINTENSIF = False
                .DATE_MASUK = deDATEMASUK.DateTime
                .DATE_KELUAR = deDATEPULANG.DateTime
                .KELASPELAYANAN = CInt(0)
                .RAWATINTENSIF_HARI = CInt(0)
                .LOS = CInt(txtLOS.Text)
                .ADLSCORE_SUBACUTE = txtADLScore_SubAcute.Text
                .ADLSCORE_CHRONIC = txtADLScore_Chronic.Text
                .DPJP = grdDPJP.EditValue
                .HAKKELAS = CInt(0)
                .UMUR = txtUmur.Text
                .LAMA = CInt(0)
                .VENTILATOR = CInt(0)
                .BERATBADAN = CInt(txtBeratBadan.Text)
                .KDCARAKELUAR = grdCaraPulang.EditValue
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
            Dim dsPendaftaran = oPendaftaran.GetData(txtKDPENDAFTARAN.Text)

            Dim dsMembuatKlaimBaru = oSetKoneksi.fn_Membuatklaimbaru("ECLAIM", "{" & """metadata"": {" & """method"": " & """new_claim""    }," & """data"": {" & """nomor_kartu"": """ & txtNoPeserta.Text & """, " & """nomor_sep"": """ & txtNoSEP.Text & """, " & """nomor_rm"": """ & dsPendaftaran.KDCUSTOMER & """, " & """nama_pasien"": """ & dsPendaftaran.M_CUSTOMER.NAME_DISPLAY & """, " & """tgl_lahir"": """ & dsPendaftaran.M_CUSTOMER.TANGGALLAHIR.ToString("yyyy-MM-dd HH:mm:ss") & """, " & """gender"": """ & IIf(dsPendaftaran.M_CUSTOMER.KDJENISKELAMIN = 0, "1", "2") & """   } } ")

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
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Membuatklaimbaru = False
        End Try
    End Function
    Private Function fn_MengisiUpdateDataKlaim() As Boolean
        Try
            Dim dsPendaftaran = oPendaftaran.GetData(txtKDPENDAFTARAN.Text)
            Dim oSetUser As New Setting.clsUser

            Dim dsMembuatKlaimBaru = oSetKoneksi.fn_MengisiUpdateDataKlaim("ECLAIM", "{" & """metadata"": { " & """method"": " & """set_claim_data"",    " & """nomor_sep"": """ & txtNoSEP.Text & """  },  " & """data"": {    " & """nomor_sep"": """ & txtNoSEP.Text & """,    " & """nomor_kartu"": """ & txtNoPeserta.Text & """,    " & """tgl_masuk"": """ & deDATEMASUK.DateTime.ToString("yyyy-MM-dd HH:mm:ss") & """,    " & """tgl_pulang"": """ & deDATEPULANG.DateTime.ToString("yyyy-MM-dd HH:mm:ss") & """,    " & """jenis_rawat"": """ & "2" & """,    " & """kelas_rawat"": """ & IIf(chkKelasEksekutif.Checked = False, "3", "1") & """,    " & """adl_sub_acute"": """ & txtADLScore_SubAcute.Text & """,    " & """adl_chronic"": """ & txtADLScore_Chronic.Text & """,    " & """icu_indikator"": """ & "0" & """,    " & """icu_los"": """ & "0" & """,    " & """ventilator_hour"": """ & "0" & """,    " & """upgrade_class_ind"": """ & "0" & """,    " & """upgrade_class_class"": """ & "" & """,    " & """upgrade_class_los"": """ & 0 & """,    " & """add_payment_pct"": """ & 0 & """,    " & """birth_weight"": """ & txtBeratBadan.Text & """,    " & """discharge_status"": """ & grdCaraPulang.EditValue & """,    " & """diagnosa"": """ & txtICD_X.Text & """,    " & """procedure"": """ & txtICCD_IX.Text & """,    " & """tarif_rs"": {      " & """prosedur_non_bedah"": """ & CInt(txtProsedurNonBedah.Text) & """,      " & """prosedur_bedah"": """ & CInt(txtProsedurBedah.Text) & """,      " & """konsultasi"": """ & CInt(txtKonsultasi.Text) & """,      " & """tenaga_ahli"": """ & CInt(txtTenagaAhli.Text) & """,      " & """keperawatan"": """ & CInt(txtKeperawatan.Text) & """,      " & """penunjang"": """ & CInt(txtPenunjang.Text) & """,      " & """radiologi"": """ & CInt(txtRadiologi.Text) & """,      " & """laboratorium"": """ & CInt(txtLaboratorium.Text) & """,      " & """pelayanan_darah"": """ & CInt(txtPelayananDarah.Text) & """,      " & """rehabilitasi"": """ & CInt(txtRehabilitasi.Text) & """,      " & """kamar"": """ & CInt(txtKamarAkomodasi.Text) & """,      " & """rawat_intensif"": """ & CInt(txtRawatIntensif.Text) & """,   " & """obat"": """ & CInt(txtObat.Text) & """,   " & """obat_kronis"": """ & CInt(txtObatKronis.Text) & """, " & """obat_kemoterapi"": """ & CInt(txtObatKemoTerapi.Text) & """,      " & """alkes"": """ & CInt(txtAlkes.Text) & """,      " & """bmhp"": """ & CInt(txtBMHP.Text) & """,      " & """sewa_alat"": """ & CInt(txtSewaAlat.Text) & """    },    " & """tarif_poli_eks"": """ & CInt(txtTarifEksekutif.Text) & """,    " & """nama_dokter"": """ & grdDPJP.Text & """,    " & """kode_tarif"": """ & grdJenisTarif.EditValue & """,    " & """payor_id"": """ & cboCaraBayar.Text & """,    " & """payor_cd"": """ & IIf(cboCaraBayar.Text = "JKN", "3", IIf(cboCaraBayar.Text = "JAMKESDA", "5", IIf(cboCaraBayar.Text = "JAMKESOS", "6", "1"))) & """,    " & """cob_cd"": """ & IIf(grdCOB.Text = "-", "#", grdCOB.EditValue) & """,    " & """coder_nik"": """ & oSetUser.GetData(sUserID).NIK & """  } } ")

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
            Case Keys.F5
                If btnICD.Enabled = True Then
                    btnICD_Click()
                End If
        End Select
    End Sub
    Private Sub btnUpdatePasien_Click() Handles btnUpdatePasien.ItemClick
        Try
            Dim dsPendaftaran = oPendaftaran.GetData(txtKDPENDAFTARAN.Text)
            Dim Request As String = ""

            Dim dsMembuatKlaimBaru = oSetKoneksi.fn_UpdateDataPasien("ECLAIM", "{" & """metadata"": {" & """method"": " & """update_patient""    }," & """data"": {" & """nomor_kartu"": """ & txtNoPeserta.Text & """, " & """nomor_rm"": """ & dsPendaftaran.KDCUSTOMER & """, " & """nama_pasien"": """ & dsPendaftaran.M_CUSTOMER.NAME_DISPLAY & """, " & """tgl_lahir"": """ & dsPendaftaran.M_CUSTOMER.TANGGALLAHIR.ToString("yyyy-MM-dd HH:mm:ss") & """, " & """gender"": """ & IIf(dsPendaftaran.M_CUSTOMER.KDJENISKELAMIN = 0, "1", "2") & """   } } ")

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
    Private Sub btnICD_Click() Handles btnICD.ItemClick
        If txtKDPENDAFTARAN.Text = String.Empty Then Exit Sub

        Dim oSalesOrderTransaksi As New Sales.clsSalesOrderTransaksi

        Dim dsKunjungan = oSalesOrderTransaksi.GetDataMasterDiagnosaByKD(txtKDPENDAFTARAN.Text)

        If dsKunjungan IsNot Nothing Then
            frmDiagnosaMaster.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsKunjungan.KDPENDAFTARAN, dsKunjungan.KDDIAGNOSAMASTER)
            frmDiagnosaMaster.ShowDialog(Me)
        Else
            frmDiagnosaMaster.LoadMe(FORM_MODE.FORM_MODE_ADD, dsKunjungan.KDPENDAFTARAN)
            frmDiagnosaMaster.ShowDialog(Me)
        End If
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
    Private Sub fn_LoadTarif(ByVal KDCASHIN As String)
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

            SQL = " SELECT  "
            SQL &= " F.KDITEM_L3 "
            SQL &= " ,TARIF = SUM(D.GRANDTOTAL) "
            SQL &= " FROM F_CASHIN_H A  "
            SQL &= " INNER JOIN F_CASHIN_D B "
            SQL &= " ON A.KDCASHIN = B.KDCASHIN "
            SQL &= " INNER JOIN S_SO_TRANSAKSI_H C  "
            SQL &= " ON B.NOINVOICE = C.KDSOTRANSAKSI "
            SQL &= " INNER JOIN S_SO_TRANSAKSI_D D "
            SQL &= " ON C.KDSOTRANSAKSI = D.KDSOTRANSAKSI "
            SQL &= " INNER JOIN M_ITEM E "
            SQL &= " ON D.KDITEM = E.KDITEM "
            SQL &= " INNER JOIN M_ITEM_L3 F "
            SQL &= " ON E.KDITEM_L3 = F.KDITEM_L3  "
            SQL &= " WHERE "
            SQL &= " A.KDCASHIN = '" & KDCASHIN & "' "
            SQL &= " GROUP BY "
            SQL &= " F.KDITEM_L3 "


            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "LOAD_TARIF")

            Dim PROSEDURNONBEDAH As Integer = 0
            Dim TENAGAAHLI As Integer = 0
            Dim RADIOLOGI As Integer = 0
            Dim REHABILITASI As Integer = 0
            Dim OBAT As Integer = 0
            Dim ALKES As Integer = 0
            Dim PROSEDURBEDAH As Integer = 0
            Dim KEPERAWATAN As Integer = 0
            Dim LABORATORIUM As Integer = 0
            Dim KAMARAKOMODASI As Integer = 0
            Dim OBATKRONIS As Integer = 0
            Dim BMHP As Integer = 0
            Dim KONSULTASI As Integer = 0
            Dim PENUNJANG As Integer = 0
            Dim PELAYANANDARAH As Integer = 0
            Dim RAWATINTENSIF As Integer = 0
            Dim OBATKEMOTERAPI As Integer = 0
            Dim SEWAALAT As Integer = 0
            Dim TARIFRS As Integer = 0
            Dim TARIFBELUMMASUK As Integer = 0

            For iLoop As Integer = 0 To ds.Tables("LOAD_TARIF").Rows.Count - 1
                With ds.Tables("LOAD_TARIF")
                    If .Rows(iLoop)("KDITEM_L3") = "001" Then
                        PROSEDURNONBEDAH += .Rows(iLoop)("TARIF")
                    ElseIf .Rows(iLoop)("KDITEM_L3") = "002" Then
                        PROSEDURBEDAH += .Rows(iLoop)("TARIF")
                    ElseIf .Rows(iLoop)("KDITEM_L3") = "003" Then
                        KONSULTASI += .Rows(iLoop)("TARIF")
                    ElseIf .Rows(iLoop)("KDITEM_L3") = "004" Then
                        TENAGAAHLI += .Rows(iLoop)("TARIF")
                    ElseIf .Rows(iLoop)("KDITEM_L3") = "005" Then
                        KEPERAWATAN += .Rows(iLoop)("TARIF")
                    ElseIf .Rows(iLoop)("KDITEM_L3") = "006" Then
                        PENUNJANG += .Rows(iLoop)("TARIF")
                    ElseIf .Rows(iLoop)("KDITEM_L3") = "007" Then
                        RADIOLOGI += .Rows(iLoop)("TARIF")
                    ElseIf .Rows(iLoop)("KDITEM_L3") = "008" Then
                        LABORATORIUM += .Rows(iLoop)("TARIF")
                    ElseIf .Rows(iLoop)("KDITEM_L3") = "009" Then
                        PELAYANANDARAH += .Rows(iLoop)("TARIF")
                    ElseIf .Rows(iLoop)("KDITEM_L3") = "010" Then
                        REHABILITASI += .Rows(iLoop)("TARIF")
                    ElseIf .Rows(iLoop)("KDITEM_L3") = "011" Then
                        KAMARAKOMODASI += .Rows(iLoop)("TARIF")
                    ElseIf .Rows(iLoop)("KDITEM_L3") = "012" Then
                        RAWATINTENSIF += .Rows(iLoop)("TARIF")
                    ElseIf .Rows(iLoop)("KDITEM_L3") = "013" Then
                        OBAT += .Rows(iLoop)("TARIF")
                    ElseIf .Rows(iLoop)("KDITEM_L3") = "014" Then
                        ALKES += .Rows(iLoop)("TARIF")
                    ElseIf .Rows(iLoop)("KDITEM_L3") = "015" Then
                        BMHP += .Rows(iLoop)("TARIF")
                    ElseIf .Rows(iLoop)("KDITEM_L3") = "016" Then
                        SEWAALAT += .Rows(iLoop)("TARIF")
                    ElseIf .Rows(iLoop)("KDITEM_L3") = "017" Then
                        OBATKRONIS += .Rows(iLoop)("TARIF")
                    ElseIf .Rows(iLoop)("KDITEM_L3") = "018" Then
                        OBATKEMOTERAPI += .Rows(iLoop)("TARIF")
                    Else
                        TARIFBELUMMASUK += .Rows(iLoop)("TARIF")
                    End If

                    TARIFRS += .Rows(iLoop)("TARIF")

                End With
            Next

            txtProsedurNonBedah.Text = PROSEDURNONBEDAH
            txtTenagaAhli.Text = TENAGAAHLI
            txtRadiologi.Text = RADIOLOGI
            txtRehabilitasi.Text = REHABILITASI
            txtObat.Text = OBAT
            txtAlkes.Text = ALKES
            txtProsedurBedah.Text = PROSEDURBEDAH
            txtKeperawatan.Text = KEPERAWATAN
            txtLaboratorium.Text = LABORATORIUM
            txtKamarAkomodasi.Text = KAMARAKOMODASI
            txtObatKronis.Text = OBATKRONIS
            txtBMHP.Text = BMHP
            txtKonsultasi.Text = KONSULTASI
            txtPenunjang.Text = PENUNJANG
            txtPelayananDarah.Text = PELAYANANDARAH
            txtRawatIntensif.Text = RAWATINTENSIF
            txtObatKemoTerapi.Text = OBATKEMOTERAPI
            txtSewaAlat.Text = SEWAALAT
            txttarifRumahSakit.Text = TARIFRS

            If TARIFBELUMMASUK > 0 Then
                MsgBox("Total Tarif Belum Masuk " & FormatNumber(TARIFBELUMMASUK, 2), MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKoding(ByVal KDPENDAFTARAN As String)
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

            SQL = " SELECT  "
            SQL &= " * "
            SQL &= " FROM S_PENDAFTARAN_DIAGNOSA_MASTER A  "
            SQL &= " WHERE "
            SQL &= " A.KDPENDAFTARAN = '" & KDPENDAFTARAN & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "LOAD_DIAGNOSA")

            Dim listDiagnosa As New List(Of String)
            Dim listTindakan As New List(Of String)

            For iLoop As Integer = 0 To ds.Tables("LOAD_DIAGNOSA").Rows.Count - 1
                With ds.Tables("LOAD_DIAGNOSA")
                    If .Rows(iLoop)("KDDIAGNOSA1") <> "-" Then
                        listDiagnosa.Add(.Rows(iLoop)("KDDIAGNOSA1"))
                    End If
                    If .Rows(iLoop)("KDDIAGNOSA2") <> "-" Then
                        listDiagnosa.Add(.Rows(iLoop)("KDDIAGNOSA2"))
                    End If
                    If .Rows(iLoop)("KDDIAGNOSA3") <> "-" Then
                        listDiagnosa.Add(.Rows(iLoop)("KDDIAGNOSA3"))
                    End If
                    If .Rows(iLoop)("KDDIAGNOSA4") <> "-" Then
                        listDiagnosa.Add(.Rows(iLoop)("KDDIAGNOSA4"))
                    End If
                    If .Rows(iLoop)("KDDIAGNOSA5") <> "-" Then
                        listDiagnosa.Add(.Rows(iLoop)("KDDIAGNOSA5"))
                    End If
                    If .Rows(iLoop)("KDDIAGNOSA6") <> "-" Then
                        listDiagnosa.Add(.Rows(iLoop)("KDDIAGNOSA6"))
                    End If
                    If .Rows(iLoop)("KDPROCEDURE1") <> "-" Then
                        listTindakan.Add(.Rows(iLoop)("KDPROCEDURE1"))
                    End If
                    If .Rows(iLoop)("KDPROCEDURE2") <> "-" Then
                        listTindakan.Add(.Rows(iLoop)("KDPROCEDURE2"))
                    End If
                    If .Rows(iLoop)("KDPROCEDURE3") <> "-" Then
                        listTindakan.Add(.Rows(iLoop)("KDPROCEDURE3"))
                    End If
                    If .Rows(iLoop)("KDPROCEDURE4") <> "-" Then
                        listTindakan.Add(.Rows(iLoop)("KDPROCEDURE4"))
                    End If
                    If .Rows(iLoop)("KDPROCEDURE5") <> "-" Then
                        listTindakan.Add(.Rows(iLoop)("KDPROCEDURE5"))
                    End If
                    If .Rows(iLoop)("KDPROCEDURE6") <> "-" Then
                        listTindakan.Add(.Rows(iLoop)("KDPROCEDURE6"))
                    End If
                End With
            Next

            If listDiagnosa.Count > 0 Then
                txtICD_X.Text = String.Join("#", listDiagnosa.ToArray)
            End If
            If listTindakan.Count > 0 Then
                txtICCD_IX.Text = String.Join("#", listTindakan.ToArray)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKodeTarif()
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
    Private Sub fn_LoadCARAPULANG()
        Dim oCARAPULANG As New Reference.clsCaraKeluar
        Try
            grdCaraPulang.Properties.DataSource = oCARAPULANG.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdCaraPulang.Properties.ValueMember = "KDCARAKELUAR"
            grdCaraPulang.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub txtCARI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCARI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If cboCARI.SelectedIndex = 0 Then
                fn_LoadKDPENDAFTARANREKAMMEDIS(txtCARI.Text)
            ElseIf cboCARI.SelectedIndex = 1 Then
                fn_LoadKDPENDAFTARANREKAMNAMA(txtCARI.Text)
            Else
                fn_LoadKDPENDAFTARAN(txtCARI.Text)
            End If
            txtCARI.ResetText()
        End If
    End Sub
    Private Sub fn_LoadKDPENDAFTARANREKAMMEDIS(ByVal Parameter As String)
        Try
            Dim oPendaftaran As New Admission.clsPendaftaran

            Dim dsPendaftaran = (From x In oPendaftaran.GetDataListrm(Parameter)
                                 Select x.KDCASHIN, x.KDPENDAFTARAN, CATEGORY = x.S_PENDAFTARAN_H.CATEGORY, x.S_PENDAFTARAN_H.KDCUSTOMER, x.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY, TUJUAN = x.S_PENDAFTARAN_H.M_DEPARTMENT.NAME_DISPLAY, DOKTER = x.S_PENDAFTARAN_H.M_DOCTOR.NAME_DISPLAY, x.DATE).OrderByDescending(Function(x) x.DATE)

            grdKDCASHIN.Properties.DataSource = dsPendaftaran.Where(Function(x) x.CATEGORY = 0).OrderBy(Function(x) x.DATE).ToList()
            grdKDCASHIN.Properties.ValueMember = "KDCASHIN"
            grdKDCASHIN.Properties.DisplayMember = "KDCASHIN"

            grdKDCASHIN.ShowPopup()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDPENDAFTARANREKAMNAMA(ByVal Parameter As String)
        Try
            Dim oPendaftaran As New Admission.clsPendaftaran

            Dim dsPendaftaran = (From x In oPendaftaran.GetDataListnama(Parameter)
                                 Select x.KDCASHIN, x.KDPENDAFTARAN, CATEGORY = x.S_PENDAFTARAN_H.CATEGORY, x.S_PENDAFTARAN_H.KDCUSTOMER, x.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY, TUJUAN = x.S_PENDAFTARAN_H.M_DEPARTMENT.NAME_DISPLAY, DOKTER = x.S_PENDAFTARAN_H.M_DOCTOR.NAME_DISPLAY, x.DATE).OrderByDescending(Function(x) x.DATE)

            grdKDCASHIN.Properties.DataSource = dsPendaftaran.Where(Function(x) x.CATEGORY = 0).OrderBy(Function(x) x.DATE).ToList()
            grdKDCASHIN.Properties.ValueMember = "KDCASHIN"
            grdKDCASHIN.Properties.DisplayMember = "KDCASHIN"

            grdKDCASHIN.ShowPopup()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDPENDAFTARAN(ByVal Parameter As String)
        Try
            Dim oPendaftaran As New Admission.clsPendaftaran

            Dim dsPendaftaran = (From x In oPendaftaran.GetDataListKDREG(Parameter)
                                 Select x.KDCASHIN, x.KDPENDAFTARAN, CATEGORY = x.S_PENDAFTARAN_H.CATEGORY, x.S_PENDAFTARAN_H.KDCUSTOMER, x.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY, TUJUAN = x.S_PENDAFTARAN_H.M_DEPARTMENT.NAME_DISPLAY, DOKTER = x.S_PENDAFTARAN_H.M_DOCTOR.NAME_DISPLAY, x.DATE).OrderByDescending(Function(x) x.DATE)

            grdKDCASHIN.Properties.DataSource = dsPendaftaran.Where(Function(x) x.CATEGORY = 0).OrderBy(Function(x) x.DATE).ToList()
            grdKDCASHIN.Properties.ValueMember = "KDPENDAFTARAN"
            grdKDCASHIN.Properties.DisplayMember = "KDPENDAFTARAN"

            grdKDCASHIN.ShowPopup()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDPENDAFTARAN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdKDCASHIN.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Dim oPendaftaran As New Admission.clsPendaftaran
            Dim dsPendaftaran = oPendaftaran.GetDataCashin(grdKDCASHIN.EditValue)
            If dsPendaftaran IsNot Nothing Then
                txtCODE.Text = dsPendaftaran.KDCASHIN
                deDATEMASUK.DateTime = dsPendaftaran.S_PENDAFTARAN_H.DATE
                deDATEPULANG.DateTime = dsPendaftaran.S_PENDAFTARAN_H.DATE
                txtKDPENDAFTARAN.Text = dsPendaftaran.KDPENDAFTARAN
                'If dsPendaftaran.KDKELASRAWAT = "3" Then
                '    rbKelasHak.SelectedIndex = 0
                'ElseIf dsPendaftaran.KDKELASRAWAT = "4" Then
                '    rbKelasHak.SelectedIndex = 1
                'Else
                '    rbKelasHak.SelectedIndex = 2
                'End If
                txtNoPeserta.Text = dsPendaftaran.S_PENDAFTARAN_H.KARTUBPJS
                txtNoSEP.Text = dsPendaftaran.S_PENDAFTARAN_H.NOMORSEP
                grdDPJP.Text = dsPendaftaran.S_PENDAFTARAN_H.KDDOCTOR

                txtUmur.Text = HitungUmur(dsPendaftaran.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR, dsPendaftaran.S_PENDAFTARAN_H.DATE)

                fn_LoadTarif(grdKDCASHIN.EditValue)
                fn_LoadKoding(txtKDPENDAFTARAN.Text)
            End If
        End If
    End Sub
    Function HitungUmur(ByVal tanggallahir As Date, ByVal tanggaldatang As Date) As String
        Dim y, m, d As Integer

        d = tanggaldatang.Day - tanggallahir.Day
        m = tanggaldatang.Month - tanggallahir.Month
        y = tanggaldatang.Year - tanggallahir.Year

        If Math.Sign(d) = -1 Then
            d = 30 - Math.Abs(d)
            m -= 1
        End If
        If Math.Sign(m) = -1 Then
            m = 12 - Math.Abs(m)
            y -= 1
        End If

        If y = 0 Then
            If m = 0 Then
                HitungUmur = d & " hari"
            Else
                HitungUmur = m & " bulan"
            End If
        Else
            HitungUmur = y & " tahun"
        End If
        'HitungUmur = y & " tahun, " & m & " bulan, " & d & " hari"
    End Function
    'Private Sub rbCATEGORY_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rbCATEGORY.SelectedIndexChanged
    '    fn_HiddenCategory()
    'End Sub
    Private Sub txtNoSEP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNoSEP.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If txtNoSEP.Text.Count = 19 Then
                MsgBox(fn_IntegrasiSEPdenganInacbg(txtNoSEP.Text.ToString.Trim.ToUpper), MsgBoxStyle.Information, Me.Text)
            ElseIf txtNoSEP.Text.Count > 19 Then
                MsgBox("Validasi : " & vbCrLf & "No SEP lebih dari 19 Digit", MsgBoxStyle.Exclamation, Me.Text)
            Else
                MsgBox("Validasi : " & vbCrLf & "No SEP kurang dari 19 Digit", MsgBoxStyle.Exclamation, Me.Text)
            End If
        End If
    End Sub
    Private Function fn_IntegrasiSEPdenganInacbg(ByVal NOMORSEP As String) As String
        Try
            If NOMORSEP = String.Empty Then
                fn_IntegrasiSEPdenganInacbg = ""
                Exit Function
            End If

            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim dsSetKoneksi = oSetKoneksi.IntegrasiSEPdenganInacbg("VCLAIM", NOMORSEP)

            Dim allData = JObject.Parse(dsSetKoneksi)
            Dim CodeResponse As String = String.Empty
            Dim messageResponse As String = String.Empty

            CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
            messageResponse = allData("metaData")("message").ToString

            If CodeResponse = "200" Then
                fn_IntegrasiSEPdenganInacbg = dsSetKoneksi
            Else
                fn_IntegrasiSEPdenganInacbg = dsSetKoneksi
            End If

        Catch oErr As Exception
            fn_IntegrasiSEPdenganInacbg = Statement.ErrorStatement & vbCrLf & oErr.Message
            'MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub chkKelasEksekutif_CheckedChanged(sender As Object, e As EventArgs) Handles chkKelasEksekutif.CheckedChanged
        If chkKelasEksekutif.Checked = False Then
            txtTarifEksekutif.Properties.ReadOnly = True
            txtTarifEksekutif.Text = "0"
        Else
            txtTarifEksekutif.Properties.ReadOnly = False
            txtTarifEksekutif.Text = "0"
        End If
    End Sub
#End Region
End Class