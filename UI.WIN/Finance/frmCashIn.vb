Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmCashIn
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oCashIn As New Finance.clsCashIn
    Private sPopUP As Boolean = False
    Private sRegister As String = String.Empty
    'Private sCategory As Integer = 10
#End Region
#Region "Function"
    'Public Sub LoadMeRegister(ByVal Register As String, ByVal Category As Integer)
    '    sCategory = Category
    '    sRegister = Register
    'End Sub
    Public Sub LoadMe(ByVal FormMode As Integer, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId
        sCostShare = False
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True

        txtCARI.Text = sREKAMMEDIS

    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = CashIn.TITLE

            lNAMAPASIEN.Text = Customer.TITLE
            lKDDEPARTMENT.Text = Department.TITLE
            lKDDOCTOR.Text = Doctor.TITLE

            lKDCASH.Text = CashIn.KDCASHIN
            lDATE.Text = CashIn.TANGGAL
            lKDPENDAFTARAN.Text = CashIn.KDPENDAFTARAN & " *"
            lKDPAYMENTTYPE.Text = CashIn.KDPAYMENTTYPE & " *"

            tab1.Text = CashIn.TAB_DETAIL
            tab2.Text = CashIn.TAB_RICNCIAN
            tab3.Text = CashIn.TAB_MEMO

            lSUBTOTAL.Text = CashIn.SUBTOTAL
            lADMIN.Text = "Pembulatan"
            lROUND.Text = "Admin Racik"
            lGRANDTOTAL.Text = CashIn.GRANDTOTAL
            lCOSTSHARING.Text = CashIn.COSTSHARE
            lDEPOSIT.Text = CashIn.DEPOSIT

            grvDetail.Columns("NOINVOICE").Caption = CashIn.DETAIL_NOINVOICE
            grvDetail.Columns("AMOUNTORIGINAL").Caption = CashIn.DETAIL_AMOUNTORIGINAL
            grvDetail.Columns("AMOUNTDUE").Caption = CashIn.DETAIL_AMOUNTDUE
            grvDetail.Columns("AMOUNTPAYMENT").Caption = CashIn.DETAIL_AMOUNTPAYMENT
            grvDetail.Columns("REMARKS").Caption = CashIn.DETAIL_REMARKS

            grvKDPAYMENTTYPE.Columns("MEMO").Caption = PaymentType.MEMO

            grvNOINVOICE.Columns("NOINVOICE").Caption = SalesInvoice.KDSI


            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDCASHIN.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDPAYMENTTYPE()
        fn_LoadKDDOCTOR()
        fn_LoadKDDEPARTMENT()
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
        grdKDPENDAFTARAN.Properties.ReadOnly = Status
        grdKDPAYMENTTYPE.Properties.ReadOnly = Status

        txtMEMO.Properties.ReadOnly = Status

        txtADMIN.Properties.ReadOnly = Status
        txtROUND.Properties.ReadOnly = True
        cboSHIFT.Properties.ReadOnly = Status

        'txtDEPOSIT.Properties.ReadOnly = Status

        grvDetail.OptionsBehavior.ReadOnly = Status

        grvDeatilRincian.OptionsBehavior.ReadOnly = True
        grvDetailItem.OptionsBehavior.ReadOnly = True
        grvDetailObat.OptionsBehavior.ReadOnly = True

    End Sub
    Private Sub fn_EmptyMe()
        txtKDCASHIN.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        grdKDPENDAFTARAN.ResetText()
        grdKDPAYMENTTYPE.ResetText()
        txtMEMO.ResetText()
        txtSUBTOTAL.ResetText()
        txtADMIN.ResetText()
        txtROUND.ResetText()
        txtGRANDTOTAL.ResetText()
        txtCOSTSHARING.ResetText()
        txtDEPOSIT.ResetText()
        cboSHIFT.ResetText()

        grdKDPAYMENTTYPE.Text = oCashIn.Daftar_Payment_Default

    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oCashIn.GetData(sNoId)

            With ds
                txtKDCASHIN.Text = .KDCASHIN
                deDATE.DateTime = .DATE
                fn_LoadKDKUNJUNGAN(.KDPENDAFTARAN, 3)
                grdKDPENDAFTARAN.Text = .KDPENDAFTARAN

                Dim oKunjungan As New Admission.clsPendaftaran
                Dim dsKunjungan = oKunjungan.GetData(grdKDPENDAFTARAN.EditValue)
                If dsKunjungan IsNot Nothing Then
                    txtNAMAPASIEN.Text = dsKunjungan.M_CUSTOMER.NAME_DISPLAY & " / " & dsKunjungan.KDCUSTOMER
                    grdKDDEPARTMENT_H.Text = dsKunjungan.KDDEPARTMENT
                    grdKDDOCTOR_H.Text = dsKunjungan.KDDOCTOR
                    txtKDPENDAFTARAN_AWAL.Text = dsKunjungan.KDPENDAFTARAN_AWAL
                Else
                    txtNAMAPASIEN.ResetText()
                    grdKDDEPARTMENT_H.ResetText()
                    grdKDDOCTOR_H.ResetText()
                    txtKDPENDAFTARAN_AWAL.ResetText()
                End If


                grdKDPAYMENTTYPE.Text = .KDPAYMENTTYPE
                txtMEMO.Text = .MEMO

                txtSUBTOTAL.Text = .SUBTOTAL
                txtADMIN.Text = .ADMIN
                txtROUND.Text = .ROUND
                txtGRANDTOTAL.Text = .GRANDTOTAL
                txtCOSTSHARING.Text = .COSTSHARE
                txtDEPOSIT.Text = .DEPOSIT

                cboSHIFT.Text = .KDSHIFT

                bindingSource.DataSource = oCashIn.GetDataDetail.Where(Function(x) x.KDCASHIN = sNoId And x.SEQ < 100).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = bindingSource

                tabControl.SelectedTabPage = tab7

                bindingSource_R.DataSource = oCashIn.GetDataDetail.Where(Function(x) x.KDCASHIN = sNoId And x.SEQ >= 100).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail_R.DataSource = bindingSource_R

                tabControl.SelectedTabPage = tab1
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If grdKDPENDAFTARAN.Text = String.Empty Then
                grdKDPENDAFTARAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDPENDAFTARAN.ErrorText = Statement.ErrorRequired

                grdKDPENDAFTARAN.Focus()
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
            If cboSHIFT.Text = String.Empty Then
                cboSHIFT.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                cboSHIFT.ErrorText = Statement.ErrorRequired

                cboSHIFT.Focus()
                fn_Validate = False
                Exit Function
            End If

            grvDetail.UpdateCurrentRow()

            If grvDetail.RowCount < 2 Then
                MsgBox(Statement.ErrorDetail, MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End If

            grvDetail.UpdateCurrentRow()

            Dim sSubTotal As Decimal = 0
            Dim sRetur As Decimal = 0
            Dim oSalesOrder As New Sales.clsSalesOrderTransaksi
            Dim oDaftar As New Admission.clsPendaftaran
            Dim sTotal As Integer = 0

            For i As Integer = 0 To grvDetail.RowCount - 2
                sSubTotal += CDec(grvDetail.GetRowCellValue(i, colAMOUNTPAYMENT))

                Dim dsDaftar = oDaftar.GetData(grdKDPENDAFTARAN.EditValue)

                Dim dsSalesOrder = oSalesOrder.GetData(grvDetail.GetRowCellValue(i, colNOINVOICE))

                If dsSalesOrder IsNot Nothing Then
                    If dsSalesOrder.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.KDCUSTOMER <> dsDaftar.KDCUSTOMER Then
                        sTotal += 1
                    End If
                End If
            Next

            For i As Integer = 0 To grvDetail_R.RowCount - 2
                sRetur = CDec(grvDetail_R.GetRowCellValue(i, colAMOUNTPAYMENT_R))
            Next
            If sSubTotal - sRetur - CDec(txtADMIN.Text) + CDec(txtROUND.Text) - CDec(txtDEPOSIT.Text) - CDec(txtCOSTSHARING.Text) <> CDec(txtSUBTOTAL.Text) - CDec(txtADMIN.Text) + CDec(txtROUND.Text) - CDec(txtDEPOSIT.Text) - CDec(txtCOSTSHARING.Text) - sRetur Then
                MsgBox("Subtotal tidak sama dengan yang dibayarkan", MsgBoxStyle.Exclamation, Me.Text)

                fn_Validate = False
                Exit Function
            End If

            If sTotal <> 0 Then
                MsgBox("Silahkan Load ulang no Transaksi", MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End If

        Catch oErr As Exception
            fn_Validate = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oCashIn.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oCashIn.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDCASHIN = sNoId
                Try
                    .CATEGORY = oCashIn.GetData(sNoId).CATEGORY
                Catch oErr As Exception
                    .CATEGORY = 1
                End Try
                .DATE = deDATE.DateTime
                .KDPENDAFTARAN = IIf(String.IsNullOrEmpty(grdKDPENDAFTARAN.EditValue), String.Empty, grdKDPENDAFTARAN.EditValue)
                .KDPAYMENTTYPE = IIf(String.IsNullOrEmpty(grdKDPAYMENTTYPE.EditValue), String.Empty, grdKDPAYMENTTYPE.EditValue)
                .MEMO = "INVOICE NO. : "
                .SUBTOTAL = CDec(txtSUBTOTAL.Text)
                .ADMIN = CDec(txtADMIN.Text)
                .ROUND = CDec(txtROUND.Text)
                .COSTSHARE = CDec(txtCOSTSHARING.Text)
                .DEPOSIT = CDec(txtDEPOSIT.Text)
                .GRANDTOTAL = CDec(txtGRANDTOTAL.Text)
                Try
                    .ISSETOR = oCashIn.GetData(sNoId).ISSETOR
                Catch oErr As Exception
                    .ISSETOR = False
                End Try
                .KDUSER = sUserID
                .KDSHIFT = cboSHIFT.Text
            End With

            ' ***** DETIL *****
            Dim arrDetail = oCashIn.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oCashIn.GetStructureDetail
                With dsDetail
                    Try
                        .DATECREATED = oCashIn.GetData(sNoId).DATECREATED
                    Catch oErr As Exception
                        .DATECREATED = Now
                    End Try
                    .DATEUPDATED = Now

                    .SEQ = i
                    .KDCASHIN = ds.KDCASHIN
                    .NOINVOICE = grvDetail.GetRowCellValue(i, colNOINVOICE)

                    ds.MEMO &= .NOINVOICE & ", "
                    .AMOUNTORIGINAL = CDec(grvDetail.GetRowCellValue(i, colAMOUNTORIGINAL))
                    .AMOUNTDUE = CDec(grvDetail.GetRowCellValue(i, colAMOUNTDUE))
                    .AMOUNTPAYMENT = CDec(grvDetail.GetRowCellValue(i, colAMOUNTPAYMENT))
                    .REMARKS = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colREMARKS)), "-", grvDetail.GetRowCellValue(i, colREMARKS))
                End With
                arrDetail.Add(dsDetail)
            Next

            ' ***** DETIL RETURN *****
            Dim arrDetail_R = oCashIn.GetStructureDetailList
            For i As Integer = 0 To grvDetail_R.RowCount - 2
                Dim dsDetail_R = oCashIn.GetStructureDetail
                With dsDetail_R
                    Try
                        .DATECREATED = oCashIn.GetData(sNoId).DATECREATED
                    Catch oErr As Exception
                        .DATECREATED = Now
                    End Try
                    .DATEUPDATED = Now

                    .SEQ = i + 100
                    .KDCASHIN = ds.KDCASHIN
                    .NOINVOICE = grvDetail_R.GetRowCellValue(i, colNOINVOICE_R)

                    ds.MEMO &= .NOINVOICE & ", "
                    .AMOUNTORIGINAL = CDec(grvDetail_R.GetRowCellValue(i, colAMOUNTORIGINAL_UB_R))
                    .AMOUNTDUE = CDec(grvDetail_R.GetRowCellValue(i, colAMOUNTDUE_UB_R))
                    .AMOUNTPAYMENT = CDec(grvDetail_R.GetRowCellValue(i, colAMOUNTPAYMENT_R))
                    .REMARKS = IIf(String.IsNullOrEmpty(grvDetail_R.GetRowCellValue(i, colREMARKS_R)), "-", grvDetail_R.GetRowCellValue(i, colREMARKS_R))
                End With
                arrDetail_R.Add(dsDetail_R)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    Dim sKDCASHIN As String = ""
                    sKDCASHIN = oCashIn.InsertData(ds, arrDetail, arrDetail_R)
                    If sKDCASHIN = "" Then
                        fn_Save = False
                    Else
                        fn_Save = True
                        txtKDCASHIN.Text = sKDCASHIN
                        fn_CetakKwitansi(txtKDCASHIN.Text.ToString)
                    End If
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oCashIn.UpdateData(ds, arrDetail, arrDetail_R)
                    fn_CetakKwitansi(txtKDCASHIN.Text.ToString)
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
    Private Sub OnValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtADMIN.EditValueChanged, txtROUND.EditValueChanged, txtCOSTSHARING.EditValueChanged, txtDEPOSIT.EditValueChanged, grvDetail.FocusedRowChanged, grvDetail_R.FocusedRowChanged
        If isLoad Then
            Calculate()
        End If
    End Sub
    Private Sub Calculate()
        Dim sSubTotal = 0
        For i As Integer = 0 To grvDetail.RowCount - 2
            sSubTotal += CDec(grvDetail.GetRowCellValue(i, colAMOUNTPAYMENT))
        Next

        'Dim sSubTotal_R = 0
        'For i As Integer = 0 To grvDetail_R.RowCount - 2
        '    sSubTotal_R += grvDetail_R.GetRowCellValue(i, colAMOUNTPAYMENT_R)
        'Next

        'txtSUBTOTAL.Text = sSubTotal - sSubTotal_R

        txtSUBTOTAL.Text = sSubTotal

        txtGRANDTOTAL.Text = CDec(txtSUBTOTAL.Text) + CDec(txtADMIN.Text) + CDec(txtROUND.Text) - CDec(txtDEPOSIT.Text) - CDec(txtCOSTSHARING.Text)

    End Sub
    Private Sub grvDetail_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDetail.CellValueChanged
        If e.Column.Name = colNOINVOICE.Name Then
            Dim oSales As New Sales.clsSalesOrderTransaksi
            Try
                If grvDetail.GetFocusedRowCellValue(colNOINVOICE) IsNot Nothing Then

                    Dim ds = oSales.GetData(grvDetail.GetFocusedRowCellValue(colNOINVOICE))

                    If ds IsNot Nothing Then
                        grvDetail.SetFocusedRowCellValue(colAMOUNTORIGINAL, ds.GRANDTOTAL)
                        grvDetail.SetFocusedRowCellValue(colAMOUNTPAYMENT, ds.GRANDTOTAL - ds.PAYAMOUNT)
                    Else
                        MsgBox(Statement.ErrorUOM, MsgBoxStyle.Exclamation, Me.Text)

                        grvDetail.CancelUpdateCurrentRow()
                    End If
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try

        ElseIf e.Column.Name = colAMOUNTPAYMENT.Name Then
            Dim sJumlah As Decimal = 0

            If grvDetail.GetFocusedRowCellValue(colAMOUNTPAYMENT) > 0 Then
                sJumlah = CDec(grvDetail.GetFocusedRowCellValue(colAMOUNTORIGINAL)) - CDec(grvDetail.GetFocusedRowCellValue(colAMOUNTPAYMENT))

                grvDetail.SetFocusedRowCellValue(colAMOUNTDUE, sJumlah)

            End If

        End If
    End Sub
    Private Sub grvDetail_R_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDetail_R.CellValueChanged
        If e.Column.Name = colNOINVOICE_R.Name Then
            Dim oSales As New Sales.clsSalesReturn
            Try
                If grvDetail_R.GetFocusedRowCellValue(colNOINVOICE_R) IsNot Nothing Then

                    Dim ds = oSales.GetData(grvDetail_R.GetFocusedRowCellValue(colNOINVOICE_R))

                    If ds IsNot Nothing Then
                        grvDetail_R.SetFocusedRowCellValue(colAMOUNTORIGINAL_UB_R, ds.GRANDTOTAL)
                        grvDetail_R.SetFocusedRowCellValue(colAMOUNTPAYMENT_R, ds.GRANDTOTAL - ds.PAYAMOUNT)
                    Else
                        MsgBox(Statement.ErrorUOM, MsgBoxStyle.Exclamation, Me.Text)

                        grvDetail_R.CancelUpdateCurrentRow()
                    End If
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try

        ElseIf e.Column.Name = colAMOUNTPAYMENT_R.Name Then
            Dim sJumlah As Decimal = 0

            If grvDetail_R.GetFocusedRowCellValue(colAMOUNTPAYMENT_R) > 0 Then
                sJumlah = CDec(grvDetail_R.GetFocusedRowCellValue(colAMOUNTORIGINAL_UB_R)) - CDec(grvDetail_R.GetFocusedRowCellValue(colAMOUNTPAYMENT_R))

                grvDetail_R.SetFocusedRowCellValue(colAMOUNTDUE_UB_R, sJumlah)

            End If

        End If
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmCashIn_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    Private Sub btnPasienPulang_Click() Handles btnPasienPulang.ItemClick
        Try
            If grdKDPENDAFTARAN.Text = String.Empty Then Exit Sub
            Dim oDaftar As New Admission.clsPendaftaran

            Dim dsDaftar = oDaftar.GetData(grdKDPENDAFTARAN.EditValue)

            If dsDaftar IsNot Nothing Then

                If dsDaftar.CATEGORY = 0 Then
                    MsgBox("Register Rawat Jalan", MsgBoxStyle.Information, Me.Text)
                Else
                    Dim oPulang As New Admission.clsUpdate_Tanggal_Pulang
                    Dim dsPulang = oPulang.GetDatabyKD(dsDaftar.KDPENDAFTARAN)

                    If dsPulang Is Nothing Then
                        sREKAMMEDIS = dsDaftar.KDCUSTOMER


                        Dim frmUpdate_Tanggal_Pulang As New frmUpdate_Tanggal_Pulang
                        frmUpdate_Tanggal_Pulang.LoadMe(FORM_MODE.FORM_MODE_ADD)
                        frmUpdate_Tanggal_Pulang.LoadMeOtomatis(True, dsDaftar.KDPENDAFTARAN, Now, 0)
                        frmUpdate_Tanggal_Pulang.ShowDialog(Me)

                    Else
                        sREKAMMEDIS = ""

                        Dim frmUpdate_Tanggal_Pulang As New frmUpdate_Tanggal_Pulang
                        frmUpdate_Tanggal_Pulang.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsPulang.KDUPDATE_TANGGAL_PULANG)
                        frmUpdate_Tanggal_Pulang.ShowDialog(Me)

                    End If
                End If
            End If

            sREKAMMEDIS = ""
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
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
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail.DeleteSelectedRows()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadHistoryPasien(ByVal sKDCUSTOMER As String)
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
            SQL &= "NomorPendaftaran = A.KDPENDAFTARAN "
            SQL &= ",Tanggal = A.DATE "
            SQL &= ",Tujuan = C.NAME_DISPLAY  "
            SQL &= ",Dokter = D.NAME_DISPLAY "
            SQL &= ",B.KDKUNJUNGAN "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_H A "
            SQL &= "INNER JOIN S_PENDAFTARAN_KUNJUNGAN B "
            SQL &= "ON A.KDPENDAFTARAN = B.KDPENDAFTARAN "
            SQL &= "INNER JOIN M_DEPARTMENT C "
            SQL &= "ON B.KDDEPARTMENT = C.KDDEPARTMENT "
            SQL &= "INNER JOIN M_DOCTOR D "
            SQL &= "ON B.KDDOCTOR = D.KDDOCTOR "
            SQL &= "WHERE A.KDCUSTOMER = '" & sKDCUSTOMER & "' "
            SQL &= "ORDER BY A.DATE DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "HISTORY")

            grdHistoryPasien.MainView = grvHistoryPasien
            grdHistoryPasien.DataSource = ds.Tables("HISTORY")
            grdHistoryPasien.ForceInitialize()

            fn_LoadFormatData()
            grvHistoryPasien.BestFitColumns()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatData()
        grvHistoryPasien.Columns("KDKUNJUNGAN").VisibleIndex = -1
        For iLoop As Integer = 0 To grvHistoryPasien.Columns.Count - 1
            If grvHistoryPasien.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvHistoryPasien.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvHistoryPasien.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grvHistoryPasien.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvHistoryPasien.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvHistoryPasien.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvHistoryPasien.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm:ss}"
            End If
        Next
    End Sub
    Private Sub fn_LoadKDDOCTOR()
        Dim oDoctor As New Reference.clsDoctor
        Try
            Dim ds = oDoctor.GetData.Where(Function(x) x.ISACTIVE = True).ToList()

            grdKDDOCTOR_H.Properties.DataSource = ds
            grdKDDOCTOR_H.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR_H.Properties.DisplayMember = "NAME_DISPLAY"

            grdKDDOCTOR.DataSource = ds
            grdKDDOCTOR.ValueMember = "KDDOCTOR"
            grdKDDOCTOR.DisplayMember = "NAME_DISPLAY"

            grdKDDOCTOR_OBAT.DataSource = ds
            grdKDDOCTOR_OBAT.ValueMember = "KDDOCTOR"
            grdKDDOCTOR_OBAT.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDDEPARTMENT()
        Dim oDepartment As New Reference.clsDepartment
        Try
            Dim ds = oDepartment.GetData.Where(Function(x) x.ISACTIVE = True).ToList()

            grdKDDEPARTMENT_H.Properties.DataSource = ds
            grdKDDEPARTMENT_H.Properties.ValueMember = "KDDEPARTMENT"
            grdKDDEPARTMENT_H.Properties.DisplayMember = "NAME_DISPLAY"

            grdKDDEPARTMENT.DataSource = ds
            grdKDDEPARTMENT.ValueMember = "KDDEPARTMENT"
            grdKDDEPARTMENT.DisplayMember = "NAME_DISPLAY"

            grdKDDEPARTMENT_OBAT.DataSource = ds
            grdKDDEPARTMENT_OBAT.ValueMember = "KDDEPARTMENT"
            grdKDDEPARTMENT_OBAT.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
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
    Private Sub txtCARI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCARI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            sPopUP = True
            If txtCARI.Text = String.Empty Then Exit Sub
            fn_LoadKDKUNJUNGAN(IIf(cboCARI.SelectedIndex = 0, txtCARI.Text.ToString.Trim, txtCARI.Text.ToString.Trim), cboCARI.SelectedIndex)
            txtCARI.ResetText()
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
            SQL &= "A.KDPENDAFTARAN "
            SQL &= ",JENISDAFTAR = E.MEMO "
            SQL &= ",PASIEN = (SELECT TOP 1 BB.GOL FROM S_PENDAFTARAN_KUNJUNGAN AA INNER JOIN M_KESATUAN BB ON AA.KDKESATUAN = BB.KDKESATUAN WHERE A.KDPENDAFTARAN = AA.KDPENDAFTARAN) "
            SQL &= ",D.NAME_DISPLAY "
            SQL &= ",A.DATE "
            SQL &= ",TUJUAN = B.NAME_DISPLAY  "
            SQL &= ",DPJP = C.NAME_DISPLAY "
            SQL &= ",PULANG = ISNULL((SELECT CONVERT(BIT, 1) FROM T_UPDATE_TANGGAL_PULANG WHERE A.KDPENDAFTARAN = KDPENDAFTARAN AND CARAPULANG <> 5), ISNULL((SELECT CONVERT(BIT, 1) FROM T_UPDATE_TANGGAL_PULANG WHERE A.KDPENDAFTARAN_AWAL = KDPENDAFTARAN AND CARAPULANG <> 5), CONVERT(BIT, 0)))  "
            SQL &= ",RANAP = (SELECT CASE WHEN KDPENDAFTARAN_AWAL <> '' THEN CONVERT(BIT, 1) ELSE CONVERT(BIT, 0) END) "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_H A "
            SQL &= "INNER JOIN M_DEPARTMENT B "
            SQL &= "ON A.KDDEPARTMENT = B.KDDEPARTMENT "
            SQL &= "INNER JOIN M_DOCTOR C "
            SQL &= "ON A.KDDOCTOR = C.KDDOCTOR "
            SQL &= "INNER JOIN M_CUSTOMER D "
            SQL &= "ON A.KDCUSTOMER = D.KDCUSTOMER "
            SQL &= "INNER JOIN M_DAFTAR_L1 E "
            SQL &= "ON A.KDDAFTAR_L1 = E.KDDAFTAR_L1 "
            If sCari = 0 Then
                SQL &= "WHERE A.KDCUSTOMER LIKE '%" & sParameter & "%' "
            ElseIf sCari = 1 Then
                SQL &= "WHERE D.NAME_DISPLAY LIKE '%" & sParameter & "%' "
            Else
                SQL &= "WHERE A.KDPENDAFTARAN LIKE '%" & sParameter & "%' "
            End If

            SQL &= "ORDER BY A.DATE DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

            grdKDPENDAFTARAN.Properties.DataSource = ds.Tables("ALL")
            grdKDPENDAFTARAN.Properties.ValueMember = "KDPENDAFTARAN"
            grdKDPENDAFTARAN.Properties.DisplayMember = "KDPENDAFTARAN"

            If sPopUP = True Then
                grdKDPENDAFTARAN.ShowPopup()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDKUNJUNGAN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdKDPENDAFTARAN.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Dim oKunjungan As New Admission.clsPendaftaran
            Dim dsKunjungan = oKunjungan.GetData(grdKDPENDAFTARAN.EditValue)
            If dsKunjungan IsNot Nothing Then
                txtNAMAPASIEN.Text = dsKunjungan.M_CUSTOMER.NAME_DISPLAY & " / " & dsKunjungan.KDCUSTOMER
                grdKDDEPARTMENT_H.Text = dsKunjungan.KDDEPARTMENT
                grdKDDOCTOR_H.Text = dsKunjungan.KDDOCTOR
                txtKDPENDAFTARAN_AWAL.Text = dsKunjungan.KDPENDAFTARAN_AWAL

                If dsKunjungan.M_DAFTAR_L1.MEMO = "BPJS" Then
                    grdKDPAYMENTTYPE.ResetText()
                Else
                    grdKDPAYMENTTYPE.Text = oCashIn.Daftar_Payment_Default
                End If
            Else
                txtNAMAPASIEN.ResetText()
                grdKDDEPARTMENT_H.ResetText()
                grdKDDOCTOR_H.ResetText()
                txtKDPENDAFTARAN_AWAL.ResetText()
            End If

            fn_LoadNOINVOICE()
            fn_LoadNOINVOICE_R()
            fn_LoadDeposit()
            fn_LoadCostShare()
            grvDetail.Focus()
        End If
    End Sub
    Private Sub fn_LoadDataRincian(ByVal sNoinvoice As String)
        Try
            Dim oSalesOrderTransaksi As New Sales.clsSalesOrderTransaksi

            ' ***** HEADER *****
            'Dim ds = oSalesOrderTransaksi.GetData(Noinvoice)

            Dim dsItem = oSalesOrderTransaksi.GetDataDetailTanpaObat(sNoinvoice).ToList()

            For Each xloop In dsItem.OrderBy(Function(x) x.S_SO_TRANSAKSI_H.DATE).OrderBy(Function(x) x.SEQ)
                grvDeatilRincian.Focus()
                grvDeatilRincian.AddNewRow()
                grvDeatilRincian.SetFocusedRowCellValue(colKDSOTRANSAKSI, xloop.KDSOTRANSAKSI)
                grvDeatilRincian.SetFocusedRowCellValue(colDATECREATED, xloop.DATECREATED)
                grvDeatilRincian.SetFocusedRowCellValue(colKDITEM, xloop.KDITEM)
                grvDeatilRincian.SetFocusedRowCellValue(colQTY, xloop.QTY)
                grvDeatilRincian.SetFocusedRowCellValue(colKDDOCTOR, xloop.KDDOCTOR)
                grvDeatilRincian.SetFocusedRowCellValue(colKDUOM, xloop.KDUOM)
                grvDeatilRincian.SetFocusedRowCellValue(colKDDEPARTMENT, xloop.KDDEPARTMENT)
                grvDeatilRincian.SetFocusedRowCellValue(colPRICE, xloop.PRICE)
                grvDeatilRincian.SetFocusedRowCellValue(colSUBTOTAL, xloop.SUBTOTAL)
                grvDeatilRincian.SetFocusedRowCellValue(colDISCOUNT, xloop.DISCOUNT)
                grvDeatilRincian.SetFocusedRowCellValue(colGRANDTOTAL, xloop.GRANDTOTAL)
                grvDeatilRincian.SetFocusedRowCellValue(colCATEGORYI, IIf(xloop.S_SO_TRANSAKSI_H.CATEGORY = 0, "Rawat Jalan", IIf(xloop.S_SO_TRANSAKSI_H.CATEGORY = 1, "Rawat Inap", IIf(xloop.S_SO_TRANSAKSI_H.CATEGORY = 2, "Laboratorium", "Radiologi"))))
                grvDeatilRincian.UpdateCurrentRow()
            Next

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDataObat(ByVal sNoinvoice As String)
        Try
            Dim oSalesOrderTransaksi As New Sales.clsSalesOrderTransaksi

            ' ***** HEADER *****
            'Dim ds = oSalesOrderTransaksi.GetData(Noinvoice)

            Dim dsObat = oSalesOrderTransaksi.GetDataDetailObat(sNoinvoice).ToList()

            For Each xloop In dsObat.OrderBy(Function(x) x.S_SO_TRANSAKSI_H.DATE).OrderBy(Function(x) x.SEQ)
                grvDetailObat.Focus()
                grvDetailObat.AddNewRow()
                grvDetailObat.SetFocusedRowCellValue(colKDSOTRANSKASI_OBAT, xloop.KDSOTRANSAKSI)
                grvDetailObat.SetFocusedRowCellValue(colDATECREATED_OBAT, xloop.DATECREATED)
                grvDetailObat.SetFocusedRowCellValue(colKDITEM_OBAT, xloop.KDITEM)
                grvDetailObat.SetFocusedRowCellValue(colQTY_OBAT, xloop.QTY)
                grvDetailObat.SetFocusedRowCellValue(colKDUOM_OBAT, xloop.KDUOM)
                grvDetailObat.SetFocusedRowCellValue(colKDDOCTOR_OBAT, xloop.KDDOCTOR)
                grvDetailObat.SetFocusedRowCellValue(colKDDEPARTMENT_OBAT, xloop.KDDEPARTMENT)
                grvDetailObat.SetFocusedRowCellValue(colPRICE_OBAT, xloop.PRICE)
                grvDetailObat.SetFocusedRowCellValue(colSUBTOTAL_OBAT, xloop.SUBTOTAL)
                grvDetailObat.SetFocusedRowCellValue(colDISCOUNT_OBAT, xloop.DISCOUNT)
                grvDetailObat.SetFocusedRowCellValue(colGRANDTORAL_OBAT, xloop.GRANDTOTAL)
                grvDetailObat.UpdateCurrentRow()
            Next

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDataItem(ByVal sKDPENDAFTARAN1 As String, ByVal sKDPENDAFTARAN2 As String)
        Try
            Dim oSalesOrderTransaksi As New Sales.clsSalesOrderTransaksi

            ' ***** HEADER *****
            'Dim ds = oSalesOrderTransaksi.GetData(Noinvoice)

            Dim dsDaftar1 = oSalesOrderTransaksi.GetDataDetailpendafataranTanpaObat(sKDPENDAFTARAN1).ToList()
            Dim dsDaftar2 = oSalesOrderTransaksi.GetDataDetailpendafataranTanpaObat(sKDPENDAFTARAN2).ToList()

            Dim dsItem1 = From x In dsDaftar1
                          Group x By x.KDITEM Into QTY = Sum(x.QTY)
                          Select KDITEM, QTY
            Dim dsItem2 = From x In dsDaftar2
                          Group x By x.KDITEM Into QTY = Sum(x.QTY)
                          Select KDITEM, QTY

            Dim dsItem = dsItem1.Union(dsItem2)

            Dim dsGroup = From x In dsItem
                          Group x By x.KDITEM Into QTY = Sum(x.QTY)
                          Select KDITEM, QTY

            For Each xloop In dsGroup
                grvDetailItem.Focus()
                grvDetailItem.AddNewRow()
                grvDetailItem.SetFocusedRowCellValue(colKDITEM_ITEM, xloop.KDITEM)
                grvDetailItem.SetFocusedRowCellValue(colQTY_ITEM, xloop.QTY)
                grvDetailItem.UpdateCurrentRow()
            Next

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadNOINVOICE()
        If grdKDPENDAFTARAN.Text = String.Empty Then Exit Sub

        Dim oKunjungan As New Admission.clsPendaftaran
        Dim oSalesTransaksi As New Sales.clsSalesOrderTransaksi

        Try
            Dim ds = From x In oKunjungan.GetDataKunjunganSalesOrder(grdKDPENDAFTARAN.EditValue, txtKDPENDAFTARAN_AWAL.Text)
                     Join y In oSalesTransaksi.GetData
                     On x.KDKUNJUNGAN Equals y.KDKUNJUNGAN
                     Where y.GRANDTOTAL > y.PAYAMOUNT
                     Select NOINVOICE = y.KDSOTRANSAKSI, y.DATE, y.GRANDTOTAL, y.PAYAMOUNT

            grdNOINVOICE.DataSource = ds.ToList
            grdNOINVOICE.ValueMember = "NOINVOICE"
            grdNOINVOICE.DisplayMember = "NOINVOICE"

            grvDetail.OptionsSelection.MultiSelect = True
            grvDetail.SelectAll()
            grvDetail.DeleteSelectedRows()
            grvDetail.OptionsSelection.MultiSelect = False

            For Each xloop In ds
                grvDetail.Focus()
                grvDetail.AddNewRow()
                grvDetail.SetFocusedRowCellValue(colNOINVOICE, xloop.NOINVOICE)
                grvDetail.UpdateCurrentRow()
            Next

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadNOINVOICE_R()
        If grdKDPENDAFTARAN.Text = String.Empty Then Exit Sub

        tabControl.SelectedTabPage = tab7

        Dim oKunjungan As New Admission.clsPendaftaran
        Dim oSalesSalesReturn As New Sales.clsSalesReturn

        Try
            Dim ds = From x In oKunjungan.GetDataKunjunganSalesOrder(grdKDPENDAFTARAN.EditValue, txtKDPENDAFTARAN_AWAL.Text)
                     Join y In oSalesSalesReturn.GetData
                     On x.KDKUNJUNGAN Equals y.KDKUNJUNGAN
                     Where y.GRANDTOTAL > y.PAYAMOUNT
                     Select NOINVOICE = y.KDSR, y.DATE, y.GRANDTOTAL, y.PAYAMOUNT


            grdNOINVOICE_R.DataSource = ds.ToList
            grdNOINVOICE_R.ValueMember = "NOINVOICE"
            grdNOINVOICE_R.DisplayMember = "NOINVOICE"

            grvDetail_R.OptionsSelection.MultiSelect = True
            grvDetail_R.SelectAll()
            grvDetail_R.DeleteSelectedRows()
            grvDetail_R.OptionsSelection.MultiSelect = False

            Dim Discount As Decimal = 0

            For Each xloop In ds
                Discount += xloop.GRANDTOTAL
                grvDetail_R.Focus()
                grvDetail_R.AddNewRow()
                grvDetail_R.SetFocusedRowCellValue(colNOINVOICE_R, xloop.NOINVOICE)
                grvDetail_R.UpdateCurrentRow()
            Next

            txtADMIN.Text = CDec(txtADMIN.Text) + Discount

            tabControl.SelectedTabPage = tab1
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

            grdKDUOM_OBAT.DataSource = oUOM.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDUOM_OBAT.ValueMember = "KDUOM"
            grdKDUOM_OBAT.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDITEM()
        Dim oITEM As New Reference.clsItem
        Try
            grdKDITEM.DataSource = oITEM.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDITEM.ValueMember = "KDITEM"
            grdKDITEM.DisplayMember = "NMITEM2"

            grdKDITEM_OBAT.DataSource = oITEM.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDITEM_OBAT.ValueMember = "KDITEM"
            grdKDITEM_OBAT.DisplayMember = "NMITEM2"

            grdKDITEM_Item.DataSource = oITEM.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDITEM_Item.ValueMember = "KDITEM"
            grdKDITEM_Item.DisplayMember = "NMITEM2"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDeposit()
        If grdKDPENDAFTARAN.Text = String.Empty Then Exit Sub

        Dim oDeposit As New Finance.clsDeposit

        Try
            Dim ds = oDeposit.GetDataByKD(grdKDPENDAFTARAN.EditValue)
            If ds IsNot Nothing Then
                txtDEPOSIT.Text = CDec(ds.TOTAL)
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadCostShare()
        If grdKDPENDAFTARAN.Text = String.Empty Then Exit Sub

        Dim oCostShare As New Finance.clsCostShare

        Try
            Dim ds = oCostShare.GetDataByKD(grdKDPENDAFTARAN.EditValue)
            If ds IsNot Nothing Then
                txtCOSTSHARING.Text = CDec(ds.TOTAL_COSTSHARE)
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_CetakKwitansi(ByVal sKDCASIN As String)
        Try
            If sKDCASIN = String.Empty Then Exit Sub
            Dim oPendaftaran As New Admission.clsPendaftaran
            Dim oPulang As New Admission.clsUpdate_Tanggal_Pulang
            Dim dsCashin = oCashIn.GetData(sKDCASIN)

            Dim dsDaftar = oPendaftaran.GetData(dsCashin.KDPENDAFTARAN)

            If dsDaftar.CATEGORY = 1 Then
                Dim sPulang As String = String.Empty
                Dim dsPulang = oPulang.GetDatabyKD(dsDaftar.KDPENDAFTARAN)
                If dsPulang Is Nothing Then
                    MsgBox("Pasien belum Pulang", MsgBoxStyle.Exclamation, Me.Text)
                    Try

                        Dim rptri As New xtraKwitansiRekap

                        rptri.ShowPrintMarginsWarning = False
                        rptri.Watermark.Text = sWATERMARK
                        Dim dsRI = oCashIn.GetData(sKDCASIN)

                        If dsRI.COSTSHARE > 0 Then
                            sPrintGrandTotal = dsRI.COSTSHARE
                            sCostShare = True
                        Else
                            sPrintGrandTotal = dsRI.GRANDTOTAL
                            sCostShare = False
                        End If

                        sPulang = dsDaftar.DATE.ToString("dd-MM-yyyy") & " sampai tanggal " & dsDaftar.DATE.ToString("dd-MM-yyyy") & " di " & sCompany.Trim.ToUpper

                        sPrintCashierDescriprtion = "Untuk pembayaran biaya pelayanan kesehatan pasien " & IIf(dsDaftar.CATEGORY = 0, "Rawat Jalan tanggal " & dsDaftar.DATE.ToString("dd-MM-yyyy"), "Rawat Inap tanggal " & sPulang)
                        rptri.bindingSource.DataSource = dsRI
                        Dim printTool1 As New DevExpress.XtraReports.UI.ReportPrintTool(rptri)
                        printTool1.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
                    Catch oErr As Exception
                        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try

                    Exit Sub
                Else
                    Try

                        Dim rptri As New xtraKwitansiRekap

                        rptri.ShowPrintMarginsWarning = False
                        rptri.Watermark.Text = sWATERMARK
                        Dim dsRI = oCashIn.GetData(sKDCASIN)
                        If dsRI.COSTSHARE > 0 Then
                            sPrintGrandTotal = dsRI.COSTSHARE
                            sCostShare = True
                        Else
                            sPrintGrandTotal = dsRI.GRANDTOTAL
                            sCostShare = False
                        End If

                        sPulang = dsDaftar.DATE.ToString("dd-MM-yyyy") & " sampai tanggal " & dsPulang.DATE.ToString("dd-MM-yyyy") & " di " & sCompany.Trim.ToUpper

                        sPrintCashierDescriprtion = "Untuk pembayaran biaya pelayanan kesehatan pasien " & IIf(dsDaftar.CATEGORY = 0, "Rawat Jalan tanggal " & dsDaftar.DATE.ToString("dd-MM-yyyy"), "Rawat Inap tanggal " & sPulang)
                        rptri.bindingSource.DataSource = dsRI
                        Dim printTool1 As New DevExpress.XtraReports.UI.ReportPrintTool(rptri)
                        printTool1.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
                    Catch oErr As Exception
                        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try

                End If
            Else
                sPrintGrandTotal = dsCashin.GRANDTOTAL
            End If

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

            Dim listTranskasi As New List(Of R_CASHIN)

            If dsDaftar.CATEGORY = 0 Then
                SQL = " SELECT  "
                SQL &= " A.KDCASHIN "
                SQL &= " ,CATEGORY = (SELECT CATEGORY FROM S_PENDAFTARAN_H WHERE A.KDPENDAFTARAN = KDPENDAFTARAN) "
                SQL &= " ,KDPENDAFTARAN = (SELECT AA.KDPENDAFTARAN FROM S_PENDAFTARAN_H AA INNER JOIN S_PENDAFTARAN_KUNJUNGAN BB ON AA.KDPENDAFTARAN = BB.KDPENDAFTARAN WHERE C.KDKUNJUNGAN = BB.KDKUNJUNGAN) "
                SQL &= " ,TUJUAN = (SELECT BB.NAME_DISPLAY FROM S_PENDAFTARAN_H AA INNER JOIN M_DEPARTMENT BB ON AA.KDDEPARTMENT = BB.KDDEPARTMENT WHERE A.KDPENDAFTARAN = AA.KDPENDAFTARAN)  "
                SQL &= " ,DPJP = (SELECT BB.NAME_DISPLAY FROM S_PENDAFTARAN_H AA INNER JOIN M_DOCTOR BB ON AA.KDDOCTOR = BB.KDDOCTOR WHERE A.KDPENDAFTARAN = AA.KDPENDAFTARAN) "
                SQL &= " ,KDCUSTOMER = (SELECT KDCUSTOMER FROM S_PENDAFTARAN_H WHERE A.KDPENDAFTARAN = KDPENDAFTARAN) "
                SQL &= " ,PASIEN = (SELECT BB.NAME_DISPLAY FROM S_PENDAFTARAN_H AA INNER JOIN M_CUSTOMER BB ON AA.KDCUSTOMER = BB.KDCUSTOMER WHERE A.KDPENDAFTARAN = AA.KDPENDAFTARAN)  "
                SQL &= " ,ALAMAT = (SELECT TOP 1 ALAMAT FROM S_PENDAFTARAN_KUNJUNGAN WHERE A.KDPENDAFTARAN = KDPENDAFTARAN) "
                SQL &= " ,KELAS = (SELECT BB.MEMO FROM S_PENDAFTARAN_H AA INNER JOIN M_KELASRAWAT BB ON AA.KDKELASRAWAT = BB.KDKELASRAWAT WHERE A.KDPENDAFTARAN = AA.KDPENDAFTARAN)  "
                SQL &= " ,TANGGAL_DATANG = (SELECT DATE FROM S_PENDAFTARAN_H WHERE A.KDPENDAFTARAN = KDPENDAFTARAN) "
                SQL &= " ,TANGGAL_PULANG = ISNULL((SELECT DATE FROM T_UPDATE_TANGGAL_PULANG WHERE A.KDPENDAFTARAN = KDPENDAFTARAN), GETDATE()) "
                SQL &= " ,A.SUBTOTAL "
                SQL &= " ,A.COSTSHARE "
                SQL &= " ,A.DEPOSIT "
                SQL &= " ,A.ADMIN "
                SQL &= " ,A.ROUND "
                SQL &= " ,A.GRANDTOTAL "
                SQL &= " ,ITEM_GROUP = F.MEMO "
                If dsDaftar.M_DAFTAR_L1.MEMO = "BPJS" Then
                    SQL &= " ,ITEM = E.NMITEM2 "
                Else
                    SQL &= " ,ITEM = E.NMITEM2  + ' (' + (SELECT NAME_DISPLAY FROM M_DOCTOR WHERE D.KDDOCTOR = KDDOCTOR) + ')' "
                End If
                SQL &= " ,QTY = SUM(D.QTY) "
                SQL &= " ,GRANDTOTAL_DETAIL = SUM(D.GRANDTOTAL)  "
                SQL &= " FROM F_CASHIN_H A  "
                SQL &= " INNER JOIN F_CASHIN_D B "
                SQL &= " ON A.KDCASHIN = B.KDCASHIN "
                SQL &= " INNER JOIN S_SO_TRANSAKSI_H C  "
                SQL &= " ON B.NOINVOICE = C.KDSOTRANSAKSI "
                SQL &= " INNER JOIN S_SO_TRANSAKSI_D D "
                SQL &= " ON C.KDSOTRANSAKSI = D.KDSOTRANSAKSI "
                SQL &= " INNER JOIN M_ITEM E "
                SQL &= " ON D.KDITEM = E.KDITEM "
                SQL &= " INNER JOIN M_ITEM_L2 F "
                SQL &= " ON E.KDITEM_L2 = F.KDITEM_L2  "
                SQL &= " WHERE "
                SQL &= " A.KDCASHIN = '" & sKDCASIN & "' "
                SQL &= "GROUP BY "
                SQL &= " A.KDCASHIN "
                SQL &= " ,A.KDPENDAFTARAN "
                SQL &= " ,C.KDKUNJUNGAN "
                SQL &= " ,A.KDPENDAFTARAN "
                'SQL &= " ,B.NOINVOICE "
                SQL &= " ,A.SUBTOTAL "
                SQL &= " ,A.COSTSHARE "
                SQL &= " ,A.DEPOSIT "
                SQL &= " ,A.ADMIN "
                SQL &= " ,A.ROUND "
                SQL &= " ,A.GRANDTOTAL "
                SQL &= " ,F.MEMO "
                SQL &= " ,E.NMITEM2 "
                If dsDaftar.M_DAFTAR_L1.MEMO = "BPJS" Then
                    'SQL &= " ,ITEM = E.NMITEM2 "
                Else
                    SQL &= " ,D.KDDOCTOR "
                End If

                oComm.Connection = oConn
                oComm.CommandText = SQL
                oComm.CommandTimeout = 120
                oComm.CommandType = CommandType.Text

                da = New SqlDataAdapter(oComm)
                da.Fill(ds, "ALL")


                For iLoop As Integer = 0 To ds.Tables("ALL").Rows.Count - 1
                    Dim dsRekap As New DataAccess.R_CASHIN

                    With ds.Tables("ALL")
                        dsRekap.KDCASHIN = .Rows(iLoop)("KDCASHIN")
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
                        dsRekap.NOINVOICE = dsDaftar.M_CUSTOMER.M_PENJAMIN.MEMO
                        dsRekap.SUBTOTAL = .Rows(iLoop)("SUBTOTAL")
                        dsRekap.COSTSHARE = .Rows(iLoop)("COSTSHARE")
                        dsRekap.DEPOSIT = .Rows(iLoop)("DEPOSIT")
                        dsRekap.ADMIN = .Rows(iLoop)("ADMIN")
                        dsRekap.ROUND = .Rows(iLoop)("ROUND")
                        dsRekap.GRANDTOTAL = .Rows(iLoop)("GRANDTOTAL")
                        dsRekap.ITEM_GROUP = .Rows(iLoop)("ITEM_GROUP")
                        dsRekap.ITEM = .Rows(iLoop)("ITEM")
                        dsRekap.ITEM_GROUP = .Rows(iLoop)("ITEM_GROUP")
                        dsRekap.QTY = .Rows(iLoop)("QTY")
                        dsRekap.GRANDTOTAL_DETAIL = .Rows(iLoop)("GRANDTOTAL_DETAIL")

                        listTranskasi.Add(dsRekap)
                    End With
                Next

            Else
                SQL = " SELECT  "
                SQL &= " A.KDCASHIN "
                SQL &= " ,CATEGORY = (SELECT CATEGORY FROM S_PENDAFTARAN_H WHERE A.KDPENDAFTARAN = KDPENDAFTARAN) "
                SQL &= " ,KDPENDAFTARAN = (SELECT AA.KDPENDAFTARAN FROM S_PENDAFTARAN_H AA INNER JOIN S_PENDAFTARAN_KUNJUNGAN BB ON AA.KDPENDAFTARAN = BB.KDPENDAFTARAN WHERE C.KDKUNJUNGAN = BB.KDKUNJUNGAN) "
                SQL &= " ,TUJUAN = (SELECT BB.NAME_DISPLAY FROM S_PENDAFTARAN_H AA INNER JOIN M_DEPARTMENT BB ON AA.KDDEPARTMENT = BB.KDDEPARTMENT WHERE A.KDPENDAFTARAN = AA.KDPENDAFTARAN)  "
                SQL &= " ,DPJP = (SELECT BB.NAME_DISPLAY FROM S_PENDAFTARAN_H AA INNER JOIN M_DOCTOR BB ON AA.KDDOCTOR = BB.KDDOCTOR WHERE A.KDPENDAFTARAN = AA.KDPENDAFTARAN) "
                SQL &= " ,KDCUSTOMER = (SELECT KDCUSTOMER FROM S_PENDAFTARAN_H WHERE A.KDPENDAFTARAN = KDPENDAFTARAN) "
                SQL &= " ,PASIEN = (SELECT BB.NAME_DISPLAY FROM S_PENDAFTARAN_H AA INNER JOIN M_CUSTOMER BB ON AA.KDCUSTOMER = BB.KDCUSTOMER WHERE A.KDPENDAFTARAN = AA.KDPENDAFTARAN)  "
                SQL &= " ,ALAMAT = (SELECT TOP 1 ALAMAT FROM S_PENDAFTARAN_KUNJUNGAN WHERE A.KDPENDAFTARAN = KDPENDAFTARAN) "
                SQL &= " ,KELAS = (SELECT BB.MEMO FROM S_PENDAFTARAN_H AA INNER JOIN M_KELASRAWAT BB ON AA.KDKELASRAWAT = BB.KDKELASRAWAT WHERE A.KDPENDAFTARAN = AA.KDPENDAFTARAN)  "
                SQL &= " ,TANGGAL_DATANG = (SELECT DATE FROM S_PENDAFTARAN_H WHERE A.KDPENDAFTARAN = KDPENDAFTARAN) "
                SQL &= " ,TANGGAL_PULANG = ISNULL((SELECT DATE FROM T_UPDATE_TANGGAL_PULANG WHERE A.KDPENDAFTARAN = KDPENDAFTARAN), GETDATE()) "
                SQL &= " ,A.SUBTOTAL "
                SQL &= " ,A.COSTSHARE "
                SQL &= " ,A.DEPOSIT "
                SQL &= " ,A.ADMIN "
                SQL &= " ,A.ROUND "
                SQL &= " ,A.GRANDTOTAL "
                SQL &= " ,ITEM_GROUP = F.MEMO "
                If dsDaftar.M_DAFTAR_L1.MEMO = "BPJS" Then
                    SQL &= " ,ITEM = E.NMITEM2 "
                Else
                    SQL &= " ,ITEM = E.NMITEM2  + ' (' + (SELECT NAME_DISPLAY FROM M_DOCTOR WHERE D.KDDOCTOR = KDDOCTOR) + ')' "
                End If
                SQL &= " ,QTY = SUM(D.QTY) "
                SQL &= " ,GRANDTOTAL_DETAIL = SUM(D.GRANDTOTAL)  "
                SQL &= " FROM F_CASHIN_H A  "
                SQL &= " INNER JOIN F_CASHIN_D B "
                SQL &= " ON A.KDCASHIN = B.KDCASHIN "
                SQL &= " INNER JOIN S_SO_TRANSAKSI_H C  "
                SQL &= " ON B.NOINVOICE = C.KDSOTRANSAKSI "
                SQL &= " INNER JOIN S_SO_TRANSAKSI_D D "
                SQL &= " ON C.KDSOTRANSAKSI = D.KDSOTRANSAKSI "
                SQL &= " INNER JOIN M_ITEM E "
                SQL &= " ON D.KDITEM = E.KDITEM "
                SQL &= " INNER JOIN M_ITEM_L2 F "
                SQL &= " ON E.KDITEM_L2 = F.KDITEM_L2  "
                SQL &= " WHERE "
                SQL &= " A.KDCASHIN = '" & sKDCASIN & "' "
                SQL &= " AND F.KDITEM_L2 <> 'ITEM_L2_0000000016' AND F.KDITEM_L2 <> 'ITEM_L2_0000000002' AND F.KDITEM_L2 <> 'ITEM_L2_0000000003' "
                SQL &= "GROUP BY "
                SQL &= " A.KDCASHIN "
                SQL &= " ,A.KDPENDAFTARAN "
                SQL &= " ,C.KDKUNJUNGAN "
                SQL &= " ,A.KDPENDAFTARAN "
                SQL &= " ,A.SUBTOTAL "
                SQL &= " ,A.COSTSHARE "
                SQL &= " ,A.DEPOSIT "
                SQL &= " ,A.ADMIN "
                SQL &= " ,A.ROUND "
                SQL &= " ,A.GRANDTOTAL "
                SQL &= " ,F.MEMO "
                SQL &= " ,E.NMITEM2 "
                SQL &= " ,A.DATE "
                If dsDaftar.M_DAFTAR_L1.MEMO = "BPJS" Then
                    'SQL &= " ,ITEM = E.NMITEM2 "
                Else
                    SQL &= " ,D.KDDOCTOR "
                End If

                'SQL &= " ORDER BY A.DATE "

                oComm.Connection = oConn
                oComm.CommandText = SQL
                oComm.CommandTimeout = 120
                oComm.CommandType = CommandType.Text

                da = New SqlDataAdapter(oComm)
                da.Fill(ds, "ALL")


                For iLoop As Integer = 0 To ds.Tables("ALL").Rows.Count - 1
                    Dim dsRekap As New DataAccess.R_CASHIN

                    With ds.Tables("ALL")
                        dsRekap.KDCASHIN = .Rows(iLoop)("KDCASHIN")
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
                        dsRekap.NOINVOICE = dsDaftar.M_CUSTOMER.M_PENJAMIN.MEMO
                        dsRekap.SUBTOTAL = .Rows(iLoop)("SUBTOTAL")
                        dsRekap.COSTSHARE = .Rows(iLoop)("COSTSHARE")
                        dsRekap.DEPOSIT = .Rows(iLoop)("DEPOSIT")
                        dsRekap.ADMIN = .Rows(iLoop)("ADMIN")
                        dsRekap.ROUND = .Rows(iLoop)("ROUND")
                        dsRekap.GRANDTOTAL = .Rows(iLoop)("GRANDTOTAL")
                        dsRekap.ITEM_GROUP = .Rows(iLoop)("ITEM_GROUP")
                        dsRekap.ITEM = .Rows(iLoop)("ITEM")
                        dsRekap.ITEM_GROUP = .Rows(iLoop)("ITEM_GROUP")
                        dsRekap.QTY = .Rows(iLoop)("QTY")
                        dsRekap.GRANDTOTAL_DETAIL = .Rows(iLoop)("GRANDTOTAL_DETAIL")

                        listTranskasi.Add(dsRekap)
                    End With
                Next


                SQL = " SELECT  "
                SQL &= " A.KDCASHIN "
                SQL &= " ,CATEGORY = '" & dsDaftar.CATEGORY & "' "
                SQL &= " ,KDPENDAFTARAN = '" & dsDaftar.KDPENDAFTARAN & "' "
                SQL &= " ,TUJUAN = '" & dsDaftar.M_DEPARTMENT.NAME_DISPLAY & "' "
                SQL &= " ,DPJP = '" & dsDaftar.M_DOCTOR.NAME_DISPLAY & "' "
                SQL &= " ,KDCUSTOMER = '" & dsDaftar.KDCUSTOMER & "' "
                SQL &= " ,PASIEN = '" & dsDaftar.M_CUSTOMER.NAME_DISPLAY & "' "
                SQL &= " ,ALAMAT = '" & dsDaftar.S_PENDAFTARAN_KUNJUNGANs.FirstOrDefault.ALAMAT & "' "
                SQL &= " ,KELAS = '" & dsDaftar.M_KELASRAWAT.MEMO & "' "
                SQL &= " ,TANGGAL_DATANG = '" & dsDaftar.DATE & "' "
                SQL &= " ,TANGGAL_PULANG = '" & dsDaftar.DATE & "' "
                SQL &= " ,A.SUBTOTAL "
                SQL &= " ,A.COSTSHARE "
                SQL &= " ,A.DEPOSIT "
                SQL &= " ,A.ADMIN "
                SQL &= " ,A.ROUND "
                SQL &= " ,A.GRANDTOTAL "
                SQL &= " ,ITEM_GROUP = F.MEMO "
                SQL &= " ,ITEM = 'BIAYA LABORATORIUM' "
                SQL &= " ,QTY = SUM(D.QTY) "
                SQL &= " ,GRANDTOTAL_DETAIL = SUM(D.GRANDTOTAL)  "
                SQL &= " FROM F_CASHIN_H A  "
                SQL &= " INNER JOIN F_CASHIN_D B "
                SQL &= " ON A.KDCASHIN = B.KDCASHIN "
                SQL &= " INNER JOIN S_SO_TRANSAKSI_H C  "
                SQL &= " ON B.NOINVOICE = C.KDSOTRANSAKSI "
                SQL &= " INNER JOIN S_SO_TRANSAKSI_D D "
                SQL &= " ON C.KDSOTRANSAKSI = D.KDSOTRANSAKSI "
                SQL &= " INNER JOIN M_ITEM E "
                SQL &= " ON D.KDITEM = E.KDITEM "
                SQL &= " INNER JOIN M_ITEM_L2 F "
                SQL &= " ON E.KDITEM_L2 = F.KDITEM_L2  "
                SQL &= " WHERE "
                SQL &= " A.KDCASHIN = '" & sKDCASIN & "' "
                SQL &= " AND F.KDITEM_L2 = 'ITEM_L2_0000000002' "
                SQL &= "GROUP BY "
                SQL &= " A.KDCASHIN "
                SQL &= " ,F.MEMO "
                SQL &= " ,A.SUBTOTAL "
                SQL &= " ,A.COSTSHARE "
                SQL &= " ,A.DEPOSIT "
                SQL &= " ,A.ADMIN "
                SQL &= " ,A.ROUND "
                SQL &= " ,A.GRANDTOTAL "
                SQL &= " ,A.DATE "


                oComm.Connection = oConn
                oComm.CommandText = SQL
                oComm.CommandTimeout = 120
                oComm.CommandType = CommandType.Text

                da = New SqlDataAdapter(oComm)
                da.Fill(ds, "ALL2")


                For iLoop As Integer = 0 To ds.Tables("ALL2").Rows.Count - 1
                    Dim dsRekap As New DataAccess.R_CASHIN

                    With ds.Tables("ALL2")
                        dsRekap.KDCASHIN = .Rows(iLoop)("KDCASHIN")
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
                        dsRekap.NOINVOICE = dsDaftar.M_CUSTOMER.M_PENJAMIN.MEMO
                        dsRekap.SUBTOTAL = .Rows(iLoop)("SUBTOTAL")
                        dsRekap.COSTSHARE = .Rows(iLoop)("COSTSHARE")
                        dsRekap.DEPOSIT = .Rows(iLoop)("DEPOSIT")
                        dsRekap.ADMIN = .Rows(iLoop)("ADMIN")
                        dsRekap.ROUND = .Rows(iLoop)("ROUND")
                        dsRekap.GRANDTOTAL = .Rows(iLoop)("GRANDTOTAL")
                        dsRekap.ITEM_GROUP = .Rows(iLoop)("ITEM_GROUP")
                        dsRekap.ITEM = .Rows(iLoop)("ITEM")
                        dsRekap.ITEM_GROUP = .Rows(iLoop)("ITEM_GROUP")
                        dsRekap.QTY = .Rows(iLoop)("QTY")
                        dsRekap.GRANDTOTAL_DETAIL = .Rows(iLoop)("GRANDTOTAL_DETAIL")

                        listTranskasi.Add(dsRekap)
                    End With
                Next

                SQL = " SELECT  "
                SQL &= " A.KDCASHIN "
                SQL &= " ,CATEGORY = '" & dsDaftar.CATEGORY & "' "
                SQL &= " ,KDPENDAFTARAN = '" & dsDaftar.KDPENDAFTARAN & "' "
                SQL &= " ,TUJUAN = '" & dsDaftar.M_DEPARTMENT.NAME_DISPLAY & "' "
                SQL &= " ,DPJP = '" & dsDaftar.M_DOCTOR.NAME_DISPLAY & "' "
                SQL &= " ,KDCUSTOMER = '" & dsDaftar.KDCUSTOMER & "' "
                SQL &= " ,PASIEN = '" & dsDaftar.M_CUSTOMER.NAME_DISPLAY & "' "
                SQL &= " ,ALAMAT = '" & dsDaftar.S_PENDAFTARAN_KUNJUNGANs.FirstOrDefault.ALAMAT & "' "
                SQL &= " ,KELAS = '" & dsDaftar.M_KELASRAWAT.MEMO & "' "
                SQL &= " ,TANGGAL_DATANG = '" & dsDaftar.DATE & "' "
                SQL &= " ,TANGGAL_PULANG = '" & dsDaftar.DATE & "' "
                SQL &= " ,A.SUBTOTAL "
                SQL &= " ,A.COSTSHARE "
                SQL &= " ,A.DEPOSIT "
                SQL &= " ,A.ADMIN "
                SQL &= " ,A.ROUND "
                SQL &= " ,A.GRANDTOTAL "
                SQL &= " ,ITEM_GROUP = F.MEMO "
                SQL &= " ,ITEM = 'BIAYA RADIOLOGI' "
                SQL &= " ,QTY = SUM(D.QTY) "
                SQL &= " ,GRANDTOTAL_DETAIL = SUM(D.GRANDTOTAL)  "
                SQL &= " FROM F_CASHIN_H A  "
                SQL &= " INNER JOIN F_CASHIN_D B "
                SQL &= " ON A.KDCASHIN = B.KDCASHIN "
                SQL &= " INNER JOIN S_SO_TRANSAKSI_H C  "
                SQL &= " ON B.NOINVOICE = C.KDSOTRANSAKSI "
                SQL &= " INNER JOIN S_SO_TRANSAKSI_D D "
                SQL &= " ON C.KDSOTRANSAKSI = D.KDSOTRANSAKSI "
                SQL &= " INNER JOIN M_ITEM E "
                SQL &= " ON D.KDITEM = E.KDITEM "
                SQL &= " INNER JOIN M_ITEM_L2 F "
                SQL &= " ON E.KDITEM_L2 = F.KDITEM_L2  "
                SQL &= " WHERE "
                SQL &= " A.KDCASHIN = '" & sKDCASIN & "' "
                SQL &= " AND F.KDITEM_L2 = 'ITEM_L2_0000000003' "
                SQL &= "GROUP BY "
                SQL &= " A.KDCASHIN "
                SQL &= " ,F.MEMO "
                SQL &= " ,A.SUBTOTAL "
                SQL &= " ,A.COSTSHARE "
                SQL &= " ,A.DEPOSIT "
                SQL &= " ,A.ADMIN "
                SQL &= " ,A.ROUND "
                SQL &= " ,A.GRANDTOTAL "
                SQL &= " ,A.DATE"


                oComm.Connection = oConn
                oComm.CommandText = SQL
                oComm.CommandTimeout = 120
                oComm.CommandType = CommandType.Text

                da = New SqlDataAdapter(oComm)
                da.Fill(ds, "ALL3")

                For iLoop As Integer = 0 To ds.Tables("ALL3").Rows.Count - 1
                    Dim dsRekap As New DataAccess.R_CASHIN

                    With ds.Tables("ALL3")
                        dsRekap.KDCASHIN = .Rows(iLoop)("KDCASHIN")
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
                        dsRekap.NOINVOICE = dsDaftar.M_CUSTOMER.M_PENJAMIN.MEMO
                        dsRekap.SUBTOTAL = .Rows(iLoop)("SUBTOTAL")
                        dsRekap.COSTSHARE = .Rows(iLoop)("COSTSHARE")
                        dsRekap.DEPOSIT = .Rows(iLoop)("DEPOSIT")
                        dsRekap.ADMIN = .Rows(iLoop)("ADMIN")
                        dsRekap.ROUND = .Rows(iLoop)("ROUND")
                        dsRekap.GRANDTOTAL = .Rows(iLoop)("GRANDTOTAL")
                        dsRekap.ITEM_GROUP = .Rows(iLoop)("ITEM_GROUP")
                        dsRekap.ITEM = .Rows(iLoop)("ITEM")
                        dsRekap.ITEM_GROUP = .Rows(iLoop)("ITEM_GROUP")
                        dsRekap.QTY = .Rows(iLoop)("QTY")
                        dsRekap.GRANDTOTAL_DETAIL = .Rows(iLoop)("GRANDTOTAL_DETAIL")

                        listTranskasi.Add(dsRekap)
                    End With
                Next

                SQL = " SELECT  "
                SQL &= " A.KDCASHIN "
                SQL &= " ,CATEGORY = '" & dsDaftar.CATEGORY & "' "
                SQL &= " ,KDPENDAFTARAN = '" & dsDaftar.KDPENDAFTARAN & "' "
                SQL &= " ,TUJUAN = '" & dsDaftar.M_DEPARTMENT.NAME_DISPLAY & "' "
                SQL &= " ,DPJP = '" & dsDaftar.M_DOCTOR.NAME_DISPLAY & "' "
                SQL &= " ,KDCUSTOMER = '" & dsDaftar.KDCUSTOMER & "' "
                SQL &= " ,PASIEN = '" & dsDaftar.M_CUSTOMER.NAME_DISPLAY & "' "
                SQL &= " ,ALAMAT = '" & dsDaftar.S_PENDAFTARAN_KUNJUNGANs.FirstOrDefault.ALAMAT & "' "
                SQL &= " ,KELAS = '" & dsDaftar.M_KELASRAWAT.MEMO & "' "
                SQL &= " ,TANGGAL_DATANG = '" & dsDaftar.DATE & "' "
                SQL &= " ,TANGGAL_PULANG = '" & dsDaftar.DATE & "' "
                SQL &= " ,A.SUBTOTAL "
                SQL &= " ,A.COSTSHARE "
                SQL &= " ,A.DEPOSIT "
                SQL &= " ,A.ADMIN "
                SQL &= " ,A.ROUND "
                SQL &= " ,A.GRANDTOTAL "
                SQL &= " ,ITEM_GROUP = F.MEMO "
                SQL &= " ,ITEM = 'BIAYA FARMASI' "
                SQL &= " ,QTY = SUM(D.QTY) "
                SQL &= " ,GRANDTOTAL_DETAIL = SUM(D.GRANDTOTAL)  "
                SQL &= " FROM F_CASHIN_H A  "
                SQL &= " INNER JOIN F_CASHIN_D B "
                SQL &= " ON A.KDCASHIN = B.KDCASHIN "
                SQL &= " INNER JOIN S_SO_TRANSAKSI_H C  "
                SQL &= " ON B.NOINVOICE = C.KDSOTRANSAKSI "
                SQL &= " INNER JOIN S_SO_TRANSAKSI_D D "
                SQL &= " ON C.KDSOTRANSAKSI = D.KDSOTRANSAKSI "
                SQL &= " INNER JOIN M_ITEM E "
                SQL &= " ON D.KDITEM = E.KDITEM "
                SQL &= " INNER JOIN M_ITEM_L2 F "
                SQL &= " ON E.KDITEM_L2 = F.KDITEM_L2  "
                SQL &= " WHERE "
                SQL &= " A.KDCASHIN = '" & sKDCASIN & "' "
                SQL &= " AND F.KDITEM_L2 = 'ITEM_L2_0000000016' "
                SQL &= "GROUP BY "
                SQL &= " A.KDCASHIN "
                SQL &= " ,F.MEMO "
                SQL &= " ,A.SUBTOTAL "
                SQL &= " ,A.COSTSHARE "
                SQL &= " ,A.DEPOSIT "
                SQL &= " ,A.ADMIN "
                SQL &= " ,A.ROUND "
                SQL &= " ,A.GRANDTOTAL "
                SQL &= " ,A.DATE "

                oComm.Connection = oConn
                oComm.CommandText = SQL
                oComm.CommandTimeout = 120
                oComm.CommandType = CommandType.Text

                da = New SqlDataAdapter(oComm)
                da.Fill(ds, "ALL1")

                For iLoop As Integer = 0 To ds.Tables("ALL1").Rows.Count - 1
                    Dim dsRekap As New DataAccess.R_CASHIN

                    With ds.Tables("ALL1")
                        dsRekap.KDCASHIN = .Rows(iLoop)("KDCASHIN")
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
                        dsRekap.NOINVOICE = dsDaftar.M_CUSTOMER.M_PENJAMIN.MEMO
                        dsRekap.SUBTOTAL = .Rows(iLoop)("SUBTOTAL")
                        dsRekap.COSTSHARE = .Rows(iLoop)("COSTSHARE")
                        dsRekap.DEPOSIT = .Rows(iLoop)("DEPOSIT")
                        dsRekap.ADMIN = .Rows(iLoop)("ADMIN")
                        dsRekap.ROUND = .Rows(iLoop)("ROUND")
                        dsRekap.GRANDTOTAL = .Rows(iLoop)("GRANDTOTAL")
                        dsRekap.ITEM_GROUP = .Rows(iLoop)("ITEM_GROUP")
                        dsRekap.ITEM = .Rows(iLoop)("ITEM")
                        dsRekap.ITEM_GROUP = .Rows(iLoop)("ITEM_GROUP")
                        dsRekap.QTY = .Rows(iLoop)("QTY")
                        dsRekap.GRANDTOTAL_DETAIL = .Rows(iLoop)("GRANDTOTAL_DETAIL")

                        listTranskasi.Add(dsRekap)
                    End With
                Next

            End If

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

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
    Private Sub OnValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs)

    End Sub
    Private Sub grdDetailRincian_DoubleClick(sender As Object, e As EventArgs) Handles grdDetailRincian.DoubleClick
        If grvDeatilRincian.GetFocusedRowCellValue("KDSOTRANSAKSI") Is Nothing Then
            Exit Sub
        End If
        Dim oSales As New Sales.clsSalesOrderTransaksi
        Dim dsSales = oSales.GetData(grvDeatilRincian.GetFocusedRowCellValue("KDSOTRANSAKSI"))

        If CBool(dsSales.PAYAMOUNT <> 0) = True Then
            MsgBox("Sudah Bayar", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        Dim frmSalesOrderTransaksi As New frmSalesOrderTransaksi
        Try
            frmSalesOrderTransaksi.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsSales.CATEGORY, dsSales.KDSOTRANSAKSI)
            frmSalesOrderTransaksi.ShowDialog(Me)
            fn_LoadNOINVOICE()
            fn_LoadNOINVOICE_R()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub tabControl_Click(sender As Object, e As EventArgs) Handles tabControl.Click
        If grdKDPENDAFTARAN.Text = String.Empty Then Exit Sub

        If tabControl.SelectedTabPageIndex = 1 Then

        ElseIf tabControl.SelectedTabPageIndex = 2 Then
            grvDeatilRincian.OptionsSelection.MultiSelect = True
            grvDeatilRincian.SelectAll()
            grvDeatilRincian.DeleteSelectedRows()
            grvDeatilRincian.OptionsSelection.MultiSelect = False

            For i As Integer = 0 To grvDetail.RowCount - 2
                fn_LoadDataRincian(grvDetail.GetRowCellValue(i, colNOINVOICE))
            Next

        ElseIf tabControl.SelectedTabPageIndex = 3 Then
            grvDetailObat.OptionsSelection.MultiSelect = True
            grvDetailObat.SelectAll()
            grvDetailObat.DeleteSelectedRows()
            grvDetailObat.OptionsSelection.MultiSelect = False

            For i As Integer = 0 To grvDetail.RowCount - 2
                fn_LoadDataObat(grvDetail.GetRowCellValue(i, colNOINVOICE))
            Next

        ElseIf tabControl.SelectedTabPageIndex = 4 Then
            grvDetailItem.OptionsSelection.MultiSelect = True
            grvDetailItem.SelectAll()
            grvDetailItem.DeleteSelectedRows()
            grvDetailItem.OptionsSelection.MultiSelect = False

            Dim oDaftar As New Admission.clsPendaftaran
            Dim dsDaftar = oDaftar.GetData(grdKDPENDAFTARAN.EditValue)
            If dsDaftar IsNot Nothing Then
                fn_LoadDataItem(dsDaftar.KDPENDAFTARAN, dsDaftar.KDPENDAFTARAN_AWAL)
            End If
        ElseIf tabControl.SelectedTabPageIndex = 5 Then
            fn_LoadHistoryPasien(oCashIn.GetDataBypendaftarankdpendaftaran(grdKDPENDAFTARAN.EditValue).KDCUSTOMER)
        End If

    End Sub
    Private Sub grdDetailObat_DoubleClick(sender As Object, e As EventArgs) Handles grdDetailObat.DoubleClick
        If grvDetailObat.GetFocusedRowCellValue("KDSOTRANSAKSI") Is Nothing Then
            Exit Sub
        End If
        Dim oSales As New Sales.clsSalesOrderTransaksi
        Dim dsSales = oSales.GetData(grvDetailObat.GetFocusedRowCellValue("KDSOTRANSAKSI"))

        If CBool(dsSales.PAYAMOUNT <> 0) = True Then
            MsgBox("Sudah Bayar", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        Dim frmSalesOrderTransaksi As New frmSalesOrderTransaksi
        Try
            frmSalesOrderTransaksi.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsSales.CATEGORY, dsSales.KDSOTRANSAKSI)
            frmSalesOrderTransaksi.ShowDialog(Me)
            fn_LoadNOINVOICE()
            fn_LoadNOINVOICE_R()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try

    End Sub
    Private Sub MutasiPasienToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MutasiPasienToolStripMenuItem.Click
        If grvHistoryPasien.GetFocusedRowCellValue("NomorPendaftaran") Is Nothing Then
            Exit Sub
        End If
        Dim oPendaftaran As New Admission.clsPendaftaran
        Dim frmPendaftaran_Kunjungan As New frmPendaftaran_Kunjungan
        Try
            frmPendaftaran_Kunjungan.LoadMe(FORM_MODE.FORM_MODE_ADD, grvHistoryPasien.GetFocusedRowCellValue("NomorPendaftaran"))
            frmPendaftaran_Kunjungan.ShowDialog(Me)

            fn_LoadHistoryPasien(oCashIn.GetDataBypendaftarankdpendaftaran(grdKDPENDAFTARAN.EditValue).KDCUSTOMER)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)

        End Try
    End Sub
    Private Sub EditMutasiPasienToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditMutasiPasienToolStripMenuItem.Click
        If grvHistoryPasien.GetFocusedRowCellValue("KDKUNJUNGAN") Is Nothing Then
            Exit Sub
        End If
        Dim frmPendaftaran_Kunjungan As New frmPendaftaran_Kunjungan
        Try
            frmPendaftaran_Kunjungan.LoadMe(FORM_MODE.FORM_MODE_EDIT, "", grvHistoryPasien.GetFocusedRowCellValue("KDKUNJUNGAN"))
            frmPendaftaran_Kunjungan.ShowDialog(Me)
            fn_LoadHistoryPasien(oCashIn.GetDataBypendaftarankdpendaftaran(grdKDPENDAFTARAN.EditValue).KDCUSTOMER)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class