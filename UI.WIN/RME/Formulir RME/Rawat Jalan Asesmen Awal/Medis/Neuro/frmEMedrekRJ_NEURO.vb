Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmEMedrekRJ_NEURO
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_RJ_NEURO As New Digital.clsDigital_RJ_NEURO
    Private down As Boolean = False
    Private sKoneksi As String = String.Empty
    Private sKDDOKTER As String = String.Empty

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

        txtNOREG.Text = KDREG

        If dsPendaftaran IsNot Nothing Then
            txtNAMA.Text = dsPendaftaran.NAMAPASIEN
            txtJK.Text = dsPendaftaran.JENISKELAMIN
            txtUmur.Text = dsPendaftaran.USIA
            txtKELAS.Text = dsPendaftaran.KELASPELAYANAN
            txtNOMORRM.Text = dsPendaftaran.KDCUSTOMER
            txtNOREG.Text = dsPendaftaran.KDKUNJUNGAN
            deDATE.Text = dsPendaftaran.DATE

            sKDDOKTER = dsPendaftaran.KDDOKTER
            Dim oSetUser As New Setting.clsUser
            Dim dsSetUser = oSetUser.GetData(sUserID)
            If dsSetUser IsNot Nothing Then
                sKDDOKTER = dsSetUser.KDDOCTOR
            End If

            txtWAKTU.Text = Now
        Else
            txtNAMA.ResetText()
            txtJK.ResetText()
            txtUmur.ResetText()
            txtKELAS.ResetText()
            txtNOMORRM.ResetText()
            txtNOREG.ResetText()
            txtWAKTU.ResetText()
            deDATE.ResetText()
            grdDOCTOR.ResetText()
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = EMedrekRJ_NEURO.TITLE
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Dispose()
        sCode = txtNOREG.Text.Trim.ToUpper
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_ChangeFormState()
        fn_NOIDUSER()
        fn_DOCTOR()

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
        'deDATE.Properties.ReadOnly = Status
        grdDOCTOR.Properties.ReadOnly = Status
        txtWAKTU.Properties.ReadOnly = Status
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
        ComboBoxEdit25.Properties.ReadOnly = Status
        ComboBoxEdit26.Properties.ReadOnly = Status
        ComboBoxEdit27.Properties.ReadOnly = Status
        ComboBoxEdit28.Properties.ReadOnly = Status
        ComboBoxEdit29.Properties.ReadOnly = Status
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
        ComboBoxEdit40.Properties.ReadOnly = Status
        ComboBoxEdit41.Properties.ReadOnly = Status
        ComboBoxEdit42.Properties.ReadOnly = Status
        ComboBoxEdit43.Properties.ReadOnly = Status
        ComboBoxEdit44.Properties.ReadOnly = Status
        ComboBoxEdit45.Properties.ReadOnly = Status
        ComboBoxEdit46.Properties.ReadOnly = Status
        ComboBoxEdit47.Properties.ReadOnly = Status
        ComboBoxEdit48.Properties.ReadOnly = Status
        ComboBoxEdit49.Properties.ReadOnly = Status
        ComboBoxEdit50.Properties.ReadOnly = Status
        ComboBoxEdit51.Properties.ReadOnly = Status
        ComboBoxEdit52.Properties.ReadOnly = Status
        ComboBoxEdit53.Properties.ReadOnly = Status
        ComboBoxEdit54.Properties.ReadOnly = Status
        ComboBoxEdit55.Properties.ReadOnly = Status
        ComboBoxEdit56.Properties.ReadOnly = Status
        ComboBoxEdit57.Properties.ReadOnly = Status
        ComboBoxEdit58.Properties.ReadOnly = Status
        ComboBoxEdit59.Properties.ReadOnly = Status
        ComboBoxEdit60.Properties.ReadOnly = Status
        ComboBoxEdit61.Properties.ReadOnly = Status
        ComboBoxEdit62.Properties.ReadOnly = Status
        ComboBoxEdit63.Properties.ReadOnly = Status
        ComboBoxEdit64.Properties.ReadOnly = Status
        ComboBoxEdit65.Properties.ReadOnly = Status
        ComboBoxEdit66.Properties.ReadOnly = Status
        ComboBoxEdit67.Properties.ReadOnly = Status
        ComboBoxEdit68.Properties.ReadOnly = Status
        ComboBoxEdit69.Properties.ReadOnly = Status
        ComboBoxEdit70.Properties.ReadOnly = Status
        ComboBoxEdit71.Properties.ReadOnly = Status
        ComboBoxEdit72.Properties.ReadOnly = Status
        ComboBoxEdit73.Properties.ReadOnly = Status
        ComboBoxEdit74.Properties.ReadOnly = Status
        ComboBoxEdit75.Properties.ReadOnly = Status
        ComboBoxEdit76.Properties.ReadOnly = Status
        ComboBoxEdit77.Properties.ReadOnly = Status
        ComboBoxEdit78.Properties.ReadOnly = Status
        ComboBoxEdit79.Properties.ReadOnly = Status
        ComboBoxEdit80.Properties.ReadOnly = Status
        ComboBoxEdit81.Properties.ReadOnly = Status
        ComboBoxEdit82.Properties.ReadOnly = Status
        ComboBoxEdit83.Properties.ReadOnly = Status
        ComboBoxEdit84.Properties.ReadOnly = Status
        ComboBoxEdit85.Properties.ReadOnly = Status
        ComboBoxEdit86.Properties.ReadOnly = Status
        ComboBoxEdit87.Properties.ReadOnly = Status
        ComboBoxEdit88.Properties.ReadOnly = Status
        ComboBoxEdit89.Properties.ReadOnly = Status
        ComboBoxEdit90.Properties.ReadOnly = Status
        ComboBoxEdit91.Properties.ReadOnly = Status
        ComboBoxEdit92.Properties.ReadOnly = Status
        ComboBoxEdit93.Properties.ReadOnly = Status
        ComboBoxEdit94.Properties.ReadOnly = Status
        ComboBoxEdit95.Properties.ReadOnly = Status
        ComboBoxEdit96.Properties.ReadOnly = Status
        ComboBoxEdit97.Properties.ReadOnly = Status
        ComboBoxEdit98.Properties.ReadOnly = Status
        ComboBoxEdit99.Properties.ReadOnly = Status
        ComboBoxEdit100.Properties.ReadOnly = Status
        ComboBoxEdit101.Properties.ReadOnly = Status
        ComboBoxEdit102.Properties.ReadOnly = Status
        ComboBoxEdit103.Properties.ReadOnly = Status
        ComboBoxEdit104.Properties.ReadOnly = Status
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
        MemoEdit1.Properties.ReadOnly = Status
        MemoEdit2.Properties.ReadOnly = Status
        MemoEdit3.Properties.ReadOnly = Status
        MemoEdit4.Properties.ReadOnly = Status
        MemoEdit5.Properties.ReadOnly = Status
        'picGAMBAR2.Properties.ReadOnly = Status
        txtWAKTUSELESAI.Properties.ReadOnly = Status

    End Sub
    Private Sub fn_EmptyMe()
        deDATE.DateTime = Now
        txtWAKTU.Text = now
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
        ComboBoxEdit1.ResetText()
        ComboBoxEdit2.ResetText()
        ComboBoxEdit3.ResetText()
        ComboBoxEdit4.ResetText()
        ComboBoxEdit5.ResetText()
        ComboBoxEdit6.ResetText()
        ComboBoxEdit7.ResetText()
        ComboBoxEdit8.ResetText()
        ComboBoxEdit9.ResetText()
        ComboBoxEdit10.ResetText()
        ComboBoxEdit11.ResetText()
        ComboBoxEdit12.ResetText()
        ComboBoxEdit13.ResetText()
        ComboBoxEdit14.ResetText()
        ComboBoxEdit15.ResetText()
        ComboBoxEdit16.ResetText()
        ComboBoxEdit17.ResetText()
        ComboBoxEdit18.ResetText()
        ComboBoxEdit19.ResetText()
        ComboBoxEdit20.ResetText()
        ComboBoxEdit21.ResetText()
        ComboBoxEdit22.ResetText()
        ComboBoxEdit23.ResetText()
        ComboBoxEdit24.ResetText()
        ComboBoxEdit25.ResetText()
        ComboBoxEdit26.ResetText()
        ComboBoxEdit27.ResetText()
        ComboBoxEdit28.ResetText()
        ComboBoxEdit29.ResetText()
        ComboBoxEdit30.ResetText()
        ComboBoxEdit31.ResetText()
        ComboBoxEdit32.ResetText()
        ComboBoxEdit33.ResetText()
        ComboBoxEdit34.ResetText()
        ComboBoxEdit35.ResetText()
        ComboBoxEdit36.ResetText()
        ComboBoxEdit37.ResetText()
        ComboBoxEdit38.ResetText()
        ComboBoxEdit39.ResetText()
        ComboBoxEdit40.ResetText()
        ComboBoxEdit41.ResetText()
        ComboBoxEdit42.ResetText()
        ComboBoxEdit43.ResetText()
        ComboBoxEdit44.ResetText()
        ComboBoxEdit45.ResetText()
        ComboBoxEdit46.ResetText()
        ComboBoxEdit47.ResetText()
        ComboBoxEdit48.ResetText()
        ComboBoxEdit49.ResetText()
        ComboBoxEdit50.ResetText()
        ComboBoxEdit51.ResetText()
        ComboBoxEdit52.ResetText()
        ComboBoxEdit53.ResetText()
        ComboBoxEdit54.ResetText()
        ComboBoxEdit55.ResetText()
        ComboBoxEdit56.ResetText()
        ComboBoxEdit57.ResetText()
        ComboBoxEdit58.ResetText()
        ComboBoxEdit59.ResetText()
        ComboBoxEdit60.ResetText()
        ComboBoxEdit61.ResetText()
        ComboBoxEdit62.ResetText()
        ComboBoxEdit63.ResetText()
        ComboBoxEdit64.ResetText()
        ComboBoxEdit65.ResetText()
        ComboBoxEdit66.ResetText()
        ComboBoxEdit67.ResetText()
        ComboBoxEdit68.ResetText()
        ComboBoxEdit69.ResetText()
        ComboBoxEdit70.ResetText()
        ComboBoxEdit71.ResetText()
        ComboBoxEdit72.ResetText()
        ComboBoxEdit73.ResetText()
        ComboBoxEdit74.ResetText()
        ComboBoxEdit75.ResetText()
        ComboBoxEdit76.ResetText()
        ComboBoxEdit77.ResetText()
        ComboBoxEdit78.ResetText()
        ComboBoxEdit79.ResetText()
        ComboBoxEdit80.ResetText()
        ComboBoxEdit81.ResetText()
        ComboBoxEdit82.ResetText()
        ComboBoxEdit83.ResetText()
        ComboBoxEdit84.ResetText()
        ComboBoxEdit85.ResetText()
        ComboBoxEdit86.ResetText()
        ComboBoxEdit87.ResetText()
        ComboBoxEdit88.ResetText()
        ComboBoxEdit89.ResetText()
        ComboBoxEdit90.ResetText()
        ComboBoxEdit91.ResetText()
        ComboBoxEdit92.ResetText()
        ComboBoxEdit93.ResetText()
        ComboBoxEdit94.ResetText()
        ComboBoxEdit95.ResetText()
        ComboBoxEdit96.ResetText()
        ComboBoxEdit97.ResetText()
        ComboBoxEdit98.ResetText()
        ComboBoxEdit99.ResetText()
        ComboBoxEdit100.ResetText()
        ComboBoxEdit101.ResetText()
        ComboBoxEdit102.ResetText()
        ComboBoxEdit103.ResetText()
        ComboBoxEdit104.ResetText()
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
        MemoEdit1.ResetText()
        MemoEdit2.ResetText()
        MemoEdit3.ResetText()
        MemoEdit4.ResetText()
        MemoEdit5.ResetText()
        txtWAKTUSELESAI.ResetText()
        grdDOCTOR.Text = sKDDOKTER

        fn_LoadAsessmenRawatJalan(txtNOREG.Text)
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RJ_NEURO.GetData(txtNOREG.Text)

            With ds
                deDATE.DateTime = .DATE
                grdDOCTOR.EditValue = .DOKTER_KODE
                txtWAKTU.Text = .WAKTUAWAL
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
                ComboBoxEdit1.Text = .ComboBoxEdit1
                ComboBoxEdit2.Text = .ComboBoxEdit2
                ComboBoxEdit3.Text = .ComboBoxEdit3
                ComboBoxEdit4.Text = .ComboBoxEdit4
                ComboBoxEdit5.Text = .ComboBoxEdit5
                ComboBoxEdit6.Text = .ComboBoxEdit6
                ComboBoxEdit7.Text = .ComboBoxEdit7
                ComboBoxEdit8.Text = .ComboBoxEdit8
                ComboBoxEdit9.Text = .ComboBoxEdit9
                ComboBoxEdit10.Text = .ComboBoxEdit10
                ComboBoxEdit11.Text = .ComboBoxEdit11
                ComboBoxEdit12.Text = .ComboBoxEdit12
                ComboBoxEdit13.Text = .ComboBoxEdit13
                ComboBoxEdit14.Text = .ComboBoxEdit14
                ComboBoxEdit15.Text = .ComboBoxEdit15
                ComboBoxEdit16.Text = .ComboBoxEdit16
                ComboBoxEdit17.Text = .ComboBoxEdit17
                ComboBoxEdit18.Text = .ComboBoxEdit18
                ComboBoxEdit19.Text = .ComboBoxEdit19
                ComboBoxEdit20.Text = .ComboBoxEdit20
                ComboBoxEdit21.Text = .ComboBoxEdit21
                ComboBoxEdit22.Text = .ComboBoxEdit22
                ComboBoxEdit23.Text = .ComboBoxEdit23
                ComboBoxEdit24.Text = .ComboBoxEdit24
                ComboBoxEdit25.Text = .ComboBoxEdit25
                ComboBoxEdit26.Text = .ComboBoxEdit26
                ComboBoxEdit27.Text = .ComboBoxEdit27
                ComboBoxEdit28.Text = .ComboBoxEdit28
                ComboBoxEdit29.Text = .ComboBoxEdit29
                ComboBoxEdit30.Text = .ComboBoxEdit30
                ComboBoxEdit31.Text = .ComboBoxEdit31
                ComboBoxEdit32.Text = .ComboBoxEdit32
                ComboBoxEdit33.Text = .ComboBoxEdit33
                ComboBoxEdit34.Text = .ComboBoxEdit34
                ComboBoxEdit35.Text = .ComboBoxEdit35
                ComboBoxEdit36.Text = .ComboBoxEdit36
                ComboBoxEdit37.Text = .ComboBoxEdit37
                ComboBoxEdit38.Text = .ComboBoxEdit38
                ComboBoxEdit39.Text = .ComboBoxEdit39
                ComboBoxEdit40.Text = .ComboBoxEdit40
                ComboBoxEdit41.Text = .ComboBoxEdit41
                ComboBoxEdit42.Text = .ComboBoxEdit42
                ComboBoxEdit43.Text = .ComboBoxEdit43
                ComboBoxEdit44.Text = .ComboBoxEdit44
                ComboBoxEdit45.Text = .ComboBoxEdit45
                ComboBoxEdit46.Text = .ComboBoxEdit46
                ComboBoxEdit47.Text = .ComboBoxEdit47
                ComboBoxEdit48.Text = .ComboBoxEdit48
                ComboBoxEdit49.Text = .ComboBoxEdit49
                ComboBoxEdit50.Text = .ComboBoxEdit50
                ComboBoxEdit51.Text = .ComboBoxEdit51
                ComboBoxEdit52.Text = .ComboBoxEdit52
                ComboBoxEdit53.Text = .ComboBoxEdit53
                ComboBoxEdit54.Text = .ComboBoxEdit54
                ComboBoxEdit55.Text = .ComboBoxEdit55
                ComboBoxEdit56.Text = .ComboBoxEdit56
                ComboBoxEdit57.Text = .ComboBoxEdit57
                ComboBoxEdit58.Text = .ComboBoxEdit58
                ComboBoxEdit59.Text = .ComboBoxEdit59
                ComboBoxEdit60.Text = .ComboBoxEdit60
                ComboBoxEdit61.Text = .ComboBoxEdit61
                ComboBoxEdit62.Text = .ComboBoxEdit62
                ComboBoxEdit63.Text = .ComboBoxEdit63
                ComboBoxEdit64.Text = .ComboBoxEdit64
                ComboBoxEdit65.Text = .ComboBoxEdit65
                ComboBoxEdit66.Text = .ComboBoxEdit66
                ComboBoxEdit67.Text = .ComboBoxEdit67
                ComboBoxEdit68.Text = .ComboBoxEdit68
                ComboBoxEdit69.Text = .ComboBoxEdit69
                ComboBoxEdit70.Text = .ComboBoxEdit70
                ComboBoxEdit71.Text = .ComboBoxEdit71
                ComboBoxEdit72.Text = .ComboBoxEdit72
                ComboBoxEdit73.Text = .ComboBoxEdit73
                ComboBoxEdit74.Text = .ComboBoxEdit74
                ComboBoxEdit75.Text = .ComboBoxEdit75
                ComboBoxEdit76.Text = .ComboBoxEdit76
                ComboBoxEdit77.Text = .ComboBoxEdit77
                ComboBoxEdit78.Text = .ComboBoxEdit78
                ComboBoxEdit79.Text = .ComboBoxEdit79
                ComboBoxEdit80.Text = .ComboBoxEdit80
                ComboBoxEdit81.Text = .ComboBoxEdit81
                ComboBoxEdit82.Text = .ComboBoxEdit82
                ComboBoxEdit83.Text = .ComboBoxEdit83
                ComboBoxEdit84.Text = .ComboBoxEdit84
                ComboBoxEdit85.Text = .ComboBoxEdit85
                ComboBoxEdit86.Text = .ComboBoxEdit86
                ComboBoxEdit87.Text = .ComboBoxEdit87
                ComboBoxEdit88.Text = .ComboBoxEdit88
                ComboBoxEdit89.Text = .ComboBoxEdit89
                ComboBoxEdit90.Text = .ComboBoxEdit90
                ComboBoxEdit91.Text = .ComboBoxEdit91
                ComboBoxEdit92.Text = .ComboBoxEdit92
                ComboBoxEdit93.Text = .ComboBoxEdit93
                ComboBoxEdit94.Text = .ComboBoxEdit94
                ComboBoxEdit95.Text = .ComboBoxEdit95
                ComboBoxEdit96.Text = .ComboBoxEdit96
                ComboBoxEdit97.Text = .ComboBoxEdit97
                ComboBoxEdit98.Text = .ComboBoxEdit98
                ComboBoxEdit99.Text = .ComboBoxEdit99
                ComboBoxEdit100.Text = .ComboBoxEdit100
                ComboBoxEdit101.Text = .ComboBoxEdit101
                ComboBoxEdit102.Text = .ComboBoxEdit102
                ComboBoxEdit103.Text = .ComboBoxEdit103
                ComboBoxEdit104.Text = .ComboBoxEdit104
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
                MemoEdit1.Text = .MemoEdit1
                MemoEdit2.Text = .MemoEdit2
                MemoEdit3.Text = .MemoEdit3
                MemoEdit4.Text = .MemoEdit4
                MemoEdit5.Text = .MemoEdit5
                txtWAKTUSELESAI.Text = .WAKTUSELESAI

                Try
                    Dim img = oS_DIGITAL_RJ_NEURO.GetData(txtNOREG.Text).picGAMBAR2

                    picGAMBAR2.Image = ByteArrayToImage(img.ToArray())
                Catch oErr As Exception
                End Try
            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadAsessmenRawatJalan(ByVal Parameter As String)
        Dim oAsessmenRawatJalan As New Digital.clsDigital_RJ_08
        Dim dsAsessmenRawatJalan = oAsessmenRawatJalan.GetData(Parameter)

        Dim oAsessmenRawatJalan2 As New Digital.clsS_DIGITAL_ASKEP_RAWATJALAN
        Dim dsAsessmenRawatJalan2 = oAsessmenRawatJalan2.GetDatabyKDKUNJUNGAN(Parameter)

        If dsAsessmenRawatJalan IsNot Nothing Then
            'txtSOAP_O.Text = "Tensi : " & dsAsessmenRawatJalan.TANDA_VITAL_01 & " mmHg" & vbCrLf & vbCrLf & "Nadi : " & dsAsessmenRawatJalan.TANDA_VITAL_02 & " x/m" & vbCrLf & vbCrLf & "Respirasi : " & dsAsessmenRawatJalan.TANDA_VITAL_04 & " x/m" & vbCrLf & vbCrLf & "Suhu : " & dsAsessmenRawatJalan.TANDA_VITAL_03 & " °C" & vbCrLf & vbCrLf & "Berat Badan : " & dsAsessmenRawatJalan.TANDA_VITAL_05 & " Kg" & vbCrLf & vbCrLf & "Tinggi Badan : " & dsAsessmenRawatJalan.TANDA_VITAL_06 & " Cm"
            'keluhan utama
            TextEdit1.Text = dsAsessmenRawatJalan.KELUHAN_UTAMA
            'bb
            TextEdit7.Text = dsAsessmenRawatJalan.TANDA_VITAL_05
            'tb
            TextEdit8.Text = dsAsessmenRawatJalan.TANDA_VITAL_06
            'tensi
            TextEdit9.Text = dsAsessmenRawatJalan.TANDA_VITAL_01
            'nadi
            TextEdit10.Text = dsAsessmenRawatJalan.TANDA_VITAL_02
            'suhu
            TextEdit11.Text = dsAsessmenRawatJalan.TANDA_VITAL_03
            'respirasi
            TextEdit12.Text = dsAsessmenRawatJalan.TANDA_VITAL_04
        ElseIf dsAsessmenRawatJalan2 IsNot Nothing Then
            'keluhan utama
            TextEdit1.Text = dsAsessmenRawatJalan2.KELUHAN_UTAMA
            'bb
            TextEdit7.Text = dsAsessmenRawatJalan2.TANDA_VITAL_BB
            'tb
            TextEdit8.Text = dsAsessmenRawatJalan2.TANDA_VITAL_TB
            'tensi
            TextEdit9.Text = dsAsessmenRawatJalan2.TANDA_VITAL_TD
            'nadi
            TextEdit10.Text = dsAsessmenRawatJalan2.TANDA_VITAL_NADI
            'suhu
            TextEdit11.Text = dsAsessmenRawatJalan2.TANDA_VITAL_SUHU
            'respirasi
            TextEdit12.Text = dsAsessmenRawatJalan2.TANDA_VITAL_R
        Else
            Exit Sub
        End If
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
            If txtNOREG.Text = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                txtNOREG.Focus()
                fn_Validate = False
                Exit Function
            End If
             If grdDOCTOR.Text = String.Empty Then
                MsgBox("Dibutuhkan Dokter", MsgBoxStyle.Exclamation, Me.Text)
                grdDOCTOR.Focus()
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
            Dim ds = oS_DIGITAL_RJ_NEURO.GetStructureHeader
            With ds
                .KDKUNJUNGAN = txtNOREG.Text
                Try
                    .DATECREATED = oS_DIGITAL_RJ_NEURO.GetData(txtNOREG.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime

                .WAKTUAWAL = txtWAKTU.Text
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
                .ComboBoxEdit1 = ComboBoxEdit1.Text
                .ComboBoxEdit2 = ComboBoxEdit2.Text
                .ComboBoxEdit3 = ComboBoxEdit3.Text
                .ComboBoxEdit4 = ComboBoxEdit4.Text
                .ComboBoxEdit5 = ComboBoxEdit5.Text
                .ComboBoxEdit6 = ComboBoxEdit6.Text
                .ComboBoxEdit7 = ComboBoxEdit7.Text
                .ComboBoxEdit8 = ComboBoxEdit8.Text
                .ComboBoxEdit9 = ComboBoxEdit9.Text
                .ComboBoxEdit10 = ComboBoxEdit10.Text
                .ComboBoxEdit11 = ComboBoxEdit11.Text
                .ComboBoxEdit12 = ComboBoxEdit12.Text
                .ComboBoxEdit13 = ComboBoxEdit13.Text
                .ComboBoxEdit14 = ComboBoxEdit14.Text
                .ComboBoxEdit15 = ComboBoxEdit15.Text
                .ComboBoxEdit16 = ComboBoxEdit16.Text
                .ComboBoxEdit17 = ComboBoxEdit17.Text
                .ComboBoxEdit18 = ComboBoxEdit18.Text
                .ComboBoxEdit19 = ComboBoxEdit19.Text
                .ComboBoxEdit20 = ComboBoxEdit20.Text
                .ComboBoxEdit21 = ComboBoxEdit21.Text
                .ComboBoxEdit22 = ComboBoxEdit22.Text
                .ComboBoxEdit23 = ComboBoxEdit23.Text
                .ComboBoxEdit24 = ComboBoxEdit24.Text
                .ComboBoxEdit25 = ComboBoxEdit25.Text
                .ComboBoxEdit26 = ComboBoxEdit26.Text
                .ComboBoxEdit27 = ComboBoxEdit27.Text
                .ComboBoxEdit28 = ComboBoxEdit28.Text
                .ComboBoxEdit29 = ComboBoxEdit29.Text
                .ComboBoxEdit30 = ComboBoxEdit30.Text
                .ComboBoxEdit31 = ComboBoxEdit31.Text
                .ComboBoxEdit32 = ComboBoxEdit32.Text
                .ComboBoxEdit33 = ComboBoxEdit33.Text
                .ComboBoxEdit34 = ComboBoxEdit34.Text
                .ComboBoxEdit35 = ComboBoxEdit35.Text
                .ComboBoxEdit36 = ComboBoxEdit36.Text
                .ComboBoxEdit37 = ComboBoxEdit37.Text
                .ComboBoxEdit38 = ComboBoxEdit38.Text
                .ComboBoxEdit39 = ComboBoxEdit39.Text
                .ComboBoxEdit40 = ComboBoxEdit40.Text
                .ComboBoxEdit41 = ComboBoxEdit41.Text
                .ComboBoxEdit42 = ComboBoxEdit42.Text
                .ComboBoxEdit43 = ComboBoxEdit43.Text
                .ComboBoxEdit44 = ComboBoxEdit44.Text
                .ComboBoxEdit45 = ComboBoxEdit45.Text
                .ComboBoxEdit46 = ComboBoxEdit46.Text
                .ComboBoxEdit47 = ComboBoxEdit47.Text
                .ComboBoxEdit48 = ComboBoxEdit48.Text
                .ComboBoxEdit49 = ComboBoxEdit49.Text
                .ComboBoxEdit50 = ComboBoxEdit50.Text
                .ComboBoxEdit51 = ComboBoxEdit51.Text
                .ComboBoxEdit52 = ComboBoxEdit52.Text
                .ComboBoxEdit53 = ComboBoxEdit53.Text
                .ComboBoxEdit54 = ComboBoxEdit54.Text
                .ComboBoxEdit55 = ComboBoxEdit55.Text
                .ComboBoxEdit56 = ComboBoxEdit56.Text
                .ComboBoxEdit57 = ComboBoxEdit57.Text
                .ComboBoxEdit58 = ComboBoxEdit58.Text
                .ComboBoxEdit59 = ComboBoxEdit59.Text
                .ComboBoxEdit60 = ComboBoxEdit60.Text
                .ComboBoxEdit61 = ComboBoxEdit61.Text
                .ComboBoxEdit62 = ComboBoxEdit62.Text
                .ComboBoxEdit63 = ComboBoxEdit63.Text
                .ComboBoxEdit64 = ComboBoxEdit64.Text
                .ComboBoxEdit65 = ComboBoxEdit65.Text
                .ComboBoxEdit66 = ComboBoxEdit66.Text
                .ComboBoxEdit67 = ComboBoxEdit67.Text
                .ComboBoxEdit68 = ComboBoxEdit68.Text
                .ComboBoxEdit69 = ComboBoxEdit69.Text
                .ComboBoxEdit70 = ComboBoxEdit70.Text
                .ComboBoxEdit71 = ComboBoxEdit71.Text
                .ComboBoxEdit72 = ComboBoxEdit72.Text
                .ComboBoxEdit73 = ComboBoxEdit73.Text
                .ComboBoxEdit74 = ComboBoxEdit74.Text
                .ComboBoxEdit75 = ComboBoxEdit75.Text
                .ComboBoxEdit76 = ComboBoxEdit76.Text
                .ComboBoxEdit77 = ComboBoxEdit77.Text
                .ComboBoxEdit78 = ComboBoxEdit78.Text
                .ComboBoxEdit79 = ComboBoxEdit79.Text
                .ComboBoxEdit80 = ComboBoxEdit80.Text
                .ComboBoxEdit81 = ComboBoxEdit81.Text
                .ComboBoxEdit82 = ComboBoxEdit82.Text
                .ComboBoxEdit83 = ComboBoxEdit83.Text
                .ComboBoxEdit84 = ComboBoxEdit84.Text
                .ComboBoxEdit85 = ComboBoxEdit85.Text
                .ComboBoxEdit86 = ComboBoxEdit86.Text
                .ComboBoxEdit87 = ComboBoxEdit87.Text
                .ComboBoxEdit88 = ComboBoxEdit88.Text
                .ComboBoxEdit89 = ComboBoxEdit89.Text
                .ComboBoxEdit90 = ComboBoxEdit90.Text
                .ComboBoxEdit91 = ComboBoxEdit91.Text
                .ComboBoxEdit92 = ComboBoxEdit92.Text
                .ComboBoxEdit93 = ComboBoxEdit93.Text
                .ComboBoxEdit94 = ComboBoxEdit94.Text
                .ComboBoxEdit95 = ComboBoxEdit95.Text
                .ComboBoxEdit96 = ComboBoxEdit96.Text
                .ComboBoxEdit97 = ComboBoxEdit97.Text
                .ComboBoxEdit98 = ComboBoxEdit98.Text
                .ComboBoxEdit99 = ComboBoxEdit99.Text
                .ComboBoxEdit100 = ComboBoxEdit100.Text
                .ComboBoxEdit101 = ComboBoxEdit101.Text
                .ComboBoxEdit102 = ComboBoxEdit102.Text
                .ComboBoxEdit103 = ComboBoxEdit103.Text
                .ComboBoxEdit104 = ComboBoxEdit104.Text
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
                .MemoEdit1 = MemoEdit1.Text
                .MemoEdit2 = MemoEdit2.Text
                .MemoEdit3 = MemoEdit3.Text
                .MemoEdit4 = MemoEdit4.Text
                .MemoEdit5 = MemoEdit5.Text
                .WAKTUSELESAI = txtWAKTUSELESAI.Text

                Try
                    Dim ms As New IO.MemoryStream()
                    picGAMBAR2.Image.Save(ms, picGAMBAR2.Image.RawFormat)

                    Dim data As Byte() = ms.GetBuffer()

                    .picGAMBAR2 = data
                Catch oErr As Exception
                    Try
                        .picGAMBAR2 = oS_DIGITAL_RJ_NEURO.GetData(txtNOREG.Text).picGAMBAR2
                    Catch ex As Exception

                    End Try
                End Try

                .DOKTER_KODE = grdDOCTOR.EditValue
                .DOKTER_NAMEDISPLAY = grdDOCTOR.Text

                Try
                    .CETAK = oS_DIGITAL_RJ_NEURO.GetData(txtNOREG.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                .KDUSER = sUserID
                .KDUSER_SIGNATURE = sUserSIGNATURE
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_RJ_NEURO.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_RJ_NEURO.UpdateData(ds)
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
            Case Keys.F5
                If btnReload.Enabled = True Then
                    btnReload_Click()
                End If
            Case Keys.F6
                If btnDiagnosa.Enabled = True Then
                    btnDiagnosa_Click()
                End If
        End Select
    End Sub
    Private Sub btnDiagnosa_Click() Handles btnDiagnosa.ItemClick
        Dim oMasterDiagnosa As New Diagnosa.clsMasterDiagnosa
        Dim dsMasterDiagnosa = oMasterDiagnosa.GetData(txtNOREG.Text)
        If dsMasterDiagnosa IsNot Nothing Then
            Dim frmMasterDiagnosa As New frmMasterDiagnosa
            frmMasterDiagnosa.LoadMe(FORM_MODE.FORM_MODE_EDIT, txtNOREG.Text)
            frmMasterDiagnosa.ShowDialog(Me)
        Else
            Dim frmMasterDiagnosa As New frmMasterDiagnosa
            frmMasterDiagnosa.LoadMe(FORM_MODE.FORM_MODE_ADD, txtNOREG.Text)
            frmMasterDiagnosa.ShowDialog(Me)
        End If

        If sCode = "Berhasil" Then
            Dim listDiagnosa As New List(Of String)
            For Each xloop In oMasterDiagnosa.GetDataDetail(txtNOREG.Text)
                listDiagnosa.Add(xloop.KATEGORI & " : " & xloop.REMARKS)
            Next

            Dim listProsedur As New List(Of String)
            For Each xloop In oMasterDiagnosa.GetDataDetail_(txtNOREG.Text)
                listProsedur.Add(xloop.SEQ + 1 & ". " & xloop.REMARKS)
            Next
            MemoEdit1.Text = String.Join(vbCrLf, listProsedur.ToArray)
            MemoEdit2.Text = String.Join(vbCrLf, listDiagnosa.ToArray)
        End If
    End Sub
    Private Sub btnReload_Click() Handles btnReload.ItemClick
        Dim dsKunjungan = oS_DIGITAL_RJ_NEURO.GetDataByKunjungan(txtNOREG.Text)

        Dim listPenunjang As New List(Of String)
        Dim listTindakanPengobatan As New List(Of String)

        If dsKunjungan IsNot Nothing Then
            Dim oOrderTindakan As New Inventory.clsOrderTindakan
            Dim oOrderLab As New Inventory.clsOrderLab
            Dim oOrderRad As New Inventory.clsOrderRad
            Dim oKonsul As New Digital.clsKonsul
            Dim oKonsulJawab As New Digital.clsJawabKonsul

            Dim dsTindakan = From x In oOrderTindakan.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
                             Join y In oOrderTindakan.GetDataDetail()
                             On x.KDORDERTINDAKAN Equals y.KDORDERTINDAKAN
                             Select TANGGAL = x.DATE, ITEM = "Tindakan : " & y.TARIFKT & " " & y.REMARKS & ", "

            Dim dsLab = From x In oOrderLab.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
                        Join y In oOrderLab.GetDataDetail()
                        On x.KDORDERLAB Equals y.KDORDERLAB
                        Select TANGGAL = x.DATE, ITEM = "Lab : " & y.TARIFKT & " " & y.REMARKS & ", "

            Dim dsRad = From x In oOrderRad.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
                        Join y In oOrderRad.GetDataDetail()
                        On x.KDORDERRAD Equals y.KDORDERRAD
                        Select TANGGAL = x.DATE, ITEM = "Rad : " & y.TARIFKT & " " & y.REMARKS & ", "

            Dim dsKonsul = From x In oKonsul.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
                           Select TANGGAL = x.DATE, ITEM = "Konsul : " & "Dari " & x.DOKTER_DARI & " Kepada " & x.DOKTER_KEPADA & " Isi " & x.MEMO & ", "

            Dim dsJawabKonsul = From x In oKonsulJawab.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
                                Select TANGGAL = x.DATE, ITEM = "Jawab Konsul : " & "Dari " & x.DOKTER_DARI & " Kepada " & x.DOKTER_KEPADA & " Isi " & x.MEMO & ", "

            Dim dsUnionPenunjang = dsLab.Union(dsRad)

            For Each xloop In dsUnionPenunjang.OrderBy(Function(x) x.TANGGAL)
                listPenunjang.Add("*) " & xloop.TANGGAL.ToString("dd-MM-yyy HH:mm:ss") & " " & xloop.ITEM)
            Next

            Dim dsUnionTindakanKonsul = dsTindakan.Union(dsKonsul).Union(dsJawabKonsul)

            For Each xloop In dsUnionTindakanKonsul.OrderBy(Function(x) x.TANGGAL)
                listTindakanPengobatan.Add("*) " & xloop.TANGGAL.ToString("dd-MM-yyy HH:mm:ss") & " " & xloop.ITEM)
            Next

            Dim oResep As New Inventory.clsOrderResep

            Dim dsResep = oResep.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)

            For Each xloop In dsResep
                For Each yloop In oResep.GetDataDetail(xloop.KDORDERRESEP)
                    listTindakanPengobatan.Add(yloop.TARIFKT & " " & yloop.SIGNA)
                Next
            Next

            'MemoEdit7.Text = String.Join(", ", listPenunjang.ToArray)
            MemoEdit3.Text = String.Join(", ", listTindakanPengobatan.ToArray)
        End If
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & txtNOREG.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtNOREG.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_NOIDUSER()

    End Sub
    Private Sub fn_DOCTOR()
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

            grdDOCTOR.Properties.DataSource = ds.Tables("DOKTER")
            grdDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnEDITIMAGE_Click(sender As Object, e As EventArgs) Handles btnEDITIMAGE.Click
        Dim frmPopUp_Image As New frmPopUp_img20
        frmPopUp_Image.fn_LoadMe(picGAMBAR2.Image)
        frmPopUp_Image.ShowDialog()

        If sPicture IsNot Nothing Then
            picGAMBAR2.Image = sPicture
        End If

        picGAMBAR2.Focus()
        sFind10 = String.Empty
        sPicture = Nothing
    End Sub
#End Region
End Class