Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmCash
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oCash As New Finance.clsCash

    Private sNOINVOICE As New List(Of String)
    Private sNOINVOICE_R As New List(Of String)
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
        fn_LoadSecurity()
    End Sub
    Private Sub fn_LoadSecurity()
        Try
            Dim oOtority As New Setting.clsOtority
            Dim oUser As New Setting.clsUser

            Dim ds = (From x In oOtority.GetDataDetail _
                     Join y In oUser.GetData _
                     On x.KDOTORITY Equals y.KDOTORITY _
                     Where x.MODUL = "ADMIN" _
                     And y.KDUSER = sUserID _
                     Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                If ds.ISVIEW = True Then
                    fn_LoadKDCOA()
                Else
                    fn_LoadKDCOA_NonAdmin()
                End If
            Catch ex As Exception
                MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)

                fn_LoadKDCOA_NonAdmin()
            End Try
        Catch ex As Exception
            MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)

            fn_LoadKDCOA_NonAdmin()
        End Try
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = Cash.TITLE

            lKDCASH.Text = Cash.KDCASH
            lDATE.Text = Cash.TANGGAL
            lKDPAYMENTTYPE.Text = Cash.KDPAYMENTTYPE & " *"
            lKDCOA.Text = Cash.KDCOA & " *"

            tab3.Text = Cash.TAB_MEMO

            lTOTAL.Text = Cash.TOTAL

            grvKDPAYMENTTYPE.Columns("MEMO").Caption = PaymentType.MEMO
            grvKDCOA.Columns("NMCOA").Caption = COA.NMCOA

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDCASH.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDPAYMENTTYPE()
        'fn_LoadKDCOA()
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

        deDATE.Properties.ReadOnly = Status
        grdKDPAYMENTTYPE.Properties.ReadOnly = Status
        grdKDCOA.Properties.ReadOnly = Status

        txtMEMO.Properties.ReadOnly = Status

        txtTOTAL.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDCASH.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        grdKDPAYMENTTYPE.ResetText()
        grdKDCOA.ResetText()
        txtMEMO.ResetText()
        txtTOTAL.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oCash.GetData(sNoId)

            With ds
                txtKDCASH.Text = .KDCASH
                deDATE.DateTime = .DATE
                grdKDPAYMENTTYPE.Text = .KDPAYMENTTYPE
                grdKDCOA.Text = .KDCOA
                txtMEMO.Text = .MEMO

                txtTOTAL.Text = .TOTAL
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If grdKDPAYMENTTYPE.Text = String.Empty Then
                grdKDPAYMENTTYPE.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDPAYMENTTYPE.ErrorText = Statement.ErrorRequired

                grdKDPAYMENTTYPE.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDCOA.Text = String.Empty Then
                grdKDCOA.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDCOA.ErrorText = Statement.ErrorRequired

                grdKDCOA.Focus()
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
            Dim ds = oCash.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oCash.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDCASH = sNoId
                .DATE = deDATE.DateTime
                .KDPAYMENTTYPE = IIf(String.IsNullOrEmpty(grdKDPAYMENTTYPE.EditValue), String.Empty, grdKDPAYMENTTYPE.EditValue)
                .KDCOA = IIf(String.IsNullOrEmpty(grdKDCOA.EditValue), String.Empty, grdKDCOA.EditValue)
                .MEMO = txtMEMO.Text.Trim.ToUpper
                .TOTAL = CDec(txtTOTAL.Text)
                .KDUSER = sUserID
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oCash.InsertData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oCash.UpdateData(ds)
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
    Private Sub frmCash_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    Private Sub fn_LoadKDPAYMENTTYPE()
        Dim oPAYMENTTYPE As New Reference.clsPaymentType
        Try
            grdKDPAYMENTTYPE.Properties.DataSource = oPAYMENTTYPE.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDPAYMENTTYPE.Properties.ValueMember = "KDPAYMENTTYPE"
            grdKDPAYMENTTYPE.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDPAYMENTTYPE_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdKDPAYMENTTYPE.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdKDPAYMENTTYPE.ResetText()
        End If
    End Sub
    Private Sub fn_LoadKDCOA()
        Dim oCOA As New Accounting.clsCOA
        Try
            grdKDCOA.Properties.DataSource = oCOA.GetData.Where(Function(x) x.TYPE = 1).ToList()
            grdKDCOA.Properties.ValueMember = "KDCOA"
            grdKDCOA.Properties.DisplayMember = "NMCOA"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDCOA_NonAdmin()
        Dim oCOA As New Accounting.clsCOA
        Try
            grdKDCOA.Properties.DataSource = oCOA.GetData.Where(Function(x) x.TYPE = 1 And (x.PARENTKDCOA = "1110-000" Or x.PARENTKDCOA = "1120-000" Or x.PARENTKDCOA = "6000-000")).ToList()
            grdKDCOA.Properties.ValueMember = "KDCOA"
            grdKDCOA.Properties.DisplayMember = "NMCOA"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDCOA_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdKDCOA.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdKDCOA.ResetText()
        End If
    End Sub
#End Region
End Class