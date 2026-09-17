Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmDirectSales
    Public Sub New()
        InitializeComponent()

        fn_Binding()
        fn_LoadKDCUSTOMER()
        fn_LoadKDSALESMAN()
        fn_LoadKDITEM()
        fn_LoadKDUOM()

        txtITEM.Focus()

        Me.Text = "Direct Sales"
    End Sub
    Private Sub frmDirectSales_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtITEM.Focus()
    End Sub
#Region "Function Key"
    Private Sub fn_LoadKDCUSTOMER()
        Dim oCUSTOMER As New Reference.clsCustomer
        Try
            grdKDCUSTOMER.Properties.DataSource = oCUSTOMER.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDCUSTOMER.Properties.ValueMember = "KDCUSTOMER"
            grdKDCUSTOMER.Properties.DisplayMember = "NAME_DISPLAY"

            grdKDCUSTOMER.Text = "CUSTOMER_0000000001"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDCUSTOMER_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdKDCUSTOMER.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdKDCUSTOMER.ResetText()
        End If
    End Sub
    Private Sub fn_LoadKDSALESMAN()
        Dim oSALESMAN As New Reference.clsSalesman
        Try
            grdKDSALESMAN.Properties.DataSource = oSALESMAN.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDSALESMAN.Properties.ValueMember = "KDSALESMAN"
            grdKDSALESMAN.Properties.DisplayMember = "NAME_DISPLAY"

            grdKDSALESMAN.Text = "SALESMAN_0000000001"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDSALESMAN_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdKDSALESMAN.KeyDown, grdKDSALESMAN.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdKDSALESMAN.ResetText()
        End If
    End Sub
    Private Sub fn_LoadKDITEM()
        Dim oItem As New Reference.clsItem
        Dim oWarehouse As New Reference.clsWarehouse

        Try
            Dim ds2 = From x In oItem.GetData
                      Join y In oItem.GetDataDetail_WAREHOUSE
                     On x.KDITEM Equals y.KDITEM
                      Join z In oItem.GetDataDetail_UOM
                     On x.KDITEM Equals z.KDITEM
                      Where x.ISACTIVE = True
                      Group x, y By x.KDITEM, x.NMITEM1, x.NMITEM2, z.PRICESALESSTANDARD Into AMOUNT = Sum(y.AMOUNT)
                      Select KDITEM, NMITEM1, NMITEM2, PRICE = CDbl(PRICESALESSTANDARD), AMOUNT = CDbl(AMOUNT)
                      Order By NMITEM1

            Dim ds1 = From x In oItem.GetData
                      Join z In oItem.GetDataDetail_UOM
                      On x.KDITEM Equals z.KDITEM
                      Where x.ISACTIVE = True
                      Select x.KDITEM, x.NMITEM1, x.NMITEM2, PRICE = CDbl(z.PRICESALESSTANDARD), AMOUNT = CDbl(0)

            Dim dsJoin = ds1.Union(ds2)

            Dim ds = From x In dsJoin
                     Group x By x.KDITEM, x.NMITEM1, x.NMITEM2, x.PRICE Into AMOUNT = Sum(x.AMOUNT)
                     Select KDITEM, NMITEM1, NMITEM2, PRICE, AMOUNT
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
    Private Sub fn_Binding()
        Try
            lblName.Text = sCompany
            lblAddress.Text = sAddress
            lblPhone.Text = "Phone : " & sPhone
            lblCashier.Text = sUserID
        Catch ex As Exception
            MsgBox("Load Binding Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
#Region "Shortcut Key"
    Private Sub frmPOS_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F1
                If e.Alt = True Then
                    grdKDCUSTOMER.Focus()
                End If
            Case Keys.F2
                txtItem_ButtonClick()
            Case Keys.F3
                If e.Alt = True Then
                    txtITEM.Focus()
                Else
                    picF3_Click()
                End If
            Case Keys.F5
                picF5_Click()
            Case Keys.F6
                picF6_Click()
            Case Keys.F7
                picF7_Click()
            Case Keys.F8
                picF8_Click()
            Case Keys.F9
                picF9_Click()
            Case Keys.F10
                picF10_Click()
            Case Keys.F11
                picF11_Click()
            Case Keys.F12
                picF12_Click()
        End Select
    End Sub
    Private Sub picF2_Click() Handles picF2.Click
        txtItem_ButtonClick()
    End Sub
    Private Sub picF3_Click() Handles picF3.Click
        Dim frmCUSTOMER As New frmCUSTOMER
        frmCUSTOMER.ShowDialog()
        fn_LoadKDCUSTOMER()
    End Sub
    Private Sub picF5_Click() Handles picF5.Click
        Dim frmCash As New frmCash
        frmCash.ShowDialog()
    End Sub
    Private Sub picF6_Click() Handles picF6.Click
        Dim fQuantity As New frmQuantity

        If grv.GetFocusedRowCellValue(colKDITEM) = String.Empty Then
            fQuantity.fn_Load(grv.GetRowCellValue(grv.RowCount - 2, colKDITEM))

            If fQuantity.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Dim oItem As New Reference.clsItem
                Dim sQty = 0

                'Try
                'sQty = CDec(oItem.GetDataWarehouse.FirstOrDefault(Function(x) x.M_Warehouse.IsDefault = True And x.KdItem = grv.GetRowCellValue(grv.RowCount - 2, cKdItem)).Amount)
                'Catch ex As Exception
                'sQty = 0
                'End Try

                'If sQty < fQuantity.iQuantity Then
                'MsgBox("Stock is not sufficient! Please consult your supervisor!", MsgBoxStyle.Information, Me.Text)
                'txtITEM.Focus()
                'Else
                grv.SetRowCellValue(grv.RowCount - 2, colQTY, fQuantity.iQuantity)
                grv.RefreshRow(grv.RowCount - 2)
                SendKeys.Send("{DOWN}")
                'End If
            End If
        Else
            fQuantity.fn_Load(grv.GetFocusedRowCellValue(colKDITEM))

            If fQuantity.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Dim oItem As New Reference.clsItem
                Dim sQty = 0

                'Try
                '    sQty = CDec(oItem.GetDataWarehouse.FirstOrDefault(Function(x) x.M_Warehouse.IsDefault = True And x.KdItem = grv.GetFocusedRowCellValue(cKdItem)).Amount)
                'Catch ex As Exception
                '    sQty = 0
                'End Try

                'If sQty < fQuantity.iQuantity Then
                '    MsgBox("Stock is not sufficient! Please consult your supervisor!", MsgBoxStyle.Information, Me.Text)
                '    txtITEM.Focus()
                'Else
                grv.SetFocusedRowCellValue(colQTY, fQuantity.iQuantity)
                grv.RefreshRow(grv.FocusedRowHandle())
                SendKeys.Send("{DOWN}")
                'End If
            End If
        End If
    End Sub
    Private Sub picF7_Click() Handles picF7.Click
        Dim fPrice As New frmPrice

        If grv.GetFocusedRowCellValue(colKDITEM) = String.Empty Then
            fPrice.fn_Load(grv.GetRowCellValue(grv.RowCount - 2, colKDITEM))

            If fPrice.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Dim oItem As New Reference.clsItem
                Dim sQty = 0

                grv.SetRowCellValue(grv.RowCount - 2, colPRICE, fPrice.iQuantity)
                grv.RefreshRow(grv.RowCount - 2)
                SendKeys.Send("{DOWN}")
            End If
        Else
            fPrice.fn_Load(grv.GetFocusedRowCellValue(colKDITEM))

            If fPrice.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Dim oItem As New Reference.clsItem

                grv.SetFocusedRowCellValue(colPRICE, fPrice.iQuantity)
                grv.RefreshRow(grv.FocusedRowHandle())
                SendKeys.Send("{DOWN}")
            End If
        End If
    End Sub
    Private Sub picF8_Click() Handles picF8.Click
        If grv.GetFocusedRowCellValue(colKDITEM) Is Nothing Then
            Exit Sub
        End If


        Dim fPrice As New frmPrice

        If grv.GetFocusedRowCellValue(colKDITEM) = String.Empty Then
            Dim frmBrowseSN As New frmBrowseSN
            frmBrowseSN.fn_LoadMe(0, grv.GetRowCellValue(grv.RowCount - 2, colKDITEM))
            frmBrowseSN.ShowDialog()

            grv.SetRowCellValue(grv.RowCount - 2, colREMARKS, sFind1)
            grv.RefreshRow(grv.RowCount - 2)
            SendKeys.Send("{DOWN}")
        Else
            Dim frmBrowseSN As New frmBrowseSN
            frmBrowseSN.fn_LoadMe(0, grv.GetFocusedRowCellValue(colKDITEM))
            frmBrowseSN.ShowDialog()

            grv.SetFocusedRowCellValue(colREMARKS, sFind1)
            grv.RefreshRow(grv.FocusedRowHandle())
            SendKeys.Send("{DOWN}")
        End If
    End Sub
    Private Sub picF9_Click() Handles picF9.Click
        Dim frmDirectSales As New frmDirectSales

        frmDirectSales.MdiParent = Me.MdiParent
        frmDirectSales.Show()
        frmDirectSales.WindowState = FormWindowState.Maximized
    End Sub
    Private Sub picF10_Click() Handles picF10.Click
        Dim frmDirectSales As New frmDirectSales

        frmDirectSales.MdiParent = Me.MdiParent
        Me.Close()
        frmDirectSales.Show()
        frmDirectSales.WindowState = FormWindowState.Maximized
    End Sub
    Private Function fn_Save(ByVal sTYPE As Integer) As Boolean
        Try

            Dim oSalesInvoice As New Sales.clsSalesInvoice
            Dim oWarehouse As New Reference.clsWarehouse

            ' ***** HEADER *****
            Dim ds = oSalesInvoice.GetStructureHeader
            With ds
                .DATECREATED = Now
                .DATEUPDATED = Now
                .KDSI = "-"
                .DATE = Now
                .DATEDUE = Now
                .KDCUSTOMER = IIf(String.IsNullOrEmpty(grdKDCUSTOMER.EditValue), String.Empty, grdKDCUSTOMER.EditValue)
                .KDSALESMAN = IIf(String.IsNullOrEmpty(grdKDSALESMAN.EditValue), String.Empty, grdKDSALESMAN.EditValue)
                .KDWAREHOUSE = oWarehouse.GetData().FirstOrDefault().KDWAREHOUSE
                .MEMO = "DIRECT SALES"
                .SUBTOTAL = CDec(txtGrandTotal.Text)

                .DISCOUNT = CDec(0)

                .TAX = CDec(0)

                .GRANDTOTAL = CDec(txtGrandTotal.Text) - .DISCOUNT
                .PAYAMOUNT = 0
                .KDUSER = sUserID
            End With


            ' ***** DETIL *****
            Dim arrDetail = oSalesInvoice.GetStructureDetailList
            For i As Integer = 0 To grv.RowCount - 2
                Dim dsDetail = oSalesInvoice.GetStructureDetail
                With dsDetail
                    .DATECREATED = Now
                    .DATEUPDATED = Now
                    .SEQ = i
                    .KDSI = ds.KDSI
                    .KDITEM = grv.GetRowCellValue(i, colKDITEM)
                    .KDUOM = grv.GetRowCellValue(i, colKDUOM)
                    .QTY = CDec(grv.GetRowCellValue(i, colQTY))
                    .PRICE = CDec(grv.GetRowCellValue(i, colPRICE))
                    .SUBTOTAL = CDec(grv.GetRowCellValue(i, colSUBTOTAL))
                    .DISCOUNT = CDec(grv.GetRowCellValue(i, colDISCOUNT))
                    .GRANDTOTAL = CDec(grv.GetRowCellValue(i, colGRANDTOTAL))
                    .REMARKS = IIf(String.IsNullOrEmpty(grv.GetRowCellValue(i, colREMARKS)), "-", grv.GetRowCellValue(i, colREMARKS))
                End With
                arrDetail.Add(dsDetail)
            Next

            Try
                Dim frmPayment As New frmPayment
                frmPayment.fn_Load(txtGrandTotal.Text, "-", ds, arrDetail, FORM_MODE.FORM_MODE_ADD)
                frmPayment.ShowDialog(Me)

                fn_Save = True
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Sub picF11_Click() Handles picF11.Click
        If grdKDCUSTOMER.Text = String.Empty Then
            MsgBox("Customer can't empty!", MsgBoxStyle.Information, Me.Text)
            grdKDCUSTOMER.Focus()
            Exit Sub
        End If
        If grdKDSALESMAN.Text = String.Empty Then
            MsgBox("Salesman can't empty!", MsgBoxStyle.Information, Me.Text)
            grdKDSALESMAN.Focus()
            Exit Sub
        End If

        For i As Integer = 0 To grv.RowCount - 2
            Dim oItem As New Reference.clsItem

            If grv.GetRowCellValue(i, colKDITEM) IsNot Nothing Then
                Dim ds = oItem.GetData(grv.GetRowCellValue(i, colKDITEM))

                If ds.M_ITEM_L1.MEMO = "HANDPHONE" Then
                    If grv.GetRowCellValue(i, colREMARKS) = "" Or grv.GetRowCellValue(i, colREMARKS) = "-" Then
                        MsgBox("Catatan harus diisi dengan nomer IMEI!", MsgBoxStyle.Exclamation, Me.Text)

                        grv.FocusedRowHandle = i
                        Exit Sub
                    End If
                End If
            End If
        Next

        If fn_Save(2) = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            If sSavePayment = True Then
                MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
                sCode = "Direct Sales"
                Me.Dispose()
                Me.Close()
            Else

            End If
        End If
    End Sub
    Private Sub picF12_Click() Handles picF12.Click
        Me.Close()
    End Sub
#End Region
#Region "Bind Item To Grid"
    Private Function ItemBinding(ByVal sKdItem As String) As Boolean
        ItemBinding = False

        Try
            If sKdItem <> String.Empty Then
                Dim oItem As New Reference.clsItem
                Dim ds = oItem.GetData().FirstOrDefault(Function(x) x.NMITEM1 = sKdItem)

                grv.AddNewRow()

                With ds
                    grv.SetFocusedRowCellValue(colKDITEM, .KDITEM)
                    grv.SetFocusedRowCellValue(colKDUOM, .M_ITEM_UOMs.FirstOrDefault().KDUOM)
                    grv.SetFocusedRowCellValue(colQTY, 1)
                    grv.SetFocusedRowCellValue(colPRICE, CDec(.M_ITEM_UOMs.FirstOrDefault().PRICESALESSTANDARD))
                End With

                grv.UpdateCurrentRow()
                ItemBinding = True
            End If
        Catch oErr As Exception
            MsgBox("Load Item : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            ItemBinding = False
        End Try
    End Function
    Private Sub txtItem_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtITEM.Leave
        If ItemBinding(txtITEM.Text.Trim.ToUpper) = True Then
            txtITEM.ResetText()
            txtItemName.ResetText()
            txtITEM.Focus()
            CalculateGrandTotal()
        Else
            grv.CancelUpdateCurrentRow()
            txtITEM.ResetText()
            txtItemName.ResetText()
            CalculateGrandTotal()
        End If
    End Sub
    Private Sub txtItem_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtITEM.EditValueChanged
        Try
            Dim oItem As New Reference.clsItem
            Dim ds = oItem.GetData().FirstOrDefault(Function(x) x.NMITEM1 = txtITEM.Text.Trim.ToUpper)

            txtItemName.Text = ds.NMITEM2
        Catch ex As Exception
            txtItemName.ResetText()
        End Try
    End Sub
    Private Sub txtItem_ButtonClick() Handles txtITEM.ButtonClick
        Dim frmBrowseItemPOS As New frmBrowseItemPOS
        frmBrowseItemPOS.fn_LoadMe(0)
        frmBrowseItemPOS.ShowDialog()

        txtITEM.Focus()

        txtITEM.Text = sFind1
        txtItemName.Text = sFind2
        sFind1 = String.Empty
        sFind2 = String.Empty
    End Sub
    Private Sub txtItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtITEM.KeyDown
        If e.KeyCode = Keys.F4 Then
            txtItem_ButtonClick()
        End If
    End Sub
    Private Sub grv_CellValueChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grv.CellValueChanged
        If e.Column.Name = colQTY.Name Then
            Try
                'If CDec(grv.GetFocusedRowCellValue(colQTY)) < 1 Then
                '    grv.SetFocusedRowCellValue(colQTY, 1)
                'Else
                '    'Dim oItem As New Reference.clsItem
                '    'Dim ds = oItem.GetData(grv.GetFocusedRowCellValue(colKDITEM))

                '    'If ds.M_ITEM_L1.MEMO = "HANDPHONE" Then
                '    '    grv.SetFocusedRowCellValue(colQTY, 1)
                '    'End If
                'End If

                Dim sQty = grv.GetFocusedRowCellValue(colQTY)
                Dim sPrice = grv.GetFocusedRowCellValue(colPRICE)

                grv.SetFocusedRowCellValue(colSUBTOTAL, sQty * sPrice)

                For iLoop As Integer = 0 To grv.RowCount - 2
                    sQty = grv.GetRowCellValue(iLoop, colQTY)
                    sPrice = grv.GetRowCellValue(iLoop, colPRICE)

                    grv.SetRowCellValue(iLoop, colSUBTOTAL, sQty * sPrice)
                Next
            Catch oErr As Exception
                MsgBox("Load Item : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf e.Column.Name = colPRICE.Name Then
            Try
                Dim sQty = grv.GetFocusedRowCellValue(colQTY)
                Dim sPrice = grv.GetFocusedRowCellValue(colPRICE)

                grv.SetFocusedRowCellValue(colSUBTOTAL, sQty * sPrice)

                For iLoop As Integer = 0 To grv.RowCount - 2
                    sQty = grv.GetRowCellValue(iLoop, colQTY)
                    sPrice = grv.GetRowCellValue(iLoop, colPRICE)

                    grv.SetRowCellValue(iLoop, colSUBTOTAL, sQty * sPrice)
                Next
            Catch oErr As Exception
                MsgBox("Load Item : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf e.Column.Name = colSUBTOTAL.Name Or e.Column.Name = colDISCOUNT.Name Then
            grv.SetFocusedRowCellValue(colGRANDTOTAL, CDec(grv.GetFocusedRowCellValue(colSUBTOTAL)) - (CDec(grv.GetFocusedRowCellValue(colSUBTOTAL)) * (CDec(grv.GetFocusedRowCellValue(colDISCOUNT)) / 100)))

            For iLoop As Integer = 0 To grv.RowCount - 2
                grv.SetRowCellValue(iLoop, colGRANDTOTAL, CDec(grv.GetRowCellValue(iLoop, colSUBTOTAL)) - (CDec(grv.GetRowCellValue(iLoop, colSUBTOTAL)) * (CDec(grv.GetRowCellValue(iLoop, colDISCOUNT)) / 100)))
            Next

            CalculateGrandTotal()
        End If
    End Sub
    Private Sub CalculateGrandTotal()
        Dim sTotal As Integer = 0
        For iLoop As Integer = 0 To grv.RowCount - 2
            sTotal += CDec(grv.GetRowCellValue(iLoop, colGRANDTOTAL))
        Next

        txtGrandTotal.Text = CDec(sTotal)
    End Sub
#End Region
End Class