Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports DevExpress.XtraPrinting

Public Class frmDietList
    Implements ILanguage

    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oDiet As New Digital.clsDiet

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "Diet"

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
                      Where x.MODUL = "DIET" _
                     And y.KDUSER = sUserID
                      Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                picAdd.Enabled = ds.ISADD
                picDelete.Enabled = ds.ISDELETE
                picUpdate.Enabled = ds.ISUPDATE
                picPrint.Enabled = ds.ISPRINT
                picRefresh.Enabled = ds.ISVIEW
                picEtiket.Enabled = ds.ISPRINT

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
                picEtiket.Enabled = False
            End Try
        Catch oErr As Exception
            MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            Me.Text = "Diet"

            fn_LoadLanguageMaster()
            fn_LoadLanguageDetail()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Sub fn_LoadLanguageMaster()
        Try
            'grv.Columns("KDDIET").Caption = Diet.KDDiet
            'grv.Columns("TANGGAL").Caption = Diet.TANGGAL
            'grv.Columns("KDWAREHOUSE").Caption = Diet.KDWAREHOUSE
            'grv.Columns("MEMO").Caption = Diet.MEMO
            'grv.Columns("KDUSER").Caption = Caption.User
        Catch oErr As Exception

        End Try
    End Sub
    Public Sub fn_LoadLanguageDetail()
        Try
            'grv1.Columns("QTY").Caption = Diet.DETAIL_QTY
            'grv1.Columns("REMARKS").Caption = Diet.DETAIL_REMARKS

            'grv1.Columns("M_ITEM.NMITEM1").Caption = Item.NMITEM1_2
            'grv1.Columns("M_ITEM.NMITEM2").Caption = Item.NMITEM2_2
            'grv1.Columns("M_ITEM.NMITEM3").Caption = Item.NMITEM3_2
            'grv1.Columns("M_UOM.MEMO").Caption = Diet.DETAIL_KDUOM
        Catch oErr As Exception

        End Try
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = From x In oDiet.GetData
                     Select x.KDDIET, TANGGAL = x.DATE, x.KATEGORI, x.MEMO, Details = x.S_DIET_Ds, x.KDUSER
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

        grv1.Columns().AddVisible("M_BENTUKMAKANAN.MEMO")
        grv1.Columns().AddVisible("M_JENISDIET.MEMO")

        grv1.Columns("KDDIET").Visible = False
        grv1.Columns("S_DIET_H").Visible = False
        grv1.Columns("KDBENTUKMAKANAN").Visible = False
        grv1.Columns("KDJENISDIET").Visible = False
        grv1.Columns("M_BENTUKMAKANAN").Visible = False
        grv1.Columns("M_JENISDIET").Visible = False
        grv1.Columns("KDDOCTOR").Visible = False
        grv1.Columns("ISCHEKED").Visible = False
        grv1.Columns("A_IDENTITASPASIEN_LIST").Visible = False
        grv1.Columns("SEQ").Visible = False

        grv1.Columns("KDDIET").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("S_DIET_H").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("M_BENTUKMAKANAN").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("M_JENISDIET").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("KDDOCTOR").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("ISCHEKED").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("KDBENTUKMAKANAN").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("KDJENISDIET").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("A_IDENTITASPASIEN_LIST").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("SEQ").OptionsColumn.ShowInCustomizationForm = False

        grv1.Columns("M_BENTUKMAKANAN.MEMO").Caption = "Bentuk Makanan"
        grv1.Columns("M_JENISDIET.MEMO").Caption = "Jenis Diet"
        grv1.Columns("KDDOCTOR_NAMEDISPLAY").Caption = "Dokter"
    End Sub
    Private Sub MasterColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MasterColumnChooserToolStripMenuItem.Click
        grv.ShowCustomization()
    End Sub
    Private Sub DetailColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DetailColumnChooserToolStripMenuItem.Click
        grv1.ShowCustomization()
    End Sub
    Private Function fn_DeleteData(ByVal sKDDiet As String) As Boolean
        Try
            oDiet.DeleteData(sKDDiet)

            fn_DeleteData = True
        Catch oErr As Exception
            fn_DeleteData = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub grv_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grv.RowStyle
        'If grv.IsFilterRow(e.RowHandle) Then Exit Sub
        'If CBool(grv.GetRowCellValue(e.RowHandle, "ISDELETE")) = True Then
        '    e.Appearance.BackColor = Color.LightGray
        'Else
        '    e.Appearance.BackColor = Color.LightGreen
        'End If
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
        If grv.GetFocusedRowCellValue("KDDIET") Is Nothing Then
            Exit Sub
        End If

        Dim frmDiet As New frmDiet
        Try
            frmDiet.LoadMe(FORM_MODE.FORM_MODE_VIEW, grv.GetFocusedRowCellValue("KDDIET"))
            frmDiet.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAdd_Click() Handles picAdd.Click
        Dim frmDiet As New frmDiet
        Try
            frmDiet.LoadMe(FORM_MODE.FORM_MODE_ADD)
            frmDiet.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmDiet Is Nothing Then frmDiet.Dispose()
            frmDiet = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDDIET"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KDDIET") Is Nothing Then
            Exit Sub
        End If
        Dim frmDiet As New frmDiet
        Try
            frmDiet.LoadMe(FORM_MODE.FORM_MODE_EDIT, grv.GetFocusedRowCellValue("KDDIET"))
            frmDiet.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmDiet Is Nothing Then frmDiet.Dispose()
            frmDiet = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDDIET"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KDDIET") Is Nothing Then
            Exit Sub
        End If
        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_DeleteData(grv.GetFocusedRowCellValue("KDDIET")) = False Then
            MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        fn_LoadSecurity()
    End Sub
    Private Sub picEtiket_Click() Handles picEtiket.Click
        Try
            If grv.GetFocusedRowCellValue("KDDIET") = String.Empty Then Exit Sub

            For Each xloop In oDiet.GetDataDetail(grv.GetFocusedRowCellValue("KDDIET"))
                Dim dsEtiket = oDiet.GetStructureHeaderEtiket

                With dsEtiket
                    .RUANGAN = xloop.RUANGAN
                    .DESCRIPTION = xloop.WAKTUMAKAN
                    .NAMA = xloop.KDCUSTOMER & " / " & xloop.NAMAPASIEN
                    .NORM = xloop.KDCUSTOMER
                    .EXPIREOBAT = xloop.A_IDENTITASPASIEN_LIST.TANGGALLAHIR.ToString("dd-MM-yyyy")
                    .NORESEP = xloop.BATASMAKAN
                    .NAMAOBAT = xloop.M_JENISDIET.MEMO
                    .DOKTER = xloop.M_BENTUKMAKANAN.MEMO
                    .TANGGAL = Now
                    .TANGGALLAHIR = xloop.A_IDENTITASPASIEN_LIST.TANGGALLAHIR

                    Dim rpt As New xtraEtiketGizi
                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK
                    rpt.BindingSource.DataSource = dsEtiket
                    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                    printTool.Print()
                End With
            Next
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        Try
            PrintableComponentLink.Landscape = True
            PrintableComponentLink.PaperKind = Printing.PaperKind.A4

            Dim phf As PageHeaderFooter =
        TryCast(PrintableComponentLink.PageHeaderFooter, PageHeaderFooter)
            phf.Header.Content.Clear()
            phf.Header.Font = New Font("Times New Roman", 14, FontStyle.Bold)
            phf.Header.LineAlignment = BrickAlignment.Center
            phf.Footer.Font = New Font("Times New Roman", 9.75)
            phf.Footer.LineAlignment = BrickAlignment.Far
            phf.Footer.Content.AddRange(New String() _
        {sWATERMARK, "", Report.REPORT_PAGE & " : [Page # of Pages #]"})

            phf.Header.Content.AddRange(New String() _
{"", "List Diet", ""})

            PrintableComponentLink.Component = grd
            PrintableComponentLink.CreateDocument()
            PrintableComponentLink.ShowPreviewDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_LoadSecurity()
    End Sub

    Private Sub picPrint_Click(sender As Object, e As EventArgs) Handles picPrint.Click

    End Sub
#End Region
End Class