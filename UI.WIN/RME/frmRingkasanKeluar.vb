Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq
Imports System.Data.SqlClient

Public Class frmRingkasanKeluar
#Region "Declaration"
    Private oRingkasanKeluar As New EMedrek.clsRingkasanKeluar
    Private oRingkasanKeluarTemplate As New EMedrek.clsTemplateRingkasanKeluar
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private sKDIDENTITAS As Integer
    Private isLoad As Boolean = False
    Private sKDCUSTOMER As String = String.Empty
    Private sNAMAPASIEN As String = String.Empty
    Private sJENISKELAMIN As String = String.Empty
    Private sTANGGALLAHIR As DateTime = Now
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal NoId As String, ByVal KDIDENTITAS As Integer)
        sPoli = String.Empty
        oFormMode = FormMode
        sNoId = NoId
        sKDIDENTITAS = KDIDENTITAS

        Dim oAdmisi As New Admission.clsPendaftaran

        Dim dsAdmisi = oAdmisi.GetData(sNoId)
        If dsAdmisi IsNot Nothing Then
            deTANGGALMASUK.DateTime = dsAdmisi.DATE
            deTANGGALPULANG.DateTime = dsAdmisi.DATE
            grdDPJP.Text = dsAdmisi.KDDOCTOR
            grdKDDEPARTMENT.Text = dsAdmisi.KDDEPARTMENT
            sKDCUSTOMER = dsAdmisi.KDCUSTOMER
            sNAMAPASIEN = dsAdmisi.M_CUSTOMER.NAME_DISPLAY
            sJENISKELAMIN = IIf(dsAdmisi.M_CUSTOMER.KDJENISKELAMIN = 1, "Laki-laki", "Perempuan")
            sTANGGALLAHIR = dsAdmisi.M_CUSTOMER.TANGGALLAHIR
            deTANGGALMASUK.DateTime = dsAdmisi.DATE
            deTANGGALPULANG.DateTime = dsAdmisi.DATE
            grdDPJP.Text = dsAdmisi.KDDOCTOR
            grdKDDEPARTMENT.Text = dsAdmisi.KDDEPARTMENT
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "Template"

            btnSaveClose.Caption = "Simpan"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'sCode = txtMEMO.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadDPJP()
        fn_LoadDepartment()

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
        Dim oUpdatePulang As New Admission.clsUpdate_Tanggal_Pulang

        Dim dsUpdatePulang = oUpdatePulang.GetDatabyKD(sNoId)
        If dsUpdatePulang Is Nothing Then
            Dim frmUpdate_Tanggal_Pulang As New frmUpdate_Tanggal_Pulang
            Try
                Dim carapulang As Integer = 0
                If CheckEdit26.Checked = True Then
                    carapulang = 0
                End If
                If CheckEdit27.Checked = True Then
                    carapulang = 1
                End If
                If CheckEdit28.Checked = True Then
                    carapulang = 2
                End If
                If CheckEdit29.Checked = True Then
                    carapulang = 3
                End If
                If CheckEdit30.Checked = True Then
                    carapulang = 4
                End If
                frmUpdate_Tanggal_Pulang.LoadMe(FORM_MODE.FORM_MODE_ADD)
                frmUpdate_Tanggal_Pulang.LoadMeOtomatis(True, sNoId, deTANGGALPULANG.DateTime, carapulang)
                frmUpdate_Tanggal_Pulang.ShowDialog(Me)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmUpdate_Tanggal_Pulang Is Nothing Then frmUpdate_Tanggal_Pulang.Dispose()
                frmUpdate_Tanggal_Pulang = Nothing

                If sCaraPulang <> "" Then
                    CheckEdit26.Checked = False
                    CheckEdit27.Checked = False
                    CheckEdit28.Checked = False
                    CheckEdit29.Checked = False
                    CheckEdit30.Checked = False
                    chkKEADAANSAATKELUAR_3.Checked = False
                    chkKEADAANSAATKELUAR_5.Checked = False

                    If sCaraPulang = "Atas Persetujuan Dokter" Then
                        CheckEdit26.Checked = True
                        CheckEdit27.Checked = False
                        CheckEdit28.Checked = False
                        CheckEdit29.Checked = False
                        CheckEdit30.Checked = False
                    ElseIf sCaraPulang = "Atas Permintaan Sendiri" Then
                        CheckEdit26.Checked = False
                        CheckEdit27.Checked = False
                        CheckEdit28.Checked = True
                        CheckEdit29.Checked = False
                        CheckEdit30.Checked = False
                    ElseIf sCaraPulang = "Dirujuk" Then
                        CheckEdit26.Checked = False
                        CheckEdit27.Checked = False
                        CheckEdit28.Checked = False
                        CheckEdit29.Checked = False
                        CheckEdit30.Checked = True
                    ElseIf sCaraPulang = "Lain-lain" Then
                        CheckEdit26.Checked = False
                        CheckEdit27.Checked = False
                        CheckEdit28.Checked = False
                        CheckEdit29.Checked = True
                        CheckEdit30.Checked = False
                    ElseIf sCaraPulang = "Meninggal <48 Jam" Then
                        chkKEADAANSAATKELUAR_3.Checked = True
                        chkKEADAANSAATKELUAR_5.Checked = False
                    ElseIf sCaraPulang = "Meninggal > 48 Jam" Then
                        chkKEADAANSAATKELUAR_3.Checked = False
                        chkKEADAANSAATKELUAR_5.Checked = True
                    Else
                        CheckEdit26.Checked = False
                        CheckEdit27.Checked = False
                        CheckEdit28.Checked = False
                        CheckEdit29.Checked = False
                        CheckEdit30.Checked = False
                    End If
                End If

                deTANGGALPULANG.DateTime = sTanggalPulang
            End Try
        Else
            Dim frmUpdate_Tanggal_Pulang As New frmUpdate_Tanggal_Pulang
            Try
                frmUpdate_Tanggal_Pulang.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsUpdatePulang.KDUPDATE_TANGGAL_PULANG)
                frmUpdate_Tanggal_Pulang.ShowDialog(Me)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmUpdate_Tanggal_Pulang Is Nothing Then frmUpdate_Tanggal_Pulang.Dispose()
                frmUpdate_Tanggal_Pulang = Nothing

                If sCaraPulang <> "" Then
                    CheckEdit26.Checked = False
                    CheckEdit27.Checked = False
                    CheckEdit28.Checked = False
                    CheckEdit29.Checked = False
                    CheckEdit30.Checked = False
                    chkKEADAANSAATKELUAR_3.Checked = False
                    chkKEADAANSAATKELUAR_5.Checked = False

                    If sCaraPulang = "Atas Persetujuan Dokter" Then
                        CheckEdit26.Checked = True
                        CheckEdit27.Checked = False
                        CheckEdit28.Checked = False
                        CheckEdit29.Checked = False
                        CheckEdit30.Checked = False
                    ElseIf sCaraPulang = "Atas Permintaan Sendiri" Then
                        CheckEdit26.Checked = False
                        CheckEdit27.Checked = False
                        CheckEdit28.Checked = True
                        CheckEdit29.Checked = False
                        CheckEdit30.Checked = False
                    ElseIf sCaraPulang = "Dirujuk" Then
                        CheckEdit26.Checked = False
                        CheckEdit27.Checked = False
                        CheckEdit28.Checked = False
                        CheckEdit29.Checked = False
                        CheckEdit30.Checked = True
                    ElseIf sCaraPulang = "Lain-lain" Then
                        CheckEdit26.Checked = False
                        CheckEdit27.Checked = False
                        CheckEdit28.Checked = False
                        CheckEdit29.Checked = True
                        CheckEdit30.Checked = False
                    ElseIf sCaraPulang = "Meninggal <48 Jam" Then
                        chkKEADAANSAATKELUAR_3.Checked = True
                        chkKEADAANSAATKELUAR_5.Checked = False
                    ElseIf sCaraPulang = "Meninggal > 48 Jam" Then
                        chkKEADAANSAATKELUAR_3.Checked = False
                        chkKEADAANSAATKELUAR_5.Checked = True
                    Else
                        CheckEdit26.Checked = False
                        CheckEdit27.Checked = False
                        CheckEdit28.Checked = False
                        CheckEdit29.Checked = False
                        CheckEdit30.Checked = False
                    End If
                End If

                deTANGGALPULANG.DateTime = sTanggalPulang
            End Try
        End If
    End Sub
    Private Sub fn_EmptyMe()
        txtKeluhanUtama.ResetText()
        txtAnamnesa.ResetText()
        txtKomorbiditasLain.ResetText()
        txtPemeriksaanFisik.ResetText()
        txtHasilPemeriksaan.ResetText()
        txtIndikasiRawat.ResetText()
        txtTerapi.ResetText()
        txtKeadaanUmum.ResetText()
        txtKesadaran.ResetText()
        txtGCS.ResetText()
        txtNadi.ResetText()
        txtSuhu.ResetText()
        txtSistole.Text = "0"
        txtDiastole.Text = "0"
        txtFrekuensi.ResetText()
        txtCatatanPenting.ResetText()
        txtSebabKematian.ResetText()
        chkKEADAANSAATKELUAR_1.Checked = False
        chkKEADAANSAATKELUAR_2.Checked = False
        chkKEADAANSAATKELUAR_3.Checked = False
        chkKEADAANSAATKELUAR_4.Checked = False
        chkKEADAANSAATKELUAR_5.Checked = False
        CheckEdit26.Checked = False
        CheckEdit27.Checked = False
        CheckEdit28.Checked = False
        CheckEdit29.Checked = False
        CheckEdit30.Checked = False
        CheckEdit21.Checked = False
        CheckEdit22.Checked = False
        CheckEdit23.Checked = False
        CheckEdit24.Checked = False
        ComboBoxEdit1.Text = "0"
        DateEdit4.DateTime = Now
        TextBox23.ResetText()
        TextBox24.ResetText()
        CheckEdit25.Checked = False
        CheckEdit31.Checked = False
        CheckEdit32.Checked = False
        txtEdukasidanIntruksi.ResetText()
        txtSaturasi.ResetText()
        'TextBox2.Text = "-"
        txtObatPulang.ResetText()
        txtBeratBadan.Text = "0"

        'fn_CopyCPPTAkhirdiResume(lblKODECPPT.Text)
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oRingkasanKeluar.GetData(sNoId)

            With ds
                grdDokter2.Text = .DOKTERKEDUA
                grdDokter3.Text = .DOKTERKETIGA
                grdDokter4.Text = .DOKTERKEEMPAT
                grdDokter5.Text = .DOKTERKELIMA
                deTANGGALMASUK.DateTime = .TANGGALMASUK
                deTANGGALPULANG.DateTime = .TANGGALPULANG
                txtKeluhanUtama.Text = .KELUHANUTAMA
                txtAnamnesa.Text = .ANAMNESA
                txtKomorbiditasLain.Text = .KOMORBIDITASLAIN
                txtPemeriksaanFisik.Text = .PEMERIKSAANFISIK
                txtHasilPemeriksaan.Text = .HASILPEMERIKSAAN
                txtIndikasiRawat.Text = .INDIKASIRAWAT
                txtTerapi.Text = .TERAPI
                txtKeadaanUmum.Text = .KEADAANUMUM
                txtKesadaran.Text = .KESADARAN
                txtGCS.Text = .GCS
                txtNadi.Text = .TEKANANDARAH
                txtSuhu.Text = .SUHU
                txtSistole.Text = .NADI_1
                txtDiastole.Text = .NADI_2
                txtFrekuensi.Text = .FREKUENSINAPAS
                txtCatatanPenting.Text = .CATATANPENTING
                txtSebabKematian.Text = .SEBABKEMATIAN
                chkKEADAANSAATKELUAR_1.Checked = .KEADAANKELUARRS_1
                chkKEADAANSAATKELUAR_2.Checked = .KEADAANKELUARRS_2
                chkKEADAANSAATKELUAR_3.Checked = .KEADAANKELUARRS_3
                chkKEADAANSAATKELUAR_4.Checked = .KEADAANKELUARRS_4
                chkKEADAANSAATKELUAR_5.Checked = .KEADAANKELUARRS_5
                CheckEdit26.Checked = .CARAKELUAR_1
                CheckEdit27.Checked = .CARAKELUAR_2
                CheckEdit28.Checked = .CARAKELUAR_3
                CheckEdit29.Checked = .CARAKELUAR_4
                CheckEdit30.Checked = .CARAKELUAR_5
                CheckEdit21.Checked = .KONTROL_1
                CheckEdit22.Checked = .KONTROL_2
                CheckEdit23.Checked = .KONTROL_3
                CheckEdit24.Checked = .KONTROL_4
                ComboBoxEdit1.Text = .KONTROL_HARI
                DateEdit4.DateTime = .TANGGALKONTROL
                TextBox23.Text = .POLIKLINIK
                TextBox24.Text = .INSTITUSI
                CheckEdit25.Checked = .OBATPULANG_1
                CheckEdit31.Checked = .OBATPULANG_2
                CheckEdit32.Checked = .OBATPULANG_3
                txtEdukasidanIntruksi.Text = .EDUKASI
                txtObatPulang.Text = .OBATPULANG
                'TextBox1.Text = FormatNumber(.TARIF_GROUPER, 0)
                txtSaturasi.Text = .SATURASI
                'TextBox2.Text = .HASIL_GROUPER
                txtBeratBadan.Text = .BERATBADAN
                'grdKDDEPARTMENT.Text = .HASIL_GROUPER
                'grdDPJP.Text = .KDDOCTOR

                BindingSourceDiagnosa.DataSource = oRingkasanKeluar.GetDataDetail_1(.KDREG).OrderBy(Function(x) x.SEQ).ToList()
                grdDiagnosa.DataSource = BindingSourceDiagnosa

                BindingSourceProsedur.DataSource = oRingkasanKeluar.GetDataDetail_2(.KDREG).OrderBy(Function(x) x.SEQ).ToList()
                grdPROSEDUR.DataSource = BindingSourceProsedur

            End With

        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = False

            If sNoId = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                'txtRegister.Focus()
                fn_Validate = False
                Exit Function
            End If

            'If sCasemix = False Then
            '    Dim Pesan As String = fn_Cek(2, "RESUME MEDIS", deTANGGALPULANG.DateTime)

            '    If Pesan <> "" Then
            '        MsgBox(Pesan, MsgBoxStyle.Information, Me.Text)
            '        fn_Validate = False
            '        Exit Function
            '    End If
            'End If
            'If lblTandaAsesmen.Text <> "Sudah Ada Catatan Medis" Then
            '    MsgBox("Catatan Awal Medis Belum dibuat", MsgBoxStyle.Exclamation, Me.Text)
            '    'txtRegister.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If
            If deTANGGALMASUK.DateTime > deTANGGALPULANG.DateTime Then
                MsgBox("Tanggal Keluar Lebih Kecil dari Tanggal Masuk", MsgBoxStyle.Exclamation, Me.Text)
                deTANGGALPULANG.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdDPJP.Text = "" Then
                MsgBox("DPJP Belum di Isi", MsgBoxStyle.Exclamation, Me.Text)
                grdDPJP.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDEPARTMENT.Text = "" Then
                MsgBox("Ruangan Belum di Isi", MsgBoxStyle.Exclamation, Me.Text)
                grdKDDEPARTMENT.Focus()
                fn_Validate = False
                Exit Function
            End If
            'If Finish = True Then
            '    If txtKeluhanUtama.Text = "" Then
            '        MsgBox("Dibutuhkan Keluhan Utama", MsgBoxStyle.Exclamation, Me.Text)
            '        txtKeluhanUtama.Focus()
            '        fn_Validate = False
            '        Exit Function
            '    End If
            '    If txtAnamnesa.Text = "" Then
            '        MsgBox("Dibutuhkan Anamnesa", MsgBoxStyle.Exclamation, Me.Text)
            '        txtAnamnesa.Focus()
            '        fn_Validate = False
            '        Exit Function
            '    End If
            '    If txtKomorbiditasLain.Text = "" Then
            '        MsgBox("Dibutuhkan Komorbiditas Lain", MsgBoxStyle.Exclamation, Me.Text)
            '        txtKomorbiditasLain.Focus()
            '        fn_Validate = False
            '        Exit Function
            '    End If
            '    If txtPemeriksaanFisik.Text = "" Then
            '        MsgBox("Dibutuhkan Pemeriksaan Fisik", MsgBoxStyle.Exclamation, Me.Text)
            '        txtPemeriksaanFisik.Focus()
            '        fn_Validate = False
            '        Exit Function
            '    End If
            '    If txtHasilPemeriksaan.Text = "" Then
            '        MsgBox("Dibutuhkan Hasil Pemeriksaan", MsgBoxStyle.Exclamation, Me.Text)
            '        txtHasilPemeriksaan.Focus()
            '        fn_Validate = False
            '        Exit Function
            '    End If
            '    If txtIndikasiRawat.Text = "" Then
            '        MsgBox("Dibutuhkan Indikasi Rawat", MsgBoxStyle.Exclamation, Me.Text)
            '        txtIndikasiRawat.Focus()
            '        fn_Validate = False
            '        Exit Function
            '    End If
            '    If txtTerapi.Text = "" Then
            '        MsgBox("Dibutuhkan Terapi", MsgBoxStyle.Exclamation, Me.Text)
            '        txtTerapi.Focus()
            '        fn_Validate = False
            '        Exit Function
            '    End If
            '    If txtKeadaanUmum.Text = "" Then
            '        MsgBox("Dibutuhkan Keadaan Umum", MsgBoxStyle.Exclamation, Me.Text)
            '        txtKeadaanUmum.Focus()
            '        fn_Validate = False
            '        Exit Function
            '    End If
            '    If txtKesadaran.Text = "" Then
            '        MsgBox("Dibutuhkan Kesadaran", MsgBoxStyle.Exclamation, Me.Text)
            '        txtKesadaran.Focus()
            '        fn_Validate = False
            '        Exit Function
            '    End If
            '    'If TextBox12.Text = "" Then
            '    '    MsgBox("Dibutuhkan GCS", MsgBoxStyle.Exclamation, Me.Text)
            '    '    TextBox12.Focus()
            '    '    fn_Validate = False
            '    '    Exit Function
            '    'End If
            '    If chkKEADAANSAATKELUAR_3.Checked = False Then
            '        If chkKEADAANSAATKELUAR_5.Checked = False Then
            '            If TextBox13.Text = "0" Then
            '                MsgBox("Dibutuhkan Tekanan Darah Sistole", MsgBoxStyle.Exclamation, Me.Text)
            '                TextBox13.Focus()
            '                fn_Validate = False
            '                Exit Function
            '            End If
            '            If TextBox14.Text = "0" Then
            '                MsgBox("Dibutuhkan Tekanan Darah Diastole", MsgBoxStyle.Exclamation, Me.Text)
            '                TextBox14.Focus()
            '                fn_Validate = False
            '                Exit Function
            '            End If
            '            If TextBox16.Text = "" Then
            '                MsgBox("Dibutuhkan Suhu", MsgBoxStyle.Exclamation, Me.Text)
            '                TextBox16.Focus()
            '                fn_Validate = False
            '                Exit Function
            '            End If
            '            If TextBox15.Text = "" Then
            '                MsgBox("Dibutuhkan Nadi", MsgBoxStyle.Exclamation, Me.Text)
            '                TextBox15.Focus()
            '                fn_Validate = False
            '                Exit Function
            '            End If
            '            If TextBox18.Text = "0" Then
            '                MsgBox("Dibutuhkan Frekuensi Napas", MsgBoxStyle.Exclamation, Me.Text)
            '                TextBox18.Focus()
            '                fn_Validate = False
            '                Exit Function
            '            End If
            '            'If txtCatatanPenting.Text = "" Then
            '            '    MsgBox("Dibutuhkan Catatan Penting", MsgBoxStyle.Exclamation, Me.Text)
            '            '    txtCatatanPenting.Focus()
            '            '    fn_Validate = False
            '            '    Exit Function
            '            'End If
            '        End If
            '    End If

            '    Dim cek As Boolean = False
            '    If CheckEdit26.Checked = True Then
            '        cek = True
            '    End If
            '    If CheckEdit27.Checked = True Then
            '        cek = True
            '    End If
            '    If CheckEdit28.Checked = True Then
            '        cek = True
            '    End If
            '    If CheckEdit29.Checked = True Then
            '        cek = True
            '    End If
            '    If CheckEdit30.Checked = True Then
            '        cek = True
            '    End If
            '    If chkKEADAANSAATKELUAR_3.Checked = True Then
            '        cek = True
            '    End If
            '    If chkKEADAANSAATKELUAR_5.Checked = True Then
            '        cek = True
            '    End If

            '    If cek = False Then
            '        MsgBox("Dibutuhkan Cara Keluar Pasien", MsgBoxStyle.Exclamation, Me.Text)
            '        fn_Validate = False
            '        Exit Function
            '    End If

            '    Dim CekDiagnosa As Boolean = False

            '    For i As Integer = 0 To grvDIAGNOSA.RowCount - 2
            '        CekDiagnosa = True
            '    Next

            '    If CekDiagnosa = False Then
            '        MsgBox("Dibutuhkan Diagnosa Pasien", MsgBoxStyle.Exclamation, Me.Text)
            '        fn_Validate = False
            '        Exit Function
            '    End If

            'End If

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Dim oUpdatePulang As New Admission.clsUpdate_Tanggal_Pulang

                Dim dsUpdatePulang = oUpdatePulang.GetDatabyKD(sNoId)
                If dsUpdatePulang Is Nothing Then
                    MsgBox("Belum Di pulangkan", MsgBoxStyle.Exclamation, Me.Text)
                    fn_Validate = False
                    Exit Function
                End If
            End If
            fn_Validate = True
        Catch oErr As Exception
            MsgBox("Validate Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****

            Dim ds = oRingkasanKeluar.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oRingkasanKeluar.GetData(sNoId).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDIDENTITAS = sKDIDENTITAS
                .KDREG = sNoId
                .KDCUSTOMER = sKDCUSTOMER
                .NAMAPASIEN = sNAMAPASIEN
                .JK = sJENISKELAMIN
                .TANGGALLAHIR = sTANGGALLAHIR
                .TANGGALMASUK = deTANGGALMASUK.DateTime
                .TANGGALPULANG = deTANGGALPULANG.DateTime
                .DOKTERUTAMA = grdDPJP.Text
                .DOKTERKEDUA = grdDokter2.Text
                .DOKTERKETIGA = grdDokter3.Text
                .DOKTERKEEMPAT = grdDokter4.Text
                .DOKTERKELIMA = grdDokter5.Text
                .KELUHANUTAMA = txtKeluhanUtama.Text
                .ANAMNESA = txtAnamnesa.Text
                .KOMORBIDITASLAIN = txtKomorbiditasLain.Text
                .PEMERIKSAANFISIK = txtPemeriksaanFisik.Text
                .HASILPEMERIKSAAN = txtHasilPemeriksaan.Text
                .INDIKASIRAWAT = txtIndikasiRawat.Text
                .TERAPI = txtTerapi.Text

                Dim listdiagnosa As New List(Of String)
                For i As Integer = 0 To grvDiagnosa.RowCount - 2
                    If grvDiagnosa.GetRowCellValue(i, colKATEGORI) = "Primer" Then
                        listdiagnosa.Add(grvDiagnosa.GetRowCellValue(i, colNAMADIAGNOSA))
                    End If
                Next
                .DIAGNOSAUTAMA = String.Join(", ", listdiagnosa.ToArray)

                Dim listdiagnosaPenyerta As New List(Of String)
                For i As Integer = 0 To grvDiagnosa.RowCount - 2
                    If grvDiagnosa.GetRowCellValue(i, colKATEGORI) <> "Primer" Then
                        listdiagnosaPenyerta.Add(i & "." & grvDiagnosa.GetRowCellValue(i, colNAMADIAGNOSA))
                    End If
                Next
                .DIAGNOSAPENYERTA = String.Join(vbCrLf, listdiagnosaPenyerta.ToArray)

                Dim listdiagnosaTindakan As New List(Of String)
                For i As Integer = 0 To grvPROSEDUR.RowCount - 2
                    listdiagnosaTindakan.Add(grvPROSEDUR.GetRowCellValue(i, colNAMA) & " " & grvPROSEDUR.GetRowCellValue(i, colKDPROSEDUR))
                Next

                .TINDAKAN = String.Join(", ", listdiagnosaTindakan.ToArray)
                .KEADAANUMUM = txtKeadaanUmum.Text
                .KESADARAN = txtKesadaran.Text
                .GCS = txtGCS.Text
                .TEKANANDARAH = txtNadi.Text
                .SUHU = txtSuhu.Text
                .NADI_1 = txtSistole.Text
                .NADI_2 = txtDiastole.Text
                .FREKUENSINAPAS = txtFrekuensi.Text
                .CATATANPENTING = txtCatatanPenting.Text
                .SEBABKEMATIAN = txtSebabKematian.Text
                .KEADAANKELUARRS_1 = chkKEADAANSAATKELUAR_1.Checked
                .KEADAANKELUARRS_2 = chkKEADAANSAATKELUAR_2.Checked
                .KEADAANKELUARRS_3 = chkKEADAANSAATKELUAR_3.Checked
                .KEADAANKELUARRS_4 = chkKEADAANSAATKELUAR_4.Checked
                .KEADAANKELUARRS_5 = chkKEADAANSAATKELUAR_5.Checked
                .CARAKELUAR_1 = CheckEdit26.Checked
                .CARAKELUAR_2 = CheckEdit27.Checked
                .CARAKELUAR_3 = CheckEdit28.Checked
                .CARAKELUAR_4 = CheckEdit29.Checked
                .CARAKELUAR_5 = CheckEdit30.Checked
                .KONTROL_1 = CheckEdit21.Checked
                .KONTROL_2 = CheckEdit22.Checked
                .KONTROL_3 = CheckEdit23.Checked
                .KONTROL_4 = CheckEdit24.Checked
                .KONTROL_HARI = CInt(ComboBoxEdit1.Text)
                .TANGGALKONTROL = DateEdit4.DateTime
                .POLIKLINIK = TextBox23.Text
                .INSTITUSI = TextBox24.Text
                .OBATPULANG_1 = CheckEdit25.Checked
                .OBATPULANG_2 = CheckEdit31.Checked
                .OBATPULANG_3 = CheckEdit32.Checked
                .OBATPULANG = txtObatPulang.Text
                .EDUKASI = txtEdukasidanIntruksi.Text
                .KDUSER = sUserID
                .RUANGRAWAT = grdKDDEPARTMENT.Text
                .TARIF_GROUPER = CDec(0)
                .SATURASI = txtSaturasi.Text
                .HASIL_GROUPER = grdKDDEPARTMENT.EditValue
                .KDDOCTOR = grdDPJP.EditValue
                .BERATBADAN = txtBeratBadan.Text
                Try
                    .ISDELETE = oRingkasanKeluar.GetData(sNoId).ISDELETE
                Catch ex As Exception
                    .ISDELETE = False
                End Try
                Try
                    .DATEDELETE = oRingkasanKeluar.GetData(sNoId).DATEDELETE
                Catch ex As Exception
                    .DATEDELETE = Now
                End Try
                Try
                    .USERDELETE = oRingkasanKeluar.GetData(sNoId).USERDELETE
                Catch ex As Exception
                    .USERDELETE = sUserID
                End Try
            End With

            'DIAGNOSA
            Dim arrDetailDiagnosa = oRingkasanKeluar.GetStructureDetail_1List
            For i As Integer = 0 To grvDiagnosa.RowCount - 2
                Dim dsDetail = oRingkasanKeluar.GetStructureDetail_1
                With dsDetail
                    .SEQ = i
                    .KDREG = ds.KDREG
                    .KATEGORI = IIf(String.IsNullOrEmpty(grvDiagnosa.GetRowCellValue(i, colKATEGORI)), "", grvDiagnosa.GetRowCellValue(i, colKATEGORI))
                    .NAMADIAGNOSA = IIf(String.IsNullOrEmpty(grvDiagnosa.GetRowCellValue(i, colNAMADIAGNOSA)), "", grvDiagnosa.GetRowCellValue(i, colNAMADIAGNOSA))
                    .KDDIAGNOSA = IIf(String.IsNullOrEmpty(grvDiagnosa.GetRowCellValue(i, colKDDIAGNOSA)), "", grvDiagnosa.GetRowCellValue(i, colKDDIAGNOSA))
                End With
                arrDetailDiagnosa.Add(dsDetail)
            Next

            'PROSEDUR
            Dim arrDetailProsedur = oRingkasanKeluar.GetStructureDetail_2List
            For i As Integer = 0 To grvPROSEDUR.RowCount - 2
                Dim dsDetail = oRingkasanKeluar.GetStructureDetail_2
                With dsDetail
                    .SEQ = i
                    .KDREG = ds.KDREG
                    .NAMA = IIf(String.IsNullOrEmpty(grvPROSEDUR.GetRowCellValue(i, colNAMA)), "", grvPROSEDUR.GetRowCellValue(i, colNAMA))
                    .KDPROSEDUR = IIf(String.IsNullOrEmpty(grvPROSEDUR.GetRowCellValue(i, colKDPROSEDUR)), "", grvPROSEDUR.GetRowCellValue(i, colKDPROSEDUR))
                End With
                arrDetailProsedur.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    sNoId = oRingkasanKeluar.InsertData(ds, arrDetailDiagnosa, arrDetailProsedur)
                    If sNoId = "" Then
                        fn_Save = False
                    Else
                        fn_Save = True
                    End If
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                Try
                    fn_Save = oRingkasanKeluar.UpdateData(ds, arrDetailDiagnosa, arrDetailProsedur)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

            'Try
            '    Dim oTerimaResep As New Reference.clsResepTerima
            '    Dim dsCekTerimaResep = oTerimaResep.GetData(lblRegister.Text & "-" & Now.ToString("ddMMyyyy"))

            '    If dsCekTerimaResep IsNot Nothing Then
            '        Dim dsTerimaResep = oTerimaResep.GetStructureHeader
            '        With dsTerimaResep
            '            .KDTERIMARESEP = dsCekTerimaResep.KDTERIMARESEP
            '            .DESCRIPTION = "PERBAIKAN"
            '        End With

            '        oTerimaResep.UpdateData(dsTerimaResep)
            '    End If
            'Catch ex As Exception
            '    MsgBox("Update Data Terima Resep: " & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            'End Try
        Catch oErr As Exception
            MsgBox("Simpan Data : " & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Sub fn_LoadDataRINGKASANKELUARTEMPLATE(ByVal KODE As String)
        Try
            ' ***** HEADER *****
            Dim ds = oRingkasanKeluarTemplate.GetData(KODE)

            With ds
                'deTANGGALMASUK.DateTime = .TANGGALMASUK
                'deTANGGALPULANG.DateTime = .TANGGALPULANG
                txtKeluhanUtama.Text = .KELUHANUTAMA
                txtAnamnesa.Text = .ANAMNESA
                txtKomorbiditasLain.Text = .KOMORBIDITASLAIN
                txtPemeriksaanFisik.Text = .PEMERIKSAANFISIK
                txtHasilPemeriksaan.Text = .HASILPEMERIKSAAN
                txtIndikasiRawat.Text = .INDIKASIRAWAT
                txtTerapi.Text = .TERAPI
                txtKeadaanUmum.Text = .KEADAANUMUM
                txtKesadaran.Text = .KESADARAN
                txtGCS.Text = .GCS
                txtNadi.Text = .TEKANANDARAH
                txtSuhu.Text = .SUHU
                txtSistole.Text = .NADI_1
                txtDiastole.Text = .NADI_2
                txtFrekuensi.Text = .FREKUENSINAPAS
                txtCatatanPenting.Text = .CATATANPENTING
                txtSebabKematian.Text = .SEBABKEMATIAN
                chkKEADAANSAATKELUAR_1.Checked = .KEADAANKELUARRS_1
                chkKEADAANSAATKELUAR_2.Checked = .KEADAANKELUARRS_2
                chkKEADAANSAATKELUAR_3.Checked = .KEADAANKELUARRS_3
                chkKEADAANSAATKELUAR_4.Checked = .KEADAANKELUARRS_4
                chkKEADAANSAATKELUAR_5.Checked = .KEADAANKELUARRS_5
                CheckEdit26.Checked = .CARAKELUAR_1
                CheckEdit27.Checked = .CARAKELUAR_2
                CheckEdit28.Checked = .CARAKELUAR_3
                CheckEdit29.Checked = .CARAKELUAR_4
                CheckEdit30.Checked = .CARAKELUAR_5
                CheckEdit21.Checked = .KONTROL_1
                CheckEdit22.Checked = .KONTROL_2
                CheckEdit23.Checked = .KONTROL_3
                CheckEdit24.Checked = .KONTROL_4
                ComboBoxEdit1.Text = .KONTROL_HARI
                DateEdit4.DateTime = .TANGGALKONTROL
                TextBox23.Text = .POLIKLINIK
                TextBox24.Text = .INSTITUSI
                CheckEdit25.Checked = .OBATPULANG_1
                CheckEdit31.Checked = .OBATPULANG_2
                CheckEdit32.Checked = .OBATPULANG_3
                txtEdukasidanIntruksi.Text = .EDUKASI
                txtObatPulang.Text = .OBATPULANG
                'TextBox1.Text = 0
                txtSaturasi.Text = .SATURASI
                'TextBox2.Text = .HASIL_GROUPER

                For Each xloop In oRingkasanKeluarTemplate.GetDataDetail_1(KODE)
                    grvDiagnosa.Focus()
                    grvDiagnosa.AddNewRow()
                    grvDiagnosa.SetFocusedRowCellValue(colKATEGORI, xloop.KATEGORI)
                    grvDiagnosa.SetFocusedRowCellValue(colKDDIAGNOSA, xloop.KDDIAGNOSA)
                    grvDiagnosa.SetFocusedRowCellValue(colNAMADIAGNOSA, xloop.NAMADIAGNOSA)
                    grvDiagnosa.UpdateCurrentRow()
                Next

                For Each xloop In oRingkasanKeluarTemplate.GetDataDetail_2(KODE)
                    grvPROSEDUR.Focus()
                    grvPROSEDUR.AddNewRow()
                    grvPROSEDUR.SetFocusedRowCellValue(colKDPROSEDUR, xloop.KDPROSEDUR)
                    grvPROSEDUR.SetFocusedRowCellValue(colNAMA, xloop.NAMA)
                    grvPROSEDUR.UpdateCurrentRow()
                Next

            End With

        Catch oErr As Exception
            MsgBox("Load List Data Template Ringkasan Keluar: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_SaveRINGKASANKELUARTemplate(ByVal kode As String) As Boolean
        Try
            ' ***** HEADER *****

            Dim ds = oRingkasanKeluarTemplate.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oRingkasanKeluar.GetData(kode).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .RUANGRAWAT = ""
                .KDTEMPLATE_RINGKASANKELUAR = kode
                .KDCUSTOMER = ""
                Try
                    .NAMAPASIEN = oRingkasanKeluarTemplate.GetData(kode).NAMAPASIEN
                Catch ex As Exception
                    .NAMAPASIEN = ""
                End Try
                .JK = ""
                .TANGGALLAHIR = Now
                .TANGGALMASUK = deTANGGALMASUK.DateTime
                .TANGGALPULANG = deTANGGALPULANG.DateTime
                .DOKTERUTAMA = ""
                .DOKTERKEDUA = ""
                .DOKTERKETIGA = ""
                .DOKTERKEEMPAT = ""
                .DOKTERKELIMA = ""
                .KELUHANUTAMA = txtKeluhanUtama.Text
                .ANAMNESA = txtAnamnesa.Text
                .KOMORBIDITASLAIN = txtKomorbiditasLain.Text
                .PEMERIKSAANFISIK = txtPemeriksaanFisik.Text
                .HASILPEMERIKSAAN = txtHasilPemeriksaan.Text
                .INDIKASIRAWAT = txtIndikasiRawat.Text
                .TERAPI = txtTerapi.Text

                Dim listdiagnosa As New List(Of String)
                For i As Integer = 0 To grvDiagnosa.RowCount - 2
                    If grvDiagnosa.GetRowCellValue(i, colKATEGORI) = "Primer" Then
                        listdiagnosa.Add(grvDiagnosa.GetRowCellValue(i, colNAMADIAGNOSA))
                    End If
                Next
                .DIAGNOSAUTAMA = String.Join(", ", listdiagnosa.ToArray)

                Dim listdiagnosaPenyerta As New List(Of String)
                For i As Integer = 0 To grvDiagnosa.RowCount - 2
                    If grvDiagnosa.GetRowCellValue(i, colKATEGORI) <> "Primer" Then
                        listdiagnosaPenyerta.Add(i & "." & grvDiagnosa.GetRowCellValue(i, colNAMADIAGNOSA))
                    End If
                Next
                .DIAGNOSAPENYERTA = String.Join(vbCrLf, listdiagnosaPenyerta.ToArray)

                Dim listdiagnosaTindakan As New List(Of String)
                For i As Integer = 0 To grvPROSEDUR.RowCount - 2
                    listdiagnosaTindakan.Add(grvPROSEDUR.GetRowCellValue(i, colNAMA) & " " & grvPROSEDUR.GetRowCellValue(i, colKDPROSEDUR))
                Next

                .TINDAKAN = String.Join(", ", listdiagnosaTindakan.ToArray)
                .KEADAANUMUM = txtKeadaanUmum.Text
                .KESADARAN = txtKesadaran.Text
                .GCS = txtGCS.Text
                .TEKANANDARAH = txtNadi.Text
                .SUHU = txtSuhu.Text
                .NADI_1 = txtSistole.Text
                .NADI_2 = txtDiastole.Text
                .FREKUENSINAPAS = txtFrekuensi.Text
                .CATATANPENTING = txtCatatanPenting.Text
                .SEBABKEMATIAN = txtSebabKematian.Text
                .KEADAANKELUARRS_1 = chkKEADAANSAATKELUAR_1.Checked
                .KEADAANKELUARRS_2 = chkKEADAANSAATKELUAR_2.Checked
                .KEADAANKELUARRS_3 = chkKEADAANSAATKELUAR_3.Checked
                .KEADAANKELUARRS_4 = chkKEADAANSAATKELUAR_4.Checked
                .KEADAANKELUARRS_5 = chkKEADAANSAATKELUAR_5.Checked
                .CARAKELUAR_1 = CheckEdit26.Checked
                .CARAKELUAR_2 = CheckEdit27.Checked
                .CARAKELUAR_3 = CheckEdit28.Checked
                .CARAKELUAR_4 = CheckEdit29.Checked
                .CARAKELUAR_5 = CheckEdit30.Checked
                .KONTROL_1 = CheckEdit21.Checked
                .KONTROL_2 = CheckEdit22.Checked
                .KONTROL_3 = CheckEdit23.Checked
                .KONTROL_4 = CheckEdit24.Checked
                .KONTROL_HARI = CInt(ComboBoxEdit1.Text)
                .TANGGALKONTROL = DateEdit4.DateTime
                .POLIKLINIK = TextBox23.Text
                .INSTITUSI = TextBox24.Text
                .OBATPULANG_1 = CheckEdit25.Checked
                .OBATPULANG_2 = CheckEdit31.Checked
                .OBATPULANG_3 = CheckEdit32.Checked
                'Dim listObatPulang As New List(Of String)
                .OBATPULANG = txtObatPulang.Text
                .EDUKASI = txtEdukasidanIntruksi.Text
                .KDUSER = sUserID
                .TARIF_GROUPER = CDec(0)
                .SATURASI = txtSaturasi.Text
                .HASIL_GROUPER = ""
            End With

            'DIAGNOSA
            Dim arrDetailDiagnosa = oRingkasanKeluarTemplate.GetStructureDetail_1List
            For i As Integer = 0 To grvDiagnosa.RowCount - 2
                Dim dsDetail = oRingkasanKeluarTemplate.GetStructureDetail_1
                With dsDetail
                    .SEQ = i
                    .KDTEMPLATE_RINGKASANKELUAR = ds.KDTEMPLATE_RINGKASANKELUAR
                    .KATEGORI = IIf(String.IsNullOrEmpty(grvDiagnosa.GetRowCellValue(i, colKATEGORI)), "", grvDiagnosa.GetRowCellValue(i, colKATEGORI))
                    .NAMADIAGNOSA = IIf(String.IsNullOrEmpty(grvDiagnosa.GetRowCellValue(i, colNAMADIAGNOSA)), "", grvDiagnosa.GetRowCellValue(i, colNAMADIAGNOSA))
                    .KDDIAGNOSA = IIf(String.IsNullOrEmpty(grvDiagnosa.GetRowCellValue(i, colKDDIAGNOSA)), "", grvDiagnosa.GetRowCellValue(i, colKDDIAGNOSA))
                End With
                arrDetailDiagnosa.Add(dsDetail)
            Next

            'PROSEDUR
            Dim arrDetailProsedur = oRingkasanKeluarTemplate.GetStructureDetail_2List
            For i As Integer = 0 To grvPROSEDUR.RowCount - 2
                Dim dsDetail = oRingkasanKeluarTemplate.GetStructureDetail_2
                With dsDetail
                    .SEQ = i
                    .KDTEMPLATE_RINGKASANKELUAR = ds.KDTEMPLATE_RINGKASANKELUAR
                    .NAMA = IIf(String.IsNullOrEmpty(grvPROSEDUR.GetRowCellValue(i, colNAMA)), "", grvPROSEDUR.GetRowCellValue(i, colNAMA))
                    .KDPROSEDUR = IIf(String.IsNullOrEmpty(grvPROSEDUR.GetRowCellValue(i, colKDPROSEDUR)), "", grvPROSEDUR.GetRowCellValue(i, colKDPROSEDUR))
                End With
                arrDetailProsedur.Add(dsDetail)
            Next

            If kode = "" Then
                Try
                    kode = oRingkasanKeluarTemplate.InsertData(ds, arrDetailDiagnosa, arrDetailProsedur)
                    If kode = "" Then
                        fn_SaveRINGKASANKELUARTemplate = False
                    Else
                        fn_SaveRINGKASANKELUARTemplate = True
                    End If
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                Try
                    fn_SaveRINGKASANKELUARTemplate = oRingkasanKeluarTemplate.UpdateData(ds, arrDetailDiagnosa, arrDetailProsedur)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

        Catch oErr As Exception
            MsgBox("Simpan Data Ringkasan Keluar Template: " & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveRINGKASANKELUARTemplate = False
        End Try
    End Function
#End Region
#Region "Command Button"
    Private Sub CheckEdit26_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEdit26.CheckedChanged
        If CheckEdit26.Checked = True Then
            CheckEdit27.Checked = False
            CheckEdit28.Checked = False
            CheckEdit29.Checked = False
            CheckEdit30.Checked = False
        End If
    End Sub
    Private Sub CheckEdit27_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEdit27.CheckedChanged
        If CheckEdit27.Checked = True Then
            CheckEdit26.Checked = False
            CheckEdit28.Checked = False
            CheckEdit29.Checked = False
            CheckEdit30.Checked = False
        End If
    End Sub
    Private Sub CheckEdit28_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEdit28.CheckedChanged
        If CheckEdit28.Checked = True Then
            CheckEdit26.Checked = False
            CheckEdit27.Checked = False
            CheckEdit29.Checked = False
            CheckEdit30.Checked = False
        End If
    End Sub
    Private Sub CheckEdit29_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEdit29.CheckedChanged
        If CheckEdit29.Checked = True Then
            CheckEdit26.Checked = False
            CheckEdit27.Checked = False
            CheckEdit28.Checked = False
            CheckEdit30.Checked = False
        End If
    End Sub
    Private Sub CheckEdit30_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEdit30.CheckedChanged
        If CheckEdit30.Checked = True Then
            CheckEdit26.Checked = False
            CheckEdit27.Checked = False
            CheckEdit28.Checked = False
            CheckEdit29.Checked = False
        End If
    End Sub
    Private Sub CheckEdit21_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEdit21.CheckedChanged
        If CheckEdit21.Checked = True Then
            CheckEdit22.Checked = False
            CheckEdit23.Checked = False
            CheckEdit24.Checked = False
        End If

        Dim oSkd As New Admission.clsSKD
        Dim oPendaftaran As New Admission.clsPendaftaran

        Dim dsPendaftaran = oPendaftaran.GetData(sNoId)
        If dsPendaftaran IsNot Nothing Then
            Dim dsSKD = oSkd.GetDataPendaftaran(dsPendaftaran.KDPENDAFTARAN)

            If dsSKD IsNot Nothing Then
                Dim frmSKD As New frmSKD
                Try
                    frmSKD.fn_LoadNoPendaftaranPolidanDokter(0, dsPendaftaran.KDDEPARTMENT, dsPendaftaran.KDDOCTOR, dsPendaftaran.KDCUSTOMER, dsPendaftaran.KDPENDAFTARAN, "")
                    frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsSKD.KDSKD)
                    frmSKD.ShowDialog(Me)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                Finally
                    If Not frmSKD Is Nothing Then frmSKD.Dispose()
                    frmSKD = Nothing

                    If sPoli <> "" Then
                        TextBox23.Text = sPoli
                    End If
                End Try
            Else
                Dim frmSKD As New frmSKD
                Try
                    frmSKD.fn_LoadNoPendaftaranPolidanDokter(0, dsPendaftaran.KDDEPARTMENT, dsPendaftaran.KDDOCTOR, dsPendaftaran.KDCUSTOMER, dsPendaftaran.KDPENDAFTARAN, "")
                    frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
                    frmSKD.ShowDialog(Me)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                Finally
                    If Not frmSKD Is Nothing Then frmSKD.Dispose()
                    frmSKD = Nothing

                    If sPoli <> "" Then
                        TextBox23.Text = sPoli
                    End If
                End Try
            End If
        Else
            MsgBox("Maap Registrasi Tidak di Temukan", MsgBoxStyle.Exclamation, Me.Text)
        End If

        fn_HitungTanggal()

    End Sub
    Private Sub CheckEdit22_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEdit22.CheckedChanged
        If CheckEdit22.Checked = True Then
            CheckEdit21.Checked = False
            CheckEdit23.Checked = False
            CheckEdit24.Checked = False
        End If

        Dim oSkd As New Admission.clsSKD
        Dim oPendaftaran As New Admission.clsPendaftaran

        Dim dsPendaftaran = oPendaftaran.GetData(sNoId)
        If dsPendaftaran IsNot Nothing Then
            Dim dsSKD = oSkd.GetDataPendaftaran(dsPendaftaran.KDPENDAFTARAN)

            If dsSKD IsNot Nothing Then
                Dim frmSKD As New frmSKD
                Try
                    frmSKD.fn_LoadNoPendaftaranPolidanDokter(0, dsPendaftaran.KDDEPARTMENT, dsPendaftaran.KDDOCTOR, dsPendaftaran.KDCUSTOMER, dsPendaftaran.KDPENDAFTARAN, "")
                    frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsSKD.KDSKD)
                    frmSKD.ShowDialog(Me)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                Finally
                    If Not frmSKD Is Nothing Then frmSKD.Dispose()
                    frmSKD = Nothing

                    If sPoli <> "" Then
                        TextBox23.Text = sPoli
                    End If
                End Try
            Else
                Dim frmSKD As New frmSKD
                Try
                    frmSKD.fn_LoadNoPendaftaranPolidanDokter(0, dsPendaftaran.KDDEPARTMENT, dsPendaftaran.KDDOCTOR, dsPendaftaran.KDCUSTOMER, dsPendaftaran.KDPENDAFTARAN, "")
                    frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
                    frmSKD.ShowDialog(Me)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                Finally
                    If Not frmSKD Is Nothing Then frmSKD.Dispose()
                    frmSKD = Nothing

                    If sPoli <> "" Then
                        TextBox23.Text = sPoli
                    End If
                End Try
            End If
        Else
            MsgBox("Maap Registrasi Tidak di Temukan", MsgBoxStyle.Exclamation, Me.Text)
        End If

        fn_HitungTanggal()
    End Sub
    Private Sub CheckEdit23_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEdit23.CheckedChanged
        If CheckEdit23.Checked = True Then
            CheckEdit21.Checked = False
            CheckEdit22.Checked = False
            CheckEdit24.Checked = False
        End If

        Dim oSkd As New Admission.clsSKD
        Dim oPendaftaran As New Admission.clsPendaftaran

        Dim dsPendaftaran = oPendaftaran.GetData(sNoId)
        If dsPendaftaran IsNot Nothing Then
            Dim dsSKD = oSkd.GetDataPendaftaran(dsPendaftaran.KDPENDAFTARAN)

            If dsSKD IsNot Nothing Then
                Dim frmSKD As New frmSKD
                Try
                    frmSKD.fn_LoadNoPendaftaranPolidanDokter(0, dsPendaftaran.KDDEPARTMENT, dsPendaftaran.KDDOCTOR, dsPendaftaran.KDCUSTOMER, dsPendaftaran.KDPENDAFTARAN, "")
                    frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsSKD.KDSKD)
                    frmSKD.ShowDialog(Me)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                Finally
                    If Not frmSKD Is Nothing Then frmSKD.Dispose()
                    frmSKD = Nothing

                    If sPoli <> "" Then
                        TextBox23.Text = sPoli
                    End If
                End Try
            Else
                Dim frmSKD As New frmSKD
                Try
                    frmSKD.fn_LoadNoPendaftaranPolidanDokter(0, dsPendaftaran.KDDEPARTMENT, dsPendaftaran.KDDOCTOR, dsPendaftaran.KDCUSTOMER, dsPendaftaran.KDPENDAFTARAN, "")
                    frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
                    frmSKD.ShowDialog(Me)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                Finally
                    If Not frmSKD Is Nothing Then frmSKD.Dispose()
                    frmSKD = Nothing

                    If sPoli <> "" Then
                        TextBox23.Text = sPoli
                    End If
                End Try
            End If
        Else
            MsgBox("Maap Registrasi Tidak di Temukan", MsgBoxStyle.Exclamation, Me.Text)
        End If

        fn_HitungTanggal()
    End Sub
    Private Sub CheckEdit24_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEdit24.CheckedChanged
        If CheckEdit24.Checked = True Then
            CheckEdit21.Checked = False
            CheckEdit22.Checked = False
            CheckEdit23.Checked = False
        End If
        DateEdit4.DateTime = deTANGGALMASUK.DateTime
        'fn_HitungTanggal()
    End Sub

    Private Sub ComboBoxEdit1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxEdit1.SelectedIndexChanged
        fn_HitungTanggal()
    End Sub
    Private Sub fn_HitungTanggal()
        DateEdit4.DateTime = deTANGGALPULANG.DateTime

        If CheckEdit21.Checked = True Then
            DateEdit4.DateTime = deTANGGALPULANG.DateTime.AddDays(3)
        End If
        If CheckEdit22.Checked = True Then
            DateEdit4.DateTime = deTANGGALPULANG.DateTime.AddDays(ComboBoxEdit1.Text)
        End If
        If CheckEdit23.Checked = True Then
            DateEdit4.DateTime = deTANGGALPULANG.DateTime.AddDays(7)
        End If
    End Sub
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        'Select Case e.KeyCode
        '    Case Keys.F12
        '        btnClose_Click()
        '    Case Keys.F2
        '        If btnSaveNew.Enabled = True Then
        '            btnSaveNew_Click()
        '        End If
        '    Case Keys.F3
        '        If btnSaveClose.Enabled = True Then
        '            btnSaveClose_Click()
        '        End If
        'End Select
    End Sub
    'Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
    '    If fn_Validate() = False Then Exit Sub
    '    If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
    '    If fn_Save() = False Then
    '        MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
    '    Else
    '        MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
    '        sStatusSave = "NEW"
    '        Me.Close()
    '    End If
    'End Sub
    Private Sub btnSimpan_Click() Handles btnSimpan.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            oFormMode = FORM_MODE.FORM_MODE_EDIT
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
            'Me.Close()
        End If
    End Sub
    Private Sub txtCariDiagnosa_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCariDiagnosa.KeyPress
        If Asc(e.KeyChar) = 13 Then
            btnCariDiagnosaAsesmen_Click()
        End If
    End Sub
    Private Sub btnCariDiagnosa_Click(sender As Object, e As EventArgs) Handles btnCariDiagnosa.Click
        btnCariDiagnosaAsesmen_Click()
    End Sub
    Private Sub btnCariDiagnosaAsesmen_Click()
        If txtCariDiagnosa.Text.Length <3 Then
            MsgBox("Minimal 3 Huruf !!!", MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                End If
                Try
            Dim oGetGrouper As New Brigging.clsSetKoneksi
            Dim jsonDecode = JObject.Parse(oGetGrouper.fn_Pencariandiagnosa(sEklaim_Url, sEklaim_Generate, txtCariDiagnosa.Text))
            Dim sDataDuplicate As String = String.Empty
            Dim smessage As String = String.Empty

            sDataDuplicate = jsonDecode("metadata")("code").ToString
            smessage = jsonDecode("metadata")("message").ToString

            If sDataDuplicate = "200" Then
                Dim table As DataTable

                table = New DataTable("M_DIAGNOSA")
                table.Columns.Add("nama")
                table.Columns.Add("kode")

                For Each item In jsonDecode("response")("data")
                    table.Rows.Add(New String() {item(0), item(1)})
                Next

                grdCariDiagnosa.Properties.DataSource = table
                grdCariDiagnosa.Properties.ValueMember = "kode"
                grdCariDiagnosa.Properties.DisplayMember = "nama"

                grdCariDiagnosa.ShowPopup()

                txtCariDiagnosa.ResetText()
            Else
                MsgBox("Pencarian Diagnosa" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Information, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox("Pencarian Diagnosa: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdCariDiagnosa_EditValueChanged(sender As Object, e As EventArgs) Handles grdCariDiagnosa.EditValueChanged
        If grdCariDiagnosa.Text <> "" Then
            Dim sCek As Integer = 0

            For i As Integer = 0 To grvDiagnosa.RowCount - 2
                sCek += 1
            Next

            grvDiagnosa.Focus()
            grvDiagnosa.AddNewRow()
            grvDiagnosa.SetFocusedRowCellValue(colKATEGORI, IIf(sCek = 0, "Primer", "Sekunder"))
            grvDiagnosa.SetFocusedRowCellValue(colKDDIAGNOSA, grdCariDiagnosa.EditValue)
            grvDiagnosa.SetFocusedRowCellValue(colNAMADIAGNOSA, grdCariDiagnosa.Text)
            grvDiagnosa.UpdateCurrentRow()
        End If
    End Sub
    Private Sub grvDIAGNOSA_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDiagnosa.CellValueChanged
        If e.Column.Name = colNAMADIAGNOSA.Name Then
            Dim CEK As Boolean = False

            For i As Integer = 0 To grvDiagnosa.RowCount - 2
                If grvDiagnosa.GetRowCellValue(i, colKATEGORI) = "Primer" Then
                    CEK = True
                End If
            Next

            If CEK = False Then
                grvDiagnosa.SetFocusedRowCellValue(colKATEGORI, "Primer")
            Else
                grvDiagnosa.SetFocusedRowCellValue(colKATEGORI, "Sekunder")
            End If
        End If
    End Sub
    Private Sub deTANGGALPULANG_EditValueChanged_1(sender As Object, e As EventArgs) Handles deTANGGALPULANG.EditValueChanged
        fn_HitungTanggal()
    End Sub
    Private Sub txtCARI_PROSEDUR_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox10.KeyPress
        If Asc(e.KeyChar) = 13 Then
            btnCariProsedur_Click()
        End If
    End Sub
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        btnCariProsedur_Click()
    End Sub
    Private Sub btnCariProsedur_Click()
        Try
            If TextBox10.Text.Length < 3 Then
                MsgBox("Minimal 3 Huruf !!!", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            Try
                Dim oGetGrouper As New Brigging.clsSetKoneksi
                Dim jsonDecode = JObject.Parse(oGetGrouper.fn_PencarianProsedur(sEklaim_Url, sEklaim_Generate, TextBox10.Text))
                Dim sDataDuplicate As String = String.Empty
                Dim smessage As String = String.Empty

                sDataDuplicate = jsonDecode("metadata")("code").ToString
                smessage = jsonDecode("metadata")("message").ToString

                If sDataDuplicate = "200" Then
                    Dim table As DataTable

                    table = New DataTable("M_PROSEDUR")
                    table.Columns.Add("nama")
                    table.Columns.Add("kode")

                    For Each item In jsonDecode("response")("data")
                        table.Rows.Add(New String() {item(0), item(1)})
                    Next

                    GridLookUpEdit2.Properties.DataSource = table
                    GridLookUpEdit2.Properties.ValueMember = "kode"
                    GridLookUpEdit2.Properties.DisplayMember = "nama"

                    GridLookUpEdit2.ShowPopup()

                    TextBox10.ResetText()
                Else
                    MsgBox("Pencarian Prosedur" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Information, Me.Text)
                End If
            Catch oErr As Exception
                MsgBox("Pencarian Prosedur: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub GridLookUpEdit2_EditValueChanged_1(sender As Object, e As EventArgs) Handles GridLookUpEdit2.EditValueChanged
        If GridLookUpEdit2.Text <> "" Then
            grvPROSEDUR.Focus()
            grvPROSEDUR.AddNewRow()
            grvPROSEDUR.SetFocusedRowCellValue(colKDPROSEDUR, GridLookUpEdit2.EditValue)
            grvPROSEDUR.SetFocusedRowCellValue(colNAMA, GridLookUpEdit2.Text)
            grvPROSEDUR.UpdateCurrentRow()
        End If
    End Sub
    Private Sub btnLoadData_Click(sender As Object, e As EventArgs) Handles btnLoadData.Click
        Dim frmListTemplate As New frmListTemplate
        Try
            sKODETEMPLATE_RINGKASAN = String.Empty

            frmListTemplate.fn_LoadKategori(1, "")
            frmListTemplate.ShowDialog(Me)

            If sKODETEMPLATE_RINGKASAN <> "" Then
                fn_LoadDataRINGKASANKELUARTEMPLATE(sKODETEMPLATE_RINGKASAN)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnSaveAs_Click(sender As Object, e As EventArgs) Handles btnSaveAs.Click
        'sREMARKS = String.Empty
        'frmJudulTemplate.ShowDialog(Me)

        'If sREMARKS = "" Then
        '    MsgBox("Judul Template Masih Kosong", MsgBoxStyle.Exclamation, Me.Text)
        'Else
        '    sKODETEMPLATE_RINGKASAN = String.Empty
        '    fn_SaveRINGKASANKELUARTemplate(sKODETEMPLATE_RINGKASAN)
        '    sREMARKS = String.Empty
        'End If

        frmJudulTemplate.ShowDialog(Me)

        sKODETEMPLATE_RINGKASAN = String.Empty
        fn_SaveRINGKASANKELUARTemplate(sKODETEMPLATE_RINGKASAN)
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If sKODETEMPLATE_RINGKASAN = "" Then
            MsgBox("Load Data Template Belum di pilih", MsgBoxStyle.Exclamation, Me.Text)
        Else
            fn_SaveRINGKASANKELUARTemplate(sKODETEMPLATE_RINGKASAN)
            'sKODETEMPLATE_RINGKASAN = String.Empty
        End If
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadDPJP()
        Dim oDPJP As New Reference.clsDoctor
        Try
            Dim ds = oDPJP.GetData.Where(Function(x) x.ISACTIVE = True)

            grdDPJP.Properties.DataSource = ds.ToList()
            grdDPJP.Properties.ValueMember = "KDDOCTOR"
            grdDPJP.Properties.DisplayMember = "NAME_DISPLAY"

            grdDokter2.Properties.DataSource = ds.ToList()
            grdDokter2.Properties.ValueMember = "KDDOCTOR"
            grdDokter2.Properties.DisplayMember = "NAME_DISPLAY"

            grdDokter3.Properties.DataSource = ds.ToList()
            grdDokter3.Properties.ValueMember = "KDDOCTOR"
            grdDokter3.Properties.DisplayMember = "NAME_DISPLAY"

            grdDokter4.Properties.DataSource = ds.ToList()
            grdDokter4.Properties.ValueMember = "KDDOCTOR"
            grdDokter4.Properties.DisplayMember = "NAME_DISPLAY"

            grdDokter5.Properties.DataSource = ds.ToList()
            grdDokter5.Properties.ValueMember = "KDDOCTOR"
            grdDokter5.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDepartment()
        Dim oDepartment As New Reference.clsDepartment
        Try
            grdKDDEPARTMENT.Properties.DataSource = oDepartment.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDEPARTMENT.Properties.ValueMember = "KDDEPARTMENT"
            grdKDDEPARTMENT.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        grvDiagnosa.DeleteSelectedRows()
    End Sub
    Private Sub DeleteToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem1.Click
        grvPROSEDUR.DeleteSelectedRows()
    End Sub
    Private Sub btnDischargePlanning_Click(sender As Object, e As EventArgs) Handles btnDischargePlanning.Click
        Try
            Dim oDigital_DischargePlanning As New EMedrek.clsDigital_DischargePlanning

            Dim dsAsesmen = oDigital_DischargePlanning.GetData(sNoId)
            If dsAsesmen IsNot Nothing Then
                txtKeluhanUtama.Text = dsAsesmen.ANAMNESIS_01
                txtAnamnesa.Text = dsAsesmen.ANAMNESIS_02
                txtTerapi.Text = dsAsesmen.ANAMNESIS_36 & vbCrLf & dsAsesmen.ANAMNESIS_37
                txtKomorbiditasLain.Text = dsAsesmen.ANAMNESIS_03

                Dim listPemeriksaanFisik As New List(Of String)

                If dsAsesmen.ANAMNESIS_08 <> "" Then
                    listPemeriksaanFisik.Add("Berat Badan : " & dsAsesmen.ANAMNESIS_08 & " Kg")
                End If
                If dsAsesmen.ANAMNESIS_12 <> "" Then
                    listPemeriksaanFisik.Add("Tensi : " & dsAsesmen.ANAMNESIS_12 & " mmHg")
                End If
                If dsAsesmen.ANAMNESIS_11 <> "" Then
                    listPemeriksaanFisik.Add("Nadi : " & dsAsesmen.ANAMNESIS_11 & " x/mnt")
                End If
                If dsAsesmen.ANAMNESIS_14 <> "" Then
                    listPemeriksaanFisik.Add("Pernapasan : " & dsAsesmen.ANAMNESIS_14 & " x/mnt")
                End If
                If dsAsesmen.ANAMNESIS_13 <> "" Then
                    listPemeriksaanFisik.Add("Suhu : " & dsAsesmen.ANAMNESIS_13 & " oC")
                End If
                If dsAsesmen.ANAMNESIS_09 <> "" Then
                    listPemeriksaanFisik.Add("Tinggi Badan : " & dsAsesmen.ANAMNESIS_09 & " Cm")
                End If
                If dsAsesmen.ANAMNESIS_10 <> "" Then
                    listPemeriksaanFisik.Add("Gizi : " & dsAsesmen.ANAMNESIS_10)
                End If

                If dsAsesmen.ANAMNESIS_05 <> "" Then
                    listPemeriksaanFisik.Add("Keadaan Umum : " & dsAsesmen.ANAMNESIS_05)
                End If
                If dsAsesmen.ANAMNESIS_06 <> "" Then
                    listPemeriksaanFisik.Add("Kesadaran : " & dsAsesmen.ANAMNESIS_06)
                End If

                If dsAsesmen.ANAMNESIS_15_2_CHEK = True Then
                    If dsAsesmen.ANAMNESIS_15 <> "" Then
                        listPemeriksaanFisik.Add("Kepala " & dsAsesmen.ANAMNESIS_15)
                    End If
                End If

                If dsAsesmen.ANAMNESIS_16_2_CHEK = True Then
                    If dsAsesmen.ANAMNESIS_16 <> "" Then
                        listPemeriksaanFisik.Add("Mata " & dsAsesmen.ANAMNESIS_16)
                    End If
                End If

                If dsAsesmen.ANAMNESIS_17_2_CHEK = True Then
                    If dsAsesmen.ANAMNESIS_17 <> "" Then
                        listPemeriksaanFisik.Add("Leher " & dsAsesmen.ANAMNESIS_17)
                    End If
                End If

                If dsAsesmen.ANAMNESIS_18_2_CHEK = True Then
                    If dsAsesmen.ANAMNESIS_18 <> "" Then
                        listPemeriksaanFisik.Add("Dada " & dsAsesmen.ANAMNESIS_18)
                    End If
                End If

                If dsAsesmen.ANAMNESIS_19_2_CHEK = True Then
                    If dsAsesmen.ANAMNESIS_19 <> "" Then
                        listPemeriksaanFisik.Add("Perut " & dsAsesmen.ANAMNESIS_19)
                    End If
                End If

                If dsAsesmen.ANAMNESIS_20_2_CHEK = True Then
                    If dsAsesmen.ANAMNESIS_20 <> "" Then
                        listPemeriksaanFisik.Add("Alat Gerak " & dsAsesmen.ANAMNESIS_20)
                    End If
                End If

                If dsAsesmen.ANAMNESIS_29 <> "" Then
                    listPemeriksaanFisik.Add("Genetalia : " & dsAsesmen.ANAMNESIS_29)
                End If
                If dsAsesmen.ANAMNESIS_30 <> "" Then
                    listPemeriksaanFisik.Add("Ekstremitas : " & dsAsesmen.ANAMNESIS_30)
                End If
                If dsAsesmen.ANAMNESIS_31 <> "" Then
                    listPemeriksaanFisik.Add("Kulit : " & dsAsesmen.ANAMNESIS_31)
                End If
                If dsAsesmen.ANAMNESIS_32 <> "" Then
                    listPemeriksaanFisik.Add("status lokalis : " & dsAsesmen.ANAMNESIS_32)
                End If

                txtPemeriksaanFisik.Text = String.Join(vbCrLf, listPemeriksaanFisik.ToArray)

            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Try
            Dim oGrouperDataCppt As New Grouper.clsR_CPPT

            Dim dsTerakhirTTV = oGrouperDataCppt.GetDataByRmTerakhirTTV(sKDCUSTOMER)
            If dsTerakhirTTV IsNot Nothing Then
                txtNadi.Text = dsTerakhirTTV.OBJEKTIF_HR
                txtFrekuensi.Text = dsTerakhirTTV.OBJEKTIF_RR
                txtSaturasi.Text = dsTerakhirTTV.OBJEKTIF_SPO2
                txtSuhu.Text = dsTerakhirTTV.OBJEKTIF_SUHU
                txtBeratBadan.Text = dsTerakhirTTV.OBJEKTIF_BERATBADAN
                txtSistole.Text = dsTerakhirTTV.OBJEKTIF_SISTOLE
                txtDiastole.Text = dsTerakhirTTV.OBJEKTIF_DIASTOLE
                txtGCS.Text = dsTerakhirTTV.OBJEKTIF_GCS
            End If

            Dim dsTerakhirUser = oGrouperDataCppt.GetDataByRmTerakhirByUserAndDokter(sKDCUSTOMER, sUserID)
            If dsTerakhirUser IsNot Nothing Then
                For Each xloop In oGrouperDataCppt.GetDataDetailDiagnosa(dsTerakhirUser.KDCPPT)
                    grvDiagnosa.Focus()
                    grvDiagnosa.AddNewRow()
                    grvDiagnosa.SetFocusedRowCellValue(colKATEGORI, xloop.KATEGORI)
                    grvDiagnosa.SetFocusedRowCellValue(colKDDIAGNOSA, xloop.KDDIAGNOSA)
                    grvDiagnosa.SetFocusedRowCellValue(colNAMADIAGNOSA, xloop.MEMO)
                    grvDiagnosa.UpdateCurrentRow()
                Next

                'For Each xloop In oGrouperDataCppt.GetDataDetailProsedu(dsTerakhirUser.KDCPPT)
                '    grvPROSEDUR.Focus()
                '    grvPROSEDUR.AddNewRow()
                '    grvPROSEDUR.SetFocusedRowCellValue(colKDPROSEDUR, xloop.KDPROSEDUR)
                '    grvPROSEDUR.SetFocusedRowCellValue(colNAMA, xloop.NAMAPROSEDUR)
                '    grvPROSEDUR.UpdateCurrentRow()
                'Next
            End If
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub chkKEADAANSAATKELUAR_1_CheckedChanged(sender As Object, e As EventArgs) Handles chkKEADAANSAATKELUAR_1.CheckedChanged
        If chkKEADAANSAATKELUAR_1.Checked = True Then
            chkKEADAANSAATKELUAR_3.Checked = False
            chkKEADAANSAATKELUAR_5.Checked = False
            chkKEADAANSAATKELUAR_2.Checked = False
            chkKEADAANSAATKELUAR_4.Checked = False
        End If
    End Sub
    Private Sub chkKEADAANSAATKELUAR_3_CheckedChanged(sender As Object, e As EventArgs) Handles chkKEADAANSAATKELUAR_3.CheckedChanged
        If chkKEADAANSAATKELUAR_3.Checked = True Then
            chkKEADAANSAATKELUAR_1.Checked = False
            chkKEADAANSAATKELUAR_5.Checked = False
            chkKEADAANSAATKELUAR_2.Checked = False
            chkKEADAANSAATKELUAR_4.Checked = False
        End If
    End Sub
    Private Sub chkKEADAANSAATKELUAR_5_CheckedChanged(sender As Object, e As EventArgs) Handles chkKEADAANSAATKELUAR_5.CheckedChanged
        If chkKEADAANSAATKELUAR_5.Checked = True Then
            chkKEADAANSAATKELUAR_3.Checked = False
            chkKEADAANSAATKELUAR_1.Checked = False
            chkKEADAANSAATKELUAR_2.Checked = False
            chkKEADAANSAATKELUAR_4.Checked = False
        End If
    End Sub
    Private Sub chkKEADAANSAATKELUAR_2_CheckedChanged(sender As Object, e As EventArgs) Handles chkKEADAANSAATKELUAR_2.CheckedChanged
        If chkKEADAANSAATKELUAR_2.Checked = True Then
            chkKEADAANSAATKELUAR_3.Checked = False
            chkKEADAANSAATKELUAR_5.Checked = False
            chkKEADAANSAATKELUAR_1.Checked = False
            chkKEADAANSAATKELUAR_4.Checked = False
        End If
    End Sub
    Private Sub chkKEADAANSAATKELUAR_4_CheckedChanged(sender As Object, e As EventArgs) Handles chkKEADAANSAATKELUAR_4.CheckedChanged
        If chkKEADAANSAATKELUAR_4.Checked = True Then
            chkKEADAANSAATKELUAR_3.Checked = False
            chkKEADAANSAATKELUAR_5.Checked = False
            chkKEADAANSAATKELUAR_2.Checked = False
            chkKEADAANSAATKELUAR_1.Checked = False
        End If
    End Sub
#End Region
End Class