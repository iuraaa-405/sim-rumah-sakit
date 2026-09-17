Imports UI.WIN.MAIN.My.Resources
Imports MySql.Data.MySqlClient

Public Class frmReportWebAntrian
    Implements ILanguage

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "Identitas Pasien"
        fn_Preview()

        WebBrowser1.ScriptErrorsSuppressed = True
        WebBrowser1.Navigate("https://p1.mail-sender.my.id/verifikator")
    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed

    End Sub
    Public Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            Me.Text = "Identitas Pasien"

            'fn_LoadLanguageAll()

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Preview()
        'Try
        '    grv.Columns.Clear()
        '    grd.DataSource = Nothing
        '    grv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        '    grv1.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

        '    fn_LoadDataOnline(sId)

        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub

#Region "Master Detail"
    'Private Sub fn_LoadDataOnline(ByVal id As String)
    '    Try
    '        Dim oConnMySql As New MySql.Data.MySqlClient.MySqlConnection
    '        Dim oCommMySql As New MySql.Data.MySqlClient.MySqlCommand
    '        Dim daMySql As MySql.Data.MySqlClient.MySqlDataAdapter
    '        Dim dsMySql As New DataSet
    '        Dim MYSQL As String

    '        dsMySql = New DataSet

    '        oConnMySql = New MySqlConnection(sConnMySql)

    '        If oConnMySql.State = ConnectionState.Closed Then
    '            oConnMySql.Open()
    '        End If

    '        MYSQL = "SELECT  "
    '        MYSQL &= "* "
    '        MYSQL &= "FROM keluarga "
    '        MYSQL &= "WHERE id = '" & id & "' "

    '        oCommMySql.Connection = oConnMySql
    '        oCommMySql.CommandText = MYSQL
    '        oCommMySql.CommandTimeout = 120
    '        oCommMySql.CommandType = CommandType.Text

    '        daMySql = New MySql.Data.MySqlClient.MySqlDataAdapter(oCommMySql)
    '        daMySql.Fill(dsMySql, "pasienidentitas")

    '        If oConnMySql.State = ConnectionState.Open Then
    '            oConnMySql.Close()
    '        End If

    '        'If dsMySql.Tables("R_HISTORYRI").Rows.Count < 1 Then

    '        'End If

    '        grd.MainView = grv
    '        grd.DataSource = dsMySql.Tables("pasienidentitas")
    '        grd.ForceInitialize()

    '        fn_LoadFormatData()
    '    Catch oErr As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
    'Private Sub fn_LoadFormatData()
    '    For iLoop As Integer = 0 To grv.Columns.Count - 1
    '        If grv.Columns(iLoop).ColumnType.Name = "Decimal" Then
    '            grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
    '            grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
    '            grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    '        ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
    '            grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
    '            grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
    '        End If
    '    Next
    'End Sub
    'Private Sub grv_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs)
    '    If grv.IsFilterRow(e.RowHandle) Then Exit Sub
    '    If CDec(grv.GetRowCellValue(e.RowHandle, "TERSEDIA")) = 0 Then
    '        'e.Appearance.BackColor = Color.YellowGreen
    '    Else
    '        e.Appearance.BackColor = Color.LightGreen
    '    End If
    'End Sub
#End Region
#End Region
#Region "Command Button"
    Private Sub frmMember_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                Me.Close()
        End Select
    End Sub
#End Region
End Class