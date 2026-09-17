Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Imports DevExpress.XtraPrinting

Imports System.Data
Imports System.Data.SqlClient


Public Class frmReportMutation
    Implements ILanguage

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = Mutation.TITLE_REPORT

        lTYPE.Text = Report.FILTER_TYPE
        lDATEFROM.Text = Report.FILTER_DATEFROM
        lDATETO.Text = Report.FILTER_DATETO
        lWAREHOUSE.Text = Warehouse.TITLE

        cboTYPE.Properties.Items.Clear()
        cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_01)
        cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_02)
        cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_03)
        cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_04)
        cboTYPE.Properties.Items.Add("Mutasi Gudang Adjustment")
        cboTYPE.Properties.Items.Add("Mutasi Gudang Non Adjustment")
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
            Me.Text = Mutation.TITLE_REPORT

            lTYPE.Text = Report.FILTER_TYPE
            lDATEFROM.Text = Report.FILTER_DATEFROM
            lDATETO.Text = Report.FILTER_DATETO

            cboTYPE.Properties.Items.Clear()
            cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_01)
            cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_02)
            cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_03)
            cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_04)
            cboTYPE.Properties.Items.Add("Mutasi Gudang Adjustment")
            cboTYPE.Properties.Items.Add("Mutasi Gudang Non Adjustment")

            If cboTYPE.SelectedIndex = 3 Then
                fn_LoadLanguageAll()
            ElseIf cboTYPE.SelectedIndex = 4 Then
                fn_LoadLanguageMutasiGudangAdjustment()
            ElseIf cboTYPE.SelectedIndex = 5 Then
                fn_LoadLanguageMutasiGudangNonAdjustment()
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
                     Where x.MODUL = "MUTATION_R" _
                     And y.KDUSER = sUserID _
                     Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            'If sUserID = "ADMINISTRATOR" Or sUserID = "DEMO" Then
            '    picConfirm.Visible = True
            '    lConfirm.Visible = True
            'Else
            '    picConfirm.Visible = False
            '    lConfirm.Visible = False
            'End If

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
{"", Mutation.TITLE_REPORT & vbCrLf & deDATEFrom.DateTime.ToString("dd/MM/yyyy") & " - " & deDATETo.DateTime.ToString("dd/MM/yyyy"), ""})

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

            If deDATEFrom.DateTime > deDATETo.DateTime Then
                MsgBox("Tanggal Dari Lebih Besar dari Tanggal Sampai", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            If cboTYPE.SelectedIndex = 3 Then
                lWAREHOUSE.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lALL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                fn_LoadDataAll()
            ElseIf cboTYPE.SelectedIndex = 4 Then
                lWAREHOUSE.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lALL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                fn_LoadWarehouse()
                fn_MutasiGudangAdjustment()
            ElseIf cboTYPE.SelectedIndex = 5 Then
                lWAREHOUSE.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lALL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                fn_LoadWarehouse()
                fn_MutasiGudangNonAdjustment()
            Else
                lWAREHOUSE.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lALL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                fn_LoadData()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadWarehouse()
        Dim oWAREHOUSE As New Reference.clsWarehouse
        Try
            grdKDWAREHOUSE.Properties.DataSource = oWAREHOUSE.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDWAREHOUSE.Properties.ValueMember = "KDWAREHOUSE"
            grdKDWAREHOUSE.Properties.DisplayMember = "NAME_DISPLAY"
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
            SQL &= "A.KDMUTATION "
            SQL &= ",A.DATE "
            SQL &= ",KDWAREHOUSEFROM = B.NAME_DISPLAY "
            SQL &= ",KDWAREHOUSETO = C.NAME_DISPLAY "
            SQL &= ",A.MEMO "

            SQL &= ",A.KDUSER "
            SQL &= "FROM I_MUTATION_H A "
            SQL &= "INNER JOIN M_WAREHOUSE B "
            SQL &= "ON A.KDWAREHOUSEFROM = B.KDWAREHOUSE "
            SQL &= "INNER JOIN M_WAREHOUSE C "
            SQL &= "ON A.KDWAREHOUSETO = C.KDWAREHOUSE "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "ORDER BY DATE, KDMUTATION DESC"

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "I_MUTATION_H")

            SQL = "SELECT "
            SQL &= "B.KDMUTATION "
            SQL &= ",C.NMITEM1 "
            SQL &= ",C.NMITEM2 "
            SQL &= ",C.NMITEM3 "
            SQL &= ",B.QTY "
            SQL &= ",KDUOM = D.MEMO "
            SQL &= ",B.REMARKS "
            SQL &= "FROM I_MUTATION_H A "
            SQL &= "INNER JOIN I_MUTATION_D B "
            SQL &= "ON A.KDMUTATION = B.KDMUTATION "
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
            da.Fill(ds, "I_MUTATION_D")

            If cboTYPE.SelectedIndex = 0 Then
                Dim keyColumn As DataColumn = ds.Tables("I_MUTATION_H").Columns("KDMUTATION")
                Dim foreignKeyColumn As DataColumn = ds.Tables("I_MUTATION_D").Columns("KDMUTATION")
                ds.Relations.Add("FK_RELATION", keyColumn, foreignKeyColumn)

                grd.MainView = grv
                grd.DataSource = ds.Tables("I_MUTATION_H")
                grd.ForceInitialize()

                grd.LevelTree.Nodes.Add("FK_RELATION", grv1)
                grv1.ViewCaption = "Details"

                grv1.PopulateColumns(ds.Tables("I_MUTATION_D"))
                grv1.Columns("KDMUTATION").VisibleIndex = -1

                fn_LoadFormatData()
                fn_LoadFormatDataDetail()
            ElseIf cboTYPE.SelectedIndex = 1 Then
                grd.MainView = grv
                grd.DataSource = ds.Tables("I_MUTATION_H")
                grd.ForceInitialize()

                fn_LoadFormatData()
            ElseIf cboTYPE.SelectedIndex = 2 Then
                grd.MainView = grv1
                grd.DataSource = ds.Tables("I_MUTATION_D")
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
            If grv1.Columns(iLoop).FieldName = "QTY" Then
                grv1.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv1.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv1.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            End If
        Next
    End Sub
    Public Sub fn_LoadLanguageMaster()
        Try
            grv.Columns("KDMUTATION").Caption = Mutation.KDMUTATION
            grv.Columns("DATE").Caption = Mutation.TANGGAL
            grv.Columns("KDWAREHOUSEFROM").Caption = Mutation.KDWAREHOUSEFROM
            grv.Columns("KDWAREHOUSETO").Caption = Mutation.KDWAREHOUSETO
            grv.Columns("MEMO").Caption = Mutation.MEMO
            grv.Columns("KDUSER").Caption = Caption.User

            MasterColumnChooserToolStripMenuItem.Text = Caption.ColumnChooserMaster
            DetailColumnChooserToolStripMenuItem.Text = Caption.ColumnChooserDetail
        Catch oErr As Exception

        End Try
    End Sub
    Public Sub fn_LoadLanguageDetail()
        Try
            grv1.Columns("QTY").Caption = Mutation.DETAIL_QTY
            grv1.Columns("REMARKS").Caption = Mutation.DETAIL_REMARKS

            grv1.Columns("NMITEM1").Caption = Item.NMITEM1_2
            grv1.Columns("NMITEM2").Caption = Item.NMITEM2_2
            grv1.Columns("NMITEM3").Caption = Item.NMITEM3_2
            grv1.Columns("KDUOM").Caption = Mutation.DETAIL_KDUOM
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
            SQL &= "A.KDMUTATION "
            SQL &= ",A.DATE "
            SQL &= ",KDWAREHOUSEFROM = B.NAME_DISPLAY "
            SQL &= ",KDWAREHOUSETO = C.NAME_DISPLAY "
            SQL &= ",A.MEMO "

            SQL &= ",A.KDUSER "
            SQL &= "FROM I_MUTATION_H A "
            SQL &= "INNER JOIN M_WAREHOUSE B "
            SQL &= "ON A.KDWAREHOUSEFROM = B.KDWAREHOUSE "
            SQL &= "INNER JOIN M_WAREHOUSE C "
            SQL &= "ON A.KDWAREHOUSETO = C.KDWAREHOUSE "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= ") AS A "

            SQL &= "INNER JOIN "
            SQL &= "( "
            SQL &= "SELECT "
            SQL &= "B.KDMUTATION "
            SQL &= ",C.NMITEM1 "
            SQL &= ",C.NMITEM2 "
            SQL &= ",C.NMITEM3 "
            SQL &= ",B.QTY "
            SQL &= ",KDUOM = D.MEMO "
            SQL &= ",B.REMARKS "
            SQL &= "FROM I_MUTATION_H A "
            SQL &= "INNER JOIN I_MUTATION_D B "
            SQL &= "ON A.KDMUTATION = B.KDMUTATION "
            SQL &= "INNER JOIN M_ITEM C "
            SQL &= "ON B.KDITEM = C.KDITEM "
            SQL &= "INNER JOIN M_UOM D "
            SQL &= "ON B.KDUOM = D.KDUOM "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= ") AS B "

            SQL &= "ON A.KDMUTATION = B.KDMUTATION "
            SQL &= "ORDER BY A.KDMUTATION DESC "

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

        grv.Columns("KDMUTATION1").Visible = False
        grv.Columns("KDMUTATION1").OptionsColumn.ShowInCustomizationForm = False
    End Sub
    Public Sub fn_LoadLanguageAll()
        Try
            grv.Columns("KDMUTATION").Caption = Mutation.KDMUTATION
            grv.Columns("DATE").Caption = Mutation.TANGGAL
            grv.Columns("KDWAREHOUSEFROM").Caption = Mutation.KDWAREHOUSEFROM
            grv.Columns("KDWAREHOUSETO").Caption = Mutation.KDWAREHOUSETO
            grv.Columns("MEMO").Caption = Mutation.MEMO
            grv.Columns("KDUSER").Caption = Caption.User

            grv.Columns("QTY").Caption = Mutation.DETAIL_QTY
            grv.Columns("REMARKS").Caption = Mutation.DETAIL_REMARKS

            grv.Columns("NMITEM1").Caption = Item.NMITEM1_2
            grv.Columns("NMITEM2").Caption = Item.NMITEM2_2
            grv.Columns("NMITEM3").Caption = Item.NMITEM3_2
            grv.Columns("KDUOM").Caption = Mutation.DETAIL_KDUOM

            MasterColumnChooserToolStripMenuItem.Text = Caption.ColumnChooserMaster
            DetailColumnChooserToolStripMenuItem.Text = Caption.ColumnChooserDetail
        Catch oErr As Exception

        End Try
    End Sub
