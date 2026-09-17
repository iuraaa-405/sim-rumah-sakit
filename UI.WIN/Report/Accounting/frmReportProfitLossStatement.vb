Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Imports DevExpress.XtraPrinting

Imports System.Data
Imports System.Data.SqlClient


Public Class frmGrouper2List
    Implements ILanguage

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = Journal.TITLE_PROFITLOSSSTATEMENT

        lMONTH.Text = Report.FILTER_MONTH
        lYEAR.Text = Report.FILTER_YEAR
        lTYPE.Text = Report.FILTER_TYPE

        For iLoop As Integer = 1 To 12
            cboMONTH.Properties.Items.Add(MonthName(iLoop))
        Next

        For iLoop As Integer = 2000 To Now.Year + 50
            cboYEAR.Properties.Items.Add(iLoop)
        Next

        cboTYPE.Properties.Items.Add(Report.FILTER_MONTH)
        cboTYPE.Properties.Items.Add(Report.FILTER_YEAR)

        cboMONTH.SelectedIndex = Now.Month - 1
        cboYEAR.Text = Now.Year
        cboTYPE.SelectedIndex = 0

        fn_LoadSecurity()
    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            grv.SaveLayoutToRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text & "\")
        Catch oErr As Exception

        End Try
    End Sub
    Public Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            Me.Text = Journal.TITLE_PROFITLOSSSTATEMENT

            lMONTH.Text = Report.FILTER_MONTH
            lYEAR.Text = Report.FILTER_YEAR
            lTYPE.Text = Report.FILTER_TYPE

            cboTYPE.Properties.Items.Clear()
            cboTYPE.Properties.Items.Add(Report.FILTER_MONTH)
            cboTYPE.Properties.Items.Add(Report.FILTER_YEAR)

            fn_LoadLanguageAll()
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
                     Where x.MODUL = "PROFITLOSSSTATEMENT_R" _
                     And y.KDUSER = sUserID _
                     Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                picPrint.Enabled = ds.ISPRINT
                picRefresh.Enabled = ds.ISVIEW

                If ds.ISVIEW = True Then
                    fn_Preview()
                    fn_LoadLanguage()

                    Try
                        grv.RestoreLayoutFromRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text & "\")
                    Catch ex As Exception

                    End Try
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

            If cboTYPE.SelectedIndex = 0 Then
                phf.Header.Content.AddRange(New String() _
{"", Journal.TITLE_PROFITLOSSSTATEMENT & vbCrLf & cboMONTH.Text & " " & cboYEAR.Text, ""})
            Else
                phf.Header.Content.AddRange(New String() _
{"", Journal.TITLE_PROFITLOSSSTATEMENT & vbCrLf & cboYEAR.Text, ""})
            End If

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

            SQL = "SELECT "
            SQL &= "GROUPS, KDCOA, NMCOA, BALANCE = SUM(BALANCE) "
            SQL &= "FROM "

            SQL &= "( "
            SQL &= "( "
            For iLoop As Integer = 1 To 12
                If iLoop <> 1 Then
                    SQL &= " UNION "
                End If

                SQL &= "( "
                SQL &= "SELECT "
                SQL &= "YEAR = B.YEAR "
                SQL &= ",MONTH = " & iLoop & " "
                SQL &= ",GROUPS = A.GROUPS "
                SQL &= ",KDCOA = B.KDCOA "
                SQL &= ",NMCOA = A.NMCOA "
                SQL &= ",CASE WHEN A.DC = 'C' AND A.GROUPS = 'PENDAPATAN' THEN B.SALDO" & IIf(iLoop < 10, "0" & iLoop, iLoop) & " WHEN A.DC = 'D' AND A.GROUPS = 'PENDAPATAN' THEN -(B.SALDO" & IIf(iLoop < 10, "0" & iLoop, iLoop) & ") WHEN A.DC = 'D' AND A.GROUPS = 'PENGELUARAN' THEN B.SALDO" & IIf(iLoop < 10, "0" & iLoop, iLoop) & " WHEN A.DC = 'C' AND A.GROUPS = 'PENGELUARAN' THEN -(B.SALDO" & IIf(iLoop < 10, "0" & iLoop, iLoop) & ") WHEN A.DC = 'D' AND A.GROUPS = 'LAIN - LAIN' THEN B.SALDO" & IIf(iLoop < 10, "0" & iLoop, iLoop) & " WHEN A.DC = 'C' AND A.GROUPS = 'LAIN - LAIN' THEN -(B.SALDO" & IIf(iLoop < 10, "0" & iLoop, iLoop) & ") END AS BALANCE "
                SQL &= ",PARENTKDCOA = A.PARENTKDCOA "
                SQL &= "FROM "
                SQL &= "A_COA AS A "
                SQL &= "LEFT JOIN "
                SQL &= "A_COA_BALANCE AS B "
                SQL &= "ON B.KDCOA = A.KDCOA "
                SQL &= "WHERE A.TYPE = 1 AND A.GROUPS IN ('PENDAPATAN','LAIN - LAIN','PENGELUARAN') "
                SQL &= ") "
            Next
            SQL &= ") "
            SQL &= ") AS A "

            If cboTYPE.SelectedIndex = 0 Then
                SQL &= "WHERE YEAR = " & cboYEAR.Text & " AND MONTH = " & cboMONTH.SelectedIndex + 1 & " "
            Else
                SQL &= "WHERE (YEAR = " & cboYEAR.Text & " AND MONTH <= " & cboMONTH.SelectedIndex + 1 & ") OR (YEAR = " & cboYEAR.Text & ") "
            End If

            SQL &= "GROUP BY GROUPS, KDCOA, NMCOA, PARENTKDCOA "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

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

                grv.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, grv.Columns(iLoop).FieldName, grv.Columns(iLoop), "{0:n2}")

                grv.Columns(iLoop).SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Custom, String.Empty)

                grv.Columns(iLoop).OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
            ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

        grv.ExpandAllGroups()
    End Sub

    Dim sBalance1 As Decimal
    Dim sBalance2 As Decimal
    Dim sBalance3 As Decimal

    Private Sub grv_CustomSummaryCalculate(ByVal sender As Object, ByVal e As  _
     DevExpress.Data.CustomSummaryEventArgs) Handles grv.CustomSummaryCalculate
        Dim summaryID As Integer = Convert.ToInt32(CType(e.Item, DevExpress.XtraGrid.GridSummaryItem).Tag)
        Dim View As DevExpress.XtraGrid.Views.Grid.GridView = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
        ' Initialization 
        If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Start Then
            sBalance1 = 0
            sBalance2 = 0
            sBalance3 = 0
        End If
        ' Calculation 
        If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Calculate Then
            If View.GetRowCellValue(e.RowHandle, "GROUPS") = "PENDAPATAN" Then
                sBalance1 += CDec(View.GetRowCellValue(e.RowHandle, "BALANCE"))
            ElseIf View.GetRowCellValue(e.RowHandle, "GROUPS") = "PENGELUARAN" Then
                sBalance2 += CDec(View.GetRowCellValue(e.RowHandle, "BALANCE"))
            ElseIf View.GetRowCellValue(e.RowHandle, "GROUPS") = "LAIN - LAIN" Then
                sBalance3 += CDec(View.GetRowCellValue(e.RowHandle, "BALANCE"))
            End If
        End If
        ' Finalization 
        If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Finalize Then
            If (sBalance1 > sBalance2 + sBalance3) Then
                e.TotalValue = "LABA = " & FormatNumber(sBalance1 - (sBalance2 + sBalance3), 2)
            Else
                e.TotalValue = "RUGI = " & FormatNumber(Math.Abs(sBalance1 - (sBalance2 + sBalance3)), 2)
            End If
        End If
    End Sub
    Public Sub fn_LoadLanguageAll()
        Try
            grv.Columns("KDCOA").Caption = COA.KDCOA
            grv.Columns("NMCOA").Caption = COA.NMCOA
            grv.Columns("GROUPS").Caption = COA.GROUPS
            grv.Columns("BALANCE").Caption = Journal.DETAIL_BALANCE

            MasterColumnChooserToolStripMenuItem.Text = Caption.ColumnChooserMaster
        Catch oErr As Exception

        End Try
    End Sub
#End Region

    Private Sub MasterColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MasterColumnChooserToolStripMenuItem.Click
        grv.ColumnsCustomization()
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