Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmSalesOrderTanpaResepList
    Implements ILanguage

    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oSalesOrderTanpaResep As New Sales.clsSalesOrderTanpaResep
    Private sparamater As Boolean = False

#Region "Function"
    Public Sub fnVisiblePembayaran(ByVal parameter As Boolean)
        sparamater = parameter
    End Sub
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "Penjualan - List"
        deDATEFrom.DateTime = Now
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
            picPembayaran.Visible = sparamater
            LabelControl2.Visible = sparamater

            Dim oOtority As New Setting.clsOtority
            Dim oUser As New Setting.clsUser

            Dim ds = (From x In oOtority.GetDataDetail
                      Join y In oUser.GetData
                      On x.KDOTORITY Equals y.KDOTORITY
                      Where x.MODUL = "SOTANPARESEP" _
                      And y.KDUSER = sUserID
                      Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Dim dsP = (From x In oOtority.GetDataDetail
                       Join y In oUser.GetData
                      On x.KDOTORITY Equals y.KDOTORITY
                       Where x.MODUL = "SOTANPARESEPPEMBAYARAN" _
                      And y.KDUSER = sUserID
                       Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                picPembayaran.Enabled = ds.ISADD
            Catch ex As Exception
                picPembayaran.Enabled = False
            End Try

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
            Me.Text = "Penjualan - List"

            fn_LoadLanguageMaster()
            fn_LoadLanguageDetail()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Sub fn_LoadLanguageMaster()
        Try
            grv.Columns("KDSOTANPARESEP").Caption = SalesOrderTanpaResep.KDSOTANPARESEP
            grv.Columns("TANGGAL").Caption = SalesOrderTanpaResep.TANGGAL
            grv.Columns("KDPENDAFTARAN").Caption = SalesOrderTanpaResep.KDPENDAFTARAN
            grv.Columns("SUBTOTAL").Caption = SalesOrderTanpaResep.SUBTOTAL
            grv.Columns("DISCOUNT").Caption = SalesOrderTanpaResep.DISCOUNT
            grv.Columns("GRANDTOTAL").Caption = SalesOrderTanpaResep.GRANDTOTAL
            grv.Columns("MEMO").Caption = SalesOrderTanpaResep.MEMO
            grv.Columns("KDUSER").Caption = Caption.User
        Catch oErr As Exception

        End Try
    End Sub
    Public Sub fn_LoadLanguageDetail()
        Try
            grv1.Columns("M_ITEM.NMITEM1").Caption = Item.NMITEM1_2
            grv1.Columns("M_ITEM.NMITEM2").Caption = Item.NMITEM2_2
            grv1.Columns("M_ITEM.NMITEM3").Caption = Item.NMITEM3_2
            grv1.Columns("M_UOM.MEMO").Caption = "Satuan"

            grv1.Columns("QTY").Caption = SalesOrderTanpaResep.DETAIL_QTY
            grv1.Columns("PRICE").Caption = SalesOrderTanpaResep.DETAIL_PRICE
            grv1.Columns("SUBTOTAL").Caption = SalesOrderTanpaResep.DETAIL_SUBTOTAL
            grv1.Columns("DISCOUNT").Caption = SalesOrderTanpaResep.DETAIL_DISCOUNT
            grv1.Columns("GRANDTOTAL").Caption = SalesOrderTanpaResep.DETAIL_GRANDTOTAL

            grv1.Columns("REMARKS").Caption = SalesOrderTanpaResep.DETAIL_REMARKS

        Catch oErr As Exception

        End Try
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = From x In oSalesOrderTanpaResep.GetDataByCategoryDate(deDATEFrom.DateTime, deDATETo.DateTime)
                     Select x.KDSOTANPARESEP, TANGGAL = x.DATE, KDPENDAFTARAN = x.NAMAPASIEN, x.SUBTOTAL, x.DISCOUNT, x.GRANDTOTAL, x.MEMO, Details = x.S_SO_TANPARESEP_Ds, Bayar = IIf(x.PAYAMOUNT = x.GRANDTOTAL, CBool(True), CBool(False)), x.KDUSER

            grd.DataSource = ds.ToList

            fn_LoadFormatData()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grv_MasterRowExpanded(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventArgs) Handles grv.MasterRowExpanded
        grv1 = TryCast(grv.GetDetailView(e.RowHandle, e.RelationIndex), DevExpress.XtraGrid.Views.Grid.GridView)

        fn_LoadFormatDataDetail()
        fn_LoadLanguageDetail()
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

        grv.Columns("Bayar").VisibleIndex = -1

    End Sub
    Private Sub fn_LoadFormatDataDetail()
        For iLoop As Integer = 0 To grv1.Columns.Count - 1
            If grv1.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grv1.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv1.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv1.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grv1.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv1.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv1.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

        grv1.Columns().AddVisible("M_ITEM.NMITEM1")
        grv1.Columns().AddVisible("M_ITEM.NMITEM2")
        grv1.Columns().AddVisible("M_ITEM.NMITEM3")
        grv1.Columns().AddVisible("M_UOM.MEMO")

        grv1.Columns("DATECREATED").Visible = False
        grv1.Columns("DATEUPDATED").Visible = False
        grv1.Columns("SEQ").Visible = False
        grv1.Columns("KDSOTANPARESEP").Visible = False
        grv1.Columns("M_ITEM").Visible = False
        grv1.Columns("M_UOM").Visible = False
        grv1.Columns("S_SO_TANPARESEP_H").Visible = False
        grv1.Columns("KDITEM").Visible = False
        grv1.Columns("KDUOM").Visible = False
        grv1.Columns("ISRACIK").Visible = False
        grv1.Columns("KDSIGNA").Visible = False
        grv1.Columns("M_SIGNA").Visible = False

        grv1.Columns("DATECREATED").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("DATEUPDATED").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("SEQ").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("KDSOTANPARESEP").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("M_ITEM").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("M_UOM").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("S_SO_TANPARESEP_H").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("KDITEM").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("KDUOM").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("ISRACIK").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("KDSIGNA").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("M_SIGNA").OptionsColumn.ShowInCustomizationForm = False

        grv1.Columns("M_ITEM.NMITEM1").VisibleIndex = -1
        grv1.Columns("M_ITEM.NMITEM3").VisibleIndex = -1

        grv1.Columns("M_ITEM.NMITEM2").VisibleIndex = 0

    End Sub
    Private Sub MasterColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MasterColumnChooserToolStripMenuItem.Click
        grv.ShowCustomization()
    End Sub
    Private Sub DetailColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DetailColumnChooserToolStripMenuItem.Click
        grv1.ShowCustomization()
    End Sub
    Private Function fn_DeleteData(ByVal sKDSOTANPARESEP As String) As Boolean
        Try
            Dim dsTotal = oSalesOrderTanpaResep.GetData(sKDSOTANPARESEP)
            Dim Total As String = "0"

            If dsTotal IsNot Nothing Then
                Total = dsTotal.GRANDTOTAL
            End If

            Dim oDelete As New Setting.clsDelete
            Dim frmPesanDelete As New frmPesanDelete
            frmPesanDelete.ShowDialog(Me)

            If sPesanHapus <> "XXXXXBATALXXXXX" Then
                oSalesOrderTanpaResep.DeleteData(sKDSOTANPARESEP)

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
        If grv.GetFocusedRowCellValue("KDSOTANPARESEP") Is Nothing Then
            Exit Sub
        End If

        Dim frmSalesOrderTanpaResep As New frmSalesOrderTanpaResep
        Try
            frmSalesOrderTanpaResep.LoadMe(FORM_MODE.FORM_MODE_VIEW, grv.GetFocusedRowCellValue("KDSOTANPARESEP"))
            frmSalesOrderTanpaResep.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAdd_Click() Handles picAdd.Click
        Dim frmSalesOrderTanpaResep As New frmSalesOrderTanpaResep
        Try
            frmSalesOrderTanpaResep.LoadMe(FORM_MODE.FORM_MODE_ADD)
            frmSalesOrderTanpaResep.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmSalesOrderTanpaResep Is Nothing Then frmSalesOrderTanpaResep.Dispose()
            frmSalesOrderTanpaResep = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDSOTANPARESEP"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KDSOTANPARESEP") Is Nothing Then
            Exit Sub
        End If
        If CBool(grv.GetFocusedRowCellValue("Bayar")) = True Then
            MsgBox("Sudah Bayar", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim ds = oSalesOrderTanpaResep.GetData(grv.GetFocusedRowCellValue("KDSOTANPARESEP"))
        If ds IsNot Nothing Then
            If ds.PAYAMOUNT > 0 Then
                MsgBox("Sudah Bayar", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
        End If

        Dim frmSalesOrderTanpaResep As New frmSalesOrderTanpaResep
        Try
            frmSalesOrderTanpaResep.LoadMe(FORM_MODE.FORM_MODE_EDIT, grv.GetFocusedRowCellValue("KDSOTANPARESEP"))
            frmSalesOrderTanpaResep.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmSalesOrderTanpaResep Is Nothing Then frmSalesOrderTanpaResep.Dispose()
            frmSalesOrderTanpaResep = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDSOTANPARESEP"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KDSOTANPARESEP") Is Nothing Then
            Exit Sub
        End If
        If CBool(grv.GetFocusedRowCellValue("Bayar")) = True Then
            MsgBox("Sudah Bayar", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_DeleteData(grv.GetFocusedRowCellValue("KDSOTANPARESEP")) = False Then
            MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        fn_LoadSecurity()

    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        'Try
        If grv.GetFocusedRowCellValue("KDSOTANPARESEP") = String.Empty Then Exit Sub

        '    Dim rpt As New xtraSalesOrder

        '    rpt.ShowPrintMarginsWarning = False
        '    rpt.Watermark.Text = sWATERMARK
        '    Dim ds = oSalesOrderTanpaResep.GetData(grv.GetFocusedRowCellValue("KDSOTANPARESEP"))
        '    rpt.bindingSource.DataSource = ds
        '    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
        '    printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
        Try

            Dim oPendaftaran As New Admission.clsPendaftaran
            Dim oPulang As New Admission.clsUpdate_Tanggal_Pulang
            Dim oCashin As New Finance.clsCashIn

            Dim dsCashin = oSalesOrderTanpaResep.GetData(grv.GetFocusedRowCellValue("KDSOTANPARESEP"))

            sPrintGrandTotal = dsCashin.GRANDTOTAL


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

            SQL = " SELECT  "
            SQL &= " CATEGORY = 0 "
            SQL &= " ,A.KDSOTANPARESEP "
            SQL &= " ,KDPENDAFTARAN = '-' "
            SQL &= " ,TUJUAN = 'APOTEK' "
            SQL &= " ,DPJP = '-' "
            SQL &= " ,KDCUSTOMER = '' "
            SQL &= " ,PASIEN = A.NAMAPASIEN  "
            SQL &= " ,ALAMAT = A.NOMORTELEPON "
            SQL &= " ,KELAS = '-'  "
            SQL &= " ,TANGGAL_DATANG = A.DATE "
            SQL &= " ,TANGGAL_PULANG = DATE "
            SQL &= " ,A.SUBTOTAL "
            SQL &= " ,COSTSHARE = 0 "
            SQL &= " ,DEPOSIT = 0 "
            SQL &= " ,A.DISCOUNT "
            SQL &= " ,A.TAX "
            SQL &= " ,A.GRANDTOTAL "
            SQL &= " ,ITEM_GROUP = F.MEMO "
            SQL &= " ,ITEM = E.NMITEM2 "
            SQL &= " ,QTY = SUM(B.QTY) "
            SQL &= " ,GRANDTOTAL_DETAIL = SUM(B.GRANDTOTAL)  "
            SQL &= " FROM S_SO_TANPARESEP_H A  "
            SQL &= " INNER JOIN S_SO_TANPARESEP_D B "
            SQL &= " ON A.KDSOTANPARESEP = B.KDSOTANPARESEP "
            SQL &= " INNER JOIN M_ITEM E "
            SQL &= " ON B.KDITEM = E.KDITEM "
            SQL &= " INNER JOIN M_ITEM_L2 F "
            SQL &= " ON E.KDITEM_L2 = F.KDITEM_L2  "
            SQL &= " WHERE "
            SQL &= " A.KDSOTANPARESEP = '" & grv.GetFocusedRowCellValue("KDSOTANPARESEP") & "' "
            SQL &= "GROUP BY "
            SQL &= " A.KDSOTANPARESEP "
            SQL &= " ,A.SUBTOTAL "
            SQL &= " ,A.DISCOUNT "
            SQL &= " ,A.TAX "
            SQL &= " ,A.GRANDTOTAL "
            SQL &= " ,F.MEMO "
            SQL &= " ,E.NMITEM2 "
            SQL &= " ,A.NAMAPASIEN  "
            SQL &= " ,A.NOMORTELEPON "
            SQL &= " ,A.DATE "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

            Dim listTranskasi As New List(Of R_CASHIN)

            For iLoop As Integer = 0 To ds.Tables("ALL").Rows.Count - 1
                Dim dsRekap As New DataAccess.R_CASHIN

                With ds.Tables("ALL")
                    dsRekap.KDCASHIN = .Rows(iLoop)("KDSOTANPARESEP")
                    dsRekap.CATEGORY = .Rows(iLoop)("CATEGORY")
                    dsRekap.KDPENDAFTARAN = .Rows(iLoop)("KDPENDAFTARAN")
                    dsRekap.KDCUSTOMER = .Rows(iLoop)("KDCUSTOMER")
                    dsRekap.TUJUAN = .Rows(iLoop)("TUJUAN")
                    dsRekap.DPJP = .Rows(iLoop)("DPJP")
                    dsRekap.PASIEN = .Rows(iLoop)("PASIEN")
                    dsRekap.ALAMAT = .Rows(iLoop)("ALAMAT")
                    dsRekap.KELAS = .Rows(iLoop)("KELAS")
                    dsRekap.TANGGAL_DATANG = .Rows(iLoop)("TANGGAL_DATANG")
                    dsRekap.TANGGAL_PULANG = .Rows(iLoop)("TANGGAL_PULANG")
                    dsRekap.NOINVOICE = ""
                    dsRekap.SUBTOTAL = .Rows(iLoop)("SUBTOTAL")
                    dsRekap.COSTSHARE = .Rows(iLoop)("COSTSHARE")
                    dsRekap.DEPOSIT = .Rows(iLoop)("DEPOSIT")
                    dsRekap.ADMIN = .Rows(iLoop)("DISCOUNT")
                    dsRekap.ROUND = .Rows(iLoop)("TAX")
                    dsRekap.GRANDTOTAL = .Rows(iLoop)("GRANDTOTAL")
                    dsRekap.ITEM_GROUP = .Rows(iLoop)("ITEM_GROUP")
                    dsRekap.ITEM = .Rows(iLoop)("ITEM")
                    dsRekap.ITEM_GROUP = .Rows(iLoop)("ITEM_GROUP")
                    dsRekap.QTY = .Rows(iLoop)("QTY")
                    dsRekap.GRANDTOTAL_DETAIL = .Rows(iLoop)("GRANDTOTAL_DETAIL")

                    listTranskasi.Add(dsRekap)
                End With
            Next

            Dim rpt As New xtraCashIn

            rpt.ShowPrintMarginsWarning = False
            rpt.Watermark.Text = sWATERMARK

            rpt.bindingSource.DataSource = listTranskasi
            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
            printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_LoadSecurity()
    End Sub
    Private Sub picPembayaran_Click(sender As Object, e As EventArgs) Handles picPembayaran.Click
        If grv.GetFocusedRowCellValue("KDSOTANPARESEP") Is Nothing Then
            Exit Sub
        End If
        If CBool(grv.GetFocusedRowCellValue("Bayar")) = True Then
            MsgBox("Sudah Bayar", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If MsgBox("Apakah yakin ada pembayaran?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        If oSalesOrderTanpaResep.UpdatePayamount(grv.GetFocusedRowCellValue("KDSOTANPARESEP")) = True Then
            MsgBox("Berhasil Pembayaran", MsgBoxStyle.Information, Me.Text)
            fn_LoadSecurity()
        Else
            MsgBox("Gagal Pembayaran", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
#End Region
End Class