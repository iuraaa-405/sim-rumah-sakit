Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports DevExpress.XtraPrinting
Imports System.Data
Imports System.Data.SqlClient

Public Class frmReportCustomer
    Implements ILanguage

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "Master Pasien"
    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        'Try
        '    grv.SaveLayoutToRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text & "\" & cboTYPE.Text)
        'Catch oErr As Exception

        'End Try
    End Sub
    Public Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            Me.Text = Customer.TITLE

            grv.Columns("KDCUSTOMER").Caption = Customer.KDCUSTOMER
            grv.Columns("NAME_DISPLAY").Caption = Customer.NAME_DISPLAY
            grv.Columns("KDCOA").Caption = Customer.KDCOA
            grv.Columns("EMAIL").Caption = Customer.EMAIL
            grv.Columns("PHONE").Caption = Customer.PHONE
            grv.Columns("MOBILE").Caption = Customer.MOBILE
            grv.Columns("FAX").Caption = Customer.FAX
            grv.Columns("OTHER").Caption = Customer.OTHER
            grv.Columns("WEBSITE").Caption = Customer.WEBSITE
            'grv.Columns("BILL_STREET").Caption = Customer.BILL_STREET
            'grv.Columns("BILL_CITY").Caption = Customer.BILL_CITY
            'grv.Columns("BILL_STATE").Caption = Customer.BILL_STATE
            'grv.Columns("BILL_ZIP").Caption = Customer.BILL_ZIP
            'grv.Columns("BILL_COUNTRY").Caption = Customer.BILL_COUNTRY
            grv.Columns("MEMO").Caption = Customer.MEMO
            grv.Columns("ISACTIVE").Caption = Customer.ISACTIVE

            'ColumnChooserToolStripMenuItem.Text = Caption.ColumnChooser
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
                      Where x.MODUL = "CUSTOMER" _
                     And y.KDUSER = sUserID
                      Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                picRefresh.Enabled = ds.ISVIEW

                If ds.ISVIEW = True Then
                    fn_Preview()
                    fn_LoadLanguage()
                End If
            Catch oErr As Exception
                MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)

                picRefresh.Enabled = False
            End Try
        Catch oErr As Exception
            MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Preview()
        Try
            grv.Columns.Clear()
            grd.DataSource = Nothing
            grv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            grv1.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            fn_LoadData()

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

#Region "Master Detail"
    Private Sub fn_LoadData()
        Try
            Dim tes = CInt(txtInt.Text)
        Catch oErr As Exception
            MsgBox("Silahkan Masukan angka", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End Try
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
            SQL &= "TOP " & txtInt.Text & " "
            SQL &= "A.KDCUSTOMER "
            SQL &= ",NORMRLAMA = A.KDCUSTOMER_LAMA"
            SQL &= ",NONIK = A.KTP "
            SQL &= ",NOKARTUBPJS = A.KARTUBPJS "
            SQL &= ",A.TANGGALLAHIR "
            SQL &= ",A.NAME_DISPLAY "
            SQL &= ",A.ALAMAT "
            SQL &= ",A.KDCOA "
            SQL &= ",A.EMAIL  "
            SQL &= ",A.PHONE "
            SQL &= ",A.MOBILE "
            SQL &= ",A.FAX "
            SQL &= ",A.OTHER "
            SQL &= ",A.WEBSITE "
            SQL &= ",A.MEMO "
            SQL &= ",A.ISACTIVE "
            SQL &= "FROM "
            SQL &= "M_CUSTOMER A "
            SQL &= "ORDER BY A.DATECREATED DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "HISTORY")

            grd.MainView = grv
            grd.DataSource = ds.Tables("HISTORY")
            grd.ForceInitialize()

            fn_LoadFormatData()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
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
            Case Keys.R
                If e.Alt = True And picRefresh.Enabled = True Then
                    picRefresh_Click()
                End If
        End Select
    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_LoadSecurity()
    End Sub
#End Region
End Class