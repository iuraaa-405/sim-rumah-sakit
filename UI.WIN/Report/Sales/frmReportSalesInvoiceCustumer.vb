Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Imports DevExpress.XtraPrinting

Imports System.Data
Imports System.Data.SqlClient


Public Class frmReportSalesInvoiceCustomer

    Implements ILanguage
    Dim ds As New DataSet

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = SalesInvoice.TITLE_REPORT
        fn_LoadKDCUSTOMER()

        lDATEFROM.Text = Report.FILTER_DATEFROM
        lDATETO.Text = Report.FILTER_DATETO
        lKDCUSTOMER.Text = "Customer"




        deDATEFrom.DateTime = Now
        deDATETo.DateTime = Now
    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            grv.SaveLayoutToRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text & "\")
        Catch oErr As Exception

        End Try
    End Sub
    Public Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            Me.Text = SalesInvoice.TITLE_REPORT


            lDATEFROM.Text = Report.FILTER_DATEFROM
            lDATETO.Text = Report.FILTER_DATETO

            fn_LoadLanguageDetail()

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
                      Where x.MODUL = "SI_R" _
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

            Dim dsKeuntungan = (From x In oOtority.GetDataDetail
                                Join y In oUser.GetData
                     On x.KDOTORITY Equals y.KDOTORITY
                                Where x.MODUL = "PROFIT_R" _
                     And y.KDUSER = sUserID
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
        'Try
        '    Dim rpt As New xtraSalesInvoiceCustomer

        '    rpt.ShowPrintMarginsWarning = False
        '    rpt.Watermark.Text = sWATERMARK

        '    sDATE1 = deDATEFrom.DateTime
        '    sDATE2 = deDATETo.DateTime
        '    sCustomer = grdKDCUSTOMER.EditValue


        '    Dim sID As String = ""
        '    For i As Integer = 0 To grv.RowCount - 1
        '        If sID = "" Then
        '            sID += "'" & grv.GetRowCellValue(i, "NONOTA") & "'"
        '        Else
        '            sID += ", '" & grv.GetRowCellValue(i, "NONOTA") & "'"
        '        End If
        '    Next

        '    Dim sFILTER As String = "NONOTA IN (" & sID & ")"

        '    Dim dv = ds.Tables("R_INVOICE_CUSTOMER").DefaultView
        '    dv.RowFilter = sFILTER

        '    Dim dsFILTERED = New DataSet()
        '    Dim dtFILTERED = dv.ToTable()
        '    dsFILTERED.Tables.Add(dtFILTERED)

        '    rpt.bindingSource.DataSource = dsFILTERED.Tables("R_INVOICE_CUSTOMER")

        '    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
        '    printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)

        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub
    Private Sub fn_Preview()
        Try
            grv.Columns.Clear()
            grd.DataSource = Nothing
            grv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            grv1.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways


            fn_LoadDataDetil()


        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub fn_LoadKDCUSTOMER()
        Dim oCUSTOMER As New Reference.clsCustomer
        Try
            grdKDCUSTOMER.Properties.DataSource = oCUSTOMER.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDCUSTOMER.Properties.ValueMember = "KDCUSTOMER"
            grdKDCUSTOMER.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

#Region "Master Detail"

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


    Public Sub fn_LoadLanguageDetail()
        Try
            grv.Columns("TANGGAL").Caption = "Tanggal"
            grv.Columns("NONOTA").Caption = "No Nota"
            grv.Columns("TOTAL").Caption = "Total"
            grv.Columns("TITIP").Caption = "Titip"
            grv.Columns("KELOMPOK").Caption = "Kelompok"

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
            Dim SQL As String

            ds = New DataSet



            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "TANGGAL = A.DATE "
            SQL &= ", NONOTA = A.KDSI "
            SQL &= ",TOTAL = A.GRANDTOTAL "
            SQL &= ",TITIP = A.PAYAMOUNT "
            SQL &= ", KELOMPOK = B.DUE "
            SQL &= "FROM S_SI_H AS A "
            SQL &= "INNER JOIN M_CUSTOMER AS B "
            SQL &= "ON A.KDCUSTOMER = B.KDCUSTOMER "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "'  AND A.KDCUSTOMER = '" & grdKDCUSTOMER.EditValue & "' AND PAYAMOUNT < GRANDTOTAL "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "R_INVOICE_CUSTOMER")

            grd.MainView = grv
            grd.DataSource = ds.Tables("R_INVOICE_CUSTOMER")
            grd.ForceInitialize()
            fn_LoadFormatData()

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

#End Region
#Region "All"



#End Region

    Private Sub MasterColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MasterColumnChooserToolStripMenuItem.Click
        grv.ShowCustomization()
    End Sub
    Private Sub DetailColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DetailColumnChooserToolStripMenuItem.Click
        grv1.ShowCustomization()
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
            grv.RestoreLayoutFromRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text & "\")
        Catch ex As Exception

        End Try
    End Sub
#End Region
End Class