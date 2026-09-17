Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data
Imports System.Data.SqlClient

Public Class frmEMedrekTriage
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_Triage As New Digital.clsDigital_Triage
    Private down As Boolean = False
    Private sNoid As String
    Private sKodeIdentitas As Integer
    Private sKDDOCTOR As String
    Private sKeluar As Boolean = False

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDDOCTOR As String, ByVal KodeIdentitas As Integer, ByVal NoId As String, ByVal keluar As Boolean)
        oFormMode = FormMode

        sKodeIdentitas = KodeIdentitas
        sNoid = NoId
        sKDDOCTOR = KDDOCTOR
        sKeluar = keluar
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "LEMBAR TRIAGE"
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
        fn_NOIDUSER()
        fn_Doctor2()

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

        chkJALANNAFAS1.Properties.ReadOnly = Status
        chkJALANNAFAS2.Properties.ReadOnly = Status
        chkJALANNAFAS3.Properties.ReadOnly = Status
        chkJALANNAFAS4.Properties.ReadOnly = Status
        chkPERNAFASAN1.Properties.ReadOnly = Status
        chkPERNAFASAN2.Properties.ReadOnly = Status
        chkPERNAFASAN3.Properties.ReadOnly = Status
        chkPERNAFASAN4.Properties.ReadOnly = Status
        chkSPO2DEWASA1.Properties.ReadOnly = Status
        chkSPO2DEWASA2.Properties.ReadOnly = Status
        chkSPO2DEWASA3.Properties.ReadOnly = Status
        chkSPO2ANAK1.Properties.ReadOnly = Status
        chkSPO2ANAK2.Properties.ReadOnly = Status
        chkSPO2ANAK3.Properties.ReadOnly = Status
        chkRRDEWASA1.Properties.ReadOnly = Status
        chkRRDEWASA2.Properties.ReadOnly = Status
        chkRRDEWASA3.Properties.ReadOnly = Status
        chkRRANAK1BLN1.Properties.ReadOnly = Status
        chkRRANAK1BLN2.Properties.ReadOnly = Status
        chkRRANAK1BLN3.Properties.ReadOnly = Status
        chkRRANAK14BLN1.Properties.ReadOnly = Status
        chkRRANAK14BLN2.Properties.ReadOnly = Status
        chkRRANAK14BLN3.Properties.ReadOnly = Status
        chkRRANAK41.Properties.ReadOnly = Status
        chkRRANAK42.Properties.ReadOnly = Status
        chkRRANAK43.Properties.ReadOnly = Status
        chkSIRKULASI1.Properties.ReadOnly = Status
        chkSIRKULASI2.Properties.ReadOnly = Status
        chkSIRKULASI3.Properties.ReadOnly = Status
        chkSIRKULASI4.Properties.ReadOnly = Status
        chkHRDEWASA1.Properties.ReadOnly = Status
        chkHRDEWASA2.Properties.ReadOnly = Status
        chkHRDEWASA3.Properties.ReadOnly = Status
        chkHRANAK1BLN1.Properties.ReadOnly = Status
        chkHRANAK1BLN2.Properties.ReadOnly = Status
        chkHRANAK1BLN3.Properties.ReadOnly = Status
        chkHRANAK14BLN1.Properties.ReadOnly = Status
        chkHRANAK14BLN2.Properties.ReadOnly = Status
        chkHRANAK14BLN3.Properties.ReadOnly = Status
        chkHRANAK4BLN1.Properties.ReadOnly = Status
        chkHRANAK4BLN2.Properties.ReadOnly = Status
        chkHRANAK4BLN3.Properties.ReadOnly = Status
        chkMAP1.Properties.ReadOnly = Status
        chkMAP2.Properties.ReadOnly = Status
        chkMAP3.Properties.ReadOnly = Status
        chkSISTOLIC1.Properties.ReadOnly = Status
        chkSISTOLIC2.Properties.ReadOnly = Status
        chkSISTOLIC3.Properties.ReadOnly = Status
        chkDIASTOLIC1.Properties.ReadOnly = Status
        chkDIASTOLIC2.Properties.ReadOnly = Status
        chkDIASTOLIC3.Properties.ReadOnly = Status
        chkEKG1.Properties.ReadOnly = Status
        chkEKG2.Properties.ReadOnly = Status
        chkEKG3.Properties.ReadOnly = Status
        chkDISABILITASSPONTANEOUS.Properties.ReadOnly = Status
        chkDISABILITASORIENTED.Properties.ReadOnly = Status
        chkDISABILITASOBEYSCOMMANDS.Properties.ReadOnly = Status
        chkDISABILITASTOVOICE.Properties.ReadOnly = Status
        chkDISABILITASCONFUSED.Properties.ReadOnly = Status
        chkDISABILITASLOCALIZESPAIN.Properties.ReadOnly = Status
        chkDISABILITASTOPAIN.Properties.ReadOnly = Status
        chkDISABILITASINAPPROPRIATE.Properties.ReadOnly = Status
        chkDISABILITASWITHDRAWSTOPAIN.Properties.ReadOnly = Status
        chkDISABILITAS1NONE.Properties.ReadOnly = Status
        chkDISABILITASINCOMPREHENSIBLE.Properties.ReadOnly = Status
        chkDISABILITASFLEXIONTOPAIN.Properties.ReadOnly = Status
        chkDISABILITAS2NONE.Properties.ReadOnly = Status
        chkDISABILITASEXTENTIONTOPAIN.Properties.ReadOnly = Status
        chkDISABILITAS3NONE.Properties.ReadOnly = Status
        txtEYEOPENING.Properties.ReadOnly = Status
        txtVERBAL.Properties.ReadOnly = Status
        txtMOTOR.Properties.ReadOnly = Status
        chkKESADARANKEJANG1.Properties.ReadOnly = Status
        chkKESADARANKEJANG2.Properties.ReadOnly = Status
        chkKESADARANKEJANG3.Properties.ReadOnly = Status
        chkKESADARANKEJANG4.Properties.ReadOnly = Status
        chkKESADARANKEJANG5.Properties.ReadOnly = Status
        chkKESADARANKEJANG6.Properties.ReadOnly = Status
        chkKESADARANKEJANG7.Properties.ReadOnly = Status
        chkKESADARANKEJANG8.Properties.ReadOnly = Status
        chkKESADARANKEJANG9.Properties.ReadOnly = Status
        chkKESADARANKEJANG10.Properties.ReadOnly = Status
        chkSUASANASKALANYERI1.Properties.ReadOnly = Status
        chkSUASANASKALANYERI2.Properties.ReadOnly = Status
        chkSUASANASKALANYERI3.Properties.ReadOnly = Status
        chkSUASANASKALANYERI4.Properties.ReadOnly = Status
        chkSUASANAPENDARAHAN1.Properties.ReadOnly = Status
        chkSUASANAPENDARAHAN2.Properties.ReadOnly = Status
        chkSUASANAPENDARAHAN3.Properties.ReadOnly = Status
        chkSUASANAPAPARAN1.Properties.ReadOnly = Status
        chkSUASANAPAPARAN2.Properties.ReadOnly = Status
        chkSUASANAPAPARAN3.Properties.ReadOnly = Status
        chkSUASANASUHU1.Properties.ReadOnly = Status
        chkSUASANASUHU2.Properties.ReadOnly = Status
        chkSUASANASUHU3.Properties.ReadOnly = Status
        chkRUJUKAN1.Properties.ReadOnly = Status
        chkRUJUKAN2.Properties.ReadOnly = Status
        chkKEBUTUHAN1.Properties.ReadOnly = Status
        chkKEBUTUHAN2.Properties.ReadOnly = Status
        chkKEPUTUSAN1.Properties.ReadOnly = Status
        chkKEPUTUSAN2.Properties.ReadOnly = Status
        chkKEPUTUSAN3.Properties.ReadOnly = Status
        chkKEPUTUSAN4.Properties.ReadOnly = Status
        chkKESIMPULAN1.Properties.ReadOnly = Status
        chkKESIMPULAN2.Properties.ReadOnly = Status
        chkPENGANTAR1.Properties.ReadOnly = Status
        chkPENGANTAR2.Properties.ReadOnly = Status
        chkPENGANTAR3.Properties.ReadOnly = Status
        chkPENGANTAR4.Properties.ReadOnly = Status
        txtRIWAYATKELUHANUTAMA.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        'txtCODE.Text = "<--- AUTO --->"

        chkJALANNAFAS1.Checked = False
        chkJALANNAFAS2.Checked = False
        chkJALANNAFAS3.Checked = False
        chkJALANNAFAS4.Checked = False
        chkPERNAFASAN1.Checked = False
        chkPERNAFASAN2.Checked = False
        chkPERNAFASAN3.Checked = False
        chkPERNAFASAN4.Checked = False
        chkSPO2DEWASA1.Checked = False
        chkSPO2DEWASA2.Checked = False
        chkSPO2DEWASA3.Checked = False
        chkSPO2ANAK1.Checked = False
        chkSPO2ANAK2.Checked = False
        chkSPO2ANAK3.Checked = False
        chkRRDEWASA1.Checked = False
        chkRRDEWASA2.Checked = False
        chkRRDEWASA3.Checked = False
        chkRRANAK1BLN1.Checked = False
        chkRRANAK1BLN2.Checked = False
        chkRRANAK1BLN3.Checked = False
        chkRRANAK14BLN1.Checked = False
        chkRRANAK14BLN2.Checked = False
        chkRRANAK14BLN3.Checked = False
        chkRRANAK41.Checked = False
        chkRRANAK42.Checked = False
        chkRRANAK43.Checked = False
        chkSIRKULASI1.Checked = False
        chkSIRKULASI2.Checked = False
        chkSIRKULASI3.Checked = False
        chkSIRKULASI4.Checked = False
        chkHRDEWASA1.Checked = False
        chkHRDEWASA2.Checked = False
        chkHRDEWASA3.Checked = False
        chkHRANAK1BLN1.Checked = False
        chkHRANAK1BLN2.Checked = False
        chkHRANAK1BLN3.Checked = False
        chkHRANAK14BLN1.Checked = False
        chkHRANAK14BLN2.Checked = False
        chkHRANAK14BLN3.Checked = False
        chkHRANAK4BLN1.Checked = False
        chkHRANAK4BLN2.Checked = False
        chkHRANAK4BLN3.Checked = False
        chkMAP1.Checked = False
        chkMAP2.Checked = False
        chkMAP3.Checked = False
        chkSISTOLIC1.Checked = False
        chkSISTOLIC2.Checked = False
        chkSISTOLIC3.Checked = False
        chkDIASTOLIC1.Checked = False
        chkDIASTOLIC2.Checked = False
        chkDIASTOLIC3.Checked = False
        chkEKG1.Checked = False
        chkEKG2.Checked = False
        chkEKG3.Checked = False
        chkDISABILITASSPONTANEOUS.Checked = False
        chkDISABILITASORIENTED.Checked = False
        chkDISABILITASOBEYSCOMMANDS.Checked = False
        chkDISABILITASTOVOICE.Checked = False
        chkDISABILITASCONFUSED.Checked = False
        chkDISABILITASLOCALIZESPAIN.Checked = False
        chkDISABILITASTOPAIN.Checked = False
        chkDISABILITASINAPPROPRIATE.Checked = False
        chkDISABILITASWITHDRAWSTOPAIN.Checked = False
        chkDISABILITAS1NONE.Checked = False
        chkDISABILITASINCOMPREHENSIBLE.Checked = False
        chkDISABILITASFLEXIONTOPAIN.Checked = False
        chkDISABILITAS2NONE.Checked = False
        chkDISABILITASEXTENTIONTOPAIN.Checked = False
        chkDISABILITAS3NONE.Checked = False
        txtEYEOPENING.ResetText()
        txtVERBAL.ResetText()
        txtMOTOR.ResetText()
        chkKESADARANKEJANG1.Checked = False
        chkKESADARANKEJANG2.Checked = False
        chkKESADARANKEJANG3.Checked = False
        chkKESADARANKEJANG4.Checked = False
        chkKESADARANKEJANG5.Checked = False
        chkKESADARANKEJANG6.Checked = False
        chkKESADARANKEJANG7.Checked = False
        chkKESADARANKEJANG8.Checked = False
        chkKESADARANKEJANG9.Checked = False
        chkKESADARANKEJANG10.Checked = False
        chkSUASANASKALANYERI1.Checked = False
        chkSUASANASKALANYERI2.Checked = False
        chkSUASANASKALANYERI3.Checked = False
        chkSUASANASKALANYERI4.Checked = False
        chkSUASANAPENDARAHAN1.Checked = False
        chkSUASANAPENDARAHAN2.Checked = False
        chkSUASANAPENDARAHAN3.Checked = False
        chkSUASANAPAPARAN1.Checked = False
        chkSUASANAPAPARAN2.Checked = False
        chkSUASANAPAPARAN3.Checked = False
        chkSUASANASUHU1.Checked = False
        chkSUASANASUHU2.Checked = False
        chkSUASANASUHU3.Checked = False
        chkRUJUKAN1.Checked = False
        chkRUJUKAN2.Checked = False
        chkKEBUTUHAN1.Checked = False
        chkKEBUTUHAN2.Checked = False
        chkKEPUTUSAN1.Checked = False
        chkKEPUTUSAN2.Checked = False
        chkKEPUTUSAN3.Checked = False
        chkKEPUTUSAN4.Checked = False
        chkKESIMPULAN1.Checked = False
        chkKESIMPULAN2.Checked = False
        chkPENGANTAR1.Checked = False
        chkPENGANTAR2.Checked = False
        chkPENGANTAR3.Checked = False
        chkPENGANTAR4.Checked = False
        txtRIWAYATKELUHANUTAMA.ResetText()
        deDatePeriksa.DateTime = Now
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_Triage.GetData(sNoid)

            With ds
                deDatePeriksa.DateTime = .PUKULPERIKSA
                chkJALANNAFAS1.Checked = .JALANNAFAS1
                chkJALANNAFAS2.Checked = .JALANNAFAS2
                chkJALANNAFAS3.Checked = .JALANNAFAS3
                chkJALANNAFAS4.Checked = .JALANNAFAS4
                chkPERNAFASAN1.Checked = .PERNAFASAN1
                chkPERNAFASAN2.Checked = .PERNAFASAN2
                chkPERNAFASAN3.Checked = .PERNAFASAN3
                chkPERNAFASAN4.Checked = .PERNAFASAN4
                chkSPO2DEWASA1.Checked = .SPO2DEWASA1
                chkSPO2DEWASA2.Checked = .SPO2DEWASA2
                chkSPO2DEWASA3.Checked = .SPO2DEWASA3
                chkSPO2ANAK1.Checked = .SPO2ANAK1
                chkSPO2ANAK2.Checked = .SPO2ANAK2
                chkSPO2ANAK3.Checked = .SPO2ANAK3
                chkRRDEWASA1.Checked = .RRDEWASA1
                chkRRDEWASA2.Checked = .RRDEWASA2
                chkRRDEWASA3.Checked = .RRDEWASA3
                chkRRANAK1BLN1.Checked = .RRANAK1BLN1
                chkRRANAK1BLN2.Checked = .RRANAK1BLN2
                chkRRANAK1BLN3.Checked = .RRANAK1BLN3
                chkRRANAK14BLN1.Checked = .RRANAK14BLN1
                chkRRANAK14BLN2.Checked = .RRANAK14BLN2
                chkRRANAK14BLN3.Checked = .RRANAK14BLN3
                chkRRANAK41.Checked = .RRANAK41
                chkRRANAK42.Checked = .RRANAK42
                chkRRANAK43.Checked = .RRANAK43
                chkSIRKULASI1.Checked = .SIRKULASI1
                chkSIRKULASI2.Checked = .SIRKULASI2
                chkSIRKULASI3.Checked = .SIRKULASI3
                chkSIRKULASI4.Checked = .SIRKULASI4
                chkHRDEWASA1.Checked = .HRDEWASA1
                chkHRDEWASA2.Checked = .HRDEWASA2
                chkHRDEWASA3.Checked = .HRDEWASA3
                chkHRANAK1BLN1.Checked = .HRANAK1BLN1
                chkHRANAK1BLN2.Checked = .HRANAK1BLN2
                chkHRANAK1BLN3.Checked = .HRANAK1BLN3
                chkHRANAK14BLN1.Checked = .HRANAK14BLN1
                chkHRANAK14BLN2.Checked = .HRANAK14BLN2
                chkHRANAK14BLN3.Checked = .HRANAK14BLN3
                chkHRANAK4BLN1.Checked = .HRANAK4BLN1
                chkHRANAK4BLN2.Checked = .HRANAK4BLN2
                chkHRANAK4BLN3.Checked = .HRANAK4BLN3
                chkMAP1.Checked = .MAP1
                chkMAP2.Checked = .MAP2
                chkMAP3.Checked = .MAP3
                chkSISTOLIC1.Checked = .SISTOLIC1
                chkSISTOLIC2.Checked = .SISTOLIC2
                chkSISTOLIC3.Checked = .SISTOLIC3
                chkDIASTOLIC1.Checked = .DIASTOLIC1
                chkDIASTOLIC2.Checked = .DIASTOLIC2
                chkDIASTOLIC3.Checked = .DIASTOLIC3
                chkEKG1.Checked = .EKG1
                chkEKG2.Checked = .EKG2
                chkEKG3.Checked = .EKG3
                chkDISABILITASSPONTANEOUS.Checked = .DISABILITASSPONTANEOUS
                chkDISABILITASORIENTED.Checked = .DISABILITASORIENTED
                chkDISABILITASOBEYSCOMMANDS.Checked = .DISABILITASOBEYSCOMMANDS
                chkDISABILITASTOVOICE.Checked = .DISABILITASTOVOICE
                chkDISABILITASCONFUSED.Checked = .DISABILITASCONFUSED
                chkDISABILITASLOCALIZESPAIN.Checked = .DISABILITASLOCALIZESPAIN
                chkDISABILITASTOPAIN.Checked = .DISABILITASTOPAIN
                chkDISABILITASINAPPROPRIATE.Checked = .DISABILITASINAPPROPRIATE
                chkDISABILITASWITHDRAWSTOPAIN.Checked = .DISABILITASWITHDRAWSTOPAIN
                chkDISABILITAS1NONE.Checked = .DISABILITAS1NONE
                chkDISABILITASINCOMPREHENSIBLE.Checked = .DISABILITASINCOMPREHENSIBLE
                chkDISABILITASFLEXIONTOPAIN.Checked = .DISABILITASFLEXIONTOPAIN
                chkDISABILITAS2NONE.Checked = .DISABILITAS2NONE
                chkDISABILITASEXTENTIONTOPAIN.Checked = .DISABILITASEXTENTIONTOPAIN
                chkDISABILITAS3NONE.Checked = .DISABILITAS3NONE
                txtEYEOPENING.Text = .EYEOPENING
                txtVERBAL.Text = .VERBAL
                txtMOTOR.Text = .MOTOR
                chkKESADARANKEJANG1.Checked = .KESADARANKEJANG1
                chkKESADARANKEJANG2.Checked = .KESADARANKEJANG2
                chkKESADARANKEJANG3.Checked = .KESADARANKEJANG3
                chkKESADARANKEJANG4.Checked = .KESADARANKEJANG4
                chkKESADARANKEJANG5.Checked = .KESADARANKEJANG5
                chkKESADARANKEJANG6.Checked = .KESADARANKEJANG6
                chkKESADARANKEJANG7.Checked = .KESADARANKEJANG7
                chkKESADARANKEJANG8.Checked = .KESADARANKEJANG8
                chkKESADARANKEJANG9.Checked = .KESADARANKEJANG9
                chkKESADARANKEJANG10.Checked = .KESADARANKEJANG10
                chkSUASANASKALANYERI1.Checked = .SUASANASKALANYERI1
                chkSUASANASKALANYERI2.Checked = .SUASANASKALANYERI2
                chkSUASANASKALANYERI3.Checked = .SUASANASKALANYERI3
                chkSUASANASKALANYERI4.Checked = .SUASANASKALANYERI4
                chkSUASANAPENDARAHAN1.Checked = .SUASANAPENDARAHAN1
                chkSUASANAPENDARAHAN2.Checked = .SUASANAPENDARAHAN2
                chkSUASANAPENDARAHAN3.Checked = .SUASANAPENDARAHAN3
                chkSUASANAPAPARAN1.Checked = .SUASANAPAPARAN1
                chkSUASANAPAPARAN2.Checked = .SUASANAPAPARAN2
                chkSUASANAPAPARAN3.Checked = .SUASANAPAPARAN3
                chkSUASANASUHU1.Checked = .SUASANASUHU1
                chkSUASANASUHU2.Checked = .SUASANASUHU2
                chkSUASANASUHU3.Checked = .SUASANASUHU3
                chkRUJUKAN1.Checked = .RUJUKAN1
                chkRUJUKAN2.Checked = .RUJUKAN2
                chkKEBUTUHAN1.Checked = .KEBUTUHAN1
                chkKEBUTUHAN2.Checked = .KEBUTUHAN2
                chkKEPUTUSAN1.Checked = .KEPUTUSAN1
                chkKEPUTUSAN2.Checked = .KEPUTUSAN2
                chkKEPUTUSAN3.Checked = .KEPUTUSAN3
                chkKEPUTUSAN4.Checked = .KEPUTUSAN4
                chkKESIMPULAN1.Checked = .KESIMPULAN1
                chkKESIMPULAN2.Checked = .KESIMPULAN2
                chkPENGANTAR1.Checked = .PENGANTAR1
                chkPENGANTAR2.Checked = .PENGANTAR2
                chkPENGANTAR3.Checked = .PENGANTAR3
                chkPENGANTAR4.Checked = .PENGANTAR4
                chkJALANNAFAS1.Checked = .JALANNAFAS1
                chkJALANNAFAS2.Checked = .JALANNAFAS2
                chkJALANNAFAS3.Checked = .JALANNAFAS3
                chkJALANNAFAS4.Checked = .JALANNAFAS4
                chkPERNAFASAN1.Checked = .PERNAFASAN1
                chkPERNAFASAN2.Checked = .PERNAFASAN2
                chkPERNAFASAN3.Checked = .PERNAFASAN3
                chkPERNAFASAN4.Checked = .PERNAFASAN4
                chkSPO2DEWASA1.Checked = .SPO2DEWASA1
                chkSPO2DEWASA2.Checked = .SPO2DEWASA2
                chkSPO2DEWASA3.Checked = .SPO2DEWASA3
                chkSPO2ANAK1.Checked = .SPO2ANAK1
                chkSPO2ANAK2.Checked = .SPO2ANAK2
                chkSPO2ANAK3.Checked = .SPO2ANAK3
                chkRRDEWASA1.Checked = .RRDEWASA1
                chkRRDEWASA2.Checked = .RRDEWASA2
                chkRRDEWASA3.Checked = .RRDEWASA3
                chkRRANAK1BLN1.Checked = .RRANAK1BLN1
                chkRRANAK1BLN2.Checked = .RRANAK1BLN2
                chkRRANAK1BLN3.Checked = .RRANAK1BLN3
                chkRRANAK14BLN1.Checked = .RRANAK14BLN1
                chkRRANAK14BLN2.Checked = .RRANAK14BLN2
                chkRRANAK14BLN3.Checked = .RRANAK14BLN3
                chkRRANAK41.Checked = .RRANAK41
                chkRRANAK42.Checked = .RRANAK42
                chkRRANAK43.Checked = .RRANAK43
                chkSIRKULASI1.Checked = .SIRKULASI1
                chkSIRKULASI2.Checked = .SIRKULASI2
                chkSIRKULASI3.Checked = .SIRKULASI3
                chkSIRKULASI4.Checked = .SIRKULASI4
                chkHRDEWASA1.Checked = .HRDEWASA1
                chkHRDEWASA2.Checked = .HRDEWASA2
                chkHRDEWASA3.Checked = .HRDEWASA3
                chkHRANAK1BLN1.Checked = .HRANAK1BLN1
                chkHRANAK1BLN2.Checked = .HRANAK1BLN2
                chkHRANAK1BLN3.Checked = .HRANAK1BLN3
                chkHRANAK14BLN1.Checked = .HRANAK14BLN1
                chkHRANAK14BLN2.Checked = .HRANAK14BLN2
                chkHRANAK14BLN3.Checked = .HRANAK14BLN3
                chkHRANAK4BLN1.Checked = .HRANAK4BLN1
                chkHRANAK4BLN2.Checked = .HRANAK4BLN2
                chkHRANAK4BLN3.Checked = .HRANAK4BLN3
                chkMAP1.Checked = .MAP1
                chkMAP2.Checked = .MAP2
                chkMAP3.Checked = .MAP3
                chkSISTOLIC1.Checked = .SISTOLIC1
                chkSISTOLIC2.Checked = .SISTOLIC2
                chkSISTOLIC3.Checked = .SISTOLIC3
                chkDIASTOLIC1.Checked = .DIASTOLIC1
                chkDIASTOLIC2.Checked = .DIASTOLIC2
                chkDIASTOLIC3.Checked = .DIASTOLIC3
                chkEKG1.Checked = .EKG1
                chkEKG2.Checked = .EKG2
                chkEKG3.Checked = .EKG3
                chkDISABILITASSPONTANEOUS.Checked = .DISABILITASSPONTANEOUS
                chkDISABILITASORIENTED.Checked = .DISABILITASORIENTED
                chkDISABILITASOBEYSCOMMANDS.Checked = .DISABILITASOBEYSCOMMANDS
                chkDISABILITASTOVOICE.Checked = .DISABILITASTOVOICE
                chkDISABILITASCONFUSED.Checked = .DISABILITASCONFUSED
                chkDISABILITASLOCALIZESPAIN.Checked = .DISABILITASLOCALIZESPAIN
                chkDISABILITASTOPAIN.Checked = .DISABILITASTOPAIN
                chkDISABILITASINAPPROPRIATE.Checked = .DISABILITASINAPPROPRIATE
                chkDISABILITASWITHDRAWSTOPAIN.Checked = .DISABILITASWITHDRAWSTOPAIN
                chkDISABILITAS1NONE.Checked = .DISABILITAS1NONE
                chkDISABILITASINCOMPREHENSIBLE.Checked = .DISABILITASINCOMPREHENSIBLE
                chkDISABILITASFLEXIONTOPAIN.Checked = .DISABILITASFLEXIONTOPAIN
                chkDISABILITAS2NONE.Checked = .DISABILITAS2NONE
                chkDISABILITASEXTENTIONTOPAIN.Checked = .DISABILITASEXTENTIONTOPAIN
                chkDISABILITAS3NONE.Checked = .DISABILITAS3NONE
                txtEYEOPENING.Text = .EYEOPENING
                txtVERBAL.Text = .VERBAL
                txtMOTOR.Text = .MOTOR
                chkKESADARANKEJANG1.Checked = .KESADARANKEJANG1
                chkKESADARANKEJANG2.Checked = .KESADARANKEJANG2
                chkKESADARANKEJANG3.Checked = .KESADARANKEJANG3
                chkKESADARANKEJANG4.Checked = .KESADARANKEJANG4
                chkKESADARANKEJANG5.Checked = .KESADARANKEJANG5
                chkKESADARANKEJANG6.Checked = .KESADARANKEJANG6
                chkKESADARANKEJANG7.Checked = .KESADARANKEJANG7
                chkKESADARANKEJANG8.Checked = .KESADARANKEJANG8
                chkKESADARANKEJANG9.Checked = .KESADARANKEJANG9
                chkKESADARANKEJANG10.Checked = .KESADARANKEJANG10
                chkSUASANASKALANYERI1.Checked = .SUASANASKALANYERI1
                chkSUASANASKALANYERI2.Checked = .SUASANASKALANYERI2
                chkSUASANASKALANYERI3.Checked = .SUASANASKALANYERI3
                chkSUASANASKALANYERI4.Checked = .SUASANASKALANYERI4
                chkSUASANAPENDARAHAN1.Checked = .SUASANAPENDARAHAN1
                chkSUASANAPENDARAHAN2.Checked = .SUASANAPENDARAHAN2
                chkSUASANAPENDARAHAN3.Checked = .SUASANAPENDARAHAN3
                chkSUASANAPAPARAN1.Checked = .SUASANAPAPARAN1
                chkSUASANAPAPARAN2.Checked = .SUASANAPAPARAN2
                chkSUASANAPAPARAN3.Checked = .SUASANAPAPARAN3
                chkSUASANASUHU1.Checked = .SUASANASUHU1
                chkSUASANASUHU2.Checked = .SUASANASUHU2
                chkSUASANASUHU3.Checked = .SUASANASUHU3
                chkRUJUKAN1.Checked = .RUJUKAN1
                chkRUJUKAN2.Checked = .RUJUKAN2
                chkKEBUTUHAN1.Checked = .KEBUTUHAN1
                chkKEBUTUHAN2.Checked = .KEBUTUHAN2
                chkKEPUTUSAN1.Checked = .KEPUTUSAN1
                chkKEPUTUSAN2.Checked = .KEPUTUSAN2
                chkKEPUTUSAN3.Checked = .KEPUTUSAN3
                chkKEPUTUSAN4.Checked = .KEPUTUSAN4
                chkKESIMPULAN1.Checked = .KESIMPULAN1
                chkKESIMPULAN2.Checked = .KESIMPULAN2
                chkPENGANTAR1.Checked = .PENGANTAR1
                chkPENGANTAR2.Checked = .PENGANTAR2
                chkPENGANTAR3.Checked = .PENGANTAR3
                chkPENGANTAR4.Checked = .PENGANTAR4
                grdPetugasTriage.EditValue = .DOKTER_KODE
                txtRIWAYATKELUHANUTAMA.Text = .RIWAYATKELUHANUTAMA

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
            If sNoid = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                'ded.Focus()
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
            Dim ds = oS_DIGITAL_Triage.GetStructureHeader
            With ds
                .KDKUNJUNGAN = sNoid
                Try
                    .DATECREATED = oS_DIGITAL_Triage.GetData(sNoid).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = Now
                .KDIDENTITAS = sKodeIdentitas
                .PUKULPERIKSA = deDatePeriksa.DateTime
                .JALANNAFAS1 = chkJALANNAFAS1.Checked
                .JALANNAFAS2 = chkJALANNAFAS2.Checked
                .JALANNAFAS3 = chkJALANNAFAS3.Checked
                .JALANNAFAS4 = chkJALANNAFAS4.Checked
                .PERNAFASAN1 = chkPERNAFASAN1.Checked
                .PERNAFASAN2 = chkPERNAFASAN2.Checked
                .PERNAFASAN3 = chkPERNAFASAN3.Checked
                .PERNAFASAN4 = chkPERNAFASAN4.Checked
                .SPO2DEWASA1 = chkSPO2DEWASA1.Checked
                .SPO2DEWASA2 = chkSPO2DEWASA2.Checked
                .SPO2DEWASA3 = chkSPO2DEWASA3.Checked
                .SPO2ANAK1 = chkSPO2ANAK1.Checked
                .SPO2ANAK2 = chkSPO2ANAK2.Checked
                .SPO2ANAK3 = chkSPO2ANAK3.Checked
                .RRDEWASA1 = chkRRDEWASA1.Checked
                .RRDEWASA2 = chkRRDEWASA2.Checked
                .RRDEWASA3 = chkRRDEWASA3.Checked
                .RRANAK1BLN1 = chkRRANAK1BLN1.Checked
                .RRANAK1BLN2 = chkRRANAK1BLN2.Checked
                .RRANAK1BLN3 = chkRRANAK1BLN3.Checked
                .RRANAK14BLN1 = chkRRANAK14BLN1.Checked
                .RRANAK14BLN2 = chkRRANAK14BLN2.Checked
                .RRANAK14BLN3 = chkRRANAK14BLN3.Checked
                .RRANAK41 = chkRRANAK41.Checked
                .RRANAK42 = chkRRANAK42.Checked
                .RRANAK43 = chkRRANAK43.Checked
                .SIRKULASI1 = chkSIRKULASI1.Checked
                .SIRKULASI2 = chkSIRKULASI2.Checked
                .SIRKULASI3 = chkSIRKULASI3.Checked
                .SIRKULASI4 = chkSIRKULASI4.Checked
                .HRDEWASA1 = chkHRDEWASA1.Checked
                .HRDEWASA2 = chkHRDEWASA2.Checked
                .HRDEWASA3 = chkHRDEWASA3.Checked
                .HRANAK1BLN1 = chkHRANAK1BLN1.Checked
                .HRANAK1BLN2 = chkHRANAK1BLN2.Checked
                .HRANAK1BLN3 = chkHRANAK1BLN3.Checked
                .HRANAK14BLN1 = chkHRANAK14BLN1.Checked
                .HRANAK14BLN2 = chkHRANAK14BLN2.Checked
                .HRANAK14BLN3 = chkHRANAK14BLN3.Checked
                .HRANAK4BLN1 = chkHRANAK4BLN1.Checked
                .HRANAK4BLN2 = chkHRANAK4BLN2.Checked
                .HRANAK4BLN3 = chkHRANAK4BLN3.Checked
                .MAP1 = chkMAP1.Checked
                .MAP2 = chkMAP2.Checked
                .MAP3 = chkMAP3.Checked
                .SISTOLIC1 = chkSISTOLIC1.Checked
                .SISTOLIC2 = chkSISTOLIC2.Checked
                .SISTOLIC3 = chkSISTOLIC3.Checked
                .DIASTOLIC1 = chkDIASTOLIC1.Checked
                .DIASTOLIC2 = chkDIASTOLIC2.Checked
                .DIASTOLIC3 = chkDIASTOLIC3.Checked
                .EKG1 = chkEKG1.Checked
                .EKG2 = chkEKG2.Checked
                .EKG3 = chkEKG3.Checked
                .DISABILITASSPONTANEOUS = chkDISABILITASSPONTANEOUS.Checked
                .DISABILITASORIENTED = chkDISABILITASORIENTED.Checked
                .DISABILITASOBEYSCOMMANDS = chkDISABILITASOBEYSCOMMANDS.Checked
                .DISABILITASTOVOICE = chkDISABILITASTOVOICE.Checked
                .DISABILITASCONFUSED = chkDISABILITASCONFUSED.Checked
                .DISABILITASLOCALIZESPAIN = chkDISABILITASLOCALIZESPAIN.Checked
                .DISABILITASTOPAIN = chkDISABILITASTOPAIN.Checked
                .DISABILITASINAPPROPRIATE = chkDISABILITASINAPPROPRIATE.Checked
                .DISABILITASWITHDRAWSTOPAIN = chkDISABILITASWITHDRAWSTOPAIN.Checked
                .DISABILITAS1NONE = chkDISABILITAS1NONE.Checked
                .DISABILITASINCOMPREHENSIBLE = chkDISABILITASINCOMPREHENSIBLE.Checked
                .DISABILITASFLEXIONTOPAIN = chkDISABILITASFLEXIONTOPAIN.Checked
                .DISABILITAS2NONE = chkDISABILITAS2NONE.Checked
                .DISABILITASEXTENTIONTOPAIN = chkDISABILITASEXTENTIONTOPAIN.Checked
                .DISABILITAS3NONE = chkDISABILITAS3NONE.Checked
                .EYEOPENING = txtEYEOPENING.Text
                .VERBAL = txtVERBAL.Text
                .MOTOR = txtMOTOR.Text
                .KESADARANKEJANG1 = chkKESADARANKEJANG1.Checked
                .KESADARANKEJANG2 = chkKESADARANKEJANG2.Checked
                .KESADARANKEJANG3 = chkKESADARANKEJANG3.Checked
                .KESADARANKEJANG4 = chkKESADARANKEJANG4.Checked
                .KESADARANKEJANG5 = chkKESADARANKEJANG5.Checked
                .KESADARANKEJANG6 = chkKESADARANKEJANG6.Checked
                .KESADARANKEJANG7 = chkKESADARANKEJANG7.Checked
                .KESADARANKEJANG8 = chkKESADARANKEJANG8.Checked
                .KESADARANKEJANG9 = chkKESADARANKEJANG9.Checked
                .KESADARANKEJANG10 = chkKESADARANKEJANG10.Checked
                .SUASANASKALANYERI1 = chkSUASANASKALANYERI1.Checked
                .SUASANASKALANYERI2 = chkSUASANASKALANYERI2.Checked
                .SUASANASKALANYERI3 = chkSUASANASKALANYERI3.Checked
                .SUASANASKALANYERI4 = chkSUASANASKALANYERI4.Checked
                .SUASANAPENDARAHAN1 = chkSUASANAPENDARAHAN1.Checked
                .SUASANAPENDARAHAN2 = chkSUASANAPENDARAHAN2.Checked
                .SUASANAPENDARAHAN3 = chkSUASANAPENDARAHAN3.Checked
                .SUASANAPAPARAN1 = chkSUASANAPAPARAN1.Checked
                .SUASANAPAPARAN2 = chkSUASANAPAPARAN2.Checked
                .SUASANAPAPARAN3 = chkSUASANAPAPARAN3.Checked
                .SUASANASUHU1 = chkSUASANASUHU1.Checked
                .SUASANASUHU2 = chkSUASANASUHU2.Checked
                .SUASANASUHU3 = chkSUASANASUHU3.Checked
                .RUJUKAN1 = chkRUJUKAN1.Checked
                .RUJUKAN2 = chkRUJUKAN2.Checked
                .KEBUTUHAN1 = chkKEBUTUHAN1.Checked
                .KEBUTUHAN2 = chkKEBUTUHAN2.Checked
                .KEPUTUSAN1 = chkKEPUTUSAN1.Checked
                .KEPUTUSAN2 = chkKEPUTUSAN2.Checked
                .KEPUTUSAN3 = chkKEPUTUSAN3.Checked
                .KEPUTUSAN4 = chkKEPUTUSAN4.Checked
                .KESIMPULAN1 = chkKESIMPULAN1.Checked
                .KESIMPULAN2 = chkKESIMPULAN2.Checked
                .PENGANTAR1 = chkPENGANTAR1.Checked
                .PENGANTAR2 = chkPENGANTAR2.Checked
                .PENGANTAR3 = chkPENGANTAR3.Checked
                .PENGANTAR4 = chkPENGANTAR4.Checked

                .DOKTER_KODE = grdPetugasTriage.EditValue
                .DOKTER_NAMEDISPLAY = grdPetugasTriage.Text
                .RIWAYATKELUHANUTAMA = txtRIWAYATKELUHANUTAMA.Text

                Try
                    .CETAK = oS_DIGITAL_Triage.GetData(sNoid).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                .KDUSER = sUserID
                .KDUSER_SIGNATURE = ""
            End With
            'oFormMode = 1
            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_Triage.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_Triage.UpdateData(ds)
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
            'Case Keys.F12
            '    btnClose_Click()
            'Case Keys.F3
            '    If btnSaveClose.Enabled = True Then
            '        btnSaveClose_Click()
            '    End If
            Case Keys.PageUp
                fn_ScrollPage(True)
            Case Keys.PageDown
                fn_ScrollPage(False)
        End Select
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & sNoid & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            oFormMode = FORM_MODE.FORM_MODE_EDIT
            MsgBox("Save " & sNoid & " success!", MsgBoxStyle.Information, Me.Text)
            If sKeluar = True Then
                Me.Close()
            End If
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

    Private Sub frmEMedrekTriage_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
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
    Private Sub fn_Doctor2()
        Dim oUSER As New Setting.clsUser

        Try
            grdPetugasTriage.Properties.DataSource = oUSER.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdPetugasTriage.Properties.ValueMember = "KDUSER"
            grdPetugasTriage.Properties.DisplayMember = "KDUSER"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class