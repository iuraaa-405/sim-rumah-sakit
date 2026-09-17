Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Imports DevExpress.XtraPrinting

Imports System.Data
Imports System.Data.SqlClient


Public Class frmReportSalesInvoice
    Implements ILanguage

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = SalesInvoice.TITLE_REPORT

        lTYPE.Text = Report.FILTER_TYPE
        lDATEFROM.Text = Report.FILTER_DATEFROM
        lDATETO.Text = Report.FILTER_DATETO

        cboTYPE.Properties.Items.Clear()
        cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_01)
        cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_02)
        cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_03)
        cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_04)
        cboTYPE.Properties.Items.Add("Keuntungan")
        cboTYPE.Properties.Items.Add("Pengeluaran Barang Per Hari")
        cboTYPE.SelectedIndex = 0

        deDATEFrom.DateTime = Now
        deDATETo.DateTime = Now
    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            grv.SaveLayoutToRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text & "\" & cboTYPE.Text)
        Catch oErr As Exception

        End Try
    End Sub
    Public Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            Me.Text = SalesInvoice.TITLE_REPORT

            lTYPE.Text = Report.FILTER_TYPE
            lDATEFROM.Text = Report.FILTER_DATEFROM
            lDATETO.Text = Report.FILTER_DATETO

            cboTYPE.Properties.Items.Clear()
            cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_01)
            cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_02)
            cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_03)
            cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_04)
            cboTYPE.Properties.Items.Add("Keuntungan")
            cboTYPE.Properties.Items.Add("Pengeluaran Barang Per Hari")

            If cboTYPE.SelectedIndex = 4 Then

            ElseIf cboTYPE.SelectedIndex = 3 Then
                fn_LoadLanguageAll()
            Else
                fn_LoadLanguageMaster()
                fn_LoadLanguageDetail()
            End If
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
                     Where x.MODUL = "SI_R" _
                     And y.KDUSER = sUserID _
                     Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                picPrint.Enabled = ds.ISPRINT
                picRefresh.Enabled = ds.ISVIEW

                If ds.ISVIEW = True Then
                    fn_Preview()
                    fn_LoadLanguage()
                End If
            Catch oErr As Exception
                MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)

                picPrint.Enabled = False
                picRefresh.Enabled = False
            End Try

            Dim dsKeuntungan = (From x In oOtority.GetDataDetail _
                     Join y In oUser.GetData _
                     On x.KDOTORITY Equals y.KDOTORITY _
                     Where x.MODUL = "PROFIT_R" _
                     And y.KDUSER = sUserID _
                     Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                If ds.ISVIEW = True Then

                End If
            Catch oErr As Exception
                MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Catch oErr As Exception
            MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Print()
        Try
            printableComponentLink.Landscape = True
            printableComponentLink.PaperKind = Printing.PaperKind.A4

            Dim phf As PageHeaderFooter = _
        TryCast(printableComponentLink.PageHeaderFooter, PageHeaderFooter)
            phf.Header.Content.Clear()
            phf.Header.Font = New Font("Times New Roman", 14, FontStyle.Bold)
            phf.Header.LineAlignment = BrickAlignment.Center
            phf.Footer.Font = New Font("Times New Roman", 9.75)
            phf.Footer.LineAlignment = BrickAlignment.Far
            phf.Footer.Content.AddRange(New String() _
        {sWATERMARK, "", Report.REPORT_PAGE & " : [Page # of Pages #]"})

            phf.Header.Content.AddRange(New String() _
{"", SalesInvoice.TITLE_REPORT & vbCrLf & deDATEFrom.DateTime.ToString("dd/MM/yyyy") & " - " & deDATETo.DateTime.ToString("dd/MM/yyyy"), ""})

            printableComponentLink.Component = grd
            printableComponentLink.CreateDocument()
            printableComponentLink.ShowPreviewDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Preview()
        Try
            grv.Columns.Clear()
            grd.DataSource = Nothing
            grv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            grv1.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            If cboTYPE.SelectedIndex = 5 Then
                fn_LoadDataLaporanBarangPerHari()
            ElseIf cboTYPE.SelectedIndex = 4 Then
                fn_LoadDataKeuntungan()
            ElseIf cboTYPE.SelectedIndex = 3 Then
                fn_LoadDataAll()
            ElseIf cboTYPE.SelectedIndex = 2 Then
                fn_LoadDataDetil()
            Else
                fn_LoadData()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

#Region "Master Detail"
    Private Sub fn_LoadData()
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
            SQL &= "A.KDSI "
            SQL &= ",A.DATE "
            SQL &= ",KDCUSTOMER = B.NAME_DISPLAY "
            SQL &= ",KDWAREHOUSE = C.NAME_DISPLAY "
            SQL &= ",KDSALESMAN = D.NAME_DISPLAY "
            SQL &= ",A.MEMO "
            SQL &= ",A.KDUSER "
            SQL &= ",A.SUBTOTAL "
            SQL &= ",A.DISCOUNT "
            SQL &= ",A.TAX "
            SQL &= ",A.GRANDTOTAL "
            SQL &= "FROM S_SI_H A "
            SQL &= "INNER JOIN M_CUSTOMER B "
            SQL &= "ON A.KDCUSTOMER = B.KDCUSTOMER "
            SQL &= "INNER JOIN M_WAREHOUSE C "
            SQL &= "ON A.KDWAREHOUSE = C.KDWAREHOUSE "
            SQL &= "INNER JOIN M_SALESMAN D "
            SQL &= "ON A.KDSALESMAN = D.KDSALESMAN "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "ORDER BY DATE, KDSI DESC"

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_SI_H")

            SQL = "SELECT "
            SQL &= "B.KDSI "
            SQL &= ",C.NMITEM1 "
            SQL &= ",C.NMITEM2 "
            SQL &= ",C.NMITEM3 "
            SQL &= ",B.QTY "
            SQL &= ",KDUOM = D.MEMO "
            SQL &= ",B.PRICE "
            SQL &= ",B.SUBTOTAL "
            SQL &= ",B.DISCOUNT "
            SQL &= ",B.GRANDTOTAL "
            SQL &= ",B.REMARKS "
            SQL &= "FROM S_SI_H A "
            SQL &= "INNER JOIN S_SI_D B "
            SQL &= "ON A.KDSI = B.KDSI "
            SQL &= "INNER JOIN M_ITEM C "
            SQL &= "ON B.KDITEM = C.KDITEM "
            SQL &= "INNER JOIN M_UOM D "
            SQL &= "ON B.KDUOM = D.KDUOM "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_SI_D")

            If cboTYPE.SelectedIndex = 0 Then
                Dim keyColumn As DataColumn = ds.Tables("S_SI_H").Columns("KDSI")
                Dim foreignKeyColumn As DataColumn = ds.Tables("S_SI_D").Columns("KDSI")
                ds.Relations.Add("FK_RELATION", keyColumn, foreignKeyColumn)

                grd.MainView = grv
                grd.DataSource = ds.Tables("S_SI_H")
                grd.ForceInitialize()

                grd.LevelTree.Nodes.Add("FK_RELATION", grv1)
                grv1.ViewCaption = "Details"

                grv1.PopulateColumns(ds.Tables("S_SI_D"))
                grv1.Columns("KDSI").VisibleIndex = -1

                fn_LoadFormatData()
                fn_LoadFormatDataDetail()
            ElseIf cboTYPE.SelectedIndex = 1 Then
                grd.MainView = grv
                grd.DataSource = ds.Tables("S_SI_H")
                grd.ForceInitialize()

                fn_LoadFormatData()
            ElseIf cboTYPE.SelectedIndex = 2 Then
                grd.MainView = grv1
                grd.DataSource = ds.Tables("S_SI_D")
                grd.ForceInitialize()

                fn_LoadFormatDataDetail()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatData()
        For iLoop As Integer = 0 To grv.Columns.Count - 1
            If grv.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next
    End Sub
    Private Sub fn_LoadFormatDataDetail()
        For iLoop As Integer = 0 To grv1.Columns.Count - 1
            If grv1.Columns(iLoop).FieldName = "QTY" Or grv1.Columns(iLoop).FieldName = "PRICE" Or grv1.Columns(iLoop).FieldName = "SUBTOTAL" Or grv1.Columns(iLoop).FieldName = "DISCOUNT" Or grv1.Columns(iLoop).FieldName = "GRANDTOTAL" Then
                grv1.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv1.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv1.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            End If
        Next
    End Sub
    Public Sub fn_LoadLanguageMaster()
        Try
            grv.Columns("KDSI").Caption = SalesInvoice.KDSI
            grv.Columns("DATE").Caption = SalesInvoice.TANGGAL
            grv.Columns("KDCUSTOMER").Caption = SalesInvoice.KDCUSTOMER
            grv.Columns("KDWAREHOUSE").Caption = SalesInvoice.KDWAREHOUSE
            grv.Columns("MEMO").Caption = SalesInvoice.MEMO
            grv.Columns("SUBTOTAL").Caption = SalesInvoice.SUBTOTAL
            grv.Columns("DISCOUNT").Caption = SalesInvoice.DISCOUNT
            grv.Columns("TAX").Caption = SalesInvoice.TAX
            grv.Columns("GRANDTOTAL").Caption = SalesInvoice.GRANDTOTAL
            grv.Columns("KDUSER").Caption = Caption.User
            grv.Columns("KDSALESMAN").Caption = "Penjual"

            MasterColumnChooserToolStripMenuItem.Text = Caption.ColumnChooserMaster
            DetailColumnChooserToolStripMenuItem.Text = Caption.ColumnChooserDetail
        Catch oErr As Exception

        End Try
    End Sub
    Public Sub fn_LoadLanguageDetail()
        Try
            grv1.Columns("QTY").Caption = SalesInvoice.DETAIL_QTY
            grv1.Columns("PRICE").Caption = SalesInvoice.DETAIL_PRICE
            grv1.Columns("SUBTOTAL").Caption = SalesInvoice.DETAIL_SUBTOTAL
            grv1.Columns("DISCOUNT").Caption = SalesInvoice.DETAIL_DISCOUNT
            grv1.Columns("GRANDTOTAL").Caption = SalesInvoice.DETAIL_GRANDTOTAL
            grv1.Columns("REMARKS").Caption = SalesInvoice.DETAIL_REMARKS

            grv1.Columns("NMITEM1").Caption = Item.NMITEM1_2
            grv1.Columns("NMITEM2").Caption = Item.NMITEM2_2
            grv1.Columns("NMITEM3").Caption = Item.NMITEM3_2
            grv1.Columns("KDUOM").Caption = SalesInvoice.DETAIL_KDUOM
        Catch oErr As Exception

        End Try
    End Sub
#End Region
#Region "Detail"
    Private Sub fn_LoadDataDetil()
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
            SQL &= "C.NMITEM1 "
            SQL &= ",C.NMITEM2 "
            SQL &= ",QTY = SUM(B.QTY) "
            SQL &= ",KDUOM = D.MEMO "
            SQL &= ",B.PRICE "
            SQL &= ",B.DISCOUNT "
            SQL &= ",GRANDTOTAL = (SUM(B.QTY) * B.PRICE) - B.DISCOUNT "
            SQL &= "FROM S_SI_H A "
            SQL &= "INNER JOIN S_SI_D B "
            SQL &= "ON A.KDSI = B.KDSI "
            SQL &= "INNER JOIN M_ITEM C "
            SQL &= "ON B.KDITEM = C.KDITEM "
            SQL &= "INNER JOIN M_UOM D "
            SQL &= "ON B.KDUOM = D.KDUOM "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "GROUP BY C.NMITEM1, C.NMITEM2, D.MEMO, B.PRICE, B.DISCOUNT "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_SI_D")

            If cboTYPE.SelectedIndex = 2 Then
                grd.MainView = grv1
                grd.DataSource = ds.Tables("S_SI_D")
                grd.ForceInitialize()

                fn_LoadFormatDetail()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatDetail()
        For iLoop As Integer = 0 To grv1.Columns.Count - 1
            If grv1.Columns(iLoop).FieldName = "QTY" Or grv1.Columns(iLoop).FieldName = "PRICE" Or grv1.Columns(iLoop).FieldName = "SUBTOTAL" Or grv1.Columns(iLoop).FieldName = "DISCOUNT" Or grv1.Columns(iLoop).FieldName = "GRANDTOTAL" Then
                grv1.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv1.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv1.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            End If
        Next
    End Sub
#End Region
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

            SQL = "SELECT "
            SQL &= "A.* "
            SQL &= ",B.* "
            SQL &= "FROM "
            SQL &= "( "
            SQL &= "SELECT "
            SQL &= "A.KDSI "
            SQL &= ",A.DATE "
            SQL &= ",KDCUSTOMER = B.NAME_DISPLAY "
            SQL &= ",KDWAREHOUSE = C.NAME_DISPLAY "
            SQL &= ",KDSALESMAN = D.NAME_DISPLAY "
            SQL &= ",A.MEMO "
            SQL &= ",A.KDUSER "
            SQL &= ",A.SUBTOTAL "
            SQL &= ",A.DISCOUNT "
            SQL &= ",A.TAX "
            SQL &= ",A.GRANDTOTAL "
            SQL &= "FROM S_SI_H A "
            SQL &= "INNER JOIN M_CUSTOMER B "
            SQL &= "ON A.KDCUSTOMER = B.KDCUSTOMER "
            SQL &= "INNER JOIN M_WAREHOUSE C "
            SQL &= "ON A.KDWAREHOUSE = C.KDWAREHOUSE "
            SQL &= "INNER JOIN M_SALESMAN D "
            SQL &= "ON A.KDSALESMAN = D.KDSALESMAN "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= ") AS A "

            SQL &= "INNER JOIN "
            SQL &= "( "
            SQL &= "SELECT "
            SQL &= "B.KDSI "
            SQL &= ",C.NMITEM1 "
            SQL &= ",C.NMITEM2 "
            SQL &= ",C.NMITEM3 "
            SQL &= ",B.QTY "
            SQL &= ",KDUOM = D.MEMO "
            SQL &= ",B.PRICE "
            SQL &= ",B.SUBTOTAL "
            SQL &= ",B.DISCOUNT "
            SQL &= ",B.GRANDTOTAL "
            SQL &= ",B.REMARKS "
            SQL &= "FROM S_SI_H A "
            SQL &= "INNER JOIN S_SI_D B "
            SQL &= "ON A.KDSI = B.KDSI "
            SQL &= "INNER JOIN M_ITEM C "
            SQL &= "ON B.KDITEM = C.KDITEM "
            SQL &= "INNER JOIN M_UOM D "
            SQL &= "ON B.KDUOM = D.KDUOM "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= ") AS B "

            SQL &= "ON A.KDSI = B.KDSI "
            SQL &= "ORDER BY A.KDSI DESC "

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
            ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

        grv.Columns("KDSI1").Visible = False
        grv.Columns("KDSI1").OptionsColumn.ShowInCustomizationForm = False
    End Sub
    Public Sub fn_LoadLanguageAll()
        Try
            grv.Columns("KDSI").Caption = SalesInvoice.KDSI
            grv.Columns("DATE").Caption = SalesInvoice.TANGGAL
            grv.Columns("KDCUSTOMER").Caption = SalesInvoice.KDCUSTOMER
            grv.Columns("KDWAREHOUSE").Caption = SalesInvoice.KDWAREHOUSE
            grv.Columns("MEMO").Caption = SalesInvoice.MEMO
            grv.Columns("SUBTOTAL").Caption = SalesInvoice.SUBTOTAL
            grv.Columns("DISCOUNT").Caption = SalesInvoice.DISCOUNT
            grv.Columns("TAX").Caption = SalesInvoice.TAX
            grv.Columns("GRANDTOTAL").Caption = SalesInvoice.GRANDTOTAL
            grv.Columns("KDUSER").Caption = Caption.User
            grv.Columns("KDSALESMAN").Caption = "Penjual"

            grv.Columns("QTY").Caption = SalesInvoice.DETAIL_QTY
            grv.Columns("PRICE").Caption = SalesInvoice.DETAIL_PRICE
            grv.Columns("SUBTOTAL1").Caption = SalesInvoice.DETAIL_SUBTOTAL
            grv.Columns("DISCOUNT1").Caption = SalesInvoice.DETAIL_DISCOUNT
            grv.Columns("GRANDTOTAL1").Caption = SalesInvoice.DETAIL_GRANDTOTAL
            grv.Columns("REMARKS").Caption = SalesInvoice.DETAIL_REMARKS

            grv.Columns("NMITEM1").Caption = Item.NMITEM1_2
            grv.Columns("NMITEM2").Caption = Item.NMITEM2_2
            grv.Columns("NMITEM3").Caption = Item.NMITEM3_2
            grv.Columns("KDUOM").Caption = SalesInvoice.DETAIL_KDUOM

            MasterColumnChooserToolStripMenuItem.Text = Caption.ColumnChooserMaster
            DetailColumnChooserToolStripMenuItem.Text = Caption.ColumnChooserDetail
        Catch oErr As Exception

        End Try
    End Sub
#End Region
#Region "Keuntungan"
    Private Sub fn_LoadDataKeuntungan()
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
            SQL &= "A.* "
            SQL &= ",B.* "
            SQL &= "FROM "
            SQL &= "( "
            SQL &= "SELECT "
            SQL &= "NOMOR = A.KDSI "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",PELANGGAN = B.NAME_DISPLAY "
            SQL &= ",GUDANG = C.NAME_DISPLAY "
            SQL &= ",PENJUAL = D.NAME_DISPLAY "
            SQL &= "FROM S_SI_H A "
            SQL &= "INNER JOIN M_CUSTOMER B "
            SQL &= "ON A.KDCUSTOMER = B.KDCUSTOMER "
            SQL &= "INNER JOIN M_WAREHOUSE C "
            SQL &= "ON A.KDWAREHOUSE = C.KDWAREHOUSE "
            SQL &= "INNER JOIN M_SALESMAN D "
            SQL &= "ON A.KDSALESMAN = D.KDSALESMAN "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= ") AS A "

            SQL &= "INNER JOIN "
            SQL &= "( "
            SQL &= "SELECT "
            SQL &= "NOMOR = B.KDSI "
            SQL &= ",[NAMA BARANG] = C.NMITEM2 "
            SQL &= ",JUMLAH = B.QTY "
            SQL &= ",[HARGA JUAL] = B.PRICE "
            SQL &= ",DISKON = B.DISCOUNT "

            '    Dim ds = From x In oSalesOrder.GetData _
            '             Join y In oSalesOrder.GetDataDetail _
            '             On x.KDSO Equals y.KDSO _
            '             Join z In oStockCard.GetData _
            '             On x.KDSO Equals z.NOREFERENCE And y.KDITEM Equals z.KDITEM And y.KDUOM Equals z.KDUOM And y.SEQ Equals z.SEQREF _
            '             Where x.DATE.ToString("yyyyMMdd") >= deDATEFrom.DateTime.ToString("yyyyMMdd") And x.DATE.ToString("yyyyMMdd") <= deDATETo.DateTime.ToString("yyyyMMdd") _
            '             Group y By y.M_ITEM.NMITEM1, y.M_ITEM.NMITEM2, y.M_ITEM_UOM.M_UOM.DESCRIPTION, y.PRICE, z.HPPOUT Into Jumlah = Sum(y.QTY), GrandTotal = Sum(y.GRANDTOTAL) _
            '             Select Barcode = NMITEM1, NamaDagang = NMITEM2, Jumlah, Satuan = DESCRIPTION, Harga = PRICE, Modal = HPPOUT, GrandTotal, Keuntungan = GrandTotal - (Jumlah * HPPOUT) _
            '             Order By GrandTotal Descending

            SQL &= ",[TOTAL JUAL] = B.GRANDTOTAL "
            SQL &= ",[HARGA MODAL] = D.HPPOUT "
            SQL &= ",[TOTAL MODAL] = D.HPPOUT * B.QTY "
            SQL &= ",[KEUNTUNGAN] = B.GRANDTOTAL - (D.HPPOUT * B.QTY) "
            SQL &= "FROM S_SI_H A "
            SQL &= "INNER JOIN S_SI_D B "
            SQL &= "ON A.KDSI = B.KDSI "
            SQL &= "INNER JOIN M_ITEM C "
            SQL &= "ON B.KDITEM = C.KDITEM "
            SQL &= "INNER JOIN A_STOCK_CARD D "
            SQL &= "ON A.KDSI = D.NOREFERENCE AND B.KDITEM = D.KDITEM AND B.KDUOM = D.KDUOM AND B.SEQ = D.SEQREF "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= ") AS B "

            SQL &= "ON A.NOMOR = B.NOMOR "
            SQL &= "ORDER BY A.NOMOR DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

            grd.MainView = grv
            grd.DataSource = ds.Tables("ALL")
            grd.ForceInitialize()

            fn_LoadFormatDataKeuntungan()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatDataKeuntungan()
        For iLoop As Integer = 0 To grv.Columns.Count - 1
            grv.Columns(iLoop).BestFit()

            If grv.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

        grv.Columns("NOMOR1").Visible = False
        grv.Columns("NOMOR1").OptionsColumn.ShowInCustomizationForm = False
    End Sub
#End Region
#Region "Laporan Barang Per Hari"
    Private Sub fn_LoadDataLaporanBarangPerHari()
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
            SQL &= "[NAMA BARANG] = A.NMITEM2 "
            SQL &= ",[TGL BELI] = (SELECT TOP 1 BB.DATE FROM P_PI_D AS AA INNER JOIN P_PI_H AS BB ON AA.KDPI = BB.KDPI WHERE AA.KDITEM = A.KDITEM ORDER BY BB.DATE DESC) "
            SQL &= ",[TGL JUAL] = (SELECT TOP 1 BB.DATE FROM S_SI_D AS AA INNER JOIN S_SI_H AS BB ON AA.KDSI = BB.KDSI WHERE AA.KDITEM = A.KDITEM ORDER BY BB.DATE DESC) "
            SQL &= ",[HARI JUAL] = DATEDIFF(dd,(SELECT TOP 1 BB.DATE FROM S_SI_D AS AA INNER JOIN S_SI_H AS BB ON AA.KDSI = BB.KDSI WHERE AA.KDITEM = A.KDITEM ORDER BY BB.DATE DESC),'" & deDATEFrom.DateTime.ToString("yyyy-MM-dd") & "') "
            SQL &= ",[0 - 10] = (SELECT SUM(QTY) FROM S_SI_D AS AA INNER JOIN S_SI_H AS BB ON AA.KDSI = BB.KDSI WHERE AA.KDITEM = A.KDITEM AND (CONVERT(VARCHAR(8), BB.DATE, 112) >= '" & deDATEFrom.DateTime.AddDays(-10).ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), BB.DATE, 112) <= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "')) "
            SQL &= ",[0 - 20] = (SELECT SUM(QTY) FROM S_SI_D AS AA INNER JOIN S_SI_H AS BB ON AA.KDSI = BB.KDSI WHERE AA.KDITEM = A.KDITEM AND (CONVERT(VARCHAR(8), BB.DATE, 112) >= '" & deDATEFrom.DateTime.AddDays(-20).ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), BB.DATE, 112) <= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "')) "
            SQL &= ",[0 - 30] = (SELECT SUM(QTY) FROM S_SI_D AS AA INNER JOIN S_SI_H AS BB ON AA.KDSI = BB.KDSI WHERE AA.KDITEM = A.KDITEM AND (CONVERT(VARCHAR(8), BB.DATE, 112) >= '" & deDATEFrom.DateTime.AddDays(-30).ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), BB.DATE, 112) <= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "')) "
            SQL &= ",[STOK] = (SELECT AMOUNT FROM M_ITEM_WAREHOUSE AS AA WHERE AA.KDITEM = A.KDITEM) "
            SQL &= ",AVERAGE = ISNULL((SELECT TOP 1 HPPAVERAGE FROM A_STOCK_CARD AS AA WHERE AA.KDITEM = A.KDITEM ORDER BY SEQ DESC), 0) "
            SQL &= ",TOTAL = ISNULL((SELECT AMOUNT FROM M_ITEM_WAREHOUSE AS AA WHERE AA.KDITEM = A.KDITEM), 0) * ISNULL((SELECT TOP 1 HPPAVERAGE FROM A_STOCK_CARD AS AA WHERE AA.KDITEM = A.KDITEM ORDER BY SEQ DESC), 0) "
            SQL &= "FROM M_ITEM AS A "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

            grd.MainView = grv
            grd.DataSource = ds.Tables("ALL")
            grd.ForceInitialize()

            fn_LoadFormatDataLaporanBarangPerHari()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatDataLaporanBarangPerHari()
        For iLoop As Integer = 0 To grv.Columns.Count - 1
            grv.Columns(iLoop).BestFit()

            If grv.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next
    End Sub
#End Region

    Private Sub MasterColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MasterColumnChooserToolStripMenuItem.Click
        grv.ShowCustomization()
    End Sub
    Private Sub DetailColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DetailColumnChooserToolStripMenuItem.Click
        grv1.ShowCustomization()
    End Sub
    Private Sub cboTYPE_SelectedIndexChanged() Handles cboTYPE.SelectedIndexChanged
        fn_LoadSecurity()

        Try
            grv.RestoreLayoutFromRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text & "\" & cboTYPE.Text)
        Catch ex As Exception

        End Try
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

        Try
            grv.RestoreLayoutFromRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text & "\" & cboTYPE.Text)
        Catch ex As Exception

        End Try
    End Sub
#End Region
End Class