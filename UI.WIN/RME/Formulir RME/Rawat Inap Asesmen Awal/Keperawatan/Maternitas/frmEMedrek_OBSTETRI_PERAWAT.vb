Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmEMedrek_OBSTETRI_PERAWAT
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_ASKEP_PEDIATRIK_03 As New Digital.clsDigital_RI_OBSTETRI_PERAWAT
    Private down As Boolean = False

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String)
        oFormMode = FormMode

        Dim oPendaftaran As New Identitas.clsIdentitasPasien
        Dim dsPendaftaran = oPendaftaran.GetData(KDREG)

        txtKDKUNJUNGAN.Text = KDREG

        If dsPendaftaran IsNot Nothing Then
            txtNamaPasien.Text = dsPendaftaran.NAMAPASIEN.ToString.Trim.ToUpper
            txtJK.Text = dsPendaftaran.JENISKELAMIN
            txtNIK.Text = dsPendaftaran.NIK
            txtTanggalLahir.Text = dsPendaftaran.TANGGALLAHIR.ToString("dd-MM-yyyy HH:mm:ss")
            txtUmur.Text = dsPendaftaran.USIA
            txtPangkat.Text = dsPendaftaran.PANGKAT
            txtNRP.Text = dsPendaftaran.NRP
            txtKesatuan.Text = dsPendaftaran.KESATUAN
            txtNoPasien.Text = dsPendaftaran.KDCUSTOMER
            txtNoTelp.Text = dsPendaftaran.NOMORTELEPON
            txtPendidikan.Text = dsPendaftaran.PENDIDIKAN
            txtSuku.Text = dsPendaftaran.SUKU
            txtTanggalDaftar.Text = dsPendaftaran.DATE.ToString("dd-MM-yyyy HH:mm:ss")
            txtAlamat.Text = dsPendaftaran.ALAMAT
            txtAgama.Text = dsPendaftaran.AGAMA


        Else

            txtNamaPasien.ResetText()
            txtJK.ResetText()
            txtNIK.ResetText()
            txtTanggalLahir.ResetText()
            txtUmur.ResetText()
            txtPangkat.ResetText()
            txtNRP.ResetText()
            txtKesatuan.ResetText()
            txtNoPasien.ResetText()
            txtNoTelp.ResetText()
            txtPendidikan.ResetText()
            txtSuku.ResetText()
            txtTanggalDaftar.ResetText()
            txtAlamat.ResetText()
            txtAgama.ResetText()


        End If

        txtNamaPasien.Properties.ReadOnly = True
        txtJK.Properties.ReadOnly = True
        txtNIK.Properties.ReadOnly = True
        txtTanggalLahir.Properties.ReadOnly = True
        txtUmur.Properties.ReadOnly = True
        txtPangkat.Properties.ReadOnly = True
        txtNRP.Properties.ReadOnly = True
        txtKesatuan.Properties.ReadOnly = True
        txtNoPasien.Properties.ReadOnly = True
        txtNoTelp.Properties.ReadOnly = True
        txtSuku.Properties.ReadOnly = True
        txtAlamat.Properties.ReadOnly = True
        txtAgama.Properties.ReadOnly = True

        DateEdit1.EditValue = Now
        DateEdit2.EditValue = Now

        DateEdit7.EditValue = Now
        DateEdit8.EditValue = Now
        DateEdit9.EditValue = Now
        DateEdit10.EditValue = Now

        DateEdit11.EditValue = Now
        DateEdit12.EditValue = Now
        DateEdit13.EditValue = Now
        DateEdit14.EditValue = Now


    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
        sCode = txtAlamat.Text.Trim.ToUpper
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_ChangeFormState()

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

        DateEdit1.Properties.ReadOnly = Status
        DateEdit2.Properties.ReadOnly = Status
        CheckBox9.Enabled = IIf(Status = True, False, True)
        CheckBox12.Enabled = IIf(Status = True, False, True)
        TextEdit1.Properties.ReadOnly = Status
        TextEdit2.Properties.ReadOnly = Status
        CheckBox10.Enabled = IIf(Status = True, False, True)
        CheckBox15.Enabled = IIf(Status = True, False, True)
        CheckBox11.Enabled = IIf(Status = True, False, True)
        CheckBox13.Enabled = IIf(Status = True, False, True)
        CheckBox14.Enabled = IIf(Status = True, False, True)
        CheckBox1.Enabled = IIf(Status = True, False, True)
        CheckBox2.Enabled = IIf(Status = True, False, True)
        CheckBox3.Enabled = IIf(Status = True, False, True)
        MemoEdit2.Properties.ReadOnly = Status
        MemoEdit3.Properties.ReadOnly = Status
        MemoEdit4.Properties.ReadOnly = Status
        MemoEdit5.Properties.ReadOnly = Status
        MemoEdit6.Properties.ReadOnly = Status
        MemoEdit7.Properties.ReadOnly = Status
        MemoEdit8.Properties.ReadOnly = Status
        TextEdit3.Properties.ReadOnly = Status
        TextEdit4.Properties.ReadOnly = Status
        TextEdit6.Properties.ReadOnly = Status
        TextEdit7.Properties.ReadOnly = Status
        TextEdit8.Properties.ReadOnly = Status
        TextEdit9.Properties.ReadOnly = Status
        TextEdit10.Properties.ReadOnly = Status
        TextEdit11.Properties.ReadOnly = Status
        TextEdit12.Properties.ReadOnly = Status
        TextEdit13.Properties.ReadOnly = Status
        TextEdit14.Properties.ReadOnly = Status
        TextEdit15.Properties.ReadOnly = Status
        CheckBox4.Enabled = IIf(Status = True, False, True)
        CheckBox5.Enabled = IIf(Status = True, False, True)
        TextEdit16.Properties.ReadOnly = Status
        TextEdit17.Properties.ReadOnly = Status
        CheckBox7.Enabled = IIf(Status = True, False, True)
        CheckBox6.Enabled = IIf(Status = True, False, True)
        TextEdit19.Properties.ReadOnly = Status
        TextEdit18.Properties.ReadOnly = Status
        CheckBox16.Enabled = IIf(Status = True, False, True)
        CheckBox8.Enabled = IIf(Status = True, False, True)
        TextEdit21.Properties.ReadOnly = Status
        TextEdit20.Properties.ReadOnly = Status
        CheckBox18.Enabled = IIf(Status = True, False, True)
        CheckBox17.Enabled = IIf(Status = True, False, True)
        CheckBox20.Enabled = IIf(Status = True, False, True)
        CheckBox19.Enabled = IIf(Status = True, False, True)
        CheckBox22.Enabled = IIf(Status = True, False, True)
        CheckBox21.Enabled = IIf(Status = True, False, True)
        CheckBox24.Enabled = IIf(Status = True, False, True)
        CheckBox23.Enabled = IIf(Status = True, False, True)
        TextEdit22.Properties.ReadOnly = Status
        CheckBox25.Enabled = IIf(Status = True, False, True)
        CheckBox26.Enabled = IIf(Status = True, False, True)
        CheckBox28.Enabled = IIf(Status = True, False, True)
        CheckBox27.Enabled = IIf(Status = True, False, True)
        CheckBox29.Enabled = IIf(Status = True, False, True)
        CheckBox30.Enabled = IIf(Status = True, False, True)
        CheckBox31.Enabled = IIf(Status = True, False, True)
        CheckBox34.Enabled = IIf(Status = True, False, True)
        CheckBox33.Enabled = IIf(Status = True, False, True)
        CheckBox32.Enabled = IIf(Status = True, False, True)
        CheckBox77.Enabled = IIf(Status = True, False, True)
        CheckBox76.Enabled = IIf(Status = True, False, True)
        CheckBox75.Enabled = IIf(Status = True, False, True)
        CheckBox81.Enabled = IIf(Status = True, False, True)
        CheckBox80.Enabled = IIf(Status = True, False, True)
        CheckBox73.Enabled = IIf(Status = True, False, True)
        CheckBox36.Enabled = IIf(Status = True, False, True)
        CheckBox35.Enabled = IIf(Status = True, False, True)
        CheckBox79.Enabled = IIf(Status = True, False, True)
        CheckBox78.Enabled = IIf(Status = True, False, True)
        TextEdit23.Properties.ReadOnly = Status
        TextEdit25.Properties.ReadOnly = Status
        TextEdit66.Properties.ReadOnly = Status
        TextEdit24.Properties.ReadOnly = Status
        TextEdit26.Properties.ReadOnly = Status
        TextEdit65.Properties.ReadOnly = Status
        TextEdit64.Properties.ReadOnly = Status
        TextEdit27.Properties.ReadOnly = Status
        TextEdit28.Properties.ReadOnly = Status
        TextEdit70.Properties.ReadOnly = Status
        TextEdit73.Properties.ReadOnly = Status
        TextEdit67.Properties.ReadOnly = Status
        TextEdit82.Properties.ReadOnly = Status
        TextEdit83.Properties.ReadOnly = Status
        TextEdit96.Properties.ReadOnly = Status
        TextEdit95.Properties.ReadOnly = Status
        TextEdit94.Properties.ReadOnly = Status
        TextEdit97.Properties.ReadOnly = Status
        TextEdit93.Properties.ReadOnly = Status
        TextEdit71.Properties.ReadOnly = Status
        TextEdit72.Properties.ReadOnly = Status
        TextEdit68.Properties.ReadOnly = Status
        TextEdit84.Properties.ReadOnly = Status
        TextEdit85.Properties.ReadOnly = Status
        TextEdit92.Properties.ReadOnly = Status
        TextEdit98.Properties.ReadOnly = Status
        TextEdit99.Properties.ReadOnly = Status
        TextEdit100.Properties.ReadOnly = Status
        TextEdit101.Properties.ReadOnly = Status
        TextEdit75.Properties.ReadOnly = Status
        TextEdit74.Properties.ReadOnly = Status
        TextEdit69.Properties.ReadOnly = Status
        TextEdit86.Properties.ReadOnly = Status
        TextEdit87.Properties.ReadOnly = Status
        TextEdit102.Properties.ReadOnly = Status
        TextEdit103.Properties.ReadOnly = Status
        TextEdit104.Properties.ReadOnly = Status
        TextEdit105.Properties.ReadOnly = Status
        TextEdit106.Properties.ReadOnly = Status
        TextEdit76.Properties.ReadOnly = Status
        TextEdit77.Properties.ReadOnly = Status
        TextEdit78.Properties.ReadOnly = Status
        TextEdit88.Properties.ReadOnly = Status
        TextEdit89.Properties.ReadOnly = Status
        TextEdit107.Properties.ReadOnly = Status
        TextEdit108.Properties.ReadOnly = Status
        TextEdit109.Properties.ReadOnly = Status
        TextEdit110.Properties.ReadOnly = Status
        TextEdit111.Properties.ReadOnly = Status
        TextEdit81.Properties.ReadOnly = Status
        TextEdit80.Properties.ReadOnly = Status
        TextEdit79.Properties.ReadOnly = Status
        TextEdit90.Properties.ReadOnly = Status
        TextEdit91.Properties.ReadOnly = Status
        TextEdit115.Properties.ReadOnly = Status
        TextEdit116.Properties.ReadOnly = Status
        TextEdit114.Properties.ReadOnly = Status
        TextEdit112.Properties.ReadOnly = Status
        TextEdit113.Properties.ReadOnly = Status
        CheckBox74.Enabled = IIf(Status = True, False, True)
        CheckBox82.Enabled = IIf(Status = True, False, True)
        TextEdit117.Properties.ReadOnly = Status
        TextEdit118.Properties.ReadOnly = Status
        CheckBox83.Enabled = IIf(Status = True, False, True)
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
        DateEdit7.Properties.ReadOnly = Status
        DateEdit8.Properties.ReadOnly = Status
        DateEdit9.Properties.ReadOnly = Status
        DateEdit10.Properties.ReadOnly = Status
        DateEdit10.Properties.ReadOnly = Status
        ComboBoxEdit1.Properties.ReadOnly = Status
        ComboBoxEdit2.Properties.ReadOnly = Status
        ComboBoxEdit3.Properties.ReadOnly = Status
        ComboBoxEdit4.Properties.ReadOnly = Status
        ComboBoxEdit5.Properties.ReadOnly = Status
        ComboBoxEdit6.Properties.ReadOnly = Status
        ComboBoxEdit7.Properties.ReadOnly = Status
        ComboBoxEdit8.Properties.ReadOnly = Status
        ComboBoxEdit9.Properties.ReadOnly = Status
        ComboBoxEdit10.Properties.ReadOnly = Status
        ComboBoxEdit11.Properties.ReadOnly = Status
        ComboBoxEdit12.Properties.ReadOnly = Status
        ComboBoxEdit13.Properties.ReadOnly = Status
        ComboBoxEdit14.Properties.ReadOnly = Status
        ComboBoxEdit15.Properties.ReadOnly = Status
        ComboBoxEdit16.Properties.ReadOnly = Status
        ComboBoxEdit17.Properties.ReadOnly = Status
        ComboBoxEdit18.Properties.ReadOnly = Status
        ComboBoxEdit19.Properties.ReadOnly = Status
        ComboBoxEdit20.Properties.ReadOnly = Status
        ComboBoxEdit21.Properties.ReadOnly = Status
        ComboBoxEdit22.Properties.ReadOnly = Status
        ComboBoxEdit23.Properties.ReadOnly = Status
        ComboBoxEdit24.Properties.ReadOnly = Status
        TextEdit119.Properties.ReadOnly = Status
        TextEdit120.Properties.ReadOnly = Status
        TextEdit121.Properties.ReadOnly = Status
        TextEdit122.Properties.ReadOnly = Status
        TextEdit123.Properties.ReadOnly = Status
        TextEdit124.Properties.ReadOnly = Status
        TextEdit125.Properties.ReadOnly = Status
        TextEdit126.Properties.ReadOnly = Status
        DateEdit11.Properties.ReadOnly = Status
        DateEdit12.Properties.ReadOnly = Status
        DateEdit13.Properties.ReadOnly = Status
        DateEdit14.Properties.ReadOnly = Status

        TextEdit155.Properties.ReadOnly = Status
        TextEdit156.Properties.ReadOnly = Status
        TextEdit157.Properties.ReadOnly = Status
        TextEdit158.Properties.ReadOnly = Status
        TextEdit159.Properties.ReadOnly = Status
        TextEdit160.Properties.ReadOnly = Status
        TextEdit161.Properties.ReadOnly = Status
        TextEdit162.Properties.ReadOnly = Status
        TextEdit163.Properties.ReadOnly = Status
        TextEdit164.Properties.ReadOnly = Status
        TextEdit165.Properties.ReadOnly = Status
        TextEdit166.Properties.ReadOnly = Status
        CheckBox85.Enabled = IIf(Status = True, False, True)
        CheckBox84.Enabled = IIf(Status = True, False, True)
        CheckBox86.Enabled = IIf(Status = True, False, True)
        CheckBox87.Enabled = IIf(Status = True, False, True)
        CheckBox88.Enabled = IIf(Status = True, False, True)
        TextEdit167.Properties.ReadOnly = Status
        CheckBox89.Enabled = IIf(Status = True, False, True)
        CheckBox90.Enabled = IIf(Status = True, False, True)
        CheckBox91.Enabled = IIf(Status = True, False, True)
        CheckBox92.Enabled = IIf(Status = True, False, True)
        CheckBox93.Enabled = IIf(Status = True, False, True)
        CheckBox94.Enabled = IIf(Status = True, False, True)
        TextEdit168.Properties.ReadOnly = Status
        CheckBox95.Enabled = IIf(Status = True, False, True)
        CheckBox96.Enabled = IIf(Status = True, False, True)
        CheckBox97.Enabled = IIf(Status = True, False, True)
        CheckBox98.Enabled = IIf(Status = True, False, True)
        CheckBox390.Enabled = IIf(Status = True, False, True)
        CheckBox231.Enabled = IIf(Status = True, False, True)
        CheckBox99.Enabled = IIf(Status = True, False, True)
        CheckBox101.Enabled = IIf(Status = True, False, True)
        CheckBox102.Enabled = IIf(Status = True, False, True)
        CheckBox103.Enabled = IIf(Status = True, False, True)
        CheckBox100.Enabled = IIf(Status = True, False, True)
        TextEdit169.Properties.ReadOnly = Status
        CheckBox104.Enabled = IIf(Status = True, False, True)
        CheckBox105.Enabled = IIf(Status = True, False, True)
        TextEdit170.Properties.ReadOnly = Status
        CheckBox106.Enabled = IIf(Status = True, False, True)
        CheckBox107.Enabled = IIf(Status = True, False, True)
        CheckBox108.Enabled = IIf(Status = True, False, True)
        CheckBox109.Enabled = IIf(Status = True, False, True)
        TextEdit171.Properties.ReadOnly = Status
        CheckBox110.Enabled = IIf(Status = True, False, True)
        TextEdit172.Properties.ReadOnly = Status
        CheckBox111.Enabled = IIf(Status = True, False, True)
        CheckBox112.Enabled = IIf(Status = True, False, True)
        CheckBox117.Enabled = IIf(Status = True, False, True)
        CheckBox116.Enabled = IIf(Status = True, False, True)
        CheckBox113.Enabled = IIf(Status = True, False, True)
        CheckBox115.Enabled = IIf(Status = True, False, True)
        CheckBox118.Enabled = IIf(Status = True, False, True)
        CheckBox119.Enabled = IIf(Status = True, False, True)
        TextEdit173.Properties.ReadOnly = Status
        CheckBox114.Enabled = IIf(Status = True, False, True)
        CheckBox120.Enabled = IIf(Status = True, False, True)
        TextEdit174.Properties.ReadOnly = Status
        CheckBox121.Enabled = IIf(Status = True, False, True)
        TextEdit175.Properties.ReadOnly = Status
        CheckBox122.Enabled = IIf(Status = True, False, True)
        CheckBox123.Enabled = IIf(Status = True, False, True)
        CheckBox124.Enabled = IIf(Status = True, False, True)
        CheckBox125.Enabled = IIf(Status = True, False, True)
        CheckBox126.Enabled = IIf(Status = True, False, True)
        CheckBox127.Enabled = IIf(Status = True, False, True)
        CheckBox128.Enabled = IIf(Status = True, False, True)
        CheckBox129.Enabled = IIf(Status = True, False, True)
        CheckBox130.Enabled = IIf(Status = True, False, True)
        TextEdit176.Properties.ReadOnly = Status
        CheckBox131.Enabled = IIf(Status = True, False, True)
        CheckBox132.Enabled = IIf(Status = True, False, True)
        CheckBox133.Enabled = IIf(Status = True, False, True)
        CheckBox134.Enabled = IIf(Status = True, False, True)
        CheckBox136.Enabled = IIf(Status = True, False, True)
        CheckBox135.Enabled = IIf(Status = True, False, True)
        CheckBox137.Enabled = IIf(Status = True, False, True)
        CheckBox138.Enabled = IIf(Status = True, False, True)
        CheckBox140.Enabled = IIf(Status = True, False, True)
        CheckBox139.Enabled = IIf(Status = True, False, True)
        CheckBox142.Enabled = IIf(Status = True, False, True)
        CheckBox141.Enabled = IIf(Status = True, False, True)
        CheckBox144.Enabled = IIf(Status = True, False, True)
        CheckBox143.Enabled = IIf(Status = True, False, True)
        CheckBox146.Enabled = IIf(Status = True, False, True)
        CheckBox145.Enabled = IIf(Status = True, False, True)
        CheckBox147.Enabled = IIf(Status = True, False, True)
        CheckBox148.Enabled = IIf(Status = True, False, True)
        CheckBox149.Enabled = IIf(Status = True, False, True)
        CheckBox150.Enabled = IIf(Status = True, False, True)
        CheckBox151.Enabled = IIf(Status = True, False, True)
        CheckBox152.Enabled = IIf(Status = True, False, True)
        CheckBox155.Enabled = IIf(Status = True, False, True)
        CheckBox154.Enabled = IIf(Status = True, False, True)
        CheckBox157.Enabled = IIf(Status = True, False, True)
        CheckBox156.Enabled = IIf(Status = True, False, True)
        CheckBox158.Enabled = IIf(Status = True, False, True)
        TextEdit177.Properties.ReadOnly = Status
        CheckBox159.Enabled = IIf(Status = True, False, True)
        CheckBox160.Enabled = IIf(Status = True, False, True)
        TextEdit178.Properties.ReadOnly = Status
        CheckBox161.Enabled = IIf(Status = True, False, True)
        TextEdit179.Properties.ReadOnly = Status
        CheckBox153.Enabled = IIf(Status = True, False, True)
        TextEdit180.Properties.ReadOnly = Status
        CheckBox165.Enabled = IIf(Status = True, False, True)
        CheckBox164.Enabled = IIf(Status = True, False, True)
        CheckBox163.Enabled = IIf(Status = True, False, True)
        CheckBox167.Enabled = IIf(Status = True, False, True)
        CheckBox166.Enabled = IIf(Status = True, False, True)
        CheckBox169.Enabled = IIf(Status = True, False, True)
        CheckBox171.Enabled = IIf(Status = True, False, True)
        CheckBox170.Enabled = IIf(Status = True, False, True)
        CheckBox172.Enabled = IIf(Status = True, False, True)
        CheckBox173.Enabled = IIf(Status = True, False, True)
        CheckBox174.Enabled = IIf(Status = True, False, True)
        CheckBox175.Enabled = IIf(Status = True, False, True)
        CheckBox176.Enabled = IIf(Status = True, False, True)
        CheckBox177.Enabled = IIf(Status = True, False, True)
        CheckBox178.Enabled = IIf(Status = True, False, True)
        CheckBox179.Enabled = IIf(Status = True, False, True)
        CheckBox180.Enabled = IIf(Status = True, False, True)
        CheckBox181.Enabled = IIf(Status = True, False, True)
        CheckBox182.Enabled = IIf(Status = True, False, True)
        CheckBox187.Enabled = IIf(Status = True, False, True)
        CheckBox186.Enabled = IIf(Status = True, False, True)
        CheckBox185.Enabled = IIf(Status = True, False, True)
        CheckBox184.Enabled = IIf(Status = True, False, True)
        CheckBox183.Enabled = IIf(Status = True, False, True)
        TextEdit181.Properties.ReadOnly = Status
        CheckBox391.Enabled = IIf(Status = True, False, True)
        CheckBox168.Enabled = IIf(Status = True, False, True)
        TextEdit5.Properties.ReadOnly = Status
        CheckBox189.Enabled = IIf(Status = True, False, True)
        CheckBox190.Enabled = IIf(Status = True, False, True)
        CheckBox188.Enabled = IIf(Status = True, False, True)
        CheckBox191.Enabled = IIf(Status = True, False, True)
        CheckBox192.Enabled = IIf(Status = True, False, True)
        CheckBox195.Enabled = IIf(Status = True, False, True)
        CheckBox194.Enabled = IIf(Status = True, False, True)
        CheckBox193.Enabled = IIf(Status = True, False, True)
        CheckBox198.Enabled = IIf(Status = True, False, True)
        CheckBox197.Enabled = IIf(Status = True, False, True)
        TextEdit182.Properties.ReadOnly = Status
        CheckBox196.Enabled = IIf(Status = True, False, True)
        CheckBox199.Enabled = IIf(Status = True, False, True)
        CheckBox200.Enabled = IIf(Status = True, False, True)
        CheckBox201.Enabled = IIf(Status = True, False, True)
        CheckBox202.Enabled = IIf(Status = True, False, True)
        CheckBox203.Enabled = IIf(Status = True, False, True)
        CheckBox204.Enabled = IIf(Status = True, False, True)
        TextEdit183.Properties.ReadOnly = Status
        CheckBox205.Enabled = IIf(Status = True, False, True)
        CheckBox206.Enabled = IIf(Status = True, False, True)
        CheckBox207.Enabled = IIf(Status = True, False, True)
        CheckBox208.Enabled = IIf(Status = True, False, True)
        CheckBox209.Enabled = IIf(Status = True, False, True)
        TextEdit184.Properties.ReadOnly = Status
        CheckBox210.Enabled = IIf(Status = True, False, True)
        TextEdit185.Properties.ReadOnly = Status
        CheckBox211.Enabled = IIf(Status = True, False, True)
        TextEdit186.Properties.ReadOnly = Status
        CheckBox212.Enabled = IIf(Status = True, False, True)
        CheckBox213.Enabled = IIf(Status = True, False, True)
        TextEdit187.Properties.ReadOnly = Status
        CheckBox214.Enabled = IIf(Status = True, False, True)
        CheckBox215.Enabled = IIf(Status = True, False, True)
        CheckBox216.Enabled = IIf(Status = True, False, True)
        CheckBox217.Enabled = IIf(Status = True, False, True)
        CheckBox218.Enabled = IIf(Status = True, False, True)
        CheckBox219.Enabled = IIf(Status = True, False, True)
        TextEdit212.Properties.ReadOnly = Status
        CheckBox162.Enabled = IIf(Status = True, False, True)
        TextEdit213.Properties.ReadOnly = Status
        CheckBox220.Enabled = IIf(Status = True, False, True)
        TextEdit188.Properties.ReadOnly = Status
        CheckBox221.Enabled = IIf(Status = True, False, True)
        TextEdit189.Properties.ReadOnly = Status
        CheckBox222.Enabled = IIf(Status = True, False, True)
        CheckBox223.Enabled = IIf(Status = True, False, True)
        CheckBox224.Enabled = IIf(Status = True, False, True)
        CheckBox225.Enabled = IIf(Status = True, False, True)
        CheckBox226.Enabled = IIf(Status = True, False, True)
        CheckBox227.Enabled = IIf(Status = True, False, True)
        CheckBox228.Enabled = IIf(Status = True, False, True)
        TextEdit190.Properties.ReadOnly = Status
        CheckBox229.Enabled = IIf(Status = True, False, True)
        CheckBox230.Enabled = IIf(Status = True, False, True)
        CheckBox232.Enabled = IIf(Status = True, False, True)
        CheckBox233.Enabled = IIf(Status = True, False, True)
        CheckBox392.Enabled = IIf(Status = True, False, True)
        CheckBox235.Enabled = IIf(Status = True, False, True)
        CheckBox236.Enabled = IIf(Status = True, False, True)
        TextEdit191.Properties.ReadOnly = Status
        CheckBox237.Enabled = IIf(Status = True, False, True)
        CheckBox238.Enabled = IIf(Status = True, False, True)
        CheckBox239.Enabled = IIf(Status = True, False, True)
        CheckBox240.Enabled = IIf(Status = True, False, True)
        CheckBox241.Enabled = IIf(Status = True, False, True)
        CheckBox242.Enabled = IIf(Status = True, False, True)
        CheckBox243.Enabled = IIf(Status = True, False, True)
        CheckBox244.Enabled = IIf(Status = True, False, True)
        CheckBox245.Enabled = IIf(Status = True, False, True)
        TextEdit192.Properties.ReadOnly = Status
        TextEdit193.Properties.ReadOnly = Status
        CheckBox247.Enabled = IIf(Status = True, False, True)
        CheckBox248.Enabled = IIf(Status = True, False, True)
        CheckBox246.Enabled = IIf(Status = True, False, True)
        CheckBox249.Enabled = IIf(Status = True, False, True)
        CheckBox250.Enabled = IIf(Status = True, False, True)
        CheckBox251.Enabled = IIf(Status = True, False, True)
        CheckBox252.Enabled = IIf(Status = True, False, True)
        TextEdit194.Properties.ReadOnly = Status
        CheckBox253.Enabled = IIf(Status = True, False, True)
        CheckBox254.Enabled = IIf(Status = True, False, True)
        CheckBox255.Enabled = IIf(Status = True, False, True)
        CheckBox256.Enabled = IIf(Status = True, False, True)
        CheckBox257.Enabled = IIf(Status = True, False, True)
        CheckBox258.Enabled = IIf(Status = True, False, True)
        CheckBox259.Enabled = IIf(Status = True, False, True)
        CheckBox260.Enabled = IIf(Status = True, False, True)
        TextEdit195.Properties.ReadOnly = Status
        CheckBox261.Enabled = IIf(Status = True, False, True)
        CheckBox262.Enabled = IIf(Status = True, False, True)
        CheckBox263.Enabled = IIf(Status = True, False, True)
        TextEdit196.Properties.ReadOnly = Status
        CheckBox264.Enabled = IIf(Status = True, False, True)
        CheckBox265.Enabled = IIf(Status = True, False, True)
        CheckBox266.Enabled = IIf(Status = True, False, True)
        CheckBox267.Enabled = IIf(Status = True, False, True)
        CheckBox268.Enabled = IIf(Status = True, False, True)
        CheckBox269.Enabled = IIf(Status = True, False, True)
        CheckBox270.Enabled = IIf(Status = True, False, True)
        CheckBox271.Enabled = IIf(Status = True, False, True)
        CheckBox272.Enabled = IIf(Status = True, False, True)
        CheckBox273.Enabled = IIf(Status = True, False, True)
        CheckBox274.Enabled = IIf(Status = True, False, True)
        CheckBox275.Enabled = IIf(Status = True, False, True)
        CheckBox276.Enabled = IIf(Status = True, False, True)
        CheckBox277.Enabled = IIf(Status = True, False, True)
        CheckBox278.Enabled = IIf(Status = True, False, True)
        CheckBox279.Enabled = IIf(Status = True, False, True)
        CheckBox280.Enabled = IIf(Status = True, False, True)
        CheckBox281.Enabled = IIf(Status = True, False, True)
        CheckBox282.Enabled = IIf(Status = True, False, True)
        TextEdit197.Properties.ReadOnly = Status
        CheckBox283.Enabled = IIf(Status = True, False, True)
        TextEdit198.Properties.ReadOnly = Status
        CheckBox284.Enabled = IIf(Status = True, False, True)
        CheckBox285.Enabled = IIf(Status = True, False, True)
        CheckBox286.Enabled = IIf(Status = True, False, True)
        CheckBox287.Enabled = IIf(Status = True, False, True)
        CheckBox288.Enabled = IIf(Status = True, False, True)
        CheckBox289.Enabled = IIf(Status = True, False, True)
        CheckBox290.Enabled = IIf(Status = True, False, True)
        CheckBox291.Enabled = IIf(Status = True, False, True)
        CheckBox292.Enabled = IIf(Status = True, False, True)
        CheckBox293.Enabled = IIf(Status = True, False, True)
        CheckBox294.Enabled = IIf(Status = True, False, True)
        CheckBox295.Enabled = IIf(Status = True, False, True)
        CheckBox296.Enabled = IIf(Status = True, False, True)
        CheckBox297.Enabled = IIf(Status = True, False, True)
        CheckBox298.Enabled = IIf(Status = True, False, True)
        CheckBox299.Enabled = IIf(Status = True, False, True)
        CheckBox300.Enabled = IIf(Status = True, False, True)
        CheckBox301.Enabled = IIf(Status = True, False, True)
        CheckBox302.Enabled = IIf(Status = True, False, True)
        CheckBox303.Enabled = IIf(Status = True, False, True)
        CheckBox304.Enabled = IIf(Status = True, False, True)
        CheckBox305.Enabled = IIf(Status = True, False, True)
        CheckBox306.Enabled = IIf(Status = True, False, True)
        CheckBox307.Enabled = IIf(Status = True, False, True)
        CheckBox308.Enabled = IIf(Status = True, False, True)
        CheckBox309.Enabled = IIf(Status = True, False, True)
        CheckBox310.Enabled = IIf(Status = True, False, True)
        CheckBox312.Enabled = IIf(Status = True, False, True)
        CheckBox313.Enabled = IIf(Status = True, False, True)
        CheckBox314.Enabled = IIf(Status = True, False, True)
        CheckBox315.Enabled = IIf(Status = True, False, True)
        CheckBox316.Enabled = IIf(Status = True, False, True)
        TextEdit199.Properties.ReadOnly = Status
        TextEdit200.Properties.ReadOnly = Status
        CheckBox317.Enabled = IIf(Status = True, False, True)
        CheckBox318.Enabled = IIf(Status = True, False, True)
        CheckBox319.Enabled = IIf(Status = True, False, True)
        CheckBox320.Enabled = IIf(Status = True, False, True)
        CheckBox321.Enabled = IIf(Status = True, False, True)
        CheckBox322.Enabled = IIf(Status = True, False, True)
        CheckBox323.Enabled = IIf(Status = True, False, True)
        CheckBox324.Enabled = IIf(Status = True, False, True)
        CheckBox325.Enabled = IIf(Status = True, False, True)
        CheckBox326.Enabled = IIf(Status = True, False, True)
        CheckBox327.Enabled = IIf(Status = True, False, True)
        CheckBox328.Enabled = IIf(Status = True, False, True)
        CheckBox329.Enabled = IIf(Status = True, False, True)
        CheckBox330.Enabled = IIf(Status = True, False, True)
        CheckBox331.Enabled = IIf(Status = True, False, True)
        TextEdit201.Properties.ReadOnly = Status
        CheckBox333.Enabled = IIf(Status = True, False, True)
        CheckBox332.Enabled = IIf(Status = True, False, True)
        CheckBox335.Enabled = IIf(Status = True, False, True)
        CheckBox334.Enabled = IIf(Status = True, False, True)
        CheckBox336.Enabled = IIf(Status = True, False, True)
        TextEdit202.Properties.ReadOnly = Status
        CheckBox337.Enabled = IIf(Status = True, False, True)
        CheckBox338.Enabled = IIf(Status = True, False, True)
        CheckBox339.Enabled = IIf(Status = True, False, True)
        CheckBox340.Enabled = IIf(Status = True, False, True)
        TextEdit203.Properties.ReadOnly = Status
        CheckBox341.Enabled = IIf(Status = True, False, True)
        CheckBox342.Enabled = IIf(Status = True, False, True)
        CheckBox343.Enabled = IIf(Status = True, False, True)
        CheckBox344.Enabled = IIf(Status = True, False, True)
        CheckBox351.Enabled = IIf(Status = True, False, True)
        CheckBox350.Enabled = IIf(Status = True, False, True)
        CheckBox349.Enabled = IIf(Status = True, False, True)
        CheckBox348.Enabled = IIf(Status = True, False, True)
        CheckBox356.Enabled = IIf(Status = True, False, True)
        CheckBox355.Enabled = IIf(Status = True, False, True)
        CheckBox354.Enabled = IIf(Status = True, False, True)
        CheckBox345.Enabled = IIf(Status = True, False, True)
        CheckBox346.Enabled = IIf(Status = True, False, True)
        CheckBox352.Enabled = IIf(Status = True, False, True)
        CheckBox347.Enabled = IIf(Status = True, False, True)
        CheckBox353.Enabled = IIf(Status = True, False, True)
        CheckBox357.Enabled = IIf(Status = True, False, True)
        CheckBox358.Enabled = IIf(Status = True, False, True)
        ComboBoxEdit25.Properties.ReadOnly = Status
        ComboBoxEdit26.Properties.ReadOnly = Status
        ComboBoxEdit27.Properties.ReadOnly = Status
        ComboBoxEdit28.Properties.ReadOnly = Status
        ComboBoxEdit29.Properties.ReadOnly = Status
        TextEdit204.Properties.ReadOnly = Status
        ComboBoxEdit30.Properties.ReadOnly = Status
        ComboBoxEdit31.Properties.ReadOnly = Status
        ComboBoxEdit32.Properties.ReadOnly = Status
        ComboBoxEdit33.Properties.ReadOnly = Status
        ComboBoxEdit34.Properties.ReadOnly = Status
        ComboBoxEdit35.Properties.ReadOnly = Status
        ComboBoxEdit36.Properties.ReadOnly = Status
        ComboBoxEdit37.Properties.ReadOnly = Status
        ComboBoxEdit38.Properties.ReadOnly = Status
        ComboBoxEdit39.Properties.ReadOnly = Status
        CheckBox360.Enabled = IIf(Status = True, False, True)
        CheckBox359.Enabled = IIf(Status = True, False, True)
        CheckBox362.Enabled = IIf(Status = True, False, True)
        CheckBox361.Enabled = IIf(Status = True, False, True)
        CheckBox363.Enabled = IIf(Status = True, False, True)
        CheckBox364.Enabled = IIf(Status = True, False, True)
        CheckBox365.Enabled = IIf(Status = True, False, True)
        CheckBox366.Enabled = IIf(Status = True, False, True)
        CheckBox367.Enabled = IIf(Status = True, False, True)
        CheckBox368.Enabled = IIf(Status = True, False, True)
        CheckBox369.Enabled = IIf(Status = True, False, True)
        CheckBox370.Enabled = IIf(Status = True, False, True)
        CheckBox371.Enabled = IIf(Status = True, False, True)
        CheckBox372.Enabled = IIf(Status = True, False, True)
        CheckBox373.Enabled = IIf(Status = True, False, True)
        CheckBox374.Enabled = IIf(Status = True, False, True)
        CheckBox375.Enabled = IIf(Status = True, False, True)
        CheckBox376.Enabled = IIf(Status = True, False, True)
        CheckBox377.Enabled = IIf(Status = True, False, True)
        CheckBox378.Enabled = IIf(Status = True, False, True)
        CheckBox379.Enabled = IIf(Status = True, False, True)
        CheckBox380.Enabled = IIf(Status = True, False, True)
        CheckBox381.Enabled = IIf(Status = True, False, True)
        CheckBox382.Enabled = IIf(Status = True, False, True)
        CheckBox383.Enabled = IIf(Status = True, False, True)
        CheckBox384.Enabled = IIf(Status = True, False, True)
        CheckBox385.Enabled = IIf(Status = True, False, True)
        CheckBox386.Enabled = IIf(Status = True, False, True)
        CheckBox387.Enabled = IIf(Status = True, False, True)
        CheckBox388.Enabled = IIf(Status = True, False, True)
        CheckBox389.Enabled = IIf(Status = True, False, True)
        CheckBox393.Enabled = IIf(Status = True, False, True)
        CheckBox234.Enabled = IIf(Status = True, False, True)
        CheckBox395.Enabled = IIf(Status = True, False, True)
        CheckBox394.Enabled = IIf(Status = True, False, True)
        CheckBox397.Enabled = IIf(Status = True, False, True)
        CheckBox396.Enabled = IIf(Status = True, False, True)
        CheckBox405.Enabled = IIf(Status = True, False, True)
        CheckBox409.Enabled = IIf(Status = True, False, True)
        CheckBox413.Enabled = IIf(Status = True, False, True)
        CheckBox417.Enabled = IIf(Status = True, False, True)
        CheckBox404.Enabled = IIf(Status = True, False, True)
        CheckBox408.Enabled = IIf(Status = True, False, True)
        CheckBox412.Enabled = IIf(Status = True, False, True)
        CheckBox416.Enabled = IIf(Status = True, False, True)
        CheckBox403.Enabled = IIf(Status = True, False, True)
        CheckBox407.Enabled = IIf(Status = True, False, True)
        CheckBox411.Enabled = IIf(Status = True, False, True)
        CheckBox414.Enabled = IIf(Status = True, False, True)
        CheckBox400.Enabled = IIf(Status = True, False, True)
        CheckBox406.Enabled = IIf(Status = True, False, True)
        CheckBox410.Enabled = IIf(Status = True, False, True)
        CheckBox415.Enabled = IIf(Status = True, False, True)
        CheckBox418.Enabled = IIf(Status = True, False, True)
        CheckBox419.Enabled = IIf(Status = True, False, True)
        CheckBox420.Enabled = IIf(Status = True, False, True)
        CheckBox422.Enabled = IIf(Status = True, False, True)
        CheckBox423.Enabled = IIf(Status = True, False, True)
        CheckBox424.Enabled = IIf(Status = True, False, True)
        CheckBox425.Enabled = IIf(Status = True, False, True)
        CheckBox426.Enabled = IIf(Status = True, False, True)
        CheckBox427.Enabled = IIf(Status = True, False, True)
        CheckBox428.Enabled = IIf(Status = True, False, True)
        CheckBox430.Enabled = IIf(Status = True, False, True)
        CheckBox429.Enabled = IIf(Status = True, False, True)
        CheckBox431.Enabled = IIf(Status = True, False, True)
        CheckBox432.Enabled = IIf(Status = True, False, True)
        CheckBox439.Enabled = IIf(Status = True, False, True)
        CheckBox433.Enabled = IIf(Status = True, False, True)
        CheckBox434.Enabled = IIf(Status = True, False, True)
        CheckBox436.Enabled = IIf(Status = True, False, True)
        CheckBox435.Enabled = IIf(Status = True, False, True)
        CheckBox438.Enabled = IIf(Status = True, False, True)
        TextEdit205.Properties.ReadOnly = Status
        TextEdit206.Properties.ReadOnly = Status
        TextEdit207.Properties.ReadOnly = Status
        TextEdit208.Properties.ReadOnly = Status
        TextEdit209.Properties.ReadOnly = Status
        TextEdit210.Properties.ReadOnly = Status
        TextEdit211.Properties.ReadOnly = Status
        TextEdit214.Properties.ReadOnly = Status
        CheckEdit1.Properties.ReadOnly = Status
        CheckEdit2.Properties.ReadOnly = Status
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


    End Sub
    Private Sub fn_EmptyMe()


        TextEdit1.ResetText()
        TextEdit2.ResetText()
        CheckBox10.Checked = False
        CheckBox15.Checked = False
        CheckBox11.Checked = False
        CheckBox13.Checked = False
        CheckBox14.Checked = False
        CheckBox1.Checked = False
        CheckBox2.Checked = False
        CheckBox3.Checked = False
        MemoEdit2.ResetText()
        MemoEdit3.ResetText()
        MemoEdit4.ResetText()
        MemoEdit5.ResetText()
        MemoEdit6.ResetText()
        MemoEdit7.ResetText()
        MemoEdit8.ResetText()
        TextEdit3.ResetText()
        TextEdit4.ResetText()
        TextEdit6.ResetText()
        TextEdit7.ResetText()
        TextEdit8.ResetText()
        TextEdit9.ResetText()
        TextEdit10.ResetText()
        TextEdit11.ResetText()
        TextEdit12.ResetText()
        TextEdit13.ResetText()
        TextEdit14.ResetText()
        TextEdit15.ResetText()
        CheckBox4.Checked = False
        CheckBox5.Checked = False
        TextEdit16.ResetText()
        TextEdit17.ResetText()
        CheckBox7.Checked = False
        CheckBox6.Checked = False
        TextEdit19.ResetText()
        TextEdit18.ResetText()
        CheckBox16.Checked = False
        CheckBox8.Checked = False
        TextEdit21.ResetText()
        TextEdit20.ResetText()
        CheckBox18.Checked = False
        CheckBox17.Checked = False
        CheckBox20.Checked = False
        CheckBox19.Checked = False
        CheckBox22.Checked = False
        CheckBox21.Checked = False
        CheckBox24.Checked = False
        CheckBox23.Checked = False
        TextEdit22.ResetText()
        CheckBox25.Checked = False
        CheckBox26.Checked = False
        CheckBox28.Checked = False
        CheckBox27.Checked = False
        CheckBox29.Checked = False
        CheckBox30.Checked = False
        CheckBox31.Checked = False
        CheckBox34.Checked = False
        CheckBox33.Checked = False
        CheckBox32.Checked = False
        CheckBox77.Checked = False
        CheckBox76.Checked = False
        CheckBox75.Checked = False
        CheckBox81.Checked = False
        CheckBox80.Checked = False
        CheckBox73.Checked = False
        CheckBox36.Checked = False
        CheckBox35.Checked = False
        CheckBox79.Checked = False
        CheckBox78.Checked = False
        TextEdit23.ResetText()
        TextEdit25.ResetText()
        TextEdit66.ResetText()
        TextEdit24.ResetText()
        TextEdit26.ResetText()
        TextEdit65.ResetText()
        TextEdit64.ResetText()
        TextEdit27.ResetText()
        TextEdit28.ResetText()
        TextEdit70.ResetText()
        TextEdit73.ResetText()
        TextEdit67.ResetText()
        TextEdit82.ResetText()
        TextEdit83.ResetText()
        TextEdit96.ResetText()
        TextEdit95.ResetText()
        TextEdit94.ResetText()
        TextEdit97.ResetText()
        TextEdit93.ResetText()
        TextEdit71.ResetText()
        TextEdit72.ResetText()
        TextEdit68.ResetText()
        TextEdit84.ResetText()
        TextEdit85.ResetText()
        TextEdit92.ResetText()
        TextEdit98.ResetText()
        TextEdit99.ResetText()
        TextEdit100.ResetText()
        TextEdit101.ResetText()
        TextEdit75.ResetText()
        TextEdit74.ResetText()
        TextEdit69.ResetText()
        TextEdit86.ResetText()
        TextEdit87.ResetText()
        TextEdit102.ResetText()
        TextEdit103.ResetText()
        TextEdit104.ResetText()
        TextEdit105.ResetText()
        TextEdit106.ResetText()
        TextEdit76.ResetText()
        TextEdit77.ResetText()
        TextEdit78.ResetText()
        TextEdit88.ResetText()
        TextEdit89.ResetText()
        TextEdit107.ResetText()
        TextEdit108.ResetText()
        TextEdit109.ResetText()
        TextEdit110.ResetText()
        TextEdit111.ResetText()
        TextEdit81.ResetText()
        TextEdit80.ResetText()
        TextEdit79.ResetText()
        TextEdit90.ResetText()
        TextEdit91.ResetText()
        TextEdit115.ResetText()
        TextEdit116.ResetText()
        TextEdit114.ResetText()
        TextEdit112.ResetText()
        TextEdit113.ResetText()
        CheckBox74.Checked = False
        CheckBox82.Checked = False
        TextEdit117.ResetText()
        TextEdit118.ResetText()
        CheckBox83.Checked = False
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

        TextEdit119.ResetText()
        TextEdit120.ResetText()
        TextEdit121.ResetText()
        TextEdit122.ResetText()
        TextEdit123.ResetText()
        TextEdit124.ResetText()
        TextEdit125.ResetText()
        TextEdit126.ResetText()

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
        CheckBox85.Checked = False
        CheckBox84.Checked = False
        CheckBox86.Checked = False
        CheckBox87.Checked = False
        CheckBox88.Checked = False
        TextEdit167.ResetText()
        CheckBox89.Checked = False
        CheckBox90.Checked = False
        CheckBox91.Checked = False
        CheckBox92.Checked = False
        CheckBox93.Checked = False
        CheckBox94.Checked = False
        TextEdit168.ResetText()
        CheckBox95.Checked = False
        CheckBox96.Checked = False
        CheckBox97.Checked = False
        CheckBox98.Checked = False
        CheckBox390.Checked = False
        CheckBox231.Checked = False
        CheckBox99.Checked = False
        CheckBox101.Checked = False
        CheckBox102.Checked = False
        CheckBox103.Checked = False
        CheckBox100.Checked = False
        TextEdit169.ResetText()
        CheckBox104.Checked = False
        CheckBox105.Checked = False
        TextEdit170.ResetText()
        CheckBox106.Checked = False
        CheckBox107.Checked = False
        CheckBox108.Checked = False
        CheckBox109.Checked = False
        TextEdit171.ResetText()
        CheckBox110.Checked = False
        TextEdit172.ResetText()
        CheckBox111.Checked = False
        CheckBox112.Checked = False
        CheckBox117.Checked = False
        CheckBox116.Checked = False
        CheckBox113.Checked = False
        CheckBox115.Checked = False
        CheckBox118.Checked = False
        CheckBox119.Checked = False
        TextEdit173.ResetText()
        CheckBox114.Checked = False
        CheckBox120.Checked = False
        TextEdit174.ResetText()
        CheckBox121.Checked = False
        TextEdit175.ResetText()
        CheckBox122.Checked = False
        CheckBox123.Checked = False
        CheckBox124.Checked = False
        CheckBox125.Checked = False
        CheckBox126.Checked = False
        CheckBox127.Checked = False
        CheckBox128.Checked = False
        CheckBox129.Checked = False
        CheckBox130.Checked = False
        TextEdit176.ResetText()
        CheckBox131.Checked = False
        CheckBox132.Checked = False
        CheckBox133.Checked = False
        CheckBox134.Checked = False
        CheckBox136.Checked = False
        CheckBox135.Checked = False
        CheckBox137.Checked = False
        CheckBox138.Checked = False
        CheckBox140.Checked = False
        CheckBox139.Checked = False
        CheckBox142.Checked = False
        CheckBox141.Checked = False
        CheckBox144.Checked = False
        CheckBox143.Checked = False
        CheckBox146.Checked = False
        CheckBox145.Checked = False
        CheckBox147.Checked = False
        CheckBox148.Checked = False
        CheckBox149.Checked = False
        CheckBox150.Checked = False
        CheckBox151.Checked = False
        CheckBox152.Checked = False
        CheckBox155.Checked = False
        CheckBox154.Checked = False
        CheckBox157.Checked = False
        CheckBox156.Checked = False
        CheckBox158.Checked = False
        TextEdit177.ResetText()
        CheckBox159.Checked = False
        CheckBox160.Checked = False
        TextEdit178.ResetText()
        CheckBox161.Checked = False
        TextEdit179.ResetText()
        CheckBox153.Checked = False
        TextEdit180.ResetText()
        CheckBox165.Checked = False
        CheckBox164.Checked = False
        CheckBox163.Checked = False
        CheckBox167.Checked = False
        CheckBox166.Checked = False
        CheckBox169.Checked = False
        CheckBox171.Checked = False
        CheckBox170.Checked = False
        CheckBox172.Checked = False
        CheckBox173.Checked = False
        CheckBox174.Checked = False
        CheckBox175.Checked = False
        CheckBox176.Checked = False
        CheckBox177.Checked = False
        CheckBox178.Checked = False
        CheckBox179.Checked = False
        CheckBox180.Checked = False
        CheckBox181.Checked = False
        CheckBox182.Checked = False
        CheckBox187.Checked = False
        CheckBox186.Checked = False
        CheckBox185.Checked = False
        CheckBox184.Checked = False
        CheckBox183.Checked = False
        TextEdit181.ResetText()
        CheckBox391.Checked = False
        CheckBox168.Checked = False
        TextEdit5.ResetText()
        CheckBox189.Checked = False
        CheckBox190.Checked = False
        CheckBox188.Checked = False
        CheckBox191.Checked = False
        CheckBox192.Checked = False
        CheckBox195.Checked = False
        CheckBox194.Checked = False
        CheckBox193.Checked = False
        CheckBox198.Checked = False
        CheckBox197.Checked = False
        TextEdit182.ResetText()
        CheckBox196.Checked = False
        CheckBox199.Checked = False
        CheckBox200.Checked = False
        CheckBox201.Checked = False
        CheckBox202.Checked = False
        CheckBox203.Checked = False
        CheckBox204.Checked = False
        TextEdit183.ResetText()
        CheckBox205.Checked = False
        CheckBox206.Checked = False
        CheckBox207.Checked = False
        CheckBox208.Checked = False
        CheckBox209.Checked = False
        TextEdit184.ResetText()
        CheckBox210.Checked = False
        TextEdit185.ResetText()
        CheckBox211.Checked = False
        TextEdit186.ResetText()
        CheckBox212.Checked = False
        CheckBox213.Checked = False
        TextEdit187.ResetText()
        CheckBox214.Checked = False
        CheckBox215.Checked = False
        CheckBox216.Checked = False
        CheckBox217.Checked = False
        CheckBox218.Checked = False
        CheckBox219.Checked = False
        TextEdit212.ResetText()
        CheckBox162.Checked = False
        TextEdit213.ResetText()
        CheckBox220.Checked = False
        TextEdit188.ResetText()
        CheckBox221.Checked = False
        TextEdit189.ResetText()
        CheckBox222.Checked = False
        CheckBox223.Checked = False
        CheckBox224.Checked = False
        CheckBox225.Checked = False
        CheckBox226.Checked = False
        CheckBox227.Checked = False
        CheckBox228.Checked = False
        TextEdit190.ResetText()
        CheckBox229.Checked = False
        CheckBox230.Checked = False
        CheckBox232.Checked = False
        CheckBox233.Checked = False
        CheckBox392.Checked = False
        CheckBox235.Checked = False
        CheckBox236.Checked = False
        TextEdit191.ResetText()
        CheckBox237.Checked = False
        CheckBox238.Checked = False
        CheckBox239.Checked = False
        CheckBox240.Checked = False
        CheckBox241.Checked = False
        CheckBox242.Checked = False
        CheckBox243.Checked = False
        CheckBox244.Checked = False
        CheckBox245.Checked = False
        TextEdit192.ResetText()
        TextEdit193.ResetText()
        CheckBox247.Checked = False
        CheckBox248.Checked = False
        CheckBox246.Checked = False
        CheckBox249.Checked = False
        CheckBox250.Checked = False
        CheckBox251.Checked = False
        CheckBox252.Checked = False
        TextEdit194.ResetText()
        CheckBox253.Checked = False
        CheckBox254.Checked = False
        CheckBox255.Checked = False
        CheckBox256.Checked = False
        CheckBox257.Checked = False
        CheckBox258.Checked = False
        CheckBox259.Checked = False
        CheckBox260.Checked = False
        TextEdit195.ResetText()
        CheckBox261.Checked = False
        CheckBox262.Checked = False
        CheckBox263.Checked = False
        TextEdit196.ResetText()
        CheckBox264.Checked = False
        CheckBox265.Checked = False
        CheckBox266.Checked = False
        CheckBox267.Checked = False
        CheckBox268.Checked = False
        CheckBox269.Checked = False
        CheckBox270.Checked = False
        CheckBox271.Checked = False
        CheckBox272.Checked = False
        CheckBox273.Checked = False
        CheckBox274.Checked = False
        CheckBox275.Checked = False
        CheckBox276.Checked = False
        CheckBox277.Checked = False
        CheckBox278.Checked = False
        CheckBox279.Checked = False
        CheckBox280.Checked = False
        CheckBox281.Checked = False
        CheckBox282.Checked = False
        TextEdit197.ResetText()
        CheckBox283.Checked = False
        TextEdit198.ResetText()
        CheckBox284.Checked = False
        CheckBox285.Checked = False
        CheckBox286.Checked = False
        CheckBox287.Checked = False
        CheckBox288.Checked = False
        CheckBox289.Checked = False
        CheckBox290.Checked = False
        CheckBox291.Checked = False
        CheckBox292.Checked = False
        CheckBox293.Checked = False
        CheckBox294.Checked = False
        CheckBox295.Checked = False
        CheckBox296.Checked = False
        CheckBox297.Checked = False
        CheckBox298.Checked = False
        CheckBox299.Checked = False
        CheckBox300.Checked = False
        CheckBox301.Checked = False
        CheckBox302.Checked = False
        CheckBox303.Checked = False
        CheckBox304.Checked = False
        CheckBox305.Checked = False
        CheckBox306.Checked = False
        CheckBox307.Checked = False
        CheckBox308.Checked = False
        CheckBox309.Checked = False
        CheckBox310.Checked = False
        CheckBox312.Checked = False
        CheckBox313.Checked = False
        CheckBox314.Checked = False
        CheckBox315.Checked = False
        CheckBox316.Checked = False
        TextEdit199.ResetText()
        TextEdit200.ResetText()
        CheckBox317.Checked = False
        CheckBox318.Checked = False
        CheckBox319.Checked = False
        CheckBox320.Checked = False
        CheckBox321.Checked = False
        CheckBox322.Checked = False
        CheckBox323.Checked = False
        CheckBox324.Checked = False
        CheckBox325.Checked = False
        CheckBox326.Checked = False
        CheckBox327.Checked = False
        CheckBox328.Checked = False
        CheckBox329.Checked = False
        CheckBox330.Checked = False
        CheckBox331.Checked = False
        TextEdit201.ResetText()
        CheckBox333.Checked = False
        CheckBox332.Checked = False
        CheckBox335.Checked = False
        CheckBox334.Checked = False
        CheckBox336.Checked = False
        TextEdit202.ResetText()
        CheckBox337.Checked = False
        CheckBox338.Checked = False
        CheckBox339.Checked = False
        CheckBox340.Checked = False
        TextEdit203.ResetText()
        CheckBox341.Checked = False
        CheckBox342.Checked = False
        CheckBox343.Checked = False
        CheckBox344.Checked = False
        CheckBox351.Checked = False
        CheckBox350.Checked = False
        CheckBox349.Checked = False
        CheckBox348.Checked = False
        CheckBox356.Checked = False
        CheckBox355.Checked = False
        CheckBox354.Checked = False
        CheckBox345.Checked = False
        CheckBox346.Checked = False
        CheckBox352.Checked = False
        CheckBox347.Checked = False
        CheckBox353.Checked = False
        CheckBox357.Checked = False
        CheckBox358.Checked = False
        TextEdit204.ResetText()
        CheckBox360.Checked = False
        CheckBox359.Checked = False
        CheckBox362.Checked = False
        CheckBox361.Checked = False
        CheckBox363.Checked = False
        CheckBox364.Checked = False
        CheckBox365.Checked = False
        CheckBox366.Checked = False
        CheckBox367.Checked = False
        CheckBox368.Checked = False
        CheckBox369.Checked = False
        CheckBox370.Checked = False
        CheckBox371.Checked = False
        CheckBox372.Checked = False
        CheckBox373.Checked = False
        CheckBox374.Checked = False
        CheckBox375.Checked = False
        CheckBox376.Checked = False
        CheckBox377.Checked = False
        CheckBox378.Checked = False
        CheckBox379.Checked = False
        CheckBox380.Checked = False
        CheckBox381.Checked = False
        CheckBox382.Checked = False
        CheckBox383.Checked = False
        CheckBox384.Checked = False
        CheckBox385.Checked = False
        CheckBox386.Checked = False
        CheckBox387.Checked = False
        CheckBox388.Checked = False
        CheckBox389.Checked = False
        CheckBox393.Checked = False
        CheckBox234.Checked = False
        CheckBox395.Checked = False
        CheckBox394.Checked = False
        CheckBox397.Checked = False
        CheckBox396.Checked = False
        CheckBox405.Checked = False
        CheckBox409.Checked = False
        CheckBox413.Checked = False
        CheckBox417.Checked = False
        CheckBox404.Checked = False
        CheckBox408.Checked = False
        CheckBox412.Checked = False
        CheckBox416.Checked = False
        CheckBox403.Checked = False
        CheckBox407.Checked = False
        CheckBox411.Checked = False
        CheckBox414.Checked = False
        CheckBox400.Checked = False
        CheckBox406.Checked = False
        CheckBox410.Checked = False
        CheckBox415.Checked = False
        CheckBox418.Checked = False
        CheckBox419.Checked = False
        CheckBox420.Checked = False
        CheckBox422.Checked = False
        CheckBox423.Checked = False
        CheckBox424.Checked = False
        CheckBox425.Checked = False
        CheckBox426.Checked = False
        CheckBox427.Checked = False
        CheckBox428.Checked = False
        CheckBox430.Checked = False
        CheckBox429.Checked = False
        CheckBox431.Checked = False
        CheckBox432.Checked = False
        CheckBox439.Checked = False
        CheckBox433.Checked = False
        CheckBox434.Checked = False
        CheckBox436.Checked = False
        CheckBox435.Checked = False
        CheckBox438.Checked = False
        CheckEdit1.Checked = False
        CheckEdit2.Checked = False
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

        TextEdit205.ResetText()
        TextEdit206.ResetText()
        TextEdit207.ResetText()
        TextEdit208.ResetText()
        TextEdit209.ResetText()
        TextEdit210.ResetText()
        TextEdit211.ResetText()
        TextEdit214.ResetText()

    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_ASKEP_PEDIATRIK_03.GetData(txtKDKUNJUNGAN.Text)

            With ds
                DateEdit1.EditValue = .TANGGALPENGKAJIAN
                DateEdit2.EditValue = .PUKULPENGKAJIAN
                CheckBox9.Checked = .OBSTETRI_PERAWAT_1
                CheckBox12.Checked = .OBSTETRI_PERAWAT_2
                TextEdit1.Text = .OBSTETRI_PERAWAT_3
                TextEdit2.Text = .OBSTETRI_PERAWAT_4
                CheckBox10.Checked = .OBSTETRI_PERAWAT_6
                CheckBox15.Checked = .OBSTETRI_PERAWAT_7
                CheckBox11.Checked = .OBSTETRI_PERAWAT_8
                CheckBox13.Checked = .OBSTETRI_PERAWAT_9
                CheckBox14.Checked = .OBSTETRI_PERAWAT_10
                CheckBox1.Checked = .OBSTETRI_PERAWAT_11
                CheckBox2.Checked = .OBSTETRI_PERAWAT_12
                CheckBox3.Checked = .OBSTETRI_PERAWAT_13
                MemoEdit2.Text = .OBSTETRI_PERAWAT_14
                MemoEdit3.Text = .OBSTETRI_PERAWAT_15
                MemoEdit4.Text = .OBSTETRI_PERAWAT_16
                MemoEdit5.Text = .OBSTETRI_PERAWAT_17
                MemoEdit6.Text = .OBSTETRI_PERAWAT_18
                MemoEdit7.Text = .OBSTETRI_PERAWAT_19
                MemoEdit8.Text = .OBSTETRI_PERAWAT_20
                TextEdit3.Text = .OBSTETRI_PERAWAT_21
                TextEdit4.Text = .OBSTETRI_PERAWAT_22
                TextEdit6.Text = .OBSTETRI_PERAWAT_23
                TextEdit7.Text = .OBSTETRI_PERAWAT_24
                TextEdit8.Text = .OBSTETRI_PERAWAT_25
                TextEdit9.Text = .OBSTETRI_PERAWAT_26
                TextEdit10.Text = .OBSTETRI_PERAWAT_27
                TextEdit11.Text = .OBSTETRI_PERAWAT_28
                TextEdit12.Text = .OBSTETRI_PERAWAT_29
                TextEdit13.Text = .OBSTETRI_PERAWAT_30
                TextEdit14.Text = .OBSTETRI_PERAWAT_31
                TextEdit15.Text = .OBSTETRI_PERAWAT_32
                CheckBox4.Checked = .OBSTETRI_PERAWAT_33
                CheckBox5.Checked = .OBSTETRI_PERAWAT_34
                TextEdit16.Text = .OBSTETRI_PERAWAT_35
                TextEdit17.Text = .OBSTETRI_PERAWAT_36
                CheckBox7.Checked = .OBSTETRI_PERAWAT_37
                CheckBox6.Checked = .OBSTETRI_PERAWAT_38
                TextEdit19.Text = .OBSTETRI_PERAWAT_39
                TextEdit18.Text = .OBSTETRI_PERAWAT_40
                CheckBox16.Checked = .OBSTETRI_PERAWAT_41
                CheckBox8.Checked = .OBSTETRI_PERAWAT_42
                TextEdit21.Text = .OBSTETRI_PERAWAT_43
                TextEdit20.Text = .OBSTETRI_PERAWAT_44
                CheckBox18.Checked = .OBSTETRI_PERAWAT_45
                CheckBox17.Checked = .OBSTETRI_PERAWAT_46
                CheckBox20.Checked = .OBSTETRI_PERAWAT_47
                CheckBox19.Checked = .OBSTETRI_PERAWAT_48
                CheckBox22.Checked = .OBSTETRI_PERAWAT_49
                CheckBox21.Checked = .OBSTETRI_PERAWAT_50
                CheckBox24.Checked = .OBSTETRI_PERAWAT_51
                CheckBox23.Checked = .OBSTETRI_PERAWAT_52
                TextEdit22.Text = .OBSTETRI_PERAWAT_53
                CheckBox25.Checked = .OBSTETRI_PERAWAT_54
                CheckBox26.Checked = .OBSTETRI_PERAWAT_55
                CheckBox28.Checked = .OBSTETRI_PERAWAT_56
                CheckBox27.Checked = .OBSTETRI_PERAWAT_57
                CheckBox29.Checked = .OBSTETRI_PERAWAT_58
                CheckBox30.Checked = .OBSTETRI_PERAWAT_59
                CheckBox31.Checked = .OBSTETRI_PERAWAT_60
                CheckBox34.Checked = .OBSTETRI_PERAWAT_61
                CheckBox33.Checked = .OBSTETRI_PERAWAT_62
                CheckBox32.Checked = .OBSTETRI_PERAWAT_63
                CheckBox77.Checked = .OBSTETRI_PERAWAT_64
                CheckBox76.Checked = .OBSTETRI_PERAWAT_65
                CheckBox75.Checked = .OBSTETRI_PERAWAT_66
                CheckBox81.Checked = .OBSTETRI_PERAWAT_67
                CheckBox80.Checked = .OBSTETRI_PERAWAT_68
                CheckBox73.Checked = .OBSTETRI_PERAWAT_69
                CheckBox36.Checked = .OBSTETRI_PERAWAT_70
                CheckBox35.Checked = .OBSTETRI_PERAWAT_71
                CheckBox79.Checked = .OBSTETRI_PERAWAT_72
                CheckBox78.Checked = .OBSTETRI_PERAWAT_73
                TextEdit23.Text = .OBSTETRI_PERAWAT_74
                TextEdit25.Text = .OBSTETRI_PERAWAT_75
                TextEdit66.Text = .OBSTETRI_PERAWAT_76
                TextEdit24.Text = .OBSTETRI_PERAWAT_77
                TextEdit26.Text = .OBSTETRI_PERAWAT_78
                TextEdit65.Text = .OBSTETRI_PERAWAT_79
                TextEdit64.Text = .OBSTETRI_PERAWAT_80
                TextEdit27.Text = .OBSTETRI_PERAWAT_81
                TextEdit28.Text = .OBSTETRI_PERAWAT_82
                TextEdit70.Text = .OBSTETRI_PERAWAT_83
                TextEdit73.Text = .OBSTETRI_PERAWAT_84
                TextEdit67.Text = .OBSTETRI_PERAWAT_85
                TextEdit82.Text = .OBSTETRI_PERAWAT_86
                TextEdit83.Text = .OBSTETRI_PERAWAT_87
                TextEdit96.Text = .OBSTETRI_PERAWAT_88
                TextEdit95.Text = .OBSTETRI_PERAWAT_89
                TextEdit94.Text = .OBSTETRI_PERAWAT_90
                TextEdit97.Text = .OBSTETRI_PERAWAT_91
                TextEdit93.Text = .OBSTETRI_PERAWAT_92
                TextEdit71.Text = .OBSTETRI_PERAWAT_93
                TextEdit72.Text = .OBSTETRI_PERAWAT_94
                TextEdit68.Text = .OBSTETRI_PERAWAT_95
                TextEdit84.Text = .OBSTETRI_PERAWAT_96
                TextEdit85.Text = .OBSTETRI_PERAWAT_97
                TextEdit92.Text = .OBSTETRI_PERAWAT_98
                TextEdit98.Text = .OBSTETRI_PERAWAT_99
                TextEdit99.Text = .OBSTETRI_PERAWAT_100
                TextEdit100.Text = .OBSTETRI_PERAWAT_101
                TextEdit101.Text = .OBSTETRI_PERAWAT_102
                TextEdit75.Text = .OBSTETRI_PERAWAT_103
                TextEdit74.Text = .OBSTETRI_PERAWAT_104
                TextEdit69.Text = .OBSTETRI_PERAWAT_105
                TextEdit86.Text = .OBSTETRI_PERAWAT_106
                TextEdit87.Text = .OBSTETRI_PERAWAT_107
                TextEdit102.Text = .OBSTETRI_PERAWAT_108
                TextEdit103.Text = .OBSTETRI_PERAWAT_109
                TextEdit104.Text = .OBSTETRI_PERAWAT_110
                TextEdit105.Text = .OBSTETRI_PERAWAT_111
                TextEdit106.Text = .OBSTETRI_PERAWAT_112
                TextEdit76.Text = .OBSTETRI_PERAWAT_113
                TextEdit77.Text = .OBSTETRI_PERAWAT_114
                TextEdit78.Text = .OBSTETRI_PERAWAT_115
                TextEdit88.Text = .OBSTETRI_PERAWAT_116
                TextEdit89.Text = .OBSTETRI_PERAWAT_117
                TextEdit107.Text = .OBSTETRI_PERAWAT_118
                TextEdit108.Text = .OBSTETRI_PERAWAT_119
                TextEdit109.Text = .OBSTETRI_PERAWAT_120
                TextEdit110.Text = .OBSTETRI_PERAWAT_121
                TextEdit111.Text = .OBSTETRI_PERAWAT_122
                TextEdit81.Text = .OBSTETRI_PERAWAT_123
                TextEdit80.Text = .OBSTETRI_PERAWAT_124
                TextEdit79.Text = .OBSTETRI_PERAWAT_125
                TextEdit90.Text = .OBSTETRI_PERAWAT_126
                TextEdit91.Text = .OBSTETRI_PERAWAT_127
                TextEdit115.Text = .OBSTETRI_PERAWAT_128
                TextEdit116.Text = .OBSTETRI_PERAWAT_129
                TextEdit114.Text = .OBSTETRI_PERAWAT_130
                TextEdit112.Text = .OBSTETRI_PERAWAT_131
                TextEdit113.Text = .OBSTETRI_PERAWAT_132
                CheckBox74.Checked = .OBSTETRI_PERAWAT_133
                CheckBox82.Checked = .OBSTETRI_PERAWAT_134
                TextEdit117.Text = .OBSTETRI_PERAWAT_135
                TextEdit118.Text = .OBSTETRI_PERAWAT_136
                CheckBox83.Checked = .OBSTETRI_PERAWAT_137
                chkSKALANYERI_00.Checked = .OBSTETRI_PERAWAT_138
                chkSKALANYERI_01.Checked = .OBSTETRI_PERAWAT_139
                chkSKALANYERI_02.Checked = .OBSTETRI_PERAWAT_140
                chkSKALANYERI_03.Checked = .OBSTETRI_PERAWAT_141
                chkSKALANYERI_04.Checked = .OBSTETRI_PERAWAT_142
                chkSKALANYERI_05.Checked = .OBSTETRI_PERAWAT_143
                chkSKALANYERI_06.Checked = .OBSTETRI_PERAWAT_144
                chkSKALANYERI_07.Checked = .OBSTETRI_PERAWAT_145
                chkSKALANYERI_08.Checked = .OBSTETRI_PERAWAT_146
                chkSKALANYERI_09.Checked = .OBSTETRI_PERAWAT_147
                chkSKALANYERI_10.Checked = .OBSTETRI_PERAWAT_148
                DateEdit7.EditValue = .OBSTETRI_PERAWAT_149
                DateEdit8.EditValue = .OBSTETRI_PERAWAT_150
                DateEdit9.EditValue = .OBSTETRI_PERAWAT_151
                DateEdit10.EditValue = .OBSTETRI_PERAWAT_152
                DateEdit10.EditValue = .OBSTETRI_PERAWAT_153
                ComboBoxEdit1.EditValue = .OBSTETRI_PERAWAT_154
                ComboBoxEdit2.EditValue = .OBSTETRI_PERAWAT_155
                ComboBoxEdit3.EditValue = .OBSTETRI_PERAWAT_156
                ComboBoxEdit4.EditValue = .OBSTETRI_PERAWAT_157
                ComboBoxEdit5.EditValue = .OBSTETRI_PERAWAT_158
                ComboBoxEdit6.EditValue = .OBSTETRI_PERAWAT_159
                ComboBoxEdit7.EditValue = .OBSTETRI_PERAWAT_160
                ComboBoxEdit8.EditValue = .OBSTETRI_PERAWAT_161
                ComboBoxEdit9.EditValue = .OBSTETRI_PERAWAT_162
                ComboBoxEdit10.EditValue = .OBSTETRI_PERAWAT_163
                ComboBoxEdit11.EditValue = .OBSTETRI_PERAWAT_164
                ComboBoxEdit12.EditValue = .OBSTETRI_PERAWAT_165
                ComboBoxEdit13.EditValue = .OBSTETRI_PERAWAT_166
                ComboBoxEdit14.EditValue = .OBSTETRI_PERAWAT_167
                ComboBoxEdit15.EditValue = .OBSTETRI_PERAWAT_168
                ComboBoxEdit16.EditValue = .OBSTETRI_PERAWAT_169
                ComboBoxEdit17.EditValue = .OBSTETRI_PERAWAT_170
                ComboBoxEdit18.EditValue = .OBSTETRI_PERAWAT_171
                ComboBoxEdit19.EditValue = .OBSTETRI_PERAWAT_172
                ComboBoxEdit20.EditValue = .OBSTETRI_PERAWAT_173
                ComboBoxEdit21.EditValue = .OBSTETRI_PERAWAT_174
                ComboBoxEdit22.EditValue = .OBSTETRI_PERAWAT_175
                ComboBoxEdit23.EditValue = .OBSTETRI_PERAWAT_176
                ComboBoxEdit24.EditValue = .OBSTETRI_PERAWAT_177
                TextEdit119.Text = .OBSTETRI_PERAWAT_178
                TextEdit120.Text = .OBSTETRI_PERAWAT_179
                TextEdit121.Text = .OBSTETRI_PERAWAT_180
                TextEdit122.Text = .OBSTETRI_PERAWAT_181
                TextEdit123.Text = .OBSTETRI_PERAWAT_182
                TextEdit124.Text = .OBSTETRI_PERAWAT_183
                TextEdit125.Text = .OBSTETRI_PERAWAT_184
                TextEdit126.Text = .OBSTETRI_PERAWAT_185
                DateEdit11.EditValue = .OBSTETRI_PERAWAT_186
                DateEdit12.EditValue = .OBSTETRI_PERAWAT_187
                DateEdit13.EditValue = .OBSTETRI_PERAWAT_188
                DateEdit14.EditValue = .OBSTETRI_PERAWAT_189
                TextEdit155.Text = .OBSTETRI_PERAWAT_218
                TextEdit156.Text = .OBSTETRI_PERAWAT_219
                TextEdit157.Text = .OBSTETRI_PERAWAT_220
                TextEdit158.Text = .OBSTETRI_PERAWAT_221
                TextEdit159.Text = .OBSTETRI_PERAWAT_222
                TextEdit160.Text = .OBSTETRI_PERAWAT_223
                TextEdit161.Text = .OBSTETRI_PERAWAT_224
                TextEdit162.Text = .OBSTETRI_PERAWAT_225
                TextEdit163.Text = .OBSTETRI_PERAWAT_226
                TextEdit164.Text = .OBSTETRI_PERAWAT_227
                TextEdit165.Text = .OBSTETRI_PERAWAT_228
                TextEdit166.Text = .OBSTETRI_PERAWAT_229
                CheckBox85.Checked = .OBSTETRI_PERAWAT_230
                CheckBox84.Checked = .OBSTETRI_PERAWAT_231
                CheckBox86.Checked = .OBSTETRI_PERAWAT_232
                CheckBox87.Checked = .OBSTETRI_PERAWAT_233
                CheckBox88.Checked = .OBSTETRI_PERAWAT_234
                TextEdit167.Text = .OBSTETRI_PERAWAT_235
                CheckBox89.Checked = .OBSTETRI_PERAWAT_Bersih
                CheckBox90.Checked = .OBSTETRI_PERAWAT_236
                CheckBox91.Checked = .OBSTETRI_PERAWAT_237
                CheckBox92.Checked = .OBSTETRI_PERAWAT_238
                CheckBox93.Checked = .OBSTETRI_PERAWAT_239
                CheckBox94.Checked = .OBSTETRI_PERAWAT_240
                TextEdit168.Text = .OBSTETRI_PERAWAT_241
                CheckBox95.Checked = .OBSTETRI_PERAWAT_242
                CheckBox96.Checked = .OBSTETRI_PERAWAT_243
                CheckBox97.Checked = .OBSTETRI_PERAWAT_244
                CheckBox98.Checked = .OBSTETRI_PERAWAT_245
                CheckBox390.Checked = .OBSTETRI_PERAWAT_246
                CheckBox231.Checked = .OBSTETRI_PERAWAT_247
                CheckBox99.Checked = .OBSTETRI_PERAWAT_248
                CheckBox101.Checked = .OBSTETRI_PERAWAT_249
                CheckBox102.Checked = .OBSTETRI_PERAWAT_250
                CheckBox103.Checked = .OBSTETRI_PERAWAT_251
                CheckBox100.Checked = .OBSTETRI_PERAWAT_252
                TextEdit169.Text = .OBSTETRI_PERAWAT_253
                CheckBox104.Checked = .OBSTETRI_PERAWAT_254
                CheckBox105.Checked = .OBSTETRI_PERAWAT_255
                TextEdit170.Text = .OBSTETRI_PERAWAT_256
                CheckBox106.Checked = .OBSTETRI_PERAWAT_257
                CheckBox107.Checked = .OBSTETRI_PERAWAT_258
                CheckBox108.Checked = .OBSTETRI_PERAWAT_259
                CheckBox109.Checked = .OBSTETRI_PERAWAT_260
                TextEdit171.Text = .OBSTETRI_PERAWAT_261
                CheckBox110.Checked = .OBSTETRI_PERAWAT_262
                TextEdit172.Text = .OBSTETRI_PERAWAT_263
                CheckBox111.Checked = .OBSTETRI_PERAWAT_264
                CheckBox112.Checked = .OBSTETRI_PERAWAT_265
                CheckBox117.Checked = .OBSTETRI_PERAWAT_266
                CheckBox116.Checked = .OBSTETRI_PERAWAT_267
                CheckBox113.Checked = .OBSTETRI_PERAWAT_268
                CheckBox115.Checked = .OBSTETRI_PERAWAT_269
                CheckBox118.Checked = .OBSTETRI_PERAWAT_270
                CheckBox119.Checked = .OBSTETRI_PERAWAT_271
                TextEdit173.Text = .OBSTETRI_PERAWAT_272
                CheckBox114.Checked = .OBSTETRI_PERAWAT_273
                CheckBox120.Checked = .OBSTETRI_PERAWAT_274
                TextEdit174.Text = .OBSTETRI_PERAWAT_275
                CheckBox121.Checked = .OBSTETRI_PERAWAT_276
                TextEdit175.Text = .OBSTETRI_PERAWAT_277
                CheckBox122.Checked = .OBSTETRI_PERAWAT_278
                CheckBox123.Checked = .OBSTETRI_PERAWAT_279
                CheckBox124.Checked = .OBSTETRI_PERAWAT_280
                CheckBox125.Checked = .OBSTETRI_PERAWAT_281
                CheckBox126.Checked = .OBSTETRI_PERAWAT_282
                CheckBox127.Checked = .OBSTETRI_PERAWAT_283
                CheckBox128.Checked = .OBSTETRI_PERAWAT_284
                CheckBox129.Checked = .OBSTETRI_PERAWAT_285
                CheckBox130.Checked = .OBSTETRI_PERAWAT_286
                TextEdit176.Text = .OBSTETRI_PERAWAT_287
                CheckBox131.Checked = .OBSTETRI_PERAWAT_288
                CheckBox132.Checked = .OBSTETRI_PERAWAT_289
                CheckBox133.Checked = .OBSTETRI_PERAWAT_290
                CheckBox134.Checked = .OBSTETRI_PERAWAT_291
                CheckBox136.Checked = .OBSTETRI_PERAWAT_292
                CheckBox135.Checked = .OBSTETRI_PERAWAT_293
                CheckBox137.Checked = .OBSTETRI_PERAWAT_294
                CheckBox138.Checked = .OBSTETRI_PERAWAT_295
                CheckBox140.Checked = .OBSTETRI_PERAWAT_296
                CheckBox139.Checked = .OBSTETRI_PERAWAT_297
                CheckBox142.Checked = .OBSTETRI_PERAWAT_298
                CheckBox141.Checked = .OBSTETRI_PERAWAT_299
                CheckBox144.Checked = .OBSTETRI_PERAWAT_300
                CheckBox143.Checked = .OBSTETRI_PERAWAT_301
                CheckBox146.Checked = .OBSTETRI_PERAWAT_302
                CheckBox145.Checked = .OBSTETRI_PERAWAT_303
                CheckBox147.Checked = .OBSTETRI_PERAWAT_304
                CheckBox148.Checked = .OBSTETRI_PERAWAT_305
                CheckBox149.Checked = .OBSTETRI_PERAWAT_306
                CheckBox150.Checked = .OBSTETRI_PERAWAT_307
                CheckBox151.Checked = .OBSTETRI_PERAWAT_308
                CheckBox152.Checked = .OBSTETRI_PERAWAT_309
                CheckBox155.Checked = .OBSTETRI_PERAWAT_310
                CheckBox154.Checked = .OBSTETRI_PERAWAT_311
                CheckBox157.Checked = .OBSTETRI_PERAWAT_312
                CheckBox156.Checked = .OBSTETRI_PERAWAT_313
                CheckBox158.Checked = .OBSTETRI_PERAWAT_314
                TextEdit177.Text = .OBSTETRI_PERAWAT_315
                CheckBox159.Checked = .OBSTETRI_PERAWAT_316
                CheckBox160.Checked = .OBSTETRI_PERAWAT_317
                TextEdit178.Text = .OBSTETRI_PERAWAT_318
                CheckBox161.Checked = .OBSTETRI_PERAWAT_319
                TextEdit179.Text = .OBSTETRI_PERAWAT_320
                CheckBox153.Checked = .OBSTETRI_PERAWAT_321
                TextEdit180.Text = .OBSTETRI_PERAWAT_322
                CheckBox165.Checked = .OBSTETRI_PERAWAT_323
                CheckBox164.Checked = .OBSTETRI_PERAWAT_324
                CheckBox163.Checked = .OBSTETRI_PERAWAT_325
                CheckBox167.Checked = .OBSTETRI_PERAWAT_326
                CheckBox166.Checked = .OBSTETRI_PERAWAT_327
                CheckBox169.Checked = .OBSTETRI_PERAWAT_328
                CheckBox171.Checked = .OBSTETRI_PERAWAT_329
                CheckBox170.Checked = .OBSTETRI_PERAWAT_330
                CheckBox172.Checked = .OBSTETRI_PERAWAT_331
                CheckBox173.Checked = .OBSTETRI_PERAWAT_332
                CheckBox174.Checked = .OBSTETRI_PERAWAT_333
                CheckBox175.Checked = .OBSTETRI_PERAWAT_334
                CheckBox176.Checked = .OBSTETRI_PERAWAT_335
                CheckBox177.Checked = .OBSTETRI_PERAWAT_336
                CheckBox178.Checked = .OBSTETRI_PERAWAT_337
                CheckBox179.Checked = .OBSTETRI_PERAWAT_338
                CheckBox180.Checked = .OBSTETRI_PERAWAT_339
                CheckBox181.Checked = .OBSTETRI_PERAWAT_340
                CheckBox182.Checked = .OBSTETRI_PERAWAT_341
                CheckBox187.Checked = .OBSTETRI_PERAWAT_342
                CheckBox186.Checked = .OBSTETRI_PERAWAT_343
                CheckBox185.Checked = .OBSTETRI_PERAWAT_344
                CheckBox184.Checked = .OBSTETRI_PERAWAT_345
                CheckBox183.Checked = .OBSTETRI_PERAWAT_346
                TextEdit181.Text = .OBSTETRI_PERAWAT_347
                CheckBox391.Checked = .OBSTETRI_PERAWAT_348
                CheckBox168.Checked = .OBSTETRI_PERAWAT_349
                TextEdit5.Text = .OBSTETRI_PERAWAT_350
                CheckBox189.Checked = .OBSTETRI_PERAWAT_351
                CheckBox190.Checked = .OBSTETRI_PERAWAT_352
                CheckBox188.Checked = .OBSTETRI_PERAWAT_353
                CheckBox191.Checked = .OBSTETRI_PERAWAT_354
                CheckBox192.Checked = .OBSTETRI_PERAWAT_355
                CheckBox195.Checked = .OBSTETRI_PERAWAT_356
                CheckBox194.Checked = .OBSTETRI_PERAWAT_357
                CheckBox193.Checked = .OBSTETRI_PERAWAT_358
                CheckBox198.Checked = .OBSTETRI_PERAWAT_359
                CheckBox197.Checked = .OBSTETRI_PERAWAT_360
                TextEdit182.Text = .OBSTETRI_PERAWAT_361
                CheckBox196.Checked = .OBSTETRI_PERAWAT_362
                CheckBox199.Checked = .OBSTETRI_PERAWAT_363
                CheckBox200.Checked = .OBSTETRI_PERAWAT_364
                CheckBox201.Checked = .OBSTETRI_PERAWAT_365
                CheckBox202.Checked = .OBSTETRI_PERAWAT_366
                CheckBox203.Checked = .OBSTETRI_PERAWAT_367
                CheckBox204.Checked = .OBSTETRI_PERAWAT_368
                TextEdit183.Text = .OBSTETRI_PERAWAT_369
                CheckBox205.Checked = .OBSTETRI_PERAWAT_370
                CheckBox206.Checked = .OBSTETRI_PERAWAT_371
                CheckBox207.Checked = .OBSTETRI_PERAWAT_372
                CheckBox208.Checked = .OBSTETRI_PERAWAT_373
                CheckBox209.Checked = .OBSTETRI_PERAWAT_374
                TextEdit184.Text = .OBSTETRI_PERAWAT_375
                CheckBox210.Checked = .OBSTETRI_PERAWAT_376
                TextEdit185.Text = .OBSTETRI_PERAWAT_377
                CheckBox211.Checked = .OBSTETRI_PERAWAT_378
                TextEdit186.Text = .OBSTETRI_PERAWAT_379
                CheckBox212.Checked = .OBSTETRI_PERAWAT_380
                CheckBox213.Checked = .OBSTETRI_PERAWAT_381
                TextEdit187.Text = .OBSTETRI_PERAWAT_382
                CheckBox214.Checked = .OBSTETRI_PERAWAT_383
                CheckBox215.Checked = .OBSTETRI_PERAWAT_384
                CheckBox216.Checked = .OBSTETRI_PERAWAT_385
                CheckBox217.Checked = .OBSTETRI_PERAWAT_386
                CheckBox218.Checked = .OBSTETRI_PERAWAT_387
                CheckBox219.Checked = .OBSTETRI_PERAWAT_388
                TextEdit212.Text = .OBSTETRI_PERAWAT_389
                CheckBox162.Checked = .OBSTETRI_PERAWAT_390
                TextEdit213.Text = .OBSTETRI_PERAWAT_391
                CheckBox220.Checked = .OBSTETRI_PERAWAT_392
                TextEdit188.Text = .OBSTETRI_PERAWAT_393
                CheckBox221.Checked = .OBSTETRI_PERAWAT_394
                TextEdit189.Text = .OBSTETRI_PERAWAT_395
                CheckBox222.Checked = .OBSTETRI_PERAWAT_396
                CheckBox223.Checked = .OBSTETRI_PERAWAT_397
                CheckBox224.Checked = .OBSTETRI_PERAWAT_398
                CheckBox225.Checked = .OBSTETRI_PERAWAT_399
                CheckBox226.Checked = .OBSTETRI_PERAWAT_400
                CheckBox227.Checked = .OBSTETRI_PERAWAT_401
                CheckBox228.Checked = .OBSTETRI_PERAWAT_402
                TextEdit190.Text = .OBSTETRI_PERAWAT_403
                CheckBox229.Checked = .OBSTETRI_PERAWAT_404
                CheckBox230.Checked = .OBSTETRI_PERAWAT_405
                CheckBox232.Checked = .OBSTETRI_PERAWAT_406
                CheckBox233.Checked = .OBSTETRI_PERAWAT_407
                CheckBox392.Checked = .OBSTETRI_PERAWAT_408
                CheckBox235.Checked = .OBSTETRI_PERAWAT_409
                CheckBox236.Checked = .OBSTETRI_PERAWAT_410
                TextEdit191.Text = .OBSTETRI_PERAWAT_411
                CheckBox237.Checked = .OBSTETRI_PERAWAT_412
                CheckBox238.Checked = .OBSTETRI_PERAWAT_413
                CheckBox239.Checked = .OBSTETRI_PERAWAT_414
                CheckBox240.Checked = .OBSTETRI_PERAWAT_415
                CheckBox241.Checked = .OBSTETRI_PERAWAT_416
                CheckBox242.Checked = .OBSTETRI_PERAWAT_417
                CheckBox243.Checked = .OBSTETRI_PERAWAT_418
                CheckBox244.Checked = .OBSTETRI_PERAWAT_419
                CheckBox245.Checked = .OBSTETRI_PERAWAT_420
                TextEdit192.Text = .OBSTETRI_PERAWAT_421
                TextEdit193.Text = .OBSTETRI_PERAWAT_422
                CheckBox247.Checked = .OBSTETRI_PERAWAT_423
                CheckBox248.Checked = .OBSTETRI_PERAWAT_424
                CheckBox246.Checked = .OBSTETRI_PERAWAT_425
                CheckBox249.Checked = .OBSTETRI_PERAWAT_426
                CheckBox250.Checked = .OBSTETRI_PERAWAT_427
                CheckBox251.Checked = .OBSTETRI_PERAWAT_428
                CheckBox252.Checked = .OBSTETRI_PERAWAT_429
                TextEdit194.Text = .OBSTETRI_PERAWAT_430
                CheckBox253.Checked = .OBSTETRI_PERAWAT_431
                CheckBox254.Checked = .OBSTETRI_PERAWAT_432
                CheckBox255.Checked = .OBSTETRI_PERAWAT_433
                CheckBox256.Checked = .OBSTETRI_PERAWAT_434
                CheckBox257.Checked = .OBSTETRI_PERAWAT_435
                CheckBox258.Checked = .OBSTETRI_PERAWAT_436
                CheckBox259.Checked = .OBSTETRI_PERAWAT_437
                CheckBox260.Checked = .OBSTETRI_PERAWAT_438
                TextEdit195.Text = .OBSTETRI_PERAWAT_439
                CheckBox261.Checked = .OBSTETRI_PERAWAT_440
                CheckBox262.Checked = .OBSTETRI_PERAWAT_441
                CheckBox263.Checked = .OBSTETRI_PERAWAT_442
                TextEdit196.Text = .OBSTETRI_PERAWAT_443
                CheckBox264.Checked = .OBSTETRI_PERAWAT_444
                CheckBox265.Checked = .OBSTETRI_PERAWAT_445
                CheckBox266.Checked = .OBSTETRI_PERAWAT_446
                CheckBox267.Checked = .OBSTETRI_PERAWAT_447
                CheckBox268.Checked = .OBSTETRI_PERAWAT_448
                CheckBox269.Checked = .OBSTETRI_PERAWAT_449
                CheckBox270.Checked = .OBSTETRI_PERAWAT_450
                CheckBox271.Checked = .OBSTETRI_PERAWAT_451
                CheckBox272.Checked = .OBSTETRI_PERAWAT_452
                CheckBox273.Checked = .OBSTETRI_PERAWAT_453
                CheckBox274.Checked = .OBSTETRI_PERAWAT_454
                CheckBox275.Checked = .OBSTETRI_PERAWAT_455
                CheckBox276.Checked = .OBSTETRI_PERAWAT_456
                CheckBox277.Checked = .OBSTETRI_PERAWAT_457
                CheckBox278.Checked = .OBSTETRI_PERAWAT_458
                CheckBox279.Checked = .OBSTETRI_PERAWAT_459
                CheckBox280.Checked = .OBSTETRI_PERAWAT_460
                CheckBox281.Checked = .OBSTETRI_PERAWAT_461
                CheckBox282.Checked = .OBSTETRI_PERAWAT_462
                TextEdit197.Text = .OBSTETRI_PERAWAT_463
                CheckBox283.Checked = .OBSTETRI_PERAWAT_464
                TextEdit198.Text = .OBSTETRI_PERAWAT_465
                CheckBox284.Checked = .OBSTETRI_PERAWAT_466
                CheckBox285.Checked = .OBSTETRI_PERAWAT_467
                CheckBox286.Checked = .OBSTETRI_PERAWAT_468
                CheckBox287.Checked = .OBSTETRI_PERAWAT_469
                CheckBox288.Checked = .OBSTETRI_PERAWAT_470
                CheckBox289.Checked = .OBSTETRI_PERAWAT_471
                CheckBox290.Checked = .OBSTETRI_PERAWAT_472
                CheckBox291.Checked = .OBSTETRI_PERAWAT_473
                CheckBox292.Checked = .OBSTETRI_PERAWAT_474
                CheckBox293.Checked = .OBSTETRI_PERAWAT_475
                CheckBox294.Checked = .OBSTETRI_PERAWAT_476
                CheckBox295.Checked = .OBSTETRI_PERAWAT_477
                CheckBox296.Checked = .OBSTETRI_PERAWAT_478
                CheckBox297.Checked = .OBSTETRI_PERAWAT_479
                CheckBox298.Checked = .OBSTETRI_PERAWAT_480
                CheckBox299.Checked = .OBSTETRI_PERAWAT_481
                CheckBox300.Checked = .OBSTETRI_PERAWAT_482
                CheckBox301.Checked = .OBSTETRI_PERAWAT_483
                CheckBox302.Checked = .OBSTETRI_PERAWAT_484
                CheckBox303.Checked = .OBSTETRI_PERAWAT_485
                CheckBox304.Checked = .OBSTETRI_PERAWAT_486
                CheckBox305.Checked = .OBSTETRI_PERAWAT_487
                CheckBox306.Checked = .OBSTETRI_PERAWAT_489
                CheckBox307.Checked = .OBSTETRI_PERAWAT_490
                CheckBox308.Checked = .OBSTETRI_PERAWAT_491
                CheckBox309.Checked = .OBSTETRI_PERAWAT_492
                CheckBox310.Checked = .OBSTETRI_PERAWAT_493
                CheckBox312.Checked = .OBSTETRI_PERAWAT_494
                CheckBox313.Checked = .OBSTETRI_PERAWAT_495
                CheckBox314.Checked = .OBSTETRI_PERAWAT_496
                CheckBox315.Checked = .OBSTETRI_PERAWAT_497
                CheckBox316.Checked = .OBSTETRI_PERAWAT_498
                TextEdit199.Text = .OBSTETRI_PERAWAT_499
                TextEdit200.Text = .OBSTETRI_PERAWAT_500
                CheckBox317.Checked = .OBSTETRI_PERAWAT_501
                CheckBox318.Checked = .OBSTETRI_PERAWAT_502
                CheckBox319.Checked = .OBSTETRI_PERAWAT_503
                CheckBox320.Checked = .OBSTETRI_PERAWAT_504
                CheckBox321.Checked = .OBSTETRI_PERAWAT_505
                CheckBox322.Checked = .OBSTETRI_PERAWAT_506
                CheckBox323.Checked = .OBSTETRI_PERAWAT_507
                CheckBox324.Checked = .OBSTETRI_PERAWAT_508
                CheckBox325.Checked = .OBSTETRI_PERAWAT_509
                CheckBox326.Checked = .OBSTETRI_PERAWAT_510
                CheckBox327.Checked = .OBSTETRI_PERAWAT_511
                CheckBox328.Checked = .OBSTETRI_PERAWAT_512
                CheckBox329.Checked = .OBSTETRI_PERAWAT_513
                CheckBox330.Checked = .OBSTETRI_PERAWAT_514
                CheckBox331.Checked = .OBSTETRI_PERAWAT_515
                TextEdit201.Text = .OBSTETRI_PERAWAT_516
                CheckBox333.Checked = .OBSTETRI_PERAWAT_517
                CheckBox332.Checked = .OBSTETRI_PERAWAT_518
                CheckBox335.Checked = .OBSTETRI_PERAWAT_519
                CheckBox334.Checked = .OBSTETRI_PERAWAT_520
                CheckBox336.Checked = .OBSTETRI_PERAWAT_521
                TextEdit202.Text = .OBSTETRI_PERAWAT_522
                CheckBox337.Checked = .OBSTETRI_PERAWAT_523
                CheckBox338.Checked = .OBSTETRI_PERAWAT_524
                CheckBox339.Checked = .OBSTETRI_PERAWAT_525
                CheckBox340.Checked = .OBSTETRI_PERAWAT_526
                TextEdit203.Text = .OBSTETRI_PERAWAT_527
                CheckBox341.Checked = .OBSTETRI_PERAWAT_528
                CheckBox342.Checked = .OBSTETRI_PERAWAT_529
                CheckBox343.Checked = .OBSTETRI_PERAWAT_530
                CheckBox344.Checked = .OBSTETRI_PERAWAT_531
                CheckBox351.Checked = .OBSTETRI_PERAWAT_532
                CheckBox350.Checked = .OBSTETRI_PERAWAT_533
                CheckBox349.Checked = .OBSTETRI_PERAWAT_534
                CheckBox348.Checked = .OBSTETRI_PERAWAT_535
                CheckBox356.Checked = .OBSTETRI_PERAWAT_536
                CheckBox355.Checked = .OBSTETRI_PERAWAT_537
                CheckBox354.Checked = .OBSTETRI_PERAWAT_538
                CheckBox345.Checked = .OBSTETRI_PERAWAT_539
                CheckBox346.Checked = .OBSTETRI_PERAWAT_540
                CheckBox352.Checked = .OBSTETRI_PERAWAT_541
                CheckBox347.Checked = .OBSTETRI_PERAWAT_542
                CheckBox353.Checked = .OBSTETRI_PERAWAT_543
                CheckBox357.Checked = .OBSTETRI_PERAWAT_544
                CheckBox358.Checked = .OBSTETRI_PERAWAT_545
                ComboBoxEdit25.EditValue = .OBSTETRI_PERAWAT_546
                ComboBoxEdit26.EditValue = .OBSTETRI_PERAWAT_547
                ComboBoxEdit27.EditValue = .OBSTETRI_PERAWAT_548
                ComboBoxEdit28.EditValue = .OBSTETRI_PERAWAT_549
                ComboBoxEdit29.EditValue = .OBSTETRI_PERAWAT_550
                TextEdit204.Text = .OBSTETRI_PERAWAT_551
                ComboBoxEdit30.EditValue = .OBSTETRI_PERAWAT_552
                ComboBoxEdit31.EditValue = .OBSTETRI_PERAWAT_553
                ComboBoxEdit32.EditValue = .OBSTETRI_PERAWAT_554
                ComboBoxEdit33.EditValue = .OBSTETRI_PERAWAT_555
                ComboBoxEdit34.EditValue = .OBSTETRI_PERAWAT_556
                ComboBoxEdit35.EditValue = .OBSTETRI_PERAWAT_557
                ComboBoxEdit36.EditValue = .OBSTETRI_PERAWAT_558
                ComboBoxEdit37.EditValue = .OBSTETRI_PERAWAT_559
                ComboBoxEdit38.EditValue = .OBSTETRI_PERAWAT_560
                ComboBoxEdit39.EditValue = .OBSTETRI_PERAWAT_561
                CheckBox360.Checked = .OBSTETRI_PERAWAT_562
                CheckBox359.Checked = .OBSTETRI_PERAWAT_563
                CheckBox362.Checked = .OBSTETRI_PERAWAT_564
                CheckBox361.Checked = .OBSTETRI_PERAWAT_565
                CheckBox363.Checked = .OBSTETRI_PERAWAT_566
                CheckBox364.Checked = .OBSTETRI_PERAWAT_567
                CheckBox365.Checked = .OBSTETRI_PERAWAT_568
                CheckBox366.Checked = .OBSTETRI_PERAWAT_569
                CheckBox367.Checked = .OBSTETRI_PERAWAT_570
                CheckBox368.Checked = .OBSTETRI_PERAWAT_571
                CheckBox369.Checked = .OBSTETRI_PERAWAT_572
                CheckBox370.Checked = .OBSTETRI_PERAWAT_573
                CheckBox371.Checked = .OBSTETRI_PERAWAT_574
                CheckBox372.Checked = .OBSTETRI_PERAWAT_575
                CheckBox373.Checked = .OBSTETRI_PERAWAT_576
                CheckBox374.Checked = .OBSTETRI_PERAWAT_577
                CheckBox375.Checked = .OBSTETRI_PERAWAT_578
                CheckBox376.Checked = .OBSTETRI_PERAWAT_579
                CheckBox377.Checked = .OBSTETRI_PERAWAT_580
                CheckBox378.Checked = .OBSTETRI_PERAWAT_581
                CheckBox379.Checked = .OBSTETRI_PERAWAT_582
                CheckBox380.Checked = .OBSTETRI_PERAWAT_583
                CheckBox381.Checked = .OBSTETRI_PERAWAT_584
                CheckBox382.Checked = .OBSTETRI_PERAWAT_585
                CheckBox383.Checked = .OBSTETRI_PERAWAT_586
                CheckBox384.Checked = .OBSTETRI_PERAWAT_587
                CheckBox385.Checked = .OBSTETRI_PERAWAT_588
                CheckBox386.Checked = .OBSTETRI_PERAWAT_589
                CheckBox387.Checked = .OBSTETRI_PERAWAT_590
                CheckBox388.Checked = .OBSTETRI_PERAWAT_591
                CheckBox389.Checked = .OBSTETRI_PERAWAT_592
                CheckBox393.Checked = .OBSTETRI_PERAWAT_ADD_1
                CheckBox234.Checked = .OBSTETRI_PERAWAT_ADD_2
                CheckBox395.Checked = .OBSTETRI_PERAWAT_ADD_3
                CheckBox394.Checked = .OBSTETRI_PERAWAT_ADD_4
                CheckBox397.Checked = .OBSTETRI_PERAWAT_ADD_5
                CheckBox396.Checked = .OBSTETRI_PERAWAT_ADD_6
                CheckBox405.Checked = .OBSTETRI_PERAWAT_ADD_8
                CheckBox409.Checked = .OBSTETRI_PERAWAT_ADD_9
                CheckBox413.Checked = .OBSTETRI_PERAWAT_ADD_10
                CheckBox417.Checked = .OBSTETRI_PERAWAT_ADD_11
                CheckBox404.Checked = .OBSTETRI_PERAWAT_ADD_13
                CheckBox408.Checked = .OBSTETRI_PERAWAT_ADD_14
                CheckBox412.Checked = .OBSTETRI_PERAWAT_ADD_15
                CheckBox416.Checked = .OBSTETRI_PERAWAT_ADD_16
                CheckBox403.Checked = .OBSTETRI_PERAWAT_ADD_18
                CheckBox407.Checked = .OBSTETRI_PERAWAT_ADD_19
                CheckBox411.Checked = .OBSTETRI_PERAWAT_ADD_20
                CheckBox414.Checked = .OBSTETRI_PERAWAT_ADD_21
                CheckBox400.Checked = .OBSTETRI_PERAWAT_ADD_23
                CheckBox406.Checked = .OBSTETRI_PERAWAT_ADD_24
                CheckBox410.Checked = .OBSTETRI_PERAWAT_ADD_25
                CheckBox415.Checked = .OBSTETRI_PERAWAT_ADD_26
                CheckBox418.Checked = .OBSTETRI_PERAWAT_ADD_27
                CheckBox419.Checked = .OBSTETRI_PERAWAT_ADD_28
                CheckBox420.Checked = .OBSTETRI_PERAWAT_ADD_29
                CheckBox422.Checked = .OBSTETRI_PERAWAT_ADD_30
                CheckBox423.Checked = .OBSTETRI_PERAWAT_ADD_31
                CheckBox424.Checked = .OBSTETRI_PERAWAT_ADD_32
                CheckBox425.Checked = .OBSTETRI_PERAWAT_ADD_33
                CheckBox426.Checked = .OBSTETRI_PERAWAT_ADD_34
                CheckBox427.Checked = .OBSTETRI_PERAWAT_ADD_35
                CheckBox428.Checked = .OBSTETRI_PERAWAT_ADD_36
                CheckBox430.Checked = .OBSTETRI_PERAWAT_ADD_37
                CheckBox429.Checked = .OBSTETRI_PERAWAT_ADD_38
                CheckBox431.Checked = .OBSTETRI_PERAWAT_ADD_39
                CheckBox432.Checked = .OBSTETRI_PERAWAT_ADD_40
                CheckBox439.Checked = .OBSTETRI_PERAWAT_ADD_41
                CheckBox433.Checked = .OBSTETRI_PERAWAT_ADD_42
                CheckBox434.Checked = .OBSTETRI_PERAWAT_ADD_43
                CheckBox436.Checked = .OBSTETRI_PERAWAT_ADD_44
                CheckBox435.Checked = .OBSTETRI_PERAWAT_ADD_45
                CheckBox438.Checked = .OBSTETRI_PERAWAT_ADD_46
                CheckEdit1.Checked = .OBSTETRI_PERAWAT_RESIKOJATUH_1
                CheckEdit2.Checked = .OBSTETRI_PERAWAT_RESIKOJATUH_2
                CheckEdit3.Checked = .OBSTETRI_PERAWAT_RESIKOJATUH_3
                CheckEdit4.Checked = .OBSTETRI_PERAWAT_RESIKOJATUH_4
                CheckEdit5.Checked = .OBSTETRI_PERAWAT_RESIKOJATUH_5
                CheckEdit6.Checked = .OBSTETRI_PERAWAT_RESIKOJATUH_6
                CheckEdit7.Checked = .OBSTETRI_PERAWAT_RESIKOJATUH_7
                CheckEdit8.Checked = .OBSTETRI_PERAWAT_RESIKOJATUH_8
                CheckEdit9.Checked = .OBSTETRI_PERAWAT_RESIKOJATUH_9
                CheckEdit10.Checked = .OBSTETRI_PERAWAT_RESIKOJATUH_10
                CheckEdit11.Checked = .OBSTETRI_PERAWAT_RESIKOJATUH_11
                CheckEdit12.Checked = .OBSTETRI_PERAWAT_RESIKOJATUH_12
                CheckEdit13.Checked = .OBSTETRI_PERAWAT_RESIKOJATUH_13
                CheckEdit14.Checked = .OBSTETRI_PERAWAT_RESIKOJATUH_14
                CheckEdit15.Checked = .OBSTETRI_PERAWAT_RESIKOJATUH_15
                CheckEdit16.Checked = .OBSTETRI_PERAWAT_RESIKOJATUH_16
                CheckEdit17.Checked = .OBSTETRI_PERAWAT_RESIKOJATUH_17
                CheckEdit18.Checked = .OBSTETRI_PERAWAT_RESIKOJATUH_18
                CheckEdit19.Checked = .OBSTETRI_PERAWAT_RESIKOJATUH_19
                CheckEdit20.Checked = .OBSTETRI_PERAWAT_RESIKOJATUH_20
                CheckEdit21.Checked = .OBSTETRI_PERAWAT_RESIKOJATUH_21
                CheckEdit22.Checked = .OBSTETRI_PERAWAT_RESIKOJATUH_22
                CheckEdit23.Checked = .OBSTETRI_PERAWAT_RESIKOJATUH_23
                CheckEdit24.Checked = .OBSTETRI_PERAWAT_RESIKOJATUH_24
                CheckEdit25.Checked = .OBSTETRI_PERAWAT_RESIKOJATUH_25
                CheckEdit26.Checked = .OBSTETRI_PERAWAT_RESIKOJATUH_26
                CheckEdit27.Checked = .OBSTETRI_PERAWAT_RESIKOJATUH_27
                CheckEdit28.Checked = .OBSTETRI_PERAWAT_RESIKOJATUH_28

                TextEdit205.Text = .OBSTETRI_PERAWAT_593
                TextEdit206.Text = .OBSTETRI_PERAWAT_594
                TextEdit207.Text = .OBSTETRI_PERAWAT_595
                TextEdit208.Text = .OBSTETRI_PERAWAT_596
                TextEdit209.Text = .OBSTETRI_PERAWAT_597
                TextEdit210.Text = .OBSTETRI_PERAWAT_598
                TextEdit211.Text = .OBSTETRI_PERAWAT_599
                TextEdit29.Text = .KETERANGAN1
                TextEdit214.Text = .KETERANGAN2
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
            If txtAlamat.Text = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                txtAlamat.Focus()
                fn_Validate = False
                Exit Function
            End If
            'If grdNOIDUSER.Text = String.Empty Then
            ' MsgBox("Dibutuhkan Petugas Triage", MsgBoxStyle.Exclamation, Me.Text)
            ' grdNOIDUSER.Focus()
            ' fn_Validate = False
            ' Exit Function
            'End If
        Catch oErr As Exception
            MsgBox("Validate Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function

    Private Function cekVal(ByVal val As String) As Integer
        If (val = "") Then
            Return 0
        Else
            Return CInt(val)
        End If
    End Function

    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_ASKEP_PEDIATRIK_03.GetStructureHeader
            With ds

                .KDKUNJUNGAN = txtKDKUNJUNGAN.Text
                Try
                    .DATECREATED = oS_DIGITAL_ASKEP_PEDIATRIK_03.GetData(txtKDKUNJUNGAN.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try

                .DATEUPDATED = Now
                .DATE = DateEdit1.EditValue
                .TANGGALPENGKAJIAN = DateEdit1.EditValue
                .PUKULPENGKAJIAN = DateEdit2.EditValue
                .OBSTETRI_PERAWAT_1 = CheckBox9.Checked
                .OBSTETRI_PERAWAT_2 = CheckBox12.Checked
                .OBSTETRI_PERAWAT_3 = TextEdit1.Text
                .OBSTETRI_PERAWAT_4 = TextEdit2.Text
                .OBSTETRI_PERAWAT_6 = CheckBox10.Checked
                .OBSTETRI_PERAWAT_7 = CheckBox15.Checked
                .OBSTETRI_PERAWAT_8 = CheckBox11.Checked
                .OBSTETRI_PERAWAT_9 = CheckBox13.Checked
                .OBSTETRI_PERAWAT_10 = CheckBox14.Checked
                .OBSTETRI_PERAWAT_11 = CheckBox1.Checked
                .OBSTETRI_PERAWAT_12 = CheckBox2.Checked
                .OBSTETRI_PERAWAT_13 = CheckBox3.Checked
                .OBSTETRI_PERAWAT_14 = MemoEdit2.Text
                .OBSTETRI_PERAWAT_15 = MemoEdit3.Text
                .OBSTETRI_PERAWAT_16 = MemoEdit4.Text
                .OBSTETRI_PERAWAT_17 = MemoEdit5.Text
                .OBSTETRI_PERAWAT_18 = MemoEdit6.Text
                .OBSTETRI_PERAWAT_19 = MemoEdit7.Text
                .OBSTETRI_PERAWAT_20 = MemoEdit8.Text
                .OBSTETRI_PERAWAT_21 = TextEdit3.Text
                .OBSTETRI_PERAWAT_22 = TextEdit4.Text
                .OBSTETRI_PERAWAT_23 = TextEdit6.Text
                .OBSTETRI_PERAWAT_24 = TextEdit7.Text
                .OBSTETRI_PERAWAT_25 = TextEdit8.Text
                .OBSTETRI_PERAWAT_26 = TextEdit9.Text
                .OBSTETRI_PERAWAT_27 = TextEdit10.Text
                .OBSTETRI_PERAWAT_28 = TextEdit11.Text
                .OBSTETRI_PERAWAT_29 = TextEdit12.Text
                .OBSTETRI_PERAWAT_30 = TextEdit13.Text
                .OBSTETRI_PERAWAT_31 = TextEdit14.Text
                .OBSTETRI_PERAWAT_32 = TextEdit15.Text
                .OBSTETRI_PERAWAT_33 = CheckBox4.Checked
                .OBSTETRI_PERAWAT_34 = CheckBox5.Checked
                .OBSTETRI_PERAWAT_35 = TextEdit16.Text
                .OBSTETRI_PERAWAT_36 = TextEdit17.Text
                .OBSTETRI_PERAWAT_37 = CheckBox7.Checked
                .OBSTETRI_PERAWAT_38 = CheckBox6.Checked
                .OBSTETRI_PERAWAT_39 = TextEdit19.Text
                .OBSTETRI_PERAWAT_40 = TextEdit18.Text
                .OBSTETRI_PERAWAT_41 = CheckBox16.Checked
                .OBSTETRI_PERAWAT_42 = CheckBox8.Checked
                .OBSTETRI_PERAWAT_43 = TextEdit21.Text
                .OBSTETRI_PERAWAT_44 = TextEdit20.Text
                .OBSTETRI_PERAWAT_45 = CheckBox18.Checked
                .OBSTETRI_PERAWAT_46 = CheckBox17.Checked
                .OBSTETRI_PERAWAT_47 = CheckBox20.Checked
                .OBSTETRI_PERAWAT_48 = CheckBox19.Checked
                .OBSTETRI_PERAWAT_49 = CheckBox22.Checked
                .OBSTETRI_PERAWAT_50 = CheckBox21.Checked
                .OBSTETRI_PERAWAT_51 = CheckBox24.Checked
                .OBSTETRI_PERAWAT_52 = CheckBox23.Checked
                .OBSTETRI_PERAWAT_53 = TextEdit22.Text
                .OBSTETRI_PERAWAT_54 = CheckBox25.Checked
                .OBSTETRI_PERAWAT_55 = CheckBox26.Checked
                .OBSTETRI_PERAWAT_56 = CheckBox28.Checked
                .OBSTETRI_PERAWAT_57 = CheckBox27.Checked
                .OBSTETRI_PERAWAT_58 = CheckBox29.Checked
                .OBSTETRI_PERAWAT_59 = CheckBox30.Checked
                .OBSTETRI_PERAWAT_60 = CheckBox31.Checked
                .OBSTETRI_PERAWAT_61 = CheckBox34.Checked
                .OBSTETRI_PERAWAT_62 = CheckBox33.Checked
                .OBSTETRI_PERAWAT_63 = CheckBox32.Checked
                .OBSTETRI_PERAWAT_64 = CheckBox77.Checked
                .OBSTETRI_PERAWAT_65 = CheckBox76.Checked
                .OBSTETRI_PERAWAT_66 = CheckBox75.Checked
                .OBSTETRI_PERAWAT_67 = CheckBox81.Checked
                .OBSTETRI_PERAWAT_68 = CheckBox80.Checked
                .OBSTETRI_PERAWAT_69 = CheckBox73.Checked
                .OBSTETRI_PERAWAT_70 = CheckBox36.Checked
                .OBSTETRI_PERAWAT_71 = CheckBox35.Checked
                .OBSTETRI_PERAWAT_72 = CheckBox79.Checked
                .OBSTETRI_PERAWAT_73 = CheckBox78.Checked
                .OBSTETRI_PERAWAT_74 = TextEdit23.Text
                .OBSTETRI_PERAWAT_75 = TextEdit25.Text
                .OBSTETRI_PERAWAT_76 = TextEdit66.Text
                .OBSTETRI_PERAWAT_77 = TextEdit24.Text
                .OBSTETRI_PERAWAT_78 = TextEdit26.Text
                .OBSTETRI_PERAWAT_79 = TextEdit65.Text
                .OBSTETRI_PERAWAT_80 = TextEdit64.Text
                .OBSTETRI_PERAWAT_81 = TextEdit27.Text
                .OBSTETRI_PERAWAT_82 = TextEdit28.Text
                .OBSTETRI_PERAWAT_83 = TextEdit70.Text
                .OBSTETRI_PERAWAT_84 = TextEdit73.Text
                .OBSTETRI_PERAWAT_85 = TextEdit67.Text
                .OBSTETRI_PERAWAT_86 = TextEdit82.Text
                .OBSTETRI_PERAWAT_87 = TextEdit83.Text
                .OBSTETRI_PERAWAT_88 = TextEdit96.Text
                .OBSTETRI_PERAWAT_89 = TextEdit95.Text
                .OBSTETRI_PERAWAT_90 = TextEdit94.Text
                .OBSTETRI_PERAWAT_91 = TextEdit97.Text
                .OBSTETRI_PERAWAT_92 = TextEdit93.Text
                .OBSTETRI_PERAWAT_93 = TextEdit71.Text
                .OBSTETRI_PERAWAT_94 = TextEdit72.Text
                .OBSTETRI_PERAWAT_95 = TextEdit68.Text
                .OBSTETRI_PERAWAT_96 = TextEdit84.Text
                .OBSTETRI_PERAWAT_97 = TextEdit85.Text
                .OBSTETRI_PERAWAT_98 = TextEdit92.Text
                .OBSTETRI_PERAWAT_99 = TextEdit98.Text
                .OBSTETRI_PERAWAT_100 = TextEdit99.Text
                .OBSTETRI_PERAWAT_101 = TextEdit100.Text
                .OBSTETRI_PERAWAT_102 = TextEdit101.Text
                .OBSTETRI_PERAWAT_103 = TextEdit75.Text
                .OBSTETRI_PERAWAT_104 = TextEdit74.Text
                .OBSTETRI_PERAWAT_105 = TextEdit69.Text
                .OBSTETRI_PERAWAT_106 = TextEdit86.Text
                .OBSTETRI_PERAWAT_107 = TextEdit87.Text
                .OBSTETRI_PERAWAT_108 = TextEdit102.Text
                .OBSTETRI_PERAWAT_109 = TextEdit103.Text
                .OBSTETRI_PERAWAT_110 = TextEdit104.Text
                .OBSTETRI_PERAWAT_111 = TextEdit105.Text
                .OBSTETRI_PERAWAT_112 = TextEdit106.Text
                .OBSTETRI_PERAWAT_113 = TextEdit76.Text
                .OBSTETRI_PERAWAT_114 = TextEdit77.Text
                .OBSTETRI_PERAWAT_115 = TextEdit78.Text
                .OBSTETRI_PERAWAT_116 = TextEdit88.Text
                .OBSTETRI_PERAWAT_117 = TextEdit89.Text
                .OBSTETRI_PERAWAT_118 = TextEdit107.Text
                .OBSTETRI_PERAWAT_119 = TextEdit108.Text
                .OBSTETRI_PERAWAT_120 = TextEdit109.Text
                .OBSTETRI_PERAWAT_121 = TextEdit110.Text
                .OBSTETRI_PERAWAT_122 = TextEdit111.Text
                .OBSTETRI_PERAWAT_123 = TextEdit81.Text
                .OBSTETRI_PERAWAT_124 = TextEdit80.Text
                .OBSTETRI_PERAWAT_125 = TextEdit79.Text
                .OBSTETRI_PERAWAT_126 = TextEdit90.Text
                .OBSTETRI_PERAWAT_127 = TextEdit91.Text
                .OBSTETRI_PERAWAT_128 = TextEdit115.Text
                .OBSTETRI_PERAWAT_129 = TextEdit116.Text
                .OBSTETRI_PERAWAT_130 = TextEdit114.Text
                .OBSTETRI_PERAWAT_131 = TextEdit112.Text
                .OBSTETRI_PERAWAT_132 = TextEdit113.Text
                .OBSTETRI_PERAWAT_133 = CheckBox74.Checked
                .OBSTETRI_PERAWAT_134 = CheckBox82.Checked
                .OBSTETRI_PERAWAT_135 = TextEdit117.Text
                .OBSTETRI_PERAWAT_136 = TextEdit118.Text
                .OBSTETRI_PERAWAT_137 = CheckBox83.Checked
                .OBSTETRI_PERAWAT_138 = chkSKALANYERI_00.Checked
                .OBSTETRI_PERAWAT_139 = chkSKALANYERI_01.Checked
                .OBSTETRI_PERAWAT_140 = chkSKALANYERI_02.Checked
                .OBSTETRI_PERAWAT_141 = chkSKALANYERI_03.Checked
                .OBSTETRI_PERAWAT_142 = chkSKALANYERI_04.Checked
                .OBSTETRI_PERAWAT_143 = chkSKALANYERI_05.Checked
                .OBSTETRI_PERAWAT_144 = chkSKALANYERI_06.Checked
                .OBSTETRI_PERAWAT_145 = chkSKALANYERI_07.Checked
                .OBSTETRI_PERAWAT_146 = chkSKALANYERI_08.Checked
                .OBSTETRI_PERAWAT_147 = chkSKALANYERI_09.Checked
                .OBSTETRI_PERAWAT_148 = chkSKALANYERI_10.Checked
                .OBSTETRI_PERAWAT_149 = DateEdit7.EditValue
                .OBSTETRI_PERAWAT_150 = DateEdit8.EditValue
                .OBSTETRI_PERAWAT_151 = DateEdit9.EditValue
                .OBSTETRI_PERAWAT_152 = DateEdit10.EditValue
                .OBSTETRI_PERAWAT_153 = DateEdit10.EditValue
                .OBSTETRI_PERAWAT_154 = ComboBoxEdit1.EditValue
                .OBSTETRI_PERAWAT_155 = ComboBoxEdit2.EditValue
                .OBSTETRI_PERAWAT_156 = ComboBoxEdit3.EditValue
                .OBSTETRI_PERAWAT_157 = ComboBoxEdit4.EditValue
                .OBSTETRI_PERAWAT_158 = ComboBoxEdit5.EditValue
                .OBSTETRI_PERAWAT_159 = ComboBoxEdit6.EditValue
                .OBSTETRI_PERAWAT_160 = ComboBoxEdit7.EditValue
                .OBSTETRI_PERAWAT_161 = ComboBoxEdit8.EditValue
                .OBSTETRI_PERAWAT_162 = ComboBoxEdit9.EditValue
                .OBSTETRI_PERAWAT_163 = ComboBoxEdit10.EditValue
                .OBSTETRI_PERAWAT_164 = ComboBoxEdit11.EditValue
                .OBSTETRI_PERAWAT_165 = ComboBoxEdit12.EditValue
                .OBSTETRI_PERAWAT_166 = ComboBoxEdit13.EditValue
                .OBSTETRI_PERAWAT_167 = ComboBoxEdit14.EditValue
                .OBSTETRI_PERAWAT_168 = ComboBoxEdit15.EditValue
                .OBSTETRI_PERAWAT_169 = ComboBoxEdit16.EditValue
                .OBSTETRI_PERAWAT_170 = ComboBoxEdit17.EditValue
                .OBSTETRI_PERAWAT_171 = ComboBoxEdit18.EditValue
                .OBSTETRI_PERAWAT_172 = ComboBoxEdit19.EditValue
                .OBSTETRI_PERAWAT_173 = ComboBoxEdit20.EditValue
                .OBSTETRI_PERAWAT_174 = ComboBoxEdit21.EditValue
                .OBSTETRI_PERAWAT_175 = ComboBoxEdit22.EditValue
                .OBSTETRI_PERAWAT_176 = ComboBoxEdit23.EditValue
                .OBSTETRI_PERAWAT_177 = ComboBoxEdit24.EditValue
                .OBSTETRI_PERAWAT_178 = TextEdit119.Text
                .OBSTETRI_PERAWAT_179 = TextEdit120.Text
                .OBSTETRI_PERAWAT_180 = TextEdit121.Text
                .OBSTETRI_PERAWAT_181 = TextEdit122.Text
                .OBSTETRI_PERAWAT_182 = TextEdit123.Text
                .OBSTETRI_PERAWAT_183 = TextEdit124.Text
                .OBSTETRI_PERAWAT_184 = TextEdit125.Text
                .OBSTETRI_PERAWAT_185 = TextEdit126.Text
                .OBSTETRI_PERAWAT_186 = DateEdit11.EditValue
                .OBSTETRI_PERAWAT_187 = DateEdit12.EditValue
                .OBSTETRI_PERAWAT_188 = DateEdit13.EditValue
                .OBSTETRI_PERAWAT_189 = DateEdit14.EditValue
                .OBSTETRI_PERAWAT_190 = String.Empty
                .OBSTETRI_PERAWAT_191 = String.Empty
                .OBSTETRI_PERAWAT_192 = String.Empty
                .OBSTETRI_PERAWAT_193 = String.Empty
                .OBSTETRI_PERAWAT_194 = String.Empty
                .OBSTETRI_PERAWAT_195 = String.Empty
                .OBSTETRI_PERAWAT_196 = String.Empty
                .OBSTETRI_PERAWAT_197 = String.Empty
                .OBSTETRI_PERAWAT_198 = String.Empty
                .OBSTETRI_PERAWAT_199 = String.Empty
                .OBSTETRI_PERAWAT_200 = String.Empty
                .OBSTETRI_PERAWAT_201 = String.Empty
                .OBSTETRI_PERAWAT_202 = String.Empty
                .OBSTETRI_PERAWAT_203 = String.Empty
                .OBSTETRI_PERAWAT_204 = String.Empty
                .OBSTETRI_PERAWAT_205 = String.Empty
                .OBSTETRI_PERAWAT_206 = String.Empty
                .OBSTETRI_PERAWAT_207 = String.Empty
                .OBSTETRI_PERAWAT_208 = String.Empty
                .OBSTETRI_PERAWAT_209 = String.Empty
                .OBSTETRI_PERAWAT_210 = String.Empty
                .OBSTETRI_PERAWAT_211 = String.Empty
                .OBSTETRI_PERAWAT_212 = String.Empty
                .OBSTETRI_PERAWAT_213 = String.Empty
                .OBSTETRI_PERAWAT_214 = String.Empty
                .OBSTETRI_PERAWAT_215 = String.Empty
                .OBSTETRI_PERAWAT_216 = String.Empty
                .OBSTETRI_PERAWAT_217 = String.Empty
                .OBSTETRI_PERAWAT_218 = TextEdit155.Text
                .OBSTETRI_PERAWAT_219 = TextEdit156.Text
                .OBSTETRI_PERAWAT_220 = TextEdit157.Text
                .OBSTETRI_PERAWAT_221 = TextEdit158.Text
                .OBSTETRI_PERAWAT_222 = TextEdit159.Text
                .OBSTETRI_PERAWAT_223 = TextEdit160.Text
                .OBSTETRI_PERAWAT_224 = TextEdit161.Text
                .OBSTETRI_PERAWAT_225 = TextEdit162.Text
                .OBSTETRI_PERAWAT_226 = TextEdit163.Text
                .OBSTETRI_PERAWAT_227 = TextEdit164.Text
                .OBSTETRI_PERAWAT_228 = TextEdit165.Text
                .OBSTETRI_PERAWAT_229 = TextEdit166.Text
                .OBSTETRI_PERAWAT_230 = CheckBox85.Checked
                .OBSTETRI_PERAWAT_231 = CheckBox84.Checked
                .OBSTETRI_PERAWAT_232 = CheckBox86.Checked
                .OBSTETRI_PERAWAT_233 = CheckBox87.Checked
                .OBSTETRI_PERAWAT_234 = CheckBox88.Checked
                .OBSTETRI_PERAWAT_235 = TextEdit167.Text
                .OBSTETRI_PERAWAT_Bersih = CheckBox89.Checked
                .OBSTETRI_PERAWAT_236 = CheckBox90.Checked
                .OBSTETRI_PERAWAT_237 = CheckBox91.Checked
                .OBSTETRI_PERAWAT_238 = CheckBox92.Checked
                .OBSTETRI_PERAWAT_239 = CheckBox93.Checked
                .OBSTETRI_PERAWAT_240 = CheckBox94.Checked
                .OBSTETRI_PERAWAT_241 = TextEdit168.Text
                .OBSTETRI_PERAWAT_242 = CheckBox95.Checked
                .OBSTETRI_PERAWAT_243 = CheckBox96.Checked
                .OBSTETRI_PERAWAT_244 = CheckBox97.Checked
                .OBSTETRI_PERAWAT_245 = CheckBox98.Checked
                .OBSTETRI_PERAWAT_246 = CheckBox390.Checked
                .OBSTETRI_PERAWAT_247 = CheckBox231.Checked
                .OBSTETRI_PERAWAT_248 = CheckBox99.Checked
                .OBSTETRI_PERAWAT_249 = CheckBox101.Checked
                .OBSTETRI_PERAWAT_250 = CheckBox102.Checked
                .OBSTETRI_PERAWAT_251 = CheckBox103.Checked
                .OBSTETRI_PERAWAT_252 = CheckBox100.Checked
                .OBSTETRI_PERAWAT_253 = TextEdit169.Text
                .OBSTETRI_PERAWAT_254 = CheckBox104.Checked
                .OBSTETRI_PERAWAT_255 = CheckBox105.Checked
                .OBSTETRI_PERAWAT_256 = TextEdit170.Text
                .OBSTETRI_PERAWAT_257 = CheckBox106.Checked
                .OBSTETRI_PERAWAT_258 = CheckBox107.Checked
                .OBSTETRI_PERAWAT_259 = CheckBox108.Checked
                .OBSTETRI_PERAWAT_260 = CheckBox109.Checked
                .OBSTETRI_PERAWAT_261 = TextEdit171.Text
                .OBSTETRI_PERAWAT_262 = CheckBox110.Checked
                .OBSTETRI_PERAWAT_263 = TextEdit172.Text
                .OBSTETRI_PERAWAT_264 = CheckBox111.Checked
                .OBSTETRI_PERAWAT_265 = CheckBox112.Checked
                .OBSTETRI_PERAWAT_266 = CheckBox117.Checked
                .OBSTETRI_PERAWAT_267 = CheckBox116.Checked
                .OBSTETRI_PERAWAT_268 = CheckBox113.Checked
                .OBSTETRI_PERAWAT_269 = CheckBox115.Checked
                .OBSTETRI_PERAWAT_270 = CheckBox118.Checked
                .OBSTETRI_PERAWAT_271 = CheckBox119.Checked
                .OBSTETRI_PERAWAT_272 = TextEdit173.Text
                .OBSTETRI_PERAWAT_273 = CheckBox114.Checked
                .OBSTETRI_PERAWAT_274 = CheckBox120.Checked
                .OBSTETRI_PERAWAT_275 = TextEdit174.Text
                .OBSTETRI_PERAWAT_276 = CheckBox121.Checked
                .OBSTETRI_PERAWAT_277 = TextEdit175.Text
                .OBSTETRI_PERAWAT_278 = CheckBox122.Checked
                .OBSTETRI_PERAWAT_279 = CheckBox123.Checked
                .OBSTETRI_PERAWAT_280 = CheckBox124.Checked
                .OBSTETRI_PERAWAT_281 = CheckBox125.Checked
                .OBSTETRI_PERAWAT_282 = CheckBox126.Checked
                .OBSTETRI_PERAWAT_283 = CheckBox127.Checked
                .OBSTETRI_PERAWAT_284 = CheckBox128.Checked
                .OBSTETRI_PERAWAT_285 = CheckBox129.Checked
                .OBSTETRI_PERAWAT_286 = CheckBox130.Checked
                .OBSTETRI_PERAWAT_287 = TextEdit176.Text
                .OBSTETRI_PERAWAT_288 = CheckBox131.Checked
                .OBSTETRI_PERAWAT_289 = CheckBox132.Checked
                .OBSTETRI_PERAWAT_290 = CheckBox133.Checked
                .OBSTETRI_PERAWAT_291 = CheckBox134.Checked
                .OBSTETRI_PERAWAT_292 = CheckBox136.Checked
                .OBSTETRI_PERAWAT_293 = CheckBox135.Checked
                .OBSTETRI_PERAWAT_294 = CheckBox137.Checked
                .OBSTETRI_PERAWAT_295 = CheckBox138.Checked
                .OBSTETRI_PERAWAT_296 = CheckBox140.Checked
                .OBSTETRI_PERAWAT_297 = CheckBox139.Checked
                .OBSTETRI_PERAWAT_298 = CheckBox142.Checked
                .OBSTETRI_PERAWAT_299 = CheckBox141.Checked
                .OBSTETRI_PERAWAT_300 = CheckBox144.Checked
                .OBSTETRI_PERAWAT_301 = CheckBox143.Checked
                .OBSTETRI_PERAWAT_302 = CheckBox146.Checked
                .OBSTETRI_PERAWAT_303 = CheckBox145.Checked
                .OBSTETRI_PERAWAT_304 = CheckBox147.Checked
                .OBSTETRI_PERAWAT_305 = CheckBox148.Checked
                .OBSTETRI_PERAWAT_306 = CheckBox149.Checked
                .OBSTETRI_PERAWAT_307 = CheckBox150.Checked
                .OBSTETRI_PERAWAT_308 = CheckBox151.Checked
                .OBSTETRI_PERAWAT_309 = CheckBox152.Checked
                .OBSTETRI_PERAWAT_310 = CheckBox155.Checked
                .OBSTETRI_PERAWAT_311 = CheckBox154.Checked
                .OBSTETRI_PERAWAT_312 = CheckBox157.Checked
                .OBSTETRI_PERAWAT_313 = CheckBox156.Checked
                .OBSTETRI_PERAWAT_314 = CheckBox158.Checked
                .OBSTETRI_PERAWAT_315 = TextEdit177.Text
                .OBSTETRI_PERAWAT_316 = CheckBox159.Checked
                .OBSTETRI_PERAWAT_317 = CheckBox160.Checked
                .OBSTETRI_PERAWAT_318 = TextEdit178.Text
                .OBSTETRI_PERAWAT_319 = CheckBox161.Checked
                .OBSTETRI_PERAWAT_320 = TextEdit179.Text
                .OBSTETRI_PERAWAT_321 = CheckBox153.Checked
                .OBSTETRI_PERAWAT_322 = TextEdit180.Text
                .OBSTETRI_PERAWAT_323 = CheckBox165.Checked
                .OBSTETRI_PERAWAT_324 = CheckBox164.Checked
                .OBSTETRI_PERAWAT_325 = CheckBox163.Checked
                .OBSTETRI_PERAWAT_326 = CheckBox167.Checked
                .OBSTETRI_PERAWAT_327 = CheckBox166.Checked
                .OBSTETRI_PERAWAT_328 = CheckBox169.Checked
                .OBSTETRI_PERAWAT_329 = CheckBox171.Checked
                .OBSTETRI_PERAWAT_330 = CheckBox170.Checked
                .OBSTETRI_PERAWAT_331 = CheckBox172.Checked
                .OBSTETRI_PERAWAT_332 = CheckBox173.Checked
                .OBSTETRI_PERAWAT_333 = CheckBox174.Checked
                .OBSTETRI_PERAWAT_334 = CheckBox175.Checked
                .OBSTETRI_PERAWAT_335 = CheckBox176.Checked
                .OBSTETRI_PERAWAT_336 = CheckBox177.Checked
                .OBSTETRI_PERAWAT_337 = CheckBox178.Checked
                .OBSTETRI_PERAWAT_338 = CheckBox179.Checked
                .OBSTETRI_PERAWAT_339 = CheckBox180.Checked
                .OBSTETRI_PERAWAT_340 = CheckBox181.Checked
                .OBSTETRI_PERAWAT_341 = CheckBox182.Checked
                .OBSTETRI_PERAWAT_342 = CheckBox187.Checked
                .OBSTETRI_PERAWAT_343 = CheckBox186.Checked
                .OBSTETRI_PERAWAT_344 = CheckBox185.Checked
                .OBSTETRI_PERAWAT_345 = CheckBox184.Checked
                .OBSTETRI_PERAWAT_346 = CheckBox183.Checked
                .OBSTETRI_PERAWAT_347 = TextEdit181.Text
                .OBSTETRI_PERAWAT_348 = CheckBox391.Checked
                .OBSTETRI_PERAWAT_349 = CheckBox168.Checked
                .OBSTETRI_PERAWAT_350 = TextEdit5.Text
                .OBSTETRI_PERAWAT_351 = CheckBox189.Checked
                .OBSTETRI_PERAWAT_352 = CheckBox190.Checked
                .OBSTETRI_PERAWAT_353 = CheckBox188.Checked
                .OBSTETRI_PERAWAT_354 = CheckBox191.Checked
                .OBSTETRI_PERAWAT_355 = CheckBox192.Checked
                .OBSTETRI_PERAWAT_356 = CheckBox195.Checked
                .OBSTETRI_PERAWAT_357 = CheckBox194.Checked
                .OBSTETRI_PERAWAT_358 = CheckBox193.Checked
                .OBSTETRI_PERAWAT_359 = CheckBox198.Checked
                .OBSTETRI_PERAWAT_360 = CheckBox197.Checked
                .OBSTETRI_PERAWAT_361 = TextEdit182.Text
                .OBSTETRI_PERAWAT_362 = CheckBox196.Checked
                .OBSTETRI_PERAWAT_363 = CheckBox199.Checked
                .OBSTETRI_PERAWAT_364 = CheckBox200.Checked
                .OBSTETRI_PERAWAT_365 = CheckBox201.Checked
                .OBSTETRI_PERAWAT_366 = CheckBox202.Checked
                .OBSTETRI_PERAWAT_367 = CheckBox203.Checked
                .OBSTETRI_PERAWAT_368 = CheckBox204.Checked
                .OBSTETRI_PERAWAT_369 = TextEdit183.Text
                .OBSTETRI_PERAWAT_370 = CheckBox205.Checked
                .OBSTETRI_PERAWAT_371 = CheckBox206.Checked
                .OBSTETRI_PERAWAT_372 = CheckBox207.Checked
                .OBSTETRI_PERAWAT_373 = CheckBox208.Checked
                .OBSTETRI_PERAWAT_374 = CheckBox209.Checked
                .OBSTETRI_PERAWAT_375 = TextEdit184.Text
                .OBSTETRI_PERAWAT_376 = CheckBox210.Checked
                .OBSTETRI_PERAWAT_377 = TextEdit185.Text
                .OBSTETRI_PERAWAT_378 = CheckBox211.Checked
                .OBSTETRI_PERAWAT_379 = TextEdit186.Text
                .OBSTETRI_PERAWAT_380 = CheckBox212.Checked
                .OBSTETRI_PERAWAT_381 = CheckBox213.Checked
                .OBSTETRI_PERAWAT_382 = TextEdit187.Text
                .OBSTETRI_PERAWAT_383 = CheckBox214.Checked
                .OBSTETRI_PERAWAT_384 = CheckBox215.Checked
                .OBSTETRI_PERAWAT_385 = CheckBox216.Checked
                .OBSTETRI_PERAWAT_386 = CheckBox217.Checked
                .OBSTETRI_PERAWAT_387 = CheckBox218.Checked
                .OBSTETRI_PERAWAT_388 = CheckBox219.Checked
                .OBSTETRI_PERAWAT_389 = TextEdit212.Text
                .OBSTETRI_PERAWAT_390 = CheckBox162.Checked
                .OBSTETRI_PERAWAT_391 = TextEdit213.Text
                .OBSTETRI_PERAWAT_392 = CheckBox220.Checked
                .OBSTETRI_PERAWAT_393 = TextEdit188.Text
                .OBSTETRI_PERAWAT_394 = CheckBox221.Checked
                .OBSTETRI_PERAWAT_395 = TextEdit189.Text
                .OBSTETRI_PERAWAT_396 = CheckBox222.Checked
                .OBSTETRI_PERAWAT_397 = CheckBox223.Checked
                .OBSTETRI_PERAWAT_398 = CheckBox224.Checked
                .OBSTETRI_PERAWAT_399 = CheckBox225.Checked
                .OBSTETRI_PERAWAT_400 = CheckBox226.Checked
                .OBSTETRI_PERAWAT_401 = CheckBox227.Checked
                .OBSTETRI_PERAWAT_402 = CheckBox228.Checked
                .OBSTETRI_PERAWAT_403 = TextEdit190.Text
                .OBSTETRI_PERAWAT_404 = CheckBox229.Checked
                .OBSTETRI_PERAWAT_405 = CheckBox230.Checked
                .OBSTETRI_PERAWAT_406 = CheckBox232.Checked
                .OBSTETRI_PERAWAT_407 = CheckBox233.Checked
                .OBSTETRI_PERAWAT_408 = CheckBox392.Checked
                .OBSTETRI_PERAWAT_409 = CheckBox235.Checked
                .OBSTETRI_PERAWAT_410 = CheckBox236.Checked
                .OBSTETRI_PERAWAT_411 = TextEdit191.Text
                .OBSTETRI_PERAWAT_412 = CheckBox237.Checked
                .OBSTETRI_PERAWAT_413 = CheckBox238.Checked
                .OBSTETRI_PERAWAT_414 = CheckBox239.Checked
                .OBSTETRI_PERAWAT_415 = CheckBox240.Checked
                .OBSTETRI_PERAWAT_416 = CheckBox241.Checked
                .OBSTETRI_PERAWAT_417 = CheckBox242.Checked
                .OBSTETRI_PERAWAT_418 = CheckBox243.Checked
                .OBSTETRI_PERAWAT_419 = CheckBox244.Checked
                .OBSTETRI_PERAWAT_420 = CheckBox245.Checked
                .OBSTETRI_PERAWAT_421 = TextEdit192.Text
                .OBSTETRI_PERAWAT_422 = TextEdit193.Text
                .OBSTETRI_PERAWAT_423 = CheckBox247.Checked
                .OBSTETRI_PERAWAT_424 = CheckBox248.Checked
                .OBSTETRI_PERAWAT_425 = CheckBox246.Checked
                .OBSTETRI_PERAWAT_426 = CheckBox249.Checked
                .OBSTETRI_PERAWAT_427 = CheckBox250.Checked
                .OBSTETRI_PERAWAT_428 = CheckBox251.Checked
                .OBSTETRI_PERAWAT_429 = CheckBox252.Checked
                .OBSTETRI_PERAWAT_430 = TextEdit194.Text
                .OBSTETRI_PERAWAT_431 = CheckBox253.Checked
                .OBSTETRI_PERAWAT_432 = CheckBox254.Checked
                .OBSTETRI_PERAWAT_433 = CheckBox255.Checked
                .OBSTETRI_PERAWAT_434 = CheckBox256.Checked
                .OBSTETRI_PERAWAT_435 = CheckBox257.Checked
                .OBSTETRI_PERAWAT_436 = CheckBox258.Checked
                .OBSTETRI_PERAWAT_437 = CheckBox259.Checked
                .OBSTETRI_PERAWAT_438 = CheckBox260.Checked
                .OBSTETRI_PERAWAT_439 = TextEdit195.Text
                .OBSTETRI_PERAWAT_440 = CheckBox261.Checked
                .OBSTETRI_PERAWAT_441 = CheckBox262.Checked
                .OBSTETRI_PERAWAT_442 = CheckBox263.Checked
                .OBSTETRI_PERAWAT_443 = TextEdit196.Text
                .OBSTETRI_PERAWAT_444 = CheckBox264.Checked
                .OBSTETRI_PERAWAT_445 = CheckBox265.Checked
                .OBSTETRI_PERAWAT_446 = CheckBox266.Checked
                .OBSTETRI_PERAWAT_447 = CheckBox267.Checked
                .OBSTETRI_PERAWAT_448 = CheckBox268.Checked
                .OBSTETRI_PERAWAT_449 = CheckBox269.Checked
                .OBSTETRI_PERAWAT_450 = CheckBox270.Checked
                .OBSTETRI_PERAWAT_451 = CheckBox271.Checked
                .OBSTETRI_PERAWAT_452 = CheckBox272.Checked
                .OBSTETRI_PERAWAT_453 = CheckBox273.Checked
                .OBSTETRI_PERAWAT_454 = CheckBox274.Checked
                .OBSTETRI_PERAWAT_455 = CheckBox275.Checked
                .OBSTETRI_PERAWAT_456 = CheckBox276.Checked
                .OBSTETRI_PERAWAT_457 = CheckBox277.Checked
                .OBSTETRI_PERAWAT_458 = CheckBox278.Checked
                .OBSTETRI_PERAWAT_459 = CheckBox279.Checked
                .OBSTETRI_PERAWAT_460 = CheckBox280.Checked
                .OBSTETRI_PERAWAT_461 = CheckBox281.Checked
                .OBSTETRI_PERAWAT_462 = CheckBox282.Checked
                .OBSTETRI_PERAWAT_463 = TextEdit197.Text
                .OBSTETRI_PERAWAT_464 = CheckBox283.Checked
                .OBSTETRI_PERAWAT_465 = TextEdit198.Text
                .OBSTETRI_PERAWAT_466 = CheckBox284.Checked
                .OBSTETRI_PERAWAT_467 = CheckBox285.Checked
                .OBSTETRI_PERAWAT_468 = CheckBox286.Checked
                .OBSTETRI_PERAWAT_469 = CheckBox287.Checked
                .OBSTETRI_PERAWAT_470 = CheckBox288.Checked
                .OBSTETRI_PERAWAT_471 = CheckBox289.Checked
                .OBSTETRI_PERAWAT_472 = CheckBox290.Checked
                .OBSTETRI_PERAWAT_473 = CheckBox291.Checked
                .OBSTETRI_PERAWAT_474 = CheckBox292.Checked
                .OBSTETRI_PERAWAT_475 = CheckBox293.Checked
                .OBSTETRI_PERAWAT_476 = CheckBox294.Checked
                .OBSTETRI_PERAWAT_477 = CheckBox295.Checked
                .OBSTETRI_PERAWAT_478 = CheckBox296.Checked
                .OBSTETRI_PERAWAT_479 = CheckBox297.Checked
                .OBSTETRI_PERAWAT_480 = CheckBox298.Checked
                .OBSTETRI_PERAWAT_481 = CheckBox299.Checked
                .OBSTETRI_PERAWAT_482 = CheckBox300.Checked
                .OBSTETRI_PERAWAT_483 = CheckBox301.Checked
                .OBSTETRI_PERAWAT_484 = CheckBox302.Checked
                .OBSTETRI_PERAWAT_485 = CheckBox303.Checked
                .OBSTETRI_PERAWAT_486 = CheckBox304.Checked
                .OBSTETRI_PERAWAT_487 = CheckBox305.Checked
                .OBSTETRI_PERAWAT_489 = CheckBox306.Checked
                .OBSTETRI_PERAWAT_490 = CheckBox307.Checked
                .OBSTETRI_PERAWAT_491 = CheckBox308.Checked
                .OBSTETRI_PERAWAT_492 = CheckBox309.Checked
                .OBSTETRI_PERAWAT_493 = CheckBox310.Checked
                .OBSTETRI_PERAWAT_494 = CheckBox312.Checked
                .OBSTETRI_PERAWAT_495 = CheckBox313.Checked
                .OBSTETRI_PERAWAT_496 = CheckBox314.Checked
                .OBSTETRI_PERAWAT_497 = CheckBox315.Checked
                .OBSTETRI_PERAWAT_498 = CheckBox316.Checked
                .OBSTETRI_PERAWAT_499 = TextEdit199.Text
                .OBSTETRI_PERAWAT_500 = TextEdit200.Text
                .OBSTETRI_PERAWAT_501 = CheckBox317.Checked
                .OBSTETRI_PERAWAT_502 = CheckBox318.Checked
                .OBSTETRI_PERAWAT_503 = CheckBox319.Checked
                .OBSTETRI_PERAWAT_504 = CheckBox320.Checked
                .OBSTETRI_PERAWAT_505 = CheckBox321.Checked
                .OBSTETRI_PERAWAT_506 = CheckBox322.Checked
                .OBSTETRI_PERAWAT_507 = CheckBox323.Checked
                .OBSTETRI_PERAWAT_508 = CheckBox324.Checked
                .OBSTETRI_PERAWAT_509 = CheckBox325.Checked
                .OBSTETRI_PERAWAT_510 = CheckBox326.Checked
                .OBSTETRI_PERAWAT_511 = CheckBox327.Checked
                .OBSTETRI_PERAWAT_512 = CheckBox328.Checked
                .OBSTETRI_PERAWAT_513 = CheckBox329.Checked
                .OBSTETRI_PERAWAT_514 = CheckBox330.Checked
                .OBSTETRI_PERAWAT_515 = CheckBox331.Checked
                .OBSTETRI_PERAWAT_516 = TextEdit201.Text
                .OBSTETRI_PERAWAT_517 = CheckBox333.Checked
                .OBSTETRI_PERAWAT_518 = CheckBox332.Checked
                .OBSTETRI_PERAWAT_519 = CheckBox335.Checked
                .OBSTETRI_PERAWAT_520 = CheckBox334.Checked
                .OBSTETRI_PERAWAT_521 = CheckBox336.Checked
                .OBSTETRI_PERAWAT_522 = TextEdit202.Text
                .OBSTETRI_PERAWAT_523 = CheckBox337.Checked
                .OBSTETRI_PERAWAT_524 = CheckBox338.Checked
                .OBSTETRI_PERAWAT_525 = CheckBox339.Checked
                .OBSTETRI_PERAWAT_526 = CheckBox340.Checked
                .OBSTETRI_PERAWAT_527 = TextEdit203.Text
                .OBSTETRI_PERAWAT_528 = CheckBox341.Checked
                .OBSTETRI_PERAWAT_529 = CheckBox342.Checked
                .OBSTETRI_PERAWAT_530 = CheckBox343.Checked
                .OBSTETRI_PERAWAT_531 = CheckBox344.Checked
                .OBSTETRI_PERAWAT_532 = CheckBox351.Checked
                .OBSTETRI_PERAWAT_533 = CheckBox350.Checked
                .OBSTETRI_PERAWAT_534 = CheckBox349.Checked
                .OBSTETRI_PERAWAT_535 = CheckBox348.Checked
                .OBSTETRI_PERAWAT_536 = CheckBox356.Checked
                .OBSTETRI_PERAWAT_537 = CheckBox355.Checked
                .OBSTETRI_PERAWAT_538 = CheckBox354.Checked
                .OBSTETRI_PERAWAT_539 = CheckBox345.Checked
                .OBSTETRI_PERAWAT_540 = CheckBox346.Checked
                .OBSTETRI_PERAWAT_541 = CheckBox352.Checked
                .OBSTETRI_PERAWAT_542 = CheckBox347.Checked
                .OBSTETRI_PERAWAT_543 = CheckBox353.Checked
                .OBSTETRI_PERAWAT_544 = CheckBox357.Checked
                .OBSTETRI_PERAWAT_545 = CheckBox358.Checked
                .OBSTETRI_PERAWAT_546 = ComboBoxEdit25.EditValue
                .OBSTETRI_PERAWAT_547 = ComboBoxEdit26.EditValue
                .OBSTETRI_PERAWAT_548 = ComboBoxEdit27.EditValue
                .OBSTETRI_PERAWAT_549 = ComboBoxEdit28.EditValue
                .OBSTETRI_PERAWAT_550 = ComboBoxEdit29.EditValue
                .OBSTETRI_PERAWAT_551 = 0
                .OBSTETRI_PERAWAT_552 = ComboBoxEdit30.EditValue
                .OBSTETRI_PERAWAT_553 = ComboBoxEdit31.EditValue
                .OBSTETRI_PERAWAT_554 = ComboBoxEdit32.EditValue
                .OBSTETRI_PERAWAT_555 = ComboBoxEdit33.EditValue
                .OBSTETRI_PERAWAT_556 = ComboBoxEdit34.EditValue
                .OBSTETRI_PERAWAT_557 = ComboBoxEdit35.EditValue
                .OBSTETRI_PERAWAT_558 = ComboBoxEdit36.EditValue
                .OBSTETRI_PERAWAT_559 = ComboBoxEdit37.EditValue
                .OBSTETRI_PERAWAT_560 = ComboBoxEdit38.EditValue
                .OBSTETRI_PERAWAT_561 = ComboBoxEdit39.EditValue
                .OBSTETRI_PERAWAT_562 = CheckBox360.Checked
                .OBSTETRI_PERAWAT_563 = CheckBox359.Checked
                .OBSTETRI_PERAWAT_564 = CheckBox362.Checked
                .OBSTETRI_PERAWAT_565 = CheckBox361.Checked
                .OBSTETRI_PERAWAT_566 = CheckBox363.Checked
                .OBSTETRI_PERAWAT_567 = CheckBox364.Checked
                .OBSTETRI_PERAWAT_568 = CheckBox365.Checked
                .OBSTETRI_PERAWAT_569 = CheckBox366.Checked
                .OBSTETRI_PERAWAT_570 = CheckBox367.Checked
                .OBSTETRI_PERAWAT_571 = CheckBox368.Checked
                .OBSTETRI_PERAWAT_572 = CheckBox369.Checked
                .OBSTETRI_PERAWAT_573 = CheckBox370.Checked
                .OBSTETRI_PERAWAT_574 = CheckBox371.Checked
                .OBSTETRI_PERAWAT_575 = CheckBox372.Checked
                .OBSTETRI_PERAWAT_576 = CheckBox373.Checked
                .OBSTETRI_PERAWAT_577 = CheckBox374.Checked
                .OBSTETRI_PERAWAT_578 = CheckBox375.Checked
                .OBSTETRI_PERAWAT_579 = CheckBox376.Checked
                .OBSTETRI_PERAWAT_580 = CheckBox377.Checked
                .OBSTETRI_PERAWAT_581 = CheckBox378.Checked
                .OBSTETRI_PERAWAT_582 = CheckBox379.Checked
                .OBSTETRI_PERAWAT_583 = CheckBox380.Checked
                .OBSTETRI_PERAWAT_584 = CheckBox381.Checked
                .OBSTETRI_PERAWAT_585 = CheckBox382.Checked
                .OBSTETRI_PERAWAT_586 = CheckBox383.Checked
                .OBSTETRI_PERAWAT_587 = CheckBox384.Checked
                .OBSTETRI_PERAWAT_588 = CheckBox385.Checked
                .OBSTETRI_PERAWAT_589 = CheckBox386.Checked
                .OBSTETRI_PERAWAT_590 = CheckBox387.Checked
                .OBSTETRI_PERAWAT_591 = CheckBox388.Checked
                .OBSTETRI_PERAWAT_592 = CheckBox389.Checked
                .OBSTETRI_PERAWAT_593 = TextEdit205.Text
                .OBSTETRI_PERAWAT_594 = TextEdit206.Text
                .OBSTETRI_PERAWAT_595 = TextEdit207.Text
                .OBSTETRI_PERAWAT_596 = TextEdit208.Text
                .OBSTETRI_PERAWAT_597 = TextEdit209.Text
                .OBSTETRI_PERAWAT_598 = TextEdit210.Text
                .OBSTETRI_PERAWAT_599 = TextEdit211.Text
                .KETERANGAN1 = TextEdit29.Text
                .KETERANGAN2 = TextEdit214.Text
                .KETERANGAN3 = String.Empty
                .KETERANGAN4 = String.Empty
                .NAMAPERAWAT = String.Empty
                .KODEPERAWAT = String.Empty
                .OBSTETRI_PERAWAT_ADD_1 = CheckBox393.Checked
                .OBSTETRI_PERAWAT_ADD_2 = CheckBox234.Checked
                .OBSTETRI_PERAWAT_ADD_3 = CheckBox395.Checked
                .OBSTETRI_PERAWAT_ADD_4 = CheckBox394.Checked
                .OBSTETRI_PERAWAT_ADD_5 = CheckBox397.Checked
                .OBSTETRI_PERAWAT_ADD_6 = CheckBox396.Checked
                .OBSTETRI_PERAWAT_ADD_7 = False
                .OBSTETRI_PERAWAT_ADD_8 = CheckBox405.Checked
                .OBSTETRI_PERAWAT_ADD_9 = CheckBox409.Checked
                .OBSTETRI_PERAWAT_ADD_10 = CheckBox413.Checked
                .OBSTETRI_PERAWAT_ADD_11 = CheckBox417.Checked
                .OBSTETRI_PERAWAT_ADD_12 = False
                .OBSTETRI_PERAWAT_ADD_13 = CheckBox404.Checked
                .OBSTETRI_PERAWAT_ADD_14 = CheckBox408.Checked
                .OBSTETRI_PERAWAT_ADD_15 = CheckBox412.Checked
                .OBSTETRI_PERAWAT_ADD_16 = CheckBox416.Checked
                .OBSTETRI_PERAWAT_ADD_17 = False
                .OBSTETRI_PERAWAT_ADD_18 = CheckBox403.Checked
                .OBSTETRI_PERAWAT_ADD_19 = CheckBox407.Checked
                .OBSTETRI_PERAWAT_ADD_20 = CheckBox411.Checked
                .OBSTETRI_PERAWAT_ADD_21 = CheckBox414.Checked
                .OBSTETRI_PERAWAT_ADD_22 = False
                .OBSTETRI_PERAWAT_ADD_23 = CheckBox400.Checked
                .OBSTETRI_PERAWAT_ADD_24 = CheckBox406.Checked
                .OBSTETRI_PERAWAT_ADD_25 = CheckBox410.Checked
                .OBSTETRI_PERAWAT_ADD_26 = CheckBox415.Checked
                .OBSTETRI_PERAWAT_ADD_27 = CheckBox418.Checked
                .OBSTETRI_PERAWAT_ADD_28 = CheckBox419.Checked
                .OBSTETRI_PERAWAT_ADD_29 = CheckBox420.Checked
                .OBSTETRI_PERAWAT_ADD_30 = CheckBox422.Checked
                .OBSTETRI_PERAWAT_ADD_31 = CheckBox423.Checked
                .OBSTETRI_PERAWAT_ADD_32 = CheckBox424.Checked
                .OBSTETRI_PERAWAT_ADD_33 = CheckBox425.Checked
                .OBSTETRI_PERAWAT_ADD_34 = CheckBox426.Checked
                .OBSTETRI_PERAWAT_ADD_35 = CheckBox427.Checked
                .OBSTETRI_PERAWAT_ADD_36 = CheckBox428.Checked
                .OBSTETRI_PERAWAT_ADD_37 = CheckBox430.Checked
                .OBSTETRI_PERAWAT_ADD_38 = CheckBox429.Checked
                .OBSTETRI_PERAWAT_ADD_39 = CheckBox431.Checked
                .OBSTETRI_PERAWAT_ADD_40 = CheckBox432.Checked
                .OBSTETRI_PERAWAT_ADD_41 = CheckBox439.Checked
                .OBSTETRI_PERAWAT_ADD_42 = CheckBox433.Checked
                .OBSTETRI_PERAWAT_ADD_43 = CheckBox434.Checked
                .OBSTETRI_PERAWAT_ADD_44 = CheckBox436.Checked
                .OBSTETRI_PERAWAT_ADD_45 = CheckBox435.Checked
                .OBSTETRI_PERAWAT_ADD_46 = CheckBox438.Checked
                .OBSTETRI_PERAWAT_RESIKOJATUH_1 = CheckEdit1.Checked
                .OBSTETRI_PERAWAT_RESIKOJATUH_2 = CheckEdit2.Checked
                .OBSTETRI_PERAWAT_RESIKOJATUH_3 = CheckEdit3.Checked
                .OBSTETRI_PERAWAT_RESIKOJATUH_4 = CheckEdit4.Checked
                .OBSTETRI_PERAWAT_RESIKOJATUH_5 = CheckEdit5.Checked
                .OBSTETRI_PERAWAT_RESIKOJATUH_6 = CheckEdit6.Checked
                .OBSTETRI_PERAWAT_RESIKOJATUH_7 = CheckEdit7.Checked
                .OBSTETRI_PERAWAT_RESIKOJATUH_8 = CheckEdit8.Checked
                .OBSTETRI_PERAWAT_RESIKOJATUH_9 = CheckEdit9.Checked
                .OBSTETRI_PERAWAT_RESIKOJATUH_10 = CheckEdit10.Checked
                .OBSTETRI_PERAWAT_RESIKOJATUH_11 = CheckEdit11.Checked
                .OBSTETRI_PERAWAT_RESIKOJATUH_12 = CheckEdit12.Checked
                .OBSTETRI_PERAWAT_RESIKOJATUH_13 = CheckEdit13.Checked
                .OBSTETRI_PERAWAT_RESIKOJATUH_14 = CheckEdit14.Checked
                .OBSTETRI_PERAWAT_RESIKOJATUH_15 = CheckEdit15.Checked
                .OBSTETRI_PERAWAT_RESIKOJATUH_16 = CheckEdit16.Checked
                .OBSTETRI_PERAWAT_RESIKOJATUH_17 = CheckEdit17.Checked
                .OBSTETRI_PERAWAT_RESIKOJATUH_18 = CheckEdit18.Checked
                .OBSTETRI_PERAWAT_RESIKOJATUH_19 = CheckEdit19.Checked
                .OBSTETRI_PERAWAT_RESIKOJATUH_20 = CheckEdit20.Checked
                .OBSTETRI_PERAWAT_RESIKOJATUH_21 = CheckEdit21.Checked
                .OBSTETRI_PERAWAT_RESIKOJATUH_22 = CheckEdit22.Checked
                .OBSTETRI_PERAWAT_RESIKOJATUH_23 = CheckEdit23.Checked
                .OBSTETRI_PERAWAT_RESIKOJATUH_24 = CheckEdit24.Checked
                .OBSTETRI_PERAWAT_RESIKOJATUH_25 = CheckEdit25.Checked
                .OBSTETRI_PERAWAT_RESIKOJATUH_26 = CheckEdit26.Checked
                .OBSTETRI_PERAWAT_RESIKOJATUH_27 = CheckEdit27.Checked
                .OBSTETRI_PERAWAT_RESIKOJATUH_28 = CheckEdit28.Checked
                .OBSTETRI_PERAWAT_RESIKOJATUH_29 = False
                .OBSTETRI_PERAWAT_RESIKOJATUH_30 = False
                .OBSTETRI_PERAWAT_RESIKOJATUH_31 = False
                .OBSTETRI_PERAWAT_RESIKOJATUH_32 = False
                .OBSTETRI_PERAWAT_RESIKOJATUH_33 = False
                .OBSTETRI_PERAWAT_RESIKOJATUH_34 = False
                .OBSTETRI_PERAWAT_RESIKOJATUH_35 = False
                .OBSTETRI_PERAWAT_RESIKOJATUH_36 = False
                .OBSTETRI_PERAWAT_RESIKOJATUH_37 = False
                .OBSTETRI_PERAWAT_RESIKOJATUH_38 = False
                .OBSTETRI_PERAWAT_RESIKOJATUH_39 = False
                .OBSTETRI_PERAWAT_RESIKOJATUH_40 = False
                .OBSTETRI_PERAWAT_RESIKOJATUH_41 = False
                .OBSTETRI_PERAWAT_RESIKOJATUH_42 = False
                .OBSTETRI_PERAWAT_RESIKOJATUH_43 = False
                .OBSTETRI_PERAWAT_RESIKOJATUH_44 = False
                .OBSTETRI_PERAWAT_RESIKOJATUH_45 = False
                .OBSTETRI_PERAWAT_RESIKOJATUH_46 = False
                .OBSTETRI_PERAWAT_RESIKOJATUH_47 = False
                .OBSTETRI_PERAWAT_RESIKOJATUH_48 = False
                .OBSTETRI_PERAWAT_RESIKOJATUH_49 = False
                .OBSTETRI_PERAWAT_RESIKOJATUH_50 = False
                .OBSTETRI_PERAWAT_RESIKOJATUH_51 = False
                .OBSTETRI_PERAWAT_RESIKOJATUH_52 = False
                .OBSTETRI_PERAWAT_RESIKOJATUH_53 = False
                .OBSTETRI_PERAWAT_RESIKOJATUH_54 = False
                .OBSTETRI_PERAWAT_RESIKOJATUH_55 = False
                .OBSTETRI_PERAWAT_RESIKOJATUH_56 = False

                .NAMAKELUARGAPASIEN = String.Empty
                .KODEKELUARGAPASIEN = String.Empty
                Try
                    .CETAK = oS_DIGITAL_ASKEP_PEDIATRIK_03.GetData(txtAlamat.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try
                .KDUSER = sUserID
                .KDUSER_SIGNATURE = sUserSIGNATURE
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_ASKEP_PEDIATRIK_03.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_ASKEP_PEDIATRIK_03.UpdateData(ds)
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
        If MsgBox("Save " & txtKDKUNJUNGAN.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtKDKUNJUNGAN.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
     Private Sub frmEMedrek_OBSTETRI_PERAWAT_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
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
#End Region
End Class