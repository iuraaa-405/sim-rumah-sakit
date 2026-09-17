Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmHasilLabMaster
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oHasilLabMaster As New Reference.clsHasilLabMaster
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
            Me.Text = "Reference Hasil Laboratorium"

            chkISACTIVE.Text = "Aktif?"

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtJUDUL.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadJenisTarif()
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

        txtJUDUL.Properties.ReadOnly = Status
        chkISACTIVE.Properties.ReadOnly = Status
        grdKDITEM.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtJUDUL.ResetText()
        chkISACTIVE.Checked = True
        grdKDITEM.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oHasilLabMaster.GetData(sNoId)

            With ds
                txtJUDUL.Text = .JUDUL
                chkISACTIVE.Checked = .ISACTIVE
                grdKDITEM.Text = .KDITEM
            End With

            BindingSource.DataSource = oHasilLabMaster.GetDataDetail(sNoId).OrderBy(Function(x) x.SEQ).ToList()
            grdTemplate.DataSource = BindingSource
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtJUDUL.Text = String.Empty Then
                txtJUDUL.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtJUDUL.ErrorText = Statement.ErrorRequired

                txtJUDUL.Focus()
                fn_Validate = False
                Exit Function
            End If

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                If grdKDITEM.Text <> "" Then
                    Dim dsCek = oHasilLabMaster.GetDataByKDITEM(grdKDITEM.EditValue)
                    If dsCek IsNot Nothing Then
                        MsgBox("Item Sudah Ada di Hasil Judul " & dsCek.JUDUL, MsgBoxStyle.Exclamation, Me.Text)
                        grdKDITEM.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                        grdKDITEM.ErrorText = Statement.ErrorRequired

                        grdKDITEM.Focus()
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
            Dim ds = oHasilLabMaster.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oHasilLabMaster.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDHASILLAB = sNoId
                .JUDUL = txtJUDUL.Text.Trim
                .PEMERIKSAAN_GROUP = ""
                .ISACTIVE = chkISACTIVE.Checked
                .ISDEFAULT = False
                .KDITEM = grdKDITEM.EditValue
            End With

            Dim arrDetail = oHasilLabMaster.GetStructureDetailList
            For i As Integer = 0 To grvTemplate.RowCount - 2
                Dim dsDetail_Tempalte = oHasilLabMaster.GetStructureDetail
                With dsDetail_Tempalte
                    .DATECREATED = ds.DATECREATED
                    .DATEUPDATED = ds.DATEUPDATED
                    .KDHASILLAB = sNoId
                    .SEQ = i
                    .ISGROUP = IIf(String.IsNullOrEmpty(grvTemplate.GetRowCellValue(i, colISGROUP)), False, grvTemplate.GetRowCellValue(i, colISGROUP))
                    .PEMERIKSAAN = IIf(String.IsNullOrEmpty(grvTemplate.GetRowCellValue(i, colPEMERIKSAAN)), String.Empty, grvTemplate.GetRowCellValue(i, colPEMERIKSAAN))
                    .HASIL = IIf(String.IsNullOrEmpty(grvTemplate.GetRowCellValue(i, colHASIL)), String.Empty, grvTemplate.GetRowCellValue(i, colHASIL))
                    .NILAIRUJUKAN = IIf(String.IsNullOrEmpty(grvTemplate.GetRowCellValue(i, colNILAIRUJUKAN)), String.Empty, grvTemplate.GetRowCellValue(i, colNILAIRUJUKAN))
                    .SATUAN = IIf(String.IsNullOrEmpty(grvTemplate.GetRowCellValue(i, colSATUAN)), String.Empty, grvTemplate.GetRowCellValue(i, colSATUAN))
                    .KETERANGAN = IIf(String.IsNullOrEmpty(grvTemplate.GetRowCellValue(i, colKETERANGAN)), String.Empty, grvTemplate.GetRowCellValue(i, colKETERANGAN))
                    .NILAI1 = IIf(String.IsNullOrEmpty(grvTemplate.GetRowCellValue(i, colNILAI1)), 0, grvTemplate.GetRowCellValue(i, colNILAI1))
                    .NILAI2 = IIf(String.IsNullOrEmpty(grvTemplate.GetRowCellValue(i, colNILAI2)), 0, grvTemplate.GetRowCellValue(i, colNILAI2))
                End With
                arrDetail.Add(dsDetail_Tempalte)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oHasilLabMaster.InsertData(ds, arrDetail)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oHasilLabMaster.UpdateData(ds, arrDetail)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvTemplate.DeleteSelectedRows()
    End Sub
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
    Private Sub fn_LoadJenisTarif()
        Dim oJenisTarif As New Reference.clsItem
        Try
            grdKDITEM.Properties.DataSource = oJenisTarif.GetData.Where(Function(x) x.ISACTIVE = True And x.M_ITEM_L3.MEMO.Contains("LABORATORIUM")).ToList()
            grdKDITEM.Properties.ValueMember = "KDITEM"
            grdKDITEM.Properties.DisplayMember = "NMITEM2"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grvDetail_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvTemplate.CellValueChanged
        If e.Column.Name = colPEMERIKSAAN.Name Then
            Try
                If grvTemplate.GetFocusedRowCellValue(colPEMERIKSAAN) IsNot Nothing Then
                    grvTemplate.SetFocusedRowCellValue(colISGROUP, False)
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        grdKDITEM.ResetText()
    End Sub
#End Region
End Class