Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq
Imports MySql.Data.MySqlClient
Imports System.Text.RegularExpressions
Imports System
Imports System.Text
Imports System.Runtime.InteropServices
Imports System.Data.SqlClient

Public Class frmDepartment
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oDepartment As New Reference.clsDepartment
    Private oDepartmentSatuSehat As New Reference.clsDepartmentSatuSehat
    Private oDepartmentLocationSatuSehat As New Reference.clsDepartmentLocationSatuSehat

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, Optional ByVal NoId As String = "")
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
            Me.Text = Department.TITLE

            lVCLAIM_KODEPOLI.Text = Department.VCLAIM_KODEPOLI
            lNAME_DISPLAY.Text = Department.NAME_DISPLAY & " *"
            chkISACTIVE.Text = Department.ISACTIVE

            lPHONE.Text = Department.PHONE
            lFAX.Text = Department.FAX
            lMOBILE.Text = Department.MOBILE
            lOTHER.Text = Department.OTHER
            lEMAIL.Text = Department.EMAIL
            lWEBSITE.Text = Department.WEBSITE

            lBILL_STREET.Text = Department.BILL_STREET
            lBILL_CITY.Text = Department.BILL_CITY
            lBILL_STATE.Text = Department.BILL_STATE
            lBILL_ZIP.Text = Department.BILL_ZIP
            lBILL_COUNTRY.Text = Department.BILL_COUNTRY

            lKDCOA.Text = Department.KDCOA & " *"

            lKDKELASRAWAT.Text = Department.KDKELASRAWAT & " *"

            tab1.Text = Department.TAB_CONTACT
            tab2.Text = Department.TAB_BILL
            tab3.Text = Department.TAB_ACCOUNT
            tab4.Text = Department.TAB_OTHER

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
        fn_LoadKDCOA()
        fn_LoadKDKELASRAWAT()
        fn_LoadORGANIZATION()
        'fn_LoadDataOnline()

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

        txtKDDEPARTMENT.Text = sNoId

        Dim dsSatuSehat = oDepartmentSatuSehat.GetDataSatuSehatOrganisasiByKddepartment(sNoId)
        If dsSatuSehat IsNot Nothing Then
            txtOrganization.Text = dsSatuSehat.IDSATUSEHAT
            txtOrganization_Name.Text = dsSatuSehat.NAME_DISPLAY
        End If
        Dim dsLocationSatuSehat = oDepartmentLocationSatuSehat.GetData(sNoId)
        If dsLocationSatuSehat IsNot Nothing Then
            txtIDLOCATIONSATUSEHAT.Text = dsLocationSatuSehat.IDSATUSEHAT
            'txtlongitude.Text = SatusehatAuth.GetToken(Regex.Replace(dsLocationSatuSehat.RESPON, "^.{4}", ""), "longitude")
            'txtlatitude2.Text = SatusehatAuth.GetToken(Regex.Replace(dsLocationSatuSehat.RESPON, "^.{4}", ""), "latitude")
        End If
    End Sub
    Private Sub fn_ViewMode(ByVal Status As Boolean)
        btnSaveNew.Enabled = Not Status
        btnSaveClose.Enabled = Not Status
        btnRuanganBaru.Enabled = Not Status
        btnHapusRuangan.Enabled = Not Status

        txtVCLAIM_KODEPOLI.Properties.ReadOnly = Status
        txtNAME_DISPLAY.Properties.ReadOnly = Status
        chkISACTIVE.Properties.ReadOnly = Status
        chkRUANGRAWAT.Properties.ReadOnly = Status
        txtKAPASITAS.Properties.ReadOnly = Status
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

        txtMEMO.Properties.ReadOnly = Status

        chkISEKSEKUTIF.Properties.ReadOnly = Status
        chkISKATARAK.Properties.ReadOnly = Status
        txtANTRIAN.Properties.ReadOnly = Status
        txtNOMOR.Properties.ReadOnly = Status
        txtNAME_ONLINE.Properties.ReadOnly = Status

        grdKDCOA.Properties.ReadOnly = Status
        grdKDKELASRAWAT.Properties.ReadOnly = Status
        txtKAPASITAS_ONLINE.Properties.ReadOnly = Status

        txtKDDEPARTMENT.Properties.ReadOnly = True
    End Sub
    Private Sub fn_EmptyMe()
        txtVCLAIM_KODEPOLI.ResetText()
        txtNAME_DISPLAY.ResetText()
        txtNOMOR.Text = "0"
        txtKAPASITAS.Text = "0"
        txtKAPASITAS_ONLINE.Text = "0"

        chkISACTIVE.Checked = True
        chkRUANGRAWAT.Checked = False

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

        Try
            grdKDCOA.Text = oDepartment.AccountDefault
            grdKDKELASRAWAT.Text = oDepartment.AccountDefaultKelas
        Catch oErr As Exception

        End Try
        txtANTRIAN.ResetText()
        txtNAME_ONLINE.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oDepartment.GetData(sNoId)

            With ds
                txtKAPASITAS_ONLINE.Text = .KAPASITAS_ONLINE
                txtNOMOR.Text = .NOMOR
                txtKAPASITAS.Text = .KAPASITAS
                txtVCLAIM_KODEPOLI.Text = .VCLAIM_KODEPOLI
                txtNAME_DISPLAY.Text = .NAME_DISPLAY
                chkISACTIVE.Checked = .ISACTIVE
                chkRUANGRAWAT.Checked = .ISRUANGRAWAT

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

                grdKDKELASRAWAT.Text = .KDKELASRAWAT

                chkISEKSEKUTIF.Checked = .ISEKSEKUTIF
                chkISKATARAK.Checked = .ISKATARAK

                txtANTRIAN.Text = .ANTRIAN
                txtNAME_ONLINE.Text = .NAME_ONLINE

                tabControl.SelectedTabPage = tab4
                tabControl.SelectedTabPage = tab3
                tabControl.SelectedTabPage = tab2
                tabControl.SelectedTabPage = tab1

            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtNAME_DISPLAY.Text = String.Empty Then
                txtNAME_DISPLAY.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtNAME_DISPLAY.ErrorText = Statement.ErrorRequired

                txtNAME_DISPLAY.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDKELASRAWAT.Text = String.Empty Then
                grdKDKELASRAWAT.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDKELASRAWAT.ErrorText = Statement.ErrorRequired

                grdKDKELASRAWAT.Focus()
                fn_Validate = False
                Exit Function
            End If

            If txtPHONE.Text = String.Empty Then
                txtPHONE.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtPHONE.ErrorText = Statement.ErrorRequired

                txtPHONE.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtEMAIL.Text = String.Empty Then
                txtEMAIL.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtEMAIL.ErrorText = Statement.ErrorRequired

                txtEMAIL.Focus()
                fn_Validate = False
                Exit Function
            End If
            'If txtWEBSITE.Text = String.Empty Then
            '    txtWEBSITE.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '    txtWEBSITE.ErrorText = Statement.ErrorRequired

            '    txtWEBSITE.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If
            If txtBILL_STREET.Text = String.Empty Then
                txtBILL_STREET.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtBILL_STREET.ErrorText = Statement.ErrorRequired

                txtBILL_STREET.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtBILL_ZIP.Text = String.Empty Then
                txtBILL_ZIP.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtBILL_ZIP.ErrorText = Statement.ErrorRequired

                txtBILL_ZIP.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtBILL_CITY.Text = String.Empty Then
                txtBILL_CITY.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtBILL_CITY.ErrorText = Statement.ErrorRequired

                txtBILL_CITY.Focus()
                fn_Validate = False
                Exit Function
            End If

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                If oDepartment.IsExist(txtNAME_DISPLAY.Text.ToUpper.Trim) = True Then
                    txtNAME_DISPLAY.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    txtNAME_DISPLAY.ErrorText = Statement.ErrorRegistered

                    txtNAME_DISPLAY.Focus()
                    fn_Validate = False
                    Exit Function
                End If
            Else
                If txtNAME_DISPLAY.Text.Trim <> oDepartment.GetData(sNoId).NAME_DISPLAY Then
                    If oDepartment.IsExist(txtNAME_DISPLAY.Text.ToUpper.Trim) = True Then
                        txtNAME_DISPLAY.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                        txtNAME_DISPLAY.ErrorText = Statement.ErrorRegistered

                        txtNAME_DISPLAY.Focus()
                        fn_Validate = False
                        Exit Function
                    End If
                End If
            End If
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
            Dim ds = oDepartment.GetStructureHeader
            With ds
                .NOMOR = txtNOMOR.Text
                .KAPASITAS = txtKAPASITAS.Text

                Try
                    .DATECREATED = oDepartment.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try

                .DATEUPDATED = Now
                .ISRUANGRAWAT = chkRUANGRAWAT.Checked
                .KDDEPARTMENT = sNoId
                .VCLAIM_KODEPOLI = txtVCLAIM_KODEPOLI.Text
                .NAME_DISPLAY = txtNAME_DISPLAY.Text.Trim
                .PHONE = txtPHONE.Text
                .FAX = txtFAX.Text
                .MOBILE = txtMOBILE.Text
                .EMAIL = txtEMAIL.Text
                .OTHER = txtOTHER.Text
                .WEBSITE = txtWEBSITE.Text
                .BILL_STREET = txtBILL_STREET.Text
                .BILL_CITY = txtBILL_CITY.Text
                .BILL_STATE = txtBILL_STATE.Text
                .BILL_ZIP = txtBILL_ZIP.Text
                .BILL_COUNTRY = txtBILL_COUNTRY.Text
                .MEMO = txtMEMO.Text
                .KDCOA = IIf(String.IsNullOrEmpty(grdKDCOA.EditValue), String.Empty, grdKDCOA.EditValue)
                .ISACTIVE = chkISACTIVE.Checked
                .ISEKSEKUTIF = chkISEKSEKUTIF.Checked
                .ISKATARAK = chkISKATARAK.Checked
                .KDKELASRAWAT = grdKDKELASRAWAT.EditValue
                .ANTRIAN = txtANTRIAN.Text.ToString.Trim
                .NAME_ONLINE = txtNAME_ONLINE.Text
                .KAPASITAS_ONLINE = CInt(txtKAPASITAS_ONLINE.Text)
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oDepartment.InsertData(ds, "")
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oDepartment.UpdateData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

            Try
                If fn_Save = True Then
                    Dim dsLocationSatuSehat = oDepartmentLocationSatuSehat.GetData(ds.KDDEPARTMENT)
                    If dsLocationSatuSehat Is Nothing Then
                        If txtIDLOCATIONSATUSEHAT.Text = "" Then
                            If MsgBox("APAKAH YAKIN AKAN MEMBUAT ID LOCATION SATU SEHAT YANG BARU????", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.Yes Then
                                If txtMEMO.Text = "" Then
                                    txtMEMO.Focus()
                                    MsgBox("Memo kosong", MsgBoxStyle.Exclamation, Me.Text)
                                    Exit Function
                                End If
                                If txtlatitude2.Text = "" Then
                                    txtlatitude2.Focus()
                                    MsgBox("latitude kosong", MsgBoxStyle.Exclamation, Me.Text)
                                    Exit Function
                                End If
                                If txtlongitude.Text = "" Then
                                    txtlongitude.Focus()
                                    MsgBox("longitude kosong", MsgBoxStyle.Exclamation, Me.Text)
                                    Exit Function
                                End If
                                If txtOrganization.Text = "" Then
                                    txtlongitude.Focus()
                                    MsgBox("Id Organisation kosong", MsgBoxStyle.Exclamation, Me.Text)
                                    Exit Function
                                End If

                                If SatuSehat_Organisasi <> "" Then
                                    Dim jsondata As String = BuildLocationJSON(txtNAME_DISPLAY.Text, txtMEMO.Text, txtPHONE.Text, txtWEBSITE.Text, txtEMAIL.Text, txtOrganization.Text, txtlongitude.Text, txtlatitude2.Text)

                                    Dim respon As String = SatusehatAuth.LocationCreatePoliRuang(SatuSehat_Production, SatuSehat_token, jsondata)

                                    If Not String.IsNullOrEmpty(respon) Then
                                        'MsgBox("✓ Respon satu sehat berhasil didapatkan!" & vbCrLf & respon, MsgBoxStyle.Information, Me.Text)

                                        If respon.Contains("Exception") Then
                                            Dim frmPesanSatuSehat As New frmPesanSatuSehat
                                            frmPesanSatuSehat.fn_LoadJson("✗ Exception!" & vbCrLf & respon, "Exception")
                                            frmPesanSatuSehat.ShowDialog(Me)
                                        Else
                                            If respon.Contains("200") Then
                                                Dim ID As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "id")

                                                txtIDLOCATIONSATUSEHAT.Text = ID

                                                If ID = "" Then
                                                    Dim frmPesanSatuSehat As New frmPesanSatuSehat
                                                    frmPesanSatuSehat.fn_LoadJson(Regex.Replace(respon, "^.{4}", ""), respon.Substring(0, 3))
                                                    frmPesanSatuSehat.ShowDialog(Me)
                                                Else
                                                    If fn_SaveLocationSatuSehat(True, ds.KDDEPARTMENT, ID, jsondata, Regex.Replace(respon, "^.{4}", "")) = True Then
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
                                                            Dim responulang As String = SatusehatAuth.LocationCreatePoliRuang(SatuSehat_Production, SatuSehat_token, jsondata)

                                                            Dim ID As String = SatusehatAuth.GetToken(Regex.Replace(responulang, "^.{4}", ""), "id")

                                                            txtIDLOCATIONSATUSEHAT.Text = ID

                                                            If ID = "" Then
                                                                Dim frmPesanSatuSehat As New frmPesanSatuSehat
                                                                frmPesanSatuSehat.fn_LoadJson(Regex.Replace(responulang, "^.{4}", ""), responulang.Substring(0, 3))
                                                                frmPesanSatuSehat.ShowDialog(Me)
                                                            Else
                                                                If fn_SaveLocationSatuSehat(True, ds.KDDEPARTMENT, ID, jsondata, Regex.Replace(responulang, "^.{4}", "")) = True Then
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
                                                    Dim ID As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "id")

                                                    txtIDLOCATIONSATUSEHAT.Text = ID

                                                    If ID = "" Then
                                                        Dim frmPesanSatuSehat As New frmPesanSatuSehat
                                                        frmPesanSatuSehat.fn_LoadJson(Regex.Replace(respon, "^.{4}", ""), respon.Substring(0, 3))
                                                        frmPesanSatuSehat.ShowDialog(Me)
                                                    Else
                                                        If fn_SaveLocationSatuSehat(True, ds.KDDEPARTMENT, ID, jsondata, Regex.Replace(respon, "^.{4}", "")) = True Then
                                                            Dim frmPesanSatuSehat As New frmPesanSatuSehat
                                                            frmPesanSatuSehat.fn_LoadJson(Regex.Replace(respon, "^.{4}", ""), respon.Substring(0, 3))
                                                            frmPesanSatuSehat.ShowDialog(Me)
                                                        Else
                                                            MsgBox("✗ Gagal simpan database!" & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)
                                                        End If
                                                    End If
                                                    'Dim frmPesanSatuSehat As New frmPesanSatuSehat
                                                    'frmPesanSatuSehat.fn_LoadJson(Regex.Replace(respon, "^.{4}", ""), respon.Substring(0, 3))
                                                    'frmPesanSatuSehat.ShowDialog(Me)
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
                        Else
                            If fn_SaveLocationSatuSehat(True, ds.KDDEPARTMENT, txtIDLOCATIONSATUSEHAT.Text, "-", "-") = True Then
                                'Dim frmPesanSatuSehat As New frmPesanSatuSehat
                                'frmPesanSatuSehat.fn_LoadJson(Regex.Replace(respon, "^.{4}", ""), respon.Substring(0, 3))
                                'frmPesanSatuSehat.ShowDialog(Me)
                            Else
                                MsgBox("✗ Gagal simpan loaction database!", MsgBoxStyle.Exclamation, Me.Text)
                            End If
                        End If
                    Else
                        If fn_SaveLocationSatuSehat(False, ds.KDDEPARTMENT, txtIDLOCATIONSATUSEHAT.Text, "-", "-") = True Then
                            'Dim frmPesanSatuSehat As New frmPesanSatuSehat
                            'frmPesanSatuSehat.fn_LoadJson(Regex.Replace(respon, "^.{4}", ""), respon.Substring(0, 3))
                            'frmPesanSatuSehat.ShowDialog(Me)
                        Else
                            MsgBox("✗ Gagal simpan loaction database!", MsgBoxStyle.Exclamation, Me.Text)
                        End If
                    End If
                End If
            Catch oErr As Exception
                MsgBox("Save Location" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Function fn_SaveSatuSehat(ByVal isadd As Boolean, ByVal KDDEPARTMENT As String, ByVal IDSATUSEHAT As String, ByVal REQUEST As String, ByVal RESPON As String, ByVal NAME_DISPLAY As String) As Boolean
        Try
            ' ***** HEADER *****

            Dim ds = oDepartmentSatuSehat.GetStructureHeader
            With ds
                .DATECREATED = Now
                .DATEUPDATED = Now
                .KDDEPARTMENT = KDDEPARTMENT
                .IDSATUSEHAT = IDSATUSEHAT
                .REQUEST = REQUEST
                .RESPON = RESPON
                .ISDEFAULT = False
                .ISACTIVE = True
                .NAME_DISPLAY = NAME_DISPLAY
            End With

            If isadd = True Then
                Try
                    fn_SaveSatuSehat = oDepartmentSatuSehat.InsertData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                Try
                    fn_SaveSatuSehat = oDepartmentSatuSehat.UpdateData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveSatuSehat = False
        End Try
    End Function
    Private Function fn_SaveLocationSatuSehat(ByVal isadd As Boolean, ByVal KDDEPARTMENT As String, ByVal IDSATUSEHAT As String, ByVal REQUEST As String, ByVal RESPON As String) As Boolean
        Try
            ' ***** HEADER *****

            Dim ds = oDepartmentLocationSatuSehat.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oDepartmentLocationSatuSehat.GetData(KDDEPARTMENT).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDDEPARTMENT = KDDEPARTMENT
                .IDSATUSEHAT = IDSATUSEHAT
                .REQUEST = REQUEST
                .RESPON = RESPON
                .ISDEFAULT = False
                .ISACTIVE = True
            End With

            If isadd = True Then
                Try
                    fn_SaveLocationSatuSehat = oDepartmentLocationSatuSehat.InsertData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                Try
                    fn_SaveLocationSatuSehat = oDepartmentLocationSatuSehat.UpdateData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveLocationSatuSehat = False
        End Try
    End Function
    Public Shared Function BuildLocationJSON(poli As String, descriptionpoli As String, phone As String, website As String, email As String, orgPoli As String, longitude As String, latitude As String) As String
        Dim sb As New StringBuilder()

        sb.AppendLine("{")
        sb.AppendLine("   ""resourceType"": ""Location"",")
        sb.AppendLine("   ""identifier"": [")
        sb.AppendLine("       {")
        sb.AppendLine("           ""system"": """ & "" & """,")
        sb.AppendLine("           ""value"": """ & "" & """")
        sb.AppendLine("       }")
        sb.AppendLine("   ],")
        sb.AppendLine("   ""status"": ""active"",")
        sb.AppendLine("   ""name"": """ & poli & """,")
        sb.AppendLine("   ""description"": """ & descriptionpoli & """,")
        sb.AppendLine("   ""mode"": ""instance"",")
        sb.AppendLine("   ""telecom"": [")
        sb.AppendLine("        {")
        sb.AppendLine("            ""system"": ""phone"",")
        sb.AppendLine("            ""value"": """ & phone & """,")
        sb.AppendLine("            ""use"": ""work""")
        sb.AppendLine("        },")
        sb.AppendLine("        {")
        sb.AppendLine("            ""system"": ""email"",")
        sb.AppendLine("            ""value"": """ & email & """,")
        sb.AppendLine("            ""use"": ""work""")
        sb.AppendLine("        },")
        sb.AppendLine("        {")
        sb.AppendLine("            ""system"": ""url"",")
        sb.AppendLine("            ""value"": """ & website & """,")
        sb.AppendLine("            ""use"": ""work""")
        sb.AppendLine("        }")
        sb.AppendLine("    ],")
        sb.AppendLine("   ""physicalType"": {")
        sb.AppendLine("       ""coding"": [")
        sb.AppendLine("           {")
        sb.AppendLine("               ""system"": ""http://terminology.hl7.org/CodeSystem/location-physical-type"",")
        sb.AppendLine("               ""code"": ""ro"",")
        sb.AppendLine("               ""display"": ""Room""")
        sb.AppendLine("           }")
        sb.AppendLine("       ]")
        sb.AppendLine("   },")
        sb.AppendLine("   ""position"": {")
        sb.AppendLine("       ""longitude"": " & longitude & ",")
        sb.AppendLine("       ""latitude"": " & latitude & ",")
        sb.AppendLine("       ""altitude"": 0")
        sb.AppendLine("   },")
        sb.AppendLine("   ""managingOrganization"": {")
        sb.AppendLine("       ""reference"": ""Organization/" & orgPoli & """")
        sb.AppendLine("   }")
        sb.AppendLine("}")

        Return sb.ToString()
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
    Private Sub fn_LoadKDKELASRAWAT()
        Dim oKelas As New Reference.clsKelasRawat
        Try
            grdKDKELASRAWAT.Properties.DataSource = oKelas.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDKELASRAWAT.Properties.ValueMember = "KDKELASRAWAT"
            grdKDKELASRAWAT.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadORGANIZATION()
        'Try
        '    grdOrganization.Properties.DataSource = oDepartmentSatuSehat.GetDataList.Where(Function(x) x.ISACTIVE = True).ToList()
        '    grdOrganization.Properties.ValueMember = "IDSATUSEHAT"
        '    grdOrganization.Properties.DisplayMember = "NAME_DISPLAY"
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try

        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "IDSATUSEHAT, NAME_DISPLAY "
            SQL &= "FROM M_DEPARTMENT_SATUSEHAT "
            SQL &= "GROUP BY "
            SQL &= "IDSATUSEHAT, NAME_DISPLAY"

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "M_DEPARTMENT_SATUSEHAT")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grdOrganization.Properties.DataSource = ds.Tables("M_DEPARTMENT_SATUSEHAT")
            grdOrganization.Properties.ValueMember = "IDSATUSEHAT"
            grdOrganization.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDKELASRAWAT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdKDKELASRAWAT.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdKDKELASRAWAT.ResetText()
        End If
    End Sub
    Private Sub txtVCLAIM_KODEPOLI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtVCLAIM_KODEPOLI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If chkRUANGRAWAT.Checked = False Then
                Try
                    Dim oSetKoneksi As New Brigging.clsSetKoneksi
                    Dim uTime As Integer = 0

                    If sVclaim_ConsId <> "" Then
                        uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                        Dim dsSetKoneksi = oSetKoneksi.GetDataVClaimReferensiPoli(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, txtVCLAIM_KODEPOLI.Text)
                        Dim allData = JObject.Parse(dsSetKoneksi)
                        tabControl.SelectedTabPage = tab4
                        txtMEMO.Text = oSetKoneksi.Decrypt(allData("response"), sVclaim_ConsId & sVclaim_SecreatKey & uTime)
                    Else
                        MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                Try
                    Dim oSetKoneksi As New Brigging.clsSetKoneksi

                    Dim uTime As Integer = 0

                    If sVclaim_ConsId <> "" Then
                        uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                        Dim dsSetKoneksi = oSetKoneksi.GetDataVClaimReferensiRuangRawat(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime)
                        Dim allData = JObject.Parse(dsSetKoneksi)
                        tabControl.SelectedTabPage = tab4
                        txtMEMO.Text = oSetKoneksi.Decrypt(allData("response"), sVclaim_ConsId & sVclaim_SecreatKey & uTime)
                        If txtMEMO.Text = "" Then
                            txtMEMO.Text = dsSetKoneksi
                        End If
                    Else
                        MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        End If
    End Sub
    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        Try
            If TextEdit1.Text <> "" Then
                If SatuSehat_Organisasi <> "" Then
                    Dim respon As String = SatusehatAuth.LocationSearchbyName(SatuSehat_Production, SatuSehat_token, TextEdit1.Text)

                    If Not String.IsNullOrEmpty(respon) Then
                        Dim frmPesanSatuSehat As New frmPesanSatuSehat
                        frmPesanSatuSehat.fn_LoadJson(Regex.Replace(respon, "^.{4}", ""), respon.Substring(0, 3))
                        frmPesanSatuSehat.ShowDialog(Me)
                    Else
                        MsgBox("✗ Gagal mendapatkan respon. respon kosong", MsgBoxStyle.Information, Me.Text)
                    End If
                Else
                    MsgBox("Organisasi ID masih kosong", MsgBoxStyle.Information, Me.Text)
                End If
            Else
                MsgBox("Cari Satu Sehat Masih Kosong", MsgBoxStyle.Information, Me.Text)
                TextEdit1.Focus()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub BarButtonItem2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        Try
            If TextEdit1.Text <> "" Then
                If SatuSehat_Organisasi <> "" Then
                    Dim respon As String = SatusehatAuth.LocationSearchbyOrgID(SatuSehat_Production, SatuSehat_token, TextEdit1.Text)

                    If Not String.IsNullOrEmpty(respon) Then
                        Dim frmPesanSatuSehat As New frmPesanSatuSehat
                        frmPesanSatuSehat.fn_LoadJson(Regex.Replace(respon, "^.{4}", ""), respon.Substring(0, 3))
                        frmPesanSatuSehat.ShowDialog(Me)
                    Else
                        MsgBox("✗ Gagal mendapatkan respon. respon kosong", MsgBoxStyle.Information, Me.Text)
                    End If
                Else
                    MsgBox("Organisasi ID masih kosong", MsgBoxStyle.Information, Me.Text)
                End If
            Else
                MsgBox("Cari Satu Sehat Masih Kosong", MsgBoxStyle.Information, Me.Text)
                TextEdit1.Focus()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub BarButtonItem4_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem4.ItemClick
        Try
            If TextEdit1.Text <> "" Then
                If SatuSehat_Organisasi <> "" Then
                    Dim respon As String = SatusehatAuth.OrganizationSearchbyName(SatuSehat_Production, SatuSehat_token, TextEdit1.Text)

                    If Not String.IsNullOrEmpty(respon) Then
                        Dim frmPesanSatuSehat As New frmPesanSatuSehat
                        frmPesanSatuSehat.fn_LoadJson(Regex.Replace(respon, "^.{4}", ""), respon.Substring(0, 3))
                        frmPesanSatuSehat.ShowDialog(Me)
                    Else
                        MsgBox("✗ Gagal mendapatkan respon. respon kosong", MsgBoxStyle.Information, Me.Text)
                    End If
                Else
                    MsgBox("Organisasi ID masih kosong", MsgBoxStyle.Information, Me.Text)
                End If
            Else
                MsgBox("Cari Satu Sehat Masih Kosong", MsgBoxStyle.Information, Me.Text)
                TextEdit1.Focus()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub BarButtonItem5_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem5.ItemClick
        Try
            If TextEdit1.Text <> "" Then
                If SatuSehat_Organisasi <> "" Then
                    Dim respon As String = SatusehatAuth.OrganizationByID(SatuSehat_Production, SatuSehat_token, TextEdit1.Text)

                    If Not String.IsNullOrEmpty(respon) Then
                        Dim frmPesanSatuSehat As New frmPesanSatuSehat
                        frmPesanSatuSehat.fn_LoadJson(Regex.Replace(respon, "^.{4}", ""), respon.Substring(0, 3))
                        frmPesanSatuSehat.ShowDialog(Me)
                    Else
                        MsgBox("✗ Gagal mendapatkan respon. respon kosong", MsgBoxStyle.Information, Me.Text)
                    End If
                Else
                    MsgBox("Organisasi ID masih kosong", MsgBoxStyle.Information, Me.Text)
                End If
            Else
                MsgBox("Cari Satu Sehat Masih Kosong", MsgBoxStyle.Information, Me.Text)
                TextEdit1.Focus()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnCariWilayah_Click(sender As Object, e As EventArgs) Handles btnCariWilayah.Click
        Dim frmBrowseWilayah As New frmBrowseWilayah
        Try
            frmBrowseWilayah.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmBrowseWilayah Is Nothing Then frmBrowseWilayah.Dispose()
            frmBrowseWilayah = Nothing

            If sFind1 <> "" Then
                fn_LoadCariPropinsi(sFind1)
            End If

        End Try
    End Sub
    Private Sub fn_LoadCariPropinsi(ByVal kdkulrahan As String)
        Dim oKelurahan As New Reference.clsKelurahan

        Dim dsKelurahan = oKelurahan.GetData(kdkulrahan)

        If dsKelurahan IsNot Nothing Then
            txtKDKELURAHAN.Text = dsKelurahan.KDKELURAHAN
            'txtKELURAHAN.Text = dsKelurahan.MEMO
            txtKDKECAMATAN.Text = dsKelurahan.M_KECAMATAN.KDKECAMATAN
            txtKDKABUPATEN.Text = dsKelurahan.M_KECAMATAN.M_KABUPATEN.KDKABUPATEN
            txtKDPROPINSI.Text = dsKelurahan.M_KECAMATAN.M_KABUPATEN.M_PROPINSI.KDPROPINSI
            'txtBILL_ZIP.Text = dsKelurahan.KODEPOS
        Else
            txtKDKELURAHAN.ResetText()
            'txtKELURAHAN.ResetText()
            txtKDKECAMATAN.ResetText()
            txtKDKABUPATEN.ResetText()
            txtKDPROPINSI.ResetText()
            'txtBILL_ZIP.ResetText()
        End If
    End Sub

    Private Sub BarButtonItem6_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem6.ItemClick
        Try
            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                MsgBox("Id Department Masih Kosong, Silahkan Simpan Terlebih dahulu Poli", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If
            If TextEdit1.Text = "" Then
                MsgBox("Masukan Nama Yang Akan diBuat", MsgBoxStyle.Information, Me.Text)
                TextEdit1.Focus()
                Exit Sub
            End If

            If MsgBox("APAKAH YAKIN AKAN MEMBUAT ID POLI SATU SEHAT YANG BARU????", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.Yes Then
                Dim dsCek = oDepartmentSatuSehat.GetDataSatuSehatOrganisasiByKddepartment(sNoId)

                If dsCek IsNot Nothing Then
                    MsgBox("Kode Department Sudah Masuk Tidak Bisa Double", MsgBoxStyle.Information, Me.Text)
                    Exit Sub
                End If

                If SatuSehat_Organisasi <> "" Then
                    Dim oKelurahan As New Reference.clsKelurahan

                    Dim dsKelurahan = oKelurahan.GetData(txtKDKELURAHAN.Text)

                    If dsKelurahan Is Nothing Then
                        MsgBox("Kode kelurahan kosong", MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                    End If

                    Dim jsondata As String = SatusehatAuth.BuildOrganizationJSON(SatuSehat_Organisasi, sCompany, TextEdit1.Text, txtPHONE.Text, txtEMAIL.Text, txtWEBSITE.Text, txtBILL_STREET.Text, txtBILL_CITY.Text, txtBILL_ZIP.Text, txtKDPROPINSI.Text, txtKDKABUPATEN.Text, txtKDKECAMATAN.Text, txtKDKELURAHAN.Text)

                    Dim respon As String = SatusehatAuth.OrganizationCreatePoliOrg(SatuSehat_Production, SatuSehat_token, jsondata)

                    If Not String.IsNullOrEmpty(respon) Then
                        'MsgBox("✓ Respon satu sehat berhasil didapatkan!" & vbCrLf & respon, MsgBoxStyle.Information, Me.Text)

                        If respon.Contains("Exception") Then
                            Dim frmPesanSatuSehat As New frmPesanSatuSehat
                            frmPesanSatuSehat.fn_LoadJson("✗ Exception!" & vbCrLf & respon, "Exception")
                            frmPesanSatuSehat.ShowDialog(Me)
                        Else
                            If respon.Contains("200") Then
                                Dim ID As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "id")

                                txtOrganization.Text = ID
                                txtOrganization_Name.Text = TextEdit1.Text

                                If ID = "" Then
                                    Dim frmPesanSatuSehat As New frmPesanSatuSehat
                                    frmPesanSatuSehat.fn_LoadJson(Regex.Replace(respon, "^.{4}", ""), respon.Substring(0, 3))
                                    frmPesanSatuSehat.ShowDialog(Me)
                                Else
                                    If fn_SaveSatuSehat(True, sNoId, ID, jsondata, Regex.Replace(respon, "^.{4}", ""), TextEdit1.Text) = True Then
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
                                            Dim responulang As String = SatusehatAuth.OrganizationCreatePoliOrg(SatuSehat_Production, SatuSehat_token, jsondata)

                                            Dim ID As String = SatusehatAuth.GetToken(Regex.Replace(responulang, "^.{4}", ""), "id")

                                            txtOrganization.Text = ID

                                            If ID = "" Then
                                                Dim frmPesanSatuSehat As New frmPesanSatuSehat
                                                frmPesanSatuSehat.fn_LoadJson(Regex.Replace(responulang, "^.{4}", ""), responulang.Substring(0, 3))
                                                frmPesanSatuSehat.ShowDialog(Me)
                                            Else
                                                If fn_SaveSatuSehat(True, sNoId, ID, jsondata, Regex.Replace(responulang, "^.{4}", ""), TextEdit1.Text) = True Then
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
                                    'jika respon 201

                                    Dim ID As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "id")

                                    txtOrganization.Text = ID
                                    txtOrganization_Name.Text = TextEdit1.Text

                                    If ID = "" Then
                                        Dim frmPesanSatuSehat As New frmPesanSatuSehat
                                        frmPesanSatuSehat.fn_LoadJson(Regex.Replace(respon, "^.{4}", ""), respon.Substring(0, 3))
                                        frmPesanSatuSehat.ShowDialog(Me)
                                    Else
                                        If fn_SaveSatuSehat(True, sNoId, ID, jsondata, Regex.Replace(respon, "^.{4}", ""), TextEdit1.Text) = True Then
                                            Dim frmPesanSatuSehat As New frmPesanSatuSehat
                                            frmPesanSatuSehat.fn_LoadJson(Regex.Replace(respon, "^.{4}", ""), respon.Substring(0, 3))
                                            frmPesanSatuSehat.ShowDialog(Me)
                                        Else
                                            MsgBox("✗ Gagal simpan database!" & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)
                                        End If
                                    End If

                                    'Dim frmPesanSatuSehat As New frmPesanSatuSehat
                                    'frmPesanSatuSehat.fn_LoadJson(Regex.Replace(respon, "^.{4}", ""), respon.Substring(0, 3))
                                    'frmPesanSatuSehat.ShowDialog(Me)
                                End If
                            End If
                        End If

                        fn_LoadORGANIZATION()
                    Else
                        MsgBox("✗ Gagal mendapatkan respon. respon kosong", MsgBoxStyle.Information, Me.Text)
                    End If
                Else
                    MsgBox("Organisasi ID masih kosong", MsgBoxStyle.Information, Me.Text)
                End If
            End If
        Catch oErr As Exception
            MsgBox("Save Organization" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try

    End Sub
    Private Sub btnSimpanOrganization_Click(sender As Object, e As EventArgs) Handles btnSimpanOrganization.Click
        If grdOrganization.Text <> "" Then
            Dim dsCek = oDepartmentSatuSehat.GetDataSatuSehatOrganisasiByKddepartment(sNoId)

            If dsCek IsNot Nothing Then
                If fn_SaveSatuSehat(False, sNoId, grdOrganization.EditValue, dsCek.REQUEST, dsCek.RESPON, grdOrganization.Text) = True Then
                    MsgBox("✗ Berhasil simpan database!", MsgBoxStyle.Exclamation, Me.Text)

                    txtOrganization.Text = grdOrganization.EditValue
                    txtOrganization_Name.Text = grdOrganization.Text
                Else
                    MsgBox("✗ Gagal simpan database!", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                If fn_SaveSatuSehat(True, sNoId, grdOrganization.EditValue, "", "", grdOrganization.Text) = True Then
                    MsgBox("✗ Berhasil simpan database!", MsgBoxStyle.Exclamation, Me.Text)

                    txtOrganization.Text = grdOrganization.EditValue
                    txtOrganization_Name.Text = grdOrganization.Text
                Else
                    MsgBox("✗ Gagal simpan database!", MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If
        Else
            MsgBox("Silahkan Pilih Cari Organisasi ID terlebih dahulu", MsgBoxStyle.Information, Me.Text)
            grdOrganization.Focus()
        End If
    End Sub
#End Region
End Class