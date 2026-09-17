Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmSalesInvoice
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oSalesInvoice As New Sales.clsSalesInvoice
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
            Me.Text = SalesInvoice.TITLE

            lKDSI.Text = SalesInvoice.KDSI
            lDATE.Text = SalesInvoice.TANGGAL
            lDATEDUE.Text = SalesInvoice.DATEDUE
            lKDCUSTOMER.Text = SalesInvoice.KDCUSTOMER & " *"
            lKDWAREHOUSE.Text = SalesInvoice.KDWAREHOUSE & " *"

            lSUBTOTAL.Text = SalesInvoice.SUBTOTAL
            lDISCOUNT.Text = SalesInvoice.DISCOUNT
            lTAX.Text = SalesInvoice.TAX
            lGRANDTOTAL.Text = SalesInvoice.GRANDTOTAL

            tab1.Text = SalesInvoice.TAB_DETAIL
            tab2.Text = SalesInvoice.TAB_MEMO

            grvDetail.Columns("KDITEM").Caption = SalesInvoice.DETAIL_KDITEM
            grvDetail.Columns("KDUOM").Caption = SalesInvoice.DETAIL_KDUOM
            grvDetail.Columns("QTY").Caption = SalesInvoice.DETAIL_QTY
            grvDetail.Columns("PRICE").Caption = PurchaseInvoice.DETAIL_PRICE
            grvDetail.Columns("SUBTOTAL").Caption = PurchaseInvoice.DETAIL_SUBTOTAL
            grvDetail.Columns("DISCOUNT").Caption = PurchaseInvoice.DETAIL_DISCOUNT
            grvDetail.Columns("GRANDTOTAL").Caption = PurchaseInvoice.DETAIL_GRANDTOTAL
            grvDetail.Columns("REMARKS").Caption = SalesInvoice.DETAIL_REMARKS

            grvKDCUSTOMER.Columns("NAME_DISPLAY").Caption = Customer.NAME_DISPLAY
            grvKDWAREHOUSE.Columns("NAME_DISPLAY").Caption = Warehouse.NAME_DISPLAY

            grvKDITEM.Columns("NMITEM1").Caption = Item.NMITEM1
            grvKDITEM.Columns("NMITEM2").Caption = Item.NMITEM2

            grvKDUOM.Columns("MEMO").Caption = UOM.MEMO

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnSavePayment.Caption = Caption.FormSavePayment
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDSI.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDCUSTOMER()
        fn_LoadKDWAREHOUSE()
        fn_LoadKDITEM()
        fn_LoadKDUOM()
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
        deDATEDUE.Properties.ReadOnly = Status
        grdKDCUSTOMER.Properties.ReadOnly = Status
        grdKDSALESMAN.Properties.ReadOnly = Status
        grdKDWAREHOUSE.Properties.ReadOnly = Status
        txtDISCOUNT.Properties.ReadOnly = Status
        txtTAX.Properties.ReadOnly = Status

        txtMEMO.Properties.ReadOnly = Status

        grvDetail.OptionsBehavior.ReadOnly = Status

        If sUserID = "ADMIN" Then
            btnCustomer.Enabled = False
        Else
            btnCustomer.Enabled = True
        End If
    End Sub
    Private Sub fn_EmptyMe()
        txtKDSI.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        deDATEDUE.DateTime = Now
        grdKDCUSTOMER.ResetText()
        'grdKDWAREHOUSE.ResetText()
        txtMEMO.ResetText()
        txtSUBTOTAL.ResetText()
        txtDISCOUNT.ResetText()
        txtTAX.ResetText()
        txtGRANDTOTAL.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oSalesInvoice.GetData(sNoId)

            With ds
                txtKDSI.Text = .KDSI
                deDATE.DateTime = .DATE
                deDATEDUE.DateTime = .DATEDUE
                grdKDCUSTOMER.Text = .KDCUSTOMER
                grdKDSALESMAN.Text = .KDSALESMAN
                grdKDWAREHOUSE.Text = .KDWAREHOUSE
                txtMEMO.Text = .MEMO
                txtSUBTOTAL.Text = .SUBTOTAL
                txtDISCOUNT.Text = .DISCOUNT
                txtTAX.Text = .TAX
                txtGRANDTOTAL.Text = .GRANDTOTAL

                bindingSource.DataSource = oSalesInvoice.GetDataDetail.Where(Function(x) x.KDSI = sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = bindingSource
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If grdKDCUSTOMER.Text = String.Empty Then
                grdKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDCUSTOMER.ErrorText = Statement.ErrorRequired

                grdKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDSALESMAN.Text = String.Empty Then
                grdKDSALESMAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDSALESMAN.ErrorText = Statement.ErrorRequired

                grdKDSALESMAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDWAREHOUSE.Text = String.Empty Then
                grdKDWAREHOUSE.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDWAREHOUSE.ErrorText = Statement.ErrorRequired

                grdKDWAREHOUSE.Focus()
                fn_Validate = False
                Exit Function
            End If

            grvDetail.UpdateCurrentRow()

            If grvDetail.RowCount < 2 Then
                MsgBox(Statement.ErrorDetail, MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End If

            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim oItem As New Reference.clsItem

                If grvDetail.GetRowCellValue(i, colKDITEM) IsNot Nothing And grvDetail.GetRowCellValue(i, colKDUOM) IsNot Nothing Then
                    Dim ds = oItem.GetData(grvDetail.GetRowCellValue(i, colKDITEM))

                    If ds.M_ITEM_L1.MEMO = "HANDPHONE" Then
                        If grvDetail.GetRowCellValue(i, colREMARKS) = "" Or grvDetail.GetRowCellValue(i, colREMARKS) = "-" Then
                            MsgBox("Catatan harus diisi dengan nomer IMEI!", MsgBoxStyle.Exclamation, Me.Text)

                            grvDetail.FocusedRowHandle = i
                            fn_Validate = False
                            Exit Function
                        Else
                            Dim sCOUNT = grvDetail.GetRowCellValue(i, colREMARKS).ToString().Split(Environment.NewLine).ToList()

                            If CDec(grvDetail.GetRowCellValue(i, colQTY)) <> sCOUNT.Count Then
                                MsgBox("Nomer IMEI harus sesuai dengan jumlah yang dimasukkan!" & vbCrLf & "Anda baru memasukkan : " & sCOUNT.Count & " dari " & FormatNumber(grvDetail.GetFocusedRowCellValue(colQTY), 0) & " nomer IMEI!", MsgBoxStyle.Exclamation, Me.Text)

                                grvDetail.FocusedRowHandle = i
                                fn_Validate = False
                                Exit Function
                            End If
                        End If
                    End If
                End If
            Next
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save(ByVal sTYPE As Integer) As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oSalesInvoice.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oSalesInvoice.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDSI = sNoId
                .DATE = deDATE.DateTime
                .DATEDUE = deDATEDUE.DateTime
                .KDCUSTOMER = IIf(String.IsNullOrEmpty(grdKDCUSTOMER.EditValue), String.Empty, grdKDCUSTOMER.EditValue)
                .KDSALESMAN = IIf(String.IsNullOrEmpty(grdKDSALESMAN.EditValue), String.Empty, grdKDSALESMAN.EditValue)
                .KDWAREHOUSE = IIf(String.IsNullOrEmpty(grdKDWAREHOUSE.EditValue), String.Empty, grdKDWAREHOUSE.EditValue)
                .MEMO = txtMEMO.Text.Trim.ToUpper
                .SUBTOTAL = CDec(txtSUBTOTAL.Text)
                .DISCOUNT = CDec(txtDISCOUNT.Text)
                .TAX = CDec(txtTAX.Text)
                .GRANDTOTAL = CDec(txtGRANDTOTAL.Text)
                Try
                    .PAYAMOUNT = oSalesInvoice.GetData(sNoId).PAYAMOUNT
                Catch oErr As Exception
                    .PAYAMOUNT = 0
                End Try
                .KDUSER = sUserID
            End With

            ' ***** DETIL *****
            Dim arrDetail = oSalesInvoice.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oSalesInvoice.GetStructureDetail
                With dsDetail
                    Try
                        .DATECREATED = oSalesInvoice.GetData(sNoId).DATECREATED
                    Catch oErr As Exception
                        .DATECREATED = Now
                    End Try
                    .DATEUPDATED = Now

                    .SEQ = i
                    .KDSI = ds.KDSI
                    .KDITEM = grvDetail.GetRowCellValue(i, colKDITEM)
                    .KDUOM = grvDetail.GetRowCellValue(i, colKDUOM)
                    .QTY = CDec(grvDetail.GetRowCellValue(i, colQTY))
                    .PRICE = CDec(grvDetail.GetRowCellValue(i, colPRICE))
                    .SUBTOTAL = CDec(grvDetail.GetRowCellValue(i, colSUBTOTAL))
                    .DISCOUNT = CDec(grvDetail.GetRowCellValue(i, colDISCOUNT))
                    .GRANDTOTAL = CDec(grvDetail.GetRowCellValue(i, colGRANDTOTAL))
                    .REMARKS = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colREMARKS)), "-", grvDetail.GetRowCellValue(i, colREMARKS))
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    If sTYPE = 1 Then
                        fn_Save = oSalesInvoice.InsertData(ds, arrDetail)
                    ElseIf sTYPE = 2 Then
                        Dim frmPayment As New frmPayment
                        frmPayment.fn_Load(txtGRANDTOTAL.Text, "-", ds, arrDetail, FORM_MODE.FORM_MODE_ADD)
                        frmPayment.ShowDialog(Me)

                        fn_Save = True
                    End If
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    If sTYPE = 1 Then
                        fn_Save = oSalesInvoice.UpdateData(ds, arrDetail)
                    ElseIf sTYPE = 2 Then
                        Dim frmPayment As New frmPayment
                        frmPayment.fn_Load(txtGRANDTOTAL.Text, "-", ds, arrDetail, FORM_MODE.FORM_MODE_EDIT)
                        frmPayment.ShowDialog(Me)

                        fn_Save = True
                    End If
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
    Private Sub OnValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDISCOUNT.EditValueChanged, txtTAX.EditValueChanged, grvDetail.FocusedRowChanged
        If isLoad Then
            Calculate()
        End If
    End Sub
    Private Sub Calculate()
        Dim sSubTotal = 0
        For i As Integer = 0 To grvDetail.RowCount - 2
            sSubTotal += CDec(grvDetail.GetRowCellValue(i, colGRANDTOTAL))
        Next

        txtSUBTOTAL.Text = sSubTotal

        txtGRANDTOTAL.Text = CDec(txtSUBTOTAL.Text) - CDec(txtDISCOUNT.Text) + CDec(txtTAX.Text)
    End Sub
    Private Sub grvDetail_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDetail.CellValueChanged
        If e.Column.Name = colKDITEM.Name Then
            Dim oItem As New Reference.clsItem
            Try
                If grvDetail.GetFocusedRowCellValue(colKDITEM) IsNot Nothing Then
                    Dim ds = oItem.GetDataDetail_UOM(grvDetail.GetFocusedRowCellValue(colKDITEM))

                    If ds IsNot Nothing Then
                        grvDetail.SetFocusedRowCellValue(colKDUOM, ds.FirstOrDefault(Function(x) x.RATE = 1).KDUOM)
                    Else
                        MsgBox(Statement.ErrorUOM, MsgBoxStyle.Exclamation, Me.Text)

                        grvDetail.CancelUpdateCurrentRow()
                    End If
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf e.Column.Name = colKDUOM.Name Then
            Dim oItem As New Reference.clsItem
            Try
                If grvDetail.GetFocusedRowCellValue(colKDITEM) IsNot Nothing And grvDetail.GetFocusedRowCellValue(colKDUOM) IsNot Nothing Then
                    Dim ds = oItem.GetDataDetail_UOM(grvDetail.GetFocusedRowCellValue(colKDITEM), grvDetail.GetFocusedRowCellValue(colKDUOM))

                    If ds IsNot Nothing Then
                        Try
                            Dim sLASTPRICE = oSalesInvoice.GetDataLastPrice(grdKDCUSTOMER.EditValue, grvDetail.GetFocusedRowCellValue(colKDITEM), grvDetail.GetFocusedRowCellValue(colKDUOM))

                            If sLASTPRICE <> 0 Then
                                grvDetail.SetFocusedRowCellValue(colPRICE, sLASTPRICE)
                            Else
                                grvDetail.SetFocusedRowCellValue(colPRICE, ds.PRICESALESSTANDARD)
                            End If
                        Catch ex As Exception
                            grvDetail.SetFocusedRowCellValue(colPRICE, ds.PRICESALESSTANDARD)
                        End Try
                    Else
                        MsgBox(Statement.ErrorUOM, MsgBoxStyle.Exclamation, Me.Text)

                        Dim sItem = grvDetail.GetFocusedRowCellValue(colKDITEM)
                        grvDetail.CancelUpdateCurrentRow()

                        grvDetail.AddNewRow()
                        grvDetail.SetFocusedRowCellValue(colKDITEM, sItem)
                    End If
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf e.Column.Name = colQTY.Name Or e.Column.Name = colPRICE.Name Then
            'If CDec(grvDetail.GetFocusedRowCellValue(colQTY)) < 1 Then
            '    grvDetail.SetFocusedRowCellValue(colQTY, 1)
            'End If

            Dim sSubTotal As Decimal = CDec(grvDetail.GetFocusedRowCellValue(colQTY)) * CDec(grvDetail.GetFocusedRowCellValue(colPRICE))

            grvDetail.SetFocusedRowCellValue(colSUBTOTAL, sSubTotal)
        ElseIf e.Column.Name = colSUBTOTAL.Name Or e.Column.Name = colDISCOUNT.Name Then
            Dim sGrandTotal As Decimal = CDec(grvDetail.GetFocusedRowCellValue(colSUBTOTAL)) - CDec(grvDetail.GetFocusedRowCellValue(colDISCOUNT))

            grvDetail.SetFocusedRowCellValue(colGRANDTOTAL, sGrandTotal)
        End If
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail.DeleteSelectedRows()
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmSalesInvoice_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClose_Click()
                'Case Keys.F2
                '    If btnSaveNew.Enabled = True Then
                '        btnSaveNew_Click()
                '    End If
                'Case Keys.F3
                '    If btnSaveClose.Enabled = True Then
                '        btnSaveClose_Click()
                '    End If
            Case Keys.F5
                If btnSavePayment.Enabled = True Then
                    btnSavePayment_Click()
                End If
        End Select
    End Sub
    Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save(1) = False Then
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
        If fn_Save(1) = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnSavePayment_Click() Handles btnSavePayment.ItemClick
        If fn_Validate() = False Then Exit Sub
        If fn_Save(2) = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            If sSavePayment = True Then
                MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
                Me.Close()
            Else

            End If
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadKDCUSTOMER()
        Dim oCUSTOMER As New Reference.clsCustomer
        Try
            grdKDCUSTOMER.Properties.DataSource = oCUSTOMER.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDCUSTOMER.Properties.ValueMember = "KDCUSTOMER"
            grdKDCUSTOMER.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDCUSTOMER_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdKDCUSTOMER.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdKDCUSTOMER.ResetText()
        End If
    End Sub
    Private Sub btnCustomer_Click(sender As System.Object, e As System.EventArgs) Handles btnCustomer.Click
        frmCustomer.LoadMe(FORM_MODE.FORM_MODE_ADD)
        frmCustomer.ShowDialog(Me)
        fn_LoadKDCUSTOMER()
    End Sub
    Private Sub grdKDSALESMAN_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdKDSALESMAN.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdKDSALESMAN.ResetText()
        End If
    End Sub
    Private Sub fn_LoadKDWAREHOUSE()
        Dim oWAREHOUSE As New Reference.clsWarehouse
        Try
            grdKDWAREHOUSE.Properties.DataSource = oWAREHOUSE.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDWAREHOUSE.Properties.ValueMember = "KDWAREHOUSE"
            grdKDWAREHOUSE.Properties.DisplayMember = "NAME_DISPLAY"

            grdKDWAREHOUSE.Text = oWAREHOUSE.GetData().Where(Function(x) x.ISACTIVE = True).FirstOrDefault().KDWAREHOUSE
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDWAREHOUSE_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdKDWAREHOUSE.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdKDWAREHOUSE.ResetText()
        End If
    End Sub
    Private Sub fn_LoadKDITEM()
        Dim oItem As New Reference.clsItem
        Dim oWarehouse As New Reference.clsWarehouse

        Try
            Dim ds2 = From x In oItem.GetData _
                     Join y In oItem.GetDataDetail_WAREHOUSE _
                     On x.KDITEM Equals y.KDITEM _
                     Join z In oItem.GetDataDetail_UOM _
                     On x.KDITEM Equals z.KDITEM _
                     Where x.ISACTIVE = True _
                     Group x, y By x.KDITEM, x.NMITEM1, x.NMITEM2, z.PRICESALESSTANDARD Into AMOUNT = Sum(y.AMOUNT) _
                     Select KDITEM, NMITEM1, NMITEM2, PRICE = CDbl(PRICESALESSTANDARD), AMOUNT = CDbl(AMOUNT) _
                     Order By NMITEM1

            Dim ds1 = From x In oItem.GetData _
                      Join z In oItem.GetDataDetail_UOM _
                      On x.KDITEM Equals z.KDITEM _
                      Where x.ISACTIVE = True _
                     Select x.KDITEM, x.NMITEM1, x.NMITEM2, PRICE = CDbl(z.PRICESALESSTANDARD), AMOUNT = CDbl(0)

            Dim dsJoin = ds1.Union(ds2)

            Dim ds = From x In dsJoin _
                     Group x By x.KDITEM, x.NMITEM1, x.NMITEM2, x.PRICE Into AMOUNT = Sum(x.AMOUNT) _
                     Select KDITEM, NMITEM1, NMITEM2, PRICE, AMOUNT _
                     Order By NMITEM1

            grdKDITEM.DataSource = ds.ToList()
            grdKDITEM.ValueMember = "KDITEM"
            grdKDITEM.DisplayMember = "NMITEM2"
        Catch oErr As Exception
            MsgBox("Load Item Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDUOM()
        Dim oUOM As New Reference.clsUOM
        Try
            grdKDUOM.DataSource = oUOM.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDUOM.ValueMember = "KDUOM"
            grdKDUOM.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class