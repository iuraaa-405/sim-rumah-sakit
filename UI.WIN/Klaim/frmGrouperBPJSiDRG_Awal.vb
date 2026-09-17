Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Data.SqlClient
Imports Newtonsoft.Json.Linq
Imports DevExpress.XtraSplashScreen
Imports System.Linq
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class frmGrouperBPJSiDRG_Awal
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As Integer
    Private isLoad = False
    Private oStatusGrouper As New Grouper.clsStatusGrouper
    Private oStatusGrouperHasil As New Grouper.clsStatusGrouperHasil
    Private oRIdentitasGrouper As New Grouper.clsR_Identitas_Grouper
    Private oRIdentitasGrouperData As New Grouper.clsR_Identitas_Grouper_Data
    Private oGetGrouper As New Brigging.clsSetKoneksi
    Private sRegisterAwal As String
    Private sTanggalLahir As String
    Private sCategory As Integer
#End Region
#Region "Function"
    Public Sub LoadMe(FormMode As Integer, ByVal NoId As Integer)
        listdiagnosakodeidRG.Clear()
        listprosedurkodeidRG.Clear()
        oFormMode = FormMode
        sNoId = NoId
    End Sub
    Private Sub Form_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        PdfViewerCPPTRawatJalan.CloseDocument()
        chkINA.Checked = True
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        PdfViewerCPPTRawatJalan.CloseDocument()
        sCode = txtCODE.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadJenisTarif()
        fn_LoadCOB()
        fn_LoadDPJP()
        fn_LoadCARAKELUAR()
        fn_LoadCARAMASUK()
        fn_LoadDiagnosa()
        fn_LoadProsedur()
        fn_LoadDataSpecialCMG()

        lCariDiagnosaiDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lCariProseduriDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

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

        fn_StatusView()
        fn_LoadApgar(chkAPGAR.Checked)
        fn_LoadPersalinan(chkPERSALINAN.Checked)

        Try
            txtTopUpCostWeight.Text = "-"

            Dim ds = oStatusGrouperHasil.GetData(sNoId)
            If ds IsNot Nothing Then
                txtTopUpCostWeight.Text = ds.TopUpCostWeight
                txtTopuCode.Text = ds.TopUpCostWeight_codeA
                HasilIDRG(ds.memo, ds.TopUpCostWeight_codeA)

                If txtMDC.Text = "Ungroupable or Unrelated" Then
                    'lGROUPER_HASILiDRG_FINALiDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                    'btnFinaliDRG.Visible = False

                    txtMDC.ForeColor = Color.Red
                    txtDRG.ForeColor = Color.Red
                Else
                    'lGROUPER_HASILiDRG_FINALiDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                    'btnFinaliDRG.Visible = True

                    txtMDC.ForeColor = Color.Black
                    txtDRG.ForeColor = Color.Black
                End If
            End If
        Catch oErr As Exception
            MsgBox("Load Hasil Idrg " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_ViewMode(Status)
        btnSaveNew.Enabled = Not Status
        btnSaveClose.Enabled = Not Status

        grdJenisTarif.Properties.ReadOnly = Status
        rbCategory.Properties.ReadOnly = Status
        grdCARAMASUK.Properties.ReadOnly = Status
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
        txtADLScore_SubAcute.Properties.ReadOnly = Status
        txtADLScore_Chronic.Properties.ReadOnly = Status
        rbKELASHAK.Properties.ReadOnly = Status
        txtUmur.Properties.ReadOnly = Status
        txtBeratBadan.Properties.ReadOnly = Status
        grdCaraKeluar.Properties.ReadOnly = Status
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
        'txtICD_X.Properties.ReadOnly = Status
        'txtICCD_IX.Properties.ReadOnly = Status
        txtRAWATINTENSIF_HARI.Properties.ReadOnly = Status
        txtVENTILATOR.Properties.ReadOnly = Status
        txtSISTOLE.Properties.ReadOnly = Status
        txtDIASTOLE.Properties.ReadOnly = Status
        chkPasienTB.Properties.ReadOnly = Status
        txtNOMORSITB.Properties.ReadOnly = Status
        rbDIALIZERSINGGLEUSE.Properties.ReadOnly = Status
        cboVENTILATOR_use_ind.Properties.ReadOnly = Status
        deVENTILATOR_start_dttm.Properties.ReadOnly = Status
        deVENTILATOR_stop_dttm.Properties.ReadOnly = Status
        txtKANTONGDARAH.Properties.ReadOnly = Status
        cboALTEPLASE_IND.Properties.ReadOnly = Status
        chkAPGAR.Properties.ReadOnly = Status
        cboMenit1_appearance.Properties.ReadOnly = Status
        cboMenit1_Pulse.Properties.ReadOnly = Status
        cboMenit1_Grimace.Properties.ReadOnly = Status
        cboMenit1_Activity.Properties.ReadOnly = Status
        cboMenit1_Respiration.Properties.ReadOnly = Status
        cboMenit5_appearance.Properties.ReadOnly = Status
        cboMenit5_Pulse.Properties.ReadOnly = Status
        cboMenit5_Grimace.Properties.ReadOnly = Status
        cboMenit5_Activity.Properties.ReadOnly = Status
        cboMenit5_Respiration.Properties.ReadOnly = Status
        chkPERSALINAN.Properties.ReadOnly = Status
        txtPERSALINAN_usia_kehamilan.Properties.ReadOnly = Status
        txtPERSALINAN_gravida.Properties.ReadOnly = Status
        txtPERSALINAN_partus.Properties.ReadOnly = Status
        txtPERSALINAN_abortus.Properties.ReadOnly = Status
        cboPERSALINAN_onset_kontraksi.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_StatusView()
        Dim dsStatusGrouper = oStatusGrouper.GetData(sNoId)

        If dsStatusGrouper IsNot Nothing Then
            If dsStatusGrouper.STATUS = "fn_00NEWCLAIM" Then
                lCariDiagnosaiDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lCariProseduriDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                lGROUPER_TRANSAKSI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lGROUPER_TARIF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lGROUPER_iDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lGROUPER_HASILiDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                'lGROUPER_HASILiDRG_FINALiDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                'lGROUPER_HASILiDRG_EDITiDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                'lGROUPER_iDRG_GROUPING.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                btnFinaliDRG.Visible = False
                btnGroupingiDRG.Visible = True

                lKlaimBaruCovid.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lGROUPER_HAPUSKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                lCETAKKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lKIRIMONLINEKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lFINALKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lEDITULANGKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                lHASILKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            ElseIf dsStatusGrouper.STATUS = "fn_06GROUPINGIDRG" Or dsStatusGrouper.STATUS = "fn_08REEDIT" Then
                If isLoad = False Then
                    fn_03IDRGDIAGNOSAGET()
                    fn_05IDRGPROCEDUREGET()

                    lGROUPER_TRANSAKSI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    lGROUPER_TARIF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    lGROUPER_iDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    lGROUPER_HASILiDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                    lKlaimBaruCovid.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    lGROUPER_HAPUSKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    lHASILKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                End If

                lGROUPER_HASILGROUPER.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                lGROUPER_INACBG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lGROUPER_HASILiDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                lCariDiagnosaiDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lCariProseduriDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                'lGROUPER_iDRG_GROUPING.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                'lGROUPER_HASILiDRG_EDITiDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                btnGroupingiDRG.Visible = True
                btnFinaliDRG.Text = "Final iDRG"

                'fn_LoadHasilGroupingiDRG()

                If txtMDC.Text = "Ungroupable or Unrelated" Then
                    'lGROUPER_HASILiDRG_FINALiDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                    btnFinaliDRG.Visible = False

                    txtMDC.ForeColor = Color.Red
                    txtDRG.ForeColor = Color.Red
                Else
                    'lGROUPER_HASILiDRG_FINALiDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                    btnFinaliDRG.Visible = True

                    txtMDC.ForeColor = Color.Black
                    txtDRG.ForeColor = Color.Black
                End If

                lCETAKKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lKIRIMONLINEKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lFINALKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lEDITULANGKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            ElseIf dsStatusGrouper.STATUS = "fn_07FINALIDRG" Then
                If isLoad = False Then
                    fn_03IDRGDIAGNOSAGET()
                    fn_05IDRGPROCEDUREGET()
                    'fn_10INACBGDIAGNOSAGET()
                    'fn_13INACBGPROCEDUREGET()

                    lGROUPER_TRANSAKSI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    lGROUPER_TARIF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    lGROUPER_iDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    lGROUPER_HASILiDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    lKlaimBaruCovid.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    lGROUPER_HAPUSKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                    'fn_LoadHasilGroupingiDRG()

                    If txtMDC.Text = "Ungroupable or Unrelated" Then
                        'lGROUPER_HASILiDRG_EDITiDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        'lGROUPER_iDRG_GROUPING.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                        txtMDC.ForeColor = Color.Red
                        txtDRG.ForeColor = Color.Red
                    Else
                        'lGROUPER_iDRG_GROUPING.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        'lGROUPER_HASILiDRG_EDITiDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                        txtMDC.ForeColor = Color.Black
                        txtDRG.ForeColor = Color.Black
                    End If

                    lHASILKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                End If

                lGROUPER_HASILGROUPER.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lCariDiagnosaiDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lCariProseduriDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                btnFinaliDRG.Visible = True
                btnGroupingiDRG.Visible = False
                btnFinaliDRG.Text = "Edit Ulang iDRG"

                lGROUPER_INACBG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lGROUPER_INACBG_IMPORT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lGROUPER_INACBG_GROUPING.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                'lGROUPER_HASILiDRG_FINALiDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                lCETAKKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lKIRIMONLINEKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lFINALKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lEDITULANGKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

            ElseIf dsStatusGrouper.STATUS = "fn_14GROUPINGINACBGSTAGE1" Or dsStatusGrouper.STATUS = "fn_14GROUPINGINACBGSTAGE2" Or dsStatusGrouper.STATUS = "fn_17REEDITINACBG" Then
                If isLoad = False Then
                    fn_03IDRGDIAGNOSAGET()
                    fn_05IDRGPROCEDUREGET()
                    'fn_10INACBGDIAGNOSAGET()
                    'fn_13INACBGPROCEDUREGET()

                    lGROUPER_TRANSAKSI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    lGROUPER_TARIF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    lGROUPER_iDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    lGROUPER_HASILiDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    lKlaimBaruCovid.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    lGROUPER_HAPUSKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                    'fn_LoadHasilGroupingiDRG()

                    If txtMDC.Text = "Ungroupable or Unrelated" Then
                        'lGROUPER_HASILiDRG_EDITiDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        'lGROUPER_iDRG_GROUPING.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                        txtMDC.ForeColor = Color.Red
                        txtDRG.ForeColor = Color.Red
                    Else
                        'lGROUPER_iDRG_GROUPING.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        'lGROUPER_HASILiDRG_EDITiDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                        txtMDC.ForeColor = Color.Black
                        txtDRG.ForeColor = Color.Black
                    End If

                    lHASILKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                End If

                lGROUPER_HAPUSKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lGROUPER_HASILGROUPER.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lCariDiagnosaiDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lCariProseduriDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                btnFinaliDRG.Visible = True
                btnGroupingiDRG.Visible = False
                btnFinaliDRG.Text = "Edit Ulang iDRG"

                lGROUPER_INACBG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lGROUPER_INACBG_IMPORT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lGROUPER_INACBG_GROUPING.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                'lGROUPER_HASILiDRG_FINALiDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                If CDec(txtTarifCBG.Text) = 0 Then
                    lFINALKLAIMINACBG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    lEDITULANGKLAIMINACBG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Else
                    lFINALKLAIMINACBG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    lEDITULANGKLAIMINACBG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                End If

                lCETAKKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lKIRIMONLINEKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lFINALKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lEDITULANGKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            ElseIf dsStatusGrouper.STATUS = "fn_16FINALINACBG" Or dsStatusGrouper.STATUS = "fn_19CLAIMREEDIT" Then
                If isLoad = False Then
                    fn_03IDRGDIAGNOSAGET()
                    fn_05IDRGPROCEDUREGET()
                    'fn_10INACBGDIAGNOSAGET()
                    'fn_13INACBGPROCEDUREGET()

                    lGROUPER_TRANSAKSI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    lGROUPER_TARIF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    lGROUPER_iDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    lGROUPER_HASILiDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    lKlaimBaruCovid.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    lGROUPER_HAPUSKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                    'fn_LoadHasilGroupingiDRG()

                    If txtMDC.Text = "Ungroupable or Unrelated" Then
                        'lGROUPER_HASILiDRG_EDITiDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        'lGROUPER_iDRG_GROUPING.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                        txtMDC.ForeColor = Color.Red
                        txtDRG.ForeColor = Color.Red
                    Else
                        'lGROUPER_iDRG_GROUPING.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        'lGROUPER_HASILiDRG_EDITiDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                        txtMDC.ForeColor = Color.Black
                        txtDRG.ForeColor = Color.Black
                    End If
                End If


                lGROUPER_HASILGROUPER.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lCariDiagnosaiDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lCariProseduriDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lGROUPER_HAPUSKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                btnFinaliDRG.Visible = True
                btnGroupingiDRG.Visible = False
                btnFinaliDRG.Text = "Edit Ulang iDRG"

                lGROUPER_INACBG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lGROUPER_INACBG_IMPORT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lGROUPER_INACBG_GROUPING.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                'lGROUPER_HASILiDRG_FINALiDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                If CDec(txtTarifCBG.Text) = 0 Then
                    lFINALKLAIMINACBG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    lEDITULANGKLAIMINACBG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    lFINALKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    lEDITULANGKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Else
                    lFINALKLAIMINACBG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    lEDITULANGKLAIMINACBG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    lFINALKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    lEDITULANGKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                End If

                lCETAKKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lKIRIMONLINEKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lHASILKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            ElseIf dsStatusGrouper.STATUS = "fn_18CLAIMFINAL" Or dsStatusGrouper.STATUS = "fn_20CLAIMSEND" Then
                If isLoad = False Then
                    fn_03IDRGDIAGNOSAGET()
                    fn_05IDRGPROCEDUREGET()
                    'fn_10INACBGDIAGNOSAGET()
                    'fn_13INACBGPROCEDUREGET()

                    lGROUPER_TRANSAKSI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    lGROUPER_TARIF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    lGROUPER_iDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    lGROUPER_HASILiDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    lKlaimBaruCovid.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    lGROUPER_HAPUSKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                    'fn_LoadHasilGroupingiDRG()

                    If txtMDC.Text = "Ungroupable or Unrelated" Then
                        'lGROUPER_HASILiDRG_EDITiDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        'lGROUPER_iDRG_GROUPING.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                        txtMDC.ForeColor = Color.Red
                        txtDRG.ForeColor = Color.Red
                    Else
                        'lGROUPER_iDRG_GROUPING.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        'lGROUPER_HASILiDRG_EDITiDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                        txtMDC.ForeColor = Color.Black
                        txtDRG.ForeColor = Color.Black
                    End If
                End If

                lGROUPER_HASILGROUPER.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lCariDiagnosaiDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lCariProseduriDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lGROUPER_HAPUSKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                btnFinaliDRG.Visible = False
                btnGroupingiDRG.Visible = False
                btnFinaliDRG.Text = "Edit Ulang iDRG"

                lGROUPER_INACBG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lGROUPER_INACBG_IMPORT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lGROUPER_INACBG_GROUPING.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                'lGROUPER_HASILiDRG_FINALiDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                If CDec(txtTarifCBG.Text) = 0 Then
                    lFINALKLAIMINACBG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    lEDITULANGKLAIMINACBG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    lFINALKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    lEDITULANGKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    lCETAKKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    lKIRIMONLINEKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Else
                    lFINALKLAIMINACBG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    lEDITULANGKLAIMINACBG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    lFINALKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    lEDITULANGKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    lCETAKKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    lKIRIMONLINEKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                End If

                lHASILKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                If txtSTATUS_DCKEMENKES.Text = "Klaim belum terkirim ke Pusat Data Kementerian Kesehatan" Then
                    txtSTATUS_DCKEMENKES.ForeColor = Color.Red
                Else
                    txtSTATUS_DCKEMENKES.ForeColor = Color.Black
                End If
            Else
                lGROUPER_TRANSAKSI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lGROUPER_APGAR.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lGROUPER_TARIF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lGROUPER_iDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                'lGROUPER_iDRG_GROUPING.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lGROUPER_HASILiDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                'lGROUPER_HASILiDRG_EDITiDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                'lGROUPER_HASILiDRG_FINALiDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lGROUPER_INACBG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lGROUPER_INACBG_IMPORT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lGROUPER_INACBG_GROUPING.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lEDITULANGKLAIMINACBG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lCETAKKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lKIRIMONLINEKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lFINALKLAIMINACBG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lGROUPER8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lGROUPER_HAPUSKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lFINALKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lEDITULANGKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        Else
            lKlaimBaruCovid.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lGROUPER_TRANSAKSI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lGROUPER_APGAR.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lGROUPER_TARIF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lGROUPER_iDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            'lGROUPER_iDRG_GROUPING.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lGROUPER_HASILiDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            'lGROUPER_HASILiDRG_EDITiDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            'lGROUPER_HASILiDRG_FINALiDRG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lGROUPER_INACBG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lGROUPER_INACBG_IMPORT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lGROUPER_INACBG_GROUPING.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lEDITULANGKLAIMINACBG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lCETAKKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lKIRIMONLINEKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lFINALKLAIMINACBG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lGROUPER8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lGROUPER_HAPUSKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lFINALKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lEDITULANGKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lHASILKLAIM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lGROUPER_HASILGROUPER.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub
    Private Sub fn_EmptyMe()
        listdiagnosakodeidRG.Clear()
        listprosedurkodeidRG.Clear()
        sGantiDiagnosa = False
        sGantiProsedur = False
        sJumlahProsedur = 0

        txtGroupInacbg.ResetText()
        txtCodeCBG.ResetText()
        txtDescriptionCBG.ResetText()
        txtTarifCBG.Text = 0
        txtCodeSubAcute.ResetText()
        txtDescriptionSubAcute.ResetText()
        txtTarifSubAcute.Text = 0
        txtCodeChronic.ResetText()
        txtDescriptionChronic.ResetText()
        txtTarifChronic.Text = 0
        txtSPECIAL1.ResetText()
        txtSPECIAL1_CODE.ResetText()
        txtSPECIAL1_DESCRIPTION.ResetText()
        txtTarifSpecialProcedure.Text = 0
        txtSPECIAL2.ResetText()
        txtSPECIAL2_CODE.ResetText()
        txtSPECIAL2_DESCRIPTION.ResetText()
        txtTarifSpecialProsthesis.Text = 0
        txtSPECIAL3.ResetText()
        txtSPECIAL3_CODE.ResetText()
        txtSPECIAL3_DESCRIPTION.ResetText()
        txtTarifSpecialInvestigation.Text = 0
        txtSPECIAL4.ResetText()
        txtSPECIAL4_CODE.ResetText()
        txtSPECIAL4_DESCRIPTION.ResetText()
        txtTarifSpecialDrug.Text = 0

        txtInfo.ResetText()
        txtCostWeight.ResetText()
        txttotal_cost_weight.Text = "-"
        txtTopUpCostWeight.ResetText()
        txtTopuCode.ResetText()
        txttotal_tarif.ResetText()
        txtNBR.ResetText()
        txtJenisRawat.ResetText()
        txtMDC.ResetText()
        txtDRG.ResetText()
        txtNumber.ResetText()
        txtCodeIdrg.ResetText()
        txtStatus.ResetText()
        lblDiagnosaiDRG.ResetText()
        lblDiagnosaiDRG_1.ResetText()
        lblDiagnosaiDRG_2.ResetText()
        lblProseduriDRG.ResetText()
        lblProseduriDRG_1.ResetText()
        lblProseduriDRG_2.ResetText()
        lblLabelHasilImport.ResetText()
        lblLabelHasilImportProsedur.ResetText()

        grdCARAMASUK.Text = "gp"
        txtCODE.Text = sNoId
        chkKelasEksekutif.Checked = False
        cboCaraBayar.SelectedIndex = 0
        txtNoPeserta.ResetText()
        grdJenisTarif.Text = oRIdentitasGrouperData.KodeTarifKlaim_Default()
        grdCOB.Text = oRIdentitasGrouper.cbo_Default()
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
        grdCaraKeluar.Text = oRIdentitasGrouper.CaraKeluar_Default()
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
        'txtICD_X.ResetText()
        'txtICCD_IX.ResetText()
        txtInfoInacbg.ResetText()
        txttJenisRawatInacbg.ResetText()
        txtGroupInacbg.Reset()
        txtStatusInacbg.ResetText()
        txtRAWATINTENSIF_HARI.Text = 0
        txtVENTILATOR.Text = 0
        txtCodeChronic.Text = "-"
        txtDescriptionChronic.Text = "-"
        txtTarifChronic.Text = 0
        txtCodeSubAcute.Text = "-"
        txtDescriptionSubAcute.Text = "-"
        txtTarifSubAcute.Text = 0
        txtSPECIAL1.Text = ""
        txtSPECIAL1_CODE.Text = ""
        txtSPECIAL1_DESCRIPTION.Text = ""
        txtSPECIAL2.Text = ""
        txtSPECIAL2_CODE.Text = ""
        txtSPECIAL2_DESCRIPTION.Text = ""
        txtSPECIAL3.Text = ""
        txtSPECIAL3_CODE.Text = ""
        txtSPECIAL3_DESCRIPTION.Text = ""
        txtSPECIAL4.Text = ""
        txtSPECIAL4_CODE.Text = ""
        txtSPECIAL4_DESCRIPTION.Text = ""
        txtSISTOLE.Text = "0"
        txtDIASTOLE.Text = "0"
        chkPasienTB.Checked = False
        txtNOMORSITB.ResetText()
        rbDIALIZERSINGGLEUSE.SelectedIndex = 0
        cboVENTILATOR_use_ind.Text = "0"
        deVENTILATOR_start_dttm.DateTime = Now
        deVENTILATOR_stop_dttm.DateTime = Now
        txtKANTONGDARAH.Text = "0"
        cboALTEPLASE_IND.Text = "0"
        chkAPGAR.Checked = False
        cboMenit1_appearance.ResetText()
        cboMenit1_Pulse.ResetText()
        cboMenit1_Grimace.ResetText()
        cboMenit1_Activity.ResetText()
        cboMenit1_Respiration.ResetText()
        cboMenit5_appearance.ResetText()
        cboMenit5_Pulse.ResetText()
        cboMenit5_Grimace.ResetText()
        cboMenit5_Activity.ResetText()
        cboMenit5_Respiration.ResetText()
        chkPERSALINAN.Checked = False
        txtPERSALINAN_usia_kehamilan.ResetText()
        txtPERSALINAN_gravida.ResetText()
        txtPERSALINAN_partus.ResetText()
        txtPERSALINAN_abortus.ResetText()
        cboPERSALINAN_onset_kontraksi.ResetText()

        txtMEMO.ResetText()
        txtSTATUS_DCKEMENKES.ResetText()

        grvDiagnosa.OptionsSelection.MultiSelect = True
        grvDiagnosa.SelectAll()
        grvDiagnosa.DeleteSelectedRows()
        grvDiagnosa.OptionsSelection.MultiSelect = False

        'grvDiagnosaiDRG.OptionsSelection.MultiSelect = True
        'grvDiagnosaiDRG.SelectAll()
        'grvDiagnosaiDRG.DeleteSelectedRows()
        'grvDiagnosaiDRG.OptionsSelection.MultiSelect = False

        grvProsedur.OptionsSelection.MultiSelect = True
        grvProsedur.SelectAll()
        grvProsedur.DeleteSelectedRows()
        grvProsedur.OptionsSelection.MultiSelect = False

        'grvProseduriDRG.OptionsSelection.MultiSelect = True
        'grvProseduriDRG.SelectAll()
        'grvProseduriDRG.DeleteSelectedRows()
        'grvProseduriDRG.OptionsSelection.MultiSelect = False

        fn_LoadDataDGCareRincianHeader()
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim dsR_IDENTITAS_GROUPER = oRIdentitasGrouperData.GetData(sNoId)

            With dsR_IDENTITAS_GROUPER
                txtCODE.Text = .kodegrouper
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
                'txtICD_X.Text = .ICD_10
                'txtICCD_IX.Text = .ICD_9
                txtRAWATINTENSIF_HARI.Text = .RAWATINTENSIF_HARI
                txtVENTILATOR.Text = .VENTILATOR
                txtCodeCBG.Text = .CBG_CODE
                txtDescriptionCBG.Text = .CBG_DESCRIPTION
                txtTarifCBG.Text = .CBG_TARIF
                txtCodeSubAcute.Text = .SUBACUTE_CODE
                txtDescriptionSubAcute.Text = .SUBACUTE_DESCRIPTION
                txtTarifSubAcute.Text = .SUBACUTE_TARIF
                txtCodeChronic.Text = .CHRONIC_CODE
                txtDescriptionChronic.Text = .CHRONIC_DESCRIPTION
                txtTarifChronic.Text = .CHRONIC_TARIF
                txtSPECIAL1.Text = .SPECIAL1
                txtSPECIAL1_CODE.Text = .SPECIAL1_CODE
                txtSPECIAL1_DESCRIPTION.Text = .SPECIAL1_DESCRIPTION
                txtTarifSpecialProcedure.Text = .SPECIAL1_TARIF
                txtSPECIAL2.Text = .SPECIAL2
                txtSPECIAL2_CODE.Text = .SPECIAL2_CODE
                txtSPECIAL2_DESCRIPTION.Text = .SPECIAL2_DESCRIPTION
                txtTarifSpecialProsthesis.Text = .SPECIAL2_TARIF
                txtSPECIAL3.Text = .SPECIAL3
                txtSPECIAL3_CODE.Text = .SPECIAL3_CODE
                txtSPECIAL3_DESCRIPTION.Text = .SPECIAL3_DESCRIPTION
                txtTarifSpecialInvestigation.Text = .SPECIAL3_TARIF
                txtSPECIAL4.Text = .SPECIAL4
                txtSPECIAL4_CODE.Text = .SPECIAL4_CODE
                txtSPECIAL4_DESCRIPTION.Text = .SPECIAL4_DESCRIPTION
                txtTarifSpecialDrug.Text = .SPECIAL4_TARIF
                txtTarifBiayaTambahan.Text = .BIAYATAMBAHAN_TARIF
                txtPersenTambahanBiaya.Text = .BIAYATAMBAHAN_PERSEN
                txtTarifTotal.Text = .TOTALTARIF
                rbDIALIZERSINGGLEUSE.SelectedIndex = .DIALIZER
                cboCaraBayar.SelectedIndex = .CARABAYAR
                grdCARAMASUK.Text = .KDCARAMASUK
                txtSISTOLE.Text = .SISTOLE
                txtDIASTOLE.Text = .DIASTOLE
                txtNOMORSITB.Text = .NOMORSITB
                If txtNOMORSITB.Text <> "" Then
                    chkPasienTB.Checked = True
                End If

                If .DIAGNOSA_INAGROUPER <> "" Then
                    chkINA.Checked = True
                End If
                If .PROCEDURE_INAGROUPER <> "" Then
                    chkINA.Checked = True
                End If

                cboVENTILATOR_use_ind.Text = .VENTILATOR_use_ind
                deVENTILATOR_start_dttm.DateTime = .VENTILATOR_start_dttm
                deVENTILATOR_stop_dttm.DateTime = .VENTILATOR_stop_dttm

                txtKANTONGDARAH.Text = .KANTONGDARAH
                cboALTEPLASE_IND.Text = .ALTEPLASE_IND

                chkAPGAR.Checked = .APGAR
                cboMenit1_appearance.Text = .APGAR_menit_1_appearance
                cboMenit1_Pulse.Text = .APGAR_menit_1_pulse
                cboMenit1_Grimace.Text = .APGAR_menit_1_grimace
                cboMenit1_Activity.Text = .APGAR_menit_1_activity
                cboMenit1_Respiration.Text = .APGAR_menit_1_respiration
                cboMenit5_appearance.Text = .APGAR_menit_5_appearance
                cboMenit5_Pulse.Text = .APGAR_menit_5_pulse
                cboMenit5_Grimace.Text = .APGAR_menit_5_grimace
                cboMenit5_Activity.Text = .APGAR_menit_5_activity
                cboMenit5_Respiration.Text = .APGAR_menit_5_respiration

                chkPERSALINAN.Checked = .PERSALINAN
                txtPERSALINAN_usia_kehamilan.Text = .PERSALINAN_usia_kehamilan
                txtPERSALINAN_gravida.Text = .PERSALINAN_gravida
                txtPERSALINAN_partus.Text = .PERSALINAN_partus
                txtPERSALINAN_abortus.Text = .PERSALINAN_abortus
                cboPERSALINAN_onset_kontraksi.Text = .PERSALINAN_onset_kontraksi

                txtKDDIAGNOSA_V6.Text = .DIAGNOSA_INAGROUPER
                txtKDPROSEDUR_V6.Text = .PROCEDURE_INAGROUPER

                'sNorec = .R_IDENTITAS_GROUPER.norec
                'snostruklastfk = .R_IDENTITAS_GROUPER.nostruklastfk

                txtNORM.Text = .R_IDENTITAS_GROUPER.noRm.ToString.PadLeft(6, "0")
                txtNAMAPASIEN.Text = .R_IDENTITAS_GROUPER.nama.ToString
                txtTGLLAHIR.Text = .R_IDENTITAS_GROUPER.tgl_lahir.ToString("yyyy-MM-dd HH:mm:ss")
                txtJENISKELAMIN.Text = .R_IDENTITAS_GROUPER.gender

                txtInfoInacbg.Text = .INFO
                txttJenisRawatInacbg.Text = .JENISRAWAT
                txtGroupInacbg.Text = .HASILGROUPER
                txtStatusInacbg.Text = .STATUS_INACBG
                txtMEMO.Text = .MEMO
                txtSTATUS_DCKEMENKES.Text = .STATUS_DCKEMENKES

                BindingSourceDiagnosa.DataSource = oRIdentitasGrouperData.GetDataDetailDiagnosa(sNoId).OrderBy(Function(x) x.seq).ToList()
                grdDiagnosa.DataSource = BindingSourceDiagnosa

                BindingSourceProsedur.DataSource = oRIdentitasGrouperData.GetDataDetailProsedur(sNoId).OrderBy(Function(x) x.seq).ToList()
                grdProsedur.DataSource = BindingSourceProsedur

                'BindingSourceRincian.DataSource = oRIdentitasGrouperData.GetDataDetailrincian(sNoId).OrderBy(Function(x) x.seq).ToList()
                'grdRincian.DataSource = BindingSourceRincian

                BindingSourcePersalinan.DataSource = oRIdentitasGrouperData.GetDataDetailPersalinan(sNoId).OrderBy(Function(x) x.delivery_sequence).ToList()
                grdPersalinan.DataSource = BindingSourcePersalinan

                XtraTabControl1.SelectedTabPage = tab2
                XtraTabControl1.SelectedTabPage = tab1

                'fn_LoadFormatDataRincian()

                Dim ds2 = oRIdentitasGrouper.GetData(sNoId)
                If ds2 IsNot Nothing Then
                    fn_LoadDataDGCareRincianDetilNew(ds2.norec)
                End If

                rbCategory_SelectedIndexChanged()

                If cboVENTILATOR_use_ind.Text = "0" Then
                    lVentilatorDate1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    lVentilatorDate2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Else
                    lVentilatorDate1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    lVentilatorDate2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                End If

            End With


        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate()
        Try
            fn_Validate = True

            If grdCARAMASUK.Text = "" Then
                grdCARAMASUK.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                MsgBox("Di butuhkan cara masuk", MsgBoxStyle.Exclamation, Me.Text)

                grdCARAMASUK.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtJENISKELAMIN.Text = "-" Then
                txtJENISKELAMIN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                MsgBox("Jenis Kelamin Strip", MsgBoxStyle.Exclamation, Me.Text)

                txtJENISKELAMIN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdDPJP.Text = String.Empty Then
                grdDPJP.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                MsgBox("Dibutuhkan DPJP", MsgBoxStyle.Exclamation, Me.Text)

                grdDPJP.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtNoPeserta.Text = String.Empty Then
                txtNoPeserta.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                MsgBox("Dibutuhkan No Peserta", MsgBoxStyle.Exclamation, Me.Text)

                txtNoPeserta.Focus()
                fn_Validate = False
                Exit Function
            End If
            Dim validasi As Boolean = False

            For i As Integer = 0 To grvDiagnosa.RowCount - 2
                validasi = True
            Next

            If validasi = False Then
                MsgBox("Dibutuhkan ICD X", MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End If

            If txtNoSEP.Text = String.Empty Then
                txtNoSEP.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                MsgBox("Dibutuhkan Nomor SEP", MsgBoxStyle.Exclamation, Me.Text)

                txtNoSEP.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdCOB.Text = String.Empty Then
                grdCOB.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                MsgBox("Dibutuhkan COB", MsgBoxStyle.Exclamation, Me.Text)

                grdCOB.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdCaraKeluar.Text = String.Empty Then
                grdCaraKeluar.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                MsgBox("Dibutuhkan Cara Keluar", MsgBoxStyle.Exclamation, Me.Text)

                grdCaraKeluar.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txttarifRumahSakit.Text <= 0 Then
                txttarifRumahSakit.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                MsgBox("Tarif Rumah Sakit Masih 0", MsgBoxStyle.Exclamation, Me.Text)

                txttarifRumahSakit.Focus()
                fn_Validate = False
                Exit Function
            End If

            Dim sLanjut As Boolean = False

            For i As Integer = 0 To grvRincian.RowCount - 2
                If grvRincian.GetRowCellValue(i, "kategoribpjs") = "" Then
                    sLanjut = False
                    Exit For
                ElseIf grvRincian.GetRowCellValue(i, "kategoribpjs") = "-" Then
                    sLanjut = False
                    Exit For
                Else
                    sLanjut = True
                End If
            Next

            If sLanjut = False Then
                MsgBox("Item/kategoribpjs masih ada yang kosong!!!", MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End If
        Catch oErr As Exception
            fn_Validate = False
            MsgBox("Validate Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save(ByVal MEMO As String, ByVal sSTATUSINACB As String, ByVal sSTATUSDCKMENKES As String) As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oRIdentitasGrouperData.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oRIdentitasGrouperData.GetData(txtCODE.Text).DATECREATED
                    oFormMode = FORM_MODE.FORM_MODE_EDIT
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .kodegrouper = txtCODE.Text
                .KDPENDAFTARAN = ""
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
                .RAWATINTENSIF_HARI = txtRAWATINTENSIF_HARI.Text
                .LOS = txtLOS.Text
                .ADLSCORE_SUBACUTE = IIf(txtADLScore_SubAcute.Text = "-", 0, 0)
                .ADLSCORE_CHRONIC = IIf(txtADLScore_Chronic.Text = "-", 0, 0)
                .DPJP = grdDPJP.EditValue
                .HAKKELAS = rbKELASHAK.SelectedIndex
                .UMUR = txtUmur.Text
                .LAMA = txtLAMA.Text
                .VENTILATOR = txtVENTILATOR.Text
                .BERATBADAN = txtBeratBadan.Text
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

                Dim listdiagnosakode As New List(Of String)
                For i As Integer = 0 To grvDiagnosa.RowCount - 2
                    listdiagnosakode.Add(grvDiagnosa.GetRowCellValue(i, colkddiagnosa))
                Next

                .ICD_10 = String.Join("#", listdiagnosakode.ToArray)

                Dim listprosedurkode As New List(Of String)
                For i As Integer = 0 To grvProsedur.RowCount - 2
                    listprosedurkode.Add(grvProsedur.GetRowCellValue(i, colkdprpsedur))
                Next

                .ICD_9 = String.Join("#", listprosedurkode.ToArray)
                .CBG_CODE = txtCodeCBG.Text
                .CBG_DESCRIPTION = txtDescriptionCBG.Text
                .CBG_TARIF = txtTarifCBG.Text
                .SUBACUTE_CODE = txtCodeSubAcute.Text
                .SUBACUTE_DESCRIPTION = txtDescriptionSubAcute.Text
                .SUBACUTE_TARIF = txtTarifSubAcute.Text
                .CHRONIC_CODE = txtCodeChronic.Text
                .CHRONIC_DESCRIPTION = txtDescriptionChronic.Text
                .CHRONIC_TARIF = txtTarifChronic.Text
                .SPECIAL1 = txtSPECIAL1.Text
                .SPECIAL1_CODE = txtSPECIAL1_CODE.Text
                .SPECIAL1_DESCRIPTION = txtSPECIAL1_DESCRIPTION.Text
                .SPECIAL1_TARIF = txtTarifSpecialProcedure.Text
                .SPECIAL2 = txtSPECIAL2.Text
                .SPECIAL2_CODE = txtSPECIAL2_CODE.Text
                .SPECIAL2_DESCRIPTION = txtSPECIAL2_DESCRIPTION.Text
                .SPECIAL2_TARIF = txtTarifSpecialProsthesis.Text
                .SPECIAL3 = txtSPECIAL3.Text
                .SPECIAL3_CODE = txtSPECIAL3_CODE.Text
                .SPECIAL3_DESCRIPTION = txtSPECIAL3_DESCRIPTION.Text
                .SPECIAL3_TARIF = txtTarifSpecialInvestigation.Text
                .SPECIAL4 = txtSPECIAL4.Text
                .SPECIAL4_CODE = txtSPECIAL4_CODE.Text
                .SPECIAL4_DESCRIPTION = txtSPECIAL4_DESCRIPTION.Text
                .SPECIAL4_TARIF = txtTarifSpecialDrug.Text
                .BIAYATAMBAHAN_TARIF = txtTarifBiayaTambahan.Text
                .BIAYATAMBAHAN_PERSEN = txtPersenTambahanBiaya.Text
                .TOTALTARIF = txtTarifTotal.Text
                .MEMO = MEMO
                Try
                    .ISKIRIMONLINE = oRIdentitasGrouperData.GetData(sNoId).ISKIRIMONLINE
                Catch ex As Exception
                    .ISKIRIMONLINE = False
                End Try
                .NOIDUSER = sUserID
                .COVID_EPISODES = "0"
                .COVID_EPISODES_HARI = "0"
                .COVID_SUSPEK = "0"
                .COVID_KRITERIAAKSESNAAT = "0"
                .COVID_DARURATLAPANGAN = "0"
                .COVID_KOMORBID = "0"
                .COVID_ISOLASI = "0"
                .COVID_COINSIDENS = "0"
                .COVID_ASAMLAKTAT = False
                .COVID_KULTUR = False
                .COVID_APTT = False
                .COVID_ANALISAGAS = False
                .COVID_PROCALCITONIN = False
                .COVID_DDIMER = False
                .COVID_WAKTUPENDARAHAN = False
                .COVID_ALBUMIN = False
                .COVID_CRP = False
                .COVID_PT = False
                .COVID_ANTIHIV = False
                .COVID_THORAX = False
                .COVID_PEMULASARANJENAZAH = False
                .COVID_PLASTIKERAT = False
                .COVID_KANTONGJENAZAH = False
                .COVID_DESINFEKTANJENAZAH = False
                .COVID_DISINFEKTANMOBIL = False
                .COVID_PETIJENAZAH = False
                .COVID_TRANSPORTMOBIL = False
                .COVID_NOMORKARTU = "0"
                .COVID_TERAPICONVESIONAL = "0"
                .DIAGNOSA_INAGROUPER = txtKDDIAGNOSA_V6.Text
                .PROCEDURE_INAGROUPER = txtKDPROSEDUR_V6.Text
                .KDCARAMASUK = grdCARAMASUK.EditValue
                .SISTOLE = txtSISTOLE.Text
                .DIASTOLE = txtDIASTOLE.Text
                .NOMORSITB = txtNOMORSITB.Text
                .DIALIZER = rbDIALIZERSINGGLEUSE.SelectedIndex
                .VENTILATOR_use_ind = cboVENTILATOR_use_ind.Text
                .VENTILATOR_start_dttm = deVENTILATOR_start_dttm.DateTime
                .VENTILATOR_stop_dttm = deVENTILATOR_stop_dttm.DateTime
                .KANTONGDARAH = txtKANTONGDARAH.Text
                .ALTEPLASE_IND = cboALTEPLASE_IND.Text
                .APGAR = chkAPGAR.Checked
                .APGAR_menit_1_appearance = cboMenit1_appearance.Text
                .APGAR_menit_1_pulse = cboMenit1_Pulse.Text
                .APGAR_menit_1_grimace = cboMenit1_Grimace.Text
                .APGAR_menit_1_activity = cboMenit1_Activity.Text
                .APGAR_menit_1_respiration = cboMenit1_Respiration.Text
                .APGAR_menit_5_appearance = cboMenit5_appearance.Text
                .APGAR_menit_5_pulse = cboMenit5_Pulse.Text
                .APGAR_menit_5_grimace = cboMenit5_Grimace.Text
                .APGAR_menit_5_activity = cboMenit5_Activity.Text
                .APGAR_menit_5_respiration = cboMenit5_Respiration.Text
                .PERSALINAN = chkPERSALINAN.Checked
                .PERSALINAN_usia_kehamilan = txtPERSALINAN_usia_kehamilan.Text
                .PERSALINAN_gravida = txtPERSALINAN_gravida.Text
                .PERSALINAN_partus = txtPERSALINAN_partus.Text
                .PERSALINAN_abortus = txtPERSALINAN_abortus.Text
                .PERSALINAN_onset_kontraksi = cboPERSALINAN_onset_kontraksi.Text
                .INFO = txtInfoInacbg.Text
                .JENISRAWAT = txttJenisRawatInacbg.Text
                .HASILGROUPER = txtGroupInacbg.Text
                .STATUS_INACBG = sSTATUSINACB
                .STATUS_DCKEMENKES = sSTATUSDCKMENKES
            End With

            'DIAGNOSA
            Dim arrDetailDiagnosa = oRIdentitasGrouperData.GetStructureDetaiDiagnosalList
            For i As Integer = 0 To grvDiagnosa.RowCount - 2
                Dim dsDetail = oRIdentitasGrouperData.GetStructureDetailDiagnosa
                With dsDetail
                    Try
                        .datecreated = oRIdentitasGrouperData.GetDataDiagnosaBySeq(ds.kodegrouper, i).datecreated
                    Catch ex As Exception
                        .datecreated = Now
                    End Try
                    .dateupdated = Now
                    .kodegrouper = ds.kodegrouper
                    .seq = i
                    .kategori = IIf(String.IsNullOrEmpty(grvDiagnosa.GetRowCellValue(i, colkategori)), "", grvDiagnosa.GetRowCellValue(i, colkategori))
                    .kddiagnosa = IIf(String.IsNullOrEmpty(grvDiagnosa.GetRowCellValue(i, colkddiagnosa)), "", grvDiagnosa.GetRowCellValue(i, colkddiagnosa))
                    .memo = IIf(String.IsNullOrEmpty(grvDiagnosa.GetRowCellValue(i, colmemodiagnosa)), "", grvDiagnosa.GetRowCellValue(i, colmemodiagnosa))
                    .kduser = sUserID
                End With
                arrDetailDiagnosa.Add(dsDetail)
            Next
            'PROSEDUR
            Dim arrDetailProsedur = oRIdentitasGrouperData.GetStructureDetaiProsedurlList
            For i As Integer = 0 To grvProsedur.RowCount - 2
                Dim dsDetail = oRIdentitasGrouperData.GetStructureDetailProsedur
                With dsDetail
                    Try
                        .datecreated = oRIdentitasGrouperData.GetDataProsedurBySeq(ds.kodegrouper, i).datecreated
                    Catch ex As Exception
                        .datecreated = Now
                    End Try
                    .dateupdated = Now
                    .kodegrouper = ds.kodegrouper
                    .seq = i
                    .kdprpsedur = IIf(String.IsNullOrEmpty(grvProsedur.GetRowCellValue(i, colkdprpsedur)), "", grvProsedur.GetRowCellValue(i, colkdprpsedur))
                    .memo = IIf(String.IsNullOrEmpty(grvProsedur.GetRowCellValue(i, colmemoprosedur)), "", grvProsedur.GetRowCellValue(i, colmemoprosedur))
                    .kduser = sUserID
                End With
                arrDetailProsedur.Add(dsDetail)
            Next

            'RINCIAN
            Dim arrDetailRincian = oRIdentitasGrouperData.GetStructureDetaiRincianlList
            For i As Integer = 0 To grvRincian.RowCount - 2
                Dim dsDetail = oRIdentitasGrouperData.GetStructureDetailRincian
                With dsDetail
                    Try
                        .datecreated = oRIdentitasGrouperData.GetDataRincianBySeq(ds.kodegrouper, i).datecreated
                    Catch ex As Exception
                        .datecreated = Now
                    End Try
                    .dateupdated = Now
                    .kodegrouper = ds.kodegrouper
                    .seq = i
                    .produkfk = IIf(String.IsNullOrEmpty(grvRincian.GetRowCellValue(i, colprodukfk)), "", grvRincian.GetRowCellValue(i, colprodukfk))
                    .kategoribpjs = IIf(String.IsNullOrEmpty(grvRincian.GetRowCellValue(i, colkategoribpjs)), "", grvRincian.GetRowCellValue(i, colkategoribpjs))
                    .isobat = IIf(String.IsNullOrEmpty(grvRincian.GetRowCellValue(i, colisobat)), False, grvRincian.GetRowCellValue(i, colisobat))
                    .namaproduk = IIf(String.IsNullOrEmpty(grvRincian.GetRowCellValue(i, colnamaproduk)), "", grvRincian.GetRowCellValue(i, colnamaproduk))
                    .jumlah = IIf(String.IsNullOrEmpty(grvRincian.GetRowCellValue(i, coljumlah)), 0, grvRincian.GetRowCellValue(i, coljumlah))
                    .hargajual = IIf(String.IsNullOrEmpty(grvRincian.GetRowCellValue(i, colhargajual)), 0, grvRincian.GetRowCellValue(i, colhargajual))
                    .kduser = sUserID
                End With
                arrDetailRincian.Add(dsDetail)
            Next

            'PERSALINAN
            Dim arrDetailPersalinan = oRIdentitasGrouperData.GetStructureDetaiPersalinanList
            For i As Integer = 0 To grvPersalinan.RowCount - 2
                Dim dsDetail = oRIdentitasGrouperData.GetStructureDetailPersalian
                With dsDetail
                    .kodegrouper = ds.kodegrouper
                    .delivery_sequence = IIf(String.IsNullOrEmpty(grvPersalinan.GetRowCellValue(i, coldelivery_sequence)), "1", grvPersalinan.GetRowCellValue(i, coldelivery_sequence))
                    .delivery_method = IIf(String.IsNullOrEmpty(grvPersalinan.GetRowCellValue(i, coldelivery_method)), "", grvPersalinan.GetRowCellValue(i, coldelivery_method))
                    .delivery_dttm = IIf(String.IsNullOrEmpty(grvPersalinan.GetRowCellValue(i, coldelivery_dttm)), Now, grvPersalinan.GetRowCellValue(i, coldelivery_dttm))
                    .letak_janin = IIf(String.IsNullOrEmpty(grvPersalinan.GetRowCellValue(i, colletak_janin)), "", grvPersalinan.GetRowCellValue(i, colletak_janin))
                    .kondisi = IIf(String.IsNullOrEmpty(grvPersalinan.GetRowCellValue(i, colkondisi)), "", grvPersalinan.GetRowCellValue(i, colkondisi))
                    .use_manual = IIf(String.IsNullOrEmpty(grvPersalinan.GetRowCellValue(i, coluse_manual)), "", grvPersalinan.GetRowCellValue(i, coluse_manual))
                    .use_forcep = IIf(String.IsNullOrEmpty(grvPersalinan.GetRowCellValue(i, coluse_forcep)), "", grvPersalinan.GetRowCellValue(i, coluse_forcep))
                    .use_vacuum = IIf(String.IsNullOrEmpty(grvPersalinan.GetRowCellValue(i, coluse_vacuum)), "", grvPersalinan.GetRowCellValue(i, coluse_vacuum))
                    .shk_spesimen_ambil = IIf(String.IsNullOrEmpty(grvPersalinan.GetRowCellValue(i, colshk_spesimen_ambil)), "", grvPersalinan.GetRowCellValue(i, colshk_spesimen_ambil))
                    .shk_lokasi = IIf(String.IsNullOrEmpty(grvPersalinan.GetRowCellValue(i, colshk_lokasi)), "", grvPersalinan.GetRowCellValue(i, colshk_lokasi))
                    .shk_spesimen_dttm = IIf(String.IsNullOrEmpty(grvPersalinan.GetRowCellValue(i, colshk_spesimen_dttm)), Now, grvPersalinan.GetRowCellValue(i, colshk_spesimen_dttm))
                    .memo = IIf(String.IsNullOrEmpty(grvPersalinan.GetRowCellValue(i, colshk_alasan)), "", grvPersalinan.GetRowCellValue(i, colshk_alasan))
                    .kduser = sUserID
                End With
                arrDetailPersalinan.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oRIdentitasGrouperData.InsertData(ds, arrDetailDiagnosa, arrDetailProsedur, arrDetailRincian, arrDetailPersalinan)
                Catch oErr As Exception
                    MsgBox("Simpan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oRIdentitasGrouperData.UpdateData(ds, arrDetailDiagnosa, arrDetailProsedur, arrDetailRincian, arrDetailPersalinan)
                Catch oErr As Exception
                    MsgBox("Simpan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

        Catch oErr As Exception
            fn_Save = False
            MsgBox("Simpan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try

    End Function
    Private Function fn_SaveStatus(ByVal sSTATUS As String, ByVal sRequest As String, ByVal sRespons As String) As Boolean
        Try
            Dim sCekStatus As Boolean = True

            ' ***** HEADER *****
            Dim ds = oStatusGrouper.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oStatusGrouper.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    sCekStatus = False
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .kodegrouper = sNoId
                .STATUS = sSTATUS

                If sRequest = "" Then
                    Try
                        .REQUEST = oStatusGrouper.GetData(sNoId).REQUEST
                    Catch ex As Exception
                        .REQUEST = sSTATUS & vbCrLf & sRequest
                    End Try
                Else
                    Try
                        .REQUEST = oStatusGrouper.GetData(sNoId).REQUEST & vbCrLf & sSTATUS & vbCrLf & sRequest
                    Catch ex As Exception
                        .REQUEST = sSTATUS & vbCrLf & sRequest
                    End Try
                End If
                If sRespons = "" Then
                    Try
                        .RESPONS = oStatusGrouper.GetData(sNoId).RESPONS
                    Catch ex As Exception
                        .RESPONS = sSTATUS & vbCrLf & sRespons
                    End Try
                Else
                    Try
                        .RESPONS = oStatusGrouper.GetData(sNoId).RESPONS & vbCrLf & sSTATUS & vbCrLf & sRespons
                    Catch ex As Exception
                        .RESPONS = sSTATUS & vbCrLf & sRespons
                    End Try
                End If

                .MEMO = ""
                .KDUSER = sUserID
            End With

            If sCekStatus = False Then
                fn_SaveStatus = oStatusGrouper.InsertData(ds)
            Else
                fn_SaveStatus = oStatusGrouper.UpdateData(ds)
            End If

        Catch oErr As Exception
            fn_SaveStatus = False
            MsgBox("Simpan Status : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_SaveStatusHasil(ByVal Respon As String) As Boolean
        Try
            Dim sCekStatus As Boolean = True

            ' ***** HEADER *****
            Dim ds = oStatusGrouperHasil.GetStructureHeader
            With ds
                Try
                    .datecreated = oStatusGrouperHasil.GetData(sNoId).datecreated
                Catch oErr As Exception
                    sCekStatus = False
                    .datecreated = Now
                End Try
                .dateupdated = Now
                .kodegrouper = sNoId
                .seq = 0
                .info = txtInfo.Text
                .jenirawat = txtJenisRawat.Text
                .MDC = txtMDC.Text
                .DRG = txtDRG.Text
                .status = txtStatus.Text
                .drg_code = txtCodeIdrg.Text
                .mdc_number = txtNumber.Text
                .memo = Respon
                .kduser = sUserID
                .COSTWEIGHT = txtCostWeight.Text
                .NBR = txtNBR.Text
                Try
                    .TopUpCostWeight_codeA = oStatusGrouperHasil.GetData(sNoId).TopUpCostWeight_codeA
                Catch ex As Exception
                    .TopUpCostWeight_codeA = ""
                End Try
                'Try
                '    .TopUpCostWeight = oStatusGrouperHasil.GetData(sNoId).TopUpCostWeight
                'Catch ex As Exception
                '    .TopUpCostWeight = ""
                'End Try

                .TopUpCostWeight = ""
            End With

            Dim jsonObj As JObject = JObject.Parse(ds.memo)
            Dim arrDetail = oStatusGrouperHasil.GetStructureDetailList

            ' Ambil topup_options
            Dim topupOptions As JToken = jsonObj("response_idrg")("topup_options")

            ' Cek apakah tidak null
            If topupOptions IsNot Nothing AndAlso topupOptions.Type = JTokenType.Array Then
                ' Looping array
                Dim iArrayInteger As Integer = 0

                For Each item As JObject In topupOptions
                    Dim dsDetail = oStatusGrouperHasil.GetStructureDetail
                    With dsDetail
                        .datecreated = ds.datecreated
                        .dateupdated = ds.dateupdated
                        .kodegrouper = ds.kodegrouper
                        .seq = iArrayInteger
                        .code = item("code").ToString()
                        .description = item("description").ToString()
                        .type = item("type").ToString()
                        .cost_weight = item("cost_weight").ToString()
                    End With

                    iArrayInteger += 1

                    arrDetail.Add(dsDetail)
                Next
            End If

            If sCekStatus = False Then
                fn_SaveStatusHasil = oStatusGrouperHasil.InsertData(ds, arrDetail)
            Else
                fn_SaveStatusHasil = oStatusGrouperHasil.UpdateData(ds, arrDetail)
            End If

        Catch oErr As Exception
            fn_SaveStatusHasil = False
            MsgBox("Simpan Status : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub HasilIDRG(ByVal Respon As String, ByVal code As String)
        Try
            Dim jsonDecode = JObject.Parse(Respon)

            txtInfo.Text = "@ " & Now.ToString("dd MMM yyyy HH:mm:ss") & " - " & jsonDecode("response_idrg")("script_version").ToString & " / " & jsonDecode("response_idrg")("logic_version").ToString
            txtJenisRawat.Text = IIf(rbCategory.SelectedIndex = 0, "Rawat Jalan", "Rawat Inap" & " (" & txtLOS.Text & " Hari)")
            txtMDC.Text = jsonDecode("response_idrg")("mdc_description").ToString
            txtDRG.Text = jsonDecode("response_idrg")("drg_description").ToString
            txtNumber.Text = jsonDecode("response_idrg")("mdc_number").ToString
            txtCodeIdrg.Text = jsonDecode("response_idrg")("drg_code").ToString
            txtStatus.Text = jsonDecode("response_idrg")("status_cd").ToString

            txtCostWeight.Text = "** " & jsonDecode("response_idrg")("cost_weight").ToString
            txtNBR.Text = "** " & FormatNumber(jsonDecode("response_idrg")("nbr").ToString, 0)
            txttotal_cost_weight.Text = "** " & jsonDecode("response_idrg")("total_cost_weight").ToString
            'txtTopUpCostWeight.Text = "-"
            'txtTopuCode.Text = ""
            txttotal_tarif.Text = "** " & FormatNumber(jsonDecode("response_idrg")("total_tarif").ToString, 0)

            Try
                Dim topupOptions As JToken = jsonDecode("response_idrg")("topup_options")

                If topupOptions IsNot Nothing AndAlso topupOptions.Type = JTokenType.Array Then
                    For Each item As JObject In topupOptions
                        ' item("description").ToString()
                        If item("code").ToString() = code Then
                            txtTopUpCostWeight.Text = "** " & item("cost_weight").ToString()
                            txtTopuCode.Text = item("code").ToString()

                            oStatusGrouperHasil.UpdateDataStage2(sNoId, item("code").ToString(), item("cost_weight"))

                        End If
                    Next
                End If

            Catch ex As Exception

            End Try

            fn_Topup(txtTopuCode.Text)

        Catch ex As Exception
            txtInfo.ResetText()
            txtJenisRawat.ResetText()
            txtMDC.ResetText()
            txtDRG.ResetText()
            txtNumber.ResetText()
            txtCodeIdrg.ResetText()
            txtStatus.ResetText()
            txtCostWeight.ResetText()
            txtNBR.ResetText()
            txttotal_cost_weight.ResetText()
            txtTopUpCostWeight.Text = "-"
            txtTopuCode.ResetText()
            txttotal_tarif.ResetText()
            TextEdit1.Text = ex.Message & vbCrLf & Respon
        End Try
    End Sub
    Private Function fn_HitungUmur(ByVal tgldatang As Date, tanggllahir As Date) As String
        Dim y, m, d As Integer
        d = tgldatang.Day - tanggllahir.Day
        m = tgldatang.Month - tanggllahir.Month
        y = tgldatang.Year - tanggllahir.Year
        If Math.Sign(d) = -1 Then
            d = 30 - Math.Abs(m)
            m -= 1
        End If
        If Math.Sign(m) = -1 Then
            m = 12 - Math.Abs(m)
            y -= 1

        End If

        fn_HitungUmur = y & " Tahun, " & m & " bulan, " & d & " hari"
    End Function
    Private Function BytesToMegabytes(Bytes As Double) As Double
        'This function gives an estimate to two decimal
        'places.  For a more precise answer, format to
        'more decimal places or just return dblAns

        Dim dblAns As Double
        dblAns = (Bytes / 1024) / 1024
        BytesToMegabytes = Format(dblAns, "###,###,##0.00")
    End Function
    Private Sub XtraTabControl1_SelectedPageChanged() Handles XtraTabControl1.SelectedPageChanged
        If txtNORM.Text = "" Then
            Exit Sub
        End If

        If XtraTabControl1.SelectedTabPageIndex = 2 Then
            If sCategory = 0 Then
                fn_LoadHistoryPasienCPPTRJ(txtNORM.Text, sCategory, sTanggalLahir)
            Else
                fn_LoadPdfViewerCPPTRAJALNAIKRANAP(sRegisterAwal, txtNORM.Text)
            End If
        End If
    End Sub
    Private Sub fn_LoadHistoryPasienCPPTRJ(ByVal sKDCUSTOMER As String, ByVal Category As Integer, ByVal Tanggallahir As String)
        Try
            PdfViewerCPPTRawatJalan.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data CPPT.....")

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

            'GetDataByRekamMedis = oConnection.dbRME.R_CPPTs.Where(Function(x) x.A_IDENTITASPASIEN_LIST.KDCUSTOMER = snoRm And x.ISDELETE = False).OrderByDescending(Function(x) x.DATE).ToList()

            SQL = "Select "
            SQL &= "B.* "
            SQL &= ",PROFESI = ISNULL((Select MEMO FROM R_CPPT_M_PROFESI WHERE B.KDPROFESI = KDPROFESI), '') "
            SQL &= "FROM "
            SQL &= "A_IDENTITASPASIEN_LIST A "
            SQL &= "INNER JOIN R_CPPT B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE A.KDCUSTOMER = '" & sKDCUSTOMER & "' "
            SQL &= "AND B.ISDELETE = 0 "
            SQL &= "AND A.CATEGORY = " & Category & " "
            SQL &= "AND ISNULL((SELECT MEMO FROM R_CPPT_M_PROFESI WHERE B.KDPROFESI = KDPROFESI), '') LIKE '%DOKTER%' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "HISTORY_CPPT")

            Dim listCPPT As New List(Of DataAccess.R_CPPT)
            Dim Identitas As Integer = 0

            For iLoop As Integer = 0 To ds.Tables("HISTORY_CPPT").Rows.Count - 1
                Dim dsRekap As New DataAccess.R_CPPT
                With ds.Tables("HISTORY_CPPT")
                    Identitas = .Rows(iLoop)("KDIDENTITAS")
                    dsRekap.DATECREATED = .Rows(iLoop)("DATECREATED")
                    dsRekap.DATEUPDATED = .Rows(iLoop)("DATEUPDATED")
                    dsRekap.DATE = .Rows(iLoop)("DATE")
                    dsRekap.KDIDENTITAS = .Rows(iLoop)("KDIDENTITAS")
                    dsRekap.KDCPPT = .Rows(iLoop)("KDCPPT")
                    dsRekap.KDPROFESI = .Rows(iLoop)("PROFESI")
                    dsRekap.SUBJEKTIF_KELUHANUTAMA = .Rows(iLoop)("SUBJEKTIF_KELUHANUTAMA")
                    dsRekap.SUBJEKTIF_ALERGI_TIDAK = .Rows(iLoop)("SUBJEKTIF_ALERGI_TIDAK")
                    dsRekap.SUBJEKTIF_ALERGI_YA = .Rows(iLoop)("SUBJEKTIF_ALERGI_YA")
                    dsRekap.SUBJEKTIF_ALERGI_YA_TEXT = .Rows(iLoop)("SUBJEKTIF_ALERGI_YA_TEXT")
                    dsRekap.SUBJEKTIF_TEXT = .Rows(iLoop)("SUBJEKTIF_TEXT")
                    dsRekap.OBJEKTIF_KESADARAN = .Rows(iLoop)("OBJEKTIF_KESADARAN")
                    dsRekap.OBJEKTIF_GCS = .Rows(iLoop)("OBJEKTIF_GCS")
                    dsRekap.OBJEKTIF_TAMPAKSAKIT = .Rows(iLoop)("OBJEKTIF_TAMPAKSAKIT")
                    dsRekap.OBJEKTIF_VISUALANALOGSCORE = .Rows(iLoop)("OBJEKTIF_VISUALANALOGSCORE")
                    dsRekap.OBJEKTIF_BERATBADAN = .Rows(iLoop)("OBJEKTIF_BERATBADAN")
                    dsRekap.OBJEKTIF_TINGGIBADAN = .Rows(iLoop)("OBJEKTIF_TINGGIBADAN")
                    dsRekap.OBJEKTIF_SPO2 = .Rows(iLoop)("OBJEKTIF_SPO2")
                    dsRekap.OBJEKTIF_SISTOLE = .Rows(iLoop)("OBJEKTIF_SISTOLE")
                    dsRekap.OBJEKTIF_DIASTOLE = .Rows(iLoop)("OBJEKTIF_DIASTOLE")
                    dsRekap.OBJEKTIF_HR = .Rows(iLoop)("OBJEKTIF_HR")
                    dsRekap.OBJEKTIF_RR = .Rows(iLoop)("OBJEKTIF_RR")
                    dsRekap.OBJEKTIF_SUHU = .Rows(iLoop)("OBJEKTIF_SUHU")
                    dsRekap.OBJEKTIF_PEMERIKSAAN = .Rows(iLoop)("OBJEKTIF_PEMERIKSAAN")
                    dsRekap.OBJEKTIF_ALAMATGAMBARPEMERIKSAAN = .Rows(iLoop)("OBJEKTIF_ALAMATGAMBARPEMERIKSAAN")
                    dsRekap.OBJEKTIF_TEXT = .Rows(iLoop)("OBJEKTIF_TEXT")
                    dsRekap.ASSEMENT_INDIKASI = .Rows(iLoop)("ASSEMENT_INDIKASI")
                    dsRekap.ASSEMENT_TEXT = .Rows(iLoop)("ASSEMENT_TEXT")
                    dsRekap.PLANNING_ISTINDAKLANJUT_PULANG = .Rows(iLoop)("PLANNING_ISTINDAKLANJUT_PULANG")
                    dsRekap.PLANNING_ISTINDAKLANJUT_RAWAT = .Rows(iLoop)("PLANNING_ISTINDAKLANJUT_RAWAT")
                    dsRekap.PLANNING_ISTINDAKLANJUT_KONSUL = .Rows(iLoop)("PLANNING_ISTINDAKLANJUT_KONSUL")
                    dsRekap.PLANNING_ISTINDAKLANJUT_KONSUL_TEXT = .Rows(iLoop)("PLANNING_ISTINDAKLANJUT_KONSUL_TEXT")
                    dsRekap.PLANNING_ISTINDAKLANJUT_RUJUK = .Rows(iLoop)("PLANNING_ISTINDAKLANJUT_RUJUK")
                    dsRekap.PLANNING_ISTINDAKLANJUT_RUJUK_TEXT = .Rows(iLoop)("PLANNING_ISTINDAKLANJUT_RUJUK_TEXT")
                    dsRekap.PLANNING_ALASAN = .Rows(iLoop)("PLANNING_ALASAN")
                    dsRekap.PLANNING_TEXT = .Rows(iLoop)("PLANNING_TEXT")
                    dsRekap.CATATAN = .Rows(iLoop)("CATATAN")
                    dsRekap.KDUSER = .Rows(iLoop)("KDUSER")
                    dsRekap.ISDELETE = .Rows(iLoop)("ISDELETE")
                    dsRekap.DATEDELETE = .Rows(iLoop)("DATEDELETE")
                    dsRekap.USERDELETE = .Rows(iLoop)("USERDELETE")

                    listCPPT.Add(dsRekap)
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            sFind1_cppt = String.Empty
            sFind2_cppt = String.Empty
            sFind3_cppt = String.Empty

            If listCPPT.Count > 0 Then
                sFind1_cppt = sKDCUSTOMER
                sFind2_cppt = txtNAMAPASIEN.Text
                sFind3_cppt = Tanggallahir

                Dim FolderSimpan = "C:/SIMRS/CPPT/"

                If Not IO.Directory.Exists(FolderSimpan) Then
                    IO.Directory.CreateDirectory(FolderSimpan)
                Else
                    DeleteDirectory(FolderSimpan)
                    IO.Directory.CreateDirectory(FolderSimpan)
                End If

                Dim AlamatCPPT As String = FolderSimpan & sKDCUSTOMER & Now.ToString("yyyyMMddHHmmss") & ".pdf"

                Dim rpt As New xtraDigital_CPPT_01_QR

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.bindingSource.DataSource = listCPPT.OrderByDescending(Function(x) x.DATE)
                rpt.ExportToPdf(AlamatCPPT)

                If FileIO.FileSystem.FileExists(AlamatCPPT) Then
                    PdfViewerCPPTRawatJalan.LoadDocument(AlamatCPPT)
                End If

            End If

            sFind1_cppt = String.Empty
            sFind2_cppt = String.Empty
            sFind3_cppt = String.Empty

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Load Report CPPT" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
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
    Private Sub fn_LoadPdfViewerCPPTRAJALNAIKRANAP(ByVal RegisterRawatJalan As String, ByVal RM As String)
        Try
            PdfViewerCPPTRawatJalan.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data CPPT Naik Ranap.....")

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
            SQL &= "* "
            SQL &= ",PROFESI = ISNULL((SELECT MEMO FROM R_CPPT_M_PROFESI WHERE A.KDPROFESI = KDPROFESI), '') "
            SQL &= "FROM "
            SQL &= "R_CPPT A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDPENDAFTARAN = '" & IIf(RegisterRawatJalan = "", "KOSONGTRANSAKSI", RegisterRawatJalan) & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "HISTORY_CPPT")

            Dim listCPPT As New List(Of DataAccess.S_REQ_CPPT)
            Dim dataList As New List(Of Byte())
            Dim dsLoad() As Byte

            For iLoop As Integer = 0 To ds.Tables("HISTORY_CPPT").Rows.Count - 1
                Dim dsRekap As New DataAccess.S_REQ_CPPT
                With ds.Tables("HISTORY_CPPT")
                    dsRekap.DATECREATED = .Rows(iLoop)("DATECREATED")
                    dsRekap.DATEUPDATED = .Rows(iLoop)("DATEUPDATED")
                    dsRekap.DATE = .Rows(iLoop)("DATE")
                    dsRekap.KDCPPT = .Rows(iLoop)("KDCPPT")
                    dsRekap.KDCUSTOMER = .Rows(iLoop)("KDCUSTOMER")
                    dsRekap.KDPENDAFTARAN = .Rows(iLoop)("KDPENDAFTARAN")
                    dsRekap.PROFESI = .Rows(iLoop)("PROFESI")
                    dsRekap.NAMAPASIEN = .Rows(iLoop)("NAMAPASIEN")
                    dsRekap.JK = .Rows(iLoop)("JENISKELAMIN")
                    dsRekap.NIK = .Rows(iLoop)("NIK")
                    dsRekap.TEMPATLAHIR = ""
                    dsRekap.TANGGALLAHIR = .Rows(iLoop)("TANGGALLAHIR")
                    dsRekap.AGAMA = ""
                    dsRekap.PENJAMIN = .Rows(iLoop)("KDDAFTAR_L1_NAMA")
                    dsRekap.NOTELEPON = ""
                    dsRekap.SUKU = ""
                    dsRekap.ALAMAT = .Rows(iLoop)("ALAMAT")
                    dsRekap.SUBJEKTIF = .Rows(iLoop)("SUBJEKTIF_TEXT")
                    dsRekap.OBJEKTIF = .Rows(iLoop)("OBJEKTIF_TEXT")
                    dsRekap.ASSEMENT = .Rows(iLoop)("ASSEMENT_TEXT")
                    dsRekap.PLANNING = .Rows(iLoop)("PLANNING_TEXT")
                    dsRekap.KDUSER = .Rows(iLoop)("KDUSER")
                    dsRekap.ISCHEKED = True

                    listCPPT.Add(dsRekap)
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            'If listCPPT.Count > 0 Then
            '    Dim FolderSimpanrj = "C:/SIMRS/CPPTNAIKRANAP/"

            '    If Not Directory.Exists(FolderSimpanrj) Then
            '        Directory.CreateDirectory(FolderSimpanrj)
            '    Else
            '        DeleteDirectory(FolderSimpanrj)
            '        Directory.CreateDirectory(FolderSimpanrj)
            '    End If

            '    Dim AlamatCPPT As String = FolderSimpanrj & sKDREGRAWATJALAN & Now.ToString("yyyyMMddHHmmss") & ".pdf"

            '    Dim rpt As New xtraDigital_CPPT_01_RawatInap

            '    rpt.ShowPrintMarginsWarning = False
            '    rpt.Watermark.Text = sWATERMARK

            '    rpt.bindingSource.DataSource = listCPPT.OrderByDescending(Function(x) x.DATECREATED)
            '    rpt.ExportToPdf(AlamatCPPT)

            '    If FileIO.FileSystem.FileExists(AlamatCPPT) Then
            '        dataList.Add(File.ReadAllBytes(AlamatCPPT))
            '    End If
            'End If

            '
            Dim oCPPTRanap As New Transaksi.clsCPPT
            Dim dsRawat = oCPPTRanap.GetDataByRMRANAP(RM)

            Dim hasilUnion = dsRawat.Union(listCPPT).OrderByDescending(Function(x) x.DATECREATED)

            If hasilUnion.Count > 0 Then
                Dim FolderSimpan = "C:/CPPTRANAP/"

                If Not IO.Directory.Exists(FolderSimpan) Then
                    IO.Directory.CreateDirectory(FolderSimpan)
                Else
                    DeleteDirectory(FolderSimpan)
                    IO.Directory.CreateDirectory(FolderSimpan)
                End If

                Dim rpt As New xtraDigital_CPPT_01_RawatInap

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.bindingSource.DataSource = hasilUnion
                rpt.ExportToPdf(FolderSimpan & "HASIL" & ".pdf")
                dataList.Add(IO.File.ReadAllBytes(FolderSimpan & "HASIL" & ".pdf"))

            End If

            If dataList.Count > 0 Then
                dsLoad = MergeFilesByte(dataList)
                Dim stream As New IO.MemoryStream(dsLoad)
                PdfViewerCPPTRawatJalan.LoadDocument(stream)
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Load Report CPPT" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function MergeFilesByte(ByVal sourceFiles As List(Of Byte())) As Byte()
        Try
            Dim mergedPdf As Byte() = Nothing
            Using ms As New IO.MemoryStream()
                Using document As New Document()
                    Using copy As New PdfCopy(document, ms)
                        document.Open()
                        For i As Integer = 0 To sourceFiles.Count - 1
                            Dim reader As New PdfReader(sourceFiles(i))
                            ' loop over the pages in that document
                            Dim n As Integer = reader.NumberOfPages
                            Dim page As Integer = 0
                            While page < n
                                page = page + 1
                                copy.AddPage(copy.GetImportedPage(reader, page))
                            End While
                        Next
                    End Using
                End Using

                mergedPdf = ms.ToArray()

                Return mergedPdf

            End Using
        Catch ex As Exception
            MergeFilesByte = Nothing
            MsgBox("Load Merge Data : " & vbCrLf & ex.Message, MsgBoxStyle.Information, Me.Text)
        End Try
    End Function
    Private Function AppendArray(Of T)(ByVal thisArray() As T, ByVal itemToAppend As T) As T()
        If thisArray Is Nothing Then thisArray = New T() {}
        Dim tempList As List(Of T) = thisArray.ToList
        tempList.Add(itemToAppend)
        Return tempList.ToArray
    End Function
#End Region
#Region "EKlaim"
    Private Function fn_00NEWCLAIM() As Boolean
        Try
            fn_00NEWCLAIM = False

            Dim gender As String = String.Empty

            If txtJENISKELAMIN.Text = "L" Then
                gender = "1"
            Else
                gender = "2"
            End If

            Dim Request As String = "{" & """metadata"": {" & """method"": " & """new_claim""    }," & """data"": {" & """nomor_kartu"": """ & txtNoPeserta.Text & """, " & """nomor_sep"": """ & txtNoSEP.Text & """, " & """nomor_rm"": """ & CInt(txtNORM.Text).ToString.PadLeft(6, "0") & """, " & """nama_pasien"": """ & txtNAMAPASIEN.Text & """, " & """tgl_lahir"": """ & txtTGLLAHIR.Text & """, " & """gender"": """ & gender & """   } } "

            'Dim Request As String = "{" & """metadata"": {" & """method"": " & """new_claim""    }," & """data"": {" & """nomor_kartu"": """ & "123456" & """, " & """nomor_sep"": """ & txtNoSEP.Text & """, " & """nomor_rm"": """ & "000000" & """, " & """nama_pasien"": """ & "CONTOH2" & """, " & """tgl_lahir"": """ & "2008-09-12 08:00:00" & """, " & """gender"": """ & gender & """   } } "

            Dim Klaim As String = oGetGrouper.fn_BriggingEKlaim(sEklaim_Url, sEklaim_Generate, Request)

            If Klaim.Contains("ERORSIMRS") Then
                MsgBox(Klaim, MsgBoxStyle.Exclamation, Me.Text)
            Else
                Dim jsonDecode = JObject.Parse(Klaim)
                Dim sDataDuplicate As String = String.Empty
                Dim smessage As String = String.Empty

                sDataDuplicate = jsonDecode("metadata")("code").ToString
                smessage = jsonDecode("metadata")("message").ToString

                If sDataDuplicate = "200" Then
                    If fn_SaveStatus("fn_00NEWCLAIM", Request, jsonDecode.ToString) = False Then
                        MsgBox("Gagal Simpan Status Grouper", MsgBoxStyle.Exclamation, Me.Text)
                    Else
                        fn_00NEWCLAIM = True
                    End If
                Else
                    If smessage.Contains("Duplikasi nomor SEP") Then
                        If fn_SaveStatus("fn_00NEWCLAIM", Request, jsonDecode.ToString) = False Then
                            MsgBox("Gagal Simpan Status Grouper", MsgBoxStyle.Exclamation, Me.Text)
                        Else
                            fn_00NEWCLAIM = True
                        End If
                    Else
                        MsgBox("Membuat Klaim Baru" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                End If
            End If
        Catch oErr As Exception
            MsgBox("New Claim : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_01SETCLAIMDATA() As Boolean
        Try
            fn_01SETCLAIMDATA = False

            Dim jsonEncode As String = String.Empty

            jsonEncode = "{" & """metadata"": { " & """method"": " & """set_claim_data"",    " & """nomor_sep"": """ & txtNoSEP.Text & """  },  "
            jsonEncode &= """data"": {    " & """nomor_sep"": """ & txtNoSEP.Text & """,    "
            jsonEncode &= """nomor_kartu"": """ & txtNoPeserta.Text & """,    "
            jsonEncode &= """tgl_masuk"": """ & deDATEMASUK.DateTime.ToString("yyyy-MM-dd HH:mm:ss") & """,    "
            jsonEncode &= """tgl_pulang"": """ & deDATEPULANG.DateTime.ToString("yyyy-MM-dd HH:mm:ss") & """,    "
            jsonEncode &= """cara_masuk"": """ & grdCARAMASUK.EditValue & """,    "
            jsonEncode &= """jenis_rawat"": """ & IIf(rbCategory.SelectedIndex = 0, "2", "1") & """,    "
            jsonEncode &= """kelas_rawat"": """ & IIf(rbKELASHAK.SelectedIndex = 2, "1", IIf(rbKELASHAK.SelectedIndex = 1, "2", "3")) & """,    "
            jsonEncode &= """adl_sub_acute"": """ & IIf(txtADLScore_SubAcute.Text = "-", "", IIf(txtADLScore_SubAcute.Text = "0", "", txtADLScore_Chronic.Text)) & """,    "
            jsonEncode &= """adl_chronic"": """ & IIf(txtADLScore_Chronic.Text = "-", "", IIf(txtADLScore_Chronic.Text = "0", "", txtADLScore_Chronic.Text)) & """,    "
            jsonEncode &= """icu_indikator"": """ & IIf(txtRAWATINTENSIF_HARI.Text = 0, "0", "1") & """,    "
            jsonEncode &= """icu_los"": """ & txtRAWATINTENSIF_HARI.Text & """,    "
            jsonEncode &= """upgrade_class_ind"": """ & IIf(chkNaikKelas.Checked = True, "1", "0") & """,    "
            jsonEncode &= """add_payment_pct"": """ & "0" & """,    "
            jsonEncode &= """birth_weight"": """ & CInt(txtBeratBadan.Text) & """,    "
            jsonEncode &= """sistole"": """ & txtSISTOLE.Text & """,    "
            jsonEncode &= """diastole"": """ & txtDIASTOLE.Text & """,    "
            jsonEncode &= """discharge_status"": """ & grdCaraKeluar.EditValue & """,    "
            jsonEncode &= """tarif_rs"": {      " & """prosedur_non_bedah"": """ & CInt(txtProsedurNonBedah.Text) & """,      "
            jsonEncode &= """prosedur_bedah"": """ & CInt(txtProsedurBedah.Text) & """,      "
            jsonEncode &= """konsultasi"": """ & CInt(txtKonsultasi.Text) & """,      "
            jsonEncode &= """tenaga_ahli"": """ & CInt(txtTenagaAhli.Text) & """,      "
            jsonEncode &= """keperawatan"": """ & CInt(txtKeperawatan.Text) & """,      "
            jsonEncode &= """penunjang"": """ & CInt(txtPenunjang.Text) & """,      "
            jsonEncode &= """radiologi"": """ & CInt(txtRadiologi.Text) & """,      "
            jsonEncode &= """laboratorium"": """ & CInt(txtLaboratorium.Text) & """,      "
            jsonEncode &= """pelayanan_darah"": """ & CInt(txtPelayananDarah.Text) & """,      "
            jsonEncode &= """rehabilitasi"": """ & CInt(txtRehabilitasi.Text) & """,      "
            jsonEncode &= """kamar"": """ & CInt(txtKamarAkomodasi.Text) & """,      "
            jsonEncode &= """rawat_intensif"": """ & CInt(txtRawatIntensif.Text) & """,   "
            jsonEncode &= """obat"": """ & CInt(txtObat.Text) & """,   "
            jsonEncode &= """obat_kronis"": """ & CInt(txtObatKronis.Text) & """, "
            jsonEncode &= """obat_kemoterapi"": """ & CInt(txtObatKemoTerapi.Text) & """,      "
            jsonEncode &= """alkes"": """ & CInt(txtAlkes.Text) & """,      "
            jsonEncode &= """bmhp"": """ & CInt(txtBMHP.Text) & """,      "
            jsonEncode &= """sewa_alat"": """ & CInt(txtSewaAlat.Text) & """    },    "
            jsonEncode &= """pemulasaraan_jenazah"": """ & "0" & """,      "
            jsonEncode &= """kantong_jenazah"": """ & "0" & """,      "
            jsonEncode &= """peti_jenazah"": """ & "0" & """,      "
            jsonEncode &= """plastik_erat"": """ & "0" & """,      "
            jsonEncode &= """desinfektan_jenazah"": """ & "0" & """,      "
            jsonEncode &= """mobil_jenazah"": """ & "0" & """,      "
            jsonEncode &= """desinfektan_mobil_jenazah"": """ & "0" & """,      "
            jsonEncode &= """covid19_status_cd"": """ & "" & """,      "
            jsonEncode &= """nomor_kartu_t"": """ & "" & """,      "
            jsonEncode &= """episodes"": """ & "" & """,      "
            jsonEncode &= """akses_naat"": """ & "" & """,      "
            jsonEncode &= """isoman_ind"": """ & "0" & """,      "
            jsonEncode &= """bayi_lahir_status_cd"": """ & "" & """,      "
            jsonEncode &= """dializer_single_use"": """ & IIf(rbDIALIZERSINGGLEUSE.SelectedIndex = 0, "", IIf(rbDIALIZERSINGGLEUSE.SelectedIndex = 1, "0", "1")) & """,    "
            If txtKANTONGDARAH.Text <> "0" Then
                jsonEncode &= """kantong_darah"": """ & txtKANTONGDARAH.Text & """,    "
            Else
                jsonEncode &= """kantong_darah"": """ & "" & """,    "
            End If
            jsonEncode &= """alteplase_ind"": """ & cboALTEPLASE_IND.Text & """,    "
            jsonEncode &= """tarif_poli_eks"": """ & CInt(txtTarifBiayaTambahan.Text) & """,    "
            jsonEncode &= """nama_dokter"": """ & grdDPJP.Text & """,    "
            jsonEncode &= """kode_tarif"": """ & grdJenisTarif.EditValue & """,    "
            jsonEncode &= """payor_id"": """ & "3" & """,    "
            jsonEncode &= """payor_cd"": """ & "JKN" & """,    "
            jsonEncode &= """cob_cd"": """ & "" & """,    "
            jsonEncode &= """coder_nik"": """ & sUserKTP & """  } } "


            Dim Klaim As String = oGetGrouper.fn_BriggingEKlaim(sEklaim_Url, sEklaim_Generate, jsonEncode)

            If Klaim.Contains("ERORSIMRS") Then
                MsgBox(Klaim, MsgBoxStyle.Exclamation, Me.Text)
            Else
                Dim jsonDecode = JObject.Parse(Klaim)
                Dim sDataDuplicate = String.Empty
                Dim smessage As String = String.Empty

                sDataDuplicate = jsonDecode("metadata")("code").ToString
                smessage = jsonDecode("metadata")("message").ToString

                If sDataDuplicate = "200" Then
                    fn_01SETCLAIMDATA = True
                    'If fn_SaveStatus("fn_01SETCLAIMDATA", jsonEncode, jsonDecode.ToString) = False Then
                    '    MsgBox("Gagal Simpan Status Grouper", MsgBoxStyle.Exclamation, Me.Text)
                    'Else
                    '    fn_01SETCLAIMDATA = True
                    'End If
                Else
                    MsgBox(sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)

                    If smessage = "Nomor SEP tidak ditemukan" Then
                        If oStatusGrouper.DeleteData(sNoId) = True Then
                            fn_EmptyMe()
                            fn_StatusView()
                        End If
                    Else
                        If smessage = "iDRG coding sudah final" Then
                            MsgBox(sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
                            If fn_SaveStatus("fn_07FINALIDRG", jsonEncode, jsonDecode.ToString) = True Then
                                fn_StatusView()
                            End If
                        Else
                            MsgBox(sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
                        End If
                    End If
                End If
            End If
        Catch oErr As Exception
            MsgBox("Set Claim Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_02IDRGDIAGNOSASET() As Boolean
        Try
            fn_02IDRGDIAGNOSASET = False
            Dim list As New List(Of String)

            For Each xloop In listdiagnosakodeidRG
                list.Add(xloop.kddiagnosa.ToString.Trim)
            Next

            Dim jsonString As String = "{" & """metadata"": {" & """method"": ""idrg_diagnosa_set""," & """nomor_sep"": """ & txtNoSEP.Text & """" & "}," & """data"": {" & """diagnosa"": """ & String.Join("#", list.ToArray) & """" & "}" & "}"

            Dim Klaim As String = oGetGrouper.fn_BriggingEKlaim(sEklaim_Url, sEklaim_Generate, jsonString)

            If Klaim.Contains("ERORSIMRS") Then
                MsgBox(Klaim, MsgBoxStyle.Exclamation, Me.Text)
            Else
                Dim jsonDecode = JObject.Parse(Klaim)
                Dim sDataDuplicate As String = String.Empty
                Dim smessage As String = String.Empty

                sDataDuplicate = jsonDecode("metadata")("code").ToString
                smessage = jsonDecode("metadata")("message").ToString

                If sDataDuplicate = "200" Then
                    fn_02IDRGDIAGNOSASET = True

                    'If fn_SaveStatus("fn_02IDRGDIAGNOSASET", jsonString, jsonDecode.ToString) = False Then
                    '    MsgBox("Gagal Simpan Status Grouper", MsgBoxStyle.Exclamation, Me.Text)
                    'Else
                    '    fn_02IDRGDIAGNOSASET = True
                    'End If
                Else
                    MsgBox("Diagnosa Set" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If
        Catch oErr As Exception
            MsgBox("New Claim : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub fn_03IDRGDIAGNOSAGET()
        Try
            Dim jsonString As String = "{" & """metadata"": {" & """method"": ""idrg_diagnosa_get""}," & """data"": {" & """nomor_sep"": """ & txtNoSEP.Text & """" & "}" & "}"

            Dim Klaim As String = oGetGrouper.fn_BriggingEKlaim(sEklaim_Url, sEklaim_Generate, jsonString)

            If Klaim.Contains("ERORSIMRS") Then
                MsgBox(Klaim, MsgBoxStyle.Exclamation, Me.Text)
            Else
                Dim jsonDecode = JObject.Parse(Klaim)
                Dim sDataDuplicate As String = String.Empty
                Dim smessage As String = String.Empty

                sDataDuplicate = jsonDecode("metadata")("code").ToString
                smessage = jsonDecode("metadata")("message").ToString

                If sDataDuplicate = "200" Then
                    Dim sCek As Integer = 0

                    For Each item In jsonDecode("data")("expanded")
                        'grvDiagnosaiDRG.Focus()
                        'grvDiagnosaiDRG.AddNewRow()

                        'If item("no").ToString() = "1" Then
                        '    grvDiagnosaiDRG.SetFocusedRowCellValue(colkategoriiDRG, "Primer")
                        'Else
                        '    grvDiagnosaiDRG.SetFocusedRowCellValue(colkategoriiDRG, "Sekunder")
                        'End If

                        'grvDiagnosaiDRG.SetFocusedRowCellValue(colkddiagnosaiDRG, item("code").ToString())
                        'grvDiagnosaiDRG.SetFocusedRowCellValue(colmemoDiagnosaiDRG, item("display").ToString())
                        'grvDiagnosaiDRG.UpdateCurrentRow()

                        Dim dsRekap As New DataAccess.R_IDENTITAS_GROUPER_DATA_DIAGNOSAIDRG
                        dsRekap.datecreated = Now
                        dsRekap.dateupdated = Now
                        dsRekap.kodegrouper = sNoId
                        dsRekap.seq = sCek
                        dsRekap.kategori = IIf(item("no").ToString() = "1", "Primary", "Secondary")
                        dsRekap.kddiagnosa = item("code").ToString()
                        dsRekap.memo = IIf(item("no").ToString() = "1", item("display").ToString(), "    " & item("display").ToString())

                        sCek += 1

                        listdiagnosakodeidRG.Add(dsRekap)
                    Next

                    lblDiagnosaiDRG.ResetText()
                    lblDiagnosaiDRG_1.ResetText()
                    lblDiagnosaiDRG_2.ResetText()

                    Dim listkode As New List(Of String)
                    Dim listkode1 As New List(Of String)
                    Dim listkode2 As New List(Of String)

                    For Each xloop In listdiagnosakodeidRG.OrderBy(Function(x) x.seq)
                        listkode.Add(xloop.memo)
                        listkode1.Add(xloop.kddiagnosa)
                        listkode2.Add(xloop.kategori)
                    Next

                    lblDiagnosaiDRG.Text = String.Join(vbCrLf, listkode.ToArray)
                    lblDiagnosaiDRG_1.Text = String.Join(vbCrLf, listkode1.ToArray)
                    lblDiagnosaiDRG_2.Text = String.Join(vbCrLf, listkode2.ToArray)
                Else
                    MsgBox("Diagnosa Get" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If
        Catch oErr As Exception
            MsgBox("New Claim : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_04IDRGPROCEDURESET() As Boolean
        Try
            fn_04IDRGPROCEDURESET = False

            Dim list As New List(Of String)

            For Each xloop In listprosedurkodeidRG
                If xloop.jumlah > 1 Then
                    list.Add(xloop.kdprpsedur & "+" & xloop.jumlah)
                Else
                    list.Add(xloop.kdprpsedur)
                End If
            Next

            Dim jsonString As String = "{" & """metadata"": {" & """method"": ""idrg_procedure_set""," & """nomor_sep"": """ & txtNoSEP.Text & """" & "}," & """data"": {" & """procedure"": """ & IIf(list.Count > 0, String.Join("#", list.ToArray), "#") & """" & "}" & "}"

            Dim Klaim As String = oGetGrouper.fn_BriggingEKlaim(sEklaim_Url, sEklaim_Generate, jsonString)

            If Klaim.Contains("ERORSIMRS") Then
                MsgBox(Klaim, MsgBoxStyle.Exclamation, Me.Text)
            Else
                Dim jsonDecode = JObject.Parse(Klaim)
                Dim sDataDuplicate As String = String.Empty
                Dim smessage As String = String.Empty

                sDataDuplicate = jsonDecode("metadata")("code").ToString
                smessage = jsonDecode("metadata")("message").ToString

                If sDataDuplicate = "200" Then
                    fn_04IDRGPROCEDURESET = True
                Else
                    MsgBox("Membuat Prosedur Set" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If
        Catch oErr As Exception
            MsgBox("New Claim : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub fn_05IDRGPROCEDUREGET()
        Try
            Dim jsonString As String = "{" & """metadata"": {" & """method"": ""idrg_procedure_get""}," & """data"": {" & """nomor_sep"": """ & txtNoSEP.Text & """" & "}" & "}"

            Dim Klaim As String = oGetGrouper.fn_BriggingEKlaim(sEklaim_Url, sEklaim_Generate, jsonString)

            If Klaim.Contains("ERORSIMRS") Then
                MsgBox(Klaim, MsgBoxStyle.Exclamation, Me.Text)
            Else
                Dim jsonDecode = JObject.Parse(Klaim)
                Dim sDataDuplicate As String = String.Empty
                Dim smessage As String = String.Empty

                sDataDuplicate = jsonDecode("metadata")("code").ToString
                smessage = jsonDecode("metadata")("message").ToString

                If sDataDuplicate = "200" Then
                    lblProseduriDRG.ResetText()
                    lblProseduriDRG_1.ResetText()
                    lblProseduriDRG_2.ResetText()

                    Dim listkode As New List(Of String)
                    Dim listkode1 As New List(Of String)
                    Dim listkode2 As New List(Of String)

                    For Each item In jsonDecode("data")("expanded")
                        listkode.Add(item("display").ToString())
                        listkode1.Add(item("code").ToString())
                        If CDec(item("multiplicity").ToString()) > 1 Then
                            listkode2.Add(" x " & item("multiplicity").ToString())
                        Else
                            listkode2.Add("")
                        End If

                        Dim dsRekap As New DataAccess.R_IDENTITAS_GROUPER_DATA_PROSEDURIDRG
                        dsRekap.datecreated = Now
                        dsRekap.dateupdated = Now
                        dsRekap.kodegrouper = sNoId
                        dsRekap.seq = item("no").ToString()
                        dsRekap.jumlah = item("multiplicity").ToString()
                        dsRekap.kdprpsedur = item("code").ToString()
                        dsRekap.memo = item("display").ToString()

                        listprosedurkodeidRG.Add(dsRekap)
                    Next

                    lblProseduriDRG.Text = String.Join(vbCrLf, listkode.ToArray)
                    lblProseduriDRG_1.Text = String.Join(vbCrLf, listkode1.ToArray)
                    lblProseduriDRG_2.Text = String.Join(vbCrLf, listkode2.ToArray)
                Else
                    MsgBox("Prosedur Get" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If
        Catch oErr As Exception
            MsgBox("New Claim : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_06GROUPINGIDRG() As Boolean
        Try
            fn_06GROUPINGIDRG = False

            Dim jsonString As String = "{" & """metadata"": {" & """method"": ""grouper""," & """stage"": """ & "1" & """" & "," & """grouper"": """ & "idrg" & """" & "}," & """data"": {" & """nomor_sep"": """ & txtNoSEP.Text & """" & "}" & "}"

            Dim Klaim As String = oGetGrouper.fn_BriggingEKlaim(sEklaim_Url, sEklaim_Generate, jsonString)

            If Klaim.Contains("ERORSIMRS") Then
                MsgBox(Klaim, MsgBoxStyle.Exclamation, Me.Text)
            Else
                Dim jsonDecode = JObject.Parse(Klaim)
                Dim sDataDuplicate As String = String.Empty
                Dim smessage As String = String.Empty

                sDataDuplicate = jsonDecode("metadata")("code").ToString
                smessage = jsonDecode("metadata")("message").ToString

                If sDataDuplicate = "200" Then
                    If fn_SaveStatus("fn_06GROUPINGIDRG", jsonString, jsonDecode.ToString) = False Then
                        MsgBox("Gagal Simpan Status Grouper", MsgBoxStyle.Exclamation, Me.Text)
                    Else
                        fn_06GROUPINGIDRG = True

                        If fn_SaveStatusHasil(jsonDecode.ToString) = True Then
                            HasilIDRG(jsonDecode.ToString, "")
                            fn_StatusView()
                        End If
                    End If
                Else
                    MsgBox("Membuat Klaim Baru" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
                    If smessage = "Nomor SEP tidak ditemukan" Then
                        If oStatusGrouper.DeleteData(sNoId) = True Then
                            fn_EmptyMe()
                            fn_StatusView()
                        End If
                    End If
                End If
            End If
        Catch oErr As Exception
            MsgBox("New Claim : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_06GROUPINGIDRGSTAGE2(ByVal code As String) As Boolean
        Try
            fn_06GROUPINGIDRGSTAGE2 = False

            Dim jsonString As String = "{" & """metadata"": {" & """method"": ""grouper""," & """stage"": """ & "2" & """" & "," & """grouper"": """ & "idrg" & """" & "}," & """data"": {" & """nomor_sep"": """ & txtNoSEP.Text & """" & "," & """topup_codes"": """ & code & """" & "}" & "}"

            Dim Klaim As String = oGetGrouper.fn_BriggingEKlaim(sEklaim_Url, sEklaim_Generate, jsonString)

            If Klaim.Contains("ERORSIMRS") Then
                MsgBox(Klaim, MsgBoxStyle.Exclamation, Me.Text)
            Else
                Dim jsonDecode = JObject.Parse(Klaim)
                Dim sDataDuplicate As String = String.Empty
                Dim smessage As String = String.Empty

                sDataDuplicate = jsonDecode("metadata")("code").ToString
                smessage = jsonDecode("metadata")("message").ToString

                If sDataDuplicate = "200" Then
                    If fn_SaveStatus("fn_06GROUPINGIDRG", jsonString, jsonDecode.ToString) = False Then
                        MsgBox("Gagal Simpan Status Grouper", MsgBoxStyle.Exclamation, Me.Text)
                    Else
                        fn_06GROUPINGIDRGSTAGE2 = True
                        If fn_SaveStatusHasil(jsonDecode.ToString) = True Then
                            HasilIDRG(jsonDecode.ToString, code)
                        End If
                    End If
                Else
                    MsgBox("Membuat Klaim Baru" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
                    If smessage = "Nomor SEP tidak ditemukan" Then
                        If oStatusGrouper.DeleteData(sNoId) = True Then
                            fn_EmptyMe()
                            fn_StatusView()
                        End If
                    End If
                End If
            End If
        Catch oErr As Exception
            MsgBox("New Claim : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_07FINALIDRG() As Boolean
        Try
            fn_07FINALIDRG = False

            Dim jsonString As String = "{" & """metadata"": {" & """method"": ""idrg_grouper_final""}," & """data"": {" & """nomor_sep"": """ & txtNoSEP.Text & """" & "}" & "}"

            Dim Klaim As String = oGetGrouper.fn_BriggingEKlaim(sEklaim_Url, sEklaim_Generate, jsonString)

            If Klaim.Contains("ERORSIMRS") Then
                MsgBox(Klaim, MsgBoxStyle.Exclamation, Me.Text)
            Else
                Dim jsonDecode = JObject.Parse(Klaim)
                Dim sDataDuplicate As String = String.Empty
                Dim smessage As String = String.Empty

                sDataDuplicate = jsonDecode("metadata")("code").ToString
                smessage = jsonDecode("metadata")("message").ToString

                If sDataDuplicate = "200" Then
                    If fn_SaveStatus("fn_07FINALIDRG", jsonString, jsonDecode.ToString) = True Then
                        fn_07FINALIDRG = True
                    Else
                        MsgBox("Gagal Simpan Status Grouper", MsgBoxStyle.Exclamation, Me.Text)
                        fn_StatusView()
                    End If
                Else
                    MsgBox(sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)

                    If smessage = "iDRG coding sudah final" Then
                        If fn_SaveStatus("fn_07FINALIDRG", jsonString, jsonDecode.ToString) = True Then
                            MsgBox("Gagal Simpan Status Grouper", MsgBoxStyle.Exclamation, Me.Text)
                            fn_StatusView()
                        Else
                            fn_07FINALIDRG = True
                        End If
                    ElseIf smessage = "Nomor SEP tidak ditemukan" Then
                        If oStatusGrouper.DeleteData(sNoId) = True Then
                            fn_EmptyMe()
                            fn_StatusView()
                        End If
                    End If
                End If
            End If
        Catch oErr As Exception
            MsgBox("New Claim : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_08REEDIT() As Boolean
        Try
            fn_08REEDIT = False

            Dim jsonString As String = "{" & """metadata"": {" & """method"": ""idrg_grouper_reedit""}," & """data"": {" & """nomor_sep"": """ & txtNoSEP.Text & """" & "}" & "}"

            Dim Klaim As String = oGetGrouper.fn_BriggingEKlaim(sEklaim_Url, sEklaim_Generate, jsonString)

            If Klaim.Contains("ERORSIMRS") Then
                MsgBox(Klaim, MsgBoxStyle.Exclamation, Me.Text)
            Else
                Dim jsonDecode = JObject.Parse(Klaim)
                Dim sDataDuplicate As String = String.Empty
                Dim smessage As String = String.Empty

                sDataDuplicate = jsonDecode("metadata")("code").ToString
                smessage = jsonDecode("metadata")("message").ToString

                If sDataDuplicate = "200" Then
                    If fn_SaveStatus("fn_08REEDIT", jsonString, jsonDecode.ToString) = True Then
                        fn_08REEDIT = True
                    Else
                        MsgBox("Gagal Simpan Status Grouper", MsgBoxStyle.Exclamation, Me.Text)
                        fn_StatusView()
                    End If
                Else
                    MsgBox("Membuat Klaim Baru" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
                    If smessage = "iDRG coding belum final" Then
                        oStatusGrouper.UpdateStatus(sNoId, "fn_08REEDIT")
                        fn_StatusView()
                    ElseIf smessage = "Nomor SEP tidak ditemukan" Then
                        If oStatusGrouper.DeleteData(sNoId) = True Then
                            fn_EmptyMe()
                            fn_StatusView()
                        End If
                    End If
                End If
            End If
        Catch oErr As Exception
            MsgBox("New Claim : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub fn_09IDRGTOINACBGIMPORT()
        Try
            Dim jsonString As String = "{" & """metadata"": {" & """method"": ""idrg_to_inacbg_import""}," & """data"": {" & """nomor_sep"": """ & txtNoSEP.Text & """" & "}" & "}"

            Dim Klaim As String = oGetGrouper.fn_BriggingEKlaim(sEklaim_Url, sEklaim_Generate, jsonString)

            If Klaim.Contains("ERORSIMRS") Then
                MsgBox(Klaim, MsgBoxStyle.Exclamation, Me.Text)
            Else
                lblLabelHasilImport.ResetText()
                lblLabelHasilImportProsedur.ResetText()

                grvDiagnosa.OptionsSelection.MultiSelect = True
                grvDiagnosa.SelectAll()
                grvDiagnosa.DeleteSelectedRows()
                grvDiagnosa.OptionsSelection.MultiSelect = False

                grvProsedur.OptionsSelection.MultiSelect = True
                grvProsedur.SelectAll()
                grvProsedur.DeleteSelectedRows()
                grvProsedur.OptionsSelection.MultiSelect = False

                Dim jsonDecode = JObject.Parse(Klaim)
                Dim sDataDuplicate As String = String.Empty
                Dim smessage As String = String.Empty

                sDataDuplicate = jsonDecode("metadata")("code").ToString
                smessage = jsonDecode("metadata")("message").ToString

                If sDataDuplicate = "200" Then
                    Dim listHasilErorDiagnosa As New List(Of String)
                    Dim listHasilErorProsedur As New List(Of String)

                    Dim list As New List(Of String)

                    For Each item In jsonDecode("data")("diagnosa")("expanded")
                        Dim Eror As String = String.Empty

                        If item("metadata")("code") = "400" Then
                            Eror = item("metadata")("message")
                            listHasilErorDiagnosa.Add("ICD 10" & "(" & Eror & ") " & item("code").ToString())
                        End If

                        grvDiagnosa.Focus()
                        grvDiagnosa.AddNewRow()
                        grvDiagnosa.SetFocusedRowCellValue(colkategori, IIf(item("no").ToString() = "1", "Primer", "Sekunder"))
                        grvDiagnosa.SetFocusedRowCellValue(colkddiagnosa, item("code").ToString())
                        grvDiagnosa.SetFocusedRowCellValue(colmemodiagnosa, IIf(item("validcode").ToString() = "1", item("display").ToString(), "(" & Eror & ") " & item("display").ToString()))
                        grvDiagnosa.UpdateCurrentRow()

                        If item("validcode").ToString() = "1" Then
                            list.Add(item("code").ToString())
                        End If
                    Next

                    Dim listProsedur As New List(Of String)

                    For Each item In jsonDecode("data")("procedure")("expanded")
                        Dim Eror As String = String.Empty

                        If item("metadata")("code") = "400" Then
                            Eror = item("metadata")("message")
                            listHasilErorProsedur.Add("ICD 9" & "(" & Eror & ") " & item("code").ToString())
                        End If

                        grvProsedur.Focus()
                        grvProsedur.AddNewRow()
                        grvProsedur.SetFocusedRowCellValue(colkdprpsedur, item("code").ToString())
                        grvProsedur.SetFocusedRowCellValue(colmemoprosedur, IIf(item("validcode").ToString() = "1", item("display").ToString(), "(" & Eror & ") " & item("display").ToString()))
                        grvProsedur.UpdateCurrentRow()

                        If item("validcode").ToString() = "1" Then
                            listProsedur.Add(item("code").ToString())
                        End If
                    Next

                    If chkINA.Checked = True Then
                        txtKDDIAGNOSA_V6.Text = String.Join("#", list.ToArray)
                        txtKDPROSEDUR_V6.Text = String.Join("#", listProsedur.ToArray)
                    End If

                    If listHasilErorDiagnosa.Count > 0 Then
                        lblLabelHasilImport.Text = String.Join(vbCrLf, listHasilErorDiagnosa.ToArray)
                    End If
                    If listHasilErorProsedur.Count > 0 Then
                        lblLabelHasilImportProsedur.Text = String.Join(vbCrLf, listHasilErorProsedur.ToArray)
                    End If
                Else
                    MsgBox("Import" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If
        Catch oErr As Exception
            MsgBox("New Claim : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_10INACBGDIAGNOSAGET()
        Try
            Dim jsonString As String = "{" & """metadata"": {" & """method"": ""inacbg_diagnosa_get""}," & """data"": {" & """nomor_sep"": """ & txtNoSEP.Text & """" & "}" & "}"

            Dim Klaim As String = oGetGrouper.fn_BriggingEKlaim(sEklaim_Url, sEklaim_Generate, jsonString)

            If Klaim.Contains("ERORSIMRS") Then
                MsgBox(Klaim, MsgBoxStyle.Exclamation, Me.Text)
            Else
                lblLabelHasilImport.ResetText()

                grvDiagnosa.OptionsSelection.MultiSelect = True
                grvDiagnosa.SelectAll()
                grvDiagnosa.DeleteSelectedRows()
                grvDiagnosa.OptionsSelection.MultiSelect = False

                Dim jsonDecode = JObject.Parse(Klaim)
                Dim sDataDuplicate As String = String.Empty
                Dim smessage As String = String.Empty

                sDataDuplicate = jsonDecode("metadata")("code").ToString
                smessage = jsonDecode("metadata")("message").ToString

                If sDataDuplicate = "200" Then
                    Dim listHasilEror As New List(Of String)
                    Dim list As New List(Of String)

                    For Each item In jsonDecode("data")("expanded")
                        Dim Eror As String = String.Empty

                        If item("metadata")("code") = "400" Then
                            Eror = item("metadata")("message")
                            listHasilEror.Add("ICD 10" & "(" & Eror & ") " & item("code").ToString())
                        End If

                        grvDiagnosa.Focus()
                        grvDiagnosa.AddNewRow()
                        grvDiagnosa.SetFocusedRowCellValue(colkategori, IIf(item("no").ToString() = "1", "Primer", "Sekunder"))
                        grvDiagnosa.SetFocusedRowCellValue(colkddiagnosa, item("code").ToString())
                        grvDiagnosa.SetFocusedRowCellValue(colmemodiagnosa, IIf(item("validcode").ToString() = "1", item("display").ToString(), "(" & Eror & ") " & item("display").ToString()))
                        grvDiagnosa.UpdateCurrentRow()

                        list.Add(item("code").ToString())
                    Next

                    'If chkINA.Checked = True Then
                    '    txtKDDIAGNOSA_V6.Text = String.Join("#", list.ToArray)
                    'End If

                    If listHasilEror.Count > 0 Then
                        lblLabelHasilImport.Text = String.Join(vbCrLf, listHasilEror.ToArray)
                    End If
                Else
                    MsgBox("Diagnosa Incbg Get" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If
        Catch oErr As Exception
            MsgBox("New Claim : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_11INACBGDIAGNOSASET() As Boolean
        Try
            fn_11INACBGDIAGNOSASET = False

            Dim list As New List(Of String)

            For i As Integer = 0 To grvDiagnosa.RowCount - 2
                list.Add(grvDiagnosa.GetRowCellValue(i, colkddiagnosa))
            Next

            If list.Count > 0 Then
                Dim jsonString As String = "{" & """metadata"": {" & """method"": ""inacbg_diagnosa_set""," & """nomor_sep"": """ & txtNoSEP.Text & """" & "}," & """data"": {" & """diagnosa"": """ & String.Join("#", list.ToArray) & """" & "}" & "}"

                Dim Klaim As String = oGetGrouper.fn_BriggingEKlaim(sEklaim_Url, sEklaim_Generate, jsonString)

                If Klaim.Contains("ERORSIMRS") Then
                    MsgBox(Klaim, MsgBoxStyle.Exclamation, Me.Text)
                Else
                    Dim jsonDecode = JObject.Parse(Klaim)
                    Dim sDataDuplicate As String = String.Empty
                    Dim smessage As String = String.Empty

                    sDataDuplicate = jsonDecode("metadata")("code").ToString
                    smessage = jsonDecode("metadata")("message").ToString

                    If sDataDuplicate = "200" Then
                        fn_11INACBGDIAGNOSASET = True
                    Else
                        MsgBox("Diagnosa Icd Set" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                End If
            Else
                MsgBox("Diagnosa Masih Kosong", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox("New Claim fn_11INACBGDIAGNOSASET : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_12INACBGPROCEDURESET() As Boolean
        Try
            fn_12INACBGPROCEDURESET = False

            Dim list As New List(Of String)

            For i As Integer = 0 To grvProsedur.RowCount - 2
                list.Add(grvProsedur.GetRowCellValue(i, colkdprpsedur))
            Next

            Dim jsonString As String = "{" & """metadata"": {" & """method"": ""inacbg_procedure_set""," & """nomor_sep"": """ & txtNoSEP.Text & """" & "}," & """data"": {" & """procedure"": """ & IIf(list.Count > 0, String.Join("#", list.ToArray), "#") & """" & "}" & "}"

            Dim Klaim As String = oGetGrouper.fn_BriggingEKlaim(sEklaim_Url, sEklaim_Generate, jsonString)

            If Klaim.Contains("ERORSIMRS") Then
                MsgBox(Klaim, MsgBoxStyle.Exclamation, Me.Text)
            Else
                Dim jsonDecode = JObject.Parse(Klaim)
                Dim sDataDuplicate As String = String.Empty
                Dim smessage As String = String.Empty

                sDataDuplicate = jsonDecode("metadata")("code").ToString
                smessage = jsonDecode("metadata")("message").ToString

                If sDataDuplicate = "200" Then
                    fn_12INACBGPROCEDURESET = True
                Else
                    MsgBox("Procedure Icd Set" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If
        Catch oErr As Exception
            MsgBox("New Claim fn_11INACBGDIAGNOSASET : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub fn_13INACBGPROCEDUREGET()
        Try
            Dim jsonString As String = "{" & """metadata"": {" & """method"": ""inacbg_procedure_get""}," & """data"": {" & """nomor_sep"": """ & txtNoSEP.Text & """" & "}" & "}"

            Dim Klaim As String = oGetGrouper.fn_BriggingEKlaim(sEklaim_Url, sEklaim_Generate, jsonString)

            If Klaim.Contains("ERORSIMRS") Then
                MsgBox(Klaim, MsgBoxStyle.Exclamation, Me.Text)
            Else
                lblLabelHasilImportProsedur.ResetText()

                grvProsedur.OptionsSelection.MultiSelect = True
                grvProsedur.SelectAll()
                grvProsedur.DeleteSelectedRows()
                grvProsedur.OptionsSelection.MultiSelect = False

                Dim jsonDecode = JObject.Parse(Klaim)
                Dim sDataDuplicate As String = String.Empty
                Dim smessage As String = String.Empty

                sDataDuplicate = jsonDecode("metadata")("code").ToString
                smessage = jsonDecode("metadata")("message").ToString

                If sDataDuplicate = "200" Then
                    Dim listHasilEror As New List(Of String)
                    Dim listProsedur As New List(Of String)

                    For Each item In jsonDecode("data")("expanded")
                        Dim Eror As String = String.Empty

                        If item("metadata")("code") = "400" Then
                            Eror = item("metadata")("message")
                            listHasilEror.Add("ICD 9" & "(" & Eror & ") " & item("code").ToString())
                        End If

                        grvProsedur.Focus()
                        grvProsedur.AddNewRow()
                        grvProsedur.SetFocusedRowCellValue(colkdprpsedur, item("code").ToString())
                        grvProsedur.SetFocusedRowCellValue(colmemoprosedur, IIf(item("validcode").ToString() = "1", item("display").ToString(), "(" & Eror & ") " & item("display").ToString()))
                        grvProsedur.UpdateCurrentRow()

                        listProsedur.Add(item("code").ToString())
                    Next

                    'If chkINA.Checked = True Then
                    '    txtKDPROSEDUR_V6.Text = String.Join("#", listProsedur.ToArray)
                    'End If

                    If listHasilEror.Count > 0 Then
                        lblLabelHasilImportProsedur.Text = String.Join(vbCrLf, listHasilEror.ToArray)
                    End If
                Else
                    MsgBox("Prosedur Incbg Get" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If
        Catch oErr As Exception
            MsgBox("New Claim : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_14GROUPINGINACBGSTAGE1() As Boolean
        Try
            fn_14GROUPINGINACBGSTAGE1 = False

            Dim jsonString As String = "{" & """metadata"": {" & """method"": ""grouper""," & """stage"": """ & "1" & """" & "," & """grouper"": """ & "inacbg" & """" & "}," & """data"": {" & """nomor_sep"": """ & txtNoSEP.Text & """" & "}" & "}"

            Dim Klaim As String = oGetGrouper.fn_BriggingEKlaim(sEklaim_Url, sEklaim_Generate, jsonString)

            If Klaim.Contains("ERORSIMRS") Then
                MsgBox(Klaim, MsgBoxStyle.Exclamation, Me.Text)
            Else
                Dim jsonDecode = JObject.Parse(Klaim)
                Dim sDataDuplicate As String = String.Empty
                Dim smessage As String = String.Empty

                sDataDuplicate = jsonDecode("metadata")("code").ToString
                smessage = jsonDecode("metadata")("message").ToString

                txtTarifCBG.Text = 0
                txtTarifSubAcute.Text = 0
                txtTarifChronic.Text = 0
                txtTarifSpecialProcedure.Text = 0
                txtTarifSpecialProsthesis.Text = 0
                txtTarifSpecialInvestigation.Text = 0
                txtTarifSpecialDrug.Text = 0
                txtTarifTotal.Text = 0
                txtInfoInacbg.ResetText()
                txttJenisRawatInacbg.ResetText()
                txtGroupInacbg.ResetText()
                txtCodeCBG.Text = "-"
                txtDescriptionCBG.Text = "-"
                txtTarifCBG.Text = "-"

                If sDataDuplicate = "200" Then
                    If fn_SaveStatus("fn_14GROUPINGINACBGSTAGE1", jsonString, jsonDecode.ToString) = False Then
                        MsgBox("Gagal Simpan Status Grouper", MsgBoxStyle.Exclamation, Me.Text)
                    Else
                        Dim hasil As String = jsonDecode("response_inacbg").ToString

                        txtInfoInacbg.Text = "INACBG @" & Now.ToString("dd MMM yyyy HH:mm:ss") & " •• " & grdJenisTarif.Text & " •• "
                        txttJenisRawatInacbg.Text = IIf(rbCategory.SelectedIndex = 0, "Rawat Jalan Regular", "Rawat Inap Regular")
                        txtGroupInacbg.Text = jsonDecode("response_inacbg")("cbg")("description").ToString

                        Try
                            txtCodeCBG.Text = jsonDecode("response_inacbg")("cbg")("code").ToString
                            txtDescriptionCBG.Text = jsonDecode("response_inacbg")("cbg")("description").ToString
                            txtTarifCBG.Text = jsonDecode("response_inacbg")("tariff").ToString
                        Catch ex As Exception
                        End Try
                        Try
                            txtCodeSubAcute.Text = jsonDecode("response_inacbg")("sub_acute")("code").ToString
                            txtDescriptionSubAcute.Text = jsonDecode("response_inacbg")("sub_acute")("description").ToString
                            txtTarifSubAcute.Text = jsonDecode("response_inacbg")("sub_acute")("tariff").ToString
                        Catch ex As Exception
                        End Try
                        Try
                            txtCodeChronic.Text = jsonDecode("response_inacbg")("chronic")("code").ToString
                            txtDescriptionChronic.Text = jsonDecode("response_inacbg")("chronic")("description").ToString
                            txtTarifChronic.Text = jsonDecode("response_inacbg")("chronic")("tariff").ToString
                        Catch ex As Exception
                        End Try

                        If fn_Save("GROUPER", "normal", "") = False Then
                            MsgBox("Save gagal Ke SIMRS! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
                            Exit Function
                        Else
                            txtStatusInacbg.Text = "normal"
                            fn_StatusView()
                        End If

                        If jsonDecode("special_cmg_option") IsNot Nothing Then
                            Dim sPecialCMG As Boolean = False

                            Dim arrDetail = oRIdentitasGrouperData.GetStructureDetaiSpecialCMGlList
                            Dim sSeq As Integer = 0

                            For Each item In jsonDecode("special_cmg_option")
                                sPecialCMG = True

                                Dim dsDetail = oRIdentitasGrouperData.GetStructureDetailSpecialCMG

                                With dsDetail
                                    .datecreated = Now
                                    .dateupdated = Now
                                    .kodegrouper = sNoId
                                    .seq = sSeq
                                    .code = item("code")
                                    .description = item("description")
                                    .type = item("type")
                                    .kduser = sUserID
                                End With

                                arrDetail.Add(dsDetail)

                                sSeq += 1
                            Next

                            If arrDetail.Count > 0 Then
                                oRIdentitasGrouperData.DeleteDataSpecialCMG(sNoId)
                                oRIdentitasGrouperData.InsertDataSpecialCMG(arrDetail)
                            End If

                            fn_LoadDataSpecialCMG()

                            If sPecialCMG = True Then
                                fn_14GROUPINGINACBGSTAGE1 = True
                                MsgBox("Terdapat Spesial cmg !!!", MsgBoxStyle.Information, Me.Text)
                            Else
                                fn_14GROUPINGINACBGSTAGE1 = True
                            End If
                        Else
                            fn_14GROUPINGINACBGSTAGE1 = True
                        End If
                    End If
                Else
                    MsgBox("Membuat Klaim Baru" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
                    If smessage = "Nomor SEP tidak ditemukan" Then
                        If oStatusGrouper.DeleteData(sNoId) = True Then
                            fn_EmptyMe()
                            fn_StatusView()
                        End If
                    End If
                End If
            End If
        Catch oErr As Exception
            MsgBox("New Claim : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_15GROUPINGINACBGSTAGE2(ByVal type As String) As Boolean
        Try
            Dim listkode As New List(Of String)

            If txtSPECIAL1_CODE.Text <> "" Then
                listkode.Add(txtSPECIAL1_CODE.Text)
            End If
            If txtSPECIAL2_CODE.Text <> "" Then
                listkode.Add(txtSPECIAL2_CODE.Text)
            End If
            If txtSPECIAL3_CODE.Text <> "" Then
                listkode.Add(txtSPECIAL3_CODE.Text)
            End If
            If txtSPECIAL4_CODE.Text <> "" Then
                listkode.Add(txtSPECIAL4_CODE.Text)
            End If

            fn_15GROUPINGINACBGSTAGE2 = False

            Dim jsonString As String = "{" & """metadata"": {" & """method"": ""grouper""," & """stage"": """ & "2" & """" & "," & """grouper"": """ & "inacbg" & """" & "}," & """data"": {" & """nomor_sep"": """ & txtNoSEP.Text & """, " & """special_cmg"": """ & String.Join("#", listkode.ToArray) & """   } } "

            Dim Klaim As String = oGetGrouper.fn_BriggingEKlaim(sEklaim_Url, sEklaim_Generate, jsonString)

            If Klaim.Contains("ERORSIMRS") Then
                MsgBox(Klaim, MsgBoxStyle.Exclamation, Me.Text)
            Else
                Dim jsonDecode = JObject.Parse(Klaim)
                Dim sDataDuplicate As String = String.Empty
                Dim smessage As String = String.Empty

                sDataDuplicate = jsonDecode("metadata")("code").ToString
                smessage = jsonDecode("metadata")("message").ToString

                If sDataDuplicate = "200" Then
                    If fn_SaveStatus("fn_14GROUPINGINACBGSTAGE2", jsonString, jsonDecode.ToString) = False Then
                        MsgBox("Gagal Simpan Status Grouper", MsgBoxStyle.Exclamation, Me.Text)
                    Else
                        fn_15GROUPINGINACBGSTAGE2 = True

                        Dim tarif As Decimal = CDec(jsonDecode("response_inacbg")("base_tariff"))

                        For Each item In jsonDecode("special_cmg_option")
                            If item("type") = "Special Procedure" Then
                                If type = "Special Procedure" Then
                                    txtSPECIAL1_CODE.Text = item("code").ToString
                                    txtSPECIAL1_DESCRIPTION.Text = item("description").ToString
                                    txtTarifSpecialProcedure.Text = tarif
                                End If
                            ElseIf item("type") = "Special Prosthesis" Then
                                If type = "Special Prosthesis" Then
                                    txtSPECIAL2_CODE.Text = item("code").ToString
                                    txtSPECIAL2_DESCRIPTION.Text = item("description").ToString
                                    txtTarifSpecialProsthesis.Text = tarif
                                End If
                            ElseIf item("type") = "Special Investigation" Then
                                If type = "Special Investigation" Then
                                    txtSPECIAL3_CODE.Text = item("code").ToString
                                    txtSPECIAL3_DESCRIPTION.Text = item("description").ToString
                                    txtTarifSpecialInvestigation.Text = tarif
                                End If
                            ElseIf item("type") = "Special Drug" Then
                                If type = "Special Drug" Then
                                    txtSPECIAL4_CODE.Text = item("code").ToString
                                    txtSPECIAL4_DESCRIPTION.Text = item("description").ToString
                                    txtTarifSpecialDrug.Text = tarif
                                End If
                            End If
                        Next
                    End If
                Else
                    MsgBox("Membuat Klaim Baru" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
                    If smessage = "Nomor SEP tidak ditemukan" Then
                        If oStatusGrouper.DeleteData(sNoId) = True Then
                            fn_EmptyMe()
                            fn_StatusView()
                        End If
                    End If
                End If
            End If
        Catch oErr As Exception
            MsgBox("New Claim : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_16FINALINACBG() As Boolean
        Try
            fn_16FINALINACBG = False

            Dim jsonString As String = "{" & """metadata"": {" & """method"": ""inacbg_grouper_final""}," & """data"": {" & """nomor_sep"": """ & txtNoSEP.Text & """" & "}" & "}"

            Dim Klaim As String = oGetGrouper.fn_BriggingEKlaim(sEklaim_Url, sEklaim_Generate, jsonString)

            If Klaim.Contains("ERORSIMRS") Then
                MsgBox(Klaim, MsgBoxStyle.Exclamation, Me.Text)
            Else
                Dim jsonDecode = JObject.Parse(Klaim)
                Dim sDataDuplicate As String = String.Empty
                Dim smessage As String = String.Empty

                sDataDuplicate = jsonDecode("metadata")("code").ToString
                smessage = jsonDecode("metadata")("message").ToString

                If sDataDuplicate = "200" Then
                    If fn_SaveStatus("fn_16FINALINACBG", jsonString, jsonDecode.ToString) = True Then
                        fn_16FINALINACBG = True
                    Else
                        MsgBox("Gagal Simpan Status Grouper", MsgBoxStyle.Exclamation, Me.Text)
                        fn_StatusView()
                    End If
                Else
                    MsgBox(sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)

                    If smessage.Contains("coding sudah fina") Then
                        If fn_SaveStatus("fn_16FINALINACBG", jsonString, jsonDecode.ToString) = True Then
                            MsgBox("Gagal Simpan Status Grouper", MsgBoxStyle.Exclamation, Me.Text)
                            fn_StatusView()
                        Else
                            fn_16FINALINACBG = True
                        End If
                    ElseIf smessage = "Nomor SEP tidak ditemukan" Then
                        If oStatusGrouper.DeleteData(sNoId) = True Then
                            fn_EmptyMe()
                            fn_StatusView()
                        End If
                    End If
                End If
            End If
        Catch oErr As Exception
            MsgBox("New Claim : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_17REEDITINACBG() As Boolean
        Try
            fn_17REEDITINACBG = False

            Dim jsonString As String = "{" & """metadata"": {" & """method"": ""inacbg_grouper_reedit""}," & """data"": {" & """nomor_sep"": """ & txtNoSEP.Text & """" & "}" & "}"

            Dim Klaim As String = oGetGrouper.fn_BriggingEKlaim(sEklaim_Url, sEklaim_Generate, jsonString)

            If Klaim.Contains("ERORSIMRS") Then
                MsgBox(Klaim, MsgBoxStyle.Exclamation, Me.Text)
            Else
                Dim jsonDecode = JObject.Parse(Klaim)
                Dim sDataDuplicate As String = String.Empty
                Dim smessage As String = String.Empty

                sDataDuplicate = jsonDecode("metadata")("code").ToString
                smessage = jsonDecode("metadata")("message").ToString

                If sDataDuplicate = "200" Then
                    If fn_SaveStatus("fn_17REEDITINACBG", jsonString, jsonDecode.ToString) = True Then
                        fn_17REEDITINACBG = True
                    Else
                        MsgBox("Gagal Simpan Status Grouper", MsgBoxStyle.Exclamation, Me.Text)
                        fn_StatusView()
                    End If
                Else
                    MsgBox(sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)

                    If smessage = "Nomor SEP tidak ditemukan" Then
                        If oStatusGrouper.DeleteData(sNoId) = True Then
                            fn_EmptyMe()
                            fn_StatusView()
                        End If
                    ElseIf smessage = "INACBG coding belum final" Then
                        If fn_SaveStatus("fn_14GROUPINGINACBGSTAGE1", jsonString, jsonDecode.ToString) = True Then
                            fn_17REEDITINACBG = False
                            fn_StatusView()
                        Else
                            MsgBox("Gagal Simpan Status Grouper", MsgBoxStyle.Exclamation, Me.Text)
                            fn_StatusView()
                        End If
                    ElseIf smessage = "Klaim sudah final" Then
                        If fn_SaveStatus("fn_16FINALINACBG", jsonString, jsonDecode.ToString) = True Then
                            fn_17REEDITINACBG = False
                            fn_StatusView()
                        Else
                            MsgBox("Gagal Simpan Status Grouper", MsgBoxStyle.Exclamation, Me.Text)
                            fn_StatusView()
                        End If
                    End If
                End If
            End If
        Catch oErr As Exception
            MsgBox("New Claim : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_18CLAIMFINAL() As Boolean
        Try
            fn_18CLAIMFINAL = False

            Dim jsonString As String = "{" & """metadata"": {" & """method"": " & """claim_final""    }," & """data"": {" & """nomor_sep"": """ & txtNoSEP.Text & """, " & """coder_nik"": """ & sUserKTP & """   } } "

            Dim Klaim As String = oGetGrouper.fn_BriggingEKlaim(sEklaim_Url, sEklaim_Generate, jsonString)

            If Klaim.Contains("ERORSIMRS") Then
                MsgBox(Klaim, MsgBoxStyle.Exclamation, Me.Text)
            Else
                Dim jsonDecode = JObject.Parse(Klaim)
                Dim sDataDuplicate As String = String.Empty
                Dim smessage As String = String.Empty

                sDataDuplicate = jsonDecode("metadata")("code").ToString
                smessage = jsonDecode("metadata")("message").ToString

                If sDataDuplicate = "200" Then
                    If fn_SaveStatus("fn_18CLAIMFINAL", jsonString, jsonDecode.ToString) = True Then
                        fn_18CLAIMFINAL = True
                    Else
                        MsgBox("Gagal Simpan Status Grouper", MsgBoxStyle.Exclamation, Me.Text)
                        fn_StatusView()
                    End If
                Else
                    MsgBox(sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)

                    If smessage = "Nomor SEP tidak ditemukan" Then
                        If oStatusGrouper.DeleteData(sNoId) = True Then
                            fn_EmptyMe()
                            fn_StatusView()
                        End If
                    End If
                End If
            End If
        Catch oErr As Exception
            MsgBox("New Claim : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_19CLAIMREEDIT() As Boolean
        Try
            fn_19CLAIMREEDIT = False

            Dim jsonString As String = "{" & """metadata"": {" & """method"": " & """reedit_claim""    }," & """data"": {" & """nomor_sep"": """ & txtNoSEP.Text & """} } "

            Dim Klaim As String = oGetGrouper.fn_BriggingEKlaim(sEklaim_Url, sEklaim_Generate, jsonString)

            If Klaim.Contains("ERORSIMRS") Then
                MsgBox(Klaim, MsgBoxStyle.Exclamation, Me.Text)
            Else
                Dim jsonDecode = JObject.Parse(Klaim)
                Dim sDataDuplicate As String = String.Empty
                Dim smessage As String = String.Empty

                sDataDuplicate = jsonDecode("metadata")("code").ToString
                smessage = jsonDecode("metadata")("message").ToString

                If sDataDuplicate = "200" Then
                    If fn_SaveStatus("fn_19CLAIMREEDIT", jsonString, jsonDecode.ToString) = True Then
                        fn_19CLAIMREEDIT = True
                    Else
                        MsgBox("Gagal Simpan Status Grouper", MsgBoxStyle.Exclamation, Me.Text)
                        fn_StatusView()
                    End If
                Else

                    If smessage = "Nomor SEP tidak ditemukan" Then
                        If oStatusGrouper.DeleteData(sNoId) = True Then
                            fn_EmptyMe()
                            fn_StatusView()
                        End If
                    ElseIf smessage = "Klaim belum final"
                        If fn_SaveStatus("fn_19CLAIMREEDIT", jsonString, jsonDecode.ToString) = True Then
                            fn_19CLAIMREEDIT = True
                        Else
                            MsgBox("Gagal Simpan Status Grouper", MsgBoxStyle.Exclamation, Me.Text)
                            fn_StatusView()
                        End If
                    Else
                        MsgBox(sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)

                    End If
                End If
            End If
        Catch oErr As Exception
            MsgBox("New Claim : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_20CLAIMSEND() As Boolean
        Try
            fn_20CLAIMSEND = False

            Dim jsonString As String = "{" & """metadata"": {" & """method"": " & """send_claim_individual""    }," & """data"": {" & """nomor_sep"": """ & txtNoSEP.Text & """} } "

            Dim Klaim As String = oGetGrouper.fn_BriggingEKlaim(sEklaim_Url, sEklaim_Generate, jsonString)

            If Klaim.Contains("ERORSIMRS") Then
                MsgBox(Klaim, MsgBoxStyle.Exclamation, Me.Text)
            Else
                Dim jsonDecode = JObject.Parse(Klaim)
                Dim sDataDuplicate As String = String.Empty
                Dim smessage As String = String.Empty

                sDataDuplicate = jsonDecode("metadata")("code").ToString
                smessage = jsonDecode("metadata")("message").ToString

                If sDataDuplicate = "200" Then
                    If fn_SaveStatus("fn_20CLAIMSEND", jsonString, jsonDecode.ToString) = True Then
                        fn_20CLAIMSEND = True
                    Else
                        MsgBox("Gagal Simpan Status Grouper", MsgBoxStyle.Exclamation, Me.Text)
                        fn_StatusView()
                    End If
                Else
                    MsgBox(sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)

                    If smessage = "Nomor SEP tidak ditemukan" Then
                        If oStatusGrouper.DeleteData(sNoId) = True Then
                            fn_EmptyMe()
                            fn_StatusView()
                        End If
                    End If
                End If
            End If
        Catch oErr As Exception
            MsgBox("New Claim : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
#End Region
#Region "Command Button"
    'Private Sub DeleteToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem3.Click
    '    grvDiagnosaiDRG.DeleteSelectedRows()
    'End Sub
    'Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
    '    grvProseduriDRG.DeleteSelectedRows()
    'End Sub
    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        grvDiagnosa.DeleteSelectedRows()
    End Sub
    Private Sub DeleteToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem1.Click
        grvProsedur.DeleteSelectedRows()
    End Sub
    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        grvPersalinan.DeleteSelectedRows()
    End Sub
    Private Sub frmItem_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClose_Click()
                'Case Keys.F3
                '    If btnSaveClose.Enabled = True Then
                '        btnSaveClose_Click()
                '    End If
                'Case Keys.F5
                '    btnFocusICD10_Click()
                'Case Keys.F6
                '    btnFocusICD9_Click()
        End Select
    End Sub
    Private Sub btnFocusICD10_Click() Handles btnFocusICD10.ItemClick
        grdCariDiagnosa.Focus()
    End Sub
    Private Sub btnFocusICD9_Click() Handles btnFocusICD9.ItemClick
        grdCariProsedur.Focus()
    End Sub
    'Private Sub btnSaveSIMRS_Click() Handles btnSaveSIMRS.ItemClick
    '    If fn_Validate() = False Then Exit Sub
    '    If MsgBox("Data akan di Simpan Tanpa di Grouper?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
    '    If fn_Save("SAVE SIMRS", "", "") = False Then
    '        MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
    '    Else
    '        MsgBox("Save " & txtNoSEP.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
    '        Me.Close()
    '    End If
    'End Sub
    'Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
    '    Dim list As New List(Of String)

    '    If grdKDSPECIAL1.Text <> "" Then
    '        list.Add(grdKDSPECIAL1.EditValue)
    '    End If
    '    If grdKDSPECIAL2.Text <> "" Then
    '        list.Add(grdKDSPECIAL2.EditValue)
    '    End If
    '    If grdKDSPECIAL3.Text <> "" Then
    '        list.Add(grdKDSPECIAL3.EditValue)
    '    End If
    '    If grdKDSPECIAL4.Text <> "" Then
    '        list.Add(grdKDSPECIAL4.EditValue)
    '    End If

    '    If fn_Validate() = False Then Exit Sub
    '    If MsgBox("Data akan di Simpan Grouper?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

    '    If list.Count > 0 Then
    '        If fn_FinalisasiKlaim(True) = True Then
    '            If fn_Save("FINAL KLAIM", "", "") = False Then
    '                MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
    '            Else
    '                MsgBox("Save " & txtNoSEP.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
    '                Me.Close()
    '            End If
    '        Else
    '            MsgBox("Final Klaim Gagal", MsgBoxStyle.Exclamation, Me.Text)
    '        End If
    '    Else
    '        'If fn_Membuatklaimbaru(txtJENISKELAMIN.Text, txtNoPeserta.Text, txtNoSEP.Text, CInt(txtNORM.Text).ToString.PadLeft(6, "0"), txtNAMAPASIEN.Text, txtTGLLAHIR.Text) = True Then
    '        '    If fn_MengisiUpdateDataKlaimKlaim() = True Then
    '        '        If fn_GroupingStage() = True Then
    '        '            If fn_FinalisasiKlaim(True) = True Then
    '        '                If fn_Save("FINAL KLAIM") = False Then
    '        '                    MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
    '        '                Else
    '        '                    MsgBox("Save " & txtNoSEP.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
    '        '                    Me.Close()
    '        '                End If
    '        '            Else
    '        '                MsgBox("Final Klaim Gagal", MsgBoxStyle.Exclamation, Me.Text)
    '        '            End If
    '        '        Else
    '        '            MsgBox("Grouping Gagal", MsgBoxStyle.Exclamation, Me.Text)
    '        '        End If
    '        '    End If
    '        'End If
    '    End If
    'End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
    Private Sub btnKlaimBarudanUpdateData_Click(sender As Object, e As EventArgs) Handles btnKlaimBarudanUpdateData.Click
        If fn_00NEWCLAIM() = True Then
            fn_StatusView()
        End If
    End Sub
    Private Sub btnGroupingiDRG_Click(sender As Object, e As EventArgs) Handles btnGroupingiDRG.Click
        If CDec(txttarifRumahSakit.Text) > 0 Then
            If listdiagnosakodeidRG.Count > 0 Then
                If fn_01SETCLAIMDATA() = True Then
                    If fn_02IDRGDIAGNOSASET() = True Then
                        If fn_04IDRGPROCEDURESET() = True Then
                            If fn_06GROUPINGIDRG() = True Then

                            End If
                        End If
                    End If
                End If
            Else
                MsgBox("Diagnosa iDRG Masih Kosong", MsgBoxStyle.Information, Me.Text)
            End If
        Else
            MsgBox("Tarif Rumah Sakit Belum Sesuai!", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub btnFinaliDRG_Click(sender As Object, e As EventArgs) Handles btnFinaliDRG.Click
        If btnFinaliDRG.Text = "Final iDRG" Then
            If fn_07FINALIDRG() = True Then
                fn_StatusView()
            End If
        Else
            If fn_08REEDIT() = True Then
                grvDiagnosa.OptionsSelection.MultiSelect = True
                grvDiagnosa.SelectAll()
                grvDiagnosa.DeleteSelectedRows()
                grvDiagnosa.OptionsSelection.MultiSelect = False

                grvProsedur.OptionsSelection.MultiSelect = True
                grvProsedur.SelectAll()
                grvProsedur.DeleteSelectedRows()
                grvProsedur.OptionsSelection.MultiSelect = False

                txtKDDIAGNOSA_V6.ResetText()
                txtKDPROSEDUR_V6.ResetText()

                Try
                    If oRIdentitasGrouperData.DeleteData(sNoId) = True Then
                        oFormMode = FORM_MODE.FORM_MODE_ADD
                        fn_StatusView()
                    End If
                Catch oErr As Exception
                    MsgBox("Hapus Data: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        End If
    End Sub
    'Private Sub SimpleButton2_Click(sender As Object, e As EventArgs)
    '    If fn_08REEDIT() = True Then
    '        fn_StatusView()
    '    End If
    'End Sub
    Private Sub btnGrouper_Click(sender As Object, e As EventArgs) Handles btnGrouper.Click
        If fn_11INACBGDIAGNOSASET() = True Then
            If fn_12INACBGPROCEDURESET() = True Then
                If fn_14GROUPINGINACBGSTAGE1() = True Then

                End If
            End If
        End If
    End Sub
    Private Sub btnHapusKlaim_Click(sender As Object, e As EventArgs) Handles btnHapusKlaim.Click
        If MsgBox("Apakah Akan Hapus Klaim ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        Try
            Dim jsonDecode = JObject.Parse(oGetGrouper.fn_UntukMenghapusKlaim(sEklaim_Url, sEklaim_Generate, txtNoSEP.Text, sUserKTP))
            Dim sDataDuplicate As String = String.Empty
            Dim smessage As String = String.Empty

            sDataDuplicate = jsonDecode("metadata")("code").ToString
            smessage = jsonDecode("metadata")("message").ToString

            If sDataDuplicate = "200" Then
                If oStatusGrouper.DeleteData(sNoId) = False Then
                    MsgBox("Gagal Simpan Status Grouper", MsgBoxStyle.Exclamation, Me.Text)
                Else
                    oRIdentitasGrouperData.UpdateDataMEMO(sNoId, "HAPUS KLAIM")
                    fn_EmptyMe()
                    fn_StatusView()
                End If
            Else
                MsgBox("Gagal Hapus Klaim" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)

                If smessage = "Nomor SEP tidak ditemukan" Then
                    If oStatusGrouper.DeleteData(sNoId) = True Then
                        fn_StatusView()
                    End If
                End If
            End If
        Catch oErr As Exception
            MsgBox("Untuk Menghapus Klaim: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnKirimOnlineKlaim_Click(sender As Object, e As EventArgs) Handles btnKirimOnlineKlaim.Click
        If fn_20CLAIMSEND() = True Then
            If fn_Save("KIRIM ONLINE", "final", "Terkirim") = False Then
                MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
            Else
                txtMEMO.Text = "KIRIM ONLINE"
                txtSTATUS_DCKEMENKES.Text = "Terkirim"
                fn_StatusView()
            End If
        End If
    End Sub
    Private Sub btnUpdateKlaimINACBG_Click(sender As Object, e As EventArgs) Handles btnUpdateKlaimINACBG.Click
        If fn_17REEDITINACBG() = True Then
            If fn_Save("EDIT ULANG INACBG", "normal", "") = False Then
                MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
            Else
                txtStatusInacbg.Text = "normal"
                fn_StatusView()
            End If
        End If
    End Sub
    Private Sub btnUpdateKlaim_Click(sender As Object, e As EventArgs) Handles btnUpdateKlaim.Click
        If fn_19CLAIMREEDIT() = True Then
            If fn_Save("EDIT ULANG KLAIM", "normal", "") = False Then
                MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
            Else
                txtStatusInacbg.Text = "normal"
                fn_StatusView()
            End If
        End If
    End Sub
    Private Sub btnFinalKlaimINCABG_Click(sender As Object, e As EventArgs) Handles btnFinalKlaimINCABG.Click
        If fn_16FINALINACBG() = True Then
            If fn_Save("FINAL INACBG", "final", "") = False Then
                MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
            Else
                txtStatusInacbg.Text = "final"
                fn_StatusView()
            End If
        End If
    End Sub
    Private Sub btnFinalKlaim_Click(sender As Object, e As EventArgs) Handles btnFinalKlaim.Click
        If fn_18CLAIMFINAL() = True Then
            If fn_Save("FINAL KLAIM", "final", "Klaim belum terkirim ke Pusat Data Kementerian Kesehatan") = False Then
                MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
            Else
                txtMEMO.Text = "FINAL KLAIM"
                txtSTATUS_DCKEMENKES.Text = "Klaim belum terkirim ke Pusat Data Kementerian Kesehatan"
                fn_StatusView()
            End If
        End If
    End Sub
    Private Sub btnCetakKlaim_Click(sender As Object, e As EventArgs) Handles btnCetakKlaim.Click
        Try
            'Try
            '    If Not IO.Directory.Exists("C:/SIMRS/") Then
            '        IO.Directory.CreateDirectory("C:/SIMRS/")
            '    End If

            'Catch ex As Exception

            'End Try

            'Dim jsonDecode = JObject.Parse(oGetGrouper.fn_Cetakklaim(sEklaim_Url, sEklaim_Generate, txtNoSEP.Text))
            'Dim sDataDuplicate As String = String.Empty
            'Dim smessage As String = String.Empty
            'Dim sDataPasien As String = String.Empty

            'sDataDuplicate = jsonDecode("metadata")("code").ToString
            'smessage = jsonDecode("metadata")("message").ToString

            'If sDataDuplicate = "200" Then
            '    sDataPasien = jsonDecode("data").ToString

            '    If Not IO.Directory.Exists("C:/SIMRS/" & DateTime.Now.ToString("yyyyMMdd") & "") Then
            '        IO.Directory.CreateDirectory("C:/SIMRS/" & DateTime.Now.ToString("yyyyMMdd") & "")
            '    End If

            '    Dim str As String = sDataPasien
            '    Dim Base64Byte() As Byte = Convert.FromBase64String(str)
            '    Dim obj As IO.FileStream = IO.File.Create("C:\SIMRS\" & DateTime.Now.ToString("yyyyMMdd") & "\" & txtNoSEP.Text & ".pdf")
            '    obj.Write(Base64Byte, 0, Base64Byte.Length)
            '    'pdfPremViewer.LoadFile("C:\users\steve\desktop\test.pdf")
            '    obj.Flush()
            '    obj.Close()

            '    Dim psi As New ProcessStartInfo

            '    psi.UseShellExecute = True

            '    psi.Verb = "print"

            '    psi.WindowStyle = ProcessWindowStyle.Hidden

            '    'psi.Arguments = PrintDialog1.PrinterSettings.PrinterName.ToString()

            '    psi.FileName = "C:\SIMRS\" & DateTime.Now.ToString("yyyyMMdd") & "\" & txtNoSEP.Text & ".pdf" ' Here specify a document to be printed

            '    Process.Start(psi)
            'Else
            '    MsgBox("Cetak Klaim -8" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
            'End If

            Dim FolderSimpan As String = "C:/SIMRS/"
            Dim alamatsep As String = FolderSimpan & "1" & txtNoSEP.Text & Now.ToString("yyyyMMdd HHmmss") & ".pdf"

            Try
                If Not IO.Directory.Exists("C:/SIMRS/") Then
                    IO.Directory.CreateDirectory("C:/SIMRS/")
                End If

            Catch ex As Exception

            End Try

            If txtNoSEP.Text <> "" Then
                'Dim dataList As New List(Of Byte())
                'Dim dsLoad() As Byte
                '--------------------1 CETAK LIP KLAIM

                Dim Cetak As String = fn_Cetakklaim(txtNoSEP.Text)

                If Cetak <> "" Then
                    'Dim Base64Byte() As Byte
                    'Base64Byte = Convert.FromBase64String(Cetak)
                    'dataList.Add(Base64Byte)

                    ' Asumsi: Cetak adalah string Base64 dari file PDF
                    Dim pdfBytes As Byte() = Convert.FromBase64String(Cetak)

                    ' Simpan ke file PDF
                    Dim outputPath As String = alamatsep
                    IO.File.WriteAllBytes(outputPath, pdfBytes)

                    ' Buka file dengan default PDF viewer
                    'Process.Start(outputPath)

                    If FileIO.FileSystem.FileExists(alamatsep) Then
                        Dim frmPopPDF As New frmPopPDF
                        Try
                            frmPopPDF.fn_LoadMe(alamatsep)
                            frmPopPDF.ShowDialog(Me)
                        Catch oErr As Exception
                            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                        Finally
                            If Not frmPopPDF Is Nothing Then frmCustomer.Dispose()
                            frmPopPDF = Nothing
                        End Try
                    End If

                End If

                'If alamatsep <> "" Then
                '    If FileIO.FileSystem.FileExists(alamatsep) Then
                '        dataList.Add(IO.File.ReadAllBytes(alamatsep))
                '    End If
                'End If

                'If dataList.Count > 0 Then
                '    dsLoad = MergeFilesByte(dataList)
                '    Dim stream As New IO.MemoryStream(dsLoad)
                '    If NotUpload = True Then
                '        PdfViewer1.LoadDocument(stream)
                '    Else
                '        IO.File.WriteAllBytes(FolderSimpanPDF & "\" & If(dsData.noSep <> "", dsData.noSep, dsData.norec) & ".pdf", dsLoad)
                '    End If
                'End If
            Else
                MsgBox("Nomor Sep Kosong, Silahkan Lakukan Update Nomor SEP", MsgBoxStyle.Critical, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox("Update Ulang: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Cetakklaim(ByVal NOSEP As String) As String
        Try
            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Processing data LIP .....")

            Dim jsonDecode = JObject.Parse(oGetGrouper.fn_Cetakklaim(sEklaim_Url, sEklaim_Generate, NOSEP))
            Dim sDataDuplicate As String = String.Empty
            Dim smessage As String = String.Empty

            sDataDuplicate = jsonDecode("metadata")("code").ToString
            smessage = jsonDecode("metadata")("message").ToString

            If sDataDuplicate = "200" Then
                SplashScreenManager.CloseForm(False)
                fn_Cetakklaim = jsonDecode("data").ToString
            Else
                fn_Cetakklaim = ""
                SplashScreenManager.CloseForm(False)
                MsgBox("Cetak Klaim : " & sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            fn_Cetakklaim = ""
            MsgBox("Cetak Klaim : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub rbCategory_SelectedIndexChanged() Handles rbCategory.SelectedIndexChanged
        If rbCategory.SelectedIndex = 0 Then
            lKELASEKSEKUTIF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lNAIKKELAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lADARAWAT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lKELASHAKRJ.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lKELASHAKRI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            If chkKelasEksekutif.Checked = False Then
                lTARIFEKSEKUTIF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                    txtTarifEksekutif.Text = "0"
                End If
            Else
                lTARIFEKSEKUTIF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
            LKELASPELAYANAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lLAMA.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lRAWATINTENSIF_HARI_TEXT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lVENTILATOR_TEXT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                chkNaikKelas.Checked = False
                chkAdaRawat.Checked = False
                rbKELASHAK.SelectedIndex = 0
                rbKELASPELAYANAN.SelectedIndex = 0
                txtLAMA.Text = 0
                txtRAWATINTENSIF_HARI.Text = 0
                txtVENTILATOR.Text = 0
            End If
        Else
            lKELASEKSEKUTIF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lNAIKKELAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lADARAWAT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lKELASHAKRJ.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lKELASHAKRI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lTARIFEKSEKUTIF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            If chkNaikKelas.Checked = False Then
                lLAMA.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                LKELASPELAYANAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                    txtLAMA.Text = "0"
                    rbKELASPELAYANAN.SelectedIndex = 0
                End If
            Else
                lLAMA.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                LKELASPELAYANAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
            If chkAdaRawat.Checked = False Then
                lVentilator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lRAWATINTENSIF_HARI_TEXT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lVENTILATOR_TEXT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                    txtRAWATINTENSIF_HARI.Text = 0
                    txtVENTILATOR.Text = 0
                End If
            Else
                lVentilator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lRAWATINTENSIF_HARI_TEXT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lVENTILATOR_TEXT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                txtTarifEksekutif.Text = "0"
                chkKelasEksekutif.Checked = False
            End If
        End If

        If txtNOMORSITB.Text <> "" Then
            If chkPasienTB.Checked = False Then
                lNOMORSITB.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lVALIDASITB.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Else
                lNOMORSITB.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lVALIDASITB.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
        Else
            lNOMORSITB.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lVALIDASITB.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub
    Private Sub chkKelasEksekutif_CheckedChanged(sender As Object, e As EventArgs) Handles chkKelasEksekutif.CheckedChanged
        If isLoad = True Then
            If chkKelasEksekutif.Checked = False Then
                lTARIFEKSEKUTIF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                txtTarifEksekutif.Text = "0"
            Else
                lTARIFEKSEKUTIF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                txtTarifEksekutif.Text = "0"
            End If
        End If
    End Sub
    Private Sub chkNaikKelas_CheckedChanged(sender As Object, e As EventArgs) Handles chkNaikKelas.CheckedChanged
        If isLoad = True Then
            If chkNaikKelas.Checked = False Then
                lLAMA.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                LKELASPELAYANAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                LKELASPELAYANAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                txtLAMA.Text = "0"
                rbKELASPELAYANAN.SelectedIndex = 0
            Else
                lLAMA.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                LKELASPELAYANAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                LKELASPELAYANAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                txtLAMA.Text = "0"
                rbKELASPELAYANAN.SelectedIndex = 0
            End If
        End If
    End Sub
    Private Sub chkAdaRawat_CheckedChanged(sender As Object, e As EventArgs) Handles chkAdaRawat.CheckedChanged
        If isLoad = True Then
            If chkAdaRawat.Checked = False Then
                lVentilator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lRAWATINTENSIF_HARI_TEXT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lVENTILATOR_TEXT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                txtRAWATINTENSIF_HARI.Text = 0
                txtVENTILATOR.Text = 0
            Else
                lVentilator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lRAWATINTENSIF_HARI_TEXT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lVENTILATOR_TEXT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                txtRAWATINTENSIF_HARI.Text = 0
                txtVENTILATOR.Text = 0
            End If
        End If
    End Sub
    Private Sub OnValueChanged(sender As System.Object, e As System.EventArgs) Handles txtProsedurNonBedah.EditValueChanged, txtTenagaAhli.EditValueChanged, txtRadiologi.EditValueChanged, txtRehabilitasi.EditValueChanged _
                                      , txtObat.EditValueChanged, txtAlkes.EditValueChanged, txtProsedurBedah.EditValueChanged, txtKeperawatan.EditValueChanged, txtLaboratorium.EditValueChanged _
                                      , txtKamarAkomodasi.EditValueChanged, txtObatKronis.EditValueChanged, txtBMHP.EditValueChanged, txtKonsultasi.EditValueChanged _
                                      , txtPenunjang.EditValueChanged, txtPelayananDarah.EditValueChanged, txtRawatIntensif.EditValueChanged, txtObatKemoTerapi.EditValueChanged, txtSewaAlat.EditValueChanged _
                                      , txtTarifEksekutif.EditValueChanged
        If isLoad Then
            Calculate()
        End If
    End Sub
    Private Sub OnValueChanged1(sender As System.Object, e As System.EventArgs) Handles txtTarifCBG.EditValueChanged, txtTarifSubAcute.EditValueChanged, txtTarifChronic.EditValueChanged, txtTarifSpecialProcedure.EditValueChanged, txtTarifSpecialProsthesis.EditValueChanged, txtTarifSpecialInvestigation.EditValueChanged, txtTarifSpecialDrug.EditValueChanged, txtTarifBiayaTambahan.EditValueChanged
        If isLoad Then
            txtTarifTotal.Text = CDec(txtTarifCBG.Text) + CDec(txtTarifSubAcute.Text) + CDec(txtTarifChronic.Text) + CDec(txtTarifSpecialProcedure.Text) + CDec(txtTarifSpecialProsthesis.Text) + CDec(txtTarifSpecialInvestigation.Text) + CDec(txtTarifSpecialDrug.Text) + CDec(txtTarifBiayaTambahan.Text)
        End If
    End Sub
    Private Sub Calculate()
        Dim sSubTotal = 0

        sSubTotal = CDec(txtProsedurNonBedah.Text) + CDec(txtTenagaAhli.Text) + CDec(txtRadiologi.Text) + CDec(txtRehabilitasi.Text) _
                                  + CDec(txtObat.Text) + CDec(txtAlkes.Text) + CDec(txtProsedurBedah.Text) + CDec(txtKeperawatan.Text) + CDec(txtLaboratorium.Text) _
                                  + CDec(txtKamarAkomodasi.Text) + CDec(txtObatKronis.Text) + CDec(txtBMHP.Text) + CDec(txtKonsultasi.Text) _
                                  + CDec(txtPenunjang.Text) + CDec(txtPelayananDarah.Text) + CDec(txtRawatIntensif.Text) + CDec(txtObatKemoTerapi.Text) + CDec(txtSewaAlat.Text) _
                                  + CDec(txtTarifEksekutif.Text)

        txttarifRumahSakit.Text = sSubTotal

    End Sub
#End Region
#Region "Grid Method"
    Private Sub grvPersalinan_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvPersalinan.CellValueChanged
        If e.Column.Name = coldelivery_sequence.Name Then
            If grvPersalinan.GetFocusedRowCellValue(coldelivery_sequence) IsNot Nothing Then
                grvPersalinan.SetFocusedRowCellValue(coldelivery_dttm, Now)
                grvPersalinan.SetFocusedRowCellValue(colshk_spesimen_dttm, Now)
            End If
        End If
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_Topup(ByVal code As String)
        Try

            Dim dsList = oStatusGrouperHasil.GetDataDetail(sNoId)
            grdTopUpProsthesis.Properties.DataSource = dsList.ToList()
            grdTopUpProsthesis.Properties.ValueMember = "code"
            grdTopUpProsthesis.Properties.DisplayMember = "description"

            grdTopUpProsthesis.Text = code

            If dsList.Count > 0 Then
                lToup1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lToup2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lToup3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                lToup1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lToup2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lToup3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub DeleteToolStripMenuItem2_Click(sender As Object, e As EventArgs)
        'If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvRincian.DeleteSelectedRows()
    End Sub
    Private Sub chkPasienTB_CheckedChanged(sender As Object, e As EventArgs) Handles chkPasienTB.CheckedChanged
        If isLoad = True Then
            If chkPasienTB.Checked = False Then
                lNOMORSITB.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lVALIDASITB.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Else
                lNOMORSITB.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lVALIDASITB.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
        End If
    End Sub
    Private Sub txtCODE_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCODE.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If txtCODE.Text <> "" Then
                fn_LoadDataDGCareRincianHeader()
            End If
        End If
    End Sub
    Private Sub grdTopUpProsthesis_EditValueChanged(sender As Object, e As EventArgs) Handles grdTopUpProsthesis.EditValueChanged
        If isLoad = False Then Exit Sub

        If grdTopUpProsthesis.Text <> "" Then
            If fn_06GROUPINGIDRGSTAGE2(grdTopUpProsthesis.EditValue) = True Then

            End If
        End If
    End Sub
    Private Sub grdKDSPECIAL_PROCEDURE_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDSPECIAL_PROCEDURE.EditValueChanged
        If isLoad = False Then Exit Sub

        If grdKDSPECIAL_PROCEDURE.Text <> "" Then
            Dim ds = oRIdentitasGrouperData.GetDataDetailSpecialCMGFirst(sNoId, grdKDSPECIAL_PROCEDURE.EditValue, "Special Procedure")

            If ds IsNot Nothing Then
                If fn_15GROUPINGINACBGSTAGE2("Special Procedure") = True Then
                    If fn_Save("GROUPER STAGE 2", "normal", "") = False Then
                        MsgBox("Gagal Simpan", MsgBoxStyle.Exclamation, Me.Text)
                    End If
                End If
            Else
                MsgBox("Special Procedure Tidak Ada, SIlahkan Laukukan Stage 1 Terlebih dahulu", MsgBoxStyle.Exclamation, Me.Text)
            End If
        End If
    End Sub
    Private Sub grdKDSPECIAL_PROSTHESIS_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDSPECIAL_PROSTHESIS.EditValueChanged
        If isLoad = False Then Exit Sub

        If grdKDSPECIAL_PROSTHESIS.Text <> "" Then
            Dim ds = oRIdentitasGrouperData.GetDataDetailSpecialCMGFirst(sNoId, grdKDSPECIAL_PROSTHESIS.EditValue, "Special Prosthesis")

            If ds IsNot Nothing Then
                If fn_15GROUPINGINACBGSTAGE2("Special Prosthesis") = True Then
                    If fn_Save("GROUPER STAGE 2", "normal", "") = False Then
                        MsgBox("Gagal Simpan", MsgBoxStyle.Exclamation, Me.Text)
                    End If
                End If
            Else
                MsgBox("Special Prosthesis Tidak Ada, SIlahkan Laukukan Stage 1 Terlebih dahulu", MsgBoxStyle.Exclamation, Me.Text)
            End If
        End If
    End Sub
    Private Sub grdKDSPECIAL_INVESTIGATION_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDSPECIAL_INVESTIGATION.EditValueChanged
        If isLoad = False Then Exit Sub

        If grdKDSPECIAL_INVESTIGATION.Text <> "" Then
            Dim ds = oRIdentitasGrouperData.GetDataDetailSpecialCMGFirst(sNoId, grdKDSPECIAL_INVESTIGATION.EditValue, "Special Investigation")

            If ds IsNot Nothing Then
                If fn_15GROUPINGINACBGSTAGE2("Special Investigation") = True Then
                    If fn_Save("GROUPER STAGE 2", "normal", "") = False Then
                        MsgBox("Gagal Simpan", MsgBoxStyle.Exclamation, Me.Text)
                    End If
                End If
            Else
                MsgBox("Special Investigation Tidak Ada, SIlahkan Laukukan Stage 1 Terlebih dahulu", MsgBoxStyle.Exclamation, Me.Text)
            End If
        End If
    End Sub
    Private Sub grdKDSPECIAL_DRUG_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDSPECIAL_DRUG.EditValueChanged
        If isLoad = False Then Exit Sub

        If grdKDSPECIAL_DRUG.Text <> "" Then
            Dim ds = oRIdentitasGrouperData.GetDataDetailSpecialCMGFirst(sNoId, grdKDSPECIAL_DRUG.EditValue, "Special Drug")

            If ds IsNot Nothing Then
                If fn_15GROUPINGINACBGSTAGE2("Special Drug") = True Then
                    If fn_Save("GROUPER STAGE 2", "normal", "") = False Then
                        MsgBox("Gagal Simpan", MsgBoxStyle.Exclamation, Me.Text)
                    End If
                End If
            Else
                MsgBox("Special Drug Tidak Ada, SIlahkan Laukukan Stage 1 Terlebih dahulu", MsgBoxStyle.Exclamation, Me.Text)
            End If
        End If
    End Sub
    Private Sub grdCariDiagnosaiDRG_EditValueChanged(sender As Object, e As EventArgs) Handles grdCariDiagnosaiDRG.EditValueChanged
        If grdCariDiagnosaiDRG.Text <> "" Then

            If grvCariDiagnosaiDRG.GetFocusedRowCellValue("ISDEFAULT") Is Nothing Then
                Exit Sub
            End If

            Dim sCek As Integer = 0

            If listdiagnosakodeidRG.Count > 0 Then
                sCek += 1
            End If

            If sCek = 0 Then
                If CBool(grvCariDiagnosaiDRG.GetFocusedRowCellValue("ISACTIVE")) = False Then
                    MsgBox("Diagnosa " & grdCariDiagnosaiDRG.EditValue & " Tidak Bisa Jadi Primery", MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                End If
            End If

            If CBool(grvCariDiagnosaiDRG.GetFocusedRowCellValue("ISDEFAULT")) = False Then
                MsgBox("Diagnosa " & grdCariDiagnosaiDRG.EditValue & " Tidak Valid Untuk Grouper", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim dsRekap As New DataAccess.R_IDENTITAS_GROUPER_DATA_DIAGNOSAIDRG
            dsRekap.datecreated = Now
            dsRekap.dateupdated = Now
            dsRekap.kodegrouper = sNoId
            dsRekap.seq = sCek
            dsRekap.kategori = IIf(sCek = 0, "Primary", "Secondary")
            dsRekap.kddiagnosa = grdCariDiagnosaiDRG.EditValue
            dsRekap.memo = IIf(sCek = 0, grdCariDiagnosaiDRG.Text.ToString.Replace(grdCariDiagnosaiDRG.EditValue & " - ", ""), "    " & grdCariDiagnosaiDRG.Text.ToString.Replace(grdCariDiagnosaiDRG.EditValue & " - ", ""))

            listdiagnosakodeidRG.Add(dsRekap)

            lblDiagnosaiDRG.ResetText()
            lblDiagnosaiDRG_1.ResetText()
            lblDiagnosaiDRG_2.ResetText()

            Dim listkode As New List(Of String)
            Dim listkode1 As New List(Of String)
            Dim listkode2 As New List(Of String)

            For Each xloop In listdiagnosakodeidRG.OrderBy(Function(x) x.seq)
                listkode.Add(xloop.memo)
                listkode1.Add(xloop.kddiagnosa)
                listkode2.Add(xloop.kategori)
            Next

            lblDiagnosaiDRG.Text = String.Join(vbCrLf, listkode.ToArray)
            lblDiagnosaiDRG_1.Text = String.Join(vbCrLf, listkode1.ToArray)
            lblDiagnosaiDRG_2.Text = String.Join(vbCrLf, listkode2.ToArray)

            grdCariDiagnosaiDRG.ResetText()

        End If
    End Sub
    Private Sub grdCariProseduriDRG_EditValueChanged(sender As Object, e As EventArgs) Handles grdCariProseduriDRG.EditValueChanged
        If grdCariProseduriDRG.Text <> "" Then
            If grvCariProseduriDRG.GetFocusedRowCellValue("ISDEFAULT") Is Nothing Then
                Exit Sub
            End If

            Dim sCek As Integer = 0

            If listprosedurkodeidRG.Count > 0 Then
                sCek += 1
            End If

            If sCek = 0 Then
                If CBool(grvCariProseduriDRG.GetFocusedRowCellValue("ISACTIVE")) = False Then
                    MsgBox("Prosedur " & grdCariProseduriDRG.EditValue & " Tidak Bisa Jadi Primery", MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                End If
            End If

            If CBool(grvCariProseduriDRG.GetFocusedRowCellValue("ISDEFAULT")) = False Then
                MsgBox("Prosedur " & grdCariProseduriDRG.EditValue & " Tidak Valid Untuk Grouper", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim dsRekap As New DataAccess.R_IDENTITAS_GROUPER_DATA_PROSEDURIDRG
            dsRekap.datecreated = Now
            dsRekap.dateupdated = Now
            dsRekap.kodegrouper = sNoId
            dsRekap.seq = sCek
            dsRekap.jumlah = 1
            dsRekap.kdprpsedur = grdCariProseduriDRG.EditValue
            dsRekap.memo = grdCariProseduriDRG.Text.ToString.Replace(grdCariProseduriDRG.EditValue & " - ", "")

            listprosedurkodeidRG.Add(dsRekap)

            lblProseduriDRG.ResetText()
            lblProseduriDRG_1.ResetText()
            lblProseduriDRG_2.ResetText()

            Dim listkode As New List(Of String)
            Dim listkode1 As New List(Of String)
            Dim listkode2 As New List(Of String)

            For Each xloop In listprosedurkodeidRG.OrderBy(Function(x) x.seq)
                listkode.Add(xloop.memo)
                listkode1.Add(xloop.kdprpsedur)

                If xloop.jumlah > 1 Then
                    listkode2.Add(" x " & xloop.jumlah)
                Else
                    listkode2.Add("")
                End If
            Next

            lblProseduriDRG.Text = String.Join(vbCrLf, listkode.ToArray)
            lblProseduriDRG_1.Text = String.Join(vbCrLf, listkode1.ToArray)
            lblProseduriDRG_2.Text = String.Join(vbCrLf, listkode2.ToArray)

            grdCariProseduriDRG.ResetText()

        End If
    End Sub
    Private Sub grdCariDiagnosa_EditValueChanged(sender As Object, e As EventArgs) Handles grdCariDiagnosa.EditValueChanged
        If grdCariDiagnosa.Text <> "" Then
            Dim sCek As Integer = 0

            For i As Integer = 0 To grvDiagnosa.RowCount - 2
                sCek += 1
            Next

            grvDiagnosa.Focus()
            grvDiagnosa.AddNewRow()
            grvDiagnosa.SetFocusedRowCellValue(colkategori, IIf(sCek = 0, "Primer", "Sekunder"))
            grvDiagnosa.SetFocusedRowCellValue(colkddiagnosa, grdCariDiagnosa.EditValue)
            grvDiagnosa.SetFocusedRowCellValue(colmemodiagnosa, grdCariDiagnosa.Text)
            grvDiagnosa.UpdateCurrentRow()

            If chkINA.Checked = True Then
                txtKDDIAGNOSA_V6.Text = If(txtKDDIAGNOSA_V6.Text = "", grdCariDiagnosa.EditValue, txtKDDIAGNOSA_V6.Text & "#" & grdCariDiagnosa.EditValue)
            End If
        End If
    End Sub
    Private Sub grdDIAGNOSA_V6_EditValueChanged(sender As Object, e As EventArgs) Handles grdDIAGNOSA_V6.EditValueChanged
        'If isLoad = True Then
        '    If grdDIAGNOSA_V6.Text <> "" Then
        '        Dim listdiagnosakode As New List(Of String)

        '        listdiagnosakode.Add(txtKDDIAGNOSA_V6.Text)

        '        txtKDDIAGNOSA_V6.Text = String.Join("#", listdiagnosakode.ToArray)
        '    End If
        'End If
    End Sub
    Private Sub grdPROSEDUR_V6_EditValueChanged(sender As Object, e As EventArgs) Handles grdPROSEDUR_V6.EditValueChanged
        If isLoad = True Then
            'If grdPROSEDUR_V6.Text <> "" Then
            '    Dim listdiagnosakode As New List(Of String)

            '    listdiagnosakode.Add(txtKDPROSEDUR_V6.Text)

            '    txtKDPROSEDUR_V6.Text = String.Join("#", listdiagnosakode.ToArray)
            'End If
        End If
    End Sub
    Private Sub grdCariProsedur_EditValueChanged(sender As Object, e As EventArgs) Handles grdCariProsedur.EditValueChanged
        If grdCariProsedur.Text <> "" Then
            grvProsedur.Focus()
            grvProsedur.AddNewRow()
            grvProsedur.SetFocusedRowCellValue(colkdprpsedur, grdCariProsedur.EditValue)
            grvProsedur.SetFocusedRowCellValue(colmemoprosedur, grdCariProsedur.Text)
            grvProsedur.UpdateCurrentRow()

            If chkINA.Checked = True Then
                txtKDPROSEDUR_V6.Text = If(txtKDPROSEDUR_V6.Text = "", grdCariProsedur.EditValue, txtKDPROSEDUR_V6.Text & "#" & grdCariProsedur.EditValue)
            End If
        End If
    End Sub
    'Public Sub fn_LoadDiagnosa(ByVal Diagnosa As String, ByVal iDRG As Boolean)
    '    Try
    '        Dim oConn As New SqlConnection
    '        Dim oComm As New SqlCommand
    '        Dim da As SqlDataAdapter
    '        Dim ds As New DataSet
    '        Dim SQL As String = ""
    '        Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

    '        oConn = New SqlConnection(sConn)

    '        If oConn.State = ConnectionState.Closed Then
    '            oConn.Open()
    '        End If

    '        '''''''''''''''''''''''''''''''start code'''''''''''''''''''''''''''''''
    '        Dim search As String = ""
    '        Dim searchArray() As String
    '        Dim searchArrayNewTemp = New List(Of String)

    '        Dim searchArrayNew() As String

    '        Dim i As Integer

    '        search = Diagnosa
    '        searchArray = search.Split(" ")

    '        i = 0
    '        For Each s In searchArray
    '            If (searchArray(i).Trim = "") Then
    '            Else
    '                searchArrayNewTemp.Add(searchArray(i))
    '            End If
    '            i = i + 1
    '        Next

    '        searchArrayNew = searchArrayNewTemp.ToArray

    '        If (searchArrayNew.Length = 1) Then
    '            SQL = "Select TOP 50 kode = KDDIAGNOSA, NAMA = MEMO FROM [dbo].[M_DIAGNOSA] where ISACTIVE = 1 And MEMO Like '%" & searchArrayNew(0) & "%'"
    '        ElseIf (searchArrayNew.Length > 1) Then
    '            i = 0
    '            For Each s In searchArrayNew
    '                If SQL = "" Then
    '                    SQL = "SELECT TOP 50 kode = KDDIAGNOSA, nama = MEMO FROM [dbo].[M_DIAGNOSA] where ISACTIVE = 1 AND MEMO like '%" & searchArrayNew(i) & "%'"
    '                    i = i + 1
    '                Else
    '                    SQL = SQL & " AND MEMO like '%" & searchArrayNew(i) & "%'"
    '                    i = i + 1
    '                End If
    '            Next
    '        Else
    '            SQL = "SELECT kode = KDDIAGNOSA, nama = MEMO FROM [dbo].[M_DIAGNOSA] WHERE ISACTIVE = 1 "
    '        End If

    '        '''''''''''''''''''''''''''''''end code'''''''''''''''''''''''''''''''

    '        oComm.Connection = oConn
    '        oComm.CommandText = SQL
    '        oComm.CommandTimeout = 120
    '        oComm.CommandType = CommandType.Text

    '        da = New SqlDataAdapter(oComm)
    '        da.Fill(ds, "CARI_DIAGNOSA")

    '        If iDRG = False Then
    '            grdCariDiagnosa.Properties.DataSource = ds.Tables("CARI_DIAGNOSA")
    '            grdCariDiagnosa.Properties.ValueMember = "kode"
    '            grdCariDiagnosa.Properties.DisplayMember = "nama"

    '            grdDIAGNOSA_V6.Properties.DataSource = ds.Tables("CARI_DIAGNOSA")
    '            grdDIAGNOSA_V6.Properties.ValueMember = "kode"
    '            grdDIAGNOSA_V6.Properties.DisplayMember = "nama"

    '            grvCariDiagnosa.Columns("kode").VisibleIndex = -1

    '            grvCariDiagnosa.BestFitColumns()
    '            grdCariDiagnosa.ShowPopup()
    '        Else
    '            grdCariDiagnosaiDRG.Properties.DataSource = ds.Tables("CARI_DIAGNOSA")
    '            grdCariDiagnosaiDRG.Properties.ValueMember = "kode"
    '            grdCariDiagnosaiDRG.Properties.DisplayMember = "nama"

    '            grvCariDiagnosaiDRG.Columns("kode").VisibleIndex = -1
    '            grvCariDiagnosaiDRG.BestFitColumns()
    '            grdCariDiagnosaiDRG.ShowPopup()
    '        End If

    '        If oConn.State = ConnectionState.Open Then
    '            oConn.Close()
    '        End If

    '    Catch ex As Exception

    '    End Try
    '    'Try
    '    '    Dim jsonDecode = JObject.Parse(oGetGrouper.fn_Pencariandiagnosa(sEklaim_Url, sEklaim_Generate, Diagnosa))
    '    '    Dim sDataDuplicate As String = String.Empty
    '    '    Dim smessage As String = String.Empty

    '    '    sDataDuplicate = jsonDecode("metadata")("code").ToString
    '    '    smessage = jsonDecode("metadata")("message").ToString

    '    '    If sDataDuplicate = "200" Then
    '    '        Dim table As DataTable

    '    '        table = New DataTable("M_DIAGNOSA")
    '    '        table.Columns.Add("nama")
    '    '        table.Columns.Add("kode")

    '    '        For Each item In jsonDecode("response")("data")
    '    '            table.Rows.Add(New String() {item(0), item(1)})
    '    '        Next

    '    '        If iDRG = False Then
    '    '            grdCariDiagnosa.Properties.DataSource = table
    '    '            grdCariDiagnosa.Properties.ValueMember = "kode"
    '    '            grdCariDiagnosa.Properties.DisplayMember = "nama"

    '    '            grdDIAGNOSA_V6.Properties.DataSource = table
    '    '            grdDIAGNOSA_V6.Properties.ValueMember = "kode"
    '    '            grdDIAGNOSA_V6.Properties.DisplayMember = "nama"

    '    '            grvCariDiagnosa.BestFitColumns()
    '    '            grdCariDiagnosa.ShowPopup()
    '    '        Else
    '    '            grdCariDiagnosaiDRG.Properties.DataSource = table
    '    '            grdCariDiagnosaiDRG.Properties.ValueMember = "kode"
    '    '            grdCariDiagnosaiDRG.Properties.DisplayMember = "nama"

    '    '            grvCariDiagnosaiDRG.BestFitColumns()
    '    '            grdCariDiagnosaiDRG.ShowPopup()
    '    '        End If

    '    '    Else
    '    '        MsgBox("Pencarian Diagnosa" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Information, Me.Text)
    '    '    End If
    '    'Catch oErr As Exception
    '    '    MsgBox("Pencarian Diagnosa: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    'End Try
    'End Sub
    'Public Sub fn_LoadProsedur(ByVal Diagnosa As String, ByVal iDRG As Boolean)
    '    Try
    '        Dim oConn As New SqlConnection
    '        Dim oComm As New SqlCommand
    '        Dim da As SqlDataAdapter
    '        Dim ds As New DataSet
    '        Dim SQL As String = ""
    '        Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

    '        oConn = New SqlConnection(sConn)

    '        If oConn.State = ConnectionState.Closed Then
    '            oConn.Open()
    '        End If

    '        '''''''''''''''''''''''''''''''start code'''''''''''''''''''''''''''''''
    '        Dim search As String = ""
    '        Dim searchArray() As String
    '        Dim searchArrayNewTemp = New List(Of String)

    '        Dim searchArrayNew() As String

    '        Dim i As Integer

    '        search = Diagnosa
    '        searchArray = search.Split(" ")

    '        i = 0
    '        For Each s In searchArray
    '            If (searchArray(i).Trim = "") Then
    '            Else
    '                searchArrayNewTemp.Add(searchArray(i))
    '            End If
    '            i = i + 1
    '        Next

    '        searchArrayNew = searchArrayNewTemp.ToArray

    '        If (searchArrayNew.Length = 1) Then
    '            SQL = "SELECT TOP 50 kode = KDPROSEDUR, nama = MEMO FROM [dbo].[M_PROSEDUR] where ISACTIVE = 1 AND MEMO like '%" & searchArrayNew(0) & "%'"
    '        ElseIf (searchArrayNew.Length > 1) Then
    '            i = 0
    '            For Each s In searchArrayNew
    '                If SQL = "" Then
    '                    SQL = "SELECT TOP 50 kode = KDPROSEDUR, nama = MEMO FROM [dbo].[M_PROSEDUR] where ISACTIVE = 1 AND MEMO like '%" & searchArrayNew(i) & "%'"
    '                    i = i + 1
    '                Else
    '                    SQL = SQL & " AND MEMO like '%" & searchArrayNew(i) & "%'"
    '                    i = i + 1
    '                End If
    '            Next
    '        Else
    '            SQL = "SELECT kode = KDPROSEDUR, nama = MEMO FROM [dbo].[M_PROSEDUR] WHERE ISACTIVE = 1 "
    '        End If

    '        '''''''''''''''''''''''''''''''end code'''''''''''''''''''''''''''''''

    '        oComm.Connection = oConn
    '        oComm.CommandText = SQL
    '        oComm.CommandTimeout = 120
    '        oComm.CommandType = CommandType.Text

    '        da = New SqlDataAdapter(oComm)
    '        da.Fill(ds, "CARI_PROSEDUR")

    '        If iDRG = False Then
    '            grdCariProsedur.Properties.DataSource = ds.Tables("CARI_PROSEDUR")
    '            grdCariProsedur.Properties.ValueMember = "kode"
    '            grdCariProsedur.Properties.DisplayMember = "nama"

    '            grdPROSEDUR_V6.Properties.DataSource = ds.Tables("CARI_PROSEDUR")
    '            grdPROSEDUR_V6.Properties.ValueMember = "kode"
    '            grdPROSEDUR_V6.Properties.DisplayMember = "nama"

    '            grvCariProsedur.Columns("kode").VisibleIndex = -1
    '            grvCariProsedur.BestFitColumns()
    '            grdCariProsedur.ShowPopup()
    '        Else
    '            grdCariProseduriDRG.Properties.DataSource = ds.Tables("CARI_PROSEDUR")
    '            grdCariProseduriDRG.Properties.ValueMember = "kode"
    '            grdCariProseduriDRG.Properties.DisplayMember = "nama"

    '            grvCariProseduriDRG.Columns("kode").VisibleIndex = -1
    '            grvCariProseduriDRG.BestFitColumns()
    '            grdCariProseduriDRG.ShowPopup()
    '        End If

    '        If oConn.State = ConnectionState.Open Then
    '            oConn.Close()
    '        End If

    '    Catch ex As Exception

    '    End Try
    '    'Try
    '    '    Dim jsonDecode = JObject.Parse(oGetGrouper.fn_PencarianProsedur(sEklaim_Url, sEklaim_Generate, Diagnosa))
    '    '    Dim sDataDuplicate As String = String.Empty
    '    '    Dim smessage As String = String.Empty

    '    '    sDataDuplicate = jsonDecode("metadata")("code").ToString
    '    '    smessage = jsonDecode("metadata")("message").ToString

    '    '    If sDataDuplicate = "200" Then
    '    '        Dim table As DataTable

    '    '        table = New DataTable("M_PROSEDUR")
    '    '        table.Columns.Add("nama")
    '    '        table.Columns.Add("kode")

    '    '        For Each item In jsonDecode("response")("data")
    '    '            table.Rows.Add(New String() {item(0), item(1)})
    '    '        Next

    '    '        If iDRG = False Then
    '    '            grdCariProsedur.Properties.DataSource = table
    '    '            grdCariProsedur.Properties.ValueMember = "kode"
    '    '            grdCariProsedur.Properties.DisplayMember = "nama"

    '    '            grdPROSEDUR_V6.Properties.DataSource = table
    '    '            grdPROSEDUR_V6.Properties.ValueMember = "kode"
    '    '            grdPROSEDUR_V6.Properties.DisplayMember = "nama"

    '    '            grvCariProsedur.BestFitColumns()
    '    '            grdCariProsedur.ShowPopup()
    '    '        Else
    '    '            grdCariProseduriDRG.Properties.DataSource = table
    '    '            grdCariProseduriDRG.Properties.ValueMember = "kode"
    '    '            grdCariProseduriDRG.Properties.DisplayMember = "nama"

    '    '            grvCariProseduriDRG.BestFitColumns()
    '    '            grdCariProseduriDRG.ShowPopup()
    '    '        End If
    '    '    Else
    '    '        MsgBox("Pencarian Prosedur" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Information, Me.Text)
    '    '    End If
    '    'Catch oErr As Exception
    '    '    MsgBox("Pencarian Prosedur: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    'End Try
    'End Sub
    Private Sub fn_LoadDataSpecialCMG()
        Try
            grdKDSPECIAL_PROCEDURE.Properties.DataSource = oRIdentitasGrouperData.GetDataDetailSpecialCMG(sNoId, "Special Procedure").ToList()
            grdKDSPECIAL_PROCEDURE.Properties.ValueMember = "code"
            grdKDSPECIAL_PROCEDURE.Properties.DisplayMember = "description"

            grdKDSPECIAL_PROSTHESIS.Properties.DataSource = oRIdentitasGrouperData.GetDataDetailSpecialCMG(sNoId, "Special Prosthesis").ToList()
            grdKDSPECIAL_PROSTHESIS.Properties.ValueMember = "code"
            grdKDSPECIAL_PROSTHESIS.Properties.DisplayMember = "description"

            grdKDSPECIAL_INVESTIGATION.Properties.DataSource = oRIdentitasGrouperData.GetDataDetailSpecialCMG(sNoId, "Special Investigation").ToList()
            grdKDSPECIAL_INVESTIGATION.Properties.ValueMember = "code"
            grdKDSPECIAL_INVESTIGATION.Properties.DisplayMember = "description"

            grdKDSPECIAL_DRUG.Properties.DataSource = oRIdentitasGrouperData.GetDataDetailSpecialCMG(sNoId, "Special Drug").ToList()
            grdKDSPECIAL_DRUG.Properties.ValueMember = "code"
            grdKDSPECIAL_DRUG.Properties.DisplayMember = "description"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadCARAMASUK()
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

            SQL = "SELECT A.* "
            SQL &= "FROM M_CARAMASUK AS A "
            SQL &= "WHERE A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "M_CARAMASUK")

            grdCARAMASUK.Properties.DataSource = ds.Tables("M_CARAMASUK")

            grdCARAMASUK.Properties.ValueMember = "KDCARAMASUK"
            grdCARAMASUK.Properties.DisplayMember = "MEMO"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox("Preview COB: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadCOB()
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

            SQL = "SELECT A.* "
            SQL &= "FROM M_COB AS A "
            SQL &= "WHERE A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "M_COB")

            grdCOB.Properties.DataSource = ds.Tables("M_COB")

            grdCOB.Properties.ValueMember = "KDCOB"
            grdCOB.Properties.DisplayMember = "MEMO"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox("Preview COB: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDPJP()
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

            SQL = "SELECT A.* "
            SQL &= "FROM M_DOCTOR AS A "
            SQL &= "WHERE A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "M_DOCTOR")

            grdDPJP.Properties.DataSource = ds.Tables("M_DOCTOR")

            grdDPJP.Properties.ValueMember = "KDDOCTOR"
            grdDPJP.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox("Preview Data Dokter: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadCARAKELUAR()
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

            SQL = "SELECT A.* "
            SQL &= "FROM M_CARAKELUAR AS A "
            SQL &= "WHERE A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "M_CARAKELUAR")

            grdCaraKeluar.Properties.DataSource = ds.Tables("M_CARAKELUAR")

            grdCaraKeluar.Properties.ValueMember = "KDCARAKELUAR"
            grdCaraKeluar.Properties.DisplayMember = "MEMO"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Load Cara Keluar" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadJenisTarif()
        Dim oJenisTarif As New Reference.clsJenisTarif
        Try
            grdJenisTarif.Properties.DataSource = oJenisTarif.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdJenisTarif.Properties.ValueMember = "KODETARIF_KLAIM"
            grdJenisTarif.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDiagnosa()
        Try
            grdCariDiagnosaiDRG.Properties.DataSource = ListDiagnosaiDRG
            grdCariDiagnosaiDRG.Properties.ValueMember = "KDDIAGNOSA"
            grdCariDiagnosaiDRG.Properties.DisplayMember = "MEMO"

            grdCariDiagnosa.Properties.DataSource = ListDiagnosaiNACBG
            grdCariDiagnosa.Properties.ValueMember = "KDDIAGNOSA"
            grdCariDiagnosa.Properties.DisplayMember = "MEMO"

            grdDIAGNOSA_V6.Properties.DataSource = ListDiagnosaiNACBG
            grdDIAGNOSA_V6.Properties.ValueMember = "KDDIAGNOSA"
            grdDIAGNOSA_V6.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadProsedur()
        Try
            grdCariProseduriDRG.Properties.DataSource = ListProseduriDRG
            grdCariProseduriDRG.Properties.ValueMember = "KDPROSEDUR"
            grdCariProseduriDRG.Properties.DisplayMember = "MEMO"

            grdCariProsedur.Properties.DataSource = ListProseduriNACBG
            grdCariProsedur.Properties.ValueMember = "KDPROSEDUR"
            grdCariProsedur.Properties.DisplayMember = "MEMO"

            grdPROSEDUR_V6.Properties.DataSource = ListProseduriNACBG
            grdPROSEDUR_V6.Properties.ValueMember = "KDPROSEDUR"
            grdPROSEDUR_V6.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
#Region "Reload Data"
    Private Sub fn_LoadDataDGCareRincianHeader()
        Try
            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Processing data.....")

            Dim ds = oRIdentitasGrouper.GetData(sNoId)

            If ds IsNot Nothing Then
                Dim oPendaftaran As New Admission.clsPendaftaran

                Dim dsPendaftaran = oPendaftaran.GetData(ds.norec)
                If dsPendaftaran IsNot Nothing Then
                    sCategory = dsPendaftaran.CATEGORY
                    sTanggalLahir = dsPendaftaran.M_CUSTOMER.TANGGALLAHIR
                    If dsPendaftaran.CATEGORY = 0 Then
                        sRegisterAwal = dsPendaftaran.KDPENDAFTARAN
                    Else
                        sRegisterAwal = dsPendaftaran.KDPENDAFTARAN_AWAL
                    End If
                End If


                txtNORM.Text = ds.noRm.ToString.PadLeft(6, "0")
                txtNAMAPASIEN.Text = ds.nama.ToString
                txtTGLLAHIR.Text = ds.tgl_lahir.ToString("yyyy-MM-dd HH:mm:ss")
                txtJENISKELAMIN.Text = ds.gender
                txtNoPeserta.Text = ds.noKartu.ToString
                txtNoSEP.Text = ds.noSep.ToString

                Dim oDoctor As New Reference.clsDoctor
                Dim dsDoctor = oDoctor.GetDataByKodeVclaim(ds.dpjpkodevclaim)
                If dsDoctor IsNot Nothing Then
                    grdDPJP.Text = dsDoctor.KDDOCTOR
                End If

                Dim dsJKN = oRIdentitasGrouper.GetDataByNoSepJkn(ds.noSep)
                If dsJKN IsNot Nothing Then
                    If dsJKN.jnsPelayanan = "R.Inap" Then
                        If dsJKN.kelasRawat.Contains("3") Then
                            rbKELASHAK.SelectedIndex = 0
                        ElseIf dsJKN.kelasRawat.Contains("2") Then
                            rbKELASHAK.SelectedIndex = 1
                        ElseIf dsJKN.kelasRawat.Contains("1") Then
                            rbKELASHAK.SelectedIndex = 2
                        End If
                    End If
                End If

                If ds.jnsPelayanan = "R.Jalan" Then
                    rbCategory.SelectedIndex = 0
                    deDATEMASUK.DateTime = ds.tglSep
                    deDATEPULANG.DateTime = ds.tglSep
                Else
                    rbCategory.SelectedIndex = 1
                    deDATEMASUK.DateTime = ds.tglSep
                    Dim oResume As New EMedrek.clsRingkasanKeluar
                    Dim dsResume = oResume.GetData(ds.noregistrasi)
                    If dsResume IsNot Nothing Then
                        deDATEPULANG.DateTime = dsResume.TANGGALPULANG
                    Else
                        deDATEPULANG.DateTime = ds.tglSep
                    End If
                End If

                txtLOS.Text = HitungLOSEKlaim(deDATEMASUK.DateTime, deDATEPULANG.DateTime)
                txtUmur.Text = fn_HitungUmur(deDATEMASUK.DateTime, ds.tgl_lahir)

                rbCategory_SelectedIndexChanged()

                If ds.propinsi = "" Then
                    fn_LoadDataDGCareRincianDetil(ds.norec, ds.nostruklastfk)
                Else
                    fn_LoadDataDGCareRincianDetilNew(ds.norec)
                End If
            End If

            SplashScreenManager.CloseForm(False)

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Function HitungLOSEKlaim(masuk As DateTime, pulang As DateTime) As Integer
        ' Jika pulang belum diisi, gunakan tanggal sekarang
        If pulang = DateTime.MinValue Then
            pulang = DateTime.Now
        End If

        ' LOS = (tanggal pulang - tanggal masuk) + 1
        Dim los As Integer = (pulang.Date - masuk.Date).Days + 1

        ' Pastikan minimal 1 hari
        If los < 1 Then los = 1

        Return los
    End Function
    Private Sub fn_LoadDataDGCareRincianDetil(ByVal norec As String, ByVal nostruklastfk As String)
        'Try
        '    SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
        '    SplashScreenManager.Default.SetWaitFormCaption("Processing data.....")

        '    Dim oConnNpgsql As New NpgsqlConnection
        '    Dim oCommNpgsql As New NpgsqlCommand
        '    Dim daNpgsql As NpgsqlDataAdapter
        '    Dim dsNpgsql As New DataSet

        '    Dim sConnNpgsql As String = sConnNpgsqlproduction

        '    oConnNpgsql = New NpgsqlConnection(sConnNpgsql)

        '    If oConnNpgsql.State = ConnectionState.Closed Then
        '        oConnNpgsql.Open()
        '    End If

        '    oCommNpgsql.Connection = oConnNpgsql
        '    oCommNpgsql.CommandText = frmIncbgList.fn_LoadQuey(False, norec, nostruklastfk, sIsnorec, False)
        '    oCommNpgsql.CommandTimeout = 120
        '    oCommNpgsql.CommandType = CommandType.Text

        '    daNpgsql = New NpgsqlDataAdapter(oCommNpgsql)
        '    daNpgsql.Fill(dsNpgsql, "HISTORY_DETIL")

        '    If oConnNpgsql.State = ConnectionState.Open Then
        '        oConnNpgsql.Close()
        '    End If

        '    Dim listDetil_x As New List(Of DataAccess.R_IDENTITAS_GROUPER_ITEM)
        '    Dim oItem As New Reference.clsItem

        '    Dim TarifNonPBedah As Integer = 0
        '    Dim TarifPBedah As Integer = 0
        '    Dim TarifKonsultasi As Integer = 0
        '    Dim TarifTenagaAhli As Integer = 0
        '    Dim TarifKeperawatan As Integer = 0
        '    Dim TarifPenunjang As Integer = 0
        '    Dim TarifRadiologi As Integer = 0
        '    Dim TarifLaboratorium As Integer = 0
        '    Dim TarifPelayananDarah As Integer = 0
        '    Dim TarifRehabilitas As Integer = 0
        '    Dim TarifKamarAkomodasi As Integer = 0
        '    Dim TarifRawatInsentif As Integer = 0
        '    Dim TarifBMHP As Integer = 0
        '    Dim TarifAlatMedis As Integer = 0
        '    Dim TarifPoliEksekutif As Integer = 0
        '    Dim TarifRumahSakit As Integer = 0
        '    Dim TarifObat As Integer = 0
        '    Dim TarifAlkes As Integer = 0
        '    Dim TarifKronis As Integer = 0
        '    Dim TarifKemoTerafi As Integer = 0

        '    For iLoop As Integer = 0 To dsNpgsql.Tables("HISTORY_DETIL").Rows.Count - 1
        '        Dim dsRekap As New DataAccess.R_IDENTITAS_GROUPER_ITEM
        '        With dsNpgsql.Tables("HISTORY_DETIL")
        '            dsRekap.produkfk = .Rows(iLoop)("produkfk")

        '            If .Rows(iLoop)("Obat") = False Then
        '                Dim dsItem = oItem.GetData(.Rows(iLoop)("produkfk"))
        '                If dsItem IsNot Nothing Then
        '                    dsRekap.kategoribpjs = dsItem.M_ITEM_L3.MEMO
        '                Else
        '                    dsRekap.kategoribpjs = ""
        '                End If
        '            Else
        '                dsRekap.kategoribpjs = "OBAT-OBATAN DAN ALKES"
        '            End If

        '            dsRekap.isobat = .Rows(iLoop)("Obat")
        '            dsRekap.namaproduk = .Rows(iLoop)("namaproduk")
        '            dsRekap.jumlah = .Rows(iLoop)("jumlah")
        '            dsRekap.hargajual = .Rows(iLoop)("hargajual")
        '            listDetil_x.Add(dsRekap)
        '        End With
        '    Next

        '    For Each xloop In listDetil_x
        '        If xloop.kategoribpjs = "PROSEDUR NON-BEDAH" Then
        '            TarifNonPBedah += xloop.hargajual
        '        ElseIf xloop.kategoribpjs = "PROSEDUR BEDAH" Then
        '            TarifPBedah += xloop.hargajual
        '        ElseIf xloop.kategoribpjs = "KONSULTASI" Then
        '            TarifKonsultasi += xloop.hargajual
        '        ElseIf xloop.kategoribpjs = "TENAGA AHLI" Then
        '            TarifTenagaAhli += xloop.hargajual
        '        ElseIf xloop.kategoribpjs = "KEPERAWATAN" Then
        '            TarifKeperawatan += xloop.hargajual
        '        ElseIf xloop.kategoribpjs = "PENUNJANG" Then
        '            TarifPenunjang += xloop.hargajual
        '        ElseIf xloop.kategoribpjs = "RADIOLOGI" Then
        '            TarifRadiologi += xloop.hargajual
        '        ElseIf xloop.kategoribpjs = "LABORATORIUM" Then
        '            TarifLaboratorium += xloop.hargajual
        '        ElseIf xloop.kategoribpjs = "PELAYANAN DARAH" Then
        '            TarifPelayananDarah += xloop.hargajual
        '        ElseIf xloop.kategoribpjs = "REHABILITASI" Then
        '            TarifRehabilitas += xloop.hargajual
        '            'ElseIf xloop.kategoribpjs = "ALKES" Then
        '            '    TarifAlkes += xloop.hargajual
        '            'ElseIf xloop.kategoribpjs = "OBAT" Then
        '            '    TarifObat += xloop.hargajual
        '            'ElseIf xloop.kategoribpjs = "OBAT KRONIS" Then
        '            '    TarifKronis += xloop.hargajual
        '        ElseIf xloop.kategoribpjs = "OBAT-OBATAN DAN ALKES" Then
        '            TarifObat += xloop.hargajual
        '        ElseIf xloop.kategoribpjs = "KAMAR / AKOMODASI" Then
        '            TarifKamarAkomodasi += xloop.hargajual
        '        ElseIf xloop.kategoribpjs = "BMHP" Then
        '            TarifBMHP += xloop.hargajual
        '        ElseIf xloop.kategoribpjs = "RAWAT INTENSIF" Then
        '            TarifRawatInsentif += xloop.hargajual
        '        ElseIf xloop.kategoribpjs = "OBAT KEMOTERAPI" Then
        '            TarifKemoTerafi += xloop.hargajual
        '        ElseIf xloop.kategoribpjs = "SEWA ALAT" Then
        '            TarifAlatMedis += xloop.hargajual
        '        ElseIf xloop.kategoribpjs = "EKSEKUTIF" Then
        '            TarifPoliEksekutif += xloop.hargajual
        '        End If
        '    Next

        '    txtProsedurNonBedah.Text = TarifNonPBedah
        '    txtProsedurBedah.Text = TarifPBedah
        '    txtKonsultasi.Text = TarifKonsultasi
        '    txtTenagaAhli.Text = TarifTenagaAhli
        '    txtKeperawatan.Text = TarifKeperawatan
        '    txtPenunjang.Text = TarifPenunjang
        '    txtRadiologi.Text = TarifRadiologi
        '    txtLaboratorium.Text = TarifLaboratorium
        '    txtPelayananDarah.Text = TarifPelayananDarah
        '    txtRehabilitasi.Text = TarifRehabilitas
        '    txtKamarAkomodasi.Text = TarifKamarAkomodasi '+ TarifKamarAkomodasi40
        '    txtRawatIntensif.Text = TarifRawatInsentif
        '    txtBMHP.Text = TarifBMHP
        '    txtSewaAlat.Text = TarifAlatMedis
        '    txtTarifEksekutif.Text = TarifPoliEksekutif
        '    txtAlkes.Text = TarifAlkes
        '    txtObat.Text = TarifObat
        '    txtObatKronis.Text = TarifKronis
        '    txtObatKemoTerapi.Text = TarifKemoTerafi

        '    Calculate()

        '    grvRincian.Columns.Clear()
        '    grdRincian.DataSource = Nothing
        '    grvRincian.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

        '    grdRincian.MainView = grvRincian
        '    grdRincian.DataSource = listDetil_x
        '    grdRincian.ForceInitialize()

        '    SplashScreenManager.CloseForm(False)

        '    fn_LoadFormatDataRincian()
        'Catch oErr As Exception
        '    SplashScreenManager.CloseForm(False)
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub
    'Private Sub fn_LoadFormatDataRincian()
    '    For iLoop As Integer = 0 To grvRincian.Columns.Count - 1
    '        If grvRincian.Columns(iLoop).ColumnType.Name = "Decimal" Then
    '            grvRincian.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
    '            grvRincian.Columns(iLoop).DisplayFormat.FormatString = "{0:n0}"
    '            grvRincian.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    '            grvRincian.Columns(iLoop).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
    '            grvRincian.Columns(iLoop).SummaryItem.DisplayFormat = "{0:n0}"

    '            grvRincian.Columns(iLoop).OptionsColumn.ReadOnly = True
    '            grvRincian.Columns(iLoop).OptionsColumn.AllowEdit = False
    '        ElseIf grvRincian.Columns(iLoop).ColumnType.Name = "DateTime" Then
    '            grvRincian.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
    '            grvRincian.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"

    '            grvRincian.Columns(iLoop).OptionsColumn.ReadOnly = True
    '            grvRincian.Columns(iLoop).OptionsColumn.AllowEdit = False
    '        End If
    '    Next

    '    Dim sLanjut As Boolean = False
    '    Dim kosong As Integer = 0

    '    For i As Integer = 0 To grvRincian.RowCount - 2
    '        kosong += 1
    '    Next

    '    If kosong = 0 Then
    '        fn_LoadDataDGCareRincianHeader()
    '    End If

    '    For i As Integer = 0 To grvRincian.RowCount - 2
    '        If grvRincian.GetRowCellValue(i, "kategoribpjs") = "" Then
    '            sLanjut = False
    '            Exit For
    '        ElseIf grvRincian.GetRowCellValue(i, "kategoribpjs") = "-" Then
    '            sLanjut = False
    '            Exit For
    '        Else
    '            sLanjut = True
    '        End If
    '    Next

    '    If sLanjut = False Then
    '        MsgBox("Item/kategoribpjs masih ada yang kosong!!!", MsgBoxStyle.Exclamation, Me.Text)
    '        XtraTabControl1.SelectedTabPage = tab2
    '    End If

    '    grvRincian.BestFitColumns()
    'End Sub
    Private Sub fn_LoadDataDGCareRincianDetilNew(ByVal norec As String)
        Try
            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Processing data.....")

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String = frmIncbgList.fn_LoadQuey(norec)

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "HISTORY_DETIL")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            Dim listDetil_x As New List(Of DataAccess.R_IDENTITAS_GROUPER_ITEM)
            Dim oItem As New Reference.clsItem

            Dim TarifNonPBedah As Integer = 0
            Dim TarifPBedah As Integer = 0
            Dim TarifKonsultasi As Integer = 0
            Dim TarifTenagaAhli As Integer = 0
            Dim TarifKeperawatan As Integer = 0
            Dim TarifPenunjang As Integer = 0
            Dim TarifRadiologi As Integer = 0
            Dim TarifLaboratorium As Integer = 0
            Dim TarifPelayananDarah As Integer = 0
            Dim TarifRehabilitas As Integer = 0
            Dim TarifKamarAkomodasi As Integer = 0
            Dim TarifRawatInsentif As Integer = 0
            Dim TarifBMHP As Integer = 0
            Dim TarifAlatMedis As Integer = 0
            Dim TarifPoliEksekutif As Integer = 0
            Dim TarifRumahSakit As Integer = 0
            Dim TarifObat As Integer = 0
            Dim TarifAlkes As Integer = 0
            Dim TarifKronis As Integer = 0
            Dim TarifKemoTerafi As Integer = 0

            For iLoop As Integer = 0 To ds.Tables("HISTORY_DETIL").Rows.Count - 1
                Dim dsRekap As New DataAccess.R_IDENTITAS_GROUPER_ITEM
                With ds.Tables("HISTORY_DETIL")
                    dsRekap.produkfk = .Rows(iLoop)("produkfk")

                    If .Rows(iLoop)("Obat") = False Then
                        Dim dsItem = oItem.GetData(.Rows(iLoop)("produkfk"))
                        If dsItem IsNot Nothing Then
                            dsRekap.kategoribpjs = dsItem.M_ITEM_L3.MEMO
                        Else
                            dsRekap.kategoribpjs = ""
                        End If
                    Else
                        dsRekap.kategoribpjs = "OBAT-OBATAN DAN ALKES"
                    End If

                    dsRekap.isobat = .Rows(iLoop)("Obat")
                    dsRekap.namaproduk = .Rows(iLoop)("namaproduk")
                    dsRekap.jumlah = .Rows(iLoop)("jumlah")
                    dsRekap.hargajual = .Rows(iLoop)("hargajual")
                    listDetil_x.Add(dsRekap)
                End With
            Next

            For Each xloop In listDetil_x
                If xloop.kategoribpjs = "PROSEDUR NON-BEDAH" Then
                    TarifNonPBedah += xloop.hargajual
                ElseIf xloop.kategoribpjs = "PROSEDUR BEDAH" Then
                    TarifPBedah += xloop.hargajual
                ElseIf xloop.kategoribpjs = "KONSULTASI" Then
                    TarifKonsultasi += xloop.hargajual
                ElseIf xloop.kategoribpjs = "TENAGA AHLI" Then
                    TarifTenagaAhli += xloop.hargajual
                ElseIf xloop.kategoribpjs = "KEPERAWATAN" Then
                    TarifKeperawatan += xloop.hargajual
                ElseIf xloop.kategoribpjs = "PENUNJANG" Then
                    TarifPenunjang += xloop.hargajual
                ElseIf xloop.kategoribpjs = "RADIOLOGI" Then
                    TarifRadiologi += xloop.hargajual
                ElseIf xloop.kategoribpjs = "LABORATORIUM" Then
                    TarifLaboratorium += xloop.hargajual
                ElseIf xloop.kategoribpjs = "PELAYANAN DARAH" Then
                    TarifPelayananDarah += xloop.hargajual
                ElseIf xloop.kategoribpjs = "REHABILITASI" Then
                    TarifRehabilitas += xloop.hargajual
                ElseIf xloop.kategoribpjs = "ALKES" Then
                    TarifAlkes += xloop.hargajual
                ElseIf xloop.kategoribpjs = "OBAT" Then
                    TarifObat += xloop.hargajual
                ElseIf xloop.kategoribpjs = "OBAT KRONIS" Then
                    TarifKronis += xloop.hargajual
                ElseIf xloop.kategoribpjs = "OBAT-OBATAN DAN ALKES" Then
                    TarifObat += xloop.hargajual
                ElseIf xloop.kategoribpjs = "KAMAR / AKOMODASI" Then
                    TarifKamarAkomodasi += xloop.hargajual
                ElseIf xloop.kategoribpjs = "BMHP" Then
                    TarifBMHP += xloop.hargajual
                ElseIf xloop.kategoribpjs = "RAWAT INTENSIF" Then
                    TarifRawatInsentif += xloop.hargajual
                ElseIf xloop.kategoribpjs = "OBAT KEMOTERAPI" Then
                    TarifKemoTerafi += xloop.hargajual
                ElseIf xloop.kategoribpjs = "SEWA ALAT" Then
                    TarifAlatMedis += xloop.hargajual
                ElseIf xloop.kategoribpjs = "EKSEKUTIF" Then
                    TarifPoliEksekutif += xloop.hargajual
                End If
            Next

            txtProsedurNonBedah.Text = TarifNonPBedah
            txtProsedurBedah.Text = TarifPBedah
            txtKonsultasi.Text = TarifKonsultasi
            txtTenagaAhli.Text = TarifTenagaAhli
            txtKeperawatan.Text = TarifKeperawatan
            txtPenunjang.Text = TarifPenunjang
            txtRadiologi.Text = TarifRadiologi
            txtLaboratorium.Text = TarifLaboratorium
            txtPelayananDarah.Text = TarifPelayananDarah
            txtRehabilitasi.Text = TarifRehabilitas
            txtKamarAkomodasi.Text = TarifKamarAkomodasi '+ TarifKamarAkomodasi40
            txtRawatIntensif.Text = TarifRawatInsentif
            txtBMHP.Text = TarifBMHP
            txtSewaAlat.Text = TarifAlatMedis
            txtTarifEksekutif.Text = TarifPoliEksekutif
            txtAlkes.Text = TarifAlkes
            txtObat.Text = TarifObat
            txtObatKronis.Text = TarifKronis
            txtObatKemoTerapi.Text = TarifKemoTerafi

            Calculate()

            grvRincian.Columns.Clear()
            grdRincian.DataSource = Nothing
            grvRincian.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            grdRincian.MainView = grvRincian
            grdRincian.DataSource = listDetil_x
            grdRincian.ForceInitialize()

            SplashScreenManager.CloseForm(False)

            fn_LoadFormatDataRincianNew()
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatDataRincianNew()
        For iLoop As Integer = 0 To grvRincian.Columns.Count - 1
            If grvRincian.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvRincian.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvRincian.Columns(iLoop).DisplayFormat.FormatString = "{0:n0}"
                grvRincian.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                grvRincian.Columns(iLoop).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                grvRincian.Columns(iLoop).SummaryItem.DisplayFormat = "{0:n0}"

                grvRincian.Columns(iLoop).OptionsColumn.ReadOnly = True
                grvRincian.Columns(iLoop).OptionsColumn.AllowEdit = False
            ElseIf grvRincian.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvRincian.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvRincian.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"

                grvRincian.Columns(iLoop).OptionsColumn.ReadOnly = True
                grvRincian.Columns(iLoop).OptionsColumn.AllowEdit = False
            End If
        Next

        Dim sLanjut As Boolean = False

        For i As Integer = 0 To grvRincian.RowCount - 2
            If grvRincian.GetRowCellValue(i, "kategoribpjs") = "" Then
                sLanjut = False
                Exit For
            ElseIf grvRincian.GetRowCellValue(i, "kategoribpjs") = "-" Then
                sLanjut = False
                Exit For
            Else
                sLanjut = True
            End If
        Next

        If sLanjut = False Then
            MsgBox("Item/kategoribpjs masih ada yang kosong!!!", MsgBoxStyle.Exclamation, Me.Text)
            XtraTabControl1.SelectedTabPage = tab2
        End If

        grvRincian.BestFitColumns()
    End Sub
#End Region
#Region "EKLAIM"
    Private Sub btnValidasiTB_Click(sender As Object, e As EventArgs) Handles btnValidasiTB.Click
        Try
            If txtNOMORSITB.Text <> "" Then
                Dim jsonDecode = JObject.Parse(oGetGrouper.fn_ValidasiNomorRegisterSITB(sEklaim_Url, sEklaim_Generate, txtNOMORSITB.Text, txtNoSEP.Text))
                Dim sDataDuplicate As String = String.Empty
                Dim smessage As String = String.Empty

                sDataDuplicate = jsonDecode("metadata")("code").ToString
                smessage = jsonDecode("metadata")("message").ToString

                MsgBox("Validasi Nomor Register SITB" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Information, Me.Text)

            Else
                MsgBox("Nomor SITB Masih Kosong", MsgBoxStyle.Information, Me.Text)
            End If

        Catch oErr As Exception
            MsgBox("Validasi Nomor Register SITB: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_MengisiUpdateDataKlaimKlaim() As Boolean
        Try
            Dim jsonEncode As String = String.Empty
            Dim upgrade_class_class As String = String.Empty
            If chkNaikKelas.Checked = True Then
                If rbKELASPELAYANAN.SelectedIndex = 0 Then
                    upgrade_class_class = ""
                ElseIf rbKELASPELAYANAN.SelectedIndex = 1 Then
                    upgrade_class_class = "kelas_2"
                ElseIf rbKELASPELAYANAN.SelectedIndex = 2 Then
                    upgrade_class_class = "kelas_1"
                ElseIf rbKELASPELAYANAN.SelectedIndex = 3 Then
                    upgrade_class_class = "vip"
                Else
                    upgrade_class_class = "vvip"
                End If
            End If

            jsonEncode = "{" & """metadata"": { " & """method"": " & """set_claim_data"",    " & """nomor_sep"": """ & txtNoSEP.Text & """  },  "
            jsonEncode &= """data"": {    " & """nomor_sep"": """ & txtNoSEP.Text & """,    "
            jsonEncode &= """nomor_kartu"": """ & txtNoPeserta.Text & """,    "
            jsonEncode &= """tgl_masuk"": """ & deDATEMASUK.DateTime.ToString("yyyy-MM-dd HH:mm:ss") & """,    "
            jsonEncode &= """tgl_pulang"": """ & deDATEPULANG.DateTime.ToString("yyyy-MM-dd HH:mm:ss") & """,    "
            jsonEncode &= """cara_masuk"": """ & grdCARAMASUK.EditValue & """,    "
            jsonEncode &= """jenis_rawat"": """ & IIf(rbCategory.SelectedIndex = 0, "2", "1") & """,    "
            jsonEncode &= """kelas_rawat"": """ & IIf(rbKELASHAK.SelectedIndex = 2, "1", IIf(rbKELASHAK.SelectedIndex = 1, "2", "3")) & """,    "
            jsonEncode &= """adl_sub_acute"": """ & IIf(txtADLScore_SubAcute.Text = "-", "", IIf(txtADLScore_SubAcute.Text = "0", "", txtADLScore_Chronic.Text)) & """,    "
            jsonEncode &= """adl_chronic"": """ & IIf(txtADLScore_Chronic.Text = "-", "", IIf(txtADLScore_Chronic.Text = "0", "", txtADLScore_Chronic.Text)) & """,    "
            jsonEncode &= """icu_indikator"": """ & IIf(txtRAWATINTENSIF_HARI.Text = 0, "0", "1") & """,    "
            jsonEncode &= """icu_los"": """ & txtRAWATINTENSIF_HARI.Text & """,    "
            jsonEncode &= """ventilator_hour"": """ & txtVENTILATOR.Text & """,    "
            jsonEncode &= """ventilator"": {      " & """use_ind"": """ & cboVENTILATOR_use_ind.Text & """,      "
            jsonEncode &= """start_dttm"": """ & IIf(cboVENTILATOR_use_ind.Text = "0", "", deVENTILATOR_start_dttm.DateTime.ToString("yyyy-mm-dd hh:mm:ss")) & """,      "
            jsonEncode &= """stop_dttm"": """ & IIf(cboVENTILATOR_use_ind.Text = "0", "", deVENTILATOR_stop_dttm.DateTime.ToString("yyyy-mm-dd hh:mm:ss")) & """    },    "
            jsonEncode &= """upgrade_class_ind"": """ & IIf(chkNaikKelas.Checked = True, "1", "0") & """,    "
            jsonEncode &= """upgrade_class_class"": """ & upgrade_class_class & """,    "
            jsonEncode &= """upgrade_class_los"": """ & IIf(chkNaikKelas.Checked = True, txtLAMA.Text, "0") & """,    "
            jsonEncode &= """upgrade_class_payor"": """ & "" & """,    "
            jsonEncode &= """add_payment_pct"": """ & "" & """,    "
            jsonEncode &= """birth_weight"": """ & CInt(txtBeratBadan.Text) & """,    "
            jsonEncode &= """sistole"": """ & txtSISTOLE.Text & """,    "
            jsonEncode &= """diastole"": """ & txtDIASTOLE.Text & """,    "
            jsonEncode &= """discharge_status"": """ & grdCaraKeluar.EditValue & """,    "

            Dim listdiagnosakode As New List(Of String)
            For i As Integer = 0 To grvDiagnosa.RowCount - 2
                listdiagnosakode.Add(grvDiagnosa.GetRowCellValue(i, colkddiagnosa))
            Next

            Dim listprosedurkode As New List(Of String)
            For i As Integer = 0 To grvProsedur.RowCount - 2
                listprosedurkode.Add(grvProsedur.GetRowCellValue(i, colkdprpsedur))
            Next

            jsonEncode &= """diagnosa"": """ & String.Join("#", listdiagnosakode.ToArray) & """,    "
            jsonEncode &= """procedure"": """ & String.Join("#", listprosedurkode.ToArray) & """,    "
            jsonEncode &= """diagnosa_inagrouper"": """ & txtKDDIAGNOSA_V6.Text & """,    "
            jsonEncode &= """procedure_inagrouper"": """ & txtKDPROSEDUR_V6.Text & """,    "

            'If chkINA.Checked = True Then
            '    jsonEncode &= """diagnosa_inagrouper"": """ & String.Join("#", listdiagnosakode.ToArray) & """,    "
            '    jsonEncode &= """procedure_inagrouper"": """ & String.Join("#", listprosedurkode.ToArray) & """,    "
            'Else
            '    jsonEncode &= """diagnosa_inagrouper"": """ & "" & """,    "
            '    jsonEncode &= """procedure_inagrouper"": """ & "" & """,    "
            'End If

            jsonEncode &= """tarif_rs"": {      " & """prosedur_non_bedah"": """ & CInt(txtProsedurNonBedah.Text) & """,      "
            jsonEncode &= """prosedur_bedah"": """ & CInt(txtProsedurBedah.Text) & """,      "
            jsonEncode &= """konsultasi"": """ & CInt(txtKonsultasi.Text) & """,      "
            jsonEncode &= """tenaga_ahli"": """ & CInt(txtTenagaAhli.Text) & """,      "
            jsonEncode &= """keperawatan"": """ & CInt(txtKeperawatan.Text) & """,      "
            jsonEncode &= """penunjang"": """ & CInt(txtPenunjang.Text) & """,      "
            jsonEncode &= """radiologi"": """ & CInt(txtRadiologi.Text) & """,      "
            jsonEncode &= """laboratorium"": """ & CInt(txtLaboratorium.Text) & """,      "
            jsonEncode &= """pelayanan_darah"": """ & CInt(txtPelayananDarah.Text) & """,      "
            jsonEncode &= """rehabilitasi"": """ & CInt(txtRehabilitasi.Text) & """,      "
            jsonEncode &= """kamar"": """ & CInt(txtKamarAkomodasi.Text) & """,      "
            jsonEncode &= """rawat_intensif"": """ & CInt(txtRawatIntensif.Text) & """,   "
            jsonEncode &= """obat"": """ & CInt(txtObat.Text) & """,   "
            jsonEncode &= """obat_kronis"": """ & CInt(txtObatKronis.Text) & """, "
            jsonEncode &= """obat_kemoterapi"": """ & CInt(txtObatKemoTerapi.Text) & """,      "
            jsonEncode &= """alkes"": """ & CInt(txtAlkes.Text) & """,      "
            jsonEncode &= """bmhp"": """ & CInt(txtBMHP.Text) & """,      "
            jsonEncode &= """sewa_alat"": """ & CInt(txtSewaAlat.Text) & """    },    "
            jsonEncode &= """pemulasaraan_jenazah"": """ & "0" & """,      "
            jsonEncode &= """kantong_jenazah"": """ & "0" & """,      "
            jsonEncode &= """peti_jenazah"": """ & "0" & """,      "
            jsonEncode &= """plastik_erat"": """ & "0" & """,      "
            jsonEncode &= """desinfektan_jenazah"": """ & "0" & """,      "
            jsonEncode &= """mobil_jenazah"": """ & "0" & """,      "
            jsonEncode &= """desinfektan_mobil_jenazah"": """ & "0" & """,      "
            jsonEncode &= """covid19_status_cd"": """ & "" & """,      "
            jsonEncode &= """nomor_kartu_t"": """ & "" & """,      "
            jsonEncode &= """episodes"": """ & "" & """,      "
            jsonEncode &= """covid19_cc_ind"": """ & "" & """,      "
            jsonEncode &= """covid19_rs_darurat_ind"": """ & "" & """,      "
            jsonEncode &= """covid19_co_insidense_ind"": """ & "" & """,      "
            jsonEncode &= """covid19_penunjang_pengurang"": {      " & """lab_asam_laktat"": """ & "1" & """,      "
            jsonEncode &= """lab_procalcitonin"": """ & "1" & """,      "
            jsonEncode &= """lab_crp"": """ & "1" & """,      "
            jsonEncode &= """lab_kultur"": """ & "1" & """,      "
            jsonEncode &= """lab_d_dimer"": """ & "1" & """,      "
            jsonEncode &= """lab_pt"": """ & "1" & """,      "
            jsonEncode &= """lab_aptt"": """ & "1" & """,      "
            jsonEncode &= """lab_waktu_pendarahan"": """ & "1" & """,      "
            jsonEncode &= """lab_anti_hiv"": """ & "1" & """,      "
            jsonEncode &= """lab_analisa_gas"": """ & "1" & """,      "
            jsonEncode &= """lab_albumin"": """ & "1" & """,      "
            jsonEncode &= """rad_thorax_ap_pa"": """ & "1" & """    },    "
            jsonEncode &= """terapi_konvalesen"": """ & "0" & """,      "
            jsonEncode &= """akses_naat"": """ & "" & """,      "
            jsonEncode &= """isoman_ind"": """ & "0" & """,      "
            jsonEncode &= """bayi_lahir_status_cd"": """ & "" & """,      "
            jsonEncode &= """dializer_single_use"": """ & IIf(rbDIALIZERSINGGLEUSE.SelectedIndex = 0, "", IIf(rbDIALIZERSINGGLEUSE.SelectedIndex = 1, "0", "1")) & """,    "
            If txtKANTONGDARAH.Text <> "0" Then
                jsonEncode &= """kantong_darah"": """ & txtKANTONGDARAH.Text & """,    "
            End If

            jsonEncode &= """alteplase_ind"": """ & cboALTEPLASE_IND.Text & """,    "

            Dim sCek As Boolean = False
            If chkAPGAR.Checked = True Then
                sCek = True
            End If
            If chkPERSALINAN.Checked = True Then
                sCek = True
            End If

            If sCek = True Then
                jsonEncode &= """apgar"": { "
                jsonEncode &= """menit_1"": { "
                jsonEncode &= """appearance"": """ & cboMenit1_appearance.Text & """, "
                jsonEncode &= """pulse"": """ & cboMenit1_Pulse.Text & """, "
                jsonEncode &= """grimace"": """ & cboMenit1_Grimace.Text & """, "
                jsonEncode &= """activity"": """ & cboMenit1_Activity.Text & """, "
                jsonEncode &= """respiration"": """ & cboMenit1_Respiration.Text & """ } , "
                jsonEncode &= """menit_5"": { "
                jsonEncode &= """appearance"": """ & cboMenit5_appearance.Text & """, "
                jsonEncode &= """pulse"": """ & cboMenit5_Pulse.Text & """, "
                jsonEncode &= """grimace"": """ & cboMenit5_Grimace.Text & """, "
                jsonEncode &= """activity"": """ & cboMenit5_Activity.Text & """, "
                jsonEncode &= """respiration"": """ & cboMenit5_Respiration.Text & """ } }, "

                jsonEncode &= """persalinan"": { "
                jsonEncode &= """usia_kehamilan"": """ & txtPERSALINAN_usia_kehamilan.Text & """, "
                jsonEncode &= """gravida"": """ & txtPERSALINAN_gravida.Text & """, "
                jsonEncode &= """partus"": """ & txtPERSALINAN_partus.Text & """, "
                jsonEncode &= """abortus"": """ & txtPERSALINAN_abortus.Text & """, "
                jsonEncode &= """onset_kontraksi"": """ & cboPERSALINAN_onset_kontraksi.Text & """, "
                jsonEncode &= """delivery"": [  "

                Dim list As New List(Of String)

                Dim sSeq As Integer = 0
                For i As Integer = 0 To grvPersalinan.RowCount - 2
                    sSeq = i
                Next

                For i As Integer = 0 To grvPersalinan.RowCount - 2
                    If grvPersalinan.GetRowCellValue(i, colshk_spesimen_ambil) = "ya" Then
                        list.Add("{ ")
                        list.Add("""delivery_sequence"": """ & grvPersalinan.GetRowCellValue(i, coldelivery_sequence) & """, ")
                        list.Add("""delivery_method"": """ & grvPersalinan.GetRowCellValue(i, coldelivery_method) & """, ")
                        list.Add("""delivery_dttm"": """ & CDate(grvPersalinan.GetRowCellValue(i, coldelivery_dttm)).ToString("yyyy-MM-dd HH:mm:ss") & """, ")
                        list.Add("""letak_janin"": """ & grvPersalinan.GetRowCellValue(i, colletak_janin) & """, ")
                        list.Add("""kondisi"": """ & grvPersalinan.GetRowCellValue(i, colkondisi) & """, ")
                        list.Add("""use_manual"": """ & grvPersalinan.GetRowCellValue(i, coluse_manual) & """, ")
                        list.Add("""use_forcep"": """ & grvPersalinan.GetRowCellValue(i, coluse_forcep) & """, ")
                        list.Add("""use_vacuum"": """ & grvPersalinan.GetRowCellValue(i, coluse_vacuum) & """, ")
                        list.Add("""shk_spesimen_ambil"": """ & grvPersalinan.GetRowCellValue(i, colshk_spesimen_ambil) & """, ")
                        list.Add("""shk_lokasi"": """ & grvPersalinan.GetRowCellValue(i, colshk_lokasi) & """, ")
                        list.Add("""shk_spesimen_dttm"": """ & CDate(grvPersalinan.GetRowCellValue(i, colshk_spesimen_dttm)).ToString("yyyy-MM-dd HH:mm:ss") & """ ")
                        If sSeq = i Then
                            list.Add("} ")
                        Else
                            list.Add("}, ")
                        End If
                    Else
                        list.Add("{ ")
                        list.Add("""delivery_sequence"": """ & grvPersalinan.GetRowCellValue(i, coldelivery_sequence) & """, ")
                        list.Add("""delivery_method"": """ & grvPersalinan.GetRowCellValue(i, coldelivery_method) & """, ")
                        list.Add("""delivery_dttm"": """ & CDate(grvPersalinan.GetRowCellValue(i, coldelivery_dttm)).ToString("yyyy-MM-dd HH:mm:ss") & """, ")
                        list.Add("""letak_janin"": """ & grvPersalinan.GetRowCellValue(i, colletak_janin) & """, ")
                        list.Add("""kondisi"": """ & grvPersalinan.GetRowCellValue(i, colkondisi) & """, ")
                        list.Add("""use_manual"": """ & grvPersalinan.GetRowCellValue(i, coluse_manual) & """, ")
                        list.Add("""use_forcep"": """ & grvPersalinan.GetRowCellValue(i, coluse_forcep) & """, ")
                        list.Add("""use_vacuum"": """ & grvPersalinan.GetRowCellValue(i, coluse_vacuum) & """, ")
                        list.Add("""shk_spesimen_ambil"": """ & grvPersalinan.GetRowCellValue(i, colshk_spesimen_ambil) & """, ")
                        list.Add("""shk_alasan"": """ & grvPersalinan.GetRowCellValue(i, colshk_alasan) & """ ")
                        If sSeq = i Then
                            list.Add("} ")
                        Else
                            list.Add("}, ")
                        End If
                    End If
                Next

                jsonEncode &= String.Join("", list.ToArray)

                jsonEncode &= " ] }, "

            End If

            jsonEncode &= """tarif_poli_eks"": """ & CInt(txtTarifBiayaTambahan.Text) & """,    "
            If grdDPJP.Text.Contains("Rudy Dwi Laksono") Then
                jsonEncode &= """nama_dokter"": """ & "dr. Rudy Dwi Laksono, SpPD" & """,    "
            Else
                jsonEncode &= """nama_dokter"": """ & grdDPJP.Text & """,    "
            End If
            jsonEncode &= """kode_tarif"": """ & grdJenisTarif.EditValue & """,    "
            jsonEncode &= """payor_id"": """ & "3" & """,    "
            jsonEncode &= """payor_cd"": """ & "JKN" & """,    "
            jsonEncode &= """cob_cd"": """ & "" & """,    "
            jsonEncode &= """coder_nik"": """ & sUserKTP & """  } } "

            Dim jsonDecode = JObject.Parse(oGetGrouper.fn_MengisiUpdateDataKlaim(sEklaim_Url, sEklaim_Generate, jsonEncode))
            Dim sDataDuplicate = String.Empty
            Dim smessage As String = String.Empty

            sDataDuplicate = jsonDecode("metadata")("code").ToString
            smessage = jsonDecode("metadata")("message").ToString

            fn_MengisiUpdateDataKlaimKlaim = False

            If sDataDuplicate = "200" Then
                fn_MengisiUpdateDataKlaimKlaim = True
            Else
                If smessage = "Klaim sudah final" Then
                    MsgBox(sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
                Else
                    MsgBox("Gagal Grouper" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If
        Catch oErr As Exception
            MsgBox("Mengisi Update Data Klaim: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    'Private Function fn_GroupingStage() As Boolean
    '    Try
    '        fn_GroupingStage = False

    '        Dim listdiagnosakode As New List(Of String)
    '        For i As Integer = 0 To grvDiagnosa.RowCount - 2
    '            listdiagnosakode.Add(grvDiagnosa.GetRowCellValue(i, colkddiagnosa))
    '        Next

    '        If listdiagnosakode.Count <= 0 Then
    '            MsgBox("Diagnosa Masih Kosong", MsgBoxStyle.Information, Me.Text)
    '            Exit Function
    '        End If

    '        Dim jsonDecode = JObject.Parse(oGetGrouper.fn_GroupingStage1(sEklaim_Url, sEklaim_Generate, txtNoSEP.Text))
    '        Dim sDataDuplicate As String = String.Empty
    '        Dim smessage As String = String.Empty

    '        sDataDuplicate = jsonDecode("metadata")("code").ToString
    '        smessage = jsonDecode("metadata")("message").ToString

    '        'txtSPECIAL2.Text = grdKDSPECIAL2.EditValue

    '        Dim list As New List(Of String)

    '        If grdKDSPECIAL1.Text <> "" Then
    '            list.Add(grdKDSPECIAL1.EditValue)
    '        End If
    '        If grdKDSPECIAL2.Text <> "" Then
    '            list.Add(grdKDSPECIAL2.EditValue)
    '        End If
    '        If grdKDSPECIAL3.Text <> "" Then
    '            list.Add(grdKDSPECIAL3.EditValue)
    '        End If
    '        If grdKDSPECIAL4.Text <> "" Then
    '            list.Add(grdKDSPECIAL4.EditValue)
    '        End If

    '        If sDataDuplicate = "200" Then
    '            Try
    '                txtCodeCBG.Text = jsonDecode("response")("cbg")("code").ToString
    '                txtDescriptionCBG.Text = jsonDecode("response")("cbg")("description").ToString
    '                txtTarifCBG.Text = jsonDecode("response")("cbg")("tariff").ToString
    '            Catch ex As Exception
    '            End Try
    '            Try
    '                txtCodeSubAcute.Text = jsonDecode("response")("sub_acute")("code").ToString
    '                txtDescriptionSubAcute.Text = jsonDecode("response")("sub_acute")("description").ToString
    '                txtTarifSubAcute.Text = jsonDecode("response")("sub_acute")("tariff").ToString
    '            Catch ex As Exception
    '            End Try
    '            Try
    '                txtCodeChronic.Text = jsonDecode("response")("chronic")("code").ToString
    '                txtDescriptionChronic.Text = jsonDecode("response")("chronic")("description").ToString
    '                txtTarifChronic.Text = jsonDecode("response")("chronic")("tariff").ToString
    '            Catch ex As Exception
    '            End Try

    '            If jsonDecode("special_cmg_option") IsNot Nothing Then
    '                Dim sPecialCMG As Boolean = False

    '                Dim table1 As DataTable
    '                Dim table2 As DataTable
    '                Dim table3 As DataTable
    '                Dim table4 As DataTable

    '                table1 = New DataTable("M_SPECIAL1")
    '                table1.Columns.Add("code")
    '                table1.Columns.Add("description")

    '                table2 = New DataTable("M_SPECIAL2")
    '                table2.Columns.Add("code")
    '                table2.Columns.Add("description")

    '                table3 = New DataTable("M_SPECIAL3")
    '                table3.Columns.Add("code")
    '                table3.Columns.Add("description")

    '                table4 = New DataTable("M_SPECIAL4")
    '                table4.Columns.Add("code")
    '                table4.Columns.Add("description")

    '                For Each item In jsonDecode("special_cmg_option")
    '                    sPecialCMG = True
    '                    If item("type") = "Special Procedure" Then
    '                        table1.Rows.Add(New String() {item("code"), item("description")})
    '                    ElseIf item("type") = "Special Prosthesis" Then
    '                        table2.Rows.Add(New String() {item("code"), item("description")})
    '                    ElseIf item("type") = "Special Investigation" Then
    '                        table3.Rows.Add(New String() {item("code"), item("description")})
    '                    ElseIf item("type") = "Special Drug" Then
    '                        table4.Rows.Add(New String() {item("code"), item("description")})
    '                    End If
    '                Next

    '                grdKDSPECIAL1.Properties.DataSource = table1
    '                grdKDSPECIAL1.Properties.ValueMember = "code"
    '                grdKDSPECIAL1.Properties.DisplayMember = "description"

    '                grdKDSPECIAL2.Properties.DataSource = table2
    '                grdKDSPECIAL2.Properties.ValueMember = "code"
    '                grdKDSPECIAL2.Properties.DisplayMember = "description"

    '                grdKDSPECIAL3.Properties.DataSource = table3
    '                grdKDSPECIAL3.Properties.ValueMember = "code"
    '                grdKDSPECIAL3.Properties.DisplayMember = "description"

    '                grdKDSPECIAL4.Properties.DataSource = table4
    '                grdKDSPECIAL4.Properties.ValueMember = "code"
    '                grdKDSPECIAL4.Properties.DisplayMember = "description"

    '                If sPecialCMG = True Then
    '                    If MsgBox("Terdapat Spesial cmg, Apakah Akan Lanjut?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.Yes Then
    '                        fn_GroupingStage = True
    '                    End If
    '                Else
    '                    fn_GroupingStage = True
    '                End If

    '                lGROUPER_HASILGROUPER.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
    '            Else
    '                fn_GroupingStage = True
    '                lGROUPER_HASILGROUPER.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
    '            End If
    '        Else
    '            MsgBox("Gagal Grouper 1" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
    '        End If
    '    Catch oErr As Exception
    '        MsgBox("Grouper: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Function
    'Private Function fn_FinalisasiKlaim(ByVal Pesan As Boolean) As Boolean
    '    Try
    '        fn_FinalisasiKlaim = False

    '        Dim jsonDecode = JObject.Parse(oGetGrouper.fn_UntukFinalisasiKlaim(sEklaim_Url, sEklaim_Generate, txtNoSEP.Text, sUserKTP))
    '        Dim sDataDuplicate As String = String.Empty
    '        Dim smessage As String = String.Empty

    '        sDataDuplicate = jsonDecode("metadata")("code").ToString
    '        smessage = jsonDecode("metadata")("message").ToString

    '        If sDataDuplicate = "200" Then
    '            If fn_Save("FINAL KLAIM", "", "") = False Then
    '                MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
    '            Else
    '                MsgBox("Save " & txtNoSEP.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
    '                'fn_CetakLIP(txtNoSEP.Text, sKDREG1, 1, deDATEMASUK.DateTime)
    '                Me.Close()
    '            End If
    '            fn_FinalisasiKlaim = True
    '        End If

    '        If Pesan = True Then
    '            MsgBox(sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
    '        End If
    '    Catch oErr As Exception
    '        MsgBox("Simpan: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Function
    'Private Function fn_KirimOnline(ByVal Pesan As Boolean) As Boolean
    '    Try
    '        fn_KirimOnline = False

    '        Dim jsonDecode = JObject.Parse(oGetGrouper.fn_MengirimKlaimIndividualKeDataCenter(sEklaim_Url, sEklaim_Generate, txtNoSEP.Text))
    '        Dim sDataDuplicate As String = String.Empty
    '        Dim smessage As String = String.Empty

    '        sDataDuplicate = jsonDecode("metadata")("code").ToString
    '        smessage = jsonDecode("metadata")("message").ToString

    '        If sDataDuplicate = "200" Then
    '            fn_KirimOnline = True

    '            If fn_Save("KIRIM ONLINE", "", "") = False Then
    '                MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
    '            Else
    '                MsgBox("Save " & txtNoSEP.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
    '                'fn_CetakLIP(txtNoSEP.Text, sKDREG1, 1, deDATEMASUK.DateTime)
    '                Me.Close()
    '            End If
    '        End If

    '        If Pesan = True Then
    '            MsgBox(sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
    '        End If
    '    Catch oErr As Exception
    '        MsgBox("Kirim Online: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Function
    Private Sub chkAPGAR_CheckedChanged(sender As Object, e As EventArgs) Handles chkAPGAR.CheckedChanged
        If isLoad = True Then
            fn_LoadApgar(chkAPGAR.Checked)
        End If
    End Sub
    Private Sub fn_LoadApgar(ByVal Parameter As Boolean)
        If Parameter = False Then
            lGROUPER_APGAR.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

            lApgarMenit1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lApgarMenit5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

            cboMenit1_Pulse.ResetText()
            cboMenit1_Grimace.ResetText()
            cboMenit1_Activity.ResetText()
            cboMenit1_Respiration.ResetText()
            cboMenit5_appearance.ResetText()
            cboMenit5_Pulse.ResetText()
            cboMenit5_Grimace.ResetText()
            cboMenit5_Activity.ResetText()
            cboMenit5_Respiration.ResetText()
        Else
            lGROUPER_APGAR.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            lApgarMenit1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lApgarMenit5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            If cboMenit1_Pulse.Text = "" Then
                cboMenit1_Pulse.Text = "0"
            End If
            If cboMenit1_Grimace.Text = "" Then
                cboMenit1_Grimace.Text = "0"
            End If
            If cboMenit1_Activity.Text = "" Then
                cboMenit1_Activity.Text = "0"
            End If
            If cboMenit1_Respiration.Text = "" Then
                cboMenit1_Respiration.Text = "0"
            End If
            If cboMenit5_appearance.Text = "" Then
                cboMenit5_appearance.Text = "0"
            End If
            If cboMenit5_Pulse.Text = "" Then
                cboMenit5_Pulse.Text = "0"
            End If
            If cboMenit5_Grimace.Text = "" Then
                cboMenit5_Grimace.Text = "0"
            End If
            If cboMenit5_Activity.Text = "" Then
                cboMenit5_Activity.Text = "0"
            End If
            If cboMenit5_Respiration.Text = "" Then
                cboMenit5_Respiration.Text = "0"
            End If
        End If
    End Sub
    Private Sub fn_LoadPersalinan(ByVal Parameter As Boolean)
        If chkPERSALINAN.Checked = False Then
            lPersalinan.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lPersalinanTmabah1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lPersalinanTambah2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

            txtPERSALINAN_usia_kehamilan.ResetText()
            txtPERSALINAN_gravida.ResetText()
            txtPERSALINAN_partus.ResetText()
            txtPERSALINAN_abortus.ResetText()
            cboPERSALINAN_onset_kontraksi.ResetText()
        Else
            lPersalinan.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lPersalinanTmabah1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lPersalinanTambah2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            If txtPERSALINAN_usia_kehamilan.Text = "" Then
                txtPERSALINAN_usia_kehamilan.Text = "0"
            End If
            If txtPERSALINAN_gravida.Text = "" Then
                txtPERSALINAN_gravida.Text = "0"
            End If
            If txtPERSALINAN_partus.Text = "" Then
                txtPERSALINAN_partus.Text = "0"
            End If
            If txtPERSALINAN_abortus.Text = "" Then
                txtPERSALINAN_abortus.Text = "0"
            End If
            If cboPERSALINAN_onset_kontraksi.Text = "" Then
                cboPERSALINAN_onset_kontraksi.ResetText()
            End If
        End If
    End Sub
    Private Sub chkPERSALINAN_CheckedChanged(sender As Object, e As EventArgs) Handles chkPERSALINAN.CheckedChanged
        If isLoad = True Then
            fn_LoadPersalinan(chkPERSALINAN.Checked)
        End If
    End Sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Dim frmPersalinan As New frmPersalinan
        Try
            Dim sNomor As Integer = 1

            For i As Integer = 0 To grvPersalinan.RowCount - 2
                sNomor += 1
            Next

            frmPersalinan.ShowDialog(Me)

            If sdelivery_method <> String.Empty Then
                grvPersalinan.Focus()
                grvPersalinan.AddNewRow()
                grvPersalinan.SetFocusedRowCellValue(coldelivery_sequence, sNomor)
                grvPersalinan.SetFocusedRowCellValue(coldelivery_method, sdelivery_method)
                grvPersalinan.SetFocusedRowCellValue(coldelivery_dttm, sdelivery_dttm)
                grvPersalinan.SetFocusedRowCellValue(colletak_janin, sletak_janin)
                grvPersalinan.SetFocusedRowCellValue(colkondisi, skondisi)

                grvPersalinan.SetFocusedRowCellValue(coluse_manual, suse_manual)
                grvPersalinan.SetFocusedRowCellValue(coluse_forcep, suse_forcep)
                grvPersalinan.SetFocusedRowCellValue(coluse_vacuum, suse_vacuum)

                grvPersalinan.SetFocusedRowCellValue(colshk_spesimen_ambil, sshk_spesimen_ambil)
                grvPersalinan.SetFocusedRowCellValue(colshk_spesimen_dttm, sshk_spesimen_dttm)

                If sshk_spesimen_ambil = "ya" Then
                    grvPersalinan.SetFocusedRowCellValue(colshk_lokasi, sshk_lokasi)
                    grvPersalinan.SetFocusedRowCellValue(colshk_alasan, "")
                Else
                    grvPersalinan.SetFocusedRowCellValue(colshk_lokasi, "")
                    grvPersalinan.SetFocusedRowCellValue(colshk_alasan, sshk_alasan)
                End If

                grvPersalinan.UpdateCurrentRow()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmPersalinan Is Nothing Then frmPersalinan.Dispose()
            frmPersalinan = Nothing
        End Try
    End Sub
    Private Sub cboVENTILATOR_use_ind_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboVENTILATOR_use_ind.SelectedIndexChanged
        If isLoad = True Then
            If cboVENTILATOR_use_ind.Text = "0" Then
                lVentilatorDate1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lVentilatorDate2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Else
                lVentilatorDate1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lVentilatorDate2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
        End If
    End Sub
    Private Sub lblDiagnosaiDRG_Click(sender As Object, e As EventArgs) Handles lblDiagnosaiDRG.Click
        Dim dsStatusGrouper = oStatusGrouper.GetData(sNoId)

        If dsStatusGrouper IsNot Nothing Then
            If dsStatusGrouper.STATUS = "fn_00NEWCLAIM" Then

            ElseIf dsStatusGrouper.STATUS = "fn_06GROUPINGIDRG" Then

            ElseIf dsStatusGrouper.STATUS = "fn_08REEDIT" Then

            Else
                Exit Sub
            End If
        Else
            Exit Sub
        End If

        If listdiagnosakodeidRG.Count > 0 Then
            Dim frmRubahDiagnosa As New frmRubahDiagnosa
            Try
                sGantiDiagnosa = False

                frmRubahDiagnosa.fn_loadDiagnosa(listdiagnosakodeidRG, sNoId)
                frmRubahDiagnosa.ShowDialog(Me)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If sGantiDiagnosa = True Then
                    lblDiagnosaiDRG.ResetText()
                    lblDiagnosaiDRG_1.ResetText()
                    lblDiagnosaiDRG_2.ResetText()

                    Dim listkode As New List(Of String)
                    Dim listkode1 As New List(Of String)
                    Dim listkode2 As New List(Of String)

                    For Each xloop In listdiagnosakodeidRG.OrderBy(Function(x) x.seq)
                        listkode.Add(xloop.memo)
                        listkode1.Add(xloop.kddiagnosa)
                        listkode2.Add(xloop.kategori)
                    Next

                    lblDiagnosaiDRG.Text = String.Join(vbCrLf, listkode.ToArray)
                    lblDiagnosaiDRG_1.Text = String.Join(vbCrLf, listkode1.ToArray)
                    lblDiagnosaiDRG_2.Text = String.Join(vbCrLf, listkode2.ToArray)
                End If
            End Try

        End If
    End Sub
    Private Sub lblProseduriDRG_Click(sender As Object, e As EventArgs) Handles lblProseduriDRG.Click
        Dim dsStatusGrouper = oStatusGrouper.GetData(sNoId)

        If dsStatusGrouper IsNot Nothing Then
            If dsStatusGrouper.STATUS = "fn_00NEWCLAIM" Then

            ElseIf dsStatusGrouper.STATUS = "fn_06GROUPINGIDRG" Then

            ElseIf dsStatusGrouper.STATUS = "fn_08REEDIT" Then

            Else
                Exit Sub
            End If
        Else
            Exit Sub
        End If

        If listprosedurkodeidRG.Count > 0 Then
            Dim frmRubahProsedur As New frmRubahProsedur
            Try
                sGantiProsedur = False

                frmRubahProsedur.fn_loadDiagnosa(listprosedurkodeidRG, sNoId)
                frmRubahProsedur.ShowDialog(Me)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If sGantiProsedur = True Then
                    lblProseduriDRG.ResetText()
                    lblProseduriDRG_1.ResetText()
                    lblProseduriDRG_2.ResetText()

                    Dim listkode As New List(Of String)
                    Dim listkode1 As New List(Of String)
                    Dim listkode2 As New List(Of String)

                    For Each xloop In listprosedurkodeidRG.OrderBy(Function(x) x.seq)
                        listkode.Add(xloop.memo)
                        listkode1.Add(xloop.kdprpsedur)
                        If xloop.jumlah > 1 Then
                            listkode2.Add(" x " & xloop.jumlah)
                        Else
                            listkode2.Add("")
                        End If
                    Next

                    lblProseduriDRG.Text = String.Join(vbCrLf, listkode.ToArray)
                    lblProseduriDRG_1.Text = String.Join(vbCrLf, listkode1.ToArray)
                    lblProseduriDRG_2.Text = String.Join(vbCrLf, listkode2.ToArray)
                End If
            End Try

        End If
    End Sub
    Private Sub btnImportCoding_Click(sender As Object, e As EventArgs) Handles btnImportCoding.Click
        fn_09IDRGTOINACBGIMPORT()
    End Sub

    Private Sub TextEdit2_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit2.EditValueChanged

    End Sub
#End Region
End Class