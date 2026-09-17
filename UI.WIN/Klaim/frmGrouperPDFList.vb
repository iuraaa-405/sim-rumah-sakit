Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmGrouperPDFList
    Implements ILanguage

    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oPendaftaranPDF As New Admission.clsPendaftaranPDF

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "Scan PDF - List"

        deDATEFrom.DateTime = Now
        deDATETo.DateTime = Now

        fn_LoadSecurity()
    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        PdfViewer1.CloseDocument()
    End Sub
    Private Sub fn_LoadSecurity()
        Try
            Dim oOtority As New Setting.clsOtority
            Dim oUser As New Setting.clsUser

            Dim ds = (From x In oOtority.GetDataDetail
                      Join y In oUser.GetData
                      On x.KDOTORITY Equals y.KDOTORITY
                      Where x.MODUL = "SCANPDF" _
                      And y.KDUSER = sUserID
                      Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                picAdd.Enabled = ds.ISADD
                picDelete.Enabled = ds.ISDELETE
                'picUpdate.Enabled = ds.ISUPDATE
                'picPrint.Enabled = ds.ISPRINT
                picRefresh.Enabled = ds.ISVIEW

                If ds.ISVIEW = True Then
                    fn_LoadData()
                    fn_LoadLanguage()
                End If
            Catch oErr As Exception
                MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)

                picAdd.Enabled = False
                picDelete.Enabled = False
                'picUpdate.Enabled = False
                'picPrint.Enabled = False
                picRefresh.Enabled = False
            End Try

        Catch oErr As Exception
            MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            Me.Text = "Scan PDF List"

            'fn_LoadLanguageMaster()
            'fn_LoadLanguageDetail()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Sub fn_LoadLanguageMaster()
        Try
            'grv.Columns("KDPENDAFTARAN").Caption = GrouperPDF.KDGrouperPDF
            'grv.Columns("TANGGAL").Caption = GrouperPDF.TANGGAL
            'grv.Columns("KDWAREHOUSE").Caption = GrouperPDF.KDWAREHOUSE
            'grv.Columns("MEMO").Caption = GrouperPDF.MEMO
            'grv.Columns("KDUSER").Caption = Caption.User
        Catch oErr As Exception

        End Try
    End Sub
    Public Sub fn_LoadLanguageDetail()
        Try
            'grv1.Columns("QTY").Caption = GrouperPDF.DETAIL_QTY
            'grv1.Columns("REMARKS").Caption = GrouperPDF.DETAIL_REMARKS

            'grv1.Columns("M_ITEM.NMITEM1").Caption = Item.NMITEM1_2
            'grv1.Columns("M_ITEM.NMITEM2").Caption = Item.NMITEM2_2
            'grv1.Columns("M_ITEM.NMITEM3").Caption = Item.NMITEM3_2
            'grv1.Columns("M_UOM.MEMO").Caption = GrouperPDF.DETAIL_KDUOM
        Catch oErr As Exception

        End Try
    End Sub
    Private Sub fn_LoadData()
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
            'SQL &= "No = ROW_NUMBER() OVER (ORDER BY A.KDPENDAFTARAN) "
            SQL &= "A.KDPENDAFTARAN "
            SQL &= ",TANGGAL = A.DATE "
            'SQL &= ",DAFTAR_L1 = E.MEMO "
            'SQL &= ",DAFTAR_L2 = F.MEMO "
            'SQL &= ",DAFTAR_L3 = G.MEMO "
            'SQL &= ",DAFTAR_L4 = H.MEMO "
            'SQL &= ",DAFTAR_L5 = I.MEMO "
            'SQL &= ",DAFTAR_L6 = J.MEMO "
            SQL &= ",NORM = A.KDCUSTOMER "
            SQL &= ",PASIEN = D.NAME_DISPLAY "
            SQL &= ",TUJUAN = B.NAME_DISPLAY  "
            SQL &= ",DOKTER = C.NAME_DISPLAY "
            SQL &= ",NoSEP = A.NOMORSEP "
            'SQL &= ",KodeBooking = A.KODEBOOKING "
            SQL &= ",NoSKD = A.NOMORSKDP "
            'SQL &= ",TujuanKunjungan = A.TUJUANKUNJUNGAN "
            SQL &= ",A.KDUSER "
            'SQL &= ",STATUSDAFTAR = (SELECT CASE A.STATUSDAFTAR WHEN 0 THEN 'DAFTAR' WHEN 1 THEN 'BATAL' ELSE '' END) "
            'SQL &= ",JENISPASIEN = (SELECT CASE D.KDCUSTOMER_LAMA WHEN '' THEN (SELECT CASE WHEN CONVERT(VARCHAR(8), A.DATE, 112) = CONVERT(VARCHAR(8), D.DATECREATED, 112) THEN 'PASIEN BARU' ELSE 'PASIEN LAMA' END) ELSE 'PASIEN LAMA' END) "
            SQL &= ",NaikRanap = (SELECT CASE A.CATEGORY WHEN 0 THEN (SELECT CASE A.KDPENDAFTARAN_AWAL WHEN '' THEN CONVERT(BIT, 0) ELSE CONVERT(BIT, 1) END) ELSE CONVERT(BIT, 0) END) "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_H A "
            SQL &= "INNER JOIN M_DEPARTMENT B "
            SQL &= "ON A.KDDEPARTMENT = B.KDDEPARTMENT "
            SQL &= "INNER JOIN M_DOCTOR C "
            SQL &= "ON A.KDDOCTOR = C.KDDOCTOR "
            SQL &= "INNER JOIN M_CUSTOMER D "
            SQL &= "ON A.KDCUSTOMER = D.KDCUSTOMER "
            SQL &= "INNER JOIN M_DAFTAR_L1 E "
            SQL &= "ON A.KDDAFTAR_L1 = E.KDDAFTAR_L1 "
            SQL &= "INNER JOIN M_DAFTAR_L2 F "
            SQL &= "ON A.KDDAFTAR_L2 = F.KDDAFTAR_L2 "
            SQL &= "INNER JOIN M_DAFTAR_L3 G "
            SQL &= "ON A.KDDAFTAR_L3 = G.KDDAFTAR_L3 "
            SQL &= "INNER JOIN M_DAFTAR_L4 H "
            SQL &= "ON A.KDDAFTAR_L4 = H.KDDAFTAR_L4 "
            SQL &= "INNER JOIN M_DAFTAR_L5 I "
            SQL &= "ON A.KDDAFTAR_L5 = I.KDDAFTAR_L5 "
            SQL &= "INNER JOIN M_DAFTAR_L6 J "
            SQL &= "ON A.KDDAFTAR_L6 = J.KDDAFTAR_L6 "
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

        'grv.Columns("KDPENDAFTARAN").VisibleIndex = -1
        'grv.Columns("SEQ").VisibleIndex = -1
        'grv.Columns("ALAMAT_UPLOAD").VisibleIndex = -1
    End Sub
    Private Sub MasterColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MasterColumnChooserToolStripMenuItem.Click
        grv.ShowCustomization()
    End Sub
    Private Function fn_DeleteData(ByVal sKDGrouperPDF As String) As Boolean
        Try
            oPendaftaranPDF.DeleteData(sKDGrouperPDF)

            fn_DeleteData = True
        Catch oErr As Exception
            fn_DeleteData = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub grv_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grv.RowStyle
        If grv.IsFilterRow(e.RowHandle) Then Exit Sub
        'If CBool(grv.GetRowCellValue(e.RowHandle, "ISDELETE")) = True Then
        '    e.Appearance.BackColor = Color.LightGray
        'Else
        '    e.Appearance.BackColor = Color.LightGreen
        'End If
    End Sub
