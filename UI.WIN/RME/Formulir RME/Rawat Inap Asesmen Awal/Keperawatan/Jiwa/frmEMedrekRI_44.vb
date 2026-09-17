Imports System.Data.SqlClient
Imports System.Linq
Imports DataAccess

Public Class frmEMedrekRI_44
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_RI_44 As New Digital.clsDigital_RI_44
    Private down As Boolean = False
    Private sKoneksi As String = String.Empty
    Private sIsOtority As Boolean = False

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDKUNJUNGAN As String)
        oFormMode = FormMode

        Dim oPendaftaran As New Identitas.clsIdentitasPasien
        Dim dsPendaftaran = oPendaftaran.GetData(KDKUNJUNGAN)

        txtNoRegister.Text = KDKUNJUNGAN

        Dim oSetKoneksi As New Setting.clsSetKoneksi
        Dim dsSetKoneksi = oSetKoneksi.GetData()
        If dsSetKoneksi IsNot Nothing Then
            sKoneksi = dsSetKoneksi.KONEKSI
        End If

        If dsPendaftaran IsNot Nothing Then
            txtNoPasien.Text = dsPendaftaran.KDCUSTOMER
            txtNamaPasien.Text = dsPendaftaran.NAMAPASIEN
            txtUmur.Text = dsPendaftaran.USIA
            txtTanggalDaftar.Text = dsPendaftaran.DATE.ToString("dd-MM-yyyy HH:mm:ss")
            txtNoRegister.Text = dspendaftaran.KDKUNJUNGAN
            txtTujuan.Text = dsPendaftaran.TUJUAN
            txtDokter.Text = dsPendaftaran.DOKTER
        Else
            txtNoPasien.ResetText()
            txtNamaPasien.ResetText()
            txtUmur.ResetText()
            txtTanggalDaftar.ResetText()
            txtNoRegister.ResetText()
            txtTujuan.ResetText()
            txtDokter.ResetText()
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

        grdKDDOCTOR2.Properties.ReadOnly = Status
        deDATE.Properties.ReadOnly = Status
        MemoEdit1.Properties.ReadOnly = Status
        MemoEdit2.Properties.ReadOnly = Status
        MemoEdit3.Properties.ReadOnly = Status
        TextEdit1.Properties.ReadOnly = Status
        TextEdit2.Properties.ReadOnly = Status
        TextEdit3.Properties.ReadOnly = Status
        TextEdit4.Properties.ReadOnly = Status
        TextEdit5.Properties.ReadOnly = Status
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
        TextEdit16.Properties.ReadOnly = Status
        TextEdit17.Properties.ReadOnly = Status
        TextEdit18.Properties.ReadOnly = Status
        TextEdit19.Properties.ReadOnly = Status
        TextEdit20.Properties.ReadOnly = Status
        TextEdit21.Properties.ReadOnly = Status
        TextEdit22.Properties.ReadOnly = Status
        TextEdit23.Properties.ReadOnly = Status
        TextEdit24.Properties.ReadOnly = Status
        TextEdit25.Properties.ReadOnly = Status
        TextEdit26.Properties.ReadOnly = Status
        TextEdit27.Properties.ReadOnly = Status
        TextEdit28.Properties.ReadOnly = Status
        TextEdit29.Properties.ReadOnly = Status
        TextEdit30.Properties.ReadOnly = Status
        TextEdit31.Properties.ReadOnly = Status
        TextEdit32.Properties.ReadOnly = Status
        TextEdit33.Properties.ReadOnly = Status
        TextEdit34.Properties.ReadOnly = Status
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
        TextEdit75.Properties.ReadOnly = Status
        TextEdit76.Properties.ReadOnly = Status
        TextEdit77.Properties.ReadOnly = Status
        TextEdit78.Properties.ReadOnly = Status
        TextEdit79.Properties.ReadOnly = Status
        TextEdit80.Properties.ReadOnly = Status
        TextEdit81.Properties.ReadOnly = Status
        TextEdit82.Properties.ReadOnly = Status
        TextEdit83.Properties.ReadOnly = Status
        TextEdit84.Properties.ReadOnly = Status
        TextEdit85.Properties.ReadOnly = Status
        TextEdit86.Properties.ReadOnly = Status
        TextEdit87.Properties.ReadOnly = Status
        TextEdit88.Properties.ReadOnly = Status
        TextEdit89.Properties.ReadOnly = Status
        TextEdit90.Properties.ReadOnly = Status
        TextEdit91.Properties.ReadOnly = Status
        TextEdit92.Properties.ReadOnly = Status
        TextEdit93.Properties.ReadOnly = Status
        TextEdit94.Properties.ReadOnly = Status
        TextEdit95.Properties.ReadOnly = Status
        TextEdit96.Properties.ReadOnly = Status
        TextEdit97.Properties.ReadOnly = Status
        TextEdit98.Properties.ReadOnly = Status
        TextEdit99.Properties.ReadOnly = Status
        TextEdit100.Properties.ReadOnly = Status
        TextEdit101.Properties.ReadOnly = Status
        TextEdit102.Properties.ReadOnly = Status
        TextEdit103.Properties.ReadOnly = Status
        TextEdit104.Properties.ReadOnly = Status
        TextEdit105.Properties.ReadOnly = Status
        TextEdit106.Properties.ReadOnly = Status
        TextEdit107.Properties.ReadOnly = Status
        TextEdit108.Properties.ReadOnly = Status
        TextEdit109.Properties.ReadOnly = Status
        TextEdit110.Properties.ReadOnly = Status
        TextEdit111.Properties.ReadOnly = Status
        TextEdit112.Properties.ReadOnly = Status
        TextEdit113.Properties.ReadOnly = Status
        TextEdit114.Properties.ReadOnly = Status
        TextEdit115.Properties.ReadOnly = Status
        TextEdit116.Properties.ReadOnly = Status
        TextEdit117.Properties.ReadOnly = Status
        TextEdit118.Properties.ReadOnly = Status
        TextEdit119.Properties.ReadOnly = Status
        TextEdit120.Properties.ReadOnly = Status
        TextEdit121.Properties.ReadOnly = Status
        TextEdit122.Properties.ReadOnly = Status
        TextEdit123.Properties.ReadOnly = Status
        TextEdit124.Properties.ReadOnly = Status
        TextEdit125.Properties.ReadOnly = Status
        TextEdit126.Properties.ReadOnly = Status
        TextEdit127.Properties.ReadOnly = Status
        TextEdit128.Properties.ReadOnly = Status
        TextEdit129.Properties.ReadOnly = Status
        TextEdit130.Properties.ReadOnly = Status
        TextEdit131.Properties.ReadOnly = Status
        TextEdit132.Properties.ReadOnly = Status
        TextEdit133.Properties.ReadOnly = Status
        TextEdit134.Properties.ReadOnly = Status
        TextEdit135.Properties.ReadOnly = Status
        TextEdit136.Properties.ReadOnly = Status
        TextEdit137.Properties.ReadOnly = Status
        TextEdit138.Properties.ReadOnly = Status
        TextEdit139.Properties.ReadOnly = Status
        TextEdit140.Properties.ReadOnly = Status
        TextEdit141.Properties.ReadOnly = Status
        TextEdit142.Properties.ReadOnly = Status
        TextEdit143.Properties.ReadOnly = Status
        TextEdit144.Properties.ReadOnly = Status
        TextEdit145.Properties.ReadOnly = Status
        TextEdit146.Properties.ReadOnly = Status
        TextEdit147.Properties.ReadOnly = Status
        TextEdit148.Properties.ReadOnly = Status
        TextEdit149.Properties.ReadOnly = Status
        TextEdit150.Properties.ReadOnly = Status
        TextEdit151.Properties.ReadOnly = Status
        TextEdit152.Properties.ReadOnly = Status
        TextEdit153.Properties.ReadOnly = Status
        TextEdit154.Properties.ReadOnly = Status
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
        TextEdit167.Properties.ReadOnly = Status
        TextEdit168.Properties.ReadOnly = Status
        TextEdit169.Properties.ReadOnly = Status
        TextEdit170.Properties.ReadOnly = Status
        TextEdit171.Properties.ReadOnly = Status
        TextEdit172.Properties.ReadOnly = Status
        TextEdit173.Properties.ReadOnly = Status
        TextEdit174.Properties.ReadOnly = Status
        TextEdit175.Properties.ReadOnly = Status
        TextEdit176.Properties.ReadOnly = Status
        TextEdit177.Properties.ReadOnly = Status
        TextEdit178.Properties.ReadOnly = Status
        TextEdit179.Properties.ReadOnly = Status
        TextEdit180.Properties.ReadOnly = Status
        TextEdit181.Properties.ReadOnly = Status
        TextEdit182.Properties.ReadOnly = Status
        TextEdit183.Properties.ReadOnly = Status
        TextEdit184.Properties.ReadOnly = Status
        TextEdit185.Properties.ReadOnly = Status
        TextEdit186.Properties.ReadOnly = Status
        TextEdit187.Properties.ReadOnly = Status
        TextEdit188.Properties.ReadOnly = Status
        TextEdit189.Properties.ReadOnly = Status
        TextEdit190.Properties.ReadOnly = Status
        TextEdit191.Properties.ReadOnly = Status
        TextEdit192.Properties.ReadOnly = Status
        TextEdit193.Properties.ReadOnly = Status
        TextEdit194.Properties.ReadOnly = Status
        TextEdit195.Properties.ReadOnly = Status
        TextEdit196.Properties.ReadOnly = Status
        TextEdit197.Properties.ReadOnly = Status
        TextEdit198.Properties.ReadOnly = Status
        TextEdit199.Properties.ReadOnly = Status
        TextEdit200.Properties.ReadOnly = Status
        TextEdit201.Properties.ReadOnly = Status
        TextEdit202.Properties.ReadOnly = Status
        TextEdit203.Properties.ReadOnly = Status
        TextEdit204.Properties.ReadOnly = Status
        TextEdit205.Properties.ReadOnly = Status
        TextEdit206.Properties.ReadOnly = Status
        TextEdit207.Properties.ReadOnly = Status
        TextEdit208.Properties.ReadOnly = Status
        TextEdit209.Properties.ReadOnly = Status
        TextEdit210.Properties.ReadOnly = Status
        TextEdit211.Properties.ReadOnly = Status
        TextEdit212.Properties.ReadOnly = Status
        TextEdit213.Properties.ReadOnly = Status
        TextEdit214.Properties.ReadOnly = Status
        TextEdit215.Properties.ReadOnly = Status
        TextEdit216.Properties.ReadOnly = Status
        TextEdit217.Properties.ReadOnly = Status
        TextEdit218.Properties.ReadOnly = Status
        TextEdit219.Properties.ReadOnly = Status
        TextEdit220.Properties.ReadOnly = Status
        TextEdit221.Properties.ReadOnly = Status
        TextEdit222.Properties.ReadOnly = Status
        TextEdit223.Properties.ReadOnly = Status
        TextEdit224.Properties.ReadOnly = Status
        TextEdit225.Properties.ReadOnly = Status
        TextEdit226.Properties.ReadOnly = Status
        TextEdit227.Properties.ReadOnly = Status
        TextEdit228.Properties.ReadOnly = Status
        TextEdit229.Properties.ReadOnly = Status
        TextEdit230.Properties.ReadOnly = Status
        TextEdit231.Properties.ReadOnly = Status
        TextEdit232.Properties.ReadOnly = Status
        TextEdit233.Properties.ReadOnly = Status
        TextEdit234.Properties.ReadOnly = Status
        TextEdit235.Properties.ReadOnly = Status
        TextEdit236.Properties.ReadOnly = Status
        TextEdit237.Properties.ReadOnly = Status
        TextEdit238.Properties.ReadOnly = Status
        TextEdit239.Properties.ReadOnly = Status
        TextEdit240.Properties.ReadOnly = Status
        TextEdit241.Properties.ReadOnly = Status
        TextEdit242.Properties.ReadOnly = Status
        TextEdit243.Properties.ReadOnly = Status
        TextEdit244.Properties.ReadOnly = Status
        TextEdit245.Properties.ReadOnly = Status
        TextEdit246.Properties.ReadOnly = Status
        TextEdit247.Properties.ReadOnly = Status
        TextEdit248.Properties.ReadOnly = Status
        TextEdit249.Properties.ReadOnly = Status
        TextEdit250.Properties.ReadOnly = Status
        TextEdit251.Properties.ReadOnly = Status
        TextEdit252.Properties.ReadOnly = Status
        TextEdit253.Properties.ReadOnly = Status
        TextEdit254.Properties.ReadOnly = Status
        TextEdit255.Properties.ReadOnly = Status
        TextEdit256.Properties.ReadOnly = Status
        TextEdit257.Properties.ReadOnly = Status
        TextEdit258.Properties.ReadOnly = Status
        TextEdit259.Properties.ReadOnly = Status
        TextEdit260.Properties.ReadOnly = Status
        TextEdit261.Properties.ReadOnly = Status
        TextEdit262.Properties.ReadOnly = Status
        TextEdit263.Properties.ReadOnly = Status
        TextEdit264.Properties.ReadOnly = Status
        TextEdit265.Properties.ReadOnly = Status
        TextEdit266.Properties.ReadOnly = Status
        TextEdit267.Properties.ReadOnly = Status
        TextEdit268.Properties.ReadOnly = Status
        TextEdit269.Properties.ReadOnly = Status
        TextEdit270.Properties.ReadOnly = Status
        TextEdit271.Properties.ReadOnly = Status
        TextEdit272.Properties.ReadOnly = Status
        TextEdit273.Properties.ReadOnly = Status
        TextEdit274.Properties.ReadOnly = Status
        TextEdit275.Properties.ReadOnly = Status
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
        CheckEdit29.Properties.ReadOnly = Status
        CheckEdit30.Properties.ReadOnly = Status
        CheckEdit31.Properties.ReadOnly = Status
        CheckEdit32.Properties.ReadOnly = Status
        CheckEdit33.Properties.ReadOnly = Status
        CheckEdit34.Properties.ReadOnly = Status
        CheckEdit35.Properties.ReadOnly = Status
        CheckEdit36.Properties.ReadOnly = Status
        CheckEdit37.Properties.ReadOnly = Status
        CheckEdit38.Properties.ReadOnly = Status
        CheckEdit39.Properties.ReadOnly = Status
        CheckEdit40.Properties.ReadOnly = Status
        CheckEdit41.Properties.ReadOnly = Status
        CheckEdit42.Properties.ReadOnly = Status
        CheckEdit43.Properties.ReadOnly = Status
        CheckEdit44.Properties.ReadOnly = Status
        CheckEdit45.Properties.ReadOnly = Status
        CheckEdit46.Properties.ReadOnly = Status
        CheckEdit47.Properties.ReadOnly = Status
        CheckEdit48.Properties.ReadOnly = Status
        CheckEdit49.Properties.ReadOnly = Status
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
        CheckEdit67.Properties.ReadOnly = Status
        CheckEdit68.Properties.ReadOnly = Status
        CheckEdit69.Properties.ReadOnly = Status
        CheckEdit70.Properties.ReadOnly = Status
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
        CheckEdit89.Properties.ReadOnly = Status
        CheckEdit90.Properties.ReadOnly = Status
        CheckEdit91.Properties.ReadOnly = Status
        CheckEdit92.Properties.ReadOnly = Status
        CheckEdit93.Properties.ReadOnly = Status
        CheckEdit94.Properties.ReadOnly = Status
        CheckEdit95.Properties.ReadOnly = Status
        CheckEdit96.Properties.ReadOnly = Status
        CheckEdit97.Properties.ReadOnly = Status
        CheckEdit98.Properties.ReadOnly = Status
        CheckEdit99.Properties.ReadOnly = Status
        CheckEdit100.Properties.ReadOnly = Status
        CheckEdit101.Properties.ReadOnly = Status
        CheckEdit102.Properties.ReadOnly = Status
        CheckEdit103.Properties.ReadOnly = Status
        CheckEdit104.Properties.ReadOnly = Status
        CheckEdit105.Properties.ReadOnly = Status
        CheckEdit106.Properties.ReadOnly = Status
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
        CheckEdit117.Properties.ReadOnly = Status
        CheckEdit118.Properties.ReadOnly = Status
        CheckEdit119.Properties.ReadOnly = Status
        CheckEdit120.Properties.ReadOnly = Status
        CheckEdit121.Properties.ReadOnly = Status
        CheckEdit122.Properties.ReadOnly = Status
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
        CheckEdit162.Properties.ReadOnly = Status
        CheckEdit163.Properties.ReadOnly = Status
        CheckEdit164.Properties.ReadOnly = Status
        CheckEdit165.Properties.ReadOnly = Status
        CheckEdit166.Properties.ReadOnly = Status
        CheckEdit167.Properties.ReadOnly = Status
        CheckEdit168.Properties.ReadOnly = Status
        CheckEdit169.Properties.ReadOnly = Status
        CheckEdit170.Properties.ReadOnly = Status
        CheckEdit171.Properties.ReadOnly = Status
        CheckEdit172.Properties.ReadOnly = Status
        CheckEdit173.Properties.ReadOnly = Status
        CheckEdit174.Properties.ReadOnly = Status
        CheckEdit175.Properties.ReadOnly = Status
        CheckEdit176.Properties.ReadOnly = Status
        CheckEdit177.Properties.ReadOnly = Status
        CheckEdit178.Properties.ReadOnly = Status
        CheckEdit179.Properties.ReadOnly = Status
        CheckEdit180.Properties.ReadOnly = Status
        CheckEdit181.Properties.ReadOnly = Status
        CheckEdit182.Properties.ReadOnly = Status
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
        CheckEdit193.Properties.ReadOnly = Status
        CheckEdit194.Properties.ReadOnly = Status
        CheckEdit195.Properties.ReadOnly = Status
        CheckEdit196.Properties.ReadOnly = Status
        CheckEdit197.Properties.ReadOnly = Status
        CheckEdit198.Properties.ReadOnly = Status
        CheckEdit199.Properties.ReadOnly = Status
        CheckEdit200.Properties.ReadOnly = Status
        CheckEdit201.Properties.ReadOnly = Status
        CheckEdit202.Properties.ReadOnly = Status
        CheckEdit203.Properties.ReadOnly = Status
        CheckEdit204.Properties.ReadOnly = Status
        CheckEdit205.Properties.ReadOnly = Status
        CheckEdit206.Properties.ReadOnly = Status
        CheckEdit207.Properties.ReadOnly = Status
        CheckEdit208.Properties.ReadOnly = Status
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
        CheckEdit220.Properties.ReadOnly = Status
        CheckEdit221.Properties.ReadOnly = Status
        CheckEdit222.Properties.ReadOnly = Status
        CheckEdit223.Properties.ReadOnly = Status
        CheckEdit224.Properties.ReadOnly = Status
        CheckEdit225.Properties.ReadOnly = Status
        CheckEdit226.Properties.ReadOnly = Status
        CheckEdit227.Properties.ReadOnly = Status
        CheckEdit228.Properties.ReadOnly = Status
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
        CheckEdit240.Properties.ReadOnly = Status
        CheckEdit241.Properties.ReadOnly = Status
        CheckEdit242.Properties.ReadOnly = Status
        CheckEdit243.Properties.ReadOnly = Status
        CheckEdit244.Properties.ReadOnly = Status
        CheckEdit245.Properties.ReadOnly = Status
        CheckEdit246.Properties.ReadOnly = Status
        CheckEdit247.Properties.ReadOnly = Status
        CheckEdit248.Properties.ReadOnly = Status
        CheckEdit249.Properties.ReadOnly = Status
        CheckEdit250.Properties.ReadOnly = Status
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
        CheckEdit261.Properties.ReadOnly = Status
        CheckEdit262.Properties.ReadOnly = Status
        CheckEdit263.Properties.ReadOnly = Status
        CheckEdit264.Properties.ReadOnly = Status
        CheckEdit265.Properties.ReadOnly = Status
        CheckEdit266.Properties.ReadOnly = Status
        CheckEdit267.Properties.ReadOnly = Status
        CheckEdit268.Properties.ReadOnly = Status
        CheckEdit269.Properties.ReadOnly = Status
        CheckEdit270.Properties.ReadOnly = Status
        CheckEdit271.Properties.ReadOnly = Status
        CheckEdit272.Properties.ReadOnly = Status
        CheckEdit273.Properties.ReadOnly = Status
        CheckEdit274.Properties.ReadOnly = Status
        CheckEdit275.Properties.ReadOnly = Status
        CheckEdit276.Properties.ReadOnly = Status
        CheckEdit277.Properties.ReadOnly = Status
        CheckEdit278.Properties.ReadOnly = Status
        CheckEdit279.Properties.ReadOnly = Status
        CheckEdit280.Properties.ReadOnly = Status
        CheckEdit281.Properties.ReadOnly = Status
        CheckEdit282.Properties.ReadOnly = Status
        CheckEdit283.Properties.ReadOnly = Status
        CheckEdit284.Properties.ReadOnly = Status
        CheckEdit285.Properties.ReadOnly = Status
        CheckEdit286.Properties.ReadOnly = Status

        Dim oSetUser As New Setting.clsUser
        Dim dsUser = oSetUser.GetData(sUserID)
        If dsUser IsNot Nothing Then
            If dsUser.ISOTORTY = True Then
                sIsOtority = True
            Else
                sIsOtority = False
            End If
        End If
    End Sub
    Private Sub fn_EmptyMe()
        grdKDDOCTOR2.ResetText()
        deDATE.DateTime = Now
        MemoEdit1.ResetText()
        MemoEdit2.ResetText()
        MemoEdit3.ResetText()
        TextEdit1.ResetText()
        TextEdit2.ResetText()
        TextEdit3.ResetText()
        TextEdit4.ResetText()
        TextEdit5.ResetText()
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
        TextEdit239.ResetText()
        TextEdit240.ResetText()
        TextEdit241.ResetText()
        TextEdit242.ResetText()
        TextEdit243.ResetText()
        TextEdit244.ResetText()
        TextEdit245.ResetText()
        TextEdit246.ResetText()
        TextEdit247.ResetText()
        TextEdit248.ResetText()
        TextEdit249.ResetText()
        TextEdit250.ResetText()
        TextEdit251.ResetText()
        TextEdit252.ResetText()
        TextEdit253.ResetText()
        TextEdit254.ResetText()
        TextEdit255.ResetText()
        TextEdit256.ResetText()
        TextEdit257.ResetText()
        TextEdit258.ResetText()
        TextEdit259.ResetText()
        TextEdit260.ResetText()
        TextEdit261.ResetText()
        TextEdit262.ResetText()
        TextEdit263.ResetText()
        TextEdit264.ResetText()
        TextEdit265.ResetText()
        TextEdit266.ResetText()
        TextEdit267.ResetText()
        TextEdit268.ResetText()
        TextEdit269.ResetText()
        TextEdit270.ResetText()
        TextEdit271.ResetText()
        TextEdit272.ResetText()
        TextEdit273.ResetText()
        TextEdit274.ResetText()
        TextEdit275.ResetText()
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
        CheckEdit42.Checked = False
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
        CheckEdit89.Checked = False
        CheckEdit90.Checked = False
        CheckEdit91.Checked = False
        CheckEdit92.Checked = False
        CheckEdit93.Checked = False
        CheckEdit94.Checked = False
        CheckEdit95.Checked = False
        CheckEdit96.Checked = False
        CheckEdit97.Checked = False
        CheckEdit98.Checked = False
        CheckEdit99.Checked = False
        CheckEdit100.Checked = False
        CheckEdit101.Checked = False
        CheckEdit102.Checked = False
        CheckEdit103.Checked = False
        CheckEdit104.Checked = False
        CheckEdit105.Checked = False
        CheckEdit106.Checked = False
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
        CheckEdit117.Checked = False
        CheckEdit118.Checked = False
        CheckEdit119.Checked = False
        CheckEdit120.Checked = False
        CheckEdit121.Checked = False
        CheckEdit122.Checked = False
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
        CheckEdit162.Checked = False
        CheckEdit163.Checked = False
        CheckEdit164.Checked = False
        CheckEdit165.Checked = False
        CheckEdit166.Checked = False
        CheckEdit167.Checked = False
        CheckEdit168.Checked = False
        CheckEdit169.Checked = False
        CheckEdit170.Checked = False
        CheckEdit171.Checked = False
        CheckEdit172.Checked = False
        CheckEdit173.Checked = False
        CheckEdit174.Checked = False
        CheckEdit175.Checked = False
        CheckEdit176.Checked = False
        CheckEdit177.Checked = False
        CheckEdit178.Checked = False
        CheckEdit179.Checked = False
        CheckEdit180.Checked = False
        CheckEdit181.Checked = False
        CheckEdit182.Checked = False
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
        CheckEdit193.Checked = False
        CheckEdit194.Checked = False
        CheckEdit195.Checked = False
        CheckEdit196.Checked = False
        CheckEdit197.Checked = False
        CheckEdit198.Checked = False
        CheckEdit199.Checked = False
        CheckEdit200.Checked = False
        CheckEdit201.Checked = False
        CheckEdit202.Checked = False
        CheckEdit203.Checked = False
        CheckEdit204.Checked = False
        CheckEdit205.Checked = False
        CheckEdit206.Checked = False
        CheckEdit207.Checked = False
        CheckEdit208.Checked = False
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
        CheckEdit220.Checked = False
        CheckEdit221.Checked = False
        CheckEdit222.Checked = False
        CheckEdit223.Checked = False
        CheckEdit224.Checked = False
        CheckEdit225.Checked = False
        CheckEdit226.Checked = False
        CheckEdit227.Checked = False
        CheckEdit228.Checked = False
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
        CheckEdit240.Checked = False
        CheckEdit241.Checked = False
        CheckEdit242.Checked = False
        CheckEdit243.Checked = False
        CheckEdit244.Checked = False
        CheckEdit245.Checked = False
        CheckEdit246.Checked = False
        CheckEdit247.Checked = False
        CheckEdit248.Checked = False
        CheckEdit249.Checked = False
        CheckEdit250.Checked = False
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
        CheckEdit261.Checked = False
        CheckEdit262.Checked = False
        CheckEdit263.Checked = False
        CheckEdit264.Checked = False
        CheckEdit265.Checked = False
        CheckEdit266.Checked = False
        CheckEdit267.Checked = False
        CheckEdit268.Checked = False
        CheckEdit269.Checked = False
        CheckEdit270.Checked = False
        CheckEdit271.Checked = False
        CheckEdit272.Checked = False
        CheckEdit273.Checked = False
        CheckEdit274.Checked = False
        CheckEdit275.Checked = False
        CheckEdit276.Checked = False
        CheckEdit277.Checked = False
        CheckEdit278.Checked = False
        CheckEdit279.Checked = False
        CheckEdit280.Checked = False
        CheckEdit281.Checked = False
        CheckEdit282.Checked = False
        CheckEdit283.Checked = False
        CheckEdit284.Checked = False
        CheckEdit285.Checked = False
        CheckEdit286.Checked = False


    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RI_44.GetData(txtNoRegister.Text)

            With ds
                grdKDDOCTOR2.EditValue = .KDDOCTOR2
                deDATE.DateTime = .DATE
                MemoEdit1.Text = .ASESMEN_01
                MemoEdit2.Text = .ASESMEN_02
                MemoEdit3.Text = .ASESMEN_03
                TextEdit1.Text = .ASESMEN_04
                TextEdit2.Text = .ASESMEN_05
                TextEdit3.Text = .ASESMEN_06
                TextEdit4.Text = .ASESMEN_07
                TextEdit5.Text = .ASESMEN_08
                TextEdit6.Text = .ASESMEN_09
                TextEdit7.Text = .ASESMEN_10
                TextEdit8.Text = .ASESMEN_11
                TextEdit9.Text = .ASESMEN_12
                TextEdit10.Text = .ASESMEN_13
                TextEdit11.Text = .ASESMEN_14
                TextEdit12.Text = .ASESMEN_15
                TextEdit13.Text = .ASESMEN_16
                TextEdit14.Text = .ASESMEN_17
                TextEdit15.Text = .ASESMEN_18
                TextEdit16.Text = .ASESMEN_19
                TextEdit17.Text = .ASESMEN_20
                TextEdit18.Text = .ASESMEN_21
                TextEdit19.Text = .ASESMEN_22
                TextEdit20.Text = .ASESMEN_23
                TextEdit21.Text = .ASESMEN_24
                TextEdit22.Text = .ASESMEN_25
                TextEdit23.Text = .ASESMEN_26
                TextEdit24.Text = .ASESMEN_27
                TextEdit25.Text = .ASESMEN_28
                TextEdit26.Text = .ASESMEN_29
                TextEdit27.Text = .ASESMEN_30
                TextEdit28.Text = .ASESMEN_31
                TextEdit29.Text = .ASESMEN_32
                TextEdit30.Text = .ASESMEN_33
                TextEdit31.Text = .ASESMEN_34
                TextEdit32.Text = .ASESMEN_35
                TextEdit33.Text = .ASESMEN_36
                TextEdit34.Text = .ASESMEN_37
                TextEdit35.Text = .ASESMEN_38
                TextEdit36.Text = .ASESMEN_39
                TextEdit37.Text = .ASESMEN_40
                TextEdit38.Text = .ASESMEN_41
                TextEdit39.Text = .ASESMEN_42
                TextEdit40.Text = .ASESMEN_43
                TextEdit41.Text = .ASESMEN_44
                TextEdit42.Text = .ASESMEN_45
                TextEdit43.Text = .ASESMEN_46
                TextEdit44.Text = .ASESMEN_47
                TextEdit45.Text = .ASESMEN_48
                TextEdit46.Text = .ASESMEN_49
                TextEdit47.Text = .ASESMEN_50
                TextEdit48.Text = .ASESMEN_51
                TextEdit49.Text = .ASESMEN_52
                TextEdit50.Text = .ASESMEN_53
                TextEdit51.Text = .ASESMEN_54
                TextEdit52.Text = .ASESMEN_55
                TextEdit53.Text = .ASESMEN_56
                TextEdit54.Text = .ASESMEN_57
                TextEdit55.Text = .ASESMEN_58
                TextEdit56.Text = .ASESMEN_59
                TextEdit57.Text = .ASESMEN_60
                TextEdit58.Text = .ASESMEN_61
                TextEdit59.Text = .ASESMEN_62
                TextEdit60.Text = .ASESMEN_63
                TextEdit61.Text = .ASESMEN_64
                TextEdit62.Text = .ASESMEN_65
                TextEdit63.Text = .ASESMEN_66
                TextEdit64.Text = .ASESMEN_67
                TextEdit65.Text = .ASESMEN_68
                TextEdit66.Text = .ASESMEN_69
                TextEdit67.Text = .ASESMEN_70
                TextEdit68.Text = .ASESMEN_71
                TextEdit69.Text = .ASESMEN_72
                TextEdit70.Text = .ASESMEN_73
                TextEdit71.Text = .ASESMEN_74
                TextEdit72.Text = .ASESMEN_75
                TextEdit73.Text = .ASESMEN_76
                TextEdit74.Text = .ASESMEN_77
                TextEdit75.Text = .ASESMEN_78
                TextEdit76.Text = .ASESMEN_79
                TextEdit77.Text = .ASESMEN_80
                TextEdit78.Text = .ASESMEN_81
                TextEdit79.Text = .ASESMEN_82
                TextEdit80.Text = .ASESMEN_83
                TextEdit81.Text = .ASESMEN_84
                TextEdit82.Text = .ASESMEN_85
                TextEdit83.Text = .ASESMEN_86
                TextEdit84.Text = .ASESMEN_87
                TextEdit85.Text = .ASESMEN_88
                TextEdit86.Text = .ASESMEN_89
                TextEdit87.Text = .ASESMEN_90
                TextEdit88.Text = .ASESMEN_91
                TextEdit89.Text = .ASESMEN_92
                TextEdit90.Text = .ASESMEN_93
                TextEdit91.Text = .ASESMEN_94
                TextEdit92.Text = .ASESMEN_95
                TextEdit93.Text = .ASESMEN_96
                TextEdit94.Text = .ASESMEN_97
                TextEdit95.Text = .ASESMEN_98
                TextEdit96.Text = .ASESMEN_99
                TextEdit97.Text = .ASESMEN_100
                TextEdit98.Text = .ASESMEN_101
                TextEdit99.Text = .ASESMEN_102
                TextEdit100.Text = .ASESMEN_103
                TextEdit101.Text = .ASESMEN_104
                TextEdit102.Text = .ASESMEN_105
                TextEdit103.Text = .ASESMEN_106
                TextEdit104.Text = .ASESMEN_107
                TextEdit105.Text = .ASESMEN_108
                TextEdit106.Text = .ASESMEN_109
                TextEdit107.Text = .ASESMEN_110
                TextEdit108.Text = .ASESMEN_111
                TextEdit109.Text = .ASESMEN_112
                TextEdit110.Text = .ASESMEN_113
                TextEdit111.Text = .ASESMEN_114
                TextEdit112.Text = .ASESMEN_115
                TextEdit113.Text = .ASESMEN_116
                TextEdit114.Text = .ASESMEN_117
                TextEdit115.Text = .ASESMEN_118
                TextEdit116.Text = .ASESMEN_119
                TextEdit117.Text = .ASESMEN_120
                TextEdit118.Text = .ASESMEN_121
                TextEdit119.Text = .ASESMEN_122
                TextEdit120.Text = .ASESMEN_123
                TextEdit121.Text = .ASESMEN_124
                TextEdit122.Text = .ASESMEN_125
                TextEdit123.Text = .ASESMEN_126
                TextEdit124.Text = .ASESMEN_127
                TextEdit125.Text = .ASESMEN_128
                TextEdit126.Text = .ASESMEN_129
                TextEdit127.Text = .ASESMEN_130
                TextEdit128.Text = .ASESMEN_131
                TextEdit129.Text = .ASESMEN_132
                TextEdit130.Text = .ASESMEN_133
                TextEdit131.Text = .ASESMEN_134
                TextEdit132.Text = .ASESMEN_135
                TextEdit133.Text = .ASESMEN_136
                TextEdit134.Text = .ASESMEN_137
                TextEdit135.Text = .ASESMEN_138
                TextEdit136.Text = .ASESMEN_139
                TextEdit137.Text = .ASESMEN_140
                TextEdit138.Text = .ASESMEN_141
                TextEdit139.Text = .ASESMEN_142
                TextEdit140.Text = .ASESMEN_143
                TextEdit141.Text = .ASESMEN_144
                TextEdit142.Text = .ASESMEN_145
                TextEdit143.Text = .ASESMEN_146
                TextEdit144.Text = .ASESMEN_147
                TextEdit145.Text = .ASESMEN_148
                TextEdit146.Text = .ASESMEN_149
                TextEdit147.Text = .ASESMEN_150
                TextEdit148.Text = .ASESMEN_151
                TextEdit149.Text = .ASESMEN_152
                TextEdit150.Text = .ASESMEN_153
                TextEdit151.Text = .ASESMEN_154
                TextEdit152.Text = .ASESMEN_155
                TextEdit153.Text = .ASESMEN_156
                TextEdit154.Text = .ASESMEN_157
                TextEdit155.Text = .ASESMEN_158
                TextEdit156.Text = .ASESMEN_159
                TextEdit157.Text = .ASESMEN_160
                TextEdit158.Text = .ASESMEN_161
                TextEdit159.Text = .ASESMEN_162
                TextEdit160.Text = .ASESMEN_163
                TextEdit161.Text = .ASESMEN_164
                TextEdit162.Text = .ASESMEN_165
                TextEdit163.Text = .ASESMEN_166
                TextEdit164.Text = .ASESMEN_167
                TextEdit165.Text = .ASESMEN_168
                TextEdit166.Text = .ASESMEN_169
                TextEdit167.Text = .ASESMEN_170
                TextEdit168.Text = .ASESMEN_171
                TextEdit169.Text = .ASESMEN_172
                TextEdit170.Text = .ASESMEN_173
                TextEdit171.Text = .ASESMEN_174
                TextEdit172.Text = .ASESMEN_175
                TextEdit173.Text = .ASESMEN_176
                TextEdit174.Text = .ASESMEN_177
                TextEdit175.Text = .ASESMEN_178
                TextEdit176.Text = .ASESMEN_179
                TextEdit177.Text = .ASESMEN_180
                TextEdit178.Text = .ASESMEN_181
                TextEdit179.Text = .ASESMEN_182
                TextEdit180.Text = .ASESMEN_183
                TextEdit181.Text = .ASESMEN_184
                TextEdit182.Text = .ASESMEN_185
                TextEdit183.Text = .ASESMEN_186
                TextEdit184.Text = .ASESMEN_187
                TextEdit185.Text = .ASESMEN_188
                TextEdit186.Text = .ASESMEN_189
                TextEdit187.Text = .ASESMEN_190
                TextEdit188.Text = .ASESMEN_191
                TextEdit189.Text = .ASESMEN_192
                TextEdit190.Text = .ASESMEN_193
                TextEdit191.Text = .ASESMEN_194
                TextEdit192.Text = .ASESMEN_195
                TextEdit193.Text = .ASESMEN_196
                TextEdit194.Text = .ASESMEN_197
                TextEdit195.Text = .ASESMEN_198
                TextEdit196.Text = .ASESMEN_199
                TextEdit197.Text = .ASESMEN_200
                TextEdit198.Text = .ASESMEN_201
                TextEdit199.Text = .ASESMEN_202
                TextEdit200.Text = .ASESMEN_203
                TextEdit201.Text = .ASESMEN_204
                TextEdit202.Text = .ASESMEN_205
                TextEdit203.Text = .ASESMEN_206
                TextEdit204.Text = .ASESMEN_207
                TextEdit205.Text = .ASESMEN_208
                TextEdit206.Text = .ASESMEN_209
                TextEdit207.Text = .ASESMEN_210
                TextEdit208.Text = .ASESMEN_211
                TextEdit209.Text = .ASESMEN_212
                TextEdit210.Text = .ASESMEN_213
                TextEdit211.Text = .ASESMEN_214
                TextEdit212.Text = .ASESMEN_215
                TextEdit213.Text = .ASESMEN_216
                TextEdit214.Text = .ASESMEN_217
                TextEdit215.Text = .ASESMEN_218
                TextEdit216.Text = .ASESMEN_219
                TextEdit217.Text = .ASESMEN_220
                TextEdit218.Text = .ASESMEN_221
                TextEdit219.Text = .ASESMEN_222
                TextEdit220.Text = .ASESMEN_223
                TextEdit221.Text = .ASESMEN_224
                TextEdit222.Text = .ASESMEN_225
                TextEdit223.Text = .ASESMEN_226
                TextEdit224.Text = .ASESMEN_227
                TextEdit225.Text = .ASESMEN_228
                TextEdit226.Text = .ASESMEN_229
                TextEdit227.Text = .ASESMEN_230
                TextEdit228.Text = .ASESMEN_231
                TextEdit229.Text = .ASESMEN_232
                TextEdit230.Text = .ASESMEN_233
                TextEdit231.Text = .ASESMEN_234
                TextEdit232.Text = .ASESMEN_235
                TextEdit233.Text = .ASESMEN_236
                TextEdit234.Text = .ASESMEN_237
                TextEdit235.Text = .ASESMEN_238
                TextEdit236.Text = .ASESMEN_239
                TextEdit237.Text = .ASESMEN_240
                TextEdit238.Text = .ASESMEN_241
                TextEdit239.Text = .ASESMEN_242
                TextEdit240.Text = .ASESMEN_243
                TextEdit241.Text = .ASESMEN_244
                TextEdit242.Text = .ASESMEN_245
                TextEdit243.Text = .ASESMEN_246
                TextEdit244.Text = .ASESMEN_247
                TextEdit245.Text = .ASESMEN_248
                TextEdit246.Text = .ASESMEN_249
                TextEdit247.Text = .ASESMEN_250
                TextEdit248.Text = .ASESMEN_251
                TextEdit249.Text = .ASESMEN_252
                TextEdit250.Text = .ASESMEN_253
                TextEdit251.Text = .ASESMEN_254
                TextEdit252.Text = .ASESMEN_255
                TextEdit253.Text = .ASESMEN_256
                TextEdit254.Text = .ASESMEN_257
                TextEdit255.Text = .ASESMEN_258
                TextEdit256.Text = .ASESMEN_259
                TextEdit257.Text = .ASESMEN_260
                TextEdit258.Text = .ASESMEN_261
                TextEdit259.Text = .ASESMEN_262
                TextEdit260.Text = .ASESMEN_263
                TextEdit261.Text = .ASESMEN_264
                TextEdit262.Text = .ASESMEN_265
                TextEdit263.Text = .ASESMEN_266
                TextEdit264.Text = .ASESMEN_267
                TextEdit265.Text = .ASESMEN_268
                TextEdit266.Text = .ASESMEN_269
                TextEdit267.Text = .ASESMEN_270
                TextEdit268.Text = .ASESMEN_271
                TextEdit269.Text = .ASESMEN_272
                TextEdit270.Text = .ASESMEN_273
                TextEdit271.Text = .ASESMEN_274
                TextEdit272.Text = .ASESMEN_275
                TextEdit273.Text = .ASESMEN_276
                TextEdit274.Text = .ASESMEN_277
                TextEdit275.Text = .ASESMEN_278
                CheckEdit1.Checked = .ASESMEN_279
                CheckEdit2.Checked = .ASESMEN_280
                CheckEdit3.Checked = .ASESMEN_281
                CheckEdit4.Checked = .ASESMEN_282
                CheckEdit5.Checked = .ASESMEN_283
                CheckEdit6.Checked = .ASESMEN_284
                CheckEdit7.Checked = .ASESMEN_285
                CheckEdit8.Checked = .ASESMEN_286
                CheckEdit9.Checked = .ASESMEN_287
                CheckEdit10.Checked = .ASESMEN_288
                CheckEdit11.Checked = .ASESMEN_289
                CheckEdit12.Checked = .ASESMEN_290
                CheckEdit13.Checked = .ASESMEN_291
                CheckEdit14.Checked = .ASESMEN_292
                CheckEdit15.Checked = .ASESMEN_293
                CheckEdit16.Checked = .ASESMEN_294
                CheckEdit17.Checked = .ASESMEN_295
                CheckEdit18.Checked = .ASESMEN_296
                CheckEdit19.Checked = .ASESMEN_297
                CheckEdit20.Checked = .ASESMEN_298
                CheckEdit21.Checked = .ASESMEN_299
                CheckEdit22.Checked = .ASESMEN_300
                CheckEdit23.Checked = .ASESMEN_301
                CheckEdit24.Checked = .ASESMEN_302
                CheckEdit25.Checked = .ASESMEN_303
                CheckEdit26.Checked = .ASESMEN_304
                CheckEdit27.Checked = .ASESMEN_305
                CheckEdit28.Checked = .ASESMEN_306
                CheckEdit29.Checked = .ASESMEN_307
                CheckEdit30.Checked = .ASESMEN_308
                CheckEdit31.Checked = .ASESMEN_309
                CheckEdit32.Checked = .ASESMEN_310
                CheckEdit33.Checked = .ASESMEN_311
                CheckEdit34.Checked = .ASESMEN_312
                CheckEdit35.Checked = .ASESMEN_313
                CheckEdit36.Checked = .ASESMEN_314
                CheckEdit37.Checked = .ASESMEN_315
                CheckEdit38.Checked = .ASESMEN_316
                CheckEdit39.Checked = .ASESMEN_317
                CheckEdit40.Checked = .ASESMEN_318
                CheckEdit41.Checked = .ASESMEN_319
                CheckEdit42.Checked = .ASESMEN_320
                CheckEdit43.Checked = .ASESMEN_321
                CheckEdit44.Checked = .ASESMEN_322
                CheckEdit45.Checked = .ASESMEN_323
                CheckEdit46.Checked = .ASESMEN_324
                CheckEdit47.Checked = .ASESMEN_325
                CheckEdit48.Checked = .ASESMEN_326
                CheckEdit49.Checked = .ASESMEN_327
                CheckEdit50.Checked = .ASESMEN_328
                CheckEdit51.Checked = .ASESMEN_329
                CheckEdit52.Checked = .ASESMEN_330
                CheckEdit53.Checked = .ASESMEN_331
                CheckEdit54.Checked = .ASESMEN_332
                CheckEdit55.Checked = .ASESMEN_333
                CheckEdit56.Checked = .ASESMEN_334
                CheckEdit57.Checked = .ASESMEN_335
                CheckEdit58.Checked = .ASESMEN_336
                CheckEdit59.Checked = .ASESMEN_337
                CheckEdit60.Checked = .ASESMEN_338
                CheckEdit61.Checked = .ASESMEN_339
                CheckEdit62.Checked = .ASESMEN_340
                CheckEdit63.Checked = .ASESMEN_341
                CheckEdit64.Checked = .ASESMEN_342
                CheckEdit65.Checked = .ASESMEN_343
                CheckEdit66.Checked = .ASESMEN_344
                CheckEdit67.Checked = .ASESMEN_345
                CheckEdit68.Checked = .ASESMEN_346
                CheckEdit69.Checked = .ASESMEN_347
                CheckEdit70.Checked = .ASESMEN_348
                CheckEdit71.Checked = .ASESMEN_349
                CheckEdit72.Checked = .ASESMEN_350
                CheckEdit73.Checked = .ASESMEN_351
                CheckEdit74.Checked = .ASESMEN_352
                CheckEdit75.Checked = .ASESMEN_353
                CheckEdit76.Checked = .ASESMEN_354
                CheckEdit77.Checked = .ASESMEN_355
                CheckEdit78.Checked = .ASESMEN_356
                CheckEdit79.Checked = .ASESMEN_357
                CheckEdit80.Checked = .ASESMEN_358
                CheckEdit81.Checked = .ASESMEN_359
                CheckEdit82.Checked = .ASESMEN_360
                CheckEdit83.Checked = .ASESMEN_361
                CheckEdit84.Checked = .ASESMEN_362
                CheckEdit85.Checked = .ASESMEN_363
                CheckEdit86.Checked = .ASESMEN_364
                CheckEdit87.Checked = .ASESMEN_365
                CheckEdit88.Checked = .ASESMEN_366
                CheckEdit89.Checked = .ASESMEN_367
                CheckEdit90.Checked = .ASESMEN_368
                CheckEdit91.Checked = .ASESMEN_369
                CheckEdit92.Checked = .ASESMEN_370
                CheckEdit93.Checked = .ASESMEN_371
                CheckEdit94.Checked = .ASESMEN_372
                CheckEdit95.Checked = .ASESMEN_373
                CheckEdit96.Checked = .ASESMEN_374
                CheckEdit97.Checked = .ASESMEN_375
                CheckEdit98.Checked = .ASESMEN_376
                CheckEdit99.Checked = .ASESMEN_377
                CheckEdit100.Checked = .ASESMEN_378
                CheckEdit101.Checked = .ASESMEN_379
                CheckEdit102.Checked = .ASESMEN_380
                CheckEdit103.Checked = .ASESMEN_381
                CheckEdit104.Checked = .ASESMEN_382
                CheckEdit105.Checked = .ASESMEN_383
                CheckEdit106.Checked = .ASESMEN_384
                CheckEdit107.Checked = .ASESMEN_385
                CheckEdit108.Checked = .ASESMEN_386
                CheckEdit109.Checked = .ASESMEN_387
                CheckEdit110.Checked = .ASESMEN_388
                CheckEdit111.Checked = .ASESMEN_389
                CheckEdit112.Checked = .ASESMEN_390
                CheckEdit113.Checked = .ASESMEN_391
                CheckEdit114.Checked = .ASESMEN_392
                CheckEdit115.Checked = .ASESMEN_393
                CheckEdit116.Checked = .ASESMEN_394
                CheckEdit117.Checked = .ASESMEN_395
                CheckEdit118.Checked = .ASESMEN_396
                CheckEdit119.Checked = .ASESMEN_397
                CheckEdit120.Checked = .ASESMEN_398
                CheckEdit121.Checked = .ASESMEN_399
                CheckEdit122.Checked = .ASESMEN_400
                CheckEdit123.Checked = .ASESMEN_401
                CheckEdit124.Checked = .ASESMEN_402
                CheckEdit125.Checked = .ASESMEN_403
                CheckEdit126.Checked = .ASESMEN_404
                CheckEdit127.Checked = .ASESMEN_405
                CheckEdit128.Checked = .ASESMEN_406
                CheckEdit129.Checked = .ASESMEN_407
                CheckEdit130.Checked = .ASESMEN_408
                CheckEdit131.Checked = .ASESMEN_409
                CheckEdit132.Checked = .ASESMEN_410
                CheckEdit133.Checked = .ASESMEN_411
                CheckEdit134.Checked = .ASESMEN_412
                CheckEdit135.Checked = .ASESMEN_413
                CheckEdit136.Checked = .ASESMEN_414
                CheckEdit137.Checked = .ASESMEN_415
                CheckEdit138.Checked = .ASESMEN_416
                CheckEdit139.Checked = .ASESMEN_417
                CheckEdit140.Checked = .ASESMEN_418
                CheckEdit141.Checked = .ASESMEN_419
                CheckEdit142.Checked = .ASESMEN_420
                CheckEdit143.Checked = .ASESMEN_421
                CheckEdit144.Checked = .ASESMEN_422
                CheckEdit145.Checked = .ASESMEN_423
                CheckEdit146.Checked = .ASESMEN_424
                CheckEdit147.Checked = .ASESMEN_425
                CheckEdit148.Checked = .ASESMEN_426
                CheckEdit149.Checked = .ASESMEN_427
                CheckEdit150.Checked = .ASESMEN_428
                CheckEdit151.Checked = .ASESMEN_429
                CheckEdit152.Checked = .ASESMEN_430
                CheckEdit153.Checked = .ASESMEN_431
                CheckEdit154.Checked = .ASESMEN_432
                CheckEdit155.Checked = .ASESMEN_433
                CheckEdit156.Checked = .ASESMEN_434
                CheckEdit157.Checked = .ASESMEN_435
                CheckEdit158.Checked = .ASESMEN_436
                CheckEdit159.Checked = .ASESMEN_437
                CheckEdit160.Checked = .ASESMEN_438
                CheckEdit161.Checked = .ASESMEN_439
                CheckEdit162.Checked = .ASESMEN_440
                CheckEdit163.Checked = .ASESMEN_441
                CheckEdit164.Checked = .ASESMEN_442
                CheckEdit165.Checked = .ASESMEN_443
                CheckEdit166.Checked = .ASESMEN_444
                CheckEdit167.Checked = .ASESMEN_445
                CheckEdit168.Checked = .ASESMEN_446
                CheckEdit169.Checked = .ASESMEN_447
                CheckEdit170.Checked = .ASESMEN_448
                CheckEdit171.Checked = .ASESMEN_449
                CheckEdit172.Checked = .ASESMEN_450
                CheckEdit173.Checked = .ASESMEN_451
                CheckEdit174.Checked = .ASESMEN_452
                CheckEdit175.Checked = .ASESMEN_453
                CheckEdit176.Checked = .ASESMEN_454
                CheckEdit177.Checked = .ASESMEN_455
                CheckEdit178.Checked = .ASESMEN_456
                CheckEdit179.Checked = .ASESMEN_457
                CheckEdit180.Checked = .ASESMEN_458
                CheckEdit181.Checked = .ASESMEN_459
                CheckEdit182.Checked = .ASESMEN_460
                CheckEdit183.Checked = .ASESMEN_461
                CheckEdit184.Checked = .ASESMEN_462
                CheckEdit185.Checked = .ASESMEN_463
                CheckEdit186.Checked = .ASESMEN_464
                CheckEdit187.Checked = .ASESMEN_465
                CheckEdit188.Checked = .ASESMEN_466
                CheckEdit189.Checked = .ASESMEN_467
                CheckEdit190.Checked = .ASESMEN_468
                CheckEdit191.Checked = .ASESMEN_469
                CheckEdit192.Checked = .ASESMEN_470
                CheckEdit193.Checked = .ASESMEN_471
                CheckEdit194.Checked = .ASESMEN_472
                CheckEdit195.Checked = .ASESMEN_473
                CheckEdit196.Checked = .ASESMEN_474
                CheckEdit197.Checked = .ASESMEN_475
                CheckEdit198.Checked = .ASESMEN_476
                CheckEdit199.Checked = .ASESMEN_477
                CheckEdit200.Checked = .ASESMEN_478
                CheckEdit201.Checked = .ASESMEN_479
                CheckEdit202.Checked = .ASESMEN_480
                CheckEdit203.Checked = .ASESMEN_481
                CheckEdit204.Checked = .ASESMEN_482
                CheckEdit205.Checked = .ASESMEN_483
                CheckEdit206.Checked = .ASESMEN_484
                CheckEdit207.Checked = .ASESMEN_485
                CheckEdit208.Checked = .ASESMEN_486
                CheckEdit209.Checked = .ASESMEN_487
                CheckEdit210.Checked = .ASESMEN_488
                CheckEdit211.Checked = .ASESMEN_489
                CheckEdit212.Checked = .ASESMEN_490
                CheckEdit213.Checked = .ASESMEN_491
                CheckEdit214.Checked = .ASESMEN_492
                CheckEdit215.Checked = .ASESMEN_493
                CheckEdit216.Checked = .ASESMEN_494
                CheckEdit217.Checked = .ASESMEN_495
                CheckEdit218.Checked = .ASESMEN_496
                CheckEdit219.Checked = .ASESMEN_497
                CheckEdit220.Checked = .ASESMEN_498
                CheckEdit221.Checked = .ASESMEN_499
                CheckEdit222.Checked = .ASESMEN_500
                CheckEdit223.Checked = .ASESMEN_501
                CheckEdit224.Checked = .ASESMEN_502
                CheckEdit225.Checked = .ASESMEN_503
                CheckEdit226.Checked = .ASESMEN_504
                CheckEdit227.Checked = .ASESMEN_505
                CheckEdit228.Checked = .ASESMEN_506
                CheckEdit229.Checked = .ASESMEN_507
                CheckEdit230.Checked = .ASESMEN_508
                CheckEdit231.Checked = .ASESMEN_509
                CheckEdit232.Checked = .ASESMEN_510
                CheckEdit233.Checked = .ASESMEN_511
                CheckEdit234.Checked = .ASESMEN_512
                CheckEdit235.Checked = .ASESMEN_513
                CheckEdit236.Checked = .ASESMEN_514
                CheckEdit237.Checked = .ASESMEN_515
                CheckEdit238.Checked = .ASESMEN_516
                CheckEdit239.Checked = .ASESMEN_517
                CheckEdit240.Checked = .ASESMEN_518
                CheckEdit241.Checked = .ASESMEN_519
                CheckEdit242.Checked = .ASESMEN_520
                CheckEdit243.Checked = .ASESMEN_521
                CheckEdit244.Checked = .ASESMEN_522
                CheckEdit245.Checked = .ASESMEN_523
                CheckEdit246.Checked = .ASESMEN_524
                CheckEdit247.Checked = .ASESMEN_525
                CheckEdit248.Checked = .ASESMEN_526
                CheckEdit249.Checked = .ASESMEN_527
                CheckEdit250.Checked = .ASESMEN_528
                CheckEdit251.Checked = .ASESMEN_529
                CheckEdit252.Checked = .ASESMEN_530
                CheckEdit253.Checked = .ASESMEN_531
                CheckEdit254.Checked = .ASESMEN_532
                CheckEdit255.Checked = .ASESMEN_533
                CheckEdit256.Checked = .ASESMEN_534
                CheckEdit257.Checked = .ASESMEN_535
                CheckEdit258.Checked = .ASESMEN_536
                CheckEdit259.Checked = .ASESMEN_537
                CheckEdit260.Checked = .ASESMEN_538
                CheckEdit261.Checked = .ASESMEN_539
                CheckEdit262.Checked = .ASESMEN_540
                CheckEdit263.Checked = .ASESMEN_541
                CheckEdit264.Checked = .ASESMEN_542
                CheckEdit265.Checked = .ASESMEN_543
                CheckEdit266.Checked = .ASESMEN_544
                CheckEdit267.Checked = .ASESMEN_545
                CheckEdit268.Checked = .ASESMEN_546
                CheckEdit269.Checked = .ASESMEN_547
                CheckEdit270.Checked = .ASESMEN_548
                CheckEdit271.Checked = .ASESMEN_549
                CheckEdit272.Checked = .ASESMEN_550
                CheckEdit273.Checked = .ASESMEN_551
                CheckEdit274.Checked = .ASESMEN_552
                CheckEdit275.Checked = .ASESMEN_553
                CheckEdit276.Checked = .ASESMEN_554
                CheckEdit277.Checked = .ASESMEN_555
                CheckEdit278.Checked = .ASESMEN_556
                CheckEdit279.Checked = .ASESMEN_557
                CheckEdit280.Checked = .ASESMEN_558
                CheckEdit281.Checked = .ASESMEN_559
                CheckEdit282.Checked = .ASESMEN_560
                CheckEdit283.Checked = .ASESMEN_561
                CheckEdit284.Checked = .ASESMEN_562
                CheckEdit285.Checked = .ASESMEN_563
                CheckEdit286.Checked = .ASESMEN_564

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
            Dim ds = oS_DIGITAL_RI_44.GetStructureHeader
            With ds
                .KDKUNJUNGAN = txtNoRegister.Text
                Try
                    .DATECREATED = oS_DIGITAL_RI_44.GetData(txtNoRegister.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDDOCTOR2 = grdKDDOCTOR2.EditValue
                .NMDOCTOR2 = grdKDDOCTOR2.Text
                .DATE = deDATE.DateTime
                .ASESMEN_01 = MemoEdit1.Text
                .ASESMEN_02 = MemoEdit2.Text
                .ASESMEN_03 = MemoEdit3.Text
                .ASESMEN_04 = TextEdit1.Text
                .ASESMEN_05 = TextEdit2.Text
                .ASESMEN_06 = TextEdit3.Text
                .ASESMEN_07 = TextEdit4.Text
                .ASESMEN_08 = TextEdit5.Text
                .ASESMEN_09 = TextEdit6.Text
                .ASESMEN_10 = TextEdit7.Text
                .ASESMEN_11 = TextEdit8.Text
                .ASESMEN_12 = TextEdit9.Text
                .ASESMEN_13 = TextEdit10.Text
                .ASESMEN_14 = TextEdit11.Text
                .ASESMEN_15 = TextEdit12.Text
                .ASESMEN_16 = TextEdit13.Text
                .ASESMEN_17 = TextEdit14.Text
                .ASESMEN_18 = TextEdit15.Text
                .ASESMEN_19 = TextEdit16.Text
                .ASESMEN_20 = TextEdit17.Text
                .ASESMEN_21 = TextEdit18.Text
                .ASESMEN_22 = TextEdit19.Text
                .ASESMEN_23 = TextEdit20.Text
                .ASESMEN_24 = TextEdit21.Text
                .ASESMEN_25 = TextEdit22.Text
                .ASESMEN_26 = TextEdit23.Text
                .ASESMEN_27 = TextEdit24.Text
                .ASESMEN_28 = TextEdit25.Text
                .ASESMEN_29 = TextEdit26.Text
                .ASESMEN_30 = TextEdit27.Text
                .ASESMEN_31 = TextEdit28.Text
                .ASESMEN_32 = TextEdit29.Text
                .ASESMEN_33 = TextEdit30.Text
                .ASESMEN_34 = TextEdit31.Text
                .ASESMEN_35 = TextEdit32.Text
                .ASESMEN_36 = TextEdit33.Text
                .ASESMEN_37 = TextEdit34.Text
                .ASESMEN_38 = TextEdit35.Text
                .ASESMEN_39 = TextEdit36.Text
                .ASESMEN_40 = TextEdit37.Text
                .ASESMEN_41 = TextEdit38.Text
                .ASESMEN_42 = TextEdit39.Text
                .ASESMEN_43 = TextEdit40.Text
                .ASESMEN_44 = TextEdit41.Text
                .ASESMEN_45 = TextEdit42.Text
                .ASESMEN_46 = TextEdit43.Text
                .ASESMEN_47 = TextEdit44.Text
                .ASESMEN_48 = TextEdit45.Text
                .ASESMEN_49 = TextEdit46.Text
                .ASESMEN_50 = TextEdit47.Text
                .ASESMEN_51 = TextEdit48.Text
                .ASESMEN_52 = TextEdit49.Text
                .ASESMEN_53 = TextEdit50.Text
                .ASESMEN_54 = TextEdit51.Text
                .ASESMEN_55 = TextEdit52.Text
                .ASESMEN_56 = TextEdit53.Text
                .ASESMEN_57 = TextEdit54.Text
                .ASESMEN_58 = TextEdit55.Text
                .ASESMEN_59 = TextEdit56.Text
                .ASESMEN_60 = TextEdit57.Text
                .ASESMEN_61 = TextEdit58.Text
                .ASESMEN_62 = TextEdit59.Text
                .ASESMEN_63 = TextEdit60.Text
                .ASESMEN_64 = TextEdit61.Text
                .ASESMEN_65 = TextEdit62.Text
                .ASESMEN_66 = TextEdit63.Text
                .ASESMEN_67 = TextEdit64.Text
                .ASESMEN_68 = TextEdit65.Text
                .ASESMEN_69 = TextEdit66.Text
                .ASESMEN_70 = TextEdit67.Text
                .ASESMEN_71 = TextEdit68.Text
                .ASESMEN_72 = TextEdit69.Text
                .ASESMEN_73 = TextEdit70.Text
                .ASESMEN_74 = TextEdit71.Text
                .ASESMEN_75 = TextEdit72.Text
                .ASESMEN_76 = TextEdit73.Text
                .ASESMEN_77 = TextEdit74.Text
                .ASESMEN_78 = TextEdit75.Text
                .ASESMEN_79 = TextEdit76.Text
                .ASESMEN_80 = TextEdit77.Text
                .ASESMEN_81 = TextEdit78.Text
                .ASESMEN_82 = TextEdit79.Text
                .ASESMEN_83 = TextEdit80.Text
                .ASESMEN_84 = TextEdit81.Text
                .ASESMEN_85 = TextEdit82.Text
                .ASESMEN_86 = TextEdit83.Text
                .ASESMEN_87 = TextEdit84.Text
                .ASESMEN_88 = TextEdit85.Text
                .ASESMEN_89 = TextEdit86.Text
                .ASESMEN_90 = TextEdit87.Text
                .ASESMEN_91 = TextEdit88.Text
                .ASESMEN_92 = TextEdit89.Text
                .ASESMEN_93 = TextEdit90.Text
                .ASESMEN_94 = TextEdit91.Text
                .ASESMEN_95 = TextEdit92.Text
                .ASESMEN_96 = TextEdit93.Text
                .ASESMEN_97 = TextEdit94.Text
                .ASESMEN_98 = TextEdit95.Text
                .ASESMEN_99 = TextEdit96.Text
                .ASESMEN_100 = TextEdit97.Text
                .ASESMEN_101 = TextEdit98.Text
                .ASESMEN_102 = TextEdit99.Text
                .ASESMEN_103 = TextEdit100.Text
                .ASESMEN_104 = TextEdit101.Text
                .ASESMEN_105 = TextEdit102.Text
                .ASESMEN_106 = TextEdit103.Text
                .ASESMEN_107 = TextEdit104.Text
                .ASESMEN_108 = TextEdit105.Text
                .ASESMEN_109 = TextEdit106.Text
                .ASESMEN_110 = TextEdit107.Text
                .ASESMEN_111 = TextEdit108.Text
                .ASESMEN_112 = TextEdit109.Text
                .ASESMEN_113 = TextEdit110.Text
                .ASESMEN_114 = TextEdit111.Text
                .ASESMEN_115 = TextEdit112.Text
                .ASESMEN_116 = TextEdit113.Text
                .ASESMEN_117 = TextEdit114.Text
                .ASESMEN_118 = TextEdit115.Text
                .ASESMEN_119 = TextEdit116.Text
                .ASESMEN_120 = TextEdit117.Text
                .ASESMEN_121 = TextEdit118.Text
                .ASESMEN_122 = TextEdit119.Text
                .ASESMEN_123 = TextEdit120.Text
                .ASESMEN_124 = TextEdit121.Text
                .ASESMEN_125 = TextEdit122.Text
                .ASESMEN_126 = TextEdit123.Text
                .ASESMEN_127 = TextEdit124.Text
                .ASESMEN_128 = TextEdit125.Text
                .ASESMEN_129 = TextEdit126.Text
                .ASESMEN_130 = TextEdit127.Text
                .ASESMEN_131 = TextEdit128.Text
                .ASESMEN_132 = TextEdit129.Text
                .ASESMEN_133 = TextEdit130.Text
                .ASESMEN_134 = TextEdit131.Text
                .ASESMEN_135 = TextEdit132.Text
                .ASESMEN_136 = TextEdit133.Text
                .ASESMEN_137 = TextEdit134.Text
                .ASESMEN_138 = TextEdit135.Text
                .ASESMEN_139 = TextEdit136.Text
                .ASESMEN_140 = TextEdit137.Text
                .ASESMEN_141 = TextEdit138.Text
                .ASESMEN_142 = TextEdit139.Text
                .ASESMEN_143 = TextEdit140.Text
                .ASESMEN_144 = TextEdit141.Text
                .ASESMEN_145 = TextEdit142.Text
                .ASESMEN_146 = TextEdit143.Text
                .ASESMEN_147 = TextEdit144.Text
                .ASESMEN_148 = TextEdit145.Text
                .ASESMEN_149 = TextEdit146.Text
                .ASESMEN_150 = TextEdit147.Text
                .ASESMEN_151 = TextEdit148.Text
                .ASESMEN_152 = TextEdit149.Text
                .ASESMEN_153 = TextEdit150.Text
                .ASESMEN_154 = TextEdit151.Text
                .ASESMEN_155 = TextEdit152.Text
                .ASESMEN_156 = TextEdit153.Text
                .ASESMEN_157 = TextEdit154.Text
                .ASESMEN_158 = TextEdit155.Text
                .ASESMEN_159 = TextEdit156.Text
                .ASESMEN_160 = TextEdit157.Text
                .ASESMEN_161 = TextEdit158.Text
                .ASESMEN_162 = TextEdit159.Text
                .ASESMEN_163 = TextEdit160.Text
                .ASESMEN_164 = TextEdit161.Text
                .ASESMEN_165 = TextEdit162.Text
                .ASESMEN_166 = TextEdit163.Text
                .ASESMEN_167 = TextEdit164.Text
                .ASESMEN_168 = TextEdit165.Text
                .ASESMEN_169 = TextEdit166.Text
                .ASESMEN_170 = TextEdit167.Text
                .ASESMEN_171 = TextEdit168.Text
                .ASESMEN_172 = TextEdit169.Text
                .ASESMEN_173 = TextEdit170.Text
                .ASESMEN_174 = TextEdit171.Text
                .ASESMEN_175 = TextEdit172.Text
                .ASESMEN_176 = TextEdit173.Text
                .ASESMEN_177 = TextEdit174.Text
                .ASESMEN_178 = TextEdit175.Text
                .ASESMEN_179 = TextEdit176.Text
                .ASESMEN_180 = TextEdit177.Text
                .ASESMEN_181 = TextEdit178.Text
                .ASESMEN_182 = TextEdit179.Text
                .ASESMEN_183 = TextEdit180.Text
                .ASESMEN_184 = TextEdit181.Text
                .ASESMEN_185 = TextEdit182.Text
                .ASESMEN_186 = TextEdit183.Text
                .ASESMEN_187 = TextEdit184.Text
                .ASESMEN_188 = TextEdit185.Text
                .ASESMEN_189 = TextEdit186.Text
                .ASESMEN_190 = TextEdit187.Text
                .ASESMEN_191 = TextEdit188.Text
                .ASESMEN_192 = TextEdit189.Text
                .ASESMEN_193 = TextEdit190.Text
                .ASESMEN_194 = TextEdit191.Text
                .ASESMEN_195 = TextEdit192.Text
                .ASESMEN_196 = TextEdit193.Text
                .ASESMEN_197 = TextEdit194.Text
                .ASESMEN_198 = TextEdit195.Text
                .ASESMEN_199 = TextEdit196.Text
                .ASESMEN_200 = TextEdit197.Text
                .ASESMEN_201 = TextEdit198.Text
                .ASESMEN_202 = TextEdit199.Text
                .ASESMEN_203 = TextEdit200.Text
                .ASESMEN_204 = TextEdit201.Text
                .ASESMEN_205 = TextEdit202.Text
                .ASESMEN_206 = TextEdit203.Text
                .ASESMEN_207 = TextEdit204.Text
                .ASESMEN_208 = TextEdit205.Text
                .ASESMEN_209 = TextEdit206.Text
                .ASESMEN_210 = TextEdit207.Text
                .ASESMEN_211 = TextEdit208.Text
                .ASESMEN_212 = TextEdit209.Text
                .ASESMEN_213 = TextEdit210.Text
                .ASESMEN_214 = TextEdit211.Text
                .ASESMEN_215 = TextEdit212.Text
                .ASESMEN_216 = TextEdit213.Text
                .ASESMEN_217 = TextEdit214.Text
                .ASESMEN_218 = TextEdit215.Text
                .ASESMEN_219 = TextEdit216.Text
                .ASESMEN_220 = TextEdit217.Text
                .ASESMEN_221 = TextEdit218.Text
                .ASESMEN_222 = TextEdit219.Text
                .ASESMEN_223 = TextEdit220.Text
                .ASESMEN_224 = TextEdit221.Text
                .ASESMEN_225 = TextEdit222.Text
                .ASESMEN_226 = TextEdit223.Text
                .ASESMEN_227 = TextEdit224.Text
                .ASESMEN_228 = TextEdit225.Text
                .ASESMEN_229 = TextEdit226.Text
                .ASESMEN_230 = TextEdit227.Text
                .ASESMEN_231 = TextEdit228.Text
                .ASESMEN_232 = TextEdit229.Text
                .ASESMEN_233 = TextEdit230.Text
                .ASESMEN_234 = TextEdit231.Text
                .ASESMEN_235 = TextEdit232.Text
                .ASESMEN_236 = TextEdit233.Text
                .ASESMEN_237 = TextEdit234.Text
                .ASESMEN_238 = TextEdit235.Text
                .ASESMEN_239 = TextEdit236.Text
                .ASESMEN_240 = TextEdit237.Text
                .ASESMEN_241 = TextEdit238.Text
                .ASESMEN_242 = TextEdit239.Text
                .ASESMEN_243 = TextEdit240.Text
                .ASESMEN_244 = TextEdit241.Text
                .ASESMEN_245 = TextEdit242.Text
                .ASESMEN_246 = TextEdit243.Text
                .ASESMEN_247 = TextEdit244.Text
                .ASESMEN_248 = TextEdit245.Text
                .ASESMEN_249 = TextEdit246.Text
                .ASESMEN_250 = TextEdit247.Text
                .ASESMEN_251 = TextEdit248.Text
                .ASESMEN_252 = TextEdit249.Text
                .ASESMEN_253 = TextEdit250.Text
                .ASESMEN_254 = TextEdit251.Text
                .ASESMEN_255 = TextEdit252.Text
                .ASESMEN_256 = TextEdit253.Text
                .ASESMEN_257 = TextEdit254.Text
                .ASESMEN_258 = TextEdit255.Text
                .ASESMEN_259 = TextEdit256.Text
                .ASESMEN_260 = TextEdit257.Text
                .ASESMEN_261 = TextEdit258.Text
                .ASESMEN_262 = TextEdit259.Text
                .ASESMEN_263 = TextEdit260.Text
                .ASESMEN_264 = TextEdit261.Text
                .ASESMEN_265 = TextEdit262.Text
                .ASESMEN_266 = TextEdit263.Text
                .ASESMEN_267 = TextEdit264.Text
                .ASESMEN_268 = TextEdit265.Text
                .ASESMEN_269 = TextEdit266.Text
                .ASESMEN_270 = TextEdit267.Text
                .ASESMEN_271 = TextEdit268.Text
                .ASESMEN_272 = TextEdit269.Text
                .ASESMEN_273 = TextEdit270.Text
                .ASESMEN_274 = TextEdit271.Text
                .ASESMEN_275 = TextEdit272.Text
                .ASESMEN_276 = TextEdit273.Text
                .ASESMEN_277 = TextEdit274.Text
                .ASESMEN_278 = TextEdit275.Text
                .ASESMEN_279 = CheckEdit1.Checked
                .ASESMEN_280 = CheckEdit2.Checked
                .ASESMEN_281 = CheckEdit3.Checked
                .ASESMEN_282 = CheckEdit4.Checked
                .ASESMEN_283 = CheckEdit5.Checked
                .ASESMEN_284 = CheckEdit6.Checked
                .ASESMEN_285 = CheckEdit7.Checked
                .ASESMEN_286 = CheckEdit8.Checked
                .ASESMEN_287 = CheckEdit9.Checked
                .ASESMEN_288 = CheckEdit10.Checked
                .ASESMEN_289 = CheckEdit11.Checked
                .ASESMEN_290 = CheckEdit12.Checked
                .ASESMEN_291 = CheckEdit13.Checked
                .ASESMEN_292 = CheckEdit14.Checked
                .ASESMEN_293 = CheckEdit15.Checked
                .ASESMEN_294 = CheckEdit16.Checked
                .ASESMEN_295 = CheckEdit17.Checked
                .ASESMEN_296 = CheckEdit18.Checked
                .ASESMEN_297 = CheckEdit19.Checked
                .ASESMEN_298 = CheckEdit20.Checked
                .ASESMEN_299 = CheckEdit21.Checked
                .ASESMEN_300 = CheckEdit22.Checked
                .ASESMEN_301 = CheckEdit23.Checked
                .ASESMEN_302 = CheckEdit24.Checked
                .ASESMEN_303 = CheckEdit25.Checked
                .ASESMEN_304 = CheckEdit26.Checked
                .ASESMEN_305 = CheckEdit27.Checked
                .ASESMEN_306 = CheckEdit28.Checked
                .ASESMEN_307 = CheckEdit29.Checked
                .ASESMEN_308 = CheckEdit30.Checked
                .ASESMEN_309 = CheckEdit31.Checked
                .ASESMEN_310 = CheckEdit32.Checked
                .ASESMEN_311 = CheckEdit33.Checked
                .ASESMEN_312 = CheckEdit34.Checked
                .ASESMEN_313 = CheckEdit35.Checked
                .ASESMEN_314 = CheckEdit36.Checked
                .ASESMEN_315 = CheckEdit37.Checked
                .ASESMEN_316 = CheckEdit38.Checked
                .ASESMEN_317 = CheckEdit39.Checked
                .ASESMEN_318 = CheckEdit40.Checked
                .ASESMEN_319 = CheckEdit41.Checked
                .ASESMEN_320 = CheckEdit42.Checked
                .ASESMEN_321 = CheckEdit43.Checked
                .ASESMEN_322 = CheckEdit44.Checked
                .ASESMEN_323 = CheckEdit45.Checked
                .ASESMEN_324 = CheckEdit46.Checked
                .ASESMEN_325 = CheckEdit47.Checked
                .ASESMEN_326 = CheckEdit48.Checked
                .ASESMEN_327 = CheckEdit49.Checked
                .ASESMEN_328 = CheckEdit50.Checked
                .ASESMEN_329 = CheckEdit51.Checked
                .ASESMEN_330 = CheckEdit52.Checked
                .ASESMEN_331 = CheckEdit53.Checked
                .ASESMEN_332 = CheckEdit54.Checked
                .ASESMEN_333 = CheckEdit55.Checked
                .ASESMEN_334 = CheckEdit56.Checked
                .ASESMEN_335 = CheckEdit57.Checked
                .ASESMEN_336 = CheckEdit58.Checked
                .ASESMEN_337 = CheckEdit59.Checked
                .ASESMEN_338 = CheckEdit60.Checked
                .ASESMEN_339 = CheckEdit61.Checked
                .ASESMEN_340 = CheckEdit62.Checked
                .ASESMEN_341 = CheckEdit63.Checked
                .ASESMEN_342 = CheckEdit64.Checked
                .ASESMEN_343 = CheckEdit65.Checked
                .ASESMEN_344 = CheckEdit66.Checked
                .ASESMEN_345 = CheckEdit67.Checked
                .ASESMEN_346 = CheckEdit68.Checked
                .ASESMEN_347 = CheckEdit69.Checked
                .ASESMEN_348 = CheckEdit70.Checked
                .ASESMEN_349 = CheckEdit71.Checked
                .ASESMEN_350 = CheckEdit72.Checked
                .ASESMEN_351 = CheckEdit73.Checked
                .ASESMEN_352 = CheckEdit74.Checked
                .ASESMEN_353 = CheckEdit75.Checked
                .ASESMEN_354 = CheckEdit76.Checked
                .ASESMEN_355 = CheckEdit77.Checked
                .ASESMEN_356 = CheckEdit78.Checked
                .ASESMEN_357 = CheckEdit79.Checked
                .ASESMEN_358 = CheckEdit80.Checked
                .ASESMEN_359 = CheckEdit81.Checked
                .ASESMEN_360 = CheckEdit82.Checked
                .ASESMEN_361 = CheckEdit83.Checked
                .ASESMEN_362 = CheckEdit84.Checked
                .ASESMEN_363 = CheckEdit85.Checked
                .ASESMEN_364 = CheckEdit86.Checked
                .ASESMEN_365 = CheckEdit87.Checked
                .ASESMEN_366 = CheckEdit88.Checked
                .ASESMEN_367 = CheckEdit89.Checked
                .ASESMEN_368 = CheckEdit90.Checked
                .ASESMEN_369 = CheckEdit91.Checked
                .ASESMEN_370 = CheckEdit92.Checked
                .ASESMEN_371 = CheckEdit93.Checked
                .ASESMEN_372 = CheckEdit94.Checked
                .ASESMEN_373 = CheckEdit95.Checked
                .ASESMEN_374 = CheckEdit96.Checked
                .ASESMEN_375 = CheckEdit97.Checked
                .ASESMEN_376 = CheckEdit98.Checked
                .ASESMEN_377 = CheckEdit99.Checked
                .ASESMEN_378 = CheckEdit100.Checked
                .ASESMEN_379 = CheckEdit101.Checked
                .ASESMEN_380 = CheckEdit102.Checked
                .ASESMEN_381 = CheckEdit103.Checked
                .ASESMEN_382 = CheckEdit104.Checked
                .ASESMEN_383 = CheckEdit105.Checked
                .ASESMEN_384 = CheckEdit106.Checked
                .ASESMEN_385 = CheckEdit107.Checked
                .ASESMEN_386 = CheckEdit108.Checked
                .ASESMEN_387 = CheckEdit109.Checked
                .ASESMEN_388 = CheckEdit110.Checked
                .ASESMEN_389 = CheckEdit111.Checked
                .ASESMEN_390 = CheckEdit112.Checked
                .ASESMEN_391 = CheckEdit113.Checked
                .ASESMEN_392 = CheckEdit114.Checked
                .ASESMEN_393 = CheckEdit115.Checked
                .ASESMEN_394 = CheckEdit116.Checked
                .ASESMEN_395 = CheckEdit117.Checked
                .ASESMEN_396 = CheckEdit118.Checked
                .ASESMEN_397 = CheckEdit119.Checked
                .ASESMEN_398 = CheckEdit120.Checked
                .ASESMEN_399 = CheckEdit121.Checked
                .ASESMEN_400 = CheckEdit122.Checked
                .ASESMEN_401 = CheckEdit123.Checked
                .ASESMEN_402 = CheckEdit124.Checked
                .ASESMEN_403 = CheckEdit125.Checked
                .ASESMEN_404 = CheckEdit126.Checked
                .ASESMEN_405 = CheckEdit127.Checked
                .ASESMEN_406 = CheckEdit128.Checked
                .ASESMEN_407 = CheckEdit129.Checked
                .ASESMEN_408 = CheckEdit130.Checked
                .ASESMEN_409 = CheckEdit131.Checked
                .ASESMEN_410 = CheckEdit132.Checked
                .ASESMEN_411 = CheckEdit133.Checked
                .ASESMEN_412 = CheckEdit134.Checked
                .ASESMEN_413 = CheckEdit135.Checked
                .ASESMEN_414 = CheckEdit136.Checked
                .ASESMEN_415 = CheckEdit137.Checked
                .ASESMEN_416 = CheckEdit138.Checked
                .ASESMEN_417 = CheckEdit139.Checked
                .ASESMEN_418 = CheckEdit140.Checked
                .ASESMEN_419 = CheckEdit141.Checked
                .ASESMEN_420 = CheckEdit142.Checked
                .ASESMEN_421 = CheckEdit143.Checked
                .ASESMEN_422 = CheckEdit144.Checked
                .ASESMEN_423 = CheckEdit145.Checked
                .ASESMEN_424 = CheckEdit146.Checked
                .ASESMEN_425 = CheckEdit147.Checked
                .ASESMEN_426 = CheckEdit148.Checked
                .ASESMEN_427 = CheckEdit149.Checked
                .ASESMEN_428 = CheckEdit150.Checked
                .ASESMEN_429 = CheckEdit151.Checked
                .ASESMEN_430 = CheckEdit152.Checked
                .ASESMEN_431 = CheckEdit153.Checked
                .ASESMEN_432 = CheckEdit154.Checked
                .ASESMEN_433 = CheckEdit155.Checked
                .ASESMEN_434 = CheckEdit156.Checked
                .ASESMEN_435 = CheckEdit157.Checked
                .ASESMEN_436 = CheckEdit158.Checked
                .ASESMEN_437 = CheckEdit159.Checked
                .ASESMEN_438 = CheckEdit160.Checked
                .ASESMEN_439 = CheckEdit161.Checked
                .ASESMEN_440 = CheckEdit162.Checked
                .ASESMEN_441 = CheckEdit163.Checked
                .ASESMEN_442 = CheckEdit164.Checked
                .ASESMEN_443 = CheckEdit165.Checked
                .ASESMEN_444 = CheckEdit166.Checked
                .ASESMEN_445 = CheckEdit167.Checked
                .ASESMEN_446 = CheckEdit168.Checked
                .ASESMEN_447 = CheckEdit169.Checked
                .ASESMEN_448 = CheckEdit170.Checked
                .ASESMEN_449 = CheckEdit171.Checked
                .ASESMEN_450 = CheckEdit172.Checked
                .ASESMEN_451 = CheckEdit173.Checked
                .ASESMEN_452 = CheckEdit174.Checked
                .ASESMEN_453 = CheckEdit175.Checked
                .ASESMEN_454 = CheckEdit176.Checked
                .ASESMEN_455 = CheckEdit177.Checked
                .ASESMEN_456 = CheckEdit178.Checked
                .ASESMEN_457 = CheckEdit179.Checked
                .ASESMEN_458 = CheckEdit180.Checked
                .ASESMEN_459 = CheckEdit181.Checked
                .ASESMEN_460 = CheckEdit182.Checked
                .ASESMEN_461 = CheckEdit183.Checked
                .ASESMEN_462 = CheckEdit184.Checked
                .ASESMEN_463 = CheckEdit185.Checked
                .ASESMEN_464 = CheckEdit186.Checked
                .ASESMEN_465 = CheckEdit187.Checked
                .ASESMEN_466 = CheckEdit188.Checked
                .ASESMEN_467 = CheckEdit189.Checked
                .ASESMEN_468 = CheckEdit190.Checked
                .ASESMEN_469 = CheckEdit191.Checked
                .ASESMEN_470 = CheckEdit192.Checked
                .ASESMEN_471 = CheckEdit193.Checked
                .ASESMEN_472 = CheckEdit194.Checked
                .ASESMEN_473 = CheckEdit195.Checked
                .ASESMEN_474 = CheckEdit196.Checked
                .ASESMEN_475 = CheckEdit197.Checked
                .ASESMEN_476 = CheckEdit198.Checked
                .ASESMEN_477 = CheckEdit199.Checked
                .ASESMEN_478 = CheckEdit200.Checked
                .ASESMEN_479 = CheckEdit201.Checked
                .ASESMEN_480 = CheckEdit202.Checked
                .ASESMEN_481 = CheckEdit203.Checked
                .ASESMEN_482 = CheckEdit204.Checked
                .ASESMEN_483 = CheckEdit205.Checked
                .ASESMEN_484 = CheckEdit206.Checked
                .ASESMEN_485 = CheckEdit207.Checked
                .ASESMEN_486 = CheckEdit208.Checked
                .ASESMEN_487 = CheckEdit209.Checked
                .ASESMEN_488 = CheckEdit210.Checked
                .ASESMEN_489 = CheckEdit211.Checked
                .ASESMEN_490 = CheckEdit212.Checked
                .ASESMEN_491 = CheckEdit213.Checked
                .ASESMEN_492 = CheckEdit214.Checked
                .ASESMEN_493 = CheckEdit215.Checked
                .ASESMEN_494 = CheckEdit216.Checked
                .ASESMEN_495 = CheckEdit217.Checked
                .ASESMEN_496 = CheckEdit218.Checked
                .ASESMEN_497 = CheckEdit219.Checked
                .ASESMEN_498 = CheckEdit220.Checked
                .ASESMEN_499 = CheckEdit221.Checked
                .ASESMEN_500 = CheckEdit222.Checked
                .ASESMEN_501 = CheckEdit223.Checked
                .ASESMEN_502 = CheckEdit224.Checked
                .ASESMEN_503 = CheckEdit225.Checked
                .ASESMEN_504 = CheckEdit226.Checked
                .ASESMEN_505 = CheckEdit227.Checked
                .ASESMEN_506 = CheckEdit228.Checked
                .ASESMEN_507 = CheckEdit229.Checked
                .ASESMEN_508 = CheckEdit230.Checked
                .ASESMEN_509 = CheckEdit231.Checked
                .ASESMEN_510 = CheckEdit232.Checked
                .ASESMEN_511 = CheckEdit233.Checked
                .ASESMEN_512 = CheckEdit234.Checked
                .ASESMEN_513 = CheckEdit235.Checked
                .ASESMEN_514 = CheckEdit236.Checked
                .ASESMEN_515 = CheckEdit237.Checked
                .ASESMEN_516 = CheckEdit238.Checked
                .ASESMEN_517 = CheckEdit239.Checked
                .ASESMEN_518 = CheckEdit240.Checked
                .ASESMEN_519 = CheckEdit241.Checked
                .ASESMEN_520 = CheckEdit242.Checked
                .ASESMEN_521 = CheckEdit243.Checked
                .ASESMEN_522 = CheckEdit244.Checked
                .ASESMEN_523 = CheckEdit245.Checked
                .ASESMEN_524 = CheckEdit246.Checked
                .ASESMEN_525 = CheckEdit247.Checked
                .ASESMEN_526 = CheckEdit248.Checked
                .ASESMEN_527 = CheckEdit249.Checked
                .ASESMEN_528 = CheckEdit250.Checked
                .ASESMEN_529 = CheckEdit251.Checked
                .ASESMEN_530 = CheckEdit252.Checked
                .ASESMEN_531 = CheckEdit253.Checked
                .ASESMEN_532 = CheckEdit254.Checked
                .ASESMEN_533 = CheckEdit255.Checked
                .ASESMEN_534 = CheckEdit256.Checked
                .ASESMEN_535 = CheckEdit257.Checked
                .ASESMEN_536 = CheckEdit258.Checked
                .ASESMEN_537 = CheckEdit259.Checked
                .ASESMEN_538 = CheckEdit260.Checked
                .ASESMEN_539 = CheckEdit261.Checked
                .ASESMEN_540 = CheckEdit262.Checked
                .ASESMEN_541 = CheckEdit263.Checked
                .ASESMEN_542 = CheckEdit264.Checked
                .ASESMEN_543 = CheckEdit265.Checked
                .ASESMEN_544 = CheckEdit266.Checked
                .ASESMEN_545 = CheckEdit267.Checked
                .ASESMEN_546 = CheckEdit268.Checked
                .ASESMEN_547 = CheckEdit269.Checked
                .ASESMEN_548 = CheckEdit270.Checked
                .ASESMEN_549 = CheckEdit271.Checked
                .ASESMEN_550 = CheckEdit272.Checked
                .ASESMEN_551 = CheckEdit273.Checked
                .ASESMEN_552 = CheckEdit274.Checked
                .ASESMEN_553 = CheckEdit275.Checked
                .ASESMEN_554 = CheckEdit276.Checked
                .ASESMEN_555 = CheckEdit277.Checked
                .ASESMEN_556 = CheckEdit278.Checked
                .ASESMEN_557 = CheckEdit279.Checked
                .ASESMEN_558 = CheckEdit280.Checked
                .ASESMEN_559 = CheckEdit281.Checked
                .ASESMEN_560 = CheckEdit282.Checked
                .ASESMEN_561 = CheckEdit283.Checked
                .ASESMEN_562 = CheckEdit284.Checked
                .ASESMEN_563 = CheckEdit285.Checked
                .ASESMEN_564 = CheckEdit286.Checked

                Try
                    .CETAK = oS_DIGITAL_RI_44.GetData(txtNoRegister.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                Try
                    If sIsOtority = True Then
                        .KDUSER = oS_DIGITAL_RI_44.GetData(txtNoRegister.Text).KDUSER
                        .KDUSER_SIGNATURE = oS_DIGITAL_RI_44.GetData(txtNoRegister.Text).KDUSER_SIGNATURE
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
                    fn_Save = oS_DIGITAL_RI_44.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_RI_44.UpdateData(ds)
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
    Private Sub fn_Doctor2()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = sKoneksi
            oConn = New SqlConnection(sConn)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "M_DOCTOR A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "DOKTER")

            grdKDDOCTOR2.Properties.DataSource = ds.Tables("DOKTER")
            grdKDDOCTOR2.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR2.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox("Load No Hubungan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub frmEMedrekRI_44_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
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