Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq

Public Class frmRujukan
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oRujukan As New Admission.clsRujukan
    Private PopUP As Boolean = False
    Private sKDPENDFTARAN As String = String.Empty
    Private stipeRujukan As Integer = 0
#End Region
#Region "Function"
    Public Sub LoadMeRegister(ByVal Paramater As String, ByVal tipeRujukan As Integer)
        sKDPENDFTARAN = Paramater
        stipeRujukan = tipeRujukan
    End Sub
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
            Me.Text = Rujukan.TITLE

            lKDRUJUKAN.Text = Rujukan.KDRUJUKAN
            lKDPENDAFATRAN.Text = Rujukan.KDPENDAFTARAN & " *"
            lDATE.Text = "Tanggal Rujukan"
            lRENCANATGLKUNJUNGAN.Text = "Tgl Rencana Kunjungan"
            lKDPPK.Text = Rujukan.KDPPK
            lCATATAN.Text = Rujukan.CATATAN
            lKDDEPARTMENT.Text = Rujukan.KDDEPARTMENT
            lKDDIAGNOSA.Text = Rujukan.KDDIAGNOSA
            lTIPERUJUKAN.Text = Rujukan.TIPERUJUKAN
            lNOMORSEP.Text = Rujukan.NOMORSEP
            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtCODE.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDDEPARTMENT()
        fn_LoadKDDIAGNOSA()
        fn_LoadKDPPK()

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

        grdKDPENDAFTARAN.Properties.ReadOnly = Status
        deDATE.Properties.ReadOnly = Status
        deDATERENCANAKUNJUNGAN.Properties.ReadOnly = Status
        txtNOMORSEP.Properties.ReadOnly = Status
        grdKDPPK.Properties.ReadOnly = Status
        grdKDDEPARTMENT.Properties.ReadOnly = True
        grdKDPPK.Properties.ReadOnly = Status
        grdKDDIAGNOSA.Properties.ReadOnly = Status
        cboTIPERUJUKAN.Properties.ReadOnly = Status
        txtCATATAN.Properties.ReadOnly = Status
        txtPOLI_KODE.Properties.ReadOnly = True
        txtPOLI_NAME_DISPLAY.Properties.ReadOnly = True

        If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            l1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            l2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            l3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            l4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            l1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            l2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            l3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            l4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub
    Private Sub fn_EmptyMe()
        txtCODE.Text = "<--- AUTO --->"

        grdKDPENDAFTARAN.ResetText()
        deDATE.DateTime = Now
        deDATERENCANAKUNJUNGAN.DateTime = Now
        txtNOMORSEP.ResetText()
        grdKDDEPARTMENT.ResetText()
        grdKDPPK.ResetText()
        grdKDDIAGNOSA.ResetText()
        txtCATATAN.ResetText()
        txtPOLI_KODE.ResetText()
        txtPOLI_NAME_DISPLAY.ResetText()

        If sKDPENDFTARAN <> "" Then
            cboCARI.SelectedIndex = 2
            fn_LoadKDPENDAFTARAN(sKDPENDFTARAN)

            grdKDPENDAFTARAN.Text = sKDPENDFTARAN

            Dim oPendaftaran As New Admission.clsPendaftaran
            Dim dsPendaftaran = oPendaftaran.GetData(grdKDPENDAFTARAN.EditValue)
            If dsPendaftaran IsNot Nothing Then
                deDATE.DateTime = dsPendaftaran.DATE_RUJUKAN
                txtNOMORSEP.Text = dsPendaftaran.NOMORSEP
                grdKDDIAGNOSA.Text = dsPendaftaran.KDDIAGNOSA
                grdKDDEPARTMENT.Text = dsPendaftaran.KDDEPARTMENT
            End If

            cboTIPERUJUKAN.SelectedIndex = stipeRujukan
        End If
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oRujukan.GetData(sNoId)

            With ds
                txtCODE.Text = sNoId
                cboCARI.SelectedIndex = 2
                fn_LoadKDPENDAFTARAN(.KDPENDAFTARAN)
                grdKDPENDAFTARAN.Text = .KDPENDAFTARAN
                txtNOMORSEP.Text = .NOMORSEP
                deDATE.DateTime = .DATE
                deDATERENCANAKUNJUNGAN.DateTime = .DATERENCANAKONTROL
                grdKDPPK.Text = .KDPPK
                txtCATATAN.Text = .CATATAN
                grdKDDIAGNOSA.Text = .KDDIAGNOSA
                cboTIPERUJUKAN.SelectedIndex = .TIPERUJUKAN
                grdKDDEPARTMENT.Text = .KDDEPARTMENT
                txtPOLI_KODE.Text = .POLI_KODE
                txtPOLI_NAME_DISPLAY.Text = .POLI_NAME_DISPLAY
                txtDATE_BERLAKUKUNJUNGAN.Text = .DATE_BERLAKUKUNJUNGAN
                txtDATE_RENCANAKUNJUNGAN.Text = .DATE_RENCANAKUNJUNGAN
                txtDATE_RUJUKAN.Text = .DATE_RUJUKAN
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtCATATAN.Text = String.Empty Then
                txtCATATAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtCATATAN.ErrorText = Statement.ErrorRequired

                txtCATATAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDPENDAFTARAN.Text = String.Empty Then
                grdKDPENDAFTARAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDPENDAFTARAN.ErrorText = Statement.ErrorRequired

                grdKDPENDAFTARAN.Focus()
                fn_Validate = False
                Exit Function
            End If

            'If txtNOMORSEP.Text = String.Empty Then
            '    txtNOMORSEP.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '    txtNOMORSEP.ErrorText = Statement.ErrorRequired

            '    txtNOMORSEP.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If
            If grdKDDEPARTMENT.Text = String.Empty Then
                grdKDDEPARTMENT.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDEPARTMENT.ErrorText = Statement.ErrorRequired

                grdKDDEPARTMENT.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDPPK.Text = String.Empty Then
                grdKDPPK.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDPPK.ErrorText = Statement.ErrorRequired

                grdKDPPK.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDIAGNOSA.Text = String.Empty Then
                grdKDDIAGNOSA.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDIAGNOSA.ErrorText = Statement.ErrorRequired

                grdKDDIAGNOSA.Focus()
                fn_Validate = False
                Exit Function
            End If
            If cboTIPERUJUKAN.SelectedIndex <> 2 Then
                If txtPOLI_KODE.Text = String.Empty Then
                    txtPOLI_KODE.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    txtPOLI_KODE.ErrorText = Statement.ErrorRequired

                    txtPOLI_KODE.Focus()
                    fn_Validate = False
                    Exit Function
                End If
            End If

            If txtPOLI_NAME_DISPLAY.Text = String.Empty Then
                txtPOLI_NAME_DISPLAY.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtPOLI_NAME_DISPLAY.ErrorText = Statement.ErrorRequired

                txtPOLI_NAME_DISPLAY.Focus()
                fn_Validate = False
                Exit Function
            End If
            'If txtDATE_BERLAKUKUNJUNGAN.Text = String.Empty Then
            '    txtDATE_BERLAKUKUNJUNGAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '    txtDATE_BERLAKUKUNJUNGAN.ErrorText = Statement.ErrorRequired

            '    txtDATE_BERLAKUKUNJUNGAN.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If
            'If txtDATE_RENCANAKUNJUNGAN.Text = String.Empty Then
            '    txtDATE_RENCANAKUNJUNGAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '    txtDATE_RENCANAKUNJUNGAN.ErrorText = Statement.ErrorRequired

            '    txtDATE_RENCANAKUNJUNGAN.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If
            'If txtDATE_RUJUKAN.Text = String.Empty Then
            '    txtDATE_RUJUKAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '    txtDATE_RUJUKAN.ErrorText = Statement.ErrorRequired

            '    txtDATE_RUJUKAN.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            Dim jsonRequest As String = String.Empty
            Dim jsonResponse As String = String.Empty
            Dim oSetKoneksi As New Brigging.clsSetKoneksi

            If txtNOMORSEP.Text <> "" Then
                Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                    jsonRequest = fn_RequestInsertRujukanv2()

                    If jsonRequest <> "" Then
                        jsonResponse = fn_CreateRujukanV2(jsonRequest, uTime)
                        If jsonResponse <> "" Then
                            Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(jsonResponse, sVclaim_ConsId & sVclaim_SecreatKey & uTime))
                            txtCODE.Text = DataDecrypt.Item("rujukan")("noRujukan").ToString()
                            txtDATE_BERLAKUKUNJUNGAN.Text = DataDecrypt.Item("rujukan")("tglBerlakuKunjungan").ToString()
                            txtDATE_RENCANAKUNJUNGAN.Text = DataDecrypt.Item("rujukan")("tglRencanaKunjungan").ToString()
                            txtDATE_RUJUKAN.Text = DataDecrypt.Item("rujukan")("tglRujukan").ToString()
                        Else
                            fn_Save = False
                            Exit Function
                        End If
                    Else
                        fn_Save = False
                        Exit Function
                    End If
                Else
                    jsonRequest = fn_RequestUpdateRujukanV2()
                    If jsonRequest <> "" Then
                        jsonResponse = fn_UpdateRujukanV2(jsonRequest, uTime)
                        If jsonResponse <> "" Then
                            Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(jsonResponse, sVclaim_ConsId & sVclaim_SecreatKey & uTime))

                            'txtDATE_BERLAKUKUNJUNGAN.Text = DataDecrypt.Item("rujukan")("tglBerlakuKunjungan").ToString()
                            'txtDATE_RENCANAKUNJUNGAN.Text = DataDecrypt.Item("rujukan")("tglRencanaKunjungan").ToString()
                            'txtDATE_RUJUKAN.Text = DataDecrypt.Item("rujukan")("tglRujukan").ToString()
                        Else
                            fn_Save = False
                            Exit Function
                        End If
                    Else
                        fn_Save = False
                        Exit Function
                    End If
                End If
            Else
                If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                    txtCODE.ResetText()
                End If
            End If

            ' ***** HEADER *****

            Dim ds = oRujukan.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oRujukan.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDRUJUKAN = txtCODE.Text
                .KDPENDAFTARAN = grdKDPENDAFTARAN.EditValue
                .NOMORSEP = txtNOMORSEP.Text.Trim.ToUpper
                .DATE = deDATE.DateTime
                .DATERENCANAKONTROL = deDATERENCANAKUNJUNGAN.DateTime
                .KDPPK = grdKDPPK.EditValue
                .KDDEPARTMENT = grdKDDEPARTMENT.EditValue
                .KDPPK = grdKDPPK.EditValue
                .KDDIAGNOSA = grdKDDIAGNOSA.EditValue
                .CATATAN = txtCATATAN.Text.Trim.ToUpper
                .ISDELETE = False
                .KDUSER = sUserID
                .REQUEST = jsonRequest
                .RESPONSE = jsonResponse
                .POLI_KODE = txtPOLI_KODE.Text
                .POLI_NAME_DISPLAY = txtPOLI_NAME_DISPLAY.Text
                .TIPERUJUKAN = cboTIPERUJUKAN.SelectedIndex
                Try
                    .DATE_BERLAKUKUNJUNGAN = oRujukan.GetData(sNoId).DATE_BERLAKUKUNJUNGAN
                    .DATE_RENCANAKUNJUNGAN = oRujukan.GetData(sNoId).DATE_RENCANAKUNJUNGAN
                    .DATE_RUJUKAN = oRujukan.GetData(sNoId).DATE_RUJUKAN
                Catch ex As Exception
                    .DATE_BERLAKUKUNJUNGAN = txtDATE_BERLAKUKUNJUNGAN.Text
                    .DATE_RENCANAKUNJUNGAN = txtDATE_RENCANAKUNJUNGAN.Text
                    .DATE_RUJUKAN = txtDATE_RUJUKAN.Text
                End Try
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    txtCODE.Text = oRujukan.InsertData(ds)
                    If txtCODE.Text = "" Then
                        fn_Save = False
                    Else
                        fn_Save = True
                        CetakRegister(txtCODE.Text)
                    End If

                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oRujukan.UpdateData(ds)
                    CetakRegister(txtCODE.Text)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Sub CetakRegister(ByVal KDRUJUKAN As String)
        'Dim rpt As New xtraRujukan

        'Dim ds = oRujukan.GetData(KDRUJUKAN)
        'rpt.bindingSource.DataSource = ds
        'Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
        'printTool.PrintDialog()
    End Sub
    Private Function fn_RequestInsertRujukanv2()
        Try
            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim oDaftar As New Admission.clsPendaftaran
            Dim oDepartment As New Reference.clsDepartment
            Dim jsonRequest As String = String.Empty
            Dim oRujukan As New Reference.clsPPK

            jsonRequest = " { "
            jsonRequest &= """request"" :  { "
            jsonRequest &= """t_rujukan"": { "
            jsonRequest &= """noSep"": """ & txtNOMORSEP.Text.Trim.ToUpper & """, "
            jsonRequest &= """tglRujukan"": """ & deDATE.DateTime.ToString("yyyy-MM-dd") & """, "
            jsonRequest &= """tglRencanaKunjungan"": """ & deDATERENCANAKUNJUNGAN.DateTime.ToString("yyyy-MM-dd") & """, "
            jsonRequest &= """ppkDirujuk"": """ & oRujukan.GetData(grdKDPPK.EditValue).KODEFASKES & """, "
            jsonRequest &= """jnsPelayanan"": """ & IIf(oDaftar.GetData(grdKDPENDAFTARAN.EditValue).CATEGORY = 1, "1", "2") & """, "
            jsonRequest &= """catatan"": """ & txtCATATAN.Text.Trim.ToUpper & """, "
            jsonRequest &= """diagRujukan"": """ & grdKDDIAGNOSA.EditValue & """, "
            jsonRequest &= """tipeRujukan"": """ & cboTIPERUJUKAN.SelectedIndex & """, "
            jsonRequest &= """poliRujukan"": """ & IIf(oDaftar.GetData(grdKDPENDAFTARAN.EditValue).CATEGORY = 1, "", txtPOLI_KODE.Text) & """, "
            jsonRequest &= """user"": """ & sUserID & """ "
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "}  "

            fn_RequestInsertRujukanv2 = jsonRequest

        Catch oErr As Exception
            fn_RequestInsertRujukanv2 = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_CreateRujukanV2(ByVal jsonRequest As String, ByVal uTime As Integer) As String
        Try
            Dim oSetKoneksi As New Brigging.clsSetKoneksi

            If sVclaim_ConsId <> "" Then
                Dim dsSetKoneksi = oSetKoneksi.InsertRujukanV2(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, jsonRequest)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        fn_CreateRujukanV2 = allData("response")
                    Else
                        fn_CreateRujukanV2 = ""
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    fn_CreateRujukanV2 = ""
                    MsgBox("Koneksi Rujukan Kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_CreateRujukanV2 = ""
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            fn_CreateRujukanV2 = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_RequestUpdateRujukanV2()
        Try
            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim oDaftar As New Admission.clsPendaftaran
            Dim oDepartment As New Reference.clsDepartment
            Dim jsonRequest As String = String.Empty
            Dim oRujukan As New Reference.clsPPK

            jsonRequest = " { "
            jsonRequest &= """request"" :  { "
            jsonRequest &= """t_rujukan"": { "
            jsonRequest &= """noRujukan"": """ & txtCODE.Text.Trim.ToUpper & """, "
            jsonRequest &= """tglRujukan"": """ & deDATE.DateTime.ToString("yyyy-MM-dd") & """, "
            jsonRequest &= """tglRencanaKunjungan"": """ & deDATERENCANAKUNJUNGAN.DateTime.ToString("yyyy-MM-dd") & """, "
            jsonRequest &= """ppkDirujuk"": """ & oRujukan.GetData(grdKDPPK.EditValue).KODEFASKES & """, "
            jsonRequest &= """jnsPelayanan"": """ & IIf(oDaftar.GetData(grdKDPENDAFTARAN.EditValue).CATEGORY = 1, "1", "2") & """, "
            jsonRequest &= """catatan"": """ & txtCATATAN.Text.Trim.ToUpper & """, "
            jsonRequest &= """diagRujukan"": """ & grdKDDIAGNOSA.EditValue & """, "
            'jsonRequest &= """tipe"": """ & cboTIPERUJUKAN.SelectedIndex & """, "
            jsonRequest &= """tipeRujukan"": """ & cboTIPERUJUKAN.SelectedIndex & """, "
            jsonRequest &= """poliRujukan"": """ & IIf(oDaftar.GetData(grdKDPENDAFTARAN.EditValue).CATEGORY = 1, "", txtPOLI_KODE.Text) & """, "
            jsonRequest &= """user"": """ & sUserID & """ "
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "}  "

            fn_RequestUpdateRujukanV2 = jsonRequest

        Catch oErr As Exception
            fn_RequestUpdateRujukanV2 = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_UpdateRujukanV2(ByVal jsonRequest As String, ByVal uTime As Integer) As String
        Try
            Dim oSetKoneksi As New Brigging.clsSetKoneksi

            If sVclaim_ConsId <> "" Then
                Dim dsSetKoneksi = oSetKoneksi.UpdateRujukanV2(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, jsonRequest)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        fn_UpdateRujukanV2 = allData("response")
                    Else
                        fn_UpdateRujukanV2 = ""
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                        Exit Function
                    End If
                Else
                    fn_UpdateRujukanV2 = ""
                    MsgBox("Koneksi Rujukan Kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_UpdateRujukanV2 = ""
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            fn_UpdateRujukanV2 = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub btnFaskes_Click(sender As Object, e As EventArgs)
        frmPPK.LoadMe(FORM_MODE.FORM_MODE_ADD)
        frmPPK.ShowDialog(Me)
        fn_LoadKDPPK()

        Dim oPPK As New Reference.clsPPK

        grdKDPPK.Text = oPPK.GetData.FirstOrDefault(Function(x) x.MEMO = sCode).KDPPK

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
    Private Sub fn_LoadKDDEPARTMENT()
        Dim oDEPARTMENT As New Reference.clsDepartment
        Try
            grdKDDEPARTMENT.Properties.DataSource = oDEPARTMENT.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDEPARTMENT.Properties.ValueMember = "KDDEPARTMENT"
            grdKDDEPARTMENT.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDDIAGNOSA()
        Dim oDIAGNOSA As New Reference.clsDiagnosa

        Try
            grdKDDIAGNOSA.Properties.DataSource = oDIAGNOSA.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDIAGNOSA.Properties.ValueMember = "KDDIAGNOSA"
            grdKDDIAGNOSA.Properties.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDPPK()
        Dim oPPK As New Reference.clsPPK

        Try
            grdKDPPK.Properties.DataSource = oPPK.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDPPK.Properties.ValueMember = "KDPPK"
            grdKDPPK.Properties.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub txtCARI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCARI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            PopUP = True
            fn_LoadKDPENDAFTARAN(txtCARI.Text)
            txtCARI.ResetText()
        End If
    End Sub
    Private Sub fn_LoadKDPENDAFTARAN(ByVal Parameter As String)
        Dim oDEPARTMENT As New Reference.clsDepartment
        Try
            Dim oPendaftaran As New Admission.clsPendaftaran

            Dim dsPendaftaran = (From x In oPendaftaran.GetDataBySKD(Parameter, cboCARI.SelectedIndex)
                                 Select x.KDPENDAFTARAN, x.CATEGORY, x.KDCUSTOMER, x.M_CUSTOMER.NAME_DISPLAY, TUJUAN = x.M_DEPARTMENT.NAME_DISPLAY, DOKTER = x.M_DOCTOR.NAME_DISPLAY, x.DATE).OrderByDescending(Function(x) x.DATE)

            grdKDPENDAFTARAN.Properties.DataSource = dsPendaftaran.ToList()
            grdKDPENDAFTARAN.Properties.ValueMember = "KDPENDAFTARAN"
            grdKDPENDAFTARAN.Properties.DisplayMember = "KDPENDAFTARAN"

            If PopUP = True Then
                grdKDPENDAFTARAN.ShowPopup()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDPENDAFTARAN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdKDPENDAFTARAN.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Dim oPendaftaran As New Admission.clsPendaftaran
            Dim dsPendaftaran = oPendaftaran.GetData(grdKDPENDAFTARAN.EditValue)
            If dsPendaftaran IsNot Nothing Then
                deDATE.DateTime = dsPendaftaran.DATE_RUJUKAN
                txtNOMORSEP.Text = dsPendaftaran.NOMORSEP
                grdKDDIAGNOSA.Text = dsPendaftaran.KDDIAGNOSA
                grdKDDEPARTMENT.Text = dsPendaftaran.KDDEPARTMENT
            End If
        End If
    End Sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        If grdKDPPK.Text <> "" Then
            fn_LoadData02()
        Else
            MsgBox("Silahkan Pilih Faskes", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub grdKDPPK_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDPPK.EditValueChanged
        If isLoad = True Then
            If grdKDPPK.Text <> "" Then
                fn_LoadData02()
            End If
        End If
    End Sub
    Private Sub fn_LoadData02()
        Try
            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim oPPK As New Reference.clsPPK

            Dim uTime As Integer = 0

            If sVclaim_ConsId <> "" Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.GetDataVClaimListSpesialistikRujukan(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, oPPK.GetData(grdKDPPK.EditValue).KODEFASKES, deDATERENCANAKUNJUNGAN.DateTime.ToString("yyyy-MM-dd"))

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), sVclaim_ConsId & sVclaim_SecreatKey & uTime))

                        Dim table As DataTable

                        table = New DataTable("DATAKUNJUNGAN")
                        table.Columns.Add("kodeSpesialis")
                        table.Columns.Add("namaSpesialis")
                        table.Columns.Add("kapasitas")
                        table.Columns.Add("jumlahRujukan")
                        table.Columns.Add("persentase")

                        For Each item In DataDecrypt("list")
                            table.Rows.Add(New String() {item("kodeSpesialis"), item("namaSpesialis"), item("kapasitas"), item("jumlahRujukan"), item("persentase")})
                        Next

                        grd.MainView = grv
                        grd.DataSource = table
                        grd.ForceInitialize()
                        fn_LoadFormatData()
                    Else
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    MsgBox("Obat Generik Program PRB Kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatData()
        For iLoop As Integer = 0 To grv.Columns.Count - 1
            If grv.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next
    End Sub
    Private Sub grv_DoubleClick(sender As Object, e As EventArgs) Handles grv.DoubleClick
        If grv.GetFocusedRowCellValue("kodeSpesialis") Is Nothing Then
            Exit Sub
        End If
        txtPOLI_KODE.Text = grv.GetFocusedRowCellValue("kodeSpesialis")
        txtPOLI_NAME_DISPLAY.Text = grv.GetFocusedRowCellValue("namaSpesialis")
    End Sub
    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        Dim ds = oRujukan.GetData(sNoId)
        If ds IsNot Nothing Then
            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
            Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(ds.RESPONSE, sVclaim_ConsId & sVclaim_SecreatKey & uTime))

            txtDATE_BERLAKUKUNJUNGAN.Text = DataDecrypt.Item("rujukan")("tglBerlakuKunjungan").ToString()
            txtDATE_RENCANAKUNJUNGAN.Text = DataDecrypt.Item("rujukan")("tglRencanaKunjungan").ToString()
            txtDATE_RUJUKAN.Text = DataDecrypt.Item("rujukan")("tglRujukan").ToString()

            oRujukan.UpdateDataRespon(ds.KDRUJUKAN, txtDATE_BERLAKUKUNJUNGAN.Text, txtDATE_RENCANAKUNJUNGAN.Text, txtDATE_RUJUKAN.Text)
        End If
    End Sub
    Private Sub btnOfline_Click(sender As Object, e As EventArgs) Handles btnOfline.Click
        txtNOMORSEP.ResetText()
    End Sub
#End Region
End Class