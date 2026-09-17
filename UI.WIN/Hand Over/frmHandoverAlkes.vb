Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient
Imports System.Math
Imports Newtonsoft.Json.Linq

Public Class frmHandoverAlkes
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oHandoverAlkes As New Digital.clsS_DIGITAL_HANDOVERALKEALKES

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, Optional ByVal sID As String = "")
        oFormMode = FormMode
        sNoId = sID
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "Handover Alat Kesehatan"
            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'sCode = txtCODE.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadPOLI()

        Select Case oFormMode
            Case FORM_MODE.FORM_MODE_VIEW
                fn_ViewMode(True)
                fn_LoadData(sNoId)
            Case FORM_MODE.FORM_MODE_ADD
                fn_ViewMode(False)
                fn_EmptyMe()
            Case FORM_MODE.FORM_MODE_EDIT
                fn_ViewMode(False)
                fn_LoadData(sNoId)
            Case Else
                fn_ViewMode(True)
        End Select
    End Sub
    Private Sub fn_ViewMode(ByVal Status As Boolean)
        btnSaveNew.Enabled = Not Status
        btnSaveClose.Enabled = Not Status

        deDATE.Properties.ReadOnly = Status
        grdPOLIRUANGAN.Properties.ReadOnly = True
        grvDetail.OptionsBehavior.ReadOnly = Status

        grvDetail.BestFitColumns()

    End Sub
    Private Sub fn_EmptyMe()
        'txtCODE.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        grdPOLIRUANGAN.ResetText()

        grdPOLIRUANGAN.Text = sKDDEPARTMENT_POLI

         If sHandoverAlkesTambahan = False Then
            For Each iLoop In sKDKUNJUNGANLIST
                grvDetail.Focus()
                grvDetail.AddNewRow()
                grvDetail.SetFocusedRowCellValue(colDATECREATED, Now)
                grvDetail.SetFocusedRowCellValue(colDATEUPDATED, Now)
                grvDetail.SetFocusedRowCellValue(colKDKUNJUNGAN, iLoop)
                grvDetail.SetFocusedRowCellValue(colUSER, sUserID)
                grvDetail.UpdateCurrentRow()
            Next
        End If

    End Sub
    Private Sub fn_LoadData(ByVal sNoid As String)
        Try
            '***** HEADER *****
            Dim ds = oHandoverAlkes.GetData(sNoId)

            With ds
                deDATE.DateTime = .DATECREATED
                grdPOLIRUANGAN.Text = .KDRUANGAN

                BindingSource1.DataSource = ds
                grdDetail.DataSource = BindingSource1

                tabControl.SelectedTabPage = tab1
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If grdPOLIRUANGAN.Text = String.Empty Then
                grdPOLIRUANGAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdPOLIRUANGAN.ErrorText = Statement.ErrorRequired

                grdPOLIRUANGAN.Focus()
                fn_Validate = False
                Exit Function
            End If

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
            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    ' ***** DETIL *****
                    Dim arrHeader = oHandoverAlkes.GetStructureHeaderList
                    For i As Integer = 0 To grvDetail.RowCount - 2
                        Dim ds = oHandoverAlkes.GetStructureHeader
                        With ds
                            '.ID = i
                            .DATECREATED = grvDetail.GetRowCellValue(i, colDATECREATED)
                            .DATEUPDATED = grvDetail.GetRowCellValue(i, colDATEUPDATED)
                            .KDKUNJUNGAN = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colKDKUNJUNGAN)), "", grvDetail.GetRowCellValue(i, colKDKUNJUNGAN))
                            .NAME_DISPLAY = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colNAME_DISPLAY)), "", grvDetail.GetRowCellValue(i, colNAME_DISPLAY))
                            .STETOSKOP_ADA = grvDetail.GetRowCellValue(i, colSTETOSKOP_ADA)
                            .STETOSKOP_BERFUNGSI = grvDetail.GetRowCellValue(i, colSTETOSKOP_BERFUNGSI)
                            .SPHYGMOMANOMETER_ADA = grvDetail.GetRowCellValue(i, colSPHYGMOMANOMETER_ADA)
                            .SPHYGMOMANOMETER_BERFUNGSI = grvDetail.GetRowCellValue(i, colSPHYGMOMANOMETER_BERFUNGSI)
                            .TERMOMETER_ADA = grvDetail.GetRowCellValue(i, colTERMOMETER_ADA)
                            .TERMOMETER_BERFUNGSI = grvDetail.GetRowCellValue(i, colTERMOMETER_BERFUNGSI)
                            .OXYMETER_ADA = grvDetail.GetRowCellValue(i, colOXYMETER_ADA)
                            .OXYMETER_BERFUNGSI = grvDetail.GetRowCellValue(i, colOXYMETER_BERFUNGSI)
                            .EKG_ADA = grvDetail.GetRowCellValue(i, colEKG_ADA)
                            .EKG_BERFUNGSI = grvDetail.GetRowCellValue(i, colEKG_BERFUNGSI)
                            .KDRUANGAN = sKDDEPARTMENT_POLI
                            .NAMARUANGAN = sNAMEDISPLAY_POLI
                            .SESI = "-"
                            .KDUSER = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colUSER)), "", grvDetail.GetRowCellValue(i, colUSER))
                        End With
                        arrHeader.Add(ds)
                    Next

                    fn_Save = oHandoverAlkes.InsertDataList(arrHeader)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try

                    ' ***** UPDATE SATUAN *****
                    Dim ds = oHandoverAlkes.GetStructureHeader
                    With ds
                        .ID = grvDetail.GetRowCellValue(i, colID)
                        .DATECREATED = grvDetail.GetRowCellValue(i, colDATECREATED)
                        .DATEUPDATED = grvDetail.GetRowCellValue(i, colDATEUPDATED)
                        .KDKUNJUNGAN = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colKDKUNJUNGAN)), "", grvDetail.GetRowCellValue(i, colKDKUNJUNGAN))
                        .NAME_DISPLAY = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colNAME_DISPLAY)), "", grvDetail.GetRowCellValue(i, colNAME_DISPLAY))
                        .STETOSKOP_ADA = grvDetail.GetRowCellValue(i, colSTETOSKOP_ADA)
                        .STETOSKOP_BERFUNGSI = grvDetail.GetRowCellValue(i, colSTETOSKOP_BERFUNGSI)
                        .SPHYGMOMANOMETER_ADA = grvDetail.GetRowCellValue(i, colSPHYGMOMANOMETER_ADA)
                        .SPHYGMOMANOMETER_BERFUNGSI = grvDetail.GetRowCellValue(i, colSPHYGMOMANOMETER_BERFUNGSI)
                        .TERMOMETER_ADA = grvDetail.GetRowCellValue(i, colTERMOMETER_ADA)
                        .TERMOMETER_BERFUNGSI = grvDetail.GetRowCellValue(i, colTERMOMETER_BERFUNGSI)
                        .OXYMETER_ADA = grvDetail.GetRowCellValue(i, colOXYMETER_ADA)
                        .OXYMETER_BERFUNGSI = grvDetail.GetRowCellValue(i, colOXYMETER_BERFUNGSI)
                        .EKG_ADA = grvDetail.GetRowCellValue(i, colEKG_ADA)
                        .EKG_BERFUNGSI = grvDetail.GetRowCellValue(i, colEKG_BERFUNGSI)
                        .KDRUANGAN = grvDetail.GetRowCellValue(i, colKDRUANGAN)
                        .NAMARUANGAN = grvDetail.GetRowCellValue(i, colNAMARUANGAN)
                        .SESI = grvDetail.GetRowCellValue(i, colSESI)
                        .KDUSER = grvDetail.GetRowCellValue(i, colUSER)
                    End With

                    fn_Save = oHandoverAlkes.UpdateData(ds)
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
    Private Sub frmHandoverAlkes_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClose_Click()
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
        End Select
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox(Statement.SaveSuccess & vbCrLf, MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadPOLI()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\EMEDREK\", "Database", "").ToString())
            oConn = New SqlConnection(sConn)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "KDPOLI = A.KDRUANGRAWAT "
            SQL &= ",A.NAME_DISPLAY "
            SQL &= "FROM "
            SQL &= "DATABASE_NEW.dbo.M_RUANGRAWAT A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "POLI")

            grdPOLIRUANGAN.Properties.DataSource = ds.Tables("POLI")
            grdPOLIRUANGAN.Properties.ValueMember = "KDPOLI"
            grdPOLIRUANGAN.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub


    Private Sub grvDetail_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDetail.CellValueChanged
        Try
            If e.Column.Name = colKDKUNJUNGAN.Name Then
                If grvDetail.GetFocusedRowCellValue(colKDKUNJUNGAN) IsNot Nothing Then
                    Dim dsKunjungan = oHandoverAlkes.GetDataByKunjungan(grvDetail.GetFocusedRowCellValue(colKDKUNJUNGAN))
                    If dsKunjungan IsNot Nothing Then
                        grvDetail.SetFocusedRowCellValue(colNAME_DISPLAY, dsKunjungan.NAMAPASIEN)
                    End If
                End If
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub btnCariPasien_Click(sender As Object, e As EventArgs) Handles btnCariPasien.Click
        fn_LoadDataKunjunganRawatInap(sKDDEPARTMENT_POLI, txtRM.Text.ToString.PadLeft(8, "0"))
    End Sub

    Private Sub fn_CariDataPasien(ByVal norm As String)

        If txtRM.Text <> "" Then
            Dim dsKunjungan = oHandoverAlkes.GetDataByKunjungan(txtRM.Text)
            If dsKunjungan IsNot Nothing Then
                grvDetail.SetFocusedRowCellValue(colNAME_DISPLAY, dsKunjungan.NAMAPASIEN)
            End If
        End If

        grvDetail.Focus()
        grvDetail.AddNewRow()
        grvDetail.SetFocusedRowCellValue(colDATECREATED, Now)
        grvDetail.SetFocusedRowCellValue(colDATEUPDATED, Now)
        grvDetail.SetFocusedRowCellValue(colKDKUNJUNGAN, "TAMBAHAN")
        grvDetail.SetFocusedRowCellValue(colUSER, sUserID)
        grvDetail.UpdateCurrentRow()
    End Sub

    Private Sub fn_LoadDataKunjunganRawatInap(ByVal KDDEPARTMENT As String, ByVal KDCUSTOMER As String)
        Try
            'SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            'SplashScreenManager.Default.SetWaitFormCaption("Processing data.....")

            Dim Nomor As Integer = 0

            Dim listPendaftaran As New List(Of DataAccess.R_IDENTITAS_PASIEN_LOAD)

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\EMEDREK\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM( "
            SQL &= "SELECT "
            SQL &= "RM = B.KDCUSTOMER "
            SQL &= ",TANGGAL = CONVERT(VARCHAR, A.DATE_MASUK, 3) "
            SQL &= ",REGISTER = A.KDPENDAFTARAN "
            SQL &= ",NAMAPASIEN = F.NAME_DISPLAY "
            SQL &= ",RUANGAN = C.NAME_DISPLAY "
            SQL &= ",KUNJUNGAN = A.KDKUNJUNGAN_RUANGAN "

            SQL &= "FROM DATABASE_NEW.dbo.S_PENDAFTARAN_KUNJUNGANRUANGAN A "
            SQL &= "INNER JOIN DATABASE_NEW.dbo.S_PENDAFTARAN_H B "
            SQL &= "ON A.KDPENDAFTARAN = B.KDPENDAFTARAN "
            SQL &= "INNER JOIN DATABASE_NEW.dbo.M_RUANGRAWAT C "
            SQL &= "ON A.KDRUANGRAWAT = C.KDRUANGRAWAT "
            SQL &= "INNER JOIN DATABASE_NEW.dbo.M_CUSTOMER F "
            SQL &= "ON B.KDCUSTOMER = F.KDCUSTOMER "

            SQL &= "WHERE "
            If KDCUSTOMER <> "" Then
                SQL &= "B.KDCUSTOMER = '" & KDCUSTOMER & "' "
                SQL &= "AND A.ISCHEKED = 0 "
                SQL &= "AND A.KDRUANGRAWAT = '" & KDDEPARTMENT & "' "
            End If

            SQL &= ") AS X "

            If KDCUSTOMER <> "" Then
                SQL &= "ORDER BY TANGGAL DESC "
            End If

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL_1")

            grdCARI.Properties.DataSource = ds.Tables("ALL_1")
            grdCARI.Properties.ValueMember = "KUNJUNGAN"
            grdCARI.Properties.DisplayMember = "NAMAPASIEN"

            'grvCARI.Columns("KDKUNJUNGAN").Visible = false

            'grvCARI.Columns("TANGGAL").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            'grvCARI.Columns("TANGGAL").DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm:ss}"

            grdCARI.ShowPopup()

            'SplashScreenManager.CloseForm(False)
            'fn_LoadFormatDatan()
        Catch oErr As Exception
            'SplashScreenManager.CloseForm(False)
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub grdCARI_EditValueChanged(sender As Object, e As EventArgs) Handles grdCARI.EditValueChanged
        If grdCARI.Text = "" Then Exit Sub

        grvDetail.Focus()
        grvDetail.AddNewRow()
        grvDetail.SetFocusedRowCellValue(colDATECREATED, Now)
        grvDetail.SetFocusedRowCellValue(colDATEUPDATED, Now)
        grvDetail.SetFocusedRowCellValue(colKDKUNJUNGAN, grdCARI.EditValue)
        grvDetail.SetFocusedRowCellValue(colUSER, sUserID)
        grvDetail.UpdateCurrentRow()
    End Sub


#End Region
End Class