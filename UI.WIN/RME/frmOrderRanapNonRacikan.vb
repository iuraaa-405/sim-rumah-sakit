Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmOrderRanapNonRacikan
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oOrderRanapNonRacikan As New Order.clsOrderRanapNonRacikan
    Private oReqRecipeRawatInap As New Transaksi.clsReqRecipeRawatInap
    Private sKDIDENTITAS As Integer = 0
    Private sPENJAMIN As String = String.Empty
    Private sKDPENDAFTARAN As String = String.Empty
    Private sKDCUSTOMER As String = String.Empty
    Private sNAMAPASIEN As String = String.Empty
    Private sDOKTER As String = String.Empty
    Private sKDDOCTOR As String = String.Empty
    Private sTUJUAN As String = String.Empty
    Private sPulang As Boolean = False
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDIDENTITAS As String, ByVal Diagnosa As String, ByVal bb As String, ByVal tb As String, ByVal NoId As String, ByVal ResepPulang As Boolean)
        oFormMode = FormMode
        sNoId = NoId
        sKDIDENTITAS = KDIDENTITAS
        txtMEMO1.Text = Diagnosa
        txtBERATBADAN.Text = bb
        txtTINGGIBADAN.Text = tb
        sPulang = ResepPulang

        Dim dsCekIdentitas = oOrderRanapNonRacikan.GetDataIdentitas(KDIDENTITAS)
        If dsCekIdentitas IsNot Nothing Then
            sPENJAMIN = dsCekIdentitas.KDDAFTAR_L1_NAMA
            sKDPENDAFTARAN = dsCekIdentitas.KDPENDAFTARAN
            sKDCUSTOMER = dsCekIdentitas.KDCUSTOMER
            sNAMAPASIEN = dsCekIdentitas.NAMAPASIEN
            sDOKTER = dsCekIdentitas.KDDOCTOR_NAMA
            sKDDOCTOR = dsCekIdentitas.KDDOCTOR
            sTUJUAN = dsCekIdentitas.KDDEPARTMENT_NAMA
        End If

    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "Order Non Racikan"

            'lKDOrderRanapNonRacikan.Text = OrderRanapNonRacikan.KDOrderRanapNonRacikan
            lDATE.Text = "Tanggal Order * :"
            'lKDWAREHOUSE.Text = OrderRanapNonRacikan.KDWAREHOUSE & " *"

            'tab1.Text = OrderRanapNonRacikan.TAB_DETAIL
            'tab2.Text = OrderRanapNonRacikan.TAB_MEMO

            'grvDetail.Columns("KDITEM").Caption = OrderRanapNonRacikan.DETAIL_KDITEM
            'grvDetail.Columns("KDUOM").Caption = OrderRanapNonRacikan.DETAIL_KDUOM
            'grvDetail.Columns("QTY").Caption = OrderRanapNonRacikan.DETAIL_QTY
            'grvDetail.Columns("REMARKS").Caption = OrderRanapNonRacikan.DETAIL_REMARKS

            'grvKDWAREHOUSE.Columns("NAME_DISPLAY").Caption = Warehouse.NAME_DISPLAY

            ''grvKDITEM.Columns("NMITEM1").Caption = Item.NMITEM1_2
            'grvKDITEM.Columns("NMITEM2").Caption = Item.NMITEM2_2
            '' grvKDITEM.Columns("NMITEM3").Caption = Item.NMITEM3_2

            'grvKDUOM.Columns("MEMO").Caption = UOM.MEMO

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'sCode = txtKDOrderRanapNonRacikan.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadITEM()
        fn_LoadUOM()
        fn_LoadSIGNA()
        fn_LoadCARAPAKAI()
        fn_LoadTEMPLATE()

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
        grvDetailResep.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDORDER.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        grvDetailResep.OptionsSelection.MultiSelect = True
        grvDetailResep.SelectAll()
        grvDetailResep.DeleteSelectedRows()
        grvDetailResep.OptionsSelection.MultiSelect = False

        chkResepPulang.Checked = sPulang
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oOrderRanapNonRacikan.GetData(sNoId)

            With ds
                txtKDORDER.Text = .KDORDER
                deDATE.DateTime = .TANGGALORDER

                bindingSource.DataSource = oOrderRanapNonRacikan.GetDataDetail(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetailResep.DataSource = bindingSource

            End With

            Dim dsLainnya = oOrderRanapNonRacikan.GetDataLainnya(sNoId)
            With dsLainnya
                txtMEMO1.Text = .MEMO1
                txtBERATBADAN.Text = .BERATBADAN
                txtTINGGIBADAN.Text = .TINGGIBADAN
                txtINDIKASI.Text = .INDIKASI

                chkResepPulang.Checked = IIf(.MEMO5 <> "", True, False)

            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If sKDIDENTITAS = 0 Then
                MsgBox("Kode Identitas Kosong", MsgBoxStyle.Exclamation, Me.Text)
                txtKDORDER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDORDER.ErrorText = Statement.ErrorRequired

                txtKDORDER.Focus()
                fn_Validate = False
                Exit Function
            End If

            If txtMEMO1.Text = "" Then
                MsgBox("Diagnosa Kosong", MsgBoxStyle.Exclamation, Me.Text)
                txtMEMO1.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtMEMO1.ErrorText = Statement.ErrorRequired

                txtMEMO1.Focus()
                fn_Validate = False
                Exit Function
            End If

            'If txtTINGGIBADAN.Text = "" Then
            '    MsgBox("Tinggi Badan Kosong", MsgBoxStyle.Exclamation, Me.Text)
            '    txtTINGGIBADAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '    txtTINGGIBADAN.ErrorText = Statement.ErrorRequired

            '    txtTINGGIBADAN.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If
            'If txtBERATBADAN.Text = "" Then
            '    MsgBox("Berat Badan Kosong", MsgBoxStyle.Exclamation, Me.Text)
            '    txtBERATBADAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '    txtBERATBADAN.ErrorText = Statement.ErrorRequired

            '    txtBERATBADAN.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If

            'If txtINDIKASI.Text = "" Then
            '    MsgBox("Indikasi Tindakan Kosong", MsgBoxStyle.Exclamation, Me.Text)
            '    txtINDIKASI.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '    txtINDIKASI.ErrorText = Statement.ErrorRequired

            '    txtINDIKASI.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If

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
            Dim ds = oOrderRanapNonRacikan.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oOrderRanapNonRacikan.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .TANGGALORDER = deDATE.DateTime
                .JENISORDER = "FARRI"
                .KDORDER = sNoId
                .KDIDENTITAS = sKDIDENTITAS
                .NOMORREFERENCE = ""
                .USERORDER = sUserID
                Try
                    .STATUS = oOrderRanapNonRacikan.GetData(sNoId).STATUS
                Catch oErr As Exception
                    .STATUS = "TAMBAH"
                End Try
                .MEMO = "ORDER FARMASI"
            End With

            Dim dsLainnya = oOrderRanapNonRacikan.GetStructureHeaderLainnya
            With dsLainnya
                .KDORDER = ds.KDORDER
                .INDIKASI = txtINDIKASI.Text
                .TINGGIBADAN = txtTINGGIBADAN.Text
                .BERATBADAN = txtBERATBADAN.Text
                .MEMO1 = txtMEMO1.Text
                .MEMO2 = ""
                .MEMO3 = ""
                .MEMO4 = ""
                .MEMO5 = IIf(chkResepPulang.Checked = False, "", "RESEP PULANG")
            End With

            Dim oItem As New Reference.clsItem
            Dim oUom As New Reference.clsUOM
            Dim oSiga As New Reference.clsSigna
            Dim oCaraPakai As New Reference.clsCaraPakai

            ' ***** DETIL *****
            Dim arrDetail = oOrderRanapNonRacikan.GetStructureDetailList
            For i As Integer = 0 To grvDetailResep.RowCount - 2
                Dim dsDetail = oOrderRanapNonRacikan.GetStructureDetail
                With dsDetail
                    .DATECREATED = ds.DATECREATED
                    .DATEUPDATED = ds.DATEUPDATED
                    .SEQ = i
                    .KDORDER = ds.KDORDER
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
                    fn_Save = oOrderRanapNonRacikan.InsertData(ds, arrDetail, dsLainnya)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oOrderRanapNonRacikan.UpdateData(ds, arrDetail, dsLainnya)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

            fn_SaveRawatInap(ds.KDORDER)

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Function fn_SaveRawatInap(ByVal Kode As String) As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oReqRecipeRawatInap.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oReqRecipeRawatInap.GetData(sNoId).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .KDREQRECIPE_RI = Kode
                .NOANTRIAN = String.Empty
                .KDPENDAFTARAN = sKDPENDAFTARAN
                .KDWAREHOUSE = ""
                .ALERGIOBAT = ""
                .BERATBADAN = txtBERATBADAN.Text
                Try
                    .DESCRIPTION = oReqRecipeRawatInap.GetData(sNoId).DESCRIPTION
                Catch ex As Exception
                    .DESCRIPTION = deDATE.DateTime.ToString("ddMMyyyy")
                End Try
                Try
                    .ISCHEKED = oReqRecipeRawatInap.GetData(sNoId).ISCHEKED
                Catch ex As Exception
                    .ISCHEKED = False
                End Try
                Try
                    .KONFIRMASIRESEP = oReqRecipeRawatInap.GetData(sNoId).KONFIRMASIRESEP
                Catch ex As Exception
                    .KONFIRMASIRESEP = "Resep Belum Diterima"
                End Try
                .SUBTOTAL_RINCIAN = CDec(0)
                .SUBTOTAL_PAKET = CDec(0)
                .SUBTOTAL_KRONIS = CDec(0)
                .GRANDTOTAL = CDec(0)
                .NOIDUSER = sUserID
                .ISPERUBAHANRESEP = False
                .ISAPPROVAL = False
                Try
                    .NOIDUSER_FARMASI = oReqRecipeRawatInap.GetData(sNoId).NOIDUSER_FARMASI
                Catch ex As Exception
                    .NOIDUSER_FARMASI = String.Empty
                End Try
                Try
                    .DESCRIPTION_PERUBAHAN = oReqRecipeRawatInap.GetData(sNoId).DESCRIPTION_PERUBAHAN
                Catch ex As Exception
                    .DESCRIPTION_PERUBAHAN = ""
                End Try

                .PENJAMIN = sPENJAMIN
                .KDCUSTOMER = sKDCUSTOMER
                .PASIEN = sNAMAPASIEN
                .ALAMAT = sKDDOCTOR
                .DOKTER = sDOKTER
                .TUJUAN = sTUJUAN
                .DIAGNOSA = txtMEMO1.Text

                Dim oDoctor As New Reference.clsDoctor
                Dim dsDoctor = oDoctor.GetData(sKDDOCTOR)
                If dsDoctor IsNot Nothing Then
                    .SIPDOKTER = dsDoctor.SIP
                Else
                    .SIPDOKTER = ""
                End If
                Try
                    .KDRECIPE = oReqRecipeRawatInap.GetData(sNoId).KDRECIPE
                Catch ex As Exception
                    .KDRECIPE = ""
                End Try
            End With

            Dim oItem As New Reference.clsItem
            Dim oUom As New Reference.clsUOM
            Dim oSiga As New Reference.clsSigna
            Dim oCaraPakai As New Reference.clsCaraPakai

            ' ***** DETIL *****
            Dim arrDetail = oReqRecipeRawatInap.GetStructureDetailList
            For i As Integer = 0 To grvDetailResep.RowCount - 2
                Dim dsDetail = oReqRecipeRawatInap.GetStructureDetail
                With dsDetail
                    .SEQ = i
                    .KDREQRECIPE_RI = ds.KDREQRECIPE_RI

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

                    .QTY = CDec(grvDetailResep.GetRowCellValue(i, colQTY))
                    .PRICE = CDec(grvDetailResep.GetRowCellValue(i, colPRICE))
                    .GRANDTOTAL = CDec(0)
                    .ROMAWI = IntegerToRoman(CInt(grvDetailResep.GetFocusedRowCellValue(colQTY)))
                    .REMARKS_DOKTER = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER)), "", grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER))
                    .KDITEM = grvDetailResep.GetRowCellValue(i, colKDITEM)
                    .KDUOM = grvDetailResep.GetRowCellValue(i, colKDUOM)
                    .KDSIGNA = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDSIGNA)), oSiga.DefaultItem_L1(), grvDetailResep.GetRowCellValue(i, colKDSIGNA))
                    .KDCARAPAKAI = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)), oCaraPakai.DefaultItem_L1(), grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI))
                    .QTY_PERUBAHAN = grvDetailResep.GetRowCellValue(i, colQTY)
                    Try
                        .REMARKS_FARMASI = oReqRecipeRawatInap.GetDataObatBySeq(ds.KDREQRECIPE_RI, i).REMARKS_FARMASI
                    Catch ex As Exception
                        .REMARKS_FARMASI = ""
                    End Try
                    .DOSIS = ""
                End With

                If grvDetailResep.GetRowCellValue(i, colKDITEM) IsNot Nothing Then
                    arrDetail.Add(dsDetail)
                End If
            Next

            Dim dsCek = oReqRecipeRawatInap.GetData(Kode)

            If dsCek Is Nothing Then
                fn_SaveRawatInap = oReqRecipeRawatInap.InsertData(ds, arrDetail)
            Else
                fn_SaveRawatInap = oReqRecipeRawatInap.UpdateData(ds, arrDetail)
            End If

        Catch oErr As Exception
            MsgBox("Simpan Ke Order Farmasi" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveRawatInap = False
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
    Private Sub frmOrderRanapNonRacikan_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            oConn = New SqlConnection(Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString()))

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            'ds.PRICEPURCHASESTANDARD + (ds.PRICEPURCHASESTANDARD * (ds.MARGIN / 100))

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM ( "
            SQL &= "SELECT "
            SQL &= "A.KDITEM "
            SQL &= ",A.NMITEM1 "
            SQL &= ",A.NMITEM2 "
            'SQL &= ",HARGA = B.PRICEPURCHASESTANDARD "
            SQL &= ",HARGA = B.PRICESALESSTANDARD "
            SQL &= ",SATUAN = C.MEMO "
            SQL &= ",STOK = ISNULL((SELECT SUM(AMOUNT) FROM M_ITEM_WAREHOUSE WHERE B.KDITEM = KDITEM AND B.KDUOM = KDUOM GROUP BY KDITEM) ,0) "
            SQL &= "FROM M_ITEM A "
            SQL &= "INNER JOIN M_ITEM_UOM B "
            SQL &= "ON A.KDITEM = B.KDITEM "
            SQL &= "INNER JOIN M_UOM C "
            SQL &= "ON B.KDUOM = C.KDUOM "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "
            SQL &= "AND B.RATE = 1 "
            SQL &= "AND A.ISSTOK = 1 "

            'SQL &= "UNION "

            'SQL &= "SELECT "
            'SQL &= "KDITEM = 'R999999' "
            'SQL &= ",NMITEM1 = A.DESCRIPTION "
            'SQL &= ",NMITEM2 = A.DESCRIPTION "
            'SQL &= ",HARGA = 0 "
            'SQL &= ",SATUAN = '-' "
            'SQL &= ",STOK = 0 "
            'SQL &= "FROM M_ITEM_RACIK A "
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

            'grdKDITEMOBATRACIKAN.DataSource = ds.Tables("ITEM")
            'grdKDITEMOBATRACIKAN.ValueMember = "KDITEM"
            'grdKDITEMOBATRACIKAN.DisplayMember = "NMITEM2"

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
        Dim ds = oOrderRanapNonRacikan.GetDataByRmTerakhir(sKDCUSTOMER)
        Dim kosong As Boolean = False

        If ds IsNot Nothing Then
            For Each iLoop In oOrderRanapNonRacikan.GetDataDetail(ds.KDORDER).OrderBy(Function(x) x.SEQ)
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
                frmRiwayatResep.LoadMe(sKDCUSTOMER, 0)
                frmRiwayatResep.ShowDialog(Me)

                If sNoTransaksi <> String.Empty Then
                    For Each iLoop In oOrderRanapNonRacikan.GetDataDetail(sNoTransaksi).OrderBy(Function(x) x.SEQ)
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

                    Dim oGrouperDataCppt As New Grouper.clsR_CPPT
                    For Each iLoop In oGrouperDataCppt.GetDataDetailNonRacikan(sNoTransaksi).OrderBy(Function(x) x.SEQ)
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

                    For Each iLoop In oGrouperDataCppt.GetDataDetailRacikan(sNoTransaksi).OrderBy(Function(x) x.SEQ)
                        grvDetailResep.Focus()
                        grvDetailResep.AddNewRow()

                        grvDetailResep.SetFocusedRowCellValue(colKDITEM, iLoop.KDITEM)
                        'grvDetailResep.SetFocusedRowCellValue(colKDUOM, iLoop.M_UOM.MEMO)
                        'grvDetailResep.SetFocusedRowCellValue(colKDSIGNA, iLoop.M_SIGNA.MEMO)
                        'grvDetailResep.SetFocusedRowCellValue(colKDCARAPAKAI, iLoop.M_CARAPAKAI.MEMO)
                        grvDetailResep.SetFocusedRowCellValue(colQTY, iLoop.JUMLAH)
                        grvDetailResep.SetFocusedRowCellValue(colREMARKS_DOKTER, iLoop.REMARKS)

                        'grvDetailResep.SetFocusedRowCellValue(colKDITEM, iLoop.KDITEM)
                        grvDetailResep.SetFocusedRowCellValue(colKDUOM, iLoop.KDUOM)

                        Dim oSigna As New Reference.clsSigna
                        Dim oCaraPakai As New Reference.clsCaraPakai

                        grvDetailResep.SetFocusedRowCellValue(colKDSIGNA, oSigna.DefaultItem_L1())
                        grvDetailResep.SetFocusedRowCellValue(colKDCARAPAKAI, oCaraPakai.DefaultItem_L1())
                        grvDetailResep.SetFocusedRowCellValue(colREMARKS_FARMASI, "")
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
    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        If sKDCUSTOMER <> "" Then
            Dim frmRiwayatResep As New frmRiwayatResep
            Try
                frmRiwayatResep.LoadMe(sKDCUSTOMER, 1)
                frmRiwayatResep.ShowDialog(Me)

                If sNoTransaksi <> String.Empty Then
                    Dim oReq_Recipe As New Sales.clsSalesOrderTransaksi

                    Dim dsTemplate = oReq_Recipe.GetDataDetail(sNoTransaksi)

                    For Each iLoop In dsTemplate.OrderBy(Function(x) x.SEQ)
                        grvDetailResep.Focus()
                        grvDetailResep.AddNewRow()

                        grvDetailResep.SetFocusedRowCellValue(colKDITEM, iLoop.KDITEM)
                        'grvDetailResep.SetFocusedRowCellValue(colKDUOM, iLoop.M_UOM.MEMO)
                        'grvDetailResep.SetFocusedRowCellValue(colKDSIGNA, iLoop.M_SIGNA.MEMO)
                        'grvDetailResep.SetFocusedRowCellValue(colKDCARAPAKAI, iLoop.M_CARAPAKAI.MEMO)
                        grvDetailResep.SetFocusedRowCellValue(colQTY, iLoop.QTY)
                        grvDetailResep.SetFocusedRowCellValue(colREMARKS_DOKTER, iLoop.REMARKS)

                        'grvDetailResep.SetFocusedRowCellValue(colKDITEM, iLoop.KDITEM)
                        grvDetailResep.SetFocusedRowCellValue(colKDUOM, iLoop.KDUOM)
                        grvDetailResep.SetFocusedRowCellValue(colKDSIGNA, iLoop.KDSIGNA)
                        grvDetailResep.SetFocusedRowCellValue(colKDCARAPAKAI, iLoop.KDCARAPAKAI)
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
#End Region
End Class