Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient
Imports Newtonsoft.Json.Linq
Imports DevExpress.XtraSplashScreen

Public Class frmPendaftaran
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oPendaftaranA As New Admission.clsPendaftaran
    Private sLoadAwal As Boolean = False
    Private oSetKoneksi As New Brigging.clsSetKoneksi
    Private sKODEBOOKINGEMPTY As String = String.Empty
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KODEBOOKING As String, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId

        sKODEBOOKINGEMPTY = KODEBOOKING
        'sKARTUKODEBOKING = ""

        txtKARTUBPJS.Properties.MaxLength = 13
        lPEMETAAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lFLAG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lKODEKUNJUNGAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cboCARI.SelectedIndex = 0
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = Pendaftaran.TITLE

            lKDPENDAFATRAN.Text = Pendaftaran.KDPENDAFTARAN & " *"
            lKDPENDAFTARAN_AWAL.Text = Pendaftaran.KDPENDAFTARAN_AWAL
            lNOMORSEP.Text = Pendaftaran.NOMORSEP & " *"
            lNOKARTUBPJS.Text = Pendaftaran.KARTUBPJS & " *"
            lKTP.Text = Pendaftaran.KTP & " *"
            lDATE.Text = Pendaftaran.TANGGAL
            lDAFTAR_L1.Text = sDaftar_L1
            lDAFTAR_L2.Text = sDaftar_L2
            lDAFTAR_L3.Text = sDaftar_L3
            lDAFTAR_L4.Text = sDaftar_L4
            lDAFTAR_L5.Text = sDaftar_L5
            lCARI.Text = Caption.Search
            lCATEGORY.Text = Pendaftaran.CATEGORY
            lKDCUSTOMER.Text = Pendaftaran.KDCUSTOMER & " *"
            lKDDEPARTMENT.Text = Pendaftaran.KDDEPARTMENT & " *"
            lKDDOCTOR.Text = Pendaftaran.KDDOCTOR & " *"
            lKDDIAGNOSA.Text = Pendaftaran.KDDIAGNOSA & " *"
            lCATATAN.Text = Pendaftaran.CATATAN & " *"

            lNAMAPASIEN.Text = Pendaftaran.KDCUSTOMER_NAMAPASIEN
            lALAMAT.Text = Pendaftaran.KDCUSTOMER_ALAMAT
            lPENJAMIN.Text = Pendaftaran.KDCUSTOMER_KDPENJAMIN
            lKESATUAN.Text = Pendaftaran.KDCUSTOMER_KESATUAN
            lPANGKAT.Text = Pendaftaran.KDCUSTOMER_KDPANGKAT
            lGOLONGAN.Text = Pendaftaran.KDCUSTOMER_KDGOLONGAN
            lPENDIDIKAN.Text = Pendaftaran.KDCUSTOMER_KDPENDIDIKAN
            lPEKERJAAN.Text = Pendaftaran.KDCUSTOMER_KDPEKERJAAN
            lPERUSAHAAN.Text = Pendaftaran.KDCUSTOMER_KDPERUSAHAAN
            lSTATUSKAWIN.Text = Pendaftaran.KDCUSTOMER_KDSTATUSKAWIN
            lSTATUSKELUARGA.Text = Pendaftaran.KDCUSTOMER_KDSTATUSKELUARGA

            lASALRUJUKAN.Text = Pendaftaran.ASALRUJUKAN & " *"
            lNOMORRUJUKAN.Text = Pendaftaran.NOMORRUJUKAN & " *"
            lDATE_RUJUKAN.Text = Pendaftaran.DATE_RUJUKAN & " *"
            lNOMORSKDP.Text = Pendaftaran.NOMORSKDP
            lKDDOCTOR_SKD.Text = Pendaftaran.KDKONTROL
            lNOMORTELEPON.Text = Pendaftaran.NOMORTELEPON & " *"
            lKDCOB.Text = Pendaftaran.KDCOB & " *"
            lKDPPK.Text = Pendaftaran.KDPPK & " *"
            lKDKELASRAWAT.Text = Pendaftaran.KDKELASRAWAT & " *"

            'btnSaveNew.Caption = Caption.FormSaveNew
            'btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose

            tab1.Text = Pendaftaran.TAB_1
            tab2.Text = Pendaftaran.TAB_2
            tab3.Text = Pendaftaran.TAB_3

            chkCOB.Text = Pendaftaran.ISCOB
            chkLakaLantas.Text = Pendaftaran.JAMINAN_ISLAKLANTAS
            chkPenjamin1.Text = Pendaftaran.JAMINAN_PENJAMIN_PENJAMIN1
            chkPenjamin2.Text = Pendaftaran.JAMINAN_PENJAMIN_PENJAMIN2
            chkPenjamin3.Text = Pendaftaran.JAMINAN_PENJAMIN_PENJAMIN3
            chkPenjamin4.Text = Pendaftaran.JAMINAN_PENJAMIN_PENJAMIN4
            lTAB3_TGLKEJADIAN.Text = Pendaftaran.JAMINAN_PENJAMIN_TGLKEJADIAN
            lTAB3_KETERANGAN.Text = Pendaftaran.JAMINAN_PENJAMIN_KETERANGAN
            chkISSUPLESI.Text = Pendaftaran.JAMINAN_PENJAMIN_SUPLESI_ISSUPLESI
            lTAB3_NOSEPSUPLESI.Text = Pendaftaran.JAMINAN_PENJAMIN_SUPLESI_NOSEPSUPLESI
            'lTAB3_PROPINSI.Text = Pendaftaran.JAMINAN_PENJAMIN_SUPLESI_LOKASILAKA_KDPROPINSI
            'lTAB3_KABUPATEN.Text = Pendaftaran.JAMINAN_PENJAMIN_SUPLESI_LOKASILAKA_KABUPATEN
            'lTAB3_KECAMATAN.Text = Pendaftaran.JAMINAN_PENJAMIN_SUPLESI_LOKASILAKA_KECAMATAN

            'grvKDPAYMENTTYPE.Columns("MEMO").Caption = PaymentType.MEMO
            'grvKDCOA.Columns("NMCOA").Caption = COA.NMCOA

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        sCode = txtKDPENDAFTARAN.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadDaftar1()
        fn_LoadDaftar2()
        fn_LoadDaftar3()
        fn_LoadDaftar4()
        fn_LoadDaftar5()
        fn_LoadDiganosa()
        fn_LoadFaskes()
        fn_LoadDoctor_SKD()
        fn_LoadkelasRawat()
        fn_LoadCOB()
        fn_LoadPemetaan()

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
        btnAddCustomer.Enabled = Not Status
        btnPemetaan.Enabled = Not Status
        btnNCI.Enabled = Not Status
        btnCreateSEP.Enabled = Not Status
        btnCari.Enabled = Not Status

        txtNOMORSEP.Properties.ReadOnly = Status
        'txtKDPENDAFTARAN_AWAL.Properties.ReadOnly = Status
        deDATE.Properties.ReadOnly = Status
        txtKDCUSTOMER.Properties.ReadOnly = Status
        txtKARTUBPJS.Properties.ReadOnly = Status
        txtKTP.Properties.ReadOnly = Status
        grdKDDAFTAR_L1.Properties.ReadOnly = Status
        grdKDDAFTAR_L2.Properties.ReadOnly = Status
        grdKDDAFTAR_L3.Properties.ReadOnly = Status
        grdKDDAFTAR_L4.Properties.ReadOnly = Status
        grdKDDAFTAR_L5.Properties.ReadOnly = Status
        chkISEKSEKUTIF.Properties.ReadOnly = Status
        chkISKATARAK.Properties.ReadOnly = Status
        grdKDDEPARTMENT.Properties.ReadOnly = Status
        grdKDDOCTOR.Properties.ReadOnly = Status
        deDATE_RUJUKAN.Properties.ReadOnly = Status
        txtNOMORRUJUKAN.Properties.ReadOnly = Status
        txtNOMORSKDP.Properties.ReadOnly = Status
        grdKDDOCTOR_SKD.Properties.ReadOnly = Status
        grdKDDIAGNOSA.Properties.ReadOnly = Status
        txtNOMORTELEPON.Properties.ReadOnly = Status
        txtCATATAN.Properties.ReadOnly = Status
        grdKDCOB.Properties.ReadOnly = Status
        grdKDPPK.Properties.ReadOnly = Status
        grdKDKELASRAWAT.Properties.ReadOnly = Status
        chkCOB.Properties.ReadOnly = Status
        chkLakaLantas.Properties.ReadOnly = Status
        chkPenjamin1.Properties.ReadOnly = Status
        chkPenjamin2.Properties.ReadOnly = Status
        chkPenjamin3.Properties.ReadOnly = Status
        chkPenjamin4.Properties.ReadOnly = Status
        deDATE_PENJAMIN_TGLKEJADIAN.Properties.ReadOnly = Status
        txtJAMINAN_PENJAMIN_KETERANGAN.Properties.ReadOnly = Status
        chkISSUPLESI.Properties.ReadOnly = Status
        txtPENJAMIN_SUPLESI_NOSEPSUPLESI.Properties.ReadOnly = Status
        txtSUPLESI_PROPINSI.Properties.ReadOnly = Status
        txtSUPLESI_KABUPATEN.Properties.ReadOnly = Status
        txtSUPLESI_KECAMATAN.Properties.ReadOnly = Status
        txtNAMAPENANGGUNGJAWAB.Properties.ReadOnly = Status
        cboHUBUNGANPENANGGUNGJAWAB.Properties.ReadOnly = Status
        txtALAMATPENANGGUNGJAWAB.Properties.ReadOnly = Status
        txtNOMORTELEPONPENANGGUNGJAWAB.Properties.ReadOnly = Status
        If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            rbCATEGORY.Properties.ReadOnly = False
        Else
            rbCATEGORY.Properties.ReadOnly = True
        End If

        txtKDBOOKING.Properties.ReadOnly = Status

        grdPEMETAAN.Properties.ReadOnly = Status

        grdNAIKKELAS.Properties.ReadOnly = False
        cboPEMBIAYAAN.Properties.ReadOnly = False
        txtPENANGGUNGJAWAB.Properties.ReadOnly = False
        cboTUJUANKUNJUNGAN.Properties.ReadOnly = False
        cboFLAGPROCEDURE.Properties.ReadOnly = False
        cboKODEKUNJUNGAN.Properties.ReadOnly = False
        cboASESMENPELAYANAN.Properties.ReadOnly = False

    End Sub
    Private Sub fn_EmptyMe()
        txtKDPENDAFTARAN.Text = "<--- AUTO --->"
        txtNOMORSEP.Text = "<--- AUTO --->"
        txtANTRIANPOLI.Text = "<--- AUTO --->"
        txtKDPENDAFTARAN_AWAL.ResetText()
        deDATE.DateTime = Now
        txtKDCUSTOMER.ResetText()
        txtKARTUBPJS.ResetText()
        txtKTP.ResetText()
        If sLoadAwal = False Then
            grdKDDAFTAR_L1.Text = oPendaftaranA.Daftar_L1_Default
        End If
        grdKDDAFTAR_L2.Text = oPendaftaranA.Daftar_L2_Default
        grdKDDAFTAR_L3.Text = oPendaftaranA.Daftar_L3_Default
        grdKDDAFTAR_L4.Text = oPendaftaranA.Daftar_L4_Default
        grdKDDAFTAR_L5.Text = oPendaftaranA.Daftar_L5_Default
        rbCATEGORY_SelectedIndexChanged()
        chkISEKSEKUTIF.Checked = False
        chkISKATARAK.Checked = False

        grdKDDEPARTMENT.ResetText()
        grdKDDOCTOR.ResetText()

        deDATE_RUJUKAN.DateTime = Now
        txtNOMORRUJUKAN.ResetText()
        txtNOMORSKDP.ResetText()
        grdKDDOCTOR_SKD.ResetText()
        grdKDDIAGNOSA.ResetText()
        txtNOMORTELEPON.Text = ""
        txtCATATAN.Text = "-"
        grdKDCOB.Text = oPendaftaranA.Daftar_COB_Default
        grdKDPPK.Text = oPendaftaranA.Daftar_PPK_Default
        grdKDKELASRAWAT.Text = oPendaftaranA.Daftar_KELASRAWAT_Default
        grdKDKELASRAWAT.ResetText()
        chkCOB.Checked = False
        chkLakaLantas.Checked = False
        chkPenjamin1.Checked = False
        chkPenjamin2.Checked = False
        chkPenjamin3.Checked = False
        chkPenjamin4.Checked = False
        deDATE_PENJAMIN_TGLKEJADIAN.DateTime = Now
        txtJAMINAN_PENJAMIN_KETERANGAN.ResetText()
        chkISSUPLESI.Checked = False
        txtPENJAMIN_SUPLESI_NOSEPSUPLESI.ResetText()
        txtSUPLESI_PROPINSI.ResetText()
        txtSUPLESI_KABUPATEN.ResetText()
        txtSUPLESI_KECAMATAN.ResetText()
        txtNAMAPASIEN.ResetText()
        txtKDJENISKELAMIN.ResetText()
        txtALAMAT.ResetText()
        grdKDPENJAMIN.ResetText()
        grdKDKESATUAN.ResetText()
        grdKDGOLONGAN.ResetText()
        grdKDPENDIDIKAN.ResetText()
        grdKDPEKERJAAN.ResetText()
        grdKDPERUSAHAAN.ResetText()
        cboKDSTATUSKAWIN.ResetText()
        grdKDSTATUSKELUARGA.ResetText()
        txtSTATUSKELUARGA.ResetText()
        txtNAMAPENANGGUNGJAWAB.ResetText()
        cboHUBUNGANPENANGGUNGJAWAB.SelectedIndex = 0
        txtALAMATPENANGGUNGJAWAB.ResetText()
        txtNOMORTELEPONPENANGGUNGJAWAB.ResetText()
        grdPEMETAAN.ResetText()
        grdNAIKKELAS.ResetText()
        cboPEMBIAYAAN.SelectedIndex = 0
        txtPENANGGUNGJAWAB.Text = "Pribadi"
        cboTUJUANKUNJUNGAN.ResetText()
        cboFLAGPROCEDURE.ResetText()
        cboKODEKUNJUNGAN.ResetText()
        cboASESMENPELAYANAN.ResetText()

        txtKDBOOKING.Text = sKODEBOOKINGEMPTY

        If rbCATEGORY.SelectedIndex = 0 Then
            If txtKDBOOKING.Text <> "" Then
                Dim oSetBooking As New SettingAntrian.clsSetAntrian
                Dim dsSetBooking = oSetBooking.GetData(txtKDBOOKING.Text)
                If dsSetBooking IsNot Nothing Then
                    If dsSetBooking.NOMORREFERENSI <> "" Then
                        txtNOMORRUJUKAN.Text = dsSetBooking.NOMORREFERENSI

                        txtKARTUBPJS.Text = dsSetBooking.NOMORKARTU

                        Dim oCustomer As New Reference.clsCustomer
                        Dim KDCUSTOMER As String = String.Empty
                        Dim dsCustomer = oCustomer.GetDataKARTUBPJS(txtKARTUBPJS.Text.ToString.Trim.ToUpper)
                        If dsCustomer IsNot Nothing Then
                            KDCUSTOMER = dsCustomer.KDCUSTOMER
                        End If

                        Dim KARTUBPJS As String = String.Empty
                        KARTUBPJS = txtKARTUBPJS.Text.ToString.Trim.ToUpper

                        fn_GetDataCustomer(KDCUSTOMER)

                        If chkIsOfline.Checked = False Then
                            fn_LoadKartuBPJSSatuRecord(KARTUBPJS)
                            fn_LoadKartuBPJS(KARTUBPJS)
                        End If

                        If KDCUSTOMER = "" Then
                            If txtKDCUSTOMER.Text <> "" Then
                                fn_GetDataCustomer(txtKDCUSTOMER.Text)
                            End If
                        End If

                        'fn_LoadNomorRujukan(txtNOMORRUJUKAN.Text)

                        'Dim KDCUSTOMER As String = dsSetBooking.NORM

                        'If KDCUSTOMER.Contains("P") Then
                        '    sKARTUKODEBOKING = dsSetBooking.NOMORKARTU
                        '    btnAddCustomer_Click()
                        'Else
                        '    KDCUSTOMER = txtKDCUSTOMER.Text.ToString

                        '    fn_GetDataCustomer(KDCUSTOMER, True)

                        '    If chkIsOfline.Checked = False Then
                        '        fn_LoadKartuBPJS(txtKARTUBPJS.Text)
                        '        fn_LoadKartuBPJSSatuRecord(txtKARTUBPJS.Text)
                        '    End If
                        'End If


                        Dim oDepartment As New Reference.clsDepartment
                        Dim oDoctor As New Reference.clsDoctor

                        Dim dsDepartment = oDepartment.GetDatakodebpjs(dsSetBooking.KODEPOLI)
                        If dsDepartment IsNot Nothing Then
                            fn_LoadDepartment()
                            grdKDDEPARTMENT.Text = dsDepartment.KDDEPARTMENT
                        End If

                        Dim dsDoctor = oDoctor.GetDatakodebpjs(dsSetBooking.KODEDOKTER)
                        If dsDoctor IsNot Nothing Then
                            fn_LoadDoctor(grdKDDEPARTMENT.Text)
                            grdKDDOCTOR.Text = dsDoctor.KDDOCTOR
                        End If
                    Else
                        Dim oDepartment As New Reference.clsDepartment
                        Dim oDoctor As New Reference.clsDoctor

                        Dim dsDepartment = oDepartment.GetDatakodebpjs(dsSetBooking.KODEPOLI)
                        If dsDepartment IsNot Nothing Then
                            fn_LoadDepartment()
                            grdKDDEPARTMENT.Text = dsDepartment.KDDEPARTMENT
                        End If

                        Dim dsDoctor = oDoctor.GetDatakodebpjs(dsSetBooking.KODEDOKTER)
                        If dsDoctor IsNot Nothing Then
                            fn_LoadDoctor(grdKDDEPARTMENT.Text)
                            grdKDDOCTOR.Text = dsDoctor.KDDOCTOR
                        Else
                            Dim dsDoctor2 = oDoctor.GetData(dsSetBooking.KODEDOKTER)
                            If dsDoctor2 IsNot Nothing Then
                                fn_LoadDoctor(grdKDDEPARTMENT.Text)
                                grdKDDOCTOR.Text = dsDoctor2.KDDOCTOR
                            End If
                        End If
                    End If
                Else
                    txtKDBOOKING.ResetText()
                End If
            End If
        Else
            txtKDBOOKING.ResetText()
        End If
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oPendaftaranA.GetData(sNoId)
            Dim dsKunjungan = oPendaftaranA.GetDataKunjunganByPendaftaran(sNoId)
            Dim dsPenanggungJawab = oPendaftaranA.GetDataPenanggungJawabByPendaftaran(sNoId)

            With ds
                grdPEMETAAN.Text = .KDUPDATE_APLICARE

                txtKDPENDAFTARAN.Text = sNoId
                txtNOMORSEP.Text = .NOMORSEP
                txtKDPENDAFTARAN_AWAL.Text = .KDPENDAFTARAN_AWAL
                deDATE.DateTime = .DATE
                txtKDCUSTOMER.Text = .KDCUSTOMER
                txtKARTUBPJS.Text = .KARTUBPJS
                txtKTP.Text = .KTP
                grdKDDAFTAR_L1.Text = .KDDAFTAR_L1
                grdKDDAFTAR_L2.Text = .KDDAFTAR_L2
                grdKDDAFTAR_L3.Text = .KDDAFTAR_L3
                grdKDDAFTAR_L4.Text = .KDDAFTAR_L4
                grdKDDAFTAR_L5.Text = .KDDAFTAR_L5
                rbCATEGORY.SelectedIndex = .CATEGORY
                fn_LoadDepartment()
                grdKDDEPARTMENT.Text = .KDDEPARTMENT
                'sKDDEPARTMENTEDIT = .KDDEPARTMENT
                fn_LoadDoctor(.KDDEPARTMENT)
                grdKDDOCTOR.Text = .KDDOCTOR
                chkIsOfline.Checked = .ISOFFLINE
                chkISEKSEKUTIF.Checked = .ISEKSEKUTIF
                chkISKATARAK.Checked = .ISKATARAK
                deDATE_RUJUKAN.DateTime = .DATE_RUJUKAN
                cboASALRUJUKAN.SelectedIndex = .ASALRUJUKAN
                txtNOMORRUJUKAN.Text = .NOMORRUJUKAN
                txtNOMORSKDP.Text = .NOMORSKDP
                grdKDDOCTOR_SKD.Text = .KDDOCTOR_SKD
                grdKDDIAGNOSA.Text = .KDDIAGNOSA
                txtNOMORTELEPON.Text = .NOMORTELEPON
                txtCATATAN.Text = .CATATAN
                grdKDCOB.Text = .KDCOB
                grdKDPPK.Text = .KDPPK
                grdKDKELASRAWAT.Text = .KDKELASRAWAT
                chkCOB.Checked = .ISCOB
                chkLakaLantas.Checked = .JAMINAN_ISLAKALANTAS
                chkPenjamin1.Checked = .JAMINAN_PENJAMIN_PENJAMIN1
                chkPenjamin2.Checked = .JAMINAN_PENJAMIN_PENJAMIN2
                chkPenjamin3.Checked = .JAMINAN_PENJAMIN_PENJAMIN3
                chkPenjamin4.Checked = .JAMINAN_PENJAMIN_PENJAMIN4
                deDATE_PENJAMIN_TGLKEJADIAN.DateTime = Now
                txtJAMINAN_PENJAMIN_KETERANGAN.Text = .JAMINAN_PENJAMIN_KETERANGAN
                chkISSUPLESI.Checked = .JAMINAN_PENJAMIN_SUPLESI_ISSUPLESI
                txtPENJAMIN_SUPLESI_NOSEPSUPLESI.Text = .JAMINAN_PENJAMIN_SUPLESI_NOSEPSUPLESI
                txtSUPLESI_PROPINSI.Text = .JAMINAN_PENJAMIN_SUPLESI_LOKASILAKA_KDPROPONSI
                txtSUPLESI_KABUPATEN.Text = .JAMINAN_PENJAMIN_SUPLESI_LOKASILAKA_KDKABUPATEN
                txtSUPLESI_KECAMATAN.Text = .JAMINAN_PENJAMIN_SUPLESI_LOKASILAKA_KDKECAMATAN

                tabControl.SelectedTabPage = tab3
                tabControl.SelectedTabPage = tab2
                tabControl.SelectedTabPage = tab1

                If .CATEGORY = 1 Then
                    With dsPenanggungJawab
                        txtNAMAPENANGGUNGJAWAB.Text = dsPenanggungJawab.NAMA
                        cboHUBUNGANPENANGGUNGJAWAB.Text = dsPenanggungJawab.HUBUNGAN
                        txtALAMATPENANGGUNGJAWAB.Text = dsPenanggungJawab.ALAMAT
                        txtNOMORTELEPONPENANGGUNGJAWAB.Text = dsPenanggungJawab.NOMORTELEPON
                    End With
                End If

                fn_GetDataCustomer(.KDCUSTOMER)

                grdNAIKKELAS.Text = .NAIKRANAP
                cboPEMBIAYAAN.Text = .PEMBIAYAAN
                txtPENANGGUNGJAWAB.Text = .PENANGGUNGJAWAB
                cboTUJUANKUNJUNGAN.Text = .TUJUANKUNJUNGAN
                cboFLAGPROCEDURE.Text = .FLAGPROCEDURE
                cboKODEKUNJUNGAN.Text = .KDPENUNJANG
                cboASESMENPELAYANAN.Text = .ASESMENPELAYANAN
                txtKDBOOKING.Text = .KODEBOOKING
            End With

            With dsKunjungan
                txtALAMAT.Text = .ALAMAT
                grdKDPENJAMIN.Text = .KDPENJAMIN
                grdKDKESATUAN.Text = .KDKESATUAN
                grdKDPANGKAT.Text = .KDPANGKAT
                grdKDGOLONGAN.Text = .KDGOLONGAN
                grdKDPENDIDIKAN.Text = .KDPENDIDIKAN
                grdKDPEKERJAAN.Text = .KDPEKERJAAN
                grdKDPERUSAHAAN.Text = .KDPERUSAHAAN
                cboKDSTATUSKAWIN.SelectedIndex = .KDSTATUSKAWIN
                txtSTATUSKELUARGA.Text = .NAMAKELUARGA
                grdKDSTATUSKELUARGA.Text = .KDSTATUSKELUARGA
                grdPEMETAAN.Text = .KDUPDATE_APLICARE
            End With

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_UpdateBookingAntrian(ByVal KODEBOOKING As String, ByVal PasienLama As Integer, ByVal sISPANGGIL As Integer) As Boolean
        Try
            Dim oSet_Antrian_Simpan As New SettingAntrian.clsSetAntrian
            Dim oDoctor As New Reference.clsDoctor
            Dim oDepartment As New Reference.clsDepartment
            Dim sHARI As String = String.Empty
            Dim sKDDOCTOR_BPJS As String = String.Empty
            Dim sKDDEPARTMENT_BPJS As String = oDepartment.GetData(grdKDDEPARTMENT.EditValue).VCLAIM_KODEPOLI

            Select Case Weekday(Now)
                Case 1
                    sHARI = "Minggu"
                Case 2
                    sHARI = "Senin"
                Case 3
                    sHARI = "Selasa"
                Case 4
                    sHARI = "Rabu"
                Case 5
                    sHARI = "Kamis"
                Case 6
                    sHARI = "Jumat"
                Case 7
                    sHARI = "Sabtu"
            End Select

            Dim dsJawdwalDokter = oDoctor.GetDataDetailJadwalDokter(grdKDDOCTOR.EditValue, sHARI)

            If dsJawdwalDokter Is Nothing Then
                MsgBox("Jadwal Dokter di SIMRS belum ada Untuk Dokter " & grdKDDOCTOR.Text & " di Hari " & sHARI & " Silahkan Lengkapi Jadwal Dokter di Referensi Dokter", MsgBoxStyle.Exclamation, Me.Text)
                fn_UpdateBookingAntrian = False
                Exit Function
            Else
                sKDDOCTOR_BPJS = dsJawdwalDokter.M_DOCTOR.VCLAIM_KDDPJP
            End If

            ' ***** HEADER *****
            Dim ds = oSet_Antrian_Simpan.GetStructureHeader

            With ds
                Try
                    .DATECREATED = oSet_Antrian_Simpan.GetData(KODEBOOKING).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDPENJAMIN = grdKDPENJAMIN.EditValue
                .KODEBOOKING = KODEBOOKING
                Try
                    .JENISPASIEN_RS = oSet_Antrian_Simpan.GetData(KODEBOOKING).JENISKUNJUNGAN
                Catch ex As Exception
                    .JENISPASIEN_RS = "U"
                End Try
                .JENISPASIEN = IIf(grdKDDAFTAR_L1.Text = "BPJS", "JKN", "NON JKN")
                .NOMORKARTU = txtKARTUBPJS.Text
                .NOHP = txtNOMORTELEPON.Text.ToString.Trim
                .NIK = txtKTP.Text.ToString.Trim
                .KODEPOLI = sKDDEPARTMENT_BPJS
                .NAMAPOLI = grdKDDEPARTMENT.Text
                .PASIENBARU = PasienLama
                .NORM = txtKDCUSTOMER.Text
                .TANGGALPERIKSA = deDATE.DateTime
                .TANGGALPERIKSA_TEXT = deDATE.DateTime.ToString("ddMMyyyy")
                .KODEDOKTER = sKDDOCTOR_BPJS
                .NAMADOKTER = grdKDDOCTOR.Text
                .JAMPRAKTEK = dsJawdwalDokter.BUKA & "-" & dsJawdwalDokter.TUTUP

                Dim Rawat As Boolean = False

                Dim oSKD As New Admission.clsSKD
                Dim dsSKD = oSKD.GetData(txtNOMORSKDP.Text)
                If dsSKD IsNot Nothing Then
                    If dsSKD.S_PENDAFTARAN_H.CATEGORY = 1 Then
                        Rawat = True
                    End If
                End If

                If sNomorSKDPspriSEP <> "" Then
                    Rawat = True
                End If

                sNomorSKDPspriSEP = ""

                If Rawat = True Then
                    .JENISKUNJUNGAN = 3
                    .NOMORREFERENSI = txtNOMORSKDP.Text
                Else
                    If cboTUJUANKUNJUNGAN.Text = "Konsul Dokter" Then
                        If cboASESMENPELAYANAN.Text = "Tujuan Kontrol" Then
                            .JENISKUNJUNGAN = 3
                        End If
                    ElseIf cboTUJUANKUNJUNGAN.Text = "Normal" Then
                        If txtNOMORSKDP.Text <> "" Then
                            'pasca rawat inap
                            .JENISKUNJUNGAN = 3
                        Else
                            If cboASESMENPELAYANAN.Text = "" Then
                                .JENISKUNJUNGAN = IIf(cboASALRUJUKAN.SelectedIndex = 0, 1, 4)
                            Else
                                .JENISKUNJUNGAN = 2
                            End If
                        End If
                    Else
                        .JENISKUNJUNGAN = 2
                    End If

                    If .JENISKUNJUNGAN = 2 Then
                        Dim oCounter As New Setting.clsCounter

                        Dim sMODULRUJUKANINTERNAL As String = "RINTERNAL"
                        Dim sLASTNUMBERINTERNAL As Integer = oCounter.GetLastNumber(sMODULRUJUKANINTERNAL, .TANGGALPERIKSA)
                        If sLASTNUMBERINTERNAL = 0 Then
                            Try
                                oCounter.InsertData(sMODULRUJUKANINTERNAL, .TANGGALPERIKSA)
                                sLASTNUMBERINTERNAL = oCounter.GetLastNumber(sMODULRUJUKANINTERNAL, .TANGGALPERIKSA)
                            Catch ex As Exception
                                sLASTNUMBERINTERNAL = 0
                            End Try
                        End If

                        .NOMORREFERENSI = .TANGGALPERIKSA.ToString("yyyyMMdd") & (sLASTNUMBERINTERNAL + 1).ToString.PadLeft(11, "0")

                        oCounter.UpdateData(sMODULRUJUKANINTERNAL, sLASTNUMBERINTERNAL + 1, Month(.TANGGALPERIKSA), Year(.TANGGALPERIKSA))

                    Else
                        If grdKDDAFTAR_L1.EditValue = "DAFTAR_L1_0000000001" Then
                            .NOMORREFERENSI = IIf(cboTUJUANKUNJUNGAN.Text = "Konsul Dokter", txtNOMORSKDP.Text, txtNOMORRUJUKAN.Text)
                        Else
                            .NOMORREFERENSI = ""
                        End If
                    End If
                End If

                Try
                    .NOMORANTREAN = oSet_Antrian_Simpan.GetData(KODEBOOKING).NOMORANTREAN
                    .ANGKAANTREAN = oSet_Antrian_Simpan.GetData(KODEBOOKING).ANGKAANTREAN
                Catch ex As Exception
                    .NOMORANTREAN = ""
                End Try

                If .NOMORANTREAN = "" Then
                    Dim dsDoctor = oDoctor.GetData(grdKDDOCTOR.EditValue)
                    Dim sMODULDOKTER As String = String.Empty

                    If dsDoctor IsNot Nothing Then
                        If dsDoctor.MEMO <> "" Then
                            sMODULDOKTER = dsDoctor.MEMO
                        End If
                    End If

                    If sMODULDOKTER = "" Then
                        sMODULDOKTER = .KODEPOLI
                    End If

                    If sMODULDOKTER = "" Then
                        sMODULDOKTER = "XXX"
                    End If

                    Dim oSetCounter As New SettingAntrian.clsSet_CounterAntrian
                    Dim sLASTNUMBER As Integer = 0

                    Try
                        sLASTNUMBER = oSetCounter.GetLastNumberDay(sMODULDOKTER, deDATE.DateTime)

                        If sLASTNUMBER = 0 Then
                            Try
                                oSetCounter.InsertData(sMODULDOKTER, deDATE.DateTime)
                                sLASTNUMBER = oSetCounter.GetLastNumberDay(sMODULDOKTER, deDATE.DateTime)
                            Catch ex As Exception
                                sLASTNUMBER = 0
                            End Try
                        End If

                    Catch ex As Exception
                        Throw ex
                    End Try

                    Try
                        oSetCounter.UpdateData(sMODULDOKTER, sLASTNUMBER + 1, deDATE.DateTime.ToString("dd"), deDATE.DateTime.ToString("MM"), deDATE.DateTime.ToString("yyyy"))
                    Catch ex As Exception
                        Throw ex
                    End Try

                    .NOMORANTREAN = sMODULDOKTER & (sLASTNUMBER + 1).ToString.PadLeft(3, "0")
                    .ANGKAANTREAN = sLASTNUMBER + 1
                End If

                txtANTRIANPOLI.Text = .NOMORANTREAN

                Dim sDokter = oDoctor.GetData(grdKDDOCTOR.EditValue)

                Dim estimasi As DateTime = DateTime.Parse(Now.ToString("yyyy-MM-dd") & " " & dsJawdwalDokter.BUKA)
                .ESTIMASIDILAYANI = (estimasi.AddMinutes(sDokter.ESTIMASI_MENIT * oDoctor.GetDataMonitoringTotal(sKDDEPARTMENT_BPJS, sKDDOCTOR_BPJS, Now)).ToString("yyyy-MM-dd HH:mm"))

                If estimasi > Now Then
                    estimasi = DateTime.Parse(Now.AddMinutes(sDokter.ESTIMASI_MENIT).ToString("yyyy-MM-dd HH:mm"))
                    .ESTIMASIDILAYANI = estimasi.ToString("yyyy-MM-dd HH:mm")
                End If

                .SISAKUOTAJKN = IIf(dsJawdwalDokter.KAPASITASPASIEN_JKN - oDoctor.GetDataMonitoringSisaJKN(sKDDEPARTMENT_BPJS, sKDDOCTOR_BPJS, Now) < 0, 0, dsJawdwalDokter.KAPASITASPASIEN_JKN - oDoctor.GetDataMonitoringSisaJKN(sKDDEPARTMENT_BPJS, sKDDOCTOR_BPJS, Now))
                .KUOTAJKN = dsJawdwalDokter.KAPASITASPASIEN_JKN
                .SISAKUOTANONJKN = IIf(dsJawdwalDokter.KAPASITASPASIEN_NONJKN - oDoctor.GetDataMonitoringSisaNONJKN(sKDDEPARTMENT_BPJS, sKDDOCTOR_BPJS, Now) < 0, 0, dsJawdwalDokter.KAPASITASPASIEN_NONJKN - oDoctor.GetDataMonitoringSisaNONJKN(sKDDEPARTMENT_BPJS, sKDDOCTOR_BPJS, Now))
                .KUOTANONJKN = dsJawdwalDokter.KAPASITASPASIEN_NONJKN
                .ISPANGGIL = sISPANGGIL
                Try
                    .ISONLINE = oSet_Antrian_Simpan.GetData(KODEBOOKING).ISONLINE
                Catch ex As Exception
                    .ISONLINE = False
                End Try
                Try
                    .KETERANGAN = oSet_Antrian_Simpan.GetData(KODEBOOKING).KETERANGAN
                Catch ex As Exception
                    .KETERANGAN = ""
                End Try
                Try
                    .KDSKD = oSet_Antrian_Simpan.GetData(KODEBOOKING).KDSKD
                Catch ex As Exception
                    .KDSKD = txtNOMORSKDP.Text
                End Try
            End With

            If oSet_Antrian_Simpan.UpdateData(ds) = True Then
                fn_UpdateBookingAntrian = True
            Else
                fn_UpdateBookingAntrian = False
            End If

        Catch oErr As Exception
            fn_UpdateBookingAntrian = False
            MsgBox("fn_SaveBookingAntrian : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_RequestTambahAntrean(ByVal KODEBOOKING As String) As String
        Try
            Dim oAntrian As New SettingAntrian.clsSetAntrian
            Dim jsonRequest As String = String.Empty

            Dim dsAntrian = oAntrian.GetData(KODEBOOKING)
            If dsAntrian IsNot Nothing Then
                If dsAntrian.KODEPOLI = "IGD" Then
                    fn_RequestTambahAntrean = ""
                    Exit Function
                End If


                'Dim s As String = dsAntrian.ESTIMASIDILAYANI
                'Dim dt As DateTime = DateTime.ParseExact(s, "yyyy-MM-dd HH:mm", Globalization.CultureInfo.InvariantCulture)

                'Dim uTime As String = (dt.Subtract(New DateTime(1970, 1, 1))).TotalMilliseconds

                '     Dim TanggalEstimasi As DateTime = Now
                '     TanggalEstimasi = dsAntrian.ESTIMASIDILAYANI & ":00"
                'TanggalEstimasi = dsAntrian.ESTIMASIDILAYANI

                '     Dim uTime As String = (TanggalEstimasi.Subtract(New DateTime(1970, 1, 1))).TotalMilliseconds

                'Dim jam As String = dsAntrian.ESTIMASIDILAYANI & ":00"

                'Dim TanggalEstimasi As DateTime = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd") & " " & jam, "yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture)

                'Dim unixMs As Long = New DateTimeOffset(TanggalEstimasi).ToUnixTimeMilliseconds()

                'Dim s As String = dsAntrian.ESTIMASIDILAYANI

                'Dim utc As DateTime = DateTime.ParseExact(s, "yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.AssumeUniversal)

                Dim TanggalEstimasi As DateTime = Now

                If dsAntrian.ESTIMASIDILAYANI IsNot Nothing Then
                    Dim waktuStr As String = dsAntrian.ESTIMASIDILAYANI.ToString().Trim()

                    Try
                        TanggalEstimasi = DateTime.Parse(waktuStr)


                    Catch ex As Exception
                        Console.WriteLine("Gagal parse tanggal: " & waktuStr & " - Error: " & ex.Message)
                        TanggalEstimasi = Now
                    End Try
                End If

                Dim epochWIB As DateTime = New DateTime(1970, 1, 1, 7, 0, 0)
                Dim uTime As String = (TanggalEstimasi.Subtract(epochWIB)).TotalMilliseconds.ToString()

                jsonRequest = " { "
                jsonRequest &= """kodebooking"": """ & dsAntrian.KODEBOOKING & ""","
                jsonRequest &= """jenispasien"": """ & dsAntrian.JENISPASIEN & """, "
                jsonRequest &= """nomorkartu"": """ & dsAntrian.NOMORKARTU & """, "
                jsonRequest &= """nik"": """ & dsAntrian.NIK.ToString & """, "
                jsonRequest &= """nohp"": """ & dsAntrian.NOHP.ToString.Trim & """, "
                'jsonRequest &= """kodepoli"": """ & dsAntrian.KODEPOLI & """, "
                jsonRequest &= """kodepoli"": """ & IIf(dsAntrian.KODEPOLI = "HDL", "INT", dsAntrian.KODEPOLI) & """, "
                jsonRequest &= """namapoli"": """ & dsAntrian.NAMAPOLI & """, "
                jsonRequest &= """pasienbaru"": """ & dsAntrian.PASIENBARU.ToString.Trim & """, "
                jsonRequest &= """norm"": """ & dsAntrian.NORM & """, "
                jsonRequest &= """tanggalperiksa"": """ & dsAntrian.TANGGALPERIKSA.ToString("yyyy-MM-dd") & """, "
                jsonRequest &= """kodedokter"": """ & dsAntrian.KODEDOKTER & """, "
                jsonRequest &= """namadokter"": """ & dsAntrian.NAMADOKTER & """, "
                jsonRequest &= """jampraktek"": """ & dsAntrian.JAMPRAKTEK & """, "
                jsonRequest &= """jeniskunjungan"": """ & dsAntrian.JENISKUNJUNGAN & """, "
                jsonRequest &= """nomorreferensi"": """ & dsAntrian.NOMORREFERENSI & """, "
                jsonRequest &= """nomorantrean"": """ & dsAntrian.NOMORANTREAN & """, "
                jsonRequest &= """angkaantrean"": """ & dsAntrian.ANGKAANTREAN & """, "
                jsonRequest &= """estimasidilayani"": """ & uTime & """, "
                jsonRequest &= """sisakuotajkn"": """ & dsAntrian.SISAKUOTAJKN & """, "
                jsonRequest &= """kuotajkn"": """ & dsAntrian.KUOTAJKN & """, "
                jsonRequest &= """sisakuotanonjkn"": """ & dsAntrian.SISAKUOTANONJKN & """, "
                jsonRequest &= """kuotanonjkn"": """ & dsAntrian.KUOTANONJKN & """, "
                jsonRequest &= """keterangan"": """ & dsAntrian.KETERANGAN & """ "
                jsonRequest &= "}  "

                fn_RequestTambahAntrean = jsonRequest
            Else
                fn_RequestTambahAntrean = ""
            End If
        Catch oErr As Exception
            fn_RequestTambahAntrean = ""
            MsgBox("Requset Tambah Antrean" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_TambahAntrean(ByVal jsonRequest As String, ByVal uTime As Integer) As String
        Try
            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Processing data tambah antrian ke bpjs.....")

            If sAntrol_ConsId <> "" Then
                Dim dsSetKoneksi = oSetKoneksi.TambahAntrean(sAntrol_Url, sAntrol_ConsId, sAntrol_SecreatKey, sAntrol_UserKey, uTime, jsonRequest)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metadata").Item("code")) = True, "", allData.Item("metadata").Item("code"))
                    messageResponse = allData("metadata")("message").ToString

                    If CodeResponse = "200" Then
                        fn_TambahAntrean = CodeResponse
                    Else
                        fn_TambahAntrean = CodeResponse & " - " & messageResponse
                    End If
                Else
                    fn_TambahAntrean = "Tambah Antrean Gagal"
                End If
            Else
                fn_TambahAntrean = "Koneksi Tidak ditemukan"
            End If

            'Simpan Keterangan
            Dim oSet_Antrian_Simpan As New SettingAntrian.clsSetAntrian

            Dim dscek = oSet_Antrian_Simpan.GetDataKeterangan(txtKDBOOKING.Text)

            Dim dKeterangan = oSet_Antrian_Simpan.GetStructureHeaderKeterangan
            With dKeterangan
                .KODEBOOKING = txtKDBOOKING.Text
                .REQUEST = jsonRequest
                .RESPON = fn_TambahAntrean
            End With

            If dscek IsNot Nothing Then
                oSet_Antrian_Simpan.UpdateDataKeterangan(dKeterangan)
            Else
                oSet_Antrian_Simpan.InsertDataKeterangan(dKeterangan)
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            fn_TambahAntrean = "Tambah Antrean" & vbCrLf & oErr.Message
        End Try
    End Function
    Private Sub fn_UpdateWaktuAntrean(ByVal KODEBOOKING As String, ByVal taskid As Integer)
        Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
        Dim oUpdateWaktuAntrian As New SettingAntrian_UI.clsSetAntrian_UI
        Dim JsonRequest As String = oUpdateWaktuAntrian.fn_RequestUpdateWaktuAntrean(KODEBOOKING, taskid, uTime)
        If JsonRequest <> "" Then
            MsgBox("Update Waktu Ke BPJS" & vbCrLf & oUpdateWaktuAntrian.fn_UpdateWaktuAntrean(JsonRequest, uTime), MsgBoxStyle.Information, Me.Text)
        End If
    End Sub
    Private Function fn_AntrianOnsiteBPJS(ByVal KODEBOOKING As String) As String
        fn_AntrianOnsiteBPJS = ""

        Try
            Dim jsonRequest As String = fn_RequestTambahAntrean(KODEBOOKING)

            Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

            If jsonRequest <> "" Then
                Dim HASIL = fn_TambahAntrean(jsonRequest, uTime)

                fn_AntrianOnsiteBPJS = HASIL

                'If Not HASIL.Contains("200") Then
                '    If HASIL.Contains("208 - Terdapat duplikasi Kode Booking") Then

                '    Else
                '        MsgBox("Antrian BPJS" & vbCrLf & HASIL, MsgBoxStyle.Information, Me.Text)
                '    End If
                'End If
            Else
                fn_AntrianOnsiteBPJS = "json tambah antrian kosong/IGD"
            End If
        Catch oErr As Exception
            MsgBox("Antrian Onsite : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                If txtANTRIANPOLI.Text = "<--- AUTO --->" Then
                    txtANTRIANPOLI.ResetText()
                End If
            End If

            If txtKDCUSTOMER.Text = "" Then
                MsgBox("Rekam medis tidak boleh " & txtKDCUSTOMER.Text & " Silahkan perbaiki Nomor Rekam Medis", MsgBoxStyle.Exclamation, Me.Text)

                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            ElseIf txtKDCUSTOMER.Text = "0" Then
                MsgBox("Rekam medis tidak boleh " & txtKDCUSTOMER.Text & " Silahkan perbaiki Nomor Rekam Medis", MsgBoxStyle.Exclamation, Me.Text)

                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            ElseIf txtKDCUSTOMER.Text = "00" Then
                MsgBox("Rekam medis tidak boleh " & txtKDCUSTOMER.Text & " Silahkan perbaiki Nomor Rekam Medis", MsgBoxStyle.Exclamation, Me.Text)

                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            ElseIf txtKDCUSTOMER.Text = "000" Then
                MsgBox("Rekam medis tidak boleh " & txtKDCUSTOMER.Text & " Silahkan perbaiki Nomor Rekam Medis", MsgBoxStyle.Exclamation, Me.Text)

                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            ElseIf txtKDCUSTOMER.Text = "0000" Then
                MsgBox("Rekam medis tidak boleh " & txtKDCUSTOMER.Text & " Silahkan perbaiki Nomor Rekam Medis", MsgBoxStyle.Exclamation, Me.Text)

                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            ElseIf txtKDCUSTOMER.Text = "00000" Then
                MsgBox("Rekam medis tidak boleh " & txtKDCUSTOMER.Text & " Silahkan perbaiki Nomor Rekam Medis", MsgBoxStyle.Exclamation, Me.Text)

                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            ElseIf txtKDCUSTOMER.Text = "000000" Then
                MsgBox("Rekam medis tidak boleh " & txtKDCUSTOMER.Text & " Silahkan perbaiki Nomor Rekam Medis", MsgBoxStyle.Exclamation, Me.Text)

                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            ElseIf txtKDCUSTOMER.Text = "0000000" Then
                MsgBox("Rekam medis tidak boleh " & txtKDCUSTOMER.Text & " Silahkan perbaiki Nomor Rekam Medis", MsgBoxStyle.Exclamation, Me.Text)

                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            ElseIf txtKDCUSTOMER.Text = "00000000" Then
                MsgBox("Rekam medis tidak boleh " & txtKDCUSTOMER.Text & " Silahkan perbaiki Nomor Rekam Medis", MsgBoxStyle.Exclamation, Me.Text)

                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            ElseIf txtKDCUSTOMER.Text = "000000000" Then
                MsgBox("Rekam medis tidak boleh " & txtKDCUSTOMER.Text & " Silahkan perbaiki Nomor Rekam Medis", MsgBoxStyle.Exclamation, Me.Text)

                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            ElseIf txtKDCUSTOMER.Text = "0000000000" Then
                MsgBox("Rekam medis tidak boleh " & txtKDCUSTOMER.Text & " Silahkan perbaiki Nomor Rekam Medis", MsgBoxStyle.Exclamation, Me.Text)

                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            ElseIf txtKDCUSTOMER.Text = "00000000000" Then
                MsgBox("Rekam medis tidak boleh " & txtKDCUSTOMER.Text & " Silahkan perbaiki Nomor Rekam Medis", MsgBoxStyle.Exclamation, Me.Text)

                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            End If

            If grdKDCOB.Text = String.Empty Then
                grdKDCOB.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDCOB.ErrorText = Statement.ErrorRequired

                grdKDCOB.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDPPK.Text = String.Empty Then
                grdKDPPK.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDPPK.ErrorText = Statement.ErrorRequired

                grdKDPPK.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDAFTAR_L1.Text = String.Empty Then
                grdKDDAFTAR_L1.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDAFTAR_L1.ErrorText = Statement.ErrorRequired

                grdKDDAFTAR_L1.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDAFTAR_L2.Text = String.Empty Then
                grdKDDAFTAR_L2.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDAFTAR_L2.ErrorText = Statement.ErrorRequired

                grdKDDAFTAR_L2.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDAFTAR_L3.Text = String.Empty Then
                grdKDDAFTAR_L3.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDAFTAR_L3.ErrorText = Statement.ErrorRequired

                grdKDDAFTAR_L3.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDAFTAR_L4.Text = String.Empty Then
                grdKDDAFTAR_L4.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDAFTAR_L4.ErrorText = Statement.ErrorRequired

                grdKDDAFTAR_L4.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDAFTAR_L5.Text = String.Empty Then
                grdKDDAFTAR_L5.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDAFTAR_L5.ErrorText = Statement.ErrorRequired

                grdKDDAFTAR_L5.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDKELASRAWAT.Text = String.Empty Then
                grdKDKELASRAWAT.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDKELASRAWAT.ErrorText = Statement.ErrorRequired

                grdKDKELASRAWAT.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtKDCUSTOMER.Text = String.Empty Then
                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDEPARTMENT.Text = String.Empty Then
                grdKDDEPARTMENT.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDEPARTMENT.ErrorText = Statement.ErrorRequired

                grdKDDEPARTMENT.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDOCTOR.Text = String.Empty Then
                grdKDDOCTOR.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDOCTOR.ErrorText = Statement.ErrorRequired

                grdKDDOCTOR.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDIAGNOSA.Text = String.Empty Then
                grdKDDIAGNOSA.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDIAGNOSA.ErrorText = Statement.ErrorRequired

                grdKDDIAGNOSA.Focus()
                fn_Validate = False
                Exit Function
            End If
            If chkCOB.Checked = True Then
                grdKDCOB.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDCOB.ErrorText = Statement.ErrorRequired

                grdKDCOB.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtKDPENDAFTARAN_AWAL.Text = String.Empty And rbCATEGORY.SelectedIndex = 1 Then
                If cboASALRUJUKAN.SelectedIndex = 0 Then
                    txtKDPENDAFTARAN_AWAL.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    txtKDPENDAFTARAN_AWAL.ErrorText = Statement.ErrorRequired

                    txtKDPENDAFTARAN_AWAL.Focus()
                    fn_Validate = False
                    Exit Function
                End If
            End If

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                If rbCATEGORY.SelectedIndex = 0 Then
                    Dim oPOLI As New Reference.clsDepartment
                    If oPOLI.GetData(grdKDDEPARTMENT.EditValue).VCLAIM_KODEPOLI <> "IGD" Then
                        If txtKDBOOKING.Text = "" Then
                            MsgBox("Kode Booking Kosong silahkan, Silhakan Masukkan Kode Booking", MsgBoxStyle.Information, Me.Text)

                            txtKDBOOKING.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                            txtKDBOOKING.ErrorText = Statement.ErrorRequired

                            txtKDBOOKING.Focus()
                            fn_Validate = False
                            Exit Function
                        Else
                            Dim oSetBooking As New SettingAntrian.clsSetAntrian
                            Dim dsCEK = oSetBooking.GetData(txtKDBOOKING.Text)
                            If dsCEK IsNot Nothing Then
                                Dim dsCekPendftaran = oSetBooking.GetDataPendaftaran(txtKDBOOKING.Text)
                                If dsCekPendftaran IsNot Nothing Then
                                    If dsCekPendftaran.KDCUSTOMER <> dsCEK.NORM Then
                                        txtKDBOOKING.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                                        txtKDBOOKING.ErrorText = Statement.ErrorRequired

                                        txtKDBOOKING.Focus()
                                        MsgBox("Nomor Antrian : " & vbCrLf & txtKDBOOKING.Text & " " & "Sudah digunakan Pasien" & dsCekPendftaran.M_CUSTOMER.NAME_DISPLAY, MsgBoxStyle.Exclamation, Me.Text)
                                        fn_Validate = False
                                        Exit Function
                                    End If
                                End If
                            Else
                                MsgBox("Kode Booking Nomor " & txtKDBOOKING.Text & " Kosong silahkan, Silhakan Masukkan Kode Booking", MsgBoxStyle.Exclamation, Me.Text)
                                fn_Validate = False
                                Exit Function
                            End If
                        End If
                    End If
                End If
            End If

            If cboTUJUANKUNJUNGAN.Text = "Konsul Dokter" Then
                If txtNOMORSKDP.Text = "" Then
                    MsgBox(Statement.ErrorStatement & "Merupakan Konsul Dokter, Silahkan Masukkan Nomor SKDP", MsgBoxStyle.Information, Me.Text)

                    txtNOMORSKDP.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    txtNOMORSKDP.ErrorText = Statement.ErrorRequired

                    txtNOMORSKDP.Focus()
                    fn_Validate = False
                    Exit Function
                End If
                If cboASESMENPELAYANAN.Text = "" Then
                    MsgBox(Statement.ErrorStatement & "Merupakan Konsul Dokter, Silahkan Pilih Pelayanan Tujuan Kontrol", MsgBoxStyle.Information, Me.Text)

                    cboASESMENPELAYANAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    cboASESMENPELAYANAN.ErrorText = Statement.ErrorRequired

                    cboASESMENPELAYANAN.Focus()
                    fn_Validate = False
                    Exit Function
                End If
            End If
            If txtNOMORSKDP.Text <> "" Then
                If cboTUJUANKUNJUNGAN.Text = "" Then
                    MsgBox(Statement.ErrorStatement & "Merupakan Konsul Dokter, Silahkan Masukan Tujuan Kunjungan", MsgBoxStyle.Information, Me.Text)

                    txtNOMORSKDP.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    txtNOMORSKDP.ErrorText = Statement.ErrorRequired

                    txtNOMORSKDP.Focus()
                    fn_Validate = False
                    Exit Function
                End If
            End If
            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    If txtNOMORSEP.Text <> "" Then
                        Dim dsSEP = oPendaftaranA.GetDataNomorSEP(txtNOMORSEP.Text)
                        If dsSEP IsNot Nothing Then
                            MsgBox("Rujukan Internal" & vbCrLf & "Sep Sudah Ada pada tanggal " & dsSEP.DATE.ToString("dd-MM-yyyy"), MsgBoxStyle.Exclamation, Me.Text)
                            txtNOMORSEP.Focus()
                            fn_Validate = False
                            Exit Function
                        End If
                    End If
                Catch ex As Exception
                    MsgBox("Pencarian SEP yang sama" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try

            End If

            If grdKDKELASRAWAT.Text = String.Empty And rbCATEGORY.SelectedIndex = 1 Then
                grdKDKELASRAWAT.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDKELASRAWAT.ErrorText = Statement.ErrorRequired

                grdKDKELASRAWAT.Focus()
                fn_Validate = False
                Exit Function
            End If

            Dim oDepartment As New Reference.clsDepartment

            If chkISEKSEKUTIF.Checked = True Then
                Dim dsDepartment = oDepartment.GetData(grdKDDEPARTMENT.EditValue)
                If dsDepartment IsNot Nothing Then
                    If dsDepartment.ISEKSEKUTIF = False Then
                        MsgBox("Bukan Eksekutif")
                        fn_Validate = False
                        Exit Function
                    End If
                End If
            End If

            If txtCATATAN.Text = String.Empty Then
                txtCATATAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtCATATAN.ErrorText = Statement.ErrorRequired

                txtCATATAN.Focus()
                fn_Validate = False
                Exit Function
            End If

            If chkISKATARAK.Checked = True Then
                Dim dsDepartment = oDepartment.GetData(grdKDDEPARTMENT.EditValue)
                If dsDepartment IsNot Nothing Then
                    If dsDepartment.ISKATARAK = False Then
                        MsgBox("Tidak bisa buka Katarak")
                        fn_Validate = False
                        Exit Function
                    End If
                End If
            End If

            If chkLakaLantas.Checked = True Then
                If txtJAMINAN_PENJAMIN_KETERANGAN.Text = String.Empty Then
                    txtJAMINAN_PENJAMIN_KETERANGAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    txtJAMINAN_PENJAMIN_KETERANGAN.ErrorText = Statement.ErrorRequired

                    txtJAMINAN_PENJAMIN_KETERANGAN.Focus()
                    fn_Validate = False
                    Exit Function
                End If

                If chkISSUPLESI.Checked = True Then
                    If txtPENJAMIN_SUPLESI_NOSEPSUPLESI.Text = String.Empty Then
                        txtPENJAMIN_SUPLESI_NOSEPSUPLESI.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                        txtPENJAMIN_SUPLESI_NOSEPSUPLESI.ErrorText = Statement.ErrorRequired

                        txtPENJAMIN_SUPLESI_NOSEPSUPLESI.Focus()
                        fn_Validate = False
                        Exit Function
                    End If
                    If txtSUPLESI_PROPINSI.Text = String.Empty Then
                        txtSUPLESI_PROPINSI.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                        txtSUPLESI_PROPINSI.ErrorText = Statement.ErrorRequired

                        txtSUPLESI_KABUPATEN.Focus()
                        fn_Validate = False
                        Exit Function
                    End If
                    If txtSUPLESI_KABUPATEN.Text = String.Empty Then
                        txtSUPLESI_KABUPATEN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                        txtSUPLESI_KABUPATEN.ErrorText = Statement.ErrorRequired

                        txtSUPLESI_KABUPATEN.Focus()
                        fn_Validate = False
                        Exit Function
                    End If
                    If txtSUPLESI_KECAMATAN.Text = String.Empty Then
                        txtSUPLESI_KECAMATAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                        txtSUPLESI_KECAMATAN.ErrorText = Statement.ErrorRequired

                        txtSUPLESI_KECAMATAN.Focus()
                        fn_Validate = False
                        Exit Function
                    End If
                End If
            End If

            'Dim dsSIP

            'Daftar Poli sama
            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                If grdKDDAFTAR_L1.Text = "BPJS" Then
                    Dim oPOLI As New Reference.clsDepartment
                    If oPOLI.GetData(grdKDDEPARTMENT.EditValue).VCLAIM_KODEPOLI <> "IGD" Then
                        Dim dsKunjunganSama = oPendaftaranA.GetDataByRMUnitDate(txtKDCUSTOMER.Text, grdKDDEPARTMENT.EditValue, deDATE.DateTime)
                        If dsKunjunganSama IsNot Nothing Then
                            MsgBox("Pasien hari ini sudah berkunjung ke Poli yg sama dengan No Pendaftaran " & dsKunjunganSama.KDPENDAFTARAN, MsgBoxStyle.Exclamation, Me.Text)

                            'fn_Validate = False
                            'Exit Function
                        End If
                        If chkIsOfline.Checked = False Then
                            If rbCATEGORY.SelectedIndex = 0 Then
                                If txtNOMORRUJUKAN.Text = String.Empty Then
                                    txtNOMORRUJUKAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                                    txtNOMORRUJUKAN.ErrorText = Statement.ErrorRequired

                                    txtNOMORRUJUKAN.Focus()
                                    fn_Validate = False
                                    Exit Function
                                End If
                            End If
                        End If
                    End If
                Else
                    Dim oPOLI As New Reference.clsDepartment
                    If oPOLI.GetData(grdKDDEPARTMENT.EditValue).VCLAIM_KODEPOLI <> "IGD" Then
                        Dim dsKunjunganSama = oPendaftaranA.GetDataByRMUnitDate(txtKDCUSTOMER.Text, grdKDDEPARTMENT.EditValue, deDATE.DateTime)
                        If dsKunjunganSama IsNot Nothing Then
                            If MsgBox("Pasien hari ini sudah berkunjung ke Poli yg sama dengan No Pendaftaran " & dsKunjunganSama.KDPENDAFTARAN & " Apakah Akan lanjut Transaksi ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then
                                fn_Validate = False
                                Exit Function
                            End If

                        End If
                    End If
                End If

                If grdKDDAFTAR_L1.Text = "BPJS" Then
                    If rbCATEGORY.SelectedIndex = 0 Then
                        Dim dsPoliTerakhir = oPendaftaranA.GetDataByRMKunjunganTerkahir(txtKDCUSTOMER.Text)

                        If dsPoliTerakhir IsNot Nothing Then
                            Dim oKunjungan = DateDiff(DateInterval.Day, dsPoliTerakhir.DATE, deDATE.DateTime) + 1

                            If oKunjungan <= 7 Then
                                If MsgBox("Apakah akan dilanjutkan, Tujuan Terkahir : " & dsPoliTerakhir.M_DEPARTMENT.NAME_DISPLAY & " Tanggal " & dsPoliTerakhir.DATE.ToString("dd-MM-yyyy"), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Kunjungan Kurang dari 7 hari : " & oKunjungan) = MsgBoxResult.No Then
                                    fn_Validate = False
                                    Exit Function
                                End If
                            End If

                        End If
                    End If
                End If

            End If

            If txtNOMORTELEPON.Text = "00000000" Then
                txtNOMORTELEPON.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtNOMORTELEPON.ErrorText = Statement.ErrorRequired

                txtNOMORTELEPON.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtNOMORTELEPON.Text = "00000000" Then
                txtNOMORTELEPON.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtNOMORTELEPON.ErrorText = Statement.ErrorRequired

                txtNOMORTELEPON.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtNOMORTELEPON.Text = "000000000" Then
                txtNOMORTELEPON.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtNOMORTELEPON.ErrorText = Statement.ErrorRequired

                txtNOMORTELEPON.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtNOMORTELEPON.Text = "0000000000" Then
                txtNOMORTELEPON.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtNOMORTELEPON.ErrorText = Statement.ErrorRequired

                txtNOMORTELEPON.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtNOMORTELEPON.Text = "00000000000" Then
                txtNOMORTELEPON.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtNOMORTELEPON.ErrorText = Statement.ErrorRequired

                txtNOMORTELEPON.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtNOMORTELEPON.Text = "000000000000" Then
                txtNOMORTELEPON.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtNOMORTELEPON.ErrorText = Statement.ErrorRequired

                txtNOMORTELEPON.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtNOMORTELEPON.Text = "" Then
                txtNOMORTELEPON.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtNOMORTELEPON.ErrorText = Statement.ErrorRequired

                txtNOMORTELEPON.Focus()
                fn_Validate = False
                Exit Function
            End If

            Dim oKelasAplicares As New Reference.clsKelasAplicare

            If rbCATEGORY.SelectedIndex = 1 Then
                If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                    Dim dsRuangan = oPendaftaranA.GetDataByRMDateRawatInap(txtKDCUSTOMER.Text, deDATE.DateTime)

                    If dsRuangan IsNot Nothing Then
                        If MsgBox("Rekam Medis : " & dsRuangan.KDCUSTOMER & " Sudah mendapatkan register rawat inap dengan tanggal yang sama nomor pendaftaran : " & dsRuangan.KDPENDAFTARAN & " Apakah akan melanjutkan?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then
                            fn_Validate = False
                            Exit Function
                        End If

                    End If
                End If

                If txtNAMAPENANGGUNGJAWAB.Text = String.Empty Then
                    tabControl.SelectedTabPage = tab2
                    txtNAMAPENANGGUNGJAWAB.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    txtNAMAPENANGGUNGJAWAB.ErrorText = Statement.ErrorRequired

                    txtNAMAPENANGGUNGJAWAB.Focus()
                    fn_Validate = False
                    Exit Function
                End If
                If txtALAMATPENANGGUNGJAWAB.Text = String.Empty Then
                    txtALAMATPENANGGUNGJAWAB.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    txtALAMATPENANGGUNGJAWAB.ErrorText = Statement.ErrorRequired

                    txtALAMATPENANGGUNGJAWAB.Focus()
                    fn_Validate = False
                    Exit Function
                End If
                If txtNOMORTELEPONPENANGGUNGJAWAB.Text = String.Empty Then
                    txtNOMORTELEPONPENANGGUNGJAWAB.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    txtNOMORTELEPONPENANGGUNGJAWAB.ErrorText = Statement.ErrorRequired

                    txtNOMORTELEPONPENANGGUNGJAWAB.Focus()
                    fn_Validate = False
                    Exit Function
                End If
                'If cboTERSEDIA.Text = String.Empty Then
                '    cboTERSEDIA.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                '    cboTERSEDIA.ErrorText = Statement.ErrorRequired

                '    cboTERSEDIA.Focus()
                '    fn_Validate = False
                '    Exit Function
                'End If

                If grdPEMETAAN.Text = String.Empty Then Exit Function

                Dim dsKelasAplicare = oKelasAplicares.GetDataDetail_UOM(grdPEMETAAN.EditValue)

                If dsKelasAplicare IsNot Nothing Then
                    If dsKelasAplicare.TERSEDIA_LAKIPEREMPUAN > 0 Then
                        If dsKelasAplicare.TERSEDIA_LAKIPEREMPUAN = 0 Then
                            MsgBox(Statement.ErrorStatement & " Ruangan Penuh Untuk Pasien Laki dan Perempuan", MsgBoxStyle.Information, Me.Text)

                            grdPEMETAAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                            grdPEMETAAN.ErrorText = Statement.ErrorRequired

                            grdPEMETAAN.Focus()
                            fn_Validate = False
                            Exit Function

                        End If
                    Else
                        If txtKDJENISKELAMIN.Text = "P" Then
                            If dsKelasAplicare.TERSEDIA_PEREMPUAN < 0 Then
                                MsgBox(Statement.ErrorStatement & " Ruangan Penuh Untuk Pasien Perempuan", MsgBoxStyle.Information, Me.Text)

                                grdPEMETAAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                                grdPEMETAAN.ErrorText = Statement.ErrorRequired

                                grdPEMETAAN.Focus()
                                fn_Validate = False
                                Exit Function

                            End If
                        Else
                            If dsKelasAplicare.TERSEDIA_LAKI < 0 Then
                                MsgBox(Statement.ErrorStatement & " Ruangan Penuh Untuk Pasien Laki-laki", MsgBoxStyle.Information, Me.Text)

                                grdPEMETAAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                                grdPEMETAAN.ErrorText = Statement.ErrorRequired

                                grdPEMETAAN.Focus()
                                fn_Validate = False
                                Exit Function

                            End If
                        End If
                    End If
                End If
            End If

            If rbCATEGORY.SelectedIndex = 1 Then
                If grdPEMETAAN.Text <> "" Then
                    Dim dsDepartmentLast = oKelasAplicares.GetDataDetail_UOM(grdPEMETAAN.EditValue)
                    If dsDepartmentLast IsNot Nothing Then
                        If grdKDDEPARTMENT.EditValue <> dsDepartmentLast.KDDEPARTMENT Then
                            MsgBox(Statement.ErrorStatement & " Pemetaan Tidak sama dengan ruangan yg dipilih", MsgBoxStyle.Information, Me.Text)

                            grdPEMETAAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                            grdPEMETAAN.ErrorText = Statement.ErrorRequired

                            grdPEMETAAN.Focus()
                            fn_Validate = False
                            Exit Function
                        End If
                    End If
                Else
                    MsgBox(Statement.ErrorStatement & " Pemetaan Masih Kosong", MsgBoxStyle.Exclamation, Me.Text)

                    grdPEMETAAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    grdPEMETAAN.ErrorText = Statement.ErrorRequired

                    grdPEMETAAN.Focus()
                    fn_Validate = False
                    Exit Function
                End If
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    'Private Function fn_SaveAutoSKD(ByVal KDPENDAFATRAN As String) As Boolean
    '    Try
    '        Dim oSKD As New Admission.clsSKD
    '        ' ***** HEADER *****
    '        Dim ds = oSKD.GetStructureHeader
    '        With ds
    '            Try
    '                .DATECREATED = oSKD.GetData(sNoId).DATECREATED
    '            Catch oErr As Exception
    '                .DATECREATED = Now
    '            End Try
    '            .DATEUPDATED = Now

    '            .KDSKD = txtNOMORSKDP.Text.ToString.Trim
    '            .KDPENDAFTARAN = KDPENDAFATRAN
    '            .DATE = deDATE.DateTime
    '            .ISCATEGORY = rbCATEGORY.SelectedIndex
    '            .KDDEPARTMENT = grdKDDEPARTMENT.EditValue
    '            .KDDOCTOR = grdKDDOCTOR.EditValue
    '            .NOMORRUJUKAN = txtNOMORRUJUKAN.Text.Trim.ToUpper
    '            .DESCRIPTION = ""
    '            Try
    '                .ISCHEKED = oSKD.GetData(sNoId).ISCHEKED
    '            Catch oErr As Exception
    '                .ISCHEKED = False
    '            End Try
    '            .KDUSER = sUserID
    '            .DATEKONTROL = deDATE.DateTime
    '            .ALASAN = ""
    '            .TINDAKLANJUT = "KONTROL"
    '            .TANGGALPERIKSA_TEXT = deDATE.DateTime.ToString("ddMMyyyy")
    '            Try
    '                .KDJADWALDOKTER = oSKD.GetData(sNoId).KDJADWALDOKTER
    '            Catch oErr As Exception
    '                .KDJADWALDOKTER = ""
    '            End Try
    '            Try
    '                .SEQ = oSKD.GetData(sNoId).SEQ
    '            Catch oErr As Exception
    '                .SEQ = 0
    '            End Try
    '            .REQUEST = ""
    '            .RESPONSE = ""
    '            .NOMORSEP = ""
    '        End With

    '        If oFormMode = FORM_MODE.FORM_MODE_ADD Then
    '            Try
    '                Dim KDSKD As String = oSKD.InsertData(ds, True, "")

    '                If KDSKD <> "" Then
    '                    fn_SaveAutoSKD = True
    '                Else
    '                    fn_SaveAutoSKD = False
    '                End If
    '            Catch oErr As Exception
    '                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '            End Try
    '        ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
    '            Try
    '                fn_SaveAutoSKD = oSKD.UpdateData(ds)
    '            Catch oErr As Exception
    '                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '            End Try
    '        End If
    '    Catch oErr As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '        fn_SaveAutoSKD = False
    '    End Try
    'End Function
    Private Function fn_jsonRequestInsertSEPV1() As String
        Try
            Dim jsonRequest As String = String.Empty
            Dim oFaskes As New Reference.clsPPK
            Dim oPoli As New Reference.clsDepartment
            Dim oDoctor As New Reference.clsDoctor
            Dim jaminanPenjamin As String = String.Empty
            Dim oDepartment As New Reference.clsDepartment
            Dim oKelas As New Reference.clsKelasRawat

            If chkPenjamin1.Checked = True And chkPenjamin2.Checked = False And chkPenjamin3.Checked = False And chkPenjamin4.Checked = False Then
                jaminanPenjamin = "1"
            ElseIf chkPenjamin1.Checked = True And chkPenjamin2.Checked = True And chkPenjamin3.Checked = False And chkPenjamin4.Checked = False Then
                jaminanPenjamin = "1,2"
            ElseIf chkPenjamin1.Checked = True And chkPenjamin2.Checked = True And chkPenjamin3.Checked = True And chkPenjamin4.Checked = False Then
                jaminanPenjamin = "1,2,3"
            ElseIf chkPenjamin1.Checked = True And chkPenjamin2.Checked = True And chkPenjamin3.Checked = True And chkPenjamin4.Checked = True Then
                jaminanPenjamin = "1,2,3,4"
            ElseIf chkPenjamin1.Checked = True And chkPenjamin2.Checked = False And chkPenjamin3.Checked = True And chkPenjamin4.Checked = False Then
                jaminanPenjamin = "1,3"
            ElseIf chkPenjamin1.Checked = True And chkPenjamin2.Checked = False And chkPenjamin3.Checked = False And chkPenjamin4.Checked = True Then
                jaminanPenjamin = "1,4"
            ElseIf chkPenjamin1.Checked = False And chkPenjamin2.Checked = True And chkPenjamin3.Checked = False And chkPenjamin4.Checked = False Then
                jaminanPenjamin = "2"
            ElseIf chkPenjamin1.Checked = False And chkPenjamin2.Checked = True And chkPenjamin3.Checked = True And chkPenjamin4.Checked = False Then
                jaminanPenjamin = "2,3"
            ElseIf chkPenjamin1.Checked = False And chkPenjamin2.Checked = True And chkPenjamin3.Checked = True And chkPenjamin4.Checked = True Then
                jaminanPenjamin = "2,3,4"
            ElseIf chkPenjamin1.Checked = False And chkPenjamin2.Checked = True And chkPenjamin3.Checked = False And chkPenjamin4.Checked = True Then
                jaminanPenjamin = "2,4"
            ElseIf chkPenjamin1.Checked = False And chkPenjamin2.Checked = False And chkPenjamin3.Checked = True And chkPenjamin4.Checked = False Then
                jaminanPenjamin = "3"
            ElseIf chkPenjamin1.Checked = False And chkPenjamin2.Checked = False And chkPenjamin3.Checked = True And chkPenjamin4.Checked = True Then
                jaminanPenjamin = "3,4"
            ElseIf chkPenjamin1.Checked = False And chkPenjamin2.Checked = False And chkPenjamin3.Checked = False And chkPenjamin4.Checked = True Then
                jaminanPenjamin = "4"
            End If

            jsonRequest = " { "
            jsonRequest &= """request"" :  { "
            jsonRequest &= """t_sep"": { "
            jsonRequest &= """noKartu"": """ & txtKARTUBPJS.Text.Trim.ToUpper & ""","
            jsonRequest &= """tglSep"": """ & deDATE.DateTime.ToString("yyyy-MM-dd") & """, "
            jsonRequest &= """ppkPelayanan"": """ & sPPKPELAYANAN & """, "
            jsonRequest &= """jnsPelayanan"": """ & IIf(rbCATEGORY.SelectedIndex = 0, 2, 1) & """, "
            jsonRequest &= """klsRawat"": """ & IIf(rbCATEGORY.SelectedIndex = 0, "3", oKelas.GetData(grdKDKELASRAWAT.EditValue).KODEPENDAFTARAN) & """, "
            jsonRequest &= """noMR"": """ & txtKDCUSTOMER.Text.ToString & """, "
            jsonRequest &= """rujukan"": { "
            jsonRequest &= """asalRujukan"": """ & cboASALRUJUKAN.SelectedIndex + 1 & """, "
            jsonRequest &= """tglRujukan"": """ & deDATE_RUJUKAN.DateTime.ToString("yyyy-MM-dd") & """, "
            jsonRequest &= """noRujukan"": """ & txtNOMORRUJUKAN.Text.ToString.Trim.ToUpper & """, "
            jsonRequest &= """ppkRujukan"": """ & oFaskes.GetData(grdKDPPK.EditValue).KODEFASKES & """ "
            jsonRequest &= "}, "
            jsonRequest &= """catatan"": """ & txtCATATAN.Text.ToString.Trim.ToUpper & """, "
            jsonRequest &= """diagAwal"": """ & grdKDDIAGNOSA.EditValue & """, "
            jsonRequest &= """poli"": { "
            jsonRequest &= """tujuan"": """ & oDepartment.GetData(grdKDDEPARTMENT.EditValue).VCLAIM_KODEPOLI & """, "
            jsonRequest &= """eksekutif"": """ & IIf(chkISEKSEKUTIF.Checked = False, 0, 1) & """ "
            jsonRequest &= "}, "
            jsonRequest &= """cob"": { "
            jsonRequest &= """cob"": """ & IIf(chkCOB.Checked = False, 0, 1) & """ "
            jsonRequest &= "}, "
            jsonRequest &= """katarak"": { "
            jsonRequest &= """katarak"": """ & IIf(chkISKATARAK.Checked = False, 0, 1) & """ "
            jsonRequest &= "}, "
            jsonRequest &= """jaminan"": { "
            If chkLakaLantas.Checked = False Then
                jsonRequest &= """lakaLantas"": """ & 0 & """, "
                jsonRequest &= """penjamin"": { "
                jsonRequest &= """penjamin"": """ & "" & """, "
                jsonRequest &= """tglKejadian"": """ & "" & """, "
                jsonRequest &= """keterangan"": """ & "" & """, "
                jsonRequest &= """suplesi"": { "
                jsonRequest &= """suplesi"": """ & 0 & """, "
                jsonRequest &= """noSepSuplesi"": """ & "" & """, "
                jsonRequest &= """lokasiLaka"": { "
                jsonRequest &= """kdPropinsi"": """ & "" & """, "
                jsonRequest &= """kdKabupaten"": """ & "" & """, "
                jsonRequest &= """kdKecamatan"": """ & "" & """ "
            Else
                jsonRequest &= """lakaLantas"": """ & 1 & """, "
                jsonRequest &= """penjamin"": { "
                jsonRequest &= """penjamin"": """ & jaminanPenjamin & """, "
                jsonRequest &= """tglKejadian"": """ & deDATE_PENJAMIN_TGLKEJADIAN.DateTime.ToString("yyyy-MM-dd") & """, "
                jsonRequest &= """keterangan"": """ & txtJAMINAN_PENJAMIN_KETERANGAN.Text.ToString.Trim.ToUpper & """, "
                jsonRequest &= """suplesi"": { "
                jsonRequest &= """suplesi"": """ & IIf(chkISSUPLESI.Checked = False, 0, 1) & """, "
                jsonRequest &= """noSepSuplesi"": """ & txtPENJAMIN_SUPLESI_NOSEPSUPLESI.Text.ToString.Trim.ToUpper & """, "
                jsonRequest &= """lokasiLaka"": { "
                jsonRequest &= """kdPropinsi"": """ & txtSUPLESI_PROPINSI.Text & """, "
                jsonRequest &= """kdKabupaten"": """ & txtSUPLESI_KABUPATEN.Text & """, "
                jsonRequest &= """kdKecamatan"": """ & txtSUPLESI_KECAMATAN.Text & """ "
            End If
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "}, "
            jsonRequest &= """skdp"": { "
            'jsonRequest &= """noSurat"": """ & IIf(txtNOMORSKDP.Text = String.Empty, "", Microsoft.VisualBasic.Right(txtNOMORSKDP.Text, 6)) & """, "
            jsonRequest &= """noSurat"": """ & txtNOMORSKDP.Text & """, "
            jsonRequest &= """kodeDPJP"": """ & oDoctor.GetData(grdKDDOCTOR.EditValue).VCLAIM_KDDPJP & """ "
            jsonRequest &= "}, "
            jsonRequest &= """noTelp"": """ & txtNOMORTELEPON.Text.ToString.Trim.ToUpper & """, "
            jsonRequest &= """user"": """ & sUserID & """ "
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "}  "

            fn_jsonRequestInsertSEPV1 = jsonRequest
        Catch oErr As Exception
            fn_jsonRequestInsertSEPV1 = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_jsonRequestUpdateSEPV1() As String
        Try
            Dim jsonRequest As String = String.Empty
            Dim oFaskes As New Reference.clsPPK
            Dim oPoli As New Reference.clsDepartment
            Dim oDoctor As New Reference.clsDoctor
            Dim jaminanPenjamin As String = String.Empty
            Dim oKelas As New Reference.clsKelasRawat

            If chkPenjamin1.Checked = True And chkPenjamin2.Checked = False And chkPenjamin3.Checked = False And chkPenjamin4.Checked = False Then
                jaminanPenjamin = "1"
            ElseIf chkPenjamin1.Checked = True And chkPenjamin2.Checked = True And chkPenjamin3.Checked = False And chkPenjamin4.Checked = False Then
                jaminanPenjamin = "1,2"
            ElseIf chkPenjamin1.Checked = True And chkPenjamin2.Checked = True And chkPenjamin3.Checked = True And chkPenjamin4.Checked = False Then
                jaminanPenjamin = "1,2,3"
            ElseIf chkPenjamin1.Checked = True And chkPenjamin2.Checked = True And chkPenjamin3.Checked = True And chkPenjamin4.Checked = True Then
                jaminanPenjamin = "1,2,3,4"
            ElseIf chkPenjamin1.Checked = True And chkPenjamin2.Checked = False And chkPenjamin3.Checked = True And chkPenjamin4.Checked = False Then
                jaminanPenjamin = "1,3"
            ElseIf chkPenjamin1.Checked = True And chkPenjamin2.Checked = False And chkPenjamin3.Checked = False And chkPenjamin4.Checked = True Then
                jaminanPenjamin = "1,4"
            ElseIf chkPenjamin1.Checked = False And chkPenjamin2.Checked = True And chkPenjamin3.Checked = False And chkPenjamin4.Checked = False Then
                jaminanPenjamin = "2"
            ElseIf chkPenjamin1.Checked = False And chkPenjamin2.Checked = True And chkPenjamin3.Checked = True And chkPenjamin4.Checked = False Then
                jaminanPenjamin = "2,3"
            ElseIf chkPenjamin1.Checked = False And chkPenjamin2.Checked = True And chkPenjamin3.Checked = True And chkPenjamin4.Checked = True Then
                jaminanPenjamin = "2,3,4"
            ElseIf chkPenjamin1.Checked = False And chkPenjamin2.Checked = True And chkPenjamin3.Checked = False And chkPenjamin4.Checked = True Then
                jaminanPenjamin = "2,4"
            ElseIf chkPenjamin1.Checked = False And chkPenjamin2.Checked = False And chkPenjamin3.Checked = True And chkPenjamin4.Checked = False Then
                jaminanPenjamin = "3"
            ElseIf chkPenjamin1.Checked = False And chkPenjamin2.Checked = False And chkPenjamin3.Checked = True And chkPenjamin4.Checked = True Then
                jaminanPenjamin = "3,4"
            ElseIf chkPenjamin1.Checked = False And chkPenjamin2.Checked = False And chkPenjamin3.Checked = False And chkPenjamin4.Checked = True Then
                jaminanPenjamin = "4"
            End If

            jsonRequest = " { "
            jsonRequest &= """request"": { "
            jsonRequest &= """t_sep"": { "
            jsonRequest &= """noSep"": """ & txtNOMORSEP.Text & """, "
            jsonRequest &= """klsRawat"": """ & IIf(rbCATEGORY.SelectedIndex = 0, "3", oKelas.GetData(grdKDKELASRAWAT.EditValue).KODEPENDAFTARAN) & """, "
            jsonRequest &= """noMR"": """ & txtKDCUSTOMER.Text.ToString & """, "
            jsonRequest &= """rujukan"":{ "
            jsonRequest &= """asalRujukan"":""" & cboASALRUJUKAN.SelectedIndex + 1 & """, "
            jsonRequest &= """tglRujukan"":""" & deDATE_RUJUKAN.DateTime.ToString("yyyy-MM-dd") & """, "
            jsonRequest &= """noRujukan"":""" & txtNOMORRUJUKAN.Text.ToString.Trim.ToUpper & """, "
            jsonRequest &= """ppkRujukan"":""" & oFaskes.GetData(grdKDPPK.EditValue).KODEFASKES & """ "
            jsonRequest &= "}, "
            jsonRequest &= """catatan"": """ & txtCATATAN.Text.ToString.Trim.ToUpper & """, "
            jsonRequest &= """diagAwal"": """ & grdKDDIAGNOSA.EditValue & """, "
            jsonRequest &= """poli"" :  { "
            jsonRequest &= """eksekutif"": """ & IIf(chkISEKSEKUTIF.Checked = False, 0, 1) & """ "
            jsonRequest &= "}, "
            jsonRequest &= """cob"": { "
            jsonRequest &= """cob"": """ & IIf(chkCOB.Checked = False, 0, 1) & """ "
            jsonRequest &= "}, "
            jsonRequest &= """katarak"":{ "
            jsonRequest &= """katarak"":""" & IIf(chkISKATARAK.Checked = False, 0, 1) & """ "
            jsonRequest &= "}, "
            jsonRequest &= """skdp"":{ "
            jsonRequest &= """noSurat"":""" & IIf(txtNOMORSKDP.Text = String.Empty, "", Microsoft.VisualBasic.Right(txtNOMORSKDP.Text, 6)) & """, "
            jsonRequest &= """kodeDPJP"":""" & oDoctor.GetData(grdKDDOCTOR.EditValue).VCLAIM_KDDPJP & """ "
            jsonRequest &= "}, "
            jsonRequest &= """jaminan"": { "
            If chkLakaLantas.Checked = False Then
                jsonRequest &= """lakaLantas"": """ & 0 & """, "
                jsonRequest &= """penjamin"": { "
                jsonRequest &= """penjamin"": """ & "" & """, "
                jsonRequest &= """tglKejadian"": """ & "" & """, "
                jsonRequest &= """keterangan"": """ & "" & """, "
                jsonRequest &= """suplesi"": { "
                jsonRequest &= """suplesi"": """ & 0 & """, "
                jsonRequest &= """noSepSuplesi"": """ & "" & """, "
                jsonRequest &= """lokasiLaka"": { "
                jsonRequest &= """kdPropinsi"": """ & "" & """, "
                jsonRequest &= """kdKabupaten"": """ & "" & """, "
                jsonRequest &= """kdKecamatan"": """ & "" & """ "
            Else
                jsonRequest &= """lakaLantas"": """ & 1 & """, "
                jsonRequest &= """penjamin"": { "
                jsonRequest &= """penjamin"": """ & jaminanPenjamin & """, "
                jsonRequest &= """tglKejadian"": """ & deDATE_PENJAMIN_TGLKEJADIAN.DateTime.ToString("yyyy-MM-dd") & """, "
                jsonRequest &= """keterangan"": """ & txtJAMINAN_PENJAMIN_KETERANGAN.Text.ToString.Trim.ToUpper & """, "
                jsonRequest &= """suplesi"": { "
                jsonRequest &= """suplesi"": """ & IIf(chkISSUPLESI.Checked = False, 0, 1) & """, "
                jsonRequest &= """noSepSuplesi"": """ & txtPENJAMIN_SUPLESI_NOSEPSUPLESI.Text.ToString.Trim.ToUpper & """, "
                jsonRequest &= """lokasiLaka"": { "
                jsonRequest &= """kdPropinsi"": """ & txtSUPLESI_PROPINSI.Text & """, "
                jsonRequest &= """kdKabupaten"": """ & txtSUPLESI_KABUPATEN.Text & """, "
                jsonRequest &= """kdKecamatan"": """ & txtSUPLESI_KECAMATAN.Text & """ "
            End If

            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "}, "
            jsonRequest &= """noTelp"": """ & txtNOMORTELEPON.Text.ToString.Trim.ToUpper & """, "
            jsonRequest &= """user"": """ & sUserID & """ "
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "} "

            fn_jsonRequestUpdateSEPV1 = jsonRequest
        Catch oErr As Exception
            fn_jsonRequestUpdateSEPV1 = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_jsonRequestInsertSEPV2() As String
        Try
            Dim jsonRequest As String = String.Empty
            Dim oFaskes As New Reference.clsPPK
            Dim oPoli As New Reference.clsDepartment
            Dim oDoctor As New Reference.clsDoctor
            Dim oDepartment As New Reference.clsDepartment
            Dim oKelas As New Reference.clsKelasRawat

            If oDoctor.GetData(grdKDDOCTOR.EditValue).VCLAIM_KDDPJP = "" Then
                MsgBox("Kode Dokter Vclaim Masih Kosong, Silahkan Update Master Dokter", MsgBoxStyle.Exclamation, Me.Text)
                fn_jsonRequestInsertSEPV2 = ""
            End If

            Dim NaikKelas As String = String.Empty
            Dim dsNaikkelas = oKelas.GetData(grdNAIKKELAS.EditValue)
            If dsNaikkelas IsNot Nothing Then
                NaikKelas = dsNaikkelas.KODEPENDAFTARAN
            End If

            jsonRequest = " { "
            jsonRequest &= """request"" :  { "
            jsonRequest &= """t_sep"": { "
            jsonRequest &= """noKartu"": """ & txtKARTUBPJS.Text.Trim.ToUpper & ""","
            jsonRequest &= """tglSep"": """ & deDATE.DateTime.ToString("yyyy-MM-dd") & """, "
            jsonRequest &= """ppkPelayanan"": """ & sPPKPELAYANAN & """, "
            jsonRequest &= """jnsPelayanan"": """ & IIf(rbCATEGORY.SelectedIndex = 0, 2, 1) & """, "
            jsonRequest &= """klsRawat"": { "
            jsonRequest &= """klsRawatHak"": """ & IIf(rbCATEGORY.SelectedIndex = 0, "3", oKelas.GetData(grdKDKELASRAWAT.EditValue).KODEPENDAFTARAN) & """, "
            jsonRequest &= """klsRawatNaik"": """ & NaikKelas & """, "
            jsonRequest &= """pembiayaan"": """ & IIf(NaikKelas = "", "", cboPEMBIAYAAN.SelectedIndex + 1) & """, "
            jsonRequest &= """penanggungJawab"": """ & IIf(NaikKelas = "", "", txtPENANGGUNGJAWAB.Text) & """ "
            jsonRequest &= "}, "
            jsonRequest &= """noMR"": """ & txtKDCUSTOMER.Text.ToString & """, "
            jsonRequest &= """rujukan"": { "
            jsonRequest &= """asalRujukan"": """ & cboASALRUJUKAN.SelectedIndex + 1 & """, "
            jsonRequest &= """tglRujukan"": """ & deDATE_RUJUKAN.DateTime.ToString("yyyy-MM-dd") & """, "
            jsonRequest &= """noRujukan"": """ & txtNOMORRUJUKAN.Text.ToString.Trim.ToUpper & """, "
            jsonRequest &= """ppkRujukan"": """ & oFaskes.GetData(grdKDPPK.EditValue).KODEFASKES & """ "
            jsonRequest &= "}, "
            jsonRequest &= """catatan"": """ & txtCATATAN.Text.ToString.Trim.ToUpper & """, "
            jsonRequest &= """diagAwal"": """ & grdKDDIAGNOSA.EditValue & """, "
            jsonRequest &= """poli"": { "
            jsonRequest &= """tujuan"": """ & IIf(rbCATEGORY.SelectedIndex = 0, oDepartment.GetData(grdKDDEPARTMENT.EditValue).VCLAIM_KODEPOLI, "") & """, "
            jsonRequest &= """eksekutif"": """ & IIf(chkISEKSEKUTIF.Checked = False, 0, 1) & """ "
            jsonRequest &= "}, "
            jsonRequest &= """cob"": { "
            jsonRequest &= """cob"": """ & IIf(chkCOB.Checked = False, 0, 1) & """ "
            jsonRequest &= "}, "
            jsonRequest &= """katarak"": { "
            jsonRequest &= """katarak"": """ & IIf(chkISKATARAK.Checked = False, 0, 1) & """ "
            jsonRequest &= "}, "
            jsonRequest &= """jaminan"": { "
            If chkLakaLantas.Checked = False Then
                jsonRequest &= """lakaLantas"": """ & 0 & """, "
                jsonRequest &= """penjamin"": { "
                jsonRequest &= """tglKejadian"": """ & "" & """, "
                jsonRequest &= """keterangan"": """ & "" & """, "
                jsonRequest &= """suplesi"": { "
                jsonRequest &= """suplesi"": """ & 0 & """, "
                jsonRequest &= """noSepSuplesi"": """ & "" & """, "
                jsonRequest &= """lokasiLaka"": { "
                jsonRequest &= """kdPropinsi"": """ & "" & """, "
                jsonRequest &= """kdKabupaten"": """ & "" & """, "
                jsonRequest &= """kdKecamatan"": """ & "" & """ "
            Else
                jsonRequest &= """lakaLantas"": """ & 1 & """, "
                jsonRequest &= """penjamin"": { "
                jsonRequest &= """tglKejadian"": """ & deDATE_PENJAMIN_TGLKEJADIAN.DateTime.ToString("yyyy-MM-dd") & """, "
                jsonRequest &= """keterangan"": """ & txtJAMINAN_PENJAMIN_KETERANGAN.Text.ToString.Trim.ToUpper & """, "
                jsonRequest &= """suplesi"": { "
                jsonRequest &= """suplesi"": """ & IIf(chkISSUPLESI.Checked = False, 0, 1) & """, "
                jsonRequest &= """noSepSuplesi"": """ & txtPENJAMIN_SUPLESI_NOSEPSUPLESI.Text.ToString.Trim.ToUpper & """, "
                jsonRequest &= """lokasiLaka"": { "
                jsonRequest &= """kdPropinsi"": """ & txtSUPLESI_PROPINSI.Text & """, "
                jsonRequest &= """kdKabupaten"": """ & txtSUPLESI_KABUPATEN.Text & """, "
                jsonRequest &= """kdKecamatan"": """ & txtSUPLESI_KECAMATAN.Text & """ "
            End If
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "}, "
            jsonRequest &= """tujuanKunj"": """ & IIf(cboTUJUANKUNJUNGAN.Text = "", "", cboTUJUANKUNJUNGAN.SelectedIndex) & """, "
            jsonRequest &= """flagProcedure"": """ & IIf(cboFLAGPROCEDURE.Text = "", "", cboFLAGPROCEDURE.SelectedIndex) & """, "
            jsonRequest &= """kdPenunjang"": """ & IIf(cboKODEKUNJUNGAN.Text = "", "", cboKODEKUNJUNGAN.SelectedIndex + 1) & """, "
            jsonRequest &= """assesmentPel"": """ & IIf(cboASESMENPELAYANAN.Text = "", "", cboASESMENPELAYANAN.SelectedIndex + 1) & """, "
            jsonRequest &= """skdp"": { "
            jsonRequest &= """noSurat"": """ & txtNOMORSKDP.Text & """, "
            jsonRequest &= """kodeDPJP"": """ & oDoctor.GetData(grdKDDOCTOR.EditValue).VCLAIM_KDDPJP & """ "
            jsonRequest &= "}, "
            jsonRequest &= """dpjpLayan"": """ & IIf(rbCATEGORY.SelectedIndex = 0, oDoctor.GetData(grdKDDOCTOR.EditValue).VCLAIM_KDDPJP, "") & """, "
            jsonRequest &= """noTelp"": """ & txtNOMORTELEPON.Text.ToString.Trim.ToUpper & """, "
            jsonRequest &= """user"": """ & sUserID & """ "
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "}  "
            fn_jsonRequestInsertSEPV2 = jsonRequest
        Catch oErr As Exception
            fn_jsonRequestInsertSEPV2 = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_jsonRequestUpdateSEPV2() As String
        Try
            Dim jsonRequest As String = String.Empty
            Dim oFaskes As New Reference.clsPPK
            Dim oPoli As New Reference.clsDepartment
            Dim oDoctor As New Reference.clsDoctor
            Dim oDepartment As New Reference.clsDepartment
            Dim oKelas As New Reference.clsKelasRawat

            Dim NaikKelas As String = String.Empty
            Dim dsNaikkelas = oKelas.GetData(grdNAIKKELAS.EditValue)
            If dsNaikkelas IsNot Nothing Then
                NaikKelas = dsNaikkelas.KODEPENDAFTARAN
            End If

            jsonRequest = " { "
            jsonRequest &= """request"": { "
            jsonRequest &= """t_sep"": { "
            jsonRequest &= """noSep"": """ & txtNOMORSEP.Text & """, "
            jsonRequest &= """klsRawat"": { "
            jsonRequest &= """klsRawatHak"": """ & IIf(rbCATEGORY.SelectedIndex = 0, "3", oKelas.GetData(grdKDKELASRAWAT.EditValue).KODEPENDAFTARAN) & """, "
            jsonRequest &= """klsRawatNaik"": """ & NaikKelas & """, "
            jsonRequest &= """pembiayaan"": """ & IIf(NaikKelas = "", "", cboPEMBIAYAAN.SelectedIndex + 1) & """, "
            jsonRequest &= """penanggungJawab"": """ & IIf(NaikKelas = "", "", txtPENANGGUNGJAWAB.Text) & """ "
            jsonRequest &= "}, "
            jsonRequest &= """noMR"": """ & txtKDCUSTOMER.Text.ToString & """, "
            jsonRequest &= """catatan"": """ & txtCATATAN.Text.ToString.Trim.ToUpper & """, "
            jsonRequest &= """diagAwal"": """ & grdKDDIAGNOSA.EditValue & """, "
            jsonRequest &= """poli"" :  { "
            jsonRequest &= """tujuan"": """ & oDepartment.GetData(grdKDDEPARTMENT.EditValue).VCLAIM_KODEPOLI & """, "
            jsonRequest &= """eksekutif"": """ & IIf(chkISEKSEKUTIF.Checked = False, 0, 1) & """ "
            jsonRequest &= "}, "
            jsonRequest &= """cob"": { "
            jsonRequest &= """cob"": """ & IIf(chkCOB.Checked = False, 0, 1) & """ "
            jsonRequest &= "}, "
            jsonRequest &= """katarak"":{ "
            jsonRequest &= """katarak"":""" & IIf(chkISKATARAK.Checked = False, 0, 1) & """ "
            jsonRequest &= "}, "
            jsonRequest &= """jaminan"": { "
            If chkLakaLantas.Checked = False Then
                jsonRequest &= """lakaLantas"": """ & 0 & """, "
                jsonRequest &= """penjamin"": { "
                jsonRequest &= """tglKejadian"": """ & "" & """, "
                jsonRequest &= """keterangan"": """ & "" & """, "
                jsonRequest &= """suplesi"": { "
                jsonRequest &= """suplesi"": """ & 0 & """, "
                jsonRequest &= """noSepSuplesi"": """ & "" & """, "
                jsonRequest &= """lokasiLaka"": { "
                jsonRequest &= """kdPropinsi"": """ & "" & """, "
                jsonRequest &= """kdKabupaten"": """ & "" & """, "
                jsonRequest &= """kdKecamatan"": """ & "" & """ "
            Else
                jsonRequest &= """lakaLantas"": """ & 1 & """, "
                jsonRequest &= """penjamin"": { "
                jsonRequest &= """tglKejadian"": """ & deDATE_PENJAMIN_TGLKEJADIAN.DateTime.ToString("yyyy-MM-dd") & """, "
                jsonRequest &= """keterangan"": """ & txtJAMINAN_PENJAMIN_KETERANGAN.Text.ToString.Trim.ToUpper & """, "
                jsonRequest &= """suplesi"": { "
                jsonRequest &= """suplesi"": """ & IIf(chkISSUPLESI.Checked = False, 0, 1) & """, "
                jsonRequest &= """noSepSuplesi"": """ & txtPENJAMIN_SUPLESI_NOSEPSUPLESI.Text.ToString.Trim.ToUpper & """, "
                jsonRequest &= """lokasiLaka"": { "
                jsonRequest &= """kdPropinsi"": """ & txtSUPLESI_PROPINSI.Text & """, "
                jsonRequest &= """kdKabupaten"": """ & txtSUPLESI_KABUPATEN.Text & """, "
                jsonRequest &= """kdKecamatan"": """ & txtSUPLESI_KECAMATAN.Text & """ "
            End If
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "}, "
            'jsonRequest &= """dpjpLayan"": """ & IIf(rbCATEGORY.SelectedIndex = 0, oDoctor.GetData(grdKDDOCTOR.EditValue).VCLAIM_KDDPJP, "") & """, "
            jsonRequest &= """dpjpLayan"": """ & IIf(rbCATEGORY.SelectedIndex = 0, oDoctor.GetData(grdKDDOCTOR.EditValue).VCLAIM_KDDPJP, "") & """, "
            jsonRequest &= """noTelp"": """ & txtNOMORTELEPON.Text.ToString.Trim.ToUpper & """, "
            jsonRequest &= """user"": """ & sUserID & """ "
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "} "


            fn_jsonRequestUpdateSEPV2 = jsonRequest
        Catch oErr As Exception
            fn_jsonRequestUpdateSEPV2 = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_UpdateSEP(ByVal Requset As String, ByVal uTime As Integer) As String
        Try
            If chkIsOfline.Checked = True Then
                MsgBox("Ofline di ceklis, silahkan buka ceklis untuk melanjutkan", MsgBoxStyle.Exclamation, Me.Text)
                fn_UpdateSEP = ""
                Exit Function
            End If

            If sVclaim_ConsId <> "" Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.UpdateSEP(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, Requset)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        fn_UpdateSEP = allData("response")
                    Else
                        fn_UpdateSEP = ""
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    fn_UpdateSEP = ""
                    MsgBox("Update SEP Data Gagal/Kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_UpdateSEP = ""
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            fn_UpdateSEP = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_CariSEP(ByVal NOMORSEP As String, ByVal Pesan As Boolean) As Boolean
        Try
            If NOMORSEP = String.Empty Then
                If chkIsOfline.Checked = True Then
                    fn_CariSEP = True
                    Exit Function
                Else
                    MsgBox("Merupakan Pasien BPJS Silahkan Masukan Nomor SEP", MsgBoxStyle.Exclamation, Me.Text)
                    fn_CariSEP = False
                    Exit Function
                End If
            End If

            Dim uTime As Integer = 0

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Processing data cari SEP.....")

            If sVclaim_ConsId <> "" Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.CariSEP(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, NOMORSEP)

                'Dim dsSetKoneksi = oSetKoneksi.CariSEP("https://apijkn-dev.bpjs-kesehatan.go.id/vclaim-rest-dev/", "13973", "rsdust1r4", "6d8412dbc9eb816119015a4676b900ba", uTime, NOMORSEP)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        fn_CariSEP = True

                        If Pesan = True Then
                            Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), sVclaim_ConsId & sVclaim_SecreatKey & uTime))

                            'Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), "13973" & "rsdust1r4" & uTime))

                            'Dim jsonString As String = "{""name"":""John"", ""age"":30, ""city"":""New York""}"

                            '' Parse JSON string into JObject
                            'Dim jsonObject As JObject = JObject.Parse(jsonString)

                            '' Convert JObject back to formatted JSON string
                            'Dim formattedJson As String = jsonObject.ToString()
                            SplashScreenManager.CloseForm(False)
                            MsgBox(DataDecrypt.ToString(), MsgBoxStyle.Exclamation, Me.Text)
                        End If

                    Else
                        fn_CariSEP = False
                        SplashScreenManager.CloseForm(False)
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    fn_CariSEP = False
                    SplashScreenManager.CloseForm(False)
                    MsgBox("Insert SEP Data Gagal", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_CariSEP = False
                SplashScreenManager.CloseForm(False)
                MsgBox("Cari SEP Data Gagal/Kosong", MsgBoxStyle.Exclamation, Me.Text)
            End If

            SplashScreenManager.CloseForm(False)

        Catch oErr As Exception
            fn_CariSEP = False
            SplashScreenManager.CloseForm(False)
            MsgBox("Pencarian SEP" & vbCrLf & "-" & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_CreateSEPv2(ByVal Request As String, ByVal uTime As Integer) As String
        Dim setKoneksi As String = String.Empty

        Try
            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Processing data create SEP.....")

            If sVclaim_ConsId <> "" Then
                Dim DATETEIM As DateTime = Now

                Dim TanggalEstimasi As DateTime = Now

                Dim dsSetKoneksi = oSetKoneksi.InsertSEPv2(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, Request)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        fn_CreateSEPv2 = allData("response")
                    Else
                        fn_CreateSEPv2 = ""
                        SplashScreenManager.CloseForm(False)
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                        Exit Function
                    End If
                Else
                    fn_CreateSEPv2 = ""
                    SplashScreenManager.CloseForm(False)
                    MsgBox("Insert SEP Data Gagal/Kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_CreateSEPv2 = ""
                SplashScreenManager.CloseForm(False)
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            fn_CreateSEPv2 = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & setKoneksi & "-" & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_UpdateSEPv2(ByVal Request As String, ByVal uTime As Integer) As String
        Try
            If chkIsOfline.Checked = True Then
                MsgBox("Ofline di ceklis, silahkan buka ceklis untuk melanjutkan", MsgBoxStyle.Exclamation, Me.Text)
                fn_UpdateSEPv2 = ""
                Exit Function
            End If

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Processing data update SEP.....")

            If sVclaim_ConsId <> "" Then
                Dim dsSetKoneksi = oSetKoneksi.UpdateSEPv2(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, Request)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        fn_UpdateSEPv2 = allData("response")
                    Else
                        fn_UpdateSEPv2 = ""
                        SplashScreenManager.CloseForm(False)
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                        Exit Function
                    End If
                Else
                    fn_UpdateSEPv2 = ""
                    SplashScreenManager.CloseForm(False)
                    MsgBox("Update SEP Data Gagal/Kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_UpdateSEPv2 = ""
                SplashScreenManager.CloseForm(False)
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            fn_UpdateSEPv2 = ""
            SplashScreenManager.CloseForm(False)
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            Dim oCustomer As New Reference.clsCustomer
            Dim jsonRequest As String = String.Empty
            Dim jsonResponse As String = String.Empty
            Dim INFORMASIPRB As String = String.Empty
            Dim Pasienbaru As Boolean = False

            Dim dsCustomer = oCustomer.GetData(txtKDCUSTOMER.Text)

            If dsCustomer Is Nothing Then
                MsgBox("Pasien Tidak diTemukan di SIMRS", MsgBoxStyle.Exclamation, Me.Text)
                fn_Save = False
                Exit Function
            Else
                If dsCustomer.KDCUSTOMER_LAMA = "" Then
                    If dsCustomer.DATECREATED.ToString("yyyyMMdd") = deDATE.DateTime.ToString("yyyyMMdd") Then
                        Pasienbaru = True
                    End If
                End If

                If dsCustomer.KDJENISKELAMIN = 0 Then

                ElseIf dsCustomer.KDJENISKELAMIN = 1 Then

                Else
                    fn_Save = False
                    MsgBox("Silahkan Perbaiki Data Pasien Terlebih dahulu", MsgBoxStyle.Exclamation, Me.Text)
                    Exit Function
                End If
            End If

            ' ***** SAVE SEP V2
            If rbCATEGORY.SelectedIndex = 0 Then
                Try
                    Dim oSet_Antrian_Simpan As New SettingAntrian.clsSetAntrian
                    Dim oDepartment As New Reference.clsDepartment

                    Dim dsDepartment = oDepartment.GetData(grdKDDEPARTMENT.EditValue)
                    If dsDepartment.VCLAIM_KODEPOLI <> "IGD" Then
                        If chkAntrian.Checked = False Then
                            If txtKDBOOKING.Text <> "" Then
                                If dsCustomer.DATECREATED.ToString("yyyyMMdd") <> Now.ToString("yyyyMMdd") Then
                                    'Pasien Lama
                                    If fn_UpdateBookingAntrian(txtKDBOOKING.Text, 0, 3) = False Then
                                        MsgBox("Update Kode Booking Gagal", MsgBoxStyle.Critical, Me.Text)
                                        fn_Save = False
                                        Exit Function
                                    End If
                                Else
                                    If fn_UpdateBookingAntrian(txtKDBOOKING.Text, 1, 3) = False Then
                                        MsgBox("Update Kode Booking Gagal", MsgBoxStyle.Critical, Me.Text)
                                        fn_Save = False
                                        Exit Function
                                    End If
                                End If

                                Dim HASIL As String = fn_AntrianOnsiteBPJS(txtKDBOOKING.Text)

                                If Not HASIL.Contains("200") Then
                                    If HASIL.Contains("208 - Terdapat duplikasi Kode Booking") Then
                                        frmErmList.fn_TaskID(txtKDBOOKING.Text, 3)
                                    Else
                                        MsgBox("Kode Booking Gagal" & vbCrLf & HASIL, MsgBoxStyle.Exclamation, Me.Text)
                                        Exit Function
                                        fn_Save = False
                                    End If
                                Else
                                    frmErmList.fn_TaskID(txtKDBOOKING.Text, 3)
                                    'oSet_Antrian_Simpan.UpdateDataIsCheked(txtKDBOOKING.Text, 3, HASIL)
                                End If
                            Else
                                MsgBox("Kode Booking Kosong !!!", MsgBoxStyle.Critical, Me.Text)
                                Exit Function
                                fn_Save = False
                            End If
                        Else
                            If txtKDBOOKING.Text <> "" Then
                                frmErmList.fn_TaskID(txtKDBOOKING.Text, 3)
                                oSet_Antrian_Simpan.UpdateDataIsCheked(txtKDBOOKING.Text, 3, "")
                            End If
                        End If
                    Else
                        txtANTRIANPOLI.Text = "-"
                    End If
                Catch oErr As Exception
                    MsgBox("Tambah Kode Booking / Antrian Ke BPJS" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    Exit Function
                    fn_Save = False
                End Try
            Else
                txtANTRIANPOLI.Text = "-"
            End If

            If chkIsOfline.Checked = False Then
                Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                    jsonRequest = fn_jsonRequestInsertSEPV2()

                    If jsonRequest <> "" Then
                        jsonResponse = fn_CreateSEPv2(jsonRequest, uTime)
                        If jsonResponse <> "" Then
                            Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(jsonResponse, sVclaim_ConsId & sVclaim_SecreatKey & uTime))

                            Try
                                jsonResponse = DataDecrypt.ToString()
                            Catch ex As Exception

                            End Try

                            txtNOMORSEP.Text = DataDecrypt.Item("sep")("noSep").ToString()
                            Dim dsSEP = oPendaftaranA.GetDataNomorSEP(txtNOMORSEP.Text)
                            If dsSEP IsNot Nothing Then
                                MsgBox("Rujukan Internal" & vbCrLf & "Sep Sudah Ada pada tanggal " & dsSEP.DATE.ToString("dd-MM-yyyy"), MsgBoxStyle.Exclamation, Me.Text)
                                txtNOMORSEP.ResetText()
                            End If
                            INFORMASIPRB = DataDecrypt.Item("sep")("informasi")("prolanisPRB").ToString()
                        Else
                            fn_Save = False
                            Exit Function
                        End If
                    Else
                        fn_Save = False
                        Exit Function
                    End If
                Else
                    jsonRequest = fn_jsonRequestUpdateSEPV2()
                    If jsonRequest <> "" Then
                        jsonResponse = fn_UpdateSEPv2(jsonRequest, uTime)
                        If jsonResponse <> "" Then
                            'Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM2")
                            'Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(jsonResponse, dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime))
                            'txtNOMORSEP.Text = DataDecrypt.Item("sep")("noSep").ToString()
                            'txtINFORMASIPRB.Text = DataDecrypt.Item("sep")("informasi")("prolanisPRB").ToString()
                        Else
                            fn_Save = False
                            Exit Function
                        End If
                    Else
                        fn_Save = False
                        Exit Function
                    End If
                End If
            Else
                If txtNOMORSEP.Text = "<--- AUTO --->" Then
                    txtNOMORSEP.ResetText()
                Else
                    If grdKDDAFTAR_L1.Text = "BPJS" Then
                        If fn_CariSEP(txtNOMORSEP.Text.Trim.ToUpper, False) = False Then
                            fn_Save = False
                            Exit Function
                        End If
                    End If
                End If
            End If

            ' ****************************************************

            ' ***** HEADER *****
            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Processing data Simpan SIMRS.....")

            Dim ds = oPendaftaranA.GetStructureHeader
            With ds
                .KDPENDAFTARAN = sNoId
                .KDPENDAFTARAN_AWAL = IIf(rbCATEGORY.SelectedIndex = 0, "", txtKDPENDAFTARAN_AWAL.Text.ToString.Trim.ToUpper)
                .NOMORSEP = IIf(txtNOMORSEP.Text = "<--- AUTO --->", "", txtNOMORSEP.Text.ToString.Trim.ToUpper)
                Try
                    .DATECREATED = oPendaftaranA.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .KDCUSTOMER = txtKDCUSTOMER.Text.ToString.Trim.ToUpper
                .KARTUBPJS = txtKARTUBPJS.Text.ToString.Trim.ToUpper
                .KTP = txtKTP.Text.ToString.Trim.ToUpper
                .KDDAFTAR_L1 = grdKDDAFTAR_L1.EditValue
                .KDDAFTAR_L2 = grdKDDAFTAR_L2.EditValue
                .KDDAFTAR_L3 = grdKDDAFTAR_L3.EditValue
                .KDDAFTAR_L4 = grdKDDAFTAR_L4.EditValue
                .KDDAFTAR_L5 = grdKDDAFTAR_L5.EditValue
                Try
                    .KDDAFTAR_L6 = oPendaftaranA.GetData(sNoId).KDDAFTAR_L6
                Catch oErr As Exception
                    .KDDAFTAR_L6 = "DAFTAR_L6_0000000001"
                End Try
                .CATEGORY = rbCATEGORY.SelectedIndex
                Try
                    .STATUSDAFTAR = oPendaftaranA.GetData(sNoId).STATUSDAFTAR
                Catch oErr As Exception
                    .STATUSDAFTAR = 0
                End Try
                .KDUSER = sUserID
                .ISEKSEKUTIF = chkISEKSEKUTIF.Checked
                .ISKATARAK = chkISKATARAK.Checked
                .KDDEPARTMENT = grdKDDEPARTMENT.EditValue
                .KDDOCTOR = grdKDDOCTOR.EditValue
                .ASALRUJUKAN = cboASALRUJUKAN.SelectedIndex
                .DATE_RUJUKAN = deDATE_RUJUKAN.DateTime
                .NOMORRUJUKAN = txtNOMORRUJUKAN.Text.ToString.Trim.ToUpper
                .NOMORSKDP = txtNOMORSKDP.Text
                .KDDOCTOR_SKD = IIf(grdKDDOCTOR_SKD.Text = "", grdKDDOCTOR.EditValue, grdKDDOCTOR_SKD.EditValue)
                .KDDIAGNOSA = grdKDDIAGNOSA.EditValue
                .NOMORTELEPON = txtNOMORTELEPON.Text.ToString.Trim.ToUpper
                .CATATAN = txtCATATAN.Text.ToString.Trim.ToUpper
                .ISOFFLINE = chkIsOfline.Checked
                .KDCOB = grdKDCOB.EditValue
                .KDPPK = grdKDPPK.EditValue
                .KDKELASRAWAT = grdKDKELASRAWAT.EditValue
                .REQUEST = jsonRequest
                .RESPON = jsonResponse
                .ISCOB = chkCOB.Checked
                .JAMINAN_ISLAKALANTAS = chkLakaLantas.Checked
                .JAMINAN_PENJAMIN_PENJAMIN1 = chkPenjamin1.Checked
                .JAMINAN_PENJAMIN_PENJAMIN2 = chkPenjamin2.Checked
                .JAMINAN_PENJAMIN_PENJAMIN3 = chkPenjamin3.Checked
                .JAMINAN_PENJAMIN_PENJAMIN4 = chkPenjamin4.Checked
                .JAMINAN_PENJAMIN_TGLKEJADIAN = deDATE_PENJAMIN_TGLKEJADIAN.DateTime
                .JAMINAN_PENJAMIN_KETERANGAN = txtJAMINAN_PENJAMIN_KETERANGAN.Text.Trim
                .JAMINAN_PENJAMIN_SUPLESI_ISSUPLESI = chkISSUPLESI.Checked
                .JAMINAN_PENJAMIN_SUPLESI_NOSEPSUPLESI = txtPENJAMIN_SUPLESI_NOSEPSUPLESI.Text.ToString.Trim.ToUpper
                .JAMINAN_PENJAMIN_SUPLESI_LOKASILAKA_KDPROPONSI = txtSUPLESI_PROPINSI.EditValue
                .JAMINAN_PENJAMIN_SUPLESI_LOKASILAKA_KDKABUPATEN = txtSUPLESI_KABUPATEN.EditValue
                .JAMINAN_PENJAMIN_SUPLESI_LOKASILAKA_KDKECAMATAN = txtSUPLESI_KECAMATAN.EditValue
                Try
                    .INFORMASIPRB = oPendaftaranA.GetData(sNoId).INFORMASIPRB
                Catch ex As Exception
                    .INFORMASIPRB = INFORMASIPRB
                End Try
                Try
                    .CETAK = oPendaftaranA.GetData(sNoId).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try
                .TERSEDIA = 0
                .KDSHIFT = sSHIFT
                .KDUPDATE_APLICARE = IIf(rbCATEGORY.SelectedIndex = 0, "", IIf(grdPEMETAAN.Text = "", "", grdPEMETAAN.EditValue))
                .KODEBOOKING = txtKDBOOKING.Text.ToString.Trim.ToUpper
                .KDBOOKING = txtANTRIANPOLI.Text
                .NAIKRANAP = grdNAIKKELAS.Text
                .PEMBIAYAAN = cboPEMBIAYAAN.Text
                .PENANGGUNGJAWAB = txtPENANGGUNGJAWAB.Text
                .TUJUANKUNJUNGAN = cboTUJUANKUNJUNGAN.Text
                .FLAGPROCEDURE = cboFLAGPROCEDURE.Text
                .KDPENUNJANG = cboKODEKUNJUNGAN.Text
                .ASESMENPELAYANAN = cboASESMENPELAYANAN.Text
            End With

            '***** Kunjungan *****
            Dim dsKunjungan = oPendaftaranA.GetStructureHeader_Kunjungan
            With dsKunjungan
                .DATECREATED = ds.DATECREATED
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                Try
                    .KDKUNJUNGAN = oPendaftaranA.GetDataKunjunganByPendaftaran(sNoId).KDKUNJUNGAN
                Catch ex As Exception
                    .KDKUNJUNGAN = ""
                End Try
                .KDPENDAFTARAN = ds.KDPENDAFTARAN
                .KDDEPARTMENT = grdKDDEPARTMENT.EditValue
                .KDDOCTOR = grdKDDOCTOR.EditValue
                .ALAMAT = txtALAMAT.Text.ToString.ToString.ToUpper
                .KDPENJAMIN = grdKDPENJAMIN.EditValue
                .KDKESATUAN = grdKDKESATUAN.EditValue
                .KDPANGKAT = grdKDPANGKAT.EditValue
                .KDGOLONGAN = grdKDGOLONGAN.EditValue
                .KDPENDIDIKAN = grdKDPENDIDIKAN.EditValue
                .KDPEKERJAAN = grdKDPEKERJAAN.EditValue
                .KDPERUSAHAAN = grdKDPERUSAHAAN.EditValue
                .KDSTATUSKAWIN = cboKDSTATUSKAWIN.SelectedIndex
                .NAMAKELUARGA = txtSTATUSKELUARGA.Text.ToString.Trim.ToUpper
                .KDSTATUSKELUARGA = grdKDSTATUSKELUARGA.EditValue
                .KDUSER = sUserID
                .TERSEDIA = 0
                .KDUPDATE_APLICARE = IIf(rbCATEGORY.SelectedIndex = 0, "", IIf(grdPEMETAAN.Text = "", "", grdPEMETAAN.EditValue))
            End With

            '***** Penanggung Jawab *****
            Dim dsPenanggunjawab = oPendaftaranA.GetStructureHeader_PenanggungJawab
            With dsPenanggunjawab
                .DATECREATED = ds.DATECREATED
                .DATEUPDATED = Now
                .KDPENDAFTARAN = ds.KDPENDAFTARAN
                .NAMA = txtNAMAPENANGGUNGJAWAB.Text.ToString.Trim.ToUpper
                .HUBUNGAN = cboHUBUNGANPENANGGUNGJAWAB.Text
                .ALAMAT = txtALAMATPENANGGUNGJAWAB.Text.ToString.ToString.ToUpper
                .NOMORTELEPON = txtNOMORTELEPONPENANGGUNGJAWAB.Text.ToString.Trim.ToUpper
            End With

            Dim dsIdentitas = oPendaftaranA.GetStructureHeader_Identitas

            If dsCustomer IsNot Nothing Then
                '***** Identitas *****
                With dsIdentitas
                    .DATECREATED = ds.DATECREATED
                    .DATEUPDATED = Now
                    .DATE = deDATE.DateTime
                    .CATEGORY = rbCATEGORY.SelectedIndex
                    .KDKUNJUNGAN = dsKunjungan.KDKUNJUNGAN
                    .KDPENDAFTARAN = ds.KDPENDAFTARAN
                    .PENJAMIN = grdKDDAFTAR_L1.Text
                    .KDCUSTOMER = txtKDCUSTOMER.Text
                    .NAMAPASIEN = txtNAMAPASIEN.Text
                    .ALAMAT = txtALAMAT.Text
                    .DOKTER = grdKDDOCTOR.Text
                    .TUJUAN = grdKDDEPARTMENT.Text
                    .KDDOKTER = grdKDDOCTOR.EditValue
                    .KDTUJUAN = grdKDDEPARTMENT.EditValue
                    .TANGGALLAHIR = dsCustomer.TANGGALLAHIR
                    .JENISKELAMIN = IIf(dsCustomer.KDJENISKELAMIN = 0, "P", "L")
                    .NIK = txtKTP.Text.ToString.Trim.ToUpper
                    .TEMPATLAHIR = dsCustomer.TEMPATLAHIR
                    .AGAMA = dsCustomer.M_AGAMA.MEMO
                    .PANGKAT = dsCustomer.M_PANGKAT.MEMO
                    .NRP = dsCustomer.NRP
                    .KESATUAN = dsCustomer.M_KESATUAN.MEMO
                    .NOMORTELEPON = txtNOMORTELEPON.Text
                    .PENDIDIKAN = dsCustomer.KDPENDIDIKAN
                    .SUKU = dsCustomer.M_SUKU.MEMO
                    .USIA = oPendaftaranA.GetUmurPasien(deDATE.DateTime, dsCustomer.TANGGALLAHIR)
                    .NOMORSEP = txtNOMORSEP.Text
                    .KELASPELAYANAN = grdKDKELASRAWAT.EditValue
                    .KARTUBPJS = txtKARTUBPJS.Text
                    .STATUS_KAWIN = dsCustomer.KDSTATUSKAWIN
                    .HUBUNGAN = cboHUBUNGANPENANGGUNGJAWAB.Text
                    .HUBUNGAN_NAMA = ""
                    .HUBUNGAN_PENDIDIKAN = ""
                    .HUBUNGAN_PEKERJAAN = ""
                    .NOMORASURANSILAIN = ""
                    .KDGOLONGANDARAH = dsCustomer.KDGOLONGANDARAH
                    .KDDIAGNOSA = grdKDDIAGNOSA.EditValue
                    .KDPENJAMIN = ""
                    .KDPERUSAHAAN = ""
                    .DIAGNOSA = grdKDDIAGNOSA.Text
                    .KDUSER = sUserID
                    .HAKKELAS = ""
                    .URL_SIGNATURE = ""
                    .NAMA_TANDATANGAN = ""
                    .FASKES = grdKDPPK.Text
                End With
            Else
                dsIdentitas = Nothing
            End If

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                txtKDPENDAFTARAN.Text = oPendaftaranA.InsertData(ds, dsKunjungan, IIf(rbCATEGORY.SelectedIndex = 1, dsPenanggunjawab, Nothing), dsIdentitas)

                If txtKDPENDAFTARAN.Text = "" Then
                    fn_Save = False
                Else
                    fn_Save = True
                End If
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                fn_Save = oPendaftaranA.UpdateData(ds, dsKunjungan, IIf(rbCATEGORY.SelectedIndex = 1, dsPenanggunjawab, Nothing), dsIdentitas)
            End If

            If fn_Save = True Then
                Try
                    Try
                        oCustomer.UpdateNomorTelepon(dsCustomer.KDCUSTOMER, txtNOMORTELEPON.Text, txtKTP.Text.Trim)
                    Catch ex As Exception

                    End Try

                    Dim oGrouperRawatJalan As New Grouper.clsR_Identitas_Grouper
                    Dim dsDataGrouper = oGrouperRawatJalan.GetStructureHeader
                    Dim dsCariNosep = oGrouperRawatJalan.GetDataByNoRec(ds.KDPENDAFTARAN)

                    With dsDataGrouper
                        If dsCariNosep Is Nothing Then
                            .kodegrouper = 0
                            .datecreated = ds.DATECREATED
                        Else
                            .kodegrouper = dsCariNosep.kodegrouper
                            .datecreated = dsCariNosep.datecreated
                        End If
                        .dateupdated = ds.DATEUPDATED
                        .jeniskelompokpasien = ds.M_DAFTAR_L1.MEMO

                        If dsCariNosep Is Nothing Then
                            .statusenabled = "1"
                        Else
                            .statusenabled = dsCariNosep.statusenabled
                        End If

                        .norec = ds.KDPENDAFTARAN
                        .nostruklastfk = ds.KDPENDAFTARAN_AWAL
                        .jnsPelayanan = IIf(ds.CATEGORY = 0, "R.Jalan", "R.Inap")
                        .kelasRawat = grdKDKELASRAWAT.Text
                        .noRm = ds.KDCUSTOMER
                        .nama = ds.M_CUSTOMER.NAME_DISPLAY
                        .noKartu = ds.KARTUBPJS
                        .asalrujukan = IIf(ds.ASALRUJUKAN = 0, 1, 2)
                        .noSep = ds.NOMORSEP
                        .noRujukan = ds.NOMORRUJUKAN
                        .Faskes = ds.M_PPK.KODEFASKES & " " & ds.M_PPK.MEMO
                        .dpjpkodevclaim = ds.M_DOCTOR.VCLAIM_KDDPJP
                        .dpjp = ds.M_DOCTOR.NAME_DISPLAY
                        .polikodevclaim = ds.M_DEPARTMENT.VCLAIM_KODEPOLI
                        .poli = ds.M_DEPARTMENT.NAME_DISPLAY
                        .catatan = ds.CATATAN
                        .infromasiprb = ds.INFORMASIPRB
                        .peserta = ds.M_DAFTAR_L2.MEMO
                        .cob = ds.M_COB.MEMO
                        .nomortelepon = ds.NOMORTELEPON
                        .noregistrasi = ds.KDPENDAFTARAN
                        .tglPlgSep = ds.DATE
                        .tglSep = ds.DATE
                        .tgl_lahir = ds.M_CUSTOMER.TANGGALLAHIR
                        .gender = IIf(ds.M_CUSTOMER.KDJENISKELAMIN = 0, "P", "L")
                        .diagnosaawal = ds.M_DIAGNOSA.MEMO
                        If dsCariNosep Is Nothing Then
                            .status = ""
                        Else
                            .status = dsCariNosep.status
                        End If
                        .statuspasien = IIf(ds.DATE.ToString("ddMMyyyy") = ds.M_CUSTOMER.DATECREATED.ToString("ddMMyyy"), IIf(ds.M_CUSTOMER.KDCUSTOMER_LAMA = "", "BARU", "LAMA"), "LAMA")
                        .alamat = ds.M_CUSTOMER.ALAMAT

                        If dsCariNosep Is Nothing Then
                            .tglpulang = ds.DATE
                        Else
                            .tglpulang = dsCariNosep.tglpulang
                        End If
                        .jsonpost = ""
                        .kduser = sUserID
                        .ruangan = IIf(ds.CATEGORY = 0, "", ds.M_DEPARTMENT.NAME_DISPLAY)
                        .pangkat = ds.M_CUSTOMER.M_PANGKAT.MEMO
                        .kesatuan = ds.M_CUSTOMER.M_KESATUAN.MEMO
                        .pendidikan = ds.M_CUSTOMER.KDPENDIDIKAN
                        .statusmenikah = ds.M_CUSTOMER.KDSTATUSKAWIN
                        .propinsi = ds.M_CUSTOMER.M_KELURAHAN.M_KECAMATAN.M_KABUPATEN.M_PROPINSI.MEMO
                        .kabupaten = ds.M_CUSTOMER.M_KELURAHAN.M_KECAMATAN.M_KABUPATEN.MEMO
                        .kecamatan = ds.M_CUSTOMER.M_KELURAHAN.M_KECAMATAN.MEMO
                        .kelurahan = ds.M_CUSTOMER.M_KELURAHAN.MEMO
                    End With

                    If dsCariNosep Is Nothing Then
                        If oGrouperRawatJalan.InsertData(dsDataGrouper) = False Then
                        End If
                    Else
                        If oGrouperRawatJalan.UpdateData(dsDataGrouper) = False Then
                        End If
                    End If

                Catch oErr As Exception
                    'MsgBox("Simpan Data Grouper" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

            SplashScreenManager.CloseForm(False)

            If ds.CATEGORY = 0 Then
                If grdKDDAFTAR_L1.Text = "UMUM" Then
                    Dim dsKunjunganSImpan = oPendaftaranA.GetDataKunjunganByPendaftaran(txtKDPENDAFTARAN.Text)
                    If dsKunjunganSImpan IsNot Nothing Then
                        fn_SaveBilling(dsKunjunganSImpan.KDKUNJUNGAN)
                    End If
                End If
            End If

            CetakRegister(txtKDPENDAFTARAN.Text)

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Function fn_SaveBilling(ByVal sKDKUNJUNGAN As String) As Boolean
        Try
            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Processing data Simpan Automatis Billing.....")

            Dim oSalesOrderTransaksi As New Sales.clsSalesOrderTransaksi
            Dim dsCekBilling = oSalesOrderTransaksi.GetDataByKDkunjungan(sKDKUNJUNGAN)

            Dim kdsotransaksi As String = ""
            If dsCekBilling IsNot Nothing Then
                kdsotransaksi = dsCekBilling.KDSOTRANSAKSI
            End If

            ' ***** HEADER *****
            Dim ds = oSalesOrderTransaksi.GetStructureHeader
            With ds
                .DATECREATED = Now
                .DATEUPDATED = Now
                .KDSOTRANSAKSI = kdsotransaksi
                .CATEGORY = rbCATEGORY.SelectedIndex
                .DATE = deDATE.DateTime
                .KDKUNJUNGAN = sKDKUNJUNGAN
                .KDWAREHOUSE = ""
                .ISBHP = False
                .SUBTOTAL = CDec(0)
                .DISCOUNT = CDec(0)
                .TAX = CDec(0)
                .GRANDTOTAL = CDec(0)
                .PAYAMOUNT = CDec(0)
                .MEMO = "auto dari daftar"
                .KDUSER = sUserID
                .KDSHIFT = sSHIFT
                .KDDOCTOR = grdKDDOCTOR.EditValue
                .TUSLAH = CDec(0)
                .KDCPPT = ""
                .KDORDER = ""
            End With

            Dim oItem As New Reference.clsItem
            Dim arrDetail = oSalesOrderTransaksi.GetStructureDetailList

            If sHargaApotik = True Then
                For Each xloop In oItem.GetDataItemOutomatis()
                    Dim dsDetail = oSalesOrderTransaksi.GetStructureDetail
                    With dsDetail
                        .DATECREATED = deDATE.DateTime
                        .DATEUPDATED = Now
                        .SEQ = i
                        .KDSOTRANSAKSI = ds.KDSOTRANSAKSI
                        .KDITEM = xloop.KDITEM
                        Dim dsUom = oItem.GetDataDetail_UOM(xloop.KDITEM)

                        .KDUOM = dsUom.FirstOrDefault(Function(x) x.RATE = 1).KDUOM
                        .KDSIGNA = oItem.DefaultItem_Signa
                        .KDCARAPAKAI = oItem.DefaultItem_CaraPakai
                        .QTY = CDec(1)
                        .ISRACIK = False
                        .PRICE = CDec(dsUom.FirstOrDefault(Function(x) x.RATE = 1).PRICESALESSTANDARD)
                        .SUBTOTAL = CDec(.PRICE)
                        .DISCOUNT = CDec(0)
                        .GRANDTOTAL = CDec(.PRICE)
                        .KDDOCTOR = ds.KDDOCTOR
                        .KDDEPARTMENT = grdKDDEPARTMENT.EditValue
                        .REMARKS = "-"
                        .ISCETAKETIKET = False
                        .KDUSER = sUserID

                        ds.SUBTOTAL += .GRANDTOTAL
                    End With
                    arrDetail.Add(dsDetail)
                Next
            Else
                Dim dsItem = oItem.GetDataLItem6Description(grdKDDEPARTMENT.Text)
                If dsItem IsNot Nothing Then
                    Dim dsDetail = oSalesOrderTransaksi.GetStructureDetail
                    With dsDetail
                        .DATECREATED = deDATE.DateTime
                        .DATEUPDATED = Now
                        .SEQ = i
                        .KDSOTRANSAKSI = ds.KDSOTRANSAKSI
                        .KDITEM = dsItem.KDITEM
                        Dim dsUom = oItem.GetDataDetail_UOM(dsItem.KDITEM)

                        .KDUOM = dsUom.FirstOrDefault(Function(x) x.RATE = 1).KDUOM
                        .KDSIGNA = oItem.DefaultItem_Signa
                        .KDCARAPAKAI = oItem.DefaultItem_CaraPakai
                        .QTY = CDec(1)
                        .ISRACIK = False
                        .PRICE = CDec(dsUom.FirstOrDefault(Function(x) x.RATE = 1).PRICESALESSTANDARD)
                        .SUBTOTAL = CDec(.PRICE)
                        .DISCOUNT = CDec(0)
                        .GRANDTOTAL = CDec(.PRICE)
                        .KDDOCTOR = ds.KDDOCTOR
                        .KDDEPARTMENT = grdKDDEPARTMENT.EditValue
                        .REMARKS = "-"
                        .ISCETAKETIKET = False
                        .KDUSER = sUserID

                        ds.SUBTOTAL += .GRANDTOTAL
                    End With
                    arrDetail.Add(dsDetail)
                Else
                    For Each xloop In oItem.GetDataItemOutomatis()
                        Dim dsDetail = oSalesOrderTransaksi.GetStructureDetail
                        With dsDetail
                            .DATECREATED = deDATE.DateTime
                            .DATEUPDATED = Now
                            .SEQ = i
                            .KDSOTRANSAKSI = ds.KDSOTRANSAKSI
                            .KDITEM = xloop.KDITEM
                            Dim dsUom = oItem.GetDataDetail_UOM(xloop.KDITEM)

                            .KDUOM = dsUom.FirstOrDefault(Function(x) x.RATE = 1).KDUOM
                            .KDSIGNA = oItem.DefaultItem_Signa
                            .KDCARAPAKAI = oItem.DefaultItem_CaraPakai
                            .QTY = CDec(1)
                            .ISRACIK = False
                            .PRICE = CDec(dsUom.FirstOrDefault(Function(x) x.RATE = 1).PRICESALESSTANDARD)
                            .SUBTOTAL = CDec(.PRICE)
                            .DISCOUNT = CDec(0)
                            .GRANDTOTAL = CDec(.PRICE)
                            .KDDOCTOR = ds.KDDOCTOR
                            .KDDEPARTMENT = grdKDDEPARTMENT.EditValue
                            .REMARKS = "-"
                            .ISCETAKETIKET = False
                            .KDUSER = sUserID
                            .GROUPRACIK = 0

                            ds.SUBTOTAL += .GRANDTOTAL
                        End With
                        arrDetail.Add(dsDetail)
                    Next
                End If
            End If

            ds.SUBTOTAL = ds.SUBTOTAL
            ds.GRANDTOTAL = ds.SUBTOTAL

            If arrDetail.Count > 0 Then
                If dsCekBilling Is Nothing Then
                    Dim sKDSOTRANSAKSI = oSalesOrderTransaksi.InsertData(ds, arrDetail)
                    If sKDSOTRANSAKSI = "" Then
                        fn_SaveBilling = False
                    Else
                        fn_SaveBilling = True
                    End If
                Else
                    ds.KDSOTRANSAKSI = dsCekBilling.KDSOTRANSAKSI
                    ds.DATECREATED = dsCekBilling.DATECREATED
                    fn_SaveBilling = oSalesOrderTransaksi.UpdateData(ds, arrDetail)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveBilling = False
        End Try
    End Function
    'Private Sub fn_SaveOnline()
    '    Try
    '        Dim oConnMySql As New MySql.Data.MySqlClient.MySqlConnection
    '        Dim oCommMySql As New MySql.Data.MySqlClient.MySqlCommand
    '        Dim daMySql As MySql.Data.MySqlClient.MySqlDataAdapter
    '        Dim dsMySql As New DataSet
    '        Dim MYSQL As String

    '        dsMySql = New DataSet

    '        oConnMySql = New MySqlConnection(sConnMySql)

    '        If oConnMySql.State = ConnectionState.Closed Then
    '            oConnMySql.Open()
    '        End If

    '        MYSQL = "SELECT * FROM "
    '        MYSQL &= "pasien_lama "
    '        MYSQL &= "where "
    '        MYSQL &= "rm = '" & CInt(txtKDCUSTOMER.Text) & "' "

    '        oCommMySql.Connection = oConnMySql
    '        oCommMySql.CommandText = MYSQL
    '        oCommMySql.CommandTimeout = 120
    '        oCommMySql.CommandType = CommandType.Text

    '        daMySql = New MySql.Data.MySqlClient.MySqlDataAdapter(oCommMySql)
    '        daMySql.Fill(dsMySql, "selectskd")

    '        If dsMySql.Tables("selectskd").Rows.Count < 1 Then
    '            MYSQL = "INSERT INTO "
    '            MYSQL &= "pasien_lama "
    '            MYSQL &= "( "
    '            MYSQL &= "rm "
    '            MYSQL &= ",nama "
    '            MYSQL &= ",ttl "
    '            MYSQL &= ") "
    '            MYSQL &= "VALUES ( "
    '            MYSQL &= "'" & CInt(txtKDCUSTOMER.Text) & "' "
    '            MYSQL &= ",'" & txtNAMAPASIEN.Text & "' "
    '            MYSQL &= ",'" & oPendaftaran.GetDataMasterPasien(txtKDCUSTOMER.Text).TANGGALLAHIR.ToString("yyyy-MM-dd") & "' "
    '            MYSQL &= ") "

    '            oCommMySql.Connection = oConnMySql
    '            oCommMySql.CommandText = MYSQL
    '            oCommMySql.CommandTimeout = 120
    '            oCommMySql.CommandType = CommandType.Text

    '            daMySql = New MySql.Data.MySqlClient.MySqlDataAdapter(oCommMySql)
    '            daMySql.Fill(dsMySql, "insertskd")
    '        Else
    '            MYSQL = "UPDATE "
    '            MYSQL = "pasien_lama SET "
    '            MYSQL &= "rm = '" & CInt(txtKDCUSTOMER.Text) & "' "
    '            MYSQL &= ",nama = '" & txtNAMAPASIEN.Text & "' "
    '            MYSQL &= ",ttl = '" & oPendaftaran.GetDataMasterPasien(txtKDCUSTOMER.Text).TANGGALLAHIR.ToString("yyyy-MM-dd") & "' "
    '            MYSQL &= "WHERE rm = '" & CInt(txtKDCUSTOMER.Text) & "' "

    '            oCommMySql.Connection = oConnMySql
    '            oCommMySql.CommandText = MYSQL
    '            oCommMySql.CommandTimeout = 120
    '            oCommMySql.CommandType = CommandType.Text

    '            daMySql = New MySql.Data.MySqlClient.MySqlDataAdapter(oCommMySql)
    '            daMySql.Fill(dsMySql, "updateskd")
    '        End If

    '        If oConnMySql.State = ConnectionState.Open Then
    '            oConnMySql.Close()
    '        End If
    '    Catch oErr As Exception
    '        'MsgBox("Save Online Gagal" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
#End Region
#Region "Command Button"
    Private Sub frmPendaftaran_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F2
                If btnSaveNew.Enabled = True Then
                    btnSaveNew_Click()
                End If
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
            Case Keys.F5
                If btnAddCustomer.Enabled = True Then
                    btnAddCustomer_Click()
                End If
            'Case Keys.F6
            '    If btnNCI.Enabled = True Then
            '        btnNCI_Click()
            '    End If
            Case Keys.F7
                If btnPemetaan.Enabled = True Then
                    btnPemetaan_Click()
                End If
            Case Keys.F8
                If btnCreateSEP.Enabled = True Then
                    btnCreateSEP_Click()
                End If
            Case Keys.F9
                If btnSKD.Enabled = True Then
                    btnSKD_Click()
                End If
            Case Keys.F10
                If btnCari.Enabled = True Then
                    btnCari_Click()
                End If
            Case Keys.F12
                btnClose_Click()
        End Select
    End Sub
    Private Sub btnKunjunganBPJS_Click() Handles btnKunjunganBPJS.ItemClick
        frmMonitoringBPJS.fn_LoadDataBPJS(txtKARTUBPJS.Text)
        frmMonitoringBPJS.ShowDialog(Me)
    End Sub
    Private Sub btnSKD_Click() Handles btnSKD.ItemClick
        Dim frmSKD As New frmSKD
        Try
            If grdKDDOCTOR.Text <> "" Then
                frmSKD.fn_LoadNoPendaftaranPolidanDokter(rbCATEGORY.SelectedIndex, grdKDDEPARTMENT.EditValue, grdKDDOCTOR.EditValue, txtKDCUSTOMER.Text, "", "")
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
                frmSKD.ShowDialog(Me)
                If sCodeSKD <> "<--- AUTO --->" Then
                    If sCodeSKD <> "" Then
                        txtNOMORSKDP.Text = sCodeSKD
                        fn_LoadKDSKD(txtNOMORSKDP.Text)
                    End If
                End If
                txtNOMORSKDP.Focus()
            Else
                MsgBox("Dokter Belum dipilih", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmSKD Is Nothing Then frmSKD.Dispose()
            frmSKD = Nothing
        End Try
    End Sub
    'Private Sub btnNCI_Click() Handles btnNCI.ItemClick
    '    Dim frmPencarianCustomerNCI As New frmPencarianCustomerNCI
    '    Try
    '        frmPencarianCustomerNCI.ShowDialog(Me)

    '        Dim rm As String = String.Empty

    '        If sKD_PASIENdgCare = "" Then
    '            rm = sKD_PASIENnci
    '        Else
    '            rm = sKD_PASIENdgCare
    '        End If

    '        sKD_PASIENnci = ""
    '        sKD_PASIENdgCare = ""

    '        txtKDCUSTOMER.Text = rm
    '        txtKDCUSTOMER.Focus()

    '        If fn_GetDataCustomer(rm) = False Then
    '            btnAddCustomer_Click()
    '        End If

    '    Catch oErr As Exception
    '        sKD_PASIENnci = ""
    '        sKD_PASIENdgCare = ""
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
    Private Sub btnAntrian_Click() Handles btnAntrian.ItemClick
        Try
            frmReportAntrianOnlineJKN.ShowDialog(Me)
            txtKDBOOKING.Focus()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmReportAntrianOnlineJKN Is Nothing Then frmReportAntrianOnlineJKN.Dispose()
            frmReportAntrianOnlineJKN = Nothing

            If sCOPYKODDEBOOKING <> "" Then
                txtKDBOOKING.Text = sCOPYKODDEBOOKING
            End If
        End Try
    End Sub
    Private Sub btnCari_Click() Handles btnCari.ItemClick
        sNomorSKDPspri = ""
        sNomorSKDPspriSEP = ""

        frmReportRencaKontrol.ShowDialog(Me)

        If sNomorSKDPspri <> "" Then
            txtNOMORSKDP.Text = sNomorSKDPspri
            fn_LoadKDSKD(txtNOMORSKDP.Text)

            If sNomorSKDPspriSEP <> "" Then
                txtNOMORRUJUKAN.Text = sNomorSKDPspriSEP
                cboASALRUJUKAN.SelectedIndex = 1
                cboTUJUANKUNJUNGAN.SelectedIndex = 0
                cboASESMENPELAYANAN.ResetText()
            Else
                cboTUJUANKUNJUNGAN.SelectedIndex = 0
                cboASESMENPELAYANAN.ResetText()
                cboFLAGPROCEDURE.ResetText()
                cboKODEKUNJUNGAN.ResetText()
            End If
        End If

        txtNOMORSKDP.Focus()
    End Sub
    Private Function fn_GetFingerPrint(ByVal Kartu As String, ByVal TglPel As String) As Boolean
        Try
            Dim uTime As Integer = 0

            If sVclaim_ConsId <> "" Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.GetFingerPrint(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, Kartu, TglPel)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        fn_GetFingerPrint = True
                        Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), sVclaim_ConsId & sVclaim_SecreatKey & uTime))

                        MsgBox(CodeResponse & " - " & DataDecrypt("status").ToString(), MsgBoxStyle.Exclamation, Me.Text)

                    Else
                        fn_GetFingerPrint = False
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    fn_GetFingerPrint = False
                    MsgBox("Kosong Koneksi", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_GetFingerPrint = False
                MsgBox("Cari Finger Print Data Gagal/Kosong", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            fn_GetFingerPrint = False
            fn_GetFingerPrint = Statement.ErrorStatement & vbCrLf & oErr.Message
        End Try
    End Function
    Private Sub btnFinger_Click(sender As Object, e As EventArgs) Handles btnFinger.Click
        If txtKARTUBPJS.Text = "" Then Exit Sub

        If txtKARTUBPJS.Text.Count <> 13 Then
            txtKARTUBPJS.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            txtKARTUBPJS.ErrorText = Statement.ErrorRequired
            MsgBox(Statement.ErrorStatement & " No Kartu BPJS Tidak sama dengan 13 Digit", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        fn_GetFingerPrint(txtKARTUBPJS.Text, Now.ToString("yyyy-MM-dd"))
    End Sub
    Private Sub btnListFinger_Click() Handles btnListFinger.ItemClick
        Try
            Dim uTime As Integer = 0

            If sVclaim_ConsId <> "" Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.GetListFingerPrint(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, Now.ToString("yyyy-MM-dd"))

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), sVclaim_ConsId & sVclaim_SecreatKey & uTime))
                        MsgBox(CodeResponse & " - " & DataDecrypt("list").ToString(), MsgBoxStyle.Exclamation, Me.Text)
                    Else
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    MsgBox("Kosong Koneksi", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                MsgBox("Cari Finger Print Data Gagal/Kosong", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnCreateSEP_Click() Handles btnCreateSEP.ItemClick
        If oFormMode = FORM_MODE.FORM_MODE_EDIT Then
            Dim dsDaftar = oPendaftaranA.GetData(txtKDPENDAFTARAN.Text.ToString)
            If dsDaftar IsNot Nothing Then
                Try
                    Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                    Dim jsonRequest As String = String.Empty
                    Dim jsonResponse As String = String.Empty
                    Dim INFORMASIPRB As String = String.Empty

                    jsonRequest = fn_jsonRequestInsertSEPV2()

                    If jsonRequest <> "" Then
                        jsonResponse = fn_CreateSEPv2(jsonRequest, uTime)
                        If jsonResponse <> "" Then
                            Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(jsonResponse, sVclaim_ConsId & sVclaim_SecreatKey & uTime))
                            txtNOMORSEP.Text = DataDecrypt.Item("sep")("noSep").ToString()
                            INFORMASIPRB = DataDecrypt.Item("sep")("informasi")("prolanisPRB").ToString()
                            oPendaftaranA.UpdateSEP(dsDaftar.KDPENDAFTARAN, txtNOMORSEP.Text, jsonRequest, jsonResponse, sUserID, txtNOMORSKDP.Text, INFORMASIPRB)
                        End If
                    End If
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Else
            MsgBox("Hanya dilakukan pada saat edit pendaftaran", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub btnPemetaan_Click() Handles btnPemetaan.ItemClick
        Dim frmPemetaanRuangan As New frmPemetaanRuangan
        Try
            frmPemetaanRuangan.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
        chkIsOfline.Checked = True

        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            If txtNOMORSKDP.Text <> "" Then
                Dim oSKDS As New Admission.clsSKD
                Dim dsSKDS = oSKDS.GetDataOfline(txtNOMORSKDP.Text.ToString.Trim.ToUpper)
                If dsSKDS IsNot Nothing Then
                    oSKDS.UpdateDataFix(dsSKDS.KDSKD, True)
                End If
            End If

            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)

            Me.Close()
        End If
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            If txtNOMORSKDP.Text <> "" Then
                Dim oSKDS As New Admission.clsSKD
                Dim dsSKDS = oSKDS.GetDataOfline(txtNOMORSKDP.Text.ToString.Trim.ToUpper)
                If dsSKDS IsNot Nothing Then
                    oSKDS.UpdateDataFix(dsSKDS.KDSKD, True)
                End If
            End If

            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)

            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
    Private Sub btnAddCustomer_Click() Handles btnAddCustomer.ItemClick
        Dim oCustomer As New Reference.clsCustomer
        Dim dsCustomer = oCustomer.GetData(txtKDCUSTOMER.Text)
        sCodeCustomer = ""
        If dsCustomer IsNot Nothing Then
            Dim frmCustomer As New frmCustomer
            Try
                frmCustomer.LoadMe(FORM_MODE.FORM_MODE_EDIT, Nothing, dsCustomer.KDCUSTOMER)
                frmCustomer.ShowDialog(Me)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                txtKDCUSTOMER.Focus()
            End Try
        Else
            Dim frmCustomer As New frmCustomer
            Try
                frmCustomer.LoadMe(FORM_MODE.FORM_MODE_ADD, Nothing)
                frmCustomer.ShowDialog(Me)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If sCodeCustomer <> "" Then
                    If sCodeCustomer <> "<--- AUTO --->" Then
                        txtKDCUSTOMER.Text = sCodeCustomer
                        txtKDCUSTOMER.Focus()
                    End If
                End If
            End Try
        End If
    End Sub
    Private Sub DeleteDirectory(path As String)
        If IO.Directory.Exists(path) Then
            If IO.Directory.Exists(path) Then
                'Delete all files from the Directory
                For Each filepath As String In IO.Directory.GetFiles(path)
                    IO.File.Delete(filepath)
                Next
                'Delete all child Directories
                For Each dir As String In IO.Directory.GetDirectories(path)
                    DeleteDirectory(dir)
                Next
                'Delete a Directory
                IO.Directory.Delete(path)
            End If

        End If
    End Sub
    Private Sub CetakRegister(ByVal KDPENDAFTARAN As String)
        Try
            sCetakSEP = False

            If grdKDDAFTAR_L1.Text = "BPJS" Then
                If rbCATEGORY.SelectedIndex = 0 Then
                    Dim ds = oPendaftaranA.GetData(KDPENDAFTARAN)

                    sUmur = oPendaftaranA.GetUmurPasien(ds.DATE, ds.M_CUSTOMER.TANGGALLAHIR)

                    Dim rpt As New xtraAntrianPendaftaran_88_Kecil
                    rpt.BindingSource.DataSource = ds
                    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                    printTool.PrintDialog()

                    If ds.NOMORSEP <> "" Then
                        Dim rptsep As New xtraSEPNomorAntrian
                        rptsep.bindingSource.DataSource = ds
                        Dim printToolsep As New DevExpress.XtraReports.UI.ReportPrintTool(rptsep)
                        printToolsep.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)

                        Dim rpt1 As New xtraSEPNomorAntrian
                        rpt1.bindingSource.DataSource = ds
                        Dim printTool1 As New DevExpress.XtraReports.UI.ReportPrintTool(rpt1)
                        printTool1.PrintDialog()
                    Else
                        MsgBox("Nomor SEP Kosong", MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    If txtNOMORSEP.Text <> "" Then
                        Dim ds = oPendaftaranA.GetData(KDPENDAFTARAN)

                        sUmur = oPendaftaranA.GetUmurPasien(ds.DATE, ds.M_CUSTOMER.TANGGALLAHIR)

                        Dim rptsep As New xtraSEPNomorAntrian
                        rptsep.bindingSource.DataSource = ds
                        Dim printToolsep As New DevExpress.XtraReports.UI.ReportPrintTool(rptsep)
                        printToolsep.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
                    Else
                        MsgBox("Nomor SEP Kosong", MsgBoxStyle.Exclamation, Me.Text)
                    End If
                End If
            Else
                Dim ds = oPendaftaranA.GetData(KDPENDAFTARAN)

                If ds IsNot Nothing Then
                    sUmur = oPendaftaranA.GetUmurPasien(ds.DATE, ds.M_CUSTOMER.TANGGALLAHIR)

                    If ds.M_DEPARTMENT.OTHER = "ANTRIAN" Then
                        Dim rpt As New xtraUmumAntrian

                        rpt.bindingSource.DataSource = ds
                        Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                        printTool.PrintDialog()
                    Else
                        Dim rpt As New xtraUmum
                        rpt.bindingSource.DataSource = ds
                        Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                        printTool.PrintDialog()
                    End If
                Else
                    MsgBox("Pendaftaran Kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If

            End If

            If sCetakSEP = True Then
                oPendaftaranA.UpdateCetak(txtKDPENDAFTARAN.Text)
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
#Region "Lookup"
    Private Sub fn_LoadDaftar1()
        Dim oDAFTAR_L1 As New Reference.clsDaftar_L1
        Try
            grdKDDAFTAR_L1.Properties.DataSource = oDAFTAR_L1.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDAFTAR_L1.Properties.ValueMember = "KDDAFTAR_L1"
            grdKDDAFTAR_L1.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDaftar2()
        Dim oDAFTAR_L2 As New Reference.clsDaftar_L2
        Try
            grdKDDAFTAR_L2.Properties.DataSource = oDAFTAR_L2.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDAFTAR_L2.Properties.ValueMember = "KDDAFTAR_L2"
            grdKDDAFTAR_L2.Properties.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDaftar3()
        Dim oDAFTAR_L3 As New Reference.clsDaftar_L3
        Try
            grdKDDAFTAR_L3.Properties.DataSource = oDAFTAR_L3.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDAFTAR_L3.Properties.ValueMember = "KDDAFTAR_L3"
            grdKDDAFTAR_L3.Properties.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDaftar4()
        Dim oDAFTAR_L4 As New Reference.clsDaftar_L4
        Try
            grdKDDAFTAR_L4.Properties.DataSource = oDAFTAR_L4.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDAFTAR_L4.Properties.ValueMember = "KDDAFTAR_L4"
            grdKDDAFTAR_L4.Properties.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDaftar5()
        Dim oDAFTAR_L5 As New Reference.clsDaftar_L5
        Try
            grdKDDAFTAR_L5.Properties.DataSource = oDAFTAR_L5.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDAFTAR_L5.Properties.ValueMember = "KDDAFTAR_L5"
            grdKDDAFTAR_L5.Properties.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDepartment()
        Dim oDepartment As New Reference.clsDepartment
        Try
            grdKDDEPARTMENT.Properties.DataSource = oDepartment.GetData.Where(Function(x) x.ISACTIVE = True And x.ISRUANGRAWAT = IIf(rbCATEGORY.SelectedIndex = 0, False, True)).ToList()
            grdKDDEPARTMENT.Properties.ValueMember = "KDDEPARTMENT"
            grdKDDEPARTMENT.Properties.DisplayMember = "NAME_DISPLAY"

            If rbCATEGORY.SelectedIndex = 0 Then
                colTERSEDIA.VisibleIndex = -1
            Else
                colTERSEDIA.VisibleIndex = 1
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDoctor(ByVal KDDEPARTMENT As String)
        If grdKDDEPARTMENT.Text = "" Then Exit Sub
        Dim oDoctor As New Reference.clsDoctor
        Dim oDepartment As New Reference.clsDepartment

        If rbCATEGORY.SelectedIndex = 0 Then
            Try
                Dim dsDoctor = From x In oDoctor.GetDataDetail_DEPARMENT
                               Where x.KDDEPARTMENT = grdKDDEPARTMENT.EditValue And x.M_DEPARTMENT.ISRUANGRAWAT = False And x.M_DOCTOR.ISACTIVE = True
                               Select x.KDDOCTOR, x.M_DOCTOR.NAME_DISPLAY

                grdKDDOCTOR.Properties.DataSource = dsDoctor.ToList()
                grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
                grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Else
            Try
                grdKDDOCTOR.Properties.DataSource = oDoctor.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
                grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
                grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    'Private Sub fn_LoadDiganosa()
    '    Dim oDiagnosa As New Reference.clsDiagnosa
    '    Try
    '        grdKDDIAGNOSA.Properties.DataSource = oDiagnosa.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
    '        grdKDDIAGNOSA.Properties.ValueMember = "KDDIAGNOSA"
    '        grdKDDIAGNOSA.Properties.DisplayMember = "MEMO"

    '    Catch oErr As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
    Private Sub fn_LoadDiganosa()
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

            SQL = "EXEC "
            SQL &= "CARIDIAGNOSA "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "CARIDIAGNOSA")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grdKDDIAGNOSA.Properties.DataSource = ds.Tables("CARIDIAGNOSA")
            grdKDDIAGNOSA.Properties.ValueMember = "KDDIAGNOSA"
            grdKDDIAGNOSA.Properties.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFaskes()
        Dim oPPK As New Reference.clsPPK
        Try
            grdKDPPK.Properties.DataSource = oPPK.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDPPK.Properties.ValueMember = "KDPPK"
            grdKDPPK.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDoctor_SKD()
        Dim oDOCTOR As New Reference.clsDoctor

        Try
            grdKDDOCTOR_SKD.Properties.DataSource = oDOCTOR.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDOCTOR_SKD.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR_SKD.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadkelasRawat()
        Dim oKELAS As New Reference.clsKelasRawat
        Try
            grdKDKELASRAWAT.Properties.DataSource = oKELAS.GetData.Where(Function(x) x.ISACTIVE = True And x.KODEPENDAFTARAN <> 0).ToList()
            grdKDKELASRAWAT.Properties.ValueMember = "KDKELASRAWAT"
            grdKDKELASRAWAT.Properties.DisplayMember = "MEMO"

            grdNAIKKELAS.Properties.DataSource = oKELAS.GetData.Where(Function(x) x.ISACTIVE = True And x.KODEPENDAFTARAN <> 0).ToList()
            grdNAIKKELAS.Properties.ValueMember = "KDKELASRAWAT"
            grdNAIKKELAS.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadCOB()
        Dim oCOB As New Reference.clsCOB
        Try
            grdKDCOB.Properties.DataSource = oCOB.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDCOB.Properties.ValueMember = "KDCOB"
            grdKDCOB.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadPemetaan()
        Try
            Dim oKelasAplicare As New Reference.clsKelasAplicare
            Dim dsDepartment = From x In oKelasAplicare.GetDataDetail_UOM
                               Where x.KAPASITAS <> 0 And x.M_KELASAPLICARE.ISACTIVE = True And x.KDUPDATE_APLICARE <> ""
                               Select x.KDUPDATE_APLICARE, KELAS = x.M_KELASAPLICARE.MEMO, RUANGAN = x.M_DEPARTMENT.NAME_DISPLAY, x.KAPASITAS, x.TERSEDIA, x.TERSEDIA_LAKI, x.TERSEDIA_PEREMPUAN

            grdPEMETAAN.Properties.DataSource = dsDepartment.ToList()
            grdPEMETAAN.Properties.ValueMember = "KDUPDATE_APLICARE"
            grdPEMETAAN.Properties.DisplayMember = "KDUPDATE_APLICARE"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdPEMETAAN_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdPEMETAAN.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdPEMETAAN.ResetText()
        End If
    End Sub
    Private Function fn_CariKartuBPJS(ByVal NomorKartu As String) As Boolean
        Try
            If grdKDDAFTAR_L1.Text = "BPJS" Then
                fn_CariKartuBPJS = False

                Dim ocustomer As New Reference.clsCustomer

                Dim oCek As Integer = 0

                For Each xloop In ocustomer.GetDataListKartu(NomorKartu)
                    If xloop.M_PENJAMIN.MEMO = "BPJS" Then
                        oCek += 1
                    End If
                Next

                If oCek > 1 Then
                    fn_CariKartuBPJS = False
                    MsgBox("Nomor Kartu BPJS Duplikasi, Silahkan Perbaiki kartu BPJS anda ke Bagian Admisi", MsgBoxStyle.Exclamation, Me.Text)
                Else
                    fn_CariKartuBPJS = True
                End If
            Else
                fn_CariKartuBPJS = True
            End If
        Catch oErr As Exception
            fn_CariKartuBPJS = False
            MsgBox("fn_CariKartuBPJS" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_GetDataCustomer(ByVal Parameter As String) As Boolean
        fn_GetDataCustomer = False

        If Parameter = "" Then Exit Function

        Dim oCUSTOMER As New Reference.clsCustomer
        Dim oSKD As New Admission.clsSKD
        Dim dsCustomer = oCUSTOMER.GetData(Parameter)
        Dim JenisKelaminCek As String = ""
        Try
            If dsCustomer IsNot Nothing Then
                If dsCustomer.KDJENISKELAMIN = 0 Then
                    JenisKelaminCek = "P"
                ElseIf dsCustomer.KDJENISKELAMIN = 1 Then
                    JenisKelaminCek = "L"
                Else
                    txtKDCUSTOMER.Text = dsCustomer.KDCUSTOMER
                    MsgBox("Silahkan Perbaiki Data Pasien Terlebih dahulu", MsgBoxStyle.Exclamation, Me.Text)
                    btnAddCustomer_Click()

                    Dim dsCek = oCUSTOMER.GetData(txtKDCUSTOMER.Text)
                    If dsCek IsNot Nothing Then
                        If dsCek.KDJENISKELAMIN = 0 Then
                            JenisKelaminCek = "P"
                        ElseIf dsCek.KDJENISKELAMIN = 1 Then
                            JenisKelaminCek = "L"
                        End If
                    Else
                        MsgBox("Data Belum di Perbaiki, Silahkan Perbaiki Data Pasien Terlebih dahulu", MsgBoxStyle.Critical, Me.Text)
                        Exit Function
                    End If

                    If JenisKelaminCek = "" Then
                        MsgBox("Data Belum di Perbaiki, Silahkan Perbaiki Data Pasien Terlebih dahulu", MsgBoxStyle.Critical, Me.Text)
                        Exit Function
                    End If
                End If

                If txtKARTUBPJS.Text <> "" Then
                    If fn_CariKartuBPJS(txtKARTUBPJS.Text) = False Then
                        Exit Function
                    End If
                End If

                fn_GetDataCustomer = True

                Dim a, b, c As String
                a = Year(Now)
                b = Year(dsCustomer.TANGGALLAHIR)
                c = a - b

                Dim dsPasienBaru = oPendaftaranA.GetDataByRMPasienBaru(dsCustomer.KDCUSTOMER, deDATE.DateTime)

                If dsPasienBaru IsNot Nothing Then
                    lblDINAS.Text = dsCustomer.M_KESATUAN.GOL.ToString.Trim.ToUpper & "( PASIEN LAMA )"
                Else
                    lblDINAS.Text = dsCustomer.M_KESATUAN.GOL.ToString.Trim.ToUpper & "( PASIEN BARU )"
                End If

                txtKDCUSTOMER.Text = dsCustomer.KDCUSTOMER.ToString.Trim.ToUpper
                txtNAMAPASIEN.Text = dsCustomer.NAME_DISPLAY.ToString.Trim.ToUpper & " ( " & c & " tahun" & " )"

                txtKDJENISKELAMIN.Text = JenisKelaminCek

                txtKARTUBPJS.Text = dsCustomer.KARTUBPJS.ToString.Trim.ToUpper
                txtKTP.Text = dsCustomer.KTP.ToString.Trim.ToUpper
                txtALAMAT.Text = dsCustomer.ALAMAT & " KELURAHAN : " & IIf(dsCustomer.M_KELURAHAN.MEMO = "DEFAULT", "-", dsCustomer.M_KELURAHAN.MEMO) & " KECAMATAN : " & dsCustomer.M_KELURAHAN.M_KECAMATAN.MEMO & " Kode Pos : " & dsCustomer.M_KELURAHAN.KODEPOS & " KABUPATEN/KOTA : " & dsCustomer.M_KELURAHAN.M_KECAMATAN.M_KABUPATEN.MEMO & " PROPINSI : " & dsCustomer.M_KELURAHAN.M_KECAMATAN.M_KABUPATEN.M_PROPINSI.MEMO & " NO TELEPON : " & dsCustomer.PHONE

                txtNOMORTELEPON.Text = oCUSTOMER.GetData(dsCustomer.KDCUSTOMER).PHONE

                If txtNOMORTELEPON.Text = "" Then
                    txtNOMORTELEPON.Text = oCUSTOMER.GetData(dsCustomer.KDCUSTOMER).MOBILE
                ElseIf txtNOMORTELEPON.Text = "000000000000" Then
                    txtNOMORTELEPON.Text = oCUSTOMER.GetData(dsCustomer.KDCUSTOMER).MOBILE
                End If

                fn_LoadKunjunganPenjamin()
                grdKDPENJAMIN.Text = dsCustomer.KDPENJAMIN
                fn_LoadKunjunganKesatuan()
                grdKDKESATUAN.Text = dsCustomer.KDKESATUAN
                fn_LoadKunjunganPangkat()
                grdKDPANGKAT.Text = dsCustomer.KDPANGKAT
                fn_LoadKunjunganGolongan()
                grdKDGOLONGAN.Text = dsCustomer.KDGOLONGAN
                fn_LoadKunjunganPendidikan()
                grdKDPENDIDIKAN.Text = dsCustomer.KDPENDIDIKAN
                fn_LoadKunjunganPekerjaan()
                grdKDPEKERJAAN.Text = dsCustomer.KDPEKERJAAN
                fn_LoadKunjunganPerusahaan()
                grdKDPERUSAHAAN.Text = dsCustomer.KDPERUSAHAAN
                fn_LoadKunjunganKeluarga()
                grdKDSTATUSKELUARGA.Text = dsCustomer.KDSTATUSKELUARGA
                txtSTATUSKELUARGA.Text = dsCustomer.M_STATUSKELUARGA.MEMO
                cboKDSTATUSKAWIN.SelectedIndex = dsCustomer.KDSTATUSKAWIN

                fn_LoadHistoryPasien(dsCustomer.KDCUSTOMER)

                'fn_LoadKDSKDByKDCUSTOMER(dsCustomer.KDCUSTOMER)
                Dim dsSKD = oSKD.GetDataByRMTanggalKonsul(dsCustomer.KDCUSTOMER, deDATE.DateTime)
                If dsSKD IsNot Nothing Then
                    txtNOMORSKDP.Text = dsSKD.KDSKD
                    fn_LoadDoctor(dsSKD.KDDEPARTMENT)
                    grdKDDOCTOR_SKD.Text = dsSKD.KDDOCTOR
                    grdKDDOCTOR.Text = dsSKD.KDDOCTOR
                    If txtNOMORRUJUKAN.Text = "" Then
                        txtNOMORRUJUKAN.Text = dsSKD.NOMORRUJUKAN
                    End If
                End If
            Else
                'fn_LoadKDSKDByKDCUSTOMER(txtKDCUSTOMER.Text)
                txtKDCUSTOMER.ResetText()
                txtKARTUBPJS.ResetText()

                If txtNOMORRUJUKAN.Text = "" Then
                    fn_EmptyMe()
                End If

                MsgBox("Pasien Tidak ditemukan di Database SIMRS", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox("Get Data Pasien" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub fn_LoadKunjunganPenjamin()
        Dim oPENJAMIN As New Reference.clsPenjamin
        Try
            grdKDPENJAMIN.Properties.DataSource = oPENJAMIN.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDPENJAMIN.Properties.ValueMember = "KDPENJAMIN"
            grdKDPENJAMIN.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKunjunganKesatuan()
        Dim oKESATUAN As New Reference.clsKesatuan
        Try
            grdKDKESATUAN.Properties.DataSource = oKESATUAN.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDKESATUAN.Properties.ValueMember = "KDKESATUAN"
            grdKDKESATUAN.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKunjunganPangkat()
        Dim oPANGKAT As New Reference.clsPangkat
        Try
            grdKDPANGKAT.Properties.DataSource = oPANGKAT.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDPANGKAT.Properties.ValueMember = "KDPANGKAT"
            grdKDPANGKAT.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKunjunganGolongan()
        Dim oGOLONGAN As New Reference.clsGolongan
        Try
            grdKDGOLONGAN.Properties.DataSource = oGOLONGAN.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDGOLONGAN.Properties.ValueMember = "KDGOLONGAN"
            grdKDGOLONGAN.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKunjunganPendidikan()
        Dim oPENDIDIKAN As New Reference.clsPendidikan
        Try
            grdKDPENDIDIKAN.Properties.DataSource = oPENDIDIKAN.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDPENDIDIKAN.Properties.ValueMember = "KDPENDIDIKAN"
            grdKDPENDIDIKAN.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKunjunganPekerjaan()
        Dim oPEKERJAAN As New Reference.clsPekerjaan
        Try
            grdKDPEKERJAAN.Properties.DataSource = oPEKERJAAN.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDPEKERJAAN.Properties.ValueMember = "KDPEKERJAAN"
            grdKDPEKERJAAN.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKunjunganPerusahaan()
        Dim oPERUSAHAAN As New Reference.clsPerusahaan
        Try
            grdKDPERUSAHAAN.Properties.DataSource = oPERUSAHAAN.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDPERUSAHAAN.Properties.ValueMember = "KDPERUSAHAAN"
            grdKDPERUSAHAAN.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKunjunganKeluarga()
        Dim oSTATUSKELUARGA As New Reference.clsStatusKeluarga
        Try
            grdKDSTATUSKELUARGA.Properties.DataSource = oSTATUSKELUARGA.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDSTATUSKELUARGA.Properties.ValueMember = "KDSTATUSKELUARGA"
            grdKDSTATUSKELUARGA.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadHistoryPasien(ByVal sKDCUSTOMER As String)
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
            SQL &= "NomorPendaftaran = A.KDPENDAFTARAN "
            SQL &= ",Tanggal = A.DATE "
            SQL &= ",Tujuan = C.NAME_DISPLAY  "
            SQL &= ",Dokter = D.NAME_DISPLAY "
            SQL &= ",B.KDKUNJUNGAN "
            SQL &= ",NoKontrol = ISNULL((SELECT top 1 KDSKD FROM S_PENDAFTARAN_SKD WHERE A.KDPENDAFTARAN = KDPENDAFTARAN AND ISONLINE = 1 ), '') "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_H A "
            SQL &= "INNER JOIN S_PENDAFTARAN_KUNJUNGAN B "
            SQL &= "ON A.KDPENDAFTARAN = B.KDPENDAFTARAN "
            SQL &= "INNER JOIN M_DEPARTMENT C "
            SQL &= "ON B.KDDEPARTMENT = C.KDDEPARTMENT "
            SQL &= "INNER JOIN M_DOCTOR D "
            SQL &= "ON B.KDDOCTOR = D.KDDOCTOR "
            SQL &= "WHERE A.KDCUSTOMER = '" & sKDCUSTOMER & "' "
            SQL &= "ORDER BY A.DATE DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "HISTORY")

            Dim listPendaftaran As New List(Of DataAccess.R_RIWAYAT_DAFTAR)

            For iLoop As Integer = 0 To ds.Tables("HISTORY").Rows.Count - 1
                Dim dsRekap As New DataAccess.R_RIWAYAT_DAFTAR
                With ds.Tables("HISTORY")
                    dsRekap.NomorPendaftaran = .Rows(iLoop)("NomorPendaftaran")
                    dsRekap.Tanggal = .Rows(iLoop)("Tanggal")
                    dsRekap.Tujuan = .Rows(iLoop)("Tujuan")
                    dsRekap.Dokter = .Rows(iLoop)("Dokter")
                    dsRekap.KDKUNJUNGAN = .Rows(iLoop)("KDKUNJUNGAN")
                    dsRekap.NoKontrol = .Rows(iLoop)("NoKontrol")
                    dsRekap.ISDAFTAR = True
                    listPendaftaran.Add(dsRekap)
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            'Dim oConnNpgsql As New NpgsqlConnection
            'Dim oCommNpgsql As New NpgsqlCommand
            'Dim daNpgsql As NpgsqlDataAdapter
            'Dim dsNpgsql As New DataSet
            'Dim SQLNpgsql As String

            'Dim sConnNpgsql As String = sConnNpgsqlproduction

            'oConnNpgsql = New NpgsqlConnection(sConnNpgsql)

            'If oConnNpgsql.State = ConnectionState.Closed Then
            '    oConnNpgsql.Open()
            'End If

            'SQLNpgsql = "SELECT "
            'SQLNpgsql &= "A.noregistrasi as nomorendaftaran "
            'SQLNpgsql &= ",A.tglregistrasi as tanggal "
            'SQLNpgsql &= ",C.namaruangan as tujuan  "
            'SQLNpgsql &= ",B.namalengkap as dokter "
            ''SQLNpgsql &= ",B.KDKUNJUNGAN "
            'SQLNpgsql &= ",F.nosuratkontrol as nokontrol "
            'SQLNpgsql &= "FROM "
            'SQLNpgsql &= "pasiendaftar_t A "
            'SQLNpgsql &= "INNER JOIN pegawai_m B ON A.objectpegawaifk = B.id "
            'SQLNpgsql &= "INNER JOIN ruangan_m C ON A.objectruanganasalfk = C.id "
            'SQLNpgsql &= "INNER JOIN pasien_m D ON A.nocmfk = D.id "
            'SQLNpgsql &= "LEFT JOIN bpjsrujukan_t E ON E.nokartu = D.nobpjs "
            'SQLNpgsql &= "LEFT JOIN bpjsrencanakontrol_t F ON F.nokartu = D.nobpjs "
            'SQLNpgsql &= "where A.nocmfk  = '" & sKDCUSTOMER & "' "


            'oCommNpgsql.Connection = oConnNpgsql
            'oCommNpgsql.CommandText = SQLNpgsql
            'oCommNpgsql.CommandTimeout = 120
            'oCommNpgsql.CommandType = CommandType.Text

            'daNpgsql = New NpgsqlDataAdapter(oCommNpgsql)
            'daNpgsql.Fill(dsNpgsql, "HISTORY_2")


            'For iLoop As Integer = 0 To dsNpgsql.Tables("HISTORY_2").Rows.Count - 1
            '    Dim dsRekap As New DataAccess.R_RIWAYAT_DAFTAR
            '    With dsNpgsql.Tables("HISTORY_2")
            '        dsRekap.NomorPendaftaran = .Rows(iLoop)("nomorendaftaran")
            '        dsRekap.Tanggal = .Rows(iLoop)("tanggal")
            '        dsRekap.Tujuan = .Rows(iLoop)("tujuan")
            '        dsRekap.Dokter = .Rows(iLoop)("dokter")
            '        dsRekap.KDKUNJUNGAN = ""
            '        dsRekap.NoKontrol = .Rows(iLoop)("nokontrol")
            '        dsRekap.ISDAFTAR = False
            '        listPendaftaran.Add(dsRekap)
            '    End With
            'Next

            'If oConnNpgsql.State = ConnectionState.Open Then
            '    oConnNpgsql.Close()
            'End If

            grdHistoryPasien.MainView = grvHistoryPasien
            grdHistoryPasien.DataSource = listPendaftaran
            grdHistoryPasien.ForceInitialize()

            fn_LoadFormatDataAll()
            grvHistoryPasien.BestFitColumns()

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatDataAll()
        For iLoop As Integer = 0 To grvHistoryPasien.Columns.Count - 1
            If grvHistoryPasien.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvHistoryPasien.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvHistoryPasien.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grvHistoryPasien.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

                grvHistoryPasien.Columns(iLoop).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                grvHistoryPasien.Columns(iLoop).SummaryItem.DisplayFormat = "{0:n2}"
            ElseIf grvHistoryPasien.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvHistoryPasien.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvHistoryPasien.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next
        grvHistoryPasien.Columns("KDKUNJUNGAN").VisibleIndex = -1
        grvHistoryPasien.Columns("ISDAFTAR").VisibleIndex = -1
    End Sub
    Private Sub fn_LoadKDSKDByKDCUSTOMER(ByVal sKDCUSTOMER As String)
        Dim oSKD As New Admission.clsSKD
        Dim dsSKD = oSKD.GetDataByRM(sKDCUSTOMER)
        If dsSKD IsNot Nothing Then
            'Right(testString, 6)
            txtNOMORSKDP.Text = dsSKD.KDSKD
            grdKDDOCTOR_SKD.Text = dsSKD.KDDOCTOR
            If txtNOMORRUJUKAN.Text = "" Then
                txtNOMORRUJUKAN.Text = dsSKD.NOMORRUJUKAN
            End If
        Else
            If grdKDDOCTOR_SKD.Text = String.Empty Then
                txtNOMORSKDP.ResetText()
                grdKDDOCTOR_SKD.ResetText()

                'txtNOMORRUJUKAN.ResetText()

                'Dim sMODUL As String = ""
                'Dim sLASTNUMBER As Integer = 0

                'If rbCATEGORY.SelectedIndex = 0 Then
                '    If chkIsOfline.Checked = False Then
                '        sMODUL = "SKD-RJ"
                '        Try
                '            Dim oCounter As New Setting.clsCounter
                '            sLASTNUMBER = oCounter.GetLastNumber(sMODUL, deDATE.DateTime)
                '            If sLASTNUMBER = 0 Then
                '                Try
                '                    oCounter.InsertData(sMODUL, deDATE.DateTime)
                '                    sLASTNUMBER = oCounter.GetLastNumber(sMODUL, deDATE.DateTime)
                '                Catch ex As Exception
                '                    sLASTNUMBER = 0
                '                End Try
                '            End If

                '            txtNOMORSKDP.Text = "Auto-" & AutoNumber(sMODUL, sLASTNUMBER + 1, deDATE.DateTime)

                '            oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(deDATE.DateTime), Year(deDATE.DateTime))

                '        Catch ex As Exception
                '            'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                '            'Throw ex
                '        End Try
                '    End If

                'End If

            End If
        End If
    End Sub
    Public Function AutoNumber(ByVal sKDCOUNTER As String, ByVal sLASTNUMBER As Integer, ByVal sDATE As DateTime) As String
        Dim sMonth As String = String.Empty

        Select Case Month(sDATE)
            Case 1
                sMonth = "A"
            Case 2
                sMonth = "B"
            Case 3
                sMonth = "C"
            Case 4
                sMonth = "D"
            Case 5
                sMonth = "E"
            Case 6
                sMonth = "F"
            Case 7
                sMonth = "G"
            Case 8
                sMonth = "H"
            Case 9
                sMonth = "I"
            Case 10
                sMonth = "J"
            Case 11
                sMonth = "K"
            Case 12
                sMonth = "L"
        End Select

        If sLASTNUMBER < 10 Then
            AutoNumber = sKDCOUNTER & Year(sDATE) & sMonth & "00000" & sLASTNUMBER.ToString
        ElseIf sLASTNUMBER < 100 Then
            AutoNumber = sKDCOUNTER & Year(sDATE) & sMonth & "0000" & sLASTNUMBER.ToString
        ElseIf sLASTNUMBER < 1000 Then
            AutoNumber = sKDCOUNTER & Year(sDATE) & sMonth & "000" & sLASTNUMBER.ToString
        ElseIf sLASTNUMBER < 10000 Then
            AutoNumber = sKDCOUNTER & Year(sDATE) & sMonth & "00" & sLASTNUMBER.ToString
        ElseIf sLASTNUMBER < 100000 Then
            AutoNumber = sKDCOUNTER & Year(sDATE) & sMonth & "0" & sLASTNUMBER.ToString
        Else
            AutoNumber = sKDCOUNTER & Year(sDATE) & sMonth & sLASTNUMBER.ToString
        End If
    End Function
    Private Sub fn_LoadKDSKD(ByVal sSKD As String)
        Dim oSKD As New Admission.clsSKD
        Dim dsSKD = oSKD.GetData(sSKD)
        If dsSKD IsNot Nothing Then
            grdKDDEPARTMENT.Text = dsSKD.KDDEPARTMENT
            fn_LoadDoctor(dsSKD.KDDEPARTMENT)
            txtNOMORSKDP.Text = dsSKD.KDSKD
            grdKDDOCTOR_SKD.Text = dsSKD.KDDOCTOR
            grdKDDOCTOR.Text = dsSKD.KDDOCTOR
            grdKDKELASRAWAT.Text = dsSKD.S_PENDAFTARAN_H.KDKELASRAWAT
            deDATE_RUJUKAN.DateTime = dsSKD.S_PENDAFTARAN_H.DATE_RUJUKAN

            If dsSKD.S_PENDAFTARAN_H.CATEGORY = 0 Then
                cboTUJUANKUNJUNGAN.SelectedIndex = 2
                cboASESMENPELAYANAN.SelectedIndex = 4
                If txtNOMORRUJUKAN.Text = "" Then
                    txtNOMORRUJUKAN.Text = dsSKD.NOMORRUJUKAN
                End If

            Else
                cboTUJUANKUNJUNGAN.SelectedIndex = 0

                If txtNOMORRUJUKAN.Text = "" Then
                    txtNOMORRUJUKAN.Text = dsSKD.NOMORSEP
                End If

                Dim oFaskes As New Reference.clsPPK
                cboASALRUJUKAN.SelectedIndex = oFaskes.GetDataKodeFaskes(sPPKPELAYANAN).JENISFASKES
                grdKDPPK.Text = oFaskes.GetDataKodeFaskes(sPPKPELAYANAN).KDPPK
            End If

            txtKARTUBPJS.Text = dsSKD.S_PENDAFTARAN_H.KARTUBPJS
            fn_GetDataCustomer(dsSKD.S_PENDAFTARAN_H.KDCUSTOMER)
            grdKDDIAGNOSA.Text = dsSKD.S_PENDAFTARAN_H.KDDIAGNOSA

            Try
                Dim uTime As Integer = 0

                If sVclaim_ConsId <> "" Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                    Dim dsSetKoneksi = oSetKoneksi.GetDataVClaimPesertaNoKartuBPJS(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, txtKARTUBPJS.Text, Now.ToString("yyyy-MM-dd"))

                    Dim allData = JObject.Parse(dsSetKoneksi)
                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString


                    If CodeResponse = "200" Then
                        Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), sVclaim_ConsId & sVclaim_SecreatKey & uTime))
                        Dim oKelas As New Reference.clsKelasRawat
                        grdKDKELASRAWAT.Text = oKelas.GetDataByKode(DataDecrypt("peserta")("hakKelas")("kode").ToString()).KDKELASRAWAT

                        If txtKDCUSTOMER.Text = "" Then
                            txtKDCUSTOMER.Text = DataDecrypt("peserta")("mr")("noMR")
                        End If

                        Dim NoTelepon = DataDecrypt("peserta")("mr")("noTelepon").ToString()
                        If NoTelepon <> "" Then
                            If txtNOMORTELEPON.Text = "" Then
                                txtNOMORTELEPON.Text = NoTelepon
                            End If
                            If txtNOMORTELEPON.Text = "000000000000" Then
                                txtNOMORTELEPON.Text = NoTelepon
                            End If
                        End If

                        Dim jenisPeserta As String = DataDecrypt("peserta")("jenisPeserta")("keterangan").ToString()
                        Dim kodejenisPeserta As String = DataDecrypt("peserta")("jenisPeserta")("kode").ToString()

                        If jenisPeserta <> "" Then
                            InsertJenisPeserta(kodejenisPeserta, jenisPeserta)
                        End If

                        'Insert

                        Dim nmAsuransi As String = DataDecrypt("peserta")("cob")("nmAsuransi").ToString()
                        Dim noAsuransi As String = DataDecrypt("peserta")("cob")("noAsuransi").ToString()
                        Dim tglTAT As String = DataDecrypt("peserta")("cob")("tglTAT").ToString()
                        Dim tglTMT As String = DataDecrypt("peserta")("cob")("tglTMT").ToString()

                        Dim nmProvider As String = DataDecrypt("peserta")("provUmum")("nmProvider").ToString()
                        Dim kodeProvider As String = DataDecrypt("peserta")("provUmum")("kdProvider").ToString()

                        If nmAsuransi <> "" Then
                            InsertCOB(noAsuransi, nmAsuransi, tglTAT, tglTMT)
                        End If

                        If nmProvider <> "" Then
                            InsertPPK(kodeProvider, nmProvider)
                        End If

                        Dim sJudul As String = String.Empty
                        Dim sMessage As String = String.Empty

                        sMessage = "* Peserta COB " & vbCrLf _
                             & "  - Nama Auransi: " & DataDecrypt("peserta")("cob")("nmAsuransi").ToString() & vbCrLf _
                             & "  - No Auransi: " & DataDecrypt("peserta")("cob")("noAsuransi").ToString() & vbCrLf _
                             & "  - Tgl TAT : " & DataDecrypt("peserta")("cob")("tglTAT").ToString() & vbCrLf _
                             & "  - Tgl TMT: " & DataDecrypt("peserta")("cob")("tglTMT").ToString() & vbCrLf _
                             & "" & vbCrLf _
                             & "* Informasi Dinsos " & vbCrLf _
                             & "  - Dinsos: " & DataDecrypt("peserta")("informasi")("dinsos").ToString() & vbCrLf _
                             & "  - No SKTM: " & DataDecrypt("peserta")("informasi")("noSKTM").ToString() & vbCrLf _
                             & "  - Prolanis: PRB " & DataDecrypt("peserta")("informasi")("prolanisPRB").ToString() & vbCrLf _
                             & "" & vbCrLf _
                             & "Tgl Cetak Kartu: " & DataDecrypt("peserta")("tglCetakKartu").ToString() & vbCrLf _
                             & "Tgl TAT: " & DataDecrypt("peserta")("tglTAT").ToString() & vbCrLf _
                             & "Tgl TMT: " & DataDecrypt("peserta")("tglTMT").ToString()

                        sJudul = DataDecrypt("peserta")("statusPeserta")("keterangan").ToString() & " !!!"
                        Dim Message As New frmPesertaBPJS
                        Message.LoadMe(sJudul, DataDecrypt("peserta")("nama").ToString(), DataDecrypt("peserta")("jenisPeserta")("keterangan").ToString(), DataDecrypt("peserta")("noKartu").ToString(), txtKDCUSTOMER.Text.ToString(), DataDecrypt("peserta")("mr")("noMR").ToString(), IIf(DataDecrypt("peserta")("sex").ToString() = "L", "Laki-Laki", "Perempuan"), DataDecrypt("peserta")("tglLahir").ToString(), DataDecrypt("peserta")("umur")("umurSekarang").ToString(), DataDecrypt("peserta")("provUmum")("kdProvider").ToString() & " - " & DataDecrypt("peserta")("provUmum")("nmProvider").ToString(), sMessage)
                        Message.ShowDialog(Me)

                    Else
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                    End If

                End If

            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Else
            Try
                Dim uTime As Integer = 0

                If sVclaim_ConsId <> "" Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                    Dim dsSetKoneksi = oSetKoneksi.CariNomorRencanaKontrol(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, sSKD)

                    If dsSetKoneksi <> "" Then
                        Dim allData = JObject.Parse(dsSetKoneksi)

                        Dim CodeResponse As String = String.Empty
                        Dim messageResponse As String = String.Empty

                        CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                        messageResponse = allData("metaData")("message").ToString

                        If CodeResponse = "200" Then
                            'txtNOMORSKDP.ResetText()
                            grdKDDOCTOR_SKD.ResetText()
                            grdKDDOCTOR.ResetText()
                            'txtNOMORRUJUKAN.ResetText()

                            grdKDDIAGNOSA.ResetText()
                            txtKARTUBPJS.ResetText()

                            Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), sVclaim_ConsId & sVclaim_SecreatKey & uTime))
                            'MsgBox(CodeResponse & " - " & DataDecrypt.ToString(), MsgBoxStyle.Exclamation, Me.Text)

                            Dim oDepartment As New Reference.clsDepartment
                            Dim dsDepartment = oDepartment.GetDataByKodeVclaim(DataDecrypt.Item("poliTujuan").ToString())

                            If dsDepartment IsNot Nothing Then
                                Dim oDoctor As New Reference.clsDoctor

                                grdKDDEPARTMENT.Text = dsDepartment.KDDEPARTMENT
                                fn_LoadDoctor(dsDepartment.KDDEPARTMENT)

                                Dim dsDoctor = oDoctor.GetDataByKodeVclaim(DataDecrypt.Item("kodeDokter").ToString())
                                If dsDoctor IsNot Nothing Then
                                    grdKDDOCTOR_SKD.Text = dsDoctor.KDDOCTOR
                                    grdKDDOCTOR.Text = dsDoctor.KDDOCTOR
                                End If
                            End If

                            'DataDecrypt.Item("sep")("peserta")("noKartu").ToString(), True

                            txtKARTUBPJS.Text = DataDecrypt.Item("sep")("peserta")("noKartu").ToString()
                            'Dim oKelas As New Reference.clsKelasRawat

                            'grdKDKELASRAWAT.Text = oKelas.GetDataByKode(DataDecrypt("sep")("peserta")("hakKelas").ToString()).KDKELASRAWAT

                            Dim oCustomer As New Reference.clsCustomer
                            Dim KDCUSTOMER As String = String.Empty
                            Dim dsCustomer = oCustomer.GetDataKARTUBPJS(txtKARTUBPJS.Text.ToString.Trim.ToUpper)
                            If dsCustomer IsNot Nothing Then
                                KDCUSTOMER = dsCustomer.KDCUSTOMER
                                fn_GetDataCustomer(KDCUSTOMER)
                            End If

                            Dim oDiagnosa As New Reference.clsDiagnosa
                            Dim dsdiagnosa = oDiagnosa.GetDataByMemo(DataDecrypt.Item("sep")("diagnosa").ToString())
                            If dsdiagnosa IsNot Nothing Then
                                grdKDDIAGNOSA.Text = dsdiagnosa.KDDIAGNOSA
                            End If

                            If sNomorSKDPspriSEP <> "" Then
                                Dim oFaskes As New Reference.clsPPK
                                Dim dsCekFaskes = oFaskes.GetDataKodeFaskes(sPPKPELAYANAN)
                                If dsCekFaskes IsNot Nothing Then
                                    cboASALRUJUKAN.SelectedIndex = dsCekFaskes.JENISFASKES
                                    grdKDPPK.Text = dsCekFaskes.KDPPK
                                End If
                            End If
                        Else
                            MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                        End If
                    Else
                        MsgBox("Load Data Gagal", MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
                End If

                Try
                    If sVclaim_ConsId <> "" Then
                        uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                        Dim dsSetKoneksi = oSetKoneksi.GetDataVClaimPesertaNoKartuBPJS(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, txtKARTUBPJS.Text, Now.ToString("yyyy-MM-dd"))

                        Dim allData = JObject.Parse(dsSetKoneksi)
                        Dim CodeResponse As String = String.Empty
                        Dim messageResponse As String = String.Empty

                        CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                        messageResponse = allData("metaData")("message").ToString


                        If CodeResponse = "200" Then
                            Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), sVclaim_ConsId & sVclaim_SecreatKey & uTime))
                            Dim oKelas As New Reference.clsKelasRawat
                            grdKDKELASRAWAT.Text = oKelas.GetDataByKode(DataDecrypt("peserta")("hakKelas")("kode").ToString()).KDKELASRAWAT

                            If txtKDCUSTOMER.Text = "" Then
                                txtKDCUSTOMER.Text = DataDecrypt("peserta")("mr")("noMR")
                            End If

                            Dim NoTelepon = DataDecrypt("peserta")("mr")("noTelepon").ToString()
                            If NoTelepon <> "" Then
                                If txtNOMORTELEPON.Text = "" Then
                                    txtNOMORTELEPON.Text = NoTelepon
                                End If
                                If txtNOMORTELEPON.Text = "000000000000" Then
                                    txtNOMORTELEPON.Text = NoTelepon
                                End If
                            End If

                            Dim jenisPeserta As String = DataDecrypt("peserta")("jenisPeserta")("keterangan").ToString()
                            Dim kodejenisPeserta As String = DataDecrypt("peserta")("jenisPeserta")("kode").ToString()

                            If jenisPeserta <> "" Then
                                InsertJenisPeserta(kodejenisPeserta, jenisPeserta)
                            End If

                            'Insert

                            Dim nmAsuransi As String = DataDecrypt("peserta")("cob")("nmAsuransi").ToString()
                            Dim noAsuransi As String = DataDecrypt("peserta")("cob")("noAsuransi").ToString()
                            Dim tglTAT As String = DataDecrypt("peserta")("cob")("tglTAT").ToString()
                            Dim tglTMT As String = DataDecrypt("peserta")("cob")("tglTMT").ToString()

                            Dim nmProvider As String = DataDecrypt("peserta")("provUmum")("nmProvider").ToString()
                            Dim kodeProvider As String = DataDecrypt("peserta")("provUmum")("kdProvider").ToString()

                            If nmAsuransi <> "" Then
                                InsertCOB(noAsuransi, nmAsuransi, tglTAT, tglTMT)
                            End If

                            If nmProvider <> "" Then
                                InsertPPK(kodeProvider, nmProvider)
                            End If

                            Dim sJudul As String = String.Empty
                            Dim sMessage As String = String.Empty

                            sMessage = "* Peserta COB " & vbCrLf _
                                 & "  - Nama Auransi: " & DataDecrypt("peserta")("cob")("nmAsuransi").ToString() & vbCrLf _
                                 & "  - No Auransi: " & DataDecrypt("peserta")("cob")("noAsuransi").ToString() & vbCrLf _
                                 & "  - Tgl TAT : " & DataDecrypt("peserta")("cob")("tglTAT").ToString() & vbCrLf _
                                 & "  - Tgl TMT: " & DataDecrypt("peserta")("cob")("tglTMT").ToString() & vbCrLf _
                                 & "" & vbCrLf _
                                 & "* Informasi Dinsos " & vbCrLf _
                                 & "  - Dinsos: " & DataDecrypt("peserta")("informasi")("dinsos").ToString() & vbCrLf _
                                 & "  - No SKTM: " & DataDecrypt("peserta")("informasi")("noSKTM").ToString() & vbCrLf _
                                 & "  - Prolanis: PRB " & DataDecrypt("peserta")("informasi")("prolanisPRB").ToString() & vbCrLf _
                                 & "" & vbCrLf _
                                 & "Tgl Cetak Kartu: " & DataDecrypt("peserta")("tglCetakKartu").ToString() & vbCrLf _
                                 & "Tgl TAT: " & DataDecrypt("peserta")("tglTAT").ToString() & vbCrLf _
                                 & "Tgl TMT: " & DataDecrypt("peserta")("tglTMT").ToString()

                            sJudul = DataDecrypt("peserta")("statusPeserta")("keterangan").ToString() & " !!!"
                            Dim Message As New frmPesertaBPJS
                            Message.LoadMe(sJudul, DataDecrypt("peserta")("nama").ToString(), DataDecrypt("peserta")("jenisPeserta")("keterangan").ToString(), DataDecrypt("peserta")("noKartu").ToString(), txtKDCUSTOMER.Text.ToString, DataDecrypt("peserta")("mr")("noMR").ToString(), IIf(DataDecrypt("peserta")("sex").ToString() = "L", "Laki-Laki", "Perempuan"), DataDecrypt("peserta")("tglLahir").ToString(), DataDecrypt("peserta")("umur")("umurSekarang").ToString(), DataDecrypt("peserta")("provUmum")("kdProvider").ToString() & " - " & DataDecrypt("peserta")("provUmum")("nmProvider").ToString(), sMessage)
                            Message.ShowDialog(Me)

                        Else
                            MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                        End If

                    End If

                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try

            'MsgBox("Nomor SKD Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            'txtNOMORSKDP.ResetText()
            'grdKDDOCTOR_SKD.ResetText()
            'txtNOMORRUJUKAN.ResetText()
        End If
    End Sub
    Private Sub fn_LoadLakaLantas()
        If chkLakaLantas.Checked = True Then
            chkPenjamin1.Properties.ReadOnly = False
            chkPenjamin2.Properties.ReadOnly = False
            chkPenjamin3.Properties.ReadOnly = False
            chkPenjamin4.Properties.ReadOnly = False
            deDATE_PENJAMIN_TGLKEJADIAN.Properties.ReadOnly = False
            txtJAMINAN_PENJAMIN_KETERANGAN.Properties.ReadOnly = False
            chkISSUPLESI.Properties.ReadOnly = False

        Else
            chkPenjamin1.Properties.ReadOnly = True
            chkPenjamin2.Properties.ReadOnly = True
            chkPenjamin3.Properties.ReadOnly = True
            chkPenjamin4.Properties.ReadOnly = True
            deDATE_PENJAMIN_TGLKEJADIAN.Properties.ReadOnly = True
            txtJAMINAN_PENJAMIN_KETERANGAN.Properties.ReadOnly = True
            chkISSUPLESI.Properties.ReadOnly = True

        End If
    End Sub
    Private Sub fn_LoadSuplesi()
        If chkISSUPLESI.Checked = True Then
            txtCARISUPLESI.Properties.ReadOnly = False
            grdSUPLESI.Properties.ReadOnly = False
            txtPENJAMIN_SUPLESI_NOSEPSUPLESI.Properties.ReadOnly = False
        Else
            txtCARISUPLESI.Properties.ReadOnly = True
            grdSUPLESI.Properties.ReadOnly = True
            txtPENJAMIN_SUPLESI_NOSEPSUPLESI.Properties.ReadOnly = True
        End If
    End Sub
    Private Sub fn_LoadParameter(ByVal sParameter As String, ByVal sCategori As Integer)
        Try
            If sParameter = String.Empty Then Exit Sub

            Dim oCustomer As New Reference.clsCustomer

            Dim dsCustomerList = From x In oCustomer.GetDataList(sParameter, sCategori)
                                 Select x.KDCUSTOMER, x.KARTUBPJS, x.NAME_DISPLAY, x.TANGGALLAHIR, ALAMAT = x.ALAMAT & " KELURAHAN : " & x.M_KELURAHAN.MEMO & " Kode Pos : " & x.M_KELURAHAN.KODEPOS & " KECAMATAN : " & x.M_KELURAHAN.M_KECAMATAN.MEMO & " KABUPATEN/KOTA : " & x.M_KELURAHAN.M_KECAMATAN.M_KABUPATEN.MEMO & " PROPINSI : " & x.M_KELURAHAN.M_KECAMATAN.M_KABUPATEN.M_PROPINSI.MEMO

            grdCARI.Properties.DataSource = dsCustomerList.ToList()
            grdCARI.Properties.ValueMember = "KDCUSTOMER"
            grdCARI.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
#Region "Event"
    Private Sub txtCARI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCARI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If txtCARI.Text = String.Empty Then Exit Sub

            grvCARI.Columns.Clear()
            grdCARI.Properties.DataSource = Nothing
            grvCARI.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            'No.Rekam Medis
            'No.Rujukan
            'No.Kartu BPJS(1 Record)
            'No.Kartu BPJS(Multi Record)
            'No.NIK
            'Nama Pasien
            'Alamat
            'SKD
            'Register Rawat Jalan

            If rbCATEGORY.SelectedIndex = 0 Then
                Select Case cboCARI.SelectedIndex
                    Case 0
                        fn_LoadParameter(txtCARI.Text.ToString.Trim, 0)
                        grdCARI.ShowPopup()
                    Case 1
                        fn_LoadNomorRujukan(txtCARI.Text.ToString.Trim.ToUpper)
                    Case 2
                        Dim oCustomer As New Reference.clsCustomer
                        Dim KDCUSTOMER As String = String.Empty
                        Dim dsCustomer = oCustomer.GetDataKARTUBPJS(txtCARI.Text.ToString.Trim.ToUpper)
                        If dsCustomer IsNot Nothing Then
                            KDCUSTOMER = dsCustomer.KDCUSTOMER
                        End If

                        fn_GetDataCustomer(KDCUSTOMER)
                        fn_LoadKartuBPJS(txtCARI.Text.ToString.Trim.ToUpper)
                        fn_LoadKartuBPJSSatuRecord(txtCARI.Text.ToString.Trim.ToUpper)
                    Case 3
                        fn_LoadKartuBPJSMultiRecord(txtCARI.Text.ToString.Trim.ToUpper)
                        grdCARI.ShowPopup()
                    Case 4
                        fn_LoadParameter(txtCARI.Text.ToString.Trim, 1)
                        grdCARI.ShowPopup()

                        'Dim oCustomer As New Reference.clsCustomer
                        'Dim KDCUSTOMER As String = String.Empty
                        'Dim dsCustomer = oCustomer.GetDataKTP(txtCARI.Text.ToString.Trim.ToUpper)
                        'If dsCustomer IsNot Nothing Then
                        '    KDCUSTOMER = dsCustomer.KDCUSTOMER
                        'End If

                        'fn_GetDataCustomer(KDCUSTOMER, True)
                        'fn_LoadKTP(txtCARI.Text.ToString.Trim.ToUpper)
                    Case 5
                        fn_LoadParameter(txtCARI.Text.ToString.Trim.ToUpper, 2)
                        grdCARI.ShowPopup()
                    Case 6
                        fn_LoadParameter(txtCARI.Text.ToString.Trim.ToUpper, 4)
                        grdCARI.ShowPopup()
                    Case 7
                        If txtCARI.Text <> "" Then
                            fn_LoadKDSKD(txtCARI.Text.ToString.Trim.ToUpper)
                        End If
                    Case 8
                        MsgBox("Pencarian Hanya Untuk Rawat Inap", MsgBoxStyle.Information, Me.Text)
                End Select
            Else
                Select Case cboCARI.SelectedIndex
                    Case 7, 3
                        MsgBox("Pencarian Hanya Untuk Rawat Jalan", MsgBoxStyle.Information, Me.Text)
                End Select

                'No.Rekam Medis
                'No.Rujukan
                'No.Kartu BPJS(1 Record)
                'No.Kartu BPJS(Multi Record)
                'No.NIK
                'NAMA Pasien
                'Alamat
                'SKD
                'Register Rawat Jalan

                Try
                    Dim dsPendaftaran = From x In oPendaftaranA.GetDataByPendaftaranRJ(txtCARI.Text.ToString.Trim.ToUpper, cboCARI.SelectedIndex)
                                        Where x.CATEGORY = 0 And x.DATE >= deDATE.DateTime.ToString("yyyy-MM-dd") & " 00:00:00"
                                        Select x.KDPENDAFTARAN, x.KDCUSTOMER, x.KARTUBPJS, x.M_CUSTOMER.NAME_DISPLAY, x.M_CUSTOMER.TANGGALLAHIR, ALAMAT = x.M_CUSTOMER.ALAMAT & " KELURAHAN : " & x.M_CUSTOMER.M_KELURAHAN.MEMO & " Kode Pos : " & x.M_CUSTOMER.M_KELURAHAN.KODEPOS & " KECAMATAN : " & x.M_CUSTOMER.M_KELURAHAN.M_KECAMATAN.MEMO & " KABUPATEN/KOTA : " & x.M_CUSTOMER.M_KELURAHAN.M_KECAMATAN.M_KABUPATEN.MEMO & " PROPINSI : " & x.M_CUSTOMER.M_KELURAHAN.M_KECAMATAN.M_KABUPATEN.M_PROPINSI.MEMO

                    grdCARI.Properties.DataSource = dsPendaftaran.ToList()
                    grdCARI.Properties.ValueMember = "KDPENDAFTARAN"
                    grdCARI.Properties.DisplayMember = "KDPENDAFTARAN"

                    grdCARI.ShowPopup()

                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

        End If
    End Sub
    Private Sub grdCARI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdCARI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If grdCARI.Text = String.Empty Then Exit Sub

            'No.Rekam Medis
            'No.Rujukan
            'No.Kartu BPJS(1 Record)
            'No.Kartu BPJS(Multi Record)
            'No.NIK
            'Nama Pasien
            'Alamat
            'SKD
            'Register Rawat Jalan

            If rbCATEGORY.SelectedIndex = 0 Then
                Select Case cboCARI.SelectedIndex
                    Case 0
                        Dim KDCUSTOMER As String = String.Empty

                        KDCUSTOMER = grdCARI.EditValue
                        fn_GetDataCustomer(KDCUSTOMER)

                        If chkIsOfline.Checked = False Then
                            fn_LoadKartuBPJS(txtKARTUBPJS.Text)
                            fn_LoadKartuBPJSSatuRecord(txtKARTUBPJS.Text)
                        End If
                    Case 1
                        txtNOMORRUJUKAN.Text = grdCARI.EditValue
                    Case 2

                    Case 3
                        txtNOMORRUJUKAN.Text = grdCARI.EditValue
                    Case 4
                        Dim KDCUSTOMER As String = String.Empty

                        KDCUSTOMER = grdCARI.EditValue
                        fn_GetDataCustomer(KDCUSTOMER)

                        If chkIsOfline.Checked = False Then
                            fn_LoadKartuBPJS(txtKARTUBPJS.Text)
                            fn_LoadKartuBPJSSatuRecord(txtKARTUBPJS.Text)
                        End If
                    Case 5
                        Dim KDCUSTOMER As String = String.Empty

                        KDCUSTOMER = grdCARI.EditValue
                        fn_GetDataCustomer(KDCUSTOMER)

                        If chkIsOfline.Checked = False Then
                            fn_LoadKartuBPJS(txtKARTUBPJS.Text)
                            fn_LoadKartuBPJSSatuRecord(txtKARTUBPJS.Text)
                        End If
                    Case 6
                        Dim KDCUSTOMER As String = String.Empty

                        KDCUSTOMER = grdCARI.EditValue
                        fn_GetDataCustomer(KDCUSTOMER)

                        If chkIsOfline.Checked = False Then
                            fn_LoadKartuBPJS(txtKARTUBPJS.Text)
                            fn_LoadKartuBPJSSatuRecord(txtKARTUBPJS.Text)
                        End If
                    Case 7

                    Case 8
                        txtKDPENDAFTARAN_AWAL.Text = grdCARI.EditValue
                End Select
            Else
                Dim dsPendaftaranRawatJalan = oPendaftaranA.GetData(grdCARI.EditValue)

                If dsPendaftaranRawatJalan IsNot Nothing Then
                    grdKDDAFTAR_L1.Text = dsPendaftaranRawatJalan.KDDAFTAR_L1
                    fn_GetDataCustomer(dsPendaftaranRawatJalan.KDCUSTOMER)
                    txtKDPENDAFTARAN_AWAL.Text = dsPendaftaranRawatJalan.KDPENDAFTARAN
                    grdKDDAFTAR_L2.Text = dsPendaftaranRawatJalan.KDDAFTAR_L2
                    grdKDDAFTAR_L3.Text = dsPendaftaranRawatJalan.KDDAFTAR_L3
                    grdKDDAFTAR_L4.Text = dsPendaftaranRawatJalan.KDDAFTAR_L4
                    grdKDDAFTAR_L5.Text = dsPendaftaranRawatJalan.KDDAFTAR_L5
                    deDATE.DateTime = dsPendaftaranRawatJalan.DATE
                    txtKDCUSTOMER.Text = dsPendaftaranRawatJalan.KDCUSTOMER
                    txtKARTUBPJS.Text = dsPendaftaranRawatJalan.KARTUBPJS
                    txtKTP.Text = dsPendaftaranRawatJalan.KTP
                    grdKDDIAGNOSA.Text = dsPendaftaranRawatJalan.KDDIAGNOSA

                    Try
                        Dim oFaskes As New Reference.clsPPK
                        cboASALRUJUKAN.SelectedIndex = oFaskes.GetDataKodeFaskes(sPPKPELAYANAN).JENISFASKES
                        grdKDPPK.Text = oFaskes.GetDataKodeFaskes(sPPKPELAYANAN).KDPPK

                    Catch ex As Exception
                        Dim oFaskes As New Reference.clsPPK
                        cboASALRUJUKAN.SelectedIndex = oFaskes.GetDataKodeFaskes(sPPKPELAYANAN).JENISFASKES
                        grdKDPPK.Text = oFaskes.GetDataKodeFaskes(sPPKPELAYANAN).KDPPK
                    End Try

                    txtNOMORRUJUKAN.Text = dsPendaftaranRawatJalan.NOMORSEP
                    deDATE_RUJUKAN.DateTime = dsPendaftaranRawatJalan.DATE_RUJUKAN
                    txtNOMORSKDP.ResetText()
                    grdKDDOCTOR_SKD.ResetText()
                    grdKDCOB.Text = dsPendaftaranRawatJalan.KDCOB
                    chkCOB.Checked = dsPendaftaranRawatJalan.ISCOB
                    chkISEKSEKUTIF.Checked = dsPendaftaranRawatJalan.ISEKSEKUTIF
                    chkISKATARAK.Checked = dsPendaftaranRawatJalan.ISKATARAK
                    txtNOMORTELEPON.Text = dsPendaftaranRawatJalan.NOMORTELEPON
                    'If dsPendaftaranRawatJalan.M_KELASRAWAT.MEMO = "NON KELAS" Then
                    '    grdKDKELASRAWAT.ResetText()
                    'Else
                    '    grdKDKELASRAWAT.Text = dsPendaftaranRawatJalan.KDKELASRAWAT
                    'End If
                    'txtCATATAN.Text = dsPendaftaranRawatJalan.CATATAN
                Else
                    fn_EmptyMe()
                End If

            End If

        End If
    End Sub
    Private Sub cboCARI_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCARI.SelectedIndexChanged
        If cboCARI.SelectedIndex = 2 Or cboCARI.SelectedIndex = 3 Then
            txtCARI.Properties.MaxLength = 13
        Else
            txtCARI.Properties.MaxLength = 0
        End If

        txtCARI.ResetText()

    End Sub
    Private Sub txtKDCUSTOMER_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtKDCUSTOMER.KeyPress
        If Asc(e.KeyChar) = 13 Then
            fn_GetDataCustomer(txtKDCUSTOMER.Text.ToString.Trim.ToUpper)

            If chkIsOfline.Checked = False Then
                fn_LoadKartuBPJS(txtKARTUBPJS.Text)
                fn_LoadKartuBPJSSatuRecord(txtKARTUBPJS.Text)
            End If
        End If
    End Sub
    Private Sub txtKARTUBPJS_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtKARTUBPJS.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Dim oCustomer As New Reference.clsCustomer
            Dim KDCUSTOMER As String = String.Empty
            Dim dsCustomer = oCustomer.GetDataKARTUBPJS(txtKARTUBPJS.Text.ToString.Trim.ToUpper)
            If dsCustomer IsNot Nothing Then
                KDCUSTOMER = dsCustomer.KDCUSTOMER
            End If

            Dim KARTUBPJS As String = String.Empty
            KARTUBPJS = txtKARTUBPJS.Text.ToString.Trim.ToUpper

            fn_GetDataCustomer(KDCUSTOMER)

            If chkIsOfline.Checked = False Then
                fn_LoadKartuBPJSSatuRecord(KARTUBPJS)
                fn_LoadKartuBPJS(KARTUBPJS)
            End If

            If KDCUSTOMER = "" Then
                If txtKDCUSTOMER.Text <> "" Then
                    fn_GetDataCustomer(txtKDCUSTOMER.Text)
                End If
            End If
        End If
    End Sub
    Private Sub txtKTP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtKTP.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Dim oCustomer As New Reference.clsCustomer
            Dim KDCUSTOMER As String = String.Empty
            Dim dsCustomer = oCustomer.GetDataKTP(txtKTP.Text.ToString.Trim.ToUpper)
            If dsCustomer IsNot Nothing Then
                KDCUSTOMER = dsCustomer.KDCUSTOMER
            End If

            'Dim KTP As String = String.Empty
            'KTP = txtKARTUBPJS.Text.ToString.Trim.ToUpper

            'fn_GetDataCustomer(KDCUSTOMER, True)
            'fn_LoadKTP(KTP)

            KDCUSTOMER = txtKDCUSTOMER.Text.ToString.Trim
            fn_GetDataCustomer(KDCUSTOMER)

            If chkIsOfline.Checked = False Then
                fn_LoadKartuBPJS(txtKARTUBPJS.Text)
                fn_LoadKartuBPJSSatuRecord(txtKARTUBPJS.Text)
            End If
        End If
    End Sub
    Private Sub rbCATEGORY_SelectedIndexChanged() Handles rbCATEGORY.SelectedIndexChanged
        fn_LoadDepartment()
        grdKDDOCTOR.Properties.DataSource = Nothing
        If rbCATEGORY.SelectedIndex = 0 Then
            lPEMETAAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            lPEMETAAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
        If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            rbCATEGORY.Properties.ReadOnly = False
        Else
            rbCATEGORY.Properties.ReadOnly = True
        End If
    End Sub
    Private Sub grdKDDEPARTMENT_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdKDDEPARTMENT.KeyPress
        If Asc(e.KeyChar) = 13 Then
            fn_LoadDoctor(grdKDDEPARTMENT.EditValue)

            Dim oPOLI As New Reference.clsDepartment
            Dim dsPoli = oPOLI.GetData(grdKDDEPARTMENT.EditValue)

            If dsPoli IsNot Nothing Then
                If dsPoli.VCLAIM_KODEPOLI = "IGD" Then
                    cboASALRUJUKAN.SelectedIndex = 1
                    Dim oFaskes As New Reference.clsPPK
                    Dim dsFaskes = oFaskes.GetDataKodeFaskes(sPPKPELAYANAN)
                    If dsFaskes IsNot Nothing Then
                        grdKDPPK.Text = dsFaskes.KDPPK
                    End If
                End If

            End If

            grdKDDOCTOR.ShowPopup()

        End If
    End Sub
    Private Sub grdKDDOCTOR_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdKDDOCTOR.KeyPress
        If Asc(e.KeyChar) = 13 Then
            grdKDDOCTOR_SKD.Text = grdKDDOCTOR.EditValue
            grdKDDIAGNOSA.ShowPopup()
        End If
    End Sub
    'Private Sub grdKDDOCTOR_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDDOCTOR.EditValueChanged
    '    If isLoad = True Then
    '        If grdKDDOCTOR.Text <> "" Then
    '            grdKDDOCTOR_SKD.Text = grdKDDOCTOR.EditValue
    '            grdKDDIAGNOSA.ShowPopup()
    '        End If
    '    End If
    'End Sub
    Private Sub grdKDDAFTAR_L1_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDDAFTAR_L1.EditValueChanged
        If grdKDDAFTAR_L1.Text = "BPJS" Then
            chkIsOfline.Properties.ReadOnly = False
            txtKARTUBPJS.Properties.MaxLength = 13

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                chkIsOfline.Checked = False
            End If
        Else
            chkIsOfline.Properties.ReadOnly = True
            chkIsOfline.Checked = True
            txtKARTUBPJS.Properties.MaxLength = 0
        End If
        sLoadAwal = True
    End Sub
    Private Sub txtNOMORRUJUKAN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNOMORRUJUKAN.KeyPress
        If Asc(e.KeyChar) = 13 Then
            fn_LoadNomorRujukan(txtNOMORRUJUKAN.Text.ToString.Trim.ToUpper)
        End If
    End Sub
    Private Sub chkISEKSEKUTIF_Click(sender As Object, e As EventArgs) Handles chkISEKSEKUTIF.Click
        If grdKDDEPARTMENT.Text = String.Empty Then
            MsgBox("Bukan Eksekutif", MsgBoxStyle.Exclamation, Me.Text)
            chkISEKSEKUTIF.Checked = False
        End If
        Dim oDepartment As New Reference.clsDepartment
        Dim dsDepartment = oDepartment.GetData(grdKDDEPARTMENT.EditValue)
        If dsDepartment IsNot Nothing Then
            If dsDepartment.ISEKSEKUTIF = False Then
                MsgBox("Bukan Eksekutif", MsgBoxStyle.Exclamation, Me.Text)
                chkISEKSEKUTIF.Checked = False
            End If
        Else
            chkISEKSEKUTIF.Checked = False
        End If
    End Sub
    Private Sub chkISKATARAK_Click(sender As Object, e As EventArgs) Handles chkISKATARAK.Click
        If grdKDDEPARTMENT.Text = String.Empty Then
            MsgBox("Tidak bisa buka Katarak", MsgBoxStyle.Exclamation, Me.Text)
            chkISEKSEKUTIF.Checked = False
        End If
        Dim oDepartment As New Reference.clsDepartment
        Dim dsDepartment = oDepartment.GetData(grdKDDEPARTMENT.EditValue)
        If dsDepartment IsNot Nothing Then
            If dsDepartment.ISKATARAK = False Then
                MsgBox("Tidak bisa buka Katarak", MsgBoxStyle.Exclamation, Me.Text)
                chkISKATARAK.Checked = False
            End If
        Else
            chkISKATARAK.Checked = False
        End If
    End Sub
    Private Sub chkLakaLantas_Click(sender As Object, e As EventArgs) Handles chkLakaLantas.Click
        fn_LoadLakaLantas()
    End Sub
    Private Sub grdSUPLESI_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Asc(e.KeyChar) = 13 Then
            txtPENJAMIN_SUPLESI_NOSEPSUPLESI.Text = grdSUPLESI.Text
        End If
    End Sub
    Private Sub txtNOMORSKDP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNOMORSKDP.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If txtNOMORSKDP.Text <> "" Then
                fn_LoadKDSKD(txtNOMORSKDP.Text.ToString.Trim.ToUpper)
            End If
        End If
    End Sub
    Private Sub txtNOMORSEP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNOMORSEP.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If txtNOMORSEP.Text.Count = 19 Then
                fn_CariSEP(txtNOMORSEP.Text.ToString.Trim.ToUpper, True)
                'MsgBox(fn_CariSEP(txtNOMORSEP.Text.ToString.Trim.ToUpper), MsgBoxStyle.Information, Me.Text)
            ElseIf txtNOMORSEP.Text.Count > 19 Then
                MsgBox("Validasi : " & vbCrLf & "No SEP lebih dari 19 Digit", MsgBoxStyle.Exclamation, Me.Text)
            Else
                MsgBox("Validasi : " & vbCrLf & "No SEP kurang dari 19 Digit", MsgBoxStyle.Exclamation, Me.Text)
            End If
        End If
    End Sub
#End Region
#Region "Insert Reference"
    Private Function InsertDiagnosa(ByVal sKDDIAGNOSA As String, ByVal sMEMO As String) As Boolean
        Try
            Dim oDiagnosa As New Reference.clsDiagnosa

            InsertDiagnosa = True

            If oDiagnosa.IsExist(sKDDIAGNOSA) Then
                grdKDDIAGNOSA.EditValue = sKDDIAGNOSA
            Else
                Dim ds = oDiagnosa.GetStructureHeader
                With ds
                    .DATECREATED = Now
                    .DATEUPDATED = Now
                    .KDDIAGNOSA = sKDDIAGNOSA
                    .MEMO = sMEMO.ToString.Trim.ToUpper
                    .ISDEFAULT = IIf(oDiagnosa.CheckDefault(0, False) = False, True, False)
                    .ISACTIVE = True
                End With

                InsertDiagnosa = oDiagnosa.InsertData(ds)

                fn_LoadDiganosa()
                grdKDDIAGNOSA.EditValue = sKDDIAGNOSA
            End If

        Catch ex As Exception
            InsertDiagnosa = False
            MsgBox(ex.ToString)
        End Try
    End Function
    Private Function InsertPPK(ByVal sKODEFASKES As String, ByVal sMEMO As String) As Boolean
        Try
            If sNomorSKDPspriSEP = "" Then
                Dim oPPK As New Reference.clsPPK

                InsertPPK = True

                If oPPK.IsExistKodeFaskes(sKODEFASKES) Then
                    grdKDPPK.EditValue = oPPK.GetDataKodeFaskes(sKODEFASKES).KDPPK
                Else
                    Dim ds = oPPK.GetStructureHeader
                    With ds
                        .DATECREATED = Now
                        .DATEUPDATED = Now
                        .KDPPK = ""
                        .JENISFASKES = cboASALRUJUKAN.SelectedIndex
                        .KODEFASKES = sKODEFASKES
                        .MEMO = sMEMO
                        .ISDEFAULT = IIf(oPPK.CheckDefault(0, False) = False, True, False)
                        .ISACTIVE = True
                    End With

                    InsertPPK = oPPK.InsertData(ds)

                    fn_LoadFaskes()
                    grdKDPPK.EditValue = oPPK.GetDataKodeFaskes(sKODEFASKES).KDPPK
                End If
            End If
        Catch ex As Exception
            InsertPPK = False
            MsgBox(ex.ToString)
        End Try
    End Function
    Private Function InsertJenisPeserta(ByVal sKDJENISPESERTA As String, ByVal sMEMO As String) As Boolean
        Try
            Dim oJenisPeserta As New Reference.clsDaftar_L2

            InsertJenisPeserta = True

            If oJenisPeserta.IsExist(sKDJENISPESERTA) Then
                grdKDDAFTAR_L2.EditValue = sKDJENISPESERTA
            Else
                Dim ds = oJenisPeserta.GetStructureHeader
                With ds
                    .DATECREATED = Now
                    .DATEUPDATED = Now
                    .KDDAFTAR_L2 = sKDJENISPESERTA
                    .MEMO = sMEMO
                    .ISDEFAULT = IIf(oJenisPeserta.CheckDefault(0, False) = False, True, False)
                    .ISACTIVE = True
                End With

                InsertJenisPeserta = oJenisPeserta.InsertData(ds)

                fn_LoadDaftar2()
                grdKDDAFTAR_L2.EditValue = sKDJENISPESERTA
            End If

        Catch ex As Exception
            InsertJenisPeserta = False
            MsgBox(ex.ToString)
        End Try
    End Function
    Private Function InsertCOB(ByVal sKDCOB As String, ByVal sMEMO As String, ByVal sTGLTAT As DateTime, ByVal sTGLTMT As DateTime) As Boolean
        Try
            Dim oCOB As New Reference.clsCOB

            InsertCOB = True

            If oCOB.IsExist(sKDCOB) Then
                'grdKDCOB.EditValue = oCOB.GetData(sKDCOB).KDCOB
            Else
                Dim ds = oCOB.GetStructureHeader
                With ds
                    .DATECREATED = Now
                    .DATEUPDATED = Now
                    .KDCOB = sKDCOB
                    .MEMO = sMEMO
                    .ISDEFAULT = IIf(oCOB.CheckDefault(0, False) = False, True, False)
                    .ISACTIVE = True
                    .TGLTAT = sTGLTAT
                    .TGLTMT = sTGLTMT
                End With

                InsertCOB = oCOB.InsertData(ds)

                fn_LoadCOB()

            End If

        Catch ex As Exception
            InsertCOB = False
            MsgBox(ex.ToString)
        End Try
    End Function
#End Region
#Region "Katalog BPJS Kesehatan"
    Private Sub fn_LoadKartuBPJS(ByVal KARTUBPJS As String)
        Try
            If KARTUBPJS = String.Empty Then Exit Sub
            Dim uTime As Integer = 0

            If sVclaim_ConsId <> "" Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.GetDataVClaimPesertaNoKartuBPJS(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, KARTUBPJS, Now.ToString("yyyy-MM-dd"))

                Dim allData = JObject.Parse(dsSetKoneksi)
                Dim CodeResponse As String = String.Empty
                Dim messageResponse As String = String.Empty

                CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                messageResponse = allData("metaData")("message").ToString


                If CodeResponse = "200" Then
                    Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), sVclaim_ConsId & sVclaim_SecreatKey & uTime))
                    Dim oKelas As New Reference.clsKelasRawat
                    grdKDKELASRAWAT.Text = oKelas.GetDataByKode(DataDecrypt("peserta")("hakKelas")("kode").ToString()).KDKELASRAWAT

                    If txtKDCUSTOMER.Text = "" Then
                        txtKDCUSTOMER.Text = DataDecrypt("peserta")("mr")("noMR")
                    End If

                    Dim NoTelepon = DataDecrypt("peserta")("mr")("noTelepon").ToString()
                    If NoTelepon <> "" Then
                        If txtNOMORTELEPON.Text = "" Then
                            txtNOMORTELEPON.Text = NoTelepon
                        End If
                        If txtNOMORTELEPON.Text = "000000000000" Then
                            txtNOMORTELEPON.Text = NoTelepon
                        End If
                    End If

                    Dim jenisPeserta As String = DataDecrypt("peserta")("jenisPeserta")("keterangan").ToString()
                    Dim kodejenisPeserta As String = DataDecrypt("peserta")("jenisPeserta")("kode").ToString()

                    If jenisPeserta <> "" Then
                        InsertJenisPeserta(kodejenisPeserta, jenisPeserta)
                    End If

                    'Insert

                    Dim nmAsuransi As String = DataDecrypt("peserta")("cob")("nmAsuransi").ToString()
                    Dim noAsuransi As String = DataDecrypt("peserta")("cob")("noAsuransi").ToString()
                    Dim tglTAT As String = DataDecrypt("peserta")("cob")("tglTAT").ToString()
                    Dim tglTMT As String = DataDecrypt("peserta")("cob")("tglTMT").ToString()

                    Dim nmProvider As String = DataDecrypt("peserta")("provUmum")("nmProvider").ToString()
                    Dim kodeProvider As String = DataDecrypt("peserta")("provUmum")("kdProvider").ToString()

                    If grdKDCOB.Text = "-" Then
                        If nmAsuransi <> "" Then
                            InsertCOB(noAsuransi, nmAsuransi, tglTAT, tglTMT)
                        End If

                    End If

                    If grdKDPPK.Text = "-" Then
                        If nmProvider <> "" Then
                            InsertPPK(kodeProvider, nmProvider)
                        End If
                    End If

                    Dim sJudul As String = String.Empty
                    Dim sMessage As String = String.Empty

                    sMessage = "* Peserta COB " & vbCrLf _
                         & "  - Nama Auransi: " & DataDecrypt("peserta")("cob")("nmAsuransi").ToString() & vbCrLf _
                         & "  - No Auransi: " & DataDecrypt("peserta")("cob")("noAsuransi").ToString() & vbCrLf _
                         & "  - Tgl TAT : " & DataDecrypt("peserta")("cob")("tglTAT").ToString() & vbCrLf _
                         & "  - Tgl TMT: " & DataDecrypt("peserta")("cob")("tglTMT").ToString() & vbCrLf _
                         & "" & vbCrLf _
                         & "* Informasi Dinsos " & vbCrLf _
                         & "  - Dinsos: " & DataDecrypt("peserta")("informasi")("dinsos").ToString() & vbCrLf _
                         & "  - No SKTM: " & DataDecrypt("peserta")("informasi")("noSKTM").ToString() & vbCrLf _
                         & "  - Prolanis: PRB " & DataDecrypt("peserta")("informasi")("prolanisPRB").ToString() & vbCrLf _
                         & "" & vbCrLf _
                         & "Tgl Cetak Kartu: " & DataDecrypt("peserta")("tglCetakKartu").ToString() & vbCrLf _
                         & "Tgl TAT: " & DataDecrypt("peserta")("tglTAT").ToString() & vbCrLf _
                         & "Tgl TMT: " & DataDecrypt("peserta")("tglTMT").ToString()

                    sJudul = DataDecrypt("peserta")("statusPeserta")("keterangan").ToString() & " !!!"
                    Dim Message As New frmPesertaBPJS
                    Message.LoadMe(sJudul, DataDecrypt("peserta")("nama").ToString(), DataDecrypt("peserta")("jenisPeserta")("keterangan").ToString(), DataDecrypt("peserta")("noKartu").ToString(), txtKDCUSTOMER.Text.ToString, DataDecrypt("peserta")("mr")("noMR").ToString(), IIf(DataDecrypt("peserta")("sex").ToString() = "L", "Laki-Laki", "Perempuan"), DataDecrypt("peserta")("tglLahir").ToString(), DataDecrypt("peserta")("umur")("umurSekarang").ToString(), DataDecrypt("peserta")("provUmum")("kdProvider").ToString() & " - " & DataDecrypt("peserta")("provUmum")("nmProvider").ToString(), sMessage)
                    Message.ShowDialog(Me)

                Else
                    MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                End If

            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKTP(ByVal KTP As String)
        Try
            If KTP = String.Empty Then Exit Sub

            Dim uTime As Integer = 0

            If sVclaim_ConsId <> "" Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.GetDataVClaimPesertaNIK(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, KTP, Now.ToString("yyyy-MM-dd"))

                Dim allData = JObject.Parse(dsSetKoneksi)
                Dim CodeResponse As String = String.Empty
                Dim messageResponse As String = String.Empty

                CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                messageResponse = allData("metaData")("message").ToString

                If CodeResponse = "200" Then
                    Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), sVclaim_ConsId & sVclaim_SecreatKey & uTime))
                    Dim oKelas As New Reference.clsKelasRawat
                    grdKDKELASRAWAT.Text = oKelas.GetDataByKode(DataDecrypt("peserta")("hakKelas")("kode").ToString()).KDKELASRAWAT

                    Dim NoTelepon = DataDecrypt("peserta")("mr")("noTelepon").ToString()
                    If NoTelepon <> "" Then
                        If txtNOMORTELEPON.Text = "" Then
                            txtNOMORTELEPON.Text = NoTelepon
                        End If
                        If txtNOMORTELEPON.Text = "000000000000" Then
                            txtNOMORTELEPON.Text = NoTelepon
                        End If
                    End If

                    Dim jenisPeserta As String = DataDecrypt("peserta")("jenisPeserta")("keterangan").ToString()
                    Dim kodejenisPeserta As String = DataDecrypt("peserta")("jenisPeserta")("kode").ToString()

                    If jenisPeserta <> "" Then
                        InsertJenisPeserta(kodejenisPeserta, jenisPeserta)
                    End If

                    Dim sJudul As String = String.Empty
                    Dim sMessage As String = String.Empty

                    sMessage = "* Peserta COB " & vbCrLf _
                             & "  - Nama Auransi: " & DataDecrypt("peserta")("cob")("nmAsuransi").ToString() & vbCrLf _
                             & "  - No Auransi: " & DataDecrypt("peserta")("cob")("noAsuransi").ToString() & vbCrLf _
                             & "  - Tgl TAT : " & DataDecrypt("peserta")("cob")("tglTAT").ToString() & vbCrLf _
                             & "  - Tgl TMT: " & DataDecrypt("peserta")("cob")("tglTMT").ToString() & vbCrLf _
                             & "" & vbCrLf _
                             & "* Informasi Dinsos " & vbCrLf _
                             & "  - Dinsos: " & DataDecrypt("peserta")("informasi")("dinsos").ToString() & vbCrLf _
                             & "  - No SKTM: " & DataDecrypt("peserta")("informasi")("noSKTM").ToString() & vbCrLf _
                             & "  - Prolanis: PRB " & DataDecrypt("peserta")("informasi")("prolanisPRB").ToString() & vbCrLf _
                             & "" & vbCrLf _
                             & "Tgl Cetak Kartu: " & DataDecrypt("peserta")("tglCetakKartu").ToString() & vbCrLf _
                             & "Tgl TAT: " & DataDecrypt("peserta")("tglTAT").ToString() & vbCrLf _
                             & "Tgl TMT: " & DataDecrypt("peserta")("tglTMT").ToString()

                    sJudul = DataDecrypt("peserta")("statusPeserta")("keterangan").ToString() & " !!!"
                    Dim Message As New frmPesertaBPJS
                    Message.LoadMe(sJudul, DataDecrypt("peserta")("nama").ToString(), DataDecrypt("peserta")("jenisPeserta")("keterangan").ToString(), DataDecrypt("peserta")("noKartu").ToString(), txtKDCUSTOMER.Text.ToString(), DataDecrypt("peserta")("mr")("noMR").ToString(), IIf(DataDecrypt("peserta")("sex").ToString() = "L", "Laki-Laki", "Perempuan"), DataDecrypt("peserta")("tglLahir").ToString(), DataDecrypt("peserta")("umur")("umurSekarang").ToString(), DataDecrypt("peserta")("provUmum")("kdProvider").ToString() & " - " & DataDecrypt("peserta")("provUmum")("nmProvider").ToString(), sMessage)
                    Message.ShowDialog(Me)

                Else
                    MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKartuBPJSSatuRecord(ByVal sKARTUBPS As String)
        Try
            If sKARTUBPS = String.Empty Then Exit Sub

            Dim uTime As Integer = 0

            If sVclaim_ConsId <> "" Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.CariRujukanKartuSatuRecord(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, sKARTUBPS, cboASALRUJUKAN.SelectedIndex)

                Dim allData = JObject.Parse(dsSetKoneksi)
                Dim CodeResponse As String = String.Empty
                Dim messageResponse As String = String.Empty

                CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                messageResponse = allData("metaData")("message").ToString

                If CodeResponse = "200" Then
                    Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), sVclaim_ConsId & sVclaim_SecreatKey & uTime))

                    txtNOMORRUJUKAN.Text = DataDecrypt("rujukan")("noKunjungan").ToString()
                    deDATE_RUJUKAN.DateTime = DataDecrypt("rujukan")("tglKunjungan").ToString()

                    InsertDiagnosa(DataDecrypt("rujukan")("diagnosa")("kode").ToString(), DataDecrypt("rujukan")("diagnosa")("kode").ToString() & " - " & DataDecrypt("rujukan")("diagnosa")("nama").ToString())

                    Dim oKelas As New Reference.clsKelasRawat
                    grdKDKELASRAWAT.Text = oKelas.GetDataByKode(DataDecrypt("rujukan")("peserta")("hakKelas")("kode").ToString()).KDKELASRAWAT

                    'txtCATATAN.Text = DataDecrypt("rujukan")("keluhan").ToString()

                    Dim NoTelepon = DataDecrypt("rujukan")("peserta")("mr")("noTelepon").ToString()

                    If NoTelepon <> "" Then
                        If txtNOMORTELEPON.Text = "" Then
                            txtNOMORTELEPON.Text = NoTelepon
                        End If
                        If txtNOMORTELEPON.Text = "000000000000" Then
                            txtNOMORTELEPON.Text = NoTelepon
                        End If
                    End If

                    Dim oDepartment As New Reference.clsDepartment

                    Dim dsDepartment = oDepartment.GetDataByKodeVclaim(DataDecrypt("rujukan")("poliRujukan")("kode").ToString())

                    If dsDepartment IsNot Nothing Then
                        grdKDDEPARTMENT.Text = dsDepartment.KDDEPARTMENT
                        fn_LoadDoctor(dsDepartment.KDDEPARTMENT)
                        grdKDDOCTOR.ShowPopup()
                    End If

                    'Insert

                    Dim nmAsuransi As String = DataDecrypt("rujukan")("peserta")("cob")("nmAsuransi").ToString()
                    Dim noAsuransi As String = DataDecrypt("rujukan")("peserta")("cob")("noAsuransi").ToString()
                    Dim tglTAT As String = DataDecrypt("rujukan")("peserta")("cob")("tglTAT").ToString()
                    Dim tglTMT As String = DataDecrypt("rujukan")("peserta")("cob")("tglTMT").ToString()

                    'Dim nmProvider As String = DataDecrypt("rujukan")("peserta")("provUmum")("nmProvider").ToString()
                    'Dim kodeProvider As String = DataDecrypt("rujukan")("peserta")("provUmum")("kdProvider").ToString()

                    Dim nmProvider As String = DataDecrypt("rujukan")("provPerujuk")("nama").ToString()
                    Dim kodeProvider As String = DataDecrypt("rujukan")("provPerujuk")("kode").ToString()


                    If nmAsuransi <> "" Then
                        InsertCOB(noAsuransi, nmAsuransi, tglTAT, tglTMT)
                    End If

                    If nmProvider <> "" Then
                        InsertPPK(kodeProvider, nmProvider)
                    End If

                Else
                    deDATE_RUJUKAN.DateTime = Now
                    txtNOMORRUJUKAN.ResetText()
                    grdKDDIAGNOSA.ResetText()
                    'grdKDDEPARTMENT.ResetText()
                    'grdKDDOCTOR.ResetText()
                    grdKDPPK.Reset()
                    'grdKDDOCTOR.Properties.DataSource = Nothing
                    MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKartuBPJSMultiRecord(ByVal sKARTUBPS As String)
        Try
            If sKARTUBPS = String.Empty Then Exit Sub

            Dim uTime As Integer = 0

            If sVclaim_ConsId <> "" Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.CariRujukanKartuMultiRecord(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, sKARTUBPS, cboASALRUJUKAN.SelectedIndex)

                Dim allData = JObject.Parse(dsSetKoneksi)
                Dim CodeResponse As String = String.Empty
                Dim messageResponse As String = String.Empty

                CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                messageResponse = allData("metaData")("message").ToString

                If CodeResponse = "200" Then
                    Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), sVclaim_ConsId & sVclaim_SecreatKey & uTime))

                    Dim table As DataTable

                    table = New DataTable("M_RUJUKAN")

                    table.Columns.Add("noKunjungan")
                    table.Columns.Add("tglKunjungan")
                    table.Columns.Add("noKartu")
                    table.Columns.Add("nama")
                    table.Columns.Add("provPerujuk")
                    table.Columns.Add("poliRujukan")

                    For Each item In DataDecrypt("rujukan")
                        table.Rows.Add(New String() {item("noKunjungan"), item("tglKunjungan"), item("peserta")("noKartu"), item("peserta")("nama"), item("provPerujuk")("nama"), item("poliRujukan")("nama")})
                    Next

                    grdCARI.Properties.DataSource = table

                    grdCARI.Properties.ValueMember = "noKunjungan"
                    grdCARI.Properties.DisplayMember = "noKunjungan"

                    grdCARI.ShowPopup()

                Else
                    grvCARI.Columns.Clear()
                    grdCARI.Properties.DataSource = Nothing
                    grvCARI.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

                    MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadNomorRujukan(ByVal sNOMORRUJUKAN As String)
        Try
            If sNOMORRUJUKAN = String.Empty Then Exit Sub
            If chkIsOfline.Checked = True Then Exit Sub

            Dim uTime As Integer = 0

            If sVclaim_ConsId <> "" Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.CariRujukan(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, sNOMORRUJUKAN, cboASALRUJUKAN.SelectedIndex)

                Dim allData = JObject.Parse(dsSetKoneksi)
                Dim CodeResponse As String = String.Empty
                Dim messageResponse As String = String.Empty

                CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                messageResponse = allData("metaData")("message").ToString

                If CodeResponse = "200" Then
                    Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), sVclaim_ConsId & sVclaim_SecreatKey & uTime))

                    txtNOMORRUJUKAN.Text = DataDecrypt("rujukan")("noKunjungan").ToString()

                    deDATE_RUJUKAN.DateTime = DataDecrypt("rujukan")("tglKunjungan").ToString()

                    InsertDiagnosa(DataDecrypt("rujukan")("diagnosa")("kode").ToString(), DataDecrypt("rujukan")("diagnosa")("kode").ToString() & " - " & DataDecrypt("rujukan")("diagnosa")("nama").ToString())

                    Dim oKelas As New Reference.clsKelasRawat
                    grdKDKELASRAWAT.Text = oKelas.GetDataByKode(DataDecrypt("rujukan")("peserta")("hakKelas")("kode").ToString()).KDKELASRAWAT

                    If DataDecrypt("rujukan")("keluhan").ToString() <> "" Then
                        'txtCATATAN.Text = DataDecrypt("rujukan")("keluhan").ToString()
                    End If

                    Dim NoTelepon = DataDecrypt("rujukan")("peserta")("mr")("noTelepon").ToString()

                    If NoTelepon <> "" Then
                        If NoTelepon <> "00000000" Then
                            txtNOMORTELEPON.Text = NoTelepon
                        End If
                    End If

                    Dim oDepartment As New Reference.clsDepartment

                    Dim dsDepartment = oDepartment.GetDataByKodeVclaim(DataDecrypt("rujukan")("poliRujukan")("kode").ToString())

                    If dsDepartment IsNot Nothing Then
                        grdKDDEPARTMENT.Text = dsDepartment.KDDEPARTMENT
                        fn_LoadDoctor(dsDepartment.KDDEPARTMENT)
                        grdKDDOCTOR.ShowPopup()
                    End If

                    Dim nmAsuransi As String = DataDecrypt("rujukan")("peserta")("cob")("nmAsuransi").ToString()
                    Dim noAsuransi As String = DataDecrypt("rujukan")("peserta")("cob")("noAsuransi").ToString()
                    Dim tglTAT As String = DataDecrypt("rujukan")("peserta")("cob")("tglTAT").ToString()
                    Dim tglTMT As String = DataDecrypt("rujukan")("peserta")("cob")("tglTMT").ToString()

                    'Dim nmProvider As String = DataDecrypt("rujukan")("peserta")("provUmum")("nmProvider").ToString()
                    'Dim kodeProvider As String = DataDecrypt("rujukan")("peserta")("provUmum")("kdProvider").ToString()

                    Dim nmProvider As String = DataDecrypt("rujukan")("provPerujuk")("nama").ToString()
                    Dim kodeProvider As String = DataDecrypt("rujukan")("provPerujuk")("kode").ToString()

                    If nmAsuransi <> "" Then
                        InsertCOB(noAsuransi, nmAsuransi, tglTAT, tglTMT)
                    End If
                    If nmProvider <> "" Then
                        InsertPPK(kodeProvider, nmProvider)
                    End If

                    txtKARTUBPJS.Text = DataDecrypt("rujukan")("peserta")("noKartu")
                    txtKDCUSTOMER.Text = DataDecrypt("rujukan")("peserta")("mr")("noMR")
                    fn_LoadKartuBPJS(txtKARTUBPJS.Text)
                    fn_GetDataCustomer(txtKDCUSTOMER.Text.ToString.Trim.ToUpper)
                Else
                    deDATE_RUJUKAN.DateTime = Now
                    txtNOMORRUJUKAN.ResetText()
                    grdKDDIAGNOSA.ResetText()

                    'grdKDDEPARTMENT.ResetText()
                    'grdKDDOCTOR.ResetText()

                    grdKDPPK.Reset()
                    'grdKDDOCTOR.Properties.DataSource = Nothing
                    MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadSuplesi(ByVal sNoKartuPeserta As String)
        Try
            Dim uTime As Integer = 0

            If sVclaim_ConsId <> "" Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.GetDataSuplesiJasaRaharja(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, sNoKartuPeserta, deDATE.DateTime.ToString("yyyy-MM-dd"))

                Dim allData = JObject.Parse(dsSetKoneksi)
                Dim CodeResponse As String = String.Empty
                Dim messageResponse As String = String.Empty

                CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                messageResponse = allData("metaData")("message").ToString

                If CodeResponse = "200" Then
                    Dim table As DataTable

                    table = New DataTable("M_SUPLESI")

                    table.Columns.Add("noRegister")
                    table.Columns.Add("noSep")
                    table.Columns.Add("noSepAwal")
                    table.Columns.Add("noSuratJaminan")
                    table.Columns.Add("tglKejadian")
                    table.Columns.Add("tglSep")

                    For Each item In allData("response")("jaminan")
                        table.Rows.Add(New String() {item("noRegister"), item("noSep"), item("noSepAwal"), item("noSuratJaminan"), item("tglKejadian"), item("tglSep")})
                    Next

                    grdSUPLESI.Properties.DataSource = table

                    grdSUPLESI.Properties.ValueMember = "noSep"
                    grdSUPLESI.Properties.DisplayMember = "noSep"

                    grdSUPLESI.ShowPopup()

                Else
                    Dim table As DataTable
                    table = New DataTable("M_SUPLESI")

                    table.Columns.Add("noRegister")
                    table.Columns.Add("noSep")
                    table.Columns.Add("noSepAwal")
                    table.Columns.Add("noSuratJaminan")
                    table.Columns.Add("tglKejadian")
                    table.Columns.Add("tglSep")

                    grdSUPLESI.Properties.DataSource = table

                    MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdHistoryPasien_DoubleClick(sender As Object, e As EventArgs) Handles grdHistoryPasien.DoubleClick
        If grvHistoryPasien.GetFocusedRowCellValue("NomorPendaftaran") Is Nothing Then
            Exit Sub
        End If

        Dim frmPENDAFTARAN As New frmPendaftaran
        Try
            frmPENDAFTARAN.LoadMe(FORM_MODE.FORM_MODE_VIEW, grvHistoryPasien.GetFocusedRowCellValue("NomorPendaftaran"))
            frmPENDAFTARAN.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub MutasiPasienToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MutasiPasienToolStripMenuItem.Click
        If grvHistoryPasien.GetFocusedRowCellValue("NomorPendaftaran") Is Nothing Then
            Exit Sub
        End If
        If grvHistoryPasien.GetFocusedRowCellValue("ISDAFTAR") = False Then
            MsgBox("Data DgCare !!!", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim frmPendaftaran_Kunjungan As New frmPendaftaran_Kunjungan
        Try
            frmPendaftaran_Kunjungan.LoadMe(FORM_MODE.FORM_MODE_ADD, grvHistoryPasien.GetFocusedRowCellValue("NomorPendaftaran"))
            frmPendaftaran_Kunjungan.ShowDialog(Me)
            fn_LoadHistoryPasien(txtKDCUSTOMER.Text.ToString)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)

        End Try
    End Sub
    Private Sub grdPEMETAAN_EditValueChanged(sender As Object, e As EventArgs) Handles grdPEMETAAN.EditValueChanged
        If isLoad = True Then
            If grdPEMETAAN.Text = String.Empty Then Exit Sub
            Dim oKelasAplicares As New Reference.clsKelasAplicare

            Dim dsKelasAplicare = oKelasAplicares.GetDataDetail_UOM(grdPEMETAAN.EditValue)
            If dsKelasAplicare IsNot Nothing Then
                If dsKelasAplicare.TERSEDIA > 0 Then
                    grdKDDEPARTMENT.EditValue = dsKelasAplicare.KDDEPARTMENT

                    fn_LoadDoctor(grdKDDEPARTMENT.EditValue)

                    Dim oPOLI As New Reference.clsDepartment
                    Dim dsPoli = oPOLI.GetData(grdKDDEPARTMENT.EditValue)

                    If dsPoli IsNot Nothing Then
                        If dsPoli.VCLAIM_KODEPOLI = "IGD" Then
                            cboASALRUJUKAN.SelectedIndex = 1
                            Dim oFaskes As New Reference.clsPPK

                            Dim dsFaskes = oFaskes.GetDataKodeFaskes(sPPKPELAYANAN)
                            If dsFaskes IsNot Nothing Then
                                grdKDPPK.Text = dsFaskes.KDPPK
                            End If
                        End If

                    End If
                Else
                    MsgBox(Statement.ErrorStatement & " Ruangan Penuh", MsgBoxStyle.Information, Me.Text)
                    grdPEMETAAN.ResetText()
                End If
            End If
        End If

    End Sub
    Private Sub txtCARISUPLESI_KeyPress_1(sender As Object, e As KeyPressEventArgs) Handles txtCARISUPLESI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If txtCARISUPLESI.Text <> "" Then
                fn_DataIndukKecelakaan(txtCARISUPLESI.Text.Trim.ToUpper)
            End If
        End If
    End Sub
    Private Sub txtPENJAMIN_SUPLESI_NOSEPSUPLESI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPENJAMIN_SUPLESI_NOSEPSUPLESI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If txtPENJAMIN_SUPLESI_NOSEPSUPLESI.Text <> "" Then
                fn_SuplesiNokartuV1(txtCARISUPLESI.Text.Trim.ToUpper)
            End If
        End If
    End Sub
    Private Sub grdSUPLESI_KeyPress_1(sender As Object, e As KeyPressEventArgs) Handles grdSUPLESI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtPENJAMIN_SUPLESI_NOSEPSUPLESI.Text = grdSUPLESI.EditValue
            txtSUPLESI_PROPINSI.Text = grvSuplesi.GetFocusedRowCellValue("kdProp")
            txtSUPLESI_KABUPATEN.Text = grvSuplesi.GetFocusedRowCellValue("kdKab")
            txtSUPLESI_KECAMATAN.Text = grvSuplesi.GetFocusedRowCellValue("kdKec")
        End If
    End Sub
    Private Sub fn_DataIndukKecelakaan(ByVal sNoKartuPeserta As String)
        Try
            Dim uTime As Integer = 0

            If sVclaim_ConsId <> "" Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.GetDataDataIndukKecelakaan(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, sNoKartuPeserta)

                If dsSetKoneksi <> "" Then
                    Try
                        Dim allData = JObject.Parse(dsSetKoneksi)

                        Dim CodeResponse As String = String.Empty
                        Dim messageResponse As String = String.Empty

                        CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                        messageResponse = allData("metaData")("message").ToString

                        If CodeResponse = "200" Then
                            Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), sVclaim_ConsId & sVclaim_SecreatKey & uTime))

                            Dim table As DataTable

                            table = New DataTable("M_SUPLESI")

                            table.Columns.Add("noSep")
                            table.Columns.Add("tglKejadian")
                            table.Columns.Add("ppkPelSEP")
                            table.Columns.Add("kdProp")
                            table.Columns.Add("kdKab")
                            table.Columns.Add("kdKec")
                            table.Columns.Add("ketKejadian")
                            table.Columns.Add("noSEPSuplesi")

                            Dim oPropinsi As New Reference.clsPropinsi
                            Dim oKabupaten As New Reference.clsKabupaten
                            Dim oKecamatan As New Reference.clsKecamatan
                            Dim Propinsi As String = String.Empty
                            Dim Kabupaten As String = String.Empty
                            Dim Kecamatan As String = String.Empty

                            For Each item In DataDecrypt("list")
                                Dim dsPropinsi = oPropinsi.GetDatakodebpjs(item("kdProp"))
                                If dsPropinsi IsNot Nothing Then
                                    Propinsi = dsPropinsi.MEMO
                                Else
                                    Propinsi = item("kdProp")
                                End If
                                Dim dsKabupaten = oKabupaten.GetDatakodebpjs(item("kdKab"))
                                If dsKabupaten IsNot Nothing Then
                                    Kabupaten = dsKabupaten.MEMO
                                Else
                                    Kabupaten = item("kdKab")
                                End If
                                Dim dsKecamatan = oKecamatan.GetDatakodebpjs(item("kdKec"))
                                If dsKecamatan IsNot Nothing Then
                                    Kecamatan = dsKecamatan.MEMO
                                Else
                                    Kecamatan = item("kdKec")
                                End If

                                table.Rows.Add(New String() {item("noSep"), item("tglKejadian"), item("ppkPelSEP"), item("kdProp"), Propinsi, item("kdKab"), Kabupaten, item("kdKec"), Kecamatan, item("ketKejadian"), item("noSEPSuplesi")})
                            Next

                            grdSUPLESI.Properties.DataSource = table

                            grdSUPLESI.Properties.ValueMember = "noSEPSuplesi"
                            grdSUPLESI.Properties.DisplayMember = "noSEPSuplesi"

                            grdSUPLESI.ShowPopup()
                        Else
                            Dim table As DataTable
                            table = New DataTable("M_SUPLESI")

                            table.Columns.Add("noRegister")
                            table.Columns.Add("noSep")
                            table.Columns.Add("noSepAwal")
                            table.Columns.Add("noSuratJaminan")
                            table.Columns.Add("tglKejadian")
                            table.Columns.Add("tglSep")

                            grdSUPLESI.Properties.DataSource = table

                            MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                        End If
                    Catch oErr As Exception
                        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message & vbCrLf & vbCrLf & "Result : " & vbCrLf & dsSetKoneksi, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                End If
            Else
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_SuplesiNokartuV1(ByVal sNoKartuPeserta As String)
        Try
            Dim uTime As Integer = 0

            If sVclaim_ConsId <> "" Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.GetDataSuplesiJasaRaharja(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, sNoKartuPeserta, deDATE.DateTime.ToString("yyyy-MM-dd"))

                If dsSetKoneksi <> "" Then
                    Try
                        Dim allData = JObject.Parse(dsSetKoneksi)

                        Dim CodeResponse As String = String.Empty
                        Dim messageResponse As String = String.Empty

                        CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                        messageResponse = allData("metaData")("message").ToString

                        If CodeResponse = "200" Then
                            Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), sVclaim_ConsId & sVclaim_SecreatKey & uTime))

                            Dim table As DataTable

                            table = New DataTable("M_SUPLESI")

                            table.Columns.Add("noRegister")
                            table.Columns.Add("noSep")
                            table.Columns.Add("noSepAwal")
                            table.Columns.Add("noSuratJaminan")
                            table.Columns.Add("tglKejadian")
                            table.Columns.Add("tglSep")

                            For Each item In DataDecrypt("response")("jaminan")
                                table.Rows.Add(New String() {item("noRegister"), item("noSep"), item("noSepAwal"), item("noSuratJaminan"), item("tglKejadian"), item("tglSep")})
                            Next

                            grdSUPLESI.Properties.DataSource = table

                            grdSUPLESI.Properties.ValueMember = "noSep"
                            grdSUPLESI.Properties.DisplayMember = "noSep"

                            grdSUPLESI.ShowPopup()
                        Else
                            Dim table As DataTable
                            table = New DataTable("M_SUPLESI")

                            table.Columns.Add("noRegister")
                            table.Columns.Add("noSep")
                            table.Columns.Add("noSepAwal")
                            table.Columns.Add("noSuratJaminan")
                            table.Columns.Add("tglKejadian")
                            table.Columns.Add("tglSep")

                            grdSUPLESI.Properties.DataSource = table

                            MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                        End If
                    Catch oErr As Exception
                        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message & vbCrLf & vbCrLf & "Result : " & vbCrLf & dsSetKoneksi, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                End If
            Else
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnDataSEPInternal_Click(sender As Object, e As EventArgs) Handles btnDataSEPInternal.Click
        Try
            If txtSEPINTERNAL.Text = "" Then Exit Sub

            Dim uTime As Integer = 0

            If sVclaim_ConsId <> "" Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.DataSEPInternal(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, txtSEPINTERNAL.Text)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), sVclaim_ConsId & sVclaim_SecreatKey & uTime))

                        Dim table As DataTable

                        table = New DataTable("M_INTERNAL")

                        table.Columns.Add("tujuanrujuk")
                        table.Columns.Add("nmtujuanrujuk")
                        table.Columns.Add("nmpoliasal")
                        table.Columns.Add("tglrujukinternal")
                        table.Columns.Add("nosep")
                        table.Columns.Add("kdpolituj")
                        table.Columns.Add("nmdokter")
                        table.Columns.Add("nosurat")

                        For Each item In DataDecrypt("list")
                            table.Rows.Add(New String() {item("tujuanrujuk"), item("nmtujuanrujuk"), item("nmpoliasal"), item("tglrujukinternal"), item("nosep"), item("kdpolituj"), item("nmdokter"), item("nosurat")})
                        Next

                        grdNOMORSEURATSEP.Properties.DataSource = table
                        grdNOMORSEURATSEP.Properties.ValueMember = "nosurat"
                        grdNOMORSEURATSEP.Properties.DisplayMember = "nosurat"

                        grdNOMORSEURATSEP.ShowPopup()
                    Else
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    MsgBox("Kosong Koneksi", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                MsgBox("Cari Finger Print Data Gagal/Kosong", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnHapusSEPInternal_Click(sender As Object, e As EventArgs) Handles btnHapusSEPInternal.Click
        Dim Pesan As String = String.Empty
        Try
            If grdNOMORSEURATSEP.Text = "" Then Exit Sub

            Dim uTime As Integer = 0

            Dim jsonRequest As String = String.Empty

            jsonRequest = "{" & """request"": {" & """t_sep"": {" & """noSep"": """ & txtSEPINTERNAL.Text & """," & """noSurat"": """ & grdNOMORSEURATSEP.EditValue & """," & """tglRujukanInternal"": """ & txtTGLRUJUKANINTERNAL.Text & """," & """kdPoliTuj"": """ & txtKODEPOLIINTERNAL.Text & """," & """user"": """ & sUserID & """" & "}}}"

            If sVclaim_ConsId <> "" Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.HapusSEPInternal(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, jsonRequest)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        Pesan = allData("response")
                        Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), sVclaim_ConsId & sVclaim_SecreatKey & uTime))
                        MsgBox(CodeResponse & DataDecrypt.ToString(), MsgBoxStyle.Exclamation, Me.Text)
                    Else
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    MsgBox("Kosong Koneksi", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                MsgBox("Cari Finger Print Data Gagal/Kosong", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message & vbCrLf & Pesan, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdNOMORSEURATSEP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdNOMORSEURATSEP.KeyPress
        If txtKARTUBPJS.Text.Count <> 13 Then
            txtTGLRUJUKANINTERNAL.Text = grvNOMORSEURATSEP.GetFocusedRowCellValue("tglrujukinternal")
            txtKODEPOLIINTERNAL.Text = grvNOMORSEURATSEP.GetFocusedRowCellValue("kdpolituj")
        End If
    End Sub
    Private Sub cboTUJUANKUNJUNGAN_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTUJUANKUNJUNGAN.SelectedIndexChanged
        If cboTUJUANKUNJUNGAN.SelectedIndex = 0 Then
            cboFLAGPROCEDURE.ResetText()
            cboKODEKUNJUNGAN.ResetText()
            lFLAG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lKODEKUNJUNGAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            cboFLAGPROCEDURE.ResetText()
            cboKODEKUNJUNGAN.ResetText()
            lFLAG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lKODEKUNJUNGAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub
    Private Sub cboTUJUANKUNJUNGAN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboTUJUANKUNJUNGAN.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cboTUJUANKUNJUNGAN.ResetText()
        End If
    End Sub
    Private Sub cboASESMENPELAYANAN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboASESMENPELAYANAN.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cboASESMENPELAYANAN.ResetText()
        End If
    End Sub
    Private Sub cboFLAGPROCEDURE_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboFLAGPROCEDURE.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cboFLAGPROCEDURE.ResetText()
        End If
    End Sub
    Private Sub cboKODEKUNJUNGAN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboKODEKUNJUNGAN.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cboKODEKUNJUNGAN.ResetText()
        End If
    End Sub
    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        Dim frmSKD As New frmSKD
        Try
            If grdKDDOCTOR.Text <> "" Then
                frmSKD.fn_LoadNoPendaftaranPolidanDokter(rbCATEGORY.SelectedIndex, grdKDDEPARTMENT.EditValue, grdKDDOCTOR.EditValue, txtKDCUSTOMER.Text, "", "")
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
                frmSKD.ShowDialog(Me)
                txtNOMORSKDP.Text = sCode
                'If sCode <> "" Then
                '    fn_LoadKDSKD(txtNOMORSKDP.Text.ToString.Trim.ToUpper)
                'End If
            Else
                MsgBox("Dokter Belum dipilih", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmSKD Is Nothing Then frmSKD.Dispose()
            frmSKD = Nothing
        End Try
    End Sub
    Private Sub btnIcare_Click(sender As Object, e As EventArgs) Handles btnIcare.Click
        If grdKDDOCTOR.Text = "" Then
            MsgBox("Dokter Belum dipilih", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If txtKARTUBPJS.Text = "" Then
            MsgBox("No Kartu BPJS Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

        SplashScreenManager.Default.SetWaitFormCaption("Processing data.....")

        Dim oDoctor As New Reference.clsDoctor
        Dim dsDoctor = oDoctor.GetData(grdKDDOCTOR.EditValue)

        If dsDoctor IsNot Nothing Then
            Try
                Dim oDokter As New Reference.clsDoctor
                Dim oKoneksi As New Brigging.clsSetKoneksi
                Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim VCLAIM_KDDPJP As Integer = 0

                If dsDoctor.VCLAIM_KDDPJP = "" Then
                    MsgBox("Icare: " & vbCrLf & "Kode Dokter kosong", MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                Else
                    VCLAIM_KDDPJP = dsDoctor.VCLAIM_KDDPJP
                End If

                If sURLICARE <> "" Then
                    Dim allData = JObject.Parse(oKoneksi.GetDataIcare(txtKARTUBPJS.Text, VCLAIM_KDDPJP, uTime, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, sURLICARE))

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        Dim DataDecrypt = JObject.Parse(oKoneksi.Decrypt(allData("response"), sVclaim_ConsId & sVclaim_SecreatKey & uTime))
                        Dim variabel As String = "CMD /c Start chrome /profile-directory=""Default"" """ & DataDecrypt.Item("url").ToString() & """"
                        Shell(variabel, vbNormalFocus)
                    Else
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    SplashScreenManager.CloseForm(False)
                    MsgBox("Silahkan Setting Koneksi Brigging", MsgBoxStyle.Exclamation, Me.Text)
                End If

            Catch oErr As Exception
                SplashScreenManager.CloseForm(False)
                MsgBox("Icare: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If

        SplashScreenManager.CloseForm(False)
    End Sub
    Private Sub btnKodeBookingThalasemi_Click(sender As Object, e As EventArgs) Handles btnKodeBookingThalasemi.Click
        If rbCATEGORY.SelectedIndex = 0 Then
            If grdKDDEPARTMENT.Text <> "" Then
                If grdKDDOCTOR.Text <> "" Then
                    Create_KodeBooking(IIf(grdKDDAFTAR_L1.Text = "BPJS", "B", "A"), grdKDDEPARTMENT.EditValue, grdKDDOCTOR.EditValue)
                Else
                    MsgBox("Dokter Kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                MsgBox("Klinik Kosong", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Else
            MsgBox("hanya Untuk Klinik", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub btnKodeBookingHD_Click(sender As Object, e As EventArgs) Handles btnKodeBookingHD.Click
        ' Create_KodeBooking("HDD", "218")
    End Sub
    Private Sub Create_KodeBooking(ByVal Penjamin As String, ByVal KDDPERTMENT As String, ByVal kddoctor As String)
        Try
            If grdKDPENJAMIN.Text = "" Then
                MsgBox("Penjamin Kosong", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            If txtKDBOOKING.Text <> "" Then
                Exit Sub
            End If

            Dim oSet_Antrian_Simpan As New SettingAntrian.clsSetAntrian

            If txtKARTUBPJS.Text <> "" Then
                Dim dsCekKartu = oSet_Antrian_Simpan.GetDataByKartuBPJSTanggal(txtKARTUBPJS.Text, deDATE.DateTime)
                If dsCekKartu IsNot Nothing Then
                    txtKDBOOKING.Text = dsCekKartu.KODEBOOKING
                    Exit Sub
                End If
            End If

            Dim NomorAntrian As String = String.Empty

            Dim oDepartment As New Reference.clsDepartment
            Dim dsdepartment = oDepartment.GetData(KDDPERTMENT)
            If dsdepartment IsNot Nothing Then
                ' ***** HEADER *****
                Dim ds = oSet_Antrian_Simpan.GetStructureHeader
                With ds
                    .DATECREATED = Now
                    .DATEUPDATED = Now
                    .KDPENJAMIN = grdKDPENJAMIN.EditValue
                    .KODEBOOKING = ""
                    .JENISPASIEN_RS = Penjamin
                    .JENISPASIEN = "JKN"
                    .NOMORKARTU = ""
                    .NOHP = ""
                    .NIK = ""
                    '.KODEPOLI = dsdepartment.VCLAIM_KODEPOLI
                    '.NAMAPOLI = dsdepartment.NAME_DISPLAY
                    .KODEPOLI = dsdepartment.VCLAIM_KODEPOLI
                    .NAMAPOLI = dsdepartment.NAME_DISPLAY
                    .PASIENBARU = 0
                    .NORM = ""
                    .TANGGALPERIKSA = Now
                    .TANGGALPERIKSA_TEXT = Now.ToString("ddMMyyyy")
                    .KODEDOKTER = kddoctor
                    .NAMADOKTER = ""
                    .JAMPRAKTEK = ""
                    .JENISKUNJUNGAN = 10
                    .NOMORREFERENSI = ""

                    Dim oDoctor As New Reference.clsDoctor
                    Dim dsDoctor = oDoctor.GetData(grdKDDOCTOR.EditValue)
                    Dim sMODULDOKTER As String = String.Empty

                    If dsDoctor IsNot Nothing Then
                        If dsDoctor.MEMO <> "" Then
                            sMODULDOKTER = dsDoctor.MEMO
                        End If
                    End If

                    If sMODULDOKTER = "" Then
                        sMODULDOKTER = .KODEPOLI
                    End If

                    If sMODULDOKTER = "" Then
                        sMODULDOKTER = "XXX"
                    End If

                    Dim oSetCounter As New SettingAntrian.clsSet_CounterAntrian
                    Dim sLASTNUMBER As Integer = 0

                    Try
                        sLASTNUMBER = oSetCounter.GetLastNumberDay(sMODULDOKTER, deDATE.DateTime)

                        If sLASTNUMBER = 0 Then
                            Try
                                oSetCounter.InsertData(sMODULDOKTER, deDATE.DateTime)
                                sLASTNUMBER = oSetCounter.GetLastNumberDay(sMODULDOKTER, deDATE.DateTime)
                            Catch ex As Exception
                                sLASTNUMBER = 0
                            End Try
                        End If

                    Catch ex As Exception
                        Throw ex
                    End Try

                    Try
                        oSetCounter.UpdateData(sMODULDOKTER, sLASTNUMBER + 1, deDATE.DateTime.ToString("dd"), deDATE.DateTime.ToString("MM"), deDATE.DateTime.ToString("yyyy"))
                    Catch ex As Exception
                        Throw ex
                    End Try

                    .NOMORANTREAN = sMODULDOKTER & (sLASTNUMBER + 1).ToString.PadLeft(3, "0")

                    NomorAntrian = .NOMORANTREAN

                    .ANGKAANTREAN = sLASTNUMBER + 1
                    '.ANGKAANTREAN = 0
                    .ESTIMASIDILAYANI = Now.ToString("yyyy-MM-dd") & "00:00"
                    .SISAKUOTAJKN = 0
                    .KUOTAJKN = 0
                    .SISAKUOTANONJKN = 0
                    .SISAKUOTANONJKN = 0
                    .ISPANGGIL = 2
                    .ISONLINE = False
                    .KETERANGAN = "OFLINE"
                    .KDSKD = ""
                End With

                Dim sKDBOOKINGANTREAN As String = ""

                Try
                    sKDBOOKINGANTREAN = oSet_Antrian_Simpan.InsertData(ds)
                    txtKDBOOKING.Text = sKDBOOKINGANTREAN
                    txtANTRIANPOLI.Text = NomorAntrian

                    'If sKDBOOKINGANTREAN = "" Then
                    '    Create_KodeBooking = False
                    'Else
                    '    Create_KodeBooking = True
                    'End If

                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try

            Else
                MsgBox("poli Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            'Create_KodeBooking = False
        End Try
    End Sub
#End Region
End Class