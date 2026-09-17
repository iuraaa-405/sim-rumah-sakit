Imports DataAccess
Imports UI.WIN.MAIN.My.Resources

Public Class frmEMedrekRI_24
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_RI_24 As New Digital.clsDigital_RI_24
    Private sKODEDOKTER As String
    Private sNAMADOKTER  As String
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
            txtUmur.Text = dsPendaftaran.USIA
            txtJAM.Text = Now.ToString("HH:mm")
            txtTanggalDaftar.Text = dsPendaftaran.DATE.ToString("dd-MM-yyyy HH:mm:ss")
            txtNoRegister.Text = dsPendaftaran.KDKUNJUNGAN
            txtTujuan.Text = dsPendaftaran.TUJUAN
            txtDokter.Text = dsPendaftaran.DOKTER
            txtPerawat.Text = sUserID
            sKODEDOKTER = dsPendaftaran.KDDOKTER
            sNAMADOKTER =  dsPendaftaran.DOKTER
           
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
        Me.Text = EMedrekRI_24.TITLE
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
        fn_NOIDUSER()
        fn_Doctor()
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
        txtJAM.Properties.ReadOnly = Status
        txtPerawat.Properties.ReadOnly = Status
        txtALASAN_KUNJUNGAN.Properties.ReadOnly = Status
        txtKELUHAN_UTAMA.Properties.ReadOnly = Status
        chkASESMEN_01.Properties.ReadOnly = Status
        chkASESMEN_02.Properties.ReadOnly = Status
        chkASESMEN_03.Properties.ReadOnly = Status
        chkASESMEN_04.Properties.ReadOnly = Status
        chkASESMEN_05.Properties.ReadOnly = Status
        chkASESMEN_06.Properties.ReadOnly = Status
        chkASESMEN_07.Properties.ReadOnly = Status
        chkASESMEN_08.Properties.ReadOnly = Status
        chkASESMEN_09.Properties.ReadOnly = Status
        chkASESMEN_10.Properties.ReadOnly = Status
        txtASESMEN_11.Properties.ReadOnly = Status
        chkASESMEN_12.Properties.ReadOnly = Status
        chkASESMEN_13.Properties.ReadOnly = Status
        chkASESMEN_14.Properties.ReadOnly = Status
        chkASESMEN_15.Properties.ReadOnly = Status
        chkASESMEN_16.Properties.ReadOnly = Status
        chkASESMEN_17.Properties.ReadOnly = Status
        chkASESMEN_18.Properties.ReadOnly = Status
        chkASESMEN_19.Properties.ReadOnly = Status
        chkASESMEN_20.Properties.ReadOnly = Status
        chkASESMEN_21.Properties.ReadOnly = Status
        chkASESMEN_22.Properties.ReadOnly = Status
        chkASESMEN_23.Properties.ReadOnly = Status
        chkASESMEN_24.Properties.ReadOnly = Status
        chkASESMEN_25.Properties.ReadOnly = Status
        chkASESMEN_26.Properties.ReadOnly = Status
        chkASESMEN_27.Properties.ReadOnly = Status
        chkASESMEN_28.Properties.ReadOnly = Status
        chkASESMEN_29.Properties.ReadOnly = Status
        chkASESMEN_30.Properties.ReadOnly = Status
        txtASESMEN_31.Properties.ReadOnly = Status
        chkASESMEN_32.Properties.ReadOnly = Status
        chkASESMEN_33.Properties.ReadOnly = Status
        txtASESMEN_34.Properties.ReadOnly = Status
        chkASESMEN_35.Properties.ReadOnly = Status
        txtASESMEN_36.Properties.ReadOnly = Status
        chkASESMEN_37.Properties.ReadOnly = Status
        chkASESMEN_38.Properties.ReadOnly = Status
        chkASESMEN_39.Properties.ReadOnly = Status
        chkASESMEN_40.Properties.ReadOnly = Status
        chkASESMEN_41.Properties.ReadOnly = Status
        chkASESMEN_42.Properties.ReadOnly = Status
        chkASESMEN_43.Properties.ReadOnly = Status
        chkASESMEN_44.Properties.ReadOnly = Status
        txtASESMEN_45.Properties.ReadOnly = Status
        chkASESMEN_46.Properties.ReadOnly = Status
        chkASESMEN_47.Properties.ReadOnly = Status
        chkASESMEN_48.Properties.ReadOnly = Status
        txtASESMEN_49.Properties.ReadOnly = Status
        txtASESMEN_50.Properties.ReadOnly = Status
        chkASESMEN_51.Properties.ReadOnly = Status
        chkASESMEN_52.Properties.ReadOnly = Status
        txtASESMEN_53.Properties.ReadOnly = Status
        chkASESMEN_54.Properties.ReadOnly = Status
        chkASESMEN_55.Properties.ReadOnly = Status
        chkASESMEN_56.Properties.ReadOnly = Status
        txtASESMEN_57.Properties.ReadOnly = Status
        txtASESMEN_58.Properties.ReadOnly = Status
        chkASESMEN_59.Properties.ReadOnly = Status
        chkASESMEN_60.Properties.ReadOnly = Status
        chkASESMEN_61.Properties.ReadOnly = Status
        chkASESMEN_62.Properties.ReadOnly = Status
        chkASESMEN_63.Properties.ReadOnly = Status
        chkASESMEN_64.Properties.ReadOnly = Status
        chkASESMEN_65.Properties.ReadOnly = Status
        chkASESMEN_66.Properties.ReadOnly = Status
        chkASESMEN_67.Properties.ReadOnly = Status
        txtASESMEN_68.Properties.ReadOnly = Status
        chkASESMEN_69.Properties.ReadOnly = Status
        chkASESMEN_70.Properties.ReadOnly = Status
        chkASESMEN_71.Properties.ReadOnly = Status
        chkASESMEN_72.Properties.ReadOnly = Status
        chkASESMEN_73.Properties.ReadOnly = Status
        chkASESMEN_74.Properties.ReadOnly = Status
        chkASESMEN_75.Properties.ReadOnly = Status
        chkASESMEN_76.Properties.ReadOnly = Status
        chkASESMEN_77.Properties.ReadOnly = Status
        chkASESMEN_78.Properties.ReadOnly = Status
        chkASESMEN_79.Properties.ReadOnly = Status
        chkASESMEN_80.Properties.ReadOnly = Status
        chkASESMEN_81.Properties.ReadOnly = Status
        chkASESMEN_82.Properties.ReadOnly = Status
        chkASESMEN_83.Properties.ReadOnly = Status
        txtASESMEN_84.Properties.ReadOnly = Status
        chkASESMEN_85.Properties.ReadOnly = Status
        chkASESMEN_86.Properties.ReadOnly = Status
        chkASESMEN_87.Properties.ReadOnly = Status
        chkASESMEN_88.Properties.ReadOnly = Status
        chkASESMEN_89.Properties.ReadOnly = Status
        chkASESMEN_90.Properties.ReadOnly = Status
        chkASESMEN_91.Properties.ReadOnly = Status
        chkASESMEN_92.Properties.ReadOnly = Status
        chkASESMEN_93.Properties.ReadOnly = Status
        chkASESMEN_94.Properties.ReadOnly = Status
        chkASESMEN_95.Properties.ReadOnly = Status
        chkASESMEN_96.Properties.ReadOnly = Status
        chkASESMEN_97.Properties.ReadOnly = Status
        chkASESMEN_98.Properties.ReadOnly = Status
        chkASESMEN_99.Properties.ReadOnly = Status
        chkASESMEN_100.Properties.ReadOnly = Status
        chkASESMEN_101.Properties.ReadOnly = Status
        chkASESMEN_102.Properties.ReadOnly = Status
        chkASESMEN_103.Properties.ReadOnly = Status
        chkASESMEN_104.Properties.ReadOnly = Status
        chkASESMEN_105.Properties.ReadOnly = Status
        txtASESMEN_106.Properties.ReadOnly = Status
        txtASESMEN_107.Properties.ReadOnly = Status
        txtASESMEN_108.Properties.ReadOnly = Status
        txtASESMEN_109.Properties.ReadOnly = Status
        chkASESMEN_110.Properties.ReadOnly = Status
        chkASESMEN_111.Properties.ReadOnly = Status
        chkASESMEN_112.Properties.ReadOnly = Status
        chkASESMEN_113.Properties.ReadOnly = Status
        chkASESMEN_114.Properties.ReadOnly = Status
        chkASESMEN_115.Properties.ReadOnly = Status
        chkASESMEN_116.Properties.ReadOnly = Status
        chkASESMEN_117.Properties.ReadOnly = Status
        chkASESMEN_118.Properties.ReadOnly = Status
        chkASESMEN_119.Properties.ReadOnly = Status
        chkASESMEN_120.Properties.ReadOnly = Status
        chkASESMEN_121.Properties.ReadOnly = Status
        chkASESMEN_122.Properties.ReadOnly = Status
        chkASESMEN_123.Properties.ReadOnly = Status
        chkASESMEN_124.Properties.ReadOnly = Status
        chkASESMEN_125.Properties.ReadOnly = Status
        chkASESMEN_126.Properties.ReadOnly = Status
        chkASESMEN_127.Properties.ReadOnly = Status
        chkASESMEN_128.Properties.ReadOnly = Status
        chkASESMEN_129.Properties.ReadOnly = Status
        chkASESMEN_130.Properties.ReadOnly = Status
        chkASESMEN_131.Properties.ReadOnly = Status
        chkASESMEN_132.Properties.ReadOnly = Status
        chkASESMEN_133.Properties.ReadOnly = Status
        chkASESMEN_134.Properties.ReadOnly = Status
        chkASESMEN_135.Properties.ReadOnly = Status
        chkASESMEN_136.Properties.ReadOnly = Status
        chkASESMEN_137.Properties.ReadOnly = Status
        chkASESMEN_138.Properties.ReadOnly = Status
        chkASESMEN_139.Properties.ReadOnly = Status
        chkASESMEN_140.Properties.ReadOnly = Status
        chkASESMEN_141.Properties.ReadOnly = Status
        chkASESMEN_142.Properties.ReadOnly = Status
        chkASESMEN_143.Properties.ReadOnly = Status
        chkASESMEN_144.Properties.ReadOnly = Status
        chkASESMEN_145.Properties.ReadOnly = Status
        chkASESMEN_146.Properties.ReadOnly = Status
        chkASESMEN_147.Properties.ReadOnly = Status
        chkASESMEN_148.Properties.ReadOnly = Status
        chkASESMEN_149.Properties.ReadOnly = Status
        chkASESMEN_150.Properties.ReadOnly = Status
        chkASESMEN_151.Properties.ReadOnly = Status
        chkASESMEN_152.Properties.ReadOnly = Status
        chkASESMEN_153.Properties.ReadOnly = Status
        chkASESMEN_154.Properties.ReadOnly = Status
        chkASESMEN_155.Properties.ReadOnly = Status
        chkASESMEN_156.Properties.ReadOnly = Status
        chkASESMEN_157.Properties.ReadOnly = Status
        chkASESMEN_158.Properties.ReadOnly = Status
        chkASESMEN_159.Properties.ReadOnly = Status
        chkASESMEN_160.Properties.ReadOnly = Status
        chkASESMEN_161.Properties.ReadOnly = Status
        chkASESMEN_162.Properties.ReadOnly = Status
        chkASESMEN_163.Properties.ReadOnly = Status
        chkASESMEN_164.Properties.ReadOnly = Status
        chkASESMEN_165.Properties.ReadOnly = Status
        chkASESMEN_166.Properties.ReadOnly = Status
        chkASESMEN_167.Properties.ReadOnly = Status
        chkASESMEN_168.Properties.ReadOnly = Status
        chkASESMEN_169.Properties.ReadOnly = Status
        txtASESMEN_170.Properties.ReadOnly = Status
        txtTGL_DITEMUKAN_01.Properties.ReadOnly = Status
        txtTGL_DITEMUKAN_02.Properties.ReadOnly = Status
        txtTGL_DITEMUKAN_03.Properties.ReadOnly = Status
        txtTGL_DITEMUKAN_04.Properties.ReadOnly = Status
        txtTGL_TERATASI_01.Properties.ReadOnly = Status
        txtTGL_TERATASI_02.Properties.ReadOnly = Status
        txtTGL_TERATASI_03.Properties.ReadOnly = Status
        txtTGL_TERATASI_04.Properties.ReadOnly = Status
        chkDP_YA_01.Properties.ReadOnly = Status
        chkDP_YA_02.Properties.ReadOnly = Status
        chkDP_YA_03.Properties.ReadOnly = Status
        chkDP_YA_04.Properties.ReadOnly = Status
        chkDP_YA_05.Properties.ReadOnly = Status
        chkDP_YA_06.Properties.ReadOnly = Status
        chkDP_YA_07.Properties.ReadOnly = Status
        chkDP_YA_08.Properties.ReadOnly = Status
        chkDP_TIDAK_01.Properties.ReadOnly = Status
        chkDP_TIDAK_02.Properties.ReadOnly = Status
        chkDP_TIDAK_03.Properties.ReadOnly = Status
        chkDP_TIDAK_04.Properties.ReadOnly = Status
        chkDP_TIDAK_05.Properties.ReadOnly = Status
        chkDP_TIDAK_06.Properties.ReadOnly = Status
        chkDP_TIDAK_07.Properties.ReadOnly = Status
        chkDP_TIDAK_08.Properties.ReadOnly = Status
        txtDP_KET_01.Properties.ReadOnly = Status
        txtDP_KET_02.Properties.ReadOnly = Status
        txtDP_KET_03.Properties.ReadOnly = Status
        txtDP_KET_04.Properties.ReadOnly = Status
        txtDP_KET_05.Properties.ReadOnly = Status
        txtDP_KET_06.Properties.ReadOnly = Status
        txtDP_KET_07.Properties.ReadOnly = Status
        txtDP_KET_08.Properties.ReadOnly = Status

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
        'txtCODE.Text = "<--- AUTO --->"

        deDATE.DateTime = Now
        txtALASAN_KUNJUNGAN.ResetText()
        txtKELUHAN_UTAMA.ResetText()
        chkASESMEN_01.Checked = False
        chkASESMEN_02.Checked = False
        chkASESMEN_03.Checked = False
        chkASESMEN_04.Checked = False
        chkASESMEN_05.Checked = False
        chkASESMEN_06.Checked = False
        chkASESMEN_07.Checked = False
        chkASESMEN_08.Checked = False
        chkASESMEN_09.Checked = False
        chkASESMEN_10.Checked = False
        txtASESMEN_11.ResetText()
        chkASESMEN_12.Checked = False
        chkASESMEN_13.Checked = False
        chkASESMEN_14.Checked = False
        chkASESMEN_15.Checked = False
        chkASESMEN_16.Checked = False
        chkASESMEN_17.Checked = False
        chkASESMEN_18.Checked = False
        chkASESMEN_19.Checked = False
        chkASESMEN_20.Checked = False
        chkASESMEN_21.Checked = False
        chkASESMEN_22.Checked = False
        chkASESMEN_23.Checked = False
        chkASESMEN_24.Checked = False
        chkASESMEN_25.Checked = False
        chkASESMEN_26.Checked = False
        chkASESMEN_27.Checked = False
        chkASESMEN_28.Checked = False
        chkASESMEN_29.Checked = False
        chkASESMEN_30.Checked = False
        txtASESMEN_31.ResetText()
        chkASESMEN_32.Checked = False
        chkASESMEN_33.Checked = False
        txtASESMEN_34.ResetText()
        chkASESMEN_35.Checked = False
        txtASESMEN_36.ResetText()
        chkASESMEN_37.Checked = False
        chkASESMEN_38.Checked = False
        chkASESMEN_39.Checked = False
        chkASESMEN_40.Checked = False
        chkASESMEN_41.Checked = False
        chkASESMEN_42.Checked = False
        chkASESMEN_43.Checked = False
        chkASESMEN_44.Checked = False
        txtASESMEN_45.ResetText()
        chkASESMEN_46.Checked = False
        chkASESMEN_47.Checked = False
        chkASESMEN_48.Checked = False
        txtASESMEN_49.ResetText()
        txtASESMEN_50.ResetText()
        chkASESMEN_51.Checked = False
        chkASESMEN_52.Checked = False
        txtASESMEN_53.ResetText()
        chkASESMEN_54.Checked = False
        chkASESMEN_55.Checked = False
        chkASESMEN_56.Checked = False
        txtASESMEN_57.ResetText()
        txtASESMEN_58.ResetText()
        chkASESMEN_59.Checked = False
        chkASESMEN_60.Checked = False
        chkASESMEN_61.Checked = False
        chkASESMEN_62.Checked = False
        chkASESMEN_63.Checked = False
        chkASESMEN_64.Checked = False
        chkASESMEN_65.Checked = False
        chkASESMEN_66.Checked = False
        chkASESMEN_67.Checked = False
        txtASESMEN_68.ResetText()
        chkASESMEN_69.Checked = False
        chkASESMEN_70.Checked = False
        chkASESMEN_71.Checked = False
        chkASESMEN_72.Checked = False
        chkASESMEN_73.Checked = False
        chkASESMEN_74.Checked = False
        chkASESMEN_75.Checked = False
        chkASESMEN_76.Checked = False
        chkASESMEN_77.Checked = False
        chkASESMEN_78.Checked = False
        chkASESMEN_79.Checked = False
        chkASESMEN_80.Checked = False
        chkASESMEN_81.Checked = False
        chkASESMEN_82.Checked = False
        chkASESMEN_83.Checked = False
        txtASESMEN_84.ResetText()
        chkASESMEN_85.Checked = False
        chkASESMEN_86.Checked = False
        chkASESMEN_87.Checked = False
        chkASESMEN_88.Checked = False
        chkASESMEN_89.Checked = False
        chkASESMEN_90.Checked = False
        chkASESMEN_91.Checked = False
        chkASESMEN_92.Checked = False
        chkASESMEN_93.Checked = False
        chkASESMEN_94.Checked = False
        chkASESMEN_95.Checked = False
        chkASESMEN_96.Checked = False
        chkASESMEN_97.Checked = False
        chkASESMEN_98.Checked = False
        chkASESMEN_99.Checked = False
        chkASESMEN_100.Checked = False
        chkASESMEN_101.Checked = False
        chkASESMEN_102.Checked = False
        chkASESMEN_103.Checked = False
        chkASESMEN_104.Checked = False
        chkASESMEN_105.Checked = False
        txtASESMEN_106.ResetText()
        txtASESMEN_107.ResetText()
        txtASESMEN_108.ResetText()
        txtASESMEN_109.ResetText()
        chkASESMEN_110.Checked = False
        chkASESMEN_111.Checked = False
        chkASESMEN_112.Checked = False
        chkASESMEN_113.Checked = False
        chkASESMEN_114.Checked = False
        chkASESMEN_115.Checked = False
        chkASESMEN_116.Checked = False
        chkASESMEN_117.Checked = False
        chkASESMEN_118.Checked = False
        chkASESMEN_119.Checked = False
        chkASESMEN_120.Checked = False
        chkASESMEN_121.Checked = False
        chkASESMEN_122.Checked = False
        chkASESMEN_123.Checked = False
        chkASESMEN_124.Checked = False
        chkASESMEN_125.Checked = False
        chkASESMEN_126.Checked = False
        chkASESMEN_127.Checked = False
        chkASESMEN_128.Checked = False
        chkASESMEN_129.Checked = False
        chkASESMEN_130.Checked = False
        chkASESMEN_131.Checked = False
        chkASESMEN_132.Checked = False
        chkASESMEN_133.Checked = False
        chkASESMEN_134.Checked = False
        chkASESMEN_135.Checked = False
        chkASESMEN_136.Checked = False
        chkASESMEN_137.Checked = False
        chkASESMEN_138.Checked = False
        chkASESMEN_139.Checked = False
        chkASESMEN_140.Checked = False
        chkASESMEN_141.Checked = False
        chkASESMEN_142.Checked = False
        chkASESMEN_143.Checked = False
        chkASESMEN_144.Checked = False
        chkASESMEN_145.Checked = False
        chkASESMEN_146.Checked = False
        chkASESMEN_147.Checked = False
        chkASESMEN_148.Checked = False
        chkASESMEN_149.Checked = False
        chkASESMEN_150.Checked = False
        chkASESMEN_151.Checked = False
        chkASESMEN_152.Checked = False
        chkASESMEN_153.Checked = False
        chkASESMEN_154.Checked = False
        chkASESMEN_155.Checked = False
        chkASESMEN_156.Checked = False
        chkASESMEN_157.Checked = False
        chkASESMEN_158.Checked = False
        chkASESMEN_159.Checked = False
        chkASESMEN_160.Checked = False
        chkASESMEN_161.Checked = False
        chkASESMEN_162.Checked = False
        chkASESMEN_163.Checked = False
        chkASESMEN_164.Checked = False
        chkASESMEN_165.Checked = False
        chkASESMEN_166.Checked = False
        chkASESMEN_167.Checked = False
        chkASESMEN_168.Checked = False
        chkASESMEN_169.Checked = False
        txtASESMEN_170.ResetText()
        txtTGL_DITEMUKAN_01.ResetText()
        txtTGL_DITEMUKAN_02.ResetText()
        txtTGL_DITEMUKAN_03.ResetText()
        txtTGL_DITEMUKAN_04.ResetText()
        txtTGL_TERATASI_01.ResetText()
        txtTGL_TERATASI_02.ResetText()
        txtTGL_TERATASI_03.ResetText()
        txtTGL_TERATASI_04.ResetText()
        chkDP_YA_01.Checked = False
        chkDP_YA_02.Checked = False
        chkDP_YA_03.Checked = False
        chkDP_YA_04.Checked = False
        chkDP_YA_05.Checked = False
        chkDP_YA_06.Checked = False
        chkDP_YA_07.Checked = False
        chkDP_YA_08.Checked = False
        chkDP_TIDAK_01.Checked = False
        chkDP_TIDAK_02.Checked = False
        chkDP_TIDAK_03.Checked = False
        chkDP_TIDAK_04.Checked = False
        chkDP_TIDAK_05.Checked = False
        chkDP_TIDAK_06.Checked = False
        chkDP_TIDAK_07.Checked = False
        chkDP_TIDAK_08.Checked = False
        txtDP_KET_01.ResetText()
        txtDP_KET_02.ResetText()
        txtDP_KET_03.ResetText()
        txtDP_KET_04.ResetText()
        txtDP_KET_05.ResetText()
        txtDP_KET_06.ResetText()
        txtDP_KET_07.ResetText()
        txtDP_KET_08.ResetText()

        fn_LoadAsessmenRawatJalan(txtNoRegister.text)

    End Sub

    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RI_24.GetData(txtNoRegister.Text)

            With ds
                deDATE.DateTime = .DATE
                txtJAM.Text = .JAM
                txtDokter.Text = .DOKTER_KODE
                txtPerawat.Text = .KDUSER
                txtALASAN_KUNJUNGAN.Text = .ALASAN_KUNJUNGAN
                txtKELUHAN_UTAMA.Text = .KELUHAN_UTAMA
                chkASESMEN_01.Checked = .ASESMEN_01
                chkASESMEN_02.Checked = .ASESMEN_02
                chkASESMEN_03.Checked = .ASESMEN_03
                chkASESMEN_04.Checked = .ASESMEN_04
                chkASESMEN_05.Checked = .ASESMEN_05
                chkASESMEN_06.Checked = .ASESMEN_06
                chkASESMEN_07.Checked = .ASESMEN_07
                chkASESMEN_08.Checked = .ASESMEN_08
                chkASESMEN_09.Checked = .ASESMEN_09
                chkASESMEN_10.Checked = .ASESMEN_10
                txtASESMEN_11.Text = .ASESMEN_11
                chkASESMEN_12.Checked = .ASESMEN_12
                chkASESMEN_13.Checked = .ASESMEN_13
                chkASESMEN_14.Checked = .ASESMEN_14
                chkASESMEN_15.Checked = .ASESMEN_15
                chkASESMEN_16.Checked = .ASESMEN_16
                chkASESMEN_17.Checked = .ASESMEN_17
                chkASESMEN_18.Checked = .ASESMEN_18
                chkASESMEN_19.Checked = .ASESMEN_19
                chkASESMEN_20.Checked = .ASESMEN_20
                chkASESMEN_21.Checked = .ASESMEN_21
                chkASESMEN_22.Checked = .ASESMEN_22
                chkASESMEN_23.Checked = .ASESMEN_23
                chkASESMEN_24.Checked = .ASESMEN_24
                chkASESMEN_25.Checked = .ASESMEN_25
                chkASESMEN_26.Checked = .ASESMEN_26
                chkASESMEN_27.Checked = .ASESMEN_27
                chkASESMEN_28.Checked = .ASESMEN_28
                chkASESMEN_29.Checked = .ASESMEN_29
                chkASESMEN_30.Checked = .ASESMEN_30
                txtASESMEN_31.Text = .ASESMEN_31
                chkASESMEN_32.Checked = .ASESMEN_32
                chkASESMEN_33.Checked = .ASESMEN_33
                txtASESMEN_34.Text = .ASESMEN_34
                chkASESMEN_35.Checked = .ASESMEN_35
                txtASESMEN_36.Text = .ASESMEN_36
                chkASESMEN_37.Checked = .ASESMEN_37
                chkASESMEN_38.Checked = .ASESMEN_38
                chkASESMEN_39.Checked = .ASESMEN_39
                chkASESMEN_40.Checked = .ASESMEN_40
                chkASESMEN_41.Checked = .ASESMEN_41
                chkASESMEN_42.Checked = .ASESMEN_42
                chkASESMEN_43.Checked = .ASESMEN_43
                chkASESMEN_44.Checked = .ASESMEN_44
                txtASESMEN_45.Text = .ASESMEN_45
                chkASESMEN_46.Checked = .ASESMEN_46
                chkASESMEN_47.Checked = .ASESMEN_47
                chkASESMEN_48.Checked = .ASESMEN_48
                txtASESMEN_49.Text = .ASESMEN_49
                txtASESMEN_50.Text = .ASESMEN_50
                chkASESMEN_51.Checked = .ASESMEN_51
                chkASESMEN_52.Checked = .ASESMEN_52
                txtASESMEN_53.Text = .ASESMEN_53
                chkASESMEN_54.Checked = .ASESMEN_54
                chkASESMEN_55.Checked = .ASESMEN_55
                chkASESMEN_56.Checked = .ASESMEN_56
                txtASESMEN_57.Text = .ASESMEN_57
                txtASESMEN_58.Text = .ASESMEN_58
                chkASESMEN_59.Checked = .ASESMEN_59
                chkASESMEN_60.Checked = .ASESMEN_60
                chkASESMEN_61.Checked = .ASESMEN_61
                chkASESMEN_62.Checked = .ASESMEN_62
                chkASESMEN_63.Checked = .ASESMEN_63
                chkASESMEN_64.Checked = .ASESMEN_64
                chkASESMEN_65.Checked = .ASESMEN_65
                chkASESMEN_66.Checked = .ASESMEN_66
                chkASESMEN_67.Checked = .ASESMEN_67
                txtASESMEN_68.Text = .ASESMEN_68
                chkASESMEN_69.Checked = .ASESMEN_69
                chkASESMEN_70.Checked = .ASESMEN_70
                chkASESMEN_71.Checked = .ASESMEN_71
                chkASESMEN_72.Checked = .ASESMEN_72
                chkASESMEN_73.Checked = .ASESMEN_73
                chkASESMEN_74.Checked = .ASESMEN_74
                chkASESMEN_75.Checked = .ASESMEN_75
                chkASESMEN_76.Checked = .ASESMEN_76
                chkASESMEN_77.Checked = .ASESMEN_77
                chkASESMEN_78.Checked = .ASESMEN_78
                chkASESMEN_79.Checked = .ASESMEN_79
                chkASESMEN_80.Checked = .ASESMEN_80
                chkASESMEN_81.Checked = .ASESMEN_81
                chkASESMEN_82.Checked = .ASESMEN_82
                chkASESMEN_83.Checked = .ASESMEN_83
                txtASESMEN_84.Text = .ASESMEN_84
                chkASESMEN_85.Checked = .ASESMEN_85
                chkASESMEN_86.Checked = .ASESMEN_86
                chkASESMEN_87.Checked = .ASESMEN_87
                chkASESMEN_88.Checked = .ASESMEN_88
                chkASESMEN_89.Checked = .ASESMEN_89
                chkASESMEN_90.Checked = .ASESMEN_90
                chkASESMEN_91.Checked = .ASESMEN_91
                chkASESMEN_92.Checked = .ASESMEN_92
                chkASESMEN_93.Checked = .ASESMEN_93
                chkASESMEN_94.Checked = .ASESMEN_94
                chkASESMEN_95.Checked = .ASESMEN_95
                chkASESMEN_96.Checked = .ASESMEN_96
                chkASESMEN_97.Checked = .ASESMEN_97
                chkASESMEN_98.Checked = .ASESMEN_98
                chkASESMEN_99.Checked = .ASESMEN_99
                chkASESMEN_100.Checked = .ASESMEN_100
                chkASESMEN_101.Checked = .ASESMEN_101
                chkASESMEN_102.Checked = .ASESMEN_102
                chkASESMEN_103.Checked = .ASESMEN_103
                chkASESMEN_104.Checked = .ASESMEN_104
                chkASESMEN_105.Checked = .ASESMEN_105
                txtASESMEN_106.Text = .ASESMEN_106
                txtASESMEN_107.Text = .ASESMEN_107
                txtASESMEN_108.Text = .ASESMEN_108
                txtASESMEN_109.Text = .ASESMEN_109
                chkASESMEN_110.Checked = .ASESMEN_110
                chkASESMEN_111.Checked = .ASESMEN_111
                chkASESMEN_112.Checked = .ASESMEN_112
                chkASESMEN_113.Checked = .ASESMEN_113
                chkASESMEN_114.Checked = .ASESMEN_114
                chkASESMEN_115.Checked = .ASESMEN_115
                chkASESMEN_116.Checked = .ASESMEN_116
                chkASESMEN_117.Checked = .ASESMEN_117
                chkASESMEN_118.Checked = .ASESMEN_118
                chkASESMEN_119.Checked = .ASESMEN_119
                chkASESMEN_120.Checked = .ASESMEN_120
                chkASESMEN_121.Checked = .ASESMEN_121
                chkASESMEN_122.Checked = .ASESMEN_122
                chkASESMEN_123.Checked = .ASESMEN_123
                chkASESMEN_124.Checked = .ASESMEN_124
                chkASESMEN_125.Checked = .ASESMEN_125
                chkASESMEN_126.Checked = .ASESMEN_126
                chkASESMEN_127.Checked = .ASESMEN_127
                chkASESMEN_128.Checked = .ASESMEN_128
                chkASESMEN_129.Checked = .ASESMEN_129
                chkASESMEN_130.Checked = .ASESMEN_130
                chkASESMEN_131.Checked = .ASESMEN_131
                chkASESMEN_132.Checked = .ASESMEN_132
                chkASESMEN_133.Checked = .ASESMEN_133
                chkASESMEN_134.Checked = .ASESMEN_134
                chkASESMEN_135.Checked = .ASESMEN_135
                chkASESMEN_136.Checked = .ASESMEN_136
                chkASESMEN_137.Checked = .ASESMEN_137
                chkASESMEN_138.Checked = .ASESMEN_138
                chkASESMEN_139.Checked = .ASESMEN_139
                chkASESMEN_140.Checked = .ASESMEN_140
                chkASESMEN_141.Checked = .ASESMEN_141
                chkASESMEN_142.Checked = .ASESMEN_142
                chkASESMEN_143.Checked = .ASESMEN_143
                chkASESMEN_144.Checked = .ASESMEN_144
                chkASESMEN_145.Checked = .ASESMEN_145
                chkASESMEN_146.Checked = .ASESMEN_146
                chkASESMEN_147.Checked = .ASESMEN_147
                chkASESMEN_148.Checked = .ASESMEN_148
                chkASESMEN_149.Checked = .ASESMEN_149
                chkASESMEN_150.Checked = .ASESMEN_150
                chkASESMEN_151.Checked = .ASESMEN_151
                chkASESMEN_152.Checked = .ASESMEN_152
                chkASESMEN_153.Checked = .ASESMEN_153
                chkASESMEN_154.Checked = .ASESMEN_154
                chkASESMEN_155.Checked = .ASESMEN_155
                chkASESMEN_156.Checked = .ASESMEN_156
                chkASESMEN_157.Checked = .ASESMEN_157
                chkASESMEN_158.Checked = .ASESMEN_158
                chkASESMEN_159.Checked = .ASESMEN_159
                chkASESMEN_160.Checked = .ASESMEN_160
                chkASESMEN_161.Checked = .ASESMEN_161
                chkASESMEN_162.Checked = .ASESMEN_162
                chkASESMEN_163.Checked = .ASESMEN_163
                chkASESMEN_164.Checked = .ASESMEN_164
                chkASESMEN_165.Checked = .ASESMEN_165
                chkASESMEN_166.Checked = .ASESMEN_166
                chkASESMEN_167.Checked = .ASESMEN_167
                chkASESMEN_168.Checked = .ASESMEN_168
                chkASESMEN_169.Checked = .ASESMEN_169
                txtASESMEN_170.Text = .ASESMEN_170
                txtTGL_DITEMUKAN_01.Text = .TGL_DITEMUKAN_01
                txtTGL_DITEMUKAN_02.Text = .TGL_DITEMUKAN_02
                txtTGL_DITEMUKAN_03.Text = .TGL_DITEMUKAN_03
                txtTGL_DITEMUKAN_04.Text = .TGL_DITEMUKAN_04
                txtTGL_TERATASI_01.Text = .TGL_TERATASI_01
                txtTGL_TERATASI_02.Text = .TGL_TERATASI_02
                txtTGL_TERATASI_03.Text = .TGL_TERATASI_03
                txtTGL_TERATASI_04.Text = .TGL_TERATASI_04
                chkDP_YA_01.Checked = .DP_YA_01
                chkDP_YA_02.Checked = .DP_YA_02
                chkDP_YA_03.Checked = .DP_YA_03
                chkDP_YA_04.Checked = .DP_YA_04
                chkDP_YA_05.Checked = .DP_YA_05
                chkDP_YA_06.Checked = .DP_YA_06
                chkDP_YA_07.Checked = .DP_YA_07
                chkDP_YA_08.Checked = .DP_YA_08
                chkDP_TIDAK_01.Checked = .DP_TIDAK_01
                chkDP_TIDAK_02.Checked = .DP_TIDAK_02
                chkDP_TIDAK_03.Checked = .DP_TIDAK_03
                chkDP_TIDAK_04.Checked = .DP_TIDAK_04
                chkDP_TIDAK_05.Checked = .DP_TIDAK_05
                chkDP_TIDAK_06.Checked = .DP_TIDAK_06
                chkDP_TIDAK_07.Checked = .DP_TIDAK_07
                chkDP_TIDAK_08.Checked = .DP_TIDAK_08
                txtDP_KET_01.Text = .DP_KET_01
                txtDP_KET_02.Text = .DP_KET_02
                txtDP_KET_03.Text = .DP_KET_03
                txtDP_KET_04.Text = .DP_KET_04
                txtDP_KET_05.Text = .DP_KET_05
                txtDP_KET_06.Text = .DP_KET_06
                txtDP_KET_07.Text = .DP_KET_07
                txtDP_KET_08.Text = .DP_KET_08

            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadAsessmenRawatJalan(ByVal Parameter As String)
        'Dim oAsessmenRawatJalan As New Digital.clsDigital_RJ_08
        'Dim dsAsessmenRawatJalan = oAsessmenRawatJalan.GetData(Parameter)

        'If dsAsessmenRawatJalan IsNot Nothing Then
        '    'txtSOAP_O.Text = "Tensi : " & dsAsessmenRawatJalan.TANDA_VITAL_01 & " mmHg" & vbCrLf & vbCrLf & "Nadi : " & dsAsessmenRawatJalan.TANDA_VITAL_02 & " x/m" & vbCrLf & vbCrLf & "Respirasi : " & dsAsessmenRawatJalan.TANDA_VITAL_04 & " x/m" & vbCrLf & vbCrLf & "Suhu : " & dsAsessmenRawatJalan.TANDA_VITAL_03 & " °C" & vbCrLf & vbCrLf & "Berat Badan : " & dsAsessmenRawatJalan.TANDA_VITAL_05 & " Kg" & vbCrLf & vbCrLf & "Tinggi Badan : " & dsAsessmenRawatJalan.TANDA_VITAL_06 & " Cm"
        '    txtKELUHAN_UTAMA.Text = dsAsessmenRawatJalan.KELUHAN_UTAMA
        '    txtASESMEN_53.Text = dsAsessmenRawatJalan.TANDA_VITAL_01
        '    txtASESMEN_50.text = dsAsessmenRawatJalan.TANDA_VITAL_02
        '    txtASESMEN_45.text = dsAsessmenRawatJalan.TANDA_VITAL_04
        '    chkASESMEN_51.Text = dsAsessmenRawatJalan.TANDA_VITAL_REGULER
        '    chkASESMEN_52.Text = dsAsessmenRawatJalan.TANDA_VITAL_IREGULER
        'Else
        '    MsgBox("Assemen Awal Keperawatan rawat jalan belum di input", MsgBoxStyle.Exclamation, Me.Text)
        'End If
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
            Dim ds = oS_DIGITAL_RI_24.GetStructureHeader
            With ds
                .KDKUNJUNGAN = txtNoRegister.Text
                Try
                    .DATECREATED = oS_DIGITAL_RI_24.GetData(txtNoRegister.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .DATE = deDATE.DateTime
                .JAM = txtJAM.Text
                
                .ALASAN_KUNJUNGAN = txtALASAN_KUNJUNGAN.Text
                .KELUHAN_UTAMA = txtKELUHAN_UTAMA.Text
                .ASESMEN_01 = chkASESMEN_01.Checked
                .ASESMEN_02 = chkASESMEN_02.Checked
                .ASESMEN_03 = chkASESMEN_03.Checked
                .ASESMEN_04 = chkASESMEN_04.Checked
                .ASESMEN_05 = chkASESMEN_05.Checked
                .ASESMEN_06 = chkASESMEN_06.Checked
                .ASESMEN_07 = chkASESMEN_07.Checked
                .ASESMEN_08 = chkASESMEN_08.Checked
                .ASESMEN_09 = chkASESMEN_09.Checked
                .ASESMEN_10 = chkASESMEN_10.Checked
                .ASESMEN_11 = txtASESMEN_11.Text
                .ASESMEN_12 = chkASESMEN_12.Checked
                .ASESMEN_13 = chkASESMEN_13.Checked
                .ASESMEN_14 = chkASESMEN_14.Checked
                .ASESMEN_15 = chkASESMEN_15.Checked
                .ASESMEN_16 = chkASESMEN_16.Checked
                .ASESMEN_17 = chkASESMEN_17.Checked
                .ASESMEN_18 = chkASESMEN_18.Checked
                .ASESMEN_19 = chkASESMEN_19.Checked
                .ASESMEN_20 = chkASESMEN_20.Checked
                .ASESMEN_21 = chkASESMEN_21.Checked
                .ASESMEN_22 = chkASESMEN_22.Checked
                .ASESMEN_23 = chkASESMEN_23.Checked
                .ASESMEN_24 = chkASESMEN_24.Checked
                .ASESMEN_25 = chkASESMEN_25.Checked
                .ASESMEN_26 = chkASESMEN_26.Checked
                .ASESMEN_27 = chkASESMEN_27.Checked
                .ASESMEN_28 = chkASESMEN_28.Checked
                .ASESMEN_29 = chkASESMEN_29.Checked
                .ASESMEN_30 = chkASESMEN_30.Checked
                .ASESMEN_31 = txtASESMEN_31.Text
                .ASESMEN_32 = chkASESMEN_32.Checked
                .ASESMEN_33 = chkASESMEN_33.Checked
                .ASESMEN_34 = txtASESMEN_34.Text
                .ASESMEN_35 = chkASESMEN_35.Checked
                .ASESMEN_36 = txtASESMEN_36.Text
                .ASESMEN_37 = chkASESMEN_37.Checked
                .ASESMEN_38 = chkASESMEN_38.Checked
                .ASESMEN_39 = chkASESMEN_39.Checked
                .ASESMEN_40 = chkASESMEN_40.Checked
                .ASESMEN_41 = chkASESMEN_41.Checked
                .ASESMEN_42 = chkASESMEN_42.Checked
                .ASESMEN_43 = chkASESMEN_43.Checked
                .ASESMEN_44 = chkASESMEN_44.Checked
                .ASESMEN_45 = txtASESMEN_45.Text
                .ASESMEN_46 = chkASESMEN_46.Checked
                .ASESMEN_47 = chkASESMEN_47.Checked
                .ASESMEN_48 = chkASESMEN_48.Checked
                .ASESMEN_49 = txtASESMEN_49.Text
                .ASESMEN_50 = txtASESMEN_50.Text
                .ASESMEN_51 = chkASESMEN_51.Checked
                .ASESMEN_52 = chkASESMEN_52.Checked
                .ASESMEN_53 = txtASESMEN_53.Text
                .ASESMEN_54 = chkASESMEN_54.Checked
                .ASESMEN_55 = chkASESMEN_55.Checked
                .ASESMEN_56 = chkASESMEN_56.Checked
                .ASESMEN_57 = txtASESMEN_57.Text
                .ASESMEN_58 = txtASESMEN_58.Text
                .ASESMEN_59 = chkASESMEN_59.Checked
                .ASESMEN_60 = chkASESMEN_60.Checked
                .ASESMEN_61 = chkASESMEN_61.Checked
                .ASESMEN_62 = chkASESMEN_62.Checked
                .ASESMEN_63 = chkASESMEN_63.Checked
                .ASESMEN_64 = chkASESMEN_64.Checked
                .ASESMEN_65 = chkASESMEN_65.Checked
                .ASESMEN_66 = chkASESMEN_66.Checked
                .ASESMEN_67 = chkASESMEN_67.Checked
                .ASESMEN_68 = txtASESMEN_68.Text
                .ASESMEN_69 = chkASESMEN_69.Checked
                .ASESMEN_70 = chkASESMEN_70.Checked
                .ASESMEN_71 = chkASESMEN_71.Checked
                .ASESMEN_72 = chkASESMEN_72.Checked
                .ASESMEN_73 = chkASESMEN_73.Checked
                .ASESMEN_74 = chkASESMEN_74.Checked
                .ASESMEN_75 = chkASESMEN_75.Checked
                .ASESMEN_76 = chkASESMEN_76.Checked
                .ASESMEN_77 = chkASESMEN_77.Checked
                .ASESMEN_78 = chkASESMEN_78.Checked
                .ASESMEN_79 = chkASESMEN_79.Checked
                .ASESMEN_80 = chkASESMEN_80.Checked
                .ASESMEN_81 = chkASESMEN_81.Checked
                .ASESMEN_82 = chkASESMEN_82.Checked
                .ASESMEN_83 = chkASESMEN_83.Checked
                .ASESMEN_84 = txtASESMEN_84.Text
                .ASESMEN_85 = chkASESMEN_85.Checked
                .ASESMEN_86 = chkASESMEN_86.Checked
                .ASESMEN_87 = chkASESMEN_87.Checked
                .ASESMEN_88 = chkASESMEN_88.Checked
                .ASESMEN_89 = chkASESMEN_89.Checked
                .ASESMEN_90 = chkASESMEN_90.Checked
                .ASESMEN_91 = chkASESMEN_91.Checked
                .ASESMEN_92 = chkASESMEN_92.Checked
                .ASESMEN_93 = chkASESMEN_93.Checked
                .ASESMEN_94 = chkASESMEN_94.Checked
                .ASESMEN_95 = chkASESMEN_95.Checked
                .ASESMEN_96 = chkASESMEN_96.Checked
                .ASESMEN_97 = chkASESMEN_97.Checked
                .ASESMEN_98 = chkASESMEN_98.Checked
                .ASESMEN_99 = chkASESMEN_99.Checked
                .ASESMEN_100 = chkASESMEN_100.Checked
                .ASESMEN_101 = chkASESMEN_101.Checked
                .ASESMEN_102 = chkASESMEN_102.Checked
                .ASESMEN_103 = chkASESMEN_103.Checked
                .ASESMEN_104 = chkASESMEN_104.Checked
                .ASESMEN_105 = chkASESMEN_105.Checked
                .ASESMEN_106 = txtASESMEN_106.Text
                .ASESMEN_107 = txtASESMEN_107.Text
                .ASESMEN_108 = txtASESMEN_108.Text
                .ASESMEN_109 = txtASESMEN_109.Text
                .ASESMEN_110 = chkASESMEN_110.Checked
                .ASESMEN_111 = chkASESMEN_111.Checked
                .ASESMEN_112 = chkASESMEN_112.Checked
                .ASESMEN_113 = chkASESMEN_113.Checked
                .ASESMEN_114 = chkASESMEN_114.Checked
                .ASESMEN_115 = chkASESMEN_115.Checked
                .ASESMEN_116 = chkASESMEN_116.Checked
                .ASESMEN_117 = chkASESMEN_117.Checked
                .ASESMEN_118 = chkASESMEN_118.Checked
                .ASESMEN_119 = chkASESMEN_119.Checked
                .ASESMEN_120 = chkASESMEN_120.Checked
                .ASESMEN_121 = chkASESMEN_121.Checked
                .ASESMEN_122 = chkASESMEN_122.Checked
                .ASESMEN_123 = chkASESMEN_123.Checked
                .ASESMEN_124 = chkASESMEN_124.Checked
                .ASESMEN_125 = chkASESMEN_125.Checked
                .ASESMEN_126 = chkASESMEN_126.Checked
                .ASESMEN_127 = chkASESMEN_127.Checked
                .ASESMEN_128 = chkASESMEN_128.Checked
                .ASESMEN_129 = chkASESMEN_129.Checked
                .ASESMEN_130 = chkASESMEN_130.Checked
                .ASESMEN_131 = chkASESMEN_131.Checked
                .ASESMEN_132 = chkASESMEN_132.Checked
                .ASESMEN_133 = chkASESMEN_133.Checked
                .ASESMEN_134 = chkASESMEN_134.Checked
                .ASESMEN_135 = chkASESMEN_135.Checked
                .ASESMEN_136 = chkASESMEN_136.Checked
                .ASESMEN_137 = chkASESMEN_137.Checked
                .ASESMEN_138 = chkASESMEN_138.Checked
                .ASESMEN_139 = chkASESMEN_139.Checked
                .ASESMEN_140 = chkASESMEN_140.Checked
                .ASESMEN_141 = chkASESMEN_141.Checked
                .ASESMEN_142 = chkASESMEN_142.Checked
                .ASESMEN_143 = chkASESMEN_143.Checked
                .ASESMEN_144 = chkASESMEN_144.Checked
                .ASESMEN_145 = chkASESMEN_145.Checked
                .ASESMEN_146 = chkASESMEN_146.Checked
                .ASESMEN_147 = chkASESMEN_147.Checked
                .ASESMEN_148 = chkASESMEN_148.Checked
                .ASESMEN_149 = chkASESMEN_149.Checked
                .ASESMEN_150 = chkASESMEN_150.Checked
                .ASESMEN_151 = chkASESMEN_151.Checked
                .ASESMEN_152 = chkASESMEN_152.Checked
                .ASESMEN_153 = chkASESMEN_153.Checked
                .ASESMEN_154 = chkASESMEN_154.Checked
                .ASESMEN_155 = chkASESMEN_155.Checked
                .ASESMEN_156 = chkASESMEN_156.Checked
                .ASESMEN_157 = chkASESMEN_157.Checked
                .ASESMEN_158 = chkASESMEN_158.Checked
                .ASESMEN_159 = chkASESMEN_159.Checked
                .ASESMEN_160 = chkASESMEN_160.Checked
                .ASESMEN_161 = chkASESMEN_161.Checked
                .ASESMEN_162 = chkASESMEN_162.Checked
                .ASESMEN_163 = chkASESMEN_163.Checked
                .ASESMEN_164 = chkASESMEN_164.Checked
                .ASESMEN_165 = chkASESMEN_165.Checked
                .ASESMEN_166 = chkASESMEN_166.Checked
                .ASESMEN_167 = chkASESMEN_167.Checked
                .ASESMEN_168 = chkASESMEN_168.Checked
                .ASESMEN_169 = chkASESMEN_169.Checked
                .ASESMEN_170 = txtASESMEN_170.Text
                .TGL_DITEMUKAN_01 = txtTGL_DITEMUKAN_01.Text
                .TGL_DITEMUKAN_02 = txtTGL_DITEMUKAN_02.Text
                .TGL_DITEMUKAN_03 = txtTGL_DITEMUKAN_03.Text
                .TGL_DITEMUKAN_04 = txtTGL_DITEMUKAN_04.Text
                .TGL_TERATASI_01 = txtTGL_TERATASI_01.Text
                .TGL_TERATASI_02 = txtTGL_TERATASI_02.Text
                .TGL_TERATASI_03 = txtTGL_TERATASI_03.Text
                .TGL_TERATASI_04 = txtTGL_TERATASI_04.Text
                .DP_YA_01 = chkDP_YA_01.Checked
                .DP_YA_02 = chkDP_YA_02.Checked
                .DP_YA_03 = chkDP_YA_03.Checked
                .DP_YA_04 = chkDP_YA_04.Checked
                .DP_YA_05 = chkDP_YA_05.Checked
                .DP_YA_06 = chkDP_YA_06.Checked
                .DP_YA_07 = chkDP_YA_07.Checked
                .DP_YA_08 = chkDP_YA_08.Checked
                .DP_TIDAK_01 = chkDP_TIDAK_01.Checked
                .DP_TIDAK_02 = chkDP_TIDAK_02.Checked
                .DP_TIDAK_03 = chkDP_TIDAK_03.Checked
                .DP_TIDAK_04 = chkDP_TIDAK_04.Checked
                .DP_TIDAK_05 = chkDP_TIDAK_05.Checked
                .DP_TIDAK_06 = chkDP_TIDAK_06.Checked
                .DP_TIDAK_07 = chkDP_TIDAK_07.Checked
                .DP_TIDAK_08 = chkDP_TIDAK_08.Checked
                .DP_KET_01 = txtDP_KET_01.Text
                .DP_KET_02 = txtDP_KET_02.Text
                .DP_KET_03 = txtDP_KET_03.Text
                .DP_KET_04 = txtDP_KET_04.Text
                .DP_KET_05 = txtDP_KET_05.Text
                .DP_KET_06 = txtDP_KET_06.Text
                .DP_KET_07 = txtDP_KET_07.Text
                .DP_KET_08 = txtDP_KET_08.Text

                .DOKTER_KODE = sKODEDOKTER
                .DOKTER_NAMEDISPLAY = sNAMADOKTER

                Try
                    .CETAK = oS_DIGITAL_RI_24.GetData(txtNoRegister.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                Try
                    If sIsOtority = True Then
                        .KDUSER = oS_DIGITAL_RI_24.GetData(txtNoRegister.Text).KDUSER
                        .KDUSER_SIGNATURE = oS_DIGITAL_RI_24.GetData(txtNoRegister.Text).KDUSER_SIGNATURE
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
                    fn_Save = oS_DIGITAL_RI_24.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_RI_24.UpdateData(ds)
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
    Private Sub fn_Doctor()
        'Dim oDoctor As New Master.clsDoctor2
        'Try
        '    grdKDDOCTOR2.Properties.DataSource = oDoctor.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
        '    grdKDDOCTOR2.Properties.ValueMember = "KDDOCTOR2"
        '    grdKDDOCTOR2.Properties.DisplayMember = "NAME_DISPLAY"

        'Catch oErr As Exception
        '    MsgBox("Load Doctor Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub

    Private Sub frmEMedrekRI_24_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
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