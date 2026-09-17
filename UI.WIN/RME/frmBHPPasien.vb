Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmBHPPasien
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oBHPPasien As New Inventory.clsBHPPasien
    Private sKDKUNJUNGAN As String = String.Empty
    Private sPENJAMIN As String = String.Empty
    Private sKDCUSTOMER As String = String.Empty
    Private sKDDOCTOR As String = String.Empty
    Private sKDDEPARTMENT As String = String.Empty

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal kddepartment As String, ByVal KDIDENTITAS As String, ByVal NoId As String)
        oFormMode = FormMode
        sNoId = NoId
        sKDDEPARTMENT = kddepartment

        Dim dsCekIdentitas = oBHPPasien.GetDataIdentitas(KDIDENTITAS)
        If dsCekIdentitas IsNot Nothing Then
            sPENJAMIN = dsCekIdentitas.KDDAFTAR_L1_NAMA
            sKDKUNJUNGAN = dsCekIdentitas.KDKUNJUNGAN
            sKDCUSTOMER = dsCekIdentitas.KDCUSTOMER
            sKDDOCTOR = dsCekIdentitas.KDDOCTOR
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "BHP Pasien"

            lDATE.Text = "Tanggal * :"

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'sCode = txtKDBHPPasien.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadITEM()
        fn_LoadUOM()
        fn_LoadSIGNA()
        fn_LoadCARAPAKAI()
        fn_LoadTEMPLATE()
        fn_LoadKDWAREHOUSE()

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

        deDATE.Properties.ReadOnly = Status
        grdKDWAREHOUSE.Properties.ReadOnly = Status
        txtMEMO.Properties.ReadOnly = Status
        grvDetailResep.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDBHPPASIEN.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        grdKDWAREHOUSE.ResetText()
        txtMEMO.ResetText()

        grvDetailResep.OptionsSelection.MultiSelect = True
        grvDetailResep.SelectAll()
        grvDetailResep.DeleteSelectedRows()
        grvDetailResep.OptionsSelection.MultiSelect = False
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oBHPPasien.GetData(sNoId)

            With ds
                txtKDBHPPASIEN.Text = .KDBHPPASIEN
                deDATE.DateTime = .DATE
                grdKDWAREHOUSE.Text = .KDWAREHOUSE
                txtMEMO.Text = .MEMO

                fn_LoadITEM()

                bindingSource.DataSource = oBHPPasien.GetDataDetail(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetailResep.DataSource = bindingSource
            End With

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If sKDKUNJUNGAN = "" Then
                MsgBox("Kode Kunjungan Kosong", MsgBoxStyle.Exclamation, Me.Text)
                txtKDBHPPASIEN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDBHPPASIEN.ErrorText = Statement.ErrorRequired

                txtKDBHPPASIEN.Focus()
                fn_Validate = False
                Exit Function
            End If

            If grdKDWAREHOUSE.Text = "" Then
                MsgBox("Diagnosa Depo", MsgBoxStyle.Exclamation, Me.Text)
                grdKDWAREHOUSE.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDWAREHOUSE.ErrorText = Statement.ErrorRequired

                grdKDWAREHOUSE.Focus()
                fn_Validate = False
                Exit Function
            End If

            grvDetailResep.UpdateCurrentRow()

            If grvDetailResep.RowCount < 2 Then
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
            ' ***** HEADER *****
            Dim ds = oBHPPasien.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oBHPPasien.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .KDBHPPASIEN = sNoId
                .KDKUNJUNGAN = sKDKUNJUNGAN
                .KATEGORI = "BHPPASIEN"
                .KDUSER = sUserID
                .MEMO = txtMEMO.Text
                .KDWAREHOUSE = grdKDWAREHOUSE.EditValue
            End With


            Dim oItem As New Reference.clsItem
            Dim oUom As New Reference.clsUOM
            Dim oSiga As New Reference.clsSigna
            Dim oCaraPakai As New Reference.clsCaraPakai

            ' ***** DETIL *****
            Dim arrDetail = oBHPPasien.GetStructureDetailList
            For i As Integer = 0 To grvDetailResep.RowCount - 2
                Dim dsDetail = oBHPPasien.GetStructureDetail
                With dsDetail
                    .DATECREATED = ds.DATECREATED
                    .DATEUPDATED = ds.DATEUPDATED
                    .SEQ = i
                    .KDBHPPASIEN = ds.KDBHPPASIEN
                    Dim dsItem = oItem.GetData(grvDetailResep.GetRowCellValue(i, colKDITEM))
                    If dsItem IsNot Nothing Then
                        .NAMAOBAT = dsItem.NMITEM2
                    Else
                        .NAMAOBAT = ""
                    End If
                    Dim dsUom = oUom.GetData(grvDetailResep.GetRowCellValue(i, colKDUOM))
                    If dsUom IsNot Nothing Then
                        .SATUAN = dsUom.MEMO
                    Else
                        .SATUAN = ""
                    End If
                    Dim dsSigna = oSiga.GetData(grvDetailResep.GetRowCellValue(i, colKDSIGNA))
                    If dsSigna IsNot Nothing Then
                        .SIGNA = dsSigna.MEMO
                    Else
                        .SIGNA = ""
                    End If
                    Dim dsCaraPakai = oCaraPakai.GetData(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI))
                    If dsCaraPakai IsNot Nothing Then
                        .CARAPAKAI = dsCaraPakai.MEMO
                    Else
                        .CARAPAKAI = ""
                    End If
                    .KDCARAPAKAI = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)), oItem.DefaultItem_CaraPakai, grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI))
                    .JUMLAH_PAKET = CDec(0)
                    .JUMLAH_NONPAKET = CDec(0)
                    .JUMLAH = CDec(grvDetailResep.GetRowCellValue(i, colQTY))
                    .HARGA = CDec(grvDetailResep.GetRowCellValue(i, colPRICE))
                    .TOTAL_PAKET = CDec(0)
                    .TOTAL_NONPAKET = CDec(0)
                    .TOTAL = CDec(grvDetailResep.GetRowCellValue(i, colGRANDTOTAL))
                    .ROMAWI = IntegerToRoman(CInt(grvDetailResep.GetFocusedRowCellValue(colQTY)))
                    .REMARKS_DOKTER = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER)), "", grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER))
                    .KDITEM = grvDetailResep.GetRowCellValue(i, colKDITEM)
                    .ISKRONIS = False
                    .ISALKES = False
                    .KDUOM = grvDetailResep.GetRowCellValue(i, colKDUOM)
                    .KDSIGNA = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDSIGNA)), oItem.DefaultItem_Signa, grvDetailResep.GetRowCellValue(i, colKDSIGNA))
                    .QTY_PERUBAHAN = 0
                    .REMARKS_FARMASI = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colREMARKS_FARMASI)), "", grvDetailResep.GetRowCellValue(i, colREMARKS_FARMASI))
                    .KDUSER = sUserID
                    .ISBACA = "0"
                End With

                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oBHPPasien.InsertData(ds, arrDetail)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oBHPPasien.UpdateData(ds, arrDetail)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

            If fn_SaveBillingFarmasiCategor4(ds.KDBHPPASIEN) = False Then
                MsgBox("Eror Save Billing Farmasi Stok Tidak memotong", MsgBoxStyle.Exclamation, Me.Text)
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Function fn_SaveBillingFarmasiCategor4(ByVal KODE As String) As Boolean
        Try
            Dim sSubTotal As Decimal = 0
            For i As Integer = 0 To grvDetailResep.RowCount - 2
                sSubTotal += CDec(grvDetailResep.GetRowCellValue(i, colGRANDTOTAL))
            Next

            Dim oWarehouse As New Reference.clsWarehouseDepartment


            Dim oSalesOrderTransaksi As New Sales.clsSalesOrderTransaksi
            ' ***** HEADER *****
            Dim ds = oSalesOrderTransaksi.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oSalesOrderTransaksi.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDSOTRANSAKSI = KODE
                .CATEGORY = 4
                .DATE = deDATE.DateTime
                .KDKUNJUNGAN = sKDKUNJUNGAN
                .KDWAREHOUSE = grdKDWAREHOUSE.EditValue
                .ISBHP = True
                .SUBTOTAL = CDec(sSubTotal)
                .DISCOUNT = CDec(0)
                .TAX = CDec(0)
                .GRANDTOTAL = CDec(sSubTotal)
                Try
                    .PAYAMOUNT = oSalesOrderTransaksi.GetData(sNoId).PAYAMOUNT
                Catch oErr As Exception
                    .PAYAMOUNT = CDec(0)
                End Try
                .MEMO = txtMEMO.Text.Trim.ToUpper
                Try
                    .KDUSER = oSalesOrderTransaksi.GetData(sNoId).KDUSER
                Catch ex As Exception
                    .KDUSER = sUserID
                End Try
                '.KDSHIFT = sSHIFT
                .KDSHIFT = sSHIFT
                .KDDOCTOR = sKDDOCTOR
                .TUSLAH = CDec(0)

                Try
                    .KDCPPT = oSalesOrderTransaksi.GetData(sNoId).KDCPPT
                Catch ex As Exception
                    .KDCPPT = ""
                End Try
                Try
                    .KDORDER = oSalesOrderTransaksi.GetData(sNoId).KDORDER
                Catch ex As Exception
                    .KDORDER = KODE
                End Try
            End With

            Dim oItem As New Reference.clsItem

            ' ***** DETIL *****
            Dim arrDetail = oSalesOrderTransaksi.GetStructureDetailList
            For i As Integer = 0 To grvDetailResep.RowCount - 2
                Dim dsDetail = oSalesOrderTransaksi.GetStructureDetail
                With dsDetail
                    .DATECREATED = deDATE.DateTime
                    .DATEUPDATED = Now
                    .SEQ = i
                    .KDSOTRANSAKSI = KODE
                    .KDITEM = grvDetailResep.GetRowCellValue(i, colKDITEM)
                    .KDUOM = grvDetailResep.GetRowCellValue(i, colKDUOM)
                    .KDSIGNA = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDSIGNA)), oItem.DefaultItem_Signa, grvDetailResep.GetRowCellValue(i, colKDSIGNA))
                    .KDCARAPAKAI = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)), oItem.DefaultItem_CaraPakai, grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI))
                    .QTY = CDec(grvDetailResep.GetRowCellValue(i, colQTY))
                    .ISRACIK = False
                    .PRICE = CDec(grvDetailResep.GetRowCellValue(i, colPRICE))
                    .SUBTOTAL = CDec(grvDetailResep.GetRowCellValue(i, colGRANDTOTAL))
                    .DISCOUNT = CDec(0)
                    .GRANDTOTAL = CDec(grvDetailResep.GetRowCellValue(i, colGRANDTOTAL))
                    .KDDOCTOR = sKDDOCTOR
                    .KDDEPARTMENT = sKDDEPARTMENT
                    .REMARKS = ""
                    .ISCETAKETIKET = False
                    .GROUPRACIK = 0
                    .KDUSER = sUserID
                End With
                arrDetail.Add(dsDetail)
            Next

            Dim dsCek = oSalesOrderTransaksi.GetData(KODE)
            If dsCek Is Nothing Then
                Try
                    Dim sKDSOTRANSAKSI = oSalesOrderTransaksi.InsertData(ds, arrDetail)
                    If sKDSOTRANSAKSI = "" Then
                        fn_SaveBillingFarmasiCategor4 = False
                    Else
                        fn_SaveBillingFarmasiCategor4 = True
                    End If
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_SaveBillingFarmasiCategor4 = oSalesOrderTransaksi.UpdateData(ds, arrDetail)

                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveBillingFarmasiCategor4 = False
        End Try
    End Function
    Private Function IntegerToRoman(IntNumberValue As Integer) As String
        Dim RomanNumbers As New Dictionary(Of String, Integer)()
        RomanNumbers.Add("M", 1000)
        RomanNumbers.Add("CM", 900)
        RomanNumbers.Add("D", 500)
        RomanNumbers.Add("CD", 400)
        RomanNumbers.Add("C", 100)
        RomanNumbers.Add("XC", 90)
        RomanNumbers.Add("L", 50)
        RomanNumbers.Add("XL", 40)
        RomanNumbers.Add("X", 10)
        RomanNumbers.Add("IX", 9)
        RomanNumbers.Add("V", 5)
        RomanNumbers.Add("IV", 4)
        RomanNumbers.Add("I", 1)

        Dim result As String = ""

        For Each pair As KeyValuePair(Of String, Integer) In RomanNumbers
            While IntNumberValue >= pair.Value
                IntNumberValue -= pair.Value
                result += pair.Key
            End While
        Next
        Return result
    End Function
