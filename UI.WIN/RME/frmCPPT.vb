Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq
Imports System.Data.SqlClient

Public Class frmCPPT
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oGrouperDataCppt As New Grouper.clsR_CPPT
    Private oGetGrouper As New Brigging.clsSetKoneksi
    Private oSalesOrderTransaksi As New Sales.clsSalesOrderTransaksi
    Private oRME As New RME.clsRME
    Private isLoad As Boolean = False
    Private sRincianSudahSimpanByKdPendaftaran As Decimal = 0
    Private sNoRekamMedis As String = String.Empty
    Private sCategory As Integer = 0
    Private sPenjamin As String = String.Empty
    Private sTanggalLahir As DateTime = Now
    Private sRead As Boolean = False
    Private sKelas As String = String.Empty

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDIDENTITAS As Integer, ByVal isRead As Boolean, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        txtCPPT_Kode.Text = NoId
        txtKDIDENTITAS.Text = KDIDENTITAS
        sRead = isRead
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "CPPT"

            lKDITEM_L2.Text = sItem_L2 & " :"
            btnSaveClosee.Caption = "Simpan"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'sCode = txtMEMO.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDITEMTARIF()
        fn_LoadKDITEMOBAT()
        fn_LoadKDITEMSearch()
        fn_LoadITEM_L2()
        fn_LoadKDUOM()
        fn_LoadKDSIGNA()
        fn_LoadKDCARAPAKAI()
        fn_LoadKDPROFESI()
        fn_LoadDaftar4()
        fn_LoadCPPT_TemplateTindakan()
        fn_LoadDPJP()

        Dim oKunjungan As New Admission.clsPendaftaran_Kunjungan
        Dim dsKunjungan = oKunjungan.GetDatabyIdentitas(txtKDIDENTITAS.Text)

        If dsKunjungan IsNot Nothing Then
            sNoRekamMedis = dsKunjungan.KDCUSTOMER
            sCategory = dsKunjungan.CATEGORY
            sPenjamin = dsKunjungan.KDDAFTAR_L1_NAMA
            Dim oCustomer As New Reference.clsCustomer
            Dim dsCustomer = oCustomer.GetData(sNoRekamMedis)
            If dsCustomer IsNot Nothing Then
                sTanggalLahir = dsCustomer.TANGGALLAHIR
            End If
            txtRUANGAN.Text = dsKunjungan.KDDEPARTMENT_NAMA
            grdCPPT_KDDOCTOR.Text = dsKunjungan.KDDOCTOR

            Dim ds = oSalesOrderTransaksi.GetDataKunjunganByKD(dsKunjungan.KDKUNJUNGAN)
            If ds IsNot Nothing Then
                sKelas = ds.S_PENDAFTARAN_H.KDKELASRAWAT
            End If
        End If

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
        'btnSaveNew.Enabled = False
        'btnSaveClose.Enabled = Not Status

        'btnSimpanLaporanOperasi.Enabled = Not Status

        'TableLayoutPanel9.Anchor = AnchorStyles.Top
        'TableLayoutPanel9.Anchor = AnchorStyles.Left
        'TableLayoutPanel9.Anchor = AnchorStyles.Right

        'GroupControl9.Anchor = AnchorStyles.Top
        'GroupControl9.Anchor = AnchorStyles.Left
        'GroupControl9.Anchor = AnchorStyles.Right
        If sRead = True Then
            grdCPPT_TemplateTindakan.Properties.ReadOnly = True
            grdSemuaTindakan.Properties.ReadOnly = True
            grdITEM_L2.ReadOnly = True
        End If
    End Sub
    Private Sub fn_EmptyMe()
        txtCPPT_Kode.Text = "<--- AUTO --->"
        'txtRUANGAN.ResetText()
        XtraTabControlCPPT_Resep.SelectedTabPage = tabCPPT_NonRacikan
        sPicture = Nothing
        picCPPT_Gambar.Image = CType(My.Resources.ResourceManager.GetObject("image1"), Image)
        deCPPT_Tanggal.DateTime = Now
        grdCPPT_Profesi.Text = oGrouperDataCppt.Profesi_Default()
        txtCPPT_KeluhanUtama.ResetText()
        chkCPPT_AlergiTidak.Checked = False
        chkCPPT_AlergiYa.Checked = False
        txtCPPT_AlergiYa.ResetText()
        txtCPPT_Kesadaran.ResetText()
        txtCPPT_GCS.ResetText()
        txtCPPT_TampakSakit.ResetText()
        txtCPPT_VisualAnalogScore.ResetText()
        txtCPPT_BeratBadan.ResetText()
        txtCPPT_TinggiBadan.ResetText()
        txtCPPT_SpO2.ResetText()
        txtCPPT_Sistole.Text = "0"
        txtCPPT_Diastole.Text = "0"
        txtCPPT_HR.ResetText()
        txtCPPT_RR.ResetText()
        txtCPPT_Suhu.ResetText()
        txtCPPT_Pemeriksaan.ResetText()
        txtCPPT_Indikasi.ResetText()
        txtCPPT_CATATAN.ResetText()
        txtCPPT_Alasan.ResetText()
        txtCPPT_OBJEKTIF_ALAMATGAMBARPEMERIKSAAN.ResetText()
        grdKDDAFTAR_L4.Text = oGrouperDataCppt.Daftar_L4_Default()

        grvCPPT_Diagnosa.OptionsSelection.MultiSelect = True
        grvCPPT_Diagnosa.SelectAll()
        grvCPPT_Diagnosa.DeleteSelectedRows()
        grvCPPT_Diagnosa.OptionsSelection.MultiSelect = False

        grvCPPT_Tindakan.OptionsSelection.MultiSelect = True
        grvCPPT_Tindakan.SelectAll()
        grvCPPT_Tindakan.DeleteSelectedRows()
        grvCPPT_Tindakan.OptionsSelection.MultiSelect = False

        grvCPPT_ResepNonRacikan.OptionsSelection.MultiSelect = True
        grvCPPT_ResepNonRacikan.SelectAll()
        grvCPPT_ResepNonRacikan.DeleteSelectedRows()
        grvCPPT_ResepNonRacikan.OptionsSelection.MultiSelect = False

        grvCPPT_ResepRacikan.OptionsSelection.MultiSelect = True
        grvCPPT_ResepRacikan.SelectAll()
        grvCPPT_ResepRacikan.DeleteSelectedRows()
        grvCPPT_ResepRacikan.OptionsSelection.MultiSelect = False

        Dim oUser As New Setting.clsUser
        Dim dsUser = oUser.GetData(sUserID)
        If dsUser IsNot Nothing Then
            If dsUser.KDDOCTOR <> "" Then
                grdCPPT_Profesi.Text = "PROFESI_0000000001"
            End If
        End If

        Dim dsTerakhirTTV = oGrouperDataCppt.GetDataByRmTerakhirTTV(sNoRekamMedis)
        If dsTerakhirTTV IsNot Nothing Then
            If dsTerakhirTTV.ISDELETE = False Then
                fn_CPPT_LoadDataTerakhirTTV(dsTerakhirTTV.KDCPPT)
            End If
        End If

        Dim dsTerakhirUser = oGrouperDataCppt.GetDataByRmTerakhirByUserAndDokter(sNoRekamMedis, sUserID)
        If dsTerakhirUser IsNot Nothing Then
            If dsTerakhirUser.ISDELETE = False Then
                BindingSourceCPPT_Diagnosa.DataSource = oGrouperDataCppt.GetDataDetailDiagnosa(dsTerakhirUser.KDCPPT).OrderBy(Function(x) x.SEQ).ToList()
                grdCPPT_Diagnosa.DataSource = BindingSourceCPPT_Diagnosa
            End If
        End If

        'Try
        '    sRincianSudahSimpanByKdPendaftaran = oSalesOrderTransaksi.GetTotalByKdPendftaran(lblNorec.Text, lblNorec2.Text)
        'Catch ex As Exception
        '    sRincianSudahSimpanByKdPendaftaran = 0
        'End Try

        'txtCPPT_TotalKlaim.Text = 186800
        'txtCPPT_TotalPaket.Text = 0
        'txtCPPT_Limit.Text = 0
        Calculate()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oGrouperDataCppt.GetData(txtCPPT_Kode.Text)
            Dim sBaca As Boolean = False

            With ds
                txtCPPT_Kode.Text = .KDCPPT
                txtKDIDENTITAS.Text = .KDIDENTITAS
                deCPPT_Tanggal.DateTime = .DATE
                grdCPPT_Profesi.Text = .KDPROFESI
                txtCPPT_KeluhanUtama.Text = .SUBJEKTIF_KELUHANUTAMA
                chkCPPT_AlergiTidak.Checked = .SUBJEKTIF_ALERGI_TIDAK
                chkCPPT_AlergiYa.Checked = .SUBJEKTIF_ALERGI_YA
                txtCPPT_AlergiYa.Text = .SUBJEKTIF_ALERGI_YA_TEXT
                txtCPPT_Kesadaran.Text = .OBJEKTIF_KESADARAN
                txtCPPT_GCS.Text = .OBJEKTIF_GCS
                txtCPPT_TampakSakit.Text = .OBJEKTIF_TAMPAKSAKIT
                txtCPPT_VisualAnalogScore.Text = .OBJEKTIF_VISUALANALOGSCORE
                txtCPPT_BeratBadan.Text = .OBJEKTIF_BERATBADAN
                txtCPPT_TinggiBadan.Text = .OBJEKTIF_TINGGIBADAN
                txtCPPT_SpO2.Text = .OBJEKTIF_SPO2
                txtCPPT_Sistole.Text = .OBJEKTIF_SISTOLE
                txtCPPT_Diastole.Text = .OBJEKTIF_DIASTOLE
                txtCPPT_HR.Text = .OBJEKTIF_HR
                txtCPPT_RR.Text = .OBJEKTIF_RR
                txtCPPT_Suhu.Text = .OBJEKTIF_SUHU
                txtCPPT_Pemeriksaan.Text = .OBJEKTIF_PEMERIKSAAN
                txtCPPT_Indikasi.Text = .ASSEMENT_INDIKASI
                grdKDDAFTAR_L4.Text = .PLANNING_ISTINDAKLANJUT_KONSUL_TEXT
                txtCPPT_CATATAN.Text = .CATATAN
                txtCPPT_Alasan.Text = .PLANNING_ALASAN
                txtCPPT_OBJEKTIF_ALAMATGAMBARPEMERIKSAAN.Text = .OBJEKTIF_ALAMATGAMBARPEMERIKSAAN

                If txtCPPT_OBJEKTIF_ALAMATGAMBARPEMERIKSAAN.Text <> "" Then
                    picCPPT_Gambar.Image = Image.FromFile(txtCPPT_OBJEKTIF_ALAMATGAMBARPEMERIKSAAN.Text)
                    sPicture = picCPPT_Gambar.Image
                Else
                    picCPPT_Gambar.Image = CType(My.Resources.ResourceManager.GetObject("image1"), Image)
                    sPicture = Nothing
                End If

                BindingSourceCPPT_Diagnosa.DataSource = oGrouperDataCppt.GetDataDetailDiagnosa(.KDCPPT).OrderBy(Function(x) x.SEQ).ToList()
                grdCPPT_Diagnosa.DataSource = BindingSourceCPPT_Diagnosa

                BindingSourceCPPT_Tindakan.DataSource = oGrouperDataCppt.GetDataDetailTindakan(.KDCPPT).OrderBy(Function(x) x.SEQ).ToList()
                grdCPPT_Tindakan.DataSource = BindingSourceCPPT_Tindakan

                BindingSourceCPPT_NonRacikan.DataSource = oGrouperDataCppt.GetDataDetailNonRacikan(.KDCPPT).OrderBy(Function(x) x.SEQ).ToList()
                grdCPPT_ResepNonRacikan.DataSource = BindingSourceCPPT_NonRacikan

                BindingSourceCPPT_Racikan.DataSource = oGrouperDataCppt.GetDataDetailRacikan(.KDCPPT).OrderBy(Function(x) x.SEQ).ToList()
                grdCPPT_ResepRacikan.DataSource = BindingSourceCPPT_Racikan

                XtraTabControlCPPT_Resep.SelectedTabPage = tabCPPT_Racikan
                XtraTabControlCPPT_Resep.SelectedTabPage = tabCPPT_NonRacikan

                'Try
                '    sRincianSudahSimpanByKdPendaftaran = oSalesOrderTransaksi.GetTotalByKdPendftaran(.R_IDENTITAS_GROUPER.norec, .R_IDENTITAS_GROUPER.nostruklastfk)
                'Catch ex As Exception
                '    sRincianSudahSimpanByKdPendaftaran = 0
                'End Try
                'For i As Integer = 0 To grvCPPT_ResepNonRacikan.RowCount - 2
                '    If grvCPPT_ResepNonRacikan.GetRowCellValue(i, colCPPT_ISBACANONRACIKAN) = 1 Then
                '        sBaca = True
                '    End If
                'Next
                'For i As Integer = 0 To grvCPPT_ResepRacikan.RowCount - 2
                '    If grvCPPT_ResepRacikan.GetRowCellValue(i, colCPPT_ISBACARACIKAN) = 1 Then
                '        sBaca = True
                '    End If
                'Next

                Dim dsCekFarmasi = oSalesOrderTransaksi.GetDataByCategory(.KDCPPT, 4)

                If dsCekFarmasi IsNot Nothing Then
                    grvCPPT_ResepNonRacikan.Columns("KDITEM").OptionsColumn.AllowEdit = False
                    grvCPPT_ResepNonRacikan.Columns("KDITEM").OptionsColumn.AllowFocus = False
                    grvCPPT_ResepNonRacikan.Columns("KDITEM").OptionsColumn.ReadOnly = True
                    grvCPPT_ResepNonRacikan.Columns("KDITEM").OptionsColumn.TabStop = False

                    grvCPPT_ResepRacikan.Columns("KDITEM").OptionsColumn.AllowEdit = False
                    grvCPPT_ResepRacikan.Columns("KDITEM").OptionsColumn.AllowFocus = False
                    grvCPPT_ResepRacikan.Columns("KDITEM").OptionsColumn.ReadOnly = True
                    grvCPPT_ResepRacikan.Columns("KDITEM").OptionsColumn.TabStop = False

                    MsgBox("Maap Tidak dapat Merubah Resep karena Sudah di Baca Farmasi, Silahkan Hubungi Unit Bersangkutan", MsgBoxStyle.Exclamation, Me.Text)
                End If

                Calculate()
                'CalculateLimit()
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Dim oPembayaran As New Finance.clsCashIn

                Dim dsPembayaran = oPembayaran.GetDataDetailBayar(txtCPPT_Kode.Text)
                If dsPembayaran IsNot Nothing Then
                    MsgBox("Kasir Sudah Membuat Pembayaran", MsgBoxStyle.Exclamation, Me.Text)
                    fn_Validate = False
                    Exit Function
                End If
            End If

            If txtKDIDENTITAS.Text = "" Then
                If txtKDIDENTITAS.Text = 0 Then
                    txtKDIDENTITAS.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    txtKDIDENTITAS.ErrorText = Statement.ErrorRequired

                    MsgBox("Kode Kunjungan Kosong", MsgBoxStyle.Exclamation, Me.Text)
                    fn_Validate = False
                    Exit Function
                End If
            End If
            If grdCPPT_KDDOCTOR.Text = "" Then
                grdCPPT_KDDOCTOR.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdCPPT_KDDOCTOR.ErrorText = Statement.ErrorRequired

                MsgBox("Dibutuhkan DPJP", MsgBoxStyle.Exclamation, Me.Text)
                grdCPPT_KDDOCTOR.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtRUANGAN.Text = "" Then
                txtRUANGAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtRUANGAN.ErrorText = Statement.ErrorRequired

                MsgBox("Dibutuhkan Poli/Ruangan", MsgBoxStyle.Exclamation, Me.Text)
                txtRUANGAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdCPPT_Profesi.Text = "" Then
                grdCPPT_Profesi.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdCPPT_Profesi.ErrorText = Statement.ErrorRequired

                MsgBox("Dibutuhkan Profesi", MsgBoxStyle.Exclamation, Me.Text)
                grdCPPT_Profesi.Focus()
                fn_Validate = False
                Exit Function
            End If

            grvCPPT_Diagnosa.UpdateCurrentRow()

            If grdCPPT_Profesi.Text = "DOKTER" Then
                If grvCPPT_Diagnosa.RowCount < 2 Then
                    MsgBox("Dibutuhkan Diagnosa", MsgBoxStyle.Exclamation, Me.Text)
                    fn_Validate = False
                    Exit Function
                End If
            Else
                If grvCPPT_ResepNonRacikan.RowCount < 2 Then

                Else
                    MsgBox("Profesi " & grdCPPT_Profesi.Text & " Tidak dapat input Resep", MsgBoxStyle.Exclamation, Me.Text)
                    fn_Validate = False
                    Exit Function
                End If
                If grvCPPT_ResepRacikan.RowCount < 2 Then
                Else
                    MsgBox("Profesi " & grdCPPT_Profesi.Text & " Tidak dapat input Resep", MsgBoxStyle.Exclamation, Me.Text)
                    fn_Validate = False
                    Exit Function
                End If

                'If sCategory = 0 Then
                '    If Not txtRUANGAN.Text.ToString.Contains("Darurat") Then
                '        If grvCPPT_ResepNonRacikan.RowCount < 2 Then

                '        Else
                '            MsgBox("Profesi " & grdCPPT_Profesi.Text & " Tidak dapat input Resep", MsgBoxStyle.Exclamation, Me.Text)
                '            fn_Validate = False
                '            Exit Function
                '        End If
                '        If grvCPPT_ResepRacikan.RowCount < 2 Then
                '        Else
                '            MsgBox("Profesi " & grdCPPT_Profesi.Text & " Tidak dapat input Resep", MsgBoxStyle.Exclamation, Me.Text)
                '            fn_Validate = False
                '            Exit Function
                '        End If
                '    End If
                'End If
            End If

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                If sCategory = 0 Then
                    If Not txtRUANGAN.Text.ToString.Contains("Darurat") Then
                        If grdCPPT_Profesi.Text = "DOKTER" Then
                            Dim dsByNorec = oGrouperDataCppt.GetDataValidasiRawatJalanUntukResume(txtKDIDENTITAS.Text)
                            If dsByNorec IsNot Nothing Then
                                MsgBox("CPPT Sudah di Buat Silahkan Pilih Propesi Dokter Konsul atau Profesi Lain", MsgBoxStyle.Exclamation, Me.Text)
                                fn_Validate = False
                                Exit Function
                            End If
                        End If
                    End If
                End If
            End If
        Catch oErr As Exception
            fn_Validate = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub fn_CPPT_LoadDataTerakhirTTV(ByVal KDCPPT As String)
        Try
            ' ***** HEADER *****
            Dim ds = oGrouperDataCppt.GetData(KDCPPT)

            If ds IsNot Nothing Then
                With ds
                    If ds.KDIDENTITAS = txtKDIDENTITAS.Text Then
                        txtCPPT_KeluhanUtama.Text = .SUBJEKTIF_KELUHANUTAMA
                        chkCPPT_AlergiTidak.Checked = .SUBJEKTIF_ALERGI_TIDAK
                        chkCPPT_AlergiYa.Checked = .SUBJEKTIF_ALERGI_YA
                        txtCPPT_AlergiYa.Text = .SUBJEKTIF_ALERGI_YA_TEXT
                        txtCPPT_Kesadaran.Text = .OBJEKTIF_KESADARAN
                        txtCPPT_GCS.Text = .OBJEKTIF_GCS
                        txtCPPT_TampakSakit.Text = .OBJEKTIF_TAMPAKSAKIT
                        txtCPPT_VisualAnalogScore.Text = .OBJEKTIF_VISUALANALOGSCORE
                        txtCPPT_BeratBadan.Text = .OBJEKTIF_BERATBADAN
                        txtCPPT_TinggiBadan.Text = .OBJEKTIF_TINGGIBADAN
                        txtCPPT_SpO2.Text = .OBJEKTIF_SPO2
                        txtCPPT_Sistole.Text = .OBJEKTIF_SISTOLE
                        txtCPPT_Diastole.Text = .OBJEKTIF_DIASTOLE
                        txtCPPT_HR.Text = .OBJEKTIF_HR
                        txtCPPT_RR.Text = .OBJEKTIF_RR
                        txtCPPT_Suhu.Text = .OBJEKTIF_SUHU
                        txtCPPT_Pemeriksaan.Text = .OBJEKTIF_PEMERIKSAAN
                    Else
                        txtCPPT_BeratBadan.Text = .OBJEKTIF_BERATBADAN
                        txtCPPT_TinggiBadan.Text = .OBJEKTIF_TINGGIBADAN
                    End If
                End With
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_CPPT_LoadDataTerakhirUserSemua(ByVal KDCPPT As String)
        Try
            ' ***** HEADER *****
            Dim ds = oGrouperDataCppt.GetData(KDCPPT)

            If ds IsNot Nothing Then
                With ds
                    BindingSourceCPPT_Diagnosa.DataSource = oGrouperDataCppt.GetDataDetailDiagnosa(.KDCPPT).OrderBy(Function(x) x.SEQ).ToList()
                    grdCPPT_Diagnosa.DataSource = BindingSourceCPPT_Diagnosa

                    If ds.A_IDENTITASPASIEN_LIST.CATEGORY = 0 Then
                        BindingSourceCPPT_Tindakan.DataSource = oGrouperDataCppt.GetDataDetailTindakan(.KDCPPT).OrderBy(Function(x) x.SEQ).ToList()
                        grdCPPT_Tindakan.DataSource = BindingSourceCPPT_Tindakan

                        If grdCPPT_Profesi.Text = "DOKTER" Then
                            BindingSourceCPPT_NonRacikan.DataSource = oGrouperDataCppt.GetDataDetailNonRacikan(.KDCPPT).OrderBy(Function(x) x.SEQ).ToList()
                            grdCPPT_ResepNonRacikan.DataSource = BindingSourceCPPT_NonRacikan

                            BindingSourceCPPT_Racikan.DataSource = oGrouperDataCppt.GetDataDetailRacikan(.KDCPPT).OrderBy(Function(x) x.SEQ).ToList()
                            grdCPPT_ResepRacikan.DataSource = BindingSourceCPPT_Racikan
                        End If

                        XtraTabControlCPPT_Resep.SelectedTabPage = tabCPPT_Racikan
                        XtraTabControlCPPT_Resep.SelectedTabPage = tabCPPT_NonRacikan
                    End If

                    Calculate()
                    'CalculateLimit()
                End With
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Save() As Boolean
        Try
            Dim oItem As New Reference.clsItem
            Dim oUom As New Reference.clsUOM
            Dim oSiga As New Reference.clsSigna
            Dim oCaraPakai As New Reference.clsCaraPakai
            Dim Alasan As String = String.Empty

            ' ***** HEADER *****
            Dim ds = oGrouperDataCppt.GetStructureHeader
            With ds
                .DATECREATED = Now
                .DATEUPDATED = Now
                .DATE = deCPPT_Tanggal.DateTime
                .KDCPPT = txtCPPT_Kode.Text
                .KDIDENTITAS = txtKDIDENTITAS.Text
                .DATE = deCPPT_Tanggal.DateTime
                .KDPROFESI = grdCPPT_Profesi.EditValue

                .SUBJEKTIF_KELUHANUTAMA = txtCPPT_KeluhanUtama.Text
                .SUBJEKTIF_ALERGI_TIDAK = chkCPPT_AlergiTidak.Checked
                .SUBJEKTIF_ALERGI_YA = chkCPPT_AlergiYa.Checked
                .SUBJEKTIF_ALERGI_YA_TEXT = txtCPPT_AlergiYa.Text

                Dim listSubjektif As New List(Of String)

                listSubjektif.Add("Keluhan Utama " & .SUBJEKTIF_KELUHANUTAMA)

                Dim Alergi As String = String.Empty

                If .SUBJEKTIF_ALERGI_TIDAK = True Then
                    Alergi = "Alergi Obat: Tidak"
                End If
                If .SUBJEKTIF_ALERGI_YA = True Then
                    Alergi = "Alergi Obat: Ya"
                End If

                'listSubjektif.Add((Alergi & " " & .SUBJEKTIF_ALERGI_YA_TEXT).Trim)

                .SUBJEKTIF_TEXT = String.Join(vbCrLf, listSubjektif.ToArray)

                .OBJEKTIF_KESADARAN = txtCPPT_Kesadaran.Text
                .OBJEKTIF_GCS = txtCPPT_GCS.Text
                .OBJEKTIF_TAMPAKSAKIT = txtCPPT_TampakSakit.Text
                .OBJEKTIF_VISUALANALOGSCORE = txtCPPT_VisualAnalogScore.Text
                .OBJEKTIF_BERATBADAN = txtCPPT_BeratBadan.Text
                .OBJEKTIF_TINGGIBADAN = txtCPPT_TinggiBadan.Text
                .OBJEKTIF_SPO2 = txtCPPT_SpO2.Text
                .OBJEKTIF_SISTOLE = txtCPPT_Sistole.Text
                .OBJEKTIF_DIASTOLE = txtCPPT_Diastole.Text
                .OBJEKTIF_HR = txtCPPT_HR.Text
                .OBJEKTIF_RR = txtCPPT_RR.Text
                .OBJEKTIF_SUHU = txtCPPT_Suhu.Text
                .OBJEKTIF_PEMERIKSAAN = txtCPPT_Pemeriksaan.Text
                If sPicture Is Nothing Then
                    .OBJEKTIF_ALAMATGAMBARPEMERIKSAAN = txtCPPT_OBJEKTIF_ALAMATGAMBARPEMERIKSAAN.Text
                Else
                    'Dim oSetKoneksi As New Brigging.clsSetKoneksi
                    'Dim sALAMATSIMPAN As String = String.Empty
                    'Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "SIMPANPDFCASMIX")
                    'If dsDataSetKoneksi IsNot Nothing Then
                    '    sALAMATSIMPAN = dsDataSetKoneksi.ALAMATWEB

                    '    If Not IO.Directory.Exists(sALAMATSIMPAN) Then
                    '        .OBJEKTIF_ALAMATGAMBARPEMERIKSAAN = txtCPPT_OBJEKTIF_ALAMATGAMBARPEMERIKSAAN.Text
                    '    Else
                    '        Dim Alamat As String = sALAMATSIMPAN & Now.ToString("ddMMyyyyHHmm") & "." & .KDIDENTITAS & ".Png"
                    '        picCPPT_Gambar.Image.Save(Alamat, System.Drawing.Imaging.ImageFormat.Png)
                    '        .OBJEKTIF_ALAMATGAMBARPEMERIKSAAN = Alamat
                    '    End If
                    'Else
                    '    .OBJEKTIF_ALAMATGAMBARPEMERIKSAAN = txtCPPT_OBJEKTIF_ALAMATGAMBARPEMERIKSAAN.Text
                    'End If

                    MsgBox("Belum Ada Simpan Gambar")
                    .OBJEKTIF_ALAMATGAMBARPEMERIKSAAN = txtCPPT_OBJEKTIF_ALAMATGAMBARPEMERIKSAAN.Text
                End If

                Dim listOBjektif As New List(Of String)

                listOBjektif.Add("Umur : " & oRME.GetUmurPasien(Now, sTanggalLahir))

                If .OBJEKTIF_KESADARAN <> "" Then
                    listOBjektif.Add("Kesadaran : " & .OBJEKTIF_KESADARAN)
                End If
                If .OBJEKTIF_GCS <> "" Then
                    listOBjektif.Add("GCS : " & .OBJEKTIF_GCS)
                End If
                If .OBJEKTIF_TAMPAKSAKIT <> "" Then
                    listOBjektif.Add("Tampak Sakit : " & .OBJEKTIF_TAMPAKSAKIT)
                End If
                If .OBJEKTIF_VISUALANALOGSCORE <> "" Then
                    listOBjektif.Add("Visual Analog Score : " & .OBJEKTIF_VISUALANALOGSCORE)
                End If
                If .OBJEKTIF_BERATBADAN <> "" Then
                    listOBjektif.Add("Berat Badan : " & .OBJEKTIF_BERATBADAN.Replace(",", ".") & " kg")
                End If
                If .OBJEKTIF_TINGGIBADAN <> "" Then
                    listOBjektif.Add("Tinggi Badan : " & .OBJEKTIF_TINGGIBADAN.Replace(",", ".") & " cm")
                End If
                If .OBJEKTIF_SPO2 <> "" Then
                    listOBjektif.Add("SpO2 : " & .OBJEKTIF_SPO2 & " %")
                End If
                listOBjektif.Add("Tekanan Darah : " & .OBJEKTIF_SISTOLE & "/" & .OBJEKTIF_DIASTOLE & " mmHg")
                If .OBJEKTIF_HR <> "" Then
                    listOBjektif.Add("HR : " & .OBJEKTIF_HR & " x/mnt")
                End If
                If .OBJEKTIF_RR <> "" Then
                    listOBjektif.Add("RR : " & .OBJEKTIF_RR & " x/mnt")
                End If
                If .OBJEKTIF_SUHU <> "" Then
                    listOBjektif.Add("Suhu : " & .OBJEKTIF_SUHU & " oC")
                End If
                If .OBJEKTIF_PEMERIKSAAN <> "" Then
                    listOBjektif.Add("Pemeriksaan : " & .OBJEKTIF_PEMERIKSAAN)
                End If

                .OBJEKTIF_TEXT = String.Join(vbCrLf, listOBjektif.ToArray)

                .ASSEMENT_INDIKASI = txtCPPT_Indikasi.Text

                Dim listAsesment As New List(Of String)
                Dim listDiagnosaPrimer As New List(Of String)
                Dim listDiagnosaSekunder As New List(Of String)

                For i As Integer = 0 To grvCPPT_Diagnosa.RowCount - 2
                    If (grvCPPT_Diagnosa.GetRowCellValue(i, colCPPT_KATEGORI)) = "Primer" Then
                        listDiagnosaPrimer.Add(grvCPPT_Diagnosa.GetRowCellValue(i, colCPPT_MEMODIAGNOSA))
                    Else
                        listDiagnosaSekunder.Add(grvCPPT_Diagnosa.GetRowCellValue(i, colCPPT_MEMODIAGNOSA))
                    End If
                Next

                listAsesment.Add(String.Join(", ", listDiagnosaPrimer.ToArray))
                listAsesment.Add(String.Join(", ", listDiagnosaSekunder.ToArray))

                'If .ASSEMENT_INDIKASI <> "" Then
                '    listAsesment.Add("Indikasi : " & .ASSEMENT_INDIKASI)
                'End If

                .ASSEMENT_TEXT = String.Join(vbCrLf, listAsesment.ToArray)

                .PLANNING_ISTINDAKLANJUT_PULANG = False
                .PLANNING_ISTINDAKLANJUT_RAWAT = False
                .PLANNING_ISTINDAKLANJUT_KONSUL = False
                .PLANNING_ISTINDAKLANJUT_RUJUK = False
                .PLANNING_ISTINDAKLANJUT_KONSUL_TEXT = grdKDDAFTAR_L4.EditValue
                .PLANNING_ISTINDAKLANJUT_RUJUK_TEXT = ""
                .PLANNING_ALASAN = txtCPPT_Alasan.Text
                .PLANNING_TEXT = ""

                Dim oSkd As New Admission.clsSKD
                Dim oKunjungan As New Admission.clsPendaftaran_Kunjungan

                Dim dsPendaftaran = oKunjungan.GetDatabyIdentitas(txtKDIDENTITAS.Text)
                If dsPendaftaran IsNot Nothing Then
                    Dim dsKunjungan = oKunjungan.GetData(dsPendaftaran.KDKUNJUNGAN)

                    If dsKunjungan IsNot Nothing Then
                        Dim dsSKD = oSkd.GetDataPendaftaran(dsKunjungan.KDPENDAFTARAN)

                        If dsSKD IsNot Nothing Then
                            txtCPPT_CATATAN.Text = "Kontrol Tanggal : " & dsSKD.DATEKONTROL.ToString("dd-MM-yyyy")
                            Alasan = dsSKD.DESCRIPTION & " " & dsSKD.ALASAN
                        End If
                    End If
                Else
                    MsgBox("Maap Registrasi Tidak di Temukan", MsgBoxStyle.Exclamation, Me.Text)
                End If


                .CATATAN = txtCPPT_CATATAN.Text
                .KDUSER = sUserID
                Try
                    .ISDELETE = oGrouperDataCppt.GetData(txtCPPT_Kode.Text).ISDELETE
                Catch ex As Exception
                    .ISDELETE = False
                End Try
                Try
                    .DATEDELETE = oGrouperDataCppt.GetData(txtCPPT_Kode.Text).DATEDELETE
                Catch ex As Exception
                    .DATEDELETE = ds.DATECREATED
                End Try
                Try
                    .USERDELETE = oGrouperDataCppt.GetData(txtCPPT_Kode.Text).USERDELETE
                Catch ex As Exception
                    .USERDELETE = ""
                End Try
                Try
                    .GOALOFTREATMENT = oGrouperDataCppt.GetData(txtCPPT_Kode.Text).GOALOFTREATMENT
                Catch ex As Exception
                    .GOALOFTREATMENT = ""
                End Try
                Try
                    .TINDAKANREHAB = oGrouperDataCppt.GetData(txtCPPT_Kode.Text).TINDAKANREHAB
                Catch ex As Exception
                    .TINDAKANREHAB = ""
                End Try
                Try
                    .EDUKASI = oGrouperDataCppt.GetData(txtCPPT_Kode.Text).EDUKASI
                Catch ex As Exception
                    .EDUKASI = ""
                End Try
                Try
                    .FREKUENSIKUNJUNGAN = oGrouperDataCppt.GetData(txtCPPT_Kode.Text).FREKUENSIKUNJUNGAN
                Catch ex As Exception
                    .FREKUENSIKUNJUNGAN = ""
                End Try
            End With

            ' ***** DETIL *****
            Dim arrDetailDiagnosa = oGrouperDataCppt.GetStructureDetailDiagnosaList
            For i As Integer = 0 To grvCPPT_Diagnosa.RowCount - 2
                Dim dsDetail = oGrouperDataCppt.GetStructureDetailDiagnosa
                With dsDetail
                    .DATECREATED = ds.DATECREATED
                    .DATEUPDATED = ds.DATEUPDATED
                    .KDCPPT = ds.KDCPPT
                    .SEQ = i
                    .KATEGORI = IIf(String.IsNullOrEmpty(grvCPPT_Diagnosa.GetRowCellValue(i, colCPPT_KATEGORI)), "", grvCPPT_Diagnosa.GetRowCellValue(i, colCPPT_KATEGORI))
                    .KDDIAGNOSA = IIf(String.IsNullOrEmpty(grvCPPT_Diagnosa.GetRowCellValue(i, colCPPT_KDDIAGNOSA)), "", grvCPPT_Diagnosa.GetRowCellValue(i, colCPPT_KDDIAGNOSA))
                    .MEMO = IIf(String.IsNullOrEmpty(grvCPPT_Diagnosa.GetRowCellValue(i, colCPPT_MEMODIAGNOSA)), "", grvCPPT_Diagnosa.GetRowCellValue(i, colCPPT_MEMODIAGNOSA))
                    .KDUSER = sUserID
                End With
                arrDetailDiagnosa.Add(dsDetail)
            Next

            Dim arrDetailTindakan = oGrouperDataCppt.GetStructureDetailTindakanList
            For i As Integer = 0 To grvCPPT_Tindakan.RowCount - 2
                Dim dsDetail = oGrouperDataCppt.GetStructureDetailTindakan
                With dsDetail
                    .DATECREATED = ds.DATECREATED
                    .DATEUPDATED = ds.DATEUPDATED
                    .KDCPPT = ds.KDCPPT
                    .SEQ = i
                    .KDITEM = grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_KDITEMTINDAKAN)
                    .KDUOM = grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_KDUOMTINDAKAN)
                    .JUMLAH = CDec(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_JUMLAHTINDAKAN))
                    .HARGA = CDec(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_HARGATINDAKAN))
                    .TOTAL = CDec(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_TOTALTINDAKAN))
                    .MEMO = IIf(String.IsNullOrEmpty(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)), "", grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN))
                    .KDUSER = sUserID
                    .ISBACA = IIf(String.IsNullOrEmpty(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_ISBACA)), "0", grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_ISBACA))
                End With
                arrDetailTindakan.Add(dsDetail)
            Next

            Dim arrDetail = oGrouperDataCppt.GetStructureDetailNonRacikanList
            For i As Integer = 0 To grvCPPT_ResepNonRacikan.RowCount - 2
                Dim dsDetail = oGrouperDataCppt.GetStructureDetailNonRacikan
                With dsDetail
                    .DATECREATED = ds.DATECREATED
                    .DATEUPDATED = ds.DATEUPDATED
                    .SEQ = i
                    .KDCPPT = ds.KDCPPT
                    Dim dsItem = oItem.GetData(grvCPPT_ResepNonRacikan.GetRowCellValue(i, colCPPT_KDITEMNONRACIKAN))
                    If dsItem IsNot Nothing Then
                        .NAMAOBAT = dsItem.NMITEM2
                    Else
                        .NAMAOBAT = ""
                    End If
                    Dim dsUom = oUom.GetData(grvCPPT_ResepNonRacikan.GetRowCellValue(i, colCPPT_KDUOMNONRACIKAN))
                    If dsUom IsNot Nothing Then
                        .SATUAN = dsUom.MEMO
                    Else
                        .SATUAN = ""
                    End If
                    Dim dsSigna = oSiga.GetData(grvCPPT_ResepNonRacikan.GetRowCellValue(i, colCPPT_KDSIGNANONRACIKAN))
                    If dsSigna IsNot Nothing Then
                        .SIGNA = dsSigna.MEMO
                    Else
                        .SIGNA = ""
                    End If
                    Dim dsCaraPakai = oCaraPakai.GetData(grvCPPT_ResepNonRacikan.GetRowCellValue(i, colCPPT_KDCARAPAKAINONRACIKAN))
                    If dsCaraPakai IsNot Nothing Then
                        .CARAPAKAI = dsCaraPakai.MEMO
                    Else
                        .CARAPAKAI = ""
                    End If
                    .KDCARAPAKAI = IIf(String.IsNullOrEmpty(grvCPPT_ResepNonRacikan.GetRowCellValue(i, colCPPT_KDCARAPAKAINONRACIKAN)), oItem.DefaultItem_CaraPakai, grvCPPT_ResepNonRacikan.GetRowCellValue(i, colCPPT_KDCARAPAKAINONRACIKAN))
                    .JUMLAH_PAKET = CDec(grvCPPT_ResepNonRacikan.GetRowCellValue(i, colCPPT_JUMLAH_PAKETNONRACIKAN))
                    .JUMLAH_NONPAKET = CDec(grvCPPT_ResepNonRacikan.GetRowCellValue(i, colCPPT_JUMLAH_NONPAKETNONRACIKAN))
                    .JUMLAH = CDec(grvCPPT_ResepNonRacikan.GetRowCellValue(i, colCPPT_JUMLAHNONRACIKAN))
                    .HARGA = CDec(grvCPPT_ResepNonRacikan.GetRowCellValue(i, colCPPT_HARGANONRACIKAN))
                    .TOTAL_PAKET = CDec(grvCPPT_ResepNonRacikan.GetRowCellValue(i, colCPPT_TOTAL_PAKETNONRACIKAN))
                    .TOTAL_NONPAKET = CDec(grvCPPT_ResepNonRacikan.GetRowCellValue(i, colCPPT_TOTAL_NONPAKETNONRACIKAN))
                    .TOTAL = CDec(grvCPPT_ResepNonRacikan.GetRowCellValue(i, colCPPT_TOTALNONRACIKAN))
                    .ROMAWI = oRME.IntegerToRoman(CInt(grvCPPT_ResepNonRacikan.GetFocusedRowCellValue(colCPPT_JUMLAHNONRACIKAN)))
                    .REMARKS_DOKTER = IIf(String.IsNullOrEmpty(grvCPPT_ResepNonRacikan.GetRowCellValue(i, colCPPT_REMARKS_DOKTERNONRACIKAN)), "", grvCPPT_ResepNonRacikan.GetRowCellValue(i, colCPPT_REMARKS_DOKTERNONRACIKAN))
                    .KDITEM = grvCPPT_ResepNonRacikan.GetRowCellValue(i, colCPPT_KDITEMNONRACIKAN)
                    .ISKRONIS = CBool(grvCPPT_ResepNonRacikan.GetRowCellValue(i, colCPPT_ISKRONISNONRACIKAN))
                    .ISALKES = CBool(grvCPPT_ResepNonRacikan.GetRowCellValue(i, colCPPT_ISALKESNONRACIKAN))
                    .KDUOM = grvCPPT_ResepNonRacikan.GetRowCellValue(i, colCPPT_KDUOMTINDAKAN)
                    .KDSIGNA = grvCPPT_ResepNonRacikan.GetRowCellValue(i, colCPPT_KDSIGNANONRACIKAN)
                    .QTY_PERUBAHAN = 0
                    .REMARKS_FARMASI = ""
                    .KDUSER = sUserID
                    .ISBACA = IIf(String.IsNullOrEmpty(grvCPPT_ResepNonRacikan.GetRowCellValue(i, colCPPT_ISBACANONRACIKAN)), "0", grvCPPT_ResepNonRacikan.GetRowCellValue(i, colCPPT_ISBACANONRACIKAN))
                End With

                arrDetail.Add(dsDetail)
            Next

            Dim arrDetailObatRacikan = oGrouperDataCppt.GetStructureDetailRacikanList
            For i As Integer = 0 To grvCPPT_ResepRacikan.RowCount - 2
                Dim dsDetail = oGrouperDataCppt.GetStructureDetailRacikan
                With dsDetail
                    .DATECREATED = ds.DATECREATED
                    .DATEUPDATED = ds.DATEUPDATED
                    .KDCPPT = ds.KDCPPT
                    .SEQ = i
                    .KDITEM = grvCPPT_ResepRacikan.GetRowCellValue(i, colCPPT_KDITEMRACIKAN)
                    Dim dsItem = oItem.GetData(grvCPPT_ResepNonRacikan.GetRowCellValue(i, colCPPT_KDITEMRACIKAN))
                    If dsItem IsNot Nothing Then
                        .NAMAOBAT = dsItem.NMITEM2
                    Else
                        .NAMAOBAT = ""
                    End If
                    .KDUOM = grvCPPT_ResepRacikan.GetRowCellValue(i, colCPPT_KDUOMRACIKAN)
                    Dim dsUom = oUom.GetData(grvCPPT_ResepNonRacikan.GetRowCellValue(i, colCPPT_KDUOMRACIKAN))
                    If dsUom IsNot Nothing Then
                        .SATUAN = dsUom.MEMO
                    Else
                        .SATUAN = ""
                    End If
                    .SIGNA = IIf(String.IsNullOrEmpty(grvCPPT_ResepRacikan.GetRowCellValue(i, colCPPT_SIGNARACIKAN)), "", grvCPPT_ResepRacikan.GetRowCellValue(i, colCPPT_SIGNARACIKAN))
                    .PERMINTAAN = IIf(String.IsNullOrEmpty(grvCPPT_ResepRacikan.GetRowCellValue(i, colCPPT_PERMINTAANRACIKAN)), "", grvCPPT_ResepRacikan.GetRowCellValue(i, colCPPT_PERMINTAANRACIKAN))
                    .JUMLAH = grvCPPT_ResepRacikan.GetRowCellValue(i, colCPPT_JUMLAHRACIKAN)
                    .HARGA = CDec(grvCPPT_ResepRacikan.GetRowCellValue(i, colCPPT_HARGARACIKAN))
                    .TOTAL = CDec(grvCPPT_ResepRacikan.GetRowCellValue(i, colCPPT_TOTALRACIKAN))
                    .REMARKS = grvCPPT_ResepRacikan.GetRowCellValue(i, colCPPT_REMARKSRACIKAN)
                    .KDUSER = sUserID
                    .ISBACA = IIf(String.IsNullOrEmpty(grvCPPT_ResepRacikan.GetRowCellValue(i, colCPPT_ISBACARACIKAN)), "0", grvCPPT_ResepRacikan.GetRowCellValue(i, colCPPT_ISBACARACIKAN))
                End With
                arrDetailObatRacikan.Add(dsDetail)
            Next

            Dim listPlanning As New List(Of String)
            Dim listTindakanLab As New List(Of String)
            Dim listTindakanRad As New List(Of String)
            Dim listTindakanLainnya As New List(Of String)
            Dim listTindakanTerapiNonRacikan As New List(Of String)
            Dim listTindakanTerapiRacikan As New List(Of String)

            For i As Integer = 0 To grvCPPT_Tindakan.RowCount - 2
                Dim dsItem = oItem.GetData(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_KDITEMTINDAKAN))
                If dsItem IsNot Nothing Then
                    If dsItem.M_ITEM_L3.MEMO = "LABORATORIUM" Then
                        listTindakanLab.Add(dsItem.NMITEM2 & " " & IIf(String.IsNullOrEmpty(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)), "", grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)))
                    ElseIf dsItem.M_ITEM_L3.MEMO = "BANK DARAH" Then
                        listTindakanLab.Add(dsItem.NMITEM2 & " " & IIf(String.IsNullOrEmpty(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)), "", grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)))
                    ElseIf dsItem.M_ITEM_L3.MEMO = "RADIOLOGI" Then
                        listTindakanRad.Add(dsItem.NMITEM2 & " " & IIf(String.IsNullOrEmpty(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)), "", grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)))
                        'Else
                        '    listTindakanLainnya.Add(dsItem.NMITEM2 & " " & IIf(String.IsNullOrEmpty(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)), "", grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)))
                    End If
                End If
            Next

            If listTindakanLab.Count > 0 Then
                listPlanning.Add(String.Join(vbCrLf, listTindakanLab.ToArray))
            End If
            If listTindakanRad.Count > 0 Then
                listPlanning.Add(String.Join(vbCrLf, listTindakanRad.ToArray))
            End If
            If listTindakanLainnya.Count > 0 Then
                listPlanning.Add(String.Join(vbCrLf, listTindakanLainnya.ToArray))
            End If

            'If chkCPPT_TindakLanjutPulang.Checked = True Then
            '    listPlanning.Add("Pulang")
            'End If
            'If chkCPPT_TindakLanjutRawat.Checked = True Then
            '    listPlanning.Add("Rawat")
            'End If
            'If chkCPPT_TindakLanjutKonsulKe.Checked = True Then
            '    listPlanning.Add("Konsul Ke " & txtCPPT_KonsulKe.Text)
            'End If
            'If chkCPPT_TindakLanjutRujukKe.Checked = True Then
            '    listPlanning.Add("Rujuk Ke " & txtCPPT_KonsulRujukKe.Text)
            'End If

            For Each xloop In arrDetail
                listTindakanTerapiNonRacikan.Add(xloop.NAMAOBAT & " " & xloop.SATUAN & " " & xloop.SIGNA & " " & xloop.CARAPAKAI & " No " & xloop.ROMAWI & " " & xloop.REMARKS_DOKTER)
            Next

            For Each xloop In arrDetailObatRacikan
                listTindakanTerapiRacikan.Add(xloop.SEQ + 1 & ". " & xloop.NAMAOBAT & " " & xloop.SATUAN & " " & xloop.SIGNA & " " & xloop.PERMINTAAN & " " & xloop.REMARKS)
            Next

            If listTindakanTerapiNonRacikan.Count > 0 Then
                listPlanning.Add(String.Join(vbCrLf, listTindakanTerapiNonRacikan.ToArray))
            End If

            If listTindakanTerapiRacikan.Count > 0 Then
                listPlanning.Add(String.Join(vbCrLf, listTindakanTerapiRacikan.ToArray))
            End If

            If txtCPPT_CATATAN.Text <> "" Then
                listPlanning.Add("Tindak Lanjut : " & vbCrLf & txtCPPT_CATATAN.Text)
            Else
                listPlanning.Add("Tindak Lanjut : " & grdKDDAFTAR_L4.Text)
            End If
            If txtCPPT_Alasan.Text <> "" Then
                listPlanning.Add("Alasan : " & vbCrLf & (txtCPPT_Alasan.Text & " " & Alasan).Trim)
            End If

            ds.PLANNING_TEXT = String.Join(vbCrLf, listPlanning.ToArray)

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    txtCPPT_Kode.Text = oGrouperDataCppt.InsertData(ds, arrDetailDiagnosa, arrDetailTindakan, arrDetail, arrDetailObatRacikan, Nothing)
                    If txtCPPT_Kode.Text = "" Then
                        fn_Save = False
                    Else
                        fn_Save = True
                    End If
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oGrouperDataCppt.UpdateData(ds, arrDetailDiagnosa, arrDetailTindakan, arrDetail, arrDetailObatRacikan, Nothing)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

            If fn_Save = True Then
                Dim oAdmision As New Admission.clsPendaftaran_Kunjungan
                Dim dsIdentitas = oAdmision.GetDatabyIdentitas(txtKDIDENTITAS.Text)

                If dsIdentitas IsNot Nothing Then
                    Dim dsKunjungan = oAdmision.GetData(dsIdentitas.KDKUNJUNGAN)
                    If dsKunjungan IsNot Nothing Then
                        If dsKunjungan.S_PENDAFTARAN_H.CATEGORY = 0 Then
                            fn_SaveTranskasiPoli(ds.KDCPPT, dsKunjungan.S_PENDAFTARAN_H.CATEGORY, dsKunjungan.KDKUNJUNGAN, dsKunjungan.KDDOCTOR, dsKunjungan.KDDEPARTMENT)
                        End If
                    End If

                    oAdmision.UpdateDokterKunjunganIdentitas(dsIdentitas.KDKUNJUNGAN, grdCPPT_KDDOCTOR.EditValue, grdCPPT_KDDOCTOR.Text)
                    oAdmision.UpdateDokterKunjungan(dsIdentitas.KDKUNJUNGAN, grdCPPT_KDDOCTOR.EditValue)

                    If dsKunjungan.S_PENDAFTARAN_H.CATEGORY = 0 Then
                        If dsKunjungan.S_PENDAFTARAN_H.M_DEPARTMENT.VCLAIM_KODEPOLI <> "IGD" Then
                            Dim oAntrian As New SettingAntrian.clsSetAntrian
                            Dim dsTaskId = oAntrian.GetDataBySaveWaktuTunggu(dsKunjungan.S_PENDAFTARAN_H.KODEBOOKING, 4)

                            If dsTaskId IsNot Nothing Then
                                frmErmList.fn_TaskID(dsKunjungan.S_PENDAFTARAN_H.KODEBOOKING, 5)
                            End If
                        End If
                    End If
                End If

                Dim oOrder As New Digital.clsR_Order

                If arrDetail.Count > 0 Then
                    Try
                        Dim dsNomorRefrence = oOrder.GetDataNoReference(ds.KDCPPT, "ORDER FARMASI")
                        Dim NoOrder As String = String.Empty
                        Dim StatusOrder As String = String.Empty
                        Dim JenisOrder As String = String.Empty

                        If dsNomorRefrence IsNot Nothing Then
                            NoOrder = dsNomorRefrence.KDORDER
                            StatusOrder = "UPDATE"
                            JenisOrder = dsNomorRefrence.JENISORDER
                        Else
                            StatusOrder = "TAMBAH"
                            If sCategory = 0 Then
                                JenisOrder = "FARRJ"
                            Else
                                JenisOrder = "FARRI"
                            End If
                        End If

                        Dim dsOrder = oOrder.GetStructureHeader
                        With dsOrder
                            .DATECREATED = ds.DATECREATED
                            .DATEUPDATED = ds.DATEUPDATED
                            .TANGGALORDER = ds.DATE
                            .JENISORDER = JenisOrder
                            .KDORDER = NoOrder
                            .KDIDENTITAS = ds.KDIDENTITAS
                            .NOMORREFERENCE = ds.KDCPPT
                            .USERORDER = ds.KDUSER
                            .STATUS = StatusOrder
                            .MEMO = "ORDER FARMASI"
                        End With

                        If NoOrder = "" Then
                            oOrder.InsertData(dsOrder)
                        Else
                            oOrder.UpdateData(dsOrder)
                        End If
                    Catch oErr As Exception
                        MsgBox("Order" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                End If

                If listTindakanLab.Count > 0 Then
                    Try
                        Dim dsNomorRefrence = oOrder.GetDataNoReference(ds.KDCPPT, "ORDER LABORATORIUM")
                        Dim NoOrder As String = String.Empty
                        Dim StatusOrder As String = String.Empty
                        Dim JenisOrder As String = String.Empty

                        If dsNomorRefrence IsNot Nothing Then
                            NoOrder = dsNomorRefrence.KDORDER
                            If dsNomorRefrence.STATUS = "TAMBAH" Then
                                StatusOrder = "UPDATE"
                            Else
                                StatusOrder = dsNomorRefrence.STATUS
                            End If

                            JenisOrder = dsNomorRefrence.JENISORDER
                        Else
                            StatusOrder = "TAMBAH"
                            If sCategory = 0 Then
                                JenisOrder = "LABRJ"
                            Else
                                JenisOrder = "LABRI"
                            End If
                        End If

                        Dim dsOrder = oOrder.GetStructureHeader
                        With dsOrder
                            .DATECREATED = ds.DATECREATED
                            .DATEUPDATED = ds.DATEUPDATED
                            .TANGGALORDER = ds.DATE
                            .JENISORDER = JenisOrder
                            .KDORDER = NoOrder
                            .KDIDENTITAS = ds.KDIDENTITAS
                            .NOMORREFERENCE = ds.KDCPPT
                            .USERORDER = ds.KDUSER
                            .STATUS = StatusOrder
                            .MEMO = "ORDER LABORATORIUM"
                        End With

                        If NoOrder = "" Then
                            oOrder.InsertData(dsOrder)
                        Else
                            oOrder.UpdateData(dsOrder)
                        End If
                    Catch oErr As Exception
                        MsgBox("Order" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                End If

                If listTindakanRad.Count > 0 Then
                    Try
                        Dim dsNomorRefrence = oOrder.GetDataNoReference(ds.KDCPPT, "ORDER RADIOLOGI")
                        Dim NoOrder As String = String.Empty
                        Dim StatusOrder As String = String.Empty
                        Dim JenisOrder As String = String.Empty

                        If dsNomorRefrence IsNot Nothing Then
                            NoOrder = dsNomorRefrence.KDORDER
                            If dsNomorRefrence.STATUS = "TAMBAH" Then
                                StatusOrder = "UPDATE"
                            Else
                                StatusOrder = dsNomorRefrence.STATUS
                            End If
                            JenisOrder = dsNomorRefrence.JENISORDER
                        Else
                            StatusOrder = "TAMBAH"
                            If sCategory = 0 Then
                                JenisOrder = "RADRJ"
                            Else
                                JenisOrder = "RADRI"
                            End If
                        End If

                        Dim dsOrder = oOrder.GetStructureHeader
                        With dsOrder
                            .DATECREATED = ds.DATECREATED
                            .DATEUPDATED = ds.DATEUPDATED
                            .TANGGALORDER = ds.DATE
                            .JENISORDER = JenisOrder
                            .KDORDER = NoOrder
                            .KDIDENTITAS = ds.KDIDENTITAS
                            .NOMORREFERENCE = ds.KDCPPT
                            .USERORDER = ds.KDUSER
                            .STATUS = StatusOrder
                            .MEMO = "ORDER RADIOLOGI"
                        End With

                        If NoOrder = "" Then
                            oOrder.InsertData(dsOrder)
                        Else
                            oOrder.UpdateData(dsOrder)
                        End If
                    Catch oErr As Exception
                        MsgBox("Order" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                End If
            End If
        Catch oErr As Exception
            fn_Save = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_SaveTranskasiPoli(ByVal sNoId As String, ByVal sCategoryBilling As Integer, ByVal KDKUNJUNGAN As String, ByVal KDDOCTOR As String, ByVal KDDEPARTMENT As String) As Boolean
        Try
            ' ***** HEADER *****
            Dim oItem As New Reference.clsItem
            Dim Total As Decimal = 0

            For i As Integer = 0 To grvCPPT_Tindakan.RowCount - 2
                Dim dsItem = oItem.GetData(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_KDITEMTINDAKAN))
                If dsItem IsNot Nothing Then
                    If dsItem.M_ITEM_L2.MEMO.Contains("RADIOLOGI") Then

                    ElseIf dsItem.M_ITEM_L2.MEMO.Contains("LABORATORIUM") Then

                    ElseIf dsItem.M_ITEM_L2.MEMO.Contains("BANK DARAH") Then

                    Else
                        Total += CDec(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_TOTALTINDAKAN))
                    End If
                End If
            Next

            Dim ds = oSalesOrderTransaksi.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oSalesOrderTransaksi.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDSOTRANSAKSI = sNoId
                .CATEGORY = sCategoryBilling
                .DATE = deCPPT_Tanggal.DateTime
                .KDKUNJUNGAN = KDKUNJUNGAN
                .KDWAREHOUSE = ""
                .ISBHP = False
                .SUBTOTAL = CDec(Total)
                .DISCOUNT = CDec(0)
                .TAX = CDec(0)
                .GRANDTOTAL = CDec(Total)
                .PAYAMOUNT = CDec(0)
                .MEMO = "AUTO"
                Try
                    .KDUSER = oSalesOrderTransaksi.GetData(sNoId).KDUSER
                Catch ex As Exception
                    .KDUSER = sUserID
                End Try
                '.KDSHIFT = sSHIFT
                .KDSHIFT = sSHIFT
                .KDDOCTOR = KDDOCTOR
                .TUSLAH = CDec(0)

                Try
                    .KDCPPT = oSalesOrderTransaksi.GetData(sNoId).KDCPPT
                Catch ex As Exception
                    .KDCPPT = ""
                End Try
                Try
                    .KDORDER = oSalesOrderTransaksi.GetData(sNoId).KDORDER
                Catch ex As Exception
                    .KDORDER = ""
                End Try
            End With

            ' ***** DETIL *****
            Dim arrDetail = oSalesOrderTransaksi.GetStructureDetailList
            For i As Integer = 0 To grvCPPT_Tindakan.RowCount - 2
                Dim dsDetail = oSalesOrderTransaksi.GetStructureDetail
                With dsDetail
                    .DATECREATED = deCPPT_Tanggal.DateTime
                    .DATEUPDATED = Now
                    .SEQ = i
                    .KDSOTRANSAKSI = ds.KDSOTRANSAKSI
                    .KDITEM = grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_KDITEMTINDAKAN)
                    .KDUOM = grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_KDUOMTINDAKAN)
                    .KDCARAPAKAI = oItem.DefaultItem_CaraPakai
                    .KDSIGNA = oItem.DefaultItem_Signa
                    .QTY = CDec(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_JUMLAHTINDAKAN))
                    .ISRACIK = CBool(False)
                    .PRICE = CDec(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_HARGATINDAKAN))
                    .SUBTOTAL = CDec(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_TOTALTINDAKAN))
                    .DISCOUNT = CDec(0)
                    .GRANDTOTAL = CDec(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_TOTALTINDAKAN))
                    .KDDOCTOR = KDDOCTOR
                    .KDDEPARTMENT = KDDEPARTMENT
                    .REMARKS = IIf(String.IsNullOrEmpty(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)), "-", grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN))
                    .ISCETAKETIKET = False
                    .KDUSER = sUserID
                    .GROUPRACIK = 0
                End With

                Dim dsItem = oItem.GetData(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_KDITEMTINDAKAN))
                If dsItem IsNot Nothing Then
                    If dsItem.M_ITEM_L3.MEMO = "RADIOLOGI" Then
                    ElseIf dsItem.M_ITEM_L3.MEMO = "LABORATORIUM" Then
                    ElseIf dsItem.M_ITEM_L3.MEMO = "BANK DARAH" Then
                    Else
                        arrDetail.Add(dsDetail)
                    End If
                End If
            Next

            If arrDetail.Count > 0 Then
                Dim dsCek = oSalesOrderTransaksi.GetData(sNoId)

                If dsCek Is Nothing Then
                    Try
                        Dim sKDSOTRANSAKSI = oSalesOrderTransaksi.InsertData(ds, arrDetail)
                        If sKDSOTRANSAKSI = "" Then
                            fn_SaveTranskasiPoli = False
                        Else
                            fn_SaveTranskasiPoli = True
                        End If
                    Catch oErr As Exception
                        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                Else
                    Try
                        fn_SaveTranskasiPoli = oSalesOrderTransaksi.UpdateData(ds, arrDetail)
                    Catch oErr As Exception
                        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                End If
            End If

        Catch oErr As Exception
            MsgBox("Simpan Transaksi" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveTranskasiPoli = False
        End Try
    End Function
#End Region
#Region "Command Button"
    Private Sub btnSaveClosee_Click() Handles btnSaveClosee.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            oFormMode = FORM_MODE.FORM_MODE_EDIT
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
        End If
    End Sub
#End Region
#Region "Lookup"
    Private Sub fn_LoadDPJP()
        Dim oDPJP As New Reference.clsDoctor
        Try
            grdCPPT_KDDOCTOR.Properties.DataSource = oDPJP.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdCPPT_KDDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdCPPT_KDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDaftar4()
        Dim oDAFTAR_L4 As New Reference.clsDaftar_L4
        Try
            grdKDDAFTAR_L4.Properties.DataSource = oDAFTAR_L4.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDAFTAR_L4.Properties.ValueMember = "KDDAFTAR_L4"
            grdKDDAFTAR_L4.Properties.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Sub fn_LoadDiagnosa(ByVal Diagnosa As String)
        Try
            Dim jsonDecode = JObject.Parse(oGetGrouper.fn_Pencariandiagnosa(sEklaim_Url, sEklaim_Generate, Diagnosa))
            Dim sDataDuplicate As String = String.Empty
            Dim smessage As String = String.Empty

            sDataDuplicate = jsonDecode("metadata")("code").ToString
            smessage = jsonDecode("metadata")("message").ToString

            If sDataDuplicate = "200" Then
                Dim table As DataTable

                table = New DataTable("M_DIAGNOSA")
                table.Columns.Add("nama")
                table.Columns.Add("kode")

                For Each item In jsonDecode("response")("data")
                    table.Rows.Add(New String() {item(0), item(1)})
                Next

                grdCariDiagnosa.Properties.DataSource = table
                grdCariDiagnosa.Properties.ValueMember = "kode"
                grdCariDiagnosa.Properties.DisplayMember = "nama"

                grvCariDiagnosa.BestFitColumns()
                grdCariDiagnosa.ShowPopup()
            Else
                MsgBox("Pencarian Diagnosa" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Information, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox("Pencarian Diagnosa: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
        'If grdCPPT_Profesi.Text.Contains("DOKTER") Then
        '    Try
        '        Dim jsonDecode = JObject.Parse(oGetGrouper.fn_Pencariandiagnosa(sURLKLAIM, sREMARKS, Diagnosa))
        '        Dim sDataDuplicate As String = String.Empty
        '        Dim smessage As String = String.Empty

        '        sDataDuplicate = jsonDecode("metadata")("code").ToString
        '        smessage = jsonDecode("metadata")("message").ToString

        '        If sDataDuplicate = "200" Then
        '            Dim table As DataTable

        '            table = New DataTable("M_DIAGNOSA")
        '            table.Columns.Add("nama")
        '            table.Columns.Add("kode")

        '            For Each item In jsonDecode("response")("data")
        '                table.Rows.Add(New String() {item(0), item(1)})
        '            Next

        '            grdCariDiagnosa.Properties.DataSource = table
        '            grdCariDiagnosa.Properties.ValueMember = "kode"
        '            grdCariDiagnosa.Properties.DisplayMember = "nama"

        '            grvCariDiagnosa.BestFitColumns()
        '            grdCariDiagnosa.ShowPopup()
        '        Else
        '            MsgBox("Pencarian Diagnosa" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Information, Me.Text)
        '        End If
        '    Catch oErr As Exception
        '        MsgBox("Pencarian Diagnosa: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    End Try
        'Else
        '    Dim oItemDiagnosaPerawat As New Master.clsItemDiagnosaPerawat

        '    Try
        '        Dim dsList = From x In oItemDiagnosaPerawat.GetDataDetailByName(Diagnosa)
        '                     Select nama = x.DESCRIPTION, kode = x.KDITEMDIAGNOSAPERAWAT

        '        grdCariDiagnosa.Properties.DataSource = dsList.ToList()
        '        grdCariDiagnosa.Properties.ValueMember = "kode"
        '        grdCariDiagnosa.Properties.DisplayMember = "nama"

        '        grvCariDiagnosa.BestFitColumns()
        '        grdCariDiagnosa.ShowPopup()
        '    Catch oErr As Exception
        '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    End Try
        'End If
    End Sub
    Private Sub fn_LoadCPPT_TemplateTindakan()
        Dim oGrouperDataCppt_Template As New Template.clsCPPT_TemplateProsedur
        Try
            grdCPPT_TemplateTindakan.Properties.DataSource = oGrouperDataCppt_Template.GetData.Where(Function(x) x.KDUSER = sUserID And x.ISDELETE = True).ToList()
            grdCPPT_TemplateTindakan.Properties.ValueMember = "KDTEMPLATE"
            grdCPPT_TemplateTindakan.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDPROFESI()
        Dim oPROFESI As New Reference.clsProfesi
        Try
            grdCPPT_Profesi.Properties.DataSource = oPROFESI.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdCPPT_Profesi.Properties.ValueMember = "KDPROFESI"
            grdCPPT_Profesi.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadITEM_L2()
        Dim oItem_L2 As New Reference.clsItem_L2
        Try
            grdITEM_L2.Properties.DataSource = oItem_L2.GetData.Where(Function(x) x.ISACTIVE = True And Not x.MEMO.Contains("FARMASI") And x.MEMO <> "DGCARE").ToList()
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
            'SQL &= ",NMITEM2 = (SELECT CASE D.MEMO WHEN 'NON KELAS' THEN A.NMITEM2 ELSE A.NMITEM2 + ' ' + D.MEMO END) "
            SQL &= ",A.NMITEM2 "
            SQL &= ",KELOMPOK = B.MEMO "
            'SQL &= ",PRICE = C.PRICESALESSTANDARD "
            SQL &= "FROM "
            SQL &= "M_ITEM A "
            SQL &= "INNER JOIN M_ITEM_L2 B "
            SQL &= "ON A.KDITEM_L2 = B.KDITEM_L2 "
            'SQL &= "INNER JOIN M_ITEM_UOM C "
            'SQL &= "ON A.KDITEM = C.KDITEM "
            'SQL &= "INNER JOIN M_UOM D "
            'SQL &= "ON C.KDUOM = D.KDUOM "
            SQL &= "WHERE A.ISSTOK = 0 "
            SQL &= "AND A.ISACTIVE = 1 "
            SQL &= "AND B.MEMO <> 'DGCARE' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ITEM_ALL")

            grdSemuaTindakan.Properties.DataSource = ds.Tables("ITEM_ALL")
            grdSemuaTindakan.Properties.ValueMember = "KDITEM"
            grdSemuaTindakan.Properties.DisplayMember = "NMITEM2"

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
            Dim dsList = oUOM.GetData()

            grdCPPT_KDUOMNONRACIKAN.DataSource = dsList.ToList()
            grdCPPT_KDUOMNONRACIKAN.ValueMember = "KDUOM"
            grdCPPT_KDUOMNONRACIKAN.DisplayMember = "MEMO"

            grdCPPT_KDUOMRACIKAN.DataSource = dsList.ToList()
            grdCPPT_KDUOMRACIKAN.ValueMember = "KDUOM"
            grdCPPT_KDUOMRACIKAN.DisplayMember = "MEMO"

            grdCPPT_KDUOMTINDAKAN.DataSource = dsList.ToList()
            grdCPPT_KDUOMTINDAKAN.ValueMember = "KDUOM"
            grdCPPT_KDUOMTINDAKAN.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDSIGNA()
        Dim oSigna As New Reference.clsSigna
        Try
            grdCPPT_KDSIGNANONRACIKAN.DataSource = oSigna.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdCPPT_KDSIGNANONRACIKAN.ValueMember = "KDSIGNA"
            grdCPPT_KDSIGNANONRACIKAN.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDCARAPAKAI()
        Dim oCaraPakai As New Reference.clsCaraPakai
        Try
            grdCPPT_KDCARAPAKAINONRACIKAN.DataSource = oCaraPakai.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdCPPT_KDCARAPAKAINONRACIKAN.ValueMember = "KDCARAPAKAI"
            grdCPPT_KDCARAPAKAINONRACIKAN.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDITEMTARIF()
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
            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                SQL &= "AND A.ISACTIVE = 1 "
            End If

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
    Private Sub fn_LoadKDITEMOBAT()
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
            SQL &= ",A.NMITEM1 "
            SQL &= ",A.NMITEM2 "
            SQL &= ",HARGA = B.PRICESALESSTANDARD "
            SQL &= ",SATUAN = C.MEMO "
            If sCategory = 0 Then
                SQL &= ",STOK = ISNULL((SELECT SUM(AMOUNT) FROM M_ITEM_WAREHOUSE WHERE B.KDITEM = KDITEM AND B.KDUOM = KDUOM AND KDWAREHOUSE = 'WAREHOUSE_0000000008' GROUP BY KDITEM) ,0) "
            Else
                SQL &= ",STOK = ISNULL((SELECT SUM(AMOUNT) FROM M_ITEM_WAREHOUSE WHERE B.KDITEM = KDITEM AND B.KDUOM = KDUOM GROUP BY KDITEM) ,0) "
            End If
            SQL &= "FROM M_ITEM A "
            SQL &= "INNER JOIN M_ITEM_UOM B "
            SQL &= "ON A.KDITEM = B.KDITEM "
            SQL &= "INNER JOIN M_UOM C "
            SQL &= "ON B.KDUOM = C.KDUOM "
            SQL &= "WHERE "
            SQL &= "B.RATE = 1 "
            SQL &= "AND A.ISSTOK = 1 "
            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                SQL &= "AND A.ISACTIVE = 1 "
            End If

            'SQL = "SELECT "
            'SQL &= "A.KDITEM "
            'SQL &= ",A.NMITEM2 "
            'SQL &= "FROM "
            'SQL &= "M_ITEM A "
            'SQL &= "WHERE A.ISSTOK = 1 "
            'SQL &= "AND A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ITEM")

            grdCPPT_KDITEMNONRACIKAN.DataSource = ds.Tables("ITEM")
            grdCPPT_KDITEMNONRACIKAN.ValueMember = "KDITEM"
            grdCPPT_KDITEMNONRACIKAN.DisplayMember = "NMITEM2"

            grdCPPT_KDITEMRACIKAN.DataSource = ds.Tables("ITEM")
            grdCPPT_KDITEMRACIKAN.ValueMember = "KDITEM"
            grdCPPT_KDITEMRACIKAN.DisplayMember = "NMITEM2"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    'Private Sub QueryCPPT_LoadKDITEMSearch()
    '    Try
    '        Dim oConn As New SqlConnection
    '        Dim oComm As New SqlCommand
    '        Dim da As SqlDataAdapter
    '        Dim ds As New DataSet
    '        Dim SQL As String

    '        Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

    '        oConn = New SqlConnection(sConn)
    '        If oConn.State = ConnectionState.Closed Then
    '            oConn.Open()
    '        End If

    '        SQL = "SELECT "
    '        SQL &= "A.KDITEM "
    '        SQL &= ",NMITEM2 = (SELECT CASE D.MEMO WHEN 'NON KELAS' THEN A.NMITEM2 ELSE A.NMITEM2 + ' ' + D.MEMO END) "
    '        SQL &= ",KELOMPOK = B.MEMO "
    '        SQL &= ",PRICE = C.PRICESALESSTANDARD "
    '        SQL &= "FROM "
    '        SQL &= "M_ITEM A "
    '        SQL &= "INNER JOIN M_ITEM_L2 B "
    '        SQL &= "ON A.KDITEM_L2 = B.KDITEM_L2 "
    '        SQL &= "INNER JOIN M_ITEM_UOM C "
    '        SQL &= "ON A.KDITEM = C.KDITEM "
    '        SQL &= "INNER JOIN M_UOM D "
    '        SQL &= "ON C.KDUOM = D.KDUOM "
    '        SQL &= "WHERE A.ISSTOK = 0 "
    '        SQL &= "AND A.ISACTIVE = 1 "
    '        SQL &= "AND B.MEMO <> 'DGCARE' "

    '        oComm.Connection = oConn
    '        oComm.CommandText = SQL
    '        oComm.CommandTimeout = 120
    '        oComm.CommandType = CommandType.Text

    '        da = New SqlDataAdapter(oComm)
    '        da.Fill(ds, "ITEM_ALL")

    '        grdKDITEMSearch.Properties.DataSource = ds.Tables("ITEM_ALL")
    '        grdKDITEMSearch.Properties.ValueMember = "KDITEM"
    '        grdKDITEMSearch.Properties.DisplayMember = "NMITEM2"

    '        If oConn.State = ConnectionState.Open Then
    '            oConn.Close()
    '        End If

    '    Catch oErr As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
#End Region
#Region "Event"
    Private Sub grdKDDAFTAR_L4_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDDAFTAR_L4.EditValueChanged
        If isLoad = True Then
            If grdKDDAFTAR_L4.Text = "KONTROL" Or grdKDDAFTAR_L4.Text = "RAWAT INAP" Then
                Dim oSkd As New Admission.clsSKD
                Dim oKunjungan As New Admission.clsPendaftaran_Kunjungan

                Dim dsPendaftaran = oKunjungan.GetDatabyIdentitas(txtKDIDENTITAS.Text)
                If dsPendaftaran IsNot Nothing Then
                    Dim dsKunjungan = oKunjungan.GetData(dsPendaftaran.KDKUNJUNGAN)

                    If dsKunjungan IsNot Nothing Then
                        Dim dsSKD = oSkd.GetDataPendaftaran(dsKunjungan.KDPENDAFTARAN)

                        If dsSKD IsNot Nothing Then
                            Dim frmSKD As New frmSKD
                            Try
                                frmSKD.fn_LoadNoPendaftaranPolidanDokter(IIf(grdKDDAFTAR_L4.Text = "KONTROL", 0, 1), dsPendaftaran.KDDEPARTMENT, dsPendaftaran.KDDOCTOR, dsKunjungan.S_PENDAFTARAN_H.KDCUSTOMER, dsKunjungan.KDPENDAFTARAN, "")
                                frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsSKD.KDSKD)
                                frmSKD.ShowDialog(Me)
                            Catch oErr As Exception
                                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                            Finally
                                If Not frmSKD Is Nothing Then frmSKD.Dispose()
                                frmSKD = Nothing
                            End Try
                        Else
                            Dim frmSKD As New frmSKD
                            Try
                                frmSKD.fn_LoadNoPendaftaranPolidanDokter(IIf(grdKDDAFTAR_L4.Text = "KONTROL", 0, 1), dsPendaftaran.KDDEPARTMENT, dsPendaftaran.KDDOCTOR, dsKunjungan.S_PENDAFTARAN_H.KDCUSTOMER, dsKunjungan.KDPENDAFTARAN, "")
                                frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
                                frmSKD.ShowDialog(Me)
                            Catch oErr As Exception
                                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                            Finally
                                If Not frmSKD Is Nothing Then frmSKD.Dispose()
                                frmSKD = Nothing
                            End Try
                        End If
                    End If
                Else
                    MsgBox("Maap Registrasi Tidak di Temukan", MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If
        End If
    End Sub
    Private Sub txtCPPT_CARIDIAGNOSA_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCPPT_CARIDIAGNOSA.KeyPress
        If Asc(e.KeyChar) = 13 Then
            fn_LoadDiagnosa(txtCPPT_CARIDIAGNOSA.Text)
            'txtCPPT_CARIDIAGNOSA.ResetText()
            'If grdCPPT_Profesi.Text <> "" Then
            '    fn_LoadDiagnosa(txtCPPT_CARIDIAGNOSA.Text)
            '    txtCPPT_CARIDIAGNOSA.ResetText()
            'Else
            '    MsgBox("Maap Profesi Masih Kosong", MsgBoxStyle.Exclamation, Me.Text)
            '    grdCPPT_Profesi.Focus()
            'End If
        End If
    End Sub
    Private Sub btnCPPT_CariICD10_Click(sender As Object, e As EventArgs) Handles btnCPPT_CariICD10.Click
        fn_LoadDiagnosa(txtCPPT_CARIDIAGNOSA.Text)
        'txtCPPT_CARIDIAGNOSA.ResetText()
        'If grdCPPT_Profesi.Text <> "" Then
        '    fn_LoadDiagnosa(txtCPPT_CARIDIAGNOSA.Text)
        '    txtCPPT_CARIDIAGNOSA.ResetText()
        'Else
        '    MsgBox("Maap Profesi Masih Kosong", MsgBoxStyle.Exclamation, Me.Text)
        '    grdCPPT_Profesi.Focus()
        'End If
    End Sub
    Private Sub grdCariDiagnosa_EditValueChanged(sender As Object, e As EventArgs) Handles grdCariDiagnosa.EditValueChanged
        If isLoad = True Then
            If grdCariDiagnosa.Text <> "" Then
                grvCPPT_Diagnosa.Focus()
                grvCPPT_Diagnosa.AddNewRow()
                grvCPPT_Diagnosa.SetFocusedRowCellValue(colCPPT_KDDIAGNOSA, grdCariDiagnosa.EditValue)
                grvCPPT_Diagnosa.SetFocusedRowCellValue(colCPPT_MEMODIAGNOSA, grdCariDiagnosa.EditValue & " - " & grdCariDiagnosa.Text)
                grvCPPT_Diagnosa.UpdateCurrentRow()

                txtCPPT_CARIDIAGNOSA.Focus()
            End If
        End If
    End Sub
    Private Sub grdCPPT_TemplateTindakan_EditValueChanged(sender As Object, e As EventArgs) Handles grdCPPT_TemplateTindakan.EditValueChanged
        If isLoad = True Then
            If grdCPPT_TemplateTindakan.Text <> "" Then
                Dim oGrouperDataCppt_Template As New Template.clsCPPT_TemplateProsedur

                For Each xloop In oGrouperDataCppt_Template.GetDataDetail(grdCPPT_TemplateTindakan.EditValue).OrderBy(Function(x) x.SEQ)
                    grvCPPT_Tindakan.Focus()
                    grvCPPT_Tindakan.AddNewRow()
                    grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN, xloop.KDITEM)
                    grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_ISBACA, "0")
                    grvCPPT_Tindakan.UpdateCurrentRow()
                Next
            End If
        End If
    End Sub
    Private Sub chkCPPT_AlergiTidak_CheckedChanged(sender As Object, e As EventArgs) Handles chkCPPT_AlergiTidak.CheckedChanged
        If isLoad = True Then
            If chkCPPT_AlergiTidak.Checked = True Then
                txtCPPT_AlergiYa.ResetText()
                chkCPPT_AlergiYa.Checked = False
            End If
        End If
    End Sub
    Private Sub chkCPPT_AlergiYa_CheckedChanged(sender As Object, e As EventArgs) Handles chkCPPT_AlergiYa.CheckedChanged
        If isLoad = True Then
            If chkCPPT_AlergiYa.Checked = True Then
                chkCPPT_AlergiTidak.Checked = False
            End If
        End If
    End Sub
    Private Sub grdKDITEMALL_DoubleClick(sender As Object, e As EventArgs) Handles grdKDITEMALL.DoubleClick
        If sRead = False Then
            grvCPPT_Tindakan.Focus()
            grvCPPT_Tindakan.AddNewRow()
            grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN, grvKDITEMALL.GetFocusedRowCellValue("KDITEM"))
            grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_ISBACA, "0")
            grvCPPT_Tindakan.UpdateCurrentRow()
            grvKDITEMALL.SetFocusedRowCellValue(colPilih, False)
        End If
    End Sub
    Private Sub grdITEM_L2_EditValueChanged(sender As Object, e As EventArgs) Handles grdITEM_L2.EditValueChanged
        If isLoad = True Then
            fn_LoadSearchKDITEM()
        End If
    End Sub
    Private Sub grdSemuaTindakan_EditValueChanged(sender As Object, e As EventArgs) Handles grdSemuaTindakan.EditValueChanged
        If isLoad = True Then
            If grdSemuaTindakan.Text <> "" Then
                grvCPPT_Tindakan.Focus()
                grvCPPT_Tindakan.AddNewRow()
                grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN, grdSemuaTindakan.EditValue)
                grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_ISBACA, "0")
                grvCPPT_Tindakan.UpdateCurrentRow()
            End If
        End If
    End Sub
    'Private Sub grdKDITEMSearch_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDITEMSearch.EditValueChanged
    '    If isLoad = True Then
    '        If grdKDITEMSearch.Text <> "" Then
    '            grvCPPT_Tindakan.Focus()
    '            grvCPPT_Tindakan.AddNewRow()
    '            grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN, grdKDITEMSearch.EditValue)
    '            grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_ISBACA, "0")
    '            grvCPPT_Tindakan.UpdateCurrentRow()
    '        End If
    '    End If
    'End Sub
