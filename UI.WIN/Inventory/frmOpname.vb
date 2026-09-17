Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmOpname
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oOpname As New Inventory.clsOpname
    Private oPI As New Purchasing.clsPurchaseInvoice
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
            Me.Text = Opname.TITLE

            lKDOPNAME.Text = Opname.KDOPNAME
            lDATE.Text = Opname.TANGGAL
            lKDWAREHOUSE.Text = Opname.KDWAREHOUSE & " *"

            tab1.Text = Opname.TAB_DETAIL
            tab2.Text = Opname.TAB_MEMO

            grvDetail.Columns("KDITEM").Caption = Opname.DETAIL_KDITEM
            grvDetail.Columns("KDUOM").Caption = Opname.DETAIL_KDUOM
            grvDetail.Columns("QTY").Caption = Opname.DETAIL_QTY
            grvDetail.Columns("REMARKS").Caption = Opname.DETAIL_REMARKS

            grvKDWAREHOUSE.Columns("NAME_DISPLAY").Caption = Warehouse.NAME_DISPLAY

            'grvKDITEM.Columns("NMITEM1").Caption = Item.NMITEM1_2
            grvKDITEM.Columns("NMITEM2").Caption = Item.NMITEM2_2
            'grvKDITEM.Columns("NMITEM3").Caption = Item.NMITEM3_2

            grvKDUOM.Columns("MEMO").Caption = UOM.MEMO

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDOPNAME.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDWAREHOUSE()
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
        grdKDWAREHOUSE.Properties.ReadOnly = Status

        txtMEMO.Properties.ReadOnly = Status

        grvDetail.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDOPNAME.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        grdKDWAREHOUSE.ResetText()
        txtMEMO.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oOpname.GetData(sNoId)

            With ds
                txtKDOPNAME.Text = .KDOPNAME
                deDATE.DateTime = .DATE
                grdKDWAREHOUSE.Text = .KDWAREHOUSE
                txtMEMO.Text = .MEMO

                bindingSource.DataSource = oOpname.GetDataDetail.Where(Function(x) x.KDOPNAME = sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = bindingSource
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If grdKDWAREHOUSE.Text = String.Empty Then
                grdKDWAREHOUSE.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDWAREHOUSE.ErrorText = Statement.ErrorRequired

                grdKDWAREHOUSE.Focus()
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
            Dim ds = oOpname.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oOpname.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDOPNAME = sNoId
                .DATE = deDATE.DateTime
                .KDWAREHOUSE = IIf(String.IsNullOrEmpty(grdKDWAREHOUSE.EditValue), String.Empty, grdKDWAREHOUSE.EditValue)
                .MEMO = txtMEMO.Text.Trim.ToUpper
                .KDUSER = sUserID
            End With

            ' ***** DETIL *****
            Dim arrDetail = oOpname.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oOpname.GetStructureDetail
                With dsDetail
                    Try
                        .DATECREATED = oOpname.GetData(sNoId).DATECREATED
                    Catch oErr As Exception
                        .DATECREATED = Now
                    End Try
                    .DATEUPDATED = Now

                    .SEQ = i
                    .KDOPNAME = ds.KDOPNAME
                    .KDITEM = grvDetail.GetRowCellValue(i, colKDITEM)
                    .KDUOM = grvDetail.GetRowCellValue(i, colKDUOM)
                    .QTYFISIK = CDec(grvDetail.GetRowCellValue(i, colQTYFISIK))
                    .QTYKOMPUTER = CDec(grvDetail.GetRowCellValue(i, colQTYKOMPUTER))
                    .QTY = CDec(grvDetail.GetRowCellValue(i, colQTY))
                    .REMARKS = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colREMARKS)), "-", grvDetail.GetRowCellValue(i, colREMARKS))
                    .TANGGALEXPIRE = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colTANGGALEXPIRE)), Now, grvDetail.GetRowCellValue(i, colTANGGALEXPIRE))
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oOpname.InsertData(ds, arrDetail)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oOpname.UpdateData(ds, arrDetail)
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
        If grdKDWAREHOUSE.Text <> "" Then
            If e.Column.Name = colKDITEM.Name Then
                Dim oItem As New Reference.clsItem
                Try
                    If grvDetail.GetFocusedRowCellValue(colKDITEM) IsNot Nothing Then
                        Dim ds = oItem.GetDataDetail_UOM(grvDetail.GetFocusedRowCellValue(colKDITEM))

                        If ds IsNot Nothing Then
                            grvDetail.SetFocusedRowCellValue(colKDUOM, ds.FirstOrDefault(Function(x) x.RATE = 1).KDUOM)
                            'grvDetail.SetFocusedRowCellValue(colTANGGALEXPIRE, Now)

                            Dim ds2 = oItem.GetDataDetail_WAREHOUSE(grvDetail.GetFocusedRowCellValue(colKDITEM), grdKDWAREHOUSE.EditValue, grvDetail.GetFocusedRowCellValue(colKDUOM))

                            If ds2 IsNot Nothing Then
                                grvDetail.SetFocusedRowCellValue(colQTYKOMPUTER, ds2.AMOUNT)
                            Else
                                grvDetail.SetFocusedRowCellValue(colQTYKOMPUTER, 0)
                            End If
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
                            Dim dsExpirePI = oPI.GetDataPembelianTerakhir(ds.KDITEM, ds.KDUOM)
                            If dsExpirePI IsNot Nothing Then
                                grvDetail.SetFocusedRowCellValue(colTANGGALEXPIRE, dsExpirePI.TANGGALEXPIRE)
                            Else
                                grvDetail.SetFocusedRowCellValue(colTANGGALEXPIRE, Now)
                            End If
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
                'ElseIf e.Column.Name = colQTY.Name Then
                '    If CDec(grvDetail.GetFocusedRowCellValue(colQTY)) = 0 Then
                '        grvDetail.SetFocusedRowCellValue(colQTY, 1)
                '    End If
            ElseIf e.Column.Name = colQTYFISIK.Name Or e.Column.Name = colQTYKOMPUTER.Name Then
                Try
                    Dim sSelisih As Decimal = (CDec(grvDetail.GetFocusedRowCellValue(colQTYFISIK)) - CDec(grvDetail.GetFocusedRowCellValue(colQTYKOMPUTER)))

                    grvDetail.SetFocusedRowCellValue(colQTY, sSelisih)
                Catch ex As Exception
                    grvDetail.SetFocusedRowCellValue(colQTY, 0)
                End Try
            End If
        End If
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail.DeleteSelectedRows()
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmOpname_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    Private Sub fn_LoadKDWAREHOUSE()
        Dim oWAREHOUSE As New Reference.clsWarehouse
        Try
            grdKDWAREHOUSE.Properties.DataSource = oWAREHOUSE.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDWAREHOUSE.Properties.ValueMember = "KDWAREHOUSE"
            grdKDWAREHOUSE.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDWAREHOUSE_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdKDWAREHOUSE.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdKDWAREHOUSE.ResetText()
        End If
    End Sub
    Private Sub fn_LoadKDITEM()
        'Dim oITEM As New Reference.clsItem
        'Try
        '    grdKDITEM.DataSource = oITEM.GetData.Where(Function(x) x.ISACTIVE = True And x.ISSTOK = True).ToList()
        '    grdKDITEM.ValueMember = "KDITEM"
        '    grdKDITEM.DisplayMember = "NMITEM2"
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
        If grdKDWAREHOUSE.Text = String.Empty Then Exit Sub

        'Try
        '    Dim oConn As New SqlConnection
        '    Dim oComm As New SqlCommand
        '    Dim da As SqlDataAdapter
        '    Dim ds As New DataSet
        '    Dim SQL As String

        '    Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

        '    oConn = New SqlConnection(sConn)
        '    If oConn.State = ConnectionState.Closed Then
        '        oConn.Open()
        '    End If

        '    SQL = "SELECT "
        '    SQL &= "A.KDITEM "
        '    SQL &= ",A.NMITEM2 "
        '    SQL &= ",STOK = B.AMOUNT "
        '    SQL &= "FROM "
        '    SQL &= "M_ITEM A "
        '    SQL &= "INNER JOIN M_ITEM_WAREHOUSE B "
        '    SQL &= "ON A.KDITEM = B.KDITEM "
        '    SQL &= "INNER JOIN M_ITEM_UOM C "
        '    SQL &= "ON A.KDITEM = C.KDITEM AND B.KDITEM = C.KDITEM "
        '    SQL &= "WHERE C.RATE = 1 "
        '    SQL &= "AND A.ISACTIVE = 1 "
        '    SQL &= "AND A.ISSTOK = 1 "
        '    SQL &= "AND B.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "

        '    oComm.Connection = oConn
        '    oComm.CommandText = SQL
        '    oComm.CommandTimeout = 120
        '    oComm.CommandType = CommandType.Text

        '    da = New SqlDataAdapter(oComm)
        '    da.Fill(ds, "ITEM")

        '    grdKDITEM.DataSource = ds.Tables("ITEM")
        '    grdKDITEM.ValueMember = "KDITEM"
        '    grdKDITEM.DisplayMember = "NMITEM2"

        '    If oConn.State = ConnectionState.Open Then
        '        oConn.Close()
        '    End If

        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "A.KDITEM "
            SQL &= ",A.NMITEM1 "
            SQL &= ",A.NMITEM2"
            SQL &= ",A.NMITEM3 "
            SQL &= ",STOK = ISNULL((SELECT AA.AMOUNT FROM M_ITEM_WAREHOUSE AA INNER JOIN M_ITEM_UOM BB ON AA.KDITEM = BB.KDITEM AND AA.KDUOM = BB.KDUOM WHERE A.KDITEM = AA.KDITEM AND AA.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' AND BB.RATE =1), 0 ) "
            SQL &= "FROM "
            SQL &= "M_ITEM A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "
            SQL &= "AND A.ISSTOK = 1 "

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
    Private Sub grdKDWAREHOUSE_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDWAREHOUSE.EditValueChanged
        fn_LoadKDITEM()
    End Sub
    Private Sub btnClean_Click(sender As Object, e As EventArgs) Handles btnClean.Click
        Dim oItem As New Reference.clsItem
        Try
            If grdKDWAREHOUSE.Text = "" Then
                MsgBox("Pilih Depo !!!", MsgBoxStyle.Exclamation, Me.Text)
                grdKDWAREHOUSE.Focus()
                Exit Sub
            End If

            If MsgBox("Apakah yakin akan di clean ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

            grvDetail.OptionsSelection.MultiSelect = True
            grvDetail.SelectAll()
            grvDetail.DeleteSelectedRows()
            grvDetail.OptionsSelection.MultiSelect = False

            fn_MutasiGudangAdjustment()

            For Each iLoop In oItem.GetDataDetail_WAREHOUSEList(grdKDWAREHOUSE.EditValue)
                grvDetail.AddNewRow()
                grvDetail.SetFocusedRowCellValue(colKDITEM, iLoop.KDITEM)
                grvDetail.SetFocusedRowCellValue(colKDUOM, iLoop.KDUOM)
                grvDetail.SetFocusedRowCellValue(colQTYKOMPUTER, iLoop.AMOUNT)
                grvDetail.SetFocusedRowCellValue(colQTYFISIK, 0)
                grvDetail.UpdateCurrentRow()
            Next

            txtMEMO.Text = "CLEAN"
            txtMEMO.Properties.ReadOnly = True

            colKDITEM.OptionsColumn.AllowFocus = True
            colKDITEM.OptionsColumn.AllowEdit = True
            colKDITEM.OptionsColumn.ReadOnly = True
            colKDITEM.OptionsColumn.TabStop = True

            colQTYFISIK.OptionsColumn.AllowFocus = True
            colQTYFISIK.OptionsColumn.AllowEdit = True
            colQTYFISIK.OptionsColumn.ReadOnly = True
            colQTYFISIK.OptionsColumn.TabStop = True
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_MutasiGudangAdjustment()
        Try
            If grdKDWAREHOUSE.Text = String.Empty Then Exit Sub

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= ",A.KDUOM "
            SQL &= ",STOK = Z.BELI + Z.RETURBELI + Z.MUTASIKELUAR + Z.MUTASIMASUK + Z.PENJUALAN + Z.PENJUALAN_TANPARESEP + Z.OPNAME + Z.ADJUSMENT "
            SQL &= "FROM ( "
            SQL &= "SELECT "
            SQL &= "DISTINCT A.KDITEM "
            SQL &= ",BELI = ISNULL(B.TOTAL, 0) "
            SQL &= ",RETURBELI = ISNULL(C.TOTAL, 0) "
            SQL &= ",MUTASIKELUAR = ISNULL(D.TOTAL, 0) "
            SQL &= ",MUTASIMASUK = ISNULL(E.TOTAL, 0) "
            SQL &= ",PENJUALAN = ISNULL(F.TOTAL, 0) "
            SQL &= ",OPNAME = ISNULL(G.TOTAL, 0) "
            SQL &= ",ADJUSMENT = ISNULL(H.TOTAL, 0) "
            SQL &= ",PENJUALAN_TANPARESEP = ISNULL(I.TOTAL, 0) "
            SQL &= "FROM "
            SQL &= "(SELECT KDITEM FROM M_ITEM) AS A "
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = SUM(A.QTY * C.RATE) "
            SQL &= "FROM P_PI_D AS A INNER JOIN P_PI_H AS B ON A.KDPI = B.KDPI INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE B.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "GROUP BY A.KDITEM) AS B	 "
            SQL &= "ON A.KDITEM = B.KDITEM "
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = - SUM(A.QTY * C.RATE) "
            SQL &= "FROM P_PR_D AS A INNER JOIN P_PR_H AS B ON A.KDPR = B.KDPR INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE B.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "GROUP BY A.KDITEM) AS C	 "
            SQL &= "ON A.KDITEM = C.KDITEM "
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = - SUM(A.QTY * C.RATE) "
            SQL &= "FROM I_MUTATION_D AS A INNER JOIN I_MUTATION_H AS B ON A.KDMUTATION = B.KDMUTATION INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE B.KDWAREHOUSEFROM = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "GROUP BY A.KDITEM) AS D	 "
            SQL &= "ON A.KDITEM = D.KDITEM "
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = SUM(A.QTY * C.RATE) "
            SQL &= "FROM I_TERIMA_D AS A INNER JOIN I_TERIMA_H AS B ON A.KDTERIMA = B.KDTERIMA INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE B.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "GROUP BY A.KDITEM) AS E	 "
            SQL &= "ON A.KDITEM = E.KDITEM "
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = - SUM(A.QTY * C.RATE) "
            SQL &= "FROM S_SO_TRANSAKSI_D AS A INNER JOIN S_SO_TRANSAKSI_H AS B ON A.KDSOTRANSAKSI = B.KDSOTRANSAKSI INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE B.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "GROUP BY A.KDITEM) AS F	 "
            SQL &= "ON A.KDITEM = F.KDITEM "
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = SUM(A.QTY * C.RATE) "
            SQL &= "FROM I_OPNAME_D AS A INNER JOIN I_OPNAME_H AS B ON A.KDOPNAME = B.KDOPNAME INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE B.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "GROUP BY A.KDITEM) AS G	 "
            SQL &= "ON A.KDITEM = G.KDITEM "
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = SUM(A.QTY * C.RATE) "
            SQL &= "FROM I_ADJUSTMENT_D AS A INNER JOIN I_ADJUSTMENT_H AS B ON A.KDADJUSTMENT = B.KDADJUSTMENT INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE B.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "GROUP BY A.KDITEM) AS H	 "
            SQL &= "ON A.KDITEM = H.KDITEM "
            SQL &= "LEFT JOIN "
            SQL &= "(SELECT A.KDITEM, TOTAL = - SUM(A.QTY * C.RATE) "
            SQL &= "FROM S_SO_TANPARESEP_D AS A INNER JOIN S_SO_TANPARESEP_H AS B ON A.KDSOTANPARESEP = B.KDSOTANPARESEP INNER JOIN M_ITEM_UOM AS C ON A.KDITEM = C.KDITEM AND A.KDUOM = C.KDUOM "
            SQL &= "WHERE B.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "GROUP BY A.KDITEM) AS I	 "
            SQL &= "ON A.KDITEM = I.KDITEM "
            SQL &= ") Z "
            SQL &= "INNER JOIN M_ITEM_UOM A "
            SQL &= "ON Z.KDITEM = A.KDITEM "
            SQL &= "INNER JOIN M_ITEM_WAREHOUSE B "
            SQL &= "ON A.KDITEM = B.KDITEM AND A.KDUOM = B.KDUOM "
            SQL &= "WHERE "
            SQL &= "Z.BELI + Z.RETURBELI + Z.MUTASIKELUAR + Z.MUTASIMASUK + Z.PENJUALAN + Z.PENJUALAN_TANPARESEP + Z.OPNAME + Z.ADJUSMENT <> 0 "
            SQL &= "AND A.RATE = 1 "
            SQL &= "AND B.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "AND B.AMOUNT <> 0 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "M_ITEM_WAREHOUSE")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            If ds.Tables("M_ITEM_WAREHOUSE").Rows.Count < 0 Then
                Exit Sub
            End If

            For iLoop As Integer = 0 To ds.Tables("M_ITEM_WAREHOUSE").Rows.Count - 1
                With ds.Tables("M_ITEM_WAREHOUSE")
                    oOpname.UpdateAmount(.Rows(iLoop)("KDITEM"), .Rows(iLoop)("KDUOM"), grdKDWAREHOUSE.EditValue, .Rows(iLoop)("STOK"))
                End With
            Next
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class