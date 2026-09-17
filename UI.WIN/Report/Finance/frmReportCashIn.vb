Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Imports DevExpress.XtraPrinting

Imports System.Data
Imports System.Data.SqlClient


Public Class frmReportCashIn
    Implements ILanguage

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = CashIn.TITLE_REPORT

        lTYPE.Text = Report.FILTER_TYPE
        lDATEFROM.Text = Report.FILTER_DATEFROM
        lDATETO.Text = Report.FILTER_DATETO

        cboTYPE.Properties.Items.Clear()
        cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_01)
        cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_02)
        cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_03)
        cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_04)
        'cboTYPE.Properties.Items.Add("Detil Partik Rawat Inap Pasien Pulang")
        'cboTYPE.Properties.Items.Add("Rekap Partik Rawat Inap Pasien Pulang")
        'cboTYPE.Properties.Items.Add("Detil BPJS")
        'cboTYPE.Properties.Items.Add("Rekap BPJS")

        cboTYPE.SelectedIndex = 0

        deDATEFrom.DateTime = Now.AddDays((-Now.Day) + 1)
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
            Me.Text = CashIn.TITLE_REPORT

            lTYPE.Text = Report.FILTER_TYPE
            lDATEFROM.Text = Report.FILTER_DATEFROM
            lDATETO.Text = Report.FILTER_DATETO

            cboTYPE.Properties.Items.Clear()
            cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_01)
            cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_02)
            cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_03)
            cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_04)
            'cboTYPE.Properties.Items.Add("Detil Partik Rawat Inap Pasien Pulang")
            'cboTYPE.Properties.Items.Add("Rekap Partik Rawat Inap Pasien Pulang")
            'cboTYPE.Properties.Items.Add("Detil BPJS")
            'cboTYPE.Properties.Items.Add("Rekap BPJS")

            If cboTYPE.SelectedIndex = 3 Then
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
                     Where x.MODUL = "CASHIN_R" _
                     And y.KDUSER = sUserID _
                     Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                picPrint.Enabled = ds.ISPRINT
                picRefresh.Enabled = ds.ISVIEW
                picUpdate.Enabled = ds.ISUPDATE

                If ds.ISVIEW = True Then
                    fn_Preview()
                    fn_LoadLanguage()
                End If
            Catch oErr As Exception
                MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)

                picPrint.Enabled = False
                picRefresh.Enabled = False
                picUpdate.Enabled = False
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
{"", CashIn.TITLE_REPORT & vbCrLf & deDATEFrom.DateTime.ToString("dd/MM/yyyy") & " - " & deDATETo.DateTime.ToString("dd/MM/yyyy"), ""})

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

            If cboTYPE.SelectedIndex = 3 Then
                fn_LoadDataAll()
            ElseIf cboTYPE.SelectedIndex = 4 Then
                fn_LoadDataPartikDetil()
            ElseIf cboTYPE.SelectedIndex = 5 Then
                fn_LoadDataPartikRekap()
            ElseIf cboTYPE.SelectedIndex = 6 Then
                fn_LoadDataBPJSDetil()
            ElseIf cboTYPE.SelectedIndex = 7 Then
                fn_LoadDataBPJSRekap()
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
            SQL &= "A.KDCASHIN "
            SQL &= ",A.DATE "
            SQL &= ",KDCUSTOMER = C.NAME_DISPLAY "
            SQL &= ",KDPAYMENTTYPE = D.MEMO "
            SQL &= ",A.MEMO "

            SQL &= ",A.KDUSER "
            SQL &= ",A.SUBTOTAL "
            SQL &= ",A.ADMIN "
            SQL &= ",A.ROUND "
            SQL &= ",A.GRANDTOTAL "
            SQL &= "FROM F_CASHIN_H A "
            SQL &= "INNER JOIN S_PENDAFTARAN_H B "
            SQL &= "ON A.KDPENDAFTARAN = B.KDPENDAFTARAN "
            SQL &= "INNER JOIN M_CUSTOMER C "
            SQL &= "ON B.KDCUSTOMER = C.KDCUSTOMER "
            SQL &= "INNER JOIN M_PAYMENTTYPE D "
            SQL &= "ON A.KDPAYMENTTYPE = D.KDPAYMENTTYPE "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "ORDER BY DATE, KDCASHIN DESC"

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "F_CASHIN_H")

            SQL = "SELECT "
            SQL &= "B.KDCASHIN "
            SQL &= ",B.NOINVOICE "
            SQL &= ",B.AMOUNTPAYMENT "
            SQL &= ",B.REMARKS "
            SQL &= "FROM F_CASHIN_H A "
            SQL &= "INNER JOIN F_CASHIN_D B "
            SQL &= "ON A.KDCASHIN = B.KDCASHIN "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "F_CASHIN_D")

            If cboTYPE.SelectedIndex = 0 Then
                Dim keyColumn As DataColumn = ds.Tables("F_CASHIN_H").Columns("KDCASHIN")
                Dim foreignKeyColumn As DataColumn = ds.Tables("F_CASHIN_D").Columns("KDCASHIN")
                ds.Relations.Add("FK_RELATION", keyColumn, foreignKeyColumn)

                grd.MainView = grv
                grd.DataSource = ds.Tables("F_CASHIN_H")
                grd.ForceInitialize()

                grd.LevelTree.Nodes.Add("FK_RELATION", grv1)
                grv1.ViewCaption = "Details"

                grv1.PopulateColumns(ds.Tables("F_CASHIN_D"))
                grv1.Columns("KDCASHIN").VisibleIndex = -1

                fn_LoadFormatData()
                fn_LoadFormatDataDetail()
            ElseIf cboTYPE.SelectedIndex = 1 Then
                grd.MainView = grv
                grd.DataSource = ds.Tables("F_CASHIN_H")
                grd.ForceInitialize()

                fn_LoadFormatData()
            ElseIf cboTYPE.SelectedIndex = 2 Then
                grd.MainView = grv1
                grd.DataSource = ds.Tables("F_CASHIN_D")
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
            If grv1.Columns(iLoop).FieldName = "AMOUNTPAYMENT" Then
                grv1.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv1.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv1.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            End If
        Next
    End Sub
    Public Sub fn_LoadLanguageMaster()
        Try
            grv.Columns("KDCASHIN").Caption = CashIn.KDCASHIN
            grv.Columns("DATE").Caption = CashIn.TANGGAL
            grv.Columns("KDPENDAFTARAN").Caption = CashIn.KDPENDAFTARAN
            grv.Columns("KDPAYMENTTYPE").Caption = CashIn.KDPAYMENTTYPE
            grv.Columns("MEMO").Caption = CashIn.MEMO
            grv.Columns("SUBTOTAL").Caption = CashIn.SUBTOTAL
            grv.Columns("ADMIN").Caption = CashIn.ADMIN
            grv.Columns("ROUND").Caption = CashIn.ROUND
            grv.Columns("GRANDTOTAL").Caption = CashIn.GRANDTOTAL
            grv.Columns("KDUSER").Caption = Caption.User

            MasterColumnChooserToolStripMenuItem.Text = Caption.ColumnChooserMaster
            DetailColumnChooserToolStripMenuItem.Text = Caption.ColumnChooserDetail
        Catch oErr As Exception

        End Try
    End Sub
    Public Sub fn_LoadLanguageDetail()
        Try
            grv1.Columns("AMOUNTPAYMENT").Caption = CashIn.DETAIL_AMOUNTPAYMENT
            grv1.Columns("REMARKS").Caption = CashIn.DETAIL_REMARKS

            grv1.Columns("NOINVOICE").Caption = CashIn.DETAIL_NOINVOICE
        Catch oErr As Exception

        End Try
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
            SQL &= "A.KDCASHIN "
            SQL &= ",A.DATE "
            SQL &= ",KDCUSTOMER = C.NAME_DISPLAY "
            SQL &= ",KDPAYMENTTYPE = D.MEMO "
            SQL &= ",A.MEMO "

            SQL &= ",A.KDUSER "
            SQL &= ",A.SUBTOTAL "
            SQL &= ",A.ADMIN "
            SQL &= ",A.ROUND "
            SQL &= ",A.GRANDTOTAL "
            SQL &= "FROM F_CASHIN_H A "
            SQL &= "INNER JOIN S_PENDAFTARAN_H B "
            SQL &= "ON A.KDPENDAFTARAN = B.KDPENDAFTARAN "
            SQL &= "INNER JOIN M_CUSTOMER C "
            SQL &= "ON B.KDCUSTOMER = C.KDCUSTOMER "
            SQL &= "INNER JOIN M_PAYMENTTYPE D "
            SQL &= "ON A.KDPAYMENTTYPE = D.KDPAYMENTTYPE "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= ") AS A "

            SQL &= "INNER JOIN "
            SQL &= "( "
            SQL &= "SELECT "
            SQL &= "B.KDCASHIN "
            SQL &= ",B.NOINVOICE "
            SQL &= ",B.AMOUNTPAYMENT "
            SQL &= ",B.REMARKS "
            SQL &= "FROM F_CASHIN_H A "
            SQL &= "INNER JOIN F_CASHIN_D B "
            SQL &= "ON A.KDCASHIN = B.KDCASHIN "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= ") AS B "

            SQL &= "ON A.KDCASHIN = B.KDCASHIN "
            SQL &= "ORDER BY A.KDCASHIN DESC "

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

        grv.Columns("KDCASHIN1").Visible = False
        grv.Columns("KDCASHIN1").OptionsColumn.ShowInCustomizationForm = False
    End Sub
    Public Sub fn_LoadLanguageAll()
        Try
            grv.Columns("KDCASHIN").Caption = CashIn.KDCASHIN
            grv.Columns("DATE").Caption = CashIn.TANGGAL
            grv.Columns("KDPENDAFTARAN").Caption = CashIn.KDPENDAFTARAN
            grv.Columns("KDPAYMENTTYPE").Caption = CashIn.KDPAYMENTTYPE
            grv.Columns("MEMO").Caption = CashIn.MEMO
            grv.Columns("SUBTOTAL").Caption = CashIn.SUBTOTAL
            grv.Columns("ADMIN").Caption = CashIn.ADMIN
            grv.Columns("ROUND").Caption = CashIn.ROUND
            grv.Columns("GRANDTOTAL").Caption = CashIn.GRANDTOTAL
            grv.Columns("KDUSER").Caption = Caption.User

            grv.Columns("AMOUNTPAYMENT").Caption = CashIn.DETAIL_AMOUNTPAYMENT
            grv.Columns("REMARKS").Caption = CashIn.DETAIL_REMARKS

            grv.Columns("NOINVOICE").Caption = CashIn.DETAIL_NOINVOICE

            MasterColumnChooserToolStripMenuItem.Text = Caption.ColumnChooserMaster
            DetailColumnChooserToolStripMenuItem.Text = Caption.ColumnChooserDetail
        Catch oErr As Exception

        End Try
    End Sub
