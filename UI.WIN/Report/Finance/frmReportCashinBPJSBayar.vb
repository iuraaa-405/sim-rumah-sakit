Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Imports DevExpress.XtraPrinting
Imports DevExpress.XtraSplashScreen
Imports System.Data
Imports System.Data.SqlClient


Public Class frmReportCashinBPJSBayar
    Implements ILanguage

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "Laporan Jasa"

        cboTYPE.Properties.Items.Clear()
        cboTYPE.Properties.Items.Add("Rekap")
        cboTYPE.Properties.Items.Add("Dokter DPJP")
        cboTYPE.Properties.Items.Add("Dokter Delegasi")
        cboTYPE.SelectedIndex = 0
    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed

    End Sub
    Public Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            Me.Text = "Laporan Jasa"

            cboTYPE.Properties.Items.Clear()
            cboTYPE.Properties.Items.Add("Rekap")
            cboTYPE.Properties.Items.Add("Dokter DPJP")
            cboTYPE.Properties.Items.Add("Dokter Delegasi")
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
                      Where x.MODUL = "CASHINBPJSBAYAR_R" _
                      And y.KDUSER = sUserID
                      Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                picPrint.Enabled = ds.ISPRINT
                picRefresh.Enabled = ds.ISVIEW

                If ds.ISVIEW = True Then
                    fn_LoadKDPAYMENTTYPE()
                    fn_LoadKDPAYMENTTYPE2()
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
        {sWATERMARK, "", Report.REPORT_PAGE & " : [Page # of Pages #]"})

            phf.Header.Content.AddRange(New String() _
{"", "LAPORAN JASA " & grdKDJUDULJASA.Text, "-" & cboTYPE.Text})

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
                    fn_LoadDataDokterDPJP()
                Case 2
                    fn_LoadDataDokterDelegasi()
            End Select

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#Region "Master Detail"
    Private Sub fn_LoadData()
        Try
            If grdKDJUDULJASA.Text <> "" And grdJudulTXT.Text <> "" Then

            Else
                MsgBox("Silahkan Masukan Judul Pemabayaran dan TXT", MsgBoxStyle.Exclamation, Me.Text)
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

            SQL = "EXEC R_JASAREKAP @KDBAYAR = '" & grdKDJUDULJASA.EditValue & "', @KDTXT = '" & grdJudulTXT.EditValue & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "F_CASHINBPJS_H")

            grd.MainView = grv
            grd.DataSource = ds.Tables("F_CASHINBPJS_H")
            grd.ForceInitialize()

            fn_LoadFormatData()

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDataDokterDPJP()
        Try
            If grdKDJUDULJASA.Text <> "" And grdJudulTXT.Text <> "" Then

            Else
                MsgBox("Silahkan Masukan Judul Pemabayaran dan TXT", MsgBoxStyle.Exclamation, Me.Text)
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

            SQL = "EXEC R_JASAREKAP_DOKTER @KDBAYAR = '" & grdKDJUDULJASA.EditValue & "', @KDTXT = '" & grdJudulTXT.EditValue & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "F_CASHINBPJS_H")

            grd.MainView = grv
            grd.DataSource = ds.Tables("F_CASHINBPJS_H")
            grd.ForceInitialize()

            fn_LoadFormatData()

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDataDokterDelegasi()
        Try
            If grdKDJUDULJASA.Text <> "" And grdJudulTXT.Text <> "" Then

            Else
                MsgBox("Silahkan Masukan Judul Pemabayaran dan TXT", MsgBoxStyle.Exclamation, Me.Text)
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

            SQL = "EXEC R_JASAREKAP_DOKTERDELEGASI @KDBAYAR = '" & grdKDJUDULJASA.EditValue & "', @KDTXT = '" & grdJudulTXT.EditValue & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "F_CASHINBPJS_H")

            grd.MainView = grv
            grd.DataSource = ds.Tables("F_CASHINBPJS_H")
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

        grv.BestFitColumns()

    End Sub
    Private Sub fn_LoadKDPAYMENTTYPE()
        Dim oPAYMENTTYPE As New Finance.clsCashinBPJSBayar
        Try
            Dim dsLis = From x In oPAYMENTTYPE.GetDataHeader
                        Select KATEGORI = IIf(x.CATEGORY = 0, "RJTL", "RITL"), x.KDCASHINBAYARBPJS, x.JUDULJASABAYAR

            grdKDJUDULJASA.Properties.DataSource = dsLis.ToList
            grdKDJUDULJASA.Properties.ValueMember = "KDCASHINBAYARBPJS"
            grdKDJUDULJASA.Properties.DisplayMember = "JUDULJASABAYAR"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDPAYMENTTYPE2()
        Dim oPAYMENTTYPE As New Finance.clsCashinBPJS
        Try
            Dim ds = From x In oPAYMENTTYPE.GetData
                     Select x.KDCASHINBPJS, MEMO = x.M_JUDULJASA.MEMO, KATEGORI = IIf(x.CATEGORY = 0, "RJTL", "RITL")

            grdJudulTXT.Properties.DataSource = ds.ToList()
            grdJudulTXT.Properties.ValueMember = "KDCASHINBPJS"
            grdJudulTXT.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
    Private Sub cboTYPE_SelectedIndexChanged() Handles cboTYPE.SelectedIndexChanged
        fn_LoadSecurity()
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