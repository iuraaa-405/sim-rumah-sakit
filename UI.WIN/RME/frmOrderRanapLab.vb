Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmOrderRanapLab
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oOrderRanapLab As New Order.clsOrderRanapLab
    Private sKDIDENTITAS As Integer = 0
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDIDENTITAS As String, ByVal Diagnosa As String, ByVal bb As String, ByVal tb As String, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId
        sKDIDENTITAS = KDIDENTITAS
        txtMEMO1.Text = Diagnosa
        txtBERATBADAN.Text = bb
        txtTINGGIBADAN.Text = tb
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "Order Laboratroium"

            'lKDOrderRanapLab.Text = OrderRanapLab.KDOrderRanapLab
            lDATE.Text = "Tanggal Order * :"
            'lKDWAREHOUSE.Text = OrderRanapLab.KDWAREHOUSE & " *"

            'tab1.Text = OrderRanapLab.TAB_DETAIL
            'tab2.Text = OrderRanapLab.TAB_MEMO

            'grvDetail.Columns("KDITEM").Caption = OrderRanapLab.DETAIL_KDITEM
            'grvDetail.Columns("KDUOM").Caption = OrderRanapLab.DETAIL_KDUOM
            'grvDetail.Columns("QTY").Caption = OrderRanapLab.DETAIL_QTY
            'grvDetail.Columns("REMARKS").Caption = OrderRanapLab.DETAIL_REMARKS

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
        'sCode = txtKDOrderRanapLab.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDITEM()
        fn_LoadKDUOM()
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
        grvDetail.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDORDER.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        txtINDIKASI.Text = "-"
        grvDetail.OptionsSelection.MultiSelect = True
        grvDetail.SelectAll()
        grvDetail.DeleteSelectedRows()
        grvDetail.OptionsSelection.MultiSelect = False
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oOrderRanapLab.GetData(sNoId)

            With ds
                txtKDORDER.Text = .KDORDER
                deDATE.DateTime = .TANGGALORDER

                bindingSource.DataSource = oOrderRanapLab.GetDataDetail(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = bindingSource

            End With

            Dim dsLainnya = oOrderRanapLab.GetDataLainnya(sNoId)
            With dsLainnya
                txtMEMO1.Text = .MEMO1
                txtBERATBADAN.Text = .BERATBADAN
                txtTINGGIBADAN.Text = .TINGGIBADAN
                txtINDIKASI.Text = .INDIKASI
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
            If txtTINGGIBADAN.Text = "" Then
                MsgBox("Tinggi Badan Kosong", MsgBoxStyle.Exclamation, Me.Text)
                txtTINGGIBADAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtTINGGIBADAN.ErrorText = Statement.ErrorRequired

                txtTINGGIBADAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtBERATBADAN.Text = "" Then
                MsgBox("Berat Badan Kosong", MsgBoxStyle.Exclamation, Me.Text)
                txtBERATBADAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtBERATBADAN.ErrorText = Statement.ErrorRequired

                txtBERATBADAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtINDIKASI.Text = "" Then
                MsgBox("Indikasi Tindakan Kosong", MsgBoxStyle.Exclamation, Me.Text)
                txtINDIKASI.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtINDIKASI.ErrorText = Statement.ErrorRequired

                txtINDIKASI.Focus()
                fn_Validate = False
                Exit Function
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
            Dim ds = oOrderRanapLab.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oOrderRanapLab.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .TANGGALORDER = deDATE.DateTime
                .JENISORDER = "LABRI"
                .KDORDER = sNoId
                .KDIDENTITAS = sKDIDENTITAS
                .NOMORREFERENCE = ""
                .USERORDER = sUserID
                Try
                    .STATUS = oOrderRanapLab.GetData(sNoId).STATUS
                Catch oErr As Exception
                    .STATUS = "TAMBAH"
                End Try
                .MEMO = "ORDER LABORATORIUM"
            End With

            Dim dsLainnya = oOrderRanapLab.GetStructureHeaderLainnya
            With dsLainnya
                .KDORDER = ds.KDORDER
                .INDIKASI = txtINDIKASI.Text
                .TINGGIBADAN = txtTINGGIBADAN.Text
                .BERATBADAN = txtBERATBADAN.Text
                .MEMO1 = txtMEMO1.Text
                .MEMO2 = ""
                .MEMO3 = ""
                .MEMO4 = ""
                .MEMO5 = ""
            End With

            Dim oItem As New Reference.clsItem

            ' ***** DETIL *****
            Dim arrDetail = oOrderRanapLab.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oOrderRanapLab.GetStructureDetail
                With dsDetail
                    .DATECREATED = ds.DATECREATED
                    .DATEUPDATED = ds.DATEUPDATED
                    .KDORDER = ds.KDORDER
                    .SEQ = i
                    .KDITEM = grvDetail.GetRowCellValue(i, colKDITEM)
                    .NAMATINDAKAN = oItem.GetData(grvDetail.GetRowCellValue(i, colKDITEM)).NMITEM2
                    .KDUOM = grvDetail.GetRowCellValue(i, colKDUOM)
                    .JUMLAH = CDec(1)
                    .HARGA = CDec(0)
                    .TOTAL = CDec(0)
                    .MEMO = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colREMARKS)), "", grvDetail.GetRowCellValue(i, colREMARKS))
                    .KDUSER = sUserID
                    .ISBACA = 0
                    .ISPERAWAT = False
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oOrderRanapLab.InsertData(ds, arrDetail, dsLainnya)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oOrderRanapLab.UpdateData(ds, arrDetail, dsLainnya)
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
        If e.Column.Name = colKDITEM.Name Then
            Dim oItem As New Reference.clsItem
            Try
                If grvDetail.GetFocusedRowCellValue(colKDITEM) IsNot Nothing Then
                    Dim ds = oItem.GetDataDetail_UOM(grvDetail.GetFocusedRowCellValue(colKDITEM))

                    If ds IsNot Nothing Then
                        grvDetail.SetFocusedRowCellValue(colKDUOM, ds.FirstOrDefault(Function(x) x.RATE = 1).KDUOM)
                    Else
                        MsgBox(Statement.ErrorUOM, MsgBoxStyle.Exclamation, Me.Text)

                        grvDetail.CancelUpdateCurrentRow()
                    End If
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf e.Column.Name = colKDUOM.Name Then
            Dim oItem As New Reference.clsItem
            Try
                If grvDetail.GetFocusedRowCellValue(colKDITEM) IsNot Nothing And grvDetail.GetFocusedRowCellValue(colKDUOM) IsNot Nothing Then
                    Dim ds = oItem.GetDataDetail_UOM(grvDetail.GetFocusedRowCellValue(colKDITEM), grvDetail.GetFocusedRowCellValue(colKDUOM))

                    If ds IsNot Nothing Then

                    Else
                        MsgBox(Statement.ErrorUOM, MsgBoxStyle.Exclamation, Me.Text)

                        Dim sItem = grvDetail.GetFocusedRowCellValue(colKDITEM)
                        grvDetail.CancelUpdateCurrentRow()

                        grvDetail.AddNewRow()
                        grvDetail.SetFocusedRowCellValue(colKDITEM, sItem)
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
    Private Sub frmOrderRanapLab_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    Private Sub fn_LoadKDITEM()
        Dim oITEM As New Reference.clsItem
        Try
            grdKDITEM.DataSource = oITEM.GetData.Where(Function(x) x.ISACTIVE = True And x.ISSTOK = False And x.M_ITEM_L3.MEMO.Contains("LABORATORIUM")).ToList()
            grdKDITEM.ValueMember = "KDITEM"
            grdKDITEM.DisplayMember = "NMITEM2"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDUOM()
        Dim oUOM As New Reference.clsUOM
        Try
            grdKDUOM.DataSource = oUOM.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDUOM.ValueMember = "KDUOM"
            grdKDUOM.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class