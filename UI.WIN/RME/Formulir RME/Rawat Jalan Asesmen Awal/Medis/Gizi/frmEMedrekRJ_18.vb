Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmEMedrekRJ_18
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_RJ_18 As New Digital.clsDigital_RJ_18
    Private down As Boolean = False
    Private sIsOtority As Boolean = False

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String)
        oFormMode = FormMode

        Dim oPendaftaran As New Identitas.clsIdentitasPasien
        Dim dsPendaftaran = oPendaftaran.GetData(KDREG)

        GroupControl1.Text = Me.Text

        If dsPendaftaran IsNot Nothing Then
            txtNOREG.Text = KDREG
            txtNoPasien.Text = dsPendaftaran.KDCUSTOMER
            txtNamaPasien.Text = dsPendaftaran.NAMAPASIEN
            txtRuang.Text = dsPendaftaran.TUJUAN
            txtWaktuAsesmen.Text = dsPendaftaran.DATE.ToString("dd-MM-yyyy HH:mm:ss")
            txtAlamat.Text = dsPendaftaran.ALAMAT
            txtKelas.Text = dsPendaftaran.KELASPELAYANAN
            txtTmptLahir.Text = dsPendaftaran.TEMPATLAHIR + ", " + dsPendaftaran.TANGGALLAHIR
            txtAgama.Text = dsPendaftaran.AGAMA
            txtUmum.Text = "" 'dsPendaftaran.MASTER_PASIEN.tlppasien
            txtPekerjaan.Text = dsPendaftaran.HUBUNGAN_PEKERJAAN
            txtSukuBangsa.Text = dsPendaftaran.SUKU
            txtGolDar.Text = dsPendaftaran.KDGOLONGANDARAH
            txtTgl.Text = Now

            If (dsPendaftaran.JENISKELAMIN = "L") Then
                txtJenisKelamin.Text = "Laki-laki"
            Else
                txtJenisKelamin.Text = "Perempuan"
            End If
        Else
            txtNOREG.ResetText()
                txtNoPasien.ResetText()
                txtNamaPasien.ResetText()
                txtRuang.ResetText()
                txtWaktuAsesmen.ResetText()
                txtAlamat.ResetText()
                txtKelas.ResetText()
                txtTmptLahir.ResetText()
                txtAgama.ResetText()
                txtUmum.ResetText()
                txtPekerjaan.ResetText()
                txtSukuBangsa.ResetText()
                txtGolDar.ResetText()
                txtTgl.ResetText()
                txtJenisKelamin.ResetText()
            End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = EMedrekRJ_18.TITLE
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
        txtDIGITALRJ_1.Properties.ReadOnly = Status
        txtDIGITALRJ_2.Properties.ReadOnly = Status
        txtDIGITALRJ_3.Properties.ReadOnly = Status
        txtDIGITALRJ_4.Properties.ReadOnly = Status
        txtDIGITALRJ_5.Properties.ReadOnly = Status
        txtDIGITALRJ_8.Properties.ReadOnly = Status
        chkDIGITALRJ_6.Properties.ReadOnly = Status
        chkDIGITALRJ_7.Properties.ReadOnly = Status
        txtDIGITALRJ_10.Properties.ReadOnly = Status
        txtDIGITALRJ_11.Properties.ReadOnly = Status
        txtDIGITALRJ_12.Properties.ReadOnly = Status
        txtDIGITALRJ_13.Properties.ReadOnly = Status
        txtDIGITALRJ_14.Properties.ReadOnly = Status
        txtDIGITALRJ_15.Properties.ReadOnly = Status
        txtDIGITALRJ_16.Properties.ReadOnly = Status
        txtDIGITALRJ_17.Properties.ReadOnly = Status
        txtDIGITALRJ_18.Properties.ReadOnly = Status
        txtDIGITALRJ_19.Properties.ReadOnly = Status
        txtDIGITALRJ_20.Properties.ReadOnly = Status
        txtDIGITALRJ_21.Properties.ReadOnly = Status
        chkDIGITALRJ_22.Properties.ReadOnly = Status
        chkDIGITALRJ_23.Properties.ReadOnly = Status
        chkDIGITALRJ_24.Properties.ReadOnly = Status
        chkDIGITALRJ_25.Properties.ReadOnly = Status
        chkDIGITALRJ_26.Properties.ReadOnly = Status
        chkDIGITALRJ_27.Properties.ReadOnly = Status
        txtDIGITALRJ_28.Properties.ReadOnly = Status
        chkDIGITALRJ_29.Properties.ReadOnly = Status
        chkDIGITALRJ_30.Properties.ReadOnly = Status
        txtDIGITALRJ_31.Properties.ReadOnly = Status
        chkDIGITALRJ_32.Properties.ReadOnly = Status
        txtDIGITALRJ_33.Properties.ReadOnly = Status
        txtDIGITALRJ_34.Properties.ReadOnly = Status
        chkDIGITALRJ_35.Properties.ReadOnly = Status
        chkDIGITALRJ_36.Properties.ReadOnly = Status
        chkDIGITALRJ_37.Properties.ReadOnly = Status
        txtDIGITALRJ_38.Properties.ReadOnly = Status
        chkDIGITALRJ_39.Properties.ReadOnly = Status
        chkDIGITALRJ_40.Properties.ReadOnly = Status
        txtDIGITALRJ_41.Properties.ReadOnly = Status
        txtDIGITALRJ_42.Properties.ReadOnly = Status
        txtDIGITALRJ_43.Properties.ReadOnly = Status
        txtDIGITALRJ_44.Properties.ReadOnly = Status
        txtDIGITALRJ_45.Properties.ReadOnly = Status
        chkDIGITALRJ_46.Properties.ReadOnly = Status
        chkDIGITALRJ_47.Properties.ReadOnly = Status
        chkDIGITALRJ_48.Properties.ReadOnly = Status
        chkDIGITALRJ_49.Properties.ReadOnly = Status
        chkDIGITALRJ_50.Properties.ReadOnly = Status
        chkDIGITALRJ_51.Properties.ReadOnly = Status
        chkDIGITALRJ_52.Properties.ReadOnly = Status
        chkDIGITALRJ_53.Properties.ReadOnly = Status
        chkDIGITALRJ_54.Properties.ReadOnly = Status
        chkDIGITALRJ_55.Properties.ReadOnly = Status
        chkDIGITALRJ_56.Properties.ReadOnly = Status
        chkDIGITALRJ_57.Properties.ReadOnly = Status
        chkDIGITALRJ_58.Properties.ReadOnly = Status
        chkDIGITALRJ_59.Properties.ReadOnly = Status
        chkDIGITALRJ_60.Properties.ReadOnly = Status
        txtDIGITALRJ_61.Properties.ReadOnly = Status
        chkDIGITALRJ_62.Properties.ReadOnly = Status
        txtDIGITALRJ_63.Properties.ReadOnly = Status
        chkDIGITALRJ_64.Properties.ReadOnly = Status
        chkDIGITALRJ_65.Properties.ReadOnly = Status
        txtDIGITALRJ_66.Properties.ReadOnly = Status
        chkDIGITALRJ_67.Properties.ReadOnly = Status
        txtDIGITALRJ_68.Properties.ReadOnly = Status
        chkDIGITALRJ_69.Properties.ReadOnly = Status
        chkDIGITALRJ_70.Properties.ReadOnly = Status
        chkDIGITALRJ_71.Properties.ReadOnly = Status
        chkDIGITALRJ_72.Properties.ReadOnly = Status
        chkDIGITALRJ_73.Properties.ReadOnly = Status
        chkDIGITALRJ_74.Properties.ReadOnly = Status
        chkDIGITALRJ_75.Properties.ReadOnly = Status
        chkDIGITALRJ_76.Properties.ReadOnly = Status
        chkDIGITALRJ_77.Properties.ReadOnly = Status
        chkDIGITALRJ_78.Properties.ReadOnly = Status
        chkDIGITALRJ_80.Properties.ReadOnly = Status
        chkDIGITALRJ_84.Properties.ReadOnly = Status
        txtDIGITALRJ_81.Properties.ReadOnly = Status
        txtDIGITALRJ_85.Properties.ReadOnly = Status
        txtDIGITALRJ_82.Properties.ReadOnly = Status
        txtDIGITALRJ_86.Properties.ReadOnly = Status
        txtDIGITALRJ_83.Properties.ReadOnly = Status
        txtDIGITALRJ_87.Properties.ReadOnly = Status
        chkDIGITALRJ_88.Properties.ReadOnly = Status
        chkDIGITALRJ_89.Properties.ReadOnly = Status
        chkDIGITALRJ_90.Properties.ReadOnly = Status
        chkDIGITALRJ_91.Properties.ReadOnly = Status
        chkDIGITALRJ_92.Properties.ReadOnly = Status
        txtDIGITALRJ_93.Properties.ReadOnly = Status
        txtDIGITALRJ_94.Properties.ReadOnly = Status
        txtDIGITALRJ_95.Properties.ReadOnly = Status
        txtDIGITALRJ_96.Properties.ReadOnly = Status
        txtDIGITALRJ_97.Properties.ReadOnly = Status
        txtDIGITALRJ_98.Properties.ReadOnly = Status
        txtDIGITALRJ_99.Properties.ReadOnly = Status
        txtDIGITALRJ_100.Properties.ReadOnly = Status
        txtDIGITALRJ_101.Properties.ReadOnly = Status
        txtDIGITALRJ_102.Properties.ReadOnly = Status
        txtDIGITALRJ_103.Properties.ReadOnly = Status
        txtDIGITALRJ_104.Properties.ReadOnly = Status
        txtDIGITALRJ_105.Properties.ReadOnly = Status
        txtDIGITALRJ_106.Properties.ReadOnly = Status
        txtDIGITALRJ_107.Properties.ReadOnly = Status
        txtDIGITALRJ_108.Properties.ReadOnly = Status
        txtDIGITALRJ_109.Properties.ReadOnly = Status
        txtDIGITALRJ_110.Properties.ReadOnly = Status
        txtDIGITALRJ_111.Properties.ReadOnly = Status
        txtDIGITALRJ_112.Properties.ReadOnly = Status
        txtDIGITALRJ_113.Properties.ReadOnly = Status
        txtDIGITALRJ_114.Properties.ReadOnly = Status
        txtDIGITALRJ_115.Properties.ReadOnly = Status
        txtDIGITALRJ_116.Properties.ReadOnly = Status
        txtDIGITALRJ_117.Properties.ReadOnly = Status
        txtDIGITALRJ_118.Properties.ReadOnly = Status
        txtDIGITALRJ_119.Properties.ReadOnly = Status
        txtDIGITALRJ_120.Properties.ReadOnly = Status
        txtDIGITALRJ_121.Properties.ReadOnly = Status
        txtDIGITALRJ_122.Properties.ReadOnly = Status
        txtDIGITALRJ_123.Properties.ReadOnly = Status
        chkDIGITALRJ_124.Properties.ReadOnly = Status
        chkDIGITALRJ_125.Properties.ReadOnly = Status
        chkDIGITALRJ_126.Properties.ReadOnly = Status
        chkDIGITALRJ_127.Properties.ReadOnly = Status
        chkDIGITALRJ_128.Properties.ReadOnly = Status
        txtDIGITALRJ_129.Properties.ReadOnly = Status
        txtDIGITALRJ_130.Properties.ReadOnly = Status
        txtDIGITALRJ_131.Properties.ReadOnly = Status
        txtDIGITALRJ_132.Properties.ReadOnly = Status
        txtDIGITALRJ_133.Properties.ReadOnly = Status
        txtDIGITALRJ_134.Properties.ReadOnly = Status
        txtDIGITALRJ_135.Properties.ReadOnly = Status
        txtDIGITALRJ_136.Properties.ReadOnly = Status
        txtDIGITALRJ_137.Properties.ReadOnly = Status
        txtDIGITALRJ_138.Properties.ReadOnly = Status
        chkDIGITALRJ_79.Properties.ReadOnly = Status

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
        'txtCODE.Text = "<--- AUTO --->"

        txtDIGITALRJ_1.ResetText()
        txtDIGITALRJ_2.ResetText()
        txtDIGITALRJ_3.ResetText()
        txtDIGITALRJ_4.ResetText()
        txtDIGITALRJ_5.ResetText()
        txtDIGITALRJ_8.ResetText()
        chkDIGITALRJ_6.Checked = False
        chkDIGITALRJ_7.Checked = False
        txtDIGITALRJ_10.ResetText()
        txtDIGITALRJ_11.ResetText()
        txtDIGITALRJ_12.ResetText()
        txtDIGITALRJ_13.ResetText()
        txtDIGITALRJ_14.ResetText()
        txtDIGITALRJ_15.ResetText()
        txtDIGITALRJ_16.ResetText()
        txtDIGITALRJ_17.ResetText()
        txtDIGITALRJ_18.ResetText()
        txtDIGITALRJ_19.ResetText()
        txtDIGITALRJ_20.ResetText()
        txtDIGITALRJ_21.ResetText()
        chkDIGITALRJ_22.Checked = False
        chkDIGITALRJ_23.Checked = False
        chkDIGITALRJ_24.Checked = False
        chkDIGITALRJ_25.Checked = False
        chkDIGITALRJ_26.Checked = False
        chkDIGITALRJ_27.Checked = False
        txtDIGITALRJ_28.ResetText()
        chkDIGITALRJ_29.Checked = False
        chkDIGITALRJ_30.Checked = False
        txtDIGITALRJ_31.ResetText()
        chkDIGITALRJ_32.Checked = False
        txtDIGITALRJ_33.ResetText()
        txtDIGITALRJ_34.ResetText()
        chkDIGITALRJ_35.Checked = False
        chkDIGITALRJ_36.Checked = False
        chkDIGITALRJ_37.Checked = False
        txtDIGITALRJ_38.ResetText()
        chkDIGITALRJ_39.Checked = False
        chkDIGITALRJ_40.Checked = False
        txtDIGITALRJ_41.ResetText()
        txtDIGITALRJ_42.ResetText()
        txtDIGITALRJ_43.ResetText()
        txtDIGITALRJ_44.ResetText()
        txtDIGITALRJ_45.ResetText()
        chkDIGITALRJ_46.Checked = False
        chkDIGITALRJ_47.Checked = False
        chkDIGITALRJ_48.Checked = False
        chkDIGITALRJ_49.Checked = False
        chkDIGITALRJ_50.Checked = False
        chkDIGITALRJ_51.Checked = False
        chkDIGITALRJ_52.Checked = False
        chkDIGITALRJ_53.Checked = False
        chkDIGITALRJ_54.Checked = False
        chkDIGITALRJ_55.Checked = False
        chkDIGITALRJ_56.Checked = False
        chkDIGITALRJ_57.Checked = False
        chkDIGITALRJ_58.Checked = False
        chkDIGITALRJ_59.Checked = False
        chkDIGITALRJ_60.Checked = False
        txtDIGITALRJ_61.ResetText()
        chkDIGITALRJ_62.Checked = False
        txtDIGITALRJ_63.ResetText()
        chkDIGITALRJ_64.Checked = False
        chkDIGITALRJ_65.Checked = False
        txtDIGITALRJ_66.ResetText()
        chkDIGITALRJ_67.Checked = False
        txtDIGITALRJ_68.ResetText()
        chkDIGITALRJ_69.Checked = False
        chkDIGITALRJ_70.Checked = False
        chkDIGITALRJ_71.Checked = False
        chkDIGITALRJ_72.Checked = False
        chkDIGITALRJ_73.Checked = False
        chkDIGITALRJ_74.Checked = False
        chkDIGITALRJ_75.Checked = False
        chkDIGITALRJ_76.Checked = False
        chkDIGITALRJ_77.Checked = False
        chkDIGITALRJ_78.Checked = False
        chkDIGITALRJ_80.Checked = False
        chkDIGITALRJ_84.Checked = False
        txtDIGITALRJ_81.ResetText()
        txtDIGITALRJ_85.ResetText()
        txtDIGITALRJ_82.ResetText()
        txtDIGITALRJ_86.ResetText()
        txtDIGITALRJ_83.ResetText()
        txtDIGITALRJ_87.ResetText()
        chkDIGITALRJ_88.Checked = False
        chkDIGITALRJ_89.Checked = False
        chkDIGITALRJ_90.Checked = False
        chkDIGITALRJ_91.Checked = False
        chkDIGITALRJ_92.Checked = False
        txtDIGITALRJ_93.ResetText()
        txtDIGITALRJ_94.ResetText()
        txtDIGITALRJ_95.ResetText()
        txtDIGITALRJ_96.ResetText()
        txtDIGITALRJ_97.ResetText()
        txtDIGITALRJ_98.ResetText()
        txtDIGITALRJ_99.ResetText()
        txtDIGITALRJ_100.ResetText()
        txtDIGITALRJ_101.ResetText()
        txtDIGITALRJ_102.ResetText()
        txtDIGITALRJ_103.ResetText()
        txtDIGITALRJ_104.ResetText()
        txtDIGITALRJ_105.ResetText()
        txtDIGITALRJ_106.ResetText()
        txtDIGITALRJ_107.ResetText()
        txtDIGITALRJ_108.ResetText()
        txtDIGITALRJ_109.ResetText()
        txtDIGITALRJ_110.ResetText()
        txtDIGITALRJ_111.ResetText()
        txtDIGITALRJ_112.ResetText()
        txtDIGITALRJ_113.ResetText()
        txtDIGITALRJ_114.ResetText()
        txtDIGITALRJ_115.ResetText()
        txtDIGITALRJ_116.ResetText()
        txtDIGITALRJ_117.ResetText()
        txtDIGITALRJ_118.ResetText()
        txtDIGITALRJ_119.ResetText()
        txtDIGITALRJ_120.ResetText()
        txtDIGITALRJ_121.ResetText()
        txtDIGITALRJ_122.ResetText()
        txtDIGITALRJ_123.ResetText()
        chkDIGITALRJ_124.Checked = False
        chkDIGITALRJ_125.Checked = False
        chkDIGITALRJ_126.Checked = False
        chkDIGITALRJ_127.Checked = False
        chkDIGITALRJ_128.Checked = False
        txtDIGITALRJ_129.ResetText()
        txtDIGITALRJ_130.ResetText()
        txtDIGITALRJ_131.ResetText()
        txtDIGITALRJ_132.ResetText()
        txtDIGITALRJ_133.ResetText()
        txtDIGITALRJ_134.ResetText()
        txtDIGITALRJ_135.ResetText()
        txtDIGITALRJ_136.ResetText()
        txtDIGITALRJ_137.ResetText()
        txtDIGITALRJ_138.ResetText()

        deDATE.DateTime = Now

        fn_LoadAsessmenRawatJalan(txtNOREG.Text)
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RJ_18.GetData(txtNOREG.Text)

            With ds

                txtDIGITALRJ_1.Text = .SDIGITALRJ18_1
                txtDIGITALRJ_2.Text = .SDIGITALRJ18_2
                txtDIGITALRJ_3.Text = .SDIGITALRJ18_3
                txtDIGITALRJ_4.Text = .SDIGITALRJ18_4
                txtDIGITALRJ_5.Text = .SDIGITALRJ18_5
                chkDIGITALRJ_6.Checked = .SDIGITALRJ18_6
                chkDIGITALRJ_7.Checked = .SDIGITALRJ18_7
                txtDIGITALRJ_8.Text = .SDIGITALRJ18_8
                txtDIGITALRJ_9.Text = .SDIGITALRJ18_9
                txtDIGITALRJ_10.Text = .SDIGITALRJ18_10
                txtDIGITALRJ_11.Text = .SDIGITALRJ18_11
                txtDIGITALRJ_12.Text = .SDIGITALRJ18_12
                txtDIGITALRJ_13.Text = .SDIGITALRJ18_13
                txtDIGITALRJ_14.Text = .SDIGITALRJ18_14
                txtDIGITALRJ_15.Text = .SDIGITALRJ18_15
                txtDIGITALRJ_16.Text = .SDIGITALRJ18_16
                txtDIGITALRJ_17.Text = .SDIGITALRJ18_17
                txtDIGITALRJ_18.Text = .SDIGITALRJ18_18
                txtDIGITALRJ_19.Text = .SDIGITALRJ18_19
                txtDIGITALRJ_20.Text = .SDIGITALRJ18_20
                txtDIGITALRJ_21.Text = .SDIGITALRJ18_21
                chkDIGITALRJ_22.Checked = .SDIGITALRJ18_22
                chkDIGITALRJ_23.Checked = .SDIGITALRJ18_23
                chkDIGITALRJ_24.Checked = .SDIGITALRJ18_24
                chkDIGITALRJ_25.Checked = .SDIGITALRJ18_25
                chkDIGITALRJ_26.Checked = .SDIGITALRJ18_26
                chkDIGITALRJ_27.Checked = .SDIGITALRJ18_27
                txtDIGITALRJ_28.Text = .SDIGITALRJ18_28
                chkDIGITALRJ_29.Checked = .SDIGITALRJ18_29
                chkDIGITALRJ_30.Checked = .SDIGITALRJ18_30
                txtDIGITALRJ_31.Text = .SDIGITALRJ18_31
                chkDIGITALRJ_32.Checked = .SDIGITALRJ18_32
                txtDIGITALRJ_33.Text = .SDIGITALRJ18_33
                txtDIGITALRJ_34.Text = .SDIGITALRJ18_34
                chkDIGITALRJ_35.Checked = .SDIGITALRJ18_35
                chkDIGITALRJ_36.Checked = .SDIGITALRJ18_36
                chkDIGITALRJ_37.Checked = .SDIGITALRJ18_37
                txtDIGITALRJ_38.Text = .SDIGITALRJ18_38
                chkDIGITALRJ_39.Checked = .SDIGITALRJ18_39
                chkDIGITALRJ_40.Checked = .SDIGITALRJ18_40
                txtDIGITALRJ_41.Text = .SDIGITALRJ18_41
                txtDIGITALRJ_42.Text = .SDIGITALRJ18_42
                txtDIGITALRJ_43.Text = .SDIGITALRJ18_43
                txtDIGITALRJ_44.Text = .SDIGITALRJ18_44
                txtDIGITALRJ_45.Text = .SDIGITALRJ18_45
                chkDIGITALRJ_46.Checked = .SDIGITALRJ18_46
                chkDIGITALRJ_47.Checked = .SDIGITALRJ18_47
                chkDIGITALRJ_48.Checked = .SDIGITALRJ18_48
                chkDIGITALRJ_49.Checked = .SDIGITALRJ18_49
                chkDIGITALRJ_50.Checked = .SDIGITALRJ18_50
                chkDIGITALRJ_51.Checked = .SDIGITALRJ18_51
                chkDIGITALRJ_52.Checked = .SDIGITALRJ18_52
                chkDIGITALRJ_53.Checked = .SDIGITALRJ18_53
                chkDIGITALRJ_54.Checked = .SDIGITALRJ18_54
                chkDIGITALRJ_55.Checked = .SDIGITALRJ18_55
                chkDIGITALRJ_56.Checked = .SDIGITALRJ18_56
                chkDIGITALRJ_57.Checked = .SDIGITALRJ18_57
                chkDIGITALRJ_58.Checked = .SDIGITALRJ18_58
                chkDIGITALRJ_59.Checked = .SDIGITALRJ18_59
                chkDIGITALRJ_60.Checked = .SDIGITALRJ18_60
                txtDIGITALRJ_61.Text = .SDIGITALRJ18_61
                chkDIGITALRJ_62.Checked = .SDIGITALRJ18_62
                txtDIGITALRJ_63.Text = .SDIGITALRJ18_63
                chkDIGITALRJ_64.Checked = .SDIGITALRJ18_64
                chkDIGITALRJ_65.Checked = .SDIGITALRJ18_65
                txtDIGITALRJ_66.Text = .SDIGITALRJ18_66
                chkDIGITALRJ_67.Checked = .SDIGITALRJ18_67
                txtDIGITALRJ_68.Text = .SDIGITALRJ18_68
                chkDIGITALRJ_69.Checked = .SDIGITALRJ18_69
                chkDIGITALRJ_70.Checked = .SDIGITALRJ18_70
                chkDIGITALRJ_71.Checked = .SDIGITALRJ18_71
                chkDIGITALRJ_72.Checked = .SDIGITALRJ18_72
                chkDIGITALRJ_73.Checked = .SDIGITALRJ18_73
                chkDIGITALRJ_74.Checked = .SDIGITALRJ18_74
                chkDIGITALRJ_75.Checked = .SDIGITALRJ18_75
                chkDIGITALRJ_76.Checked = .SDIGITALRJ18_76
                chkDIGITALRJ_77.Checked = .SDIGITALRJ18_77
                chkDIGITALRJ_78.Checked = .SDIGITALRJ18_78
                chkDIGITALRJ_79.Checked = .SDIGITALRJ18_79
                chkDIGITALRJ_80.Checked = .SDIGITALRJ18_80
                txtDIGITALRJ_81.Text = .SDIGITALRJ18_81
                txtDIGITALRJ_82.Text = .SDIGITALRJ18_82
                txtDIGITALRJ_83.Text = .SDIGITALRJ18_83
                chkDIGITALRJ_84.Checked = .SDIGITALRJ18_84
                txtDIGITALRJ_85.Text = .SDIGITALRJ18_85
                txtDIGITALRJ_86.Text = .SDIGITALRJ18_86
                txtDIGITALRJ_87.Text = .SDIGITALRJ18_87
                chkDIGITALRJ_88.Checked = .SDIGITALRJ18_88
                chkDIGITALRJ_89.Checked = .SDIGITALRJ18_89
                chkDIGITALRJ_90.Checked = .SDIGITALRJ18_90
                chkDIGITALRJ_91.Checked = .SDIGITALRJ18_91
                chkDIGITALRJ_92.Checked = .SDIGITALRJ18_92
                txtDIGITALRJ_93.Text = .SDIGITALRJ18_93
                txtDIGITALRJ_94.Text = .SDIGITALRJ18_94
                txtDIGITALRJ_95.Text = .SDIGITALRJ18_95
                txtDIGITALRJ_96.Text = .SDIGITALRJ18_96
                txtDIGITALRJ_97.Text = .SDIGITALRJ18_97
                txtDIGITALRJ_98.Text = .SDIGITALRJ18_98
                txtDIGITALRJ_99.Text = .SDIGITALRJ18_99
                txtDIGITALRJ_100.Text = .SDIGITALRJ18_100
                txtDIGITALRJ_101.Text = .SDIGITALRJ18_101
                txtDIGITALRJ_102.Text = .SDIGITALRJ18_102
                txtDIGITALRJ_103.Text = .SDIGITALRJ18_103
                txtDIGITALRJ_104.Text = .SDIGITALRJ18_104
                txtDIGITALRJ_105.Text = .SDIGITALRJ18_105
                txtDIGITALRJ_106.Text = .SDIGITALRJ18_106
                txtDIGITALRJ_107.Text = .SDIGITALRJ18_107
                txtDIGITALRJ_108.Text = .SDIGITALRJ18_108
                txtDIGITALRJ_109.Text = .SDIGITALRJ18_109
                txtDIGITALRJ_110.Text = .SDIGITALRJ18_110
                txtDIGITALRJ_111.Text = .SDIGITALRJ18_111
                txtDIGITALRJ_112.Text = .SDIGITALRJ18_112
                txtDIGITALRJ_113.Text = .SDIGITALRJ18_113
                txtDIGITALRJ_114.Text = .SDIGITALRJ18_114
                txtDIGITALRJ_115.Text = .SDIGITALRJ18_115
                txtDIGITALRJ_116.Text = .SDIGITALRJ18_116
                txtDIGITALRJ_117.Text = .SDIGITALRJ18_117
                txtDIGITALRJ_118.Text = .SDIGITALRJ18_118
                txtDIGITALRJ_119.Text = .SDIGITALRJ18_119
                txtDIGITALRJ_120.Text = .SDIGITALRJ18_120
                txtDIGITALRJ_121.Text = .SDIGITALRJ18_121
                txtDIGITALRJ_122.Text = .SDIGITALRJ18_122
                txtDIGITALRJ_123.Text = .SDIGITALRJ18_123
                chkDIGITALRJ_124.Checked = .SDIGITALRJ18_124
                chkDIGITALRJ_125.Checked = .SDIGITALRJ18_125
                chkDIGITALRJ_126.Checked = .SDIGITALRJ18_126
                chkDIGITALRJ_127.Checked = .SDIGITALRJ18_127
                chkDIGITALRJ_128.Checked = .SDIGITALRJ18_128
                txtDIGITALRJ_129.Text = .SDIGITALRJ18_129
                txtDIGITALRJ_130.Text = .SDIGITALRJ18_130
                txtDIGITALRJ_131.Text = .SDIGITALRJ18_131
                txtDIGITALRJ_132.Text = .SDIGITALRJ18_132
                txtDIGITALRJ_133.Text = .SDIGITALRJ18_133
                txtDIGITALRJ_134.Text = .SDIGITALRJ18_134
                txtDIGITALRJ_135.Text = .SDIGITALRJ18_135
                txtDIGITALRJ_136.Text = .SDIGITALRJ18_136
                txtDIGITALRJ_137.Text = .SDIGITALRJ18_137
                txtDIGITALRJ_138.Text = .SDIGITALRJ18_138

                deDATE.DateTime = .DATE
            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub fn_LoadAsessmenRawatJalan(ByVal Parameter As String)
        Dim oAsessmenRawatJalan As New Digital.clsDigital_RJ_06
        Dim dsAsessmenRawatJalan = oAsessmenRawatJalan.GetData(Parameter)

        If dsAsessmenRawatJalan IsNot Nothing Then
            'bb
            txtDIGITALRJ_15.Text = dsAsessmenRawatJalan.ANTROPOMETRI_BB
            'tb
            txtDIGITALRJ_16.Text = dsAsessmenRawatJalan.ANTROPOMETRI_TB
            'gizi
            txtDIGITALRJ_17.Text = dsAsessmenRawatJalan.STATUS_GIZI
            'nadi
            txtDIGITALRJ_18.text = dsAsessmenRawatJalan.PEMERIKSAAN_FISIK_KLINIS_NADI
            'tensi
            txtDIGITALRJ_19.Text = dsAsessmenRawatJalan.PEMERIKSAAN_FISIK_KLINIS_TENSI
            'suhu
            txtDIGITALRJ_20.Text = dsAsessmenRawatJalan.PEMERIKSAAN_FISIK_KLINIS_SUHU
            'respirasi
            txtDIGITALRJ_21.Text = dsAsessmenRawatJalan.PEMERIKSAAN_FISIK_KLINIS_RESP
        Else
            MsgBox("Assemen Awal Keperawatan belum di input", MsgBoxStyle.Exclamation, Me.Text)
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
            Dim oPendaftaran As New Identitas.clsIdentitasPasien
            Dim dsPendaftaran = oPendaftaran.GetData(txtNOREG.Text)
            Dim ds = oS_DIGITAL_RJ_18.GetStructureHeader
            With ds
                .KDKUNJUNGAN = txtNOREG.Text
                Try
                    .DATECREATED = oS_DIGITAL_RJ_18.GetData(txtNOREG.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = now
                End Try
                .DATEUPDATED = now
                .DATE = deDATE.DateTime

                .SDIGITALRJ18_1 = txtDIGITALRJ_1.Text
                .SDIGITALRJ18_2 = txtDIGITALRJ_2.Text
                .SDIGITALRJ18_3 = txtDIGITALRJ_3.Text
                .SDIGITALRJ18_4 = txtDIGITALRJ_4.Text
                .SDIGITALRJ18_5 = txtDIGITALRJ_5.Text
                .SDIGITALRJ18_6 = chkDIGITALRJ_6.Checked
                .SDIGITALRJ18_7 = chkDIGITALRJ_7.Checked
                .SDIGITALRJ18_8 = txtDIGITALRJ_8.Text
                .SDIGITALRJ18_9 = txtDIGITALRJ_9.Text
                .SDIGITALRJ18_10 = txtDIGITALRJ_10.Text
                .SDIGITALRJ18_11 = txtDIGITALRJ_11.Text
                .SDIGITALRJ18_12 = txtDIGITALRJ_12.Text
                .SDIGITALRJ18_13 = txtDIGITALRJ_13.Text
                .SDIGITALRJ18_14 = txtDIGITALRJ_14.Text
                .SDIGITALRJ18_15 = txtDIGITALRJ_15.Text
                .SDIGITALRJ18_16 = txtDIGITALRJ_16.Text
                .SDIGITALRJ18_17 = txtDIGITALRJ_17.Text
                .SDIGITALRJ18_18 = txtDIGITALRJ_18.Text
                .SDIGITALRJ18_19 = txtDIGITALRJ_19.Text
                .SDIGITALRJ18_20 = txtDIGITALRJ_20.Text
                .SDIGITALRJ18_21 = txtDIGITALRJ_21.Text
                .SDIGITALRJ18_22 = chkDIGITALRJ_22.Checked
                .SDIGITALRJ18_23 = chkDIGITALRJ_23.Checked
                .SDIGITALRJ18_24 = chkDIGITALRJ_24.Checked
                .SDIGITALRJ18_25 = chkDIGITALRJ_25.Checked
                .SDIGITALRJ18_26 = chkDIGITALRJ_26.Checked
                .SDIGITALRJ18_27 = chkDIGITALRJ_27.Checked
                .SDIGITALRJ18_28 = txtDIGITALRJ_28.Text
                .SDIGITALRJ18_29 = chkDIGITALRJ_29.Checked
                .SDIGITALRJ18_30 = chkDIGITALRJ_30.Checked
                .SDIGITALRJ18_31 = txtDIGITALRJ_31.Text
                .SDIGITALRJ18_32 = chkDIGITALRJ_32.Checked
                .SDIGITALRJ18_33 = txtDIGITALRJ_33.Text
                .SDIGITALRJ18_34 = txtDIGITALRJ_34.Text
                .SDIGITALRJ18_35 = chkDIGITALRJ_35.Checked
                .SDIGITALRJ18_36 = chkDIGITALRJ_36.Checked
                .SDIGITALRJ18_37 = chkDIGITALRJ_37.Checked
                .SDIGITALRJ18_38 = txtDIGITALRJ_38.Text
                .SDIGITALRJ18_39 = chkDIGITALRJ_39.Checked
                .SDIGITALRJ18_40 = chkDIGITALRJ_40.Checked
                .SDIGITALRJ18_41 = txtDIGITALRJ_41.Text
                .SDIGITALRJ18_42 = txtDIGITALRJ_42.Text
                .SDIGITALRJ18_43 = txtDIGITALRJ_43.Text
                .SDIGITALRJ18_44 = txtDIGITALRJ_44.Text
                .SDIGITALRJ18_45 = txtDIGITALRJ_45.Text
                .SDIGITALRJ18_46 = chkDIGITALRJ_46.Checked
                .SDIGITALRJ18_47 = chkDIGITALRJ_47.Checked
                .SDIGITALRJ18_48 = chkDIGITALRJ_48.Checked
                .SDIGITALRJ18_49 = chkDIGITALRJ_49.Checked
                .SDIGITALRJ18_50 = chkDIGITALRJ_50.Checked
                .SDIGITALRJ18_51 = chkDIGITALRJ_51.Checked
                .SDIGITALRJ18_52 = chkDIGITALRJ_52.Checked
                .SDIGITALRJ18_53 = chkDIGITALRJ_53.Checked
                .SDIGITALRJ18_54 = chkDIGITALRJ_54.Checked
                .SDIGITALRJ18_55 = chkDIGITALRJ_55.Checked
                .SDIGITALRJ18_56 = chkDIGITALRJ_56.Checked
                .SDIGITALRJ18_57 = chkDIGITALRJ_57.Checked
                .SDIGITALRJ18_58 = chkDIGITALRJ_58.Checked
                .SDIGITALRJ18_59 = chkDIGITALRJ_59.Checked
                .SDIGITALRJ18_60 = chkDIGITALRJ_60.Checked
                .SDIGITALRJ18_61 = txtDIGITALRJ_61.Text
                .SDIGITALRJ18_62 = chkDIGITALRJ_62.Checked
                .SDIGITALRJ18_63 = txtDIGITALRJ_63.Text
                .SDIGITALRJ18_64 = chkDIGITALRJ_64.Checked
                .SDIGITALRJ18_65 = chkDIGITALRJ_65.Checked
                .SDIGITALRJ18_66 = txtDIGITALRJ_66.Text
                .SDIGITALRJ18_67 = chkDIGITALRJ_67.Checked
                .SDIGITALRJ18_68 = txtDIGITALRJ_68.Text
                .SDIGITALRJ18_69 = chkDIGITALRJ_69.Checked
                .SDIGITALRJ18_70 = chkDIGITALRJ_70.Checked
                .SDIGITALRJ18_71 = chkDIGITALRJ_71.Checked
                .SDIGITALRJ18_72 = chkDIGITALRJ_72.Checked
                .SDIGITALRJ18_73 = chkDIGITALRJ_73.Checked
                .SDIGITALRJ18_74 = chkDIGITALRJ_74.Checked
                .SDIGITALRJ18_75 = chkDIGITALRJ_75.Checked
                .SDIGITALRJ18_76 = chkDIGITALRJ_76.Checked
                .SDIGITALRJ18_77 = chkDIGITALRJ_77.Checked
                .SDIGITALRJ18_78 = chkDIGITALRJ_78.Checked
                .SDIGITALRJ18_79 = chkDIGITALRJ_79.Checked
                .SDIGITALRJ18_80 = chkDIGITALRJ_80.Checked
                .SDIGITALRJ18_81 = txtDIGITALRJ_81.Text
                .SDIGITALRJ18_82 = txtDIGITALRJ_82.Text
                .SDIGITALRJ18_83 = txtDIGITALRJ_83.Text
                .SDIGITALRJ18_84 = chkDIGITALRJ_84.Checked
                .SDIGITALRJ18_85 = txtDIGITALRJ_85.Text
                .SDIGITALRJ18_86 = txtDIGITALRJ_86.Text
                .SDIGITALRJ18_87 = txtDIGITALRJ_87.Text
                .SDIGITALRJ18_88 = chkDIGITALRJ_88.Checked
                .SDIGITALRJ18_89 = chkDIGITALRJ_89.Checked
                .SDIGITALRJ18_90 = chkDIGITALRJ_90.Checked
                .SDIGITALRJ18_91 = chkDIGITALRJ_91.Checked
                .SDIGITALRJ18_92 = chkDIGITALRJ_92.Checked
                .SDIGITALRJ18_93 = txtDIGITALRJ_93.Text
                .SDIGITALRJ18_94 = txtDIGITALRJ_94.Text
                .SDIGITALRJ18_95 = txtDIGITALRJ_95.Text
                .SDIGITALRJ18_96 = txtDIGITALRJ_96.Text
                .SDIGITALRJ18_97 = txtDIGITALRJ_97.Text
                .SDIGITALRJ18_98 = txtDIGITALRJ_98.Text
                .SDIGITALRJ18_99 = txtDIGITALRJ_99.Text
                .SDIGITALRJ18_100 = txtDIGITALRJ_100.Text
                .SDIGITALRJ18_101 = txtDIGITALRJ_101.Text
                .SDIGITALRJ18_102 = txtDIGITALRJ_102.Text
                .SDIGITALRJ18_103 = txtDIGITALRJ_103.Text
                .SDIGITALRJ18_104 = txtDIGITALRJ_104.Text
                .SDIGITALRJ18_105 = txtDIGITALRJ_105.Text
                .SDIGITALRJ18_106 = txtDIGITALRJ_106.Text
                .SDIGITALRJ18_107 = txtDIGITALRJ_107.Text
                .SDIGITALRJ18_108 = txtDIGITALRJ_108.Text
                .SDIGITALRJ18_109 = txtDIGITALRJ_109.Text
                .SDIGITALRJ18_110 = txtDIGITALRJ_110.Text
                .SDIGITALRJ18_111 = txtDIGITALRJ_111.Text
                .SDIGITALRJ18_112 = txtDIGITALRJ_112.Text
                .SDIGITALRJ18_113 = txtDIGITALRJ_113.Text
                .SDIGITALRJ18_114 = txtDIGITALRJ_114.Text
                .SDIGITALRJ18_115 = txtDIGITALRJ_115.Text
                .SDIGITALRJ18_116 = txtDIGITALRJ_116.Text
                .SDIGITALRJ18_117 = txtDIGITALRJ_117.Text
                .SDIGITALRJ18_118 = txtDIGITALRJ_118.Text
                .SDIGITALRJ18_119 = txtDIGITALRJ_119.Text
                .SDIGITALRJ18_120 = txtDIGITALRJ_120.Text
                .SDIGITALRJ18_121 = txtDIGITALRJ_121.Text
                .SDIGITALRJ18_122 = txtDIGITALRJ_122.Text
                .SDIGITALRJ18_123 = txtDIGITALRJ_123.Text
                .SDIGITALRJ18_124 = chkDIGITALRJ_124.Checked
                .SDIGITALRJ18_125 = chkDIGITALRJ_125.Checked
                .SDIGITALRJ18_126 = chkDIGITALRJ_126.Checked
                .SDIGITALRJ18_127 = chkDIGITALRJ_127.Checked
                .SDIGITALRJ18_128 = chkDIGITALRJ_128.Checked
                .SDIGITALRJ18_129 = txtDIGITALRJ_129.Text
                .SDIGITALRJ18_130 = txtDIGITALRJ_130.Text
                .SDIGITALRJ18_131 = txtDIGITALRJ_131.Text
                .SDIGITALRJ18_132 = txtDIGITALRJ_132.Text
                .SDIGITALRJ18_133 = txtDIGITALRJ_133.Text
                .SDIGITALRJ18_134 = txtDIGITALRJ_134.Text
                .SDIGITALRJ18_135 = txtDIGITALRJ_135.Text
                .SDIGITALRJ18_136 = txtDIGITALRJ_136.Text
                .SDIGITALRJ18_137 = txtDIGITALRJ_137.Text
                .SDIGITALRJ18_138 = txtDIGITALRJ_138.Text

                .DOKTER_KODE = sUserID
                .DOKTER_NAMEDISPLAY = ""

                Try
                    .CETAK = oS_DIGITAL_RJ_18.GetData(txtNOREG.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                Try
                    If sIsOtority = True Then
                        .KDUSER = oS_DIGITAL_RJ_18.GetData(txtNOREG.Text).KDUSER
                        .KDUSER_SIGNATURE = oS_DIGITAL_RJ_18.GetData(txtNOREG.Text).KDUSER_SIGNATURE
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
                    fn_Save = oS_DIGITAL_RJ_18.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_RJ_18.UpdateData(ds)
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
                If btnReload.Enabled = True Then
                    btnReload_Click()
                End If
            Case Keys.PageUp
	            fn_ScrollPage(True)
            Case Keys.PageDown
	            fn_ScrollPage(False)
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
            txtDIGITALRJ_133.Text = String.Join(vbCrLf, listProsedur.ToArray)
            txtDIGITALRJ_134.Text = String.Join(vbCrLf, listDiagnosa.ToArray)
        End If
    End Sub
Private Sub btnReload_Click() Handles btnReload.ItemClick
        Dim dsKunjungan = oS_DIGITAL_RJ_18.GetDataByKunjungan(txtNOREG.Text)

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

            txtDIGITALRJ_132.Text = String.Join(", ", listPenunjang.ToArray)
            txtDIGITALRJ_135.Text = String.Join(", ", listTindakanPengobatan.ToArray)
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
    Private Sub frmEMedrekRJ_18_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
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