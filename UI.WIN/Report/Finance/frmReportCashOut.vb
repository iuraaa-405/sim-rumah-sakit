Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Imports DevExpress.XtraPrinting

Imports System.Data
Imports System.Data.SqlClient


Public Class frmReportCashOut
    Implements ILanguage

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = CashOut.TITLE_REPORT

        lTYPE.Text = Report.FILTER_TYPE
        lDATEFROM.Text = Report.FILTER_DATEFROM
        lDATETO.Text = Report.FILTER_DATETO

        cboTYPE.Properties.Items.Clear()
        cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_01)
        cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_02)
        cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_03)
        cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_04)
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
            Me.Text = CashOut.TITLE_REPORT

            lTYPE.Text = Report.FILTER_TYPE
            lDATEFROM.Text = Report.FILTER_DATEFROM
            lDATETO.Text = Report.FILTER_DATETO

            cboTYPE.Properties.Items.Clear()
            cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_01)
            cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_02)
            cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_03)
            cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_04)

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
                     Where x.MODUL = "CASHOUT_R" _
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
{"", CashOut.TITLE_REPORT & vbCrLf & deDATEFrom.DateTime.ToString("dd/MM/yyyy") & " - " & deDATETo.DateTime.ToString("dd/MM/yyyy"), ""})

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
            SQL &= "A.KDCASHOUT "
            SQL &= ",A.DATE "
            SQL &= ",KDVENDOR = B.NAME_DISPLAY "
            SQL &= ",KDPAYMENTTYPE = C.MEMO "
            SQL &= ",A.MEMO "

            SQL &= ",A.KDUSER "
            SQL &= ",A.SUBTOTAL "
            SQL &= ",A.ADMIN "
            SQL &= ",A.ROUND "
            SQL &= ",A.GRANDTOTAL "
            SQL &= "FROM F_CASHOUT_H A "
            SQL &= "INNER JOIN M_VENDOR B "
            SQL &= "ON A.KDVENDOR = B.KDVENDOR "
            SQL &= "INNER JOIN M_PAYMENTTYPE C "
            SQL &= "ON A.KDPAYMENTTYPE = C.KDPAYMENTTYPE "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "ORDER BY DATE, KDCASHOUT DESC"

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "F_CASHOUT_H")

            SQL = "SELECT "
            SQL &= "B.KDCASHOUT "
            SQL &= ",B.NOINVOICE "
            SQL &= ",B.AMOUNTPAYMENT "
            SQL &= ",B.REMARKS "
            SQL &= "FROM F_CASHOUT_H A "
            SQL &= "INNER JOIN F_CASHOUT_D B "
            SQL &= "ON A.KDCASHOUT = B.KDCASHOUT "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "F_CASHOUT_D")

            If cboTYPE.SelectedIndex = 0 Then
                Dim keyColumn As DataColumn = ds.Tables("F_CASHOUT_H").Columns("KDCASHOUT")
                Dim foreignKeyColumn As DataColumn = ds.Tables("F_CASHOUT_D").Columns("KDCASHOUT")
                ds.Relations.Add("FK_RELATION", keyColumn, foreignKeyColumn)

                grd.MainView = grv
                grd.DataSource = ds.Tables("F_CASHOUT_H")
                grd.ForceInitialize()

                grd.LevelTree.Nodes.Add("FK_RELATION", grv1)
                grv1.ViewCaption = "Details"

                grv1.PopulateColumns(ds.Tables("F_CASHOUT_D"))
                grv1.Columns("KDCASHOUT").VisibleIndex = -1

                fn_LoadFormatData()
                fn_LoadFormatDataDetail()
            ElseIf cboTYPE.SelectedIndex = 1 Then
                grd.MainView = grv
                grd.DataSource = ds.Tables("F_CASHOUT_H")
                grd.ForceInitialize()

                fn_LoadFormatData()
            ElseIf cboTYPE.SelectedIndex = 2 Then
                grd.MainView = grv1
                grd.DataSource = ds.Tables("F_CASHOUT_D")
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
            grv.Columns("KDCASHOUT").Caption = CashOut.KDCASHOUT
            grv.Columns("DATE").Caption = CashOut.TANGGAL
            grv.Columns("KDVENDOR").Caption = CashOut.KDVENDOR
            grv.Columns("KDPAYMENTTYPE").Caption = CashOut.KDPAYMENTTYPE
            grv.Columns("MEMO").Caption = CashOut.MEMO
            grv.Columns("SUBTOTAL").Caption = CashOut.SUBTOTAL
            grv.Columns("ADMIN").Caption = CashOut.ADMIN
            grv.Columns("ROUND").Caption = CashOut.ROUND
            grv.Columns("GRANDTOTAL").Caption = CashOut.GRANDTOTAL
            grv.Columns("KDUSER").Caption = Caption.User

            MasterColumnChooserToolStripMenuItem.Text = Caption.ColumnChooserMaster
            DetailColumnChooserToolStripMenuItem.Text = Caption.ColumnChooserDetail
        Catch oErr As Exception

        End Try
    End Sub
    Public Sub fn_LoadLanguageDetail()
        Try
            grv1.Columns("AMOUNTPAYMENT").Caption = CashOut.DETAIL_AMOUNTPAYMENT
            grv1.Columns("REMARKS").Caption = CashOut.DETAIL_REMARKS

            grv1.Columns("NOINVOICE").Caption = CashOut.DETAIL_NOINVOICE
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
            SQL &= "A.KDCASHOUT "
            SQL &= ",A.DATE "
            SQL &= ",KDVENDOR = B.NAME_DISPLAY "
            SQL &= ",KDPAYMENTTYPE = C.MEMO "
            SQL &= ",A.MEMO "

            SQL &= ",A.KDUSER "
            SQL &= ",A.SUBTOTAL "
            SQL &= ",A.ADMIN "
            SQL &= ",A.ROUND "
            SQL &= ",A.GRANDTOTAL "
            SQL &= "FROM F_CASHOUT_H A "
            SQL &= "INNER JOIN M_VENDOR B "
            SQL &= "ON A.KDVENDOR = B.KDVENDOR "
            SQL &= "INNER JOIN M_PAYMENTTYPE C "
            SQL &= "ON A.KDPAYMENTTYPE = C.KDPAYMENTTYPE "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= ") AS A "

            SQL &= "INNER JOIN "
            SQL &= "( "
            SQL &= "SELECT "
            SQL &= "B.KDCASHOUT "
            SQL &= ",B.NOINVOICE "
            SQL &= ",B.AMOUNTPAYMENT "
            SQL &= ",B.REMARKS "
            SQL &= "FROM F_CASHOUT_H A "
            SQL &= "INNER JOIN F_CASHOUT_D B "
            SQL &= "ON A.KDCASHOUT = B.KDCASHOUT "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= ") AS B "

            SQL &= "ON A.KDCASHOUT = B.KDCASHOUT "
            SQL &= "ORDER BY A.KDCASHOUT DESC "

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

        grv.Columns("KDCASHOUT1").Visible = False
        grv.Columns("KDCASHOUT1").OptionsColumn.ShowInCustomizationForm = False
    End Sub
    Public Sub fn_LoadLanguageAll()
        Try
            grv.Columns("KDCASHOUT").Caption = CashOut.KDCASHOUT
            grv.Columns("DATE").Caption = CashOut.TANGGAL
            grv.Columns("KDVENDOR").Caption = CashOut.KDVENDOR
            grv.Columns("KDPAYMENTTYPE").Caption = CashOut.KDPAYMENTTYPE
            grv.Columns("MEMO").Caption = CashOut.MEMO
            grv.Columns("SUBTOTAL").Caption = CashOut.SUBTOTAL
            grv.Columns("ADMIN").Caption = CashOut.ADMIN
            grv.Columns("ROUND").Caption = CashOut.ROUND
            grv.Columns("GRANDTOTAL").Caption = CashOut.GRANDTOTAL
            grv.Columns("KDUSER").Caption = Caption.User

            grv.Columns("AMOUNTPAYMENT").Caption = CashOut.DETAIL_AMOUNTPAYMENT
            grv.Columns("REMARKS").Caption = CashOut.DETAIL_REMARKS

            grv.Columns("NOINVOICE").Caption = CashOut.DETAIL_NOINVOICE

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