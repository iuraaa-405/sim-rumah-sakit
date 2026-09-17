Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq

Public Class frmUpdate_Tanggal_Pulang
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oUpdate_Tanggal_Pulang As New Admission.clsUpdate_Tanggal_Pulang
    Private REQUEST As String = String.Empty
    Private RESPONSE As String = String.Empty

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
            Me.Text = Update_Tanggal_Pulang.TITLE

            lKDUPDATE_TANGGAL_PULANG.Text = Update_Tanggal_Pulang.KDUPDATE_TANGGAL_PULANG
            lNOMORSEP.Text = Update_Tanggal_Pulang.NOMORSEP & " *"
            lDATE.Text = Update_Tanggal_Pulang.TANGGAL

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

        txtNOMORSEP.Properties.ReadOnly = Status
        deDATE.Properties.ReadOnly = Status

    End Sub
    Private Sub fn_EmptyMe()
        txtCODE.Text = "<--- AUTO --->"

        txtNOMORSEP.ResetText()
        deDATE.DateTime = Now
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oUpdate_Tanggal_Pulang.GetData(sNoId)

            With ds
                txtCODE.Text = sNoId
                txtNOMORSEP.Text = .NOMORSEP
                deDATE.DateTime = .DATE

            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtNOMORSEP.Text = String.Empty Then
                txtNOMORSEP.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtNOMORSEP.ErrorText = Statement.ErrorRequired

                txtNOMORSEP.Focus()
                fn_Validate = False
                Exit Function
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_UpdateTanggalPulang() As Boolean
        Try
            fn_UpdateTanggalPulang = True

            Dim jsonRequest As String = String.Empty
            Dim noSep As String = String.Empty
            Dim tglPulang As String = String.Empty
            Dim user As String = String.Empty

            noSep = txtNOMORSEP.Text.Trim.ToUpper
            tglPulang = deDATE.DateTime.ToString("yyyy-MM-dd HH:mm:ss")
            user = sUserID

            jsonRequest = "{ "
            jsonRequest &= """request"" :  { "
            jsonRequest &= """t_sep"": { "
            jsonRequest &= """noSep"": """ & noSep & ""","
            jsonRequest &= """tglPulang"": """ & tglPulang & ""","
            jsonRequest &= """user"": """ & user & """"
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "} "

            Dim oSetKoneksi As New Brigging.clsSetKoneksi

            Dim dsSetKoneksi = oSetKoneksi.UpdateTanggalPulang("VCLAIM", jsonRequest)

            If dsSetKoneksi <> "" Then
                Dim allData = JObject.Parse(dsSetKoneksi)
                Dim CodeResponse As String = String.Empty
                Dim messageResponse As String = String.Empty

                CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                messageResponse = allData("metaData")("message").ToString

                If CodeResponse = "200" Then
                    REQUEST = jsonRequest
                    RESPONSE = dsSetKoneksi
                Else
                    fn_UpdateTanggalPulang = False
                    MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If

        Catch oErr As Exception
            fn_UpdateTanggalPulang = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oUpdate_Tanggal_Pulang.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oUpdate_Tanggal_Pulang.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .KDUPDATE_TANGGAL_PULANG = sNoId
                .NOMORSEP = txtNOMORSEP.Text.Trim.ToUpper
                .KDUSER = sUserID
                .REQUEST = REQUEST
                .RESPON = RESPONSE

            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oUpdate_Tanggal_Pulang.InsertData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oUpdate_Tanggal_Pulang.UpdateData(ds)
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

        REQUEST = String.Empty
        RESPONSE = String.Empty

        If fn_UpdateTanggalPulang() = True Then
            If fn_Save() = False Then
                MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
            Else
                MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
                sStatusSave = "NEW"
                Me.Close()
            End If
        End If

    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        REQUEST = String.Empty
        RESPONSE = String.Empty

        If fn_UpdateTanggalPulang() = True Then
            If fn_Save() = False Then
                MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
            Else
                MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
                Me.Close()
            End If
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"

#End Region
End Class