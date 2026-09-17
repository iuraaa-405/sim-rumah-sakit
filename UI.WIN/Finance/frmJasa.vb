Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data
Imports System.Data.SqlClient

Public Class frmJasa
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oJasa As New Finance.clsJasa
    Private sConnGuntur As String = ""
    Private listDetailTransaksi As New List(Of DataAccess.I_JASA_D)
    Private DokterUtama As String = ""

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim dsDatabase = oJasa.GetDataSetting

        sConnGuntur = dsDatabase.KONEKSI

        'txtCATEGORYBAYAR.Text = sCategoryBayar

        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = Jasa.TITLE

            lKDJASA.Text = Jasa.KDJASA
            lDATE.Text = Jasa.TANGGAL
            lCATEGORY.Text = Jasa.CATEGORY
            lNOSEP.Text = Jasa.NOSEP & " *"
            lKDCUSTOMER.Text = Jasa.KDCUSTOMER & " *"
            lNO_TRANSAKSI.Text = Jasa.NO_TRANSKASI & " *"
            lGRANDTOTAL.Text = Jasa.GRANDTOTAL

            tab1.Text = Jasa.TAB_DETAIL
            tab2.Text = Jasa.TAB_MEMO

            'grvDetail.Columns("KDDOCTOR").Caption = Jasa.DETAIL_KDDOCTOR
            'grvDetail.Columns("KDDEPARTMENT").Caption = Jasa.DETAIL_KDDEPARTMENT
            'grvDetail.Columns("JUMLAH").Caption = Jasa.DETAIL_JUMLAH
            'grvDetail.Columns("REMARKS").Caption = Jasa.DETAIL_REMARKS

            ' grvKDCUSTOMER.Columns("NAME_DISPLAY").Caption = Jasa.KDCUSTOMER

            'grvKDITEM.Columns("NMITEM1").Caption = Item.NMITEM1
            'grvKDITEM.Columns("NMITEM2").Caption = Item.NMITEM2
            'grvKDITEM.Columns("NMITEM3").Caption = Item.NMITEM3

            'grvKDUOM.Columns("MEMO").Caption = UOM.MEMO

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDJASA.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadDEPARTMENT()
        fn_LoadDOCTOR()
        fn_LoadITEM()

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
        rbCATEGORY.Properties.ReadOnly = Status
        txtNOSEP.Properties.ReadOnly = Status
        grdKDCUSTOMER.Properties.ReadOnly = Status
        txtNO_TRANSAKSI.Properties.ReadOnly = Status
        cboHitung.Properties.ReadOnly = Status
        txtGRANDTOTAL.Properties.ReadOnly = True

        txtMEMO.Properties.ReadOnly = Status

        grvDetail.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDJASA.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        grdKDCUSTOMER.ResetText()
        txtNO_TRANSAKSI.ResetText()
        txtNOSEP.ResetText()
        cboHitung.SelectedIndex = 0
        txtMEMO.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oJasa.GetData(sNoId)

            With ds
                txtKDJASA.Text = .KDJASA
                deDATE.DateTime = .DATE
                rbCATEGORY.SelectedIndex = .CATEGORY
                txtNOSEP.Text = .NOSEP
                grdKDCUSTOMER.Text = .KDCUSTOMER
                txtNO_TRANSAKSI.Text = .NO_TRANSAKSI
                txtGRANDTOTAL.Text = .GRANDTOTAL
                txtMEMO.Text = .DESCRIPTION
                cboHitung.SelectedIndex = .CATEGORYRUMUS
                fn_LoadCUSTOMER(.KDCUSTOMER)

                grdKDCUSTOMER.EditValue = .KDCUSTOMER

                bindingSource.DataSource = oJasa.GetDataDetail.Where(Function(x) x.KDJASA = sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = bindingSource
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If grdKDCUSTOMER.Text = String.Empty Then
                grdKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDCUSTOMER.ErrorText = Statement.ErrorRequired

                grdKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            End If
            'If txtNOSEP.Text = String.Empty Then
            '    txtNOSEP.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '    txtNOSEP.ErrorText = Statement.ErrorRequired

            '    txtNOSEP.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If
            If txtNO_TRANSAKSI.Text = String.Empty Then
                txtNO_TRANSAKSI.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtNO_TRANSAKSI.ErrorText = Statement.ErrorRequired

                txtNO_TRANSAKSI.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtCATEGORYBAYAR.Text = "BPJS" Then
                If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                    Dim dsJasa = oJasa.GetDataSEP(txtNOSEP.Text)
                    If dsJasa IsNot Nothing Then
                        If dsJasa.NOSEP <> "" Or dsJasa.NOSEP <> "-" Then
                            MsgBox("SEP Sudah di input dengan nomor Transaksi " & dsJasa.KDJASA, MsgBoxStyle.Exclamation, Me.Text)
                            fn_Validate = False
                            Exit Function
                        End If

                    End If
                End If
            End If


            grvDetail.UpdateCurrentRow()

            If grvDetail.RowCount < 2 Then
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
            Dim ds = oJasa.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oJasa.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDJASA = sNoId
                .DATE = deDATE.DateTime
                .CATEGORY = rbCATEGORY.SelectedIndex
                .NOSEP = txtNOSEP.Text.ToString.Trim.ToUpper
                .KDCUSTOMER = IIf(String.IsNullOrEmpty(grdKDCUSTOMER.EditValue), String.Empty, grdKDCUSTOMER.EditValue)
                .NO_TRANSAKSI = IIf(String.IsNullOrEmpty(txtNO_TRANSAKSI.Text), String.Empty, txtNO_TRANSAKSI.Text)
                .KD_KASIR = IIf(rbCATEGORY.SelectedIndex = 0, "", "02")
                .DESCRIPTION = txtMEMO.Text.Trim.ToUpper
                .KDUSER = sUserID
                .GRANDTOTAL = CDec(txtGRANDTOTAL.Text)
                .CATEGORYBAYAR = IIf(txtCATEGORYBAYAR.Text = "BPJS", 0, 1)
                .CATEGORYRUMUS = cboHitung.SelectedIndex
            End With

            ' ***** DETIL *****
            Dim arrDetail = oJasa.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oJasa.GetStructureDetail
                With dsDetail

                    .SEQ = i
                    .KDJASA = ds.KDJASA
                    .KDITEM = grvDetail.GetRowCellValue(i, colKDITEM)
                    .KDDOCTOR = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colKDDOCTOR)), "XXX", grvDetail.GetRowCellValue(i, colKDDOCTOR))
                    .KDDEPARTMENT = grvDetail.GetRowCellValue(i, colKDDEPARTMENT)
                    .JUMLAH = CDec(grvDetail.GetRowCellValue(i, colJUMLAH))
                    .REMARKS = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colREMARKS)), "-", grvDetail.GetRowCellValue(i, colREMARKS))


                    Dim Kosong = grvDetail.GetRowCellValue(i, colKD_KASIR)
                    If Kosong IsNot Nothing Then
                        .KD_KASIR = grvDetail.GetRowCellValue(i, colKD_KASIR)
                        .NO_TRANSAKSI = grvDetail.GetRowCellValue(i, colNO_TRANSAKSI)
                        .URUT = grvDetail.GetRowCellValue(i, colURUT)
                        .TGL_TRANSAKSI = grvDetail.GetRowCellValue(i, colTGL_TRANSAKSI)
                        .KD_USER = grvDetail.GetRowCellValue(i, colKD_USER)
                        .KD_TARIF = grvDetail.GetRowCellValue(i, colKD_TARIF)
                        .KD_PRODUK = grvDetail.GetRowCellValue(i, colKD_PRODUK)
                        .KD_UNIT = grvDetail.GetRowCellValue(i, colKD_UNIT)
                        .TGL_BERLAKU = grvDetail.GetRowCellValue(i, colTGL_BERLAKU)
                        .CHARGE = grvDetail.GetRowCellValue(i, colCHARGE)
                        .ADJUST = grvDetail.GetRowCellValue(i, colADJUST)
                        .FOLIO = grvDetail.GetRowCellValue(i, colFOLIO)
                        .QTY = grvDetail.GetRowCellValue(i, colQTY)
                        .HARGA = grvDetail.GetRowCellValue(i, colHARGA)
                        .SHIFT = grvDetail.GetRowCellValue(i, colSHIFT)
                        .KD_DOKTER = grvDetail.GetRowCellValue(i, colKD_DOKTER)
                        .KD_UNIT_TR = grvDetail.GetRowCellValue(i, colKD_UNIT_TR)
                        .CITO = grvDetail.GetRowCellValue(i, colCITO)
                        .JS = grvDetail.GetRowCellValue(i, colJS)
                        .JP = grvDetail.GetRowCellValue(i, colJP)
                        .NO_FAKTUR = grvDetail.GetRowCellValue(i, colNO_FAKTUR)
                        .FLAG = grvDetail.GetRowCellValue(i, colFLAG)
                        .TAG = grvDetail.GetRowCellValue(i, colTAG)
                        .HRG_ASLI = grvDetail.GetRowCellValue(i, colHRG_ASLI)
                        .KD_CUSTOMER = grvDetail.GetRowCellValue(i, colKD_CUSTOMER)
                        .CLOSE_SHIFT_STATUS = grvDetail.GetRowCellValue(i, colCLOSE_SHIFT_STATUS)
                        .KD_LOKET = grvDetail.GetRowCellValue(i, colKD_LOKET)

                    Else
                        .KD_KASIR = 0
                        .NO_TRANSAKSI = 0
                        .URUT = 0
                        .TGL_TRANSAKSI = Now
                        .KD_USER = 0
                        .KD_TARIF = 0
                        .KD_PRODUK = 0
                        .KD_UNIT = 0
                        .TGL_BERLAKU = Now
                        .CHARGE = 0
                        .ADJUST = 0
                        .FOLIO = 0
                        .QTY = 0
                        .HARGA = 0
                        .SHIFT = 0
                        .KD_DOKTER = 0
                        .KD_UNIT_TR = 0
                        .CITO = 0
                        .JS = 0
                        .JP = 0
                        .NO_FAKTUR = 0
                        .FLAG = 0
                        .TAG = 0
                        .HRG_ASLI = 0
                        .KD_CUSTOMER = 0
                        .CLOSE_SHIFT_STATUS = 0
                        .KD_LOKET = 0


                    End If

                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oJasa.InsertData(ds, arrDetail)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oJasa.UpdateData(ds, arrDetail)
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
    Private Sub OnValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grvDetail.FocusedRowChanged
        If isLoad Then
            Calculate()
        End If
    End Sub
    Private Sub Calculate()
        Dim sSubtotal As Decimal = 0

        For i As Integer = 0 To grvDetail.RowCount - 2
            sSubtotal += CDec(grvDetail.GetRowCellValue(i, colJUMLAH))
        Next

        txtGRANDTOTAL.Text = sSubtotal

    End Sub
