Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmJadwalDokter
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oJadwalDokter As New Inventory.clsJadwalDokter
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
            Me.Text = JadwalDokter.TITLE

            lKDJADWALDOKTER.Text = JadwalDokter.KDJADWALDOKTER
            lKDDOCTOR.Text = JadwalDokter.KDDOCTOR & " *"
            lKDDEPARTMENT.Text = JadwalDokter.KDDEPARTMENT & " *"

            grvDetail.Columns("HARI").Caption = JadwalDokter.DETAIL_HARI
            grvDetail.Columns("KAPASITASPASIEN_TOTAL").Caption = "Kapasitas"
            grvDetail.Columns("KAPASITASPASIEN_JKN").Caption = "Kapasitas JKN"
            grvDetail.Columns("KAPASITASPASIEN_NONJKN").Caption = "Kapasitas Non JKN"
            grvDetail.Columns("LIBUR").Caption = JadwalDokter.DETAIL_LIBUR
            grvDetail.Columns("BUKA").Caption = JadwalDokter.DETAIL_BUKA
            grvDetail.Columns("TUTUP").Caption = JadwalDokter.DETAIL_TUTUP
            grvDetail.Columns("DESCRIPTION").Caption = JadwalDokter.DETAIL_DESCRIPTION

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDJADWALDOKTER.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        'fn_LoadKDDOCTOR()
        fn_LoadKDDEPARTMENT()
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

        grdKDDOCTOR.Properties.ReadOnly = Status
        grdKDDEPARTMENT.Properties.ReadOnly = Status
        txtMEMO.Properties.ReadOnly = Status

        grvDetail.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDJADWALDOKTER.Text = "<--- AUTO --->"
        grdKDDOCTOR.ResetText()
        grdKDDEPARTMENT.ResetText()
        txtMEMO.ResetText()
        grvDetail.OptionsSelection.MultiSelect = True
        grvDetail.SelectAll()
        grvDetail.DeleteSelectedRows()
        grvDetail.OptionsSelection.MultiSelect = False
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oJadwalDokter.GetData(sNoId)

            With ds
                txtKDJADWALDOKTER.Text = .KDJADWALDOKTER
                fn_LoadKDDOCTOR(.KDDEPARTMENT)
                grdKDDEPARTMENT.Text = .KDDEPARTMENT
                grdKDDOCTOR.Text = .KDDOCTOR
                txtMEMO.Text = .DESCRIPTION

                bindingSource.DataSource = oJadwalDokter.GetDataDetail.Where(Function(x) x.KDJADWALDOKTER = sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = bindingSource
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If grdKDDOCTOR.Text = String.Empty Then
                grdKDDOCTOR.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDOCTOR.ErrorText = Statement.ErrorRequired

                grdKDDOCTOR.Focus()
                fn_Validate = False
                Exit Function
            End If

            If grdKDDEPARTMENT.Text = String.Empty Then
                grdKDDEPARTMENT.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDEPARTMENT.ErrorText = Statement.ErrorRequired

                grdKDDEPARTMENT.Focus()
                fn_Validate = False
                Exit Function
            End If

            grvDetail.UpdateCurrentRow()

            If grvDetail.RowCount < 2 Then
                MsgBox(Statement.ErrorDetail, MsgBoxStyle.Exclamation, Me.Text)
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
            Dim ds = oJadwalDokter.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oJadwalDokter.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDJADWALDOKTER = sNoId
                .KDDOCTOR = grdKDDOCTOR.EditValue
                .KDDEPARTMENT = grdKDDEPARTMENT.EditValue
                .DESCRIPTION = txtMEMO.Text.Trim.ToUpper
                .KDUSER = sUserID

            End With

            ' ***** DETIL *****
            Dim arrDetail = oJadwalDokter.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oJadwalDokter.GetStructureDetail
                With dsDetail
                    .SEQ = i
                    .HARI = grvDetail.GetRowCellValue(i, colHARI)
                    .KDJADWALDOKTER = ds.KDJADWALDOKTER
                    .KAPASITASPASIEN_TOTAL = grvDetail.GetRowCellValue(i, colKAPASITASPASIEN_TOTAL)
                    .KAPASITASPASIEN_JKN = grvDetail.GetRowCellValue(i, colKAPASITASPASIEN_JKN)
                    .KAPASITASPASIEN_NONJKN = grvDetail.GetRowCellValue(i, colKAPASITASPASIEN_NONJKN)
                    .LIBUR = grvDetail.GetRowCellValue(i, colLIBUR)
                    .BUKA = grvDetail.GetRowCellValue(i, colBUKA)
                    .TUTUP = grvDetail.GetRowCellValue(i, colTUTUP)
                    .DESCRIPTION = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colDESCRIPTION)), "-", grvDetail.GetRowCellValue(i, colDESCRIPTION))
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oJadwalDokter.InsertData(ds, arrDetail)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oJadwalDokter.UpdateData(ds, arrDetail)
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
#Region "Grid Method"
    Private Sub grvDetail_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDetail.CellValueChanged
        'If e.Column.Name = colKDITEM.Name Then
        '    Dim oItem As New Reference.clsItem
        '    Try
        '        If grvDetail.GetFocusedRowCellValue(colKDITEM) IsNot Nothing Then
        '            Dim ds = oItem.GetDataDetail_UOM(grvDetail.GetFocusedRowCellValue(colKDITEM))

        '            If ds IsNot Nothing Then
        '                grvDetail.SetFocusedRowCellValue(colKDUOM, ds.FirstOrDefault(Function(x) x.RATE = 1).KDUOM)
        '            Else
        '                MsgBox(Statement.ErrorUOM, MsgBoxStyle.Exclamation, Me.Text)

        '                grvDetail.CancelUpdateCurrentRow()
        '            End If
        '        End If
        '    Catch oErr As Exception
        '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    End Try
        'ElseIf e.Column.Name = colKDUOM.Name Then
        '    Dim oItem As New Reference.clsItem
        '    Try
        '        If grvDetail.GetFocusedRowCellValue(colKDITEM) IsNot Nothing And grvDetail.GetFocusedRowCellValue(colKDUOM) IsNot Nothing Then
        '            Dim ds = oItem.GetDataDetail_UOM(grvDetail.GetFocusedRowCellValue(colKDITEM), grvDetail.GetFocusedRowCellValue(colKDUOM))

        '            If ds IsNot Nothing Then

        '            Else
        '                MsgBox(Statement.ErrorUOM, MsgBoxStyle.Exclamation, Me.Text)

        '                Dim sItem = grvDetail.GetFocusedRowCellValue(colKDITEM)
        '                grvDetail.CancelUpdateCurrentRow()

        '                grvDetail.AddNewRow()
        '                grvDetail.SetFocusedRowCellValue(colKDITEM, sItem)
        '            End If
        '        End If
        '    Catch oErr As Exception
        '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    End Try
        'ElseIf e.Column.Name = colQTY.Name Then
        '    If CDec(grvDetail.GetFocusedRowCellValue(colQTY)) = 0 Then
        '        grvDetail.SetFocusedRowCellValue(colQTY, 1)
        '    End If
        'End If
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail.DeleteSelectedRows()
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmJadwalDokter_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    Private Sub fn_LoadKDDOCTOR(ByVal Parameter As String)
        Dim oKDDOCTOR As New Reference.clsDoctor
        Try
            grdKDDOCTOR.Properties.DataSource = oKDDOCTOR.GetData.Where(Function(x) x.ISACTIVE = True And x.VCLAIM_KDDPJP <> "" And x.KDDEPARTMENT = Parameter).ToList()
            grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDDEPARTMENT()
        Dim oKDEAPRTMENT As New Reference.clsDepartment
        Try
            grdKDDEPARTMENT.Properties.DataSource = oKDEAPRTMENT.GetData.Where(Function(x) x.ISACTIVE = True And x.VCLAIM_KODEPOLI <> "").ToList()
            grdKDDEPARTMENT.Properties.ValueMember = "KDDEPARTMENT"
            grdKDDEPARTMENT.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDDEPARTMENT_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdKDDEPARTMENT.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If grdKDDEPARTMENT.Text = String.Empty Then Exit Sub
            fn_LoadKDDOCTOR(grdKDDEPARTMENT.EditValue)
        End If
    End Sub

#End Region
End Class