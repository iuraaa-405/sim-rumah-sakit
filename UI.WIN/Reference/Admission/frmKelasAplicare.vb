Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq

Public Class frmKelasAplicare
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oKelasAplicare As New Reference.clsKelasAplicare
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
            Me.Text = KelasAplicare.TITLE

            lKDKelasAplicare.Text = KelasAplicare.KDKELASAPLICARE & " *"
            lMEMO.Text = KelasAplicare.MEMO & " *"
            chkISACTIVE.Text = KelasAplicare.ISACTIVE
            chkISDEFAULT.Text = KelasAplicare.ISDEFAULT

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtMEMO.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKelasAplicare()
        fn_LoadKDDEPARTMENT()

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
        btnKetersediaanKamar.Enabled = Not Status

        txtKDKELASAPLICARE.Properties.ReadOnly = True
        txtMEMO.Properties.ReadOnly = True
        chkISACTIVE.Properties.ReadOnly = Status
        chkISDEFAULT.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDKelasAplicare.ResetText()
        txtMEMO.ResetText()

        chkISACTIVE.Checked = True
        chkISDEFAULT.Checked = False

        txtMEMO.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oKelasAplicare.GetData(sNoId)

            With ds
                txtKDKELASAPLICARE.Text = sNoId
                txtMEMO.Text = .MEMO
                chkISACTIVE.Checked = .ISACTIVE
                chkISDEFAULT.Checked = .ISDEFAULT

                BindingSource.DataSource = oKelasAplicare.GetDataDetail_UOM.Where(Function(x) x.KDKELASAPLICARE = sNoId).OrderBy(Function(x) x.M_DEPARTMENT.NAME_DISPLAY).ToList()
                grdDetail_UOM.DataSource = BindingSource

            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtKDKelasAplicare.Text = String.Empty Then
                txtKDKelasAplicare.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDKelasAplicare.ErrorText = Statement.ErrorRequired

                txtKDKelasAplicare.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtMEMO.Text = String.Empty Then
                txtMEMO.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtMEMO.ErrorText = Statement.ErrorRequired

                txtMEMO.Focus()
                fn_Validate = False
                Exit Function
            End If
            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                If oKelasAplicare.IsExist(txtMEMO.Text.ToUpper.Trim) = True Then
                    txtMEMO.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    txtMEMO.ErrorText = Statement.ErrorRegistered

                    txtMEMO.Focus()
                    fn_Validate = False
                    Exit Function
                End If
            Else
                If txtMEMO.Text.Trim.ToUpper <> oKelasAplicare.GetData(sNoId).MEMO Then
                    If oKelasAplicare.IsExist(txtMEMO.Text.ToUpper.Trim) = True Then
                        txtMEMO.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                        txtMEMO.ErrorText = Statement.ErrorRegistered

                        txtMEMO.Focus()
                        fn_Validate = False
                        Exit Function
                    End If
                End If
            End If


            'If grvDetail_UOM.RowCount < 2 Then
            '    MsgBox("Dibutuhkan Detil", MsgBoxStyle.Exclamation, Me.Text)
            '    fn_Validate = False
            '    Exit Function
            'End If

            For iLoop As Integer = 0 To grvDetail_UOM.RowCount - 2
                If grvDetail_UOM.GetRowCellValue(iLoop, colTERSEDIA) > grvDetail_UOM.GetRowCellValue(iLoop, colKAPASITAS) Then
                    Dim oDepartment As New Reference.clsDepartment
                    MsgBox("Tersedia Ruangan " & oDepartment.GetData(grvDetail_UOM.GetRowCellValue(iLoop, colKDUOM)).NAME_DISPLAY & " Lebih besar dari Kapasitas", MsgBoxStyle.Exclamation, Me.Text)
                    fn_Validate = False
                    Exit Function
                End If
            Next
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oKelasAplicare.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oKelasAplicare.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDKELASAPLICARE = txtKDKELASAPLICARE.Text.ToString.Trim.ToUpper
                .MEMO = txtMEMO.Text.Trim.ToUpper
                .ISACTIVE = chkISACTIVE.Checked
                .ISDEFAULT = chkISDEFAULT.Checked

            End With

            ' ***** Satuan *****
            Dim arrDetail_UOM = oKelasAplicare.GetStructureDetail_UOMList
            For i As Integer = 0 To grvDetail_UOM.RowCount - 2
                Dim dsDetail_UOM = oKelasAplicare.GetStructureDetail_UOM
                With dsDetail_UOM
                    Try
                        .DATECREATED = oKelasAplicare.GetData(sNoId).DATECREATED
                    Catch oErr As Exception
                        .DATECREATED = Now
                    End Try
                    .DATEUPDATED = Now
                    .KDUPDATE_APLICARE = IIf(String.IsNullOrEmpty(grvDetail_UOM.GetRowCellValue(i, colKDUPDATE_APLICARE)), "", grvDetail_UOM.GetRowCellValue(i, colKDUPDATE_APLICARE))
                    .KDKELASAPLICARE = txtKDKELASAPLICARE.Text.ToString.Trim.ToUpper
                    .KDDEPARTMENT = grvDetail_UOM.GetRowCellValue(i, colKDUOM)
                    .KAPASITAS = grvDetail_UOM.GetRowCellValue(i, colKAPASITAS)
                    .TERSEDIA = grvDetail_UOM.GetRowCellValue(i, colTERSEDIA)
                    .TERSEDIA_LAKI = grvDetail_UOM.GetRowCellValue(i, colTERSEDIA_LAKI)
                    .TERSEDIA_PEREMPUAN = grvDetail_UOM.GetRowCellValue(i, colTERSEDIA_PEREMPUAN)
                    .TERSEDIA_LAKIPEREMPUAN = grvDetail_UOM.GetRowCellValue(i, colTERSEDIA_LAKIPEREMPUAN)
                    .ISAPLICARE = grvDetail_UOM.GetRowCellValue(i, colIAPLICARE)

                End With
                arrDetail_UOM.Add(dsDetail_UOM)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oKelasAplicare.InsertData(ds, arrDetail_UOM)
                Catch oErr As Exception
                    MsgBox("Add Ketersedian Tempat Tidur gagal" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oKelasAplicare.UpdateData(ds, arrDetail_UOM)
                    fn_UpdateKetersediaan()
                Catch oErr As Exception
                    MsgBox("Update Ketersedian Tempat Tidur gagal" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Sub fn_RuanganBaru()
        Try
            If grvDetail_UOM.GetFocusedRowCellValue(colKDUPDATE_APLICARE) <> "" Then
                Dim jsonRequest As String = String.Empty

                jsonRequest = "{ "
                jsonRequest &= """kodekelas"": """ & txtKDKELASAPLICARE.Text & ""","
                jsonRequest &= """koderuang"": """ & grvDetail_UOM.GetFocusedRowCellValue(colKDUPDATE_APLICARE) & ""","
                jsonRequest &= """namaruang"": """ & oKelasAplicare.GetDataByDepartment(grvDetail_UOM.GetFocusedRowCellValue(colKDUOM)).NAME_DISPLAY & ""","
                jsonRequest &= """kapasitas"": """ & grvDetail_UOM.GetFocusedRowCellValue(colKAPASITAS) & ""","
                jsonRequest &= """tersedia"": """ & grvDetail_UOM.GetFocusedRowCellValue(colTERSEDIA) & ""","
                jsonRequest &= """tersediapria"": """ & grvDetail_UOM.GetFocusedRowCellValue(colTERSEDIA_LAKI) & ""","
                jsonRequest &= """tersediawanita"": """ & grvDetail_UOM.GetFocusedRowCellValue(colTERSEDIA_PEREMPUAN) & ""","
                jsonRequest &= """tersediapriawanita"": """ & grvDetail_UOM.GetFocusedRowCellValue(colTERSEDIA_LAKIPEREMPUAN) & """"
                jsonRequest &= "} "

                Dim oSetKoneksi As New Brigging.clsSetKoneksi
                Dim dsSetKoneksi = oSetKoneksi.RuanganBaruNew(sAplicare_Url, sAplicare_ConsId, sAplicare_SecreatKey, sAplicare_UserKey, sAplicare_PPK, jsonRequest)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)
                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = allData("metadata")("code").ToString
                    messageResponse = allData("metadata")("message").ToString

                    If CodeResponse = 1 Then
                        grvDetail_UOM.SetFocusedRowCellValue(colIAPLICARE, True)
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Information, Me.Text)
                    Else
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    MsgBox("Koneksi Kosong", MsgBoxStyle.Exclamation, "Update Aplicare Gagal !!!")
                End If
            Else
                MsgBox("Kode Aplicare Kosong", MsgBoxStyle.Exclamation, Me.Text)
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, "Update Aplicare Gagal !!!")
        End Try
    End Sub
    Private Sub fn_HapusBaru(ByVal KodeRuangan As String)
        Try
            Dim jsonRequest As String = String.Empty

            jsonRequest = "{ "
            jsonRequest &= """kodekelas"": """ & txtKDKELASAPLICARE.Text & ""","
            jsonRequest &= """koderuang"": """ & KodeRuangan & """"
            jsonRequest &= "} "

            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim dsSetKoneksi = oSetKoneksi.RuanganHapusNew(sAplicare_Url, sAplicare_ConsId, sAplicare_SecreatKey, sAplicare_UserKey, sAplicare_PPK, jsonRequest)

            If dsSetKoneksi <> "" Then
                Dim allData = JObject.Parse(dsSetKoneksi)
                Dim CodeResponse As String = String.Empty
                Dim messageResponse As String = String.Empty

                CodeResponse = allData("metadata")("code").ToString
                messageResponse = allData("metadata")("message").ToString

                If CodeResponse = 1 Then
                    grvDetail_UOM.SetFocusedRowCellValue(colIAPLICARE, False)
                    MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Information, Me.Text)
                Else
                    MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                MsgBox("Brigging Aplicare Tidak Aktif", MsgBoxStyle.Exclamation, "Update Aplicare Gagal !!!")
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, "Update Aplicare Gagal !!!")
        End Try
    End Sub
    Private Sub fn_UpdateKetersediaan()
        For i As Integer = 0 To grvDetail_UOM.RowCount - 2
            Try
                If grvDetail_UOM.GetRowCellValue(i, colKDUPDATE_APLICARE) <> "" Then
                    Dim dsDeparment = oKelasAplicare.GetDataByDepartment(grvDetail_UOM.GetRowCellValue(i, colKDUOM))
                    If dsDeparment IsNot Nothing Then
                        Dim jsonRequest As String = String.Empty

                        jsonRequest = "{ "
                        jsonRequest &= """kodekelas"": """ & txtKDKELASAPLICARE.Text & ""","
                        jsonRequest &= """koderuang"": """ & grvDetail_UOM.GetRowCellValue(i, colKDUPDATE_APLICARE) & ""","
                        jsonRequest &= """namaruang"": """ & dsDeparment.NAME_DISPLAY & ""","
                        jsonRequest &= """kapasitas"": """ & grvDetail_UOM.GetRowCellValue(i, colKAPASITAS) & ""","
                        jsonRequest &= """tersedia"": """ & grvDetail_UOM.GetRowCellValue(i, colTERSEDIA) & ""","
                        jsonRequest &= """tersediapria"": """ & grvDetail_UOM.GetRowCellValue(i, colTERSEDIA_LAKI) & ""","
                        jsonRequest &= """tersediawanita"": """ & grvDetail_UOM.GetRowCellValue(i, colTERSEDIA_PEREMPUAN) & ""","
                        jsonRequest &= """tersediapriawanita"": """ & grvDetail_UOM.GetRowCellValue(i, colTERSEDIA_LAKIPEREMPUAN) & """"
                        jsonRequest &= "} "

                        Dim oSetKoneksi As New Brigging.clsSetKoneksi
                        Dim dsSetKoneksi = oSetKoneksi.UpdateKetersediaanTempatTidurNew(sAplicare_Url, sAplicare_ConsId, sAplicare_SecreatKey, sAplicare_UserKey, sAplicare_PPK, jsonRequest)

                        If dsSetKoneksi <> "" Then
                            Dim allData = JObject.Parse(dsSetKoneksi)
                            Dim CodeResponse As String = String.Empty
                            Dim messageResponse As String = String.Empty

                            CodeResponse = allData("metadata")("code").ToString
                            messageResponse = allData("metadata")("message").ToString

                            If CodeResponse = 1 Then
                                MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Information, Me.Text)

                                Dim oUpdateAplicare As New Sales.clsUpdateAplicare

                                ' ***** HEADER *****
                                Dim ds = oUpdateAplicare.GetStructureHeader
                                With ds
                                    .DATECREATED = Now
                                    .DATEUPDATED = Now
                                    .KDUPDATE_APLICARE = 0
                                    .REQUEST = jsonRequest
                                    .RESPON = dsSetKoneksi
                                    .KDUSER = sUserID
                                End With
                            Else
                                MsgBox(CodeResponse & " - " & messageResponse & " Kelas " & txtMEMO.Text & " Ruangan " & oKelasAplicare.GetDataByDepartment(grvDetail_UOM.GetRowCellValue(i, colKDUOM)).NAME_DISPLAY, MsgBoxStyle.Exclamation, Me.Text)
                            End If
                        Else
                            MsgBox("Brigging Aplicare Tidak Aktif " & " Kelas " & txtMEMO.Text & " Ruangan " & oKelasAplicare.GetDataByDepartment(grvDetail_UOM.GetRowCellValue(i, colKDUOM)).NAME_DISPLAY, MsgBoxStyle.Exclamation, "Update Aplicare Gagal !!!")
                        End If
                    End If
                End If
            Catch oErr As Exception
                MsgBox("Looping Aplicare" & Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, "Update Aplicare Gagal !!!")
            End Try
        Next
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail_UOM.DeleteSelectedRows()
    End Sub
    Private Sub RuanganBaruToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RuanganBaruToolStripMenuItem.Click
        If grvDetail_UOM.GetFocusedRowCellValue(colKDUOM) IsNot Nothing Then
            If oKelasAplicare.GetDataDetail_KelasDepartment(txtKDKELASAPLICARE.Text, grvDetail_UOM.GetFocusedRowCellValue(colKDUOM)) IsNot Nothing Then
                Dim sResult = MsgBox("Apakah Yakin Ruangan akan di Simpan Aplicare?", MsgBoxStyle.YesNo, Me.Text)

                If sResult = Windows.Forms.DialogResult.Yes Then
                    fn_RuanganBaru()
                End If
            Else
                MsgBox("Data Rumah Sakit tidak ditemukan, Silahkan Simpan Ketersedian terlebih dahulu", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Else
            MsgBox("Silahkan Pilih Ruangan terlebih dahulu", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub HapusRuanganToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HapusRuanganToolStripMenuItem.Click
        If grvDetail_UOM.GetFocusedRowCellValue(colKDUOM) IsNot Nothing Then
            Dim sResult = MsgBox("Apakah Yakin Ruangan akan di Hapus Aplicare?", MsgBoxStyle.YesNo, Me.Text)

            If sResult = Windows.Forms.DialogResult.Yes Then
                If grvDetail_UOM.GetFocusedRowCellValue(colKDUPDATE_APLICARE) <> "" Then
                    fn_HapusBaru(grvDetail_UOM.GetFocusedRowCellValue(colKDUPDATE_APLICARE))
                Else
                    MsgBox("Kode Kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If
        Else
            MsgBox("Silahkan Pilih Ruangan terlebih dahulu", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
                If btnKetersediaanKamar.Enabled = True Then
                    btnKetersediaanKamar_Click()
                End If
        End Select
    End Sub
    Private Sub btnKetersediaanKamar_Click() Handles btnKetersediaanKamar.ItemClick
        Dim frmReportAplicares As New frmReportAplicares
        Try
            frmReportAplicares.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
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
    Private Sub grdCARI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdCARI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtKDKELASAPLICARE.Text = grdCARI.EditValue
            txtMEMO.Text = grdCARI.Text
        End If
    End Sub
#End Region
#Region "Grid Method"
    Private Sub grdDetail_UOM_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDetail_UOM.CellValueChanged
        If e.Column.Name = colKDUOM.Name Then
            Dim oDepartment As New Reference.clsDepartment
            Try
                If grvDetail_UOM.GetFocusedRowCellValue(colKDUOM) IsNot Nothing Then
                    Dim ds = oDepartment.GetData(grvDetail_UOM.GetFocusedRowCellValue(colKDUOM))

                    If ds IsNot Nothing Then
                        Dim cek As Integer = 0
                        For i As Integer = 0 To grvDetail_UOM.RowCount - 2
                            If grvDetail_UOM.GetRowCellValue(i, colKDUOM) = grvDetail_UOM.GetFocusedRowCellValue(colKDUOM) Then
                                cek = cek + 1
                            End If
                        Next

                        If cek <> 0 Then
                            MsgBox("Ruangan sudah ada di list!", MsgBoxStyle.Critical, Me.Text)
                            grvDetail_UOM.CancelUpdateCurrentRow()
                        End If
                    Else
                        MsgBox(Statement.ErrorUOM, MsgBoxStyle.Exclamation, Me.Text)

                        grvDetail_UOM.CancelUpdateCurrentRow()
                    End If
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf e.Column.Name = colKAPASITAS.Name Or e.Column.Name = colTERSEDIA_LAKI.Name Or e.Column.Name = colTERSEDIA_PEREMPUAN.Name Or e.Column.Name = colTERSEDIA_LAKIPEREMPUAN.Name Then
            grvDetail_UOM.SetFocusedRowCellValue(colTERSEDIA, grvDetail_UOM.GetFocusedRowCellValue(colTERSEDIA_LAKI) + grvDetail_UOM.GetFocusedRowCellValue(colTERSEDIA_PEREMPUAN) + grvDetail_UOM.GetFocusedRowCellValue(colTERSEDIA_LAKIPEREMPUAN))

        ElseIf e.Column.Name = colTERSEDIA.Name Then
            If grvDetail_UOM.GetFocusedRowCellValue(colTERSEDIA_LAKI) + grvDetail_UOM.GetFocusedRowCellValue(colTERSEDIA_PEREMPUAN) + grvDetail_UOM.GetFocusedRowCellValue(colTERSEDIA_LAKIPEREMPUAN) > grvDetail_UOM.GetFocusedRowCellValue(colKAPASITAS) Then
                MsgBox("Tersedia tidak boleh lebih besar dari kapasitas", MsgBoxStyle.Exclamation, Me.Text)
                grvDetail_UOM.CancelUpdateCurrentRow()
            End If
        End If
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadKDDEPARTMENT()
        Dim oDepartment As New Reference.clsDepartment
        Try
            grdUOM.DataSource = oDepartment.GetData.Where(Function(x) x.ISACTIVE = True And x.ISRUANGRAWAT = True).ToList()
            grdUOM.ValueMember = "KDDEPARTMENT"
            grdUOM.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKelasAplicare()
        Try
            Dim oSetKoneksi As New Brigging.clsSetKoneksi

            Dim dsSetKoneksi = oSetKoneksi.GetDataAplicareReferensiKelasRawat(sAplicare_Url, sAplicare_ConsId, sAplicare_SecreatKey, sAplicare_UserKey)

            If dsSetKoneksi <> "" Then
                Try
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim table As DataTable

                    table = New DataTable("M_KELAS")
                    table.Columns.Add("kode")
                    table.Columns.Add("nama")

                    For Each item In allData("response")("list")
                        table.Rows.Add(New String() {item("kodekelas"), item("namakelas")})
                    Next

                    grdCARI.Properties.DataSource = table

                    grdCARI.Properties.ValueMember = "kode"
                    grdCARI.Properties.DisplayMember = "nama"

                    grdCARI.ShowPopup()

                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message & vbCrLf & vbCrLf & "Result : " & vbCrLf & dsSetKoneksi, MsgBoxStyle.Exclamation, Me.Text)
                End Try

            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnHapusAplicare_Click(sender As Object, e As EventArgs) Handles btnHapusAplicare.Click
        If txtKODELAIN.Text <> "" Then
            Dim sResult = MsgBox("Apakah Yakin Ruangan akan di Hapus Aplicare?", MsgBoxStyle.YesNo, Me.Text)

            If sResult = Windows.Forms.DialogResult.Yes Then
                fn_HapusBaru(txtKODELAIN.Text)
            End If
        Else
            MsgBox("Silahkan Pilih Ruangan terlebih dahulu", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub

    Private Sub BedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BedToolStripMenuItem.Click
        If grvDetail_UOM.GetFocusedRowCellValue(colKDUPDATE_APLICARE) IsNot Nothing Then
            Dim oKelasAplicareBed As New Reference.clsKelasAplicareBed

            Dim ds = oKelasAplicareBed.GetDatadefaultKelasAplicare(grvDetail_UOM.GetFocusedRowCellValue(colKDUPDATE_APLICARE))
            If ds Is Nothing Then
                Dim frmKelasAplicareBed As New frmKelasAplicareBed
                Try
                    frmKelasAplicareBed.LoadMe(FORM_MODE.FORM_MODE_ADD, grvDetail_UOM.GetFocusedRowCellValue(colKDUPDATE_APLICARE))
                    frmKelasAplicareBed.ShowDialog(Me)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                Finally
                    If Not frmKelasAplicareBed Is Nothing Then frmKelasAplicareBed.Dispose()
                    frmKelasAplicareBed = Nothing
                End Try
            Else
                Dim frmKelasAplicareBed As New frmKelasAplicareBed
                Try
                    frmKelasAplicareBed.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDUPDATE_APLICARE)
                    frmKelasAplicareBed.ShowDialog(Me)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                Finally
                    If Not frmKelasAplicareBed Is Nothing Then frmKelasAplicareBed.Dispose()
                    frmKelasAplicareBed = Nothing
                End Try
            End If
        Else
            MsgBox("Silahkan Pilih Ruangan terlebih dahulu", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub

#End Region
End Class