#Region "Grid Method"
    Private Sub grvDetail_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDetail.CellValueChanged
        If e.Column.Name = colKDITEM.Name Then
            Dim oItem As New Reference.clsItem
            Try
                If grvDetail.GetFocusedRowCellValue(colKDITEM) IsNot Nothing Then
                    Dim ds = oItem.GetDataDetail_UOM(grvDetail.GetFocusedRowCellValue(colKDITEM))

                    If ds IsNot Nothing Then
                        'grvDetail.SetFocusedRowCellValue(colKDDEPARTMENT, ds.FirstOrDefault(Function(x) x.RATE = 1).KDUOM)
                    Else
                        MsgBox(Statement.ErrorUOM, MsgBoxStyle.Exclamation, Me.Text)

                        grvDetail.CancelUpdateCurrentRow()
                    End If
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
    Private Sub frmJasa_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    Private Sub fn_LoadCUSTOMER(ByVal KDCUSTOMER As String)
        Dim oCustomer As New Reference.clsCustomer
        Try
            grdKDCUSTOMER.Properties.DataSource = oCustomer.GetData.Where(Function(x) x.KDCUSTOMER = KDCUSTOMER).ToList()
            grdKDCUSTOMER.Properties.ValueMember = "KDCUSTOMER"
            grdKDCUSTOMER.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox("Load Customer Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDEPARTMENT()
        Dim oDepartment As New Reference.clsDepartment
        Try
            grdKDDEPARTMENT.DataSource = oDepartment.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDEPARTMENT.ValueMember = "KDDEPARTMENT"
            grdKDDEPARTMENT.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox("Load Department Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDOCTOR()
        Dim oDoctor As New Reference.clsDoctor
        Try
            grdKDDOCTOR.DataSource = oDoctor.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDOCTOR.ValueMember = "KDDOCTOR"
            grdKDDOCTOR.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox("Load Dokter Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadITEM()
        Dim oDoctor As New Reference.clsItem
        Try
            grdKDITEM.DataSource = oDoctor.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDITEM.ValueMember = "KDITEM"
            grdKDITEM.DisplayMember = "NMITEM2"
        Catch oErr As Exception
            MsgBox("Load Item Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadTransaksi()
        grvDetail.OptionsSelection.MultiSelect = True
        grvDetail.SelectAll()
        grvDetail.DeleteSelectedRows()
        grvDetail.OptionsSelection.MultiSelect = False


        For Each iLoop In listDetailTransaksi

            grvDetail.Focus()
            grvDetail.AddNewRow()
            grvDetail.SetFocusedRowCellValue(colKDITEM, iLoop.KDITEM)
            grvDetail.SetFocusedRowCellValue(colKDDOCTOR, iLoop.KDDOCTOR)
            grvDetail.SetFocusedRowCellValue(colKDDEPARTMENT, iLoop.KDDEPARTMENT)
            grvDetail.SetFocusedRowCellValue(colJUMLAH, iLoop.JUMLAH)
            grvDetail.SetFocusedRowCellValue(colREMARKS, "-")

            'grvDetail.SetFocusedRowCellValue(colKDDOCTOR, iLoop.KDDOCTOR)

            grvDetail.SetFocusedRowCellValue(colKD_KASIR, iLoop.KD_KASIR)
            grvDetail.SetFocusedRowCellValue(colNO_TRANSAKSI, iLoop.NO_TRANSAKSI)
            grvDetail.SetFocusedRowCellValue(colURUT, iLoop.URUT)
            grvDetail.SetFocusedRowCellValue(colTGL_TRANSAKSI, iLoop.TGL_TRANSAKSI)
            grvDetail.SetFocusedRowCellValue(colKD_USER, iLoop.KD_USER)
            grvDetail.SetFocusedRowCellValue(colKD_TARIF, iLoop.KD_TARIF)
            grvDetail.SetFocusedRowCellValue(colKD_PRODUK, iLoop.KD_PRODUK)
            grvDetail.SetFocusedRowCellValue(colKD_UNIT, iLoop.KD_UNIT)
            grvDetail.SetFocusedRowCellValue(colTGL_BERLAKU, iLoop.TGL_BERLAKU)
            grvDetail.SetFocusedRowCellValue(colCHARGE, iLoop.CHARGE)
            grvDetail.SetFocusedRowCellValue(colADJUST, iLoop.ADJUST)
            grvDetail.SetFocusedRowCellValue(colFOLIO, iLoop.FOLIO)
            grvDetail.SetFocusedRowCellValue(colQTY, iLoop.QTY)
            grvDetail.SetFocusedRowCellValue(colHARGA, iLoop.HARGA)
            grvDetail.SetFocusedRowCellValue(colSHIFT, iLoop.SHIFT)
            grvDetail.SetFocusedRowCellValue(colKD_DOKTER, iLoop.KD_DOKTER)
            grvDetail.SetFocusedRowCellValue(colKD_UNIT_TR, iLoop.KD_UNIT_TR)
            grvDetail.SetFocusedRowCellValue(colCITO, iLoop.CITO)
            grvDetail.SetFocusedRowCellValue(colJS, iLoop.JS)
            grvDetail.SetFocusedRowCellValue(colJP, iLoop.JP)
            grvDetail.SetFocusedRowCellValue(colNO_FAKTUR, iLoop.NO_FAKTUR)
            grvDetail.SetFocusedRowCellValue(colFLAG, iLoop.FLAG)
            grvDetail.SetFocusedRowCellValue(colHRG_ASLI, iLoop.HRG_ASLI)
            grvDetail.SetFocusedRowCellValue(colKD_CUSTOMER, iLoop.KD_CUSTOMER)
            grvDetail.SetFocusedRowCellValue(colCLOSE_SHIFT_STATUS, iLoop.CLOSE_SHIFT_STATUS)
            grvDetail.SetFocusedRowCellValue(colKD_LOKET, iLoop.KD_LOKET)

            grvDetail.UpdateCurrentRow()

        Next
    End Sub
    Private Function fn_SearchTransaction(ByVal KD_KASIR As String, ByVal NOTRANSAKSI As String) As Boolean
        Try
            fn_SearchTransaction = True

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            'Public sConnMySql As String = "Data Source=172.165.115.212;Initial Catalog=simrs_dustira;Persist Security Info=True;User ID=simrs;Password=8091016; Port = 3308;"
            'Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            Dim sConn As String = sConnGuntur

            oConn = New SqlConnection(sConn)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "A.KD_PASIEN "
            SQL &= ",A.TGL_TRANSAKSI "
            SQL &= ",KDDOKTER = ISNULL((SELECT KD_DOKTER FROM KUNJUNGAN WHERE CONVERT(VARCHAR(8), A.TGL_TRANSAKSI, 112) = CONVERT(VARCHAR(8), TGL_MASUK, 112) AND A.KD_UNIT = KD_UNIT AND A.KD_PASIEN = KD_PASIEN), '') "
            SQL &= ",NAMA_DOKTER = ISNULL((SELECT BB.NAMA FROM KUNJUNGAN AA INNER JOIN DOKTER BB ON AA.KD_DOKTER = BB.KD_DOKTER WHERE CONVERT(VARCHAR(8), A.TGL_TRANSAKSI, 112) = CONVERT(VARCHAR(8), AA.TGL_MASUK, 112) AND A.KD_UNIT = AA.KD_UNIT AND A.KD_PASIEN = AA.KD_PASIEN), '') "
            SQL &= ",B.NAMA "
            SQL &= ",B.ALAMAT "
            SQL &= "FROM "
            SQL &= "TRANSAKSI A "
            SQL &= "INNER JOIN PASIEN B "
            SQL &= "ON A.KD_PASIEN = B.KD_PASIEN "
            SQL &= "WHERE "
            SQL &= "A.KD_KASIR = '" & KD_KASIR & "' "
            SQL &= "AND A.NO_TRANSAKSI = '" & NOTRANSAKSI & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

            If ds.Tables("ALL").Rows.Count < 1 Then
                MsgBox("Pasien Tidak diTemukan", MsgBoxStyle.Information, Me.Text)
            End If

            For iLoop As Integer = 0 To ds.Tables("ALL").Rows.Count - 1
                With ds.Tables("ALL")

                    deDATE.DateTime = .Rows(iLoop)("TGL_TRANSAKSI")

                    If txtCARIRM.Text = .Rows(iLoop)("KD_PASIEN") Then
                        InsertCustomer(.Rows(iLoop)("KD_PASIEN"), .Rows(iLoop)("NAMA"), .Rows(iLoop)("ALAMAT"))
                    Else
                        fn_SearchTransaction = False
                        MsgBox("No Transaksi " & grdCARIRM.EditValue & " Atas Nama " & .Rows(iLoop)("NAMA") & " Nomor RM " & .Rows(iLoop)("KD_PASIEN"), MsgBoxStyle.Exclamation, Me.Text)
                        InsertCustomer(txtCARIRM.Text, grdCARIRM.Text, "")
                    End If

                End With

            Next

        Catch oErr As Exception
            fn_SearchTransaction = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_SearchDetailTransaction(ByVal KD_KASIR As String, ByVal NOTRANSAKSI As String) As Boolean
        Try

            fn_SearchDetailTransaction = True

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = sConnGuntur

            oConn = New SqlConnection(sConn)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            Dim dsDatabase = oJasa.GetDataSetting

            'SQL = "EXEC DATABASE_GUNTUR_BAK_.dbo.TRANSAKSI_NEW  "
            SQL = dsDatabase.COA_OTHER_EXPENSE
            SQL &= " @CATEGORY = " & rbCATEGORY.SelectedIndex & ", "
            SQL &= " @KD_KASIR = '" & KD_KASIR & "', "
            SQL &= " @NO_TRANSAKSI = '" & NOTRANSAKSI & "', "
            SQL &= " @KD_PASIEN = '" & grdKDCUSTOMER.EditValue & "', "
            SQL &= " @RUMUS = '" & cboHitung.SelectedIndex & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

            If ds.Tables("ALL").Rows.Count < 1 Then
                MsgBox("Data Transaksi Tidak diTemukan", MsgBoxStyle.Information, Me.Text)
            End If

            listDetailTransaksi.Clear()

            Dim SEQ As Integer = 0

            For iLoop As Integer = 0 To ds.Tables("ALL").Rows.Count - 1
                Dim dsRekap As New DataAccess.I_JASA_D

                With ds.Tables("ALL")
                    If .Rows(iLoop)("KDDOKTERUTAMA") <> "" Then
                        DokterUtama = .Rows(iLoop)("KDDOKTERUTAMA")
                    End If
                End With

                With ds.Tables("ALL")

                    If .Rows(iLoop)("KD_UNIT") <> "" Then
                        InsertDepartment(.Rows(iLoop)("KD_UNIT"), .Rows(iLoop)("NAMA_UNIT"))
                    End If

                    If .Rows(iLoop)("KDDOKTERUTAMA") <> "" Then
                        InsertDoctor(.Rows(iLoop)("KDDOKTERUTAMA"), .Rows(iLoop)("NAMA_DOKTERUTAMA"))
                    End If

                    If .Rows(iLoop)("KDDOKTER") <> "" Then
                        InsertDoctor(.Rows(iLoop)("KDDOKTER"), .Rows(iLoop)("NAMA_DOKTER"))
                    End If

                    If .Rows(iLoop)("KD_PRODUK") <> 0 Then
                        InsertItem(.Rows(iLoop)("KD_PRODUK"), .Rows(iLoop)("NAMA_PRODUK"), .Rows(iLoop)("GRANDTOTAL"))
                    End If

                    If .Rows(iLoop)("NAMA_PRODUK") = "BIAYA FARMASI" Then
                        Dim TES2 = .Rows(iLoop)("KDDOKTER")
                    End If
                    SEQ += 1

                    dsRekap.KDJASA = SEQ
                    dsRekap.SEQ = SEQ

                    dsRekap.KDITEM = .Rows(iLoop)("KD_PRODUK")
                    dsRekap.KDDOCTOR = IIf(.Rows(iLoop)("KDDOKTER") <> "", .Rows(iLoop)("KDDOKTER"), DokterUtama)
                    dsRekap.KDDEPARTMENT = .Rows(iLoop)("KD_UNIT")

                    Dim TES = .Rows(iLoop)("GRANDTOTAL")

                    dsRekap.JUMLAH = .Rows(iLoop)("GRANDTOTAL")
                    dsRekap.REMARKS = "-"

                    If IsDBNull(.Rows(iLoop)("KD_KASIR")) Then
                        dsRekap.KD_KASIR = ""
                    Else
                        dsRekap.KD_KASIR = .Rows(iLoop)("KD_KASIR")
                    End If

                    If IsDBNull(.Rows(iLoop)("NO_TRANSAKSI")) Then
                        dsRekap.NO_TRANSAKSI = ""
                    Else
                        dsRekap.NO_TRANSAKSI = .Rows(iLoop)("NO_TRANSAKSI")
                    End If

                    If IsDBNull(.Rows(iLoop)("URUT")) Then
                        dsRekap.URUT = ""
                    Else
                        dsRekap.URUT = .Rows(iLoop)("URUT")
                    End If

                    If IsDBNull(.Rows(iLoop)("TGL_TRANSAKSI")) Then
                        dsRekap.TGL_TRANSAKSI = Now
                    Else
                        dsRekap.TGL_TRANSAKSI = .Rows(iLoop)("TGL_TRANSAKSI")
                    End If

                    If IsDBNull(.Rows(iLoop)("KD_USER")) Then
                        dsRekap.KD_USER = ""
                    Else
                        dsRekap.KD_USER = .Rows(iLoop)("KD_USER")
                    End If

                    If IsDBNull(.Rows(iLoop)("KD_TARIF")) Then
                        dsRekap.KD_TARIF = ""
                    Else
                        dsRekap.KD_TARIF = .Rows(iLoop)("KD_TARIF")
                    End If

                    If IsDBNull(.Rows(iLoop)("KD_PRODUK")) Then
                        dsRekap.KD_PRODUK = ""
                    Else
                        dsRekap.KD_PRODUK = .Rows(iLoop)("KD_PRODUK")
                    End If

                    If IsDBNull(.Rows(iLoop)("KD_UNIT")) Then
                        dsRekap.KD_UNIT = ""
                    Else
                        dsRekap.KD_UNIT = .Rows(iLoop)("KD_UNIT")
                    End If

                    If IsDBNull(.Rows(iLoop)("TGL_BERLAKU")) Then
                        dsRekap.TGL_BERLAKU = Now
                    Else
                        dsRekap.TGL_BERLAKU = .Rows(iLoop)("TGL_BERLAKU")
                    End If

                    If IsDBNull(.Rows(iLoop)("CHARGE")) Then
                        dsRekap.CHARGE = ""
                    Else
                        dsRekap.CHARGE = .Rows(iLoop)("CHARGE")
                    End If

                    If IsDBNull(.Rows(iLoop)("ADJUST")) Then
                        dsRekap.ADJUST = ""
                    Else
                        dsRekap.ADJUST = .Rows(iLoop)("ADJUST")
                    End If

                    If IsDBNull(.Rows(iLoop)("FOLIO")) Then
                        dsRekap.FOLIO = ""
                    Else
                        dsRekap.FOLIO = .Rows(iLoop)("FOLIO")
                    End If

                    If IsDBNull(.Rows(iLoop)("QTY")) Then
                        dsRekap.QTY = 0
                    Else
                        dsRekap.QTY = .Rows(iLoop)("QTY")
                    End If

                    If IsDBNull(.Rows(iLoop)("HARGA")) Then
                        dsRekap.HARGA = 0
                    Else
                        dsRekap.HARGA = .Rows(iLoop)("HARGA")
                    End If

                    If IsDBNull(.Rows(iLoop)("KD_DOKTER")) Then
                        dsRekap.KD_DOKTER = ""
                    Else
                        dsRekap.KD_DOKTER = .Rows(iLoop)("KD_DOKTER")
                    End If

                    If IsDBNull(.Rows(iLoop)("KD_UNIT_TR")) Then
                        dsRekap.KD_UNIT_TR = ""
                    Else
                        dsRekap.KD_UNIT_TR = .Rows(iLoop)("KD_UNIT_TR")
                    End If

                    If IsDBNull(.Rows(iLoop)("CITO")) Then
                        dsRekap.CITO = ""
                    Else
                        dsRekap.CITO = .Rows(iLoop)("CITO")
                    End If

                    If IsDBNull(.Rows(iLoop)("JS")) Then
                        dsRekap.JS = ""
                    Else
                        dsRekap.JS = .Rows(iLoop)("JS")
                    End If

                    If IsDBNull(.Rows(iLoop)("JP")) Then
                        dsRekap.JP = ""
                    Else
                        dsRekap.JP = .Rows(iLoop)("JP")
                    End If

                    If IsDBNull(.Rows(iLoop)("NO_FAKTUR")) Then
                        dsRekap.NO_FAKTUR = ""
                    Else
                        dsRekap.NO_FAKTUR = .Rows(iLoop)("NO_FAKTUR")
                    End If

                    If IsDBNull(.Rows(iLoop)("FLAG")) Then
                        dsRekap.FLAG = ""
                    Else
                        dsRekap.FLAG = .Rows(iLoop)("FLAG")
                    End If

                    If IsDBNull(.Rows(iLoop)("TAG")) Then
                        dsRekap.TAG = ""
                    Else
                        dsRekap.TAG = .Rows(iLoop)("TAG")
                    End If

                    If IsDBNull(.Rows(iLoop)("HRG_ASLI")) Then
                        dsRekap.HRG_ASLI = 0
                    Else
                        dsRekap.HRG_ASLI = .Rows(iLoop)("HRG_ASLI")
                    End If

                    If IsDBNull(.Rows(iLoop)("KD_CUSTOMER")) Then
                        dsRekap.KD_CUSTOMER = ""
                    Else
                        dsRekap.KD_CUSTOMER = .Rows(iLoop)("KD_CUSTOMER")
                    End If

                    If IsDBNull(.Rows(iLoop)("CLOSE_SHIFT_STATUS")) Then
                        dsRekap.CLOSE_SHIFT_STATUS = ""
                    Else
                        dsRekap.CLOSE_SHIFT_STATUS = .Rows(iLoop)("CLOSE_SHIFT_STATUS")
                    End If

                    If IsDBNull(.Rows(iLoop)("KD_LOKET")) Then
                        dsRekap.KD_LOKET = ""
                    Else
                        dsRekap.KD_LOKET = .Rows(iLoop)("KD_LOKET")
                    End If

                    listDetailTransaksi.Add(dsRekap)

                End With

            Next

            'For iLoop As Integer = 0 To ds.Tables("REPORT").Rows.Count - 1
            '    Dim dsRekap As New DA.dcEntity.R_PENJASAAN

            '    With ds.Tables("REPORT")
            '        dsRekap.Transaksi = .Rows(iLoop)("TRANSAKSI")
            '        dsRekap.NoSEP = .Rows(iLoop)("NOMORSEP")
            '        dsRekap.Kategori = .Rows(iLoop)("ISRUANGAN")
            '        dsRekap.KDREG = .Rows(iLoop)("KDREG")
            '        dsRekap.Tanggal = .Rows(iLoop)("TANGGAL")
            '        dsRekap.Pasien = .Rows(iLoop)("PASIEN")
            '        dsRekap.NoRM = .Rows(iLoop)("RM")
            '        dsRekap.NoInput = .Rows(iLoop)("KDTRANSAKSI")
            '        dsRekap.Nomor = .Rows(iLoop)("NOMOR")
            '        dsRekap.Uraian = .Rows(iLoop)("URAIAN")
            '        dsRekap.MataAnggaran = .Rows(iLoop)("MATAANGARAN")
            '        dsRekap.SubSpesialis = .Rows(iLoop)("SUB")
            '        dsRekap.Tujuan = .Rows(iLoop)("TUJUAN")
            '        dsRekap.Dokter = .Rows(iLoop)("DOKTER_1")
            '        dsRekap.Dokter2 = .Rows(iLoop)("DOKTER_2")
            '        dsRekap.Paramedis = .Rows(iLoop)("PARAMEDIS")
            '        dsRekap.NamaJasa = .Rows(iLoop)("DESCRIPTION")
            '        dsRekap.NamaTarif = .Rows(iLoop)("NAMATARIF")
            '        dsRekap.AmountPayment = .Rows(iLoop)("AMOUNTPAYMENT")
            '        dsRekap.AmountOriginal = .Rows(iLoop)("AMOUNTORIGINAL")
            '        dsRekap.AmountSelisih = .Rows(iLoop)("SELISIH")
            '        dsRekap.AmountCathlabSelisih = 0
            '        dsRekap.Tarif = .Rows(iLoop)("TARIF")
            '        dsRekap.TarifRS = .Rows(iLoop)("TARIF")
            '        dsRekap.Persen = .Rows(iLoop)("PERSEN")
            '        dsRekap.Total = .Rows(iLoop)("TOTAL")
            '        dsRekap.IsDefault = .Rows(iLoop)("IsDefault")
            '        dsRekap.Ket = .Rows(iLoop)("KETERANGAN")
            '        dsRekap.Selisih = .Rows(iLoop)("ISSELISIH")
            '        dsRekap.Anestesi = .Rows(iLoop)("ISANASTESI")
            '        dsRekap.Cathlab = .Rows(iLoop)("ISCATHLAB")
            '        dsRekap.CathlabCari = 0

            '        listTranskasiQuery_1.Add(dsRekap)

            '    End With
            'Next

        Catch oErr As Exception
            fn_SearchDetailTransaction = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub txtNO_TRANSAKSI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNO_TRANSAKSI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtNO_TRANSAKSI.Text = txtNO_TRANSAKSI.Text.ToString.PadLeft(7, "0")

            If fn_SearchTransaction(IIf(rbCATEGORY.SelectedIndex = 0, "01", "02"), txtNO_TRANSAKSI.Text.Trim.ToUpper) = True Then
                If fn_SearchDetailTransaction(IIf(rbCATEGORY.SelectedIndex = 0, "01", "02"), txtNO_TRANSAKSI.Text.Trim.ToUpper) = True Then
                    fn_LoadTransaksi()
                    txtNOSEP.Focus()

                    If txtCATEGORYBAYAR.Text = "BPJS" Then
                        txtNOSEP.Text = "1024R002" & deDATE.DateTime.ToString("MMyy") & "V"
                    End If

                End If
            End If

        End If
    End Sub
    Public Function InsertCustomer(ByVal KDCUSTOMER As String, ByVal Nama As String, ByVal Alamat As String) As Boolean
        InsertCustomer = True

        Dim oCustomer As New Reference.clsCustomer
        Try
            Dim dsCustomer = oCustomer.GetData(KDCUSTOMER)

            If dsCustomer IsNot Nothing Then
                fn_LoadCUSTOMER(dsCustomer.KDCUSTOMER)
                grdKDCUSTOMER.EditValue = oCustomer.GetData(dsCustomer.KDCUSTOMER).KDCUSTOMER
            Else
                'Dim ds = oCustomer.GetStructureHeader
                'With ds
                '    .DATECREATED = Now
                '    .DATEUPDATED = Now
                '    .KDCUSTOMER = KDCUSTOMER
                '    .KTP = ""
                '    .NAME_DISPLAY = Nama
                '    .EMAIL = ""
                '    .PHONE = ""
                '    .MOBILE = ""
                '    .FAX = ""
                '    .OTHER = ""
                '    .WEBSITE = ""
                '    .MEMO = ""
                '    .KDCOA = oCustomer.AccountDefault
                '    .ISACTIVE = True
                '    .ALAMAT = Alamat
                '    .KDKELURAHAN = 0
                '    .KODEPOS = ""
                '    .KDPANGKAT = oCustomer.PangkatDefault
                '    .KDGOLONGAN = oCustomer.GolonganDefault
                '    .KDPENDIDIKAN = oCustomer.PendidikanDefault
                '    .KDPEKERJAAN = oCustomer.PekerjaanDefault
                '    .KDPERUSAHAAN = oCustomer.PerusahaanDefault
                '    .KDAGAMA = oCustomer.AgamaDefault
                '    .KDJENISKELAMIN = 0
                '    .KDGOLONGANDARAH = 0
                '    .KDSTATUSKAWIN = 0
                '    .KDSUKU = oCustomer.SukuDefault
                '    .NRP = ""
                '    .NAMAKELUARGA = ""
                '    .KDSTATUSKELUARGA = oCustomer.StatusKeluargaDefault
                '    .TEMPATLAHIR = ""
                '    .TANGGALLAHIR = Now
                '    .WNI = 0
                '    .NEGARA = "INDONESIA"
                '    .KARTUBPJS = ""

                'End With

                '' ***** Kesatuan *****
                'Dim arrDetail_Kesatuan = oCustomer.GetStructureDetail_KesatuanList

                'Dim dsDetail_Kesatuan = oCustomer.GetStructureDetail_Kesatuan

                'With dsDetail_Kesatuan
                '    .DATECREATED = Now
                '    .DATEUPDATED = Now
                '    .KDCUSTOMER = ds.KDCUSTOMER
                '    .KDKESATUAN = "999"
                '    .JENIS = False
                '    .REMARKS = "-"
                '    .ISACTIVE = True
                'End With

                'arrDetail_Kesatuan.Add(dsDetail_Kesatuan)

                'InsertCustomer = oCustomer.InsertData(ds, KDCUSTOMER, arrDetail_Kesatuan)
                'fn_LoadCUSTOMER(KDCUSTOMER)
                'grdKDCUSTOMER.Text = oCustomer.GetData(KDCUSTOMER).KDCUSTOMER

                MsgBox("Tidak dapat Insert Customer Otomatis di Form Jasa")
            End If

        Catch ex As Exception
            InsertCustomer = False
            MsgBox(ex.ToString)
        End Try
    End Function
    Public Function InsertDepartment(ByVal KDDEPARTMENT As String, ByVal Nama As String) As Boolean
        InsertDepartment = True

        Dim oDepartment As New Reference.clsDepartment
        Dim oCustomer As New Reference.clsCustomer

        Try
            Dim dsDepartment = oDepartment.GetData(KDDEPARTMENT)
            If dsDepartment Is Nothing Then

                Dim ds = oDepartment.GetStructureHeader
                With ds
                    .DATECREATED = Now
                    .DATEUPDATED = Now
                    .KDDEPARTMENT = KDDEPARTMENT
                    .NAME_DISPLAY = Nama
                    .EMAIL = ""
                    .PHONE = ""
                    .MOBILE = ""
                    .FAX = ""
                    .OTHER = ""
                    .WEBSITE = ""
                    .BILL_STREET = ""
                    .BILL_CITY = ""
                    .BILL_STATE = ""
                    .BILL_ZIP = ""
                    .BILL_COUNTRY = ""
                    .MEMO = ""
                    .KDCOA = oCustomer.AccountDefault
                    .ISACTIVE = True
                End With

                InsertDepartment = oDepartment.InsertData(ds, KDDEPARTMENT)

                fn_LoadDEPARTMENT()

            End If

        Catch ex As Exception
            InsertDepartment = False
            MsgBox(ex.ToString)
        End Try
    End Function
    Public Function InsertDoctor(ByVal KDDOCTOR As String, ByVal Nama As String) As Boolean
        InsertDoctor = True

        Dim oDoctor As New Reference.clsDoctor
        Dim oCustomer As New Reference.clsCustomer

        Try
            Dim dsDoctor = oDoctor.GetData(KDDOCTOR)
            If dsDoctor Is Nothing Then

                Dim ds = oDoctor.GetStructureHeader
                With ds
                    .DATECREATED = Now
                    .DATEUPDATED = Now
                    .KDDOCTOR = KDDOCTOR
                    .NAME_DISPLAY = Nama
                    .EMAIL = ""
                    .PHONE = ""
                    .MOBILE = ""
                    .FAX = ""
                    .OTHER = ""
                    .WEBSITE = ""
                    .BILL_STREET = ""
                    .BILL_CITY = ""
                    .BILL_STATE = ""
                    .BILL_ZIP = ""
                    .BILL_COUNTRY = ""
                    .MEMO = ""
                    .KDCOA = oCustomer.AccountDefault
                    .ISACTIVE = True
                End With

                'InsertDoctor = oDoctor.InsertData(ds, KDDOCTOR)

                fn_LoadDOCTOR()

            End If

        Catch ex As Exception
            InsertDoctor = False
            MsgBox(ex.ToString)
        End Try
    End Function
    Public Function InsertItem(ByVal KD_PRODUK As String, ByVal NMITEM2 As String, ByVal PRICE As Decimal) As Boolean
        'InsertItem = True

        'Dim oItem As New Reference.clsItem

        'Try
        '    Dim dsDepartment = oItem.GetData(KD_PRODUK)
        '    If dsDepartment Is Nothing Then

        '        Dim ds = oItem.GetStructureHeader
        '        With ds
        '            .DATECREATED = Now
        '            .DATEUPDATED = Now
        '            .KDITEM = KD_PRODUK
        '            .NMITEM1 = ""
        '            .NMITEM2 = NMITEM2
        '            .NMITEM3 = ""
        '            .KDITEM_L1 = oItem.DefaultItem_L1
        '            .KDITEM_L2 = oItem.DefaultItem_L2
        '            .KDITEM_L3 = oItem.DefaultItem_L3
        '            .KDITEM_L4 = oItem.DefaultItem_L4
        '            .KDITEM_L5 = oItem.DefaultItem_L5
        '            .KDITEM_L6 = oItem.DefaultItem_L6
        '            .KDCOA_COST = oItem.AccountCostDefault
        '            .KDCOA_INVENTORY = oItem.AccountInventoryDefault
        '            .KDCOA_SALES = oItem.AccountSalesDefault
        '            .QTYMAXIMUM = PRICE
        '            .QTYMINIMUM = 0
        '            .ISTAX = False
        '            .ISPOINT = False
        '            .ISACTIVE = True
        '        End With

        '        ' ***** Satuan *****
        '        Dim arrDetail_UOM = oItem.GetStructureDetail_UOMList
        '        Dim oUOM As New Reference.clsUOM

        '        Dim dsDetail_UOM = oItem.GetStructureDetail_UOM
        '        With dsDetail_UOM
        '            .DATECREATED = Now
        '            .DATEUPDATED = Now
        '            .KDITEM = ds.KDITEM
        '            .KDUOM = oUOM.GetData().FirstOrDefault(Function(x) x.ISDEFAULT = True).KDUOM
        '            .RATE = CDec(30)
        '            .PRICEPURCHASESTANDARD = CDec(0)
        '            .PRICESALESSTANDARD = CDec(0)
        '            .MARGIN = PRICE * (30 / 100)
        '        End With

        '        arrDetail_UOM.Add(dsDetail_UOM)

        '        InsertItem = oItem.InsertData(ds, arrDetail_UOM, KD_PRODUK)

        '        fn_LoadITEM()

        '    End If

        'Catch ex As Exception
        '    InsertItem = False
        '    MsgBox(ex.ToString)
        'End Try
    End Function
    Private Sub txtCARIRM_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCARIRM.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Dim Data1 As String = ""
            Dim Data2 As String = ""
            Dim Data3 As String = ""
            Dim Data As String = ""

            Data = txtCARIRM.Text.ToString.PadLeft(6, "0")

            Data1 = Data.Substring(0, 2)
            Data2 = Data.Substring(2, 2)
            Data3 = Data.Substring(4, 2)

            txtCARIRM.Text = "0-" & Data1 & "-" & Data2 & "-" & Data3

            fn_SearchPasien("0-" & Data1 & "-" & Data2 & "-" & Data3)

        End If
    End Sub
    Private Function fn_SearchPasien(ByVal KD_PASIEN As String) As Boolean
        Try
            fn_SearchPasien = True

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            'Public sConnMySql As String = "Data Source=172.165.115.212;Initial Catalog=simrs_dustira;Persist Security Info=True;User ID=simrs;Password=8091016; Port = 3308;"
            'Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            Dim sConn As String = sConnGuntur

            oConn = New SqlConnection(sConn)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "Kasir = ISNULL((SELECT DESKRIPSI FROM KASIR WHERE A.KD_KASIR = KD_KASIR), '-') "
            SQL &= ",TanggalTranskasi = A.TGL_TRANSAKSI "
            SQL &= ",Unit = ISNULL((SELECT NAMA_UNIT FROM UNIT WHERE A.KD_UNIT = KD_UNIT), '-') "
            SQL &= ",Transkasi = A.NO_TRANSAKSI "
            SQL &= ",Pasien = (SELECT NAMA FROM PASIEN WHERE A.KD_PASIEN = KD_PASIEN)  "
            'SQL &= ",Alamat = B.ALAMAT "
            SQL &= ",TanggalPulang = A.TGL_CO "
            SQL &= "FROM "
            SQL &= "TRANSAKSI A "
            SQL &= "WHERE "
            SQL &= "A.KD_PASIEN = '" & KD_PASIEN & "' "
            SQL &= "AND " & IIf(rbCATEGORY.SelectedIndex = 0, "A.KD_KASIR = '01' ", "A.KD_KASIR = 02") & " "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "TRANSAKSIRS")

            If ds.Tables("TRANSAKSIRS").Rows.Count < 1 Then
                MsgBox("Pasien Tidak diTemukan", MsgBoxStyle.Information, Me.Text)
            End If

            grdCARIRM.Properties.DataSource = ds.Tables("TRANSAKSIRS")

            grdCARIRM.Properties.ValueMember = "Transkasi"
            grdCARIRM.Properties.DisplayMember = "Pasien"

            grdCARIRM.ShowPopup()

            grvCARIRM.BestFitColumns()

        Catch oErr As Exception
            fn_SearchPasien = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub grdCARIRM_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdCARIRM.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtNO_TRANSAKSI.Text = grdCARIRM.EditValue

            If fn_SearchTransaction(IIf(rbCATEGORY.SelectedIndex = 0, "01", "02"), txtNO_TRANSAKSI.Text.Trim.ToUpper) = True Then
                If fn_SearchDetailTransaction(IIf(rbCATEGORY.SelectedIndex = 0, "01", "02"), txtNO_TRANSAKSI.Text.Trim.ToUpper) = True Then
                    fn_LoadTransaksi()
                    txtNOSEP.Focus()
                    txtNOSEP.Text = "1024R002" & deDATE.DateTime.ToString("MMyy") & "V"
                End If
            End If

        End If
    End Sub
#End Region
End Class