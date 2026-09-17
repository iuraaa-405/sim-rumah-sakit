Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmSetor
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oSetor As New Finance.clsSetor
    Private sAuto As Boolean = False
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId

        If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            colNOINVOICE.OptionsColumn.AllowEdit = True
            colNOINVOICE.OptionsColumn.AllowFocus = True
            colNOINVOICE.OptionsColumn.ReadOnly = False
            colNOINVOICE.OptionsColumn.TabStop = True
        Else
            colNOINVOICE.OptionsColumn.AllowEdit = False
            colNOINVOICE.OptionsColumn.AllowFocus = False
            colNOINVOICE.OptionsColumn.ReadOnly = True
            colNOINVOICE.OptionsColumn.TabStop = False
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = Setor.TITLE

            tab1.Text = Setor.TAB_DETAIL
            'tab3.Text = Setor.TAB_MEMO

            lKDSETOR.Text = Setor.KDSETOR
            'lCATEGORY.Text = Setor.CATEGORY
            'lSHIFT.Text = Setor.SHIFT
            lDATE.Text = Setor.TANGGAL
            lSUBTOTAL.Text = Setor.SUBTOTAL
            lADMIN.Text = Setor.ADMIN
            lROUND.Text = Setor.ROUND
            lGRANDTOTAL.Text = Setor.GRANDTOTAL

            grvDetail.Columns("NOINVOICE").Caption = Setor.DETAIL_NOINVOICE
            grvDetail.Columns("AMOUNTPAYMENT").Caption = Setor.DETAIL_AMOUNTPAYMENT
            grvDetail.Columns("REMARKS").Caption = Setor.DETAIL_REMARKS

            grvNOINVOICE.Columns("NOINVOICE").Caption = CashIn.KDCASHIN

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDSETOR.Text.Trim.ToUpper
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
        'rbCATEGORY.Properties.ReadOnly = Status
        'rbSHIFT.Properties.ReadOnly = Status

        txtMEMO.Properties.ReadOnly = Status

        txtADMIN.Properties.ReadOnly = Status
        txtROUND.Properties.ReadOnly = Status

        grvDetail.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDSetor.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        'txtMEMO.ResetText()
        txtSUBTOTAL.ResetText()
        txtADMIN.ResetText()
        txtROUND.ResetText()
        txtGRANDTOTAL.ResetText()
        'rbCATEGORY.SelectedIndex = 0
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oSetor.GetData(sNoId)

            With ds
                txtKDSETOR.Text = .KDSETOR
                deDATE.DateTime = .DATE
                'rbCATEGORY.SelectedIndex = .CATEGORY
                'rbSHIFT.SelectedIndex = .SHIFT
                txtMEMO.Text = .MEMO
                txtSUBTOTAL.Text = .SUBTOTAL
                txtADMIN.Text = .ADMIN
                txtROUND.Text = .ROUND
                txtGRANDTOTAL.Text = .GRANDTOTAL

                bindingSource.DataSource = oSetor.GetDataDetail.Where(Function(x) x.KDSETOR = sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = bindingSource

            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            'If txtMEMO.Text = String.Empty Then
            '    txtMEMO.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '    txtMEMO.ErrorText = Statement.ErrorRequired

            '    txtMEMO.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If

            grvDetail.UpdateCurrentRow()

            If grvDetail.RowCount < 2 Then
                MsgBox(Statement.ErrorDetail, MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End If
            If CDec(txtGRANDTOTAL.Text) = 0 Then
                Calculate()
            End If
            grvDetail.UpdateCurrentRow()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oSetor.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oSetor.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDSETOR = sNoId
                .DATE = deDATE.DateTime
                .CATEGORY = 0
                .SHIFT = 0
                .MEMO = txtMEMO.Text
                .SUBTOTAL = CDec(txtSUBTOTAL.Text)
                .ADMIN = CDec(txtADMIN.Text)
                .ROUND = CDec(txtROUND.Text)
                .GRANDTOTAL = CDec(txtGRANDTOTAL.Text)
                .KDUSER = sUserID
            End With

            ' ***** DETIL *****
            Dim arrDetail = oSetor.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oSetor.GetStructureDetail
                With dsDetail
                    Try
                        .DATECREATED = oSetor.GetData(sNoId).DATECREATED
                    Catch oErr As Exception
                        .DATECREATED = Now
                    End Try
                    .DATEUPDATED = Now

                    .SEQ = i
                    .KDSETOR = ds.KDSETOR
                    .NOINVOICE = grvDetail.GetRowCellValue(i, colNOINVOICE)
                    .AMOUNTPAYMENT = CDec(grvDetail.GetRowCellValue(i, colAMOUNTPAYMENT))
                    .REMARKS = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colREMARKS)), "-", grvDetail.GetRowCellValue(i, colREMARKS))
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oSetor.InsertData(ds, arrDetail)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oSetor.UpdateData(ds, arrDetail)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail.DeleteSelectedRows()
    End Sub
