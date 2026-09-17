Imports System.Data.SqlClient
Imports DataAccess
Imports UI.WIN.MAIN.My.Resources

Public Class frmEMedrekRJ_04
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_RJ_04 As New Digital.clsDigital_RJ_04
    Private down As Boolean = False
    Private sIsOtority As Boolean = False

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String)
        oFormMode = FormMode

        Dim oPendaftaran As New Identitas.clsIdentitasPasien
        Dim dsPendaftaran = oPendaftaran.GetData(KDREG)

        txtNoRegister.Text = KDREG

        If dsPendaftaran IsNot Nothing Then
            txtNoRM.Text = dsPendaftaran.KDCUSTOMER
            txtNamaPasien.Text = dsPendaftaran.NAMAPASIEN
            txtUmur.Text = dsPendaftaran.USIA
            txtNoRegister.Text = dsPendaftaran.KDKUNJUNGAN
            txtTujuan.Text = dsPendaftaran.TUJUAN
            txtDR.Text = dsPendaftaran.DOKTER
            txtNIK.Text = dsPendaftaran.NIK
            txtTmpTglLahir.Text = dsPendaftaran.TEMPATLAHIR & ", " & CDate(dsPendaftaran.TANGGALLAHIR).ToString("dd-MM-yyyy HH:mm:ss")
            txtPangkatGol.Text = dsPendaftaran.PANGKAT
            txtKesatuan.Text = dsPendaftaran.KESATUAN
            txtNoTelepon.Text = dsPendaftaran.NOMORTELEPON
            txtPendidikan.Text = dsPendaftaran.HUBUNGAN_PEKERJAAN
            txtSukuBangsa.Text = dsPendaftaran.SUKU
            txtTanggalMasuk.Text = dsPendaftaran.DATE.ToString("dd-MM-yyyy")
            txtJam.Text = dsPendaftaran.DATE.ToString("HH:mm:ss")
            txtAlamat.Text = dsPendaftaran.ALAMAT

        Else
            txtNoRM.ResetText()
            txtNamaPasien.ResetText()
            txtUmur.ResetText()
            txtNoRegister.ResetText()
            txtTujuan.ResetText()
            txtDR.ResetText()
            txtNIK.ResetText()
            txtTmpTglLahir.ResetText()
            txtPangkatGol.ResetText()
            txtKesatuan.ResetText()
            txtNoTelepon.ResetText()
            txtPendidikan.ResetText()
            txtSukuBangsa.ResetText()
            txtTanggalMasuk.ResetText()
            txtJam.ResetText()
            txtAlamat.ResetText()
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

        chkRujukanYa.Properties.ReadOnly = Status
        chkRS.Properties.ReadOnly = Status
        txtRS.ReadOnly = Status
        chkPuskesmas.Properties.ReadOnly = Status
        txtPuskesmas.ReadOnly = Status
        chkDR.Properties.ReadOnly = Status
        txtDR.ReadOnly = Status
        chkLainnya.Properties.ReadOnly = Status
        txtLainnya.ReadOnly = Status
        txtDXMedis.Properties.ReadOnly = Status

        chkRiwayatKehamilanCukupBulan.Properties.ReadOnly = Status
        chkRiwayatKehamilanPrematur.Properties.ReadOnly = Status
        chkRiwayatKehamilanTrauma.Properties.ReadOnly = Status
        chkRiwayatKehamilanTokso.Properties.ReadOnly = Status
        chkRiwayatKehamilanTAK.Properties.ReadOnly = Status
        chkRiwayatPersalinanNormal.Properties.ReadOnly = Status
        chkRiwayatPersalinanSC.Properties.ReadOnly = Status
        chkRiwayatPersalinanKetubanPecahDini.Properties.ReadOnly = Status
        chkRiwayatPostNatalKejang.Properties.ReadOnly = Status
        chkRiwayatPostNatalAfiksia.Properties.ReadOnly = Status
        chkRiwayatPostNatalKuning.Properties.ReadOnly = Status
        chkRiwayatPostNatalObat.Properties.ReadOnly = Status
        chkRiwayatPostNatalTAK.Properties.ReadOnly = Status

        txtApgar.Properties.ReadOnly = Status
        txtBeratBadanLahir.Properties.ReadOnly = Status
        txtPanjangLahir.Properties.ReadOnly = Status
        txtAnakKe.Properties.ReadOnly = Status
        chkPenyertaTAK.Properties.ReadOnly = Status
        txtPenyerta.Properties.ReadOnly = Status
        chkMotorikTAK.Properties.ReadOnly = Status
        chkMotorikKeTerungkap.Properties.ReadOnly = Status
        chkMotorikKeDuduk.Properties.ReadOnly = Status
        chkMotorikMerayap.Properties.ReadOnly = Status
        chkMotorikMerangkak.Properties.ReadOnly = Status
        chkMotorikKeBerdiri.Properties.ReadOnly = Status
        chkMotorikRambatan.Properties.ReadOnly = Status
        chkMotorikJalan.Properties.ReadOnly = Status
        chkMotorikBicara.Properties.ReadOnly = Status
        TextEdit6.Properties.ReadOnly = Status

        chkNutrisiNormal.Properties.ReadOnly = Status
        chkNutrisiBerlebih.Properties.ReadOnly = Status
        chkNutrisiKurang.Properties.ReadOnly = Status
        chkOrientasiNormal.Properties.ReadOnly = Status
        chkOrientasiBerlebih.Properties.ReadOnly = Status
        chkOrientasiKurang.Properties.ReadOnly = Status
        chkPostureScoliosis.Properties.ReadOnly = Status
        chkPostureKurus.Properties.ReadOnly = Status
        chkPostureDeformitas.Properties.ReadOnly = Status
        chkPostureTAK.Properties.ReadOnly = Status
        chkBABBAKBisaKontrol.Properties.ReadOnly = Status
        chkBABBAKTBisaKontrol.Properties.ReadOnly = Status
        chkBABBAKTTAK.Properties.ReadOnly = Status
        chkPsikosialBermain.Properties.ReadOnly = Status
        chkPsikosialTidak.Properties.ReadOnly = Status

        txtPemahamanLisan.Properties.ReadOnly = Status
        txtPengujianLisan.Properties.ReadOnly = Status
        txtBibir.Properties.ReadOnly = Status
        txtLidah.Properties.ReadOnly = Status
        txtLangitLangit.Properties.ReadOnly = Status
        txtRahang.Properties.ReadOnly = Status
        txtPipi.Properties.ReadOnly = Status
        txtPersepsiBunyi.Properties.ReadOnly = Status
        txtArtikulasi.Properties.ReadOnly = Status
        txtPangjangKata.Properties.ReadOnly = Status
        txtReseptif.Properties.ReadOnly = Status
        txtFonasi.Properties.ReadOnly = Status
        txtResonasi.Properties.ReadOnly = Status
        txtMakan.Properties.ReadOnly = Status
        txtMinum.Properties.ReadOnly = Status
        txtNeurologi.Properties.ReadOnly = Status
        txtTHT.Properties.ReadOnly = Status
        txtPsikologi.Properties.ReadOnly = Status
        txtDiagnosa.Properties.ReadOnly = Status
        txtProgram.Properties.ReadOnly = Status
        txtEvaluasi.Properties.ReadOnly = Status

        Dim oSetUser As New Setting.clsUser
        Dim dsUser = oSetUser.GetData(sUserID)
        If dsUser IsNot Nothing Then
            If dsUser.ISOTORTY = True Then
                lTanggal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                sIsOtority = True
            Else
                lTanggal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                sIsOtority = False
            End If
        End If
    End Sub
    Private Sub fn_EmptyMe()
        'txtCODE.Text = "<--- AUTO --->"

        chkRujukanYa.Checked = False
        chkRS.Checked = False
        txtRS.ResetText()
        chkPuskesmas.Checked = False
        txtPuskesmas.ResetText()
        chkDR.Checked = False
        txtDR.ResetText()
        chkLainnya.Checked = False
        txtLainnya.ResetText()
        txtDXMedis.ResetText()
        
        chkRujukanYa.Checked = False
        chkRS.Checked = False
        txtRS.ResetText()
        chkPuskesmas.Checked = False
        txtPuskesmas.ResetText()
        chkDR.Checked = False
        txtDR.ResetText()
        chkLainnya.Checked = False
        txtLainnya.ResetText()
        txtDXMedis.ResetText()

        chkRiwayatKehamilanCukupBulan.Checked = False
        chkRiwayatKehamilanPrematur.Checked = False
        chkRiwayatKehamilanTrauma.Checked = False
        chkRiwayatKehamilanTokso.Checked = False
        chkRiwayatKehamilanTAK.Checked = False
        chkRiwayatPersalinanNormal.Checked = False
        chkRiwayatPersalinanSC.Checked = False
        chkRiwayatPersalinanKetubanPecahDini.Checked = False
        chkRiwayatPostNatalKejang.Checked = False
        chkRiwayatPostNatalAfiksia.Checked = False
        chkRiwayatPostNatalKuning.Checked = False
        chkRiwayatPostNatalObat.Checked = False
        chkRiwayatPostNatalTAK.Checked = False


        txtApgar.ResetText()
        txtBeratBadanLahir.ResetText()
        txtPanjangLahir.ResetText()
        txtAnakKe.ResetText()
        chkPenyertaTAK.Checked = False
        txtPenyerta.ResetText()
        chkMotorikTAK.Checked = False
        chkMotorikKeTerungkap.Checked = False
        chkMotorikKeDuduk.Checked = False
        chkMotorikMerayap.Checked = False
        chkMotorikMerangkak.Checked = False
        chkMotorikKeBerdiri.Checked = False
        chkMotorikRambatan.Checked = False
        chkMotorikJalan.Checked = False
        chkMotorikBicara.Checked = False
        TextEdit6.ResetText()

        chkNutrisiNormal.Checked = False
        chkNutrisiBerlebih.Checked = False
        chkNutrisiKurang.Checked = False
        chkOrientasiNormal.Checked = False
        chkOrientasiBerlebih.Checked = False
        chkOrientasiKurang.Checked = False
        chkPostureScoliosis.Checked = False
        chkPostureKurus.Checked = False
        chkPostureDeformitas.Checked = False
        chkPostureTAK.Checked = False
        chkBABBAKBisaKontrol.Checked = False
        chkBABBAKTBisaKontrol.Checked = False
        chkBABBAKTTAK.Checked = False
        chkPsikosialBermain.Checked = False
        chkPsikosialTidak.Checked = False

        txtPemahamanLisan.ResetText()
        txtPengujianLisan.ResetText()
        txtBibir.ResetText()
        txtLidah.ResetText()
        txtLangitLangit.ResetText()
        txtRahang.ResetText()
        txtPipi.ResetText()
        txtPersepsiBunyi.ResetText()
        txtArtikulasi.ResetText()
        txtPangjangKata.ResetText()
        txtReseptif.ResetText()
        txtFonasi.ResetText()
        txtResonasi.ResetText()
        txtMakan.ResetText()
        txtMinum.ResetText()
        txtNeurologi.ResetText()
        txtTHT.ResetText()
        txtPsikologi.ResetText()
        txtDiagnosa.ResetText()
        txtProgram.ResetText()
        txtEvaluasi.ResetText()

        deDATE.DateTime = Now
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RJ_04.GetData(txtNoRegister.Text)

            With ds
                
                chkRujukanYa.Checked = .RUJUKAN
                chkRS.Checked = .DARIRSBIT
                txtRS.Text = .DARIRS
                chkPuskesmas.Checked = .DARIPUSKESMASBIT
                txtPuskesmas.Text = .DARIPUSKESMAS
                chkDR.Checked = .DARIDRBIT
                txtDR.Text = .DARIDR
                chkLainnya.Checked = .DARILAINNYABIT
                txtLainnya.Text = .DARILAINNYA
                txtDXMedis.Text = .DXMEDIS

                chkRiwayatKehamilanCukupBulan.Checked = .RIWAYATKEHAMILAN_CUKUPBULAN
                chkRiwayatKehamilanPrematur.Checked = .RIWAYATKEHAMILAN_PREMATUR
                chkRiwayatKehamilanTrauma.Checked = .RIWAYATKEHAMILAN_TRAUMA
                chkRiwayatKehamilanTokso.Checked = .RIWAYATKEHAMILAN_TOKSO
                chkRiwayatKehamilanTAK.Checked = .RIWAYATKEHAMILAN_TAK
                chkRiwayatPersalinanNormal.Checked = .RIWAYATPERSALINAN_NORMAL
                chkRiwayatPersalinanSC.Checked = .RIWAYATPERSALINAN_SC
                chkRiwayatPersalinanKetubanPecahDini.Checked = .RIWAYATPERSALINAN_KETUBANPECAHDINI
                chkRiwayatPostNatalKejang.Checked = .RIWAYATPOSTNATAL_KEJANG
                chkRiwayatPostNatalAfiksia.Checked = .RIWAYATPOSTNATAL_ASFIKSIA
                chkRiwayatPostNatalKuning.Checked = .RIWAYATPOSTNATAL_KUNING
                chkRiwayatPostNatalObat.Checked = .RIWAYATPOSTNATAL_OBATOBATAN
                chkRiwayatPostNatalTAK.Checked = .RIWAYATPOSTNATAL_TAK

                txtApgar.Text = .RIWAYATANAK_APGAR
                txtBeratBadanLahir.Text = .RIWAYATANAK_BERATBADANLAHIR
                txtPanjangLahir.Text = .RIWAYATANAK_PANJANGLAHIR
                txtAnakKe.Text = .RIWAYATANAK_ANAKKE
                chkPenyertaTAK.Checked = .RIWAYATPENYAKITPENYERTA_TAK
                txtPenyerta.Text = .RIWAYATPENYAKITPENYERTA_LAINNYA
                chkMotorikTAK.Checked = .RIWAYATPERKEMBANGANMOTORIK_TAK
                chkMotorikKeTerungkap.Checked = .RIWAYATPERKEMBANGANMOTORIK_KETENGKURAP
                chkMotorikKeDuduk.Checked = .RIWAYATPERKEMBANGANMOTORIK_KEDUDUK
                chkMotorikMerayap.Checked = .RIWAYATPERKEMBANGANMOTORIK_MERANGKAP
                chkMotorikMerangkak.Checked = .RIWAYATPERKEMBANGANMOTORIK_MERANGKAP
                chkMotorikKeBerdiri.Checked = .RIWAYATPERKEMBANGANMOTORIK_KEBERDIRI
                chkMotorikRambatan.Checked = .RIWAYATPERKEMBANGANMOTORIK_RAMBATAN
                chkMotorikJalan.Checked = .RIWAYATPERKEMBANGANMOTORIK_JALAN
                chkMotorikBicara.Checked = .RIWAYATPERKEMBANGANMOTORIK_BICARA
                TextEdit6.Text = .HARAPANORANGTUA

                chkNutrisiNormal.Checked = .NUTRISI_NORMAL
                chkNutrisiBerlebih.Checked = .NUTRISI_BERLEBIH
                chkNutrisiKurang.Checked = .NUTRISI_KURANG
                chkOrientasiNormal.Checked = .ORIENTASIRUANG_NORMAL
                chkOrientasiBerlebih.Checked = .ORIENTASIRUANG_BERLEBIH
                chkOrientasiKurang.Checked = .ORIENTASIRUANG_KURANG
                chkPostureScoliosis.Checked = .KEADAANFISIKPOSTURE_SCOLIOSIS
                chkPostureKurus.Checked = .KEADAANFISIKPOSTURE_KURUS
                chkPostureDeformitas.Checked = .KEADAANFISIKPOSTURE_DEFORMITAS
                chkPostureTAK.Checked = .KEADAANFISIKPOSTURE_TAK
                chkBABBAKBisaKontrol.Checked = .BABBAK_BISAKONTROL
                chkBABBAKTBisaKontrol.Checked = .BABBAK_TIDAKBISAKONTROL
                chkBABBAKTTAK.Checked = .BABBAK_TAK
                chkPsikosialBermain.Checked = .KEADAANPSIKOSIAL_BISABERMAIN
                chkPsikosialTidak.Checked = .KEADAANPSIKOSIAL_TIDAKBISABERMAIN

                txtPemahamanLisan.Text = .BAHASARESEPTIF
                txtPengujianLisan.Text = .BAHASAEKSPRESIF
                txtBibir.Text = .WICARAMOTORIKMULUT_BIBIR
                txtLidah.Text = .WICARAMOTORIKMULUT_LIDAH
                txtLangitLangit.Text = .WICARAMOTORIKMULUT_LANGITLANGIT
                txtRahang.Text = .WICARAMOTORIKMULUT_RAHANG
                txtPipi.Text = .WICARAMOTORIKMULUT_PIPI
                txtPersepsiBunyi.Text = .WICARAMOTORIKMULUT_PERSEPSIBUNYI
                txtArtikulasi.Text = .WICARAMOTORIKMULUT_ARTIKULASI
                txtPangjangKata.Text = .WICARAMOTORIKMULUT_PANJANGKATA
                txtReseptif.Text = .SUARA_KEMAMPUANRESEPTIF
                txtFonasi.Text = .SUARA_KEMAMPUANFONASI
                txtResonasi.Text = .SUARA_KEMAMPUANRESONASI
                txtMakan.Text = .MAKANDANMINUM_MAKAN
                txtMinum.Text = .MAKANDANMINUM_MINUM
                txtNeurologi.Text = .KETERANGANAHLI_NEOROLOGI
                txtTHT.Text = .KETERANGANAHLI_THT
                txtPsikologi.Text = .KETERANGANAHLI_PSIKOLOG
                txtDiagnosa.Text = .DIAGNOSA
                txtProgram.Text = .PROGRAM
                txtEvaluasi.Text = .EVALUASI

                deDATE.DateTime = .DATE
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
            '    MsgBox("Dibutuhkan Petugas Triage", MsgBoxStyle.Exclamation, Me.Text)
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
            Dim ds = oS_DIGITAL_RJ_04.GetStructureHeader
            With ds
                .KDKUNJUNGAN = txtNoRegister.Text
                Try
                    .DATECREATED = oS_DIGITAL_RJ_04.GetData(txtNoRegister.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = now
                End Try
                .DATEUPDATED = now
                .DATE = deDATE.DateTime

                Dim oPendaftaran As New Identitas.clsIdentitasPasien
                Dim dsPendaftaran = oPendaftaran.GetData(txtNoRegister.Text)

                .RUJUKAN = chkRujukanYa.Checked
                .DARIRSBIT = chkRS.Checked
                .DARIRS = txtRS.Text
                .DARIPUSKESMASBIT = chkPuskesmas.Checked
                .DARIPUSKESMAS = txtPuskesmas.Text
                .DARIDRBIT = chkDR.Checked
                .DARIDR = txtDR.Text
                .DARILAINNYABIT = chkLainnya.Checked
                .DARILAINNYA = txtLainnya.Text
                .DXMEDIS = txtDXMedis.Text

                .RIWAYATKEHAMILAN_CUKUPBULAN = chkRiwayatKehamilanCukupBulan.Checked
                .RIWAYATKEHAMILAN_PREMATUR = chkRiwayatKehamilanPrematur.Checked
                .RIWAYATKEHAMILAN_TRAUMA = chkRiwayatKehamilanTrauma.Checked
                .RIWAYATKEHAMILAN_TOKSO = chkRiwayatKehamilanTokso.Checked
                .RIWAYATKEHAMILAN_TAK = chkRiwayatKehamilanTAK.Checked
                .RIWAYATPERSALINAN_NORMAL = chkRiwayatPersalinanNormal.Checked
                .RIWAYATPERSALINAN_SC = chkRiwayatPersalinanSC.Checked
                .RIWAYATPERSALINAN_KETUBANPECAHDINI = chkRiwayatPersalinanKetubanPecahDini.Checked
                .RIWAYATPOSTNATAL_KEJANG = chkRiwayatPostNatalKejang.Checked
                .RIWAYATPOSTNATAL_ASFIKSIA = chkRiwayatPostNatalAfiksia.Checked
                .RIWAYATPOSTNATAL_KUNING = chkRiwayatPostNatalKuning.Checked
                .RIWAYATPOSTNATAL_OBATOBATAN = chkRiwayatPostNatalObat.Checked 
                .RIWAYATPOSTNATAL_TAK = chkRiwayatPostNatalTAK.Checked

                .RIWAYATANAK_APGAR = txtApgar.Text
                .RIWAYATANAK_BERATBADANLAHIR = txtBeratBadanLahir.Text
                .RIWAYATANAK_PANJANGLAHIR = txtPanjangLahir.Text
                .RIWAYATANAK_ANAKKE = txtAnakKe.Text
                .RIWAYATPENYAKITPENYERTA_TAK = chkPenyertaTAK.Checked
                .RIWAYATPENYAKITPENYERTA_LAINNYA = txtPenyerta.Text
                .RIWAYATPERKEMBANGANMOTORIK_TAK = chkMotorikTAK.Checked
                .RIWAYATPERKEMBANGANMOTORIK_KETENGKURAP = chkMotorikKeTerungkap.Checked
                .RIWAYATPERKEMBANGANMOTORIK_KEDUDUK = chkMotorikKeDuduk.Checked
                .RIWAYATPERKEMBANGANMOTORIK_MERANGKAP = chkMotorikMerayap.Checked
                .RIWAYATPERKEMBANGANMOTORIK_MERANGKAP = chkMotorikMerangkak.Checked
                .RIWAYATPERKEMBANGANMOTORIK_KEBERDIRI = chkMotorikKeBerdiri.Checked
                .RIWAYATPERKEMBANGANMOTORIK_RAMBATAN = chkMotorikRambatan.Checked
                .RIWAYATPERKEMBANGANMOTORIK_JALAN = chkMotorikJalan.Checked
                .RIWAYATPERKEMBANGANMOTORIK_BICARA = chkMotorikBicara.Checked
                .HARAPANORANGTUA = TextEdit6.Text

                .NUTRISI_NORMAL = chkNutrisiNormal.Checked
                .NUTRISI_BERLEBIH = chkNutrisiBerlebih.Checked
                .NUTRISI_KURANG = chkNutrisiKurang.Checked
                .ORIENTASIRUANG_NORMAL = chkOrientasiNormal.Checked
                .ORIENTASIRUANG_BERLEBIH = chkOrientasiBerlebih.Checked
                .ORIENTASIRUANG_KURANG = chkOrientasiKurang.Checked
                .KEADAANFISIKPOSTURE_SCOLIOSIS = chkPostureScoliosis.Checked
                .KEADAANFISIKPOSTURE_KURUS = chkPostureKurus.Checked
                .KEADAANFISIKPOSTURE_DEFORMITAS = chkPostureDeformitas.Checked
                .KEADAANFISIKPOSTURE_TAK = chkPostureTAK.Checked
                .BABBAK_BISAKONTROL = chkBABBAKBisaKontrol.Checked
                .BABBAK_TIDAKBISAKONTROL = chkBABBAKTBisaKontrol.Checked
                .BABBAK_TAK = chkBABBAKTTAK.Checked
                .KEADAANPSIKOSIAL_BISABERMAIN = chkPsikosialBermain.Checked
                .KEADAANPSIKOSIAL_TIDAKBISABERMAIN = chkPsikosialTidak.Checked

                .BAHASARESEPTIF = txtPemahamanLisan.Text
                .BAHASAEKSPRESIF = txtPengujianLisan.Text
                .WICARAMOTORIKMULUT_BIBIR = txtBibir.Text
                .WICARAMOTORIKMULUT_LIDAH = txtLidah.Text
                .WICARAMOTORIKMULUT_LANGITLANGIT = txtLangitLangit.Text
                .WICARAMOTORIKMULUT_RAHANG = txtRahang.Text
                .WICARAMOTORIKMULUT_PIPI = txtPipi.Text
                .WICARAMOTORIKMULUT_PERSEPSIBUNYI = txtPersepsiBunyi.Text
                .WICARAMOTORIKMULUT_ARTIKULASI = txtArtikulasi.Text
                .WICARAMOTORIKMULUT_PANJANGKATA = txtPangjangKata.Text
                .SUARA_KEMAMPUANRESEPTIF = txtReseptif.Text
                .SUARA_KEMAMPUANFONASI = txtFonasi.Text
                .SUARA_KEMAMPUANRESONASI = txtResonasi.Text
                .MAKANDANMINUM_MAKAN = txtMakan.Text
                .MAKANDANMINUM_MINUM = txtMinum.Text
                .KETERANGANAHLI_NEOROLOGI = txtNeurologi.Text
                .KETERANGANAHLI_THT = txtTHT.Text
                .KETERANGANAHLI_PSIKOLOG = txtPsikologi.Text
                .DIAGNOSA = txtDiagnosa.Text
                .PROGRAM = txtProgram.Text

                .EVALUASI = txtEvaluasi.Text


                .DOKTER_KODE = dsPendaftaran.KDDOKTER
                .DOKTER_NAMEDISPLAY = dsPendaftaran.DOKTER
                Try
                    .CETAK = oS_DIGITAL_RJ_04.GetData(txtNoRegister.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                Try
                    If sIsOtority = True Then
                        .KDUSER = oS_DIGITAL_RJ_04.GetData(txtNoRegister.Text).KDUSER
                        .KDUSER_SIGNATURE = oS_DIGITAL_RJ_04.GetData(txtNoRegister.Text).KDUSER_SIGNATURE
                    Else
                        .KDUSER = sUserID
                        .KDUSER_SIGNATURE = sUserSIGNATURE
                    End If
                Catch ex As Exception
                    .KDUSER = sUserID
                    .KDUSER_SIGNATURE = sUserSIGNATURE
                End Try
            End With
            oFormMode = 1
            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_RJ_04.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_RJ_04.UpdateData(ds)
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

    Private Sub frmEMedrekRJ_04_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
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