Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Imports DevExpress.XtraPrinting

Imports System.Data
Imports System.Data.SqlClient


Public Class frmReportMonitoringItem
    Implements ILanguage

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = Item.TITLE_REPORT
        deDATEFrom.DateTime = Now.AddDays((-Now.Day) + 1)
        deDATETo.DateTime = Now
        fn_LoadWarehouse()
        fn_LoadItem()
        cboTYPE.Properties.Items.Clear()
        cboTYPE.Properties.Items.Add(Report.FILTER_ITEM_TYPE_01)
        cboTYPE.Properties.Items.Add("Kartu Persediaan Barang")
        cboTYPE.SelectedIndex = 0
    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            grv.SaveLayoutToRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text & "\" & cboTYPE.Text)
        Catch oErr As Exception

        End Try
    End Sub
    Public Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            Me.Text = Item.TITLE_REPORT

            cboTYPE.Properties.Items.Clear()
            cboTYPE.Properties.Items.Add(Report.FILTER_ITEM_TYPE_01)
            cboTYPE.Properties.Items.Add("Kartu Persediaan Barang")

            If cboTYPE.SelectedIndex = 0 Then
                fn_LoadLanguageCurrent()
            Else
                fn_LoadLanguageCard()
            End If
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
                      Where x.MODUL = "ITEM_R" _
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
        TryCast(printableComponentLink.PageHeaderFooter, PageHeaderFooter)
            phf.Header.Content.Clear()
            phf.Header.Font = New Font("Times New Roman", 14, FontStyle.Bold)
            phf.Header.LineAlignment = BrickAlignment.Center
            phf.Footer.Font = New Font("Times New Roman", 9.75)
            phf.Footer.LineAlignment = BrickAlignment.Far
            phf.Footer.Content.AddRange(New String() _
        {sWATERMARK, "", Report.REPORT_PAGE & " : [Page # of Pages #]"})

            phf.Header.Content.AddRange(New String() _
{"", Item.TITLE_REPORT & vbCrLf & " " & cboTYPE.Text.ToString.Trim.ToUpper & IIf(cboTYPE.SelectedIndex = 0, "", vbCrLf & grdKDWAREHOUSE.Text & vbCrLf & grdKDITEM.Text), ""})

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
            grv.GroupSummary.Clear()
            grv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            grv1.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            If cboTYPE.SelectedIndex = 0 Then
                fn_LoadDataCurrent()
            Else
                fn_LoadDataCard()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