#End Region
#Region "Command Button"
    Private Sub grv_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grv.FocusedRowChanged
        'Try
        '    If grv.GetFocusedRowCellValue("KDPENDAFTARAN") Is Nothing Then
        '        Exit Sub
        '    End If

        '    PdfViewer1.CloseDocument()

        '    If grv.GetFocusedRowCellValue("ALAMAT_UPLOAD") <> "" Then
        '        PdfViewer1.LoadDocument(grv.GetFocusedRowCellValue("ALAMAT_UPLOAD"))
        '    End If
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub
    Private Sub frmMember_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.A
                If e.Alt = True And picAdd.Enabled = True Then
                    picAdd_Click()
                End If
            'Case Keys.E
            '    If e.Alt = True And picUpdate.Enabled = True Then
            '        picUpdate_Click()
            '    End If
            Case Keys.D
                If e.Alt = True And picDelete.Enabled = True Then
                    picDelete_Click()
                End If
            'Case Keys.P
            '    If e.Alt = True And picPrint.Enabled = True Then
            '        picPrint_Click()
            '    End If
            Case Keys.R
                If e.Alt = True And picRefresh.Enabled = True Then
                    picRefresh_Click()
                End If
        End Select
    End Sub
    Private Sub grv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grv.DoubleClick
        If grv.GetFocusedRowCellValue("KDPENDAFTARAN") Is Nothing Then
            Exit Sub
        End If

        Dim frmGrouperPDF As New frmGrouperPDF
        Try
            frmGrouperPDF.LoadMeRegistrasi(FORM_MODE.FORM_MODE_VIEW, grv.GetFocusedRowCellValue("KDPENDAFTARAN"))
            frmGrouperPDF.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAdd_Click() Handles picAdd.Click
        If grv.GetFocusedRowCellValue("KDPENDAFTARAN") Is Nothing Then
            Exit Sub
        End If

        Dim ds = oPendaftaranPDF.GetData(grv.GetFocusedRowCellValue("KDPENDAFTARAN"))
        If ds Is Nothing Then
            Dim frmGrouperPDF As New frmGrouperPDF
            Try
                frmGrouperPDF.LoadMeRegistrasi(FORM_MODE.FORM_MODE_ADD, grv.GetFocusedRowCellValue("KDPENDAFTARAN"))
                frmGrouperPDF.ShowDialog(Me)
                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmGrouperPDF Is Nothing Then frmGrouperPDF.Dispose()
                frmGrouperPDF = Nothing

                Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDPENDAFTARAN"), sCode)
                If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

                If sStatusSave = "NEW" Then
                    sStatusSave = "NONE"
                    picAdd_Click()
                End If
            End Try
        Else
            Dim frmGrouperPDF As New frmGrouperPDF
            Try
                frmGrouperPDF.LoadMeRegistrasi(FORM_MODE.FORM_MODE_ADD, ds.KDPENDAFTARAN)
                frmGrouperPDF.ShowDialog(Me)
                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmGrouperPDF Is Nothing Then frmGrouperPDF.Dispose()
                frmGrouperPDF = Nothing

                Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDPENDAFTARAN"), sCode)
                If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

                If sStatusSave = "NEW" Then
                    sStatusSave = "NONE"
                    picAdd_Click()
                End If
            End Try
        End If
    End Sub
    'Private Sub picUpdate_Click() Handles picUpdate.Click
    'If grv.GetFocusedRowCellValue("KDPENDAFTARAN") Is Nothing Then
    '    Exit Sub
    'End If
    'Dim frmGrouperPDF As New frmGrouperPDF
    'Try
    '    frmGrouperPDF.LoadMe(FORM_MODE.FORM_MODE_EDIT, "", "", grv.GetFocusedRowCellValue("KDPENDAFTARAN"))
    '    frmGrouperPDF.ShowDialog(Me)
    '    fn_LoadSecurity()
    'Catch oErr As Exception
    '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    'Finally
    '    If Not frmGrouperPDF Is Nothing Then frmGrouperPDF.Dispose()
    '    frmGrouperPDF = Nothing

    '    Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDPENDAFTARAN"), sCode)
    '    If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

    '    If sStatusSave = "NEW" Then
    '        sStatusSave = "NONE"
    '        picAdd_Click()
    '    End If
    'End Try
    'End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KDPENDAFTARAN") Is Nothing Then
            Exit Sub
        End If
        Dim ds = oPendaftaranPDF.GetData(grv.GetFocusedRowCellValue("KDPENDAFTARAN"))
        If ds Is Nothing Then
            MsgBox("Belum Ada Upload", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_DeleteData(grv.GetFocusedRowCellValue("KDPENDAFTARAN")) = False Then
            MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        fn_LoadSecurity()
    End Sub
    ' Private Sub picPrint_Click() Handles picPrint.Click
    'Try
    'If grv.GetFocusedRowCellValue("KDPENDAFTARAN") = String.Empty Then Exit Sub

    'Dim rpt As New xtraGrouperPDF

    'rpt.ShowPrintMarginsWarning = False
    'rpt.Watermark.Text = sWATERMARK
    'Dim ds = oGrouperPDF.GetData(grv.GetFocusedRowCellValue("KDPENDAFTARAN"))
    'rpt.bindingSource.DataSource = ds
    'Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
    'printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
    'Catch oErr As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_LoadSecurity()
    End Sub
#End Region
End Class