Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmFlowchartNICU
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oNICU As New Flowchart.clsNICU
    Private sKDKUNJUNGAN As String = String.Empty
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDKUNJUNGAN As String, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId
        sKDKUNJUNGAN = KDKUNJUNGAN
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "Flow Chart NICU"

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

        deDATE.Properties.ReadOnly = Status
        txtMEMO.Properties.ReadOnly = Status
        grv_1.OptionsBehavior.ReadOnly = Status
        grv_2.OptionsBehavior.ReadOnly = Status
        grv_3.OptionsBehavior.ReadOnly = Status
        grv_4.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtCODE.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        txtMEMO.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oNICU.GetData(sNoId)

            With ds
                txtCODE.Text = .KDFLOWCHARTNICU
                deDATE.DateTime = .DATE
                txtMEMO.Text = .DESCRIPTION

                bindingSource_1.DataSource = oNICU.GetDataDetail_1(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grd_1.DataSource = bindingSource_1

                BindingSource_2.DataSource = oNICU.GetDataDetail_2(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grd_2.DataSource = BindingSource_2

                BindingSource_3.DataSource = oNICU.GetDataDetail_3(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grd_3.DataSource = BindingSource_3

                BindingSource_4.DataSource = oNICU.GetDataDetail_4(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grd_4.DataSource = BindingSource_4
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If sKDKUNJUNGAN = String.Empty Then
                MsgBox("Kode Kunjungan Kosong", MsgBoxStyle.Exclamation, Me.Text)

                fn_Validate = False
                Exit Function
            End If

            grv_1.UpdateCurrentRow()
            grv_2.UpdateCurrentRow()
            grv_3.UpdateCurrentRow()
            grv_4.UpdateCurrentRow()

            'If grv_1.RowCount < 2 Then
            '    MsgBox(Statement.ErrorDetail, MsgBoxStyle.Exclamation, Me.Text)
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
            Dim ds = oNICU.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oNICU.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDFLOWCHARTNICU = sNoId
                .DATE = deDATE.DateTime
                .KDKUNJUNGAN = sKDKUNJUNGAN
                .DESCRIPTION = txtMEMO.Text.Trim.ToUpper
                .KDUSER = sUserID
                Try
                    .ISACTIVE = oNICU.GetData(sNoId).ISACTIVE
                Catch oErr As Exception
                    .ISACTIVE = False
                End Try
            End With

            ' ***** DETIL 1 *****
            Dim arrDetail_1 = oNICU.GetStructureDetailList_1
            For i As Integer = 0 To grv_1.RowCount - 2
                Dim dsDetail = oNICU.GetStructureDetail_1
                With dsDetail
                    .SEQ = i
                    .KDFLOWCHARTNICU = ds.KDFLOWCHARTNICU
                    .KATEGORI = grv_1.GetRowCellValue(i, colKATEGORI)
                    .JAM = grv_1.GetRowCellValue(i, colJAM_1)
                    .JAM_INT = CDate(grv_1.GetRowCellValue(i, colJAM_1)).ToString("HH") + CDate(grv_1.GetRowCellValue(i, colJAM_1)).ToString("mm") / 60
                    .NILAI = grv_1.GetRowCellValue(i, colANGKA)
                    .REMARKS = IIf(String.IsNullOrEmpty(grv_1.GetRowCellValue(i, colREMARKS)), "-", grv_1.GetRowCellValue(i, colREMARKS))
                End With
                arrDetail_1.Add(dsDetail)
            Next

            '' ***** DETIL 2 *****
            Dim arrDetail_2 = oNICU.GetStructureDetailList_2
            'For i As Integer = 0 To grv_2.RowCount - 2
            '    Dim dsDetail = oNICU.GetStructureDetail_2
            '    With dsDetail
            '        .SEQ = i
            '        .KDFLOWCHARTNICU = ds.KDFLOWCHARTNICU
            '        .JAM = grv_2.GetRowCellValue(i, colJAM_2)
            '        Dim dt = Date.Parse(grv_2.GetRowCellValue(i, colJAM_2)) ' presumes that this is the correct format
            '        Dim ticks As Long = dt.Ticks
            '        .JAM_INT = ticks
            '        '.KESADARAN = grv_2.GetRowCellValue(i, colKESADARAN)
            '        '.SATURASI_OKSIGEN = grv_2.GetRowCellValue(i, colSATURASI_OKSIGEN)
            '        '.WARNA_KULIT = grv_2.GetRowCellValue(i, colWARNA_KULIT)
            '        '.LINGKAR_KEPALA = grv_2.GetRowCellValue(i, colLINGKAR_KEPALA)
            '        '.LINGKAR_PERUT = grv_2.GetRowCellValue(i, colLINGKAR_PERUT)
            '        '.SUHU_INKUBATOR = grv_2.GetRowCellValue(i, colSUHU_INKUBATOR)
            '        '.KELEMBABAN_INKUBATOR = grv_2.GetRowCellValue(i, colKELEMBABAN_INKUBATOR)
            '        '.REMARKS = IIf(String.IsNullOrEmpty(grv_2.GetRowCellValue(i, colREMARKS_2)), "-", grv_2.GetRowCellValue(i, colREMARKS_2))
            '    End With
            '    arrDetail_2.Add(dsDetail)
            'Next

            ' ***** DETIL 3 *****
            Dim arrDetail_3 = oNICU.GetStructureDetailList_3
            For i As Integer = 0 To grv_3.RowCount - 2
                Dim dsDetail = oNICU.GetStructureDetail_3
                With dsDetail
                    .SEQ = i
                    .KDFLOWCHARTNICU = ds.KDFLOWCHARTNICU
                    .KATEGORI = grv_3.GetRowCellValue(i, colKATEGORI_3)
                    .JAM = grv_3.GetRowCellValue(i, colJAM_3)
                    Dim dt = Date.Parse(grv_3.GetRowCellValue(i, colJAM_3)) ' presumes that this is the correct format
                    Dim ticks As Long = dt.Ticks
                    .JAM_INT = ticks
                    .NILAI = grv_3.GetRowCellValue(i, colNILAI_3)
                    .REMARKS = IIf(String.IsNullOrEmpty(grv_3.GetRowCellValue(i, colREMARKS_3)), "-", grv_3.GetRowCellValue(i, colREMARKS_3))
                End With
                arrDetail_3.Add(dsDetail)
            Next

            ' ***** DETIL 4 *****
            Dim arrDetail_4 = oNICU.GetStructureDetailList_4
            For i As Integer = 0 To grv_4.RowCount - 2
                Dim dsDetail = oNICU.GetStructureDetail_4
                With dsDetail
                    .SEQ = i
                    .KDFLOWCHARTNICU = ds.KDFLOWCHARTNICU
                    .JAM = grv_4.GetRowCellValue(i, colJAM_4)
                    Dim dt = Date.Parse(grv_4.GetRowCellValue(i, colJAM_4)) ' presumes that this is the correct format
                    Dim ticks As Long = dt.Ticks
                    .JAM_INT = ticks
                    .TIPE = grv_4.GetRowCellValue(i, colTIPE)
                    .TV_AKTUAL = grv_4.GetRowCellValue(i, colTV_AKTUAL)
                    .MV_AKTUAL = grv_4.GetRowCellValue(i, colMV_AKTUAL)
                    .RESPIRATORI_RATE = grv_4.GetRowCellValue(i, colRESPIRATORI_RATE)
                    .PEEP = grv_4.GetRowCellValue(i, colPEEP)
                    .PIP_AKTUAL = grv_4.GetRowCellValue(i, colPIP_AKTUAL)
                    .IPL = grv_4.GetRowCellValue(i, colIPL)
                    .LEO_RATIO = grv_4.GetRowCellValue(i, colLEO_RATIO)
                    .FIO2 = grv_4.GetRowCellValue(i, colFIO2)
                    .FLOW = grv_4.GetRowCellValue(i, colFLOW)
                    .IT = grv_4.GetRowCellValue(i, colIT)
                    .REMARKS = "-"
                End With
                arrDetail_4.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oNICU.InsertData(ds, arrDetail_1, arrDetail_2, arrDetail_3, arrDetail_4)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oNICU.UpdateData(ds, arrDetail_1, arrDetail_2, arrDetail_3, arrDetail_4)
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
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grv_1.DeleteSelectedRows()
    End Sub
    Private Sub Delete_2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Delete_2.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grv_2.DeleteSelectedRows()
    End Sub
    Private Sub Delete_3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Delete_3.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grv_3.DeleteSelectedRows()
    End Sub

    Private Sub grv_1_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grv_1.CellValueChanged
        If e.Column.Name = colKATEGORI.Name Then
            Try
                If grv_1.GetFocusedRowCellValue(colKATEGORI) IsNot Nothing Then
                    grv_1.SetFocusedRowCellValue(colJAM_1, Now)
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub grv_2_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grv_2.CellValueChanged
        If e.Column.Name = colKESADARAN.Name Then
            Try
                If grv_2.GetFocusedRowCellValue(colKESADARAN) IsNot Nothing Then
                    grv_2.SetFocusedRowCellValue(colJAM_2, Now)
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub grv_3_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grv_3.CellValueChanged
        If e.Column.Name = colKATEGORI_3.Name Then
            Try
                If grv_3.GetFocusedRowCellValue(colKATEGORI_3) IsNot Nothing Then
                    grv_3.SetFocusedRowCellValue(colJAM_3, Now)
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub grv_4_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grv_4.CellValueChanged
        If e.Column.Name = colTIPE.Name Then
            Try
                If grv_4.GetFocusedRowCellValue(colTIPE) IsNot Nothing Then
                    grv_4.SetFocusedRowCellValue(colJAM_4, Now)
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmNICU_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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

#End Region
End Class