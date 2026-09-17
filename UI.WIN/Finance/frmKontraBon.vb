Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmKontraBon
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oKontraBon As New Finance.clsKontraBon
    Private sNOINVOICE As New List(Of String)
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
            Me.Text = Kontrabon.TITLE

            lKDCASH.Text = Kontrabon.KDKONTRABON
            lDATE.Text = Kontrabon.TANGGAL
            lTANGGALJATUHTEMPO.Text = Kontrabon.TANGGALJATUHTEMPO
            lKDVENDOR.Text = Kontrabon.KDVENDOR & " *"
            lKDPAYMENTTYPE.Text = Kontrabon.KDPAYMENTTYPE & " *"

            tab1.Text = Kontrabon.TAB_DETAIL
            tab3.Text = Kontrabon.TAB_MEMO

            lSUBTOTAL.Text = Kontrabon.SUBTOTAL
            lADMIN.Text = Kontrabon.ADMIN
            lROUND.Text = Kontrabon.ROUND
            lGRANDTOTAL.Text = Kontrabon.GRANDTOTAL

            grvDetail.Columns("NOINVOICE").Caption = Kontrabon.DETAIL_NOINVOICE
            grvDetail.Columns("AMOUNTORIGINAL_UB").Caption = Kontrabon.DETAIL_AMOUNTORIGINAL
            grvDetail.Columns("AMOUNTDUE_UB").Caption = Kontrabon.DETAIL_AMOUNTDUE
            grvDetail.Columns("AMOUNTPAYMENT").Caption = Kontrabon.DETAIL_AMOUNTPAYMENT
            grvDetail.Columns("REMARKS").Caption = Kontrabon.DETAIL_REMARKS

            grvKDVENDOR.Columns("NAME_DISPLAY").Caption = Vendor.NAME_DISPLAY
            grvKDPAYMENTTYPE.Columns("MEMO").Caption = PaymentType.MEMO

            grvNOINVOICE.Columns("NOINVOICE").Caption = PurchaseInvoice.KDPI

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDKontraBon.Text.Trim.ToUpper
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
    End Sub
    Private Sub fn_ViewMode(ByVal Status As Boolean)
        btnSaveNew.Enabled = Not Status
        btnSaveClose.Enabled = Not Status

        deDATE.Properties.ReadOnly = Status
        deTANGGALJATUHTEMPO.Properties.ReadOnly = Status
        grdKDVENDOR.Properties.ReadOnly = Status
        grdKDPAYMENTTYPE.Properties.ReadOnly = Status
        txtMEMO.Properties.ReadOnly = Status
        txtADMIN.Properties.ReadOnly = Status
        txtROUND.Properties.ReadOnly = Status

        grvDetail.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDKontraBon.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        deTANGGALJATUHTEMPO.DateTime = Now
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
            Dim ds = oKontraBon.GetData(sNoId)

            With ds
                txtKDKontraBon.Text = .KDKONTRABON
                deDATE.DateTime = .DATE
                deTANGGALJATUHTEMPO.DateTime = .TANGGALJATUHTEMPO
                grdKDVENDOR.Text = .KDVENDOR
                grdKDPAYMENTTYPE.Text = .KDPAYMENTTYPE
                txtMEMO.Text = .MEMO

                txtSUBTOTAL.Text = .SUBTOTAL
                txtADMIN.Text = .ADMIN
                txtROUND.Text = .ROUND
                txtGRANDTOTAL.Text = .GRANDTOTAL

                bindingSource.DataSource = oKontraBon.GetDataDetail(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = bindingSource

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
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oKontraBon.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oKontraBon.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDKONTRABON = sNoId
                .DATE = deDATE.DateTime
                .TANGGALJATUHTEMPO = deTANGGALJATUHTEMPO.DateTime
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
            Dim arrDetail = oKontraBon.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oKontraBon.GetStructureDetail
                With dsDetail
                    Try
                        .DATECREATED = oKontraBon.GetData(sNoId).DATECREATED
                    Catch oErr As Exception
                        .DATECREATED = Now
                    End Try
                    .DATEUPDATED = Now

                    .SEQ = i
                    .KDKONTRABON = ds.KDKONTRABON
                    .NOINVOICE = grvDetail.GetRowCellValue(i, colNOINVOICE)
                    .AMOUNTPAYMENT = CDec(grvDetail.GetRowCellValue(i, colAMOUNTPAYMENT))
                    .REMARKS = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colREMARKS)), "-", grvDetail.GetRowCellValue(i, colREMARKS))
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oKontraBon.InsertData(ds, arrDetail)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oKontraBon.UpdateData(ds, arrDetail)
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
    Private Sub OnValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtADMIN.EditValueChanged, txtROUND.EditValueChanged, grvDetail.FocusedRowChanged, grdKDVENDOR.Validated
        If isLoad Then
            Calculate()
        End If
    End Sub
    Private Sub Calculate()
        Dim sSubTotal As Decimal = 0
        For i As Integer = 0 To grvDetail.RowCount - 2
            sSubTotal += CDec(grvDetail.GetRowCellValue(i, colAMOUNTPAYMENT))
        Next

        txtSUBTOTAL.Text = sSubTotal

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
            Dim oKontraBon As New Finance.clsKontraBon
            Try
                If grvDetail.GetRowCellValue(e.ListSourceRowIndex, colNOINVOICE) <> String.Empty Then
                    If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                        e.Value = oPurchaseInvoice.GetData(grvDetail.GetRowCellValue(e.ListSourceRowIndex, colNOINVOICE)).GRANDTOTAL - oPurchaseInvoice.GetData(grvDetail.GetRowCellValue(e.ListSourceRowIndex, colNOINVOICE)).PAYAMOUNT - grvDetail.GetRowCellValue(e.ListSourceRowIndex, colAMOUNTPAYMENT)
                    Else
                        Dim sPayment = (From x In oKontraBon.GetDataDetail
                                        Where x.KDKONTRABON = txtKDKONTRABON.Text _
                                       And x.NOINVOICE = grvDetail.GetRowCellValue(e.ListSourceRowIndex, colNOINVOICE)
                                        Group x By x.NOINVOICE Into Sum(x.AMOUNTPAYMENT)
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
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail.DeleteSelectedRows()
    End Sub
    Private Sub grdNOINVOICE_QueryPopUp(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles grdNOINVOICE.QueryPopUp
        sNOINVOICE.Clear()

        For iLoop As Integer = 0 To grvDetail.RowCount - 2
            sNOINVOICE.Add(grvDetail.GetRowCellValue(iLoop, colNOINVOICE))
        Next
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmKontraBon_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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

        grvDetail.OptionsSelection.MultiSelect = True
        grvDetail.SelectAll()
        grvDetail.DeleteSelectedRows()
        grvDetail.OptionsSelection.MultiSelect = False
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
                    Dim ds = From x In oPurchaseInvoice.GetData
                             Where x.KDVENDOR = grdKDVENDOR.EditValue And x.GRANDTOTAL > x.PAYAMOUNT
                             Select NOINVOICE = x.KDPI, x.DATE, x.GRANDTOTAL, x.PAYAMOUNT

                    grdNOINVOICE.DataSource = ds.ToList
                    grdNOINVOICE.ValueMember = "NOINVOICE"
                    grdNOINVOICE.DisplayMember = "NOINVOICE"
                Else
                    Dim ds1 = From x In oPurchaseInvoice.GetData
                              Where x.KDVENDOR = grdKDVENDOR.EditValue And x.GRANDTOTAL > x.PAYAMOUNT
                              Select NOINVOICE = x.KDPI, x.DATE, x.GRANDTOTAL, x.PAYAMOUNT

                    Dim ds2 = From x In oPurchaseInvoice.GetData
                              Join y In oKontraBon.GetDataDetail
                              On x.KDPI Equals y.NOINVOICE
                              Where y.KDKONTRABON = sNoId
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
#End Region
End Class