Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmPendaftaranLangsung
#Region "Declaration"
    Private isLoad As Boolean = False
    Private oCustomer As New Reference.clsCustomer
    Private oPendaftaran As New Admission.clsPendaftaran
    Private sKDDOCTOR As String = String.Empty
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal KDDOCTOR As String)
        sKDDOCTOR = KDDOCTOR
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "Pendaftaran Langsung"

            chkISACTIVE.Text = Item_L1.ISACTIVE

            'btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDCUSTOMER.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDCUSTOMER()
        fn_EmptyMe()
    End Sub
    Private Sub fn_EmptyMe()
        txtKDCUSTOMER.Text = ""
        rbKDJENISKELAMIN.SelectedIndex = 0
        txtNAME_DISPLAY.ResetText()
        deTANGGLLAHIR.DateTime = Now
        txtALAMAT.ResetText()
        chkISACTIVE.Checked = True
    End Sub
    Private Sub fn_LoadDataCustomer(ByVal KDCUSTOMER As String)
        Try
            Dim ds = oCustomer.GetData(KDCUSTOMER)

            If ds IsNot Nothing Then
                txtKDCUSTOMER.Text = ds.KDCUSTOMER
                rbKDJENISKELAMIN.SelectedIndex = ds.KDJENISKELAMIN
                txtNAME_DISPLAY.Text = ds.NAME_DISPLAY
                deTANGGLLAHIR.DateTime = ds.TANGGALLAHIR
                txtALAMAT.Text = ds.ALAMAT
                chkISACTIVE.Checked = ds.ISACTIVE
            Else
                fn_EmptyMe()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtNAME_DISPLAY.Text = String.Empty Then
                txtNAME_DISPLAY.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtNAME_DISPLAY.ErrorText = Statement.ErrorRequired

                txtNAME_DISPLAY.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtALAMAT.Text = String.Empty Then
                txtALAMAT.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtALAMAT.ErrorText = Statement.ErrorRequired

                txtALAMAT.Focus()
                fn_Validate = False
                Exit Function
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_SaveCustomer() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oCustomer.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oCustomer.GetData(txtKDCUSTOMER.Text).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDCUSTOMER = txtKDCUSTOMER.Text
                .KDCUSTOMER_LAMA = ""
                .NAME_DISPLAY = txtNAME_DISPLAY.Text.Trim.ToUpper

                .PHONE = ""
                .FAX = ""
                .MOBILE = ""
                .EMAIL = ""
                .OTHER = ""
                .WEBSITE = ""
                .ALAMAT = txtALAMAT.Text.Trim
                .KTP = ""
                .KDKELURAHAN = oCustomer.StatusKelurahanDefault
                .KODEPOS = ""
                .NEGARA = ""
                .MEMO = sUserID
                .KDCOA = "1401-001"
                .ISACTIVE = chkISACTIVE.Checked
                .KDPERUSAHAAN = oCustomer.PerusahaanDefault
                .KDKESATUAN = oCustomer.KesatuanDefault
                .KDPENJAMIN = oCustomer.PenjaminDefault
                .KDPANGKAT = oCustomer.PangkatDefault
                .KDGOLONGAN = oCustomer.GolonganDefault
                .KDPENDIDIKAN = oCustomer.PendidikanDefault
                .KDPEKERJAAN = oCustomer.PekerjaanDefault
                .KDAGAMA = oCustomer.AgamaDefault
                .KDJENISKELAMIN = rbKDJENISKELAMIN.SelectedIndex
                .KDGOLONGANDARAH = 0
                .KDSTATUSKAWIN = 0
                .KDSUKU = oCustomer.SukuDefault
                .KDSTATUSHIDUP = 1
                .NRP = ""
                .NAMAKELUARGA = ""
                .KDSTATUSKELUARGA = oCustomer.StatusKeluargaDefault
                .TEMPATLAHIR = ""
                .TANGGALLAHIR = deTANGGLLAHIR.DateTime
                .WNI = 0
                .KARTUBPJS = ""
            End With

            If txtKDCUSTOMER.Text = "" Then
                Try
                    txtKDCUSTOMER.Text = oCustomer.InsertData(ds, IIf(txtKDCUSTOMER.Text = "<--- AUTO --->", "", IIf(txtKDCUSTOMER.Text.ToString.Trim = "", "", txtKDCUSTOMER.Text.ToString.Trim)))

                    If txtKDCUSTOMER.Text = "" Then
                        fn_SaveCustomer = False
                    Else
                        fn_SaveCustomer = True
                    End If

                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                Try
                    fn_SaveCustomer = oCustomer.UpdateData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveCustomer = False
        End Try
    End Function
    Private Function fn_SaveDaftar() As Boolean
        Try
            Dim dsCustomer = oCustomer.GetData(txtKDCUSTOMER.Text)
            Dim Pasienbaru As Boolean = False

            If dsCustomer Is Nothing Then
                MsgBox("Pasien Tidak diTemukan di SIMRS", MsgBoxStyle.Exclamation, Me.Text)
                Exit Function
                fn_SaveDaftar = False
            Else
                If dsCustomer.KDCUSTOMER_LAMA = "" Then
                    If dsCustomer.DATECREATED.ToString("yyyyMMdd") = Now.ToString("yyyyMMdd") Then
                        Pasienbaru = True
                    End If
                End If
            End If


            ' ***** HEADER *****
            Dim ds = oPendaftaran.GetStructureHeader
            With ds
                .KDPENDAFTARAN = ""
                .KDPENDAFTARAN_AWAL = ""
                .NOMORSEP = ""
                .DATECREATED = Now
                .DATEUPDATED = Now
                .DATE = Now
                .KDCUSTOMER = txtKDCUSTOMER.Text.ToString.Trim.ToUpper
                .KARTUBPJS = ""
                .KTP = ""
                .KDDAFTAR_L1 = oPendaftaran.Daftar_L1_Default
                .KDDAFTAR_L2 = oPendaftaran.Daftar_L2_Default
                .KDDAFTAR_L3 = oPendaftaran.Daftar_L3_Default
                .KDDAFTAR_L4 = oPendaftaran.Daftar_L4_Default
                .KDDAFTAR_L5 = oPendaftaran.Daftar_L5_Default
                .KDDAFTAR_L6 = "DAFTAR_L6_0000000001"
                .CATEGORY = 0
                .STATUSDAFTAR = 0
                .KDUSER = sUserID
                .ISEKSEKUTIF = False
                .ISKATARAK = False
                .KDDEPARTMENT = oPendaftaran.Depatment_Default(sKDDOCTOR)
                .KDDOCTOR = sKDDOCTOR
                .ASALRUJUKAN = 0
                .DATE_RUJUKAN = Now
                .NOMORRUJUKAN = ""
                .NOMORSKDP = ""
                .KDDOCTOR_SKD = sKDDOCTOR
                .KDDIAGNOSA = oPendaftaran.Diagnosa_Default
                .NOMORTELEPON = ""
                .CATATAN = "AUTO"
                .ISOFFLINE = False
                .KDCOB = oPendaftaran.Daftar_COB_Default
                .KDPPK = oPendaftaran.Daftar_PPK_Default
                .KDKELASRAWAT = oPendaftaran.Daftar_KELASRAWAT_Default
                .REQUEST = ""
                .RESPON = ""
                .ISCOB = False
                .JAMINAN_ISLAKALANTAS = False
                .JAMINAN_PENJAMIN_PENJAMIN1 = False
                .JAMINAN_PENJAMIN_PENJAMIN2 = False
                .JAMINAN_PENJAMIN_PENJAMIN3 = False
                .JAMINAN_PENJAMIN_PENJAMIN4 = False
                .JAMINAN_PENJAMIN_TGLKEJADIAN = Now
                .JAMINAN_PENJAMIN_KETERANGAN = ""
                .JAMINAN_PENJAMIN_SUPLESI_ISSUPLESI = False
                .JAMINAN_PENJAMIN_SUPLESI_NOSEPSUPLESI = ""
                .JAMINAN_PENJAMIN_SUPLESI_LOKASILAKA_KDPROPONSI = ""
                .JAMINAN_PENJAMIN_SUPLESI_LOKASILAKA_KDKABUPATEN = ""
                .JAMINAN_PENJAMIN_SUPLESI_LOKASILAKA_KDKECAMATAN = ""
                .INFORMASIPRB = ""
                .CETAK = 0
                .TERSEDIA = 0
                .KDSHIFT = sSHIFT
                .KDUPDATE_APLICARE = ""
                .KODEBOOKING = ""
                .KDBOOKING = ""
                .NAIKRANAP = ""
                .PEMBIAYAAN = ""
                .PENANGGUNGJAWAB = ""
                .TUJUANKUNJUNGAN = ""
                .FLAGPROCEDURE = ""
                .KDPENUNJANG = ""
                .ASESMENPELAYANAN = ""
            End With

            '***** Kunjungan *****
            Dim dsKunjungan = oPendaftaran.GetStructureHeader_Kunjungan
            With dsKunjungan
                .DATECREATED = Now
                .DATEUPDATED = Now
                .DATE = Now
                .KDKUNJUNGAN = ""
                .KDPENDAFTARAN = ds.KDPENDAFTARAN
                .KDDEPARTMENT = ds.KDDEPARTMENT
                .KDDOCTOR = ds.KDDOCTOR
                .ALAMAT = txtALAMAT.Text.ToString.ToString.ToUpper
                .KDPENJAMIN = dsCustomer.KDPENJAMIN
                .KDKESATUAN = dsCustomer.KDKESATUAN
                .KDPANGKAT = dsCustomer.KDPANGKAT
                .KDGOLONGAN = dsCustomer.KDGOLONGAN
                .KDPENDIDIKAN = dsCustomer.KDPENDIDIKAN
                .KDPEKERJAAN = dsCustomer.KDPEKERJAAN
                .KDPERUSAHAAN = dsCustomer.KDPERUSAHAAN
                .KDSTATUSKAWIN = 0
                .NAMAKELUARGA = ""
                .KDSTATUSKELUARGA = dsCustomer.KDSTATUSKELUARGA
                .KDUSER = sUserID
                .TERSEDIA = 0
                .KDUPDATE_APLICARE = ""

            End With

            '***** Penanggung Jawab *****
            Dim dsPenanggunjawab = oPendaftaran.GetStructureHeader_PenanggungJawab
            With dsPenanggunjawab
                .DATECREATED = Now
                .DATEUPDATED = Now
                .KDPENDAFTARAN = ds.KDPENDAFTARAN
                .NAMA = ""
                .HUBUNGAN = ""
                .ALAMAT = ""
                .NOMORTELEPON = ""
            End With

            Dim dsIdentitas = oPendaftaran.GetStructureHeader_Identitas
            Dim oDepartment As New Reference.clsDepartment
            Dim oDoctor As New Reference.clsDoctor
            Dim oPPK As New Reference.clsPPK
            Dim oDiagnosa As New Reference.clsDiagnosa

            If dsCustomer IsNot Nothing Then
                '***** Identitas *****
                With dsIdentitas
                    .DATECREATED = Now
                    .DATEUPDATED = Now
                    .DATE = Now
                    .CATEGORY = 0
                    .KDKUNJUNGAN = dsKunjungan.KDKUNJUNGAN
                    .KDPENDAFTARAN = ds.KDPENDAFTARAN
                    .PENJAMIN = oPendaftaran.Daftar_L1_Default
                    .KDCUSTOMER = txtKDCUSTOMER.Text
                    .NAMAPASIEN = txtNAME_DISPLAY.Text
                    .ALAMAT = txtALAMAT.Text
                    .DOKTER = ds.KDDOCTOR
                    .TUJUAN = oDepartment.GetData(oPendaftaran.Depatment_Default(sKDDOCTOR)).NAME_DISPLAY
                    .KDDOKTER = sKDDOCTOR
                    .KDTUJUAN = oPendaftaran.Depatment_Default(sKDDOCTOR)
                    .TANGGALLAHIR = dsCustomer.TANGGALLAHIR
                    .JENISKELAMIN = IIf(dsCustomer.KDJENISKELAMIN = 0, "P", "L")
                    .NIK = ""
                    .TEMPATLAHIR = dsCustomer.TEMPATLAHIR
                    .AGAMA = dsCustomer.M_AGAMA.MEMO
                    .PANGKAT = dsCustomer.M_PANGKAT.MEMO
                    .NRP = dsCustomer.NRP
                    .KESATUAN = dsCustomer.M_KESATUAN.MEMO
                    .NOMORTELEPON = ""
                    .PENDIDIKAN = dsCustomer.KDPENDIDIKAN
                    .SUKU = dsCustomer.M_SUKU.MEMO
                    .USIA = oPendaftaran.GetUmurPasien(Now, dsCustomer.TANGGALLAHIR)
                    .NOMORSEP = ""
                    .KELASPELAYANAN = oPendaftaran.Daftar_KELASRAWAT_Default
                    .KARTUBPJS = ""
                    .STATUS_KAWIN = dsCustomer.KDSTATUSKAWIN
                    .HUBUNGAN = ""
                    .HUBUNGAN_NAMA = ""
                    .HUBUNGAN_PENDIDIKAN = ""
                    .HUBUNGAN_PEKERJAAN = ""
                    .NOMORASURANSILAIN = ""
                    .KDGOLONGANDARAH = dsCustomer.KDGOLONGANDARAH
                    .KDDIAGNOSA = oPendaftaran.Diagnosa_Default
                    .KDPENJAMIN = dsCustomer.KDPENJAMIN
                    .KDPERUSAHAAN = dsCustomer.KDPERUSAHAAN
                    .DIAGNOSA = oDiagnosa.GetData(oPendaftaran.Diagnosa_Default).MEMO
                    .KDUSER = sUserID
                    .HAKKELAS = ""
                    .URL_SIGNATURE = ""
                    .NAMA_TANDATANGAN = ""
                    .FASKES = oPPK.GetData(oPendaftaran.Daftar_PPK_Default).MEMO
                End With
            Else
                dsIdentitas = Nothing
            End If


            Try

                Dim sKDPENDAFTARAN As String = oPendaftaran.InsertData(ds, dsKunjungan, Nothing, dsIdentitas)

                If sKDPENDAFTARAN = "" Then
                    fn_SaveDaftar = False
                Else
                    fn_SaveDaftar = True
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try

            If fn_SaveDaftar = True Then
                Try
                    oCustomer.UpdateNomorTelepon(dsCustomer.KDCUSTOMER, "", "")

                    Dim oGrouperRawatJalan As New Grouper.clsR_Identitas_Grouper
                    Dim dsDataGrouper = oGrouperRawatJalan.GetStructureHeader
                    Dim dsCariNosep = oGrouperRawatJalan.GetDataByNoRec(ds.KDPENDAFTARAN)

                    With dsDataGrouper
                        If dsCariNosep Is Nothing Then
                            .kodegrouper = 0
                            .datecreated = ds.DATECREATED
                        Else
                            .kodegrouper = dsCariNosep.kodegrouper
                            .datecreated = dsCariNosep.datecreated
                        End If
                        .dateupdated = ds.DATEUPDATED
                        .jeniskelompokpasien = ds.M_DAFTAR_L1.MEMO

                        If dsCariNosep Is Nothing Then
                            .statusenabled = "1"
                        Else
                            .statusenabled = dsCariNosep.statusenabled
                        End If

                        .norec = ds.KDPENDAFTARAN
                        .nostruklastfk = ds.KDPENDAFTARAN_AWAL
                        .jnsPelayanan = IIf(ds.CATEGORY = 0, "R.Jalan", "R.Inap")
                        .kelasRawat = oPendaftaran.Daftar_KELASRAWAT_Default
                        .noRm = ds.KDCUSTOMER
                        .nama = ds.M_CUSTOMER.NAME_DISPLAY
                        .noKartu = ds.KARTUBPJS
                        .asalrujukan = IIf(ds.ASALRUJUKAN = 0, 1, 2)
                        .noSep = ds.NOMORSEP
                        .noRujukan = ds.NOMORRUJUKAN
                        .Faskes = ds.M_PPK.KODEFASKES & " " & ds.M_PPK.MEMO
                        .dpjpkodevclaim = ds.M_DOCTOR.VCLAIM_KDDPJP
                        .dpjp = ds.M_DOCTOR.NAME_DISPLAY
                        .polikodevclaim = ds.M_DEPARTMENT.VCLAIM_KODEPOLI
                        .poli = ds.M_DEPARTMENT.NAME_DISPLAY
                        .catatan = ds.CATATAN
                        .infromasiprb = ds.INFORMASIPRB
                        .peserta = ds.M_DAFTAR_L2.MEMO
                        .cob = ds.M_COB.MEMO
                        .nomortelepon = ds.NOMORTELEPON
                        .noregistrasi = ds.KDPENDAFTARAN
                        .tglPlgSep = ds.DATE
                        .tglSep = ds.DATE
                        .tgl_lahir = ds.M_CUSTOMER.TANGGALLAHIR
                        .gender = IIf(ds.M_CUSTOMER.KDJENISKELAMIN = 0, "P", "L")
                        .diagnosaawal = ds.M_DIAGNOSA.MEMO
                        If dsCariNosep Is Nothing Then
                            .status = ""
                        Else
                            .status = dsCariNosep.status
                        End If
                        .statuspasien = IIf(ds.DATE.ToString("ddMMyyyy") = ds.M_CUSTOMER.DATECREATED.ToString("ddMMyyy"), IIf(ds.M_CUSTOMER.KDCUSTOMER_LAMA = "", "BARU", "LAMA"), "LAMA")
                        .alamat = ds.M_CUSTOMER.ALAMAT

                        If dsCariNosep Is Nothing Then
                            .tglpulang = ds.DATE
                        Else
                            .tglpulang = dsCariNosep.tglpulang
                        End If
                        .jsonpost = ""
                        .kduser = sUserID
                        .ruangan = IIf(ds.CATEGORY = 0, "", ds.M_DEPARTMENT.NAME_DISPLAY)
                        .pangkat = ds.M_CUSTOMER.M_PANGKAT.MEMO
                        .kesatuan = ds.M_CUSTOMER.M_KESATUAN.MEMO
                        .pendidikan = ds.M_CUSTOMER.KDPENDIDIKAN
                        .statusmenikah = ds.M_CUSTOMER.KDSTATUSKAWIN
                        .propinsi = ds.M_CUSTOMER.M_KELURAHAN.M_KECAMATAN.M_KABUPATEN.M_PROPINSI.MEMO
                        .kabupaten = ds.M_CUSTOMER.M_KELURAHAN.M_KECAMATAN.M_KABUPATEN.MEMO
                        .kecamatan = ds.M_CUSTOMER.M_KELURAHAN.M_KECAMATAN.MEMO
                        .kelurahan = ds.M_CUSTOMER.M_KELURAHAN.MEMO
                    End With

                    If dsCariNosep Is Nothing Then
                        If oGrouperRawatJalan.InsertData(dsDataGrouper) = False Then
                            MsgBox("Simpan Data Grouper Gagal", MsgBoxStyle.Exclamation, Me.Text)
                        End If
                    Else
                        If oGrouperRawatJalan.UpdateData(dsDataGrouper) = False Then
                            MsgBox("Simpan Data Grouper Gagal", MsgBoxStyle.Exclamation, Me.Text)
                        End If
                    End If
                Catch oErr As Exception
                    MsgBox("Simpan Data Grouper" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveDaftar = False
        End Try
    End Function
#End Region
#Region "Command Button"
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        'Select Case e.KeyCode
        'Case Keys.F12
        '    btnClose_Click()
        'Case Keys.F2
        '    If btnSaveNew.Enabled = True Then
        '        btnSaveNew_Click()
        '    End If
        'Case Keys.F3
        '    If btnSaveClose.Enabled = True Then
        '        btnSaveClose_Click()
        '    End If
        'End Select
    End Sub
    'Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
    '    If fn_Validate() = False Then Exit Sub
    '    If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
    '    If fn_Save() = False Then
    '        MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
    '    Else
    '        MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
    '        sStatusSave = "NEW"
    '        Me.Close()
    '    End If
    'End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_SaveCustomer() = False Then
            MsgBox("Simpan Pasien Gagal", MsgBoxStyle.Exclamation, Me.Text)
        Else
            If fn_SaveDaftar() = False Then
                MsgBox("Simpan Pendafataran Gagal", MsgBoxStyle.Exclamation, Me.Text)
            Else
                MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
                Me.Close()
            End If
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadKDCUSTOMER()
        Try
            Dim dsList = From x In oCustomer.GetData()
                         Select x.KDCUSTOMER, x.NAME_DISPLAY, JENISKELAMIN = IIf(x.KDJENISKELAMIN = 0, "P", "L"), ALAMAT = x.ALAMAT, x.TANGGALLAHIR

            grdKDCSUTOMER.Properties.DataSource = oCustomer.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDCSUTOMER.Properties.ValueMember = "KDCUSTOMER"
            grdKDCSUTOMER.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDCSUTOMER_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDCSUTOMER.EditValueChanged
        If isLoad = True Then
            If grdKDCSUTOMER.Text <> "" Then
                fn_LoadDataCustomer(grdKDCSUTOMER.EditValue)
            End If
        End If
    End Sub
#End Region
End Class