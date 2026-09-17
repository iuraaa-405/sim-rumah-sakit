Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmTemplateRacikan
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oTemplateNonRacikan As New EMedrek.clsTemplateNonRacikan

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
            Me.Text = "Template Non Racikan"

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDTEMPLATE.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadITEM()
        fn_LoadUOM()
        'fn_LoadSIGNA()
        'fn_LoadCARAPAKAI()

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
    End Sub
    Private Sub fn_EmptyMe()
        txtKDTEMPLATE.Text = "<--- AUTO --->"
        txtREMARKS.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oTemplateNonRacikan.GetDataHeader(sNoId)

            With ds
                txtKDTEMPLATE.Text = sNoId
                txtREMARKS.Text = .REMARKS

                BindingSource.DataSource = oTemplateNonRacikan.GetDataDetailRacikan1(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdOBATRACIKAN.DataSource = BindingSource

            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtREMARKS.Text = String.Empty Then
                txtREMARKS.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtREMARKS.ErrorText = Statement.ErrorRequired

                txtREMARKS.Focus()
                fn_Validate = False
                Exit Function
            End If

            grvOBATRACIKAN.UpdateCurrentRow()

            If grvOBATRACIKAN.RowCount < 2 Then
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
            Dim sNoTemplateNonRacikan As String = String.Empty

            Dim ds = oTemplateNonRacikan.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oTemplateNonRacikan.GetDataHeader(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDCPPTTEMPLATE = sNoId
                .REMARKS = txtREMARKS.Text
            End With

            Dim oItem As New Reference.clsItem
            Dim oUom As New Reference.clsUOM
            Dim oSiga As New Reference.clsSigna
            Dim oCaraPakai As New Reference.clsCaraPakai

            Dim arrDetailObatRacikan = oTemplateNonRacikan.GetStructureDetailRacikanList
            For i As Integer = 0 To grvOBATRACIKAN.RowCount - 2
                Dim dsDetail = oTemplateNonRacikan.GetStructureDetailRacikan
                With dsDetail
                    .DATECREATED = ds.DATECREATED
                    .DATEUPDATED = ds.DATEUPDATED
                    .KDCPPTTEMPLATE = ds.KDCPPTTEMPLATE
                    .SEQ = i
                    .KDITEM = grvOBATRACIKAN.GetRowCellValue(i, colKDITEMOBATRACIKAN)
                    Dim dsItem = oItem.GetData(grvOBATRACIKAN.GetRowCellValue(i, colKDITEMOBATRACIKAN))
                    If dsItem IsNot Nothing Then
                        .NAMAOBAT = dsItem.NMITEM2
                    Else
                        .NAMAOBAT = "RACIKAN"
                    End If
                    .KDUOM = IIf(String.IsNullOrEmpty(grvOBATRACIKAN.GetRowCellValue(i, colKDUOMOBATRACIKAN)), "", grvOBATRACIKAN.GetRowCellValue(i, colKDUOMOBATRACIKAN))
                    Dim dsUom = oUom.GetData(grvOBATRACIKAN.GetRowCellValue(i, colKDUOMOBATRACIKAN))
                    If dsUom IsNot Nothing Then
                        .SATUAN = dsUom.MEMO
                    Else
                        .SATUAN = ""
                    End If
                    .SIGNA = IIf(String.IsNullOrEmpty(grvOBATRACIKAN.GetRowCellValue(i, colSIGNAOBATRACIKAN)), "", grvOBATRACIKAN.GetRowCellValue(i, colSIGNAOBATRACIKAN))
                    .PERMINTAAN = IIf(String.IsNullOrEmpty(grvOBATRACIKAN.GetRowCellValue(i, colPERMINTAANOBATRACIKAN)), "", grvOBATRACIKAN.GetRowCellValue(i, colPERMINTAANOBATRACIKAN))
                    .JUMLAH = IIf(String.IsNullOrEmpty(grvOBATRACIKAN.GetRowCellValue(i, colQTYOBATRACIKAN)), 0, grvOBATRACIKAN.GetRowCellValue(i, colQTYOBATRACIKAN))
                    .HARGA = CDec(0)
                    .TOTAL = CDec(IIf(String.IsNullOrEmpty(grvOBATRACIKAN.GetRowCellValue(i, colQTYOBATRACIKAN)), 0, grvOBATRACIKAN.GetRowCellValue(i, colQTYOBATRACIKAN)))
                    .REMARKS = IIf(String.IsNullOrEmpty(grvOBATRACIKAN.GetRowCellValue(i, colREMARKSRACIKAN)), "", grvOBATRACIKAN.GetRowCellValue(i, colREMARKSRACIKAN))
                    .KDUSER = sUserID
                    .ISBACA = 0
                End With
                arrDetailObatRacikan.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    txtKDTEMPLATE.Text = oTemplateNonRacikan.InsertDataRacikan(ds, arrDetailObatRacikan)
                    If txtKDTEMPLATE.Text = "" Then
                        fn_Save = False
                    Else
                        fn_Save = True
                    End If
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oTemplateNonRacikan.UpdateDataRacikan(ds, arrDetailObatRacikan)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
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
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        grvOBATRACIKAN.DeleteSelectedRows()
    End Sub
    Private Sub DeleteAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteAllToolStripMenuItem.Click
        grvOBATRACIKAN.OptionsSelection.MultiSelect = True
        grvOBATRACIKAN.SelectAll()
        grvOBATRACIKAN.DeleteSelectedRows()
        grvOBATRACIKAN.OptionsSelection.MultiSelect = False
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
    Private Sub fn_LoadUOM()
        Dim oUOM As New Reference.clsUOM
        Try
            Dim dsList = oUOM.GetData()

            grdKDUOMRACIKAN.DataSource = dsList.ToList()
            grdKDUOMRACIKAN.ValueMember = "KDUOM"
            grdKDUOMRACIKAN.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox("Load Uom" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    'Private Sub fn_LoadSIGNA()
    '    Dim oSigna As New Reference.clsSigna
    '    Try
    '        grdKDITEMOBATRACIKAN.DataSource = oSigna.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
    '        grdKDSIGNA.ValueMember = "KDSIGNA"
    '        grdKDSIGNA.DisplayMember = "MEMO"
    '    Catch oErr As Exception
    '        MsgBox("Load Signa" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
    'Private Sub fn_LoadCARAPAKAI()
    '    Dim oCaraPakai As New Reference.clsCaraPakai
    '    Try
    '        grdCARAPAKAI.DataSource = oCaraPakai.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
    '        grdCARAPAKAI.ValueMember = "KDCARAPAKAI"
    '        grdCARAPAKAI.DisplayMember = "MEMO"
    '    Catch oErr As Exception
    '        MsgBox("Load Cara Pakai" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
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

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDITEM = 'R999999' "
            SQL &= ",NMITEM1 = A.DESCRIPTION "
            SQL &= ",NMITEM2 = A.DESCRIPTION "
            SQL &= ",HARGA = 0 "
            SQL &= ",SATUAN = '-' "
            SQL &= ",STOK = 0 "
            SQL &= "FROM M_ITEM_RACIK A "
            SQL &= ") X "
            SQL &= "ORDER BY "
            SQL &= "X.HARGA "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ITEM")

            grdKDITEMOBATRACIKAN.DataSource = ds.Tables("ITEM")
            grdKDITEMOBATRACIKAN.ValueMember = "KDITEM"
            grdKDITEMOBATRACIKAN.DisplayMember = "NMITEM2"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Load Obat Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grvOBATRACIKAN_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvOBATRACIKAN.CellValueChanged
        If e.Column.Name = colKDITEMOBATRACIKAN.Name Then
            Try
                If grvOBATRACIKAN.GetFocusedRowCellValue(colKDITEMOBATRACIKAN) IsNot Nothing Then
                    Dim oItem As New Reference.clsItem
                    Dim ds = oItem.GetDataDetail_UOM(grvOBATRACIKAN.GetFocusedRowCellValue(colKDITEMOBATRACIKAN))

                    If ds.Count > 0 Then
                        grvOBATRACIKAN.SetFocusedRowCellValue(colKDUOMOBATRACIKAN, ds.FirstOrDefault(Function(x) x.RATE = 1).KDUOM)
                        'grvOBATRACIKAN.SetFocusedRowCellValue(colREMARKS_DOKTER, "")
                        grvOBATRACIKAN.SetFocusedRowCellValue(colQTYOBATRACIKAN, 0)
                        grvOBATRACIKAN.SetFocusedRowCellValue(colREMARKSRACIKAN, "-")
                    End If
                End If
            Catch oErr As Exception
                MsgBox("Load Detail : " & vbCrLf & oErr.Message, MsgBoxStyle.Critical, Me.Text)
            End Try
        End If
    End Sub
#End Region
End Class