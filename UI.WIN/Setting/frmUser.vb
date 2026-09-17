Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmUser
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oUser As New Setting.clsUser
    Private oWarehouse As New Reference.clsWarehouse
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
            Me.Text = User.TITLE

            lKDUSER.Text = User.KDUSER
            lPASSWORD1.Text = User.PASSWORD1
            lPASSWORD2.Text = User.PASSWORD2
            lKDOTORITY.Text = User.KDOTORITY
            chkISACTIVE.Text = User.ISACTIVE

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDUSER.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDOTORITY()
        fn_LoadWAREHOUSE()
        fn_LoadITEM()
        fn_LoadDPJP()

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
                txtKDUSER.Properties.ReadOnly = True
            Case Else
                fn_ViewMode(True)
        End Select
    End Sub
    Private Sub fn_ViewMode(ByVal Status As Boolean)
        btnSaveNew.Enabled = Not Status
        btnSaveClose.Enabled = Not Status

        txtKDUSER.Properties.ReadOnly = Status
        txtPASSWORD1.Properties.ReadOnly = Status
        txtPASSWORD2.Properties.ReadOnly = Status
        grdKDOTORITY.Properties.ReadOnly = Status
        grdDPJP.Properties.ReadOnly = Status

        chkISACTIVE.Properties.ReadOnly = Status
        grvDetail_WAREHOUSE.OptionsBehavior.ReadOnly = Status
        grvDetailItem.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDUSER.ResetText()
        txtPASSWORD1.ResetText()
        txtPASSWORD2.ResetText()
        grdKDOTORITY.ResetText()
        txtNIK.ResetText()
        grdDPJP.ResetText()

        chkISACTIVE.Checked = True
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oUser.GetData(sNoId)

            With ds
                txtKDUSER.Text = .KDUSER
                txtPASSWORD1.Text = .PASSWORD
                txtPASSWORD2.Text = .PASSWORD
                grdKDOTORITY.Text = .KDOTORITY
                chkISACTIVE.Checked = .ISACTIVE
                txtNIK.Text = .NIK
                grdDPJP.Text = .KDDOCTOR

                BindingSource_WAREHOUSE.DataSource = oWarehouse.GetDataDetail_WAREHOUSE.Where(Function(x) x.KDUSER = sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail_WAREHOUSE.DataSource = BindingSource_WAREHOUSE

                BindingSourceiTEM.DataSource = oWarehouse.GetDataDetail_ITEM.Where(Function(x) x.KDUSER = sNoId).OrderBy(Function(x) x.SEQ).ToList()

                grdDetailItem.DataSource = BindingSourceiTEM
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If txtKDUSER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight Then
                txtKDUSER.ErrorText = Statement.ErrorRequired
                txtKDUSER.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtPASSWORD1.Text = String.Empty Then
                txtPASSWORD1.ErrorText = Statement.ErrorRequired
                txtPASSWORD1.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtPASSWORD2.Text = String.Empty Then
                txtPASSWORD2.ErrorText = Statement.ErrorRequired
                txtPASSWORD2.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtPASSWORD1.Text <> txtPASSWORD2.Text Then
                txtPASSWORD2.ErrorText = Statement.ErrorPasswordRepeat
                txtPASSWORD2.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtNIK.Text = String.Empty Then
                txtNIK.ErrorText = Statement.ErrorPasswordRepeat
                txtNIK.Focus()
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
            Dim ds = oUser.GetStructureHeader
            With ds
                .KDUSER = txtKDUSER.Text.Trim.ToUpper
                .KDSERVER = sKDSERVER
                .KDDATABASE = sKDDATABASE
                .KDSERVER_TAX = sKDSERVER_TAX
                .KDDATABASE_TAX = sKDDATABASE_TAX
                .KDOTORITY = grdKDOTORITY.EditValue
                .KDCOMPANY = sKDCOMPANY
                .PASSWORD = txtPASSWORD1.Text.Trim
                .ISACTIVE = chkISACTIVE.Checked
                .NIK = txtNIK.Text
                .KDDOCTOR = grdDPJP.EditValue
                Try
                    .SKINDEFAULT = oUser.GetData(txtKDUSER.Text.Trim.ToUpper).SKINDEFAULT
                Catch ex As Exception
                    .SKINDEFAULT = ""
                End Try
                Try
                    .KDDATABASE_NEW = oUser.GetData(txtKDUSER.Text.Trim.ToUpper).KDDATABASE_NEW
                Catch ex As Exception
                    .KDDATABASE_NEW = sKDDATABASE_NEW
                End Try
                Try
                    .KDSERVER_NEW = oUser.GetData(txtKDUSER.Text.Trim.ToUpper).KDSERVER_NEW
                Catch ex As Exception
                    .KDSERVER_NEW = sKDSERVER_NEW
                End Try
            End With

            ' ***** Warehouse *****
            Dim arrDetail_WAREHOUSE = oWarehouse.GetStructureDetail_WAREHOUSEList
            For i As Integer = 0 To grvDetail_WAREHOUSE.RowCount - 2
                Dim dsDetail_DOCTOR = oWarehouse.GetStructureDetail_WAREHOUSE
                With dsDetail_DOCTOR
                    Try
                        .DATECREATED = Now
                    Catch ex As Exception
                        .DATECREATED = Now
                    End Try
                    .DATEUPDATED = Now
                    .KDUSER = txtKDUSER.Text.Trim.ToUpper
                    .KDWAREHOUSE = grvDetail_WAREHOUSE.GetRowCellValue(i, colKDWAREHOUSE)
                    .SEQ = i
                    .ISDEFAULT = grvDetail_WAREHOUSE.GetRowCellValue(i, colISDEFAULT)
                End With
                arrDetail_WAREHOUSE.Add(dsDetail_DOCTOR)
            Next
            ' ***** iTEM *****
            Dim arrDetailITEM = oWarehouse.GetStructureDetail_ITEMList
            For i As Integer = 0 To grvDetailItem.RowCount - 2
                Dim dsDetail_ITEM = oWarehouse.GetStructureDetail_ITEM
                With dsDetail_ITEM
                    Try
                        .DATECREATED = Now
                    Catch ex As Exception
                        .DATECREATED = Now
                    End Try
                    .DATEUPDATED = Now
                    .KDUSER = txtKDUSER.Text.Trim.ToUpper
                    .KDITEM = grvDetailItem.GetRowCellValue(i, colKDITEM)
                    .KDUOM = ""
                    .SEQ = i
                    .ISDEFAULT = grvDetailItem.GetRowCellValue(i, colISDEFAULT)
                End With
                arrDetailITEM.Add(dsDetail_ITEM)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oUser.InsertData(ds)
                    If arrDetail_WAREHOUSE IsNot Nothing Then
                        fn_Save = oWarehouse.InsertDataWarehouseUser(arrDetail_WAREHOUSE)
                    End If
                    If arrDetailITEM IsNot Nothing Then
                        fn_Save = oWarehouse.InsertDataItemUser(arrDetailITEM)
                    End If
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oUser.UpdateData(ds)
                    If arrDetail_WAREHOUSE IsNot Nothing Then
                        fn_Save = oWarehouse.UpdateDataWarehouseUser(arrDetail_WAREHOUSE, txtKDUSER.Text.ToString)
                    End If
                    If arrDetailITEM IsNot Nothing Then
                        fn_Save = oWarehouse.UpdateDataItemUser(arrDetailITEM, txtKDUSER.Text.ToString)
                    End If
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail_WAREHOUSE.DeleteSelectedRows()
    End Sub
    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetailItem.DeleteSelectedRows()
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmUser_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    Private Sub fn_LoadDPJP()
        Dim oDPJP As New Reference.clsDoctor
        Try
            grdDPJP.Properties.DataSource = oDPJP.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdDPJP.Properties.ValueMember = "KDDOCTOR"
            grdDPJP.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDOTORITY()
        Dim oOTORITY As New Setting.clsOtority
        Try
            grdKDOTORITY.Properties.DataSource = oOTORITY.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDOTORITY.Properties.ValueMember = "KDOTORITY"
            grdKDOTORITY.Properties.DisplayMember = "KDOTORITY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDOTORITY_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdKDOTORITY.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdKDOTORITY.ResetText()
        End If
    End Sub
    Private Sub fn_LoadWAREHOUSE()
        Dim oWAREHOUSE As New Reference.clsWarehouse
        Try
            grdKDWAREHOUSE.DataSource = oWAREHOUSE.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDWAREHOUSE.ValueMember = "KDWAREHOUSE"
            grdKDWAREHOUSE.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadITEM()
        Dim oITEM As New Reference.clsItem

        Try
            Dim ds = From x In oITEM.GetData
                     Where x.ISACTIVE = True And x.ISSTOK = False
                     Select x.KDITEM, x.NMITEM2, KELOMPOK = x.M_ITEM_L2.MEMO

            grdKDITEM.DataSource = ds.ToList()
            grdKDITEM.ValueMember = "KDITEM"
            grdKDITEM.DisplayMember = "NMITEM2"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub


#End Region
End Class