#Region "Current"
    Private Sub fn_LoadDataCurrent()
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

            SQL = "SELECT GUDANG, NMITEM1, NMITEM2, SATUAN, AMOUNT, HARGA_BELI_PPN,TOTAL_PPN, EXPIRE "
            SQL &= "FROM ( "
            SQL &= "SELECT "
            SQL &= "GUDANG = D.NAME_DISPLAY "
            SQL &= ",B.NMITEM1 "
            SQL &= ",B.NMITEM2 "
            SQL &= ",SATUAN = C.MEMO "
            SQL &= ",AMOUNT = A.AMOUNT * K.RATE "
            SQL &= ",HARGA_BELI_PPN = K.PRICEPURCHASESTANDARD "
            SQL &= ",TOTAL_PPN = (A.AMOUNT * K.RATE) * K.PRICEPURCHASESTANDARD "
            SQL &= ",EXPIRE = ISNULL((SELECT TOP 1 FORMAT(AA.TANGGALEXPIRE, 'dd-MM-yyyy') FROM P_PI_D AA WHERE AA.KDITEM = A.KDITEM AND A.KDUOM = AA.KDUOM ORDER BY AA.TANGGALEXPIRE DESC), '-') "
            SQL &= "FROM M_ITEM_WAREHOUSE A "
            SQL &= "INNER JOIN M_ITEM B "
            SQL &= "ON A.KDITEM = B.KDITEM "
            SQL &= "INNER JOIN M_UOM C "
            SQL &= "ON A.KDUOM = C.KDUOM "
            SQL &= "INNER JOIN M_WAREHOUSE D "
            SQL &= "ON A.KDWAREHOUSE = D.KDWAREHOUSE "
            SQL &= "INNER JOIN M_ITEM_UOM K "
            SQL &= "ON A.KDITEM = K.KDITEM AND A.KDUOM = K.KDUOM ) AS A "
            SQL &= "GROUP BY NMITEM1, NMITEM2, AMOUNT, GUDANG,HARGA_BELI_PPN, TOTAL_PPN, SATUAN,EXPIRE  "
            SQL &= "ORDER BY NMITEM2 DESC"

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ITEM")


            grd.MainView = grv
            grd.DataSource = ds.Tables("ITEM")
            grd.ForceInitialize()

            fn_LoadFormatDataCurrent()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatDataCurrent()
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

        'grv.Columns("TOTAL").SummaryItem.FieldName = "TOTAL"
        'grv.Columns("TOTAL").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        'grv.Columns("TOTAL").SummaryItem.DisplayFormat = "{0:n2}"

        grv.Columns("TOTAL_PPN").SummaryItem.FieldName = "TOTAL_PPN"
        grv.Columns("TOTAL_PPN").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        grv.Columns("TOTAL_PPN").SummaryItem.DisplayFormat = "{0:n2}"

    End Sub
    Public Sub fn_LoadLanguageCurrent()
        Try
            grv.Columns("GUDANG").Group()
            grv.ExpandAllGroups()

            grv.Columns("NMITEM1").Caption = Item.NMITEM1_2
            grv.Columns("EXPIRE").Caption = "Expire"
            grv.Columns("NMITEM2").Caption = Item.NMITEM2_2
            'grv.Columns("NMITEM3").Caption = Item.NMITEM3_2
            'grv.Columns("HARGA_BELI").Caption = "Harga Beli"
            'grv.Columns("PPN").Caption = "PPn(11%)"
            grv.Columns("HARGA_BELI_PPN").Caption = "Harga Beli PPn"
            grv.Columns("GUDANG").Caption = "Gudang"
            grv.Columns("AMOUNT").Caption = Item.AMOUNT
            'grv.Columns("AVERAGE").Caption = "Average"
            ' grv.Columns("TOTAL").Caption = "Total Sebelum PPn"
            grv.Columns("TOTAL_PPN").Caption = "Total Sesudah PPn"

            MasterColumnChooserToolStripMenuItem.Text = Caption.ColumnChooserMaster
        Catch oErr As Exception

        End Try
    End Sub
