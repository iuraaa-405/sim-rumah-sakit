Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports DevExpress.XtraSplashScreen
Imports System.Data.SqlClient

Public Class frmCashinBPJS
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oCashinBPJS As New Finance.clsCashinBPJS
    Private oJasa As New Finance.clsJasa
    Private sConnGuntur As String = ""
    Private sTransaksiDetail As Boolean = False
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId

        Dim dsDatabase = oJasa.GetDataSetting

        sConnGuntur = dsDatabase.KONEKSI

    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = CashinBPJS.TITLE

            lKDCASHNPJS.Text = CashinBPJS.KDCASHINBPJS
            lDATE.Text = CashinBPJS.TANGGAL
            lJUDULJASA.Text = CashinBPJS.KDJUDULJASA & " *"
            lCATEGORY.Text = CashinBPJS.CATEGORY

            tab1.Text = CashinBPJS.TAB_DETAIL
            tab3.Text = CashinBPJS.TAB_MEMO

            lTOTAL_TARIF.Text = CashinBPJS.TOTAL_TARIF
            lTARIF_RS.Text = CashinBPJS.TARIF_RS

            grvDetail.Columns("REMARKS").Caption = CashinBPJS.DETAIL_REMARKS

            grvKDJUDULJASA.Columns("MEMO").Caption = "Name Display"

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDCashinBPJS.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDPAYMENTTYPE()

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
        btnLoadTXT.Enabled = Not Status

        deDATE.Properties.ReadOnly = Status
        grdKDJUDULJASA.Properties.ReadOnly = Status
        rbCATEGORY.Properties.ReadOnly = Status

        txtMEMO.Properties.ReadOnly = Status

        txtTOTAL_TARIF.Properties.ReadOnly = Status
        txtTARIF_RS.Properties.ReadOnly = Status

        grvDetail.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDCASHINBPJS.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        grdKDJUDULJASA.ResetText()
        txtMEMO.ResetText()
        txtTOTAL_TARIF.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oCashinBPJS.GetData(sNoId)

            With ds
                txtKDCASHINBPJS.Text = .KDCASHINBPJS
                deDATE.DateTime = .DATE
                grdKDJUDULJASA.Text = .KDJUDULJASA
                txtMEMO.Text = .MEMO
                txtTOTAL_TARIF.Text = .GRANDTOTAL
                txtTARIF_RS.Text = .ROUND
                rbCATEGORY.SelectedIndex = .CATEGORY

                bindingSource.DataSource = oCashinBPJS.GetDataDetail.Where(Function(x) x.KDCASHINBPJS = sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = bindingSource

            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If grdKDJUDULJASA.Text = String.Empty Then
                grdKDJUDULJASA.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDJUDULJASA.ErrorText = Statement.ErrorRequired

                grdKDJUDULJASA.Focus()
                fn_Validate = False
                Exit Function
            End If

            grvDetail.UpdateCurrentRow()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim BelumSave As Boolean = False

            Dim ds = oCashinBPJS.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oCashinBPJS.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    BelumSave = True
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDCASHINBPJS = sNoId
                .DATE = deDATE.DateTime
                .CATEGORY = rbCATEGORY.SelectedIndex
                .KDJUDULJASA = IIf(String.IsNullOrEmpty(grdKDJUDULJASA.EditValue), String.Empty, grdKDJUDULJASA.EditValue)
                .MEMO = txtMEMO.Text
                .SUBTOTAL = CDec(0)
                .ADMIN = CDec(0)
                .ROUND = CDec(txtTARIF_RS.Text)
                .GRANDTOTAL = CDec(txtTOTAL_TARIF.Text)
                .KDUSER = sUserID
            End With

            ' ***** DETIL *****
            Dim arrDetail = oCashinBPJS.GetStructureDetailList

            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oCashinBPJS.GetStructureDetail
                With dsDetail

                    'Try
                    '    .DATECREATED = oCashinBPJS.GetData(sNoId).DATECREATED
                    'Catch oErr As Exception
                    '    .DATECREATED = Now
                    'End Try

                    If BelumSave = False Then
                        .DATECREATED = oCashinBPJS.GetData(sNoId).DATECREATED
                    Else
                        .DATECREATED = Now
                    End If
                    .DATEUPDATED = Now
                    .KDCASHINBPJS = ds.KDCASHINBPJS
                    .SEQ = i
                    .ADMISSION_DATE = grvDetail.GetRowCellValue(i, colADMISSION_DATE)
                    .DISCHARGE_DATE = grvDetail.GetRowCellValue(i, colDISCHARGE_DATE)
                    .NAME_DPJP = grvDetail.GetRowCellValue(i, colNAME_DPJP)
                    .NOSEP = grvDetail.GetRowCellValue(i, colSEP).ToString.Trim.ToUpper
                    .TOTAL_TARIF = CDec(grvDetail.GetRowCellValue(i, colTOTAL_TARIF))
                    .TARIF_RS = CDec(grvDetail.GetRowCellValue(i, colTARIF_RS))
                    .DIAGLIST = grvDetail.GetRowCellValue(i, colDIAGLIST)
                    .PROCLIST = grvDetail.GetRowCellValue(i, colPROCLIST)
                    .ISCHEKED_KUNJUNGAN = grvDetail.GetRowCellValue(i, colISCHEKED_KUNJUNGAN)
                    .ISCHEKED_TRANSAKSI = grvDetail.GetRowCellValue(i, colISCHEKED_TRANSAKSI)
                    .MRN = grvDetail.GetRowCellValue(i, colMRN)
                    .REMARKS = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colREMARKS)), "-", grvDetail.GetRowCellValue(i, colREMARKS))
                    .OBAT = CDec(grvDetail.GetRowCellValue(i, colOBAT))
                    .LABORATORIUM = CDec(grvDetail.GetRowCellValue(i, colLABORATORIUM))
                    .RADIOLOGI = CDec(grvDetail.GetRowCellValue(i, colRADIOLOGI))
                    .DELEGASI = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colDELEGASI)), "", grvDetail.GetRowCellValue(i, colDELEGASI))
                    .UNIT = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colUNIT)), "", grvDetail.GetRowCellValue(i, colUNIT))
                    .NAMAPASIEN = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colNAMAPASIEN)), "", grvDetail.GetRowCellValue(i, colNAMAPASIEN))
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oCashinBPJS.InsertData(ds, arrDetail)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oCashinBPJS.UpdateData(ds, arrDetail)
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
    Private Sub frmCashinBPJS_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
            Case Keys.F5
                If btnLoadTXT.Enabled = True Then
                    btnLoadTXT_Click()
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
    Private Sub btnLoadTXT_Click() Handles btnLoadTXT.ItemClick
        fn_BrowseTxt()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadKDPAYMENTTYPE()
        Dim oPAYMENTTYPE As New Reference.clsJudulJasa
        Try
            grdKDJUDULJASA.Properties.DataSource = oPAYMENTTYPE.GetData.Where(Function(x) x.ISACTIVE = True And x.CATEGORY = rbCATEGORY.SelectedIndex).ToList()
            grdKDJUDULJASA.Properties.ValueMember = "KDJUDULJASA"
            grdKDJUDULJASA.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDPAYMENTTYPE_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdKDJUDULJASA.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdKDJUDULJASA.ResetText()
        End If
    End Sub
    Private Sub fn_BrowseTxt()
        If fn_Validate() = True Then
            Dim fBrowse As New OpenFileDialog
            With fBrowse
                .Filter = "Txt files(*.txt)|*.txt|All files (*.*)|*.*"
                .FilterIndex = 1
                .Title = "Import data from Txt file"
            End With

            If fBrowse.ShowDialog() = Windows.Forms.DialogResult.OK Then
                grvDetail.OptionsSelection.MultiSelect = True
                grvDetail.SelectAll()
                grvDetail.DeleteSelectedRows()
                grvDetail.OptionsSelection.MultiSelect = False
                txtTOTAL_TARIF.ResetText()
                txtTARIF_RS.ResetText()

                Dim arrDetail = oCashinBPJS.GetStructureDetailList

                Dim sProcess As Integer = 0
                Dim sTotal As Integer = 0

                Dim oUmpanBalik As New Finance.clsCashinBPJS
                Dim reader As New System.IO.StreamReader(fBrowse.FileName)
                Dim allLines As List(Of String) = New List(Of String)
                Dim RecordLine As List(Of String) = New List(Of String)

                Do While Not reader.EndOfStream
                    allLines.Add(reader.ReadLine())
                    sTotal += 1
                Loop

                If MsgBox("Apa anda yakin akan mengimport " & sTotal - 1 & " Baris data ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

                Try
                    SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

                    For Each xLoop In allLines

                        RecordLine.Add(xLoop)

                        Dim Record As List(Of String) = New List(Of String)

                        Dim ADMISSION_DATE As DateTime = Now
                        Dim DISCHARGE_DATE As DateTime = Now
                        Dim NOMORSEP As String = String.Empty
                        Dim DPJP As String = String.Empty
                        Dim TOTAL_TARIF As Decimal = 0
                        Dim TARIF_RS As Decimal = 0
                        Dim DIAGLIST As String = String.Empty
                        Dim PROCLIST As String = String.Empty
                        Dim MRN As String = String.Empty
                        Dim OBAT As Decimal = 0
                        Dim LABORATORIUM As Decimal = 0
                        Dim RADIOLOGI As Decimal = 0
                        Dim NAMAPASIEN As String = String.Empty

                        If RecordLine.Count > 1 Then

                            For Each field As String In xLoop.Split(New String() {ControlChars.Tab}, StringSplitOptions.None)
                                Record.Add(field)

                                If Record.Count = 6 Then
                                    Dim tess = field

                                    Dim tes1 = field.Substring(0, 2)
                                    Dim tes2 = field.Substring(3, 2)
                                    Dim tes3 = field.Substring(6, 4)

                                    ADMISSION_DATE = field.Substring(3, 2) & "/" & field.Substring(0, 2) & "/" & field.Substring(6, 4)

                                End If

                                If Record.Count = 7 Then
                                    DISCHARGE_DATE = field.Substring(3, 2) & "/" & field.Substring(0, 2) & "/" & field.Substring(6, 4)
                                End If

                                If Record.Count = 12 Then
                                    DIAGLIST = field
                                End If

                                If Record.Count = 17 Then
                                    NAMAPASIEN = field
                                End If

                                If Record.Count = 13 Then
                                    PROCLIST = field
                                End If

                                If Record.Count = 39 Then
                                    TOTAL_TARIF = field
                                End If

                                If Record.Count = 40 Then
                                    TARIF_RS = field
                                End If

                                If Record.Count = 47 Then
                                    MRN = field
                                End If

                                If Record.Count = 50 Then
                                    DPJP = field
                                End If

                                If Record.Count = 51 Then
                                    NOMORSEP = field
                                End If

                                If Record.Count = 67 Then
                                    RADIOLOGI = field
                                End If

                                If Record.Count = 68 Then
                                    LABORATORIUM = field
                                End If

                                If Record.Count = 73 Then
                                    OBAT = field
                                End If

                            Next

                        End If

                        If NOMORSEP <> String.Empty Then
                            Dim dsDetail = oCashinBPJS.GetStructureDetail
                            With dsDetail

                                .DATECREATED = Now
                                .DATEUPDATED = Now
                                .KDCASHINBPJS = ""
                                .SEQ = 0
                                .ADMISSION_DATE = ADMISSION_DATE
                                .DISCHARGE_DATE = DISCHARGE_DATE
                                .NAME_DPJP = DPJP.ToString.Trim
                                .NOSEP = NOMORSEP
                                .TOTAL_TARIF = TOTAL_TARIF
                                .TARIF_RS = TARIF_RS
                                .DIAGLIST = DIAGLIST.ToString.Trim
                                .PROCLIST = PROCLIST.ToString.Trim
                                .REMARKS = ""
                                .MRN = MRN

                                Dim DATA = MRN.ToString.PadLeft(6, "0")

                                If fn_SearchPasien("0-" & DATA.Substring(0, 2) & "-" & DATA.Substring(2, 2) & "-" & DATA.Substring(4, 2), ADMISSION_DATE, NOMORSEP, TARIF_RS, DPJP, OBAT, LABORATORIUM, RADIOLOGI) = True Then
                                    .ISCHEKED_KUNJUNGAN = True
                                    .ISCHEKED_TRANSAKSI = sTransaksiDetail
                                    .REMARKS = ""
                                Else
                                    .ISCHEKED_KUNJUNGAN = False
                                    .ISCHEKED_TRANSAKSI = sTransaksiDetail
                                    .REMARKS = ""
                                End If

                                .OBAT = OBAT
                                .LABORATORIUM = LABORATORIUM
                                .RADIOLOGI = RADIOLOGI
                                .NAMAPASIEN = NAMAPASIEN

                                arrDetail.Add(dsDetail)

                            End With
                        End If

                        SplashScreenManager.Default.SetWaitFormCaption("Processing data " & sProcess & " of " & sTotal - 1 & "")

                        sProcess += 1

                        txtTOTAL_TARIF.Text += TOTAL_TARIF
                        txtTARIF_RS.Text += TARIF_RS
                    Next

                    Dim tes = arrDetail
                    bindingSource.DataSource = arrDetail
                    grdDetail.DataSource = bindingSource

                Catch ex As Exception
                    MsgBox("Load Data" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                Finally
                    SplashScreenManager.CloseForm(False)
                    MsgBox("Export Selesai", MsgBoxStyle.Information, Me.Text)
                End Try

            End If
        End If
    End Sub
    Private Function fn_SearchPasien(ByVal KD_PASIEN As String, ByVal TGL_MASUK As DateTime, ByVal NOSEP As String, ByVal TARIF_RS As Decimal, ByVal DPJP As String, ByVal OBAT As Decimal, LABORATORIUM As Decimal, RADIOLOGI As Decimal) As Boolean
        Try
            fn_SearchPasien = True

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

            SQL = "SELECT "
            SQL &= "NAMADOKTER = (SELECT NAMA FROM DOKTER WHERE A.KD_DOKTER = KD_DOKTER ) "
            SQL &= ",* "
            SQL &= "FROM "
            SQL &= "KUNJUNGAN A "
            SQL &= "INNER JOIN PASIEN B "
            SQL &= "ON A.KD_PASIEN = B.KD_PASIEN "
            SQL &= "WHERE "
            SQL &= "A.KD_PASIEN = '" & KD_PASIEN & "' "
            SQL &= "AND A.KD_DOKTER <> 'XXX' "
            SQL &= "AND CONVERT(VARCHAR(8), A.TGL_MASUK, 112) = '" & TGL_MASUK.ToString("yyyyMMdd") & "' "
            If rbCATEGORY.SelectedIndex = 1 Then
                SQL &= "AND A.URUT_MASUK <> 0 "
            End If

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "TRANSAKSIRS")

            If ds.Tables("TRANSAKSIRS").Rows.Count <> 1 Then
                fn_SearchPasien = False
            End If

            For iLoop As Integer = 0 To ds.Tables("TRANSAKSIRS").Rows.Count - 1
                With ds.Tables("TRANSAKSIRS")
                    InsertCustomer(KD_PASIEN, .Rows(iLoop)("NAMA"), .Rows(iLoop)("ALAMAT"))
                    InsertDoctor(.Rows(iLoop)("KD_DOKTER"), .Rows(iLoop)("NAMA"))

                    If fn_SearchTransaksi(KD_PASIEN, .Rows(iLoop)("KD_DOKTER"), .Rows(iLoop)("KD_UNIT"), TGL_MASUK, NOSEP, TARIF_RS, DPJP, OBAT, LABORATORIUM, RADIOLOGI) = True Then
                        sTransaksiDetail = True
                    Else
                        sTransaksiDetail = False
                    End If

                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            fn_SearchPasien = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_SearchTransaksi(ByVal KD_PASIEN As String, ByVal KD_DOKTER As String, ByVal KD_UNIT As String, ByVal DATE_MASUK As DateTime, ByVal NOSEP As String, ByVal TARIF_RS As Decimal, ByVal DPJP As String, ByVal OBAT As Decimal, LABORATORIUM As Decimal, RADIOLOGI As Decimal) As Boolean
        Try
            fn_SearchTransaksi = True

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

            Dim NO_TRANSAKSI As String = ""
            Dim KDJASA As String = ""

            Dim dsJasaSEP = oJasa.GetDataSEP(NOSEP)
            If dsJasaSEP IsNot Nothing Then
                KDJASA = dsJasaSEP.KDJASA
            End If

            SQL = "SELECT "
            SQL &= "NO_TRANSAKSI "
            SQL &= "FROM "
            SQL &= "TRANSAKSI A "
            SQL &= "WHERE "
            SQL &= "A.KD_PASIEN = '" & KD_PASIEN & "' "
            SQL &= "AND CONVERT(VARCHAR(8), A.TGL_TRANSAKSI, 112) = '" & DATE_MASUK.ToString("yyyyMMdd") & "'  "
            SQL &= "AND A.KD_UNIT = '" & KD_UNIT & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "TRANSAKKSI_H")

            If ds.Tables("TRANSAKKSI_H").Rows.Count <> 1 Then
                fn_SearchTransaksi = False
                Exit Function
            End If

            If rbCATEGORY.SelectedIndex = 0 Then
                For iLoop As Integer = 0 To ds.Tables("TRANSAKKSI_H").Rows.Count - 1

                    With ds.Tables("TRANSAKKSI_H")
                        NO_TRANSAKSI = .Rows(iLoop)("NO_TRANSAKSI")

                        SQL = " SELECT NAMA_UNIT = (SELECT NAMA_UNIT FROM UNIT WHERE KD_UNIT = B.KD_UNIT)   "
                        SQL &= ",KDDOKTERVISITE = ISNULL((SELECT KD_DOKTER FROM VISITE_DOKTER  "
                        SQL &= "WHERE NO_TRANSAKSI = B.NO_TRANSAKSI AND KD_KASIR = B.KD_KASIR AND URUT = B.URUT), '')  "
                        SQL &= ",NAMA_DOKTERVISITE = ISNULL((SELECT BB.NAMA FROM VISITE_DOKTER AA INNER JOIN DOKTER BB ON AA.KD_DOKTER = BB.KD_DOKTER  "
                        SQL &= "WHERE AA.NO_TRANSAKSI = B.NO_TRANSAKSI And AA.KD_KASIR = B.KD_KASIR AND AA.URUT = B.URUT), '')  "
                        SQL &= ",GRANDTOTAL = B.HARGA ,NAMA_PRODUK = ISNULL((SELECT DESKRIPSI FROM PRODUK WHERE B.KD_PRODUK = KD_PRODUK), '')  "
                        SQL &= ",B.* FROM DETAIL_TRANSAKSI B  WHERE B.NO_TRANSAKSI = '" & .Rows(iLoop)("NO_TRANSAKSI") & "' "

                        oComm.Connection = oConn
                        oComm.CommandText = SQL
                        oComm.CommandTimeout = 120
                        oComm.CommandType = CommandType.Text

                        da = New SqlDataAdapter(oComm)
                        da.Fill(ds, "TRANSAKSIDETAIL")
                    End With

                Next

                If ds.Tables("TRANSAKSIDETAIL").Rows.Count < 1 Then
                    fn_SearchTransaksi = False
                    Exit Function
                End If

                Dim GRANDTOTAL As Decimal = 0
                Dim arrDetail = oJasa.GetStructureDetailList
                Dim SEQ As Integer = 0

                For iLoop As Integer = 0 To ds.Tables("TRANSAKSIDETAIL").Rows.Count - 1
                    With ds.Tables("TRANSAKSIDETAIL")
                        GRANDTOTAL += .Rows(iLoop)("GRANDTOTAL")
                    End With
                Next

                If TARIF_RS <> GRANDTOTAL Then
                    For iLoop As Integer = 0 To ds.Tables("TRANSAKKSI_H").Rows.Count - 1

                        With ds.Tables("TRANSAKKSI_H")
                            NO_TRANSAKSI = .Rows(iLoop)("NO_TRANSAKSI")

                            SQL = " SELECT NAMA_UNIT = (SELECT NAMA_UNIT FROM UNIT WHERE KD_UNIT = B.KD_UNIT)   "
                            SQL &= " ,KDDOKTERVISITE = ISNULL((SELECT KD_DOKTER FROM VISITE_DOKTER  "
                            SQL &= " WHERE NO_TRANSAKSI = B.NO_TRANSAKSI AND KD_KASIR = B.KD_KASIR AND URUT = B.URUT), '')  "
                            SQL &= " ,NAMA_DOKTERVISITE = ISNULL((SELECT BB.NAMA FROM VISITE_DOKTER AA INNER JOIN DOKTER BB ON AA.KD_DOKTER = BB.KD_DOKTER  "
                            SQL &= " WHERE AA.NO_TRANSAKSI = B.NO_TRANSAKSI And AA.KD_KASIR = B.KD_KASIR AND AA.URUT = B.URUT), '')  "
                            SQL &= " ,GRANDTOTAL = B.HARGA ,NAMA_PRODUK = ISNULL((SELECT DESKRIPSI FROM PRODUK WHERE B.KD_PRODUK = KD_PRODUK), '')  "
                            SQL &= " ,B.* FROM DETAIL_TRANSAKSI B  WHERE B.NO_TRANSAKSI = '" & .Rows(iLoop)("NO_TRANSAKSI") & "' AND B.KD_UNIT <> '61' "

                            oComm.Connection = oConn
                            oComm.CommandText = SQL
                            oComm.CommandTimeout = 120
                            oComm.CommandType = CommandType.Text

                            da = New SqlDataAdapter(oComm)
                            da.Fill(ds, "TRANSAKSIDETAIL_01")
                        End With

                    Next

                    GRANDTOTAL = 0

                    For iLoop As Integer = 0 To ds.Tables("TRANSAKSIDETAIL_01").Rows.Count - 1
                        Dim dsRekap As New DataAccess.I_JASA_D

                        With ds.Tables("TRANSAKSIDETAIL_01")

                            GRANDTOTAL += .Rows(iLoop)("GRANDTOTAL")

                            dsRekap.KDJASA = KDJASA
                            dsRekap.SEQ = SEQ

                            SEQ += 1

                            If .Rows(iLoop)("KD_PRODUK") <> 0 Then
                                InsertItem(.Rows(iLoop)("KD_PRODUK"), .Rows(iLoop)("NAMA_PRODUK"), .Rows(iLoop)("GRANDTOTAL"))
                            End If

                            dsRekap.KDITEM = .Rows(iLoop)("KD_PRODUK")

                            If .Rows(iLoop)("KD_UNIT") <> "" Then
                                InsertDepartment(.Rows(iLoop)("KD_UNIT"), .Rows(iLoop)("NAMA_UNIT"))
                            End If

                            dsRekap.KDDEPARTMENT = .Rows(iLoop)("KD_UNIT")

                            If rbCATEGORY.SelectedIndex = 0 Then
                                dsRekap.KDDOCTOR = KD_DOKTER

                                Dim oDoctor As New Reference.clsDoctor
                                Dim dsDoctor = oDoctor.GetDataDetail_DOCTORBPJSByNAMA(DPJP)

                                If dsDoctor IsNot Nothing Then
                                    'dsRekap.KDDOCTOR = dsDoctor.KDDOCTOR
                                Else
                                    MsgBox("DPJP " & DPJP & " Tidak ditemukan di Database", MsgBoxStyle.Exclamation, Me.Text)
                                    fn_SearchTransaksi = False
                                    Exit Function
                                End If

                            Else
                                If .Rows(iLoop)("KDDOKTERVISITE") <> "" Then
                                    InsertDoctor(.Rows(iLoop)("KDDOKTERVISITE"), .Rows(iLoop)("NAMA_DOKTERVISITE"))

                                    dsRekap.KDDOCTOR = .Rows(iLoop)("KDDOKTERVISITE")

                                Else

                                    dsRekap.KDDOCTOR = KD_DOKTER

                                    Dim oDoctor As New Reference.clsDoctor
                                    Dim dsDoctor = oDoctor.GetDataDetail_DOCTORBPJSByNAMA(DPJP)

                                    If dsDoctor IsNot Nothing Then
                                        'dsRekap.KDDOCTOR = dsDoctor.KDDOCTOR
                                    Else
                                        MsgBox("DPJP " & DPJP & " Tidak ditemukan di Database", MsgBoxStyle.Exclamation, Me.Text)
                                        fn_SearchTransaksi = False
                                        Exit Function
                                    End If

                                End If
                            End If


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

                        End With

                        arrDetail.Add(dsRekap)

                    Next

                    ' ***** HEADER *****
                    Dim dsJasa = oJasa.GetStructureHeader
                    With dsJasa
                        .DATECREATED = Now
                        .DATEUPDATED = Now
                        .KDJASA = KDJASA
                        .DATE = DATE_MASUK
                        .CATEGORY = rbCATEGORY.SelectedIndex
                        .NOSEP = NOSEP
                        .KDCUSTOMER = KD_PASIEN
                        .NO_TRANSAKSI = NO_TRANSAKSI
                        .KD_KASIR = IIf(rbCATEGORY.SelectedIndex = 0, "", "02")
                        .DESCRIPTION = txtMEMO.Text.Trim.ToUpper
                        .KDUSER = sUserID
                        .GRANDTOTAL = GRANDTOTAL
                        .CATEGORYBAYAR = 0
                        .CATEGORYRUMUS = 0
                    End With

                    If TARIF_RS <> GRANDTOTAL Then
                        fn_SearchTransaksi = False

                        arrDetail.Clear()

                        GRANDTOTAL = 0

                        Dim dsRekapAdministrasi As New DataAccess.I_JASA_D

                        GRANDTOTAL += 5000

                        dsRekapAdministrasi.KDJASA = KDJASA
                        dsRekapAdministrasi.SEQ = 0


                        dsRekapAdministrasi.KDITEM = 18

                        dsRekapAdministrasi.KDDEPARTMENT = KD_UNIT

                        dsRekapAdministrasi.KDDOCTOR = KD_DOKTER

                        Dim oDoctor As New Reference.clsDoctor
                        Dim dsDoctor = oDoctor.GetDataDetail_DOCTORBPJSByNAMA(DPJP)

                        If dsDoctor IsNot Nothing Then
                            'dsRekap.KDDOCTOR = dsDoctor.KDDOCTOR
                        Else
                            MsgBox("DPJP " & DPJP & " Tidak ditemukan di Database", MsgBoxStyle.Exclamation, Me.Text)
                            fn_SearchTransaksi = False
                            Exit Function
                        End If

                        dsRekapAdministrasi.JUMLAH = 5000
                        dsRekapAdministrasi.REMARKS = "Auto"
                        dsRekapAdministrasi.KD_KASIR = 0
                        dsRekapAdministrasi.NO_TRANSAKSI = 0
                        dsRekapAdministrasi.URUT = 0
                        dsRekapAdministrasi.TGL_TRANSAKSI = Now
                        dsRekapAdministrasi.KD_USER = 0
                        dsRekapAdministrasi.KD_TARIF = 0
                        dsRekapAdministrasi.KD_PRODUK = 0
                        dsRekapAdministrasi.KD_UNIT = 0
                        dsRekapAdministrasi.TGL_BERLAKU = Now
                        dsRekapAdministrasi.CHARGE = 0
                        dsRekapAdministrasi.ADJUST = 0
                        dsRekapAdministrasi.FOLIO = 0
                        dsRekapAdministrasi.QTY = 0
                        dsRekapAdministrasi.HARGA = 0
                        dsRekapAdministrasi.KD_DOKTER = 0
                        dsRekapAdministrasi.KD_UNIT_TR = 0
                        dsRekapAdministrasi.CITO = 0
                        dsRekapAdministrasi.JS = 0
                        dsRekapAdministrasi.JP = 0
                        dsRekapAdministrasi.NO_FAKTUR = 0
                        dsRekapAdministrasi.FLAG = 0
                        dsRekapAdministrasi.TAG = 0
                        dsRekapAdministrasi.HRG_ASLI = 0
                        dsRekapAdministrasi.KD_CUSTOMER = 0
                        dsRekapAdministrasi.CLOSE_SHIFT_STATUS = 0
                        dsRekapAdministrasi.KD_LOKET = 0

                        arrDetail.Add(dsRekapAdministrasi)


                        Dim dsRekapSpesialis As New DataAccess.I_JASA_D

                        GRANDTOTAL += 50000

                        dsRekapSpesialis.KDJASA = KDJASA
                        dsRekapSpesialis.SEQ = 1

                        dsRekapSpesialis.KDITEM = 261

                        dsRekapSpesialis.KDDEPARTMENT = KD_UNIT

                        dsRekapSpesialis.KDDOCTOR = KD_DOKTER

                        If dsDoctor IsNot Nothing Then
                            'dsRekap.KDDOCTOR = dsDoctor.KDDOCTOR
                        Else
                            MsgBox("DPJP " & DPJP & " Tidak ditemukan di Database", MsgBoxStyle.Exclamation, Me.Text)
                            fn_SearchTransaksi = False
                            Exit Function
                        End If

                        dsRekapSpesialis.JUMLAH = 50000
                        dsRekapSpesialis.REMARKS = "Auto"
                        dsRekapSpesialis.KD_KASIR = 0
                        dsRekapSpesialis.NO_TRANSAKSI = 0
                        dsRekapSpesialis.URUT = 0
                        dsRekapSpesialis.TGL_TRANSAKSI = Now
                        dsRekapSpesialis.KD_USER = 0
                        dsRekapSpesialis.KD_TARIF = 0
                        dsRekapSpesialis.KD_PRODUK = 0
                        dsRekapSpesialis.KD_UNIT = 0
                        dsRekapSpesialis.TGL_BERLAKU = Now
                        dsRekapSpesialis.CHARGE = 0
                        dsRekapSpesialis.ADJUST = 0
                        dsRekapSpesialis.FOLIO = 0
                        dsRekapSpesialis.QTY = 0
                        dsRekapSpesialis.HARGA = 0
                        dsRekapSpesialis.KD_DOKTER = 0
                        dsRekapSpesialis.KD_UNIT_TR = 0
                        dsRekapSpesialis.CITO = 0
                        dsRekapSpesialis.JS = 0
                        dsRekapSpesialis.JP = 0
                        dsRekapSpesialis.NO_FAKTUR = 0
                        dsRekapSpesialis.FLAG = 0
                        dsRekapSpesialis.TAG = 0
                        dsRekapSpesialis.HRG_ASLI = 0
                        dsRekapSpesialis.KD_CUSTOMER = 0
                        dsRekapSpesialis.CLOSE_SHIFT_STATUS = 0
                        dsRekapSpesialis.KD_LOKET = 0

                        arrDetail.Add(dsRekapSpesialis)

                        If OBAT <> 0 Then
                            Dim dsRekapObat As New DataAccess.I_JASA_D

                            GRANDTOTAL += OBAT

                            dsRekapObat.KDJASA = KDJASA
                            dsRekapObat.SEQ = 2

                            dsRekapObat.KDITEM = 4

                            dsRekapObat.KDDEPARTMENT = KD_UNIT

                            dsRekapObat.KDDOCTOR = KD_DOKTER

                            If dsDoctor IsNot Nothing Then
                                'dsRekap.KDDOCTOR = dsDoctor.KDDOCTOR
                            Else
                                MsgBox("DPJP " & DPJP & " Tidak ditemukan di Database", MsgBoxStyle.Exclamation, Me.Text)
                                fn_SearchTransaksi = False
                                Exit Function
                            End If

                            dsRekapObat.JUMLAH = OBAT
                            dsRekapObat.REMARKS = "Auto"
                            dsRekapObat.KD_KASIR = 0
                            dsRekapObat.NO_TRANSAKSI = 0
                            dsRekapObat.URUT = 0
                            dsRekapObat.TGL_TRANSAKSI = Now
                            dsRekapObat.KD_USER = 0
                            dsRekapObat.KD_TARIF = 0
                            dsRekapObat.KD_PRODUK = 0
                            dsRekapObat.KD_UNIT = 0
                            dsRekapObat.TGL_BERLAKU = Now
                            dsRekapObat.CHARGE = 0
                            dsRekapObat.ADJUST = 0
                            dsRekapObat.FOLIO = 0
                            dsRekapObat.QTY = 0
                            dsRekapObat.HARGA = 0
                            dsRekapObat.KD_DOKTER = 0
                            dsRekapObat.KD_UNIT_TR = 0
                            dsRekapObat.CITO = 0
                            dsRekapObat.JS = 0
                            dsRekapObat.JP = 0
                            dsRekapObat.NO_FAKTUR = 0
                            dsRekapObat.FLAG = 0
                            dsRekapObat.TAG = 0
                            dsRekapObat.HRG_ASLI = 0
                            dsRekapObat.KD_CUSTOMER = 0
                            dsRekapObat.CLOSE_SHIFT_STATUS = 0
                            dsRekapObat.KD_LOKET = 0

                            arrDetail.Add(dsRekapObat)
                        End If

                        If LABORATORIUM <> 0 Then
                            Dim dsRekapLaboratorium As New DataAccess.I_JASA_D

                            GRANDTOTAL += LABORATORIUM

                            dsRekapLaboratorium.KDJASA = KDJASA
                            dsRekapLaboratorium.SEQ = 3

                            dsRekapLaboratorium.KDITEM = 2

                            dsRekapLaboratorium.KDDEPARTMENT = KD_UNIT

                            dsRekapLaboratorium.KDDOCTOR = KD_DOKTER

                            If dsDoctor IsNot Nothing Then
                                'dsRekap.KDDOCTOR = dsDoctor.KDDOCTOR
                            Else
                                MsgBox("DPJP " & DPJP & " Tidak ditemukan di Database", MsgBoxStyle.Exclamation, Me.Text)
                                fn_SearchTransaksi = False
                                Exit Function
                            End If

                            dsRekapLaboratorium.JUMLAH = LABORATORIUM
                            dsRekapLaboratorium.REMARKS = "Auto"
                            dsRekapLaboratorium.KD_KASIR = 0
                            dsRekapLaboratorium.NO_TRANSAKSI = 0
                            dsRekapLaboratorium.URUT = 0
                            dsRekapLaboratorium.TGL_TRANSAKSI = Now
                            dsRekapLaboratorium.KD_USER = 0
                            dsRekapLaboratorium.KD_TARIF = 0
                            dsRekapLaboratorium.KD_PRODUK = 0
                            dsRekapLaboratorium.KD_UNIT = 0
                            dsRekapLaboratorium.TGL_BERLAKU = Now
                            dsRekapLaboratorium.CHARGE = 0
                            dsRekapLaboratorium.ADJUST = 0
                            dsRekapLaboratorium.FOLIO = 0
                            dsRekapLaboratorium.QTY = 0
                            dsRekapLaboratorium.HARGA = 0
                            dsRekapLaboratorium.KD_DOKTER = 0
                            dsRekapLaboratorium.KD_UNIT_TR = 0
                            dsRekapLaboratorium.CITO = 0
                            dsRekapLaboratorium.JS = 0
                            dsRekapLaboratorium.JP = 0
                            dsRekapLaboratorium.NO_FAKTUR = 0
                            dsRekapLaboratorium.FLAG = 0
                            dsRekapLaboratorium.TAG = 0
                            dsRekapLaboratorium.HRG_ASLI = 0
                            dsRekapLaboratorium.KD_CUSTOMER = 0
                            dsRekapLaboratorium.CLOSE_SHIFT_STATUS = 0
                            dsRekapLaboratorium.KD_LOKET = 0

                            arrDetail.Add(dsRekapLaboratorium)
                        End If

                        If RADIOLOGI <> 0 Then
                            Dim dsRekapRadiologi As New DataAccess.I_JASA_D

                            GRANDTOTAL += RADIOLOGI

                            dsRekapRadiologi.KDJASA = KDJASA
                            dsRekapRadiologi.SEQ = 4

                            dsRekapRadiologi.KDITEM = 7

                            dsRekapRadiologi.KDDEPARTMENT = KD_UNIT

                            dsRekapRadiologi.KDDOCTOR = KD_DOKTER

                            If dsDoctor IsNot Nothing Then
                                'dsRekap.KDDOCTOR = dsDoctor.KDDOCTOR
                            Else
                                MsgBox("DPJP " & DPJP & " Tidak ditemukan di Database", MsgBoxStyle.Exclamation, Me.Text)
                                fn_SearchTransaksi = False
                                Exit Function
                            End If

                            dsRekapRadiologi.JUMLAH = RADIOLOGI
                            dsRekapRadiologi.REMARKS = "Auto"
                            dsRekapRadiologi.KD_KASIR = 0
                            dsRekapRadiologi.NO_TRANSAKSI = 0
                            dsRekapRadiologi.URUT = 0
                            dsRekapRadiologi.TGL_TRANSAKSI = Now
                            dsRekapRadiologi.KD_USER = 0
                            dsRekapRadiologi.KD_TARIF = 0
                            dsRekapRadiologi.KD_PRODUK = 0
                            dsRekapRadiologi.KD_UNIT = 0
                            dsRekapRadiologi.TGL_BERLAKU = Now
                            dsRekapRadiologi.CHARGE = 0
                            dsRekapRadiologi.ADJUST = 0
                            dsRekapRadiologi.FOLIO = 0
                            dsRekapRadiologi.QTY = 0
                            dsRekapRadiologi.HARGA = 0
                            dsRekapRadiologi.KD_DOKTER = 0
                            dsRekapRadiologi.KD_UNIT_TR = 0
                            dsRekapRadiologi.CITO = 0
                            dsRekapRadiologi.JS = 0
                            dsRekapRadiologi.JP = 0
                            dsRekapRadiologi.NO_FAKTUR = 0
                            dsRekapRadiologi.FLAG = 0
                            dsRekapRadiologi.TAG = 0
                            dsRekapRadiologi.HRG_ASLI = 0
                            dsRekapRadiologi.KD_CUSTOMER = 0
                            dsRekapRadiologi.CLOSE_SHIFT_STATUS = 0
                            dsRekapRadiologi.KD_LOKET = 0

                            arrDetail.Add(dsRekapRadiologi)
                        End If

                        ' ***** HEADER *****
                        With dsJasa
                            .DATECREATED = Now
                            .DATEUPDATED = Now
                            .KDJASA = KDJASA
                            .DATE = DATE_MASUK
                            .CATEGORY = rbCATEGORY.SelectedIndex
                            .NOSEP = NOSEP
                            .KDCUSTOMER = KD_PASIEN
                            .NO_TRANSAKSI = NO_TRANSAKSI
                            .KD_KASIR = IIf(rbCATEGORY.SelectedIndex = 0, "", "02")
                            .DESCRIPTION = "Auto"
                            .KDUSER = sUserID
                            .GRANDTOTAL = GRANDTOTAL
                            .CATEGORYBAYAR = 0
                            .CATEGORYRUMUS = 0

                        End With

                        If KDJASA = String.Empty Then
                            oJasa.InsertData(dsJasa, arrDetail)
                        Else
                            oJasa.UpdateData(dsJasa, arrDetail)
                        End If


                        Exit Function

                    Else

                        If KDJASA = String.Empty Then
                            oJasa.InsertData(dsJasa, arrDetail)
                        Else
                            oJasa.UpdateData(dsJasa, arrDetail)
                        End If

                    End If

                Else
                    For iLoop As Integer = 0 To ds.Tables("TRANSAKSIDETAIL").Rows.Count - 1
                        Dim dsRekap As New DataAccess.I_JASA_D

                        With ds.Tables("TRANSAKSIDETAIL")

                            dsRekap.KDJASA = KDJASA
                            dsRekap.SEQ = SEQ

                            SEQ += 1

                            If .Rows(iLoop)("KD_PRODUK") <> 0 Then
                                InsertItem(.Rows(iLoop)("KD_PRODUK"), .Rows(iLoop)("NAMA_PRODUK"), .Rows(iLoop)("GRANDTOTAL"))
                            End If

                            dsRekap.KDITEM = .Rows(iLoop)("KD_PRODUK")

                            If .Rows(iLoop)("KD_UNIT") <> "" Then
                                InsertDepartment(.Rows(iLoop)("KD_UNIT"), .Rows(iLoop)("NAMA_UNIT"))
                            End If

                            dsRekap.KDDEPARTMENT = .Rows(iLoop)("KD_UNIT")

                            If rbCATEGORY.SelectedIndex = 0 Then
                                dsRekap.KDDOCTOR = KD_DOKTER

                                Dim oDoctor As New Reference.clsDoctor
                                Dim dsDoctor = oDoctor.GetDataDetail_DOCTORBPJSByNAMA(DPJP)

                                If dsDoctor IsNot Nothing Then
                                    'dsRekap.KDDOCTOR = dsDoctor.KDDOCTOR
                                Else
                                    MsgBox("DPJP " & DPJP & " Tidak ditemukan di Database", MsgBoxStyle.Exclamation, Me.Text)
                                    fn_SearchTransaksi = False
                                    Exit Function
                                End If

                            Else

                                If .Rows(iLoop)("KDDOKTERVISITE") <> "" Then
                                    InsertDoctor(.Rows(iLoop)("KDDOKTERVISITE"), .Rows(iLoop)("NAMA_DOKTERVISITE"))

                                    dsRekap.KDDOCTOR = .Rows(iLoop)("KDDOKTERVISITE")

                                Else
                                    dsRekap.KDDOCTOR = KD_DOKTER

                                    Dim oDoctor As New Reference.clsDoctor
                                    Dim dsDoctor = oDoctor.GetDataDetail_DOCTORBPJSByNAMA(DPJP)

                                    If dsDoctor IsNot Nothing Then
                                        'dsRekap.KDDOCTOR = dsDoctor.KDDOCTOR
                                    Else
                                        MsgBox("DPJP " & DPJP & " Tidak ditemukan di Database", MsgBoxStyle.Exclamation, Me.Text)
                                        fn_SearchTransaksi = False
                                        Exit Function
                                    End If

                                End If
                            End If

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

                        End With

                        arrDetail.Add(dsRekap)

                    Next

                    ' ***** HEADER *****
                    Dim dsJasa = oJasa.GetStructureHeader
                    With dsJasa
                        .DATECREATED = Now
                        .DATEUPDATED = Now
                        .KDJASA = KDJASA
                        .DATE = DATE_MASUK
                        .CATEGORY = rbCATEGORY.SelectedIndex
                        .NOSEP = NOSEP
                        .KDCUSTOMER = KD_PASIEN
                        .NO_TRANSAKSI = NO_TRANSAKSI
                        .KD_KASIR = IIf(rbCATEGORY.SelectedIndex = 0, "", "02")
                        .DESCRIPTION = txtMEMO.Text.Trim.ToUpper
                        .KDUSER = sUserID
                        .GRANDTOTAL = GRANDTOTAL
                        .CATEGORYBAYAR = 0
                        .CATEGORYRUMUS = 0
                    End With

                    If TARIF_RS <> GRANDTOTAL Then
                        fn_SearchTransaksi = False

                        arrDetail.Clear()

                        GRANDTOTAL = 0

                        Dim dsRekapAdministrasi As New DataAccess.I_JASA_D

                        GRANDTOTAL += 5000

                        dsRekapAdministrasi.KDJASA = KDJASA
                        dsRekapAdministrasi.SEQ = 0


                        dsRekapAdministrasi.KDITEM = 18

                        dsRekapAdministrasi.KDDEPARTMENT = KD_UNIT

                        dsRekapAdministrasi.KDDOCTOR = KD_DOKTER

                        Dim oDoctor As New Reference.clsDoctor
                        Dim dsDoctor = oDoctor.GetDataDetail_DOCTORBPJSByNAMA(DPJP)

                        If dsDoctor IsNot Nothing Then
                            'dsRekap.KDDOCTOR = dsDoctor.KDDOCTOR
                        Else
                            MsgBox("DPJP " & DPJP & " Tidak ditemukan di Database", MsgBoxStyle.Exclamation, Me.Text)
                            fn_SearchTransaksi = False
                            Exit Function
                        End If

                        dsRekapAdministrasi.JUMLAH = 5000
                        dsRekapAdministrasi.REMARKS = "Auto"
                        dsRekapAdministrasi.KD_KASIR = 0
                        dsRekapAdministrasi.NO_TRANSAKSI = 0
                        dsRekapAdministrasi.URUT = 0
                        dsRekapAdministrasi.TGL_TRANSAKSI = Now
                        dsRekapAdministrasi.KD_USER = 0
                        dsRekapAdministrasi.KD_TARIF = 0
                        dsRekapAdministrasi.KD_PRODUK = 0
                        dsRekapAdministrasi.KD_UNIT = 0
                        dsRekapAdministrasi.TGL_BERLAKU = Now
                        dsRekapAdministrasi.CHARGE = 0
                        dsRekapAdministrasi.ADJUST = 0
                        dsRekapAdministrasi.FOLIO = 0
                        dsRekapAdministrasi.QTY = 0
                        dsRekapAdministrasi.HARGA = 0
                        dsRekapAdministrasi.KD_DOKTER = 0
                        dsRekapAdministrasi.KD_UNIT_TR = 0
                        dsRekapAdministrasi.CITO = 0
                        dsRekapAdministrasi.JS = 0
                        dsRekapAdministrasi.JP = 0
                        dsRekapAdministrasi.NO_FAKTUR = 0
                        dsRekapAdministrasi.FLAG = 0
                        dsRekapAdministrasi.TAG = 0
                        dsRekapAdministrasi.HRG_ASLI = 0
                        dsRekapAdministrasi.KD_CUSTOMER = 0
                        dsRekapAdministrasi.CLOSE_SHIFT_STATUS = 0
                        dsRekapAdministrasi.KD_LOKET = 0

                        arrDetail.Add(dsRekapAdministrasi)


                        Dim dsRekapSpesialis As New DataAccess.I_JASA_D

                        GRANDTOTAL += 50000

                        dsRekapSpesialis.KDJASA = KDJASA
                        dsRekapSpesialis.SEQ = 1

                        dsRekapSpesialis.KDITEM = 261

                        dsRekapSpesialis.KDDEPARTMENT = KD_UNIT

                        dsRekapSpesialis.KDDOCTOR = KD_DOKTER

                        If dsDoctor IsNot Nothing Then
                            'dsRekap.KDDOCTOR = dsDoctor.KDDOCTOR
                        Else
                            MsgBox("DPJP " & DPJP & " Tidak ditemukan di Database", MsgBoxStyle.Exclamation, Me.Text)
                            fn_SearchTransaksi = False
                            Exit Function
                        End If

                        dsRekapSpesialis.JUMLAH = 50000
                        dsRekapSpesialis.REMARKS = "Auto"
                        dsRekapSpesialis.KD_KASIR = 0
                        dsRekapSpesialis.NO_TRANSAKSI = 0
                        dsRekapSpesialis.URUT = 0
                        dsRekapSpesialis.TGL_TRANSAKSI = Now
                        dsRekapSpesialis.KD_USER = 0
                        dsRekapSpesialis.KD_TARIF = 0
                        dsRekapSpesialis.KD_PRODUK = 0
                        dsRekapSpesialis.KD_UNIT = 0
                        dsRekapSpesialis.TGL_BERLAKU = Now
                        dsRekapSpesialis.CHARGE = 0
                        dsRekapSpesialis.ADJUST = 0
                        dsRekapSpesialis.FOLIO = 0
                        dsRekapSpesialis.QTY = 0
                        dsRekapSpesialis.HARGA = 0
                        dsRekapSpesialis.KD_DOKTER = 0
                        dsRekapSpesialis.KD_UNIT_TR = 0
                        dsRekapSpesialis.CITO = 0
                        dsRekapSpesialis.JS = 0
                        dsRekapSpesialis.JP = 0
                        dsRekapSpesialis.NO_FAKTUR = 0
                        dsRekapSpesialis.FLAG = 0
                        dsRekapSpesialis.TAG = 0
                        dsRekapSpesialis.HRG_ASLI = 0
                        dsRekapSpesialis.KD_CUSTOMER = 0
                        dsRekapSpesialis.CLOSE_SHIFT_STATUS = 0
                        dsRekapSpesialis.KD_LOKET = 0

                        arrDetail.Add(dsRekapSpesialis)

                        If OBAT <> 0 Then
                            Dim dsRekapObat As New DataAccess.I_JASA_D

                            GRANDTOTAL += OBAT

                            dsRekapObat.KDJASA = KDJASA
                            dsRekapObat.SEQ = 2

                            dsRekapObat.KDITEM = 4

                            dsRekapObat.KDDEPARTMENT = KD_UNIT

                            dsRekapObat.KDDOCTOR = KD_DOKTER

                            If dsDoctor IsNot Nothing Then
                                'dsRekap.KDDOCTOR = dsDoctor.KDDOCTOR
                            Else
                                MsgBox("DPJP " & DPJP & " Tidak ditemukan di Database", MsgBoxStyle.Exclamation, Me.Text)
                                fn_SearchTransaksi = False
                                Exit Function
                            End If

                            dsRekapObat.JUMLAH = OBAT
                            dsRekapObat.REMARKS = "Auto"
                            dsRekapObat.KD_KASIR = 0
                            dsRekapObat.NO_TRANSAKSI = 0
                            dsRekapObat.URUT = 0
                            dsRekapObat.TGL_TRANSAKSI = Now
                            dsRekapObat.KD_USER = 0
                            dsRekapObat.KD_TARIF = 0
                            dsRekapObat.KD_PRODUK = 0
                            dsRekapObat.KD_UNIT = 0
                            dsRekapObat.TGL_BERLAKU = Now
                            dsRekapObat.CHARGE = 0
                            dsRekapObat.ADJUST = 0
                            dsRekapObat.FOLIO = 0
                            dsRekapObat.QTY = 0
                            dsRekapObat.HARGA = 0
                            dsRekapObat.KD_DOKTER = 0
                            dsRekapObat.KD_UNIT_TR = 0
                            dsRekapObat.CITO = 0
                            dsRekapObat.JS = 0
                            dsRekapObat.JP = 0
                            dsRekapObat.NO_FAKTUR = 0
                            dsRekapObat.FLAG = 0
                            dsRekapObat.TAG = 0
                            dsRekapObat.HRG_ASLI = 0
                            dsRekapObat.KD_CUSTOMER = 0
                            dsRekapObat.CLOSE_SHIFT_STATUS = 0
                            dsRekapObat.KD_LOKET = 0

                            arrDetail.Add(dsRekapObat)
                        End If

                        If LABORATORIUM <> 0 Then
                            Dim dsRekapLaboratorium As New DataAccess.I_JASA_D

                            GRANDTOTAL += LABORATORIUM

                            dsRekapLaboratorium.KDJASA = KDJASA
                            dsRekapLaboratorium.SEQ = 3

                            dsRekapLaboratorium.KDITEM = 2

                            dsRekapLaboratorium.KDDEPARTMENT = KD_UNIT

                            dsRekapLaboratorium.KDDOCTOR = KD_DOKTER

                            If dsDoctor IsNot Nothing Then
                                'dsRekap.KDDOCTOR = dsDoctor.KDDOCTOR
                            Else
                                MsgBox("DPJP " & DPJP & " Tidak ditemukan di Database", MsgBoxStyle.Exclamation, Me.Text)
                                fn_SearchTransaksi = False
                                Exit Function
                            End If

                            dsRekapLaboratorium.JUMLAH = LABORATORIUM
                            dsRekapLaboratorium.REMARKS = "Auto"
                            dsRekapLaboratorium.KD_KASIR = 0
                            dsRekapLaboratorium.NO_TRANSAKSI = 0
                            dsRekapLaboratorium.URUT = 0
                            dsRekapLaboratorium.TGL_TRANSAKSI = Now
                            dsRekapLaboratorium.KD_USER = 0
                            dsRekapLaboratorium.KD_TARIF = 0
                            dsRekapLaboratorium.KD_PRODUK = 0
                            dsRekapLaboratorium.KD_UNIT = 0
                            dsRekapLaboratorium.TGL_BERLAKU = Now
                            dsRekapLaboratorium.CHARGE = 0
                            dsRekapLaboratorium.ADJUST = 0
                            dsRekapLaboratorium.FOLIO = 0
                            dsRekapLaboratorium.QTY = 0
                            dsRekapLaboratorium.HARGA = 0
                            dsRekapLaboratorium.KD_DOKTER = 0
                            dsRekapLaboratorium.KD_UNIT_TR = 0
                            dsRekapLaboratorium.CITO = 0
                            dsRekapLaboratorium.JS = 0
                            dsRekapLaboratorium.JP = 0
                            dsRekapLaboratorium.NO_FAKTUR = 0
                            dsRekapLaboratorium.FLAG = 0
                            dsRekapLaboratorium.TAG = 0
                            dsRekapLaboratorium.HRG_ASLI = 0
                            dsRekapLaboratorium.KD_CUSTOMER = 0
                            dsRekapLaboratorium.CLOSE_SHIFT_STATUS = 0
                            dsRekapLaboratorium.KD_LOKET = 0

                            arrDetail.Add(dsRekapLaboratorium)
                        End If

                        If RADIOLOGI <> 0 Then
                            Dim dsRekapRadiologi As New DataAccess.I_JASA_D

                            GRANDTOTAL += RADIOLOGI

                            dsRekapRadiologi.KDJASA = KDJASA
                            dsRekapRadiologi.SEQ = 4

                            dsRekapRadiologi.KDITEM = 7

                            dsRekapRadiologi.KDDEPARTMENT = KD_UNIT

                            dsRekapRadiologi.KDDOCTOR = KD_DOKTER

                            If dsDoctor IsNot Nothing Then
                                'dsRekap.KDDOCTOR = dsDoctor.KDDOCTOR
                            Else
                                MsgBox("DPJP " & DPJP & " Tidak ditemukan di Database", MsgBoxStyle.Exclamation, Me.Text)
                                fn_SearchTransaksi = False
                                Exit Function
                            End If

                            dsRekapRadiologi.JUMLAH = RADIOLOGI
                            dsRekapRadiologi.REMARKS = "Auto"
                            dsRekapRadiologi.KD_KASIR = 0
                            dsRekapRadiologi.NO_TRANSAKSI = 0
                            dsRekapRadiologi.URUT = 0
                            dsRekapRadiologi.TGL_TRANSAKSI = Now
                            dsRekapRadiologi.KD_USER = 0
                            dsRekapRadiologi.KD_TARIF = 0
                            dsRekapRadiologi.KD_PRODUK = 0
                            dsRekapRadiologi.KD_UNIT = 0
                            dsRekapRadiologi.TGL_BERLAKU = Now
                            dsRekapRadiologi.CHARGE = 0
                            dsRekapRadiologi.ADJUST = 0
                            dsRekapRadiologi.FOLIO = 0
                            dsRekapRadiologi.QTY = 0
                            dsRekapRadiologi.HARGA = 0
                            dsRekapRadiologi.KD_DOKTER = 0
                            dsRekapRadiologi.KD_UNIT_TR = 0
                            dsRekapRadiologi.CITO = 0
                            dsRekapRadiologi.JS = 0
                            dsRekapRadiologi.JP = 0
                            dsRekapRadiologi.NO_FAKTUR = 0
                            dsRekapRadiologi.FLAG = 0
                            dsRekapRadiologi.TAG = 0
                            dsRekapRadiologi.HRG_ASLI = 0
                            dsRekapRadiologi.KD_CUSTOMER = 0
                            dsRekapRadiologi.CLOSE_SHIFT_STATUS = 0
                            dsRekapRadiologi.KD_LOKET = 0

                            arrDetail.Add(dsRekapRadiologi)
                        End If

                        ' ***** HEADER *****
                        With dsJasa
                            .DATECREATED = Now
                            .DATEUPDATED = Now
                            .KDJASA = KDJASA
                            .DATE = DATE_MASUK
                            .CATEGORY = rbCATEGORY.SelectedIndex
                            .NOSEP = NOSEP
                            .KDCUSTOMER = KD_PASIEN
                            .NO_TRANSAKSI = NO_TRANSAKSI
                            .KD_KASIR = IIf(rbCATEGORY.SelectedIndex = 0, "", "02")
                            .DESCRIPTION = "Auto-"
                            .KDUSER = sUserID
                            .GRANDTOTAL = GRANDTOTAL
                            .CATEGORYBAYAR = 0
                            .CATEGORYRUMUS = 0

                        End With

                        If KDJASA = String.Empty Then
                            oJasa.InsertData(dsJasa, arrDetail)
                        Else
                            oJasa.UpdateData(dsJasa, arrDetail)
                        End If

                        Exit Function

                    Else
                        If KDJASA = String.Empty Then
                            oJasa.InsertData(dsJasa, arrDetail)
                        Else
                            oJasa.UpdateData(dsJasa, arrDetail)
                        End If

                    End If
                End If

            Else
                For iLoop As Integer = 0 To ds.Tables("TRANSAKKSI_H").Rows.Count - 1

                    With ds.Tables("TRANSAKKSI_H")
                        NO_TRANSAKSI = .Rows(iLoop)("NO_TRANSAKSI")
                    End With

                Next

                Dim GRANDTOTAL As Decimal = 0

                For index As Integer = 0 To 5
                    GRANDTOTAL = 0

                    Dim dsDatabase = oJasa.GetDataSetting

                    SQL = dsDatabase.COA_OTHER_EXPENSE
                    SQL &= " @CATEGORY = " & rbCATEGORY.SelectedIndex & ", "
                    SQL &= " @KD_KASIR = '02', "
                    SQL &= " @NO_TRANSAKSI = '" & NO_TRANSAKSI & "', "
                    SQL &= " @KD_PASIEN = '" & KD_PASIEN & "', "
                    SQL &= " @RUMUS = '" & index & "' "

                    oComm.Connection = oConn
                    oComm.CommandText = SQL
                    oComm.CommandTimeout = 120
                    oComm.CommandType = CommandType.Text

                    da = New SqlDataAdapter(oComm)
                    da.Fill(ds, "ALL")


                    Dim Cari As Boolean = False
                    Dim arrDetail = oJasa.GetStructureDetailList

                    For iLoop As Integer = 0 To ds.Tables("ALL").Rows.Count - 1
                        With ds.Tables("ALL")
                            GRANDTOTAL += .Rows(iLoop)("GRANDTOTAL")
                        End With
                    Next

                    If TARIF_RS = GRANDTOTAL Then
                        Exit For
                    End If

                    fn_SearchTransaksi = False

                Next

                If fn_SearchTransaksi = True Then
                    Dim arrDetail = oJasa.GetStructureDetailList
                    Dim SEQ As Integer = 0

                    For iLoop As Integer = 0 To ds.Tables("ALL").Rows.Count - 1
                        Dim dsRekap As New DataAccess.I_JASA_D

                        With ds.Tables("ALL")

                            dsRekap.KDJASA = KDJASA
                            dsRekap.SEQ = SEQ

                            SEQ += 1

                            If .Rows(iLoop)("KD_PRODUK") <> 0 Then
                                InsertItem(.Rows(iLoop)("KD_PRODUK"), .Rows(iLoop)("NAMA_PRODUK"), .Rows(iLoop)("GRANDTOTAL"))
                            End If

                            dsRekap.KDITEM = .Rows(iLoop)("KD_PRODUK")

                            If .Rows(iLoop)("KD_UNIT") <> "" Then
                                InsertDepartment(.Rows(iLoop)("KD_UNIT"), .Rows(iLoop)("NAMA_UNIT"))
                            End If

                            dsRekap.KDDEPARTMENT = .Rows(iLoop)("KD_UNIT")

                            If rbCATEGORY.SelectedIndex = 0 Then
                                dsRekap.KDDOCTOR = KD_DOKTER

                                Dim oDoctor As New Reference.clsDoctor
                                Dim dsDoctor = oDoctor.GetDataDetail_DOCTORBPJSByNAMA(DPJP)

                                If dsDoctor IsNot Nothing Then
                                    'dsRekap.KDDOCTOR = dsDoctor.KDDOCTOR
                                Else
                                    MsgBox("DPJP " & DPJP & " Tidak ditemukan di Database", MsgBoxStyle.Exclamation, Me.Text)
                                    fn_SearchTransaksi = False
                                    Exit Function
                                End If

                            Else

                                If .Rows(iLoop)("KDDOKTER") <> "" Then
                                    InsertDoctor(.Rows(iLoop)("KDDOKTER"), .Rows(iLoop)("NAMA_DOKTER"))

                                    dsRekap.KDDOCTOR = .Rows(iLoop)("KDDOKTER")

                                Else
                                    dsRekap.KDDOCTOR = KD_DOKTER

                                    Dim oDoctor As New Reference.clsDoctor
                                    Dim dsDoctor = oDoctor.GetDataDetail_DOCTORBPJSByNAMA(DPJP)

                                    If dsDoctor IsNot Nothing Then
                                        'dsRekap.KDDOCTOR = dsDoctor.KDDOCTOR
                                    Else
                                        MsgBox("DPJP " & DPJP & " Tidak ditemukan di Database", MsgBoxStyle.Exclamation, Me.Text)
                                        fn_SearchTransaksi = False
                                        Exit Function
                                    End If

                                End If
                            End If

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

                        End With

                        arrDetail.Add(dsRekap)

                    Next

                    ' ***** HEADER *****
                    Dim dsJasa = oJasa.GetStructureHeader
                    With dsJasa
                        .DATECREATED = Now
                        .DATEUPDATED = Now
                        .KDJASA = KDJASA
                        .DATE = DATE_MASUK
                        .CATEGORY = rbCATEGORY.SelectedIndex
                        .NOSEP = NOSEP
                        .KDCUSTOMER = KD_PASIEN
                        .NO_TRANSAKSI = NO_TRANSAKSI
                        .KD_KASIR = IIf(rbCATEGORY.SelectedIndex = 0, "", "02")
                        .DESCRIPTION = txtMEMO.Text.Trim.ToUpper
                        .KDUSER = sUserID
                        .GRANDTOTAL = GRANDTOTAL
                        .CATEGORYBAYAR = 0
                        .CATEGORYRUMUS = 0
                    End With

                    If KDJASA = String.Empty Then
                        oJasa.InsertData(dsJasa, arrDetail)
                    Else
                        oJasa.UpdateData(dsJasa, arrDetail)
                    End If

                End If

            End If

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            fn_SearchTransaksi = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Public Function InsertCustomer(ByVal KDCUSTOMER As String, ByVal Nama As String, ByVal Alamat As String) As Boolean
        InsertCustomer = True

        Dim oCustomer As New Reference.clsCustomer
        Try
            Dim dsCustomer = oCustomer.GetData(KDCUSTOMER)

            If dsCustomer IsNot Nothing Then
                'fn_LoadCUSTOMER(dsCustomer.KDCUSTOMER)
                'grdKDCUSTOMER.EditValue = oCustomer.GetData(dsCustomer.KDCUSTOMER).KDCUSTOMER
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

                'fn_LoadDEPARTMENT()

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

                InsertDoctor = oDoctor.InsertData(ds, KDDOCTOR)

                'fn_LoadDOCTOR()

            End If

        Catch ex As Exception
            InsertDoctor = False
            MsgBox(ex.ToString)
        End Try
    End Function
    Public Function InsertItem(ByVal KD_PRODUK As String, ByVal NMITEM2 As String, ByVal PRICE As Decimal) As Boolean
        'Dim oItem As New Reference.clsItem

        'InsertItem = True

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

        '        oItem.InsertData(ds, arrDetail_UOM, KD_PRODUK)

        '    End If

        'Catch ex As Exception
        '    InsertItem = False
        '    MsgBox(ex.ToString)
        'End Try
    End Function
    Private Sub rbCATEGORY_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rbCATEGORY.SelectedIndexChanged
        fn_LoadKDPAYMENTTYPE()
    End Sub
#End Region
End Class