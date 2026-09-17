Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmSalesOrderTransaksiPCRList
    Implements ILanguage

    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oSalesOrderTransaksi As New Sales.clsSalesOrderTransaksi

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "Hasil - List"
        deDATEFrom.DateTime = Now
        deDATETo.DateTime = Now

        fn_LoadSecurity()

    End Sub
    Private Sub fn_LoadSecurity()
        Try
            Dim oOtority As New Setting.clsOtority
            Dim oUser As New Setting.clsUser

            Dim ds = (From x In oOtority.GetDataDetail
                      Join y In oUser.GetData
                      On x.KDOTORITY Equals y.KDOTORITY
                      Where x.MODUL = "SOPCR" _
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
            Me.Text = "Hasil - List"

            fn_LoadLanguageMaster()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Sub fn_LoadLanguageMaster()
        Try
            grv.Columns("KDSOTRANSAKSI").Caption = SalesOrderTransaksi.KDSOTRANSAKSI
            grv.Columns("TANGGAL").Caption = SalesOrderTransaksi.TANGGAL
            grv.Columns("KDPENDAFTARAN").Caption = SalesOrderTransaksi.KDPENDAFTARAN
            grv.Columns("KDUSER").Caption = Caption.User
        Catch oErr As Exception

        End Try
    End Sub
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
            SQL &= "D.KDSOTRANSAKSI "
            SQL &= ",E.KDITEM "
            SQL &= ",TANGGAL = D.DATE "
            SQL &= ",KDPENDAFTARAN = C.NAME_DISPLAY "
            SQL &= ",Item = F.NMITEM2 "
            SQL &= ",Bayar = (SELECT CASE D.PAYAMOUNT WHEN 0 THEN CONVERT(BIT, 0) ELSE CONVERT(BIT, 1) END) "
            SQL &= ",Hasil = ISNULL((SELECT HASIL FROM S_SO_TRANSAKSI_D_PCR WHERE D.KDSOTRANSAKSI = KDSOTRANSAKSI AND E.KDITEM = KDITEM) , 'BELUM') "
            SQL &= ",D.KDUSER "
            SQL &= ",KELOMPOK = F.NMITEM1 "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_H A "
            SQL &= "INNER JOIN S_PENDAFTARAN_KUNJUNGAN B "
            SQL &= "ON A.KDPENDAFTARAN = B.KDPENDAFTARAN "
            SQL &= "INNER JOIN M_CUSTOMER C "
            SQL &= "ON A.KDCUSTOMER = C.KDCUSTOMER "
            SQL &= "INNER JOIN S_SO_TRANSAKSI_H D "
            SQL &= "ON B.KDKUNJUNGAN = D.KDKUNJUNGAN "
            SQL &= "INNER JOIN S_SO_TRANSAKSI_D E "
            SQL &= "ON D.KDSOTRANSAKSI = E.KDSOTRANSAKSI "
            SQL &= "INNER JOIN M_ITEM F "
            SQL &= "ON E.KDITEM = F.KDITEM "
            SQL &= "INNER JOIN M_ITEM_L3 G "
            SQL &= "ON F.KDITEM_L3 = G.KDITEM_L3 "
            SQL &= "WHERE CONVERT(VARCHAR(8), D.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), D.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND G.MEMO = 'LABORATORIUM' "
            SQL &= "ORDER BY B.DATE DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "PCR")

            grd.MainView = grv
            grd.DataSource = ds.Tables("PCR")
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

        grv.Columns("KDSOTRANSAKSI").VisibleIndex = -1
        grv.Columns("KDITEM").VisibleIndex = -1

    End Sub
    Private Sub MasterColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MasterColumnChooserToolStripMenuItem.Click
        grv.ShowCustomization()
    End Sub
    Private Function fn_DeleteData(ByVal sKDSOTANPARESEP As String, ByVal sKDITEM As String) As Boolean
        Try
            Dim dsTotal = oSalesOrderTransaksi.GetData(sKDSOTANPARESEP)
            Dim Total As String = "0"

            If dsTotal IsNot Nothing Then
                Total = dsTotal.GRANDTOTAL
            End If

            Dim oDelete As New Setting.clsDelete
            Dim frmPesanDelete As New frmPesanDelete
            frmPesanDelete.ShowDialog(Me)

            If sPesanHapus <> "XXXXXBATALXXXXX" Then
                oSalesOrderTransaksi.DeleteDataPCR(sKDSOTANPARESEP, sKDITEM)

                If oDelete.InsertData("BILLINGPARTIK", sUserID, sPesanHapus, sKDSOTANPARESEP & " : " & Total) = False Then
                    'fn_DeleteData = False
                End If

                fn_DeleteData = True

            Else
                fn_DeleteData = False
            End If

        Catch oErr As Exception
            fn_DeleteData = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub grv_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grv.RowStyle
        If grv.IsFilterRow(e.RowHandle) Then Exit Sub
        If CBool(grv.GetRowCellValue(e.RowHandle, "Bayar")) = False Then
            e.Appearance.BackColor = Color.Red
        Else
            e.Appearance.BackColor = Color.LightGreen
        End If
    End Sub
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
    Private Sub grv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        If grv.GetFocusedRowCellValue("KDSOTRANSAKSI") Is Nothing Then
            Exit Sub
        End If

        Dim frmSalesOrderTransaksiPCR As New frmSalesOrderTransaksiPCR
        Try
            frmSalesOrderTransaksiPCR.LoadMe(FORM_MODE.FORM_MODE_VIEW, grv.GetFocusedRowCellValue("KDSOTRANSAKSI"), grv.GetFocusedRowCellValue("KDITEM"))
            frmSalesOrderTransaksiPCR.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAdd_Click() Handles picAdd.Click
        If grv.GetFocusedRowCellValue("KDSOTRANSAKSI") Is Nothing Then
            Exit Sub
        End If

        If grv.GetFocusedRowCellValue("KELOMPOK") = "SWAB" Then
            Dim frmSalesOrderTransaksiPCR As New frmSalesOrderTransaksiPCR
            Try
                frmSalesOrderTransaksiPCR.LoadMe(FORM_MODE.FORM_MODE_ADD, grv.GetFocusedRowCellValue("KDSOTRANSAKSI"), grv.GetFocusedRowCellValue("KDITEM"))
                frmSalesOrderTransaksiPCR.ShowDialog(Me)
                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSalesOrderTransaksiPCR Is Nothing Then frmSalesOrderTransaksiPCR.Dispose()
                frmSalesOrderTransaksiPCR = Nothing

                Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDSOTRANSAKSI"), sCode)
                If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

                If sStatusSave = "NEW" Then
                    sStatusSave = "NONE"
                    picAdd_Click()
                End If
            End Try
        Else

        End If
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KDSOTRANSAKSI") Is Nothing Then
            Exit Sub
        End If

        Dim ds = oSalesOrderTransaksi.GetDataPCR(grv.GetFocusedRowCellValue("KDSOTRANSAKSI"), grv.GetFocusedRowCellValue("KDITEM"))
        If ds Is Nothing Then
            MsgBox("Belum input hasil, silahkan klik Add", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If grv.GetFocusedRowCellValue("KELOMPOK") = "SWAB" Then
            Dim frmSalesOrderTransaksiPCR As New frmSalesOrderTransaksiPCR
            Try
                frmSalesOrderTransaksiPCR.LoadMe(FORM_MODE.FORM_MODE_EDIT, grv.GetFocusedRowCellValue("KDSOTRANSAKSI"), grv.GetFocusedRowCellValue("KDITEM"))
                frmSalesOrderTransaksiPCR.ShowDialog(Me)
                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSalesOrderTransaksiPCR Is Nothing Then frmSalesOrderTransaksiPCR.Dispose()
                frmSalesOrderTransaksiPCR = Nothing

                Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDSOTRANSAKSI"), sCode)
                If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

                If sStatusSave = "NEW" Then
                    sStatusSave = "NONE"
                    picAdd_Click()
                End If
            End Try
        Else

        End If

    End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KDSOTRANSAKSI") Is Nothing Then
            Exit Sub
        End If
        Dim ds = oSalesOrderTransaksi.GetDataPCR(grv.GetFocusedRowCellValue("KDSOTRANSAKSI"), grv.GetFocusedRowCellValue("KDITEM"))
        If ds Is Nothing Then
            MsgBox("Belum input hasil, silahkan klik Add", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_DeleteData(grv.GetFocusedRowCellValue("KDSOTRANSAKSI"), grv.GetFocusedRowCellValue("KDITEM")) = False Then
            MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        fn_LoadSecurity()
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        Try
            If grv.GetFocusedRowCellValue("KDSOTRANSAKSI") = String.Empty Then Exit Sub

            Dim ds = oSalesOrderTransaksi.GetDataPCR(grv.GetFocusedRowCellValue("KDSOTRANSAKSI"), grv.GetFocusedRowCellValue("KDITEM"))
            If ds Is Nothing Then
                MsgBox("Belum input hasil, silahkan klik Add", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            Else
                If grv.GetFocusedRowCellValue("KELOMPOK") = "SWAB" Then
                    Dim rpt As New xtraPCR

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK
                    rpt.bindingSource.DataSource = ds
                    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                    printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
                Else

                End If
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_LoadSecurity()
    End Sub
#End Region
End Class