#End Region
#Region "Kartu Gantung"
    Private Sub fn_LoadDataCard()
        Try
            If grdKDWAREHOUSE.Text = String.Empty And grdKDITEM.Text = String.Empty Then
                MsgBox("Silahkan Pilih Gudang dan Item", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim oStokCard As New Accounting.clsStockCard
            Dim oItem As New Reference.clsItem
            Dim dsItem = oItem.GetDataDetail_UOM(grdKDITEM.EditValue)

            If oStokCard.PostingAverageItemNew(grdKDITEM.EditValue, dsItem.FirstOrDefault(Function(x) x.RATE = 1).KDUOM, grdKDWAREHOUSE.EditValue) = False Then
                MsgBox("Tidak Ada Transkasi", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
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

            'SQL = "SELECT * FROM ("
            'SQL &= "(SELECT "
            'SQL &= "A.DATE "
            'SQL &= ",A.NOREFERENCE "
            'SQL &= ",Uraian = A.DESCRIPTION "
            'SQL &= ",B.KDITEM "
            'SQL &= ",B.NMITEM2 "
            'SQL &= ",A.QTYIN "
            'SQL &= ",A.QTYOUT "
            'SQL &= ",A.QTYAVERAGE "
            'SQL &= ",A.PRICE "
            'SQL &= ",A.SEQ "
            'SQL &= "FROM M_ITEM B "
            'SQL &= "INNER JOIN A_STOCK_CARD A "
            'SQL &= "ON B.KDITEM = A.KDITEM "
            'SQL &= "INNER JOIN M_UOM C "
            'SQL &= "ON A.KDUOM = C.KDUOM "
            'SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            'SQL &= ") "
            'SQL &= "UNION "
            'SQL &= "(SELECT * "
            'SQL &= "FROM "
            'SQL &= "(SELECT "
            'SQL &= "DATE = '" & deDATEFrom.DateTime.ToString("yyyy-MM-dd") & "' "
            'SQL &= ",NOREFERENCE = 'SALDO AWAL' "
            'SQL &= ",Uraian = '-' "
            'SQL &= ",B.KDITEM "
            'SQL &= ",B.NMITEM2 "
            'SQL &= ",QTYIN = ISNULL((SELECT TOP 1 AA.QTYAVERAGE FROM A_STOCK_CARD AS AA WHERE CONVERT(VARCHAR(8), AA.DATE, 112) < '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND AA.KDITEM = B.KDITEM ORDER BY SEQ DESC), 0) "
            'SQL &= ",QTYOUT = 0 "
            'SQL &= ",QTYAVERAGE = ISNULL((SELECT TOP 1 AA.QTYAVERAGE FROM A_STOCK_CARD AS AA WHERE CONVERT(VARCHAR(8), AA.DATE, 112) < '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND AA.KDITEM = B.KDITEM ORDER BY SEQ DESC), 0) "
            'SQL &= ",PRICE = 0 "
            'SQL &= ",SEQ =ISNULL((SELECT TOP 1 AA.SEQ FROM A_STOCK_CARD AS AA WHERE CONVERT(VARCHAR(8), AA.DATE, 112) < '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND AA.KDITEM = B.KDITEM ORDER BY SEQ DESC), 0) "
            'SQL &= "FROM M_ITEM B "
            'SQL &= ") AS A "
            'SQL &= ") "
            'SQL &= ") AS A "
            'SQL &= "WHERE A.KDITEM = '" & grdKDITEM.EditValue & "' "
            'SQL &= "ORDER BY NMITEM2, SEQ ASC "


            SQL = "SELECT * FROM ("
            SQL &= "(SELECT "
            SQL &= "A.DATE "
            SQL &= ",A.NOREFERENCE "
            SQL &= ",A.BATCH "
            SQL &= ",A.URAIAN "
            SQL &= ",B.KDITEM "
            SQL &= ",B.NMITEM2 "
            SQL &= ",A.MASUK "
            SQL &= ",A.KELUAR "
            SQL &= ",A.SISA "
            SQL &= ",HARGA = A.PRICE "
            SQL &= ",A.EXPIRE "
            SQL &= ",A.KETERANGAN "
            SQL &= ",A.SEQ "
            SQL &= "FROM M_ITEM B "
            SQL &= "INNER JOIN A_STOCK_CARD_NEW A "
            SQL &= "ON B.KDITEM = A.KDITEM "
            SQL &= "INNER JOIN M_UOM C "
            SQL &= "ON A.KDUOM = C.KDUOM "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= ") "
            SQL &= "UNION "
            SQL &= "(SELECT * "
            SQL &= "FROM "
            SQL &= "(SELECT "
            SQL &= "DATE = '" & deDATEFrom.DateTime.ToString("yyyy-MM-dd") & "' "
            SQL &= ",NOREFERENCE = 'SALDO AWAL' "
            SQL &= ",BATCH = '' "
            SQL &= ",URAIAN = '-' "
            SQL &= ",B.KDITEM "
            SQL &= ",B.NMITEM2 "
            SQL &= ",MASUK = ISNULL((SELECT TOP 1 AA.SISA FROM A_STOCK_CARD_NEW AS AA WHERE CONVERT(VARCHAR(8), AA.DATE, 112) < '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND AA.KDITEM = B.KDITEM ORDER BY SEQ DESC), 0) "
            SQL &= ",KELUAR = 0 "
            SQL &= ",SISA = ISNULL((SELECT TOP 1 AA.SISA FROM A_STOCK_CARD_NEW AS AA WHERE CONVERT(VARCHAR(8), AA.DATE, 112) < '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND AA.KDITEM = B.KDITEM ORDER BY SEQ DESC), 0) "
            SQL &= ",HARGA = 0 "
            SQL &= ",SEQ =ISNULL((SELECT TOP 1 AA.SEQ FROM A_STOCK_CARD_NEW AS AA WHERE CONVERT(VARCHAR(8), AA.DATE, 112) < '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND AA.KDITEM = B.KDITEM ORDER BY SEQ DESC), 0) "
            SQL &= ",EXPIRE = '' "
            SQL &= ",KETERANGAN = '' "
            SQL &= "FROM M_ITEM B "
            SQL &= ") AS A "
            SQL &= ") "
            SQL &= ") AS A "
            SQL &= "WHERE A.KDITEM = '" & grdKDITEM.EditValue & "' "
            SQL &= "ORDER BY NMITEM2, SEQ ASC "


            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ITEM")

            grd.MainView = grv
            grd.DataSource = ds.Tables("ITEM")
            grd.ForceInitialize()

            fn_LoadFormatDataCard()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatDataCard()
        For iLoop As Integer = 0 To grv.Columns.Count - 1
            If grv.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

        grv.Columns("SEQ").Visible = False
        grv.Columns("SEQ").OptionsColumn.ShowInCustomizationForm = False

        grv.Columns("KDITEM").Visible = False
        grv.Columns("KDITEM").OptionsColumn.ShowInCustomizationForm = False

        grv.Columns("NMITEM2").Visible = False
        grv.Columns("NMITEM2").OptionsColumn.ShowInCustomizationForm = False

        'grv.Columns("NMITEM2").Group()
        'grv.ExpandAllGroups()

        grv.BestFitColumns()
    End Sub
    Public Sub fn_LoadLanguageCard()
        Try
            grv.Columns("NOREFERENCE").Caption = "No. Reference"
            grv.Columns("DATE").Caption = "Tanggal"
            grv.Columns("NMITEM2").Caption = "Nama Dagang"
            grv.Columns("HARGA").Caption = "Harga Beli"
            grv.Columns("MASUK").Caption = "Masuk"
            grv.Columns("KELUAR").Caption = "Keluar"
            grv.Columns("SISA").Caption = "Sisa"
            grv.Columns("BATCH").Caption = "Nomor Batch"
            grv.Columns("EXPIRE").Caption = "Tanggal Expire"
            grv.Columns("KETERANGAN").Caption = "Keterangan"

            MasterColumnChooserToolStripMenuItem.Text = Caption.ColumnChooserMaster
        Catch oErr As Exception

        End Try
    End Sub
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

        'Try
        '    grv.RestoreLayoutFromRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text & "\" & cboTYPE.Text)
        'Catch ex As Exception

        'End Try
    End Sub
#End Region
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
    Private Sub fn_LoadItem()
        Dim oITEM As New Reference.clsItem
        Try
            grdKDITEM.Properties.DataSource = oITEM.GetData.Where(Function(x) x.ISACTIVE = True And x.ISSTOK = True).ToList()
            grdKDITEM.Properties.ValueMember = "KDITEM"
            grdKDITEM.Properties.DisplayMember = "NMITEM2"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Hidden()
        If cboTYPE.SelectedIndex = 0 Then
            lDARI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lSAMPAI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lGUDANG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lITEM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            lDARI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lSAMPAI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lGUDANG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lITEM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub
    Private Sub cboTYPE_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTYPE.SelectedIndexChanged
        fn_LoadSecurity()
        fn_Hidden()
    End Sub
End Class