Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Imports DevExpress.XtraPrinting

Imports System.Data
Imports System.Data.SqlClient


Public Class frmReportTransaksiJasa
    Implements ILanguage

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = Jasa.TITLE_REPORT

        lDATEFROM.Text = Report.FILTER_DATEFROM
        lDATETO.Text = Report.FILTER_DATETO

        fn_LoadUOM()

        cboTYPE.Properties.Items.Clear()
        cboTYPE.Properties.Items.Add("Rekap")
        cboTYPE.Properties.Items.Add("Kelompok Bagi")
        cboTYPE.Properties.Items.Add("Group Kelompok Bagi")

        cboTYPE.SelectedIndex = 0

        deDATEFrom.DateTime = Now.AddDays((-Now.Day) + 1)
        deDATETo.DateTime = Now

        lKELOMPOK.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            If cboTYPE.SelectedIndex <> 2 Then
                grv.SaveLayoutToRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text & "\")

            End If

        Catch oErr As Exception

        End Try
    End Sub
    Public Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            Me.Text = Jasa.TITLE_REPORT

            lDATEFROM.Text = Report.FILTER_DATEFROM
            lDATETO.Text = Report.FILTER_DATETO

            fn_LoadLanguageAll()
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
                      Where x.MODUL = "REPORTJASA" _
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
            PrintableComponentLink.Landscape = True
            PrintableComponentLink.PaperKind = Printing.PaperKind.A4

            Dim phf As PageHeaderFooter =
        TryCast(PrintableComponentLink.PageHeaderFooter, PageHeaderFooter)
            phf.Header.Content.Clear()
            phf.Header.Font = New Font("Times New Roman", 14, FontStyle.Bold)
            phf.Header.LineAlignment = BrickAlignment.Center
            phf.Footer.Font = New Font("Times New Roman", 9.75)
            phf.Footer.LineAlignment = BrickAlignment.Far
            phf.Footer.Content.AddRange(New String() _
        {"", "", Report.REPORT_PAGE & " : [Page # of Pages #]"})

            phf.Header.Content.AddRange(New String() _
{"", Cash.TITLE_REPORT & vbCrLf & deDATEFrom.DateTime.ToString("dd/MM/yyyy") & " - " & deDATETo.DateTime.ToString("dd/MM/yyyy"), ""})

            PrintableComponentLink.Component = grd
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

            fn_LoadData01()

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

#Region "All"
    Private Sub fn_LoadData01()
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

            Dim oJasa As New Finance.clsJasa
            Dim dsDatabase = oJasa.GetDataSetting

            'SQL = "EXEC DATABASE_GUNTUR_BAK_.dbo.TRANSAKSI_NEW  "
            SQL = dsDatabase.COA_OTHER_INCOME
            SQL &= " @CATEGORY = " & cboTYPE.SelectedIndex & ", "
            SQL &= " @CATEGORY_STRING_1 = " & IIf(grdKDKELOMPOK.Text = String.Empty, "''", "'" & grdKDKELOMPOK.Text & "'") & ", "
            SQL &= " @CATEGORY_STRING_2 = '', "
            SQL &= " @CATEGORY_STRING_3 = '', "
            SQL &= " @DATEFROM = '" & deDATEFrom.DateTime & "', "
            SQL &= " @DATETO = '" & deDATETo.DateTime & "' "

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
        If cboTYPE.SelectedIndex = 2 Then
            grv.Columns("Kategori").Group()
            grv.Columns("KelompokBagi").Group()
            grv.ExpandAllGroups()

            'grv.Columns("Unit").VisibleIndex = 1
            'grv.Columns("Dokter").VisibleIndex = 2
            'grv.Columns("Total").VisibleIndex = 3

        End If
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
    Public Sub fn_LoadLanguageAll()
        Try
            grv.Columns("KDCASH").Caption = Cash.KDCASH
            grv.Columns("DATE").Caption = Cash.TANGGAL
            grv.Columns("KDPAYMENTTYPE").Caption = Cash.KDPAYMENTTYPE
            grv.Columns("KDCOA").Caption = Cash.KDCOA
            grv.Columns("MEMO").Caption = Cash.MEMO
            grv.Columns("TOTAL").Caption = Cash.TOTAL
            grv.Columns("KDUSER").Caption = Caption.User

            MasterColumnChooserToolStripMenuItem.Text = Caption.ColumnChooserMaster
        Catch oErr As Exception

        End Try
    End Sub
#End Region
    Private Sub fn_LoadUOM()
        Dim oUOM As New Reference.clsUOM
        Try
            grdKDKELOMPOK.Properties.DataSource = oUOM.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDKELOMPOK.Properties.ValueMember = "KDUOM"
            grdKDKELOMPOK.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
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

        Try
            If cboTYPE.SelectedIndex <> 2 Then
                grv.RestoreLayoutFromRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text & "\")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub cboTYPE_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTYPE.SelectedIndexChanged
        If cboTYPE.SelectedIndex = 0 Or cboTYPE.SelectedIndex = 2 Then
            lKELOMPOK.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            lKELOMPOK.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            grdKDKELOMPOK.EditValue = "UOM_0000000001"
        End If
    End Sub

#End Region
End Class