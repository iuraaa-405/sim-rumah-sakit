Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json

Public Class frmSatuSehatKoneksiToken
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private sSEQ As Integer
    Private isLoad As Boolean = False
    Private oSatuSehatKoneksiToken As New Setting.clsSatuSehatKoneksiToken
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, Optional ByVal NoId As String = "", Optional ByVal SEQ As Integer = 0)
        oFormMode = FormMode
        sNoId = NoId
        sSEQ = SEQ
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "Satu Sehat Koneksi Token"

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'sCode = txtKDUSER.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        'fn_LoadKDKONEKSI()

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

        'If oFormMode = FORM_MODE.FORM_MODE_ADD Then
        '    grdKDKONEKSI.Properties.ReadOnly = False
        'Else
        '    grdKDKONEKSI.Properties.ReadOnly = True
        'End If

        cboCOLLECTIONS.Properties.ReadOnly = Status
        'txtURL.Properties.ReadOnly = Status
        txtGENERATETOKEN.Properties.ReadOnly = Status
        chkISACTIVE.Properties.ReadOnly = Status
        txtMEMO.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        cboCOLLECTIONS.ResetText()
        'txtURL.ResetText()
        txtGENERATETOKEN.ResetText()
        txtMEMO.ResetText()
        chkISACTIVE.Checked = True
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oSatuSehatKoneksiToken.GetData(sNoId, sSEQ)

            With ds
                'grdKDKONEKSI.Text = .KDKONEKSI
                cboCOLLECTIONS.Text = .COLLECTIONS
                'txtURL.Text = .URL
                txtGENERATETOKEN.Text = .GENERATETOKEN
                txtMEMO.Text = .MEMO
                chkISACTIVE.Checked = .ISACTIVE
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            'If grdKDKONEKSI.ErrorIconAlignment = ErrorIconAlignment.MiddleRight Then
            '    grdKDKONEKSI.ErrorText = Statement.ErrorRequired
            '    grdKDKONEKSI.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If
            If cboCOLLECTIONS.Text = String.Empty Then
                cboCOLLECTIONS.ErrorText = Statement.ErrorRequired
                cboCOLLECTIONS.Focus()
                fn_Validate = False
                Exit Function
            End If
            'If txtURL.Text = String.Empty Then
            '    txtURL.ErrorText = Statement.ErrorRequired
            '    txtURL.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oSatuSehatKoneksiToken.GetStructureHeader
            With ds
                .DATECREATED = Now
                .DATEUPDATED = Now
                .KDKONEKSI = IIf(SatuSehat_Production = False, "SANDBOX", "PRODUCTION")

                If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                    If sSEQ = 0 Then
                        Dim dsCollection = oSatuSehatKoneksiToken.GetDataSEQ(.KDKONEKSI)

                        If dsCollection Is Nothing Then
                            .SEQ = 0
                        Else
                            .SEQ = dsCollection.SEQ + 1
                        End If
                    Else
                        .SEQ = sSEQ
                    End If
                Else
                    .SEQ = sSEQ
                End If

                .COLLECTIONS = cboCOLLECTIONS.Text
                .URL = IIf(SatuSehat_Production = False, "https://api-satusehat-stg.dto.kemkes.go.id", "https://api-satusehat.kemkes.go.id")
                .GENERATETOKEN = txtGENERATETOKEN.Text.Trim
                .MEMO = txtMEMO.Text
                .KDUSER = sUserID
                .ISACTIVE = chkISACTIVE.Checked
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oSatuSehatKoneksiToken.InsertData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oSatuSehatKoneksiToken.UpdateData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
#End Region
#Region "Command Button"
    Private Sub frmUser_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    'Private Sub fn_LoadKDKONEKSI()
    '    Dim oSatuSehatKoneksi As New Setting.clsSatuSehatKoneksi
    '    Try
    '        grdKDKONEKSI.Properties.DataSource = oSatuSehatKoneksi.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
    '        grdKDKONEKSI.Properties.ValueMember = "KDKONEKSI"
    '        grdKDKONEKSI.Properties.DisplayMember = "KDKONEKSI"
    '    Catch oErr As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
    'Private Sub cboCOLLECTIONS_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCOLLECTIONS.SelectedIndexChanged
    '    If isLoad = True Then
    '        If SatuSehat_Url <> "" Then
    '            If grdKDKONEKSI.Text = "SANDBOX" And cboCOLLECTIONS.Text = "00. FHIR Resource" Then
    '                txtURL.Text = SatuSehat_Url & "/oauth2/v1/accesstoken?grant_type=client_credentials"
    '            Else
    '                MsgBox("Belum Tersedia", MsgBoxStyle.Exclamation, Me.Text)
    '            End If
    '        End If
    '    End If
    'End Sub
    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        Try
            If SatuSehat_Organisasi <> "" Then
                Dim token As String = SatusehatAuth.GetAccessToken(SatuSehat_Production, SatuSehat_client_id, SatuSehat_client_secret)

                If Not String.IsNullOrEmpty(token) Then
                    txtGENERATETOKEN.Text = SatusehatAuth.GetToken(token, "access_token")

                    txtMEMO.Text = IIf(token.Contains("Error"), token, FormatJson(token))

                    SatuSehat_token = txtGENERATETOKEN.Text
                    'MsgBox("✓ Token berhasil didapatkan!" & vbCrLf & txtGENERATETOKEN.Text, MsgBoxStyle.Exclamation, Me.Text)

                    'Dim frmPesanSatuSehat As New frmPesanSatuSehat
                    'frmPesanSatuSehat.fn_LoadJson(txtMEMO.Text)
                    'frmPesanSatuSehat.ShowDialog(Me)

                    btnSaveClose_Click()
                Else
                    MsgBox("✗ Gagal mendapatkan token. token kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                MsgBox("ID Organisasi masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function FormatJson(json As String) As String
        Try
            ' Parse the JSON string
            Dim parsedJson = JToken.Parse(json)

            ' Format with indentation
            Dim formattedJson = parsedJson.ToString(Formatting.Indented)

            Return formattedJson
        Catch ex As Exception
            Return "Invalid JSON: " & ex.Message
        End Try
    End Function
#End Region
End Class