#End Region
#Region "Grid Method"
    Private Sub grvDetailResep_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDetailResep.CellValueChanged
        Try
            If e.Column.Name = colKDITEM.Name Then
                If grvDetailResep.GetFocusedRowCellValue(colKDITEM) IsNot Nothing Then
                    Dim oItem As New Reference.clsItem
                    Dim ds = oItem.GetDataDetail_UOM(grvDetailResep.GetFocusedRowCellValue(colKDITEM))

                    If ds IsNot Nothing Then
                        grvDetailResep.SetFocusedRowCellValue(colKDUOM, ds.FirstOrDefault(Function(x) x.RATE = 1).KDUOM)
                        grvDetailResep.SetFocusedRowCellValue(colREMARKS_DOKTER, "")
                        grvDetailResep.SetFocusedRowCellValue(colREMARKS_FARMASI, IIf(ds.FirstOrDefault.M_ITEM.KDITEM_L5.Contains("INTOLERANSI OBAT"), "Ya", "No"))

                        'grvDetailResep.SetFocusedRowCellValue(colCPPT_ISBACANONRACIKAN, "0")
                        'grvDetailResep.SetFocusedRowCellValue(colCPPT_ISALKESNONRACIKAN, IIf(ds.FirstOrDefault.M_ITEM.M_ITEM_L3.MEMO = "ALKES", True, False))
                        'grvDetailResep.SetFocusedRowCellValue(colCPPT_ISALKESNONRACIKAN, IIf(ds.FirstOrDefault.M_ITEM.M_ITEM_L3.MEMO = "OBAT KRONIS", True, False))
                        grvDetailResep.SetFocusedRowCellValue(colQTY, 1)
                    End If
                End If
            ElseIf e.Column.Name = colREMARKS_DOKTER.Name Then
                'grvTindakan.Focus()
                'grvTindakan.AddNewRow()
                'grvTindakan.SetFocusedRowCellValue(colTERAPI, fn_LoadITEM(grvDetailResep.GetFocusedRowCellValue(colKDITEM)) & " " & fn_LoadSIGNA(grvDetailResep.GetFocusedRowCellValue(colKDSIGNA)))
                'grvTindakan.SetFocusedRowCellValue(colTINDAKAN, "")
                'grvTindakan.UpdateCurrentRow()
            ElseIf e.Column.Name = colKDUOM.Name Then
                Dim oItem As New Reference.clsItem
                Dim ds = oItem.GetDataDetail_UOM(grvDetailResep.GetFocusedRowCellValue(colKDITEM))

                'Dim hargajualawal As Decimal = fn_LoadUOMKDUOMHARGA(grvDetailResep.GetFocusedRowCellValue(colKDITEM), grvDetailResep.GetFocusedRowCellValue(colKDUOM))
                'Dim margin As Decimal = fn_LoadUOMKDUOMMARGIN(grvDetailResep.GetFocusedRowCellValue(colKDITEM), grvDetailResep.GetFocusedRowCellValue(colKDUOM))

                'Dim sPriceSales As Decimal = hargajualawal + (hargajualawal * (margin / 100))
                If sHargaApotik = True Then
                    If sPENJAMIN <> "BPJS" Then
                        grvDetailResep.SetFocusedRowCellValue(colPRICE, ds.FirstOrDefault.PRICESALESSTANDARD)
                    Else
                        grvDetailResep.SetFocusedRowCellValue(colPRICE, ds.FirstOrDefault.PRICESALESTERMIN)
                    End If
                Else
                    grvDetailResep.SetFocusedRowCellValue(colPRICE, ds.FirstOrDefault.PRICEPURCHASESTANDARD)

                    If CDec(grvDetailResep.GetFocusedRowCellValue(colPRICE)) > 0 Then
                        Dim sPriceSetelahPPN = 0 + CDec(grvDetailResep.GetFocusedRowCellValue(colPRICE))
                        Dim sPriceTermin = buletin(CDec(sPriceSetelahPPN * (IIf(sPENJAMIN <> "BPJS", (ds.FirstOrDefault.MARGIN / 100), (ds.FirstOrDefault.PRICESALESTERMIN / 100))) + CDec(sPriceSetelahPPN)), 100)

                        grvDetailResep.SetFocusedRowCellValue(colPRICE, sPriceTermin)
                    Else
                        grvDetailResep.SetFocusedRowCellValue(colPRICE, 0)
                    End If
                End If

                'grvDetailResep.SetFocusedRowCellValue(colPRICE, ds.FirstOrDefault.PRICESALESSTANDARD)
            ElseIf e.Column.Name = colQTY.Name Or e.Column.Name = colPRICE.Name Then
                Dim sSubTotal As Decimal = CDec(grvDetailResep.GetFocusedRowCellValue(colQTY)) * CDec(grvDetailResep.GetFocusedRowCellValue(colPRICE))
                grvDetailResep.SetFocusedRowCellValue(colGRANDTOTAL, sSubTotal)
            End If

        Catch oErr As Exception
            MsgBox("Event : " & vbCrLf & oErr.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetailResep.DeleteSelectedRows()
    End Sub
    Private Function buletin(ByVal Number As Double, Optional ByVal Range As Integer = 10) As Decimal

        buletin = Math.Round(Number / Range, 0) * Range

    End Function
#End Region
#Region "Command Button"
    Private Sub grdTEMPLATE_EditValueChanged(sender As Object, e As EventArgs) Handles grdTEMPLATE.EditValueChanged
        If isLoad = True Then
            If grdTEMPLATE.Text <> "" Then
                Dim oTemplate As New EMedrek.clsTemplateNonRacikan

                Dim dsTemplate = oTemplate.GetDataDetail(grdTEMPLATE.EditValue)

                For Each iLoop In dsTemplate.OrderBy(Function(x) x.SEQ)
                    'txtDIAGNOOSA.Text = iLoop.S_REQ_RECIPE_TEMPLATE_H.DESCRIPTION
                    grvDetailResep.Focus()
                    grvDetailResep.AddNewRow()
                    grvDetailResep.SetFocusedRowCellValue(colKDITEM, iLoop.KDITEM)
                    grvDetailResep.SetFocusedRowCellValue(colKDUOM, iLoop.KDUOM)
                    grvDetailResep.SetFocusedRowCellValue(colKDSIGNA, iLoop.KDSIGNA)
                    grvDetailResep.SetFocusedRowCellValue(colKDCARAPAKAI, iLoop.KDCARAPAKAI)
                    grvDetailResep.SetFocusedRowCellValue(colQTY, iLoop.JUMLAH)
                    grvDetailResep.SetFocusedRowCellValue(colREMARKS_DOKTER, iLoop.REMARKS_DOKTER)
                    grvDetailResep.UpdateCurrentRow()
                Next
            End If
        End If
    End Sub
    Private Sub frmBHPPasien_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadKDWAREHOUSE()
        Dim oWAREHOUSE As New Reference.clsWarehouseDepartment
        Try
            grdKDWAREHOUSE.Properties.DataSource = oWAREHOUSE.GetData.Where(Function(x) x.ISACTIVE = True And x.KDDEPARTMENT = sKDDEPARTMENT).ToList()
            grdKDWAREHOUSE.Properties.ValueMember = "KDWAREHOUSE"
            grdKDWAREHOUSE.Properties.DisplayMember = "NAME_DISPLAY"

            Dim ds = oWAREHOUSE.GetDataDepartment(sKDDEPARTMENT)
            If ds IsNot Nothing Then
                grdKDWAREHOUSE.Text = ds.KDWAREHOUSE
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadTEMPLATE()
        Dim oTemplateNonRacikan As New EMedrek.clsTemplateNonRacikan
        Try
            Dim ds = From x In oTemplateNonRacikan.GetDataDetailList()
                     Join y In oTemplateNonRacikan.GetDataDetail()
                     On x.KDCPPTTEMPLATE Equals y.KDCPPTTEMPLATE
                     Where y.KDUSER = sUserID
                     Group y By x.KDCPPTTEMPLATE, x.REMARKS, y.KDUSER Into Total = Sum(y.TOTAL)
                     Select KDCPPTTEMPLATE, REMARKS

            grdTEMPLATE.Properties.DataSource = ds.ToList()
            grdTEMPLATE.Properties.ValueMember = "KDCPPTTEMPLATE"
            grdTEMPLATE.Properties.DisplayMember = "REMARKS"

        Catch oErr As Exception
            MsgBox("Load Template Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadITEM()
        Try
            If grdKDWAREHOUSE.Text = "" Then Exit Sub

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            oConn = New SqlConnection(Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString()))

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM ( "
            SQL &= "SELECT "
            SQL &= "A.KDITEM "
            SQL &= ",A.NMITEM1 "
            SQL &= ",A.NMITEM2 "
            SQL &= ",HARGA = ISNULL((SELECT PRICESALESSTANDARD FROM DATABASERS..M_ITEM_UOM WHERE B.KDITEM = KDITEM AND B.KDUOM = KDUOM), 0) "
            SQL &= ",SATUAN = C.MEMO "
            SQL &= ",STOK = B.AMOUNT "
            SQL &= "FROM M_ITEM A "
            SQL &= "INNER JOIN M_ITEM_WAREHOUSE B "
            SQL &= "ON A.KDITEM = B.KDITEM "
            SQL &= "INNER JOIN M_UOM C "
            SQL &= "ON B.KDUOM = C.KDUOM "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "
            SQL &= "AND A.ISSTOK = 1 "
            SQL &= "AND B.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= ") X "
            SQL &= "ORDER BY "
            SQL &= "X.HARGA "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ITEM")

            grdKDITEM.DataSource = ds.Tables("ITEM")
            grdKDITEM.ValueMember = "KDITEM"
            grdKDITEM.DisplayMember = "NMITEM2"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Load Obat Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadUOM()
        Dim oUOM As New Reference.clsUOM
        Try
            Dim dsList = oUOM.GetData()

            grdUOM.DataSource = dsList.ToList()
            grdUOM.ValueMember = "KDUOM"
            grdUOM.DisplayMember = "MEMO"

            'grdKDUOMRACIKAN.DataSource = dsList.ToList()
            'grdKDUOMRACIKAN.ValueMember = "KDUOM"
            'grdKDUOMRACIKAN.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox("Load Uom" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadSIGNA()
        Dim oSigna As New Reference.clsSigna
        Try
            grdKDSIGNA.DataSource = oSigna.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDSIGNA.ValueMember = "KDSIGNA"
            grdKDSIGNA.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox("Load Signa" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadCARAPAKAI()
        Dim oCaraPakai As New Reference.clsCaraPakai
        Try
            grdCARAPAKAI.DataSource = oCaraPakai.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdCARAPAKAI.ValueMember = "KDCARAPAKAI"
            grdCARAPAKAI.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox("Load Cara Pakai" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub SimpleButton7_Click(sender As Object, e As EventArgs) Handles SimpleButton7.Click
        'salin resep
        Dim ds = oBHPPasien.GetDataByRmTerakhir(sKDCUSTOMER)
        Dim kosong As Boolean = False

        If ds IsNot Nothing Then
            For Each iLoop In oBHPPasien.GetDataDetail(ds.KDBHPPASIEN).OrderBy(Function(x) x.SEQ)
                kosong = True
                grvDetailResep.Focus()
                grvDetailResep.AddNewRow()

                grvDetailResep.SetFocusedRowCellValue(colKDITEM, iLoop.KDITEM)
                grvDetailResep.SetFocusedRowCellValue(colQTY, iLoop.JUMLAH)
                grvDetailResep.SetFocusedRowCellValue(colREMARKS_DOKTER, iLoop.REMARKS_DOKTER)
                grvDetailResep.SetFocusedRowCellValue(colKDUOM, iLoop.KDUOM)
                grvDetailResep.SetFocusedRowCellValue(colKDSIGNA, iLoop.KDSIGNA)
                grvDetailResep.SetFocusedRowCellValue(colKDCARAPAKAI, iLoop.KDCARAPAKAI)
                grvDetailResep.SetFocusedRowCellValue(colREMARKS_FARMASI, iLoop.REMARKS_FARMASI)
                grvDetailResep.UpdateCurrentRow()
            Next
        End If
    End Sub
    Private Sub btnRiwayatPemberianResep_Click(sender As Object, e As EventArgs) Handles btnRiwayatPemberianResep.Click
        If sKDCUSTOMER <> "" Then
            Dim frmRiwayatResep As New frmRiwayatResep
            Try
                frmRiwayatResep.LoadMe(sKDCUSTOMER, 2)
                frmRiwayatResep.ShowDialog(Me)

                If sNoTransaksi <> String.Empty Then
                    For Each iLoop In oBHPPasien.GetDataDetail(sNoTransaksi).OrderBy(Function(x) x.SEQ)
                        grvDetailResep.Focus()
                        grvDetailResep.AddNewRow()

                        grvDetailResep.SetFocusedRowCellValue(colKDITEM, iLoop.KDITEM)
                        'grvDetailResep.SetFocusedRowCellValue(colKDUOM, iLoop.M_UOM.MEMO)
                        'grvDetailResep.SetFocusedRowCellValue(colKDSIGNA, iLoop.M_SIGNA.MEMO)
                        'grvDetailResep.SetFocusedRowCellValue(colKDCARAPAKAI, iLoop.M_CARAPAKAI.MEMO)
                        grvDetailResep.SetFocusedRowCellValue(colQTY, iLoop.JUMLAH)
                        grvDetailResep.SetFocusedRowCellValue(colREMARKS_DOKTER, iLoop.REMARKS_DOKTER)

                        'grvDetailResep.SetFocusedRowCellValue(colKDITEM, iLoop.KDITEM)
                        grvDetailResep.SetFocusedRowCellValue(colKDUOM, iLoop.KDUOM)
                        grvDetailResep.SetFocusedRowCellValue(colKDSIGNA, iLoop.KDSIGNA)
                        grvDetailResep.SetFocusedRowCellValue(colKDCARAPAKAI, iLoop.KDCARAPAKAI)
                        grvDetailResep.SetFocusedRowCellValue(colREMARKS_FARMASI, iLoop.REMARKS_FARMASI)
                        'grvDetailResep.SetFocusedRowCellValue(colQTY_PERUBAHAN, iLoop.QTY)
                        'grvDetailResep.SetFocusedRowCellValue(colREMARKS_FARMASI, iLoop.REMARKS_FARMASI)
                        grvDetailResep.UpdateCurrentRow()
                    Next
                End If

            Catch ex As Exception
                MsgBox("Load Form Riwayat Resep : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Else
            MsgBox("Nomor Rekam Medis Kosong!!!", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub btnBuatTemplate_Click(sender As Object, e As EventArgs) Handles btnBuatTemplate.Click
        Dim frmTemplateNonRacikanList As New frmTemplateNonRacikanList
        Try
            frmTemplateNonRacikanList.fn_LoadRacikan(False)
            frmTemplateNonRacikanList.ShowDialog(Me)
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmTemplateNonRacikanList Is Nothing Then frmTemplateNonRacikanList.Dispose()
            frmTemplateNonRacikanList = Nothing

            fn_LoadTEMPLATE()
        End Try
    End Sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Dim frmDefecta As New frmDefecta
        Try
            frmDefecta.LoadMe(FORM_MODE.FORM_MODE_ADD)
            frmDefecta.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmDefecta Is Nothing Then frmDefecta.Dispose()
            frmDefecta = Nothing

        End Try
    End Sub
    Private Sub grdKDWAREHOUSE_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDWAREHOUSE.EditValueChanged
        If isLoad = True Then
            fn_LoadITEM()
        End If
    End Sub
#End Region
End Class