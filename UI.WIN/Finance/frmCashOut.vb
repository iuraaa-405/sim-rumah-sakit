Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmCashOut
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oCashOut As New Finance.clsCashOut
    Private oKontraBon As New Finance.clsKontraBon
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
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = CashOut.TITLE

            lKDCASH.Text = CashOut.KDCASHOUT
            lDATE.Text = CashOut.TANGGAL
            lKDVENDOR.Text = CashOut.KDVENDOR & " *"
            lKDPAYMENTTYPE.Text = CashOut.KDPAYMENTTYPE & " *"

            tab1.Text = CashOut.TAB_DETAIL
            tab2.Text = CashOut.TAB_DETAILRETURN
            tab3.Text = CashOut.TAB_MEMO

            lSUBTOTAL.Text = CashOut.SUBTOTAL
            lADMIN.Text = CashOut.ADMIN
            lROUND.Text = CashOut.ROUND
            lGRANDTOTAL.Text = CashOut.GRANDTOTAL

            grvDetail.Columns("NOINVOICE").Caption = CashOut.DETAIL_NOINVOICE
            grvDetail.Columns("AMOUNTORIGINAL_UB").Caption = CashOut.DETAIL_AMOUNTORIGINAL
            grvDetail.Columns("AMOUNTDUE_UB").Caption = CashOut.DETAIL_AMOUNTDUE
            grvDetail.Columns("AMOUNTPAYMENT").Caption = CashOut.DETAIL_AMOUNTPAYMENT
            grvDetail.Columns("REMARKS").Caption = CashOut.DETAIL_REMARKS

            grvDetail_R.Columns("NOINVOICE").Caption = CashOut.DETAIL_NOINVOICE
            grvDetail_R.Columns("AMOUNTORIGINAL_UB").Caption = CashOut.DETAIL_AMOUNTORIGINAL
            grvDetail_R.Columns("AMOUNTDUE_UB").Caption = CashOut.DETAIL_AMOUNTDUE
            grvDetail_R.Columns("AMOUNTPAYMENT").Caption = CashOut.DETAIL_AMOUNTPAYMENT
            grvDetail_R.Columns("REMARKS").Caption = CashOut.DETAIL_REMARKS

            grvKDVENDOR.Columns("NAME_DISPLAY").Caption = Vendor.NAME_DISPLAY
            grvKDPAYMENTTYPE.Columns("MEMO").Caption = PaymentType.MEMO

            grvNOINVOICE.Columns("NOINVOICE").Caption = PurchaseInvoice.KDPI
            grvNOINVOICE_R.Columns("NOINVOICE").Caption = PurchaseReturn.KDPR

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDCASHOUT.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDVENDOR()
        fn_LoadKDPAYMENTTYPE()
        
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

        fn_LoadNOINVOICE()
        fn_LoadNOINVOICE_R()
    End Sub
    Private Sub fn_ViewMode(ByVal Status As Boolean)
        btnSaveNew.Enabled = Not Status
        btnSaveClose.Enabled = Not Status

        deDATE.Properties.ReadOnly = Status
        grdKDVENDOR.Properties.ReadOnly = Status
        grdKDPAYMENTTYPE.Properties.ReadOnly = Status

        txtMEMO.Properties.ReadOnly = Status

        txtADMIN.Properties.ReadOnly = Status
        txtROUND.Properties.ReadOnly = Status

        grvDetail.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDCASHOUT.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        grdKDVENDOR.ResetText()
        grdKDPAYMENTTYPE.ResetText()
        txtMEMO.ResetText()
        txtSUBTOTAL.ResetText()
        txtADMIN.ResetText()
        txtROUND.ResetText()
        txtGRANDTOTAL.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oCashOut.GetData(sNoId)

            With ds
                txtKDCASHOUT.Text = .KDCASHOUT
                deDATE.DateTime = .DATE
                grdKDVENDOR.Text = .KDVENDOR
                grdKDPAYMENTTYPE.Text = .KDPAYMENTTYPE
                txtMEMO.Text = .MEMO

                txtSUBTOTAL.Text = .SUBTOTAL
                txtADMIN.Text = .ADMIN
                txtROUND.Text = .ROUND
                txtGRANDTOTAL.Text = .GRANDTOTAL

                bindingSource.DataSource = oCashOut.GetDataDetail.Where(Function(x) x.KDCASHOUT = sNoId And x.SEQ < 100).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = bindingSource

                bindingSource_R.DataSource = oCashOut.GetDataDetail.Where(Function(x) x.KDCASHOUT = sNoId And x.SEQ >= 100).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail_R.DataSource = bindingSource_R
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If grdKDVENDOR.Text = String.Empty Then
                grdKDVENDOR.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDVENDOR.ErrorText = Statement.ErrorRequired

                grdKDVENDOR.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDPAYMENTTYPE.Text = String.Empty Then
                grdKDPAYMENTTYPE.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDPAYMENTTYPE.ErrorText = Statement.ErrorRequired

                grdKDPAYMENTTYPE.Focus()
                fn_Validate = False
                Exit Function
            End If

            grvDetail.UpdateCurrentRow()
            grvDetail_R.UpdateCurrentRow()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oCashOut.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oCashOut.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDCASHOUT = sNoId
                .DATE = deDATE.DateTime
                .KDVENDOR = IIf(String.IsNullOrEmpty(grdKDVENDOR.EditValue), String.Empty, grdKDVENDOR.EditValue)
                .KDPAYMENTTYPE = IIf(String.IsNullOrEmpty(grdKDPAYMENTTYPE.EditValue), String.Empty, grdKDPAYMENTTYPE.EditValue)
                .MEMO = txtMEMO.Text.Trim.ToUpper
                .SUBTOTAL = CDec(txtSUBTOTAL.Text)
                .ADMIN = CDec(txtADMIN.Text)
                .ROUND = CDec(txtROUND.Text)
                .GRANDTOTAL = CDec(txtGRANDTOTAL.Text)
                .KDUSER = sUserID
            End With

            ' ***** DETIL *****
            Dim arrDetail = oCashOut.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oCashOut.GetStructureDetail
                With dsDetail
                    Try
                        .DATECREATED = oCashOut.GetData(sNoId).DATECREATED
                    Catch oErr As Exception
                        .DATECREATED = Now
                    End Try
                    .DATEUPDATED = Now

                    .SEQ = i
                    .KDCASHOUT = ds.KDCASHOUT
                    .NOINVOICE = grvDetail.GetRowCellValue(i, colNOINVOICE)
                    .AMOUNTPAYMENT = CDec(grvDetail.GetRowCellValue(i, colAMOUNTPAYMENT))
                    .REMARKS = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colREMARKS)), "-", grvDetail.GetRowCellValue(i, colREMARKS))
                End With
                arrDetail.Add(dsDetail)
            Next

            ' ***** DETIL RETURN *****
            Dim arrDetail_R = oCashOut.GetStructureDetailList
            For i As Integer = 0 To grvDetail_R.RowCount - 2
                Dim dsDetail_R = oCashOut.GetStructureDetail
                With dsDetail_R
                    Try
                        .DATECREATED = oCashOut.GetData(sNoId).DATECREATED
                    Catch oErr As Exception
                        .DATECREATED = Now
                    End Try
                    .DATEUPDATED = Now

                    .SEQ = i + 100
                    .KDCASHOUT = ds.KDCASHOUT
                    .NOINVOICE = grvDetail_R.GetRowCellValue(i, colNOINVOICE_R)
                    .AMOUNTPAYMENT = CDec(grvDetail_R.GetRowCellValue(i, colAMOUNTPAYMENT_R))
                    .REMARKS = IIf(String.IsNullOrEmpty(grvDetail_R.GetRowCellValue(i, colREMARKS_R)), "-", grvDetail_R.GetRowCellValue(i, colREMARKS_R))
                End With
                arrDetail_R.Add(dsDetail_R)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oCashOut.InsertData(ds, arrDetail, arrDetail_R)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oCashOut.UpdateData(ds, arrDetail, arrDetail_R)
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
    Private Sub OnValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtADMIN.EditValueChanged, txtROUND.EditValueChanged, grvDetail.FocusedRowChanged, grvDetail_R.FocusedRowChanged, grdKDVENDOR.Validated
        If isLoad Then
            Calculate()
        End If
    End Sub
    Private Sub Calculate()
        Dim sSubTotal = 0
        For i As Integer = 0 To grvDetail.RowCount - 2
            sSubTotal += CDec(grvDetail.GetRowCellValue(i, colAMOUNTPAYMENT))
        Next

        Dim sSubTotal_R = 0
        For i As Integer = 0 To grvDetail_R.RowCount - 2
            sSubTotal_R += grvDetail_R.GetRowCellValue(i, colAMOUNTPAYMENT_R)
        Next

        txtSUBTOTAL.Text = sSubTotal - sSubTotal_R

        txtGRANDTOTAL.Text = CDec(txtSUBTOTAL.Text) + CDec(txtADMIN.Text) + CDec(txtROUND.Text)
    End Sub
    Private Sub grvDetail_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDetail.CellValueChanged
        If e.Column.Name = colNOINVOICE.Name Then
            Dim oPurchaseInvoice As New Purchasing.clsPurchaseInvoice
            Try
                If grvDetail.GetFocusedRowCellValue(colNOINVOICE) <> String.Empty Then
                    If sNOINVOICE.Contains(grvDetail.GetFocusedRowCellValue(colNOINVOICE)) Then
                        Dim sNOINVOICE = grvDetail.GetFocusedRowCellValue(colNOINVOICE)

                        grvDetail.CancelUpdateCurrentRow()

                        Dim rowHandle As Integer = grvDetail.LocateByValue("NOINVOICE", sNOINVOICE)
                        grvDetail.FocusedRowHandle = rowHandle
                        Exit Sub
                    End If

                    Dim ds = oPurchaseInvoice.GetData(grvDetail.GetFocusedRowCellValue(colNOINVOICE))
                    grvDetail.SetFocusedRowCellValue(colAMOUNTPAYMENT, ds.GRANDTOTAL - ds.PAYAMOUNT)
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Critical, Me.Text)
            End Try
        ElseIf e.Column.Name = colAMOUNTPAYMENT.Name Then
            If grvDetail.GetFocusedRowCellValue(colAMOUNTDUE_UB) < 0 Then
                Dim oPurchaseInvoice As New Purchasing.clsPurchaseInvoice
                Dim ds = oPurchaseInvoice.GetData(grvDetail.GetFocusedRowCellValue(colNOINVOICE))

                grvDetail.SetFocusedRowCellValue(colAMOUNTPAYMENT, ds.GRANDTOTAL - ds.PAYAMOUNT)
            End If
        End If
    End Sub
    Private Sub grvDetail_R_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDetail_R.CellValueChanged
        If e.Column.Name = colNOINVOICE_R.Name Then
            Dim oPurchaseReturn As New Purchasing.clsPurchaseReturn
            Try
                If grvDetail_R.GetFocusedRowCellValue(colNOINVOICE_R) <> String.Empty Then
                    If sNOINVOICE_R.Contains(grvDetail_R.GetFocusedRowCellValue(colNOINVOICE_R)) Then
                        Dim sNOINVOICE_R = grvDetail_R.GetFocusedRowCellValue(colNOINVOICE_R)

                        grvDetail_R.CancelUpdateCurrentRow()

                        Dim rowHandle As Integer = grvDetail_R.LocateByValue("NOINVOICE", sNOINVOICE_R)
                        grvDetail_R.FocusedRowHandle = rowHandle
                        Exit Sub
                    End If


                    Dim ds = oPurchaseReturn.GetData(grvDetail_R.GetFocusedRowCellValue(colNOINVOICE_R))
                    grvDetail_R.SetFocusedRowCellValue(colAMOUNTPAYMENT_R, ds.GRANDTOTAL - ds.PAYAMOUNT)
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Critical, Me.Text)
            End Try
        ElseIf e.Column.Name = colAMOUNTPAYMENT_R.Name Then
            If grvDetail_R.GetFocusedRowCellValue(colAMOUNTDUE_UB_R) < 0 Then
                Dim oPurchaseReturn As New Purchasing.clsPurchaseReturn
                Dim ds = oPurchaseReturn.GetData(grvDetail_R.GetFocusedRowCellValue(colNOINVOICE_R))

                grvDetail_R.SetFocusedRowCellValue(colAMOUNTPAYMENT_R, ds.GRANDTOTAL - ds.PAYAMOUNT)
            End If
        End If
    End Sub
    Private Sub grvDetail_CustomUnboundColumnData(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles grvDetail.CustomUnboundColumnData
        If e.Column.Name = colAMOUNTORIGINAL_UB.Name Then
            Dim oPurchaseInvoice As New Purchasing.clsPurchaseInvoice
            Try
                If grvDetail.GetRowCellValue(e.ListSourceRowIndex, colNOINVOICE) <> String.Empty Then
                    e.Value = oPurchaseInvoice.GetData(grvDetail.GetRowCellValue(e.ListSourceRowIndex, colNOINVOICE)).GRANDTOTAL
                ElseIf grvDetail.GetFocusedRowCellValue(colNOINVOICE) <> String.Empty Then
                    e.Value = oPurchaseInvoice.GetData(grvDetail.GetFocusedRowCellValue(colNOINVOICE)).GRANDTOTAL
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Critical, Me.Text)
            End Try
        End If
        If e.Column.Name = colAMOUNTDUE_UB.Name Then
            Dim oPurchaseInvoice As New Purchasing.clsPurchaseInvoice
            Dim oCashOut As New Finance.clsCashOut
            Try
                If grvDetail.GetRowCellValue(e.ListSourceRowIndex, colNOINVOICE) <> String.Empty Then
                    If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                        e.Value = oPurchaseInvoice.GetData(grvDetail.GetRowCellValue(e.ListSourceRowIndex, colNOINVOICE)).GRANDTOTAL - oPurchaseInvoice.GetData(grvDetail.GetRowCellValue(e.ListSourceRowIndex, colNOINVOICE)).PAYAMOUNT - grvDetail.GetRowCellValue(e.ListSourceRowIndex, colAMOUNTPAYMENT)
                    Else
                        Dim sPayment = (From x In oCashOut.GetDataDetail _
                                       Where x.KDCASHOUT = txtKDCASHOUT.Text _
                                       And x.NOINVOICE = grvDetail.GetRowCellValue(e.ListSourceRowIndex, colNOINVOICE) _
                                       Group x By x.NOINVOICE Into Sum(x.AMOUNTPAYMENT) _
                                       Select Sum).FirstOrDefault

                        e.Value = oPurchaseInvoice.GetData(grvDetail.GetRowCellValue(e.ListSourceRowIndex, colNOINVOICE)).GRANDTOTAL - oPurchaseInvoice.GetData(grvDetail.GetRowCellValue(e.ListSourceRowIndex, colNOINVOICE)).PAYAMOUNT + sPayment - grvDetail.GetRowCellValue(e.ListSourceRowIndex, colAMOUNTPAYMENT)
                    End If
                ElseIf grvDetail.GetFocusedRowCellValue(colNOINVOICE) <> String.Empty Then
                    e.Value = oPurchaseInvoice.GetData(grvDetail.GetFocusedRowCellValue(colNOINVOICE)).GRANDTOTAL - oPurchaseInvoice.GetData(grvDetail.GetFocusedRowCellValue(colNOINVOICE)).PAYAMOUNT - grvDetail.GetFocusedRowCellValue(colAMOUNTPAYMENT)
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Critical, Me.Text)
            End Try
        End If
    End Sub
    Private Sub grvDetail_R_CustomUnboundColumnData(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles grvDetail_R.CustomUnboundColumnData
        If e.Column.Name = colAMOUNTORIGINAL_UB_R.Name Then
            Dim oPurchaseReturn As New Purchasing.clsPurchaseReturn
            Try
                If grvDetail_R.GetRowCellValue(e.ListSourceRowIndex, colNOINVOICE_R) <> String.Empty Then
                    e.Value = oPurchaseReturn.GetData(grvDetail_R.GetRowCellValue(e.ListSourceRowIndex, colNOINVOICE_R)).GRANDTOTAL
                ElseIf grvDetail_R.GetFocusedRowCellValue(colNOINVOICE_R) <> String.Empty Then
                    e.Value = oPurchaseReturn.GetData(grvDetail_R.GetFocusedRowCellValue(colNOINVOICE_R)).GRANDTOTAL
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Critical, Me.Text)
            End Try
        End If
        If e.Column.Name = colAMOUNTDUE_UB_R.Name Then
            Dim oPurchaseReturn As New Purchasing.clsPurchaseReturn
            Dim oCashOut As New Finance.clsCashOut
            Try
                If grvDetail_R.GetRowCellValue(e.ListSourceRowIndex, colNOINVOICE_R) <> String.Empty Then
                    If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                        e.Value = oPurchaseReturn.GetData(grvDetail_R.GetRowCellValue(e.ListSourceRowIndex, colNOINVOICE_R)).GRANDTOTAL - oPurchaseReturn.GetData(grvDetail_R.GetRowCellValue(e.ListSourceRowIndex, colNOINVOICE_R)).PAYAMOUNT - grvDetail_R.GetRowCellValue(e.ListSourceRowIndex, colAMOUNTPAYMENT_R)
                    Else
                        Dim sPayment = (From x In oCashOut.GetDataDetail _
                                       Where x.KDCASHOUT = txtKDCASHOUT.Text _
                                       And x.NOINVOICE = grvDetail_R.GetRowCellValue(e.ListSourceRowIndex, colNOINVOICE_R) _
                                       Group x By x.NOINVOICE Into Sum(x.AMOUNTPAYMENT) _
                                       Select Sum).FirstOrDefault

                        e.Value = oPurchaseReturn.GetData(grvDetail_R.GetRowCellValue(e.ListSourceRowIndex, colNOINVOICE_R)).GRANDTOTAL - oPurchaseReturn.GetData(grvDetail_R.GetRowCellValue(e.ListSourceRowIndex, colNOINVOICE_R)).PAYAMOUNT + sPayment - grvDetail_R.GetRowCellValue(e.ListSourceRowIndex, colAMOUNTPAYMENT_R)
                    End If
                ElseIf grvDetail_R.GetFocusedRowCellValue(colNOINVOICE_R) <> String.Empty Then
                    e.Value = oPurchaseReturn.GetData(grvDetail_R.GetFocusedRowCellValue(colNOINVOICE_R)).GRANDTOTAL - oPurchaseReturn.GetData(grvDetail_R.GetFocusedRowCellValue(colNOINVOICE_R)).PAYAMOUNT - grvDetail_R.GetFocusedRowCellValue(colAMOUNTPAYMENT_R)
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Critical, Me.Text)
            End Try
        End If
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail.DeleteSelectedRows()
    End Sub
    Private Sub DeleteToolStripMenuItem_R_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem_R.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail_R.DeleteSelectedRows()
    End Sub
    Private Sub grdNOINVOICE_QueryPopUp(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles grdNOINVOICE.QueryPopUp
        sNOINVOICE.Clear()

        For iLoop As Integer = 0 To grvDetail.RowCount - 2
            sNOINVOICE.Add(grvDetail.GetRowCellValue(iLoop, colNOINVOICE))
        Next
    End Sub
    Private Sub grdNOINVOICE_R_QueryPopUp(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles grdNOINVOICE_R.QueryPopUp
        sNOINVOICE_R.Clear()

        For iLoop As Integer = 0 To grvDetail_R.RowCount - 2
            sNOINVOICE_R.Add(grvDetail_R.GetRowCellValue(iLoop, colNOINVOICE_R))
        Next
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmCashOut_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    Private Sub fn_LoadKDVENDOR()
        Dim oVENDOR As New Reference.clsVendor
        Try
            grdKDVENDOR.Properties.DataSource = oVENDOR.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDVENDOR.Properties.ValueMember = "KDVENDOR"
            grdKDVENDOR.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDVENDOR_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdKDVENDOR.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdKDVENDOR.ResetText()
        End If
    End Sub
    Private Sub grdKDVENDOR_Validated(sender As System.Object, e As System.EventArgs) Handles grdKDVENDOR.Validated
        fn_LoadNOINVOICE()
        fn_LoadNOINVOICE_R()

        grvDetail.OptionsSelection.MultiSelect = True
        grvDetail.SelectAll()
        grvDetail.DeleteSelectedRows()
        grvDetail.OptionsSelection.MultiSelect = False

        grvDetail_R.OptionsSelection.MultiSelect = True
        grvDetail_R.SelectAll()
        grvDetail_R.DeleteSelectedRows()
        grvDetail_R.OptionsSelection.MultiSelect = False
    End Sub
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
    Private Sub fn_LoadNOINVOICE()
        Dim oPurchaseInvoice As New Purchasing.clsPurchaseInvoice

        Try
            If grdKDVENDOR.Text <> String.Empty Then
                If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                    Dim ds = From x In oPurchaseInvoice.GetData _
                             Where x.KDVENDOR = grdKDVENDOR.EditValue And x.GRANDTOTAL > x.PAYAMOUNT _
                             Select NOINVOICE = x.KDPI, x.DATE, x.GRANDTOTAL, x.PAYAMOUNT

                    grdNOINVOICE.DataSource = ds.ToList
                    grdNOINVOICE.ValueMember = "NOINVOICE"
                    grdNOINVOICE.DisplayMember = "NOINVOICE"
                Else
                    Dim ds1 = From x In oPurchaseInvoice.GetData _
                            Where x.KDVENDOR = grdKDVENDOR.EditValue And x.GRANDTOTAL > x.PAYAMOUNT _
                            Select NOINVOICE = x.KDPI, x.DATE, x.GRANDTOTAL, x.PAYAMOUNT

                    Dim ds2 = From x In oPurchaseInvoice.GetData _
                              Join y In oCashOut.GetDataDetail _
                              On x.KDPI Equals y.NOINVOICE _
                              Where y.KDCASHOUT = sNoId _
                            Select NOINVOICE = x.KDPI, x.DATE, x.GRANDTOTAL, x.PAYAMOUNT

                    Dim ds = ds1.Union(ds2).Distinct

                    grdNOINVOICE.DataSource = ds.ToList
                    grdNOINVOICE.ValueMember = "NOINVOICE"
                    grdNOINVOICE.DisplayMember = "NOINVOICE"
                End If
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadNOINVOICE_R()
        Dim oPurchaseReturn As New Purchasing.clsPurchaseReturn

        Try
            If grdKDVENDOR.Text <> String.Empty Then
                If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                    Dim ds = From x In oPurchaseReturn.GetData _
                             Where x.KDVENDOR = grdKDVENDOR.EditValue And x.GRANDTOTAL > x.PAYAMOUNT _
                             Select NOINVOICE = x.KDPR, x.GRANDTOTAL, x.PAYAMOUNT

                    grdNOINVOICE_R.DataSource = ds.ToList
                    grdNOINVOICE_R.ValueMember = "NOINVOICE"
                    grdNOINVOICE_R.DisplayMember = "NOINVOICE"
                Else
                    Dim ds1 = From x In oPurchaseReturn.GetData _
                            Where x.KDVENDOR = grdKDVENDOR.EditValue And x.GRANDTOTAL > x.PAYAMOUNT _
                            Select NOINVOICE = x.KDPR, x.GRANDTOTAL, x.PAYAMOUNT

                    Dim ds2 = From x In oPurchaseReturn.GetData _
                              Join y In oCashOut.GetDataDetail _
                              On x.KDPR Equals y.NOINVOICE _
                              Where y.KDCASHOUT = sNoId _
                            Select NOINVOICE = x.KDPR, x.GRANDTOTAL, x.PAYAMOUNT

                    Dim ds = ds1.Union(ds2).Distinct

                    grdNOINVOICE_R.DataSource = ds.ToList
                    grdNOINVOICE_R.ValueMember = "NOINVOICE"
                    grdNOINVOICE_R.DisplayMember = "NOINVOICE"
                End If
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnKontraBon_Click(sender As Object, e As EventArgs) Handles btnKontraBon.Click
        Dim frmReportKontraBon As New frmReportKontraBon
        Try
            frmReportKontraBon.fn_LoadAmbil(True)
            frmReportKontraBon.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            frmReportKontraBon.Dispose()

            If sFind1 <> "" Then
                Dim dsKontraBon = oKontraBon.GetData(sFind1)
                If dsKontraBon IsNot Nothing Then
                    grdKDVENDOR.Text = dsKontraBon.KDVENDOR

                    fn_LoadNOINVOICE()

                    For Each xloop In oKontraBon.GetDataDetail(dsKontraBon.KDKONTRABON)
                        grvDetail.Focus()
                        grvDetail.AddNewRow()
                        grvDetail.SetFocusedRowCellValue(colNOINVOICE, xloop.NOINVOICE)
                        grvDetail.UpdateCurrentRow()
                    Next
                End If

            End If

            sFind1 = ""
        End Try
    End Sub
#End Region
End Class