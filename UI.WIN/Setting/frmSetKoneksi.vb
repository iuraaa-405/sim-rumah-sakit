Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmSetKoneksi
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oSetKoneksi As New Brigging.clsSetKoneksi
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
            Me.Text = SetKoneksi.TITLE

            lNAME_DISPLAY.Text = SetKoneksi.NAME_DISPLAY & " *"
            lPPKPELAYANAN.Text = SetKoneksi.PPKPELAYANAN & " *"
            lCONSID.Text = SetKoneksi.CONSID & " *"
            lSCREATKEY.Text = SetKoneksi.SCREATKEY & " *"
            lALAMATWEB.Text = SetKoneksi.ALMATWEB & " *"
            lREMARKS.Text = SetKoneksi.REMARKS
            chkISACTIVE.Text = SetKoneksi.ISACTIVE

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = cboNAME_DISPLAY.Text.Trim.ToUpper
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

        cboNAME_DISPLAY.Properties.ReadOnly = Status
        txtPPKPELAYANAN.Properties.ReadOnly = Status
        txtCONSID.Properties.ReadOnly = Status
        txtSCREATKEY.Properties.ReadOnly = Status
        txtALALAMTWEB.Properties.ReadOnly = Status
        txtREMARKS.Properties.ReadOnly = Status
        chkISACTIVEVERSI2.Properties.ReadOnly = Status
        chkISACTIVE.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        cboNAME_DISPLAY.SelectedIndex = 0
        txtPPKPELAYANAN.ResetText()
        txtCONSID.ResetText()
        txtSCREATKEY.ResetText()
        txtALALAMTWEB.ResetText()
        txtREMARKS.ResetText()
        chkISACTIVE.Checked = True

    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oSetKoneksi.GetData(sNoId)

            With ds
                cboNAME_DISPLAY.Text = .NAME_DISPLAY
                txtPPKPELAYANAN.Text = .PPKPELAYANAN
                txtCONSID.Text = .CONSID
                txtSCREATKEY.Text = .SECREATKEY
                txtALALAMTWEB.Text = .ALAMATWEB
                txtREMARKS.Text = .REMARKS
                chkISACTIVE.Checked = .ISACTIVE
                chkISACTIVEVERSI2.Checked = .ISACTIVEVERSI2
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If cboNAME_DISPLAY.Text = String.Empty Then
                cboNAME_DISPLAY.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                cboNAME_DISPLAY.ErrorText = Statement.ErrorRequired

                cboNAME_DISPLAY.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtPPKPELAYANAN.Text = String.Empty Then
                txtPPKPELAYANAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtPPKPELAYANAN.ErrorText = Statement.ErrorRequired

                txtPPKPELAYANAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtCONSID.Text = String.Empty Then
                txtCONSID.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtCONSID.ErrorText = Statement.ErrorRequired

                txtCONSID.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtSCREATKEY.Text = String.Empty Then
                txtSCREATKEY.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtSCREATKEY.ErrorText = Statement.ErrorRequired

                txtSCREATKEY.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtALALAMTWEB.Text = String.Empty Then
                txtALALAMTWEB.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtALALAMTWEB.ErrorText = Statement.ErrorRequired

                txtALALAMTWEB.Focus()
                fn_Validate = False
                Exit Function
            End If
            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                If oSetKoneksi.IsExist(txtCONSID.Text.ToUpper.Trim) = True Then
                    txtCONSID.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    txtCONSID.ErrorText = Statement.ErrorRegistered

                    txtCONSID.Focus()
                    fn_Validate = False
                    Exit Function
                End If
            Else
                If txtALALAMTWEB.Text.Trim.ToUpper <> oSetKoneksi.GetData(sNoId).NAME_DISPLAY Then
                    If oSetKoneksi.IsExist(txtCONSID.Text.ToUpper.Trim) = True Then
                        txtCONSID.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                        txtCONSID.ErrorText = Statement.ErrorRegistered

                        txtCONSID.Focus()
                        fn_Validate = False
                        Exit Function
                    End If
                End If
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oSetKoneksi.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oSetKoneksi.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDKONEKSI = sNoId
                .NAME_DISPLAY = cboNAME_DISPLAY.Text
                .PPKPELAYANAN = txtPPKPELAYANAN.Text
                .CONSID = txtCONSID.Text
                .SECREATKEY = txtSCREATKEY.Text
                .ALAMATWEB = txtALALAMTWEB.Text
                .REMARKS = txtREMARKS.Text
                .ISACTIVE = chkISACTIVE.Checked
                .ISACTIVEVERSI2 = chkISACTIVEVERSI2.Checked

                sAktiveVersi2 = chkISACTIVEVERSI2.Checked
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oSetKoneksi.InsertData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oSetKoneksi.UpdateData(ds)
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

#End Region
End Class