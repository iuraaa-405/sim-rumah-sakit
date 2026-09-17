Imports DevExpress.XtraEditors

Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Imports Microsoft.VisualBasic.PowerPacks.Printing.Compatibility.VB6

Public Class frmPayment
    Private oSalesInvoice As New Sales.clsSalesInvoice
    Private oCashIn As New Finance.clsCashIn

    Dim dsSI_H As New S_SI_H
    Dim dsSI_D1 As New List(Of S_SI_D)

    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW

    Private GRANDTOTAL As Decimal = 0

    Private isLoad As Boolean = False

    Private Sub frmPayment_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
    End Sub

#Region "Function"
    Public Sub fn_Load(ByVal sTotal As String, ByVal sNoTransaction As String, ByVal entity As S_SI_H, ByVal entityDetail As List(Of S_SI_D), ByVal FormMode As Integer)
        Try
            oFormMode = FormMode

            Dim sPayment = 0
            Try
                sPayment = (From x In oCashIn.GetDataDetail _
                           Where x.KDCASHIN.Contains(entity.KDSI & " - CI") _
                           And x.NOINVOICE = entity.KDSI _
                           Group x By x.NOINVOICE Into Sum(x.AMOUNTPAYMENT) _
                           Select Sum).FirstOrDefault
            Catch ex As Exception
                sPayment = 0
            End Try

            txtTotal.Text = entity.GRANDTOTAL - entity.PAYAMOUNT + sPayment

            'txtTotal.Text = sTotal
            lblNoTransaction.Caption = sNoTransaction

            dsSI_H = entity
            dsSI_D1 = entityDetail

            txtGRANDTOTAL1.Focus()

            Dim oPaymentType As New Reference.clsPaymentType
            Try
                txtKDPAYMENTTYPE1.Text = oPaymentType.GetData.FirstOrDefault(Function(x) x.ISDEFAULT = True).KDPAYMENTTYPE
                txtNAME1.Text = oPaymentType.GetData.FirstOrDefault(Function(x) x.ISDEFAULT = True).MEMO
            Catch ex As Exception
                txtKDPAYMENTTYPE1.Text = oPaymentType.GetData.FirstOrDefault().KDPAYMENTTYPE
                txtNAME1.Text = oPaymentType.GetData.FirstOrDefault().MEMO
            End Try

            If FormMode = FORM_MODE.FORM_MODE_EDIT Then
                Dim dsPayment = oSalesInvoice.GetDataPayment.Where(Function(x) x.KDSI = entity.KDSI).OrderBy(Function(x) x.SEQ)

                Dim arrPayment() As ButtonEdit = {txtKDPAYMENTTYPE1, txtKDPAYMENTTYPE2, txtKDPAYMENTTYPE3, txtKDPAYMENTTYPE4, txtKDPAYMENTTYPE5}
                Dim arrPaymentName() As TextEdit = {txtNAME1, txtNAME2, txtNAME3, txtNAME4, txtNAME5}
                Dim arrGRANDTOTAL() As TextEdit = {txtGRANDTOTAL1, txtGRANDTOTAL2, txtGRANDTOTAL3, txtGRANDTOTAL4, txtGRANDTOTAL5}
                Dim arrDesc() As TextEdit = {txtMEMO1, txtMEMO2, txtMEMO3, txtMEMO4, txtMEMO5}

                For Each iLoop In dsPayment
                    Dim sSEQ = iLoop.SEQ - 1

                    arrPayment(sSEQ).Text = iLoop.KDPAYMENTTYPE
                    arrPaymentName(sSEQ).Text = iLoop.M_PAYMENTTYPE.MEMO

                    arrGRANDTOTAL(sSEQ).Text = iLoop.GRANDTOTAL
                    arrDesc(sSEQ).Text = iLoop.MEMO
                Next
            End If

            fn_LoadLanguage()
            isLoad = True
        Catch ex As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = SalesInvoice.PAYMENT_TITLE

            lGRANDTOTAL.Text = SalesInvoice.PAYMENT_GRANDTOTAL
            lPAY.Text = SalesInvoice.PAYMENT_PAY
            lCHANGE.Text = SalesInvoice.PAYMENT_CHANGE
            btnSave.Caption = My.Resources.Caption.FormSaveClose
            btnClose.Caption = My.Resources.Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function DataIsValid() As Boolean
        DataIsValid = True

        Dim sGrandTotal As Decimal = 0
        Dim arrPayment() As ButtonEdit = {txtKDPAYMENTTYPE1, txtKDPAYMENTTYPE2, txtKDPAYMENTTYPE3, txtKDPAYMENTTYPE4, txtKDPAYMENTTYPE5}
        Dim arrGRANDTOTAL() As TextEdit = {txtGRANDTOTAL1, txtGRANDTOTAL2, txtGRANDTOTAL3, txtGRANDTOTAL4, txtGRANDTOTAL5}

        For iLoop As Integer = 0 To arrPayment.Length - 1
            If arrPayment(iLoop).Text <> String.Empty Then
                sGrandTotal += CDec(arrGRANDTOTAL(iLoop).Text)
            End If
        Next

        Dim sDue As Decimal = 0
        If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            sDue = dsSI_H.GRANDTOTAL - dsSI_H.PAYAMOUNT - sGrandTotal
        Else
            Dim sPayment = (From x In oCashIn.GetDataDetail _
                           Where x.KDCASHIN.Contains(dsSI_H.KDSI & " - CI") _
                           And x.NOINVOICE = dsSI_H.KDSI _
                           Group x By x.NOINVOICE Into Sum(x.AMOUNTPAYMENT) _
                           Select Sum).FirstOrDefault

            sDue = dsSI_H.GRANDTOTAL - dsSI_H.PAYAMOUNT + sPayment - sGrandTotal
        End If

        'If sDue < 0 Then
        '    MsgBox("Pembayaran Melebihi Yang Seharusnya! Cek Kembali Pembayaran!", MsgBoxStyle.Exclamation, Me.Text)
        '    DataIsValid = False
        '    Exit Function
        'End If
    End Function
    Private Function fn_Save() As Boolean
        Dim arrPayment() As ButtonEdit = {txtKDPAYMENTTYPE1, txtKDPAYMENTTYPE2, txtKDPAYMENTTYPE3, txtKDPAYMENTTYPE4, txtKDPAYMENTTYPE5}
        Dim arrGRANDTOTAL() As TextEdit = {txtGRANDTOTAL1, txtGRANDTOTAL2, txtGRANDTOTAL3, txtGRANDTOTAL4, txtGRANDTOTAL5}
        Dim arrDesc() As TextEdit = {txtMEMO1, txtMEMO2, txtMEMO3, txtMEMO4, txtMEMO5}

        Try
            Dim arrPOS_D2 As New List(Of S_SI_PAYMENT)
            Dim oPaymentType As New Reference.clsPaymentType

            For iLoop As Integer = 0 To arrPayment.Length - 1
                If arrPayment(iLoop).Text <> String.Empty Then
                    GRANDTOTAL += CDec(arrGRANDTOTAL(iLoop).Text)

                    If GRANDTOTAL > CDec(txtTotal.Text) Then
                        Dim dsSI_D2 As New S_SI_PAYMENT With _
                   {.KDSI = dsSI_H.KDSI, _
                    .KDPAYMENTTYPE = arrPayment(iLoop).Text.ToUpper, _
                    .GRANDTOTAL = CDec(arrGRANDTOTAL(iLoop).Text) - (GRANDTOTAL - CDec(txtTotal.Text)), _
                    .MEMO = arrDesc(iLoop).Text.ToUpper, _
                    .SEQ = iLoop + 1}

                        arrPOS_D2.Add(dsSI_D2)
                    Else
                        Dim dsSI_D2 As New S_SI_PAYMENT With _
                   {.KDSI = dsSI_H.KDSI, _
                    .KDPAYMENTTYPE = arrPayment(iLoop).Text.ToUpper, _
                    .GRANDTOTAL = CDec(arrGRANDTOTAL(iLoop).Text), _
                    .MEMO = arrDesc(iLoop).Text.ToUpper, _
                    .SEQ = iLoop + 1}

                        arrPOS_D2.Add(dsSI_D2)
                    End If

                End If
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                fn_Save = oSalesInvoice.InsertData(dsSI_H, dsSI_D1, arrPOS_D2)

                Dim dsSI = oSalesInvoice.GetData.Where(Function(x) x.DATE.Year = Year(dsSI_H.DATE) And x.DATE.Month = Month(dsSI_H.DATE) And Not x.KDSI.Contains("POS")).OrderByDescending(Function(x) x.KDSI).FirstOrDefault

                If CDec(GRANDTOTAL) <> 0 Then
                    For Each iLoop In arrPOS_D2
                        fn_SaveCash(dsSI.KDSI, iLoop)
                    Next
                End If

                Try
                    If dsSI_H.MEMO = "DIRECT SALES" Then
                        fn_PrintStruk(dsSI.KDSI)
                    Else
                        Dim rpt As New xtraSalesInvoice
                        rpt.ShowPrintMarginsWarning = False
                        rpt.Watermark.Text = sWATERMARK
                        Dim dsPrint = dsSI
                        rpt.bindingSource.DataSource = dsPrint
                        Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                        printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
                    End If
                Catch ex As Exception
                    MsgBox("Print Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                fn_Save = oSalesInvoice.UpdateData(dsSI_H, dsSI_D1, arrPOS_D2)

                Dim dsSI = oSalesInvoice.GetData(dsSI_H.KDSI)

                If CDec(GRANDTOTAL) <> 0 Then
                    Try
                        Dim oCashIn As New Finance.clsCashIn
                        oCashIn.DeleteData(dsSI.KDSI & " - CI")
                    Catch ex As Exception

                    End Try

                    For Each iLoop In arrPOS_D2
                        fn_SaveCash(dsSI.KDSI, iLoop)
                    Next
                Else
                    Try
                        Dim oCashIn As New Finance.clsCashIn
                        oCashIn.DeleteData(dsSI.KDSI & " - CI")
                    Catch ex As Exception

                    End Try
                End If

                Try
                    If dsSI_H.MEMO = "DIRECT SALES" Then
                        fn_PrintStruk(dsSI.KDSI)
                    Else
                        Dim rpt As New xtraSalesInvoice
                        rpt.ShowPrintMarginsWarning = False
                        rpt.Watermark.Text = sWATERMARK
                        Dim dsPrint = dsSI
                        rpt.bindingSource.DataSource = dsPrint
                        Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                        printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
                    End If
                Catch ex As Exception
                    MsgBox("Print Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

            sSavePayment = True
        Catch ex As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)

            sSavePayment = False
        End Try
    End Function
    Private Function fn_PrintStruk(ByVal sKDSI As String) As Boolean
        Try
            Dim Printer As New Printer

            Dim dsCustomer = oSalesInvoice.GetData(sKDSI)

            Printer.FontName = "Lucida Console"
            Printer.FontSize = 9
            Printer.Print(pSpace_(sCompany, 33, 2))
            Printer.Print(pSpace_(sAddress, 33, 2))
            Printer.Print(pSpace_(sPhone, 33, 2))
            Printer.Print(pSpace_("---------------------------------", 33, 2))
            Printer.Print(pSpace_("No      : " & sKDSI, 33, 3))
            Printer.Print(pSpace_("Cashier : " & sUserID, 33, 3))

            Printer.Print(pSpace_("Date    : " & Format(dsSI_H.DATE, "dd/MM/yyyy"), 33, 3))
            Printer.Print(pSpace_("Time    : " & Format(dsSI_H.DATE, "HH:mm:ss"), 33, 3))

            If dsCustomer IsNot Nothing Then
                Printer.Print(pSpace_("Nama    : " & dsCustomer.M_CUSTOMER.NAME_DISPLAY, 33, 3))
                'Printer.Print(pSpace_("Golongan: " & dsCustomer.M_CUSTOMER.M_STAFF.M_GOLONGAN.MEMO, 33, 3))

            End If

            Printer.Print(pSpace_("---------------------------------", 33, 2))

            Dim JumlahItem As Integer = 0
            Dim TotalSaving As Integer = 0
            For Each iLoop In dsSI_D1
                Dim oItem As New Reference.clsItem

                'Printer.Print(pSpace_(oItem.GetData(iLoop.KDITEM).NMITEM1, 33, 3))
                Printer.Print(pSpace_(oItem.GetData(iLoop.KDITEM).NMITEM2, 33, 3))

                If iLoop.REMARKS <> String.Empty Then
                    If iLoop.REMARKS <> "-" Then
                        Printer.Print(pSpace_(iLoop.REMARKS, 33, 3))
                    End If
                End If
                

                Printer.Print(pSpace_(FormatNumber(CDec(iLoop.QTY), 0), 7, 1) & pSpace_(" x ", 3, 0) & pSpace_(FormatNumber(iLoop.PRICE, 0), 11, 1) & pSpace_(FormatNumber(iLoop.GRANDTOTAL, 0), 12, 1))

                JumlahItem = JumlahItem + CDec(iLoop.QTY)
                TotalSaving = TotalSaving + CDec(iLoop.DISCOUNT)
            Next

            Printer.Print(pSpace_("---------------------------------", 33, 2))

            If dsSI_H.DISCOUNT > 0 Then
                Printer.Print(pSpace_("SUB TOTAL", 11, 0) & pSpace_(FormatNumber(dsSI_H.SUBTOTAL, 0), 22, 1))
                Printer.Print(pSpace_("DISCOUNT", 11, 0) & pSpace_(FormatNumber(dsSI_H.DISCOUNT, 0), 22, 1))
            End If

            Printer.Print(pSpace_("GRAND TOTAL", 11, 0) & pSpace_(FormatNumber(txtTotal.Text, 0), 22, 1))

            Dim arrPaymentName() As TextEdit = {txtNAME1, txtNAME2, txtNAME3, txtNAME4, txtNAME5}
            Dim arrAmount() As TextEdit = {txtGRANDTOTAL1, txtGRANDTOTAL2, txtGRANDTOTAL3, txtGRANDTOTAL4, txtGRANDTOTAL5}
            Dim arrDesc() As TextEdit = {txtMEMO1, txtMEMO2, txtMEMO3, txtMEMO4, txtMEMO5}

            For iLoop As Integer = 0 To arrPaymentName.Length - 1
                If arrPaymentName(iLoop).Text <> String.Empty Then
                    Printer.Print(pSpace_(arrPaymentName(iLoop).Text, 33, 0))
                    If arrDesc(iLoop).Text <> String.Empty Then
                        Printer.Print(pSpace_(arrDesc(iLoop).Text, 33, 0))
                    End If
                    Printer.Print(pSpace_(arrAmount(iLoop).Text, 33, 1))
                End If
            Next

            Printer.Print(pSpace_("---------------------------------", 33, 2))
            Printer.Print(pSpace_("CHANGE", 11, 0) & pSpace_(FormatNumber(txtBalance.Text, 0), 22, 1))
            Printer.Print(pSpace_("---------------------------------", 33, 2))
            Printer.Print(pSpace_("TOTAL ITEM : " & FormatNumber(JumlahItem, 0), 33, 3))
            Printer.Print(pSpace_("TOTAL SAVING : " & FormatNumber(TotalSaving, 0), 33, 3))
            Printer.Print(pSpace_("---------------------------------", 33, 2))
            Printer.Print(pSpace_("TERIMA KASIH ATAS KUNJUNGAN ANDA", 33, 2))
            Printer.Print(pSpace_("BARANG YG SUDAH DIBELI", 33, 2))
            Printer.Print(pSpace_("TIDAK DAPAT DITUKAR", 33, 2))
            Printer.Print(pSpace_("ATAU DIKEMBALIKAN", 33, 2))
            Printer.Print(pSpace_("---------------------------------", 33, 2))
            'Printer.Print(pSpace_("JOIN OUR NEWSLETTER", 33, 2))
            'Printer.Print(pSpace_("DAPATKAN VOUCHER IDR 25.000", 33, 2))
            'Printer.Print(pSpace_("INFO :", 33, 2))
            'Printer.Print(pSpace_("WWW.INDIGO-ACC.COM", 33, 2))
            'Printer.Print(pSpace_("---------------------------------", 33, 2))
            'Printer.Print(pSpace_(sCompany, 33, 2))
            'Printer.Print(pSpace_("PROMO TOP SPENDER", 33, 2))
            'Printer.Print(pSpace_("PERIODE 1 - 31 JULY 2015", 33, 2))
            'Printer.Print(pSpace_("INFO WWW.INDIGO-ACC.COM", 33, 2))
            'Printer.Print(pSpace_("NO    :" & sKDSI, 33, 3))
            'Printer.Print(pSpace_("TGL   :" & Format(dsSI_H.DATE, "dd/MM/yyyy"), 33, 3))
            'Printer.Print(pSpace_("TOTAL :" & FormatNumber(txtTotal.Text, 0), 33, 3))
            'Printer.Print(pSpace_("NAMA  :", 33, 3))
            'Printer.Print(pSpace_("EMAIL :", 33, 3))
            'Printer.Print(pSpace_("---------------------------------", 33, 2))

            Printer.EndDoc()
            fn_PrintStruk = True
        Catch ex As Exception
            MsgBox("Load Printer : " & vbCrLf & ex.Message, MsgBoxStyle.Critical, Me.Text)
            fn_PrintStruk = False

            If Not System.IO.Directory.Exists("c:\temp\mypos_log\") Then
                System.IO.Directory.CreateDirectory("c:\temp\mypos_log\")
            End If
            Dim myLog As New System.IO.StreamWriter("c:\temp\mypos_log\" & Now.ToString("yyyyMMdd") & ".txt")

            myLog.WriteLine(Me.Text)
            myLog.WriteLine(ex.ToString())
            myLog.Close()
        End Try
    End Function
    Private Function fn_SaveCash(ByVal sCode As String, ByVal iLoop As S_SI_PAYMENT) As Boolean
        Try
            Dim oPaymentType As New Reference.clsPaymentType
            Dim oCashIn As New Finance.clsCashIn

            Dim ds = oCashIn.GetStructureHeader
            With ds
                .DATECREATED = dsSI_H.DATECREATED
                .DATEUPDATED = dsSI_H.DATEUPDATED
                .KDCASHIN = sCode & " - CI - " & oPaymentType.GetData(iLoop.KDPAYMENTTYPE).MEMO & " - " & iLoop.SEQ
                .DATE = dsSI_H.DATE
                .KDCUSTOMER = dsSI_H.KDCUSTOMER
                .KDPAYMENTTYPE = iLoop.KDPAYMENTTYPE
                .MEMO = "INVOICE NO. : " & sCode & " - " & oPaymentType.GetData(iLoop.KDPAYMENTTYPE).MEMO & " - " & iLoop.SEQ
                .SUBTOTAL = CDec(iLoop.GRANDTOTAL)
                .ADMIN = CDec(0)
                .ROUND = CDec(0)
                .GRANDTOTAL = CDec(iLoop.GRANDTOTAL)
                .KDUSER = sUserID
            End With

            ' ***** DETIL *****
            Dim arrDetail = oCashIn.GetStructureDetailList
            Dim dsDetail = oCashIn.GetStructureDetail
            With dsDetail
                .DATECREATED = dsSI_H.DATECREATED
                .DATEUPDATED = dsSI_H.DATEUPDATED
                .KDCASHIN = sCode & " - CI - " & oPaymentType.GetData(iLoop.KDPAYMENTTYPE).MEMO & " - " & iLoop.SEQ
                .NOINVOICE = sCode
                .AMOUNTPAYMENT = CDec(iLoop.GRANDTOTAL)
                .SEQ = i
                .REMARKS = "AUTO"
            End With
            arrDetail.Add(dsDetail)

            ' ***** DETIL *****
            Dim arrDetail_R = oCashIn.GetStructureDetailList

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_SaveCash = oCashIn.InsertData(ds, arrDetail, arrDetail_R, True)
                Catch ex As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    Dim dsCI = oCashIn.GetData(sCode & " - CI - " & oPaymentType.GetData(iLoop.KDPAYMENTTYPE).MEMO & " - " & iLoop.SEQ)

                    If dsCI IsNot Nothing Then
                        fn_SaveCash = oCashIn.UpdateData(ds, arrDetail, arrDetail_R)
                    Else
                        fn_SaveCash = oCashIn.InsertData(ds, arrDetail, arrDetail_R, True)
                    End If
                Catch ex As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveCash = False
        End Try
    End Function
    Private Sub PaymentCalculation() Handles txtGRANDTOTAL1.TextChanged, txtGRANDTOTAL2.TextChanged, txtGRANDTOTAL3.TextChanged, txtGRANDTOTAL4.TextChanged, txtGRANDTOTAL5.TextChanged
        Try
            If txtGRANDTOTAL1.Text <> String.Empty And txtGRANDTOTAL2.Text <> String.Empty And txtGRANDTOTAL3.Text <> String.Empty And txtGRANDTOTAL4.Text <> String.Empty And txtGRANDTOTAL5.Text <> String.Empty Then
                txtKDPAYMENTTYPE.Text = (CDbl(txtGRANDTOTAL1.Text) + CDbl(txtGRANDTOTAL2.Text) + CDbl(txtGRANDTOTAL3.Text) + CDbl(txtGRANDTOTAL4.Text) + CDbl(txtGRANDTOTAL5.Text)).ToString()

                txtBalance.Text = (CDbl(txtKDPAYMENTTYPE.Text) - CDbl(txtTotal.Text)).ToString()

                If CDbl(txtKDPAYMENTTYPE.Text) < CDbl(txtTotal.Text) Then
                    txtBalance.Properties.Appearance.ForeColor = Color.Red
                Else
                    txtBalance.Properties.Appearance.ForeColor = Color.Green
                End If
            End If
        Catch ex As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
#Region "Command Key"
    Private Sub btnSave_ItemClick() Handles btnSave.ItemClick
        If Not DataIsValid() Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.OkCancel, Me.Text) = MsgBoxResult.Cancel Then Exit Sub

        If Not fn_Save() Then Exit Sub

        Me.Close()
    End Sub
    Private Sub btnClose_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnClose.ItemClick
        sSavePayment = False

        Me.Close()
    End Sub
    Private Sub frmPayment_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If

        If (e.KeyCode = Keys.F1 And e.Control = True) Then
            txtKDPAYMENTTYPE1_ButtonClick()
        ElseIf (e.KeyCode = Keys.F2 And e.Control = True) Then
            txtKDPAYMENTTYPE2_ButtonClick()
        ElseIf (e.KeyCode = Keys.F3 And e.Control = True) Then
            txtKDPAYMENTTYPE3_ButtonClick()
        ElseIf (e.KeyCode = Keys.F4 And e.Control = True) Then
            txtKDPAYMENTTYPE4_ButtonClick()
        ElseIf (e.KeyCode = Keys.F5 And e.Control = True) Then
            txtKDPAYMENTTYPE5_ButtonClick()
        End If
    End Sub
    Private Sub txtKDPAYMENTTYPE1_ButtonClick() Handles txtKDPAYMENTTYPE1.ButtonClick
        frmBrowsePaymentType.fn_LoadMe(1)
        frmBrowsePaymentType.ShowDialog(Me)
        txtKDPAYMENTTYPE1.Text = sFind1

        If sFind1 <> String.Empty Then
            Dim oPayment As New Reference.clsPaymentType
            oPayment.GetData()

            txtNAME1.Text = sFind2
            txtGRANDTOTAL1.Focus()
        End If

        sFind1 = String.Empty
        sFind2 = String.Empty
    End Sub
    Private Sub txtKDPAYMENTTYPE1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtKDPAYMENTTYPE1.KeyDown
        If (e.KeyCode = Keys.F4 And e.Alt = False) Or (e.KeyCode = Keys.F1 And e.Control = True) Then
            txtKDPAYMENTTYPE1_ButtonClick()
        End If
    End Sub
    Private Sub txtKDPAYMENTTYPE2_ButtonClick() Handles txtKDPAYMENTTYPE2.ButtonClick
        frmBrowsePaymentType.fn_LoadMe(1)
        frmBrowsePaymentType.ShowDialog(Me)
        txtKDPAYMENTTYPE2.Text = sFind1

        If sFind1 <> String.Empty Then
            Dim oPayment As New Reference.clsPaymentType
            oPayment.GetData()

            txtNAME2.Text = sFind2
            txtGRANDTOTAL2.Focus()
        End If

        sFind1 = String.Empty
        sFind2 = String.Empty
    End Sub
    Private Sub txtKDPAYMENTTYPE2_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtKDPAYMENTTYPE2.KeyDown
        If (e.KeyCode = Keys.F4 And e.Alt = False) Or (e.KeyCode = Keys.F2 And e.Control = True) Then
            txtKDPAYMENTTYPE2_ButtonClick()
        End If
    End Sub
    Private Sub txtKDPAYMENTTYPE3_ButtonClick() Handles txtKDPAYMENTTYPE3.ButtonClick
        frmBrowsePaymentType.fn_LoadMe(1)
        frmBrowsePaymentType.ShowDialog(Me)
        txtKDPAYMENTTYPE3.Text = sFind1

        If sFind1 <> String.Empty Then
            Dim oPayment As New Reference.clsPaymentType
            oPayment.GetData()

            txtNAME3.Text = sFind2
            txtGRANDTOTAL3.Focus()
        End If

        sFind1 = String.Empty
        sFind2 = String.Empty
    End Sub
    Private Sub txtKDPAYMENTTYPE3_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtKDPAYMENTTYPE3.KeyDown
        If (e.KeyCode = Keys.F4 And e.Alt = False) Or (e.KeyCode = Keys.F3 And e.Control = True) Then
            txtKDPAYMENTTYPE3_ButtonClick()
        End If
    End Sub
    Private Sub txtKDPAYMENTTYPE4_ButtonClick() Handles txtKDPAYMENTTYPE4.ButtonClick
        frmBrowsePaymentType.fn_LoadMe(1)
        frmBrowsePaymentType.ShowDialog(Me)
        txtKDPAYMENTTYPE4.Text = sFind1

        If sFind1 <> String.Empty Then
            Dim oPayment As New Reference.clsPaymentType
            oPayment.GetData()

            txtNAME4.Text = sFind2
            txtGRANDTOTAL4.Focus()
        End If

        sFind1 = String.Empty
        sFind2 = String.Empty
    End Sub
    Private Sub txtKDPAYMENTTYPE4_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtKDPAYMENTTYPE4.KeyDown
        If (e.KeyCode = Keys.F4 And e.Alt = False) Or (e.KeyCode = Keys.F4 And e.Control = True) Then
            txtKDPAYMENTTYPE4_ButtonClick()
        End If
    End Sub
    Private Sub txtKDPAYMENTTYPE5_ButtonClick() Handles txtKDPAYMENTTYPE5.ButtonClick
        frmBrowsePaymentType.fn_LoadMe(1)
        frmBrowsePaymentType.ShowDialog(Me)
        txtKDPAYMENTTYPE5.Text = sFind1

        If sFind1 <> String.Empty Then
            Dim oPayment As New Reference.clsPaymentType
            oPayment.GetData()

            txtNAME5.Text = sFind2
            txtGRANDTOTAL5.Focus()
        End If

        sFind1 = String.Empty
        sFind2 = String.Empty
    End Sub
    Private Sub txtKDPAYMENTTYPE5_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtKDPAYMENTTYPE5.KeyDown
        If (e.KeyCode = Keys.F4 And e.Alt = False) Or (e.KeyCode = Keys.F5 And e.Control = True) Then
            txtKDPAYMENTTYPE5_ButtonClick()
        End If
    End Sub
    Private Sub txtMEMO1_Validated() Handles txtMEMO1.Validated
        If CDbl(txtKDPAYMENTTYPE.Text) >= CDbl(txtTotal.Text) Then
            If isLoad = True Then
                btnSave_ItemClick()
            End If
        Else
            If isLoad = True Then
                txtKDPAYMENTTYPE2_ButtonClick()

                If txtKDPAYMENTTYPE2.Text <> String.Empty Then
                    txtGRANDTOTAL2.Text = (CDbl(txtTotal.Text) - CDbl(txtGRANDTOTAL1.Text)).ToString
                End If
            End If
        End If
    End Sub
    Private Sub txtMEMO2_Validated() Handles txtMEMO2.Validated
        If CDbl(txtKDPAYMENTTYPE.Text) >= CDbl(txtTotal.Text) Then
            If isLoad = True Then
                btnSave_ItemClick()
            End If
        Else
            If isLoad = True Then
                txtKDPAYMENTTYPE3_ButtonClick()

                If txtKDPAYMENTTYPE3.Text <> String.Empty Then
                    txtGRANDTOTAL3.Text = (CDbl(txtTotal.Text) - CDbl(txtGRANDTOTAL1.Text) - CDbl(txtGRANDTOTAL2.Text)).ToString
                End If
            End If
        End If
    End Sub
    Private Sub txtMEMO3_Validated() Handles txtMEMO3.Validated
        If CDbl(txtKDPAYMENTTYPE.Text) >= CDbl(txtTotal.Text) Then
            If isLoad = True Then
                btnSave_ItemClick()
            End If
        Else
            If isLoad = True Then
                txtKDPAYMENTTYPE4_ButtonClick()

                If txtKDPAYMENTTYPE4.Text <> String.Empty Then
                    txtGRANDTOTAL4.Text = (CDbl(txtTotal.Text) - CDbl(txtGRANDTOTAL1.Text) - CDbl(txtGRANDTOTAL2.Text) - CDbl(txtGRANDTOTAL3.Text)).ToString
                End If
            End If
        End If
    End Sub
    Private Sub txtMEMO4_Validated() Handles txtMEMO4.Validated
        If CDbl(txtKDPAYMENTTYPE.Text) >= CDbl(txtTotal.Text) Then
            If isLoad = True Then
                btnSave_ItemClick()
            End If
        Else
            If isLoad = True Then
                txtKDPAYMENTTYPE5_ButtonClick()

                If txtKDPAYMENTTYPE5.Text <> String.Empty Then
                    txtGRANDTOTAL5.Text = (CDbl(txtTotal.Text) - CDbl(txtGRANDTOTAL1.Text) - CDbl(txtGRANDTOTAL2.Text) - CDbl(txtGRANDTOTAL3.Text) - CDbl(txtGRANDTOTAL4.Text)).ToString
                End If
            End If
        End If
    End Sub
    Private Sub txtMEMO5_Validated() Handles txtMEMO5.Validated
        If CDbl(txtKDPAYMENTTYPE.Text) >= CDbl(txtTotal.Text) Then
            If isLoad = True Then
                btnSave_ItemClick()
            End If
        End If
    End Sub
    Private Sub StatusControl() Handles txtKDPAYMENTTYPE1.TextChanged, txtKDPAYMENTTYPE2.TextChanged, txtKDPAYMENTTYPE3.TextChanged, txtKDPAYMENTTYPE4.TextChanged, txtKDPAYMENTTYPE5.TextChanged
        If txtKDPAYMENTTYPE1.Text <> String.Empty Then
            txtGRANDTOTAL1.Properties.ReadOnly = False
            txtMEMO1.Properties.ReadOnly = False
        Else
            txtGRANDTOTAL1.Properties.ReadOnly = True
            txtGRANDTOTAL1.Text = String.Empty
            txtMEMO1.Properties.ReadOnly = True
            txtMEMO1.Text = String.Empty
        End If

        If txtKDPAYMENTTYPE2.Text <> String.Empty Then
            txtGRANDTOTAL2.Properties.ReadOnly = False
            txtMEMO2.Properties.ReadOnly = False
        Else
            txtGRANDTOTAL2.Properties.ReadOnly = True
            txtGRANDTOTAL2.Text = String.Empty
            txtMEMO2.Properties.ReadOnly = True
            txtMEMO2.Text = String.Empty
        End If

        If txtKDPAYMENTTYPE3.Text <> String.Empty Then
            txtGRANDTOTAL3.Properties.ReadOnly = False
            txtMEMO3.Properties.ReadOnly = False
        Else
            txtGRANDTOTAL3.Properties.ReadOnly = True
            txtGRANDTOTAL3.Text = String.Empty
            txtMEMO3.Properties.ReadOnly = True
            txtMEMO3.Text = String.Empty
        End If

        If txtKDPAYMENTTYPE4.Text <> String.Empty Then
            txtGRANDTOTAL4.Properties.ReadOnly = False
            txtMEMO4.Properties.ReadOnly = False
        Else
            txtGRANDTOTAL4.Properties.ReadOnly = True
            txtGRANDTOTAL4.Text = String.Empty
            txtMEMO4.Properties.ReadOnly = True
            txtMEMO4.Text = String.Empty
        End If

        If txtKDPAYMENTTYPE5.Text <> String.Empty Then
            txtGRANDTOTAL5.Properties.ReadOnly = False
            txtMEMO5.Properties.ReadOnly = False
        Else
            txtGRANDTOTAL5.Properties.ReadOnly = True
            txtGRANDTOTAL5.Text = String.Empty
            txtMEMO5.Properties.ReadOnly = True
            txtMEMO5.Text = String.Empty
        End If
    End Sub
#End Region
End Class