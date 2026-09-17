Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmCPPT_TemplateProsedur
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oCPPT_TemplateProsedur As New Template.clsCPPT_TemplateProsedur
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
            Me.Text = CPPT_TemplateProsedur.TITLE

            lMEMO.Text = CPPT_TemplateProsedur.MEMO & " *"
            lKDITEM_L2.Text = sItem_L2 & " :"

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
        fn_LoadKDITEM()
        fn_LoadKDITEMSearch()
        fn_LoadITEM_L2()
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

        txtMEMO.Properties.ReadOnly = Status
        chkISDELETE.Checked = True
    End Sub
    Private Sub fn_EmptyMe()
        txtMEMO.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oCPPT_TemplateProsedur.GetData(sNoId)

            With ds
                txtMEMO.Text = .MEMO
                chkISDELETE.Checked = .ISDELETE

                BindingSourceCPPT_Tindakan.DataSource = oCPPT_TemplateProsedur.GetDataDetail(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdCPPT_Tindakan.DataSource = BindingSourceCPPT_Tindakan
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtMEMO.Text = String.Empty Then
                txtMEMO.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtMEMO.ErrorText = Statement.ErrorRequired

                txtMEMO.Focus()
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
            Dim ds = oCPPT_TemplateProsedur.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oCPPT_TemplateProsedur.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDTEMPLATE = sNoId
                .MEMO = txtMEMO.Text.Trim.ToUpper
                .KDUSER = sUserID
                .ISDELETE = chkISDELETE.Checked
            End With

            ' ***** DETIL *****
            Dim arrDetail = oCPPT_TemplateProsedur.GetStructureDetailList
            For i As Integer = 0 To grvCPPT_Tindakan.RowCount - 2
                Dim dsDetail = oCPPT_TemplateProsedur.GetStructureDetail
                With dsDetail
                    .DATECREATED = Now
                    .DATEUPDATED = Now
                    .KDTEMPLATE = ds.KDTEMPLATE
                    .SEQ = i
                    .KDITEM = grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_KDITEMTINDAKAN)
                    .KDUOM = grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_KDUOMTINDAKAN)
                    .JUMLAH = CDec(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_JUMLAHTINDAKAN))
                    .HARGA = CDec(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_HARGATINDAKAN))
                    .TOTAL = CDec(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_TOTALTINDAKAN))
                    .MEMO = IIf(String.IsNullOrEmpty(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)), "", grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN))
                    .KDUSER = sUserID
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oCPPT_TemplateProsedur.InsertData(ds, arrDetail)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oCPPT_TemplateProsedur.UpdateData(ds, arrDetail)
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
    Private Sub fn_LoadITEM_L2()
        Dim oItem_L2 As New Reference.clsItem_L2
        Try
            grdITEM_L2.Properties.DataSource = oItem_L2.GetData.Where(Function(x) x.ISACTIVE = True And Not x.MEMO.Contains("FARMASI")).ToList()
            grdITEM_L2.Properties.ValueMember = "KDITEM_L2"
            grdITEM_L2.Properties.DisplayMember = "MEMO"

            grdITEM_L2.Text = oItem_L2.DefaultItem_L2

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDITEMSearch()
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
            SQL &= ",NMITEM2 = (SELECT CASE D.MEMO WHEN 'NON KELAS' THEN A.NMITEM2 ELSE A.NMITEM2 + ' ' + D.MEMO END) "
            SQL &= ",KELOMPOK = B.MEMO "
            SQL &= ",PRICE = C.PRICESALESSTANDARD "
            SQL &= "FROM "
            SQL &= "M_ITEM A "
            SQL &= "INNER JOIN M_ITEM_L2 B "
            SQL &= "ON A.KDITEM_L2 = B.KDITEM_L2 "
            SQL &= "INNER JOIN M_ITEM_UOM C "
            SQL &= "ON A.KDITEM = C.KDITEM "
            SQL &= "INNER JOIN M_UOM D "
            SQL &= "ON C.KDUOM = D.KDUOM "
            SQL &= "WHERE A.ISSTOK = 0 "
            SQL &= "AND A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ITEM_ALL")

            grdKDITEMSearch.Properties.DataSource = ds.Tables("ITEM_ALL")
            grdKDITEMSearch.Properties.ValueMember = "KDITEM"
            grdKDITEMSearch.Properties.DisplayMember = "NMITEM2"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadSearchKDITEM()
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
            SQL &= "KELOMPOK = (SELECT MEMO FROM M_ITEM_L6 WHERE A.KDITEM_L6 = KDITEM_L6) "
            SQL &= ",A.KDITEM "
            SQL &= ",A.NMITEM2 "
            SQL &= ",KDPILIH = CONVERT(BIT, 0) "
            SQL &= "FROM "
            SQL &= "M_ITEM A "
            SQL &= "WHERE A.ISSTOK = 0 "
            SQL &= "AND A.ISACTIVE = 1 "
            SQL &= "AND A.KDITEM_L2 = '" & grdITEM_L2.EditValue & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ITEM")

            grdKDITEMALL.DataSource = ds.Tables("ITEM")

            grvKDITEMALL.UpdateCurrentRow()
            grvKDITEMALL.RefreshRow(grvKDITEMALL.GetFocusedDataSourceRowIndex())

            grvKDITEMALL.OptionsBehavior.Editable = False
            grvKDITEMALL.OptionsBehavior.ReadOnly = True

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grvKDITEMALL.Columns("KELOMPOK").Group()
            grvKDITEMALL.ExpandAllGroups()

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDUOM()
        Dim oUOM As New Reference.clsUOM
        Try
            Dim dsList = oUOM.GetData.Where(Function(x) x.ISACTIVE = True)

            grdCPPT_KDUOMTINDAKAN.DataSource = dsList.ToList()
            grdCPPT_KDUOMTINDAKAN.ValueMember = "KDUOM"
            grdCPPT_KDUOMTINDAKAN.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDITEM()
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
            SQL &= ",A.NMITEM2 "
            SQL &= "FROM "
            SQL &= "M_ITEM A "
            SQL &= "WHERE A.ISSTOK = 0 "
            SQL &= "AND A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ITEM")

            grdCPPT_KDITEMTINDAKAN.DataSource = ds.Tables("ITEM")
            grdCPPT_KDITEMTINDAKAN.ValueMember = "KDITEM"
            grdCPPT_KDITEMTINDAKAN.DisplayMember = "NMITEM2"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub DeleteToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem1.Click
        grvCPPT_Tindakan.DeleteSelectedRows()
    End Sub
    Private Sub grdKDITEMALL_DoubleClick(sender As Object, e As EventArgs) Handles grdKDITEMALL.DoubleClick
        grvCPPT_Tindakan.Focus()
        grvCPPT_Tindakan.AddNewRow()
        grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN, grvKDITEMALL.GetFocusedRowCellValue("KDITEM"))
        grvCPPT_Tindakan.UpdateCurrentRow()
        grvKDITEMALL.SetFocusedRowCellValue(colPilih, False)
    End Sub
    Private Sub grdITEM_L2_EditValueChanged(sender As Object, e As EventArgs) Handles grdITEM_L2.EditValueChanged
        fn_LoadSearchKDITEM()
    End Sub
    Private Sub grdKDITEMSearch_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDITEMSearch.EditValueChanged
        If isLoad = True Then
            If grdKDITEMSearch.Text <> "" Then
                grvCPPT_Tindakan.Focus()
                grvCPPT_Tindakan.AddNewRow()
                grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN, grdKDITEMSearch.EditValue)
                grvCPPT_Tindakan.UpdateCurrentRow()
            End If
        End If
    End Sub
#End Region
#Region "Grid Method"
    Private Sub grvCPPT_Tindakan_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvCPPT_Tindakan.CellValueChanged
        If e.Column.Name = colCPPT_KDITEMTINDAKAN.Name Then
            Dim oItem As New Reference.clsItem
            If grvCPPT_Tindakan.GetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN) IsNot Nothing Then
                Dim ds = oItem.GetDataDetail_UOM(grvCPPT_Tindakan.GetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN))

                If ds IsNot Nothing Then
                    grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_KDUOMTINDAKAN, ds.FirstOrDefault(Function(x) x.RATE = 1).KDUOM)
                    grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_JUMLAHTINDAKAN, 1)
                    grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_HARGATINDAKAN, ds.FirstOrDefault(Function(x) x.RATE = 1).PRICESALESSTANDARD)
                    grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_TOTALTINDAKAN, ds.FirstOrDefault(Function(x) x.RATE = 1).PRICESALESSTANDARD)
                End If
            End If
        End If
    End Sub
#End Region
End Class