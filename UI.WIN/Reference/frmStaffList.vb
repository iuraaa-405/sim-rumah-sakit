Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmStaffList
    Implements ILanguage

    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oStaff As New Reference.clsStaff

#Region "Function"
    Private Sub Me_Load() Handles Me.Load
        Me.Text = Staff.TITLE

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
                      Where x.MODUL = "STAFF" _
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
            Me.Text = Staff.TITLE

            grv.Columns("NAME_DISPLAY").Caption = Staff.NAME_DISPLAY
            grv.Columns("ISACTIVE").Caption = Staff.ISACTIVE
            grv.Columns("SUB").Caption = "Sub"
            grv.Columns("BAGIAN").Caption = Staff.KDSTAFFBAGIAN
            grv.Columns("PANGKAT").Caption = Staff.KDSTAFFPANGKAT
            grv.Columns("JABATAN").Caption = Staff.KDSTAFFJABATAN
            grv.Columns("TMT").Caption = Staff.TMT
            grv.Columns("TANGGALLAHIR").Caption = Staff.TANGGALLAHIR
            grv.Columns("NOMOR_NIP").Caption = Staff.NOMOR_NIP

            ColumnChooserToolStripMenuItem.Text = Caption.ColumnChooser
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData()
        Try
            If chkFinger.Checked = True Then
                Dim ds = From x In oStaff.GetData
                         Select x.KDSTAFF, x.M_STAFFBAGIAN.SUB, BAGIAN = x.M_STAFFBAGIAN.MEMO, PANGKAT = x.M_STAFFPANGKAT.MEMO, JABATAN = x.M_STAFFJABATAN.MEMO, x.NAME_DISPLAY, x.NOMOR_NIP, x.TMT, x.TANGGALLAHIR, J0 = IIf(x.J0 IsNot Nothing, "YA", "NO"), J1 = IIf(x.J1 IsNot Nothing, "YA", "NO"), J2 = IIf(x.J2 IsNot Nothing, "YA", "NO"), J3 = IIf(x.J3 IsNot Nothing, "YA", "NO"), J4 = IIf(x.J4 IsNot Nothing, "YA", "NO"), J5 = IIf(x.J5 IsNot Nothing, "YA", "NO"), J6 = IIf(x.J6 IsNot Nothing, "YA", "NO"), J7 = IIf(x.J7 IsNot Nothing, "YA", "NO"), J8 = IIf(x.J8 IsNot Nothing, "YA", "NO"), J9 = IIf(x.J9 IsNot Nothing, "YA", "NO"), x.ISACTIVE

                grd.DataSource = ds.ToList

                fn_LoadFormatData()
            Else
                Dim ds = From x In oStaff.GetData
                         Select x.KDSTAFF, x.M_STAFFBAGIAN.SUB, BAGIAN = x.M_STAFFBAGIAN.MEMO, PANGKAT = x.M_STAFFPANGKAT.MEMO, JABATAN = x.M_STAFFJABATAN.MEMO, x.NAME_DISPLAY, x.NOMOR_NIP, x.TMT, x.TANGGALLAHIR, x.ISACTIVE

                grd.DataSource = ds.ToList

                fn_LoadFormatData()
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

        grv.Columns("KDSTAFF").Visible = False
        grv.Columns("KDSTAFF").OptionsColumn.ShowInCustomizationForm = False
    End Sub
    Private Sub ColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ColumnChooserToolStripMenuItem.Click
        grv.ShowCustomization()
    End Sub
    Private Function fn_DeleteData(ByVal sKDStaff As String) As Boolean
        Try
            oStaff.DeleteData(sKDStaff)
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
        If grv.GetFocusedRowCellValue("KDSTAFF") Is Nothing Then
            Exit Sub
        End If

        Dim frmStaff As New frmStaff
        Try
            frmStaff.LoadMe(FORM_MODE.FORM_MODE_VIEW, grv.GetFocusedRowCellValue("KDSTAFF"))
            frmStaff.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAdd_Click() Handles picAdd.Click
        Dim frmStaff As New frmStaff
        Try
            frmStaff.LoadMe(FORM_MODE.FORM_MODE_ADD)
            frmStaff.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmStaff Is Nothing Then frmStaff.Dispose()
            frmStaff = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("NAME_DISPLAY"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KDSTAFF") Is Nothing Then
            Exit Sub
        End If
        Dim frmStaff As New frmStaff
        Try
            frmStaff.LoadMe(FORM_MODE.FORM_MODE_EDIT, grv.GetFocusedRowCellValue("KDSTAFF"))
            frmStaff.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmStaff Is Nothing Then frmStaff.Dispose()
            frmStaff = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("NAME_DISPLAY"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KDSTAFF") Is Nothing Then
            Exit Sub
        End If
        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_DeleteData(grv.GetFocusedRowCellValue("KDSTAFF")) = False Then
            MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        fn_LoadSecurity()
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        'Try
        '    Dim xtraStaff As New xtraStaff

        '    Dim sKDStaff As New List(Of String)
        '    For i As Integer = 0 To grv.RowCount - 1
        '        sKDStaff.Add(grv.GetRowCellValue(i, "KDSTAFF"))
        '    Next

        '    Dim ds = oStaff.GetData.Where(Function(x) sKDStaff.Contains(x.KDStaff)).ToList

        '    xtraStaff.bindingSource.DataSource = ds
        '    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(xtraStaff)
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