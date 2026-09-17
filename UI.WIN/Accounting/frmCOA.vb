Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmCOA
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oCOA As New Accounting.clsCOA
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
            Me.Text = COA.TITLE

            lKDCOA.Text = COA.KDCOA
            lNMCOA.Text = COA.NMCOA
            lPARENTKDCOA.Text = COA.PARENTKDCOA
            lTYPE.Text = COA.TYPE
            lGROUPS.Text = COA.GROUPS
            lDC.Text = COA.DC

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDCOA.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadPARENTCOA()

        Select Case oFormMode
            Case FORM_MODE.FORM_MODE_VIEW
                fn_ViewMode(True)
                fn_LoadData()
                txtKDCOA.Properties.ReadOnly = True
            Case FORM_MODE.FORM_MODE_ADD
                fn_ViewMode(False)
                fn_EmptyMe()
                txtKDCOA.Properties.ReadOnly = False
            Case FORM_MODE.FORM_MODE_EDIT
                fn_ViewMode(False)
                fn_LoadData()
                txtKDCOA.Properties.ReadOnly = True
                txtNMCOA.Focus()
            Case Else
                fn_ViewMode(True)
        End Select
    End Sub
    Private Sub fn_ViewMode(ByVal Status As Boolean)
        btnSaveNew.Enabled = Not Status
        btnSaveClose.Enabled = Not Status

        txtNMCOA.Properties.ReadOnly = Status
        rbTYPE.Properties.ReadOnly = Status
        grdPARENTKDCOA.Properties.ReadOnly = Status
        cboGROUPS.Properties.ReadOnly = Status
        cboDC.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDCOA.ResetText()
        txtNMCOA.ResetText()
        rbTYPE.SelectedIndex = 0
        grdPARENTKDCOA.ResetText()
        cboGROUPS.SelectedIndex = 0
        cboDC.SelectedIndex = 0
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oCOA.GetData(sNoId)

            With ds
                txtKDCOA.Text = .KDCOA
                txtNMCOA.Text = .NMCOA
                rbTYPE.SelectedIndex = .TYPE
                grdPARENTKDCOA.Text = .PARENTKDCOA
                cboGROUPS.Text = .GROUPS
                cboDC.Text = .DC
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtKDCOA.Text = String.Empty Then
                txtKDCOA.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCOA.ErrorText = Statement.ErrorRequired

                txtKDCOA.Focus()
                fn_Validate = False
                Exit Function
            End If
            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                If oCOA.IsExist(txtKDCOA.Text.ToUpper.Trim) = True Then
                    txtKDCOA.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    txtKDCOA.ErrorText = Statement.ErrorRegistered

                    txtKDCOA.Focus()
                    fn_Validate = False
                    Exit Function
                End If
            Else
                If txtKDCOA.Text.Trim.ToUpper <> oCOA.GetData(sNoId).KDCOA Then
                    If oCOA.IsExist(txtKDCOA.Text.ToUpper.Trim) = True Then
                        txtKDCOA.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                        txtKDCOA.ErrorText = Statement.ErrorRegistered

                        txtKDCOA.Focus()
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
            Dim ds = oCOA.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oCOA.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDCOA = txtKDCOA.Text.Trim.ToUpper
                .NMCOA = txtNMCOA.Text.Trim.ToUpper
                .TYPE = rbTYPE.SelectedIndex
                .PARENTKDCOA = grdPARENTKDCOA.EditValue.ToString.Trim.ToUpper
                .GROUPS = cboGROUPS.Text.Trim.ToUpper
                .DC = cboDC.Text.Trim.ToUpper
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oCOA.InsertData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oCOA.UpdateData(ds)
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
    Private Sub fn_LoadPARENTCOA()
        Dim oCOA As New Accounting.clsCOA
        Try
            grdPARENTKDCOA.Properties.DataSource = oCOA.GetData.ToList()
            grdPARENTKDCOA.Properties.ValueMember = "KDCOA"
            grdPARENTKDCOA.Properties.DisplayMember = "NMCOA"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdPARENTKDCOA_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdPARENTKDCOA.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdPARENTKDCOA.ResetText()
        End If
    End Sub
#End Region
End Class