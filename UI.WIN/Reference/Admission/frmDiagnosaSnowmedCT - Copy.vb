Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmDiagnosaSnowmedCT
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oDiagnosaSnowmedCT As New Reference.clsDiagnosaSnowmedCT
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
            'Me.Text = DiagnosaSnowmedCT.TITLE
            Me.Text = Diagnosa.TITLE & " Snowmed-CT"

            lMEMO.Text = Diagnosa.MEMO & " *"
            chkISACTIVE.Text = Diagnosa.ISACTIVE
            chkISDEFAULT.Text = Diagnosa.ISDEFAULT

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtMEMO.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDDIAGNOSA()

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
            grdKDDIAGNOSA.Properties.ReadOnly = False
            txtKDSNOWMED.Properties.ReadOnly = False
        Else
            grdKDDIAGNOSA.Properties.ReadOnly = True
            txtKDSNOWMED.Properties.ReadOnly = True
        End If

        txtMEMO.Properties.ReadOnly = Status
        chkISACTIVE.Properties.ReadOnly = Status
        chkISDEFAULT.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        grdKDDIAGNOSA.ResetText()
        txtKDSNOWMED.ResetText()
        chkISACTIVE.Checked = True
        chkISDEFAULT.Checked = False
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oDiagnosaSnowmedCT.GetData(sNoId)

            With ds
                grdKDDIAGNOSA.Text = .KDDIAGNOSA
                txtKDSNOWMED.Text = .KDSNOMED_CT
                txtMEMO.Text = .MEMO
                chkISACTIVE.Checked = .ISACTIVE
                chkISDEFAULT.Checked = .ISDEFAULT
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If grdKDDIAGNOSA.Text = String.Empty Then
                grdKDDIAGNOSA.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDIAGNOSA.ErrorText = Statement.ErrorRequired

                grdKDDIAGNOSA.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtKDSNOWMED.Text = String.Empty Then
                txtKDSNOWMED.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDSNOWMED.ErrorText = Statement.ErrorRequired

                txtKDSNOWMED.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtMEMO.Text = String.Empty Then
                txtMEMO.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtMEMO.ErrorText = Statement.ErrorRequired

                txtMEMO.Focus()
                fn_Validate = False
                Exit Function
            End If
            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                'If oDiagnosaSnowmedCT.IsExist(txtMEMO.Text.ToUpper.Trim) = True Then
                '    txtMEMO.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                '    txtMEMO.ErrorText = Statement.ErrorRegistered

                '    txtMEMO.Focus()
                '    fn_Validate = False
                '    Exit Function
                'End If
            Else
                Dim dsCek = oDiagnosaSnowmedCT.GetData(grdKDDIAGNOSA.EditValue)
                If dsCek IsNot Nothing Then
                    MsgBox("Sudah Ada diagnosa " & grdKDDIAGNOSA.EditValue, MsgBoxStyle.Exclamation, Me.Text)
                    grdKDDIAGNOSA.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    grdKDDIAGNOSA.ErrorText = Statement.ErrorRegistered

                    grdKDDIAGNOSA.Focus()
                    fn_Validate = False
                    Exit Function
                End If
                Dim dsCek2 = oDiagnosaSnowmedCT.GetDataSnowmed(txtKDSNOWMED.Text)
                If dsCek2 IsNot Nothing Then
                    MsgBox("Sudah Ada diagnosa " & txtKDSNOWMED.Text, MsgBoxStyle.Exclamation, Me.Text)
                    grdKDDIAGNOSA.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    grdKDDIAGNOSA.ErrorText = Statement.ErrorRegistered

                    grdKDDIAGNOSA.Focus()
                    fn_Validate = False
                    Exit Function
                End If
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oDiagnosaSnowmedCT.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oDiagnosaSnowmedCT.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDDIAGNOSA = grdKDDIAGNOSA.EditValue
                .KDSNOMED_CT = txtKDSNOWMED.Text
                .MEMO = txtMEMO.Text.Trim.ToUpper
                .ISACTIVE = chkISACTIVE.Checked
                .ISDEFAULT = chkISDEFAULT.Checked
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oDiagnosaSnowmedCT.InsertData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oDiagnosaSnowmedCT.UpdateData(ds)
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
    Private Sub fn_LoadKDDIAGNOSA()
        Dim oDiagnosa As New Reference.clsDiagnosa
        Try
            grdKDDIAGNOSA.Properties.DataSource = oDiagnosa.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDIAGNOSA.Properties.ValueMember = "KDDIAGNOSA"
            grdKDDIAGNOSA.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class