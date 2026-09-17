Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq

Public Class frmKelasAplicareBed
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oKelasAplicareBed As New Reference.clsKelasAplicareBed

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
            Me.Text = "Tempat Tidur - Bed"

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
    End Sub
    Private Sub fn_ViewMode(ByVal Status As Boolean)
        btnSaveNew.Enabled = Not Status
        btnSaveClose.Enabled = Not Status
        'btnKetersediaanKamar.Enabled = Not Status
    End Sub
    Private Sub fn_EmptyMe()

    End Sub
    Private Sub fn_LoadData()
        Try
            BindingSource.DataSource = oKelasAplicareBed.GetDataDetailAplicare(sNoId).OrderBy(Function(x) x.KODEBED).ToList()
            grdDetail_UOM.DataSource = BindingSource
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If sNoId = "" Then
                MsgBox("Dibutuhkan Id", MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End If

            If grvDetail_UOM.RowCount < 2 Then
                MsgBox("Dibutuhkan Detil", MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End If

            For i As Integer = 0 To grvDetail_UOM.RowCount - 2
                If grvDetail_UOM.GetRowCellValue(i, colKODEBED) = "" Then
                    MsgBox("Kode Ada yg kosong", MsgBoxStyle.Exclamation, Me.Text)
                    fn_Validate = False
                    Exit Function
                End If
            Next
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** Satuan *****
            Dim arrDetail = oKelasAplicareBed.GetStructureDetailList
            For i As Integer = 0 To grvDetail_UOM.RowCount - 2
                Dim dsDetail = oKelasAplicareBed.GetStructureDetail
                With dsDetail
                    Try
                        .DATECREATED = oKelasAplicareBed.GetDatadefaultKelasAplicare(sNoId).DATECREATED
                    Catch oErr As Exception
                        .DATECREATED = Now
                    End Try
                    .DATEUPDATED = Now
                    .KDUPDATE_APLICARE = sNoId

                    Dim oKelasAplicare As New Reference.clsKelasAplicare

                    .KDKELASAPLICARE = oKelasAplicare.GetDataDetail_UOM(sNoId).KDKELASAPLICARE
                    .KDDEPARTMENT = oKelasAplicare.GetDataDetail_UOM(sNoId).KDDEPARTMENT
                    .BED = grvDetail_UOM.GetRowCellValue(i, colBED)
                    .KDPENDAFTARAN = IIf(String.IsNullOrEmpty(grvDetail_UOM.GetRowCellValue(i, colKDPENDAFTARAN)), "", grvDetail_UOM.GetRowCellValue(i, colKDPENDAFTARAN))
                    .STATUS = IIf(String.IsNullOrEmpty(grvDetail_UOM.GetRowCellValue(i, colSTATUS)), "", grvDetail_UOM.GetRowCellValue(i, colSTATUS))
                    .MEMO = IIf(String.IsNullOrEmpty(grvDetail_UOM.GetRowCellValue(i, colMEMO)), "", grvDetail_UOM.GetRowCellValue(i, colMEMO))
                    .KODEBED = IIf(String.IsNullOrEmpty(grvDetail_UOM.GetRowCellValue(i, colKODEBED)), "", grvDetail_UOM.GetRowCellValue(i, colKODEBED))
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                fn_Save = oKelasAplicareBed.InsertData(arrDetail)
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                fn_Save = oKelasAplicareBed.UpdateData(arrDetail)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail_UOM.DeleteSelectedRows()
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
                'Case Keys.F5
                '    If btnKetersediaanKamar.Enabled = True Then
                '        btnKetersediaanKamar_Click()
                '    End If
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
            ' MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Grid Method"
    Private Sub grdDetail_UOM_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDetail_UOM.CellValueChanged
        If e.Column.Name = colKODEBED.Name Then
            'Dim oDepartment As New Reference.clsDepartment
            'Try
            '    If grvDetail_UOM.GetFocusedRowCellValue(colKDUOM) IsNot Nothing Then
            '        Dim ds = oDepartment.GetData(grvDetail_UOM.GetFocusedRowCellValue(colKDUOM))

            '        If ds IsNot Nothing Then
            '            Dim cek As Integer = 0
            '            For i As Integer = 0 To grvDetail_UOM.RowCount - 2
            '                If grvDetail_UOM.GetRowCellValue(i, colKDUOM) = grvDetail_UOM.GetFocusedRowCellValue(colKDUOM) Then
            '                    cek = cek + 1
            '                End If
            '            Next

            '            If cek <> 0 Then
            '                MsgBox("Ruangan sudah ada di list!", MsgBoxStyle.Critical, Me.Text)
            '                grvDetail_UOM.CancelUpdateCurrentRow()
            '            End If
            '        Else
            '            MsgBox(Statement.ErrorUOM, MsgBoxStyle.Exclamation, Me.Text)

            '            grvDetail_UOM.CancelUpdateCurrentRow()
            '        End If
            '    End If
            'Catch oErr As Exception
            '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            'End Try
            'ElseIf e.Column.Name = colTERSEDIA_LAKI.Name Or e.Column.Name = colTERSEDIA_PEREMPUAN.Name Or e.Column.Name = colTERSEDIA_LAKIPEREMPUAN.Name Then
            '    Dim Jumlah As Decimal = grvDetail_UOM.GetFocusedRowCellValue(colTERSEDIA_LAKI) + grvDetail_UOM.GetFocusedRowCellValue(colTERSEDIA_PEREMPUAN) + grvDetail_UOM.GetFocusedRowCellValue(colTERSEDIA_LAKIPEREMPUAN)
            '    Dim Tersedia As Decimal = grvDetail_UOM.GetFocusedRowCellValue(colTERSEDIA)

            '    If Jumlah > Tersedia Then
            '        MsgBox("Tersedia tidak boleh lebih besar dari kapasitas", MsgBoxStyle.Exclamation, Me.Text)
            '        grvDetail_UOM.CancelUpdateCurrentRow()
            '    End If
            '    grvDetail_UOM.SetFocusedRowCellValue(colTERSEDIA, grvDetail_UOM.GetFocusedRowCellValue(colTERSEDIA_LAKI) + grvDetail_UOM.GetFocusedRowCellValue(colTERSEDIA_PEREMPUAN) + grvDetail_UOM.GetFocusedRowCellValue(colTERSEDIA_LAKIPEREMPUAN))

        ElseIf e.Column.Name = colSTATUS.Name Then
            'If grvDetail_UOM.GetFocusedRowCellValue(colSTATUS) <> "TERISI" Then
            '    MsgBox("Status Terisi", MsgBoxStyle.Exclamation, Me.Text)
            '    grvDetail_UOM.CancelUpdateCurrentRow()
            'End If
        End If
    End Sub
#End Region
#Region "Lookup / Event"

#End Region
End Class