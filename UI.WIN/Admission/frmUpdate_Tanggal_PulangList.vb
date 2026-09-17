Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmUpdate_Tanggal_PulangList
    Implements ILanguage

    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oUpdate_Tanggal_Pulang As New Admission.clsUpdate_Tanggal_Pulang

#Region "Function"
    Private Sub Me_Load() Handles Me.Load
        Me.Text = Update_Tanggal_Pulang.TITLE

        deDATEFrom.DateTime = Now.AddDays((-Now.Day) + 1)
        deDATETo.DateTime = Now

        fn_LoadSecurity()

        Try
            grv.RestoreLayoutFromRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            grv.SaveLayoutToRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub fn_LoadSecurity()
        Try
            Dim oOtority As New Setting.clsOtority
            Dim oUser As New Setting.clsUser

            Dim ds = (From x In oOtority.GetDataDetail
                      Join y In oUser.GetData
                      On x.KDOTORITY Equals y.KDOTORITY
                      Where x.MODUL = "UPDATE_TANGGAL_PULANG" _
                      And y.KDUSER = sUserID
                      Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                picAdd.Enabled = ds.ISADD
                picDelete.Enabled = ds.ISDELETE
                picUpdate.Enabled = ds.ISUPDATE
                picPrint.Enabled = ds.ISPRINT
                picRefresh.Enabled = ds.ISVIEW

                If ds.ISVIEW = True Then
                    fn_LoadData()
                    fn_LoadLanguage()
                End If
            Catch oErr As Exception
                MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)

                picAdd.Enabled = False
                picDelete.Enabled = False
                picUpdate.Enabled = False
                picPrint.Enabled = False
                picRefresh.Enabled = False
            End Try
        Catch oErr As Exception
            MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            Me.Text = Update_Tanggal_Pulang.TITLE

            grv.Columns("DATE").Caption = Update_Tanggal_Pulang.TANGGAL
            grv.Columns("KDUPDATE_TANGGAL_PULANG").Caption = Update_Tanggal_Pulang.KDUPDATE_TANGGAL_PULANG
            grv.Columns("NOMORSEP").Caption = Update_Tanggal_Pulang.NOMORSEP
            grv.Columns("KDPENDAFTARAN").Caption = Update_Tanggal_Pulang.KDPENDAFTARAN
            grv.Columns("KDCUSTOMER").Caption = Customer.KDCUSTOMER
            grv.Columns("NAME_DISPLAY").Caption = Customer.NAME_DISPLAY
            grv.Columns("KDUSER").Caption = User.KDUSER

            ColumnChooserToolStripMenuItem.Text = Caption.ColumnChooser
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData()
        'Try
        '    Dim ds = From x In oUpdate_Tanggal_Pulang.GetData
        '             Select x.KDUPDATE_TANGGAL_PULANG, x.KDPENDAFTARAN, x.S_PENDAFTARAN_H.KDCUSTOMER, x.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY, x.DATE, x.NOMORSEP, x.KDUSER

        '    grd.DataSource = ds.ToList

        '    fn_LoadFormatData()

        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
        Try
            grv.Columns.Clear()
            grd.DataSource = Nothing

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
            SQL &= "A.KDUPDATE_TANGGAL_PULANG "
            SQL &= ",JENISDAFTAR = E.MEMO "
            SQL &= ",A.KDPENDAFTARAN "
            SQL &= ",B.KDCUSTOMER "
            SQL &= ",C.NAME_DISPLAY "
            SQL &= ",A.DATE "
            SQL &= ",B.NOMORSEP "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "T_UPDATE_TANGGAL_PULANG A "
            SQL &= "INNER JOIN S_PENDAFTARAN_H B "
            SQL &= "ON A.KDPENDAFTARAN = B.KDPENDAFTARAN "
            SQL &= "INNER JOIN M_CUSTOMER C "
            SQL &= "ON B.KDCUSTOMER = C.KDCUSTOMER "
            SQL &= "INNER JOIN M_DEPARTMENT D "
            SQL &= "ON B.KDDEPARTMENT = D.KDDEPARTMENT "
            SQL &= "INNER JOIN M_DAFTAR_L1 E "
            SQL &= "ON B.KDDAFTAR_L1 = E.KDDAFTAR_L1 "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "ORDER BY A.DATE DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

            grd.MainView = grv
            grd.DataSource = ds.Tables("ALL")
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

        grv.Columns("KDUPDATE_TANGGAL_PULANG").Visible = False
        'grv.Columns("ISDEFAULT").Visible = False
        'grv.Columns("ISACTIVE").Visible = False

        grv.Columns("KDUPDATE_TANGGAL_PULANG").OptionsColumn.ShowInCustomizationForm = False
    End Sub
    Private Sub ColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ColumnChooserToolStripMenuItem.Click
        grv.ShowCustomization()
    End Sub
    Private Function fn_DeleteData(ByVal Update_Tanggal_PulangUpdate_Tanggal_Pulang As String) As Boolean
        Try
            oUpdate_Tanggal_Pulang.DeleteData(Update_Tanggal_PulangUpdate_Tanggal_Pulang)
            fn_DeleteData = True
        Catch oErr As Exception
            fn_DeleteData = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
