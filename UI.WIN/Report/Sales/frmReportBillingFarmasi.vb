Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Imports DevExpress.XtraPrinting

Imports System.Data
Imports System.Data.SqlClient


Public Class frmReportBillingFarmasi
    Implements ILanguage

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "Laporan Billing Farmasi"

        lTYPE.Text = Report.FILTER_TYPE
        lDATEFROM.Text = Report.FILTER_DATEFROM
        lDATETO.Text = Report.FILTER_DATETO

        cboTYPE.Properties.Items.Clear()
        cboTYPE.Properties.Items.Add("Semua Unit")
        cboTYPE.Properties.Items.Add("Pemakaian")
        cboTYPE.Properties.Items.Add("Pemakaian Covid 19")
        cboTYPE.Properties.Items.Add("Pemakaian All")
        cboTYPE.Properties.Items.Add("Laporan Semua Billing R/")
        cboTYPE.Properties.Items.Add("Laporan Semua Tanpa Resep R/")
        cboTYPE.Properties.Items.Add("Laporan Unit Billing R/")
        cboTYPE.Properties.Items.Add("Laporan Unit Tanpa Resep R/")
        cboTYPE.Properties.Items.Add("Laporan Rawat Jalan Resep R/")
        cboTYPE.Properties.Items.Add("Laporan Rawat Inap Resep R/")

        'cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_03)
        'cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_04)
        cboTYPE.SelectedIndex = 0

        deDATEFrom.DateTime = Now.AddDays((-Now.Day) + 1)
        deDATETo.DateTime = Now
    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        'Try
        '    grv.SaveLayoutToRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text & "\" & cboTYPE.Text)
        'Catch oErr As Exception

        'End Try
    End Sub
    Public Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            Me.Text = "Laporan Billing Farmasi"

            lTYPE.Text = Report.FILTER_TYPE
            lDATEFROM.Text = Report.FILTER_DATEFROM
            lDATETO.Text = Report.FILTER_DATETO

            cboTYPE.Properties.Items.Clear()
            cboTYPE.Properties.Items.Add("Semua Unit")
            cboTYPE.Properties.Items.Add("Pemakaian")
            cboTYPE.Properties.Items.Add("Pemakaian Covid 19")
            cboTYPE.Properties.Items.Add("Pemakaian All")
            cboTYPE.Properties.Items.Add("Laporan Semua Billing R/")
            cboTYPE.Properties.Items.Add("Laporan Semua Tanpa Resep R/")
            cboTYPE.Properties.Items.Add("Laporan Unit Billing R/")
            cboTYPE.Properties.Items.Add("Laporan Unit Tanpa Resep R/")
            cboTYPE.Properties.Items.Add("Laporan Rawat Jalan Resep R/")
            cboTYPE.Properties.Items.Add("Laporan Rawat Inap Resep R/")

            'cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_03)
            'cboTYPE.Properties.Items.Add("Laporan")

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadSecurity()
        Try
            Dim oOtority As New Setting.clsOtority
            Dim oUser As New Setting.clsUser

            Dim ds = (From x In oOtority.GetDataDetail
                      Join y In oUser.GetData
                      On x.KDOTORITY Equals y.KDOTORITY
                      Where x.MODUL = "BILLINGFARMASI_R" _
                      And y.KDUSER = sUserID
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
        Catch oErr As Exception
            MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Print()
        Try
            printableComponentLink.Landscape = True
            printableComponentLink.PaperKind = Printing.PaperKind.A4

            Dim phf As PageHeaderFooter =
        TryCast(PrintableComponentLink.PageHeaderFooter, PageHeaderFooter)
            phf.Header.Content.Clear()
            phf.Header.Font = New Font("Times New Roman", 14, FontStyle.Bold)
            phf.Header.LineAlignment = BrickAlignment.Center
            phf.Footer.Font = New Font("Times New Roman", 9.75)
            phf.Footer.LineAlignment = BrickAlignment.Far
            phf.Footer.Content.AddRange(New String() _
        {sWATERMARK, "", Report.REPORT_PAGE & " : [Page # of Pages #]"})

            phf.Header.Content.AddRange(New String() _
{"", "Laporan Penggunaan Farmasi " & vbCrLf & cboTYPE.Text & " Tanggal : " & deDATEFrom.DateTime.ToString("dd/MM/yyyy") & " - " & deDATETo.DateTime.ToString("dd/MM/yyyy"), ""})

            printableComponentLink.Component = grd
            PrintableComponentLink.CreateDocument()
            PrintableComponentLink.ShowPreviewDialog(Me)
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

            Select Case cboTYPE.SelectedIndex
                Case 0
                    fn_LoadData()
                Case 1
                    fn_LoadDataPemakaian()
                Case 2
                    fn_LoadDataPemakaianCovid()
                Case 3
                    fn_LoadDataPemakaianAll()
                Case 4
                    fn_LoadDataPerResepBilling()
                Case 5
                    fn_LoadDataPerTanpaResep()
                Case 6
                    fn_LoadDataPerResepBillingUnit()
                Case 7
                    fn_LoadDataPerTanpaResepBillingUnit()
                Case 8
                    fn_LoadDataPerTanpaResepRawatJalan()
                Case 9
                    fn_LoadDataPerTanpaResepRawatInap()
            End Select

            If cboTYPE.SelectedIndex = 1 Then
                grv.OptionsView.AllowCellMerge = True
            Else
                grv.OptionsView.AllowCellMerge = False
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
            SQL &= "NoTransaksi = A.KDSOTRANSAKSI "
            SQL &= ",NoPendaftaran = B.KDPENDAFTARAN "
            SQL &= ",NoMedrek = (SELECT KDCUSTOMER FROM S_PENDAFTARAN_H WHERE B.KDPENDAFTARAN = KDPENDAFTARAN) "
            SQL &= ",NamaPasien = (SELECT AA.NAME_DISPLAY FROM M_CUSTOMER AA INNER JOIN S_PENDAFTARAN_H BB ON AA.KDCUSTOMER = BB.KDCUSTOMER WHERE B.KDPENDAFTARAN = BB.KDPENDAFTARAN) "
            SQL &= ",Dokter = D.NAME_DISPLAY "
            SQL &= ",Poli = C.NAME_DISPLAY "
            SQL &= ",Transaksi = A.SUBTOTAL "
            SQL &= ",Discount = A.DISCOUNT "
            SQL &= ",Tuslah = A.TUSLAH "
            SQL &= ",AdmRacik = A.TAX "
            SQL &= ",Tunai = ISNULL((SELECT TOP 1 A.GRANDTOTAL FROM F_CASHIN_D AA INNER JOIN F_CASHIN_H BB ON AA.KDCASHIN = BB.KDCASHIN WHERE A.KDSOTRANSAKSI = AA.NOINVOICE AND BB.CATEGORY = 2), 0) "
            SQL &= ",Kredit = ISNULL((SELECT TOP 1 A.GRANDTOTAL FROM F_CASHIN_D AA INNER JOIN F_CASHIN_H BB ON AA.KDCASHIN = BB.KDCASHIN WHERE A.KDSOTRANSAKSI = AA.NOINVOICE AND BB.CATEGORY <> 2), 0) "
            SQL &= ",[User] = A.KDUSER "
            SQL &= ",Shift = A.KDSHIFT "
            SQL &= ",Kategori = 'Resep' "
            SQL &= "FROM S_SO_TRANSAKSI_H A "
            SQL &= "INNER JOIN S_PENDAFTARAN_KUNJUNGAN B "
            SQL &= "ON A.KDKUNJUNGAN = B.KDKUNJUNGAN "
            SQL &= "INNER JOIN M_DEPARTMENT C "
            SQL &= "ON B.KDDEPARTMENT = C.KDDEPARTMENT "
            SQL &= "INNER JOIN M_DOCTOR D "
            SQL &= "ON B.KDDOCTOR = D.KDDOCTOR "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND A.CATEGORY = 4 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "NoTransaksi = A.KDSOTANPARESEP "
            SQL &= ",NoPendaftaran = 'RJ' + A.KDSOTANPARESEP "
            SQL &= ",NoMedrek = '-' "
            SQL &= ",NamaPasien = A.NAMAPASIEN "
            SQL &= ",Dokter = '-' "
            SQL &= ",Poli = '-' "
            SQL &= ",Transaksi = A.SUBTOTAL "
            SQL &= ",Discount = A.DISCOUNT "
            SQL &= ",Tuslah = A.TUSLAH "
            SQL &= ",AdmRacik = A.TAX "
            SQL &= ",Tunai = A.GRANDTOTAL "
            SQL &= ",Kredit = 0 "
            SQL &= ",[User] = A.KDUSER "
            SQL &= ",Shift = A.KDSHIFT "
            SQL &= ",Kategori = 'Tanpa Resep' "
            SQL &= "FROM S_SO_TANPARESEP_H A "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "P_PO_H")

            grd.MainView = grv
            grd.DataSource = ds.Tables("P_PO_H")
            grd.ForceInitialize()

            fn_LoadFormatData()

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatData()
        For iLoop As Integer = 0 To grv.Columns.Count - 1
            If grv.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n0}"
                grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

        'grv.Columns("GRANDTOTAL").SummaryItem.FieldName = "GRANDTOTAL"
        'grv.Columns("GRANDTOTAL").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        'grv.Columns("GRANDTOTAL").SummaryItem.DisplayFormat = "{0:n0}"

        grv.Columns("Kategori").Group()
        grv.Columns("Shift").Group()
        grv.ExpandAllGroups()

        grv.BestFitColumns()

    End Sub
    Private Sub fn_LoadDataPemakaian()
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
            SQL &= "NoTransaksi = A.KDSOTRANSAKSI "
            SQL &= ",NoPendaftaran = B.KDPENDAFTARAN "
            SQL &= ",NoMedrek = (SELECT KDCUSTOMER FROM S_PENDAFTARAN_H WHERE B.KDPENDAFTARAN = KDPENDAFTARAN) "
            SQL &= ",NamaPasien = (SELECT AA.NAME_DISPLAY FROM M_CUSTOMER AA INNER JOIN S_PENDAFTARAN_H BB ON AA.KDCUSTOMER = BB.KDCUSTOMER WHERE B.KDPENDAFTARAN = BB.KDPENDAFTARAN) "
            SQL &= ",C.SEQ "
            SQL &= ",NamaObat = D.NMITEM2 "
            SQL &= ",Qty = C.QTY "
            SQL &= ",GrandTotal = C.GRANDTOTAL "
            SQL &= ",Transaksi = (SELECT CASE C.SEQ WHEN 0 THEN A.SUBTOTAL ELSE 0 END) "
            SQL &= ",Diskon = (SELECT CASE C.SEQ WHEN 0 THEN A.DISCOUNT ELSE 0 END) "
            SQL &= ",Tuslah = (SELECT CASE C.SEQ WHEN 0 THEN A.TUSLAH ELSE 0 END) "
            SQL &= ",AdmRacik = (SELECT CASE C.SEQ WHEN 0 THEN A.TAX ELSE 0 END) "
            SQL &= ",Tunai = ISNULL((SELECT (SELECT CASE C.SEQ WHEN 0 THEN A.GRANDTOTAL ELSE 0 END) FROM F_CASHIN_D AA INNER JOIN F_CASHIN_H BB ON AA.KDCASHIN = BB.KDCASHIN WHERE A.KDSOTRANSAKSI = AA.NOINVOICE AND BB.CATEGORY = 2), 0) "
            SQL &= ",Kredit = ISNULL((SELECT (SELECT CASE C.SEQ WHEN 0 THEN A.GRANDTOTAL ELSE 0 END) FROM F_CASHIN_D AA INNER JOIN F_CASHIN_H BB ON AA.KDCASHIN = BB.KDCASHIN WHERE A.KDSOTRANSAKSI = AA.NOINVOICE AND BB.CATEGORY <> 2), 0) "
            SQL &= ",[User] = A.KDUSER "
            SQL &= ",Shift = A.KDSHIFT "
            SQL &= ",Kategori = 'Resep' "
            SQL &= "FROM S_SO_TRANSAKSI_H A "
            SQL &= "INNER JOIN S_PENDAFTARAN_KUNJUNGAN B "
            SQL &= "ON A.KDKUNJUNGAN = B.KDKUNJUNGAN "
            SQL &= "INNER JOIN S_SO_TRANSAKSI_D C "
            SQL &= "ON A.KDSOTRANSAKSI = C.KDSOTRANSAKSI "
            SQL &= "INNER JOIN M_ITEM D "
            SQL &= "ON C.KDITEM = D.KDITEM "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND A.CATEGORY = 4 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "NoTransaksi = A.KDSOTANPARESEP "
            SQL &= ",NoPendaftaran = 'RJ' + A.KDSOTANPARESEP "
            SQL &= ",NoMedrek = '-' "
            SQL &= ",NamaPasien = A.NAMAPASIEN "
            SQL &= ",C.SEQ "
            SQL &= ",NamaObat = D.NMITEM2 "
            SQL &= ",Qty = C.QTY "
            SQL &= ",GrandTotal = C.GRANDTOTAL "
            SQL &= ",Transaksi = (SELECT CASE C.SEQ WHEN 0 THEN A.SUBTOTAL ELSE 0 END) "
            SQL &= ",Diskon = (SELECT CASE C.SEQ WHEN 0 THEN A.DISCOUNT ELSE 0 END) "
            SQL &= ",Tuslah = (SELECT CASE C.SEQ WHEN 0 THEN A.TUSLAH ELSE 0 END) "
            SQL &= ",AdmRacik = (SELECT CASE C.SEQ WHEN 0 THEN A.TAX ELSE 0 END) "
            SQL &= ",Tunai = A.GRANDTOTAL "
            SQL &= ",Kredit = 0 "
            SQL &= ",[User] = A.KDUSER "
            SQL &= ",Shift = A.KDSHIFT "
            SQL &= ",Kategori = 'Tanpa Resep' "
            SQL &= "FROM S_SO_TANPARESEP_H A "
            SQL &= "INNER JOIN S_SO_TANPARESEP_D C "
            SQL &= "ON A.KDSOTANPARESEP = C.KDSOTANPARESEP "
            SQL &= "INNER JOIN M_ITEM D "
            SQL &= "ON C.KDITEM = D.KDITEM "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "

            SQL &= ") Z "
            SQL &= "ORDER BY "
            SQL &= "Z.[User] "
            SQL &= ",Z.NoTransaksi "
            SQL &= ",Z.NoMedrek "
            SQL &= ",Z.NamaPasien "
            SQL &= ",Z.Kategori "
            SQL &= ",Z.SEQ "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "P_PO_H")

            grd.MainView = grv
            grd.DataSource = ds.Tables("P_PO_H")
            grd.ForceInitialize()

            fn_LoadFormatPemakaian()

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatPemakaian()
        For iLoop As Integer = 0 To grv.Columns.Count - 1
            If grv.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n0}"
                grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

                grv.Columns(iLoop).OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False

            ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"

                grv.Columns(iLoop).OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False

            End If
        Next

        grv.Columns("User").Group()
        grv.Columns("Kategori").Group()
        grv.Columns("Shift").Group()
        grv.ExpandAllGroups()

        grv.BestFitColumns()
    End Sub
    Private Sub fn_LoadDataPemakaianCovid()
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
            SQL &= "TglMasuk = C.DATE "
            SQL &= ",TglKeluar = ISNULL((SELECT convert(varchar, DATE, 110) FROM T_UPDATE_TANGGAL_PULANG WHERE C.KDPENDAFTARAN = KDPENDAFTARAN), '') "
            SQL &= ",NoRM = C.KDCUSTOMER "
            SQL &= ",NamaPasien = F.NAME_DISPLAY "
            SQL &= ",NamaObat = E.NMITEM2 "
            SQL &= ",Jumlah = D.QTY "
            SQL &= ",Harga = D.PRICE "
            SQL &= ",Total = D.GRANDTOTAL "
            SQL &= "FROM S_SO_TRANSAKSI_H A  "
            SQL &= "INNER JOIN S_PENDAFTARAN_KUNJUNGAN B	"
            SQL &= "ON A.KDKUNJUNGAN = B.KDKUNJUNGAN 	"
            SQL &= "INNER JOIN S_PENDAFTARAN_H C 	"
            SQL &= "ON B.KDPENDAFTARAN = C.KDPENDAFTARAN	"
            SQL &= "INNER JOIN S_SO_TRANSAKSI_D D	"
            SQL &= "ON A.KDSOTRANSAKSI = D.KDSOTRANSAKSI	"
            SQL &= "INNER JOIN M_ITEM E	"
            SQL &= "ON D.KDITEM = E.KDITEM	"
            SQL &= "INNER JOIN M_CUSTOMER F "
            SQL &= "ON C.KDCUSTOMER = F.KDCUSTOMER "
            SQL &= "WHERE CONVERT(VARCHAR(8), C.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), C.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND C.KDDAFTAR_L1 ='DAFTAR_L1_0000000003'	"
            SQL &= "AND E.ISSTOK = 1 "


            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "P_PO_H")

            grd.MainView = grv
            grd.DataSource = ds.Tables("P_PO_H")
            grd.ForceInitialize()

            fn_LoadFormatPemakaianCovid()

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatPemakaianCovid()
        For iLoop As Integer = 0 To grv.Columns.Count - 1
            If grv.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n0}"
                grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

                grv.Columns(iLoop).OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False

            ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd-MM-yyyy}"

                grv.Columns(iLoop).OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False

            End If
        Next

        grv.BestFitColumns()
    End Sub
    Private Sub fn_LoadDataPemakaianAll()
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
            SQL &= "JenisDaftar = G.MEMO "
            SQL &= ",NoPendaftaran = C.KDPENDAFTARAN "
            SQL &= ",TglMasuk = C.DATE "
            SQL &= ",TglKeluar = ISNULL((SELECT convert(varchar, DATE, 110) FROM T_UPDATE_TANGGAL_PULANG WHERE C.KDPENDAFTARAN = KDPENDAFTARAN), '') "
            SQL &= ",NoRM = C.KDCUSTOMER "
            SQL &= ",NamaPasien = F.NAME_DISPLAY "
            SQL &= ",NamaObat = E.NMITEM2 "
            SQL &= ",Jumlah = D.QTY "
            SQL &= ",Harga = D.PRICE "
            SQL &= ",Total = D.GRANDTOTAL "
            SQL &= "FROM S_SO_TRANSAKSI_H A  "
            SQL &= "INNER JOIN S_PENDAFTARAN_KUNJUNGAN B	"
            SQL &= "ON A.KDKUNJUNGAN = B.KDKUNJUNGAN 	"
            SQL &= "INNER JOIN S_PENDAFTARAN_H C 	"
            SQL &= "ON B.KDPENDAFTARAN = C.KDPENDAFTARAN	"
            SQL &= "INNER JOIN S_SO_TRANSAKSI_D D	"
            SQL &= "ON A.KDSOTRANSAKSI = D.KDSOTRANSAKSI	"
            SQL &= "INNER JOIN M_ITEM E	"
            SQL &= "ON D.KDITEM = E.KDITEM	"
            SQL &= "INNER JOIN M_CUSTOMER F "
            SQL &= "ON C.KDCUSTOMER = F.KDCUSTOMER "
            SQL &= "INNER JOIN M_DAFTAR_L1 G "
            SQL &= "ON C.KDDAFTAR_L1 = G.KDDAFTAR_L1 "
            SQL &= "WHERE CONVERT(VARCHAR(8), C.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), C.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND E.ISSTOK = 1 "


            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "P_PO_H")

            grd.MainView = grv
            grd.DataSource = ds.Tables("P_PO_H")
            grd.ForceInitialize()

            fn_LoadFormatPemakaianCovid()

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDataPerResepBilling()
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

            SQL = "  SELECT "
            SQL &= " * "
            SQL &= " FROM ("
            SQL &= " SELECT "
            SQL &= " Z.TANGGAL	"
            SQL &= " ,RESEP = SUM(Z.R) "
            SQL &= " ,RETUR = SUM(Z.R_RETUR) "
            SQL &= " ,RESEP_ITEM = SUM(Z.RESEP_ITEM) "
            SQL &= " ,RETUR_ITEM = SUM(Z.RETUR_ITEM) "
            SQL &= " FROM "
            SQL &= " ( "
            SQL &= " SELECT "
            SQL &= " X.TANGGAL  "
            SQL &= " ,R = COUNT(X.KDSOTRANSAKSI) "
            SQL &= " ,RESEP_ITEM = 0 "
            SQL &= " ,R_RETUR = 0 "
            SQL &= " ,RETUR_ITEM = 0 "
            SQL &= " FROM "
            SQL &= " ( "
            SQL &= " SELECT  "
            SQL &= " TANGGAL = CONVERT(varchar, A.DATE, 103) "
            SQL &= " ,A.KDSOTRANSAKSI "
            SQL &= " FROM S_SO_TRANSAKSI_H A "
            SQL &= " WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= " AND A.KDWAREHOUSE <> '' "
            SQL &= " ) X "
            SQL &= " GROUP BY X.TANGGAL "
            SQL &= " UNION "
            SQL &= " SELECT "
            SQL &= " X.TANGGAL  "
            SQL &= " ,R = 0 "
            SQL &= " ,RESEP_ITEM = COUNT(X.KDITEM) "
            SQL &= " ,R_RETUR = 0 "
            SQL &= " ,RETUR_ITEM = 0 "
            SQL &= " FROM "
            SQL &= " ( "
            SQL &= "  "
            SQL &= " SELECT  "
            SQL &= " TANGGAL = CONVERT(varchar, A.DATE, 103) "
            SQL &= " ,B.KDITEM "
            SQL &= " FROM S_SO_TRANSAKSI_H A "
            SQL &= " INNER JOIN S_SO_TRANSAKSI_D B ON A.KDSOTRANSAKSI = B.KDSOTRANSAKSI "
            SQL &= " WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= " AND A.KDWAREHOUSE <> '' "
            SQL &= " ) X "
            SQL &= " GROUP BY X.TANGGAL "
            SQL &= " UNION "
            SQL &= " SELECT "
            SQL &= " X.TANGGAL  "
            SQL &= " ,R = 0 "
            SQL &= " ,RESEP_ITEM = 0 "
            SQL &= " ,R_RETUR = COUNT(X.KDSR) "
            SQL &= " ,RETUR_ITEM = 0 "
            SQL &= " FROM "
            SQL &= " ( "
            SQL &= " SELECT  "
            SQL &= " TANGGAL = CONVERT(varchar, A.DATE, 103) "
            SQL &= " ,A.KDSR "
            SQL &= " FROM S_SR_H A "
            SQL &= " WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= " ) X "
            SQL &= " GROUP BY X.TANGGAL "
            SQL &= " UNION "
            SQL &= " SELECT "
            SQL &= " X.TANGGAL  "
            SQL &= " ,R = 0 "
            SQL &= " ,RESEP_ITEM = 0 "
            SQL &= " ,R_RETUR = 0 "
            SQL &= " ,RETUR_ITEM =  COUNT(X.KDITEM) "
            SQL &= " FROM "
            SQL &= " ( "
            SQL &= "  "
            SQL &= " SELECT  "
            SQL &= " TANGGAL = CONVERT(varchar, A.DATE, 103) "
            SQL &= " ,B.KDITEM "
            SQL &= " FROM S_SR_H A "
            SQL &= " INNER JOIN S_SR_D B ON A.KDSR = B.KDSR "
            SQL &= " WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= " ) X "
            SQL &= " GROUP BY X.TANGGAL "
            SQL &= "  "
            SQL &= " ) Z "
            SQL &= " GROUP BY Z.TANGGAL "
            SQL &= " ) XX "
            SQL &= " ORDER BY XX.TANGGAL "


            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_RESEP")

            grd.MainView = grv
            grd.DataSource = ds.Tables("S_RESEP")
            grd.ForceInitialize()

            fn_LoadFormatPemakaianCovid()

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDataPerTanpaResep()
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

            SQL = "  SELECT "
            SQL &= " * "
            SQL &= " FROM ("
            SQL &= " SELECT "
            SQL &= " Z.TANGGAL	"
            SQL &= " ,RESEP = SUM(Z.R) "
            SQL &= " ,RETUR = SUM(Z.R_RETUR) "
            SQL &= " ,RESEP_ITEM = SUM(Z.RESEP_ITEM) "
            SQL &= " ,RETUR_ITEM = SUM(Z.RETUR_ITEM) "
            SQL &= " FROM "
            SQL &= " ( "
            SQL &= " SELECT "
            SQL &= " X.TANGGAL  "
            SQL &= " ,R = COUNT(X.KDSOTANPARESEP) "
            SQL &= " ,RESEP_ITEM = 0 "
            SQL &= " ,R_RETUR = 0 "
            SQL &= " ,RETUR_ITEM = 0 "
            SQL &= " FROM "
            SQL &= " ( "
            SQL &= " SELECT  "
            SQL &= " TANGGAL = CONVERT(varchar, A.DATE, 103) "
            SQL &= " ,A.KDSOTANPARESEP "
            SQL &= " FROM S_SO_TANPARESEP_H A "
            SQL &= " WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= " ) X "
            SQL &= " GROUP BY X.TANGGAL "
            SQL &= " UNION "
            SQL &= " SELECT "
            SQL &= " X.TANGGAL  "
            SQL &= " ,R = 0 "
            SQL &= " ,RESEP_ITEM = COUNT(X.KDITEM) "
            SQL &= " ,R_RETUR = 0 "
            SQL &= " ,RETUR_ITEM = 0 "
            SQL &= " FROM "
            SQL &= " ( "
            SQL &= "  "
            SQL &= " SELECT  "
            SQL &= " TANGGAL = CONVERT(varchar, A.DATE, 103) "
            SQL &= " ,B.KDITEM "
            SQL &= " FROM S_SO_TANPARESEP_H A "
            SQL &= " INNER JOIN S_SO_TANPARESEP_D B ON A.KDSOTANPARESEP = B.KDSOTANPARESEP "
            SQL &= " WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= " ) X "
            SQL &= " GROUP BY X.TANGGAL "
            SQL &= " UNION "
            SQL &= " SELECT "
            SQL &= " X.TANGGAL  "
            SQL &= " ,R = 0 "
            SQL &= " ,RESEP_ITEM = 0 "
            SQL &= " ,R_RETUR = COUNT(X.KDSR) "
            SQL &= " ,RETUR_ITEM = 0 "
            SQL &= " FROM "
            SQL &= " ( "
            SQL &= " SELECT  "
            SQL &= " TANGGAL = CONVERT(varchar, A.DATE, 103) "
            SQL &= " ,A.KDSR "
            SQL &= " FROM S_SR_H A "
            SQL &= " WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= " ) X "
            SQL &= " GROUP BY X.TANGGAL "
            SQL &= " UNION "
            SQL &= " SELECT "
            SQL &= " X.TANGGAL  "
            SQL &= " ,R = 0 "
            SQL &= " ,RESEP_ITEM = 0 "
            SQL &= " ,R_RETUR = 0 "
            SQL &= " ,RETUR_ITEM =  COUNT(X.KDITEM) "
            SQL &= " FROM "
            SQL &= " ( "
            SQL &= "  "
            SQL &= " SELECT  "
            SQL &= " TANGGAL = CONVERT(varchar, A.DATE, 103) "
            SQL &= " ,B.KDITEM "
            SQL &= " FROM S_SR_H A "
            SQL &= " INNER JOIN S_SR_D B ON A.KDSR = B.KDSR "
            SQL &= " WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= " ) X "
            SQL &= " GROUP BY X.TANGGAL "
            SQL &= "  "
            SQL &= " ) Z "
            SQL &= " GROUP BY Z.TANGGAL "
            SQL &= " ) XX "
            SQL &= " ORDER BY XX.TANGGAL "


            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_RESEP")

            grd.MainView = grv
            grd.DataSource = ds.Tables("S_RESEP")
            grd.ForceInitialize()

            fn_LoadFormatPemakaianCovid()

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDataPerResepBillingUnit()
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

            SQL = "  SELECT "
            SQL &= " * "
            SQL &= " FROM ("
            SQL &= " SELECT "
            SQL &= " Z.TANGGAL	"
            SQL &= " ,UNIT = (SELECT NAME_DISPLAY FROM M_WAREHOUSE WHERE Z.KDWAREHOUSE = KDWAREHOUSE) "
            SQL &= " ,RESEP = SUM(Z.R) "
            SQL &= " ,RETUR = SUM(Z.R_RETUR) "
            SQL &= " ,RESEP_ITEM = SUM(Z.RESEP_ITEM) "
            SQL &= " ,RETUR_ITEM = SUM(Z.RETUR_ITEM) "
            SQL &= " FROM "
            SQL &= " ( "
            SQL &= " SELECT "
            SQL &= " X.TANGGAL  "
            SQL &= " ,X.KDWAREHOUSE "
            SQL &= " ,R = COUNT(X.KDSOTRANSAKSI) "
            SQL &= " ,RESEP_ITEM = 0 "
            SQL &= " ,R_RETUR = 0 "
            SQL &= " ,RETUR_ITEM = 0 "
            SQL &= " FROM "
            SQL &= " ( "
            SQL &= " SELECT  "
            SQL &= " TANGGAL = CONVERT(varchar, A.DATE, 103) "
            SQL &= " ,A.KDWAREHOUSE "
            SQL &= " ,A.KDSOTRANSAKSI "
            SQL &= " FROM S_SO_TRANSAKSI_H A "
            SQL &= " WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= " AND A.KDWAREHOUSE <> '' "
            SQL &= " ) X "
            SQL &= " GROUP BY X.TANGGAL, X.KDWAREHOUSE "
            SQL &= " UNION "
            SQL &= " SELECT "
            SQL &= " X.TANGGAL  "
            SQL &= " ,X.KDWAREHOUSE "
            SQL &= " ,R = 0 "
            SQL &= " ,RESEP_ITEM = COUNT(X.KDITEM) "
            SQL &= " ,R_RETUR = 0 "
            SQL &= " ,RETUR_ITEM = 0 "
            SQL &= " FROM "
            SQL &= " ( "
            SQL &= "  "
            SQL &= " SELECT  "
            SQL &= " TANGGAL = CONVERT(varchar, A.DATE, 103) "
            SQL &= " ,A.KDWAREHOUSE "
            SQL &= " ,B.KDITEM "
            SQL &= " FROM S_SO_TRANSAKSI_H A "
            SQL &= " INNER JOIN S_SO_TRANSAKSI_D B ON A.KDSOTRANSAKSI = B.KDSOTRANSAKSI "
            SQL &= " WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= " AND A.KDWAREHOUSE <> '' "
            SQL &= " ) X "
            SQL &= " GROUP BY X.TANGGAL, X.KDWAREHOUSE "
            SQL &= " UNION "
            SQL &= " SELECT "
            SQL &= " X.TANGGAL  "
            SQL &= " ,X.KDWAREHOUSE "
            SQL &= " ,R = 0 "
            SQL &= " ,RESEP_ITEM = 0 "
            SQL &= " ,R_RETUR = COUNT(X.KDSR) "
            SQL &= " ,RETUR_ITEM = 0 "
            SQL &= " FROM "
            SQL &= " ( "
            SQL &= " SELECT  "
            SQL &= " TANGGAL = CONVERT(varchar, A.DATE, 103) "
            SQL &= " ,A.KDWAREHOUSE "
            SQL &= " ,A.KDSR "
            SQL &= " FROM S_SR_H A "
            SQL &= " WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= " ) X "
            SQL &= " GROUP BY X.TANGGAL, X.KDWAREHOUSE "
            SQL &= " UNION "
            SQL &= " SELECT "
            SQL &= " X.TANGGAL  "
            SQL &= " ,X.KDWAREHOUSE "
            SQL &= " ,R = 0 "
            SQL &= " ,RESEP_ITEM = 0 "
            SQL &= " ,R_RETUR = 0 "
            SQL &= " ,RETUR_ITEM =  COUNT(X.KDITEM) "
            SQL &= " FROM "
            SQL &= " ( "
            SQL &= "  "
            SQL &= " SELECT  "
            SQL &= " TANGGAL = CONVERT(varchar, A.DATE, 103) "
            SQL &= " ,A.KDWAREHOUSE "
            SQL &= " ,B.KDITEM "
            SQL &= " FROM S_SR_H A "
            SQL &= " INNER JOIN S_SR_D B ON A.KDSR = B.KDSR "
            SQL &= " WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= " ) X "
            SQL &= " GROUP BY X.TANGGAL, X.KDWAREHOUSE "
            SQL &= "  "
            SQL &= " ) Z "
            SQL &= " GROUP BY Z.TANGGAL, Z.KDWAREHOUSE "
            SQL &= " ) XX "
            SQL &= " ORDER BY XX.TANGGAL "


            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_RESEP")

            grd.MainView = grv
            grd.DataSource = ds.Tables("S_RESEP")
            grd.ForceInitialize()

            fn_LoadFormatPemakaianCovid()

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDataPerTanpaResepBillingUnit()
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

            SQL = "  SELECT "
            SQL &= " * "
            SQL &= " FROM ("
            SQL &= " SELECT "
            SQL &= " Z.TANGGAL	"
            SQL &= " ,UNIT = (SELECT NAME_DISPLAY FROM M_WAREHOUSE WHERE Z.KDWAREHOUSE = KDWAREHOUSE) "
            SQL &= " ,RESEP = SUM(Z.R) "
            SQL &= " ,RETUR = SUM(Z.R_RETUR) "
            SQL &= " ,RESEP_ITEM = SUM(Z.RESEP_ITEM) "
            SQL &= " ,RETUR_ITEM = SUM(Z.RETUR_ITEM) "
            SQL &= " FROM "
            SQL &= " ( "
            SQL &= " SELECT "
            SQL &= " X.TANGGAL  "
            SQL &= " ,X.KDWAREHOUSE "
            SQL &= " ,R = COUNT(X.KDSOTANPARESEP) "
            SQL &= " ,RESEP_ITEM = 0 "
            SQL &= " ,R_RETUR = 0 "
            SQL &= " ,RETUR_ITEM = 0 "
            SQL &= " FROM "
            SQL &= " ( "
            SQL &= " SELECT  "
            SQL &= " TANGGAL = CONVERT(varchar, A.DATE, 103) "
            SQL &= " ,A.KDWAREHOUSE "
            SQL &= " ,A.KDSOTANPARESEP "
            SQL &= " FROM S_SO_TANPARESEP_H A "
            SQL &= " WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= " AND A.KDWAREHOUSE <> '' "
            SQL &= " ) X "
            SQL &= " GROUP BY X.TANGGAL, X.KDWAREHOUSE "
            SQL &= " UNION "
            SQL &= " SELECT "
            SQL &= " X.TANGGAL  "
            SQL &= " ,X.KDWAREHOUSE "
            SQL &= " ,R = 0 "
            SQL &= " ,RESEP_ITEM = COUNT(X.KDITEM) "
            SQL &= " ,R_RETUR = 0 "
            SQL &= " ,RETUR_ITEM = 0 "
            SQL &= " FROM "
            SQL &= " ( "
            SQL &= "  "
            SQL &= " SELECT  "
            SQL &= " TANGGAL = CONVERT(varchar, A.DATE, 103) "
            SQL &= " ,A.KDWAREHOUSE "
            SQL &= " ,B.KDITEM "
            SQL &= " FROM S_SO_TANPARESEP_H A "
            SQL &= " INNER JOIN S_SO_TANPARESEP_D B ON A.KDSOTANPARESEP = B.KDSOTANPARESEP "
            SQL &= " WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= " AND A.KDWAREHOUSE <> '' "
            SQL &= " ) X "
            SQL &= " GROUP BY X.TANGGAL, X.KDWAREHOUSE "
            SQL &= " UNION "
            SQL &= " SELECT "
            SQL &= " X.TANGGAL  "
            SQL &= " ,X.KDWAREHOUSE "
            SQL &= " ,R = 0 "
            SQL &= " ,RESEP_ITEM = 0 "
            SQL &= " ,R_RETUR = COUNT(X.KDSR) "
            SQL &= " ,RETUR_ITEM = 0 "
            SQL &= " FROM "
            SQL &= " ( "
            SQL &= " SELECT  "
            SQL &= " TANGGAL = CONVERT(varchar, A.DATE, 103) "
            SQL &= " ,A.KDWAREHOUSE "
            SQL &= " ,A.KDSR "
            SQL &= " FROM S_SR_H A "
            SQL &= " WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= " ) X "
            SQL &= " GROUP BY X.TANGGAL, X.KDWAREHOUSE "
            SQL &= " UNION "
            SQL &= " SELECT "
            SQL &= " X.TANGGAL  "
            SQL &= " ,X.KDWAREHOUSE "
            SQL &= " ,R = 0 "
            SQL &= " ,RESEP_ITEM = 0 "
            SQL &= " ,R_RETUR = 0 "
            SQL &= " ,RETUR_ITEM =  COUNT(X.KDITEM) "
            SQL &= " FROM "
            SQL &= " ( "
            SQL &= "  "
            SQL &= " SELECT  "
            SQL &= " TANGGAL = CONVERT(varchar, A.DATE, 103) "
            SQL &= " ,A.KDWAREHOUSE "
            SQL &= " ,B.KDITEM "
            SQL &= " FROM S_SR_H A "
            SQL &= " INNER JOIN S_SR_D B ON A.KDSR = B.KDSR "
            SQL &= " WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= " ) X "
            SQL &= " GROUP BY X.TANGGAL, X.KDWAREHOUSE "
            SQL &= "  "
            SQL &= " ) Z "
            SQL &= " GROUP BY Z.TANGGAL, Z.KDWAREHOUSE "
            SQL &= " ) XX "
            SQL &= " ORDER BY XX.TANGGAL "


            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_RESEP")

            grd.MainView = grv
            grd.DataSource = ds.Tables("S_RESEP")
            grd.ForceInitialize()

            fn_LoadFormatPemakaianCovid()

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDataPerTanpaResepRawatJalan()
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

            SQL = "  SELECT "
            SQL &= " * "
            SQL &= " FROM ("
            SQL &= " SELECT "
            SQL &= " Z.TANGGAL	"
            SQL &= " ,RESEP = SUM(Z.R) "
            SQL &= " ,RETUR = SUM(Z.R_RETUR) "
            SQL &= " ,RESEP_ITEM = SUM(Z.RESEP_ITEM) "
            SQL &= " ,RETUR_ITEM = SUM(Z.RETUR_ITEM) "
            SQL &= " FROM "
            SQL &= " ( "
            SQL &= " SELECT "
            SQL &= " X.TANGGAL  "
            SQL &= " ,R = COUNT(X.KDSOTRANSAKSI) "
            SQL &= " ,RESEP_ITEM = 0 "
            SQL &= " ,R_RETUR = 0 "
            SQL &= " ,RETUR_ITEM = 0 "
            SQL &= " FROM "
            SQL &= " ( "
            SQL &= " SELECT  "
            SQL &= " TANGGAL = CONVERT(varchar, A.DATE, 103) "
            SQL &= " ,A.KDSOTRANSAKSI "
            SQL &= " FROM S_SO_TRANSAKSI_H A "
            SQL &= " WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= " AND A.KDWAREHOUSE <> '' "
            SQL &= " AND A.KDKUNJUNGAN LIKE '%RJ%' "
            SQL &= " ) X "
            SQL &= " GROUP BY X.TANGGAL "
            SQL &= " UNION "
            SQL &= " SELECT "
            SQL &= " X.TANGGAL  "
            SQL &= " ,R = 0 "
            SQL &= " ,RESEP_ITEM = COUNT(X.KDITEM) "
            SQL &= " ,R_RETUR = 0 "
            SQL &= " ,RETUR_ITEM = 0 "
            SQL &= " FROM "
            SQL &= " ( "
            SQL &= "  "
            SQL &= " SELECT  "
            SQL &= " TANGGAL = CONVERT(varchar, A.DATE, 103) "
            SQL &= " ,B.KDITEM "
            SQL &= " FROM S_SO_TRANSAKSI_H A "
            SQL &= " INNER JOIN S_SO_TRANSAKSI_D B ON A.KDSOTRANSAKSI = B.KDSOTRANSAKSI "
            SQL &= " WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= " AND A.KDWAREHOUSE <> '' "
            SQL &= " AND A.KDKUNJUNGAN LIKE '%RJ%' "
            SQL &= " ) X "
            SQL &= " GROUP BY X.TANGGAL "
            SQL &= " UNION "
            SQL &= " SELECT "
            SQL &= " X.TANGGAL  "
            SQL &= " ,R = 0 "
            SQL &= " ,RESEP_ITEM = 0 "
            SQL &= " ,R_RETUR = COUNT(X.KDSR) "
            SQL &= " ,RETUR_ITEM = 0 "
            SQL &= " FROM "
            SQL &= " ( "
            SQL &= " SELECT  "
            SQL &= " TANGGAL = CONVERT(varchar, A.DATE, 103) "
            SQL &= " ,A.KDSR "
            SQL &= " FROM S_SR_H A "
            SQL &= " WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= " ) X "
            SQL &= " GROUP BY X.TANGGAL "
            SQL &= " UNION "
            SQL &= " SELECT "
            SQL &= " X.TANGGAL  "
            SQL &= " ,R = 0 "
            SQL &= " ,RESEP_ITEM = 0 "
            SQL &= " ,R_RETUR = 0 "
            SQL &= " ,RETUR_ITEM =  COUNT(X.KDITEM) "
            SQL &= " FROM "
            SQL &= " ( "
            SQL &= "  "
            SQL &= " SELECT  "
            SQL &= " TANGGAL = CONVERT(varchar, A.DATE, 103) "
            SQL &= " ,B.KDITEM "
            SQL &= " FROM S_SR_H A "
            SQL &= " INNER JOIN S_SR_D B ON A.KDSR = B.KDSR "
            SQL &= " WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= " ) X "
            SQL &= " GROUP BY X.TANGGAL "
            SQL &= "  "
            SQL &= " ) Z "
            SQL &= " GROUP BY Z.TANGGAL "
            SQL &= " ) XX "
            SQL &= " ORDER BY XX.TANGGAL "


            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_RESEP")

            grd.MainView = grv
            grd.DataSource = ds.Tables("S_RESEP")
            grd.ForceInitialize()

            fn_LoadFormatPemakaianCovid()

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDataPerTanpaResepRawatInap()
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

            SQL = "  SELECT "
            SQL &= " * "
            SQL &= " FROM ("
            SQL &= " SELECT "
            SQL &= " Z.TANGGAL	"
            SQL &= " ,RESEP = SUM(Z.R) "
            SQL &= " ,RETUR = SUM(Z.R_RETUR) "
            SQL &= " ,RESEP_ITEM = SUM(Z.RESEP_ITEM) "
            SQL &= " ,RETUR_ITEM = SUM(Z.RETUR_ITEM) "
            SQL &= " FROM "
            SQL &= " ( "
            SQL &= " SELECT "
            SQL &= " X.TANGGAL  "
            SQL &= " ,R = COUNT(X.KDSOTRANSAKSI) "
            SQL &= " ,RESEP_ITEM = 0 "
            SQL &= " ,R_RETUR = 0 "
            SQL &= " ,RETUR_ITEM = 0 "
            SQL &= " FROM "
            SQL &= " ( "
            SQL &= " SELECT  "
            SQL &= " TANGGAL = CONVERT(varchar, A.DATE, 103) "
            SQL &= " ,A.KDSOTRANSAKSI "
            SQL &= " FROM S_SO_TRANSAKSI_H A "
            SQL &= " WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= " AND A.KDWAREHOUSE <> '' "
            SQL &= " AND A.KDKUNJUNGAN LIKE '%RI%' "
            SQL &= " ) X "
            SQL &= " GROUP BY X.TANGGAL "
            SQL &= " UNION "
            SQL &= " SELECT "
            SQL &= " X.TANGGAL  "
            SQL &= " ,R = 0 "
            SQL &= " ,RESEP_ITEM = COUNT(X.KDITEM) "
            SQL &= " ,R_RETUR = 0 "
            SQL &= " ,RETUR_ITEM = 0 "
            SQL &= " FROM "
            SQL &= " ( "
            SQL &= "  "
            SQL &= " SELECT  "
            SQL &= " TANGGAL = CONVERT(varchar, A.DATE, 103) "
            SQL &= " ,B.KDITEM "
            SQL &= " FROM S_SO_TRANSAKSI_H A "
            SQL &= " INNER JOIN S_SO_TRANSAKSI_D B ON A.KDSOTRANSAKSI = B.KDSOTRANSAKSI "
            SQL &= " WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= " AND A.KDWAREHOUSE <> '' "
            SQL &= " AND A.KDKUNJUNGAN LIKE '%RI%' "
            SQL &= " ) X "
            SQL &= " GROUP BY X.TANGGAL "
            SQL &= " UNION "
            SQL &= " SELECT "
            SQL &= " X.TANGGAL  "
            SQL &= " ,R = 0 "
            SQL &= " ,RESEP_ITEM = 0 "
            SQL &= " ,R_RETUR = COUNT(X.KDSR) "
            SQL &= " ,RETUR_ITEM = 0 "
            SQL &= " FROM "
            SQL &= " ( "
            SQL &= " SELECT  "
            SQL &= " TANGGAL = CONVERT(varchar, A.DATE, 103) "
            SQL &= " ,A.KDSR "
            SQL &= " FROM S_SR_H A "
            SQL &= " WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= " ) X "
            SQL &= " GROUP BY X.TANGGAL "
            SQL &= " UNION "
            SQL &= " SELECT "
            SQL &= " X.TANGGAL  "
            SQL &= " ,R = 0 "
            SQL &= " ,RESEP_ITEM = 0 "
            SQL &= " ,R_RETUR = 0 "
            SQL &= " ,RETUR_ITEM =  COUNT(X.KDITEM) "
            SQL &= " FROM "
            SQL &= " ( "
            SQL &= "  "
            SQL &= " SELECT  "
            SQL &= " TANGGAL = CONVERT(varchar, A.DATE, 103) "
            SQL &= " ,B.KDITEM "
            SQL &= " FROM S_SR_H A "
            SQL &= " INNER JOIN S_SR_D B ON A.KDSR = B.KDSR "
            SQL &= " WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= " ) X "
            SQL &= " GROUP BY X.TANGGAL "
            SQL &= "  "
            SQL &= " ) Z "
            SQL &= " GROUP BY Z.TANGGAL "
            SQL &= " ) XX "
            SQL &= " ORDER BY XX.TANGGAL "


            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_RESEP")

            grd.MainView = grv
            grd.DataSource = ds.Tables("S_RESEP")
            grd.ForceInitialize()

            fn_LoadFormatPemakaianCovid()

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatPemakaianAll()
        For iLoop As Integer = 0 To grv.Columns.Count - 1
            If grv.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n0}"
                grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

                grv.Columns(iLoop).OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False

            ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd-MM-yyyy}"

                grv.Columns(iLoop).OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False

            End If
        Next

        grv.BestFitColumns()
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

        'Try
        '    grv.RestoreLayoutFromRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text & "\" & cboTYPE.Text)
        'Catch ex As Exception

        'End Try
    End Sub
#End Region
End Class