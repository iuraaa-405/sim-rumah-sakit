Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Imports DevExpress.XtraPrinting

Imports System.Data
Imports System.Data.SqlClient

Imports Microsoft.VisualBasic.PowerPacks.Printing.Compatibility.VB6


Public Class frmReportDailyIncome
    Implements ILanguage

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "Daily Income"

        deDATEFrom.DateTime = Now
        deDATETo.DateTime = Now

        lDATEFROM.Text = Report.FILTER_DATEFROM
        lDATETO.Text = Report.FILTER_DATETO

        fn_LoadSecurity()
    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try

        Catch oErr As Exception

        End Try
    End Sub
    Public Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            Me.Text = "Daily Income"

            lDATEFROM.Text = Report.FILTER_DATEFROM
            lDATETO.Text = Report.FILTER_DATETO
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadSecurity()
        Try
            Dim oOtority As New Setting.clsOtority
            Dim oUser As New Setting.clsUser

            Dim ds = (From x In oOtority.GetDataDetail _
                     Join y In oUser.GetData _
                     On x.KDOTORITY Equals y.KDOTORITY _
                     Where x.MODUL = "ADMIN" _
                     And y.KDUSER = sUserID _
                     Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                If ds.ISVIEW = True Then
                    deDATEFrom.Properties.ReadOnly = False
                    deDATETo.Properties.ReadOnly = False

                    fn_Preview()
                Else
                    deDATEFrom.Properties.ReadOnly = True
                    deDATETo.Properties.ReadOnly = True

                    deDATEFrom.DateTime = Now
                    deDATETo.DateTime = Now

                    fn_Preview()
                End If
            Catch oErr As Exception
                deDATEFrom.Properties.ReadOnly = True
                deDATETo.Properties.ReadOnly = True

                deDATEFrom.DateTime = Now
                deDATETo.DateTime = Now

                fn_Preview()
            End Try
        Catch oErr As Exception
            deDATEFrom.Properties.ReadOnly = True
            deDATETo.Properties.ReadOnly = True

            deDATEFrom.DateTime = Now
            deDATETo.DateTime = Now

            fn_Preview()
        End Try
    End Sub
    Private Sub fn_Print()

        fn_PrintStruk()

        '        Try
        '            printableComponentLink.Landscape = True
        '            printableComponentLink.PaperKind = Printing.PaperKind.A4

        '            Dim phf As PageHeaderFooter = _
        '        TryCast(printableComponentLink.PageHeaderFooter, PageHeaderFooter)
        '            phf.Header.Content.Clear()
        '            phf.Header.Font = New Font("Times New Roman", 14, FontStyle.Bold)
        '            phf.Header.LineAlignment = BrickAlignment.Center
        '            phf.Footer.Font = New Font("Times New Roman", 9.75)
        '            phf.Footer.LineAlignment = BrickAlignment.Far
        '            phf.Footer.Content.AddRange(New String() _
        '        {"", "", Report.REPORT_PAGE & " : [Page # of Pages #]"})

        '            phf.Header.Content.AddRange(New String() _
        '{"", "Daily Income" & vbCrLf & deDATEFrom.DateTime.ToString("dd/MM/yyyy") & " - " & deDATETo.DateTime.ToString("dd/MM/yyyy"), ""})

        '            printableComponentLink.Component = grd
        '            printableComponentLink.CreateDocument()
        '            printableComponentLink.ShowPreviewDialog(Me)
        '        Catch oErr As Exception
        '            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        '        End Try
    End Sub
    Private Sub fn_Preview()
        Try
            grv.Columns.Clear()
            grd.DataSource = Nothing
            grv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            grv1.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            fn_LoadDataAll()

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

#Region "All"
    Private Sub fn_LoadDataAll()
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

            If sCompany = "INDIGO" Then
                SQL = "WITH SAMPLE AS ( "
                SQL &= "SELECT CAST('" & deDATEFrom.DateTime.ToString("yyyy-MM-dd") & "' AS DATETIME) AS DT "
                SQL &= "UNION ALL "
                SQL &= "SELECT DATEADD(dd, 1, DT) "
                SQL &= "FROM SAMPLE S "
                SQL &= "WHERE DATEADD(dd, 1, DT) <= CAST('" & deDATETo.DateTime.ToString("yyyy-MM-dd") & "' AS DATETIME) "
                SQL &= ") "

                SQL &= "SELECT A.*, TOTAL_PEMBAYARAN = CASH + DEBIT_BCA + DEBIT_MANDIRI + VISA_MASTER_BCA + VISA_MASTER_MANDIRI + JCB_DEBIT + KREDIT_MANDIRI, BELUM_BAYAR = A.PENJUALAN - (CASH + DEBIT_BCA + DEBIT_MANDIRI + VISA_MASTER_BCA + VISA_MASTER_MANDIRI + JCB_DEBIT + KREDIT_MANDIRI) "
                SQL &= "FROM ( "
                SQL &= "SELECT "
                SQL &= "TANGGAL = A.DT "
                SQL &= ",PENJUALAN_BARANG = ISNULL((SELECT DISTINCT SUM(BB.QTY) FROM S_SI_H AS AA INNER JOIN S_SI_D AS BB ON AA.KDSI = BB.KDSI WHERE CONVERT(VARCHAR(8), AA.DATE, 112) = CONVERT(VARCHAR(8), A.DT, 112) AND AA.MEMO = 'DIRECT SALES'), 0) "
                SQL &= ",PENJUALAN = ISNULL((SELECT DISTINCT SUM(AA.GRANDTOTAL) FROM S_SI_H AS AA WHERE CONVERT(VARCHAR(8), AA.DATE, 112) = CONVERT(VARCHAR(8), A.DT, 112) AND AA.MEMO = 'DIRECT SALES'), 0) "
                SQL &= ",CASH = ISNULL((SELECT DISTINCT SUM(BB.AMOUNTPAYMENT) FROM F_CASHIN_H AS AA INNER JOIN F_CASHIN_D AS BB ON AA.KDCASHIN = BB.KDCASHIN INNER JOIN S_SI_H AS CC ON BB.NOINVOICE = CC.KDSI WHERE CONVERT(VARCHAR(8), AA.DATE, 112) = CONVERT(VARCHAR(8), A.DT, 112) AND AA.KDPAYMENTTYPE = 'P-0001' AND CC.MEMO = 'DIRECT SALES'), 0) "
                SQL &= ",DEBIT_BCA = ISNULL((SELECT DISTINCT SUM(BB.AMOUNTPAYMENT) FROM F_CASHIN_H AS AA INNER JOIN F_CASHIN_D AS BB ON AA.KDCASHIN = BB.KDCASHIN INNER JOIN S_SI_H AS CC ON BB.NOINVOICE = CC.KDSI WHERE CONVERT(VARCHAR(8), AA.DATE, 112) = CONVERT(VARCHAR(8), A.DT, 112) AND AA.KDPAYMENTTYPE = 'P-0003' AND CC.MEMO = 'DIRECT SALES'), 0) "
                SQL &= ",DEBIT_MANDIRI = ISNULL((SELECT DISTINCT SUM(BB.AMOUNTPAYMENT) FROM F_CASHIN_H AS AA INNER JOIN F_CASHIN_D AS BB ON AA.KDCASHIN = BB.KDCASHIN INNER JOIN S_SI_H AS CC ON BB.NOINVOICE = CC.KDSI WHERE CONVERT(VARCHAR(8), AA.DATE, 112) = CONVERT(VARCHAR(8), A.DT, 112) AND AA.KDPAYMENTTYPE = 'P-0005' AND CC.MEMO = 'DIRECT SALES'), 0) "
                SQL &= ",VISA_MASTER_BCA = ISNULL((SELECT DISTINCT SUM(BB.AMOUNTPAYMENT) FROM F_CASHIN_H AS AA INNER JOIN F_CASHIN_D AS BB ON AA.KDCASHIN = BB.KDCASHIN INNER JOIN S_SI_H AS CC ON BB.NOINVOICE = CC.KDSI WHERE CONVERT(VARCHAR(8), AA.DATE, 112) = CONVERT(VARCHAR(8), A.DT, 112) AND AA.KDPAYMENTTYPE = 'P-0002' AND CC.MEMO = 'DIRECT SALES'), 0) "
                SQL &= ",VISA_MASTER_MANDIRI = ISNULL((SELECT DISTINCT SUM(BB.AMOUNTPAYMENT) FROM F_CASHIN_H AS AA INNER JOIN F_CASHIN_D AS BB ON AA.KDCASHIN = BB.KDCASHIN INNER JOIN S_SI_H AS CC ON BB.NOINVOICE = CC.KDSI WHERE CONVERT(VARCHAR(8), AA.DATE, 112) = CONVERT(VARCHAR(8), A.DT, 112) AND AA.KDPAYMENTTYPE = 'P-0004' AND CC.MEMO = 'DIRECT SALES'), 0) "
                SQL &= ",JCB_DEBIT = ISNULL((SELECT DISTINCT SUM(BB.AMOUNTPAYMENT) FROM F_CASHIN_H AS AA INNER JOIN F_CASHIN_D AS BB ON AA.KDCASHIN = BB.KDCASHIN INNER JOIN S_SI_H AS CC ON BB.NOINVOICE = CC.KDSI WHERE CONVERT(VARCHAR(8), AA.DATE, 112) = CONVERT(VARCHAR(8), A.DT, 112) AND AA.KDPAYMENTTYPE = 'P-0008' AND CC.MEMO = 'DIRECT SALES'), 0) "
                SQL &= ",KREDIT_MANDIRI = ISNULL((SELECT DISTINCT SUM(BB.AMOUNTPAYMENT) FROM F_CASHIN_H AS AA INNER JOIN F_CASHIN_D AS BB ON AA.KDCASHIN = BB.KDCASHIN INNER JOIN S_SI_H AS CC ON BB.NOINVOICE = CC.KDSI WHERE CONVERT(VARCHAR(8), AA.DATE, 112) = CONVERT(VARCHAR(8), A.DT, 112) AND AA.KDPAYMENTTYPE = 'P-0009' AND CC.MEMO = 'DIRECT SALES'), 0) "
                SQL &= "FROM SAMPLE AS A "
                SQL &= "WHERE CONVERT(VARCHAR(8), A.DT, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DT, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
                SQL &= ") AS A "
                SQL &= "ORDER BY TANGGAL ASC "
            Else
                SQL = "WITH SAMPLE AS ( "
                SQL &= "SELECT CAST('" & deDATEFrom.DateTime.ToString("yyyy-MM-dd") & "' AS DATETIME) AS DT "
                SQL &= "UNION ALL "
                SQL &= "SELECT DATEADD(dd, 1, DT) "
                SQL &= "FROM SAMPLE S "
                SQL &= "WHERE DATEADD(dd, 1, DT) <= CAST('" & deDATETo.DateTime.ToString("yyyy-MM-dd") & "' AS DATETIME) "
                SQL &= ") "

                SQL &= "SELECT A.*, BELUM_BAYAR = A.PENJUALAN - A.TOTAL_PEMBAYARAN "
                SQL &= "FROM ( "
                SQL &= "SELECT "
                SQL &= "TANGGAL = A.DT "
                SQL &= ",PENJUALAN_BARANG = ISNULL((SELECT DISTINCT SUM(BB.QTY) FROM S_SI_H AS AA INNER JOIN S_SI_D AS BB ON AA.KDSI = BB.KDSI WHERE CONVERT(VARCHAR(8), AA.DATE, 112) = CONVERT(VARCHAR(8), A.DT, 112) AND AA.MEMO = 'DIRECT SALES'), 0) "
                SQL &= ",PENJUALAN = ISNULL((SELECT DISTINCT SUM(AA.GRANDTOTAL) FROM S_SI_H AS AA WHERE CONVERT(VARCHAR(8), AA.DATE, 112) = CONVERT(VARCHAR(8), A.DT, 112) AND AA.MEMO = 'DIRECT SALES'), 0) "
                SQL &= ",CASH = ISNULL((SELECT DISTINCT SUM(BB.AMOUNTPAYMENT) FROM F_CASHIN_H AS AA INNER JOIN F_CASHIN_D AS BB ON AA.KDCASHIN = BB.KDCASHIN INNER JOIN S_SI_H AS CC ON BB.NOINVOICE = CC.KDSI WHERE CONVERT(VARCHAR(8), AA.DATE, 112) = CONVERT(VARCHAR(8), A.DT, 112) AND AA.KDPAYMENTTYPE = 'PAYMENTTYPE_0000000001' AND CC.MEMO = 'DIRECT SALES'), 0) "
                SQL &= ",DEBIT_BCA = ISNULL((SELECT DISTINCT SUM(BB.AMOUNTPAYMENT) FROM F_CASHIN_H AS AA INNER JOIN F_CASHIN_D AS BB ON AA.KDCASHIN = BB.KDCASHIN INNER JOIN S_SI_H AS CC ON BB.NOINVOICE = CC.KDSI WHERE CONVERT(VARCHAR(8), AA.DATE, 112) = CONVERT(VARCHAR(8), A.DT, 112) AND AA.KDPAYMENTTYPE = 'PAYMENTTYPE_0000000002' AND CC.MEMO = 'DIRECT SALES'), 0) "
                SQL &= ",DEBIT_MANDIRI = ISNULL((SELECT DISTINCT SUM(BB.AMOUNTPAYMENT) FROM F_CASHIN_H AS AA INNER JOIN F_CASHIN_D AS BB ON AA.KDCASHIN = BB.KDCASHIN INNER JOIN S_SI_H AS CC ON BB.NOINVOICE = CC.KDSI WHERE CONVERT(VARCHAR(8), AA.DATE, 112) = CONVERT(VARCHAR(8), A.DT, 112) AND AA.KDPAYMENTTYPE = 'PAYMENTTYPE_0000000004' AND CC.MEMO = 'DIRECT SALES'), 0) "
                SQL &= ",VISA_MASTER = ISNULL((SELECT DISTINCT SUM(BB.AMOUNTPAYMENT) FROM F_CASHIN_H AS AA INNER JOIN F_CASHIN_D AS BB ON AA.KDCASHIN = BB.KDCASHIN INNER JOIN S_SI_H AS CC ON BB.NOINVOICE = CC.KDSI WHERE CONVERT(VARCHAR(8), AA.DATE, 112) = CONVERT(VARCHAR(8), A.DT, 112) AND AA.KDPAYMENTTYPE = 'PAYMENTTYPE_0000000003' AND CC.MEMO = 'DIRECT SALES'), 0) "
                SQL &= ",VOUCHER = ISNULL((SELECT DISTINCT SUM(BB.AMOUNTPAYMENT) FROM F_CASHIN_H AS AA INNER JOIN F_CASHIN_D AS BB ON AA.KDCASHIN = BB.KDCASHIN INNER JOIN S_SI_H AS CC ON BB.NOINVOICE = CC.KDSI WHERE CONVERT(VARCHAR(8), AA.DATE, 112) = CONVERT(VARCHAR(8), A.DT, 112) AND AA.KDPAYMENTTYPE = 'PAYMENTTYPE_0000000006' AND CC.MEMO = 'DIRECT SALES'), 0) "
                SQL &= ",TOTAL_PEMBAYARAN = ISNULL((SELECT SUM(BB.AMOUNTPAYMENT) FROM F_CASHIN_H AS AA INNER JOIN F_CASHIN_D AS BB ON AA.KDCASHIN = BB.KDCASHIN INNER JOIN S_SI_H AS CC ON BB.NOINVOICE = CC.KDSI WHERE CONVERT(VARCHAR(8), AA.DATE, 112) = CONVERT(VARCHAR(8), A.DT, 112) AND AA.KDPAYMENTTYPE = 'PAYMENTTYPE_0000000001' AND CC.MEMO = 'DIRECT SALES'), 0) + ISNULL((SELECT SUM(BB.AMOUNTPAYMENT) FROM F_CASHIN_H AS AA INNER JOIN F_CASHIN_D AS BB ON AA.KDCASHIN = BB.KDCASHIN INNER JOIN S_SI_H AS CC ON BB.NOINVOICE = CC.KDSI WHERE CONVERT(VARCHAR(8), AA.DATE, 112) = CONVERT(VARCHAR(8), A.DT, 112) AND AA.KDPAYMENTTYPE = 'PAYMENTTYPE_0000000002' AND CC.MEMO = 'DIRECT SALES'), 0) + ISNULL((SELECT SUM(BB.AMOUNTPAYMENT) FROM F_CASHIN_H AS AA INNER JOIN F_CASHIN_D AS BB ON AA.KDCASHIN = BB.KDCASHIN INNER JOIN S_SI_H AS CC ON BB.NOINVOICE = CC.KDSI WHERE CONVERT(VARCHAR(8), AA.DATE, 112) = CONVERT(VARCHAR(8), A.DT, 112) AND AA.KDPAYMENTTYPE = 'PAYMENTTYPE_0000000004' AND CC.MEMO = 'DIRECT SALES'), 0) + ISNULL((SELECT SUM(BB.AMOUNTPAYMENT) FROM F_CASHIN_H AS AA INNER JOIN F_CASHIN_D AS BB ON AA.KDCASHIN = BB.KDCASHIN INNER JOIN S_SI_H AS CC ON BB.NOINVOICE = CC.KDSI WHERE CONVERT(VARCHAR(8), AA.DATE, 112) = CONVERT(VARCHAR(8), A.DT, 112) AND AA.KDPAYMENTTYPE = 'PAYMENTTYPE_0000000003' AND CC.MEMO = 'DIRECT SALES'), 0) + ISNULL((SELECT SUM(BB.AMOUNTPAYMENT) FROM F_CASHIN_H AS AA INNER JOIN F_CASHIN_D AS BB ON AA.KDCASHIN = BB.KDCASHIN INNER JOIN S_SI_H AS CC ON BB.NOINVOICE = CC.KDSI WHERE CONVERT(VARCHAR(8), AA.DATE, 112) = CONVERT(VARCHAR(8), A.DT, 112) AND AA.KDPAYMENTTYPE = 'PAYMENTTYPE_0000000006' AND CC.MEMO = 'DIRECT SALES'), 0) "
                SQL &= "FROM SAMPLE AS A "
                SQL &= "WHERE CONVERT(VARCHAR(8), A.DT, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DT, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
                SQL &= ") AS A "
                SQL &= "ORDER BY TANGGAL ASC "
            End If

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

            grd.MainView = grv
            grd.DataSource = ds.Tables("ALL")
            grd.ForceInitialize()

            fn_LoadFormatDataAll()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatDataAll()
        For iLoop As Integer = 0 To grv.Columns.Count - 1
            If grv.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

                grv.Columns(iLoop).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                grv.Columns(iLoop).SummaryItem.DisplayFormat = "{0:n2}"
            ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next
    End Sub
    Private Function fn_PrintStruk() As Boolean
        'Try
        '    If sCompany = "INDIGO" Then
        '        Dim Printer As New Printer

        '        Printer.FontName = "Lucida Console"
        '        Printer.FontSize = 9
        '        Printer.Print(pSpace_(sCompany, 33, 2))
        '        Printer.Print(pSpace_(sAddress, 33, 2))
        '        Printer.Print(pSpace_("---------------------------------", 33, 2))
        '        Printer.Print(pSpace_("User  : " & sUserID, 33, 3))

        '        Printer.Print(pSpace_("From Date : " & deDATEFrom.DateTime.ToString("dd/MM/yyyy"), 33, 3))
        '        Printer.Print(pSpace_("To Date   : " & deDATETo.DateTime.ToString("dd/MM/yyyy"), 33, 3))
        '        Printer.Print(pSpace_("Printed   : " & Now.ToString("dd/MM/yyyy HH:mm:ss"), 33, 3))
        '        Printer.Print(pSpace_("---------------------------------", 33, 2))
        '        Printer.Print(pSpace_("ITEM        : " & FormatNumber(grv.Columns("PENJUALAN_BARANG").SummaryText, 0), 33, 3))
        '        Printer.Print(pSpace_("PENJUALAN   : " & FormatNumber(grv.Columns("PENJUALAN").SummaryText, 0), 33, 3))
        '        Printer.Print(pSpace_("---------------------------------", 33, 2))
        '        Printer.Print(pSpace_("CASH        : " & FormatNumber(grv.Columns("CASH").SummaryText, 0), 33, 3))
        '        Printer.Print(pSpace_("DBT BCA     : " & FormatNumber(grv.Columns("DEBIT_BCA").SummaryText, 0), 33, 3))
        '        Printer.Print(pSpace_("DBT MANDIRI : " & FormatNumber(grv.Columns("DEBIT_MANDIRI").SummaryText, 0), 33, 3))
        '        Printer.Print(pSpace_("V/M BCA     : " & FormatNumber(grv.Columns("VISA_MASTER_BCA").SummaryText, 0), 33, 3))
        '        Printer.Print(pSpace_("V/M MANDIRI : " & FormatNumber(grv.Columns("VISA_MASTER_MANDIRI").SummaryText, 0), 33, 3))
        '        Printer.Print(pSpace_("DBT JCB     : " & FormatNumber(grv.Columns("JCB_DEBIT").SummaryText, 0), 33, 3))
        '        Printer.Print(pSpace_("KRT MANDIRI : " & FormatNumber(grv.Columns("KREDIT_MANDIRI").SummaryText, 0), 33, 3))
        '        Printer.Print(pSpace_("TOTAL       : " & FormatNumber(grv.Columns("TOTAL_PEMBAYARAN").SummaryText, 0), 33, 3))
        '        Printer.Print(pSpace_("---------------------------------", 33, 2))

        '        Printer.EndDoc()
        '        fn_PrintStruk = True
        '    Else
        '        Dim Printer As New Printer

        '        Printer.FontName = "Lucida Console"
        '        Printer.FontSize = 9
        '        Printer.Print(pSpace_(sCompany, 33, 2))
        '        Printer.Print(pSpace_(sAddress, 33, 2))
        '        Printer.Print(pSpace_("---------------------------------", 33, 2))
        '        Printer.Print(pSpace_("User  : " & sUserID, 33, 3))

        '        Printer.Print(pSpace_("From Date : " & deDATEFrom.DateTime.ToString("dd/MM/yyyy"), 33, 3))
        '        Printer.Print(pSpace_("To Date   : " & deDATETo.DateTime.ToString("dd/MM/yyyy"), 33, 3))
        '        Printer.Print(pSpace_("Printed   : " & Now.ToString("dd/MM/yyyy HH:mm:ss"), 33, 3))
        '        Printer.Print(pSpace_("---------------------------------", 33, 2))
        '        Printer.Print(pSpace_("ITEM        : " & FormatNumber(grv.Columns("PENJUALAN_BARANG").SummaryText, 0), 33, 3))
        '        Printer.Print(pSpace_("PENJUALAN   : " & FormatNumber(grv.Columns("PENJUALAN").SummaryText, 0), 33, 3))
        '        Printer.Print(pSpace_("---------------------------------", 33, 2))
        '        Printer.Print(pSpace_("CASH        : " & FormatNumber(grv.Columns("CASH").SummaryText, 0), 33, 3))
        '        Printer.Print(pSpace_("DBT BCA     : " & FormatNumber(grv.Columns("DEBIT_BCA").SummaryText, 0), 33, 3))
        '        Printer.Print(pSpace_("DBT MANDIRI : " & FormatNumber(grv.Columns("DEBIT_MANDIRI").SummaryText, 0), 33, 3))
        '        Printer.Print(pSpace_("VISA/MASTER : " & FormatNumber(grv.Columns("VISA_MASTER").SummaryText, 0), 33, 3))
        '        Printer.Print(pSpace_("TOTAL       : " & FormatNumber(grv.Columns("TOTAL_PEMBAYARAN").SummaryText, 0), 33, 3))
        '        Printer.Print(pSpace_("---------------------------------", 33, 2))

        '        Printer.EndDoc()
        '        fn_PrintStruk = True
        '    End If
        'Catch ex As Exception
        '    MsgBox("Load Printer : " & vbCrLf & ex.Message, MsgBoxStyle.Critical, Me.Text)
        '    fn_PrintStruk = False

        '    If Not System.IO.Directory.Exists("c:\temp\mypos_log\") Then
        '        System.IO.Directory.CreateDirectory("c:\temp\mypos_log\")
        '    End If
        '    Dim myLog As New System.IO.StreamWriter("c:\temp\mypos_log\" & Now.ToString("yyyyMMdd") & ".txt")

        '    myLog.WriteLine(Me.Text)
        '    myLog.WriteLine(ex.ToString())
        '    myLog.Close()
        'End Try
    End Function
#End Region

    Private Sub MasterColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MasterColumnChooserToolStripMenuItem.Click
        grv.ShowCustomization()
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmMember_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                Me.Close()
            Case Keys.P
                If e.Alt = True And picPrint.Enabled = True Then
                    picPrint_Click()
                End If
            Case Keys.R
                If e.Alt = True And picRefresh.Enabled = True Then
                    picRefresh_Click()
                End If
        End Select
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        Try
            fn_Print()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_LoadSecurity()
    End Sub
#End Region
End Class