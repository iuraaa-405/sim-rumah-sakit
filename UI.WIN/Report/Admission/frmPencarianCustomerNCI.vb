Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports MySql.Data.MySqlClient
Imports DevExpress.XtraPrinting
Imports System.Data.SqlClient


Public Class frmPencarianCustomerNCI
    Implements ILanguage
    Private oPendaftaran As New Admission.clsPendaftaran

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "Pencarian Pasien"
    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        'Try
        '    grv.SaveLayoutToRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text)
        'Catch oErr As Exception

        'End Try
    End Sub
    Public Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            Me.Text = "Pencarian Pasien"
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
                      Where x.MODUL = "PENDAFTARAN" _
                      And y.KDUSER = sUserID
                      Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                picPrint.Enabled = ds.ISPRINT

                If ds.ISVIEW = True Then
                    fn_LoadLanguage()
                End If
            Catch oErr As Exception
                MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)

                picPrint.Enabled = False
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
{"", "Pencarian Pasien Tanggal " & Now.ToString("dd-MM-yyyy"), ""})

            printableComponentLink.Component = grd
            PrintableComponentLink.CreateDocument()
            PrintableComponentLink.ShowPreviewDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

#Region "Master Detail"
    'Private Sub fn_LoadDataNCI(ByVal Parameter As String)
    '    Try
    '        grv.Columns.Clear()
    '        grd.DataSource = Nothing
    '        grv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
    '        grv1.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

    '        Dim oConn As New SqlConnection
    '        Dim oComm As New SqlCommand
    '        Dim da As SqlDataAdapter
    '        Dim ds As New DataSet
    '        Dim SQL As String

    '        Dim sConn As String = sConnGuntur
    '        oConn = New SqlConnection(sConn)

    '        If oConn.State = ConnectionState.Closed Then
    '            oConn.Open()
    '        End If

    '        SQL = " SELECT  "
    '        SQL &= " norekammedis = A.KD_PASIEN "
    '        SQL &= " ,namapasien = A.NAMA "
    '        SQL &= " ,alamat = A.ALAMAT "
    '        SQL &= " ,nrp = ISNULL((SELECT TOP 1 NRP FROM PASIEN_DINAS WHERE A.KD_PASIEN = KD_PASIEN), '') "
    '        SQL &= " ,tempatlahir = A.TEMPAT_LAHIR "
    '        SQL &= " ,tanggallahir = A.TGL_LAHIR "
    '        SQL &= " ,Noasuransi = A.NO_ASURANSI "
    '        SQL &= " FROM PASIEN A "
    '        SQL &= " WHERE "
    '        If cboCARI.SelectedIndex = 0 Then

    '            Dim Data1 As String = ""
    '            Dim Data2 As String = ""
    '            Dim Data3 As String = ""
    '            Dim Data As String = ""

    '            Dim Parameter_ As Integer = 0

    '            Parameter_ = Parameter

    '            Data = Parameter_.ToString.PadLeft(6, "0")

    '            Data1 = Data.Substring(0, 2)
    '            Data2 = Data.Substring(2, 2)
    '            Data3 = Data.Substring(4, 2)

    '            SQL &= "A.KD_PASIEN = '" & "0-" & Data1 & "-" & Data2 & "-" & Data3 & "' "
    '        ElseIf cboCARI.SelectedIndex = 1 Then
    '            SQL &= "A.NAMA LIKE '%" & txtCARI.Text.ToString & "%' "
    '        Else
    '            SQL &= "A.ALAMAT LIKE '%" & txtCARI.Text.ToString & "%' "
    '        End If

    '        oComm.Connection = oConn
    '        oComm.CommandText = SQL
    '        oComm.CommandTimeout = 120
    '        oComm.CommandType = CommandType.Text

    '        da = New SqlDataAdapter(oComm)
    '        da.Fill(ds, "PASIEN")

    '        grd.MainView = grv
    '        grd.DataSource = ds.Tables("PASIEN")
    '        grd.ForceInitialize()
    '        fn_LoadFormatData()

    '        If oConn.State = ConnectionState.Open Then
    '            oConn.Close()
    '        End If

    '    Catch oErr As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
    'Private Sub fn_LoadDatadgCare(ByVal Parameter As String)
    '    Try
    '        grv.Columns.Clear()
    '        grd.DataSource = Nothing
    '        grv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
    '        grv1.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

    '        Dim oConnNpgsql As New NpgsqlConnection
    '        Dim oCommNpgsql As New NpgsqlCommand
    '        Dim daNpgsql As NpgsqlDataAdapter
    '        Dim dsNpgsql As New DataSet
    '        Dim SQLNpgsql As String

    '        Dim sConnNpgsql As String = sConnNpgsqlproduction

    '        oConnNpgsql = New NpgsqlConnection(sConnNpgsql)

    '        If oConnNpgsql.State = ConnectionState.Closed Then
    '            oConnNpgsql.Open()
    '        End If

    '        SQLNpgsql = " SELECT  "
    '        SQLNpgsql &= "A.nocm as norekammedis "
    '        SQLNpgsql &= ",A.noidentitas as ktp "
    '        SQLNpgsql &= ",A.nobpjs "
    '        SQLNpgsql &= ",A.namapasien as namapasien "
    '        SQLNpgsql &= ",A.alamatrmh as alamat1 "
    '        SQLNpgsql &= ",B.alamatlengkap as alamat2 "
    '        SQLNpgsql &= ",A.nip as nrp "
    '        SQLNpgsql &= ",A.tempatlahir as tempatlahir "
    '        SQLNpgsql &= ",A.tgllahir as tanggallahir "
    '        SQLNpgsql &= ",A.nobpjs as noasuransi "
    '        SQLNpgsql &= "FROM pasien_m A "
    '        SQLNpgsql &= "left join alamat_m B on A.id = B.nocmfk "
    '        SQLNpgsql &= "WHERE "
    '        If cboCARI.SelectedIndex = 0 Then
    '            SQLNpgsql &= "A.nocm = '" & txtCARI.Text.ToString.PadLeft(9, "0") & "' "
    '        ElseIf cboCARI.SelectedIndex = 1 Then
    '            SQLNpgsql &= "A.namapasien LIKE '%" & txtCARI.Text.ToString & "%' "
    '        Else
    '            SQLNpgsql &= "B.alamatlengkap LIKE '%" & txtCARI.Text.ToString & "%' "
    '        End If

    '        oCommNpgsql.Connection = oConnNpgsql
    '        oCommNpgsql.CommandText = SQLNpgsql
    '        oCommNpgsql.CommandTimeout = 120
    '        oCommNpgsql.CommandType = CommandType.Text

    '        daNpgsql = New NpgsqlDataAdapter(oCommNpgsql)
    '        daNpgsql.Fill(dsNpgsql, "PASIEN")

    '        grd.MainView = grv
    '        grd.DataSource = dsNpgsql.Tables("PASIEN")
    '        grd.ForceInitialize()
    '        fn_LoadFormatData()

    '        If oConnNpgsql.State = ConnectionState.Open Then
    '            oConnNpgsql.Close()
    '        End If

    '    Catch oErr As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
    Private Sub fn_LoadDataSIMGOS()
        Try
            grv.Columns.Clear()
            grd.DataSource = Nothing
            grv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            grv1.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            Dim oConnMySql As New MySqlConnection
            Dim oCommMySql As New MySqlCommand
            Dim daMySql As MySqlDataAdapter
            Dim dsMySql As New DataSet
            Dim SQLMySql As String

            Dim sConnMySql As String = sMySQL_Url

            oConnMySql = New MySqlConnection(sConnMySql)

            If oConnMySql.State = ConnectionState.Closed Then
                oConnMySql.Open()
            End If

            SQLMySql = " SELECT  "
            SQLMySql &= "A.NORM as norekammedis "
            SQLMySql &= ",A.NAMA as namapasien "
            SQLMySql &= ",A.ALAMAT as alamat "
            SQLMySql &= ",A.TEMPAT_LAHIR as tempatlahir "
            SQLMySql &= ",A.TANGGAL_LAHIR as tanggallahir "
            SQLMySql &= "FROM pasien A "
            SQLMySql &= "WHERE "
            If cboCARI.SelectedIndex = 0 Then
                SQLMySql &= "A.NORM LIKE '%" & txtCARI.Text.ToString & "%' "
            ElseIf cboCARI.SelectedIndex = 1 Then
                SQLMySql &= "A.NAMA LIKE '%" & txtCARI.Text.ToString & "%' "
            Else
                SQLMySql &= "A.ALAMAT LIKE '%" & txtCARI.Text.ToString & "%' "
            End If

            oCommMySql.Connection = oConnMySql
            oCommMySql.CommandText = SQLMySql
            oCommMySql.CommandTimeout = 120
            oCommMySql.CommandType = CommandType.Text

            daMySql = New MySqlDataAdapter(oCommMySql)
            daMySql.Fill(dsMySql, "PASIEN")

            grd.MainView = grv
            grd.DataSource = dsMySql.Tables("PASIEN")
            grd.ForceInitialize()
            fn_LoadFormatData()

            If oConnMySql.State = ConnectionState.Open Then
                oConnMySql.Close()
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
#End Region
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
        End Select
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        Try
            fn_Print()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub txtCARI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCARI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If txtCARI.Text.Length < 3 Then
                MsgBox("Minimal 3 Karakter", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            Else
                fn_LoadDataSIMGOS()

                'If chkdGCare.Checked = False Then
                '    fn_LoadDataNCI(txtCARI.Text.ToString)
                'Else
                '    fn_LoadDatadgCare(txtCARI.Text.ToString)
                'End If
            End If
        End If
    End Sub
    Private Sub grd_DoubleClick(sender As Object, e As EventArgs) Handles grd.DoubleClick
        If grv.GetFocusedRowCellValue("norekammedis") Is Nothing Then
            Exit Sub
        End If

        sKD_PASIENdgCare = (grv.GetFocusedRowCellValue("norekammedis").Replace("-", "")).ToString.PadLeft(9, "0")

        'If chkdGCare.Checked = False Then
        '    sKD_PASIENnci = (grv.GetFocusedRowCellValue("norekammedis").Replace("-", "")).ToString.PadLeft(9, "0")
        'Else
        '    sKD_PASIENdgCare = (grv.GetFocusedRowCellValue("norekammedis").Replace("-", "")).ToString.PadLeft(9, "0")
        'End If

        Me.Close()

    End Sub
#End Region
End Class