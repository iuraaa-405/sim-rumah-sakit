Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports DevExpress.XtraSplashScreen
Imports Newtonsoft.Json.Linq
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports iTextSharp.text.pdf
Imports MySql.Data.MySqlClient
Imports System.Text.RegularExpressions
Imports System.Net

Public Class frmErmList
#Region "Form Load Utama"
    Private sSplashScreen As Boolean = False
    Private sLoadOnForm As Boolean = False
    Private sLoadCPPT As Boolean = False
    Private oGetGrouper As New Brigging.clsSetKoneksi
    Private oRME As New RME.clsRME
    Private sTanggalLahir As DateTime = Now
    Private FolderSimpan = "C:/Source/RME/"
    Private sRincianSudahSimpanByKdPendaftaran As Decimal = 0

    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = "ERM-List"

        If Not Directory.Exists(FolderSimpan) Then
            Directory.CreateDirectory(FolderSimpan)
        Else
            oRME.DeleteDirectory(FolderSimpan)
            Directory.CreateDirectory(FolderSimpan)
        End If

        picPanggilDokterTaksId4.Visible = False
        lpicPanggilDokterTaksId4.Visible = False

        picPanggilDokterTaksId5.Visible = False
        lpicPanggilDokterTaksId5.Visible = False

        picDelegasi.Visible = False
        lblDelegasiPemetaan.Visible = False

        deDATEFrom.DateTime = Now.AddDays((-Now.Day) + 1)
        deDATETo.DateTime = Now

        fn_LoadDPJPdanUnit()
        cboTYPE.SelectedIndex = 0
        fn_Category()
        fn_LoadSecurity()

        lCPPT_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lCPPT_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lCPPT_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        lgrdLaporanOperasi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lPanelLaporanOperasi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lLaporanOperasi_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        lTransferIntrenal_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lTransferIntrenal_Pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lTransferInternal_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        lTindakanEvaluasi_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lTindakanEvaluasi_Pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lTindakanEvaluasi_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        lGerdQ_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lGerdQ_Pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lGerdQ_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        lAsesmenPerawat_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lAsesmenPerawat_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lAsesmenKeperawatan_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        lPanelAsesmenMedis_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lPanelAsesmenMedis_Panel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lAsesmenMedis_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        lRencanaOperasi_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lRencanaOperasi_Pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lRencanaOperasi_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        lLaporanTindakan_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lLaporanTindakan_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lLaporanTindakan_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        lResumeRawatInap_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lResumeRawatInap_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lResumeRawatInap_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        fn_Empty()
        sSplashScreen = True
    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        fn_Empty()
    End Sub
#End Region
#Region "List"
#Region "Fungction"
    Private Sub fn_LoadSecurity()
        Try
            Dim oOtority As New Setting.clsOtority
            Dim oUser As New Setting.clsUser

            Dim ds = (From x In oOtority.GetDataDetail
                      Join y In oUser.GetData
                      On x.KDOTORITY Equals y.KDOTORITY
                      Where x.MODUL = "RME" _
                      And y.KDUSER = sUserID
                      Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                picPrint.Enabled = ds.ISPRINT
                picRefresh.Enabled = ds.ISVIEW
                piciCareJKN.Enabled = ds.ISVIEW

                If ds.ISVIEW = True Then
                    fn_Preview()
                End If
            Catch oErr As Exception
                MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)

                picPrint.Enabled = False
                picRefresh.Enabled = False
                piciCareJKN.Enabled = False
            End Try

            If sModulBoolean = True Then
                btnAdmision.Visible = True
                tabAksi.PageVisible = False
                tabAsesmenAwalPerawat.PageVisible = False
                tabAsesmenAwalMedis.PageVisible = False
                tabResumeRawatInap.PageVisible = False
                tabLembarObservasi.PageVisible = False
                tabTransferInternal.PageVisible = False
                tabSuratKeterangan.PageVisible = False
                tabKartuObat.PageVisible = False
                tabRencanaOperasi.PageVisible = False
                tabLaporanOperasi.PageVisible = False
                tabLaporanTindakan.PageVisible = False
                tabTindakanEvaluasiKeperawatan.PageVisible = False
                tabGerdQ.PageVisible = False
                tabScan.PageVisible = True
                tabInfConsent.PageVisible = False
                tabSuratKematian.PageVisible = False
                tabFormEdukasiPasien.PageVisible = False
                tabCaseManager.PageVisible = False

                tabDokumenLainnya.PageVisible = False
                lRencanaOperasi_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                tabCPPT_PDF.PageVisible = True
                tabResume_PDF.PageVisible = False
                tabAsesmenPerawat_PDF.PageVisible = False
                tabAsesmenMedis_PDF.PageVisible = False
                tabHasilLab_PDF.PageVisible = False
                tabHasilExpertise_PDF.PageVisible = False
                tabScan_PDF.PageVisible = True
                tabLaporanOperasi_PDF.PageVisible = False
            Else
                btnAdmision.Visible = False
            End If
        Catch oErr As Exception
            MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Print()
        Try
            printableComponentLink.Landscape = True
            printableComponentLink.PaperKind = Printing.PaperKind.A4

            Dim phf As DevExpress.XtraPrinting.PageHeaderFooter =
        TryCast(printableComponentLink.PageHeaderFooter, DevExpress.XtraPrinting.PageHeaderFooter)
            phf.Header.Content.Clear()
            'phf.Header.Font = New Font("Times New Roman", 14, FontStyle.Bold)
            'phf.Header.LineAlignment = BrickAlignment.Center
            'phf.Footer.Font = New Font("Times New Roman", 9.75)
            'phf.Footer.LineAlignment = BrickAlignment.Far
            phf.Footer.Content.AddRange(New String() _
        {sWATERMARK, "", Report.REPORT_PAGE & " : [Page # of Pages #]"})

            phf.Header.Content.AddRange(New String() _
{"", "ERM" & vbCrLf & cboTYPE.Text, ""})

            printableComponentLink.Component = grd
            printableComponentLink.CreateDocument()
            printableComponentLink.ShowPreviewDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Preview()
        Try
            grv.Columns.Clear()
            grd.DataSource = Nothing
            grv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            If cboTYPE.SelectedIndex = 0 Then
                Query_LoadHistoryPasien("IGD")
            ElseIf cboTYPE.SelectedIndex = 1 Then
                Query_LoadHistoryPasien("R.Jalan")
            ElseIf cboTYPE.SelectedIndex = 2 Then
                Query_LoadHistoryPasien("R.Inap")
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Empty()
        grvCPPT_List.Columns.Clear()
        grdCPPT_List.DataSource = Nothing
        grvCPPT_List.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

        grvTindakanEvaluasi_Transaksi.Columns.Clear()
        grdTindakanEvaluasi_Transaksi.DataSource = Nothing
        grvTindakanEvaluasi_Transaksi.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

        grvTransferInternal_Transaksi.Columns.Clear()
        grdTransferInternal_Transaksi.DataSource = Nothing
        grvTransferInternal_Transaksi.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

        grvResume_List.Columns.Clear()
        grdResume_List.DataSource = Nothing
        grvResume_List.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

        grvAsesmenMedis_PDF.Columns.Clear()
        grdAsesmenMedis_PDF.DataSource = Nothing
        grvAsesmenMedis_PDF.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

        grvHasilExpertise_List.Columns.Clear()
        grdHasilExpertise_List.DataSource = Nothing
        grvHasilExpertise_List.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

        grvHasilLaboratorium_List.Columns.Clear()
        grdHasilLaboratorium_List.DataSource = Nothing
        grvHasilLaboratorium_List.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

        grvLaporanOperasi_PDF.Columns.Clear()
        grdLaporanOperasi_PDF.DataSource = Nothing
        grvLaporanOperasi_PDF.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

        PdfViewerCPPTRawatJalan.CloseDocument()
        PdfViewerResume.CloseDocument()
        PdfViewerAsesmenMedis.CloseDocument()
        PdfViewerHasilLaboratorium.CloseDocument()
        PdfViewerHasilExpertise.CloseDocument()
        PdfViewerLaporanOperasi.CloseDocument()
        PdfViewerAsesmenNakes.CloseDocument()
        PdfViewerRincian.CloseDocument()

        lblKDKUNJUNGAN.ResetText()
        lblRM.ResetText()
        lblNamaPasien.ResetText()
        lblJenisKelamin.ResetText()
        lblTanggalLahir.ResetText()
        lblDiagnosaAwal.ResetText()
        lblJenisPeserta.ResetText()
        lblUsia.ResetText()
        lblNIK.ResetText()
        lblKelas.ResetText()
        lblAGAMA.ResetText()
        lblAlamat.ResetText()
        sTanggalLahir = Now

        pnlCPPT_pic.Dock = DockStyle.Top

        lCPPT_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lCPPT_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lCPPT_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If Not frmCPPT Is Nothing Then frmCPPT.Dispose()
        frmCPPT = Nothing

        pnlAsesmenMedis_pic.Dock = DockStyle.Top

        lPanelAsesmenMedis_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lPanelAsesmenMedis_Panel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lAsesmenMedis_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If Not frmAsesmenAwalMedisIGD2 Is Nothing Then frmAsesmenAwalMedisIGD2.Dispose()
        frmAsesmenAwalMedisIGD2 = Nothing

        If Not frmDischargePlanning Is Nothing Then frmDischargePlanning.Dispose()
        frmDischargePlanning = Nothing

        If Not frmKartuAnastesi_A Is Nothing Then frmKartuAnastesi_A.Dispose()
        frmKartuAnastesi_A = Nothing

        If Not frmKartuAnastesi_B Is Nothing Then frmKartuAnastesi_B.Dispose()
        frmKartuAnastesi_B = Nothing

        If Not frmEMedrekRJ_31 Is Nothing Then frmEMedrekRJ_31.Dispose()
        frmEMedrekRJ_31 = Nothing

        If Not frmEMedrekRJ_36 Is Nothing Then frmEMedrekRJ_36.Dispose()
        frmEMedrekRJ_36 = Nothing

        If Not frmEMedrekRJ_34 Is Nothing Then frmEMedrekRJ_34.Dispose()
        frmEMedrekRJ_34 = Nothing

        If Not frmEMedrekRJ_29 Is Nothing Then frmEMedrekRJ_29.Dispose()
        frmEMedrekRJ_29 = Nothing

        If Not frmAsmedNeuro Is Nothing Then frmAsmedNeuro.Dispose()
        frmAsmedNeuro = Nothing

        If Not frmEMedrekRJ_35 Is Nothing Then frmEMedrekRJ_35.Dispose()
        frmEMedrekRJ_35 = Nothing

        If Not frmEMedrekRJ_30 Is Nothing Then frmEMedrekRJ_30.Dispose()
        frmEMedrekRJ_30 = Nothing

        If Not frmAsesmenAwalMedisNeonatusRawatInap Is Nothing Then frmAsesmenAwalMedisNeonatusRawatInap.Dispose()
        frmAsesmenAwalMedisNeonatusRawatInap = Nothing

        'If Not frmLembarUjiFungsiRehab Is Nothing Then frmLembarUjiFungsiRehab.Dispose()
        'frmLembarUjiFungsiRehab = Nothing

        'If Not frmCatatanKlinisRehab Is Nothing Then frmCatatanKlinisRehab.Dispose()
        'frmCatatanKlinisRehab = Nothing

        If Not frmResumeRawatJalanRehab Is Nothing Then frmResumeRawatJalanRehab.Dispose()
        frmResumeRawatJalanRehab = Nothing

        If Not frmEMedrekRJ_18 Is Nothing Then frmEMedrekRJ_18.Dispose()
        frmEMedrekRJ_18 = Nothing

        If Not frmEMedrekRJ_41 Is Nothing Then frmEMedrekRJ_41.Dispose()
        frmEMedrekRJ_41 = Nothing

        If Not frmEMedrek_AsmedAnak Is Nothing Then frmEMedrek_AsmedAnak.Dispose()
        frmEMedrek_AsmedAnak = Nothing

        If Not frmEMedrekAsmedKebidanan Is Nothing Then frmEMedrekAsmedKebidanan.Dispose()
        frmEMedrekAsmedKebidanan = Nothing

        If Not frmEMedrekRJ_33 Is Nothing Then frmEMedrekRJ_33.Dispose()
        frmEMedrekRJ_33 = Nothing

        If Not frmEMedrekRJ_32 Is Nothing Then frmEMedrekRJ_32.Dispose()
        frmEMedrekRJ_32 = Nothing

        If Not frmAsesmenAwalMedisTHT Is Nothing Then frmAsesmenAwalMedisTHT.Dispose()
        frmAsesmenAwalMedisTHT = Nothing

        If Not frmEMedrekAWAL_MEDIS_NEONATUS_RJ Is Nothing Then frmEMedrekAWAL_MEDIS_NEONATUS_RJ.Dispose()
        frmEMedrekAWAL_MEDIS_NEONATUS_RJ = Nothing

        If Not frmAsesmenMedisRawatJalan Is Nothing Then frmAsesmenMedisRawatJalan.Dispose()
        frmAsesmenMedisRawatJalan = Nothing

        pnlAsesmenKeperawatan_pic.Dock = DockStyle.Top

        lAsesmenPerawat_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lAsesmenPerawat_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lAsesmenKeperawatan_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        ClearFormAsesmenNakes()

        pnlResumeRawatInap_pic.Dock = DockStyle.Top

        lResumeRawatInap_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lResumeRawatInap_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lResumeRawatInap_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If Not frmRingkasanKeluar Is Nothing Then frmRingkasanKeluar.Dispose()
        frmRingkasanKeluar = Nothing

        pnlLembarObservasi_pic.Dock = DockStyle.Top

        lTransferIntrenal_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lTransferIntrenal_Pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lLembarObservasi_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If Not frmLembarObservasi Is Nothing Then frmLembarObservasi.Dispose()
        frmLembarObservasi = Nothing

        pnlTindakanEvaluasi_pic.Dock = DockStyle.Top

        lTindakanEvaluasi_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lTindakanEvaluasi_Pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lTindakanEvaluasi_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If Not frmDiagnosaPerawat Is Nothing Then frmDiagnosaPerawat.Dispose()
        frmDiagnosaPerawat = Nothing

        pnlGerdQ_pic.Dock = DockStyle.Top

        lGerdQ_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lGerdQ_Pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lGerdQ_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If Not frmGerdQ Is Nothing Then frmGerdQ.Dispose()
        frmGerdQ = Nothing

        pnlTransferInternal_pic.Dock = DockStyle.Top

        lTransferIntrenal_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lTransferIntrenal_Pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lTransferInternal_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If Not frmTransferInternal Is Nothing Then frmTransferInternal.Dispose()
        frmTransferInternal = Nothing

        pnlRencanaOperasi_pic.Dock = DockStyle.Top

        lRencanaOperasi_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lRencanaOperasi_Pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lRencanaOperasi_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If Not frmRencanaOperasi Is Nothing Then frmRencanaOperasi.Dispose()
        frmRencanaOperasi = Nothing

        pnlLaporanOperasi_pic.Dock = DockStyle.Top

        lgrdLaporanOperasi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lPanelLaporanOperasi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lLaporanOperasi_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If Not frmLaporanOperasi Is Nothing Then frmLaporanOperasi.Dispose()
        frmLaporanOperasi = Nothing

        pnlLaporanTindakan_pic.Dock = DockStyle.Top

        lLaporanTindakan_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lLaporanTindakan_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lLaporanTindakan_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If Not frmLaporanTindakan Is Nothing Then frmLaporanTindakan.Dispose()
        frmLaporanTindakan = Nothing
    End Sub
    Private Sub fn_LoadIdentitas(ByVal kdkunjungan As String)
        Try
            Dim oKunjungan As New Admission.clsPendaftaran_Kunjungan

            Dim dsKunjungan = oKunjungan.GetData(kdkunjungan)
            If dsKunjungan IsNot Nothing Then
                If dsKunjungan.S_PENDAFTARAN_H.CATEGORY = 1 Then
                    Dim oCekUpdateRuagan As New Reference.clsKelasAplicareUpdate

                    Dim dsCekUpdateRuangan = oCekUpdateRuagan.GetData(dsKunjungan.KDDEPARTMENT)
                    If dsCekUpdateRuangan IsNot Nothing Then
                        If dsCekUpdateRuangan.DATEUPDATED.ToString("yyyyMMdd") <> Now.ToString("yyyyMMdd") Then
                            MsgBox("hari Ini Ruangan Belum Update Ketersediaan Tempat Tidur, Silahkan Update dengan klik tombol Pemetaan", MsgBoxStyle.Exclamation, Me.Text)
                        End If
                    Else
                        MsgBox("hari Ini Ruangan Belum Update Ketersediaan Tempat Tidur, Silahkan Update dengan klik tombol Pemetaan", MsgBoxStyle.Exclamation, Me.Text)
                    End If
                End If

                lblKDKUNJUNGAN.Text = dsKunjungan.KDKUNJUNGAN
                lblRM.Text = dsKunjungan.S_PENDAFTARAN_H.KDCUSTOMER
                lblNamaPasien.Text = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY
                lblJenisKelamin.Text = IIf(dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.KDJENISKELAMIN = 1, "LAKI-LAKI", "PEREMPUAN")
                lblTanggalLahir.Text = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy")
                lblDiagnosaAwal.Text = dsKunjungan.S_PENDAFTARAN_H.M_DIAGNOSA.MEMO
                lblJenisPeserta.Text = dsKunjungan.S_PENDAFTARAN_H.M_DAFTAR_L2.MEMO
                sTanggalLahir = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR
                lblUsia.Text = oRME.GetUmurPasien(Now, sTanggalLahir)
                lblNIK.Text = dsKunjungan.S_PENDAFTARAN_H.KTP
                lblKelas.Text = dsKunjungan.S_PENDAFTARAN_H.M_KELASRAWAT.MEMO
                lblAGAMA.Text = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.M_AGAMA.MEMO
                lblAlamat.Text = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.ALAMAT
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Query_LoadHistoryPasien(ByVal JenisRawat As String)
        Try
            If sSplashScreen = False Then Exit Sub

            grv.Columns.Clear()
            grd.DataSource = Nothing
            grv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            grv1.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Processing data.....")

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\RME\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            If grdDPJP.Text = "" Then
                If JenisRawat = "IGD" Then
                    SQL = "EXEC LISTRME_IGD @CATEGORY = 'IGD'  "
                    SQL &= ", @DARITANGGAL = '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' "
                    SQL &= ", @SAMPAITANGGAL = '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
                    SQL &= ", @KDDOCTOR = '" & grdDPJP.EditValue & "' "
                ElseIf JenisRawat = "R.Jalan" Then
                    SQL = "EXEC LISTRME_POLI @CATEGORY = 'R.Jalan'  "
                    SQL &= ", @DARITANGGAL = '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' "
                    SQL &= ", @SAMPAITANGGAL = '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
                    SQL &= ", @KDDOCTOR = '" & grdDPJP.EditValue & "' "
                Else
                    SQL = "EXEC LISTRME_INAP @CATEGORY = 'R.Inap'  "
                    SQL &= ", @DARITANGGAL = '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' "
                    SQL &= ", @SAMPAITANGGAL = '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
                    SQL &= ", @KDDOCTOR = '" & grdDPJP.EditValue & "' "
                End If
            Else
                If JenisRawat = "IGD" Then
                    SQL = "EXEC LISTRME_IGDDOKTER @CATEGORY = 'IGD'  "
                    SQL &= ", @DARITANGGAL = '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' "
                    SQL &= ", @SAMPAITANGGAL = '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
                    SQL &= ", @KDDOCTOR = '" & grdDPJP.EditValue & "' "
                ElseIf JenisRawat = "R.Jalan" Then
                    'If btnPoliAtauDokter.Text = "Filter Dokter" Then
                    '    SQL = "EXEC LISTRME_POLIDOKTER_POLI @CATEGORY = 'R.Jalan'  "
                    '    SQL &= ", @DARITANGGAL = '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' "
                    '    SQL &= ", @SAMPAITANGGAL = '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
                    '    SQL &= ", @KDDEPARTMENT = '" & grdDPJP.EditValue & "' "
                    'Else
                    '    SQL = "EXEC LISTRME_POLIDOKTER @CATEGORY = 'R.Jalan'  "
                    '    SQL &= ", @DARITANGGAL = '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' "
                    '    SQL &= ", @SAMPAITANGGAL = '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
                    '    SQL &= ", @KDDOCTOR = '" & grdDPJP.EditValue & "' "
                    'End If
                    SQL = "EXEC LISTRME_POLIDOKTER_POLI @CATEGORY = 'R.Jalan'  "
                    SQL &= ", @DARITANGGAL = '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' "
                    SQL &= ", @SAMPAITANGGAL = '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
                    SQL &= ", @KDDEPARTMENT = '" & grdDPJP.EditValue & "' "
                Else
                    SQL = "EXEC LISTRME_INAPDOKTER @CATEGORY = 'R.Inap'  "
                    SQL &= ", @DARITANGGAL = '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' "
                    SQL &= ", @SAMPAITANGGAL = '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
                    SQL &= ", @KDDOCTOR = '" & grdDPJP.EditValue & "' "
                End If
            End If

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "R_IDENTITAS_GROUPER")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grd.MainView = grv
            grd.DataSource = ds.Tables("R_IDENTITAS_GROUPER")
            grd.ForceInitialize()

            SplashScreenManager.CloseForm(False)

            Qyery_LoadFormatData(JenisRawat)

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Qyery_LoadFormatData(ByVal JenisRawat As String)
        For iLoop As Integer = 0 To grv.Columns.Count - 1
            If grv.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n0}"
                grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm:ss}"
            End If
        Next

        grv.Columns("KDKUNJUNGAN").Visible = False
        grv.Columns("KODE").Visible = False
        grv.Columns("STATUSWARNA").Visible = False

        If JenisRawat = "R.Inap" Then
            grv.Columns("KDDEPARTMENT_NAMA").Caption = "RUANGAN"
        Else
            grv.Columns("KDDEPARTMENT_NAMA").Caption = "POLI"
        End If

        grv.Columns("KDDAFTAR_L1_NAMA").Caption = "PENJAMIN"
        grv.Columns("KDCUSTOMER").Caption = "NO RM"
        grv.Columns("KDDOCTOR_NAMA").Caption = "DPJP UTAMA"

        grv.BestFitColumns()

    End Sub
#End Region
#Region "Lookup"
    Private Sub fn_LoadDPJPdanUnit()
        Dim oDPJP As New Reference.clsDoctor
        Try
            grdDPJP.Properties.DataSource = oDPJP.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdDPJP.Properties.ValueMember = "KDDOCTOR"
            grdDPJP.Properties.DisplayMember = "NAME_DISPLAY"

            Dim oUser As New Setting.clsUser
            Dim dsUser = oUser.GetData(sUserID)
            If dsUser IsNot Nothing Then
                grdDPJP.Text = dsUser.KDDOCTOR.ToString()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
#Region "Command Button"
    Private Sub picPrint_Click() Handles picPrint.Click
        Try
            fn_Print()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_LoadSecurity()
    End Sub
    Private Sub btnResetDPJP_Click(sender As Object, e As EventArgs) Handles btnResetDPJP.Click
        grdDPJP.ResetText()
    End Sub
    Private Sub piciCareJKN_Click(sender As Object, e As EventArgs) Handles piciCareJKN.Click
        'Try
        '    Dim VCLAIM_KDDPJP As Integer = 45581
        '    Dim sKARTU As String = "0002077271008"

        '    Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
        '    Dim oKoneksi As New Brigging.clsSetKoneksi
        '    Dim allData = JObject.Parse(oKoneksi.GetDataIcareDevelop(sKARTU, VCLAIM_KDDPJP, uTime, "13973", "rsdust1r4", "6d8412dbc9eb816119015a4676b900ba"))

        '    Dim CodeResponse As String = String.Empty
        '    Dim messageResponse As String = String.Empty

        '    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
        '    messageResponse = allData("metaData")("message").ToString

        '    If CodeResponse = "200" Then
        '        Dim DataDecrypt = JObject.Parse(oKoneksi.Decrypt(allData("response"), "13973" & "rsdust1r4" & uTime))
        '        Dim variabel As String = "CMD /c Start chrome /profile-directory=""Default"" """ & DataDecrypt.Item("url").ToString() & """"
        '        Shell(variabel, vbNormalFocus)
        '    Else
        '        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
        '    End If
        'Catch ex As Exception

        'End Try

        'If grv.GetFocusedRowCellValue("KDKUNJUNGAN") Is Nothing Then
        '    MsgBox("Silahkan Pilih Pasien Terlebih dahulu", MsgBoxStyle.Exclamation, Me.Text)
        '    Exit Sub
        'End If

        Dim oAdmisi As New Admission.clsPendaftaran_Kunjungan
        Dim oSetKoneksi As New Brigging.clsSetKoneksi
        Dim dsCariNosep = oAdmisi.GetData(grv.GetFocusedRowCellValue("KDKUNJUNGAN"))

        If dsCariNosep Is Nothing Then
            MsgBox("Maap Registrasi Tidak di Temukan", MsgBoxStyle.Exclamation, Me.Text)
        Else
            If dsCariNosep.S_PENDAFTARAN_H.KARTUBPJS = "" Then
                MsgBox("No Kartu BPJS Kosong", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data.....")

            Try
                Dim oDokter As New Reference.clsDoctor
                Dim oKoneksi As New Brigging.clsSetKoneksi
                Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim VCLAIM_KDDPJP As Integer = 0

                If dsCariNosep.M_DOCTOR.VCLAIM_KDDPJP = "" Then
                    MsgBox("Icare: " & vbCrLf & "Kode Dokter kosong", MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                Else
                    VCLAIM_KDDPJP = dsCariNosep.M_DOCTOR.VCLAIM_KDDPJP
                End If

                If sURLICARE <> "" Then
                    Dim allData = JObject.Parse(oKoneksi.GetDataIcare(dsCariNosep.S_PENDAFTARAN_H.KARTUBPJS, VCLAIM_KDDPJP, uTime, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, sURLICARE))

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        Dim DataDecrypt = JObject.Parse(oKoneksi.Decrypt(allData("response"), sVclaim_ConsId & sVclaim_SecreatKey & uTime))
                        Dim variabel As String = "CMD /c Start chrome /profile-directory=""Default"" """ & DataDecrypt.Item("url").ToString() & """"
                        Shell(variabel, vbNormalFocus)
                    Else
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    SplashScreenManager.CloseForm(False)
                    MsgBox("Silahkan Setting Koneksi Brigging", MsgBoxStyle.Exclamation, Me.Text)
                End If

            Catch oErr As Exception
                SplashScreenManager.CloseForm(False)
                MsgBox("Icare: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try

            SplashScreenManager.CloseForm(False)
        End If
    End Sub
    Private Sub picPanggilDokterTaksId4_Click(sender As Object, e As EventArgs) Handles picPanggilDokterTaksId4.Click
        If picPanggilDokterTaksId4.Visible = True Then
            Dim oAdmisi As New Admission.clsPendaftaran_Kunjungan
            If grv.GetFocusedRowCellValue("KDKUNJUNGAN") Is Nothing Then
                MsgBox("Silahkan Pilih Pasien Terlebih dahulu", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim dsAdmisi = oAdmisi.GetData(grv.GetFocusedRowCellValue("KDKUNJUNGAN"))
            If dsAdmisi IsNot Nothing Then
                If dsAdmisi.S_PENDAFTARAN_H.KODEBOOKING <> "" Then
                    Try
                        fn_TaskID(dsAdmisi.S_PENDAFTARAN_H.KODEBOOKING, 4)
                        XtraTabList.SelectedTabPageIndex = 1
                        XtraTabList_SelectedPageChanged()

                        Dim oSet_panggilan As New AntrianRS.clsSetPanggilSET_PANGGIL_ANTRIAN
                        Dim oAntrian As New SettingAntrian.clsSetAntrian
                        Dim dsKodebooking = oAntrian.GetData(dsAdmisi.S_PENDAFTARAN_H.KODEBOOKING)

                        If dsKodebooking IsNot Nothing Then
                            ' ***** HEADER *****
                            Dim dsSetPanggilan = oSet_panggilan.GetDataPoli(dsAdmisi.S_PENDAFTARAN_H.M_DEPARTMENT.VCLAIM_KODEPOLI)

                            Dim ds = oSet_panggilan.GetStructureHeaderPoli

                            With ds
                                .LOKET = dsAdmisi.S_PENDAFTARAN_H.M_DEPARTMENT.VCLAIM_KODEPOLI
                                .KODE = Microsoft.VisualBasic.Left(dsKodebooking.NOMORANTREAN, 4)
                                .NOMORANTRIAN = Microsoft.VisualBasic.Right(dsKodebooking.NOMORANTREAN, 3)
                                .DESCRIPTION = "ANTRIAN " & Microsoft.VisualBasic.Mid(.KODE, 1, 1) & " " & Microsoft.VisualBasic.Mid(.KODE, 2, 1) & " " & Microsoft.VisualBasic.Mid(.KODE, 3, 1) & " " & Microsoft.VisualBasic.Mid(.KODE, 4, 1) & " " & dsKodebooking.ANGKAANTREAN & " ATAS NAMA " & dsAdmisi.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY & " MENUJU " & dsAdmisi.S_PENDAFTARAN_H.M_DEPARTMENT.NAME_DISPLAY.ToUpper & IIf(dsAdmisi.S_PENDAFTARAN_H.M_DOCTOR.NAME_ONLINE = "", "", " DOKTER " & dsAdmisi.S_PENDAFTARAN_H.M_DOCTOR.NAME_ONLINE.ToUpper)
                                .ISPANGGIL = False
                                .SISA = 0
                            End With

                            If dsSetPanggilan Is Nothing Then
                                oSet_panggilan.InsertDataPoli(ds)
                            Else
                                oSet_panggilan.UpdateDataPoli(ds)
                            End If
                        Else
                            MsgBox("Kode Booking Tidak di Input, Silahkan Panggil Manual", MsgBoxStyle.Exclamation, Me.Text)
                        End If
                    Catch oErr As Exception
                        MsgBox("Simpan Panggil: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                Else
                    MsgBox("Kode Booking Tidak di Input, Silahkan Panggil Manual", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                MsgBox("Maap Registrasi Tidak di Temukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        End If
    End Sub
    Private Sub picPanggilDokterTaksId5_Click(sender As Object, e As EventArgs) Handles picPanggilDokterTaksId5.Click
        If picPanggilDokterTaksId5.Visible = True Then
            Dim oAdmisi As New Admission.clsPendaftaran_Kunjungan
            If grv.GetFocusedRowCellValue("KDKUNJUNGAN") Is Nothing Then
                MsgBox("Silahkan Pilih Pasien Terlebih dahulu", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim dsAdmisi = oAdmisi.GetData(grv.GetFocusedRowCellValue("KDKUNJUNGAN"))
            If dsAdmisi IsNot Nothing Then
                If dsAdmisi.S_PENDAFTARAN_H.KODEBOOKING <> "" Then
                    Dim oAntrian As New SettingAntrian.clsSetAntrian
                    Dim dsTaskId = oAntrian.GetDataBySaveWaktuTunggu(dsAdmisi.S_PENDAFTARAN_H.KODEBOOKING, 4)

                    If dsTaskId Is Nothing Then
                        MsgBox("Pasien Belum di Panggil (Taks Id 4 Tidak Ada), Silahkan Klik Panggil Terlebih dahulu", MsgBoxStyle.Exclamation, Me.Text)
                    Else
                        fn_TaskID(dsAdmisi.S_PENDAFTARAN_H.KODEBOOKING, 5)
                    End If
                Else
                    MsgBox("Kode Booking Tidak di Input, Silahkan Panggil Manual", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                MsgBox("Maap Registrasi Tidak di Temukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        End If
    End Sub
#End Region
#Region "Event"
    Private Sub grv_DoubleClick(sender As Object, e As EventArgs) Handles grv.DoubleClick
        Dim oAdmisi As New Admission.clsPendaftaran_Kunjungan
        If grv.GetFocusedRowCellValue("KDKUNJUNGAN") Is Nothing Then
            MsgBox("Silahkan Pilih Pasien Terlebih dahulu", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim dsAdmisi = oAdmisi.GetData(grv.GetFocusedRowCellValue("KDKUNJUNGAN"))
        If dsAdmisi IsNot Nothing Then
            If dsAdmisi.S_PENDAFTARAN_H.KODEBOOKING <> "" Then
                Try
                    Dim oSet_panggilan As New AntrianRS.clsSetPanggilSET_PANGGIL_ANTRIAN
                    Dim oAntrian As New SettingAntrian.clsSetAntrian
                    Dim dsKodebooking = oAntrian.GetData(dsAdmisi.S_PENDAFTARAN_H.KODEBOOKING)

                    If dsKodebooking IsNot Nothing Then
                        fn_TaskID(dsAdmisi.S_PENDAFTARAN_H.KODEBOOKING, 4)
                        XtraTabList.SelectedTabPageIndex = 1
                        XtraTabList_SelectedPageChanged()

                        ' ***** HEADER *****
                        Dim dsSetPanggilan = oSet_panggilan.GetDataPoli(dsAdmisi.S_PENDAFTARAN_H.M_DEPARTMENT.VCLAIM_KODEPOLI)

                        Dim ds = oSet_panggilan.GetStructureHeaderPoli

                        With ds
                            .LOKET = dsAdmisi.S_PENDAFTARAN_H.M_DEPARTMENT.VCLAIM_KODEPOLI
                            .KODE = Microsoft.VisualBasic.Left(dsKodebooking.NOMORANTREAN, 4)
                            .NOMORANTRIAN = Microsoft.VisualBasic.Right(dsKodebooking.NOMORANTREAN, 3)
                            .DESCRIPTION = "ANTRIAN " & Microsoft.VisualBasic.Mid(.KODE, 1, 1) & " " & Microsoft.VisualBasic.Mid(.KODE, 2, 1) & " " & Microsoft.VisualBasic.Mid(.KODE, 3, 1) & " " & Microsoft.VisualBasic.Mid(.KODE, 4, 1) & " " & dsKodebooking.ANGKAANTREAN & " ATAS NAMA " & dsAdmisi.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY & " MENUJU " & dsAdmisi.S_PENDAFTARAN_H.M_DEPARTMENT.NAME_DISPLAY.ToUpper & IIf(dsAdmisi.S_PENDAFTARAN_H.M_DOCTOR.NAME_ONLINE = "", "", " DOKTER " & dsAdmisi.S_PENDAFTARAN_H.M_DOCTOR.NAME_ONLINE.ToUpper)
                            .ISPANGGIL = False
                            .SISA = 0
                        End With

                        If dsSetPanggilan Is Nothing Then
                            oSet_panggilan.InsertDataPoli(ds)
                        Else
                            oSet_panggilan.UpdateDataPoli(ds)
                        End If
                    Else
                        XtraTabList.SelectedTabPageIndex = 1
                        XtraTabList_SelectedPageChanged()
                        'MsgBox("Kode Booking Tidak di Input, Silahkan Panggil Manual", MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Catch oErr As Exception
                    MsgBox("Simpan Panggil: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                XtraTabList.SelectedTabPageIndex = 1
                XtraTabList_SelectedPageChanged()
                'MsgBox("Kode Booking Tidak di Input, Silahkan Panggil Manual", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Else
            MsgBox("Maap Registrasi Tidak di Temukan", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub grv_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grv.FocusedRowChanged
        Try
            If grv.GetFocusedRowCellValue("KDKUNJUNGAN") Is Nothing Then
                If XtraTabList.SelectedTabPageIndex <> 0 Then
                    MsgBox("Silahkan Pilih Dulu Pasien", MsgBoxStyle.Exclamation, Me.Text)
                End If

                fn_Empty()
                Exit Sub
            End If

            XtraTabList_SelectedPageChanged()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub XtraTabList_SelectedPageChanged() Handles XtraTabList.SelectedPageChanged
        If lblRM.Text = "" Then
            lblRM.Text = grv.GetFocusedRowCellValue("KDCUSTOMER")
        End If

        If lblRM.Text = "" Then
            MsgBox("Rekam Medis Pasien Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        fn_LoadIdentitas(grv.GetFocusedRowCellValue("KDKUNJUNGAN"))

        Select Case XtraTabList.SelectedTabPage.Name
            Case "tabListPasien"
                fn_Empty()
                XtraTabControlPDF_SelectedPageChanged()
            Case "tabDokumen"
                XtraTabControlDokumenTransaksi_SelectedPageChanged()
                XtraTabControlPDF_SelectedPageChanged()
        End Select

        'If XtraTabList.SelectedTabPageIndex = 0 Then
        '    fn_Empty()
        '    XtraTabControlPDF_SelectedPageChanged()
        'Else
        '    XtraTabControlDokumenTransaksi_SelectedPageChanged()
        '    XtraTabControlPDF_SelectedPageChanged()
        'End If
    End Sub
    Private Sub XtraTabControlDokumenTransaksi_SelectedPageChanged() Handles XtraTabControlDokumenTransaksi.SelectedPageChanged
        If lblRM.Text = "" Then
            MsgBox("Rekam Medis Pasien Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If lblKDKUNJUNGAN.Text = "" Then
            MsgBox("Nomor Kunjungan Pasien Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Select Case XtraTabControlDokumenTransaksi.SelectedTabPage.Name
            Case "tabCPPT"
                '0
                QueryCPPT_LoadHistory(lblRM.Text)
            Case "tabAksi"
                '1
                XtraTabControlTransaksi_SelectedPageChanged()
            Case "tabAsesmenAwalPerawat"
                '2
                QueryAsesmenPerawat_LoadHistory(lblRM.Text)
            Case "tabAsesmenAwalMedis"
                '3
                QueryAsemenMedis_LoadHistory(lblRM.Text)
            Case "tabResumeRawatInap"
                '4
                QueryResumeRawatInap_LoadHistory(lblRM.Text)
            Case "tabLembarObservasi"
                '5

            Case "tabTransferInternal"
                '6
                QueryTransferInternal_LoadHistory(lblRM.Text)
            Case "tabSuratKeterangan"
                '7

            Case "tabKartuObat"
                '8

            Case "tabRencanaOperasi"
                '9
                QueryRencanaOperasi_LoadHistory(lblRM.Text)
            Case "tabLaporanOperasi"
                '10
                QueryLaporanOperasi_LoadHistory(lblRM.Text)
            Case "tabLaporanTindakan"
                '11
                QueryLaporanTindakan_LoadHistory(lblRM.Text)
            Case "tabTindakanEvaluasiKeperawatan"
                '12
                QueryTindakanEvaluasi_LoadHistory(lblRM.Text)
            Case "tabGerdQ"
                '13
                QueryGerdQ_LoadHistory(lblRM.Text)
            Case "tabScan"
                '14
                QueryScan_LoadHistory(lblRM.Text)
            Case "tabInfConsent"
                '15

            Case "tabSuratKematian"
                '16

            Case "tabFormEdukasiPasien"
                '17

            Case "tabCaseManager"
                '18

        End Select

        'Select Case XtraTabControlDokumenTransaksi.SelectedTabPageIndex
        '    Case 0
        '        QueryCPPT_LoadHistory(lblRM.Text)
        '    Case 1

        '    Case 2
        '        QueryAsesmenPerawat_LoadHistory(lblRM.Text)
        '    Case 3
        '        QueryAsemenMedis_LoadHistory(lblRM.Text)
        '    Case 4
        '        QueryResumeRawatInap_LoadHistory(lblRM.Text)
        '    Case 6
        '        QueryTransferInternal_LoadHistory(lblRM.Text)
        '    Case 2
        '        QueryAsemenMedis_LoadHistory(lblRM.Text)
        '    Case 9
        '        QueryRencanaOperasi_LoadHistory(lblRM.Text)
        '    Case 10
        '        QueryLaporanOperasi_LoadHistory(lblRM.Text)
        '    Case 11
        '        QueryLaporanTindakan_LoadHistory(lblRM.Text)
        '    Case 12
        '        QueryTindakanEvaluasi_LoadHistory(lblRM.Text)
        '    Case 13
        '        QueryGerdQ_LoadHistory(lblRM.Text)
        '    Case 14
        '        QueryScan_LoadHistory(lblRM.Text)
        'End Select
    End Sub
    Private Sub XtraTabControlPDF_SelectedPageChanged() Handles XtraTabControlPDF.SelectedPageChanged
        ' Utama Sebelah Kanan

        Select Case XtraTabControlPDF.SelectedTabPage.Name
            Case "tabDokumenRME"
                XtraTabControlDokumenRawatJalanPDF_SelectedPageChanged()
            Case "tabDokumenLainnya"
                XtraTabControlList_SelectedPageChanged()
        End Select

        'If XtraTabControlPDF.SelectedTabPageIndex = 0 Then
        '    XtraTabControlDokumenRawatJalanPDF_SelectedPageChanged()
        'Else
        '    XtraTabControlList_SelectedPageChanged()
        'End If
    End Sub
    Private Sub XtraTabControlTransaksi_SelectedPageChanged() Handles XtraTabControlTransaksi.SelectedPageChanged
        Select Case XtraTabControlTransaksi.SelectedTabPage.Name
            Case "tabBilling1"
                'Aksi Billing
                picAksiBilling_Refresh_Click()
            Case "tabBilling2"
                'Order Lab
            Case "tabBilling3"
                'Order Rad
            Case "tabBilling4"
                'Resep
        End Select
    End Sub
    Private Sub fn_LoadHistoryPasienCPPT(ByVal sKDCUSTOMER As String)
        Try
            PdfViewerCPPTRawatJalan.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data CPPT.....")

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\RME\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            'GetDataByRekamMedis = oConnection.dbRME.R_CPPTs.Where(Function(x) x.A_IDENTITASPASIEN_LIST.KDCUSTOMER = snoRm And x.ISDELETE = False).OrderByDescending(Function(x) x.DATE).ToList()

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "A_IDENTITASPASIEN_LIST A "
            SQL &= "INNER JOIN R_CPPT B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE A.KDCUSTOMER = '" & sKDCUSTOMER & "' "
            SQL &= "AND B.ISDELETE = 0 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "HISTORY_CPPT")

            Dim listCPPT As New List(Of DataAccess.R_CPPT)
            Dim Identitas As Integer = 0

            For iLoop As Integer = 0 To ds.Tables("HISTORY_CPPT").Rows.Count - 1
                Dim dsRekap As New DataAccess.R_CPPT
                With ds.Tables("HISTORY_CPPT")
                    Identitas = .Rows(iLoop)("KDIDENTITAS")
                    dsRekap.DATECREATED = .Rows(iLoop)("DATECREATED")
                    dsRekap.DATEUPDATED = .Rows(iLoop)("DATEUPDATED")
                    dsRekap.DATE = .Rows(iLoop)("DATE")
                    dsRekap.KDIDENTITAS = .Rows(iLoop)("KDIDENTITAS")
                    dsRekap.KDCPPT = .Rows(iLoop)("KDCPPT")
                    dsRekap.KDPROFESI = .Rows(iLoop)("KDPROFESI")
                    dsRekap.SUBJEKTIF_KELUHANUTAMA = .Rows(iLoop)("SUBJEKTIF_KELUHANUTAMA")
                    dsRekap.SUBJEKTIF_ALERGI_TIDAK = .Rows(iLoop)("SUBJEKTIF_ALERGI_TIDAK")
                    dsRekap.SUBJEKTIF_ALERGI_YA = .Rows(iLoop)("SUBJEKTIF_ALERGI_YA")
                    dsRekap.SUBJEKTIF_ALERGI_YA_TEXT = .Rows(iLoop)("SUBJEKTIF_ALERGI_YA_TEXT")
                    dsRekap.OBJEKTIF_KESADARAN = .Rows(iLoop)("OBJEKTIF_KESADARAN")
                    dsRekap.OBJEKTIF_GCS = .Rows(iLoop)("OBJEKTIF_GCS")
                    dsRekap.OBJEKTIF_TAMPAKSAKIT = .Rows(iLoop)("OBJEKTIF_TAMPAKSAKIT")
                    dsRekap.OBJEKTIF_VISUALANALOGSCORE = .Rows(iLoop)("OBJEKTIF_VISUALANALOGSCORE")
                    dsRekap.OBJEKTIF_BERATBADAN = .Rows(iLoop)("OBJEKTIF_BERATBADAN")
                    dsRekap.OBJEKTIF_TINGGIBADAN = .Rows(iLoop)("OBJEKTIF_TINGGIBADAN")
                    dsRekap.OBJEKTIF_SPO2 = .Rows(iLoop)("OBJEKTIF_SPO2")
                    dsRekap.OBJEKTIF_SISTOLE = .Rows(iLoop)("OBJEKTIF_SISTOLE")
                    dsRekap.OBJEKTIF_DIASTOLE = .Rows(iLoop)("OBJEKTIF_DIASTOLE")
                    dsRekap.OBJEKTIF_HR = .Rows(iLoop)("OBJEKTIF_HR")
                    dsRekap.OBJEKTIF_RR = .Rows(iLoop)("OBJEKTIF_RR")
                    dsRekap.OBJEKTIF_SUHU = .Rows(iLoop)("OBJEKTIF_SUHU")
                    dsRekap.OBJEKTIF_PEMERIKSAAN = .Rows(iLoop)("OBJEKTIF_PEMERIKSAAN")
                    dsRekap.OBJEKTIF_ALAMATGAMBARPEMERIKSAAN = .Rows(iLoop)("OBJEKTIF_ALAMATGAMBARPEMERIKSAAN")
                    dsRekap.OBJEKTIF_TEXT = .Rows(iLoop)("OBJEKTIF_TEXT")
                    dsRekap.ASSEMENT_INDIKASI = .Rows(iLoop)("ASSEMENT_INDIKASI")
                    dsRekap.ASSEMENT_TEXT = .Rows(iLoop)("ASSEMENT_TEXT")
                    dsRekap.PLANNING_ISTINDAKLANJUT_PULANG = .Rows(iLoop)("PLANNING_ISTINDAKLANJUT_PULANG")
                    dsRekap.PLANNING_ISTINDAKLANJUT_RAWAT = .Rows(iLoop)("PLANNING_ISTINDAKLANJUT_RAWAT")
                    dsRekap.PLANNING_ISTINDAKLANJUT_KONSUL = .Rows(iLoop)("PLANNING_ISTINDAKLANJUT_KONSUL")
                    dsRekap.PLANNING_ISTINDAKLANJUT_KONSUL_TEXT = .Rows(iLoop)("PLANNING_ISTINDAKLANJUT_KONSUL_TEXT")
                    dsRekap.PLANNING_ISTINDAKLANJUT_RUJUK = .Rows(iLoop)("PLANNING_ISTINDAKLANJUT_RUJUK")
                    dsRekap.PLANNING_ISTINDAKLANJUT_RUJUK_TEXT = .Rows(iLoop)("PLANNING_ISTINDAKLANJUT_RUJUK_TEXT")
                    dsRekap.PLANNING_ALASAN = .Rows(iLoop)("PLANNING_ALASAN")
                    dsRekap.PLANNING_TEXT = .Rows(iLoop)("PLANNING_TEXT")
                    dsRekap.CATATAN = .Rows(iLoop)("CATATAN")
                    dsRekap.KDUSER = .Rows(iLoop)("KDUSER")
                    dsRekap.ISDELETE = .Rows(iLoop)("ISDELETE")
                    dsRekap.DATEDELETE = .Rows(iLoop)("DATEDELETE")
                    dsRekap.USERDELETE = .Rows(iLoop)("USERDELETE")

                    listCPPT.Add(dsRekap)
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            Try
                Dim oConnMySql As New MySql.Data.MySqlClient.MySqlConnection
                Dim oCommMySql As New MySql.Data.MySqlClient.MySqlCommand
                Dim daMySql As MySql.Data.MySqlClient.MySqlDataAdapter
                Dim dsMySql As New DataSet
                Dim MYSQL As String
                dsMySql = New DataSet

                oConnMySql = New MySqlConnection(sMySQL_Url)

                If oConnMySql.State = ConnectionState.Closed Then
                    oConnMySql.Open()
                End If

                MYSQL = "SELECT "
                MYSQL &= "a.KUNJUNGAN "
                MYSQL &= ",a.TANGGAL as tanggal "
                MYSQL &= ",a.SUBYEKTIF "
                MYSQL &= ",a.OBYEKTIF "
                MYSQL &= ",a.ASSESMENT "
                MYSQL &= ",a.PLANNING "
                MYSQL &= ",a.INSTRUKSI "
                MYSQL &= ",d.NAMA "
                MYSQL &= "From medicalrecord.cppt as a "
                MYSQL &= "INNER Join pendaftaran.kunjungan as b "
                MYSQL &= "On a.KUNJUNGAN = b.NOMOR "
                MYSQL &= "INNER Join pendaftaran.pendaftaran as c "
                MYSQL &= "On b.NOPEN = c.NOMOR "
                MYSQL &= "INNER JOIN aplikasi.pengguna as d "
                MYSQL &= "On a.OLEH = d.ID "
                MYSQL &= "WHERE c.NORM = '" & CInt(lblRM.Text) & "' "

                oCommMySql.Connection = oConnMySql
                oCommMySql.CommandText = MYSQL
                oCommMySql.CommandTimeout = 120
                oCommMySql.CommandType = CommandType.Text

                daMySql = New MySql.Data.MySqlClient.MySqlDataAdapter(oCommMySql)
                daMySql.Fill(dsMySql, "medicalrecordcppt")

                If oConnMySql.State = ConnectionState.Open Then
                    oConnMySql.Close()
                End If

                For iLoop As Integer = 0 To dsMySql.Tables("medicalrecordcppt").Rows.Count - 1
                    Dim dsRekap As New DataAccess.R_CPPT
                    With dsMySql.Tables("medicalrecordcppt")
                        dsRekap.DATECREATED = CDate(.Rows(iLoop)("tanggal"))
                        dsRekap.DATEUPDATED = CDate(.Rows(iLoop)("tanggal"))
                        dsRekap.DATE = CDate(.Rows(iLoop)("tanggal"))
                        dsRekap.KDIDENTITAS = Identitas
                        dsRekap.KDCPPT = .Rows(iLoop)("KUNJUNGAN")
                        dsRekap.KDPROFESI = "PROFESI_0000000001"
                        dsRekap.SUBJEKTIF_KELUHANUTAMA = 0
                        dsRekap.SUBJEKTIF_ALERGI_TIDAK = 0
                        dsRekap.SUBJEKTIF_ALERGI_YA = 0
                        dsRekap.SUBJEKTIF_ALERGI_YA_TEXT = .Rows(iLoop)("SUBYEKTIF")
                        dsRekap.OBJEKTIF_KESADARAN = 0
                        dsRekap.OBJEKTIF_GCS = 0
                        dsRekap.OBJEKTIF_TAMPAKSAKIT = 0
                        dsRekap.OBJEKTIF_VISUALANALOGSCORE = 0
                        dsRekap.OBJEKTIF_BERATBADAN = 0
                        dsRekap.OBJEKTIF_TINGGIBADAN = 0
                        dsRekap.OBJEKTIF_SPO2 = 0
                        dsRekap.OBJEKTIF_SISTOLE = 0
                        dsRekap.OBJEKTIF_DIASTOLE = 0
                        dsRekap.OBJEKTIF_HR = 0
                        dsRekap.OBJEKTIF_RR = 0
                        dsRekap.OBJEKTIF_SUHU = 0
                        dsRekap.OBJEKTIF_PEMERIKSAAN = 0
                        dsRekap.OBJEKTIF_ALAMATGAMBARPEMERIKSAAN = 0
                        dsRekap.OBJEKTIF_TEXT = .Rows(iLoop)("OBYEKTIF")
                        dsRekap.ASSEMENT_INDIKASI = 0
                        dsRekap.ASSEMENT_TEXT = .Rows(iLoop)("ASSESMENT")
                        dsRekap.PLANNING_ISTINDAKLANJUT_PULANG = 0
                        dsRekap.PLANNING_ISTINDAKLANJUT_RAWAT = 0
                        dsRekap.PLANNING_ISTINDAKLANJUT_KONSUL = 0
                        dsRekap.PLANNING_ISTINDAKLANJUT_KONSUL_TEXT = 0
                        dsRekap.PLANNING_ISTINDAKLANJUT_RUJUK = 0
                        dsRekap.PLANNING_ISTINDAKLANJUT_RUJUK_TEXT = 0
                        dsRekap.PLANNING_ALASAN = 0
                        dsRekap.PLANNING_TEXT = CleanHtmlToPlainText(.Rows(iLoop)("PLANNING")) & vbCrLf & CleanHtmlToPlainText(.Rows(iLoop)("INSTRUKSI"))
                        dsRekap.CATATAN = 0
                        dsRekap.KDUSER = .Rows(iLoop)("NAMA")
                        dsRekap.ISDELETE = 0
                        dsRekap.DATEDELETE = CDate(.Rows(iLoop)("TANGGAL"))
                        dsRekap.USERDELETE = 0

                        listCPPT.Add(dsRekap)
                    End With
                Next
            Catch oErr As Exception
                SplashScreenManager.CloseForm(False)
                MsgBox("Koneksi CPPT Aplikasi Lama" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try

            If listCPPT.Count > 0 Then
                Dim AlamatCPPT As String = FolderSimpan & lblRM.Text & Now.ToString("yyyyMMddHHmmss") & ".pdf"

                Dim rpt As New xtraDigital_CPPT_01_QR

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.bindingSource.DataSource = listCPPT.OrderByDescending(Function(x) x.DATE)
                rpt.ExportToPdf(AlamatCPPT)

                If FileIO.FileSystem.FileExists(AlamatCPPT) Then
                    PdfViewerCPPTRawatJalan.LoadDocument(AlamatCPPT)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Function CleanHtmlToPlainText(html As String) As String
        If String.IsNullOrWhiteSpace(html) Then Return String.Empty

        Dim s As String = html

        ' 1) Remove script/style blocks
        s = Regex.Replace(s, "(?is)<(script|style)\b.*?>.*?</\1>", String.Empty)

        ' 2) Replace common block tags with newlines
        s = Regex.Replace(s, "(?i)</?(div|p|h[1-6]|section|article)[^>]*>", vbCrLf)

        ' 3) Replace <br> and <br/> with newline
        s = Regex.Replace(s, "(?i)<br\s*/?>", vbCrLf)

        ' 4) Replace <li> with bullet
        s = Regex.Replace(s, "(?i)<li[^>]*>", vbCrLf & "- ")
        s = Regex.Replace(s, "(?i)</li>", String.Empty)

        ' 5) Remove all remaining tags
        s = Regex.Replace(s, "<[^>]+>", String.Empty)

        ' 6) Decode HTML entities
        s = WebUtility.HtmlDecode(s)

        ' 7) Normalize whitespace: collapse multiple newlines and spaces
        s = Regex.Replace(s, "\r\n[\s\r\n]+", vbCrLf)           ' collapse blank lines
        s = Regex.Replace(s, "[ \t]{2,}", " ")                 ' collapse repeated spaces
        s = Regex.Replace(s, "(?:\r\n){3,}", vbCrLf & vbCrLf)   ' limit successive newlines

        ' Trim
        s = s.Trim()

        Return s
    End Function
    Private Sub XtraTabControlDokumenRawatJalanPDF_SelectedPageChanged() Handles XtraTabControlDokumenRawatJalanPDF.SelectedPageChanged
        If lblKDKUNJUNGAN.Text = "" Then
            If grv.GetFocusedRowCellValue("KDKUNJUNGAN") Is Nothing Then
                Exit Sub
            Else
                lblRM.Text = grv.GetFocusedRowCellValue("KDCUSTOMER")
            End If
        End If

        Select Case XtraTabControlDokumenRawatJalanPDF.SelectedTabPage.Name
            Case "tabCPPT_PDF"
                fn_LoadHistoryPasienCPPT(lblRM.Text)
            Case "tabResume_PDF"
                If lblRM.Text <> "" Then
                    QueryResume_LoadHistory(lblRM.Text)
                End If
            Case "tabAsesmenPerawat_PDF"
                If lblRM.Text <> "" Then
                    QueryAsemenKeperawatan_LoadHistoryPDF(lblRM.Text)
                End If
            Case "tabAsesmenMedis_PDF"
                If lblRM.Text <> "" Then
                    QueryAsemenMedis_LoadHistoryPDF(lblRM.Text)
                End If
            Case "tabHasilLab_PDF"
                If lblRM.Text <> "" Then
                    QueryHasilLaboratorium_LoadHistory(lblRM.Text)
                End If
            Case "tabHasilExpertise_PDF"
                If lblRM.Text <> "" Then
                    QueryHasilExpertise_LoadHistory(lblRM.Text)
                End If
            Case "tabLaporanOperasi_PDF"
                If lblRM.Text <> "" Then
                    QueryLaporanOperasi_LoadHistoryPDF(lblRM.Text)
                End If
        End Select

        'Select Case XtraTabControlDokumenRawatJalanPDF.SelectedTabPageIndex
        '    Case 0
        '        'CPPT
        '        Try
        '            PdfViewerCPPTRawatJalan.CloseDocument()

        '            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

        '            SplashScreenManager.Default.SetWaitFormCaption("Processing data CPPT.....")

        '            Dim oGrouperDataCppt As New Grouper.clsR_CPPT

        '            Dim oGetGrouper As New Brigging.clsSetKoneksi
        '            Dim dataList As New List(Of Byte())
        '            Dim AlamatCPPT As String = FolderSimpan & lblRM.Text & Now.ToString("yyyyMMddHHmmss") & ".pdf"
        '            Dim ds = oGrouperDataCppt.GetDataByRekamMedis(lblRM.Text)

        '            If ds.Count > 0 Then
        '                Dim rpt As New xtraDigital_CPPT_01_QR

        '                rpt.ShowPrintMarginsWarning = False
        '                rpt.Watermark.Text = sWATERMARK

        '                rpt.bindingSource.DataSource = ds
        '                rpt.ExportToPdf(AlamatCPPT)

        '                If FileIO.FileSystem.FileExists(AlamatCPPT) Then
        '                    PdfViewerCPPTRawatJalan.LoadDocument(AlamatCPPT)
        '                End If
        '            End If

        '            SplashScreenManager.CloseForm(False)
        '        Catch oErr As Exception
        '            SplashScreenManager.CloseForm(False)
        '            MsgBox("Cetak CPPT Rajal" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        '        End Try
        '    Case 1
        '        If lblRM.Text <> "" Then
        '            QueryResume_LoadHistory(lblRM.Text)
        '        End If
        '    Case 2
        '        If lblRM.Text <> "" Then
        '            QueryAsemenKeperawatan_LoadHistoryPDF(lblRM.Text)
        '        End If
        '    Case 3
        '        If lblRM.Text <> "" Then
        '            QueryAsemenMedis_LoadHistoryPDF(lblRM.Text)
        '        End If
        '    Case 4
        '        If lblRM.Text <> "" Then
        '            QueryHasilLaboratorium_LoadHistory(lblRM.Text)
        '        End If
        '    Case 5
        '        If lblRM.Text <> "" Then
        '            QueryHasilExpertise_LoadHistory(lblRM.Text)
        '        End If
        '    Case 7
        '        If lblRM.Text <> "" Then
        '            QueryLaporanOperasi_LoadHistoryPDF(lblRM.Text)
        '        End If
        'End Select
    End Sub
    Private Sub XtraTabControlList_SelectedPageChanged() Handles XtraTabControlList.SelectedPageChanged
        PdfViewerRincian.CloseDocument()

        'If lblRM.Text = "" Then
        '    MsgBox("Rekam Medis Pasien Kosong", MsgBoxStyle.Exclamation, Me.Text)
        '    Exit Sub
        'End If
        If lblKDKUNJUNGAN.Text = "" Then
            Exit Sub
        End If

        Select Case XtraTabControlList.SelectedTabPageIndex
            Case 0
                Dim oKunjungan As New Admission.clsPendaftaran_Kunjungan
                Dim dsKunjungan = oKunjungan.GetData(lblKDKUNJUNGAN.Text)
                If dsKunjungan IsNot Nothing Then
                    fn_Rincian(dsKunjungan.S_PENDAFTARAN_H.KDPENDAFTARAN)
                End If
            Case 1

        End Select
    End Sub
    Private Sub fn_Rincian(ByVal NoRegister1 As String)
        Try
            Dim oPendaftaran As New Admission.clsPendaftaran
            Dim dsPendaftaran = oPendaftaran.GetData(NoRegister1)
            Dim TanggalPulang As DateTime = Now

            If dsPendaftaran Is Nothing Then
                Exit Sub
            Else
                Dim oPulang As New Admission.clsUpdate_Tanggal_Pulang
                Dim dsPulang = oPulang.GetDatabyKD(NoRegister1)
                If dsPulang IsNot Nothing Then
                    TanggalPulang = dsPulang.DATE
                End If
            End If

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String = frmIncbgList.fn_LoadQuey(NoRegister1)

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            If SQL = "" Then
                Exit Sub
            End If

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALLL")

            Dim listTranskasi As New List(Of R_BILLING)

            For iLoop As Integer = 0 To ds.Tables("ALLL").Rows.Count - 1
                Dim dsRekap As New DataAccess.R_BILLING

                With ds.Tables("ALLL")
                    dsRekap.KDPENDAFTARAN = .Rows(iLoop)("KDPENDAFTARAN")
                    dsRekap.DPJP = .Rows(iLoop)("DPJP")
                    dsRekap.PASIEN = .Rows(iLoop)("PASIEN")
                    dsRekap.KDCUSTOMER = .Rows(iLoop)("KDCUSTOMER")
                    dsRekap.ALAMAT = .Rows(iLoop)("ALAMAT")
                    dsRekap.KELAS = .Rows(iLoop)("KELAS")
                    dsRekap.TANGGAL_DATANG = .Rows(iLoop)("TANGGAL_DATANG")
                    dsRekap.TANGGAL_PULANG = TanggalPulang
                    dsRekap.CATEGORY = .Rows(iLoop)("CATEGORY")
                    dsRekap.KDKUNJUNGAN = .Rows(iLoop)("KDKUNJUNGAN")
                    dsRekap.NAMAKUNJUNGAN = .Rows(iLoop)("NAMAKUNJUNGAN")
                    dsRekap.KDSOTRANSAKSI = .Rows(iLoop)("KDSOTRANSAKSI")
                    dsRekap.TANGGAL_INPUT = .Rows(iLoop)("TANGGAL_INPUT")
                    dsRekap.ITEM_GROUP = .Rows(iLoop)("ITEM_GROUP")
                    dsRekap.ITEM = .Rows(iLoop)("namaproduk")
                    dsRekap.QTY = .Rows(iLoop)("jumlah")
                    dsRekap.GRANDTOTAL = .Rows(iLoop)("hargajual")
                    dsRekap.TUJUAN = dsPendaftaran.M_DEPARTMENT.NAME_DISPLAY
                    dsRekap.KELOMPOKPASIEN = dsPendaftaran.M_DAFTAR_L2.MEMO
                    dsRekap.KDUSER = .Rows(iLoop)("KDUSER")

                    listTranskasi.Add(dsRekap)

                    sPrintSubtotal = .Rows(iLoop)("SUBTOTAL")
                    sPrintPotongan = .Rows(iLoop)("ADMIN")
                    sPrintGrandTotal = .Rows(iLoop)("GRANDTOTALHEADER")
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            Dim rpt As New xtraRincianCasmix

            rpt.ShowPrintMarginsWarning = False
            rpt.Watermark.Text = "SIMRS"

            Dim AlamatCPPT As String = FolderSimpan & lblRM.Text & Now.ToString("yyyyMMddHHmmss") & ".pdf"

            rpt.bindingSource.DataSource = listTranskasi
            rpt.ExportToPdf(AlamatCPPT)

            If FileIO.FileSystem.FileExists(AlamatCPPT) Then
                PdfViewerRincian.LoadDocument(AlamatCPPT)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Category()
        picPanggilDokterTaksId4.Visible = False
        lpicPanggilDokterTaksId4.Visible = False

        picPanggilDokterTaksId5.Visible = False
        lpicPanggilDokterTaksId5.Visible = False

        picDelegasi.Visible = False
        lblDelegasiPemetaan.Visible = False

        If cboTYPE.SelectedIndex = 0 Then
            lblKonsul.Text = "Rujuk " & vbCrLf & "Internal"
            lblPoliDokter.Text = "Dokter"

            lblDelegasiPemetaan.Text = "Pemetaan"
        ElseIf cboTYPE.SelectedIndex = 1 Then
            lblKonsul.Text = "Rujuk " & vbCrLf & "Internal"
            picPanggilDokterTaksId4.Visible = True
            lpicPanggilDokterTaksId4.Visible = True

            picDelegasi.Visible = True
            lblDelegasiPemetaan.Visible = True
            lblDelegasiPemetaan.Text = "Delegasi"

            picPanggilDokterTaksId5.Visible = True
            lpicPanggilDokterTaksId5.Visible = True

            deDATEFrom.DateTime = Now
            deDATETo.DateTime = Now

        ElseIf cboTYPE.SelectedIndex = 2 Then
            lblKonsul.Text = "Mutasi"
            lblPoliDokter.Text = "Dokter"

            picDelegasi.Visible = True
            lblDelegasiPemetaan.Visible = True
            lblDelegasiPemetaan.Text = "Pemetaan"
        End If
    End Sub
    Private Sub cboTYPE_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTYPE.SelectedIndexChanged
        If sSplashScreen = False Then Exit Sub
        fn_Category()
        fn_LoadSecurity()
    End Sub
#End Region
#End Region
#Region "CPPT"
#Region "Function"
    Public Sub fn_TaskID(ByVal kodebooking As String, ByVal isPanggil As Integer)
        Try
            Dim oAntrian As New SettingAntrian.clsSetAntrian
            Dim dsKodebooking = oAntrian.GetData(kodebooking)

            If dsKodebooking IsNot Nothing Then
                Dim dsTaskId = oAntrian.GetDataBySaveWaktuTunggu(dsKodebooking.KODEBOOKING, isPanggil)
                If dsTaskId Is Nothing Then
                    Dim dsTaskIdSimpan = oAntrian.GetStructureHeaderWaktuTunggu
                    With dsTaskIdSimpan
                        Dim oData As New Grouper.clsR_Identitas_Grouper_Data

                        Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()

                        If isPanggil = 5 Then
                            WaktuServer = WaktuServer.AddMinutes(1)
                        End If

                        dsTaskIdSimpan.DATECREATED = WaktuServer
                        dsTaskIdSimpan.DATEUPDATED = WaktuServer
                        dsTaskIdSimpan.KODEBOOKING = dsKodebooking.KODEBOOKING
                        dsTaskIdSimpan.ISPANGGIL = isPanggil
                        dsTaskIdSimpan.DATE = WaktuServer
                        dsTaskIdSimpan.DATE_TEXT = WaktuServer.ToString("ddMMyyyy")
                        dsTaskIdSimpan.KDCUSTOMER = ""
                        dsTaskIdSimpan.REMARKS = "INSERT"
                    End With

                    oAntrian.InsertDataWaktuTungguSemuaKesini(dsTaskIdSimpan)

                    'oAntrian.UpdateDataIsCheked(dsCariNosep.S_PENDAFTARAN_H.KODEBOOKING, isPanggil, "PANGGIL")

                End If
            Else
                MsgBox("Kode Booking Tidak di Temukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Function fn_TaskIDyangLalu(ByVal kodebooking As String, ByVal isPanggil As Integer, ByVal tanggal As DateTime) As Boolean
        Try
            fn_TaskIDyangLalu = False

            Dim oAntrian As New SettingAntrian.clsSetAntrian
            Dim dsKodebooking = oAntrian.GetData(kodebooking)

            If dsKodebooking IsNot Nothing Then
                Dim dsTaskId = oAntrian.GetDataBySaveWaktuTunggu(dsKodebooking.KODEBOOKING, isPanggil)
                If dsTaskId Is Nothing Then
                    Dim dsTaskIdSimpan = oAntrian.GetStructureHeaderWaktuTunggu
                    With dsTaskIdSimpan
                        dsTaskIdSimpan.DATECREATED = tanggal
                        dsTaskIdSimpan.DATEUPDATED = Now
                        dsTaskIdSimpan.KODEBOOKING = dsKodebooking.KODEBOOKING
                        dsTaskIdSimpan.ISPANGGIL = isPanggil
                        dsTaskIdSimpan.DATE = tanggal
                        dsTaskIdSimpan.DATE_TEXT = tanggal.ToString("ddMMyyyy")
                        dsTaskIdSimpan.KDCUSTOMER = ""
                        dsTaskIdSimpan.REMARKS = "INSERT"
                    End With

                    If oAntrian.InsertDataWaktuTungguSemuaKesini(dsTaskIdSimpan) = "" Then
                        fn_TaskIDyangLalu = False
                    Else
                        fn_TaskIDyangLalu = True
                    End If
                Else
                    fn_TaskIDyangLalu = True
                End If
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub QueryCPPT_LoadHistory(ByVal NoRM As String)
        Try
            If sSplashScreen = False Then Exit Sub

            grvCPPT_List.Columns.Clear()
            grdCPPT_List.DataSource = Nothing
            grvCPPT_List.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Processing data.....")

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\RME\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "Select "
            SQL &= "A.KDIDENTITAS "
            SQL &= ", Penjamin = A.KDDAFTAR_L1_NAMA "
            SQL &= ", PROFESI = C.MEMO "
            SQL &= ", B.KDCPPT "
            SQL &= ", CEK = CASE WHEN B.DATECREATED = B.DATEUPDATED THEN '' ELSE 'EDIT' END "
            SQL &= ",TANGGALDAFATAR = A.DATE "
            SQL &= ",B.DATE "
            SQL &= ",B.DATECREATED "
            SQL &= ",B.DATEUPDATED "
            SQL &= ",TUJUAN = A.KDDEPARTMENT_NAMA "
            SQL &= ",DOKTER = A.KDDOCTOR_NAMA "
            SQL &= ",B.KDUSER "
            SQL &= "FROM "
            SQL &= "A_IDENTITASPASIEN_LIST A "
            SQL &= "INNER JOIN R_CPPT B "
            SQL &= "ON A.KDIDENTITAS= B.KDIDENTITAS "
            SQL &= "INNER JOIN R_CPPT_M_PROFESI C "
            SQL &= "ON B.KDPROFESI = C.KDPROFESI "
            SQL &= "WHERE A.KDCUSTOMER = '" & NoRM & "' "
            SQL &= "AND ISDELETE = 0 "
            SQL &= "ORDER BY B.KDCPPT DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "R_IDENTITAS_GROUPER_CPPT")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grdCPPT_List.MainView = grvCPPT_List
            grdCPPT_List.DataSource = ds.Tables("R_IDENTITAS_GROUPER_CPPT")
            grdCPPT_List.ForceInitialize()

            SplashScreenManager.CloseForm(False)

            QyeryCPPT_LoadFormatData()

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub QyeryCPPT_LoadFormatData()
        For iLoop As Integer = 0 To grvCPPT_List.Columns.Count - 1
            If grvCPPT_List.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvCPPT_List.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvCPPT_List.Columns(iLoop).DisplayFormat.FormatString = "{0:n0}"
                grvCPPT_List.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvCPPT_List.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvCPPT_List.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvCPPT_List.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm:ss}"
            End If
        Next

        grvCPPT_List.Columns("KDIDENTITAS").Visible = False
        grvCPPT_List.Columns("KDCPPT").Visible = False
        grvCPPT_List.Columns("CEK").Visible = False
        grvCPPT_List.Columns("DATECREATED").Visible = False
        grvCPPT_List.Columns("DATEUPDATED").Visible = False
        grvCPPT_List.Columns("DATE").Caption = "TANGGAL CPPT"
        grvCPPT_List.Columns("DATECREATED").Caption = "TANGGAL BUAT"
        grvCPPT_List.Columns("DATEUPDATED").Caption = "TANGGAL EDIT"
        grvCPPT_List.Columns("KDUSER").Caption = "USER"
        grvCPPT_List.Columns("PROFESI").Caption = "PROFESI"
        grvCPPT_List.Columns("TUJUAN").Caption = "POLI/RUANGAN"

        grvCPPT_List.BestFitColumns()
    End Sub
#End Region
#Region "Command Button"
    Private Sub grvCPPT_List_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grvCPPT_List.RowStyle
        If grvCPPT_List.IsFilterRow(e.RowHandle) Then Exit Sub
        If grvCPPT_List.GetRowCellValue(e.RowHandle, "CEK") <> "" Then
            e.Appearance.BackColor = Color.Violet
        End If
    End Sub
    Private Sub picDelegasi_Click(sender As Object, e As EventArgs) Handles picDelegasi.Click
        If grv.GetFocusedRowCellValue("KDKUNJUNGAN") Is Nothing Then
            MsgBox("Silahkan Pilih Pasien Terlebih dahulu", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim Kode As String = grv.GetFocusedRowCellValue("KDKUNJUNGAN")

        If cboTYPE.SelectedIndex = 1 Then
            Dim frmDelegasi As New frmDelegasi
            Try
                Dim oDelegasi As New Reference.clsDelegasi
                Dim dsDelegasi = oDelegasi.GetDataKodeKunjungan(Kode)
                If dsDelegasi IsNot Nothing Then
                    frmDelegasi.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDelegasi.KDKUNJUNGAN, dsDelegasi.KDDELEGASI)
                    frmDelegasi.ShowDialog(Me)
                Else
                    frmDelegasi.LoadMe(FORM_MODE.FORM_MODE_ADD, Kode)
                    frmDelegasi.ShowDialog(Me)
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmDelegasi Is Nothing Then frmDelegasi.Dispose()
                frmDelegasi = Nothing
            End Try
        Else
            Dim oKunjungan As New Admission.clsPendaftaran_Kunjungan

            Dim dsKunjungan = oKunjungan.GetData(Kode)
            If dsKunjungan IsNot Nothing Then
                Dim frmKelasAplicarePerRuangan As New frmKelasAplicarePerRuangan
                Try
                    frmKelasAplicarePerRuangan.LoadMe(dsKunjungan.KDDEPARTMENT)
                    frmKelasAplicarePerRuangan.ShowDialog(Me)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                Finally
                    If Not frmKelasAplicarePerRuangan Is Nothing Then frmKelasAplicarePerRuangan.Dispose()
                    frmKelasAplicarePerRuangan = Nothing
                End Try
            Else
                MsgBox("Kunjungan Kosong", MsgBoxStyle.Exclamation, Me.Text)
            End If
        End If
    End Sub
    Private Sub picCPPT_Konsul_Click(sender As Object, e As EventArgs) Handles picCPPT_Konsul.Click
        Dim frmPendaftaran_Kunjungan As New frmPendaftaran_Kunjungan
        Try
            Dim oAdmisi As New Admission.clsPendaftaran_Kunjungan
            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim dsCariNosep = oAdmisi.GetData(lblKDKUNJUNGAN.Text)

            If dsCariNosep Is Nothing Then
                MsgBox("Maap Registrasi Tidak di Temukan", MsgBoxStyle.Exclamation, Me.Text)
            Else
                frmPendaftaran_Kunjungan.LoadMe(FORM_MODE.FORM_MODE_ADD, dsCariNosep.KDPENDAFTARAN)
                frmPendaftaran_Kunjungan.ShowDialog(Me)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmPendaftaran_Kunjungan Is Nothing Then frmPendaftaran_Kunjungan.Dispose()
            frmPendaftaran_Kunjungan = Nothing
        End Try
    End Sub
    Private Sub btnCPPT_Back_Click(sender As Object, e As EventArgs) Handles btnCPPT_btnBack.Click
        pnlCPPT_pic.Dock = DockStyle.Top

        XtraTabList_SelectedPageChanged()
        lCPPT_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lCPPT_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lCPPT_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If Not frmCPPT Is Nothing Then frmCPPT.Dispose()
        frmCPPT = Nothing
    End Sub
    'Private Sub btnCPPT_BukaTutupList_Click(sender As Object, e As EventArgs) Handles btnCPPT_BukaTutupList.Click
    '    If btnCPPT_BukaTutupList.Text = "Tutup" Then
    '        btnCPPT_BukaTutupList.Text = "Buka"
    '        grdCPPT_PDF.Visible = False
    '    Else
    '        btnCPPT_BukaTutupList.Text = "Tutup"
    '        grdCPPT_PDF.Visible = True
    '    End If
    'End Sub
    Private Sub picCPPT_Add_Click(sender As Object, e As EventArgs) Handles picCPPT_Add.Click
        If lblKDKUNJUNGAN.Text = "" Then
            MsgBox("Silahkan Pilih Dulu Pasien", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim oDataGrouper As New Admission.clsPendaftaran_Kunjungan
        Dim ds = oDataGrouper.GetDatabyKodeKunjungan(lblKDKUNJUNGAN.Text)

        If ds IsNot Nothing Then
            Dim sRead As Boolean = False
            Dim oCashin As New Finance.clsCashIn
            Dim oPendaftaranKunjungan As New Admission.clsPendaftaran_Kunjungan
            Dim dsKunjungan = oPendaftaranKunjungan.GetData(ds.KDKUNJUNGAN)
            If dsKunjungan IsNot Nothing Then
                'If dsKunjungan.S_PENDAFTARAN_H.CATEGORY = 0 Then
                '    If dsKunjungan.S_PENDAFTARAN_H.M_DEPARTMENT.VCLAIM_KODEPOLI <> "IGD" Then
                '        Dim oAntrian As New SettingAntrian.clsSetAntrian
                '        Dim dsTaskId = oAntrian.GetDataBySaveWaktuTunggu(dsKunjungan.S_PENDAFTARAN_H.KODEBOOKING, 4)

                '        If dsTaskId Is Nothing Then
                '            MsgBox("Pasien Belum di Panggil (Taks Id 4 Tidak Ada), Silahkan Klik Panggil Terlebih dahulu", MsgBoxStyle.Exclamation, Me.Text)
                '            Exit Sub
                '        End If
                '    End If
                'End If

                Dim dsCekBayarKasir = oCashin.GetDataByKdPendaftaran(dsKunjungan.KDPENDAFTARAN)
                If dsCekBayarKasir IsNot Nothing Then
                    MsgBox("Data Pasien Sudah di Input Kasir, Jika Ada perubahan silahkan hubungi Kasir", MsgBoxStyle.Exclamation, Me.Text)
                    'sRead = True
                    'Exit Sub
                End If
            Else
                MsgBox("Kunjungan Kosong", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            pnlCPPT_pic.Dock = DockStyle.None

            lCPPT_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lCPPT_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lCPPT_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            Try
                ' Bersihkan konten panel
                pnlCPPT_Transaksi.Controls.Clear()

                ' Set properti form anak
                frmCPPT.TopLevel = False
                frmCPPT.FormBorderStyle = FormBorderStyle.None
                frmCPPT.Dock = DockStyle.Fill

                ' Tambahkan form anak ke panel
                pnlCPPT_Transaksi.Controls.Add(frmCPPT)

                ' Tampilkan form anak
                frmCPPT.LoadMe(FORM_MODE.FORM_MODE_ADD, ds.KDIDENTITAS, sRead)
                frmCPPT.Show()
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Else
            MsgBox("Data Tidak ditemukan di List RME", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub picCPPT_Update_Click(sender As Object, e As EventArgs) Handles picCPPT_Update.Click
        If grvCPPT_List.GetFocusedRowCellValue("KDCPPT") Is Nothing Then
            MsgBox("Silahkan Pilih Kode", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If grvCPPT_List.GetFocusedRowCellValue("KDUSER") <> sUserID Then
            MsgBox("Maap Tidak dapat Rubah Silahkan Hubungi User " & grvCPPT_List.GetFocusedRowCellValue("KDUSER"), MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim oDigital As New Grouper.clsR_CPPT

        Dim dsDigital = oDigital.GetData(grvCPPT_List.GetFocusedRowCellValue("KDCPPT"))

        If dsDigital IsNot Nothing Then
            Try
                Dim frmPesanDelete As New frmPesanDelete
                frmPesanDelete.ShowDialog(Me)

                If sPesanHapus <> "XXXXXBATALXXXXX" Then
                    Dim oDelete As New Setting.clsDelete

                    If oDelete.InsertData("CPPTUPDATE", sUserID, sPesanHapus, dsDigital.KDCPPT) = True Then
                        Try
                            Dim sRead As Boolean = False
                            Dim oCashin As New Finance.clsCashIn
                            Dim oPendaftaranKunjungan As New Admission.clsPendaftaran_Kunjungan
                            Dim dsKunjungan = oPendaftaranKunjungan.GetData(dsDigital.A_IDENTITASPASIEN_LIST.KDKUNJUNGAN)
                            If dsKunjungan IsNot Nothing Then
                                Dim dsCekBayarKasir = oCashin.GetDataByKdPendaftaran(dsKunjungan.KDPENDAFTARAN)
                                If dsCekBayarKasir IsNot Nothing Then
                                    'sRead = True
                                    MsgBox("Data Pasien Sudah di Input Kasir, Jika Ada perubahan silahkan hubungi Kasir", MsgBoxStyle.Exclamation, Me.Text)
                                    'Exit Sub
                                End If
                            Else
                                MsgBox("Kunjungan Kosong", MsgBoxStyle.Exclamation, Me.Text)
                                Exit Sub
                            End If

                            pnlCPPT_pic.Dock = DockStyle.None

                            lCPPT_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                            lCPPT_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                            lCPPT_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always


                            ' Bersihkan konten panel
                            pnlCPPT_Transaksi.Controls.Clear()

                            ' Set properti form anak
                            frmCPPT.TopLevel = False
                            frmCPPT.FormBorderStyle = FormBorderStyle.None
                            frmCPPT.Dock = DockStyle.Fill

                            ' Tambahkan form anak ke panel
                            pnlCPPT_Transaksi.Controls.Add(frmCPPT)

                            ' Tampilkan form anak
                            frmCPPT.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.KDIDENTITAS, sRead, dsDigital.KDCPPT)
                            frmCPPT.Show()
                        Catch ex As Exception
                            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                        End Try
                    Else
                        MsgBox("Insert Delete Gagal", MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    MsgBox("Batal Update", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Else
            MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub picCPPT_Delete_Click(sender As Object, e As EventArgs) Handles picCPPT_Delete.Click
        If grvCPPT_List.GetFocusedRowCellValue("KDCPPT") Is Nothing Then
            MsgBox("Silahkan Pilih Kode", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If grvCPPT_List.GetFocusedRowCellValue("KDUSER") <> sUserID Then
            MsgBox("Maap Tidak dapat Rubah Silahkan Hubungi User " & grvCPPT_List.GetFocusedRowCellValue("KDUSER"), MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim kodecppt As String = grvCPPT_List.GetFocusedRowCellValue("KDCPPT")

        Dim oDigitalCPPT As New Grouper.clsR_CPPT

        Dim dsDigital = oDigitalCPPT.GetData(kodecppt)
        If dsDigital IsNot Nothing Then
            Dim oCashin As New Finance.clsCashIn
            Dim oPendaftaranKunjungan As New Admission.clsPendaftaran_Kunjungan
            Dim dsKunjungan = oPendaftaranKunjungan.GetData(dsDigital.A_IDENTITASPASIEN_LIST.KDKUNJUNGAN)
            If dsKunjungan IsNot Nothing Then
                Dim dsCekBayarKasir = oCashin.GetDataByKdPendaftaran(dsKunjungan.KDPENDAFTARAN)
                If dsCekBayarKasir IsNot Nothing Then
                    MsgBox("Data Pasien Sudah di Input Kasir, Jika Ada perubahan silahkan hubungi Kasir", MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                End If
            Else
                MsgBox("Kunjungan Kosong", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
        End If

        If MsgBox("Apakah Yakin Data Akan di Hapus", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        Dim frmPesanDelete As New frmPesanDelete
        frmPesanDelete.ShowDialog(Me)

        If sPesanHapus <> "XXXXXBATALXXXXX" Then
            Dim oDelete As New Setting.clsDelete

            If oDelete.InsertData("DELETECPPT", sUserID, sPesanHapus, kodecppt) = True Then
                Dim oDigital As New Grouper.clsR_CPPT
                Dim oDigitalOrder As New Digital.clsR_Order

                oDigital.DeleteData(kodecppt, sUserID)

                For Each xloop In oDigitalOrder.GetDataDetailNoReference(kodecppt)
                    oDigitalOrder.UpdateStatus(xloop.KDORDER, "HAPUS")
                Next

                XtraTabList_SelectedPageChanged()
            End If
        End If
    End Sub
    Private Sub picCPPT_Refresh_Click(sender As Object, e As EventArgs) Handles picCPPT_Refresh.Click
        XtraTabList_SelectedPageChanged()
        lCPPT_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lCPPT_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lCPPT_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If Not frmCPPT Is Nothing Then frmCPPT.Dispose()
        frmCPPT = Nothing
    End Sub
    Private Sub picCPPT_CetakSKD_Click(sender As Object, e As EventArgs) Handles picCPPT_CetakSKD.Click
        Dim oSkd As New Admission.clsSKD
        Dim oKunjungan As New Admission.clsPendaftaran_Kunjungan

        Dim dsKunjungan = oKunjungan.GetData(lblKDKUNJUNGAN.Text)
        If dsKunjungan IsNot Nothing Then
            Dim dsSKD = oSkd.GetDataPendaftaran(dsKunjungan.KDPENDAFTARAN)
            If dsSKD IsNot Nothing Then
                Try
                    Dim rpt As New xtraRencanaKontrol

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK
                    Dim ds = oSkd.GetDataOfline(dsSKD.KDSKD)

                    rpt.bindingSource.DataSource = ds
                    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                    printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        End If
    End Sub
#End Region
#End Region
#Region "Resume Rawat Jalan"
#Region "Fungction"
    Private Sub QueryResume_LoadHistory(ByVal NoRM As String)
        Try
            If sSplashScreen = False Then Exit Sub

            grvResume_List.Columns.Clear()
            grdResume_List.DataSource = Nothing
            grvResume_List.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Processing data.....")

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\RME\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If


            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM ( "
            SQL &= "SELECT "
            SQL &= "KETERANGAN = 'RESUME RAWAT JALAN' "
            SQL &= ",KODE = B.KDCPPT "
            SQL &= ",B.DATE "
            SQL &= ",TUJUAN = A.KDDEPARTMENT_NAMA "
            SQL &= ",DOKTER = A.KDDOCTOR_NAMA "
            SQL &= ",B.KDUSER "
            SQL &= "FROM "
            SQL &= "A_IDENTITASPASIEN_LIST A "
            SQL &= "INNER JOIN R_CPPT B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "INNER JOIN R_CPPT_M_PROFESI C "
            SQL &= "ON B.KDPROFESI = C.KDPROFESI "
            SQL &= "WHERE A.KDCUSTOMER = '" & NoRM & "' "
            SQL &= "AND ISDELETE = 0 "
            SQL &= "AND C.MEMO = 'DOKTER' "
            SQL &= "AND A.CATEGORY = 0 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KETERANGAN = 'RESUME RAWAT INAP' "
            SQL &= ",KODE = B.KDREG "
            SQL &= ",DATE = B.TANGGALMASUK "
            SQL &= ",TUJUAN = A.KDDEPARTMENT_NAMA "
            SQL &= ",DOKTER = A.KDDOCTOR_NAMA "
            SQL &= ",B.KDUSER "
            SQL &= "FROM "
            SQL &= "A_IDENTITASPASIEN_LIST A "
            SQL &= "INNER JOIN S_RINGKASANKELUARRAWATINAP B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE A.KDCUSTOMER = '" & NoRM & "' "
            SQL &= ") X "
            SQL &= "ORDER BY "
            SQL &= "X.DATE DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "R_IDENTITAS_GROUPER_CPPT_RESUME")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grdResume_List.MainView = grvResume_List
            grdResume_List.DataSource = ds.Tables("R_IDENTITAS_GROUPER_CPPT_RESUME")
            grdResume_List.ForceInitialize()

            SplashScreenManager.CloseForm(False)

            QyeryResume_LoadFormatData()

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub QyeryResume_LoadFormatData()
        For iLoop As Integer = 0 To grvResume_List.Columns.Count - 1
            If grvResume_List.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvResume_List.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvResume_List.Columns(iLoop).DisplayFormat.FormatString = "{0:n0}"
                grvResume_List.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvResume_List.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvResume_List.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvResume_List.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm}"
            End If
        Next

        grvResume_List.Columns("KODE").Visible = False
        grvResume_List.Columns("DATE").Caption = "Tanggal"
        grvResume_List.Columns("KDUSER").Caption = "USER"
        grvResume_List.BestFitColumns()
    End Sub
    Private Sub xtraReportResumeRawatJalan(ByVal Kode As String)
        Try
            PdfViewerResume.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Resume.....")

            Dim dataList As New List(Of Byte())
            Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim oGrouperDataCppt As New Grouper.clsR_CPPT
            Dim ds = oGrouperDataCppt.GetData(Kode)

            If ds IsNot Nothing Then
                Dim rpt As New xtraResumeRawatJalan

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.bindingSource.DataSource = ds
                rpt.ExportToPdf(Alamat)

                If FileIO.FileSystem.FileExists(Alamat) Then
                    PdfViewerResume.LoadDocument(Alamat)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Resume Rajal" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub xtraReportResumeRawatInap(ByVal Kode As String)
        Try
            PdfViewerResume.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Resume.....")

            Dim dataList As New List(Of Byte())
            Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim oGrouperDataCppt As New EMedrek.clsRingkasanKeluar
            Dim ds = oGrouperDataCppt.GetData(Kode)

            If ds IsNot Nothing Then
                Dim rpt As New xtraRingkasanKeluarRawatInap

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.bindingSource.DataSource = ds
                rpt.ExportToPdf(Alamat)

                If FileIO.FileSystem.FileExists(Alamat) Then
                    PdfViewerResume.LoadDocument(Alamat)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Resume Rajal" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
#Region "Command Button"
    Private Sub btnResume_BukaTutupList_Click(sender As Object, e As EventArgs) Handles btnResume_BukaTutupList.Click
        If btnResume_BukaTutupList.Text = "Tutup" Then
            btnResume_BukaTutupList.Text = "Buka"
            grdResume_List.Visible = False
        Else
            btnResume_BukaTutupList.Text = "Tutup"
            grdResume_List.Visible = True
        End If
    End Sub
    Private Sub grvResume_List_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvResume_List.FocusedRowChanged
        Try
            If grvResume_List.GetFocusedRowCellValue("KODE") Is Nothing Then
                PdfViewerResume.CloseDocument()
                Exit Sub
            End If

            If grvResume_List.GetFocusedRowCellValue("KETERANGAN") = "RESUME RAWAT JALAN" Then
                xtraReportResumeRawatJalan(grvResume_List.GetFocusedRowCellValue("KODE"))
            ElseIf grvResume_List.GetFocusedRowCellValue("KETERANGAN") = "RESUME RAWAT INAP" Then
                xtraReportResumeRawatInap(grvResume_List.GetFocusedRowCellValue("KODE"))
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
#End Region
#Region "Laboratorium"
#Region "Function"
    Private Sub QueryHasilLaboratorium_LoadHistory(ByVal NoRM As String)
        Try
            If sSplashScreen = False Then Exit Sub

            grvHasilLaboratorium_List.Columns.Clear()
            grdHasilLaboratorium_List.DataSource = Nothing
            grvHasilLaboratorium_List.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Processing data.....")

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\RME\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            'SQL = "SELECT "
            'SQL &= "D.KDSOTRANSAKSI "
            'SQL &= ",KELOMPOK = CASE F.NMITEM1 WHEN 'SWAB' THEN F.NMITEM1 ELSE '-' END "
            'SQL &= ",E.KDITEM "
            'SQL &= ",TANGGAL = D.DATE "
            'SQL &= ",NAMAPASIEN = C.NAME_DISPLAY "
            'SQL &= ",DOKTERPENGIRIM = H.NAME_DISPLAY "
            ''SQL &= ",DOKTERLABORATORIUM = ISNULL((SELECT BB.NAME_DISPLAY FROM S_SO_TRANSAKSI_D_HASIL_LABORATORIUM AA INNER JOIN M_DOCTOR BB ON AA.KDDOCTOR = BB.KDDOCTOR WHERE D.KDSOTRANSAKSI = AA.KDSOTRANSAKSI AND E.SEQ = AA.SEQ_SO GROUP BY BB.NAME_DISPLAY, AA.SEQ_SO) , 'BELUM') "
            ''SQL &= ",SEQ_SO = ISNULL((SELECT AA.SEQ_SO FROM S_SO_TRANSAKSI_D_HASIL_LABORATORIUM AA WHERE D.KDSOTRANSAKSI = AA.KDSOTRANSAKSI AND E.SEQ = AA.SEQ_SO GROUP BY AA.SEQ_SO) , 100) "
            'SQL &= ",TINDAKAN = F.NMITEM2 "
            ''SQL &= ",BAYAR = (SELECT CASE D.PAYAMOUNT WHEN 0 THEN CONVERT(BIT, 0) ELSE CONVERT(BIT, 1) END) "
            'SQL &= ",D.KDUSER "
            'SQL &= ",E.KDDOCTOR "
            'SQL &= "FROM "
            'SQL &= "S_PENDAFTARAN_H A "
            'SQL &= "INNER JOIN S_PENDAFTARAN_KUNJUNGAN B "
            'SQL &= "ON A.KDPENDAFTARAN = B.KDPENDAFTARAN "
            'SQL &= "INNER JOIN M_CUSTOMER C "
            'SQL &= "ON A.KDCUSTOMER = C.KDCUSTOMER "
            'SQL &= "INNER JOIN S_SO_TRANSAKSI_H D "
            'SQL &= "ON B.KDKUNJUNGAN = D.KDKUNJUNGAN "
            'SQL &= "INNER JOIN S_SO_TRANSAKSI_D E "
            'SQL &= "ON D.KDSOTRANSAKSI = E.KDSOTRANSAKSI "
            'SQL &= "INNER JOIN M_ITEM F "
            'SQL &= "ON E.KDITEM = F.KDITEM "
            'SQL &= "INNER JOIN M_ITEM_L3 G "
            'SQL &= "ON F.KDITEM_L3 = G.KDITEM_L3 "
            'SQL &= "INNER JOIN M_DOCTOR H "
            'SQL &= "ON D.KDDOCTOR = H.KDDOCTOR "
            'SQL &= "WHERE A.KDCUSTOMER = '" & NoRM & "' "
            'SQL &= "AND G.MEMO = 'LABORATORIUM' "
            'SQL &= "AND ISNULL((SELECT TOP 1 'YA' FROM S_SO_TRANSAKSI_D_HASIL_LABORATORIUM AA WHERE D.KDSOTRANSAKSI = AA.KDSOTRANSAKSI AND AA.APPROVE = 1) , 'NO') = 'YA' "
            'SQL &= "ORDER BY B.DATE DESC "

            SQL = "EXEC ORDERLABORATORIUM_REKAMMEDIS @KDCUSTOMER = '" & NoRM & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_SO_TRANSAKSI_D_HASIL_LABORATORIUM")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grdHasilLaboratorium_List.MainView = grvHasilLaboratorium_List
            grdHasilLaboratorium_List.DataSource = ds.Tables("S_SO_TRANSAKSI_D_HASIL_LABORATORIUM")
            grdHasilLaboratorium_List.ForceInitialize()

            SplashScreenManager.CloseForm(False)

            QyeryHasilLaboratorium_LoadFormatData()

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub QyeryHasilLaboratorium_LoadFormatData()
        For iLoop As Integer = 0 To grvHasilLaboratorium_List.Columns.Count - 1
            If grvHasilLaboratorium_List.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvHasilLaboratorium_List.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvHasilLaboratorium_List.Columns(iLoop).DisplayFormat.FormatString = "{0:n0}"
                grvHasilLaboratorium_List.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvHasilLaboratorium_List.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvHasilLaboratorium_List.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvHasilLaboratorium_List.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm}"
            End If
        Next

        grvHasilLaboratorium_List.Columns("KDSOTRANSAKSI").Visible = False
        grvHasilLaboratorium_List.Columns("KDSOTRANSAKSI").OptionsColumn.ShowInCustomizationForm = False
        'grvHasilLaboratorium_List.Columns("KELOMPOK").Visible = False
        'grvHasilLaboratorium_List.Columns("KELOMPOK").OptionsColumn.ShowInCustomizationForm = False
        grvHasilLaboratorium_List.Columns("KDITEM").Visible = False
        grvHasilLaboratorium_List.Columns("KDITEM").OptionsColumn.ShowInCustomizationForm = False
        'grvHasilLaboratorium_List.Columns("KDDOCTOR").Visible = False
        'grvHasilLaboratorium_List.Columns("KDDOCTOR").OptionsColumn.ShowInCustomizationForm = False
        'grvHasilLaboratorium_List.Columns("SEQ_SO").Visible = False
        'grvHasilLaboratorium_List.Columns("SEQ_SO").OptionsColumn.ShowInCustomizationForm = False
        'grvHasilLaboratorium_List.Columns("TANGGAL").Caption = "Tanggal"
        'grvHasilLaboratorium_List.Columns("KELOMPOK").Caption = "Kelompok"
        'grvHasilLaboratorium_List.Columns("NAMAPASIEN").Caption = "Nama Pasien"
        'grvHasilLaboratorium_List.Columns("DOKTERPENGIRIM").Caption = "Dokter Pengirim"
        'grvHasilLaboratorium_List.Columns("DOKTERLABORATORIUM").Caption = "Dokter Laboratorium"
        'grvHasilLaboratorium_List.Columns("TINDAKAN").Caption = "Tindakan"
        'grvHasilLaboratorium_List.Columns("KDUSER").Caption = "User"
        grvHasilLaboratorium_List.BestFitColumns()
    End Sub
    Private Sub xtraReportHasilLaboratoriumSwab(ByVal KDSOTRANSAKSI As String, ByVal KDITEM As String)
        Try
            Dim oOrder As New Sales.clsSalesOrderTransaksi

            PdfViewerHasilLaboratorium.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Hasil Laboratorium.....")

            Dim dataList As New List(Of Byte())
            Dim Alamat As String = FolderSimpan & KDSOTRANSAKSI & KDITEM & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim ds = oOrder.GetDataPCR(KDSOTRANSAKSI, KDITEM)

            If ds IsNot Nothing Then
                Dim rpt As New xtraPCR

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.bindingSource.DataSource = ds
                rpt.ExportToPdf(Alamat)

                If FileIO.FileSystem.FileExists(Alamat) Then
                    PdfViewerHasilLaboratorium.LoadDocument(Alamat)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Hasil Laboratorium" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub xtraReportHasilLaboratorium(ByVal KDSOTRANSAKSI As String)
        Try
            PdfViewerHasilLaboratorium.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Hasil Laboratorium.....")

            Dim oSalesOrder As New Sales.clsSalesOrderTransaksi
            Dim oRME As New RME.clsRME

            Dim ds = oSalesOrder.GetData(KDSOTRANSAKSI)
            If ds IsNot Nothing Then
                Dim rpt As New xtraHasilLabSementara

                sSIPNIP = ds.S_SO_TRANSAKSI_D_HASIL_LABORATORIUMs.FirstOrDefault.M_DOCTOR.SIP
                sUSIA = oRME.GetUmurPasien(ds.DATE, ds.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK
                rpt.bindingSource.DataSource = ds

                Dim alamatlab As String = FolderSimpan & "ZLAB" & KDSOTRANSAKSI & Now.ToString("yyyyMMddHHmmss") & ".pdf"

                If FileIO.FileSystem.FileExists(alamatlab) Then
                    PdfViewerHasilLaboratorium.LoadDocument(alamatlab)
                End If
            End If

            'Dim oRME As New RME.clsRME
            'Dim oADokumen As New Digital.clsDigital_A_Dokumen
            'Dim dataList As New List(Of Byte())
            'Dim Alamat As String = FolderSimpan & KDSOTRANSAKSI & Now.ToString("yyyyMMddHHmmss") & ".pdf"

            'Dim oHasil As New Grouper.clsHasiLab

            'Dim ds = oADokumen.GetData(KDSOTRANSAKSI & KDHASILLAB)

            'If ds IsNot Nothing Then
            '    Dim dsHasilItem = oHasil.GetDataDetailTransaksikditem(KDSOTRANSAKSI, KDHASILLAB)

            '    If dsHasilItem IsNot Nothing Then
            '        sUSIA = oRME.GetUmurPasien(dsHasilItem.FirstOrDefault.S_SO_TRANSAKSI_H.DATE, dsHasilItem.FirstOrDefault.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)

            '        Dim rpt As New xtraHasilLabAkhir

            '        rpt.ShowPrintMarginsWarning = False
            '        rpt.Watermark.Text = sWATERMARK
            '        rpt.bindingSource.DataSource = dsHasilItem
            '        rpt.ExportToPdf(Alamat)

            '        If FileIO.FileSystem.FileExists(Alamat) Then
            '            PdfViewerHasilLaboratorium.LoadDocument(Alamat)
            '        End If
            '    End If
            'Else
            '    Dim dsCekHasilLab = oHasil.GetDataDetailTransaksikditem(KDSOTRANSAKSI, KDHASILLAB)

            '    If dsCekHasilLab IsNot Nothing Then
            '        sUSIA = oRME.GetUmurPasien(dsCekHasilLab.FirstOrDefault.S_SO_TRANSAKSI_H.DATE, dsCekHasilLab.FirstOrDefault.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)

            '        Dim rpt As New xtraHasilLabSementara

            '        rpt.ShowPrintMarginsWarning = False
            '        rpt.Watermark.Text = sWATERMARK
            '        rpt.bindingSource.DataSource = dsCekHasilLab
            '        rpt.ExportToPdf(Alamat)

            '        If FileIO.FileSystem.FileExists(Alamat) Then
            '            PdfViewerHasilLaboratorium.LoadDocument(Alamat)
            '        End If
            '    End If
            'End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Hasil Laboratorium" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
#Region "Command Button"
    Private Sub btnHasilLaboratorium_BukaTutupList_Click(sender As Object, e As EventArgs) Handles btnHasilLaboratorium_BukaTutupList.Click
        If btnHasilLaboratorium_BukaTutupList.Text = "Tutup" Then
            btnHasilLaboratorium_BukaTutupList.Text = "Buka"
            grdHasilLaboratorium_List.Visible = False
        Else
            btnHasilLaboratorium_BukaTutupList.Text = "Tutup"
            grdHasilLaboratorium_List.Visible = True
        End If
    End Sub
#End Region
#Region "Event"
    Private Sub grvHasilLaboratorium_List_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvHasilLaboratorium_List.FocusedRowChanged
        'Try
        '    If grvHasilLaboratorium_List.GetFocusedRowCellValue("KDSOTRANSAKSI") Is Nothing Then
        '        PdfViewerHasilLaboratorium.CloseDocument()
        '        Exit Sub
        '    End If

        '    xtraReportHasilLaboratorium(grvHasilLaboratorium_List.GetFocusedRowCellValue("KDSOTRANSAKSI"), grvHasilLaboratorium_List.GetFocusedRowCellValue("KDITEM"))

        '    'If grvHasilLaboratorium_List.GetFocusedRowCellValue("KELOMPOK") = "SWAB" Then
        '    '    xtraReportHasilLaboratoriumSwab(grvHasilLaboratorium_List.GetFocusedRowCellValue("KDSOTRANSAKSI"), grvHasilLaboratorium_List.GetFocusedRowCellValue("KDITEM"))
        '    'Else
        '    '    xtraReportHasilLaboratorium(grvHasilLaboratorium_List.GetFocusedRowCellValue("KDSOTRANSAKSI"), grvHasilLaboratorium_List.GetFocusedRowCellValue("KDITEM"))
        '    'End If

        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub
#End Region
#End Region
#Region "Radiologi"
#Region "Function"
    Private Sub QueryHasilExpertise_LoadHistory(ByVal NoRM As String)
        Try
            If sSplashScreen = False Then Exit Sub

            grvHasilExpertise_List.Columns.Clear()
            grdHasilExpertise_List.DataSource = Nothing
            grvHasilExpertise_List.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Processing data.....")

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
            SQL &= "D.KDSOTRANSAKSI "
            SQL &= ",E.KDITEM "
            SQL &= ",TANGGAL = D.DATE "
            SQL &= ",NAMAPASIEN = C.NAME_DISPLAY "
            SQL &= ",DOKTERPENGIRIM = H.NAME_DISPLAY "
            SQL &= ",DOKTERRADIOLOGI = ISNULL((SELECT BB.NAME_DISPLAY FROM S_SO_TRANSAKSI_D_HASIL_RADIOLOGI_H AA INNER JOIN M_DOCTOR BB ON AA.KDDOCTOR = BB.KDDOCTOR WHERE D.KDSOTRANSAKSI = AA.KDSOTRANSAKSI AND E.SEQ = AA.SEQ) , 'BELUM') "
            SQL &= ",TINDAKAN = F.NMITEM2 "
            SQL &= ",BAYAR = (SELECT CASE D.PAYAMOUNT WHEN 0 THEN CONVERT(BIT, 0) ELSE CONVERT(BIT, 1) END) "
            SQL &= ",D.KDUSER "
            'SQL &= ",KELOMPOK = E.NMITEM1 "
            SQL &= ",SEQ_SO = E.SEQ "
            SQL &= ",E.KDDOCTOR "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_H A "
            SQL &= "INNER JOIN S_PENDAFTARAN_KUNJUNGAN B "
            SQL &= "ON A.KDPENDAFTARAN = B.KDPENDAFTARAN "
            SQL &= "INNER JOIN M_CUSTOMER C "
            SQL &= "ON A.KDCUSTOMER = C.KDCUSTOMER "
            SQL &= "INNER JOIN S_SO_TRANSAKSI_H D "
            SQL &= "ON B.KDKUNJUNGAN = D.KDKUNJUNGAN "
            SQL &= "INNER JOIN S_SO_TRANSAKSI_D E "
            SQL &= "ON D.KDSOTRANSAKSI = E.KDSOTRANSAKSI "
            SQL &= "INNER JOIN M_ITEM F "
            SQL &= "ON E.KDITEM = F.KDITEM "
            SQL &= "INNER JOIN M_ITEM_L3 G "
            SQL &= "ON F.KDITEM_L3 = G.KDITEM_L3 "
            SQL &= "INNER JOIN M_DOCTOR H "
            SQL &= "ON D.KDDOCTOR = H.KDDOCTOR "
            SQL &= "WHERE CONVERT(VARCHAR(8), D.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), D.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND G.MEMO = 'RADIOLOGI' "
            SQL &= "ORDER BY B.DATE DESC"

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_SO_TRANSAKSI_D_HASIL_RADIOLOGI_H")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grdHasilExpertise_List.MainView = grvHasilExpertise_List
            grdHasilExpertise_List.DataSource = ds.Tables("S_SO_TRANSAKSI_D_HASIL_RADIOLOGI_H")
            grdHasilExpertise_List.ForceInitialize()

            SplashScreenManager.CloseForm(False)

            QyeryHasilExpertise_LoadFormatData()

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub QyeryHasilExpertise_LoadFormatData()
        For iLoop As Integer = 0 To grvHasilExpertise_List.Columns.Count - 1
            If grvHasilExpertise_List.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvHasilExpertise_List.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvHasilExpertise_List.Columns(iLoop).DisplayFormat.FormatString = "{0:n0}"
                grvHasilExpertise_List.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvHasilExpertise_List.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvHasilExpertise_List.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvHasilExpertise_List.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm}"
            End If
        Next

        grvHasilExpertise_List.Columns("KDSOTRANSAKSI").Visible = False
        grvHasilExpertise_List.Columns("KDSOTRANSAKSI").OptionsColumn.ShowInCustomizationForm = False
        grvHasilExpertise_List.Columns("KDITEM").Visible = False
        grvHasilExpertise_List.Columns("KDITEM").OptionsColumn.ShowInCustomizationForm = False
        grvHasilExpertise_List.Columns("KDDOCTOR").Visible = False
        grvHasilExpertise_List.Columns("KDDOCTOR").OptionsColumn.ShowInCustomizationForm = False
        grvHasilExpertise_List.Columns("SEQ_SO").Visible = False
        grvHasilExpertise_List.Columns("SEQ_SO").OptionsColumn.ShowInCustomizationForm = False
        grvHasilExpertise_List.Columns("TANGGAL").Caption = "Tanggal"
        grvHasilExpertise_List.Columns("NAMAPASIEN").Caption = "Nama Pasien"
        grvHasilExpertise_List.Columns("DOKTERPENGIRIM").Caption = "Dokter Pengirim"
        grvHasilExpertise_List.Columns("DOKTERRADIOLOGI").Caption = "Dokter Radiologi"
        grvHasilExpertise_List.Columns("TINDAKAN").Caption = "Tindakan"
        grvHasilExpertise_List.Columns("KDUSER").Caption = "User"
        grvHasilExpertise_List.BestFitColumns()
    End Sub
    Private Sub xtraReportHasilExpertise(ByVal KDSOTRANSAKSI As String, ByVal SEQ_SO As Integer)
        Try
            Dim oExpertise As New Grouper.clsExpertise
            Dim oRME As New RME.clsRME

            PdfViewerHasilExpertise.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Hasil Laboratorium.....")

            Dim dataList As New List(Of Byte())
            Dim Alamat As String = FolderSimpan & KDSOTRANSAKSI & SEQ_SO & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim ds = oExpertise.GetData(KDSOTRANSAKSI, SEQ_SO)

            If ds IsNot Nothing Then
                sUSIA = oRME.GetUmurPasien(ds.DATE, ds.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)

                Dim rpt As New xtraExpertise

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.bindingSource.DataSource = ds
                rpt.ExportToPdf(Alamat)

                If FileIO.FileSystem.FileExists(Alamat) Then
                    PdfViewerHasilExpertise.LoadDocument(Alamat)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Hasil Expertise" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
#Region "Command Button"
    Private Sub btnHasilExpertise_BukaTutupList_Click(sender As Object, e As EventArgs) Handles btnHasilExpertise_BukaTutupList.Click
        If btnHasilExpertise_BukaTutupList.Text = "Tutup" Then
            btnHasilExpertise_BukaTutupList.Text = "Buka"
            grdHasilExpertise_List.Visible = False
        Else
            btnHasilExpertise_BukaTutupList.Text = "Tutup"
            grdHasilExpertise_List.Visible = True
        End If
    End Sub
#End Region
#Region "Event"
    Private Sub grvHasilExpertise_List_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvHasilExpertise_List.FocusedRowChanged
        Try
            If grvHasilExpertise_List.GetFocusedRowCellValue("KDSOTRANSAKSI") Is Nothing Then
                PdfViewerHasilExpertise.CloseDocument()
                Exit Sub
            End If

            xtraReportHasilExpertise(grvHasilExpertise_List.GetFocusedRowCellValue("KDSOTRANSAKSI"), grvHasilExpertise_List.GetFocusedRowCellValue("SEQ_SO"))

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
#End Region
#Region "Asesmen Medis"
#Region "Function"
    Private Sub QueryAsemenMedis_LoadHistory(ByVal NoRM As String)
        Try
            If sSplashScreen = False Then Exit Sub

            grvAsesmenMedis_Transaksi.Columns.Clear()
            grdAsesmenMedis_Transaksi.DataSource = Nothing
            grvAsesmenMedis_Transaksi.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Processing data.....")

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\RME\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "KDFORMULIR = A.KODE "
            SQL &= ",DESCRIPTION = 'ASESMEN AWAL MEDIS IGD' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",A.KDPENDAFTARAN "
            SQL &= ",TUJUAN = 'IGD' "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_IGD_01 A "
            SQL &= "WHERE A.KDCUSTOMER = '" & NoRM & "' "
            SQL &= "AND A.ISDELETE = 0 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDREG "
            SQL &= ",DESCRIPTION = 'CATATAN MEDIS AWAL RAWAT INAP DAN DISCHARGE PLANNING' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDREG "
            SQL &= ",TUJUAN = A.ANAMNESIS_04 "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RI_13 A "
            SQL &= "WHERE A.KDCUSTOMER = '" & NoRM & "' "
            SQL &= "AND A.ISDELETE = 0 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KODE "
            SQL &= ",DESCRIPTION = 'KARTU ANASTESI A' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDPENDAFTARAN "
            SQL &= ",TUJUAN = '' "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_OK_KARTU_ANESTESI_A A "
            SQL &= "WHERE A.KDCUSTOMER = '" & NoRM & "' "
            SQL &= "AND A.ISDELETE = 0 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KODE "
            SQL &= ",DESCRIPTION = 'KARTU ANASTESI B' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDPENDAFTARAN "
            SQL &= ",TUJUAN = '' "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_OK_KARTU_ANESTESI_B A "
            SQL &= "WHERE A.KDCUSTOMER = '" & NoRM & "' "
            SQL &= "AND A.ISDELETE = 0 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",DESCRIPTION = 'ASESMEN AWAL MEDIS PASIEN PARU' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDKUNJUNGAN "
            SQL &= ",TUJUAN = B.KDDEPARTMENT_NAMA "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RJ_36 A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "
            'SQL &= "AND A.ISDELETE = 0 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",DESCRIPTION = 'ASESMEN AWAL MEDIS PASIEN GIZI' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDKUNJUNGAN "
            SQL &= ",TUJUAN = B.KDDEPARTMENT_NAMA "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RJ_18 A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "


            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",DESCRIPTION = 'ASESMEN AWAL MEDIS PASIEN PENYAKIT DALAM' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDKUNJUNGAN "
            SQL &= ",TUJUAN = B.KDDEPARTMENT_NAMA "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RJ_31 A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",DESCRIPTION = 'ASESMEN AWAL MEDIS PASIEN BEDAH' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDKUNJUNGAN "
            SQL &= ",TUJUAN = B.KDDEPARTMENT_NAMA "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RJ_34 A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",DESCRIPTION = 'ASESMEN AWAL MEDIS PASIEN JANTUNG' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDKUNJUNGAN "
            SQL &= ",TUJUAN = B.KDDEPARTMENT_NAMA "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RJ_29 A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",DESCRIPTION = 'ASESMEN AWAL MEDIS PASIEN NEUROLOGI' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDKUNJUNGAN "
            SQL &= ",TUJUAN = B.KDDEPARTMENT_NAMA "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_ASMEDNEURO A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",DESCRIPTION = 'ASESMEN AWAL MEDIS PASIEN GIGI' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDKUNJUNGAN "
            SQL &= ",TUJUAN = B.KDDEPARTMENT_NAMA "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RJ_35 A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",DESCRIPTION = 'ASESMEN AWAL MEDIS PASIEN PSIKIATRIK' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDKUNJUNGAN "
            SQL &= ",TUJUAN = B.KDDEPARTMENT_NAMA "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RJ_30 A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",DESCRIPTION = 'ASESMEN AWAL MEDIS PASIEN TUMBUH KEMBANG' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDKUNJUNGAN "
            SQL &= ",TUJUAN = B.KDDEPARTMENT_NAMA "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RJ_41 A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",DESCRIPTION = 'ASESMEN AWAL MEDIS PASIEN ANAK' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDKUNJUNGAN "
            SQL &= ",TUJUAN = B.KDDEPARTMENT_NAMA "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_ASMEDANAK A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",DESCRIPTION = 'ASESMEN AWAL MEDIS OBSTETRI & GYNEKOLOGI' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDKUNJUNGAN "
            SQL &= ",TUJUAN = B.KDDEPARTMENT_NAMA "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_ASMEDOBGYN A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",DESCRIPTION = 'ASESMEN AWAL MEDIS PASIEN KULIT DAN KELAMIN' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDKUNJUNGAN "
            SQL &= ",TUJUAN = B.KDDEPARTMENT_NAMA "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RJ_33 A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "


            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",DESCRIPTION = 'ASESMEN AWAL MEDIS PASIEN MATA' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDKUNJUNGAN "
            SQL &= ",TUJUAN = B.KDDEPARTMENT_NAMA "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RJ_32 A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",DESCRIPTION = 'ASESMEN AWAL MEDIS PASIEN THT' "
            SQL &= ",TANGGAL = A.DATE_AWAL "
            SQL &= ",KDPENDAFTARAN = A.KDKUNJUNGAN "
            SQL &= ",TUJUAN = B.KDDEPARTMENT_NAMA "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_ASESMENAWALMEDISTHT A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",DESCRIPTION = 'ASESMEN AWAL MEDIS NEONATUS' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDKUNJUNGAN "
            SQL &= ",TUJUAN = B.KDDEPARTMENT_NAMA "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_AWAL_MEDIS_NEONATUS_RJ A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "


            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",DESCRIPTION = 'UJI FUNGSI, LAYANAN & PROSEDUR KEDOKTERAN FISIK & REHABILITASI' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDKUNJUNGAN "
            SQL &= ",TUJUAN = A.TUJUAN + CASE A.ISDELETE WHEN 1 THEN 'HAPUS' ELSE '' END "
            SQL &= ",A.KDUSER  "
            SQL &= "FROM "
            SQL &= "S_FISIOTERAFI_1 A "
            SQL &= "WHERE A.KDCUSTOMER = '" & NoRM & "' "
            'SQL &= "AND A.ISDELETE = 0 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",DESCRIPTION = 'FORMULIR CATATAN KLINIS REHABILITASI MEDIK' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDKUNJUNGAN "
            SQL &= ",TUJUAN = A.TUJUAN + CASE A.ISDELETE WHEN 1 THEN 'HAPUS' ELSE '' END "
            SQL &= ",A.KDUSER  "
            SQL &= "FROM "
            SQL &= "S_FISIOTERAFI_2 A "
            SQL &= "WHERE A.KDCUSTOMER = '" & NoRM & "' "
            'SQL &= "AND A.ISDELETE = 0 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",DESCRIPTION = 'LAYANAN KEDOKTERAN FISIK DAN REHABILITASI' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDKUNJUNGAN "
            SQL &= ",TUJUAN = A.TUJUAN + CASE A.ISDELETE WHEN 1 THEN 'HAPUS' ELSE '' END "
            SQL &= ",A.KDUSER  "
            SQL &= "FROM "
            SQL &= "S_FISIOTERAFI_3_H A "
            SQL &= "WHERE A.KDCUSTOMER = '" & NoRM & "' "
            'SQL &= "AND A.ISDELETE = 0 "


            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",DESCRIPTION = 'ASESMEN AWAL MEDIS RAWAT JALAN' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDKUNJUNGAN "
            SQL &= ",TUJUAN = B.KDDEPARTMENT_NAMA "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_ASESMENMEDISRAWATJALAN A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "


            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "R_IDENTITAS_GROUPER_CPPT_ASEMENMEDIS")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grdAsesmenMedis_Transaksi.MainView = grvAsesmenMedis_Transaksi
            grdAsesmenMedis_Transaksi.DataSource = ds.Tables("R_IDENTITAS_GROUPER_CPPT_ASEMENMEDIS")
            grdAsesmenMedis_Transaksi.ForceInitialize()

            SplashScreenManager.CloseForm(False)

            QyeryAsesmenMedis_LoadFormatData()

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub QyeryAsesmenMedis_LoadFormatData()
        For iLoop As Integer = 0 To grvAsesmenMedis_Transaksi.Columns.Count - 1
            If grvAsesmenMedis_Transaksi.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvAsesmenMedis_Transaksi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvAsesmenMedis_Transaksi.Columns(iLoop).DisplayFormat.FormatString = "{0:n0}"
                grvAsesmenMedis_Transaksi.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvAsesmenMedis_Transaksi.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvAsesmenMedis_Transaksi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvAsesmenMedis_Transaksi.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm}"
            End If
        Next

        grvAsesmenMedis_Transaksi.Columns("KDFORMULIR").Visible = False
        grvAsesmenMedis_Transaksi.BestFitColumns()
    End Sub
    Private Sub QueryAsemenMedis_LoadHistoryPDF(ByVal NoRM As String)
        Try
            If sSplashScreen = False Then Exit Sub

            grvAsesmenMedis_PDF.Columns.Clear()
            grdAsesmenMedis_PDF.DataSource = Nothing
            grvAsesmenMedis_PDF.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Processing data.....")

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\RME\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "KDFORMULIR = A.KODE "
            SQL &= ",DESCRIPTION = 'ASESMEN AWAL MEDIS IGD' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",A.KDPENDAFTARAN "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_IGD_01 A "
            SQL &= "WHERE A.KDCUSTOMER = '" & NoRM & "' "
            SQL &= "AND A.ISDELETE = 0 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDREG "
            SQL &= ",DESCRIPTION = 'CATATAN MEDIS AWAL RAWAT INAP DAN DISCHARGE PLANNING' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDREG "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RI_13 A "
            SQL &= "WHERE A.KDCUSTOMER = '" & NoRM & "' "
            SQL &= "AND A.ISDELETE = 0 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KODE "
            SQL &= ",DESCRIPTION = 'KARTU ANASTESI A' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDPENDAFTARAN "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_OK_KARTU_ANESTESI_A A "
            SQL &= "WHERE A.KDCUSTOMER = '" & NoRM & "' "
            SQL &= "AND A.ISDELETE = 0 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KODE "
            SQL &= ",DESCRIPTION = 'KARTU ANASTESI B' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDPENDAFTARAN "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_OK_KARTU_ANESTESI_B A "
            SQL &= "WHERE A.KDCUSTOMER = '" & NoRM & "' "
            SQL &= "AND A.ISDELETE = 0 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",DESCRIPTION = 'ASESMEN AWAL MEDIS PASIEN PENYAKIT DALAM' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDKUNJUNGAN "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RJ_31 A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",DESCRIPTION = 'ASESMEN AWAL MEDIS PASIEN BEDAH' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDKUNJUNGAN "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RJ_34 A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",DESCRIPTION = 'ASESMEN AWAL MEDIS PASIEN JANTUNG' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDKUNJUNGAN "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RJ_29 A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",DESCRIPTION = 'ASESMEN AWAL MEDIS PASIEN GIGI' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDKUNJUNGAN "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RJ_35 A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",DESCRIPTION = 'ASESMEN AWAL MEDIS PASIEN NEUROLOGI' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDKUNJUNGAN "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_ASMEDNEURO A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",DESCRIPTION = 'ASESMEN AWAL MEDIS OBSTETRI & GYNEKOLOGI' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDKUNJUNGAN "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_ASMEDOBGYN A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",DESCRIPTION = 'ASESMEN AWAL MEDIS PASIEN KULIT DAN KELAMIN' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDKUNJUNGAN "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RJ_33 A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",DESCRIPTION = 'ASESMEN AWAL MEDIS PASIEN MATA' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDKUNJUNGAN "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RJ_32 A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",DESCRIPTION = 'ASESMEN AWAL MEDIS PASIEN THT' "
            SQL &= ",TANGGAL = A.DATE_AWAL "
            SQL &= ",KDPENDAFTARAN = A.KDKUNJUNGAN "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_ASESMENAWALMEDISTHT A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",DESCRIPTION = 'ASESMEN AWAL MEDIS NEONATUS' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDKUNJUNGAN "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_AWAL_MEDIS_NEONATUS_RJ A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",DESCRIPTION = 'UJI FUNGSI, LAYANAN & PROSEDUR KEDOKTERAN FISIK & REHABILITASI' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDKUNJUNGAN "
            SQL &= ",A.KDUSER  "
            SQL &= "FROM "
            SQL &= "S_FISIOTERAFI_1 A "
            SQL &= "WHERE A.KDCUSTOMER = '" & NoRM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",DESCRIPTION = 'FORMULIR CATATAN KLINIS REHABILITASI MEDIK' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDKUNJUNGAN "
            SQL &= ",A.KDUSER  "
            SQL &= "FROM "
            SQL &= "S_FISIOTERAFI_2 A "
            SQL &= "WHERE A.KDCUSTOMER = '" & NoRM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",DESCRIPTION = 'LAYANAN KEDOKTERAN FISIK DAN REHABILITASI' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDKUNJUNGAN "
            SQL &= ",A.KDUSER  "
            SQL &= "FROM "
            SQL &= "S_FISIOTERAFI_3_H A "
            SQL &= "WHERE A.KDCUSTOMER = '" & NoRM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",DESCRIPTION = 'ASESMEN AWAL MEDIS PASIEN PARU' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDKUNJUNGAN "
            SQL &= ",A.KDUSER  "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RJ_36 A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",DESCRIPTION = 'ASESMEN AWAL MEDIS PASIEN GIZI' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDKUNJUNGAN "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RJ_18 A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",DESCRIPTION = 'ASESMEN AWAL MEDIS PASIEN PSIKIATRIK' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDKUNJUNGAN "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RJ_30 A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",DESCRIPTION = 'ASESMEN AWAL MEDIS PASIEN TUMBUH KEMBANG' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDKUNJUNGAN "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RJ_41 A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",DESCRIPTION = 'ASESMEN AWAL MEDIS PASIEN ANAK' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDKUNJUNGAN "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_ASMEDANAK A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",DESCRIPTION = 'ASESMEN AWAL MEDIS RAWAT JALAN' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPENDAFTARAN = A.KDKUNJUNGAN "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_ASESMENMEDISRAWATJALAN A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "R_IDENTITAS_GROUPER_CPPT_ASEMENMEDIS")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grdAsesmenMedis_PDF.MainView = grvAsesmenMedis_PDF
            grdAsesmenMedis_PDF.DataSource = ds.Tables("R_IDENTITAS_GROUPER_CPPT_ASEMENMEDIS")
            grdAsesmenMedis_PDF.ForceInitialize()

            SplashScreenManager.CloseForm(False)

            QyeryAsesmenMedis_LoadFormatDataPDF()

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub QyeryAsesmenMedis_LoadFormatDataPDF()
        For iLoop As Integer = 0 To grvAsesmenMedis_PDF.Columns.Count - 1
            If grvAsesmenMedis_PDF.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvAsesmenMedis_PDF.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvAsesmenMedis_PDF.Columns(iLoop).DisplayFormat.FormatString = "{0:n0}"
                grvAsesmenMedis_PDF.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvAsesmenMedis_PDF.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvAsesmenMedis_PDF.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvAsesmenMedis_PDF.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm}"
            End If
        Next

        grvAsesmenMedis_PDF.Columns("KDFORMULIR").Visible = False
        grvAsesmenMedis_PDF.BestFitColumns()
    End Sub
    Private Sub xtraReportAsesmenAwalMedisIGD(ByVal Kode As String)
        Try
            PdfViewerAsesmenMedis.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Medis.....")

            Dim dataList As New List(Of Byte())
            Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim oDigital As New Transaksi.clsDigital_IGD_01

            Dim ds = oDigital.GetDataByKodeIGD(Kode)

            If ds IsNot Nothing Then
                Dim rpt As New xtraReportFormulirIGD1

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.BindingSource.DataSource = ds
                rpt.ExportToPdf(Alamat)

                If FileIO.FileSystem.FileExists(Alamat) Then
                    PdfViewerAsesmenMedis.LoadDocument(Alamat)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Asesmen Medis" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub xtraReportAsesmenAwalRawatInap(ByVal Kode As String)
        Try
            PdfViewerAsesmenMedis.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Medis.....")

            Dim dataList As New List(Of Byte())
            Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim oDigital As New EMedrek.clsDigital_DischargePlanning

            Dim ds = oDigital.GetData(Kode)

            If ds IsNot Nothing Then
                Dim rpt As New xtraReportEMedrekRI_13

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.BindingSource.DataSource = ds
                rpt.ExportToPdf(Alamat)

                If FileIO.FileSystem.FileExists(Alamat) Then
                    PdfViewerAsesmenMedis.LoadDocument(Alamat)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Asesmen Medis" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub xtraReportKartuAnastesiA(ByVal Kode As String)
        Try
            PdfViewerAsesmenMedis.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Medis.....")

            Dim dataList As New List(Of Byte())
            Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim oDigital As New EMedrek.clsS_DIGITAL_OK_KARTU_ANESTESI_A
            Dim oKunjungan As New Admission.clsPendaftaran_Kunjungan

            Dim ds = oDigital.GetData(Kode)
            Dim dsKunjungan = oKunjungan.GetDatabyKodeKunjungan(ds.KDPENDAFTARAN)

            If ds IsNot Nothing Then
                Dim rpt As New xtraReportKARTUANESTESI_A

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                '*** identitas pasien ***'
                rpt.pNAMA.Value = dsKunjungan.NAMAPASIEN
                rpt.pJK.Value = dsKunjungan.JENISKELAMIN
                rpt.pRM.Value = dsKunjungan.KDCUSTOMER
                rpt.pUMUR.Value = dsKunjungan.UMUR

                rpt.sAttacment_1 = ds.imgMonitoringAnestesi
                rpt.sAttacment_2 = ds.imgMonitoringSkalaNyeri

                rpt.bindingSource.DataSource = ds
                rpt.ExportToPdf(Alamat)

                If FileIO.FileSystem.FileExists(Alamat) Then
                    PdfViewerAsesmenMedis.LoadDocument(Alamat)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Asesmen Medis" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub xtraReportKartuAnastesiB(ByVal Kode As String)
        Try
            PdfViewerAsesmenMedis.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Medis.....")

            Dim dataList As New List(Of Byte())
            Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim oDigital As New EMedrek.clsS_DIGITAL_OK_KARTU_ANESTESI_B
            Dim oKunjungan As New Admission.clsPendaftaran_Kunjungan

            Dim ds = oDigital.GetData(Kode)
            Dim dsKunjungan = oKunjungan.GetDatabyKodeKunjungan(ds.KDPENDAFTARAN)

            If ds IsNot Nothing Then
                Dim rpt As New xtraReportKARTUANESTESI_B

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                '*** identitas pasien ***'
                rpt.pNAMA.Value = dsKunjungan.NAMAPASIEN
                rpt.pJK.Value = dsKunjungan.JENISKELAMIN
                rpt.pRM.Value = dsKunjungan.KDCUSTOMER
                rpt.pUMUR.Value = dsKunjungan.UMUR
                rpt.pPANGKAT.Value = dsKunjungan.PANGKAT
                'rpt.sFIELDNRP.Expression = dsKunjungan.KDIDENTITAS
                rpt.pKESATUAN.Value = dsKunjungan.KESATUAN

                rpt.sAttacment_1 = ds.img1
                rpt.sAttacment_2 = ds.img2


                rpt.BindingSource1.DataSource = ds
                rpt.ExportToPdf(Alamat)

                If FileIO.FileSystem.FileExists(Alamat) Then
                    PdfViewerAsesmenMedis.LoadDocument(Alamat)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Asesmen Medis" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub xtraReportAsesmenAwalPenyakitBedah(ByVal Kode As String)
        Try
            PdfViewerAsesmenMedis.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Medis.....")

            Dim dataList As New List(Of Byte())
            Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim oDigital As New EMedrek.clsDigital_RJ_34

            Dim ds = oDigital.GetData(Kode)

            If ds IsNot Nothing Then
                Dim rpt As New xtraReportEMedrekRJ_34

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                sAttacment_1 = ds.SDIGITALRJ34_27

                rpt.BindingSource.DataSource = ds
                rpt.ExportToPdf(Alamat)

                If FileIO.FileSystem.FileExists(Alamat) Then
                    PdfViewerAsesmenMedis.LoadDocument(Alamat)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Asesmen Medis" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub xtraReportAsesmenAwalPenyakitDalam(ByVal Kode As String)
        Try
            PdfViewerAsesmenMedis.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Medis.....")

            Dim dataList As New List(Of Byte())
            Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim oDigital As New EMedrek.clsDigital_RJ_31

            Dim ds = oDigital.GetData(Kode)

            If ds IsNot Nothing Then
                Dim rpt As New xtraReportEMedrekRJ_31

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.BindingSource.DataSource = ds
                rpt.ExportToPdf(Alamat)

                If FileIO.FileSystem.FileExists(Alamat) Then
                    PdfViewerAsesmenMedis.LoadDocument(Alamat)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Asesmen Medis" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub xtraReportAsesmenAwalParu(ByVal Kode As String)
        Try
            PdfViewerAsesmenMedis.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Medis.....")

            Dim dataList As New List(Of Byte())
            Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim oDigital As New EMedrek.clsDigital_RJ_36

            Dim ds = oDigital.GetData(Kode)

            If ds IsNot Nothing Then
                Dim rpt As New xtraReportEMedrekRJ_36

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                sAttacment_1 = ds.SDIGITALRJ36_25

                rpt.BindingSource.DataSource = ds
                rpt.ExportToPdf(Alamat)

                If FileIO.FileSystem.FileExists(Alamat) Then
                    PdfViewerAsesmenMedis.LoadDocument(Alamat)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Asesmen Medis" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub xtraReportAsesmenAwalGizi(ByVal Kode As String)
        Try
            PdfViewerAsesmenMedis.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Medis.....")

            Dim dataList As New List(Of Byte())
            Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim oDigital As New EMedrek.clsDigital_RJ_18

            Dim ds = oDigital.GetData(Kode)

            If ds IsNot Nothing Then
                Dim rpt As New xtraReportEMedrekRJ_18

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.BindingSource.DataSource = ds
                rpt.ExportToPdf(Alamat)

                If FileIO.FileSystem.FileExists(Alamat) Then
                    PdfViewerAsesmenMedis.LoadDocument(Alamat)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Asesmen Medis" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub xtraReportAsesmenAwalGigi(ByVal Kode As String)
        Try
            PdfViewerAsesmenMedis.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Medis.....")

            Dim dataList As New List(Of Byte())
            Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim oDigital As New EMedrek.clsDigital_RJ_35

            Dim ds = oDigital.GetData(Kode)

            If ds IsNot Nothing Then
                Dim rpt As New xtraReportEMedrekRJ_35

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                sAttacment_1 = ds.SDIGITALRJ35_96_TEXT

                rpt.BindingSource.DataSource = ds
                rpt.ExportToPdf(Alamat)

                If FileIO.FileSystem.FileExists(Alamat) Then
                    PdfViewerAsesmenMedis.LoadDocument(Alamat)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Asesmen Medis" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub xtraReportAsesmenAwalJantung(ByVal Kode As String)
        Try
            PdfViewerAsesmenMedis.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Medis.....")

            Dim dataList As New List(Of Byte())
            Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim oDigital As New EMedrek.clsDigital_RJ_29

            Dim ds = oDigital.GetData(Kode)

            If ds IsNot Nothing Then
                Dim rpt As New xtraReportEMedrekRJ_29

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                'sAttacment_1 = ds.SDIGITALRJ35_96_TEXT

                rpt.BindingSource.DataSource = ds
                rpt.ExportToPdf(Alamat)

                If FileIO.FileSystem.FileExists(Alamat) Then
                    PdfViewerAsesmenMedis.LoadDocument(Alamat)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Asesmen Medis" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub xtraReportAsesmenAwalNeuro(ByVal Kode As String)
        Try
            PdfViewerAsesmenMedis.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Medis.....")

            Dim dataList As New List(Of Byte())
            Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim oDigital As New EMedrek.clsDigital_RJ_ASMEDNEURO

            Dim ds = oDigital.GetData(Kode)

            If ds IsNot Nothing Then
                Dim rpt As New xtraReportAsmedNeuro

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                'sAttacment_1 = ds.SDIGITALRJ35_96_TEXT

                rpt.BindingSource1.DataSource = ds
                rpt.ExportToPdf(Alamat)

                If FileIO.FileSystem.FileExists(Alamat) Then
                    PdfViewerAsesmenMedis.LoadDocument(Alamat)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Asesmen Medis" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub xtraReportAsesmenAwalJiwa(ByVal Kode As String)
        Try
            PdfViewerAsesmenMedis.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Medis.....")

            Dim dataList As New List(Of Byte())
            Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim oDigital As New EMedrek.clsDigital_RJ_30

            Dim ds = oDigital.GetData(Kode)

            If ds IsNot Nothing Then
                Dim rpt As New xtraReportEMedrekRJ_30

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                'sAttacment_1 = ds.SDIGITALRJ35_96_TEXT

                rpt.BindingSource.DataSource = ds
                rpt.ExportToPdf(Alamat)

                If FileIO.FileSystem.FileExists(Alamat) Then
                    PdfViewerAsesmenMedis.LoadDocument(Alamat)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Asesmen Medis" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub xtraReportAsesmenAwalTumbuhKembang(ByVal Kode As String)
        Try
            PdfViewerAsesmenMedis.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Medis.....")

            Dim dataList As New List(Of Byte())
            Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim oDigital As New EMedrek.clsDigital_RJ_41

            Dim ds = oDigital.GetData(Kode)

            If ds IsNot Nothing Then
                Dim rpt As New xtraReportEMedrekRJ_41

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                'sAttacment_1 = ds.SDIGITALRJ35_96_TEXT

                rpt.BindingSource1.DataSource = ds
                rpt.ExportToPdf(Alamat)

                If FileIO.FileSystem.FileExists(Alamat) Then
                    PdfViewerAsesmenMedis.LoadDocument(Alamat)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Asesmen Medis" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub xtraReportAsesmenAwalAnak(ByVal Kode As String)
        Try
            PdfViewerAsesmenMedis.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Medis.....")

            Dim dataList As New List(Of Byte())
            Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim oDigital As New EMedrek.clsDigital_RJ_ASMEDANAK

            Dim ds = oDigital.GetData(Kode)

            If ds IsNot Nothing Then
                Dim rpt As New xtraReportEMedrek_AsmedAnak

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                sAttacment_1 = ds.DINDING_DADA_PARU_IMG
                sAttacment_2 = ds.JANTUNG_IMG
                sAttachGambarPerut = ds.PERUT_IMG

                rpt.BindingSource.DataSource = ds
                rpt.ExportToPdf(Alamat)

                If FileIO.FileSystem.FileExists(Alamat) Then
                    PdfViewerAsesmenMedis.LoadDocument(Alamat)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Asesmen Medis" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub xtraReportAsesmenAwalKebidan(ByVal Kode As String)
        Try
            PdfViewerAsesmenMedis.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Medis.....")

            Dim dataList As New List(Of Byte())
            Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim oDigital As New EMedrek.clsS_DIGITAL_ASMEDOBGYN

            Dim ds = oDigital.GetData(Kode)

            If ds IsNot Nothing Then
                Dim rpt As New xtraReportAsmedKebidanan

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                sAttacment_1 = ds.picPERUT

                rpt.BindingSource1.DataSource = ds
                rpt.ExportToPdf(Alamat)

                If FileIO.FileSystem.FileExists(Alamat) Then
                    PdfViewerAsesmenMedis.LoadDocument(Alamat)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Asesmen Medis" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub xtraReportAsesmenAwalKulit(ByVal Kode As String)
        Try
            PdfViewerAsesmenMedis.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Medis.....")

            Dim dataList As New List(Of Byte())
            Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim oDigital As New EMedrek.clsDigital_RJ_33

            Dim ds = oDigital.GetData(Kode)

            If ds IsNot Nothing Then
                Dim rpt As New xtraReportEMedrekRJ_33

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                sAttacment_1 = ds.SDIGITALRJ33_15

                rpt.BindingSource.DataSource = ds
                rpt.ExportToPdf(Alamat)

                If FileIO.FileSystem.FileExists(Alamat) Then
                    PdfViewerAsesmenMedis.LoadDocument(Alamat)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Asesmen Medis" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub xtraReportAsesmenAwalMata(ByVal Kode As String)
        Try
            PdfViewerAsesmenMedis.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Medis.....")

            Dim dataList As New List(Of Byte())
            Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim oDigital As New EMedrek.clsDigital_RJ_32

            Dim ds = oDigital.GetData(Kode)

            If ds IsNot Nothing Then
                Dim rpt As New xtraReportEMedrekRJ_32

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                'sAttacment_1 = ds.SDIGITALRJ33_15

                rpt.BindingSource.DataSource = ds
                rpt.ExportToPdf(Alamat)

                If FileIO.FileSystem.FileExists(Alamat) Then
                    PdfViewerAsesmenMedis.LoadDocument(Alamat)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Asesmen Medis" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub xtraReportAsesmenAwalTHT(ByVal Kode As String)
        Try
            PdfViewerAsesmenMedis.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Medis.....")

            Dim dataList As New List(Of Byte())
            Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim oDigital As New EMedrek.clsDigital_RJ_ASMEDTHT

            Dim ds = oDigital.GetData(Kode)

            If ds IsNot Nothing Then
                Dim rpt As New xtraReportAsesmenAwalMedisTHT

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                'sAttacment_1 = ds.SDIGITALRJ33_15

                rpt.BindingSource.DataSource = ds
                rpt.ExportToPdf(Alamat)

                If FileIO.FileSystem.FileExists(Alamat) Then
                    PdfViewerAsesmenMedis.LoadDocument(Alamat)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Asesmen Medis" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub xtraReportAsesmenAwalNeonatus(ByVal Kode As String)
        Try
            PdfViewerAsesmenMedis.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Medis.....")

            Dim dataList As New List(Of Byte())
            Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim oDigital As New EMedrek.clsDigital_RJ_Neonatus

            Dim ds = oDigital.GetData(Kode)

            If ds IsNot Nothing Then
                Dim rpt As New xtraReportEMedrekRJ_Neonatus

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                'sAttacment_1 = ds.SDIGITALRJ33_15

                rpt.BindingSource.DataSource = ds
                rpt.ExportToPdf(Alamat)

                If FileIO.FileSystem.FileExists(Alamat) Then
                    PdfViewerAsesmenMedis.LoadDocument(Alamat)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Asesmen Medis" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub xtraReportLembarUjiFungsi(ByVal Kode As String)
        'Try
        '    PdfViewerAsesmenMedis.CloseDocument()

        '    SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

        '    SplashScreenManager.Default.SetWaitFormCaption("Processing data Lembar Uji Fungsi.....")

        '    Dim Alamat1 As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & "1.pdf"
        '    Dim Alamat2 As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & "2.pdf"
        '    Dim arrfname2 As New List(Of String)()

        '    Dim oDigital As New EMedrek.clsFisioterafi_1

        '    Dim ds = oDigital.GetData(Kode)

        '    If ds IsNot Nothing Then
        '        Dim rpt As New xtraLembarUjiFungsiRehabResumeNew

        '        rpt.ShowPrintMarginsWarning = False
        '        rpt.Watermark.Text = sWATERMARK

        '        rpt.bindingSource.DataSource = ds
        '        rpt.ExportToPdf(Alamat1)

        '        If FileIO.FileSystem.FileExists(Alamat1) Then
        '            arrfname2.Add(Alamat1)
        '        End If

        '        Dim rpt1 As New xtraLembarUjiFungsiRehab

        '        rpt1.ShowPrintMarginsWarning = False
        '        rpt1.Watermark.Text = sWATERMARK

        '        rpt1.bindingSource.DataSource = ds
        '        rpt1.ExportToPdf(Alamat2)

        '        If FileIO.FileSystem.FileExists(Alamat2) Then
        '            arrfname2.Add(Alamat2)
        '        End If
        '    End If

        '    Dim sinputFiles2() As String = {}
        '    For Each ifname In arrfname2
        '        sinputFiles2 = AppendArray(sinputFiles2, ifname)
        '    Next

        '    Dim AlamatMerge As String = FolderSimpan & Now.ToString("yyyyMMddHHmmss") & "XZHASIL" & ".pdf"
        '    MergePdfFiles(sinputFiles2, AlamatMerge)

        '    If System.IO.File.Exists(AlamatMerge) Then
        '        PdfViewerAsesmenMedis.LoadDocument(AlamatMerge)
        '    End If

        '    SplashScreenManager.CloseForm(False)
        'Catch oErr As Exception
        '    SplashScreenManager.CloseForm(False)
        '    MsgBox("Cetak Asesmen Medis" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub
    Private Function AppendArray(Of T)(ByVal thisArray() As T, ByVal itemToAppend As T) As T()
        If thisArray Is Nothing Then thisArray = New T() {}
        Dim tempList As List(Of T) = thisArray.ToList
        tempList.Add(itemToAppend)
        Return tempList.ToArray
    End Function
    Private Function MergePdfFiles(ByVal pdfFiles() As String, ByVal outputPath As String) As Boolean
        Dim result As Boolean = False
        Dim pdfCount As Integer = 0     'total input pdf file count
        Dim f As Integer = 0    'pointer to current input pdf file
        Dim fileName As String
        Dim reader As iTextSharp.text.pdf.PdfReader = Nothing
        Dim pageCount As Integer = 0
        Dim pdfDoc As iTextSharp.text.Document = Nothing    'the output pdf document
        Dim writer As PdfWriter = Nothing
        Dim cb As PdfContentByte = Nothing

        Dim page As PdfImportedPage = Nothing
        Dim rotation As Integer = 0

        Try
            pdfCount = pdfFiles.Length
            If pdfCount >= 1 Then
                'Open the 1st item in the array PDFFiles
                fileName = pdfFiles(f)
                reader = New iTextSharp.text.pdf.PdfReader(fileName)
                'Get page count
                pageCount = reader.NumberOfPages

                pdfDoc = New iTextSharp.text.Document(reader.GetPageSizeWithRotation(1), 18, 18, 18, 18)

                writer = PdfWriter.GetInstance(pdfDoc, New FileStream(outputPath, FileMode.OpenOrCreate))

                With pdfDoc
                    .Open()
                End With
                'Instantiate a PdfContentByte object
                cb = writer.DirectContent
                'Now loop thru the input pdfs
                While f < pdfCount
                    'Declare a page counter variable
                    Dim i As Integer = 0
                    'Loop thru the current input pdf's pages starting at page 1
                    While i < pageCount
                        i += 1
                        'Get the input page size
                        pdfDoc.SetPageSize(reader.GetPageSizeWithRotation(i))
                        'Create a new page on the output document
                        pdfDoc.NewPage()
                        'If it is the 1st page, we add bookmarks to the page
                        'Now we get the imported page
                        page = writer.GetImportedPage(reader, i)
                        'Read the imported page's rotation
                        rotation = reader.GetPageRotation(i)
                        'Then add the imported page to the PdfContentByte object as a template based on the page's rotation
                        If rotation = 90 Then
                            cb.AddTemplate(page, 0, -1.0F, 1.0F, 0, 0, reader.GetPageSizeWithRotation(i).Height)
                        ElseIf rotation = 270 Then
                            cb.AddTemplate(page, 0, 1.0F, -1.0F, 0, reader.GetPageSizeWithRotation(i).Width + 60, -30)
                        Else
                            cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, 0, 0)
                        End If
                    End While
                    'Increment f and read the next input pdf file
                    f += 1
                    If f < pdfCount Then
                        fileName = pdfFiles(f)
                        reader = New iTextSharp.text.pdf.PdfReader(fileName)
                        pageCount = reader.NumberOfPages
                    End If
                End While
                'When all done, we close the document so that the pdfwriter object can write it to the output file
                pdfDoc.Close()
                result = True

            End If
        Catch ex As Exception
            pdfDoc.Close()
            Return False
        End Try
        Return result
    End Function
    Private Sub xtraReportCatatanKlinisRehab(ByVal Kode As String)
        Try
            PdfViewerAsesmenMedis.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Catatan Klinis Rehab.....")

            Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim oDigital As New EMedrek.clsFisioterafi_2

            Dim ds = oDigital.GetData(Kode)

            If ds IsNot Nothing Then
                Dim rpt As New xtraCatatanKlinisRehab

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.bindingSource.DataSource = ds
                rpt.ExportToPdf(Alamat)

                If FileIO.FileSystem.FileExists(Alamat) Then
                    PdfViewerAsesmenMedis.LoadDocument(Alamat)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Asesmen Medis" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub xtraReportResumeRehab(ByVal Kode As String)
        Try
            PdfViewerAsesmenMedis.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Resume Rehab.....")

            Dim dataList As New List(Of Byte())
            Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim oDigital As New EMedrek.clsFisioterafi_3

            Dim rpt As New xtraResumeRehab

            rpt.ShowPrintMarginsWarning = False
            rpt.Watermark.Text = sWATERMARK

            rpt.bindingSource.DataSource = oDigital.GetDataDetail(Kode)
            rpt.ExportToPdf(Alamat)

            If FileIO.FileSystem.FileExists(Alamat) Then
                PdfViewerAsesmenMedis.LoadDocument(Alamat)
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Asesmen Medis" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
#Region "Command Button"
    Private Sub fn_EmptyAsesmenMedis()
        pnlAsesmenMedis_pic.Dock = DockStyle.Top

        XtraTabList_SelectedPageChanged()
        lPanelAsesmenMedis_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lPanelAsesmenMedis_Panel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lAsesmenMedis_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If Not frmAsesmenAwalMedisIGD2 Is Nothing Then frmAsesmenAwalMedisIGD2.Dispose()
        frmAsesmenAwalMedisIGD2 = Nothing

        If Not frmDischargePlanning Is Nothing Then frmDischargePlanning.Dispose()
        frmDischargePlanning = Nothing

        If Not frmKartuAnastesi_A Is Nothing Then frmKartuAnastesi_A.Dispose()
        frmKartuAnastesi_A = Nothing

        If Not frmKartuAnastesi_B Is Nothing Then frmKartuAnastesi_B.Dispose()
        frmKartuAnastesi_B = Nothing

        If Not frmEMedrekRJ_31 Is Nothing Then frmEMedrekRJ_31.Dispose()
        frmEMedrekRJ_31 = Nothing

        If Not frmEMedrekRJ_36 Is Nothing Then frmEMedrekRJ_36.Dispose()
        frmEMedrekRJ_36 = Nothing

        If Not frmEMedrekRJ_34 Is Nothing Then frmEMedrekRJ_34.Dispose()
        frmEMedrekRJ_34 = Nothing

        If Not frmEMedrekRJ_29 Is Nothing Then frmEMedrekRJ_29.Dispose()
        frmEMedrekRJ_29 = Nothing

        If Not frmAsmedNeuro Is Nothing Then frmAsmedNeuro.Dispose()
        frmAsmedNeuro = Nothing

        If Not frmEMedrekRJ_35 Is Nothing Then frmEMedrekRJ_35.Dispose()
        frmEMedrekRJ_35 = Nothing

        If Not frmEMedrekRJ_30 Is Nothing Then frmEMedrekRJ_30.Dispose()
        frmEMedrekRJ_30 = Nothing

        If Not frmAsesmenAwalMedisNeonatusRawatInap Is Nothing Then frmAsesmenAwalMedisNeonatusRawatInap.Dispose()
        frmAsesmenAwalMedisNeonatusRawatInap = Nothing

        'If Not frmLembarUjiFungsiRehab Is Nothing Then frmLembarUjiFungsiRehab.Dispose()
        'frmLembarUjiFungsiRehab = Nothing

        'If Not frmCatatanKlinisRehab Is Nothing Then frmCatatanKlinisRehab.Dispose()
        'frmCatatanKlinisRehab = Nothing

        If Not frmResumeRawatJalanRehab Is Nothing Then frmResumeRawatJalanRehab.Dispose()
        frmResumeRawatJalanRehab = Nothing

        If Not frmEMedrekRJ_18 Is Nothing Then frmEMedrekRJ_18.Dispose()
        frmEMedrekRJ_18 = Nothing

        If Not frmEMedrekRJ_41 Is Nothing Then frmEMedrekRJ_41.Dispose()
        frmEMedrekRJ_41 = Nothing

        If Not frmEMedrek_AsmedAnak Is Nothing Then frmEMedrek_AsmedAnak.Dispose()
        frmEMedrek_AsmedAnak = Nothing

        If Not frmEMedrekAsmedKebidanan Is Nothing Then frmEMedrekAsmedKebidanan.Dispose()
        frmEMedrekAsmedKebidanan = Nothing

        If Not frmEMedrekRJ_33 Is Nothing Then frmEMedrekRJ_33.Dispose()
        frmEMedrekRJ_33 = Nothing

        If Not frmEMedrekRJ_32 Is Nothing Then frmEMedrekRJ_32.Dispose()
        frmEMedrekRJ_32 = Nothing

        If Not frmAsesmenAwalMedisTHT Is Nothing Then frmAsesmenAwalMedisTHT.Dispose()
        frmAsesmenAwalMedisTHT = Nothing

        If Not frmEMedrekAWAL_MEDIS_NEONATUS_RJ Is Nothing Then frmEMedrekAWAL_MEDIS_NEONATUS_RJ.Dispose()
        frmEMedrekAWAL_MEDIS_NEONATUS_RJ = Nothing

        If Not frmAsesmenMedisRawatJalan Is Nothing Then frmAsesmenMedisRawatJalan.Dispose()
        frmAsesmenMedisRawatJalan = Nothing
    End Sub
    Private Sub btnAsesmenMedis_Back_Click(sender As Object, e As EventArgs) Handles btnAsesmenMedis_Back.Click
        fn_EmptyAsesmenMedis()
    End Sub
    Private Sub btnAsesmenMedis_BukaTutupList_Click(sender As Object, e As EventArgs) Handles btnAsesmenMedis_BukaTutupList.Click
        If btnAsesmenMedis_BukaTutupList.Text = "Tutup" Then
            btnAsesmenMedis_BukaTutupList.Text = "Buka"
            grdAsesmenMedis_PDF.Visible = False
        Else
            btnAsesmenMedis_BukaTutupList.Text = "Tutup"
            grdAsesmenMedis_PDF.Visible = True
        End If
    End Sub
    Private Sub picAsesmenAwalMedis_Add_Click(sender As Object, e As EventArgs) Handles picAsesmenAwalMedis_Add.Click
        If lblKDKUNJUNGAN.Text = "" Then
            MsgBox("Silahkan Pilih Dulu Pasien", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If cboAsesmenAwalMedis_Formulir.Text = "" Then
            MsgBox("Formulir Belum di Pilih", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        Dim oDataGrouper As New Admission.clsPendaftaran_Kunjungan

        Dim ds = oDataGrouper.GetDatabyKodeKunjungan(lblKDKUNJUNGAN.Text)

        If ds IsNot Nothing Then
            Try
                pnlAsesmenMedis_pic.Dock = DockStyle.None

                lPanelAsesmenMedis_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lPanelAsesmenMedis_Panel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lAsesmenMedis_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                If cboAsesmenAwalMedis_Formulir.Text = "ASESMEN AWAL MEDIS IGD" Then
                    ' Bersihkan konten panel
                    pnlAsemenMedis_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmAsesmenAwalMedisIGD2.TopLevel = False
                    frmAsesmenAwalMedisIGD2.FormBorderStyle = FormBorderStyle.None
                    frmAsesmenAwalMedisIGD2.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsemenMedis_Transaksi.Controls.Add(frmAsesmenAwalMedisIGD2)

                    ' Tampilkan form anak
                    frmAsesmenAwalMedisIGD2.LoadMe(True, "", ds.KDIDENTITAS, oDataGrouper.GetData(ds.KDKUNJUNGAN).KDPENDAFTARAN, ds.KDKUNJUNGAN, ds.KDDOCTOR, ds.KDCUSTOMER, ds.NAMAPASIEN, sTanggalLahir)
                    frmAsesmenAwalMedisIGD2.Show()
                ElseIf cboAsesmenAwalMedis_Formulir.Text = "CATATAN MEDIS AWAL RAWAT INAP DAN DISCHARGE PLANNING" Then
                    Dim oDigital As New EMedrek.clsDigital_DischargePlanning

                    Dim dsDigital = oDigital.GetData(oDataGrouper.GetData(ds.KDKUNJUNGAN).KDPENDAFTARAN)

                    If dsDigital IsNot Nothing Then
                        MsgBox("Formulir Sudah dibuatkan, Silahkan lakukan Edit untuk memperbaiki", MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                    End If

                    ' Bersihkan konten panel
                    pnlAsemenMedis_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmDischargePlanning.TopLevel = False
                    frmDischargePlanning.FormBorderStyle = FormBorderStyle.None
                    frmDischargePlanning.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsemenMedis_Transaksi.Controls.Add(frmDischargePlanning)

                    ' Tampilkan form anak
                    frmDischargePlanning.LoadMe(FORM_MODE.FORM_MODE_ADD, ds.KDKUNJUNGAN, ds.KDDEPARTMENT_NAMA, ds.KDDOCTOR, ds.KDCUSTOMER, ds.NAMAPASIEN, oDataGrouper.GetData(ds.KDKUNJUNGAN).KDPENDAFTARAN)
                    frmDischargePlanning.Show()
                ElseIf cboAsesmenAwalMedis_Formulir.Text = "ASESMEN AWAL MEDIS PASIEN PARU" Then
                    Dim oDigital As New EMedrek.clsDigital_RJ_36
                    Dim dsDigital = oDigital.GetData(ds.KDKUNJUNGAN)
                    Dim sNoid As String = ""

                    If dsDigital IsNot Nothing Then
                        MsgBox("Formulir Sudah dibuatkan, Silahkan lakukan Edit untuk memperbaiki", MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                    End If

                    ' Bersihkan konten panel
                    pnlAsemenMedis_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmEMedrekRJ_36.TopLevel = False
                    frmEMedrekRJ_36.FormBorderStyle = FormBorderStyle.None
                    frmEMedrekRJ_36.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsemenMedis_Transaksi.Controls.Add(frmEMedrekRJ_36)

                    ' Tampilkan form anak
                    frmEMedrekRJ_36.LoadMe(FORM_MODE.FORM_MODE_ADD, ds.KDDOCTOR, ds.KDIDENTITAS, ds.KDKUNJUNGAN)
                    frmEMedrekRJ_36.Show()
                ElseIf cboAsesmenAwalMedis_Formulir.Text = "KARTU ANASTESI A" Then
                    Dim oDigital As New EMedrek.clsS_DIGITAL_OK_KARTU_ANESTESI_A
                    Dim dsDigital = oDigital.GetDataByKdpendaftaran(ds.KDKUNJUNGAN)

                    If dsDigital IsNot Nothing Then
                        MsgBox("Formulir Sudah dibuatkan, Silahkan lakukan Edit untuk memperbaiki", MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                    End If

                    ' Bersihkan konten panel
                    pnlAsemenMedis_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmKartuAnastesi_A.TopLevel = False
                    frmKartuAnastesi_A.FormBorderStyle = FormBorderStyle.None
                    frmKartuAnastesi_A.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsemenMedis_Transaksi.Controls.Add(frmKartuAnastesi_A)

                    ' Tampilkan form anak
                    frmKartuAnastesi_A.LoadMe(FORM_MODE.FORM_MODE_ADD, ds.KDKUNJUNGAN)
                    frmKartuAnastesi_A.Show()
                ElseIf cboAsesmenAwalMedis_Formulir.Text = "KARTU ANASTESI B" Then
                    Dim oDigital As New EMedrek.clsS_DIGITAL_OK_KARTU_ANESTESI_B
                    Dim dsDigital = oDigital.GetDataByKdpendaftaran(ds.KDKUNJUNGAN)

                    If dsDigital IsNot Nothing Then
                        MsgBox("Formulir Sudah dibuatkan, Silahkan lakukan Edit untuk memperbaiki", MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                    End If

                    ' Bersihkan konten panel
                    pnlAsemenMedis_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmKartuAnastesi_B.TopLevel = False
                    frmKartuAnastesi_B.FormBorderStyle = FormBorderStyle.None
                    frmKartuAnastesi_B.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsemenMedis_Transaksi.Controls.Add(frmKartuAnastesi_B)

                    ' Tampilkan form anak
                    frmKartuAnastesi_B.LoadMe(FORM_MODE.FORM_MODE_ADD, ds.KDKUNJUNGAN)
                    frmKartuAnastesi_B.Show()
                ElseIf cboAsesmenAwalMedis_Formulir.Text = "ASESMEN AWAL MEDIS PASIEN PENYAKIT DALAM" Then
                    Dim oDigital As New EMedrek.clsDigital_RJ_31
                    Dim dsDigital = oDigital.GetData(ds.KDKUNJUNGAN)
                    Dim sNoid As String = ""

                    If dsDigital IsNot Nothing Then
                        MsgBox("Formulir Sudah dibuatkan, Silahkan lakukan Edit untuk memperbaiki", MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                    End If

                    ' Bersihkan konten panel
                    pnlAsemenMedis_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmEMedrekRJ_31.TopLevel = False
                    frmEMedrekRJ_31.FormBorderStyle = FormBorderStyle.None
                    frmEMedrekRJ_31.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsemenMedis_Transaksi.Controls.Add(frmEMedrekRJ_31)

                    ' Tampilkan form anak
                    frmEMedrekRJ_31.LoadMe(FORM_MODE.FORM_MODE_ADD, ds.KDDOCTOR, ds.KDIDENTITAS, ds.KDKUNJUNGAN)
                    frmEMedrekRJ_31.Show()
                ElseIf cboAsesmenAwalMedis_Formulir.Text = "ASESMEN AWAL MEDIS PASIEN BEDAH" Then
                    Dim oDigital As New EMedrek.clsDigital_RJ_34
                    Dim dsDigital = oDigital.GetData(ds.KDKUNJUNGAN)
                    Dim sNoid As String = ""

                    If dsDigital IsNot Nothing Then
                        MsgBox("Formulir Sudah dibuatkan, Silahkan lakukan Edit untuk memperbaiki", MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                    End If

                    ' Bersihkan konten panel
                    pnlAsemenMedis_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmEMedrekRJ_34.TopLevel = False
                    frmEMedrekRJ_34.FormBorderStyle = FormBorderStyle.None
                    frmEMedrekRJ_34.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsemenMedis_Transaksi.Controls.Add(frmEMedrekRJ_34)

                    ' Tampilkan form anak
                    frmEMedrekRJ_34.LoadMe(FORM_MODE.FORM_MODE_ADD, ds.KDDOCTOR, ds.KDIDENTITAS, ds.KDKUNJUNGAN)
                    frmEMedrekRJ_34.Show()
                ElseIf cboAsesmenAwalMedis_Formulir.Text = "ASESMEN AWAL MEDIS PASIEN JANTUNG" Then
                    Dim oDigital As New EMedrek.clsDigital_RJ_29
                    Dim dsDigital = oDigital.GetData(ds.KDKUNJUNGAN)
                    Dim sNoid As String = ""

                    If dsDigital IsNot Nothing Then
                        MsgBox("Formulir Sudah dibuatkan, Silahkan lakukan Edit untuk memperbaiki", MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                    End If

                    ' Bersihkan konten panel
                    pnlAsemenMedis_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmEMedrekRJ_29.TopLevel = False
                    frmEMedrekRJ_29.FormBorderStyle = FormBorderStyle.None
                    frmEMedrekRJ_29.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsemenMedis_Transaksi.Controls.Add(frmEMedrekRJ_29)

                    ' Tampilkan form anak
                    frmEMedrekRJ_29.LoadMe(FORM_MODE.FORM_MODE_ADD, ds.KDDOCTOR, ds.KDIDENTITAS, ds.KDKUNJUNGAN)
                    frmEMedrekRJ_29.Show()
                ElseIf cboAsesmenAwalMedis_Formulir.Text = "ASESMEN AWAL MEDIS PASIEN NEUROLOGI" Then
                    Dim oDigital As New EMedrek.clsDigital_RJ_ASMEDNEURO
                    Dim dsDigital = oDigital.GetData(ds.KDKUNJUNGAN)
                    Dim sNoid As String = ""

                    If dsDigital IsNot Nothing Then
                        MsgBox("Formulir Sudah dibuatkan, Silahkan lakukan Edit untuk memperbaiki", MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                    End If

                    ' Bersihkan konten panel
                    pnlAsemenMedis_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmAsmedNeuro.TopLevel = False
                    frmAsmedNeuro.FormBorderStyle = FormBorderStyle.None
                    frmAsmedNeuro.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsemenMedis_Transaksi.Controls.Add(frmAsmedNeuro)

                    ' Tampilkan form anak
                    frmAsmedNeuro.LoadMe(FORM_MODE.FORM_MODE_ADD, ds.KDDOCTOR, ds.KDIDENTITAS, ds.KDKUNJUNGAN)
                    frmAsmedNeuro.Show()
                ElseIf cboAsesmenAwalMedis_Formulir.Text = "ASESMEN AWAL MEDIS PASIEN GIGI" Then
                    Dim oDigital As New EMedrek.clsDigital_RJ_35
                    Dim dsDigital = oDigital.GetData(ds.KDKUNJUNGAN)
                    Dim sNoid As String = ""

                    If dsDigital IsNot Nothing Then
                        MsgBox("Formulir Sudah dibuatkan, Silahkan lakukan Edit untuk memperbaiki", MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                    End If

                    ' Bersihkan konten panel
                    pnlAsemenMedis_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmEMedrekRJ_35.TopLevel = False
                    frmEMedrekRJ_35.FormBorderStyle = FormBorderStyle.None
                    frmEMedrekRJ_35.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsemenMedis_Transaksi.Controls.Add(frmEMedrekRJ_35)

                    ' Tampilkan form anak
                    frmEMedrekRJ_35.LoadMe(FORM_MODE.FORM_MODE_ADD, ds.KDDOCTOR, ds.KDIDENTITAS, ds.KDKUNJUNGAN)
                    frmEMedrekRJ_35.Show()
                ElseIf cboAsesmenAwalMedis_Formulir.Text = "ASESMEN AWAL MEDIS PASIEN PSIKIATRIK" Then
                    Dim oDigital As New EMedrek.clsDigital_RJ_30
                    Dim dsDigital = oDigital.GetData(ds.KDKUNJUNGAN)
                    Dim sNoid As String = ""

                    If dsDigital IsNot Nothing Then
                        MsgBox("Formulir Sudah dibuatkan, Silahkan lakukan Edit untuk memperbaiki", MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                    End If

                    ' Bersihkan konten panel
                    pnlAsemenMedis_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmEMedrekRJ_30.TopLevel = False
                    frmEMedrekRJ_30.FormBorderStyle = FormBorderStyle.None
                    frmEMedrekRJ_30.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsemenMedis_Transaksi.Controls.Add(frmEMedrekRJ_30)

                    ' Tampilkan form anak
                    frmEMedrekRJ_30.LoadMe(FORM_MODE.FORM_MODE_ADD, ds.KDDOCTOR, ds.KDIDENTITAS, ds.KDKUNJUNGAN)
                    frmEMedrekRJ_30.Show()
                ElseIf cboAsesmenAwalMedis_Formulir.Text = "ASESMEN AWAL MEDIS OBSTETRI & GYNEKOLOGI" Then
                    Dim oDigital As New EMedrek.clsS_DIGITAL_ASMEDOBGYN
                    Dim dsDigital = oDigital.GetData(ds.KDKUNJUNGAN)
                    Dim sNoid As String = ""

                    If dsDigital IsNot Nothing Then
                        MsgBox("Formulir Sudah dibuatkan, Silahkan lakukan Edit untuk memperbaiki", MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                    End If

                    ' Bersihkan konten panel
                    pnlAsemenMedis_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmEMedrekAsmedKebidanan.TopLevel = False
                    frmEMedrekAsmedKebidanan.FormBorderStyle = FormBorderStyle.None
                    frmEMedrekAsmedKebidanan.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsemenMedis_Transaksi.Controls.Add(frmEMedrekAsmedKebidanan)

                    ' Tampilkan form anak
                    frmEMedrekAsmedKebidanan.LoadMe(FORM_MODE.FORM_MODE_ADD, ds.KDDOCTOR, ds.KDIDENTITAS, ds.KDKUNJUNGAN)
                    frmEMedrekAsmedKebidanan.Show()
                ElseIf cboAsesmenAwalMedis_Formulir.Text = "ASESMEN AWAL MEDIS PASIEN MATA" Then
                    Dim oDigital As New EMedrek.clsDigital_RJ_32
                    Dim dsDigital = oDigital.GetData(ds.KDKUNJUNGAN)
                    Dim sNoid As String = ""

                    If dsDigital IsNot Nothing Then
                        MsgBox("Formulir Sudah dibuatkan, Silahkan lakukan Edit untuk memperbaiki", MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                    End If

                    ' Bersihkan konten panel
                    pnlAsemenMedis_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmEMedrekRJ_32.TopLevel = False
                    frmEMedrekRJ_32.FormBorderStyle = FormBorderStyle.None
                    frmEMedrekRJ_32.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsemenMedis_Transaksi.Controls.Add(frmEMedrekRJ_32)

                    ' Tampilkan form anak
                    frmEMedrekRJ_32.LoadMe(FORM_MODE.FORM_MODE_ADD, ds.KDDOCTOR, ds.KDIDENTITAS, ds.KDKUNJUNGAN)
                    frmEMedrekRJ_32.Show()
                ElseIf cboAsesmenAwalMedis_Formulir.Text = "UJI FUNGSI, LAYANAN & PROSEDUR KEDOKTERAN FISIK & REHABILITASI" Then
                    'Dim oDigital As New EMedrek.clsFisioterafi_1
                    'Dim dsDigital = oDigital.GetData(ds.KDKUNJUNGAN)

                    'If dsDigital IsNot Nothing Then
                    '    MsgBox("Formulir Sudah dibuatkan, Silahkan lakukan Edit untuk memperbaiki", MsgBoxStyle.Exclamation, Me.Text)
                    '    Exit Sub
                    'End If

                    '' Bersihkan konten panel
                    'pnlAsemenMedis_Transaksi.Controls.Clear()

                    '' Set properti form anak
                    'frmLembarUjiFungsiRehab.TopLevel = False
                    'frmLembarUjiFungsiRehab.FormBorderStyle = FormBorderStyle.None
                    'frmLembarUjiFungsiRehab.Dock = DockStyle.Fill

                    '' Tambahkan form anak ke panel
                    'pnlAsemenMedis_Transaksi.Controls.Add(frmLembarUjiFungsiRehab)

                    '' Tampilkan form anak
                    'frmLembarUjiFungsiRehab.LoadMe(FORM_MODE.FORM_MODE_ADD, ds.KDKUNJUNGAN)
                    'frmLembarUjiFungsiRehab.Show()
                ElseIf cboAsesmenAwalMedis_Formulir.Text = "FORMULIR CATATAN KLINIS REHABILITASI MEDIK" Then
                    'Dim oRehab As New EMedrek.clsFisioterafi_1
                    'Dim dsRehab = oRehab.GetData(ds.KDKUNJUNGAN)
                    'If dsRehab IsNot Nothing Then
                    '    Dim oDigital As New EMedrek.clsFisioterafi_2
                    '    Dim dsDigital = oDigital.GetData(ds.KDKUNJUNGAN)

                    '    If dsDigital IsNot Nothing Then
                    '        MsgBox("Formulir Sudah dibuatkan, Silahkan lakukan Edit untuk memperbaiki", MsgBoxStyle.Exclamation, Me.Text)
                    '        Exit Sub
                    '    End If

                    '    ' Bersihkan konten panel
                    '    pnlAsemenMedis_Transaksi.Controls.Clear()

                    '    ' Set properti form anak
                    '    frmCatatanKlinisRehab.TopLevel = False
                    '    frmCatatanKlinisRehab.FormBorderStyle = FormBorderStyle.None
                    '    frmCatatanKlinisRehab.Dock = DockStyle.Fill

                    '    ' Tambahkan form anak ke panel
                    '    pnlAsemenMedis_Transaksi.Controls.Add(frmCatatanKlinisRehab)

                    '    ' Tampilkan form anak
                    '    frmCatatanKlinisRehab.LoadMe(FORM_MODE.FORM_MODE_ADD, ds.KDKUNJUNGAN)
                    '    frmCatatanKlinisRehab.Show()
                    'Else
                    '    MsgBox("Lembar Uji Fungsi Belum di Simpan", MsgBoxStyle.Exclamation, Me.Text)
                    'End If
                ElseIf cboAsesmenAwalMedis_Formulir.Text = "LAYANAN KEDOKTERAN FISIK DAN REHABILITASI" Then
                    'Dim oRehab As New EMedrek.clsFisioterafi_1
                    'Dim oRehab2 As New EMedrek.clsFisioterafi_2

                    'Dim dsRehab = oRehab.GetData(ds.KDKUNJUNGAN)
                    'If dsRehab IsNot Nothing Then
                    '    Dim dsRehab2 = oRehab2.GetData(dsRehab.KDKUNJUNGAN)
                    '    If dsRehab2 IsNot Nothing Then
                    '        Dim oDigital As New EMedrek.clsFisioterafi_3
                    '        Dim dsDigital = oDigital.GetData(dsRehab2.KDKUNJUNGAN)

                    '        If dsDigital IsNot Nothing Then
                    '            MsgBox("Formulir Sudah dibuatkan, Silahkan lakukan Edit untuk memperbaiki", MsgBoxStyle.Exclamation, Me.Text)
                    '            Exit Sub
                    '        End If

                    '        ' Bersihkan konten panel
                    '        pnlAsemenMedis_Transaksi.Controls.Clear()

                    '        ' Set properti form anak
                    '        frmResumeRawatJalanRehab.TopLevel = False
                    '        frmResumeRawatJalanRehab.FormBorderStyle = FormBorderStyle.None
                    '        frmResumeRawatJalanRehab.Dock = DockStyle.Fill

                    '        ' Tambahkan form anak ke panel
                    '        pnlAsemenMedis_Transaksi.Controls.Add(frmResumeRawatJalanRehab)

                    '        ' Tampilkan form anak
                    '        frmResumeRawatJalanRehab.LoadMe(FORM_MODE.FORM_MODE_ADD, dsRehab2.KDKUNJUNGAN, dsRehab2.DIAGNOSA, dsRehab2.KDDOCTOR, dsRehab.ALAMATSIMPANTTDPASIEN)
                    '        frmResumeRawatJalanRehab.Show()
                    '    Else
                    '        MsgBox("Catatan Klinis Belum di Simpan", MsgBoxStyle.Exclamation, Me.Text)
                    '    End If

                    'Else
                    '    MsgBox("Lembar Uji Fungsi Belum di Simpan", MsgBoxStyle.Exclamation, Me.Text)
                    'End If
                ElseIf cboAsesmenAwalMedis_Formulir.Text = "ASESMEN AWAL MEDIS PASIEN GIZI" Then
                    Dim oDigital As New EMedrek.clsDigital_RJ_18
                    Dim dsDigital = oDigital.GetData(ds.KDKUNJUNGAN)
                    Dim sNoid As String = ""

                    If dsDigital IsNot Nothing Then
                        MsgBox("Formulir Sudah dibuatkan, Silahkan lakukan Edit untuk memperbaiki", MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                    End If

                    ' Bersihkan konten panel
                    pnlAsemenMedis_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmEMedrekRJ_18.TopLevel = False
                    frmEMedrekRJ_18.FormBorderStyle = FormBorderStyle.None
                    frmEMedrekRJ_18.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsemenMedis_Transaksi.Controls.Add(frmEMedrekRJ_18)

                    ' Tampilkan form anak
                    frmEMedrekRJ_18.LoadMe(FORM_MODE.FORM_MODE_ADD, ds.KDDOCTOR, ds.KDIDENTITAS, ds.KDKUNJUNGAN)
                    frmEMedrekRJ_18.Show()
                ElseIf cboAsesmenAwalMedis_Formulir.Text = "ASESMEN AWAL MEDIS PASIEN TUMBUH KEMBANG" Then
                    Dim oDigital As New EMedrek.clsDigital_RJ_41
                    Dim dsDigital = oDigital.GetData(ds.KDKUNJUNGAN)
                    Dim sNoid As String = ""

                    If dsDigital IsNot Nothing Then
                        MsgBox("Formulir Sudah dibuatkan, Silahkan lakukan Edit untuk memperbaiki", MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                    End If

                    ' Bersihkan konten panel
                    pnlAsemenMedis_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmEMedrekRJ_41.TopLevel = False
                    frmEMedrekRJ_41.FormBorderStyle = FormBorderStyle.None
                    frmEMedrekRJ_41.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsemenMedis_Transaksi.Controls.Add(frmEMedrekRJ_41)

                    ' Tampilkan form anak
                    frmEMedrekRJ_41.LoadMe(FORM_MODE.FORM_MODE_ADD, ds.KDDOCTOR, ds.KDIDENTITAS, ds.KDKUNJUNGAN)
                    frmEMedrekRJ_41.Show()
                ElseIf cboAsesmenAwalMedis_Formulir.Text = "ASESMEN AWAL MEDIS PASIEN ANAK" Then
                    Dim oDigital As New EMedrek.clsDigital_RJ_ASMEDANAK
                    Dim dsDigital = oDigital.GetData(ds.KDKUNJUNGAN)
                    Dim sNoid As String = ""

                    If dsDigital IsNot Nothing Then
                        MsgBox("Formulir Sudah dibuatkan, Silahkan lakukan Edit untuk memperbaiki", MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                    End If

                    ' Bersihkan konten panel
                    pnlAsemenMedis_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmEMedrek_AsmedAnak.TopLevel = False
                    frmEMedrek_AsmedAnak.FormBorderStyle = FormBorderStyle.None
                    frmEMedrek_AsmedAnak.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsemenMedis_Transaksi.Controls.Add(frmEMedrek_AsmedAnak)

                    ' Tampilkan form anak
                    frmEMedrek_AsmedAnak.LoadMe(FORM_MODE.FORM_MODE_ADD, ds.KDDOCTOR, ds.KDIDENTITAS, ds.KDKUNJUNGAN)
                    frmEMedrek_AsmedAnak.Show()
                ElseIf cboAsesmenAwalMedis_Formulir.Text = "ASESMEN AWAL MEDIS PASIEN KULIT DAN KELAMIN" Then
                    Dim oDigital As New EMedrek.clsDigital_RJ_33
                    Dim dsDigital = oDigital.GetData(ds.KDKUNJUNGAN)
                    Dim sNoid As String = ""

                    If dsDigital IsNot Nothing Then
                        MsgBox("Formulir Sudah dibuatkan, Silahkan lakukan Edit untuk memperbaiki", MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                    End If

                    ' Bersihkan konten panel
                    pnlAsemenMedis_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmEMedrekRJ_33.TopLevel = False
                    frmEMedrekRJ_33.FormBorderStyle = FormBorderStyle.None
                    frmEMedrekRJ_33.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsemenMedis_Transaksi.Controls.Add(frmEMedrekRJ_33)

                    ' Tampilkan form anak
                    frmEMedrekRJ_33.LoadMe(FORM_MODE.FORM_MODE_ADD, ds.KDDOCTOR, ds.KDIDENTITAS, ds.KDKUNJUNGAN)
                    frmEMedrekRJ_33.Show()
                ElseIf cboAsesmenAwalMedis_Formulir.Text = "ASESMEN AWAL MEDIS PASIEN THT" Then
                    Dim oDigital As New EMedrek.clsDigital_RJ_ASMEDTHT
                    Dim dsDigital = oDigital.GetData(ds.KDKUNJUNGAN)
                    Dim sNoid As String = ""

                    If dsDigital IsNot Nothing Then
                        MsgBox("Formulir Sudah dibuatkan, Silahkan lakukan Edit untuk memperbaiki", MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                    End If

                    ' Bersihkan konten panel
                    pnlAsemenMedis_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmAsesmenAwalMedisTHT.TopLevel = False
                    frmAsesmenAwalMedisTHT.FormBorderStyle = FormBorderStyle.None
                    frmAsesmenAwalMedisTHT.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsemenMedis_Transaksi.Controls.Add(frmAsesmenAwalMedisTHT)

                    ' Tampilkan form anak
                    frmAsesmenAwalMedisTHT.LoadMe(FORM_MODE.FORM_MODE_ADD, ds.KDDOCTOR, ds.KDIDENTITAS, ds.KDKUNJUNGAN)
                    frmAsesmenAwalMedisTHT.Show()
                ElseIf cboAsesmenAwalMedis_Formulir.Text = "ASESMEN AWAL MEDIS NEONATUS" Then
                    Dim oDigital As New EMedrek.clsDigital_RJ_Neonatus
                    Dim dsDigital = oDigital.GetData(ds.KDKUNJUNGAN)
                    Dim sNoid As String = ""

                    If dsDigital IsNot Nothing Then
                        MsgBox("Formulir Sudah dibuatkan, Silahkan lakukan Edit untuk memperbaiki", MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                    End If

                    ' Bersihkan konten panel
                    pnlAsemenMedis_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmEMedrekAWAL_MEDIS_NEONATUS_RJ.TopLevel = False
                    frmEMedrekAWAL_MEDIS_NEONATUS_RJ.FormBorderStyle = FormBorderStyle.None
                    frmEMedrekAWAL_MEDIS_NEONATUS_RJ.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsemenMedis_Transaksi.Controls.Add(frmEMedrekAWAL_MEDIS_NEONATUS_RJ)

                    ' Tampilkan form anak
                    frmEMedrekAWAL_MEDIS_NEONATUS_RJ.LoadMe(FORM_MODE.FORM_MODE_ADD, ds.KDDOCTOR, ds.KDIDENTITAS, ds.KDKUNJUNGAN)
                    frmEMedrekAWAL_MEDIS_NEONATUS_RJ.Show()
                ElseIf cboAsesmenAwalMedis_Formulir.Text = "ASESMEN AWAL MEDIS RAWAT JALAN" Then
                    Dim oDigital As New EMedrek.clsDigital_AsesmenMedisRawatJalan
                    Dim dsDigital = oDigital.GetData(ds.KDKUNJUNGAN)
                    Dim sNoid As String = ""

                    If dsDigital IsNot Nothing Then
                        MsgBox("Formulir Sudah dibuatkan, Silahkan lakukan Edit untuk memperbaiki", MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                    End If

                    ' Bersihkan konten panel
                    pnlAsemenMedis_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmAsesmenMedisRawatJalan.TopLevel = False
                    frmAsesmenMedisRawatJalan.FormBorderStyle = FormBorderStyle.None
                    frmAsesmenMedisRawatJalan.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsemenMedis_Transaksi.Controls.Add(frmAsesmenMedisRawatJalan)

                    ' Tampilkan form anak
                    frmAsesmenMedisRawatJalan.LoadMe(FORM_MODE.FORM_MODE_ADD, ds.KDDOCTOR, ds.KDIDENTITAS, ds.KDKUNJUNGAN)
                    frmAsesmenMedisRawatJalan.Show()

                Else
                    MsgBox("Dokumen Belum Ada", MsgBoxStyle.Exclamation, Me.Text)
                    fn_EmptyAsesmenMedis()
                End If
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                fn_EmptyAsesmenMedis()
            End Try
        Else
            MsgBox("Identitas Kosong", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub picAsesmenAwalMedis_Update_Click(sender As Object, e As EventArgs) Handles picAsesmenAwalMedis_Update.Click
        If grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR") Is Nothing Then
            MsgBox("Silahkan Pilih Kode", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDUSER") <> sUserID Then
            MsgBox("Maap Tidak dapat Rubah Silahkan Hubungi User " & grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDUSER"), MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim frmPesanDelete As New frmPesanDelete
        frmPesanDelete.ShowDialog(Me)

        If sPesanHapus <> "XXXXXBATALXXXXX" Then
            Dim oDelete As New Setting.clsDelete

            If oDelete.InsertData("UPDATEASESMENAWALMEDIS", sUserID, grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") & " " & sPesanHapus & " " & Now.ToString("dd-MM-yyyy HH:mm:ss"), grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR")) = False Then
                MsgBox("Maap Insert Delete Gagal", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
        Else
            Exit Sub
        End If

        pnlAsesmenMedis_pic.Dock = DockStyle.None

        lPanelAsesmenMedis_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lPanelAsesmenMedis_Panel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lAsesmenMedis_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

        If grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") = "ASESMEN AWAL MEDIS IGD" Then
            Dim oDigital As New Transaksi.clsDigital_IGD_01
            Dim dsDigital = oDigital.GetDataByKodeIGD(grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR"))

            If dsDigital IsNot Nothing Then
                Try
                    ' Bersihkan konten panel
                    pnlAsemenMedis_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmAsesmenAwalMedisIGD2.TopLevel = False
                    frmAsesmenAwalMedisIGD2.FormBorderStyle = FormBorderStyle.None
                    frmAsesmenAwalMedisIGD2.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsemenMedis_Transaksi.Controls.Add(frmAsesmenAwalMedisIGD2)

                    ' Tampilkan form anak
                    frmAsesmenAwalMedisIGD2.LoadMe(True, dsDigital.KODE, 0, dsDigital.KDPENDAFTARAN, lblKDKUNJUNGAN.Text, dsDigital.DOCTOR_KODE, dsDigital.KDCUSTOMER, lblNamaPasien.Text, sTanggalLahir)
                    frmAsesmenAwalMedisIGD2.Show()
                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
            End If
        ElseIf grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") = "CATATAN MEDIS AWAL RAWAT INAP DAN DISCHARGE PLANNING" Then
            Dim oDigital As New EMedrek.clsDigital_DischargePlanning
            Dim dsDigital = oDigital.GetData(grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR"))

            If dsDigital IsNot Nothing Then
                Try
                    ' Bersihkan konten panel
                    pnlAsemenMedis_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmDischargePlanning.TopLevel = False
                    frmDischargePlanning.FormBorderStyle = FormBorderStyle.None
                    frmDischargePlanning.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsemenMedis_Transaksi.Controls.Add(frmDischargePlanning)

                    ' Tampilkan form anak
                    frmDischargePlanning.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.ANAMNESIS_23, dsDigital.ANAMNESIS_24, dsDigital.DOCTOR_KODE, dsDigital.KDCUSTOMER, lblNamaPasien.Text, dsDigital.KDREG)
                    frmDischargePlanning.Show()
                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
            End If
        ElseIf grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") = "KARTU ANASTESI A" Then
            Dim oDigital As New EMedrek.clsS_DIGITAL_OK_KARTU_ANESTESI_A
            Dim dsDigital = oDigital.GetData(grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR"))

            If dsDigital IsNot Nothing Then
                Try
                    ' Bersihkan konten panel
                    pnlAsemenMedis_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmKartuAnastesi_A.TopLevel = False
                    frmKartuAnastesi_A.FormBorderStyle = FormBorderStyle.None
                    frmKartuAnastesi_A.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsemenMedis_Transaksi.Controls.Add(frmKartuAnastesi_A)

                    ' Tampilkan form anak
                    frmKartuAnastesi_A.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.KDPENDAFTARAN, dsDigital.KODE)
                    frmKartuAnastesi_A.Show()
                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
            End If
        ElseIf grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") = "KARTU ANASTESI B" Then
            Dim oDigital As New EMedrek.clsS_DIGITAL_OK_KARTU_ANESTESI_B
            Dim dsDigital = oDigital.GetData(grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR"))

            If dsDigital IsNot Nothing Then
                Try
                    ' Bersihkan konten panel
                    pnlAsemenMedis_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmKartuAnastesi_B.TopLevel = False
                    frmKartuAnastesi_B.FormBorderStyle = FormBorderStyle.None
                    frmKartuAnastesi_B.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsemenMedis_Transaksi.Controls.Add(frmKartuAnastesi_B)

                    ' Tampilkan form anak
                    frmKartuAnastesi_B.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.KDPENDAFTARAN, dsDigital.KODE)
                    frmKartuAnastesi_B.Show()
                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
            End If
        ElseIf grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") = "ASESMEN AWAL MEDIS PASIEN PARU" Then
            Dim oDigital As New EMedrek.clsDigital_RJ_36
            Dim dsDigital = oDigital.GetData(grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR"))

            If dsDigital IsNot Nothing Then
                Try
                    ' Bersihkan konten panel
                    pnlAsemenMedis_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmEMedrekRJ_36.TopLevel = False
                    frmEMedrekRJ_36.FormBorderStyle = FormBorderStyle.None
                    frmEMedrekRJ_36.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsemenMedis_Transaksi.Controls.Add(frmEMedrekRJ_36)

                    ' Tampilkan form anak
                    frmEMedrekRJ_36.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.DOKTER_KODE, dsDigital.KDIDENTITAS, dsDigital.KDKUNJUNGAN)
                    frmEMedrekRJ_36.Show()
                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
            End If
        ElseIf grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") = "ASESMEN AWAL MEDIS PASIEN PENYAKIT DALAM" Then
            Dim oDigital As New EMedrek.clsDigital_RJ_31
            Dim dsDigital = oDigital.GetData(grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR"))

            If dsDigital IsNot Nothing Then
                Try
                    ' Bersihkan konten panel
                    pnlAsemenMedis_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmEMedrekRJ_31.TopLevel = False
                    frmEMedrekRJ_31.FormBorderStyle = FormBorderStyle.None
                    frmEMedrekRJ_31.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsemenMedis_Transaksi.Controls.Add(frmEMedrekRJ_31)

                    ' Tampilkan form anak
                    frmEMedrekRJ_31.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.DOCTOR_KODE, dsDigital.KDIDENTITAS, dsDigital.KDKUNJUNGAN)
                    frmEMedrekRJ_31.Show()
                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
            End If
        ElseIf grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") = "ASESMEN AWAL MEDIS PASIEN BEDAH" Then
            Dim oDigital As New EMedrek.clsDigital_RJ_34
            Dim dsDigital = oDigital.GetData(grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR"))

            If dsDigital IsNot Nothing Then
                Try
                    ' Bersihkan konten panel
                    pnlAsemenMedis_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmEMedrekRJ_34.TopLevel = False
                    frmEMedrekRJ_34.FormBorderStyle = FormBorderStyle.None
                    frmEMedrekRJ_34.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsemenMedis_Transaksi.Controls.Add(frmEMedrekRJ_34)

                    ' Tampilkan form anak
                    frmEMedrekRJ_34.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.KDDOCTOR_KODE, dsDigital.KDIDENTITAS, dsDigital.KDKUNJUNGAN)
                    frmEMedrekRJ_34.Show()
                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
            End If
        ElseIf grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") = "ASESMEN AWAL MEDIS PASIEN JANTUNG" Then
            Dim oDigital As New EMedrek.clsDigital_RJ_29
            Dim dsDigital = oDigital.GetData(grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR"))

            If dsDigital IsNot Nothing Then
                Try
                    ' Bersihkan konten panel
                    pnlAsemenMedis_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmEMedrekRJ_29.TopLevel = False
                    frmEMedrekRJ_29.FormBorderStyle = FormBorderStyle.None
                    frmEMedrekRJ_29.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsemenMedis_Transaksi.Controls.Add(frmEMedrekRJ_29)

                    ' Tampilkan form anak
                    frmEMedrekRJ_29.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.DOKTER_KODE, dsDigital.KDIDENTITAS, dsDigital.KDKUNJUNGAN)
                    frmEMedrekRJ_29.Show()
                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
            End If
        ElseIf grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") = "ASESMEN AWAL MEDIS PASIEN NEUROLOGI" Then
            Dim oDigital As New EMedrek.clsDigital_RJ_ASMEDNEURO
            Dim dsDigital = oDigital.GetData(grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR"))

            If dsDigital IsNot Nothing Then
                Try
                    ' Bersihkan konten panel
                    pnlAsemenMedis_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmAsmedNeuro.TopLevel = False
                    frmAsmedNeuro.FormBorderStyle = FormBorderStyle.None
                    frmAsmedNeuro.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsemenMedis_Transaksi.Controls.Add(frmAsmedNeuro)

                    ' Tampilkan form anak
                    frmAsmedNeuro.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.KDCUSTOMER, dsDigital.KDIDENTITAS, dsDigital.KDKUNJUNGAN)
                    frmAsmedNeuro.Show()
                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
            End If
        ElseIf grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") = "ASESMEN AWAL MEDIS PASIEN GIGI" Then
            Dim oDigital As New EMedrek.clsDigital_RJ_35
            Dim dsDigital = oDigital.GetData(grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR"))

            If dsDigital IsNot Nothing Then
                Try
                    ' Bersihkan konten panel
                    pnlAsemenMedis_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmEMedrekRJ_35.TopLevel = False
                    frmEMedrekRJ_35.FormBorderStyle = FormBorderStyle.None
                    frmEMedrekRJ_35.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsemenMedis_Transaksi.Controls.Add(frmEMedrekRJ_35)

                    ' Tampilkan form anak
                    frmEMedrekRJ_35.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.DOKTER_KODE, dsDigital.KDIDENTITAS, dsDigital.KDKUNJUNGAN)
                    frmEMedrekRJ_35.Show()
                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
            End If
        ElseIf grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") = "ASESMEN AWAL MEDIS PASIEN PSIKIATRIK" Then
            Dim oDigital As New EMedrek.clsDigital_RJ_30
            Dim dsDigital = oDigital.GetData(grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR"))

            If dsDigital IsNot Nothing Then
                Try
                    ' Bersihkan konten panel
                    pnlAsemenMedis_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmEMedrekRJ_30.TopLevel = False
                    frmEMedrekRJ_30.FormBorderStyle = FormBorderStyle.None
                    frmEMedrekRJ_30.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsemenMedis_Transaksi.Controls.Add(frmEMedrekRJ_30)

                    ' Tampilkan form anak
                    frmEMedrekRJ_30.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.DOKTER_KODE, dsDigital.KDIDENTITAS, dsDigital.KDKUNJUNGAN)
                    frmEMedrekRJ_30.Show()
                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
            End If
        ElseIf grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") = "UJI FUNGSI, LAYANAN & PROSEDUR KEDOKTERAN FISIK & REHABILITASI" Then
            'Dim oDigital As New EMedrek.clsFisioterafi_1
            'Dim dsDigital = oDigital.GetData(grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR"))

            'If dsDigital IsNot Nothing Then
            '    Try
            '        ' Bersihkan konten panel
            '        pnlAsemenMedis_Transaksi.Controls.Clear()

            '        ' Set properti form anak
            '        frmLembarUjiFungsiRehab.TopLevel = False
            '        frmLembarUjiFungsiRehab.FormBorderStyle = FormBorderStyle.None
            '        frmLembarUjiFungsiRehab.Dock = DockStyle.Fill

            '        ' Tambahkan form anak ke panel
            '        pnlAsemenMedis_Transaksi.Controls.Add(frmLembarUjiFungsiRehab)

            '        ' Tampilkan form anak
            '        frmLembarUjiFungsiRehab.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.KDKUNJUNGAN)
            '        frmLembarUjiFungsiRehab.Show()
            '    Catch ex As Exception
            '        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            '    End Try
            'Else
            '    MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
            'End If
        ElseIf grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") = "FORMULIR CATATAN KLINIS REHABILITASI MEDIK" Then
            'Dim oDigital As New EMedrek.clsFisioterafi_2
            'Dim dsDigital = oDigital.GetData(grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR"))

            'If dsDigital IsNot Nothing Then
            '    Try
            '        ' Bersihkan konten panel
            '        pnlAsemenMedis_Transaksi.Controls.Clear()

            '        ' Set properti form anak
            '        frmCatatanKlinisRehab.TopLevel = False
            '        frmCatatanKlinisRehab.FormBorderStyle = FormBorderStyle.None
            '        frmCatatanKlinisRehab.Dock = DockStyle.Fill

            '        ' Tambahkan form anak ke panel
            '        pnlAsemenMedis_Transaksi.Controls.Add(frmCatatanKlinisRehab)

            '        ' Tampilkan form anak
            '        frmCatatanKlinisRehab.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.KDKUNJUNGAN)
            '        frmCatatanKlinisRehab.Show()
            '    Catch ex As Exception
            '        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            '    End Try
            'Else
            '    MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
            'End If
        ElseIf grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") = "LAYANAN KEDOKTERAN FISIK DAN REHABILITASI" Then
            Try
                Dim oDigital As New EMedrek.clsFisioterafi_3
                Dim dsDigital = oDigital.GetData(grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR"))

                If dsDigital IsNot Nothing Then
                    Try
                        ' Bersihkan konten panel
                        pnlAsemenMedis_Transaksi.Controls.Clear()

                        ' Set properti form anak
                        frmResumeRawatJalanRehab.TopLevel = False
                        frmResumeRawatJalanRehab.FormBorderStyle = FormBorderStyle.None
                        frmResumeRawatJalanRehab.Dock = DockStyle.Fill

                        ' Tambahkan form anak ke panel
                        pnlAsemenMedis_Transaksi.Controls.Add(frmResumeRawatJalanRehab)

                        ' Tampilkan form anak
                        frmResumeRawatJalanRehab.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.KDKUNJUNGAN, dsDigital.DIAGNOSA, dsDigital.KDDOCTOR, dsDigital.ALAMATSIMPANTTDPASIEN)
                        frmResumeRawatJalanRehab.Show()
                    Catch ex As Exception
                        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                Else
                    MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") = "ASESMEN AWAL MEDIS PASIEN GIZI" Then
            Try
                Dim oDigital As New EMedrek.clsDigital_RJ_18
                Dim dsDigital = oDigital.GetData(grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR"))

                If dsDigital IsNot Nothing Then
                    Try
                        ' Bersihkan konten panel
                        pnlAsemenMedis_Transaksi.Controls.Clear()

                        ' Set properti form anak
                        frmEMedrekRJ_18.TopLevel = False
                        frmEMedrekRJ_18.FormBorderStyle = FormBorderStyle.None
                        frmEMedrekRJ_18.Dock = DockStyle.Fill

                        ' Tambahkan form anak ke panel
                        pnlAsemenMedis_Transaksi.Controls.Add(frmEMedrekRJ_18)

                        ' Tampilkan form anak
                        frmEMedrekRJ_18.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.DOKTER_KODE, dsDigital.KDIDENTITAS, dsDigital.KDKUNJUNGAN)
                        frmEMedrekRJ_18.Show()
                    Catch ex As Exception
                        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                Else
                    MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") = "ASESMEN AWAL MEDIS PASIEN TUMBUH KEMBANG" Then
            Try
                Dim oDigital As New EMedrek.clsDigital_RJ_41
                Dim dsDigital = oDigital.GetData(grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR"))

                If dsDigital IsNot Nothing Then
                    Try
                        ' Bersihkan konten panel
                        pnlAsemenMedis_Transaksi.Controls.Clear()

                        ' Set properti form anak
                        frmEMedrekRJ_41.TopLevel = False
                        frmEMedrekRJ_41.FormBorderStyle = FormBorderStyle.None
                        frmEMedrekRJ_41.Dock = DockStyle.Fill

                        ' Tambahkan form anak ke panel
                        pnlAsemenMedis_Transaksi.Controls.Add(frmEMedrekRJ_41)

                        ' Tampilkan form anak
                        frmEMedrekRJ_41.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.DOKTER_KODE, dsDigital.KDIDENTITAS, dsDigital.KDKUNJUNGAN)
                        frmEMedrekRJ_41.Show()
                    Catch ex As Exception
                        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                Else
                    MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") = "ASESMEN AWAL MEDIS PASIEN ANAK" Then
            Try
                Dim oDigital As New EMedrek.clsDigital_RJ_ASMEDANAK
                Dim dsDigital = oDigital.GetData(grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR"))

                If dsDigital IsNot Nothing Then
                    Try
                        ' Bersihkan konten panel
                        pnlAsemenMedis_Transaksi.Controls.Clear()

                        ' Set properti form anak
                        frmEMedrek_AsmedAnak.TopLevel = False
                        frmEMedrek_AsmedAnak.FormBorderStyle = FormBorderStyle.None
                        frmEMedrek_AsmedAnak.Dock = DockStyle.Fill

                        ' Tambahkan form anak ke panel
                        pnlAsemenMedis_Transaksi.Controls.Add(frmEMedrek_AsmedAnak)

                        ' Tampilkan form anak
                        frmEMedrek_AsmedAnak.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.KDUSER_SIGNATURE, dsDigital.KDIDENTITAS, dsDigital.KDKUNJUNGAN)
                        frmEMedrek_AsmedAnak.Show()
                    Catch ex As Exception
                        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                Else
                    MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") = "ASESMEN AWAL MEDIS OBSTETRI & GYNEKOLOGI" Then
            Try
                Dim oDigital As New EMedrek.clsS_DIGITAL_ASMEDOBGYN
                Dim dsDigital = oDigital.GetData(grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR"))

                If dsDigital IsNot Nothing Then
                    Try
                        ' Bersihkan konten panel
                        pnlAsemenMedis_Transaksi.Controls.Clear()

                        ' Set properti form anak
                        frmEMedrekAsmedKebidanan.TopLevel = False
                        frmEMedrekAsmedKebidanan.FormBorderStyle = FormBorderStyle.None
                        frmEMedrekAsmedKebidanan.Dock = DockStyle.Fill

                        ' Tambahkan form anak ke panel
                        pnlAsemenMedis_Transaksi.Controls.Add(frmEMedrekAsmedKebidanan)

                        ' Tampilkan form anak
                        frmEMedrekAsmedKebidanan.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.KDUSER_SIGNATURE, dsDigital.KDIDENTITAS, dsDigital.KDKUNJUNGAN)
                        frmEMedrekAsmedKebidanan.Show()
                    Catch ex As Exception
                        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                Else
                    MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") = "ASESMEN AWAL MEDIS PASIEN KULIT DAN KELAMIN" Then
            Try
                Dim oDigital As New EMedrek.clsDigital_RJ_33
                Dim dsDigital = oDigital.GetData(grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR"))

                If dsDigital IsNot Nothing Then
                    Try
                        ' Bersihkan konten panel
                        pnlAsemenMedis_Transaksi.Controls.Clear()

                        ' Set properti form anak
                        frmEMedrekRJ_33.TopLevel = False
                        frmEMedrekRJ_33.FormBorderStyle = FormBorderStyle.None
                        frmEMedrekRJ_33.Dock = DockStyle.Fill

                        ' Tambahkan form anak ke panel
                        pnlAsemenMedis_Transaksi.Controls.Add(frmEMedrekRJ_33)

                        ' Tampilkan form anak
                        frmEMedrekRJ_33.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.KDUSER_SIGNATURE, dsDigital.KDIDENTITAS, dsDigital.KDKUNJUNGAN)
                        frmEMedrekRJ_33.Show()
                    Catch ex As Exception
                        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                Else
                    MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") = "ASESMEN AWAL MEDIS PASIEN MATA" Then
            Try
                Dim oDigital As New EMedrek.clsDigital_RJ_32
                Dim dsDigital = oDigital.GetData(grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR"))

                If dsDigital IsNot Nothing Then
                    Try
                        ' Bersihkan konten panel
                        pnlAsemenMedis_Transaksi.Controls.Clear()

                        ' Set properti form anak
                        frmEMedrekRJ_32.TopLevel = False
                        frmEMedrekRJ_32.FormBorderStyle = FormBorderStyle.None
                        frmEMedrekRJ_32.Dock = DockStyle.Fill

                        ' Tambahkan form anak ke panel
                        pnlAsemenMedis_Transaksi.Controls.Add(frmEMedrekRJ_32)

                        ' Tampilkan form anak
                        frmEMedrekRJ_32.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.KDUSER_SIGNATURE, dsDigital.KDIDENTITAS, dsDigital.KDKUNJUNGAN)
                        frmEMedrekRJ_32.Show()
                    Catch ex As Exception
                        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                Else
                    MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") = "ASESMEN AWAL MEDIS PASIEN THT" Then
            Try
                Dim oDigital As New EMedrek.clsDigital_RJ_ASMEDTHT
                Dim dsDigital = oDigital.GetData(grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR"))

                If dsDigital IsNot Nothing Then
                    Try
                        ' Bersihkan konten panel
                        pnlAsemenMedis_Transaksi.Controls.Clear()

                        ' Set properti form anak
                        frmAsesmenAwalMedisTHT.TopLevel = False
                        frmAsesmenAwalMedisTHT.FormBorderStyle = FormBorderStyle.None
                        frmAsesmenAwalMedisTHT.Dock = DockStyle.Fill

                        ' Tambahkan form anak ke panel
                        pnlAsemenMedis_Transaksi.Controls.Add(frmAsesmenAwalMedisTHT)

                        ' Tampilkan form anak
                        frmAsesmenAwalMedisTHT.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.KDUSER_SIGNATURE, dsDigital.KDIDENTITAS, dsDigital.KDKUNJUNGAN)
                        frmAsesmenAwalMedisTHT.Show()
                    Catch ex As Exception
                        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                Else
                    MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") = "ASESMEN AWAL MEDIS NEONATUS" Then
            Try
                Dim oDigital As New EMedrek.clsDigital_RJ_Neonatus
                Dim dsDigital = oDigital.GetData(grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR"))

                If dsDigital IsNot Nothing Then
                    Try
                        ' Bersihkan konten panel
                        pnlAsemenMedis_Transaksi.Controls.Clear()

                        ' Set properti form anak
                        frmEMedrekAWAL_MEDIS_NEONATUS_RJ.TopLevel = False
                        frmEMedrekAWAL_MEDIS_NEONATUS_RJ.FormBorderStyle = FormBorderStyle.None
                        frmEMedrekAWAL_MEDIS_NEONATUS_RJ.Dock = DockStyle.Fill

                        ' Tambahkan form anak ke panel
                        pnlAsemenMedis_Transaksi.Controls.Add(frmEMedrekAWAL_MEDIS_NEONATUS_RJ)

                        ' Tampilkan form anak
                        frmEMedrekAWAL_MEDIS_NEONATUS_RJ.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.KDUSER_SIGNATURE, dsDigital.KDIDENTITAS, dsDigital.KDKUNJUNGAN)
                        frmEMedrekAWAL_MEDIS_NEONATUS_RJ.Show()
                    Catch ex As Exception
                        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                Else
                    MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") = "ASESMEN AWAL MEDIS RAWAT JALAN" Then
            Try
                Dim oDigital As New EMedrek.clsDigital_AsesmenMedisRawatJalan
                Dim dsDigital = oDigital.GetData(grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR"))

                If dsDigital IsNot Nothing Then
                    Try
                        ' Bersihkan konten panel
                        pnlAsemenMedis_Transaksi.Controls.Clear()

                        ' Set properti form anak
                        frmAsesmenMedisRawatJalan.TopLevel = False
                        frmAsesmenMedisRawatJalan.FormBorderStyle = FormBorderStyle.None
                        frmAsesmenMedisRawatJalan.Dock = DockStyle.Fill

                        ' Tambahkan form anak ke panel
                        pnlAsemenMedis_Transaksi.Controls.Add(frmAsesmenMedisRawatJalan)

                        ' Tampilkan form anak

                        Dim oDataGrouper As New Admission.clsPendaftaran_Kunjungan
                        Dim sKDDOCTOR As String = String.Empty
                        Dim ds = oDataGrouper.GetDatabyKodeKunjungan(lblKDKUNJUNGAN.Text)
                        If ds IsNot Nothing Then
                            sKDDOCTOR = ds.KDDOCTOR
                        End If
                        frmAsesmenMedisRawatJalan.LoadMe(FORM_MODE.FORM_MODE_EDIT, sKDDOCTOR, dsDigital.KDIDENTITAS, dsDigital.KDKUNJUNGAN)
                        frmAsesmenMedisRawatJalan.Show()
                    Catch ex As Exception
                        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                Else
                    MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub picAsesmenAwalMedis_Delete_Click(sender As Object, e As EventArgs) Handles picAsesmenAwalMedis_Delete.Click
        If grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR") Is Nothing Then
            MsgBox("Silahkan Pilih Kode", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        MsgBox("Maap Belum Tersedia Fungsi Delete", MsgBoxStyle.Exclamation, Me.Text)

        If grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDUSER") <> sUserID Then
            MsgBox("Maap Tidak dapat Rubah Silahkan Hubungi User " & grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDUSER"), MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If MsgBox("Apakah Yakin Data Akan di Hapus", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        Dim frmPesanDelete As New frmPesanDelete
        frmPesanDelete.ShowDialog(Me)

        If sPesanHapus <> "XXXXXBATALXXXXX" Then
            If grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") = "ASESMEN AWAL MEDIS IGD" Then
                Dim oDelete As New Setting.clsDelete

                If oDelete.InsertData("DELETEASESMENAWALMEDIS", sUserID, grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") & " " & sPesanHapus & " " & Now.ToString("dd-MM-yyyy HH:mm:ss"), grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR")) = False Then
                    'fn_DeleteData = False
                End If

                Dim oDigital As New Transaksi.clsDigital_IGD_01
                oDigital.DeleteData(grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR"), sPesanHapus)
                XtraTabList_SelectedPageChanged()
            ElseIf grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") = "CATATAN MEDIS AWAL RAWAT INAP DAN DISCHARGE PLANNING" Then
                Dim oDelete As New Setting.clsDelete

                If oDelete.InsertData("DELETEASESMENAWALMEDIS", sUserID, grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") & " " & sPesanHapus & " " & Now.ToString("dd-MM-yyyy HH:mm:ss"), grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR")) = False Then
                    'fn_DeleteData = False
                End If

                Dim oDigital As New EMedrek.clsDigital_DischargePlanning
                oDigital.DeleteData(grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR"), sUserID)
                XtraTabList_SelectedPageChanged()
            ElseIf grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") = "KARTU ANASTESI A" Then
                Dim oDelete As New Setting.clsDelete
                Dim oDigital As New EMedrek.clsS_DIGITAL_OK_KARTU_ANESTESI_A

                If oDelete.InsertData("DELETEASESMENAWALMEDIS", sUserID, grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") & " " & sPesanHapus & " " & Now.ToString("dd-MM-yyyy HH:mm:ss"), grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR")) = False Then
                    'fn_DeleteData = False
                End If

                oDigital.DeleteData(grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR"), sUserID)
                XtraTabList_SelectedPageChanged()
            ElseIf grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") = "KARTU ANASTESI B" Then
                Dim oDelete As New Setting.clsDelete
                Dim oDigital As New EMedrek.clsS_DIGITAL_OK_KARTU_ANESTESI_B

                If oDelete.InsertData("DELETEASESMENAWALMEDIS", sUserID, grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") & " " & sPesanHapus & " " & Now.ToString("dd-MM-yyyy HH:mm:ss"), grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR")) = False Then
                    'fn_DeleteData = False
                End If

                oDigital.DeleteData(grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR"), sUserID)
                XtraTabList_SelectedPageChanged()
            Else
                MsgBox("Maap Belum Tersedia Fungsi Delete", MsgBoxStyle.Exclamation, Me.Text)
            End If
        End If
    End Sub
    Private Sub picAsesmenAwalMedis_Refresh_Click(sender As Object, e As EventArgs) Handles picAsesmenAwalMedis_Refresh.Click
        XtraTabList_SelectedPageChanged()
        lPanelAsesmenMedis_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lPanelAsesmenMedis_Panel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lAsesmenMedis_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If Not frmAsesmenAwalMedisIGD2 Is Nothing Then frmAsesmenAwalMedisIGD2.Dispose()
        frmAsesmenAwalMedisIGD2 = Nothing

        If Not frmDischargePlanning Is Nothing Then frmDischargePlanning.Dispose()
        frmDischargePlanning = Nothing

        If Not frmKartuAnastesi_A Is Nothing Then frmKartuAnastesi_A.Dispose()
        frmKartuAnastesi_A = Nothing

        If Not frmKartuAnastesi_B Is Nothing Then frmKartuAnastesi_B.Dispose()
        frmKartuAnastesi_B = Nothing

        If Not frmEMedrekRJ_31 Is Nothing Then frmEMedrekRJ_31.Dispose()
        frmEMedrekRJ_31 = Nothing

        If Not frmEMedrekRJ_36 Is Nothing Then frmEMedrekRJ_36.Dispose()
        frmEMedrekRJ_36 = Nothing

        If Not frmEMedrekRJ_34 Is Nothing Then frmEMedrekRJ_34.Dispose()
        frmEMedrekRJ_34 = Nothing

        If Not frmEMedrekRJ_29 Is Nothing Then frmEMedrekRJ_29.Dispose()
        frmEMedrekRJ_29 = Nothing

        If Not frmAsmedNeuro Is Nothing Then frmAsmedNeuro.Dispose()
        frmAsmedNeuro = Nothing

        If Not frmEMedrekRJ_35 Is Nothing Then frmEMedrekRJ_35.Dispose()
        frmEMedrekRJ_35 = Nothing

        If Not frmEMedrekRJ_30 Is Nothing Then frmEMedrekRJ_30.Dispose()
        frmEMedrekRJ_30 = Nothing

        If Not frmAsesmenAwalMedisNeonatusRawatInap Is Nothing Then frmAsesmenAwalMedisNeonatusRawatInap.Dispose()
        frmAsesmenAwalMedisNeonatusRawatInap = Nothing

        'If Not frmLembarUjiFungsiRehab Is Nothing Then frmLembarUjiFungsiRehab.Dispose()
        'frmLembarUjiFungsiRehab = Nothing

        'If Not frmCatatanKlinisRehab Is Nothing Then frmCatatanKlinisRehab.Dispose()
        'frmCatatanKlinisRehab = Nothing

        If Not frmResumeRawatJalanRehab Is Nothing Then frmResumeRawatJalanRehab.Dispose()
        frmResumeRawatJalanRehab = Nothing

        If Not frmEMedrekRJ_18 Is Nothing Then frmEMedrekRJ_18.Dispose()
        frmEMedrekRJ_18 = Nothing

        If Not frmEMedrekRJ_41 Is Nothing Then frmEMedrekRJ_41.Dispose()
        frmEMedrekRJ_41 = Nothing

        If Not frmEMedrek_AsmedAnak Is Nothing Then frmEMedrek_AsmedAnak.Dispose()
        frmEMedrek_AsmedAnak = Nothing

        If Not frmEMedrekAsmedKebidanan Is Nothing Then frmEMedrekAsmedKebidanan.Dispose()
        frmEMedrekAsmedKebidanan = Nothing

        If Not frmEMedrekRJ_33 Is Nothing Then frmEMedrekRJ_33.Dispose()
        frmEMedrekRJ_33 = Nothing

        If Not frmEMedrekRJ_32 Is Nothing Then frmEMedrekRJ_32.Dispose()
        frmEMedrekRJ_32 = Nothing

        If Not frmAsesmenAwalMedisTHT Is Nothing Then frmAsesmenAwalMedisTHT.Dispose()
        frmAsesmenAwalMedisTHT = Nothing

        If Not frmEMedrekAWAL_MEDIS_NEONATUS_RJ Is Nothing Then frmEMedrekAWAL_MEDIS_NEONATUS_RJ.Dispose()
        frmEMedrekAWAL_MEDIS_NEONATUS_RJ = Nothing

        If Not frmAsesmenMedisRawatJalan Is Nothing Then frmAsesmenMedisRawatJalan.Dispose()
        frmAsesmenMedisRawatJalan = Nothing
    End Sub
#End Region
#Region "Event"
    Private Sub grvAsesmenMedis_PDFFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvAsesmenMedis_PDF.FocusedRowChanged
        Try
            If grvAsesmenMedis_PDF.GetFocusedRowCellValue("KDFORMULIR") Is Nothing Then
                PdfViewerAsesmenMedis.CloseDocument()
                Exit Sub
            End If

            If grvAsesmenMedis_PDF.GetFocusedRowCellValue("DESCRIPTION") = "ASESMEN AWAL MEDIS IGD" Then
                xtraReportAsesmenAwalMedisIGD(grvAsesmenMedis_PDF.GetFocusedRowCellValue("KDFORMULIR"))
            ElseIf grvAsesmenMedis_PDF.GetFocusedRowCellValue("DESCRIPTION") = "CATATAN MEDIS AWAL RAWAT INAP DAN DISCHARGE PLANNING" Then
                xtraReportAsesmenAwalRawatInap(grvAsesmenMedis_PDF.GetFocusedRowCellValue("KDFORMULIR"))
            ElseIf grvAsesmenMedis_PDF.GetFocusedRowCellValue("DESCRIPTION") = "KARTU ANASTESI A" Then
                xtraReportKartuAnastesiA(grvAsesmenMedis_PDF.GetFocusedRowCellValue("KDFORMULIR"))
            ElseIf grvAsesmenMedis_PDF.GetFocusedRowCellValue("DESCRIPTION") = "KARTU ANASTESI B" Then
                xtraReportKartuAnastesiB(grvAsesmenMedis_PDF.GetFocusedRowCellValue("KDFORMULIR"))
            ElseIf grvAsesmenMedis_PDF.GetFocusedRowCellValue("DESCRIPTION") = "ASESMEN AWAL MEDIS PASIEN PENYAKIT DALAM" Then
                xtraReportAsesmenAwalPenyakitDalam(grvAsesmenMedis_PDF.GetFocusedRowCellValue("KDFORMULIR"))
            ElseIf grvAsesmenMedis_PDF.GetFocusedRowCellValue("DESCRIPTION") = "ASESMEN AWAL MEDIS PASIEN BEDAH" Then
                xtraReportAsesmenAwalPenyakitBedah(grvAsesmenMedis_PDF.GetFocusedRowCellValue("KDFORMULIR"))
            ElseIf grvAsesmenMedis_PDF.GetFocusedRowCellValue("DESCRIPTION") = "UJI FUNGSI, LAYANAN & PROSEDUR KEDOKTERAN FISIK & REHABILITASI" Then
                xtraReportLembarUjiFungsi(grvAsesmenMedis_PDF.GetFocusedRowCellValue("KDFORMULIR"))
            ElseIf grvAsesmenMedis_PDF.GetFocusedRowCellValue("DESCRIPTION") = "FORMULIR CATATAN KLINIS REHABILITASI MEDIK" Then
                xtraReportCatatanKlinisRehab(grvAsesmenMedis_PDF.GetFocusedRowCellValue("KDFORMULIR"))
            ElseIf grvAsesmenMedis_PDF.GetFocusedRowCellValue("DESCRIPTION") = "LAYANAN KEDOKTERAN FISIK DAN REHABILITASI" Then
                xtraReportResumeRehab(grvAsesmenMedis_PDF.GetFocusedRowCellValue("KDFORMULIR"))
            ElseIf grvAsesmenMedis_PDF.GetFocusedRowCellValue("DESCRIPTION") = "ASESMEN AWAL MEDIS PASIEN PARU" Then
                xtraReportAsesmenAwalParu(grvAsesmenMedis_PDF.GetFocusedRowCellValue("KDFORMULIR"))
            ElseIf grvAsesmenMedis_PDF.GetFocusedRowCellValue("DESCRIPTION") = "ASESMEN AWAL MEDIS PASIEN GIZI" Then
                xtraReportAsesmenAwalGizi(grvAsesmenMedis_PDF.GetFocusedRowCellValue("KDFORMULIR"))
            ElseIf grvAsesmenMedis_PDF.GetFocusedRowCellValue("DESCRIPTION") = "ASESMEN AWAL MEDIS PASIEN GIGI" Then
                xtraReportAsesmenAwalGigi(grvAsesmenMedis_PDF.GetFocusedRowCellValue("KDFORMULIR"))
            ElseIf grvAsesmenMedis_PDF.GetFocusedRowCellValue("DESCRIPTION") = "ASESMEN AWAL MEDIS PASIEN JANTUNG" Then
                xtraReportAsesmenAwalJantung(grvAsesmenMedis_PDF.GetFocusedRowCellValue("KDFORMULIR"))
            ElseIf grvAsesmenMedis_PDF.GetFocusedRowCellValue("DESCRIPTION") = "ASESMEN AWAL MEDIS PASIEN NEUROLOGI" Then
                xtraReportAsesmenAwalNeuro(grvAsesmenMedis_PDF.GetFocusedRowCellValue("KDFORMULIR"))
            ElseIf grvAsesmenMedis_PDF.GetFocusedRowCellValue("DESCRIPTION") = "ASESMEN AWAL MEDIS PASIEN PSIKIATRIK" Then
                xtraReportAsesmenAwalJiwa(grvAsesmenMedis_PDF.GetFocusedRowCellValue("KDFORMULIR"))
            ElseIf grvAsesmenMedis_PDF.GetFocusedRowCellValue("DESCRIPTION") = "ASESMEN AWAL MEDIS PASIEN TUMBUH KEMBANG" Then
                xtraReportAsesmenAwalTumbuhKembang(grvAsesmenMedis_PDF.GetFocusedRowCellValue("KDFORMULIR"))
            ElseIf grvAsesmenMedis_PDF.GetFocusedRowCellValue("DESCRIPTION") = "ASESMEN AWAL MEDIS PASIEN ANAK" Then
                xtraReportAsesmenAwalAnak(grvAsesmenMedis_PDF.GetFocusedRowCellValue("KDFORMULIR"))
            ElseIf grvAsesmenMedis_PDF.GetFocusedRowCellValue("DESCRIPTION") = "ASESMEN AWAL MEDIS OBSTETRI & GYNEKOLOGI" Then
                xtraReportAsesmenAwalKebidan(grvAsesmenMedis_PDF.GetFocusedRowCellValue("KDFORMULIR"))
            ElseIf grvAsesmenMedis_PDF.GetFocusedRowCellValue("DESCRIPTION") = "ASESMEN AWAL MEDIS PASIEN KULIT DAN KELAMIN" Then
                xtraReportAsesmenAwalKulit(grvAsesmenMedis_PDF.GetFocusedRowCellValue("KDFORMULIR"))
            ElseIf grvAsesmenMedis_PDF.GetFocusedRowCellValue("DESCRIPTION") = "ASESMEN AWAL MEDIS PASIEN MATA" Then
                xtraReportAsesmenAwalMata(grvAsesmenMedis_PDF.GetFocusedRowCellValue("KDFORMULIR"))
            ElseIf grvAsesmenMedis_PDF.GetFocusedRowCellValue("DESCRIPTION") = "ASESMEN AWAL MEDIS PASIEN THT" Then
                xtraReportAsesmenAwalTHT(grvAsesmenMedis_PDF.GetFocusedRowCellValue("KDFORMULIR"))
            ElseIf grvAsesmenMedis_PDF.GetFocusedRowCellValue("DESCRIPTION") = "ASESMEN AWAL MEDIS NEONATUS" Then
                xtraReportAsesmenAwalNeonatus(grvAsesmenMedis_PDF.GetFocusedRowCellValue("KDFORMULIR"))
            ElseIf grvAsesmenMedis_PDF.GetFocusedRowCellValue("DESCRIPTION") = "ASESMEN AWAL MEDIS RAWAT JALAN" Then

            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
#End Region
#Region "Asesmen Keperawatan"
#Region "Function"
    Private Sub QueryAsesmenPerawat_LoadHistory(ByVal NoRM As String)
        Try
            If sSplashScreen = False Then Exit Sub

            pnlAsesmenPerawat_Transaksi.Controls.Clear()

            grvAsesmenPerawat_Transaksi.Columns.Clear()
            grdAsesmenPerawat_Transaksi.DataSource = Nothing
            grvAsesmenPerawat_Transaksi.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Processing data.....")

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\RME\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "FORMULIR = 'ASESMEN AWAL KEPERAWATAN IGD' "
            SQL &= ",KODE = A.KDASESMEN "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_IGD_02_NEW A "
            SQL &= "WHERE A.KDCUSTOMER = '" & NoRM & "' "
            SQL &= "AND A.ISDELETE = 0 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "FORMULIR = 'ASESMEN KEBIDANAN PONEK' "
            SQL &= ",KODE = A.KDASESMEN "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_IGD_03 A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "
            SQL &= "AND A.ISDELETE = 0 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "FORMULIR = 'LAPORAN PERSALINAN' "
            SQL &= ",KODE = A.KDLAPROANPERSALINAN "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_LAPORANPERSALINAN A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "
            SQL &= "AND A.ISDELETE = 0 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "FORMULIR = 'SKRINNING DAN EDUKASI GIZI' "
            SQL &= ",KODE = A.KDASESMEN "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RI_20 A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "
            SQL &= "AND A.ISDELETE = 0 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "FORMULIR = 'ASESMEN AWAL KEPERAWATAN RAWAT INAP' "
            SQL &= ",KODE = A.KDKUNJUNGAN "
            SQL &= ",TANGGAL = A.DATECREATED "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RI_38 A "
            SQL &= "WHERE A.KDCUSTOMER = '" & NoRM & "' "
            'SQL &= "AND A.ISDELETE = 0 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "FORMULIR = 'ASESMEN AWAL KEPERAWATAN RAWAT JALAN' "
            SQL &= ",KODE = A.KDKUNJUNGAN "
            SQL &= ",TANGGAL = A.DATECREATED "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_ASKEP_RAWATJALAN A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "FORMULIR = 'PROTOKOL HEMODIALISA' "
            SQL &= ",KODE = A.KDKUNJUNGAN "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_PROTOKOLHD A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "FORMULIR = 'SURAT KETERANGAN HEMODIALISA' "
            SQL &= ",KODE = A.KDKUNJUNGAN "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_SUKET_HD A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "FORMULIR = 'LEMBAR TRIAGE' "
            SQL &= ",KODE = A.KDKUNJUNGAN "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_TRIAGE A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ASESMENKEPERAWATAN")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grdAsesmenPerawat_Transaksi.MainView = grvAsesmenPerawat_Transaksi
            grdAsesmenPerawat_Transaksi.DataSource = ds.Tables("ASESMENKEPERAWATAN")
            grdAsesmenPerawat_Transaksi.ForceInitialize()

            SplashScreenManager.CloseForm(False)

            QyeryQueryAsesmenPerawat_LoadFormatData()

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub QyeryQueryAsesmenPerawat_LoadFormatData()
        For iLoop As Integer = 0 To grvAsesmenPerawat_Transaksi.Columns.Count - 1
            If grvAsesmenPerawat_Transaksi.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvAsesmenPerawat_Transaksi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvAsesmenPerawat_Transaksi.Columns(iLoop).DisplayFormat.FormatString = "{0:n0}"
                grvAsesmenPerawat_Transaksi.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvAsesmenPerawat_Transaksi.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvAsesmenPerawat_Transaksi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvAsesmenPerawat_Transaksi.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm}"
            End If
        Next

        grvAsesmenPerawat_Transaksi.Columns("KODE").Visible = False
        grvAsesmenPerawat_Transaksi.BestFitColumns()
    End Sub
    Private Sub QueryAsemenKeperawatan_LoadHistoryPDF(ByVal NoRM As String)
        Try
            If sSplashScreen = False Then Exit Sub

            grvAsesmenNakesPDF.Columns.Clear()
            grdAsesmenNakesPDF.DataSource = Nothing
            grvAsesmenNakesPDF.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Processing data.....")

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\RME\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "DESCRIPTION = 'LAPORAN PERSALINAN' "
            SQL &= ",KDFORMULIR = A.KDLAPROANPERSALINAN "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_LAPORANPERSALINAN A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE A.KDCUSTOMER = '" & NoRM & "' "
            SQL &= "AND A.ISDELETE = 0 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "DESCRIPTION = 'ASESMEN AWAL KEPERAWATAN IGD' "
            SQL &= ",KDFORMULIR = A.KDASESMEN "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_IGD_02_NEW A "
            SQL &= "WHERE A.KDCUSTOMER = '" & NoRM & "' "
            SQL &= "AND A.ISDELETE = 0 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "DESCRIPTION = 'ASESMEN KEBIDANAN PONEK' "
            SQL &= ",KDFORMULIR = A.KDASESMEN "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_IGD_03 A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "
            SQL &= "AND A.ISDELETE = 0 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "DESCRIPTION = 'SKRINNING DAN EDUKASI GIZI' "
            SQL &= ",KDFORMULIR = A.KDASESMEN "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RI_20 A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "
            SQL &= "AND A.ISDELETE = 0 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "DESCRIPTION = 'ASESMEN AWAL KEPERAWATAN RAWAT INAP' "
            SQL &= ",KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",TANGGAL = A.DATECREATED "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RI_38 A "
            SQL &= "WHERE A.KDCUSTOMER = '" & NoRM & "' "
            'SQL &= "AND A.ISDELETE = 0 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "FORMULIR = 'ASESMEN AWAL KEPERAWATAN RAWAT JALAN' "
            SQL &= ",KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",TANGGAL = A.DATECREATED "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_ASKEP_RAWATJALAN A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "FORMULIR = 'PROTOKOL HEMODIALISA' "
            SQL &= ",KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_PROTOKOLHD A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "FORMULIR = 'SURAT KETERANGAN HEMODIALISA' "
            SQL &= ",KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_SUKET_HD A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "FORMULIR = 'LEMBAR PROGRAM TERAPI' "
            SQL &= ",KDFORMULIR = A.KDCPPT "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "R_CPPT A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "
            SQL &= "AND A.KDPROFESI = 'PROFESI_0000000005' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "FORMULIR = 'LEMBAR TRIAGE' "
            SQL &= ",KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_TRIAGE A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "FORMULIR = 'FORMULIR GERD Q' "
            SQL &= ",KDFORMULIR = A.KDGERDQ "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_GERDQ_H A "
            SQL &= "WHERE A.KDCUSTOMER = '" & NoRM & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "R_IDENTITAS_GROUPER_CPPT_ASEMENAKES")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grdAsesmenNakesPDF.MainView = grvAsesmenNakesPDF
            grdAsesmenNakesPDF.DataSource = ds.Tables("R_IDENTITAS_GROUPER_CPPT_ASEMENAKES")
            grdAsesmenNakesPDF.ForceInitialize()

            SplashScreenManager.CloseForm(False)

            QyeryAsesmenKeperawatan_LoadFormatDataPDF()

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub QyeryAsesmenKeperawatan_LoadFormatDataPDF()
        For iLoop As Integer = 0 To grvAsesmenNakesPDF.Columns.Count - 1
            If grvAsesmenNakesPDF.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvAsesmenNakesPDF.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvAsesmenNakesPDF.Columns(iLoop).DisplayFormat.FormatString = "{0:n0}"
                grvAsesmenNakesPDF.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvAsesmenNakesPDF.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvAsesmenNakesPDF.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvAsesmenNakesPDF.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm}"
            End If
        Next

        grvAsesmenNakesPDF.Columns("KDFORMULIR").Visible = False
        grvAsesmenNakesPDF.BestFitColumns()
    End Sub
    Private Sub xtraReportAsesmenAwalPerawatIGD(ByVal Kode As String)
        Try
            PdfViewerAsesmenNakes.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Awal Nakes.....")

            Dim dataList As New List(Of Byte())
            Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim oDigital As New Digital.clsDigital_IGD_02

            Dim ds = oDigital.GetData(Kode)

            If ds IsNot Nothing Then
                Dim rpt As New xtraReportAsesmenKeperawatanGD_New

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.BindingSource.DataSource = ds
                rpt.ExportToPdf(Alamat)

                If FileIO.FileSystem.FileExists(Alamat) Then
                    PdfViewerAsesmenNakes.LoadDocument(Alamat)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Asesmen Awal Nakes" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub xtraReportLaporanPersalinan(ByVal Kode As String)
        Try
            PdfViewerAsesmenNakes.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Awal Nakes.....")

            Dim dataList As New List(Of Byte())
            Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim oDigital As New EMedrek.clsLaporanPersalinan

            Dim ds = oDigital.GetData(Kode)

            If ds IsNot Nothing Then
                Dim rpt As New xtraLaporanPersalian

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.bindingSource.DataSource = ds
                rpt.ExportToPdf(Alamat)

                If FileIO.FileSystem.FileExists(Alamat) Then
                    PdfViewerAsesmenNakes.LoadDocument(Alamat)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Asesmen Awal Nakes" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub xtraReportLaporanAsesmenAwalKeperawatanRawatInap(ByVal Kode As String)
        Try
            PdfViewerAsesmenNakes.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Awal Nakes.....")

            Dim dataList As New List(Of Byte())
            Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim oDigital As New Digital.clsDigital_RI_38

            Dim ds = oDigital.GetData(Kode)

            If ds IsNot Nothing Then
                sJudulAsesmen = "ASESMEN AWAL KEPERAWATAN PASIEN " & ds.KDUSER_SIGNATURE
                Dim rpt As New xtraReportEMedrekRI_38

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.BindingSource1.DataSource = ds
                rpt.ExportToPdf(Alamat)

                If FileIO.FileSystem.FileExists(Alamat) Then
                    PdfViewerAsesmenNakes.LoadDocument(Alamat)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Asesmen Awal Nakes" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub xtraReportLaporanAsesmenAwalKeperawatanRawatJalan(ByVal Kode As String)
        Try
            PdfViewerAsesmenNakes.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Awal Nakes.....")

            Dim dataList As New List(Of Byte())
            Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim oDigital As New EMedrek.clsS_DIGITAL_ASKEP_RAWATJALAN

            Dim ds = oDigital.GetData(Kode)

            If ds IsNot Nothing Then
                Dim rpt As New xtraReportAskepRawatJalan

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.BindingSource1.DataSource = ds
                rpt.ExportToPdf(Alamat)

                If FileIO.FileSystem.FileExists(Alamat) Then
                    PdfViewerAsesmenNakes.LoadDocument(Alamat)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Asesmen Awal Nakes" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub xtraReportLaporanAsesmenAwalSkrining(ByVal Kode As String)
        Try
            PdfViewerAsesmenNakes.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Awal Nakes.....")

            Dim dataList As New List(Of Byte())
            Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim oDigital As New Digital.clsDigital_RI_20

            Dim ds = oDigital.GetData(Kode)

            If ds IsNot Nothing Then
                Dim rpt As New xtraReportEMedrekRI_20

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.BindingSource.DataSource = ds
                rpt.ExportToPdf(Alamat)

                If FileIO.FileSystem.FileExists(Alamat) Then
                    PdfViewerAsesmenNakes.LoadDocument(Alamat)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Asesmen Awal Nakes" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub xtraReportLaporanAsesmenAwalPonek(ByVal Kode As String)
        Try
            PdfViewerAsesmenNakes.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Awal Nakes.....")

            Dim dataList As New List(Of Byte())
            Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim oDigital As New Digital.clsDigital_IGD_03

            Dim ds = oDigital.GetData(Kode)

            If ds IsNot Nothing Then
                Dim rpt As New xtraReportFormulirIGD3

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.BindingSource.DataSource = ds
                rpt.ExportToPdf(Alamat)

                If FileIO.FileSystem.FileExists(Alamat) Then
                    PdfViewerAsesmenNakes.LoadDocument(Alamat)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Asesmen Awal Nakes" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub xtraReportLaporanAsesmenAwalProtokolHD(ByVal Kode As String)
        Try
            PdfViewerAsesmenNakes.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Awal Nakes.....")

            Dim dataList As New List(Of Byte())
            Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim oDigital As New EMedrek.clsDigital_ProtokolHD

            Dim ds = oDigital.GetData(Kode)

            If ds IsNot Nothing Then
                Dim rpt As New xtraReportProtokolHD

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.BindingSource1.DataSource = ds
                rpt.ExportToPdf(Alamat)

                If FileIO.FileSystem.FileExists(Alamat) Then
                    PdfViewerAsesmenNakes.LoadDocument(Alamat)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Asesmen Awal Nakes" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub xtraReportLaporanAsesmenAwalSuKetHD(ByVal Kode As String)
        Try
            PdfViewerAsesmenNakes.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Awal Nakes.....")

            Dim dataList As New List(Of Byte())
            Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim oDigital As New EMedrek.clsDigital_SuketHD

            Dim ds = oDigital.GetData(Kode)

            If ds IsNot Nothing Then
                Dim rpt As New xtraReportSuketHD

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.BindingSource1.DataSource = ds
                rpt.ExportToPdf(Alamat)

                If FileIO.FileSystem.FileExists(Alamat) Then
                    PdfViewerAsesmenNakes.LoadDocument(Alamat)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Asesmen Awal Nakes" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub xtraReportLaporanLembarProgramTerapi(ByVal Kode As String)
        Try
            PdfViewerAsesmenNakes.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Awal Nakes.....")

            Dim dataList As New List(Of Byte())
            Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim oDigital As New Grouper.clsR_CPPT

            Dim ds = oDigital.GetData(Kode)

            If ds IsNot Nothing Then
                Dim rpt As New xtraLembarUjiFungsiRehabResumePerawat

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.bindingSource.DataSource = ds
                rpt.ExportToPdf(Alamat)

                If FileIO.FileSystem.FileExists(Alamat) Then
                    PdfViewerAsesmenNakes.LoadDocument(Alamat)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Asesmen Awal Nakes" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub xtraReportLaporanFormulirGerd(ByVal Kode As String)
        Try
            PdfViewerAsesmenNakes.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Formulir Gerd Q.....")

            Dim dataList As New List(Of Byte())
            Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim oDigital As New Digital.clsGerdQ

            Dim ds = oDigital.GetData(Kode)

            If ds IsNot Nothing Then
                Dim rpt As New xtraGerdQ

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.bindingSource.DataSource = ds
                rpt.ExportToPdf(Alamat)

                If FileIO.FileSystem.FileExists(Alamat) Then
                    PdfViewerAsesmenNakes.LoadDocument(Alamat)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Asesmen Awal Nakes" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
#Region "Command Button"
    Private Sub btnAsemenKeperawatan_Back_Click(sender As Object, e As EventArgs) Handles btnAsemenKeperawatan_Back.Click
        pnlAsesmenKeperawatan_pic.Dock = DockStyle.Top

        XtraTabList_SelectedPageChanged()
        lAsesmenPerawat_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lAsesmenPerawat_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lAsesmenKeperawatan_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        ClearFormAsesmenNakes()
    End Sub
    Private Sub btnAsesmenPerawat_BukaTutupList_Click(sender As Object, e As EventArgs) Handles btnAsesmenPerawat_BukaTutupList.Click
        If btnAsesmenPerawat_BukaTutupList.Text = "Tutup" Then
            btnAsesmenPerawat_BukaTutupList.Text = "Buka"
            grdAsesmenNakesPDF.Visible = False
        Else
            btnAsesmenPerawat_BukaTutupList.Text = "Tutup"
            grdAsesmenNakesPDF.Visible = True
        End If
    End Sub
    Private Sub picAsesmenPerawat_Add_Click(sender As Object, e As EventArgs) Handles picAsesmenPerawat_Add.Click
        If lblKDKUNJUNGAN.Text = "" Then
            MsgBox("Silahkan Pilih Dulu Pasien", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        Dim oDataGrouper As New Admission.clsPendaftaran_Kunjungan

        Dim ds = oDataGrouper.GetData(lblKDKUNJUNGAN.Text)

        If ds IsNot Nothing Then
            pnlAsesmenKeperawatan_pic.Dock = DockStyle.None

            lAsesmenPerawat_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lAsesmenPerawat_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lAsesmenKeperawatan_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            If cboAsesmenPerawat.Text = "ASESMEN AWAL KEPERAWATAN IGD" Then
                Try
                    ' Bersihkan konten panel
                    pnlAsesmenPerawat_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmEMedrekIDG_02_New.TopLevel = False
                    frmEMedrekIDG_02_New.FormBorderStyle = FormBorderStyle.None
                    frmEMedrekIDG_02_New.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsesmenPerawat_Transaksi.Controls.Add(frmEMedrekIDG_02_New)

                    ' Tampilkan form anak
                    frmEMedrekIDG_02_New.LoadMe(FORM_MODE.FORM_MODE_ADD, ds.KDPENDAFTARAN, ds.S_PENDAFTARAN_H.KDCUSTOMER)
                    frmEMedrekIDG_02_New.Show()
                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf cboAsesmenPerawat.Text = "ASESMEN KEBIDANAN PONEK" Then
                Try
                    Dim dsIdentitas = oDataGrouper.GetDatabyKodeKunjungan(ds.KDKUNJUNGAN)

                    If dsIdentitas IsNot Nothing Then
                        ' Bersihkan konten panel
                        pnlAsesmenPerawat_Transaksi.Controls.Clear()

                        ' Set properti form anak
                        frmKebidananVonek.TopLevel = False
                        frmKebidananVonek.FormBorderStyle = FormBorderStyle.None
                        frmKebidananVonek.Dock = DockStyle.Fill

                        ' Tambahkan form anak ke panel
                        pnlAsesmenPerawat_Transaksi.Controls.Add(frmKebidananVonek)

                        ' Tampilkan form anak
                        frmKebidananVonek.LoadMe(FORM_MODE.FORM_MODE_ADD, dsIdentitas.KDIDENTITAS)
                        frmKebidananVonek.Show()
                    End If

                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf cboAsesmenPerawat.Text = "LAPORAN PERSALINAN" Then
                Try
                    Dim dsIdentitas = oDataGrouper.GetDatabyKodeKunjungan(ds.KDKUNJUNGAN)

                    If dsIdentitas IsNot Nothing Then
                        ' Bersihkan konten panel
                        pnlAsesmenPerawat_Transaksi.Controls.Clear()

                        ' Set properti form anak
                        frmLaporanPersalinan.TopLevel = False
                        frmLaporanPersalinan.FormBorderStyle = FormBorderStyle.None
                        frmLaporanPersalinan.Dock = DockStyle.Fill

                        ' Tambahkan form anak ke panel
                        pnlAsesmenPerawat_Transaksi.Controls.Add(frmLaporanPersalinan)

                        ' Tampilkan form anak
                        frmLaporanPersalinan.LoadMe(FORM_MODE.FORM_MODE_ADD, dsIdentitas.KDIDENTITAS)
                        frmLaporanPersalinan.Show()
                    End If

                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf cboAsesmenPerawat.Text = "SKRINNING DAN EDUKASI GIZI" Then
                Try
                    Dim dsIdentitas = oDataGrouper.GetDatabyKodeKunjungan(ds.KDKUNJUNGAN)

                    If dsIdentitas IsNot Nothing Then
                        Dim oDigital As New Digital.clsDigital_RI_20

                        Dim dsDigital = oDigital.GetData(ds.KDPENDAFTARAN)

                        If dsDigital IsNot Nothing Then
                            MsgBox("Formulir Sudah dibuatkan, Silahkan lakukan Edit untuk memperbaiki", MsgBoxStyle.Exclamation, Me.Text)
                            Exit Sub
                        End If

                        ' Bersihkan konten panel
                        pnlAsesmenPerawat_Transaksi.Controls.Clear()

                        ' Set properti form anak
                        frmEMedrekRI_20.TopLevel = False
                        frmEMedrekRI_20.FormBorderStyle = FormBorderStyle.None
                        frmEMedrekRI_20.Dock = DockStyle.Fill

                        ' Tambahkan form anak ke panel
                        pnlAsesmenPerawat_Transaksi.Controls.Add(frmEMedrekRI_20)

                        ' Tampilkan form anak
                        frmEMedrekRI_20.LoadMe(FORM_MODE.FORM_MODE_ADD, dsIdentitas.KDIDENTITAS, "", "")
                        frmEMedrekRI_20.Show()
                    End If

                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf cboAsesmenPerawat.Text = "ASESMEN AWAL KEPERAWATAN RAWAT INAP" Then
                Try
                    If ds IsNot Nothing Then
                        Dim oDigital As New Digital.clsDigital_RI_38

                        Dim dsDigital = oDigital.GetData(ds.KDPENDAFTARAN)

                        If dsDigital IsNot Nothing Then
                            MsgBox("Formulir Sudah dibuatkan, Silahkan lakukan Edit untuk memperbaiki", MsgBoxStyle.Exclamation, Me.Text)
                            Exit Sub
                        End If

                        ' Bersihkan konten panel
                        pnlAsesmenPerawat_Transaksi.Controls.Clear()

                        ' Set properti form anak
                        frmEMedrekRI_38.TopLevel = False
                        frmEMedrekRI_38.FormBorderStyle = FormBorderStyle.None
                        frmEMedrekRI_38.Dock = DockStyle.Fill

                        ' Tambahkan form anak ke panel
                        pnlAsesmenPerawat_Transaksi.Controls.Add(frmEMedrekRI_38)

                        ' Tampilkan form anak
                        frmEMedrekRI_38.LoadMe(FORM_MODE.FORM_MODE_ADD, "", 0, ds.S_PENDAFTARAN_H.KDCUSTOMER, ds.KDPENDAFTARAN)
                        frmEMedrekRI_38.Show()
                    End If

                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf cboAsesmenPerawat.Text = "ASESMEN AWAL KEPERAWATAN RAWAT JALAN" Then
                Try
                    Dim dsIdentitas = oDataGrouper.GetDatabyKodeKunjungan(lblKDKUNJUNGAN.Text)
                    If dsIdentitas IsNot Nothing Then
                        Dim oDigital As New Digital.clsDigital_RI_38

                        Dim dsDigital = oDigital.GetData(dsIdentitas.KDKUNJUNGAN)

                        If dsDigital IsNot Nothing Then
                            MsgBox("Formulir Sudah dibuatkan, Silahkan lakukan Edit untuk memperbaiki", MsgBoxStyle.Exclamation, Me.Text)
                            Exit Sub
                        End If

                        ' Bersihkan konten panel
                        pnlAsesmenPerawat_Transaksi.Controls.Clear()

                        ' Set properti form anak
                        frmAskepRawatJalan.TopLevel = False
                        frmAskepRawatJalan.FormBorderStyle = FormBorderStyle.None
                        frmAskepRawatJalan.Dock = DockStyle.Fill

                        ' Tambahkan form anak ke panel
                        pnlAsesmenPerawat_Transaksi.Controls.Add(frmAskepRawatJalan)

                        ' Tampilkan form anak
                        frmAskepRawatJalan.LoadMe(FORM_MODE.FORM_MODE_ADD, dsIdentitas.KDDOCTOR, dsIdentitas.KDIDENTITAS, dsIdentitas.KDCUSTOMER, dsIdentitas.KDKUNJUNGAN)
                        frmAskepRawatJalan.Show()
                    End If
                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf cboAsesmenPerawat.Text = "PROTOKOL HEMODIALISA" Then
                Try
                    Dim dsIdentitas = oDataGrouper.GetDatabyKodeKunjungan(lblKDKUNJUNGAN.Text)
                    If dsIdentitas IsNot Nothing Then
                        Dim oDigital As New EMedrek.clsDigital_ProtokolHD

                        Dim dsDigital = oDigital.GetData(dsIdentitas.KDKUNJUNGAN)

                        If dsDigital IsNot Nothing Then
                            MsgBox("Formulir Sudah dibuatkan, Silahkan lakukan Edit untuk memperbaiki", MsgBoxStyle.Exclamation, Me.Text)
                            Exit Sub
                        End If

                        ' Bersihkan konten panel
                        pnlAsesmenPerawat_Transaksi.Controls.Clear()

                        ' Set properti form anak
                        frmProtokolHD.TopLevel = False
                        frmProtokolHD.FormBorderStyle = FormBorderStyle.None
                        frmProtokolHD.Dock = DockStyle.Fill

                        ' Tambahkan form anak ke panel
                        pnlAsesmenPerawat_Transaksi.Controls.Add(frmProtokolHD)

                        ' Tampilkan form anak
                        frmProtokolHD.LoadMe(FORM_MODE.FORM_MODE_ADD, dsIdentitas.KDDOCTOR, dsIdentitas.KDIDENTITAS, dsIdentitas.KDKUNJUNGAN)
                        frmProtokolHD.Show()
                    End If
                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf cboAsesmenPerawat.Text = "SURAT KETERANGAN HEMODIALISA" Then
                Try
                    Dim dsIdentitas = oDataGrouper.GetDatabyKodeKunjungan(lblKDKUNJUNGAN.Text)
                    If dsIdentitas IsNot Nothing Then
                        Dim oDigital As New EMedrek.clsDigital_SuketHD

                        Dim dsDigital = oDigital.GetData(dsIdentitas.KDKUNJUNGAN)

                        If dsDigital IsNot Nothing Then
                            MsgBox("Formulir Sudah dibuatkan, Silahkan lakukan Edit untuk memperbaiki", MsgBoxStyle.Exclamation, Me.Text)
                            Exit Sub
                        End If

                        ' Bersihkan konten panel
                        pnlAsesmenPerawat_Transaksi.Controls.Clear()

                        ' Set properti form anak
                        frmSuKet_HD.TopLevel = False
                        frmSuKet_HD.FormBorderStyle = FormBorderStyle.None
                        frmSuKet_HD.Dock = DockStyle.Fill

                        ' Tambahkan form anak ke panel
                        pnlAsesmenPerawat_Transaksi.Controls.Add(frmSuKet_HD)

                        ' Tampilkan form anak
                        frmSuKet_HD.LoadMe(FORM_MODE.FORM_MODE_ADD, dsIdentitas.KDDOCTOR, oDataGrouper.GetData(dsIdentitas.KDKUNJUNGAN).S_PENDAFTARAN_H.KDCUSTOMER, dsIdentitas.KDIDENTITAS, dsIdentitas.KDKUNJUNGAN)
                        frmSuKet_HD.Show()
                    End If
                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf cboAsesmenPerawat.Text = "LEMBAR TRIAGE" Then
                Try
                    Dim dsIdentitas = oDataGrouper.GetDatabyKodeKunjungan(lblKDKUNJUNGAN.Text)
                    If dsIdentitas IsNot Nothing Then
                        Dim oDigital As New EMedrek.clsDigital_SuketHD

                        Dim dsDigital = oDigital.GetData(dsIdentitas.KDKUNJUNGAN)

                        If dsDigital IsNot Nothing Then
                            MsgBox("Formulir Sudah dibuatkan, Silahkan lakukan Edit untuk memperbaiki", MsgBoxStyle.Exclamation, Me.Text)
                            Exit Sub
                        End If

                        ' Bersihkan konten panel
                        pnlAsesmenPerawat_Transaksi.Controls.Clear()

                        ' Set properti form anak
                        frmEMedrekTriage.TopLevel = False
                        frmEMedrekTriage.FormBorderStyle = FormBorderStyle.None
                        frmEMedrekTriage.Dock = DockStyle.Fill

                        ' Tambahkan form anak ke panel
                        pnlAsesmenPerawat_Transaksi.Controls.Add(frmEMedrekTriage)

                        ' Tampilkan form anak
                        frmEMedrekTriage.LoadMe(FORM_MODE.FORM_MODE_ADD, dsIdentitas.KDDOCTOR, dsIdentitas.KDIDENTITAS, dsIdentitas.KDKUNJUNGAN, False)
                        frmEMedrekTriage.Show()
                    End If
                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                MsgBox("Dokumen Belum Ada", MsgBoxStyle.Exclamation, Me.Text)

                pnlAsesmenKeperawatan_pic.Dock = DockStyle.Top

                XtraTabList_SelectedPageChanged()
                lAsesmenPerawat_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lAsesmenPerawat_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lAsesmenKeperawatan_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                ClearFormAsesmenNakes()
            End If
        Else
            MsgBox("Data Kunjungan Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub picAsesmenPerawat_Update_Click(sender As Object, e As EventArgs) Handles picAsesmenPerawat_Update.Click
        If grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("KODE") Is Nothing Then
            MsgBox("Silahkan Pilih Kode", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("KDUSER") <> sUserID Then
            MsgBox("Maap Tidak dapat Rubah Silahkan Hubungi User " & grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("KDUSER"), MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        pnlAsesmenKeperawatan_pic.Dock = DockStyle.None

        lAsesmenPerawat_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lAsesmenPerawat_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lAsesmenKeperawatan_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

        If grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("FORMULIR") = "ASESMEN AWAL KEPERAWATAN IGD" Then
            Dim oDigital As New Digital.clsDigital_IGD_02

            Dim dsDigital = oDigital.GetData(grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("KODE"))

            If dsDigital IsNot Nothing Then
                Try
                    ' Bersihkan konten panel
                    pnlAsesmenPerawat_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmEMedrekIDG_02_New.TopLevel = False
                    frmEMedrekIDG_02_New.FormBorderStyle = FormBorderStyle.None
                    frmEMedrekIDG_02_New.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsesmenPerawat_Transaksi.Controls.Add(frmEMedrekIDG_02_New)

                    ' Tampilkan form anak
                    frmEMedrekIDG_02_New.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.KDPENDAFTARAN, dsDigital.KDCUSTOMER, dsDigital.KDASESMEN)
                    frmEMedrekIDG_02_New.Show()
                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
            End If
        ElseIf grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("FORMULIR") = "ASESMEN KEBIDANAN PONEK" Then
            Dim oDigital As New Digital.clsDigital_IGD_03

            Dim dsDigital = oDigital.GetData(grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("KODE"))

            If dsDigital IsNot Nothing Then
                Try
                    ' Bersihkan konten panel
                    pnlAsesmenPerawat_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmKebidananVonek.TopLevel = False
                    frmKebidananVonek.FormBorderStyle = FormBorderStyle.None
                    frmKebidananVonek.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsesmenPerawat_Transaksi.Controls.Add(frmKebidananVonek)

                    ' Tampilkan form anak
                    frmKebidananVonek.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.KDIDENTITAS, dsDigital.KDASESMEN)
                    frmKebidananVonek.Show()
                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
            End If
        ElseIf grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("FORMULIR") = "LAPORAN PERSALINAN" Then
            Dim oDigital As New EMedrek.clsLaporanPersalinan

            Dim dsDigital = oDigital.GetData(grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("KODE"))

            If dsDigital IsNot Nothing Then
                Try
                    ' Bersihkan konten panel
                    pnlAsesmenPerawat_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmLaporanPersalinan.TopLevel = False
                    frmLaporanPersalinan.FormBorderStyle = FormBorderStyle.None
                    frmLaporanPersalinan.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsesmenPerawat_Transaksi.Controls.Add(frmLaporanPersalinan)

                    ' Tampilkan form anak
                    frmLaporanPersalinan.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.KDIDENTITAS, dsDigital.KDLAPROANPERSALINAN)
                    frmLaporanPersalinan.Show()
                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
            End If
        ElseIf grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("FORMULIR") = "SKRINNING DAN EDUKASI GIZI" Then
            Dim oDigital As New Digital.clsDigital_RI_20

            Dim dsDigital = oDigital.GetData(grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("KODE"))

            If dsDigital IsNot Nothing Then
                Try
                    ' Bersihkan konten panel
                    pnlAsesmenPerawat_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmEMedrekRI_20.TopLevel = False
                    frmEMedrekRI_20.FormBorderStyle = FormBorderStyle.None
                    frmEMedrekRI_20.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsesmenPerawat_Transaksi.Controls.Add(frmEMedrekRI_20)

                    ' Tampilkan form anak
                    frmEMedrekRI_20.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.KDIDENTITAS, dsDigital.KDASESMEN, dsDigital.RUANGRAWAT2)
                    frmEMedrekRI_20.Show()
                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
            End If
        ElseIf grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("FORMULIR") = "ASESMEN AWAL KEPERAWATAN RAWAT INAP" Then
            Dim oDigital As New Digital.clsDigital_RI_38

            Dim dsDigital = oDigital.GetData(grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("KODE"))

            If dsDigital IsNot Nothing Then
                Try
                    ' Bersihkan konten panel
                    pnlAsesmenPerawat_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmEMedrekRI_38.TopLevel = False
                    frmEMedrekRI_38.FormBorderStyle = FormBorderStyle.None
                    frmEMedrekRI_38.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsesmenPerawat_Transaksi.Controls.Add(frmEMedrekRI_38)

                    ' Tampilkan form anak
                    frmEMedrekRI_38.LoadMe(FORM_MODE.FORM_MODE_EDIT, "", 0, dsDigital.KDCUSTOMER, dsDigital.KDKUNJUNGAN)
                    frmEMedrekRI_38.Show()
                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
            End If
        ElseIf grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("FORMULIR") = "ASESMEN AWAL KEPERAWATAN RAWAT JALAN" Then
            Dim oDigital As New EMedrek.clsS_DIGITAL_ASKEP_RAWATJALAN

            Dim dsDigital = oDigital.GetData(grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("KODE"))

            If dsDigital IsNot Nothing Then
                Try
                    ' Bersihkan konten panel
                    pnlAsesmenPerawat_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmAskepRawatJalan.TopLevel = False
                    frmAskepRawatJalan.FormBorderStyle = FormBorderStyle.None
                    frmAskepRawatJalan.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsesmenPerawat_Transaksi.Controls.Add(frmAskepRawatJalan)

                    ' Tampilkan form anak
                    frmAskepRawatJalan.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.A_IDENTITASPASIEN_LIST.KDDOCTOR, dsDigital.KDIDENTITAS, dsDigital.A_IDENTITASPASIEN_LIST.KDCUSTOMER, dsDigital.KDKUNJUNGAN)
                    frmAskepRawatJalan.Show()
                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
            End If
        ElseIf grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("FORMULIR") = "PROTOKOL HEMODIALISA" Then
            Dim oDigital As New EMedrek.clsDigital_ProtokolHD

            Dim dsDigital = oDigital.GetData(grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("KODE"))

            If dsDigital IsNot Nothing Then
                Try
                    ' Bersihkan konten panel
                    pnlAsesmenPerawat_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmProtokolHD.TopLevel = False
                    frmProtokolHD.FormBorderStyle = FormBorderStyle.None
                    frmProtokolHD.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsesmenPerawat_Transaksi.Controls.Add(frmProtokolHD)

                    ' Tampilkan form anak
                    frmProtokolHD.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.A_IDENTITASPASIEN_LIST.KDDOCTOR, dsDigital.KDIDENTITAS, dsDigital.KDKUNJUNGAN)
                    frmProtokolHD.Show()
                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
            End If
        ElseIf grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("FORMULIR") = "SURAT KETERANGAN HEMODIALISA" Then
            Dim oDigital As New EMedrek.clsDigital_SuketHD
            Dim oDataGrouper As New Admission.clsPendaftaran_Kunjungan

            Dim dsDigital = oDigital.GetData(grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("KODE"))

            If dsDigital IsNot Nothing Then
                Try
                    ' Bersihkan konten panel
                    pnlAsesmenPerawat_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmSuKet_HD.TopLevel = False
                    frmSuKet_HD.FormBorderStyle = FormBorderStyle.None
                    frmSuKet_HD.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsesmenPerawat_Transaksi.Controls.Add(frmSuKet_HD)

                    ' Tampilkan form anak
                    frmSuKet_HD.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.A_IDENTITASPASIEN_LIST.KDDOCTOR, oDataGrouper.GetData(dsDigital.KDKUNJUNGAN).S_PENDAFTARAN_H.KDCUSTOMER, dsDigital.KDIDENTITAS, dsDigital.KDKUNJUNGAN)
                    frmSuKet_HD.Show()
                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
            End If
        ElseIf grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("FORMULIR") = "LEMBAR TRIAGE" Then
            Dim oDigital As New Digital.clsDigital_Triage

            Dim dsDigital = oDigital.GetData(grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("KODE"))

            If dsDigital IsNot Nothing Then
                Try
                    ' Bersihkan konten panel
                    pnlAsesmenPerawat_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmEMedrekTriage.TopLevel = False
                    frmEMedrekTriage.FormBorderStyle = FormBorderStyle.None
                    frmEMedrekTriage.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsesmenPerawat_Transaksi.Controls.Add(frmEMedrekTriage)

                    ' Tampilkan form anak
                    frmEMedrekTriage.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.A_IDENTITASPASIEN_LIST.KDDOCTOR, dsDigital.KDIDENTITAS, dsDigital.KDKUNJUNGAN, False)
                    frmEMedrekTriage.Show()
                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
            End If
        End If
    End Sub
    Private Sub picAsesmenPerawat_Delete_Click(sender As Object, e As EventArgs) Handles picAsesmenPerawat_Delete.Click
        If grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("KODE") Is Nothing Then
            MsgBox("Silahkan Pilih Kode", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("KDUSER") <> sUserID Then
            MsgBox("Maap Tidak dapat Rubah Silahkan Hubungi User " & grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("KDUSER"), MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If MsgBox("Apakah Yakin Data Akan di Hapus", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        If grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("FORMULIR") = "ASESMEN AWAL KEPERAWATAN IGD" Then
            Dim frmPesanDelete As New frmPesanDelete
            frmPesanDelete.ShowDialog(Me)

            If sPesanHapus <> "XXXXXBATALXXXXX" Then
                Dim oDigital As New Digital.clsDigital_IGD_02
                oDigital.UpdateDelete(grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("KODE"), sUserID)
                XtraTabList_SelectedPageChanged()
            End If
        ElseIf grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("FORMULIR") = "ASESMEN KEBIDANAN VONEK" Then
            Dim frmPesanDelete As New frmPesanDelete
            frmPesanDelete.ShowDialog(Me)

            If sPesanHapus <> "XXXXXBATALXXXXX" Then
                Dim oDigital As New Digital.clsDigital_IGD_03
                oDigital.UpdateDelete(grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("KODE"), sUserID)
                XtraTabList_SelectedPageChanged()
            End If
        ElseIf grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("FORMULIR") = "LAPORAN PERSALINAN" Then
            Dim frmPesanDelete As New frmPesanDelete
            frmPesanDelete.ShowDialog(Me)

            If sPesanHapus <> "XXXXXBATALXXXXX" Then
                Dim oDigital As New EMedrek.clsLaporanPersalinan
                oDigital.UpdateDelete(grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("KODE"), sUserID)
                XtraTabList_SelectedPageChanged()
            End If
        ElseIf grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("FORMULIR") = "SKRINNING DAN EDUKASI GIZI" Then
            Dim frmPesanDelete As New frmPesanDelete
            frmPesanDelete.ShowDialog(Me)

            If sPesanHapus <> "XXXXXBATALXXXXX" Then
                Dim oDigital As New Digital.clsDigital_RI_20
                oDigital.DeleteData(grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("KODE"))
                XtraTabList_SelectedPageChanged()
            End If
        End If
    End Sub
    Private Sub picAsesmenPerawat_Refresh_Click(sender As Object, e As EventArgs) Handles picAsesmenPerawat_Refresh.Click
        XtraTabList_SelectedPageChanged()
        lAsesmenPerawat_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lAsesmenPerawat_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lAsesmenKeperawatan_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        ClearFormAsesmenNakes()
    End Sub
    Private Sub ClearFormAsesmenNakes()
        If Not frmEMedrekIDG_02_New Is Nothing Then frmEMedrekIDG_02_New.Dispose()
        frmEMedrekIDG_02_New = Nothing

        If Not frmLaporanPersalinan Is Nothing Then frmLaporanPersalinan.Dispose()
        frmLaporanPersalinan = Nothing

        If Not frmKebidananVonek Is Nothing Then frmKebidananVonek.Dispose()
        frmKebidananVonek = Nothing

        If Not frmEMedrekRI_20 Is Nothing Then frmEMedrekRI_20.Dispose()
        frmEMedrekRI_20 = Nothing

        If Not frmEMedrekRI_38 Is Nothing Then frmEMedrekRI_38.Dispose()
        frmEMedrekRI_38 = Nothing

        If Not frmAskepRawatJalan Is Nothing Then frmAskepRawatJalan.Dispose()
        frmAskepRawatJalan = Nothing

        If Not frmProtokolHD Is Nothing Then frmProtokolHD.Dispose()
        frmProtokolHD = Nothing

        If Not frmSuKet_HD Is Nothing Then frmSuKet_HD.Dispose()
        frmSuKet_HD = Nothing

        If Not frmEMedrekTriage Is Nothing Then frmEMedrekTriage.Dispose()
        frmEMedrekTriage = Nothing
    End Sub
#End Region
#Region "Event"
    Private Sub grvAsesmenNakesPDF_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvAsesmenNakesPDF.FocusedRowChanged
        Try
            If grvAsesmenNakesPDF.GetFocusedRowCellValue("KDFORMULIR") Is Nothing Then
                PdfViewerAsesmenNakes.CloseDocument()
                Exit Sub
            End If

            If grvAsesmenNakesPDF.GetFocusedRowCellValue("DESCRIPTION") = "LAPORAN PERSALINAN" Then
                xtraReportLaporanPersalinan(grvAsesmenNakesPDF.GetFocusedRowCellValue("KDFORMULIR"))
            ElseIf grvAsesmenNakesPDF.GetFocusedRowCellValue("DESCRIPTION") = "ASESMEN AWAL KEPERAWATAN IGD" Then
                xtraReportAsesmenAwalPerawatIGD(grvAsesmenNakesPDF.GetFocusedRowCellValue("KDFORMULIR"))
            ElseIf grvAsesmenNakesPDF.GetFocusedRowCellValue("DESCRIPTION") = "ASESMEN AWAL KEPERAWATAN RAWAT JALAN" Then
                xtraReportLaporanAsesmenAwalKeperawatanRawatJalan(grvAsesmenNakesPDF.GetFocusedRowCellValue("KDFORMULIR"))
            ElseIf grvAsesmenNakesPDF.GetFocusedRowCellValue("DESCRIPTION") = "ASESMEN AWAL KEPERAWATAN RAWAT INAP" Then
                xtraReportLaporanAsesmenAwalKeperawatanRawatInap(grvAsesmenNakesPDF.GetFocusedRowCellValue("KDFORMULIR"))
            ElseIf grvAsesmenNakesPDF.GetFocusedRowCellValue("DESCRIPTION") = "SKRINNING DAN EDUKASI GIZI" Then
                xtraReportLaporanAsesmenAwalSkrining(grvAsesmenNakesPDF.GetFocusedRowCellValue("KDFORMULIR"))
            ElseIf grvAsesmenNakesPDF.GetFocusedRowCellValue("DESCRIPTION") = "ASESMEN KEBIDANAN PONEK" Then
                xtraReportLaporanAsesmenAwalPonek(grvAsesmenNakesPDF.GetFocusedRowCellValue("KDFORMULIR"))
            ElseIf grvAsesmenNakesPDF.GetFocusedRowCellValue("DESCRIPTION") = "PROTOKOL HEMODIALISA" Then
                xtraReportLaporanAsesmenAwalProtokolHD(grvAsesmenNakesPDF.GetFocusedRowCellValue("KDFORMULIR"))
            ElseIf grvAsesmenNakesPDF.GetFocusedRowCellValue("DESCRIPTION") = "SURAT KETERANGAN HEMODIALISA" Then
                xtraReportLaporanAsesmenAwalSuKetHD(grvAsesmenNakesPDF.GetFocusedRowCellValue("KDFORMULIR"))
            ElseIf grvAsesmenNakesPDF.GetFocusedRowCellValue("DESCRIPTION") = "LEMBAR PROGRAM TERAPI" Then
                xtraReportLaporanLembarProgramTerapi(grvAsesmenNakesPDF.GetFocusedRowCellValue("KDFORMULIR"))
            ElseIf grvAsesmenNakesPDF.GetFocusedRowCellValue("DESCRIPTION") = "LEMBAR TRIAGE" Then

            ElseIf grvAsesmenNakesPDF.GetFocusedRowCellValue("DESCRIPTION") = "FORMULIR GERD Q" Then
                xtraReportLaporanFormulirGerd(grvAsesmenNakesPDF.GetFocusedRowCellValue("KDFORMULIR"))
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
#End Region
#Region "Laporan Operasi"
#Region "Function"
    Private Sub QueryLaporanOperasi_LoadHistory(ByVal NoRM As String)
        Try
            If sSplashScreen = False Then Exit Sub

            grvLaporanOperasi_List.Columns.Clear()
            grdLaporanOperasi_List.DataSource = Nothing
            grvLaporanOperasi_List.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Processing data.....")

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\RME\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "A.KDLAPORANOPERASI "
            SQL &= ",A.JENIS_OPERASI "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",A.KDPENDAFTARAN "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_OK_LAPORANOPERASI A "
            SQL &= "WHERE A.KDCUSTOMER = '" & NoRM & "' "
            SQL &= "AND A.ISDELETE = 0 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_DIGITAL_OK_LAPORANOPERASI")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grdLaporanOperasi_List.MainView = grvLaporanOperasi_List
            grdLaporanOperasi_List.DataSource = ds.Tables("S_DIGITAL_OK_LAPORANOPERASI")
            grdLaporanOperasi_List.ForceInitialize()

            SplashScreenManager.CloseForm(False)

            QyeryLaporanOperasi_LoadFormatData()

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub QyeryLaporanOperasi_LoadFormatData()
        For iLoop As Integer = 0 To grvLaporanOperasi_List.Columns.Count - 1
            If grvLaporanOperasi_List.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvLaporanOperasi_List.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvLaporanOperasi_List.Columns(iLoop).DisplayFormat.FormatString = "{0:n0}"
                grvLaporanOperasi_List.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvLaporanOperasi_List.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvLaporanOperasi_List.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvLaporanOperasi_List.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm}"
            End If
        Next

        grvLaporanOperasi_List.Columns("KDLAPORANOPERASI").Visible = False
        grvLaporanOperasi_List.BestFitColumns()
    End Sub
    Private Sub QueryLaporanOperasi_LoadHistoryPDF(ByVal NoRM As String)
        Try
            If sSplashScreen = False Then Exit Sub

            grvLaporanOperasi_PDF.Columns.Clear()
            grdLaporanOperasi_PDF.DataSource = Nothing
            grvLaporanOperasi_PDF.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Processing data.....")

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\RME\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "A.KDLAPORANOPERASI "
            SQL &= ",A.JENIS_OPERASI "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",A.KDPENDAFTARAN "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_OK_LAPORANOPERASI A "
            SQL &= "WHERE A.KDCUSTOMER = '" & NoRM & "' "
            SQL &= "AND A.ISDELETE = 0 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_DIGITAL_OK_LAPORANOPERASI")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grdLaporanOperasi_PDF.MainView = grvLaporanOperasi_PDF
            grdLaporanOperasi_PDF.DataSource = ds.Tables("S_DIGITAL_OK_LAPORANOPERASI")
            grdLaporanOperasi_PDF.ForceInitialize()

            SplashScreenManager.CloseForm(False)

            QyeryLaporanOperasi_LoadFormatDataPDF()

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub QyeryLaporanOperasi_LoadFormatDataPDF()
        For iLoop As Integer = 0 To grvLaporanOperasi_PDF.Columns.Count - 1
            If grvLaporanOperasi_PDF.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvLaporanOperasi_PDF.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvLaporanOperasi_PDF.Columns(iLoop).DisplayFormat.FormatString = "{0:n0}"
                grvLaporanOperasi_PDF.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvLaporanOperasi_PDF.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvLaporanOperasi_PDF.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvLaporanOperasi_PDF.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm}"
            End If
        Next

        grvLaporanOperasi_PDF.Columns("KDLAPORANOPERASI").Visible = False
        grvLaporanOperasi_PDF.BestFitColumns()
    End Sub
    Private Sub xtraReportLaporanOperasi(ByVal Kode As String)
        Try
            PdfViewerLaporanOperasi.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Medis.....")

            Dim dataList As New List(Of Byte())
            Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim oDigital As New EMedrek.clsS_DIGITAL_OK_LAPORANOPERASI

            Dim ds = oDigital.GetDataKode(Kode)

            If ds IsNot Nothing Then
                Dim rpt As New xtraReportLAPORANOPERASI

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.BindingSource1.DataSource = ds
                rpt.ExportToPdf(Alamat)

                If FileIO.FileSystem.FileExists(Alamat) Then
                    PdfViewerLaporanOperasi.LoadDocument(Alamat)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Asesmen Medis" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
#Region "Command Button"
    Private Sub btnLaporanOperasi_Back_Click(sender As Object, e As EventArgs) Handles btnLaporanOperasi_Back.Click
        pnlLaporanOperasi_pic.Dock = DockStyle.Top

        XtraTabList_SelectedPageChanged()
        lgrdLaporanOperasi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lPanelLaporanOperasi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lLaporanOperasi_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If Not frmLaporanOperasi Is Nothing Then frmLaporanOperasi.Dispose()
        frmLaporanOperasi = Nothing
    End Sub
    Private Sub btnLaporanOperasi_BukaTutupList_Click(sender As Object, e As EventArgs) Handles btnLaporanOperasi_BukaTutupList.Click
        If btnLaporanOperasi_BukaTutupList.Text = "Tutup" Then
            btnLaporanOperasi_BukaTutupList.Text = "Buka"
            grdLaporanOperasi_PDF.Visible = False
        Else
            btnLaporanOperasi_BukaTutupList.Text = "Tutup"
            grdLaporanOperasi_PDF.Visible = True
        End If
    End Sub
    Private Sub picLaporanOperasi_Add_Click(sender As Object, e As EventArgs) Handles picLaporanOperasi_Add.Click
        If lblKDKUNJUNGAN.Text = "" Then
            MsgBox("Silahkan Pilih Dulu Pasien", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        Dim oDataGrouper As New Admission.clsPendaftaran_Kunjungan

        Dim ds = oDataGrouper.GetData(lblKDKUNJUNGAN.Text)

        If ds IsNot Nothing Then
            pnlLaporanOperasi_pic.Dock = DockStyle.None

            lgrdLaporanOperasi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lPanelLaporanOperasi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lLaporanOperasi_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            Try
                ' Bersihkan konten panel
                pnlLaporanOperasi.Controls.Clear()

                ' Set properti form anak
                frmLaporanOperasi.TopLevel = False
                frmLaporanOperasi.FormBorderStyle = FormBorderStyle.None
                frmLaporanOperasi.Dock = DockStyle.Fill

                ' Tambahkan form anak ke panel
                pnlLaporanOperasi.Controls.Add(frmLaporanOperasi)

                ' Tampilkan form anak
                frmLaporanOperasi.LoadMe(FORM_MODE.FORM_MODE_ADD, "", grdDPJP.EditValue, ds.KDDOCTOR, ds.S_PENDAFTARAN_H.KDCUSTOMER, ds.KDPENDAFTARAN)
                frmLaporanOperasi.Show()
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub picLaporanOperasi_Update_Click(sender As Object, e As EventArgs) Handles picLaporanOperasi_Update.Click
        If grvLaporanOperasi_List.GetFocusedRowCellValue("KDLAPORANOPERASI") Is Nothing Then
            MsgBox("Silahkan Pilih Kode", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If grvLaporanOperasi_List.GetFocusedRowCellValue("KDUSER") <> sUserID Then
            MsgBox("Maap Tidak dapat Rubah Silahkan Hubungi User " & grvLaporanOperasi_List.GetFocusedRowCellValue("KDUSER"), MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        pnlLaporanOperasi_pic.Dock = DockStyle.None

        lgrdLaporanOperasi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lPanelLaporanOperasi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lLaporanOperasi_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

        Dim oDigital As New EMedrek.clsS_DIGITAL_OK_LAPORANOPERASI

        Dim dsDigital = oDigital.GetDataKode(grvLaporanOperasi_List.GetFocusedRowCellValue("KDLAPORANOPERASI"))

        If dsDigital IsNot Nothing Then
            Try
                ' Bersihkan konten panel
                pnlLaporanOperasi.Controls.Clear()

                ' Set properti form anak
                frmLaporanOperasi.TopLevel = False
                frmLaporanOperasi.FormBorderStyle = FormBorderStyle.None
                frmLaporanOperasi.Dock = DockStyle.Fill

                ' Tambahkan form anak ke panel
                pnlLaporanOperasi.Controls.Add(frmLaporanOperasi)

                ' Tampilkan form anak
                frmLaporanOperasi.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.KDLAPORANOPERASI, grdDPJP.EditValue, dsDigital.KDDOCTOR, dsDigital.KDCUSTOMER, dsDigital.KDPENDAFTARAN)
                frmLaporanOperasi.Show()
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Else
            MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub picLaporanOperasi_Delete_Click(sender As Object, e As EventArgs) Handles picLaporanOperasi_Delete.Click
        If grvLaporanOperasi_List.GetFocusedRowCellValue("KDLAPORANOPERASI") Is Nothing Then
            MsgBox("Silahkan Pilih Kode", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If grvLaporanOperasi_List.GetFocusedRowCellValue("KDUSER") <> sUserID Then
            MsgBox("Maap Tidak dapat Rubah Silahkan Hubungi User " & grvLaporanOperasi_List.GetFocusedRowCellValue("KDUSER"), MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If MsgBox("Apakah Yakin Data Akan di Hapus", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        Dim frmPesanDelete As New frmPesanDelete
        frmPesanDelete.ShowDialog(Me)

        If sPesanHapus <> "XXXXXBATALXXXXX" Then
            Dim oDelete As New Setting.clsDelete

            If oDelete.InsertData("Laporan Operasi", sUserID, sPesanHapus & " " & Now.ToString("dd-MM-yyyy HH:mm:ss"), grvLaporanOperasi_List.GetFocusedRowCellValue("KDLAPORANOPERASI")) = False Then
                'fn_DeleteData = False
            End If

            Dim oDigital As New EMedrek.clsS_DIGITAL_OK_LAPORANOPERASI
            oDigital.UpdateDeleteLaporanOperasi(grvLaporanOperasi_List.GetFocusedRowCellValue("KDLAPORANOPERASI"), sUserID)
            XtraTabList_SelectedPageChanged()
        End If
    End Sub
    Private Sub picLaporanOperasi_Refresh_Click(sender As Object, e As EventArgs) Handles picLaporanOperasi_Refresh.Click
        XtraTabList_SelectedPageChanged()
        lgrdLaporanOperasi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lPanelLaporanOperasi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lLaporanOperasi_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If Not frmLaporanOperasi Is Nothing Then frmLaporanOperasi.Dispose()
        frmLaporanOperasi = Nothing
    End Sub
#End Region
#Region "Event"
    Private Sub grvLaporanOperasi_PDFFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvLaporanOperasi_PDF.FocusedRowChanged
        Try
            If grvLaporanOperasi_PDF.GetFocusedRowCellValue("KDLAPORANOPERASI") Is Nothing Then
                PdfViewerLaporanOperasi.CloseDocument()
                Exit Sub
            End If

            xtraReportLaporanOperasi(grvLaporanOperasi_PDF.GetFocusedRowCellValue("KDLAPORANOPERASI"))
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
#End Region
#Region "Laporan Tindakan"
#Region "Function"
    Private Sub QueryLaporanTindakan_LoadHistory(ByVal NoRM As String)
        Try
            If sSplashScreen = False Then Exit Sub

            grvLaporanTindakan_Transaksi.Columns.Clear()
            grdLaporanTindakan_Transaksi.DataSource = Nothing
            grvLaporanTindakan_Transaksi.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Processing data.....")

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\RME\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "A.KDLAPORANTINDAKAN "
            SQL &= ",JENISTINDAKAN = A.JENIS_OPERASI "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_OK_LAPORANTINDAKAN A "
            SQL &= "WHERE A.KDCUSTOMER = '" & NoRM & "' "
            SQL &= "AND A.ISDELETE = 0 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_DIGITAL_OK_LAPORANTINDAKAN")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grdLaporanTindakan_Transaksi.MainView = grvLaporanTindakan_Transaksi
            grdLaporanTindakan_Transaksi.DataSource = ds.Tables("S_DIGITAL_OK_LAPORANTINDAKAN")
            grdLaporanTindakan_Transaksi.ForceInitialize()

            SplashScreenManager.CloseForm(False)

            QyeryQueryLaporanTindakan_LoadFormatData()

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub QyeryQueryLaporanTindakan_LoadFormatData()
        For iLoop As Integer = 0 To grvLaporanTindakan_Transaksi.Columns.Count - 1
            If grvLaporanTindakan_Transaksi.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvLaporanTindakan_Transaksi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvLaporanTindakan_Transaksi.Columns(iLoop).DisplayFormat.FormatString = "{0:n0}"
                grvLaporanTindakan_Transaksi.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvLaporanTindakan_Transaksi.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvLaporanTindakan_Transaksi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvLaporanTindakan_Transaksi.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm}"
            End If
        Next

        grvLaporanTindakan_Transaksi.Columns("KDUSER").Caption = "User"

        'grvLaporanTindakan_Transaksi.Columns("Kode").Visible = False
        'grvLaporanTindakan_Transaksi.Columns("KDKUNJUNGAN").Visible = False
    End Sub
#End Region
#Region "Command Button"
    Private Sub btnLaporanTindakan_btnBack_Click(sender As Object, e As EventArgs) Handles btnLaporanTindakan_btnBack.Click
        pnlLaporanTindakan_pic.Dock = DockStyle.Top

        XtraTabList_SelectedPageChanged()
        lLaporanTindakan_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lLaporanTindakan_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lLaporanTindakan_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If Not frmLaporanTindakan Is Nothing Then frmLaporanTindakan.Dispose()
        frmLaporanTindakan = Nothing
    End Sub
    Private Sub picLaporanTindakan_Add_Click(sender As Object, e As EventArgs) Handles picLaporanTindakan_Add.Click
        If lblKDKUNJUNGAN.Text = "" Then
            MsgBox("Silahkan Pilih Dulu Pasien", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        Dim oDataGrouper As New Admission.clsPendaftaran_Kunjungan

        Dim ds = oDataGrouper.GetData(lblKDKUNJUNGAN.Text)

        If ds IsNot Nothing Then
            pnlLaporanTindakan_pic.Dock = DockStyle.None

            lLaporanTindakan_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lLaporanTindakan_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lLaporanTindakan_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            Try
                ' Bersihkan konten panel
                pnlLaporanTindakan.Controls.Clear()

                ' Set properti form anak
                frmLaporanTindakan.TopLevel = False
                frmLaporanTindakan.FormBorderStyle = FormBorderStyle.None
                frmLaporanTindakan.Dock = DockStyle.Fill

                ' Tambahkan form anak ke panel
                pnlLaporanTindakan.Controls.Add(frmLaporanTindakan)

                ' Tampilkan form anak
                frmLaporanTindakan.LoadMe(FORM_MODE.FORM_MODE_ADD, "", grdDPJP.EditValue, ds.KDDOCTOR, ds.S_PENDAFTARAN_H.KDCUSTOMER, ds.KDPENDAFTARAN)
                frmLaporanTindakan.Show()
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub picLaporanTindakan_Update_Click(sender As Object, e As EventArgs) Handles picLaporanTindakan_Update.Click
        If grvLaporanTindakan_Transaksi.GetFocusedRowCellValue("KDLAPORANTINDAKAN") Is Nothing Then
            MsgBox("Silahkan Pilih Kode", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If grvLaporanTindakan_Transaksi.GetFocusedRowCellValue("KDUSER") <> sUserID Then
            MsgBox("Maap Tidak dapat Rubah Silahkan Hubungi User " & grvLaporanTindakan_Transaksi.GetFocusedRowCellValue("KDUSER"), MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        pnlLaporanTindakan_pic.Dock = DockStyle.None

        lLaporanTindakan_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lLaporanTindakan_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lLaporanTindakan_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

        Dim oDigital As New EMedrek.clsS_DIGITAL_OK_LAPORANTINDAKAN

        Dim dsDigital = oDigital.GetData(grvLaporanTindakan_Transaksi.GetFocusedRowCellValue("KDLAPORANTINDAKAN"))

        If dsDigital IsNot Nothing Then
            Try
                ' Bersihkan konten panel
                pnlLaporanTindakan.Controls.Clear()

                ' Set properti form anak
                frmLaporanTindakan.TopLevel = False
                frmLaporanTindakan.FormBorderStyle = FormBorderStyle.None
                frmLaporanTindakan.Dock = DockStyle.Fill

                ' Tambahkan form anak ke panel
                pnlLaporanTindakan.Controls.Add(frmLaporanTindakan)

                ' Tampilkan form anak
                frmLaporanTindakan.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.KDLAPORANTINDAKAN, grdDPJP.EditValue, dsDigital.TextEdit1, dsDigital.KDCUSTOMER, dsDigital.KDPENDAFTARAN)
                frmLaporanTindakan.Show()
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Else
            MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub picLaporanTindakan_Delete_Click(sender As Object, e As EventArgs) Handles picLaporanTindakan_Delete.Click
        If grvLaporanTindakan_Transaksi.GetFocusedRowCellValue("KDLAPORANTINDAKAN") Is Nothing Then
            MsgBox("Silahkan Pilih Kode", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If grvLaporanTindakan_Transaksi.GetFocusedRowCellValue("KDUSER") <> sUserID Then
            MsgBox("Maap Tidak dapat Rubah Silahkan Hubungi User " & grvLaporanTindakan_Transaksi.GetFocusedRowCellValue("KDUSER"), MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If MsgBox("Apakah Yakin Data Akan di Hapus", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        Dim frmPesanDelete As New frmPesanDelete
        frmPesanDelete.ShowDialog(Me)

        If sPesanHapus <> "XXXXXBATALXXXXX" Then
            Dim oDelete As New Setting.clsDelete

            If oDelete.InsertData("LaporanTindakan", sUserID, sPesanHapus & " " & Now.ToString("dd-MM-yyyy HH:mm:ss"), grvLaporanTindakan_Transaksi.GetFocusedRowCellValue("KDLAPORANTINDAKAN")) = False Then
                'fn_DeleteData = False
            End If

            Dim oDigital As New EMedrek.clsS_DIGITAL_OK_LAPORANTINDAKAN
            oDigital.UpdateDeleteLaporanTindakan(grvLaporanTindakan_Transaksi.GetFocusedRowCellValue("KDLAPORANTINDAKAN"), sUserID)
            XtraTabList_SelectedPageChanged()
        End If
    End Sub
    Private Sub picLaporanTindakan_Refresh_Click(sender As Object, e As EventArgs) Handles picLaporanTindakan_Refresh.Click
        XtraTabList_SelectedPageChanged()
        lLaporanTindakan_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lLaporanTindakan_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lLaporanTindakan_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If Not frmLaporanTindakan Is Nothing Then frmLaporanTindakan.Dispose()
        frmLaporanTindakan = Nothing
    End Sub
#End Region
#Region "Event"
#End Region
#End Region
#Region "Transfer Internal"
#Region "Function"
    Private Sub QueryTransferInternal_LoadHistory(ByVal NoRM As String)
        Try
            If sSplashScreen = False Then Exit Sub

            grvTransferInternal_Transaksi.Columns.Clear()
            grdTransferInternal_Transaksi.DataSource = Nothing
            grvTransferInternal_Transaksi.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Processing data.....")

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\RME\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "A.KDTRANSFERINTERNAL "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_TRANSFER_INTERNAL A "
            SQL &= "WHERE A.KDCUSTOMER = '" & NoRM & "' "
            SQL &= "AND A.TEXT56 <> '' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_TRANSFER_INTERNAL")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grdTransferInternal_Transaksi.MainView = grvTransferInternal_Transaksi
            grdTransferInternal_Transaksi.DataSource = ds.Tables("S_TRANSFER_INTERNAL")
            grdTransferInternal_Transaksi.ForceInitialize()

            SplashScreenManager.CloseForm(False)

            QyeryQueryTransferInternal_LoadFormatData()

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub QyeryQueryTransferInternal_LoadFormatData()
        For iLoop As Integer = 0 To grvTransferInternal_Transaksi.Columns.Count - 1
            If grvTransferInternal_Transaksi.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvTransferInternal_Transaksi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvTransferInternal_Transaksi.Columns(iLoop).DisplayFormat.FormatString = "{0:n0}"
                grvTransferInternal_Transaksi.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvTransferInternal_Transaksi.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvTransferInternal_Transaksi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvTransferInternal_Transaksi.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm}"
            End If
        Next

        grvTransferInternal_Transaksi.Columns("KDTRANSFERINTERNAL").Visible = False
        grvTransferInternal_Transaksi.BestFitColumns()
    End Sub
#End Region
#Region "Command Button"
    Private Sub btnTransferInternal_Back_Click(sender As Object, e As EventArgs) Handles btnTransferInternal_Back.Click
        pnlTransferInternal_pic.Dock = DockStyle.Top

        XtraTabList_SelectedPageChanged()
        lTransferIntrenal_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lTransferIntrenal_Pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lTransferInternal_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If Not frmTransferInternal Is Nothing Then frmTransferInternal.Dispose()
        frmTransferInternal = Nothing
    End Sub
    'Private Sub btnTransferInternal_BukaTutupList_Click(sender As Object, e As EventArgs) Handles btnTransferInternal_BukaTutupList.Click
    '    If btnTransferInternal_BukaTutupList.Text = "Tutup" Then
    '        btnTransferInternal_BukaTutupList.Text = "Buka"
    '        grdTransferInternal_PDF.Visible = False
    '    Else
    '        btnTransferInternal_BukaTutupList.Text = "Tutup"
    '        grdTransferInternal_PDF.Visible = True
    '    End If
    'End Sub
    Private Sub picTransferInternal_Add_Click(sender As Object, e As EventArgs) Handles picTransferInternal_Add.Click
        If lblKDKUNJUNGAN.Text = "" Then
            MsgBox("Silahkan Pilih Dulu Pasien", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        Dim oDataGrouper As New Admission.clsPendaftaran_Kunjungan

        Dim ds = oDataGrouper.GetData(lblKDKUNJUNGAN.Text)

        If ds IsNot Nothing Then
            pnlTransferInternal_pic.Dock = DockStyle.None

            lTransferIntrenal_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lTransferIntrenal_Pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lTransferInternal_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            Try
                ' Bersihkan konten panel
                pnlTransferInternal_Transaksi.Controls.Clear()

                ' Set properti form anak
                frmTransferInternal.TopLevel = False
                frmTransferInternal.FormBorderStyle = FormBorderStyle.None
                frmTransferInternal.Dock = DockStyle.Fill

                ' Tambahkan form anak ke panel
                pnlTransferInternal_Transaksi.Controls.Add(frmTransferInternal)

                ' Tampilkan form anak
                frmTransferInternal.LoadMe(FORM_MODE.FORM_MODE_ADD, "", ds.KDPENDAFTARAN, ds.KDDOCTOR, ds.S_PENDAFTARAN_H.KDCUSTOMER, ds.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY, "")
                frmTransferInternal.Show()
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub picTransferInternal_Update_Click(sender As Object, e As EventArgs) Handles picTransferInternal_Update.Click
        If grvTransferInternal_Transaksi.GetFocusedRowCellValue("KDTRANSFERINTERNAL") Is Nothing Then
            MsgBox("Silahkan Pilih Kode", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If grvTransferInternal_Transaksi.GetFocusedRowCellValue("KDUSER") <> sUserID Then
            MsgBox("Maap Tidak dapat Rubah Silahkan Hubungi User " & grvLaporanOperasi_List.GetFocusedRowCellValue("KDUSER"), MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        pnlTransferInternal_pic.Dock = DockStyle.None

        lTransferIntrenal_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lTransferIntrenal_Pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lTransferInternal_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

        Dim oDigital As New Transaksi.clsTransferInternal

        Dim dsDigital = oDigital.GetData(grvTransferInternal_Transaksi.GetFocusedRowCellValue("KDTRANSFERINTERNAL"))

        If dsDigital IsNot Nothing Then
            Try
                ' Bersihkan konten panel
                pnlTransferInternal_Transaksi.Controls.Clear()

                ' Set properti form anak
                frmTransferInternal.TopLevel = False
                frmTransferInternal.FormBorderStyle = FormBorderStyle.None
                frmTransferInternal.Dock = DockStyle.Fill

                ' Tambahkan form anak ke panel
                pnlTransferInternal_Transaksi.Controls.Add(frmTransferInternal)

                ' Tampilkan form anak
                frmTransferInternal.LoadMe(FORM_MODE.FORM_MODE_EDIT, "", dsDigital.KDREG, dsDigital.DESCRIPTION, dsDigital.KDCUSTOMER, dsDigital.TEXT59, "")
                frmTransferInternal.Show()
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Else
            MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub picTransferInternal_Delete_Click(sender As Object, e As EventArgs) Handles picTransferInternal_Delete.Click
        If grvTransferInternal_Transaksi.GetFocusedRowCellValue("KDTRANSFERINTERNAL") Is Nothing Then
            MsgBox("Silahkan Pilih Kode", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If grvTransferInternal_Transaksi.GetFocusedRowCellValue("KDUSER") <> sUserID Then
            MsgBox("Maap Tidak dapat Rubah Silahkan Hubungi User " & grvTransferInternal_Transaksi.GetFocusedRowCellValue("KDUSER"), MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If MsgBox("Apakah Yakin Data Akan di Hapus", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        Dim frmPesanDelete As New frmPesanDelete
        frmPesanDelete.ShowDialog(Me)

        If sPesanHapus <> "XXXXXBATALXXXXX" Then
            Dim oDigital As New Transaksi.clsTransferInternal
            oDigital.UpdateDeleteTransferInternal(grvTransferInternal_Transaksi.GetFocusedRowCellValue("KDTRANSFERINTERNAL"), sUserID, sPesanHapus)
            XtraTabList_SelectedPageChanged()
        End If
    End Sub
    Private Sub picTransferInternal_Refresh_Click(sender As Object, e As EventArgs) Handles picTransferInternal_Refresh.Click
        XtraTabList_SelectedPageChanged()
        lTransferIntrenal_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lTransferIntrenal_Pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lTransferInternal_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If Not frmTransferInternal Is Nothing Then frmTransferInternal.Dispose()
        frmTransferInternal = Nothing
    End Sub
#End Region
#Region "Event"

#End Region
#End Region
#Region "Tindakan Evaluasi Keperawatan"
#Region "Function"
    Private Sub QueryTindakanEvaluasi_LoadHistory(ByVal NoRM As String)
        Try
            If sSplashScreen = False Then Exit Sub

            grvTindakanEvaluasi_Transaksi.Columns.Clear()
            grdTindakanEvaluasi_Transaksi.DataSource = Nothing
            grvTindakanEvaluasi_Transaksi.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Processing data.....")

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\RME\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM ( "
            SQL &= "SELECT "
            SQL &= "KATEGORI = '1. B.D' "
            SQL &= ",TANGGAL = CONVERT(VARCHAR(8), A.DATE, 103) "
            SQL &= ",A.KDPENDAFTARAN "
            SQL &= ",A.KDDIAGNOSAPERAWAT "
            SQL &= ",A.KDITEMDIAGNOSAPERAWAT "
            SQL &= ",DESKRIPSI = C.BERHUBUNGANDENGAN "
            SQL &= ",EVALUASI = '' "
            SQL &= ",A.KDUSER "
            SQL &= ",C.SEQ "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_DIAGNOSAPERAWAT_H A "
            SQL &= "INNER JOIN S_DIGITAL_DIAGNOSAPERAWAT_D_1 C "
            SQL &= "ON A.KDDIAGNOSAPERAWAT = C.KDDIAGNOSAPERAWAT "
            SQL &= "WHERE A.KDCUSTOMER = '" & NoRM & "' "
            SQL &= "AND C.BERHUBUNGANDENGAN_ISCHEKED = 1 "
            SQL &= "AND A.ISACTIVE = 0 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KATEGORI = '2. D.D' "
            SQL &= ",TANGGAL = CONVERT(VARCHAR(8), A.DATE, 103) "
            SQL &= ",A.KDPENDAFTARAN "
            SQL &= ",A.KDDIAGNOSAPERAWAT "
            SQL &= ",A.KDITEMDIAGNOSAPERAWAT "
            SQL &= ",DESKRIPSI = C.DITANDAIDENGAN "
            SQL &= ",EVALUASI = '' "
            SQL &= ",A.KDUSER "
            SQL &= ",C.SEQ "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_DIAGNOSAPERAWAT_H A "
            SQL &= "INNER JOIN S_DIGITAL_DIAGNOSAPERAWAT_D_2 C "
            SQL &= "ON A.KDDIAGNOSAPERAWAT = C.KDDIAGNOSAPERAWAT "
            SQL &= "WHERE A.KDCUSTOMER = '" & NoRM & "' "
            SQL &= "AND C.DITANDAIDENGAN_ISCHEKED = 1 "
            SQL &= "AND A.ISACTIVE = 0 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KATEGORI = '3. TUJUAN' "
            SQL &= ",TANGGAL = CONVERT(VARCHAR(8), A.DATE, 103) "
            SQL &= ",A.KDPENDAFTARAN "
            SQL &= ",A.KDDIAGNOSAPERAWAT "
            SQL &= ",A.KDITEMDIAGNOSAPERAWAT "
            SQL &= ",DESKRIPSI = C.TUJUAN "
            SQL &= ",EVALUASI = '' "
            SQL &= ",A.KDUSER "
            SQL &= ",C.SEQ "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_DIAGNOSAPERAWAT_H A "
            SQL &= "INNER JOIN S_DIGITAL_DIAGNOSAPERAWAT_D_3 C "
            SQL &= "ON A.KDDIAGNOSAPERAWAT = C.KDDIAGNOSAPERAWAT "
            SQL &= "WHERE A.KDCUSTOMER = '" & NoRM & "' "
            SQL &= "AND C.TUJUAN_ISCHEKED = 1 "
            SQL &= "AND A.ISACTIVE = 0 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KATEGORI = '4. KRITERIA' "
            SQL &= ",TANGGAL = CONVERT(VARCHAR(8), A.DATE, 103) "
            SQL &= ",A.KDPENDAFTARAN "
            SQL &= ",A.KDDIAGNOSAPERAWAT "
            SQL &= ",A.KDITEMDIAGNOSAPERAWAT "
            SQL &= ",DESKRIPSI = C.KRITERIA "
            SQL &= ",EVALUASI = '' "
            SQL &= ",A.KDUSER "
            SQL &= ",C.SEQ "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_DIAGNOSAPERAWAT_H A "
            SQL &= "INNER JOIN S_DIGITAL_DIAGNOSAPERAWAT_D_4 C "
            SQL &= "ON A.KDDIAGNOSAPERAWAT = C.KDDIAGNOSAPERAWAT "
            SQL &= "WHERE A.KDCUSTOMER = '" & NoRM & "' "
            SQL &= "AND C.KRITERIA_ISCHEKED = 1 "
            SQL &= "AND A.ISACTIVE = 0 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KATEGORI = '5. INTERVENSI' "
            SQL &= ",TANGGAL = CONVERT(VARCHAR(8), A.DATE, 103) "
            SQL &= ",A.KDPENDAFTARAN "
            SQL &= ",A.KDDIAGNOSAPERAWAT "
            SQL &= ",A.KDITEMDIAGNOSAPERAWAT "
            SQL &= ",DESKRIPSI = C.INTERVENSI "
            SQL &= ",EVALUASI = '' "
            SQL &= ",A.KDUSER "
            SQL &= ",C.SEQ "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_DIAGNOSAPERAWAT_H A "
            SQL &= "INNER JOIN S_DIGITAL_DIAGNOSAPERAWAT_D_5 C "
            SQL &= "ON A.KDDIAGNOSAPERAWAT = C.KDDIAGNOSAPERAWAT "
            SQL &= "WHERE A.KDCUSTOMER = '" & NoRM & "' "
            SQL &= "AND C.INTERVENSI_ISCHEKED = 1 "
            SQL &= "AND A.ISACTIVE = 0 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KATEGORI = '6. IMPLEMENTASI' "
            SQL &= ",C.TANGGAL "
            SQL &= ",A.KDPENDAFTARAN "
            SQL &= ",A.KDDIAGNOSAPERAWAT "
            SQL &= ",A.KDITEMDIAGNOSAPERAWAT "
            SQL &= ",DESKRIPSI = C.IMPLEMENTASI "
            SQL &= ",C.EVALUASI "
            SQL &= ",C.KDUSER "
            SQL &= ",C.SEQ "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_DIAGNOSAPERAWAT_H A "
            SQL &= "INNER JOIN S_DIGITAL_DIAGNOSAPERAWAT_D_6 C "
            SQL &= "ON A.KDDIAGNOSAPERAWAT = C.KDDIAGNOSAPERAWAT "
            SQL &= "WHERE A.KDCUSTOMER = '" & NoRM & "' "
            SQL &= "AND C.IMPLEMENTASI_ISCHEKED = 1 "
            SQL &= "AND A.ISACTIVE = 0 "

            SQL &= ") Z "
            SQL &= "ORDER BY Z.KDPENDAFTARAN, Z.SEQ "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_DIGITAL_DIAGNOSAPERAWAT_H")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grdTindakanEvaluasi_Transaksi.MainView = grvTindakanEvaluasi_Transaksi
            grdTindakanEvaluasi_Transaksi.DataSource = ds.Tables("S_DIGITAL_DIAGNOSAPERAWAT_H")
            grdTindakanEvaluasi_Transaksi.ForceInitialize()

            SplashScreenManager.CloseForm(False)

            QyeryQueryTindakanEvaluasi_LoadFormatData()

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub QyeryQueryTindakanEvaluasi_LoadFormatData()
        For iLoop As Integer = 0 To grvTindakanEvaluasi_Transaksi.Columns.Count - 1
            If grvTindakanEvaluasi_Transaksi.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvTindakanEvaluasi_Transaksi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvTindakanEvaluasi_Transaksi.Columns(iLoop).DisplayFormat.FormatString = "{0:n0}"
                grvTindakanEvaluasi_Transaksi.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvTindakanEvaluasi_Transaksi.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvTindakanEvaluasi_Transaksi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvTindakanEvaluasi_Transaksi.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm}"
            End If
        Next

        grvTindakanEvaluasi_Transaksi.Columns("KDUSER").Caption = "User"
        grvTindakanEvaluasi_Transaksi.Columns("SEQ").Visible = False
        grvTindakanEvaluasi_Transaksi.Columns("KDPENDAFTARAN").Visible = False

        grvTindakanEvaluasi_Transaksi.Columns("KDDIAGNOSAPERAWAT").Group()
        grvTindakanEvaluasi_Transaksi.Columns("KDITEMDIAGNOSAPERAWAT").Group()
        grvTindakanEvaluasi_Transaksi.Columns("KATEGORI").Group()
        grvTindakanEvaluasi_Transaksi.ExpandAllGroups()
    End Sub
#End Region
#Region "Command Button"
    Private Sub btnTindakanEvaluasi_Back_Click(sender As Object, e As EventArgs) Handles btnTindakanEvaluasi_Back.Click
        pnlTindakanEvaluasi_pic.Dock = DockStyle.Top

        XtraTabList_SelectedPageChanged()
        lTindakanEvaluasi_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lTindakanEvaluasi_Pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lTindakanEvaluasi_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If Not frmDiagnosaPerawat Is Nothing Then frmDiagnosaPerawat.Dispose()
        frmDiagnosaPerawat = Nothing
    End Sub
    'Private Sub btnTindakanEvaluasi_BukaTutupList_Click(sender As Object, e As EventArgs) Handles btnTindakanEvaluasi_BukaTutupList.Click
    '    If btnTindakanEvaluasi_BukaTutupList.Text = "Tutup" Then
    '        btnTindakanEvaluasi_BukaTutupList.Text = "Buka"
    '        grdTindakanEvaluasi_PDF.Visible = False
    '    Else
    '        btnTindakanEvaluasi_BukaTutupList.Text = "Tutup"
    '        grdTindakanEvaluasi_PDF.Visible = True
    '    End If
    'End Sub
    Private Sub picTindakanEvaluasi_Add_Click(sender As Object, e As EventArgs) Handles picTindakanEvaluasi_Add.Click
        If lblKDKUNJUNGAN.Text = "" Then
            MsgBox("Silahkan Pilih Dulu Pasien", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        Dim oDataGrouper As New Admission.clsPendaftaran_Kunjungan

        Dim ds = oDataGrouper.GetData(lblKDKUNJUNGAN.Text)

        If ds IsNot Nothing Then
            pnlTindakanEvaluasi_pic.Dock = DockStyle.None

            lTindakanEvaluasi_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lTindakanEvaluasi_Pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lTindakanEvaluasi_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            Try
                ' Bersihkan konten panel
                pnllTindakanEvaluasi.Controls.Clear()

                ' Set properti form anak
                frmDiagnosaPerawat.TopLevel = False
                frmDiagnosaPerawat.FormBorderStyle = FormBorderStyle.None
                frmDiagnosaPerawat.Dock = DockStyle.Fill

                ' Tambahkan form anak ke panel
                pnllTindakanEvaluasi.Controls.Add(frmDiagnosaPerawat)

                ' Tampilkan form anak
                frmDiagnosaPerawat.LoadMe(FORM_MODE.FORM_MODE_ADD, ds.KDPENDAFTARAN, ds.S_PENDAFTARAN_H.KDCUSTOMER, sUserID)
                frmDiagnosaPerawat.Show()
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub picTindakanEvaluasi_Update_Click(sender As Object, e As EventArgs) Handles picTindakanEvaluasi_Update.Click
        If grvTindakanEvaluasi_Transaksi.GetFocusedRowCellValue("KDDIAGNOSAPERAWAT") Is Nothing Then
            MsgBox("Silahkan Pilih Kode", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If grvTindakanEvaluasi_Transaksi.GetFocusedRowCellValue("KDUSER") <> sUserID Then
            MsgBox("Maap Tidak dapat Rubah Silahkan Hubungi User " & grvTindakanEvaluasi_Transaksi.GetFocusedRowCellValue("KDUSER"), MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        pnlTindakanEvaluasi_pic.Dock = DockStyle.None

        lTindakanEvaluasi_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lTindakanEvaluasi_Pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lTindakanEvaluasi_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

        Dim oDigital As New Digital.clsDiagnosaPerawat

        Dim dsDigital = oDigital.GetData(grvTindakanEvaluasi_Transaksi.GetFocusedRowCellValue("KDDIAGNOSAPERAWAT"))

        If dsDigital IsNot Nothing Then
            Try
                ' Bersihkan konten panel
                pnllTindakanEvaluasi.Controls.Clear()

                ' Set properti form anak
                frmDiagnosaPerawat.TopLevel = False
                frmDiagnosaPerawat.FormBorderStyle = FormBorderStyle.None
                frmDiagnosaPerawat.Dock = DockStyle.Fill

                ' Tambahkan form anak ke panel
                pnllTindakanEvaluasi.Controls.Add(frmDiagnosaPerawat)

                ' Tampilkan form anak
                frmDiagnosaPerawat.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.KDPENDAFTARAN, dsDigital.KDCUSTOMER, sUserID, dsDigital.KDDIAGNOSAPERAWAT)
                frmDiagnosaPerawat.Show()
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Else
            MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub picTindakanEvaluasi_Delete_Click(sender As Object, e As EventArgs) Handles picTindakanEvaluasi_Delete.Click
        If grvTindakanEvaluasi_Transaksi.GetFocusedRowCellValue("KDDIAGNOSAPERAWAT") Is Nothing Then
            MsgBox("Silahkan Pilih Kode", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If grvTindakanEvaluasi_Transaksi.GetFocusedRowCellValue("KDUSER") <> sUserID Then
            MsgBox("Maap Tidak dapat Rubah Silahkan Hubungi User " & grvTindakanEvaluasi_Transaksi.GetFocusedRowCellValue("KDUSER"), MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If


        If MsgBox("Apakah Yakin Data Akan di Hapus", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        Dim frmPesanDelete As New frmPesanDelete
        frmPesanDelete.ShowDialog(Me)

        If sPesanHapus <> "XXXXXBATALXXXXX" Then
            Dim oDelete As New Setting.clsDelete

            If oDelete.InsertData("TINDAKANEVALUASI", sUserID, sPesanHapus & " " & Now.ToString("dd-MM-yyyy HH:mm:ss"), grvTindakanEvaluasi_Transaksi.GetFocusedRowCellValue("KDDIAGNOSAPERAWAT")) = False Then
                'fn_DeleteData = False
            End If

            Dim oDigital As New Digital.clsDiagnosaPerawat
            oDigital.DeleteData(grvTindakanEvaluasi_Transaksi.GetFocusedRowCellValue("KDDIAGNOSAPERAWAT"))
            XtraTabList_SelectedPageChanged()
        End If
    End Sub
    Private Sub picTindakanEvaluasi_Refresh_Click(sender As Object, e As EventArgs) Handles picTindakanEvaluasi_Refresh.Click
        XtraTabList_SelectedPageChanged()
        lTindakanEvaluasi_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lTindakanEvaluasi_Pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lTindakanEvaluasi_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If Not frmDiagnosaPerawat Is Nothing Then frmDiagnosaPerawat.Dispose()
        frmDiagnosaPerawat = Nothing
    End Sub
#End Region
#Region "Event"

#End Region
#End Region
#Region "GerdQ"
#Region "Function"
    Private Sub QueryGerdQ_LoadHistory(ByVal NoRM As String)
        Try
            If sSplashScreen = False Then Exit Sub

            grvGerdQ_Transaksi.Columns.Clear()
            grdGerdQ_Transaksi.DataSource = Nothing
            grvGerdQ_Transaksi.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Processing data.....")

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\RME\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "Kode = A.KDGERDQ "
            SQL &= ",Tanggal = A.DATE "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_GERDQ_H A "
            SQL &= "WHERE A.KDCUSTOMER = '" & NoRM & "' "
            SQL &= "AND A.ISDELETE = 0 "
            SQL &= "ORDER BY A.DATE DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_DIGITAL_GERDQ_H")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grdGerdQ_Transaksi.MainView = grvGerdQ_Transaksi
            grdGerdQ_Transaksi.DataSource = ds.Tables("S_DIGITAL_GERDQ_H")
            grdGerdQ_Transaksi.ForceInitialize()

            SplashScreenManager.CloseForm(False)

            QyeryQueryGerdQ_LoadFormatData()

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub QyeryQueryGerdQ_LoadFormatData()
        For iLoop As Integer = 0 To grvGerdQ_Transaksi.Columns.Count - 1
            If grvGerdQ_Transaksi.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvGerdQ_Transaksi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvGerdQ_Transaksi.Columns(iLoop).DisplayFormat.FormatString = "{0:n0}"
                grvGerdQ_Transaksi.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvGerdQ_Transaksi.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvGerdQ_Transaksi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvGerdQ_Transaksi.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm}"
            End If
        Next

        grvGerdQ_Transaksi.Columns("KDUSER").Caption = "User"

        grvGerdQ_Transaksi.Columns("Kode").Group()
        grvGerdQ_Transaksi.ExpandAllGroups()
    End Sub
#End Region
#Region "Command Button"
    Private Sub btnGerdQ_Back_Click(sender As Object, e As EventArgs) Handles btnGerdQ_Back.Click
        pnlGerdQ_pic.Dock = DockStyle.Top

        XtraTabList_SelectedPageChanged()
        lGerdQ_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lGerdQ_Pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lGerdQ_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If Not frmGerdQ Is Nothing Then frmGerdQ.Dispose()
        frmGerdQ = Nothing
    End Sub
    Private Sub picGerdQ_Add_Click(sender As Object, e As EventArgs) Handles picGerdQ_Add.Click
        If lblKDKUNJUNGAN.Text = "" Then
            MsgBox("Silahkan Pilih Dulu Pasien", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        Dim oDataGrouper As New Admission.clsPendaftaran_Kunjungan

        Dim ds = oDataGrouper.GetData(lblKDKUNJUNGAN.Text)

        If ds IsNot Nothing Then
            pnlGerdQ_pic.Dock = DockStyle.None

            lGerdQ_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lGerdQ_Pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lGerdQ_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            Try
                ' Bersihkan konten panel
                pnllGerdQ.Controls.Clear()

                ' Set properti form anak
                frmGerdQ.TopLevel = False
                frmGerdQ.FormBorderStyle = FormBorderStyle.None
                frmGerdQ.Dock = DockStyle.Fill

                ' Tambahkan form anak ke panel
                pnllGerdQ.Controls.Add(frmGerdQ)

                ' Tampilkan form anak
                frmGerdQ.LoadMe(FORM_MODE.FORM_MODE_ADD, ds.KDPENDAFTARAN, ds.S_PENDAFTARAN_H.KDCUSTOMER, ds.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY, ds.KDDOCTOR, "", False)
                frmGerdQ.Show()
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub picGerdQ_Update_Click(sender As Object, e As EventArgs) Handles picGerdQ_Update.Click
        If grvGerdQ_Transaksi.GetFocusedRowCellValue("Kode") Is Nothing Then
            MsgBox("Silahkan Pilih Kode", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If grvGerdQ_Transaksi.GetFocusedRowCellValue("KDUSER") <> sUserID Then
            MsgBox("Maap Tidak dapat Rubah Silahkan Hubungi User " & grvGerdQ_Transaksi.GetFocusedRowCellValue("KDUSER"), MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        pnlGerdQ_pic.Dock = DockStyle.None

        lGerdQ_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lGerdQ_Pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lGerdQ_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

        Dim oDigital As New Digital.clsGerdQ

        Dim dsDigital = oDigital.GetData(grvGerdQ_Transaksi.GetFocusedRowCellValue("Kode"))

        If dsDigital IsNot Nothing Then
            Try
                ' Bersihkan konten panel
                pnllGerdQ.Controls.Clear()

                ' Set properti form anak
                frmGerdQ.TopLevel = False
                frmGerdQ.FormBorderStyle = FormBorderStyle.None
                frmGerdQ.Dock = DockStyle.Fill

                ' Tambahkan form anak ke panel
                pnllGerdQ.Controls.Add(frmGerdQ)

                ' Tampilkan form anak
                frmGerdQ.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.KDPENDAFTARAN, dsDigital.KDCUSTOMER, lblNamaPasien.Text, dsDigital.KDDOCTOR, dsDigital.KDGERDQ, False)
                frmGerdQ.Show()
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Else
            MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub picGerdQ_Delete_Click(sender As Object, e As EventArgs) Handles picGerdQ_Delete.Click
        If grvGerdQ_Transaksi.GetFocusedRowCellValue("Kode") Is Nothing Then
            MsgBox("Silahkan Pilih Kode", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If grvGerdQ_Transaksi.GetFocusedRowCellValue("KDUSER") <> sUserID Then
            MsgBox("Maap Tidak dapat Rubah Silahkan Hubungi User " & grvGerdQ_Transaksi.GetFocusedRowCellValue("KDUSER"), MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If MsgBox("Apakah Yakin Data Akan di Hapus", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        Dim frmPesanDelete As New frmPesanDelete
        frmPesanDelete.ShowDialog(Me)

        If sPesanHapus <> "XXXXXBATALXXXXX" Then
            Dim oDelete As New Setting.clsDelete

            If oDelete.InsertData("GERDQ", sUserID, sPesanHapus & " " & Now.ToString("dd-MM-yyyy HH:mm:ss"), grvGerdQ_Transaksi.GetFocusedRowCellValue("Kode")) = False Then
                'fn_DeleteData = False
            End If

            Dim oDigital As New Digital.clsGerdQ
            oDigital.UpdateDelete(grvGerdQ_Transaksi.GetFocusedRowCellValue("Kode"), sUserID)
            XtraTabList_SelectedPageChanged()
        End If
    End Sub
    Private Sub picGerdQ_Refresh_Click(sender As Object, e As EventArgs) Handles picGerdQ_Refresh.Click
        XtraTabList_SelectedPageChanged()
        lGerdQ_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lGerdQ_Pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lGerdQ_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If Not frmGerdQ Is Nothing Then frmGerdQ.Dispose()
        frmGerdQ = Nothing
    End Sub
#End Region
#Region "Event"
#End Region
#End Region
#Region "Rencana Operasi"
#Region "Function"
    Private Sub QueryRencanaOperasi_LoadHistory(ByVal NoRM As String)
        Try
            If sSplashScreen = False Then Exit Sub

            grvRencanaOperasi_Transaksi.Columns.Clear()
            grdRencanaOperasi_Transaksi.DataSource = Nothing
            grvRencanaOperasi_Transaksi.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Processing data.....")

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
            SQL &= "Kode = A.KDBOOKINGJADWALOPERASI "
            SQL &= ",A.KDKUNJUNGAN "
            SQL &= ",Tanggal = A.TANGGALOPERASI "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "SET_BOOKING_JADWALOPERASI A "
            SQL &= "INNER JOIN S_PENDAFTARAN_KUNJUNGAN B "
            SQL &= "ON A.KDKUNJUNGAN = B.KDKUNJUNGAN "
            SQL &= "INNER JOIN S_PENDAFTARAN_H C "
            SQL &= "ON B.KDPENDAFTARAN = C.KDPENDAFTARAN "
            SQL &= "WHERE C.KDCUSTOMER = '" & NoRM & "' "
            SQL &= "ORDER BY A.TANGGALOPERASI DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "SET_BOOKING_JADWALOPERASI")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grdRencanaOperasi_Transaksi.MainView = grvRencanaOperasi_Transaksi
            grdRencanaOperasi_Transaksi.DataSource = ds.Tables("SET_BOOKING_JADWALOPERASI")
            grdRencanaOperasi_Transaksi.ForceInitialize()

            SplashScreenManager.CloseForm(False)

            QyeryQueryRencanaOperasi_LoadFormatData()

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub QyeryQueryRencanaOperasi_LoadFormatData()
        For iLoop As Integer = 0 To grvRencanaOperasi_Transaksi.Columns.Count - 1
            If grvRencanaOperasi_Transaksi.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvRencanaOperasi_Transaksi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvRencanaOperasi_Transaksi.Columns(iLoop).DisplayFormat.FormatString = "{0:n0}"
                grvRencanaOperasi_Transaksi.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvRencanaOperasi_Transaksi.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvRencanaOperasi_Transaksi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvRencanaOperasi_Transaksi.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm}"
            End If
        Next

        grvRencanaOperasi_Transaksi.Columns("KDUSER").Caption = "User"

        grvRencanaOperasi_Transaksi.Columns("Kode").Visible = False
        grvRencanaOperasi_Transaksi.Columns("KDKUNJUNGAN").Visible = False
    End Sub
#End Region
#Region "Command Button"
    Private Sub btnRencanaOperasi_btnBack_Click(sender As Object, e As EventArgs) Handles btnRencanaOperasi_btnBack.Click
        pnlRencanaOperasi_pic.Dock = DockStyle.Top

        XtraTabList_SelectedPageChanged()
        lRencanaOperasi_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lRencanaOperasi_Pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lRencanaOperasi_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If Not frmRencanaOperasi Is Nothing Then frmRencanaOperasi.Dispose()
        frmRencanaOperasi = Nothing
    End Sub
    Private Sub picRencanaOperasi_Add_Click(sender As Object, e As EventArgs) Handles picRencanaOperasi_Add.Click
        If lblKDKUNJUNGAN.Text = "" Then
            MsgBox("Silahkan Pilih Dulu Pasien", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        Dim oDataGrouper As New Admission.clsPendaftaran_Kunjungan

        Dim ds = oDataGrouper.GetData(lblKDKUNJUNGAN.Text)

        If ds IsNot Nothing Then
            pnlRencanaOperasi_pic.Dock = DockStyle.None

            lRencanaOperasi_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lRencanaOperasi_Pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lRencanaOperasi_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            Try
                ' Bersihkan konten panel
                pnlRencanaOperasi_Transaksi.Controls.Clear()

                ' Set properti form anak
                frmRencanaOperasi.TopLevel = False
                frmRencanaOperasi.FormBorderStyle = FormBorderStyle.None
                frmRencanaOperasi.Dock = DockStyle.Fill

                ' Tambahkan form anak ke panel
                pnlRencanaOperasi_Transaksi.Controls.Add(frmRencanaOperasi)

                ' Tampilkan form anak
                frmRencanaOperasi.LoadMe(FORM_MODE.FORM_MODE_ADD, ds.KDKUNJUNGAN)
                frmRencanaOperasi.Show()
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub picRencanaOperasi_Update_Click(sender As Object, e As EventArgs) Handles picRencanaOperasi_Update.Click
        If grvRencanaOperasi_Transaksi.GetFocusedRowCellValue("Kode") Is Nothing Then
            MsgBox("Silahkan Pilih Kode", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If grvRencanaOperasi_Transaksi.GetFocusedRowCellValue("KDUSER") <> sUserID Then
            MsgBox("Maap Tidak dapat Rubah Silahkan Hubungi User " & grvRencanaOperasi_Transaksi.GetFocusedRowCellValue("KDUSER"), MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        pnlRencanaOperasi_pic.Dock = DockStyle.None

        lRencanaOperasi_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lRencanaOperasi_Pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lRencanaOperasi_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

        Dim oDigital As New WebService.clsSET_BOOKING_JADWALOPERASI

        Dim dsDigital = oDigital.GetData(grvRencanaOperasi_Transaksi.GetFocusedRowCellValue("Kode"))

        If dsDigital IsNot Nothing Then
            Try
                ' Bersihkan konten panel
                pnlRencanaOperasi_Transaksi.Controls.Clear()

                ' Set properti form anak
                frmRencanaOperasi.TopLevel = False
                frmRencanaOperasi.FormBorderStyle = FormBorderStyle.None
                frmRencanaOperasi.Dock = DockStyle.Fill

                ' Tambahkan form anak ke panel
                pnlRencanaOperasi_Transaksi.Controls.Add(frmRencanaOperasi)

                ' Tampilkan form anak
                frmRencanaOperasi.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.KDKUNJUNGAN, dsDigital.KDBOOKINGJADWALOPERASI)
                frmRencanaOperasi.Show()
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Else
            MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub picRencanaOperasi_Delete_Click(sender As Object, e As EventArgs) Handles picRencanaOperasi_Delete.Click
        If grvRencanaOperasi_Transaksi.GetFocusedRowCellValue("Kode") Is Nothing Then
            MsgBox("Silahkan Pilih Kode", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If grvRencanaOperasi_Transaksi.GetFocusedRowCellValue("KDUSER") <> sUserID Then
            MsgBox("Maap Tidak dapat Rubah Silahkan Hubungi User " & grvRencanaOperasi_Transaksi.GetFocusedRowCellValue("KDUSER"), MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        'If MsgBox("Apakah Yakin Data Akan di Hapus", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        'Dim frmPesanDelete As New frmPesanDelete
        'frmPesanDelete.ShowDialog(Me)

        'If sPesanHapus <> "XXXXXBATALXXXXX" Then
        '    Dim oDelete As New Setting.clsDelete

        '    If oDelete.InsertData("RencanaOperasi", sUserID, sPesanHapus & " " & Now.ToString("dd-MM-yyyy HH:mm:ss"), grvRencanaOperasi_Transaksi.GetFocusedRowCellValue("Kode")) = False Then
        '        'fn_DeleteData = False
        '    End If

        '    Dim oDigital As New Digital.clsRencanaOperasi
        '    oDigital.UpdateDelete(grvRencanaOperasi_Transaksi.GetFocusedRowCellValue("Kode"), sUserID)
        '    XtraTabList_SelectedPageChanged()
        'End If
    End Sub
    Private Sub picRencanaOperasi_Refresh_Click(sender As Object, e As EventArgs) Handles picRencanaOperasi_Refresh.Click
        XtraTabList_SelectedPageChanged()
        lRencanaOperasi_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lRencanaOperasi_Pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lRencanaOperasi_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If Not frmRencanaOperasi Is Nothing Then frmRencanaOperasi.Dispose()
        frmRencanaOperasi = Nothing
    End Sub
#End Region
#Region "Event"
#End Region
#End Region
#Region "Resume Rawat Inap"
#Region "Function"
    Private Sub QueryResumeRawatInap_LoadHistory(ByVal NoRM As String)
        Try
            If sSplashScreen = False Then Exit Sub

            grvResumeRawatInap_Transaksi.Columns.Clear()
            grdResumeRawatInap_Transaksi.DataSource = Nothing
            grvResumeRawatInap_Transaksi.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Processing data.....")

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\RME\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "Kode = A.KDREG "
            SQL &= ",TanggalMasuk = A.TANGGALMASUK "
            SQL &= ",NoRegister = A.KDREG "
            SQL &= ",RuangRawat = A.RUANGRAWAT "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_RINGKASANKELUARRAWATINAP A "
            SQL &= "WHERE A.KDCUSTOMER = '" & NoRM & "' "
            SQL &= "AND A.ISDELETE = 0 "
            SQL &= "ORDER BY A.TANGGALMASUK DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_RINGKASANKELUARRAWATINAP")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grdResumeRawatInap_Transaksi.MainView = grvResumeRawatInap_Transaksi
            grdResumeRawatInap_Transaksi.DataSource = ds.Tables("S_RINGKASANKELUARRAWATINAP")
            grdResumeRawatInap_Transaksi.ForceInitialize()

            SplashScreenManager.CloseForm(False)

            QyeryQueryResumeRawatInap_LoadFormatData()

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub QyeryQueryResumeRawatInap_LoadFormatData()
        For iLoop As Integer = 0 To grvResumeRawatInap_Transaksi.Columns.Count - 1
            If grvResumeRawatInap_Transaksi.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvResumeRawatInap_Transaksi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvResumeRawatInap_Transaksi.Columns(iLoop).DisplayFormat.FormatString = "{0:n0}"
                grvResumeRawatInap_Transaksi.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvResumeRawatInap_Transaksi.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvResumeRawatInap_Transaksi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvResumeRawatInap_Transaksi.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm}"
            End If
        Next

        grvResumeRawatInap_Transaksi.Columns("KDUSER").Caption = "User"

        grvResumeRawatInap_Transaksi.Columns("Kode").Visible = False
    End Sub
#End Region
#Region "Command Button"
    Private Sub btnResumeRawatInap_btnBack_Click(sender As Object, e As EventArgs) Handles btnResumeRawatInap_btnBack.Click
        pnlResumeRawatInap_pic.Dock = DockStyle.Top

        XtraTabList_SelectedPageChanged()
        lResumeRawatInap_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lResumeRawatInap_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lResumeRawatInap_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If Not frmRingkasanKeluar Is Nothing Then frmRingkasanKeluar.Dispose()
        frmRingkasanKeluar = Nothing
    End Sub
    Private Sub picResumeRawatInap_Add_Click(sender As Object, e As EventArgs) Handles picResumeRawatInap_Add.Click
        If lblKDKUNJUNGAN.Text = "" Then
            MsgBox("Silahkan Pilih Dulu Pasien", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim oDataGrouper As New Admission.clsPendaftaran_Kunjungan
        Dim ds = oDataGrouper.GetDatabyKodeKunjungan(lblKDKUNJUNGAN.Text)

        If ds IsNot Nothing Then
            Dim dsKunjungan = oDataGrouper.GetData(ds.KDKUNJUNGAN)
            If dsKunjungan IsNot Nothing Then

                Dim oDigital As New EMedrek.clsRingkasanKeluar

                Dim dsDigital = oDigital.GetData(dsKunjungan.KDPENDAFTARAN)

                If dsDigital IsNot Nothing Then
                    MsgBox("Resume Rawat Inap Sudah dibuatkan, Silahkan lakukan Edit untuk memperbaiki", MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                End If

                pnlResumeRawatInap_pic.Dock = DockStyle.None

                lResumeRawatInap_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lResumeRawatInap_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lResumeRawatInap_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                Try
                    ' Bersihkan konten panel
                    pnlResumeRawatInap_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmRingkasanKeluar.TopLevel = False
                    frmRingkasanKeluar.FormBorderStyle = FormBorderStyle.None
                    frmRingkasanKeluar.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlResumeRawatInap_Transaksi.Controls.Add(frmRingkasanKeluar)

                    ' Tampilkan form anak
                    frmRingkasanKeluar.LoadMe(FORM_MODE.FORM_MODE_ADD, dsKunjungan.KDPENDAFTARAN, ds.KDIDENTITAS)
                    frmRingkasanKeluar.Show()
                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                MsgBox("Kunjungan Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Else
            MsgBox("Identitas Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub picResumeRawatInap_Update_Click(sender As Object, e As EventArgs) Handles picResumeRawatInap_Update.Click
        If grvResumeRawatInap_Transaksi.GetFocusedRowCellValue("Kode") Is Nothing Then
            MsgBox("Silahkan Pilih Kode", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If grvResumeRawatInap_Transaksi.GetFocusedRowCellValue("KDUSER") <> sUserID Then
            MsgBox("Maap Tidak dapat Rubah Silahkan Hubungi User " & grvResumeRawatInap_Transaksi.GetFocusedRowCellValue("KDUSER"), MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        pnlResumeRawatInap_pic.Dock = DockStyle.None

        lResumeRawatInap_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lResumeRawatInap_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lResumeRawatInap_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

        Dim oDigital As New EMedrek.clsRingkasanKeluar

        Dim dsDigital = oDigital.GetData(grvResumeRawatInap_Transaksi.GetFocusedRowCellValue("Kode"))

        If dsDigital IsNot Nothing Then
            Try
                ' Bersihkan konten panel
                pnlResumeRawatInap_Transaksi.Controls.Clear()

                ' Set properti form anak
                frmRingkasanKeluar.TopLevel = False
                frmRingkasanKeluar.FormBorderStyle = FormBorderStyle.None
                frmRingkasanKeluar.Dock = DockStyle.Fill

                ' Tambahkan form anak ke panel
                pnlResumeRawatInap_Transaksi.Controls.Add(frmRingkasanKeluar)

                ' Tampilkan form anak
                frmRingkasanKeluar.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.KDREG, dsDigital.KDIDENTITAS)
                frmRingkasanKeluar.Show()
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Else
            MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub picResumeRawatInap_Delete_Click(sender As Object, e As EventArgs) Handles picResumeRawatInap_Delete.Click
        'If grvResumeRawatInap_Transaksi.GetFocusedRowCellValue("Kode") Is Nothing Then
        '    MsgBox("Silahkan Pilih Kode", MsgBoxStyle.Exclamation, Me.Text)
        '    Exit Sub
        'End If

        'If grvResumeRawatInap_Transaksi.GetFocusedRowCellValue("KDUSER") <> sUserID Then
        '    MsgBox("Maap Tidak dapat Rubah Silahkan Hubungi User " & grvResumeRawatInap_Transaksi.GetFocusedRowCellValue("KDUSER"), MsgBoxStyle.Exclamation, Me.Text)
        '    Exit Sub
        'End If

        'If MsgBox("Apakah Yakin Data Akan di Hapus", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        'Dim frmPesanDelete As New frmPesanDelete
        'frmPesanDelete.ShowDialog(Me)

        'If sPesanHapus <> "XXXXXBATALXXXXX" Then
        '    Dim oDelete As New Setting.clsDelete

        '    If oDelete.InsertData("ResumeRawatInap", sUserID, sPesanHapus & " " & Now.ToString("dd-MM-yyyy HH:mm:ss"), grvResumeRawatInap_Transaksi.GetFocusedRowCellValue("Kode")) = False Then
        '        'fn_DeleteData = False
        '    End If

        '    Dim oDigital As New EMedrek.clsRingkasanKeluar
        '    oDigital.UpdateDelete(grvResumeRawatInap_Transaksi.GetFocusedRowCellValue("Kode"), sUserID)
        '    XtraTabList_SelectedPageChanged()
        'End If
    End Sub
    Private Sub picResumeRawatInap_Refresh_Click(sender As Object, e As EventArgs) Handles picResumeRawatInap_Refresh.Click
        XtraTabList_SelectedPageChanged()
        lResumeRawatInap_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lResumeRawatInap_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lResumeRawatInap_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If Not frmRingkasanKeluar Is Nothing Then frmRingkasanKeluar.Dispose()
        frmRingkasanKeluar = Nothing
    End Sub
#End Region
#Region "Event"
#End Region
#End Region
#Region "Lembar Observasi"
#Region "Function"
    Private Sub QueryLembarObservasi_LoadHistory(ByVal NoRM As String)
        Try
            If sSplashScreen = False Then Exit Sub

            grvLembarObservasi_Transaksi.Columns.Clear()
            grdLembarObservasi_Transaksi.DataSource = Nothing
            grvLembarObservasi_Transaksi.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Processing data.....")

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\RME\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "Kode = A.KDLEMBAROBSERVASI "
            SQL &= ",Tanggal = A.DATE "
            SQL &= ",NoRegister = A.KDPENDAFTARAN "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_LEMBAROBSERVASI_H A "
            SQL &= "WHERE A.KDCUSTOMER = '" & NoRM & "' "
            SQL &= "AND A.ISDELETE = 0 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_LEMBAROBSERVASI_H")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grdLembarObservasi_Transaksi.MainView = grvLembarObservasi_Transaksi
            grdLembarObservasi_Transaksi.DataSource = ds.Tables("S_LEMBAROBSERVASI_H")
            grdLembarObservasi_Transaksi.ForceInitialize()

            SplashScreenManager.CloseForm(False)

            QyeryQueryLembarObservasi_LoadFormatData()

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub QyeryQueryLembarObservasi_LoadFormatData()
        For iLoop As Integer = 0 To grvLembarObservasi_Transaksi.Columns.Count - 1
            If grvLembarObservasi_Transaksi.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvLembarObservasi_Transaksi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvLembarObservasi_Transaksi.Columns(iLoop).DisplayFormat.FormatString = "{0:n0}"
                grvLembarObservasi_Transaksi.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvLembarObservasi_Transaksi.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvLembarObservasi_Transaksi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvLembarObservasi_Transaksi.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm}"
            End If
        Next

        grvLembarObservasi_Transaksi.Columns("Kode").Visible = False
        grvLembarObservasi_Transaksi.BestFitColumns()
    End Sub
#End Region
#Region "Command Button"
    Private Sub btnLembarObservasi_btnBack_Click(sender As Object, e As EventArgs) Handles btnLembarObservasi_btnBack.Click
        pnlLembarObservasi_pic.Dock = DockStyle.Top

        XtraTabList_SelectedPageChanged()
        lTransferIntrenal_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lTransferIntrenal_Pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lLembarObservasi_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If Not frmLembarObservasi Is Nothing Then frmLembarObservasi.Dispose()
        frmLembarObservasi = Nothing
    End Sub
    'Private Sub btnLembarObservasi_BukaTutupList_Click(sender As Object, e As EventArgs) Handles btnLembarObservasi_BukaTutupList.Click
    '    If btnLembarObservasi_BukaTutupList.Text = "Tutup" Then
    '        btnLembarObservasi_BukaTutupList.Text = "Buka"
    '        grdLembarObservasi_PDF.Visible = False
    '    Else
    '        btnLembarObservasi_BukaTutupList.Text = "Tutup"
    '        grdLembarObservasi_PDF.Visible = True
    '    End If
    'End Sub
    Private Sub picLembarObservasi_Add_Click(sender As Object, e As EventArgs) Handles picLembarObservasi_Add.Click
        If lblKDKUNJUNGAN.Text = "" Then
            MsgBox("Silahkan Pilih Dulu Pasien", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        Dim oDataGrouper As New Admission.clsPendaftaran_Kunjungan

        Dim ds = oDataGrouper.GetData(lblKDKUNJUNGAN.Text)

        If ds IsNot Nothing Then
            pnlLembarObservasi_pic.Dock = DockStyle.None

            lTransferIntrenal_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lTransferIntrenal_Pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lLembarObservasi_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            Try
                ' Bersihkan konten panel
                pnlLembarObservasi_Transaksi.Controls.Clear()

                ' Set properti form anak
                frmLembarObservasi.TopLevel = False
                frmLembarObservasi.FormBorderStyle = FormBorderStyle.None
                frmLembarObservasi.Dock = DockStyle.Fill

                ' Tambahkan form anak ke panel
                pnlLembarObservasi_Transaksi.Controls.Add(frmLembarObservasi)

                ' Tampilkan form anak
                frmLembarObservasi.LoadMe(FORM_MODE.FORM_MODE_ADD, ds.KDPENDAFTARAN, ds.S_PENDAFTARAN_H.KDCUSTOMER, ds.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY, ds.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR, IIf(ds.S_PENDAFTARAN_H.M_CUSTOMER.KDJENISKELAMIN = 1, "LAKI-LAKI", "PEREMPUAN"), ds.M_DEPARTMENT.NAME_DISPLAY, ds.KDDOCTOR, ds.S_PENDAFTARAN_H.KARTUBPJS, "")
                frmLembarObservasi.Show()
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub picLembarObservasi_Update_Click(sender As Object, e As EventArgs) Handles picLembarObservasi_Update.Click
        If grvLembarObservasi_Transaksi.GetFocusedRowCellValue("Kode") Is Nothing Then
            MsgBox("Silahkan Pilih Kode", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If grvLembarObservasi_Transaksi.GetFocusedRowCellValue("KDUSER") <> sUserID Then
            MsgBox("Maap Tidak dapat Rubah Silahkan Hubungi User " & grvLembarObservasi_Transaksi.GetFocusedRowCellValue("KDUSER"), MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        pnlLembarObservasi_pic.Dock = DockStyle.None

        lTransferIntrenal_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lTransferIntrenal_Pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lLembarObservasi_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

        Dim oDigital As New EMedrek.clsLembarObservasi

        Dim dsDigital = oDigital.GetData(grvLembarObservasi_Transaksi.GetFocusedRowCellValue("Kode"))

        If dsDigital IsNot Nothing Then
            Try
                ' Bersihkan konten panel
                pnlLembarObservasi_Transaksi.Controls.Clear()

                ' Set properti form anak
                frmLembarObservasi.TopLevel = False
                frmLembarObservasi.FormBorderStyle = FormBorderStyle.None
                frmLembarObservasi.Dock = DockStyle.Fill

                ' Tambahkan form anak ke panel
                pnlLembarObservasi_Transaksi.Controls.Add(frmLembarObservasi)

                ' Tampilkan form anak
                'frmLembarObservasi.LoadMe(FORM_MODE.FORM_MODE_EDIT, )
                frmLembarObservasi.Show()
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Else
            MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub picLembarObservasi_Delete_Click(sender As Object, e As EventArgs) Handles picLembarObservasi_Delete.Click
        If grvLembarObservasi_Transaksi.GetFocusedRowCellValue("Kode") Is Nothing Then
            MsgBox("Silahkan Pilih Kode", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If grvLembarObservasi_Transaksi.GetFocusedRowCellValue("KDUSER") <> sUserID Then
            MsgBox("Maap Tidak dapat Rubah Silahkan Hubungi User " & grvLembarObservasi_Transaksi.GetFocusedRowCellValue("KDUSER"), MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If MsgBox("Apakah Yakin Data Akan di Hapus", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        Dim frmPesanDelete As New frmPesanDelete
        frmPesanDelete.ShowDialog(Me)

        If sPesanHapus <> "XXXXXBATALXXXXX" Then
            Dim oDigital As New EMedrek.clsLembarObservasi
            oDigital.DeleteData(grvLembarObservasi_Transaksi.GetFocusedRowCellValue("Kode"), sUserID)
            XtraTabList_SelectedPageChanged()
        End If
    End Sub
    Private Sub picLembarObservasi_Refresh_Click(sender As Object, e As EventArgs) Handles picLembarObservasi_Refresh.Click
        XtraTabList_SelectedPageChanged()
        lTransferIntrenal_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lTransferIntrenal_Pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lLembarObservasi_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If Not frmLembarObservasi Is Nothing Then frmLembarObservasi.Dispose()
        frmLembarObservasi = Nothing
    End Sub
#End Region
#Region "Event"

#End Region
#End Region
#Region "Lembar Observasi"
#Region "Function"
    Private Sub QueryScan_LoadHistory(ByVal NoRM As String)
        Try
            If sSplashScreen = False Then Exit Sub

            grvScan_Transaksi.Columns.Clear()
            grdScan_Transaksi.DataSource = Nothing
            grvScan_Transaksi.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Processing data.....")

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
            SQL &= "Kode = A.KDPDF "
            SQL &= ",Tanggal = B.tglSep "
            SQL &= ",NoRegister = B.norec "
            SQL &= ",KDUSER = A.NOIDUSER "
            SQL &= "FROM "
            SQL &= "R_IDENTITAS_GROUPER_PDF A "
            SQL &= "INNER JOIN R_IDENTITAS_GROUPER B "
            SQL &= "ON A.kodegrouper = B.kodegrouper "
            SQL &= "WHERE B.noRm = '" & NoRM & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "R_IDENTITAS_GROUPER_PDF")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grdScan_Transaksi.MainView = grvScan_Transaksi
            grdScan_Transaksi.DataSource = ds.Tables("R_IDENTITAS_GROUPER_PDF")
            grdScan_Transaksi.ForceInitialize()

            SplashScreenManager.CloseForm(False)

            QyeryQueryScan_LoadFormatData()

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub QyeryQueryScan_LoadFormatData()
        For iLoop As Integer = 0 To grvScan_Transaksi.Columns.Count - 1
            If grvScan_Transaksi.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvScan_Transaksi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvScan_Transaksi.Columns(iLoop).DisplayFormat.FormatString = "{0:n0}"
                grvScan_Transaksi.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvScan_Transaksi.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvScan_Transaksi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvScan_Transaksi.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm}"
            End If
        Next

        grvScan_Transaksi.Columns("Kode").Visible = False
        grvScan_Transaksi.BestFitColumns()
    End Sub
#End Region
#Region "Command Button"
    Private Sub picScan_Add_Click(sender As Object, e As EventArgs) Handles picScan_Add.Click
        If lblKDKUNJUNGAN.Text = "" Then
            MsgBox("Silahkan Pilih Dulu Pasien", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        Dim oDataGrouper As New Admission.clsPendaftaran_Kunjungan
        Dim ds = oDataGrouper.GetData(lblKDKUNJUNGAN.Text)

        If ds IsNot Nothing Then
            Dim oGrouper As New Grouper.clsR_Identitas_Grouper

            Dim dsGrouer = oGrouper.GetDataByNoRec(ds.KDPENDAFTARAN)
            If dsGrouer IsNot Nothing Then
                Dim frmGrouperPDFList As New frmGrouperPDFList
                Try
                    frmGrouperPDFList.ShowDialog(Me)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                Finally
                    If Not frmGrouperPDFList Is Nothing Then frmGrouperPDFList.Dispose()
                    frmGrouperPDFList = Nothing
                End Try
            End If
        End If
    End Sub
    Private Sub btnCariPDFRincian_Click(sender As Object, e As EventArgs) Handles btnCariPDFRincian.Click
        If lblRM.Text = "" Then
            lblRM.Text = grv.GetFocusedRowCellValue("KDCUSTOMER")
        End If

        If lblRM.Text = "" Then
            MsgBox("Rekam Medis Pasien Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        fn_LoadIdentitas(grv.GetFocusedRowCellValue("KDKUNJUNGAN"))

        If XtraTabList.SelectedTabPageIndex = 0 Then
            fn_Empty()
        Else
            XtraTabControlPDF_SelectedPageChanged()
        End If
    End Sub
    Private Sub btnAdmision_Click(sender As Object, e As EventArgs) Handles btnAdmision.Click
        If grdDPJP.Text = "" Then
            MsgBox("Dokter Masih Kosong", MsgBoxStyle.Exclamation, Me.Text)
        Else
            Dim frmPendaftaranLangsung As New frmPendaftaranLangsung
            Try
                frmPendaftaranLangsung.LoadMe(grdDPJP.EditValue)
                frmPendaftaranLangsung.ShowDialog(Me)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmPendaftaranLangsung Is Nothing Then frmGrouperPDFList.Dispose()
                frmPendaftaranLangsung = Nothing

                fn_LoadSecurity()
            End Try
        End If
    End Sub
#End Region
#Region "Event"
#End Region
#End Region
#Region "Aksi"
#Region "Billing RJ/RI"
    Private Sub fn_LoadDataBilling(ByVal sKDPENDAFTARAN1 As String, ByVal sKDPENDAFTARAN2 As String)
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
            SQL &= "A.KDSOTRANSAKSI "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",M_DAFTAR_L1 = D.MEMO "
            SQL &= ",M_DAFTAR_L4 = E.MEMO "
            SQL &= ",C.KDPENDAFTARAN "
            SQL &= ",TANGGALDATANG = C.DATE "
            SQL &= ",Tujuan = F.NAME_DISPLAY "
            SQL &= ",KDCUSTOMER = C.KDCUSTOMER "
            SQL &= ",NAMA = G.NAME_DISPLAY "
            SQL &= ",A.SUBTOTAL "
            SQL &= ",A.DISCOUNT "
            SQL &= ",A.GRANDTOTAL "
            SQL &= ",A.MEMO "
            SQL &= ",Bayar = CASE WHEN A.GRANDTOTAL = 0 THEN CONVERT(BIT, 0) ELSE CASE WHEN A.PAYAMOUNT = A.GRANDTOTAL THEN CONVERT(BIT, 1) ELSE CONVERT(BIT, 0) END END "
            SQL &= ",KDKUNJUNGAN = A.KDKUNJUNGAN "
            SQL &= ",A.KDUSER "
            SQL &= ",A.CATEGORY "
            SQL &= "FROM S_SO_TRANSAKSI_H A "
            SQL &= "INNER JOIN S_PENDAFTARAN_KUNJUNGAN B "
            SQL &= "ON A.KDKUNJUNGAN = B.KDKUNJUNGAN "
            SQL &= "INNER JOIN S_PENDAFTARAN_H C "
            SQL &= "ON B.KDPENDAFTARAN = C.KDPENDAFTARAN "
            SQL &= "INNER JOIN M_DAFTAR_L1 D "
            SQL &= "ON C.KDDAFTAR_L1 = D.KDDAFTAR_L1 "
            SQL &= "INNER JOIN M_DAFTAR_L4 E "
            SQL &= "ON C.KDDAFTAR_L4 = E.KDDAFTAR_L4 "
            SQL &= "INNER JOIN M_DEPARTMENT F "
            SQL &= "ON B.KDDEPARTMENT = F.KDDEPARTMENT "
            SQL &= "INNER JOIN M_CUSTOMER G "
            SQL &= "ON C.KDCUSTOMER = G.KDCUSTOMER "
            SQL &= "WHERE "
            SQL &= "C.KDPENDAFTARAN = '" & sKDPENDAFTARAN1 & "' "
            SQL &= "OR C.KDPENDAFTARAN = '" & sKDPENDAFTARAN2 & "' "
            SQL &= "ORDER BY A.DATE, A.KDSOTRANSAKSI DESC"

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_SO_TRANSAKSI_H")

            grdBilling.MainView = grvBilling
            grdBilling.DataSource = ds.Tables("S_SO_TRANSAKSI_H")
            grdBilling.ForceInitialize()

            fn_LoadFormatData()
            fn_LoadLanguageMasterBilling()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatData()
        For iLoop As Integer = 0 To grvBilling.Columns.Count - 1
            If grvBilling.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvBilling.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvBilling.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grvBilling.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvBilling.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvBilling.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvBilling.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

        grvBilling.Columns("Bayar").VisibleIndex = -1
        grvBilling.Columns("KDKUNJUNGAN").VisibleIndex = -1
        grvBilling.Columns("CATEGORY").VisibleIndex = -1
    End Sub
    Private Sub grvbilling_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grvBilling.DoubleClick
        If grvBilling.GetFocusedRowCellValue("KDSOTRANSAKSI") Is Nothing Then
            Exit Sub
        End If

        Dim frmSalesOrderTransaksi As New frmSalesOrderTransaksi
        Try
            frmSalesOrderTransaksi.LoadMe(FORM_MODE.FORM_MODE_VIEW, grvBilling.GetFocusedRowCellValue("CATEGORY"), grvBilling.GetFocusedRowCellValue("KDSOTRANSAKSI"))
            frmSalesOrderTransaksi.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAksiBilling_Add_Click() Handles picAksiBilling_Add.Click
        Dim oKunjungan As New Admission.clsPendaftaran_Kunjungan

        Dim dsKunjungan = oKunjungan.GetData(lblKDKUNJUNGAN.Text)

        If dsKunjungan IsNot Nothing Then
            Dim frmSalesOrderTransaksi As New frmSalesOrderTransaksi
            Try
                frmSalesOrderTransaksi.LoadMe(FORM_MODE.FORM_MODE_ADD, dsKunjungan.S_PENDAFTARAN_H.CATEGORY)
                frmSalesOrderTransaksi.ShowDialog(Me)
                fn_LoadDataBilling(dsKunjungan.S_PENDAFTARAN_H.KDPENDAFTARAN, dsKunjungan.S_PENDAFTARAN_H.KDPENDAFTARAN_AWAL)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSalesOrderTransaksi Is Nothing Then frmSalesOrderTransaksi.Dispose()
                frmSalesOrderTransaksi = Nothing

                Dim rowHandle As Integer = grvBilling.LocateByValue(rowHandle, grvBilling.Columns("KDSOTRANSAKSI"), sCode)
                If rowHandle > 0 Then grvBilling.FocusedRowHandle = rowHandle
            End Try
        Else
            MsgBox("Tidak Ada Kunjungan", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub picAksiBilling_Update_Click() Handles picAksiBilling_Update.Click
        If grvBilling.GetFocusedRowCellValue("KDSOTRANSAKSI") Is Nothing Then
            Exit Sub
        End If

        If grvBilling.GetFocusedRowCellValue("CATEGORY") = 0 Then

        ElseIf grvBilling.GetFocusedRowCellValue("CATEGORY") = 1 Then

        Else
            MsgBox("Maap Kode Ini merupakan Billing Penunjang", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If CBool(grvBilling.GetFocusedRowCellValue("Bayar")) = True Then
            MsgBox("Sudah Bayar", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        Else
            Dim oKunjungan As New Admission.clsPendaftaran_Kunjungan

            Dim dsKunjungan = oKunjungan.GetData(lblKDKUNJUNGAN.Text)

            If dsKunjungan IsNot Nothing Then
                Dim oCashin As New Finance.clsCashIn
                Dim frmSalesOrderTransaksi As New frmSalesOrderTransaksi
                Try
                    frmSalesOrderTransaksi.LoadMe(FORM_MODE.FORM_MODE_EDIT, grvBilling.GetFocusedRowCellValue("CATEGORY"), grvBilling.GetFocusedRowCellValue("KDSOTRANSAKSI"))
                    frmSalesOrderTransaksi.ShowDialog(Me)
                    fn_LoadDataBilling(dsKunjungan.S_PENDAFTARAN_H.KDPENDAFTARAN, dsKunjungan.S_PENDAFTARAN_H.KDPENDAFTARAN_AWAL)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                Finally
                    If Not frmSalesOrderTransaksi Is Nothing Then frmSalesOrderTransaksi.Dispose()
                    frmSalesOrderTransaksi = Nothing

                    Dim rowHandle As Integer = grvBilling.LocateByValue(rowHandle, grvBilling.Columns("KDSOTRANSAKSI"), sCode)
                    If rowHandle > 0 Then grvBilling.FocusedRowHandle = rowHandle
                End Try
            Else
                MsgBox("Tidak Ada Kunjungan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        End If
    End Sub
    Private Sub picAksiBilling_Delete_Click() Handles picAksiBilling_Delete.Click
        Dim oKunjungan As New Admission.clsPendaftaran_Kunjungan

        Dim dsKunjungan = oKunjungan.GetData(lblKDKUNJUNGAN.Text)

        If dsKunjungan IsNot Nothing Then
            If grvBilling.GetFocusedRowCellValue("CATEGORY") = 0 Then

            ElseIf grvBilling.GetFocusedRowCellValue("CATEGORY") = 2 Then

            Else
                MsgBox("Maap Kode Ini merupakan Billing Penunjang", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            If grvBilling.GetFocusedRowCellValue("KDSOTRANSAKSI") Is Nothing Then
                Exit Sub
            End If
            If CBool(grvBilling.GetFocusedRowCellValue("Bayar")) = True Then
                MsgBox("Sudah Bayar", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            If dsKunjungan.S_PENDAFTARAN_H.CATEGORY = 0 Then
                If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

                If fn_DeleteDataBilling(grvBilling.GetFocusedRowCellValue("KDSOTRANSAKSI")) = False Then
                    MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                End If

                MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
                fn_LoadDataBilling(dsKunjungan.S_PENDAFTARAN_H.KDPENDAFTARAN, dsKunjungan.S_PENDAFTARAN_H.KDPENDAFTARAN_AWAL)
            ElseIf dsKunjungan.S_PENDAFTARAN_H.CATEGORY = 1 Then
                If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

                If fn_DeleteDataBilling(grvBilling.GetFocusedRowCellValue("KDSOTRANSAKSI")) = False Then
                    MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                End If

                MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
                fn_LoadDataBilling(dsKunjungan.S_PENDAFTARAN_H.KDPENDAFTARAN, dsKunjungan.S_PENDAFTARAN_H.KDPENDAFTARAN_AWAL)
            Else
                MsgBox("Bukan Billing Rawat Jalan atau Rawat Inap", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Else
            MsgBox("Tidak Ada Kunjungan", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Function fn_DeleteDataBilling(ByVal sKDSOTRANSAKSI As String) As Boolean
        Try
            Dim oSalesOrderTransaksi As New Sales.clsSalesOrderTransaksi

            Dim dsTotal = oSalesOrderTransaksi.GetData(sKDSOTRANSAKSI)
            Dim Total As String = "0"

            If dsTotal IsNot Nothing Then
                Total = dsTotal.GRANDTOTAL
            End If

            Dim oDelete As New Setting.clsDelete
            Dim frmPesanDelete As New frmPesanDelete
            frmPesanDelete.ShowDialog(Me)

            If sPesanHapus <> "XXXXXBATALXXXXX" Then
                oSalesOrderTransaksi.DeleteData(sKDSOTRANSAKSI, sUserID)

                If oDelete.InsertData("BILLING", sUserID, sPesanHapus, sKDSOTRANSAKSI & " : " & Total) = False Then
                    'fn_DeleteData = False
                End If

                fn_DeleteDataBilling = True

            Else
                fn_DeleteDataBilling = False
            End If
        Catch oErr As Exception
            fn_DeleteDataBilling = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub picAksiBilling_Refresh_Click() Handles picAksiBilling_Refresh.Click
        Dim oKunjungan As New Admission.clsPendaftaran_Kunjungan

        Dim dsKunjungan = oKunjungan.GetData(lblKDKUNJUNGAN.Text)

        If dsKunjungan IsNot Nothing Then
            fn_LoadDataBilling(dsKunjungan.S_PENDAFTARAN_H.KDPENDAFTARAN, dsKunjungan.S_PENDAFTARAN_H.KDPENDAFTARAN_AWAL)
        Else
            MsgBox("Tidak Ada Kunjungan", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Public Sub fn_LoadLanguageMasterBilling()
        Try
            grvBilling.Columns("KDSOTRANSAKSI").Caption = SalesOrderTransaksi.KDSOTRANSAKSI
            grvBilling.Columns("TANGGAL").Caption = SalesOrderTransaksi.TANGGAL
            grvBilling.Columns("TANGGALDATANG").Caption = Pendaftaran.TANGGAL
            grvBilling.Columns("KDPENDAFTARAN").Caption = Pendaftaran.KDPENDAFTARAN
            grvBilling.Columns("KDCUSTOMER").Caption = "No Medrek"

            grvBilling.Columns("NAMA").Caption = SalesOrderTransaksi.KDPENDAFTARAN
            grvBilling.Columns("SUBTOTAL").Caption = SalesOrderTransaksi.SUBTOTAL
            grvBilling.Columns("DISCOUNT").Caption = SalesOrderTransaksi.DISCOUNT
            grvBilling.Columns("GRANDTOTAL").Caption = SalesOrderTransaksi.GRANDTOTAL
            grvBilling.Columns("MEMO").Caption = SalesOrderTransaksi.MEMO
            grvBilling.Columns("KDUSER").Caption = Caption.User
            grvBilling.Columns("M_DAFTAR_L1").Caption = sDaftar_L1
            grvBilling.Columns("M_DAFTAR_L4").Caption = sDaftar_L4
        Catch oErr As Exception

        End Try
    End Sub

#End Region
#End Region
End Class