#End Region
#Region "Permintaan"
    Private Sub fn_LoadDataPartikDetil()
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
            SQL &= "J.KDCASHIN "
            SQL &= ",D.KDSOTRANSAKSI "
            SQL &= ",TANGGALMASUK = A.DATE "
            SQL &= ",TANGGALPULANG = F.DATE "
            SQL &= ",PASIEN = B.NAME_DISPLAY "
            SQL &= ",JENIS = H.MEMO "
            SQL &= ",TARIF = G.NMITEM2 "
            SQL &= ",HARGA = E.PRICE "
            SQL &= ",JUMLAH = E.QTY "
            SQL &= ",GRANDTOTAL = E.GRANDTOTAL "
            SQL &= ",DOKTER = I.NAME_DISPLAY "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_H A "
            SQL &= "INNER JOIN M_CUSTOMER B "
            SQL &= "ON A.KDCUSTOMER = B.KDCUSTOMER "
            SQL &= "INNER JOIN S_PENDAFTARAN_KUNJUNGAN C "
            SQL &= "ON A.KDPENDAFTARAN = C.KDPENDAFTARAN "
            SQL &= "INNER JOIN S_SO_TRANSAKSI_H D "
            SQL &= "ON C.KDKUNJUNGAN = D.KDKUNJUNGAN "
            SQL &= "INNER JOIN S_SO_TRANSAKSI_D E "
            SQL &= "ON D.KDSOTRANSAKSI = E.KDSOTRANSAKSI "
            SQL &= "INNER JOIN T_UPDATE_TANGGAL_PULANG F "
            SQL &= "ON A.KDPENDAFTARAN = F.KDPENDAFTARAN "
            SQL &= "INNER JOIN M_ITEM G "
            SQL &= "ON E.KDITEM = G.KDITEM "
            SQL &= "INNER JOIN M_ITEM_L1 H "
            SQL &= "ON G.KDITEM_L1 = H.KDITEM_L1 "
            SQL &= "INNER JOIN M_DOCTOR I "
            SQL &= "ON E.KDDOCTOR = I.KDDOCTOR "
            SQL &= "INNER JOIN F_CASHIN_D J "
            SQL &= "ON E.KDSOTRANSAKSI = J.NOINVOICE "
            SQL &= "WHERE CONVERT(VARCHAR(8), F.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), F.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND A.KDDAFTAR_L1 <> 'DAFTAR_L1_0000000001' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

            grd.MainView = grv
            grd.DataSource = ds.Tables("ALL")
            grd.ForceInitialize()

            fn_LoadFormatDataBilling()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDataPartikRekap()
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
            SQL &= "A.KDCASHIN "
            SQL &= ",A.TANGGALMASUK "
            SQL &= ",A.TANGGALPULANG "
            SQL &= ",A.PASIEN "
            SQL &= ",JUMLAH_VISITE = SUM(A.JUMLAH) "
            SQL &= ",VISITE = SUM(A.GRANDTOTAL) "
            SQL &= ",TINDAKAN = 0 "
            SQL &= ",A.DOKTER "
            SQL &= "FROM ( "
            SQL &= "SELECT "
            SQL &= "J.KDCASHIN "
            SQL &= ",TANGGALMASUK = A.DATE "
            SQL &= ",TANGGALPULANG = F.DATE "
            SQL &= ",PASIEN = B.NAME_DISPLAY "
            SQL &= ",JENIS = H.MEMO "
            SQL &= ",TARIF = G.NMITEM2 "
            SQL &= ",HARGA = E.PRICE "
            SQL &= ",JUMLAH = E.QTY "
            SQL &= ",GRANDTOTAL = E.GRANDTOTAL "
            SQL &= ",DOKTER = I.NAME_DISPLAY "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_H A "
            SQL &= "INNER JOIN M_CUSTOMER B "
            SQL &= "ON A.KDCUSTOMER = B.KDCUSTOMER "
            SQL &= "INNER JOIN S_PENDAFTARAN_KUNJUNGAN C "
            SQL &= "ON A.KDPENDAFTARAN = C.KDPENDAFTARAN "
            SQL &= "INNER JOIN S_SO_TRANSAKSI_H D "
            SQL &= "ON C.KDKUNJUNGAN = D.KDKUNJUNGAN "
            SQL &= "INNER JOIN S_SO_TRANSAKSI_D E "
            SQL &= "ON D.KDSOTRANSAKSI = E.KDSOTRANSAKSI "
            SQL &= "INNER JOIN T_UPDATE_TANGGAL_PULANG F "
            SQL &= "ON A.KDPENDAFTARAN = F.KDPENDAFTARAN "
            SQL &= "INNER JOIN M_ITEM G "
            SQL &= "ON E.KDITEM = G.KDITEM "
            SQL &= "INNER JOIN M_ITEM_L1 H "
            SQL &= "ON G.KDITEM_L1 = H.KDITEM_L1 "
            SQL &= "INNER JOIN M_DOCTOR I "
            SQL &= "ON E.KDDOCTOR = I.KDDOCTOR "
            SQL &= "INNER JOIN F_CASHIN_D J "
            SQL &= "ON E.KDSOTRANSAKSI = J.NOINVOICE "
            SQL &= "WHERE CONVERT(VARCHAR(8), F.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), F.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND H.MEMO = 'VISITE' "
            SQL &= "AND A.KDDAFTAR_L1 <> 'DAFTAR_L1_0000000001' "
            SQL &= ") A "
            SQL &= "GROUP BY "
            SQL &= "A.KDCASHIN "
            SQL &= ",A.TANGGALMASUK "
            SQL &= ",A.TANGGALPULANG "
            SQL &= ",A.PASIEN "
            SQL &= ",A.DOKTER "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "A.KDCASHIN "
            SQL &= ",A.TANGGALMASUK "
            SQL &= ",A.TANGGALPULANG "
            SQL &= ",A.PASIEN "
            SQL &= ",JUMLAH_VISITE = 0 "
            SQL &= ",VISITE = 0 "
            SQL &= ",TINDAKAN = SUM(A.GRANDTOTAL) "
            SQL &= ",A.DOKTER "
            SQL &= "FROM ( "
            SQL &= "SELECT "
            SQL &= "J.KDCASHIN "
            SQL &= ",TANGGALMASUK = A.DATE "
            SQL &= ",TANGGALPULANG = F.DATE "
            SQL &= ",PASIEN = B.NAME_DISPLAY "
            SQL &= ",JENIS = H.MEMO "
            SQL &= ",TARIF = G.NMITEM2 "
            SQL &= ",HARGA = E.PRICE "
            SQL &= ",JUMLAH = E.QTY "
            SQL &= ",GRANDTOTAL = E.GRANDTOTAL "
            SQL &= ",DOKTER = I.NAME_DISPLAY "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_H A "
            SQL &= "INNER JOIN M_CUSTOMER B "
            SQL &= "ON A.KDCUSTOMER = B.KDCUSTOMER "
            SQL &= "INNER JOIN S_PENDAFTARAN_KUNJUNGAN C "
            SQL &= "ON A.KDPENDAFTARAN = C.KDPENDAFTARAN "
            SQL &= "INNER JOIN S_SO_TRANSAKSI_H D "
            SQL &= "ON C.KDKUNJUNGAN = D.KDKUNJUNGAN "
            SQL &= "INNER JOIN S_SO_TRANSAKSI_D E "
            SQL &= "ON D.KDSOTRANSAKSI = E.KDSOTRANSAKSI "
            SQL &= "INNER JOIN T_UPDATE_TANGGAL_PULANG F "
            SQL &= "ON A.KDPENDAFTARAN = F.KDPENDAFTARAN "
            SQL &= "INNER JOIN M_ITEM G "
            SQL &= "ON E.KDITEM = G.KDITEM "
            SQL &= "INNER JOIN M_ITEM_L1 H "
            SQL &= "ON G.KDITEM_L1 = H.KDITEM_L1 "
            SQL &= "INNER JOIN M_DOCTOR I "
            SQL &= "ON E.KDDOCTOR = I.KDDOCTOR "
            SQL &= "INNER JOIN F_CASHIN_D J "
            SQL &= "ON E.KDSOTRANSAKSI = J.NOINVOICE "
            SQL &= "WHERE CONVERT(VARCHAR(8), F.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), F.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND H.MEMO = 'TINDAKAN' "
            SQL &= "AND A.KDDAFTAR_L1 <> 'DAFTAR_L1_0000000001' "
            SQL &= ") A "
            SQL &= "GROUP BY "
            SQL &= "A.KDCASHIN "
            SQL &= ",A.TANGGALMASUK "
            SQL &= ",A.TANGGALPULANG "
            SQL &= ",A.PASIEN "
            SQL &= ",A.DOKTER "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

            grd.MainView = grv
            grd.DataSource = ds.Tables("ALL")
            grd.ForceInitialize()

            fn_LoadFormatDataBilling()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDataBPJSDetil()
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
            SQL &= ",A.NOMORSEP "
            SQL &= ",D.KDSOTRANSAKSI "
            SQL &= ",TANGGALMASUK = A.DATE "
            SQL &= ",TANGGALPULANG = F.DATE "
            SQL &= ",PASIEN = B.NAME_DISPLAY "
            SQL &= ",JENIS = H.MEMO "
            SQL &= ",TARIF = G.NMITEM2 "
            SQL &= ",HARGA = E.PRICE "
            SQL &= ",JUMLAH = E.QTY "
            SQL &= ",GRANDTOTAL = E.GRANDTOTAL "
            SQL &= ",DOKTER = I.NAME_DISPLAY "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_H A "
            SQL &= "INNER JOIN M_CUSTOMER B "
            SQL &= "ON A.KDCUSTOMER = B.KDCUSTOMER "
            SQL &= "INNER JOIN S_PENDAFTARAN_KUNJUNGAN C "
            SQL &= "ON A.KDPENDAFTARAN = C.KDPENDAFTARAN "
            SQL &= "INNER JOIN S_SO_TRANSAKSI_H D "
            SQL &= "ON C.KDKUNJUNGAN = D.KDKUNJUNGAN "
            SQL &= "INNER JOIN S_SO_TRANSAKSI_D E "
            SQL &= "ON D.KDSOTRANSAKSI = E.KDSOTRANSAKSI "
            SQL &= "INNER JOIN T_UPDATE_TANGGAL_PULANG F "
            SQL &= "ON A.KDPENDAFTARAN = F.KDPENDAFTARAN "
            SQL &= "INNER JOIN M_ITEM G "
            SQL &= "ON E.KDITEM = G.KDITEM "
            SQL &= "INNER JOIN M_ITEM_L1 H "
            SQL &= "ON G.KDITEM_L1 = H.KDITEM_L1 "
            SQL &= "INNER JOIN M_DOCTOR I "
            SQL &= "ON E.KDDOCTOR = I.KDDOCTOR "
            SQL &= "WHERE CONVERT(VARCHAR(8), F.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), F.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND A.KDDAFTAR_L1 = 'DAFTAR_L1_0000000001' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

            grd.MainView = grv
            grd.DataSource = ds.Tables("ALL")
            grd.ForceInitialize()

            fn_LoadFormatDataBilling()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDataBPJSRekap()
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
            SQL &= ",A.NOMORSEP "
            SQL &= ",A.TANGGALMASUK "
            SQL &= ",A.TANGGALPULANG "
            SQL &= ",A.PASIEN "
            SQL &= ",JUMLAH_VISITE = SUM(A.JUMLAH) "
            SQL &= ",VISITE = SUM(A.GRANDTOTAL) "
            SQL &= ",TINDAKAN = 0 "
            SQL &= ",A.DOKTER "
            SQL &= "FROM ( "
            SQL &= "SELECT "
            SQL &= "A.KDPENDAFTARAN "
            SQL &= ",A.NOMORSEP "
            SQL &= ",TANGGALMASUK = A.DATE "
            SQL &= ",TANGGALPULANG = F.DATE "
            SQL &= ",PASIEN = B.NAME_DISPLAY "
            SQL &= ",JENIS = H.MEMO "
            SQL &= ",TARIF = G.NMITEM2 "
            SQL &= ",HARGA = E.PRICE "
            SQL &= ",JUMLAH = E.QTY "
            SQL &= ",GRANDTOTAL = E.GRANDTOTAL "
            SQL &= ",DOKTER = I.NAME_DISPLAY "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_H A "
            SQL &= "INNER JOIN M_CUSTOMER B "
            SQL &= "ON A.KDCUSTOMER = B.KDCUSTOMER "
            SQL &= "INNER JOIN S_PENDAFTARAN_KUNJUNGAN C "
            SQL &= "ON A.KDPENDAFTARAN = C.KDPENDAFTARAN "
            SQL &= "INNER JOIN S_SO_TRANSAKSI_H D "
            SQL &= "ON C.KDKUNJUNGAN = D.KDKUNJUNGAN "
            SQL &= "INNER JOIN S_SO_TRANSAKSI_D E "
            SQL &= "ON D.KDSOTRANSAKSI = E.KDSOTRANSAKSI "
            SQL &= "INNER JOIN T_UPDATE_TANGGAL_PULANG F "
            SQL &= "ON A.KDPENDAFTARAN = F.KDPENDAFTARAN "
            SQL &= "INNER JOIN M_ITEM G "
            SQL &= "ON E.KDITEM = G.KDITEM "
            SQL &= "INNER JOIN M_ITEM_L1 H "
            SQL &= "ON G.KDITEM_L1 = H.KDITEM_L1 "
            SQL &= "INNER JOIN M_DOCTOR I "
            SQL &= "ON E.KDDOCTOR = I.KDDOCTOR "
            SQL &= "WHERE CONVERT(VARCHAR(8), F.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), F.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND H.MEMO = 'VISITE' "
            SQL &= "AND A.KDDAFTAR_L1 = 'DAFTAR_L1_0000000001' "
            SQL &= ") A "
            SQL &= "GROUP BY "
            SQL &= "A.KDPENDAFTARAN "
            SQL &= ",A.NOMORSEP "
            SQL &= ",A.TANGGALMASUK "
            SQL &= ",A.TANGGALPULANG "
            SQL &= ",A.PASIEN "
            SQL &= ",A.DOKTER "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "A.KDPENDAFTARAN "
            SQL &= ",A.NOMORSEP "
            SQL &= ",A.TANGGALMASUK "
            SQL &= ",A.TANGGALPULANG "
            SQL &= ",A.PASIEN "
            SQL &= ",JUMLAH_VISITE = 0 "
            SQL &= ",VISITE = 0 "
            SQL &= ",TINDAKAN = SUM(A.GRANDTOTAL) "
            SQL &= ",A.DOKTER "
            SQL &= "FROM ( "
            SQL &= "SELECT "
            SQL &= "A.KDPENDAFTARAN "
            SQL &= ",A.NOMORSEP "
            SQL &= ",TANGGALMASUK = A.DATE "
            SQL &= ",TANGGALPULANG = F.DATE "
            SQL &= ",PASIEN = B.NAME_DISPLAY "
            SQL &= ",JENIS = H.MEMO "
            SQL &= ",TARIF = G.NMITEM2 "
            SQL &= ",HARGA = E.PRICE "
            SQL &= ",JUMLAH = E.QTY "
            SQL &= ",GRANDTOTAL = E.GRANDTOTAL "
            SQL &= ",DOKTER = I.NAME_DISPLAY "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_H A "
            SQL &= "INNER JOIN M_CUSTOMER B "
            SQL &= "ON A.KDCUSTOMER = B.KDCUSTOMER "
            SQL &= "INNER JOIN S_PENDAFTARAN_KUNJUNGAN C "
            SQL &= "ON A.KDPENDAFTARAN = C.KDPENDAFTARAN "
            SQL &= "INNER JOIN S_SO_TRANSAKSI_H D "
            SQL &= "ON C.KDKUNJUNGAN = D.KDKUNJUNGAN "
            SQL &= "INNER JOIN S_SO_TRANSAKSI_D E "
            SQL &= "ON D.KDSOTRANSAKSI = E.KDSOTRANSAKSI "
            SQL &= "INNER JOIN T_UPDATE_TANGGAL_PULANG F "
            SQL &= "ON A.KDPENDAFTARAN = F.KDPENDAFTARAN "
            SQL &= "INNER JOIN M_ITEM G "
            SQL &= "ON E.KDITEM = G.KDITEM "
            SQL &= "INNER JOIN M_ITEM_L1 H "
            SQL &= "ON G.KDITEM_L1 = H.KDITEM_L1 "
            SQL &= "INNER JOIN M_DOCTOR I "
            SQL &= "ON E.KDDOCTOR = I.KDDOCTOR "
            SQL &= "WHERE CONVERT(VARCHAR(8), F.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), F.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND H.MEMO = 'TINDAKAN' "
            SQL &= "AND A.KDDAFTAR_L1 = 'DAFTAR_L1_0000000001' "
            SQL &= ") A "
            SQL &= "GROUP BY "
            SQL &= "A.KDPENDAFTARAN "
            SQL &= ",A.NOMORSEP "
            SQL &= ",A.TANGGALMASUK "
            SQL &= ",A.TANGGALPULANG "
            SQL &= ",A.PASIEN "
            SQL &= ",A.DOKTER "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

            grd.MainView = grv
            grd.DataSource = ds.Tables("ALL")
            grd.ForceInitialize()

            fn_LoadFormatDataBilling()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatDataBilling()
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

        'grv.Columns("KDCASHIN1").Visible = False
        'grv.Columns("KDCASHIN1").OptionsColumn.ShowInCustomizationForm = False
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
            Case Keys.U
                If e.Alt = True And picUpdate.Enabled = True Then
                    picUpdate_Click()
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
    Private Sub picUpdate_Click() Handles picUpdate.Click
        'If cboTYPE.SelectedIndex <> 4 Or cboTYPE.SelectedIndex <> 6 Then Exit Sub

        If grv.GetFocusedRowCellValue("KDSOTRANSAKSI") Is Nothing Then
            Exit Sub
        End If

        Dim oSalesOrder As New Sales.clsSalesOrderTransaksi
        Dim frmSalesOrderTransaksi As New frmSalesOrderTransaksi
        Try
            frmSalesOrderTransaksi.LoadMe(FORM_MODE.FORM_MODE_EDIT, oSalesOrder.GetData(grv.GetFocusedRowCellValue("KDSOTRANSAKSI")).CATEGORY, grv.GetFocusedRowCellValue("KDSOTRANSAKSI"))
            frmSalesOrderTransaksi.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmSalesOrderTransaksi Is Nothing Then frmSalesOrderTransaksi.Dispose()
            frmSalesOrderTransaksi = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDSOTRANSAKSI"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                'picAdd_Click()
            End If
        End Try
    End Sub
#End Region
End Class