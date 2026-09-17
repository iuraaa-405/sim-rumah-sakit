Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmAsesmenAwalMedisNeonatusRawatInap
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private sKodeKunjungan As String
    Private skdidentitas As Integer
    Private isLoad As Boolean = False
    Private oAsesmenAwalMedisNeonatusRawatInap As New Digital.clsAsesmenAwalMedisNeonatusRawatInap
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal kdidentitas As Integer, ByVal KodeKunjungan As String, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId
        sKodeKunjungan = KodeKunjungan
        skdidentitas = kdidentitas
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "ASESMEN AWAL MEDIS NEONATUS"
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDKUNJUNGAN.Text.Trim.ToUpper
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadDOKTER()

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

        Dim oPendaftaran As New Admission.clsPendaftaran
        Dim dsIdentitas = oPendaftaran.GetData(txtKDKUNJUNGAN.Text)

        If dsIdentitas IsNot Nothing Then
            txtTanggalDaftar.Text = dsIdentitas.DATE.ToString("dd-MM-yyyy")
            txtNamaPasien.Text = dsIdentitas.M_CUSTOMER.NAME_DISPLAY
            txtNoPasien.Text = dsIdentitas.KDCUSTOMER
            txtTujuan.Text = dsIdentitas.M_DEPARTMENT.NAME_DISPLAY
            txtDokter.Text = dsIdentitas.M_DOCTOR.NAME_DISPLAY
        End If
    End Sub
    Private Sub fn_ViewMode(ByVal Status As Boolean)
        btnSaveNew.Enabled = Not Status
        btnSaveClose.Enabled = Not Status

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

        MemoEdit1.Properties.ReadOnly = Status
        MemoEdit2.Properties.ReadOnly = Status
        MemoEdit3.Properties.ReadOnly = Status
        MemoEdit4.Properties.ReadOnly = Status
        MemoEdit5.Properties.ReadOnly = Status
        MemoEdit6.Properties.ReadOnly = Status
        MemoEdit7.Properties.ReadOnly = Status
        MemoEdit8.Properties.ReadOnly = Status
        MemoEdit9.Properties.ReadOnly = Status
        MemoEdit10.Properties.ReadOnly = Status
        MemoEdit11.Properties.ReadOnly = Status

        TimeEdit1.Properties.ReadOnly = Status
        TimeEdit2.Properties.ReadOnly = Status
        TimeEdit3.Properties.ReadOnly = Status
        TimeEdit4.Properties.ReadOnly = Status
        TimeEdit5.Properties.ReadOnly = Status
        TimeEdit6.Properties.ReadOnly = Status
        TimeEdit7.Properties.ReadOnly = Status

        DateEdit1.Properties.ReadOnly = Status
        DateEdit2.Properties.ReadOnly = Status
        TextEdit136.Properties.ReadOnly = Status
        DateEdit4.Properties.ReadOnly = Status
        DateEdit5.Properties.ReadOnly = Status

        grdKDDOCTOR_1.Properties.ReadOnly = Status
        grdKDDOCTOR_1.Properties.ReadOnly = Status
        grdKDDOCTOR_1.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        deDATE.DateTime = Now

        txtKDKUNJUNGAN.Text = sKodeKunjungan
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

        MemoEdit1.ResetText()
        MemoEdit2.ResetText()
        MemoEdit3.ResetText()
        MemoEdit4.ResetText()
        MemoEdit5.ResetText()
        MemoEdit6.ResetText()
        MemoEdit7.ResetText()
        MemoEdit8.ResetText()
        MemoEdit9.ResetText()
        MemoEdit10.ResetText()
        MemoEdit11.ResetText()

        TimeEdit1.Time = Now
        TimeEdit2.Time = Now
        TimeEdit3.Time = Now
        TimeEdit4.Time = Now
        TimeEdit5.Time = Now
        TimeEdit6.Time = Now
        TimeEdit7.Time = Now

        DateEdit1.DateTime = Now
        DateEdit2.DateTime = Now
        TextEdit136.ResetText()
        DateEdit4.DateTime = Now
        DateEdit5.DateTime = Now

        grdKDDOCTOR_1.ResetText()
        grdKDDOCTOR_2.ResetText()
        grdKDDOCTOR_3.ResetText()

        txtDOKTER_1.ResetText()
        txtDOKTER_2.ResetText()
        txtDOKTER_3.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oAsesmenAwalMedisNeonatusRawatInap.GetData(sNoId)

            With ds
                deDATE.DateTime = .DATE
                txtKDKUNJUNGAN.Text = .KDKUNJUNGAN

                CheckEdit1.Checked = .CheckEdit1
                CheckEdit2.Checked = .CheckEdit2
                CheckEdit3.Checked = .CheckEdit3
                CheckEdit4.Checked = .CheckEdit4
                CheckEdit5.Checked = .CheckEdit5
                CheckEdit6.Checked = .CheckEdit6
                CheckEdit7.Checked = .CheckEdit7
                CheckEdit8.Checked = .CheckEdit8
                CheckEdit9.Checked = .CheckEdit9
                CheckEdit10.Checked = .CheckEdit10
                CheckEdit11.Checked = .CheckEdit11
                CheckEdit12.Checked = .CheckEdit12
                CheckEdit13.Checked = .CheckEdit13
                CheckEdit14.Checked = .CheckEdit14
                CheckEdit15.Checked = .CheckEdit15
                CheckEdit16.Checked = .CheckEdit16
                CheckEdit17.Checked = .CheckEdit17
                CheckEdit18.Checked = .CheckEdit18
                CheckEdit19.Checked = .CheckEdit19
                CheckEdit20.Checked = .CheckEdit20
                CheckEdit21.Checked = .CheckEdit21
                CheckEdit22.Checked = .CheckEdit22
                CheckEdit23.Checked = .CheckEdit23
                CheckEdit24.Checked = .CheckEdit24
                CheckEdit25.Checked = .CheckEdit25
                CheckEdit26.Checked = .CheckEdit26
                CheckEdit27.Checked = .CheckEdit27
                CheckEdit28.Checked = .CheckEdit28
                CheckEdit29.Checked = .CheckEdit29
                CheckEdit30.Checked = .CheckEdit30
                CheckEdit31.Checked = .CheckEdit31
                CheckEdit32.Checked = .CheckEdit32
                CheckEdit33.Checked = .CheckEdit33
                CheckEdit34.Checked = .CheckEdit34
                CheckEdit35.Checked = .CheckEdit35
                CheckEdit36.Checked = .CheckEdit36
                CheckEdit37.Checked = .CheckEdit37
                CheckEdit38.Checked = .CheckEdit38
                CheckEdit39.Checked = .CheckEdit39
                CheckEdit40.Checked = .CheckEdit40
                CheckEdit41.Checked = .CheckEdit41
                CheckEdit42.Checked = .CheckEdit42
                CheckEdit43.Checked = .CheckEdit43
                CheckEdit44.Checked = .CheckEdit44
                CheckEdit45.Checked = .CheckEdit45
                CheckEdit46.Checked = .CheckEdit46
                CheckEdit47.Checked = .CheckEdit47
                CheckEdit48.Checked = .CheckEdit48
                CheckEdit49.Checked = .CheckEdit49
                CheckEdit50.Checked = .CheckEdit50
                CheckEdit51.Checked = .CheckEdit51
                CheckEdit52.Checked = .CheckEdit52
                CheckEdit53.Checked = .CheckEdit53
                CheckEdit54.Checked = .CheckEdit54
                CheckEdit55.Checked = .CheckEdit55
                CheckEdit56.Checked = .CheckEdit56
                CheckEdit57.Checked = .CheckEdit57
                CheckEdit58.Checked = .CheckEdit58
                CheckEdit59.Checked = .CheckEdit59
                CheckEdit60.Checked = .CheckEdit60
                CheckEdit61.Checked = .CheckEdit61
                CheckEdit62.Checked = .CheckEdit62
                CheckEdit63.Checked = .CheckEdit63
                CheckEdit64.Checked = .CheckEdit64
                CheckEdit65.Checked = .CheckEdit65
                CheckEdit66.Checked = .CheckEdit66
                CheckEdit67.Checked = .CheckEdit67
                CheckEdit68.Checked = .CheckEdit68
                CheckEdit69.Checked = .CheckEdit69
                CheckEdit70.Checked = .CheckEdit70
                CheckEdit71.Checked = .CheckEdit71
                CheckEdit72.Checked = .CheckEdit72
                CheckEdit73.Checked = .CheckEdit73
                CheckEdit74.Checked = .CheckEdit74
                CheckEdit75.Checked = .CheckEdit75
                CheckEdit76.Checked = .CheckEdit76
                CheckEdit77.Checked = .CheckEdit77
                CheckEdit78.Checked = .CheckEdit78
                CheckEdit79.Checked = .CheckEdit79
                CheckEdit80.Checked = .CheckEdit80
                CheckEdit81.Checked = .CheckEdit81
                CheckEdit82.Checked = .CheckEdit82
                CheckEdit83.Checked = .CheckEdit83
                CheckEdit84.Checked = .CheckEdit84
                CheckEdit85.Checked = .CheckEdit85
                CheckEdit86.Checked = .CheckEdit86
                CheckEdit87.Checked = .CheckEdit87
                CheckEdit88.Checked = .CheckEdit88
                CheckEdit89.Checked = .CheckEdit89
                CheckEdit90.Checked = .CheckEdit90
                CheckEdit91.Checked = .CheckEdit91
                CheckEdit92.Checked = .CheckEdit92
                CheckEdit93.Checked = .CheckEdit93
                CheckEdit94.Checked = .CheckEdit94
                CheckEdit95.Checked = .CheckEdit95
                CheckEdit96.Checked = .CheckEdit96
                CheckEdit97.Checked = .CheckEdit97
                CheckEdit98.Checked = .CheckEdit98
                CheckEdit99.Checked = .CheckEdit99
                CheckEdit100.Checked = .CheckEdit100
                CheckEdit101.Checked = .CheckEdit101
                CheckEdit102.Checked = .CheckEdit102
                CheckEdit103.Checked = .CheckEdit103
                CheckEdit104.Checked = .CheckEdit104
                CheckEdit105.Checked = .CheckEdit105
                CheckEdit106.Checked = .CheckEdit106
                CheckEdit107.Checked = .CheckEdit107
                CheckEdit108.Checked = .CheckEdit108
                CheckEdit109.Checked = .CheckEdit109
                CheckEdit110.Checked = .CheckEdit110
                CheckEdit111.Checked = .CheckEdit111
                CheckEdit112.Checked = .CheckEdit112
                CheckEdit113.Checked = .CheckEdit113
                CheckEdit114.Checked = .CheckEdit114
                CheckEdit115.Checked = .CheckEdit115
                CheckEdit116.Checked = .CheckEdit116
                CheckEdit117.Checked = .CheckEdit117
                CheckEdit118.Checked = .CheckEdit118
                CheckEdit119.Checked = .CheckEdit119
                CheckEdit120.Checked = .CheckEdit120
                CheckEdit121.Checked = .CheckEdit121
                CheckEdit122.Checked = .CheckEdit122
                CheckEdit123.Checked = .CheckEdit123
                CheckEdit124.Checked = .CheckEdit124
                CheckEdit125.Checked = .CheckEdit125
                CheckEdit126.Checked = .CheckEdit126
                CheckEdit127.Checked = .CheckEdit127
                CheckEdit128.Checked = .CheckEdit128
                CheckEdit129.Checked = .CheckEdit129
                CheckEdit130.Checked = .CheckEdit130
                CheckEdit131.Checked = .CheckEdit131
                CheckEdit132.Checked = .CheckEdit132
                CheckEdit133.Checked = .CheckEdit133
                CheckEdit134.Checked = .CheckEdit134
                CheckEdit135.Checked = .CheckEdit135
                CheckEdit136.Checked = .CheckEdit136
                CheckEdit137.Checked = .CheckEdit137
                CheckEdit138.Checked = .CheckEdit138
                CheckEdit139.Checked = .CheckEdit139
                CheckEdit140.Checked = .CheckEdit140
                CheckEdit141.Checked = .CheckEdit141
                CheckEdit142.Checked = .CheckEdit142
                CheckEdit143.Checked = .CheckEdit143
                CheckEdit144.Checked = .CheckEdit144
                CheckEdit145.Checked = .CheckEdit145
                CheckEdit146.Checked = .CheckEdit146
                CheckEdit147.Checked = .CheckEdit147
                CheckEdit148.Checked = .CheckEdit148
                CheckEdit149.Checked = .CheckEdit149
                CheckEdit150.Checked = .CheckEdit150
                CheckEdit151.Checked = .CheckEdit151
                CheckEdit152.Checked = .CheckEdit152
                CheckEdit153.Checked = .CheckEdit153
                CheckEdit154.Checked = .CheckEdit154
                CheckEdit155.Checked = .CheckEdit155
                CheckEdit156.Checked = .CheckEdit156
                CheckEdit157.Checked = .CheckEdit157
                CheckEdit158.Checked = .CheckEdit158
                CheckEdit159.Checked = .CheckEdit159
                CheckEdit160.Checked = .CheckEdit160
                CheckEdit161.Checked = .CheckEdit161
                CheckEdit162.Checked = .CheckEdit162

                TextEdit1.Text = .TextEdit1
                TextEdit2.Text = .TextEdit2
                TextEdit3.Text = .TextEdit3
                TextEdit4.Text = .TextEdit4
                TextEdit5.Text = .TextEdit5
                TextEdit6.Text = .TextEdit6
                TextEdit7.Text = .TextEdit7
                TextEdit8.Text = .TextEdit8
                TextEdit9.Text = .TextEdit9
                TextEdit10.Text = .TextEdit10
                TextEdit11.Text = .TextEdit11
                TextEdit12.Text = .TextEdit12
                TextEdit13.Text = .TextEdit13
                TextEdit14.Text = .TextEdit14
                TextEdit15.Text = .TextEdit15
                TextEdit16.Text = .TextEdit16
                TextEdit17.Text = .TextEdit17
                TextEdit18.Text = .TextEdit18
                TextEdit19.Text = .TextEdit19
                TextEdit20.Text = .TextEdit20
                TextEdit21.Text = .TextEdit21
                TextEdit22.Text = .TextEdit22
                TextEdit23.Text = .TextEdit23
                TextEdit24.Text = .TextEdit24
                TextEdit25.Text = .TextEdit25
                TextEdit26.Text = .TextEdit26
                TextEdit27.Text = .TextEdit27
                TextEdit28.Text = .TextEdit28
                TextEdit29.Text = .TextEdit29
                TextEdit30.Text = .TextEdit30
                TextEdit31.Text = .TextEdit31
                TextEdit32.Text = .TextEdit32
                TextEdit33.Text = .TextEdit33
                TextEdit34.Text = .TextEdit34
                TextEdit35.Text = .TextEdit35
                TextEdit36.Text = .TextEdit36
                TextEdit37.Text = .TextEdit37
                TextEdit38.Text = .TextEdit38
                TextEdit39.Text = .TextEdit39
                TextEdit40.Text = .TextEdit40
                TextEdit41.Text = .TextEdit41
                TextEdit42.Text = .TextEdit42
                TextEdit43.Text = .TextEdit43
                TextEdit44.Text = .TextEdit44
                TextEdit45.Text = .TextEdit45
                TextEdit46.Text = .TextEdit46
                TextEdit47.Text = .TextEdit47
                TextEdit48.Text = .TextEdit48
                TextEdit49.Text = .TextEdit49
                TextEdit50.Text = .TextEdit50
                TextEdit51.Text = .TextEdit51
                TextEdit52.Text = .TextEdit52
                TextEdit53.Text = .TextEdit53
                TextEdit54.Text = .TextEdit54
                TextEdit55.Text = .TextEdit55
                TextEdit56.Text = .TextEdit56
                TextEdit57.Text = .TextEdit57
                TextEdit58.Text = .TextEdit58
                TextEdit59.Text = .TextEdit59
                TextEdit60.Text = .TextEdit60
                TextEdit61.Text = .TextEdit61
                TextEdit62.Text = .TextEdit62
                TextEdit63.Text = .TextEdit63
                TextEdit64.Text = .TextEdit64
                TextEdit65.Text = .TextEdit65
                TextEdit66.Text = .TextEdit66
                TextEdit67.Text = .TextEdit67
                TextEdit68.Text = .TextEdit68
                TextEdit69.Text = .TextEdit69
                TextEdit70.Text = .TextEdit70
                TextEdit71.Text = .TextEdit71
                TextEdit72.Text = .TextEdit72
                TextEdit73.Text = .TextEdit73
                TextEdit74.Text = .TextEdit74
                TextEdit75.Text = .TextEdit75
                TextEdit76.Text = .TextEdit76
                TextEdit77.Text = .TextEdit77
                TextEdit78.Text = .TextEdit78
                TextEdit79.Text = .TextEdit79
                TextEdit80.Text = .TextEdit80
                TextEdit81.Text = .TextEdit81
                TextEdit82.Text = .TextEdit82
                TextEdit83.Text = .TextEdit83
                TextEdit84.Text = .TextEdit84
                TextEdit85.Text = .TextEdit85
                TextEdit86.Text = .TextEdit86
                TextEdit87.Text = .TextEdit87
                TextEdit88.Text = .TextEdit88
                TextEdit89.Text = .TextEdit89
                TextEdit90.Text = .TextEdit90
                TextEdit91.Text = .TextEdit91
                TextEdit92.Text = .TextEdit92
                TextEdit93.Text = .TextEdit93
                TextEdit94.Text = .TextEdit94
                TextEdit95.Text = .TextEdit95
                TextEdit96.Text = .TextEdit96
                TextEdit97.Text = .TextEdit97
                TextEdit98.Text = .TextEdit98
                TextEdit99.Text = .TextEdit99
                TextEdit100.Text = .TextEdit100
                TextEdit101.Text = .TextEdit101
                TextEdit102.Text = .TextEdit102
                TextEdit103.Text = .TextEdit103
                TextEdit104.Text = .TextEdit104
                TextEdit105.Text = .TextEdit105
                TextEdit106.Text = .TextEdit106
                TextEdit107.Text = .TextEdit107
                TextEdit108.Text = .TextEdit108
                TextEdit109.Text = .TextEdit109
                TextEdit110.Text = .TextEdit110
                TextEdit111.Text = .TextEdit111
                TextEdit112.Text = .TextEdit112
                TextEdit113.Text = .TextEdit113
                TextEdit114.Text = .TextEdit114
                TextEdit115.Text = .TextEdit115
                TextEdit116.Text = .TextEdit116
                TextEdit117.Text = .TextEdit117
                TextEdit118.Text = .TextEdit118
                TextEdit119.Text = .TextEdit119
                TextEdit120.Text = .TextEdit120
                TextEdit121.Text = .TextEdit121
                TextEdit122.Text = .TextEdit122
                TextEdit123.Text = .TextEdit123
                TextEdit124.Text = .TextEdit124
                TextEdit125.Text = .TextEdit125
                TextEdit126.Text = .TextEdit126
                TextEdit127.Text = .TextEdit127
                TextEdit128.Text = .TextEdit128
                TextEdit129.Text = .TextEdit129
                TextEdit130.Text = .TextEdit130
                TextEdit131.Text = .TextEdit131
                TextEdit132.Text = .TextEdit132
                TextEdit133.Text = .TextEdit133
                TextEdit134.Text = .TextEdit134
                TextEdit135.Text = .TextEdit135
                TextEdit136.Text = .TextEdit136

                MemoEdit1.Text = .MemoEdit1
                MemoEdit2.Text = .MemoEdit2
                MemoEdit3.Text = .MemoEdit3
                MemoEdit4.Text = .MemoEdit4
                MemoEdit5.Text = .MemoEdit5
                MemoEdit6.Text = .MemoEdit6
                MemoEdit7.Text = .MemoEdit7
                MemoEdit8.Text = .MemoEdit8
                MemoEdit9.Text = .MemoEdit9
                MemoEdit10.Text = .MemoEdit10
                MemoEdit11.Text = .MemoEdit11

                TimeEdit1.Time = .TimeEdit1
                TimeEdit2.Time = .TimeEdit2
                TimeEdit3.Time = .TimeEdit3
                TimeEdit4.Time = .TimeEdit4
                TimeEdit5.Time = .TimeEdit5
                TimeEdit6.Time = .TimeEdit6
                TimeEdit7.Time = .TimeEdit7

                DateEdit1.DateTime = .DateEdit1
                DateEdit2.DateTime = .DateEdit2
                DateEdit4.DateTime = .DateEdit4
                DateEdit5.DateTime = .DateEdit5

                'grdKDDOCTOR_1.Text = .grdKDDOCTOR_1
                'grdKDDOCTOR_2.Text = .grdKDDOCTOR_2
                'grdKDDOCTOR_3.Text = .grdKDDOCTOR_3

                txtDOKTER_1.Text = .grdKDDOCTOR_1
                txtDOKTER_2.Text = .grdKDDOCTOR_2
                txtDOKTER_3.Text = .grdKDDOCTOR_3

                BindingSource1.DataSource = oAsesmenAwalMedisNeonatusRawatInap.GetDataDetail(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = BindingSource1

                BindingSource2.DataSource = oAsesmenAwalMedisNeonatusRawatInap.GetDataDetail_(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grd_DIAGNOSASEMENTARA.DataSource = BindingSource2
            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If txtKDKUNJUNGAN.Text = String.Empty Then
                MsgBox("Dibutuhkan Nomor Kunjungan", MsgBoxStyle.Exclamation, Me.Text)
                txtKDKUNJUNGAN.Focus()
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
            Dim ds = oAsesmenAwalMedisNeonatusRawatInap.GetStructureHeader
            With ds
                .KDIDENTITAS = skdidentitas
                Try
                    .DATECREATED = oAsesmenAwalMedisNeonatusRawatInap.GetData(sNoId).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .KDCUSTOMER = txtNoPasien.Text
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .KDASESMENNEONATUS = sNoId
                .KDKUNJUNGAN = txtKDKUNJUNGAN.Text

                .CheckEdit1 = CheckEdit1.Checked
                .CheckEdit2 = CheckEdit2.Checked
                .CheckEdit3 = CheckEdit3.Checked
                .CheckEdit4 = CheckEdit4.Checked
                .CheckEdit5 = CheckEdit5.Checked
                .CheckEdit6 = CheckEdit6.Checked
                .CheckEdit7 = CheckEdit7.Checked
                .CheckEdit8 = CheckEdit8.Checked
                .CheckEdit9 = CheckEdit9.Checked
                .CheckEdit10 = CheckEdit10.Checked
                .CheckEdit11 = CheckEdit11.Checked
                .CheckEdit12 = CheckEdit12.Checked
                .CheckEdit13 = CheckEdit13.Checked
                .CheckEdit14 = CheckEdit14.Checked
                .CheckEdit15 = CheckEdit15.Checked
                .CheckEdit16 = CheckEdit16.Checked
                .CheckEdit17 = CheckEdit17.Checked
                .CheckEdit18 = CheckEdit18.Checked
                .CheckEdit19 = CheckEdit19.Checked
                .CheckEdit20 = CheckEdit20.Checked
                .CheckEdit21 = CheckEdit21.Checked
                .CheckEdit22 = CheckEdit22.Checked
                .CheckEdit23 = CheckEdit23.Checked
                .CheckEdit24 = CheckEdit24.Checked
                .CheckEdit25 = CheckEdit25.Checked
                .CheckEdit26 = CheckEdit26.Checked
                .CheckEdit27 = CheckEdit27.Checked
                .CheckEdit28 = CheckEdit28.Checked
                .CheckEdit29 = CheckEdit29.Checked
                .CheckEdit30 = CheckEdit30.Checked
                .CheckEdit31 = CheckEdit31.Checked
                .CheckEdit32 = CheckEdit32.Checked
                .CheckEdit33 = CheckEdit33.Checked
                .CheckEdit34 = CheckEdit34.Checked
                .CheckEdit35 = CheckEdit35.Checked
                .CheckEdit36 = CheckEdit36.Checked
                .CheckEdit37 = CheckEdit37.Checked
                .CheckEdit38 = CheckEdit38.Checked
                .CheckEdit39 = CheckEdit39.Checked
                .CheckEdit40 = CheckEdit40.Checked
                .CheckEdit41 = CheckEdit41.Checked
                .CheckEdit42 = CheckEdit42.Checked
                .CheckEdit43 = CheckEdit43.Checked
                .CheckEdit44 = CheckEdit44.Checked
                .CheckEdit45 = CheckEdit45.Checked
                .CheckEdit46 = CheckEdit46.Checked
                .CheckEdit47 = CheckEdit47.Checked
                .CheckEdit48 = CheckEdit48.Checked
                .CheckEdit49 = CheckEdit49.Checked
                .CheckEdit50 = CheckEdit50.Checked
                .CheckEdit51 = CheckEdit51.Checked
                .CheckEdit52 = CheckEdit52.Checked
                .CheckEdit53 = CheckEdit53.Checked
                .CheckEdit54 = CheckEdit54.Checked
                .CheckEdit55 = CheckEdit55.Checked
                .CheckEdit56 = CheckEdit56.Checked
                .CheckEdit57 = CheckEdit57.Checked
                .CheckEdit58 = CheckEdit58.Checked
                .CheckEdit59 = CheckEdit59.Checked
                .CheckEdit60 = CheckEdit60.Checked
                .CheckEdit61 = CheckEdit61.Checked
                .CheckEdit62 = CheckEdit62.Checked
                .CheckEdit63 = CheckEdit63.Checked
                .CheckEdit64 = CheckEdit64.Checked
                .CheckEdit65 = CheckEdit65.Checked
                .CheckEdit66 = CheckEdit66.Checked
                .CheckEdit67 = CheckEdit67.Checked
                .CheckEdit68 = CheckEdit68.Checked
                .CheckEdit69 = CheckEdit69.Checked
                .CheckEdit70 = CheckEdit70.Checked
                .CheckEdit71 = CheckEdit71.Checked
                .CheckEdit72 = CheckEdit72.Checked
                .CheckEdit73 = CheckEdit73.Checked
                .CheckEdit74 = CheckEdit74.Checked
                .CheckEdit75 = CheckEdit75.Checked
                .CheckEdit76 = CheckEdit76.Checked
                .CheckEdit77 = CheckEdit77.Checked
                .CheckEdit78 = CheckEdit78.Checked
                .CheckEdit79 = CheckEdit79.Checked
                .CheckEdit80 = CheckEdit80.Checked
                .CheckEdit81 = CheckEdit81.Checked
                .CheckEdit82 = CheckEdit82.Checked
                .CheckEdit83 = CheckEdit83.Checked
                .CheckEdit84 = CheckEdit84.Checked
                .CheckEdit85 = CheckEdit85.Checked
                .CheckEdit86 = CheckEdit86.Checked
                .CheckEdit87 = CheckEdit87.Checked
                .CheckEdit88 = CheckEdit88.Checked
                .CheckEdit89 = CheckEdit89.Checked
                .CheckEdit90 = CheckEdit90.Checked
                .CheckEdit91 = CheckEdit91.Checked
                .CheckEdit92 = CheckEdit92.Checked
                .CheckEdit93 = CheckEdit93.Checked
                .CheckEdit94 = CheckEdit94.Checked
                .CheckEdit95 = CheckEdit95.Checked
                .CheckEdit96 = CheckEdit96.Checked
                .CheckEdit97 = CheckEdit97.Checked
                .CheckEdit98 = CheckEdit98.Checked
                .CheckEdit99 = CheckEdit99.Checked
                .CheckEdit100 = CheckEdit100.Checked
                .CheckEdit101 = CheckEdit101.Checked
                .CheckEdit102 = CheckEdit102.Checked
                .CheckEdit103 = CheckEdit103.Checked
                .CheckEdit104 = CheckEdit104.Checked
                .CheckEdit105 = CheckEdit105.Checked
                .CheckEdit106 = CheckEdit106.Checked
                .CheckEdit107 = CheckEdit107.Checked
                .CheckEdit108 = CheckEdit108.Checked
                .CheckEdit109 = CheckEdit109.Checked
                .CheckEdit110 = CheckEdit110.Checked
                .CheckEdit111 = CheckEdit111.Checked
                .CheckEdit112 = CheckEdit112.Checked
                .CheckEdit113 = CheckEdit113.Checked
                .CheckEdit114 = CheckEdit114.Checked
                .CheckEdit115 = CheckEdit115.Checked
                .CheckEdit116 = CheckEdit116.Checked
                .CheckEdit117 = CheckEdit117.Checked
                .CheckEdit118 = CheckEdit118.Checked
                .CheckEdit119 = CheckEdit119.Checked
                .CheckEdit120 = CheckEdit120.Checked
                .CheckEdit121 = CheckEdit121.Checked
                .CheckEdit122 = CheckEdit122.Checked
                .CheckEdit123 = CheckEdit123.Checked
                .CheckEdit124 = CheckEdit124.Checked
                .CheckEdit125 = CheckEdit125.Checked
                .CheckEdit126 = CheckEdit126.Checked
                .CheckEdit127 = CheckEdit127.Checked
                .CheckEdit128 = CheckEdit128.Checked
                .CheckEdit129 = CheckEdit129.Checked
                .CheckEdit130 = CheckEdit130.Checked
                .CheckEdit131 = CheckEdit131.Checked
                .CheckEdit132 = CheckEdit132.Checked
                .CheckEdit133 = CheckEdit133.Checked
                .CheckEdit134 = CheckEdit134.Checked
                .CheckEdit135 = CheckEdit135.Checked
                .CheckEdit136 = CheckEdit136.Checked
                .CheckEdit137 = CheckEdit137.Checked
                .CheckEdit138 = CheckEdit138.Checked
                .CheckEdit139 = CheckEdit139.Checked
                .CheckEdit140 = CheckEdit140.Checked
                .CheckEdit141 = CheckEdit141.Checked
                .CheckEdit142 = CheckEdit142.Checked
                .CheckEdit143 = CheckEdit143.Checked
                .CheckEdit144 = CheckEdit144.Checked
                .CheckEdit145 = CheckEdit145.Checked
                .CheckEdit146 = CheckEdit146.Checked
                .CheckEdit147 = CheckEdit147.Checked
                .CheckEdit148 = CheckEdit148.Checked
                .CheckEdit149 = CheckEdit149.Checked
                .CheckEdit150 = CheckEdit150.Checked
                .CheckEdit151 = CheckEdit151.Checked
                .CheckEdit152 = CheckEdit152.Checked
                .CheckEdit153 = CheckEdit153.Checked
                .CheckEdit154 = CheckEdit154.Checked
                .CheckEdit155 = CheckEdit155.Checked
                .CheckEdit156 = CheckEdit156.Checked
                .CheckEdit157 = CheckEdit157.Checked
                .CheckEdit158 = CheckEdit158.Checked
                .CheckEdit159 = CheckEdit159.Checked
                .CheckEdit160 = CheckEdit160.Checked
                .CheckEdit161 = CheckEdit161.Checked
                .CheckEdit162 = CheckEdit162.Checked

                .TextEdit1 = TextEdit1.Text
                .TextEdit2 = TextEdit2.Text
                .TextEdit3 = TextEdit3.Text
                .TextEdit4 = TextEdit4.Text
                .TextEdit5 = TextEdit5.Text
                .TextEdit6 = TextEdit6.Text
                .TextEdit7 = TextEdit7.Text
                .TextEdit8 = TextEdit8.Text
                .TextEdit9 = TextEdit9.Text
                .TextEdit10 = TextEdit10.Text
                .TextEdit11 = TextEdit11.Text
                .TextEdit12 = TextEdit12.Text
                .TextEdit13 = TextEdit13.Text
                .TextEdit14 = TextEdit14.Text
                .TextEdit15 = TextEdit15.Text
                .TextEdit16 = TextEdit16.Text
                .TextEdit17 = TextEdit17.Text
                .TextEdit18 = TextEdit18.Text
                .TextEdit19 = TextEdit19.Text
                .TextEdit20 = TextEdit20.Text
                .TextEdit21 = TextEdit21.Text
                .TextEdit22 = TextEdit22.Text
                .TextEdit23 = TextEdit23.Text
                .TextEdit24 = TextEdit24.Text
                .TextEdit25 = TextEdit25.Text
                .TextEdit26 = TextEdit26.Text
                .TextEdit27 = TextEdit27.Text
                .TextEdit28 = TextEdit28.Text
                .TextEdit29 = TextEdit29.Text
                .TextEdit30 = TextEdit30.Text
                .TextEdit31 = TextEdit31.Text
                .TextEdit32 = TextEdit32.Text
                .TextEdit33 = TextEdit33.Text
                .TextEdit34 = TextEdit34.Text
                .TextEdit35 = TextEdit35.Text
                .TextEdit36 = TextEdit36.Text
                .TextEdit37 = TextEdit37.Text
                .TextEdit38 = TextEdit38.Text
                .TextEdit39 = TextEdit39.Text
                .TextEdit40 = TextEdit40.Text
                .TextEdit41 = TextEdit41.Text
                .TextEdit42 = TextEdit42.Text
                .TextEdit43 = TextEdit43.Text
                .TextEdit44 = TextEdit44.Text
                .TextEdit45 = TextEdit45.Text
                .TextEdit46 = TextEdit46.Text
                .TextEdit47 = TextEdit47.Text
                .TextEdit48 = TextEdit48.Text
                .TextEdit49 = TextEdit49.Text
                .TextEdit50 = TextEdit50.Text
                .TextEdit51 = TextEdit51.Text
                .TextEdit52 = TextEdit52.Text
                .TextEdit53 = TextEdit53.Text
                .TextEdit54 = TextEdit54.Text
                .TextEdit55 = TextEdit55.Text
                .TextEdit56 = TextEdit56.Text
                .TextEdit57 = TextEdit57.Text
                .TextEdit58 = TextEdit58.Text
                .TextEdit59 = TextEdit59.Text
                .TextEdit60 = TextEdit60.Text
                .TextEdit61 = TextEdit61.Text
                .TextEdit62 = TextEdit62.Text
                .TextEdit63 = TextEdit63.Text
                .TextEdit64 = TextEdit64.Text
                .TextEdit65 = TextEdit65.Text
                .TextEdit66 = TextEdit66.Text
                .TextEdit67 = TextEdit67.Text
                .TextEdit68 = TextEdit68.Text
                .TextEdit69 = TextEdit69.Text
                .TextEdit70 = TextEdit70.Text
                .TextEdit71 = TextEdit71.Text
                .TextEdit72 = TextEdit72.Text
                .TextEdit73 = TextEdit73.Text
                .TextEdit74 = TextEdit74.Text
                .TextEdit75 = TextEdit75.Text
                .TextEdit76 = TextEdit76.Text
                .TextEdit77 = TextEdit77.Text
                .TextEdit78 = TextEdit78.Text
                .TextEdit79 = TextEdit79.Text
                .TextEdit80 = TextEdit80.Text
                .TextEdit81 = TextEdit81.Text
                .TextEdit82 = TextEdit82.Text
                .TextEdit83 = TextEdit83.Text
                .TextEdit84 = TextEdit84.Text
                .TextEdit85 = TextEdit85.Text
                .TextEdit86 = TextEdit86.Text
                .TextEdit87 = TextEdit87.Text
                .TextEdit88 = TextEdit88.Text
                .TextEdit89 = TextEdit89.Text
                .TextEdit90 = TextEdit90.Text
                .TextEdit91 = TextEdit91.Text
                .TextEdit92 = TextEdit92.Text
                .TextEdit93 = TextEdit93.Text
                .TextEdit94 = TextEdit94.Text
                .TextEdit95 = TextEdit95.Text
                .TextEdit96 = TextEdit96.Text
                .TextEdit97 = TextEdit97.Text
                .TextEdit98 = TextEdit98.Text
                .TextEdit99 = TextEdit99.Text
                .TextEdit100 = TextEdit100.Text
                .TextEdit101 = TextEdit101.Text
                .TextEdit102 = TextEdit102.Text
                .TextEdit103 = TextEdit103.Text
                .TextEdit104 = TextEdit104.Text
                .TextEdit105 = TextEdit105.Text
                .TextEdit106 = TextEdit106.Text
                .TextEdit107 = TextEdit107.Text
                .TextEdit108 = TextEdit108.Text
                .TextEdit109 = TextEdit109.Text
                .TextEdit110 = TextEdit110.Text
                .TextEdit111 = TextEdit111.Text
                .TextEdit112 = TextEdit112.Text
                .TextEdit113 = TextEdit113.Text
                .TextEdit114 = TextEdit114.Text
                .TextEdit115 = TextEdit115.Text
                .TextEdit116 = TextEdit116.Text
                .TextEdit117 = TextEdit117.Text
                .TextEdit118 = TextEdit118.Text
                .TextEdit119 = TextEdit119.Text
                .TextEdit120 = TextEdit120.Text
                .TextEdit121 = TextEdit121.Text
                .TextEdit122 = TextEdit122.Text
                .TextEdit123 = TextEdit123.Text
                .TextEdit124 = TextEdit124.Text
                .TextEdit125 = TextEdit125.Text
                .TextEdit126 = TextEdit126.Text
                .TextEdit127 = TextEdit127.Text
                .TextEdit128 = TextEdit128.Text
                .TextEdit129 = TextEdit129.Text
                .TextEdit130 = TextEdit130.Text
                .TextEdit131 = TextEdit131.Text
                .TextEdit132 = TextEdit132.Text
                .TextEdit133 = TextEdit133.Text
                .TextEdit134 = TextEdit134.Text
                .TextEdit135 = TextEdit135.Text
                .TextEdit136 = TextEdit136.Text

                .MemoEdit1 = MemoEdit1.Text
                .MemoEdit2 = MemoEdit2.Text
                .MemoEdit3 = MemoEdit3.Text
                .MemoEdit4 = MemoEdit4.Text
                .MemoEdit5 = MemoEdit5.Text
                .MemoEdit6 = MemoEdit6.Text
                .MemoEdit7 = MemoEdit7.Text
                .MemoEdit8 = MemoEdit8.Text
                .MemoEdit9 = MemoEdit9.Text
                .MemoEdit10 = MemoEdit10.Text
                .MemoEdit11 = MemoEdit11.Text

                .TimeEdit1 = TimeEdit1.Time
                .TimeEdit2 = TimeEdit2.Time
                .TimeEdit3 = TimeEdit3.Time
                .TimeEdit4 = TimeEdit4.Time
                .TimeEdit5 = TimeEdit5.Time
                .TimeEdit6 = TimeEdit5.Time
                .TimeEdit7 = TimeEdit5.Time

                .DateEdit1 = DateEdit1.DateTime
                .DateEdit2 = DateEdit2.DateTime
                .DateEdit4 = DateEdit4.DateTime
                .DateEdit5 = DateEdit5.DateTime

                '.grdKDDOCTOR_1 = grdKDDOCTOR_1.Text
                '.grdKDDOCTOR_2 = grdKDDOCTOR_2.Text
                '.grdKDDOCTOR_3 = grdKDDOCTOR_3.Text

                .grdKDDOCTOR_1 = txtDOKTER_1.Text
                .grdKDDOCTOR_2 = txtDOKTER_2.Text
                .grdKDDOCTOR_3 = txtDOKTER_3.Text

                .KDUSER = sUserID
            End With

            ' ***** DETIL *****
            Dim arrDetail = oAsesmenAwalMedisNeonatusRawatInap.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oAsesmenAwalMedisNeonatusRawatInap.GetStructureDetail
                With dsDetail
                    .SEQ = i
                    .KDASESMENNEONATUS = ds.KDASESMENNEONATUS
                    .NAMADIAGNOSA = grvDetail.GetRowCellValue(i, colNAMADIAGNOSA)
                    .KDDIAGNOSA = grvDetail.GetRowCellValue(i, colKDDIAGNOSA)
                End With
                arrDetail.Add(dsDetail)
            Next

            ' ***** DETIL *****
            Dim arrDetail_ = oAsesmenAwalMedisNeonatusRawatInap.GetStructureDetailList_
            For i As Integer = 0 To grv_DIAGNOSASEMENTARA.RowCount - 2
                Dim dsDetail = oAsesmenAwalMedisNeonatusRawatInap.GetStructureDetail_
                With dsDetail
                    .SEQ = i
                    .KDASESMENNEONATUS = ds.KDASESMENNEONATUS
                    .NAMADIAGNOSA = grv_DIAGNOSASEMENTARA.GetRowCellValue(i, colNAMADIAGNOSA_)
                    .KDDIAGNOSA = grv_DIAGNOSASEMENTARA.GetRowCellValue(i, colKDDIAGNOSA_)
                End With
                arrDetail_.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oAsesmenAwalMedisNeonatusRawatInap.InsertData(ds, arrDetail, arrDetail_)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oAsesmenAwalMedisNeonatusRawatInap.UpdateData(ds, arrDetail, arrDetail_)
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
#Region "Grid Method"
#End Region
#Region "Command Button"
    Private Sub frm_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
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
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        'Dim frmBrowseOrderDiagnosaICD10 As New frmBrowseOrderDiagnosaICD10
        'Try
        '    frmBrowseOrderDiagnosaICD10.ShowDialog(Me)
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'Finally
        '    If Not frmBrowseOrderDiagnosaICD10 Is Nothing Then frmBrowseOrderDiagnosaICD10.Dispose()
        '    frmBrowseOrderDiagnosaICD10 = Nothing

        '    If sNAMADIAGNOSA <> "" Then
        '        grvDetail.Focus()
        '        grvDetail.AddNewRow()
        '        grvDetail.SetFocusedRowCellValue(colNAMADIAGNOSA, sNAMADIAGNOSA)
        '        grvDetail.SetFocusedRowCellValue(colKDDIAGNOSA, sKDDIAGNOSA)
        '        grvDetail.UpdateCurrentRow()
        '    End If
        'End Try
    End Sub
    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        'Dim frmBrowseOrderDiagnosaICD10 As New frmBrowseOrderDiagnosaICD10
        'Try
        '    frmBrowseOrderDiagnosaICD10.ShowDialog(Me)
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'Finally
        '    If Not frmBrowseOrderDiagnosaICD10 Is Nothing Then frmBrowseOrderDiagnosaICD10.Dispose()
        '    frmBrowseOrderDiagnosaICD10 = Nothing

        '    If sNAMADIAGNOSA <> "" Then
        '        grv_DIAGNOSASEMENTARA.Focus()
        '        grv_DIAGNOSASEMENTARA.AddNewRow()
        '        grv_DIAGNOSASEMENTARA.SetFocusedRowCellValue(colNAMADIAGNOSA_, sNAMADIAGNOSA)
        '        grv_DIAGNOSASEMENTARA.SetFocusedRowCellValue(colKDDIAGNOSA_, sKDDIAGNOSA)
        '        grv_DIAGNOSASEMENTARA.UpdateCurrentRow()
        '    End If
        'End Try
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail.DeleteSelectedRows()
    End Sub
    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grv_DIAGNOSASEMENTARA.DeleteSelectedRows()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadDOKTER()
        Dim oDPJP As New Reference.clsDoctor
        Try
            grdKDDOCTOR_1.Properties.DataSource = oDPJP.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDOCTOR_1.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR_1.Properties.DisplayMember = "NAME_DISPLAY"

            grdKDDOCTOR_2.Properties.DataSource = oDPJP.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDOCTOR_2.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR_2.Properties.DisplayMember = "NAME_DISPLAY"

            grdKDDOCTOR_3.Properties.DataSource = oDPJP.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDOCTOR_3.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR_3.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
        'Try
        '    Dim sKoneksi As String = String.Empty
        '    Dim oSetKoneksi As New Setting.clsSetKoneksi
        '    Dim dsSetKoneksi = oSetKoneksi.GetData()
        '    If dsSetKoneksi IsNot Nothing Then
        '        sKoneksi = dsSetKoneksi.KONEKSI
        '    End If

        '    Dim oConn As New SqlConnection
        '    Dim oComm As New SqlCommand
        '    Dim da As SqlDataAdapter
        '    Dim ds As New DataSet
        '    Dim SQL As String
        '    Dim sConn As String = sKoneksi

        '    oConn = New SqlConnection(sConn)

        '    If oConn.State = ConnectionState.Closed Then
        '        oConn.Open()
        '    End If

        '    SQL = "SELECT "
        '    SQL &= "* "
        '    SQL &= "FROM "
        '    SQL &= "M_DOCTOR A "
        '    SQL &= "WHERE "
        '    SQL &= "A.ISACTIVE = 1 "

        '    oComm.Connection = oConn
        '    oComm.CommandText = SQL
        '    oComm.CommandTimeout = 120
        '    oComm.CommandType = CommandType.Text

        '    da = New SqlDataAdapter(oComm)
        '    da.Fill(ds, "DOKTER")

        '    grdKDDOCTOR_1.Properties.DataSource = ds.Tables("DOKTER")
        '    grdKDDOCTOR_1.Properties.ValueMember = "KDDOCTOR"
        '    grdKDDOCTOR_1.Properties.DisplayMember = "NAME_DISPLAY"

        '    grdKDDOCTOR_2.Properties.DataSource = ds.Tables("DOKTER")
        '    grdKDDOCTOR_2.Properties.ValueMember = "KDDOCTOR"
        '    grdKDDOCTOR_2.Properties.DisplayMember = "NAME_DISPLAY"

        '    grdKDDOCTOR_3.Properties.DataSource = ds.Tables("DOKTER")
        '    grdKDDOCTOR_3.Properties.ValueMember = "KDDOCTOR"
        '    grdKDDOCTOR_3.Properties.DisplayMember = "NAME_DISPLAY"

        '    If oConn.State = ConnectionState.Open Then
        '        oConn.Close()
        '    End If
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub
    Private Sub grdKDDOCTOR_1_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDDOCTOR_1.EditValueChanged
        If isLoad = True Then
            txtDOKTER_1.Text = grdKDDOCTOR_1.Text
        End If
    End Sub
    Private Sub grdKDDOCTOR_2_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDDOCTOR_2.EditValueChanged
        If isLoad = True Then
            txtDOKTER_2.Text = grdKDDOCTOR_2.Text
        End If
    End Sub
    Private Sub grdKDDOCTOR_3_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDDOCTOR_3.EditValueChanged
        If isLoad = True Then
            txtDOKTER_3.Text = grdKDDOCTOR_3.Text
        End If
    End Sub
#End Region
#Region "Handles"
    Private Sub frmAsesmenAwalMedisNeonatusRawatInap_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
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