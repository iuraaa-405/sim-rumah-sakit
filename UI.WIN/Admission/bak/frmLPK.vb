Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq

Public Class frmLPK
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oLPK As New Admission.clsLPK
    Private PopUP As Boolean = False
    Private REQUEST As String = ""
    Private RESPONSE As String = ""

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
            Me.Text = LPK.TITLE

            lKDLPK.Text = LPK.KDLPK
            lKDPENDAFATRAN.Text = LPK.KDPENDAFTARAN
            lNOMORSEP.Text = LPK.NOMORSEP
            lDATE_MASUK.Text = LPK.DATE_MASUK
            lDATE_KELUAR.Text = LPK.DATE_KELUAR
            lJAMINAN.Text = LPK.JAMINAN
            lKDDEPARTMENT.Text = LPK.KDDEPARTMENT
            lKDKELASRAWAT.Text = LPK.KDKELASRAWAT
            lKDSPESIALISTIK.Text = LPK.KDSPESIALISTIK
            lKDCARAKELUAR.Text = LPK.KDCARAKELUAR
            lKDDIAGNOSA1.Text = LPK.KDDIAGNOSA1
            lKDDIAGNOSA2.Text = LPK.KDDIAGNOSA2
            lKDDIAGNOSA3.Text = LPK.KDDIAGNOSA3
            lKDDIAGNOSA4.Text = LPK.KDDIAGNOSA4
            lKDDIAGNOSA5.Text = LPK.KDDIAGNOSA5
            lKDDIAGNOSA6.Text = LPK.KDDIAGNOSA6
            lKDPROSEDURE1.Text = LPK.KDPROCEDURE1
            lKDPROSEDURE2.Text = LPK.KDPROCEDURE2
            lKDPROSEDURE3.Text = LPK.KDPROCEDURE3
            lKDPROSEDURE4.Text = LPK.KDPROCEDURE4
            lKDPROSEDURE5.Text = LPK.KDPROCEDURE5
            lKDPROSEDURE6.Text = LPK.KDPROCEDURE6
            lTINDAKLANJUT.Text = LPK.TINDAKLANJUT
            lKDPPK.Text = LPK.KDPPK
            lDATE_KONTROL.Text = LPK.DATE_KONTROL
            lKDDEPARTMENT_KONTROL.Text = LPK.KDDEPARTMENT_KONTROL
            lKDDOCTOR.Text = LPK.KDDOCTOR
            lKDPASCAPULANG.Text = LPK.KDPASCAPULANG

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
        fn_LoadKDPROCEDURE()
        fn_LoadKDPPK()
        fn_LoadKDDOCTOR()
        fn_LoadKDCARAKELUAR()
        fn_LoadKDKELASRAWAT()
        fn_LoadKDSUBSPESIALISTIK()
        fn_LoadKDPASCAPULANG()

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
        txtNOMORSEP.Properties.ReadOnly = True
        deDATE_MASUK.Properties.ReadOnly = True
        deDATE_KELUAR.Properties.ReadOnly = Status
        cboJAMINAN.Properties.ReadOnly = Status
        grdKDDEPARTMENT.Properties.ReadOnly = Status
        grdKDKELASRAWAT.Properties.ReadOnly = Status
        grdKDSPESIALISTIK.Properties.ReadOnly = Status
        grdKDCARAKELUAR.Properties.ReadOnly = Status
        grdKDPASCAPULANG.Properties.ReadOnly = Status
        grdKDDIAGNOSA1.Properties.ReadOnly = Status
        grdKDDIAGNOSA2.Properties.ReadOnly = Status
        grdKDDIAGNOSA3.Properties.ReadOnly = Status
        grdKDDIAGNOSA4.Properties.ReadOnly = Status
        grdKDDIAGNOSA5.Properties.ReadOnly = Status
        grdKDDIAGNOSA6.Properties.ReadOnly = Status
        grdKDPROCEDURE1.Properties.ReadOnly = Status
        grdKDPROCEDURE2.Properties.ReadOnly = Status
        grdKDPROCEDURE3.Properties.ReadOnly = Status
        grdKDPROCEDURE4.Properties.ReadOnly = Status
        grdKDPROCEDURE5.Properties.ReadOnly = Status
        grdKDPROCEDURE6.Properties.ReadOnly = Status
        cboTINDAKLANJUT.Properties.ReadOnly = Status
        grdKDPPK.Properties.ReadOnly = Status
        deDATE_KONTROL.Properties.ReadOnly = Status
        grdKDDEPARTMENT_KONTROL.Properties.ReadOnly = Status
        grdKDDOCTOR.Properties.ReadOnly = Status

    End Sub
    Private Sub fn_EmptyMe()
        txtCODE.Text = "<--- AUTO --->"

        grdKDPENDAFTARAN.ResetText()
        txtNOMORSEP.ResetText()
        deDATE_MASUK.DateTime = Now
        deDATE_KELUAR.DateTime = Now
        cboJAMINAN.ResetText()
        deDATE_KONTROL.DateTime = Now

        grdKDDIAGNOSA1.Text = oLPK.DIAGNOSA_Default
        grdKDDIAGNOSA2.Text = oLPK.DIAGNOSA_Default
        grdKDDIAGNOSA3.Text = oLPK.DIAGNOSA_Default
        grdKDDIAGNOSA4.Text = oLPK.DIAGNOSA_Default
        grdKDDIAGNOSA5.Text = oLPK.DIAGNOSA_Default
        grdKDDIAGNOSA6.Text = oLPK.DIAGNOSA_Default

        grdKDPROCEDURE1.Text = oLPK.PROSEDUR_Default
        grdKDPROCEDURE2.Text = oLPK.PROSEDUR_Default
        grdKDPROCEDURE3.Text = oLPK.PROSEDUR_Default
        grdKDPROCEDURE4.Text = oLPK.PROSEDUR_Default
        grdKDPROCEDURE5.Text = oLPK.PROSEDUR_Default
        grdKDPROCEDURE6.Text = oLPK.PROSEDUR_Default

    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oLPK.GetData(sNoId)

            With ds
                txtCODE.Text = sNoId
                cboCARI.SelectedIndex = 2
                fn_LoadKDPENDAFTARAN(.KDPENDAFTARAN)

                grdKDPENDAFTARAN.Text = .KDPENDAFTARAN
                txtNOMORSEP.Text = .NOMORSEP
                deDATE_MASUK.DateTime = .DATE_MASUK
                deDATE_KELUAR.DateTime = .DATE_KELUAR
                cboJAMINAN.SelectedIndex = .JAMINAN
                grdKDDEPARTMENT.Text = .KDDEPARTMENT_POLI
                grdKDKELASRAWAT.Text = .KDKELASRAWAT
                grdKDSPESIALISTIK.Text = .KDSPESIALISTIK
                grdKDCARAKELUAR.Text = .KDCARAKELUAR
                grdKDPASCAPULANG.Text = .KDPASCAPULANG
                grdKDDIAGNOSA1.Text = .KDDIAGNOSA1
                grdKDDIAGNOSA2.Text = .KDDIAGNOSA2
                grdKDDIAGNOSA3.Text = .KDDIAGNOSA3
                grdKDDIAGNOSA4.Text = .KDDIAGNOSA4
                grdKDDIAGNOSA5.Text = .KDDIAGNOSA5
                grdKDDIAGNOSA6.Text = .KDDIAGNOSA6
                grdKDPROCEDURE1.Text = .KDPROCEDURE1
                grdKDPROCEDURE2.Text = .KDPROCEDURE2
                grdKDPROCEDURE3.Text = .KDPROCEDURE3
                grdKDPROCEDURE4.Text = .KDPROCEDURE4
                grdKDPROCEDURE5.Text = .KDPROCEDURE5
                grdKDPROCEDURE6.Text = .KDPROCEDURE6
                cboTINDAKLANJUT.SelectedIndex = .TINDAKLANJUT
                grdKDPPK.Text = .KDPPK
                deDATE_KONTROL.DateTime = .DATE_KONTROL
                grdKDDEPARTMENT_KONTROL.Text = .KDDEPARTMENT_KONTROL
                grdKDDOCTOR.Text = .KDDOCTOR

            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If grdKDPENDAFTARAN.Text = String.Empty Then
                grdKDPENDAFTARAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDPENDAFTARAN.ErrorText = Statement.ErrorRequired

                grdKDPENDAFTARAN.Focus()
                fn_Validate = False
                Exit Function
            End If

            If cboJAMINAN.Text = String.Empty Then
                cboJAMINAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                cboJAMINAN.ErrorText = Statement.ErrorRequired

                cboJAMINAN.Focus()
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
            If grdKDPPK.Text = String.Empty Then
                grdKDPPK.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDPPK.ErrorText = Statement.ErrorRequired

                grdKDPPK.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDIAGNOSA1.Text = String.Empty Then
                grdKDDIAGNOSA1.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDIAGNOSA1.ErrorText = Statement.ErrorRequired

                grdKDDIAGNOSA1.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDOCTOR.Text = String.Empty Then
                grdKDDOCTOR.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDOCTOR.ErrorText = Statement.ErrorRequired

                grdKDDOCTOR.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDEPARTMENT_KONTROL.Text = String.Empty Then
                grdKDDEPARTMENT_KONTROL.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDEPARTMENT_KONTROL.ErrorText = Statement.ErrorRequired

                grdKDDEPARTMENT_KONTROL.Focus()
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
            Dim sNoLPK As String = String.Empty

            'If txtNOMORSEP.Text <> "" Then
            '    If fn_CariSEP(txtNOMORSEP.Text.ToString.Trim.ToUpper) = True Then
            '        If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            '            sNoLPK = fn_CreateLPK()
            '        Else
            '            If fn_UpdateLPK() = "" Then
            '                MsgBox("Update LPK gagal", MsgBoxStyle.Exclamation, Me.Text)
            '            End If
            '        End If
            '    Else
            '        MsgBox("Sep tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            '    End If
            'End If

            Dim ds = oLPK.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oLPK.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDLPK = IIf(sNoLPK = String.Empty, sNoId, sNoLPK)
                .KDPENDAFTARAN = grdKDPENDAFTARAN.EditValue
                .NOMORSEP = txtNOMORSEP.Text.Trim.ToUpper
                .DATE_MASUK = deDATE_MASUK.DateTime
                .DATE_KELUAR = deDATE_KELUAR.DateTime
                .JAMINAN = cboJAMINAN.SelectedIndex
                .KDDEPARTMENT_POLI = grdKDDEPARTMENT.EditValue
                .KDKELASRAWAT = grdKDKELASRAWAT.EditValue
                .KDSPESIALISTIK = grdKDSPESIALISTIK.EditValue
                .KDCARAKELUAR = grdKDCARAKELUAR.EditValue
                .KDPASCAPULANG = grdKDPASCAPULANG.EditValue
                .KDDIAGNOSA1 = grdKDDIAGNOSA1.EditValue
                .KDDIAGNOSA2 = grdKDDIAGNOSA2.EditValue
                .KDDIAGNOSA3 = grdKDDIAGNOSA3.EditValue
                .KDDIAGNOSA4 = grdKDDIAGNOSA4.EditValue
                .KDDIAGNOSA5 = grdKDDIAGNOSA5.EditValue
                .KDDIAGNOSA6 = grdKDDIAGNOSA6.EditValue
                .KDPROCEDURE1 = grdKDPROCEDURE1.EditValue
                .KDPROCEDURE2 = grdKDPROCEDURE2.EditValue
                .KDPROCEDURE3 = grdKDPROCEDURE3.EditValue
                .KDPROCEDURE4 = grdKDPROCEDURE4.EditValue
                .KDPROCEDURE5 = grdKDPROCEDURE5.EditValue
                .KDPROCEDURE6 = grdKDPROCEDURE6.EditValue
                .TINDAKLANJUT = cboTINDAKLANJUT.SelectedIndex
                .KDPPK = grdKDPPK.EditValue
                .DATE_KONTROL = deDATE_KONTROL.DateTime
                .KDDEPARTMENT_KONTROL = grdKDDEPARTMENT_KONTROL.EditValue
                .KDDOCTOR = grdKDDOCTOR.EditValue
                .KDUSER = sUserID
                .REQUEST = REQUEST
                .RESPONSE = RESPONSE
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    txtCODE.Text = oLPK.InsertData(ds)
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
                    fn_Save = oLPK.UpdateData(ds)
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
    Private Sub CetakRegister(ByVal KDLPK As String)
        'Dim rpt As New xtraLPK

        'Dim ds = oLPK.GetData(KDLPK)
        'rpt.bindingSource.DataSource = ds
        'Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
        'printTool.PrintDialog()
    End Sub
    Private Function fn_CariSEP(ByVal NOMORSEP As String) As Boolean
        Try
            If NOMORSEP = String.Empty Then
                fn_CariSEP = False
                Exit Function
            End If

            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim dsSetKoneksi = oSetKoneksi.CariSEP("VCLAIM", NOMORSEP)

            Dim allData = JObject.Parse(dsSetKoneksi)
            Dim CodeResponse As String = String.Empty
            Dim messageResponse As String = String.Empty

            CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
            messageResponse = allData("metaData")("message").ToString

            If CodeResponse = "200" Then
                fn_CariSEP = True
            Else
                fn_CariSEP = False
            End If

        Catch oErr As Exception
            fn_CariSEP = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_CreateLPK() As String
        Try
            fn_CreateLPK = ""

            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim oDaftar As New Admission.clsPendaftaran
            Dim oDepartment As New Reference.clsDepartment
            Dim jsonRequest As String = String.Empty
            Dim NoLPK As String = String.Empty

            Dim oLPK As New Reference.clsPPK
            Dim oPoli As New Reference.clsDepartment

            jsonRequest = " { "
            jsonRequest &= """request"" :  { "
            jsonRequest &= """t_LPK"": { "
            jsonRequest &= """noSep"": """ & txtNOMORSEP.Text.Trim.ToUpper & """, "
            jsonRequest &= """tglMasuk"": """ & deDATE_MASUK.DateTime.ToString("yyyy-MM-dd") & """, "
            jsonRequest &= """tglKeluar"": """ & deDATE_KELUAR.DateTime.ToString("yyyy-MM-dd") & """, "
            jsonRequest &= """jaminan"": """ & cboJAMINAN.SelectedIndex + 1 & """, "
            jsonRequest &= """poli"": { "
            jsonRequest &= """poli"": """ & oPoli.GetData(grdKDDEPARTMENT.EditValue).VCLAIM_KODEPOLI & """ "
            jsonRequest &= "}, "
            jsonRequest &= """perawatan"": { "
            jsonRequest &= """ruangRawat"": """ & oPoli.GetData(grdKDDEPARTMENT.EditValue).VCLAIM_KODEPOLI & """ "
            jsonRequest &= """kelasRawat"": """ & grdKDKELASRAWAT.EditValue & """ "
            jsonRequest &= """spesialistik"": """ & grdKDSPESIALISTIK.EditValue & """ "
            jsonRequest &= """caraKeluar"": """ & grdKDCARAKELUAR.EditValue & """ "
            jsonRequest &= """kondisiPulang"": """ & grdKDPASCAPULANG.EditValue & """ "
            jsonRequest &= "}, "
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "}  "

            'Dim dsSetKoneksi = oSetKoneksi.InsertLPK("VCLAIM", jsonRequest)

            'If dsSetKoneksi <> "" Then
            '    Dim allData = JObject.Parse(dsSetKoneksi)
            '    Dim CodeResponse As String = String.Empty
            '    Dim messageResponse As String = String.Empty

            '    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
            '    messageResponse = allData("metaData")("message").ToString

            '    If CodeResponse = "200" Then
            '        REQUEST = jsonRequest
            '        RESPONSE = dsSetKoneksi
            '        NoLPK = allData.Item("response")("LPK")("noLPK").ToString()
            '    Else
            '        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
            '        fn_CreateLPK = ""
            '        Exit Function
            '    End If
            'End If

            'fn_CreateLPK = NoLPK

        Catch oErr As Exception
            fn_CreateLPK = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_UpdateLPK() As String
        Try
            fn_UpdateLPK = ""

            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim oDaftar As New Admission.clsPendaftaran
            Dim oDepartment As New Reference.clsDepartment
            Dim jsonRequest As String = String.Empty
            Dim NoLPK As String = String.Empty

            Dim oLPK As New Reference.clsPPK

            'jsonRequest = " { "
            'jsonRequest &= """request"" :  { "
            'jsonRequest &= """t_LPK"": { "
            'jsonRequest &= """noLPK"": """ & txtCODE.Text.Trim.ToUpper & """, "
            'jsonRequest &= """ppkDirujuk"": """ & oLPK.GetData(grdKDPPK.EditValue).KODEFASKES & """, "
            'jsonRequest &= """tipe"": """ & cboTIPELPK.SelectedIndex & """, "
            'jsonRequest &= """jnsPelayanan"": """ & IIf(oDaftar.GetData(grdKDPENDAFTARAN.EditValue).CATEGORY = 0, 1, 2) & """, "
            'jsonRequest &= """catatan"": """ & txtCATATAN.Text.Trim.ToUpper & """, "
            'jsonRequest &= """diagLPK"": """ & grdKDDIAGNOSA1.EditValue & """, "
            'jsonRequest &= """tipeLPK"": """ & cboTIPELPK.SelectedIndex & """, "
            'jsonRequest &= """poliLPK"": """ & oDepartment.GetData(grdKDDEPARTMENT.EditValue).VCLAIM_KODEPOLI & """, "
            'jsonRequest &= """user"": """ & sUserID & """ "
            'jsonRequest &= "} "
            'jsonRequest &= "} "
            'jsonRequest &= "}  "

            'Dim dsSetKoneksi = oSetKoneksi.UpdateLPK("VCLAIM", jsonRequest)

            'If dsSetKoneksi <> "" Then
            '    Dim allData = JObject.Parse(dsSetKoneksi)
            '    Dim CodeResponse As String = String.Empty
            '    Dim messageResponse As String = String.Empty

            '    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
            '    messageResponse = allData("metaData")("message").ToString

            '    If CodeResponse = "200" Then
            '        REQUEST = jsonRequest
            '        RESPONSE = dsSetKoneksi
            '        'NoLPK = allData.Item("response")("LPK")("noLPK").ToString()
            '    Else
            '        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
            '        fn_UpdateLPK = ""
            '        Exit Function
            '    End If
            'End If

            'fn_UpdateLPK = "OK"

        Catch oErr As Exception
            fn_UpdateLPK = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
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

            grdKDDEPARTMENT_KONTROL.Properties.DataSource = oDEPARTMENT.GetData.Where(Function(x) x.ISACTIVE = True And x.VCLAIM_KODEPOLI <> "").ToList()
            grdKDDEPARTMENT_KONTROL.Properties.ValueMember = "KDDEPARTMENT"
            grdKDDEPARTMENT_KONTROL.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDDOCTOR()
        Dim oDOCTOR As New Reference.clsDoctor
        Try
            grdKDDOCTOR.Properties.DataSource = oDOCTOR.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDDIAGNOSA()
        Dim oDIAGNOSA As New Reference.clsDiagnosa

        Try
            Dim dsDiagnosa = oDIAGNOSA.GetData.Where(Function(x) x.ISACTIVE = True)

            grdKDDIAGNOSA1.Properties.DataSource = dsDiagnosa.ToList()
            grdKDDIAGNOSA1.Properties.ValueMember = "KDDIAGNOSA"
            grdKDDIAGNOSA1.Properties.DisplayMember = "MEMO"

            grdKDDIAGNOSA2.Properties.DataSource = dsDiagnosa.ToList()
            grdKDDIAGNOSA2.Properties.ValueMember = "KDDIAGNOSA"
            grdKDDIAGNOSA2.Properties.DisplayMember = "MEMO"

            grdKDDIAGNOSA3.Properties.DataSource = dsDiagnosa.ToList()
            grdKDDIAGNOSA3.Properties.ValueMember = "KDDIAGNOSA"
            grdKDDIAGNOSA3.Properties.DisplayMember = "MEMO"

            grdKDDIAGNOSA4.Properties.DataSource = dsDiagnosa.ToList()
            grdKDDIAGNOSA4.Properties.ValueMember = "KDDIAGNOSA"
            grdKDDIAGNOSA4.Properties.DisplayMember = "MEMO"

            grdKDDIAGNOSA5.Properties.DataSource = dsDiagnosa.ToList()
            grdKDDIAGNOSA5.Properties.ValueMember = "KDDIAGNOSA"
            grdKDDIAGNOSA5.Properties.DisplayMember = "MEMO"

            grdKDDIAGNOSA6.Properties.DataSource = dsDiagnosa.ToList()
            grdKDDIAGNOSA6.Properties.ValueMember = "KDDIAGNOSA"
            grdKDDIAGNOSA6.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDPROCEDURE()
        Dim oPROCEDURE As New Reference.clsProsedur

        Try
            Dim dsDiagnosa = oPROCEDURE.GetData.Where(Function(x) x.ISACTIVE = True)

            grdKDPROCEDURE1.Properties.DataSource = dsDiagnosa.ToList()
            grdKDPROCEDURE1.Properties.ValueMember = "KDPROCEDURE"
            grdKDPROCEDURE1.Properties.DisplayMember = "MEMO"

            grdKDPROCEDURE2.Properties.DataSource = dsDiagnosa.ToList()
            grdKDPROCEDURE2.Properties.ValueMember = "KDPROCEDURE"
            grdKDPROCEDURE2.Properties.DisplayMember = "MEMO"

            grdKDPROCEDURE3.Properties.DataSource = dsDiagnosa.ToList()
            grdKDPROCEDURE3.Properties.ValueMember = "KDPROCEDURE"
            grdKDPROCEDURE3.Properties.DisplayMember = "MEMO"

            grdKDPROCEDURE4.Properties.DataSource = dsDiagnosa.ToList()
            grdKDPROCEDURE4.Properties.ValueMember = "KDPROCEDURE"
            grdKDPROCEDURE4.Properties.DisplayMember = "MEMO"

            grdKDPROCEDURE5.Properties.DataSource = dsDiagnosa.ToList()
            grdKDPROCEDURE5.Properties.ValueMember = "KDPROCEDURE"
            grdKDPROCEDURE5.Properties.DisplayMember = "MEMO"

            grdKDPROCEDURE6.Properties.DataSource = dsDiagnosa.ToList()
            grdKDPROCEDURE6.Properties.ValueMember = "KDPROCEDURE"
            grdKDPROCEDURE6.Properties.DisplayMember = "MEMO"
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
    Private Sub fn_LoadKDKELASRAWAT()
        Dim oKELAS As New Reference.clsKelasRawat

        Try
            grdKDKELASRAWAT.Properties.DataSource = oKELAS.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDKELASRAWAT.Properties.ValueMember = "KDKELASRAWAT"
            grdKDKELASRAWAT.Properties.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDSUBSPESIALISTIK()
        Dim oSUBSPESIALISTIK As New Reference.clsSpesialistik

        Try
            grdKDSPESIALISTIK.Properties.DataSource = oSUBSPESIALISTIK.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDSPESIALISTIK.Properties.ValueMember = "KDSPESIALISTIK"
            grdKDSPESIALISTIK.Properties.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDCARAKELUAR()
        Dim oCARAPULANG As New Reference.clsCaraKeluar

        Try
            grdKDCARAKELUAR.Properties.DataSource = oCARAPULANG.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDCARAKELUAR.Properties.ValueMember = "KDCARAKELUAR"
            grdKDCARAKELUAR.Properties.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDPASCAPULANG()
        Dim oPASCAPULANG As New Reference.clsPascaPulang

        Try
            grdKDPASCAPULANG.Properties.DataSource = oPASCAPULANG.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDPASCAPULANG.Properties.ValueMember = "KDPASCAPULANG"
            grdKDPASCAPULANG.Properties.DisplayMember = "MEMO"

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
                deDATE_MASUK.DateTime = dsPendaftaran.DATE
                txtNOMORSEP.Text = dsPendaftaran.NOMORSEP
                grdKDDEPARTMENT.Text = dsPendaftaran.KDDEPARTMENT
            End If
        End If
    End Sub
#End Region
End Class