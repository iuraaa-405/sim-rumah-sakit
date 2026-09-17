Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.IO
Imports System.Threading.Tasks

Public Class frmGrouperPDF
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oPendaftaranPDF As New Admission.clsPendaftaranPDF
    Private sAlamatSimpan As String = String.Empty

#End Region
#Region "Function"
    Public Sub LoadMeRegistrasi(ByVal FormMode As Integer, NoId As String)
        oFormMode = FormMode
        txtkodegrouper.Text = NoId
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "Scan PDF"

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtkodegrouper.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDPDF()

        Select Case oFormMode
            Case FORM_MODE.FORM_MODE_VIEW
                fn_ViewMode(True)
                fn_LoadData()
            Case FORM_MODE.FORM_MODE_ADD
                fn_ViewMode(False)
                fn_EmptyMe()
            Case FORM_MODE.FORM_MODE_EDIT
                fn_ViewMode(False)
                fn_LoadData()
            Case Else
                fn_ViewMode(True)
        End Select
    End Sub
    Private Sub fn_ViewMode(ByVal Status As Boolean)
        btnSaveNew.Enabled = Not Status
        btnSaveClose.Enabled = Not Status

        grvDetail.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        grvDetail.OptionsSelection.MultiSelect = True
        grvDetail.SelectAll()
        grvDetail.DeleteSelectedRows()
        grvDetail.OptionsSelection.MultiSelect = False
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            bindingSource.DataSource = oPendaftaranPDF.GetDataListByPendaftaran(txtkodegrouper.Text).ToList()
            grdDetail.DataSource = bindingSource
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If txtkodegrouper.Text = String.Empty Then
                txtkodegrouper.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtkodegrouper.ErrorText = Statement.ErrorRequired

                txtkodegrouper.Focus()
                fn_Validate = False
                Exit Function
            End If

            grvDetail.UpdateCurrentRow()

            If grvDetail.RowCount < 2 Then
                MsgBox(Statement.ErrorDetail, MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End If

            Dim oUser As New Setting.clsUser

            Dim dsDataSetKoneksi = oUser.GetDataKoneksiBPJS("SIMPANPDFCASMIX")
            If dsDataSetKoneksi IsNot Nothing Then
                sAlamatSimpan = dsDataSetKoneksi.ALAMATWEB
                If Not IO.Directory.Exists(sAlamatSimpan) Then
                    MsgBox("Alamat Simpan Tidak di Temukan", MsgBoxStyle.Exclamation, Me.Text)
                    fn_Validate = False
                    Exit Function
                End If
            Else
                MsgBox("Alamat Simpan Tidak di Temukan", MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End If

            'For i As Integer = 0 To grvDetail.RowCount - 2
            '    Dim TES As Decimal = CDec(grvDetail.GetRowCellValue(i, colSIZE))
            '    If CDec(grvDetail.GetRowCellValue(i, colSIZE)) > 10 Then
            '        MsgBox("Terdapat File Lebih dari 10 MB, Silahkan upload Ulang", MsgBoxStyle.Exclamation, Me.Text)
            '        fn_Validate = False
            '        Exit Function
            '    End If
            'Next
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** DETIL *****
            Dim arrDetail = oPendaftaranPDF.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim kirim As Boolean = False

                Dim dsDetail = oPendaftaranPDF.GetStructureDetail
                With dsDetail
                    .DATECREATED = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colDATECREATED)), Now, grvDetail.GetRowCellValue(i, colDATECREATED))
                    .DATEUPDATED = Now
                    .KDPENDAFTARAN = txtkodegrouper.Text
                    .SEQ = i
                    .KDPDF = grvDetail.GetRowCellValue(i, colKDPDF)
                    .ALAMAT_AWAL = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colALAMAT_AWAL)), "", grvDetail.GetRowCellValue(i, colALAMAT_AWAL))
                    .SIZE = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colSIZE)), "-", grvDetail.GetRowCellValue(i, colSIZE))
                    .ISCHEKED = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colISCHEKED)), True, grvDetail.GetRowCellValue(i, colISCHEKED))
                    .NOIDUSER = sUserID
                    .TYPEFILE = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colTYPEFILE)), "", grvDetail.GetRowCellValue(i, colTYPEFILE))

                    Dim sourceFile As String = grvDetail.GetRowCellValue(i, colALAMAT_AWAL)
                    'Dim destinationFile As String = sAlamatSimpan & i & Now.ToString("ddMMyyy HHmmss") & txtkodegrouper.Text & .TYPEFILE

                    Dim destinationFile As String = sAlamatSimpan & i & Now.ToString("ddMMyyy HHmmss") & txtkodegrouper.Text & .TYPEFILE

                    If grvDetail.GetRowCellValue(i, colALAMAT_UPLOAD) = "" Then
                        Try
                            'IO.File.Copy(sourceFile, destinationFile, True)
                            .ISUPLOADKLAIM = True
                            .ALAMAT_UPLOAD = destinationFile

                            ' Copy file (overwrite jika sudah ada)
                            File.Copy(sourceFile, destinationFile, True)
                        Catch ex As Exception
                            MsgBox("gagal Copy", MsgBoxStyle.Exclamation, Me.Text)
                            .ISUPLOADKLAIM = False
                            .ALAMAT_UPLOAD = ""
                        End Try
                    Else
                        .ISUPLOADKLAIM = True
                        .ALAMAT_UPLOAD = grvDetail.GetRowCellValue(i, colALAMAT_UPLOAD)
                    End If

                    kirim = .ISUPLOADKLAIM

                    .REMARKS = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colREMARKS)), "-", grvDetail.GetRowCellValue(i, colREMARKS))
                End With

                If kirim = True Then
                    arrDetail.Add(dsDetail)
                End If
            Next

            If arrDetail.Count <= 0 Then
                MsgBox("Gagal Upload", MsgBoxStyle.Exclamation, Me.Text)
                Exit Function
            End If

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oPendaftaranPDF.InsertData(arrDetail)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oPendaftaranPDF.UpdateData(arrDetail)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
#End Region
#Region "Grid Method"
    Private Sub grvDetail_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDetail.CellValueChanged
        If e.Column.Name = colKDPDF.Name Then
            Try
                If grvDetail.GetFocusedRowCellValue(colKDPDF) IsNot Nothing Then
                    grvDetail.SetFocusedRowCellValue(colDATECREATED, Now)
                    grvDetail.SetFocusedRowCellValue(colISCHEKED, True)
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail.DeleteSelectedRows()
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmAdjustment_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClose_Click()
            'Case Keys.F2
            '    If btnSaveNew.Enabled = True Then
            '        btnSaveNew_Click()
            '    End If
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
        End Select
    End Sub
    Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
            sStatusSave = "NEW"
            Me.Close()
        End If
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
    Private Sub btnBrowsePDF_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles btnBrowsePdf.ButtonClick
        Try
            'If grvDetail.GetFocusedRowCellValue(colKDPDF) IsNot Nothing Then
            '    Dim fBrowse As New OpenFileDialog
            '    With fBrowse
            '        .Filter = "Txt files(*.pdf)|*.pdf|All files (*.*)|*.*"
            '        .FilterIndex = 1
            '        .Title = "Import data from Txt file"
            '    End With

            '    If fBrowse.ShowDialog() = Windows.Forms.DialogResult.OK Then
            '        grvDetail.SetFocusedRowCellValue(colALAMAT_AWAL, fBrowse.FileName)
            '        Dim length As Int64 = New System.IO.FileInfo(fBrowse.FileName).Length
            '        grvDetail.SetFocusedRowCellValue(colSIZE, BytesToMegabytes(length))

            '        grvDetail.UpdateCurrentRow()
            '    End If
            'End If

            Using ofd As New OpenFileDialog()
                ofd.Title = "Pilih file untuk disalin"
                ofd.Filter = "Semua File|*.*"
                ofd.Multiselect = False ' hanya 1 file

                If ofd.ShowDialog() = DialogResult.OK Then
                    Dim sumber As String = ofd.FileName
                    Dim namaFile As String = Path.GetFileName(sumber)
                    grvDetail.SetFocusedRowCellValue(colALAMAT_AWAL, sumber)
                    Dim length As Int64 = New System.IO.FileInfo(sumber).Length
                    grvDetail.SetFocusedRowCellValue(colSIZE, BytesToMegabytes(length))
                    grvDetail.SetFocusedRowCellValue(colTYPEFILE, Path.GetExtension(sumber).ToLower())
                    grvDetail.SetFocusedRowCellValue(colALAMAT_UPLOAD, "")
                    grvDetail.UpdateCurrentRow()
                End If
            End Using
        Catch oErr As Exception
            MsgBox("Load Detail : " & vbCrLf & oErr.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub
    Public Function BytesToMegabytes(Bytes As Double) As Double
        'This function gives an estimate to two decimal
        'places.  For a more precise answer, format to
        'more decimal places or just return dblAns

        Dim dblAns As Double
        dblAns = (Bytes / 1024) / 1024
        BytesToMegabytes = Format(dblAns, "###,###,##0.00")
    End Function
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadKDPDF()
        Dim oPDF As New Reference.clsPDF
        Try
            grdKDPDF.DataSource = oPDF.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDPDF.ValueMember = "KDPDF"
            grdKDPDF.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnUpload_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles btnUpload.ButtonClick
        'Try
        '    Dim oPendaftaran As New Admission.clsPendaftaran
        '    Dim dsPendafatran = oPendaftaran.GetData(txtkodegrouper.Text)
        '    If dsPendafatran Is Nothing Then
        '        MsgBox("Pendaftaran Masih Kosong", MsgBoxStyle.Exclamation, Me.Text)
        '        Exit Sub
        '    End If
        '    frmReportLoadPasienPACS.fn_LoadRM(dsPendafatran.KDCUSTOMER)
        '    frmReportLoadPasienPACS.ShowDialog(Me)
        'Catch ex As Exception
        '    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        'Finally
        '    If Not frmReportLoadPasienPACS Is Nothing Then frmReportLoadPasienPACS.Dispose()
        '    frmReportLoadPasienPACS = Nothing
        'End Try
    End Sub
#End Region
End Class