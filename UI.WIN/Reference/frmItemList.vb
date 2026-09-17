Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmItemList
    Implements ILanguage

    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oItem As New Reference.clsItem

#Region "Function"
    Private Sub Me_Load() Handles Me.Load
        Me.Text = IIf(sStok = False, Item.TITLE_1, Item.TITLE_2)

        fn_LoadSecurity()

        'Try
        '    grv.RestoreLayoutFromRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text)
        'Catch ex As Exception

        'End Try
    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        'Try
        '    grv.SaveLayoutToRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text)
        'Catch ex As Exception

        'End Try
    End Sub
    Private Sub fn_LoadSecurity()
        Try
            Dim oOtority As New Setting.clsOtority
            Dim oUser As New Setting.clsUser

            Dim ds = (From x In oOtority.GetDataDetail _
                     Join y In oUser.GetData _
                     On x.KDOTORITY Equals y.KDOTORITY _
                     Where x.MODUL = "ITEM" _
                     And y.KDUSER = sUserID _
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
            Me.Text = IIf(sStok = False, Item.TITLE_1, Item.TITLE_2)

            grv.Columns("NMITEM1").Caption = IIf(sStok = False, Item.NMITEM1_1, Item.NMITEM1_2)
            grv.Columns("NMITEM2").Caption = IIf(sStok = False, Item.NMITEM2_1, Item.NMITEM2_2)
            grv.Columns("NMITEM3").Caption = IIf(sStok = False, Item.NMITEM3_1, Item.NMITEM3_2)
            grv.Columns("KDITEM_L1").Caption = sItem_L1
            grv.Columns("KDITEM_L2").Caption = sItem_L2
            grv.Columns("KDITEM_L3").Caption = sItem_L3
            grv.Columns("KDITEM_L4").Caption = sItem_L4
            grv.Columns("KDITEM_L5").Caption = sItem_L5
            grv.Columns("KDITEM_L6").Caption = sItem_L6
            grv.Columns("KDCOA_INVENTORY").Caption = Item.KDCOA_INVENTORY
            grv.Columns("KDCOA_SALES").Caption = Item.KDCOA_SALES
            grv.Columns("KDCOA_COST").Caption = Item.KDCOA_COST
            grv.Columns("ISTAX").Caption = Item.ISTAX
            grv.Columns("ISACTIVE").Caption = Item.ISACTIVE

            ColumnChooserToolStripMenuItem.Text = Caption.ColumnChooser
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = From x In oItem.GetData
                     Where x.ISSTOK = sStok
                     Select x.KDITEM, x.NMITEM1, x.NMITEM2, x.NMITEM3, KDITEM_L1 = x.M_ITEM_L1.MEMO, KDITEM_L2 = x.M_ITEM_L2.MEMO, KDITEM_L3 = x.M_ITEM_L3.MEMO, KDITEM_L4 = x.M_ITEM_L4.MEMO, KDITEM_L5 = x.M_ITEM_L5.MEMO, KDITEM_L6 = x.M_ITEM_L6.MEMO, KDCOA_INVENTORY = x.A_COA1.NMCOA, KDCOA_SALES = x.A_COA2.NMCOA, KDCOA_COST = x.A_COA.NMCOA, x.ISTAX, x.ISACTIVE, IDSATUSEHAT = oItem.GetIDSATUSEHAT(x.KDITEM)

            grd.DataSource = ds.ToList
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
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

        grv.Columns("KDITEM").Visible = False
        'grv.Columns("KDITEM_L1").Visible = False
        'grv.Columns("KDITEM_L2").Visible = False
        'grv.Columns("KDITEM_L3").Visible = True
        'grv.Columns("KDITEM_L4").Visible = False
        'grv.Columns("KDITEM_L5").Visible = False
        'grv.Columns("KDITEM_L6").Visible = False
        grv.Columns("KDCOA_INVENTORY").Visible = False
        grv.Columns("KDCOA_SALES").Visible = False
        grv.Columns("KDCOA_COST").Visible = False
        grv.Columns("ISTAX").Visible = False
        grv.Columns("NMITEM1").Visible = False
        grv.Columns("NMITEM3").Visible = False

        grv.Columns("KDITEM").OptionsColumn.ShowInCustomizationForm = False
    End Sub
    Private Sub ColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ColumnChooserToolStripMenuItem.Click
        grv.ShowCustomization()
    End Sub
    Private Function fn_DeleteData(ByVal sKDITEM As String) As Boolean
        Try
            oItem.DeleteData(sKDItem)
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
        If grv.GetFocusedRowCellValue("KDITEM") Is Nothing Then
            Exit Sub
        End If

        Dim frmItem As New frmItem
        Try
            frmItem.LoadMe(FORM_MODE.FORM_MODE_VIEW, grv.GetFocusedRowCellValue("KDITEM"))
            frmItem.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAdd_Click() Handles picAdd.Click
        Dim frmItem As New frmItem
        Try
            frmItem.LoadMe(FORM_MODE.FORM_MODE_ADD)
            frmItem.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmItem Is Nothing Then frmItem.Dispose()
            frmItem = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("NMITEM1"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KDITEM") Is Nothing Then
            Exit Sub
        End If
        Dim frmItem As New frmItem
        Try
            frmItem.LoadMe(FORM_MODE.FORM_MODE_EDIT, grv.GetFocusedRowCellValue("KDITEM"))
            frmItem.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmItem Is Nothing Then frmItem.Dispose()
            frmItem = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("NMITEM1"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KDITEM") Is Nothing Then
            Exit Sub
        End If
        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        Dim Handle As Integer = grv.LocateByValue(Handle, grv.Columns("KDITEM"), grv.GetFocusedRowCellValue("KDITEM"))
        If Handle > 0 Then sCode = grv.GetRowCellValue(Handle - 1, "NMITEM1")

        If fn_DeleteData(grv.GetFocusedRowCellValue("KDITEM")) = False Then
            MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)

        fn_LoadSecurity()

        Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("NMITEM1"), sCode)
        If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        Try
            Dim xtraItem As New xtraItem

            Dim sKDITEM As New List(Of String)
            For i As Integer = 0 To grv.RowCount - 1
                sKDITEM.Add(grv.GetRowCellValue(i, "KDITEM"))
            Next

            Dim ds = oItem.GetData.Where(Function(x) sKDITEM.Contains(x.KDITEM)).ToList

            xtraItem.bindingSource.DataSource = ds
            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(xtraItem)
            printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_LoadSecurity()
    End Sub
#End Region
End Class