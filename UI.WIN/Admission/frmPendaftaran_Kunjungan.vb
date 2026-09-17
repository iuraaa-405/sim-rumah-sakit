Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmPendaftaran_Kunjungan
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oPendaftaran_Kunjungan As New Admission.clsPendaftaran_Kunjungan
    Private oKelasAplicareBed As New Reference.clsKelasAplicareBed
    Private sPopUP As Boolean = False
    Private sKDPENDAFTARAN As String = String.Empty

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDPENDAFTARAN As String, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId
        sKDPENDAFTARAN = KDPENDAFTARAN
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = Pendaftaran_Kunjungan.TITLE

            lNAMAPASIEN.Text = Customer.TITLE
            lKDDEPARTMENT_H.Text = Department.TITLE
            lKDDEPARTMENT.Text = Department.TITLE
            lKDDOCTOR_H.Text = Doctor.TITLE & " DPJP"
            'lKDDOCTOR.Text = Doctor.TITLE

            lKDPENDAFTARAN.Text = Pendaftaran_Kunjungan.KDPENDAFTARAN
            lDATE.Text = Pendaftaran_Kunjungan.TANGGAL

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDKUNJUNGAN.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDDOCTOR()
        fn_LoadPemetaan()

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
        grdKDPENDAFTARAN.Properties.ReadOnly = Status
        grdKDDEPARTMENT.Properties.ReadOnly = Status
        'grdKDDOCTOR.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDKUNJUNGAN.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        grdKDPENDAFTARAN.ResetText()
        'grdKDDOCTOR.ResetText()
        grdKDDEPARTMENT.ResetText()

        If sKDPENDAFTARAN <> String.Empty Then
            fn_LoadKDKUNJUNGAN(sKDPENDAFTARAN, 3)
            grdKDPENDAFTARAN.Text = sKDPENDAFTARAN

            Dim dsKunjungan = oPendaftaran_Kunjungan.GetDatabykd(grdKDPENDAFTARAN.EditValue)
            If dsKunjungan IsNot Nothing Then
                txtNAMAPASIEN.Text = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY & " / " & dsKunjungan.S_PENDAFTARAN_H.KDCUSTOMER
                txtKDJENISKELAMIN.Text = IIf(dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.KDJENISKELAMIN = 0, "P", "L")
                grdKDDEPARTMENT_H.Text = dsKunjungan.S_PENDAFTARAN_H.KDDEPARTMENT
                grdKDDOCTOR_H.Text = dsKunjungan.S_PENDAFTARAN_H.KDDOCTOR
                txtKDPENDAFTARAN_AWAL.Text = dsKunjungan.S_PENDAFTARAN_H.KDPENDAFTARAN_AWAL

                fn_LoadKDDEPARTMENT(dsKunjungan.S_PENDAFTARAN_H.CATEGORY)

                If dsKunjungan.S_PENDAFTARAN_H.CATEGORY = 0 Then
                    lPEMETAAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Else
                    lPEMETAAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                End If
            Else
                txtNAMAPASIEN.ResetText()
                txtKDJENISKELAMIN.ResetText()
                grdKDDEPARTMENT_H.ResetText()
                grdKDDOCTOR_H.ResetText()
                txtKDPENDAFTARAN_AWAL.ResetText()
            End If
        Else
            txtNAMAPASIEN.ResetText()
            txtKDJENISKELAMIN.ResetText()
            grdKDDEPARTMENT_H.ResetText()
            grdKDDOCTOR_H.ResetText()
            txtKDPENDAFTARAN_AWAL.ResetText()
        End If
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oPendaftaran_Kunjungan.GetData(sNoId)

            With ds
                txtKDKUNJUNGAN.Text = .KDKUNJUNGAN

                fn_LoadKDKUNJUNGAN(.KDPENDAFTARAN, 3)
                grdKDPENDAFTARAN.Text = .KDPENDAFTARAN

                Dim dsKunjungan = oPendaftaran_Kunjungan.GetDatabykd(grdKDPENDAFTARAN.EditValue)
                If dsKunjungan IsNot Nothing Then
                    txtNAMAPASIEN.Text = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY & " / " & dsKunjungan.S_PENDAFTARAN_H.KDCUSTOMER
                    txtKDJENISKELAMIN.Text = IIf(dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.KDJENISKELAMIN = 0, "P", "L")
                    grdKDDEPARTMENT_H.Text = dsKunjungan.S_PENDAFTARAN_H.KDDEPARTMENT
                    grdKDDOCTOR_H.Text = dsKunjungan.S_PENDAFTARAN_H.KDDOCTOR
                    txtKDPENDAFTARAN_AWAL.Text = dsKunjungan.S_PENDAFTARAN_H.KDPENDAFTARAN_AWAL

                    fn_LoadKDDEPARTMENT(dsKunjungan.S_PENDAFTARAN_H.CATEGORY)

                    If dsKunjungan.S_PENDAFTARAN_H.CATEGORY = 0 Then
                        lPEMETAAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    Else
                        lPEMETAAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    End If
                Else
                    txtNAMAPASIEN.ResetText()
                    txtKDJENISKELAMIN.ResetText()
                    grdKDDEPARTMENT_H.ResetText()
                    grdKDDOCTOR_H.ResetText()
                    txtKDPENDAFTARAN_AWAL.ResetText()
                End If

                grdPEMETAAN.Text = .KDUPDATE_APLICARE
                grdKDDEPARTMENT.Text = .KDDEPARTMENT
                'grdKDDOCTOR.Text = .KDDOCTOR

                deDATE.DateTime = .DATE

                Dim dsBed = oKelasAplicareBed.GetDataByKdPendaftaran(.KDKUNJUNGAN)
                If dsBed IsNot Nothing Then
                    lblBed.Text = dsBed.BED & ", " & dsBed.M_KELASAPLICARE.MEMO & ", " & dsBed.M_DEPARTMENT.NAME_DISPLAY
                End If

            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If grdKDPENDAFTARAN.Text = String.Empty Then
                grdKDPENDAFTARAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDPENDAFTARAN.ErrorText = Statement.ErrorRequired

                grdKDPENDAFTARAN.Focus()
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

            If grdKDBED.Text <> "" Then
                If grdKDPENDAFTARAN.Text.Contains("RJ") Then
                    MsgBox("Pilih Kunjungan Rawat Inap", MsgBoxStyle.Exclamation, Me.Text)
                    grdKDPENDAFTARAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    grdKDPENDAFTARAN.ErrorText = Statement.ErrorRequired

                    grdKDPENDAFTARAN.Focus()
                    fn_Validate = False
                    Exit Function
                End If
            End If

            'If grdKDDOCTOR.Text = String.Empty Then
            '    grdKDDOCTOR.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '    grdKDDOCTOR.ErrorText = Statement.ErrorRequired

            '    grdKDDOCTOR.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If

            'If grdKDDEPARTMENT.EditValue = grdKDDEPARTMENT_H.EditValue Then
            '    grdKDDEPARTMENT.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '    grdKDDEPARTMENT.ErrorText = Statement.ErrorRequired

            '    grdKDDEPARTMENT.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If

            'If grdPEMETAAN.Text = String.Empty Then Exit Function
            Dim oKelasAplicares As New Reference.clsKelasAplicare

            'Dim dsKelasAplicare = oKelasAplicares.GetDataDetail_UOM(grdPEMETAAN.EditValue)
            'If dsKelasAplicare IsNot Nothing Then
            '    If dsKelasAplicare.TERSEDIA_LAKIPEREMPUAN >= 0 Then
            '        If dsKelasAplicare.TERSEDIA_LAKIPEREMPUAN = 0 Then
            '            MsgBox(Statement.ErrorStatement & " Ruangan Penuh Untuk Pasien Laki dan Perempuan", MsgBoxStyle.Information, Me.Text)

            '            grdPEMETAAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '            grdPEMETAAN.ErrorText = Statement.ErrorRequired

            '            grdPEMETAAN.Focus()
            '            fn_Validate = False
            '            Exit Function

            '        End If
            '    Else
            '        If txtKDJENISKELAMIN.Text = "P" Then
            '            If dsKelasAplicare.TERSEDIA_PEREMPUAN <= 0 Then
            '                MsgBox(Statement.ErrorStatement & " Ruangan Penuh Untuk Pasien Perempuan", MsgBoxStyle.Information, Me.Text)

            '                grdPEMETAAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '                grdPEMETAAN.ErrorText = Statement.ErrorRequired

            '                grdPEMETAAN.Focus()
            '                fn_Validate = False
            '                Exit Function

            '            End If

            '        Else
            '            If dsKelasAplicare.TERSEDIA_LAKI <= 0 Then
            '                MsgBox(Statement.ErrorStatement & " Ruangan Penuh Untuk Pasien Laki-laki", MsgBoxStyle.Information, Me.Text)

            '                grdPEMETAAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '                grdPEMETAAN.ErrorText = Statement.ErrorRequired

            '                grdPEMETAAN.Focus()
            '                fn_Validate = False
            '                Exit Function

            '            End If
            '        End If
            '    End If
            'End If

            Dim dsDepartmentLast = oKelasAplicares.GetDataDetail_UOM(grdPEMETAAN.EditValue)
            If dsDepartmentLast IsNot Nothing Then

                If grdKDDEPARTMENT.EditValue <> dsDepartmentLast.KDDEPARTMENT Then
                    MsgBox(Statement.ErrorStatement & " Pemetaan Tidak sama dengan ruangan yg dipilih", MsgBoxStyle.Information, Me.Text)

                    grdPEMETAAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    grdPEMETAAN.ErrorText = Statement.ErrorRequired

                    grdPEMETAAN.Focus()
                    fn_Validate = False
                    Exit Function
                End If
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oPendaftaran_Kunjungan.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oPendaftaran_Kunjungan.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDPENDAFTARAN = grdKDPENDAFTARAN.EditValue
                .KDKUNJUNGAN = sNoId
                .DATE = deDATE.DateTime
                .KDDOCTOR = grdKDDOCTOR_H.EditValue
                .KDDEPARTMENT = grdKDDEPARTMENT.EditValue

                Dim dsDaftar = oPendaftaran_Kunjungan.GetDatabykd(grdKDPENDAFTARAN.EditValue)
                .ALAMAT = dsDaftar.ALAMAT
                .KDPENJAMIN = dsDaftar.KDPENJAMIN
                .KDKESATUAN = dsDaftar.KDKESATUAN
                .KDPANGKAT = dsDaftar.KDPANGKAT
                .KDGOLONGAN = dsDaftar.KDGOLONGAN
                .KDPENDIDIKAN = dsDaftar.KDPENDIDIKAN
                .KDPEKERJAAN = dsDaftar.KDPEKERJAAN
                .KDPERUSAHAAN = dsDaftar.KDPERUSAHAAN
                .KDSTATUSKAWIN = dsDaftar.KDSTATUSKAWIN
                .NAMAKELUARGA = dsDaftar.NAMAKELUARGA
                .KDSTATUSKELUARGA = dsDaftar.KDSTATUSKELUARGA
                .TERSEDIA = 0
                .KDUPDATE_APLICARE = IIf(grdPEMETAAN.Text = "", "", grdPEMETAAN.EditValue)
                .KDUSER = sUserID
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oPendaftaran_Kunjungan.InsertData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try

            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oPendaftaran_Kunjungan.UpdateData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

            If fn_Save = True Then
                Try
                    Dim oPendaftaran As New Admission.clsPendaftaran
                    Dim dsKunjungan = oPendaftaran.GetDataKunjungan(ds.KDKUNJUNGAN)

                    Dim dsIdentitas = oPendaftaran.GetStructureHeader_Identitas

                    With dsIdentitas
                        .DATECREATED = dsKunjungan.DATECREATED
                        .DATEUPDATED = dsKunjungan.DATEUPDATED
                        .DATE = dsKunjungan.DATE
                        .CATEGORY = dsKunjungan.S_PENDAFTARAN_H.CATEGORY
                        .KDKUNJUNGAN = dsKunjungan.KDKUNJUNGAN
                        .KDPENDAFTARAN = dsKunjungan.KDPENDAFTARAN
                        .PENJAMIN = dsKunjungan.S_PENDAFTARAN_H.M_DAFTAR_L1.MEMO
                        .KDCUSTOMER = dsKunjungan.S_PENDAFTARAN_H.KDCUSTOMER
                        .NAMAPASIEN = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY
                        .ALAMAT = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.ALAMAT
                        .DOKTER = grdKDDOCTOR_H.Text
                        .TUJUAN = grdKDDEPARTMENT.Text
                        .KDDOKTER = grdKDDOCTOR_H.EditValue
                        .KDTUJUAN = grdKDDEPARTMENT.EditValue
                        .TANGGALLAHIR = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR
                        .JENISKELAMIN = IIf(dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.KDJENISKELAMIN = 0, "P", "L")
                        .NIK = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.KTP
                        .TEMPATLAHIR = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.TEMPATLAHIR
                        .AGAMA = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.M_AGAMA.MEMO
                        .PANGKAT = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.M_PANGKAT.MEMO
                        .NRP = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.NRP
                        .KESATUAN = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.M_KESATUAN.MEMO
                        .NOMORTELEPON = dsKunjungan.S_PENDAFTARAN_H.NOMORTELEPON
                        .PENDIDIKAN = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.KDPENDIDIKAN
                        .SUKU = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.M_SUKU.MEMO
                        .USIA = oPendaftaran.GetUmurPasien(deDATE.DateTime, dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)
                        .NOMORSEP = dsKunjungan.S_PENDAFTARAN_H.NOMORSEP
                        .KELASPELAYANAN = dsKunjungan.S_PENDAFTARAN_H.KDKELASRAWAT
                        .KARTUBPJS = dsKunjungan.S_PENDAFTARAN_H.KARTUBPJS
                        .STATUS_KAWIN = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.KDSTATUSKAWIN
                        .HUBUNGAN = ""
                        .HUBUNGAN_NAMA = ""
                        .HUBUNGAN_PENDIDIKAN = ""
                        .HUBUNGAN_PEKERJAAN = ""
                        .NOMORASURANSILAIN = ""
                        .KDGOLONGANDARAH = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.KDGOLONGANDARAH
                        .KDDIAGNOSA = dsKunjungan.S_PENDAFTARAN_H.KDDIAGNOSA
                        .KDPENJAMIN = ""
                        .KDPERUSAHAAN = ""
                        .DIAGNOSA = dsKunjungan.S_PENDAFTARAN_H.M_DIAGNOSA.MEMO
                        .KDUSER = sUserID
                        .HAKKELAS = ""
                        .URL_SIGNATURE = ""
                        .NAMA_TANDATANGAN = ""
                        .FASKES = dsKunjungan.S_PENDAFTARAN_H.M_PPK.MEMO
                    End With

                    Dim dsIdentitasCek = oPendaftaran.GetDataIdentitas(ds.KDKUNJUNGAN)
                    If dsIdentitasCek Is Nothing Then
                        oPendaftaran.InsertDataR_identitas(dsIdentitas)
                    Else
                        oPendaftaran.UpdateDataR_Identitas(dsIdentitas)
                    End If
                Catch ex As Exception
                    MsgBox("R_Identitas Eror" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

            If fn_Save = True Then
                If grdKDBED.Text <> "" Then
                    For Each xloop In oKelasAplicareBed.GetDataDetailByKdPendaftaranKunjungan(ds.KDPENDAFTARAN)
                        For Each yloop In oKelasAplicareBed.GetDataDetailByKdPendaftaran(xloop.KDKUNJUNGAN)
                            oKelasAplicareBed.UpdateDataPemetaan(yloop.KODEBED, "", "KOSONG", yloop.MEMO)
                        Next
                    Next

                    oKelasAplicareBed.UpdateDataPemetaan(grdKDBED.EditValue, ds.KDKUNJUNGAN, "TERISI", "")
                End If
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
#End Region
#Region "Command Button"
    Private Sub frmPendaftaran_Kunjungan_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    Private Sub txtCARI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCARI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            sPopUP = True
            If txtCARI.Text = String.Empty Then Exit Sub
            fn_LoadKDKUNJUNGAN(IIf(cboCARI.SelectedIndex = 0, txtCARI.Text.ToString, txtCARI.Text.ToString.Trim), cboCARI.SelectedIndex)
            txtCARI.ResetText()
        End If
    End Sub
    Private Sub fn_LoadKDKUNJUNGAN(ByVal sParameter As String, ByVal sCari As Integer)
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
            SQL &= "A.KDPENDAFTARAN "
            SQL &= ",D.NAME_DISPLAY "
            SQL &= ",A.DATE "
            SQL &= ",TUJUAN = B.NAME_DISPLAY  "
            SQL &= ",DPJP = C.NAME_DISPLAY "
            SQL &= ",PULANG = ISNULL((SELECT CONVERT(BIT, 1) FROM T_UPDATE_TANGGAL_PULANG WHERE A.KDPENDAFTARAN = KDPENDAFTARAN AND CARAPULANG <> 5), ISNULL((SELECT CONVERT(BIT, 1) FROM T_UPDATE_TANGGAL_PULANG WHERE A.KDPENDAFTARAN_AWAL = KDPENDAFTARAN AND CARAPULANG <> 5), CONVERT(BIT, 0)))  "
            SQL &= ",RANAP = (SELECT CASE WHEN KDPENDAFTARAN_AWAL <> '' THEN CONVERT(BIT, 1) ELSE CONVERT(BIT, 0) END) "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_H A "
            SQL &= "INNER JOIN M_DEPARTMENT B "
            SQL &= "ON A.KDDEPARTMENT = B.KDDEPARTMENT "
            SQL &= "INNER JOIN M_DOCTOR C "
            SQL &= "ON A.KDDOCTOR = C.KDDOCTOR "
            SQL &= "INNER JOIN M_CUSTOMER D "
            SQL &= "ON A.KDCUSTOMER = D.KDCUSTOMER "
            If sCari = 0 Then
                SQL &= "WHERE A.KDCUSTOMER LIKE '%" & sParameter & "%' "
            ElseIf sCari = 1 Then
                SQL &= "WHERE D.NAME_DISPLAY LIKE '%" & sParameter & "%' "
            Else
                SQL &= "WHERE A.KDPENDAFTARAN LIKE '%" & sParameter & "%' "
            End If

            SQL &= "ORDER BY A.DATE DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

            grdKDPENDAFTARAN.Properties.DataSource = ds.Tables("ALL")
            grdKDPENDAFTARAN.Properties.ValueMember = "KDPENDAFTARAN"
            grdKDPENDAFTARAN.Properties.DisplayMember = "KDPENDAFTARAN"

            If sPopUP = True Then
                grdKDPENDAFTARAN.ShowPopup()
            End If

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDKUNJUNGAN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdKDPENDAFTARAN.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Dim oKunjungan As New Admission.clsPendaftaran
            Dim dsKunjungan = oKunjungan.GetData(grdKDPENDAFTARAN.EditValue)
            If dsKunjungan IsNot Nothing Then
                txtNAMAPASIEN.Text = dsKunjungan.M_CUSTOMER.NAME_DISPLAY & " / " & dsKunjungan.KDCUSTOMER
                txtKDJENISKELAMIN.Text = IIf(dsKunjungan.M_CUSTOMER.KDJENISKELAMIN = 0, "P", "L")
                grdKDDEPARTMENT_H.Text = dsKunjungan.KDDEPARTMENT
                grdKDDOCTOR_H.Text = dsKunjungan.KDDOCTOR
                txtKDPENDAFTARAN_AWAL.Text = dsKunjungan.KDPENDAFTARAN_AWAL

                Dim dsDaftar = oPendaftaran_Kunjungan.GetDatabykd(grdKDPENDAFTARAN.EditValue)

                If dsDaftar IsNot Nothing Then
                    fn_LoadKDDEPARTMENT(dsDaftar.S_PENDAFTARAN_H.CATEGORY)
                End If

                GridLookUpEdit1View.BestFitColumns()

                If dsKunjungan.CATEGORY = 0 Then
                    lPEMETAAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    lCARIBED.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    lBED.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    lNAMABED.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Else
                    lPEMETAAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    lCARIBED.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    lBED.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    lNAMABED.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                End If

            Else
                txtNAMAPASIEN.ResetText()
                txtKDJENISKELAMIN.ResetText()
                grdKDDEPARTMENT_H.ResetText()
                grdKDDOCTOR_H.ResetText()
                txtKDPENDAFTARAN_AWAL.ResetText()
            End If
        End If
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadPemetaan()
        Try
            Dim oKelasAplicare As New Reference.clsKelasAplicare
            Dim dsDepartment = From x In oKelasAplicare.GetDataDetail_UOM
                               Select x.KDUPDATE_APLICARE, KELAS = x.M_KELASAPLICARE.MEMO, RUANGAN = x.M_DEPARTMENT.NAME_DISPLAY, x.KAPASITAS, x.TERSEDIA, x.TERSEDIA_LAKI, x.TERSEDIA_PEREMPUAN

            grdPEMETAAN.Properties.DataSource = dsDepartment.ToList()
            grdPEMETAAN.Properties.ValueMember = "KDUPDATE_APLICARE"
            grdPEMETAAN.Properties.DisplayMember = "KDUPDATE_APLICARE"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDDOCTOR()
        Dim oDoctor As New Reference.clsDoctor
        Try
            Dim ds = oDoctor.GetData.Where(Function(x) x.ISACTIVE = True).ToList()

            grdKDDOCTOR_H.Properties.DataSource = ds
            grdKDDOCTOR_H.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR_H.Properties.DisplayMember = "NAME_DISPLAY"

            'grdKDDOCTOR.Properties.DataSource = ds
            'grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
            'grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDDEPARTMENT(ByVal kategeori As Integer)
        Dim oDepartment As New Reference.clsDepartment
        Try
            Dim ds = oDepartment.GetData.Where(Function(x) x.ISACTIVE = True And IIf(kategeori = 0, x.ISRUANGRAWAT = False, x.ISRUANGRAWAT = True)).ToList()

            grdKDDEPARTMENT_H.Properties.DataSource = ds
            grdKDDEPARTMENT_H.Properties.ValueMember = "KDDEPARTMENT"
            grdKDDEPARTMENT_H.Properties.DisplayMember = "NAME_DISPLAY"

            grdKDDEPARTMENT.Properties.DataSource = ds
            grdKDDEPARTMENT.Properties.ValueMember = "KDDEPARTMENT"
            grdKDDEPARTMENT.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
    Private Sub OnValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Calculate()
    End Sub
    Private Sub Calculate()
    End Sub
    Private Sub grdKDDEPARTMENT_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdKDDEPARTMENT.KeyPress
        If Asc(e.KeyChar) = 13 Then

            'grdKDDOCTOR.ShowPopup()

            If grdKDDEPARTMENT.Text = String.Empty Then Exit Sub

            Dim oKelasAplicare As New Reference.clsKelasAplicare
            Dim dsDepartment = From x In oKelasAplicare.GetDataDetail_Department(grdKDDEPARTMENT.EditValue)
                               Select x.KDUPDATE_APLICARE, KELAS = x.M_KELASAPLICARE.MEMO, RUANGAN = x.M_DEPARTMENT.NAME_DISPLAY, x.KAPASITAS, x.TERSEDIA, x.TERSEDIA_LAKI, x.TERSEDIA_PEREMPUAN

            grdPEMETAAN.Properties.DataSource = dsDepartment.ToList()
            grdPEMETAAN.Properties.ValueMember = "KDUPDATE_APLICARE"
            grdPEMETAAN.Properties.DisplayMember = "KDUPDATE_APLICARE"

        End If

    End Sub
    Private Sub grdPEMETAAN_EditValueChanged(sender As Object, e As EventArgs) Handles grdPEMETAAN.EditValueChanged
        If isLoad = True Then
            If grdPEMETAAN.Text = String.Empty Then Exit Sub
            Dim oKelasAplicares As New Reference.clsKelasAplicare

            Dim dsKelasAplicare = oKelasAplicares.GetDataDetail_UOM(grdPEMETAAN.EditValue)
            If dsKelasAplicare IsNot Nothing Then
                If dsKelasAplicare.TERSEDIA > 0 Then
                    grdKDDEPARTMENT.EditValue = dsKelasAplicare.KDDEPARTMENT
                    'grdKDDOCTOR.Text = grdKDDOCTOR_H.EditValue
                Else
                    MsgBox(Statement.ErrorStatement & " Ruangan Penuh", MsgBoxStyle.Information, Me.Text)
                    grdPEMETAAN.ResetText()
                End If
            End If
        End If

    End Sub
    Private Sub btnCariBed_Click(sender As Object, e As EventArgs) Handles btnCariBed.Click
        If grdKDDEPARTMENT.Text = "" Then
            MsgBox("Ruangan Kosong", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If
        If grdKDPENDAFTARAN.Text = "" Then
            MsgBox("Nomor Pendaftaran Kosong", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If

        Try
            Dim dsCariRuanganTerakhir = oPendaftaran_Kunjungan.GetDatabyRegisterdanDeparment(grdKDPENDAFTARAN.EditValue, grdKDDEPARTMENT.EditValue)

            If dsCariRuanganTerakhir IsNot Nothing Then
                sNoId = dsCariRuanganTerakhir.KDKUNJUNGAN
                oFormMode = FORM_MODE.FORM_MODE_EDIT
                fn_ChangeFormState()
            End If

            fn_LoadKDITEMSearch(grdKDDEPARTMENT.EditValue)

            'Dim ds = (From x In oKelasAplicareBed.GetDataDetailByRuangan(grdKDDEPARTMENT.EditValue)
            '          Where x.STATUS <> "TERISI"
            '          Select x.KDUPDATE_APLICARE, TEMPATTIDUR = x.BED, KELAS = x.M_KELASAPLICARE.MEMO, x.STATUS).ToList()

            'grdBed.Properties.DataSource = ds
            'grdBed.Properties.ValueMember = "KDUPDATE_APLICARE"
            'grdBed.Properties.DisplayMember = "TEMPATTIDUR"

            'grdBed.ShowPopup()

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDITEMSearch(ByVal kddepartment As String)
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
            SQL &= "A.KODEBED "
            SQL &= ",NAME_DISPLAY = A.BED + ' ' + B.MEMO  "
            'SQL &= ",KELAS = B.MEMO "
            SQL &= ",A.STATUS "
            SQL &= "FROM "
            SQL &= "M_KELASAPLICARE_DEPARTMENT_BED A "
            SQL &= "INNER JOIN M_KELASAPLICARE B "
            SQL &= "ON A.KDKELASAPLICARE = B.KDKELASAPLICARE "
            SQL &= "WHERE A.KDDEPARTMENT = '" & kddepartment & "' "
            SQL &= "AND A.STATUS <> 'TERISI' "

            'SQL = "SELECT "
            'SQL &= "A.KDDOCTOR "
            'SQL &= ",A.NAME_DISPLAY  "
            'SQL &= "FROM "
            'SQL &= "M_DOCTOR A "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "M_KELASAPLICARE_DEPARTMENT_BED")

            grdKDBED.Properties.DataSource = ds.Tables("M_KELASAPLICARE_DEPARTMENT_BED")
            grdKDBED.Properties.ValueMember = "KODEBED"
            grdKDBED.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grdKDBED.ShowPopup()

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDBED_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDBED.EditValueChanged
        If grdKDBED.Text <> "" Then
            Dim dsBed = oKelasAplicareBed.GetDataKode(grdKDBED.EditValue)
            If dsBed IsNot Nothing Then
                lblBed.Text = dsBed.BED & ", " & dsBed.M_KELASAPLICARE.MEMO & ", " & dsBed.M_DEPARTMENT.NAME_DISPLAY
            End If
        End If
    End Sub
End Class