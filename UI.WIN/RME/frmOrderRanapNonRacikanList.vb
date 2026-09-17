Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmOrderRanapNonRacikanList
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oOrderRanapNonRacikan As New Order.clsOrderRanapNonRacikan
    Private sRegister As String = String.Empty
    Private sDiagnosa As String = String.Empty
    Private sBB As String = String.Empty
    Private sTB As String = String.Empty
    Private sKDIDENTITAS As Integer = 0
    Private sPulang As Boolean = False

#Region "Function"
    Public Sub Pulang(ByVal isPulang As Boolean)
        sPulang = isPulang
    End Sub
    Public Sub fn_LoadData(ByVal kdidentitas As Integer, ByVal Register As String, ByVal Diagnosa As String, ByVal bb As String, ByVal tb As String)
        sRegister = Register
        sDiagnosa = Diagnosa
        sBB = bb
        sTB = tb
        sKDIDENTITAS = kdidentitas
    End Sub
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "Order Resep List"

        Dim oGrouperDataCppt As New Grouper.clsR_CPPT

        Dim dsIdentitas = oGrouperDataCppt.GetDataIdentitas(sKDIDENTITAS)
        If dsIdentitas IsNot Nothing Then
            txtRM.Text = dsIdentitas.KDCUSTOMER
            txtNAMAPASIEN.Text = dsIdentitas.NAMAPASIEN
            txtTANGGALLAHIR.Text = dsIdentitas.TANGGALLAHIR.ToString("dd-MM-yyyy")
            txtNIK.Text = dsIdentitas.NIK
            txtJENISKELAMIN.Text = dsIdentitas.JENISKELAMIN
            txtPENJAMIN.Text = dsIdentitas.KDDAFTAR_L1_NAMA
        Else
            MsgBox("Identitas Kosong", MsgBoxStyle.Exclamation, Me.Text)
        End If

        fn_LoadSecurity()

    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed

    End Sub
    Private Sub fn_LoadSecurity()
        Try
            Dim oOtority As New Setting.clsOtority
            Dim oUser As New Setting.clsUser

            Dim ds = (From x In oOtority.GetDataDetail
                      Join y In oUser.GetData
                     On x.KDOTORITY Equals y.KDOTORITY
                      Where x.MODUL = "MEDREK_RJ" _
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
    Private Sub fn_LoadData()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\RME\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "B.KDORDER  "
            SQL &= ",TanggalOrder = B.TANGGALORDER "
            SQL &= ",Penunjang = B.MEMO "
            SQL &= ",Dokter = B.USERORDER "
            SQL &= ",Keterangan = B.STATUS "
            SQL &= ",ResepPulang = ISNULL((SELECT MEMO5 FROM R_ORDER_LAINNYA WHERE B.KDORDER = KDORDER ), '') "
            SQL &= "FROM "
            SQL &= "A_IDENTITASPASIEN_LIST A "
            SQL &= "INNER JOIN R_ORDER B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE "
            SQL &= "A.KDPENDAFTARAN = '" & sRegister & "' "
            SQL &= "AND B.MEMO = 'ORDER FARMASI' "

            SQL &= "ORDER BY B.TANGGALORDER "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ORDERPENUNJANG")

            grd.DataSource = ds.Tables("ORDERPENUNJANG")
            grd.ForceInitialize()

            fn_LoadFormatData()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox("Load Penunjang: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    'Private Sub grv_MasterRowExpanded(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventArgs) Handles grv.MasterRowExpanded
    '    grv1 = TryCast(grv.GetDetailView(e.RowHandle, e.RelationIndex), DevExpress.XtraGrid.Views.Grid.GridView)

    '    fn_LoadFormatDataDetail()
    '    fn_LoadLanguageDetail()
    'End Sub
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

        grv.Columns("KDORDER").VisibleIndex = -1
        grv.BestFitColumns()
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

        'grv1.Columns().AddVisible("M_ITEM.NMITEM1")
        'grv1.Columns().AddVisible("M_ITEM.NMITEM2")
        'grv1.Columns().AddVisible("M_ITEM.NMITEM3")
        'grv1.Columns().AddVisible("M_UOM.MEMO")

        'grv1.Columns("DATECREATED").Visible = False
        'grv1.Columns("DATEUPDATED").Visible = False
        'grv1.Columns("SEQ").Visible = False
        'grv1.Columns("KDORDER").Visible = False
        'grv1.Columns("M_ITEM").Visible = False
        'grv1.Columns("M_UOM").Visible = False
        'grv1.Columns("I_OrderRanapNonRacikan_H").Visible = False
        'grv1.Columns("KDITEM").Visible = False
        'grv1.Columns("KDUOM").Visible = False

        'grv1.Columns("DATECREATED").OptionsColumn.ShowInCustomizationForm = False
        'grv1.Columns("DATEUPDATED").OptionsColumn.ShowInCustomizationForm = False
        'grv1.Columns("SEQ").OptionsColumn.ShowInCustomizationForm = False
        'grv1.Columns("KDORDER").OptionsColumn.ShowInCustomizationForm = False
        'grv1.Columns("M_ITEM").OptionsColumn.ShowInCustomizationForm = False
        'grv1.Columns("M_UOM").OptionsColumn.ShowInCustomizationForm = False
        'grv1.Columns("I_OrderRanapNonRacikan_H").OptionsColumn.ShowInCustomizationForm = False
        'grv1.Columns("KDITEM").OptionsColumn.ShowInCustomizationForm = False
        'grv1.Columns("KDUOM").OptionsColumn.ShowInCustomizationForm = False

        'grv1.Columns("M_ITEM.NMITEM1").VisibleIndex = 0
        'grv1.Columns("M_ITEM.NMITEM2").VisibleIndex = 1
        'grv1.Columns("M_ITEM.NMITEM3").VisibleIndex = 2
        'grv1.Columns("M_UOM.MEMO").VisibleIndex = 4
    End Sub
    'Private Sub MasterColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MasterColumnChooserToolStripMenuItem.Click
    '    grv.ShowCustomization()
    'End Sub
    'Private Sub DetailColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DetailColumnChooserToolStripMenuItem.Click
    '    grv1.ShowCustomization()
    'End Sub
    Private Function fn_DeleteData(ByVal sKDOrderRanapNonRacikan As String) As Boolean
        Try
            oOrderRanapNonRacikan.DeleteData(sKDOrderRanapNonRacikan)

            fn_DeleteData = True
        Catch oErr As Exception
            fn_DeleteData = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub grv_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grv.RowStyle
        If grv.IsFilterRow(e.RowHandle) Then Exit Sub
        If CBool(grv.GetRowCellValue(e.RowHandle, "ISDELETE")) = True Then
            e.Appearance.BackColor = Color.LightGray
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
    Private Sub grv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grv.DoubleClick
        If grv.GetFocusedRowCellValue("KDORDER") Is Nothing Then
            Exit Sub
        End If

        Dim frmOrderRanapNonRacikan As New frmOrderRanapNonRacikan
        Try
            frmOrderRanapNonRacikan.LoadMe(FORM_MODE.FORM_MODE_VIEW, sKDIDENTITAS, sDiagnosa, sBB, sTB, grv.GetFocusedRowCellValue("KDORDER"), sPulang)
            frmOrderRanapNonRacikan.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAdd_Click() Handles picAdd.Click
        Dim dsCekPulang = oOrderRanapNonRacikan.GetDataLainnyaByRegisterPasienPulang(sRegister)
        If dsCekPulang IsNot Nothing Then
            MsgBox("Sudah Ada Resep Pulang, Silahkan perbaiki atau hapus resep", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim frmOrderRanapNonRacikan As New frmOrderRanapNonRacikan
        Try
            frmOrderRanapNonRacikan.LoadMe(FORM_MODE.FORM_MODE_ADD, sKDIDENTITAS, sDiagnosa, sBB, sTB, "", sPulang)
            frmOrderRanapNonRacikan.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmOrderRanapNonRacikan Is Nothing Then frmOrderRanapNonRacikan.Dispose()
            frmOrderRanapNonRacikan = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDORDER"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KDORDER") Is Nothing Then
            Exit Sub
        End If
        'GetDataByOrderFarmasi
        Dim oSalesOrderTransaksi As New Sales.clsSalesOrderTransaksi

        Dim dsCekSudahInput = oSalesOrderTransaksi.GetDataByOrderFarmasi(grv.GetFocusedRowCellValue("KDORDER"))
        If dsCekSudahInput IsNot Nothing Then
            MsgBox("Sudah diTerima Farmasi Tidak dapat dirubah, Silahkan hubungi Farmasi jika ada perubahan", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim frmOrderRanapNonRacikan As New frmOrderRanapNonRacikan
        Try
            frmOrderRanapNonRacikan.LoadMe(FORM_MODE.FORM_MODE_EDIT, sKDIDENTITAS, sDiagnosa, sBB, sTB, grv.GetFocusedRowCellValue("KDORDER"), sPulang)
            frmOrderRanapNonRacikan.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmOrderRanapNonRacikan Is Nothing Then frmOrderRanapNonRacikan.Dispose()
            frmOrderRanapNonRacikan = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDORDER"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KDORDER") Is Nothing Then
            Exit Sub
        End If
        Dim oSalesOrderTransaksi As New Sales.clsSalesOrderTransaksi

        Dim dsCekSudahInput = oSalesOrderTransaksi.GetDataByOrderFarmasi(grv.GetFocusedRowCellValue("KDORDER"))
        If dsCekSudahInput IsNot Nothing Then
            MsgBox("Sudah diTerima Farmasi Tidak dapat dirubah, Silahkan hubungi Farmasi jika ada perubahan", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_DeleteData(grv.GetFocusedRowCellValue("KDORDER")) = False Then
            MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        fn_LoadSecurity()
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        Try
            'If grv.GetFocusedRowCellValue("KDORDER") = String.Empty Then Exit Sub

            'Dim rpt As New xtraOrderRanapNonRacikan

            'rpt.ShowPrintMarginsWarning = False
            'rpt.Watermark.Text = sWATERMARK
            'Dim ds = oOrderRanapNonRacikan.GetData(grv.GetFocusedRowCellValue("KDORDER"))
            'rpt.bindingSource.DataSource = ds
            'Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
            'printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_LoadSecurity()
    End Sub
#End Region
End Class