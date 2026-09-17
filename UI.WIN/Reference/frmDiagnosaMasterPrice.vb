Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq

Public Class frmDiagnosaMasterPrice
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oDiagnosaMaster As New Reference.clsDiagnosaMasterPrice
    Private PopUP As Boolean = False

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
            Me.Text = DiagnosaMaster.TITLE

            lCODE.Text = DiagnosaMaster.KDKDDIAGNOSAMASTER
            lJENIS.Text = "Jenis"
            lINACBG.Text = "Inacbg"
            lKLSI.Text = "Kelas I"
            lKLSII.Text = "Kelas II"
            lKLSIII.Text = "Kelas III"
            lKET.Text = "KET SEKUNDER & KOMPLIKASI"
            lKDDIAGNOSA1.Text = DiagnosaMaster.KDDIAGNOSA1
            lKDDIAGNOSA2.Text = DiagnosaMaster.KDDIAGNOSA2
            lKDDIAGNOSA3.Text = DiagnosaMaster.KDDIAGNOSA3
            lKDDIAGNOSA4.Text = DiagnosaMaster.KDDIAGNOSA4
            lKDDIAGNOSA5.Text = DiagnosaMaster.KDDIAGNOSA5
            lKDDIAGNOSA6.Text = DiagnosaMaster.KDDIAGNOSA6
            lKDPROSEDURE1.Text = DiagnosaMaster.KDPROCEDURE1
            lKDPROSEDURE2.Text = DiagnosaMaster.KDPROCEDURE2
            lKDPROSEDURE3.Text = DiagnosaMaster.KDPROCEDURE3
            lKDPROSEDURE4.Text = DiagnosaMaster.KDPROCEDURE4
            lKDPROSEDURE5.Text = DiagnosaMaster.KDPROCEDURE5
            lKDPROSEDURE6.Text = DiagnosaMaster.KDPROCEDURE6

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
        fn_LoadKDDIAGNOSA()
        fn_LoadKDPROCEDURE()

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

        txtJENIS.Properties.ReadOnly = Status
        txtINACBG.Properties.ReadOnly = Status
        txtKLSI.Properties.ReadOnly = Status
        txtKLSII.Properties.ReadOnly = Status
        txtKLSIII.Properties.ReadOnly = Status
        txtKET.Properties.ReadOnly = Status
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

    End Sub
    Private Sub fn_EmptyMe()
        txtCODE.Text = "<--- AUTO --->"
        grdKDDIAGNOSA1.Text = oDiagnosaMaster.DIAGNOSA_Default
        grdKDDIAGNOSA2.Text = oDiagnosaMaster.DIAGNOSA_Default
        grdKDDIAGNOSA3.Text = oDiagnosaMaster.DIAGNOSA_Default
        grdKDDIAGNOSA4.Text = oDiagnosaMaster.DIAGNOSA_Default
        grdKDDIAGNOSA5.Text = oDiagnosaMaster.DIAGNOSA_Default
        grdKDDIAGNOSA6.Text = oDiagnosaMaster.DIAGNOSA_Default

        grdKDPROCEDURE1.Text = oDiagnosaMaster.PROSEDUR_Default
        grdKDPROCEDURE2.Text = oDiagnosaMaster.PROSEDUR_Default
        grdKDPROCEDURE3.Text = oDiagnosaMaster.PROSEDUR_Default
        grdKDPROCEDURE4.Text = oDiagnosaMaster.PROSEDUR_Default
        grdKDPROCEDURE5.Text = oDiagnosaMaster.PROSEDUR_Default
        grdKDPROCEDURE6.Text = oDiagnosaMaster.PROSEDUR_Default

    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oDiagnosaMaster.GetData(sNoId)

            With ds
                txtCODE.Text = sNoId
                txtJENIS.Text = .JENIS
                txtINACBG.Text = .INACBG
                txtKLSI.Text = .KLSI
                txtKLSII.Text = .KLS2
                txtKLSIII.Text = .KLS3
                txtKET.Text = .DESCRIPTION
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

            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If grdKDDIAGNOSA1.Text = String.Empty Then
                grdKDDIAGNOSA1.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDIAGNOSA1.ErrorText = Statement.ErrorRequired

                grdKDDIAGNOSA1.Focus()
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
            Dim ds = oDiagnosaMaster.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oDiagnosaMaster.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDDIAGNOSAMASTER = sNoId
                .JENIS = txtJENIS.Text
                .INACBG = txtINACBG.Text
                .KLSI = CDec(txtKLSI.Text)
                .KLS2 = CDec(txtKLSII.Text)
                .KLS3 = CDec(txtKLSIII.Text)
                .DESCRIPTION = txtKET.Text
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
                .KDUSER = sUserID
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    txtCODE.Text = oDiagnosaMaster.InsertData(ds)
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
                    fn_Save = oDiagnosaMaster.UpdateData(ds)
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
    Private Sub CetakRegister(ByVal KDDiagnosaMaster As String)
        'Dim rpt As New xtraDiagnosaMaster

        'Dim ds = oDiagnosaMaster.GetData(KDDiagnosaMaster)
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
            Dim uTime As Integer = 0

            If sVclaim_ConsId <> "" Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.CariSEP(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, NOMORSEP)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        fn_CariSEP = True
                        'Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime))
                    Else
                        fn_CariSEP = False
                    End If
                Else
                    fn_CariSEP = False
                End If
            Else
                fn_CariSEP = False
            End If
        Catch oErr As Exception
            fn_CariSEP = False
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
            grdKDPROCEDURE1.Properties.ValueMember = "KDPROSEDUR"
            grdKDPROCEDURE1.Properties.DisplayMember = "MEMO"

            grdKDPROCEDURE2.Properties.DataSource = dsDiagnosa.ToList()
            grdKDPROCEDURE2.Properties.ValueMember = "KDPROSEDUR"
            grdKDPROCEDURE2.Properties.DisplayMember = "MEMO"

            grdKDPROCEDURE3.Properties.DataSource = dsDiagnosa.ToList()
            grdKDPROCEDURE3.Properties.ValueMember = "KDPROSEDUR"
            grdKDPROCEDURE3.Properties.DisplayMember = "MEMO"

            grdKDPROCEDURE4.Properties.DataSource = dsDiagnosa.ToList()
            grdKDPROCEDURE4.Properties.ValueMember = "KDPROSEDUR"
            grdKDPROCEDURE4.Properties.DisplayMember = "MEMO"

            grdKDPROCEDURE5.Properties.DataSource = dsDiagnosa.ToList()
            grdKDPROCEDURE5.Properties.ValueMember = "KDPROSEDUR"
            grdKDPROCEDURE5.Properties.DisplayMember = "MEMO"

            grdKDPROCEDURE6.Properties.DataSource = dsDiagnosa.ToList()
            grdKDPROCEDURE6.Properties.ValueMember = "KDPROSEDUR"
            grdKDPROCEDURE6.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

#End Region
End Class