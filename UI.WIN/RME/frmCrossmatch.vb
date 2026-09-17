Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmCrossmatch
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oCrossmatch As New Order.clsCrossmatch
    Private sKDIDENTITAS As Integer = 0
    Private sKDORDER As String = ""
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDIDENTITAS As String, ByVal NOORDER As String, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId
        sKDIDENTITAS = KDIDENTITAS
        sKDORDER = NOORDER
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "Lembar Kerja Pemeriksaan Crossmatch"

            'lKDCrossmatch.Text = Crossmatch.KDCrossmatch
            lDATE.Text = "Tanggal Order * :"
            'lKDWAREHOUSE.Text = Crossmatch.KDWAREHOUSE & " *"

            'tab1.Text = Crossmatch.TAB_DETAIL
            'tab2.Text = Crossmatch.TAB_MEMO

            'grvDetail.Columns("KDITEM").Caption = Crossmatch.DETAIL_KDITEM
            'grvDetail.Columns("KDUOM").Caption = Crossmatch.DETAIL_KDUOM
            'grvDetail.Columns("QTY").Caption = Crossmatch.DETAIL_QTY
            'grvDetail.Columns("REMARKS").Caption = Crossmatch.DETAIL_REMARKS

            'grvKDWAREHOUSE.Columns("NAME_DISPLAY").Caption = Warehouse.NAME_DISPLAY

            ''grvKDITEM.Columns("NMITEM1").Caption = Item.NMITEM1_2
            'grvKDITEM.Columns("NMITEM2").Caption = Item.NMITEM2_2
            '' grvKDITEM.Columns("NMITEM3").Caption = Item.NMITEM3_2

            'grvKDUOM.Columns("MEMO").Caption = UOM.MEMO

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'sCode = txtKDCrossmatch.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        'fn_LoadKDITEM()
        'fn_LoadKDUOM()
        'fn_LoadKDDOCTOR()

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

        MemoEdit1.Properties.ReadOnly = Status
        MemoEdit2.Properties.ReadOnly = Status
        MemoEdit3.Properties.ReadOnly = Status
        MemoEdit4.Properties.ReadOnly = Status
        MemoEdit5.Properties.ReadOnly = Status
        MemoEdit6.Properties.ReadOnly = Status
        MemoEdit7.Properties.ReadOnly = Status
        MemoEdit8.Properties.ReadOnly = Status
        grvDetail.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDORDER.Text = "<--- AUTO --->"
        deDATE.DateTime = Now

        MemoEdit1.Text = sCompany
        MemoEdit2.ResetText()
        MemoEdit3.ResetText()
        MemoEdit4.ResetText()
        MemoEdit5.ResetText()
        MemoEdit6.ResetText()
        MemoEdit7.ResetText()
        MemoEdit8.ResetText()

        grvDetail.OptionsSelection.MultiSelect = True
        grvDetail.SelectAll()
        grvDetail.DeleteSelectedRows()
        grvDetail.OptionsSelection.MultiSelect = False
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oCrossmatch.GetData(sNoId)

            With ds
                txtKDORDER.Text = .KDCROSSMATCH
                deDATE.DateTime = .DATE

                MemoEdit1.Text = .MemoEdit1
                MemoEdit2.Text = .MemoEdit2
                MemoEdit3.Text = .MemoEdit3
                MemoEdit4.Text = .MemoEdit4
                MemoEdit5.Text = .MemoEdit5
                MemoEdit6.Text = .MemoEdit6
                MemoEdit7.Text = .MemoEdit7
                MemoEdit8.Text = .MemoEdit8

                BindingSource.DataSource = oCrossmatch.GetDataDetail(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = BindingSource

            End With

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If sKDIDENTITAS = 0 Then
                MsgBox("Kode Identitas Kosong", MsgBoxStyle.Exclamation, Me.Text)
                txtKDORDER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDORDER.ErrorText = Statement.ErrorRequired

                txtKDORDER.Focus()
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
            Dim ds = oCrossmatch.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oCrossmatch.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .KDIDENTITAS = sKDIDENTITAS
                .KDCROSSMATCH = sNoId
                .MemoEdit1 = MemoEdit1.Text
                .MemoEdit2 = MemoEdit2.Text
                .MemoEdit3 = MemoEdit3.Text
                .MemoEdit4 = MemoEdit4.EditValue
                .MemoEdit5 = MemoEdit5.Text
                .MemoEdit6 = MemoEdit6.Text
                .MemoEdit7 = MemoEdit7.Text
                .MemoEdit8 = MemoEdit8.Text
                .MemoEdit9 = sKDORDER
                .MemoEdit10 = ""
                .MemoEdit11 = ""
                .MemoEdit12 = ""
                .MemoEdit13 = ""
                .MemoEdit14 = ""
                .MemoEdit15 = ""
                .MemoEdit16 = ""
                .MemoEdit17 = ""
                .MemoEdit18 = ""
                .MemoEdit19 = ""
                .MemoEdit20 = ""
                .MemoEdit21 = ""
                .MemoEdit22 = ""
                .MemoEdit23 = ""
                .MemoEdit24 = ""
                .MemoEdit25 = ""
                .MemoEdit26 = ""
                .MemoEdit27 = ""
                .MemoEdit28 = ""
                .MemoEdit29 = ""
                .MemoEdit30 = ""
                .CheckEdit1 = False
                .CheckEdit2 = False
                .CheckEdit3 = False
                .CheckEdit4 = False
                .CheckEdit5 = False
                .CheckEdit6 = False
                .CheckEdit7 = False
                .CheckEdit8 = False
                .CheckEdit9 = False
                .CheckEdit10 = False
                .CheckEdit11 = False
                .CheckEdit12 = False
                .CheckEdit13 = False
                .CheckEdit14 = False
                .CheckEdit15 = False
                .CheckEdit16 = False
                .CheckEdit17 = False
                .CheckEdit18 = False
                .CheckEdit19 = False
                .CheckEdit20 = False
                .CheckEdit21 = False
                .CheckEdit22 = False
                .CheckEdit23 = False
                .CheckEdit24 = False
                .CheckEdit25 = False
                .CheckEdit26 = False
                .CheckEdit27 = False
                .CheckEdit28 = False
                .CheckEdit29 = False
                .CheckEdit30 = False
                .KDUSER = sUserID
                .MEMO = ""
            End With

            Dim oItem As New Reference.clsItem

            ' ***** DETIL *****
            Dim arrDetail = oCrossmatch.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oCrossmatch.GetStructureDetail
                With dsDetail
                    .DATECREATED = ds.DATECREATED
                    .DATEUPDATED = ds.DATEUPDATED
                    .DATE = CDate(IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colDATE)), Now, grvDetail.GetRowCellValue(i, colDATE)))
                    .KDCROSSMATCH = ds.KDCROSSMATCH
                    .SEQ = i
                    .NOLABU = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colNOLABU)), "", grvDetail.GetRowCellValue(i, colNOLABU))
                    .EXPIRE = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colEXPIRE)), Now, grvDetail.GetRowCellValue(i, colEXPIRE))
                    .JENIS = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colJENIS)), "", grvDetail.GetRowCellValue(i, colJENIS))
                    .VOL = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colVOL)), "", grvDetail.GetRowCellValue(i, colVOL))
                    .ANTI_A = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colANTI_A)), "", grvDetail.GetRowCellValue(i, colANTI_A))
                    .ANTI_B = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colANTI_B)), "", grvDetail.GetRowCellValue(i, colANTI_B))
                    .ERI_A = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colERI_A)), "", grvDetail.GetRowCellValue(i, colERI_A))
                    .ERI_B = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colERI_B)), "", grvDetail.GetRowCellValue(i, colERI_B))
                    .CROSSMATCH_MAYOR = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colCROSSMATCH_MAYOR)), "", grvDetail.GetRowCellValue(i, colCROSSMATCH_MAYOR))
                    .CROSSMATCH_MINOR = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colCROSSMATCH_MINOR)), "", grvDetail.GetRowCellValue(i, colCROSSMATCH_MINOR))
                    .AC = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colAC)), "", grvDetail.GetRowCellValue(i, colAC))
                    .CI = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colCI)), "", grvDetail.GetRowCellValue(i, colCI))
                    .TANGGALDANJAM = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colTANGGALDANJAM)), Now, grvDetail.GetRowCellValue(i, colTANGGALDANJAM))
                    .NOBON = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colNOBON)), "", grvDetail.GetRowCellValue(i, colNOBON))
                    .MEMO = ""
                    .KDUSER = sUserID
                    .ISBACA = 0
                    .ISPERAWAT = False
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oCrossmatch.InsertData(ds, arrDetail)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oCrossmatch.UpdateData(ds, arrDetail)
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
        '    Dim oCustomer As New Reference.clsCustomer

        '    Try
        '        If grvDetail.GetFocusedRowCellValue(colKDITEM) IsNot Nothing Then
        '            Dim ds = oItem.GetDataDetail_UOM(grvDetail.GetFocusedRowCellValue(colKDITEM))

        '            If ds IsNot Nothing Then
        '                grvDetail.SetFocusedRowCellValue(colKDUOM, ds.FirstOrDefault(Function(x) x.RATE = 1).KDUOM)

        '                Dim dsCustomer = oCustomer.GetData(sKDCUSTOMER)

        '                If dsCustomer IsNot Nothing Then
        '                    If dsCustomer.KDGOLONGANDARAH = "0" Then
        '                        grvDetail.SetFocusedRowCellValue(colGOLONGANDARAH, "A")
        '                    ElseIf dsCustomer.KDGOLONGANDARAH = "1"
        '                        grvDetail.SetFocusedRowCellValue(colGOLONGANDARAH, "B")
        '                    ElseIf dsCustomer.KDGOLONGANDARAH = "2"
        '                        grvDetail.SetFocusedRowCellValue(colGOLONGANDARAH, "AB")
        '                    ElseIf dsCustomer.KDGOLONGANDARAH = "3"
        '                        grvDetail.SetFocusedRowCellValue(colGOLONGANDARAH, "O")
        '                    Else
        '                        grvDetail.SetFocusedRowCellValue(colGOLONGANDARAH, "-")
        '                    End If

        '                End If

        '                grvDetail.SetFocusedRowCellValue(colDATE, Now)
        '                grvDetail.SetFocusedRowCellValue(colJUMLAH, 1)
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
        'End If
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail.DeleteSelectedRows()
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmCrossmatch_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    'Private Sub fn_LoadKDITEM()
    '    Dim oITEM As New Reference.clsItem
    '    Try
    '        grdKDITEM.DataSource = oITEM.GetData.Where(Function(x) x.ISACTIVE = True And x.ISSTOK = False And x.M_ITEM_L3.MEMO.Contains("DARAH")).ToList()
    '        grdKDITEM.ValueMember = "KDITEM"
    '        grdKDITEM.DisplayMember = "NMITEM2"
    '    Catch oErr As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
    'Private Sub fn_LoadKDUOM()
    '    Dim oUOM As New Reference.clsUOM
    '    Try
    '        grdKDUOM.DataSource = oUOM.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
    '        grdKDUOM.ValueMember = "KDUOM"
    '        grdKDUOM.DisplayMember = "MEMO"
    '    Catch oErr As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
    'Private Sub fn_LoadKDDOCTOR()
    '    Dim oUOM As New Reference.clsDoctor
    '    Try
    '        MemoEdit4.Properties.DataSource = oUOM.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
    '        MemoEdit4.Properties.ValueMember = "KDDOCTOR"
    '        MemoEdit4.Properties.DisplayMember = "NAME_DISPLAY"
    '    Catch oErr As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
#End Region
End Class