#End Region
#Region "Grid Method"
    Private Sub OnValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtADMIN.EditValueChanged, txtROUND.EditValueChanged, grvDetail.FocusedRowChanged
        If isLoad Then
            Calculate()
        End If
    End Sub
    Private Sub Calculate()
        Dim sSubTotal = 0
        For i As Integer = 0 To grvDetail.RowCount - 2
            sSubTotal += CDec(grvDetail.GetRowCellValue(i, colAMOUNTPAYMENT))
        Next

        txtSUBTOTAL.Text = sSubTotal

        txtGRANDTOTAL.Text = CDec(txtSUBTOTAL.Text) + CDec(txtADMIN.Text) - CDec(txtROUND.Text)
    End Sub
    Private Sub grvDetail_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDetail.CellValueChanged
        If e.Column.Name = colNOINVOICE.Name Then
            Dim oCashin As New Finance.clsCashIn
            Dim oCashinTanpaResep As New Sales.clsSalesOrderTanpaResep
            Try
                If grvDetail.GetFocusedRowCellValue(colNOINVOICE) IsNot Nothing Then

                    Dim ds = oCashin.GetData(grvDetail.GetFocusedRowCellValue(colNOINVOICE))

                    If ds IsNot Nothing Then
                        If ds.ISSETOR = False Then
                            If ds.COSTSHARE > 0 Then
                                grvDetail.SetFocusedRowCellValue(colAMOUNTPAYMENT, ds.COSTSHARE)
                            Else
                                grvDetail.SetFocusedRowCellValue(colAMOUNTPAYMENT, ds.GRANDTOTAL)
                            End If
                        Else
                            MsgBox("Sudah Setor", MsgBoxStyle.Exclamation, Me.Text)
                            grvDetail.CancelUpdateCurrentRow()
                        End If
                    Else
                        Dim dsTanpaResep = oCashinTanpaResep.GetData(grvDetail.GetFocusedRowCellValue(colNOINVOICE))

                        If dsTanpaResep IsNot Nothing Then
                            If dsTanpaResep.PAYAMOUNT = 0 Then
                                grvDetail.SetFocusedRowCellValue(colAMOUNTPAYMENT, dsTanpaResep.GRANDTOTAL)
                            Else
                                MsgBox("Sudah Setor", MsgBoxStyle.Exclamation, Me.Text)
                                grvDetail.CancelUpdateCurrentRow()
                            End If
                        Else
                            MsgBox(Statement.ErrorUOM, MsgBoxStyle.Exclamation, Me.Text)

                            grvDetail.CancelUpdateCurrentRow()
                        End If

                    End If
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try

            'ElseIf e.Column.Name = colAMOUNTPAYMENT.Name Then
            '    Dim sJumlah As Decimal = 0

            '    If grvDetail.GetFocusedRowCellValue(colAMOUNTPAYMENT) > 0 Then
            '        sJumlah = CDec(grvDetail.GetFocusedRowCellValue(colAMOUNTORIGINAL)) - CDec(grvDetail.GetFocusedRowCellValue(colAMOUNTPAYMENT))

            '        grvDetail.SetFocusedRowCellValue(colAMOUNTDUE, sJumlah)

            '    End If

        End If
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmSetor_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    Private Sub btnCariPembayaran_Click(sender As Object, e As EventArgs) Handles btnCariPembayaran.Click
        Dim frmSKD As New frmReportPembayaran
        Try
            frmReportPembayaran.ShowDialog(Me)
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmReportPembayaran Is Nothing Then frmReportPembayaran.Dispose()
            frmReportPembayaran = Nothing

            Dim oCashin As New Finance.clsCashIn

            For Each xloop In listNoInvoiceAmbil
                grvDetail.Focus()
                grvDetail.AddNewRow()
                grvDetail.SetFocusedRowCellValue(colNOINVOICE, xloop)
                Dim dsCashin = oCashin.GetData(xloop)
                If dsCashin IsNot Nothing Then
                    grvDetail.SetFocusedRowCellValue(colAMOUNTPAYMENT, dsCashin.GRANDTOTAL)
                Else
                    grvDetail.SetFocusedRowCellValue(colAMOUNTPAYMENT, 0)
                End If
                grvDetail.UpdateCurrentRow()
            Next
        End Try
    End Sub
