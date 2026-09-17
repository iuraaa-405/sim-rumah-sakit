Imports System.Data.SqlClient
Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Drawing.Drawing2D

Public Class frmAskepGeriatri12
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_ASKEPGERIATRI_1_2 As New Digital.clsDigital_AskepGeriatri_1_2
    Private sKDPENDAFTARAN As string
    Private sKDUSER As String
    Private sKDUSERSIGNATURE As String
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String)
        oFormMode = FormMode

        Dim oPendaftaran As New Identitas.clsIdentitasPasien
        Dim dsPendaftaran = oPendaftaran.GetData(KDREG)

        txtNoRegister.Text = KDREG

        If dsPendaftaran IsNot Nothing Then
            txtNoPasien.Text = dsPendaftaran.KDCUSTOMER
            txtNamaPasien.Text = dsPendaftaran.NAMAPASIEN.ToString.Trim.ToUpper
            txtUmur.Text = dsPendaftaran.USIA
            txtTanggalDaftar.Text = dsPendaftaran.DATE.ToString("dd-MM-yyyy HH:mm:ss")
            txtNoRegister.Text = dsPendaftaran.KDKUNJUNGAN
            txtTujuan.Text = dsPendaftaran.TUJUAN
            txtJenisKelamin.Text = dsPendaftaran.JENISKELAMIN
            sKDPENDAFTARAN = dsPendaftaran.KDPENDAFTARAN
            deDATE.DateTime = Now

            sKDUSER = sUserID
            sKDUSERSIGNATURE = sUserSIGNATURE
        Else
            txtNoPasien.ResetText()
            txtNamaPasien.ResetText()
            txtUmur.ResetText()
            txtTanggalDaftar.ResetText()
            txtNoRegister.ResetText()
            txtTujuan.ResetText()
            txtJenisKelamin.ResetText()
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
        fn_LoadAsessmenRawatJalan(txtNoRegister.Text)
        fn_LoadDataHistory(txtNoPasien.Text)
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

        deDATE.Properties.ReadOnly = Status

        txtKEADAANUMUM.Properties.ReadOnly = Status
        txtKESADARAN.Properties.ReadOnly = Status
        txtTINGGIBADAN.Properties.ReadOnly = Status
        txtTINGGIBADANLUTUT.Properties.ReadOnly = Status
        txtBERATBADAN.Properties.ReadOnly = Status
        txtTD.Properties.ReadOnly = Status
        txtPERNAPASAN.Properties.ReadOnly = Status
        txtNADI.Properties.ReadOnly = Status
        txtSUHU.Properties.ReadOnly = Status
        txtSATURASI.Properties.ReadOnly = Status
        chkSKALANYERI_00.Properties.ReadOnly = Status
        chkSKALANYERI_01.Properties.ReadOnly = Status
        chkSKALANYERI_02.Properties.ReadOnly = Status
        chkSKALANYERI_03.Properties.ReadOnly = Status
        chkSKALANYERI_04.Properties.ReadOnly = Status
        chkSKALANYERI_05.Properties.ReadOnly = Status
        chkSKALANYERI_06.Properties.ReadOnly = Status
        chkSKALANYERI_07.Properties.ReadOnly = Status
        chkSKALANYERI_08.Properties.ReadOnly = Status
        chkSKALANYERI_09.Properties.ReadOnly = Status
        chkSKALANYERI_10.Properties.ReadOnly = Status
        CheckEdit1.Properties.ReadOnly = Status
        CheckEdit2.Properties.ReadOnly = Status
        CheckEdit3.Properties.ReadOnly = Status
        'TextEdit1.Properties.ReadOnly = Status
        CheckEdit4.Properties.ReadOnly = Status
        CheckEdit5.Properties.ReadOnly = Status
        CheckEdit6.Properties.ReadOnly = Status
        CheckEdit7.Properties.ReadOnly = Status
        CheckEdit8.Properties.ReadOnly = Status
        CheckEdit9.Properties.ReadOnly = Status
        CheckEdit10.Properties.ReadOnly = Status
        CheckEdit11.Properties.ReadOnly = Status
        CheckEdit12.Properties.ReadOnly = Status
        CheckEdit13.Properties.ReadOnly = Status
        CheckEdit14.Properties.ReadOnly = Status
        CheckEdit15.Properties.ReadOnly = Status
        TextEdit2.Properties.ReadOnly = Status
        CheckEdit16.Properties.ReadOnly = Status
        CheckEdit17.Properties.ReadOnly = Status
        TextEdit3.Properties.ReadOnly = Status
        TextEdit4.Properties.ReadOnly = Status
        TextEdit5.Properties.ReadOnly = Status
        CheckEdit18.Properties.ReadOnly = Status
        CheckEdit19.Properties.ReadOnly = Status
        CheckEdit20.Properties.ReadOnly = Status
        CheckEdit21.Properties.ReadOnly = Status
        CheckEdit22.Properties.ReadOnly = Status
        TextEdit6.Properties.ReadOnly = Status
        CheckEdit23.Properties.ReadOnly = Status
        TextEdit7.Properties.ReadOnly = Status
        CheckEdit24.Properties.ReadOnly = Status
        CheckEdit25.Properties.ReadOnly = Status
        CheckEdit26.Properties.ReadOnly = Status
        CheckEdit27.Properties.ReadOnly = Status
        CheckEdit28.Properties.ReadOnly = Status
        CheckEdit29.Properties.ReadOnly = Status
        CheckEdit31.Properties.ReadOnly = Status
        CheckEdit30.Properties.ReadOnly = Status
        CheckEdit32.Properties.ReadOnly = Status
        CheckEdit33.Properties.ReadOnly = Status
        CheckEdit34.Properties.ReadOnly = Status
        CheckEdit35.Properties.ReadOnly = Status
        CheckEdit36.Properties.ReadOnly = Status
        txtKELUHANUTAMA.Properties.ReadOnly = Status
        CheckEdit37.Properties.ReadOnly = Status
        CheckEdit38.Properties.ReadOnly = Status
        CheckEdit39.Properties.ReadOnly = Status
        CheckEdit40.Properties.ReadOnly = Status
        CheckEdit41.Properties.ReadOnly = Status
        TextEdit8.Properties.ReadOnly = Status
        CheckEdit43.Properties.ReadOnly = Status
        CheckEdit44.Properties.ReadOnly = Status
        CheckEdit45.Properties.ReadOnly = Status
        CheckEdit46.Properties.ReadOnly = Status
        CheckEdit47.Properties.ReadOnly = Status
        TextEdit9.Properties.ReadOnly = Status
        CheckEdit48.Properties.ReadOnly = Status
        CheckEdit49.Properties.ReadOnly = Status
        TextEdit10.Properties.ReadOnly = Status
        CheckEdit51.Properties.ReadOnly = Status
        CheckEdit52.Properties.ReadOnly = Status
        CheckEdit50.Properties.ReadOnly = Status
        TextEdit11.Properties.ReadOnly = Status
        CheckEdit54.Properties.ReadOnly = Status
        CheckEdit53.Properties.ReadOnly = Status
        TextEdit12.Properties.ReadOnly = Status
        CheckEdit55.Properties.ReadOnly = Status
        CheckEdit56.Properties.ReadOnly = Status
        CheckEdit57.Properties.ReadOnly = Status
        CheckEdit58.Properties.ReadOnly = Status
        TextEdit13.Properties.ReadOnly = Status
        CheckEdit60.Properties.ReadOnly = Status
        CheckEdit61.Properties.ReadOnly = Status
        CheckEdit62.Properties.ReadOnly = Status
        CheckEdit63.Properties.ReadOnly = Status
        CheckEdit59.Properties.ReadOnly = Status
        CheckEdit64.Properties.ReadOnly = Status
        CheckEdit65.Properties.ReadOnly = Status
        TextEdit14.Properties.ReadOnly = Status
        CheckEdit66.Properties.ReadOnly = Status
        CheckEdit67.Properties.ReadOnly = Status
        CheckEdit68.Properties.ReadOnly = Status
        CheckEdit69.Properties.ReadOnly = Status
        CheckEdit70.Properties.ReadOnly = Status
        CheckEdit71.Properties.ReadOnly = Status
        TextEdit15.Properties.ReadOnly = Status
        CheckEdit72.Properties.ReadOnly = Status
        CheckEdit73.Properties.ReadOnly = Status
        CheckEdit74.Properties.ReadOnly = Status
        CheckEdit75.Properties.ReadOnly = Status
        CheckEdit76.Properties.ReadOnly = Status
        CheckEdit77.Properties.ReadOnly = Status
        CheckEdit78.Properties.ReadOnly = Status
        chkHAID_YA.Properties.ReadOnly = Status
        chkHAID_TIDAK.Properties.ReadOnly = Status
        CheckEdit79.Properties.ReadOnly = Status
        CheckEdit80.Properties.ReadOnly = Status
        CheckEdit81.Properties.ReadOnly = Status
        CheckEdit82.Properties.ReadOnly = Status
        TextEdit16.Properties.ReadOnly = Status
        CheckEdit83.Properties.ReadOnly = Status
        CheckEdit84.Properties.ReadOnly = Status
        CheckEdit85.Properties.ReadOnly = Status
        CheckEdit86.Properties.ReadOnly = Status
        chkPRODUKSI_ADA.Properties.ReadOnly = Status
        chkPRODUKSI_TIDAK.Properties.ReadOnly = Status
        CheckEdit87.Properties.ReadOnly = Status
        TextEdit17.Properties.ReadOnly = Status
        CheckEdit88.Properties.ReadOnly = Status
        CheckEdit89.Properties.ReadOnly = Status
        CheckEdit90.Properties.ReadOnly = Status
        CheckEdit91.Properties.ReadOnly = Status
        TextEdit18.Properties.ReadOnly = Status
        CheckEdit92.Properties.ReadOnly = Status
        CheckEdit93.Properties.ReadOnly = Status
        CheckEdit94.Properties.ReadOnly = Status
        CheckEdit95.Properties.ReadOnly = Status
        CheckEdit96.Properties.ReadOnly = Status
        CheckEdit97.Properties.ReadOnly = Status
        TextEdit19.Properties.ReadOnly = Status
        CheckEdit100.Properties.ReadOnly = Status
        CheckEdit101.Properties.ReadOnly = Status
        CheckEdit102.Properties.ReadOnly = Status
        CheckEdit103.Properties.ReadOnly = Status
        CheckEdit104.Properties.ReadOnly = Status
        CheckEdit105.Properties.ReadOnly = Status
        TextEdit20.Properties.ReadOnly = Status
        CheckEdit106.Properties.ReadOnly = Status
        TextEdit21.Properties.ReadOnly = Status
        CheckEdit107.Properties.ReadOnly = Status
        CheckEdit108.Properties.ReadOnly = Status
        CheckEdit109.Properties.ReadOnly = Status
        CheckEdit110.Properties.ReadOnly = Status
        CheckEdit111.Properties.ReadOnly = Status
        CheckEdit112.Properties.ReadOnly = Status
        CheckEdit113.Properties.ReadOnly = Status
        CheckEdit114.Properties.ReadOnly = Status
        CheckEdit115.Properties.ReadOnly = Status
        CheckEdit116.Properties.ReadOnly = Status
        TextEdit22.Properties.ReadOnly = Status
        CheckEdit117.Properties.ReadOnly = Status
        CheckEdit118.Properties.ReadOnly = Status
        CheckEdit119.Properties.ReadOnly = Status
        CheckEdit120.Properties.ReadOnly = Status
        CheckEdit42.Properties.ReadOnly = Status
        CheckEdit121.Properties.ReadOnly = Status
        CheckEdit122.Properties.ReadOnly = Status
        CheckEdit123.Properties.ReadOnly = Status
        TextEdit23.Properties.ReadOnly = Status
        CheckEdit124.Properties.ReadOnly = Status
        CheckEdit125.Properties.ReadOnly = Status
        CheckEdit126.Properties.ReadOnly = Status
        CheckEdit127.Properties.ReadOnly = Status
        CheckEdit128.Properties.ReadOnly = Status
        CheckEdit129.Properties.ReadOnly = Status
        CheckEdit130.Properties.ReadOnly = Status
        CheckEdit131.Properties.ReadOnly = Status
        CheckEdit132.Properties.ReadOnly = Status
        CheckEdit133.Properties.ReadOnly = Status
        CheckEdit134.Properties.ReadOnly = Status
        CheckEdit135.Properties.ReadOnly = Status
        CheckEdit136.Properties.ReadOnly = Status
        CheckEdit137.Properties.ReadOnly = Status
        CheckEdit138.Properties.ReadOnly = Status
        CheckEdit139.Properties.ReadOnly = Status
        CheckEdit140.Properties.ReadOnly = Status
        CheckEdit141.Properties.ReadOnly = Status
        CheckEdit142.Properties.ReadOnly = Status
        TextEdit24.Properties.ReadOnly = Status
        CheckEdit143.Properties.ReadOnly = Status
        CheckEdit144.Properties.ReadOnly = Status
        CheckEdit145.Properties.ReadOnly = Status
        CheckEdit146.Properties.ReadOnly = Status
        CheckEdit147.Properties.ReadOnly = Status
        CheckEdit148.Properties.ReadOnly = Status
        CheckEdit149.Properties.ReadOnly = Status
        CheckEdit150.Properties.ReadOnly = Status
        CheckEdit151.Properties.ReadOnly = Status
        CheckEdit152.Properties.ReadOnly = Status
        CheckEdit153.Properties.ReadOnly = Status
        CheckEdit154.Properties.ReadOnly = Status
        CheckEdit155.Properties.ReadOnly = Status
        CheckEdit156.Properties.ReadOnly = Status
        CheckEdit157.Properties.ReadOnly = Status
        CheckEdit158.Properties.ReadOnly = Status
        CheckEdit159.Properties.ReadOnly = Status
        CheckEdit160.Properties.ReadOnly = Status
        CheckEdit161.Properties.ReadOnly = Status
        TextEdit25.Properties.ReadOnly = Status
        CheckEdit162.Properties.ReadOnly = Status
        CheckEdit163.Properties.ReadOnly = Status
        CheckEdit164.Properties.ReadOnly = Status
        CheckEdit165.Properties.ReadOnly = Status
        CheckEdit166.Properties.ReadOnly = Status
        CheckEdit167.Properties.ReadOnly = Status
        MemoEdit1.Properties.ReadOnly = Status
        chkREGULER.Properties.ReadOnly = Status
        chkIREGULER.Properties.ReadOnly = Status

        TextEdit1.Properties.ReadOnly = Status
        chk1_0.Properties.ReadOnly = Status
        chk1_2.Properties.ReadOnly = Status
        chk1_YA.Properties.ReadOnly = Status
        chk1_YA_1.Properties.ReadOnly = Status
        chk1_YA_2.Properties.ReadOnly = Status
        chk1_YA_3.Properties.ReadOnly = Status
        chk1_YA_4.Properties.ReadOnly = Status
        txtSkor1.Properties.ReadOnly = Status
        chk2_0.Properties.ReadOnly = Status
        chk2_1.Properties.ReadOnly = Status
        txtSkor2.Properties.ReadOnly = Status
        chk3_YA.Properties.ReadOnly = Status
        chk3_DM.Properties.ReadOnly = Status
        chk3_Kemoterapi.Properties.ReadOnly = Status
        chk3_HD.Properties.ReadOnly = Status
        chk3_Imun.Properties.ReadOnly = Status
        txt3.Properties.ReadOnly = Status
        txtSkor3.Properties.ReadOnly = Status
        txtTOTALSKOR.Properties.ReadOnly = Status
        txtskorBraden.Properties.ReadOnly = Status
        chkResikoDekubitus_YA.Properties.ReadOnly = Status
        chkResikoDekubitus_TIDAK.Properties.ReadOnly = Status
        chkTerdapatLuka_YA.Properties.ReadOnly = Status
        chkTerdapatLuka_TIDAK.Properties.ReadOnly = Status
        chkResikoTinggi.Properties.ReadOnly = Status
        chkResikoSedang.Properties.ReadOnly = Status
        chkResikoRendah.Properties.ReadOnly = Status
        MemoEdit2.Properties.ReadOnly = Status
        chkPresepsiSensori_1.Properties.ReadOnly = Status
        chkPresepsiSensori_2.Properties.ReadOnly = Status
        chkPresepsiSensori_3.Properties.ReadOnly = Status
        chkPresepsiSensori_4.Properties.ReadOnly = Status
        txtSkorPersepsi.Properties.ReadOnly = Status
        chkKelembaban_1.Properties.ReadOnly = Status
        chkKelembaban_2.Properties.ReadOnly = Status
        chkKelembaban_3.Properties.ReadOnly = Status
        chkKelembaban_4.Properties.ReadOnly = Status
        txtSkorKelembaban.Properties.ReadOnly = Status
        chkAktivitas_1.Properties.ReadOnly = Status
        chkAktivitas_2.Properties.ReadOnly = Status
        chkAktivitas_3.Properties.ReadOnly = Status
        chkAktivitas_4.Properties.ReadOnly = Status
        txtSkorAktivitas.Properties.ReadOnly = Status
        chkMobilitas_1.Properties.ReadOnly = Status
        chkMobilitas_2.Properties.ReadOnly = Status
        chkMobilitas_3.Properties.ReadOnly = Status
        chkMobilitas_4.Properties.ReadOnly = Status
        txtSkorMobilitas.Properties.ReadOnly = Status
        chkNutrisi_1.Properties.ReadOnly = Status
        chkNutrisi_2.Properties.ReadOnly = Status
        chkNutrisi_3.Properties.ReadOnly = Status
        chkNutrisi_4.Properties.ReadOnly = Status
        txtSkorNutrisi.Properties.ReadOnly = Status
        chkGesekan_1.Properties.ReadOnly = Status
        chkGesekan_2.Properties.ReadOnly = Status
        chkGesekan_3.Properties.ReadOnly = Status
        txtSkorGesekan.Properties.ReadOnly = Status

        'tambahan
        CheckEdit98.Properties.ReadOnly = Status
        CheckEdit99.Properties.ReadOnly = Status
        CheckEdit168.Properties.ReadOnly = Status
        CheckEdit169.Properties.ReadOnly = Status
        CheckEdit170.Properties.ReadOnly = Status
        CheckEdit171.Properties.ReadOnly = Status
        CheckEdit174.Properties.ReadOnly = Status
        CheckEdit180.Properties.ReadOnly = Status
        CheckEdit181.Properties.ReadOnly = Status

    End Sub
    Private Sub fn_EmptyMe()
        deDATE.DateTime = Now

        txtKEADAANUMUM.ResetText()
        txtKESADARAN.ResetText()
        txtTINGGIBADAN.ResetText()
        txtTINGGIBADANLUTUT.ResetText()
        txtBERATBADAN.ResetText()
        txtTD.ResetText()
        txtPERNAPASAN.ResetText()
        txtNADI.ResetText()
        txtSUHU.ResetText()
        txtSATURASI.ResetText()
        chkSKALANYERI_00.Checked = False
        chkSKALANYERI_01.Checked = False
        chkSKALANYERI_02.Checked = False
        chkSKALANYERI_03.Checked = False
        chkSKALANYERI_04.Checked = False
        chkSKALANYERI_05.Checked = False
        chkSKALANYERI_06.Checked = False
        chkSKALANYERI_07.Checked = False
        chkSKALANYERI_08.Checked = False
        chkSKALANYERI_09.Checked = False
        chkSKALANYERI_10.Checked = False
        CheckEdit1.Checked = False
        CheckEdit2.Checked = False
        CheckEdit3.Checked = False
        'TextEdit1.ResetText()
        CheckEdit4.Checked = False
        CheckEdit5.Checked = False
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
        TextEdit2.ResetText()
        CheckEdit16.Checked = False
        CheckEdit17.Checked = False
        TextEdit3.Text = "                                       (NRS/WBFS)"
        TextEdit4.ResetText()
        TextEdit5.ResetText()
        CheckEdit18.Checked = False
        CheckEdit19.Checked = False
        CheckEdit20.Checked = False
        CheckEdit21.Checked = False
        CheckEdit22.Checked = False
        TextEdit6.ResetText()
        CheckEdit23.Checked = False
        TextEdit7.ResetText()
        CheckEdit24.Checked = False
        CheckEdit25.Checked = False
        CheckEdit26.Checked = False
        CheckEdit27.Checked = False
        CheckEdit28.Checked = False
        CheckEdit29.Checked = False
        CheckEdit31.Checked = False
        CheckEdit30.Checked = False
        CheckEdit32.Checked = False
        CheckEdit33.Checked = False
        CheckEdit34.Checked = False
        CheckEdit35.Checked = False
        CheckEdit36.Checked = False
        txtKELUHANUTAMA.ResetText()
        CheckEdit37.Checked = False
        CheckEdit38.Checked = False
        CheckEdit39.Checked = False
        CheckEdit40.Checked = False
        CheckEdit41.Checked = False
        TextEdit8.ResetText()
        CheckEdit43.Checked = False
        CheckEdit44.Checked = False
        CheckEdit45.Checked = False
        CheckEdit46.Checked = False
        CheckEdit47.Checked = False
        TextEdit9.ResetText()
        CheckEdit48.Checked = False
        CheckEdit49.Checked = False
        TextEdit10.ResetText()
        CheckEdit51.Checked = False
        CheckEdit52.Checked = False
        CheckEdit50.Checked = False
        TextEdit11.ResetText()
        CheckEdit54.Checked = False
        CheckEdit53.Checked = False
        TextEdit12.ResetText()
        CheckEdit55.Checked = False
        CheckEdit56.Checked = False
        CheckEdit57.Checked = False
        CheckEdit58.Checked = False
        TextEdit13.ResetText()
        CheckEdit60.Checked = False
        CheckEdit61.Checked = False
        CheckEdit62.Checked = False
        CheckEdit63.Checked = False
        CheckEdit59.Checked = False
        CheckEdit64.Checked = False
        CheckEdit65.Checked = False
        TextEdit14.ResetText()
        CheckEdit66.Checked = False
        CheckEdit67.Checked = False
        CheckEdit68.Checked = False
        CheckEdit69.Checked = False
        CheckEdit70.Checked = False
        CheckEdit71.Checked = False
        TextEdit15.ResetText()
        CheckEdit72.Checked = False
        CheckEdit73.Checked = False
        CheckEdit74.Checked = False
        CheckEdit75.Checked = False
        CheckEdit76.Checked = False
        CheckEdit77.Checked = False
        CheckEdit78.Checked = False
        chkHAID_YA.Checked = False
        chkHAID_TIDAK.Checked = False
        CheckEdit79.Checked = False
        CheckEdit80.Checked = False
        CheckEdit81.Checked = False
        CheckEdit82.Checked = False
        TextEdit16.ResetText()
        CheckEdit83.Checked = False
        CheckEdit84.Checked = False
        CheckEdit85.Checked = False
        CheckEdit86.Checked = False
        chkPRODUKSI_ADA.Checked = False
        chkPRODUKSI_TIDAK.Checked = False
        CheckEdit87.Checked = False
        TextEdit17.ResetText()
        CheckEdit88.Checked = False
        CheckEdit89.Checked = False
        CheckEdit90.Checked = False
        CheckEdit91.Checked = False
        TextEdit18.ResetText()
        CheckEdit92.Checked = False
        CheckEdit93.Checked = False
        CheckEdit94.Checked = False
        CheckEdit95.Checked = False
        CheckEdit96.Checked = False
        CheckEdit97.Checked = False
        TextEdit19.ResetText()
        CheckEdit100.Checked = False
        CheckEdit101.Checked = False
        CheckEdit102.Checked = False
        CheckEdit103.Checked = False
        CheckEdit104.Checked = False
        CheckEdit105.Checked = False
        TextEdit20.ResetText()
        CheckEdit106.Checked = False
        TextEdit21.ResetText()
        CheckEdit107.Checked = False
        CheckEdit108.Checked = False
        CheckEdit109.Checked = False
        CheckEdit110.Checked = False
        CheckEdit111.Checked = False
        CheckEdit112.Checked = False
        CheckEdit113.Checked = False
        CheckEdit114.Checked = False
        CheckEdit115.Checked = False
        CheckEdit116.Checked = False
        TextEdit22.ResetText()
        CheckEdit117.Checked = False
        CheckEdit118.Checked = False
        CheckEdit119.Checked = False
        CheckEdit120.Checked = False
        CheckEdit42.Checked = False
        CheckEdit121.Checked = False
        CheckEdit122.Checked = False
        CheckEdit123.Checked = False
        TextEdit23.ResetText()
        CheckEdit124.Checked = False
        CheckEdit125.Checked = False
        CheckEdit126.Checked = False
        CheckEdit127.Checked = False
        CheckEdit128.Checked = False
        CheckEdit129.Checked = False
        CheckEdit130.Checked = False
        CheckEdit131.Checked = False
        CheckEdit132.Checked = False
        CheckEdit133.Checked = False
        CheckEdit134.Checked = False
        CheckEdit135.Checked = False
        CheckEdit136.Checked = False
        CheckEdit137.Checked = False
        CheckEdit138.Checked = False
        CheckEdit139.Checked = False
        CheckEdit140.Checked = False
        CheckEdit141.Checked = False
        CheckEdit142.Checked = False
        TextEdit24.ResetText()
        CheckEdit143.Checked = False
        CheckEdit144.Checked = False
        CheckEdit145.Checked = False
        CheckEdit146.Checked = False
        CheckEdit147.Checked = False
        CheckEdit148.Checked = False
        CheckEdit149.Checked = False
        CheckEdit150.Checked = False
        CheckEdit151.Checked = False
        CheckEdit152.Checked = False
        CheckEdit153.Checked = False
        CheckEdit154.Checked = False
        CheckEdit155.Checked = False
        CheckEdit156.Checked = False
        CheckEdit157.Checked = False
        CheckEdit158.Checked = False
        CheckEdit159.Checked = False
        CheckEdit160.Checked = False
        CheckEdit161.Checked = False
        TextEdit25.ResetText()
        CheckEdit162.Checked = False
        CheckEdit163.Checked = False
        CheckEdit164.Checked = False
        CheckEdit165.Checked = False
        CheckEdit166.Checked = False
        CheckEdit167.Checked = False
        MemoEdit1.ResetText()
        chkREGULER.Checked = False
        chkIREGULER.Checked = False

        TextEdit1.ResetText()
        chk1_0.Checked = False
        chk1_2.Checked = False
        chk1_YA.Checked = False
        chk1_YA_1.Checked = False
        chk1_YA_2.Checked = False
        chk1_YA_3.Checked = False
        chk1_YA_4.Checked = False
        txtSkor1.ResetText()
        chk2_0.Checked = False
        chk2_1.Checked = False
        txtSkor2.ResetText()
        chk3_YA.Checked = False
        chk3_DM.Checked = False
        chk3_Kemoterapi.Checked = False
        chk3_HD.Checked = False
        chk3_Imun.Checked = False
        txt3.ResetText()
        txtSkor3.ResetText()
        txtTOTALSKOR.ResetText()
        txtskorBraden.ResetText()
        chkResikoDekubitus_YA.Checked = False
        chkResikoDekubitus_TIDAK.Checked = False
        chkTerdapatLuka_YA.Checked = False
        chkTerdapatLuka_TIDAK.Checked = False
        chkResikoTinggi.Checked = False
        chkResikoSedang.Checked = False
        chkResikoRendah.Checked = False
        MemoEdit2.ResetText()
        chkPresepsiSensori_1.Checked = False
        chkPresepsiSensori_2.Checked = False
        chkPresepsiSensori_3.Checked = False
        chkPresepsiSensori_4.Checked = False
        txtSkorPersepsi.ResetText()
        chkKelembaban_1.Checked = False
        chkKelembaban_2.Checked = False
        chkKelembaban_3.Checked = False
        chkKelembaban_4.Checked = False
        txtSkorKelembaban.ResetText()
        chkAktivitas_1.Checked = False
        chkAktivitas_2.Checked = False
        chkAktivitas_3.Checked = False
        chkAktivitas_4.Checked = False
        txtSkorAktivitas.ResetText()
        chkMobilitas_1.Checked = False
        chkMobilitas_2.Checked = False
        chkMobilitas_3.Checked = False
        chkMobilitas_4.Checked = False
        txtSkorMobilitas.ResetText()
        chkNutrisi_1.Checked = False
        chkNutrisi_2.Checked = False
        chkNutrisi_3.Checked = False
        chkNutrisi_4.Checked = False
        txtSkorNutrisi.ResetText()
        chkGesekan_1.Checked = False
        chkGesekan_2.Checked = False
        chkGesekan_3.Checked = False
        txtSkorGesekan.ResetText()

        'tambahan
        CheckEdit98.Checked = False
        CheckEdit99.Checked = False
        CheckEdit168.Checked = False
        CheckEdit169.Checked = False
        CheckEdit170.Checked = False
        CheckEdit171.Checked = False
        CheckEdit174.Checked = False
        CheckEdit180.Checked = False
        CheckEdit181.Checked = False

    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_ASKEPGERIATRI_1_2.GetData(txtNoRegister.Text)
            With ds

                deDATE.DateTime = .DATE
                
                txtKEADAANUMUM.Text = .S_DIGITAL_1
                txtKESADARAN.Text = .S_DIGITAL_2
                txtTINGGIBADAN.Text = .S_DIGITAL_3
                txtTINGGIBADANLUTUT.Text = .S_DIGITAL_4
                txtBERATBADAN.Text = .S_DIGITAL_5
                txtTD.Text = .S_DIGITAL_6
                txtPERNAPASAN.Text = .S_DIGITAL_7
                txtNADI.Text = .S_DIGITAL_8
                txtSUHU.Text = .S_DIGITAL_9
                txtSATURASI.Text = .S_DIGITAL_24
                chkSKALANYERI_00.Checked = .S_DIGITAL_10
                chkSKALANYERI_01.Checked = .S_DIGITAL_11
                chkSKALANYERI_02.Checked = .S_DIGITAL_12
                chkSKALANYERI_03.Checked = .S_DIGITAL_13
                chkSKALANYERI_04.Checked = .S_DIGITAL_14
                chkSKALANYERI_05.Checked = .S_DIGITAL_15
                chkSKALANYERI_06.Checked = .S_DIGITAL_16
                chkSKALANYERI_07.Checked = .S_DIGITAL_17
                chkSKALANYERI_08.Checked = .S_DIGITAL_18
                chkSKALANYERI_09.Checked = .S_DIGITAL_19
                chkSKALANYERI_10.Checked = .S_DIGITAL_20
                CheckEdit1.Checked = .S_DIGITAL_21
                CheckEdit2.Checked = .S_DIGITAL_22
                CheckEdit3.Checked = .S_DIGITAL_23
                CheckEdit4.Checked = .S_DIGITAL_25
                CheckEdit5.Checked = .S_DIGITAL_26
                CheckEdit6.Checked = .S_DIGITAL_27
                CheckEdit7.Checked = .S_DIGITAL_28
                CheckEdit8.Checked = .S_DIGITAL_29
                CheckEdit9.Checked = .S_DIGITAL_30
                CheckEdit10.Checked = .S_DIGITAL_31
                CheckEdit11.Checked = .S_DIGITAL_32
                CheckEdit12.Checked = .S_DIGITAL_33
                CheckEdit13.Checked = .S_DIGITAL_34
                CheckEdit14.Checked = .S_DIGITAL_35
                CheckEdit15.Checked = .S_DIGITAL_36
                TextEdit2.Text = .S_DIGITAL_37
                CheckEdit16.Checked = .S_DIGITAL_38
                CheckEdit17.Checked = .S_DIGITAL_39
                TextEdit3.Text = .S_DIGITAL_40
                TextEdit4.Text = .S_DIGITAL_41
                TextEdit5.Text = .S_DIGITAL_42
                CheckEdit18.Checked = .S_DIGITAL_43
                CheckEdit19.Checked = .S_DIGITAL_44
                CheckEdit20.Checked = .S_DIGITAL_45
                CheckEdit21.Checked = .S_DIGITAL_46
                CheckEdit22.Checked = .S_DIGITAL_47
                TextEdit6.Text = .S_DIGITAL_48
                CheckEdit23.Checked = .S_DIGITAL_49
                TextEdit7.Text = .S_DIGITAL_50
                CheckEdit24.Checked = .S_DIGITAL_51
                CheckEdit25.Checked = .S_DIGITAL_52
                CheckEdit26.Checked = .S_DIGITAL_53
                CheckEdit27.Checked = .S_DIGITAL_54
                CheckEdit28.Checked = .S_DIGITAL_55
                CheckEdit29.Checked = .S_DIGITAL_56
                CheckEdit31.Checked = .S_DIGITAL_57
                CheckEdit30.Checked = .S_DIGITAL_58
                CheckEdit32.Checked = .S_DIGITAL_59
                CheckEdit33.Checked = .S_DIGITAL_60
                CheckEdit34.Checked = .S_DIGITAL_61
                CheckEdit35.Checked = .S_DIGITAL_62
                CheckEdit36.Checked = .S_DIGITAL_63
                txtKELUHANUTAMA.Text = .S_DIGITAL_64
                CheckEdit37.Checked = .S_DIGITAL_65
                CheckEdit38.Checked = .S_DIGITAL_66
                CheckEdit39.Checked = .S_DIGITAL_67
                CheckEdit40.Checked = .S_DIGITAL_68
                CheckEdit41.Checked = .S_DIGITAL_69
                TextEdit8.Text = .S_DIGITAL_70
                CheckEdit43.Checked = .S_DIGITAL_71
                CheckEdit44.Checked = .S_DIGITAL_72
                CheckEdit45.Checked = .S_DIGITAL_73
                CheckEdit46.Checked = .S_DIGITAL_74
                CheckEdit47.Checked = .S_DIGITAL_75
                TextEdit9.Text = .S_DIGITAL_76
                CheckEdit48.Checked = .S_DIGITAL_77
                CheckEdit49.Checked = .S_DIGITAL_78
                TextEdit10.Text = .S_DIGITAL_79
                CheckEdit51.Checked = .S_DIGITAL_80
                CheckEdit52.Checked = .S_DIGITAL_81
                CheckEdit50.Checked = .S_DIGITAL_82
                TextEdit11.Text = .S_DIGITAL_83
                CheckEdit54.Checked = .S_DIGITAL_84
                CheckEdit53.Checked = .S_DIGITAL_85
                TextEdit12.Text = .S_DIGITAL_86
                CheckEdit55.Checked = .S_DIGITAL_87
                CheckEdit56.Checked = .S_DIGITAL_88
                CheckEdit57.Checked = .S_DIGITAL_89
                CheckEdit58.Checked = .S_DIGITAL_90
                TextEdit13.Text = .S_DIGITAL_91
                CheckEdit60.Checked = .S_DIGITAL_92
                CheckEdit61.Checked = .S_DIGITAL_93
                CheckEdit62.Checked = .S_DIGITAL_94
                CheckEdit63.Checked = .S_DIGITAL_95
                CheckEdit59.Checked = .S_DIGITAL_96
                CheckEdit64.Checked = .S_DIGITAL_97
                CheckEdit65.Checked = .S_DIGITAL_98
                TextEdit14.Text = .S_DIGITAL_99
                CheckEdit66.Checked = .S_DIGITAL_100
                CheckEdit67.Checked = .S_DIGITAL_101
                CheckEdit68.Checked = .S_DIGITAL_102
                CheckEdit69.Checked = .S_DIGITAL_103
                CheckEdit70.Checked = .S_DIGITAL_104
                CheckEdit71.Checked = .S_DIGITAL_105
                TextEdit15.Text = .S_DIGITAL_106
                CheckEdit72.Checked = .S_DIGITAL_107
                CheckEdit73.Checked = .S_DIGITAL_108
                CheckEdit74.Checked = .S_DIGITAL_109
                CheckEdit75.Checked = .S_DIGITAL_110
                CheckEdit76.Checked = .S_DIGITAL_111
                CheckEdit77.Checked = .S_DIGITAL_112
                CheckEdit78.Checked = .S_DIGITAL_113
                chkHAID_YA.Checked = .S_DIGITAL_114
                chkHAID_TIDAK.Checked = .S_DIGITAL_115
                CheckEdit79.Checked = .S_DIGITAL_116
                CheckEdit80.Checked = .S_DIGITAL_117
                CheckEdit81.Checked = .S_DIGITAL_118
                CheckEdit82.Checked = .S_DIGITAL_119
                TextEdit16.Text = .S_DIGITAL_120
                CheckEdit83.Checked = .S_DIGITAL_121
                CheckEdit84.Checked = .S_DIGITAL_122
                CheckEdit85.Checked = .S_DIGITAL_123
                CheckEdit86.Checked = .S_DIGITAL_124
                chkPRODUKSI_ADA.Checked = .S_DIGITAL_125
                chkPRODUKSI_TIDAK.Checked = .S_DIGITAL_126
                CheckEdit87.Checked = .S_DIGITAL_127
                TextEdit17.Text = .S_DIGITAL_128
                CheckEdit88.Checked = .S_DIGITAL_129
                CheckEdit89.Checked = .S_DIGITAL_130
                CheckEdit90.Checked = .S_DIGITAL_131
                CheckEdit91.Checked = .S_DIGITAL_132
                TextEdit18.Text = .S_DIGITAL_133
                CheckEdit92.Checked = .S_DIGITAL_134
                CheckEdit93.Checked = .S_DIGITAL_135
                CheckEdit94.Checked = .S_DIGITAL_136
                CheckEdit95.Checked = .S_DIGITAL_137
                CheckEdit96.Checked = .S_DIGITAL_138
                CheckEdit97.Checked = .S_DIGITAL_139
                TextEdit19.Text = .S_DIGITAL_140
                CheckEdit100.Checked = .S_DIGITAL_141
                CheckEdit101.Checked = .S_DIGITAL_142
                CheckEdit102.Checked = .S_DIGITAL_143
                CheckEdit103.Checked = .S_DIGITAL_144
                CheckEdit104.Checked = .S_DIGITAL_145
                CheckEdit105.Checked = .S_DIGITAL_146
                TextEdit20.Text = .S_DIGITAL_147
                CheckEdit106.Checked = .S_DIGITAL_148
                TextEdit21.Text = .S_DIGITAL_149
                CheckEdit107.Checked = .S_DIGITAL_150
                CheckEdit108.Checked = .S_DIGITAL_151
                CheckEdit109.Checked = .S_DIGITAL_152
                CheckEdit110.Checked = .S_DIGITAL_153
                CheckEdit111.Checked = .S_DIGITAL_154
                CheckEdit112.Checked = .S_DIGITAL_155
                CheckEdit113.Checked = .S_DIGITAL_156
                CheckEdit114.Checked = .S_DIGITAL_157
                CheckEdit115.Checked = .S_DIGITAL_158
                CheckEdit116.Checked = .S_DIGITAL_159
                TextEdit22.Text = .S_DIGITAL_160
                CheckEdit117.Checked = .S_DIGITAL_161
                CheckEdit118.Checked = .S_DIGITAL_162
                CheckEdit119.Checked = .S_DIGITAL_163
                CheckEdit120.Checked = .S_DIGITAL_164
                CheckEdit42.Checked = .S_DIGITAL_274
                CheckEdit121.Checked = .S_DIGITAL_165
                CheckEdit122.Checked = .S_DIGITAL_166
                CheckEdit123.Checked = .S_DIGITAL_167
                TextEdit23.Text = .S_DIGITAL_168
                CheckEdit124.Checked = .S_DIGITAL_169
                CheckEdit125.Checked = .S_DIGITAL_170
                CheckEdit126.Checked = .S_DIGITAL_171
                CheckEdit127.Checked = .S_DIGITAL_172
                CheckEdit128.Checked = .S_DIGITAL_173
                CheckEdit129.Checked = .S_DIGITAL_174
                CheckEdit130.Checked = .S_DIGITAL_175
                CheckEdit131.Checked = .S_DIGITAL_176
                CheckEdit132.Checked = .S_DIGITAL_177
                CheckEdit133.Checked = .S_DIGITAL_178
                CheckEdit134.Checked = .S_DIGITAL_179
                CheckEdit135.Checked = .S_DIGITAL_180
                CheckEdit136.Checked = .S_DIGITAL_181
                CheckEdit137.Checked = .S_DIGITAL_182
                CheckEdit138.Checked = .S_DIGITAL_183
                CheckEdit139.Checked = .S_DIGITAL_184
                CheckEdit140.Checked = .S_DIGITAL_185
                CheckEdit141.Checked = .S_DIGITAL_186
                CheckEdit142.Checked = .S_DIGITAL_187
                TextEdit24.Text = .S_DIGITAL_188
                CheckEdit143.Checked = .S_DIGITAL_189
                CheckEdit144.Checked = .S_DIGITAL_190
                CheckEdit145.Checked = .S_DIGITAL_191
                CheckEdit146.Checked = .S_DIGITAL_192
                CheckEdit147.Checked = .S_DIGITAL_193
                CheckEdit148.Checked = .S_DIGITAL_194
                CheckEdit149.Checked = .S_DIGITAL_195
                CheckEdit150.Checked = .S_DIGITAL_196
                CheckEdit151.Checked = .S_DIGITAL_197
                CheckEdit152.Checked = .S_DIGITAL_198
                CheckEdit153.Checked = .S_DIGITAL_199
                CheckEdit154.Checked = .S_DIGITAL_200
                CheckEdit155.Checked = .S_DIGITAL_201
                CheckEdit156.Checked = .S_DIGITAL_202
                CheckEdit157.Checked = .S_DIGITAL_203
                CheckEdit158.Checked = .S_DIGITAL_204
                CheckEdit159.Checked = .S_DIGITAL_205
                CheckEdit160.Checked = .S_DIGITAL_206
                CheckEdit161.Checked = .S_DIGITAL_207
                TextEdit25.Text = .S_DIGITAL_208
                CheckEdit162.Checked = .S_DIGITAL_209
                CheckEdit163.Checked = .S_DIGITAL_210
                CheckEdit164.Checked = .S_DIGITAL_211
                CheckEdit165.Checked = .S_DIGITAL_212
                CheckEdit166.Checked = .S_DIGITAL_213
                CheckEdit167.Checked = .S_DIGITAL_214
                MemoEdit1.Text = .S_DIGITAL_215
                chkREGULER.Checked = .ISREGULER
                chkIREGULER.Checked = .ISIREGULER

                Try
                    PictureBox1.Image = ByteArrayToImage(.picGambar.ToArray())
                Catch oErr As Exception
                End Try

                TextEdit1.Text = .S_DIGITAL_216
                chk1_0.Checked = .S_DIGITAL_217
                chk1_2.Checked = .S_DIGITAL_218
                chk1_YA.Checked = .S_DIGITAL_219
                chk1_YA_1.Checked = .S_DIGITAL_220
                chk1_YA_2.Checked = .S_DIGITAL_221
                chk1_YA_3.Checked = .S_DIGITAL_222
                chk1_YA_4.Checked = .S_DIGITAL_223
                txtSkor1.Text = .S_DIGITAL_224
                chk2_0.Checked = .S_DIGITAL_225
                chk2_1.Checked = .S_DIGITAL_226
                txtSkor2.Text = .S_DIGITAL_227
                chk3_YA.Checked = .S_DIGITAL_228
                chk3_DM.Checked = .S_DIGITAL_229
                chk3_Kemoterapi.Checked = .S_DIGITAL_230
                chk3_HD.Checked = .S_DIGITAL_231
                chk3_Imun.Checked = .S_DIGITAL_232
                txt3.Text = .S_DIGITAL_233
                txtSkor3.Text = .S_DIGITAL_234
                txtTOTALSKOR.Text = .S_DIGITAL_235
                txtskorBraden.Text = .S_DIGITAL_236
                chkResikoDekubitus_YA.Checked = .S_DIGITAL_237
                chkResikoDekubitus_TIDAK.Checked = .S_DIGITAL_238
                chkTerdapatLuka_YA.Checked = .S_DIGITAL_239
                chkTerdapatLuka_TIDAK.Checked = .S_DIGITAL_240
                chkResikoTinggi.Checked = .S_DIGITAL_241
                chkResikoSedang.Checked = .S_DIGITAL_242
                chkResikoRendah.Checked = .S_DIGITAL_243
                MemoEdit2.Text = .S_DIGITAL_244
                chkPresepsiSensori_1.Checked = .S_DIGITAL_245
                chkPresepsiSensori_2.Checked = .S_DIGITAL_246
                chkPresepsiSensori_3.Checked = .S_DIGITAL_247
                chkPresepsiSensori_4.Checked = .S_DIGITAL_248
                txtSkorPersepsi.Text = .S_DIGITAL_249
                chkKelembaban_1.Checked = .S_DIGITAL_250
                chkKelembaban_2.Checked = .S_DIGITAL_251
                chkKelembaban_3.Checked = .S_DIGITAL_252
                chkKelembaban_4.Checked = .S_DIGITAL_253
                txtSkorKelembaban.Text = .S_DIGITAL_254
                chkAktivitas_1.Checked = .S_DIGITAL_255
                chkAktivitas_2.Checked = .S_DIGITAL_256
                chkAktivitas_3.Checked = .S_DIGITAL_257
                chkAktivitas_4.Checked = .S_DIGITAL_258
                txtSkorAktivitas.Text = .S_DIGITAL_259
                chkMobilitas_1.Checked = .S_DIGITAL_260
                chkMobilitas_2.Checked = .S_DIGITAL_261
                chkMobilitas_3.Checked = .S_DIGITAL_262
                chkMobilitas_4.Checked = .S_DIGITAL_263
                txtSkorMobilitas.Text = .S_DIGITAL_264
                chkNutrisi_1.Checked = .S_DIGITAL_265
                chkNutrisi_2.Checked = .S_DIGITAL_266
                chkNutrisi_3.Checked = .S_DIGITAL_267
                chkNutrisi_4.Checked = .S_DIGITAL_268
                txtSkorNutrisi.Text = .S_DIGITAL_269
                chkGesekan_1.Checked = .S_DIGITAL_270
                chkGesekan_2.Checked = .S_DIGITAL_271
                chkGesekan_3.Checked = .S_DIGITAL_272
                txtSkorGesekan.Text = .S_DIGITAL_273

                sKDUSER = .KDUSER
                sKDUSERSIGNATURE = .KDUSER_SIGNATURE

                'tambahan
                CheckEdit98.Checked = .S_DIGITAL_275
                CheckEdit99.Checked = .S_DIGITAL_276
                CheckEdit168.Checked = .S_DIGITAL_277
                CheckEdit169.Checked = .S_DIGITAL_278
                CheckEdit170.Checked = .S_DIGITAL_279
                CheckEdit171.Checked = .S_DIGITAL_280
                CheckEdit174.Checked = .S_DIGITAL_281
                CheckEdit180.Checked = .S_DIGITAL_282
                CheckEdit181.Checked = .S_DIGITAL_283

                

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
            Dim ds = oS_DIGITAL_ASKEPGERIATRI_1_2.GetStructureHeader
            With ds
                .KDKUNJUNGAN = txtNoRegister.Text
                .KDPENDAFTARAN = sKDPENDAFTARAN
                Try
                    .DATECREATED = oS_DIGITAL_ASKEPGERIATRI_1_2.GetData(txtNoRegister.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .DATE = deDATE.DateTime

                .S_DIGITAL_1 = txtKEADAANUMUM.Text
                .S_DIGITAL_2 = txtKESADARAN.Text
                .S_DIGITAL_3 = txtTINGGIBADAN.Text
                .S_DIGITAL_4 = txtTINGGIBADANLUTUT.Text
                .S_DIGITAL_5 = txtBERATBADAN.Text
                .S_DIGITAL_6 = txtTD.Text
                .S_DIGITAL_7 = txtPERNAPASAN.Text
                .S_DIGITAL_8 = txtNADI.Text
                .S_DIGITAL_9 = txtSUHU.Text
                .S_DIGITAL_10 = chkSKALANYERI_00.Checked
                .S_DIGITAL_11 = chkSKALANYERI_01.Checked
                .S_DIGITAL_12 = chkSKALANYERI_02.Checked
                .S_DIGITAL_13 = chkSKALANYERI_03.Checked
                .S_DIGITAL_14 = chkSKALANYERI_04.Checked
                .S_DIGITAL_15 = chkSKALANYERI_05.Checked
                .S_DIGITAL_16 = chkSKALANYERI_06.Checked
                .S_DIGITAL_17 = chkSKALANYERI_07.Checked
                .S_DIGITAL_18 = chkSKALANYERI_08.Checked
                .S_DIGITAL_19 = chkSKALANYERI_09.Checked
                .S_DIGITAL_20 = chkSKALANYERI_10.Checked
                .S_DIGITAL_21 = CheckEdit1.Checked
                .S_DIGITAL_22 = CheckEdit2.Checked
                .S_DIGITAL_23 = CheckEdit3.Checked
                .S_DIGITAL_24 = txtSATURASI.Text
                .S_DIGITAL_25 = CheckEdit4.Checked
                .S_DIGITAL_26 = CheckEdit5.Checked
                .S_DIGITAL_27 = CheckEdit6.Checked
                .S_DIGITAL_28 = CheckEdit7.Checked
                .S_DIGITAL_29 = CheckEdit8.Checked
                .S_DIGITAL_30 = CheckEdit9.Checked
                .S_DIGITAL_31 = CheckEdit10.Checked
                .S_DIGITAL_32 = CheckEdit11.Checked
                .S_DIGITAL_33 = CheckEdit12.Checked
                .S_DIGITAL_34 = CheckEdit13.Checked
                .S_DIGITAL_35 = CheckEdit14.Checked
                .S_DIGITAL_36 = CheckEdit15.Checked
                .S_DIGITAL_37 = TextEdit2.Text
                .S_DIGITAL_38 = CheckEdit16.Checked
                .S_DIGITAL_39 = CheckEdit17.Checked
                .S_DIGITAL_40 = TextEdit3.Text
                .S_DIGITAL_41 = TextEdit4.Text
                .S_DIGITAL_42 = TextEdit5.Text
                .S_DIGITAL_43 = CheckEdit18.Checked
                .S_DIGITAL_44 = CheckEdit19.Checked
                .S_DIGITAL_45 = CheckEdit20.Checked
                .S_DIGITAL_46 = CheckEdit21.Checked
                .S_DIGITAL_47 = CheckEdit22.Checked
                .S_DIGITAL_48 = TextEdit6.Text
                .S_DIGITAL_49 = CheckEdit23.Checked
                .S_DIGITAL_50 = TextEdit7.Text
                .S_DIGITAL_51 = CheckEdit24.Checked
                .S_DIGITAL_52 = CheckEdit25.Checked
                .S_DIGITAL_53 = CheckEdit26.Checked
                .S_DIGITAL_54 = CheckEdit27.Checked
                .S_DIGITAL_55 = CheckEdit28.Checked
                .S_DIGITAL_56 = CheckEdit29.Checked
                .S_DIGITAL_57 = CheckEdit31.Checked
                .S_DIGITAL_58 = CheckEdit30.Checked
                .S_DIGITAL_59 = CheckEdit32.Checked
                .S_DIGITAL_60 = CheckEdit33.Checked
                .S_DIGITAL_61 = CheckEdit34.Checked
                .S_DIGITAL_62 = CheckEdit35.Checked
                .S_DIGITAL_63 = CheckEdit36.Checked
                .S_DIGITAL_64 = txtKELUHANUTAMA.Text
                .S_DIGITAL_65 = CheckEdit37.Checked
                .S_DIGITAL_66 = CheckEdit38.Checked
                .S_DIGITAL_67 = CheckEdit39.Checked
                .S_DIGITAL_68 = CheckEdit40.Checked
                .S_DIGITAL_69 = CheckEdit41.Checked
                .S_DIGITAL_70 = TextEdit8.Text
                .S_DIGITAL_71 = CheckEdit43.Checked
                .S_DIGITAL_72 = CheckEdit44.Checked
                .S_DIGITAL_73 = CheckEdit45.Checked
                .S_DIGITAL_74 = CheckEdit46.Checked
                .S_DIGITAL_75 = CheckEdit47.Checked
                .S_DIGITAL_76 = TextEdit9.Text
                .S_DIGITAL_77 = CheckEdit48.Checked
                .S_DIGITAL_78 = CheckEdit49.Checked
                .S_DIGITAL_79 = TextEdit10.Text
                .S_DIGITAL_80 = CheckEdit51.Checked
                .S_DIGITAL_81 = CheckEdit52.Checked
                .S_DIGITAL_82 = CheckEdit50.Checked
                .S_DIGITAL_83 = TextEdit11.Text
                .S_DIGITAL_84 = CheckEdit54.Checked
                .S_DIGITAL_85 = CheckEdit53.Checked
                .S_DIGITAL_86 = TextEdit12.Text
                .S_DIGITAL_87 = CheckEdit55.Checked
                .S_DIGITAL_88 = CheckEdit56.Checked
                .S_DIGITAL_89 = CheckEdit57.Checked
                .S_DIGITAL_90 = CheckEdit58.Checked
                .S_DIGITAL_91 = TextEdit13.Text
                .S_DIGITAL_92 = CheckEdit60.Checked
                .S_DIGITAL_93 = CheckEdit61.Checked
                .S_DIGITAL_94 = CheckEdit62.Checked
                .S_DIGITAL_95 = CheckEdit63.Checked
                .S_DIGITAL_96 = CheckEdit59.Checked
                .S_DIGITAL_97 = CheckEdit64.Checked
                .S_DIGITAL_98 = CheckEdit65.Checked
                .S_DIGITAL_99 = TextEdit14.Text
                .S_DIGITAL_100 = CheckEdit66.Checked
                .S_DIGITAL_101 = CheckEdit67.Checked
                .S_DIGITAL_102 = CheckEdit68.Checked
                .S_DIGITAL_103 = CheckEdit69.Checked
                .S_DIGITAL_104 = CheckEdit70.Checked
                .S_DIGITAL_105 = CheckEdit71.Checked
                .S_DIGITAL_106 = TextEdit15.Text
                .S_DIGITAL_107 = CheckEdit72.Checked
                .S_DIGITAL_108 = CheckEdit73.Checked
                .S_DIGITAL_109 = CheckEdit74.Checked
                .S_DIGITAL_110 = CheckEdit75.Checked
                .S_DIGITAL_111 = CheckEdit76.Checked
                .S_DIGITAL_112 = CheckEdit77.Checked
                .S_DIGITAL_113 = CheckEdit78.Checked
                .S_DIGITAL_114 = chkHAID_YA.Checked
                .S_DIGITAL_115 = chkHAID_TIDAK.Checked
                .S_DIGITAL_116 = CheckEdit79.Checked
                .S_DIGITAL_117 = CheckEdit80.Checked
                .S_DIGITAL_118 = CheckEdit81.Checked
                .S_DIGITAL_119 = CheckEdit82.Checked
                .S_DIGITAL_120 = TextEdit16.Text
                .S_DIGITAL_121 = CheckEdit83.Checked
                .S_DIGITAL_122 = CheckEdit84.Checked
                .S_DIGITAL_123 = CheckEdit85.Checked
                .S_DIGITAL_124 = CheckEdit86.Checked
                .S_DIGITAL_125 = chkPRODUKSI_ADA.Checked
                .S_DIGITAL_126 = chkPRODUKSI_TIDAK.Checked
                .S_DIGITAL_127 = CheckEdit87.Checked
                .S_DIGITAL_128 = TextEdit17.Text
                .S_DIGITAL_129 = CheckEdit88.Checked
                .S_DIGITAL_130 = CheckEdit89.Checked
                .S_DIGITAL_131 = CheckEdit90.Checked
                .S_DIGITAL_132 = CheckEdit91.Checked
                .S_DIGITAL_133 = TextEdit18.Text
                .S_DIGITAL_134 = CheckEdit92.Checked
                .S_DIGITAL_135 = CheckEdit93.Checked
                .S_DIGITAL_136 = CheckEdit94.Checked
                .S_DIGITAL_137 = CheckEdit95.Checked
                .S_DIGITAL_138 = CheckEdit96.Checked
                .S_DIGITAL_139 = CheckEdit97.Checked
                .S_DIGITAL_140 = TextEdit19.Text
                .S_DIGITAL_141 = CheckEdit100.Checked
                .S_DIGITAL_142 = CheckEdit101.Checked
                .S_DIGITAL_143 = CheckEdit102.Checked
                .S_DIGITAL_144 = CheckEdit103.Checked
                .S_DIGITAL_145 = CheckEdit104.Checked
                .S_DIGITAL_146 = CheckEdit105.Checked
                .S_DIGITAL_147 = TextEdit20.Text
                .S_DIGITAL_148 = CheckEdit106.Checked
                .S_DIGITAL_149 = TextEdit21.Text
                .S_DIGITAL_150 = CheckEdit107.Checked
                .S_DIGITAL_151 = CheckEdit108.Checked
                .S_DIGITAL_152 = CheckEdit109.Checked
                .S_DIGITAL_153 = CheckEdit110.Checked
                .S_DIGITAL_154 = CheckEdit111.Checked
                .S_DIGITAL_155 = CheckEdit112.Checked
                .S_DIGITAL_156 = CheckEdit113.Checked
                .S_DIGITAL_157 = CheckEdit114.Checked
                .S_DIGITAL_158 = CheckEdit115.Checked
                .S_DIGITAL_159 = CheckEdit116.Checked
                .S_DIGITAL_160 = TextEdit22.Text
                .S_DIGITAL_161 = CheckEdit117.Checked
                .S_DIGITAL_162 = CheckEdit118.Checked
                .S_DIGITAL_163 = CheckEdit119.Checked
                .S_DIGITAL_164 = CheckEdit120.Checked
                .S_DIGITAL_165 = CheckEdit121.Checked
                .S_DIGITAL_166 = CheckEdit122.Checked
                .S_DIGITAL_167 = CheckEdit123.Checked
                .S_DIGITAL_168 = TextEdit23.Text
                .S_DIGITAL_169 = CheckEdit124.Checked
                .S_DIGITAL_170 = CheckEdit125.Checked
                .S_DIGITAL_171 = CheckEdit126.Checked
                .S_DIGITAL_172 = CheckEdit127.Checked
                .S_DIGITAL_173 = CheckEdit128.Checked
                .S_DIGITAL_174 = CheckEdit129.Checked
                .S_DIGITAL_175 = CheckEdit130.Checked
                .S_DIGITAL_176 = CheckEdit131.Checked
                .S_DIGITAL_177 = CheckEdit132.Checked
                .S_DIGITAL_178 = CheckEdit133.Checked
                .S_DIGITAL_179 = CheckEdit134.Checked
                .S_DIGITAL_180 = CheckEdit135.Checked
                .S_DIGITAL_181 = CheckEdit136.Checked
                .S_DIGITAL_182 = CheckEdit137.Checked
                .S_DIGITAL_183 = CheckEdit138.Checked
                .S_DIGITAL_184 = CheckEdit139.Checked
                .S_DIGITAL_185 = CheckEdit140.Checked
                .S_DIGITAL_186 = CheckEdit141.Checked
                .S_DIGITAL_187 = CheckEdit142.Checked
                .S_DIGITAL_188 = TextEdit24.Text
                .S_DIGITAL_189 = CheckEdit143.Checked
                .S_DIGITAL_190 = CheckEdit144.Checked
                .S_DIGITAL_191 = CheckEdit145.Checked
                .S_DIGITAL_192 = CheckEdit146.Checked
                .S_DIGITAL_193 = CheckEdit147.Checked
                .S_DIGITAL_194 = CheckEdit148.Checked
                .S_DIGITAL_195 = CheckEdit149.Checked
                .S_DIGITAL_196 = CheckEdit150.Checked
                .S_DIGITAL_197 = CheckEdit151.Checked
                .S_DIGITAL_198 = CheckEdit152.Checked
                .S_DIGITAL_199 = CheckEdit153.Checked
                .S_DIGITAL_200 = CheckEdit154.Checked
                .S_DIGITAL_201 = CheckEdit155.Checked
                .S_DIGITAL_202 = CheckEdit156.Checked
                .S_DIGITAL_203 = CheckEdit157.Checked
                .S_DIGITAL_204 = CheckEdit158.Checked
                .S_DIGITAL_205 = CheckEdit159.Checked
                .S_DIGITAL_206 = CheckEdit160.Checked
                .S_DIGITAL_207 = CheckEdit161.Checked
                .S_DIGITAL_208 = TextEdit25.Text
                .S_DIGITAL_209 = CheckEdit162.Checked
                .S_DIGITAL_210 = CheckEdit163.Checked
                .S_DIGITAL_211 = CheckEdit164.Checked
                .S_DIGITAL_212 = CheckEdit165.Checked
                .S_DIGITAL_213 = CheckEdit166.Checked
                .S_DIGITAL_214 = CheckEdit167.Checked
                .S_DIGITAL_215 = MemoEdit1.Text
                .ISREGULER = chkREGULER.Checked
                .ISIREGULER = chkIREGULER.Checked

                Try
                    Dim ms As New IO.MemoryStream()
                    PictureBox1.Image.Save(ms, PictureBox1.Image.RawFormat)

                    Dim data As Byte() = ms.GetBuffer()

                    .picGambar   = data
                Catch oErr As Exception
                    Try
                        .picGambar = oS_DIGITAL_ASKEPGERIATRI_1_2.GetData(txtNoRegister.Text).picGambar
                    Catch ex As Exception

                    End Try
                End Try

                .S_DIGITAL_216 = TextEdit1.Text
                .S_DIGITAL_217 = chk1_0.Checked
                .S_DIGITAL_218 = chk1_2.Checked
                .S_DIGITAL_219 = chk1_YA.Checked
                .S_DIGITAL_220 = chk1_YA_1.Checked
                .S_DIGITAL_221 = chk1_YA_2.Checked
                .S_DIGITAL_222 = chk1_YA_3.Checked
                .S_DIGITAL_223 = chk1_YA_4.Checked
                .S_DIGITAL_224 = txtSkor1.Text
                .S_DIGITAL_225 = chk2_0.Checked
                .S_DIGITAL_226 = chk2_1.Checked
                .S_DIGITAL_227 = txtSkor2.Text
                .S_DIGITAL_228 = chk3_YA.Checked
                .S_DIGITAL_229 = chk3_DM.Checked
                .S_DIGITAL_230 = chk3_Kemoterapi.Checked
                .S_DIGITAL_231 = chk3_HD.Checked
                .S_DIGITAL_232 = chk3_Imun.Checked
                .S_DIGITAL_233 = txt3.Text
                .S_DIGITAL_234 = txtSkor3.Text
                .S_DIGITAL_235 = txtTOTALSKOR.Text
                .S_DIGITAL_236 = txtskorBraden.Text
                .S_DIGITAL_237 = chkResikoDekubitus_YA.Checked
                .S_DIGITAL_238 = chkResikoDekubitus_TIDAK.Checked
                .S_DIGITAL_239 = chkTerdapatLuka_YA.Checked
                .S_DIGITAL_240 = chkTerdapatLuka_TIDAK.Checked
                .S_DIGITAL_241 = chkResikoTinggi.Checked
                .S_DIGITAL_242 = chkResikoSedang.Checked
                .S_DIGITAL_243 = chkResikoRendah.Checked
                .S_DIGITAL_244 = MemoEdit2.Text
                .S_DIGITAL_245 = chkPresepsiSensori_1.Checked
                .S_DIGITAL_246 = chkPresepsiSensori_2.Checked
                .S_DIGITAL_247 = chkPresepsiSensori_3.Checked
                .S_DIGITAL_248 = chkPresepsiSensori_4.Checked
                .S_DIGITAL_249 = txtSkorPersepsi.Text
                .S_DIGITAL_250 = chkKelembaban_1.Checked
                .S_DIGITAL_251 = chkKelembaban_2.Checked
                .S_DIGITAL_252 = chkKelembaban_3.Checked
                .S_DIGITAL_253 = chkKelembaban_4.Checked
                .S_DIGITAL_254 = txtSkorKelembaban.Text
                .S_DIGITAL_255 = chkAktivitas_1.Checked
                .S_DIGITAL_256 = chkAktivitas_2.Checked
                .S_DIGITAL_257 = chkAktivitas_3.Checked
                .S_DIGITAL_258 = chkAktivitas_4.Checked
                .S_DIGITAL_259 = txtSkorAktivitas.Text
                .S_DIGITAL_260 = chkMobilitas_1.Checked
                .S_DIGITAL_261 = chkMobilitas_2.Checked
                .S_DIGITAL_262 = chkMobilitas_3.Checked
                .S_DIGITAL_263 = chkMobilitas_4.Checked
                .S_DIGITAL_264 = txtSkorMobilitas.Text
                .S_DIGITAL_265 = chkNutrisi_1.Checked
                .S_DIGITAL_266 = chkNutrisi_2.Checked
                .S_DIGITAL_267 = chkNutrisi_3.Checked
                .S_DIGITAL_268 = chkNutrisi_4.Checked
                .S_DIGITAL_269 = txtSkorNutrisi.Text
                .S_DIGITAL_270 = chkGesekan_1.Checked
                .S_DIGITAL_271 = chkGesekan_2.Checked
                .S_DIGITAL_272 = chkGesekan_3.Checked
                .S_DIGITAL_273 = txtSkorGesekan.Text
                .S_DIGITAL_274 = CheckEdit42.Checked


                Try
                    .CETAK = oS_DIGITAL_ASKEPGERIATRI_1_2.GetData(txtNoRegister.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                .KDUSER = sKDUSER
                .KDUSER_SIGNATURE = sKDUSERSIGNATURE

                'tambahan
                .S_DIGITAL_275 = CheckEdit98.Checked
                .S_DIGITAL_276 = CheckEdit99.Checked
                .S_DIGITAL_277 = CheckEdit168.Checked
                .S_DIGITAL_278 = CheckEdit169.Checked
                .S_DIGITAL_279 = CheckEdit170.Checked
                .S_DIGITAL_280 = CheckEdit171.Checked
                .S_DIGITAL_281 = CheckEdit174.Checked
                .S_DIGITAL_282 = CheckEdit180.Checked
                .S_DIGITAL_283 = CheckEdit181.Checked

            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_ASKEPGERIATRI_1_2.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_ASKEPGERIATRI_1_2.UpdateData(ds)
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

    Private _Previous As System.Nullable(Of Point) = Nothing
    Private m_Drawing As Boolean
    Private m_StartX, m_StartY, m_CurrentX, m_CurrentY As Single
    Dim MyArrow As New List(Of DataAccess.LineList)
    Dim finalarrow As New Pen(Color.Red)

    Private Sub pictureBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseDown
        _Previous = e.Location
        pictureBox1_MouseMove(sender, e)

        If chkArrow.Checked Then
            m_Drawing = True
            m_StartX = e.X
            m_StartY = e.Y

            m_CurrentX = e.X
            m_CurrentY = e.Y
        End If
    End Sub

    Private Sub pictureBox1_Paint(sender As Object, e As PaintEventArgs) Handles PictureBox1.Paint
        If chkArrow.Checked Then
            Dim GridColor As Color = Color.Red

            Dim i,x1,x2,y1,y2 As Integer

            For i = 0 To MyArrow.Count - 1
                Dim arrow2 As New Pen(GridColor)
                arrow2.Width = 3
                arrow2.EndCap = Drawing2D.LineCap.Custom
                arrow2.CustomEndCap = New AdjustableArrowCap(3, 3, False)
                x1 = MyArrow(i).X1 : x2 = MyArrow(i).X2
                y1 = MyArrow(i).Y1 : y2 = MyArrow(i).Y2
                e.Graphics.DrawLine(arrow2,x1,y1,x2,y2)
            Next

            If m_Drawing Then
                Dim arrow As New Pen(GridColor)
                arrow.Width = 3
                arrow.EndCap = Drawing2D.LineCap.Custom
                arrow.CustomEndCap = New AdjustableArrowCap(3, 3, False)
                e.Graphics.DrawLine(arrow,m_StartX,m_StartY,m_CurrentX,m_CurrentY)
            End If
            
        End If
    End Sub

    Private Sub pictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseMove
        If _Previous IsNot Nothing Then
            Dim GridColor As Color = Color.Red

            If chkArrow.Checked = False Then
                Dim GridPen As New Pen(GridColor)
                GridPen.Width = 2
                Using g As Graphics = Graphics.FromImage(PictureBox1.Image)
                    g.DrawLine(GridPen, _Previous.Value, e.Location)
                    'g.DrawString("Test", RichTextBox1.Font, Brushes.Black, New PointF(10, 10))
                End Using
            Else
                m_CurrentX = e.X
                m_CurrentY = e.Y
            End If

             PictureBox1.Invalidate()
             _Previous = e.Location
        End If
    End Sub

    Private Sub pictureBox1_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseUp
        _Previous = Nothing

        If chkArrow.Checked Then
            m_Drawing = False

            Dim drawLine As New DataAccess.LineList
            drawLine.X1 = m_StartX
            drawLine.Y1 = m_StartY
            drawLine.X2 = m_CurrentX
            drawLine.Y2 = m_CurrentY

            MyArrow.Add(drawLine)

            Dim g As Graphics = Graphics.FromImage(PictureBox1.Image)
            finalarrow.Width = 3
            finalarrow.EndCap = Drawing2D.LineCap.Custom
            finalarrow.CustomEndCap = New AdjustableArrowCap(3,3,False)
            g.DrawLine(finalarrow,m_StartX,m_StartY,m_CurrentX,m_CurrentY)
        End If
    End Sub

    Private Sub ResetGambarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResetGambarToolStripMenuItem.Click
        PictureBox1.Image = CType(My.Resources.ResourceManager.GetObject("gambar_2"), Image)
        MyArrow.Clear()
    End Sub

    Dim skorskrining1 As Integer
    Private Sub chk1_0_CheckedChanged(sender As Object, e As EventArgs) Handles chk1_0.CheckedChanged
        If chk1_0.Checked Then
            chk1_2.Checked = False
            chk1_YA.Checked = False
            chk1_YA_1.Checked = False
            chk1_YA_2.Checked = False
            chk1_YA_3.Checked = False
            chk1_YA_4.Checked = False

            skorskrining1 = 0
        Else
            skorskrining1 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Private Sub chk1_2_CheckedChanged(sender As Object, e As EventArgs) Handles chk1_2.CheckedChanged
        If chk1_2.Checked Then
            chk1_0.Checked = False
            chk1_YA.Checked = False
            chk1_YA_1.Checked = False
            chk1_YA_2.Checked = False
            chk1_YA_3.Checked = False
            chk1_YA_4.Checked = False

            skorskrining1 = 2
        Else
            skorskrining1 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Private Sub chk1_YA_1_CheckedChanged(sender As Object, e As EventArgs) Handles chk1_YA_1.CheckedChanged
        If chk1_YA_1.Checked Then
            chk1_0.Checked = False
            chk1_2.Checked = False
            chk1_YA.Checked = True
            chk1_YA_2.Checked = False
            chk1_YA_3.Checked = False
            chk1_YA_4.Checked = False

            skorskrining1 = 1
        Else
            skorskrining1 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Private Sub chk1_YA_2_CheckedChanged(sender As Object, e As EventArgs) Handles chk1_YA_2.CheckedChanged
        If chk1_YA_2.Checked Then
            chk1_0.Checked = False
            chk1_2.Checked = False
            chk1_YA.Checked = True
            chk1_YA_1.Checked = False
            chk1_YA_3.Checked = False
            chk1_YA_4.Checked = False

            skorskrining1 = 2
        Else
            skorskrining1 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Private Sub chk1_YA_3_CheckedChanged(sender As Object, e As EventArgs) Handles chk1_YA_3.CheckedChanged
        If chk1_YA_3.Checked Then
            chk1_0.Checked = False
            chk1_2.Checked = False
            chk1_YA.Checked = True
            chk1_YA_1.Checked = False
            chk1_YA_2.Checked = False
            chk1_YA_4.Checked = False

            skorskrining1 = 3
        Else
            skorskrining1 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Private Sub chk1_YA_4_CheckedChanged(sender As Object, e As EventArgs) Handles chk1_YA_4.CheckedChanged
        If chk1_YA_4.Checked Then
            chk1_0.Checked = False
            chk1_2.Checked = False
            chk1_YA.Checked = True
            chk1_YA_1.Checked = False
            chk1_YA_3.Checked = False
            chk1_YA_2.Checked = False

            skorskrining1 = 4
        Else
            skorskrining1 = 0
        End If
        fn_JumlahSkor()
    End Sub


    Dim skorskrining2 As Integer
    Private Sub chk2_0_CheckedChanged(sender As Object, e As EventArgs) Handles chk2_0.CheckedChanged
        If chk2_0.Checked Then
            chk2_1.Checked = False

            skorskrining2 = 0
        Else
            skorskrining2 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Private Sub chk2_1_CheckedChanged(sender As Object, e As EventArgs) Handles chk2_1.CheckedChanged
        If chk2_1.Checked Then
            chk2_0.Checked = False

            skorskrining2 = 1
        Else
            skorskrining2 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Dim skorskrining3 As Integer

    Private Sub txtSkor3_EditValueChanged(sender As Object, e As EventArgs) Handles txtSkor3.EditValueChanged
        If txtSkor3.Text IsNot String.Empty Then
            skorskrining3 = CInt(txtSkor3.Text)
        End If
        fn_JumlahSkor()
    End Sub

    Private Sub fn_JumlahSkor()
        txtSkor1.Text = skorskrining1
        txtSkor2.Text = skorskrining2

        txtTOTALSKOR.Text = skorskrining1 + skorskrining2 + skorskrining3
    End Sub


    Dim skorPersepsiSensori As Integer
    Private Sub chkPresepsiSensori_1_CheckedChanged(sender As Object, e As EventArgs) Handles chkPresepsiSensori_1.CheckedChanged
        If chkPresepsiSensori_1.Checked Then
            chkPresepsiSensori_2.Checked = False
            chkPresepsiSensori_3.Checked = False
            chkPresepsiSensori_4.Checked = False

            skorPersepsiSensori = 1
        Else
            skorPersepsiSensori = 0
        End If
        fn_JumlahSkorBradenScale()
    End Sub

    Private Sub chkPresepsiSensori_2_CheckedChanged(sender As Object, e As EventArgs) Handles chkPresepsiSensori_2.CheckedChanged
        If chkPresepsiSensori_2.Checked Then
            chkPresepsiSensori_1.Checked = False
            chkPresepsiSensori_3.Checked = False
            chkPresepsiSensori_4.Checked = False

            skorPersepsiSensori = 2
        Else
            skorPersepsiSensori= 0
        End If
        fn_JumlahSkorBradenScale()
    End Sub

    Private Sub chkPresepsiSensori_3_CheckedChanged(sender As Object, e As EventArgs) Handles chkPresepsiSensori_3.CheckedChanged
        If chkPresepsiSensori_3.Checked Then
            chkPresepsiSensori_1.Checked = False
            chkPresepsiSensori_2.Checked = False
            chkPresepsiSensori_4.Checked = False

            skorPersepsiSensori = 3
        Else
            skorPersepsiSensori = 0
        End If
        fn_JumlahSkorBradenScale()
    End Sub

    Private Sub chkPresepsiSensori_4_CheckedChanged(sender As Object, e As EventArgs) Handles chkPresepsiSensori_4.CheckedChanged
        If chkPresepsiSensori_4.Checked Then
            chkPresepsiSensori_1.Checked = False
            chkPresepsiSensori_2.Checked = False
            chkPresepsiSensori_3.Checked = False

            skorPersepsiSensori = 4
        Else
            skorPersepsiSensori = 0
        End If
        fn_JumlahSkorBradenScale()
    End Sub

    Dim skorKelembaban As Integer
    Private Sub chkKelembaban_1_CheckedChanged(sender As Object, e As EventArgs) Handles chkKelembaban_1.CheckedChanged
        If chkKelembaban_1.Checked Then
            chkKelembaban_2.Checked = False
            chkKelembaban_3.Checked = False
            chkKelembaban_4.Checked = False

            skorKelembaban = 1
        Else
            skorKelembaban = 0
        End If
        fn_JumlahSkorBradenScale()
    End Sub

    Private Sub chkKelembaban_2_CheckedChanged(sender As Object, e As EventArgs) Handles chkKelembaban_2.CheckedChanged
        If chkKelembaban_2.Checked Then
            chkKelembaban_1.Checked = False
            chkKelembaban_3.Checked = False
            chkKelembaban_4.Checked = False

            skorKelembaban = 2
        Else
            skorKelembaban = 0
        End If
        fn_JumlahSkorBradenScale()
    End Sub

    Private Sub chkKelembaban_3_CheckedChanged(sender As Object, e As EventArgs) Handles chkKelembaban_3.CheckedChanged
        If chkKelembaban_3.Checked Then
            chkKelembaban_1.Checked = False
            chkKelembaban_2.Checked = False
            chkKelembaban_4.Checked = False

            skorKelembaban = 3
        Else
            skorKelembaban = 0
        End If
        fn_JumlahSkorBradenScale()
    End Sub

    Private Sub chkKelembaban_4_CheckedChanged(sender As Object, e As EventArgs) Handles chkKelembaban_4.CheckedChanged
        If chkKelembaban_4.Checked Then
            chkKelembaban_1.Checked = False
            chkKelembaban_2.Checked = False
            chkKelembaban_3.Checked = False

            skorKelembaban = 4
        Else
            skorKelembaban = 0
        End If
        fn_JumlahSkorBradenScale()
    End Sub

    Dim skorAktivitas As Integer
    Private Sub chkAktivitas_1_CheckedChanged(sender As Object, e As EventArgs) Handles chkAktivitas_1.CheckedChanged
        If chkAktivitas_1.Checked Then
            chkAktivitas_2.Checked = False
            chkAktivitas_3.Checked = False
            chkAktivitas_4.Checked = False

            skorAktivitas = 1
        Else
            skorAktivitas = 0
        End If
        fn_JumlahSkorBradenScale()
    End Sub

    Private Sub chkAktivitas_2_CheckedChanged(sender As Object, e As EventArgs) Handles chkAktivitas_2.CheckedChanged
        If chkAktivitas_2.Checked Then
            chkAktivitas_1.Checked = False
            chkAktivitas_3.Checked = False
            chkAktivitas_4.Checked = False

            skorAktivitas = 2
        Else
            skorAktivitas = 0
        End If
        fn_JumlahSkorBradenScale()
    End Sub

    Private Sub chkAktivitas_3_CheckedChanged(sender As Object, e As EventArgs) Handles chkAktivitas_3.CheckedChanged
        If chkAktivitas_3.Checked Then
            chkAktivitas_1.Checked = False
            chkAktivitas_2.Checked = False
            chkAktivitas_4.Checked = False

            skorAktivitas = 3
        Else
            skorAktivitas = 0
        End If
        fn_JumlahSkorBradenScale()
    End Sub

    Private Sub chkAktivitas_4_CheckedChanged(sender As Object, e As EventArgs) Handles chkAktivitas_4.CheckedChanged
        If chkAktivitas_4.Checked Then
            chkAktivitas_1.Checked = False
            chkAktivitas_2.Checked = False
            chkAktivitas_3.Checked = False

            skorAktivitas = 4
        Else
            skorAktivitas = 0
        End If
        fn_JumlahSkorBradenScale()
    End Sub

    Dim skorMobilitas As Integer
    Private Sub chkMobilitas_1_CheckedChanged(sender As Object, e As EventArgs) Handles chkMobilitas_1.CheckedChanged
        If chkMobilitas_1.Checked Then
            chkMobilitas_2.Checked = False
            chkMobilitas_3.Checked = False
            chkMobilitas_4.Checked = False

            skorMobilitas = 1
        Else
            skorMobilitas = 0
        End If
        fn_JumlahSkorBradenScale()
    End Sub

    Private Sub chkMobilitas_2_CheckedChanged(sender As Object, e As EventArgs) Handles chkMobilitas_2.CheckedChanged
        If chkMobilitas_2.Checked Then
            chkMobilitas_1.Checked = False
            chkMobilitas_3.Checked = False
            chkMobilitas_4.Checked = False

            skorMobilitas = 2
        Else
            skorMobilitas = 0
        End If
        fn_JumlahSkorBradenScale()
    End Sub

    Private Sub chkMobilitas_3_CheckedChanged(sender As Object, e As EventArgs) Handles chkMobilitas_3.CheckedChanged
        If chkMobilitas_3.Checked Then
            chkMobilitas_1.Checked = False
            chkMobilitas_2.Checked = False
            chkMobilitas_4.Checked = False

            skorMobilitas = 3
        Else
            skorMobilitas = 0
        End If
        fn_JumlahSkorBradenScale()
    End Sub

    Private Sub chkMobilitas_4_CheckedChanged(sender As Object, e As EventArgs) Handles chkMobilitas_4.CheckedChanged
        If chkMobilitas_4.Checked Then
            chkMobilitas_1.Checked = False
            chkMobilitas_2.Checked = False
            chkMobilitas_3.Checked = False

            skorMobilitas = 4
        Else
            skorMobilitas = 0
        End If
        fn_JumlahSkorBradenScale()
    End Sub


    Dim skorNutrisi As Integer
    Private Sub chkNutrisi_1_CheckedChanged(sender As Object, e As EventArgs) Handles chkNutrisi_1.CheckedChanged
        If chkNutrisi_1.Checked Then
            chkNutrisi_2.Checked = False
            chkNutrisi_3.Checked = False
            chkNutrisi_4.Checked = False

            skorNutrisi = 1
        Else
            skorNutrisi = 0
        End If
        fn_JumlahSkorBradenScale()
    End Sub

    Private Sub chkNutrisi_2_CheckedChanged(sender As Object, e As EventArgs) Handles chkNutrisi_2.CheckedChanged
        If chkNutrisi_2.Checked Then
            chkNutrisi_1.Checked = False
            chkNutrisi_3.Checked = False
            chkNutrisi_4.Checked = False

            skorNutrisi = 2
        Else
            skorNutrisi = 0
        End If
        fn_JumlahSkorBradenScale()
    End Sub

    Private Sub chkNutrisi_3_CheckedChanged(sender As Object, e As EventArgs) Handles chkNutrisi_3.CheckedChanged
        If chkNutrisi_3.Checked Then
            chkNutrisi_1.Checked = False
            chkNutrisi_2.Checked = False
            chkNutrisi_4.Checked = False

            skorNutrisi = 3
        Else
            skorNutrisi = 0
        End If
        fn_JumlahSkorBradenScale()
    End Sub

    Private Sub chkNutrisi_4_CheckedChanged(sender As Object, e As EventArgs) Handles chkNutrisi_4.CheckedChanged
        If chkNutrisi_4.Checked Then
            chkNutrisi_1.Checked = False
            chkNutrisi_2.Checked = False
            chkNutrisi_3.Checked = False

            skorNutrisi = 4
        Else
            skorNutrisi = 0
        End If
        fn_JumlahSkorBradenScale()
    End Sub

    Dim skorGesekan As Integer
    Private Sub chkGesekan_1_CheckedChanged(sender As Object, e As EventArgs) Handles chkGesekan_1.CheckedChanged
        If chkGesekan_1.Checked Then
            chkGesekan_2.Checked = False
            chkGesekan_3.Checked = False

            skorGesekan = 1
        Else
            skorGesekan = 0
        End If
        fn_JumlahSkorBradenScale()
    End Sub

    Private Sub chkGesekan_2_CheckedChanged(sender As Object, e As EventArgs) Handles chkGesekan_2.CheckedChanged
        If chkGesekan_2.Checked Then
            chkGesekan_1.Checked = False
            chkGesekan_3.Checked = False

            skorGesekan = 2
        Else
            skorGesekan = 0
        End If
        fn_JumlahSkorBradenScale()
    End Sub

    Private Sub chkGesekan_3_CheckedChanged(sender As Object, e As EventArgs) Handles chkGesekan_3.CheckedChanged
        If chkGesekan_3.Checked Then
            chkGesekan_1.Checked = False
            chkGesekan_2.Checked = False

            skorGesekan = 3
        Else
            skorGesekan = 0
        End If
        fn_JumlahSkorBradenScale()
    End Sub

    Private Sub CopyAsesmenKeperawatanPasienGeriatriLembar14ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyAsesmenKeperawatanPasienGeriatriLembar14ToolStripMenuItem.Click
        If grv_Riwayat_Geriatri14.GetFocusedRowCellValue("KDKUNJUNGAN") Is Nothing Then
            Exit Sub
        End If

        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_ASKEPGERIATRI_1_2.GetData(grv_Riwayat_Geriatri14.GetFocusedRowCellValue("KDKUNJUNGAN"))
            With ds

                deDATE.DateTime = .DATE

                txtKEADAANUMUM.Text = .S_DIGITAL_1
                txtKESADARAN.Text = .S_DIGITAL_2
                txtTINGGIBADAN.Text = .S_DIGITAL_3
                txtTINGGIBADANLUTUT.Text = .S_DIGITAL_4
                txtBERATBADAN.Text = .S_DIGITAL_5
                txtTD.Text = .S_DIGITAL_6
                txtPERNAPASAN.Text = .S_DIGITAL_7
                txtNADI.Text = .S_DIGITAL_8
                txtSUHU.Text = .S_DIGITAL_9
                txtSATURASI.Text = .S_DIGITAL_24
                chkSKALANYERI_00.Checked = .S_DIGITAL_10
                chkSKALANYERI_01.Checked = .S_DIGITAL_11
                chkSKALANYERI_02.Checked = .S_DIGITAL_12
                chkSKALANYERI_03.Checked = .S_DIGITAL_13
                chkSKALANYERI_04.Checked = .S_DIGITAL_14
                chkSKALANYERI_05.Checked = .S_DIGITAL_15
                chkSKALANYERI_06.Checked = .S_DIGITAL_16
                chkSKALANYERI_07.Checked = .S_DIGITAL_17
                chkSKALANYERI_08.Checked = .S_DIGITAL_18
                chkSKALANYERI_09.Checked = .S_DIGITAL_19
                chkSKALANYERI_10.Checked = .S_DIGITAL_20
                CheckEdit1.Checked = .S_DIGITAL_21
                CheckEdit2.Checked = .S_DIGITAL_22
                CheckEdit3.Checked = .S_DIGITAL_23
                CheckEdit4.Checked = .S_DIGITAL_25
                CheckEdit5.Checked = .S_DIGITAL_26
                CheckEdit6.Checked = .S_DIGITAL_27
                CheckEdit7.Checked = .S_DIGITAL_28
                CheckEdit8.Checked = .S_DIGITAL_29
                CheckEdit9.Checked = .S_DIGITAL_30
                CheckEdit10.Checked = .S_DIGITAL_31
                CheckEdit11.Checked = .S_DIGITAL_32
                CheckEdit12.Checked = .S_DIGITAL_33
                CheckEdit13.Checked = .S_DIGITAL_34
                CheckEdit14.Checked = .S_DIGITAL_35
                CheckEdit15.Checked = .S_DIGITAL_36
                TextEdit2.Text = .S_DIGITAL_37
                CheckEdit16.Checked = .S_DIGITAL_38
                CheckEdit17.Checked = .S_DIGITAL_39
                TextEdit3.Text = .S_DIGITAL_40
                TextEdit4.Text = .S_DIGITAL_41
                TextEdit5.Text = .S_DIGITAL_42
                CheckEdit18.Checked = .S_DIGITAL_43
                CheckEdit19.Checked = .S_DIGITAL_44
                CheckEdit20.Checked = .S_DIGITAL_45
                CheckEdit21.Checked = .S_DIGITAL_46
                CheckEdit22.Checked = .S_DIGITAL_47
                TextEdit6.Text = .S_DIGITAL_48
                CheckEdit23.Checked = .S_DIGITAL_49
                TextEdit7.Text = .S_DIGITAL_50
                CheckEdit24.Checked = .S_DIGITAL_51
                CheckEdit25.Checked = .S_DIGITAL_52
                CheckEdit26.Checked = .S_DIGITAL_53
                CheckEdit27.Checked = .S_DIGITAL_54
                CheckEdit28.Checked = .S_DIGITAL_55
                CheckEdit29.Checked = .S_DIGITAL_56
                CheckEdit31.Checked = .S_DIGITAL_57
                CheckEdit30.Checked = .S_DIGITAL_58
                CheckEdit32.Checked = .S_DIGITAL_59
                CheckEdit33.Checked = .S_DIGITAL_60
                CheckEdit34.Checked = .S_DIGITAL_61
                CheckEdit35.Checked = .S_DIGITAL_62
                CheckEdit36.Checked = .S_DIGITAL_63
                txtKELUHANUTAMA.Text = .S_DIGITAL_64
                CheckEdit37.Checked = .S_DIGITAL_65
                CheckEdit38.Checked = .S_DIGITAL_66
                CheckEdit39.Checked = .S_DIGITAL_67
                CheckEdit40.Checked = .S_DIGITAL_68
                CheckEdit41.Checked = .S_DIGITAL_69
                TextEdit8.Text = .S_DIGITAL_70
                CheckEdit43.Checked = .S_DIGITAL_71
                CheckEdit44.Checked = .S_DIGITAL_72
                CheckEdit45.Checked = .S_DIGITAL_73
                CheckEdit46.Checked = .S_DIGITAL_74
                CheckEdit47.Checked = .S_DIGITAL_75
                TextEdit9.Text = .S_DIGITAL_76
                CheckEdit48.Checked = .S_DIGITAL_77
                CheckEdit49.Checked = .S_DIGITAL_78
                TextEdit10.Text = .S_DIGITAL_79
                CheckEdit51.Checked = .S_DIGITAL_80
                CheckEdit52.Checked = .S_DIGITAL_81
                CheckEdit50.Checked = .S_DIGITAL_82
                TextEdit11.Text = .S_DIGITAL_83
                CheckEdit54.Checked = .S_DIGITAL_84
                CheckEdit53.Checked = .S_DIGITAL_85
                TextEdit12.Text = .S_DIGITAL_86
                CheckEdit55.Checked = .S_DIGITAL_87
                CheckEdit56.Checked = .S_DIGITAL_88
                CheckEdit57.Checked = .S_DIGITAL_89
                CheckEdit58.Checked = .S_DIGITAL_90
                TextEdit13.Text = .S_DIGITAL_91
                CheckEdit60.Checked = .S_DIGITAL_92
                CheckEdit61.Checked = .S_DIGITAL_93
                CheckEdit62.Checked = .S_DIGITAL_94
                CheckEdit63.Checked = .S_DIGITAL_95
                CheckEdit59.Checked = .S_DIGITAL_96
                CheckEdit64.Checked = .S_DIGITAL_97
                CheckEdit65.Checked = .S_DIGITAL_98
                TextEdit14.Text = .S_DIGITAL_99
                CheckEdit66.Checked = .S_DIGITAL_100
                CheckEdit67.Checked = .S_DIGITAL_101
                CheckEdit68.Checked = .S_DIGITAL_102
                CheckEdit69.Checked = .S_DIGITAL_103
                CheckEdit70.Checked = .S_DIGITAL_104
                CheckEdit71.Checked = .S_DIGITAL_105
                TextEdit15.Text = .S_DIGITAL_106
                CheckEdit72.Checked = .S_DIGITAL_107
                CheckEdit73.Checked = .S_DIGITAL_108
                CheckEdit74.Checked = .S_DIGITAL_109
                CheckEdit75.Checked = .S_DIGITAL_110
                CheckEdit76.Checked = .S_DIGITAL_111
                CheckEdit77.Checked = .S_DIGITAL_112
                CheckEdit78.Checked = .S_DIGITAL_113
                chkHAID_YA.Checked = .S_DIGITAL_114
                chkHAID_TIDAK.Checked = .S_DIGITAL_115
                CheckEdit79.Checked = .S_DIGITAL_116
                CheckEdit80.Checked = .S_DIGITAL_117
                CheckEdit81.Checked = .S_DIGITAL_118
                CheckEdit82.Checked = .S_DIGITAL_119
                TextEdit16.Text = .S_DIGITAL_120
                CheckEdit83.Checked = .S_DIGITAL_121
                CheckEdit84.Checked = .S_DIGITAL_122
                CheckEdit85.Checked = .S_DIGITAL_123
                CheckEdit86.Checked = .S_DIGITAL_124
                chkPRODUKSI_ADA.Checked = .S_DIGITAL_125
                chkPRODUKSI_TIDAK.Checked = .S_DIGITAL_126
                CheckEdit87.Checked = .S_DIGITAL_127
                TextEdit17.Text = .S_DIGITAL_128
                CheckEdit88.Checked = .S_DIGITAL_129
                CheckEdit89.Checked = .S_DIGITAL_130
                CheckEdit90.Checked = .S_DIGITAL_131
                CheckEdit91.Checked = .S_DIGITAL_132
                TextEdit18.Text = .S_DIGITAL_133
                CheckEdit92.Checked = .S_DIGITAL_134
                CheckEdit93.Checked = .S_DIGITAL_135
                CheckEdit94.Checked = .S_DIGITAL_136
                CheckEdit95.Checked = .S_DIGITAL_137
                CheckEdit96.Checked = .S_DIGITAL_138
                CheckEdit97.Checked = .S_DIGITAL_139
                TextEdit19.Text = .S_DIGITAL_140
                CheckEdit100.Checked = .S_DIGITAL_141
                CheckEdit101.Checked = .S_DIGITAL_142
                CheckEdit102.Checked = .S_DIGITAL_143
                CheckEdit103.Checked = .S_DIGITAL_144
                CheckEdit104.Checked = .S_DIGITAL_145
                CheckEdit105.Checked = .S_DIGITAL_146
                TextEdit20.Text = .S_DIGITAL_147
                CheckEdit106.Checked = .S_DIGITAL_148
                TextEdit21.Text = .S_DIGITAL_149
                CheckEdit107.Checked = .S_DIGITAL_150
                CheckEdit108.Checked = .S_DIGITAL_151
                CheckEdit109.Checked = .S_DIGITAL_152
                CheckEdit110.Checked = .S_DIGITAL_153
                CheckEdit111.Checked = .S_DIGITAL_154
                CheckEdit112.Checked = .S_DIGITAL_155
                CheckEdit113.Checked = .S_DIGITAL_156
                CheckEdit114.Checked = .S_DIGITAL_157
                CheckEdit115.Checked = .S_DIGITAL_158
                CheckEdit116.Checked = .S_DIGITAL_159
                TextEdit22.Text = .S_DIGITAL_160
                CheckEdit117.Checked = .S_DIGITAL_161
                CheckEdit118.Checked = .S_DIGITAL_162
                CheckEdit119.Checked = .S_DIGITAL_163
                CheckEdit120.Checked = .S_DIGITAL_164
                CheckEdit42.Checked = .S_DIGITAL_274
                CheckEdit121.Checked = .S_DIGITAL_165
                CheckEdit122.Checked = .S_DIGITAL_166
                CheckEdit123.Checked = .S_DIGITAL_167
                TextEdit23.Text = .S_DIGITAL_168
                CheckEdit124.Checked = .S_DIGITAL_169
                CheckEdit125.Checked = .S_DIGITAL_170
                CheckEdit126.Checked = .S_DIGITAL_171
                CheckEdit127.Checked = .S_DIGITAL_172
                CheckEdit128.Checked = .S_DIGITAL_173
                CheckEdit129.Checked = .S_DIGITAL_174
                CheckEdit130.Checked = .S_DIGITAL_175
                CheckEdit131.Checked = .S_DIGITAL_176
                CheckEdit132.Checked = .S_DIGITAL_177
                CheckEdit133.Checked = .S_DIGITAL_178
                CheckEdit134.Checked = .S_DIGITAL_179
                CheckEdit135.Checked = .S_DIGITAL_180
                CheckEdit136.Checked = .S_DIGITAL_181
                CheckEdit137.Checked = .S_DIGITAL_182
                CheckEdit138.Checked = .S_DIGITAL_183
                CheckEdit139.Checked = .S_DIGITAL_184
                CheckEdit140.Checked = .S_DIGITAL_185
                CheckEdit141.Checked = .S_DIGITAL_186
                CheckEdit142.Checked = .S_DIGITAL_187
                TextEdit24.Text = .S_DIGITAL_188
                CheckEdit143.Checked = .S_DIGITAL_189
                CheckEdit144.Checked = .S_DIGITAL_190
                CheckEdit145.Checked = .S_DIGITAL_191
                CheckEdit146.Checked = .S_DIGITAL_192
                CheckEdit147.Checked = .S_DIGITAL_193
                CheckEdit148.Checked = .S_DIGITAL_194
                CheckEdit149.Checked = .S_DIGITAL_195
                CheckEdit150.Checked = .S_DIGITAL_196
                CheckEdit151.Checked = .S_DIGITAL_197
                CheckEdit152.Checked = .S_DIGITAL_198
                CheckEdit153.Checked = .S_DIGITAL_199
                CheckEdit154.Checked = .S_DIGITAL_200
                CheckEdit155.Checked = .S_DIGITAL_201
                CheckEdit156.Checked = .S_DIGITAL_202
                CheckEdit157.Checked = .S_DIGITAL_203
                CheckEdit158.Checked = .S_DIGITAL_204
                CheckEdit159.Checked = .S_DIGITAL_205
                CheckEdit160.Checked = .S_DIGITAL_206
                CheckEdit161.Checked = .S_DIGITAL_207
                TextEdit25.Text = .S_DIGITAL_208
                CheckEdit162.Checked = .S_DIGITAL_209
                CheckEdit163.Checked = .S_DIGITAL_210
                CheckEdit164.Checked = .S_DIGITAL_211
                CheckEdit165.Checked = .S_DIGITAL_212
                CheckEdit166.Checked = .S_DIGITAL_213
                CheckEdit167.Checked = .S_DIGITAL_214
                MemoEdit1.Text = .S_DIGITAL_215
                chkREGULER.Checked = .ISREGULER
                chkIREGULER.Checked = .ISIREGULER

                Try
                    PictureBox1.Image = ByteArrayToImage(.picGambar.ToArray())
                Catch oErr As Exception
                End Try

                TextEdit1.Text = .S_DIGITAL_216
                chk1_0.Checked = .S_DIGITAL_217
                chk1_2.Checked = .S_DIGITAL_218
                chk1_YA.Checked = .S_DIGITAL_219
                chk1_YA_1.Checked = .S_DIGITAL_220
                chk1_YA_2.Checked = .S_DIGITAL_221
                chk1_YA_3.Checked = .S_DIGITAL_222
                chk1_YA_4.Checked = .S_DIGITAL_223
                txtSkor1.Text = .S_DIGITAL_224
                chk2_0.Checked = .S_DIGITAL_225
                chk2_1.Checked = .S_DIGITAL_226
                txtSkor2.Text = .S_DIGITAL_227
                chk3_YA.Checked = .S_DIGITAL_228
                chk3_DM.Checked = .S_DIGITAL_229
                chk3_Kemoterapi.Checked = .S_DIGITAL_230
                chk3_HD.Checked = .S_DIGITAL_231
                chk3_Imun.Checked = .S_DIGITAL_232
                txt3.Text = .S_DIGITAL_233
                txtSkor3.Text = .S_DIGITAL_234
                txtTOTALSKOR.Text = .S_DIGITAL_235
                txtskorBraden.Text = .S_DIGITAL_236
                chkResikoDekubitus_YA.Checked = .S_DIGITAL_237
                chkResikoDekubitus_TIDAK.Checked = .S_DIGITAL_238
                chkTerdapatLuka_YA.Checked = .S_DIGITAL_239
                chkTerdapatLuka_TIDAK.Checked = .S_DIGITAL_240
                chkResikoTinggi.Checked = .S_DIGITAL_241
                chkResikoSedang.Checked = .S_DIGITAL_242
                chkResikoRendah.Checked = .S_DIGITAL_243
                MemoEdit2.Text = .S_DIGITAL_244
                chkPresepsiSensori_1.Checked = .S_DIGITAL_245
                chkPresepsiSensori_2.Checked = .S_DIGITAL_246
                chkPresepsiSensori_3.Checked = .S_DIGITAL_247
                chkPresepsiSensori_4.Checked = .S_DIGITAL_248
                txtSkorPersepsi.Text = .S_DIGITAL_249
                chkKelembaban_1.Checked = .S_DIGITAL_250
                chkKelembaban_2.Checked = .S_DIGITAL_251
                chkKelembaban_3.Checked = .S_DIGITAL_252
                chkKelembaban_4.Checked = .S_DIGITAL_253
                txtSkorKelembaban.Text = .S_DIGITAL_254
                chkAktivitas_1.Checked = .S_DIGITAL_255
                chkAktivitas_2.Checked = .S_DIGITAL_256
                chkAktivitas_3.Checked = .S_DIGITAL_257
                chkAktivitas_4.Checked = .S_DIGITAL_258
                txtSkorAktivitas.Text = .S_DIGITAL_259
                chkMobilitas_1.Checked = .S_DIGITAL_260
                chkMobilitas_2.Checked = .S_DIGITAL_261
                chkMobilitas_3.Checked = .S_DIGITAL_262
                chkMobilitas_4.Checked = .S_DIGITAL_263
                txtSkorMobilitas.Text = .S_DIGITAL_264
                chkNutrisi_1.Checked = .S_DIGITAL_265
                chkNutrisi_2.Checked = .S_DIGITAL_266
                chkNutrisi_3.Checked = .S_DIGITAL_267
                chkNutrisi_4.Checked = .S_DIGITAL_268
                txtSkorNutrisi.Text = .S_DIGITAL_269
                chkGesekan_1.Checked = .S_DIGITAL_270
                chkGesekan_2.Checked = .S_DIGITAL_271
                chkGesekan_3.Checked = .S_DIGITAL_272
                txtSkorGesekan.Text = .S_DIGITAL_273

                sKDUSER = .KDUSER
                sKDUSERSIGNATURE = .KDUSER_SIGNATURE

                'tambahan
                CheckEdit98.Checked = .S_DIGITAL_275
                CheckEdit99.Checked = .S_DIGITAL_276
                CheckEdit168.Checked = .S_DIGITAL_277
                CheckEdit169.Checked = .S_DIGITAL_278
                CheckEdit170.Checked = .S_DIGITAL_279
                CheckEdit171.Checked = .S_DIGITAL_280
                CheckEdit174.Checked = .S_DIGITAL_281
                CheckEdit180.Checked = .S_DIGITAL_282
                CheckEdit181.Checked = .S_DIGITAL_283



            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub fn_JumlahSkorBradenScale()
        txtSkorPersepsi.Text = skorPersepsiSensori
        txtSkorKelembaban.Text = skorKelembaban
        txtSkorAktivitas.Text = skorAktivitas
        txtSkorMobilitas.Text = skorMobilitas
        txtSkorNutrisi.Text = skorNutrisi
        txtSkorGesekan.Text = skorGesekan

        Dim skorBradenScale = skorPersepsiSensori + skorKelembaban + skorAktivitas + skorMobilitas + skorNutrisi + skorGesekan

        txtskorBraden.Text = skorBradenScale

        If skorBradenScale <= 11 Then
            chkResikoTinggi.Checked = True
            chkResikoSedang.Checked = False
            chkResikoRendah.Checked = False
        ElseIf skorBradenScale >= 12 And skorBradenScale <= 14 Then
            chkResikoTinggi.Checked = False
            chkResikoSedang.Checked = True
            chkResikoRendah.Checked = False
        Else
            chkResikoTinggi.Checked = False
            chkResikoSedang.Checked = False
            chkResikoRendah.Checked = True
        End If

    End Sub

#End Region

#Region "Lookup / Event"
    Private Sub fn_LoadAsessmenRawatJalan(ByVal Parameter As String)
        Dim oAsessmenRawatJalan As New Digital.clsDigital_RJ_08
        Dim dsAsessmenRawatJalan = oAsessmenRawatJalan.GetData(Parameter)

        Dim oAsessmenRawatJalan2 As New Digital.clsS_DIGITAL_ASKEP_RAWATJALAN
        Dim dsAsessmenRawatJalan2 = oAsessmenRawatJalan2.GetDatabyKDKUNJUNGAN(Parameter)

        If dsAsessmenRawatJalan IsNot Nothing Then
            txtKELUHANUTAMA.Text = dsAsessmenRawatJalan.KELUHAN_UTAMA
            txtKEADAANUMUM.Text = dsAsessmenRawatJalan.ALERGI_02_02_TEXT
            txtKESADARAN.Text = dsAsessmenRawatJalan.KESADARAN_UMUM
            txtTINGGIBADAN.Text = dsAsessmenRawatJalan.TANDA_VITAL_06
            txtBERATBADAN.Text = dsAsessmenRawatJalan.TANDA_VITAL_05
            txtTD.Text = dsAsessmenRawatJalan.TANDA_VITAL_01
            txtPERNAPASAN.Text = dsAsessmenRawatJalan.TANDA_VITAL_04
            txtNADI.Text = dsAsessmenRawatJalan.TANDA_VITAL_02
            txtSUHU.Text = dsAsessmenRawatJalan.TANDA_VITAL_03
            txtSATURASI.Text = dsAsessmenRawatJalan.TANDA_VITAL_09

        ElseIf dsAsessmenRawatJalan2 IsNot Nothing Then

            txtKELUHANUTAMA.Text = dsAsessmenRawatJalan2.KELUHAN_UTAMA
            txtKEADAANUMUM.Text = dsAsessmenRawatJalan2.KEADAAN
            txtKESADARAN.Text = dsAsessmenRawatJalan2.KESADARAN_UMUM
            txtTINGGIBADAN.Text = dsAsessmenRawatJalan2.TANDA_VITAL_TB
            txtBERATBADAN.Text = dsAsessmenRawatJalan2.TANDA_VITAL_BB
            txtTD.Text = dsAsessmenRawatJalan2.TANDA_VITAL_TD
            txtPERNAPASAN.Text = dsAsessmenRawatJalan2.TANDA_VITAL_R
            txtNADI.Text = dsAsessmenRawatJalan2.TANDA_VITAL_NADI
            txtSUHU.Text = dsAsessmenRawatJalan2.TANDA_VITAL_SUHU
            txtSATURASI.Text = dsAsessmenRawatJalan2.TANDA_VITAL_SPO2

        Else
            Exit Sub
        End If

    End Sub
    Private Sub fn_LoadDataHistory(ByVal KDCUSTOMER As String)
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
            SQL &= "A.KDKUNJUNGAN "
            SQL &= ",A.KDPENDAFTARAN "
            SQL &= ",TANGGAL = A.DATE  "
            SQL &= ",A.KDUSER "
            SQL &= "FROM  "
            SQL &= "S_DIGITAL_ASKEPGERIATRI_1_2 A "
            SQL &= "INNER JOIN R_IDENTITAS_PASIEN B  "
            SQL &= "ON A.KDKUNJUNGAN = B.KDKUNJUNGAN  "
            SQL &= "WHERE b.KDCUSTOMER = '" & KDCUSTOMER & "'  "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "R_GERIATRI12")

            grd_Riwayat_Geriatri14.MainView = grv_Riwayat_Geriatri14
            grd_Riwayat_Geriatri14.DataSource = ds.Tables("R_GERIATRI12")
            grd_Riwayat_Geriatri14.ForceInitialize()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox("Preview Data Dokter: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub frmAskepGeriatri12_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
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