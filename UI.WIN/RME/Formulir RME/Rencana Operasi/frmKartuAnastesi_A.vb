Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq
Imports System.Data.SqlClient

Public Class frmKartuAnastesi_A
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_OK_KARTU_ANESTESI_A As New EMedrek.clsS_DIGITAL_OK_KARTU_ANESTESI_A
    Private sNoRekamMedis As String
    Private sNoId As String
    Private sNAMADOKTER As String
    Private sKODEDOKTER As String
    Private sDATEMASUK As DateTime = Now
#End Region
#Region "Function"

    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDKUNJUNGAN As String, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId

        Dim oKunjungan As New Admission.clsPendaftaran_Kunjungan
        Dim dsPendaftaran = oKunjungan.GetDatabyKodeKunjungan(KDKUNJUNGAN)

        If dsPendaftaran IsNot Nothing Then
            txtNoPasien.Text = dsPendaftaran.KDCUSTOMER
            txtNamaPasien.Text = dsPendaftaran.NAMAPASIEN
            txtUmur.Text = dsPendaftaran.UMUR
            txtTanggalDaftar.Text = Convert.ToDateTime(dsPendaftaran.DATE).ToString("dd-MM-yyyy HH:mm:ss")
            txtNoRegister.Text = dsPendaftaran.KDKUNJUNGAN
            txtTujuan.Text = dsPendaftaran.KDDEPARTMENT_NAMA
            txtDokter.Text = dsPendaftaran.KDDOCTOR_NAMA
            sNAMADOKTER = dsPendaftaran.KDDOCTOR_NAMA
            sKODEDOKTER = dsPendaftaran.KDDOCTOR
            sNoRekamMedis = dsPendaftaran.KDCUSTOMER
            TextEdit4.Text = dsPendaftaran.KDDEPARTMENT_NAMA
            grdRuangan.EditValue = dsPendaftaran.KDDEPARTMENT

            sDATEMASUK = dsPendaftaran.DATE
        End If

    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "Kartu Anastesi A"

            btnSaveClosee.Caption = "Simpan"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'sCode = txtMEMO.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()

        fn_Doctor()
        fn_Asisten()
        fn_Department()

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

        'txtPERAWAT_3.Properties.ReadOnly = False
        TextEdit1.Properties.ReadOnly = False
        TextEdit2.Properties.ReadOnly = False
        TextEdit3.Properties.ReadOnly = False
        TextEdit4.Properties.ReadOnly = False
        TextEdit5.Properties.ReadOnly = False
        TextEdit6.Properties.ReadOnly = False
        TextEdit7.Properties.ReadOnly = False
        TextEdit8.Properties.ReadOnly = False
        TextEdit9.Properties.ReadOnly = False
        TextEdit10.Properties.ReadOnly = False
        TextEdit11.Properties.ReadOnly = False
        TextEdit12.Properties.ReadOnly = False
        TextEdit13.Properties.ReadOnly = False
        TextEdit14.Properties.ReadOnly = False
        TextEdit15.Properties.ReadOnly = False
        TextEdit16.Properties.ReadOnly = False
        TextEdit17.Properties.ReadOnly = False
        TextEdit18.Properties.ReadOnly = False
        TextEdit19.Properties.ReadOnly = False
        TextEdit20.Properties.ReadOnly = False
        TextEdit21.Properties.ReadOnly = False
        TextEdit22.Properties.ReadOnly = False
        TextEdit23.Properties.ReadOnly = False
        TextEdit24.Properties.ReadOnly = False
        TextEdit25.Properties.ReadOnly = False
        TextEdit26.Properties.ReadOnly = False
        TextEdit27.Properties.ReadOnly = False
        TextEdit28.Properties.ReadOnly = False
        TextEdit29.Properties.ReadOnly = False
        TextEdit30.Properties.ReadOnly = False
        TextEdit31.Properties.ReadOnly = False
        TextEdit32.Properties.ReadOnly = False
        TextEdit33.Properties.ReadOnly = False
        TextEdit34.Properties.ReadOnly = False
        TextEdit35.Properties.ReadOnly = False
        TextEdit36.Properties.ReadOnly = False
        TextEdit37.Properties.ReadOnly = False
        TextEdit38.Properties.ReadOnly = False
        TextEdit39.Properties.ReadOnly = False
        TextEdit40.Properties.ReadOnly = False
        TextEdit41.Properties.ReadOnly = False
        TextEdit42.Properties.ReadOnly = False
        TextEdit43.Properties.ReadOnly = False
        TextEdit44.Properties.ReadOnly = False
        TextEdit45.Properties.ReadOnly = False
        TextEdit46.Properties.ReadOnly = False
        TextEdit47.Properties.ReadOnly = False
        TextEdit48.Properties.ReadOnly = False
        TextEdit49.Properties.ReadOnly = False
        TextEdit50.Properties.ReadOnly = False
        TextEdit51.Properties.ReadOnly = False
        TextEdit52.Properties.ReadOnly = False
        TextEdit53.Properties.ReadOnly = False
        TextEdit54.Properties.ReadOnly = False
        TextEdit55.Properties.ReadOnly = False
        TextEdit56.Properties.ReadOnly = False
        TextEdit57.Properties.ReadOnly = False
        TextEdit58.Properties.ReadOnly = False
        TextEdit59.Properties.ReadOnly = False
        TextEdit60.Properties.ReadOnly = False
        TextEdit61.Properties.ReadOnly = False
        TextEdit62.Properties.ReadOnly = False
        TextEdit63.Properties.ReadOnly = False
        TextEdit64.Properties.ReadOnly = False
        TextEdit65.Properties.ReadOnly = False
        TextEdit66.Properties.ReadOnly = False
        TextEdit67.Properties.ReadOnly = False
        TextEdit68.Properties.ReadOnly = False
        TextEdit69.Properties.ReadOnly = False
        TextEdit70.Properties.ReadOnly = False
        TextEdit71.Properties.ReadOnly = False
        TextEdit72.Properties.ReadOnly = False
        TextEdit73.Properties.ReadOnly = False
        TextEdit74.Properties.ReadOnly = False
        TextEdit75.Properties.ReadOnly = False
        TextEdit76.Properties.ReadOnly = False
        TextEdit77.Properties.ReadOnly = False
        TextEdit78.Properties.ReadOnly = False
        TextEdit79.Properties.ReadOnly = False
        TextEdit80.Properties.ReadOnly = False
        TextEdit81.Properties.ReadOnly = False
        TextEdit82.Properties.ReadOnly = False
        TextEdit83.Properties.ReadOnly = False
        TextEdit84.Properties.ReadOnly = False
        TextEdit85.Properties.ReadOnly = False
        TextEdit86.Properties.ReadOnly = False
        TextEdit87.Properties.ReadOnly = False
        TextEdit88.Properties.ReadOnly = False
        TextEdit89.Properties.ReadOnly = False
        TextEdit90.Properties.ReadOnly = False
        TextEdit91.Properties.ReadOnly = False
        TextEdit92.Properties.ReadOnly = False
        TextEdit93.Properties.ReadOnly = False
        TextEdit94.Properties.ReadOnly = False
        TextEdit95.Properties.ReadOnly = False
        TextEdit96.Properties.ReadOnly = False
        TextEdit97.Properties.ReadOnly = False
        TextEdit98.Properties.ReadOnly = False
        TextEdit99.Properties.ReadOnly = False
        TextEdit100.Properties.ReadOnly = False
        TextEdit101.Properties.ReadOnly = False
        TextEdit102.Properties.ReadOnly = False
        TextEdit103.Properties.ReadOnly = False
        TextEdit104.Properties.ReadOnly = False
        TextEdit105.Properties.ReadOnly = False
        TextEdit106.Properties.ReadOnly = False
        TextEdit107.Properties.ReadOnly = False
        TextEdit108.Properties.ReadOnly = False
        TextEdit109.Properties.ReadOnly = False
        TextEdit110.Properties.ReadOnly = False
        TextEdit111.Properties.ReadOnly = False
        TextEdit112.Properties.ReadOnly = False
        TextEdit113.Properties.ReadOnly = False
        TextEdit114.Properties.ReadOnly = False
        TextEdit115.Properties.ReadOnly = False
        TextEdit116.Properties.ReadOnly = False
        TextEdit117.Properties.ReadOnly = False
        TextEdit118.Properties.ReadOnly = False
        TextEdit119.Properties.ReadOnly = False
        TextEdit120.Properties.ReadOnly = False
        TextEdit121.Properties.ReadOnly = False
        TextEdit122.Properties.ReadOnly = False
        TextEdit123.Properties.ReadOnly = False
        TextEdit124.Properties.ReadOnly = False
        TextEdit125.Properties.ReadOnly = False
        TextEdit126.Properties.ReadOnly = False
        TextEdit127.Properties.ReadOnly = False
        TextEdit128.Properties.ReadOnly = False
        TextEdit129.Properties.ReadOnly = False
        TextEdit130.Properties.ReadOnly = False
        TextEdit131.Properties.ReadOnly = False
        TextEdit132.Properties.ReadOnly = False
        TextEdit133.Properties.ReadOnly = False
        TextEdit134.Properties.ReadOnly = False
        TextEdit135.Properties.ReadOnly = False
        TextEdit136.Properties.ReadOnly = False
        TextEdit137.Properties.ReadOnly = False
        TextEdit138.Properties.ReadOnly = False
        TextEdit139.Properties.ReadOnly = False
        TextEdit140.Properties.ReadOnly = False
        TextEdit141.Properties.ReadOnly = False
        TextEdit142.Properties.ReadOnly = False
        TextEdit143.Properties.ReadOnly = False
        TextEdit144.Properties.ReadOnly = False
        TextEdit145.Properties.ReadOnly = False
        TextEdit146.Properties.ReadOnly = False
        TextEdit147.Properties.ReadOnly = False
        TextEdit148.Properties.ReadOnly = False
        TextEdit149.Properties.ReadOnly = False
        TextEdit150.Properties.ReadOnly = False
        TextEdit151.Properties.ReadOnly = False
        TextEdit152.Properties.ReadOnly = False
        TextEdit153.Properties.ReadOnly = False
        TextEdit154.Properties.ReadOnly = False
        TextEdit155.Properties.ReadOnly = False
        TextEdit156.Properties.ReadOnly = False
        TextEdit157.Properties.ReadOnly = False
        TextEdit158.Properties.ReadOnly = False
        TextEdit159.Properties.ReadOnly = False
        TextEdit160.Properties.ReadOnly = False
        TextEdit161.Properties.ReadOnly = False
        TextEdit162.Properties.ReadOnly = False
        TextEdit163.Properties.ReadOnly = False
        TextEdit164.Properties.ReadOnly = False
        TextEdit165.Properties.ReadOnly = False
        TextEdit166.Properties.ReadOnly = False
        TextEdit167.Properties.ReadOnly = False
        TextEdit168.Properties.ReadOnly = False
        TextEdit169.Properties.ReadOnly = False
        TextEdit170.Properties.ReadOnly = False
        TextEdit171.Properties.ReadOnly = False
        TextEdit172.Properties.ReadOnly = False
        TextEdit173.Properties.ReadOnly = False
        TextEdit174.Properties.ReadOnly = False
        TextEdit175.Properties.ReadOnly = False
        TextEdit176.Properties.ReadOnly = False
        TextEdit177.Properties.ReadOnly = False
        TextEdit178.Properties.ReadOnly = False
        TextEdit179.Properties.ReadOnly = False
        TextEdit180.Properties.ReadOnly = False
        TextEdit181.Properties.ReadOnly = False
        TextEdit182.Properties.ReadOnly = False
        TextEdit183.Properties.ReadOnly = False
        TextEdit184.Properties.ReadOnly = False
        TextEdit185.Properties.ReadOnly = False
        TextEdit186.Properties.ReadOnly = False
        TextEdit187.Properties.ReadOnly = False
        TextEdit188.Properties.ReadOnly = False
        TextEdit189.Properties.ReadOnly = False
        TextEdit190.Properties.ReadOnly = False
        TextEdit191.Properties.ReadOnly = False
        TextEdit192.Properties.ReadOnly = False
        TextEdit193.Properties.ReadOnly = False
        TextEdit194.Properties.ReadOnly = False
        TextEdit195.Properties.ReadOnly = False
        TextEdit196.Properties.ReadOnly = False
        TextEdit197.Properties.ReadOnly = False
        TextEdit198.Properties.ReadOnly = False
        TextEdit199.Properties.ReadOnly = False
        TextEdit200.Properties.ReadOnly = False
        TextEdit201.Properties.ReadOnly = False
        TextEdit202.Properties.ReadOnly = False
        TextEdit203.Properties.ReadOnly = False
        TextEdit204.Properties.ReadOnly = False
        TextEdit205.Properties.ReadOnly = False
        TextEdit206.Properties.ReadOnly = False
        TextEdit207.Properties.ReadOnly = False
        TextEdit208.Properties.ReadOnly = False
        TextEdit209.Properties.ReadOnly = False
        TextEdit210.Properties.ReadOnly = False
        TextEdit211.Properties.ReadOnly = False
        TextEdit212.Properties.ReadOnly = False
        TextEdit213.Properties.ReadOnly = False
        TextEdit214.Properties.ReadOnly = False
        TextEdit215.Properties.ReadOnly = False
        TextEdit216.Properties.ReadOnly = False
        TextEdit217.Properties.ReadOnly = False
        TextEdit218.Properties.ReadOnly = False
        TextEdit219.Properties.ReadOnly = False
        TextEdit220.Properties.ReadOnly = False
        TextEdit221.Properties.ReadOnly = False
        TextEdit222.Properties.ReadOnly = False
        TextEdit223.Properties.ReadOnly = False
        TextEdit224.Properties.ReadOnly = False
        TextEdit225.Properties.ReadOnly = False
        TextEdit226.Properties.ReadOnly = False

        grdDOKTERBEDAH.Properties.ReadOnly = False
        grdASISTENBEDAH.Properties.ReadOnly = False
        grdRuangan.Properties.ReadOnly = False
        grdDOKTERANESTESI.Properties.ReadOnly = False
        grdASISTENANESTESI.Properties.ReadOnly = False
        grdDOKTERANESTESIPJ.Properties.ReadOnly = False
        grdPERAWATANESTESIPJ.Properties.ReadOnly = False

        CheckEdit1.Properties.ReadOnly = False
        CheckEdit2.Properties.ReadOnly = False
        CheckEdit3.Properties.ReadOnly = False
        CheckEdit4.Properties.ReadOnly = False
        'CheckEdit5.Properties.ReadOnly = False
        CheckEdit6.Properties.ReadOnly = False
        CheckEdit7.Properties.ReadOnly = False
        CheckEdit8.Properties.ReadOnly = False
        CheckEdit9.Properties.ReadOnly = False
        CheckEdit10.Properties.ReadOnly = False
        CheckEdit11.Properties.ReadOnly = False
        CheckEdit12.Properties.ReadOnly = False
        CheckEdit13.Properties.ReadOnly = False
        CheckEdit14.Properties.ReadOnly = False
        CheckEdit15.Properties.ReadOnly = False
        CheckEdit16.Properties.ReadOnly = False
        CheckEdit17.Properties.ReadOnly = False
        CheckEdit18.Properties.ReadOnly = False
        CheckEdit19.Properties.ReadOnly = False
        CheckEdit20.Properties.ReadOnly = False
        CheckEdit21.Properties.ReadOnly = False
        CheckEdit22.Properties.ReadOnly = False
        CheckEdit23.Properties.ReadOnly = False
        CheckEdit24.Properties.ReadOnly = False
        CheckEdit25.Properties.ReadOnly = False
        CheckEdit26.Properties.ReadOnly = False
        CheckEdit27.Properties.ReadOnly = False
        CheckEdit28.Properties.ReadOnly = False
        CheckEdit29.Properties.ReadOnly = False
        CheckEdit30.Properties.ReadOnly = False
        CheckEdit31.Properties.ReadOnly = False
        CheckEdit32.Properties.ReadOnly = False
        CheckEdit33.Properties.ReadOnly = False
        CheckEdit34.Properties.ReadOnly = False
        CheckEdit35.Properties.ReadOnly = False
        CheckEdit36.Properties.ReadOnly = False
        CheckEdit37.Properties.ReadOnly = False
        CheckEdit38.Properties.ReadOnly = False
        CheckEdit39.Properties.ReadOnly = False
        CheckEdit40.Properties.ReadOnly = False
        CheckEdit41.Properties.ReadOnly = False
        'CheckEdit42.Properties.ReadOnly = False
        CheckEdit43.Properties.ReadOnly = False
        CheckEdit44.Properties.ReadOnly = False
        CheckEdit45.Properties.ReadOnly = False
        CheckEdit46.Properties.ReadOnly = False
        CheckEdit47.Properties.ReadOnly = False
        CheckEdit48.Properties.ReadOnly = False
        CheckEdit49.Properties.ReadOnly = False
        CheckEdit50.Properties.ReadOnly = False
        CheckEdit51.Properties.ReadOnly = False
        CheckEdit52.Properties.ReadOnly = False
        CheckEdit53.Properties.ReadOnly = False
        CheckEdit54.Properties.ReadOnly = False
        CheckEdit55.Properties.ReadOnly = False
        CheckEdit56.Properties.ReadOnly = False
        CheckEdit57.Properties.ReadOnly = False
        CheckEdit58.Properties.ReadOnly = False
        CheckEdit59.Properties.ReadOnly = False
        CheckEdit60.Properties.ReadOnly = False
        CheckEdit61.Properties.ReadOnly = False
        CheckEdit62.Properties.ReadOnly = False
        CheckEdit63.Properties.ReadOnly = False
        CheckEdit64.Properties.ReadOnly = False
        CheckEdit65.Properties.ReadOnly = False
        CheckEdit66.Properties.ReadOnly = False
        CheckEdit67.Properties.ReadOnly = False
        CheckEdit68.Properties.ReadOnly = False
        CheckEdit69.Properties.ReadOnly = False
        CheckEdit70.Properties.ReadOnly = False
        CheckEdit71.Properties.ReadOnly = False
        CheckEdit72.Properties.ReadOnly = False
        CheckEdit73.Properties.ReadOnly = False
        CheckEdit74.Properties.ReadOnly = False
        CheckEdit75.Properties.ReadOnly = False
        CheckEdit76.Properties.ReadOnly = False
        CheckEdit77.Properties.ReadOnly = False
        CheckEdit78.Properties.ReadOnly = False
        CheckEdit79.Properties.ReadOnly = False
        CheckEdit80.Properties.ReadOnly = False
        CheckEdit81.Properties.ReadOnly = False
        CheckEdit82.Properties.ReadOnly = False
        CheckEdit83.Properties.ReadOnly = False
        CheckEdit84.Properties.ReadOnly = False
        CheckEdit85.Properties.ReadOnly = False
        CheckEdit86.Properties.ReadOnly = False
        CheckEdit87.Properties.ReadOnly = False
        CheckEdit88.Properties.ReadOnly = False

        MemoEdit1.Properties.ReadOnly = False
        MemoEdit2.Properties.ReadOnly = False
        MemoEdit3.Properties.ReadOnly = False
        MemoEdit4.Properties.ReadOnly = False
        MemoEdit5.Properties.ReadOnly = False
        MemoEdit6.Properties.ReadOnly = False

        TextEdit1.Properties.ReadOnly = False
    End Sub
    Private Sub fn_EmptyMe()
        'txtCODE.Text = "<--- AUTO --->"

        'chkDIAGNOSA_1.Checked = False
        TextEdit1.ResetText()
        TextEdit2.ResetText()
        TextEdit3.ResetText()
        'TextEdit4.ResetText()
        TextEdit5.Text = "(+)"
        TextEdit6.ResetText()
        TextEdit7.EditValue = 0
        TextEdit8.EditValue = 0
        TextEdit9.EditValue = 0
        TextEdit10.EditValue = 0
        TextEdit11.ResetText()
        TextEdit12.ResetText()
        TextEdit13.ResetText()
        TextEdit14.ResetText()
        TextEdit15.ResetText()
        TextEdit16.ResetText()
        TextEdit17.ResetText()
        TextEdit18.ResetText()
        TextEdit19.ResetText()
        TextEdit20.ResetText()
        TextEdit21.ResetText()
        TextEdit22.ResetText()
        TextEdit23.ResetText()
        TextEdit24.ResetText()
        TextEdit25.ResetText()
        TextEdit26.ResetText()
        TextEdit27.ResetText()
        TextEdit28.ResetText()
        TextEdit29.ResetText()
        TextEdit30.ResetText()
        TextEdit31.ResetText()
        TextEdit32.ResetText()
        TextEdit33.ResetText()
        TextEdit34.ResetText()
        TextEdit35.ResetText()
        TextEdit36.ResetText()
        TextEdit37.ResetText()
        TextEdit38.ResetText()
        TextEdit39.ResetText()
        TextEdit40.ResetText()
        TextEdit41.ResetText()
        TextEdit42.ResetText()
        TextEdit43.ResetText()
        TextEdit44.ResetText()
        TextEdit45.ResetText()
        TextEdit46.ResetText()
        TextEdit47.ResetText()
        TextEdit48.ResetText()
        TextEdit49.ResetText()
        TextEdit50.ResetText()
        TextEdit51.ResetText()
        TextEdit52.ResetText()
        TextEdit53.ResetText()
        TextEdit54.ResetText()
        TextEdit55.ResetText()
        TextEdit56.ResetText()
        TextEdit57.ResetText()
        TextEdit58.ResetText()
        TextEdit59.ResetText()
        TextEdit60.ResetText()
        TextEdit61.ResetText()
        TextEdit62.ResetText()
        TextEdit63.ResetText()
        TextEdit64.ResetText()
        TextEdit65.ResetText()
        TextEdit66.ResetText()
        TextEdit67.ResetText()
        TextEdit68.ResetText()
        TextEdit69.ResetText()
        TextEdit70.ResetText()
        TextEdit71.ResetText()
        TextEdit72.ResetText()
        TextEdit73.ResetText()
        TextEdit74.ResetText()
        TextEdit75.ResetText()
        TextEdit76.ResetText()
        TextEdit77.ResetText()
        TextEdit78.ResetText()
        TextEdit79.ResetText()
        TextEdit80.ResetText()
        TextEdit81.ResetText()
        TextEdit82.ResetText()
        TextEdit83.ResetText()
        TextEdit84.ResetText()
        TextEdit85.ResetText()
        TextEdit86.ResetText()
        TextEdit87.ResetText()
        TextEdit88.ResetText()
        TextEdit89.ResetText()
        TextEdit90.ResetText()
        TextEdit91.ResetText()
        TextEdit92.ResetText()
        TextEdit93.ResetText()
        TextEdit94.ResetText()
        TextEdit95.ResetText()
        TextEdit96.ResetText()
        TextEdit97.ResetText()
        TextEdit98.ResetText()
        TextEdit99.ResetText()
        TextEdit100.ResetText()
        TextEdit101.ResetText()
        TextEdit102.ResetText()
        TextEdit103.ResetText()
        TextEdit104.ResetText()
        TextEdit105.ResetText()
        TextEdit106.ResetText()
        TextEdit107.ResetText()
        TextEdit108.ResetText()
        TextEdit109.ResetText()
        TextEdit110.ResetText()
        TextEdit111.ResetText()
        TextEdit112.ResetText()
        TextEdit113.ResetText()
        TextEdit114.ResetText()
        TextEdit115.ResetText()
        TextEdit116.ResetText()
        TextEdit117.ResetText()
        TextEdit118.ResetText()
        TextEdit119.ResetText()
        TextEdit120.ResetText()
        TextEdit121.ResetText()
        TextEdit122.ResetText()
        TextEdit123.ResetText()
        TextEdit124.ResetText()
        TextEdit125.ResetText()
        TextEdit126.ResetText()
        TextEdit127.ResetText()
        TextEdit128.ResetText()
        TextEdit129.ResetText()
        TextEdit130.ResetText()
        TextEdit131.ResetText()
        TextEdit132.ResetText()
        TextEdit133.ResetText()
        TextEdit134.ResetText()
        TextEdit135.ResetText()
        TextEdit136.ResetText()
        TextEdit137.ResetText()
        TextEdit138.ResetText()
        TextEdit139.ResetText()
        TextEdit140.ResetText()
        TextEdit141.ResetText()
        TextEdit142.ResetText()
        TextEdit143.ResetText()
        TextEdit144.ResetText()
        TextEdit145.ResetText()
        TextEdit146.ResetText()
        TextEdit147.ResetText()
        TextEdit148.ResetText()
        TextEdit149.ResetText()
        TextEdit150.ResetText()
        TextEdit151.ResetText()
        TextEdit152.ResetText()
        TextEdit153.ResetText()
        TextEdit154.ResetText()
        TextEdit155.ResetText()
        TextEdit156.ResetText()
        TextEdit157.ResetText()
        TextEdit158.ResetText()
        TextEdit159.ResetText()
        TextEdit160.ResetText()
        TextEdit161.ResetText()
        TextEdit162.ResetText()
        TextEdit163.ResetText()
        TextEdit164.ResetText()
        TextEdit165.ResetText()
        TextEdit166.ResetText()
        TextEdit167.ResetText()
        TextEdit168.ResetText()
        TextEdit169.ResetText()
        TextEdit170.ResetText()
        TextEdit171.ResetText()
        TextEdit172.ResetText()
        TextEdit173.ResetText()
        TextEdit174.ResetText()
        TextEdit175.ResetText()
        TextEdit176.ResetText()
        TextEdit177.ResetText()
        TextEdit178.ResetText()
        TextEdit179.ResetText()
        TextEdit180.ResetText()
        TextEdit181.ResetText()
        TextEdit182.ResetText()
        TextEdit183.ResetText()
        TextEdit184.ResetText()
        TextEdit185.ResetText()
        TextEdit186.ResetText()
        TextEdit187.ResetText()
        TextEdit188.ResetText()
        TextEdit189.ResetText()
        TextEdit190.ResetText()
        TextEdit191.ResetText()
        TextEdit192.ResetText()
        TextEdit193.ResetText()
        TextEdit194.ResetText()
        TextEdit195.ResetText()
        TextEdit196.ResetText()
        TextEdit197.ResetText()
        TextEdit198.ResetText()
        TextEdit199.ResetText()
        TextEdit200.ResetText()
        TextEdit201.ResetText()
        TextEdit202.ResetText()
        TextEdit203.ResetText()
        TextEdit204.ResetText()
        TextEdit205.ResetText()
        TextEdit206.ResetText()
        TextEdit207.ResetText()
        TextEdit208.ResetText()
        TextEdit209.ResetText()
        TextEdit210.ResetText()
        TextEdit211.ResetText()
        TextEdit212.ResetText()
        TextEdit213.ResetText()
        TextEdit214.ResetText()
        TextEdit215.ResetText()
        TextEdit216.ResetText()
        TextEdit217.ResetText()
        TextEdit218.ResetText()
        TextEdit219.ResetText()
        TextEdit220.ResetText()
        TextEdit221.ResetText()
        TextEdit222.ResetText()
        TextEdit223.ResetText()
        TextEdit224.ResetText()
        TextEdit225.ResetText()
        TextEdit226.ResetText()

        grdRuangan.Reset()
        grdDOKTERANESTESI.Reset()
        grdDOKTERANESTESIPJ.Reset()
        grdPERAWATANESTESIPJ.Reset()
        grdDOKTERBEDAH.Reset()
        grdASISTENBEDAH.Reset()
        grdASISTENANESTESI.Reset()

        CheckEdit1.Checked = False
        CheckEdit2.Checked = False
        CheckEdit3.Checked = False
        CheckEdit4.Checked = False
        'CheckEdit5.Checked = False
        CheckEdit6.Checked = False
        CheckEdit7.Checked = False
        CheckEdit8.Checked = False
        CheckEdit9.Checked = False
        CheckEdit10.Checked = False
        CheckEdit11.Checked = False
        CheckEdit12.Checked = False
        CheckEdit13.Checked = False
        CheckEdit14.Checked = False
        CheckEdit15.Checked = False
        CheckEdit16.Checked = False
        CheckEdit17.Checked = False
        CheckEdit18.Checked = False
        CheckEdit19.Checked = False
        CheckEdit20.Checked = False
        CheckEdit21.Checked = False
        CheckEdit22.Checked = False
        CheckEdit23.Checked = False
        CheckEdit24.Checked = False
        CheckEdit25.Checked = False
        CheckEdit26.Checked = False
        CheckEdit27.Checked = False
        CheckEdit28.Checked = False
        CheckEdit29.Checked = False
        CheckEdit30.Checked = False
        CheckEdit31.Checked = False
        CheckEdit32.Checked = False
        CheckEdit33.Checked = False
        CheckEdit34.Checked = False
        CheckEdit35.Checked = False
        CheckEdit36.Checked = False
        CheckEdit37.Checked = False
        CheckEdit38.Checked = False
        CheckEdit39.Checked = False
        CheckEdit40.Checked = False
        CheckEdit41.Checked = False
        'CheckEdit42.Checked = False
        CheckEdit43.Checked = False
        CheckEdit44.Checked = False
        CheckEdit45.Checked = False
        CheckEdit46.Checked = False
        CheckEdit47.Checked = False
        CheckEdit48.Checked = False
        CheckEdit49.Checked = False
        CheckEdit50.Checked = False
        CheckEdit51.Checked = False
        CheckEdit52.Checked = False
        CheckEdit53.Checked = False
        CheckEdit54.Checked = False
        CheckEdit55.Checked = False
        CheckEdit56.Checked = False
        CheckEdit57.Checked = False
        CheckEdit58.Checked = False
        CheckEdit59.Checked = False
        CheckEdit60.Checked = False
        CheckEdit61.Checked = False
        CheckEdit62.Checked = False
        CheckEdit63.Checked = False
        CheckEdit64.Checked = False
        CheckEdit65.Checked = False
        CheckEdit66.Checked = False
        CheckEdit67.Checked = False
        CheckEdit68.Checked = False
        CheckEdit69.Checked = False
        CheckEdit70.Checked = False
        CheckEdit71.Checked = False
        CheckEdit72.Checked = False
        CheckEdit73.Checked = False
        CheckEdit74.Checked = False
        CheckEdit75.Checked = False
        CheckEdit76.Checked = False
        CheckEdit77.Checked = False
        CheckEdit78.Checked = False
        CheckEdit79.Checked = False
        CheckEdit80.Checked = False
        CheckEdit81.Checked = False
        CheckEdit82.Checked = False
        CheckEdit83.Checked = False
        CheckEdit84.Checked = False
        CheckEdit85.Checked = False
        CheckEdit86.Checked = False
        CheckEdit87.Checked = False
        CheckEdit88.Checked = False

        MemoEdit1.ResetText()
        MemoEdit2.ResetText()
        MemoEdit3.ResetText()
        MemoEdit4.ResetText()
        MemoEdit5.ResetText()
        MemoEdit6.ResetText()

        TimeEdit1.Reset()
        TimeEdit2.Reset()
        TimeEdit3.Reset()
        TimeEdit4.Reset()
        TimeEdit5.Reset()
        TimeEdit6.Reset()
        TimeEdit7.Reset()
        TimeEdit8.Reset()
        TimeEdit9.Reset()
        TimeEdit10.Reset()
        TimeEdit11.Reset()
        TimeEdit12.Reset()
        TimeEdit13.Reset()
        MemoEdit8.ResetText()

        TimeEdit14.Reset()
        TimeEdit15.Reset()
        TimeEdit16.Reset()
        TimeEdit17.Reset()
        TimeEdit18.Reset()
        TimeEdit19.Reset()
        TimeEdit20.Reset()
        TimeEdit21.Reset()
        TimeEdit22.Reset()
        TimeEdit23.Reset()
        TimeEdit24.Reset()
        TimeEdit25.Reset()

        TextEdit227.ResetText()
        TextEdit228.ResetText()
        TextEdit229.ResetText()
        TextEdit230.ResetText()
        TextEdit231.ResetText()
        TextEdit232.ResetText()
        TextEdit233.ResetText()
        TextEdit234.ResetText()
        TextEdit235.ResetText()
        TextEdit236.ResetText()
        TextEdit237.ResetText()
        TextEdit238.ResetText()

    End Sub
    Private Sub fn_LoadData()
        Try
            '' ***** HEADER *****
            Dim ds = oS_DIGITAL_OK_KARTU_ANESTESI_A.GetData(sNoId)
            With ds
                TextEdit1.Text = .SKARTUANASTESIA1
                TextEdit2.Text = .SKARTUANASTESIA2
                TextEdit3.Text = .SKARTUANASTESIA3
                TextEdit4.Text = .SKARTUANASTESIA4
                TextEdit5.Text = .SKARTUANASTESIA5
                TextEdit6.Text = .SKARTUANASTESIA6
                TextEdit7.Text = .SKARTUANASTESIA7
                TextEdit8.Text = .SKARTUANASTESIA8
                TextEdit9.Text = .SKARTUANASTESIA9
                TextEdit10.Text = .SKARTUANASTESIA10
                TextEdit11.Text = .SKARTUANASTESIA11
                TextEdit12.Text = .SKARTUANASTESIA12
                TextEdit13.Text = .SKARTUANASTESIA13
                TextEdit14.Text = .SKARTUANASTESIA14
                TextEdit15.Text = .SKARTUANASTESIA15
                TextEdit16.Text = .SKARTUANASTESIA16
                TextEdit17.Text = .SKARTUANASTESIA17
                TextEdit18.Text = .SKARTUANASTESIA18
                TextEdit19.Text = .SKARTUANASTESIA19
                TextEdit20.Text = .SKARTUANASTESIA20
                TextEdit21.Text = .SKARTUANASTESIA21
                TextEdit22.Text = .SKARTUANASTESIA22
                TextEdit23.Text = .SKARTUANASTESIA23
                TextEdit24.Text = .SKARTUANASTESIA24
                TextEdit25.Text = .SKARTUANASTESIA25
                TextEdit26.Text = .SKARTUANASTESIA26
                TextEdit27.Text = .SKARTUANASTESIA27
                TextEdit28.Text = .SKARTUANASTESIA28
                TextEdit29.Text = .SKARTUANASTESIA29
                TextEdit30.Text = .SKARTUANASTESIA30
                TextEdit31.Text = .SKARTUANASTESIA31
                TextEdit32.Text = .SKARTUANASTESIA32
                TextEdit33.Text = .SKARTUANASTESIA33
                TextEdit34.Text = .SKARTUANASTESIA34
                TextEdit35.Text = .SKARTUANASTESIA35
                TextEdit36.Text = .SKARTUANASTESIA36
                TextEdit37.Text = .SKARTUANASTESIA37
                TextEdit38.Text = .SKARTUANASTESIA38
                TextEdit39.Text = .SKARTUANASTESIA39
                TextEdit40.Text = .SKARTUANASTESIA40
                TextEdit41.Text = .SKARTUANASTESIA41
                TextEdit42.Text = .SKARTUANASTESIA42
                TextEdit43.Text = .SKARTUANASTESIA43
                TextEdit44.Text = .SKARTUANASTESIA44
                TextEdit45.Text = .SKARTUANASTESIA45
                TextEdit46.Text = .SKARTUANASTESIA46
                TextEdit47.Text = .SKARTUANASTESIA47
                TextEdit48.Text = .SKARTUANASTESIA48
                TextEdit49.Text = .SKARTUANASTESIA49
                TextEdit50.Text = .SKARTUANASTESIA50
                TextEdit51.Text = .SKARTUANASTESIA51
                TextEdit52.Text = .SKARTUANASTESIA52
                TextEdit53.Text = .SKARTUANASTESIA53
                TextEdit54.Text = .SKARTUANASTESIA54
                TextEdit55.Text = .SKARTUANASTESIA55
                TextEdit56.Text = .SKARTUANASTESIA56
                TextEdit57.Text = .SKARTUANASTESIA57
                TextEdit58.Text = .SKARTUANASTESIA58
                TextEdit59.Text = .SKARTUANASTESIA59
                TextEdit60.Text = .SKARTUANASTESIA60
                TextEdit61.Text = .SKARTUANASTESIA61
                TextEdit62.Text = .SKARTUANASTESIA62
                TextEdit63.Text = .SKARTUANASTESIA63
                TextEdit64.Text = .SKARTUANASTESIA64
                TextEdit65.Text = .SKARTUANASTESIA65
                TextEdit66.Text = .SKARTUANASTESIA66
                TextEdit67.Text = .SKARTUANASTESIA67
                TextEdit68.Text = .SKARTUANASTESIA68
                TextEdit69.Text = .SKARTUANASTESIA69
                TextEdit70.Text = .SKARTUANASTESIA70
                TextEdit71.Text = .SKARTUANASTESIA71
                TextEdit72.Text = .SKARTUANASTESIA72
                TextEdit73.Text = .SKARTUANASTESIA73
                TextEdit74.Text = .SKARTUANASTESIA74
                TextEdit75.Text = .SKARTUANASTESIA75
                TextEdit76.Text = .SKARTUANASTESIA76
                TextEdit77.Text = .SKARTUANASTESIA77
                TextEdit78.Text = .SKARTUANASTESIA78
                TextEdit79.Text = .SKARTUANASTESIA79
                TextEdit80.Text = .SKARTUANASTESIA80
                TextEdit81.Text = .SKARTUANASTESIA81
                TextEdit82.Text = .SKARTUANASTESIA82
                TextEdit83.Text = .SKARTUANASTESIA83
                TextEdit84.Text = .SKARTUANASTESIA84
                TextEdit85.Text = .SKARTUANASTESIA85
                TextEdit86.Text = .SKARTUANASTESIA86
                TextEdit87.Text = .SKARTUANASTESIA87
                TextEdit88.Text = .SKARTUANASTESIA88
                TextEdit89.Text = .SKARTUANASTESIA89
                TextEdit90.Text = .SKARTUANASTESIA90
                TextEdit91.Text = .SKARTUANASTESIA91
                TextEdit92.Text = .SKARTUANASTESIA92
                TextEdit93.Text = .SKARTUANASTESIA93
                TextEdit94.Text = .SKARTUANASTESIA94
                TextEdit95.Text = .SKARTUANASTESIA95
                TextEdit96.Text = .SKARTUANASTESIA96
                TextEdit97.Text = .SKARTUANASTESIA97
                TextEdit98.Text = .SKARTUANASTESIA98
                TextEdit99.Text = .SKARTUANASTESIA99
                TextEdit100.Text = .SKARTUANASTESIA100
                TextEdit101.Text = .SKARTUANASTESIA101
                TextEdit102.Text = .SKARTUANASTESIA102
                TextEdit103.Text = .SKARTUANASTESIA103
                TextEdit104.Text = .SKARTUANASTESIA104
                TextEdit105.Text = .SKARTUANASTESIA105
                TextEdit106.Text = .SKARTUANASTESIA106
                TextEdit107.Text = .SKARTUANASTESIA107
                TextEdit108.Text = .SKARTUANASTESIA108
                TextEdit109.Text = .SKARTUANASTESIA109
                TextEdit110.Text = .SKARTUANASTESIA110
                TextEdit111.Text = .SKARTUANASTESIA111
                TextEdit112.Text = .SKARTUANASTESIA112
                TextEdit113.Text = .SKARTUANASTESIA113
                TextEdit114.Text = .SKARTUANASTESIA114
                TextEdit115.Text = .SKARTUANASTESIA115
                TextEdit116.Text = .SKARTUANASTESIA116
                TextEdit117.Text = .SKARTUANASTESIA117
                TextEdit118.Text = .SKARTUANASTESIA118
                TextEdit119.Text = .SKARTUANASTESIA119
                TextEdit120.Text = .SKARTUANASTESIA120
                TextEdit121.Text = .SKARTUANASTESIA121
                TextEdit122.Text = .SKARTUANASTESIA122
                TextEdit123.Text = .SKARTUANASTESIA123
                TextEdit124.Text = .SKARTUANASTESIA124
                TextEdit125.Text = .SKARTUANASTESIA125
                TextEdit126.Text = .SKARTUANASTESIA126
                TextEdit127.Text = .SKARTUANASTESIA127
                TextEdit128.Text = .SKARTUANASTESIA128
                TextEdit129.Text = .SKARTUANASTESIA129
                TextEdit130.Text = .SKARTUANASTESIA130
                TextEdit131.Text = .SKARTUANASTESIA131
                TextEdit132.Text = .SKARTUANASTESIA132
                TextEdit133.Text = .SKARTUANASTESIA133
                TextEdit134.Text = .SKARTUANASTESIA134
                TextEdit135.Text = .SKARTUANASTESIA135
                TextEdit136.Text = .SKARTUANASTESIA136
                TextEdit137.Text = .SKARTUANASTESIA137
                TextEdit138.Text = .SKARTUANASTESIA138
                TextEdit139.Text = .SKARTUANASTESIA139
                TextEdit140.Text = .SKARTUANASTESIA140
                TextEdit141.Text = .SKARTUANASTESIA141
                TextEdit142.Text = .SKARTUANASTESIA142
                TextEdit143.Text = .SKARTUANASTESIA143
                TextEdit144.Text = .SKARTUANASTESIA144
                TextEdit145.Text = .SKARTUANASTESIA145
                TextEdit146.Text = .SKARTUANASTESIA146
                TextEdit147.Text = .SKARTUANASTESIA147
                TextEdit148.Text = .SKARTUANASTESIA148
                TextEdit149.Text = .SKARTUANASTESIA149
                TextEdit150.Text = .SKARTUANASTESIA150
                TextEdit151.Text = .SKARTUANASTESIA151
                TextEdit152.Text = .SKARTUANASTESIA152
                TextEdit153.Text = .SKARTUANASTESIA153
                TextEdit154.Text = .SKARTUANASTESIA154
                TextEdit155.Text = .SKARTUANASTESIA155
                TextEdit156.Text = .SKARTUANASTESIA156
                TextEdit157.Text = .SKARTUANASTESIA157
                TextEdit158.Text = .SKARTUANASTESIA158
                TextEdit159.Text = .SKARTUANASTESIA159
                TextEdit160.Text = .SKARTUANASTESIA160
                TextEdit161.Text = .SKARTUANASTESIA161
                TextEdit162.Text = .SKARTUANASTESIA162
                TextEdit163.Text = .SKARTUANASTESIA163
                TextEdit164.Text = .SKARTUANASTESIA164
                TextEdit165.Text = .SKARTUANASTESIA165
                TextEdit166.Text = .SKARTUANASTESIA166
                TextEdit167.Text = .SKARTUANASTESIA167
                TextEdit168.Text = .SKARTUANASTESIA168
                TextEdit169.Text = .SKARTUANASTESIA169
                TextEdit170.Text = .SKARTUANASTESIA170
                TextEdit171.Text = .SKARTUANASTESIA171
                TextEdit172.Text = .SKARTUANASTESIA172
                TextEdit173.Text = .SKARTUANASTESIA173
                TextEdit174.Text = .SKARTUANASTESIA174
                TextEdit175.Text = .SKARTUANASTESIA175
                TextEdit176.Text = .SKARTUANASTESIA176
                TextEdit177.Text = .SKARTUANASTESIA177
                TextEdit178.Text = .SKARTUANASTESIA178
                TextEdit179.Text = .SKARTUANASTESIA179
                TextEdit180.Text = .SKARTUANASTESIA180
                TextEdit181.Text = .SKARTUANASTESIA181
                TextEdit182.Text = .SKARTUANASTESIA182
                TextEdit183.Text = .SKARTUANASTESIA183
                TextEdit184.Text = .SKARTUANASTESIA184
                TextEdit185.Text = .SKARTUANASTESIA185
                TextEdit186.Text = .SKARTUANASTESIA186
                TextEdit187.Text = .SKARTUANASTESIA187
                TextEdit188.Text = .SKARTUANASTESIA188
                TextEdit189.Text = .SKARTUANASTESIA189
                TextEdit190.Text = .SKARTUANASTESIA190
                TextEdit191.Text = .SKARTUANASTESIA191
                TextEdit192.Text = .SKARTUANASTESIA192
                TextEdit193.Text = .SKARTUANASTESIA193
                TextEdit194.Text = .SKARTUANASTESIA194
                TextEdit195.Text = .SKARTUANASTESIA195
                TextEdit196.Text = .SKARTUANASTESIA196
                TextEdit197.Text = .SKARTUANASTESIA197
                TextEdit198.Text = .SKARTUANASTESIA198
                TextEdit199.Text = .SKARTUANASTESIA199
                TextEdit200.Text = .SKARTUANASTESIA200
                TextEdit201.Text = .SKARTUANASTESIA201
                TextEdit202.Text = .SKARTUANASTESIA202
                TextEdit203.Text = .SKARTUANASTESIA203
                TextEdit204.Text = .SKARTUANASTESIA204
                TextEdit205.Text = .SKARTUANASTESIA205
                TextEdit206.Text = .SKARTUANASTESIA206
                TextEdit207.Text = .SKARTUANASTESIA207
                TextEdit208.Text = .SKARTUANASTESIA208
                TextEdit209.Text = .SKARTUANASTESIA209
                TextEdit210.Text = .SKARTUANASTESIA210
                TextEdit211.Text = .SKARTUANASTESIA211
                TextEdit212.Text = .SKARTUANASTESIA212
                TextEdit213.Text = .SKARTUANASTESIA213
                TextEdit214.Text = .SKARTUANASTESIA214
                TextEdit215.Text = .SKARTUANASTESIA215
                TextEdit216.Text = .SKARTUANASTESIA216
                TextEdit217.Text = .SKARTUANASTESIA217
                TextEdit218.Text = .SKARTUANASTESIA218
                TextEdit219.Text = .SKARTUANASTESIA219
                TextEdit220.Text = .SKARTUANASTESIA220
                TextEdit221.Text = .SKARTUANASTESIA221
                TextEdit222.Text = .SKARTUANASTESIA222
                TextEdit223.Text = .SKARTUANASTESIA223
                TextEdit224.Text = .SKARTUANASTESIA224
                TextEdit225.Text = .SKARTUANASTESIA225
                TextEdit226.Text = .SKARTUANASTESIA226

                grdDOKTERBEDAH.EditValue = .SKARTUANASTESIA227
                grdASISTENBEDAH.EditValue = .SKARTUANASTESIA228
                grdRuangan.EditValue = .SKARTUANASTESIA229
                grdDOKTERANESTESI.EditValue = .SKARTUANASTESIA230
                grdASISTENANESTESI.EditValue = .SKARTUANASTESIA231
                grdDOKTERANESTESIPJ.EditValue = .SKARTUANASTESIA232
                grdPERAWATANESTESIPJ.EditValue = .SKARTUANASTESIA233

                CheckEdit1.Checked = .SKARTUANASTESIA234
                CheckEdit2.Checked = .SKARTUANASTESIA235
                CheckEdit3.Checked = .SKARTUANASTESIA236
                CheckEdit4.Checked = .SKARTUANASTESIA237
                'CheckEdit5.Checked = .SKARTUANASTESIA238
                CheckEdit6.Checked = .SKARTUANASTESIA239
                CheckEdit7.Checked = .SKARTUANASTESIA240
                CheckEdit8.Checked = .SKARTUANASTESIA241
                CheckEdit9.Checked = .SKARTUANASTESIA242
                CheckEdit10.Checked = .SKARTUANASTESIA243
                CheckEdit11.Checked = .SKARTUANASTESIA244
                CheckEdit12.Checked = .SKARTUANASTESIA245
                CheckEdit13.Checked = .SKARTUANASTESIA246
                CheckEdit14.Checked = .SKARTUANASTESIA247
                CheckEdit15.Checked = .SKARTUANASTESIA248
                CheckEdit16.Checked = .SKARTUANASTESIA249
                CheckEdit17.Checked = .SKARTUANASTESIA250
                CheckEdit18.Checked = .SKARTUANASTESIA251
                CheckEdit19.Checked = .SKARTUANASTESIA252
                CheckEdit20.Checked = .SKARTUANASTESIA253
                CheckEdit21.Checked = .SKARTUANASTESIA254
                CheckEdit22.Checked = .SKARTUANASTESIA255
                CheckEdit23.Checked = .SKARTUANASTESIA256
                CheckEdit24.Checked = .SKARTUANASTESIA257
                CheckEdit25.Checked = .SKARTUANASTESIA258
                CheckEdit26.Checked = .SKARTUANASTESIA259
                CheckEdit27.Checked = .SKARTUANASTESIA260
                CheckEdit28.Checked = .SKARTUANASTESIA261
                CheckEdit29.Checked = .SKARTUANASTESIA262
                CheckEdit30.Checked = .SKARTUANASTESIA263
                CheckEdit31.Checked = .SKARTUANASTESIA264
                CheckEdit32.Checked = .SKARTUANASTESIA265
                CheckEdit33.Checked = .SKARTUANASTESIA266
                CheckEdit34.Checked = .SKARTUANASTESIA267
                CheckEdit35.Checked = .SKARTUANASTESIA268
                CheckEdit36.Checked = .SKARTUANASTESIA269
                CheckEdit37.Checked = .SKARTUANASTESIA270
                CheckEdit38.Checked = .SKARTUANASTESIA271
                CheckEdit39.Checked = .SKARTUANASTESIA272
                CheckEdit40.Checked = .SKARTUANASTESIA273
                CheckEdit41.Checked = .SKARTUANASTESIA274
                'CheckEdit42.Checked = .SKARTUANASTESIA275
                CheckEdit43.Checked = .SKARTUANASTESIA276
                CheckEdit44.Checked = .SKARTUANASTESIA277
                CheckEdit45.Checked = .SKARTUANASTESIA278
                CheckEdit46.Checked = .SKARTUANASTESIA279
                CheckEdit47.Checked = .SKARTUANASTESIA280
                CheckEdit48.Checked = .SKARTUANASTESIA281
                CheckEdit49.Checked = .SKARTUANASTESIA282
                CheckEdit50.Checked = .SKARTUANASTESIA283
                CheckEdit51.Checked = .SKARTUANASTESIA284
                CheckEdit52.Checked = .SKARTUANASTESIA285
                CheckEdit53.Checked = .SKARTUANASTESIA286
                CheckEdit54.Checked = .SKARTUANASTESIA287
                CheckEdit55.Checked = .SKARTUANASTESIA288
                CheckEdit56.Checked = .SKARTUANASTESIA289
                CheckEdit57.Checked = .SKARTUANASTESIA290
                CheckEdit58.Checked = .SKARTUANASTESIA291
                CheckEdit59.Checked = .SKARTUANASTESIA292
                CheckEdit60.Checked = .SKARTUANASTESIA293
                CheckEdit61.Checked = .SKARTUANASTESIA294
                CheckEdit62.Checked = .SKARTUANASTESIA295
                CheckEdit63.Checked = .SKARTUANASTESIA296
                CheckEdit64.Checked = .SKARTUANASTESIA297
                CheckEdit65.Checked = .SKARTUANASTESIA298
                CheckEdit66.Checked = .SKARTUANASTESIA299
                CheckEdit67.Checked = .SKARTUANASTESIA300
                CheckEdit68.Checked = .SKARTUANASTESIA301
                CheckEdit69.Checked = .SKARTUANASTESIA302
                CheckEdit70.Checked = .SKARTUANASTESIA303
                CheckEdit71.Checked = .SKARTUANASTESIA304
                CheckEdit72.Checked = .SKARTUANASTESIA305
                CheckEdit73.Checked = .SKARTUANASTESIA306
                CheckEdit74.Checked = .SKARTUANASTESIA307
                CheckEdit75.Checked = .SKARTUANASTESIA308
                CheckEdit76.Checked = .SKARTUANASTESIA309
                CheckEdit77.Checked = .SKARTUANASTESIA310
                CheckEdit78.Checked = .SKARTUANASTESIA311
                CheckEdit79.Checked = .SKARTUANASTESIA312
                CheckEdit80.Checked = .SKARTUANASTESIA313
                CheckEdit81.Checked = .SKARTUANASTESIA314
                CheckEdit82.Checked = .SKARTUANASTESIA315
                CheckEdit83.Checked = .SKARTUANASTESIA316
                CheckEdit84.Checked = .SKARTUANASTESIA317
                CheckEdit85.Checked = .SKARTUANASTESIA318
                CheckEdit86.Checked = .SKARTUANASTESIA319
                CheckEdit87.Checked = .SKARTUANASTESIA320
                CheckEdit88.Checked = .SKARTUANASTESIA321

                MemoEdit1.Text = .SKARTUANASTESIA322
                MemoEdit2.Text = .SKARTUANASTESIA323
                MemoEdit3.Text = .SKARTUANASTESIA324
                MemoEdit4.Text = .SKARTUANASTESIA325
                MemoEdit5.Text = .SKARTUANASTESIA326
                MemoEdit6.Text = .SKARTUANASTESIA327

                TimeEdit1.Text = .SKARTUANASTESIA328
                TimeEdit2.Text = .SKARTUANASTESIA329
                TimeEdit3.Text = .SKARTUANASTESIA330
                TimeEdit4.Text = .SKARTUANASTESIA331
                TimeEdit5.Text = .SKARTUANASTESIA332
                TimeEdit6.Text = .SKARTUANASTESIA333
                TimeEdit7.Text = .SKARTUANASTESIA334
                TimeEdit8.Text = .SKARTUANASTESIA335
                TimeEdit9.Text = .SKARTUANASTESIA336
                TimeEdit10.Text = .SKARTUANASTESIA337
                TimeEdit11.Text = .SKARTUANASTESIA338
                TimeEdit12.Text = .SKARTUANASTESIA339
                TimeEdit13.Text = .SKARTUANASTESIA340
                MemoEdit8.Text = .SKARTUANASTESIA341

                TimeEdit14.Text = .SKARTUANASTESIA342
                TimeEdit15.Text = .SKARTUANASTESIA343
                TimeEdit16.Text = .SKARTUANASTESIA344
                TimeEdit17.Text = .SKARTUANASTESIA345
                TimeEdit18.Text = .SKARTUANASTESIA346
                TimeEdit19.Text = .SKARTUANASTESIA347
                TimeEdit20.Text = .SKARTUANASTESIA348
                TimeEdit21.Text = .SKARTUANASTESIA349
                TimeEdit22.Text = .SKARTUANASTESIA350
                TimeEdit23.Text = .SKARTUANASTESIA351
                TimeEdit24.Text = .SKARTUANASTESIA352
                TimeEdit25.Text = .SKARTUANASTESIA353

                TextEdit227.Text = .SKARTUANASTESIA354
                TextEdit228.Text = .SKARTUANASTESIA355
                TextEdit229.Text = .SKARTUANASTESIA356
                TextEdit230.Text = .SKARTUANASTESIA357
                TextEdit231.Text = .SKARTUANASTESIA358
                TextEdit232.Text = .SKARTUANASTESIA359
                TextEdit233.Text = .SKARTUANASTESIA360
                TextEdit234.Text = .SKARTUANASTESIA361
                TextEdit235.Text = .SKARTUANASTESIA362
                TextEdit236.Text = .SKARTUANASTESIA363
                TextEdit237.Text = .SKARTUANASTESIA364
                TextEdit238.Text = .SKARTUANASTESIA365

                Try
                    Dim img = oS_DIGITAL_OK_KARTU_ANESTESI_A.GetData(sNoId).imgMonitoringAnestesi

                    picMonitoring1.Image = ByteArrayToImage(img.ToArray())
                Catch oErr As Exception
                    MsgBox("Load List Data Gambar tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
                End Try

                Try
                    Dim img = oS_DIGITAL_OK_KARTU_ANESTESI_A.GetData(sNoId).imgMonitoringSkalaNyeri

                    picMonitoring2.Image = ByteArrayToImage(img.ToArray())
                Catch oErr As Exception
                    MsgBox("Load List Data Gambar tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End With
        Catch oErr As Exception
            MsgBox("Load List Data Kartu Anastesi A: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
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
                MsgBox("Dibutuhkan Nomor Kunjungan", MsgBoxStyle.Exclamation, Me.Text)
                txtNoRegister.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdRuangan.EditValue Is Nothing Then
                MsgBox("Dibutuhkan Bagian/Ruangan", MsgBoxStyle.Exclamation, Me.Text)
                grdRuangan.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdDOKTERBEDAH.EditValue Is Nothing Then
                MsgBox("Dibutuhkan Dokter Bedah", MsgBoxStyle.Exclamation, Me.Text)
                grdDOKTERBEDAH.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdASISTENBEDAH.EditValue Is Nothing Then
                MsgBox("Dibutuhkan Asisten Bedah", MsgBoxStyle.Exclamation, Me.Text)
                grdASISTENBEDAH.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdDOKTERANESTESI.EditValue Is Nothing Then
                MsgBox("Dibutuhkan Dokter Anestesi", MsgBoxStyle.Exclamation, Me.Text)
                grdDOKTERANESTESI.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdASISTENANESTESI.EditValue Is Nothing Then
                MsgBox("Dibutuhkan Asisten Anestesi", MsgBoxStyle.Exclamation, Me.Text)
                grdASISTENANESTESI.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdDOKTERANESTESIPJ.EditValue Is Nothing Then
                MsgBox("Dibutuhkan Dokter Anestesi Sebagai Penanggung Jawab", MsgBoxStyle.Exclamation, Me.Text)
                grdDOKTERANESTESIPJ.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdPERAWATANESTESIPJ.EditValue Is Nothing Then
                MsgBox("Dibutuhkan Perawat Anestesi Sebagai Penanggung Jawab", MsgBoxStyle.Exclamation, Me.Text)
                grdPERAWATANESTESIPJ.Focus()
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
            Dim ds = oS_DIGITAL_OK_KARTU_ANESTESI_A.GetStructureHeader
            With ds
                .KODE = sNoId
                .KDPENDAFTARAN = txtNoRegister.Text
                .KDCUSTOMER = txtNoPasien.Text
                Try
                    .DATECREATED = oS_DIGITAL_OK_KARTU_ANESTESI_A.GetData(txtNoRegister.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = sDATEMASUK
                .DOKTER_KODE = sKODEDOKTER
                .DOKTER_NAMEDISPLAY = sNAMADOKTER

                'Dim oKunjungan As New Admission.clsPendaftaran_Kunjungan
                'Dim dsPendaftaran = oKunjungan.GetDataIdentitasByKunjungan(txtNoRegister.Text)

                .SKARTUANASTESIA1 = TextEdit1.Text
                .SKARTUANASTESIA2 = TextEdit2.Text
                .SKARTUANASTESIA3 = TextEdit3.Text
                .SKARTUANASTESIA4 = TextEdit4.Text
                .SKARTUANASTESIA5 = TextEdit5.Text
                .SKARTUANASTESIA6 = TextEdit6.Text
                .SKARTUANASTESIA7 = TextEdit7.Text
                .SKARTUANASTESIA8 = TextEdit8.Text
                .SKARTUANASTESIA9 = TextEdit9.Text
                .SKARTUANASTESIA10 = TextEdit10.Text
                .SKARTUANASTESIA11 = TextEdit11.Text
                .SKARTUANASTESIA12 = TextEdit12.Text
                .SKARTUANASTESIA13 = TextEdit13.Text
                .SKARTUANASTESIA14 = TextEdit14.Text
                .SKARTUANASTESIA15 = TextEdit15.Text
                .SKARTUANASTESIA16 = TextEdit16.Text
                .SKARTUANASTESIA17 = TextEdit17.Text
                .SKARTUANASTESIA18 = TextEdit18.Text
                .SKARTUANASTESIA19 = TextEdit19.Text
                .SKARTUANASTESIA20 = TextEdit20.Text
                .SKARTUANASTESIA21 = TextEdit21.Text
                .SKARTUANASTESIA22 = TextEdit22.Text
                .SKARTUANASTESIA23 = TextEdit23.Text
                .SKARTUANASTESIA24 = TextEdit24.Text
                .SKARTUANASTESIA25 = TextEdit25.Text
                .SKARTUANASTESIA26 = TextEdit26.Text
                .SKARTUANASTESIA27 = TextEdit27.Text
                .SKARTUANASTESIA28 = TextEdit28.Text
                .SKARTUANASTESIA29 = TextEdit29.Text
                .SKARTUANASTESIA30 = TextEdit30.Text
                .SKARTUANASTESIA31 = TextEdit31.Text
                .SKARTUANASTESIA32 = TextEdit32.Text
                .SKARTUANASTESIA33 = TextEdit33.Text
                .SKARTUANASTESIA34 = TextEdit34.Text
                .SKARTUANASTESIA35 = TextEdit35.Text
                .SKARTUANASTESIA36 = TextEdit36.Text
                .SKARTUANASTESIA37 = TextEdit37.Text
                .SKARTUANASTESIA38 = TextEdit38.Text
                .SKARTUANASTESIA39 = TextEdit39.Text
                .SKARTUANASTESIA40 = TextEdit40.Text
                .SKARTUANASTESIA41 = TextEdit41.Text
                .SKARTUANASTESIA42 = TextEdit42.Text
                .SKARTUANASTESIA43 = TextEdit43.Text
                .SKARTUANASTESIA44 = TextEdit44.Text
                .SKARTUANASTESIA45 = TextEdit45.Text
                .SKARTUANASTESIA46 = TextEdit46.Text
                .SKARTUANASTESIA47 = TextEdit47.Text
                .SKARTUANASTESIA48 = TextEdit48.Text
                .SKARTUANASTESIA49 = TextEdit49.Text
                .SKARTUANASTESIA50 = TextEdit50.Text
                .SKARTUANASTESIA51 = TextEdit51.Text
                .SKARTUANASTESIA52 = TextEdit52.Text
                .SKARTUANASTESIA53 = TextEdit53.Text
                .SKARTUANASTESIA54 = TextEdit54.Text
                .SKARTUANASTESIA55 = TextEdit55.Text
                .SKARTUANASTESIA56 = TextEdit56.Text
                .SKARTUANASTESIA57 = TextEdit57.Text
                .SKARTUANASTESIA58 = TextEdit58.Text
                .SKARTUANASTESIA59 = TextEdit59.Text
                .SKARTUANASTESIA60 = TextEdit60.Text
                .SKARTUANASTESIA61 = TextEdit61.Text
                .SKARTUANASTESIA62 = TextEdit62.Text
                .SKARTUANASTESIA63 = TextEdit63.Text
                .SKARTUANASTESIA64 = TextEdit64.Text
                .SKARTUANASTESIA65 = TextEdit65.Text
                .SKARTUANASTESIA66 = TextEdit66.Text
                .SKARTUANASTESIA67 = TextEdit67.Text
                .SKARTUANASTESIA68 = TextEdit68.Text
                .SKARTUANASTESIA69 = TextEdit69.Text
                .SKARTUANASTESIA70 = TextEdit70.Text
                .SKARTUANASTESIA71 = TextEdit71.Text
                .SKARTUANASTESIA72 = TextEdit72.Text
                .SKARTUANASTESIA73 = TextEdit73.Text
                .SKARTUANASTESIA74 = TextEdit74.Text
                .SKARTUANASTESIA75 = TextEdit75.Text
                .SKARTUANASTESIA76 = TextEdit76.Text
                .SKARTUANASTESIA77 = TextEdit77.Text
                .SKARTUANASTESIA78 = TextEdit78.Text
                .SKARTUANASTESIA79 = TextEdit79.Text
                .SKARTUANASTESIA80 = TextEdit80.Text
                .SKARTUANASTESIA81 = TextEdit81.Text
                .SKARTUANASTESIA82 = TextEdit82.Text
                .SKARTUANASTESIA83 = TextEdit83.Text
                .SKARTUANASTESIA84 = TextEdit84.Text
                .SKARTUANASTESIA85 = TextEdit85.Text
                .SKARTUANASTESIA86 = TextEdit86.Text
                .SKARTUANASTESIA87 = TextEdit87.Text
                .SKARTUANASTESIA88 = TextEdit88.Text
                .SKARTUANASTESIA89 = TextEdit89.Text
                .SKARTUANASTESIA90 = TextEdit90.Text
                .SKARTUANASTESIA91 = TextEdit91.Text
                .SKARTUANASTESIA92 = TextEdit92.Text
                .SKARTUANASTESIA93 = TextEdit93.Text
                .SKARTUANASTESIA94 = TextEdit94.Text
                .SKARTUANASTESIA95 = TextEdit95.Text
                .SKARTUANASTESIA96 = TextEdit96.Text
                .SKARTUANASTESIA97 = TextEdit97.Text
                .SKARTUANASTESIA98 = TextEdit98.Text
                .SKARTUANASTESIA99 = TextEdit99.Text
                .SKARTUANASTESIA100 = TextEdit100.Text
                .SKARTUANASTESIA101 = TextEdit101.Text
                .SKARTUANASTESIA102 = TextEdit102.Text
                .SKARTUANASTESIA103 = TextEdit103.Text
                .SKARTUANASTESIA104 = TextEdit104.Text
                .SKARTUANASTESIA105 = TextEdit105.Text
                .SKARTUANASTESIA106 = TextEdit106.Text
                .SKARTUANASTESIA107 = TextEdit107.Text
                .SKARTUANASTESIA108 = TextEdit108.Text
                .SKARTUANASTESIA109 = TextEdit109.Text
                .SKARTUANASTESIA110 = TextEdit110.Text
                .SKARTUANASTESIA111 = TextEdit111.Text
                .SKARTUANASTESIA112 = TextEdit112.Text
                .SKARTUANASTESIA113 = TextEdit113.Text
                .SKARTUANASTESIA114 = TextEdit114.Text
                .SKARTUANASTESIA115 = TextEdit115.Text
                .SKARTUANASTESIA116 = TextEdit116.Text
                .SKARTUANASTESIA117 = TextEdit117.Text
                .SKARTUANASTESIA118 = TextEdit118.Text
                .SKARTUANASTESIA119 = TextEdit119.Text
                .SKARTUANASTESIA120 = TextEdit120.Text
                .SKARTUANASTESIA121 = TextEdit121.Text
                .SKARTUANASTESIA122 = TextEdit122.Text
                .SKARTUANASTESIA123 = TextEdit123.Text
                .SKARTUANASTESIA124 = TextEdit124.Text
                .SKARTUANASTESIA125 = TextEdit125.Text
                .SKARTUANASTESIA126 = TextEdit126.Text
                .SKARTUANASTESIA127 = TextEdit127.Text
                .SKARTUANASTESIA128 = TextEdit128.Text
                .SKARTUANASTESIA129 = TextEdit129.Text
                .SKARTUANASTESIA130 = TextEdit130.Text
                .SKARTUANASTESIA131 = TextEdit131.Text
                .SKARTUANASTESIA132 = TextEdit132.Text
                .SKARTUANASTESIA133 = TextEdit133.Text
                .SKARTUANASTESIA134 = TextEdit134.Text
                .SKARTUANASTESIA135 = TextEdit135.Text
                .SKARTUANASTESIA136 = TextEdit136.Text
                .SKARTUANASTESIA137 = TextEdit137.Text
                .SKARTUANASTESIA138 = TextEdit138.Text
                .SKARTUANASTESIA139 = TextEdit139.Text
                .SKARTUANASTESIA140 = TextEdit140.Text
                .SKARTUANASTESIA141 = TextEdit141.Text
                .SKARTUANASTESIA142 = TextEdit142.Text
                .SKARTUANASTESIA143 = TextEdit143.Text
                .SKARTUANASTESIA144 = TextEdit144.Text
                .SKARTUANASTESIA145 = TextEdit145.Text
                .SKARTUANASTESIA146 = TextEdit146.Text
                .SKARTUANASTESIA147 = TextEdit147.Text
                .SKARTUANASTESIA148 = TextEdit148.Text
                .SKARTUANASTESIA149 = TextEdit149.Text
                .SKARTUANASTESIA150 = TextEdit150.Text
                .SKARTUANASTESIA151 = TextEdit151.Text
                .SKARTUANASTESIA152 = TextEdit152.Text
                .SKARTUANASTESIA153 = TextEdit153.Text
                .SKARTUANASTESIA154 = TextEdit154.Text
                .SKARTUANASTESIA155 = TextEdit155.Text
                .SKARTUANASTESIA156 = TextEdit156.Text
                .SKARTUANASTESIA157 = TextEdit157.Text
                .SKARTUANASTESIA158 = TextEdit158.Text
                .SKARTUANASTESIA159 = TextEdit159.Text
                .SKARTUANASTESIA160 = TextEdit160.Text
                .SKARTUANASTESIA161 = TextEdit161.Text
                .SKARTUANASTESIA162 = TextEdit162.Text
                .SKARTUANASTESIA163 = TextEdit163.Text
                .SKARTUANASTESIA164 = TextEdit164.Text
                .SKARTUANASTESIA165 = TextEdit165.Text
                .SKARTUANASTESIA166 = TextEdit166.Text
                .SKARTUANASTESIA167 = TextEdit167.Text
                .SKARTUANASTESIA168 = TextEdit168.Text
                .SKARTUANASTESIA169 = TextEdit169.Text
                .SKARTUANASTESIA170 = TextEdit170.Text
                .SKARTUANASTESIA171 = TextEdit171.Text
                .SKARTUANASTESIA172 = TextEdit172.Text
                .SKARTUANASTESIA173 = TextEdit173.Text
                .SKARTUANASTESIA174 = TextEdit174.Text
                .SKARTUANASTESIA175 = TextEdit175.Text
                .SKARTUANASTESIA176 = TextEdit176.Text
                .SKARTUANASTESIA177 = TextEdit177.Text
                .SKARTUANASTESIA178 = TextEdit178.Text
                .SKARTUANASTESIA179 = TextEdit179.Text
                .SKARTUANASTESIA180 = TextEdit180.Text
                .SKARTUANASTESIA181 = TextEdit181.Text
                .SKARTUANASTESIA182 = TextEdit182.Text
                .SKARTUANASTESIA183 = TextEdit183.Text
                .SKARTUANASTESIA184 = TextEdit184.Text
                .SKARTUANASTESIA185 = TextEdit185.Text
                .SKARTUANASTESIA186 = TextEdit186.Text
                .SKARTUANASTESIA187 = TextEdit187.Text
                .SKARTUANASTESIA188 = TextEdit188.Text
                .SKARTUANASTESIA189 = TextEdit189.Text
                .SKARTUANASTESIA190 = TextEdit190.Text
                .SKARTUANASTESIA191 = TextEdit191.Text
                .SKARTUANASTESIA192 = TextEdit192.Text
                .SKARTUANASTESIA193 = TextEdit193.Text
                .SKARTUANASTESIA194 = TextEdit194.Text
                .SKARTUANASTESIA195 = TextEdit195.Text
                .SKARTUANASTESIA196 = TextEdit196.Text
                .SKARTUANASTESIA197 = TextEdit197.Text
                .SKARTUANASTESIA198 = TextEdit198.Text
                .SKARTUANASTESIA199 = TextEdit199.Text
                .SKARTUANASTESIA200 = TextEdit200.Text
                .SKARTUANASTESIA201 = TextEdit201.Text
                .SKARTUANASTESIA202 = TextEdit202.Text
                .SKARTUANASTESIA203 = TextEdit203.Text
                .SKARTUANASTESIA204 = TextEdit204.Text
                .SKARTUANASTESIA205 = TextEdit205.Text
                .SKARTUANASTESIA206 = TextEdit206.Text
                .SKARTUANASTESIA207 = TextEdit207.Text
                .SKARTUANASTESIA208 = TextEdit208.Text
                .SKARTUANASTESIA209 = TextEdit209.Text
                .SKARTUANASTESIA210 = TextEdit210.Text
                .SKARTUANASTESIA211 = TextEdit211.Text
                .SKARTUANASTESIA212 = TextEdit212.Text
                .SKARTUANASTESIA213 = TextEdit213.Text
                .SKARTUANASTESIA214 = TextEdit214.Text
                .SKARTUANASTESIA215 = TextEdit215.Text
                .SKARTUANASTESIA216 = TextEdit216.Text
                .SKARTUANASTESIA217 = TextEdit217.Text
                .SKARTUANASTESIA218 = TextEdit218.Text
                .SKARTUANASTESIA219 = TextEdit219.Text
                .SKARTUANASTESIA220 = TextEdit220.Text
                .SKARTUANASTESIA221 = TextEdit221.Text
                .SKARTUANASTESIA222 = TextEdit222.Text
                .SKARTUANASTESIA223 = TextEdit223.Text
                .SKARTUANASTESIA224 = TextEdit224.Text
                .SKARTUANASTESIA225 = TextEdit225.Text
                .SKARTUANASTESIA226 = TextEdit226.Text

                .SKARTUANASTESIA227 = grdDOKTERBEDAH.EditValue
                .SKARTUANASTESIA228 = grdASISTENBEDAH.EditValue
                .SKARTUANASTESIA229 = grdRuangan.EditValue
                .SKARTUANASTESIA230 = grdDOKTERANESTESI.EditValue
                .SKARTUANASTESIA231 = grdASISTENANESTESI.EditValue
                .SKARTUANASTESIA232 = grdDOKTERANESTESIPJ.EditValue
                .SKARTUANASTESIA233 = grdPERAWATANESTESIPJ.EditValue

                .SKARTUANASTESIA227_NAME = grdDOKTERBEDAH.Text
                .SKARTUANASTESIA228_NAME = grdASISTENBEDAH.Text
                .SKARTUANASTESIA229_NAME = grdRuangan.Text
                .SKARTUANASTESIA230_NAME = grdDOKTERANESTESI.Text
                .SKARTUANASTESIA231_NAME = grdASISTENANESTESI.Text
                .SKARTUANASTESIA232_NAME = grdDOKTERANESTESIPJ.Text
                .SKARTUANASTESIA233_NAME = grdPERAWATANESTESIPJ.Text

                .SKARTUANASTESIA234 = CheckEdit1.Checked
                .SKARTUANASTESIA235 = CheckEdit2.Checked
                .SKARTUANASTESIA236 = CheckEdit3.Checked
                .SKARTUANASTESIA237 = CheckEdit4.Checked
                '.SKARTUANASTESIA238 = CheckEdit5.Checked
                .SKARTUANASTESIA239 = CheckEdit6.Checked
                .SKARTUANASTESIA240 = CheckEdit7.Checked
                .SKARTUANASTESIA241 = CheckEdit8.Checked
                .SKARTUANASTESIA242 = CheckEdit9.Checked
                .SKARTUANASTESIA243 = CheckEdit10.Checked
                .SKARTUANASTESIA244 = CheckEdit11.Checked
                .SKARTUANASTESIA245 = CheckEdit12.Checked
                .SKARTUANASTESIA246 = CheckEdit13.Checked
                .SKARTUANASTESIA247 = CheckEdit14.Checked
                .SKARTUANASTESIA248 = CheckEdit15.Checked
                .SKARTUANASTESIA249 = CheckEdit16.Checked
                .SKARTUANASTESIA250 = CheckEdit17.Checked
                .SKARTUANASTESIA251 = CheckEdit18.Checked
                .SKARTUANASTESIA252 = CheckEdit19.Checked
                .SKARTUANASTESIA253 = CheckEdit20.Checked
                .SKARTUANASTESIA254 = CheckEdit21.Checked
                .SKARTUANASTESIA255 = CheckEdit22.Checked
                .SKARTUANASTESIA256 = CheckEdit23.Checked
                .SKARTUANASTESIA257 = CheckEdit24.Checked
                .SKARTUANASTESIA258 = CheckEdit25.Checked
                .SKARTUANASTESIA259 = CheckEdit26.Checked
                .SKARTUANASTESIA260 = CheckEdit27.Checked
                .SKARTUANASTESIA261 = CheckEdit28.Checked
                .SKARTUANASTESIA262 = CheckEdit29.Checked
                .SKARTUANASTESIA263 = CheckEdit30.Checked
                .SKARTUANASTESIA264 = CheckEdit31.Checked
                .SKARTUANASTESIA265 = CheckEdit32.Checked
                .SKARTUANASTESIA266 = CheckEdit33.Checked
                .SKARTUANASTESIA267 = CheckEdit34.Checked
                .SKARTUANASTESIA268 = CheckEdit35.Checked
                .SKARTUANASTESIA269 = CheckEdit36.Checked
                .SKARTUANASTESIA270 = CheckEdit37.Checked
                .SKARTUANASTESIA271 = CheckEdit38.Checked
                .SKARTUANASTESIA272 = CheckEdit39.Checked
                .SKARTUANASTESIA273 = CheckEdit40.Checked
                .SKARTUANASTESIA274 = CheckEdit41.Checked
                '.SKARTUANASTESIA275 = CheckEdit42.Checked
                .SKARTUANASTESIA276 = CheckEdit43.Checked
                .SKARTUANASTESIA277 = CheckEdit44.Checked
                .SKARTUANASTESIA278 = CheckEdit45.Checked
                .SKARTUANASTESIA279 = CheckEdit46.Checked
                .SKARTUANASTESIA280 = CheckEdit47.Checked
                .SKARTUANASTESIA281 = CheckEdit48.Checked
                .SKARTUANASTESIA282 = CheckEdit49.Checked
                .SKARTUANASTESIA283 = CheckEdit50.Checked
                .SKARTUANASTESIA284 = CheckEdit51.Checked
                .SKARTUANASTESIA285 = CheckEdit52.Checked
                .SKARTUANASTESIA286 = CheckEdit53.Checked
                .SKARTUANASTESIA287 = CheckEdit54.Checked
                .SKARTUANASTESIA288 = CheckEdit55.Checked
                .SKARTUANASTESIA289 = CheckEdit56.Checked
                .SKARTUANASTESIA290 = CheckEdit57.Checked
                .SKARTUANASTESIA291 = CheckEdit58.Checked
                .SKARTUANASTESIA292 = CheckEdit59.Checked
                .SKARTUANASTESIA293 = CheckEdit60.Checked
                .SKARTUANASTESIA294 = CheckEdit61.Checked
                .SKARTUANASTESIA295 = CheckEdit62.Checked
                .SKARTUANASTESIA296 = CheckEdit63.Checked
                .SKARTUANASTESIA297 = CheckEdit64.Checked
                .SKARTUANASTESIA298 = CheckEdit65.Checked
                .SKARTUANASTESIA299 = CheckEdit66.Checked
                .SKARTUANASTESIA300 = CheckEdit67.Checked
                .SKARTUANASTESIA301 = CheckEdit68.Checked
                .SKARTUANASTESIA302 = CheckEdit69.Checked
                .SKARTUANASTESIA303 = CheckEdit70.Checked
                .SKARTUANASTESIA304 = CheckEdit71.Checked
                .SKARTUANASTESIA305 = CheckEdit72.Checked
                .SKARTUANASTESIA306 = CheckEdit73.Checked
                .SKARTUANASTESIA307 = CheckEdit74.Checked
                .SKARTUANASTESIA308 = CheckEdit75.Checked
                .SKARTUANASTESIA309 = CheckEdit76.Checked
                .SKARTUANASTESIA310 = CheckEdit77.Checked
                .SKARTUANASTESIA311 = CheckEdit78.Checked
                .SKARTUANASTESIA312 = CheckEdit79.Checked
                .SKARTUANASTESIA313 = CheckEdit80.Checked
                .SKARTUANASTESIA314 = CheckEdit81.Checked
                .SKARTUANASTESIA315 = CheckEdit82.Checked
                .SKARTUANASTESIA316 = CheckEdit83.Checked
                .SKARTUANASTESIA317 = CheckEdit84.Checked
                .SKARTUANASTESIA318 = CheckEdit85.Checked
                .SKARTUANASTESIA319 = CheckEdit86.Checked
                .SKARTUANASTESIA320 = CheckEdit87.Checked
                .SKARTUANASTESIA321 = CheckEdit88.Checked

                .SKARTUANASTESIA322 = MemoEdit1.Text
                .SKARTUANASTESIA323 = MemoEdit2.Text
                .SKARTUANASTESIA324 = MemoEdit3.Text
                .SKARTUANASTESIA325 = MemoEdit4.Text
                .SKARTUANASTESIA326 = MemoEdit5.Text
                .SKARTUANASTESIA327 = MemoEdit6.Text

                .SKARTUANASTESIA328 = TimeEdit1.Text
                .SKARTUANASTESIA329 = TimeEdit2.Text
                .SKARTUANASTESIA330 = TimeEdit3.Text
                .SKARTUANASTESIA331 = TimeEdit4.Text
                .SKARTUANASTESIA332 = TimeEdit5.Text
                .SKARTUANASTESIA333 = TimeEdit6.Text
                .SKARTUANASTESIA334 = TimeEdit7.Text
                .SKARTUANASTESIA335 = TimeEdit8.Text
                .SKARTUANASTESIA336 = TimeEdit9.Text
                .SKARTUANASTESIA337 = TimeEdit10.Text
                .SKARTUANASTESIA338 = TimeEdit11.Text
                .SKARTUANASTESIA339 = TimeEdit12.Text
                .SKARTUANASTESIA340 = TimeEdit13.Text
                .SKARTUANASTESIA341 = MemoEdit8.Text

                .SKARTUANASTESIA342 = TimeEdit14.Text
                .SKARTUANASTESIA343 = TimeEdit15.Text
                .SKARTUANASTESIA344 = TimeEdit16.Text
                .SKARTUANASTESIA345 = TimeEdit17.Text
                .SKARTUANASTESIA346 = TimeEdit18.Text
                .SKARTUANASTESIA347 = TimeEdit19.Text
                .SKARTUANASTESIA348 = TimeEdit20.Text
                .SKARTUANASTESIA349 = TimeEdit21.Text
                .SKARTUANASTESIA350 = TimeEdit22.Text
                .SKARTUANASTESIA351 = TimeEdit23.Text
                .SKARTUANASTESIA352 = TimeEdit24.Text
                .SKARTUANASTESIA353 = TimeEdit25.Text

                .SKARTUANASTESIA354 = TextEdit227.Text
                .SKARTUANASTESIA355 = TextEdit228.Text
                .SKARTUANASTESIA356 = TextEdit229.Text
                .SKARTUANASTESIA357 = TextEdit230.Text
                .SKARTUANASTESIA358 = TextEdit231.Text
                .SKARTUANASTESIA359 = TextEdit232.Text
                .SKARTUANASTESIA360 = TextEdit233.Text
                .SKARTUANASTESIA361 = TextEdit234.Text
                .SKARTUANASTESIA362 = TextEdit235.Text
                .SKARTUANASTESIA363 = TextEdit236.Text
                .SKARTUANASTESIA364 = TextEdit237.Text
                .SKARTUANASTESIA365 = TextEdit238.Text

                Try
                    Dim ms As New IO.MemoryStream()
                    picMonitoring1.Image.Save(ms, picMonitoring1.Image.RawFormat)
                    Dim data As Byte() = ms.GetBuffer()
                    .imgMonitoringAnestesi = data
                Catch oErr As Exception
                    Try
                        .imgMonitoringAnestesi = oS_DIGITAL_OK_KARTU_ANESTESI_A.GetData(txtNoRegister.Text).imgMonitoringAnestesi
                    Catch ex As Exception
                    End Try
                End Try

                Try
                    Dim ms As New IO.MemoryStream()
                    picMonitoring2.Image.Save(ms, picMonitoring2.Image.RawFormat)
                    Dim data As Byte() = ms.GetBuffer()
                    .imgMonitoringSkalaNyeri = data
                Catch oErr As Exception
                    Try
                        .imgMonitoringSkalaNyeri = oS_DIGITAL_OK_KARTU_ANESTESI_A.GetData(txtNoRegister.Text).imgMonitoringSkalaNyeri
                    Catch ex As Exception
                    End Try
                End Try

                Try
                    .CETAK = oS_DIGITAL_OK_KARTU_ANESTESI_A.GetData(txtNoRegister.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try
                .KDUSER = sUserID
                .KDUSER_SIGNATURE = sUserID

            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_OK_KARTU_ANESTESI_A.InsertData(ds)

                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_OK_KARTU_ANESTESI_A.UpdateData(ds)
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
    Private Sub btnSaveClosee_Click() Handles btnSaveClosee.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            oFormMode = FORM_MODE.FORM_MODE_EDIT
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
        End If
    End Sub

    Private Sub ResetGambarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResetGambarToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            picMonitoring1.Image = CType(My.Resources.ResourceManager.GetObject("KartuAnastesiA_1_monitoringanestesi"), Image)
        ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
            Try
                ' ***** HEADER *****
                Dim ds = oS_DIGITAL_OK_KARTU_ANESTESI_A.GetData(txtNoRegister.Text)

                With ds
                    Try
                        Dim img = oS_DIGITAL_OK_KARTU_ANESTESI_A.GetData(txtNoRegister.Text).imgMonitoringAnestesi

                        picMonitoring1.Image = ByteArrayToImage(img.ToArray())
                    Catch oErr As Exception
                        MsgBox("Load List Data Gambar tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
                    End Try

                End With
            Catch oErr As Exception
                MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub

    Private Sub picMonitoring1_Click(sender As Object, e As MouseEventArgs) Handles picMonitoring1.Click
        If cboPilih.Text = "" Then
            MsgBox(Statement.ErrorStatement & vbCrLf & "Silahkan pilih keterangan simbol", MsgBoxStyle.Exclamation, Me.Text)
            cboPilih.Focus()
        Else
            If cboPilih.SelectedIndex <= 10 Then
                Dim g As Graphics = Graphics.FromImage(picMonitoring1.Image)
                Dim xpoint As Single = e.X - 8
                Dim ypoint As Single = e.Y - 8
                g.DrawString(cboPilih.Text.Substring(0, 1), New Font("Arial", 14, FontStyle.Regular), Brushes.DarkBlue, New PointF(xpoint, ypoint))
                g.Dispose()
                picMonitoring1.Invalidate()
            Else
                Dim g As Graphics = Graphics.FromImage(picMonitoring1.Image)
                Dim xpoint As Single = e.X - 8
                Dim ypoint As Single = e.Y - 8
                g.DrawString(cboPilih.Text, New Font("Arial", 14, FontStyle.Regular), Brushes.DarkBlue, New PointF(xpoint, ypoint))
                g.Dispose()
                picMonitoring1.Invalidate()
            End If
        End If
    End Sub

    Private Sub ResetGambarToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ResetGambarToolStripMenuItem1.Click
        If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            picMonitoring2.Image = CType(My.Resources.ResourceManager.GetObject("KartuAnastesiA_2_monitoringskalanyeri"), Image)
        ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
            Try
                ' ***** HEADER *****
                Dim ds = oS_DIGITAL_OK_KARTU_ANESTESI_A.GetData(txtNoRegister.Text)

                With ds
                    Try
                        Dim img = oS_DIGITAL_OK_KARTU_ANESTESI_A.GetData(txtNoRegister.Text).imgMonitoringSkalaNyeri

                        picMonitoring2.Image = ByteArrayToImage(img.ToArray())
                    Catch oErr As Exception
                        MsgBox("Load List Data Gambar tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
                    End Try

                End With
            Catch oErr As Exception
                MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub

    Private Sub picMonitoring2_Click(sender As Object, e As MouseEventArgs) Handles picMonitoring2.Click
        If cboPilih2.Text = "" Then
            MsgBox(Statement.ErrorStatement & vbCrLf & "Silahkan pilih keterangan simbol", MsgBoxStyle.Exclamation, Me.Text)
            cboPilih2.Focus()
        Else
            If cboPilih2.SelectedIndex <= 10 Then
                Dim g As Graphics = Graphics.FromImage(picMonitoring2.Image)
                Dim xpoint As Single = e.X - 8
                Dim ypoint As Single = e.Y - 8
                g.DrawString(cboPilih2.Text.Substring(0, 1), New Font("Arial", 14, FontStyle.Regular), Brushes.DarkBlue, New PointF(xpoint, ypoint))
                g.Dispose()
                picMonitoring2.Invalidate()
            Else
                Dim g As Graphics = Graphics.FromImage(picMonitoring2.Image)
                Dim xpoint As Single = e.X - 8
                Dim ypoint As Single = e.Y - 8
                g.DrawString(cboPilih2.Text, New Font("Arial", 14, FontStyle.Regular), Brushes.DarkBlue, New PointF(xpoint, ypoint))
                g.Dispose()
                picMonitoring2.Invalidate()
            End If
        End If
    End Sub



    Private Sub CheckEdit89_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEdit89.CheckedChanged
        If CheckEdit89.Checked Then
            TextEdit5.Text = "(+)"
        Else
            TextEdit5.ResetText()
        End If
    End Sub

    Private Sub TextEdit8_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit8.EditValueChanged
        fn_GCSValue()
    End Sub

    Private Sub TextEdit9_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit9.EditValueChanged
        fn_GCSValue()
    End Sub

    Private Sub TextEdit10_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit10.EditValueChanged
        fn_GCSValue()
    End Sub

    Private Sub fn_GCSValue()
        Try
            Dim val As Integer
            val = CInt(TextEdit8.EditValue) + CInt(TextEdit9.EditValue) + CInt(TextEdit10.EditValue)
            TextEdit7.Text = val.ToString
        Catch ex As Exception
            MsgBox("Load Sub Spesialis Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try

    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_Department()
        Dim oDepartment As New Reference.clsDepartment
        Try
            grdRuangan.Properties.DataSource = oDepartment.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdRuangan.Properties.ValueMember = "KDDEPARTMENT"
            grdRuangan.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Asisten()
        'Dim oStaf As New Setting.clsUser
        'Try
        '    Dim dsList = oStaf.GetData.Where(Function(x) x.ISACTIVE = True)

        '    grdASISTENBEDAH.Properties.DataSource = dsList.ToList()
        '    grdASISTENBEDAH.Properties.ValueMember = "KDSTAFF"
        '    grdASISTENBEDAH.Properties.DisplayMember = "NAME_DISPLAY"

        '    grdASISTENANESTESI.Properties.DataSource = dsList.ToList()
        '    grdASISTENANESTESI.Properties.ValueMember = "KDSTAFF"
        '    grdASISTENANESTESI.Properties.DisplayMember = "NAME_DISPLAY"

        '    grdPERAWATANESTESIPJ.Properties.DataSource = dsList.ToList()
        '    grdPERAWATANESTESIPJ.Properties.ValueMember = "KDSTAFF"
        '    grdPERAWATANESTESIPJ.Properties.DisplayMember = "NAME_DISPLAY"

        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try

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
            SQL &= "KDSTAFF = A.KDUSER "
            SQL &= ",NAME_DISPLAY = A.KDUSER "
            SQL &= "FROM "
            SQL &= "[USER]..SET_USER A "
            SQL &= "WHERE A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "USERUNIT")

            grdASISTENBEDAH.Properties.DataSource = ds.Tables("USERUNIT")
            grdASISTENBEDAH.Properties.ValueMember = "KDSTAFF"
            grdASISTENBEDAH.Properties.DisplayMember = "NAME_DISPLAY"

            grdASISTENANESTESI.Properties.DataSource = ds.Tables("USERUNIT")
            grdASISTENANESTESI.Properties.ValueMember = "KDSTAFF"
            grdASISTENANESTESI.Properties.DisplayMember = "NAME_DISPLAY"

            grdPERAWATANESTESIPJ.Properties.DataSource = ds.Tables("USERUNIT")
            grdPERAWATANESTESIPJ.Properties.ValueMember = "KDSTAFF"
            grdPERAWATANESTESIPJ.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Doctor()
        Dim oDPJP As New Reference.clsDoctor
        Try
            grdDOKTERBEDAH.Properties.DataSource = oDPJP.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdDOKTERBEDAH.Properties.ValueMember = "KDDOCTOR"
            grdDOKTERBEDAH.Properties.DisplayMember = "NAME_DISPLAY"

            grdDOKTERANESTESI.Properties.DataSource = oDPJP.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdDOKTERANESTESI.Properties.ValueMember = "KDDOCTOR"
            grdDOKTERANESTESI.Properties.DisplayMember = "NAME_DISPLAY"

            grdDOKTERANESTESIPJ.Properties.DataSource = oDPJP.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdDOKTERANESTESIPJ.Properties.ValueMember = "KDDOCTOR"
            grdDOKTERANESTESIPJ.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try

    End Sub

    Private Sub grdLab_DoubleClick(sender As Object, e As EventArgs) Handles grdLab.DoubleClick
        If grvLab.GetFocusedRowCellValue("Kategori") Is Nothing Then
            Exit Sub
        Else
            Dim str As String

            If MemoEdit1.Text = "" Then
                str = grvLab.GetFocusedRowCellValue("Kategori")
            Else
                str = MemoEdit1.Text + ", " + grvLab.GetFocusedRowCellValue("Kategori")
            End If

            MemoEdit1.Text = str
        End If
    End Sub

    Private Sub frmKartuAnastesi_A_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
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
            myView.Y = -scrollchange - myView.Y
        Else
            'down
            myView.X = -myView.X
            myView.Y = scrollchange - myView.Y
        End If

        Me.Panel1.AutoScrollPosition = myView
    End Sub
#End Region
End Class