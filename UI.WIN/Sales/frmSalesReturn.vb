Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmSalesReturn
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oSalesReturn As New Sales.clsSalesReturn
    Private sPopUP As Boolean = False
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
            Me.Text = SalesReturn.TITLE

            lKDKUNJUNGAN.Text = SalesOrderTransaksi.KDKUNJUNGAN & " *"
            lKDSR.Text = SalesReturn.KDSR
            lDATE.Text = SalesReturn.TANGGAL
            'lKDKUNJUNGAN.Text = SalesReturn.KDKUNJUNGAN & " *"
            lKDWAREHOUSE.Text = SalesReturn.KDWAREHOUSE & " *"

            lSUBTOTAL.Text = SalesReturn.SUBTOTAL
            lDISCOUNT.Text = SalesReturn.DISCOUNT
            lTAX.Text = SalesReturn.TAX
            lGRANDTOTAL.Text = SalesReturn.GRANDTOTAL

            tab1.Text = SalesReturn.TAB_DETAIL
            tab2.Text = SalesReturn.TAB_MEMO

            grvDetail.Columns("KDITEM").Caption = SalesReturn.DETAIL_KDITEM
            grvDetail.Columns("KDUOM").Caption = SalesReturn.DETAIL_KDUOM
            grvDetail.Columns("QTY").Caption = SalesReturn.DETAIL_QTY
            grvDetail.Columns("PRICE").Caption = PurchaseInvoice.DETAIL_PRICE
            grvDetail.Columns("SUBTOTAL").Caption = PurchaseInvoice.DETAIL_SUBTOTAL
            grvDetail.Columns("DISCOUNT").Caption = "Potongan"
            grvDetail.Columns("GRANDTOTAL").Caption = PurchaseInvoice.DETAIL_GRANDTOTAL
            grvDetail.Columns("REMARKS").Caption = SalesReturn.DETAIL_REMARKS

            'grvKDKUNJUNGAN.Columns("NAME_DISPLAY").Caption = Customer.NAME_DISPLAY
            grvKDWAREHOUSE.Columns("NAME_DISPLAY").Caption = Warehouse.NAME_DISPLAY

            'grvKDITEM.Columns("NMITEM1").Caption = Item.NMITEM1_2
            grvKDITEM.Columns("NMITEM2").Caption = Item.NMITEM2_2
            'grvKDITEM.Columns("NMITEM3").Caption = Item.NMITEM3

            grvKDUOM.Columns("MEMO").Caption = UOM.MEMO

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDSR.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        'fn_LoadKDCUSTOMER()
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
        grdKDKUNJUNGAN.Properties.ReadOnly = Status
        grdKDWAREHOUSE.Properties.ReadOnly = Status
        txtDISCOUNT.Properties.ReadOnly = Status
        txtTAX.Properties.ReadOnly = Status

        txtMEMO.Properties.ReadOnly = Status

        txtKDSOTRANSAKSI.Properties.ReadOnly = True
        cboPersen.Properties.ReadOnly = Status
        grvDetail.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDSR.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        grdKDKUNJUNGAN.ResetText()
        'grdKDWAREHOUSE.ResetText()
        txtMEMO.ResetText()
        txtSUBTOTAL.ResetText()
        txtDISCOUNT.ResetText()
        txtTAX.ResetText()
        txtGRANDTOTAL.ResetText()
        txtKDSOTRANSAKSI.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oSalesReturn.GetData(sNoId)

            With ds
                txtKDSR.Text = .KDSR
                deDATE.DateTime = .DATE

                fn_LoadKDKUNJUNGAN(.KDKUNJUNGAN, 3)
                grdKDKUNJUNGAN.Text = .KDKUNJUNGAN

                grdKDKUNJUNGAN.Text = .KDKUNJUNGAN
                grdKDWAREHOUSE.Text = .KDWAREHOUSE
                txtMEMO.Text = .MEMO
                txtSUBTOTAL.Text = .SUBTOTAL
                txtDISCOUNT.Text = .DISCOUNT
                txtTAX.Text = .TAX
                txtGRANDTOTAL.Text = .GRANDTOTAL
                cboPersen.Text = .PERSEN
                txtKDSOTRANSAKSI.Text = .KDSOTRANSAKSI

                bindingSource.DataSource = oSalesReturn.GetDataDetail.Where(Function(x) x.KDSR = sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = bindingSource
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If grdKDKUNJUNGAN.Text = String.Empty Then
                grdKDKUNJUNGAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDKUNJUNGAN.ErrorText = Statement.ErrorRequired

                grdKDKUNJUNGAN.Focus()
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
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oSalesReturn.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oSalesReturn.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDSR = sNoId
                .DATE = deDATE.DateTime
                .KDKUNJUNGAN = IIf(String.IsNullOrEmpty(grdKDKUNJUNGAN.EditValue), String.Empty, grdKDKUNJUNGAN.EditValue)
                .KDWAREHOUSE = IIf(String.IsNullOrEmpty(grdKDWAREHOUSE.EditValue), String.Empty, grdKDWAREHOUSE.EditValue)
                .MEMO = txtMEMO.Text.Trim.ToUpper
                .SUBTOTAL = CDec(txtSUBTOTAL.Text)
                .DISCOUNT = CDec(txtDISCOUNT.Text)
                .TAX = CDec(txtTAX.Text)
                .GRANDTOTAL = CDec(txtGRANDTOTAL.Text)
                Try
                    .PAYAMOUNT = oSalesReturn.GetData(sNoId).PAYAMOUNT
                Catch oErr As Exception
                    .PAYAMOUNT = 0
                End Try
                .KDUSER = sUserID
                .PERSEN = cboPersen.Text
                .KDSOTRANSAKSI = txtKDSOTRANSAKSI.Text
            End With

            ' ***** DETIL *****
            Dim arrDetail = oSalesReturn.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oSalesReturn.GetStructureDetail
                With dsDetail
                    Try
                        .DATECREATED = oSalesReturn.GetData(sNoId).DATECREATED
                    Catch oErr As Exception
                        .DATECREATED = Now
                    End Try
                    .DATEUPDATED = Now

                    .SEQ = i
                    .KDSR = ds.KDSR
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
                    fn_Save = oSalesReturn.InsertData(ds, arrDetail)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oSalesReturn.UpdateData(ds, arrDetail)
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
    Private Function buletin(ByVal Number As Double, Optional ByVal Range As Integer = 10) As Decimal
        buletin = Math.Round(Number / Range, 0) * Range
    End Function
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
                            grvDetail.SetFocusedRowCellValue(colPRICE, ds.PRICEPURCHASESTANDARD)

                            Dim isBPJS As Boolean = False
                            Dim oKunjungan As New Admission.clsPendaftaran
                            Dim dsKunjungan = oKunjungan.GetDataKunjungan(grdKDKUNJUNGAN.EditValue)

                            If dsKunjungan.S_PENDAFTARAN_H.M_DAFTAR_L1.MEMO = "BPJS" Then
                                isBPJS = True
                            Else
                                isBPJS = False
                            End If

                            If CDec(grvDetail.GetFocusedRowCellValue(colPRICE)) > 0 Then
                                Dim sPricePPN = CDec(grvDetail.GetFocusedRowCellValue(colPRICE)) * (10 / 100)
                                Dim sPriceSetelahPPN = sPricePPN + CDec(grvDetail.GetFocusedRowCellValue(colPRICE))
                                Dim sPriceTermin = buletin(CDec(sPriceSetelahPPN * IIf(isBPJS = False, (ds.MARGIN / 100), (ds.PRICESALESTERMIN / 100))) + CDec(sPriceSetelahPPN), 100)

                                grvDetail.SetFocusedRowCellValue(colPRICE, sPriceTermin)
                            Else
                                grvDetail.SetFocusedRowCellValue(colPRICE, 0)
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
            If CDec(grvDetail.GetFocusedRowCellValue(colQTY)) < 1 Then
                grvDetail.SetFocusedRowCellValue(colQTY, 1)
            End If

            Dim sSubTotal As Decimal = CDec(grvDetail.GetFocusedRowCellValue(colQTY)) * CDec(grvDetail.GetFocusedRowCellValue(colPRICE))

            grvDetail.SetFocusedRowCellValue(colSUBTOTAL, sSubTotal)
            grvDetail.SetFocusedRowCellValue(colDISCOUNT, sSubTotal * (cboPersen.Text / 100))
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
    Private Sub frmSalesReturn_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    Private Sub fn_LoadKDCUSTOMER()
        Dim oCUSTOMER As New Reference.clsCustomer
        Try
            grdKDKUNJUNGAN.Properties.DataSource = oCUSTOMER.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDKUNJUNGAN.Properties.ValueMember = "KDCUSTOMER"
            grdKDKUNJUNGAN.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDCUSTOMER_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Delete Then
            grdKDKUNJUNGAN.ResetText()
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
        Dim oITEM As New Reference.clsItem
        Try
            grdKDITEM.DataSource = oITEM.GetData.Where(Function(x) x.ISACTIVE = True And x.ISSTOK = True).ToList()
            grdKDITEM.ValueMember = "KDITEM"
            grdKDITEM.DisplayMember = "NMITEM2"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDUOM()
        Dim oUOM As New Reference.clsUOM
        Try
            grdKDUOM.DataSource = oUOM.GetData.Where(Function(x) x.ISACTIVE = True And x.KDKELASRAWAT = "").ToList()
            grdKDUOM.ValueMember = "KDUOM"
            grdKDUOM.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub txtCARI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCARI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            sPopUP = True
            If txtCARI.Text = String.Empty Then Exit Sub
            fn_LoadKDKUNJUNGAN(IIf(cboCARI.SelectedIndex = 0, txtCARI.Text.ToString.Trim.PadLeft(6, "0"), txtCARI.Text.ToString.Trim), cboCARI.SelectedIndex)
        End If
    End Sub
    Private Sub fn_LoadKDKUNJUNGAN(ByVal sParameter As String, ByVal sCari As Integer)
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
            SQL &= "* "
            SQL &= "FROM ( "
            SQL &= "SELECT "
            SQL &= "A.KDKUNJUNGAN "
            SQL &= ",KATEGORI = (SELECT CASE D.CATEGORY WHEN 0 THEN 'RJ' ELSE 'RI' END) "
            SQL &= ",D.KDCUSTOMER "
            SQL &= ",JENISDAFTAR = F.MEMO "
            SQL &= ",PASIEN = G.GOL "
            SQL &= ",A.KDPENDAFTARAN "
            SQL &= ",E.NAME_DISPLAY "
            SQL &= ",A.DATE "
            SQL &= ",TUJUAN = B.NAME_DISPLAY  "
            SQL &= ",DPJP = C.NAME_DISPLAY "
            SQL &= ",PULANG = ISNULL((SELECT CONVERT(BIT, 1) FROM T_UPDATE_TANGGAL_PULANG WHERE A.KDPENDAFTARAN = KDPENDAFTARAN AND CARAPULANG <> 5), ISNULL((SELECT CONVERT(BIT, 1) FROM T_UPDATE_TANGGAL_PULANG WHERE D.KDPENDAFTARAN_AWAL = KDPENDAFTARAN AND CARAPULANG <> 5), CONVERT(BIT, 0)))  "
            SQL &= ",RANAP = (SELECT CASE WHEN KDPENDAFTARAN_AWAL <> '' THEN CONVERT(BIT, 1) ELSE CONVERT(BIT, 0) END) "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_KUNJUNGAN A "
            SQL &= "INNER JOIN M_DEPARTMENT B "
            SQL &= "ON A.KDDEPARTMENT = B.KDDEPARTMENT "
            SQL &= "INNER JOIN M_DOCTOR C "
            SQL &= "ON A.KDDOCTOR = C.KDDOCTOR "
            SQL &= "INNER JOIN S_PENDAFTARAN_H D "
            SQL &= "ON A.KDPENDAFTARAN = D.KDPENDAFTARAN "
            SQL &= "INNER JOIN M_CUSTOMER E "
            SQL &= "ON D.KDCUSTOMER = E.KDCUSTOMER "
            SQL &= "INNER JOIN M_DAFTAR_L1 F "
            SQL &= "ON D.KDDAFTAR_L1 = F.KDDAFTAR_L1 "
            SQL &= "INNER JOIN M_KESATUAN G "
            SQL &= "ON A.KDKESATUAN = G.KDKESATUAN "
            If sCari = 0 Then
                SQL &= "WHERE D.KDCUSTOMER = '" & sParameter & "' "
            ElseIf sCari = 1 Then
                SQL &= "WHERE E.NAME_DISPLAY LIKE '%" & sParameter & "%' "
            ElseIf sCari = 2 Then
                SQL &= "WHERE A.KDPENDAFTARAN LIKE '%" & sParameter & "%' "
            Else
                SQL &= "WHERE A.KDKUNJUNGAN = '" & sParameter & "' "
            End If

            SQL &= ") Z "
            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                SQL &= "WHERE Z.PULANG = 0 AND Z.JENISDAFTAR = 'UMUM' "
            End If
            SQL &= "ORDER BY Z.DATE DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

            grdKDKUNJUNGAN.Properties.DataSource = ds.Tables("ALL")
            grdKDKUNJUNGAN.Properties.ValueMember = "KDKUNJUNGAN"
            grdKDKUNJUNGAN.Properties.DisplayMember = "KDKUNJUNGAN"

            If sPopUP = True Then
                grdKDKUNJUNGAN.ShowPopup()
            End If

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadNOINVOICE(ByVal Parameter As String)
        Dim oSalesTransaksi As New Sales.clsSalesOrderTransaksi

        Try
            Dim ds = From x In oSalesTransaksi.GetDataByKDkunjunganFarmasiList(Parameter)
                     Select x.KDSOTRANSAKSI, x.DATE, x.GRANDTOTAL, x.PAYAMOUNT

            grdKDSOTRANSAKSI.Properties.DataSource = ds.ToList
            grdKDSOTRANSAKSI.Properties.ValueMember = "KDSOTRANSAKSI"
            grdKDSOTRANSAKSI.Properties.DisplayMember = "KDSOTRANSAKSI"

            grdKDSOTRANSAKSI.ShowPopup()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadNOINVOICEGRID(ByVal Parameter As String)
        Dim oSalesTransaksi As New Sales.clsSalesOrderTransaksi

        Try
            Dim ds = From x In oSalesTransaksi.GetDataByKDkunjunganDetailList(Parameter)
                     Select x

            grvDetail.OptionsSelection.MultiSelect = True
            grvDetail.SelectAll()
            grvDetail.DeleteSelectedRows()
            grvDetail.OptionsSelection.MultiSelect = False

            For Each xloop In ds
                grvDetail.Focus()
                grvDetail.AddNewRow()
                grvDetail.SetFocusedRowCellValue(colKDITEM, xloop.KDITEM)
                grvDetail.SetFocusedRowCellValue(colQTY, xloop.QTY)
                grvDetail.SetFocusedRowCellValue(colPRICE, xloop.PRICE)
                grvDetail.SetFocusedRowCellValue(colSUBTOTAL, xloop.GRANDTOTAL)

                grvDetail.UpdateCurrentRow()
            Next

            'grdKDSOTRANSAKSI.ShowPopup()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDKUNJUNGAN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdKDKUNJUNGAN.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If grdKDKUNJUNGAN.Text = String.Empty Then Exit Sub
            fn_LoadNOINVOICE(grdKDKUNJUNGAN.EditValue)
        End If
    End Sub
    Private Sub grdKDSOTRANSAKSI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdKDSOTRANSAKSI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If grdKDSOTRANSAKSI.Text = String.Empty Then Exit Sub
            fn_LoadNOINVOICEGRID(grdKDSOTRANSAKSI.EditValue)
            txtKDSOTRANSAKSI.Text = grdKDSOTRANSAKSI.Text
        End If
    End Sub
#End Region
End Class