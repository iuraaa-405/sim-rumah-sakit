Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient
Imports Newtonsoft.Json.Linq

Public Class frmSalesKatalog
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    'Private sNoId As String
    Private isLoad As Boolean = False
    Private oSalesKatalog As New Sales.clsSalesKatalog
    Private UrlApotekOnline As String = String.Empty
    Private ConsumerSecret As String = String.Empty
    Private ConsumerID As String = String.Empty
    Private UserKey As String = String.Empty
    Private KodeApotek As String = String.Empty
    Private oUser As New Setting.clsUser
    Private oBrigging As New Brigging.clsSetKoneksi
    Private ListPesanSave As New List(Of String)

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal nomortranskasi As String)
        oFormMode = FormMode
        'sNoId = NoId
        txtKDSOTRANSAKSI.Text = nomortranskasi
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "Apotek Online"

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtNOAPOTIK.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadDepartment()
        fn_LoadDoctor()

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
        deTGLSJP.Properties.ReadOnly = Status
        txtREFASALSJP.Properties.ReadOnly = Status
        grdKDDEPARTMENT.Properties.ReadOnly = Status
        cboKDJNSOBAT.Properties.ReadOnly = Status
        txtNORESEP.Properties.ReadOnly = Status
        deTGLRSP.Properties.ReadOnly = Status
        deTGLPELRSP.Properties.ReadOnly = Status
        grdKDDOCTOR.Properties.ReadOnly = Status
        cboiterasi.Properties.ReadOnly = Status
        grvDetail.OptionsBehavior.ReadOnly = Status

        Try
            Dim dsKoneksiApotekOnline = oUser.GetDataKoneksiBPJS("APOTEKONLINE")
            If dsKoneksiApotekOnline Is Nothing Then
                btnSaveNew.Enabled = False
                btnSaveClose.Enabled = False

                MsgBox("Url Apotek Online masih kosong, tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
            Else
                btnSaveNew.Enabled = Not Status
                btnSaveClose.Enabled = Not Status

                UrlApotekOnline = dsKoneksiApotekOnline.ALAMATWEB
                ConsumerSecret = dsKoneksiApotekOnline.SECREATKEY
                ConsumerID = dsKoneksiApotekOnline.CONSID
                UserKey = dsKoneksiApotekOnline.REMARKS
                KodeApotek = dsKoneksiApotekOnline.PPKPELAYANAN
            End If
        Catch ex As Exception
            MsgBox("Load Security : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_EmptyMe()
        txtNOAPOTIK.Text = ""
        deTGLSJP.DateTime = Now
        txtREFASALSJP.ResetText()
        grdKDDEPARTMENT.ResetText()
        cboKDJNSOBAT.SelectedIndex = 1
        txtNORESEP.ResetText()
        deTGLRSP.DateTime = Now
        deTGLPELRSP.DateTime = Now
        grdKDDOCTOR.ResetText()
        cboiterasi.SelectedIndex = 0

        Dim oSalesOrder As New Sales.clsSalesOrderTransaksi
        Dim oOrder As New Digital.clsR_Order

        Dim ds = oSalesOrder.GetData(txtKDSOTRANSAKSI.Text)
        If ds IsNot Nothing Then
            grdKDDEPARTMENT.Text = ds.S_PENDAFTARAN_KUNJUNGAN.KDDEPARTMENT
            grdKDDOCTOR.Text = ds.KDDOCTOR
            deTGLSJP.DateTime = ds.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.DATE
            txtREFASALSJP.Text = ds.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.NOMORSEP

            If ds.KDORDER <> "" Then
                Dim dsOrder = oOrder.GetDataAntrianFarmasi(ds.KDORDER)
                If dsOrder IsNot Nothing Then
                    txtNORESEP.Text = dsOrder.NOMORANTRIAN
                End If
            End If
            deTGLRSP.DateTime = ds.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.DATE
            deTGLPELRSP.DateTime = ds.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.DATE

            Dim oItemDPHO As New Reference.clsItem_KDDPHO

            For Each xloop In oSalesOrder.GetDataDetail(ds.KDSOTRANSAKSI)
                If xloop.M_ITEM.KDOBATDPHO <> "" Then
                    Dim dsItem = oItemDPHO.GetData(xloop.KDITEM)
                    If dsItem IsNot Nothing Then
                        If dsItem.aktif = "aktif" Then
                            grvDetail.Focus()
                            grvDetail.AddNewRow()
                            grvDetail.SetFocusedRowCellValue(colKDITEM, dsItem.KDITEM)
                            grvDetail.SetFocusedRowCellValue(colKDOBT, dsItem.kodeobat)
                            grvDetail.SetFocusedRowCellValue(colNMOBAT, dsItem.M_ITEM.NMITEM2)
                            grvDetail.SetFocusedRowCellValue(colSIGNA1OBT, IIf(xloop.M_SIGNA.SIGNA_1 = 0, 1, xloop.M_SIGNA.SIGNA_1))
                            grvDetail.SetFocusedRowCellValue(colSIGNA2OBT, IIf(xloop.M_SIGNA.SIGNA_2 = 0, 1, xloop.M_SIGNA.SIGNA_2))
                            grvDetail.SetFocusedRowCellValue(colJMLOBT, xloop.QTY)
                            grvDetail.SetFocusedRowCellValue(colJHO, 30)
                            grvDetail.SetFocusedRowCellValue(colCatKhsObt, "")
                            grvDetail.SetFocusedRowCellValue(colISAPPROVED, False)
                            grvDetail.SetFocusedRowCellValue(colREQUEST, "")
                            grvDetail.SetFocusedRowCellValue(colRESPONSE, "")
                            grvDetail.UpdateCurrentRow()
                        End If
                    End If
                End If
            Next
        End If
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oSalesKatalog.GetData(txtKDSOTRANSAKSI.Text)

            With ds
                txtKDSOTRANSAKSI.Text = .KDSOTRANSAKSI
                txtNOAPOTIK.Text = .NOAPOTIK
                deTGLSJP.DateTime = .TGLSJP
                txtREFASALSJP.Text = .REFASALSJP
                grdKDDEPARTMENT.Text = .POLIRSP
                cboKDJNSOBAT.Text = .KDJNSOBAT
                txtNORESEP.Text = .NORESEP
                deTGLRSP.DateTime = .TGLRSP
                deTGLPELRSP.DateTime = .TGLPELRSP
                grdKDDOCTOR.Text = .KdDokter
                cboiterasi.Text = .iterasi

                bindingSource.DataSource = oSalesKatalog.GetDataDetail(txtNOAPOTIK.Text).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = bindingSource
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If txtKDSOTRANSAKSI.Text = String.Empty Then
                txtKDSOTRANSAKSI.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDSOTRANSAKSI.ErrorText = Statement.ErrorRequired

                txtKDSOTRANSAKSI.Focus()
                fn_Validate = False
                Exit Function
            End If
            'If txtNOAPOTIK.Text = String.Empty Then
            '    txtNOAPOTIK.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '    txtNOAPOTIK.ErrorText = Statement.ErrorRequired

            '    txtNOAPOTIK.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If
            If txtREFASALSJP.Text = String.Empty Then
                txtREFASALSJP.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtREFASALSJP.ErrorText = Statement.ErrorRequired

                txtREFASALSJP.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDEPARTMENT.Text = String.Empty Then
                grdKDDEPARTMENT.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDEPARTMENT.ErrorText = Statement.ErrorRequired

                grdKDDEPARTMENT.Focus()
                fn_Validate = False
                Exit Function
            End If
            If cboKDJNSOBAT.Text = String.Empty Then
                cboKDJNSOBAT.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                cboKDJNSOBAT.ErrorText = Statement.ErrorRequired

                cboKDJNSOBAT.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtNORESEP.Text = String.Empty Then
                txtNORESEP.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtNORESEP.ErrorText = Statement.ErrorRequired

                txtNORESEP.Focus()
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
            If cboiterasi.Text = String.Empty Then
                cboiterasi.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                cboiterasi.ErrorText = Statement.ErrorRequired

                cboiterasi.Focus()
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
            Dim jsonRequest As String = String.Empty
            Dim jsonResponse As String = String.Empty

            If txtNOAPOTIK.Text = "" Then
                Dim oDepratment As New Reference.clsDepartment
                Dim oDoctor As New Reference.clsDoctor

                jsonRequest = " { "
                jsonRequest &= """TGLSJP"": """ & deTGLSJP.DateTime.ToString("yyyy-MM-dd HH:mm:ss") & ""","
                jsonRequest &= """REFASALSJP"": """ & txtREFASALSJP.Text & ""","
                jsonRequest &= """POLIRSP"": """ & oDepratment.GetData(grdKDDEPARTMENT.EditValue).VCLAIM_KODEPOLI & ""","
                jsonRequest &= """KDJNSOBAT"": """ & cboKDJNSOBAT.SelectedIndex + 1 & ""","
                jsonRequest &= """NORESEP"": """ & txtNORESEP.Text & ""","
                jsonRequest &= """IDUSERSJP"": """ & sUserID & ""","
                jsonRequest &= """TGLRSP"": """ & deTGLRSP.DateTime.ToString("yyyy-MM-dd HH:mm:ss") & ""","
                jsonRequest &= """TGLPELRSP"": """ & deTGLPELRSP.DateTime.ToString("yyyy-MM-dd HH:mm:ss") & ""","
                jsonRequest &= """KdDokter"": """ & oDoctor.GetData(grdKDDOCTOR.EditValue).VCLAIM_KDDPJP & ""","
                jsonRequest &= """iterasi"": """ & IIf(cboiterasi.Text = "Non Iterasi", "0", "1") & """ "
                jsonRequest &= "}  "

                Try
                    Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    Dim allData = JObject.Parse(oBrigging.SimpanResep(uTime, UrlApotekOnline, ConsumerSecret, ConsumerID, UserKey, jsonRequest))

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        Dim DataDecrypt = JObject.Parse(oBrigging.Decrypt(allData("response"), ConsumerID & ConsumerSecret & uTime))
                        jsonResponse = allData("response")
                        txtNOAPOTIK.Text = DataDecrypt.Item("noApotik").ToString()
                    Else
                        If messageResponse.Contains("Data Sudah Di entry NoSEP") Then
                            txtNOAPOTIK.Text = Microsoft.VisualBasic.Right(messageResponse, 19)
                        Else
                            MsgBox(CodeResponse & " " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                        End If
                    End If
                Catch oErr As Exception
                    MsgBox(oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try

                If txtNOAPOTIK.Text = "" Then
                    fn_Save = False
                    Exit Function
                End If
            End If

            ' ***** HEADER *****
            Dim ds = oSalesKatalog.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oSalesKatalog.GetData(txtKDSOTRANSAKSI.Text).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDSOTRANSAKSI = txtKDSOTRANSAKSI.Text
                .NOAPOTIK = txtNOAPOTIK.Text
                .TGLSJP = deTGLSJP.DateTime.ToString("yyyy-MM-dd HH:mm:ss")
                .REFASALSJP = txtREFASALSJP.Text
                .POLIRSP = grdKDDEPARTMENT.EditValue
                .KDJNSOBAT = cboKDJNSOBAT.Text
                .NORESEP = txtNORESEP.Text
                .IDUSERSJP = sUserID
                .TGLRSP = deTGLRSP.DateTime
                .TGLPELRSP = deTGLPELRSP.DateTime
                .KdDokter = grdKDDOCTOR.EditValue
                .iterasi = cboiterasi.Text
                .ISCHEKED = False
                Try
                    .REQUEST = oSalesKatalog.GetData(txtKDSOTRANSAKSI.Text).REQUEST
                Catch ex As Exception
                    .REQUEST = jsonRequest
                End Try
                Try
                    .RESPONSE = oSalesKatalog.GetData(txtKDSOTRANSAKSI.Text).RESPONSE
                Catch ex As Exception
                    .RESPONSE = jsonResponse
                End Try
            End With

            ' ***** DETIL *****
            Dim arrDetail = oSalesKatalog.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oSalesKatalog.GetStructureDetail
                With dsDetail
                    .SEQ = i
                    .NOAPOTIK = ds.NOAPOTIK
                    .NOSJP = ""
                    .NORESEP = txtNORESEP.Text
                    .KDITEM = grvDetail.GetRowCellValue(i, colKDITEM)
                    .KDOBT = grvDetail.GetRowCellValue(i, colKDOBT)
                    .NMOBAT = grvDetail.GetRowCellValue(i, colNMOBAT)
                    .SIGNA1OBT = grvDetail.GetRowCellValue(i, colSIGNA1OBT)
                    .SIGNA2OBT = grvDetail.GetRowCellValue(i, colSIGNA2OBT)
                    .JMLOBT = grvDetail.GetRowCellValue(i, colJMLOBT)
                    .JHO = grvDetail.GetRowCellValue(i, colJHO)
                    .CatKhsObt = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colCatKhsObt)), "", grvDetail.GetRowCellValue(i, colCatKhsObt))

                    If grvDetail.GetRowCellValue(i, colISAPPROVED) = False Then
                        Dim jsonRequestObat As String = String.Empty
                        Dim jsonResponseObat As String = String.Empty

                        jsonRequestObat = "{ "
                        jsonRequestObat &= """NOSJP"": """ & .NOAPOTIK & ""","
                        jsonRequestObat &= """NORESEP"": """ & .NORESEP & ""","
                        jsonRequestObat &= """KDOBT"": """ & .KDOBT & ""","
                        jsonRequestObat &= """NMOBAT"": """ & .NMOBAT & ""","
                        jsonRequestObat &= """SIGNA1OBT"": """ & Replace(.SIGNA1OBT, ",", ".") & ""","
                        jsonRequestObat &= """SIGNA2OBT"": """ & Replace(.SIGNA2OBT, ",", ".") & ""","
                        jsonRequestObat &= """JMLOBT"": """ & Replace(.JMLOBT, ",", ".") & ""","
                        jsonRequestObat &= """JHO"": """ & Replace(.JHO, ",", ".") & ""","
                        jsonRequestObat &= """CatKhsObt"": """ & .CatKhsObt & """ "
                        jsonRequestObat &= "}  "

                        Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                        Dim CodeResponse As String = String.Empty
                        Dim messageResponse As String = String.Empty

                        jsonResponseObat = oBrigging.NonRacikan(uTime, UrlApotekOnline, ConsumerSecret, ConsumerID, UserKey, jsonRequestObat)

                        Dim allData = JObject.Parse(jsonResponseObat)

                        CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                        messageResponse = allData("metaData")("message").ToString

                        ListPesanSave.Add(.SEQ + 1 & ". " & .NMOBAT & " (" & CodeResponse & messageResponse & ")")

                        If jsonResponseObat.Contains("200") Then
                            .ISAPPROVED = True
                        Else
                            .ISAPPROVED = False
                        End If

                        .REQUEST = jsonRequestObat
                        .RESPONSE = jsonResponseObat
                    Else
                        .ISAPPROVED = grvDetail.GetRowCellValue(i, colISAPPROVED)
                        .REQUEST = grvDetail.GetRowCellValue(i, colREQUEST)
                        .RESPONSE = grvDetail.GetRowCellValue(i, colRESPONSE)
                    End If

                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oSalesKatalog.InsertData(ds, arrDetail)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oSalesKatalog.UpdateData(ds, arrDetail)
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
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail.DeleteSelectedRows()
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmSalesKatalog_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
            MsgBox(Statement.SaveSuccess & vbCrLf & String.Join(vbCrLf, ListPesanSave.ToArray), MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadDepartment()
        Dim oDepartment As New Reference.clsDepartment
        Try
            grdKDDEPARTMENT.Properties.DataSource = oDepartment.GetData.Where(Function(x) x.ISACTIVE = True And x.VCLAIM_KODEPOLI <> "").ToList()
            grdKDDEPARTMENT.Properties.ValueMember = "KDDEPARTMENT"
            grdKDDEPARTMENT.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDoctor()
        Dim oDoctor As New Reference.clsDoctor
        Try
            grdKDDOCTOR.Properties.DataSource = oDoctor.GetData.Where(Function(x) x.ISACTIVE = True And x.VCLAIM_KDDPJP <> "").ToList()
            grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class