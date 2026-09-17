Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmPurchaseInvoice
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oPurchaseInvoice As New Purchasing.clsPurchaseInvoice
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
        fn_LoadSecurity()
    End Sub
    Private Sub fn_LoadSecurity()
        Try
            Dim oOtority As New Setting.clsOtority
            Dim oUser As New Setting.clsUser

            Dim ds = (From x In oOtority.GetDataDetail _
                     Join y In oUser.GetData _
                     On x.KDOTORITY Equals y.KDOTORITY _
                     Where x.MODUL = "ITEM_PRICE" _
                     And y.KDUSER = sUserID _
                     Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                If ds.ISVIEW = True Then
                    grvDetail.Columns("PRICE").Visible = True
                    grvDetail.Columns("SUBTOTAL").Visible = True
                    grvDetail.Columns("DISCOUNT").Visible = True
                    grvDetail.Columns("GRANDTOTAL").Visible = True

                    lSUBTOTAL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    lDISCOUNT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    lTAX.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    lGRANDTOTAL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Else
                    grvDetail.Columns("PRICE").Visible = False
                    grvDetail.Columns("SUBTOTAL").Visible = False
                    grvDetail.Columns("DISCOUNT").Visible = False
                    grvDetail.Columns("GRANDTOTAL").Visible = False

                    lSUBTOTAL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    lDISCOUNT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    lTAX.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    lGRANDTOTAL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                End If
            Catch ex As Exception
                MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)

                grvDetail.Columns("PRICE").Visible = False
                grvDetail.Columns("SUBTOTAL").Visible = False
                grvDetail.Columns("DISCOUNT").Visible = False
                grvDetail.Columns("GRANDTOTAL").Visible = False

                lSUBTOTAL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lDISCOUNT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lTAX.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lGRANDTOTAL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End Try
        Catch ex As Exception
            MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = PurchaseInvoice.TITLE

            lKDPI.Text = PurchaseInvoice.KDPI
            lDATE.Text = PurchaseInvoice.TANGGAL
            lDATEDUE.Text = PurchaseInvoice.DATEDUE
            lKDVENDOR.Text = PurchaseInvoice.KDVENDOR & " *"
            lKDWAREHOUSE.Text = PurchaseInvoice.KDWAREHOUSE & " *"

            lSUBTOTAL.Text = PurchaseInvoice.SUBTOTAL
            lDISCOUNT.Text = PurchaseInvoice.DISCOUNT
            lTAX.Text = PurchaseInvoice.TAX
            lGRANDTOTAL.Text = PurchaseInvoice.GRANDTOTAL

            tab1.Text = PurchaseInvoice.TAB_DETAIL
            tab2.Text = PurchaseInvoice.TAB_MEMO

            grvDetail.Columns("KDITEM").Caption = PurchaseInvoice.DETAIL_KDITEM
            grvDetail.Columns("KDUOM").Caption = PurchaseInvoice.DETAIL_KDUOM
            grvDetail.Columns("QTY").Caption = PurchaseInvoice.DETAIL_QTY
            grvDetail.Columns("PRICE").Caption = PurchaseInvoice.DETAIL_PRICE + " PPN"
            grvDetail.Columns("SUBTOTAL").Caption = PurchaseInvoice.DETAIL_SUBTOTAL
            grvDetail.Columns("DISCOUNT").Caption = PurchaseInvoice.DETAIL_DISCOUNT
            grvDetail.Columns("GRANDTOTAL").Caption = PurchaseInvoice.DETAIL_GRANDTOTAL
            grvDetail.Columns("REMARKS").Caption = PurchaseInvoice.DETAIL_REMARKS

            grvKDVENDOR.Columns("NAME_DISPLAY").Caption = Vendor.NAME_DISPLAY
            grvKDWAREHOUSE.Columns("NAME_DISPLAY").Caption = Warehouse.NAME_DISPLAY

            grvKDITEM.Columns("NMITEM1").Caption = Item.NMITEM1_2
            grvKDITEM.Columns("NMITEM2").Caption = Item.NMITEM2_2
            grvKDITEM.Columns("NMITEM3").Caption = Item.NMITEM3_2

            grvKDUOM.Columns("MEMO").Caption = UOM.MEMO

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDPI.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDVENDOR()
        fn_LoadKDWAREHOUSE()
        fn_LoadKDITEM()
        fn_LoadKDUOM()

        txtKDPI.Properties.ReadOnly = True

        Select Case oFormMode
            Case FORM_MODE.FORM_MODE_VIEW
                fn_ViewMode(True)
                fn_LoadData()
            Case FORM_MODE.FORM_MODE_ADD
                fn_ViewMode(False)
                fn_EmptyMe()

                txtKDPI.Properties.ReadOnly = False
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
        grdKDVENDOR.Properties.ReadOnly = Status
        grdKDWAREHOUSE.Properties.ReadOnly = Status
        txtDISCOUNT.Properties.ReadOnly = Status
        txtTAX.Properties.ReadOnly = Status

        txtMEMO.Properties.ReadOnly = Status

        grvDetail.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDPI.ResetText()
        deDATE.DateTime = Now
        deDATEDUE.DateTime = Now
        grdKDVENDOR.ResetText()
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
            Dim ds = oPurchaseInvoice.GetData(sNoId)

            With ds
                txtKDPI.Text = .KDPI
                deDATE.DateTime = .DATE
                deDATEDUE.DateTime = .DATEDUE
                grdKDVENDOR.Text = .KDVENDOR
                grdKDWAREHOUSE.Text = .KDWAREHOUSE
                txtMEMO.Text = .MEMO
                txtSUBTOTAL.Text = .SUBTOTAL
                txtDISCOUNT.Text = .DISCOUNT
                txtTAX.Text = .TAX
                txtGRANDTOTAL.Text = .GRANDTOTAL

                bindingSource.DataSource = oPurchaseInvoice.GetDataDetail.Where(Function(x) x.KDPI = sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = bindingSource
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If txtKDPI.Text = "" Then
                MsgBox("Nomor Faktur Kosong", MsgBoxStyle.Exclamation, Me.Text)
                txtKDPI.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDPI.ErrorText = Statement.ErrorRequired

                txtKDPI.Focus()
                fn_Validate = False
                Exit Function
            End If

            If grdKDVENDOR.Text = String.Empty Then
                grdKDVENDOR.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDVENDOR.ErrorText = Statement.ErrorRequired

                grdKDVENDOR.Focus()
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
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oPurchaseInvoice.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oPurchaseInvoice.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDPI = txtKDPI.Text.Trim.ToUpper
                .DATE = deDATE.DateTime
                .DATEDUE = deDATEDUE.DateTime
                .KDVENDOR = IIf(String.IsNullOrEmpty(grdKDVENDOR.EditValue), String.Empty, grdKDVENDOR.EditValue)
                .KDWAREHOUSE = IIf(String.IsNullOrEmpty(grdKDWAREHOUSE.EditValue), String.Empty, grdKDWAREHOUSE.EditValue)
                .MEMO = txtMEMO.Text.Trim.ToUpper
                .SUBTOTAL = CDec(txtSUBTOTAL.Text)
                .DISCOUNT = CDec(txtDISCOUNT.Text)
                .TAX = CDec(txtTAX.Text)
                .GRANDTOTAL = CDec(txtGRANDTOTAL.Text)
                Try
                    .PAYAMOUNT = oPurchaseInvoice.GetData(sNoId).PAYAMOUNT
                Catch oErr As Exception
                    .PAYAMOUNT = 0
                End Try
                .KDUSER = sUserID
            End With

            ' ***** DETIL *****
            Dim arrDetail = oPurchaseInvoice.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oPurchaseInvoice.GetStructureDetail
                With dsDetail
                    Try
                        .DATECREATED = oPurchaseInvoice.GetData(sNoId).DATECREATED
                    Catch oErr As Exception
                        .DATECREATED = Now
                    End Try
                    .DATEUPDATED = Now

                    .SEQ = i
                    .KDPI = ds.KDPI
                    .KDITEM = grvDetail.GetRowCellValue(i, colKDITEM)
                    .KDUOM = grvDetail.GetRowCellValue(i, colKDUOM)
                    .QTY = CDec(grvDetail.GetRowCellValue(i, colQTY))
                    .PRICE = CDec(grvDetail.GetRowCellValue(i, colPRICE))
                    .SUBTOTAL = CDec(grvDetail.GetRowCellValue(i, colSUBTOTAL))
                    .DISCOUNT = CDec(grvDetail.GetRowCellValue(i, colDISCOUNT))
                    .GRANDTOTAL = CDec(grvDetail.GetRowCellValue(i, colGRANDTOTAL))
                    .REMARKS = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colREMARKS)), "-", grvDetail.GetRowCellValue(i, colREMARKS))
                    .TANGGALEXPIRE = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colTANGGALEXPIRE)), Now, grvDetail.GetRowCellValue(i, colTANGGALEXPIRE))
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oPurchaseInvoice.InsertData(ds, arrDetail)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oPurchaseInvoice.UpdateData(ds, arrDetail)
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
                        'ds.FirstOrDefault(Function(x) x.RATE = 1).KDUOM
                        grvDetail.SetFocusedRowCellValue(colKDUOM, ds.FirstOrDefault(Function(x) x.RATE = 1).KDUOM)
                        grvDetail.SetFocusedRowCellValue(colTANGGALEXPIRE, Now)
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
                        'Try
                        '    Dim sLASTPRICE = oPurchaseInvoice.GetDataLastPrice(grdKDVENDOR.EditValue, grvDetail.GetFocusedRowCellValue(colKDITEM), grvDetail.GetFocusedRowCellValue(colKDUOM))

                        '    If sLASTPRICE <> 0 Then
                        '        grvDetail.SetFocusedRowCellValue(colPRICE, sLASTPRICE)
                        '    Else
                        '        grvDetail.SetFocusedRowCellValue(colPRICE, ds.PRICEPURCHASESTANDARD)
                        '    End If
                        'Catch ex As Exception
                        '    grvDetail.SetFocusedRowCellValue(colPRICE, ds.PRICEPURCHASESTANDARD)
                        'End Try

                        grvDetail.SetFocusedRowCellValue(colPRICE, ds.PRICEPURCHASESTANDARD)

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
            If CDec(grvDetail.GetFocusedRowCellValue(colQTY)) < 1 Then
                grvDetail.SetFocusedRowCellValue(colQTY, 1)
            End If

            Dim oItem As New Reference.clsItem
            Dim ds = oItem.GetDataDetail_UOM(grvDetail.GetFocusedRowCellValue(colKDITEM), grvDetail.GetFocusedRowCellValue(colKDUOM))

            'If grvDetail.GetFocusedRowCellValue(colPRICE) > ds.PRICESALESSTANDARD Then
            '    If MessageBox.Show("Harga Jual : " & FormatNumber(ds.PRICESALESSTANDARD) & vbCrLf & "Apakah akan dilanjutkan?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then

            '    Else
            '        grvDetail.CancelUpdateCurrentRow()
            '    End If
            'End If

            Dim sSubTotal As Decimal = CDec(grvDetail.GetFocusedRowCellValue(colQTY)) * CDec(grvDetail.GetFocusedRowCellValue(colPRICE))

            grvDetail.SetFocusedRowCellValue(colSUBTOTAL, sSubTotal)

            Dim dsCek = oItem.GetDataDetail_UOM(grvDetail.GetFocusedRowCellValue(colKDITEM), grvDetail.GetFocusedRowCellValue(colKDUOM))
            If grvDetail.GetFocusedRowCellValue(colPRICE) <> dsCek.PRICEPURCHASESTANDARD Then
                If MessageBox.Show("Harga Beli Tidak sama : " & FormatNumber(ds.PRICESALESSTANDARD) & vbCrLf & "Apakah harga akan diperbaiki?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                    Dim frmItem As New frmItem
                    Try
                        frmItem.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDITEM)
                        frmItem.ShowDialog(Me)
                    Catch oErr As Exception
                        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                End If
            End If

        ElseIf e.Column.Name = colSUBTOTAL.Name Or e.Column.Name = colDISCOUNT.Name Then
            Dim sGrandTotal As Decimal = CDec(grvDetail.GetFocusedRowCellValue(colSUBTOTAL)) - CDec(grvDetail.GetFocusedRowCellValue(colDISCOUNT))

            grvDetail.SetFocusedRowCellValue(colGRANDTOTAL, sGrandTotal)
        ElseIf e.Column.Name = colREMARKS.Name Then
            'Dim oItem As New Reference.clsItem
            'Dim ds = oItem.GetData(grvDetail.GetFocusedRowCellValue(colKDITEM))

            'If ds.M_ITEM_L1.MEMO = "HANDPHONE" Then
            '    Dim sCOUNT = grvDetail.GetFocusedRowCellValue(colREMARKS).ToString().Split(Environment.NewLine)

            '    If CDec(grvDetail.GetFocusedRowCellValue(colQTY)) < sCOUNT.Count Then

            '    End If
            'End If
        End If
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail.DeleteSelectedRows()
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmPurchaseInvoice_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClose_Click()
            Case Keys.F11
                btnHistory_Click()
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

    Private Sub btnHistory_Click() Handles btnHistory.ItemClick
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        frmBrowseItem_History_In.fn_LoadMe(0, grdKDVENDOR.EditValue)
        frmBrowseItem_History_In.ShowDialog(Me)
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
    Private Sub grdKDVENDOR_Validated() Handles grdKDVENDOR.Validated
        Try
            Dim oVendor As New Reference.clsVendor
            Dim ds = oVendor.GetData(grdKDVENDOR.EditValue)

            deDATEDUE.DateTime = deDATE.DateTime.AddDays(ds.DUE)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub fn_LoadKDWAREHOUSE()
        Dim oWAREHOUSE As New Reference.clsWarehouse
        Try
            grdKDWAREHOUSE.Properties.DataSource = oWAREHOUSE.GetData.Where(Function(x) x.ISACTIVE = True And x.MEMO = "GUDANG OBAT").ToList()
            grdKDWAREHOUSE.Properties.ValueMember = "KDWAREHOUSE"
            grdKDWAREHOUSE.Properties.DisplayMember = "NAME_DISPLAY"

            Dim dsFirst = oWAREHOUSE.GetData().FirstOrDefault(Function(x) x.MEMO = "GUDANG OBAT")
            If dsFirst IsNot Nothing Then
                grdKDWAREHOUSE.Text = dsFirst.KDWAREHOUSE
            End If

            'grdKDWAREHOUSE.Text = oWAREHOUSE.GetData().Where(Function(x) x.ISACTIVE = True).FirstOrDefault().KDWAREHOUSE
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
        'Dim oITEM As New Reference.clsItem
        'Try
        '    grdKDITEM.DataSource = oITEM.GetData.Where(Function(x) x.ISACTIVE = True And x.ISSTOK = True).ToList()
        '    grdKDITEM.ValueMember = "KDITEM"
        '    grdKDITEM.DisplayMember = "NMITEM2"
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try

        If grdKDWAREHOUSE.Text = String.Empty Then Exit Sub

        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "A.KDITEM "
            SQL &= ",A.NMITEM1 "
            SQL &= ",A.NMITEM2"
            SQL &= ",A.NMITEM3 "
            SQL &= ",STOK = ISNULL((SELECT AA.AMOUNT FROM M_ITEM_WAREHOUSE AA INNER JOIN M_ITEM_UOM BB ON AA.KDITEM = BB.KDITEM WHERE A.KDITEM = AA.KDITEM AND AA.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' AND BB.RATE =1), 0 ) "
            SQL &= "FROM "
            SQL &= "M_ITEM A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "
            SQL &= "AND A.ISSTOK = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ITEM")

            grdKDITEM.DataSource = ds.Tables("ITEM")
            grdKDITEM.ValueMember = "KDITEM"
            grdKDITEM.DisplayMember = "NMITEM2"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
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

    Private Sub PrintBarcodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintBarcodeToolStripMenuItem.Click
        btnSaveClose_Click()

        Dim frmExport As New frmExport

        frmExport.fn_LoadMe(txtKDPI.Text.Trim.ToUpper)
        frmExport.ShowDialog()
    End Sub

    Private Sub grdKDWAREHOUSE_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDWAREHOUSE.EditValueChanged
        fn_LoadKDITEM()
    End Sub
End Class