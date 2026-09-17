Imports DataAccess
Imports UI.WIN.MAIN.My.Resources

Public Class frmEMedrekRI_25
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_RI_25 As New Digital.clsDigital_RI_25
    Private down As Boolean = False
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
            txtTanggalDaftar.Text = dsPendaftaran.DATE.ToString("dd-MM-yyyy HH:mm:ss")
            txtNoRegister.Text = dsPendaftaran.KDKUNJUNGAN
            txtTujuan.Text = dsPendaftaran.TUJUAN
            txtDokter.Text = dspendaftaran.DOKTER
            txtPEMERIKSA.Text = sUserID
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
        txtPEMERIKSA.Properties.ReadOnly = Status
        txtASESMEN_01.Properties.ReadOnly = Status
        txtASESMEN_02.Properties.ReadOnly = Status
        chkASESMEN_03.Properties.ReadOnly = Status
        chkASESMEN_04.Properties.ReadOnly = Status
        txtASESMEN_05.Properties.ReadOnly = Status
        chkASESMEN_06.Properties.ReadOnly = Status
        txtASESMEN_07.Properties.ReadOnly = Status
        chkASESMEN_08.Properties.ReadOnly = Status
        txtASESMEN_09.Properties.ReadOnly = Status
        chkASESMEN_10.Properties.ReadOnly = Status
        txtASESMEN_11.Properties.ReadOnly = Status
        txtASESMEN_12.Properties.ReadOnly = Status
        chkASESMEN_13.Properties.ReadOnly = Status
        chkASESMEN_14.Properties.ReadOnly = Status
        chkASESMEN_15.Properties.ReadOnly = Status
        chkASESMEN_16.Properties.ReadOnly = Status
        chkASESMEN_17.Properties.ReadOnly = Status
        txtASESMEN_18.Properties.ReadOnly = Status
        txtASESMEN_19.Properties.ReadOnly = Status
        txtASESMEN_20.Properties.ReadOnly = Status
        txtASESMEN_21.Properties.ReadOnly = Status
        txtASESMEN_22.Properties.ReadOnly = Status
        txtASESMEN_23.Properties.ReadOnly = Status
        txtASESMEN_24.Properties.ReadOnly = Status
        txtASESMEN_25.Properties.ReadOnly = Status
        txtASESMEN_26.Properties.ReadOnly = Status
        txtASESMEN_27.Properties.ReadOnly = Status
        txtASESMEN_28.Properties.ReadOnly = Status
        txtASESMEN_29.Properties.ReadOnly = Status
        txtASESMEN_30.Properties.ReadOnly = Status
        txtASESMEN_31.Properties.ReadOnly = Status
        chkASESMEN_32.Properties.ReadOnly = Status
        chkASESMEN_33.Properties.ReadOnly = Status
        txtASESMEN_34.Properties.ReadOnly = Status
        txtASESMEN_35.Properties.ReadOnly = Status
        chkASESMEN_36.Properties.ReadOnly = Status
        chkASESMEN_37.Properties.ReadOnly = Status
        chkASESMEN_38.Properties.ReadOnly = Status
        chkASESMEN_39.Properties.ReadOnly = Status
        chkASESMEN_40.Properties.ReadOnly = Status
        chkASESMEN_41.Properties.ReadOnly = Status
        chkASESMEN_42.Properties.ReadOnly = Status
        chkASESMEN_43.Properties.ReadOnly = Status
        chkASESMEN_44.Properties.ReadOnly = Status
        chkASESMEN_45.Properties.ReadOnly = Status
        chkASESMEN_46.Properties.ReadOnly = Status
        chkASESMEN_47.Properties.ReadOnly = Status
        chkASESMEN_48.Properties.ReadOnly = Status
        txtASESMEN_49.Properties.ReadOnly = Status
        txtASESMEN_50.Properties.ReadOnly = Status
        txtASESMEN_51.Properties.ReadOnly = Status
        txtASESMEN_52.Properties.ReadOnly = Status
        chkASESMEN_53.Properties.ReadOnly = Status
        chkASESMEN_54.Properties.ReadOnly = Status
        txtASESMEN_55.Properties.ReadOnly = Status
        txtASESMEN_56.Properties.ReadOnly = Status
        txtASESMEN_57.Properties.ReadOnly = Status
        txtASESMEN_58.Properties.ReadOnly = Status
        txtASESMEN_59.Properties.ReadOnly = Status
        chkASESMEN_60.Properties.ReadOnly = Status
        chkASESMEN_61.Properties.ReadOnly = Status
        chkASESMEN_62.Properties.ReadOnly = Status
        chkASESMEN_63.Properties.ReadOnly = Status
        chkASESMEN_64.Properties.ReadOnly = Status
        txtASESMEN_65.Properties.ReadOnly = Status
        txtASESMEN_66.Properties.ReadOnly = Status
        txtASESMEN_67.Properties.ReadOnly = Status
        txtASESMEN_68.Properties.ReadOnly = Status
        txtASESMEN_69.Properties.ReadOnly = Status
        txtASESMEN_70.Properties.ReadOnly = Status
        txtASESMEN_71.Properties.ReadOnly = Status
        txtASESMEN_72.Properties.ReadOnly = Status

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
        txtJAM.Text = Now.ToString("HH:mm")
        txtASESMEN_01.ResetText()
        txtASESMEN_02.ResetText()
        chkASESMEN_03.Checked = False
        chkASESMEN_04.Checked = False
        txtASESMEN_05.ResetText()
        chkASESMEN_06.Checked = False
        txtASESMEN_07.ResetText()
        chkASESMEN_08.Checked = False
        txtASESMEN_09.ResetText()
        chkASESMEN_10.Checked = False
        txtASESMEN_11.ResetText()
        txtASESMEN_12.ResetText()
        chkASESMEN_13.Checked = False
        chkASESMEN_14.Checked = False
        chkASESMEN_15.Checked = False
        chkASESMEN_16.Checked = False
        chkASESMEN_17.Checked = False
        txtASESMEN_18.ResetText()
        txtASESMEN_19.ResetText()
        txtASESMEN_20.ResetText()
        txtASESMEN_21.ResetText()
        txtASESMEN_22.ResetText()
        txtASESMEN_23.ResetText()
        txtASESMEN_24.ResetText()
        txtASESMEN_25.ResetText()
        txtASESMEN_26.ResetText()
        txtASESMEN_27.ResetText()
        txtASESMEN_28.ResetText()
        txtASESMEN_29.ResetText()
        txtASESMEN_30.ResetText()
        txtASESMEN_31.ResetText()
        chkASESMEN_32.Checked = False
        chkASESMEN_33.Checked = False
        txtASESMEN_34.ResetText()
        txtASESMEN_35.ResetText()
        chkASESMEN_36.Checked = False
        chkASESMEN_37.Checked = False
        chkASESMEN_38.Checked = False
        chkASESMEN_39.Checked = False
        chkASESMEN_40.Checked = False
        chkASESMEN_41.Checked = False
        chkASESMEN_42.Checked = False
        chkASESMEN_43.Checked = False
        chkASESMEN_44.Checked = False
        chkASESMEN_45.Checked = False
        chkASESMEN_46.Checked = False
        chkASESMEN_47.Checked = False
        chkASESMEN_48.Checked = False
        txtASESMEN_49.ResetText()
        txtASESMEN_50.ResetText()
        txtASESMEN_51.ResetText()
        txtASESMEN_52.ResetText()
        chkASESMEN_53.Checked = False
        chkASESMEN_54.Checked = False
        txtASESMEN_55.ResetText()
        txtASESMEN_56.ResetText()
        txtASESMEN_57.ResetText()
        txtASESMEN_58.ResetText()
        txtASESMEN_59.ResetText()
        chkASESMEN_60.Checked = False
        chkASESMEN_61.Checked = False
        chkASESMEN_62.Checked = False
        chkASESMEN_63.Checked = False
        chkASESMEN_64.Checked = False
        txtASESMEN_65.ResetText()
        txtASESMEN_66.ResetText()
        txtASESMEN_67.ResetText()
        txtASESMEN_68.ResetText()
        txtASESMEN_69.ResetText()
        txtASESMEN_70.ResetText()
        txtASESMEN_71.ResetText()
        txtASESMEN_72.ResetText()

        fn_LoadAsessmenRawatJalan(txtNoRegister.text)
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RI_25.GetData(txtNoRegister.Text)

            With ds
                deDATE.DateTime = .DATE
                txtJAM.Text = .JAM
                txtPEMERIKSA.Text = .PEMERIKSA
                txtASESMEN_01.Text = .ASESMEN_01
                txtASESMEN_02.Text = .ASESMEN_02
                chkASESMEN_03.Checked = .ASESMEN_03
                chkASESMEN_04.Checked = .ASESMEN_04
                txtASESMEN_05.Text = .ASESMEN_05
                chkASESMEN_06.Checked = .ASESMEN_06
                txtASESMEN_07.Text = .ASESMEN_07
                chkASESMEN_08.Checked = .ASESMEN_08
                txtASESMEN_09.Text = .ASESMEN_09
                chkASESMEN_10.Checked = .ASESMEN_10
                txtASESMEN_11.Text = .ASESMEN_11
                txtASESMEN_12.Text = .ASESMEN_12
                chkASESMEN_13.Checked = .ASESMEN_13
                chkASESMEN_14.Checked = .ASESMEN_14
                chkASESMEN_15.Checked = .ASESMEN_15
                chkASESMEN_16.Checked = .ASESMEN_16
                chkASESMEN_17.Checked = .ASESMEN_17
                txtASESMEN_18.Text = .ASESMEN_18
                txtASESMEN_19.Text = .ASESMEN_19
                txtASESMEN_20.Text = .ASESMEN_20
                txtASESMEN_21.Text = .ASESMEN_21
                txtASESMEN_22.Text = .ASESMEN_22
                txtASESMEN_23.Text = .ASESMEN_23
                txtASESMEN_24.Text = .ASESMEN_24
                txtASESMEN_25.Text = .ASESMEN_25
                txtASESMEN_26.Text = .ASESMEN_26
                txtASESMEN_27.Text = .ASESMEN_27
                txtASESMEN_28.Text = .ASESMEN_28
                txtASESMEN_29.Text = .ASESMEN_29
                txtASESMEN_30.Text = .ASESMEN_30
                txtASESMEN_31.Text = .ASESMEN_31
                chkASESMEN_32.Checked = .ASESMEN_32
                chkASESMEN_33.Checked = .ASESMEN_33
                txtASESMEN_34.Text = .ASESMEN_34
                txtASESMEN_35.Text = .ASESMEN_35
                chkASESMEN_36.Checked = .ASESMEN_36
                chkASESMEN_37.Checked = .ASESMEN_37
                chkASESMEN_38.Checked = .ASESMEN_38
                chkASESMEN_39.Checked = .ASESMEN_39
                chkASESMEN_40.Checked = .ASESMEN_40
                chkASESMEN_41.Checked = .ASESMEN_41
                chkASESMEN_42.Checked = .ASESMEN_42
                chkASESMEN_43.Checked = .ASESMEN_43
                chkASESMEN_44.Checked = .ASESMEN_44
                chkASESMEN_45.Checked = .ASESMEN_45
                chkASESMEN_46.Checked = .ASESMEN_46
                chkASESMEN_47.Checked = .ASESMEN_47
                chkASESMEN_48.Checked = .ASESMEN_48
                txtASESMEN_49.Text = .ASESMEN_49
                txtASESMEN_50.Text = .ASESMEN_50
                txtASESMEN_51.Text = .ASESMEN_51
                txtASESMEN_52.Text = .ASESMEN_52
                chkASESMEN_53.Checked = .ASESMEN_53
                chkASESMEN_54.Checked = .ASESMEN_54
                txtASESMEN_55.Text = .ASESMEN_55
                txtASESMEN_56.Text = .ASESMEN_56
                txtASESMEN_57.Text = .ASESMEN_57
                txtASESMEN_58.Text = .ASESMEN_58
                txtASESMEN_59.Text = .ASESMEN_59
                chkASESMEN_60.Checked = .ASESMEN_60
                chkASESMEN_61.Checked = .ASESMEN_61
                chkASESMEN_62.Checked = .ASESMEN_62
                chkASESMEN_63.Checked = .ASESMEN_63
                chkASESMEN_64.Checked = .ASESMEN_64
                txtASESMEN_65.Text = .ASESMEN_65
                txtASESMEN_66.Text = .ASESMEN_66
                txtASESMEN_67.Text = .ASESMEN_67
                txtASESMEN_68.Text = .ASESMEN_68
                txtASESMEN_69.Text = .ASESMEN_69
                txtASESMEN_70.Text = .ASESMEN_70
                txtASESMEN_71.Text = .ASESMEN_71
                txtASESMEN_72.Text = .ASESMEN_72

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
        '    'txtKELUHAN_UTAMA.Text = dsAsessmenRawatJalan.KELUHAN_UTAMA
        '    txtASESMEN_27.Text = dsAsessmenRawatJalan.KESADARAN_UMUM
        '    txtASESMEN_28.Text = dsAsessmenRawatJalan.TANDA_VITAL_01
        '    txtASESMEN_29.text = dsAsessmenRawatJalan.TANDA_VITAL_02
        '    txtASESMEN_31.text = dsAsessmenRawatJalan.TANDA_VITAL_03
        '    txtASESMEN_30.text = dsAsessmenRawatJalan.TANDA_VITAL_04
        '    txtASESMEN_50.Text = dsAsessmenRawatJalan.TANDA_VITAL_05
        '    txtASESMEN_49.Text = dsAsessmenRawatJalan.TANDA_VITAL_06
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
            Dim ds = oS_DIGITAL_RI_25.GetStructureHeader
            With ds
                .KDKUNJUNGAN = txtNoRegister.Text
                Try
                    .DATECREATED = oS_DIGITAL_RI_25.GetData(txtNoRegister.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try 
                .DATEUPDATED = now

                .DATE = deDATE.DateTime
                .JAM = txtJAM.Text
                .PEMERIKSA = txtPEMERIKSA.Text
                .ASESMEN_01 = txtASESMEN_01.Text
                .ASESMEN_02 = txtASESMEN_02.Text
                .ASESMEN_03 = chkASESMEN_03.Checked
                .ASESMEN_04 = chkASESMEN_04.Checked
                .ASESMEN_05 = txtASESMEN_05.Text
                .ASESMEN_06 = chkASESMEN_06.Checked
                .ASESMEN_07 = txtASESMEN_07.Text
                .ASESMEN_08 = chkASESMEN_08.Checked
                .ASESMEN_09 = txtASESMEN_09.Text
                .ASESMEN_10 = chkASESMEN_10.Checked
                .ASESMEN_11 = txtASESMEN_11.Text
                .ASESMEN_12 = txtASESMEN_12.Text
                .ASESMEN_13 = chkASESMEN_13.Checked
                .ASESMEN_14 = chkASESMEN_14.Checked
                .ASESMEN_15 = chkASESMEN_15.Checked
                .ASESMEN_16 = chkASESMEN_16.Checked
                .ASESMEN_17 = chkASESMEN_17.Checked
                .ASESMEN_18 = txtASESMEN_18.Text
                .ASESMEN_19 = txtASESMEN_19.Text
                .ASESMEN_20 = txtASESMEN_20.Text
                .ASESMEN_21 = txtASESMEN_21.Text
                .ASESMEN_22 = txtASESMEN_22.Text
                .ASESMEN_23 = txtASESMEN_23.Text
                .ASESMEN_24 = txtASESMEN_24.Text
                .ASESMEN_25 = txtASESMEN_25.Text
                .ASESMEN_26 = txtASESMEN_26.Text
                .ASESMEN_27 = txtASESMEN_27.Text
                .ASESMEN_28 = txtASESMEN_28.Text
                .ASESMEN_29 = txtASESMEN_29.Text
                .ASESMEN_30 = txtASESMEN_30.Text
                .ASESMEN_31 = txtASESMEN_31.Text
                .ASESMEN_32 = chkASESMEN_32.Checked
                .ASESMEN_33 = chkASESMEN_33.Checked
                .ASESMEN_34 = txtASESMEN_34.Text
                .ASESMEN_35 = txtASESMEN_35.Text
                .ASESMEN_36 = chkASESMEN_36.Checked
                .ASESMEN_37 = chkASESMEN_37.Checked
                .ASESMEN_38 = chkASESMEN_38.Checked
                .ASESMEN_39 = chkASESMEN_39.Checked
                .ASESMEN_40 = chkASESMEN_40.Checked
                .ASESMEN_41 = chkASESMEN_41.Checked
                .ASESMEN_42 = chkASESMEN_42.Checked
                .ASESMEN_43 = chkASESMEN_43.Checked
                .ASESMEN_44 = chkASESMEN_44.Checked
                .ASESMEN_45 = chkASESMEN_45.Checked
                .ASESMEN_46 = chkASESMEN_46.Checked
                .ASESMEN_47 = chkASESMEN_47.Checked
                .ASESMEN_48 = chkASESMEN_48.Checked
                .ASESMEN_49 = txtASESMEN_49.Text
                .ASESMEN_50 = txtASESMEN_50.Text
                .ASESMEN_51 = txtASESMEN_51.Text
                .ASESMEN_52 = txtASESMEN_52.Text
                .ASESMEN_53 = chkASESMEN_53.Checked
                .ASESMEN_54 = chkASESMEN_54.Checked
                .ASESMEN_55 = txtASESMEN_55.Text
                .ASESMEN_56 = txtASESMEN_56.Text
                .ASESMEN_57 = txtASESMEN_57.Text
                .ASESMEN_58 = txtASESMEN_58.Text
                .ASESMEN_59 = txtASESMEN_59.Text
                .ASESMEN_60 = chkASESMEN_60.Checked
                .ASESMEN_61 = chkASESMEN_61.Checked
                .ASESMEN_62 = chkASESMEN_62.Checked
                .ASESMEN_63 = chkASESMEN_63.Checked
                .ASESMEN_64 = chkASESMEN_64.Checked
                .ASESMEN_65 = txtASESMEN_65.Text
                .ASESMEN_66 = txtASESMEN_66.Text
                .ASESMEN_67 = txtASESMEN_67.Text
                .ASESMEN_68 = txtASESMEN_68.Text
                .ASESMEN_69 = txtASESMEN_69.Text
                .ASESMEN_70 = txtASESMEN_70.Text
                .ASESMEN_71 = txtASESMEN_71.Text
                .ASESMEN_72 = txtASESMEN_72.Text

                Try
                    .CETAK = oS_DIGITAL_RI_25.GetData(txtNoRegister.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

               Try
                    If sIsOtority = True Then
                        .KDUSER = oS_DIGITAL_RI_25.GetData(txtNoRegister.Text).KDUSER
                        .KDUSER_SIGNATURE = oS_DIGITAL_RI_25.GetData(txtNoRegister.Text).KDUSER_SIGNATURE
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
                    fn_Save = oS_DIGITAL_RI_25.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_RI_25.UpdateData(ds)
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
        'Dim oDoctor As New Master.clsDoctor
        'Try
        '    'grdKDDOCTOR2.Properties.DataSource = oDoctor.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
        '    'grdKDDOCTOR2.Properties.ValueMember = "KDDOCTOR"
        '    'grdKDDOCTOR2.Properties.DisplayMember = "NAME_DISPLAY"

        'Catch oErr As Exception
        '    MsgBox("Load Doctor Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub

    Private Sub frmEMedrekRI_25_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
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