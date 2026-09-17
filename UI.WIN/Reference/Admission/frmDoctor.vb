Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq
Imports MySql.Data.MySqlClient
Imports System.Text.RegularExpressions
Imports System
Imports System.Text
Imports System.Runtime.InteropServices

Public Class frmDoctor
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oDoctor As New Reference.clsDoctor
    Private oDoctorSatuSehat As New Reference.clsDoctorSatuSehat

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, Optional ByVal NoId As String = "")
        deDATESIP.DateTime = Now
        oFormMode = FormMode
        sNoId = NoId
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = Doctor.TITLE

            lNAME_DISPLAY.Text = Doctor.NAME_DISPLAY & " *"
            chkISACTIVE.Text = Doctor.ISACTIVE
            lSPESIALISTIK.Text = Doctor.KDSPESIALISTIK
            lPHONE.Text = Doctor.PHONE
            lFAX.Text = Doctor.FAX
            lMOBILE.Text = Doctor.MOBILE
            lOTHER.Text = Doctor.OTHER
            lEMAIL.Text = Doctor.EMAIL
            lWEBSITE.Text = Doctor.WEBSITE

            lBILL_STREET.Text = Doctor.BILL_STREET
            lBILL_CITY.Text = Doctor.BILL_CITY
            lBILL_STATE.Text = Doctor.BILL_STATE
            lBILL_ZIP.Text = Doctor.BILL_ZIP
            lBILL_COUNTRY.Text = Doctor.BILL_COUNTRY

            lKDCOA.Text = Doctor.KDCOA & " *"

            lSUBSPESIALIS.Text = Doctor.SUBSPESIALIS
            lSIP.Text = Doctor.SIP
            lDATESIP.Text = Doctor.DATESIP

            lVCLAIM_KDDPJP.Text = Doctor.VCLAIM_KDDPJP
            lVCLAIM_KDDOCTOR.Text = Doctor.VCLAIM_KDDOCTOR
            lKDDEPARTMENT.Text = Doctor.KDDEPARTMENT
            lJENISPELAYANAN.Text = "Jenis Pelayanan"

            tab1.Text = Doctor.TAB_CONTACT
            tab2.Text = Doctor.TAB_BILL
            tab3.Text = Doctor.TAB_ACCOUNT
            tab4.Text = Doctor.TAB_OTHER
            tab5.Text = Doctor.TAB_DOCTOR
            tab6.Text = Doctor.TAB_DEPARTMENT

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtNAME_DISPLAY.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDSPESIALISTIK()
        fn_LoadKDCOA()
        fn_LoadDOCTOR()
        fn_LoadDEPARMENT()
        fn_LoadDEPARMENT_()
        'fn_LoadDataOnline()


        cboJAM.Items.Clear()

        'For h As Integer = 0 To 23
        '    cboJAM.Items.Add(Format(h, "00") & ":00")
        'Next

        For jam As Integer = 0 To 23
            For menit As Integer = 0 To 59 Step 10
                cboJAM.Items.Add($"{jam:00}:{menit:00}")
            Next
        Next

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

        Dim dsSatuSehat = oDoctorSatuSehat.GetData(sNoId)
        If dsSatuSehat IsNot Nothing Then
            txtIDSATUSEHAT.Text = dsSatuSehat.IDSATUSEHAT
        End If
    End Sub
    Private Sub fn_ViewMode(ByVal Status As Boolean)
        btnSaveNew.Enabled = Not Status
        btnSaveClose.Enabled = Not Status

        txtKODETTD.Properties.ReadOnly = Status
        rbJENISPELAYANAN.Properties.ReadOnly = Status
        txtVCLAIM_KDDPJP.Properties.ReadOnly = True
        txtVCLAIM_KDDOCTOR.Properties.ReadOnly = Status
        grdKDDEPARTMENT.Properties.ReadOnly = Status
        grdKDSPESIALISTIK.Properties.ReadOnly = Status

        txtNAME_DISPLAY.Properties.ReadOnly = Status
        chkISACTIVE.Properties.ReadOnly = Status

        txtPHONE.Properties.ReadOnly = Status
        txtFAX.Properties.ReadOnly = Status
        txtMOBILE.Properties.ReadOnly = Status
        txtEMAIL.Properties.ReadOnly = Status
        txtOTHER.Properties.ReadOnly = Status
        txtWEBSITE.Properties.ReadOnly = Status

        txtBILL_STREET.Properties.ReadOnly = Status
        txtBILL_CITY.Properties.ReadOnly = Status
        txtBILL_STATE.Properties.ReadOnly = Status
        txtBILL_ZIP.Properties.ReadOnly = Status
        txtBILL_COUNTRY.Properties.ReadOnly = Status
        txtNAME_ONLINE.Properties.ReadOnly = Status
        txtMEMO.Properties.ReadOnly = Status
        cboJENISKELAMIN.Properties.ReadOnly = Status
        deDATETANGGALLAHIR.Properties.ReadOnly = Status

        grdKDCOA.Properties.ReadOnly = Status

        rbCATEGORY.Properties.ReadOnly = Status
        txtSIP.Properties.ReadOnly = Status
        deDATESIP.Properties.ReadOnly = Status
        txtNIP.Properties.ReadOnly = Status
        cboSTATUS.Properties.ReadOnly = Status

        grvDetail_DOCTOR.OptionsBehavior.ReadOnly = Status
        grvDetail_DEPARTMENT.OptionsBehavior.ReadOnly = Status
        grvDOCTORBPJS.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtNAME_DISPLAY.ResetText()
        grdKDDEPARTMENT.EditValue = 0
        grdKDSPESIALISTIK.ResetText()

        txtKODETTD.ResetText()

        chkISACTIVE.Checked = True

        txtPHONE.ResetText()
        txtFAX.ResetText()
        txtMOBILE.ResetText()
        txtEMAIL.ResetText()
        txtOTHER.ResetText()
        txtWEBSITE.ResetText()

        txtBILL_STREET.ResetText()
        txtBILL_CITY.ResetText()
        txtBILL_STATE.ResetText()
        txtBILL_ZIP.ResetText()
        txtBILL_COUNTRY.ResetText()

        txtMEMO.ResetText()

        txtSIP.ResetText()

        txtVCLAIM_KDDPJP.ResetText()
        txtVCLAIM_KDDOCTOR.ResetText()
        txtESTIMASI.Text = 6

        txtNAME_ONLINE.ResetText()
        txtNIP.ResetText()
        cboSTATUS.ResetText()

        cboJENISKELAMIN.ResetText()
        deDATETANGGALLAHIR.ResetText()

        Try
            grdKDCOA.Text = oDoctor.AccountDefault
        Catch oErr As Exception

        End Try
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oDoctor.GetData(sNoId)

            With ds
                grdKDSPESIALISTIK.Text = .KDSPESIALISTIK
                txtNAME_DISPLAY.Text = .NAME_DISPLAY
                chkISACTIVE.Checked = .ISACTIVE

                txtPHONE.Text = .PHONE
                txtFAX.Text = .FAX
                txtMOBILE.Text = .MOBILE
                txtEMAIL.Text = .EMAIL
                txtOTHER.Text = .OTHER
                txtWEBSITE.Text = .WEBSITE

                txtBILL_STREET.Text = .BILL_STREET
                txtBILL_CITY.Text = .BILL_CITY
                txtBILL_STATE.Text = .BILL_STATE
                txtBILL_ZIP.Text = .BILL_ZIP
                txtBILL_COUNTRY.Text = .BILL_COUNTRY

                txtMEMO.Text = .MEMO
                grdKDCOA.Text = .KDCOA
                txtNAME_ONLINE.Text = .NAME_ONLINE

                rbCATEGORY.SelectedIndex = .SUBSPESIALIS
                txtSIP.Text = .SIP
                deDATESIP.DateTime = .DATESIP

                txtVCLAIM_KDDPJP.Text = .VCLAIM_KDDPJP
                txtVCLAIM_KDDOCTOR.Text = .VCLAIM_KDDOCTOR
                grdKDDEPARTMENT.Text = .KDDEPARTMENT
                rbJENISPELAYANAN.Text = .VCLAIM_JENISPELAYANAN
                txtESTIMASI.Text = .ESTIMASI_MENIT

                txtNIP.Text = .NIP
                cboSTATUS.Text = .ISSTATUS

                txtKODETTD.Text = .KODETTD

                cboJENISKELAMIN.Text = .JENISKELAMIN
                deDATETANGGALLAHIR.DateTime = .TANGGALLAHIR

                BindingSource_DOCTOR.DataSource = oDoctor.GetDataDetail_DOCTOR.Where(Function(x) x.KDDOCTOR = sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail_DOCTOR.DataSource = BindingSource_DOCTOR

                BindingSource_DEPARTMENT.DataSource = oDoctor.GetDataDetail_DEPARMENT.Where(Function(x) x.KDDOCTOR = sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail_DEPARTMENT.DataSource = BindingSource_DEPARTMENT

                BindingSource_DOCTORBPJS.DataSource = oDoctor.GetDataDetail_DOCTORBPJS.Where(Function(x) x.KDDOCTOR = sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail_DEPARTMENT.DataSource = BindingSource_DEPARTMENT

                BindingSource.DataSource = oDoctor.GetDataDetail(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = BindingSource

                tabControl.SelectedTabPage = tab8
                tabControl.SelectedTabPage = tab7
                tabControl.SelectedTabPage = tab6
                tabControl.SelectedTabPage = tab5
                tabControl.SelectedTabPage = tab4
                tabControl.SelectedTabPage = tab3
                tabControl.SelectedTabPage = tab2
                tabControl.SelectedTabPage = tab1
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail.DeleteSelectedRows()
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If grdKDSPESIALISTIK.Text = String.Empty Then
                grdKDSPESIALISTIK.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDSPESIALISTIK.ErrorText = Statement.ErrorRequired

                grdKDSPESIALISTIK.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtNAME_DISPLAY.Text = String.Empty Then
                txtNAME_DISPLAY.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtNAME_DISPLAY.ErrorText = Statement.ErrorRequired

                txtNAME_DISPLAY.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDEPARTMENT.Text = String.Empty Then
                grdKDDEPARTMENT.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDEPARTMENT.ErrorText = Statement.ErrorRequired

                grdKDDEPARTMENT.Focus()
                fn_Validate = False
                Exit Function
            End If
            If cboJENISKELAMIN.Text = String.Empty Then
                cboJENISKELAMIN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                cboJENISKELAMIN.ErrorText = Statement.ErrorRequired

                cboJENISKELAMIN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If deDATETANGGALLAHIR.Text = String.Empty Then
                deDATETANGGALLAHIR.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                deDATETANGGALLAHIR.ErrorText = Statement.ErrorRequired

                deDATETANGGALLAHIR.Focus()
                fn_Validate = False
                Exit Function
            End If
            'If txtKODETTD.Text = String.Empty Then
            '    txtKODETTD.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '    txtKODETTD.ErrorText = Statement.ErrorRequired

            '    txtKODETTD.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If
            'If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            '    If oDoctor.IsExist(txtNAME_DISPLAY.Text.ToUpper.Trim) = True Then
            '        txtNAME_DISPLAY.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '        txtNAME_DISPLAY.ErrorText = Statement.ErrorRegistered

            '        txtNAME_DISPLAY.Focus()
            '        fn_Validate = False
            '        Exit Function
            '    End If
            'Else
            '    If txtNAME_DISPLAY.Text.ToUpper.Trim <> oDoctor.GetData(sNoId).NAME_DISPLAY Then
            '        If oDoctor.IsExist(txtNAME_DISPLAY.Text.ToUpper.Trim) = True Then
            '            txtNAME_DISPLAY.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '            txtNAME_DISPLAY.ErrorText = Statement.ErrorRegistered

            '            txtNAME_DISPLAY.Focus()
            '            fn_Validate = False
            '            Exit Function
            '        End If
            '    End If
            'End If
            If grdKDCOA.Text = String.Empty Then
                tabControl.SelectedTabPage = tab3

                grdKDCOA.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDCOA.ErrorText = Statement.ErrorRequired

                grdKDCOA.Focus()
                fn_Validate = False
                Exit Function
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oDoctor.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oDoctor.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDDOCTOR = sNoId
                .NAME_DISPLAY = txtNAME_DISPLAY.Text.Trim
                .KDSPESIALISTIK = grdKDSPESIALISTIK.EditValue.ToString.Trim
                .PHONE = txtPHONE.Text.Trim.ToUpper
                .FAX = txtFAX.Text.Trim.ToUpper
                .MOBILE = txtMOBILE.Text.Trim.ToUpper
                .EMAIL = txtEMAIL.Text.Trim.ToUpper
                .OTHER = txtOTHER.Text.Trim.ToUpper
                .WEBSITE = txtWEBSITE.Text.Trim.ToUpper
                .BILL_STREET = txtBILL_STREET.Text.Trim.ToUpper
                .BILL_CITY = txtBILL_CITY.Text.Trim.ToUpper
                .BILL_STATE = txtBILL_STATE.Text.Trim.ToUpper
                .BILL_ZIP = txtBILL_ZIP.Text.Trim.ToUpper
                .BILL_COUNTRY = txtBILL_COUNTRY.Text.Trim.ToUpper
                .MEMO = txtMEMO.Text.Trim.ToUpper
                .KDCOA = IIf(String.IsNullOrEmpty(grdKDCOA.EditValue), String.Empty, grdKDCOA.EditValue)
                .ISACTIVE = chkISACTIVE.Checked
                .SUBSPESIALIS = rbCATEGORY.SelectedIndex
                .SIP = txtSIP.Text.Trim.ToUpper
                .DATESIP = deDATESIP.DateTime
                .VCLAIM_KDDPJP = txtVCLAIM_KDDPJP.Text
                .VCLAIM_KDDOCTOR = txtVCLAIM_KDDOCTOR.Text
                .KDDEPARTMENT = grdKDDEPARTMENT.EditValue
                .VCLAIM_JENISPELAYANAN = rbJENISPELAYANAN.SelectedIndex
                .ESTIMASI_MENIT = txtESTIMASI.Text
                .NAME_ONLINE = txtNAME_ONLINE.Text.Trim
                .KODETTD = txtKODETTD.Text
                .NIP = txtNIP.Text.Trim
                .ISSTATUS = cboSTATUS.Text
                .JENISKELAMIN = cboJENISKELAMIN.Text
                .TANGGALLAHIR = deDATETANGGALLAHIR.DateTime
            End With

            ' ***** Dokter *****
            Dim arrDetail_DOCTOR = oDoctor.GetStructureDetail_DOCTORist
            For i As Integer = 0 To grvDetail_DOCTOR.RowCount - 2
                Dim dsDetail_DOCTOR = oDoctor.GetStructureDetail_DOCTOR
                With dsDetail_DOCTOR
                    .KDDOCTOR = ds.KDDOCTOR
                    .KDDOCTOR_ = grvDetail_DOCTOR.GetRowCellValue(i, colKDDOCTOR)
                    .SEQ = i
                    .MEMO = ""
                End With
                arrDetail_DOCTOR.Add(dsDetail_DOCTOR)
            Next

            ' ***** Department *****
            Dim arrDetail_DEPARTMENT = oDoctor.GetStructureDetail_DEPARTMENTist
            For i As Integer = 0 To grvDetail_DEPARTMENT.RowCount - 2
                Dim dsDetail_DEPARTMENT = oDoctor.GetStructureDetail_DEPARMENT
                With dsDetail_DEPARTMENT
                    .KDDOCTOR = ds.KDDOCTOR
                    .KDDEPARTMENT = grvDetail_DEPARTMENT.GetRowCellValue(i, colKDDEPARMENT)
                    .SEQ = i
                    .MEMO = ""
                End With
                arrDetail_DEPARTMENT.Add(dsDetail_DEPARTMENT)
            Next

            ' ***** Dokter BPJS *****
            Dim arrDetail_DOCTORBPJS = oDoctor.GetStructureDetail_DOCTORBPJSList
            For i As Integer = 0 To grvDOCTORBPJS.RowCount - 2
                Dim dsDetail_DOCTORBPJS = oDoctor.GetStructureDetail_DOCTORBPJS
                With dsDetail_DOCTORBPJS
                    .KDDOCTOR = ds.KDDOCTOR
                    .SEQ = i
                    .MEMO = grvDOCTORBPJS.GetRowCellValue(i, colMEMO)
                End With
                arrDetail_DOCTORBPJS.Add(dsDetail_DOCTORBPJS)
            Next

            ' ***** DETIL *****
            Dim arrDetail = oDoctor.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oDoctor.GetStructureDetail
                With dsDetail
                    .KDDOCTOR = ds.KDDOCTOR
                    .SEQ = i
                    .HARI = grvDetail.GetRowCellValue(i, colHARI)
                    .KAPASITASPASIEN_TOTAL = grvDetail.GetRowCellValue(i, colKAPASITASPASIEN_TOTAL)
                    .KAPASITASPASIEN_JKN = grvDetail.GetRowCellValue(i, colKAPASITASPASIEN_JKN)
                    .KAPASITASPASIEN_NONJKN = grvDetail.GetRowCellValue(i, colKAPASITASPASIEN_NONJKN)
                    .LIBUR = grvDetail.GetRowCellValue(i, colLIBUR)
                    .BUKA = grvDetail.GetRowCellValue(i, colBUKA)
                    .TUTUP = grvDetail.GetRowCellValue(i, colTUTUP)
                    .DESCRIPTION = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colDESCRIPTION)), "-", grvDetail.GetRowCellValue(i, colDESCRIPTION))
                    .KAPASITASPASIEN_JKNMOBILE = grvDetail.GetRowCellValue(i, colKAPASITASPASIEN_JKN)
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oDoctor.InsertData(ds, "", arrDetail, arrDetail_DOCTOR, arrDetail_DEPARTMENT, arrDetail_DOCTORBPJS)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oDoctor.UpdateData(ds, arrDetail, arrDetail_DOCTOR, arrDetail_DEPARTMENT, arrDetail_DOCTORBPJS)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

            'Senin
            'Selasa
            'Rabu
            'Kamis
            'Jumat
            'Sabtu
            'Minggu
            'libur nasional
            'If fn_SavePegawai() = True Then
            '    If fn_SaveDokter() = True Then
            '        For i As Integer = 0 To grvDetail.RowCount - 2
            '            If fn_SaveJadwalDokterNonJKN(IIf(grvDetail.GetRowCellValue(i, colHARI) = "Minggu", "AKHAD", grvDetail.GetRowCellValue(i, colHARI).ToString.ToUpper), grvDetail.GetRowCellValue(i, colBUKA) & "00", grvDetail.GetRowCellValue(i, colTUTUP) & "00", grvDetail.GetRowCellValue(i, colKAPASITASPASIEN_NONJKN)) = True Then
            '                MsgBox("Berhasil Brigging " & grvDetail.GetRowCellValue(i, colHARI), MsgBoxStyle.Information, Me.Text)
            '            Else
            '                MsgBox("Gagal Brigging Jadwal " & grvDetail.GetRowCellValue(i, colHARI), MsgBoxStyle.Information, Me.Text)
            '            End If
            '        Next
            '    Else
            '        MsgBox("Gagal Brigging Dokter", MsgBoxStyle.Information, Me.Text)
            '    End If
            'Else
            '    MsgBox("Gagal Brigging Pegawai", MsgBoxStyle.Information, Me.Text)
            'End If

            If fn_Save = True Then
                Dim dsCustomerSatusehat = oDoctorSatuSehat.GetData(ds.KDDOCTOR)
                If dsCustomerSatusehat Is Nothing Then
                    If SatuSehat_Organisasi <> "" Then
                        If txtOTHER.Text = "" Then
                            MsgBox("Nik Kosong", MsgBoxStyle.Information, Me.Text)
                            txtOTHER.Focus()
                            Exit Function
                        End If

                        Dim respon As String = SatusehatAuth.PractitionerByNIK(SatuSehat_Production, SatuSehat_token, txtOTHER.Text)

                        If Not String.IsNullOrEmpty(respon) Then
                            'MsgBox("✓ Respon satu sehat berhasil didapatkan!" & vbCrLf & respon, MsgBoxStyle.Information, Me.Text)

                            If respon.Contains("Exception") Then
                                Dim frmPesanSatuSehat As New frmPesanSatuSehat
                                frmPesanSatuSehat.fn_LoadJson("✗ Exception!" & vbCrLf & respon, "Exception")
                                frmPesanSatuSehat.ShowDialog(Me)
                            Else
                                If respon.Contains("200") Then
                                    Dim ID As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "id")

                                    txtIDSATUSEHAT.Text = ID

                                    If ID = "" Then
                                        Dim frmPesanSatuSehat As New frmPesanSatuSehat
                                        frmPesanSatuSehat.fn_LoadJson(Regex.Replace(respon, "^.{4}", ""), respon.Substring(0, 3))
                                        frmPesanSatuSehat.ShowDialog(Me)
                                    Else
                                        If fn_SaveSatuSehat(True, ds.KDDOCTOR, ID, "", Regex.Replace(respon, "^.{4}", "")) = True Then
                                            Dim frmPesanSatuSehat As New frmPesanSatuSehat
                                            frmPesanSatuSehat.fn_LoadJson(Regex.Replace(respon, "^.{4}", ""), respon.Substring(0, 3))
                                            frmPesanSatuSehat.ShowDialog(Me)
                                        Else
                                            MsgBox("✗ Gagal simpan database!" & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)
                                        End If
                                    End If
                                Else
                                    Dim cektoken As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "code")
                                    If cektoken = "invalid-access-token" Then
                                        Dim token As String = SatusehatAuth.GetAccessToken(SatuSehat_Production, SatuSehat_client_id, SatuSehat_client_secret)

                                        SatuSehat_token = SatusehatAuth.GetToken(token, "access_token")

                                        Dim oToken As New Setting.clsSatuSehatKoneksiToken

                                        Dim dsToken = oToken.GetDataSEQ(IIf(SatuSehat_Production = False, "SANDBOX", "PRODUCTION"))

                                        If dsToken IsNot Nothing Then
                                            If oToken.UpdateToken(dsToken.KDKONEKSI, dsToken.SEQ, Regex.Replace(respon, "^.{4}", ""), SatuSehat_token) = False Then
                                                SatuSehat_Organisasi = ""
                                                SatuSehat_client_id = ""
                                                SatuSehat_client_secret = ""

                                                MsgBox("✗ Gagal Simpan Token!" & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)
                                            Else
                                                Dim responulang As String = SatusehatAuth.PractitionerByNIK(SatuSehat_Production, SatuSehat_token, txtOTHER.Text)

                                                Dim ID As String = SatusehatAuth.GetToken(Regex.Replace(responulang, "^.{4}", ""), "id")

                                                txtIDSATUSEHAT.Text = ID

                                                If ID = "" Then
                                                    Dim frmPesanSatuSehat As New frmPesanSatuSehat
                                                    frmPesanSatuSehat.fn_LoadJson(Regex.Replace(responulang, "^.{4}", ""), responulang.Substring(0, 3))
                                                    frmPesanSatuSehat.ShowDialog(Me)
                                                Else
                                                    If fn_SaveSatuSehat(True, ds.KDDOCTOR, ID, "", Regex.Replace(responulang, "^.{4}", "")) = True Then
                                                        Dim frmPesanSatuSehat As New frmPesanSatuSehat
                                                        frmPesanSatuSehat.fn_LoadJson(Regex.Replace(responulang, "^.{4}", ""), responulang.Substring(0, 3))
                                                        frmPesanSatuSehat.ShowDialog(Me)
                                                    Else
                                                        MsgBox("✗ Gagal simpan database!" & vbCrLf & responulang, MsgBoxStyle.Exclamation, Me.Text)
                                                    End If
                                                End If
                                            End If
                                        Else
                                            SatuSehat_Organisasi = ""
                                            SatuSehat_client_id = ""
                                            SatuSehat_client_secret = ""

                                            MsgBox("✗ Token Database Kosong, Silahkan Add Terlebih Dahulu di menu Setting!" & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)

                                        End If
                                    Else
                                        Dim frmPesanSatuSehat As New frmPesanSatuSehat
                                        frmPesanSatuSehat.fn_LoadJson(Regex.Replace(respon, "^.{4}", ""), respon.Substring(0, 3))
                                        frmPesanSatuSehat.ShowDialog(Me)
                                    End If
                                End If
                            End If

                        Else
                            MsgBox("✗ Gagal mendapatkan respon. respon kosong", MsgBoxStyle.Information, Me.Text)
                        End If
                    Else
                        MsgBox("Organisasi ID masih kosong", MsgBoxStyle.Information, Me.Text)
                    End If
                End If
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Function fn_SaveSatuSehat(ByVal isadd As Boolean, ByVal KDDOCTOR As String, ByVal IDSATUSEHAT As String, ByVal REQUEST As String, ByVal RESPON As String) As Boolean
        Try
            ' ***** HEADER *****

            Dim ds = oDoctorSatuSehat.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oDoctorSatuSehat.GetData(KDDOCTOR).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDDOCTOR = KDDOCTOR
                .IDSATUSEHAT = IDSATUSEHAT
                .REQUEST = REQUEST
                .RESPON = RESPON
                .ISDEFAULT = False
                .ISACTIVE = True
            End With

            If isadd = True Then
                Try
                    fn_SaveSatuSehat = oDoctorSatuSehat.InsertData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                Try
                    fn_SaveSatuSehat = oDoctorSatuSehat.UpdateData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveSatuSehat = False
        End Try
    End Function
    'Private Function fn_SavePegawai() As Boolean
    '    Try
    '        If txtVCLAIM_KDDPJP.Text = "" Then
    '            MsgBox("Kode DPJP Kosong", MsgBoxStyle.Exclamation, Me.Text)
    '            Exit Function
    '        End If

    '        Dim oConnMySql As New MySql.Data.MySqlClient.MySqlConnection
    '        Dim oCommMySql As New MySql.Data.MySqlClient.MySqlCommand
    '        Dim daMySql As MySql.Data.MySqlClient.MySqlDataAdapter
    '        Dim dsMySql As New DataSet
    '        Dim MYSQL As String

    '        dsMySql = New DataSet

    '        oConnMySql = New MySqlConnection(sConnMySqlAntrianOnline)

    '        If oConnMySql.State = ConnectionState.Closed Then
    '            oConnMySql.Open()
    '        End If

    '        MYSQL = "select * from pegawai where nik = '" & txtVCLAIM_KDDPJP.Text & "' "

    '        oCommMySql.Connection = oConnMySql
    '        oCommMySql.CommandText = MYSQL
    '        oCommMySql.CommandTimeout = 120
    '        oCommMySql.CommandType = CommandType.Text

    '        daMySql = New MySql.Data.MySqlClient.MySqlDataAdapter(oCommMySql)
    '        daMySql.Fill(dsMySql, "jabatan")

    '        If dsMySql.Tables("jabatan").Rows.Count < 1 Then
    '            MYSQL = "insert into "
    '            MYSQL &= "pegawai "
    '            MYSQL &= "( "
    '            MYSQL &= "id "
    '            MYSQL &= ",nik "
    '            MYSQL &= ",nama "
    '            MYSQL &= ",jk "
    '            MYSQL &= ",jbtn "
    '            MYSQL &= ",jnj_jabatan "
    '            MYSQL &= ",kode_kelompok "
    '            MYSQL &= ",kode_resiko "
    '            MYSQL &= ",kode_emergency "
    '            MYSQL &= ",departemen "
    '            MYSQL &= ",bidang "
    '            MYSQL &= ",stts_wp "
    '            MYSQL &= ",stts_kerja "
    '            MYSQL &= ",npwp "
    '            MYSQL &= ",pendidikan "
    '            MYSQL &= ",gapok "
    '            MYSQL &= ",tmp_lahir "
    '            MYSQL &= ",tgl_lahir "
    '            MYSQL &= ",alamat "
    '            MYSQL &= ",kota "
    '            MYSQL &= ",mulai_kerja "
    '            MYSQL &= ",ms_kerja "
    '            MYSQL &= ",indexins "
    '            MYSQL &= ",bpd "
    '            MYSQL &= ",rekening "
    '            MYSQL &= ",stts_aktif "
    '            MYSQL &= ",wajibmasuk "
    '            MYSQL &= ",pengurang "
    '            MYSQL &= ",indek "
    '            MYSQL &= ",mulai_kontrak "
    '            MYSQL &= ",cuti_diambil "
    '            MYSQL &= ",dankes "
    '            MYSQL &= ",photo "
    '            MYSQL &= ",no_ktp "
    '            MYSQL &= ") "
    '            MYSQL &= "VALUES ( "
    '            MYSQL &= "'" & 0 & "' "
    '            MYSQL &= ",'" & txtVCLAIM_KDDPJP.Text & "' "
    '            MYSQL &= ",'" & txtNAME_DISPLAY.Text & "' "
    '            MYSQL &= "," & "NULL" & " "
    '            MYSQL &= "," & "NULL" & " "
    '            MYSQL &= "," & "NULL" & " "
    '            MYSQL &= "," & "NULL" & " "
    '            MYSQL &= "," & "NULL" & " "
    '            MYSQL &= "," & "NULL" & " "
    '            MYSQL &= "," & "NULL" & " "
    '            MYSQL &= "," & "NULL" & " "
    '            MYSQL &= "," & "NULL" & " "
    '            MYSQL &= "," & "NULL" & " "
    '            MYSQL &= "," & "NULL" & " "
    '            MYSQL &= "," & "NULL" & " "
    '            MYSQL &= "," & "NULL" & " "
    '            MYSQL &= "," & "NULL" & " "
    '            MYSQL &= "," & "NULL" & " "
    '            MYSQL &= "," & "NULL" & " "
    '            MYSQL &= "," & "NULL" & " "
    '            MYSQL &= "," & "NULL" & " "
    '            MYSQL &= "," & "NULL" & " "
    '            MYSQL &= "," & "NULL" & " "
    '            MYSQL &= "," & "NULL" & " "
    '            MYSQL &= "," & "NULL" & " "
    '            MYSQL &= "," & "NULL" & " "
    '            MYSQL &= "," & "NULL" & " "
    '            MYSQL &= "," & "NULL" & " "
    '            MYSQL &= "," & "NULL" & " "
    '            MYSQL &= "," & "NULL" & " "
    '            MYSQL &= "," & "NULL" & " "
    '            MYSQL &= "," & "NULL" & " "
    '            MYSQL &= "," & "NULL" & " "
    '            MYSQL &= "," & "NULL" & " "
    '            MYSQL &= ") "

    '            oCommMySql.Connection = oConnMySql
    '            oCommMySql.CommandText = MYSQL
    '            oCommMySql.CommandTimeout = 120
    '            oCommMySql.CommandType = CommandType.Text

    '            daMySql = New MySql.Data.MySqlClient.MySqlDataAdapter(oCommMySql)
    '            daMySql.Fill(dsMySql, "insertjabatan")
    '        Else
    '            MYSQL = "update "
    '            MYSQL &= "pegawai SET "
    '            MYSQL &= "nama = '" & txtNAME_DISPLAY.Text & "' "
    '            MYSQL &= "WHERE nik = '" & txtVCLAIM_KDDPJP.Text & "' "

    '            oCommMySql.Connection = oConnMySql
    '            oCommMySql.CommandText = MYSQL
    '            oCommMySql.CommandTimeout = 120
    '            oCommMySql.CommandType = CommandType.Text

    '            daMySql = New MySql.Data.MySqlClient.MySqlDataAdapter(oCommMySql)
    '            daMySql.Fill(dsMySql, "updatejabatan")
    '        End If

    '        If oConnMySql.State = ConnectionState.Open Then
    '            oConnMySql.Close()
    '        End If

    '        fn_SavePegawai = True
    '    Catch oErr As Exception
    '        MsgBox("Save jabatan Gagal" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Function
    'Private Function fn_SaveDokter() As Boolean
    '    Try
    '        If txtVCLAIM_KDDPJP.Text = "" Then
    '            MsgBox("Kode DPJP Kosong", MsgBoxStyle.Exclamation, Me.Text)
    '            Exit Function
    '        End If

    '        Dim oConnMySql As New MySql.Data.MySqlClient.MySqlConnection
    '        Dim oCommMySql As New MySql.Data.MySqlClient.MySqlCommand
    '        Dim daMySql As MySql.Data.MySqlClient.MySqlDataAdapter
    '        Dim dsMySql As New DataSet
    '        Dim MYSQL As String

    '        dsMySql = New DataSet

    '        oConnMySql = New MySqlConnection(sConnMySqlAntrianOnline)

    '        If oConnMySql.State = ConnectionState.Closed Then
    '            oConnMySql.Open()
    '        End If

    '        MYSQL = "select * from dokter where kd_dokter = '" & txtVCLAIM_KDDPJP.Text & "' "

    '        oCommMySql.Connection = oConnMySql
    '        oCommMySql.CommandText = MYSQL
    '        oCommMySql.CommandTimeout = 120
    '        oCommMySql.CommandType = CommandType.Text

    '        daMySql = New MySql.Data.MySqlClient.MySqlDataAdapter(oCommMySql)
    '        daMySql.Fill(dsMySql, "dokter")

    '        If dsMySql.Tables("dokter").Rows.Count < 1 Then
    '            MYSQL = "insert into "
    '            MYSQL &= "dokter "
    '            MYSQL &= "( "
    '            MYSQL &= "kd_dokter "
    '            MYSQL &= ",nm_dokter "
    '            MYSQL &= ",jk "
    '            MYSQL &= ",tmp_lahir "
    '            MYSQL &= ",tgl_lahir "
    '            MYSQL &= ",gol_drh "
    '            MYSQL &= ",agama "
    '            MYSQL &= ",almt_tgl "
    '            MYSQL &= ",no_telp "
    '            MYSQL &= ",stts_nikah "
    '            MYSQL &= ",kd_sps "
    '            MYSQL &= ",alumni "
    '            MYSQL &= ",no_ijn_praktek "
    '            MYSQL &= ",status "
    '            MYSQL &= ") "
    '            MYSQL &= "VALUES ( "
    '            MYSQL &= "'" & txtVCLAIM_KDDPJP.Text & "' "
    '            MYSQL &= ",'" & txtNAME_DISPLAY.Text & "' "
    '            MYSQL &= "," & "NULL" & " "
    '            MYSQL &= "," & "NULL" & " "
    '            MYSQL &= "," & "NULL" & " "
    '            MYSQL &= "," & "NULL" & " "
    '            MYSQL &= "," & "NULL" & " "
    '            MYSQL &= "," & "NULL" & " "
    '            MYSQL &= "," & "NULL" & " "
    '            MYSQL &= "," & "NULL" & " "
    '            MYSQL &= "," & "NULL" & " "
    '            MYSQL &= "," & "NULL" & " "
    '            MYSQL &= ",'" & txtSIP.Text & "' "
    '            MYSQL &= ",'" & IIf(chkISACTIVE.Checked = True, "1", "0") & "' "
    '            MYSQL &= ") "

    '            oCommMySql.Connection = oConnMySql
    '            oCommMySql.CommandText = MYSQL
    '            oCommMySql.CommandTimeout = 120
    '            oCommMySql.CommandType = CommandType.Text

    '            daMySql = New MySql.Data.MySqlClient.MySqlDataAdapter(oCommMySql)
    '            daMySql.Fill(dsMySql, "insertjabatan")
    '        Else
    '            MYSQL = "update "
    '            MYSQL &= "dokter SET "
    '            MYSQL &= "nm_dokter = '" & txtNAME_DISPLAY.Text & "' "
    '            MYSQL &= ",no_ijn_praktek = '" & txtSIP.Text & "' "
    '            MYSQL &= ",status = '" & IIf(chkISACTIVE.Checked = True, "1", "0") & "' "
    '            MYSQL &= "WHERE kd_dokter = '" & txtVCLAIM_KDDPJP.Text & "' "

    '            oCommMySql.Connection = oConnMySql
    '            oCommMySql.CommandText = MYSQL
    '            oCommMySql.CommandTimeout = 120
    '            oCommMySql.CommandType = CommandType.Text

    '            daMySql = New MySql.Data.MySqlClient.MySqlDataAdapter(oCommMySql)
    '            daMySql.Fill(dsMySql, "updatejabatan")
    '        End If

    '        If oConnMySql.State = ConnectionState.Open Then
    '            oConnMySql.Close()
    '        End If

    '        fn_SaveDokter = True
    '    Catch oErr As Exception
    '        MsgBox("Save dokter Gagal" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Function
    'Private Function fn_SaveJadwalDokterNonJKN(ByVal hari_kerja As String, ByVal jam_mulai As String, ByVal jam_selesai As String, ByVal kuota As Integer) As Boolean
    '    Try
    '        If txtVCLAIM_KDDPJP.Text = "" Then
    '            MsgBox("Kode DPJP Kosong", MsgBoxStyle.Exclamation, Me.Text)
    '            Exit Function
    '        End If

    '        Dim oDepartment As New Reference.clsDepartment
    '        Dim poli As String = String.Empty
    '        Dim dsPoli = oDepartment.GetData(grdKDDEPARTMENT.EditValue)
    '        If dsPoli Is Nothing Then
    '            MsgBox("Poli Kosong", MsgBoxStyle.Exclamation, Me.Text)
    '            Exit Function
    '        End If

    '        Dim oConnMySql As New MySql.Data.MySqlClient.MySqlConnection
    '        Dim oCommMySql As New MySql.Data.MySqlClient.MySqlCommand
    '        Dim daMySql As MySql.Data.MySqlClient.MySqlDataAdapter
    '        Dim dsMySql As New DataSet
    '        Dim MYSQL As String

    '        dsMySql = New DataSet

    '        oConnMySql = New MySqlConnection(sConnMySqlAntrianOnline)

    '        If oConnMySql.State = ConnectionState.Closed Then
    '            oConnMySql.Open()
    '        End If

    '        MYSQL = "select * from jadwal where kd_dokter = '" & txtVCLAIM_KDDPJP.Text & "' and hari_kerja = '" & hari_kerja & "' and jam_mulai = '" & jam_mulai & "' "

    '        oCommMySql.Connection = oConnMySql
    '        oCommMySql.CommandText = MYSQL
    '        oCommMySql.CommandTimeout = 120
    '        oCommMySql.CommandType = CommandType.Text

    '        daMySql = New MySql.Data.MySqlClient.MySqlDataAdapter(oCommMySql)
    '        daMySql.Fill(dsMySql, "jadwal")

    '        If dsMySql.Tables("jadwal").Rows.Count < 1 Then
    '            MYSQL = "insert into "
    '            MYSQL &= "jadwal "
    '            MYSQL &= "( "
    '            MYSQL &= "kd_dokter "
    '            MYSQL &= ",hari_kerja "
    '            MYSQL &= ",jam_mulai "
    '            MYSQL &= ",jam_selesai "
    '            MYSQL &= ",kd_poli "
    '            MYSQL &= ",kuota "
    '            MYSQL &= ") "
    '            MYSQL &= "VALUES ( "
    '            MYSQL &= "'" & txtVCLAIM_KDDPJP.Text & "' "
    '            MYSQL &= ",'" & hari_kerja & "' "
    '            MYSQL &= ",'" & jam_mulai & "' "
    '            MYSQL &= ",'" & jam_selesai & "' "
    '            MYSQL &= ",'" & dsPoli.VCLAIM_KODEPOLI & "' "
    '            MYSQL &= "," & kuota & " "
    '            MYSQL &= ") "

    '            oCommMySql.Connection = oConnMySql
    '            oCommMySql.CommandText = MYSQL
    '            oCommMySql.CommandTimeout = 120
    '            oCommMySql.CommandType = CommandType.Text

    '            daMySql = New MySql.Data.MySqlClient.MySqlDataAdapter(oCommMySql)
    '            daMySql.Fill(dsMySql, "insertjadwal")
    '        Else
    '            MYSQL = "update "
    '            MYSQL &= "dokter SET "
    '            MYSQL &= "nm_dokter = '" & txtNAME_DISPLAY.Text & "' "
    '            MYSQL &= ",no_ijn_praktek = '" & txtSIP.Text & "' "
    '            MYSQL &= ",status = '" & IIf(chkISACTIVE.Checked = True, "1", "0") & "' "
    '            MYSQL &= "WHERE kd_dokter = '" & txtVCLAIM_KDDPJP.Text & "' "

    '            oCommMySql.Connection = oConnMySql
    '            oCommMySql.CommandText = MYSQL
    '            oCommMySql.CommandTimeout = 120
    '            oCommMySql.CommandType = CommandType.Text

    '            daMySql = New MySql.Data.MySqlClient.MySqlDataAdapter(oCommMySql)
    '            daMySql.Fill(dsMySql, "updatejadwal")
    '        End If

    '        If oConnMySql.State = ConnectionState.Open Then
    '            oConnMySql.Close()
    '        End If

    '        fn_SaveJadwalDokterNonJKN = True
    '    Catch oErr As Exception
    '        MsgBox("Save jadwal Gagal" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Function
    'Private Function fn_SaveJadwalDokterNonJKNHapus(ByVal hari_kerja As String, ByVal jam_mulai As String) As Boolean
    '    Try
    '        If txtVCLAIM_KDDPJP.Text = "" Then
    '            MsgBox("Kode DPJP Kosong", MsgBoxStyle.Exclamation, Me.Text)
    '            Exit Function
    '        End If

    '        Dim oDepartment As New Reference.clsDepartment
    '        Dim poli As String = String.Empty
    '        Dim dsPoli = oDepartment.GetData(grdKDDEPARTMENT.EditValue)
    '        If dsPoli Is Nothing Then
    '            MsgBox("Poli Kosong", MsgBoxStyle.Exclamation, Me.Text)
    '            Exit Function
    '        End If

    '        Dim oConnMySql As New MySql.Data.MySqlClient.MySqlConnection
    '        Dim oCommMySql As New MySql.Data.MySqlClient.MySqlCommand
    '        Dim daMySql As MySql.Data.MySqlClient.MySqlDataAdapter
    '        Dim dsMySql As New DataSet
    '        Dim MYSQL As String

    '        dsMySql = New DataSet

    '        oConnMySql = New MySqlConnection(sConnMySqlAntrianOnline)

    '        If oConnMySql.State = ConnectionState.Closed Then
    '            oConnMySql.Open()
    '        End If

    '        MYSQL = "delete from jadwal "
    '        MYSQL &= "WHERE kd_dokter = '" & txtVCLAIM_KDDPJP.Text & "' "
    '        MYSQL &= "and hari_kerja = '" & hari_kerja & "' "
    '        MYSQL &= "and jam_mulai = '" & jam_mulai & "' "

    '        oCommMySql.Connection = oConnMySql
    '        oCommMySql.CommandText = MYSQL
    '        oCommMySql.CommandTimeout = 120
    '        oCommMySql.CommandType = CommandType.Text

    '        daMySql = New MySql.Data.MySqlClient.MySqlDataAdapter(oCommMySql)
    '        daMySql.Fill(dsMySql, "hapus")

    '        If oConnMySql.State = ConnectionState.Open Then
    '            oConnMySql.Close()
    '        End If

    '        fn_SaveJadwalDokterNonJKNHapus = True
    '    Catch oErr As Exception
    '        MsgBox("Save hapus jadwal Gagal" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Function
    Private Sub DeleteToolStripMenuItemDoctor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItemDoctor.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail_DOCTOR.DeleteSelectedRows()
    End Sub
    Private Sub DeleteToolStripMenuItemDepartment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItemDepartment.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail_DEPARTMENT.DeleteSelectedRows()
    End Sub
    Private Sub DeleteToolStripMenuItemDoctorBPJS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItemDoctorBPJS.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDOCTORBPJS.DeleteSelectedRows()
    End Sub
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
        End Select
    End Sub
    Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
            sStatusSave = "NEW"
            Me.Close()
        End If
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadKDSPESIALISTIK()
        Dim oSPESIALISTIK As New Reference.clsSpesialistik
        Try
            grdKDSPESIALISTIK.Properties.DataSource = oSPESIALISTIK.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDSPESIALISTIK.Properties.ValueMember = "KDSPESIALISTIK"
            grdKDSPESIALISTIK.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDCOA()
        Dim oCOA As New Accounting.clsCOA
        Try
            grdKDCOA.Properties.DataSource = oCOA.GetData.Where(Function(x) x.TYPE = 1).ToList()
            grdKDCOA.Properties.ValueMember = "KDCOA"
            grdKDCOA.Properties.DisplayMember = "NMCOA"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDCOA_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdKDCOA.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdKDCOA.ResetText()
        End If
    End Sub
    Private Sub fn_LoadDOCTOR()
        Dim oDOCTOR As New Reference.clsDoctor
        Try
            Dim dsDOCTOR = From x In oDOCTOR.GetData.Where(Function(x) x.ISACTIVE = True)
                           Select KDDOCTOR_ = x.KDDOCTOR, x.NAME_DISPLAY

            grdKDDOCTOR.DataSource = dsDOCTOR.ToList()

            grdKDDOCTOR.ValueMember = "KDDOCTOR_"
            grdKDDOCTOR.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDEPARMENT()
        Dim oDEPARMENT As New Reference.clsDepartment
        Try
            grdKDDEPARMENT.DataSource = oDEPARMENT.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDEPARMENT.ValueMember = "KDDEPARTMENT"
            grdKDDEPARMENT.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDEPARMENT_()
        Dim oDEPARMENT As New Reference.clsDepartment
        Try
            grdKDDEPARTMENT.Properties.DataSource = oDEPARMENT.GetData.Where(Function(x) x.ISACTIVE = True And x.VCLAIM_KODEPOLI <> "" Or x.ISACTIVE = True And x.KDDEPARTMENT = "0").ToList()
            grdKDDEPARTMENT.Properties.ValueMember = "KDDEPARTMENT"
            grdKDDEPARTMENT.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnCARI_Click_1(sender As Object, e As EventArgs) Handles btnCARI.Click
        Try
            If grdKDDEPARTMENT.Text = String.Empty Then
                Exit Sub
            End If

            Dim oDepartment As New Reference.clsDepartment
            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim uTime As Integer = 0

            If sVclaim_ConsId <> "" Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                Dim dsSetKoneksi = oSetKoneksi.GetDataVClaimReferensiDokterDPJP(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, IIf(rbJENISPELAYANAN.SelectedIndex = 0, 2, 1), Now.ToString("yyyy-MM-dd"), oDepartment.GetData(grdKDDEPARTMENT.EditValue).VCLAIM_KODEPOLI)
                'Dim dsSetKoneksi = oSetKoneksi.GetDataVClaimReferensiDokterDPJP("https://apijkn-dev.bpjs-kesehatan.go.id/vclaim-rest-dev/", "13973", "rsdust1r4", "6d8412dbc9eb816119015a4676b900ba", uTime, IIf(rbJENISPELAYANAN.SelectedIndex = 0, 2, 1), Now.ToString("yyyy-MM-dd"), oDepartment.GetData(grdKDDEPARTMENT.EditValue).VCLAIM_KODEPOLI)

                If dsSetKoneksi <> "" Then
                    Try
                        Dim allData = JObject.Parse(dsSetKoneksi)

                        Dim table As DataTable

                        table = New DataTable("M_TABEL")
                        table.Columns.Add("kode")
                        table.Columns.Add("nama")

                        Dim dsData = oSetKoneksi.Decrypt(allData("response"), sVclaim_ConsId & sVclaim_SecreatKey & uTime)
                        'Dim dsData = oSetKoneksi.Decrypt(allData("response"), "13973" & "rsdust1r4" & uTime)
                        Dim ds = JObject.Parse(dsData)

                        For Each item In ds("list")
                            table.Rows.Add(New String() {item("kode"), item("nama")})
                        Next

                        grdCARI.Properties.DataSource = table
                        grdCARI.Properties.ValueMember = "kode"
                        grdCARI.Properties.DisplayMember = "nama"

                        grdCARI.ShowPopup()

                    Catch oErr As Exception
                        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message & vbCrLf & vbCrLf & "Result : " & vbCrLf & dsSetKoneksi, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                End If
            Else
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    'Private Sub grdCARI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdCARI.KeyPress
    '    If Asc(e.KeyChar) = 13 Then
    '        txtVCLAIM_KDDPJP.Text = grdCARI.EditValue
    '        txtNAME_DISPLAY.Text = grdCARI.Text
    '    End If
    'End Sub
    Private Sub grdCARI_EditValueChanged(sender As Object, e As EventArgs) Handles grdCARI.EditValueChanged
        txtVCLAIM_KDDPJP.Text = grdCARI.EditValue
        txtNAME_DISPLAY.Text = grdCARI.Text
    End Sub
    Private Sub txtCARI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCARI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Try
                Dim oSetKoneksi As New Brigging.clsSetKoneksi
                Dim uTime As Integer = 0

                If sVclaim_ConsId <> "" Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                    Dim dsSetKoneksi = oSetKoneksi.GetDataVClaimReferensiDokter(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, txtVCLAIM_KDDOCTOR.Text)

                    If dsSetKoneksi <> "" Then
                        Dim allData = JObject.Parse(dsSetKoneksi)

                        Dim CodeResponse As String = String.Empty
                        Dim messageResponse As String = String.Empty

                        CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                        messageResponse = allData("metaData")("message").ToString

                        If CodeResponse = "200" Then
                            'txtMEMO.Text = oSetKoneksi.Decrypt(allData("response"), sVclaim_ConsId & sVclaim_SecreatKey & uTime)
                            MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Information, Me.Text)
                        Else
                            'txtMEMO.ResetText()
                            MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Information, Me.Text)
                        End If
                    Else
                        MsgBox("Koneksi Referensi Dokter Kosong", MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
                End If

                txtVCLAIM_KDDOCTOR.ResetText()

            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try

        End If
    End Sub
    Private Sub HapusOnlineUmumToolStripMenuItem_Click(sender As Object, e As EventArgs)
        If grvDetail.GetFocusedRowCellValue("HARI") Is Nothing Then
            Exit Sub
        End If

        'fn_SaveJadwalDokterNonJKNHapus(grvDetail.GetFocusedRowCellValue("HARI"), grvDetail.GetFocusedRowCellValue("BUKA") & ":00")
    End Sub
#End Region
End Class