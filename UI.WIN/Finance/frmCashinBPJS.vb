Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports DevExpress.XtraSplashScreen
Imports System.Data.SqlClient

Public Class frmCashinBPJS
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oCashinBPJS As New Finance.clsCashinBPJS
    Private oSales As New Sales.clsSalesOrderTransaksi

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
            Me.Text = CashinBPJS.TITLE

            lKDCASHNPJS.Text = CashinBPJS.KDCASHINBPJS
            lDATE.Text = "Tanggal Klaim"
            lJUDULJASA.Text = CashinBPJS.KDJUDULJASA & " *"
            lCATEGORY.Text = CashinBPJS.CATEGORY

            tab1.Text = CashinBPJS.TAB_DETAIL
            tab3.Text = CashinBPJS.TAB_MEMO

            lTOTAL_TARIF.Text = CashinBPJS.TOTAL_TARIF
            lTARIF_RS.Text = CashinBPJS.TARIF_RS

            grvDetail.Columns("REMARKS").Caption = CashinBPJS.DETAIL_REMARKS

            grvKDJUDULJASA.Columns("MEMO").Caption = "Name Display"

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
        fn_LoadKDPAYMENTTYPE()

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
        grdKDJUDULJASA.Properties.ReadOnly = Status
        rbCATEGORY.Properties.ReadOnly = Status

        'txtMEMO.Properties.ReadOnly = Status

        txtTOTAL_TARIF.Properties.ReadOnly = Status
        txtTARIF_RS.Properties.ReadOnly = Status

        grvDetail.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDCASHINBPJS.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        grdKDJUDULJASA.ResetText()
        txtMEMO.ResetText()
        txtTOTAL_TARIF.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oCashinBPJS.GetData(sNoId)

            With ds
                txtKDCASHINBPJS.Text = .KDCASHINBPJS
                deDATE.DateTime = .DATE
                grdKDJUDULJASA.Text = .KDJUDULJASA
                txtMEMO.Text = .MEMO
                txtTOTAL_TARIF.Text = .GRANDTOTAL
                txtTARIF_RS.Text = .ROUND
                rbCATEGORY.SelectedIndex = .CATEGORY

                bindingSource.DataSource = oCashinBPJS.GetDataDetail.Where(Function(x) x.KDCASHINBPJS = sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = bindingSource

            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If grdKDJUDULJASA.Text = String.Empty Then
                grdKDJUDULJASA.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDJUDULJASA.ErrorText = Statement.ErrorRequired

                grdKDJUDULJASA.Focus()
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
            Dim ds = oCashinBPJS.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oCashinBPJS.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDCASHINBPJS = sNoId
                .DATE = deDATE.DateTime
                .CATEGORY = rbCATEGORY.SelectedIndex
                .KDJUDULJASA = IIf(String.IsNullOrEmpty(grdKDJUDULJASA.EditValue), String.Empty, grdKDJUDULJASA.EditValue)
                Try
                    .MEMO = oCashinBPJS.GetData(sNoId).MEMO
                Catch oErr As Exception
                    .MEMO = txtMEMO.Text
                End Try
                .SUBTOTAL = CDec(0)
                .ADMIN = CDec(0)
                .ROUND = CDec(txtTARIF_RS.Text)
                .GRANDTOTAL = CDec(txtTOTAL_TARIF.Text)
                .KDUSER = sUserID
            End With

            ' ***** DETIL *****
            Dim arrDetail = oCashinBPJS.GetStructureDetailList

            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oCashinBPJS.GetStructureDetail
                With dsDetail
                    .KDCASHINBPJS = ds.KDCASHINBPJS
                    .SEQ = i
                    .ADMISSION_DATE = grvDetail.GetRowCellValue(i, colADMISSION_DATE)
                    .DISCHARGE_DATE = grvDetail.GetRowCellValue(i, colDISCHARGE_DATE)
                    .NAME_DPJP = grvDetail.GetRowCellValue(i, colNAME_DPJP)
                    .NOSEP = grvDetail.GetRowCellValue(i, colSEP).ToString.Trim.ToUpper
                    .TOTAL_TARIF = CDec(grvDetail.GetRowCellValue(i, colTOTAL_TARIF))
                    .TARIF_RS = CDec(grvDetail.GetRowCellValue(i, colTARIF_RS))
                    .DIAGLIST = grvDetail.GetRowCellValue(i, colDIAGLIST)
                    .PROCLIST = grvDetail.GetRowCellValue(i, colPROCLIST)
                    .MRN = grvDetail.GetRowCellValue(i, colMRN)
                    .NAMA_PASIEN = grvDetail.GetRowCellValue(i, colNAMA_PASIEN)
                    .REMARKS = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colREMARKS)), "-", grvDetail.GetRowCellValue(i, colREMARKS))

                    .TARIF_PROSEDUR_NON_BEDAH = CDec(grvDetail.GetRowCellValue(i, colTARIF_PROSEDUR_NON_BEDAH))
                    .TARIF_PROSEDUR_BEDAH = CDec(grvDetail.GetRowCellValue(i, colTARIF_PROSEDUR_BEDAH))
                    .TARIF_KONSULTASI = CDec(grvDetail.GetRowCellValue(i, colTARIF_KONSULTASI))
                    .TARIF_TENAGAAHLI = CDec(grvDetail.GetRowCellValue(i, colTARIF_TENAGAAHLI))
                    .TARIF_KEPERAWATAN = CDec(grvDetail.GetRowCellValue(i, colTARIF_KEPERAWATAN))
                    .TARIF_PENUNJANG = CDec(grvDetail.GetRowCellValue(i, colTARIF_PENUNJANG))
                    .TARIF_PELAYANANDARAH = CDec(grvDetail.GetRowCellValue(i, colTARIF_PELAYANANDARAH))
                    .TARIF_REHABILTASI = CDec(grvDetail.GetRowCellValue(i, colTARIF_REHABILTASI))
                    .TARIF_KAMARAKOMODASI = CDec(grvDetail.GetRowCellValue(i, colTARIF_KAMARAKOMODASI))
                    .TARIF_RAWATINTENSIF = CDec(grvDetail.GetRowCellValue(i, colTARIF_RAWATINTENSIF))
                    .TARIF_OBAT = CDec(grvDetail.GetRowCellValue(i, colTARIF_OBAT))
                    .TARIF_ALKES = CDec(grvDetail.GetRowCellValue(i, colTARIF_ALKES))
                    .TARIF_BMHP = CDec(grvDetail.GetRowCellValue(i, colTARIF_BMHP))
                    .TARIF_SEWAALAT = CDec(grvDetail.GetRowCellValue(i, colTARIF_SEWAALAT))
                    .TARIF_KRONIS = CDec(grvDetail.GetRowCellValue(i, colTARIF_KRONIS))
                    .TARIF_KEMO = CDec(grvDetail.GetRowCellValue(i, colTARIF_KEMO))
                    .TARIF_LABORATORIUM = CDec(grvDetail.GetRowCellValue(i, colTARIF_LABORATORIUM))
                    .TARIF_RADIOLOGI = CDec(grvDetail.GetRowCellValue(i, colTARIF_RADIOLOGI))
                    .KDPENDAFTARAN = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colKDPENDAFTARAN)), "-", grvDetail.GetRowCellValue(i, colKDPENDAFTARAN))
                    .JASA_RS = CDec(grvDetail.GetRowCellValue(i, colJASA_RS))
                    .JASA_PENUNJANG = CDec(grvDetail.GetRowCellValue(i, colJASA_PENUNJANG))
                    .JASA_TINDAKANLAIN = CDec(grvDetail.GetRowCellValue(i, colJJASA_TINDAKANLAIN))
                    .JASA_SISA = CDec(grvDetail.GetRowCellValue(i, colJASA_SISA))
                    .JASA_MEDIS = CDec(grvDetail.GetRowCellValue(i, colJASA_MEDIS))
                    .JASA_PARAMEDIS = CDec(grvDetail.GetRowCellValue(i, colJASA_PARAMEDIS))
                    .DESKRIPSI_INACBG = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colDESKRIPSI_INACBG)), "", grvDetail.GetRowCellValue(i, colDESKRIPSI_INACBG))
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oCashinBPJS.InsertData(ds, arrDetail)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oCashinBPJS.UpdateData(ds, arrDetail)
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
    Private Sub fn_LoadKDPAYMENTTYPE()
        Dim oPAYMENTTYPE As New Reference.clsJudulJasa
        Try
            grdKDJUDULJASA.Properties.DataSource = oPAYMENTTYPE.GetData.Where(Function(x) x.ISACTIVE = True And x.CATEGORY = rbCATEGORY.SelectedIndex).ToList()
            grdKDJUDULJASA.Properties.ValueMember = "KDJUDULJASA"
            grdKDJUDULJASA.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDPAYMENTTYPE_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdKDJUDULJASA.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdKDJUDULJASA.ResetText()
        End If
    End Sub
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
                txtTOTAL_TARIF.ResetText()
                txtTARIF_RS.ResetText()

                Dim arrDetail = oCashinBPJS.GetStructureDetailList
                Dim sSudah As Integer = 0
                Dim sProcess As Integer = 0
                Dim sTotal As Integer = 0

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

                        Dim ADMISSION_DATE As DateTime = Now
                        Dim DISCHARGE_DATE As DateTime = Now
                        Dim NOMORSEP As String = String.Empty
                        Dim DPJP As String = String.Empty
                        Dim TOTAL_TARIF As Decimal = 0
                        Dim TARIF_RS As Decimal = 0
                        Dim DIAGLIST As String = String.Empty
                        Dim PROCLIST As String = String.Empty
                        Dim MRN As String = String.Empty
                        Dim TARIF_PROSEDUR_NON_BEDAH As Decimal = 0
                        Dim TARIF_PROSEDUR_BEDAH As Decimal = 0
                        Dim TARIF_KONSULTASI As Decimal = 0
                        Dim TARIF_TENAGAAHLI As Decimal = 0
                        Dim TARIF_KEPERAWATAN As Decimal = 0
                        Dim TARIF_PENUNJANG As Decimal = 0
                        Dim TARIF_PELAYANANDARAH As Decimal = 0
                        Dim TARIF_REHABILTASI As Decimal = 0
                        Dim TARIF_KAMARAKOMODASI As Decimal = 0
                        Dim TARIF_RAWATINTENSIF As Decimal = 0
                        Dim TARIF_OBAT As Decimal = 0
                        Dim TARIF_ALKES As Decimal = 0
                        Dim TARIF_BMHP As Decimal = 0
                        Dim TARIF_SEWAALAT As Decimal = 0
                        Dim TARIF_KRONIS As Decimal = 0
                        Dim TARIF_KEMO As Decimal = 0
                        Dim TARIF_LABORATORIUM As Decimal = 0
                        Dim TARIF_RADIOLOGI As Decimal = 0
                        Dim NAMAPASIEN As String = String.Empty
                        Dim JASA_RS As Decimal = 0
                        Dim DESKRIPSI_INACBG As String = String.Empty

                        If RecordLine.Count > 1 Then

                            For Each field As String In xLoop.Split(New String() {ControlChars.Tab}, StringSplitOptions.None)
                                Record.Add(field)

                                If Record.Count = 6 Then
                                    Dim tess = field

                                    Dim tes1 = field.Substring(0, 2)
                                    Dim tes2 = field.Substring(3, 2)
                                    Dim tes3 = field.Substring(6, 4)

                                    ADMISSION_DATE = field.Substring(3, 2) & "/" & field.Substring(0, 2) & "/" & field.Substring(6, 4)

                                End If

                                If Record.Count = 7 Then
                                    DISCHARGE_DATE = field.Substring(3, 2) & "/" & field.Substring(0, 2) & "/" & field.Substring(6, 4)
                                End If

                                If Record.Count = 12 Then
                                    DIAGLIST = field
                                End If

                                If Record.Count = 13 Then
                                    PROCLIST = field
                                End If

                                If Record.Count = 27 Then
                                    DESKRIPSI_INACBG = field
                                End If

                                If Record.Count = 39 Then
                                    TOTAL_TARIF = field
                                    JASA_RS = TOTAL_TARIF * (40 / 100)
                                End If

                                If Record.Count = 40 Then
                                    TARIF_RS = field
                                End If

                                If Record.Count = 46 Then
                                    NAMAPASIEN = field
                                End If

                                If Record.Count = 47 Then
                                    MRN = field
                                End If

                                If Record.Count = 50 Then
                                    DPJP = field
                                End If

                                If Record.Count = 51 Then
                                    NOMORSEP = field
                                End If

                                If Record.Count = 61 Then
                                    TARIF_PROSEDUR_NON_BEDAH = field
                                End If

                                If Record.Count = 62 Then
                                    TARIF_PROSEDUR_BEDAH = field
                                End If

                                If Record.Count = 63 Then
                                    TARIF_KONSULTASI = field
                                End If

                                If Record.Count = 64 Then
                                    TARIF_TENAGAAHLI = field
                                End If

                                If Record.Count = 65 Then
                                    TARIF_KEPERAWATAN = field
                                End If

                                If Record.Count = 66 Then
                                    TARIF_PENUNJANG = field
                                End If

                                If Record.Count = 67 Then
                                    TARIF_RADIOLOGI = field
                                End If

                                If Record.Count = 68 Then
                                    TARIF_LABORATORIUM = field
                                End If

                                If Record.Count = 69 Then
                                    TARIF_PELAYANANDARAH = field
                                End If

                                If Record.Count = 70 Then
                                    TARIF_REHABILTASI = field
                                End If

                                If Record.Count = 71 Then
                                    TARIF_KAMARAKOMODASI = field
                                End If

                                If Record.Count = 72 Then
                                    TARIF_RAWATINTENSIF = field
                                End If

                                If Record.Count = 73 Then
                                    TARIF_OBAT = field
                                End If

                                If Record.Count = 74 Then
                                    TARIF_ALKES = field
                                End If

                                If Record.Count = 75 Then
                                    TARIF_BMHP = field
                                End If

                                If Record.Count = 76 Then
                                    TARIF_SEWAALAT = field
                                End If

                                If Record.Count = 77 Then
                                    TARIF_KRONIS = field
                                End If

                                If Record.Count = 78 Then
                                    TARIF_KEMO = field
                                End If
                            Next

                        End If

                        If NOMORSEP <> String.Empty Then
                            Dim dsDetail = oCashinBPJS.GetStructureDetail
                            With dsDetail
                                .KDCASHINBPJS = ""
                                .SEQ = 0
                                .ADMISSION_DATE = ADMISSION_DATE
                                .DISCHARGE_DATE = DISCHARGE_DATE
                                .NAME_DPJP = DPJP.ToString.Trim
                                .NOSEP = NOMORSEP
                                .TOTAL_TARIF = TOTAL_TARIF
                                .TARIF_RS = TARIF_RS
                                .DIAGLIST = DIAGLIST.ToString.Trim
                                .PROCLIST = PROCLIST.ToString.Trim
                                .MRN = MRN
                                .NAMA_PASIEN = NAMAPASIEN
                                .TARIF_PROSEDUR_NON_BEDAH = TARIF_PROSEDUR_NON_BEDAH
                                .TARIF_PROSEDUR_BEDAH = TARIF_PROSEDUR_BEDAH
                                .TARIF_KONSULTASI = TARIF_KONSULTASI
                                .TARIF_TENAGAAHLI = TARIF_TENAGAAHLI
                                .TARIF_KEPERAWATAN = TARIF_KEPERAWATAN
                                .TARIF_PENUNJANG = TARIF_PENUNJANG
                                .TARIF_PELAYANANDARAH = TARIF_PELAYANANDARAH
                                .TARIF_REHABILTASI = TARIF_REHABILTASI
                                .TARIF_KAMARAKOMODASI = TARIF_KAMARAKOMODASI
                                .TARIF_RAWATINTENSIF = TARIF_RAWATINTENSIF
                                .TARIF_OBAT = TARIF_OBAT
                                .TARIF_ALKES = TARIF_ALKES
                                .TARIF_BMHP = TARIF_BMHP
                                .TARIF_SEWAALAT = TARIF_SEWAALAT
                                .TARIF_KRONIS = TARIF_KRONIS
                                .TARIF_KEMO = TARIF_KEMO
                                .TARIF_LABORATORIUM = TARIF_LABORATORIUM
                                .TARIF_RADIOLOGI = TARIF_RADIOLOGI
                                .JASA_RS = JASA_RS
                                .JASA_PENUNJANG = 0
                                .JASA_TINDAKANLAIN = 0
                                .JASA_SISA = 0
                                .JASA_MEDIS = 0
                                .JASA_PARAMEDIS = 0
                                .REMARKS = "-"
                                .DESKRIPSI_INACBG = DESKRIPSI_INACBG

                                arrDetail.Add(dsDetail)

                            End With
                        End If

                        SplashScreenManager.Default.SetWaitFormCaption("Processing data " & sProcess & " of " & sTotal - 1 & "")

                        sProcess += 1

                        txtTOTAL_TARIF.Text += TOTAL_TARIF
                        txtTARIF_RS.Text += TARIF_RS
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
    Private Sub rbCATEGORY_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rbCATEGORY.SelectedIndexChanged
        fn_LoadKDPAYMENTTYPE()
    End Sub
#End Region
End Class