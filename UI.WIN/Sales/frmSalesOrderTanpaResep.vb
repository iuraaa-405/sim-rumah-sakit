Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmSalesOrderTanpaResep
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oSalesOrderTanpaResep As New Sales.clsSalesOrderTanpaResep
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
            Me.Text = "Penjualan"

            lNAMAPASIEN.Text = SalesOrderTanpaResep.KDPENDAFTARAN
            LNOMORTELEPON.Text = SalesOrderTanpaResep.NOMORTELEPON
            lKDSOTANPARESEP.Text = SalesOrderTanpaResep.KDSOTANPARESEP
            lDATE.Text = SalesOrderTanpaResep.TANGGAL
            lSUBTOTAL.Text = SalesOrderTanpaResep.SUBTOTAL
            lDISCOUNT.Text = SalesOrderTanpaResep.DISCOUNT
            lTAX.Text = "Admin Racik"
            lGRANDTOTAL.Text = SalesOrderTanpaResep.GRANDTOTAL
            lKDWAREHOUSE.Text = Caption.ReferenceWarehouse

            tab1.Text = SalesOrderTanpaResep.TAB_DETAIL
            tab2.Text = SalesOrderTanpaResep.TAB_MEMO

            grvDetail.Columns("KDITEM").Caption = SalesOrderTanpaResep.DETAIL_KDITEM
            grvDetail.Columns("KDUOM").Caption = SalesOrderTanpaResep.DETAIL_KDUOM
            grvDetail.Columns("QTY").Caption = SalesOrderTanpaResep.DETAIL_QTY
            grvDetail.Columns("PRICE").Caption = SalesOrderTanpaResep.DETAIL_PRICE
            grvDetail.Columns("SUBTOTAL").Caption = SalesOrderTanpaResep.DETAIL_SUBTOTAL
            grvDetail.Columns("DISCOUNT").Caption = "Tuslah"
            grvDetail.Columns("GRANDTOTAL").Caption = SalesOrderTanpaResep.DETAIL_GRANDTOTAL
            grvDetail.Columns("REMARKS").Caption = SalesOrderTanpaResep.DETAIL_REMARKS

            grvKDITEM.Columns("NMITEM2").Caption = Item.NMITEM2_1
            grvKDUOM.Columns("MEMO").Caption = UOM.MEMO

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDSOTRANPARESEP.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDITEM()
        fn_LoadKDUOM()
        fn_LoadKDSIGNA()
        fn_LoadKDWAREHOUSE()

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
        txtNAMAPASIEN.Properties.ReadOnly = Status
        txtNOMORTELEPON.Properties.ReadOnly = Status
        txtMEMO.Properties.ReadOnly = True
        txtSUBTOTAL.Properties.ReadOnly = Status
        txtDISCOUNT.Properties.ReadOnly = Status
        txtTAX.Properties.ReadOnly = Status
        txtGRANDTOTAL.Properties.ReadOnly = Status

        grvDetail.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDSOTRANPARESEP.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        txtNAMAPASIEN.ResetText()
        txtNOMORTELEPON.ResetText()

        txtMEMO.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oSalesOrderTanpaResep.GetData(sNoId)

            With ds
                txtKDSOTRANPARESEP.Text = .KDSOTANPARESEP
                deDATE.DateTime = .DATE
                txtNAMAPASIEN.Text = .NAMAPASIEN
                txtNOMORTELEPON.Text = .NOMORTELEPON
                grdKDWAREHOUSE.Text = .KDWAREHOUSE
                txtSUBTOTAL.Text = .SUBTOTAL
                txtDISCOUNT.Text = .DISCOUNT
                txtTAX.Text = .TAX
                txtGRANDTOTAL.Text = .GRANDTOTAL
                txtMEMO.Text = .MEMO
                txtTUSLAH.Text = .TUSLAH
                bindingSource.DataSource = oSalesOrderTanpaResep.GetDataDetail.Where(Function(x) x.KDSOTANPARESEP = sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = bindingSource
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If txtNAMAPASIEN.Text = String.Empty Then
                txtNAMAPASIEN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtNAMAPASIEN.ErrorText = Statement.ErrorRequired

                txtNAMAPASIEN.Focus()
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

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub fn_CetakKwitansi(ByVal sTANPARESEP As String)
        Try

            Dim oPendaftaran As New Admission.clsPendaftaran
            Dim oPulang As New Admission.clsUpdate_Tanggal_Pulang
            Dim oCashin As New Finance.clsCashIn

            Dim dsCashin = oSalesOrderTanpaResep.GetData(sTANPARESEP)

            sPrintGrandTotal = dsCashin.GRANDTOTAL


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

            SQL = " SELECT  "
            SQL &= " CATEGORY = 0 "
            SQL &= " ,A.KDSOTANPARESEP "
            SQL &= " ,KDPENDAFTARAN = '-' "
            SQL &= " ,TUJUAN = 'APOTEK' "
            SQL &= " ,DPJP = '-' "
            SQL &= " ,KDCUSTOMER = '' "
            SQL &= " ,PASIEN = A.NAMAPASIEN  "
            SQL &= " ,ALAMAT = A.NOMORTELEPON "
            SQL &= " ,KELAS = '-'  "
            SQL &= " ,TANGGAL_DATANG = A.DATE "
            SQL &= " ,TANGGAL_PULANG = DATE "
            SQL &= " ,A.SUBTOTAL "
            SQL &= " ,COSTSHARE = 0 "
            SQL &= " ,DEPOSIT = 0 "
            SQL &= " ,A.DISCOUNT "
            SQL &= " ,A.TAX "
            SQL &= " ,A.GRANDTOTAL "
            SQL &= " ,ITEM_GROUP = F.MEMO "
            SQL &= " ,ITEM = E.NMITEM2 "
            SQL &= " ,QTY = SUM(B.QTY) "
            SQL &= " ,GRANDTOTAL_DETAIL = SUM(B.GRANDTOTAL)  "
            SQL &= " FROM S_SO_TANPARESEP_H A  "
            SQL &= " INNER JOIN S_SO_TANPARESEP_D B "
            SQL &= " ON A.KDSOTANPARESEP = B.KDSOTANPARESEP "
            SQL &= " INNER JOIN M_ITEM E "
            SQL &= " ON B.KDITEM = E.KDITEM "
            SQL &= " INNER JOIN M_ITEM_L2 F "
            SQL &= " ON E.KDITEM_L2 = F.KDITEM_L2  "
            SQL &= " WHERE "
            SQL &= " A.KDSOTANPARESEP = '" & sTANPARESEP & "' "
            SQL &= "GROUP BY "
            SQL &= " A.KDSOTANPARESEP "
            SQL &= " ,A.SUBTOTAL "
            SQL &= " ,A.DISCOUNT "
            SQL &= " ,A.TAX "
            SQL &= " ,A.GRANDTOTAL "
            SQL &= " ,F.MEMO "
            SQL &= " ,E.NMITEM2 "
            SQL &= " ,A.NAMAPASIEN  "
            SQL &= " ,A.NOMORTELEPON "
            SQL &= " ,A.DATE "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

            Dim listTranskasi As New List(Of R_CASHIN)

            For iLoop As Integer = 0 To ds.Tables("ALL").Rows.Count - 1
                Dim dsRekap As New DataAccess.R_CASHIN

                With ds.Tables("ALL")
                    dsRekap.KDCASHIN = .Rows(iLoop)("KDSOTANPARESEP")
                    dsRekap.CATEGORY = .Rows(iLoop)("CATEGORY")
                    dsRekap.KDPENDAFTARAN = .Rows(iLoop)("KDPENDAFTARAN")
                    dsRekap.KDCUSTOMER = .Rows(iLoop)("KDCUSTOMER")
                    dsRekap.TUJUAN = .Rows(iLoop)("TUJUAN")
                    dsRekap.DPJP = .Rows(iLoop)("DPJP")
                    dsRekap.PASIEN = .Rows(iLoop)("PASIEN")
                    dsRekap.ALAMAT = .Rows(iLoop)("ALAMAT")
                    dsRekap.KELAS = .Rows(iLoop)("KELAS")
                    dsRekap.TANGGAL_DATANG = .Rows(iLoop)("TANGGAL_DATANG")
                    dsRekap.TANGGAL_PULANG = .Rows(iLoop)("TANGGAL_PULANG")
                    dsRekap.NOINVOICE = ""
                    dsRekap.SUBTOTAL = .Rows(iLoop)("SUBTOTAL")
                    dsRekap.COSTSHARE = .Rows(iLoop)("COSTSHARE")
                    dsRekap.DEPOSIT = .Rows(iLoop)("DEPOSIT")
                    dsRekap.ADMIN = .Rows(iLoop)("DISCOUNT")
                    dsRekap.ROUND = .Rows(iLoop)("TAX")
                    dsRekap.GRANDTOTAL = .Rows(iLoop)("GRANDTOTAL")
                    dsRekap.ITEM_GROUP = .Rows(iLoop)("ITEM_GROUP")
                    dsRekap.ITEM = .Rows(iLoop)("ITEM")
                    dsRekap.ITEM_GROUP = .Rows(iLoop)("ITEM_GROUP")
                    dsRekap.QTY = .Rows(iLoop)("QTY")
                    dsRekap.GRANDTOTAL_DETAIL = .Rows(iLoop)("GRANDTOTAL_DETAIL")

                    listTranskasi.Add(dsRekap)
                End With
            Next

            Dim rpt As New xtraCashIn

            rpt.ShowPrintMarginsWarning = False
            rpt.Watermark.Text = sWATERMARK

            rpt.bindingSource.DataSource = listTranskasi
            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
            printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oSalesOrderTanpaResep.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oSalesOrderTanpaResep.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDSOTANPARESEP = sNoId
                .DATE = deDATE.DateTime
                .NAMAPASIEN = txtNAMAPASIEN.Text.ToString.Trim.ToUpper
                .NOMORTELEPON = txtNOMORTELEPON.Text.ToString.Trim.ToUpper
                .KDWAREHOUSE = IIf(String.IsNullOrEmpty(grdKDWAREHOUSE.EditValue), String.Empty, grdKDWAREHOUSE.EditValue)
                .SUBTOTAL = CDec(txtSUBTOTAL.Text)
                .DISCOUNT = CDec(txtDISCOUNT.Text)
                .TAX = CDec(txtTAX.Text)
                .GRANDTOTAL = CDec(txtGRANDTOTAL.Text)
                Try
                    .PAYAMOUNT = oSalesOrderTanpaResep.GetData(sNoId).PAYAMOUNT
                Catch oErr As Exception
                    .PAYAMOUNT = CDec(0)
                End Try
                .MEMO = txtMEMO.Text.Trim.ToUpper
                .KDUSER = sUserID
                .KDSHIFT = sSHIFT
                .TUSLAH = CDec(txtTUSLAH.Text)
            End With

            ' ***** DETIL *****
            Dim arrDetail = oSalesOrderTanpaResep.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oSalesOrderTanpaResep.GetStructureDetail
                With dsDetail
                    Try
                        .DATECREATED = oSalesOrderTanpaResep.GetData(sNoId).DATECREATED
                    Catch oErr As Exception
                        .DATECREATED = Now
                    End Try
                    .DATEUPDATED = Now
                    .SEQ = i
                    .KDSOTANPARESEP = ds.KDSOTANPARESEP
                    .KDITEM = grvDetail.GetRowCellValue(i, colKDITEM)
                    .KDUOM = grvDetail.GetRowCellValue(i, colKDUOM)
                    .KDSIGNA = grvDetail.GetRowCellValue(i, colKDSIGNA)
                    .QTY = CDec(grvDetail.GetRowCellValue(i, colQTY))
                    .ISRACIK = CBool(grvDetail.GetRowCellValue(i, colISRACIK))
                    .PRICE = CDec(grvDetail.GetRowCellValue(i, colPRICE))
                    .DISCOUNT = CDec(grvDetail.GetRowCellValue(i, colDISCOUNT))
                    .SUBTOTAL = CDec(grvDetail.GetRowCellValue(i, colSUBTOTAL))
                    .GRANDTOTAL = CDec(grvDetail.GetRowCellValue(i, colGRANDTOTAL))
                    .REMARKS = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colREMARKS)), "-", grvDetail.GetRowCellValue(i, colREMARKS))
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    Dim sKDSOTANPARESEP = oSalesOrderTanpaResep.InsertData(ds, arrDetail)
                    If sKDSOTANPARESEP = "" Then
                        fn_Save = fn_Save = False
                    Else
                        fn_Save = True

                        txtKDSOTRANPARESEP.Text = sKDSOTANPARESEP

                        fn_CetakKwitansi(txtKDSOTRANPARESEP.Text.ToString)
                    End If

                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oSalesOrderTanpaResep.UpdateData(ds, arrDetail)
                    fn_CetakKwitansi(txtKDSOTRANPARESEP.Text.ToString)
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
        Dim sTuslah = 0
        For i As Integer = 0 To grvDetail.RowCount - 2
            sSubTotal += CDec(grvDetail.GetRowCellValue(i, colGRANDTOTAL))
            sTuslah += CDec(grvDetail.GetRowCellValue(i, colDISCOUNT))
        Next

        txtSUBTOTAL.Text = sSubTotal
        txtTUSLAH.Text = sTuslah
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
                        grvDetail.SetFocusedRowCellValue(colQTY, 1)
                        grvDetail.SetFocusedRowCellValue(colKDSIGNA, oItem.DefaultItem_Signa)
                        grvDetail.SetFocusedRowCellValue(colISRACIK, False)
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
                        'grvDetail.SetFocusedRowCellValue(colPRICE, ds.PRICEPURCHASESTANDARD)
                        'If CDec(grvDetail.GetFocusedRowCellValue(colPRICE)) > 0 Then
                        '    Dim sPricePPN = CDec(grvDetail.GetFocusedRowCellValue(colPRICE)) * (10 / 100)
                        '    Dim sPriceSetelahPPN = sPricePPN + CDec(grvDetail.GetFocusedRowCellValue(colPRICE))
                        '    Dim sPriceTermin = buletin(CDec(sPriceSetelahPPN * (ds.MARGIN / 100)) + CDec(sPriceSetelahPPN), 100)

                        '    grvDetail.SetFocusedRowCellValue(colPRICE, sPriceTermin)
                        'Else
                        '    grvDetail.SetFocusedRowCellValue(colPRICE, 0)
                        'End If
                        Dim bpjs As Boolean = False

                        If sHargaApotik = True Then
                            If bpjs = False Then
                                grvDetail.SetFocusedRowCellValue(colPRICE, ds.PRICESALESSTANDARD)
                            Else
                                grvDetail.SetFocusedRowCellValue(colPRICE, ds.PRICESALESTERMIN)
                            End If
                        Else
                            grvDetail.SetFocusedRowCellValue(colPRICE, ds.PRICEPURCHASESTANDARD)

                            If CDec(grvDetail.GetFocusedRowCellValue(colPRICE)) > 0 Then
                                Dim sPriceSetelahPPN = 0 + CDec(grvDetail.GetFocusedRowCellValue(colPRICE))
                                Dim sPriceTermin = buletin(CDec(sPriceSetelahPPN * (IIf(bpjs = False, (ds.MARGIN / 100), (ds.PRICESALESTERMIN / 100))) + CDec(sPriceSetelahPPN)), 100)

                                grvDetail.SetFocusedRowCellValue(colPRICE, sPriceTermin)
                            Else
                                grvDetail.SetFocusedRowCellValue(colPRICE, 0)
                            End If
                        End If
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
        ElseIf e.Column.Name = colISRACIK.Name Then
            If CBool(grvDetail.GetFocusedRowCellValue(colISRACIK)) = True Then
                grvDetail.SetFocusedRowCellValue(colDISCOUNT, 500)
            Else
                grvDetail.SetFocusedRowCellValue(colDISCOUNT, 0)
            End If
        ElseIf e.Column.Name = colQTY.Name Or e.Column.Name = colPRICE.Name Then
            Dim sSubTotal As Decimal = CDec(grvDetail.GetFocusedRowCellValue(colQTY)) * CDec(grvDetail.GetFocusedRowCellValue(colPRICE))

            grvDetail.SetFocusedRowCellValue(colSUBTOTAL, sSubTotal)

        ElseIf e.Column.Name = colSUBTOTAL.Name Or e.Column.Name = colDISCOUNT.Name Then
            Dim sGrandTotal As Decimal = CDec(grvDetail.GetFocusedRowCellValue(colSUBTOTAL)) + CDec(grvDetail.GetFocusedRowCellValue(colDISCOUNT))

            grvDetail.SetFocusedRowCellValue(colGRANDTOTAL, sGrandTotal)
        End If
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail.DeleteSelectedRows()
    End Sub
    Private Function buletin(ByVal Number As Double, Optional ByVal Range As Integer = 10) As Decimal

        buletin = Math.Round(Number / Range, 0) * Range

    End Function
