Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmBHPPasienList
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oBHPPasien As New Inventory.clsBHPPasien
    Private sRegister As String = String.Empty
    Private sKDDEPARTMENT As String = String.Empty
    Private sKDIDENTITAS As Integer = 0
#Region "Function"
    Public Sub fn_LoadData(ByVal kdidentitas As Integer, ByVal Register As String)
        sRegister = Register
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
            sKDDEPARTMENT = dsIdentitas.KDDEPARTMENT
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
                      Where x.MODUL = "BHPPASIEN" _
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
            SQL &= "A.KDBHPPASIEN  "
            SQL &= ",Tanggal = A.DATE "
            SQL &= ",BHPRuang = C.NAME_DISPLAY "
            SQL &= ",Catatan = A.MEMO "
            SQL &= "FROM "
            SQL &= "DATABASERS..I_BHPPASIEN_H A "
            SQL &= "INNER JOIN DATABASERS..S_PENDAFTARAN_KUNJUNGAN B "
            SQL &= "ON A.KDKUNJUNGAN = B.KDKUNJUNGAN "
            SQL &= "INNER JOIN DATABASERS..M_WAREHOUSE C "
            SQL &= "ON A.KDWAREHOUSE = C.KDWAREHOUSE "
            SQL &= "WHERE "
            SQL &= "B.KDPENDAFTARAN = '" & sRegister & "' "
            SQL &= "ORDER BY A.DATE "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "I_BHPPASIEN_H")

            grd.DataSource = ds.Tables("I_BHPPASIEN_H")
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

        grv.Columns("KDBHPPASIEN").VisibleIndex = -1
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

    End Sub
    'Private Sub MasterColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MasterColumnChooserToolStripMenuItem.Click
    '    grv.ShowCustomization()
    'End Sub
    'Private Sub DetailColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DetailColumnChooserToolStripMenuItem.Click
    '    grv1.ShowCustomization()
    'End Sub
    Private Function fn_DeleteData(ByVal sKDBHPPasien As String) As Boolean
        Try
            oBHPPasien.DeleteData(sKDBHPPasien)

            fn_DeleteData = True
        Catch oErr As Exception
            fn_DeleteData = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    'Private Sub grv_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grv.RowStyle
    '    If grv.IsFilterRow(e.RowHandle) Then Exit Sub
    '    If CBool(grv.GetRowCellValue(e.RowHandle, "ISDELETE")) = True Then
    '        e.Appearance.BackColor = Color.LightGray
    '    Else
    '        e.Appearance.BackColor = Color.LightGreen
    '    End If
    'End Sub
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
        If grv.GetFocusedRowCellValue("KDBHPPASIEN") Is Nothing Then
            Exit Sub
        End If

        Dim frmBHPPasien As New frmBHPPasien
        Try
            frmBHPPasien.LoadMe(FORM_MODE.FORM_MODE_VIEW, sKDDEPARTMENT, sKDIDENTITAS, grv.GetFocusedRowCellValue("KDBHPPASIEN"))
            frmBHPPasien.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAdd_Click() Handles picAdd.Click
        Dim frmBHPPasien As New frmBHPPasien
        Try
            frmBHPPasien.LoadMe(FORM_MODE.FORM_MODE_ADD, sKDDEPARTMENT, sKDIDENTITAS, "")
            frmBHPPasien.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmBHPPasien Is Nothing Then frmBHPPasien.Dispose()
            frmBHPPasien = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDBHPPASIEN"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KDBHPPASIEN") Is Nothing Then
            Exit Sub
        End If

        Dim frmPesanDelete As New frmPesanDelete
        frmPesanDelete.ShowDialog(Me)

        If sPesanHapus <> "XXXXXBATALXXXXX" Then
            Dim oDelete As New Setting.clsDelete
            If oDelete.InsertData("UPDATEBHPPASIEN", sUserID, sPesanHapus, grv.GetFocusedRowCellValue("KDBHPPASIEN")) = True Then
                Dim frmBHPPasien As New frmBHPPasien
                Try
                    frmBHPPasien.LoadMe(FORM_MODE.FORM_MODE_EDIT, sKDDEPARTMENT, sKDIDENTITAS, grv.GetFocusedRowCellValue("KDBHPPASIEN"))
                    frmBHPPasien.ShowDialog(Me)
                    fn_LoadSecurity()
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                Finally
                    If Not frmBHPPasien Is Nothing Then frmBHPPasien.Dispose()
                    frmBHPPasien = Nothing

                    Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDBHPPASIEN"), sCode)
                    If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

                    If sStatusSave = "NEW" Then
                        sStatusSave = "NONE"
                        picAdd_Click()
                    End If
                End Try
            End If
        End If
    End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KDBHPPASIEN") Is Nothing Then
            Exit Sub
        End If

        Dim frmPesanDelete As New frmPesanDelete
        frmPesanDelete.ShowDialog(Me)

        If sPesanHapus <> "XXXXXBATALXXXXX" Then
            Dim oDelete As New Setting.clsDelete
            If oDelete.InsertData("DELETEBHPPASIEN", sUserID, sPesanHapus, grv.GetFocusedRowCellValue("KDBHPPASIEN")) = True Then
                If fn_DeleteData(grv.GetFocusedRowCellValue("KDBHPPASIEN")) = False Then
                    MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                End If

                MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
                fn_LoadSecurity()
            End If
        End If
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        Try
            'If grv.GetFocusedRowCellValue("KDBHPPASIEN") = String.Empty Then Exit Sub

            'Dim rpt As New xtraBHPPasien

            'rpt.ShowPrintMarginsWarning = False
            'rpt.Watermark.Text = sWATERMARK
            'Dim ds = oBHPPasien.GetData(grv.GetFocusedRowCellValue("KDBHPPASIEN"))
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