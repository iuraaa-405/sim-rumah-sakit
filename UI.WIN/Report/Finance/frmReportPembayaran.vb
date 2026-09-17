Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Imports DevExpress.XtraPrinting

Imports System.Data
Imports System.Data.SqlClient


Public Class frmReportPembayaran
    Implements ILanguage

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "Laporan Pembayaran"

        lTYPE.Text = Report.FILTER_TYPE
        lDATEFROM.Text = Report.FILTER_DATEFROM
        lDATETO.Text = Report.FILTER_DATETO

        cboTYPE.Properties.Items.Clear()
        cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_01)
        cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_02)
        cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_03)
        'cboTYPE.Properties.Items.Add("Rawat Jalan")
        'cboTYPE.Properties.Items.Add("Rawat Inap")
        cboTYPE.SelectedIndex = 0

        deDATEFrom.DateTime = Now.AddDays((-Now.Day) + 1)
        deDATETo.DateTime = Now

        listNoInvoiceAmbil.Clear()
    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            If grv.SelectedRowsCount = 0 Then
                'MessageBox.Show("Tidak ada baris yang dipilih!")
            Else
                For Each rowHandle As Integer In grv.GetSelectedRows()
                    Dim noInvoice As Object = grv.GetRowCellValue(rowHandle, "KDCASHIN")
                    Dim noInvoice_Cek As Object = grv.GetRowCellValue(rowHandle, "KasMasuk")

                    If noInvoice IsNot Nothing Then
                        If noInvoice_Cek = False Then
                            listNoInvoiceAmbil.Add(noInvoice)
                        End If
                    End If
                Next
            End If
        Catch oErr As Exception

        End Try
    End Sub
    Public Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            Me.Text = "Laporan Pembayaran"

            lTYPE.Text = Report.FILTER_TYPE
            lDATEFROM.Text = Report.FILTER_DATEFROM
            lDATETO.Text = Report.FILTER_DATETO

            cboTYPE.Properties.Items.Clear()
            cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_01)
            cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_02)
            cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_03)
            'cboTYPE.Properties.Items.Add("Rawat Jalan")
            'cboTYPE.Properties.Items.Add("Rawat Inap")

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

            Dim ds = (From x In oOtority.GetDataDetail
                      Join y In oUser.GetData
                     On x.KDOTORITY Equals y.KDOTORITY
                      Where x.MODUL = "CASHIN_R" _
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
{"", "Laporan Pembayaran" & vbCrLf & deDATEFrom.DateTime.ToString("dd/MM/yyyy") & " - " & deDATETo.DateTime.ToString("dd/MM/yyyy"), ""})

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
                fn_LoadDataRawaJalanInap(True)
            ElseIf cboTYPE.SelectedIndex = 4
                fn_LoadDataRawaJalanInap(False)
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
            'SQL &= ",Kategori = (SELECT CASE A.CATEGORY WHEN 0 THEN 'Rawat Jalan' WHEN 1 THEN 'Rawat Inap' WHEN '2' THEN 'Laboratorium' WHEN '3' THEN 'Radiologi' ELSE 'Apotek' END) "
            SQL &= ",A.KDPENDAFTARAN "
            SQL &= ",NoMedrek = (SELECT KDCUSTOMER FROM S_PENDAFTARAN_H WHERE A.KDPENDAFTARAN = KDPENDAFTARAN) "
            SQL &= ",NamaPasien = (SELECT AA.NAME_DISPLAY FROM M_CUSTOMER AA INNER JOIN S_PENDAFTARAN_H BB ON AA.KDCUSTOMER = BB.KDCUSTOMER WHERE A.KDPENDAFTARAN = BB.KDPENDAFTARAN) "
            SQL &= ",UNIT = (SELECT AA.NAME_DISPLAY FROM M_DEPARTMENT AA INNER JOIN S_PENDAFTARAN_H BB ON AA.KDDEPARTMENT = BB.KDDEPARTMENT WHERE A.KDPENDAFTARAN = BB.KDPENDAFTARAN) "
            SQL &= ",A.DATE "
            SQL &= ",KDPAYMENTTYPE = B.MEMO "
            SQL &= ",A.SUBTOTAL "
            SQL &= ",A.ADMIN "
            SQL &= ",A.COSTSHARE "
            SQL &= ",A.DEPOSIT "
            SQL &= ",A.ROUND "
            SQL &= ",A.GRANDTOTAL "
            'SQL &= ",A.ISSETOR "
            SQL &= ",A.MEMO "
            SQL &= ",A.KDUSER "
            SQL &= ",SHIFT = A.KDSHIFT "
            SQL &= ",KasMasuk = ISNULL((SELECT CONVERT(BIT, 1) FROM F_SETOR_D WHERE A.KDCASHIN = NOINVOICE), CONVERT(BIT, 0)) "
            SQL &= "FROM F_CASHIN_H A "
            SQL &= "INNER JOIN M_PAYMENTTYPE B "
            SQL &= "ON A.KDPAYMENTTYPE = B.KDPAYMENTTYPE "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "ORDER BY DATE, KDCASHIN DESC"

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "P_PO_H")

            SQL = "SELECT "
            SQL &= "B.KDCASHIN "
            SQL &= ",B.NOINVOICE "
            SQL &= ",B.AMOUNTORIGINAL "
            SQL &= ",B.AMOUNTDUE "
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
            da.Fill(ds, "P_PO_D")

            If cboTYPE.SelectedIndex = 0 Then
                Dim keyColumn As DataColumn = ds.Tables("P_PO_H").Columns("KDCASHIN")
                Dim foreignKeyColumn As DataColumn = ds.Tables("P_PO_D").Columns("KDCASHIN")
                ds.Relations.Add("FK_RELATION", keyColumn, foreignKeyColumn)

                grd.MainView = grv
                grd.DataSource = ds.Tables("P_PO_H")
                grd.ForceInitialize()

                grd.LevelTree.Nodes.Add("FK_RELATION", grv1)
                grv1.ViewCaption = "Details"

                grv1.PopulateColumns(ds.Tables("P_PO_D"))
                grv1.Columns("KDCASHIN").VisibleIndex = -1

                fn_LoadFormatData()
                fn_LoadFormatDataDetail()
            ElseIf cboTYPE.SelectedIndex = 1 Then
                grd.MainView = grv
                grd.DataSource = ds.Tables("P_PO_H")
                grd.ForceInitialize()

                fn_LoadFormatData()
            ElseIf cboTYPE.SelectedIndex = 2 Then
                grd.MainView = grv1
                grd.DataSource = ds.Tables("P_PO_D")
                grd.ForceInitialize()

                fn_LoadFormatDataDetail()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatData()
        'For iLoop As Integer = 0 To grv.Columns.Count - 1
        '    If grv.Columns(iLoop).ColumnType.Name = "Decimal" Then
        '        grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        '        grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n0}"
        '        grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        '    ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
        '        grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        '        grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
        '    End If
        'Next

        'grv.Columns("GRANDTOTAL").SummaryItem.FieldName = "GRANDTOTAL"
        'grv.Columns("GRANDTOTAL").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        'grv.Columns("GRANDTOTAL").SummaryItem.DisplayFormat = "{0:n0}"


        'grv.Columns("Kategori").Group()
        'grv.ExpandAllGroups()

    End Sub
    Private Sub fn_LoadFormatDataDetail()
        For iLoop As Integer = 0 To grv1.Columns.Count - 1
            If grv1.Columns(iLoop).FieldName = "GRANDTOTAL" Then
                grv1.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv1.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv1.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            End If
        Next

        'grv1.Columns("UNIT").Group()
        'grv1.ExpandAllGroups()

    End Sub
    Public Sub fn_LoadLanguageMaster()
        Try
            grv.Columns("KDCASHIN").Caption = CashIn.KDCASHIN
            grv.Columns("DATE").Caption = CashIn.TANGGAL
            grv.Columns("KDPENDAFTARAN").Caption = CashIn.KDPENDAFTARAN
            grv.Columns("KDPAYMENTTYPE").Caption = CashIn.KDPAYMENTTYPE
            grv.Columns("SUBTOTAL").Caption = CashIn.SUBTOTAL
            grv.Columns("ADMIN").Caption = CashIn.ADMIN
            grv.Columns("COSTSHARE").Caption = CashIn.COSTSHARE
            grv.Columns("DEPOSIT").Caption = CashIn.DEPOSIT
            grv.Columns("ROUND").Caption = CashIn.ROUND
            grv.Columns("GRANDTOTAL").Caption = CashIn.GRANDTOTAL
            grv.Columns("ISSETOR").Caption = "Setor"
            grv.Columns("MEMO").Caption = CashIn.MEMO
            grv.Columns("KDUSER").Caption = Caption.User
            grv.Columns("SHIFT").Caption = "Shift"

            MasterColumnChooserToolStripMenuItem.Text = Caption.ColumnChooserMaster
            DetailColumnChooserToolStripMenuItem.Text = Caption.ColumnChooserDetail
        Catch oErr As Exception

        End Try
    End Sub
    Public Sub fn_LoadLanguageDetail()
        Try
            grv1.Columns("NOINVOICE").Caption = CashIn.DETAIL_NOINVOICE
            grv1.Columns("AMOUNTORIGINAL").Caption = CashIn.DETAIL_AMOUNTORIGINAL
            grv1.Columns("AMOUNTDUE").Caption = CashIn.DETAIL_AMOUNTDUE
            grv1.Columns("AMOUNTPAYMENT").Caption = CashIn.DETAIL_AMOUNTPAYMENT
            grv1.Columns("REMARKS").Caption = CashIn.DETAIL_REMARKS

        Catch oErr As Exception

        End Try
    End Sub
