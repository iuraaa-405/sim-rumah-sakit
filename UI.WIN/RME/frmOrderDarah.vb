Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmOrderDarah
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oOrderDarah As New Order.clsOrderDarah
    Private sKDIDENTITAS As Integer = 0
    Private sDiagnosa As String = ""
    Private sKDDOCTOR As String = ""
    Private sRuangan As String = ""
    Private sKDCUSTOMER As String = ""
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDIDENTITAS As String, ByVal kdcustomer As String, ByVal Diagnosa As String, ByVal kddoctor As String, ByVal ruangan As String, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId
        sKDIDENTITAS = KDIDENTITAS
        sDiagnosa = Diagnosa
        sKDDOCTOR = kddoctor
        sRuangan = ruangan
        sKDCUSTOMER = kdcustomer
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "Order Transfusi Darah"

            'lKDOrderDarah.Text = OrderDarah.KDOrderDarah
            lDATE.Text = "Tanggal Order * :"
            'lKDWAREHOUSE.Text = OrderDarah.KDWAREHOUSE & " *"

            'tab1.Text = OrderDarah.TAB_DETAIL
            'tab2.Text = OrderDarah.TAB_MEMO

            'grvDetail.Columns("KDITEM").Caption = OrderDarah.DETAIL_KDITEM
            'grvDetail.Columns("KDUOM").Caption = OrderDarah.DETAIL_KDUOM
            'grvDetail.Columns("QTY").Caption = OrderDarah.DETAIL_QTY
            'grvDetail.Columns("REMARKS").Caption = OrderDarah.DETAIL_REMARKS

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
        'sCode = txtKDOrderDarah.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDITEM()
        fn_LoadKDUOM()
        fn_LoadKDDOCTOR()

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

        MemoEdit1.Properties.ReadOnly = Status
        MemoEdit2.Properties.ReadOnly = Status
        MemoEdit3.Properties.ReadOnly = Status
        MemoEdit4.Properties.ReadOnly = Status
        MemoEdit5.Properties.ReadOnly = Status
        MemoEdit6.Properties.ReadOnly = Status
        MemoEdit7.Properties.ReadOnly = Status
        MemoEdit8.Properties.ReadOnly = Status
        MemoEdit9.Properties.ReadOnly = Status
        MemoEdit10.Properties.ReadOnly = Status
        MemoEdit11.Properties.ReadOnly = Status
        MemoEdit12.Properties.ReadOnly = Status
        MemoEdit13.Properties.ReadOnly = Status

        CheckEdit1.Properties.ReadOnly = Status
        CheckEdit2.Properties.ReadOnly = Status

        grvDetail.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDORDER.Text = "<--- AUTO --->"
        deDATE.DateTime = Now

        MemoEdit1.Text = sCompany
        MemoEdit2.ResetText()
        MemoEdit3.Text = sRuangan
        MemoEdit4.Text = sKDDOCTOR
        MemoEdit5.Text = sDiagnosa
        MemoEdit6.ResetText()
        MemoEdit7.ResetText()
        MemoEdit8.ResetText()
        MemoEdit9.ResetText()
        MemoEdit10.ResetText()
        MemoEdit11.ResetText()
        MemoEdit12.ResetText()
        MemoEdit13.ResetText()

        CheckEdit1.Checked = False
        CheckEdit2.Checked = False

        grvDetail.OptionsSelection.MultiSelect = True
        grvDetail.SelectAll()
        grvDetail.DeleteSelectedRows()
        grvDetail.OptionsSelection.MultiSelect = False
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oOrderDarah.GetData(sNoId)

            With ds
                txtKDORDER.Text = .KDORDERDARAH
                deDATE.DateTime = .DATE

                MemoEdit1.Text = .MemoEdit1
                MemoEdit2.Text = .MemoEdit2
                MemoEdit3.Text = .MemoEdit3
                MemoEdit4.Text = .MemoEdit4
                MemoEdit5.Text = .MemoEdit5
                MemoEdit6.Text = .MemoEdit6
                MemoEdit7.Text = .MemoEdit7
                MemoEdit8.Text = .MemoEdit8
                MemoEdit9.Text = .MemoEdit9
                MemoEdit10.Text = .MemoEdit10
                MemoEdit11.Text = .MemoEdit11
                MemoEdit12.Text = .MemoEdit12
                MemoEdit13.Text = .MemoEdit13

                CheckEdit1.Checked = .CheckEdit1
                CheckEdit2.Checked = .CheckEdit2


                BindingSource.DataSource = oOrderDarah.GetDataDetail(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = BindingSource

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
            Dim ds = oOrderDarah.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oOrderDarah.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .KDIDENTITAS = sKDIDENTITAS
                .KDORDERDARAH = sNoId
                .MemoEdit1 = MemoEdit1.Text
                .MemoEdit2 = MemoEdit2.Text
                .MemoEdit3 = MemoEdit3.Text
                .MemoEdit4 = MemoEdit4.EditValue
                .MemoEdit5 = MemoEdit5.Text
                .MemoEdit6 = MemoEdit6.Text
                .MemoEdit7 = MemoEdit7.Text
                .MemoEdit8 = MemoEdit8.Text
                .MemoEdit9 = MemoEdit9.Text
                .MemoEdit10 = MemoEdit10.Text
                .MemoEdit11 = MemoEdit11.Text
                .MemoEdit12 = MemoEdit12.Text
                .MemoEdit13 = MemoEdit13.Text
                .MemoEdit14 = MemoEdit4.Text
                .MemoEdit15 = ""
                .MemoEdit16 = ""
                .MemoEdit17 = ""
                .MemoEdit18 = ""
                .MemoEdit19 = ""
                .MemoEdit20 = ""
                .MemoEdit21 = ""
                .MemoEdit22 = ""
                .MemoEdit23 = ""
                .MemoEdit24 = ""
                .MemoEdit25 = ""
                .MemoEdit26 = ""
                .MemoEdit27 = ""
                .MemoEdit28 = ""
                .MemoEdit29 = ""
                .MemoEdit30 = ""
                .CheckEdit1 = CheckEdit1.Checked
                .CheckEdit2 = CheckEdit2.Checked
                .CheckEdit3 = False
                .CheckEdit4 = False
                .CheckEdit5 = False
                .CheckEdit6 = False
                .CheckEdit7 = False
                .CheckEdit8 = False
                .CheckEdit9 = False
                .CheckEdit10 = False
                .CheckEdit11 = False
                .CheckEdit12 = False
                .CheckEdit13 = False
                .CheckEdit14 = False
                .CheckEdit15 = False
                .CheckEdit16 = False
                .CheckEdit17 = False
                .CheckEdit18 = False
                .CheckEdit19 = False
                .CheckEdit20 = False
                .CheckEdit21 = False
                .CheckEdit22 = False
                .CheckEdit23 = False
                .CheckEdit24 = False
                .CheckEdit25 = False
                .CheckEdit26 = False
                .CheckEdit27 = False
                .CheckEdit28 = False
                .CheckEdit29 = False
                .CheckEdit30 = False
                .KDUSER = sUserID
                .MEMO = ""
            End With

            Dim oItem As New Reference.clsItem

            ' ***** DETIL *****
            Dim arrDetail = oOrderDarah.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oOrderDarah.GetStructureDetail
                With dsDetail
                    .DATECREATED = ds.DATECREATED
                    .DATEUPDATED = ds.DATEUPDATED
                    .DATE = CDate(IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colDATE)), Now, grvDetail.GetRowCellValue(i, colDATE)))
                    .KDORDERDARAH = ds.KDORDERDARAH
                    .SEQ = i
                    .KDITEM = grvDetail.GetRowCellValue(i, colKDITEM)
                    .NAMATINDAKAN = oItem.GetData(grvDetail.GetRowCellValue(i, colKDITEM)).NMITEM2
                    .GOLONGANDARAH = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colGOLONGANDARAH)), "", grvDetail.GetRowCellValue(i, colGOLONGANDARAH))
                    .KDUOM = grvDetail.GetRowCellValue(i, colKDUOM)
                    .JUMLAH = grvDetail.GetRowCellValue(i, colJUMLAH)
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
                    fn_Save = oOrderDarah.InsertData(ds, arrDetail)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oOrderDarah.UpdateData(ds, arrDetail)
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
            Dim oCustomer As New Reference.clsCustomer

            Try
                If grvDetail.GetFocusedRowCellValue(colKDITEM) IsNot Nothing Then
                    Dim ds = oItem.GetDataDetail_UOM(grvDetail.GetFocusedRowCellValue(colKDITEM))

                    If ds IsNot Nothing Then
                        grvDetail.SetFocusedRowCellValue(colKDUOM, ds.FirstOrDefault(Function(x) x.RATE = 1).KDUOM)

                        Dim dsCustomer = oCustomer.GetData(sKDCUSTOMER)

                        If dsCustomer IsNot Nothing Then
                            If dsCustomer.KDGOLONGANDARAH = "0" Then
                                grvDetail.SetFocusedRowCellValue(colGOLONGANDARAH, "A")
                            ElseIf dsCustomer.KDGOLONGANDARAH = "1"
                                grvDetail.SetFocusedRowCellValue(colGOLONGANDARAH, "B")
                            ElseIf dsCustomer.KDGOLONGANDARAH = "2"
                                grvDetail.SetFocusedRowCellValue(colGOLONGANDARAH, "AB")
                            ElseIf dsCustomer.KDGOLONGANDARAH = "3"
                                grvDetail.SetFocusedRowCellValue(colGOLONGANDARAH, "O")
                            Else
                                grvDetail.SetFocusedRowCellValue(colGOLONGANDARAH, "-")
                            End If

                        End If

                        grvDetail.SetFocusedRowCellValue(colDATE, Now)
                        grvDetail.SetFocusedRowCellValue(colJUMLAH, 1)
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
    Private Sub frmOrderDarah_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
            grdKDITEM.DataSource = oITEM.GetData.Where(Function(x) x.ISACTIVE = True And x.ISSTOK = False And x.M_ITEM_L3.MEMO.Contains("DARAH")).ToList()
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
    Private Sub fn_LoadKDDOCTOR()
        Dim oUOM As New Reference.clsDoctor
        Try
            MemoEdit4.Properties.DataSource = oUOM.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            MemoEdit4.Properties.ValueMember = "KDDOCTOR"
            MemoEdit4.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class