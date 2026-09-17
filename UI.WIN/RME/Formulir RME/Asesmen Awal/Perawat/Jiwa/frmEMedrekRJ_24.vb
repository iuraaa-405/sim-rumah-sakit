Imports System.Data.SqlClient
Imports System.Linq
Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports DevExpress.XtraSplashScreen

Public Class frmEMedrekRJ_24
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oDigital As New Digital.clsDigital_RJ_24
    Private oDiagnosaPerawat As New Digital.clsDiagnosaPerawat
    Private down As Boolean = False
    Private sKODEDOKTER As String = String.Empty
    Private sNAMADOKTER As String = String.Empty
    Private sKDKUNJUNGAN As String = String.Empty
    Private sIsOtority As Boolean = False

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String)
        oFormMode = FormMode

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
            txtSuku.Text = dsPendaftaran.SUKU
            txtAgama.Text = dsPendaftaran.AGAMA
            txtPangkat.Text = dsPendaftaran.PANGKAT
            txtDiagnosaKeperawatan.Text = ""
            txtKesatuan.Text = dsPendaftaran.KESATUAN
            txtPendidikan.Text = dsPendaftaran.PENDIDIKAN
            txtNoRegister.Text = dsPendaftaran.KDKUNJUNGAN
            txtNoPasien.Text = dsPendaftaran.KDCUSTOMER
            txtTgl.Text = dsPendaftaran.DATE
            txtNoTelepon.Text = dsPendaftaran.NOMORTELEPON
            txtAlamat.Text = dsPendaftaran.ALAMAT
            sKODEDOKTER = dsPendaftaran.KDDOKTER
            sNAMADOKTER = dsPendaftaran.DOKTER
            sKDKUNJUNGAN = dsPendaftaran.KDKUNJUNGAN
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
            txtAlamat.ResetText()
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = EMedrekRJ_24.TITLE
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
        fn_LoadPerawat()
        fn_LoadKDITEMDIAGNOSAPERAWAT()
        fn_LoadHistoryAsesmen()

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
        txtKELUHAN_UTAMA.Properties.ReadOnly = Status
        txtKESADARAN_UMUM.Properties.ReadOnly = Status
        txtTANDA_VITAL_01.Properties.ReadOnly = Status
        txtTANDA_VITAL_02.Properties.ReadOnly = Status
        txtTANDA_VITAL_03.Properties.ReadOnly = Status
        txtTANDA_VITAL_04.Properties.ReadOnly = Status
        chkTANDA_VITAL_REGULER.Properties.ReadOnly = Status
        chkTANDA_VITAL_IREGULER.Properties.ReadOnly = Status
        txtTANDA_VITAL_05.Properties.ReadOnly = Status
        txtTANDA_VITAL_06.Properties.ReadOnly = Status
        txtTANDA_VITAL_07.Properties.ReadOnly = Status
        txtTANDA_VITAL_08.Properties.ReadOnly = Status
        TextEdit42.Properties.ReadOnly = Status
        CheckEdit2.Properties.ReadOnly = Status
        CheckEdit1.Properties.ReadOnly = Status
        TextEdit1.Properties.ReadOnly = Status
        CheckEdit4.Properties.ReadOnly = Status
        CheckEdit3.Properties.ReadOnly = Status
        CheckEdit5.Properties.ReadOnly = Status
        TextEdit2.Properties.ReadOnly = Status
        CheckEdit6.Properties.ReadOnly = Status
        CheckEdit7.Properties.ReadOnly = Status
        CheckEdit9.Properties.ReadOnly = Status
        CheckEdit8.Properties.ReadOnly = Status
        TextEdit3.Properties.ReadOnly = Status
        TextEdit4.Properties.ReadOnly = Status
        TextEdit5.Properties.ReadOnly = Status
        TextEdit6.Properties.ReadOnly = Status
        CheckEdit12.Properties.ReadOnly = Status
        TextEdit7.Properties.ReadOnly = Status
        CheckEdit11.Properties.ReadOnly = Status
        CheckEdit14.Properties.ReadOnly = Status
        CheckEdit10.Properties.ReadOnly = Status
        TextEdit8.Properties.ReadOnly = Status
        CheckEdit13.Properties.ReadOnly = Status
        TextEdit9.Properties.ReadOnly = Status
        CheckEdit15.Properties.ReadOnly = Status
        TextEdit10.Properties.ReadOnly = Status
        TextEdit11.Properties.ReadOnly = Status
        CheckEdit16.Properties.ReadOnly = Status
        CheckEdit17.Properties.ReadOnly = Status
        CheckEdit19.Properties.ReadOnly = Status
        CheckEdit18.Properties.ReadOnly = Status
        CheckEdit21.Properties.ReadOnly = Status
        CheckEdit20.Properties.ReadOnly = Status
        CheckEdit23.Properties.ReadOnly = Status
        CheckEdit22.Properties.ReadOnly = Status
        CheckEdit24.Properties.ReadOnly = Status
        TextEdit13.Properties.ReadOnly = Status
        TextEdit12.Properties.ReadOnly = Status
        CheckEdit26.Properties.ReadOnly = Status
        CheckEdit25.Properties.ReadOnly = Status
        CheckEdit27.Properties.ReadOnly = Status
        CheckEdit30.Properties.ReadOnly = Status
        CheckEdit29.Properties.ReadOnly = Status
        CheckEdit28.Properties.ReadOnly = Status
        CheckEdit31.Properties.ReadOnly = Status
        CheckEdit32.Properties.ReadOnly = Status
        TextEdit14.Properties.ReadOnly = Status
        CheckEdit35.Properties.ReadOnly = Status
        CheckEdit34.Properties.ReadOnly = Status
        CheckEdit33.Properties.ReadOnly = Status
        CheckEdit50.Properties.ReadOnly = Status
        CheckEdit37.Properties.ReadOnly = Status
        CheckEdit51.Properties.ReadOnly = Status
        TextEdit15.Properties.ReadOnly = Status
        CheckEdit54.Properties.ReadOnly = Status
        CheckEdit53.Properties.ReadOnly = Status
        CheckEdit52.Properties.ReadOnly = Status
        CheckEdit36.Properties.ReadOnly = Status
        TextEdit17.Properties.ReadOnly = Status
        TextEdit18.Properties.ReadOnly = Status
        CheckEdit61.Properties.ReadOnly = Status
        CheckEdit60.Properties.ReadOnly = Status
        CheckEdit59.Properties.ReadOnly = Status
        CheckEdit58.Properties.ReadOnly = Status
        CheckEdit57.Properties.ReadOnly = Status
        CheckEdit56.Properties.ReadOnly = Status
        CheckEdit55.Properties.ReadOnly = Status
        TextEdit19.Properties.ReadOnly = Status
        CheckEdit68.Properties.ReadOnly = Status
        CheckEdit67.Properties.ReadOnly = Status
        CheckEdit66.Properties.ReadOnly = Status
        CheckEdit65.Properties.ReadOnly = Status
        CheckEdit62.Properties.ReadOnly = Status
        TextEdit20.Properties.ReadOnly = Status
        CheckEdit63.Properties.ReadOnly = Status
        CheckEdit64.Properties.ReadOnly = Status
        CheckEdit69.Properties.ReadOnly = Status
        CheckEdit70.Properties.ReadOnly = Status
        CheckEdit71.Properties.ReadOnly = Status
        CheckEdit72.Properties.ReadOnly = Status
        CheckEdit73.Properties.ReadOnly = Status
        CheckEdit74.Properties.ReadOnly = Status
        TextEdit16.Properties.ReadOnly = Status
        CheckEdit75.Properties.ReadOnly = Status
        CheckEdit76.Properties.ReadOnly = Status
        CheckEdit78.Properties.ReadOnly = Status
        TextEdit21.Properties.ReadOnly = Status
        CheckEdit77.Properties.ReadOnly = Status
        CheckEdit79.Properties.ReadOnly = Status
        CheckEdit80.Properties.ReadOnly = Status
        TextEdit22.Properties.ReadOnly = Status
        CheckEdit81.Properties.ReadOnly = Status
        CheckEdit85.Properties.ReadOnly = Status
        CheckEdit84.Properties.ReadOnly = Status
        CheckEdit86.Properties.ReadOnly = Status
        TextEdit24.Properties.ReadOnly = Status
        CheckEdit89.Properties.ReadOnly = Status
        CheckEdit88.Properties.ReadOnly = Status
        CheckEdit87.Properties.ReadOnly = Status
        CheckEdit82.Properties.ReadOnly = Status
        TextEdit25.Properties.ReadOnly = Status
        CheckEdit90.Properties.ReadOnly = Status
        CheckEdit91.Properties.ReadOnly = Status
        CheckEdit94.Properties.ReadOnly = Status
        CheckEdit93.Properties.ReadOnly = Status
        CheckEdit92.Properties.ReadOnly = Status
        TextEdit26.Properties.ReadOnly = Status
        CheckEdit96.Properties.ReadOnly = Status
        CheckEdit95.Properties.ReadOnly = Status
        CheckEdit97.Properties.ReadOnly = Status
        TextEdit27.Properties.ReadOnly = Status
        CheckEdit98.Properties.ReadOnly = Status
        TextEdit28.Properties.ReadOnly = Status
        CheckEdit100.Properties.ReadOnly = Status
        CheckEdit99.Properties.ReadOnly = Status
        TextEdit23.Properties.ReadOnly = Status
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
        chkTINDAKAN_01_TIDAK.Properties.ReadOnly = Status
        chkTINDAKAN_02_YA.Properties.ReadOnly = Status
        chkTINDAKAN_02_TIDAK.Properties.ReadOnly = Status
        chkTINDAKAN_03_YA.Properties.ReadOnly = Status
        chkTINDAKAN_03_TIDAK.Properties.ReadOnly = Status
        chkTINDAKAN_04_YA.Properties.ReadOnly = Status
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
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01.Properties.ReadOnly = Status
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02.Properties.ReadOnly = Status
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03.Properties.ReadOnly = Status
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04.Properties.ReadOnly = Status
        txtKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN.Properties.ReadOnly = Status
        chkEDUKASI_01.Properties.ReadOnly = Status
        chkEDUKASI_02.Properties.ReadOnly = Status
        chkEDUKASI_03.Properties.ReadOnly = Status
        txtEDUKASI_03_TEXT.Properties.ReadOnly = Status
        chkBICARA_SEHARI2_01.Properties.ReadOnly = Status
        chkBICARA_SEHARI2_01_1.Properties.ReadOnly = Status
        chkBICARA_SEHARI2_01_2.Properties.ReadOnly = Status
        chkBICARA_SEHARI2_02.Properties.ReadOnly = Status
        chkBICARA_SEHARI2_02_1.Properties.ReadOnly = Status
        chkBICARA_SEHARI2_02_2.Properties.ReadOnly = Status
        chkBICARA_SEHARI2_03.Properties.ReadOnly = Status
        txtBICARA_SEHARI2_03_TEXT.Properties.ReadOnly = Status
        chkBICARA_SEHARI2_04.Properties.ReadOnly = Status
        txtBICARA_SEHARI2_04_TEXT.Properties.ReadOnly = Status
        chkPERLU_PENERJEMAH_01.Properties.ReadOnly = Status
        chkPERLU_PENERJEMAH_02.Properties.ReadOnly = Status
        chkBAHASA_ISYARAT_01.Properties.ReadOnly = Status
        chkBAHASA_ISYARAT_02.Properties.ReadOnly = Status
        chkHAMBATAN_EDUKASI_01.Properties.ReadOnly = Status
        chkHAMBATAN_EDUKASI_02.Properties.ReadOnly = Status
        chkHAMBATAN_EDUKASI_03.Properties.ReadOnly = Status
        chkHAMBATAN_EDUKASI_04.Properties.ReadOnly = Status
        chkHAMBATAN_EDUKASI_05.Properties.ReadOnly = Status
        chkHAMBATAN_EDUKASI_06.Properties.ReadOnly = Status
        chkHAMBATAN_EDUKASI_07.Properties.ReadOnly = Status
        chkHAMBATAN_EDUKASI_08.Properties.ReadOnly = Status
        chkHAMBATAN_EDUKASI_09.Properties.ReadOnly = Status
        chkHAMBATAN_EDUKASI_10.Properties.ReadOnly = Status
        chkHAMBATAN_EDUKASI_11.Properties.ReadOnly = Status
        chkHAMBATAN_EDUKASI_12.Properties.ReadOnly = Status
        CheckEdit83.Properties.ReadOnly = Status
        CheckEdit101.Properties.ReadOnly = Status
        CheckEdit103.Properties.ReadOnly = Status
        CheckEdit102.Properties.ReadOnly = Status
        CheckEdit107.Properties.ReadOnly = Status
        CheckEdit106.Properties.ReadOnly = Status
        CheckEdit105.Properties.ReadOnly = Status
        CheckEdit104.Properties.ReadOnly = Status
        CheckEdit108.Properties.ReadOnly = Status
        TextEdit29.Properties.ReadOnly = Status
        CheckEdit117.Properties.ReadOnly = Status
        CheckEdit116.Properties.ReadOnly = Status
        CheckEdit115.Properties.ReadOnly = Status
        CheckEdit114.Properties.ReadOnly = Status
        CheckEdit109.Properties.ReadOnly = Status
        TextEdit30.Properties.ReadOnly = Status
        CheckEdit118.Properties.ReadOnly = Status
        CheckEdit113.Properties.ReadOnly = Status
        CheckEdit112.Properties.ReadOnly = Status
        CheckEdit110.Properties.ReadOnly = Status
        TextEdit31.Properties.ReadOnly = Status
        CheckEdit121.Properties.ReadOnly = Status
        CheckEdit119.Properties.ReadOnly = Status
        CheckEdit120.Properties.ReadOnly = Status
        CheckEdit124.Properties.ReadOnly = Status
        CheckEdit123.Properties.ReadOnly = Status
        CheckEdit122.Properties.ReadOnly = Status
        CheckEdit126.Properties.ReadOnly = Status
        CheckEdit125.Properties.ReadOnly = Status
        CheckEdit111.Properties.ReadOnly = Status
        TextEdit32.Properties.ReadOnly = Status
        CheckEdit135.Properties.ReadOnly = Status
        CheckEdit134.Properties.ReadOnly = Status
        CheckEdit133.Properties.ReadOnly = Status
        CheckEdit132.Properties.ReadOnly = Status
        CheckEdit131.Properties.ReadOnly = Status
        CheckEdit130.Properties.ReadOnly = Status
        CheckEdit129.Properties.ReadOnly = Status
        CheckEdit127.Properties.ReadOnly = Status
        TextEdit33.Properties.ReadOnly = Status
        CheckEdit139.Properties.ReadOnly = Status
        CheckEdit138.Properties.ReadOnly = Status
        CheckEdit137.Properties.ReadOnly = Status
        TextEdit34.Properties.ReadOnly = Status
        TextEdit36.Properties.ReadOnly = Status
        TextEdit35.Properties.ReadOnly = Status
        txtCATATAN_KEPERAWATAN_JAM.Properties.ReadOnly = Status
        CheckEdit145.Properties.ReadOnly = Status
        TextEdit38.Properties.ReadOnly = Status
        TextEdit39.Properties.ReadOnly = Status
        TextEdit40.Properties.ReadOnly = Status
        TextEdit41.Properties.ReadOnly = Status
        CheckEdit140.Properties.ReadOnly = Status
        CheckEdit141.Properties.ReadOnly = Status
        CheckEdit144.Properties.ReadOnly = Status
        CheckEdit143.Properties.ReadOnly = Status
        CheckEdit142.Properties.ReadOnly = Status
        CheckEdit136.Properties.ReadOnly = Status
        TextEdit37.Properties.ReadOnly = Status
        TextEdit43.Properties.ReadOnly = Status
        CheckEdit146.Properties.ReadOnly = Status
        CheckEdit147.Properties.ReadOnly = Status
        CheckEdit151.Properties.ReadOnly = Status
        CheckEdit148.Properties.ReadOnly = Status
        CheckEdit149.Properties.ReadOnly = Status
        CheckEdit150.Properties.ReadOnly = Status
        CheckEdit152.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_03_2.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_03_3.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_03_4.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_04_1.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_03_6.Properties.ReadOnly = Status
        txtINTERVENSI_IMPLEMENTASI_03_6_TEXT.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_04_2.Properties.ReadOnly = Status
        CheckEdit128.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_04_3.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_04_4.Properties.ReadOnly = Status
        txtINTERVENSI_IMPLEMENTASI_04_4_TEXT.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_04_5.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_04_6.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_04_7.Properties.ReadOnly = Status
        CheckEdit154.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_04_8.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_05_1.Properties.ReadOnly = Status
        CheckEdit153.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_05_4.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_05_5.Properties.ReadOnly = Status
        chkINTERVENSI_IMPLEMENTASI_05_6.Properties.ReadOnly = Status
        txtINTERVENSI_IMPLEMENTASI_05_6_TEXT.Properties.ReadOnly = Status
        chkHASIL_PENANGANAN_01.Properties.ReadOnly = Status
        chkHASIL_PENANGANAN_02.Properties.ReadOnly = Status
        chkHASIL_PENANGANAN_03.Properties.ReadOnly = Status
        chkHASIL_PENANGANAN_04.Properties.ReadOnly = Status
        chkHASIL_PENANGANAN_05.Properties.ReadOnly = Status

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
        txtKELUHAN_UTAMA.ResetText()
        txtKESADARAN_UMUM.ResetText()
        txtTANDA_VITAL_01.ResetText()
        txtTANDA_VITAL_02.ResetText()
        txtTANDA_VITAL_03.ResetText()
        txtTANDA_VITAL_04.ResetText()
        chkTANDA_VITAL_REGULER.Checked = False
        chkTANDA_VITAL_IREGULER.Checked = False
        txtTANDA_VITAL_05.ResetText()
        txtTANDA_VITAL_06.ResetText()
        txtTANDA_VITAL_07.ResetText()
        txtTANDA_VITAL_08.ResetText()
        TextEdit42.ResetText()
        CheckEdit2.Checked = False
        CheckEdit1.Checked = False
        TextEdit1.ResetText()
        CheckEdit4.Checked = False
        CheckEdit3.Checked = False
        CheckEdit5.Checked = False
        TextEdit2.ResetText()
        CheckEdit6.Checked = False
        CheckEdit7.Checked = False
        CheckEdit9.Checked = False
        CheckEdit8.Checked = False
        TextEdit3.ResetText()
        TextEdit4.ResetText()
        TextEdit5.ResetText()
        TextEdit6.ResetText()
        CheckEdit12.Checked = False
        TextEdit7.ResetText()
        CheckEdit11.Checked = False
        CheckEdit14.Checked = False
        CheckEdit10.Checked = False
        TextEdit8.ResetText()
        CheckEdit13.Checked = False
        TextEdit9.ResetText()
        CheckEdit15.Checked = False
        TextEdit10.ResetText()
        TextEdit11.ResetText()
        CheckEdit16.Checked = False
        CheckEdit17.Checked = False
        CheckEdit19.Checked = False
        CheckEdit18.Checked = False
        CheckEdit21.Checked = False
        CheckEdit20.Checked = False
        CheckEdit23.Checked = False
        CheckEdit22.Checked = False
        CheckEdit24.Checked = False
        TextEdit13.ResetText()
        TextEdit12.ResetText()
        CheckEdit26.Checked = False
        CheckEdit25.Checked = False
        CheckEdit27.Checked = False
        CheckEdit30.Checked = False
        CheckEdit29.Checked = False
        CheckEdit28.Checked = False
        CheckEdit31.Checked = False
        CheckEdit32.Checked = False
        TextEdit14.ResetText()
        CheckEdit35.Checked = False
        CheckEdit34.Checked = False
        CheckEdit33.Checked = False
        CheckEdit50.Checked = False
        CheckEdit37.Checked = False
        CheckEdit51.Checked = False
        TextEdit15.ResetText()
        CheckEdit54.Checked = False
        CheckEdit53.Checked = False
        CheckEdit52.Checked = False
        CheckEdit36.Checked = False
        TextEdit17.ResetText()
        TextEdit18.ResetText()
        CheckEdit61.Checked = False
        CheckEdit60.Checked = False
        CheckEdit59.Checked = False
        CheckEdit58.Checked = False
        CheckEdit57.Checked = False
        CheckEdit56.Checked = False
        CheckEdit55.Checked = False
        TextEdit19.ResetText()
        CheckEdit68.Checked = False
        CheckEdit67.Checked = False
        CheckEdit66.Checked = False
        CheckEdit65.Checked = False
        CheckEdit62.Checked = False
        TextEdit20.ResetText()
        CheckEdit63.Checked = False
        CheckEdit64.Checked = False
        CheckEdit69.Checked = False
        CheckEdit70.Checked = False
        CheckEdit71.Checked = False
        CheckEdit72.Checked = False
        CheckEdit73.Checked = False
        CheckEdit74.Checked = False
        TextEdit16.ResetText()
        CheckEdit75.Checked = False
        CheckEdit76.Checked = False
        CheckEdit78.Checked = False
        TextEdit21.ResetText()
        CheckEdit77.Checked = False
        CheckEdit79.Checked = False
        CheckEdit80.Checked = False
        TextEdit22.ResetText()
        CheckEdit81.Checked = False
        CheckEdit85.Checked = False
        CheckEdit84.Checked = False
        CheckEdit86.Checked = False
        TextEdit24.ResetText()
        CheckEdit89.Checked = False
        CheckEdit88.Checked = False
        CheckEdit87.Checked = False
        CheckEdit82.Checked = False
        TextEdit25.ResetText()
        CheckEdit90.Checked = False
        CheckEdit91.Checked = False
        CheckEdit94.Checked = False
        CheckEdit93.Checked = False
        CheckEdit92.Checked = False
        TextEdit26.ResetText()
        CheckEdit96.Checked = False
        CheckEdit95.Checked = False
        CheckEdit97.Checked = False
        TextEdit27.ResetText()
        CheckEdit98.Checked = False
        TextEdit28.ResetText()
        CheckEdit100.Checked = False
        CheckEdit99.Checked = False
        TextEdit23.ResetText()
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
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01.Checked = False
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02.Checked = False
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03.Checked = False
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04.Checked = False
        txtKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN.ResetText()
        chkEDUKASI_01.Checked = False
        chkEDUKASI_02.Checked = False
        chkEDUKASI_03.Checked = False
        txtEDUKASI_03_TEXT.ResetText()
        chkBICARA_SEHARI2_01.Checked = False
        chkBICARA_SEHARI2_01_1.Checked = False
        chkBICARA_SEHARI2_01_2.Checked = False
        chkBICARA_SEHARI2_02.Checked = False
        chkBICARA_SEHARI2_02_1.Checked = False
        chkBICARA_SEHARI2_02_2.Checked = False
        chkBICARA_SEHARI2_03.Checked = False
        txtBICARA_SEHARI2_03_TEXT.ResetText()
        chkBICARA_SEHARI2_04.Checked = False
        txtBICARA_SEHARI2_04_TEXT.ResetText()
        chkPERLU_PENERJEMAH_01.Checked = False
        chkPERLU_PENERJEMAH_02.Checked = False
        chkBAHASA_ISYARAT_01.Checked = False
        chkBAHASA_ISYARAT_02.Checked = False
        chkHAMBATAN_EDUKASI_01.Checked = False
        chkHAMBATAN_EDUKASI_02.Checked = False
        chkHAMBATAN_EDUKASI_03.Checked = False
        chkHAMBATAN_EDUKASI_04.Checked = False
        chkHAMBATAN_EDUKASI_05.Checked = False
        chkHAMBATAN_EDUKASI_06.Checked = False
        chkHAMBATAN_EDUKASI_07.Checked = False
        chkHAMBATAN_EDUKASI_08.Checked = False
        chkHAMBATAN_EDUKASI_09.Checked = False
        chkHAMBATAN_EDUKASI_10.Checked = False
        chkHAMBATAN_EDUKASI_11.Checked = False
        chkHAMBATAN_EDUKASI_12.Checked = False
        CheckEdit83.Checked = False
        CheckEdit101.Checked = False
        CheckEdit103.Checked = False
        CheckEdit102.Checked = False
        CheckEdit107.Checked = False
        CheckEdit106.Checked = False
        CheckEdit105.Checked = False
        CheckEdit104.Checked = False
        CheckEdit108.Checked = False
        TextEdit29.ResetText()
        CheckEdit117.Checked = False
        CheckEdit116.Checked = False
        CheckEdit115.Checked = False
        CheckEdit114.Checked = False
        CheckEdit109.Checked = False
        TextEdit30.ResetText()
        CheckEdit118.Checked = False
        CheckEdit113.Checked = False
        CheckEdit112.Checked = False
        CheckEdit110.Checked = False
        TextEdit31.ResetText()
        CheckEdit121.Checked = False
        CheckEdit119.Checked = False
        CheckEdit120.Checked = False
        CheckEdit124.Checked = False
        CheckEdit123.Checked = False
        CheckEdit122.Checked = False
        CheckEdit126.Checked = False
        CheckEdit125.Checked = False
        CheckEdit111.Checked = False
        TextEdit32.ResetText()
        CheckEdit135.Checked = False
        CheckEdit134.Checked = False
        CheckEdit133.Checked = False
        CheckEdit132.Checked = False
        CheckEdit131.Checked = False
        CheckEdit130.Checked = False
        CheckEdit129.Checked = False
        CheckEdit127.Checked = False
        TextEdit33.ResetText()
        CheckEdit139.Checked = False
        CheckEdit138.Checked = False
        CheckEdit137.Checked = False
        TextEdit34.ResetText()
        TextEdit36.ResetText()
        TextEdit35.ResetText()
        txtCATATAN_KEPERAWATAN_JAM.ResetText()
        CheckEdit145.Checked = False
        TextEdit38.ResetText()
        TextEdit39.ResetText()
        TextEdit40.ResetText()
        TextEdit41.ResetText()
        CheckEdit140.Checked = False
        CheckEdit141.Checked = False
        CheckEdit144.Checked = False
        CheckEdit143.Checked = False
        CheckEdit142.Checked = False
        CheckEdit136.Checked = False
        TextEdit37.ResetText()
        TextEdit43.ResetText()
        CheckEdit146.Checked = False
        CheckEdit147.Checked = False
        CheckEdit151.Checked = False
        CheckEdit148.Checked = False
        CheckEdit149.Checked = False
        CheckEdit150.Checked = False
        CheckEdit152.Checked = False
        chkINTERVENSI_IMPLEMENTASI_03_2.Checked = False
        chkINTERVENSI_IMPLEMENTASI_03_3.Checked = False
        chkINTERVENSI_IMPLEMENTASI_03_4.Checked = False
        chkINTERVENSI_IMPLEMENTASI_04_1.Checked = False
        chkINTERVENSI_IMPLEMENTASI_03_6.Checked = False
        txtINTERVENSI_IMPLEMENTASI_03_6_TEXT.ResetText()
        chkINTERVENSI_IMPLEMENTASI_04_2.Checked = False
        CheckEdit128.Checked = False
        chkINTERVENSI_IMPLEMENTASI_04_3.Checked = False
        chkINTERVENSI_IMPLEMENTASI_04_4.Checked = False
        txtINTERVENSI_IMPLEMENTASI_04_4_TEXT.ResetText()
        chkINTERVENSI_IMPLEMENTASI_04_5.Checked = False
        chkINTERVENSI_IMPLEMENTASI_04_6.Checked = False
        chkINTERVENSI_IMPLEMENTASI_04_7.Checked = False
        CheckEdit154.Checked = False
        chkINTERVENSI_IMPLEMENTASI_04_8.Checked = False
        chkINTERVENSI_IMPLEMENTASI_05_1.Checked = False
        CheckEdit153.Checked = False
        chkINTERVENSI_IMPLEMENTASI_05_4.Checked = False
        chkINTERVENSI_IMPLEMENTASI_05_5.Checked = False
        chkINTERVENSI_IMPLEMENTASI_05_6.Checked = False
        txtINTERVENSI_IMPLEMENTASI_05_6_TEXT.ResetText()
        chkHASIL_PENANGANAN_01.Checked = False
        chkHASIL_PENANGANAN_02.Checked = False
        chkHASIL_PENANGANAN_03.Checked = False
        chkHASIL_PENANGANAN_04.Checked = False
        chkHASIL_PENANGANAN_05.Checked = False

        deDATE.DateTime = Now
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oDigital.GetData(txtNoRegister.Text)

            With ds

                chkAUTO.Checked = .SDIGITALRJ24_01
                chkALLO.Checked = .SDIGITALRJ24_02
                txtNAMAPENGKAJIAN.Text = .SDIGITALRJ24_03
                txtKELUHAN_UTAMA.Text = .SDIGITALRJ24_04
                txtKESADARAN_UMUM.Text = .SDIGITALRJ24_05
                txtTANDA_VITAL_01.Text = .SDIGITALRJ24_06
                txtTANDA_VITAL_02.Text = .SDIGITALRJ24_07
                txtTANDA_VITAL_03.Text = .SDIGITALRJ24_08
                txtTANDA_VITAL_04.Text = .SDIGITALRJ24_09
                chkTANDA_VITAL_REGULER.Checked = .SDIGITALRJ24_10
                chkTANDA_VITAL_IREGULER.Checked = .SDIGITALRJ24_11
                txtTANDA_VITAL_05.Text = .SDIGITALRJ24_12
                txtTANDA_VITAL_06.Text = .SDIGITALRJ24_13
                txtTANDA_VITAL_07.Text = .SDIGITALRJ24_14
                txtTANDA_VITAL_08.Text = .SDIGITALRJ24_15
                TextEdit42.Text = .SDIGITALRJ24_16
                CheckEdit2.Checked = .SDIGITALRJ24_17
                CheckEdit1.Checked = .SDIGITALRJ24_18
                TextEdit1.Text = .SDIGITALRJ24_19
                CheckEdit4.Checked = .SDIGITALRJ24_20
                CheckEdit3.Checked = .SDIGITALRJ24_21
                CheckEdit5.Checked = .SDIGITALRJ24_22
                TextEdit2.Text = .SDIGITALRJ24_23
                CheckEdit6.Checked = .SDIGITALRJ24_24
                CheckEdit7.Checked = .SDIGITALRJ24_25
                CheckEdit9.Checked = .SDIGITALRJ24_26
                CheckEdit8.Checked = .SDIGITALRJ24_27
                TextEdit3.Text = .SDIGITALRJ24_28
                TextEdit4.Text = .SDIGITALRJ24_29
                TextEdit5.Text = .SDIGITALRJ24_30
                TextEdit6.Text = .SDIGITALRJ24_31
                CheckEdit12.Checked = .SDIGITALRJ24_32
                TextEdit7.Text = .SDIGITALRJ24_33
                CheckEdit11.Checked = .SDIGITALRJ24_34
                CheckEdit14.Checked = .SDIGITALRJ24_35
                CheckEdit10.Checked = .SDIGITALRJ24_36
                TextEdit8.Text = .SDIGITALRJ24_37
                CheckEdit13.Checked = .SDIGITALRJ24_38
                TextEdit9.Text = .SDIGITALRJ24_39
                CheckEdit15.Checked = .SDIGITALRJ24_40
                TextEdit10.Text = .SDIGITALRJ24_41
                TextEdit11.Text = .SDIGITALRJ24_42
                CheckEdit16.Checked = .SDIGITALRJ24_43
                CheckEdit17.Checked = .SDIGITALRJ24_44
                CheckEdit19.Checked = .SDIGITALRJ24_45
                CheckEdit18.Checked = .SDIGITALRJ24_46
                CheckEdit21.Checked = .SDIGITALRJ24_47
                CheckEdit20.Checked = .SDIGITALRJ24_48
                CheckEdit23.Checked = .SDIGITALRJ24_49
                CheckEdit22.Checked = .SDIGITALRJ24_50
                CheckEdit24.Checked = .SDIGITALRJ24_51
                TextEdit13.Text = .SDIGITALRJ24_52
                TextEdit12.Text = .SDIGITALRJ24_53
                CheckEdit26.Checked = .SDIGITALRJ24_54
                CheckEdit25.Checked = .SDIGITALRJ24_55
                CheckEdit27.Checked = .SDIGITALRJ24_56
                CheckEdit30.Checked = .SDIGITALRJ24_57
                CheckEdit29.Checked = .SDIGITALRJ24_58
                CheckEdit28.Checked = .SDIGITALRJ24_59
                CheckEdit31.Checked = .SDIGITALRJ24_60
                CheckEdit32.Checked = .SDIGITALRJ24_61
                TextEdit14.Text = .SDIGITALRJ24_62
                CheckEdit35.Checked = .SDIGITALRJ24_63
                CheckEdit34.Checked = .SDIGITALRJ24_64
                CheckEdit33.Checked = .SDIGITALRJ24_65
                CheckEdit50.Checked = .SDIGITALRJ24_66
                CheckEdit37.Checked = .SDIGITALRJ24_67
                CheckEdit51.Checked = .SDIGITALRJ24_68
                TextEdit15.Text = .SDIGITALRJ24_69
                CheckEdit54.Checked = .SDIGITALRJ24_70
                CheckEdit53.Checked = .SDIGITALRJ24_71
                CheckEdit52.Checked = .SDIGITALRJ24_72
                CheckEdit36.Checked = .SDIGITALRJ24_73
                TextEdit17.Text = .SDIGITALRJ24_74
                TextEdit18.Text = .SDIGITALRJ24_75
                CheckEdit61.Checked = .SDIGITALRJ24_76
                CheckEdit60.Checked = .SDIGITALRJ24_77
                CheckEdit59.Checked = .SDIGITALRJ24_78
                CheckEdit58.Checked = .SDIGITALRJ24_79
                CheckEdit57.Checked = .SDIGITALRJ24_80
                CheckEdit56.Checked = .SDIGITALRJ24_81
                CheckEdit55.Checked = .SDIGITALRJ24_82
                TextEdit19.Text = .SDIGITALRJ24_83
                CheckEdit68.Checked = .SDIGITALRJ24_84
                CheckEdit67.Checked = .SDIGITALRJ24_85
                CheckEdit66.Checked = .SDIGITALRJ24_86
                CheckEdit65.Checked = .SDIGITALRJ24_87
                CheckEdit62.Checked = .SDIGITALRJ24_88
                TextEdit20.Text = .SDIGITALRJ24_89
                CheckEdit63.Checked = .SDIGITALRJ24_90
                CheckEdit64.Checked = .SDIGITALRJ24_91
                CheckEdit69.Checked = .SDIGITALRJ24_92
                CheckEdit70.Checked = .SDIGITALRJ24_93
                CheckEdit71.Checked = .SDIGITALRJ24_94
                CheckEdit72.Checked = .SDIGITALRJ24_95
                CheckEdit73.Checked = .SDIGITALRJ24_96
                CheckEdit74.Checked = .SDIGITALRJ24_97
                TextEdit16.Text = .SDIGITALRJ24_98
                CheckEdit75.Checked = .SDIGITALRJ24_99
                CheckEdit76.Checked = .SDIGITALRJ24_100
                CheckEdit78.Checked = .SDIGITALRJ24_101
                TextEdit21.Text = .SDIGITALRJ24_102
                CheckEdit77.Checked = .SDIGITALRJ24_103
                CheckEdit79.Checked = .SDIGITALRJ24_104
                CheckEdit80.Checked = .SDIGITALRJ24_105
                TextEdit22.Text = .SDIGITALRJ24_106
                CheckEdit81.Checked = .SDIGITALRJ24_107
                CheckEdit85.Checked = .SDIGITALRJ24_108
                CheckEdit84.Checked = .SDIGITALRJ24_109
                CheckEdit86.Checked = .SDIGITALRJ24_110
                TextEdit24.Text = .SDIGITALRJ24_111
                CheckEdit89.Checked = .SDIGITALRJ24_112
                CheckEdit88.Checked = .SDIGITALRJ24_113
                CheckEdit87.Checked = .SDIGITALRJ24_114
                CheckEdit82.Checked = .SDIGITALRJ24_115
                TextEdit25.Text = .SDIGITALRJ24_116
                CheckEdit90.Checked = .SDIGITALRJ24_117
                CheckEdit91.Checked = .SDIGITALRJ24_118
                CheckEdit94.Checked = .SDIGITALRJ24_119
                CheckEdit93.Checked = .SDIGITALRJ24_120
                CheckEdit92.Checked = .SDIGITALRJ24_121
                TextEdit26.Text = .SDIGITALRJ24_122
                CheckEdit96.Checked = .SDIGITALRJ24_123
                CheckEdit95.Checked = .SDIGITALRJ24_124
                CheckEdit97.Checked = .SDIGITALRJ24_125
                TextEdit27.Text = .SDIGITALRJ24_126
                CheckEdit98.Checked = .SDIGITALRJ24_127
                TextEdit28.Text = .SDIGITALRJ24_128
                CheckEdit100.Checked = .SDIGITALRJ24_129
                CheckEdit99.Checked = .SDIGITALRJ24_130
                TextEdit23.Text = .SDIGITALRJ24_131
                chkSKRINING_GIZI_01_1.Checked = .SDIGITALRJ24_132
                chkSKRINING_GIZI_01_2.Checked = .SDIGITALRJ24_133
                chkSKRINING_GIZI_02_1.Checked = .SDIGITALRJ24_134
                chkSKRINING_GIZI_02_2.Checked = .SDIGITALRJ24_135
                chkSKRINING_GIZI_03_1.Checked = .SDIGITALRJ24_136
                chkSKRINING_GIZI_03_2.Checked = .SDIGITALRJ24_137

                chkSKRINING_RESIKO_JATUH_01_1.Checked = .SDIGITALRJ24_138
                chkSKRINING_RESIKO_JATUH_01_2.Checked = .SDIGITALRJ24_139
                chkSKRINING_RESIKO_JATUH_02_1.Checked = .SDIGITALRJ24_140
                chkSKRINING_RESIKO_JATUH_02_2.Checked = .SDIGITALRJ24_141

                CheckEdit39.Checked = .SDIGITALRJ24_142
                CheckEdit43.Checked = .SDIGITALRJ24_143
                CheckEdit48.Checked = .SDIGITALRJ24_144
                CheckEdit49.Checked = .SDIGITALRJ24_145
                CheckEdit44.Checked = .SDIGITALRJ24_146
                CheckEdit40.Checked = .SDIGITALRJ24_147
                CheckEdit41.Checked = .SDIGITALRJ24_148
                CheckEdit46.Checked = .SDIGITALRJ24_149
                CheckEdit47.Checked = .SDIGITALRJ24_150
                CheckEdit45.Checked = .SDIGITALRJ24_151
                CheckEdit38.Checked = .SDIGITALRJ24_152
                txtDIGITALRJ_79.Checked = .SDIGITALRJ24_153
                txtDIGITALRJ_81.Text = .SDIGITALRJ24_154
                txtDIGITALRJ_83.Text = .SDIGITALRJ24_155
                txtDIGITALRJ_85.Text = .SDIGITALRJ24_156
                txtDIGITALRJ_80.Checked = .SDIGITALRJ24_157
                txtDIGITALRJ_82.Text = .SDIGITALRJ24_158
                txtDIGITALRJ_84.Text = .SDIGITALRJ24_159
                txtDIGITALRJ_86.Text = .SDIGITALRJ24_160
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01.Checked = .SDIGITALRJ24_161
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02.Checked = .SDIGITALRJ24_162
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03.Checked = .SDIGITALRJ24_163
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04.Checked = .SDIGITALRJ24_164
                txtKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN.Text = .SDIGITALRJ24_165
                chkEDUKASI_01.Checked = .SDIGITALRJ24_166
                chkEDUKASI_02.Checked = .SDIGITALRJ24_167
                chkEDUKASI_03.Checked = .SDIGITALRJ24_168
                txtEDUKASI_03_TEXT.Text = .SDIGITALRJ24_169
                chkBICARA_SEHARI2_01.Checked = .SDIGITALRJ24_170
                chkBICARA_SEHARI2_01_1.Checked = .SDIGITALRJ24_171
                chkBICARA_SEHARI2_01_2.Checked = .SDIGITALRJ24_172
                chkBICARA_SEHARI2_02.Checked = .SDIGITALRJ24_173
                chkBICARA_SEHARI2_02_1.Checked = .SDIGITALRJ24_174
                chkBICARA_SEHARI2_02_2.Checked = .SDIGITALRJ24_175
                chkBICARA_SEHARI2_03.Checked = .SDIGITALRJ24_176
                txtBICARA_SEHARI2_03_TEXT.Text = .SDIGITALRJ24_177
                chkBICARA_SEHARI2_04.Checked = .SDIGITALRJ24_178
                txtBICARA_SEHARI2_04_TEXT.Text = .SDIGITALRJ24_179
                chkPERLU_PENERJEMAH_01.Checked = .SDIGITALRJ24_180
                chkPERLU_PENERJEMAH_02.Checked = .SDIGITALRJ24_181
                chkBAHASA_ISYARAT_01.Checked = .SDIGITALRJ24_182
                chkBAHASA_ISYARAT_02.Checked = .SDIGITALRJ24_183
                chkHAMBATAN_EDUKASI_01.Checked = .SDIGITALRJ24_184
                chkHAMBATAN_EDUKASI_02.Checked = .SDIGITALRJ24_185
                chkHAMBATAN_EDUKASI_03.Checked = .SDIGITALRJ24_186
                chkHAMBATAN_EDUKASI_04.Checked = .SDIGITALRJ24_187
                chkHAMBATAN_EDUKASI_05.Checked = .SDIGITALRJ24_188
                chkHAMBATAN_EDUKASI_06.Checked = .SDIGITALRJ24_189
                chkHAMBATAN_EDUKASI_07.Checked = .SDIGITALRJ24_190
                chkHAMBATAN_EDUKASI_08.Checked = .SDIGITALRJ24_191
                chkHAMBATAN_EDUKASI_09.Checked = .SDIGITALRJ24_192
                chkHAMBATAN_EDUKASI_10.Checked = .SDIGITALRJ24_193
                chkHAMBATAN_EDUKASI_11.Checked = .SDIGITALRJ24_194
                chkHAMBATAN_EDUKASI_12.Checked = .SDIGITALRJ24_195
                CheckEdit83.Checked = .SDIGITALRJ24_196
                CheckEdit101.Checked = .SDIGITALRJ24_197
                CheckEdit103.Checked = .SDIGITALRJ24_198
                CheckEdit102.Checked = .SDIGITALRJ24_199
                CheckEdit107.Checked = .SDIGITALRJ24_200
                CheckEdit106.Checked = .SDIGITALRJ24_201
                CheckEdit105.Checked = .SDIGITALRJ24_202
                CheckEdit104.Checked = .SDIGITALRJ24_203
                CheckEdit108.Checked = .SDIGITALRJ24_204
                TextEdit29.Text = .SDIGITALRJ24_205
                CheckEdit117.Checked = .SDIGITALRJ24_206
                CheckEdit116.Checked = .SDIGITALRJ24_207
                CheckEdit115.Checked = .SDIGITALRJ24_208
                CheckEdit114.Checked = .SDIGITALRJ24_209
                CheckEdit109.Checked = .SDIGITALRJ24_210
                TextEdit30.Text = .SDIGITALRJ24_211
                CheckEdit118.Checked = .SDIGITALRJ24_212
                CheckEdit113.Checked = .SDIGITALRJ24_213
                CheckEdit112.Checked = .SDIGITALRJ24_214
                CheckEdit110.Checked = .SDIGITALRJ24_215
                TextEdit31.Text = .SDIGITALRJ24_216
                CheckEdit121.Checked = .SDIGITALRJ24_217
                CheckEdit119.Checked = .SDIGITALRJ24_218
                CheckEdit120.Checked = .SDIGITALRJ24_219
                CheckEdit124.Checked = .SDIGITALRJ24_220
                CheckEdit123.Checked = .SDIGITALRJ24_221
                CheckEdit122.Checked = .SDIGITALRJ24_222
                CheckEdit126.Checked = .SDIGITALRJ24_223
                CheckEdit125.Checked = .SDIGITALRJ24_224
                CheckEdit111.Checked = .SDIGITALRJ24_225
                TextEdit32.Text = .SDIGITALRJ24_226
                CheckEdit135.Checked = .SDIGITALRJ24_227
                CheckEdit134.Checked = .SDIGITALRJ24_228
                CheckEdit133.Checked = .SDIGITALRJ24_229
                CheckEdit132.Checked = .SDIGITALRJ24_230
                CheckEdit131.Checked = .SDIGITALRJ24_231
                CheckEdit130.Checked = .SDIGITALRJ24_232
                CheckEdit129.Checked = .SDIGITALRJ24_233
                CheckEdit127.Checked = .SDIGITALRJ24_234
                TextEdit33.Text = .SDIGITALRJ24_235
                CheckEdit139.Checked = .SDIGITALRJ24_236
                CheckEdit138.Checked = .SDIGITALRJ24_237
                CheckEdit137.Checked = .SDIGITALRJ24_238
                TextEdit34.Text = .SDIGITALRJ24_239
                TextEdit36.Text = .SDIGITALRJ24_240
                TextEdit35.Text = .SDIGITALRJ24_241
                txtCATATAN_KEPERAWATAN_JAM.Text = .SDIGITALRJ24_242
                CheckEdit145.Checked = .SDIGITALRJ24_243
                TextEdit38.Text = .SDIGITALRJ24_244
                TextEdit39.Text = .SDIGITALRJ24_245
                TextEdit40.Text = .SDIGITALRJ24_246
                TextEdit41.Text = .SDIGITALRJ24_247
                CheckEdit140.Checked = .SDIGITALRJ24_248
                CheckEdit141.Checked = .SDIGITALRJ24_249
                CheckEdit144.Checked = .SDIGITALRJ24_250
                CheckEdit143.Checked = .SDIGITALRJ24_251
                CheckEdit142.Checked = .SDIGITALRJ24_252
                CheckEdit136.Checked = .SDIGITALRJ24_253
                TextEdit37.Text = .SDIGITALRJ24_254
                TextEdit43.Text = .SDIGITALRJ24_255
                CheckEdit146.Checked = .SDIGITALRJ24_256
                CheckEdit147.Checked = .SDIGITALRJ24_257
                CheckEdit151.Checked = .SDIGITALRJ24_258
                CheckEdit148.Checked = .SDIGITALRJ24_259
                CheckEdit149.Checked = .SDIGITALRJ24_260
                CheckEdit150.Checked = .SDIGITALRJ24_261
                CheckEdit152.Checked = .SDIGITALRJ24_262
                chkINTERVENSI_IMPLEMENTASI_03_2.Checked = .SDIGITALRJ24_263
                chkINTERVENSI_IMPLEMENTASI_03_3.Checked = .SDIGITALRJ24_264
                chkINTERVENSI_IMPLEMENTASI_03_4.Checked = .SDIGITALRJ24_265
                chkINTERVENSI_IMPLEMENTASI_04_1.Checked = .SDIGITALRJ24_266
                chkINTERVENSI_IMPLEMENTASI_03_6.Checked = .SDIGITALRJ24_267
                txtINTERVENSI_IMPLEMENTASI_03_6_TEXT.Text = .SDIGITALRJ24_268
                chkINTERVENSI_IMPLEMENTASI_04_2.Checked = .SDIGITALRJ24_269
                CheckEdit128.Checked = .SDIGITALRJ24_270
                chkINTERVENSI_IMPLEMENTASI_04_3.Checked = .SDIGITALRJ24_271
                chkINTERVENSI_IMPLEMENTASI_04_4.Checked = .SDIGITALRJ24_272
                txtINTERVENSI_IMPLEMENTASI_04_4_TEXT.Text = .SDIGITALRJ24_273
                chkINTERVENSI_IMPLEMENTASI_04_5.Checked = .SDIGITALRJ24_274
                chkINTERVENSI_IMPLEMENTASI_04_6.Checked = .SDIGITALRJ24_275
                chkINTERVENSI_IMPLEMENTASI_04_7.Checked = .SDIGITALRJ24_276
                CheckEdit154.Checked = .SDIGITALRJ24_277
                chkINTERVENSI_IMPLEMENTASI_04_8.Checked = .SDIGITALRJ24_278
                chkINTERVENSI_IMPLEMENTASI_05_1.Checked = .SDIGITALRJ24_279
                CheckEdit153.Checked = .SDIGITALRJ24_280
                chkINTERVENSI_IMPLEMENTASI_05_4.Checked = .SDIGITALRJ24_281
                chkINTERVENSI_IMPLEMENTASI_05_5.Checked = .SDIGITALRJ24_282
                chkINTERVENSI_IMPLEMENTASI_05_6.Checked = .SDIGITALRJ24_283
                txtINTERVENSI_IMPLEMENTASI_05_6_TEXT.Text = .SDIGITALRJ24_284
                chkHASIL_PENANGANAN_01.Checked = .SDIGITALRJ24_285
                chkHASIL_PENANGANAN_02.Checked = .SDIGITALRJ24_286
                chkHASIL_PENANGANAN_03.Checked = .SDIGITALRJ24_287
                chkHASIL_PENANGANAN_04.Checked = .SDIGITALRJ24_288
                chkHASIL_PENANGANAN_05.Checked = .SDIGITALRJ24_289

                grdPerawat.EditValue = .PERAWAT

                deDATE.DateTime = .DATE

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

                BindingSource1.DataSource = oDigital.GetDataDetail.Where(Function(x) x.KDKUNJUNGAN = sKDKUNJUNGAN).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = BindingSource1
                grvDetail.OptionsSelection.MultiSelect = True

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
        Catch oErr As Exception
            MsgBox("Validate Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oDigital.GetStructureHeader
            With ds
                .KDKUNJUNGAN = txtNoRegister.Text
                Try
                    .DATECREATED = oDigital.GetData(txtNoRegister.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime

                .SDIGITALRJ24_01 = chkAUTO.Checked
                .SDIGITALRJ24_02 = chkALLO.Checked
                .SDIGITALRJ24_03 = txtNAMAPENGKAJIAN.Text
                .SDIGITALRJ24_04 = txtKELUHAN_UTAMA.Text
                .SDIGITALRJ24_05 = txtKESADARAN_UMUM.Text
                .SDIGITALRJ24_06 = txtTANDA_VITAL_01.Text
                .SDIGITALRJ24_07 = txtTANDA_VITAL_02.Text
                .SDIGITALRJ24_08 = txtTANDA_VITAL_03.Text
                .SDIGITALRJ24_09 = txtTANDA_VITAL_04.Text
                .SDIGITALRJ24_10 = chkTANDA_VITAL_REGULER.Checked
                .SDIGITALRJ24_11 = chkTANDA_VITAL_IREGULER.Checked
                .SDIGITALRJ24_12 = txtTANDA_VITAL_05.Text
                .SDIGITALRJ24_13 = txtTANDA_VITAL_06.Text
                .SDIGITALRJ24_14 = txtTANDA_VITAL_07.Text
                .SDIGITALRJ24_15 = txtTANDA_VITAL_08.Text
                .SDIGITALRJ24_16 = TextEdit42.Text
                .SDIGITALRJ24_17 = CheckEdit2.Checked
                .SDIGITALRJ24_18 = CheckEdit1.Checked
                .SDIGITALRJ24_19 = TextEdit1.Text
                .SDIGITALRJ24_20 = CheckEdit4.Checked
                .SDIGITALRJ24_21 = CheckEdit3.Checked
                .SDIGITALRJ24_22 = CheckEdit5.Checked
                .SDIGITALRJ24_23 = TextEdit2.Text
                .SDIGITALRJ24_24 = CheckEdit6.Checked
                .SDIGITALRJ24_25 = CheckEdit7.Checked
                .SDIGITALRJ24_26 = CheckEdit9.Checked
                .SDIGITALRJ24_27 = CheckEdit8.Checked
                .SDIGITALRJ24_28 = TextEdit3.Text
                .SDIGITALRJ24_29 = TextEdit4.Text
                .SDIGITALRJ24_30 = TextEdit5.Text
                .SDIGITALRJ24_31 = TextEdit6.Text
                .SDIGITALRJ24_32 = CheckEdit12.Checked
                .SDIGITALRJ24_33 = TextEdit7.Text
                .SDIGITALRJ24_34 = CheckEdit11.Checked
                .SDIGITALRJ24_35 = CheckEdit14.Checked
                .SDIGITALRJ24_36 = CheckEdit10.Checked
                .SDIGITALRJ24_37 = TextEdit8.Text
                .SDIGITALRJ24_38 = CheckEdit13.Checked
                .SDIGITALRJ24_39 = TextEdit9.Text
                .SDIGITALRJ24_40 = CheckEdit15.Checked
                .SDIGITALRJ24_41 = TextEdit10.Text
                .SDIGITALRJ24_42 = TextEdit11.Text
                .SDIGITALRJ24_43 = CheckEdit16.Checked
                .SDIGITALRJ24_44 = CheckEdit17.Checked
                .SDIGITALRJ24_45 = CheckEdit19.Checked
                .SDIGITALRJ24_46 = CheckEdit18.Checked
                .SDIGITALRJ24_47 = CheckEdit21.Checked
                .SDIGITALRJ24_48 = CheckEdit20.Checked
                .SDIGITALRJ24_49 = CheckEdit23.Checked
                .SDIGITALRJ24_50 = CheckEdit22.Checked
                .SDIGITALRJ24_51 = CheckEdit24.Checked
                .SDIGITALRJ24_52 = TextEdit13.Text
                .SDIGITALRJ24_53 = TextEdit12.Text
                .SDIGITALRJ24_54 = CheckEdit26.Checked
                .SDIGITALRJ24_55 = CheckEdit25.Checked
                .SDIGITALRJ24_56 = CheckEdit27.Checked
                .SDIGITALRJ24_57 = CheckEdit30.Checked
                .SDIGITALRJ24_58 = CheckEdit29.Checked
                .SDIGITALRJ24_59 = CheckEdit28.Checked
                .SDIGITALRJ24_60 = CheckEdit31.Checked
                .SDIGITALRJ24_61 = CheckEdit32.Checked
                .SDIGITALRJ24_62 = TextEdit14.Text
                .SDIGITALRJ24_63 = CheckEdit35.Checked
                .SDIGITALRJ24_64 = CheckEdit34.Checked
                .SDIGITALRJ24_65 = CheckEdit33.Checked
                .SDIGITALRJ24_66 = CheckEdit50.Checked
                .SDIGITALRJ24_67 = CheckEdit37.Checked
                .SDIGITALRJ24_68 = CheckEdit51.Checked
                .SDIGITALRJ24_69 = TextEdit15.Text
                .SDIGITALRJ24_70 = CheckEdit54.Checked
                .SDIGITALRJ24_71 = CheckEdit53.Checked
                .SDIGITALRJ24_72 = CheckEdit52.Checked
                .SDIGITALRJ24_73 = CheckEdit36.Checked
                .SDIGITALRJ24_74 = TextEdit17.Text
                .SDIGITALRJ24_75 = TextEdit18.Text
                .SDIGITALRJ24_76 = CheckEdit61.Checked
                .SDIGITALRJ24_77 = CheckEdit60.Checked
                .SDIGITALRJ24_78 = CheckEdit59.Checked
                .SDIGITALRJ24_79 = CheckEdit58.Checked
                .SDIGITALRJ24_80 = CheckEdit57.Checked
                .SDIGITALRJ24_81 = CheckEdit56.Checked
                .SDIGITALRJ24_82 = CheckEdit55.Checked
                .SDIGITALRJ24_83 = TextEdit19.Text
                .SDIGITALRJ24_84 = CheckEdit68.Checked
                .SDIGITALRJ24_85 = CheckEdit67.Checked
                .SDIGITALRJ24_86 = CheckEdit66.Checked
                .SDIGITALRJ24_87 = CheckEdit65.Checked
                .SDIGITALRJ24_88 = CheckEdit62.Checked
                .SDIGITALRJ24_89 = TextEdit20.Text
                .SDIGITALRJ24_90 = CheckEdit63.Checked
                .SDIGITALRJ24_91 = CheckEdit64.Checked
                .SDIGITALRJ24_92 = CheckEdit69.Checked
                .SDIGITALRJ24_93 = CheckEdit70.Checked
                .SDIGITALRJ24_94 = CheckEdit71.Checked
                .SDIGITALRJ24_95 = CheckEdit72.Checked
                .SDIGITALRJ24_96 = CheckEdit73.Checked
                .SDIGITALRJ24_97 = CheckEdit74.Checked
                .SDIGITALRJ24_98 = TextEdit16.Text
                .SDIGITALRJ24_99 = CheckEdit75.Checked
                .SDIGITALRJ24_100 = CheckEdit76.Checked
                .SDIGITALRJ24_101 = CheckEdit78.Checked
                .SDIGITALRJ24_102 = TextEdit21.Text
                .SDIGITALRJ24_103 = CheckEdit77.Checked
                .SDIGITALRJ24_104 = CheckEdit79.Checked
                .SDIGITALRJ24_105 = CheckEdit80.Checked
                .SDIGITALRJ24_106 = TextEdit22.Text
                .SDIGITALRJ24_107 = CheckEdit81.Checked
                .SDIGITALRJ24_108 = CheckEdit85.Checked
                .SDIGITALRJ24_109 = CheckEdit84.Checked
                .SDIGITALRJ24_110 = CheckEdit86.Checked
                .SDIGITALRJ24_111 = TextEdit24.Text
                .SDIGITALRJ24_112 = CheckEdit89.Checked
                .SDIGITALRJ24_113 = CheckEdit88.Checked
                .SDIGITALRJ24_114 = CheckEdit87.Checked
                .SDIGITALRJ24_115 = CheckEdit82.Checked
                .SDIGITALRJ24_116 = TextEdit25.Text
                .SDIGITALRJ24_117 = CheckEdit90.Checked
                .SDIGITALRJ24_118 = CheckEdit91.Checked
                .SDIGITALRJ24_119 = CheckEdit94.Checked
                .SDIGITALRJ24_120 = CheckEdit93.Checked
                .SDIGITALRJ24_121 = CheckEdit92.Checked
                .SDIGITALRJ24_122 = TextEdit26.Text
                .SDIGITALRJ24_123 = CheckEdit96.Checked
                .SDIGITALRJ24_124 = CheckEdit95.Checked
                .SDIGITALRJ24_125 = CheckEdit97.Checked
                .SDIGITALRJ24_126 = TextEdit27.Text
                .SDIGITALRJ24_127 = CheckEdit98.Checked
                .SDIGITALRJ24_128 = TextEdit28.Text
                .SDIGITALRJ24_129 = CheckEdit100.Checked
                .SDIGITALRJ24_130 = CheckEdit99.Checked
                .SDIGITALRJ24_131 = TextEdit23.Text
                .SDIGITALRJ24_132 = chkSKRINING_GIZI_01_1.Checked
                .SDIGITALRJ24_133 = chkSKRINING_GIZI_01_2.Checked
                .SDIGITALRJ24_134 = chkSKRINING_GIZI_02_1.Checked
                .SDIGITALRJ24_135 = chkSKRINING_GIZI_02_2.Checked
                .SDIGITALRJ24_136 = chkSKRINING_GIZI_03_1.Checked
                .SDIGITALRJ24_137 = chkSKRINING_GIZI_03_2.Checked

                .SDIGITALRJ24_138 = chkSKRINING_RESIKO_JATUH_01_1.Checked
                .SDIGITALRJ24_139 = chkSKRINING_RESIKO_JATUH_01_2.Checked
                .SDIGITALRJ24_140 = chkSKRINING_RESIKO_JATUH_02_1.Checked
                .SDIGITALRJ24_141 = chkSKRINING_RESIKO_JATUH_02_2.Checked

                .SDIGITALRJ24_142 = CheckEdit39.Checked
                .SDIGITALRJ24_143 = CheckEdit43.Checked
                .SDIGITALRJ24_144 = CheckEdit48.Checked
                .SDIGITALRJ24_145 = CheckEdit49.Checked
                .SDIGITALRJ24_146 = CheckEdit44.Checked
                .SDIGITALRJ24_147 = CheckEdit40.Checked
                .SDIGITALRJ24_148 = CheckEdit41.Checked
                .SDIGITALRJ24_149 = CheckEdit46.Checked
                .SDIGITALRJ24_150 = CheckEdit47.Checked
                .SDIGITALRJ24_151 = CheckEdit45.Checked
                .SDIGITALRJ24_152 = CheckEdit38.Checked
                .SDIGITALRJ24_153 = txtDIGITALRJ_79.Checked
                .SDIGITALRJ24_154 = txtDIGITALRJ_81.Text
                .SDIGITALRJ24_155 = txtDIGITALRJ_83.Text
                .SDIGITALRJ24_156 = txtDIGITALRJ_85.Text
                .SDIGITALRJ24_157 = txtDIGITALRJ_80.Checked
                .SDIGITALRJ24_158 = txtDIGITALRJ_82.Text
                .SDIGITALRJ24_159 = txtDIGITALRJ_84.Text
                .SDIGITALRJ24_160 = txtDIGITALRJ_86.Text
                .SDIGITALRJ24_161 = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01.Checked
                .SDIGITALRJ24_162 = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02.Checked
                .SDIGITALRJ24_163 = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03.Checked
                .SDIGITALRJ24_164 = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04.Checked
                .SDIGITALRJ24_165 = txtKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN.Text
                .SDIGITALRJ24_166 = chkEDUKASI_01.Checked
                .SDIGITALRJ24_167 = chkEDUKASI_02.Checked
                .SDIGITALRJ24_168 = chkEDUKASI_03.Checked
                .SDIGITALRJ24_169 = txtEDUKASI_03_TEXT.Text
                .SDIGITALRJ24_170 = chkBICARA_SEHARI2_01.Checked
                .SDIGITALRJ24_171 = chkBICARA_SEHARI2_01_1.Checked
                .SDIGITALRJ24_172 = chkBICARA_SEHARI2_01_2.Checked
                .SDIGITALRJ24_173 = chkBICARA_SEHARI2_02.Checked
                .SDIGITALRJ24_174 = chkBICARA_SEHARI2_02_1.Checked
                .SDIGITALRJ24_175 = chkBICARA_SEHARI2_02_2.Checked
                .SDIGITALRJ24_176 = chkBICARA_SEHARI2_03.Checked
                .SDIGITALRJ24_177 = txtBICARA_SEHARI2_03_TEXT.Text
                .SDIGITALRJ24_178 = chkBICARA_SEHARI2_04.Checked
                .SDIGITALRJ24_179 = txtBICARA_SEHARI2_04_TEXT.Text
                .SDIGITALRJ24_180 = chkPERLU_PENERJEMAH_01.Checked
                .SDIGITALRJ24_181 = chkPERLU_PENERJEMAH_02.Checked
                .SDIGITALRJ24_182 = chkBAHASA_ISYARAT_01.Checked
                .SDIGITALRJ24_183 = chkBAHASA_ISYARAT_02.Checked
                .SDIGITALRJ24_184 = chkHAMBATAN_EDUKASI_01.Checked
                .SDIGITALRJ24_185 = chkHAMBATAN_EDUKASI_02.Checked
                .SDIGITALRJ24_186 = chkHAMBATAN_EDUKASI_03.Checked
                .SDIGITALRJ24_187 = chkHAMBATAN_EDUKASI_04.Checked
                .SDIGITALRJ24_188 = chkHAMBATAN_EDUKASI_05.Checked
                .SDIGITALRJ24_189 = chkHAMBATAN_EDUKASI_06.Checked
                .SDIGITALRJ24_190 = chkHAMBATAN_EDUKASI_07.Checked
                .SDIGITALRJ24_191 = chkHAMBATAN_EDUKASI_08.Checked
                .SDIGITALRJ24_192 = chkHAMBATAN_EDUKASI_09.Checked
                .SDIGITALRJ24_193 = chkHAMBATAN_EDUKASI_10.Checked
                .SDIGITALRJ24_194 = chkHAMBATAN_EDUKASI_11.Checked
                .SDIGITALRJ24_195 = chkHAMBATAN_EDUKASI_12.Checked
                .SDIGITALRJ24_196 = CheckEdit83.Checked
                .SDIGITALRJ24_197 = CheckEdit101.Checked
                .SDIGITALRJ24_198 = CheckEdit103.Checked
                .SDIGITALRJ24_199 = CheckEdit102.Checked
                .SDIGITALRJ24_200 = CheckEdit107.Checked
                .SDIGITALRJ24_201 = CheckEdit106.Checked
                .SDIGITALRJ24_202 = CheckEdit105.Checked
                .SDIGITALRJ24_203 = CheckEdit104.Checked
                .SDIGITALRJ24_204 = CheckEdit108.Checked
                .SDIGITALRJ24_205 = TextEdit29.Text
                .SDIGITALRJ24_206 = CheckEdit117.Checked
                .SDIGITALRJ24_207 = CheckEdit116.Checked
                .SDIGITALRJ24_208 = CheckEdit115.Checked
                .SDIGITALRJ24_209 = CheckEdit114.Checked
                .SDIGITALRJ24_210 = CheckEdit109.Checked
                .SDIGITALRJ24_211 = TextEdit30.Text
                .SDIGITALRJ24_212 = CheckEdit118.Checked
                .SDIGITALRJ24_213 = CheckEdit113.Checked
                .SDIGITALRJ24_214 = CheckEdit112.Checked
                .SDIGITALRJ24_215 = CheckEdit110.Checked
                .SDIGITALRJ24_216 = TextEdit31.Text
                .SDIGITALRJ24_217 = CheckEdit121.Checked
                .SDIGITALRJ24_218 = CheckEdit119.Checked
                .SDIGITALRJ24_219 = CheckEdit120.Checked
                .SDIGITALRJ24_220 = CheckEdit124.Checked
                .SDIGITALRJ24_221 = CheckEdit123.Checked
                .SDIGITALRJ24_222 = CheckEdit122.Checked
                .SDIGITALRJ24_223 = CheckEdit126.Checked
                .SDIGITALRJ24_224 = CheckEdit125.Checked
                .SDIGITALRJ24_225 = CheckEdit111.Checked
                .SDIGITALRJ24_226 = TextEdit32.Text
                .SDIGITALRJ24_227 = CheckEdit135.Checked
                .SDIGITALRJ24_228 = CheckEdit134.Checked
                .SDIGITALRJ24_229 = CheckEdit133.Checked
                .SDIGITALRJ24_230 = CheckEdit132.Checked
                .SDIGITALRJ24_231 = CheckEdit131.Checked
                .SDIGITALRJ24_232 = CheckEdit130.Checked
                .SDIGITALRJ24_233 = CheckEdit129.Checked
                .SDIGITALRJ24_234 = CheckEdit127.Checked
                .SDIGITALRJ24_235 = TextEdit33.Text
                .SDIGITALRJ24_236 = CheckEdit139.Checked
                .SDIGITALRJ24_237 = CheckEdit138.Checked
                .SDIGITALRJ24_238 = CheckEdit137.Checked
                .SDIGITALRJ24_239 = TextEdit34.Text
                .SDIGITALRJ24_240 = TextEdit36.Text
                .SDIGITALRJ24_241 = TextEdit35.Text
                .SDIGITALRJ24_242 = txtCATATAN_KEPERAWATAN_JAM.Text
                .SDIGITALRJ24_243 = CheckEdit145.Checked
                .SDIGITALRJ24_244 = TextEdit38.Text
                .SDIGITALRJ24_245 = TextEdit39.Text
                .SDIGITALRJ24_246 = TextEdit40.Text
                .SDIGITALRJ24_247 = TextEdit41.Text
                .SDIGITALRJ24_248 = CheckEdit140.Checked
                .SDIGITALRJ24_249 = CheckEdit141.Checked
                .SDIGITALRJ24_250 = CheckEdit144.Checked
                .SDIGITALRJ24_251 = CheckEdit143.Checked
                .SDIGITALRJ24_252 = CheckEdit142.Checked
                .SDIGITALRJ24_253 = CheckEdit136.Checked
                .SDIGITALRJ24_254 = TextEdit37.Text
                .SDIGITALRJ24_255 = TextEdit43.Text
                .SDIGITALRJ24_256 = CheckEdit146.Checked
                .SDIGITALRJ24_257 = CheckEdit147.Checked
                .SDIGITALRJ24_258 = CheckEdit151.Checked
                .SDIGITALRJ24_259 = CheckEdit148.Checked
                .SDIGITALRJ24_260 = CheckEdit149.Checked
                .SDIGITALRJ24_261 = CheckEdit150.Checked
                .SDIGITALRJ24_262 = CheckEdit152.Checked
                .SDIGITALRJ24_263 = chkINTERVENSI_IMPLEMENTASI_03_2.Checked
                .SDIGITALRJ24_264 = chkINTERVENSI_IMPLEMENTASI_03_3.Checked
                .SDIGITALRJ24_265 = chkINTERVENSI_IMPLEMENTASI_03_4.Checked
                .SDIGITALRJ24_266 = chkINTERVENSI_IMPLEMENTASI_04_1.Checked
                .SDIGITALRJ24_267 = chkINTERVENSI_IMPLEMENTASI_03_6.Checked
                .SDIGITALRJ24_268 = txtINTERVENSI_IMPLEMENTASI_03_6_TEXT.Text
                .SDIGITALRJ24_269 = chkINTERVENSI_IMPLEMENTASI_04_2.Checked
                .SDIGITALRJ24_270 = CheckEdit128.Checked
                .SDIGITALRJ24_271 = chkINTERVENSI_IMPLEMENTASI_04_3.Checked
                .SDIGITALRJ24_272 = chkINTERVENSI_IMPLEMENTASI_04_4.Checked
                .SDIGITALRJ24_273 = txtINTERVENSI_IMPLEMENTASI_04_4_TEXT.Text
                .SDIGITALRJ24_274 = chkINTERVENSI_IMPLEMENTASI_04_5.Checked
                .SDIGITALRJ24_275 = chkINTERVENSI_IMPLEMENTASI_04_6.Checked
                .SDIGITALRJ24_276 = chkINTERVENSI_IMPLEMENTASI_04_7.Checked
                .SDIGITALRJ24_277 = CheckEdit154.Checked
                .SDIGITALRJ24_278 = chkINTERVENSI_IMPLEMENTASI_04_8.Checked
                .SDIGITALRJ24_279 = chkINTERVENSI_IMPLEMENTASI_05_1.Checked
                .SDIGITALRJ24_280 = CheckEdit153.Checked
                .SDIGITALRJ24_281 = chkINTERVENSI_IMPLEMENTASI_05_4.Checked
                .SDIGITALRJ24_282 = chkINTERVENSI_IMPLEMENTASI_05_5.Checked
                .SDIGITALRJ24_283 = chkINTERVENSI_IMPLEMENTASI_05_6.Checked
                .SDIGITALRJ24_284 = txtINTERVENSI_IMPLEMENTASI_05_6_TEXT.Text
                .SDIGITALRJ24_285 = chkHASIL_PENANGANAN_01.Checked
                .SDIGITALRJ24_286 = chkHASIL_PENANGANAN_02.Checked
                .SDIGITALRJ24_287 = chkHASIL_PENANGANAN_03.Checked
                .SDIGITALRJ24_288 = chkHASIL_PENANGANAN_04.Checked
                .SDIGITALRJ24_289 = chkHASIL_PENANGANAN_05.Checked

                .PERAWAT = sUserID

                .DOKTER_KODE = sKODEDOKTER
                .DOKTER_NAMEDISPLAY = sNAMADOKTER

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

                Try
                    .CETAK = oDigital.GetData(txtNoRegister.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                Try
                    If sIsOtority = True Then
                        .KDUSER = oDigital.GetData(txtNoRegister.Text).KDUSER
                        .KDUSER_SIGNATURE = oDigital.GetData(txtNoRegister.Text).KDUSER_SIGNATURE
                    Else
                        .KDUSER = sUserID
                        .KDUSER_SIGNATURE = sUserSIGNATURE
                    End If
                Catch ex As Exception
                    .KDUSER = sUserID
                    .KDUSER_SIGNATURE = sUserSIGNATURE
                End Try

            End With

            ' ***** DETIL *****
            Dim arrDetail = oDigital.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oDigital.GetStructureDetail
                With dsDetail
                    .SEQ = i
                    .KDKUNJUNGAN = ds.KDKUNJUNGAN
                    .CEK1 = grvDetail.GetRowCellValue(i, colCeklis1)
                    '.KDITEMDIAGNOSAPERAWAT = grvDetail.GetRowCellValue(i, colKDITEMDIAGNOSAPERAWAT)
                    .KDITEMDIAGNOSAPERAWAT = IIf(grvDetail.GetRowCellValue(i, colKDITEMDIAGNOSAPERAWAT) Is Nothing, "-", grvDetail.GetRowCellValue(i, colKDITEMDIAGNOSAPERAWAT))
                    .DIAGNOSA_KEPERAWATAN = IIf(grvDetail.GetRowCellValue(i, colDiagnosaKeperawatan) Is Nothing, "", grvDetail.GetRowCellValue(i, colDiagnosaKeperawatan))
                    .CEK2 = grvDetail.GetRowCellValue(i, colCeklis2)
                    .LUARAN = IIf(grvDetail.GetRowCellValue(i, colLuaran) Is Nothing, "", grvDetail.GetRowCellValue(i, colLuaran))
                    .CEK3 = grvDetail.GetRowCellValue(i, colCeklis3)
                    .INTERVENSI = IIf(grvDetail.GetRowCellValue(i, colIntervensi) Is Nothing, "", grvDetail.GetRowCellValue(i, colIntervensi))
                    .KD_PARAF = IIf(grvDetail.GetRowCellValue(i, colKDParaf) Is Nothing, "", grvDetail.GetRowCellValue(i, colKDParaf))
                    .NAMA_PARAF = IIf(grvDetail.GetRowCellValue(i, colKDParaf) Is Nothing, "", fn_LoadSEARCHKDSTAFF(grvDetail.GetRowCellValue(i, colKDParaf)))
                End With
                arrDetail.Add(dsDetail)
            Next


            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oDigital.InsertData(ds,arrDetail)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oDigital.UpdateData(sKDKUNJUNGAN,ds,arrDetail)
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
    Private Sub fn_LoadPerawat()
        Try
            Dim sKoneksiOld As String = String.Empty
            Dim oSetKoneksi As New Setting.clsSetKoneksi
            Dim dsSetKoneksi = oSetKoneksi.GetData()
            If dsSetKoneksi IsNot Nothing Then
                sKoneksiOld = dsSetKoneksi.GENERATE_ECLAIM
            End If

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = sKoneksiOld
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
            SQL &= "AND KELOMPOKIPK = 'NAKES' "
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

            grdParaf.DataSource = ds.Tables("STAFF")
            grdParaf.ValueMember = "KDSTAFF"
            grdParaf.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If


        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub fn_LoadKDITEMDIAGNOSAPERAWAT()
        Try
            Dim ds = From x In oDiagnosaPerawat.GetDataItemAll()
                     Select x.DESCRIPTION, x.KDITEMDIAGNOSAPERAWAT, x.KATEGORI, x.ISACTIVE

            grdKDITEMDIAGNOSAPERAWAT.Properties.DataSource = ds.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDITEMDIAGNOSAPERAWAT.Properties.ValueMember = "KDITEMDIAGNOSAPERAWAT"
            grdKDITEMDIAGNOSAPERAWAT.Properties.DisplayMember = "DESCRIPTION"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Dim sSEQ As Integer = 0
    Dim sCounter As Integer = 0

    Private Sub btnPilihDiagnosa_Click(sender As Object, e As EventArgs) Handles btnPilihDiagnosa.Click
        If grdKDITEMDIAGNOSAPERAWAT.Text = String.Empty Then
            Exit Sub
        End If

        'getkategori
        Dim sKategori As String = grvKDITEMDIAGNOSAPERAWAT.GetFocusedRowCellValue("KATEGORI")
        Dim desc1 As String = String.Empty
        Dim desc2 As String = String.Empty

        If sKategori = "AKTUAL" Then
            desc1 = " b.d :"
            desc2 = "D.d :"
        ElseIf sKategori = "RISIKO" Then
            desc1 = " dibuktikan dengan :"
            desc2 = "Faktor risiko :"
        ElseIf sKategori = "PROMKES" Then
            desc1 = " dibuktikan dengan :"
            desc2 = "Tanda dan gejala :"
        End If

        isLoad = False

        Dim ds = oDigital.GetDataDetailDiagnosa(sKDKUNJUNGAN, grdKDITEMDIAGNOSAPERAWAT.EditValue)

        If ds IsNot Nothing Then
            MsgBox("Diagnosa Sudah Ada dengan nomor Kode " & ds.KDITEMDIAGNOSAPERAWAT, MsgBoxStyle.Exclamation, Me.Text)
        Else
            grvDetail.OptionsSelection.MultiSelect = True

            sSEQ = grvDetail.RowCount() - 1

            'nama diagnosa keperawatan
            grvDetail.Focus()
            grvDetail.AddNewRow()
            grvDetail.SetFocusedRowCellValue(colSEQ, sSEQ)
            grvDetail.SetFocusedRowCellValue(colKDITEMDIAGNOSAPERAWAT, grdKDITEMDIAGNOSAPERAWAT.EditValue)
            grvDetail.SetFocusedRowCellValue(colCeklis1, False)
            grvDetail.SetFocusedRowCellValue(colDiagnosaKeperawatan, grdKDITEMDIAGNOSAPERAWAT.Text & " " & grdKDITEMDIAGNOSAPERAWAT.EditValue & desc1)
            grvDetail.SetFocusedRowCellValue(colCeklis2, False)
            grvDetail.SetFocusedRowCellValue(colLuaran, "Setelah dilakukan perawatan dalam waktu ...  menit")
            grvDetail.SetFocusedRowCellValue(colCeklis3, False)
            grvDetail.SetFocusedRowCellValue(colIntervensi, "")
            grvDetail.SetFocusedRowCellValue(colKDParaf, "")
            sSEQ = sSEQ + 1


            'berhubungan dengan
            For Each xloop In oDigital.GetDataItemDetil(grdKDITEMDIAGNOSAPERAWAT.EditValue)
                If xloop.KDITEMDIAGNOSAPERAWAT <> "" And xloop.KDITEMDIAGNOSAPERAWAT <> "...." Then
                    If xloop.BERHUBUNGANDENGAN <> "" Or xloop.KRITERIA <> "" Or xloop.INTERVENSI <> "" Then
                        grvDetail.Focus()
                        grvDetail.AddNewRow()

                        grvDetail.SetFocusedRowCellValue(colSEQ, sSEQ)
                        grvDetail.SetFocusedRowCellValue(colKDITEMDIAGNOSAPERAWAT, xloop.KDITEMDIAGNOSAPERAWAT)
                        grvDetail.SetFocusedRowCellValue(colCeklis1, xloop.ISCHEKED)
                        grvDetail.SetFocusedRowCellValue(colDiagnosaKeperawatan, xloop.BERHUBUNGANDENGAN)
                        grvDetail.SetFocusedRowCellValue(colCeklis2, xloop.ISCHEKED)
                        grvDetail.SetFocusedRowCellValue(colLuaran, xloop.TUJUAN)
                        grvDetail.SetFocusedRowCellValue(colCeklis3, xloop.ISCHEKED)
                        grvDetail.SetFocusedRowCellValue(colIntervensi, xloop.INTERVENSI)
                        grvDetail.SetFocusedRowCellValue(colKDParaf, "")

                        sSEQ = sSEQ + 1
                    End If
                End If
            Next

            'ditandai dengan
            Dim countGrid As Integer = 0
            For i As Integer = 0 To grvDetail.RowCount() - 1
                If grvDetail.GetRowCellValue(i, colDiagnosaKeperawatan) = "" And grvDetail.GetRowCellValue(i, colKDITEMDIAGNOSAPERAWAT) = grdKDITEMDIAGNOSAPERAWAT.EditValue Then
                    countGrid = i
                    Exit For
                End If
                countGrid = countGrid + 1
            Next

            countGrid = countGrid + 1
            grvDetail.SetRowCellValue(countGrid, colDiagnosaKeperawatan, desc2)
            countGrid = countGrid + 1

            For Each yloop In oDigital.GetDataItemDetil(grdKDITEMDIAGNOSAPERAWAT.EditValue)
                If grvDetail.GetRowCellValue(countGrid, colDiagnosaKeperawatan) = "" And grvDetail.GetRowCellValue(countGrid, colKDITEMDIAGNOSAPERAWAT) = grdKDITEMDIAGNOSAPERAWAT.EditValue Then
                    grvDetail.SetRowCellValue(countGrid, colDiagnosaKeperawatan, yloop.DITANDAIDENGAN)
                End If
                countGrid = countGrid + 1
            Next

            'kriteria
            Dim countGrid3 As Integer = 0
            For i As Integer = 0 To grvDetail.RowCount() - 1
                If grvDetail.GetRowCellValue(i, colLuaran) = "" And grvDetail.GetRowCellValue(i, colKDITEMDIAGNOSAPERAWAT) = grdKDITEMDIAGNOSAPERAWAT.EditValue Then
                    countGrid3 = i
                    Exit For
                End If
                countGrid3 = countGrid3 + 1
            Next

            countGrid3 = countGrid3 + 1
            grvDetail.SetRowCellValue(countGrid3, colLuaran, "Kriteria hasil :")
            countGrid3 = countGrid3 + 1

            For Each yloop In oDigital.GetDataItemDetil(grdKDITEMDIAGNOSAPERAWAT.EditValue)
                If grvDetail.GetRowCellValue(countGrid3, colLuaran) = "" And grvDetail.GetRowCellValue(countGrid3, colKDITEMDIAGNOSAPERAWAT) = grdKDITEMDIAGNOSAPERAWAT.EditValue Then
                    grvDetail.SetRowCellValue(countGrid3, colLuaran, yloop.KRITERIA)
                End If
                countGrid3 = countGrid3 + 1
            Next


            'batas
            grvDetail.Focus()
            grvDetail.AddNewRow()
            grvDetail.SetFocusedRowCellValue(colSEQ, sSEQ)
            grvDetail.SetFocusedRowCellValue(colKDITEMDIAGNOSAPERAWAT, grdKDITEMDIAGNOSAPERAWAT.EditValue)
            grvDetail.SetFocusedRowCellValue(colCeklis1, False)
            grvDetail.SetFocusedRowCellValue(colDiagnosaKeperawatan, "")
            grvDetail.SetFocusedRowCellValue(colCeklis2, False)
            grvDetail.SetFocusedRowCellValue(colLuaran, "")
            grvDetail.SetFocusedRowCellValue(colCeklis3, False)
            grvDetail.SetFocusedRowCellValue(colIntervensi, "")
            grvDetail.SetFocusedRowCellValue(colKDParaf, "")
            sSEQ = sSEQ + 1

        End If

        'grvDetail.BestFitColumns()
        isLoad = True
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click, ContextMenuStrip1.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail.DeleteSelectedRows()
    End Sub

    Private Function fn_LoadSEARCHKDSTAFF(ByVal KDSTAFF As String) As String
        Try
            fn_LoadSEARCHKDSTAFF = ""

            Dim sKoneksiOld As String = String.Empty
            Dim oSetKoneksi As New Setting.clsSetKoneksi
            Dim dsSetKoneksi = oSetKoneksi.GetData()
            If dsSetKoneksi IsNot Nothing Then
                sKoneksiOld = dsSetKoneksi.GENERATE_ECLAIM
            End If

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = sKoneksiOld
            oConn = New SqlConnection(sConn)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "M_STAFF A "
            SQL &= "WHERE "
            SQL &= "A.KDSTAFF = '" & KDSTAFF & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "STAFF")

            For iLoop As Integer = 0 To ds.Tables("STAFF").Rows.Count - 1
                With ds.Tables("STAFF")
                    fn_LoadSEARCHKDSTAFF = .Rows(iLoop)("NAME_DISPLAY")
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            fn_LoadSEARCHKDSTAFF = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function

    Private Sub frmEMedrekRJ_24_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
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

    Private Sub fn_LoadHistoryAsesmen()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\EMEDREK\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "TANGGAL = A.DATE "
            SQL &= ",A.KDKUNJUNGAN "
            SQL &= ",KELUHAN = A.SDIGITALRJ24_04 "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RJ_24 A "
            SQL &= "INNER JOIN R_IDENTITAS_PASIEN B "
            SQL &= "ON A.KDKUNJUNGAN = B.KDKUNJUNGAN "
            SQL &= "WHERE KDCUSTOMER = '" & txtNoPasien.Text & "' "
            SQL &= "ORDER BY A.DATE DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "R_ASKEPJIWA")

            grd_RiwayatAskep.MainView = grv_RiwayatAskep
            grd_RiwayatAskep.DataSource = ds.Tables("R_ASKEPJIWA")
            grd_RiwayatAskep.ForceInitialize()

            'grv_RiwayatAskep.BestFitColumns()
            grv_RiwayatAskep.Columns("KDKUNJUNGAN").Visible = False

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox("Information : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub CopyAsesmenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyAsesmenToolStripMenuItem.Click
        If grv_RiwayatAskep.GetFocusedRowCellValue("KDKUNJUNGAN") Is Nothing Then
            Exit Sub
        End If

        SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

        SplashScreenManager.Default.SetWaitFormCaption("Processing data.....")

        Try
            ' ***** HEADER *****
            Dim ds = oDigital.GetData(grv_RiwayatAskep.GetFocusedRowCellValue("KDKUNJUNGAN"))

            With ds
                chkAUTO.Checked = .SDIGITALRJ24_01
                chkALLO.Checked = .SDIGITALRJ24_02
                txtNAMAPENGKAJIAN.Text = .SDIGITALRJ24_03
                txtKELUHAN_UTAMA.Text = .SDIGITALRJ24_04
                txtKESADARAN_UMUM.Text = .SDIGITALRJ24_05
                txtTANDA_VITAL_01.Text = .SDIGITALRJ24_06
                txtTANDA_VITAL_02.Text = .SDIGITALRJ24_07
                txtTANDA_VITAL_03.Text = .SDIGITALRJ24_08
                txtTANDA_VITAL_04.Text = .SDIGITALRJ24_09
                chkTANDA_VITAL_REGULER.Checked = .SDIGITALRJ24_10
                chkTANDA_VITAL_IREGULER.Checked = .SDIGITALRJ24_11
                txtTANDA_VITAL_05.Text = .SDIGITALRJ24_12
                txtTANDA_VITAL_06.Text = .SDIGITALRJ24_13
                txtTANDA_VITAL_07.Text = .SDIGITALRJ24_14
                txtTANDA_VITAL_08.Text = .SDIGITALRJ24_15
                TextEdit42.Text = .SDIGITALRJ24_16
                CheckEdit2.Checked = .SDIGITALRJ24_17
                CheckEdit1.Checked = .SDIGITALRJ24_18
                TextEdit1.Text = .SDIGITALRJ24_19
                CheckEdit4.Checked = .SDIGITALRJ24_20
                CheckEdit3.Checked = .SDIGITALRJ24_21
                CheckEdit5.Checked = .SDIGITALRJ24_22
                TextEdit2.Text = .SDIGITALRJ24_23
                CheckEdit6.Checked = .SDIGITALRJ24_24
                CheckEdit7.Checked = .SDIGITALRJ24_25
                CheckEdit9.Checked = .SDIGITALRJ24_26
                CheckEdit8.Checked = .SDIGITALRJ24_27
                TextEdit3.Text = .SDIGITALRJ24_28
                TextEdit4.Text = .SDIGITALRJ24_29
                TextEdit5.Text = .SDIGITALRJ24_30
                TextEdit6.Text = .SDIGITALRJ24_31
                CheckEdit12.Checked = .SDIGITALRJ24_32
                TextEdit7.Text = .SDIGITALRJ24_33
                CheckEdit11.Checked = .SDIGITALRJ24_34
                CheckEdit14.Checked = .SDIGITALRJ24_35
                CheckEdit10.Checked = .SDIGITALRJ24_36
                TextEdit8.Text = .SDIGITALRJ24_37
                CheckEdit13.Checked = .SDIGITALRJ24_38
                TextEdit9.Text = .SDIGITALRJ24_39
                CheckEdit15.Checked = .SDIGITALRJ24_40
                TextEdit10.Text = .SDIGITALRJ24_41
                TextEdit11.Text = .SDIGITALRJ24_42
                CheckEdit16.Checked = .SDIGITALRJ24_43
                CheckEdit17.Checked = .SDIGITALRJ24_44
                CheckEdit19.Checked = .SDIGITALRJ24_45
                CheckEdit18.Checked = .SDIGITALRJ24_46
                CheckEdit21.Checked = .SDIGITALRJ24_47
                CheckEdit20.Checked = .SDIGITALRJ24_48
                CheckEdit23.Checked = .SDIGITALRJ24_49
                CheckEdit22.Checked = .SDIGITALRJ24_50
                CheckEdit24.Checked = .SDIGITALRJ24_51
                TextEdit13.Text = .SDIGITALRJ24_52
                TextEdit12.Text = .SDIGITALRJ24_53
                CheckEdit26.Checked = .SDIGITALRJ24_54
                CheckEdit25.Checked = .SDIGITALRJ24_55
                CheckEdit27.Checked = .SDIGITALRJ24_56
                CheckEdit30.Checked = .SDIGITALRJ24_57
                CheckEdit29.Checked = .SDIGITALRJ24_58
                CheckEdit28.Checked = .SDIGITALRJ24_59
                CheckEdit31.Checked = .SDIGITALRJ24_60
                CheckEdit32.Checked = .SDIGITALRJ24_61
                TextEdit14.Text = .SDIGITALRJ24_62
                CheckEdit35.Checked = .SDIGITALRJ24_63
                CheckEdit34.Checked = .SDIGITALRJ24_64
                CheckEdit33.Checked = .SDIGITALRJ24_65
                CheckEdit50.Checked = .SDIGITALRJ24_66
                CheckEdit37.Checked = .SDIGITALRJ24_67
                CheckEdit51.Checked = .SDIGITALRJ24_68
                TextEdit15.Text = .SDIGITALRJ24_69
                CheckEdit54.Checked = .SDIGITALRJ24_70
                CheckEdit53.Checked = .SDIGITALRJ24_71
                CheckEdit52.Checked = .SDIGITALRJ24_72
                CheckEdit36.Checked = .SDIGITALRJ24_73
                TextEdit17.Text = .SDIGITALRJ24_74
                TextEdit18.Text = .SDIGITALRJ24_75
                CheckEdit61.Checked = .SDIGITALRJ24_76
                CheckEdit60.Checked = .SDIGITALRJ24_77
                CheckEdit59.Checked = .SDIGITALRJ24_78
                CheckEdit58.Checked = .SDIGITALRJ24_79
                CheckEdit57.Checked = .SDIGITALRJ24_80
                CheckEdit56.Checked = .SDIGITALRJ24_81
                CheckEdit55.Checked = .SDIGITALRJ24_82
                TextEdit19.Text = .SDIGITALRJ24_83
                CheckEdit68.Checked = .SDIGITALRJ24_84
                CheckEdit67.Checked = .SDIGITALRJ24_85
                CheckEdit66.Checked = .SDIGITALRJ24_86
                CheckEdit65.Checked = .SDIGITALRJ24_87
                CheckEdit62.Checked = .SDIGITALRJ24_88
                TextEdit20.Text = .SDIGITALRJ24_89
                CheckEdit63.Checked = .SDIGITALRJ24_90
                CheckEdit64.Checked = .SDIGITALRJ24_91
                CheckEdit69.Checked = .SDIGITALRJ24_92
                CheckEdit70.Checked = .SDIGITALRJ24_93
                CheckEdit71.Checked = .SDIGITALRJ24_94
                CheckEdit72.Checked = .SDIGITALRJ24_95
                CheckEdit73.Checked = .SDIGITALRJ24_96
                CheckEdit74.Checked = .SDIGITALRJ24_97
                TextEdit16.Text = .SDIGITALRJ24_98
                CheckEdit75.Checked = .SDIGITALRJ24_99
                CheckEdit76.Checked = .SDIGITALRJ24_100
                CheckEdit78.Checked = .SDIGITALRJ24_101
                TextEdit21.Text = .SDIGITALRJ24_102
                CheckEdit77.Checked = .SDIGITALRJ24_103
                CheckEdit79.Checked = .SDIGITALRJ24_104
                CheckEdit80.Checked = .SDIGITALRJ24_105
                TextEdit22.Text = .SDIGITALRJ24_106
                CheckEdit81.Checked = .SDIGITALRJ24_107
                CheckEdit85.Checked = .SDIGITALRJ24_108
                CheckEdit84.Checked = .SDIGITALRJ24_109
                CheckEdit86.Checked = .SDIGITALRJ24_110
                TextEdit24.Text = .SDIGITALRJ24_111
                CheckEdit89.Checked = .SDIGITALRJ24_112
                CheckEdit88.Checked = .SDIGITALRJ24_113
                CheckEdit87.Checked = .SDIGITALRJ24_114
                CheckEdit82.Checked = .SDIGITALRJ24_115
                TextEdit25.Text = .SDIGITALRJ24_116
                CheckEdit90.Checked = .SDIGITALRJ24_117
                CheckEdit91.Checked = .SDIGITALRJ24_118
                CheckEdit94.Checked = .SDIGITALRJ24_119
                CheckEdit93.Checked = .SDIGITALRJ24_120
                CheckEdit92.Checked = .SDIGITALRJ24_121
                TextEdit26.Text = .SDIGITALRJ24_122
                CheckEdit96.Checked = .SDIGITALRJ24_123
                CheckEdit95.Checked = .SDIGITALRJ24_124
                CheckEdit97.Checked = .SDIGITALRJ24_125
                TextEdit27.Text = .SDIGITALRJ24_126
                CheckEdit98.Checked = .SDIGITALRJ24_127
                TextEdit28.Text = .SDIGITALRJ24_128
                CheckEdit100.Checked = .SDIGITALRJ24_129
                CheckEdit99.Checked = .SDIGITALRJ24_130
                TextEdit23.Text = .SDIGITALRJ24_131
                chkSKRINING_GIZI_01_1.Checked = .SDIGITALRJ24_132
                chkSKRINING_GIZI_01_2.Checked = .SDIGITALRJ24_133
                chkSKRINING_GIZI_02_1.Checked = .SDIGITALRJ24_134
                chkSKRINING_GIZI_02_2.Checked = .SDIGITALRJ24_135
                chkSKRINING_GIZI_03_1.Checked = .SDIGITALRJ24_136
                chkSKRINING_GIZI_03_2.Checked = .SDIGITALRJ24_137

                chkSKRINING_RESIKO_JATUH_01_1.Checked = .SDIGITALRJ24_138
                chkSKRINING_RESIKO_JATUH_01_2.Checked = .SDIGITALRJ24_139
                chkSKRINING_RESIKO_JATUH_02_1.Checked = .SDIGITALRJ24_140
                chkSKRINING_RESIKO_JATUH_02_2.Checked = .SDIGITALRJ24_141

                CheckEdit39.Checked = .SDIGITALRJ24_142
                CheckEdit43.Checked = .SDIGITALRJ24_143
                CheckEdit48.Checked = .SDIGITALRJ24_144
                CheckEdit49.Checked = .SDIGITALRJ24_145
                CheckEdit44.Checked = .SDIGITALRJ24_146
                CheckEdit40.Checked = .SDIGITALRJ24_147
                CheckEdit41.Checked = .SDIGITALRJ24_148
                CheckEdit46.Checked = .SDIGITALRJ24_149
                CheckEdit47.Checked = .SDIGITALRJ24_150
                CheckEdit45.Checked = .SDIGITALRJ24_151
                CheckEdit38.Checked = .SDIGITALRJ24_152
                txtDIGITALRJ_79.Checked = .SDIGITALRJ24_153
                txtDIGITALRJ_81.Text = .SDIGITALRJ24_154
                txtDIGITALRJ_83.Text = .SDIGITALRJ24_155
                txtDIGITALRJ_85.Text = .SDIGITALRJ24_156
                txtDIGITALRJ_80.Checked = .SDIGITALRJ24_157
                txtDIGITALRJ_82.Text = .SDIGITALRJ24_158
                txtDIGITALRJ_84.Text = .SDIGITALRJ24_159
                txtDIGITALRJ_86.Text = .SDIGITALRJ24_160
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01.Checked = .SDIGITALRJ24_161
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02.Checked = .SDIGITALRJ24_162
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03.Checked = .SDIGITALRJ24_163
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04.Checked = .SDIGITALRJ24_164
                txtKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN.Text = .SDIGITALRJ24_165
                chkEDUKASI_01.Checked = .SDIGITALRJ24_166
                chkEDUKASI_02.Checked = .SDIGITALRJ24_167
                chkEDUKASI_03.Checked = .SDIGITALRJ24_168
                txtEDUKASI_03_TEXT.Text = .SDIGITALRJ24_169
                chkBICARA_SEHARI2_01.Checked = .SDIGITALRJ24_170
                chkBICARA_SEHARI2_01_1.Checked = .SDIGITALRJ24_171
                chkBICARA_SEHARI2_01_2.Checked = .SDIGITALRJ24_172
                chkBICARA_SEHARI2_02.Checked = .SDIGITALRJ24_173
                chkBICARA_SEHARI2_02_1.Checked = .SDIGITALRJ24_174
                chkBICARA_SEHARI2_02_2.Checked = .SDIGITALRJ24_175
                chkBICARA_SEHARI2_03.Checked = .SDIGITALRJ24_176
                txtBICARA_SEHARI2_03_TEXT.Text = .SDIGITALRJ24_177
                chkBICARA_SEHARI2_04.Checked = .SDIGITALRJ24_178
                txtBICARA_SEHARI2_04_TEXT.Text = .SDIGITALRJ24_179
                chkPERLU_PENERJEMAH_01.Checked = .SDIGITALRJ24_180
                chkPERLU_PENERJEMAH_02.Checked = .SDIGITALRJ24_181
                chkBAHASA_ISYARAT_01.Checked = .SDIGITALRJ24_182
                chkBAHASA_ISYARAT_02.Checked = .SDIGITALRJ24_183
                chkHAMBATAN_EDUKASI_01.Checked = .SDIGITALRJ24_184
                chkHAMBATAN_EDUKASI_02.Checked = .SDIGITALRJ24_185
                chkHAMBATAN_EDUKASI_03.Checked = .SDIGITALRJ24_186
                chkHAMBATAN_EDUKASI_04.Checked = .SDIGITALRJ24_187
                chkHAMBATAN_EDUKASI_05.Checked = .SDIGITALRJ24_188
                chkHAMBATAN_EDUKASI_06.Checked = .SDIGITALRJ24_189
                chkHAMBATAN_EDUKASI_07.Checked = .SDIGITALRJ24_190
                chkHAMBATAN_EDUKASI_08.Checked = .SDIGITALRJ24_191
                chkHAMBATAN_EDUKASI_09.Checked = .SDIGITALRJ24_192
                chkHAMBATAN_EDUKASI_10.Checked = .SDIGITALRJ24_193
                chkHAMBATAN_EDUKASI_11.Checked = .SDIGITALRJ24_194
                chkHAMBATAN_EDUKASI_12.Checked = .SDIGITALRJ24_195
                CheckEdit83.Checked = .SDIGITALRJ24_196
                CheckEdit101.Checked = .SDIGITALRJ24_197
                CheckEdit103.Checked = .SDIGITALRJ24_198
                CheckEdit102.Checked = .SDIGITALRJ24_199
                CheckEdit107.Checked = .SDIGITALRJ24_200
                CheckEdit106.Checked = .SDIGITALRJ24_201
                CheckEdit105.Checked = .SDIGITALRJ24_202
                CheckEdit104.Checked = .SDIGITALRJ24_203
                CheckEdit108.Checked = .SDIGITALRJ24_204
                TextEdit29.Text = .SDIGITALRJ24_205
                CheckEdit117.Checked = .SDIGITALRJ24_206
                CheckEdit116.Checked = .SDIGITALRJ24_207
                CheckEdit115.Checked = .SDIGITALRJ24_208
                CheckEdit114.Checked = .SDIGITALRJ24_209
                CheckEdit109.Checked = .SDIGITALRJ24_210
                TextEdit30.Text = .SDIGITALRJ24_211
                CheckEdit118.Checked = .SDIGITALRJ24_212
                CheckEdit113.Checked = .SDIGITALRJ24_213
                CheckEdit112.Checked = .SDIGITALRJ24_214
                CheckEdit110.Checked = .SDIGITALRJ24_215
                TextEdit31.Text = .SDIGITALRJ24_216
                CheckEdit121.Checked = .SDIGITALRJ24_217
                CheckEdit119.Checked = .SDIGITALRJ24_218
                CheckEdit120.Checked = .SDIGITALRJ24_219
                CheckEdit124.Checked = .SDIGITALRJ24_220
                CheckEdit123.Checked = .SDIGITALRJ24_221
                CheckEdit122.Checked = .SDIGITALRJ24_222
                CheckEdit126.Checked = .SDIGITALRJ24_223
                CheckEdit125.Checked = .SDIGITALRJ24_224
                CheckEdit111.Checked = .SDIGITALRJ24_225
                TextEdit32.Text = .SDIGITALRJ24_226
                CheckEdit135.Checked = .SDIGITALRJ24_227
                CheckEdit134.Checked = .SDIGITALRJ24_228
                CheckEdit133.Checked = .SDIGITALRJ24_229
                CheckEdit132.Checked = .SDIGITALRJ24_230
                CheckEdit131.Checked = .SDIGITALRJ24_231
                CheckEdit130.Checked = .SDIGITALRJ24_232
                CheckEdit129.Checked = .SDIGITALRJ24_233
                CheckEdit127.Checked = .SDIGITALRJ24_234
                TextEdit33.Text = .SDIGITALRJ24_235
                CheckEdit139.Checked = .SDIGITALRJ24_236
                CheckEdit138.Checked = .SDIGITALRJ24_237
                CheckEdit137.Checked = .SDIGITALRJ24_238
                TextEdit34.Text = .SDIGITALRJ24_239
                TextEdit36.Text = .SDIGITALRJ24_240
                TextEdit35.Text = .SDIGITALRJ24_241
                txtCATATAN_KEPERAWATAN_JAM.Text = .SDIGITALRJ24_242
                CheckEdit145.Checked = .SDIGITALRJ24_243
                TextEdit38.Text = .SDIGITALRJ24_244
                TextEdit39.Text = .SDIGITALRJ24_245
                TextEdit40.Text = .SDIGITALRJ24_246
                TextEdit41.Text = .SDIGITALRJ24_247
                CheckEdit140.Checked = .SDIGITALRJ24_248
                CheckEdit141.Checked = .SDIGITALRJ24_249
                CheckEdit144.Checked = .SDIGITALRJ24_250
                CheckEdit143.Checked = .SDIGITALRJ24_251
                CheckEdit142.Checked = .SDIGITALRJ24_252
                CheckEdit136.Checked = .SDIGITALRJ24_253
                TextEdit37.Text = .SDIGITALRJ24_254
                TextEdit43.Text = .SDIGITALRJ24_255
                CheckEdit146.Checked = .SDIGITALRJ24_256
                CheckEdit147.Checked = .SDIGITALRJ24_257
                CheckEdit151.Checked = .SDIGITALRJ24_258
                CheckEdit148.Checked = .SDIGITALRJ24_259
                CheckEdit149.Checked = .SDIGITALRJ24_260
                CheckEdit150.Checked = .SDIGITALRJ24_261
                CheckEdit152.Checked = .SDIGITALRJ24_262
                chkINTERVENSI_IMPLEMENTASI_03_2.Checked = .SDIGITALRJ24_263
                chkINTERVENSI_IMPLEMENTASI_03_3.Checked = .SDIGITALRJ24_264
                chkINTERVENSI_IMPLEMENTASI_03_4.Checked = .SDIGITALRJ24_265
                chkINTERVENSI_IMPLEMENTASI_04_1.Checked = .SDIGITALRJ24_266
                chkINTERVENSI_IMPLEMENTASI_03_6.Checked = .SDIGITALRJ24_267
                txtINTERVENSI_IMPLEMENTASI_03_6_TEXT.Text = .SDIGITALRJ24_268
                chkINTERVENSI_IMPLEMENTASI_04_2.Checked = .SDIGITALRJ24_269
                CheckEdit128.Checked = .SDIGITALRJ24_270
                chkINTERVENSI_IMPLEMENTASI_04_3.Checked = .SDIGITALRJ24_271
                chkINTERVENSI_IMPLEMENTASI_04_4.Checked = .SDIGITALRJ24_272
                txtINTERVENSI_IMPLEMENTASI_04_4_TEXT.Text = .SDIGITALRJ24_273
                chkINTERVENSI_IMPLEMENTASI_04_5.Checked = .SDIGITALRJ24_274
                chkINTERVENSI_IMPLEMENTASI_04_6.Checked = .SDIGITALRJ24_275
                chkINTERVENSI_IMPLEMENTASI_04_7.Checked = .SDIGITALRJ24_276
                CheckEdit154.Checked = .SDIGITALRJ24_277
                chkINTERVENSI_IMPLEMENTASI_04_8.Checked = .SDIGITALRJ24_278
                chkINTERVENSI_IMPLEMENTASI_05_1.Checked = .SDIGITALRJ24_279
                CheckEdit153.Checked = .SDIGITALRJ24_280
                chkINTERVENSI_IMPLEMENTASI_05_4.Checked = .SDIGITALRJ24_281
                chkINTERVENSI_IMPLEMENTASI_05_5.Checked = .SDIGITALRJ24_282
                chkINTERVENSI_IMPLEMENTASI_05_6.Checked = .SDIGITALRJ24_283
                txtINTERVENSI_IMPLEMENTASI_05_6_TEXT.Text = .SDIGITALRJ24_284
                chkHASIL_PENANGANAN_01.Checked = .SDIGITALRJ24_285
                chkHASIL_PENANGANAN_02.Checked = .SDIGITALRJ24_286
                chkHASIL_PENANGANAN_03.Checked = .SDIGITALRJ24_287
                chkHASIL_PENANGANAN_04.Checked = .SDIGITALRJ24_288
                chkHASIL_PENANGANAN_05.Checked = .SDIGITALRJ24_289

                grdPerawat.EditValue = .PERAWAT

                deDATE.DateTime = .DATE

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

                BindingSource1.DataSource = oDigital.GetDataDetail.Where(Function(x) x.KDKUNJUNGAN = sKDKUNJUNGAN).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = BindingSource1
                grvDetail.OptionsSelection.MultiSelect = True

            End With
            
            SplashScreenManager.CloseForm(False)

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class