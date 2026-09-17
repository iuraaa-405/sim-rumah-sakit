Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmCOAList
    Implements ILanguage

    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oCOA As New Accounting.clsCOA

#Region "Function"
    Private Sub Me_Load() Handles Me.Load
        Me.Text = COA.TITLE

        fn_LoadSecurity()

        Try
            tree.RestoreLayoutFromRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            tree.SaveLayoutToRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub fn_LoadSecurity()
        Try
            Dim oOtority As New Setting.clsOtority
            Dim oUser As New Setting.clsUser

            Dim ds = (From x In oOtority.GetDataDetail _
                     Join y In oUser.GetData _
                     On x.KDOTORITY Equals y.KDOTORITY _
                     Where x.MODUL = "ACCOUNT" _
                     And y.KDUSER = sUserID _
                     Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                picAdd.Enabled = ds.ISADD
                picDelete.Enabled = ds.ISDELETE
                picUpdate.Enabled = ds.ISUPDATE
                picPrint.Enabled = ds.ISPRINT
                picRefresh.Enabled = ds.ISVIEW

                If ds.ISVIEW = True Then
                    fn_LoadData()
                    fn_LoadLanguage()
                End If
            Catch oErr As Exception
                MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)

                picAdd.Enabled = False
                picDelete.Enabled = False
                picUpdate.Enabled = False
                picPrint.Enabled = False
                picRefresh.Enabled = False
            End Try
        Catch oErr As Exception
            MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            Me.Text = COA.TITLE

            tree.Columns("KDCOA").Caption = COA.KDCOA
            tree.Columns("NMCOA").Caption = COA.NMCOA

            lKDCOA.Text = COA.KDCOA
            lNMCOA.Text = COA.NMCOA
            lPARENTKDCOA.Text = COA.PARENTKDCOA
            lTYPE.Text = COA.TYPE
            lGROUPS.Text = COA.GROUPS
            lDC.Text = COA.DC

            btnView.Text = Caption.FormListButtonView
            groupControlInfo.Text = Caption.FormListGroupControlInfo
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = From x In oCOA.GetData _
                     Select x.PARENTKDCOA, x.KDCOA, x.NMCOA _
                     Order By KDCOA

            tree.DataSource = ds.ToList
            tree.ExpandAll()

            fn_LoadFormatData()

            If ds.Count < 1 Then Exit Sub

            fn_LoadDetail()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatData()

    End Sub
    Private Sub fn_LoadDetail()
        Try
            Dim ds = (From x In oCOA.GetData _
                     Where x.KDCOA = tree.FocusedNode.GetDisplayText("KDCOA") _
                     Select x).FirstOrDefault

            With ds
                txtKDCOA.Text = .KDCOA
                txtNMCOA.Text = .NMCOA

                If .PARENTKDCOA <> String.Empty Then
                    txtPARENTKDCOA.Text = .PARENTKDCOA & " " & oCOA.GetData(.PARENTKDCOA).NMCOA
                Else
                    txtPARENTKDCOA.Text = String.Empty
                End If


                If .TYPE = 0 Then
                    txtTYPE.Text = COA.TYPE_HEADER
                Else
                    txtTYPE.Text = COA.TYPE_DETAIL
                End If

                txtGROUPS.Text = .GROUPS

                If .DC = "D" Then
                    txtDC.Text = COA.DC_D
                Else
                    txtDC.Text = COA.DC_C
                End If
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_EmptyMe()
        txtKDCOA.ResetText()
        txtNMCOA.ResetText()
        txtTYPE.ResetText()
        txtGROUPS.ResetText()
        txtDC.ResetText()
    End Sub
    Private Function fn_DeleteData(ByVal sKDCOA As String) As Boolean
        Try
            oCOA.DeleteData(sKDCOA)
            fn_DeleteData = True
        Catch oErr As Exception
            fn_DeleteData = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
#End Region
#Region "Command Button"
    Private Sub frmMember_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.A
                If e.Alt = True And picAdd.Enabled = True Then
                    picAdd_Click()
                End If
            Case Keys.E
                If e.Alt = True And picUpdate.Enabled = True Then
                    picUpdate_Click()
                End If
            Case Keys.D
                If e.Alt = True And picDelete.Enabled = True Then
                    picDelete_Click()
                End If
            Case Keys.P
                If e.Alt = True And picPrint.Enabled = True Then
                    picPrint_Click()
                End If
            Case Keys.R
                If e.Alt = True And picRefresh.Enabled = True Then
                    picRefresh_Click()
                End If
        End Select
    End Sub
    Private Sub tree_FocusedNodeChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraTreeList.FocusedNodeChangedEventArgs) Handles tree.FocusedNodeChanged
        If tree.FocusedNode.GetDisplayText("KDCOA") = String.Empty Then
            fn_EmptyMe()
            Exit Sub
        End If

        fn_LoadDetail()
    End Sub
    Private Sub tree_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tree.DoubleClick, btnView.Click
        If tree.FocusedNode.GetDisplayText("KDCOA") = String.Empty Then
            fn_EmptyMe()
            Exit Sub
        End If

        Dim frmCOA As New frmCOA
        Try
            frmCOA.LoadMe(FORM_MODE.FORM_MODE_VIEW, tree.FocusedNode.GetDisplayText("KDCOA"))
            frmCOA.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub picAdd_Click() Handles picAdd.Click
        Dim frmCOA As New frmCOA
        Try
            frmCOA.LoadMe(FORM_MODE.FORM_MODE_ADD)
            frmCOA.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmCOA Is Nothing Then frmCOA.Dispose()
            frmCOA = Nothing

            tree.FocusedNode = tree.FindNodeByFieldValue("KDCOA", sCode)

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If tree.FocusedNode.GetDisplayText("KDCOA") = String.Empty Then
            fn_EmptyMe()
            Exit Sub
        End If
        Dim frmCOA As New frmCOA
        Try
            frmCOA.LoadMe(FORM_MODE.FORM_MODE_EDIT, tree.FocusedNode.GetDisplayText("KDCOA"))
            frmCOA.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmCOA Is Nothing Then frmCOA.Dispose()
            frmCOA = Nothing

            tree.FocusedNode = tree.FindNodeByFieldValue("KDCOA", sCode)

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If tree.FocusedNode.GetDisplayText("KDCOA") = String.Empty Then
            fn_EmptyMe()
            Exit Sub
        End If
        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_DeleteData(tree.FocusedNode.GetDisplayText("KDCOA")) = False Then
            MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        fn_LoadSecurity()
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        Try

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_LoadSecurity()
    End Sub
#End Region
End Class