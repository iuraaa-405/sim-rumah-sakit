Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Data.SqlClient

Public Class frmEMedrekRJ_23
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_RJ_23 As New Digital.clsDigital_RJ_23
    Private down As Boolean = False
    Private KDDOCTOR As String
    Private NMDOCTOR  As String
    Private KDDOCTOR2 As String
    Private NMDOCTOR2  As String
    Private sKoneksi As String = String.Empty
    Private sIsOtority As Boolean = False

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String)
        oFormMode = FormMode

        Dim oSetKoneksi As New Setting.clsSetKoneksi
        Dim dsSetKoneksi = oSetKoneksi.GetData()
        If dsSetKoneksi IsNot Nothing Then
            sKoneksi = dsSetKoneksi.KONEKSI
        End If

        Dim oPendaftaran As New Identitas.clsIdentitasPasien
        Dim dsPendaftaran = oPendaftaran.GetData(KDREG)

        txtNoRegister.Text = KDREG

        If dsPendaftaran IsNot Nothing Then
            txtNamaPasien.Text = dsPendaftaran.NAMAPASIEN
            txtUmur.Text = dsPendaftaran.USIA

            If dsPendaftaran.JENISKELAMIN = "L" Then
                chkJKL.Checked = True
                chkJKP.Checked = False
            Else
                chkJKL.Checked = False
                chkJKP.Checked = True
            End If
            txtAgama.Text = dsPendaftaran.AGAMA
            txtSuku.Text = dsPendaftaran.SUKU
            txtPangkat.Text = dsPendaftaran.PANGKAT
            txtDiagnosaKeperawatan.Text = dsPendaftaran.KDDIAGNOSA
            txtKesatuan.Text = dsPendaftaran.KESATUAN
            txtPendidikan.Text = dsPendaftaran.PENDIDIKAN
            txtNoRegister.Text = dsPendaftaran.KDKUNJUNGAN
            txtNoPasien.Text = dsPendaftaran.KDCUSTOMER
            txtTgl.Text = dsPendaftaran.DATE.ToString("dd-MM-yyyy HH:mm:ss")
            txtNoTelepon.Text = dsPendaftaran.NOMORTELEPON
            txtAlamat.Text = dsPendaftaran.ALAMAT
            KDDOCTOR = dsPendaftaran.KDDOKTER
            NMDOCTOR = dsPendaftaran.DOKTER
            
        Else
            txtNamaPasien.ResetText()
            txtUmur.ResetText()
            chkJKL.Checked = False
            chkJKP.Checked = True
            txtAgama.ResetText()
            txtSuku.ResetText()
            txtPangkat.ResetText()
            txtDiagnosaKeperawatan.ResetText()
            txtKesatuan.ResetText()
            txtPendidikan.ResetText()
            txtNoRegister.ResetText()
            txtNoPasien.ResetText()
            txtTgl.ResetText()
            txtNoTelepon.ResetText()
            grdPerawat.ResetText()
            txtAlamat.ResetText()
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = EMedrekRJ_23.TITLE
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
        fn_BIDAN()

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

        chkAUTO.Properties.ReadOnly = Status
        chkALLO.Properties.ReadOnly = Status
        txtNAMAPENGKAJIAN.Properties.ReadOnly = Status
        txtDIGITALRJ_2.Properties.ReadOnly = Status
        TextEdit1.Properties.ReadOnly = Status
        TextEdit2.Properties.ReadOnly = Status
        TextEdit3.Properties.ReadOnly = Status
        TextEdit4.Properties.ReadOnly = Status
        TextEdit5.Properties.ReadOnly = Status
        TextEdit10.Properties.ReadOnly = Status
        CheckEdit1.Properties.ReadOnly = Status
        CheckEdit2.Properties.ReadOnly = Status
        CheckEdit3.Properties.ReadOnly = Status
        CheckEdit4.Properties.ReadOnly = Status
        TextEdit7.Properties.ReadOnly = Status
        MemoEdit1.Properties.ReadOnly = Status
        TextEdit6.Properties.ReadOnly = Status
        CheckEdit6.Properties.ReadOnly = Status
        CheckEdit5.Properties.ReadOnly = Status
        CheckEdit8.Properties.ReadOnly = Status
        CheckEdit7.Properties.ReadOnly = Status
        TextEdit8.Properties.ReadOnly = Status
        TextEdit9.Properties.ReadOnly = Status
        TextEdit11.Properties.ReadOnly = Status
        TextEdit12.Properties.ReadOnly = Status
        CheckEdit9.Properties.ReadOnly = Status
        CheckEdit10.Properties.ReadOnly = Status
        CheckEdit13.Properties.ReadOnly = Status
        CheckEdit12.Properties.ReadOnly = Status
        CheckEdit11.Properties.ReadOnly = Status
        CheckEdit18.Properties.ReadOnly = Status
        CheckEdit17.Properties.ReadOnly = Status
        CheckEdit16.Properties.ReadOnly = Status
        CheckEdit15.Properties.ReadOnly = Status
        CheckEdit14.Properties.ReadOnly = Status
        CheckEdit19.Properties.ReadOnly = Status
        CheckEdit25.Properties.ReadOnly = Status
        CheckEdit24.Properties.ReadOnly = Status
        CheckEdit23.Properties.ReadOnly = Status
        CheckEdit22.Properties.ReadOnly = Status
        CheckEdit21.Properties.ReadOnly = Status
        CheckEdit20.Properties.ReadOnly = Status
        CheckEdit31.Properties.ReadOnly = Status
        CheckEdit30.Properties.ReadOnly = Status
        CheckEdit29.Properties.ReadOnly = Status
        CheckEdit28.Properties.ReadOnly = Status
        CheckEdit27.Properties.ReadOnly = Status
        CheckEdit26.Properties.ReadOnly = Status
        CheckEdit37.Properties.ReadOnly = Status
        CheckEdit36.Properties.ReadOnly = Status
        CheckEdit35.Properties.ReadOnly = Status
        CheckEdit34.Properties.ReadOnly = Status
        CheckEdit33.Properties.ReadOnly = Status
        CheckEdit32.Properties.ReadOnly = Status
        CheckEdit50.Properties.ReadOnly = Status
        CheckEdit51.Properties.ReadOnly = Status
        CheckEdit52.Properties.ReadOnly = Status
        CheckEdit53.Properties.ReadOnly = Status
        CheckEdit54.Properties.ReadOnly = Status
        TextEdit13.Properties.ReadOnly = Status
        TextEdit14.Properties.ReadOnly = Status
        DateEdit1.Properties.ReadOnly = Status
        TextEdit15.Properties.ReadOnly = Status
        TextEdit16.Properties.ReadOnly = Status
        TextEdit17.Properties.ReadOnly = Status
        TextEdit18.Properties.ReadOnly = Status
        TextEdit19.Properties.ReadOnly = Status
        TextEdit20.Properties.ReadOnly = Status
        TextEdit21.Properties.ReadOnly = Status
        TextEdit22.Properties.ReadOnly = Status
        TextEdit23.Properties.ReadOnly = Status
        TextEdit24.Properties.ReadOnly = Status
        DateEdit2.Properties.ReadOnly = Status
        TextEdit26.Properties.ReadOnly = Status
        TextEdit27.Properties.ReadOnly = Status
        TextEdit28.Properties.ReadOnly = Status
        TextEdit29.Properties.ReadOnly = Status
        TextEdit30.Properties.ReadOnly = Status
        TextEdit31.Properties.ReadOnly = Status
        TextEdit32.Properties.ReadOnly = Status
        TextEdit33.Properties.ReadOnly = Status
        TextEdit25.Properties.ReadOnly = Status
        TextEdit34.Properties.ReadOnly = Status
        DateEdit3.Properties.ReadOnly = Status
        TextEdit35.Properties.ReadOnly = Status
        TextEdit36.Properties.ReadOnly = Status
        TextEdit37.Properties.ReadOnly = Status
        TextEdit38.Properties.ReadOnly = Status
        TextEdit39.Properties.ReadOnly = Status
        TextEdit40.Properties.ReadOnly = Status
        TextEdit41.Properties.ReadOnly = Status
        TextEdit42.Properties.ReadOnly = Status
        TextEdit43.Properties.ReadOnly = Status
        TextEdit44.Properties.ReadOnly = Status
        DateEdit4.Properties.ReadOnly = Status
        TextEdit45.Properties.ReadOnly = Status
        TextEdit46.Properties.ReadOnly = Status
        TextEdit47.Properties.ReadOnly = Status
        TextEdit48.Properties.ReadOnly = Status
        TextEdit49.Properties.ReadOnly = Status
        TextEdit50.Properties.ReadOnly = Status
        TextEdit51.Properties.ReadOnly = Status
        TextEdit52.Properties.ReadOnly = Status
        TextEdit53.Properties.ReadOnly = Status
        TextEdit54.Properties.ReadOnly = Status
        DateEdit5.Properties.ReadOnly = Status
        TextEdit55.Properties.ReadOnly = Status
        TextEdit56.Properties.ReadOnly = Status
        TextEdit57.Properties.ReadOnly = Status
        TextEdit58.Properties.ReadOnly = Status
        TextEdit59.Properties.ReadOnly = Status
        TextEdit60.Properties.ReadOnly = Status
        TextEdit61.Properties.ReadOnly = Status
        TextEdit62.Properties.ReadOnly = Status
        TextEdit63.Properties.ReadOnly = Status
        TextEdit64.Properties.ReadOnly = Status
        DateEdit6.Properties.ReadOnly = Status
        TextEdit65.Properties.ReadOnly = Status
        TextEdit66.Properties.ReadOnly = Status
        TextEdit67.Properties.ReadOnly = Status
        TextEdit68.Properties.ReadOnly = Status
        TextEdit69.Properties.ReadOnly = Status
        TextEdit70.Properties.ReadOnly = Status
        TextEdit71.Properties.ReadOnly = Status
        TextEdit72.Properties.ReadOnly = Status
        TextEdit73.Properties.ReadOnly = Status
        TextEdit74.Properties.ReadOnly = Status
        DateEdit7.Properties.ReadOnly = Status
        TextEdit75.Properties.ReadOnly = Status
        TextEdit76.Properties.ReadOnly = Status
        TextEdit77.Properties.ReadOnly = Status
        TextEdit78.Properties.ReadOnly = Status
        TextEdit79.Properties.ReadOnly = Status
        TextEdit80.Properties.ReadOnly = Status
        TextEdit81.Properties.ReadOnly = Status
        TextEdit82.Properties.ReadOnly = Status
        TextEdit83.Properties.ReadOnly = Status
        chkSKRINING_GIZI_01_1.Properties.ReadOnly = Status
        chkSKRINING_GIZI_01_2.Properties.ReadOnly = Status
        chkSKRINING_GIZI_02_1.Properties.ReadOnly = Status
        chkSKRINING_GIZI_02_2.Properties.ReadOnly = Status
        chkSKRINING_GIZI_03_1.Properties.ReadOnly = Status
        chkSKRINING_GIZI_03_2.Properties.ReadOnly = Status
        chkSKRINING_RESIKO_JATUH_01_1.Properties.ReadOnly = Status
        chkSKRINING_RESIKO_JATUH_01_2.Properties.ReadOnly = Status
        chkSKRINING_RESIKO_JATUH_02_1.Properties.ReadOnly = Status
        chkSKRINING_RESIKO_JATUH_02_2.Properties.ReadOnly = Status

        chkRESIKO_JATUH_01.Properties.ReadOnly = Status
        chkRESIKO_JATUH_02.Properties.ReadOnly = Status
        chkRESIKO_JATUH_03.Properties.ReadOnly = Status

        chkTINDAKAN_01_YA.Properties.ReadOnly = Status
        chkTINDAKAN_02_YA.Properties.ReadOnly = Status
        chkTINDAKAN_03_YA.Properties.ReadOnly = Status
        chkTINDAKAN_04_YA.Properties.ReadOnly = Status

        chkTINDAKAN_01_TIDAK.Properties.ReadOnly = Status
        chkTINDAKAN_02_TIDAK.Properties.ReadOnly = Status
        chkTINDAKAN_03_TIDAK.Properties.ReadOnly = Status
        chkTINDAKAN_04_TIDAK.Properties.ReadOnly = Status

        grdPerawat1.Properties.ReadOnly = Status
        grdPerawat2.Properties.ReadOnly = Status
        grdPerawat3.Properties.ReadOnly = Status
        grdPerawat4.Properties.ReadOnly = Status

        txtPASIENKELUARGA1.ReadOnly = Status
        txtPASIENKELUARGA2.ReadOnly = Status
        txtPASIENKELUARGA3.ReadOnly = Status
        txtPASIENKELUARGA4.ReadOnly = Status

        CheckEdit39.Properties.ReadOnly = Status
        CheckEdit43.Properties.ReadOnly = Status
        CheckEdit48.Properties.ReadOnly = Status
        CheckEdit49.Properties.ReadOnly = Status
        CheckEdit44.Properties.ReadOnly = Status
        CheckEdit40.Properties.ReadOnly = Status
        CheckEdit41.Properties.ReadOnly = Status
        CheckEdit46.Properties.ReadOnly = Status
        CheckEdit47.Properties.ReadOnly = Status
        CheckEdit45.Properties.ReadOnly = Status
        CheckEdit38.Properties.ReadOnly = Status
        txtDIGITALRJ_79.Properties.ReadOnly = Status
        txtDIGITALRJ_81.Properties.ReadOnly = Status
        txtDIGITALRJ_83.Properties.ReadOnly = Status
        txtDIGITALRJ_85.Properties.ReadOnly = Status
        txtDIGITALRJ_80.Properties.ReadOnly = Status
        txtDIGITALRJ_82.Properties.ReadOnly = Status
        txtDIGITALRJ_84.Properties.ReadOnly = Status
        txtDIGITALRJ_86.Properties.ReadOnly = Status
        CheckEdit55.Properties.ReadOnly = Status
        CheckEdit56.Properties.ReadOnly = Status
        CheckEdit57.Properties.ReadOnly = Status
        CheckEdit58.Properties.ReadOnly = Status
        TextEdit84.Properties.ReadOnly = Status
        CheckEdit61.Properties.ReadOnly = Status
        CheckEdit59.Properties.ReadOnly = Status
        TextEdit85.Properties.ReadOnly = Status
        CheckEdit62.Properties.ReadOnly = Status
        CheckEdit60.Properties.ReadOnly = Status
        TextEdit86.Properties.ReadOnly = Status
        CheckEdit66.Properties.ReadOnly = Status
        CheckEdit65.Properties.ReadOnly = Status
        CheckEdit63.Properties.ReadOnly = Status
        TextEdit87.Properties.ReadOnly = Status
        CheckEdit64.Properties.ReadOnly = Status
        TextEdit88.Properties.ReadOnly = Status
        CheckEdit68.Properties.ReadOnly = Status
        CheckEdit67.Properties.ReadOnly = Status
        CheckEdit70.Properties.ReadOnly = Status
        CheckEdit69.Properties.ReadOnly = Status
        CheckEdit71.Properties.ReadOnly = Status
        CheckEdit72.Properties.ReadOnly = Status
        CheckEdit73.Properties.ReadOnly = Status
        CheckEdit74.Properties.ReadOnly = Status
        CheckEdit75.Properties.ReadOnly = Status
        CheckEdit76.Properties.ReadOnly = Status
        CheckEdit77.Properties.ReadOnly = Status
        CheckEdit78.Properties.ReadOnly = Status
        CheckEdit79.Properties.ReadOnly = Status
        CheckEdit80.Properties.ReadOnly = Status
        CheckEdit81.Properties.ReadOnly = Status
        CheckEdit82.Properties.ReadOnly = Status
        CheckEdit83.Properties.ReadOnly = Status
        CheckEdit84.Properties.ReadOnly = Status
        CheckEdit85.Properties.ReadOnly = Status
        CheckEdit86.Properties.ReadOnly = Status
        CheckEdit87.Properties.ReadOnly = Status
        CheckEdit88.Properties.ReadOnly = Status
        TextEdit89.Properties.ReadOnly = Status
        CheckEdit89.Properties.ReadOnly = Status
        CheckEdit90.Properties.ReadOnly = Status
        CheckEdit91.Properties.ReadOnly = Status
        CheckEdit92.Properties.ReadOnly = Status
        TextEdit90.Properties.ReadOnly = Status
        CheckEdit93.Properties.ReadOnly = Status
        TextEdit91.Properties.ReadOnly = Status
        CheckEdit94.Properties.ReadOnly = Status
        TextEdit98.Properties.ReadOnly = Status
        CheckEdit130.Properties.ReadOnly = Status
        CheckEdit95.Properties.ReadOnly = Status
        CheckEdit96.Properties.ReadOnly = Status
        CheckEdit97.Properties.ReadOnly = Status
        TextEdit92.Properties.ReadOnly = Status
        TextEdit93.Properties.ReadOnly = Status
        TextEdit94.Properties.ReadOnly = Status
        TextEdit96.Properties.ReadOnly = Status
        CheckEdit101.Properties.ReadOnly = Status
        CheckEdit100.Properties.ReadOnly = Status
        TextEdit97.Properties.ReadOnly = Status
        CheckEdit103.Properties.ReadOnly = Status
        CheckEdit102.Properties.ReadOnly = Status
        CheckEdit104.Properties.ReadOnly = Status
        CheckEdit105.Properties.ReadOnly = Status
        TextEdit99.Properties.ReadOnly = Status
        CheckEdit108.Properties.ReadOnly = Status
        CheckEdit107.Properties.ReadOnly = Status
        CheckEdit112.Properties.ReadOnly = Status
        CheckEdit111.Properties.ReadOnly = Status
        CheckEdit114.Properties.ReadOnly = Status
        CheckEdit113.Properties.ReadOnly = Status
        TextEdit100.Properties.ReadOnly = Status
        CheckEdit116.Properties.ReadOnly = Status
        CheckEdit115.Properties.ReadOnly = Status
        CheckEdit123.Properties.ReadOnly = Status
        CheckEdit122.Properties.ReadOnly = Status
        CheckEdit125.Properties.ReadOnly = Status
        CheckEdit124.Properties.ReadOnly = Status
        CheckEdit127.Properties.ReadOnly = Status
        CheckEdit126.Properties.ReadOnly = Status
        CheckEdit129.Properties.ReadOnly = Status
        CheckEdit128.Properties.ReadOnly = Status
        CheckEdit133.Properties.ReadOnly = Status
        CheckEdit132.Properties.ReadOnly = Status
        CheckEdit99.Properties.ReadOnly = Status
        CheckEdit98.Properties.ReadOnly = Status
        CheckEdit109.Properties.ReadOnly = Status
        CheckEdit106.Properties.ReadOnly = Status
        CheckEdit117.Properties.ReadOnly = Status
        CheckEdit110.Properties.ReadOnly = Status
        CheckEdit118.Properties.ReadOnly = Status
        TextEdit101.Properties.ReadOnly = Status
        CheckEdit120.Properties.ReadOnly = Status
        CheckEdit119.Properties.ReadOnly = Status
        CheckEdit131.Properties.ReadOnly = Status
        CheckEdit121.Properties.ReadOnly = Status
        TextEdit102.Properties.ReadOnly = Status
        TextEdit103.Properties.ReadOnly = Status
        TextEdit104.Properties.ReadOnly = Status
        TextEdit105.Properties.ReadOnly = Status
        TextEdit106.Properties.ReadOnly = Status
        TextEdit107.Properties.ReadOnly = Status
        TextEdit108.Properties.ReadOnly = Status
        CheckEdit135.Properties.ReadOnly = Status
        CheckEdit134.Properties.ReadOnly = Status
        TextEdit109.Properties.ReadOnly = Status
        TextEdit110.Properties.ReadOnly = Status
        TextEdit111.Properties.ReadOnly = Status
        CheckEdit137.Properties.ReadOnly = Status
        CheckEdit136.Properties.ReadOnly = Status
        TextEdit112.Properties.ReadOnly = Status
        CheckEdit141.Properties.ReadOnly = Status
        CheckEdit142.Properties.ReadOnly = Status
        CheckEdit144.Properties.ReadOnly = Status
        CheckEdit143.Properties.ReadOnly = Status
        CheckEdit146.Properties.ReadOnly = Status
        CheckEdit145.Properties.ReadOnly = Status
        TextEdit113.Properties.ReadOnly = Status
        TextEdit115.Properties.ReadOnly = Status
        CheckEdit147.Properties.ReadOnly = Status
        CheckEdit138.Properties.ReadOnly = Status
        CheckEdit149.Properties.ReadOnly = Status
        CheckEdit140.Properties.ReadOnly = Status
        CheckEdit148.Properties.ReadOnly = Status
        CheckEdit139.Properties.ReadOnly = Status
        CheckEdit150.Properties.ReadOnly = Status
        CheckEdit152.Properties.ReadOnly = Status
        CheckEdit151.Properties.ReadOnly = Status
        TextEdit114.Properties.ReadOnly = Status
        TextEdit116.Properties.ReadOnly = Status
        TextEdit117.Properties.ReadOnly = Status
        TextEdit118.Properties.ReadOnly = Status
        TextEdit119.Properties.ReadOnly = Status
        TextEdit120.Properties.ReadOnly = Status
        TextEdit122.Properties.ReadOnly = Status
        TextEdit128.Properties.ReadOnly = Status
        TextEdit127.Properties.ReadOnly = Status
        TextEdit129.Properties.ReadOnly = Status
        TextEdit124.Properties.ReadOnly = Status
        TextEdit126.Properties.ReadOnly = Status
        TextEdit123.Properties.ReadOnly = Status
        MemoEdit2.Properties.ReadOnly = Status
        MemoEdit3.Properties.ReadOnly = Status
        CheckEdit163.Properties.ReadOnly = Status
        CheckEdit162.Properties.ReadOnly = Status
        CheckEdit164.Properties.ReadOnly = Status
        CheckEdit166.Properties.ReadOnly = Status
        CheckEdit165.Properties.ReadOnly = Status
        grdPerawat.Properties.ReadOnly = Status
        TextEdit95.Properties.ReadOnly = Status

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
        chkAUTO.Checked = False
        chkALLO.Checked = False
        txtNAMAPENGKAJIAN.ResetText()
        txtDIGITALRJ_2.ResetText()
        TextEdit1.ResetText()
        TextEdit2.ResetText()
        TextEdit3.ResetText()
        TextEdit4.ResetText()
        TextEdit5.ResetText()
        TextEdit10.ResetText()
        CheckEdit1.Checked = False
        CheckEdit2.Checked = False
        CheckEdit3.Checked = False
        CheckEdit4.Checked = False
        TextEdit7.ResetText()
        MemoEdit1.ResetText()
        TextEdit6.ResetText()   
        CheckEdit6.Checked = False
        CheckEdit5.Checked = False
        CheckEdit8.Checked = False
        CheckEdit7.Checked = False
        TextEdit8.ResetText()
        TextEdit9.ResetText()
        TextEdit11.ResetText()
        TextEdit12.ResetText()
        CheckEdit9.Checked = False
        CheckEdit10.Checked = False
        CheckEdit13.Checked = False
        CheckEdit12.Checked = False
        CheckEdit11.Checked = False
        CheckEdit18.Checked = False
        CheckEdit17.Checked = False
        CheckEdit16.Checked = False
        CheckEdit15.Checked = False
        CheckEdit14.Checked = False
        CheckEdit19.Checked = False
        CheckEdit25.Checked = False
        CheckEdit24.Checked = False
        CheckEdit23.Checked = False
        CheckEdit22.Checked = False
        CheckEdit21.Checked = False
        CheckEdit20.Checked = False
        CheckEdit31.Checked = False
        CheckEdit30.Checked = False
        CheckEdit29.Checked = False
        CheckEdit28.Checked = False
        CheckEdit27.Checked = False
        CheckEdit26.Checked = False
        CheckEdit37.Checked = False
        CheckEdit36.Checked = False
        CheckEdit35.Checked = False
        CheckEdit34.Checked = False
        CheckEdit33.Checked = False
        CheckEdit32.Checked = False
        CheckEdit50.Checked = False
        CheckEdit51.Checked = False
        CheckEdit52.Checked = False
        CheckEdit53.Checked = False
        CheckEdit54.Checked = False
        TextEdit13.ResetText()
        TextEdit14.ResetText()
        DateEdit1.ResetText()
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
        DateEdit2.ResetText()
        TextEdit26.ResetText()
        TextEdit27.ResetText()
        TextEdit28.ResetText()
        TextEdit29.ResetText()
        TextEdit30.ResetText()
        TextEdit31.ResetText()
        TextEdit32.ResetText()
        TextEdit33.ResetText()
        TextEdit25.ResetText()
        TextEdit34.ResetText()
        DateEdit3.ResetText()
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
        DateEdit4.ResetText()
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
        DateEdit5.ResetText()
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
        DateEdit6.ResetText()
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
        DateEdit7.ResetText()
        TextEdit75.ResetText()
        TextEdit76.ResetText()
        TextEdit77.ResetText()
        TextEdit78.ResetText()
        TextEdit78.ResetText()
        TextEdit80.ResetText()
        TextEdit81.ResetText()
        TextEdit82.ResetText()
        TextEdit83.ResetText()
        chkSKRINING_GIZI_01_1.Checked = False
        chkSKRINING_GIZI_01_2.Checked = False
        chkSKRINING_GIZI_02_1.Checked = False
        chkSKRINING_GIZI_02_2.Checked = False
        chkSKRINING_GIZI_03_1.Checked = False
        chkSKRINING_GIZI_03_2.Checked = False
        chkSKRINING_RESIKO_JATUH_01_1.Checked = False
        chkSKRINING_RESIKO_JATUH_01_2.Checked = False
        chkSKRINING_RESIKO_JATUH_02_1.Checked = False
        chkSKRINING_RESIKO_JATUH_02_2.Checked = False

        chkRESIKO_JATUH_01.Checked = False
        chkRESIKO_JATUH_02.Checked = False
        chkRESIKO_JATUH_03.Checked = False

        chkTINDAKAN_01_YA.Checked = False
        chkTINDAKAN_02_YA.Checked = False
        chkTINDAKAN_03_YA.Checked = False
        chkTINDAKAN_04_YA.Checked = False

        chkTINDAKAN_01_TIDAK.Checked = False
        chkTINDAKAN_02_TIDAK.Checked = False
        chkTINDAKAN_03_TIDAK.Checked = False
        chkTINDAKAN_04_TIDAK.Checked = False
        
        grdPerawat1.ResetText()
        grdPerawat2.ResetText()
        grdPerawat3.ResetText()
        grdPerawat4.ResetText()

        txtPASIENKELUARGA1.ResetText()
        txtPASIENKELUARGA2.ResetText()
        txtPASIENKELUARGA3.ResetText()
        txtPASIENKELUARGA4.ResetText()

        CheckEdit39.Checked = False
        CheckEdit43.Checked = False
        CheckEdit48.Checked = False
        CheckEdit49.Checked = False
        CheckEdit44.Checked = False
        CheckEdit40.Checked = False
        CheckEdit41.Checked = False
        CheckEdit46.Checked = False
        CheckEdit47.Checked = False
        CheckEdit45.Checked = False
        CheckEdit38.Checked = False
        txtDIGITALRJ_79.Checked = False
        txtDIGITALRJ_81.ResetText()
        txtDIGITALRJ_83.ResetText()
        txtDIGITALRJ_85.ResetText()
        txtDIGITALRJ_80.Checked = False
        txtDIGITALRJ_82.ResetText()
        txtDIGITALRJ_84.ResetText()
        txtDIGITALRJ_86.ResetText()
        CheckEdit55.Checked = False
        CheckEdit56.Checked = False
        CheckEdit57.Checked = False
        CheckEdit58.Checked = False
        TextEdit84.ResetText()
        CheckEdit61.Checked = False
        CheckEdit59.Checked = False
        TextEdit85.ResetText()
        CheckEdit62.Checked = False
        CheckEdit60.Checked = False
        TextEdit86.ResetText()
        CheckEdit66.Checked = False
        CheckEdit65.Checked = False
        CheckEdit63.Checked = False
        TextEdit87.ResetText()
        CheckEdit64.Checked = False
        TextEdit88.ResetText()
        CheckEdit68.Checked = False
        CheckEdit67.Checked = False
        CheckEdit70.Checked = False
        CheckEdit69.Checked = False
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
        TextEdit89.ResetText()
        CheckEdit89.Checked = False
        CheckEdit90.Checked = False
        CheckEdit91.Checked = False
        CheckEdit92.Checked = False
        TextEdit90.ResetText()
        CheckEdit93.Checked = False
        TextEdit91.ResetText()
        CheckEdit94.Checked = False
        TextEdit98.ResetText()
        CheckEdit130.Checked = False
        CheckEdit95.Checked = False
        CheckEdit96.Checked = False
        CheckEdit97.Checked = False
        TextEdit92.ResetText()
        TextEdit93.ResetText()
        TextEdit94.ResetText()
        TextEdit96.ResetText()
        CheckEdit101.Checked = False
        CheckEdit100.Checked = False
        TextEdit97.ResetText()
        CheckEdit103.Checked = False
        CheckEdit102.Checked = False
        CheckEdit104.Checked = False
        CheckEdit105.Checked = False
        TextEdit99.ResetText()
        CheckEdit108.Checked = False
        CheckEdit107.Checked = False
        CheckEdit112.Checked = False
        CheckEdit111.Checked = False
        CheckEdit114.Checked = False
        CheckEdit113.Checked = False
        TextEdit100.ResetText()
        CheckEdit116.Checked = False
        CheckEdit115.Checked = False
        CheckEdit123.Checked = False
        CheckEdit122.Checked = False
        CheckEdit125.Checked = False
        CheckEdit124.Checked = False
        CheckEdit127.Checked = False
        CheckEdit126.Checked = False
        CheckEdit129.Checked = False
        CheckEdit128.Checked = False
        CheckEdit133.Checked = False
        CheckEdit132.Checked = False
        CheckEdit99.Checked = False
        CheckEdit98.Checked = False
        CheckEdit109.Checked = False
        CheckEdit106.Checked = False
        CheckEdit117.Checked = False
        CheckEdit110.Checked = False
        CheckEdit118.Checked = False
        TextEdit101.ResetText()
        CheckEdit120.Checked = False
        CheckEdit119.Checked = False
        CheckEdit131.Checked = False
        CheckEdit121.Checked = False
        TextEdit102.ResetText()
        TextEdit103.ResetText()
        TextEdit104.ResetText()
        TextEdit105.ResetText()
        TextEdit106.ResetText()
        TextEdit107.ResetText()
        TextEdit108.ResetText()
        CheckEdit135.Checked = False
        CheckEdit134.Checked = False
        TextEdit109.ResetText()
        TextEdit110.ResetText()
        TextEdit111.ResetText()
        CheckEdit137.Checked = False
        CheckEdit136.Checked = False
        TextEdit112.ResetText()
        CheckEdit141.Checked = False
        CheckEdit142.Checked = False
        CheckEdit144.Checked = False
        CheckEdit143.Checked = False
        CheckEdit146.Checked = False
        CheckEdit145.Checked = False
        TextEdit113.ResetText()
        TextEdit115.ResetText()
        CheckEdit147.Checked = False
        CheckEdit138.Checked = False
        CheckEdit149.Checked = False
        CheckEdit140.Checked = False
        CheckEdit148.Checked = False
        CheckEdit139.Checked = False
        CheckEdit150.Checked = False
        CheckEdit152.Checked = False
        CheckEdit151.Checked = False
        TextEdit114.ResetText()
        TextEdit116.ResetText()
        TextEdit117.ResetText()
        TextEdit118.ResetText()
        TextEdit119.ResetText()
        TextEdit120.ResetText()
        TextEdit122.ResetText()
        TextEdit128.ResetText()
        TextEdit127.ResetText()
        TextEdit129.ResetText()
        TextEdit124.ResetText()
        TextEdit126.ResetText()
        TextEdit123.ResetText()
        MemoEdit2.ResetText()
        MemoEdit3.ResetText()
        CheckEdit163.Checked = False
        CheckEdit162.Checked = False
        CheckEdit164.Checked = False
        CheckEdit166.Checked = False
        CheckEdit165.Checked = False
        grdPerawat.ResetText()
        TextEdit95.ResetText()

        deDATE.DateTime = Now
        'fn_LoadAsessmenRawatJalan(txtNoRegister.Text)
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RJ_23.GetData(txtNoRegister.Text)

            With ds

                chkAUTO.Checked = .SDIGITALRJ23_1
                chkALLO.Checked = .SDIGITALRJ23_2
                txtNAMAPENGKAJIAN.Text = .SDIGITALRJ23_3
                txtDIGITALRJ_2.Text = .SDIGITALRJ23_4
                TextEdit1.Text = .SDIGITALRJ23_5
                TextEdit2.Text = .SDIGITALRJ23_6
                TextEdit3.Text = .SDIGITALRJ23_7
                TextEdit4.Text = .SDIGITALRJ23_8
                TextEdit5.Text = .SDIGITALRJ23_9
                TextEdit10.Text = .SDIGITALRJ23_10
                CheckEdit1.Checked = .SDIGITALRJ23_11
                CheckEdit2.Checked = .SDIGITALRJ23_12
                CheckEdit3.Checked = .SDIGITALRJ23_13
                CheckEdit4.Checked = .SDIGITALRJ23_14
                TextEdit7.Text = .SDIGITALRJ23_15
                MemoEdit1.Text = .SDIGITALRJ23_16
                TextEdit6.Text = .SDIGITALRJ23_17
                CheckEdit6.Checked = .SDIGITALRJ23_18
                CheckEdit5.Checked = .SDIGITALRJ23_19
                CheckEdit8.Checked = .SDIGITALRJ23_20
                CheckEdit7.Checked = .SDIGITALRJ23_21
                TextEdit8.Text = .SDIGITALRJ23_22
                TextEdit9.Text = .SDIGITALRJ23_23
                TextEdit11.Text = .SDIGITALRJ23_24
                TextEdit12.Text = .SDIGITALRJ23_25
                CheckEdit9.Checked = .SDIGITALRJ23_26
                CheckEdit10.Checked = .SDIGITALRJ23_27
                CheckEdit13.Checked = .SDIGITALRJ23_28
                CheckEdit12.Checked = .SDIGITALRJ23_29
                CheckEdit11.Checked = .SDIGITALRJ23_30
                CheckEdit18.Checked = .SDIGITALRJ23_31
                CheckEdit17.Checked = .SDIGITALRJ23_32
                CheckEdit16.Checked = .SDIGITALRJ23_33
                CheckEdit15.Checked = .SDIGITALRJ23_34
                CheckEdit14.Checked = .SDIGITALRJ23_35
                CheckEdit19.Checked = .SDIGITALRJ23_36
                CheckEdit25.Checked = .SDIGITALRJ23_37
                CheckEdit24.Checked = .SDIGITALRJ23_38
                CheckEdit23.Checked = .SDIGITALRJ23_39
                CheckEdit22.Checked = .SDIGITALRJ23_40
                CheckEdit21.Checked = .SDIGITALRJ23_41
                CheckEdit20.Checked = .SDIGITALRJ23_42
                CheckEdit31.Checked = .SDIGITALRJ23_43
                CheckEdit30.Checked = .SDIGITALRJ23_44
                CheckEdit29.Checked = .SDIGITALRJ23_45
                CheckEdit28.Checked = .SDIGITALRJ23_46
                CheckEdit27.Checked = .SDIGITALRJ23_47
                CheckEdit26.Checked = .SDIGITALRJ23_48
                CheckEdit37.Checked = .SDIGITALRJ23_49
                CheckEdit36.Checked = .SDIGITALRJ23_50
                CheckEdit35.Checked = .SDIGITALRJ23_51
                CheckEdit34.Checked = .SDIGITALRJ23_52
                CheckEdit33.Checked = .SDIGITALRJ23_53
                CheckEdit32.Checked = .SDIGITALRJ23_54
                CheckEdit50.Checked = .SDIGITALRJ23_55
                CheckEdit51.Checked = .SDIGITALRJ23_56
                CheckEdit52.Checked = .SDIGITALRJ23_57
                CheckEdit53.Checked = .SDIGITALRJ23_58
                CheckEdit54.Checked = .SDIGITALRJ23_59
                TextEdit13.Text  = .SDIGITALRJ23_60
                TextEdit14.Text = .SDIGITALRJ23_61
                DateEdit1.Text = .SDIGITALRJ23_62
                TextEdit15.Text = .SDIGITALRJ23_63
                TextEdit16.Text = .SDIGITALRJ23_64
                TextEdit17.Text = .SDIGITALRJ23_65
                TextEdit18.Text = .SDIGITALRJ23_66
                TextEdit19.Text = .SDIGITALRJ23_67
                TextEdit20.Text = .SDIGITALRJ23_68
                TextEdit21.Text = .SDIGITALRJ23_69
                TextEdit22.Text = .SDIGITALRJ23_70
                TextEdit23.Text = .SDIGITALRJ23_71
                TextEdit24.Text = .SDIGITALRJ23_72
                DateEdit2.Text = .SDIGITALRJ23_73
                TextEdit26.Text = .SDIGITALRJ23_74
                TextEdit27.Text = .SDIGITALRJ23_75
                TextEdit28.Text = .SDIGITALRJ23_76
                TextEdit29.Text = .SDIGITALRJ23_77
                TextEdit30.Text = .SDIGITALRJ23_78
                TextEdit31.Text = .SDIGITALRJ23_79
                TextEdit32.Text = .SDIGITALRJ23_80
                TextEdit33.Text = .SDIGITALRJ23_81
                TextEdit25.Text = .SDIGITALRJ23_82
                TextEdit34.Text = .SDIGITALRJ23_83
                DateEdit3.Text = .SDIGITALRJ23_84
                TextEdit35.Text = .SDIGITALRJ23_85
                TextEdit36.Text = .SDIGITALRJ23_86
                TextEdit37.Text = .SDIGITALRJ23_87
                TextEdit38.Text = .SDIGITALRJ23_88
                TextEdit39.Text = .SDIGITALRJ23_89
                TextEdit40.Text = .SDIGITALRJ23_90
                TextEdit41.Text = .SDIGITALRJ23_91
                TextEdit42.Text = .SDIGITALRJ23_92
                TextEdit43.Text = .SDIGITALRJ23_93
                TextEdit44.Text = .SDIGITALRJ23_94
                DateEdit4.Text = .SDIGITALRJ23_95
                TextEdit45.Text = .SDIGITALRJ23_96
                TextEdit46.Text = .SDIGITALRJ23_97
                TextEdit47.Text = .SDIGITALRJ23_98
                TextEdit48.Text = .SDIGITALRJ23_99
                TextEdit49.Text = .SDIGITALRJ23_100
                TextEdit50.Text = .SDIGITALRJ23_101
                TextEdit51.Text = .SDIGITALRJ23_102
                TextEdit52.Text = .SDIGITALRJ23_103
                TextEdit53.Text = .SDIGITALRJ23_104
                TextEdit54.Text = .SDIGITALRJ23_105
                DateEdit5.Text = .SDIGITALRJ23_106
                TextEdit55.Text = .SDIGITALRJ23_107
                TextEdit56.Text = .SDIGITALRJ23_108
                TextEdit57.Text = .SDIGITALRJ23_109
                TextEdit58.Text = .SDIGITALRJ23_110
                TextEdit59.Text = .SDIGITALRJ23_111
                TextEdit60.Text = .SDIGITALRJ23_112
                TextEdit61.Text = .SDIGITALRJ23_113
                TextEdit62.Text = .SDIGITALRJ23_114
                TextEdit63.Text = .SDIGITALRJ23_115
                TextEdit64.Text = .SDIGITALRJ23_116
                DateEdit6.Text = .SDIGITALRJ23_117
                TextEdit65.Text = .SDIGITALRJ23_118
                TextEdit66.Text = .SDIGITALRJ23_119
                TextEdit67.Text = .SDIGITALRJ23_120
                TextEdit68.Text = .SDIGITALRJ23_121
                TextEdit69.Text = .SDIGITALRJ23_122
                TextEdit70.Text = .SDIGITALRJ23_123
                TextEdit71.Text = .SDIGITALRJ23_124
                TextEdit72.Text = .SDIGITALRJ23_125
                TextEdit73.Text = .SDIGITALRJ23_126
                TextEdit74.Text = .SDIGITALRJ23_127
                DateEdit7.Text = .SDIGITALRJ23_128
                TextEdit75.Text = .SDIGITALRJ23_129
                TextEdit76.Text = .SDIGITALRJ23_130
                TextEdit77.Text = .SDIGITALRJ23_131
                TextEdit78.Text = .SDIGITALRJ23_132
                TextEdit78.Text = .SDIGITALRJ23_133
                TextEdit80.Text = .SDIGITALRJ23_134
                TextEdit81.Text = .SDIGITALRJ23_135
                TextEdit82.Text = .SDIGITALRJ23_136
                TextEdit83.Text = .SDIGITALRJ23_137
                chkSKRINING_GIZI_01_1.Checked = .SDIGITALRJ23_138
                chkSKRINING_GIZI_01_2.Checked = .SDIGITALRJ23_139
                chkSKRINING_GIZI_02_1.Checked = .SDIGITALRJ23_140
                chkSKRINING_GIZI_02_2.Checked = .SDIGITALRJ23_141
                chkSKRINING_GIZI_03_1.Checked = .SDIGITALRJ23_142
                chkSKRINING_GIZI_03_2.Checked = .SDIGITALRJ23_143

                chkSKRINING_RESIKO_JATUH_01_1.Checked = .SDIGITALRJ23_144
                chkSKRINING_RESIKO_JATUH_01_2.Checked = .SDIGITALRJ23_145
                chkSKRINING_RESIKO_JATUH_02_1.Checked = .SDIGITALRJ23_146
                chkSKRINING_RESIKO_JATUH_02_2.Checked = .SDIGITALRJ23_147

                chkRESIKO_JATUH_01.Checked = .RESIKO_JATUH_01
                chkRESIKO_JATUH_02.Checked = .RESIKO_JATUH_02
                chkRESIKO_JATUH_03.Checked = .RESIKO_JATUH_03

                chkTINDAKAN_01_YA.Checked = .TINDAKAN_01_YA
                chkTINDAKAN_02_YA.Checked = .TINDAKAN_02_YA
                chkTINDAKAN_03_YA.Checked = .TINDAKAN_03_YA
                chkTINDAKAN_04_YA.Checked = .TINDAKAN_04_YA
                chkTINDAKAN_01_TIDAK.Checked = .TINDAKAN_01_TIDAK
                chkTINDAKAN_02_TIDAK.Checked = .TINDAKAN_02_TIDAK
                chkTINDAKAN_03_TIDAK.Checked = .TINDAKAN_03_TIDAK
                chkTINDAKAN_04_TIDAK.Checked = .TINDAKAN_04_TIDAK

                grdPerawat1.EditValue = .KDSTAFFPERAWAT1
                grdPerawat2.EditValue = .KDSTAFFPERAWAT2
                grdPerawat3.EditValue = .KDSTAFFPERAWAT3
                grdPerawat4.EditValue = .KDSTAFFPERAWAT4

                txtPASIENKELUARGA1.Text = .PASIENKELUARGA1
                txtPASIENKELUARGA2.Text = .PASIENKELUARGA2
                txtPASIENKELUARGA3.Text = .PASIENKELUARGA3
                txtPASIENKELUARGA4.Text = .PASIENKELUARGA4

                CheckEdit39.Checked = .SDIGITALRJ23_148
                CheckEdit43.Checked = .SDIGITALRJ23_149
                CheckEdit48.Checked = .SDIGITALRJ23_150
                CheckEdit49.Checked = .SDIGITALRJ23_151
                CheckEdit44.Checked = .SDIGITALRJ23_152
                CheckEdit40.Checked = .SDIGITALRJ23_153
                CheckEdit41.Checked = .SDIGITALRJ23_154
                CheckEdit46.Checked = .SDIGITALRJ23_155
                CheckEdit47.Checked = .SDIGITALRJ23_156
                CheckEdit45.Checked = .SDIGITALRJ23_157
                CheckEdit38.Checked = .SDIGITALRJ23_158
                txtDIGITALRJ_79.Checked = .SDIGITALRJ23_159
                txtDIGITALRJ_81.Text = .SDIGITALRJ23_160
                txtDIGITALRJ_83.Text = .SDIGITALRJ23_161
                txtDIGITALRJ_85.Text = .SDIGITALRJ23_162
                txtDIGITALRJ_80.Checked = .SDIGITALRJ23_163
                txtDIGITALRJ_82.Text = .SDIGITALRJ23_164
                txtDIGITALRJ_84.Text = .SDIGITALRJ23_165
                txtDIGITALRJ_86.Text = .SDIGITALRJ23_166
                CheckEdit55.Checked = .SDIGITALRJ23_167
                CheckEdit56.Checked = .SDIGITALRJ23_168
                CheckEdit57.Checked = .SDIGITALRJ23_169
                CheckEdit58.Checked = .SDIGITALRJ23_170
                TextEdit84.Text = .SDIGITALRJ23_171
                CheckEdit61.Checked = .SDIGITALRJ23_172
                CheckEdit59.Checked = .SDIGITALRJ23_173
                TextEdit85.Text = .SDIGITALRJ23_174
                CheckEdit62.Checked = .SDIGITALRJ23_175
                CheckEdit60.Checked = .SDIGITALRJ23_176
                TextEdit86.Text = .SDIGITALRJ23_177
                CheckEdit66.Checked = .SDIGITALRJ23_178
                CheckEdit65.Checked = .SDIGITALRJ23_179
                CheckEdit63.Checked = .SDIGITALRJ23_180
                TextEdit87.Text = .SDIGITALRJ23_181
                CheckEdit64.Checked = .SDIGITALRJ23_182
                TextEdit88.Text = .SDIGITALRJ23_183
                CheckEdit68.Checked = .SDIGITALRJ23_184
                CheckEdit67.Checked = .SDIGITALRJ23_185
                CheckEdit70.Checked = .SDIGITALRJ23_186
                CheckEdit69.Checked = .SDIGITALRJ23_187
                CheckEdit71.Checked = .SDIGITALRJ23_188
                CheckEdit72.Checked = .SDIGITALRJ23_189
                CheckEdit73.Checked = .SDIGITALRJ23_190
                CheckEdit74.Checked = .SDIGITALRJ23_191
                CheckEdit75.Checked = .SDIGITALRJ23_192
                CheckEdit76.Checked = .SDIGITALRJ23_193
                CheckEdit77.Checked = .SDIGITALRJ23_194
                CheckEdit78.Checked = .SDIGITALRJ23_195
                CheckEdit79.Checked = .SDIGITALRJ23_196
                CheckEdit80.Checked = .SDIGITALRJ23_197
                CheckEdit81.Checked = .SDIGITALRJ23_198
                CheckEdit82.Checked = .SDIGITALRJ23_199
                CheckEdit83.Checked = .SDIGITALRJ23_200
                CheckEdit84.Checked = .SDIGITALRJ23_201
                CheckEdit85.Checked = .SDIGITALRJ23_202
                CheckEdit86.Checked = .SDIGITALRJ23_203
                CheckEdit87.Checked = .SDIGITALRJ23_204
                CheckEdit88.Checked = .SDIGITALRJ23_205
                TextEdit89.Text = .SDIGITALRJ23_206
                CheckEdit89.Checked = .SDIGITALRJ23_207
                CheckEdit90.Checked = .SDIGITALRJ23_208
                CheckEdit91.Checked = .SDIGITALRJ23_209
                CheckEdit92.Checked = .SDIGITALRJ23_210
                TextEdit90.Text = .SDIGITALRJ23_211
                CheckEdit93.Checked = .SDIGITALRJ23_212
                TextEdit91.Text = .SDIGITALRJ23_213
                CheckEdit94.Checked = .SDIGITALRJ23_214
                TextEdit98.Text = .SDIGITALRJ23_215
                CheckEdit130.Checked = .SDIGITALRJ23_216
                CheckEdit95.Checked = .SDIGITALRJ23_217
                CheckEdit96.Checked = .SDIGITALRJ23_218
                CheckEdit97.Checked = .SDIGITALRJ23_219
                TextEdit92.Text = .SDIGITALRJ23_220
                TextEdit93.Text = .SDIGITALRJ23_221
                TextEdit94.Text = .SDIGITALRJ23_222
                TextEdit96.Text = .SDIGITALRJ23_223
                CheckEdit101.Checked = .SDIGITALRJ23_224
                CheckEdit100.Checked = .SDIGITALRJ23_225
                TextEdit97.Text = .SDIGITALRJ23_226
                CheckEdit103.Checked = .SDIGITALRJ23_227
                CheckEdit102.Checked = .SDIGITALRJ23_228
                CheckEdit104.Checked = .SDIGITALRJ23_229
                CheckEdit105.Checked = .SDIGITALRJ23_230
                TextEdit99.Text = .SDIGITALRJ23_231
                CheckEdit108.Checked = .SDIGITALRJ23_232
                CheckEdit107.Checked = .SDIGITALRJ23_233
                CheckEdit112.Checked = .SDIGITALRJ23_234
                CheckEdit111.Checked = .SDIGITALRJ23_235
                CheckEdit114.Checked = .SDIGITALRJ23_236
                CheckEdit113.Checked = .SDIGITALRJ23_237
                TextEdit100.Text = .SDIGITALRJ23_238
                CheckEdit116.Checked = .SDIGITALRJ23_239
                CheckEdit115.Checked = .SDIGITALRJ23_240
                CheckEdit123.Checked = .SDIGITALRJ23_241
                CheckEdit122.Checked = .SDIGITALRJ23_242
                CheckEdit125.Checked = .SDIGITALRJ23_243
                CheckEdit124.Checked = .SDIGITALRJ23_244
                CheckEdit127.Checked = .SDIGITALRJ23_245
                CheckEdit126.Checked = .SDIGITALRJ23_246
                CheckEdit129.Checked = .SDIGITALRJ23_247
                CheckEdit128.Checked = .SDIGITALRJ23_248
                CheckEdit133.Checked = .SDIGITALRJ23_249
                CheckEdit132.Checked = .SDIGITALRJ23_250
                CheckEdit99.Checked = .SDIGITALRJ23_251
                CheckEdit98.Checked = .SDIGITALRJ23_252
                CheckEdit109.Checked = .SDIGITALRJ23_253
                CheckEdit106.Checked = .SDIGITALRJ23_254
                CheckEdit117.Checked = .SDIGITALRJ23_255
                CheckEdit110.Checked = .SDIGITALRJ23_256
                CheckEdit118.Checked = .SDIGITALRJ23_257
                TextEdit101.Text = .SDIGITALRJ23_258
                CheckEdit120.Checked = .SDIGITALRJ23_259
                CheckEdit119.Checked = .SDIGITALRJ23_260
                CheckEdit131.Checked = .SDIGITALRJ23_261
                CheckEdit121.Checked = .SDIGITALRJ23_262
                TextEdit102.Text = .SDIGITALRJ23_263
                TextEdit103.Text = .SDIGITALRJ23_264
                TextEdit104.Text = .SDIGITALRJ23_265
                TextEdit105.Text = .SDIGITALRJ23_266
                TextEdit106.Text = .SDIGITALRJ23_267
                TextEdit107.Text = .SDIGITALRJ23_268
                TextEdit108.Text = .SDIGITALRJ23_269
                CheckEdit135.Checked = .SDIGITALRJ23_270
                CheckEdit134.Checked = .SDIGITALRJ23_271
                TextEdit109.Text = .SDIGITALRJ23_272
                TextEdit110.Text = .SDIGITALRJ23_273
                TextEdit111.Text = .SDIGITALRJ23_274
                CheckEdit137.Checked = .SDIGITALRJ23_275
                CheckEdit136.Checked = .SDIGITALRJ23_276
                TextEdit112.Text = .SDIGITALRJ23_277
                CheckEdit141.Checked = .SDIGITALRJ23_278
                CheckEdit142.Checked = .SDIGITALRJ23_279
                CheckEdit144.Checked = .SDIGITALRJ23_280
                CheckEdit143.Checked = .SDIGITALRJ23_281
                CheckEdit146.Checked = .SDIGITALRJ23_282
                CheckEdit145.Checked = .SDIGITALRJ23_283
                TextEdit113.Text = .SDIGITALRJ23_284
                TextEdit115.Text = .SDIGITALRJ23_285
                CheckEdit147.Checked = .SDIGITALRJ23_286
                CheckEdit138.Checked = .SDIGITALRJ23_287
                CheckEdit149.Checked = .SDIGITALRJ23_288
                CheckEdit140.Checked = .SDIGITALRJ23_289
                CheckEdit148.Checked = .SDIGITALRJ23_290
                CheckEdit139.Checked = .SDIGITALRJ23_291
                CheckEdit150.Checked = .SDIGITALRJ23_292
                CheckEdit152.Checked = .SDIGITALRJ23_293
                CheckEdit151.Checked = .SDIGITALRJ23_294
                TextEdit114.Text = .SDIGITALRJ23_295
                TextEdit116.Text = .SDIGITALRJ23_296
                TextEdit117.Text = .SDIGITALRJ23_297
                TextEdit118.Text = .SDIGITALRJ23_298
                TextEdit119.Text = .SDIGITALRJ23_299
                TextEdit120.Text = .SDIGITALRJ23_300
                TextEdit122.Text = .SDIGITALRJ23_301
                TextEdit128.Text = .SDIGITALRJ23_302
                TextEdit127.Text = .SDIGITALRJ23_303
                TextEdit129.Text = .SDIGITALRJ23_304
                TextEdit124.Text = .SDIGITALRJ23_305
                TextEdit126.Text = .SDIGITALRJ23_306
                TextEdit123.Text = .SDIGITALRJ23_307
                MemoEdit2.Text = .SDIGITALRJ23_308
                MemoEdit3.Text = .SDIGITALRJ23_309
                CheckEdit163.Checked = .SDIGITALRJ23_310
                CheckEdit162.Checked = .SDIGITALRJ23_311
                CheckEdit164.Checked = .SDIGITALRJ23_312
                CheckEdit166.Checked = .SDIGITALRJ23_313
                CheckEdit165.Checked = .SDIGITALRJ23_314

                TextEdit95.Text = .SDIGITALRJ23_315

                grdPerawat.EditValue = .KDDOCTOR2

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

            If grdPerawat.Text = String.Empty Then
                MsgBox("Dibutuhkan Bidan Pemeriksa", MsgBoxStyle.Exclamation, Me.Text)
                grdPerawat.Focus()
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
            Dim ds = oS_DIGITAL_RJ_23.GetStructureHeader
            With ds
                .KDKUNJUNGAN = txtNoRegister.Text
                Try
                    .DATECREATED = oS_DIGITAL_RJ_23.GetData(txtNoRegister.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .KDDOCTOR = KDDOCTOR
                .NMDOCTOR = NMDOCTOR

                .SDIGITALRJ23_1 = chkAUTO.Checked
                .SDIGITALRJ23_2 = chkALLO.Checked
                .SDIGITALRJ23_3 = txtNAMAPENGKAJIAN.Text 
                .SDIGITALRJ23_4 = txtDIGITALRJ_2.Text 
                .SDIGITALRJ23_5 = TextEdit1.Text 
                .SDIGITALRJ23_6 = TextEdit2.Text 
                .SDIGITALRJ23_7 = TextEdit3.Text 
                .SDIGITALRJ23_8 = TextEdit4.Text 
                .SDIGITALRJ23_9 = TextEdit5.Text 
                .SDIGITALRJ23_10 = TextEdit10.Text 
                .SDIGITALRJ23_11 = CheckEdit1.Checked
                .SDIGITALRJ23_12 = CheckEdit2.Checked
                .SDIGITALRJ23_13 = CheckEdit3.Checked
                .SDIGITALRJ23_14 = CheckEdit4.Checked
                .SDIGITALRJ23_15 = TextEdit7.Text 
                .SDIGITALRJ23_16 = MemoEdit1.Text 
                .SDIGITALRJ23_17 = TextEdit6.Text 
                .SDIGITALRJ23_18 = CheckEdit6.Checked
                .SDIGITALRJ23_19 = CheckEdit5.Checked
                .SDIGITALRJ23_20 = CheckEdit8.Checked
                .SDIGITALRJ23_21 = CheckEdit7.Checked
                .SDIGITALRJ23_22 = TextEdit8.Text 
                .SDIGITALRJ23_23 = TextEdit9.Text 
                .SDIGITALRJ23_24 = TextEdit11.Text 
                .SDIGITALRJ23_25 = TextEdit12.Text 
                .SDIGITALRJ23_26 = CheckEdit9.Checked
                .SDIGITALRJ23_27 = CheckEdit10.Checked
                .SDIGITALRJ23_28 = CheckEdit13.Checked
                .SDIGITALRJ23_29 = CheckEdit12.Checked
                .SDIGITALRJ23_30 = CheckEdit11.Checked
                .SDIGITALRJ23_31 = CheckEdit18.Checked
                .SDIGITALRJ23_32 = CheckEdit17.Checked
                .SDIGITALRJ23_33 = CheckEdit16.Checked
                .SDIGITALRJ23_34 = CheckEdit15.Checked
                .SDIGITALRJ23_35 = CheckEdit14.Checked
                .SDIGITALRJ23_36 = CheckEdit19.Checked
                .SDIGITALRJ23_37 = CheckEdit25.Checked
                .SDIGITALRJ23_38 = CheckEdit24.Checked
                .SDIGITALRJ23_39 = CheckEdit23.Checked
                .SDIGITALRJ23_40 = CheckEdit22.Checked
                .SDIGITALRJ23_41 = CheckEdit21.Checked
                .SDIGITALRJ23_42 = CheckEdit20.Checked
                .SDIGITALRJ23_43 = CheckEdit31.Checked
                .SDIGITALRJ23_44 = CheckEdit30.Checked
                .SDIGITALRJ23_45 = CheckEdit29.Checked
                .SDIGITALRJ23_46 = CheckEdit28.Checked
                .SDIGITALRJ23_47 = CheckEdit27.Checked
                .SDIGITALRJ23_48 = CheckEdit26.Checked
                .SDIGITALRJ23_49 = CheckEdit37.Checked
                .SDIGITALRJ23_50 = CheckEdit36.Checked
                .SDIGITALRJ23_51 = CheckEdit35.Checked
                .SDIGITALRJ23_52 = CheckEdit34.Checked
                .SDIGITALRJ23_53 = CheckEdit33.Checked
                .SDIGITALRJ23_54 = CheckEdit32.Checked
                .SDIGITALRJ23_55 = CheckEdit50.Checked
                .SDIGITALRJ23_56 = CheckEdit51.Checked
                .SDIGITALRJ23_57 = CheckEdit52.Checked
                .SDIGITALRJ23_58 = CheckEdit53.Checked
                .SDIGITALRJ23_59 = CheckEdit54.Checked
                .SDIGITALRJ23_60 = TextEdit13.Text
                .SDIGITALRJ23_61 = TextEdit14.Text 
                .SDIGITALRJ23_62 = DateEdit1.Text 
                .SDIGITALRJ23_63 = TextEdit15.Text 
                .SDIGITALRJ23_64 = TextEdit16.Text 
                .SDIGITALRJ23_65 = TextEdit17.Text 
                .SDIGITALRJ23_66 = TextEdit18.Text 
                .SDIGITALRJ23_67 = TextEdit19.Text 
                .SDIGITALRJ23_68 = TextEdit20.Text 
                .SDIGITALRJ23_69 = TextEdit21.Text 
                .SDIGITALRJ23_70 = TextEdit22.Text 
                .SDIGITALRJ23_71 = TextEdit23.Text 
                .SDIGITALRJ23_72 = TextEdit24.Text 
                .SDIGITALRJ23_73 = DateEdit2.Text 
                .SDIGITALRJ23_74 = TextEdit26.Text 
                .SDIGITALRJ23_75 = TextEdit27.Text 
                .SDIGITALRJ23_76 = TextEdit28.Text 
                .SDIGITALRJ23_77 = TextEdit29.Text 
                .SDIGITALRJ23_78 = TextEdit30.Text 
                .SDIGITALRJ23_79 = TextEdit31.Text 
                .SDIGITALRJ23_80 = TextEdit32.Text 
                .SDIGITALRJ23_81 = TextEdit33.Text 
                .SDIGITALRJ23_82 = TextEdit25.Text 
                .SDIGITALRJ23_83 = TextEdit34.Text 
                .SDIGITALRJ23_84 = DateEdit3.Text 
                .SDIGITALRJ23_85 = TextEdit35.Text 
                .SDIGITALRJ23_86 = TextEdit36.Text 
                .SDIGITALRJ23_87 = TextEdit37.Text 
                .SDIGITALRJ23_88 = TextEdit38.Text 
                .SDIGITALRJ23_89 = TextEdit39.Text 
                .SDIGITALRJ23_90 = TextEdit40.Text 
                .SDIGITALRJ23_91 = TextEdit41.Text 
                .SDIGITALRJ23_92 = TextEdit42.Text 
                .SDIGITALRJ23_93 = TextEdit43.Text 
                .SDIGITALRJ23_94 = TextEdit44.Text 
                .SDIGITALRJ23_95 = DateEdit4.Text 
                .SDIGITALRJ23_96 = TextEdit45.Text 
                .SDIGITALRJ23_97 = TextEdit46.Text 
                .SDIGITALRJ23_98 = TextEdit47.Text 
                .SDIGITALRJ23_99 = TextEdit48.Text 
                .SDIGITALRJ23_100 = TextEdit49.Text 
                .SDIGITALRJ23_101 = TextEdit50.Text 
                .SDIGITALRJ23_102 = TextEdit51.Text 
                .SDIGITALRJ23_103 = TextEdit52.Text 
                .SDIGITALRJ23_104 = TextEdit53.Text 
                .SDIGITALRJ23_105 = TextEdit54.Text 
                .SDIGITALRJ23_106 = DateEdit5.Text 
                .SDIGITALRJ23_107 = TextEdit55.Text 
                .SDIGITALRJ23_108 = TextEdit56.Text 
                .SDIGITALRJ23_109 = TextEdit57.Text 
                .SDIGITALRJ23_110 = TextEdit58.Text 
                .SDIGITALRJ23_111 = TextEdit59.Text 
                .SDIGITALRJ23_112 = TextEdit60.Text 
                .SDIGITALRJ23_113 = TextEdit61.Text 
                .SDIGITALRJ23_114 = TextEdit62.Text 
                .SDIGITALRJ23_115 = TextEdit63.Text 
                .SDIGITALRJ23_116 = TextEdit64.Text 
                .SDIGITALRJ23_117 = DateEdit6.Text 
                .SDIGITALRJ23_118 = TextEdit65.Text 
                .SDIGITALRJ23_119 = TextEdit66.Text 
                .SDIGITALRJ23_120 = TextEdit67.Text 
                .SDIGITALRJ23_121 = TextEdit68.Text 
                .SDIGITALRJ23_122 = TextEdit69.Text 
                .SDIGITALRJ23_123 = TextEdit70.Text 
                .SDIGITALRJ23_124 = TextEdit71.Text 
                .SDIGITALRJ23_125 = TextEdit72.Text 
                .SDIGITALRJ23_126 = TextEdit73.Text 
                .SDIGITALRJ23_127 = TextEdit74.Text 
                .SDIGITALRJ23_128 = DateEdit7.Text 
                .SDIGITALRJ23_129 = TextEdit75.Text 
                .SDIGITALRJ23_130 = TextEdit76.Text 
                .SDIGITALRJ23_131 = TextEdit77.Text 
                .SDIGITALRJ23_132 = TextEdit78.Text 
                .SDIGITALRJ23_133 = TextEdit78.Text
                .SDIGITALRJ23_134 = TextEdit80.text
                .SDIGITALRJ23_135 = TextEdit81.Text 
                .SDIGITALRJ23_136 = TextEdit82.Text 
                .SDIGITALRJ23_137 = TextEdit83.Text 
                .SDIGITALRJ23_138 = chkSKRINING_GIZI_01_1.Checked
                .SDIGITALRJ23_139 = chkSKRINING_GIZI_01_2.Checked
                .SDIGITALRJ23_140 = chkSKRINING_GIZI_02_1.Checked
                .SDIGITALRJ23_141 = chkSKRINING_GIZI_02_2.Checked
                .SDIGITALRJ23_142 = chkSKRINING_GIZI_03_1.Checked
                .SDIGITALRJ23_143 = chkSKRINING_GIZI_03_2.Checked
                .SDIGITALRJ23_144 = chkSKRINING_RESIKO_JATUH_01_1.Checked
                .SDIGITALRJ23_145 = chkSKRINING_RESIKO_JATUH_01_2.Checked
                .SDIGITALRJ23_146 = chkSKRINING_RESIKO_JATUH_02_1.Checked
                .SDIGITALRJ23_147 = chkSKRINING_RESIKO_JATUH_02_2.Checked

                .RESIKO_JATUH_01 = chkRESIKO_JATUH_01.Checked
                .RESIKO_JATUH_02 = chkRESIKO_JATUH_02.Checked
                .RESIKO_JATUH_03 = chkRESIKO_JATUH_03.Checked

                .TINDAKAN_01_YA = chkTINDAKAN_01_YA.Checked
                .TINDAKAN_02_YA = chkTINDAKAN_02_YA.Checked
                .TINDAKAN_03_YA = chkTINDAKAN_03_YA.Checked
                .TINDAKAN_04_YA = chkTINDAKAN_04_YA.Checked
                .TINDAKAN_01_TIDAK = chkTINDAKAN_01_TIDAK.Checked
                .TINDAKAN_02_TIDAK = chkTINDAKAN_02_TIDAK.Checked
                .TINDAKAN_03_TIDAK = chkTINDAKAN_03_TIDAK.Checked
                .TINDAKAN_04_TIDAK = chkTINDAKAN_04_TIDAK.Checked

                .KDSTAFFPERAWAT1 = grdPerawat1.EditValue
                .NAMAPERAWAT1 = grdPerawat1.Text
                .KDSTAFFPERAWAT2 = grdPerawat2.EditValue
                .NAMAPERAWAT2 = grdPerawat2.Text
                .KDSTAFFPERAWAT3 = grdPerawat3.EditValue
                .NAMAPERAWAT3 = grdPerawat3.Text
                .KDSTAFFPERAWAT4 = grdPerawat4.EditValue
                .NAMAPERAWAT4 = grdPerawat4.Text

                .PASIENKELUARGA1 = txtPASIENKELUARGA1.Text
                .PASIENKELUARGA2 = txtPASIENKELUARGA2.Text
                .PASIENKELUARGA3 = txtPASIENKELUARGA3.Text
                .PASIENKELUARGA4 = txtPASIENKELUARGA4.Text

                .SDIGITALRJ23_148 = CheckEdit39.Checked
                .SDIGITALRJ23_149 = CheckEdit43.Checked
                .SDIGITALRJ23_150 = CheckEdit48.Checked
                .SDIGITALRJ23_151 = CheckEdit49.Checked
                .SDIGITALRJ23_152 = CheckEdit44.Checked
                .SDIGITALRJ23_153 = CheckEdit40.Checked
                .SDIGITALRJ23_154 = CheckEdit41.Checked
                .SDIGITALRJ23_155 = CheckEdit46.Checked
                .SDIGITALRJ23_156 = CheckEdit47.Checked
                .SDIGITALRJ23_157 = CheckEdit45.Checked
                .SDIGITALRJ23_158 = CheckEdit38.Checked
                .SDIGITALRJ23_159 = txtDIGITALRJ_79.Checked
                .SDIGITALRJ23_160 = txtDIGITALRJ_81.Text 
                .SDIGITALRJ23_161 = txtDIGITALRJ_83.Text 
                .SDIGITALRJ23_162 = txtDIGITALRJ_85.Text 
                .SDIGITALRJ23_163 = txtDIGITALRJ_80.Checked
                .SDIGITALRJ23_164 = txtDIGITALRJ_82.Text 
                .SDIGITALRJ23_165 = txtDIGITALRJ_84.Text 
                .SDIGITALRJ23_166 = txtDIGITALRJ_86.Text 
                .SDIGITALRJ23_167 = CheckEdit55.Checked
                .SDIGITALRJ23_168 = CheckEdit56.Checked
                .SDIGITALRJ23_169 = CheckEdit57.Checked
                .SDIGITALRJ23_170 = CheckEdit58.Checked
                .SDIGITALRJ23_171 = TextEdit84.Text 
                .SDIGITALRJ23_172 = CheckEdit61.Checked
                .SDIGITALRJ23_173 = CheckEdit59.Checked
                .SDIGITALRJ23_174 = TextEdit85.Text 
                .SDIGITALRJ23_175 = CheckEdit62.Checked
                .SDIGITALRJ23_176 = CheckEdit60.Checked
                .SDIGITALRJ23_177 = TextEdit86.Text 
                .SDIGITALRJ23_178 = CheckEdit66.Checked
                .SDIGITALRJ23_179 = CheckEdit65.Checked
                .SDIGITALRJ23_180 = CheckEdit63.Checked
                .SDIGITALRJ23_181 = TextEdit87.Text 
                .SDIGITALRJ23_182 = CheckEdit64.Checked
                .SDIGITALRJ23_183 = TextEdit88.Text 
                .SDIGITALRJ23_184 = CheckEdit68.Checked
                .SDIGITALRJ23_185 = CheckEdit67.Checked
                .SDIGITALRJ23_186 = CheckEdit70.Checked
                .SDIGITALRJ23_187 = CheckEdit69.Checked
                .SDIGITALRJ23_188 = CheckEdit71.Checked
                .SDIGITALRJ23_189 = CheckEdit72.Checked
                .SDIGITALRJ23_190 = CheckEdit73.Checked
                .SDIGITALRJ23_191 = CheckEdit74.Checked
                .SDIGITALRJ23_192 = CheckEdit75.Checked
                .SDIGITALRJ23_193 = CheckEdit76.Checked
                .SDIGITALRJ23_194 = CheckEdit77.Checked
                .SDIGITALRJ23_195 = CheckEdit78.Checked
                .SDIGITALRJ23_196 = CheckEdit79.Checked
                .SDIGITALRJ23_197 = CheckEdit80.Checked
                .SDIGITALRJ23_198 = CheckEdit81.Checked
                .SDIGITALRJ23_199 = CheckEdit82.Checked
                .SDIGITALRJ23_200 = CheckEdit83.Checked
                .SDIGITALRJ23_201 = CheckEdit84.Checked
                .SDIGITALRJ23_202 = CheckEdit85.Checked
                .SDIGITALRJ23_203 = CheckEdit86.Checked
                .SDIGITALRJ23_204 = CheckEdit87.Checked
                .SDIGITALRJ23_205 = CheckEdit88.Checked
                .SDIGITALRJ23_206 = TextEdit89.Text 
                .SDIGITALRJ23_207 = CheckEdit89.Checked
                .SDIGITALRJ23_208 = CheckEdit90.Checked
                .SDIGITALRJ23_209 = CheckEdit91.Checked
                .SDIGITALRJ23_210 = CheckEdit92.Checked
                .SDIGITALRJ23_211 = TextEdit90.Text 
                .SDIGITALRJ23_212 = CheckEdit93.Checked
                .SDIGITALRJ23_213 = TextEdit91.Text 
                .SDIGITALRJ23_214 = CheckEdit94.Checked
                .SDIGITALRJ23_215 = TextEdit98.Text 
                .SDIGITALRJ23_216 = CheckEdit130.Checked
                .SDIGITALRJ23_217 = CheckEdit95.Checked
                .SDIGITALRJ23_218 = CheckEdit96.Checked
                .SDIGITALRJ23_219 = CheckEdit97.Checked
                .SDIGITALRJ23_220 = TextEdit92.Text 
                .SDIGITALRJ23_221 = TextEdit93.Text 
                .SDIGITALRJ23_222 = TextEdit94.Text 
                .SDIGITALRJ23_223 = TextEdit96.Text 
                .SDIGITALRJ23_224 = CheckEdit101.Checked
                .SDIGITALRJ23_225 = CheckEdit100.Checked
                .SDIGITALRJ23_226 = TextEdit97.Text 
                .SDIGITALRJ23_227 = CheckEdit103.Checked
                .SDIGITALRJ23_228 = CheckEdit102.Checked
                .SDIGITALRJ23_229 = CheckEdit104.Checked
                .SDIGITALRJ23_230 = CheckEdit105.Checked
                .SDIGITALRJ23_231 = TextEdit99.Text 
                .SDIGITALRJ23_232 = CheckEdit108.Checked
                .SDIGITALRJ23_233 = CheckEdit107.Checked
                .SDIGITALRJ23_234 = CheckEdit112.Checked
                .SDIGITALRJ23_235 = CheckEdit111.Checked
                .SDIGITALRJ23_236 = CheckEdit114.Checked
                .SDIGITALRJ23_237 = CheckEdit113.Checked
                .SDIGITALRJ23_238 = TextEdit100.Text 
                .SDIGITALRJ23_239 = CheckEdit116.Checked
                .SDIGITALRJ23_240 = CheckEdit115.Checked
                .SDIGITALRJ23_241 = CheckEdit123.Checked
                .SDIGITALRJ23_242 = CheckEdit122.Checked
                .SDIGITALRJ23_243 = CheckEdit125.Checked
                .SDIGITALRJ23_244 = CheckEdit124.Checked
                .SDIGITALRJ23_245 = CheckEdit127.Checked
                .SDIGITALRJ23_246 = CheckEdit126.Checked
                .SDIGITALRJ23_247 = CheckEdit129.Checked
                .SDIGITALRJ23_248 = CheckEdit128.Checked
                .SDIGITALRJ23_249 = CheckEdit133.Checked
                .SDIGITALRJ23_250 = CheckEdit132.Checked
                .SDIGITALRJ23_251 = CheckEdit99.Checked
                .SDIGITALRJ23_252 = CheckEdit98.Checked
                .SDIGITALRJ23_253 = CheckEdit109.Checked
                .SDIGITALRJ23_254 = CheckEdit106.Checked
                .SDIGITALRJ23_255 = CheckEdit117.Checked
                .SDIGITALRJ23_256 = CheckEdit110.Checked
                .SDIGITALRJ23_257 = CheckEdit118.Checked
                .SDIGITALRJ23_258 = TextEdit101.Text 
                .SDIGITALRJ23_259 = CheckEdit120.Checked
                .SDIGITALRJ23_260 = CheckEdit119.Checked
                .SDIGITALRJ23_261 = CheckEdit131.Checked
                .SDIGITALRJ23_262 = CheckEdit121.Checked
                .SDIGITALRJ23_263 = TextEdit102.Text 
                .SDIGITALRJ23_264 = TextEdit103.Text 
                .SDIGITALRJ23_265 = TextEdit104.Text 
                .SDIGITALRJ23_266 = TextEdit105.Text 
                .SDIGITALRJ23_267 = TextEdit106.Text 
                .SDIGITALRJ23_268 = TextEdit107.Text 
                .SDIGITALRJ23_269 = TextEdit108.Text 
                .SDIGITALRJ23_270 = CheckEdit135.Checked
                .SDIGITALRJ23_271 = CheckEdit134.Checked
                .SDIGITALRJ23_272 = TextEdit109.Text 
                .SDIGITALRJ23_273 = TextEdit110.Text 
                .SDIGITALRJ23_274 = TextEdit111.Text 
                .SDIGITALRJ23_275 = CheckEdit137.Checked
                .SDIGITALRJ23_276 = CheckEdit136.Checked
                .SDIGITALRJ23_277 = TextEdit112.Text 
                .SDIGITALRJ23_278 = CheckEdit141.Checked
                .SDIGITALRJ23_279 = CheckEdit142.Checked
                .SDIGITALRJ23_280 = CheckEdit144.Checked
                .SDIGITALRJ23_281 = CheckEdit143.Checked
                .SDIGITALRJ23_282 = CheckEdit146.Checked
                .SDIGITALRJ23_283 = CheckEdit145.Checked
                .SDIGITALRJ23_284 = TextEdit113.Text 
                .SDIGITALRJ23_285 = TextEdit115.Text 
                .SDIGITALRJ23_286 = CheckEdit147.Checked
                .SDIGITALRJ23_287 = CheckEdit138.Checked
                .SDIGITALRJ23_288 = CheckEdit149.Checked
                .SDIGITALRJ23_289 = CheckEdit140.Checked
                .SDIGITALRJ23_290 = CheckEdit148.Checked
                .SDIGITALRJ23_291 = CheckEdit139.Checked
                .SDIGITALRJ23_292 = CheckEdit150.Checked
                .SDIGITALRJ23_293 = CheckEdit152.Checked
                .SDIGITALRJ23_294 = CheckEdit151.Checked
                .SDIGITALRJ23_295 = TextEdit114.Text 
                .SDIGITALRJ23_296 = TextEdit116.Text 
                .SDIGITALRJ23_297 = TextEdit117.Text 
                .SDIGITALRJ23_298 = TextEdit118.Text 
                .SDIGITALRJ23_299 = TextEdit119.Text 
                .SDIGITALRJ23_300 = TextEdit120.Text 
                .SDIGITALRJ23_301 = TextEdit122.Text 
                .SDIGITALRJ23_302 = TextEdit128.Text 
                .SDIGITALRJ23_303 = TextEdit127.Text 
                .SDIGITALRJ23_304 = TextEdit129.Text 
                .SDIGITALRJ23_305 = TextEdit124.Text 
                .SDIGITALRJ23_306 = TextEdit126.Text 
                .SDIGITALRJ23_307 = TextEdit123.Text 
                .SDIGITALRJ23_308 = MemoEdit2.Text 
                .SDIGITALRJ23_309 = MemoEdit3.Text 
                .SDIGITALRJ23_310 = CheckEdit163.Checked
                .SDIGITALRJ23_311 = CheckEdit162.Checked
                .SDIGITALRJ23_312 = CheckEdit164.Checked
                .SDIGITALRJ23_313 = CheckEdit166.Checked
                .SDIGITALRJ23_314 = CheckEdit165.Checked

                .SDIGITALRJ23_315 = TextEdit95.Text

                .KDDOCTOR2 = grdPerawat.EditValue
                .NMDOCTOR2 = grdPerawat.Text

                Try
                    .CETAK = oS_DIGITAL_RJ_23.GetData(txtNoRegister.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                Try
                    If sIsOtority = True Then
                        .KDUSER = oS_DIGITAL_RJ_23.GetData(txtNoRegister.Text).KDUSER
                        .KDUSER_SIGNATURE = oS_DIGITAL_RJ_23.GetData(txtNoRegister.Text).KDUSER_SIGNATURE
                    Else
                        .KDUSER = sUserID
                        .KDUSER_SIGNATURE = sUserSIGNATURE
                    End If
                Catch ex As Exception
                    .KDUSER = sUserID
                    .KDUSER_SIGNATURE = sUserSIGNATURE
                End Try
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_RJ_23.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_RJ_23.UpdateData(ds)
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
            Case Keys.F2
                If btnSaveNew.Enabled = True Then
                    btnSaveNew_Click()
                End If
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
    Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & txtNoRegister.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtNoRegister.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            'sStatusSave = "NEW"
            Me.Close()
        End If
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
    Private Sub fn_BIDAN()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = "Data Source=172.165.115.150;Initial Catalog=DUSTIRA_FARMASI;Persist Security Info=True;User ID=sa;Password=dust1r@@"
            oConn = New SqlConnection(sConn)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "M_STAFF A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "
            SQL &= "AND KELOMPOKIPK = 'NAKES'"
            SQL &= "ORDER BY NAME_DISPLAY ASC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "STAFF")

            grdPerawat.Properties.DataSource = ds.Tables("STAFF")
            grdPerawat.Properties.ValueMember = "KDSTAFF"
            grdPerawat.Properties.DisplayMember = "NAME_DISPLAY"

            grdPerawat1.Properties.DataSource = ds.Tables("STAFF")
            grdPerawat1.Properties.ValueMember = "KDSTAFF"
            grdPerawat1.Properties.DisplayMember = "NAME_DISPLAY"

            grdPerawat2.Properties.DataSource = ds.Tables("STAFF")
            grdPerawat2.Properties.ValueMember = "KDSTAFF"
            grdPerawat2.Properties.DisplayMember = "NAME_DISPLAY"

            grdPerawat3.Properties.DataSource = ds.Tables("STAFF")
            grdPerawat3.Properties.ValueMember = "KDSTAFF"
            grdPerawat3.Properties.DisplayMember = "NAME_DISPLAY"

            grdPerawat4.Properties.DataSource = ds.Tables("STAFF")
            grdPerawat4.Properties.ValueMember = "KDSTAFF"
            grdPerawat4.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub frmEMedrekRJ_23_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
	    If e.Delta > 0 Then
		    'up
		    fn_ScrollPage(True)
	    Else
		    'down
		    fn_ScrollPage(False)
	    End If
    End Sub

    Private Sub fn_ScrollPage(ByVal isUp As Boolean)
	    Dim myView As Point = Me.Panel3.AutoScrollPosition
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

	    Me.Panel3.AutoScrollPosition = myView
    End Sub
#End Region
End Class