#End Region
#Region "All"
    Private Sub fn_LoadDataRawaJalanInap(ByVal rj As Boolean)
        Try
            MsgBox("Belum Tersedia", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub

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


            If rj = True Then
                SQL = "EXEC "

            Else
                SQL = ""

            End If


            'SQL = "SELECT "
            'SQL &= "A.* "
            'SQL &= ",B.* "
            'SQL &= "FROM "
            'SQL &= "( "
            'SQL &= "SELECT "
            'SQL &= "A.KDCASHIN "
            'SQL &= ",A.DATE "
            'SQL &= ",KDVENDOR = B.NAME_DISPLAY "
            'SQL &= ",A.MEMO "

            'SQL &= ",A.KDUSER "
            'SQL &= "FROM P_PO_H A "
            'SQL &= "INNER JOIN M_VENDOR B "
            'SQL &= "ON A.KDVENDOR = B.KDVENDOR "
            'SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            'SQL &= ") AS A "

            'SQL &= "INNER JOIN "
            'SQL &= "( "
            'SQL &= "SELECT "
            'SQL &= "B.KDCASHIN "
            'SQL &= ",C.NMITEM1 "
            'SQL &= ",C.NMITEM2 "
            'SQL &= ",C.NMITEM3 "
            'SQL &= ",B.QTY "
            'SQL &= ",KDUOM = D.MEMO "
            'SQL &= ",B.REMARKS "
            'SQL &= "FROM P_PO_H A "
            'SQL &= "INNER JOIN P_PO_D B "
            'SQL &= "ON A.KDCASHIN = B.KDCASHIN "
            'SQL &= "INNER JOIN M_ITEM C "
            'SQL &= "ON B.KDITEM = C.KDITEM "
            'SQL &= "INNER JOIN M_UOM D "
            'SQL &= "ON B.KDUOM = D.KDUOM "
            'SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            'SQL &= ") AS B "

            'SQL &= "ON A.KDCASHIN = B.KDCASHIN "
            'SQL &= "ORDER BY A.KDCASHIN DESC "

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

        grv.Columns("KDCASHIN").Visible = False
        grv.Columns("KDCASHIN").OptionsColumn.ShowInCustomizationForm = False
    End Sub
    Public Sub fn_LoadLanguageAll()
        'Try
        '    grv.Columns("KDCASHIN").Caption = CashIn.KDCASHIN
        '    grv.Columns("DATE").Caption = CashIn.TANGGAL
        '    grv.Columns("CATEGORY").Caption = CashIn.CATEGORY
        '    grv.Columns("SHIFT").Caption = CashIn.SHIFT
        '    grv.Columns("MEMO").Caption = CashIn.MEMO
        '    grv.Columns("KDUSER").Caption = Caption.User

        '    grv.Columns("NOINVOICE").Caption = CashIn.DETAIL_NOINVOICE
        '    grv.Columns("AMOUNTPAYMENT").Caption = CashIn.DETAIL_AMOUNTPAYMENT
        '    grv.Columns("REMARKS").Caption = CashIn.DETAIL_REMARKS

        '    MasterColumnChooserToolStripMenuItem.Text = Caption.ColumnChooserMaster
        '    DetailColumnChooserToolStripMenuItem.Text = Caption.ColumnChooserDetail
        'Catch oErr As Exception

        'End Try
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