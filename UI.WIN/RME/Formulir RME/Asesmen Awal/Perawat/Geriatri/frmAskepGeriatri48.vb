Imports System.Data.SqlClient
Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports QRCoder

Public Class frmAskepGeriatri48
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_ASKEPGERIATRI_4_8 As New Digital.clsDigital_AskepGeriatri_4_8
    Private sKDPENDAFTARAN As String
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

            Try
                Dim gen As New QRCodeGenerator
                Dim data = gen.CreateQrCode(sTandaTanganPasien, QRCodeGenerator.ECCLevel.Q)
                Dim code As New QRCode(data)
                PictureBox1.Image = code.GetGraphic(6)
            Catch ex As Exception
                PictureBox1.Visible = False
            End Try

            sKDUSER = sUserID
            sKDUSERSIGNATURE = sUserSIGNATURE

        Else
            txtNoPasien.ResetText()
            txtNamaPasien.ResetText()
            txtUmur.ResetText()
            txtTanggalDaftar.ResetText()
            txtNoRegister.ResetText()
            txtTujuan.ResetText()
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
        fn_LoadPerawat()
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

        deDATE.ReadOnly = Status

        CheckEdit1.Properties.ReadOnly = Status
        CheckEdit2.Properties.ReadOnly = Status
        CheckEdit3.Properties.ReadOnly = Status
        CheckEdit4.Properties.ReadOnly = Status
        CheckEdit5.Properties.ReadOnly = Status
        CheckEdit6.Properties.ReadOnly = Status
        TextEdit1.Properties.ReadOnly = Status
        CheckEdit7.Properties.ReadOnly = Status
        CheckEdit8.Properties.ReadOnly = Status
        CheckEdit9.Properties.ReadOnly = Status
        CheckEdit10.Properties.ReadOnly = Status
        CheckEdit11.Properties.ReadOnly = Status
        CheckEdit12.Properties.ReadOnly = Status
        CheckEdit13.Properties.ReadOnly = Status
        TextEdit2.Properties.ReadOnly = Status
        CheckEdit14.Properties.ReadOnly = Status
        CheckEdit15.Properties.ReadOnly = Status
        CheckEdit16.Properties.ReadOnly = Status
        CheckEdit17.Properties.ReadOnly = Status
        CheckEdit18.Properties.ReadOnly = Status
        CheckEdit19.Properties.ReadOnly = Status
        CheckEdit20.Properties.ReadOnly = Status
        CheckEdit21.Properties.ReadOnly = Status
        TextEdit3.Properties.ReadOnly = Status
        grdPerawat1.Properties.ReadOnly = Status
        CheckEdit22.Properties.ReadOnly = Status
        CheckEdit23.Properties.ReadOnly = Status
        CheckEdit24.Properties.ReadOnly = Status
        CheckEdit25.Properties.ReadOnly = Status
        TextEdit4.Properties.ReadOnly = Status
        CheckEdit26.Properties.ReadOnly = Status
        CheckEdit27.Properties.ReadOnly = Status
        CheckEdit28.Properties.ReadOnly = Status
        CheckEdit29.Properties.ReadOnly = Status
        CheckEdit30.Properties.ReadOnly = Status
        CheckEdit31.Properties.ReadOnly = Status
        CheckEdit32.Properties.ReadOnly = Status
        CheckEdit33.Properties.ReadOnly = Status
        TextEdit5.Properties.ReadOnly = Status
        CheckEdit34.Properties.ReadOnly = Status
        CheckEdit35.Properties.ReadOnly = Status
        CheckEdit36.Properties.ReadOnly = Status
        CheckEdit37.Properties.ReadOnly = Status
        TextEdit6.Properties.ReadOnly = Status
        CheckEdit38.Properties.ReadOnly = Status
        CheckEdit39.Properties.ReadOnly = Status
        CheckEdit40.Properties.ReadOnly = Status
        CheckEdit41.Properties.ReadOnly = Status
        CheckEdit42.Properties.ReadOnly = Status
        CheckEdit43.Properties.ReadOnly = Status
        CheckEdit44.Properties.ReadOnly = Status
        CheckEdit45.Properties.ReadOnly = Status
        CheckEdit46.Properties.ReadOnly = Status
        TextEdit7.Properties.ReadOnly = Status
        grdPerawat3.Properties.ReadOnly = Status
        CheckEdit47.Properties.ReadOnly = Status
        CheckEdit48.Properties.ReadOnly = Status
        CheckEdit49.Properties.ReadOnly = Status
        CheckEdit50.Properties.ReadOnly = Status
        CheckEdit51.Properties.ReadOnly = Status
        TextEdit8.Properties.ReadOnly = Status
        CheckEdit52.Properties.ReadOnly = Status
        CheckEdit53.Properties.ReadOnly = Status
        CheckEdit54.Properties.ReadOnly = Status
        CheckEdit55.Properties.ReadOnly = Status
        CheckEdit56.Properties.ReadOnly = Status
        TextEdit9.Properties.ReadOnly = Status
        grdPerawat4.Properties.ReadOnly = Status
        CheckEdit57.Properties.ReadOnly = Status
        CheckEdit58.Properties.ReadOnly = Status
        CheckEdit59.Properties.ReadOnly = Status
        CheckEdit60.Properties.ReadOnly = Status
        CheckEdit61.Properties.ReadOnly = Status
        TextEdit10.Properties.ReadOnly = Status
        CheckEdit62.Properties.ReadOnly = Status
        CheckEdit63.Properties.ReadOnly = Status
        CheckEdit64.Properties.ReadOnly = Status
        CheckEdit65.Properties.ReadOnly = Status
        CheckEdit66.Properties.ReadOnly = Status
        TextEdit11.Properties.ReadOnly = Status
        grdPerawat5.Properties.ReadOnly = Status
        CheckEdit67.Properties.ReadOnly = Status
        CheckEdit68.Properties.ReadOnly = Status
        CheckEdit69.Properties.ReadOnly = Status
        CheckEdit70.Properties.ReadOnly = Status
        CheckEdit71.Properties.ReadOnly = Status
        TextEdit12.Properties.ReadOnly = Status
        CheckEdit72.Properties.ReadOnly = Status
        CheckEdit73.Properties.ReadOnly = Status
        CheckEdit74.Properties.ReadOnly = Status
        CheckEdit75.Properties.ReadOnly = Status
        CheckEdit76.Properties.ReadOnly = Status
        CheckEdit77.Properties.ReadOnly = Status
        TextEdit13.Properties.ReadOnly = Status
        grdPerawatan6.Properties.ReadOnly = Status
        CheckEdit78.Properties.ReadOnly = Status
        CheckEdit79.Properties.ReadOnly = Status
        CheckEdit80.Properties.ReadOnly = Status
        CheckEdit81.Properties.ReadOnly = Status
        CheckEdit82.Properties.ReadOnly = Status
        TextEdit14.Properties.ReadOnly = Status
        CheckEdit83.Properties.ReadOnly = Status
        CheckEdit84.Properties.ReadOnly = Status
        CheckEdit85.Properties.ReadOnly = Status
        CheckEdit86.Properties.ReadOnly = Status
        CheckEdit87.Properties.ReadOnly = Status
        CheckEdit88.Properties.ReadOnly = Status
        TextEdit15.Properties.ReadOnly = Status
        grdPerawat7.Properties.ReadOnly = Status
        CheckEdit89.Properties.ReadOnly = Status
        CheckEdit90.Properties.ReadOnly = Status
        CheckEdit91.Properties.ReadOnly = Status
        CheckEdit92.Properties.ReadOnly = Status
        CheckEdit93.Properties.ReadOnly = Status
        TextEdit16.Properties.ReadOnly = Status
        CheckEdit94.Properties.ReadOnly = Status
        CheckEdit95.Properties.ReadOnly = Status
        CheckEdit96.Properties.ReadOnly = Status
        CheckEdit97.Properties.ReadOnly = Status
        CheckEdit98.Properties.ReadOnly = Status
        CheckEdit99.Properties.ReadOnly = Status
        CheckEdit100.Properties.ReadOnly = Status
        CheckEdit101.Properties.ReadOnly = Status
        CheckEdit102.Properties.ReadOnly = Status
        TextEdit17.Properties.ReadOnly = Status
        grdPerawat8.Properties.ReadOnly = Status
        CheckEdit103.Properties.ReadOnly = Status
        CheckEdit104.Properties.ReadOnly = Status
        CheckEdit105.Properties.ReadOnly = Status
        CheckEdit106.Properties.ReadOnly = Status
        TextEdit18.Properties.ReadOnly = Status
        CheckEdit107.Properties.ReadOnly = Status
        CheckEdit108.Properties.ReadOnly = Status
        CheckEdit109.Properties.ReadOnly = Status
        CheckEdit110.Properties.ReadOnly = Status
        CheckEdit111.Properties.ReadOnly = Status
        CheckEdit112.Properties.ReadOnly = Status
        CheckEdit113.Properties.ReadOnly = Status
        TextEdit19.Properties.ReadOnly = Status
        grdPerawat9.Properties.ReadOnly = Status
        CheckEdit114.Properties.ReadOnly = Status
        CheckEdit115.Properties.ReadOnly = Status
        CheckEdit116.Properties.ReadOnly = Status
        CheckEdit117.Properties.ReadOnly = Status
        TextEdit20.Properties.ReadOnly = Status
        CheckEdit118.Properties.ReadOnly = Status
        CheckEdit119.Properties.ReadOnly = Status
        CheckEdit120.Properties.ReadOnly = Status
        CheckEdit121.Properties.ReadOnly = Status
        CheckEdit122.Properties.ReadOnly = Status
        CheckEdit123.Properties.ReadOnly = Status
        CheckEdit124.Properties.ReadOnly = Status
        CheckEdit125.Properties.ReadOnly = Status
        TextEdit21.Properties.ReadOnly = Status
        grdPerawatan10.Properties.ReadOnly = Status
        CheckEdit126.Properties.ReadOnly = Status
        CheckEdit127.Properties.ReadOnly = Status
        CheckEdit128.Properties.ReadOnly = Status
        CheckEdit129.Properties.ReadOnly = Status
        CheckEdit130.Properties.ReadOnly = Status
        TextEdit22.Properties.ReadOnly = Status
        CheckEdit131.Properties.ReadOnly = Status
        CheckEdit132.Properties.ReadOnly = Status
        CheckEdit133.Properties.ReadOnly = Status
        CheckEdit134.Properties.ReadOnly = Status
        CheckEdit135.Properties.ReadOnly = Status
        CheckEdit136.Properties.ReadOnly = Status
        CheckEdit137.Properties.ReadOnly = Status
        TextEdit23.Properties.ReadOnly = Status
        grdPerawat11.Properties.ReadOnly = Status
        CheckEdit143.Properties.ReadOnly = Status
        CheckEdit144.Properties.ReadOnly = Status
        CheckEdit145.Properties.ReadOnly = Status
        CheckEdit146.Properties.ReadOnly = Status
        CheckEdit147.Properties.ReadOnly = Status
        CheckEdit148.Properties.ReadOnly = Status
        TextEdit25.Properties.ReadOnly = Status
        CheckEdit149.Properties.ReadOnly = Status
        CheckEdit150.Properties.ReadOnly = Status
        CheckEdit151.Properties.ReadOnly = Status
        CheckEdit152.Properties.ReadOnly = Status
        CheckEdit153.Properties.ReadOnly = Status
        CheckEdit154.Properties.ReadOnly = Status
        TextEdit26.Properties.ReadOnly = Status
        grdPerawat13.Properties.ReadOnly = Status
        CheckEdit155.Properties.ReadOnly = Status
        CheckEdit156.Properties.ReadOnly = Status
        CheckEdit157.Properties.ReadOnly = Status
        CheckEdit158.Properties.ReadOnly = Status
        CheckEdit159.Properties.ReadOnly = Status
        TextEdit27.Properties.ReadOnly = Status
        CheckEdit160.Properties.ReadOnly = Status
        CheckEdit161.Properties.ReadOnly = Status
        CheckEdit162.Properties.ReadOnly = Status
        CheckEdit163.Properties.ReadOnly = Status
        CheckEdit164.Properties.ReadOnly = Status
        CheckEdit165.Properties.ReadOnly = Status
        TextEdit28.Properties.ReadOnly = Status
        grdPerawat14.Properties.ReadOnly = Status
        CheckEdit166.Properties.ReadOnly = Status
        CheckEdit167.Properties.ReadOnly = Status
        CheckEdit168.Properties.ReadOnly = Status
        CheckEdit169.Properties.ReadOnly = Status
        CheckEdit170.Properties.ReadOnly = Status
        CheckEdit171.Properties.ReadOnly = Status
        TextEdit29.Properties.ReadOnly = Status
        CheckEdit172.Properties.ReadOnly = Status
        CheckEdit173.Properties.ReadOnly = Status
        CheckEdit174.Properties.ReadOnly = Status
        CheckEdit175.Properties.ReadOnly = Status
        CheckEdit176.Properties.ReadOnly = Status
        TextEdit30.Properties.ReadOnly = Status
        grdPerawat15.Properties.ReadOnly = Status
        CheckEdit177.Properties.ReadOnly = Status
        CheckEdit178.Properties.ReadOnly = Status
        CheckEdit179.Properties.ReadOnly = Status
        CheckEdit180.Properties.ReadOnly = Status
        CheckEdit181.Properties.ReadOnly = Status
        CheckEdit182.Properties.ReadOnly = Status
        TextEdit31.Properties.ReadOnly = Status
        CheckEdit183.Properties.ReadOnly = Status
        CheckEdit184.Properties.ReadOnly = Status
        CheckEdit185.Properties.ReadOnly = Status
        CheckEdit186.Properties.ReadOnly = Status
        CheckEdit187.Properties.ReadOnly = Status
        CheckEdit188.Properties.ReadOnly = Status
        CheckEdit189.Properties.ReadOnly = Status
        CheckEdit190.Properties.ReadOnly = Status
        CheckEdit191.Properties.ReadOnly = Status
        CheckEdit192.Properties.ReadOnly = Status
        TextEdit32.Properties.ReadOnly = Status
        grdPerawat16.Properties.ReadOnly = Status
        CheckEdit193.Properties.ReadOnly = Status
        CheckEdit194.Properties.ReadOnly = Status
        CheckEdit195.Properties.ReadOnly = Status
        CheckEdit196.Properties.ReadOnly = Status
        CheckEdit197.Properties.ReadOnly = Status
        TextEdit33.Properties.ReadOnly = Status
        CheckEdit198.Properties.ReadOnly = Status
        CheckEdit199.Properties.ReadOnly = Status
        CheckEdit200.Properties.ReadOnly = Status
        CheckEdit201.Properties.ReadOnly = Status
        CheckEdit202.Properties.ReadOnly = Status
        TextEdit34.Properties.ReadOnly = Status
        CheckEdit203.Properties.ReadOnly = Status
        CheckEdit204.Properties.ReadOnly = Status
        CheckEdit205.Properties.ReadOnly = Status
        CheckEdit206.Properties.ReadOnly = Status
        CheckEdit207.Properties.ReadOnly = Status
        CheckEdit208.Properties.ReadOnly = Status
        TextEdit35.Properties.ReadOnly = Status
        CheckEdit209.Properties.ReadOnly = Status
        CheckEdit210.Properties.ReadOnly = Status
        CheckEdit211.Properties.ReadOnly = Status
        CheckEdit212.Properties.ReadOnly = Status
        CheckEdit213.Properties.ReadOnly = Status
        CheckEdit214.Properties.ReadOnly = Status
        CheckEdit215.Properties.ReadOnly = Status
        CheckEdit216.Properties.ReadOnly = Status
        CheckEdit217.Properties.ReadOnly = Status
        CheckEdit218.Properties.ReadOnly = Status
        CheckEdit219.Properties.ReadOnly = Status
        TextEdit36.Properties.ReadOnly = Status
        grdPerawat18.Properties.ReadOnly = Status
        CheckEdit220.Properties.ReadOnly = Status
        CheckEdit221.Properties.ReadOnly = Status
        CheckEdit222.Properties.ReadOnly = Status
        CheckEdit223.Properties.ReadOnly = Status
        CheckEdit224.Properties.ReadOnly = Status
        CheckEdit225.Properties.ReadOnly = Status
        CheckEdit226.Properties.ReadOnly = Status
        CheckEdit227.Properties.ReadOnly = Status
        CheckEdit228.Properties.ReadOnly = Status
        TextEdit37.Properties.ReadOnly = Status
        CheckEdit229.Properties.ReadOnly = Status
        CheckEdit230.Properties.ReadOnly = Status
        CheckEdit231.Properties.ReadOnly = Status
        CheckEdit232.Properties.ReadOnly = Status
        CheckEdit233.Properties.ReadOnly = Status
        CheckEdit234.Properties.ReadOnly = Status
        CheckEdit235.Properties.ReadOnly = Status
        CheckEdit236.Properties.ReadOnly = Status
        CheckEdit237.Properties.ReadOnly = Status
        CheckEdit238.Properties.ReadOnly = Status
        CheckEdit239.Properties.ReadOnly = Status
        TextEdit38.Properties.ReadOnly = Status
        grdPerawat19.Properties.ReadOnly = Status
        CheckEdit240.Properties.ReadOnly = Status
        TextEdit39.Properties.ReadOnly = Status
        MemoEdit1.Properties.ReadOnly = Status
        MemoEdit2.Properties.ReadOnly = Status
        grdPerawat20.Properties.ReadOnly = Status
        CheckEdit246.Properties.ReadOnly = Status
        CheckEdit247.Properties.ReadOnly = Status
        CheckEdit248.Properties.ReadOnly = Status
        CheckEdit249.Properties.ReadOnly = Status
        MemoEdit3.Properties.ReadOnly = Status
        grdPerawat21.Properties.ReadOnly = Status
        CheckEdit250.Properties.ReadOnly = Status
        TextEdit41.Properties.ReadOnly = Status
        CheckEdit251.Properties.ReadOnly = Status
        CheckEdit252.Properties.ReadOnly = Status
        CheckEdit253.Properties.ReadOnly = Status
        CheckEdit254.Properties.ReadOnly = Status
        CheckEdit255.Properties.ReadOnly = Status
        CheckEdit256.Properties.ReadOnly = Status
        CheckEdit257.Properties.ReadOnly = Status
        CheckEdit258.Properties.ReadOnly = Status
        CheckEdit259.Properties.ReadOnly = Status
        CheckEdit260.Properties.ReadOnly = Status
        MemoEdit4.Properties.ReadOnly = Status
        grdPerawat22.Properties.ReadOnly = Status
        grdPerawat23.Properties.ReadOnly = Status
        'PictureBox1.Properties.ReadOnly = Status
        'tambahan
        grdPerawat2.Properties.ReadOnly = Status
		CheckEdit138.Properties.ReadOnly = Status
		CheckEdit139.Properties.ReadOnly = Status
		CheckEdit140.Properties.ReadOnly = Status
		CheckEdit141.Properties.ReadOnly = Status
		CheckEdit142.Properties.ReadOnly = Status
		TextEdit24.Properties.ReadOnly = Status
		CheckEdit241.Properties.ReadOnly = Status
		CheckEdit242.Properties.ReadOnly = Status
		CheckEdit243.Properties.ReadOnly = Status
		CheckEdit244.Properties.ReadOnly = Status
		CheckEdit245.Properties.ReadOnly = Status
		TextEdit40.Properties.ReadOnly = Status
		grdPerawat12.Properties.ReadOnly = Status
		grdPerawat17.Properties.ReadOnly = Status


    End Sub
    Private Sub fn_EmptyMe()
        deDATE.DateTime = Now

        CheckEdit1.Checked = False
        CheckEdit2.Checked = False
        CheckEdit3.Checked = False
        CheckEdit4.Checked = False
        CheckEdit5.Checked = False
        CheckEdit6.Checked = False
        TextEdit1.ResetText()
        CheckEdit7.Checked = False
        CheckEdit8.Checked = False
        CheckEdit9.Checked = False
        CheckEdit10.Checked = False
        CheckEdit11.Checked = False
        CheckEdit12.Checked = False
        CheckEdit13.Checked = False
        TextEdit2.ResetText()
        CheckEdit14.Checked = False
        CheckEdit15.Checked = False
        CheckEdit16.Checked = False
        CheckEdit17.Checked = False
        CheckEdit18.Checked = False
        CheckEdit19.Checked = False
        CheckEdit20.Checked = False
        CheckEdit21.Checked = False
        TextEdit3.ResetText()
        grdPerawat1.ResetText()
        CheckEdit22.Checked = False
        CheckEdit23.Checked = False
        CheckEdit24.Checked = False
        CheckEdit25.Checked = False
        TextEdit4.ResetText()
        CheckEdit26.Checked = False
        CheckEdit27.Checked = False
        CheckEdit28.Checked = False
        CheckEdit29.Checked = False
        CheckEdit30.Checked = False
        CheckEdit31.Checked = False
        CheckEdit32.Checked = False
        CheckEdit33.Checked = False
        TextEdit5.ResetText()
        CheckEdit34.Checked = False
        CheckEdit35.Checked = False
        CheckEdit36.Checked = False
        CheckEdit37.Checked = False
        TextEdit6.ResetText()
        CheckEdit38.Checked = False
        CheckEdit39.Checked = False
        CheckEdit40.Checked = False
        CheckEdit41.Checked = False
        CheckEdit42.Checked = False
        CheckEdit43.Checked = False
        CheckEdit44.Checked = False
        CheckEdit45.Checked = False
        CheckEdit46.Checked = False
        TextEdit7.ResetText()
        grdPerawat3.ResetText()
        CheckEdit47.Checked = False
        CheckEdit48.Checked = False
        CheckEdit49.Checked = False
        CheckEdit50.Checked = False
        CheckEdit51.Checked = False
        TextEdit8.ResetText()
        CheckEdit52.Checked = False
        CheckEdit53.Checked = False
        CheckEdit54.Checked = False
        CheckEdit55.Checked = False
        CheckEdit56.Checked = False
        TextEdit9.ResetText()
        grdPerawat4.ResetText()
        CheckEdit57.Checked = False
        CheckEdit58.Checked = False
        CheckEdit59.Checked = False
        CheckEdit60.Checked = False
        CheckEdit61.Checked = False
        TextEdit10.ResetText()
        CheckEdit62.Checked = False
        CheckEdit63.Checked = False
        CheckEdit64.Checked = False
        CheckEdit65.Checked = False
        CheckEdit66.Checked = False
        TextEdit11.ResetText()
        grdPerawat5.ResetText()
        CheckEdit67.Checked = False
        CheckEdit68.Checked = False
        CheckEdit69.Checked = False
        CheckEdit70.Checked = False
        CheckEdit71.Checked = False
        TextEdit12.ResetText()
        CheckEdit72.Checked = False
        CheckEdit73.Checked = False
        CheckEdit74.Checked = False
        CheckEdit75.Checked = False
        CheckEdit76.Checked = False
        CheckEdit77.Checked = False
        TextEdit13.ResetText()
        grdPerawatan6.ResetText()
        CheckEdit78.Checked = False
        CheckEdit79.Checked = False
        CheckEdit80.Checked = False
        CheckEdit81.Checked = False
        CheckEdit82.Checked = False
        TextEdit14.ResetText()
        CheckEdit83.Checked = False
        CheckEdit84.Checked = False
        CheckEdit85.Checked = False
        CheckEdit86.Checked = False
        CheckEdit87.Checked = False
        CheckEdit88.Checked = False
        TextEdit15.ResetText()
        grdPerawat7.ResetText()
        CheckEdit89.Checked = False
        CheckEdit90.Checked = False
        CheckEdit91.Checked = False
        CheckEdit92.Checked = False
        CheckEdit93.Checked = False
        TextEdit16.ResetText()
        CheckEdit94.Checked = False
        CheckEdit95.Checked = False
        CheckEdit96.Checked = False
        CheckEdit97.Checked = False
        CheckEdit98.Checked = False
        CheckEdit99.Checked = False
        CheckEdit100.Checked = False
        CheckEdit101.Checked = False
        CheckEdit102.Checked = False
        TextEdit17.ResetText()
        grdPerawat8.ResetText()
        CheckEdit103.Checked = False
        CheckEdit104.Checked = False
        CheckEdit105.Checked = False
        CheckEdit106.Checked = False
        TextEdit18.ResetText()
        CheckEdit107.Checked = False
        CheckEdit108.Checked = False
        CheckEdit109.Checked = False
        CheckEdit110.Checked = False
        CheckEdit111.Checked = False
        CheckEdit112.Checked = False
        CheckEdit113.Checked = False
        TextEdit19.ResetText()
        grdPerawat9.ResetText()
        CheckEdit114.Checked = False
        CheckEdit115.Checked = False
        CheckEdit116.Checked = False
        CheckEdit117.Checked = False
        TextEdit20.ResetText()
        CheckEdit118.Checked = False
        CheckEdit119.Checked = False
        CheckEdit120.Checked = False
        CheckEdit121.Checked = False
        CheckEdit122.Checked = False
        CheckEdit123.Checked = False
        CheckEdit124.Checked = False
        CheckEdit125.Checked = False
        TextEdit21.ResetText()
        grdPerawatan10.ResetText()
        CheckEdit126.Checked = False
        CheckEdit127.Checked = False
        CheckEdit128.Checked = False
        CheckEdit129.Checked = False
        CheckEdit130.Checked = False
        TextEdit22.ResetText()
        CheckEdit131.Checked = False
        CheckEdit132.Checked = False
        CheckEdit133.Checked = False
        CheckEdit134.Checked = False
        CheckEdit135.Checked = False
        CheckEdit136.Checked = False
        CheckEdit137.Checked = False
        TextEdit23.ResetText()
        grdPerawat11.ResetText()
        CheckEdit143.Checked = False
        CheckEdit144.Checked = False
        CheckEdit145.Checked = False
        CheckEdit146.Checked = False
        CheckEdit147.Checked = False
        CheckEdit148.Checked = False
        TextEdit25.ResetText()
        CheckEdit149.Checked = False
        CheckEdit150.Checked = False
        CheckEdit151.Checked = False
        CheckEdit152.Checked = False
        CheckEdit153.Checked = False
        CheckEdit154.Checked = False
        TextEdit26.ResetText()
        grdPerawat13.ResetText()
        CheckEdit155.Checked = False
        CheckEdit156.Checked = False
        CheckEdit157.Checked = False
        CheckEdit158.Checked = False
        CheckEdit159.Checked = False
        TextEdit27.ResetText()
        CheckEdit160.Checked = False
        CheckEdit161.Checked = False
        CheckEdit162.Checked = False
        CheckEdit163.Checked = False
        CheckEdit164.Checked = False
        CheckEdit165.Checked = False
        TextEdit28.ResetText()
        grdPerawat14.ResetText()
        CheckEdit166.Checked = False
        CheckEdit167.Checked = False
        CheckEdit168.Checked = False
        CheckEdit169.Checked = False
        CheckEdit170.Checked = False
        CheckEdit171.Checked = False
        TextEdit29.ResetText()
        CheckEdit172.Checked = False
        CheckEdit173.Checked = False
        CheckEdit174.Checked = False
        CheckEdit175.Checked = False
        CheckEdit176.Checked = False
        TextEdit30.ResetText()
        grdPerawat15.ResetText()
        CheckEdit177.Checked = False
        CheckEdit178.Checked = False
        CheckEdit179.Checked = False
        CheckEdit180.Checked = False
        CheckEdit181.Checked = False
        CheckEdit182.Checked = False
        TextEdit31.ResetText()
        CheckEdit183.Checked = False
        CheckEdit184.Checked = False
        CheckEdit185.Checked = False
        CheckEdit186.Checked = False
        CheckEdit187.Checked = False
        CheckEdit188.Checked = False
        CheckEdit189.Checked = False
        CheckEdit190.Checked = False
        CheckEdit191.Checked = False
        CheckEdit192.Checked = False
        TextEdit32.ResetText()
        grdPerawat16.ResetText()
        CheckEdit193.Checked = False
        CheckEdit194.Checked = False
        CheckEdit195.Checked = False
        CheckEdit196.Checked = False
        CheckEdit197.Checked = False
        TextEdit33.ResetText()
        CheckEdit198.Checked = False
        CheckEdit199.Checked = False
        CheckEdit200.Checked = False
        CheckEdit201.Checked = False
        CheckEdit202.Checked = False
        TextEdit34.ResetText()
        CheckEdit203.Checked = False
        CheckEdit204.Checked = False
        CheckEdit205.Checked = False
        CheckEdit206.Checked = False
        CheckEdit207.Checked = False
        CheckEdit208.Checked = False
        TextEdit35.ResetText()
        CheckEdit209.Checked = False
        CheckEdit210.Checked = False
        CheckEdit211.Checked = False
        CheckEdit212.Checked = False
        CheckEdit213.Checked = False
        CheckEdit214.Checked = False
        CheckEdit215.Checked = False
        CheckEdit216.Checked = False
        CheckEdit217.Checked = False
        CheckEdit218.Checked = False
        CheckEdit219.Checked = False
        TextEdit36.ResetText()
        grdPerawat18.ResetText()
        CheckEdit220.Checked = False
        CheckEdit221.Checked = False
        CheckEdit222.Checked = False
        CheckEdit223.Checked = False
        CheckEdit224.Checked = False
        CheckEdit225.Checked = False
        CheckEdit226.Checked = False
        CheckEdit227.Checked = False
        CheckEdit228.Checked = False
        TextEdit37.ResetText()
        CheckEdit229.Checked = False
        CheckEdit230.Checked = False
        CheckEdit231.Checked = False
        CheckEdit232.Checked = False
        CheckEdit233.Checked = False
        CheckEdit234.Checked = False
        CheckEdit235.Checked = False
        CheckEdit236.Checked = False
        CheckEdit237.Checked = False
        CheckEdit238.Checked = False
        CheckEdit239.Checked = False
        TextEdit38.ResetText()
        grdPerawat19.ResetText()
        CheckEdit240.Checked = False
        TextEdit39.ResetText()
        MemoEdit1.ResetText()
        MemoEdit2.ResetText()
        grdPerawat20.ResetText()
        CheckEdit246.Checked = False
        CheckEdit247.Checked = False
        CheckEdit248.Checked = False
        CheckEdit249.Checked = False
        MemoEdit3.ResetText()
        grdPerawat21.ResetText()
        CheckEdit250.Checked = False
        TextEdit41.ResetText()
        CheckEdit251.Checked = False
        CheckEdit252.Checked = False
        CheckEdit253.Checked = False
        CheckEdit254.Checked = False
        CheckEdit255.Checked = False
        CheckEdit256.Checked = False
        CheckEdit257.Checked = False
        CheckEdit258.Checked = False
        CheckEdit259.Checked = False
        CheckEdit260.Checked = False
        MemoEdit4.ResetText()
        grdPerawat22.ResetText()
        grdPerawat23.ResetText()
        'PictureBox1.ResetText()
        'tambahan
		grdPerawat2.ResetText()
		CheckEdit138.Checked = False
		CheckEdit139.Checked = False
		CheckEdit140.Checked = False
		CheckEdit141.Checked = False
		CheckEdit142.Checked = False
		TextEdit24.ResetText()
		CheckEdit241.Checked = False
		CheckEdit242.Checked = False
		CheckEdit243.Checked = False
		CheckEdit244.Checked = False
		CheckEdit245.Checked = False
		TextEdit40.ResetText()
		grdPerawat12.ResetText()
		grdPerawat17.ResetText()
        
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_ASKEPGERIATRI_4_8.GetData(txtNoRegister.Text)
            With ds

                deDATE.DateTime = .DATE

                CheckEdit1.Checked = .ASKEP_1
                CheckEdit2.Checked = .ASKEP_2
                CheckEdit3.Checked = .ASKEP_3
                CheckEdit4.Checked = .ASKEP_4
                CheckEdit5.Checked = .ASKEP_5
                CheckEdit6.Checked = .ASKEP_6
                TextEdit1.Text = .ASKEP_7
                CheckEdit7.Checked = .ASKEP_8
                CheckEdit8.Checked = .ASKEP_9
                CheckEdit9.Checked = .ASKEP_10
                CheckEdit10.Checked = .ASKEP_11
                CheckEdit11.Checked = .ASKEP_12
                CheckEdit12.Checked = .ASKEP_13
                CheckEdit13.Checked = .ASKEP_14
                TextEdit2.Text = .ASKEP_15
                CheckEdit14.Checked = .ASKEP_16
                CheckEdit15.Checked = .ASKEP_17
                CheckEdit16.Checked = .ASKEP_18
                CheckEdit17.Checked = .ASKEP_19
                CheckEdit18.Checked = .ASKEP_20
                CheckEdit19.Checked = .ASKEP_21
                CheckEdit20.Checked = .ASKEP_22
                CheckEdit21.Checked = .ASKEP_23
                TextEdit3.Text = .ASKEP_24
                grdPerawat1.Text = .ASKEP_25
                CheckEdit22.Checked = .ASKEP_26
                CheckEdit23.Checked = .ASKEP_27
                CheckEdit24.Checked = .ASKEP_28
                CheckEdit25.Checked = .ASKEP_29
                TextEdit4.Text = .ASKEP_30
                CheckEdit26.Checked = .ASKEP_31
                CheckEdit27.Checked = .ASKEP_32
                CheckEdit28.Checked = .ASKEP_33
                CheckEdit29.Checked = .ASKEP_34
                CheckEdit30.Checked = .ASKEP_35
                CheckEdit31.Checked = .ASKEP_36
                CheckEdit32.Checked = .ASKEP_37
                CheckEdit33.Checked = .ASKEP_38
                TextEdit5.Text = .ASKEP_39
                CheckEdit34.Checked = .ASKEP_40
                CheckEdit35.Checked = .ASKEP_41
                CheckEdit36.Checked = .ASKEP_42
                CheckEdit37.Checked = .ASKEP_43
                TextEdit6.Text = .ASKEP_44
                CheckEdit38.Checked = .ASKEP_45
                CheckEdit39.Checked = .ASKEP_46
                CheckEdit40.Checked = .ASKEP_47
                CheckEdit41.Checked = .ASKEP_48
                CheckEdit42.Checked = .ASKEP_49
                CheckEdit43.Checked = .ASKEP_50
                CheckEdit44.Checked = .ASKEP_51
                CheckEdit45.Checked = .ASKEP_52
                CheckEdit46.Checked = .ASKEP_53
                TextEdit7.Text = .ASKEP_54
                grdPerawat3.Text = .ASKEP_55
                CheckEdit47.Checked = .ASKEP_56
                CheckEdit48.Checked = .ASKEP_57
                CheckEdit49.Checked = .ASKEP_58
                CheckEdit50.Checked = .ASKEP_59
                CheckEdit51.Checked = .ASKEP_60
                TextEdit8.Text = .ASKEP_61
                CheckEdit52.Checked = .ASKEP_62
                CheckEdit53.Checked = .ASKEP_63
                CheckEdit54.Checked = .ASKEP_64
                CheckEdit55.Checked = .ASKEP_65
                CheckEdit56.Checked = .ASKEP_66
                TextEdit9.Text = .ASKEP_67
                grdPerawat4.Text = .ASKEP_68
                CheckEdit57.Checked = .ASKEP_69
                CheckEdit58.Checked = .ASKEP_70
                CheckEdit59.Checked = .ASKEP_71
                CheckEdit60.Checked = .ASKEP_72
                CheckEdit61.Checked = .ASKEP_73
                TextEdit10.Text = .ASKEP_74
                CheckEdit62.Checked = .ASKEP_75
                CheckEdit63.Checked = .ASKEP_76
                CheckEdit64.Checked = .ASKEP_77
                CheckEdit65.Checked = .ASKEP_78
                CheckEdit66.Checked = .ASKEP_79
                TextEdit11.Text = .ASKEP_80
                grdPerawat5.Text = .ASKEP_81
                CheckEdit67.Checked = .ASKEP_82
                CheckEdit68.Checked = .ASKEP_83
                CheckEdit69.Checked = .ASKEP_84
                CheckEdit70.Checked = .ASKEP_85
                CheckEdit71.Checked = .ASKEP_86
                TextEdit12.Text = .ASKEP_87
                CheckEdit72.Checked = .ASKEP_88
                CheckEdit73.Checked = .ASKEP_89
                CheckEdit74.Checked = .ASKEP_90
                CheckEdit75.Checked = .ASKEP_91
                CheckEdit76.Checked = .ASKEP_92
                CheckEdit77.Checked = .ASKEP_93
                TextEdit13.Text = .ASKEP_94
                grdPerawatan6.Text = .ASKEP_95
                CheckEdit78.Checked = .ASKEP_96
                CheckEdit79.Checked = .ASKEP_97
                CheckEdit80.Checked = .ASKEP_98
                CheckEdit81.Checked = .ASKEP_99
                CheckEdit82.Checked = .ASKEP_100
                TextEdit14.Text = .ASKEP_101
                CheckEdit83.Checked = .ASKEP_102
                CheckEdit84.Checked = .ASKEP_103
                CheckEdit85.Checked = .ASKEP_104
                CheckEdit86.Checked = .ASKEP_105
                CheckEdit87.Checked = .ASKEP_106
                CheckEdit88.Checked = .ASKEP_107
                TextEdit15.Text = .ASKEP_108
                grdPerawat7.Text = .ASKEP_109
                CheckEdit89.Checked = .ASKEP_110
                CheckEdit90.Checked = .ASKEP_111
                CheckEdit91.Checked = .ASKEP_112
                CheckEdit92.Checked = .ASKEP_113
                CheckEdit93.Checked = .ASKEP_114
                TextEdit16.Text = .ASKEP_115
                CheckEdit94.Checked = .ASKEP_116
                CheckEdit95.Checked = .ASKEP_117
                CheckEdit96.Checked = .ASKEP_118
                CheckEdit97.Checked = .ASKEP_119
                CheckEdit98.Checked = .ASKEP_120
                CheckEdit99.Checked = .ASKEP_121
                CheckEdit100.Checked = .ASKEP_122
                CheckEdit101.Checked = .ASKEP_123
                CheckEdit102.Checked = .ASKEP_124
                TextEdit17.Text = .ASKEP_125
                grdPerawat8.Text = .ASKEP_126
                CheckEdit103.Checked = .ASKEP_127
                CheckEdit104.Checked = .ASKEP_128
                CheckEdit105.Checked = .ASKEP_129
                CheckEdit106.Checked = .ASKEP_130
                TextEdit18.Text = .ASKEP_131
                CheckEdit107.Checked = .ASKEP_132
                CheckEdit108.Checked = .ASKEP_133
                CheckEdit109.Checked = .ASKEP_134
                CheckEdit110.Checked = .ASKEP_135
                CheckEdit111.Checked = .ASKEP_136
                CheckEdit112.Checked = .ASKEP_137
                CheckEdit113.Checked = .ASKEP_138
                TextEdit19.Text = .ASKEP_139
                grdPerawat9.Text = .ASKEP_140
                CheckEdit114.Checked = .ASKEP_141
                CheckEdit115.Checked = .ASKEP_142
                CheckEdit116.Checked = .ASKEP_143
                CheckEdit117.Checked = .ASKEP_144
                TextEdit20.Text = .ASKEP_145
                CheckEdit118.Checked = .ASKEP_146
                CheckEdit119.Checked = .ASKEP_147
                CheckEdit120.Checked = .ASKEP_148
                CheckEdit121.Checked = .ASKEP_149
                CheckEdit122.Checked = .ASKEP_150
                CheckEdit123.Checked = .ASKEP_151
                CheckEdit124.Checked = .ASKEP_152
                CheckEdit125.Checked = .ASKEP_153
                TextEdit21.Text = .ASKEP_154
                grdPerawatan10.Text = .ASKEP_155
                CheckEdit126.Checked = .ASKEP_156
                CheckEdit127.Checked = .ASKEP_157
                CheckEdit128.Checked = .ASKEP_158
                CheckEdit129.Checked = .ASKEP_159
                CheckEdit130.Checked = .ASKEP_160
                TextEdit22.Text = .ASKEP_161
                CheckEdit131.Checked = .ASKEP_162
                CheckEdit132.Checked = .ASKEP_163
                CheckEdit133.Checked = .ASKEP_164
                CheckEdit134.Checked = .ASKEP_165
                CheckEdit135.Checked = .ASKEP_166
                CheckEdit136.Checked = .ASKEP_167
                CheckEdit137.Checked = .ASKEP_168
                TextEdit23.Text = .ASKEP_169
                grdPerawat11.Text = .ASKEP_170
                CheckEdit143.Checked = .ASKEP_171
                CheckEdit144.Checked = .ASKEP_172
                CheckEdit145.Checked = .ASKEP_173
                CheckEdit146.Checked = .ASKEP_174
                CheckEdit147.Checked = .ASKEP_175
                CheckEdit148.Checked = .ASKEP_176
                TextEdit25.Text = .ASKEP_177
                CheckEdit149.Checked = .ASKEP_178
                CheckEdit150.Checked = .ASKEP_179
                CheckEdit151.Checked = .ASKEP_180
                CheckEdit152.Checked = .ASKEP_181
                CheckEdit153.Checked = .ASKEP_182
                CheckEdit154.Checked = .ASKEP_183
                TextEdit26.Text = .ASKEP_184
                grdPerawat13.Text = .ASKEP_185
                CheckEdit155.Checked = .ASKEP_186
                CheckEdit156.Checked = .ASKEP_187
                CheckEdit157.Checked = .ASKEP_188
                CheckEdit158.Checked = .ASKEP_189
                CheckEdit159.Checked = .ASKEP_190
                TextEdit27.Text = .ASKEP_191
                CheckEdit160.Checked = .ASKEP_192
                CheckEdit161.Checked = .ASKEP_193
                CheckEdit162.Checked = .ASKEP_194
                CheckEdit163.Checked = .ASKEP_195
                CheckEdit164.Checked = .ASKEP_196
                CheckEdit165.Checked = .ASKEP_197
                TextEdit28.Text = .ASKEP_198
                grdPerawat14.Text = .ASKEP_199
                CheckEdit166.Checked = .ASKEP_200
                CheckEdit167.Checked = .ASKEP_201
                CheckEdit168.Checked = .ASKEP_202
                CheckEdit169.Checked = .ASKEP_203
                CheckEdit170.Checked = .ASKEP_204
                CheckEdit171.Checked = .ASKEP_205
                TextEdit29.Text = .ASKEP_206
                CheckEdit172.Checked = .ASKEP_207
                CheckEdit173.Checked = .ASKEP_208
                CheckEdit174.Checked = .ASKEP_209
                CheckEdit175.Checked = .ASKEP_210
                CheckEdit176.Checked = .ASKEP_211
                TextEdit30.Text = .ASKEP_212
                grdPerawat15.Text = .ASKEP_213
                CheckEdit177.Checked = .ASKEP_214
                CheckEdit178.Checked = .ASKEP_215
                CheckEdit179.Checked = .ASKEP_216
                CheckEdit180.Checked = .ASKEP_217
                CheckEdit181.Checked = .ASKEP_218
                CheckEdit182.Checked = .ASKEP_219
                TextEdit31.Text = .ASKEP_220
                CheckEdit183.Checked = .ASKEP_221
                CheckEdit184.Checked = .ASKEP_222
                CheckEdit185.Checked = .ASKEP_223
                CheckEdit186.Checked = .ASKEP_224
                CheckEdit187.Checked = .ASKEP_225
                CheckEdit188.Checked = .ASKEP_226
                CheckEdit189.Checked = .ASKEP_227
                CheckEdit190.Checked = .ASKEP_228
                CheckEdit191.Checked = .ASKEP_229
                CheckEdit192.Checked = .ASKEP_230
                TextEdit32.Text = .ASKEP_231
                grdPerawat16.Text = .ASKEP_232
                CheckEdit193.Checked = .ASKEP_233
                CheckEdit194.Checked = .ASKEP_234
                CheckEdit195.Checked = .ASKEP_235
                CheckEdit196.Checked = .ASKEP_236
                CheckEdit197.Checked = .ASKEP_237
                TextEdit33.Text = .ASKEP_238
                CheckEdit198.Checked = .ASKEP_239
                CheckEdit199.Checked = .ASKEP_240
                CheckEdit200.Checked = .ASKEP_241
                CheckEdit201.Checked = .ASKEP_242
                CheckEdit202.Checked = .ASKEP_243
                TextEdit34.Text = .ASKEP_244
                CheckEdit203.Checked = .ASKEP_245
                CheckEdit204.Checked = .ASKEP_246
                CheckEdit205.Checked = .ASKEP_247
                CheckEdit206.Checked = .ASKEP_248
                CheckEdit207.Checked = .ASKEP_249
                CheckEdit208.Checked = .ASKEP_250
                TextEdit35.Text = .ASKEP_251
                CheckEdit209.Checked = .ASKEP_252
                CheckEdit210.Checked = .ASKEP_253
                CheckEdit211.Checked = .ASKEP_254
                CheckEdit212.Checked = .ASKEP_255
                CheckEdit213.Checked = .ASKEP_256
                CheckEdit214.Checked = .ASKEP_257
                CheckEdit215.Checked = .ASKEP_258
                CheckEdit216.Checked = .ASKEP_259
                CheckEdit217.Checked = .ASKEP_260
                CheckEdit218.Checked = .ASKEP_261
                CheckEdit219.Checked = .ASKEP_262
                TextEdit36.Text = .ASKEP_263
                grdPerawat18.Text = .ASKEP_264
                CheckEdit220.Checked = .ASKEP_265
                CheckEdit221.Checked = .ASKEP_266
                CheckEdit222.Checked = .ASKEP_267
                CheckEdit223.Checked = .ASKEP_268
                CheckEdit224.Checked = .ASKEP_269
                CheckEdit225.Checked = .ASKEP_270
                CheckEdit226.Checked = .ASKEP_271
                CheckEdit227.Checked = .ASKEP_272
                CheckEdit228.Checked = .ASKEP_273
                TextEdit37.Text = .ASKEP_274
                CheckEdit229.Checked = .ASKEP_275
                CheckEdit230.Checked = .ASKEP_276
                CheckEdit231.Checked = .ASKEP_277
                CheckEdit232.Checked = .ASKEP_278
                CheckEdit233.Checked = .ASKEP_279
                CheckEdit234.Checked = .ASKEP_280
                CheckEdit235.Checked = .ASKEP_281
                CheckEdit236.Checked = .ASKEP_282
                CheckEdit237.Checked = .ASKEP_283
                CheckEdit238.Checked = .ASKEP_284
                CheckEdit239.Checked = .ASKEP_285
                TextEdit38.Text = .ASKEP_286
                grdPerawat19.Text = .ASKEP_287
                CheckEdit240.Checked = .ASKEP_288
                TextEdit39.Text = .ASKEP_289
                MemoEdit1.Text = .ASKEP_290
                MemoEdit2.Text = .ASKEP_291
                grdPerawat20.Text = .ASKEP_292
                CheckEdit246.Checked = .ASKEP_293
                CheckEdit247.Checked = .ASKEP_294
                CheckEdit248.Checked = .ASKEP_295
                CheckEdit249.Checked = .ASKEP_296
                MemoEdit3.Text = .ASKEP_297
                grdPerawat21.Text = .ASKEP_298
                CheckEdit250.Checked = .ASKEP_299
                TextEdit41.Text = .ASKEP_300
                CheckEdit251.Checked = .ASKEP_301
                CheckEdit252.Checked = .ASKEP_302
                CheckEdit253.Checked = .ASKEP_303
                CheckEdit254.Checked = .ASKEP_304
                CheckEdit255.Checked = .ASKEP_305
                CheckEdit256.Checked = .ASKEP_306
                CheckEdit257.Checked = .ASKEP_307
                CheckEdit258.Checked = .ASKEP_308
                CheckEdit259.Checked = .ASKEP_309
                CheckEdit260.Checked = .ASKEP_310
                MemoEdit4.Text = .ASKEP_311
                grdPerawat22.Text = .ASKEP_312
                grdPerawat23.Text = .ASKEP_313
                'PictureBox1.Text = .ASKEP_314

                sKDUSER = .KDUSER
                sKDUSERSIGNATURE = .KDUSER_SIGNATURE

                Try
                    PictureBox1.Image = ByteArrayToImage(.ASKEP_314.ToArray())
                Catch oErr As Exception
                End Try

                'tambahan
				grdPerawat2.Text = .ASKEP_315
				CheckEdit138.Checked = .ASKEP_316
				CheckEdit139.Checked = .ASKEP_317
				CheckEdit140.Checked = .ASKEP_318
				CheckEdit141.Checked = .ASKEP_319
				CheckEdit142.Checked = .ASKEP_320
				TextEdit24.Text = .ASKEP_321
				CheckEdit241.Checked = .ASKEP_322
				CheckEdit242.Checked = .ASKEP_323
				CheckEdit243.Checked = .ASKEP_324
				CheckEdit244.Checked = .ASKEP_325
				CheckEdit245.Checked = .ASKEP_326
				TextEdit40.Text = .ASKEP_327
				grdPerawat12.Text = .ASKEP_328
				grdPerawat17.Text = .ASKEP_329


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
            Dim ds = oS_DIGITAL_ASKEPGERIATRI_4_8.GetStructureHeader
            With ds
                .KDKUNJUNGAN = txtNoRegister.Text
                .KDPENDAFTARAN = sKDPENDAFTARAN
                Try
                    .DATECREATED = oS_DIGITAL_ASKEPGERIATRI_4_8.GetData(txtNoRegister.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .DATE = deDATE.DateTime
                
                .ASKEP_1 = CheckEdit1.Checked
                .ASKEP_2 = CheckEdit2.Checked
                .ASKEP_3 = CheckEdit3.Checked
                .ASKEP_4 = CheckEdit4.Checked
                .ASKEP_5 = CheckEdit5.Checked
                .ASKEP_6 = CheckEdit6.Checked
                .ASKEP_7 = TextEdit1.Text
                .ASKEP_8 = CheckEdit7.Checked
                .ASKEP_9 = CheckEdit8.Checked
                .ASKEP_10 = CheckEdit9.Checked
                .ASKEP_11 = CheckEdit10.Checked
                .ASKEP_12 = CheckEdit11.Checked
                .ASKEP_13 = CheckEdit12.Checked
                .ASKEP_14 = CheckEdit13.Checked
                .ASKEP_15 = TextEdit2.Text
                .ASKEP_16 = CheckEdit14.Checked
                .ASKEP_17 = CheckEdit15.Checked
                .ASKEP_18 = CheckEdit16.Checked
                .ASKEP_19 = CheckEdit17.Checked
                .ASKEP_20 = CheckEdit18.Checked
                .ASKEP_21 = CheckEdit19.Checked
                .ASKEP_22 = CheckEdit20.Checked
                .ASKEP_23 = CheckEdit21.Checked
                .ASKEP_24 = TextEdit3.Text
                .ASKEP_25 = grdPerawat1.Text
                .ASKEP_26 = CheckEdit22.Checked
                .ASKEP_27 = CheckEdit23.Checked
                .ASKEP_28 = CheckEdit24.Checked
                .ASKEP_29 = CheckEdit25.Checked
                .ASKEP_30 = TextEdit4.Text
                .ASKEP_31 = CheckEdit26.Checked
                .ASKEP_32 = CheckEdit27.Checked
                .ASKEP_33 = CheckEdit28.Checked
                .ASKEP_34 = CheckEdit29.Checked
                .ASKEP_35 = CheckEdit30.Checked
                .ASKEP_36 = CheckEdit31.Checked
                .ASKEP_37 = CheckEdit32.Checked
                .ASKEP_38 = CheckEdit33.Checked
                .ASKEP_39 = TextEdit5.Text
                .ASKEP_40 = CheckEdit34.Checked
                .ASKEP_41 = CheckEdit35.Checked
                .ASKEP_42 = CheckEdit36.Checked
                .ASKEP_43 = CheckEdit37.Checked
                .ASKEP_44 = TextEdit6.Text
                .ASKEP_45 = CheckEdit38.Checked
                .ASKEP_46 = CheckEdit39.Checked
                .ASKEP_47 = CheckEdit40.Checked
                .ASKEP_48 = CheckEdit41.Checked
                .ASKEP_49 = CheckEdit42.Checked
                .ASKEP_50 = CheckEdit43.Checked
                .ASKEP_51 = CheckEdit44.Checked
                .ASKEP_52 = CheckEdit45.Checked
                .ASKEP_53 = CheckEdit46.Checked
                .ASKEP_54 = TextEdit7.Text
                .ASKEP_55 = grdPerawat3.Text
                .ASKEP_56 = CheckEdit47.Checked
                .ASKEP_57 = CheckEdit48.Checked
                .ASKEP_58 = CheckEdit49.Checked
                .ASKEP_59 = CheckEdit50.Checked
                .ASKEP_60 = CheckEdit51.Checked
                .ASKEP_61 = TextEdit8.Text
                .ASKEP_62 = CheckEdit52.Checked
                .ASKEP_63 = CheckEdit53.Checked
                .ASKEP_64 = CheckEdit54.Checked
                .ASKEP_65 = CheckEdit55.Checked
                .ASKEP_66 = CheckEdit56.Checked
                .ASKEP_67 = TextEdit9.Text
                .ASKEP_68 = grdPerawat4.Text
                .ASKEP_69 = CheckEdit57.Checked
                .ASKEP_70 = CheckEdit58.Checked
                .ASKEP_71 = CheckEdit59.Checked
                .ASKEP_72 = CheckEdit60.Checked
                .ASKEP_73 = CheckEdit61.Checked
                .ASKEP_74 = TextEdit10.Text
                .ASKEP_75 = CheckEdit62.Checked
                .ASKEP_76 = CheckEdit63.Checked
                .ASKEP_77 = CheckEdit64.Checked
                .ASKEP_78 = CheckEdit65.Checked
                .ASKEP_79 = CheckEdit66.Checked
                .ASKEP_80 = TextEdit11.Text
                .ASKEP_81 = grdPerawat5.Text
                .ASKEP_82 = CheckEdit67.Checked
                .ASKEP_83 = CheckEdit68.Checked
                .ASKEP_84 = CheckEdit69.Checked
                .ASKEP_85 = CheckEdit70.Checked
                .ASKEP_86 = CheckEdit71.Checked
                .ASKEP_87 = TextEdit12.Text
                .ASKEP_88 = CheckEdit72.Checked
                .ASKEP_89 = CheckEdit73.Checked
                .ASKEP_90 = CheckEdit74.Checked
                .ASKEP_91 = CheckEdit75.Checked
                .ASKEP_92 = CheckEdit76.Checked
                .ASKEP_93 = CheckEdit77.Checked
                .ASKEP_94 = TextEdit13.Text
                .ASKEP_95 = grdPerawatan6.Text
                .ASKEP_96 = CheckEdit78.Checked
                .ASKEP_97 = CheckEdit79.Checked
                .ASKEP_98 = CheckEdit80.Checked
                .ASKEP_99 = CheckEdit81.Checked
                .ASKEP_100 = CheckEdit82.Checked
                .ASKEP_101 = TextEdit14.Text
                .ASKEP_102 = CheckEdit83.Checked
                .ASKEP_103 = CheckEdit84.Checked
                .ASKEP_104 = CheckEdit85.Checked
                .ASKEP_105 = CheckEdit86.Checked
                .ASKEP_106 = CheckEdit87.Checked
                .ASKEP_107 = CheckEdit88.Checked
                .ASKEP_108 = TextEdit15.Text
                .ASKEP_109 = grdPerawat7.Text
                .ASKEP_110 = CheckEdit89.Checked
                .ASKEP_111 = CheckEdit90.Checked
                .ASKEP_112 = CheckEdit91.Checked
                .ASKEP_113 = CheckEdit92.Checked
                .ASKEP_114 = CheckEdit93.Checked
                .ASKEP_115 = TextEdit16.Text
                .ASKEP_116 = CheckEdit94.Checked
                .ASKEP_117 = CheckEdit95.Checked
                .ASKEP_118 = CheckEdit96.Checked
                .ASKEP_119 = CheckEdit97.Checked
                .ASKEP_120 = CheckEdit98.Checked
                .ASKEP_121 = CheckEdit99.Checked
                .ASKEP_122 = CheckEdit100.Checked
                .ASKEP_123 = CheckEdit101.Checked
                .ASKEP_124 = CheckEdit102.Checked
                .ASKEP_125 = TextEdit17.Text
                .ASKEP_126 = grdPerawat8.Text
                .ASKEP_127 = CheckEdit103.Checked
                .ASKEP_128 = CheckEdit104.Checked
                .ASKEP_129 = CheckEdit105.Checked
                .ASKEP_130 = CheckEdit106.Checked
                .ASKEP_131 = TextEdit18.Text
                .ASKEP_132 = CheckEdit107.Checked
                .ASKEP_133 = CheckEdit108.Checked
                .ASKEP_134 = CheckEdit109.Checked
                .ASKEP_135 = CheckEdit110.Checked
                .ASKEP_136 = CheckEdit111.Checked
                .ASKEP_137 = CheckEdit112.Checked
                .ASKEP_138 = CheckEdit113.Checked
                .ASKEP_139 = TextEdit19.Text
                .ASKEP_140 = grdPerawat9.Text
                .ASKEP_141 = CheckEdit114.Checked
                .ASKEP_142 = CheckEdit115.Checked
                .ASKEP_143 = CheckEdit116.Checked
                .ASKEP_144 = CheckEdit117.Checked
                .ASKEP_145 = TextEdit20.Text
                .ASKEP_146 = CheckEdit118.Checked
                .ASKEP_147 = CheckEdit119.Checked
                .ASKEP_148 = CheckEdit120.Checked
                .ASKEP_149 = CheckEdit121.Checked
                .ASKEP_150 = CheckEdit122.Checked
                .ASKEP_151 = CheckEdit123.Checked
                .ASKEP_152 = CheckEdit124.Checked
                .ASKEP_153 = CheckEdit125.Checked
                .ASKEP_154 = TextEdit21.Text
                .ASKEP_155 = grdPerawatan10.Text
                .ASKEP_156 = CheckEdit126.Checked
                .ASKEP_157 = CheckEdit127.Checked
                .ASKEP_158 = CheckEdit128.Checked
                .ASKEP_159 = CheckEdit129.Checked
                .ASKEP_160 = CheckEdit130.Checked
                .ASKEP_161 = TextEdit22.Text
                .ASKEP_162 = CheckEdit131.Checked
                .ASKEP_163 = CheckEdit132.Checked
                .ASKEP_164 = CheckEdit133.Checked
                .ASKEP_165 = CheckEdit134.Checked
                .ASKEP_166 = CheckEdit135.Checked
                .ASKEP_167 = CheckEdit136.Checked
                .ASKEP_168 = CheckEdit137.Checked
                .ASKEP_169 = TextEdit23.Text
                .ASKEP_170 = grdPerawat11.Text
                .ASKEP_171 = CheckEdit143.Checked
                .ASKEP_172 = CheckEdit144.Checked
                .ASKEP_173 = CheckEdit145.Checked
                .ASKEP_174 = CheckEdit146.Checked
                .ASKEP_175 = CheckEdit147.Checked
                .ASKEP_176 = CheckEdit148.Checked
                .ASKEP_177 = TextEdit25.Text
                .ASKEP_178 = CheckEdit149.Checked
                .ASKEP_179 = CheckEdit150.Checked
                .ASKEP_180 = CheckEdit151.Checked
                .ASKEP_181 = CheckEdit152.Checked
                .ASKEP_182 = CheckEdit153.Checked
                .ASKEP_183 = CheckEdit154.Checked
                .ASKEP_184 = TextEdit26.Text
                .ASKEP_185 = grdPerawat13.Text
                .ASKEP_186 = CheckEdit155.Checked
                .ASKEP_187 = CheckEdit156.Checked
                .ASKEP_188 = CheckEdit157.Checked
                .ASKEP_189 = CheckEdit158.Checked
                .ASKEP_190 = CheckEdit159.Checked
                .ASKEP_191 = TextEdit27.Text
                .ASKEP_192 = CheckEdit160.Checked
                .ASKEP_193 = CheckEdit161.Checked
                .ASKEP_194 = CheckEdit162.Checked
                .ASKEP_195 = CheckEdit163.Checked
                .ASKEP_196 = CheckEdit164.Checked
                .ASKEP_197 = CheckEdit165.Checked
                .ASKEP_198 = TextEdit28.Text
                .ASKEP_199 = grdPerawat14.Text
                .ASKEP_200 = CheckEdit166.Checked
                .ASKEP_201 = CheckEdit167.Checked
                .ASKEP_202 = CheckEdit168.Checked
                .ASKEP_203 = CheckEdit169.Checked
                .ASKEP_204 = CheckEdit170.Checked
                .ASKEP_205 = CheckEdit171.Checked
                .ASKEP_206 = TextEdit29.Text
                .ASKEP_207 = CheckEdit172.Checked
                .ASKEP_208 = CheckEdit173.Checked
                .ASKEP_209 = CheckEdit174.Checked
                .ASKEP_210 = CheckEdit175.Checked
                .ASKEP_211 = CheckEdit176.Checked
                .ASKEP_212 = TextEdit30.Text
                .ASKEP_213 = grdPerawat15.Text
                .ASKEP_214 = CheckEdit177.Checked
                .ASKEP_215 = CheckEdit178.Checked
                .ASKEP_216 = CheckEdit179.Checked
                .ASKEP_217 = CheckEdit180.Checked
                .ASKEP_218 = CheckEdit181.Checked
                .ASKEP_219 = CheckEdit182.Checked
                .ASKEP_220 = TextEdit31.Text
                .ASKEP_221 = CheckEdit183.Checked
                .ASKEP_222 = CheckEdit184.Checked
                .ASKEP_223 = CheckEdit185.Checked
                .ASKEP_224 = CheckEdit186.Checked
                .ASKEP_225 = CheckEdit187.Checked
                .ASKEP_226 = CheckEdit188.Checked
                .ASKEP_227 = CheckEdit189.Checked
                .ASKEP_228 = CheckEdit190.Checked
                .ASKEP_229 = CheckEdit191.Checked
                .ASKEP_230 = CheckEdit192.Checked
                .ASKEP_231 = TextEdit32.Text
                .ASKEP_232 = grdPerawat16.Text
                .ASKEP_233 = CheckEdit193.Checked
                .ASKEP_234 = CheckEdit194.Checked
                .ASKEP_235 = CheckEdit195.Checked
                .ASKEP_236 = CheckEdit196.Checked
                .ASKEP_237 = CheckEdit197.Checked
                .ASKEP_238 = TextEdit33.Text
                .ASKEP_239 = CheckEdit198.Checked
                .ASKEP_240 = CheckEdit199.Checked
                .ASKEP_241 = CheckEdit200.Checked
                .ASKEP_242 = CheckEdit201.Checked
                .ASKEP_243 = CheckEdit202.Checked
                .ASKEP_244 = TextEdit34.Text
                .ASKEP_245 = CheckEdit203.Checked
                .ASKEP_246 = CheckEdit204.Checked
                .ASKEP_247 = CheckEdit205.Checked
                .ASKEP_248 = CheckEdit206.Checked
                .ASKEP_249 = CheckEdit207.Checked
                .ASKEP_250 = CheckEdit208.Checked
                .ASKEP_251 = TextEdit35.Text
                .ASKEP_252 = CheckEdit209.Checked
                .ASKEP_253 = CheckEdit210.Checked
                .ASKEP_254 = CheckEdit211.Checked
                .ASKEP_255 = CheckEdit212.Checked
                .ASKEP_256 = CheckEdit213.Checked
                .ASKEP_257 = CheckEdit214.Checked
                .ASKEP_258 = CheckEdit215.Checked
                .ASKEP_259 = CheckEdit216.Checked
                .ASKEP_260 = CheckEdit217.Checked
                .ASKEP_261 = CheckEdit218.Checked
                .ASKEP_262 = CheckEdit219.Checked
                .ASKEP_263 = TextEdit36.Text
                .ASKEP_264 = grdPerawat18.Text
                .ASKEP_265 = CheckEdit220.Checked
                .ASKEP_266 = CheckEdit221.Checked
                .ASKEP_267 = CheckEdit222.Checked
                .ASKEP_268 = CheckEdit223.Checked
                .ASKEP_269 = CheckEdit224.Checked
                .ASKEP_270 = CheckEdit225.Checked
                .ASKEP_271 = CheckEdit226.Checked
                .ASKEP_272 = CheckEdit227.Checked
                .ASKEP_273 = CheckEdit228.Checked
                .ASKEP_274 = TextEdit37.Text
                .ASKEP_275 = CheckEdit229.Checked
                .ASKEP_276 = CheckEdit230.Checked
                .ASKEP_277 = CheckEdit231.Checked
                .ASKEP_278 = CheckEdit232.Checked
                .ASKEP_279 = CheckEdit233.Checked
                .ASKEP_280 = CheckEdit234.Checked
                .ASKEP_281 = CheckEdit235.Checked
                .ASKEP_282 = CheckEdit236.Checked
                .ASKEP_283 = CheckEdit237.Checked
                .ASKEP_284 = CheckEdit238.Checked
                .ASKEP_285 = CheckEdit239.Checked
                .ASKEP_286 = TextEdit38.Text
                .ASKEP_287 = grdPerawat19.Text
                .ASKEP_288 = CheckEdit240.Checked
                .ASKEP_289 = TextEdit39.Text
                .ASKEP_290 = MemoEdit1.Text
                .ASKEP_291 = MemoEdit2.Text
                .ASKEP_292 = grdPerawat20.Text
                .ASKEP_293 = CheckEdit246.Checked
                .ASKEP_294 = CheckEdit247.Checked
                .ASKEP_295 = CheckEdit248.Checked
                .ASKEP_296 = CheckEdit249.Checked
                .ASKEP_297 = MemoEdit3.Text
                .ASKEP_298 = grdPerawat21.Text
                .ASKEP_299 = CheckEdit250.Checked
                .ASKEP_300 = TextEdit41.Text
                .ASKEP_301 = CheckEdit251.Checked
                .ASKEP_302 = CheckEdit252.Checked
                .ASKEP_303 = CheckEdit253.Checked
                .ASKEP_304 = CheckEdit254.Checked
                .ASKEP_305 = CheckEdit255.Checked
                .ASKEP_306 = CheckEdit256.Checked
                .ASKEP_307 = CheckEdit257.Checked
                .ASKEP_308 = CheckEdit258.Checked
                .ASKEP_309 = CheckEdit259.Checked
                .ASKEP_310 = CheckEdit260.Checked
                .ASKEP_311 = MemoEdit4.Text
                .ASKEP_312 = grdPerawat22.Text
                .ASKEP_313 = grdPerawat23.Text

                '.ASKEP_314 = PictureBox1.Text

                Try
                    Dim ms As New IO.MemoryStream()
                    PictureBox1.Image.Save(ms, PictureBox1.Image.RawFormat)

                    Dim data As Byte() = ms.GetBuffer()

                    .ASKEP_314 = data
                Catch oErr As Exception
                    Try
                        .ASKEP_314 = oS_DIGITAL_ASKEPGERIATRI_4_8.GetData(txtNoRegister.Text).ASKEP_314
                    Catch ex As Exception

                    End Try
                End Try

                'tambahan
				.ASKEP_315 = grdPerawat2.Text
				.ASKEP_316 = CheckEdit138.Checked
				.ASKEP_317 = CheckEdit139.Checked
				.ASKEP_318 = CheckEdit140.Checked
				.ASKEP_319 = CheckEdit141.Checked
				.ASKEP_320 = CheckEdit142.Checked
				.ASKEP_321 = TextEdit24.Text
				.ASKEP_322 = CheckEdit241.Checked
				.ASKEP_323 = CheckEdit242.Checked
				.ASKEP_324 = CheckEdit243.Checked
				.ASKEP_325 = CheckEdit244.Checked
				.ASKEP_326 = CheckEdit245.Checked
				.ASKEP_327 = TextEdit40.Text
				.ASKEP_328 = grdPerawat12.Text
				.ASKEP_329 = grdPerawat17.Text

                
                Try
                    .CETAK = oS_DIGITAL_ASKEPGERIATRI_4_8.GetData(txtNoRegister.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                .KDUSER = sKDUSER
                .KDUSER_SIGNATURE = sKDUSERSIGNATURE

            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_ASKEPGERIATRI_4_8.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_ASKEPGERIATRI_4_8.UpdateData(ds)
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
            SQL &= "AND KELOMPOKIPK = 'NAKES'"
            SQL &= "ORDER BY NAME_DISPLAY ASC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "PERAWAT")

            grdPerawat23.Properties.DataSource = ds.Tables("PERAWAT")
            grdPerawat23.Properties.ValueMember = "NAME_DISPLAY"
            grdPerawat23.Properties.DisplayMember = "NAME_DISPLAY"


            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
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
            SQL &= "S_DIGITAL_ASKEPGERIATRI_4_8 A "
            SQL &= "INNER JOIN R_IDENTITAS_PASIEN B  "
            SQL &= "ON A.KDKUNJUNGAN = B.KDKUNJUNGAN  "
            SQL &= "WHERE b.KDCUSTOMER = '" & KDCUSTOMER & "'  "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "R_GERIATRI48")

            grd_Riwayat48.MainView = grv_Riwayat48
            grd_Riwayat48.DataSource = ds.Tables("R_GERIATRI48")
            grd_Riwayat48.ForceInitialize()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox("Preview Data Dokter: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub CopyRiwayatAskepGeriatriLembar48ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyRiwayatAskepGeriatriLembar48ToolStripMenuItem.Click
        If grv_Riwayat48.GetFocusedRowCellValue("KDKUNJUNGAN") Is Nothing Then
            Exit Sub
        End If
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_ASKEPGERIATRI_4_8.GetData(grv_Riwayat48.GetFocusedRowCellValue("KDKUNJUNGAN"))
            With ds

                deDATE.DateTime = .DATE

                CheckEdit1.Checked = .ASKEP_1
                CheckEdit2.Checked = .ASKEP_2
                CheckEdit3.Checked = .ASKEP_3
                CheckEdit4.Checked = .ASKEP_4
                CheckEdit5.Checked = .ASKEP_5
                CheckEdit6.Checked = .ASKEP_6
                TextEdit1.Text = .ASKEP_7
                CheckEdit7.Checked = .ASKEP_8
                CheckEdit8.Checked = .ASKEP_9
                CheckEdit9.Checked = .ASKEP_10
                CheckEdit10.Checked = .ASKEP_11
                CheckEdit11.Checked = .ASKEP_12
                CheckEdit12.Checked = .ASKEP_13
                CheckEdit13.Checked = .ASKEP_14
                TextEdit2.Text = .ASKEP_15
                CheckEdit14.Checked = .ASKEP_16
                CheckEdit15.Checked = .ASKEP_17
                CheckEdit16.Checked = .ASKEP_18
                CheckEdit17.Checked = .ASKEP_19
                CheckEdit18.Checked = .ASKEP_20
                CheckEdit19.Checked = .ASKEP_21
                CheckEdit20.Checked = .ASKEP_22
                CheckEdit21.Checked = .ASKEP_23
                TextEdit3.Text = .ASKEP_24
                grdPerawat1.Text = .ASKEP_25
                CheckEdit22.Checked = .ASKEP_26
                CheckEdit23.Checked = .ASKEP_27
                CheckEdit24.Checked = .ASKEP_28
                CheckEdit25.Checked = .ASKEP_29
                TextEdit4.Text = .ASKEP_30
                CheckEdit26.Checked = .ASKEP_31
                CheckEdit27.Checked = .ASKEP_32
                CheckEdit28.Checked = .ASKEP_33
                CheckEdit29.Checked = .ASKEP_34
                CheckEdit30.Checked = .ASKEP_35
                CheckEdit31.Checked = .ASKEP_36
                CheckEdit32.Checked = .ASKEP_37
                CheckEdit33.Checked = .ASKEP_38
                TextEdit5.Text = .ASKEP_39
                CheckEdit34.Checked = .ASKEP_40
                CheckEdit35.Checked = .ASKEP_41
                CheckEdit36.Checked = .ASKEP_42
                CheckEdit37.Checked = .ASKEP_43
                TextEdit6.Text = .ASKEP_44
                CheckEdit38.Checked = .ASKEP_45
                CheckEdit39.Checked = .ASKEP_46
                CheckEdit40.Checked = .ASKEP_47
                CheckEdit41.Checked = .ASKEP_48
                CheckEdit42.Checked = .ASKEP_49
                CheckEdit43.Checked = .ASKEP_50
                CheckEdit44.Checked = .ASKEP_51
                CheckEdit45.Checked = .ASKEP_52
                CheckEdit46.Checked = .ASKEP_53
                TextEdit7.Text = .ASKEP_54
                grdPerawat3.Text = .ASKEP_55
                CheckEdit47.Checked = .ASKEP_56
                CheckEdit48.Checked = .ASKEP_57
                CheckEdit49.Checked = .ASKEP_58
                CheckEdit50.Checked = .ASKEP_59
                CheckEdit51.Checked = .ASKEP_60
                TextEdit8.Text = .ASKEP_61
                CheckEdit52.Checked = .ASKEP_62
                CheckEdit53.Checked = .ASKEP_63
                CheckEdit54.Checked = .ASKEP_64
                CheckEdit55.Checked = .ASKEP_65
                CheckEdit56.Checked = .ASKEP_66
                TextEdit9.Text = .ASKEP_67
                grdPerawat4.Text = .ASKEP_68
                CheckEdit57.Checked = .ASKEP_69
                CheckEdit58.Checked = .ASKEP_70
                CheckEdit59.Checked = .ASKEP_71
                CheckEdit60.Checked = .ASKEP_72
                CheckEdit61.Checked = .ASKEP_73
                TextEdit10.Text = .ASKEP_74
                CheckEdit62.Checked = .ASKEP_75
                CheckEdit63.Checked = .ASKEP_76
                CheckEdit64.Checked = .ASKEP_77
                CheckEdit65.Checked = .ASKEP_78
                CheckEdit66.Checked = .ASKEP_79
                TextEdit11.Text = .ASKEP_80
                grdPerawat5.Text = .ASKEP_81
                CheckEdit67.Checked = .ASKEP_82
                CheckEdit68.Checked = .ASKEP_83
                CheckEdit69.Checked = .ASKEP_84
                CheckEdit70.Checked = .ASKEP_85
                CheckEdit71.Checked = .ASKEP_86
                TextEdit12.Text = .ASKEP_87
                CheckEdit72.Checked = .ASKEP_88
                CheckEdit73.Checked = .ASKEP_89
                CheckEdit74.Checked = .ASKEP_90
                CheckEdit75.Checked = .ASKEP_91
                CheckEdit76.Checked = .ASKEP_92
                CheckEdit77.Checked = .ASKEP_93
                TextEdit13.Text = .ASKEP_94
                grdPerawatan6.Text = .ASKEP_95
                CheckEdit78.Checked = .ASKEP_96
                CheckEdit79.Checked = .ASKEP_97
                CheckEdit80.Checked = .ASKEP_98
                CheckEdit81.Checked = .ASKEP_99
                CheckEdit82.Checked = .ASKEP_100
                TextEdit14.Text = .ASKEP_101
                CheckEdit83.Checked = .ASKEP_102
                CheckEdit84.Checked = .ASKEP_103
                CheckEdit85.Checked = .ASKEP_104
                CheckEdit86.Checked = .ASKEP_105
                CheckEdit87.Checked = .ASKEP_106
                CheckEdit88.Checked = .ASKEP_107
                TextEdit15.Text = .ASKEP_108
                grdPerawat7.Text = .ASKEP_109
                CheckEdit89.Checked = .ASKEP_110
                CheckEdit90.Checked = .ASKEP_111
                CheckEdit91.Checked = .ASKEP_112
                CheckEdit92.Checked = .ASKEP_113
                CheckEdit93.Checked = .ASKEP_114
                TextEdit16.Text = .ASKEP_115
                CheckEdit94.Checked = .ASKEP_116
                CheckEdit95.Checked = .ASKEP_117
                CheckEdit96.Checked = .ASKEP_118
                CheckEdit97.Checked = .ASKEP_119
                CheckEdit98.Checked = .ASKEP_120
                CheckEdit99.Checked = .ASKEP_121
                CheckEdit100.Checked = .ASKEP_122
                CheckEdit101.Checked = .ASKEP_123
                CheckEdit102.Checked = .ASKEP_124
                TextEdit17.Text = .ASKEP_125
                grdPerawat8.Text = .ASKEP_126
                CheckEdit103.Checked = .ASKEP_127
                CheckEdit104.Checked = .ASKEP_128
                CheckEdit105.Checked = .ASKEP_129
                CheckEdit106.Checked = .ASKEP_130
                TextEdit18.Text = .ASKEP_131
                CheckEdit107.Checked = .ASKEP_132
                CheckEdit108.Checked = .ASKEP_133
                CheckEdit109.Checked = .ASKEP_134
                CheckEdit110.Checked = .ASKEP_135
                CheckEdit111.Checked = .ASKEP_136
                CheckEdit112.Checked = .ASKEP_137
                CheckEdit113.Checked = .ASKEP_138
                TextEdit19.Text = .ASKEP_139
                grdPerawat9.Text = .ASKEP_140
                CheckEdit114.Checked = .ASKEP_141
                CheckEdit115.Checked = .ASKEP_142
                CheckEdit116.Checked = .ASKEP_143
                CheckEdit117.Checked = .ASKEP_144
                TextEdit20.Text = .ASKEP_145
                CheckEdit118.Checked = .ASKEP_146
                CheckEdit119.Checked = .ASKEP_147
                CheckEdit120.Checked = .ASKEP_148
                CheckEdit121.Checked = .ASKEP_149
                CheckEdit122.Checked = .ASKEP_150
                CheckEdit123.Checked = .ASKEP_151
                CheckEdit124.Checked = .ASKEP_152
                CheckEdit125.Checked = .ASKEP_153
                TextEdit21.Text = .ASKEP_154
                grdPerawatan10.Text = .ASKEP_155
                CheckEdit126.Checked = .ASKEP_156
                CheckEdit127.Checked = .ASKEP_157
                CheckEdit128.Checked = .ASKEP_158
                CheckEdit129.Checked = .ASKEP_159
                CheckEdit130.Checked = .ASKEP_160
                TextEdit22.Text = .ASKEP_161
                CheckEdit131.Checked = .ASKEP_162
                CheckEdit132.Checked = .ASKEP_163
                CheckEdit133.Checked = .ASKEP_164
                CheckEdit134.Checked = .ASKEP_165
                CheckEdit135.Checked = .ASKEP_166
                CheckEdit136.Checked = .ASKEP_167
                CheckEdit137.Checked = .ASKEP_168
                TextEdit23.Text = .ASKEP_169
                grdPerawat11.Text = .ASKEP_170
                CheckEdit143.Checked = .ASKEP_171
                CheckEdit144.Checked = .ASKEP_172
                CheckEdit145.Checked = .ASKEP_173
                CheckEdit146.Checked = .ASKEP_174
                CheckEdit147.Checked = .ASKEP_175
                CheckEdit148.Checked = .ASKEP_176
                TextEdit25.Text = .ASKEP_177
                CheckEdit149.Checked = .ASKEP_178
                CheckEdit150.Checked = .ASKEP_179
                CheckEdit151.Checked = .ASKEP_180
                CheckEdit152.Checked = .ASKEP_181
                CheckEdit153.Checked = .ASKEP_182
                CheckEdit154.Checked = .ASKEP_183
                TextEdit26.Text = .ASKEP_184
                grdPerawat13.Text = .ASKEP_185
                CheckEdit155.Checked = .ASKEP_186
                CheckEdit156.Checked = .ASKEP_187
                CheckEdit157.Checked = .ASKEP_188
                CheckEdit158.Checked = .ASKEP_189
                CheckEdit159.Checked = .ASKEP_190
                TextEdit27.Text = .ASKEP_191
                CheckEdit160.Checked = .ASKEP_192
                CheckEdit161.Checked = .ASKEP_193
                CheckEdit162.Checked = .ASKEP_194
                CheckEdit163.Checked = .ASKEP_195
                CheckEdit164.Checked = .ASKEP_196
                CheckEdit165.Checked = .ASKEP_197
                TextEdit28.Text = .ASKEP_198
                grdPerawat14.Text = .ASKEP_199
                CheckEdit166.Checked = .ASKEP_200
                CheckEdit167.Checked = .ASKEP_201
                CheckEdit168.Checked = .ASKEP_202
                CheckEdit169.Checked = .ASKEP_203
                CheckEdit170.Checked = .ASKEP_204
                CheckEdit171.Checked = .ASKEP_205
                TextEdit29.Text = .ASKEP_206
                CheckEdit172.Checked = .ASKEP_207
                CheckEdit173.Checked = .ASKEP_208
                CheckEdit174.Checked = .ASKEP_209
                CheckEdit175.Checked = .ASKEP_210
                CheckEdit176.Checked = .ASKEP_211
                TextEdit30.Text = .ASKEP_212
                grdPerawat15.Text = .ASKEP_213
                CheckEdit177.Checked = .ASKEP_214
                CheckEdit178.Checked = .ASKEP_215
                CheckEdit179.Checked = .ASKEP_216
                CheckEdit180.Checked = .ASKEP_217
                CheckEdit181.Checked = .ASKEP_218
                CheckEdit182.Checked = .ASKEP_219
                TextEdit31.Text = .ASKEP_220
                CheckEdit183.Checked = .ASKEP_221
                CheckEdit184.Checked = .ASKEP_222
                CheckEdit185.Checked = .ASKEP_223
                CheckEdit186.Checked = .ASKEP_224
                CheckEdit187.Checked = .ASKEP_225
                CheckEdit188.Checked = .ASKEP_226
                CheckEdit189.Checked = .ASKEP_227
                CheckEdit190.Checked = .ASKEP_228
                CheckEdit191.Checked = .ASKEP_229
                CheckEdit192.Checked = .ASKEP_230
                TextEdit32.Text = .ASKEP_231
                grdPerawat16.Text = .ASKEP_232
                CheckEdit193.Checked = .ASKEP_233
                CheckEdit194.Checked = .ASKEP_234
                CheckEdit195.Checked = .ASKEP_235
                CheckEdit196.Checked = .ASKEP_236
                CheckEdit197.Checked = .ASKEP_237
                TextEdit33.Text = .ASKEP_238
                CheckEdit198.Checked = .ASKEP_239
                CheckEdit199.Checked = .ASKEP_240
                CheckEdit200.Checked = .ASKEP_241
                CheckEdit201.Checked = .ASKEP_242
                CheckEdit202.Checked = .ASKEP_243
                TextEdit34.Text = .ASKEP_244
                CheckEdit203.Checked = .ASKEP_245
                CheckEdit204.Checked = .ASKEP_246
                CheckEdit205.Checked = .ASKEP_247
                CheckEdit206.Checked = .ASKEP_248
                CheckEdit207.Checked = .ASKEP_249
                CheckEdit208.Checked = .ASKEP_250
                TextEdit35.Text = .ASKEP_251
                CheckEdit209.Checked = .ASKEP_252
                CheckEdit210.Checked = .ASKEP_253
                CheckEdit211.Checked = .ASKEP_254
                CheckEdit212.Checked = .ASKEP_255
                CheckEdit213.Checked = .ASKEP_256
                CheckEdit214.Checked = .ASKEP_257
                CheckEdit215.Checked = .ASKEP_258
                CheckEdit216.Checked = .ASKEP_259
                CheckEdit217.Checked = .ASKEP_260
                CheckEdit218.Checked = .ASKEP_261
                CheckEdit219.Checked = .ASKEP_262
                TextEdit36.Text = .ASKEP_263
                grdPerawat18.Text = .ASKEP_264
                CheckEdit220.Checked = .ASKEP_265
                CheckEdit221.Checked = .ASKEP_266
                CheckEdit222.Checked = .ASKEP_267
                CheckEdit223.Checked = .ASKEP_268
                CheckEdit224.Checked = .ASKEP_269
                CheckEdit225.Checked = .ASKEP_270
                CheckEdit226.Checked = .ASKEP_271
                CheckEdit227.Checked = .ASKEP_272
                CheckEdit228.Checked = .ASKEP_273
                TextEdit37.Text = .ASKEP_274
                CheckEdit229.Checked = .ASKEP_275
                CheckEdit230.Checked = .ASKEP_276
                CheckEdit231.Checked = .ASKEP_277
                CheckEdit232.Checked = .ASKEP_278
                CheckEdit233.Checked = .ASKEP_279
                CheckEdit234.Checked = .ASKEP_280
                CheckEdit235.Checked = .ASKEP_281
                CheckEdit236.Checked = .ASKEP_282
                CheckEdit237.Checked = .ASKEP_283
                CheckEdit238.Checked = .ASKEP_284
                CheckEdit239.Checked = .ASKEP_285
                TextEdit38.Text = .ASKEP_286
                grdPerawat19.Text = .ASKEP_287
                CheckEdit240.Checked = .ASKEP_288
                TextEdit39.Text = .ASKEP_289
                MemoEdit1.Text = .ASKEP_290
                MemoEdit2.Text = .ASKEP_291
                grdPerawat20.Text = .ASKEP_292
                CheckEdit246.Checked = .ASKEP_293
                CheckEdit247.Checked = .ASKEP_294
                CheckEdit248.Checked = .ASKEP_295
                CheckEdit249.Checked = .ASKEP_296
                MemoEdit3.Text = .ASKEP_297
                grdPerawat21.Text = .ASKEP_298
                CheckEdit250.Checked = .ASKEP_299
                TextEdit41.Text = .ASKEP_300
                CheckEdit251.Checked = .ASKEP_301
                CheckEdit252.Checked = .ASKEP_302
                CheckEdit253.Checked = .ASKEP_303
                CheckEdit254.Checked = .ASKEP_304
                CheckEdit255.Checked = .ASKEP_305
                CheckEdit256.Checked = .ASKEP_306
                CheckEdit257.Checked = .ASKEP_307
                CheckEdit258.Checked = .ASKEP_308
                CheckEdit259.Checked = .ASKEP_309
                CheckEdit260.Checked = .ASKEP_310
                MemoEdit4.Text = .ASKEP_311
                grdPerawat22.Text = .ASKEP_312
                grdPerawat23.Text = .ASKEP_313
                'PictureBox1.Text = .ASKEP_314

                sKDUSER = .KDUSER
                sKDUSERSIGNATURE = .KDUSER_SIGNATURE

                Try
                    PictureBox1.Image = ByteArrayToImage(.ASKEP_314.ToArray())
                Catch oErr As Exception
                End Try

                'tambahan
                grdPerawat2.Text = .ASKEP_315
                CheckEdit138.Checked = .ASKEP_316
                CheckEdit139.Checked = .ASKEP_317
                CheckEdit140.Checked = .ASKEP_318
                CheckEdit141.Checked = .ASKEP_319
                CheckEdit142.Checked = .ASKEP_320
                TextEdit24.Text = .ASKEP_321
                CheckEdit241.Checked = .ASKEP_322
                CheckEdit242.Checked = .ASKEP_323
                CheckEdit243.Checked = .ASKEP_324
                CheckEdit244.Checked = .ASKEP_325
                CheckEdit245.Checked = .ASKEP_326
                TextEdit40.Text = .ASKEP_327
                grdPerawat12.Text = .ASKEP_328
                grdPerawat17.Text = .ASKEP_329


            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub frmAskepGeriatri48_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
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