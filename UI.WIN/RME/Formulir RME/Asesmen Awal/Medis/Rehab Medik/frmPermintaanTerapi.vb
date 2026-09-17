Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient
Imports QRCoder
Imports System.Xml
Public Class frmPermintaanTerapi
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_RJ_PERMINTAANTERAPI As New Digital.clsS_DIGITAL_RJ_PERMINTAANTERAPI
    Private sKODEDOKTER As String
    Private sNAMADOKTER  As String
    Private sNoId As String
    Private sKDKUNJUNGAN As String = String.Empty
    Private sKoneksi As String = String.Empty


#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDKUNJUNGAN As String, Optional ByVal NoId As String = "<--- AUTO --->")
        oFormMode = FormMode
        sNoId = NoId
        sKDKUNJUNGAN = KDKUNJUNGAN

        Dim oPendaftaran As New Identitas.clsIdentitasPasien
        Dim dsPendaftaran = oPendaftaran.GetData(KDKUNJUNGAN)

        If dsPendaftaran IsNot Nothing Then
            txtKDFPTERAPI.Text = sNoId
            txtNAMAPASIEN.Text = dsPendaftaran.NAMAPASIEN
            txtKDPENDAFTARAN.Text = dsPendaftaran.KDPENDAFTARAN
            txtKDKUNJUNGAN.Text = dsPendaftaran.KDKUNJUNGAN
            txtKDCUSTOMER.Text = dsPendaftaran.KDCUSTOMER
            deTANGGAL.DateTime = dsPendaftaran.DATE
            sKODEDOKTER = dsPendaftaran.KDDOKTER
            sNAMADOKTER = dsPendaftaran.DOKTER
            deTANGGALTERAPI.DateTime = Now
            txtPTDIAGNOSA.Text = "1. Diagnosa  : " & dsPendaftaran.DIAGNOSA
            txtPTUJIFUNGSI.Text = "2. Uji Fungsi : "
            txtPTFT.Text = "3. FT : "
            txtPTEDUKASI.Text = "4. Edukasi : "
            txtPTEVALUASI.Text = "5. Evaluasi : "
            txtPROGRAM.ResetText()
            grdDOKTER.EditValue = dsPendaftaran.KDDOKTER
            grdTERAPIS.ResetText()
            
            'fn_LoadDataKontrol(dsPendaftaran.KDPENDAFTARAN)
            fn_LoadTindakan(dsPendaftaran.KDKUNJUNGAN)
        Else
            fn_EmptyMe()
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = EMedrekRJ_LembarKontrol.TITLE
        Dim oSetKoneksi As New Setting.clsSetKoneksi
        Dim dsSetKoneksi = oSetKoneksi.GetData()
        If dsSetKoneksi IsNot Nothing Then
            sKoneksi = dsSetKoneksi.KONEKSI
        End If
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
        sCode = txtKDPENDAFTARAN.Text.Trim.ToUpper
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_ChangeFormState()
        fn_Doctor()
        fn_DIAGNOSA()
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
        
        deTANGGAL.Properties.ReadOnly = Status
        txtPTDIAGNOSA.Properties.ReadOnly = Status
        grdDIAGNOSA.Properties.ReadOnly = Status
        txtPROGRAM.Properties.ReadOnly = Status
        deTANGGALTERAPI.Properties.ReadOnly = Status
        grdDOKTER.Properties.ReadOnly = Status
        grdTERAPIS.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        'txtKDFPTERAPI.Text = "<--- AUTO --->"
        'txtNAMAPASIEN.ResetText()
        'txtKDPENDAFTARAN.ResetText()
        'txtKDKUNJUNGAN.ResetText()
        'txtKDCUSTOMER.ResetText()
        'deTANGGAL.DateTime = Now
        sKODEDOKTER = String.Empty
        sNAMADOKTER = String.Empty
        deTANGGALTERAPI.DateTime = Now
        'txtPTDIAGNOSA.Text = "1. Diagnosa  : "
        'txtPTUJIFUNGSI.Text = "2. Uji Fungsi : "
        'txtPTFT.Text = "3. FT : "
        'txtPTEDUKASI.Text = "4. Edukasi : "
        'txtPTEVALUASI.Text = "5. Evaluasi : "
        'txtPROGRAM.ResetText()
        grdDOKTER.ResetText()
        grdTERAPIS.ResetText()

        deTANGGAL.Focus()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RJ_PERMINTAANTERAPI.GetData(txtKDFPTERAPI.Text)

            With ds
                txtKDFPTERAPI.Text = .KDFPTERAPI
                txtKDCUSTOMER.Text = .KDCUSTOMER
                txtKDKUNJUNGAN.Text = .KDKUNJUNGAN
                txtKDPENDAFTARAN.Text = .KDPENDAFTARAN
                deTANGGAL.DateTime = .DATE
                txtPTDIAGNOSA.Text = .PTDIAGNOSA
                txtPTUJIFUNGSI.Text = .PTUJIFUNGSI
                txtPTFT.Text = .PTFT
                txtPTEDUKASI.Text = .PTEDUKASI
                txtPTEVALUASI.Text = .PTEVALUASI

                BindingSource1.DataSource = oS_DIGITAL_RJ_PERMINTAANTERAPI.GetDataDetail(txtKDFPTERAPI.Text)
                grdTerapi.DataSource = BindingSource1
            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If grvTerapi.RowCount = 0 Then
                MsgBox("Data Terapi kosong", MsgBoxStyle.Exclamation, Me.Text)
                txtPROGRAM.Focus()
                fn_Validate = False
                Exit Function
            End If
        Catch oErr As Exception
            MsgBox("Validate Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RJ_PERMINTAANTERAPI.GetStructureHeader
            With ds
                .KDFPTERAPI = sNoId
                .KDKUNJUNGAN = txtKDKUNJUNGAN.Text
                .KDCUSTOMER = txtKDCUSTOMER.Text
                .KDPENDAFTARAN = txtKDPENDAFTARAN.Text
                Try
                    .DATECREATED = oS_DIGITAL_RJ_PERMINTAANTERAPI.GetData(txtKDKUNJUNGAN.Text).DATECREATED
                    .DATE = oS_DIGITAL_RJ_PERMINTAANTERAPI.GetData(txtKDKUNJUNGAN.Text).DATE
                Catch ex As Exception
                    .DATECREATED = Now
                    .DATE = deTANGGAL.DateTime
                End Try
                .DATEUPDATED = Now
                .PTDIAGNOSA = txtPTDIAGNOSA.Text
                .PTUJIFUNGSI = txtPTUJIFUNGSI.Text
                .PTFT = txtPTFT.Text
                .PTEDUKASI = txtPTEDUKASI.Text
                .PTEVALUASI = txtPTEVALUASI.Text
                .KDUSER = sUserID
            End With

            ' ***** DETIL *****
            Dim arrDetail = oS_DIGITAL_RJ_PERMINTAANTERAPI.GetStructureDetailList 
            For i As Integer = 0 To grvTerapi.RowCount - 2
                Dim dsDetail = oS_DIGITAL_RJ_PERMINTAANTERAPI.GetStructureDetail
                With dsDetail
                    .KDFPTERAPI = ds.KDFPTERAPI
                    .KDKUNJUNGAN = ds.KDKUNJUNGAN
                    .KDPENDAFTARAN = ds.KDPENDAFTARAN
                    .DATE = CDate(grvTerapi.GetRowCellValue(i, colDATE))
                    .SEQ = i
                    .PROGRAM = grvTerapi.GetRowCellValue(i, colPROGRAM)
                    .NMPASIEN = grvTerapi.GetRowCellValue(i, colPASIEN)
                    .PASIEN_SIGNATURE = grvTerapi.GetRowCellValue(i, colPASIEN_SIGNATURE)
                    .KDDOCTOR = grvTerapi.GetRowCellValue(i, colKDDOCTOR)
                    .NMDOCTOR = grvTerapi.GetRowCellValue(i, colDOCTOR)
                    .KDDOCTOR_SIGNATURE = grvTerapi.GetRowCellValue(i, colKDDOCTOR_SIGNATURE)
                    .KDSTAFF = grvTerapi.GetRowCellValue(i, colKDSTAFF)
                    .NMSTAFF = grvTerapi.GetRowCellValue(i, colTERAPIS)
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_RJ_PERMINTAANTERAPI.InsertData(ds,arrDetail)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_RJ_PERMINTAANTERAPI.UpdateData(ds.KDFPTERAPI,ds,arrDetail)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
#End Region
#Region "Command Button"
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClose_Click()
            Case Keys.F5
                If btnReload.Enabled = True Then
                    'btnReload_Click()
                End If
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
        End Select
    End Sub
    Private Sub btnReload_Click() Handles btnReload.ItemClick
        'Dim dsKunjungan = oS_DIGITAL_RJ_PERMINTAANTERAPI.GetDataByKunjungan(txtNoRegister.Text)

        
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & txtKDKUNJUNGAN.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtKDKUNJUNGAN.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_Doctor()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = "Data Source=172.165.115.150;Initial Catalog=DUSTIRA_FARMASI;Persist Security Info=True;User ID=sa;Password=dust1r@@"
            oConn = New SqlConnection(sConn)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "M_STAFF A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "
            SQL &= "AND KELOMPOKIPK = 'NAKES'"
            SQL &= "ORDER BY NAME_DISPLAY ASC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "DOKTER")

            grdDOKTER.Properties.DataSource = ds.Tables("DOKTER")
            grdDOKTER.Properties.ValueMember = "KDSTAFF"
            grdDOKTER.Properties.DisplayMember = "NAME_DISPLAY"

            grdTERAPIS.Properties.DataSource = ds.Tables("DOKTER")
            grdTERAPIS.Properties.ValueMember = "KDSTAFF"
            grdTERAPIS.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_DIAGNOSA()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = sKoneksi
            oConn = New SqlConnection(sConn)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "M_DIAGNOSA A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "DIAGNOSA")

            grdDIAGNOSA.Properties.DataSource = ds.Tables("DIAGNOSA")
            grdDIAGNOSA.Properties.ValueMember = "KDDIAGNOSA"
            grdDIAGNOSA.Properties.DisplayMember = "MEMO"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub grdDIAGNOSA_EditValueChanged(sender As Object, e As EventArgs) 
        txtPROGRAM.Text = grdDIAGNOSA.Text
    End Sub

    Private Sub fn_LoadDataKontrol(ByVal Parameter As String)
        Try
            grvTerapi.Columns.Clear()
            grdTerapi.DataSource = Nothing

            If Parameter = String.Empty Then Exit Sub

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
            SQL &= "[NO KUNJUNGAN] = A.KDKUNJUNGAN "
            SQL &= ",PROGRAM = A.PROGRAM "
            SQL &= ",TANGGAL = A.DATECONTROL "
            SQL &= ",PASIEN = B.NAMAPASIEN "
            SQL &= ",DOKTER = A.KDDOCTOR "
            SQL &= ",TERAPIS = A.KDTERAPIS "
            SQL &= ",[USER] = A.KDUSER "
            SQL &= ",DIAGNOSA = A.PTDIAGNOSA "
            SQL &= ",UJIFUNGSI = A.PTUJIFUNGSI "
            SQL &= ",FT = A.PTFT "
            SQL &= ",EDUKASI = A.PTEDUKASI "
            SQL &= ",EVALUASI = A.PTEVALUASI "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RJ_KONTROLIRM A "
            SQL &= "INNER JOIN R_IDENTITAS_PASIEN B "
            SQL &= "ON A.KDKUNJUNGAN = B.KDKUNJUNGAN "
            SQL &= "WHERE B.KDCUSTOMER = '" & Parameter & "' "
            SQL &= "ORDER BY A.DATE ASC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "RESUME")

            grdTerapi.MainView = grvTerapi
            grdTerapi.DataSource = ds.Tables("RESUME")
            grdTerapi.ForceInitialize()

            fn_LoadFormatDataLembarKontrol()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatDataLembarKontrol()
        For iLoop As Integer = 0 To grvTerapi.Columns.Count - 1
            If grvTerapi.Columns(iLoop).ColumnType.Name = "Decimal" Then
               grvTerapi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
               grvTerapi.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
               grvTerapi.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvTerapi.Columns(iLoop).ColumnType.Name = "DateTime" Then
               grvTerapi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
               grvTerapi.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm:ss}"
            End If
        Next
        grvTerapi.BestFitColumns()
        grvTerapi.Columns("DIAGNOSA").Visible = False
        grvTerapi.Columns("UJIFUNGSI").Visible = False
        grvTerapi.Columns("FT").Visible = False
        grvTerapi.Columns("EDUKASI").Visible = False
        grvTerapi.Columns("EVALUASI").Visible = False
    End Sub

    Private Sub fn_LoadTindakan(ByVal Parameter As String)
        Try
            If Parameter = String.Empty Then Exit Sub

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

            SQL = "SELECT B.* FROM "
            SQL &= "I_ORDERTINDAKAN_H A  "
            SQL &= "INNER JOIN I_ORDERTINDAKAN_D B ON A.KDORDERTINDAKAN = B.KDORDERTINDAKAN "
            SQL &= "WHERE "
            SQL &= "A.KDKUNJUNGAN = '"& Parameter &"' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "IORDERTINDAKAN")
            
            Dim strPROGRAM As String = String.Empty
            For iLoop As Integer = 0 To ds.Tables("IORDERTINDAKAN").Rows.Count - 1
                With ds.Tables("IORDERTINDAKAN")
                    strPROGRAM = strPROGRAM & .Rows(iLoop)("TARIFKT") & " " & vbCrLf
                End With
            Next

            txtPROGRAM.Text = strPROGRAM

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub EditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem.Click
        If String.IsNullOrEmpty(grvTerapi.GetFocusedRowCellValue("KDFPTERAPI")) = False Then
            txtPROGRAM.Text = grvTerapi.GetFocusedRowCellValue("PROGRAM")
            deTANGGALTERAPI.EditValue = grvTerapi.GetFocusedRowCellValue("DATE")
            grdDOKTER.EditValue = grvTerapi.GetFocusedRowCellValue("KDDOCTOR")
            grdTERAPIS.EditValue = grvTerapi.GetFocusedRowCellValue("KDSTAFF")
            grvTerapi.DeleteSelectedRows()
        End If
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        grvTerapi.DeleteSelectedRows()
    End Sub

    Private Sub btnTambahDataTerapi_Click(sender As Object, e As EventArgs) Handles btnTambahDataTerapi.Click
        Try
            If String.IsNullOrEmpty(txtPROGRAM.Text) Then
                Exit Sub
            End If
            grvTerapi.Focus()
            grvTerapi.AddNewRow()

            grvTerapi.SetFocusedRowCellValue(colKDFPTERAPI, sNoId)
            grvTerapi.SetFocusedRowCellValue(colKDKUNJUNGAN, txtKDKUNJUNGAN.Text)
            grvTerapi.SetFocusedRowCellValue(colKDPENDAFTARAN, txtKDPENDAFTARAN.Text)
            grvTerapi.SetFocusedRowCellValue(colDATE, deTANGGALTERAPI.DateTime)
            grvTerapi.SetFocusedRowCellValue(colSEQ, grvTerapi.RowCount)
            grvTerapi.SetFocusedRowCellValue(colPROGRAM, txtPROGRAM.Text)
            grvTerapi.SetFocusedRowCellValue(colPASIEN, txtNAMAPASIEN.Text)
            grvTerapi.SetFocusedRowCellValue(colPASIEN_SIGNATURE, String.Empty)
            grvTerapi.SetFocusedRowCellValue(colKDDOCTOR, grdDOKTER.EditValue)
            grvTerapi.SetFocusedRowCellValue(colKDDOCTOR_SIGNATURE, String.Empty)
            grvTerapi.SetFocusedRowCellValue(colDOCTOR, grdDOKTER.Text)
            grvTerapi.SetFocusedRowCellValue(colKDSTAFF, grdTERAPIS.EditValue)
            grvTerapi.SetFocusedRowCellValue(colTERAPIS, grdTERAPIS.Text)
            grvTerapi.UpdateCurrentRow()
            grvTerapi.Focus()
            'MsgBox("Data tersimpan untuk Obat : " + grvDPT.GetFocusedRowCellValue(colITEMOBAT.FieldName), MsgBoxStyle.Information, Me.Text)
        Catch ex As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try

        fn_EmptyMe()
    End Sub
#End Region
End Class