Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmHasilLab
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private sKodeOrder As String
    Private sKDDOCTOR As String
    Private isLoad As Boolean = False
    Private oHasilLab As New Grouper.clsHasiLab
    Private oSalesOrder As New Sales.clsSalesOrderTransaksi

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal NoId As String, ByVal KDORDER As String, ByVal KDDOCTOR As String)
        oFormMode = FormMode
        sNoId = NoId
        sKodeOrder = KDORDER
        sKDDOCTOR = KDDOCTOR
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "Hasil Lab"

            'lMEMO.Text = HasilLab.MEMO & " *"
            'chkISACTIVE.Text = HasilLab.ISACTIVE
            'chkISDEFAULT.Text = HasilLab.ISDEFAULT

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = String.Empty
    End Sub
    Private Sub fn_ChangeFormState()
        fn_Reference()
        fn_Dokter()

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

        Dim dsTransaksi = oSalesOrder.GetData(sNoId)
        If dsTransaksi IsNot Nothing Then
            txtMEMO.Text = dsTransaksi.MEMO
        End If
    End Sub
    Private Sub fn_ViewMode(ByVal Status As Boolean)
        btnSaveNew.Enabled = Not Status
        btnSaveClose.Enabled = Not Status

        deDATE.Properties.ReadOnly = Status
        grdKDDOCTOR.Properties.ReadOnly = Status
        grdKDDOCTOR_PENANGGUNGJAWAB.Properties.ReadOnly = True
        grvTemplate.OptionsBehavior.ReadOnly = Status

        If sHargaApotik = False Then
            lMEMO.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            lMEMO.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub
    Private Sub fn_EmptyMe()
        deDATE.DateTime = Now
        grdKDDOCTOR_PENANGGUNGJAWAB.Text = sKDDOCTOR

        Dim oDoctor As New Reference.clsDoctor
        Dim dsDoctor = oDoctor.GetDataByStatus("LABORATORIUM")
        If dsDoctor IsNot Nothing Then
            grdKDDOCTOR.Text = dsDoctor.KDDOCTOR
        End If
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oHasilLab.GetDataDetailTransaksiFirst(sNoId)

            With ds
                deDATE.DateTime = .DATE
                grdKDDOCTOR_PENANGGUNGJAWAB.Text = .KDDOCTOR_PENANGGUNGJAWAB
                grdKDDOCTOR.Text = .KDDOCTOR

                'BindingSource.DataSource = oHasilLab.GetDataDetailTransaksi(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                'grdTemplate.DataSource = BindingSource
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If grdKDDOCTOR_PENANGGUNGJAWAB.Text = String.Empty Then
                grdKDDOCTOR_PENANGGUNGJAWAB.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDOCTOR_PENANGGUNGJAWAB.ErrorText = Statement.ErrorRequired

                grdKDDOCTOR_PENANGGUNGJAWAB.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDOCTOR.Text = String.Empty Then
                grdKDDOCTOR.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDOCTOR.ErrorText = Statement.ErrorRequired

                grdKDDOCTOR.Focus()
                fn_Validate = False
                Exit Function
            End If

            'If sKodeOrder = "" Then
            '    MsgBox("Dibutuhkan Nomor Order", MsgBoxStyle.Exclamation, Me.Text)
            '    fn_Validate = False
            '    Exit Function
            'End If

            grvTemplate.UpdateCurrentRow()

            If grvTemplate.RowCount < 2 Then
                MsgBox(Statement.ErrorDetail, MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** Template *****
            Dim arrDetail_Template = oHasilLab.GetStructureDetailList
            For i As Integer = 0 To grvTemplate.RowCount - 2
                Dim Lanjut As Boolean = False

                Dim dsDetail_Tempalte = oHasilLab.GetStructureDetail
                With dsDetail_Tempalte
                    Try
                        .DATECREATED = oHasilLab.GetDataTransaksi(sNoId).DATECREATED
                    Catch oErr As Exception
                        .DATECREATED = Now
                    End Try
                    .DATEUPDATED = Now
                    .DATE = deDATE.DateTime
                    .KDDOCTOR_PENANGGUNGJAWAB = grdKDDOCTOR_PENANGGUNGJAWAB.EditValue
                    .KDDOCTOR = grdKDDOCTOR.EditValue
                    .KDSOTRANSAKSI = sNoId
                    .KDITEM = IIf(String.IsNullOrEmpty(grvTemplate.GetRowCellValue(i, colKDITEM)), String.Empty, grvTemplate.GetRowCellValue(i, colKDITEM))
                    .NAMAPEMERIKSAAN = IIf(String.IsNullOrEmpty(grvTemplate.GetRowCellValue(i, colNAMAPEMERIKSAAN)), String.Empty, grvTemplate.GetRowCellValue(i, colNAMAPEMERIKSAAN))
                    .SEQ = i
                    .PEMERIKSAAN = IIf(String.IsNullOrEmpty(grvTemplate.GetRowCellValue(i, colPEMERIKSAAN)), String.Empty, grvTemplate.GetRowCellValue(i, colPEMERIKSAAN))
                    .HASIL = IIf(String.IsNullOrEmpty(grvTemplate.GetRowCellValue(i, colHASIL)), "", grvTemplate.GetRowCellValue(i, colHASIL))
                    .NILAIRUJUKAN = IIf(String.IsNullOrEmpty(grvTemplate.GetRowCellValue(i, colNILAIRUJUKAN)), String.Empty, grvTemplate.GetRowCellValue(i, colNILAIRUJUKAN))
                    .NILAIRUJUKAN_1 = 0
                    .NILAIRUJUKAN_2 = 0
                    .SATUAN = IIf(String.IsNullOrEmpty(grvTemplate.GetRowCellValue(i, colSATUAN)), String.Empty, grvTemplate.GetRowCellValue(i, colSATUAN))
                    .KETERANGAN = IIf(String.IsNullOrEmpty(grvTemplate.GetRowCellValue(i, colKETERANGAN)), String.Empty, grvTemplate.GetRowCellValue(i, colKETERANGAN))
                    .ISACTIVE = True
                    .KDUSER = sUserID
                    .SEQ_SO = IIf(String.IsNullOrEmpty(grvTemplate.GetRowCellValue(i, colSEQ_SO)), 0, grvTemplate.GetRowCellValue(i, colSEQ_SO))
                    .APPROVE = IIf(String.IsNullOrEmpty(grvTemplate.GetRowCellValue(i, colAPPROVE)), False, grvTemplate.GetRowCellValue(i, colAPPROVE))
                    .WARNA = IIf(String.IsNullOrEmpty(grvTemplate.GetRowCellValue(i, colWARNA)), String.Empty, grvTemplate.GetRowCellValue(i, colWARNA))

                    If .KDITEM = "" Then
                        Lanjut = False
                    Else
                        Lanjut = True
                    End If
                End With

                If Lanjut = True Then
                    arrDetail_Template.Add(dsDetail_Tempalte)
                End If
            Next

            If arrDetail_Template.Count > 0 Then
                If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                    Try
                        fn_Save = oHasilLab.InsertData(arrDetail_Template)
                    Catch oErr As Exception
                        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                    Try
                        fn_Save = oHasilLab.UpdateData(arrDetail_Template)
                    Catch oErr As Exception
                        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                End If

                If fn_Save = True Then
                    Dim oDigitalOrder As New Digital.clsR_Order
                    oDigitalOrder.UpdateStatus(sKodeOrder, "HASIL")

                    oSalesOrder.UpdateMemo(sNoId, txtMEMO.Text)

                End If
            Else
                fn_Save = False
                MsgBox("kditem masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Sub fn_Print()
        Try
            If sHargaApotik = False Then
                Dim ds = oSalesOrder.GetData(sNoId)

                If ds IsNot Nothing Then
                    Dim oRME As New RME.clsRME
                    sSIPNIP = ds.S_SO_TRANSAKSI_D_HASIL_LABORATORIUMs.FirstOrDefault.M_DOCTOR.SIP
                    sUSIA = oRME.GetUmurPasien(ds.DATE, ds.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)

                    Dim rpt As New xtraHasilLabSementara

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK
                    rpt.bindingSource.DataSource = ds

                    sCode = "Ok"

                    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                    printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
                End If
            Else
                Dim ds = oSalesOrder.GetData(sNoId)

                If ds IsNot Nothing Then
                    Dim oRME As New RME.clsRME
                    sSIPNIP = ds.S_SO_TRANSAKSI_D_HASIL_LABORATORIUMs.FirstOrDefault.M_DOCTOR.SIP
                    sUSIA = oRME.GetUmurPasien(ds.DATE, ds.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)

                    Dim rpt As New xtraHasilLabSementaraVersi2

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK
                    rpt.bindingSource.DataSource = ds

                    sCode = "Ok"

                    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                    printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
                End If
            End If

            'Dim oDataGrouper As New Admission.clsPendaftaran_Kunjungan
            'Dim oDokumen As New Digital.clsDigital_A_Dokumen
            'Dim oHasilLab As New Grouper.clsHasiLab

            'Dim dsHasil = From x In oHasilLab.GetDataDetailTransaksi(sNoId)
            '              Group x By x.KDSOTRANSAKSI, x.KDITEM Into seq = Sum(x.SEQ)

            'For Each xloop In dsHasil
            '    Dim dsHasilItem = oHasilLab.GetDataDetailTransaksikditem(xloop.KDSOTRANSAKSI, xloop.KDITEM)
            '    If dsHasilItem IsNot Nothing Then
            '        RichEditControl1.ResetText()

            '        Dim rpt As New xtraHasilLabAwal

            '        rpt.ShowPrintMarginsWarning = False
            '        rpt.Watermark.Text = sWATERMARK
            '        rpt.bindingSource.DataSource = dsHasilItem

            '        ' Export ke MemoryStream sebagai RTF
            '        Dim ms As New System.IO.MemoryStream()
            '        rpt.ExportToHtml(ms)

            '        ' Kembalikan posisi stream ke awal
            '        ms.Position = 0

            '        ' Load hasil RTF ke RichEditControl
            '        RichEditControl1.LoadDocument(ms, DevExpress.XtraRichEdit.DocumentFormat.Html)

            '        Dim dsIdentitas = oDataGrouper.GetDatabyKodeKunjungan(dsHasilItem.FirstOrDefault.S_SO_TRANSAKSI_H.KDKUNJUNGAN)

            '        If dsIdentitas IsNot Nothing Then
            '            Dim dsCek = oDokumen.GetData(xloop.KDSOTRANSAKSI & xloop.KDITEM)

            '            If dsCek Is Nothing Then
            '                Dim frmDigital_A_Dokumen As New frmDigital_A_Dokumen
            '                Try
            '                    frmDigital_A_Dokumen.LoadMe(FORM_MODE.FORM_MODE_ADD, sKodeOrder, RichEditControl1.RtfText, "HASILLABORATORIUM", dsIdentitas.KDIDENTITAS, xloop.KDSOTRANSAKSI & xloop.KDITEM)
            '                    frmDigital_A_Dokumen.ShowDialog(Me)
            '                Catch oErr As Exception
            '                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            '                Finally
            '                    If Not frmDigital_A_Dokumen Is Nothing Then frmDigital_A_Dokumen.Dispose()
            '                    frmDigital_A_Dokumen = Nothing
            '                End Try
            '            Else
            '                Dim frmDigital_A_Dokumen As New frmDigital_A_Dokumen
            '                Try
            '                    frmDigital_A_Dokumen.LoadMe(FORM_MODE.FORM_MODE_EDIT, sKodeOrder, RichEditControl1.RtfText, dsCek.NAMADOKUMEN, dsCek.KDIDENTITAS, dsCek.KDDKUMEN)
            '                    frmDigital_A_Dokumen.ShowDialog(Me)
            '                Catch oErr As Exception
            '                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            '                Finally
            '                    If Not frmDigital_A_Dokumen Is Nothing Then frmDigital_A_Dokumen.Dispose()
            '                    frmDigital_A_Dokumen = Nothing
            '                End Try
            '            End If
            '        Else
            '            sCode = ""
            '            MsgBox("Kunjungan Kosong", MsgBoxStyle.Exclamation, Me.Text)
            '        End If
            '    End If
            'Next
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try

        'Try
        '    Dim oHasilLab As New Grouper.clsHasiLab

        '    Dim dsHasil = From x In oHasilLab.GetDataDetailTransaksi(sNoId)
        '                  Group x By x.KDSOTRANSAKSI, x.KDITEM Into seq = Sum(x.SEQ)
        '    sCode = "OK"
        '    Dim oDigitalOrder As New Digital.clsR_Order
        '    oDigitalOrder.UpdateStatus(sKodeOrder, "HASIL")

        '    For Each xloop In dsHasil
        '        Dim oHasil As New Grouper.clsHasiLab
        '        Dim oRME As New RME.clsRME
        '        Dim dsCekHasilLab = oHasil.GetDataDetailTransaksikditem(xloop.KDSOTRANSAKSI, xloop.KDITEM)

        '        If dsCekHasilLab IsNot Nothing Then
        '            sUSIA = oRME.GetUmurPasien(dsCekHasilLab.FirstOrDefault.S_SO_TRANSAKSI_H.DATE, dsCekHasilLab.FirstOrDefault.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)

        '            Dim rpt As New xtraHasilLabSementara

        '            rpt.ShowPrintMarginsWarning = False
        '            rpt.ShowPrintMarginsWarning = False
        '            rpt.Watermark.Text = sWATERMARK
        '            rpt.bindingSource.DataSource = dsCekHasilLab
        '            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
        '            printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
        '        End If
        '    Next
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    'Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
    '    If fn_Validate() = False Then Exit Sub
    '    If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
    '    If fn_Save() = False Then
    '        MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
    '    Else
    '        MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
    '        sStatusSave = "NEW"
    '        Me.Close()
    '    End If
    'End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            fn_Print()

            If sCode = "" Then
                MsgBox("Gagal Simpan Hasil, Silahkan Ulangi", MsgBoxStyle.Exclamation, Me.Text)
            Else
                oFormMode = FORM_MODE.FORM_MODE_EDIT
                MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
                Me.Close()
            End If
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_Dokter()
        Dim oDoctor As New Reference.clsDoctor
        Try
            grdKDDOCTOR.Properties.DataSource = oDoctor.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

            grdKDDOCTOR_PENANGGUNGJAWAB.Properties.DataSource = oDoctor.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDOCTOR_PENANGGUNGJAWAB.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR_PENANGGUNGJAWAB.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Reference()
        Dim oReference As New Reference.clsHasilLabMaster
        Try
            grdKDHASILLAB.Properties.DataSource = oReference.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDHASILLAB.Properties.ValueMember = "KDHASILLAB"
            grdKDHASILLAB.Properties.DisplayMember = "JUDUL"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
#Region "Grid Method"
    Private Sub grvTemplate_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvTemplate.CellValueChanged
        If e.Column.Name = colPEMERIKSAAN.Name Then
            If grvTemplate.GetFocusedRowCellValue(colPEMERIKSAAN) IsNot Nothing Then
                grvTemplate.SetFocusedRowCellValue(colWARNA, "H")
            End If
        ElseIf e.Column.Name = colHASIL.Name Then
            Try
                If grvTemplate.GetFocusedRowCellValue(colHASIL) <> "" IsNot Nothing Then
                    Dim Hasil As Decimal = grvTemplate.GetFocusedRowCellValue(colHASIL).ToString.Replace(",", ".")

                    If CDec(grvTemplate.GetFocusedRowCellValue(colNILAIRUJUKAN_1)) > 0 Then
                        If Hasil < CDec(grvTemplate.GetFocusedRowCellValue(colNILAIRUJUKAN_1)) Then
                            grvTemplate.SetFocusedRowCellValue(colWARNA, "M")
                        Else
                            If Hasil > CDec(grvTemplate.GetFocusedRowCellValue(colNILAIRUJUKAN_2)) Then
                                grvTemplate.SetFocusedRowCellValue(colWARNA, "M")
                            Else
                                grvTemplate.SetFocusedRowCellValue(colWARNA, "H")
                            End If
                        End If
                    Else
                        If CDec(grvTemplate.GetFocusedRowCellValue(colNILAIRUJUKAN_2)) > 0 Then
                            If Hasil > CDec(grvTemplate.GetFocusedRowCellValue(colNILAIRUJUKAN_2)) Then
                                grvTemplate.SetFocusedRowCellValue(colWARNA, "M")
                            Else
                                grvTemplate.SetFocusedRowCellValue(colWARNA, "H")
                            End If
                        Else
                            grvTemplate.SetFocusedRowCellValue(colWARNA, "H")
                        End If
                    End If
                Else
                    grvTemplate.SetFocusedRowCellValue(colWARNA, "H")
                End If
            Catch ex As Exception
                grvTemplate.SetFocusedRowCellValue(colWARNA, "H")
            End Try
        End If
    End Sub
    Private Sub fn_LoadItemMaster(ByVal KDHASILLAB As String, ByVal NAMAPEMERIKSAAN As String, ByVal sSEQ As Integer)
        Dim CekValidasi As Boolean = False

        For i As Integer = 0 To grvTemplate.RowCount - 2
            If grvTemplate.GetRowCellValue(i, colKDITEM) = KDHASILLAB Then
                CekValidasi = True
                Exit For
            End If
        Next

        For Each xloop In oHasilLab.GetDataDetailTransaksikditem(sNoId, KDHASILLAB)
            grvTemplate.Focus()
            grvTemplate.AddNewRow()
            grvTemplate.SetFocusedRowCellValue(colNAMAPEMERIKSAAN, xloop.NAMAPEMERIKSAAN)
            grvTemplate.SetFocusedRowCellValue(colKDITEM, xloop.KDITEM)
            grvTemplate.SetFocusedRowCellValue(colAPPROVE, xloop.APPROVE)
            grvTemplate.SetFocusedRowCellValue(colPEMERIKSAAN, xloop.PEMERIKSAAN)
            grvTemplate.SetFocusedRowCellValue(colHASIL, xloop.HASIL)
            grvTemplate.SetFocusedRowCellValue(colNILAIRUJUKAN, xloop.NILAIRUJUKAN)
            grvTemplate.SetFocusedRowCellValue(colSATUAN, xloop.SATUAN)
            grvTemplate.SetFocusedRowCellValue(colKETERANGAN, xloop.KETERANGAN)
            grvTemplate.SetFocusedRowCellValue(colNILAIRUJUKAN_1, xloop.NILAIRUJUKAN_1)
            grvTemplate.SetFocusedRowCellValue(colNILAIRUJUKAN_2, xloop.NILAIRUJUKAN_2)
            grvTemplate.SetFocusedRowCellValue(colSEQ_SO, sSEQ)
            grvTemplate.UpdateCurrentRow()

            CekValidasi = True
        Next

        If CekValidasi = False Then
            Dim oHasilLabMaster As New Reference.clsHasilLabMaster

            For Each xloop In oHasilLabMaster.GetDataDetail(KDHASILLAB)
                grvTemplate.Focus()
                grvTemplate.AddNewRow()
                grvTemplate.SetFocusedRowCellValue(colNAMAPEMERIKSAAN, NAMAPEMERIKSAAN)
                grvTemplate.SetFocusedRowCellValue(colKDITEM, xloop.KDHASILLAB)
                grvTemplate.SetFocusedRowCellValue(colAPPROVE, xloop.ISGROUP)
                grvTemplate.SetFocusedRowCellValue(colPEMERIKSAAN, xloop.PEMERIKSAAN)
                grvTemplate.SetFocusedRowCellValue(colHASIL, xloop.HASIL)
                grvTemplate.SetFocusedRowCellValue(colNILAIRUJUKAN, xloop.NILAIRUJUKAN)
                grvTemplate.SetFocusedRowCellValue(colSATUAN, xloop.SATUAN)
                grvTemplate.SetFocusedRowCellValue(colKETERANGAN, xloop.KETERANGAN)
                grvTemplate.SetFocusedRowCellValue(colNILAIRUJUKAN_1, xloop.NILAI1)
                grvTemplate.SetFocusedRowCellValue(colNILAIRUJUKAN_2, xloop.NILAI2)
                grvTemplate.SetFocusedRowCellValue(colSEQ_SO, sSEQ)
                grvTemplate.UpdateCurrentRow()
            Next

        End If

        grvTemplate.Columns("NAMAPEMERIKSAAN").Group()
        grvTemplate.ExpandAllGroups()

        'grvDetail.Columns("NAMAPEMERIKSAAN").Group()
        'grvDetail.ExpandAllGroups()
    End Sub
    'Private Sub grdKDHASILLAB_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdKDHASILLAB.KeyPress
    '    If Asc(e.KeyChar) = 13 Then
    '        If grdKDHASILLAB.Text <> "" Then
    '            fn_LoadItemMaster(grdKDHASILLAB.EditValue)
    '        End If
    '    End If
    'End Sub
    Private Sub WarnaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WarnaToolStripMenuItem.Click
        grvTemplate.SetFocusedRowCellValue(colWARNA, "H")
    End Sub
    Private Sub MerahToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MerahToolStripMenuItem.Click
        grvTemplate.SetFocusedRowCellValue(colWARNA, "M")
    End Sub
    Private Sub SimpleButton1_Click() Handles SimpleButton1.Click
        Dim oMaster As New Reference.clsHasilLabMaster

        Dim dsSOTransaksi = oSalesOrder.GetDataByOrderLab(sKodeOrder)

        If dsSOTransaksi IsNot Nothing Then
            For Each xloop In oSalesOrder.GetDataDetail(dsSOTransaksi.KDSOTRANSAKSI)
                Dim dsMasterHasilByKdItem = oMaster.GetDataByKDITEM(xloop.KDITEM)

                If dsMasterHasilByKdItem IsNot Nothing Then
                    fn_LoadItemMaster(dsMasterHasilByKdItem.KDHASILLAB, dsMasterHasilByKdItem.JUDUL, xloop.SEQ)
                Else
                    MsgBox("Master Item Belum di Mapping Silahkan Klik Referensi Laboratorium untuk Tindakan " & xloop.M_ITEM.NMITEM2, MsgBoxStyle.Exclamation, Me.Text)
                End If
            Next
        Else
            For Each xloop In oSalesOrder.GetDataDetail(sNoId)
                Dim dsMasterHasilByKdItem = oMaster.GetDataByKDITEM(xloop.KDITEM)

                If dsMasterHasilByKdItem IsNot Nothing Then
                    fn_LoadItemMaster(dsMasterHasilByKdItem.KDHASILLAB, dsMasterHasilByKdItem.JUDUL, xloop.SEQ)
                Else
                    MsgBox("Master Item Belum di Mapping Silahkan Klik Referensi Laboratorium untuk Tindakan " & xloop.M_ITEM.NMITEM2, MsgBoxStyle.Exclamation, Me.Text)
                End If
            Next
            'MsgBox("Belum di Terima", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
#End Region
End Class