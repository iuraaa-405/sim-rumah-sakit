Imports System.Data.SqlClient
Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports DevExpress.XtraSplashScreen

Public Class frmEMedrekRJ_38
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_RJ_38 As New Digital.clsDigital_RJ_38
    Private down As Boolean = False
    Private sIsOtority As Boolean = False
    Private sKoneksi As String = String.Empty
    Private sKoneksiOld As String = String.Empty

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String)
        oFormMode = FormMode

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
            txtTanggal.Text = dsPendaftaran.DATE
            txtWAKTU.Text = Now

            TextEdit48.Text = dsPendaftaran.NIK
            TextEdit49.Text = dsPendaftaran.AGAMA
            Textedit50.Text = dsPendaftaran.PANGKAT
            textEdit51.Text = dsPendaftaran.NRP
            TextEdit52.Text = dsPendaftaran.KESATUAN
            TextEdit53.Text = dsPendaftaran.PENDIDIKAN
            TextEdit54.Text = dsPendaftaran.NOMORTELEPON
            Textedit55.Text = dsPendaftaran.SUKU
            Textedit56.Text = dsPendaftaran.ALAMAT

        Else
            txtNAMA.ResetText()
            txtJK.ResetText()
            txtUmur.ResetText()
            txtKELAS.ResetText()
            txtNOMORRM.ResetText()
            txtNOREG.ResetText()
            txtWAKTU.ResetText()
            txtTanggal.ResetText()

            TextEdit48.ResetText()
            TextEdit49.ResetText()
            TextEdit50.ResetText()
            TextEdit51.ResetText()
            TextEdit52.ResetText()
            TextEdit53.ResetText()
            TextEdit54.ResetText()
            TextEdit55.ResetText()
            TextEdit56.ResetText()
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = EMedrekRJ_38.TITLE
        Dim oSetKoneksi As New Setting.clsSetKoneksi
        Dim dsSetKoneksi = oSetKoneksi.GetDataVCLaim()
        If dsSetKoneksi IsNot Nothing Then
            sKoneksi = dsSetKoneksi.KONEKSI
            sKoneksiOld = dsSetKoneksi.GENERATE_ECLAIM
        End If
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
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
        fn_DEPARTMENT()
        fn_LoadKDDOCTOR()
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
        CheckEdit1.Properties.ReadOnly = Status
        CheckEdit2.Properties.ReadOnly = Status
        CheckEdit3.Properties.ReadOnly = Status
        CheckEdit4.Properties.ReadOnly = Status
        txt9.Properties.ReadOnly = Status
        CheckEdit5.Properties.ReadOnly = Status
        CheckEdit6.Properties.ReadOnly = Status
        CheckEdit7.Properties.ReadOnly = Status
        CheckEdit8.Properties.ReadOnly = Status
        CheckEdit9.Properties.ReadOnly = Status
        CheckEdit10.Properties.ReadOnly = Status
        CheckEdit11.Properties.ReadOnly = Status
        CheckEdit12.Properties.ReadOnly = Status
        CheckEdit14.Properties.ReadOnly = Status
        CheckEdit13.Properties.ReadOnly = Status
        TextEdit4.Properties.ReadOnly = Status
        TextEdit5.Properties.ReadOnly = Status
        CheckEdit16.Properties.ReadOnly = Status
        CheckEdit15.Properties.ReadOnly = Status
        CheckEdit17.Properties.ReadOnly = Status
        TextEdit1.Properties.ReadOnly = Status
        TextEdit2.Properties.ReadOnly = Status
        TextEdit3.Properties.ReadOnly = Status
        CheckEdit18.Properties.ReadOnly = Status
        CheckEdit19.Properties.ReadOnly = Status
        TextEdit6.Properties.ReadOnly = Status
        CheckEdit20.Properties.ReadOnly = Status
        CheckEdit21.Properties.ReadOnly = Status
        TextEdit7.Properties.ReadOnly = Status
        CheckEdit22.Properties.ReadOnly = Status
        CheckEdit23.Properties.ReadOnly = Status
        CheckEdit24.Properties.ReadOnly = Status
        CheckEdit25.Properties.ReadOnly = Status
        CheckEdit26.Properties.ReadOnly = Status
        TextEdit8.Properties.ReadOnly = Status
        CheckEdit27.Properties.ReadOnly = Status
        CheckEdit28.Properties.ReadOnly = Status
        CheckEdit29.Properties.ReadOnly = Status
        TextEdit9.Properties.ReadOnly = Status
        CheckEdit30.Properties.ReadOnly = Status
        CheckEdit31.Properties.ReadOnly = Status
        CheckEdit32.Properties.ReadOnly = Status
        CheckEdit33.Properties.ReadOnly = Status
        CheckEdit34.Properties.ReadOnly = Status
        TextEdit10.Properties.ReadOnly = Status
        TextEdit11.Properties.ReadOnly = Status
        TextEdit13.Properties.ReadOnly = Status
        TextEdit12.Properties.ReadOnly = Status
        CheckEdit35.Properties.ReadOnly = Status
        CheckEdit36.Properties.ReadOnly = Status
        CheckEdit37.Properties.ReadOnly = Status
        CheckEdit38.Properties.ReadOnly = Status
        CheckEdit39.Properties.ReadOnly = Status
        TextEdit14.Properties.ReadOnly = Status
        CheckEdit44.Properties.ReadOnly = Status
        CheckEdit45.Properties.ReadOnly = Status
        CheckEdit46.Properties.ReadOnly = Status
        CheckEdit47.Properties.ReadOnly = Status
        CheckEdit48.Properties.ReadOnly = Status
        CheckEdit49.Properties.ReadOnly = Status
        CheckEdit50.Properties.ReadOnly = Status
        CheckEdit52.Properties.ReadOnly = Status
        CheckEdit51.Properties.ReadOnly = Status
        CheckEdit53.Properties.ReadOnly = Status
        CheckEdit54.Properties.ReadOnly = Status
        CheckEdit55.Properties.ReadOnly = Status
        CheckEdit56.Properties.ReadOnly = Status
        CheckEdit57.Properties.ReadOnly = Status
        CheckEdit40.Properties.ReadOnly = Status
        CheckEdit41.Properties.ReadOnly = Status
        CheckEdit43.Properties.ReadOnly = Status
        CheckEdit58.Properties.ReadOnly = Status
        CheckEdit59.Properties.ReadOnly = Status
        CheckEdit61.Properties.ReadOnly = Status
        CheckEdit60.Properties.ReadOnly = Status
        CheckEdit64.Properties.ReadOnly = Status
        CheckEdit63.Properties.ReadOnly = Status
        CheckEdit62.Properties.ReadOnly = Status
        CheckEdit66.Properties.ReadOnly = Status
        CheckEdit65.Properties.ReadOnly = Status
        CheckEdit69.Properties.ReadOnly = Status
        CheckEdit68.Properties.ReadOnly = Status
        CheckEdit67.Properties.ReadOnly = Status
        CheckEdit71.Properties.ReadOnly = Status
        CheckEdit70.Properties.ReadOnly = Status
        TextEdit15.Properties.ReadOnly = Status
        MemoEdit1.Properties.ReadOnly = Status
        DateEdit1.Properties.ReadOnly = Status
        CheckEdit72.Properties.ReadOnly = Status
        TextEdit16.Properties.ReadOnly = Status
        CheckEdit73.Properties.ReadOnly = Status
        CheckEdit74.Properties.ReadOnly = Status
        CheckEdit75.Properties.ReadOnly = Status
        TextEdit17.Properties.ReadOnly = Status
        CheckEdit77.Properties.ReadOnly = Status
        CheckEdit76.Properties.ReadOnly = Status
        CheckEdit78.Properties.ReadOnly = Status
        CheckEdit79.Properties.ReadOnly = Status
        TextEdit18.Properties.ReadOnly = Status
        CheckEdit81.Properties.ReadOnly = Status
        CheckEdit80.Properties.ReadOnly = Status
        TextEdit19.Properties.ReadOnly = Status
        CheckEdit83.Properties.ReadOnly = Status
        CheckEdit82.Properties.ReadOnly = Status
        CheckEdit84.Properties.ReadOnly = Status
        CheckEdit85.Properties.ReadOnly = Status
        CheckEdit86.Properties.ReadOnly = Status
        TextEdit20.Properties.ReadOnly = Status
        CheckEdit87.Properties.ReadOnly = Status
        CheckEdit88.Properties.ReadOnly = Status
        CheckEdit89.Properties.ReadOnly = Status
        CheckEdit92.Properties.ReadOnly = Status
        CheckEdit91.Properties.ReadOnly = Status
        CheckEdit90.Properties.ReadOnly = Status
        CheckEdit95.Properties.ReadOnly = Status
        TextEdit21.Properties.ReadOnly = Status
        CheckEdit94.Properties.ReadOnly = Status
        CheckEdit93.Properties.ReadOnly = Status
        TextEdit22.Properties.ReadOnly = Status
        CheckEdit96.Properties.ReadOnly = Status
        CheckEdit97.Properties.ReadOnly = Status
        CheckEdit99.Properties.ReadOnly = Status
        CheckEdit98.Properties.ReadOnly = Status
        CheckEdit103.Properties.ReadOnly = Status
        CheckEdit102.Properties.ReadOnly = Status
        CheckEdit101.Properties.ReadOnly = Status
        CheckEdit100.Properties.ReadOnly = Status
        CheckEdit109.Properties.ReadOnly = Status
        TextEdit23.Properties.ReadOnly = Status
        CheckEdit108.Properties.ReadOnly = Status
        CheckEdit107.Properties.ReadOnly = Status
        CheckEdit106.Properties.ReadOnly = Status
        CheckEdit105.Properties.ReadOnly = Status
        CheckEdit104.Properties.ReadOnly = Status
        TextEdit24.Properties.ReadOnly = Status
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
        TextEdit25.Properties.ReadOnly = Status
        TextEdit26.Properties.ReadOnly = Status
        TextEdit27.Properties.ReadOnly = Status
        TextEdit28.Properties.ReadOnly = Status
        TextEdit29.Properties.ReadOnly = Status
        CheckEdit126.Properties.ReadOnly = Status
        TextEdit30.Properties.ReadOnly = Status
        CheckEdit127.Properties.ReadOnly = Status
        TextEdit31.Properties.ReadOnly = Status
        CheckEdit128.Properties.ReadOnly = Status
        TextEdit32.Properties.ReadOnly = Status
        CheckEdit132.Properties.ReadOnly = Status
        CheckEdit130.Properties.ReadOnly = Status
        CheckEdit131.Properties.ReadOnly = Status
        TextEdit33.Properties.ReadOnly = Status
        CheckEdit129.Properties.ReadOnly = Status
        TextEdit34.Properties.ReadOnly = Status
        CheckEdit133.Properties.ReadOnly = Status
        TextEdit36.Properties.ReadOnly = Status
        CheckEdit134.Properties.ReadOnly = Status
        TextEdit35.Properties.ReadOnly = Status
        CheckEdit135.Properties.ReadOnly = Status
        CheckEdit136.Properties.ReadOnly = Status
        TextEdit37.Properties.ReadOnly = Status
        CheckEdit137.Properties.ReadOnly = Status
        TextEdit38.Properties.ReadOnly = Status
        CheckEdit138.Properties.ReadOnly = Status
        TextEdit39.Properties.ReadOnly = Status
        CheckEdit139.Properties.ReadOnly = Status
        TextEdit40.Properties.ReadOnly = Status
        CheckEdit140.Properties.ReadOnly = Status
        TextEdit63.Properties.ReadOnly = Status
        MemoEdit2.Properties.ReadOnly = Status
        MemoEdit4.Properties.ReadOnly = Status
        MemoEdit5.Properties.ReadOnly = Status
        MemoEdit6.Properties.ReadOnly = Status
        MemoEdit7.Properties.ReadOnly = Status
        MemoEdit3.Properties.ReadOnly = Status
        MemoEdit8.Properties.ReadOnly = Status
        MemoEdit9.Properties.ReadOnly = Status
        MemoEdit10.Properties.ReadOnly = Status
        MemoEdit11.Properties.ReadOnly = Status
        MemoEdit12.Properties.ReadOnly = Status
        MemoEdit13.Properties.ReadOnly = Status
        MemoEdit14.Properties.ReadOnly = Status
        MemoEdit15.Properties.ReadOnly = Status
        MemoEdit16.Properties.ReadOnly = Status
        'MemoEdit29.Properties.ReadOnly = Status
        'MemoEdit28.Properties.ReadOnly = Status
        'MemoEdit27.Properties.ReadOnly = Status
        'MemoEdit30.Properties.ReadOnly = Status
        'MemoEdit26.Properties.ReadOnly = Status
        'MemoEdit25.Properties.ReadOnly = Status
        'MemoEdit24.Properties.ReadOnly = Status
        'MemoEdit23.Properties.ReadOnly = Status
        'MemoEdit22.Properties.ReadOnly = Status
        'MemoEdit21.Properties.ReadOnly = Status
        'MemoEdit20.Properties.ReadOnly = Status
        'MemoEdit19.Properties.ReadOnly = Status
        'MemoEdit18.Properties.ReadOnly = Status
        'MemoEdit17.Properties.ReadOnly = Status
        MemoEdit41.Properties.ReadOnly = Status
        MemoEdit40.Properties.ReadOnly = Status
        MemoEdit39.Properties.ReadOnly = Status
        MemoEdit42.Properties.ReadOnly = Status
        MemoEdit31.Properties.ReadOnly = Status
        MemoEdit44.Properties.ReadOnly = Status
        MemoEdit43.Properties.ReadOnly = Status
        MemoEdit34.Properties.ReadOnly = Status
        MemoEdit33.Properties.ReadOnly = Status
        MemoEdit32.Properties.ReadOnly = Status
        MemoEdit35.Properties.ReadOnly = Status
        MemoEdit38.Properties.ReadOnly = Status
        MemoEdit37.Properties.ReadOnly = Status
        MemoEdit36.Properties.ReadOnly = Status
        MemoEdit49.Properties.ReadOnly = Status
        MemoEdit50.Properties.ReadOnly = Status
        MemoEdit51.Properties.ReadOnly = Status
        MemoEdit45.Properties.ReadOnly = Status
        MemoEdit48.Properties.ReadOnly = Status
        MemoEdit47.Properties.ReadOnly = Status
        MemoEdit46.Properties.ReadOnly = Status
        TextEdit41.Properties.ReadOnly = Status
        TextEdit42.Properties.ReadOnly = Status
        TextEdit43.Properties.ReadOnly = Status
        MemoEdit54.Properties.ReadOnly = Status
        MemoEdit53.Properties.ReadOnly = Status
        MemoEdit52.Properties.ReadOnly = Status
        MemoEdit55.Properties.ReadOnly = Status
        MemoEdit59.Properties.ReadOnly = Status
        MemoEdit58.Properties.ReadOnly = Status
        MemoEdit56.Properties.ReadOnly = Status
        TextEdit44.Properties.ReadOnly = Status
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
        TextEdit45.Properties.ReadOnly = Status
        MemoEdit57.Properties.ReadOnly = Status
        MemoEdit60.Properties.ReadOnly = Status
        TextEdit46.Properties.ReadOnly = Status
        TextEdit47.Properties.ReadOnly = Status
        MemoEdit61.Properties.ReadOnly = Status
        MemoEdit62.Properties.ReadOnly = Status
        MemoEdit63.Properties.ReadOnly = Status

        Dim oSetUser As New Setting.clsUser
        Dim dsUser = oSetUser.GetData(sUserID)
        If dsUser IsNot Nothing Then
            If dsUser.ISOTORTY = True Then
                lTanggal.Visible = True
                deDATE.Visible = True
                sIsOtority = True
            Else
                lTanggal.Visible = False
                deDATE.Visible = False
                sIsOtority = False
            End If
        End If
    End Sub
    Private Sub fn_EmptyMe()
        'txtCODE.Text = "<--- AUTO --->"
       
        TextEdit57.ResetText()
        TextEdit58.ResetText()
        TextEdit59.ResetText()
        TextEdit60.ResetText()
        TextEdit61.ResetText()
        TextEdit62.ResetText()
        txt9.ResetText()
        TextEdit4.ResetText()
        TextEdit5.ResetText()
        TextEdit1.ResetText()
        TextEdit2.ResetText()
        TextEdit3.ResetText()
        TextEdit6.ResetText()
        TextEdit7.ResetText()
        TextEdit8.ResetText()
        TextEdit9.ResetText()
        TextEdit10.ResetText()
        TextEdit11.ResetText()
        TextEdit13.ResetText()
        TextEdit12.ResetText()
        TextEdit14.ResetText()
        TextEdit15.ResetText()
        MemoEdit1.ResetText()
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
        TextEdit36.ResetText()
        TextEdit35.ResetText()
        TextEdit37.ResetText()
        TextEdit38.ResetText()
        TextEdit39.ResetText()
        TextEdit40.ResetText()
        TextEdit63.ResetText()
        MemoEdit2.ResetText()
        MemoEdit4.ResetText()
        MemoEdit5.ResetText()
        MemoEdit6.ResetText()
        MemoEdit7.ResetText()
        MemoEdit3.ResetText()
        MemoEdit8.ResetText()
        MemoEdit9.ResetText()
        MemoEdit10.ResetText()
        MemoEdit11.ResetText()
        MemoEdit12.ResetText()
        MemoEdit13.ResetText()
        MemoEdit14.ResetText()
        MemoEdit15.ResetText()
        MemoEdit16.ResetText()
        'MemoEdit29.ResetText()
        'MemoEdit28.ResetText()
        'MemoEdit27.ResetText()
        'MemoEdit30.ResetText()
        'MemoEdit26.ResetText()
        'MemoEdit25.ResetText()
        'MemoEdit24.ResetText()
        'MemoEdit23.ResetText()
        'MemoEdit22.ResetText()
        'MemoEdit21.ResetText()
        'MemoEdit20.ResetText()
        'MemoEdit19.ResetText()
        'MemoEdit18.ResetText()
        'MemoEdit17.ResetText()
        MemoEdit41.ResetText()
        MemoEdit40.ResetText()
        MemoEdit39.ResetText()
        MemoEdit42.ResetText()
        MemoEdit31.ResetText()
        MemoEdit44.ResetText()
        MemoEdit43.ResetText()
        MemoEdit34.ResetText()
        MemoEdit33.ResetText()
        MemoEdit32.ResetText()
        MemoEdit35.ResetText()
        MemoEdit38.ResetText()
        MemoEdit37.ResetText()
        MemoEdit36.ResetText()
        MemoEdit49.ResetText()
        MemoEdit50.ResetText()
        MemoEdit51.ResetText()
        MemoEdit45.ResetText()
        MemoEdit48.ResetText()
        MemoEdit47.ResetText()
        MemoEdit46.ResetText()
        TextEdit41.ResetText()
        TextEdit42.ResetText()
        TextEdit43.ResetText()
        MemoEdit54.ResetText()
        MemoEdit53.ResetText()
        MemoEdit52.ResetText()
        MemoEdit55.ResetText()
        MemoEdit59.ResetText()
        MemoEdit58.ResetText()
        MemoEdit56.ResetText()
        TextEdit44.ResetText()
        TextEdit45.ResetText()
        MemoEdit57.ResetText()
        MemoEdit60.ResetText()
        TextEdit46.ResetText()
        TextEdit47.ResetText()
        MemoEdit61.ResetText()
        MemoEdit62.ResetText()
        MemoEdit63.ResetText()

        deDATE.DateTime = Now
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RJ_38.GetData(txtNOREG.Text)

            With ds
                TextEdit48.Text = .SDIGITALRJ38_1
                TextEdit49.Text = .SDIGITALRJ38_2
                TextEdit50.Text = .SDIGITALRJ38_3
                TextEdit51.Text = .SDIGITALRJ38_4
                TextEdit52.Text = .SDIGITALRJ38_5
                TextEdit53.Text = .SDIGITALRJ38_6
                TextEdit54.Text = .SDIGITALRJ38_7
                TextEdit55.Text = .SDIGITALRJ38_8
                TextEdit56.Text = .SDIGITALRJ38_9
                TextEdit57.Text = .SDIGITALRJ38_10
                TextEdit58.Text = .SDIGITALRJ38_11
                TextEdit59.Text = .SDIGITALRJ38_12
                TextEdit60.Text = .SDIGITALRJ38_13
                TextEdit61.Text = .SDIGITALRJ38_14
                TextEdit62.Text = .SDIGITALRJ38_15
                CheckEdit1.Checked = .SDIGITALRJ38_16
                CheckEdit2.Checked = .SDIGITALRJ38_17
                CheckEdit3.Checked = .SDIGITALRJ38_18
                CheckEdit4.Checked = .SDIGITALRJ38_19
                txt9.Text = .SDIGITALRJ38_20
                CheckEdit5.Checked = .SDIGITALRJ38_21
                CheckEdit6.Checked = .SDIGITALRJ38_22
                CheckEdit7.Checked = .SDIGITALRJ38_23
                CheckEdit8.Checked = .SDIGITALRJ38_24
                CheckEdit9.Checked = .SDIGITALRJ38_25
                CheckEdit10.Checked = .SDIGITALRJ38_26
                CheckEdit11.Checked = .SDIGITALRJ38_27
                CheckEdit12.Checked = .SDIGITALRJ38_28
                CheckEdit14.Checked = .SDIGITALRJ38_29
                CheckEdit13.Checked = .SDIGITALRJ38_30
                TextEdit4.Text = .SDIGITALRJ38_31
                TextEdit5.Text = .SDIGITALRJ38_32
                CheckEdit16.Checked = .SDIGITALRJ38_33
                CheckEdit15.Checked = .SDIGITALRJ38_34
                CheckEdit17.Checked = .SDIGITALRJ38_35
                TextEdit1.Text = .SDIGITALRJ38_36
                TextEdit2.Text = .SDIGITALRJ38_37
                TextEdit3.Text = .SDIGITALRJ38_38
                CheckEdit18.Checked = .SDIGITALRJ38_39
                CheckEdit19.Checked = .SDIGITALRJ38_40
                TextEdit6.Text = .SDIGITALRJ38_41
                CheckEdit20.Checked = .SDIGITALRJ38_42
                CheckEdit21.Checked = .SDIGITALRJ38_43
                TextEdit7.Text = .SDIGITALRJ38_44
                CheckEdit22.Checked = .SDIGITALRJ38_45
                CheckEdit23.Checked = .SDIGITALRJ38_46
                CheckEdit24.Checked = .SDIGITALRJ38_47
                CheckEdit25.Checked = .SDIGITALRJ38_48
                CheckEdit26.Checked = .SDIGITALRJ38_49
                TextEdit8.Text = .SDIGITALRJ38_50
                CheckEdit27.Checked = .SDIGITALRJ38_51
                CheckEdit28.Checked = .SDIGITALRJ38_52
                CheckEdit29.Checked = .SDIGITALRJ38_53
                TextEdit9.Text = .SDIGITALRJ38_54
                CheckEdit30.Checked = .SDIGITALRJ38_55
                CheckEdit31.Checked = .SDIGITALRJ38_56
                CheckEdit32.Checked = .SDIGITALRJ38_57
                CheckEdit33.Checked = .SDIGITALRJ38_58
                CheckEdit34.Checked = .SDIGITALRJ38_59
                TextEdit10.Text = .SDIGITALRJ38_60
                TextEdit11.Text = .SDIGITALRJ38_61
                TextEdit13.Text = .SDIGITALRJ38_62
                TextEdit12.Text = .SDIGITALRJ38_63
                CheckEdit35.Checked = .SDIGITALRJ38_64
                CheckEdit36.Checked = .SDIGITALRJ38_65
                CheckEdit37.Checked = .SDIGITALRJ38_66
                CheckEdit38.Checked = .SDIGITALRJ38_67
                CheckEdit39.Checked = .SDIGITALRJ38_68
                TextEdit14.Text = .SDIGITALRJ38_69
                CheckEdit44.Checked = .SDIGITALRJ38_70
                CheckEdit45.Checked = .SDIGITALRJ38_71
                CheckEdit46.Checked = .SDIGITALRJ38_72
                CheckEdit47.Checked = .SDIGITALRJ38_73
                CheckEdit48.Checked = .SDIGITALRJ38_74
                CheckEdit49.Checked = .SDIGITALRJ38_75
                CheckEdit50.Checked = .SDIGITALRJ38_76
                CheckEdit52.Checked = .SDIGITALRJ38_77
                CheckEdit51.Checked = .SDIGITALRJ38_78
                CheckEdit53.Checked = .SDIGITALRJ38_79
                CheckEdit54.Checked = .SDIGITALRJ38_80
                CheckEdit55.Checked = .SDIGITALRJ38_81
                CheckEdit56.Checked = .SDIGITALRJ38_82
                CheckEdit57.Checked = .SDIGITALRJ38_83
                CheckEdit40.Checked = .SDIGITALRJ38_84
                CheckEdit41.Checked = .SDIGITALRJ38_85
                CheckEdit43.Checked = .SDIGITALRJ38_86
                CheckEdit58.Checked = .SDIGITALRJ38_87
                CheckEdit59.Checked = .SDIGITALRJ38_88
                CheckEdit61.Checked = .SDIGITALRJ38_89
                CheckEdit60.Checked = .SDIGITALRJ38_90
                CheckEdit64.Checked = .SDIGITALRJ38_91
                CheckEdit63.Checked = .SDIGITALRJ38_92
                CheckEdit62.Checked = .SDIGITALRJ38_93
                CheckEdit66.Checked = .SDIGITALRJ38_94
                CheckEdit65.Checked = .SDIGITALRJ38_95
                CheckEdit69.Checked = .SDIGITALRJ38_96
                CheckEdit68.Checked = .SDIGITALRJ38_97
                CheckEdit67.Checked = .SDIGITALRJ38_98
                CheckEdit71.Checked = .SDIGITALRJ38_99
                CheckEdit70.Checked = .SDIGITALRJ38_100
                TextEdit15.Text = .SDIGITALRJ38_101
                MemoEdit1.Text = .SDIGITALRJ38_102
                DateEdit1.EditValue = .SDIGITALRJ38_103
                CheckEdit72.Checked = .SDIGITALRJ38_104
                TextEdit16.Text = .SDIGITALRJ38_105
                CheckEdit73.Checked = .SDIGITALRJ38_106
                CheckEdit74.Checked = .SDIGITALRJ38_107
                CheckEdit75.Checked = .SDIGITALRJ38_108
                TextEdit17.Text = .SDIGITALRJ38_109
                CheckEdit77.Checked =  .SDIGITALRJ38_110
                CheckEdit76.Checked =  .SDIGITALRJ38_111
                CheckEdit78.Checked  =  .SDIGITALRJ38_112
                CheckEdit79.Checked  =  .SDIGITALRJ38_113
                TextEdit18.Text  =  .SDIGITALRJ38_114
                CheckEdit81.Checked  =  .SDIGITALRJ38_115
                CheckEdit80.Checked  =  .SDIGITALRJ38_116
                TextEdit19.Text  =  .SDIGITALRJ38_117
                CheckEdit83.Checked  =  .SDIGITALRJ38_118
                CheckEdit82.Checked  =  .SDIGITALRJ38_119
                CheckEdit84.Checked  =  .SDIGITALRJ38_120
                CheckEdit85.Checked  =  .SDIGITALRJ38_121
                CheckEdit86.Checked  =  .SDIGITALRJ38_122
                TextEdit20.Text  =  .SDIGITALRJ38_123
                CheckEdit87.Checked  =  .SDIGITALRJ38_124
                CheckEdit88.Checked  =  .SDIGITALRJ38_125
                CheckEdit89.Checked  =  .SDIGITALRJ38_126
                CheckEdit92.Checked  =  .SDIGITALRJ38_127
                CheckEdit91.Checked  =  .SDIGITALRJ38_128
                CheckEdit90.Checked  =  .SDIGITALRJ38_129
                CheckEdit95.Checked  =  .SDIGITALRJ38_130
                TextEdit21.Text  =  .SDIGITALRJ38_131
                CheckEdit94.Checked  =  .SDIGITALRJ38_132
                CheckEdit93.Checked  =  .SDIGITALRJ38_133
                TextEdit22.Text  =  .SDIGITALRJ38_134
                CheckEdit96.Checked  =  .SDIGITALRJ38_135
                CheckEdit97.Checked  =  .SDIGITALRJ38_136
                CheckEdit99.Checked  =  .SDIGITALRJ38_137
                CheckEdit98.Checked  =  .SDIGITALRJ38_138
                CheckEdit103.Checked  =  .SDIGITALRJ38_139
                CheckEdit102.Checked  =  .SDIGITALRJ38_140
                CheckEdit101.Checked  =  .SDIGITALRJ38_141
                CheckEdit100.Checked  =  .SDIGITALRJ38_142
                CheckEdit109.Checked  =  .SDIGITALRJ38_143
                TextEdit23.Text  =  .SDIGITALRJ38_144
                CheckEdit108.Checked  =  .SDIGITALRJ38_145
                CheckEdit107.Checked  =  .SDIGITALRJ38_146
                CheckEdit106.Checked  =  .SDIGITALRJ38_147
                CheckEdit105.Checked  =  .SDIGITALRJ38_148
                CheckEdit104.Checked  =  .SDIGITALRJ38_149
                TextEdit24.Text  =  .SDIGITALRJ38_150
                CheckEdit110.Checked  =  .SDIGITALRJ38_151
                CheckEdit111.Checked  =  .SDIGITALRJ38_152
                CheckEdit112.Checked  =  .SDIGITALRJ38_153
                CheckEdit113.Checked  =  .SDIGITALRJ38_154
                CheckEdit114.Checked  =  .SDIGITALRJ38_155
                CheckEdit115.Checked  =  .SDIGITALRJ38_156
                CheckEdit116.Checked  =  .SDIGITALRJ38_157
                CheckEdit117.Checked  =  .SDIGITALRJ38_158
                CheckEdit118.Checked  =  .SDIGITALRJ38_159
                CheckEdit119.Checked  =  .SDIGITALRJ38_160
                CheckEdit120.Checked  =  .SDIGITALRJ38_161
                CheckEdit121.Checked  =  .SDIGITALRJ38_162
                CheckEdit122.Checked  =  .SDIGITALRJ38_163
                CheckEdit123.Checked  =  .SDIGITALRJ38_164
                CheckEdit124.Checked  =  .SDIGITALRJ38_165
                CheckEdit125.Checked  =  .SDIGITALRJ38_166
                TextEdit25.Text  =  .SDIGITALRJ38_167
                TextEdit26.Text  =  .SDIGITALRJ38_168
                TextEdit27.Text  =  .SDIGITALRJ38_169
                TextEdit28.Text  =  .SDIGITALRJ38_170
                TextEdit29.Text  =  .SDIGITALRJ38_171
                CheckEdit126.Checked  =  .SDIGITALRJ38_172
                TextEdit30.Text  =  .SDIGITALRJ38_173
                CheckEdit127.Checked  =  .SDIGITALRJ38_174
                TextEdit31.Text  =  .SDIGITALRJ38_175
                CheckEdit128.Checked  =  .SDIGITALRJ38_176
                TextEdit32.Text  =  .SDIGITALRJ38_177
                CheckEdit132.Checked  =  .SDIGITALRJ38_178
                CheckEdit130.Checked  =  .SDIGITALRJ38_179
                CheckEdit131.Checked  =  .SDIGITALRJ38_180
                TextEdit33.Text  =  .SDIGITALRJ38_181
                CheckEdit129.Checked  =  .SDIGITALRJ38_182
                TextEdit34.Text  =  .SDIGITALRJ38_183
                CheckEdit133.Checked  =  .SDIGITALRJ38_184
                TextEdit36.Text  =  .SDIGITALRJ38_185
                CheckEdit134.Checked  =  .SDIGITALRJ38_186
                TextEdit35.Text  =  .SDIGITALRJ38_187
                CheckEdit135.Checked  =  .SDIGITALRJ38_188
                CheckEdit136.Checked  =  .SDIGITALRJ38_189
                TextEdit37.Text  =  .SDIGITALRJ38_190
                CheckEdit137.Checked  =  .SDIGITALRJ38_191
                TextEdit38.Text  =  .SDIGITALRJ38_192
                CheckEdit138.Checked  =  .SDIGITALRJ38_193
                TextEdit39.Text  =  .SDIGITALRJ38_194
                CheckEdit139.Checked  =  .SDIGITALRJ38_195
                TextEdit40.Text  =  .SDIGITALRJ38_196
                CheckEdit140.Checked  =  .SDIGITALRJ38_197
                TextEdit63.Text  =  .DOKTER_KODE
                MemoEdit2.Text  =  .SDIGITALRJ38_199
                MemoEdit4.Text  =  .SDIGITALRJ38_200
                MemoEdit5.Text  =  .SDIGITALRJ38_201
                MemoEdit6.Text  =  .SDIGITALRJ38_202
                MemoEdit7.Text  =  .SDIGITALRJ38_203
                MemoEdit3.Text  =  .SDIGITALRJ38_204
                MemoEdit8.Text  =  .SDIGITALRJ38_205
                MemoEdit9.Text  =  .SDIGITALRJ38_206
                MemoEdit10.Text  =  .SDIGITALRJ38_207
                MemoEdit11.Text  =  .SDIGITALRJ38_208
                MemoEdit12.Text  =  .SDIGITALRJ38_209
                MemoEdit13.Text  =  .SDIGITALRJ38_210
                MemoEdit14.Text  =  .SDIGITALRJ38_211
                MemoEdit15.Text  =  .SDIGITALRJ38_212
                MemoEdit16.Text  =  .SDIGITALRJ38_213
                'MemoEdit29.Text  =  .SDIGITALRJ38_214
                'MemoEdit28.Text  =  .SDIGITALRJ38_215
                'MemoEdit27.Text  =  .SDIGITALRJ38_216
                'MemoEdit30.Text  =  .SDIGITALRJ38_217
                'MemoEdit26.Text  =  .SDIGITALRJ38_218
                'MemoEdit25.Text  =  .SDIGITALRJ38_219
                'MemoEdit24.Text  =  .SDIGITALRJ38_220
                'MemoEdit23.Text  =  .SDIGITALRJ38_221
                'MemoEdit22.Text  =  .SDIGITALRJ38_222
                'MemoEdit21.Text  =  .SDIGITALRJ38_223
                'MemoEdit20.Text  =  .SDIGITALRJ38_224
                'MemoEdit19.Text  =  .SDIGITALRJ38_225
                'MemoEdit18.Text  =  .SDIGITALRJ38_226
                'MemoEdit17.Text  =  .SDIGITALRJ38_227
                MemoEdit41.Text  =  .SDIGITALRJ38_228
                MemoEdit40.Text  =  .SDIGITALRJ38_229
                MemoEdit39.Text  =  .SDIGITALRJ38_230
                MemoEdit42.Text  =  .SDIGITALRJ38_231
                MemoEdit31.Text  =  .SDIGITALRJ38_232
                MemoEdit44.Text  =  .SDIGITALRJ38_233
                MemoEdit43.Text  =  .SDIGITALRJ38_234
                MemoEdit34.Text  =  .SDIGITALRJ38_235
                MemoEdit33.Text  =  .SDIGITALRJ38_236
                MemoEdit32.Text  =  .SDIGITALRJ38_237
                MemoEdit35.Text  =  .SDIGITALRJ38_238
                MemoEdit38.Text  =  .SDIGITALRJ38_239
                MemoEdit37.Text  =  .SDIGITALRJ38_240
                MemoEdit36.Text  =  .SDIGITALRJ38_241
                MemoEdit49.Text  =  .SDIGITALRJ38_242
                MemoEdit50.Text  =  .SDIGITALRJ38_243
                MemoEdit51.Text  =  .SDIGITALRJ38_244
                MemoEdit45.Text  =  .SDIGITALRJ38_245
                MemoEdit48.Text  =  .SDIGITALRJ38_246
                MemoEdit47.Text  =  .SDIGITALRJ38_247
                MemoEdit46.Text  =  .SDIGITALRJ38_248
                TextEdit41.Text  =  .SDIGITALRJ38_249
                TextEdit42.Text  =  .SDIGITALRJ38_250
                TextEdit43.Text  =  .SDIGITALRJ38_251
                MemoEdit54.Text  =  .SDIGITALRJ38_252
                MemoEdit53.Text  =  .SDIGITALRJ38_253
                MemoEdit52.Text  =  .SDIGITALRJ38_254
                MemoEdit55.Text  =  .SDIGITALRJ38_255
                MemoEdit59.Text  =  .SDIGITALRJ38_256
                MemoEdit58.Text  =  .SDIGITALRJ38_257
                MemoEdit56.Text  =  .SDIGITALRJ38_258
                TextEdit44.Text  =  .SDIGITALRJ38_259
                CheckEdit141.Checked  =  .SDIGITALRJ38_260
                CheckEdit142.Checked  =  .SDIGITALRJ38_261
                CheckEdit143.Checked  =  .SDIGITALRJ38_262
                CheckEdit144.Checked  =  .SDIGITALRJ38_263
                CheckEdit145.Checked  =  .SDIGITALRJ38_264
                CheckEdit146.Checked  =  .SDIGITALRJ38_265
                CheckEdit147.Checked  =  .SDIGITALRJ38_266
                CheckEdit148.Checked  =  .SDIGITALRJ38_267
                CheckEdit149.Checked  =  .SDIGITALRJ38_268
                CheckEdit150.Checked  =  .SDIGITALRJ38_269
                CheckEdit151.Checked  =  .SDIGITALRJ38_270
                CheckEdit152.Checked  =  .SDIGITALRJ38_271
                CheckEdit153.Checked  =  .SDIGITALRJ38_272
                CheckEdit154.Checked  =  .SDIGITALRJ38_273
                CheckEdit155.Checked  =  .SDIGITALRJ38_274
                TextEdit45.Text  =  .SDIGITALRJ38_275
                MemoEdit57.Text  =  .SDIGITALRJ38_276
                MemoEdit60.Text  =  .SDIGITALRJ38_277
                TextEdit46.Text  =  .SDIGITALRJ38_278
                TextEdit47.Text  =  .SDIGITALRJ38_279
                MemoEdit61.Text  =  .SDIGITALRJ38_280
                MemoEdit62.Text  =  .SDIGITALRJ38_281
                MemoEdit63.Text  =  .SDIGITALRJ38_282

                deDATE.DateTime = .DATE

                BindingSource1.DataSource = oS_DIGITAL_RJ_38.GetDataDetail(txtNOREG.Text)
                grdDetail.DataSource = BindingSource1

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
            If txtNOREG.Text = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                txtNOREG.Focus()
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
            Dim ds = oS_DIGITAL_RJ_38.GetStructureHeader
            With ds
                .KDKUNJUNGAN = txtNOREG.Text
                Try
                    .DATECREATED = oS_DIGITAL_RJ_38.GetData(txtNOREG.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .SDIGITALRJ38_1 = TextEdit48.Text
                .SDIGITALRJ38_2 = TextEdit49.Text
                .SDIGITALRJ38_3 = TextEdit50.Text
                .SDIGITALRJ38_4 = TextEdit51.Text
                .SDIGITALRJ38_5 = TextEdit52.Text
                .SDIGITALRJ38_6 = TextEdit53.Text
                .SDIGITALRJ38_7 = TextEdit54.Text
                .SDIGITALRJ38_8 = TextEdit55.Text
                .SDIGITALRJ38_9 = TextEdit56.Text
                .SDIGITALRJ38_10 = TextEdit57.Text
                .SDIGITALRJ38_11 = TextEdit58.Text
                .SDIGITALRJ38_12 = TextEdit59.Text
                .SDIGITALRJ38_13 = TextEdit60.Text
                .SDIGITALRJ38_14 = TextEdit61.Text
                .SDIGITALRJ38_15 = TextEdit62.Text
                .SDIGITALRJ38_16 = CheckEdit1.Checked
                .SDIGITALRJ38_17 = CheckEdit2.Checked
                .SDIGITALRJ38_18 = CheckEdit3.Checked
                .SDIGITALRJ38_19 = CheckEdit4.Checked
                .SDIGITALRJ38_20 = txt9.Text
                .SDIGITALRJ38_21 = CheckEdit5.Checked
                .SDIGITALRJ38_22 = CheckEdit6.Checked
                .SDIGITALRJ38_23 = CheckEdit7.Checked
                .SDIGITALRJ38_24 = CheckEdit8.Checked
                .SDIGITALRJ38_25 = CheckEdit9.Checked
                .SDIGITALRJ38_26 = CheckEdit10.Checked
                .SDIGITALRJ38_27 = CheckEdit11.Checked
                .SDIGITALRJ38_28 = CheckEdit12.Checked
                .SDIGITALRJ38_29 = CheckEdit14.Checked
                .SDIGITALRJ38_30 = CheckEdit13.Checked
                .SDIGITALRJ38_31 = TextEdit4.Text
                .SDIGITALRJ38_32 = TextEdit5.Text
                .SDIGITALRJ38_33 = CheckEdit16.Checked
                .SDIGITALRJ38_34 = CheckEdit15.Checked
                .SDIGITALRJ38_35 = CheckEdit17.Checked
                .SDIGITALRJ38_36 = TextEdit1.Text
                .SDIGITALRJ38_37 = TextEdit2.Text
                .SDIGITALRJ38_38 = TextEdit3.Text
                .SDIGITALRJ38_39 = CheckEdit18.Checked
                .SDIGITALRJ38_40 = CheckEdit19.Checked
                .SDIGITALRJ38_41 = TextEdit6.Text
                .SDIGITALRJ38_42 = CheckEdit20.Checked
                .SDIGITALRJ38_43 = CheckEdit21.Checked
                .SDIGITALRJ38_44 = TextEdit7.Text
                .SDIGITALRJ38_45 = CheckEdit22.Checked
                .SDIGITALRJ38_46 = CheckEdit23.Checked
                .SDIGITALRJ38_47 = CheckEdit24.Checked
                .SDIGITALRJ38_48 = CheckEdit25.Checked
                .SDIGITALRJ38_49 = CheckEdit26.Checked
                .SDIGITALRJ38_50 = TextEdit8.Text
                .SDIGITALRJ38_51 = CheckEdit27.Checked
                .SDIGITALRJ38_52 = CheckEdit28.Checked
                .SDIGITALRJ38_53 = CheckEdit29.Checked
                .SDIGITALRJ38_54 = TextEdit9.Text
                .SDIGITALRJ38_55 = CheckEdit30.Checked
                .SDIGITALRJ38_56 = CheckEdit31.Checked
                .SDIGITALRJ38_57 = CheckEdit32.Checked
                .SDIGITALRJ38_58 = CheckEdit33.Checked
                .SDIGITALRJ38_59 = CheckEdit34.Checked
                .SDIGITALRJ38_60 = TextEdit10.Text
                .SDIGITALRJ38_61 = TextEdit11.Text
                .SDIGITALRJ38_62 = TextEdit13.Text
                .SDIGITALRJ38_63 = TextEdit12.Text
                .SDIGITALRJ38_64 = CheckEdit35.Checked
                .SDIGITALRJ38_65 = CheckEdit36.Checked
                .SDIGITALRJ38_66 = CheckEdit37.Checked
                .SDIGITALRJ38_67 = CheckEdit38.Checked
                .SDIGITALRJ38_68 = CheckEdit39.Checked
                .SDIGITALRJ38_69 = TextEdit14.Text
                .SDIGITALRJ38_70 = CheckEdit44.Checked
                .SDIGITALRJ38_71 = CheckEdit45.Checked
                .SDIGITALRJ38_72 = CheckEdit46.Checked
                .SDIGITALRJ38_73 = CheckEdit47.Checked
                .SDIGITALRJ38_74 = CheckEdit48.Checked
                .SDIGITALRJ38_75 = CheckEdit49.Checked
                .SDIGITALRJ38_76 = CheckEdit50.Checked
                .SDIGITALRJ38_77 = CheckEdit52.Checked
                .SDIGITALRJ38_78 = CheckEdit51.Checked
                .SDIGITALRJ38_79 = CheckEdit53.Checked
                .SDIGITALRJ38_80 = CheckEdit54.Checked
                .SDIGITALRJ38_81 = CheckEdit55.Checked
                .SDIGITALRJ38_82 = CheckEdit56.Checked
                .SDIGITALRJ38_83 = CheckEdit57.Checked
                .SDIGITALRJ38_84 = CheckEdit40.Checked
                .SDIGITALRJ38_85 = CheckEdit41.Checked
                .SDIGITALRJ38_86 = CheckEdit43.Checked
                .SDIGITALRJ38_87 = CheckEdit58.Checked
                .SDIGITALRJ38_88 = CheckEdit59.Checked
                .SDIGITALRJ38_89 = CheckEdit61.Checked
                .SDIGITALRJ38_90 = CheckEdit60.Checked
                .SDIGITALRJ38_91 = CheckEdit64.Checked
                .SDIGITALRJ38_92 = CheckEdit63.Checked
                .SDIGITALRJ38_93 = CheckEdit62.Checked
                .SDIGITALRJ38_94 = CheckEdit66.Checked
                .SDIGITALRJ38_95 = CheckEdit65.Checked
                .SDIGITALRJ38_96 = CheckEdit69.Checked
                .SDIGITALRJ38_97 = CheckEdit68.Checked
                .SDIGITALRJ38_98 = CheckEdit67.Checked
                .SDIGITALRJ38_99 = CheckEdit71.Checked
                .SDIGITALRJ38_100 = CheckEdit70.Checked
                .SDIGITALRJ38_101 = TextEdit15.Text
                .SDIGITALRJ38_102 = MemoEdit1.Text
                .SDIGITALRJ38_103 = DateEdit1.EditValue
                .SDIGITALRJ38_104 = CheckEdit72.Checked
                .SDIGITALRJ38_105 = TextEdit16.Text
                .SDIGITALRJ38_106 = CheckEdit73.Checked
                .SDIGITALRJ38_107 = CheckEdit74.Checked
                .SDIGITALRJ38_108 = CheckEdit75.Checked
                .SDIGITALRJ38_109 = TextEdit17.Text
                .SDIGITALRJ38_110 = CheckEdit77.Checked
                .SDIGITALRJ38_111 = CheckEdit76.Checked
                .SDIGITALRJ38_112 = CheckEdit78.Checked
                .SDIGITALRJ38_113 = CheckEdit79.Checked
                .SDIGITALRJ38_114 = TextEdit18.Text
                .SDIGITALRJ38_115 = CheckEdit81.Checked
                .SDIGITALRJ38_116 = CheckEdit80.Checked
                .SDIGITALRJ38_117 = TextEdit19.Text
                .SDIGITALRJ38_118 = CheckEdit83.Checked
                .SDIGITALRJ38_119 = CheckEdit82.Checked
                .SDIGITALRJ38_120 = CheckEdit84.Checked
                .SDIGITALRJ38_121 = CheckEdit85.Checked
                .SDIGITALRJ38_122 = CheckEdit86.Checked
                .SDIGITALRJ38_123 = TextEdit20.Text
                .SDIGITALRJ38_124 = CheckEdit87.Checked
                .SDIGITALRJ38_125 = CheckEdit88.Checked
                .SDIGITALRJ38_126 = CheckEdit89.Checked
                .SDIGITALRJ38_127 = CheckEdit92.Checked
                .SDIGITALRJ38_128 = CheckEdit91.Checked
                .SDIGITALRJ38_129 = CheckEdit90.Checked
                .SDIGITALRJ38_130 = CheckEdit95.Checked
                .SDIGITALRJ38_131 = TextEdit21.Text
                .SDIGITALRJ38_132 = CheckEdit94.Checked
                .SDIGITALRJ38_133 = CheckEdit93.Checked
                .SDIGITALRJ38_134 = TextEdit22.Text
                .SDIGITALRJ38_135 = CheckEdit96.Checked
                .SDIGITALRJ38_136 = CheckEdit97.Checked
                .SDIGITALRJ38_137 = CheckEdit99.Checked
                .SDIGITALRJ38_138 = CheckEdit98.Checked
                .SDIGITALRJ38_139 = CheckEdit103.Checked
                .SDIGITALRJ38_140 = CheckEdit102.Checked
                .SDIGITALRJ38_141 = CheckEdit101.Checked
                .SDIGITALRJ38_142 = CheckEdit100.Checked
                .SDIGITALRJ38_143 = CheckEdit109.Checked
                .SDIGITALRJ38_144 = TextEdit23.Text
                .SDIGITALRJ38_145 = CheckEdit108.Checked
                .SDIGITALRJ38_146 = CheckEdit107.Checked
                .SDIGITALRJ38_147 = CheckEdit106.Checked
                .SDIGITALRJ38_148 = CheckEdit105.Checked
                .SDIGITALRJ38_149 = CheckEdit104.Checked
                .SDIGITALRJ38_150 = TextEdit24.Text
                .SDIGITALRJ38_151 = CheckEdit110.Checked
                .SDIGITALRJ38_152 = CheckEdit111.Checked
                .SDIGITALRJ38_153 = CheckEdit112.Checked
                .SDIGITALRJ38_154 = CheckEdit113.Checked
                .SDIGITALRJ38_155 = CheckEdit114.Checked
                .SDIGITALRJ38_156 = CheckEdit115.Checked
                .SDIGITALRJ38_157 = CheckEdit116.Checked
                .SDIGITALRJ38_158 = CheckEdit117.Checked
                .SDIGITALRJ38_159 = CheckEdit118.Checked
                .SDIGITALRJ38_160 = CheckEdit119.Checked
                .SDIGITALRJ38_161 = CheckEdit120.Checked
                .SDIGITALRJ38_162 = CheckEdit121.Checked
                .SDIGITALRJ38_163 = CheckEdit122.Checked
                .SDIGITALRJ38_164 = CheckEdit123.Checked
                .SDIGITALRJ38_165 = CheckEdit124.Checked
                .SDIGITALRJ38_166 = CheckEdit125.Checked
                .SDIGITALRJ38_167 = TextEdit25.Text
                .SDIGITALRJ38_168 = TextEdit26.Text
                .SDIGITALRJ38_169 = TextEdit27.Text
                .SDIGITALRJ38_170 = TextEdit28.Text
                .SDIGITALRJ38_171 = TextEdit29.Text
                .SDIGITALRJ38_172 = CheckEdit126.Checked
                .SDIGITALRJ38_173 = TextEdit30.Text
                .SDIGITALRJ38_174 = CheckEdit127.Checked
                .SDIGITALRJ38_175 = TextEdit31.Text
                .SDIGITALRJ38_176 = CheckEdit128.Checked
                .SDIGITALRJ38_177 = TextEdit32.Text
                .SDIGITALRJ38_178 = CheckEdit132.Checked
                .SDIGITALRJ38_179 = CheckEdit130.Checked
                .SDIGITALRJ38_180 = CheckEdit131.Checked
                .SDIGITALRJ38_181 = TextEdit33.Text
                .SDIGITALRJ38_182 = CheckEdit129.Checked
                .SDIGITALRJ38_183 = TextEdit34.Text
                .SDIGITALRJ38_184 = CheckEdit133.Checked
                .SDIGITALRJ38_185 = TextEdit36.Text
                .SDIGITALRJ38_186 = CheckEdit134.Checked
                .SDIGITALRJ38_187 = TextEdit35.Text
                .SDIGITALRJ38_188 = CheckEdit135.Checked
                .SDIGITALRJ38_189 = CheckEdit136.Checked
                .SDIGITALRJ38_190 = TextEdit37.Text
                .SDIGITALRJ38_191 = CheckEdit137.Checked
                .SDIGITALRJ38_192 = TextEdit38.Text
                .SDIGITALRJ38_193 = CheckEdit138.Checked
                .SDIGITALRJ38_194 = TextEdit39.Text
                .SDIGITALRJ38_195 = CheckEdit139.Checked
                .SDIGITALRJ38_196 = TextEdit40.Text
                .SDIGITALRJ38_197 = CheckEdit140.Checked
                .SDIGITALRJ38_198 = TextEdit63.Text
                .SDIGITALRJ38_199 = MemoEdit2.Text
                .SDIGITALRJ38_200 = MemoEdit4.Text
                .SDIGITALRJ38_201 = MemoEdit5.Text
                .SDIGITALRJ38_202 = MemoEdit6.Text
                .SDIGITALRJ38_203 = MemoEdit7.Text
                .SDIGITALRJ38_204 = MemoEdit3.Text
                .SDIGITALRJ38_205 = MemoEdit8.Text
                .SDIGITALRJ38_206 = MemoEdit9.Text
                .SDIGITALRJ38_207 = MemoEdit10.Text
                .SDIGITALRJ38_208 = MemoEdit11.Text
                .SDIGITALRJ38_209 = MemoEdit12.Text
                .SDIGITALRJ38_210 = MemoEdit13.Text
                .SDIGITALRJ38_211 = MemoEdit14.Text
                .SDIGITALRJ38_212 = MemoEdit15.Text
                .SDIGITALRJ38_213 = MemoEdit16.Text
                '.SDIGITALRJ38_214 = MemoEdit29.Text
                '.SDIGITALRJ38_215 = MemoEdit28.Text
                '.SDIGITALRJ38_216 = MemoEdit27.Text
                '.SDIGITALRJ38_217 = MemoEdit30.Text
                '.SDIGITALRJ38_218 = MemoEdit26.Text
                '.SDIGITALRJ38_219 = MemoEdit25.Text
                '.SDIGITALRJ38_220 = MemoEdit24.Text
                '.SDIGITALRJ38_221 = MemoEdit23.Text
                '.SDIGITALRJ38_222 = MemoEdit22.Text
                '.SDIGITALRJ38_223 = MemoEdit21.Text
                '.SDIGITALRJ38_224 = MemoEdit20.Text
                '.SDIGITALRJ38_225 = MemoEdit19.Text
                '.SDIGITALRJ38_226 = MemoEdit18.Text
                '.SDIGITALRJ38_227 = MemoEdit17.Text

                .SDIGITALRJ38_214 = ""
                .SDIGITALRJ38_215 = ""
                .SDIGITALRJ38_216 = ""
                .SDIGITALRJ38_217 = ""
                .SDIGITALRJ38_218 = ""
                .SDIGITALRJ38_219 = ""
                .SDIGITALRJ38_220 = ""
                .SDIGITALRJ38_221 = ""
                .SDIGITALRJ38_222 = ""
                .SDIGITALRJ38_223 = ""
                .SDIGITALRJ38_224 = ""
                .SDIGITALRJ38_225 = ""
                .SDIGITALRJ38_226 = ""
                .SDIGITALRJ38_227 = ""

                .SDIGITALRJ38_228 = MemoEdit41.Text
                .SDIGITALRJ38_229 = MemoEdit40.Text
                .SDIGITALRJ38_230 = MemoEdit39.Text
                .SDIGITALRJ38_231 = MemoEdit42.Text
                .SDIGITALRJ38_232 = MemoEdit31.Text
                .SDIGITALRJ38_233 = MemoEdit44.Text
                .SDIGITALRJ38_234 = MemoEdit43.Text
                .SDIGITALRJ38_235 = MemoEdit34.Text
                .SDIGITALRJ38_236 = MemoEdit33.Text
                .SDIGITALRJ38_237 = MemoEdit32.Text
                .SDIGITALRJ38_238 = MemoEdit35.Text
                .SDIGITALRJ38_239 = MemoEdit38.Text
                .SDIGITALRJ38_240 = MemoEdit37.Text
                .SDIGITALRJ38_241 = MemoEdit36.Text
                .SDIGITALRJ38_242 = MemoEdit49.Text
                .SDIGITALRJ38_243 = MemoEdit50.Text
                .SDIGITALRJ38_244 = MemoEdit51.Text
                .SDIGITALRJ38_245 = MemoEdit45.Text
                .SDIGITALRJ38_246 = MemoEdit48.Text
                .SDIGITALRJ38_247 = MemoEdit47.Text
                .SDIGITALRJ38_248 = MemoEdit46.Text
                .SDIGITALRJ38_249 = TextEdit41.Text
                .SDIGITALRJ38_250 = TextEdit42.Text
                .SDIGITALRJ38_251 = TextEdit43.Text
                .SDIGITALRJ38_252 = MemoEdit54.Text
                .SDIGITALRJ38_253 = MemoEdit53.Text
                .SDIGITALRJ38_254 = MemoEdit52.Text
                .SDIGITALRJ38_255 = MemoEdit55.Text
                .SDIGITALRJ38_256 = MemoEdit59.Text
                .SDIGITALRJ38_257 = MemoEdit58.Text
                .SDIGITALRJ38_258 = MemoEdit56.Text
                .SDIGITALRJ38_259 = TextEdit44.Text
                .SDIGITALRJ38_260 = CheckEdit141.Checked
                .SDIGITALRJ38_261 = CheckEdit142.Checked
                .SDIGITALRJ38_262 = CheckEdit143.Checked
                .SDIGITALRJ38_263 = CheckEdit144.Checked
                .SDIGITALRJ38_264 = CheckEdit145.Checked
                .SDIGITALRJ38_265 = CheckEdit146.Checked
                .SDIGITALRJ38_266 = CheckEdit147.Checked
                .SDIGITALRJ38_267 = CheckEdit148.Checked
                .SDIGITALRJ38_268 = CheckEdit149.Checked
                .SDIGITALRJ38_269 = CheckEdit150.Checked
                .SDIGITALRJ38_270 = CheckEdit151.Checked
                .SDIGITALRJ38_271 = CheckEdit152.Checked
                .SDIGITALRJ38_272 = CheckEdit153.Checked
                .SDIGITALRJ38_273 = CheckEdit154.Checked
                .SDIGITALRJ38_274 = CheckEdit155.Checked
                .SDIGITALRJ38_275 = TextEdit45.Text
                .SDIGITALRJ38_276 = MemoEdit57.Text
                .SDIGITALRJ38_277 = MemoEdit60.Text
                .SDIGITALRJ38_278 = TextEdit46.Text
                .SDIGITALRJ38_279 = TextEdit47.Text
                .SDIGITALRJ38_280 = MemoEdit61.Text
                .SDIGITALRJ38_281 = MemoEdit62.Text
                .SDIGITALRJ38_282 = MemoEdit63.Text

                .DOKTER_KODE = TextEdit63.EditValue
                .DOKTER_NAMEDISPLAY = ""

                Try
                    .CETAK = oS_DIGITAL_RJ_38.GetData(txtNOREG.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                Try
                    If sIsOtority = True Then
                        .KDUSER = oS_DIGITAL_RJ_38.GetData(txtNOREG.Text).KDUSER
                        .KDUSER_SIGNATURE = oS_DIGITAL_RJ_38.GetData(txtNOREG.Text).KDUSER_SIGNATURE
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
            Dim arrDetail = oS_DIGITAL_RJ_38.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oS_DIGITAL_RJ_38.GetStructureDetail
                With dsDetail
                    .SEQ = i
                    .KDKUNJUNGAN = ds.KDKUNJUNGAN
                    .DATEUPDATED = Now
                    .DATECREATED = Now
                    .OBSERVASI = grvDetail.GetRowCellValue(i,colOBSERVASI)
                    .JAM = CDate(grvDetail.GetRowCellValue(i, colJAM))
                    .QB = grvDetail.GetRowCellValue(i,colQB)
                    .UF = grvDetail.GetRowCellValue(i,colUF)
                    .TD = grvDetail.GetRowCellValue(i,colTD)
                    .NADI = grvDetail.GetRowCellValue(i,colNADI)
                    .SUHU = grvDetail.GetRowCellValue(i,colSUHU)
                    .RESEP = grvDetail.GetRowCellValue(i,colRESP)
                    .NACL = grvDetail.GetRowCellValue(i,colNACL)
                    .DEXTROSE = grvDetail.GetRowCellValue(i,colDEXTROSE)
                    .MAKANMINUM = grvDetail.GetRowCellValue(i,colMAKANMINUM)
                    .LAINLAIN = grvDetail.GetRowCellValue(i,colLAIN_LAIN)
                    .UFTERCAPAI = grvDetail.GetRowCellValue(i,colUFTERCAPAI)
                    .KETERANGANLAIN = grvDetail.GetRowCellValue(i,colKETLAIN)
                    .PARAF = grvDetail.GetRowCellValue(i,colPARAF)
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_RJ_38.InsertData(ds,arrDetail)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_RJ_38.UpdateData(ds,arrDetail)
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
        'Dim oUser As New Setting.clsUser
        'Try
        '    grdNOIDUSER.Properties.DataSource = oUser.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
        '    grdNOIDUSER.Properties.ValueMember = "NOIDUSER"
        '    grdNOIDUSER.Properties.DisplayMember = "NOIDUSER"
        'Catch oErr As Exception
        '    MsgBox("Load Sub Spesialis Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub
    Private Sub fn_DOCTOR()
        'Dim oDOCTOR As New Master.clsDoctor
        'Try
        '    'grdDOCTOR.Properties.DataSource = oDOCTOR.GetData.Where(Function(x) x.ISACTIVE = True And x.ADDRESS_COUNTRY <> "").ToList()
        '    'grdDOCTOR.Properties.ValueMember = "KDDOCTOR"
        '    'grdDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"
        'Catch oErr As Exception
        '    MsgBox("Load Dokter DPJP Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub
    Private Sub fn_DEPARTMENT()
        'Dim oDEPARTMENT As New Master.clsDepartment
        'Try
        '    'grdDEPARTMENT.Properties.DataSource = oDEPARTMENT.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
        '    'grdDEPARTMENT.Properties.ValueMember = "KDDEPARTMENT"
        '    'grdDEPARTMENT.Properties.DisplayMember = "NAME_DISPLAY"
        'Catch oErr As Exception
        '    MsgBox("Load Department Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub

    Private Sub btnEDITIMAGE_Click(sender As Object, e As EventArgs)
        Dim frmPopUp_Image As New frmPopUp_img1
        frmPopUp_Image.fn_LoadMe(picGAMBAR2.Image)
        frmPopUp_Image.ShowDialog()

        If sPicture IsNot Nothing Then
            picGAMBAR2.Image = sPicture
        End If

        picGAMBAR2.Focus()
        sFind10 = String.Empty
        sPicture = Nothing
    End Sub

    Private Sub grvDetail_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles grvDetail.InitNewRow
        grvDetail.SetFocusedRowCellValue(colJAM, now)
    End Sub
    Private Sub fn_LoadKDDOCTOR()
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

            TextEdit63.Properties.DataSource = ds.Tables("DOKTER")
            TextEdit63.Properties.ValueMember = "KDDOCTOR"
            TextEdit63.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub frmEMedrekRJ_38_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
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
            SQL &= ",EVALUASI = A.SDIGITALRJ38_276 "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RJ_38 A "
            SQL &= "INNER JOIN R_IDENTITAS_PASIEN B "
            SQL &= "ON A.KDKUNJUNGAN = B.KDKUNJUNGAN "
            SQL &= "WHERE KDCUSTOMER = '" & txtNOMORRM.Text & "' "
            SQL &= "ORDER BY A.DATE DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "R_ASKEPHD")

            grd_RiwayatAskep.MainView = grv_RiwayatAskep
            grd_RiwayatAskep.DataSource = ds.Tables("R_ASKEPHD")
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
            Dim ds = oS_DIGITAL_RJ_38.GetData(grv_RiwayatAskep.GetFocusedRowCellValue("KDKUNJUNGAN"))

            With ds
                TextEdit48.Text = .SDIGITALRJ38_1
                TextEdit49.Text = .SDIGITALRJ38_2
                TextEdit50.Text = .SDIGITALRJ38_3
                TextEdit51.Text = .SDIGITALRJ38_4
                TextEdit52.Text = .SDIGITALRJ38_5
                TextEdit53.Text = .SDIGITALRJ38_6
                TextEdit54.Text = .SDIGITALRJ38_7
                TextEdit55.Text = .SDIGITALRJ38_8
                TextEdit56.Text = .SDIGITALRJ38_9
                TextEdit57.Text = .SDIGITALRJ38_10
                TextEdit58.Text = .SDIGITALRJ38_11
                TextEdit59.Text = .SDIGITALRJ38_12
                TextEdit60.Text = .SDIGITALRJ38_13
                TextEdit61.Text = .SDIGITALRJ38_14
                TextEdit62.Text = .SDIGITALRJ38_15
                CheckEdit1.Checked = .SDIGITALRJ38_16
                CheckEdit2.Checked = .SDIGITALRJ38_17
                CheckEdit3.Checked = .SDIGITALRJ38_18
                CheckEdit4.Checked = .SDIGITALRJ38_19
                txt9.Text = .SDIGITALRJ38_20
                CheckEdit5.Checked = .SDIGITALRJ38_21
                CheckEdit6.Checked = .SDIGITALRJ38_22
                CheckEdit7.Checked = .SDIGITALRJ38_23
                CheckEdit8.Checked = .SDIGITALRJ38_24
                CheckEdit9.Checked = .SDIGITALRJ38_25
                CheckEdit10.Checked = .SDIGITALRJ38_26
                CheckEdit11.Checked = .SDIGITALRJ38_27
                CheckEdit12.Checked = .SDIGITALRJ38_28
                CheckEdit14.Checked = .SDIGITALRJ38_29
                CheckEdit13.Checked = .SDIGITALRJ38_30
                TextEdit4.Text = .SDIGITALRJ38_31
                TextEdit5.Text = .SDIGITALRJ38_32
                CheckEdit16.Checked = .SDIGITALRJ38_33
                CheckEdit15.Checked = .SDIGITALRJ38_34
                CheckEdit17.Checked = .SDIGITALRJ38_35
                TextEdit1.Text = .SDIGITALRJ38_36
                TextEdit2.Text = .SDIGITALRJ38_37
                TextEdit3.Text = .SDIGITALRJ38_38
                CheckEdit18.Checked = .SDIGITALRJ38_39
                CheckEdit19.Checked = .SDIGITALRJ38_40
                TextEdit6.Text = .SDIGITALRJ38_41
                CheckEdit20.Checked = .SDIGITALRJ38_42
                CheckEdit21.Checked = .SDIGITALRJ38_43
                TextEdit7.Text = .SDIGITALRJ38_44
                CheckEdit22.Checked = .SDIGITALRJ38_45
                CheckEdit23.Checked = .SDIGITALRJ38_46
                CheckEdit24.Checked = .SDIGITALRJ38_47
                CheckEdit25.Checked = .SDIGITALRJ38_48
                CheckEdit26.Checked = .SDIGITALRJ38_49
                TextEdit8.Text = .SDIGITALRJ38_50
                CheckEdit27.Checked = .SDIGITALRJ38_51
                CheckEdit28.Checked = .SDIGITALRJ38_52
                CheckEdit29.Checked = .SDIGITALRJ38_53
                TextEdit9.Text = .SDIGITALRJ38_54
                CheckEdit30.Checked = .SDIGITALRJ38_55
                CheckEdit31.Checked = .SDIGITALRJ38_56
                CheckEdit32.Checked = .SDIGITALRJ38_57
                CheckEdit33.Checked = .SDIGITALRJ38_58
                CheckEdit34.Checked = .SDIGITALRJ38_59
                TextEdit10.Text = .SDIGITALRJ38_60
                TextEdit11.Text = .SDIGITALRJ38_61
                TextEdit13.Text = .SDIGITALRJ38_62
                TextEdit12.Text = .SDIGITALRJ38_63
                CheckEdit35.Checked = .SDIGITALRJ38_64
                CheckEdit36.Checked = .SDIGITALRJ38_65
                CheckEdit37.Checked = .SDIGITALRJ38_66
                CheckEdit38.Checked = .SDIGITALRJ38_67
                CheckEdit39.Checked = .SDIGITALRJ38_68
                TextEdit14.Text = .SDIGITALRJ38_69
                CheckEdit44.Checked = .SDIGITALRJ38_70
                CheckEdit45.Checked = .SDIGITALRJ38_71
                CheckEdit46.Checked = .SDIGITALRJ38_72
                CheckEdit47.Checked = .SDIGITALRJ38_73
                CheckEdit48.Checked = .SDIGITALRJ38_74
                CheckEdit49.Checked = .SDIGITALRJ38_75
                CheckEdit50.Checked = .SDIGITALRJ38_76
                CheckEdit52.Checked = .SDIGITALRJ38_77
                CheckEdit51.Checked = .SDIGITALRJ38_78
                CheckEdit53.Checked = .SDIGITALRJ38_79
                CheckEdit54.Checked = .SDIGITALRJ38_80
                CheckEdit55.Checked = .SDIGITALRJ38_81
                CheckEdit56.Checked = .SDIGITALRJ38_82
                CheckEdit57.Checked = .SDIGITALRJ38_83
                CheckEdit40.Checked = .SDIGITALRJ38_84
                CheckEdit41.Checked = .SDIGITALRJ38_85
                CheckEdit43.Checked = .SDIGITALRJ38_86
                CheckEdit58.Checked = .SDIGITALRJ38_87
                CheckEdit59.Checked = .SDIGITALRJ38_88
                CheckEdit61.Checked = .SDIGITALRJ38_89
                CheckEdit60.Checked = .SDIGITALRJ38_90
                CheckEdit64.Checked = .SDIGITALRJ38_91
                CheckEdit63.Checked = .SDIGITALRJ38_92
                CheckEdit62.Checked = .SDIGITALRJ38_93
                CheckEdit66.Checked = .SDIGITALRJ38_94
                CheckEdit65.Checked = .SDIGITALRJ38_95
                CheckEdit69.Checked = .SDIGITALRJ38_96
                CheckEdit68.Checked = .SDIGITALRJ38_97
                CheckEdit67.Checked = .SDIGITALRJ38_98
                CheckEdit71.Checked = .SDIGITALRJ38_99
                CheckEdit70.Checked = .SDIGITALRJ38_100
                TextEdit15.Text = .SDIGITALRJ38_101
                MemoEdit1.Text = .SDIGITALRJ38_102
                DateEdit1.EditValue = .SDIGITALRJ38_103
                CheckEdit72.Checked = .SDIGITALRJ38_104
                TextEdit16.Text = .SDIGITALRJ38_105
                CheckEdit73.Checked = .SDIGITALRJ38_106
                CheckEdit74.Checked = .SDIGITALRJ38_107
                CheckEdit75.Checked = .SDIGITALRJ38_108
                TextEdit17.Text = .SDIGITALRJ38_109
                CheckEdit77.Checked =  .SDIGITALRJ38_110
                CheckEdit76.Checked =  .SDIGITALRJ38_111
                CheckEdit78.Checked  =  .SDIGITALRJ38_112
                CheckEdit79.Checked  =  .SDIGITALRJ38_113
                TextEdit18.Text  =  .SDIGITALRJ38_114
                CheckEdit81.Checked  =  .SDIGITALRJ38_115
                CheckEdit80.Checked  =  .SDIGITALRJ38_116
                TextEdit19.Text  =  .SDIGITALRJ38_117
                CheckEdit83.Checked  =  .SDIGITALRJ38_118
                CheckEdit82.Checked  =  .SDIGITALRJ38_119
                CheckEdit84.Checked  =  .SDIGITALRJ38_120
                CheckEdit85.Checked  =  .SDIGITALRJ38_121
                CheckEdit86.Checked  =  .SDIGITALRJ38_122
                TextEdit20.Text  =  .SDIGITALRJ38_123
                CheckEdit87.Checked  =  .SDIGITALRJ38_124
                CheckEdit88.Checked  =  .SDIGITALRJ38_125
                CheckEdit89.Checked  =  .SDIGITALRJ38_126
                CheckEdit92.Checked  =  .SDIGITALRJ38_127
                CheckEdit91.Checked  =  .SDIGITALRJ38_128
                CheckEdit90.Checked  =  .SDIGITALRJ38_129
                CheckEdit95.Checked  =  .SDIGITALRJ38_130
                TextEdit21.Text  =  .SDIGITALRJ38_131
                CheckEdit94.Checked  =  .SDIGITALRJ38_132
                CheckEdit93.Checked  =  .SDIGITALRJ38_133
                TextEdit22.Text  =  .SDIGITALRJ38_134
                CheckEdit96.Checked  =  .SDIGITALRJ38_135
                CheckEdit97.Checked  =  .SDIGITALRJ38_136
                CheckEdit99.Checked  =  .SDIGITALRJ38_137
                CheckEdit98.Checked  =  .SDIGITALRJ38_138
                CheckEdit103.Checked  =  .SDIGITALRJ38_139
                CheckEdit102.Checked  =  .SDIGITALRJ38_140
                CheckEdit101.Checked  =  .SDIGITALRJ38_141
                CheckEdit100.Checked  =  .SDIGITALRJ38_142
                CheckEdit109.Checked  =  .SDIGITALRJ38_143
                TextEdit23.Text  =  .SDIGITALRJ38_144
                CheckEdit108.Checked  =  .SDIGITALRJ38_145
                CheckEdit107.Checked  =  .SDIGITALRJ38_146
                CheckEdit106.Checked  =  .SDIGITALRJ38_147
                CheckEdit105.Checked  =  .SDIGITALRJ38_148
                CheckEdit104.Checked  =  .SDIGITALRJ38_149
                TextEdit24.Text  =  .SDIGITALRJ38_150
                CheckEdit110.Checked  =  .SDIGITALRJ38_151
                CheckEdit111.Checked  =  .SDIGITALRJ38_152
                CheckEdit112.Checked  =  .SDIGITALRJ38_153
                CheckEdit113.Checked  =  .SDIGITALRJ38_154
                CheckEdit114.Checked  =  .SDIGITALRJ38_155
                CheckEdit115.Checked  =  .SDIGITALRJ38_156
                CheckEdit116.Checked  =  .SDIGITALRJ38_157
                CheckEdit117.Checked  =  .SDIGITALRJ38_158
                CheckEdit118.Checked  =  .SDIGITALRJ38_159
                CheckEdit119.Checked  =  .SDIGITALRJ38_160
                CheckEdit120.Checked  =  .SDIGITALRJ38_161
                CheckEdit121.Checked  =  .SDIGITALRJ38_162
                CheckEdit122.Checked  =  .SDIGITALRJ38_163
                CheckEdit123.Checked  =  .SDIGITALRJ38_164
                CheckEdit124.Checked  =  .SDIGITALRJ38_165
                CheckEdit125.Checked  =  .SDIGITALRJ38_166
                TextEdit25.Text  =  .SDIGITALRJ38_167
                TextEdit26.Text  =  .SDIGITALRJ38_168
                TextEdit27.Text  =  .SDIGITALRJ38_169
                TextEdit28.Text  =  .SDIGITALRJ38_170
                TextEdit29.Text  =  .SDIGITALRJ38_171
                CheckEdit126.Checked  =  .SDIGITALRJ38_172
                TextEdit30.Text  =  .SDIGITALRJ38_173
                CheckEdit127.Checked  =  .SDIGITALRJ38_174
                TextEdit31.Text  =  .SDIGITALRJ38_175
                CheckEdit128.Checked  =  .SDIGITALRJ38_176
                TextEdit32.Text  =  .SDIGITALRJ38_177
                CheckEdit132.Checked  =  .SDIGITALRJ38_178
                CheckEdit130.Checked  =  .SDIGITALRJ38_179
                CheckEdit131.Checked  =  .SDIGITALRJ38_180
                TextEdit33.Text  =  .SDIGITALRJ38_181
                CheckEdit129.Checked  =  .SDIGITALRJ38_182
                TextEdit34.Text  =  .SDIGITALRJ38_183
                CheckEdit133.Checked  =  .SDIGITALRJ38_184
                TextEdit36.Text  =  .SDIGITALRJ38_185
                CheckEdit134.Checked  =  .SDIGITALRJ38_186
                TextEdit35.Text  =  .SDIGITALRJ38_187
                CheckEdit135.Checked  =  .SDIGITALRJ38_188
                CheckEdit136.Checked  =  .SDIGITALRJ38_189
                TextEdit37.Text  =  .SDIGITALRJ38_190
                CheckEdit137.Checked  =  .SDIGITALRJ38_191
                TextEdit38.Text  =  .SDIGITALRJ38_192
                CheckEdit138.Checked  =  .SDIGITALRJ38_193
                TextEdit39.Text  =  .SDIGITALRJ38_194
                CheckEdit139.Checked  =  .SDIGITALRJ38_195
                TextEdit40.Text  =  .SDIGITALRJ38_196
                CheckEdit140.Checked  =  .SDIGITALRJ38_197
                TextEdit63.Text  =  .DOKTER_KODE
                MemoEdit2.Text  =  .SDIGITALRJ38_199
                MemoEdit4.Text  =  .SDIGITALRJ38_200
                MemoEdit5.Text  =  .SDIGITALRJ38_201
                MemoEdit6.Text  =  .SDIGITALRJ38_202
                MemoEdit7.Text  =  .SDIGITALRJ38_203
                MemoEdit3.Text  =  .SDIGITALRJ38_204
                MemoEdit8.Text  =  .SDIGITALRJ38_205
                MemoEdit9.Text  =  .SDIGITALRJ38_206
                MemoEdit10.Text  =  .SDIGITALRJ38_207
                MemoEdit11.Text  =  .SDIGITALRJ38_208
                MemoEdit12.Text  =  .SDIGITALRJ38_209
                MemoEdit13.Text  =  .SDIGITALRJ38_210
                MemoEdit14.Text  =  .SDIGITALRJ38_211
                MemoEdit15.Text  =  .SDIGITALRJ38_212
                MemoEdit16.Text  =  .SDIGITALRJ38_213
                'MemoEdit29.Text  =  .SDIGITALRJ38_214
                'MemoEdit28.Text  =  .SDIGITALRJ38_215
                'MemoEdit27.Text  =  .SDIGITALRJ38_216
                'MemoEdit30.Text  =  .SDIGITALRJ38_217
                'MemoEdit26.Text  =  .SDIGITALRJ38_218
                'MemoEdit25.Text  =  .SDIGITALRJ38_219
                'MemoEdit24.Text  =  .SDIGITALRJ38_220
                'MemoEdit23.Text  =  .SDIGITALRJ38_221
                'MemoEdit22.Text  =  .SDIGITALRJ38_222
                'MemoEdit21.Text  =  .SDIGITALRJ38_223
                'MemoEdit20.Text  =  .SDIGITALRJ38_224
                'MemoEdit19.Text  =  .SDIGITALRJ38_225
                'MemoEdit18.Text  =  .SDIGITALRJ38_226
                'MemoEdit17.Text  =  .SDIGITALRJ38_227
                MemoEdit41.Text  =  .SDIGITALRJ38_228
                MemoEdit40.Text  =  .SDIGITALRJ38_229
                MemoEdit39.Text  =  .SDIGITALRJ38_230
                MemoEdit42.Text  =  .SDIGITALRJ38_231
                MemoEdit31.Text  =  .SDIGITALRJ38_232
                MemoEdit44.Text  =  .SDIGITALRJ38_233
                MemoEdit43.Text  =  .SDIGITALRJ38_234
                MemoEdit34.Text  =  .SDIGITALRJ38_235
                MemoEdit33.Text  =  .SDIGITALRJ38_236
                MemoEdit32.Text  =  .SDIGITALRJ38_237
                MemoEdit35.Text  =  .SDIGITALRJ38_238
                MemoEdit38.Text  =  .SDIGITALRJ38_239
                MemoEdit37.Text  =  .SDIGITALRJ38_240
                MemoEdit36.Text  =  .SDIGITALRJ38_241
                MemoEdit49.Text  =  .SDIGITALRJ38_242
                MemoEdit50.Text  =  .SDIGITALRJ38_243
                MemoEdit51.Text  =  .SDIGITALRJ38_244
                MemoEdit45.Text  =  .SDIGITALRJ38_245
                MemoEdit48.Text  =  .SDIGITALRJ38_246
                MemoEdit47.Text  =  .SDIGITALRJ38_247
                MemoEdit46.Text  =  .SDIGITALRJ38_248
                TextEdit41.Text  =  .SDIGITALRJ38_249
                TextEdit42.Text  =  .SDIGITALRJ38_250
                TextEdit43.Text  =  .SDIGITALRJ38_251
                MemoEdit54.Text  =  .SDIGITALRJ38_252
                MemoEdit53.Text  =  .SDIGITALRJ38_253
                MemoEdit52.Text  =  .SDIGITALRJ38_254
                MemoEdit55.Text  =  .SDIGITALRJ38_255
                MemoEdit59.Text  =  .SDIGITALRJ38_256
                MemoEdit58.Text  =  .SDIGITALRJ38_257
                MemoEdit56.Text  =  .SDIGITALRJ38_258
                TextEdit44.Text  =  .SDIGITALRJ38_259
                CheckEdit141.Checked  =  .SDIGITALRJ38_260
                CheckEdit142.Checked  =  .SDIGITALRJ38_261
                CheckEdit143.Checked  =  .SDIGITALRJ38_262
                CheckEdit144.Checked  =  .SDIGITALRJ38_263
                CheckEdit145.Checked  =  .SDIGITALRJ38_264
                CheckEdit146.Checked  =  .SDIGITALRJ38_265
                CheckEdit147.Checked  =  .SDIGITALRJ38_266
                CheckEdit148.Checked  =  .SDIGITALRJ38_267
                CheckEdit149.Checked  =  .SDIGITALRJ38_268
                CheckEdit150.Checked  =  .SDIGITALRJ38_269
                CheckEdit151.Checked  =  .SDIGITALRJ38_270
                CheckEdit152.Checked  =  .SDIGITALRJ38_271
                CheckEdit153.Checked  =  .SDIGITALRJ38_272
                CheckEdit154.Checked  =  .SDIGITALRJ38_273
                CheckEdit155.Checked  =  .SDIGITALRJ38_274
                TextEdit45.Text  =  .SDIGITALRJ38_275
                MemoEdit57.Text  =  .SDIGITALRJ38_276
                MemoEdit60.Text  =  .SDIGITALRJ38_277
                TextEdit46.Text  =  .SDIGITALRJ38_278
                TextEdit47.Text  =  .SDIGITALRJ38_279
                MemoEdit61.Text  =  .SDIGITALRJ38_280
                MemoEdit62.Text  =  .SDIGITALRJ38_281
                MemoEdit63.Text  =  .SDIGITALRJ38_282

                deDATE.DateTime = .DATE

                BindingSource1.DataSource = oS_DIGITAL_RJ_38.GetDataDetail(grv_RiwayatAskep.GetFocusedRowCellValue("KDKUNJUNGAN"))
                grdDetail.DataSource = BindingSource1

            End With
            
            SplashScreenManager.CloseForm(False)

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail.DeleteSelectedRows()
    End Sub


#End Region
End Class