Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Text.RegularExpressions

Public Class frmSatuSehatKoneksi
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oSatuSehatKoneksi As New Setting.clsSatuSehatKoneksi
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
            Me.Text = "Satu Sehat Koneksi"

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

        If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            cboKDKONEKSI.Properties.ReadOnly = False
        Else
            cboKDKONEKSI.Properties.ReadOnly = True
        End If

        txtORGANIZATIONID.Properties.ReadOnly = Status
        txtCLIENTID.Properties.ReadOnly = Status
        txtCLIENTSECRET.Properties.ReadOnly = Status
        chkISACTIVE.Properties.ReadOnly = Status
        txtMEMO.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtORGANIZATIONID.ResetText()
        txtCLIENTID.ResetText()
        txtCLIENTSECRET.ResetText()
        txtMEMO.ResetText()
        chkISACTIVE.Checked = True
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oSatuSehatKoneksi.GetData(sNoId)

            With ds
                cboKDKONEKSI.Text = .KDKONEKSI
                txtORGANIZATIONID.Text = .ORGANIZATIONID
                txtCLIENTID.Text = .CLIENTID
                txtCLIENTSECRET.Text = .CLIENTSECRET
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

            If cboKDKONEKSI.ErrorIconAlignment = ErrorIconAlignment.MiddleRight Then
                cboKDKONEKSI.ErrorText = Statement.ErrorRequired
                cboKDKONEKSI.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtORGANIZATIONID.Text = String.Empty Then
                txtORGANIZATIONID.ErrorText = Statement.ErrorRequired
                txtORGANIZATIONID.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtCLIENTID.Text = String.Empty Then
                txtCLIENTID.ErrorText = Statement.ErrorRequired
                txtCLIENTID.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtCLIENTSECRET.Text = String.Empty Then
                txtCLIENTSECRET.ErrorText = Statement.ErrorPasswordRepeat
                txtCLIENTSECRET.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtMEMO.Text = String.Empty Then
                txtMEMO.ErrorText = Statement.ErrorPasswordRepeat
                txtMEMO.Focus()
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
            Dim ds = oSatuSehatKoneksi.GetStructureHeader
            With ds
                .KDKONEKSI = cboKDKONEKSI.Text
                .ORGANIZATIONID = txtORGANIZATIONID.Text.Trim
                .CLIENTID = txtCLIENTID.Text.Trim
                .CLIENTSECRET = txtCLIENTSECRET.Text.Trim
                .MEMO = txtMEMO.Text
                .ISACTIVE = chkISACTIVE.Checked
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oSatuSehatKoneksi.InsertData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oSatuSehatKoneksi.UpdateData(ds)
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
    Private Sub btnCEKORGANISASI_ID_Click(sender As Object, e As EventArgs) Handles btnCEKORGANISASI_ID.Click
        Try
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
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Try
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
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
#Region "Lookup / Event"

#End Region
End Class