#End Region
#Region "Comman Button"
    'Private Sub fn_CPPTTampilan(ByVal Cek As Integer)
    '    If Cek = 0 Then
    '        lblCPPT_Simpan.Text = ""
    '        txtCPPT_Kode.Text = "<--- AUTO --->"
    '        fn_CPPT_EmptyMe(0)
    '        If lblRM.Text <> "" Then
    '            QueryCPPT_LoadHistory(lblRM.Text)
    '        End If

    '        lCPPT_List.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
    '        lCPPT_Form.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    '        lCPPT_Form_1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    '        lCPPT_Form_2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    '        lCPPT_Form_3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    '        lCPPT_Form_4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    '        lCPPT_BukaList.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    '    ElseIf Cek = 1 Then
    '        lblCPPT_Simpan.Text = "F3-Simpan"

    '        lCPPT_List.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    '        lCPPT_Form.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
    '        lCPPT_Form_1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
    '        lCPPT_Form_2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
    '        lCPPT_Form_3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
    '        lCPPT_Form_4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
    '        lCPPT_BukaList.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
    '    End If
    'End Sub
    'Private Sub picCPPT_Simpan_Click() Handles picCPPT_Simpan.Click
    '    If picCPPT_Simpan.Enabled = True Then
    '        If lblCPPT_Simpan.Text = "" Then Exit Sub

    '        If fn_Validate() = False Then
    '            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
    '        Else
    '            If MsgBox("Apakah Yakin CPPT Akan di Simpan Jam " & Now.ToString("HH:mm"), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
    '            If fn_CPPT_Save(IIf(txtCPPT_Kode.Text = "<--- AUTO --->", True, False)) = False Then
    '                MsgBox("Gagal Simpan CPPT", MsgBoxStyle.Exclamation, Me.Text)
    '            Else
    '                XtraTabControlDokumenRawatJalanPDF_SelectedPageChanged()

    '                If cboTYPE.SelectedIndex = 1 Then
    '                    fn_TaskID(5, lblNorec.Text)
    '                End If
    '                fn_CPPTTampilan(0)
    '            End If
    '        End If
    '    End If
    'End Sub
    Private Sub btnResetGambar_Click(sender As Object, e As EventArgs) Handles btnResetGambar.Click
        sPicture = Nothing
        picCPPT_Gambar.Image = CType(My.Resources.ResourceManager.GetObject("image1"), Image)
    End Sub
    Private Sub btnCPPT_AmbilGambar_Click(sender As Object, e As EventArgs) Handles btnCPPT_AmbilGambar.Click
        Dim oSetKoneksi As New Brigging.clsSetKoneksi
        Dim sALAMATSIMPAN As String = String.Empty
        Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "SIMPANPDFCASMIX")
        If dsDataSetKoneksi IsNot Nothing Then
            If dsDataSetKoneksi.ALAMATWEB <> "" Then
                If Not IO.Directory.Exists(dsDataSetKoneksi.ALAMATWEB) Then
                    MsgBox("Alamat Gambar " & dsDataSetKoneksi.ALAMATWEB & " Tidak diTemukan", MsgBoxStyle.Exclamation, Me.Text)
                Else
                    Dim frmPopUp_Image As New frmPopUp_img
                    frmPopUp_Image.fn_LoadMe(picCPPT_Gambar.Image)
                    frmPopUp_Image.ShowDialog()

                    If sPicture IsNot Nothing Then
                        picCPPT_Gambar.Image = sPicture
                    End If
                End If
            Else
                MsgBox("Alamat Gambar Kosong", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Else
            MsgBox("Alamat Gambar Kosong", MsgBoxStyle.Exclamation, Me.Text)
        End If

        'MsgBox("Alamat Gambar Kosong", MsgBoxStyle.Exclamation, Me.Text)
        picCPPT_Gambar.Focus()
    End Sub
    'Private Sub btnCPPT_Kode_Click(sender As Object, e As EventArgs)
    '    fn_CPPTTampilan(0)
    'End Sub
    'Private Sub picCPPT_Add_Click(sender As Object, e As EventArgs) Handles picCPPT_Add.Click
    '    If lblkodegrouper.Text = "" Then
    '        MsgBox("Silahkan Pilih Dulu Pasien", MsgBoxStyle.Exclamation, Me.Text)
    '        fn_CPPTTampilan(0)
    '        Exit Sub
    '    End If

    '    fn_CPPTTampilan(1)

    '    txtCPPT_Kode.Text = "<--- AUTO --->"
    '    fn_CPPT_ChangeFormState()
    'End Sub
    'Private Sub picCPPT_Update_Click(sender As Object, e As EventArgs) Handles picCPPT_Update.Click
    '    If grvCPPT_List.GetFocusedRowCellValue("KDCPPT") Is Nothing Then
    '        Exit Sub
    '    End If

    '    If grvCPPT_List.GetFocusedRowCellValue("KDUSER") <> sUserID Then
    '        MsgBox("Maap Tidak dapat Rubah Silahkan Hubungi User " & grvCPPT_List.GetFocusedRowCellValue("KDUSER"), MsgBoxStyle.Exclamation, Me.Text)
    '        fn_CPPTTampilan(0)
    '        Exit Sub
    '    End If

    '    fn_CPPTTampilan(1)

    '    txtCPPT_Kode.Text = grvCPPT_List.GetFocusedRowCellValue("KDCPPT")
    '    fn_CPPT_ChangeFormState()
    'End Sub
    'Private Sub picCPPT_Delete_Click(sender As Object, e As EventArgs) Handles picCPPT_Delete.Click
    '    If grvCPPT_List.GetFocusedRowCellValue("KDCPPT") Is Nothing Then
    '        Exit Sub
    '    End If

    '    If grvCPPT_List.GetFocusedRowCellValue("KDUSER") <> sUserID Then
    '        MsgBox("Maap Tidak dapat Rubah Silahkan Hubungi User " & grvCPPT_List.GetFocusedRowCellValue("KDUSER"), MsgBoxStyle.Exclamation, Me.Text)
    '        fn_CPPTTampilan(0)
    '        Exit Sub
    '    End If

    '    If MsgBox("Apakah Yakin Data Akan di Hapus", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

    '    Dim frmPesanDelete As New frmPesanDelete
    '    frmPesanDelete.ShowDialog(Me)

    '    If sPesanHapus <> "XXXXXBATALXXXXX" Then
    '        Dim oDelete As New Setting.clsDelete

    '        If oDelete.InsertData("CPPT", sUserID, sPesanHapus & " " & Now.ToString("dd-MM-yyyy HH:mm:ss"), grvCPPT_List.GetFocusedRowCellValue("KDCPPT")) = False Then
    '            'fn_DeleteData = False
    '        End If

    '        oGrouperDataCppt.DeleteData(grvCPPT_List.GetFocusedRowCellValue("KDCPPT"), sPesanHapus)
    '        fn_CPPTTampilan(0)
    '    End If
    'End Sub
    'Private Sub picCPPT_Refresh_Click(sender As Object, e As EventArgs) Handles picCPPT_Refresh.Click
    '    fn_CPPTTampilan(0)
    'End Sub
    'Private Sub picCPPT_Aksi_Click(sender As Object, e As EventArgs)
    '    If grvCPPT_List.GetFocusedRowCellValue("KDCPPT") Is Nothing Then
    '        MsgBox("Silahkan Pilih Kode CPPT", MsgBoxStyle.Exclamation, Me.Text)
    '        Exit Sub
    '    End If

    '    If grvCPPT_List.GetFocusedRowCellValue("PROFESI") <> "DOKTER" Then
    '        MsgBox("Silahkan Pilih Kode CPPT Profesi Dokter", MsgBoxStyle.Exclamation, Me.Text)
    '        Exit Sub
    '    End If

    'End Sub
    Private Sub picCPPT_BuatTemplateTindakan_Click(sender As Object, e As EventArgs) Handles btnbtnBuatTemplateTindakan.Click
        Dim frmCPPT_TemplateProsedurList As New frmCPPT_TemplateProsedurList
        frmCPPT_TemplateProsedurList.ShowDialog()

        fn_LoadCPPT_TemplateTindakan()
    End Sub
    Private Sub DeleteToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        grvCPPT_Diagnosa.DeleteSelectedRows()
    End Sub
    Private Sub DeleteToolStripMenuItem1_Click_1(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem1.Click
        If oFormMode = FORM_MODE.FORM_MODE_EDIT Then
            Dim dsCeklaboratorium = oSalesOrderTransaksi.GetDataByCategory(txtCPPT_Kode.Text, 2)
            If dsCeklaboratorium IsNot Nothing Then
                MsgBox("Maap Transaksi Sudah di Baca Laboratorium, Silahkan Hubungi Unit Bersangkutan", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            Dim dsCekRad = oSalesOrderTransaksi.GetDataByCategory(txtCPPT_Kode.Text, 3)
            If dsCekRad IsNot Nothing Then
                MsgBox("Maap Transaksi Sudah di Baca Radiologi, Silahkan Hubungi Unit Bersangkutan", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
        End If

        grvCPPT_Tindakan.DeleteSelectedRows()
    End Sub
    Private Sub DeleteToolStripMenuItem2_Click_1(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem2.Click
        If oFormMode = FORM_MODE.FORM_MODE_EDIT Then
            Dim dsCekFarmasi = oSalesOrderTransaksi.GetDataByCategory(txtCPPT_Kode.Text, 4)
            If dsCekFarmasi IsNot Nothing Then
                MsgBox("Maap Transaksi Sudah di Baca Farmasi, Silahkan Hubungi Unit Bersangkutan", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
        End If

        grvCPPT_ResepNonRacikan.DeleteSelectedRows()
    End Sub
    Private Sub DeleteToolStripMenuItem3_Click_1(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem3.Click
        If oFormMode = FORM_MODE.FORM_MODE_EDIT Then
            Dim dsCekFarmasi = oSalesOrderTransaksi.GetDataByCategory(txtCPPT_Kode.Text, 4)
            If dsCekFarmasi IsNot Nothing Then
                MsgBox("Maap Transaksi Sudah di Baca Farmasi, Silahkan Hubungi Unit Bersangkutan", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
        End If

        grvCPPT_ResepRacikan.DeleteSelectedRows()
    End Sub
#End Region
#Region "Grid Method"
    Private Sub btnTindakanTerakhir_Click(sender As Object, e As EventArgs) Handles btnTindakanTerakhir.Click
        Dim dsTerakhirUser = oGrouperDataCppt.GetDataByRmTerakhirByUserAndDokter(sNoRekamMedis, sUserID)
        If dsTerakhirUser IsNot Nothing Then
            If dsTerakhirUser.ISDELETE = False Then
                BindingSourceCPPT_Tindakan.DataSource = oGrouperDataCppt.GetDataDetailTindakan(dsTerakhirUser.KDCPPT).OrderBy(Function(x) x.SEQ).ToList()
                grdCPPT_Tindakan.DataSource = BindingSourceCPPT_Tindakan
            End If
        Else
            MsgBox("Maap Tindakan Terakhir User " & sUserID & " Belum Ada", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub btnObatTerakhir_Click(sender As Object, e As EventArgs) Handles btnObatTerakhir.Click
        Dim dsTerakhirUser = oGrouperDataCppt.GetDataByRmTerakhirByUserAndDokter(sNoRekamMedis, sUserID)
        If dsTerakhirUser IsNot Nothing Then
            If dsTerakhirUser.ISDELETE = False Then
                If grdCPPT_Profesi.Text = "DOKTER" Then
                    BindingSourceCPPT_NonRacikan.DataSource = oGrouperDataCppt.GetDataDetailNonRacikan(dsTerakhirUser.KDCPPT).OrderBy(Function(x) x.SEQ).ToList()
                    grdCPPT_ResepNonRacikan.DataSource = BindingSourceCPPT_NonRacikan

                    BindingSourceCPPT_Racikan.DataSource = oGrouperDataCppt.GetDataDetailRacikan(dsTerakhirUser.KDCPPT).OrderBy(Function(x) x.SEQ).ToList()
                    grdCPPT_ResepRacikan.DataSource = BindingSourceCPPT_Racikan
                End If
            End If
        Else
            MsgBox("Maap Obat Terakhir User " & sUserID & " Belum Ada", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    'Private Sub OnValueChangedLimit(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If isLoad Then
    '        CalculateLimit()
    '    End If
    'End Sub
    'Private Sub CalculateLimit()
    '    txtCPPT_Limit.Text = CDec(txtCPPT_TotalKlaim.Text) - CDec(txtCPPT_TotalPaket.Text)

    '    If CDec(txtCPPT_Limit.Text) < 0 Then
    '        txtCPPT_Limit.BackColor = Color.Red
    '    Else
    '        txtCPPT_Limit.BackColor = Color.Green
    '    End If
    'End Sub
    Private Function buletin(ByVal Number As Double, Optional ByVal Range As Integer = 10) As Decimal
        buletin = Math.Round(Number / Range, 0) * Range
    End Function
    Private Sub OnValueChangedTindakan(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grvCPPT_Tindakan.FocusedRowChanged, grvCPPT_ResepNonRacikan.FocusedRowChanged, grvCPPT_ResepRacikan.FocusedRowChanged
        If isLoad Then
            Calculate()
        End If
    End Sub
    Private Sub Calculate()
        Dim sSubTotal As Decimal = 0
        For i As Integer = 0 To grvCPPT_Tindakan.RowCount - 2
            sSubTotal += CDec(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_TOTALTINDAKAN))
        Next

        txtCPPT_TotalTindakan.Text = sSubTotal

        Dim sSubTotalNonRacikanPaket = 0
        Dim sSubTotalNonRacikanNonPaket = 0

        For i As Integer = 0 To grvCPPT_ResepNonRacikan.RowCount - 2
            sSubTotalNonRacikanPaket += CDec(grvCPPT_ResepNonRacikan.GetRowCellValue(i, colCPPT_TOTAL_PAKETNONRACIKAN))
            sSubTotalNonRacikanNonPaket += CDec(grvCPPT_ResepNonRacikan.GetRowCellValue(i, colCPPT_TOTAL_NONPAKETNONRACIKAN))
        Next

        txtCPPT_TotalNonRacikanPaket.Text = sSubTotalNonRacikanPaket
        txtCPPT_TotalNonRacikanNonPaket.Text = sSubTotalNonRacikanNonPaket

        Dim sSubTotalRacikan = 0
        For i As Integer = 0 To grvCPPT_ResepRacikan.RowCount - 2
            sSubTotalRacikan += CDec(grvCPPT_ResepRacikan.GetRowCellValue(i, colCPPT_TOTALRACIKAN))
        Next

        txtCPPT_TotalRacikanPaket.Text = sSubTotalRacikan

        'txtCPPT_TotalPaket.Text = CDec(txtCPPT_TotalTindakan.Text) + CDec(txtCPPT_TotalNonRacikanPaket.Text) + CDec(txtCPPT_TotalRacikanPaket.Text) + sRincianSudahSimpanByKdPendaftaran
        'txtCPPT_TotalNonPaket.Text = CDec(txtCPPT_TotalNonRacikanNonPaket.Text) + CDec(txtCPPT_TotalRacikanNonPaket.Text)
    End Sub
    Private Sub grvCPPT_Diagnosa_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvCPPT_Diagnosa.CellValueChanged
        If e.Column.Name = colCPPT_MEMODIAGNOSA.Name Then
            If grvCPPT_Diagnosa.GetFocusedRowCellValue(colCPPT_MEMODIAGNOSA) IsNot Nothing Then
                Dim sCek As Integer = 0

                For i As Integer = 0 To grvCPPT_Diagnosa.RowCount - 2
                    sCek += 1
                Next

                grvCPPT_Diagnosa.SetFocusedRowCellValue(colCPPT_KATEGORI, If(sCek = 0, "Primer", "Sekunder"))
            End If
        End If
    End Sub
    Private Sub grvCPPT_Tindakan_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvCPPT_Tindakan.CellValueChanged
        Dim oItem As New Reference.clsItem

        If e.Column.Name = colCPPT_KDITEMTINDAKAN.Name Then
            If grvCPPT_Tindakan.GetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN) IsNot Nothing Then
                Dim ds = oItem.GetDataDetail_UOM(grvCPPT_Tindakan.GetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN))

                If ds IsNot Nothing Then
                    Try
                        grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_KDUOMTINDAKAN, ds.FirstOrDefault(Function(x) x.M_UOM.KDKELASRAWAT = sKelas).KDUOM)
                    Catch ex As Exception
                        grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_KDUOMTINDAKAN, ds.FirstOrDefault(Function(x) x.RATE = 1).KDUOM)
                    End Try
                End If
            End If
        ElseIf e.Column.Name = colCPPT_KDUOMTINDAKAN.Name Then
            Try
                If grvCPPT_Tindakan.GetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN) IsNot Nothing And grvCPPT_Tindakan.GetFocusedRowCellValue(colCPPT_KDUOMTINDAKAN) IsNot Nothing Then
                    Dim ds = oItem.GetDataDetail_UOM(grvCPPT_Tindakan.GetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN), grvCPPT_Tindakan.GetFocusedRowCellValue(colCPPT_KDUOMTINDAKAN))

                    If ds IsNot Nothing Then
                        grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_JUMLAHTINDAKAN, 1)
                        grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_HARGATINDAKAN, ds.PRICESALESSTANDARD)
                        grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_TOTALTINDAKAN, ds.PRICESALESSTANDARD)
                    Else
                        MsgBox(Statement.ErrorUOM, MsgBoxStyle.Exclamation, Me.Text)

                        Dim sItem = grvCPPT_Tindakan.GetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN)
                        grvCPPT_Tindakan.CancelUpdateCurrentRow()

                        grvCPPT_Tindakan.AddNewRow()
                        grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN, sItem)
                    End If
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub grvCPPT_ResepNonRacikan_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvCPPT_ResepNonRacikan.CellValueChanged
        Try
            If grdCPPT_Profesi.Text = "" Then
                MsgBox("Dibutuhkan Profesi", MsgBoxStyle.Exclamation, Me.Text)
                grdCPPT_Profesi.Focus()
                Exit Sub
            Else
                If Not grdCPPT_Profesi.Text.Contains("DOKTER") Then
                    MsgBox("Silahkan Pilih Profesi Dokter", MsgBoxStyle.Exclamation, Me.Text)
                    grdCPPT_Profesi.Focus()
                    Exit Sub
                End If
            End If

            If e.Column.Name = colCPPT_KDITEMNONRACIKAN.Name Then
                If grvCPPT_ResepNonRacikan.GetFocusedRowCellValue(colCPPT_KDITEMNONRACIKAN) IsNot Nothing Then
                    Dim oItem As New Reference.clsItem
                    Dim ds = oItem.GetDataDetail_UOM(grvCPPT_ResepNonRacikan.GetFocusedRowCellValue(colCPPT_KDITEMNONRACIKAN))

                    If ds IsNot Nothing Then
                        grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_KDUOMNONRACIKAN, ds.FirstOrDefault(Function(x) x.RATE = 1).KDUOM)
                        grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_REMARKS_DOKTERNONRACIKAN, "")
                        grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_ISBACANONRACIKAN, "0")
                        grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_ISALKESNONRACIKAN, IIf(ds.FirstOrDefault.M_ITEM.M_ITEM_L3.MEMO = "ALKES", True, False))
                        grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_ISALKESNONRACIKAN, IIf(ds.FirstOrDefault.M_ITEM.M_ITEM_L3.MEMO = "OBAT KRONIS", True, False))
                        grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_JUMLAHNONRACIKAN, 0)
                    End If
                End If
            ElseIf e.Column.Name = colCPPT_KDUOMNONRACIKAN.Name Then
                Dim oItem As New Reference.clsItem
                Try
                    If grvCPPT_ResepNonRacikan.GetFocusedRowCellValue(colCPPT_KDITEMNONRACIKAN) IsNot Nothing And grvCPPT_ResepNonRacikan.GetFocusedRowCellValue(colCPPT_KDUOMNONRACIKAN) IsNot Nothing Then
                        Dim ds = oItem.GetDataDetail_UOM(grvCPPT_ResepNonRacikan.GetFocusedRowCellValue(colCPPT_KDITEMNONRACIKAN), grvCPPT_ResepNonRacikan.GetFocusedRowCellValue(colCPPT_KDUOMNONRACIKAN))

                        If ds IsNot Nothing Then
                            'grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_HARGANONRACIKAN, ds.PRICESALESSTANDARD)

                            grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_HARGANONRACIKAN, ds.PRICEPURCHASESTANDARD)

                            If CDec(grvCPPT_ResepNonRacikan.GetFocusedRowCellValue(colCPPT_HARGANONRACIKAN)) > 0 Then
                                'Dim sPricePPN = CDec(grvCPPT_ResepNonRacikan.GetFocusedRowCellValue(colPRICE)) * (11 / 100)
                                Dim sPriceSetelahPPN = 0 + CDec(grvCPPT_ResepNonRacikan.GetFocusedRowCellValue(colCPPT_HARGANONRACIKAN))
                                ' Dim tes = CDec(sPriceSetelahPPN * IIf(chkBPJS.Checked = False, (ds.MARGIN / 100), (ds.PRICESALESTERMIN / 100)))
                                'Dim sPriceTermin = buletin(CDec(sPriceSetelahPPN * IIf(Not sPenjamin.Contains("BPJS"), (ds.MARGIN / 100), (ds.PRICESALESTERMIN / 100))) + CDec(sPriceSetelahPPN), 100)
                                Dim sPriceTermin = buletin(CDec(sPriceSetelahPPN * IIf(Not sPenjamin.Contains("BPJS"), (ds.MARGIN / 100), (ds.MARGIN / 100))) + CDec(sPriceSetelahPPN), 100)

                                grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_HARGANONRACIKAN, sPriceTermin)
                            Else
                                grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_HARGANONRACIKAN, 0)
                            End If
                        Else
                            MsgBox(Statement.ErrorUOM, MsgBoxStyle.Exclamation, Me.Text)

                            Dim sItem = grvCPPT_ResepNonRacikan.GetFocusedRowCellValue(colCPPT_KDITEMNONRACIKAN)
                            grvCPPT_ResepNonRacikan.CancelUpdateCurrentRow()

                            grvCPPT_ResepNonRacikan.AddNewRow()
                            grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_KDITEMNONRACIKAN, sItem)
                        End If
                    End If
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf e.Column.Name = colCPPT_JUMLAHNONRACIKAN.Name Then
                If CDec(grvCPPT_ResepNonRacikan.GetFocusedRowCellValue(colCPPT_JUMLAHNONRACIKAN)) > 0 Then
                    Dim QTY As Decimal = CDec(grvCPPT_ResepNonRacikan.GetFocusedRowCellValue(colCPPT_JUMLAHNONRACIKAN))

                    If sPenjamin.Contains("BPJS") Then
                        If CBool(grvCPPT_ResepNonRacikan.GetFocusedRowCellValue(colCPPT_ISKRONISNONRACIKAN)) = True Then
                            If QTY = 15 Then
                                grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_JUMLAH_PAKETNONRACIKAN, 4)
                                grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_JUMLAH_NONPAKETNONRACIKAN, 11)
                            ElseIf QTY = 30 Then
                                grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_JUMLAH_PAKETNONRACIKAN, 7)
                                grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_JUMLAH_NONPAKETNONRACIKAN, 23)
                            ElseIf QTY = 60 Then
                                grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_JUMLAH_PAKETNONRACIKAN, 14)
                                grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_JUMLAH_NONPAKETNONRACIKAN, 46)
                            ElseIf QTY = 90 Then
                                grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_JUMLAH_PAKETNONRACIKAN, 21)
                                grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_JUMLAH_NONPAKETNONRACIKAN, 69)
                            ElseIf QTY = 120 Then
                                grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_JUMLAH_PAKETNONRACIKAN, 28)
                                grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_JUMLAH_NONPAKETNONRACIKAN, 92)
                            ElseIf QTY = 180 Then
                                grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_JUMLAH_PAKETNONRACIKAN, 42)
                                grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_JUMLAH_NONPAKETNONRACIKAN, 138)
                            Else
                                grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_JUMLAH_PAKETNONRACIKAN, QTY)
                                grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_JUMLAH_NONPAKETNONRACIKAN, 0)
                            End If
                        Else
                            grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_JUMLAH_PAKETNONRACIKAN, QTY)
                            grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_JUMLAH_NONPAKETNONRACIKAN, 0)
                        End If
                    Else
                        grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_JUMLAH_PAKETNONRACIKAN, QTY)
                        grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_JUMLAH_NONPAKETNONRACIKAN, 0)
                    End If
                End If
            ElseIf e.Column.Name = colCPPT_JUMLAH_PAKETNONRACIKAN.Name Or e.Column.Name = colCPPT_JUMLAH_NONPAKETNONRACIKAN.Name Then
                Dim sSubTotalPaket As Decimal = CDec(grvCPPT_ResepNonRacikan.GetFocusedRowCellValue(colCPPT_JUMLAH_PAKETNONRACIKAN)) * CDec(grvCPPT_ResepNonRacikan.GetFocusedRowCellValue(colCPPT_HARGANONRACIKAN))
                Dim sSubTotalNonPaket As Decimal = CDec(grvCPPT_ResepNonRacikan.GetFocusedRowCellValue(colCPPT_JUMLAH_NONPAKETNONRACIKAN)) * CDec(grvCPPT_ResepNonRacikan.GetFocusedRowCellValue(colCPPT_HARGANONRACIKAN))

                grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_TOTAL_PAKETNONRACIKAN, sSubTotalPaket)
                grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_TOTAL_NONPAKETNONRACIKAN, sSubTotalNonPaket)
                'grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_TOTALNONRACIKAN, sSubTotalPaket + sSubTotalNonPaket)

                Dim sSubTotal As Decimal = CDec(grvCPPT_ResepNonRacikan.GetFocusedRowCellValue(colCPPT_JUMLAHNONRACIKAN)) * CDec(grvCPPT_ResepNonRacikan.GetFocusedRowCellValue(colCPPT_HARGANONRACIKAN))
                grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_TOTALNONRACIKAN, sSubTotal)
                'ElseIf e.Column.Name = colCPPT_ISBACANONRACIKAN.Name Then
                '    Try
                '        If grvCPPT_ResepNonRacikan.GetFocusedRowCellValue(colCPPT_ISBACANONRACIKAN) IsNot Nothing Then
                '            If grvCPPT_ResepNonRacikan.GetFocusedRowCellValue(colCPPT_ISBACANONRACIKAN) = 1 Then
                '                MsgBox("Maap Transaksi Sudah di Baca Farmasi, Silahkan Hubungi Unit Bersangkutan", MsgBoxStyle.Exclamation, Me.Text)
                '                grvCPPT_ResepNonRacikan.CancelUpdateCurrentRow()
                '            End If
                '        End If
                '    Catch ex As Exception

                '    End Try
            End If
        Catch oErr As Exception
            MsgBox("Event : " & vbCrLf & oErr.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub
    Private Sub grvCPPT_ResepRacikan_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvCPPT_ResepRacikan.CellValueChanged
        Try
            If grdCPPT_Profesi.Text = "" Then
                MsgBox("Dibutuhkan Profesi", MsgBoxStyle.Exclamation, Me.Text)
                grdCPPT_Profesi.Focus()
                Exit Sub
            Else
                If Not grdCPPT_Profesi.Text.Contains("DOKTER") Then
                    MsgBox("Silahkan Pilih Profesi Dokter", MsgBoxStyle.Exclamation, Me.Text)
                    grdCPPT_Profesi.Focus()
                    Exit Sub
                End If
            End If

            If e.Column.Name = colCPPT_KDITEMRACIKAN.Name Then
                Try
                    If grvCPPT_ResepRacikan.GetFocusedRowCellValue(colCPPT_KDITEMRACIKAN) IsNot Nothing Then
                        Dim oItem As New Reference.clsItem
                        Dim ds = oItem.GetDataDetail_UOM(grvCPPT_ResepRacikan.GetFocusedRowCellValue(colCPPT_KDITEMRACIKAN))

                        If ds IsNot Nothing Then
                            grvCPPT_ResepRacikan.SetFocusedRowCellValue(colCPPT_KDUOMRACIKAN, ds.FirstOrDefault(Function(x) x.RATE = 1).KDUOM)
                            grvCPPT_ResepRacikan.SetFocusedRowCellValue(colCPPT_ISBACARACIKAN, "0")
                            grvCPPT_ResepRacikan.SetFocusedRowCellValue(colCPPT_SIGNARACIKAN, "")
                            grvCPPT_ResepRacikan.SetFocusedRowCellValue(colCPPT_PERMINTAANRACIKAN, "")
                            grvCPPT_ResepRacikan.SetFocusedRowCellValue(colCPPT_REMARKSRACIKAN, "")
                            grvCPPT_ResepRacikan.SetFocusedRowCellValue(colCPPT_JUMLAHRACIKAN, 0)
                        End If
                    End If
                Catch oErr As Exception
                    MsgBox("Load Detail : " & vbCrLf & oErr.Message, MsgBoxStyle.Critical, Me.Text)
                End Try
            ElseIf e.Column.Name = colCPPT_KDUOMRACIKAN.Name Then
                Dim oItem As New Reference.clsItem
                Try
                    If grvCPPT_ResepRacikan.GetFocusedRowCellValue(colCPPT_KDITEMRACIKAN) IsNot Nothing And grvCPPT_ResepRacikan.GetFocusedRowCellValue(colCPPT_KDUOMRACIKAN) IsNot Nothing Then
                        Dim ds = oItem.GetDataDetail_UOM(grvCPPT_ResepRacikan.GetFocusedRowCellValue(colCPPT_KDITEMRACIKAN), grvCPPT_ResepRacikan.GetFocusedRowCellValue(colCPPT_KDUOMRACIKAN))

                        If ds IsNot Nothing Then
                            'grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_HARGANONRACIKAN, ds.PRICESALESSTANDARD)

                            grvCPPT_ResepRacikan.SetFocusedRowCellValue(colCPPT_HARGARACIKAN, ds.PRICEPURCHASESTANDARD)

                            If CDec(grvCPPT_ResepRacikan.GetFocusedRowCellValue(colCPPT_HARGARACIKAN)) > 0 Then
                                'Dim sPricePPN = CDec(grvCPPT_ResepRacikan.GetFocusedRowCellValue(colPRICE)) * (11 / 100)
                                Dim sPriceSetelahPPN = 0 + CDec(grvCPPT_ResepRacikan.GetFocusedRowCellValue(colCPPT_HARGARACIKAN))
                                ' Dim tes = CDec(sPriceSetelahPPN * IIf(chkBPJS.Checked = False, (ds.MARGIN / 100), (ds.PRICESALESTERMIN / 100)))
                                Dim sPriceTermin = buletin(CDec(sPriceSetelahPPN * IIf(Not sPenjamin.Contains("BPJS"), (ds.MARGIN / 100), (ds.MARGIN / 100))) + CDec(sPriceSetelahPPN), 100)

                                grvCPPT_ResepRacikan.SetFocusedRowCellValue(colCPPT_HARGARACIKAN, sPriceTermin)

                                grvCPPT_ResepRacikan.SetFocusedRowCellValue(colCPPT_HARGARACIKAN, buletin(CDec(IIf(Not sPenjamin.Contains("BPJS"), (ds.MARGIN / 100), (ds.MARGIN / 100))), 100))
                            Else
                                grvCPPT_ResepRacikan.SetFocusedRowCellValue(colCPPT_HARGARACIKAN, 0)
                            End If
                        Else
                            MsgBox(Statement.ErrorUOM, MsgBoxStyle.Exclamation, Me.Text)

                            Dim sItem = grvCPPT_ResepRacikan.GetFocusedRowCellValue(colCPPT_KDITEMRACIKAN)
                            grvCPPT_ResepRacikan.CancelUpdateCurrentRow()

                            grvCPPT_ResepRacikan.AddNewRow()
                            grvCPPT_ResepRacikan.SetFocusedRowCellValue(colCPPT_KDITEMRACIKAN, sItem)
                        End If
                    End If
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf e.Column.Name = colCPPT_JUMLAHRACIKAN.Name Or e.Column.Name = colCPPT_HARGARACIKAN.Name Then
                Dim sSubTotal As Decimal = CDec(grvCPPT_ResepRacikan.GetFocusedRowCellValue(colCPPT_JUMLAHRACIKAN)) * CDec(grvCPPT_ResepRacikan.GetFocusedRowCellValue(colCPPT_HARGARACIKAN))
                grvCPPT_ResepRacikan.SetFocusedRowCellValue(colCPPT_TOTALRACIKAN, sSubTotal)
                'ElseIf e.Column.Name = colCPPT_ISBACANONRACIKAN.Name Then
                '    Try
                '        If grvCPPT_ResepRacikan.GetFocusedRowCellValue(colCPPT_ISBACARACIKAN) IsNot Nothing Then
                '            If grvCPPT_ResepRacikan.GetFocusedRowCellValue(colCPPT_ISBACARACIKAN) = 1 Then
                '                MsgBox("Maap Transaksi Sudah di Baca Farmasi, Silahkan Hubungi Unit Bersangkutan", MsgBoxStyle.Exclamation, Me.Text)
                '                grvCPPT_ResepRacikan.CancelUpdateCurrentRow()
                '            End If
                '        End If
                '    Catch ex As Exception

                '    End Try
            End If
        Catch oErr As Exception
            MsgBox("Event : " & vbCrLf & oErr.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub
    Private Sub grdCPPT_Profesi_EditValueChanged(sender As Object, e As EventArgs) Handles grdCPPT_Profesi.EditValueChanged
        If isLoad = True Then
            If grdCPPT_Profesi.Text.Contains("DOKTER") Then

            Else
                grvCPPT_ResepNonRacikan.OptionsSelection.MultiSelect = True
                grvCPPT_ResepNonRacikan.SelectAll()
                grvCPPT_ResepNonRacikan.DeleteSelectedRows()
                grvCPPT_ResepNonRacikan.OptionsSelection.MultiSelect = False

                grvCPPT_ResepRacikan.OptionsSelection.MultiSelect = True
                grvCPPT_ResepRacikan.SelectAll()
                grvCPPT_ResepRacikan.DeleteSelectedRows()
                grvCPPT_ResepRacikan.OptionsSelection.MultiSelect = False
            End If
        End If
    End Sub
    Private Sub btnRiwayatPemberianResep_Click(sender As Object, e As EventArgs) Handles btnRiwayatPemberianResep.Click
        Try
            frmReportBillingFarmasiPasien.fn_RekamMedis(sNoRekamMedis, "Resep")
            frmReportBillingFarmasiPasien.ShowDialog(Me)

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmReportBillingFarmasiPasien Is Nothing Then frmReportBillingFarmasiPasien.Dispose()
            frmReportBillingFarmasiPasien = Nothing

            For Each xloop In oGrouperDataCppt.GetDataDetailNonRacikan(sKODECPPTCOPY)
                grvCPPT_ResepNonRacikan.Focus()
                grvCPPT_ResepNonRacikan.AddNewRow()
                grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_KDITEMNONRACIKAN, xloop.KDITEM)
                grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_JUMLAHNONRACIKAN, xloop.JUMLAH)
                grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_KDSIGNANONRACIKAN, xloop.KDSIGNA)
                grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_KDCARAPAKAINONRACIKAN, xloop.KDCARAPAKAI)
                grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_REMARKS_DOKTERNONRACIKAN, xloop.REMARKS_DOKTER)
                grvCPPT_ResepNonRacikan.UpdateCurrentRow()
            Next

            'For Each xloop In oGrouperDataCppt.GetDataDetailRacikan(sKODECPPTCOPY)
            '        grvCPPT_ResepRacikan.Focus()
            '        grvCPPT_ResepRacikan.AddNewRow()
            '        grvCPPT_ResepRacikan.SetFocusedRowCellValue(colCPPT_KDITEMRACIKAN, xloop.KDITEM)
            '        grvCPPT_ResepRacikan.SetFocusedRowCellValue(colCPPT_JUMLAHRACIKAN, xloop.JUMLAH)
            '        grvCPPT_ResepRacikan.SetFocusedRowCellValue(colCPPT_SIGNARACIKAN, xloop.SIGNA)
            '        grvCPPT_ResepRacikan.SetFocusedRowCellValue(colCPPT_PERMINTAANRACIKAN, xloop.PERMINTAAN)
            '        grvCPPT_ResepRacikan.SetFocusedRowCellValue(colCPPT_REMARKSRACIKAN, xloop.REMARKS)
            '        grvCPPT_ResepRacikan.UpdateCurrentRow()
            'Next
        End Try
    End Sub
    Private Sub btnRiwayatPemberianObat_Click(sender As Object, e As EventArgs) Handles btnRiwayatPemberianObat.Click
        Try
            frmReportBillingFarmasiPasien.fn_RekamMedis(sNoRekamMedis, "Obat")
            frmReportBillingFarmasiPasien.ShowDialog(Me)

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmReportBillingFarmasiPasien Is Nothing Then frmReportBillingFarmasiPasien.Dispose()
            frmReportBillingFarmasiPasien = Nothing

            Dim oTransaksi As New Sales.clsSalesOrderTransaksi

            For Each xloop In oTransaksi.GetDataDetail(sKODECPPTCOPY)
                grvCPPT_ResepNonRacikan.Focus()
                grvCPPT_ResepNonRacikan.AddNewRow()
                grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_KDITEMNONRACIKAN, xloop.KDITEM)
                grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_JUMLAHNONRACIKAN, xloop.QTY)
                grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_KDSIGNANONRACIKAN, xloop.KDSIGNA)
                grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_KDCARAPAKAINONRACIKAN, xloop.KDCARAPAKAI)
                grvCPPT_ResepNonRacikan.SetFocusedRowCellValue(colCPPT_REMARKS_DOKTERNONRACIKAN, xloop.REMARKS)
                grvCPPT_ResepNonRacikan.UpdateCurrentRow()
            Next
        End Try
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click

    End Sub
#End Region
End Class