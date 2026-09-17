Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports DevExpress.XtraSplashScreen
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

Public Class frmCashinBPJSBayar
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oCashinBPJSBayar As New Finance.clsCashinBPJSBayar

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "RINCIAN DATA HASIL VERIFIKASI"

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDCASHINBPJS.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
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
        btnLoadTXT.Enabled = Not Status

        deDATE.Properties.ReadOnly = Status
        txtJUDULBAYAR.Properties.ReadOnly = Status
        rbCATEGORY.Properties.ReadOnly = Status
        txtMEMO.Properties.ReadOnly = Status
        txtSUBTOTAL.Properties.ReadOnly = Status
        txtADMIN.Properties.ReadOnly = Status
        txtGRANDTOTAL.Properties.ReadOnly = Status
        grvDetail.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDCASHINBPJS.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        txtJUDULBAYAR.ResetText()
        txtMEMO.ResetText()
        txtSUBTOTAL.ResetText()
        txtADMIN.ResetText()
        txtGRANDTOTAL.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oCashinBPJSBayar.GetData(sNoId)

            With ds
                txtKDCASHINBPJS.Text = .KDCASHINBAYARBPJS
                deDATE.DateTime = .DATE
                txtJUDULBAYAR.Text = .JUDULJASABAYAR
                txtMEMO.Text = .MEMO
                txtSUBTOTAL.Text = .SUBTOTAL
                txtADMIN.Text = .ADMIN
                txtGRANDTOTAL.Text = .GRANDTOTAL
                rbCATEGORY.SelectedIndex = .CATEGORY

                bindingSource.DataSource = oCashinBPJSBayar.GetDataDetail.Where(Function(x) x.KDCASHINBPJSBAYAR = sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = bindingSource

            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If txtJUDULBAYAR.Text = String.Empty Then
                txtJUDULBAYAR.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtJUDULBAYAR.ErrorText = Statement.ErrorRequired

                txtJUDULBAYAR.Focus()
                fn_Validate = False
                Exit Function
            End If

            grvDetail.UpdateCurrentRow()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oCashinBPJSBayar.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oCashinBPJSBayar.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDCASHINBAYARBPJS = sNoId
                .DATE = deDATE.DateTime
                .CATEGORY = rbCATEGORY.SelectedIndex
                .JUDULJASABAYAR = txtJUDULBAYAR.Text
                .MEMO = txtMEMO.Text
                .SUBTOTAL = CDec(txtSUBTOTAL.Text)
                .ADMIN = CDec(txtADMIN.Text)
                .ROUND = CDec(0)
                .GRANDTOTAL = CDec(txtSUBTOTAL.Text)
                .KDUSER = sUserID
            End With

            ' ***** DETIL *****
            Dim arrDetail = oCashinBPJSBayar.GetStructureDetailList

            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oCashinBPJSBayar.GetStructureDetail
                With dsDetail
                    .KDCASHINBPJSBAYAR = ds.KDCASHINBAYARBPJS
                    .SEQ = grvDetail.GetRowCellValue(i, colSEQ)
                    .NOSEP = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colNOSEP)), "", grvDetail.GetRowCellValue(i, colNOSEP))
                    .TANGGALVERIFIKASI = grvDetail.GetRowCellValue(i, colTANGGALVERIFIKASI)
                    .BIAYARIILRS = grvDetail.GetRowCellValue(i, colBIAYARIILRS)
                    .BIAYADIAJUKAN = grvDetail.GetRowCellValue(i, colBIAYADIAJUKAN)
                    .BIAYADISETUJUI = grvDetail.GetRowCellValue(i, colBIAYADISETUJUI)
                    .REMARKS = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colREMARKS)), "-", grvDetail.GetRowCellValue(i, colREMARKS))
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oCashinBPJSBayar.InsertData(ds, arrDetail)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oCashinBPJSBayar.UpdateData(ds, arrDetail)
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
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail.DeleteSelectedRows()
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmCashinBPJS_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClose_Click()
            Case Keys.F2
                If btnSaveNew.Enabled = True Then
                    btnSaveNew_Click()
                End If
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
            Case Keys.F5
                If btnLoadTXT.Enabled = True Then
                    btnLoadTXT_Click()
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
    Private Sub btnLoadTXT_Click() Handles btnLoadTXT.ItemClick
        fn_BrowseTxt()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_BrowseTxt()
        If fn_Validate() = True Then
            Dim fBrowse As New OpenFileDialog
            With fBrowse
                .Filter = "Txt files(*.txt)|*.txt|All files (*.*)|*.*"
                .FilterIndex = 1
                .Title = "Import data from Txt file"
            End With

            If fBrowse.ShowDialog() = Windows.Forms.DialogResult.OK Then
                grvDetail.OptionsSelection.MultiSelect = True
                grvDetail.SelectAll()
                grvDetail.DeleteSelectedRows()
                grvDetail.OptionsSelection.MultiSelect = False
                txtSUBTOTAL.ResetText()
                txtADMIN.ResetText()

                Dim arrDetail = oCashinBPJSBayar.GetStructureDetailList

                Dim sProcess As Integer = 0
                Dim sTotal As Integer = 0
                Dim NOMOR As Integer = 0

                Dim oUmpanBalik As New Finance.clsCashinBPJS
                Dim reader As New System.IO.StreamReader(fBrowse.FileName)
                Dim allLines As List(Of String) = New List(Of String)
                Dim RecordLine As List(Of String) = New List(Of String)

                Do While Not reader.EndOfStream
                    allLines.Add(reader.ReadLine())
                    sTotal += 1
                Loop

                If MsgBox("Apa anda yakin akan mengimport " & sTotal - 1 & " Baris data ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

                Try
                    SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

                    For Each xLoop In allLines

                        RecordLine.Add(xLoop)

                        Dim Record As List(Of String) = New List(Of String)

                        Dim TANGGALVERIFIKASI As DateTime = Now
                        Dim NOMORSEP As String = String.Empty
                        Dim BIAYARIIL As Decimal = 0
                        Dim BIAYADIAJUKAN As Decimal = 0
                        Dim BIAYADISETUJUI As Decimal = 0

                        If RecordLine.Count > 1 Then

                            For Each field As String In xLoop.Split(New String() {ControlChars.Tab}, StringSplitOptions.None)
                                Record.Add(field)

                                If Record.Count = 2 Then
                                    NOMORSEP = field
                                End If
                                If Record.Count = 3 Then
                                    Dim d As String
                                    Dim m As String
                                    Dim y As String

                                    y = field.Substring(0, 4)
                                    m = field.Substring(5, 2)
                                    d = field.Substring(8, 2)

                                    TANGGALVERIFIKASI = CDate(y & "-" & m & "-" & d)

                                End If
                                If Record.Count = 4 Then
                                    BIAYARIIL = field
                                End If
                                If Record.Count = 5 Then
                                    BIAYADIAJUKAN = field
                                End If
                                If Record.Count = 6 Then
                                    BIAYADISETUJUI = field
                                End If
                            Next

                        End If

                        If NOMORSEP <> String.Empty Then
                            Dim dsDetail = oCashinBPJSBayar.GetStructureDetail
                            With dsDetail
                                .KDCASHINBPJSBAYAR = ""
                                .SEQ = NOMOR
                                .NOSEP = NOMORSEP
                                .TANGGALVERIFIKASI = TANGGALVERIFIKASI
                                .BIAYARIILRS = BIAYARIIL
                                .BIAYADIAJUKAN = BIAYADIAJUKAN
                                .BIAYADISETUJUI = BIAYADISETUJUI
                                .REMARKS = ""
                                arrDetail.Add(dsDetail)
                            End With
                        End If

                        SplashScreenManager.Default.SetWaitFormCaption("Processing data " & sProcess & " of " & sTotal - 1 & "/" & vbCrLf & "Jumlah SEP " & NOMOR)

                        sProcess += 1

                        txtSUBTOTAL.Text += BIAYARIIL
                        txtADMIN.Text += BIAYADIAJUKAN
                        txtGRANDTOTAL.Text += BIAYADISETUJUI
                    Next

                    Dim tes = arrDetail
                    bindingSource.DataSource = arrDetail
                    grdDetail.DataSource = bindingSource

                Catch ex As Exception
                    MsgBox("Load Data" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                Finally
                    SplashScreenManager.CloseForm(False)
                    MsgBox("Export Selesai", MsgBoxStyle.Information, Me.Text)
                End Try
            End If
        End If
    End Sub
#End Region
End Class