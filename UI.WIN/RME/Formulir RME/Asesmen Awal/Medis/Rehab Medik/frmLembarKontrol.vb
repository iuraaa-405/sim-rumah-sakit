Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient
Imports QRCoder
Imports System.Xml
Public Class frmLembarKontrol
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_KONTROLIRM As New Digital.clsDigital_RJ_KONTROLIRM
    Private down As Boolean = False
    Private sKODEDOKTER As String
    Private sNAMADOKTER  As String
    Private sLANJUTNOREG  As String
    Private sKoneksi As String = String.Empty


#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String)
        oFormMode = FormMode

        Dim oPendaftaran As New Identitas.clsIdentitasPasien
        Dim dsPendaftaran = oPendaftaran.GetData(KDREG)

        txtNoRegister.Text = KDREG

        If dsPendaftaran IsNot Nothing Then
            txtNamaPasien.Text = dsPendaftaran.NAMAPASIEN
            txtNoRegister.Text = dsPendaftaran.KDPENDAFTARAN
            txtKDKUNJUNGAN.Text = dsPendaftaran.KDKUNJUNGAN
            txtNoRM.Text = dsPendaftaran.KDCUSTOMER
            deTANGGAL.DateTime = dsPendaftaran.DATE
            sKODEDOKTER = dsPendaftaran.KDDOKTER
            sNAMADOKTER = dsPendaftaran.DOKTER
            deDATECONTROL.DateTime = dsPendaftaran.DATE
            txtPTDIAGNOSA.Text = "1. Diagnosa  : "
            txtPTUJIFUNGSI.Text = "2. Uji Fungsi : "
            txtPTFT.Text = "3. FT : "
            txtPTEDUKASI.Text = "4. Edukasi : "
            txtPTEVALUASI.Text = "5. Evaluasi : "
            sLANJUTNOREG = dsPendaftaran.KDKUNJUNGAN
            Try
                Dim gen As New QRCodeGenerator
                Dim data = gen.CreateQrCode(sTandaTanganPasien, QRCodeGenerator.ECCLevel.Q)
                Dim code As New QRCode(data)
                picTTDPASIEN.Image = code.GetGraphic(6)
            Catch ex As Exception
                picTTDPASIEN.Visible = False
            End Try

            fn_LoadDataKontrol(dsPendaftaran.KDCUSTOMER)
            fn_LoadTindakan(dsPendaftaran.KDKUNJUNGAN)
        Else
            txtNamaPasien.ResetText()
            txtNoRegister.ResetText()
            txtNoRM.ResetText()
            deTANGGAL.ResetText()
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
        sCode = txtNoRegister.Text.Trim.ToUpper
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
        deDATECONTROL.Properties.ReadOnly = Status
        grdDOKTER.Properties.ReadOnly = Status
        grdTERAPIS.Properties.ReadOnly = Status
        
    End Sub
    Private Sub fn_EmptyMe()
        'txtCODE.Text = "<--- AUTO --->"
        deTANGGAL.Text = now
        txtPTDIAGNOSA.Text = "1. Diagnosa  : "
        txtPTUJIFUNGSI.Text = "2. Uji Fungsi : "
        txtPTFT.Text = "3. FT : "
        txtPTEDUKASI.Text = "4. Edukasi : "
        txtPTEVALUASI.Text = "5. Evaluasi : "
        txtPROGRAM.ResetText()
        deDATECONTROL.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_KONTROLIRM.GetData(txtKDKUNJUNGAN.Text)

            With ds
                deTANGGAL.DateTime = .DATE
                txtPTDIAGNOSA.Text = .PTDIAGNOSA
                txtPTUJIFUNGSI.Text = .PTUJIFUNGSI
                txtPTFT.Text = .PTFT
                txtPTEDUKASI.Text = .PTEDUKASI
                txtPTEVALUASI.Text = .PTEVALUASI
                grdDIAGNOSA.EditValue = .KDDIAGNOSA
                txtPROGRAM.Text = .PROGRAM
                deDATECONTROL.DateTime = .DATECONTROL
                grdDOKTER.EditValue = .KDDOCTOR
                grdTERAPIS.EditValue = .KDTERAPIS

            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtKDKUNJUNGAN.Text = String.Empty Then
                MsgBox("No Kunjungan Kosong", MsgBoxStyle.Exclamation, Me.Text)
                txtKDKUNJUNGAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdDIAGNOSA.Text = String.Empty Then
                MsgBox("Dibutuhkan Program", MsgBoxStyle.Exclamation, Me.Text)
                grdDIAGNOSA.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdDOKTER.Text = String.Empty Then
                MsgBox("Dibutuhkan Dokter", MsgBoxStyle.Exclamation, Me.Text)
                grdDOKTER.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdTERAPIS.Text = String.Empty Then
                MsgBox("Dibutuhkan Terapis", MsgBoxStyle.Exclamation, Me.Text)
                grdTERAPIS.Focus()
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
            Dim ds = oS_DIGITAL_KONTROLIRM.GetStructureHeader
            With ds
                .KDKUNJUNGAN = txtKDKUNJUNGAN.Text
                .KDCUSTOMER = txtNoRM.Text
                Try
                    .DATECREATED = oS_DIGITAL_KONTROLIRM.GetData(txtKDKUNJUNGAN.Text).DATECREATED
                    .DATE = oS_DIGITAL_KONTROLIRM.GetData(txtKDKUNJUNGAN.Text).DATE
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
                .KDDIAGNOSA = grdDIAGNOSA.EditValue
                .PROGRAM = txtPROGRAM.Text
                .DATECONTROL = deDATECONTROL.DateTime
                .KDDOCTOR = grdDOKTER.EditValue
                .DOKTER_SIGNATURE = grdDOKTER.Text
                .KDTERAPIS = grdTERAPIS.EditValue
                .TERAPIS_SIGNATURE = grdTERAPIS.Text

                Try
                    .COUNTKONTROL = oS_DIGITAL_KONTROLIRM.GetData(txtKDKUNJUNGAN.Text).COUNTKONTROL
                Catch ex As Exception
                    .COUNTKONTROL = 1
                End Try

                .REGISTERPERTAMAKONTROL = sLANJUTNOREG

                Try
                    .CETAK = oS_DIGITAL_KONTROLIRM.GetData(txtKDKUNJUNGAN.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                .KDUSER = sUserID
                .KDUSER_SIGNATURE = sUserSIGNATURE
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_KONTROLIRM.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_KONTROLIRM.UpdateData(ds)
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
        'Dim dsKunjungan = oS_DIGITAL_KONTROLIRM.GetDataByKunjungan(txtNoRegister.Text)

        
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save Lembar Kontrol " & txtNamaPasien.Text.Trim.ToUpper & vbCrLf & "Dengan User : " & sUserID & " ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
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

    Private Sub grdDIAGNOSA_EditValueChanged(sender As Object, e As EventArgs) Handles grdDIAGNOSA.EditValueChanged
        txtPROGRAM.Text = grdDIAGNOSA.Text
    End Sub

    Private Sub fn_LoadDataKontrol(ByVal Parameter As String)
        Try
            grvKontrol.Columns.Clear()
            grdKontrol.DataSource = Nothing

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

            grdKontrol.MainView = grvKontrol
            grdKontrol.DataSource = ds.Tables("RESUME")
            grdKontrol.ForceInitialize()

            fn_LoadFormatDataLembarKontrol()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatDataLembarKontrol()
        For iLoop As Integer = 0 To grvKontrol.Columns.Count - 1
            If grvKontrol.Columns(iLoop).ColumnType.Name = "Decimal" Then
               grvKontrol.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
               grvKontrol.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
               grvKontrol.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvKontrol.Columns(iLoop).ColumnType.Name = "DateTime" Then
               grvKontrol.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
               grvKontrol.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm:ss}"
            End If
        Next
        grvKontrol.BestFitColumns()
        grvKontrol.Columns("DIAGNOSA").Visible = False
        grvKontrol.Columns("UJIFUNGSI").Visible = False
        grvKontrol.Columns("FT").Visible = False
        grvKontrol.Columns("EDUKASI").Visible = False
        grvKontrol.Columns("EVALUASI").Visible = False
    End Sub

    Private Sub fn_LoadTindakan(ByVal Parameter As String)
        Try
            txtPROGRAM.ResetText()

            Dim oOrderTindakan As New Inventory.clsOrderTindakan
            Dim dsOrderTindakan = oOrderTindakan.GetDataOrderKunjungan(txtKDKUNJUNGAN.Text)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grvKontrol_DoubleClick(sender As Object, e As EventArgs) Handles grvKontrol.DoubleClick
         If grvKontrol.GetFocusedRowCellValue("NO KUNJUNGAN") Is Nothing Then
            Exit Sub
         Else
            txtPTDIAGNOSA.Text = grvKontrol.GetFocusedRowCellValue("DIAGNOSA")
            txtPTUJIFUNGSI.Text = grvKontrol.GetFocusedRowCellValue("UJIFUNGSI")
            txtPTFT.Text = grvKontrol.GetFocusedRowCellValue("FT")
            txtPTEDUKASI.Text = grvKontrol.GetFocusedRowCellValue("EDUKASI")
            txtPTEVALUASI.Text = grvKontrol.GetFocusedRowCellValue("EVALUASI")
            sLANJUTNOREG = grvKontrol.GetFocusedRowCellValue("NO KUNJUNGAN")

            MsgBox("Terapi dilanjutkan.", MsgBoxStyle.Information, Me.Text)
         End If
    End Sub

    Private Sub chkISFORMBARU_CheckedChanged(sender As Object, e As EventArgs) Handles chkISFORMBARU.CheckedChanged
        If chkISFORMBARU.Checked = True Then
            grvKontrol.Columns.Clear()
            grdKontrol.DataSource = Nothing
            fn_EmptyMe()
            sLANJUTNOREG = txtKDKUNJUNGAN.Text
            LabelControl1.Visible = False
            LayoutControlItem13.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            fn_LoadDataKontrol(txtNoRM.Text)
            LabelControl1.Visible = True
            LayoutControlItem13.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub
#End Region
End Class