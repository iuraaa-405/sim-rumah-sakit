Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmItemLaboratorium
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oItem As New Reference.clsItem
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal NoId As String)
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
            Me.Text = "Item Laboratorium"

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'sCode = txtMEMO.Text.Trim.ToUpper
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

        'Dim ds = oItem.GetData(sNoId)
        'If ds IsNot Nothing Then
        '    txtNMITEM1.Text = ds.NMITEM1
        'End If
    End Sub
    Private Sub fn_ViewMode(ByVal Status As Boolean)
        btnSaveNew.Enabled = Not Status
        btnSaveClose.Enabled = Not Status

    End Sub
    Private Sub fn_EmptyMe()

    End Sub
    Private Sub fn_LoadData()
        Try
            'Dim ds = oItem.GetData(sNoId)

            'With ds
            '    txtMEMO.Text = .MEMO
            '    chkISACTIVE.Checked = .ISACTIVE
            '    chkISDEFAULT.Checked = .ISDEFAULT
            'End With

            BindingSourceTemplatelab.DataSource = oItem.GetDataDetail_TemplateLab(sNoId).OrderBy(Function(x) x.SEQ).ToList()
            grdTemplate.DataSource = BindingSourceTemplatelab

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If sNoId = String.Empty Then
                'txtMEMO.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                'txtMEMO.ErrorText = Statement.ErrorRequired

                'txtMEMO.Focus()
                MsgBox("Kode Item Kosong", MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** Template *****
            Dim arrDetail_Template = oItem.GetStructureDetail_TemplatLabList
            For i As Integer = 0 To grvTemplate.RowCount - 2
                Dim dsDetail_Tempalte = oItem.GetStructureDetail_TemplatLab
                With dsDetail_Tempalte
                    Try
                        .DATECREATED = oItem.GetData(sNoId).DATECREATED
                    Catch oErr As Exception
                        .DATECREATED = Now
                    End Try
                    .DATEUPDATED = Now
                    .KDITEM = sNoId
                    .SEQ = i
                    .PEMERIKSAAN = IIf(String.IsNullOrEmpty(grvTemplate.GetRowCellValue(i, colPEMERIKSAAN)), String.Empty, grvTemplate.GetRowCellValue(i, colPEMERIKSAAN))
                    .HASIL = IIf(String.IsNullOrEmpty(grvTemplate.GetRowCellValue(i, colHASIL)), String.Empty, grvTemplate.GetRowCellValue(i, colHASIL))
                    .NILAIRUJUKAN = IIf(String.IsNullOrEmpty(grvTemplate.GetRowCellValue(i, colNILAIRUJUKAN)), String.Empty, grvTemplate.GetRowCellValue(i, colNILAIRUJUKAN))
                    .NILAIRUJUKAN_1 = IIf(String.IsNullOrEmpty(grvTemplate.GetRowCellValue(i, colNILAIRUJUKAN_1)), 0, grvTemplate.GetRowCellValue(i, colNILAIRUJUKAN_1))
                    .NILAIRUJUKAN_2 = IIf(String.IsNullOrEmpty(grvTemplate.GetRowCellValue(i, colNILAIRUJUKAN_2)), 0, grvTemplate.GetRowCellValue(i, colNILAIRUJUKAN_2))
                    .SATUAN = IIf(String.IsNullOrEmpty(grvTemplate.GetRowCellValue(i, colSATUAN)), String.Empty, grvTemplate.GetRowCellValue(i, colSATUAN))
                    .KETERANGAN = IIf(String.IsNullOrEmpty(grvTemplate.GetRowCellValue(i, colKETERANGAN)), String.Empty, grvTemplate.GetRowCellValue(i, colKETERANGAN))
                    .ISACTIVE = True
                End With
                arrDetail_Template.Add(dsDetail_Tempalte)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oItem.UpdateDataItemLab(arrDetail_Template)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oItem.UpdateDataItemLab(arrDetail_Template)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Sub DeleteToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem1.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvTemplate.DeleteSelectedRows()
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClose_Click()
            'Case Keys.F2
            '    If btnSaveNew.Enabled = True Then
            '        btnSaveNew_Click()
            '    End If
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
        End Select
    End Sub
    'Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
    '    If fn_Validate() = False Then Exit Sub
    '    If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
    '    If fn_Save() = False Then
    '        MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
    '    Else
    '        MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
    '        sStatusSave = "NEW"
    '        Me.Close()
    '    End If
    'End Sub
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