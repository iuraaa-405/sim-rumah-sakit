Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmDigital_A_Dokumen
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private sKDIDENTITAS As Integer = 0
    Private isLoad As Boolean = False
    Private oDigital_A_Dokumen As New Digital.clsDigital_A_Dokumen
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDIDENTITAS As Integer, ByVal NoId As String)
        oFormMode = FormMode
        sKDIDENTITAS = KDIDENTITAS
        sNoId = NoId
        sCode = ""
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "Dokumen"
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDDOKUMEN()

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

        txtMEMO.ReadOnly = Status
        txtNAMADOKUMEN.ReadOnly = Status
        chkISACTIVE.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtNAMADOKUMEN.ResetText()
        txtMEMO.ResetText()
        chkISACTIVE.Checked = False
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oDigital_A_Dokumen.GetData(sNoId)

            With ds
                txtMEMO.RtfText = .MEMO
                txtNAMADOKUMEN.Text = .NAMADOKUMEN
                chkISACTIVE.Checked = .ISCHEKED
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If sKDIDENTITAS = 0 Then
                MsgBox("Dibutuhkan Kunjungan", MsgBoxStyle.Exclamation, Me.Text)
                txtMEMO.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtNAMADOKUMEN.Text = "" Then
                MsgBox("Dibutuhkan Nama Dokumen", MsgBoxStyle.Exclamation, Me.Text)
                txtMEMO.Focus()
                fn_Validate = False
                Exit Function
            End If
            'If sKDORDER = "" Then
            '    MsgBox("Dibutuhkan Nomor Order", MsgBoxStyle.Exclamation, Me.Text)
            '    txtMEMO.Focus()
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
            Dim ds = oDigital_A_Dokumen.GetStructureHeader
            With ds
                .DATECREATED = Now
                .DATEUPDATED = Now
                .KDIDENTITAS = sKDIDENTITAS
                .KDDKUMEN = sNoId
                .NAMADOKUMEN = txtNAMADOKUMEN.Text
                .MEMO = txtMEMO.RtfText
                .ISCHEKED = chkISACTIVE.Checked
                'Try
                '    .ISCHEKED = oDigital_A_Dokumen.GetData(sNoId).ISCHEKED
                'Catch oErr As Exception
                '    .ISCHEKED = False
                'End Try
                .KDUSER = sUserID
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oDigital_A_Dokumen.InsertData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oDigital_A_Dokumen.UpdateData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

            'If sKDORDER <> "" Then
            '    If fn_Save = True Then
            '        sCode = "OK"
            '        Dim oDigitalOrder As New Digital.clsR_Order
            '        oDigitalOrder.UpdateStatus(sKDORDER, "HASIL")
            '    Else
            '        sCode = ""
            '    End If
            'End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
#End Region
#Region "Command Button"
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        'Select Case e.KeyCode
        '    Case Keys.F12
        '        btnClose_Click()
        '    Case Keys.F2
        '        If btnSaveNew.Enabled = True Then
        '            btnSaveNew_Click()
        '        End If
        '    Case Keys.F3
        '        If btnSaveClose.Enabled = True Then
        '            btnSaveClose_Click()
        '        End If
        'End Select
    End Sub
    Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
            'sStatusSave = "NEW"
            'Me.Close()
            oFormMode = FORM_MODE.FORM_MODE_EDIT
        End If
    End Sub
    Private Sub btnReload_Click() Handles btnReload.Click
        fn_LoadRtfDokumen()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadKDDOKUMEN()
        Dim oDokumen As New Reference.clsDokumenMaster
        Try
            grdKDDOKUMEN.Properties.DataSource = oDokumen.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDOKUMEN.Properties.ValueMember = "KDDKUMEN"
            grdKDDOKUMEN.Properties.DisplayMember = "KDDKUMEN"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadRtfDokumen() 
        If grdKDDOKUMEN.Text <> "" Then
            Dim oDataGrouper As New Admission.clsPendaftaran_Kunjungan

            Dim dsIdentitas = oDataGrouper.GetDatabyIdentitas(sKDIDENTITAS)
            If dsIdentitas IsNot Nothing Then
                Dim oDokumen As New Reference.clsDokumenMaster
                Dim dsDokumen = oDokumen.GetData(grdKDDOKUMEN.EditValue)
                If dsDokumen IsNot Nothing Then
                    chkISACTIVE.Checked = dsDokumen.ISACTIVE
                    txtNAMADOKUMEN.Text = dsDokumen.KDDKUMEN
                    txtMEMO.RtfText = dsDokumen.MEMO
                End If
            End If
        End If
    End Sub
#End Region
End Class