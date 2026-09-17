Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmLoincdanKfa
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oLoincdanKfa As New Reference.clsLoincdanKfa
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
            Me.Text = "Loinc dan Kfa"

            'lMEMO.Text = LoincdanKfa.MEMO & " *"
            'chkISACTIVE.Text = LoincdanKfa.ISACTIVE
            'chkISDEFAULT.Text = LoincdanKfa.ISDEFAULT

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
        fn_LoadKDICD9()
        fn_LoadKDKFA()

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

        txtMEMO.Properties.ReadOnly = Status
        txtMEMO1.Properties.ReadOnly = Status
        grdITEM_L1.Properties.ReadOnly = Status
        chkISACTIVE.Properties.ReadOnly = Status
        chkISDEFAULT.Properties.ReadOnly = Status

        If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            txtKDLOINCDANKFA.Properties.ReadOnly = False
        Else
            txtKDLOINCDANKFA.Properties.ReadOnly = True
        End If
    End Sub
    Private Sub fn_EmptyMe()
        txtMEMO.ResetText()
        txtMEMO1.ResetText()
        grdITEM_L1.ResetText()
        txtKDLOINCDANKFA.ResetText()
        chkISACTIVE.Checked = True
        chkISDEFAULT.Checked = False
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oLoincdanKfa.GetData(sNoId)

            With ds
                txtKDLOINCDANKFA.Text = .KDLOINCDANKFA
                txtMEMO.Text = .MEMO
                txtMEMO1.Text = .MEMO1
                grdITEM_L1.Text = .MEMO2
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
            If txtKDLOINCDANKFA.Text = String.Empty Then
                txtKDLOINCDANKFA.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDLOINCDANKFA.ErrorText = Statement.ErrorRequired

                txtKDLOINCDANKFA.Focus()
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
            'If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            '    If oLoincdanKfa.IsExist(txtMEMO.Text.ToUpper.Trim) = True Then
            '        txtMEMO.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '        txtMEMO.ErrorText = Statement.ErrorRegistered

            '        txtMEMO.Focus()
            '        fn_Validate = False
            '        Exit Function
            '    End If
            'Else
            '    If txtMEMO.Text.Trim.ToUpper <> oLoincdanKfa.GetData(sNoId).MEMO Then
            '        If oLoincdanKfa.IsExist(txtMEMO.Text.ToUpper.Trim) = True Then
            '            txtMEMO.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '            txtMEMO.ErrorText = Statement.ErrorRegistered

            '            txtMEMO.Focus()
            '            fn_Validate = False
            '            Exit Function
            '        End If
            '    End If
            'End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oLoincdanKfa.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oLoincdanKfa.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDLOINCDANKFA = txtKDLOINCDANKFA.Text
                .MEMO = txtMEMO.Text.Trim
                .MEMO1 = txtMEMO1.Text.Trim
                .MEMO2 = grdITEM_L1.EditValue
                .ISACTIVE = chkISACTIVE.Checked
                .ISDEFAULT = chkISDEFAULT.Checked
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oLoincdanKfa.InsertData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oLoincdanKfa.UpdateData(ds)
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
    Private Sub fn_LoadKDICD9()
        Try
            grdITEM_L1.Properties.DataSource = ListProseduriDRG.ToList()
            grdITEM_L1.Properties.ValueMember = "KDPROSEDUR"
            grdITEM_L1.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDKFA()
        Try
            grdKFA.Properties.DataSource = oLoincdanKfa.GetDataKFA.ToList()
            grdKFA.Properties.ValueMember = "kodegrouper"
            grdKFA.Properties.DisplayMember = "Kode_KFA_PA"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub chkISDEFAULT_CheckedChanged(sender As Object, e As EventArgs) Handles chkISDEFAULT.CheckedChanged
        If chkISDEFAULT.Checked = True Then
            lKFA.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            lKFA.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub
    Private Sub grdKFA_EditValueChanged(sender As Object, e As EventArgs) Handles grdKFA.EditValueChanged
        If isLoad = False Then Exit Sub

        If grdKFA.Text <> "" Then
            txtKDLOINCDANKFA.Text = grdKFA.EditValue
            Dim dskfa = oLoincdanKfa.GetDataKfa(grdKFA.EditValue)
            If dskfa IsNot Nothing Then
                txtMEMO.Text = dskfa.Product_Template_Display_Name
                txtMEMO1.Text = dskfa.Bahan_Baku_Aktif_Zat_Aktif_Display_Name
            End If
        End If
    End Sub
#End Region
End Class