#End Region
#Region "Lookup / Event"
    'Private Sub fn_LoadNOINVOICE_UGD(ByVal Setor As Boolean)
    '    Dim oCashin As New Finance.clsCashIn
    '    Try
    '        Dim dsCashin = From x In oCashin.GetDataUGD(Setor)
    '                       Select NOINVOICE = x.KDCASHIN, PASIEN = x.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY, x.DATE, x.GRANDTOTAL, TUJUAN = x.S_PENDAFTARAN_H.M_DEPARTMENT.NAME_DISPLAY

    '        grdNOINVOICE.DataSource = dsCashin.ToList()
    '        grdNOINVOICE.ValueMember = "NOINVOICE"
    '        grdNOINVOICE.DisplayMember = "NOINVOICE"

    '        txtMEMO.Text = "Setoran Rawat Jalan Unit Gawat Darurat"

    '        If sAuto = True Then
    '            grvDetail.OptionsSelection.MultiSelect = True
    '            grvDetail.SelectAll()
    '            grvDetail.DeleteSelectedRows()
    '            grvDetail.OptionsSelection.MultiSelect = False

    '            For Each xloop In dsCashin
    '                grvDetail.Focus()
    '                grvDetail.AddNewRow()
    '                grvDetail.SetFocusedRowCellValue(colNOINVOICE, xloop.NOINVOICE)
    '                grvDetail.UpdateCurrentRow()
    '            Next
    '        End If

    '    Catch oErr As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
    'Private Sub fn_LoadNOINVOICE_POLI(ByVal Setor As Boolean)
    '    Dim oCashin As New Finance.clsCashIn
    '    Try
    '        Dim dsCashin = From x In oCashin.GetDataPOLI(Setor)
    '                       Select NOINVOICE = x.KDCASHIN, PASIEN = x.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY, x.DATE, x.GRANDTOTAL, TUJUAN = x.S_PENDAFTARAN_H.M_DEPARTMENT.NAME_DISPLAY

    '        grdNOINVOICE.DataSource = dsCashin.ToList()
    '        grdNOINVOICE.ValueMember = "NOINVOICE"
    '        grdNOINVOICE.DisplayMember = "NOINVOICE"

    '        txtMEMO.Text = "Setoran Rawat Jalan Poliklinik"

    '        If sAuto = True Then
    '            grvDetail.OptionsSelection.MultiSelect = True
    '            grvDetail.SelectAll()
    '            grvDetail.DeleteSelectedRows()
    '            grvDetail.OptionsSelection.MultiSelect = False

    '            For Each xloop In dsCashin
    '                grvDetail.Focus()
    '                grvDetail.AddNewRow()
    '                grvDetail.SetFocusedRowCellValue(colNOINVOICE, xloop.NOINVOICE)
    '                grvDetail.UpdateCurrentRow()
    '            Next
    '        End If

    '    Catch oErr As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
    'Private Sub fn_LoadNOINVOICE_RAWATINAP(ByVal Setor As Boolean)
    '    Dim oCashin As New Finance.clsCashIn
    '    Try
    '        Dim dsCashin = From x In oCashin.GetDataRAWATINAP(Setor)
    '                       Select NOINVOICE = x.KDCASHIN, PASIEN = x.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY, x.DATE, x.GRANDTOTAL, TUJUAN = x.S_PENDAFTARAN_H.M_DEPARTMENT.NAME_DISPLAY

    '        grdNOINVOICE.DataSource = dsCashin.ToList()
    '        grdNOINVOICE.ValueMember = "NOINVOICE"
    '        grdNOINVOICE.DisplayMember = "NOINVOICE"

    '        txtMEMO.Text = "Setoran Tunai Rawat Inap"

    '        If sAuto = True Then
    '            grvDetail.OptionsSelection.MultiSelect = True
    '            grvDetail.SelectAll()
    '            grvDetail.DeleteSelectedRows()
    '            grvDetail.OptionsSelection.MultiSelect = False

    '            For Each xloop In dsCashin
    '                grvDetail.Focus()
    '                grvDetail.AddNewRow()
    '                grvDetail.SetFocusedRowCellValue(colNOINVOICE, xloop.NOINVOICE)
    '                grvDetail.UpdateCurrentRow()
    '            Next
    '        End If
    '    Catch oErr As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
    'Private Sub fn_LoadNOINVOICE_COSTSHARE(ByVal Setor As Boolean)
    '    Dim oCashin As New Finance.clsCashIn
    '    Try
    '        Dim dsCashin = From x In oCashin.GetDataCOSTSHARE(Setor)
    '                       Select NOINVOICE = x.KDCASHIN, PASIEN = x.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY, x.DATE, GRANDTOTAL = x.COSTSHARE, TUJUAN = x.S_PENDAFTARAN_H.M_DEPARTMENT.NAME_DISPLAY

    '        grdNOINVOICE.DataSource = dsCashin.ToList()
    '        grdNOINVOICE.ValueMember = "NOINVOICE"
    '        grdNOINVOICE.DisplayMember = "NOINVOICE"

    '        txtMEMO.Text = "Setoran Cosh Sharing Rawat Inap"

    '        If sAuto = True Then
    '            grvDetail.OptionsSelection.MultiSelect = True
    '            grvDetail.SelectAll()
    '            grvDetail.DeleteSelectedRows()
    '            grvDetail.OptionsSelection.MultiSelect = False

    '            For Each xloop In dsCashin
    '                grvDetail.Focus()
    '                grvDetail.AddNewRow()
    '                grvDetail.SetFocusedRowCellValue(colNOINVOICE, xloop.NOINVOICE)
    '                grvDetail.UpdateCurrentRow()
    '            Next
    '        End If
    '    Catch oErr As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
    'Private Sub fn_LoadNOINVOICE_FARMASI(ByVal Setor As Boolean)
    '    Dim oCashin As New Finance.clsCashIn
    '    Dim oCashinTanpaResep As New Sales.clsSalesOrderTanpaResep

    '    Try
    '        Dim dsCashin = From x In oCashin.GetDataFARMASI(Setor)
    '                       Select NOINVOICE = x.KDCASHIN, PASIEN = x.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY, x.DATE, x.GRANDTOTAL, TUJUAN = x.S_PENDAFTARAN_H.M_DEPARTMENT.NAME_DISPLAY

    '        Dim dsCashinTanpaResep = From x In oCashinTanpaResep.GetData
    '                                 Where IIf(Setor = False, x.PAYAMOUNT = 0, x.PAYAMOUNT <> 0)
    '                                 Select NOINVOICE = x.KDSOTANPARESEP, PASIEN = x.NAMAPASIEN, x.DATE, x.GRANDTOTAL, TUJUAN = "Apotek"

    '        Dim dsUnion = dsCashin.Union(dsCashinTanpaResep)

    '        grdNOINVOICE.DataSource = dsUnion.ToList
    '        grdNOINVOICE.ValueMember = "NOINVOICE"
    '        grdNOINVOICE.DisplayMember = "NOINVOICE"

    '        txtMEMO.Text = "Setoran Bekkes Tunai"

    '        If sAuto = True Then
    '            grvDetail.OptionsSelection.MultiSelect = True
    '            grvDetail.SelectAll()
    '            grvDetail.DeleteSelectedRows()
    '            grvDetail.OptionsSelection.MultiSelect = False

    '            For Each xloop In dsUnion
    '                grvDetail.Focus()
    '                grvDetail.AddNewRow()
    '                grvDetail.SetFocusedRowCellValue(colNOINVOICE, xloop.NOINVOICE)
    '                grvDetail.UpdateCurrentRow()
    '            Next
    '        End If
    '    Catch oErr As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try

    'End Sub
    'Private Sub rbCATEGORY_SelectedIndexChanged(ByVal Setor As Boolean)
    '    grvDetail.OptionsSelection.MultiSelect = True
    '    grvDetail.SelectAll()
    '    grvDetail.DeleteSelectedRows()
    '    grvDetail.OptionsSelection.MultiSelect = False

    '    If rbCATEGORY.SelectedIndex = 0 Then
    '        fn_LoadNOINVOICE_UGD(Setor)
    '    ElseIf rbCATEGORY.SelectedIndex = 1 Then
    '        fn_LoadNOINVOICE_POLI(Setor)
    '    ElseIf rbCATEGORY.SelectedIndex = 2 Then
    '        fn_LoadNOINVOICE_RAWATINAP(Setor)
    '    ElseIf rbCATEGORY.SelectedIndex = 3 Then
    '        fn_LoadNOINVOICE_COSTSHARE(Setor)
    '    Else
    '        fn_LoadNOINVOICE_FARMASI(Setor)
    '    End If
    'End Sub
    'Private Sub rbCATEGORY_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    rbCATEGORY_SelectedIndexChanged(IIf(oFormMode = FORM_MODE.FORM_MODE_ADD, False, True))
    'End Sub
    'Private Sub btnAutomatis_Click(sender As Object, e As EventArgs) Handles btnAutomatis.Click
    '    sAuto = True
    '    rbCATEGORY_SelectedIndexChanged(IIf(oFormMode = FORM_MODE.FORM_MODE_ADD, False, True))
    '    sAuto = False
    'End Sub
    'Private Sub fn_LoadNOINVOICE()
    '    Dim oCashin As New Finance.clsCashIn
    '    Dim oCashinTanpaResep As New Sales.clsSalesOrderTanpaResep

    '    Try
    '        grvDetail.OptionsSelection.MultiSelect = True
    '        grvDetail.SelectAll()
    '        grvDetail.DeleteSelectedRows()
    '        grvDetail.OptionsSelection.MultiSelect = False

    '        If rbCATEGORY.SelectedIndex = 0 Then
    '            Dim ds = From x In oCashin.GetDataSetor(0)

    '            For Each xloop In ds
    '                grvDetail.Focus()
    '                grvDetail.AddNewRow()
    '                grvDetail.SetFocusedRowCellValue(colNOINVOICE, xloop.KDCASHIN)
    '                grvDetail.UpdateCurrentRow()
    '            Next
    '        ElseIf rbCATEGORY.SelectedIndex = 1 Then
    '            Dim ds = From x In oCashin.GetDataSetor(1)

    '            For Each xloop In ds
    '                grvDetail.Focus()
    '                grvDetail.AddNewRow()
    '                grvDetail.SetFocusedRowCellValue(colNOINVOICE, xloop.KDCASHIN)
    '                grvDetail.UpdateCurrentRow()
    '            Next

    '        ElseIf rbCATEGORY.SelectedIndex = 2 Then
    '            Dim ds1 = From x In oCashin.GetDataSetor(0)

    '            For Each xloop In ds1
    '                grvDetail.Focus()
    '                grvDetail.AddNewRow()
    '                grvDetail.SetFocusedRowCellValue(colNOINVOICE, xloop.KDCASHIN)
    '                grvDetail.UpdateCurrentRow()
    '            Next
    '            Dim ds2 = From x In oCashin.GetDataSetor(1)

    '            For Each xloop In ds2
    '                grvDetail.Focus()
    '                grvDetail.AddNewRow()
    '                grvDetail.SetFocusedRowCellValue(colNOINVOICE, xloop.KDCASHIN)
    '                grvDetail.UpdateCurrentRow()
    '            Next

    '        Else
    '            Dim ds = From x In oCashin.GetDataSetor(2)

    '            For Each xloop In ds
    '                grvDetail.Focus()
    '                grvDetail.AddNewRow()
    '                grvDetail.SetFocusedRowCellValue(colNOINVOICE, xloop.KDCASHIN)
    '                grvDetail.UpdateCurrentRow()
    '            Next

    '            Dim dsTanpResep = From x In oCashinTanpaResep.GetDataSetor
    '            For Each xloop In dsTanpResep
    '                grvDetail.Focus()
    '                grvDetail.AddNewRow()
    '                grvDetail.SetFocusedRowCellValue(colNOINVOICE, xloop.KDSOTANPARESEP)
    '                grvDetail.UpdateCurrentRow()
    '            Next
    '        End If

    '    Catch oErr As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
    'Private Sub rbCATEGORY_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rbCATEGORY.SelectedIndexChanged
    '    fn_LoadNOINVOICE()
    'End Sub

#End Region
End Class