#End Region
#Region "Command Button"
    Private Sub frmMember_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.A
                If e.Alt = True And picAdd.Enabled = True Then
                    picAdd_Click()
                End If
            Case Keys.E
                If e.Alt = True And picUpdate.Enabled = True Then
                    picUpdate_Click()
                End If
            Case Keys.D
                If e.Alt = True And picDelete.Enabled = True Then
                    picDelete_Click()
                End If
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
    Private Sub grv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grv.DoubleClick
        If grv.GetFocusedRowCellValue("KDUPDATE_TANGGAL_PULANG") Is Nothing Then
            Exit Sub
        End If

        Dim frmUpdate_Tanggal_Pulang As New frmUpdate_Tanggal_Pulang
        Try
            frmUpdate_Tanggal_Pulang.LoadMe(FORM_MODE.FORM_MODE_VIEW, grv.GetFocusedRowCellValue("KDUPDATE_TANGGAL_PULANG"))
            frmUpdate_Tanggal_Pulang.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAdd_Click() Handles picAdd.Click
        Dim frmUpdate_Tanggal_Pulang As New frmUpdate_Tanggal_Pulang
        Try
            frmUpdate_Tanggal_Pulang.LoadMe(FORM_MODE.FORM_MODE_ADD)
            frmUpdate_Tanggal_Pulang.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmUpdate_Tanggal_Pulang Is Nothing Then frmUpdate_Tanggal_Pulang.Dispose()
            frmUpdate_Tanggal_Pulang = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("MEMO"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KDUPDATE_TANGGAL_PULANG") Is Nothing Then
            Exit Sub
        End If
        Dim frmUpdate_Tanggal_Pulang As New frmUpdate_Tanggal_Pulang
        Try
            frmUpdate_Tanggal_Pulang.LoadMe(FORM_MODE.FORM_MODE_EDIT, grv.GetFocusedRowCellValue("KDUPDATE_TANGGAL_PULANG"))
            frmUpdate_Tanggal_Pulang.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmUpdate_Tanggal_Pulang Is Nothing Then frmUpdate_Tanggal_Pulang.Dispose()
            frmUpdate_Tanggal_Pulang = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("MEMO"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KDUPDATE_TANGGAL_PULANG") Is Nothing Then
            Exit Sub
        End If
        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_DeleteData(grv.GetFocusedRowCellValue("KDUPDATE_TANGGAL_PULANG")) = False Then
            MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        fn_LoadSecurity()
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        'Try
        '    Dim xtraUpdate_Tanggal_Pulang As New xtraUpdate_Tanggal_Pulang

        '    Dim Update_Tanggal_PulangUpdate_Tanggal_Pulang As New List(Of String)
        '    For i As Integer = 0 To grv.RowCount - 1
        '        Update_Tanggal_PulangUpdate_Tanggal_Pulang.Add(grv.GetRowCellValue(i, "KDUPDATE_TANGGAL_PULANG"))
        '    Next

        '    Dim ds = oUpdate_Tanggal_Pulang.GetData.Where(Function(x) Update_Tanggal_PulangUpdate_Tanggal_Pulang.Contains(x.KDUpdate_Tanggal_Pulang)).ToList

        '    xtraUpdate_Tanggal_Pulang.bindingSource.DataSource = ds
        '    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(xtraUpdate_Tanggal_Pulang)
        '    printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_LoadSecurity()
    End Sub
#End Region
End Class