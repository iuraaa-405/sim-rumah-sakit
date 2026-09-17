Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient
Imports Newtonsoft.Json.Linq

Public Class frmPendaftaran
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oPendaftaran As New Admission.clsPendaftaran
    Private REQUEST As String = ""
    Private RESPONSE As String = ""
    Private sINFORMASIPRB As String = ""
    Private sLoadAwal As Boolean = False
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId
        txtKARTUBPJS.Properties.MaxLength = 13
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
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
            lTAB3_PROPINSI.Text = Pendaftaran.JAMINAN_PENJAMIN_SUPLESI_LOKASILAKA_KDPROPINSI
            lTAB3_KABUPATEN.Text = Pendaftaran.JAMINAN_PENJAMIN_SUPLESI_LOKASILAKA_KABUPATEN
            lTAB3_KECAMATAN.Text = Pendaftaran.JAMINAN_PENJAMIN_SUPLESI_LOKASILAKA_KECAMATAN

            'grvKDPAYMENTTYPE.Columns("MEMO").Caption = PaymentType.MEMO
            'grvKDCOA.Columns("NMCOA").Caption = COA.NMCOA
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
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
        grdSUPLESI_PROPINSI.Properties.ReadOnly = Status
        grdSUPLESI_KABUPATEN.Properties.ReadOnly = Status
        grdSUPLESI_KECAMATAN.Properties.ReadOnly = Status

        txtNAMAPENANGGUNGJAWAB.Properties.ReadOnly = Status
        cboHUBUNGANPENANGGUNGJAWAB.Properties.ReadOnly = Status
        txtALAMATPENANGGUNGJAWAB.Properties.ReadOnly = Status
        txtNOMORTELEPONPENANGGUNGJAWAB.Properties.ReadOnly = Status

    End Sub
    Private Sub fn_EmptyMe()
        txtKDPENDAFTARAN.Text = "<--- AUTO --->"
        txtNOMORSEP.Text = "<--- AUTO --->"
        txtKDPENDAFTARAN_AWAL.ResetText()
        deDATE.DateTime = Now
        txtKDCUSTOMER.ResetText()
        txtKARTUBPJS.ResetText()
        txtKTP.ResetText()
        If sLoadAwal = False Then
            grdKDDAFTAR_L1.Text = oPendaftaran.Daftar_L1_Default
        End If
        grdKDDAFTAR_L2.Text = oPendaftaran.Daftar_L2_Default
        grdKDDAFTAR_L3.Text = oPendaftaran.Daftar_L3_Default
        grdKDDAFTAR_L4.Text = oPendaftaran.Daftar_L4_Default
        grdKDDAFTAR_L5.Text = oPendaftaran.Daftar_L5_Default
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
        txtNOMORTELEPON.Text = "000000"
        txtCATATAN.Text = "-"
        grdKDCOB.Text = oPendaftaran.Daftar_COB_Default
        grdKDPPK.Text = oPendaftaran.Daftar_PPK_Default
        grdKDKELASRAWAT.Text = oPendaftaran.Daftar_KELASRAWAT_Default
        REQUEST = ""
        RESPONSE = ""
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
        grdSUPLESI_PROPINSI.ResetText()
        grdSUPLESI_KABUPATEN.ResetText()
        grdSUPLESI_KECAMATAN.ResetText()
        sINFORMASIPRB = ""
        txtNAMAPASIEN.ResetText()
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
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oPendaftaran.GetData(sNoId)
            Dim dsKunjungan = oPendaftaran.GetDataKunjunganByPendaftaran(sNoId)
            Dim dsPenanggungJawab = oPendaftaran.GetDataPenanggungJawabByPendaftaran(sNoId)

            With ds

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
                grdSUPLESI_PROPINSI.Text = .JAMINAN_PENJAMIN_SUPLESI_LOKASILAKA_KDPROPONSI
                grdSUPLESI_KABUPATEN.Text = .JAMINAN_PENJAMIN_SUPLESI_LOKASILAKA_KDKABUPATEN
                grdSUPLESI_KECAMATAN.Text = .JAMINAN_PENJAMIN_SUPLESI_LOKASILAKA_KDKECAMATAN

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
            End With

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

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
                txtKDPENDAFTARAN_AWAL.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDPENDAFTARAN_AWAL.ErrorText = Statement.ErrorRequired

                txtKDPENDAFTARAN_AWAL.Focus()
                fn_Validate = False
                Exit Function

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
                    If grdSUPLESI_PROPINSI.Text = String.Empty Then
                        grdSUPLESI_PROPINSI.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                        grdSUPLESI_PROPINSI.ErrorText = Statement.ErrorRequired

                        grdSUPLESI_PROPINSI.Focus()
                        fn_Validate = False
                        Exit Function
                    End If
                    If grdSUPLESI_KABUPATEN.Text = String.Empty Then
                        grdSUPLESI_KABUPATEN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                        grdSUPLESI_KABUPATEN.ErrorText = Statement.ErrorRequired

                        grdSUPLESI_KABUPATEN.Focus()
                        fn_Validate = False
                        Exit Function
                    End If
                    If grdSUPLESI_KECAMATAN.Text = String.Empty Then
                        grdSUPLESI_KECAMATAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                        grdSUPLESI_KECAMATAN.ErrorText = Statement.ErrorRequired

                        grdSUPLESI_KECAMATAN.Focus()
                        fn_Validate = False
                        Exit Function
                    End If
                End If
            End If

            'Dim dsSIP

            'Daftar Poli sama
            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                If chkIsOfline.Checked = False Then
                    Dim oPOLI As New Reference.clsDepartment
                    If oPOLI.GetData(grdKDDEPARTMENT.EditValue).VCLAIM_KODEPOLI <> "IGD" Then
                        Dim dsKunjunganSama = oPendaftaran.GetDataByRMUnitDate(txtKDCUSTOMER.Text, grdKDDEPARTMENT.EditValue, deDATE.DateTime)
                        If dsKunjunganSama IsNot Nothing Then
                            MsgBox("Pasien hari ini sudah berkunjung ke Poli yg sama dengan No Pendaftaran " & dsKunjunganSama.KDPENDAFTARAN, MsgBoxStyle.Exclamation, Me.Text)
                            fn_Validate = False
                            Exit Function
                        End If
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
            If rbCATEGORY.SelectedIndex = 1 Then
                If txtNAMAPENANGGUNGJAWAB.Text = String.Empty Then
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

            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_SaveAutoSKD(ByVal KDPENDAFATRAN As String) As Boolean
        Try
            Dim oSKD As New Admission.clsSKD
            ' ***** HEADER *****
            Dim ds = oSKD.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oSKD.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDSKD = sNoId
                .KDPENDAFTARAN = KDPENDAFATRAN
                .DATE = deDATE.DateTime
                .ISCATEGORY = rbCATEGORY.SelectedIndex
                .KDDEPARTMENT = grdKDDEPARTMENT.EditValue
                .KDDOCTOR = grdKDDOCTOR.EditValue
                .NOMORRUJUKAN = txtNOMORRUJUKAN.Text.Trim.ToUpper
                .DESCRIPTION = ""
                Try
                    .ISCHEKED = oSKD.GetData(sNoId).ISCHEKED
                Catch oErr As Exception
                    .ISCHEKED = False
                End Try
                .KDUSER = sUserID
                .DATEKONTROL = deDATE.DateTime
                .ALASAN = ""
                .TINDAKLANJUT = "KONTROL"

            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_SaveAutoSKD = oSKD.InsertData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_SaveAutoSKD = oSKD.UpdateData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveAutoSKD = False
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oPendaftaran.GetStructureHeader
            With ds
                .KDPENDAFTARAN = sNoId
                .KDPENDAFTARAN_AWAL = IIf(rbCATEGORY.SelectedIndex = 0, "", txtKDPENDAFTARAN_AWAL.Text.ToString.Trim.ToUpper)
                .NOMORSEP = IIf(txtNOMORSEP.Text = "<--- AUTO --->", "", txtNOMORSEP.Text.ToString.Trim.ToUpper)
                Try
                    .DATECREATED = oPendaftaran.GetData(sNoId).DATECREATED
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
                .KDDAFTAR_L6 = "DAFTAR_L6_0000000001"
                .CATEGORY = rbCATEGORY.SelectedIndex
                Try
                    .STATUSDAFTAR = oPendaftaran.GetData(sNoId).STATUSDAFTAR
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
                .KDKELASRAWAT = IIf(rbCATEGORY.SelectedIndex = 0, "KELASRAWAT_0000000004", grdKDKELASRAWAT.EditValue)
                .REQUEST = REQUEST
                .RESPON = RESPONSE
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
                .JAMINAN_PENJAMIN_SUPLESI_LOKASILAKA_KDPROPONSI = grdSUPLESI_PROPINSI.EditValue
                .JAMINAN_PENJAMIN_SUPLESI_LOKASILAKA_KDKABUPATEN = grdSUPLESI_KABUPATEN.EditValue
                .JAMINAN_PENJAMIN_SUPLESI_LOKASILAKA_KDKECAMATAN = grdSUPLESI_KECAMATAN.EditValue
                Try
                    .INFORMASIPRB = oPendaftaran.GetData(sNoId).INFORMASIPRB
                Catch ex As Exception
                    .INFORMASIPRB = sINFORMASIPRB
                End Try
                .CETAK = 0
            End With

            '***** Kunjungan *****
            Dim dsKunjungan = oPendaftaran.GetStructureHeader_Kunjungan
            With dsKunjungan
                Try
                    .DATECREATED = oPendaftaran.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime

                Try
                    .KDKUNJUNGAN = oPendaftaran.GetDataKunjunganByPendaftaran(sNoId).KDKUNJUNGAN
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
            End With

            '***** Penanggung Jawab *****
            Dim dsPenanggunjawab = oPendaftaran.GetStructureHeader_PenanggungJawab
            With dsPenanggunjawab
                Try
                    .DATECREATED = oPendaftaran.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDPENDAFTARAN = ds.KDPENDAFTARAN
                .NAMA = txtNAMAPENANGGUNGJAWAB.Text.ToString.Trim.ToUpper
                .HUBUNGAN = cboHUBUNGANPENANGGUNGJAWAB.Text
                .ALAMAT = txtALAMATPENANGGUNGJAWAB.Text.ToString.ToString.ToUpper
                .NOMORTELEPON = txtNOMORTELEPONPENANGGUNGJAWAB.Text.ToString.Trim.ToUpper
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    txtKDPENDAFTARAN.Text = oPendaftaran.InsertData(ds, dsKunjungan, IIf(rbCATEGORY.SelectedIndex = 1, dsPenanggunjawab, Nothing))
                    If txtKDPENDAFTARAN.Text = "" Then
                        fn_Save = False
                    Else
                        If rbCATEGORY.SelectedIndex = 0 Then
                            Dim oPOLI As New Reference.clsDepartment
                            If oPOLI.GetData(grdKDDEPARTMENT.EditValue).VCLAIM_KODEPOLI <> "IGD" Then
                                If chkIsOfline.Checked = False Then
                                    fn_SaveAutoSKD(txtKDPENDAFTARAN.Text)
                                End If
                            End If
                        End If

                        fn_Save = True

                        txtNOMORSEP.Text = fn_CreateSEP()

                        If txtNOMORSEP.Text <> "" Then
                            oPendaftaran.UpdateSEP(txtKDPENDAFTARAN.Text.ToString.Trim.ToUpper, txtNOMORSEP.Text.ToString.Trim.ToUpper)
                        End If

                        CetakRegister(txtKDPENDAFTARAN.Text)

                    End If
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oPendaftaran.UpdateData(ds, dsKunjungan, IIf(rbCATEGORY.SelectedIndex = 1, dsPenanggunjawab, Nothing))

                    fn_UpdateSEP()

                    CetakRegister(txtKDPENDAFTARAN.Text)

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
    Private Sub frmPendaftaran_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
                If btnAddCustomer.Enabled = True Then
                    btnAddCustomer_Click()
                End If
        End Select
    End Sub
    Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            Dim oSKDS As New Admission.clsSKD
            Dim dsSKDS = oSKDS.GetData(txtNOMORSKDP.Text.ToString.Trim.ToUpper)
            If dsSKDS IsNot Nothing Then
                oSKDS.UpdateDataFix(dsSKDS.KDSKD, True)
            End If

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
            Dim oSKDS As New Admission.clsSKD
            Dim dsSKDS = oSKDS.GetData(txtNOMORSKDP.Text.ToString.Trim.ToUpper)
            If dsSKDS IsNot Nothing Then
                oSKDS.UpdateDataFix(dsSKDS.KDSKD, True)
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
                frmCustomer.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsCustomer.KDCUSTOMER)
                frmCustomer.ShowDialog(Me)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Else
            Dim frmCustomer As New frmCustomer
            Try
                frmCustomer.LoadMe(FORM_MODE.FORM_MODE_ADD)
                frmCustomer.ShowDialog(Me)
                If sCodeCustomer <> "<--- AUTO --->" Then
                    txtKDCUSTOMER.Text = sCodeCustomer
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub CetakRegister(ByVal KDPENDAFTARAN As String)
        If grdKDDAFTAR_L1.Text = "BPJS" Then
            If txtNOMORSEP.Text <> "" Then
                Dim rpt As New xtraSEP

                Dim ds = oPendaftaran.GetData(KDPENDAFTARAN)
                rpt.bindingSource.DataSource = ds
                Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                printTool.PrintDialog()

            Else
                Dim rpt As New xtraUmum

                Dim ds = oPendaftaran.GetData(KDPENDAFTARAN)
                rpt.bindingSource.DataSource = ds
                Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                printTool.PrintDialog()
            End If
        Else
            Dim rpt As New xtraUmum

            Dim ds = oPendaftaran.GetData(KDPENDAFTARAN)
            rpt.bindingSource.DataSource = ds
            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
            printTool.PrintDialog()
        End If

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
                               Where IIf(oDepartment.GetData(KDDEPARTMENT).VCLAIM_KODEPOLI = "IGD", x.KDDEPARTMENT = grdKDDEPARTMENT.EditValue And x.M_DEPARTMENT.ISRUANGRAWAT = False And x.M_DOCTOR.ISACTIVE = True, x.KDDEPARTMENT = grdKDDEPARTMENT.EditValue And x.M_DOCTOR.VCLAIM_KDDPJP <> "" And x.M_DEPARTMENT.ISRUANGRAWAT = False And x.M_DOCTOR.ISACTIVE = True)
                               Select x.KDDOCTOR, x.M_DOCTOR.NAME_DISPLAY

                grdKDDOCTOR.Properties.DataSource = dsDoctor.ToList()
                grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
                grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Else
            Try
                grdKDDOCTOR.Properties.DataSource = oDoctor.GetData.Where(Function(x) x.ISACTIVE = True And x.VCLAIM_KDDPJP <> "").ToList()
                grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
                grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If

    End Sub
    Private Sub fn_LoadDiganosa()
        Dim oDiagnosa As New Reference.clsDiagnosa
        Try
            grdKDDIAGNOSA.Properties.DataSource = oDiagnosa.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
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
    Private Sub fn_GetDataCustomer(ByVal Parameter As String)
        Dim oCUSTOMER As New Reference.clsCustomer
        Dim dsCustomer = oCUSTOMER.GetData(Parameter)

        If dsCustomer IsNot Nothing Then
            lblDINAS.Text = dsCustomer.M_KESATUAN.GOL.ToString.Trim.ToUpper
            txtKDCUSTOMER.Text = dsCustomer.KDCUSTOMER.ToString.Trim.ToUpper
            txtNAMAPASIEN.Text = dsCustomer.NAME_DISPLAY.ToString.Trim.ToUpper
            txtKARTUBPJS.Text = dsCustomer.KARTUBPJS.ToString.Trim.ToUpper
            txtKTP.Text = dsCustomer.KTP.ToString.Trim.ToUpper
            txtALAMAT.Text = dsCustomer.ALAMAT & " KELURAHAN : " & dsCustomer.M_KELURAHAN.MEMO & " KECAMATAN : " & dsCustomer.M_KELURAHAN.M_KECAMATAN.MEMO & " Kode Pos : " & dsCustomer.M_KELURAHAN.KODEPOS & " KABUPATEN/KOTA : " & dsCustomer.M_KELURAHAN.M_KECAMATAN.M_KABUPATEN.MEMO & " PROPINSI : " & dsCustomer.M_KELURAHAN.M_KECAMATAN.M_KABUPATEN.M_PROPINSI.MEMO & " NO TELEPON : " & dsCustomer.PHONE
            txtNOMORTELEPON.Text = IIf(dsCustomer.PHONE.ToString.Trim.ToUpper = "", "000000", dsCustomer.PHONE.ToString.Trim.ToUpper)
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

            fn_LoadKDSKDByKDCUSTOMER(dsCustomer.KDCUSTOMER, rbCATEGORY.SelectedIndex)

        Else
            fn_LoadKDSKDByKDCUSTOMER(txtKDCUSTOMER.Text, rbCATEGORY.SelectedIndex)
            txtKDCUSTOMER.ResetText()
            txtKARTUBPJS.ResetText()
            If txtNOMORRUJUKAN.Text = "" Then
                fn_EmptyMe()
            End If
            MsgBox("Pasien Tidak ditemukan di Database SIMRS", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
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
            SQL &= ",Tujuan = B.NAME_DISPLAY  "
            SQL &= ",DokterDPJP = C.NAME_DISPLAY "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_H A "
            SQL &= "INNER JOIN M_DEPARTMENT B "
            SQL &= "ON A.KDDEPARTMENT = B.KDDEPARTMENT "
            SQL &= "INNER JOIN M_DOCTOR C "
            SQL &= "ON A.KDDOCTOR = C.KDDOCTOR "
            SQL &= "WHERE A.KDCUSTOMER = '" & sKDCUSTOMER & "' "
            SQL &= "ORDER BY A.DATE DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "HISTORY")

            grdHistoryPasien.MainView = grvHistoryPasien
            grdHistoryPasien.DataSource = ds.Tables("HISTORY")
            grdHistoryPasien.ForceInitialize()

            fn_LoadFormatDataAll()
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
    End Sub
    Private Sub fn_LoadKDSKDByKDCUSTOMER(ByVal sKDCUSTOMER As String, ByVal sCATEGORY As Integer)
        Dim oSKD As New Admission.clsSKD
        Dim dsSKD = oSKD.GetDataByRM(sKDCUSTOMER)
        If dsSKD IsNot Nothing Then
            'Right(testString, 6)
            txtNOMORSKDP.Text = dsSKD.KDSKD
            grdKDDOCTOR_SKD.Text = dsSKD.KDDOCTOR
            txtNOMORRUJUKAN.Text = dsSKD.NOMORRUJUKAN
        Else
            If grdKDDOCTOR_SKD.Text = String.Empty Then
                txtNOMORSKDP.ResetText()
                grdKDDOCTOR_SKD.ResetText()

                'txtNOMORRUJUKAN.ResetText()

                Dim sMODUL As String = ""
                Dim sLASTNUMBER As Integer = 0

                If rbCATEGORY.SelectedIndex = 0 Then
                    sMODUL = "SKD-RJ"
                    Try
                        Dim oCounter As New Setting.clsCounter
                        sLASTNUMBER = oCounter.GetLastNumber(sMODUL, deDATE.DateTime)
                        If sLASTNUMBER = 0 Then
                            Try
                                oCounter.InsertData(sMODUL, deDATE.DateTime)
                                sLASTNUMBER = oCounter.GetLastNumber(sMODUL, deDATE.DateTime)
                            Catch ex As Exception
                                sLASTNUMBER = 0
                            End Try
                        End If

                        txtNOMORSKDP.Text = "Auto-" & AutoNumber(sMODUL, sLASTNUMBER + 1, deDATE.DateTime)

                        oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(deDATE.DateTime), Year(deDATE.DateTime))

                    Catch ex As Exception
                        'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        'Throw ex
                    End Try

                End If

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
            txtNOMORSKDP.Text = dsSKD.KDSKD
            grdKDDOCTOR_SKD.Text = dsSKD.KDDOCTOR
            txtNOMORRUJUKAN.Text = dsSKD.NOMORRUJUKAN
        Else
            MsgBox("Nomor SKD Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            txtNOMORSKDP.ResetText()
            grdKDDOCTOR_SKD.ResetText()
            txtNOMORRUJUKAN.ResetText()
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
            fn_LoadPropinsi()
            txtCARISUPLESI.Properties.ReadOnly = False
            grdSUPLESI.Properties.ReadOnly = False
            txtPENJAMIN_SUPLESI_NOSEPSUPLESI.Properties.ReadOnly = False
            grdSUPLESI_PROPINSI.Properties.ReadOnly = False
            grdSUPLESI_KABUPATEN.Properties.ReadOnly = False
            grdSUPLESI_KECAMATAN.Properties.ReadOnly = False

        Else
            txtCARISUPLESI.Properties.ReadOnly = True
            grdSUPLESI.Properties.ReadOnly = True
            txtPENJAMIN_SUPLESI_NOSEPSUPLESI.Properties.ReadOnly = True
            grdSUPLESI_PROPINSI.Properties.ReadOnly = True
            grdSUPLESI_KABUPATEN.Properties.ReadOnly = True
            grdSUPLESI_KECAMATAN.Properties.ReadOnly = True

        End If
    End Sub
    Private Sub fn_LoadPropinsi()
        Dim oPropinsi As New Reference.clsPropinsi
        Try
            grdSUPLESI_PROPINSI.Properties.DataSource = oPropinsi.GetData.Where(Function(x) x.ISACTIVE = True And x.VCLAIM_KODE <> "").ToList()
            grdSUPLESI_PROPINSI.Properties.ValueMember = "KDPROPINSI"
            grdSUPLESI_PROPINSI.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKabupaten()
        Dim oKabupaten As New Reference.clsKabupaten
        Try
            grdSUPLESI_KABUPATEN.Properties.DataSource = oKabupaten.GetData.Where(Function(x) x.ISACTIVE = True And x.VCLAIM_KODE <> "" And x.KDPROPINSI = grdSUPLESI_PROPINSI.EditValue).ToList()
            grdSUPLESI_KABUPATEN.Properties.ValueMember = "KDKABUPATEN"
            grdSUPLESI_KABUPATEN.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKecamatan()
        Dim oKecamatan As New Reference.clsKecamatan
        Try
            grdSUPLESI_KECAMATAN.Properties.DataSource = oKecamatan.GetData.Where(Function(x) x.ISACTIVE = True And x.VCLAIM_KODE <> "" And x.KDKABUPATEN = grdSUPLESI_KABUPATEN.EditValue).ToList()
            grdSUPLESI_KECAMATAN.Properties.ValueMember = "KDKECAMATAN"
            grdSUPLESI_KECAMATAN.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
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

            If rbCATEGORY.SelectedIndex = 0 Then
                Select Case cboCARI.SelectedIndex
                    Case 0
                        fn_LoadParameter(txtCARI.Text.ToString.Trim.ToUpper.PadLeft(9, "0"), 0)
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
                        Dim oCustomer As New Reference.clsCustomer
                        Dim KDCUSTOMER As String = String.Empty
                        Dim dsCustomer = oCustomer.GetDataKTP(txtCARI.Text.ToString.Trim.ToUpper)
                        If dsCustomer IsNot Nothing Then
                            KDCUSTOMER = dsCustomer.KDCUSTOMER
                        End If

                        fn_GetDataCustomer(KDCUSTOMER)
                        fn_LoadKTP(txtCARI.Text.ToString.Trim.ToUpper)
                    Case 5
                        fn_LoadParameter(txtCARI.Text.ToString.Trim.ToUpper, 2)
                        grdCARI.ShowPopup()
                    Case 6
                        fn_LoadParameter(txtCARI.Text.ToString.Trim.ToUpper, 4)
                        grdCARI.ShowPopup()
                    Case 7
                        fn_LoadKDSKD(txtCARI.Text.ToString.Trim.ToUpper)
                    Case 8
                        MsgBox("Pencarian Hanya Untuk Rawat Inap", MsgBoxStyle.Information, Me.Text)
                End Select
            Else
                '                No.Rekam Medis
                'No.Rujukan
                '                No.Kartu BPJS(1 Record)
                'No.Kartu BPJS(Multi Record)
                'No.NIK
                '                Nama Pasien
                'Alamat
                '                SKD
                '                Register Rawat Jalan
                Select Case cboCARI.SelectedIndex
                    Case 7, 3
                        MsgBox("Pencarian Hanya Untuk Rawat Jalan", MsgBoxStyle.Information, Me.Text)

                End Select

                Try
                    Dim dsPendaftaran = From x In oPendaftaran.GetDataByPendaftaranRJ(txtCARI.Text.ToString.Trim.ToUpper, cboCARI.SelectedIndex)
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
                Dim dsPendaftaranRawatJalan = oPendaftaran.GetData(grdCARI.EditValue)

                If dsPendaftaranRawatJalan IsNot Nothing Then
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

                    Dim oSetKoneksi As New Brigging.clsSetKoneksi
                    Dim PPKPELAYANAN = oSetKoneksi.GetData().Where(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM").FirstOrDefault.PPKPELAYANAN.ToString.Trim.ToUpper

                    Dim oFaskes As New Reference.clsPPK
                    cboASALRUJUKAN.SelectedIndex = oFaskes.GetDataKodeFaskes(PPKPELAYANAN).JENISFASKES
                    grdKDPPK.Text = oFaskes.GetDataKodeFaskes(PPKPELAYANAN).KDPPK

                    txtNOMORRUJUKAN.Text = dsPendaftaranRawatJalan.NOMORSEP
                    deDATE_RUJUKAN.DateTime = dsPendaftaranRawatJalan.DATE_RUJUKAN
                    txtNOMORSKDP.ResetText()
                    grdKDDOCTOR_SKD.ResetText()
                    grdKDCOB.Text = dsPendaftaranRawatJalan.KDCOB
                    chkCOB.Checked = dsPendaftaranRawatJalan.ISCOB
                    chkISEKSEKUTIF.Checked = dsPendaftaranRawatJalan.ISEKSEKUTIF
                    chkISKATARAK.Checked = dsPendaftaranRawatJalan.ISKATARAK
                    txtNOMORTELEPON.Text = dsPendaftaranRawatJalan.NOMORTELEPON
                    grdKDKELASRAWAT.Text = dsPendaftaranRawatJalan.KDKELASRAWAT
                    txtCATATAN.Text = dsPendaftaranRawatJalan.CATATAN

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
            Dim KDCUSTOMER As String = String.Empty

            KDCUSTOMER = txtKDCUSTOMER.Text.ToString.Trim.ToUpper.PadLeft(9, "0")
            fn_GetDataCustomer(KDCUSTOMER)

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
            fn_LoadKartuBPJSSatuRecord(KARTUBPJS)
            fn_LoadKartuBPJS(KARTUBPJS)
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

            Dim KTP As String = String.Empty
            KTP = txtKARTUBPJS.Text.ToString.Trim.ToUpper

            fn_GetDataCustomer(KDCUSTOMER)
            fn_LoadKTP(KTP)

        End If
    End Sub
    Private Sub rbCATEGORY_SelectedIndexChanged() Handles rbCATEGORY.SelectedIndexChanged
        fn_LoadDepartment()
        grdKDDOCTOR.Properties.DataSource = Nothing
    End Sub
    Private Sub grdKDDEPARTMENT_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdKDDEPARTMENT.KeyPress
        If Asc(e.KeyChar) = 13 Then
            fn_LoadDoctor(grdKDDEPARTMENT.EditValue)

            Dim oPOLI As New Reference.clsDepartment
            If oPOLI.GetData(grdKDDEPARTMENT.EditValue).VCLAIM_KODEPOLI = "IGD" Then
                cboASALRUJUKAN.SelectedIndex = 1
                Dim oFaskes As New Reference.clsPPK
                Dim oSetKoneksi As New Brigging.clsSetKoneksi

                Dim dsFaskes = oFaskes.GetDataKodeFaskes(oSetKoneksi.GetData().Where(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM").FirstOrDefault.PPKPELAYANAN.ToString.Trim.ToUpper)
                If dsFaskes IsNot Nothing Then
                    grdKDPPK.Text = dsFaskes.KDPPK
                End If

            End If

            grdKDDOCTOR.ShowPopup()
        End If
    End Sub
    Private Sub grdKDDOCTOR_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdKDDOCTOR.KeyPress
        If Asc(e.KeyChar) = 13 Then
            grdKDDIAGNOSA.ShowPopup()
        End If
    End Sub
    Private Sub grdKDDAFTAR_L1_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDDAFTAR_L1.EditValueChanged
        If grdKDDAFTAR_L1.Text = "BPJS" Then
            chkIsOfline.Properties.ReadOnly = False
            chkIsOfline.Checked = False
            txtKARTUBPJS.Properties.MaxLength = 13
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
    Private Sub grdSUPLESI_PROPINSI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdSUPLESI_PROPINSI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            fn_LoadKabupaten()
            grdSUPLESI_KABUPATEN.ShowPopup()
        End If
    End Sub
    Private Sub grdSUPLESI_KABUPATEN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdSUPLESI_KABUPATEN.KeyPress
        If Asc(e.KeyChar) = 13 Then
            fn_LoadKecamatan()
            grdSUPLESI_KECAMATAN.ShowPopup()
        End If
    End Sub
    Private Sub txtCARISUPLESI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCARISUPLESI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            fn_LoadSuplesi(txtCARISUPLESI.Text.Trim.ToUpper)
        End If
    End Sub
    Private Sub grdSUPLESI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdSUPLESI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtPENJAMIN_SUPLESI_NOSEPSUPLESI.Text = grdSUPLESI.Text
        End If
    End Sub
    Private Sub txtNOMORSKDP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNOMORSKDP.KeyPress
        If Asc(e.KeyChar) = 13 Then
            fn_LoadKDSKD(txtNOMORSKDP.Text.ToString.Trim.ToUpper)
        End If
    End Sub
    Private Sub txtNOMORSEP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNOMORSEP.KeyPress
        If txtKDPENDAFTARAN.Text <> "<--- AUTO --->" Then
            If fn_CariSEP(txtNOMORSEP.Text.ToString.Trim.ToUpper) = True Then
                oPendaftaran.UpdateSEP(txtKDPENDAFTARAN.Text, txtNOMORSEP.Text.ToString.Trim.ToUpper)
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

            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim dsSetKoneksi = oSetKoneksi.GetDataVClaimPesertaNoKartuBPJS("VCLAIM", KARTUBPJS, deDATE.DateTime.ToString("yyyy-MM-dd"))

            Dim allData = JObject.Parse(dsSetKoneksi)
            Dim CodeResponse As String = String.Empty
            Dim messageResponse As String = String.Empty

            CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
            messageResponse = allData("metaData")("message").ToString

            If CodeResponse = "200" Then
                Dim oKelas As New Reference.clsKelasRawat
                grdKDKELASRAWAT.Text = oKelas.GetDataByKode(allData("response")("peserta")("hakKelas")("kode").ToString()).KDKELASRAWAT

                Dim NoTelepon = allData("response")("peserta")("mr")("noTelepon").ToString()
                If NoTelepon <> "" Then
                    If NoTelepon <> "00000000" Then
                        txtNOMORTELEPON.Text = NoTelepon
                    End If
                End If

                Dim jenisPeserta As String = allData("response")("peserta")("jenisPeserta")("keterangan").ToString()
                Dim kodejenisPeserta As String = allData("response")("peserta")("jenisPeserta")("kode").ToString()

                If jenisPeserta <> "" Then
                    InsertJenisPeserta(kodejenisPeserta, jenisPeserta)
                End If

                'Insert

                Dim nmAsuransi As String = allData("response")("peserta")("cob")("nmAsuransi").ToString()
                Dim noAsuransi As String = allData("response")("peserta")("cob")("noAsuransi").ToString()
                Dim tglTAT As String = allData("response")("peserta")("cob")("tglTAT").ToString()
                Dim tglTMT As String = allData("response")("peserta")("cob")("tglTMT").ToString()

                Dim nmProvider As String = allData("response")("peserta")("provUmum")("nmProvider").ToString()
                Dim kodeProvider As String = allData("response")("peserta")("provUmum")("kdProvider").ToString()

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
                         & "  - Nama Auransi: " & allData("response")("peserta")("cob")("nmAsuransi").ToString() & vbCrLf _
                         & "  - No Auransi: " & allData("response")("peserta")("cob")("noAsuransi").ToString() & vbCrLf _
                         & "  - Tgl TAT : " & allData("response")("peserta")("cob")("tglTAT").ToString() & vbCrLf _
                         & "  - Tgl TMT: " & allData("response")("peserta")("cob")("tglTMT").ToString() & vbCrLf _
                         & "" & vbCrLf _
                         & "* Informasi Dinsos " & vbCrLf _
                         & "  - Dinsos: " & allData("response")("peserta")("informasi")("dinsos").ToString() & vbCrLf _
                         & "  - No SKTM: " & allData("response")("peserta")("informasi")("noSKTM").ToString() & vbCrLf _
                         & "  - Prolanis: PRB " & allData("response")("peserta")("informasi")("prolanisPRB").ToString() & vbCrLf _
                         & "" & vbCrLf _
                         & "Tgl Cetak Kartu: " & allData("response")("peserta")("tglCetakKartu").ToString() & vbCrLf _
                         & "Tgl TAT: " & allData("response")("peserta")("tglTAT").ToString() & vbCrLf _
                         & "Tgl TMT: " & allData("response")("peserta")("tglTMT").ToString()

                sJudul = allData("response")("peserta")("statusPeserta")("keterangan").ToString() & " !!!"
                Dim Message As New frmPesertaBPJS
                Message.LoadMe(sJudul, allData("response")("peserta")("nama").ToString(), allData("response")("peserta")("jenisPeserta")("keterangan").ToString(), allData("response")("peserta")("noKartu").ToString(), txtKDCUSTOMER.Text.ToString.PadLeft(9, "0"), allData("response")("peserta")("mr")("noMR").ToString().PadLeft(9, "0"), IIf(allData("response")("peserta")("sex").ToString() = "L", "Laki-Laki", "Perempuan"), allData("response")("peserta")("tglLahir").ToString(), allData("response")("peserta")("umur")("umurSekarang").ToString(), allData("response")("peserta")("provUmum")("kdProvider").ToString() & " - " & allData("response")("peserta")("provUmum")("nmProvider").ToString(), sMessage)
                Message.ShowDialog(Me)

                sINFORMASIPRB = allData("response")("peserta")("informasi")("prolanisPRB").ToString
            Else
                MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKTP(ByVal KTP As String)
        Try
            If KTP = String.Empty Then Exit Sub

            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim dsSetKoneksi = oSetKoneksi.GetDataVClaimPesertaNIK("VCLAIM", KTP, deDATE.DateTime.ToString("yyyy-MM-dd"))

            Dim allData = JObject.Parse(dsSetKoneksi)
            Dim CodeResponse As String = String.Empty
            Dim messageResponse As String = String.Empty

            CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
            messageResponse = allData("metaData")("message").ToString

            If CodeResponse = "200" Then
                Dim oKelas As New Reference.clsKelasRawat
                grdKDKELASRAWAT.Text = oKelas.GetDataByKode(allData("response")("peserta")("hakKelas")("kode").ToString()).KDKELASRAWAT

                Dim NoTelepon = allData("response")("peserta")("mr")("noTelepon").ToString()
                If NoTelepon <> "" Then
                    If NoTelepon <> "00000000" Then
                        txtNOMORTELEPON.Text = NoTelepon
                    End If
                End If

                Dim jenisPeserta As String = allData("response")("peserta")("jenisPeserta")("keterangan").ToString()
                Dim kodejenisPeserta As String = allData("response")("peserta")("jenisPeserta")("kode").ToString()

                If jenisPeserta <> "" Then
                    InsertJenisPeserta(kodejenisPeserta, jenisPeserta)
                End If

                Dim sJudul As String = String.Empty
                Dim sMessage As String = String.Empty

                sMessage = "* Peserta COB " & vbCrLf _
                         & "  - Nama Auransi: " & allData("response")("peserta")("cob")("nmAsuransi").ToString() & vbCrLf _
                         & "  - No Auransi: " & allData("response")("peserta")("cob")("noAsuransi").ToString() & vbCrLf _
                         & "  - Tgl TAT : " & allData("response")("peserta")("cob")("tglTAT").ToString() & vbCrLf _
                         & "  - Tgl TMT: " & allData("response")("peserta")("cob")("tglTMT").ToString() & vbCrLf _
                         & "" & vbCrLf _
                         & "* Informasi Dinsos " & vbCrLf _
                         & "  - Dinsos: " & allData("response")("peserta")("informasi")("dinsos").ToString() & vbCrLf _
                         & "  - No SKTM: " & allData("response")("peserta")("informasi")("noSKTM").ToString() & vbCrLf _
                         & "  - Prolanis: PRB " & allData("response")("peserta")("informasi")("prolanisPRB").ToString() & vbCrLf _
                         & "" & vbCrLf _
                         & "Tgl Cetak Kartu: " & allData("response")("peserta")("tglCetakKartu").ToString() & vbCrLf _
                         & "Tgl TAT: " & allData("response")("peserta")("tglTAT").ToString() & vbCrLf _
                         & "Tgl TMT: " & allData("response")("peserta")("tglTMT").ToString()

                sJudul = allData("response")("peserta")("statusPeserta")("keterangan").ToString() & " !!!"
                Dim Message As New frmPesertaBPJS
                Message.LoadMe(sJudul, allData("response")("peserta")("nama").ToString(), allData("response")("peserta")("jenisPeserta")("keterangan").ToString(), allData("response")("peserta")("noKartu").ToString(), txtKDCUSTOMER.Text.ToString.PadLeft(9, "0"), allData("response")("peserta")("mr")("noMR").ToString().PadLeft(9, "0"), IIf(allData("response")("peserta")("sex").ToString() = "L", "Laki-Laki", "Perempuan"), allData("response")("peserta")("tglLahir").ToString(), allData("response")("peserta")("umur")("umurSekarang").ToString(), allData("response")("peserta")("provUmum")("kdProvider").ToString() & " - " & allData("response")("peserta")("provUmum")("nmProvider").ToString(), sMessage)
                Message.ShowDialog(Me)

                sINFORMASIPRB = allData("response")("peserta")("informasi")("prolanisPRB").ToString
            Else
                MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKartuBPJSSatuRecord(ByVal sKARTUBPS As String)
        Try
            If sKARTUBPS = String.Empty Then Exit Sub

            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim dsSetKoneksi = oSetKoneksi.CariRujukanKartuSatuRecord("VCLAIM", sKARTUBPS, cboASALRUJUKAN.SelectedIndex)

            Dim allData = JObject.Parse(dsSetKoneksi)
            Dim CodeResponse As String = String.Empty
            Dim messageResponse As String = String.Empty

            CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
            messageResponse = allData("metaData")("message").ToString

            If CodeResponse = "200" Then
                txtNOMORRUJUKAN.Text = allData("response")("rujukan")("noKunjungan").ToString()
                deDATE_RUJUKAN.DateTime = allData("response")("rujukan")("tglKunjungan").ToString()

                InsertDiagnosa(allData("response")("rujukan")("diagnosa")("kode").ToString(), allData("response")("rujukan")("diagnosa")("kode").ToString() & " - " & allData("response")("rujukan")("diagnosa")("nama").ToString())

                Dim oKelas As New Reference.clsKelasRawat
                grdKDKELASRAWAT.Text = oKelas.GetDataByKode(allData("response")("rujukan")("peserta")("hakKelas")("kode").ToString()).KDKELASRAWAT

                txtCATATAN.Text = allData("response")("rujukan")("keluhan").ToString()

                Dim NoTelepon = allData("response")("rujukan")("peserta")("mr")("noTelepon").ToString()

                If NoTelepon <> "" Then
                    If NoTelepon <> "00000000" Then
                        txtNOMORTELEPON.Text = NoTelepon
                    End If
                End If

                Dim oDepartment As New Reference.clsDepartment

                Dim dsDepartment = oDepartment.GetDataByKodeVclaim(allData("response")("rujukan")("poliRujukan")("kode").ToString())

                If dsDepartment IsNot Nothing Then
                    grdKDDEPARTMENT.Text = dsDepartment.KDDEPARTMENT
                    fn_LoadDoctor(dsDepartment.KDDEPARTMENT)
                    grdKDDOCTOR.ShowPopup()
                End If

                'Insert

                Dim nmAsuransi As String = allData("response")("rujukan")("peserta")("cob")("nmAsuransi").ToString()
                Dim noAsuransi As String = allData("response")("rujukan")("peserta")("cob")("noAsuransi").ToString()
                Dim tglTAT As String = allData("response")("rujukan")("peserta")("cob")("tglTAT").ToString()
                Dim tglTMT As String = allData("response")("rujukan")("peserta")("cob")("tglTMT").ToString()

                Dim nmProvider As String = allData("response")("rujukan")("peserta")("provUmum")("nmProvider").ToString()
                Dim kodeProvider As String = allData("response")("rujukan")("peserta")("provUmum")("kdProvider").ToString()

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
                grdKDDEPARTMENT.ResetText()
                grdKDDOCTOR.ResetText()
                grdKDPPK.Reset()
                grdKDDOCTOR.Properties.DataSource = Nothing
                MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKartuBPJSMultiRecord(ByVal sKARTUBPS As String)
        Try
            If sKARTUBPS = String.Empty Then Exit Sub

            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim dsSetKoneksi = oSetKoneksi.CariRujukanKartuMultiRecord("VCLAIM", sKARTUBPS, cboASALRUJUKAN.SelectedIndex)

            Dim allData = JObject.Parse(dsSetKoneksi)
            Dim CodeResponse As String = String.Empty
            Dim messageResponse As String = String.Empty

            CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
            messageResponse = allData("metaData")("message").ToString

            If CodeResponse = "200" Then
                Dim table As DataTable

                table = New DataTable("M_RUJUKAN")

                table.Columns.Add("noKunjungan")
                table.Columns.Add("tglKunjungan")
                table.Columns.Add("noKartu")
                table.Columns.Add("nama")
                table.Columns.Add("provPerujuk")
                table.Columns.Add("poliRujukan")

                For Each item In allData("response")("rujukan")
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

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadNomorRujukan(ByVal sNOMORRUJUKAN As String)
        Try
            If sNOMORRUJUKAN = String.Empty Then Exit Sub
            If chkIsOfline.Checked = True Then Exit Sub

            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim dsSetKoneksi = oSetKoneksi.CariRujukan("VCLAIM", sNOMORRUJUKAN, cboASALRUJUKAN.SelectedIndex)

            Dim allData = JObject.Parse(dsSetKoneksi)
            Dim CodeResponse As String = String.Empty
            Dim messageResponse As String = String.Empty

            CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
            messageResponse = allData("metaData")("message").ToString

            If CodeResponse = "200" Then
                txtNOMORRUJUKAN.Text = allData("response")("rujukan")("noKunjungan").ToString()

                deDATE_RUJUKAN.DateTime = allData("response")("rujukan")("tglKunjungan").ToString()

                InsertDiagnosa(allData("response")("rujukan")("diagnosa")("kode").ToString(), allData("response")("rujukan")("diagnosa")("kode").ToString() & " - " & allData("response")("rujukan")("diagnosa")("nama").ToString())

                Dim oKelas As New Reference.clsKelasRawat
                grdKDKELASRAWAT.Text = oKelas.GetDataByKode(allData("response")("rujukan")("peserta")("hakKelas")("kode").ToString()).KDKELASRAWAT

                If allData("response")("rujukan")("keluhan").ToString() <> "" Then
                    txtCATATAN.Text = allData("response")("rujukan")("keluhan").ToString()
                End If

                Dim NoTelepon = allData("response")("rujukan")("peserta")("mr")("noTelepon").ToString()
                If NoTelepon <> "" Then
                    If NoTelepon <> "00000000" Then
                        txtNOMORTELEPON.Text = NoTelepon
                    End If
                End If

                Dim oDepartment As New Reference.clsDepartment

                Dim dsDepartment = oDepartment.GetDataByKodeVclaim(allData("response")("rujukan")("poliRujukan")("kode").ToString())

                If dsDepartment IsNot Nothing Then
                    grdKDDEPARTMENT.Text = dsDepartment.KDDEPARTMENT
                    fn_LoadDoctor(dsDepartment.KDDEPARTMENT)
                    grdKDDOCTOR.ShowPopup()
                End If

                Dim nmAsuransi As String = allData("response")("rujukan")("peserta")("cob")("nmAsuransi").ToString()
                Dim noAsuransi As String = allData("response")("rujukan")("peserta")("cob")("noAsuransi").ToString()
                Dim tglTAT As String = allData("response")("rujukan")("peserta")("cob")("tglTAT").ToString()
                Dim tglTMT As String = allData("response")("rujukan")("peserta")("cob")("tglTMT").ToString()

                Dim nmProvider As String = allData("response")("rujukan")("peserta")("provUmum")("nmProvider").ToString()
                Dim kodeProvider As String = allData("response")("rujukan")("peserta")("provUmum")("kdProvider").ToString()

                If nmAsuransi <> "" Then
                    InsertCOB(noAsuransi, nmAsuransi, tglTAT, tglTMT)
                End If
                If nmProvider <> "" Then
                    InsertPPK(kodeProvider, nmProvider)
                End If

                txtKARTUBPJS.Text = allData("response")("rujukan")("peserta")("noKartu")
                txtKDCUSTOMER.Text = allData("response")("rujukan")("peserta")("mr")("noMR")
                fn_LoadKartuBPJS(txtKARTUBPJS.Text)
                fn_GetDataCustomer(txtKDCUSTOMER.Text.ToString.Trim.ToUpper.PadLeft(9, "0"))
            Else
                deDATE_RUJUKAN.DateTime = Now
                txtNOMORRUJUKAN.ResetText()
                grdKDDIAGNOSA.ResetText()
                grdKDDEPARTMENT.ResetText()
                grdKDDOCTOR.ResetText()
                grdKDPPK.Reset()
                grdKDDOCTOR.Properties.DataSource = Nothing
                MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadSuplesi(ByVal sNoKartuPeserta As String)
        Try
            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim dsSetKoneksi = oSetKoneksi.GetDataSuplesiJasaRaharja("VCLAIM", sNoKartuPeserta, deDATE.DateTime.ToString("yyyy-MM-dd"))

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

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_CreateSEP() As String
        Try
            fn_CreateSEP = ""
            Dim NOMORSEP As String = String.Empty
            REQUEST = ""
            RESPONSE = ""

            If chkIsOfline.Checked = True Then Exit Function

            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim jsonRequest As String = String.Empty
            Dim oFaskes As New Reference.clsPPK
            Dim oPoli As New Reference.clsDepartment
            Dim oDoctor As New Reference.clsDoctor
            Dim oKelas As New Reference.clsKelasRawat
            Dim jaminanPenjamin As String = String.Empty

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
            jsonRequest &= """ppkPelayanan"": """ & oSetKoneksi.GetData().Where(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM").FirstOrDefault.PPKPELAYANAN.ToString.Trim.ToUpper & """, "
            jsonRequest &= """jnsPelayanan"": """ & IIf(rbCATEGORY.SelectedIndex = 0, 2, 1) & """, "
            jsonRequest &= """klsRawat"": """ & oKelas.GetData(grdKDKELASRAWAT.EditValue).KODEPENDAFTARAN & """, "
            jsonRequest &= """noMR"": """ & txtKDCUSTOMER.Text.ToString.Trim.ToUpper.PadLeft(6, "0") & """, "
            jsonRequest &= """rujukan"": { "
            jsonRequest &= """asalRujukan"": """ & cboASALRUJUKAN.SelectedIndex + 1 & """, "
            jsonRequest &= """tglRujukan"": """ & deDATE_RUJUKAN.DateTime.ToString("yyyy-MM-dd") & """, "
            jsonRequest &= """noRujukan"": """ & txtNOMORRUJUKAN.Text.ToString.Trim.ToUpper & """, "
            jsonRequest &= """ppkRujukan"": """ & oFaskes.GetData(grdKDPPK.EditValue).KODEFASKES & """ "
            jsonRequest &= "}, "
            jsonRequest &= """catatan"": """ & txtCATATAN.Text.ToString.Trim.ToUpper & """, "
            jsonRequest &= """diagAwal"": """ & grdKDDIAGNOSA.EditValue & """, "
            jsonRequest &= """poli"": { "
            jsonRequest &= """tujuan"": """ & IIf(rbCATEGORY.SelectedIndex = 0, oPoli.GetData(grdKDDEPARTMENT.EditValue).VCLAIM_KODEPOLI, "") & """, "
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
                jsonRequest &= """kdPropinsi"": """ & grdSUPLESI_PROPINSI.EditValue & """, "
                jsonRequest &= """kdKabupaten"": """ & grdSUPLESI_KABUPATEN.EditValue & """, "
                jsonRequest &= """kdKecamatan"": """ & grdSUPLESI_KECAMATAN.EditValue & """ "
            End If

            Dim DPJP = grdKDDOCTOR_SKD.Text

            If DPJP = "" Then
                DPJP = String.Empty
            Else
                DPJP = oDoctor.GetData(grdKDDOCTOR_SKD.EditValue).VCLAIM_KDDPJP
            End If

            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "}, "
            jsonRequest &= """skdp"": { "
            jsonRequest &= """noSurat"": """ & IIf(txtNOMORSKDP.Text = String.Empty, "", Microsoft.VisualBasic.Left(txtNOMORSKDP.Text, 6)) & """, "
            jsonRequest &= """kodeDPJP"": """ & DPJP & """ "
            jsonRequest &= "}, "
            jsonRequest &= """noTelp"": """ & txtNOMORTELEPON.Text.ToString.Trim.ToUpper & """, "
            jsonRequest &= """user"": """ & sUserID & """ "
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "}  "

            Dim dsSetKoneksi = oSetKoneksi.InsertSEP("VCLAIM", jsonRequest)


            If dsSetKoneksi <> "" Then
                Dim allData = JObject.Parse(dsSetKoneksi)
                Dim CodeResponse As String = String.Empty
                Dim messageResponse As String = String.Empty

                CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                messageResponse = allData("metaData")("message").ToString

                If CodeResponse = "200" Then
                    REQUEST = jsonRequest
                    RESPONSE = dsSetKoneksi
                    NOMORSEP = allData.Item("response")("sep")("noSep").ToString()
                Else
                    MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                    Exit Function
                End If
            End If

            fn_CreateSEP = NOMORSEP

        Catch oErr As Exception
            fn_CreateSEP = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_UpdateSEP() As Boolean
        Try
            fn_UpdateSEP = True

            If chkIsOfline.Checked = True Then Exit Function

            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim jsonRequest As String = String.Empty
            Dim oFaskes As New Reference.clsPPK
            Dim oPoli As New Reference.clsDepartment
            Dim oDoctor As New Reference.clsDoctor
            Dim oKelas As New Reference.clsKelasRawat
            Dim jaminanPenjamin As String = String.Empty

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
            jsonRequest &= """klsRawat"": """ & IIf(rbCATEGORY.SelectedIndex = 0, 3, oKelas.GetData(grdKDKELASRAWAT.EditValue).KODEPENDAFTARAN) & ""","
            jsonRequest &= """noMR"": """ & txtKDCUSTOMER.Text.ToString.Trim.ToUpper.PadLeft(6, "0") & """, "
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

            Dim DPJP = grdKDDOCTOR_SKD.Text

            If DPJP = "" Then
                DPJP = String.Empty
            Else
                DPJP = oDoctor.GetData(grdKDDOCTOR_SKD.EditValue).VCLAIM_KDDPJP
            End If

            jsonRequest &= """noSurat"":""" & IIf(txtNOMORSKDP.Text = String.Empty, "", Microsoft.VisualBasic.Left(txtNOMORSKDP.Text, 6)) & """, "
            jsonRequest &= """kodeDPJP"":""" & DPJP & """ "
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
                jsonRequest &= """kdPropinsi"": """ & grdSUPLESI_PROPINSI.EditValue & """, "
                jsonRequest &= """kdKabupaten"": """ & grdSUPLESI_KABUPATEN.EditValue & """, "
                jsonRequest &= """kdKecamatan"": """ & grdSUPLESI_KECAMATAN.EditValue & """ "
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

            Dim dsSetKoneksi = oSetKoneksi.UpdateSEP("VCLAIM", jsonRequest)

            If dsSetKoneksi <> "" Then
                Dim allData = JObject.Parse(dsSetKoneksi)
                Dim CodeResponse As String = String.Empty
                Dim messageResponse As String = String.Empty

                CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                messageResponse = allData("metaData")("message").ToString

                If CodeResponse = "200" Then
                    REQUEST = jsonRequest
                    RESPONSE = dsSetKoneksi
                Else
                    fn_UpdateSEP = False
                    MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                End If

            End If

        Catch oErr As Exception
            fn_UpdateSEP = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_CariSEP(ByVal NOMORSEP As String) As Boolean
        Try
            If NOMORSEP = String.Empty Then
                fn_CariSEP = False
                Exit Function
            End If

            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim dsSetKoneksi = oSetKoneksi.CariSEP("VCLAIM", NOMORSEP)

            Dim allData = JObject.Parse(dsSetKoneksi)
            Dim CodeResponse As String = String.Empty
            Dim messageResponse As String = String.Empty

            CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
            messageResponse = allData("metaData")("message").ToString

            If CodeResponse = "200" Then
                fn_CariSEP = True
            Else
                fn_CariSEP = False
            End If

        Catch oErr As Exception
            fn_CariSEP = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function

#End Region
End Class