#End Region
#Region "Command Button"
    Private Sub frmSalesOrder_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    Private Sub fn_LoadKDITEM()
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
            SQL &= ",A.NMITEM2 "
            SQL &= ",STOK = B.AMOUNT "
            SQL &= "FROM "
            SQL &= "M_ITEM A "
            SQL &= "INNER JOIN M_ITEM_WAREHOUSE B "
            SQL &= "ON A.KDITEM = B.KDITEM "
            SQL &= "INNER JOIN M_ITEM_UOM C "
            SQL &= "ON A.KDITEM = C.KDITEM AND B.KDITEM = C.KDITEM "
            SQL &= "WHERE C.RATE = 1 "
            SQL &= "AND A.ISACTIVE = 1 "
            SQL &= "AND A.ISSTOK = 1 "
            SQL &= "AND B.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "

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
    Private Sub fn_LoadKDSIGNA()
        Dim oSigna As New Reference.clsSigna
        Try
            grdKDSIGNA.DataSource = oSigna.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDSIGNA.ValueMember = "KDSIGNA"
            grdKDSIGNA.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDWAREHOUSE()
        Dim oWarehouse As New Reference.clsWarehouse

        Try
            Dim dsWarehouse = From x In oWarehouse.GetData
                              Join y In oWarehouse.GetDataWarehouseUser
                              On x.KDWAREHOUSE Equals y.KDWAREHOUSE
                              Where y.KDUSER = sUserID And x.ISACTIVE = True
                              Select x.KDWAREHOUSE, x.NAME_DISPLAY, y.ISDEFAULT

            grdKDWAREHOUSE.Properties.DataSource = dsWarehouse.ToList()
            grdKDWAREHOUSE.Properties.ValueMember = "KDWAREHOUSE"
            grdKDWAREHOUSE.Properties.DisplayMember = "NAME_DISPLAY"

            Dim dsFisrt = dsWarehouse.FirstOrDefault(Function(x) x.ISDEFAULT = True)
            If dsFisrt IsNot Nothing Then
                grdKDWAREHOUSE.EditValue = dsFisrt.KDWAREHOUSE
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDWAREHOUSE_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDWAREHOUSE.EditValueChanged
        fn_LoadKDITEM()
    End Sub
#End Region
End Class