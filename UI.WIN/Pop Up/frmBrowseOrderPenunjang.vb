Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmBrowseOrderPenunjang
    Private sCategoryBilling As Integer = 0

    Public Sub LoadMe(ByVal CategoryBilling As Integer)
        sCategoryBilling = CategoryBilling
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sFind1 = ""
        fn_LoadGrid()
        fn_LoadLanguage()
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = PaymentType.TITLE

            'grv.Columns("MEMO").Caption = PaymentType.MEMO
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Return And e.Shift = 0 Then
            cmdSelect_Click(sender, e)
        ElseIf e.KeyCode = Keys.Escape Then
            sFind1 = String.Empty
            sFind2 = String.Empty
            Me.Close()
        End If
    End Sub

    Private Sub cmdSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelect.Click
        sFind1 = grv.GetFocusedRowCellDisplayText("KDCPPT")
        'sFind2 = grv.GetFocusedRowCellDisplayText(colMEMO)
        Me.Close()
    End Sub

    Private Sub grv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grv.DoubleClick
        cmdSelect_Click(sender, e)
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub fn_LoadGrid()
        Try
            If sCategoryBilling = 0 Or sCategoryBilling = 1 Then Exit Sub

            grv.Columns.Clear()
            grd.DataSource = Nothing
            grv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

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

            If sCategoryBilling = 2 Then
                SQL = "SELECT "
                SQL &= "A.KDCPPT "
                SQL &= ",Tanggal = A.DATE "
                SQL &= ",B.nama "
                SQL &= ",B.dpjp "
                SQL &= ",B.jnsPelayanan "
                SQL &= ",Permintaan = CASE B.jnsPelayanan WHEN 'R.Jalan' THEN B.poli ELSE B.ruangan END "
                SQL &= ",A.KDUSER "
                SQL &= "From R_IDENTITAS_GROUPER_CPPT A "
                SQL &= "INNER Join R_IDENTITAS_GROUPER B "
                SQL &= "On A.kodegrouper = B.kodegrouper "
                SQL &= "INNER Join R_IDENTITAS_GROUPER_CPPT_PROSEDUR C "
                SQL &= "On A.KDCPPT = C.KDCPPT "
                SQL &= "INNER Join M_ITEM D "
                SQL &= "On C.KDITEM = D.KDITEM "
                SQL &= "INNER Join M_ITEM_L3 E "
                SQL &= "On D.KDITEM_L3 = E.KDITEM_L3 "
                SQL &= "WHERE c.ISBACA = 0 "
                SQL &= "And E.MEMO = 'LABORATORIUM' "
                SQL &= "GROUP BY "
                SQL &= "A.KDCPPT "
                SQL &= ",A.DATE"
                SQL &= ",B.nama "
                SQL &= ",B.dpjp "
                SQL &= ",A.KDUSER "
                SQL &= ",B.jnsPelayanan "
                SQL &= ",B.poli "
                SQL &= ",B.ruangan "
            ElseIf sCategoryBilling = 3 Then
                SQL = "SELECT "
                SQL &= "A.KDCPPT "
                SQL &= ",Tanggal = A.DATE "
                SQL &= ",B.nama "
                SQL &= ",B.dpjp "
                SQL &= ",B.jnsPelayanan "
                SQL &= ",Permintaan = CASE B.jnsPelayanan WHEN 'R.Jalan' THEN B.poli ELSE B.ruangan END "
                SQL &= ",A.KDUSER "
                SQL &= "From R_IDENTITAS_GROUPER_CPPT A "
                SQL &= "INNER Join R_IDENTITAS_GROUPER B "
                SQL &= "On A.kodegrouper = B.kodegrouper "
                SQL &= "INNER Join R_IDENTITAS_GROUPER_CPPT_PROSEDUR C "
                SQL &= "On A.KDCPPT = C.KDCPPT "
                SQL &= "INNER Join M_ITEM D "
                SQL &= "On C.KDITEM = D.KDITEM "
                SQL &= "INNER Join M_ITEM_L3 E "
                SQL &= "On D.KDITEM_L3 = E.KDITEM_L3 "
                SQL &= "WHERE c.ISBACA = 0 "
                SQL &= "And E.MEMO = 'RADIOLOGI' "
                SQL &= "GROUP BY "
                SQL &= "A.KDCPPT "
                SQL &= ",A.DATE"
                SQL &= ",B.nama "
                SQL &= ",B.dpjp "
                SQL &= ",A.KDUSER "
                SQL &= ",B.jnsPelayanan "
                SQL &= ",B.poli "
                SQL &= ",B.ruangan "
            Else
                SQL = "SELECT "
                SQL &= "Kategori = 'Non Racikan' "
                SQL &= ",Tanggal = A.DATE "
                SQL &= ",A.KDCPPT "
                SQL &= ",B.nama "
                SQL &= ",B.dpjp "
                SQL &= ",B.jnsPelayanan "
                SQL &= ",Permintaan = CASE B.jnsPelayanan WHEN 'R.Jalan' THEN B.poli ELSE B.ruangan END "
                SQL &= ",A.KDUSER "
                SQL &= "From R_IDENTITAS_GROUPER_CPPT A "
                SQL &= "INNER Join R_IDENTITAS_GROUPER B "
                SQL &= "On A.kodegrouper = B.kodegrouper "
                SQL &= "INNER Join R_IDENTITAS_GROUPER_CPPT_NONRACIKAN C "
                SQL &= "On A.KDCPPT = C.KDCPPT "
                SQL &= "INNER Join M_ITEM D "
                SQL &= "On C.KDITEM = D.KDITEM "
                SQL &= "INNER Join M_ITEM_L3 E "
                SQL &= "On D.KDITEM_L3 = E.KDITEM_L3 "
                SQL &= "WHERE c.ISBACA = 0 "
                SQL &= "GROUP BY "
                SQL &= "A.KDCPPT "
                SQL &= ",A.DATE"
                SQL &= ",B.nama "
                SQL &= ",B.dpjp "
                SQL &= ",A.KDUSER "
                SQL &= ",B.jnsPelayanan "
                SQL &= ",B.poli "
                SQL &= ",B.ruangan "

                SQL &= "UNION "

                SQL &= "SELECT "
                SQL &= "Kategori = 'Racikan' "
                SQL &= ",Tanggal = A.DATE "
                SQL &= ",A.KDCPPT "
                SQL &= ",B.nama "
                SQL &= ",B.dpjp "
                SQL &= ",B.jnsPelayanan "
                SQL &= ",Permintaan = CASE B.jnsPelayanan WHEN 'R.Jalan' THEN B.poli ELSE B.ruangan END "
                SQL &= ",A.KDUSER "
                SQL &= "From R_IDENTITAS_GROUPER_CPPT A "
                SQL &= "INNER Join R_IDENTITAS_GROUPER B "
                SQL &= "On A.kodegrouper = B.kodegrouper "
                SQL &= "INNER Join R_IDENTITAS_GROUPER_CPPT_RACIKAN C "
                SQL &= "On A.KDCPPT = C.KDCPPT "
                SQL &= "INNER Join M_ITEM D "
                SQL &= "On C.KDITEM = D.KDITEM "
                SQL &= "INNER Join M_ITEM_L3 E "
                SQL &= "On D.KDITEM_L3 = E.KDITEM_L3 "
                SQL &= "WHERE c.ISBACA = 0 "
                SQL &= "GROUP BY "
                SQL &= "A.KDCPPT "
                SQL &= ",A.DATE"
                SQL &= ",B.nama "
                SQL &= ",B.dpjp "
                SQL &= ",A.KDUSER "
                SQL &= ",B.jnsPelayanan "
                SQL &= ",B.poli "
                SQL &= ",B.ruangan "
            End If


            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "R_ORDER")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grd.MainView = grv
            grd.DataSource = ds.Tables("R_ORDER")
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
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm:ss}"
            End If
        Next
        grv.BestFitColumns()
    End Sub
End Class