#End Region
#Region "Permintaan"
    Private Sub fn_MutasiGudangAdjustment()
        Try
            If grdKDWAREHOUSE.Text = String.Empty Then Exit Sub

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

            SQL = "	SELECT "
            SQL &= "JENIS = ZZZZ.MEMO "
            SQL &= ",Z.NMITEM2 "
            SQL &= ",SATUAN = ZZZ.MEMO "
            SQL &= ",HARGA = ZZ.PRICEPURCHASESTANDARD "
            SQL &= ",HARGA_PPN = (ZZ.PRICEPURCHASESTANDARD * 0.1) + ZZ.PRICEPURCHASESTANDARD "
            If chkALL.Checked = True Then
                SQL &= ",Z.KDITEM "
                SQL &= ",ZZZ.KDUOM "
                SQL &= ",BELI_X "
                SQL &= ",MUTASIKELUAR_X "
                SQL &= ",MUTASIMASUK_X "
                SQL &= ",PENJUALAN_X "
                SQL &= ",OPNAME_X "
                SQL &= ",ADJUSMENT_X "
                SQL &= ",STOKAWAL = "
                SQL &= " BELI_X "
                SQL &= " + MUTASIKELUAR_X "
                SQL &= " + MUTASIMASUK_X "
                SQL &= " + PENJUALAN_X "
                SQL &= " + OPNAME_X "
                SQL &= " + ADJUSMENT_X "
                SQL &= ",BELI "
                SQL &= ",MUTASIKELUAR "
                SQL &= ",MUTASIMASUK "
                SQL &= ",PENJUALAN "
                SQL &= ",OPNAME "
                SQL &= ",ADJUSMENT "
                SQL &= ",STOKAKHIR = "
                SQL &= " ( "
                SQL &= " BELI_X "
                SQL &= " + MUTASIKELUAR_X "
                SQL &= " + MUTASIMASUK_X "
                SQL &= " + PENJUALAN_X "
                SQL &= " + OPNAME_X "
                SQL &= " + ADJUSMENT_X "
                SQL &= " ) + "
                SQL &= " ( "
                SQL &= " BELI "
                SQL &= " + MUTASIKELUAR "
                SQL &= " + MUTASIMASUK "
                SQL &= " + PENJUALAN "
                SQL &= " + OPNAME "
                SQL &= " + ADJUSMENT "
                SQL &= " ) "
                SQL &= ",MONITORING "
                SQL &= ",SELISIH = "
                SQL &= " ( "
                SQL &= " BELI_X "
                SQL &= " + MUTASIKELUAR_X "
                SQL &= " + MUTASIMASUK_X "
                SQL &= " + PENJUALAN_X "
                SQL &= " + OPNAME_X "
                SQL &= " + ADJUSMENT_X "
                SQL &= " ) + "
                SQL &= " ( "
                SQL &= " BELI "
                SQL &= " + MUTASIKELUAR "
                SQL &= " + MUTASIMASUK "
                SQL &= " + PENJUALAN "
                SQL &= " + OPNAME "
                SQL &= " + ADJUSMENT "
                SQL &= " ) "
                SQL &= " - MONITORING "
            End If
            SQL &= ",SAWAL = "
            SQL &= " BELI_X "
            SQL &= " + MUTASIKELUAR_X "
            SQL &= " + MUTASIMASUK_X "
            SQL &= " + PENJUALAN_X "
            SQL &= " + OPNAME_X "
            SQL &= " + ADJUSMENT_X "
            SQL &= ",MASUK = BELI + MUTASIMASUK + ADJUSMENT + OPNAME_MASUK "
            SQL &= ",KELUAR = - (MUTASIKELUAR + PENJUALAN + OPNAME_KELUAR) "
            SQL &= ",TOTAL = "
            SQL &= " (BELI_X "
            SQL &= " + MUTASIKELUAR_X "
            SQL &= " + MUTASIMASUK_X "
            SQL &= " + PENJUALAN_X "
            SQL &= " + OPNAME_X "
            SQL &= " + ADJUSMENT_X) "
            SQL &= " + (BELI + MUTASIMASUK + ADJUSMENT + OPNAME_MASUK) "
            SQL &= " + (MUTASIKELUAR + PENJUALAN + OPNAME_KELUAR) "
            SQL &= ",HARGA_SAWAL = "
            SQL &= " (BELI_X "
            SQL &= " + MUTASIKELUAR_X "
            SQL &= " + MUTASIMASUK_X "
            SQL &= " + PENJUALAN_X "
            SQL &= " + OPNAME_X "
            SQL &= " + ADJUSMENT_X) * ((ZZ.PRICEPURCHASESTANDARD * 0.1) + ZZ.PRICEPURCHASESTANDARD) "
            SQL &= ",HARGA_MASUK = (BELI + MUTASIMASUK + ADJUSMENT + OPNAME_MASUK) * ((ZZ.PRICEPURCHASESTANDARD * 0.1) + ZZ.PRICEPURCHASESTANDARD) "
            SQL &= ",HARGA_KELUAR = (- (MUTASIKELUAR + PENJUALAN + OPNAME_KELUAR)) * ((ZZ.PRICEPURCHASESTANDARD * 0.1) + ZZ.PRICEPURCHASESTANDARD) "
            SQL &= ",HARGA_TOTAL = "
            SQL &= " ((BELI_X "
            SQL &= " + MUTASIKELUAR_X "
            SQL &= " + MUTASIMASUK_X "
            SQL &= " + PENJUALAN_X "
            SQL &= " + OPNAME_X "
            SQL &= " + ADJUSMENT_X) "
            SQL &= " + (BELI + MUTASIMASUK + ADJUSMENT + OPNAME_MASUK) "
            SQL &= " + (MUTASIKELUAR + PENJUALAN + OPNAME_KELUAR)) * ((ZZ.PRICEPURCHASESTANDARD * 0.1) + ZZ.PRICEPURCHASESTANDARD) "
            SQL &= "FROM "
            SQL &= "M_ITEM AS Z "
            SQL &= "LEFT JOIN "
            SQL &= "( "
            SQL &= "SELECT "
            SQL &= "DISTINCT A.KDITEM "
            SQL &= ",BELI_X = ISNULL(BB.TOTAL, 0) "
            SQL &= ",RETURBELI_X = ISNULL(CC.TOTAL, 0) "
            SQL &= ",MUTASIKELUAR_X = ISNULL(DD.TOTAL, 0) "
            SQL &= ",MUTASIMASUK_X = ISNULL(EE.TOTAL, 0) "
            SQL &= ",PENJUALAN_X = ISNULL(FF.TOTAL, 0) "
            SQL &= ",OPNAME_X = ISNULL(GG.TOTAL, 0) "
            SQL &= ",ADJUSMENT_X = ISNULL(HH.TOTAL, 0) "
            SQL &= ",BELI = ISNULL(B.TOTAL, 0) "
            SQL &= ",RETURBELI = ISNULL(C.TOTAL, 0) "
            SQL &= ",MUTASIKELUAR = ISNULL(D.TOTAL, 0) "
            SQL &= ",MUTASIMASUK = ISNULL(E.TOTAL, 0) "
            SQL &= ",PENJUALAN = ISNULL(F.TOTAL, 0) "
            SQL &= ",OPNAME = ISNULL(G.TOTAL, 0) "
            SQL &= ",ADJUSMENT = ISNULL(H.TOTAL, 0) "
            SQL &= ",MONITORING = ISNULL(I.TOTAL, 0) "
            SQL &= ",OPNAME_MASUK = ISNULL(J.TOTAL, 0) "
            SQL &= ",OPNAME_KELUAR = ISNULL(K.TOTAL, 0) "
            SQL &= "FROM "
            SQL &= "(SELECT KDITEM FROM M_ITEM) AS A "
            'AWAL
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = SUM(A.QTY * C.RATE) "
            SQL &= "FROM P_PI_D AS A INNER JOIN P_PI_H AS B ON A.KDPI = B.KDPI INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) < '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND B.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "GROUP BY A.KDITEM) AS BB	 "
            SQL &= "ON A.KDITEM = BB.KDITEM "
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = - SUM(A.QTY * C.RATE) "
            SQL &= "FROM P_PR_D AS A INNER JOIN P_PR_H AS B ON A.KDPR = B.KDPR INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) < '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND B.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "GROUP BY A.KDITEM) AS CC	 "
            SQL &= "ON A.KDITEM = CC.KDITEM "
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = - SUM(A.QTY * C.RATE) "
            SQL &= "FROM I_MUTATION_D AS A INNER JOIN I_MUTATION_H AS B ON A.KDMUTATION = B.KDMUTATION INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) < '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND B.KDWAREHOUSEFROM = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "GROUP BY A.KDITEM) AS DD	 "
            SQL &= "ON A.KDITEM = DD.KDITEM "
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = SUM(A.QTY * C.RATE) "
            SQL &= "FROM I_TERIMA_D AS A INNER JOIN I_TERIMA_H AS B ON A.KDTERIMA = B.KDTERIMA INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) < '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND B.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "GROUP BY A.KDITEM) AS EE	 "
            SQL &= "ON A.KDITEM = EE.KDITEM "
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = - SUM(A.QTY * C.RATE) "
            SQL &= "FROM S_SO_TRANSAKSI_D AS A INNER JOIN S_SO_TRANSAKSI_H AS B ON A.KDSOTRANSAKSI = B.KDSOTRANSAKSI INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) < '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND B.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "GROUP BY A.KDITEM) AS FF	 "
            SQL &= "ON A.KDITEM = FF.KDITEM "
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = SUM(A.QTY * C.RATE) "
            SQL &= "FROM I_OPNAME_D AS A INNER JOIN I_OPNAME_H AS B ON A.KDOPNAME = B.KDOPNAME INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) < '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND B.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "GROUP BY A.KDITEM) AS GG	 "
            SQL &= "ON A.KDITEM = GG.KDITEM "
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = SUM(A.QTY * C.RATE) "
            SQL &= "FROM I_ADJUSTMENT_D AS A INNER JOIN I_ADJUSTMENT_H AS B ON A.KDADJUSTMENT = B.KDADJUSTMENT INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) < '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND B.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "GROUP BY A.KDITEM) AS HH	 "
            SQL &= "ON A.KDITEM = HH.KDITEM "
            'TRANSAKSI
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = SUM(A.QTY * C.RATE) "
            SQL &= "FROM P_PI_D AS A INNER JOIN P_PI_H AS B ON A.KDPI = B.KDPI INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), B.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND B.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "GROUP BY A.KDITEM) AS B	 "
            SQL &= "ON A.KDITEM = B.KDITEM "
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = - SUM(A.QTY * C.RATE) "
            SQL &= "FROM P_PR_D AS A INNER JOIN P_PR_H AS B ON A.KDPR = B.KDPR INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), B.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND B.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "GROUP BY A.KDITEM) AS C	 "
            SQL &= "ON A.KDITEM = C.KDITEM "
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = - SUM(A.QTY * C.RATE) "
            SQL &= "FROM I_MUTATION_D AS A INNER JOIN I_MUTATION_H AS B ON A.KDMUTATION = B.KDMUTATION INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), B.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND B.KDWAREHOUSEFROM = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "GROUP BY A.KDITEM) AS D	 "
            SQL &= "ON A.KDITEM = D.KDITEM "
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = SUM(A.QTY * C.RATE) "
            SQL &= "FROM I_TERIMA_D AS A INNER JOIN I_TERIMA_H AS B ON A.KDTERIMA = B.KDTERIMA INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), B.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND B.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "GROUP BY A.KDITEM) AS E	 "
            SQL &= "ON A.KDITEM = E.KDITEM "
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = - SUM(A.QTY * C.RATE) "
            SQL &= "FROM S_SO_TRANSAKSI_D AS A INNER JOIN S_SO_TRANSAKSI_H AS B ON A.KDSOTRANSAKSI = B.KDSOTRANSAKSI INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), B.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND B.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "GROUP BY A.KDITEM) AS F	 "
            SQL &= "ON A.KDITEM = F.KDITEM "
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = SUM(A.QTY * C.RATE) "
            SQL &= "FROM I_OPNAME_D AS A INNER JOIN I_OPNAME_H AS B ON A.KDOPNAME = B.KDOPNAME INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), B.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND B.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "GROUP BY A.KDITEM) AS G	 "
            SQL &= "ON A.KDITEM = G.KDITEM "
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = SUM(A.QTY * C.RATE) "
            SQL &= "FROM I_ADJUSTMENT_D AS A INNER JOIN I_ADJUSTMENT_H AS B ON A.KDADJUSTMENT = B.KDADJUSTMENT INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), B.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND B.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "GROUP BY A.KDITEM) AS H	 "
            SQL &= "ON A.KDITEM = H.KDITEM "
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = SUM(A.AMOUNT * C.RATE) "
            SQL &= "FROM M_ITEM_WAREHOUSE AS A INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE A.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "GROUP BY A.KDITEM) AS I	 "
            SQL &= "ON A.KDITEM = I.KDITEM "
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = SUM(A.QTY * C.RATE) "
            SQL &= "FROM I_OPNAME_D AS A INNER JOIN I_OPNAME_H AS B ON A.KDOPNAME = B.KDOPNAME INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), B.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND B.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "AND A.QTY > 0 "
            SQL &= "GROUP BY A.KDITEM) AS J	 "
            SQL &= "ON A.KDITEM = J.KDITEM "
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = SUM(A.QTY * C.RATE) "
            SQL &= "FROM I_OPNAME_D AS A INNER JOIN I_OPNAME_H AS B ON A.KDOPNAME = B.KDOPNAME INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), B.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND B.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "AND A.QTY < 0 "
            SQL &= "GROUP BY A.KDITEM) AS K	 "
            SQL &= "ON A.KDITEM = K.KDITEM "
            SQL &= ") AS A "
            SQL &= "ON Z.KDITEM = A.KDITEM "
            SQL &= "INNER JOIN M_ITEM_UOM ZZ "
            SQL &= "ON Z.KDITEM = ZZ.KDITEM "
            SQL &= "INNER JOIN M_UOM ZZZ "
            SQL &= "ON ZZ.KDUOM = ZZZ.KDUOM "
            SQL &= "INNER JOIN M_ITEM_L3 ZZZZ "
            SQL &= "ON Z.KDITEM_L3 = ZZZZ.KDITEM_L3 "
            SQL &= "WHERE ZZ.RATE = 1 "
            SQL &= "AND Z.ISSTOK = 1 "
            SQL &= "ORDER BY Z.NMITEM2 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

            grd.MainView = grv
            grd.DataSource = ds.Tables("ALL")
            grd.ForceInitialize()

            fn_LoadFormatMutasiGudangAdjustment()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatMutasiGudangAdjustment()
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
    Public Sub fn_LoadLanguageMutasiGudangAdjustment()
        Try
            'grv.Columns("NMITEM1").Caption = Item.NMITEM1_2
            grv.Columns("NMITEM2").Caption = "NAMA OBAT"
            MasterColumnChooserToolStripMenuItem.Text = Caption.ColumnChooserMaster
            DetailColumnChooserToolStripMenuItem.Text = Caption.ColumnChooserDetail
        Catch oErr As Exception

        End Try
    End Sub
    Private Sub fn_MutasiGudangNonAdjustment()
        Try
            If grdKDWAREHOUSE.Text = String.Empty Then Exit Sub

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

            SQL = "	SELECT "
            SQL &= "JENIS = ZZZZ.MEMO "
            SQL &= ",Z.NMITEM2 "
            SQL &= ",SATUAN = ZZZ.MEMO "
            SQL &= ",HARGA = ZZ.PRICEPURCHASESTANDARD "
            SQL &= ",HARGA_PPN = (ZZ.PRICEPURCHASESTANDARD * 0.1) + ZZ.PRICEPURCHASESTANDARD "
            If chkALL.Checked = True Then
                SQL &= ",BELI_X "
                SQL &= ",MUTASIKELUAR_X "
                SQL &= ",MUTASIMASUK_X "
                SQL &= ",PENJUALAN_X "
                SQL &= ",OPNAME_X "
                SQL &= ",STOKAWAL = "
                SQL &= " BELI_X "
                SQL &= " + MUTASIKELUAR_X "
                SQL &= " + MUTASIMASUK_X "
                SQL &= " + PENJUALAN_X "
                SQL &= " + OPNAME_X "
                SQL &= ",BELI "
                SQL &= ",MUTASIKELUAR "
                SQL &= ",MUTASIMASUK "
                SQL &= ",PENJUALAN "
                SQL &= ",OPNAME "
                SQL &= ",STOKAKHIR = "
                SQL &= " ( "
                SQL &= " BELI_X "
                SQL &= " + MUTASIKELUAR_X "
                SQL &= " + MUTASIMASUK_X "
                SQL &= " + PENJUALAN_X "
                SQL &= " + OPNAME_X "
                SQL &= " ) + "
                SQL &= " ( "
                SQL &= " BELI "
                SQL &= " + MUTASIKELUAR "
                SQL &= " + MUTASIMASUK "
                SQL &= " + PENJUALAN "
                SQL &= " + OPNAME "
                SQL &= " ) "
                SQL &= ",MONITORING "
                SQL &= ",SELISIH = "
                SQL &= " ( "
                SQL &= " BELI_X "
                SQL &= " + MUTASIKELUAR_X "
                SQL &= " + MUTASIMASUK_X "
                SQL &= " + PENJUALAN_X "
                SQL &= " + OPNAME_X "
                SQL &= " ) + "
                SQL &= " ( "
                SQL &= " BELI "
                SQL &= " + MUTASIKELUAR "
                SQL &= " + MUTASIMASUK "
                SQL &= " + PENJUALAN "
                SQL &= " + OPNAME "
                SQL &= " ) "
                SQL &= " - MONITORING "
            End If
            SQL &= ",SAWAL = "
            SQL &= " BELI_X "
            SQL &= " + MUTASIKELUAR_X "
            SQL &= " + MUTASIMASUK_X "
            SQL &= " + PENJUALAN_X "
            SQL &= " + OPNAME_X "
            SQL &= ",MASUK = BELI + MUTASIMASUK + OPNAME_MASUK "
            SQL &= ",KELUAR = - (MUTASIKELUAR + PENJUALAN + OPNAME_KELUAR) "
            SQL &= ",TOTAL = "
            SQL &= " (BELI_X "
            SQL &= " + MUTASIKELUAR_X "
            SQL &= " + MUTASIMASUK_X "
            SQL &= " + PENJUALAN_X "
            SQL &= " + OPNAME_X "
            SQL &= " ) "
            SQL &= " + (BELI + MUTASIMASUK + OPNAME_MASUK) "
            SQL &= " + (MUTASIKELUAR + PENJUALAN + OPNAME_KELUAR) "
            SQL &= ",HARGA_SAWAL = "
            SQL &= " (BELI_X "
            SQL &= " + MUTASIKELUAR_X "
            SQL &= " + MUTASIMASUK_X "
            SQL &= " + PENJUALAN_X "
            SQL &= " + OPNAME_X "
            SQL &= " ) * ((ZZ.PRICEPURCHASESTANDARD * 0.1) + ZZ.PRICEPURCHASESTANDARD) "
            SQL &= ",HARGA_MASUK = (BELI + MUTASIMASUK + OPNAME_MASUK) * ((ZZ.PRICEPURCHASESTANDARD * 0.1) + ZZ.PRICEPURCHASESTANDARD) "
            SQL &= ",HARGA_KELUAR = (- (MUTASIKELUAR + PENJUALAN + OPNAME_KELUAR)) * ((ZZ.PRICEPURCHASESTANDARD * 0.1) + ZZ.PRICEPURCHASESTANDARD) "
            SQL &= ",HARGA_TOTAL = "
            SQL &= " ((BELI_X "
            SQL &= " + MUTASIKELUAR_X "
            SQL &= " + MUTASIMASUK_X "
            SQL &= " + PENJUALAN_X "
            SQL &= " + OPNAME_X "
            SQL &= " ) "
            SQL &= " + (BELI + MUTASIMASUK + OPNAME_MASUK) "
            SQL &= " + (MUTASIKELUAR + PENJUALAN + OPNAME_KELUAR)) * ((ZZ.PRICEPURCHASESTANDARD * 0.1) + ZZ.PRICEPURCHASESTANDARD) "
            SQL &= "FROM "
            SQL &= "M_ITEM AS Z "
            SQL &= "LEFT JOIN "
            SQL &= "( "
            SQL &= "SELECT "
            SQL &= "DISTINCT A.KDITEM "
            SQL &= ",BELI_X = ISNULL(BB.TOTAL, 0) "
            SQL &= ",RETURBELI_X = ISNULL(CC.TOTAL, 0) "
            SQL &= ",MUTASIKELUAR_X = ISNULL(DD.TOTAL, 0) "
            SQL &= ",MUTASIMASUK_X = ISNULL(EE.TOTAL, 0) "
            SQL &= ",PENJUALAN_X = ISNULL(FF.TOTAL, 0) "
            SQL &= ",OPNAME_X = ISNULL(GG.TOTAL, 0) "
            SQL &= ",ADJUSMENT_X = ISNULL(HH.TOTAL, 0) "
            SQL &= ",BELI = ISNULL(B.TOTAL, 0) "
            SQL &= ",RETURBELI = ISNULL(C.TOTAL, 0) "
            SQL &= ",MUTASIKELUAR = ISNULL(D.TOTAL, 0) "
            SQL &= ",MUTASIMASUK = ISNULL(E.TOTAL, 0) "
            SQL &= ",PENJUALAN = ISNULL(F.TOTAL, 0) "
            SQL &= ",OPNAME = ISNULL(G.TOTAL, 0) "
            SQL &= ",ADJUSMENT = ISNULL(H.TOTAL, 0) "
            SQL &= ",MONITORING = ISNULL(I.TOTAL, 0) "
            SQL &= ",OPNAME_MASUK = ISNULL(J.TOTAL, 0) "
            SQL &= ",OPNAME_KELUAR = ISNULL(K.TOTAL, 0) "
            SQL &= "FROM "
            SQL &= "(SELECT KDITEM FROM M_ITEM) AS A "
            'AWAL
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = SUM(A.QTY * C.RATE) "
            SQL &= "FROM P_PI_D AS A INNER JOIN P_PI_H AS B ON A.KDPI = B.KDPI INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) < '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND B.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "GROUP BY A.KDITEM) AS BB	 "
            SQL &= "ON A.KDITEM = BB.KDITEM "
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = - SUM(A.QTY * C.RATE) "
            SQL &= "FROM P_PR_D AS A INNER JOIN P_PR_H AS B ON A.KDPR = B.KDPR INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) < '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND B.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "GROUP BY A.KDITEM) AS CC	 "
            SQL &= "ON A.KDITEM = CC.KDITEM "
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = - SUM(A.QTY * C.RATE) "
            SQL &= "FROM I_MUTATION_D AS A INNER JOIN I_MUTATION_H AS B ON A.KDMUTATION = B.KDMUTATION INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) < '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND B.KDWAREHOUSEFROM = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "GROUP BY A.KDITEM) AS DD	 "
            SQL &= "ON A.KDITEM = DD.KDITEM "
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = SUM(A.QTY * C.RATE) "
            SQL &= "FROM I_TERIMA_D AS A INNER JOIN I_TERIMA_H AS B ON A.KDTERIMA = B.KDTERIMA INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) < '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND B.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "GROUP BY A.KDITEM) AS EE	 "
            SQL &= "ON A.KDITEM = EE.KDITEM "
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = - SUM(A.QTY * C.RATE) "
            SQL &= "FROM S_SO_TRANSAKSI_D AS A INNER JOIN S_SO_TRANSAKSI_H AS B ON A.KDSOTRANSAKSI = B.KDSOTRANSAKSI INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) < '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND B.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "GROUP BY A.KDITEM) AS FF	 "
            SQL &= "ON A.KDITEM = FF.KDITEM "
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = SUM(A.QTY * C.RATE) "
            SQL &= "FROM I_OPNAME_D AS A INNER JOIN I_OPNAME_H AS B ON A.KDOPNAME = B.KDOPNAME INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) < '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND B.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "GROUP BY A.KDITEM) AS GG	 "
            SQL &= "ON A.KDITEM = GG.KDITEM "
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = SUM(A.QTY * C.RATE) "
            SQL &= "FROM I_ADJUSTMENT_D AS A INNER JOIN I_ADJUSTMENT_H AS B ON A.KDADJUSTMENT = B.KDADJUSTMENT INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) < '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND B.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "GROUP BY A.KDITEM) AS HH	 "
            SQL &= "ON A.KDITEM = HH.KDITEM "
            'TRANSAKSI
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = SUM(A.QTY * C.RATE) "
            SQL &= "FROM P_PI_D AS A INNER JOIN P_PI_H AS B ON A.KDPI = B.KDPI INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), B.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND B.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "GROUP BY A.KDITEM) AS B	 "
            SQL &= "ON A.KDITEM = B.KDITEM "
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = - SUM(A.QTY * C.RATE) "
            SQL &= "FROM P_PR_D AS A INNER JOIN P_PR_H AS B ON A.KDPR = B.KDPR INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), B.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND B.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "GROUP BY A.KDITEM) AS C	 "
            SQL &= "ON A.KDITEM = C.KDITEM "
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = - SUM(A.QTY * C.RATE) "
            SQL &= "FROM I_MUTATION_D AS A INNER JOIN I_MUTATION_H AS B ON A.KDMUTATION = B.KDMUTATION INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), B.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND B.KDWAREHOUSEFROM = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "GROUP BY A.KDITEM) AS D	 "
            SQL &= "ON A.KDITEM = D.KDITEM "
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = SUM(A.QTY * C.RATE) "
            SQL &= "FROM I_TERIMA_D AS A INNER JOIN I_TERIMA_H AS B ON A.KDTERIMA = B.KDTERIMA INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), B.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND B.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "GROUP BY A.KDITEM) AS E	 "
            SQL &= "ON A.KDITEM = E.KDITEM "
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = - SUM(A.QTY * C.RATE) "
            SQL &= "FROM S_SO_TRANSAKSI_D AS A INNER JOIN S_SO_TRANSAKSI_H AS B ON A.KDSOTRANSAKSI = B.KDSOTRANSAKSI INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), B.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND B.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "GROUP BY A.KDITEM) AS F	 "
            SQL &= "ON A.KDITEM = F.KDITEM "
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = SUM(A.QTY * C.RATE) "
            SQL &= "FROM I_OPNAME_D AS A INNER JOIN I_OPNAME_H AS B ON A.KDOPNAME = B.KDOPNAME INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), B.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND B.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "GROUP BY A.KDITEM) AS G	 "
            SQL &= "ON A.KDITEM = G.KDITEM "
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = SUM(A.QTY * C.RATE) "
            SQL &= "FROM I_ADJUSTMENT_D AS A INNER JOIN I_ADJUSTMENT_H AS B ON A.KDADJUSTMENT = B.KDADJUSTMENT INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), B.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND B.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "GROUP BY A.KDITEM) AS H	 "
            SQL &= "ON A.KDITEM = H.KDITEM "
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = SUM(A.AMOUNT * C.RATE) "
            SQL &= "FROM M_ITEM_WAREHOUSE AS A INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE A.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "GROUP BY A.KDITEM) AS I	 "
            SQL &= "ON A.KDITEM = I.KDITEM "
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = SUM(A.QTY * C.RATE) "
            SQL &= "FROM I_OPNAME_D AS A INNER JOIN I_OPNAME_H AS B ON A.KDOPNAME = B.KDOPNAME INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), B.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND B.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "AND A.QTY > 0 "
            SQL &= "GROUP BY A.KDITEM) AS J	 "
            SQL &= "ON A.KDITEM = J.KDITEM "
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = SUM(A.QTY * C.RATE) "
            SQL &= "FROM I_OPNAME_D AS A INNER JOIN I_OPNAME_H AS B ON A.KDOPNAME = B.KDOPNAME INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), B.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND B.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "AND A.QTY < 0 "
            SQL &= "GROUP BY A.KDITEM) AS K	 "
            SQL &= "ON A.KDITEM = K.KDITEM "
            SQL &= ") AS A "
            SQL &= "ON Z.KDITEM = A.KDITEM "
            SQL &= "INNER JOIN M_ITEM_UOM ZZ "
            SQL &= "ON Z.KDITEM = ZZ.KDITEM "
            SQL &= "INNER JOIN M_UOM ZZZ "
            SQL &= "ON ZZ.KDUOM = ZZZ.KDUOM "
            SQL &= "INNER JOIN M_ITEM_L3 ZZZZ "
            SQL &= "ON Z.KDITEM_L3 = ZZZZ.KDITEM_L3 "
            SQL &= "WHERE ZZ.RATE = 1 "
            SQL &= "AND Z.ISSTOK = 1 "
            SQL &= "ORDER BY Z.NMITEM2 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

            grd.MainView = grv
            grd.DataSource = ds.Tables("ALL")
            grd.ForceInitialize()

            fn_LoadFormatMutasiGudangNonAdjustment()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatMutasiGudangNonAdjustment()
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
    Public Sub fn_LoadLanguageMutasiGudangNonAdjustment()
        Try
            'grv.Columns("NMITEM1").Caption = Item.NMITEM1_2
            grv.Columns("NMITEM2").Caption = "NAMA OBAT"
            MasterColumnChooserToolStripMenuItem.Text = Caption.ColumnChooserMaster
            DetailColumnChooserToolStripMenuItem.Text = Caption.ColumnChooserDetail
        Catch oErr As Exception

        End Try
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
                'Case Keys.C
                '    If e.Alt = True And picConfirm.Enabled = True Then
                '        picRefresh_Click()
                '    End If
        End Select
    End Sub
    'Private Sub picConfirm_Click() Handles picConfirm.Click
    '    Try
    '        If cboTYPE.SelectedIndex = 4 Then
    '            If chkALL.Checked = True Then
    '                If grdKDWAREHOUSE.Text <> "" Then
    '                    Dim oMutation As New Inventory.clsMutation

    '                    Dim sResult = MsgBox("Apakah Yakin akan di Confirm?", MsgBoxStyle.YesNo, Me.Text)

    '                    If sResult = Windows.Forms.DialogResult.Yes Then
    '                        For iLoop As Integer = 0 To grv.RowCount - 1
    '                            If grv.IsRowSelected(iLoop) = True Then
    '                                If grv.GetRowCellValue(iLoop, "SELISIH") <> 0 Then
    '                                    oMutation.UpdateAmount(grv.GetRowCellValue(iLoop, "KDITEM"), grv.GetRowCellValue(iLoop, "KDUOM"), grdKDWAREHOUSE.EditValue, grv.GetRowCellValue(iLoop, "STOKAKHIR"))
    '                                End If
    '                                'grv.SetFocusedRowCellValue("Approval", oTransaksi.GetData(grv.GetRowCellValue(iLoop, "NoLab")).ISACCDOKTER)
    '                                'grv.UpdateCurrentRow()
    '                                'grv.RefreshRow(grv.GetFocusedDataSourceRowIndex())
    '                            End If
    '                        Next

    '                    End If
    '                End If
    '            End If
    '        End If

    '        fn_LoadSecurity()
    '    Catch oErr As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
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

    Private Sub picPrint_Click(sender As Object, e As EventArgs) Handles picPrint.Click

    End Sub
#End Region
End Class