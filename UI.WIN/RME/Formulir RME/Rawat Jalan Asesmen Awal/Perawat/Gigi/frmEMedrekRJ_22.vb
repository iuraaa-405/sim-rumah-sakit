Imports System.Data.SqlClient
Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports QRCoder

Public Class frmEMedrekRJ_22
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_RJ_22 As New Digital.clsDigital_RJ_22
    Private down As Boolean = False
    Private KODEDOCTOR As String
    Private sIsOtority As Boolean = False

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String)
        oFormMode = FormMode

        Dim oPendaftaran As New Identitas.clsIdentitasPasien
        Dim dsPendaftaran = oPendaftaran.GetData(KDREG)

        txtNoRegister.Text = KDREG

        If dsPendaftaran IsNot Nothing Then
            txtNoPasien.Text = dsPendaftaran.KDCUSTOMER
            txtNamaPasien.Text = dsPendaftaran.NAMAPASIEN
            txtNoRegister.Text = dsPendaftaran.KDKUNJUNGAN
            txtUmur.Text = dsPendaftaran.USIA
            If dsPendaftaran.JENISKELAMIN = "L" Then
                chkJKL.Checked = True
                chkJKP.Checked = False
            Else
                chkJKL.Checked = False
                chkJKP.Checked = True
            End If
            txtSukuBangsa.Text = dsPendaftaran.SUKU
            txtAgama.Text = dsPendaftaran.AGAMA
            txtPangkat.Text = dsPendaftaran.PANGKAT
            txtDiagnosaKeperawatan.Text = ""
            txtKesatuan.Text = dsPendaftaran.KESATUAN
            txtPendidikan.Text = dsPendaftaran.PENDIDIKAN
            txtTgl.Text = dsPendaftaran.DATE.ToString("dd/MM/yyyy HH:mm")
            txtNoTelepon.Text = dsPendaftaran.NOMORTELEPON
            grdPerawat.Text = sUserID
            txtAlamat.Text = dsPendaftaran.ALAMAT
            KODEDOCTOR = dsPendaftaran.KDDOKTER

            Try
                Dim gen As New QRCodeGenerator
                Dim data = gen.CreateQrCode(sTandaTanganPasien, QRCodeGenerator.ECCLevel.Q)
                Dim code As New QRCode(data)
                PictureBox1.Image = code.GetGraphic(6)
            Catch ex As Exception
                PictureBox1.Visible = False
            End Try
        Else
            txtNoPasien.ResetText()
            txtNamaPasien.ResetText()
            txtNoRegister.ResetText()
            txtUmur.ResetText()
            chkJKL.Checked = False
            chkJKP.Checked = False
            txtSukuBangsa.ResetText()
            txtAgama.ResetText()
            txtPangkat.ResetText()
            txtDiagnosaKeperawatan.ResetText()
            txtKesatuan.ResetText()
            txtPendidikan.ResetText()
            txtTgl.ResetText()
            txtNoTelepon.ResetText()
            grdPerawat.ResetText()
            txtAlamat.ResetText()
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = EMedrekRJ_22.TITLE
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

        txtKELUHAN_UTAMA.Properties.ReadOnly = Status
        txtKESADARAN_UMUM.Properties.ReadOnly = Status
        txtTANDA_VITAL_01.Properties.ReadOnly = Status
        txtTANDA_VITAL_02.Properties.ReadOnly = Status
        txtTANDA_VITAL_03.Properties.ReadOnly = Status
        txtTANDA_VITAL_04.Properties.ReadOnly = Status
        txtTANDA_VITAL_05.Properties.ReadOnly = Status
        txtTANDA_VITAL_06.Properties.ReadOnly = Status
        chkTANDA_VITAL_REGULER.Properties.ReadOnly = Status
        chkTANDA_VITAL_IREGULER.Properties.ReadOnly = Status
        CheckEdit1.Properties.ReadOnly = Status
        CheckEdit2.Properties.ReadOnly = Status
        txtRIWAYAT_ALERGI.Properties.ReadOnly = Status
        txtRIWAYAT_PENYAKIT.Properties.ReadOnly = Status
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
        CheckEdit121.Properties.ReadOnly = Status
        txtDIGITALRJ_81.Properties.ReadOnly = Status
        txtDIGITALRJ_83.Properties.ReadOnly = Status
        txtDIGITALRJ_85.Properties.ReadOnly = Status
        CheckEdit122.Properties.ReadOnly = Status
        txtDIGITALRJ_82.Properties.ReadOnly = Status
        txtDIGITALRJ_84.Properties.ReadOnly = Status
        txtDIGITALRJ_86.Properties.ReadOnly = Status
        '
        CheckEdit3.Properties.ReadOnly = Status
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
        CheckEdit16.Properties.ReadOnly = Status
        CheckEdit17.Properties.ReadOnly = Status
        CheckEdit18.Properties.ReadOnly = Status
        CheckEdit19.Properties.ReadOnly = Status
        CheckEdit20.Properties.ReadOnly = Status
        CheckEdit21.Properties.ReadOnly = Status
        CheckEdit22.Properties.ReadOnly = Status
        CheckEdit23.Properties.ReadOnly = Status
        CheckEdit24.Properties.ReadOnly = Status
        CheckEdit25.Properties.ReadOnly = Status
        CheckEdit26.Properties.ReadOnly = Status
        CheckEdit27.Properties.ReadOnly = Status
        CheckEdit28.Properties.ReadOnly = Status
        CheckEdit29.Properties.ReadOnly = Status
        CheckEdit30.Properties.ReadOnly = Status
        CheckEdit31.Properties.ReadOnly = Status
        CheckEdit32.Properties.ReadOnly = Status
        CheckEdit33.Properties.ReadOnly = Status
        CheckEdit34.Properties.ReadOnly = Status
        CheckEdit35.Properties.ReadOnly = Status
        CheckEdit36.Properties.ReadOnly = Status
        CheckEdit37.Properties.ReadOnly = Status
        CheckEdit50.Properties.ReadOnly = Status
        CheckEdit51.Properties.ReadOnly = Status
        CheckEdit52.Properties.ReadOnly = Status
        CheckEdit53.Properties.ReadOnly = Status
        CheckEdit54.Properties.ReadOnly = Status
        CheckEdit55.Properties.ReadOnly = Status
        CheckEdit56.Properties.ReadOnly = Status
        CheckEdit57.Properties.ReadOnly = Status
        CheckEdit58.Properties.ReadOnly = Status
        CheckEdit59.Properties.ReadOnly = Status
        CheckEdit60.Properties.ReadOnly = Status
        CheckEdit61.Properties.ReadOnly = Status
        CheckEdit62.Properties.ReadOnly = Status
        CheckEdit63.Properties.ReadOnly = Status
        CheckEdit64.Properties.ReadOnly = Status
        CheckEdit65.Properties.ReadOnly = Status
        CheckEdit66.Properties.ReadOnly = Status
        '
        CheckEdit68.Properties.ReadOnly = Status
        CheckEdit67.Properties.ReadOnly = Status
        CheckEdit70.Properties.ReadOnly = Status
        CheckEdit69.Properties.ReadOnly = Status
        CheckEdit72.Properties.ReadOnly = Status
        CheckEdit71.Properties.ReadOnly = Status
        CheckEdit74.Properties.ReadOnly = Status
        CheckEdit73.Properties.ReadOnly = Status
        CheckEdit76.Properties.ReadOnly = Status
        CheckEdit75.Properties.ReadOnly = Status
        CheckEdit78.Properties.ReadOnly = Status
        CheckEdit77.Properties.ReadOnly = Status
        CheckEdit80.Properties.ReadOnly = Status
        CheckEdit79.Properties.ReadOnly = Status
        CheckEdit82.Properties.ReadOnly = Status
        CheckEdit81.Properties.ReadOnly = Status
        CheckEdit84.Properties.ReadOnly = Status
        CheckEdit83.Properties.ReadOnly = Status
        TextEdit3.Properties.ReadOnly = Status
        CheckEdit86.Properties.ReadOnly = Status
        CheckEdit85.Properties.ReadOnly = Status
        CheckEdit88.Properties.ReadOnly = Status
        CheckEdit87.Properties.ReadOnly = Status
        TextEdit2.Properties.ReadOnly = Status
        CheckEdit90.Properties.ReadOnly = Status
        CheckEdit89.Properties.ReadOnly = Status
        TextEdit1.Properties.ReadOnly = Status
        CheckEdit92.Properties.ReadOnly = Status
        CheckEdit91.Properties.ReadOnly = Status
        TextEdit4.Properties.ReadOnly = Status
        TextEdit6.Properties.ReadOnly = Status
        TextEdit5.Properties.ReadOnly = Status
        CheckEdit93.Properties.ReadOnly = Status
        CheckEdit94.Properties.ReadOnly = Status
        CheckEdit95.Properties.ReadOnly = Status
        CheckEdit97.Properties.ReadOnly = Status
        CheckEdit96.Properties.ReadOnly = Status
        CheckEdit98.Properties.ReadOnly = Status
        TextEdit7.Properties.ReadOnly = Status
        CheckEdit104.Properties.ReadOnly = Status
        CheckEdit103.Properties.ReadOnly = Status
        CheckEdit102.Properties.ReadOnly = Status
        CheckEdit100.Properties.ReadOnly = Status
        CheckEdit101.Properties.ReadOnly = Status
        TextEdit9.Properties.ReadOnly = Status
        CheckEdit99.Properties.ReadOnly = Status
        TextEdit8.Properties.ReadOnly = Status
        CheckEdit110.Properties.ReadOnly = Status
        CheckEdit109.Properties.ReadOnly = Status
        CheckEdit108.Properties.ReadOnly = Status
        CheckEdit106.Properties.ReadOnly = Status
        CheckEdit107.Properties.ReadOnly = Status
        CheckEdit111.Properties.ReadOnly = Status
        CheckEdit105.Properties.ReadOnly = Status
        TextEdit10.Properties.ReadOnly = Status
        CheckEdit112.Properties.ReadOnly = Status
        TextEdit11.Properties.ReadOnly = Status
        CheckEdit120.Properties.ReadOnly = Status
        CheckEdit119.Properties.ReadOnly = Status
        CheckEdit118.Properties.ReadOnly = Status
        CheckEdit116.Properties.ReadOnly = Status
        CheckEdit117.Properties.ReadOnly = Status
        CheckEdit115.Properties.ReadOnly = Status
        TextEdit13.Properties.ReadOnly = Status
        CheckEdit113.Properties.ReadOnly = Status
        TextEdit12.Properties.ReadOnly = Status

        '
        CheckEdit114.Properties.ReadOnly = Status
        CheckEdit123.Properties.ReadOnly = Status
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
        CheckEdit143.Properties.ReadOnly = Status
        CheckEdit144.Properties.ReadOnly = Status
        CheckEdit145.Properties.ReadOnly = Status
        CheckEdit146.Properties.ReadOnly = Status
        CheckEdit147.Properties.ReadOnly = Status
        CheckEdit148.Properties.ReadOnly = Status

        MemoEdit1.Properties.ReadOnly = Status
        MemoEdit2.Properties.ReadOnly = Status
        MemoEdit3.Properties.ReadOnly = Status
        MemoEdit4.Properties.ReadOnly = Status
        MemoEdit5.Properties.ReadOnly = Status
        MemoEdit6.Properties.ReadOnly = Status
        MemoEdit7.Properties.ReadOnly = Status

        CheckEdit149.Properties.ReadOnly = Status
        CheckEdit150.Properties.ReadOnly = Status
        CheckEdit151.Properties.ReadOnly = Status
        CheckEdit152.Properties.ReadOnly = Status
        CheckEdit153.Properties.ReadOnly = Status
        CheckEdit154.Properties.ReadOnly = Status
        CheckEdit155.Properties.ReadOnly = Status
        TextEdit14.Properties.ReadOnly = Status
        CheckEdit156.Properties.ReadOnly = Status
        TextEdit15.Properties.ReadOnly = Status
        CheckEdit157.Properties.ReadOnly = Status
        CheckEdit158.Properties.ReadOnly = Status
        CheckEdit159.Properties.ReadOnly = Status
        CheckEdit160.Properties.ReadOnly = Status
        CheckEdit161.Properties.ReadOnly = Status

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

        txtKELUHAN_UTAMA.ResetText()
        txtKESADARAN_UMUM.ResetText()
        txtTANDA_VITAL_01.ResetText()
        txtTANDA_VITAL_02.ResetText()
        txtTANDA_VITAL_03.ResetText()
        txtTANDA_VITAL_04.ResetText()
        txtTANDA_VITAL_05.ResetText()
        txtTANDA_VITAL_06.ResetText()
        chkTANDA_VITAL_REGULER.Checked = False
        chkTANDA_VITAL_IREGULER.Checked = False
        CheckEdit1.Checked = False
        CheckEdit2.Checked = False
        txtRIWAYAT_ALERGI.ResetText()
        txtRIWAYAT_PENYAKIT.ResetText()
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
        CheckEdit121.Checked = False
        txtDIGITALRJ_81.ResetText()
        txtDIGITALRJ_83.ResetText()
        txtDIGITALRJ_85.ResetText()
        CheckEdit122.Checked = False
        txtDIGITALRJ_82.ResetText()
        txtDIGITALRJ_84.ResetText()
        txtDIGITALRJ_86.ResetText()

        '
        CheckEdit3.Checked = False
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
        '


        CheckEdit68.Checked = False
        CheckEdit67.Checked = False
        CheckEdit70.Checked = False
        CheckEdit69.Checked = False
        CheckEdit72.Checked = False
        CheckEdit71.Checked = False
        CheckEdit74.Checked = False
        CheckEdit73.Checked = False
        CheckEdit76.Checked = False
        CheckEdit75.Checked = False
        CheckEdit78.Checked = False
        CheckEdit77.Checked = False
        CheckEdit80.Checked = False
        CheckEdit79.Checked = False
        CheckEdit82.Checked = False
        CheckEdit81.Checked = False
        CheckEdit84.Checked = False
        CheckEdit83.Checked = False
        TextEdit3.ResetText()
        CheckEdit86.Checked = False
        CheckEdit85.Checked = False
        CheckEdit88.Checked = False
        CheckEdit87.Checked = False
        TextEdit2.ResetText()
        CheckEdit90.Checked = False
        CheckEdit89.Checked = False
        TextEdit1.ResetText()
        CheckEdit92.Checked = False
        CheckEdit91.Checked = False
        TextEdit4.ResetText()
        TextEdit6.ResetText()
        TextEdit5.ResetText()
        CheckEdit93.Checked = False
        CheckEdit94.Checked = False
        CheckEdit95.Checked = False
        CheckEdit97.Checked = False
        CheckEdit96.Checked = False
        CheckEdit98.Checked = False
        TextEdit7.ResetText()
        CheckEdit104.Checked = False
        CheckEdit103.Checked = False
        CheckEdit102.Checked = False
        CheckEdit100.Checked = False
        CheckEdit101.Checked = False
        TextEdit9.ResetText()
        CheckEdit99.Checked = False
        TextEdit8.ResetText()
        CheckEdit110.Checked = False
        CheckEdit109.Checked = False
        CheckEdit108.Checked = False
        CheckEdit106.Checked = False
        CheckEdit107.Checked = False
        CheckEdit111.Checked = False
        CheckEdit105.Checked = False
        TextEdit10.ResetText()
        CheckEdit112.Checked = False
        TextEdit11.ResetText()
        CheckEdit120.Checked = False
        CheckEdit119.Checked = False
        CheckEdit118.Checked = False
        CheckEdit116.Checked = False
        CheckEdit117.Checked = False
        CheckEdit115.Checked = False
        TextEdit13.ResetText()
        CheckEdit113.Checked = False
        TextEdit12.ResetText()

        '
        CheckEdit114.Checked = False
        CheckEdit123.Checked = False
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
        CheckEdit143.Checked = False
        CheckEdit144.Checked = False
        CheckEdit145.Checked = False
        CheckEdit146.Checked = False
        CheckEdit147.Checked = False
        CheckEdit148.Checked = False

        MemoEdit1.ResetText()
        MemoEdit2.ResetText()
        MemoEdit3.ResetText()
        MemoEdit4.ResetText()
        MemoEdit5.ResetText()
        MemoEdit6.ResetText()
        MemoEdit7.ResetText()

        CheckEdit149.Checked = False
        CheckEdit150.Checked = False
        CheckEdit151.Checked = False
        CheckEdit152.Checked = False
        CheckEdit153.Checked = False
        CheckEdit154.Checked = False
        CheckEdit155.Checked = False
        TextEdit14.ResetText()
        CheckEdit156.Checked = False
        TextEdit15.ResetText()
        CheckEdit157.Checked = False
        CheckEdit158.Checked = False
        CheckEdit159.Checked = False
        CheckEdit160.Checked = False
        CheckEdit161.Checked = False
        '

        deDATE.DateTime = Now
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RJ_22.GetData(txtNoRegister.Text)

            With ds

                txtKELUHAN_UTAMA.Text = .SDIGITALRJ22_1
                txtKESADARAN_UMUM.Text = .SDIGITALRJ22_2
                txtTANDA_VITAL_01.Text = .SDIGITALRJ22_3
                txtTANDA_VITAL_02.Text = .SDIGITALRJ22_4
                txtTANDA_VITAL_03.Text = .SDIGITALRJ22_5
                txtTANDA_VITAL_04.Text = .SDIGITALRJ22_6
                txtTANDA_VITAL_05.Text = .SDIGITALRJ22_7
                txtTANDA_VITAL_06.Text = .SDIGITALRJ22_8
                chkTANDA_VITAL_REGULER.Checked = .SDIGITALRJ22_9
                chkTANDA_VITAL_IREGULER.Checked = .SDIGITALRJ22_10
                CheckEdit1.Checked = .SDIGITALRJ22_11
                CheckEdit2.Checked = .SDIGITALRJ22_12
                txtRIWAYAT_PENYAKIT.Text = .SDIGITALRJ22_13
                chkSKRINING_GIZI_01_1.Checked = .SDIGITALRJ22_14
                chkSKRINING_GIZI_01_2.Checked = .SDIGITALRJ22_15
                chkSKRINING_GIZI_02_1.Checked = .SDIGITALRJ22_16
                chkSKRINING_GIZI_02_2.Checked = .SDIGITALRJ22_17
                chkSKRINING_GIZI_03_1.Checked = .SDIGITALRJ22_18
                chkSKRINING_GIZI_03_2.Checked = .SDIGITALRJ22_19

                chkSKRINING_RESIKO_JATUH_01_1.Checked = .SDIGITALRJ22_20
                chkSKRINING_RESIKO_JATUH_01_2.Checked = .SDIGITALRJ22_21
                chkSKRINING_RESIKO_JATUH_02_1.Checked = .SDIGITALRJ22_22
                chkSKRINING_RESIKO_JATUH_02_2.Checked = .SDIGITALRJ22_23

                CheckEdit39.Checked = .SDIGITALRJ22_24
                CheckEdit43.Checked = .SDIGITALRJ22_25
                CheckEdit48.Checked = .SDIGITALRJ22_26
                CheckEdit49.Checked = .SDIGITALRJ22_27
                CheckEdit44.Checked = .SDIGITALRJ22_28
                CheckEdit40.Checked = .SDIGITALRJ22_29
                CheckEdit41.Checked = .SDIGITALRJ22_30
                CheckEdit46.Checked = .SDIGITALRJ22_31
                CheckEdit47.Checked = .SDIGITALRJ22_32
                CheckEdit45.Checked = .SDIGITALRJ22_33
                CheckEdit38.Checked = .SDIGITALRJ22_34
                CheckEdit121.Checked = .SDIGITALRJ22_35
                txtDIGITALRJ_81.Text = .SDIGITALRJ22_36
                txtDIGITALRJ_83.Text = .SDIGITALRJ22_37
                txtDIGITALRJ_85.Text = .SDIGITALRJ22_38
                CheckEdit122.Checked = .SDIGITALRJ22_39
                txtDIGITALRJ_82.Text = .SDIGITALRJ22_40
                txtDIGITALRJ_84.Text = .SDIGITALRJ22_41
                txtDIGITALRJ_86.Text = .SDIGITALRJ22_42

                '
                CheckEdit3.Checked = .SDIGITALRJ22_43
                CheckEdit4.Checked = .SDIGITALRJ22_44
                CheckEdit5.Checked = .SDIGITALRJ22_45
                CheckEdit6.Checked = .SDIGITALRJ22_46
                CheckEdit7.Checked = .SDIGITALRJ22_47
                CheckEdit8.Checked = .SDIGITALRJ22_48
                CheckEdit9.Checked = .SDIGITALRJ22_49
                CheckEdit10.Checked = .SDIGITALRJ22_50
                CheckEdit11.Checked = .SDIGITALRJ22_51
                CheckEdit12.Checked = .SDIGITALRJ22_52
                CheckEdit13.Checked = .SDIGITALRJ22_53
                CheckEdit14.Checked = .SDIGITALRJ22_54
                CheckEdit15.Checked = .SDIGITALRJ22_55
                CheckEdit16.Checked = .SDIGITALRJ22_56
                CheckEdit17.Checked = .SDIGITALRJ22_57
                CheckEdit18.Checked = .SDIGITALRJ22_58
                CheckEdit19.Checked = .SDIGITALRJ22_59
                CheckEdit20.Checked = .SDIGITALRJ22_60
                CheckEdit21.Checked = .SDIGITALRJ22_61
                CheckEdit22.Checked = .SDIGITALRJ22_62
                CheckEdit23.Checked = .SDIGITALRJ22_63
                CheckEdit24.Checked = .SDIGITALRJ22_64
                CheckEdit25.Checked = .SDIGITALRJ22_65
                CheckEdit26.Checked = .SDIGITALRJ22_66
                CheckEdit27.Checked = .SDIGITALRJ22_67
                CheckEdit28.Checked = .SDIGITALRJ22_68
                CheckEdit29.Checked = .SDIGITALRJ22_69
                CheckEdit30.Checked = .SDIGITALRJ22_70
                CheckEdit31.Checked = .SDIGITALRJ22_71
                CheckEdit32.Checked = .SDIGITALRJ22_72
                CheckEdit33.Checked = .SDIGITALRJ22_73
                CheckEdit34.Checked = .SDIGITALRJ22_74
                CheckEdit35.Checked = .SDIGITALRJ22_75
                CheckEdit36.Checked = .SDIGITALRJ22_76
                CheckEdit37.Checked = .SDIGITALRJ22_77
                CheckEdit50.Checked = .SDIGITALRJ22_78
                CheckEdit51.Checked = .SDIGITALRJ22_79
                CheckEdit52.Checked = .SDIGITALRJ22_80
                CheckEdit53.Checked = .SDIGITALRJ22_81
                CheckEdit54.Checked = .SDIGITALRJ22_82
                CheckEdit55.Checked = .SDIGITALRJ22_83
                CheckEdit56.Checked = .SDIGITALRJ22_84
                CheckEdit57.Checked = .SDIGITALRJ22_85
                CheckEdit58.Checked = .SDIGITALRJ22_86
                CheckEdit59.Checked = .SDIGITALRJ22_87
                CheckEdit60.Checked = .SDIGITALRJ22_88
                CheckEdit61.Checked = .SDIGITALRJ22_89
                CheckEdit62.Checked = .SDIGITALRJ22_90
                CheckEdit63.Checked = .SDIGITALRJ22_91
                CheckEdit64.Checked = .SDIGITALRJ22_92
                CheckEdit65.Checked = .SDIGITALRJ22_93
                CheckEdit66.Checked = .SDIGITALRJ22_94
                '

                CheckEdit68.Checked = .SDIGITALRJ22_95
                CheckEdit67.Checked = .SDIGITALRJ22_96
                CheckEdit70.Checked = .SDIGITALRJ22_97
                CheckEdit69.Checked = .SDIGITALRJ22_98
                CheckEdit72.Checked = .SDIGITALRJ22_99
                CheckEdit71.Checked = .SDIGITALRJ22_100
                CheckEdit74.Checked = .SDIGITALRJ22_101
                CheckEdit73.Checked = .SDIGITALRJ22_102
                CheckEdit76.Checked = .SDIGITALRJ22_103
                CheckEdit75.Checked = .SDIGITALRJ22_104
                CheckEdit78.Checked = .SDIGITALRJ22_105
                CheckEdit77.Checked = .SDIGITALRJ22_106
                CheckEdit80.Checked = .SDIGITALRJ22_107
                CheckEdit79.Checked = .SDIGITALRJ22_108
                CheckEdit82.Checked = .SDIGITALRJ22_109
                CheckEdit81.Checked = .SDIGITALRJ22_110
                CheckEdit84.Checked = .SDIGITALRJ22_111
                CheckEdit83.Checked = .SDIGITALRJ22_112
                TextEdit3.Text = .SDIGITALRJ22_113
                CheckEdit86.Checked = .SDIGITALRJ22_114
                CheckEdit85.Checked = .SDIGITALRJ22_115
                CheckEdit88.Checked = .SDIGITALRJ22_116
                CheckEdit87.Checked = .SDIGITALRJ22_117
                TextEdit2.Text = .SDIGITALRJ22_118
                CheckEdit90.Checked = .SDIGITALRJ22_119
                CheckEdit89.Checked = .SDIGITALRJ22_120
                TextEdit1.Text = .SDIGITALRJ22_121
                CheckEdit92.Checked = .SDIGITALRJ22_122
                CheckEdit91.Checked = .SDIGITALRJ22_123
                TextEdit4.Text = .SDIGITALRJ22_124
                TextEdit6.Text = .SDIGITALRJ22_125
                TextEdit5.Text = .SDIGITALRJ22_126
                CheckEdit93.Checked = .SDIGITALRJ22_127
                CheckEdit94.Checked = .SDIGITALRJ22_128
                CheckEdit95.Checked = .SDIGITALRJ22_129
                CheckEdit97.Checked = .SDIGITALRJ22_130
                CheckEdit96.Checked = .SDIGITALRJ22_131
                CheckEdit98.Checked = .SDIGITALRJ22_132
                TextEdit7.Text = .SDIGITALRJ22_133
                CheckEdit104.Checked = .SDIGITALRJ22_134
                CheckEdit103.Checked = .SDIGITALRJ22_135
                CheckEdit102.Checked = .SDIGITALRJ22_136
                CheckEdit100.Checked = .SDIGITALRJ22_137
                CheckEdit101.Checked = .SDIGITALRJ22_138
                TextEdit9.Text = .SDIGITALRJ22_139
                CheckEdit99.Checked = .SDIGITALRJ22_140
                TextEdit8.Text = .SDIGITALRJ22_141
                CheckEdit110.Checked = .SDIGITALRJ22_142
                CheckEdit109.Checked = .SDIGITALRJ22_143
                CheckEdit108.Checked = .SDIGITALRJ22_144
                CheckEdit106.Checked = .SDIGITALRJ22_145
                CheckEdit107.Checked = .SDIGITALRJ22_146
                CheckEdit111.Checked = .SDIGITALRJ22_147
                CheckEdit105.Checked = .SDIGITALRJ22_148
                TextEdit10.Text = .SDIGITALRJ22_149
                CheckEdit112.Checked = .SDIGITALRJ22_150
                TextEdit11.Text = .SDIGITALRJ22_151
                CheckEdit120.Checked = .SDIGITALRJ22_152
                CheckEdit119.Checked = .SDIGITALRJ22_153
                CheckEdit118.Checked = .SDIGITALRJ22_154
                CheckEdit116.Checked = .SDIGITALRJ22_155
                CheckEdit117.Checked = .SDIGITALRJ22_156
                CheckEdit115.Checked = .SDIGITALRJ22_157
                TextEdit13.Text = .SDIGITALRJ22_158
                CheckEdit113.Checked = .SDIGITALRJ22_159
                TextEdit12.Text = .SDIGITALRJ22_160
                txtRIWAYAT_ALERGI.Text = .SDIGITALRJ22_161

                Try
                    Dim img = oS_DIGITAL_RJ_22.GetData(txtNoRegister.Text).SDIGITALRJ22_161_TEXT

                    picGambar2.Image = ByteArrayToImage(img.ToArray())

                Catch oErr As Exception
                End Try

                '
                CheckEdit114.Checked = .SDIGITALRJ22_163
                CheckEdit123.Checked = .SDIGITALRJ22_164
                CheckEdit124.Checked = .SDIGITALRJ22_165
                CheckEdit125.Checked = .SDIGITALRJ22_166
                CheckEdit126.Checked = .SDIGITALRJ22_167
                CheckEdit127.Checked = .SDIGITALRJ22_168
                CheckEdit128.Checked = .SDIGITALRJ22_169
                CheckEdit129.Checked = .SDIGITALRJ22_170
                CheckEdit130.Checked = .SDIGITALRJ22_171
                CheckEdit131.Checked = .SDIGITALRJ22_172
                CheckEdit132.Checked = .SDIGITALRJ22_173
                CheckEdit133.Checked = .SDIGITALRJ22_174
                CheckEdit134.Checked = .SDIGITALRJ22_175
                CheckEdit135.Checked = .SDIGITALRJ22_176
                CheckEdit136.Checked = .SDIGITALRJ22_177
                CheckEdit137.Checked = .SDIGITALRJ22_178
                CheckEdit138.Checked = .SDIGITALRJ22_179
                CheckEdit139.Checked = .SDIGITALRJ22_180
                CheckEdit140.Checked = .SDIGITALRJ22_181
                CheckEdit141.Checked = .SDIGITALRJ22_182
                CheckEdit142.Checked = .SDIGITALRJ22_183
                CheckEdit143.Checked = .SDIGITALRJ22_184
                CheckEdit144.Checked = .SDIGITALRJ22_185
                CheckEdit145.Checked = .SDIGITALRJ22_186
                CheckEdit146.Checked = .SDIGITALRJ22_187
                CheckEdit147.Checked = .SDIGITALRJ22_188
                CheckEdit148.Checked = .SDIGITALRJ22_189
                MemoEdit1.Text = .SDIGITALRJ22_190
                MemoEdit2.Text = .SDIGITALRJ22_191
                MemoEdit3.Text = .SDIGITALRJ22_192
                MemoEdit4.Text = .SDIGITALRJ22_193
                MemoEdit5.Text = .SDIGITALRJ22_194
                MemoEdit6.Text = .SDIGITALRJ22_195
                MemoEdit7.Text = .SDIGITALRJ22_196
                CheckEdit149.Checked = .SDIGITALRJ22_197
                CheckEdit150.Checked = .SDIGITALRJ22_198
                CheckEdit151.Checked = .SDIGITALRJ22_199
                CheckEdit152.Checked = .SDIGITALRJ22_200
                CheckEdit153.Checked = .SDIGITALRJ22_201
                CheckEdit154.Checked = .SDIGITALRJ22_202
                CheckEdit155.Checked = .SDIGITALRJ22_203
                TextEdit14.Text = .SDIGITALRJ22_204
                CheckEdit156.Checked = .SDIGITALRJ22_205
                TextEdit15.Text = .SDIGITALRJ22_206
                CheckEdit157.Checked = .SDIGITALRJ22_207
                CheckEdit158.Checked = .SDIGITALRJ22_208
                CheckEdit159.Checked = .SDIGITALRJ22_209
                CheckEdit160.Checked = .SDIGITALRJ22_210
                CheckEdit161.Checked = .SDIGITALRJ22_211

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

                Try
                    Dim gen As New QRCodeGenerator
                    Dim data = gen.CreateQrCode(sTandaTanganPasien, QRCodeGenerator.ECCLevel.Q)
                    Dim code As New QRCode(data)
                    PictureBox1.Image = code.GetGraphic(6)
                Catch ex As Exception
                    PictureBox1.Visible = False
                End Try

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
        Catch oErr As Exception
            MsgBox("Validate Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RJ_22.GetStructureHeader
            With ds
                .KDKUNJUNGAN = txtNoRegister.Text
                Try
                    .DATECREATED = Now
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .DATE = deDATE.DateTime
                .SDIGITALRJ22_1 = txtKELUHAN_UTAMA.Text
                .SDIGITALRJ22_2 = txtKESADARAN_UMUM.Text
                .SDIGITALRJ22_3 = txtTANDA_VITAL_01.Text
                .SDIGITALRJ22_4 = txtTANDA_VITAL_02.Text
                .SDIGITALRJ22_5 = txtTANDA_VITAL_03.Text
                .SDIGITALRJ22_6 = txtTANDA_VITAL_04.Text
                .SDIGITALRJ22_7 = txtTANDA_VITAL_05.Text
                .SDIGITALRJ22_8 = txtTANDA_VITAL_06.Text
                .SDIGITALRJ22_9 = chkTANDA_VITAL_REGULER.Checked
                .SDIGITALRJ22_10 = chkTANDA_VITAL_IREGULER.Checked
                .SDIGITALRJ22_11 = CheckEdit1.Checked
                .SDIGITALRJ22_12 = CheckEdit2.Checked
                .SDIGITALRJ22_13 = txtRIWAYAT_PENYAKIT.Text
                .SDIGITALRJ22_14 = chkSKRINING_GIZI_01_1.Checked
                .SDIGITALRJ22_15 = chkSKRINING_GIZI_01_2.Checked
                .SDIGITALRJ22_16 = chkSKRINING_GIZI_02_1.Checked
                .SDIGITALRJ22_17 = chkSKRINING_GIZI_02_2.Checked
                .SDIGITALRJ22_18 = chkSKRINING_GIZI_03_1.Checked
                .SDIGITALRJ22_19 = chkSKRINING_GIZI_03_2.Checked
                .SDIGITALRJ22_20 = chkSKRINING_RESIKO_JATUH_01_1.Checked
                .SDIGITALRJ22_21 = chkSKRINING_RESIKO_JATUH_01_2.Checked
                .SDIGITALRJ22_22 = chkSKRINING_RESIKO_JATUH_02_1.Checked
                .SDIGITALRJ22_23 = chkSKRINING_RESIKO_JATUH_02_2.Checked
                .SDIGITALRJ22_24 = CheckEdit39.Checked
                .SDIGITALRJ22_25 = CheckEdit43.Checked
                .SDIGITALRJ22_26 = CheckEdit48.Checked
                .SDIGITALRJ22_27 = CheckEdit49.Checked
                .SDIGITALRJ22_28 = CheckEdit44.Checked
                .SDIGITALRJ22_29 = CheckEdit40.Checked
                .SDIGITALRJ22_30 = CheckEdit41.Checked
                .SDIGITALRJ22_31 = CheckEdit46.Checked
                .SDIGITALRJ22_32 = CheckEdit47.Checked
                .SDIGITALRJ22_33 = CheckEdit45.Checked
                .SDIGITALRJ22_34 = CheckEdit38.Checked
                .SDIGITALRJ22_35 = CheckEdit121.Checked
                .SDIGITALRJ22_36 = txtDIGITALRJ_81.Text
                .SDIGITALRJ22_37 = txtDIGITALRJ_83.Text
                .SDIGITALRJ22_38 = txtDIGITALRJ_85.Text
                .SDIGITALRJ22_39 = CheckEdit122.Checked
                .SDIGITALRJ22_40 = txtDIGITALRJ_82.Text
                .SDIGITALRJ22_41 = txtDIGITALRJ_84.Text
                .SDIGITALRJ22_42 = txtDIGITALRJ_86.Text
                '
                .SDIGITALRJ22_43 = CheckEdit3.Checked
                .SDIGITALRJ22_44 = CheckEdit4.Checked
                .SDIGITALRJ22_45 = CheckEdit5.Checked
                .SDIGITALRJ22_46 = CheckEdit6.Checked
                .SDIGITALRJ22_47 = CheckEdit7.Checked
                .SDIGITALRJ22_48 = CheckEdit8.Checked
                .SDIGITALRJ22_49 = CheckEdit9.Checked
                .SDIGITALRJ22_50 = CheckEdit10.Checked
                .SDIGITALRJ22_51 = CheckEdit11.Checked
                .SDIGITALRJ22_52 = CheckEdit12.Checked
                .SDIGITALRJ22_53 = CheckEdit13.Checked
                .SDIGITALRJ22_54 = CheckEdit14.Checked
                .SDIGITALRJ22_55 = CheckEdit15.Checked
                .SDIGITALRJ22_56 = CheckEdit16.Checked
                .SDIGITALRJ22_57 = CheckEdit17.Checked
                .SDIGITALRJ22_58 = CheckEdit18.Checked
                .SDIGITALRJ22_59 = CheckEdit19.Checked
                .SDIGITALRJ22_60 = CheckEdit20.Checked
                .SDIGITALRJ22_61 = CheckEdit21.Checked
                .SDIGITALRJ22_62 = CheckEdit22.Checked
                .SDIGITALRJ22_63 = CheckEdit23.Checked
                .SDIGITALRJ22_64 = CheckEdit24.Checked
                .SDIGITALRJ22_65 = CheckEdit25.Checked
                .SDIGITALRJ22_66 = CheckEdit26.Checked
                .SDIGITALRJ22_67 = CheckEdit27.Checked
                .SDIGITALRJ22_68 = CheckEdit28.Checked
                .SDIGITALRJ22_69 = CheckEdit29.Checked
                .SDIGITALRJ22_70 = CheckEdit30.Checked
                .SDIGITALRJ22_71 = CheckEdit31.Checked
                .SDIGITALRJ22_72 = CheckEdit32.Checked
                .SDIGITALRJ22_73 = CheckEdit33.Checked
                .SDIGITALRJ22_74 = CheckEdit34.Checked
                .SDIGITALRJ22_75 = CheckEdit35.Checked
                .SDIGITALRJ22_76 = CheckEdit36.Checked
                .SDIGITALRJ22_77 = CheckEdit37.Checked
                .SDIGITALRJ22_78 = CheckEdit50.Checked
                .SDIGITALRJ22_79 = CheckEdit51.Checked
                .SDIGITALRJ22_80 = CheckEdit52.Checked
                .SDIGITALRJ22_81 = CheckEdit53.Checked
                .SDIGITALRJ22_82 = CheckEdit54.Checked
                .SDIGITALRJ22_83 = CheckEdit55.Checked
                .SDIGITALRJ22_84 = CheckEdit56.Checked
                .SDIGITALRJ22_85 = CheckEdit57.Checked
                .SDIGITALRJ22_86 = CheckEdit58.Checked
                .SDIGITALRJ22_87 = CheckEdit59.Checked
                .SDIGITALRJ22_88 = CheckEdit60.Checked
                .SDIGITALRJ22_89 = CheckEdit61.Checked
                .SDIGITALRJ22_90 = CheckEdit62.Checked
                .SDIGITALRJ22_91 = CheckEdit63.Checked
                .SDIGITALRJ22_92 = CheckEdit64.Checked
                .SDIGITALRJ22_93 = CheckEdit65.Checked
                .SDIGITALRJ22_94 = CheckEdit66.Checked
                '
                .SDIGITALRJ22_95 = CheckEdit68.Checked
                .SDIGITALRJ22_96 = CheckEdit67.Checked
                .SDIGITALRJ22_97 = CheckEdit70.Checked
                .SDIGITALRJ22_98 = CheckEdit69.Checked
                .SDIGITALRJ22_99 = CheckEdit72.Checked
                .SDIGITALRJ22_100 = CheckEdit71.Checked
                .SDIGITALRJ22_101 = CheckEdit74.Checked
                .SDIGITALRJ22_102 = CheckEdit73.Checked
                .SDIGITALRJ22_103 = CheckEdit76.Checked
                .SDIGITALRJ22_104 = CheckEdit75.Checked
                .SDIGITALRJ22_105 = CheckEdit78.Checked
                .SDIGITALRJ22_106 = CheckEdit77.Checked
                .SDIGITALRJ22_107 = CheckEdit80.Checked
                .SDIGITALRJ22_108 = CheckEdit79.Checked
                .SDIGITALRJ22_109 = CheckEdit82.Checked
                .SDIGITALRJ22_110 = CheckEdit81.Checked
                .SDIGITALRJ22_111 = CheckEdit84.Checked
                .SDIGITALRJ22_112 = CheckEdit83.Checked
                .SDIGITALRJ22_113 = TextEdit3.Text
                .SDIGITALRJ22_114 = CheckEdit86.Checked
                .SDIGITALRJ22_115 = CheckEdit85.Checked
                .SDIGITALRJ22_116 = CheckEdit88.Checked
                .SDIGITALRJ22_117 = CheckEdit87.Checked
                .SDIGITALRJ22_118 = TextEdit2.Text
                .SDIGITALRJ22_119 = CheckEdit90.Checked
                .SDIGITALRJ22_120 = CheckEdit89.Checked
                .SDIGITALRJ22_121 = TextEdit1.Text
                .SDIGITALRJ22_122 = CheckEdit92.Checked
                .SDIGITALRJ22_123 = CheckEdit91.Checked
                .SDIGITALRJ22_124 = TextEdit4.Text
                .SDIGITALRJ22_125 = TextEdit6.Text
                .SDIGITALRJ22_126 = TextEdit5.Text
                .SDIGITALRJ22_127 = CheckEdit93.Checked
                .SDIGITALRJ22_128 = CheckEdit94.Checked
                .SDIGITALRJ22_129 = CheckEdit95.Checked
                .SDIGITALRJ22_130 = CheckEdit97.Checked
                .SDIGITALRJ22_131 = CheckEdit96.Checked
                .SDIGITALRJ22_132 = CheckEdit98.Checked
                .SDIGITALRJ22_133 = TextEdit7.Text
                .SDIGITALRJ22_134 = CheckEdit104.Checked
                .SDIGITALRJ22_135 = CheckEdit103.Checked
                .SDIGITALRJ22_136 = CheckEdit102.Checked
                .SDIGITALRJ22_137 = CheckEdit100.Checked
                .SDIGITALRJ22_138 = CheckEdit101.Checked
                .SDIGITALRJ22_139 = TextEdit9.Text
                .SDIGITALRJ22_140 = CheckEdit99.Checked
                .SDIGITALRJ22_141 = TextEdit8.Text
                .SDIGITALRJ22_142 = CheckEdit110.Checked
                .SDIGITALRJ22_143 = CheckEdit109.Checked
                .SDIGITALRJ22_144 = CheckEdit108.Checked
                .SDIGITALRJ22_145 = CheckEdit106.Checked
                .SDIGITALRJ22_146 = CheckEdit107.Checked
                .SDIGITALRJ22_147 = CheckEdit111.Checked
                .SDIGITALRJ22_148 = CheckEdit105.Checked
                .SDIGITALRJ22_149 = TextEdit10.Text
                .SDIGITALRJ22_150 = CheckEdit112.Checked
                .SDIGITALRJ22_151 = TextEdit11.Text
                .SDIGITALRJ22_152 = CheckEdit120.Checked
                .SDIGITALRJ22_153 = CheckEdit119.Checked
                .SDIGITALRJ22_154 = CheckEdit118.Checked
                .SDIGITALRJ22_155 = CheckEdit116.Checked
                .SDIGITALRJ22_156 = CheckEdit117.Checked
                .SDIGITALRJ22_157 = CheckEdit115.Checked
                .SDIGITALRJ22_158 = TextEdit13.Text
                .SDIGITALRJ22_159 = CheckEdit113.Checked
                .SDIGITALRJ22_160 = TextEdit12.Text
                .SDIGITALRJ22_161 = txtRIWAYAT_ALERGI.Text
                .DOKTER_KODE = grdPerawat.EditValue
                .DOKTER_NAMEDISPLAY = grdPerawat.Text

                Try
                    .CETAK = oS_DIGITAL_RJ_22.GetData(txtNoRegister.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                Try
                    Dim ms As New IO.MemoryStream()
                    picGambar2.Image.Save(ms, picGambar2.Image.RawFormat)

                    Dim data As Byte() = ms.GetBuffer()

                    .SDIGITALRJ22_161_TEXT = data
                Catch oErr As Exception
                    Try
                        .SDIGITALRJ22_161_TEXT = oS_DIGITAL_RJ_22.GetData(txtNoRegister.Text).SDIGITALRJ22_161_TEXT
                    Catch ex As Exception

                    End Try

                End Try
                .SDIGITALRJ22_162_TEXT = txtSDIGITALRJ34_27_TEXT.Text

                Try
                    If sIsOtority = True Then
                        .KDUSER = oS_DIGITAL_RJ_22.GetData(txtNoRegister.Text).KDUSER
                        .KDUSER_SIGNATURE = oS_DIGITAL_RJ_22.GetData(txtNoRegister.Text).KDUSER_SIGNATURE
                    Else
                        .KDUSER = sUserID
                        .KDUSER_SIGNATURE = sUserSIGNATURE
                    End If
                Catch ex As Exception
                    .KDUSER = sUserID
                    .KDUSER_SIGNATURE = sUserSIGNATURE
                End Try

                .SDIGITALRJ22_163 = CheckEdit114.Checked
                .SDIGITALRJ22_164 = CheckEdit123.Checked
                .SDIGITALRJ22_165 = CheckEdit124.Checked
                .SDIGITALRJ22_166 = CheckEdit125.Checked
                .SDIGITALRJ22_167 = CheckEdit126.Checked
                .SDIGITALRJ22_168 = CheckEdit127.Checked
                .SDIGITALRJ22_169 = CheckEdit128.Checked
                .SDIGITALRJ22_170 = CheckEdit129.Checked
                .SDIGITALRJ22_171 = CheckEdit130.Checked
                .SDIGITALRJ22_172 = CheckEdit131.Checked
                .SDIGITALRJ22_173 = CheckEdit132.Checked
                .SDIGITALRJ22_174 = CheckEdit133.Checked
                .SDIGITALRJ22_175 = CheckEdit134.Checked
                .SDIGITALRJ22_176 = CheckEdit135.Checked
                .SDIGITALRJ22_177 = CheckEdit136.Checked
                .SDIGITALRJ22_178 = CheckEdit137.Checked
                .SDIGITALRJ22_179 = CheckEdit138.Checked
                .SDIGITALRJ22_180 = CheckEdit139.Checked
                .SDIGITALRJ22_181 = CheckEdit140.Checked
                .SDIGITALRJ22_182 = CheckEdit141.Checked
                .SDIGITALRJ22_183 = CheckEdit142.Checked
                .SDIGITALRJ22_184 = CheckEdit143.Checked
                .SDIGITALRJ22_185 = CheckEdit144.Checked
                .SDIGITALRJ22_186 = CheckEdit145.Checked
                .SDIGITALRJ22_187 = CheckEdit146.Checked
                .SDIGITALRJ22_188 = CheckEdit147.Checked
                .SDIGITALRJ22_189 = CheckEdit148.Checked
                .SDIGITALRJ22_190 = MemoEdit1.Text
                .SDIGITALRJ22_191 = MemoEdit2.Text
                .SDIGITALRJ22_192 = MemoEdit3.Text
                .SDIGITALRJ22_193 = MemoEdit4.Text
                .SDIGITALRJ22_194 = MemoEdit5.Text
                .SDIGITALRJ22_195 = MemoEdit6.Text
                .SDIGITALRJ22_196 = MemoEdit7.Text
                .SDIGITALRJ22_197 = CheckEdit149.Checked
                .SDIGITALRJ22_198 = CheckEdit150.Checked
                .SDIGITALRJ22_199 = CheckEdit151.Checked
                .SDIGITALRJ22_200 = CheckEdit152.Checked
                .SDIGITALRJ22_201 = CheckEdit153.Checked
                .SDIGITALRJ22_202 = CheckEdit154.Checked
                .SDIGITALRJ22_203 = CheckEdit155.Checked
                .SDIGITALRJ22_204 = TextEdit14.Text
                .SDIGITALRJ22_205 = CheckEdit156.Checked
                .SDIGITALRJ22_206 = TextEdit15.Text
                .SDIGITALRJ22_207 = CheckEdit157.Checked
                .SDIGITALRJ22_208 = CheckEdit158.Checked
                .SDIGITALRJ22_209 = CheckEdit159.Checked
                .SDIGITALRJ22_210 = CheckEdit160.Checked
                .SDIGITALRJ22_211 = CheckEdit161.Checked

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


            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_RJ_22.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_RJ_22.UpdateData(ds)
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

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub btnEDITIMAGE_Click(sender As Object, e As EventArgs) Handles btnEDITIMAGE.Click
        Dim frmPopUp_Image As New frmPopUp_img10
        frmPopUp_Image.fn_LoadMe(picGambar2.Image)
        frmPopUp_Image.ShowDialog()

        If sPicture IsNot Nothing Then
            picGambar2.Image = sPicture
        End If

        picGambar2.Focus()
        sFind10 = String.Empty
        sPicture = Nothing
    End Sub

    Private Sub frmEMedrekRJ_22_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
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