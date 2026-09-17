Imports UI.WIN.MAIN.My.Resources
Imports DataAccess
Imports System.Linq
Imports System.Data.SqlClient
Imports Newtonsoft.Json.Linq
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports DevExpress.XtraSplashScreen
'Imports System.Drawing
'Imports System.Web.UI.WebControls
Imports MySql.Data.MySqlClient
Imports System.Text.RegularExpressions
Imports System.Net
'Imports System.Globalization

Public Class frmMedrekRawatNew2JalanList
    Private isLoad As Boolean = False
    Private isLoad_Splash As Boolean = False
    Private sisDOkter As Boolean = False
    Private oFormModeCPPT As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoidSimpan As String = String.Empty
    Private oGrouperDataCppt As New Grouper.clsR_CPPT
    Private sKDPENDAFTARAN As String = String.Empty
    Private sKODEBED As String = String.Empty
    Private sCategoryBilling As Integer = 0
    Private sKelas As String = String.Empty
    Private oRME2 As New RME.clsRME
    Private oPendaftaranPDF As New Admission.clsPendaftaranPDF
    Private sSplashScreen As Boolean = False
    Private FolderSimpan = "C:/Source/RME/"
    Private sKDDPJPUSER As String = String.Empty
    Private listTindakanLab_LoadData As New List(Of String)
    Private listTindakanRad_LoadData As New List(Of String)
    Private sPenunjnag As String = ""
    Private sUPDATETRANSAKSI As Boolean = False
    Private oPendaftaran As New Admission.clsPendaftaran
    Private oSalesOrderTransaksi As New Sales.clsSalesOrderTransaksi
    Private oOrder As New Digital.clsR_Order
    Private sAutoOrderObat As Boolean = True

#Region "Scrool"
    'Private Sub Form1_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseWheel
    '    If e.Delta > 0 Then
    '        Trace.WriteLine("Scrolled up!")
    '        fn_ScrollPage(True)
    '    Else
    '        Trace.WriteLine("Scrolled down!")
    '        fn_ScrollPage(False)
    '    End If
    'End Sub
    Private Sub frmRawatInapList_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
        If e.Delta > 0 Then
            'up
            fn_ScrollPage(True)
        Else
            'down
            fn_ScrollPage(False)
        End If
    End Sub
    Private Sub fn_ScrollPage(ByVal isUp As Boolean)
        Dim myView As Point = Me.Panel1.AutoScrollPosition

        Dim scrollchange As Integer = 50

        If isUp Then
            'up
            myView.X = -myView.X
            myView.Y = -scrollchange - myView.Y
        Else
            'down
            myView.X = -myView.X
            myView.Y = scrollchange - myView.Y

        End If

        Me.Panel1.AutoScrollPosition = myView
    End Sub
#End Region

#Region "Function"
    Public Sub fn_LoadMe(ByVal isDOKTER As Boolean)
        sisDOkter = isDOKTER
        fn_LoadDEPARTMENT(False)
        fn_LoadDiagnosa()
        Dim oUser As New Setting.clsUser
        Dim oDoctor As New Reference.clsDoctor
        Dim dsUser = oUser.GetData(sUserID)
        If dsUser IsNot Nothing Then
            Dim dsDoctor = oDoctor.GetData(dsUser.KDDOCTOR)
            If dsDoctor IsNot Nothing Then
                grdKDDEPARTMENT.EditValue = dsDoctor.KDDEPARTMENT
                fn_LoadDokter(dsUser.KDDOCTOR)
                sKDDPJPUSER = dsUser.KDDOCTOR
            End If
        End If
        fn_LoadFormulirMedisLoad()

    End Sub
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'lCPPT_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        'lCPPT_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        'lCPPT_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        'lGrdLaporanOperasi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        'lPanelLaporanOperasi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        'lLaporanOperasi_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        'lTransferIntrenal_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        'lTransferIntrenal_Pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        'lTransferInternal_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        'lTindakanEvaluasi_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        'lTindakanEvaluasi_Pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        'lTindakanEvaluasi_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        'lGerdQ_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        'lGerdQ_Pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        'lGerdQ_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        lAsesmenPerawat_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lAsesmenPerawat_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lAsesmenKeperawatan_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        lPanelAsesmenMedis_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lPanelAsesmenMedis_Panel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lAsesmenMedis_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        'lRencanaOperasi_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        'lRencanaOperasi_Pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        'lRencanaOperasi_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        'lLaporanTindakan_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        'lLaporanTindakan_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        'lLaporanTindakan_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        'lResumeRawatInap_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        'lResumeRawatInap_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        'lResumeRawatInap_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If sMySQL_Url = "" Then
            chkCek.Checked = True
        End If

        deDATE.DateTime = Now
        deDateTo.DateTime = Now
        deTANGGALDATANG.DateTime = Now
        deDATETANGGALLAHIR.ResetText()
        fn_LoadSecurity()
        lGROUPFORM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lLISTANTRIAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        'lIDENTITASPASIEN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        'lLISTANTRIANCHEK.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        If btnType.Text = "Rawat Jalan" Then
            If sisDOkter = False Then
                btnBataldanDeleteSEP.Visible = True
            Else
                btnBataldanDeleteSEP.Visible = False
            End If
        Else
            btnBataldanDeleteSEP.Visible = False
        End If


        If Not Directory.Exists(FolderSimpan) Then
            Directory.CreateDirectory(FolderSimpan)
        Else
            Dim oRME As New RME.clsRME
            oRME.DeleteDirectory(FolderSimpan)
            Directory.CreateDirectory(FolderSimpan)
        End If

        isLoad_Splash = True
        sSplashScreen = True

    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_LoadSecurity()
        Try
            Dim oOtority As New Setting.clsOtority
            Dim oUser As New Setting.clsUser

            Dim ds = (From x In oOtority.GetDataDetail
                      Join y In oUser.GetData
                      On x.KDOTORITY Equals y.KDOTORITY
                      Where x.MODUL = "MEDREK_RJ" _
                      And y.KDUSER = sUserID
                      Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                If ds.ISVIEW = True Then
                    fn_EmptyMe()
                    fn_LoadView()

                    fn_LoadDatPasien(grdKDDEPARTMENT.EditValue, grdDPJP.EditValue)

                    If grdDPJP.Text <> "" Then
                        Dim oDoctor As New Reference.clsDoctor
                        Dim dsDoctor = oDoctor.GetData(grdDPJP.EditValue)
                        If dsDoctor IsNot Nothing Then
                            lblSIP.Text = dsDoctor.SIP
                            'fn_LoadJadwalDokter(dsDoctor.KDDOCTOR)
                        End If
                    End If
                End If
            Catch ex As Exception
                MsgBox("Keamanan belum dipasang, tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
                btnRefreshListPasien.Enabled = False
            End Try


            Dim dsUpdate = (From x In oOtority.GetDataDetail
                            Join y In oUser.GetData
                            On x.KDOTORITY Equals y.KDOTORITY
                            Where x.MODUL = "UPDATETRANSAKSI" _
                            And y.KDUSER = sUserID
                            Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            If dsUpdate IsNot Nothing Then
                sUPDATETRANSAKSI = True
            Else
                sUPDATETRANSAKSI = False
            End If
        Catch ex As Exception
            MsgBox("Load Security : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadJadwalDokter(ByVal Kodedokter As String)
        Try
            Dim hari = ""
            Select Case deDATE.DateTime.ToString("dddd")
                Case "Sunday"
                    hari = "MINGGU"
                Case "Monday"
                    hari = "SENIN"
                Case "Tuesday"
                    hari = "SELASA"
                Case "Wednesday"
                    hari = "RABU"
                Case "Thursday"
                    hari = "KAMIS"
                Case "Friday"
                    hari = "JUMAT"
                Case "Saturday"
                    hari = "SABTU"
                Case Else
                    hari = deDATE.DateTime.ToString("dddd").ToUpper
            End Select

            Dim oDoctor As New Reference.clsDoctor
            Dim dsDoctorJadwal = oDoctor.GetDataJadwalDokter(Kodedokter, hari)
            If dsDoctorJadwal IsNot Nothing Then
                lblJadwalSIP.Text = dsDoctorJadwal.BUKA & "-" & dsDoctorJadwal.TUTUP
            Else
                lblJadwalSIP.Text = "-"
            End If
        Catch ex As Exception
            MsgBox("Jadwal Dokter : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadView()
        'Dim oDepartment As New Reference.clsDepartment
        'Dim dsDepartment1 = oDepartment.GetData(grdKDDEPARTMENT.EditValue)

        'If dsDepartment1 IsNot Nothing Then
        '    If dsDepartment1.VCLAIM_KODEPOLI = "IGD" Then
        '        AddToolStripMenuItem.Visible = False
        '        LaporanOperasiToolStripMenuItem.Visible = False
        '        LaporanTindakanToolStripMenuItem.Visible = False
        '        PemeriksaanAwalToolStripMenuItem.Visible = False
        '        tabInformasiObat.PageVisible = False
        '        tabResume.PageVisible = False

        '        If sisDOkter = False Then
        '            AsesmenAwalPerawatIGDToolStripMenuItem.Visible = False
        '            AsesmenMedisIGDToolStripMenuItem.Visible = False
        '            tabAsesmenMedis.Visible = False
        '        Else
        '            AsesmenAwalPerawatIGDToolStripMenuItem.Visible = False
        '            AsesmenMedisIGDToolStripMenuItem.Visible = False
        '            tabAsesmenMedis.Visible = True
        '        End If
        '    Else
        '        AsesmenAwalPerawatIGDToolStripMenuItem.Visible = False
        '        AsesmenMedisIGDToolStripMenuItem.Visible = False
        '        tabAsesmenMedis.Visible = False

        '        If sisDOkter = False Then
        '            LaporanOperasiToolStripMenuItem.Visible = False
        '            LaporanTindakanToolStripMenuItem.Visible = False
        '            AddToolStripMenuItem.Visible = False
        '            PemeriksaanAwalToolStripMenuItem.Visible = True
        '            tabInformasiObat.PageVisible = False
        '            tabResume.PageVisible = False
        '        Else
        '            LaporanOperasiToolStripMenuItem.Visible = False
        '            LaporanTindakanToolStripMenuItem.Visible = False
        '            AddToolStripMenuItem.Visible = True
        '            PemeriksaanAwalToolStripMenuItem.Visible = False
        '            tabInformasiObat.PageVisible = True
        '            tabResume.PageVisible = True
        '        End If
        '    End If
        'End If

        AsesmenAwalPerawatIGDToolStripMenuItem.Visible = False
        AsesmenMedisIGDToolStripMenuItem.Visible = False
        tabAsesmenMedis.Visible = False
        CPPTRawatInapToolStripMenuItem.Visible = False

        If sisDOkter = False Then
            LaporanOperasiToolStripMenuItem.Visible = False
            LaporanTindakanToolStripMenuItem.Visible = False
            AddToolStripMenuItem.Visible = False
            PemeriksaanAwalToolStripMenuItem.Visible = True
            tabInformasiObat.PageVisible = False
            tabResume.PageVisible = False
            'tabDokumenAksiResep.PageVisible = False
        Else
            LaporanOperasiToolStripMenuItem.Visible = False
            LaporanTindakanToolStripMenuItem.Visible = False
            AddToolStripMenuItem.Visible = True
            PemeriksaanAwalToolStripMenuItem.Visible = False
            tabInformasiObat.PageVisible = True
            tabResume.PageVisible = True

            'If btnType.Text = "Rawat Jalan" Then
            '    If grdKDDEPARTMENT.Text.Contains("IGD") Then
            '        tabDokumenAksiResep.PageVisible = True
            '    ElseIf grdKDDEPARTMENT.Text.Contains("DARURAT") Then
            '        tabDokumenAksiResep.PageVisible = True
            '    ElseIf grdKDDEPARTMENT.Text.Contains("UGD") Then
            '        tabDokumenAksiResep.PageVisible = True
            '    Else
            '        tabDokumenAksiResep.PageVisible = False
            '    End If
            'Else
            '    tabDokumenAksiResep.PageVisible = True
            'End If
        End If
    End Sub
    Private Sub fn_EmptyMe()
        txtKDKUNJUNGAN.ResetText()

        sKDPENDAFTARAN = ""
        sCategoryBilling = 0
        sKelas = ""
        sKODEBED = ""

        'picGAMBAR2.Image = CType(My.Resources.ResourceManager.GetObject("image1"), Image)
        picGAMBAR2.Image = My.Resources.ResourceManager.GetObject("image1")
        sPicture = Nothing

        deDATECPPT.DateTime = Now
        grvHystori.OptionsSelection.MultiSelect = True
        grvHystori.SelectAll()
        grvHystori.DeleteSelectedRows()
        grvHystori.OptionsSelection.MultiSelect = False

        grvResume_List.Columns.Clear()
        grdResume_List.DataSource = Nothing
        grvResume_List.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

        grvUpload.OptionsSelection.MultiSelect = True
        grvUpload.SelectAll()
        grvUpload.DeleteSelectedRows()
        grvUpload.OptionsSelection.MultiSelect = False

        PdfViewerHasilPenunjang.CloseDocument()
        PdfViewerCPPTRawatJalan.CloseDocument()
        PdfViewerResume.CloseDocument()
        PdfViewerAsesmenMedis.CloseDocument()
        PdfViewerAsesmenNakes.CloseDocument()
        PdfViewerCPPTRawatInap.CloseDocument()
        PdfViewerAssemenAwalMedisRawatInap.CloseDocument()
        PdfViewerLaporanOperasi.CloseDocument()
        PdfViewerTransferInternal.CloseDocument()

        txtKDCUSTOMER.ResetText()
        deDATETANGGALLAHIR.DateTime = Now
        deTANGGALDATANG.DateTime = Now
        txtNAMAPASIEN.ResetText()
        txtNOMORANTRIAN.ResetText()
        txtOBJEKTIF_JENISKELAMIN.ResetText()
        'txtDIAGNOOSA.ResetText()
        txtOBJEKTIF_UMUR.ResetText()
        txtPenjamin.ResetText()
        txtKARTUBPJS.ResetText()
        txtKODEBOOKING.ResetText()
        txtNOMORSEP.ResetText()
        cboTINGKATKESADARAN.ResetText()
        txtOBJEKTIF_IMT.ResetText()
        txtOBJEKTIF_BBIDEAL.ResetText()
        txtOBJEKTIF_BBDITURUNKAN.ResetText()

        chkALERGI_YA.Checked = False
        chkALERGI_TIDAK.Checked = True
        txtALERGIOBAT.ResetText()
        txtNAMAPASIEN.ResetText()
        deDATETANGGALLAHIR.ResetText()
        txtKDCUSTOMER.ResetText()
        txtSUBJEKTIF.ResetText()
        txtOBJEKTIF_JENISKELAMIN.ResetText()
        txtOBJEKTIF_UMUR.ResetText()
        txtOBJEKTIF_BERATBADAN.ResetText()
        txtOBJEKTIF_TINGGIBADAN.ResetText()
        txtCPPT_Diastole.Text = 0
        txtCPPT_Sistole.Text = 0
        txtOBJEKTIF_NADI.ResetText()
        txtOBJEKTIF_RESPIRASI.ResetText()
        txtOBJEKTIF_SATURASIOKSIGEN.ResetText()
        txtOBJEKTIF_SUHU.ResetText()

        txtGOALOFTREATMENT.ResetText()
        txtEDUKASI.ResetText()
        txtFREKUENSIKUNJUNGAN.ResetText()

        txtTINDAKLANJUT.ResetText()
        lblGRANDTOTAL.Text = 0
        lblGRANDTOTAL_TINDAKAN.Text = 0
        lblTarifRS.Text = 0
        lblTarifObatKronis.Text = 0
        lblTarifPaket.Text = 0
        lblTarifInacbg.Text = FormatNumber(sInacbgTarifRawatJalanDefault, 0)
        lblSelisih.Text = 0

        grvDetailResep.OptionsSelection.MultiSelect = True
        grvDetailResep.SelectAll()
        grvDetailResep.DeleteSelectedRows()
        grvDetailResep.OptionsSelection.MultiSelect = False

        grvOBATRACIKAN.OptionsSelection.MultiSelect = True
        grvOBATRACIKAN.SelectAll()
        grvOBATRACIKAN.DeleteSelectedRows()
        grvOBATRACIKAN.OptionsSelection.MultiSelect = False

        'grvTindakan.OptionsSelection.MultiSelect = True
        'grvTindakan.SelectAll()
        'grvTindakan.DeleteSelectedRows()
        'grvTindakan.OptionsSelection.MultiSelect = False

        grvCPPT_Tindakan.OptionsSelection.MultiSelect = True
        grvCPPT_Tindakan.SelectAll()
        grvCPPT_Tindakan.DeleteSelectedRows()
        grvCPPT_Tindakan.OptionsSelection.MultiSelect = False

        grvDiagnosaPenyerta.OptionsSelection.MultiSelect = True
        grvDiagnosaPenyerta.SelectAll()
        grvDiagnosaPenyerta.DeleteSelectedRows()
        grvDiagnosaPenyerta.OptionsSelection.MultiSelect = False

        'txtDIAGNOOSA.ResetText()
        txtDESKRIPSI.ResetText()


        fn_LoadDocument(txtKDKUNJUNGAN.Text, txtKDCUSTOMER.Text)

    End Sub
    Private Sub fn_LoadDiagnosa()
        Try
            grdCariDiagnosaiDRG.Properties.DataSource = ListDiagnosaiDRG
            grdCariDiagnosaiDRG.Properties.ValueMember = "KDDIAGNOSA"
            grdCariDiagnosaiDRG.Properties.DisplayMember = "MEMO"

            'grdCariDiagnosa.Properties.DataSource = ListDiagnosaiNACBG
            'grdCariDiagnosa.Properties.ValueMember = "KDDIAGNOSA"
            'grdCariDiagnosa.Properties.DisplayMember = "MEMO"

            'grdDIAGNOSA_V6.Properties.DataSource = ListDiagnosaiNACBG
            'grdDIAGNOSA_V6.Properties.ValueMember = "KDDIAGNOSA"
            'grdDIAGNOSA_V6.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox("Load Diagnosa" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDataList()
        Try
            grvListantrian.OptionsSelection.MultiSelect = True
            grvListantrian.SelectAll()
            grvListantrian.DeleteSelectedRows()
            grvListantrian.OptionsSelection.MultiSelect = False

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
            SQL &= "* "
            SQL &= "FROM ( "
            SQL &= "SELECT "
            SQL &= "B.KDPENDAFTARAN "
            SQL &= ",AntrianDokter = B.KDBOOKING "
            SQL &= ",Pasien = D.NAME_DISPLAY "
            SQL &= ",Cek = CASE B.KDDAFTAR_L6 WHEN 'DAFTAR_L6_0000000003' THEN CONVERT(BIT, 1) ELSE CONVERT(BIT, 0) END "
            SQL &= ",Keterangan = ISNULL((SELECT TINDAKLANJUT + ' ' + ALASAN + ' ' + DESCRIPTION FROM S_PENDAFTARAN_SKD WHERE B.KDPENDAFTARAN = KDPENDAFTARAN), '') "
            SQL &= ",ISPERAWAT = CASE B.KDDAFTAR_L6 WHEN 'DAFTAR_L6_0000000002' THEN CONVERT(BIT, 1) ELSE CONVERT(BIT, 0) END "
            SQL &= ",KDDAFTAR_L6 = CASE B.KDDAFTAR_L6 WHEN 'DAFTAR_L6_0000000001' THEN 'X' WHEN 'DAFTAR_L6_0000000002' THEN 'Y' ELSE 'Z' END "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_KUNJUNGAN A "
            SQL &= "INNER JOIN S_PENDAFTARAN_H B "
            SQL &= "ON A.KDPENDAFTARAN = B.KDPENDAFTARAN "
            SQL &= "INNER JOIN M_DOCTOR C "
            SQL &= "ON A.KDDOCTOR = C.KDDOCTOR "
            SQL &= "INNER JOIN M_CUSTOMER D "
            SQL &= "ON B.KDCUSTOMER = D.KDCUSTOMER "
            SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) = '" & deDATE.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND A.KDDEPARTMENT = '" & grdKDDEPARTMENT.EditValue & "' "
            SQL &= "AND A.KDDOCTOR = '" & grdDPJP.EditValue & "' "
            SQL &= "AND B.STATUSDAFTAR <> '3' "
            SQL &= "AND B.CATEGORY = 0 "
            SQL &= ") Z "
            SQL &= "ORDER BY Z.KDDAFTAR_L6, Z.KDPENDAFTARAN ASC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_LISTPASIEN_ANTRIAN")

            grdListantrian.DataSource = ds.Tables("S_LISTPASIEN_ANTRIAN")
            grdListantrian.ForceInitialize()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            'fn_LoadFormatData()

            grvListantrian.Columns("Cek").VisibleIndex = -1
            grvListantrian.Columns("Keterangan").VisibleIndex = -1
            grvListantrian.Columns("ISPERAWAT").VisibleIndex = -1
            grvListantrian.Columns("KDDAFTAR_L6").VisibleIndex = -1
            grvListantrian.Columns("KDPENDAFTARAN").VisibleIndex = -1

            grvListantrian.BestFitColumns()

        Catch oErr As Exception
            MsgBox("Load List Pasien Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDatPasien(ByVal KDDEPARTMENT As String, ByVal KDDOCTOR As String)
        Try
            If sSplashScreen = False Then Exit Sub

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing Data Pasien.....")

            grvListPasien.OptionsSelection.MultiSelect = True
            grvListPasien.SelectAll()
            grvListPasien.DeleteSelectedRows()
            grvListPasien.OptionsSelection.MultiSelect = False

            If btnType.Text = "Rawat Jalan" Then
                If grdKDDEPARTMENT.Text.Contains("GIZI") Then
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
                    SQL &= "* "
                    SQL &= "FROM ( "
                    SQL &= "SELECT "
                    SQL &= "B.KDPENDAFTARAN "
                    SQL &= ",NoRegister = A.KDKUNJUNGAN "
                    SQL &= ",Antrian = B.KDBOOKING "
                    SQL &= ",KodeBooking = B.KODEBOOKING "
                    SQL &= ",Pasien = D.NAME_DISPLAY "
                    SQL &= ",Dokter = C.NAME_DISPLAY "
                    SQL &= ",SIP = C.SIP "
                    SQL &= ",B.KDCUSTOMER "
                    SQL &= ",USIA = CONVERT(nvarchar(50), DATEDIFF(YEAR, D.TANGGALLAHIR, B.DATE)) + ' Tahun, ' + CONVERT(nvarchar(50), DATEDIFF(MONTH, D.TANGGALLAHIR, B.DATE) - (DATEDIFF(YEAR, D.TANGGALLAHIR, B.DATE) * 12)) + ' Bulan, ' + CONVERT(nvarchar(50), DATEDIFF(DAY, DATEADD(MONTH, DATEDIFF(MONTH, D.TANGGALLAHIR, B.DATE), D.TANGGALLAHIR), B.DATE)) + ' Hari' "
                    SQL &= ",D.TANGGALLAHIR "
                    SQL &= ",B.KDDIAGNOSA "
                    SQL &= ",DIAGNOSA = E.MEMO "
                    SQL &= ",B.DATE "
                    SQL &= ",WaktuPeriksa = ISNULL((SELECT FORMAT(DATE, 'HH:mm:ss') FROM SET_WAKTUTUNGGU WHERE B.KODEBOOKING = KODEBOOKING AND ISPANGGIL = 4) ,'')  "
                    SQL &= ",WaktuSelisih = ISNULL((SELECT  DATEDIFF(MINUTE, B.DATE, DATE) FROM SET_WAKTUTUNGGU WHERE B.KODEBOOKING = KODEBOOKING AND ISPANGGIL = 4) ,'') "
                    SQL &= ",PENJAMIN = F.MEMO "
                    SQL &= ",B.NOMORSEP "
                    SQL &= ",B.KARTUBPJS "
                    SQL &= ",Cek = CASE B.KDDAFTAR_L6 WHEN 'DAFTAR_L6_0000000003' THEN CONVERT(BIT, 1) ELSE CONVERT(BIT, 0) END "
                    SQL &= ",Cek2 = B.KDDAFTAR_L6 "
                    SQL &= ",C.KDDOCTOR "
                    'IIf(grvListPasien.GetFocusedRowCellValue("JK") = "1", "Laki-laki", "Perempuan")
                    SQL &= ",JK = D.KDJENISKELAMIN "
                    SQL &= ",ISPERAWAT = CASE B.KDDAFTAR_L6 WHEN 'DAFTAR_L6_0000000002' THEN CONVERT(BIT, 1) ELSE CONVERT(BIT, 0) END "
                    SQL &= ",Keterangan = ISNULL((SELECT STRING_AGG(AA.PLANNING_ALASAN,', ') FROM DATABASERME..R_CPPT AA INNER JOIN DATABASERME..A_IDENTITASPASIEN_LIST BB ON AA.KDIDENTITAS = BB.KDIDENTITAS WHERE BB.KDKUNJUNGAN = A.KDKUNJUNGAN AND AA.KDPROFESI = 'PROFESI_0000000001'), '') "

                    'If chkCek.Checked = False Then
                    '    SQL &= ",Keterangan = ISNULL((SELECT STRING_AGG(TINDAKLANJUT + ' ' + ALASAN + ' ' + DESCRIPTION,', ') FROM S_PENDAFTARAN_SKD WHERE B.KDPENDAFTARAN = KDPENDAFTARAN GROUP BY KDPENDAFTARAN), '') "
                    'Else
                    '    SQL &= ",Keterangan = ISNULL((SELECT TOP 1 TINDAKLANJUT + ' ' + ALASAN + ' ' + DESCRIPTION FROM S_PENDAFTARAN_SKD WHERE B.KDPENDAFTARAN = KDPENDAFTARAN), '') "
                    'End If
                    SQL &= ",KDDAFTAR_L6 = CASE B.KDDAFTAR_L6 WHEN 'DAFTAR_L6_0000000001' THEN 'X' WHEN 'DAFTAR_L6_0000000002' THEN 'Y' ELSE 'Z' END "
                    SQL &= ",B.KDKELASRAWAT "
                    SQL &= ",B.CATEGORY "
                    SQL &= "FROM "
                    SQL &= "S_PENDAFTARAN_KUNJUNGAN A "
                    SQL &= "INNER JOIN S_PENDAFTARAN_H B "
                    SQL &= "ON A.KDPENDAFTARAN = B.KDPENDAFTARAN "
                    SQL &= "INNER JOIN M_DOCTOR C "
                    SQL &= "ON A.KDDOCTOR = C.KDDOCTOR "
                    SQL &= "INNER JOIN M_CUSTOMER D "
                    SQL &= "ON B.KDCUSTOMER = D.KDCUSTOMER "
                    SQL &= "INNER JOIN M_DIAGNOSA E "
                    SQL &= "ON B.KDDIAGNOSA = E.KDDIAGNOSA "
                    SQL &= "INNER JOIN M_DAFTAR_L1 F "
                    SQL &= "ON B.KDDAFTAR_L1 = F.KDDAFTAR_L1 "
                    SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) = '" & deDATE.DateTime.ToString("yyyyMMdd") & "' "
                    SQL &= "AND A.KDDEPARTMENT = '" & KDDEPARTMENT & "' "
                    'SQL &= "AND A.KDDOCTOR = '" & KDDOCTOR & "' "
                    SQL &= "AND B.STATUSDAFTAR <> '3' "
                    SQL &= "AND B.CATEGORY = 0 "
                    SQL &= ") Z "
                    SQL &= "ORDER BY Z.KDDAFTAR_L6, Z.NoRegister ASC "

                    oComm.Connection = oConn
                    oComm.CommandText = SQL
                    oComm.CommandTimeout = 120
                    oComm.CommandType = CommandType.Text

                    da = New SqlDataAdapter(oComm)
                    da.Fill(ds, "S_LISTPASIEN")

                    grdListPasien.DataSource = ds.Tables("S_LISTPASIEN")
                    grdListPasien.ForceInitialize()

                    If oConn.State = ConnectionState.Open Then
                        oConn.Close()
                    End If

                    fn_LoadFormatData()

                    'grvListPasien.BestFitColumns()
                    Rata()
                Else
                    If grdKDDEPARTMENT.Text <> "" And grdDPJP.Text <> "" Then
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
                        SQL &= "* "
                        SQL &= "FROM ( "
                        SQL &= "SELECT "
                        SQL &= "B.KDPENDAFTARAN "
                        SQL &= ",NoRegister = A.KDKUNJUNGAN "
                        SQL &= ",Antrian = ISNULL((SELECT TOP 1 '(KONSUL INTERNAL)' FROM S_PENDAFTARAN_SKD WHERE ALASAN = 'KONSUL INTERNAL' AND A.KDPENDAFTARAN = KDPENDAFTARAN), '') + B.KDBOOKING "
                        SQL &= ",KodeBooking = B.KODEBOOKING "
                        SQL &= ",Pasien = D.NAME_DISPLAY "
                        SQL &= ",Dokter = C.NAME_DISPLAY "
                        SQL &= ",SIP = C.SIP "
                        SQL &= ",B.KDCUSTOMER "
                        SQL &= ",USIA = CONVERT(nvarchar(50), DATEDIFF(YEAR, D.TANGGALLAHIR, B.DATE)) + ' Tahun, ' + CONVERT(nvarchar(50), DATEDIFF(MONTH, D.TANGGALLAHIR, B.DATE) - (DATEDIFF(YEAR, D.TANGGALLAHIR, B.DATE) * 12)) + ' Bulan, ' + CONVERT(nvarchar(50), DATEDIFF(DAY, DATEADD(MONTH, DATEDIFF(MONTH, D.TANGGALLAHIR, B.DATE), D.TANGGALLAHIR), B.DATE)) + ' Hari' "
                        SQL &= ",D.TANGGALLAHIR "
                        SQL &= ",B.KDDIAGNOSA "
                        SQL &= ",DIAGNOSA = E.MEMO "
                        SQL &= ",B.DATE "
                        SQL &= ",WaktuPeriksa = ISNULL((SELECT FORMAT(DATE, 'HH:mm:ss') FROM SET_WAKTUTUNGGU WHERE B.KODEBOOKING = KODEBOOKING AND ISPANGGIL = 4) ,'')  "
                        SQL &= ",WaktuSelisih = ISNULL((SELECT  DATEDIFF(MINUTE, B.DATE, DATE) FROM SET_WAKTUTUNGGU WHERE B.KODEBOOKING = KODEBOOKING AND ISPANGGIL = 4) ,'') "
                        SQL &= ",PENJAMIN = F.MEMO "
                        SQL &= ",B.NOMORSEP "
                        SQL &= ",B.KARTUBPJS "
                        SQL &= ",Cek = CASE B.KDDAFTAR_L6 WHEN 'DAFTAR_L6_0000000003' THEN CONVERT(BIT, 1) ELSE CONVERT(BIT, 0) END "
                        SQL &= ",Cek2 = B.KDDAFTAR_L6 "
                        SQL &= ",C.KDDOCTOR "
                        'IIf(grvListPasien.GetFocusedRowCellValue("JK") = "1", "Laki-laki", "Perempuan")
                        SQL &= ",JK = D.KDJENISKELAMIN "
                        SQL &= ",ISPERAWAT = CASE B.KDDAFTAR_L6 WHEN 'DAFTAR_L6_0000000002' THEN CONVERT(BIT, 1) ELSE CONVERT(BIT, 0) END "
                        SQL &= ",Keterangan = ISNULL((SELECT STRING_AGG(AA.PLANNING_ALASAN,', ') FROM DATABASERME..R_CPPT AA INNER JOIN DATABASERME..A_IDENTITASPASIEN_LIST BB ON AA.KDIDENTITAS = BB.KDIDENTITAS WHERE BB.KDKUNJUNGAN = A.KDKUNJUNGAN AND AA.KDPROFESI = 'PROFESI_0000000001'), '') "

                        'If chkCek.Checked = False Then
                        '    SQL &= ",Keterangan = ISNULL((SELECT STRING_AGG(TINDAKLANJUT + ' ' + ALASAN + ' ' + DESCRIPTION,', ') FROM S_PENDAFTARAN_SKD WHERE B.KDPENDAFTARAN = KDPENDAFTARAN GROUP BY KDPENDAFTARAN), '') "
                        'Else
                        '    SQL &= ",Keterangan = ISNULL((SELECT TOP 1 TINDAKLANJUT + ' ' + ALASAN + ' ' + DESCRIPTION FROM S_PENDAFTARAN_SKD WHERE B.KDPENDAFTARAN = KDPENDAFTARAN), '') "
                        'End If
                        SQL &= ",KDDAFTAR_L6 = CASE B.KDDAFTAR_L6 WHEN 'DAFTAR_L6_0000000001' THEN 'X' WHEN 'DAFTAR_L6_0000000002' THEN 'Y' ELSE 'Z' END "
                        SQL &= ",B.KDKELASRAWAT "
                        SQL &= ",B.CATEGORY "
                        SQL &= "FROM "
                        SQL &= "S_PENDAFTARAN_KUNJUNGAN A "
                        SQL &= "INNER JOIN S_PENDAFTARAN_H B "
                        SQL &= "ON A.KDPENDAFTARAN = B.KDPENDAFTARAN "
                        SQL &= "INNER JOIN M_DOCTOR C "
                        SQL &= "ON A.KDDOCTOR = C.KDDOCTOR "
                        SQL &= "INNER JOIN M_CUSTOMER D "
                        SQL &= "ON B.KDCUSTOMER = D.KDCUSTOMER "
                        SQL &= "INNER JOIN M_DIAGNOSA E "
                        SQL &= "ON B.KDDIAGNOSA = E.KDDIAGNOSA "
                        SQL &= "INNER JOIN M_DAFTAR_L1 F "
                        SQL &= "ON B.KDDAFTAR_L1 = F.KDDAFTAR_L1 "
                        SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) = '" & deDATE.DateTime.ToString("yyyyMMdd") & "' "
                        SQL &= "AND A.KDDEPARTMENT = '" & KDDEPARTMENT & "' "
                        SQL &= "AND A.KDDOCTOR = '" & KDDOCTOR & "' "
                        SQL &= "AND B.STATUSDAFTAR <> '3' "
                        SQL &= "AND B.CATEGORY = 0 "
                        SQL &= ") Z "
                        SQL &= "ORDER BY Z.KDDAFTAR_L6, Z.NoRegister ASC "

                        oComm.Connection = oConn
                        oComm.CommandText = SQL
                        oComm.CommandTimeout = 120
                        oComm.CommandType = CommandType.Text

                        da = New SqlDataAdapter(oComm)
                        da.Fill(ds, "S_LISTPASIEN")

                        grdListPasien.DataSource = ds.Tables("S_LISTPASIEN")
                        grdListPasien.ForceInitialize()

                        If oConn.State = ConnectionState.Open Then
                            oConn.Close()
                        End If

                        fn_LoadFormatData()

                        'grvListPasien.BestFitColumns()
                        Rata()
                    End If
                End If
            Else
                If chkPemetaan.Checked = True Then
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
                    SQL &= "* "
                    SQL &= "FROM ( "
                    SQL &= "SELECT	"
                    SQL &= "A.KODEBED "
                    SQL &= ",A.BED "
                    SQL &= ",A.KDUPDATE_APLICARE "
                    SQL &= ",KDPENDAFTARAN = ISNULL((Z.KODEREGISTER), '') "
                    SQL &= ",NoRM = ISNULL((Z.KDCUSTOMER), '') "
                    SQL &= ",NoRegister = ISNULL((Z.KDKUNJUNGAN), '') "
                    SQL &= ",Antrian = ISNULL((Z.KDBOOKING), '')  "
                    SQL &= ",KodeBooking = ISNULL((Z.KODEBOOKING), '') "
                    SQL &= ",Pasien = ISNULL((Z.PASIEN), '')  "
                    SQL &= ",WaktuPeriksa = BBB.NAME_DISPLAY  "
                    SQL &= ",Dokter = ISNULL((Z.DOKTER), '') "
                    SQL &= ",SIP = ISNULL((Z.SIP), '') "
                    SQL &= ",KDCUSTOMER = ISNULL((Z.KDCUSTOMER), '') "
                    SQL &= ",USIA = ISNULL(CONVERT(nvarchar(50), DATEDIFF(YEAR,  ISNULL((Z.TANGGALLAHIR), GETDATE()), ISNULL((Z.DATE), GETDATE()))) + ' Tahun, ' + CONVERT(nvarchar(50), DATEDIFF(MONTH,  ISNULL((Z.TANGGALLAHIR), GETDATE()), ISNULL((Z.DATE), GETDATE())) - (DATEDIFF(YEAR,  ISNULL((Z.TANGGALLAHIR), GETDATE()), Z.DATE) * 12)) + ' Bulan, ' + CONVERT(nvarchar(50), DATEDIFF(DAY, DATEADD(MONTH, DATEDIFF(MONTH,  ISNULL((Z.TANGGALLAHIR), GETDATE()), ISNULL((Z.DATE), GETDATE())),  ISNULL((Z.TANGGALLAHIR), GETDATE())), ISNULL((Z.DATE), GETDATE()))) + ' Hari' , '')	"
                    SQL &= ",TANGGALLAHIR  = ISNULL((Z.TANGGALLAHIR), GETDATE())	"
                    SQL &= ",KDDIAGNOSA = ISNULL((Z.KDDIAGNOSA), '') "
                    SQL &= ",DIAGNOSA = '' "
                    SQL &= ",DATE = ISNULL((Z.DATE), GETDATE())	"
                    SQL &= ",WaktuSelisih = ISNULL((Z.GABUNGRUANGAN), '') "
                    SQL &= ",PENJAMIN = ISNULL((Z.PENJAMIN), '') "
                    SQL &= ",NOMORSEP = ISNULL((Z.NOMORSEP), '') "
                    SQL &= ",KARTUBPJS = ISNULL((Z.KARTUBPJS), '') "
                    SQL &= ",Cek = ISNULL((CASE Z.KDDAFTAR_L6 WHEN 'DAFTAR_L6_0000000003' THEN CONVERT(BIT, 1) ELSE CONVERT(BIT, 0) END ), CONVERT(BIT, 0))	"
                    SQL &= ",Cek2 = ISNULL((Z.KDDAFTAR_L6), '') "
                    SQL &= ",KDDOCTOR = ISNULL((Z.KDDOCTOR), '')	"
                    SQL &= ",JK = ISNULL((Z.KDJENISKELAMIN), '') "
                    SQL &= ",ISPERAWAT =  ISNULL((CASE Z.KDDAFTAR_L6 WHEN 'DAFTAR_L6_0000000002' THEN CONVERT(BIT, 1) ELSE CONVERT(BIT, 0) END ), CONVERT(BIT, 0)) "
                    SQL &= ",Keterangan = A.STATUS	"
                    SQL &= ",KDDAFTAR_L6 = ISNULL((CASE Z.KDDAFTAR_L6 WHEN 'DAFTAR_L6_0000000001' THEN 'X' WHEN 'DAFTAR_L6_0000000002' THEN 'Y' ELSE 'Z' END), 'Z') "
                    SQL &= ",KDKELASRAWAT = ISNULL((Z.KDKELASRAWAT), '')	"
                    SQL &= ",DPJPKeDua = ISNULL((SELECT BB.NAME_DISPLAY FROM I_TRACKING_KDDOCTOR AA INNER JOIN M_DOCTOR BB ON AA.KDDOCTOR_KEPADA = BB.KDDOCTOR WHERE ISNULL((Z.KDPENDAFTARAN), '')  = AA.KDREG AND AA.SEQ = 2), '') "
                    SQL &= ",DPJPKeTiga = ISNULL((SELECT BB.NAME_DISPLAY FROM I_TRACKING_KDDOCTOR AA INNER JOIN M_DOCTOR BB ON AA.KDDOCTOR_KEPADA = BB.KDDOCTOR WHERE ISNULL((Z.KDPENDAFTARAN), '')  = AA.KDREG AND AA.SEQ = 3), '') "
                    SQL &= ",DPJPKeEmpat = ISNULL((SELECT BB.NAME_DISPLAY FROM I_TRACKING_KDDOCTOR AA INNER JOIN M_DOCTOR BB ON AA.KDDOCTOR_KEPADA = BB.KDDOCTOR WHERE ISNULL((Z.KDPENDAFTARAN), '')  = AA.KDREG AND AA.SEQ = 4), '') "
                    SQL &= ",DPJPKeLima = ISNULL((SELECT BB.NAME_DISPLAY FROM I_TRACKING_KDDOCTOR AA INNER JOIN M_DOCTOR BB ON AA.KDDOCTOR_KEPADA = BB.KDDOCTOR WHERE ISNULL((Z.KDPENDAFTARAN), '')  = AA.KDREG AND AA.SEQ = 5), '') "
                    'SQL &= ",KDCPPT = ISNULL((SELECT TOP 1 KDCPPT FROM I_TRACKING_CPPT WHERE TANGGAL = ' & Now.ToString(yyyyMMdd) & ' AND ISNULL((Z.KDPENDAFTARAN), '')  = KDREG AND KDDOCTOR = ' & sKDDPJPUSER & '), '') "
                    'SQL &= ",KDCPPT_PERAWAT = ISNULL((SELECT TOP 1 KDCPPT FROM I_TRACKING_CPPT WHERE TANGGAL = ' & Now.ToString(yyyyMMdd) & ' AND ISNULL((Z.KDPENDAFTARAN), '')  = KDREG AND DESCRIPTION = 'PERAWAT'), '') "
                    'SQL &= ",KDCPPT_DOKTERAKHIR = ISNULL((SELECT TOP 1 KDCPPT FROM I_TRACKING_CPPT WHERE ISNULL((Z.KDPENDAFTARAN), '')  = KDREG AND KDDOCTOR = ' & sKDDPJPUSER & ' ORDER BY KDCPPT DESC), '') "
                    'SQL &= ",KDCPPT_PERAWATAKHIR = ISNULL((SELECT TOP 1 KDCPPT FROM I_TRACKING_CPPT WHERE ISNULL((Z.KDPENDAFTARAN), '')  = KDREG AND DESCRIPTION = 'PERAWAT' ORDER BY KDCPPT DESC), '') "
                    SQL &= ",KDPENDAFTARAN_AWAL = ISNULL((Z.KDPENDAFTARAN_AWAL), '')	"
                    SQL &= ",CATEGORY = ISNULL((Z.CATEGORY), '')	"
                    SQL &= ",CekSudahAdaCPPT = A.MEMO "
                    'SQL &= ",CekCPPTDokter = ISNULL((SELECT TOP 1 'YA' FROM DATABASERME..S_REQ_CPPT WHERE KDCUSTOMER =  ISNULL((Z.KDCUSTOMER), '') AND FORMAT(DATE, 'yyyyMMdd') = FORMAT(GETDATE(), 'yyyyMMdd') AND PROFESI = 'DOKTER'), 'NO') "
                    'SQL &= ",CekCPPTPerawat = ISNULL((SELECT TOP 1 'YA' FROM DATABASERME..S_REQ_CPPT WHERE KDCUSTOMER =  ISNULL((Z.KDCUSTOMER), '') AND FORMAT(DATE, 'yyyyMMdd') = FORMAT(GETDATE(), 'yyyyMMdd') AND PROFESI <> 'DOKTER'), 'NO')      "
                    SQL &= "FROM	"
                    SQL &= "M_KELASAPLICARE_DEPARTMENT_BED A	"
                    SQL &= "INNER JOIN M_DEPARTMENT BBB	"
                    SQL &= "ON A.KDDEPARTMENT = BBB.KDDEPARTMENT	"
                    SQL &= "LEFT JOIN (SELECT GABUNGRUANGAN = FF.ANTRIAN,KODEREGISTER = AA.KDPENDAFTARAN,AA.KDKUNJUNGAN, BB.KDPENDAFTARAN_AWAL, BB.CATEGORY,BB.KDKELASRAWAT,CC.KDJENISKELAMIN,AA.KDDOCTOR,BB.NOMORSEP, BB.KDDAFTAR_L6,BB.KARTUBPJS, BB.DATE,AA.KDPENDAFTARAN, BB.KDDIAGNOSA,PASIEN = CC.NAME_DISPLAY, BB.KDCUSTOMER, BB.KODEBOOKING, BB.KDBOOKING, PENJAMIN = EE.MEMO,DOKTER = DD.NAME_DISPLAY, DD.SIP, CC.TANGGALLAHIR FROM S_PENDAFTARAN_KUNJUNGAN AA INNER JOIN S_PENDAFTARAN_H BB ON AA.KDPENDAFTARAN = BB.KDPENDAFTARAN INNER JOIN M_CUSTOMER CC ON BB.KDCUSTOMER = CC.KDCUSTOMER INNER JOIN M_DOCTOR DD ON AA.KDDOCTOR = DD.KDDOCTOR INNER JOIN M_DAFTAR_L1 EE ON BB.KDDAFTAR_L1 = EE.KDDAFTAR_L1 INNER JOIN M_DEPARTMENT FF ON AA.KDDEPARTMENT = FF.KDDEPARTMENT) Z ON A.KDPENDAFTARAN = Z.KDKUNJUNGAN "
                    SQL &= ") XX "

                    Dim oDepartment As New Reference.clsDepartment
                    Dim kode As String = String.Empty
                    Dim dsKode = oDepartment.GetData(KDDEPARTMENT)
                    If dsKode IsNot Nothing Then
                        kode = dsKode.ANTRIAN
                    End If

                    If grdKDDEPARTMENT.Text <> "" And grdDPJP.Text = "" Then
                        'RUANGAN
                        If kode = "" Then
                            SQL &= "WHERE XX.WaktuPeriksa = '" & grdKDDEPARTMENT.Text & "' "
                        Else
                            SQL &= "WHERE XX.WaktuSelisih = '" & kode & "' "
                        End If
                    ElseIf grdKDDEPARTMENT.Text = "" And grdDPJP.Text <> ""
                        'DOKTER
                        SQL &= "WHERE XX.Dokter = '" & grdDPJP.Text & "' "
                        SQL &= "OR XX.DPJPKeDua = '" & grdDPJP.Text & "' "
                        SQL &= "OR XX.DPJPKeTiga = '" & grdDPJP.Text & "' "
                        SQL &= "OR XX.DPJPKeEmpat = '" & grdDPJP.Text & "' "
                        SQL &= "OR XX.DPJPKeLima = '" & grdDPJP.Text & "' "
                    ElseIf grdKDDEPARTMENT.Text <> "" And grdDPJP.Text <> ""
                        'RUANGAN DAN DOKTER
                        If kode = "" Then
                            SQL &= "WHERE XX.WaktuPeriksa = '" & grdKDDEPARTMENT.Text & "' "
                            SQL &= "AND XX.Dokter = '" & grdDPJP.Text & "' "
                        Else
                            SQL &= "WHERE XX.WaktuSelisih = '" & kode & "' "
                            SQL &= "AND XX.Dokter = '" & grdDPJP.Text & "' "
                        End If
                    Else
                        'SEMUA

                    End If

                    SQL &= "ORDER BY XX.WaktuPeriksa, XX.BED ASC "

                    oComm.Connection = oConn
                    oComm.CommandText = SQL
                    oComm.CommandTimeout = 120
                    oComm.CommandType = CommandType.Text

                    da = New SqlDataAdapter(oComm)
                    da.Fill(ds, "S_LISTPASIEN")

                    grdListPasien.DataSource = ds.Tables("S_LISTPASIEN")
                    grdListPasien.ForceInitialize()

                    If oConn.State = ConnectionState.Open Then
                        oConn.Close()
                    End If

                    fn_LoadFormatData()
                Else
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

                    'SQL = "EXEC LISTRME_INAP @CATEGORY = 'R.Inap'  "
                    'SQL &= ", @DARITANGGAL = '" & "20250101" & "' "
                    'SQL &= ", @SAMPAITANGGAL = '" & deDATE.DateTime.ToString("yyyyMMdd") & "' "
                    'SQL &= ", @KDDOCTOR = '" & grdDPJP.EditValue & "' "

                    SQL = "SELECT "
                    SQL &= "* "
                    SQL &= "FROM ( "
                    SQL &= "SELECT "
                    SQL &= "B.KDPENDAFTARAN "
                    SQL &= ",KODEBED = '' "
                    SQL &= ",BED = '' "
                    SQL &= ",KDUPDATE_APLICARE = '' "
                    SQL &= ",NoRM = B.KDCUSTOMER "
                    SQL &= ",NoRegister = A.KDKUNJUNGAN "
                    SQL &= ",Antrian = B.KDBOOKING "
                    SQL &= ",KodeBooking = B.KODEBOOKING "
                    SQL &= ",Pasien = D.NAME_DISPLAY "
                    SQL &= ",WaktuPeriksa = G.NAME_DISPLAY  "
                    SQL &= ",Dokter = C.NAME_DISPLAY "
                    SQL &= ",SIP = C.SIP "
                    SQL &= ",B.KDCUSTOMER "
                    SQL &= ",USIA = CONVERT(nvarchar(50), DATEDIFF(YEAR, D.TANGGALLAHIR, B.DATE)) + ' Tahun, ' + CONVERT(nvarchar(50), DATEDIFF(MONTH, D.TANGGALLAHIR, B.DATE) - (DATEDIFF(YEAR, D.TANGGALLAHIR, B.DATE) * 12)) + ' Bulan, ' + CONVERT(nvarchar(50), DATEDIFF(DAY, DATEADD(MONTH, DATEDIFF(MONTH, D.TANGGALLAHIR, B.DATE), D.TANGGALLAHIR), B.DATE)) + ' Hari' "
                    SQL &= ",D.TANGGALLAHIR "
                    SQL &= ",B.KDDIAGNOSA "
                    SQL &= ",DIAGNOSA = E.MEMO "
                    SQL &= ",A.DATE "
                    'SQL &= ",WaktuPeriksa = ISNULL((SELECT FORMAT(DATE, 'HH:mm:ss') FROM SET_WAKTUTUNGGU WHERE B.KODEBOOKING = KODEBOOKING AND ISPANGGIL = 4) ,'')  "
                    SQL &= ",WaktuSelisih = ISNULL((SELECT  DATEDIFF(MINUTE, B.DATE, DATE) FROM SET_WAKTUTUNGGU WHERE B.KODEBOOKING = KODEBOOKING AND ISPANGGIL = 4) ,'') "
                    SQL &= ",PENJAMIN = F.MEMO "
                    SQL &= ",B.NOMORSEP "
                    SQL &= ",B.KARTUBPJS "
                    SQL &= ",Cek = CASE B.KDDAFTAR_L6 WHEN 'DAFTAR_L6_0000000003' THEN CONVERT(BIT, 1) ELSE CONVERT(BIT, 0) END "
                    SQL &= ",Cek2 = B.KDDAFTAR_L6 "
                    SQL &= ",C.KDDOCTOR "
                    SQL &= ",JK = D.KDJENISKELAMIN "
                    SQL &= ",ISPERAWAT =  CASE B.KDDAFTAR_L6 WHEN 'DAFTAR_L6_0000000002' THEN CONVERT(BIT, 1) ELSE CONVERT(BIT, 0) END "
                    SQL &= ",Keterangan = ISNULL((SELECT STRING_AGG(AA.PLANNING_ALASAN,', ') FROM DATABASERME..R_CPPT AA INNER JOIN DATABASERME..A_IDENTITASPASIEN_LIST BB ON AA.KDIDENTITAS = BB.KDIDENTITAS WHERE BB.KDKUNJUNGAN = A.KDKUNJUNGAN AND AA.KDPROFESI = 'PROFESI_0000000001'), '') "
                    SQL &= ",KDDAFTAR_L6 = CASE B.KDDAFTAR_L6 WHEN 'DAFTAR_L6_0000000001' THEN 'X' WHEN 'DAFTAR_L6_0000000002' THEN 'Y' ELSE 'Z' END "
                    SQL &= ",B.KDKELASRAWAT "
                    SQL &= ",DPJPKeDua = ISNULL((SELECT BB.NAME_DISPLAY FROM I_TRACKING_KDDOCTOR AA INNER JOIN M_DOCTOR BB ON AA.KDDOCTOR_KEPADA = BB.KDDOCTOR WHERE A.KDPENDAFTARAN = AA.KDREG AND AA.SEQ = 2), '') "
                    SQL &= ",DPJPKeTiga = ISNULL((SELECT BB.NAME_DISPLAY FROM I_TRACKING_KDDOCTOR AA INNER JOIN M_DOCTOR BB ON AA.KDDOCTOR_KEPADA = BB.KDDOCTOR WHERE A.KDPENDAFTARAN = AA.KDREG AND AA.SEQ = 3), '') "
                    SQL &= ",DPJPKeEmpat = ISNULL((SELECT BB.NAME_DISPLAY FROM I_TRACKING_KDDOCTOR AA INNER JOIN M_DOCTOR BB ON AA.KDDOCTOR_KEPADA = BB.KDDOCTOR WHERE A.KDPENDAFTARAN = AA.KDREG AND AA.SEQ = 4), '') "
                    SQL &= ",DPJPKeLima = ISNULL((SELECT BB.NAME_DISPLAY FROM I_TRACKING_KDDOCTOR AA INNER JOIN M_DOCTOR BB ON AA.KDDOCTOR_KEPADA = BB.KDDOCTOR WHERE A.KDPENDAFTARAN = AA.KDREG AND AA.SEQ = 5), '') "
                    'SQL &= ",KDCPPT = ISNULL((SELECT TOP 1 KDCPPT FROM I_TRACKING_CPPT WHERE TANGGAL = '" & Now.ToString("yyyyMMdd") & "' AND A.KDPENDAFTARAN = KDREG AND KDDOCTOR = '" & sKDDPJPUSER & "'), '') "
                    'SQL &= ",KDCPPT_PERAWAT = ISNULL((SELECT TOP 1 KDCPPT FROM I_TRACKING_CPPT WHERE TANGGAL = '" & Now.ToString("yyyyMMdd") & "' AND A.KDPENDAFTARAN = KDREG AND DESCRIPTION = 'PERAWAT'), '') "
                    'SQL &= ",KDCPPT_DOKTERAKHIR = ISNULL((SELECT TOP 1 KDCPPT FROM I_TRACKING_CPPT WHERE A.KDPENDAFTARAN = KDREG AND KDDOCTOR = '" & sKDDPJPUSER & "' ORDER BY KDCPPT DESC), '') "
                    'SQL &= ",KDCPPT_PERAWATAKHIR = ISNULL((SELECT TOP 1 KDCPPT FROM I_TRACKING_CPPT WHERE A.KDPENDAFTARAN = KDREG AND DESCRIPTION = 'PERAWAT' ORDER BY KDCPPT DESC), '') "
                    SQL &= ",B.KDPENDAFTARAN_AWAL "
                    SQL &= ",B.CATEGORY "
                    SQL &= ",CekSudahAdaCPPT = '' "
                    'SQL &= ",KodeDPJPKeDua = ISNULL((SELECT AA.KDDOCTOR_KEPADA FROM I_TRACKING_KDDOCTOR AA WHERE A.KDPENDAFTARAN = AA.KDREG AND AA.SEQ = 2), '') "
                    'SQL &= ",KodeDPJPKeTiga = ISNULL((SELECT AA.KDDOCTOR_KEPADA FROM I_TRACKING_KDDOCTOR AA WHERE A.KDPENDAFTARAN = AA.KDREG AND AA.SEQ = 3), '') "
                    'SQL &= ",KodeDPJPKeEmpat = ISNULL((SELECT AA.KDDOCTOR_KEPADA FROM I_TRACKING_KDDOCTOR AA WHERE A.KDPENDAFTARAN = AA.KDREG AND AA.SEQ = 4), '') "
                    'SQL &= ",KodeDPJPKeLima = ISNULL((SELECT AA.KDDOCTOR_KEPADA FROM I_TRACKING_KDDOCTOR AA WHERE A.KDPENDAFTARAN = AA.KDREG AND AA.SEQ = 5), '') "
                    'SQL &= ",CekCPPTDokter = ISNULL((SELECT TOP 1 'YA' FROM DATABASERME..S_REQ_CPPT WHERE KDCUSTOMER = B.KDCUSTOMER AND FORMAT(DATE, 'yyyyMMdd') = FORMAT(GETDATE(), 'yyyyMMdd') AND PROFESI = 'DOKTER'), 'NO') "
                    'SQL &= ",CekCPPTPerawat = ISNULL((SELECT TOP 1 'YA' FROM DATABASERME..S_REQ_CPPT WHERE KDCUSTOMER = B.KDCUSTOMER AND FORMAT(DATE, 'yyyyMMdd') = FORMAT(GETDATE(), 'yyyyMMdd') AND PROFESI <> 'DOKTER'), 'NO') "
                    SQL &= "FROM "
                    SQL &= "S_PENDAFTARAN_KUNJUNGAN A "
                    SQL &= "INNER JOIN S_PENDAFTARAN_H B "
                    SQL &= "ON A.KDPENDAFTARAN = B.KDPENDAFTARAN "
                    SQL &= "INNER JOIN M_DOCTOR C "
                    SQL &= "ON A.KDDOCTOR = C.KDDOCTOR "
                    SQL &= "INNER JOIN M_CUSTOMER D "
                    SQL &= "ON B.KDCUSTOMER = D.KDCUSTOMER "
                    SQL &= "INNER JOIN M_DIAGNOSA E "
                    SQL &= "ON B.KDDIAGNOSA = E.KDDIAGNOSA "
                    SQL &= "INNER JOIN M_DAFTAR_L1 F "
                    SQL &= "ON B.KDDAFTAR_L1 = F.KDDAFTAR_L1 "
                    SQL &= "INNER JOIN M_DEPARTMENT G "
                    SQL &= "ON A.KDDEPARTMENT = G.KDDEPARTMENT "

                    If grdKDDEPARTMENT.Text <> "" And grdDPJP.Text = "" Then
                        'RUANGAN
                        Dim oDepartment As New Reference.clsDepartment
                        Dim kode As String = String.Empty
                        Dim dsKode = oDepartment.GetData(KDDEPARTMENT)
                        If dsKode IsNot Nothing Then
                            kode = dsKode.ANTRIAN
                        End If

                        If kode = "" Then
                            SQL &= "WHERE A.KDDEPARTMENT = '" & KDDEPARTMENT & "' "
                            SQL &= "AND B.STATUSDAFTAR <> '3' "
                            SQL &= "AND B.CATEGORY = 1 "
                            'If chkIsPulang.Checked = True Then
                            '    SQL &= "AND ISNULL((SELECT TOP 1 'Y' FROM DATABASERME..S_RINGKASANKELUARRAWATINAP WHERE KDREG = B.KDPENDAFTARAN), '') <> '' "
                            'Else
                            '    SQL &= "AND ISNULL((SELECT TOP 1 'Y' FROM DATABASERME..S_RINGKASANKELUARRAWATINAP WHERE KDREG = B.KDPENDAFTARAN), '') = '' "
                            'End If

                            If chkIsPulang.Checked = True Then
                                SQL &= "AND ISNULL((SELECT TOP 1 'Y' FROM DATABASERS..T_UPDATE_TANGGAL_PULANG WHERE KDPENDAFTARAN = B.KDPENDAFTARAN), '') <> '' "
                            Else
                                SQL &= "AND ISNULL((SELECT TOP 1 'Y' FROM DATABASERS..T_UPDATE_TANGGAL_PULANG WHERE KDPENDAFTARAN = B.KDPENDAFTARAN), '') = '' "
                            End If

                            SQL &= "AND CONVERT(VARCHAR(8), B.DATE, 112) >= '" & deDATE.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), B.DATE, 112) <= '" & deDateTo.DateTime.ToString("yyyyMMdd") & "' "

                        Else
                            SQL &= "WHERE G.ANTRIAN = '" & kode & "' "
                            SQL &= "AND B.STATUSDAFTAR <> '3' "
                            SQL &= "AND B.CATEGORY = 1 "
                            'If chkIsPulang.Checked = True Then
                            '    SQL &= "AND ISNULL((SELECT TOP 1 'Y' FROM DATABASERME..S_RINGKASANKELUARRAWATINAP WHERE KDREG = B.KDPENDAFTARAN), '') <> '' "
                            'Else
                            '    SQL &= "AND ISNULL((SELECT TOP 1 'Y' FROM DATABASERME..S_RINGKASANKELUARRAWATINAP WHERE KDREG = B.KDPENDAFTARAN), '') = '' "
                            'End If
                            If chkIsPulang.Checked = True Then
                                SQL &= "AND ISNULL((SELECT TOP 1 'Y' FROM DATABASERS..T_UPDATE_TANGGAL_PULANG WHERE KDPENDAFTARAN = B.KDPENDAFTARAN), '') <> '' "
                            Else
                                SQL &= "AND ISNULL((SELECT TOP 1 'Y' FROM DATABASERS..T_UPDATE_TANGGAL_PULANG WHERE KDPENDAFTARAN = B.KDPENDAFTARAN), '') = '' "
                            End If

                            SQL &= "AND CONVERT(VARCHAR(8), B.DATE, 112) >= '" & deDATE.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), B.DATE, 112) <= '" & deDateTo.DateTime.ToString("yyyyMMdd") & "' "

                        End If
                    ElseIf grdKDDEPARTMENT.Text = "" And grdDPJP.Text <> ""
                        'DPKTER
                        SQL &= "WHERE A.KDDOCTOR = '" & KDDOCTOR & "' "
                        SQL &= "AND B.STATUSDAFTAR <> '3' "
                        SQL &= "AND B.CATEGORY = 1 "
                        SQL &= "AND CONVERT(VARCHAR(8), B.DATE, 112) >= '" & deDATE.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), B.DATE, 112) <= '" & deDateTo.DateTime.ToString("yyyyMMdd") & "' "

                        'If chkIsPulang.Checked = True Then
                        '    SQL &= "AND ISNULL((SELECT TOP 1 'Y' FROM DATABASERME..S_RINGKASANKELUARRAWATINAP WHERE KDREG = B.KDPENDAFTARAN), '') <> '' "
                        'Else
                        '    SQL &= "AND ISNULL((SELECT TOP 1 'Y' FROM DATABASERME..S_RINGKASANKELUARRAWATINAP WHERE KDREG = B.KDPENDAFTARAN), '') = '' "
                        'End If

                        If chkIsPulang.Checked = True Then
                            SQL &= "AND ISNULL((SELECT TOP 1 'Y' FROM DATABASERS..T_UPDATE_TANGGAL_PULANG WHERE KDPENDAFTARAN = B.KDPENDAFTARAN), '') <> '' "
                        Else
                            SQL &= "AND ISNULL((SELECT TOP 1 'Y' FROM DATABASERS..T_UPDATE_TANGGAL_PULANG WHERE KDPENDAFTARAN = B.KDPENDAFTARAN), '') = '' "
                        End If

                        SQL &= "OR ISNULL((SELECT AA.KDDOCTOR_KEPADA FROM I_TRACKING_KDDOCTOR AA WHERE A.KDPENDAFTARAN = AA.KDREG AND AA.SEQ = 2), '') = '" & KDDOCTOR & "' "
                        SQL &= "AND B.STATUSDAFTAR <> '3' "
                        SQL &= "AND B.CATEGORY = 1 "
                        SQL &= "AND CONVERT(VARCHAR(8), B.DATE, 112) >= '" & deDATE.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), B.DATE, 112) <= '" & deDateTo.DateTime.ToString("yyyyMMdd") & "' "

                        'If chkIsPulang.Checked = True Then
                        '    SQL &= "AND ISNULL((SELECT TOP 1 'Y' FROM DATABASERME..S_RINGKASANKELUARRAWATINAP WHERE KDREG = B.KDPENDAFTARAN), '') <> '' "
                        'Else
                        '    SQL &= "AND ISNULL((SELECT TOP 1 'Y' FROM DATABASERME..S_RINGKASANKELUARRAWATINAP WHERE KDREG = B.KDPENDAFTARAN), '') = '' "
                        'End If

                        If chkIsPulang.Checked = True Then
                            SQL &= "AND ISNULL((SELECT TOP 1 'Y' FROM DATABASERS..T_UPDATE_TANGGAL_PULANG WHERE KDPENDAFTARAN = B.KDPENDAFTARAN), '') <> '' "
                        Else
                            SQL &= "AND ISNULL((SELECT TOP 1 'Y' FROM DATABASERS..T_UPDATE_TANGGAL_PULANG WHERE KDPENDAFTARAN = B.KDPENDAFTARAN), '') = '' "
                        End If

                        SQL &= "OR ISNULL((SELECT AA.KDDOCTOR_KEPADA FROM I_TRACKING_KDDOCTOR AA WHERE A.KDPENDAFTARAN = AA.KDREG AND AA.SEQ = 3), '') = '" & KDDOCTOR & "' "
                        SQL &= "AND B.STATUSDAFTAR <> '3' "
                        SQL &= "AND B.CATEGORY = 1 "
                        SQL &= "AND CONVERT(VARCHAR(8), B.DATE, 112) >= '" & deDATE.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), B.DATE, 112) <= '" & deDateTo.DateTime.ToString("yyyyMMdd") & "' "

                        'If chkIsPulang.Checked = True Then
                        '    SQL &= "AND ISNULL((SELECT TOP 1 'Y' FROM DATABASERME..S_RINGKASANKELUARRAWATINAP WHERE KDREG = B.KDPENDAFTARAN), '') <> '' "
                        'Else
                        '    SQL &= "AND ISNULL((SELECT TOP 1 'Y' FROM DATABASERME..S_RINGKASANKELUARRAWATINAP WHERE KDREG = B.KDPENDAFTARAN), '') = '' "
                        'End If

                        If chkIsPulang.Checked = True Then
                            SQL &= "AND ISNULL((SELECT TOP 1 'Y' FROM DATABASERS..T_UPDATE_TANGGAL_PULANG WHERE KDPENDAFTARAN = B.KDPENDAFTARAN), '') <> '' "
                        Else
                            SQL &= "AND ISNULL((SELECT TOP 1 'Y' FROM DATABASERS..T_UPDATE_TANGGAL_PULANG WHERE KDPENDAFTARAN = B.KDPENDAFTARAN), '') = '' "
                        End If

                        SQL &= "OR ISNULL((SELECT AA.KDDOCTOR_KEPADA FROM I_TRACKING_KDDOCTOR AA WHERE A.KDPENDAFTARAN = AA.KDREG AND AA.SEQ = 4), '') = '" & KDDOCTOR & "' "
                        SQL &= "AND B.STATUSDAFTAR <> '3' "
                        SQL &= "AND B.CATEGORY = 1 "
                        SQL &= "AND CONVERT(VARCHAR(8), B.DATE, 112) >= '" & deDATE.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), B.DATE, 112) <= '" & deDateTo.DateTime.ToString("yyyyMMdd") & "' "

                        'If chkIsPulang.Checked = True Then
                        '    SQL &= "AND ISNULL((SELECT TOP 1 'Y' FROM DATABASERME..S_RINGKASANKELUARRAWATINAP WHERE KDREG = B.KDPENDAFTARAN), '') <> '' "
                        'Else
                        '    SQL &= "AND ISNULL((SELECT TOP 1 'Y' FROM DATABASERME..S_RINGKASANKELUARRAWATINAP WHERE KDREG = B.KDPENDAFTARAN), '') = '' "
                        'End If

                        If chkIsPulang.Checked = True Then
                            SQL &= "AND ISNULL((SELECT TOP 1 'Y' FROM DATABASERS..T_UPDATE_TANGGAL_PULANG WHERE KDPENDAFTARAN = B.KDPENDAFTARAN), '') <> '' "
                        Else
                            SQL &= "AND ISNULL((SELECT TOP 1 'Y' FROM DATABASERS..T_UPDATE_TANGGAL_PULANG WHERE KDPENDAFTARAN = B.KDPENDAFTARAN), '') = '' "
                        End If

                        SQL &= "OR ISNULL((SELECT AA.KDDOCTOR_KEPADA FROM I_TRACKING_KDDOCTOR AA WHERE A.KDPENDAFTARAN = AA.KDREG AND AA.SEQ = 5), '') = '" & KDDOCTOR & "' "
                        SQL &= "AND B.STATUSDAFTAR <> '3' "
                        SQL &= "AND B.CATEGORY = 1 "
                        SQL &= "AND CONVERT(VARCHAR(8), B.DATE, 112) >= '" & deDATE.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), B.DATE, 112) <= '" & deDateTo.DateTime.ToString("yyyyMMdd") & "' "

                        'If chkIsPulang.Checked = True Then
                        '    SQL &= "AND ISNULL((SELECT TOP 1 'Y' FROM DATABASERME..S_RINGKASANKELUARRAWATINAP WHERE KDREG = B.KDPENDAFTARAN), '') <> '' "
                        'Else
                        '    SQL &= "AND ISNULL((SELECT TOP 1 'Y' FROM DATABASERME..S_RINGKASANKELUARRAWATINAP WHERE KDREG = B.KDPENDAFTARAN), '') = '' "
                        'End If

                        If chkIsPulang.Checked = True Then
                            SQL &= "AND ISNULL((SELECT TOP 1 'Y' FROM DATABASERS..T_UPDATE_TANGGAL_PULANG WHERE KDPENDAFTARAN = B.KDPENDAFTARAN), '') <> '' "
                        Else
                            SQL &= "AND ISNULL((SELECT TOP 1 'Y' FROM DATABASERS..T_UPDATE_TANGGAL_PULANG WHERE KDPENDAFTARAN = B.KDPENDAFTARAN), '') = '' "
                        End If

                    ElseIf grdKDDEPARTMENT.Text <> "" And grdDPJP.Text <> ""
                        'RUANGAN DAN DOKTER
                        Dim oDepartment As New Reference.clsDepartment
                        Dim kode As String = String.Empty
                        Dim dsKode = oDepartment.GetData(KDDEPARTMENT)
                        If dsKode IsNot Nothing Then
                            kode = dsKode.ANTRIAN
                        End If

                        If kode = "" Then
                            SQL &= "WHERE A.KDDEPARTMENT = '" & KDDEPARTMENT & "' "
                            SQL &= "AND A.KDDOCTOR = '" & KDDOCTOR & "' "
                            SQL &= "AND B.STATUSDAFTAR <> '3' "
                            SQL &= "AND B.CATEGORY = 1 "
                            SQL &= "AND CONVERT(VARCHAR(8), B.DATE, 112) >= '" & deDATE.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), B.DATE, 112) <= '" & deDateTo.DateTime.ToString("yyyyMMdd") & "' "

                            'If chkIsPulang.Checked = True Then
                            '    SQL &= "AND ISNULL((SELECT TOP 1 'Y' FROM DATABASERME..S_RINGKASANKELUARRAWATINAP WHERE KDREG = B.KDPENDAFTARAN), '') <> '' "
                            'Else
                            '    SQL &= "AND ISNULL((SELECT TOP 1 'Y' FROM DATABASERME..S_RINGKASANKELUARRAWATINAP WHERE KDREG = B.KDPENDAFTARAN), '') = '' "
                            'End If

                            If chkIsPulang.Checked = True Then
                                SQL &= "AND ISNULL((SELECT TOP 1 'Y' FROM DATABASERS..T_UPDATE_TANGGAL_PULANG WHERE KDPENDAFTARAN = B.KDPENDAFTARAN), '') <> '' "
                            Else
                                SQL &= "AND ISNULL((SELECT TOP 1 'Y' FROM DATABASERS..T_UPDATE_TANGGAL_PULANG WHERE KDPENDAFTARAN = B.KDPENDAFTARAN), '') = '' "
                            End If
                        Else
                            SQL &= "WHERE G.ANTRIAN = '" & kode & "' "
                            SQL &= "AND A.KDDOCTOR = '" & KDDOCTOR & "' "
                            SQL &= "AND B.STATUSDAFTAR <> '3' "
                            SQL &= "AND B.CATEGORY = 1 "
                            SQL &= "AND CONVERT(VARCHAR(8), B.DATE, 112) >= '" & deDATE.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), B.DATE, 112) <= '" & deDateTo.DateTime.ToString("yyyyMMdd") & "' "

                            'If chkIsPulang.Checked = True Then
                            '    SQL &= "AND ISNULL((SELECT TOP 1 'Y' FROM DATABASERME..S_RINGKASANKELUARRAWATINAP WHERE KDREG = B.KDPENDAFTARAN), '') <> '' "
                            'Else
                            '    SQL &= "AND ISNULL((SELECT TOP 1 'Y' FROM DATABASERME..S_RINGKASANKELUARRAWATINAP WHERE KDREG = B.KDPENDAFTARAN), '') = '' "
                            'End If

                            If chkIsPulang.Checked = True Then
                                SQL &= "AND ISNULL((SELECT TOP 1 'Y' FROM DATABASERS..T_UPDATE_TANGGAL_PULANG WHERE KDPENDAFTARAN = B.KDPENDAFTARAN), '') <> '' "
                            Else
                                SQL &= "AND ISNULL((SELECT TOP 1 'Y' FROM DATABASERS..T_UPDATE_TANGGAL_PULANG WHERE KDPENDAFTARAN = B.KDPENDAFTARAN), '') = '' "
                            End If
                        End If

                    Else
                        'SEMUA
                        SQL &= "WHERE B.STATUSDAFTAR <> '3' "
                        SQL &= "AND B.CATEGORY = 1 "
                        SQL &= "AND CONVERT(VARCHAR(8), B.DATE, 112) >= '" & deDATE.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), B.DATE, 112) <= '" & deDateTo.DateTime.ToString("yyyyMMdd") & "' "

                        'If chkIsPulang.Checked = True Then
                        '    SQL &= "AND ISNULL((SELECT TOP 1 'Y' FROM DATABASERME..S_RINGKASANKELUARRAWATINAP WHERE KDREG = B.KDPENDAFTARAN), '') <> '' "
                        'Else
                        '    SQL &= "AND ISNULL((SELECT TOP 1 'Y' FROM DATABASERME..S_RINGKASANKELUARRAWATINAP WHERE KDREG = B.KDPENDAFTARAN), '') = '' "
                        'End If

                        If chkIsPulang.Checked = True Then
                            SQL &= "AND ISNULL((SELECT TOP 1 'Y' FROM DATABASERS..T_UPDATE_TANGGAL_PULANG WHERE KDPENDAFTARAN = B.KDPENDAFTARAN), '') <> '' "
                        Else
                            SQL &= "AND ISNULL((SELECT TOP 1 'Y' FROM DATABASERS..T_UPDATE_TANGGAL_PULANG WHERE KDPENDAFTARAN = B.KDPENDAFTARAN), '') = '' "
                        End If
                    End If
                    SQL &= ") Z "
                    'SQL &= "WHERE ISNULL((SELECT KDIDENTITAS FROM DATABASERME..A_IDENTITASPASIEN_LIST WHERE Z.NoRegister = KDKUNJUNGAN), 0) <> 0 "
                    SQL &= "ORDER BY Z.KDDAFTAR_L6, Z.NoRegister ASC "


                    oComm.Connection = oConn
                    oComm.CommandText = SQL
                    oComm.CommandTimeout = 120
                    oComm.CommandType = CommandType.Text

                    da = New SqlDataAdapter(oComm)
                    da.Fill(ds, "S_LISTPASIEN")

                    grdListPasien.DataSource = ds.Tables("S_LISTPASIEN")
                    grdListPasien.ForceInitialize()

                    If oConn.State = ConnectionState.Open Then
                        oConn.Close()
                    End If

                    fn_LoadFormatData()
                End If

            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)

            MsgBox("Load List Pasien Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatData()
        Try
            If btnType.Text <> "Rawat Jalan" Then

                grvListPasien.Columns("DATE").Caption = "Tanggal Datang"
                grvListPasien.Columns("Antrian").Caption = "Bed"
                grvListPasien.Columns("WaktuPeriksa").Caption = "Ruangan"
                grvListPasien.Columns("Dokter").Caption = "DPJP Utama"

                For iLoop As Integer = 0 To grvListPasien.Columns.Count - 1
                    If grvListPasien.Columns(iLoop).ColumnType.Name = "Decimal" Then
                        grvListPasien.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        grvListPasien.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                        grvListPasien.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                        grvListPasien.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, grvListPasien.Columns(iLoop).FieldName, grvListPasien.Columns(iLoop),
                                             "{0:n2}")
                        grvListPasien.Columns(iLoop).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                        grvListPasien.Columns(iLoop).SummaryItem.DisplayFormat = "{0:n2}"
                    ElseIf grvListPasien.Columns(iLoop).ColumnType.Name = "DateTime" Then
                        grvListPasien.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                        grvListPasien.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm}"
                    End If
                Next

                grvListPasien.Columns("KODEBED").VisibleIndex = -1
                grvListPasien.Columns("DATE").VisibleIndex = -1
                grvListPasien.Columns("Keterangan").VisibleIndex = -1
                grvListPasien.Columns("PENJAMIN").VisibleIndex = -1
                grvListPasien.Columns("KDPENDAFTARAN_AWAL").VisibleIndex = -1
                grvListPasien.Columns("KDDIAGNOSA").VisibleIndex = -1
                grvListPasien.Columns("PENJAMIN").Caption = "Penjamin"
                grvListPasien.Columns("KodeBooking").VisibleIndex = -1
                grvListPasien.Columns("NoRegister").VisibleIndex = -1
                grvListPasien.Columns("SIP").VisibleIndex = -1
                grvListPasien.Columns("KDCUSTOMER").VisibleIndex = -1
                grvListPasien.Columns("USIA").VisibleIndex = -1
                grvListPasien.Columns("TANGGALLAHIR").VisibleIndex = -1
                grvListPasien.Columns("DIAGNOSA").VisibleIndex = -1
                grvListPasien.Columns("NOMORSEP").VisibleIndex = -1
                grvListPasien.Columns("KARTUBPJS").VisibleIndex = -1
                grvListPasien.Columns("Cek").VisibleIndex = -1
                grvListPasien.Columns("Cek2").VisibleIndex = -1
                grvListPasien.Columns("KDDOCTOR").VisibleIndex = -1
                grvListPasien.Columns("JK").VisibleIndex = -1
                grvListPasien.Columns("ISPERAWAT").VisibleIndex = -1
                grvListPasien.Columns("WaktuSelisih").VisibleIndex = -1
                grvListPasien.Columns("KDDAFTAR_L6").VisibleIndex = -1
                grvListPasien.Columns("KDPENDAFTARAN").VisibleIndex = -1
                grvListPasien.Columns("KDKELASRAWAT").VisibleIndex = -1
                grvListPasien.Columns("Antrian").VisibleIndex = -1
                grvListPasien.Columns("CATEGORY").VisibleIndex = -1
                grvListPasien.Columns("CekSudahAdaCPPT").VisibleIndex = -1

                If chkPemetaan.Checked = True Then
                    grvListPasien.Columns("BED").VisibleIndex = 0
                    grvListPasien.Columns("KDUPDATE_APLICARE").VisibleIndex = -1
                Else
                    grvListPasien.Columns("BED").VisibleIndex = -1
                    grvListPasien.Columns("KDUPDATE_APLICARE").VisibleIndex = -1
                End If
            Else
                grvListPasien.Columns("DATE").Caption = "Jam Pendaftaran"
                grvListPasien.Columns("WaktuPeriksa").Caption = "Waktu Periksa"

                For iLoop As Integer = 0 To grvListPasien.Columns.Count - 1
                    If grvListPasien.Columns(iLoop).ColumnType.Name = "Decimal" Then
                        grvListPasien.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        grvListPasien.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                        grvListPasien.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                        grvListPasien.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, grvListPasien.Columns(iLoop).FieldName, grvListPasien.Columns(iLoop),
                                             "{0:n2}")
                        grvListPasien.Columns(iLoop).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                        grvListPasien.Columns(iLoop).SummaryItem.DisplayFormat = "{0:n2}"
                    ElseIf grvListPasien.Columns(iLoop).ColumnType.Name = "DateTime" Then
                        grvListPasien.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                        grvListPasien.Columns(iLoop).DisplayFormat.FormatString = "{0:HH:mm:ss}"
                    End If
                Next

                grvListPasien.Columns("KDDIAGNOSA").VisibleIndex = -1
                grvListPasien.Columns("PENJAMIN").Caption = "Penjamin"
                grvListPasien.Columns("KodeBooking").VisibleIndex = -1
                grvListPasien.Columns("NoRegister").VisibleIndex = -1
                grvListPasien.Columns("Dokter").VisibleIndex = -1
                grvListPasien.Columns("SIP").VisibleIndex = -1
                grvListPasien.Columns("KDCUSTOMER").VisibleIndex = -1
                grvListPasien.Columns("USIA").VisibleIndex = -1
                grvListPasien.Columns("TANGGALLAHIR").VisibleIndex = -1
                grvListPasien.Columns("DIAGNOSA").VisibleIndex = -1
                grvListPasien.Columns("NOMORSEP").VisibleIndex = -1
                grvListPasien.Columns("KARTUBPJS").VisibleIndex = -1
                grvListPasien.Columns("Cek").VisibleIndex = -1
                grvListPasien.Columns("Cek2").VisibleIndex = -1
                grvListPasien.Columns("KDDOCTOR").VisibleIndex = -1
                grvListPasien.Columns("JK").VisibleIndex = -1
                grvListPasien.Columns("ISPERAWAT").VisibleIndex = -1
                grvListPasien.Columns("WaktuSelisih").VisibleIndex = -1
                grvListPasien.Columns("KDDAFTAR_L6").VisibleIndex = -1
                grvListPasien.Columns("KDPENDAFTARAN").VisibleIndex = -1
                grvListPasien.Columns("KDKELASRAWAT").VisibleIndex = -1
                grvListPasien.Columns("CATEGORY").VisibleIndex = -1
            End If
        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
        End Try
    End Sub
    Private Sub Rata()
        Try
            Dim totalMenit As Integer = 0
            Dim jumlahBaris As Integer = 0
            Dim TotalAntrian As Integer = 0
            Dim TotalTerlayani As Integer = 0

            For i As Integer = 0 To grvListPasien.RowCount - 1
                TotalAntrian += 1
                If CBool(grvListPasien.GetRowCellValue(i, "Cek")) = True Then
                    TotalTerlayani += 1
                End If

                If grvListPasien.GetRowCellValue(i, "WaktuPeriksa") <> "" Then
                    'jumlahBaris += 1
                    'totalMenit += grvListPasien.GetRowCellValue(i, "WaktuSelisih")

                    totalMenit += Convert.ToInt32(grvListPasien.GetRowCellValue(i, "WaktuSelisih"))
                    jumlahBaris += 1

                End If
            Next

            lblTotalAntrian.Text = "Total Antrian : " & TotalAntrian
            lblTotalTelayani.Text = "Total Terlayani : " & TotalTerlayani

            Dim rataRata As Double = totalMenit / jumlahBaris
            Dim jam As Integer = Math.Floor(rataRata / 60)
            Dim menit As Integer = CInt(Math.Round(rataRata Mod 60))

            txtLayananRataRata.Text = $"{jam} jam {menit} menit"

            If rataRata >= 50 Then
                'txtLayananRataRata.ForeColor = Color.Yellow
                txtLayananRataRata.BackColor = Color.Red
            Else
                'txtLayananRataRata.ForeColor = Color.Yellow
                txtLayananRataRata.BackColor = Color.LawnGreen
            End If
            'Console.WriteLine($"Rata-rata waktu tunggu: {jam} jam {menit} menit")

            'Dim desimalJam As Double = sJumlahWaktuMenit / sJumlahPasien

            'Dim jam As Integer = Math.Floor(desimalJam)
            'Dim menit As Integer = CInt(Math.Round((desimalJam - jam) * 60))

            'txtLayananRataRata.Text = $"{jam} jam {menit} menit"


        Catch ex As Exception

        End Try
    End Sub
    Private Sub fn_LoadHystori(ByVal nopasien As String)
        Try
            grvHystori.OptionsSelection.MultiSelect = True
            grvHystori.SelectAll()
            grvHystori.DeleteSelectedRows()
            grvHystori.OptionsSelection.MultiSelect = False

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

            SQL = "SELECT * FROM ( "
            SQL &= "SELECT "
            SQL &= "Kategori = CONVERT(VARCHAR(8), A.DATE, 103) + ', ' + A.KDDEPARTMENT_NAMA + ', ' + A.KDDOCTOR_NAMA "
            'SQL &= ",Tipe = CASE C.SEQ WHEN 0 THEN 'Primer' ELSE 'Sekunder' END "
            SQL &= ",Tipe = 'Diagnosa' "
            SQL &= ",NoRegister = A.KDKUNJUNGAN "
            SQL &= ",NoTransaksi = B.KDCPPT "
            SQL &= ",Deskripsi = B.ASSEMENT_TEXT "
            'SQL &= ",PILIH = CONVERT(BIT, 0) "
            SQL &= ",KDITEM = '' "
            SQL &= ",KDUOM = '' "
            SQL &= "FROM "
            SQL &= "A_IDENTITASPASIEN_LIST A "
            SQL &= "INNER JOIN R_CPPT B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            'SQL &= "INNER JOIN R_CPPT_DIAGNOSA C "
            'SQL &= "ON B.KDCPPT = C.KDCPPT "
            SQL &= "WHERE A.KDCUSTOMER = '" & nopasien & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "Kategori = CONVERT(VARCHAR(8), A.DATE, 103) + ', ' + A.KDDEPARTMENT_NAMA + ', ' + A.KDDOCTOR_NAMA "
            SQL &= ",Tipe ='XRESEP' "
            SQL &= ",NoRegister = A.KDKUNJUNGAN "
            SQL &= ",NoTransaksi = B.KDCPPT "
            SQL &= ",Deskripsi = C.NAMAOBAT + ', ' + C.SATUAN + ', ' + CONVERT(NVARCHAR(5), C.JUMLAH) + ', ' + C.REMARKS_DOKTER + ', ' + C.SIGNA + ', ' + C.CARAPAKAI "
            'SQL &= ",PILIH = CONVERT(BIT, 0) "
            SQL &= ",C.KDITEM "
            SQL &= ",C.KDUOM "
            SQL &= "FROM "
            SQL &= "A_IDENTITASPASIEN_LIST A "
            SQL &= "INNER JOIN R_CPPT B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "INNER JOIN R_CPPT_NONRACIKAN C "
            SQL &= "ON B.KDCPPT = C.KDCPPT "
            SQL &= "WHERE A.KDCUSTOMER = '" & nopasien & "' "
            SQL &= ") Z "
            SQL &= "ORDER BY Z.Kategori DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_HISTORY")

            BindingSource_all.DataSource = ds.Tables("S_HISTORY")

            grdHystori.DataSource = BindingSource_all
            grdHystori.ForceInitialize()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            fn_SetFormatHystori()

            grvHystori.BestFitColumns()

        Catch oErr As Exception
            MsgBox("Load History Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_SetFormatHystori()
        For iLoop As Integer = 0 To grvHystori.Columns.Count - 1
            If grvHystori.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvHystori.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvHystori.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grvHystori.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvHystori.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvHystori.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvHystori.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm:ss}"
            End If
        Next

        grvHystori.Columns("NoRegister").VisibleIndex = -1
        grvHystori.Columns("NoTransaksi").VisibleIndex = -1
        grvHystori.Columns("KDITEM").VisibleIndex = -1
        grvHystori.Columns("KDUOM").VisibleIndex = -1
        grvHystori.Columns("Kategori").Group()
        grvHystori.Columns("Tipe").Group()
        grvHystori.ExpandAllGroups()
    End Sub
    Private Sub btnHasilLaboratorium_BukaTutupList_Click(sender As Object, e As EventArgs) Handles btnHasilLaboratorium_BukaTutupList.Click
        If btnHasilLaboratorium_BukaTutupList.Text = "Tutup" Then
            btnHasilLaboratorium_BukaTutupList.Text = "Buka"
            lgrdPenunjang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            btnHasilLaboratorium_BukaTutupList.Text = "Tutup"
            lgrdPenunjang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub
    Private Sub fn_LoadDataUpload(ByVal RM As String)
        Try
            grvUpload.Columns.Clear()
            grdUpload.DataSource = Nothing
            grvUpload.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

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

            SQL = "EXEC PENUNJANG_REKAMMEDIS @KDCUSTOMER = '" & RM & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_SO_TRANSAKSI_D_HASIL_LABORATORIUM")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grdUpload.MainView = grvUpload
            grdUpload.DataSource = ds.Tables("S_SO_TRANSAKSI_D_HASIL_LABORATORIUM")
            grdUpload.ForceInitialize()

            SplashScreenManager.CloseForm(False)

            QyeryHasilLaboratorium_LoadFormatData()

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Load Hasil Penunjang" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub QyeryHasilLaboratorium_LoadFormatData()
        For iLoop As Integer = 0 To grvUpload.Columns.Count - 1
            If grvUpload.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvUpload.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvUpload.Columns(iLoop).DisplayFormat.FormatString = "{0:n0}"
                grvUpload.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvUpload.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvUpload.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvUpload.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm}"
            End If
        Next

        grvUpload.Columns("KDSOTRANSAKSI").Visible = False
        grvUpload.Columns("KDSOTRANSAKSI").OptionsColumn.ShowInCustomizationForm = False
        grvUpload.Columns("KDITEM").Visible = False
        grvUpload.Columns("KDITEM").OptionsColumn.ShowInCustomizationForm = False
        grvUpload.BestFitColumns()
    End Sub
    Private Sub fn_LoadFormatDataUpload()
        For iLoop As Integer = 0 To grvUpload.Columns.Count - 1
            If grvUpload.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvUpload.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvUpload.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grvUpload.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvUpload.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvUpload.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvUpload.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next
    End Sub
    Private Sub fn_DokterBersama(ByVal kdcustomer As String)
        'Try
        '    grvDokterBersama.OptionsSelection.MultiSelect = True
        '    grvDokterBersama.SelectAll()
        '    grvDokterBersama.DeleteSelectedRows()
        '    grvDokterBersama.OptionsSelection.MultiSelect = False

        '    Dim oConn As New SqlConnection
        '    Dim oComm As New SqlCommand
        '    Dim da As SqlDataAdapter
        '    Dim ds As New DataSet
        '    Dim SQL As String
        '    Dim sConn As String = sConnOld

        '    oConn = New SqlConnection(sConn)

        '    If oConn.State = ConnectionState.Closed Then
        '        oConn.Open()
        '    End If

        '    SQL = "SELECT "
        '    SQL &= "A.KDREG "
        '    SQL &= ",DARIDOKTER = C.NAME_DISPLAY "
        '    SQL &= ",KEPADADOKTER = B.NAME_DISPLAY "
        '    SQL &= ",KETERANGAN = 'DPJP KE ' + CONVERT(NVARCHAR(2), A.SEQ)  "
        '    SQL &= ",A.ISIKONSUL "
        '    SQL &= ",A.JAWABKONSUL "
        '    SQL &= ",A.SEQ "
        '    SQL &= ",KDDOCTOR_DARI "
        '    SQL &= ",KDDOCTOR_KEPADA "
        '    SQL &= ",TGLKONSUL = A.TANGGAL_ISIKONSUL "
        '    SQL &= ",A.ISIKONSUL "
        '    SQL &= ",TGLJAWAB = A.TANGGAL_JAWABKONSUL "
        '    SQL &= ",A.JAWABKONSUL "
        '    SQL &= "FROM "
        '    SQL &= "I_TRACKING_KDDOCTOR A "
        '    SQL &= "INNER JOIN M_DOCTOR B "
        '    SQL &= "ON A.KDDOCTOR_KEPADA = B.KDDOCTOR "
        '    SQL &= "INNER JOIN M_DOCTOR C "
        '    SQL &= "ON A.KDDOCTOR_DARI = C.KDDOCTOR "
        '    SQL &= "INNER JOIN S_PENDAFTARAN_H D "
        '    SQL &= "ON A.KDREG = D.KDREG  "
        '    SQL &= "WHERE "
        '    SQL &= "D.KDCUSTOMER = '" & kdcustomer & "' "

        '    oComm.Connection = oConn
        '    oComm.CommandText = SQL
        '    oComm.CommandTimeout = 120
        '    oComm.CommandType = CommandType.Text

        '    da = New SqlDataAdapter(oComm)
        '    da.Fill(ds, "I_TRACKING_KDDOCTOR")

        '    grdDokterBersama.DataSource = ds.Tables("I_TRACKING_KDDOCTOR")
        '    grdDokterBersama.ForceInitialize()

        '    If oConn.State = ConnectionState.Open Then
        '        oConn.Close()
        '    End If

        '    grvDokterBersama.BestFitColumns()

        'Catch oErr As Exception
        '    MsgBox("Load Tracking Dokter : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub
    Private Sub fn_LoadPdfViewerCPPTRANANP()
        Try
            PdfViewerCPPTRawatInap.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data CPPT Rawat Inap.....")

            If txtKDCUSTOMER.Text = String.Empty Then Exit Sub

            Dim FolderSimpan = "C:/CPPTRANAP/"

            If Not Directory.Exists(FolderSimpan) Then
                Directory.CreateDirectory(FolderSimpan)
            Else
                DeleteDirectory(FolderSimpan)
                Directory.CreateDirectory(FolderSimpan)
            End If

            Dim oCPPT As New Transaksi.clsCPPT
            Dim dataList As New List(Of Byte())
            Dim dsLoad() As Byte
            Dim ds = oCPPT.GetDataByRMRANAP(txtKDCUSTOMER.Text)

            If ds.Count > 0 Then
                Dim rpt As New xtraDigital_CPPT_01_RawatInap

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.bindingSource.DataSource = ds
                rpt.ExportToPdf(FolderSimpan & "HASIL" & ".pdf")
                dataList.Add(File.ReadAllBytes(FolderSimpan & "HASIL" & ".pdf"))

                If dataList.Count > 0 Then
                    dsLoad = MergeFilesByte(dataList)
                    Dim stream As New MemoryStream(dsLoad)
                    PdfViewerCPPTRawatInap.LoadDocument(stream)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak CPPT Ranap" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function MergeFilesByte(ByVal sourceFiles As List(Of Byte())) As Byte()
        Try
            Dim mergedPdf As Byte() = Nothing
            Using ms As New MemoryStream()
                Using document As New Document()
                    Using copy As New PdfCopy(document, ms)
                        document.Open()
                        For i As Integer = 0 To sourceFiles.Count - 1
                            Dim reader As New PdfReader(sourceFiles(i))
                            ' loop over the pages in that document
                            Dim n As Integer = reader.NumberOfPages
                            Dim page As Integer = 0
                            While page <n
                                           page= page + 1
                                copy.AddPage(copy.GetImportedPage(reader, page))
                            End While
                        Next
                    End Using
                End Using

                mergedPdf = ms.ToArray()

                Return mergedPdf

            End Using
        Catch ex As Exception
            MergeFilesByte = Nothing
            MsgBox("Load Merge Data : " & vbCrLf & ex.Message, MsgBoxStyle.Information, Me.Text)
        End Try
    End Function
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
            SQL &= "B.* "
            SQL &= ",PROFESI = ISNULL((SELECT MEMO FROM R_CPPT_M_PROFESI WHERE B.KDPROFESI = KDPROFESI), '') "
            SQL &= "FROM "
            SQL &= "A_IDENTITASPASIEN_LIST A "
            SQL &= "INNER JOIN R_CPPT B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE A.KDCUSTOMER = '" & sKDCUSTOMER & "' "
            SQL &= "AND B.ISDELETE = 0 "
            'SQL &= "AND A.CATEGORY = " & Category & " "
            If chkCPPTDokter.Checked = False Then
                SQL &= "AND ISNULL((SELECT MEMO FROM R_CPPT_M_PROFESI WHERE B.KDPROFESI = KDPROFESI), '') LIKE '%DOKTER%' "
            End If

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "HISTORY_CPPT")

            Dim listCPPT As New List(Of DataAccess.R_CPPT)
            Dim Identitas As Integer = 0
            Dim oCppt_HandOver_Pemberi As New EMedrek.clsCppt_HandOver_Pemberi
            Dim oCppt_HandOver_Penerima As New EMedrek.clsCppt_HandOver_Penerima

            Dim oCppt_NilaiKritis_Pemberi As New EMedrek.clsCppt_NilaiKritis_Pemberi
            Dim oCppt_NilaiKritis_Penerima As New EMedrek.clsCppt_NilaiKritis_Penerima

            Dim oCppt_SBAR_Pemberi As New EMedrek.clsCppt_SBAR_Pemberi
            Dim oCppt_SBAR_Penerima As New EMedrek.clsCppt_SBAR_Penerima

            Dim oCppt_Verifikasi As New EMedrek.clsCppt_Verifikasi

            For iLoop As Integer = 0 To ds.Tables("HISTORY_CPPT").Rows.Count - 1
                Dim dsRekap As New DataAccess.R_CPPT
                With ds.Tables("HISTORY_CPPT")
                    Identitas = .Rows(iLoop)("KDIDENTITAS")
                    dsRekap.DATECREATED = .Rows(iLoop)("DATECREATED")
                    dsRekap.DATEUPDATED = .Rows(iLoop)("DATEUPDATED")
                    dsRekap.DATE = .Rows(iLoop)("DATE")
                    dsRekap.KDIDENTITAS = .Rows(iLoop)("KDIDENTITAS")
                    dsRekap.KDCPPT = .Rows(iLoop)("KDCPPT")
                    dsRekap.KDPROFESI = .Rows(iLoop)("PROFESI")
                    dsRekap.SUBJEKTIF_KELUHANUTAMA = .Rows(iLoop)("SUBJEKTIF_KELUHANUTAMA")
                    dsRekap.SUBJEKTIF_ALERGI_TIDAK = .Rows(iLoop)("SUBJEKTIF_ALERGI_TIDAK")
                    dsRekap.SUBJEKTIF_ALERGI_YA = .Rows(iLoop)("SUBJEKTIF_ALERGI_YA")
                    dsRekap.SUBJEKTIF_ALERGI_YA_TEXT = .Rows(iLoop)("SUBJEKTIF_ALERGI_YA_TEXT")
                    dsRekap.SUBJEKTIF_TEXT = .Rows(iLoop)("SUBJEKTIF_TEXT")
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

                    Dim listCPPTCatatan As New List(Of String)
                    Dim listCPPTHandOver As New List(Of String)
                    Dim listCPPTNilaiKritis As New List(Of String)
                    Dim listCPPTSBAR As New List(Of String)

                    Dim dsCPPTPemberi = oCppt_HandOver_Pemberi.GetData(dsRekap.KDCPPT)
                    If dsCPPTPemberi IsNot Nothing Then
                        listCPPTHandOver.Add("Pemberi Hand Over " & dsCPPTPemberi.KDUSER & vbCrLf & "Tanggal " & dsCPPTPemberi.DATE.ToString("dd-MM-yyyy HH:mm:ss"))
                    End If

                    Dim dsCPPTPenerima = oCppt_HandOver_Penerima.GetData(dsRekap.KDCPPT)
                    If dsCPPTPenerima IsNot Nothing Then
                        listCPPTHandOver.Add("Penerima Hand Over " & dsCPPTPenerima.KDUSER & vbCrLf & "Tanggal " & dsCPPTPenerima.DATE.ToString("dd-MM-yyyy HH:mm:ss"))
                    End If

                    Dim dsCPPTNilaiKritisPemberi = oCppt_NilaiKritis_Pemberi.GetData(dsRekap.KDCPPT)
                    If dsCPPTNilaiKritisPemberi IsNot Nothing Then
                        listCPPTNilaiKritis.Add("Pemberi Nilai Kritis " & dsCPPTNilaiKritisPemberi.KDUSER & vbCrLf & "Tanggal " & dsCPPTNilaiKritisPemberi.DATE.ToString("dd-MM-yyyy HH:mm:ss"))
                    End If

                    Dim dsCPPTNilaiKritisPenerima = oCppt_NilaiKritis_Penerima.GetData(dsRekap.KDCPPT)
                    If dsCPPTNilaiKritisPenerima IsNot Nothing Then
                        listCPPTNilaiKritis.Add("Penerima Nilai Kritis " & dsCPPTNilaiKritisPenerima.KDUSER & vbCrLf & "Tanggal " & dsCPPTNilaiKritisPenerima.DATE.ToString("dd-MM-yyyy HH:mm:ss"))
                    End If

                    Dim dsCPPTSBARPemberi = oCppt_SBAR_Pemberi.GetData(dsRekap.KDCPPT)
                    If dsCPPTSBARPemberi IsNot Nothing Then
                        listCPPTSBAR.Add("Pemberi SBAR " & dsCPPTSBARPemberi.KDUSER & vbCrLf & "Tanggal " & dsCPPTSBARPemberi.DATE.ToString("dd-MM-yyyy HH:mm:ss"))
                    End If

                    Dim dsCPPTSBARPenerima = oCppt_SBAR_Penerima.GetData(dsRekap.KDCPPT)
                    If dsCPPTSBARPenerima IsNot Nothing Then
                        listCPPTSBAR.Add("Penerima SBAR " & dsCPPTSBARPenerima.KDUSER & vbCrLf & "Tanggal " & dsCPPTSBARPenerima.DATE.ToString("dd-MM-yyyy HH:mm:ss"))
                    End If

                    If listCPPTHandOver.Count > 0 Then
                        listCPPTCatatan.Add("========================" & vbCrLf & String.Join(vbCrLf, listCPPTHandOver.ToArray) & vbCrLf & "========================")
                    End If

                    If listCPPTNilaiKritis.Count > 0 Then
                        listCPPTCatatan.Add("========================" & vbCrLf & String.Join(vbCrLf, listCPPTNilaiKritis.ToArray) & vbCrLf & "========================")
                    End If

                    If listCPPTSBAR.Count > 0 Then
                        listCPPTCatatan.Add("========================" & vbCrLf & String.Join(vbCrLf, listCPPTSBAR.ToArray) & vbCrLf & "========================")
                    End If

                    dsRekap.CATATAN = String.Join(vbCrLf, listCPPTCatatan.ToArray)

                    dsRekap.KDUSER = .Rows(iLoop)("KDUSER")
                    dsRekap.ISDELETE = .Rows(iLoop)("ISDELETE")
                    dsRekap.DATEDELETE = .Rows(iLoop)("DATEDELETE")


                    Dim dsVerifikasi = oCppt_Verifikasi.GetData(dsRekap.KDCPPT)
                    If dsVerifikasi IsNot Nothing Then
                        dsRekap.USERDELETE = "========================" & vbCrLf & "Verifikasi" & vbCrLf & dsVerifikasi.KDUSER & vbCrLf & "Tanggal " & dsVerifikasi.DATE.ToString("dd-MM-yyyy HH:mm:ss") & "========================"
                    Else
                        dsRekap.USERDELETE = ""
                    End If

                    listCPPT.Add(dsRekap)
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            If chkCek.Checked = False Then
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

                    'MYSQL = "SELECT "
                    'MYSQL &= "a.KUNJUNGAN "
                    'MYSQL &= ",a.TANGGAL as tanggal "
                    'MYSQL &= ",a.SUBYEKTIF "
                    'MYSQL &= ",a.OBYEKTIF "
                    'MYSQL &= ",a.ASSESMENT "
                    'MYSQL &= ",a.PLANNING "
                    'MYSQL &= ",a.INSTRUKSI "
                    'MYSQL &= ",d.NAMA "
                    'MYSQL &= "From medicalrecord.cppt as a "
                    'MYSQL &= "INNER Join pendaftaran.kunjungan as b "
                    'MYSQL &= "On a.KUNJUNGAN = b.NOMOR "
                    'MYSQL &= "INNER Join pendaftaran.pendaftaran as c "
                    'MYSQL &= "On b.NOPEN = c.NOMOR "
                    'MYSQL &= "INNER JOIN aplikasi.pengguna as d "
                    'MYSQL &= "On a.OLEH = d.ID "
                    MYSQL = sQueryMySQL
                    MYSQL &= " WHERE c.NORM = '" & CInt(sKDCUSTOMER) & "' "

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
                            dsRekap.KDPROFESI = "DOKTER"
                            dsRekap.SUBJEKTIF_KELUHANUTAMA = 0
                            dsRekap.SUBJEKTIF_ALERGI_TIDAK = 0
                            dsRekap.SUBJEKTIF_ALERGI_YA = 0
                            dsRekap.SUBJEKTIF_ALERGI_YA_TEXT = ""
                            dsRekap.SUBJEKTIF_TEXT = CleanHtmlToPlainText(.Rows(iLoop)("SUBYEKTIF"))
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
                            dsRekap.OBJEKTIF_TEXT = CleanHtmlToPlainText(.Rows(iLoop)("OBYEKTIF"))
                            dsRekap.ASSEMENT_INDIKASI = 0
                            dsRekap.ASSEMENT_TEXT = CleanHtmlToPlainText(.Rows(iLoop)("ASSESMENT"))
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
            End If

            sFind1_cppt = String.Empty
            sFind2_cppt = String.Empty
            sFind3_cppt = String.Empty

            If listCPPT.Count > 0 Then
                sFind1_cppt = txtKDCUSTOMER.Text
                sFind2_cppt = txtNAMAPASIEN.Text
                sFind3_cppt = deDATETANGGALLAHIR.DateTime.ToString("dd-MM-yyyy")

                Dim FolderSimpan = "C:/SIMRS/CPPT/"

                If Not Directory.Exists(FolderSimpan) Then
                    Directory.CreateDirectory(FolderSimpan)
                Else
                    DeleteDirectory(FolderSimpan)
                    Directory.CreateDirectory(FolderSimpan)
                End If

                Dim AlamatCPPT As String = FolderSimpan & sKDCUSTOMER & Now.ToString("yyyyMMddHHmmss") & ".pdf"

                Dim rpt As New xtraDigital_CPPT_01_QR

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.bindingSource.DataSource = listCPPT.OrderByDescending(Function(x) x.DATE)
                rpt.ExportToPdf(AlamatCPPT)

                If FileIO.FileSystem.FileExists(AlamatCPPT) Then
                    PdfViewerCPPTRawatJalan.LoadDocument(AlamatCPPT)
                End If

            End If

            sFind1_cppt = String.Empty
            sFind2_cppt = String.Empty
            sFind3_cppt = String.Empty

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Load Report CPPT" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Function CleanHtmlToPlainText(html As String) As String
        Try
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
        Catch ex As Exception
            CleanHtmlToPlainText = html
        End Try
    End Function
    Private Sub fn_LoadDocument(ByVal KDREG As String, ByVal RM As String)
        If isLoad_Splash = False Then Exit Sub

        PdfViewerHasilPenunjang.CloseDocument()
        PdfViewerCPPTRawatJalan.CloseDocument()
        PdfViewerResume.CloseDocument()
        PdfViewerAsesmenMedis.CloseDocument()
        PdfViewerAsesmenNakes.CloseDocument()
        PdfViewerCPPTRawatInap.CloseDocument()
        PdfViewerAssemenAwalMedisRawatInap.CloseDocument()
        PdfViewerTransferInternal.CloseDocument()

        If RM = "" Then Exit Sub

        Select Case XtraTabControl1.SelectedTabPage.Name
            'tabCPPT
            'tab6
            'tabInformasiObat
            'tabResume
            'tabPemeriksaanPenunjang
            'tabAsesmenMedis
            'tabAsesmenPerawat
            'tabGerdQ
            'tab8
            'tab9
            'tabLaporanOperasi
            'tabTransferInternal

            Case "tabCPPT"
                fn_LoadHistoryPasienCPPT(RM)
            Case "tabCPPTRI"
                fn_LoadPdfViewerCPPTRANANP()
            Case "tabInformasiObat"
                SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

                SplashScreenManager.Default.SetWaitFormCaption("Processing data List Resume.....")

                QueryResume_LoadHistory(RM)

                SplashScreenManager.CloseForm(False)
            Case "tabResume"
                SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

                SplashScreenManager.Default.SetWaitFormCaption("Processing data List Resume.....")

                QueryResume_LoadHistory(RM)

                SplashScreenManager.CloseForm(False)
            Case "tabPemeriksaanPenunjang"
                fn_LoadDataUpload(RM)
            Case "tabAsesmenMedis"
                QueryAsemenMedis_LoadHistoryPDF(RM)
            Case "tabAsesmenPerawat"
                QueryAsemenKeperawatan_LoadHistoryPDF(RM)
        End Select

        'If XtraTabControl1.SelectedTabPageIndex = 0 Then

        'ElseIf XtraTabControl1.SelectedTabPageIndex = 1 Then
        '    fn_LoadHistoryPasienCPPT(RM, 1)
        'ElseIf XtraTabControl1.SelectedTabPageIndex = 2 Then
        '    fn_LoadHystori(RM)
        'ElseIf XtraTabControl1.SelectedTabPageIndex = 3 Then
        '    SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

        '    SplashScreenManager.Default.SetWaitFormCaption("Processing data List Resume.....")

        '    QueryResume_LoadHistory(txtKDCUSTOMER.Text)

        '    SplashScreenManager.CloseForm(False)
        'ElseIf XtraTabControl1.SelectedTabPageIndex = 4 Then
        '    'Laboratorim / Penunjang
        '    fn_LoadDataUpload(RM)
        'ElseIf XtraTabControl1.SelectedTabPageIndex = 5 Then
        '    SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

        '    SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesemen Awal Medis IGD.....")
        '    fn_PrintStrukAsesemenMedisIGD(RM)

        '    SplashScreenManager.CloseForm(False)
        'ElseIf XtraTabControl1.SelectedTabPageIndex = 6 Then
        '    SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

        '    SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesemen Awal Perawatan IGD.....")
        '    fn_PrintStrukAsesemenPerawatIGD(RM)

        '    SplashScreenManager.CloseForm(False)
        'ElseIf XtraTabControl1.SelectedTabPageIndex = 7 Then
        '    'Try
        '    '    Dim FolderSimpan = "C:/SIMRS/CPPT"

        '    '    If Not Directory.Exists(FolderSimpan) Then
        '    '        Directory.CreateDirectory(FolderSimpan)
        '    '    Else
        '    '        DeleteDirectory(FolderSimpan)
        '    '        Directory.CreateDirectory(FolderSimpan)
        '    '    End If

        '    '    SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

        '    '    SplashScreenManager.Default.SetWaitFormCaption("Processing data Tabel Diagnosis GERD-Q.....")

        '    '    Dim oIGD As New Inventory.clsDigital_IGD_01_GERD

        '    '    Dim dataList As New List(Of Byte())
        '    '    Dim dsLoad() As Byte

        '    '    For Each xloop In oIGD.GetDataByRM(txtKDCUSTOMER.Text)
        '    '        Dim ds = oIGD.GetData(xloop.KDGERD)
        '    '        If ds IsNot Nothing Then
        '    '            Dim rpt As New xtraGerd_Q

        '    '            rpt.ShowPrintMarginsWarning = False
        '    '            rpt.Watermark.Text = sWATERMARK

        '    '            rpt.bindingSource.DataSource = ds
        '    '            rpt.ExportToPdf(FolderSimpan & ds.KDGERD & ".pdf")
        '    '            dataList.Add(File.ReadAllBytes(FolderSimpan & ds.KDGERD & ".pdf"))
        '    '        End If
        '    '    Next

        '    '    If dataList.Count > 0 Then
        '    '        dsLoad = oSetKoneksi.MergeFilesByte(dataList)
        '    '        Dim stream As New MemoryStream(dsLoad)
        '    '        pdfGERD.LoadDocument(stream)
        '    '    End If

        '    '    SplashScreenManager.CloseForm(False)
        '    'Catch oErr As Exception
        '    '    SplashScreenManager.CloseForm(False)
        '    '    MsgBox("Cetak Gerd Q IGD" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    'End Try
        'ElseIf XtraTabControl1.SelectedTabPageIndex = 8 Then
        '    'SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

        '    'SplashScreenManager.Default.SetWaitFormCaption("Processing Konsultasi.....")
        '    'fn_DokterBersama(RM)

        '    'SplashScreenManager.CloseForm(False)
        'ElseIf XtraTabControl1.SelectedTabPageIndex = 9 Then
        '    'SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

        '    'SplashScreenManager.Default.SetWaitFormCaption("Processing data Assemen Awal Medis Rawat Inap.....")

        '    'Try
        '    '    Dim oDigital_DischargePlanning As New Transaksi.clsDigital_DischargePlanning

        '    '    Dim FolderSimpan = "C:/SIMRS/ASESMENAWALMEDISRI"

        '    '    If Not Directory.Exists(FolderSimpan) Then
        '    '        Directory.CreateDirectory(FolderSimpan)
        '    '    Else
        '    '        DeleteDirectory(FolderSimpan)
        '    '        Directory.CreateDirectory(FolderSimpan)
        '    '    End If

        '    '    Dim dataList As New List(Of Byte())
        '    '    Dim dsLoad() As Byte

        '    '    For Each xloop In oDigital_DischargePlanning.GetDataByRM(RM)
        '    '        Dim ds = oDigital_DischargePlanning.GetData(xloop.KDREG)
        '    '        If ds IsNot Nothing Then
        '    '            NAMA = txtNAMAPASIEN.Text
        '    '            JENISKELAMIN = ""
        '    '            TANGGALLAHIR = deDATETANGGALLAHIR.DateTime.ToString("dd-MM-yyyy")
        '    '            sKDUSER_TTD = ds.KDUSER

        '    '            Dim rpt As New xtraReportEMedrekRI_13

        '    '            rpt.ShowPrintMarginsWarning = False
        '    '            rpt.Watermark.Text = sWATERMARK

        '    '            rpt.BindingSource.DataSource = ds
        '    '            rpt.ExportToPdf(FolderSimpan & ds.KDREG & ".pdf")
        '    '            dataList.Add(File.ReadAllBytes(FolderSimpan & ds.KDREG & ".pdf"))
        '    '        End If
        '    '    Next

        '    '    If dataList.Count > 0 Then
        '    '        dsLoad = oSetKoneksi.MergeFilesByte(dataList)
        '    '        Dim stream As New MemoryStream(dsLoad)
        '    '        PdfViewerAssemenAwalMedisRawatInap.LoadDocument(stream)
        '    '    Else
        '    '        PdfViewerAssemenAwalMedisRawatInap.CloseDocument()
        '    '    End If
        '    'Catch oErr As Exception
        '    '    MsgBox("Cetak CPPT" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    'End Try

        '    'SplashScreenManager.CloseForm(False)
        'ElseIf XtraTabControl1.SelectedTabPageIndex = 10 Then
        '    'Try
        '    '    grvLaporanOperasi.OptionsSelection.MultiSelect = True
        '    '    grvLaporanOperasi.SelectAll()
        '    '    grvLaporanOperasi.DeleteSelectedRows()
        '    '    grvLaporanOperasi.OptionsSelection.MultiSelect = False

        '    '    If txtKDCUSTOMER.Text = "" Then Exit Sub

        '    '    Dim oConn As New SqlConnection
        '    '    Dim oComm As New SqlCommand
        '    '    Dim da As SqlDataAdapter
        '    '    Dim ds As New DataSet
        '    '    Dim SQL As String
        '    '    Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

        '    '    oConn = New SqlConnection(sConn)

        '    '    If oConn.State = ConnectionState.Closed Then
        '    '        oConn.Open()
        '    '    End If

        '    '    SQL = "SELECT "
        '    '    SQL &= "* "
        '    '    SQL &= "FROM ( "
        '    '    SQL &= "SELECT "
        '    '    SQL &= "KETERANGAN = 'LAPORAN OPERASI' "
        '    '    SQL &= ",KODE = A.KDLAPORANOPERASI "
        '    '    SQL &= ",TANGGAL = A.DATE "
        '    '    SQL &= ",A.JENIS_OPERASI "
        '    '    SQL &= ",[DOKTER OPERATOR] = A.DOKTER_NAMEDISPLAY "
        '    '    'SQL &= ",[DOKTER ANESTESI] = A.TextEdit4 "
        '    '    'SQL &= ",PEMBUATLAPORAN = A.KDUSER "
        '    '    SQL &= ",JENISTINDAKAN = A.PROSEDUR2 "
        '    '    SQL &= "FROM "
        '    '    SQL &= "S_DIGITAL_OK_LAPORANOPERASI A "
        '    '    SQL &= "WHERE A.KDCUSTOMER = '" & txtKDCUSTOMER.Text & "' "
        '    '    SQL &= "AND A.ISDELETE = '0' "

        '    '    SQL &= "UNION "

        '    '    SQL &= "SELECT "
        '    '    SQL &= "KETERANGAN = 'LAPORAN TINDAKAN' "
        '    '    SQL &= ",KODE = A.KDLAPORANTINDAKAN "
        '    '    SQL &= ",TANGGAL = A.DATE "
        '    '    SQL &= ",A.JENIS_OPERASI "
        '    '    SQL &= ",[DOKTER OPERATOR] = A.DOKTER_NAMEDISPLAY "
        '    '    'SQL &= ",[DOKTER ANESTESI] = '-' "
        '    '    'SQL &= ",PEMBUATLAPORAN = A.KDUSER "
        '    '    SQL &= ",JENISTINDAKAN = A.PROSEDUR2 "
        '    '    SQL &= "FROM "
        '    '    SQL &= "S_DIGITAL_OK_LAPORANTINDAKAN A "
        '    '    SQL &= "WHERE A.KDCUSTOMER = '" & txtKDCUSTOMER.Text & "' "
        '    '    SQL &= "AND A.ISDELETE = '0' "
        '    '    SQL &= ") X "
        '    '    SQL &= "ORDER BY X.TANGGAL DESC "

        '    '    oComm.Connection = oConn
        '    '    oComm.CommandText = SQL
        '    '    oComm.CommandTimeout = 120
        '    '    oComm.CommandType = CommandType.Text

        '    '    da = New SqlDataAdapter(oComm)
        '    '    da.Fill(ds, "S_DIGITAL_OK_LAPORANOPERASI")

        '    '    grdLaporanOperasi.DataSource = ds.Tables("S_DIGITAL_OK_LAPORANOPERASI")
        '    '    grdLaporanOperasi.ForceInitialize()

        '    '    If oConn.State = ConnectionState.Open Then
        '    '        oConn.Close()
        '    '    End If

        '    '    grvLaporanOperasi.Columns("KODE").Visible = False

        '    '    For iLoop As Integer = 0 To grvLaporanOperasi.Columns.Count - 1
        '    '        If grvLaporanOperasi.Columns(iLoop).ColumnType.Name = "Decimal" Then
        '    '            grvLaporanOperasi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        '    '            grvLaporanOperasi.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
        '    '            grvLaporanOperasi.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        '    '        ElseIf grvLaporanOperasi.Columns(iLoop).ColumnType.Name = "DateTime" Then
        '    '            grvLaporanOperasi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        '    '            grvLaporanOperasi.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm}"
        '    '        End If
        '    '    Next

        '    'Catch oErr As Exception
        '    '    MsgBox("Load Gerd Q : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    'End Try
        'ElseIf XtraTabControl1.SelectedTabPageIndex = 11 Then
        '    'Try
        '    '    SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

        '    '    SplashScreenManager.Default.SetWaitFormCaption("Processing data Transfer Internal.....")

        '    '    Dim oDigital As New Transaksi.clsTransferInternal

        '    '    Dim dataList As New List(Of Byte())
        '    '    Dim dsLoad() As Byte

        '    '    Dim FolderSimpan = "C:/TRANSFERINTERNAL/"

        '    '    If Not Directory.Exists(FolderSimpan) Then
        '    '        Directory.CreateDirectory(FolderSimpan)
        '    '    Else
        '    '        DeleteDirectory(FolderSimpan)
        '    '        Directory.CreateDirectory(FolderSimpan)
        '    '    End If

        '    '    For Each xloop In oDigital.GetDataByRM(txtKDCUSTOMER.Text)
        '    '        Dim ds = oDigital.GetData(xloop.KDTRANSFERINTERNAL)
        '    '        If ds IsNot Nothing Then
        '    '            Dim rpt As New xtraTransferInternal
        '    '            rpt.ShowPrintMarginsWarning = False
        '    '            rpt.Watermark.Text = sWATERMARK
        '    '            rpt.bindingSource.DataSource = ds
        '    '            rpt.ExportToPdf(FolderSimpan & ds.KDTRANSFERINTERNAL & ".pdf")
        '    '            dataList.Add(File.ReadAllBytes(FolderSimpan & ds.KDTRANSFERINTERNAL & ".pdf"))
        '    '        End If
        '    '    Next

        '    '    If dataList.Count > 0 Then
        '    '        dsLoad = MergeFilesByte(dataList)
        '    '        Dim stream As New MemoryStream(dsLoad)
        '    '        PdfViewerTransferInternal.LoadDocument(stream)
        '    '    End If

        '    '    SplashScreenManager.CloseForm(False)

        '    'Catch oErr As Exception
        '    '    SplashScreenManager.CloseForm(False)
        '    '    MsgBox("Cetak Transfer Internal" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    'End Try
        'End If
    End Sub
#End Region
#Region "Command Button"
    'Private Sub btnOrderLaboratorium_Click(sender As Object, e As EventArgs)
    'Dim frmOrderPenunjangLab As New frmOrderPenunjangLab
    'Try
    '    frmOrderPenunjangLab.LoadMe("LABORATORIUM", sNoidSimpan, grdDPJP.EditValue, txtOBJEKTIF_BERATBADAN.Text, txtOBJEKTIF_TINGGIBADAN.Text)
    '    frmOrderPenunjangLab.ShowDialog(Me)
    'Catch ex As Exception
    '    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
    'Finally
    '    If Not frmOrderPenunjangLab Is Nothing Then frmOrderPenunjangLab.Dispose()
    '    frmOrderPenunjangLab = Nothing

    '    If listOrderLab.Count > 0 Then
    '        grvTindakan.Focus()
    '        grvTindakan.AddNewRow()
    '        grvTindakan.SetFocusedRowCellValue(colKETERANGAN_, listOrderLab)
    '        grvTindakan.UpdateCurrentRow()
    '    End If
    '    If listOrderRad.Count > 0 Then
    '        grvTindakan.Focus()
    '        grvTindakan.AddNewRow()
    '        grvTindakan.SetFocusedRowCellValue(colKETERANGAN_, listOrderRad)
    '        grvTindakan.UpdateCurrentRow()
    '    End If
    'End Try
    'End Sub
    'Private Sub btnOrderRadiologi_Click(sender As Object, e As EventArgs)
    'Dim frmOrderPenunjangLab As New frmOrderPenunjangLab
    'Try
    '    frmOrderPenunjangLab.LoadMe("RADIOLOGI", sNoidSimpan, grdDPJP.EditValue, txtOBJEKTIF_BERATBADAN.Text, txtOBJEKTIF_TINGGIBADAN.Text)
    '    frmOrderPenunjangLab.ShowDialog(Me)
    'Catch ex As Exception
    '    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
    'Finally
    '    If Not frmOrderPenunjangLab Is Nothing Then frmOrderPenunjangLab.Dispose()
    '    frmOrderPenunjangLab = Nothing

    '    If listOrderLab.Count > 0 Then
    '        grvTindakan.Focus()
    '        grvTindakan.AddNewRow()
    '        grvTindakan.SetFocusedRowCellValue(colKETERANGAN_, listOrderLab)
    '        grvTindakan.UpdateCurrentRow()
    '    End If
    '    If listOrderRad.Count > 0 Then
    '        grvTindakan.Focus()
    '        grvTindakan.AddNewRow()
    '        grvTindakan.SetFocusedRowCellValue(colKETERANGAN_, listOrderRad)
    '        grvTindakan.UpdateCurrentRow()
    '    End If
    'End Try
    'End Sub
    Private Sub btnJawabKonsul_Click(sender As Object, e As EventArgs) Handles btnJawabKonsul.Click
        'If grvDokterBersama.GetFocusedRowCellValue("SEQ") = 0 Then
        '    MsgBox("Silahkan Pilih data", MsgBoxStyle.Exclamation, Me.Text)
        '    Exit Sub
        'End If

        'Dim frmKonsulDokter As New frmKonsulDokter
        'Try
        '    frmKonsulDokter.fn_LoadMe(grvDokterBersama.GetFocusedRowCellValue("KDREG"), grvDokterBersama.GetFocusedRowCellValue("KDDOCTOR_DARI"), grvDokterBersama.GetFocusedRowCellValue("KDDOCTOR_KEPADA"), grvDokterBersama.GetFocusedRowCellValue("SEQ"), grvDokterBersama.GetFocusedRowCellValue("ISIKONSUL"), grvDokterBersama.GetFocusedRowCellValue("JAWABKONSUL"), grvDokterBersama.GetFocusedRowCellValue("TGLKONSUL"), grvDokterBersama.GetFocusedRowCellValue("TGLJAWAB"))
        '    frmKonsulDokter.ShowDialog(Me)
        'Catch ex As Exception
        '    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        'Finally
        '    If Not frmKonsulDokter Is Nothing Then frmKonsulDokter.Dispose()
        '    frmKonsulDokter = Nothing
        '    fn_DokterBersama(txtKDCUSTOMER.Text)
        '    'If sKONSULTASI <> "" Then
        '    '    txtINTRUKSILAIN.Text = txtINTRUKSILAIN.Text & vbCrLf & "Konsultasi Ke Dokter" & sKONSULTASI
        '    'End If
        'End Try
    End Sub
    Private Sub QueryResume_LoadHistory(ByVal NoRM As String)
        Try
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
            MsgBox("Load Data Resume" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
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
    Private Sub fn_PrintStrukAsesemenPerawatIGD(ByVal RM As String)
        'Try
        '    Dim oIGD As New Transaksi.clsDigital_IGD_02
        '    Dim FolderSimpan = "C:/SIMRS/MEDREK/"

        '    If Not Directory.Exists(FolderSimpan) Then
        '        Directory.CreateDirectory(FolderSimpan)
        '    Else
        '        DeleteDirectory(FolderSimpan)
        '        Directory.CreateDirectory(FolderSimpan)
        '    End If

        '    Dim dataList As New List(Of Byte())
        '    Dim dsLoad() As Byte

        '    For Each xloop In oIGD.GetDataByRM(RM)
        '        Dim ds = oIGD.GetData(xloop.KDPENDAFTARAN)
        '        If ds IsNot Nothing Then
        '            Dim rpt As New xtraReportAsesmenKeperawatanGD_New

        '            rpt.ShowPrintMarginsWarning = False
        '            rpt.Watermark.Text = sWATERMARK

        '            sASESEMEN_IGD = ds.SIMPANGAMBAR_1
        '            rpt.BindingSource.DataSource = ds
        '            rpt.ExportToPdf(FolderSimpan & ds.KDPENDAFTARAN & ".pdf")
        '            dataList.Add(File.ReadAllBytes(FolderSimpan & ds.KDPENDAFTARAN & ".pdf"))
        '        End If
        '    Next

        '    If dataList.Count > 0 Then
        '        dsLoad = oSetKoneksi.MergeFilesByte(dataList)
        '        Dim stream As New MemoryStream(dsLoad)
        '        PdfViewerPerawatIGD.LoadDocument(stream)
        '    End If

        'Catch oErr As Exception
        '    MsgBox("Cetak Asesemen Medis IGD" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub
    Private Sub fn_PrintStrukAsesemenMedisIGD(ByVal RM As String)
        'Try
        '    Dim oIGD As New Transaksi.clsDigital_IGD_01
        '    Dim FolderSimpan = "C:/SIMRS/MEDREK/"


        '    If Not Directory.Exists(FolderSimpan) Then
        '        Directory.CreateDirectory(FolderSimpan)
        '    Else
        '        DeleteDirectory(FolderSimpan)
        '        Directory.CreateDirectory(FolderSimpan)
        '    End If

        '    Dim ListdataKDCPPT As New List(Of String)
        '    Dim dataList As New List(Of Byte())
        '    Dim dsLoad() As Byte

        '    For Each xloop In oIGD.GetDataByRM(RM)
        '        Dim ds = oIGD.GetData(xloop.KDPENDAFTARAN)
        '        If ds IsNot Nothing Then
        '            sASESEMEN_IGD = ds.SURVEY_PERUT_1
        '            Dim rpt As New xtraReportFormulirIGD1

        '            rpt.ShowPrintMarginsWarning = False
        '            rpt.Watermark.Text = sWATERMARK

        '            ListdataKDCPPT.Add(ds.KDPENDAFTARAN)
        '            rpt.BindingSource.DataSource = ds
        '            rpt.ExportToPdf(FolderSimpan & ds.KDPENDAFTARAN & ".pdf")
        '            dataList.Add(File.ReadAllBytes(FolderSimpan & ds.KDPENDAFTARAN & ".pdf"))
        '        End If
        '    Next

        '    If dataList.Count > 0 Then
        '        dsLoad = oSetKoneksi.MergeFilesByte(dataList)
        '        Dim stream As New MemoryStream(dsLoad)
        '        PdfViewer_IGD.LoadDocument(stream)
        '    End If

        'Catch oErr As Exception
        '    MsgBox("Cetak Asesemen Medis IGD" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadDEPARTMENT(ByVal isRuangan As Boolean)
        Dim oDepartment As New Reference.clsDepartment
        Try
            grdKDDEPARTMENT.Properties.DataSource = oDepartment.GetData.Where(Function(x) x.ISACTIVE = True And x.ISRUANGRAWAT = isRuangan).ToList()
            grdKDDEPARTMENT.Properties.ValueMember = "KDDEPARTMENT"
            grdKDDEPARTMENT.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox("Load Poli Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDokter(ByVal Paramater As String)
        Try
            Dim oDoctor As New Reference.clsDoctor

            'Dim dsDoctor = From x In oDoctor.GetData
            '               Where x.ISACTIVE = True And x.KDDEPARTMENT = grdKDDEPARTMENT.EditValue
            '               Select x.KDDOCTOR, x.NAME_DISPLAY

            'grdDPJP.Properties.DataSource = dsDoctor.ToList()
            'grdDPJP.Properties.ValueMember = "KDDOCTOR"
            'grdDPJP.Properties.DisplayMember = "NAME_DISPLAY"


            If btnType.Text = "Rawat Jalan" Then
                If grdKDDEPARTMENT.Text = String.Empty Then Exit Sub

                Dim dsDoctor = From x In oDoctor.GetDataDetail_DEPARMENT
                               Where x.KDDEPARTMENT = grdKDDEPARTMENT.EditValue And x.M_DEPARTMENT.ISRUANGRAWAT = False And x.M_DOCTOR.ISACTIVE = True
                               Select x.KDDOCTOR, x.M_DOCTOR.NAME_DISPLAY

                grdDPJP.Properties.DataSource = dsDoctor.ToList()
                grdDPJP.Properties.ValueMember = "KDDOCTOR"
                grdDPJP.Properties.DisplayMember = "NAME_DISPLAY"


                grdDPJP.Text = Paramater
            Else
                Dim dsDoctor = From x In oDoctor.GetData
                               Where x.ISACTIVE = True
                               Select x.KDDOCTOR, x.NAME_DISPLAY

                grdDPJP.Properties.DataSource = dsDoctor.ToList()
                grdDPJP.Properties.ValueMember = "KDDOCTOR"
                grdDPJP.Properties.DisplayMember = "NAME_DISPLAY"


                grdDPJP.Text = Paramater
            End If

        Catch oErr As Exception
            MsgBox("Load Dokter Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grvListPasien_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvListPasien.FocusedRowChanged

        fn_EmptyMe()
        'NoRegister sama dengan nomor kunjungan

        If grvListPasien.GetFocusedRowCellValue("NoRegister") Is Nothing Then
            Exit Sub
        End If

        SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

        SplashScreenManager.Default.SetWaitFormCaption("Processing Load Pasien.....")

        txtKDKUNJUNGAN.Text = grvListPasien.GetFocusedRowCellValue("NoRegister")
        sKDPENDAFTARAN = grvListPasien.GetFocusedRowCellValue("KDPENDAFTARAN")
        sCategoryBilling = grvListPasien.GetFocusedRowCellValue("CATEGORY")
        sKelas = grvListPasien.GetFocusedRowCellValue("KDKELASRAWAT")

        deTANGGALDATANG.DateTime = grvListPasien.GetFocusedRowCellValue("DATE")
        txtKDCUSTOMER.Text = grvListPasien.GetFocusedRowCellValue("KDCUSTOMER")
        txtNAMAPASIEN.Text = grvListPasien.GetFocusedRowCellValue("Pasien")
        txtNOMORANTRIAN.Text = grvListPasien.GetFocusedRowCellValue("Antrian")
        deDATETANGGALLAHIR.DateTime = grvListPasien.GetFocusedRowCellValue("TANGGALLAHIR")
        txtOBJEKTIF_JENISKELAMIN.Text = IIf(grvListPasien.GetFocusedRowCellValue("JK") = "1", "Laki-laki", "Perempuan")
        txtOBJEKTIF_UMUR.Text = oRME2.GetUmurPasien(deTANGGALDATANG.DateTime, deDATETANGGALLAHIR.DateTime)
        txtPenjamin.Text = grvListPasien.GetFocusedRowCellValue("PENJAMIN")
        txtKARTUBPJS.Text = grvListPasien.GetFocusedRowCellValue("KARTUBPJS")
        txtKODEBOOKING.Text = grvListPasien.GetFocusedRowCellValue("KodeBooking")
        txtNOMORSEP.Text = grvListPasien.GetFocusedRowCellValue("NOMORSEP")

        If btnType.Text = "Rawat Jalan" Then
            sKODEBED = ""
        Else
            sKODEBED = grvListPasien.GetFocusedRowCellValue("KODEBED")
        End If

        SplashScreenManager.CloseForm(False)

        fn_LoadDocument(txtKDKUNJUNGAN.Text, txtKDCUSTOMER.Text)

    End Sub
    Private Sub PemeriksaanAwalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PemeriksaanAwalToolStripMenuItem.Click
        If grdKDDEPARTMENT.Text.Contains("GIZI") Then
            MsgBox("Belum tersedia", MsgBoxStyle.Exclamation, Me.Text)
        Else
            If txtKDKUNJUNGAN.Text = "" Then
                fn_EmptyMe()
                Exit Sub
            End If

            Dim ds = oGrouperDataCppt.GetDataByKdKunjungan(txtKDKUNJUNGAN.Text)
            If ds IsNot Nothing Then
                Try
                    frmReqAwalPemeriksaanList.fn_LoadData(txtKDCUSTOMER.Text, ds.KDIDENTITAS)
                    frmReqAwalPemeriksaanList.ShowDialog(Me)
                    fn_LoadSecurity()
                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                Finally
                    If Not frmReqAwalPemeriksaanList Is Nothing Then frmReqAwalPemeriksaanList.Dispose()
                    frmReqAwalPemeriksaanList = Nothing
                End Try
            Else
                MsgBox("Nomor Kunjungan Kosong", MsgBoxStyle.Exclamation, Me.Text)
            End If
        End If
    End Sub
    Private Sub AddToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddToolStripMenuItem.Click
        If txtKDKUNJUNGAN.Text <> "" Then
            If btnType.Text = "Rawat Inap" Then
                fn_BuatCPPT(sKODEBED, txtKDKUNJUNGAN.Text, grvListPasien.GetFocusedRowCellValue("WaktuPeriksa"), grvListPasien.GetFocusedRowCellValue("DPJPKeDua"), grvListPasien.GetFocusedRowCellValue("DPJPKeTiga"), grvListPasien.GetFocusedRowCellValue("DPJPKeEmpat"), grvListPasien.GetFocusedRowCellValue("DPJPKeLima"))
            Else
                fn_BuatCPPT("", txtKDKUNJUNGAN.Text, grdKDDEPARTMENT.Text, "", "", "", "")
            End If
        Else
            fn_EmptyMe()
            MsgBox("Nomor Kunjungan Kosong", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub grvListPasien_DoubleClick(sender As Object, e As EventArgs) Handles grvListPasien.DoubleClick
        If txtKDKUNJUNGAN.Text <> "" Then
            If btnType.Text = "Rawat Inap" Then
                fn_BuatCPPT(sKODEBED, txtKDKUNJUNGAN.Text, grvListPasien.GetFocusedRowCellValue("WaktuPeriksa"), grvListPasien.GetFocusedRowCellValue("DPJPKeDua"), grvListPasien.GetFocusedRowCellValue("DPJPKeTiga"), grvListPasien.GetFocusedRowCellValue("DPJPKeEmpat"), grvListPasien.GetFocusedRowCellValue("DPJPKeLima"))
            Else
                fn_BuatCPPT("", txtKDKUNJUNGAN.Text, grdKDDEPARTMENT.Text, "", "", "", "")
            End If
        Else
            fn_EmptyMe()
            MsgBox("Nomor Kunjungan Kosong", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub fn_BuatCPPT(ByVal kodebed As String, ByVal KDKUNJUNGAN_KODE As String, ByVal ruangan As String, ByVal dpjp2 As String, ByVal dpjp3 As String, ByVal dpjp4 As String, ByVal dpjp5 As String)
        Try
            Dim oPendaftaranKunjungan As New Admission.clsPendaftaran_Kunjungan
            Dim oDepartmentX As New Reference.clsDepartment
            Dim dsIdentitas = oGrouperDataCppt.GetDataByKdKunjungan(KDKUNJUNGAN_KODE)
            Dim sKDIDENTITAS_TEXT As Integer = 0

            lblFisio1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lblFisio2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lblFisio3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lblFisio4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lblFisio5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lblFisio6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lblFisio7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

            If dsIdentitas IsNot Nothing Then
                sKDIDENTITAS_TEXT = dsIdentitas.KDIDENTITAS

                Dim dsDepart = oDepartmentX.GetData(dsIdentitas.KDDEPARTMENT)
                If dsDepart IsNot Nothing Then
                    If dsDepart.VCLAIM_KODEPOLI = "IRM" Then
                        lblFisio1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        lblFisio2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        lblFisio3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        lblFisio4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        lblFisio5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        lblFisio6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        lblFisio7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    End If
                End If
            Else
                Dim dsPendaftaranKunjungan = oPendaftaranKunjungan.GetData(KDKUNJUNGAN_KODE)
                If dsPendaftaranKunjungan IsNot Nothing Then
                    Try
                        Dim dsList = oPendaftaran.GetStructureHeaderList()
                        With dsList
                            .DATECREATED = dsPendaftaranKunjungan.DATECREATED
                            .DATEUPDATED = dsPendaftaranKunjungan.DATECREATED
                            .KDIDENTITAS = 0
                            .CATEGORY = dsPendaftaranKunjungan.S_PENDAFTARAN_H.CATEGORY
                            .DATE = dsPendaftaranKunjungan.DATE
                            .KDKUNJUNGAN = dsPendaftaranKunjungan.KDKUNJUNGAN
                            .KDDAFTAR_L1 = dsPendaftaranKunjungan.S_PENDAFTARAN_H.KDDAFTAR_L1
                            .KDDAFTAR_L1_NAMA = dsPendaftaranKunjungan.S_PENDAFTARAN_H.M_DAFTAR_L1.MEMO
                            .KDCUSTOMER = dsPendaftaranKunjungan.S_PENDAFTARAN_H.KDCUSTOMER
                            .NAMAPASIEN = dsPendaftaranKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY
                            .KDDOCTOR = dsPendaftaranKunjungan.KDDOCTOR
                            .KDDOCTOR_NAMA = dsPendaftaranKunjungan.M_DOCTOR.NAME_DISPLAY
                            .KDDEPARTMENT = dsPendaftaranKunjungan.KDDEPARTMENT
                            .KDDEPARTMENT_NAMA = dsPendaftaranKunjungan.M_DEPARTMENT.NAME_DISPLAY
                            .CATATAN = ""
                            .STATUSWARNA = ""
                            .TANGGALLAHIR = dsPendaftaranKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR
                            .JENISKELAMIN = IIf(dsPendaftaranKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.KDJENISKELAMIN = 1, "LAKI-LAKI", "PEREMPUAN")
                            .UMUR = oPendaftaran.GetUmurPasien(.DATE, .TANGGALLAHIR)
                            .PANGKAT = dsPendaftaranKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.M_PANGKAT.MEMO
                            .KESATUAN = dsPendaftaranKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.M_KESATUAN.MEMO
                            .KELAS = dsPendaftaranKunjungan.S_PENDAFTARAN_H.M_KELASRAWAT.MEMO
                            .JENISPESERTA = dsPendaftaranKunjungan.S_PENDAFTARAN_H.M_DAFTAR_L2.MEMO
                            .ALAMAT = dsPendaftaranKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.ALAMAT
                            .AGAMA = dsPendaftaranKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.M_AGAMA.MEMO
                            .NIK = dsPendaftaranKunjungan.S_PENDAFTARAN_H.KTP
                            .KDPENDAFTARAN = dsPendaftaranKunjungan.KDPENDAFTARAN
                        End With

                        If oPendaftaran.InsertDataListRME(dsList) = False Then
                            MsgBox("Identitas Kosong (Gagal Insert Data)", MsgBoxStyle.Exclamation, Me.Text)
                            Exit Sub
                        Else
                            Dim dsIdentitas2 = oGrouperDataCppt.GetDataByKdKunjungan(txtKDKUNJUNGAN.Text)
                            If dsIdentitas2 IsNot Nothing Then
                                sKDIDENTITAS_TEXT = dsIdentitas2.KDIDENTITAS

                                Dim dsDepart = oDepartmentX.GetData(dsIdentitas.KDDEPARTMENT)
                                If dsDepart IsNot Nothing Then
                                    If dsDepart.VCLAIM_KODEPOLI = "IRM" Then
                                        lblFisio1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                                        lblFisio2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                                        lblFisio3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                                        lblFisio4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                                        lblFisio5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                                        lblFisio6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                                        lblFisio7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                                    End If
                                End If
                            Else
                                MsgBox("Identitas Kosong Ke 2", MsgBoxStyle.Exclamation, Me.Text)
                                Exit Sub
                            End If
                        End If
                    Catch ex As Exception
                        MsgBox("Identitas Kosong " & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                    End Try

                Else
                    MsgBox("Identitas Kosong", MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                End If
            End If

            If sKDIDENTITAS_TEXT = 0 Then
                MsgBox("Identitas Kosong", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim dsDaftaranCek = oPendaftaranKunjungan.GetData(KDKUNJUNGAN_KODE)

            If dsDaftaranCek IsNot Nothing Then
                Dim oGrouperRawatJalan As New Grouper.clsR_Identitas_Grouper
                Dim dsDataGrouper = oGrouperRawatJalan.GetStructureHeader
                Dim dsCariNosep = oGrouperRawatJalan.GetDataByNoRec(dsDaftaranCek.KDPENDAFTARAN)

                If dsCariNosep Is Nothing Then
                    With dsDataGrouper
                        If dsCariNosep Is Nothing Then
                            .kodegrouper = 0
                            .datecreated = dsDaftaranCek.S_PENDAFTARAN_H.DATECREATED
                        Else
                            .kodegrouper = dsCariNosep.kodegrouper
                            .datecreated = dsCariNosep.datecreated
                        End If
                        .dateupdated = dsDaftaranCek.S_PENDAFTARAN_H.DATEUPDATED
                        .jeniskelompokpasien = dsDaftaranCek.S_PENDAFTARAN_H.M_DAFTAR_L1.MEMO

                        If dsCariNosep Is Nothing Then
                            .statusenabled = "1"
                        Else
                            .statusenabled = dsCariNosep.statusenabled
                        End If

                        .norec = dsDaftaranCek.S_PENDAFTARAN_H.KDPENDAFTARAN
                        .nostruklastfk = dsDaftaranCek.S_PENDAFTARAN_H.KDPENDAFTARAN_AWAL
                        .jnsPelayanan = IIf(dsDaftaranCek.S_PENDAFTARAN_H.CATEGORY = 0, "R.Jalan", "R.Inap")
                        .kelasRawat = dsDaftaranCek.S_PENDAFTARAN_H.M_KELASRAWAT.MEMO
                        .noRm = dsDaftaranCek.S_PENDAFTARAN_H.KDCUSTOMER
                        .nama = dsDaftaranCek.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY
                        .noKartu = dsDaftaranCek.S_PENDAFTARAN_H.KARTUBPJS
                        .asalrujukan = IIf(dsDaftaranCek.S_PENDAFTARAN_H.ASALRUJUKAN = 0, 1, 2)
                        .noSep = dsDaftaranCek.S_PENDAFTARAN_H.NOMORSEP
                        .noRujukan = dsDaftaranCek.S_PENDAFTARAN_H.NOMORRUJUKAN
                        .Faskes = dsDaftaranCek.S_PENDAFTARAN_H.M_PPK.KODEFASKES & " " & dsDaftaranCek.S_PENDAFTARAN_H.M_PPK.MEMO
                        .dpjpkodevclaim = dsDaftaranCek.S_PENDAFTARAN_H.M_DOCTOR.VCLAIM_KDDPJP
                        .dpjp = dsDaftaranCek.S_PENDAFTARAN_H.M_DOCTOR.NAME_DISPLAY
                        .polikodevclaim = dsDaftaranCek.S_PENDAFTARAN_H.M_DEPARTMENT.VCLAIM_KODEPOLI
                        .poli = dsDaftaranCek.S_PENDAFTARAN_H.M_DEPARTMENT.NAME_DISPLAY
                        .catatan = dsDaftaranCek.S_PENDAFTARAN_H.CATATAN
                        .infromasiprb = dsDaftaranCek.S_PENDAFTARAN_H.INFORMASIPRB
                        .peserta = dsDaftaranCek.S_PENDAFTARAN_H.M_DAFTAR_L2.MEMO
                        .cob = dsDaftaranCek.S_PENDAFTARAN_H.M_COB.MEMO
                        .nomortelepon = dsDaftaranCek.S_PENDAFTARAN_H.NOMORTELEPON
                        .noregistrasi = dsDaftaranCek.S_PENDAFTARAN_H.KDPENDAFTARAN
                        .tglPlgSep = dsDaftaranCek.S_PENDAFTARAN_H.DATE
                        .tglSep = dsDaftaranCek.S_PENDAFTARAN_H.DATE
                        .tgl_lahir = dsDaftaranCek.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR
                        .gender = IIf(dsDaftaranCek.S_PENDAFTARAN_H.M_CUSTOMER.KDJENISKELAMIN = 0, "P", "L")
                        .diagnosaawal = dsDaftaranCek.S_PENDAFTARAN_H.M_DIAGNOSA.MEMO
                        If dsCariNosep Is Nothing Then
                            .status = ""
                        Else
                            .status = dsCariNosep.status
                        End If
                        .statuspasien = IIf(dsDaftaranCek.S_PENDAFTARAN_H.DATE.ToString("ddMMyyyy") = dsDaftaranCek.S_PENDAFTARAN_H.M_CUSTOMER.DATECREATED.ToString("ddMMyyy"), IIf(dsDaftaranCek.S_PENDAFTARAN_H.M_CUSTOMER.KDCUSTOMER_LAMA = "", "BARU", "LAMA"), "LAMA")
                        .alamat = dsDaftaranCek.S_PENDAFTARAN_H.M_CUSTOMER.ALAMAT

                        If dsCariNosep Is Nothing Then
                            .tglpulang = dsDaftaranCek.S_PENDAFTARAN_H.DATE
                        Else
                            .tglpulang = dsCariNosep.tglpulang
                        End If
                        .jsonpost = ""
                        .kduser = sUserID
                        .ruangan = IIf(dsDaftaranCek.S_PENDAFTARAN_H.CATEGORY = 0, "", dsDaftaranCek.S_PENDAFTARAN_H.M_DEPARTMENT.NAME_DISPLAY)
                        .pangkat = dsDaftaranCek.S_PENDAFTARAN_H.M_CUSTOMER.M_PANGKAT.MEMO
                        .kesatuan = dsDaftaranCek.S_PENDAFTARAN_H.M_CUSTOMER.M_KESATUAN.MEMO
                        .pendidikan = dsDaftaranCek.S_PENDAFTARAN_H.M_CUSTOMER.KDPENDIDIKAN
                        .statusmenikah = dsDaftaranCek.S_PENDAFTARAN_H.M_CUSTOMER.KDSTATUSKAWIN
                        .propinsi = dsDaftaranCek.S_PENDAFTARAN_H.M_CUSTOMER.M_KELURAHAN.M_KECAMATAN.M_KABUPATEN.M_PROPINSI.MEMO
                        .kabupaten = dsDaftaranCek.S_PENDAFTARAN_H.M_CUSTOMER.M_KELURAHAN.M_KECAMATAN.M_KABUPATEN.MEMO
                        .kecamatan = dsDaftaranCek.S_PENDAFTARAN_H.M_CUSTOMER.M_KELURAHAN.M_KECAMATAN.MEMO
                        .kelurahan = dsDaftaranCek.S_PENDAFTARAN_H.M_CUSTOMER.M_KELURAHAN.MEMO
                    End With

                    If oGrouperRawatJalan.InsertData(dsDataGrouper) = False Then
                        MsgBox("Maap List Grouping Tidak di Temukan", MsgBoxStyle.Exclamation, Me.Text)
                        fn_EmptyMe()
                        Exit Sub
                    End If
                    'Else
                    '    If oGrouperRawatJalan.UpdateData(dsDataGrouper) = False Then
                    '    End If
                End If
            Else
                MsgBox("Maap Registrasi Tidak di Temukan", MsgBoxStyle.Exclamation, Me.Text)
                fn_EmptyMe()
                Exit Sub
            End If

            If btnType.Text = "Rawat Inap" Then
                Try
                    Dim oRINGKASANKELUAR As New Transaksi.clsRingkasanKeluarRawatInap
                    Dim tanggalpulang As DateTime = Now
                    Dim TANGGALMASUK As DateTime = dsDaftaranCek.S_PENDAFTARAN_H.DATE

                    Dim dsCek = oRINGKASANKELUAR.GetData(dsDaftaranCek.KDPENDAFTARAN)
                    If dsCek IsNot Nothing Then
                        tanggalpulang = dsCek.TANGGALPULANG
                        TANGGALMASUK = dsCek.TANGGALMASUK
                    End If

                    Dim JenisKelamin As String = "-"

                    If dsDaftaranCek.S_PENDAFTARAN_H.M_CUSTOMER.KDJENISKELAMIN = "0" Then
                        JenisKelamin = "P"
                    ElseIf dsDaftaranCek.S_PENDAFTARAN_H.M_CUSTOMER.KDJENISKELAMIN = "1"
                        JenisKelamin = "L"
                    Else
                        JenisKelamin = dsDaftaranCek.S_PENDAFTARAN_H.M_CUSTOMER.KDJENISKELAMIN
                    End If

                    Dim frmRawatInapList As New frmRawatInapList
                    frmRawatInapList.fn_LoadMe(KDKUNJUNGAN_KODE, kodebed, False, sisDOkter, dsDaftaranCek.S_PENDAFTARAN_H.KDKELASRAWAT, dsDaftaranCek.S_PENDAFTARAN_H.KARTUBPJS, dsDaftaranCek.S_PENDAFTARAN_H.NOMORSEP, dsDaftaranCek.KDPENDAFTARAN, dsDaftaranCek.S_PENDAFTARAN_H.KDDOCTOR, ruangan, dsDaftaranCek.S_PENDAFTARAN_H.M_DAFTAR_L1.MEMO, dsDaftaranCek.S_PENDAFTARAN_H.KDCUSTOMER, dsDaftaranCek.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY, JenisKelamin, dsDaftaranCek.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR, TANGGALMASUK, dsDaftaranCek.S_PENDAFTARAN_H.M_DOCTOR.NAME_DISPLAY, dpjp2, dpjp3, dpjp4, dpjp5, dsDaftaranCek.S_PENDAFTARAN_H.M_DOCTOR.NAME_DISPLAY, dsDaftaranCek.S_PENDAFTARAN_H.KDPENDAFTARAN_AWAL, sKDIDENTITAS_TEXT, dsDaftaranCek.S_PENDAFTARAN_H.KDDOCTOR, tanggalpulang)
                    frmRawatInapList.ShowDialog(Me)

                    Load_SimpanCPPTSelesai(dsDaftaranCek.KDPENDAFTARAN)

                    'If sisDOkter = False Then
                    '    grvListPasien.SetFocusedRowCellValue("KDCPPT_PERAWAT", Load_SimpanCPPTSelesai(dsDaftaranCek.KDPENDAFTARAN))
                    'Else
                    '    grvListPasien.SetFocusedRowCellValue("KDCPPT", Load_SimpanCPPTSelesai(dsDaftaranCek.KDPENDAFTARAN))
                    'End If
                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                Finally
                    If Not frmRawatInapList Is Nothing Then frmRawatInapList.Dispose()
                    frmRawatInapList = Nothing

                    isLoad = False
                    sNoidSimpan = ""
                    lGROUPLISTPASIEN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    lGROUPFORM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    'lIDENTITASPASIEN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    lLISTANTRIAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    fn_EmptyMe()

                    fn_LoadSecurity()

                End Try
            Else
                lblBacaFarmasi.Text = "Belum Terima Farmasi"
                colKDITEM.OptionsColumn.AllowEdit = True
                colKDITEM.OptionsColumn.AllowFocus = True
                colKDITEM.OptionsColumn.ReadOnly = False
                colKDITEM.OptionsColumn.TabStop = True

                colQTY.OptionsColumn.AllowEdit = True
                colQTY.OptionsColumn.AllowFocus = True
                colQTY.OptionsColumn.ReadOnly = False
                colQTY.OptionsColumn.TabStop = True

                colREMARKS_DOKTER.OptionsColumn.AllowEdit = True
                colREMARKS_DOKTER.OptionsColumn.AllowFocus = True
                colREMARKS_DOKTER.OptionsColumn.ReadOnly = False
                colREMARKS_DOKTER.OptionsColumn.TabStop = True

                If sisDOkter = False Then
                    If grdKDDEPARTMENT.Text.Contains("GIZI") Then
                        MsgBox("Belum tersedia", MsgBoxStyle.Exclamation, Me.Text)
                    Else
                        Dim ds = oGrouperDataCppt.GetDataByKdKunjungan(KDKUNJUNGAN_KODE)
                        If ds IsNot Nothing Then
                            Try
                                frmReqAwalPemeriksaanList.fn_LoadData(ds.KDCUSTOMER, ds.KDIDENTITAS)
                                frmReqAwalPemeriksaanList.ShowDialog(Me)
                                fn_LoadSecurity()
                            Catch ex As Exception
                                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                            Finally
                                If Not frmReqAwalPemeriksaanList Is Nothing Then frmReqAwalPemeriksaanList.Dispose()
                                frmReqAwalPemeriksaanList = Nothing
                            End Try
                        Else
                            MsgBox("Nomor Kunjungan Kosong", MsgBoxStyle.Exclamation, Me.Text)
                        End If
                    End If
                Else
                    If dsDaftaranCek.S_PENDAFTARAN_H.KODEBOOKING <> "" Then
                        Try
                            Dim oSet_panggilan As New AntrianRS.clsSetPanggilSET_PANGGIL_ANTRIAN
                            Dim oAntrian As New SettingAntrian.clsSetAntrian
                            Dim dsKodebooking = oAntrian.GetData(dsDaftaranCek.S_PENDAFTARAN_H.KODEBOOKING)

                            If dsKodebooking IsNot Nothing Then
                                frmErmList.fn_TaskID(dsDaftaranCek.S_PENDAFTARAN_H.KODEBOOKING, 4)

                                If dsDaftaranCek.S_PENDAFTARAN_H.KDDAFTAR_L6 = "DAFTAR_L6_0000000002" Then
                                    ' ***** HEADER *****
                                    Dim dsSetPanggilan = oSet_panggilan.GetDataPoli(dsDaftaranCek.S_PENDAFTARAN_H.M_DEPARTMENT.VCLAIM_KODEPOLI)

                                    Dim ds = oSet_panggilan.GetStructureHeaderPoli

                                    With ds
                                        .LOKET = dsDaftaranCek.S_PENDAFTARAN_H.M_DEPARTMENT.VCLAIM_KODEPOLI
                                        .KODE = Microsoft.VisualBasic.Left(dsKodebooking.NOMORANTREAN, 3)
                                        .NOMORANTRIAN = IIf(dsKodebooking.ANGKAANTREAN = 0, Microsoft.VisualBasic.Right(dsKodebooking.NOMORANTREAN, 3), dsKodebooking.ANGKAANTREAN)
                                        .DESCRIPTION = "Antrian " & .KODE & " " & IIf(dsKodebooking.ANGKAANTREAN = 0, Microsoft.VisualBasic.Right(dsKodebooking.NOMORANTREAN, 3), dsKodebooking.ANGKAANTREAN) & " Atas Nama " & System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dsDaftaranCek.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY.ToLower()) & " Menuju " & IIf(dsDaftaranCek.S_PENDAFTARAN_H.M_DOCTOR.NAME_ONLINE = "", "Klinik " & System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase((dsDaftaranCek.S_PENDAFTARAN_H.M_DEPARTMENT.NAME_DISPLAY).Replace("KLINIk", "").ToLower()), " Dokter " & System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dsDaftaranCek.S_PENDAFTARAN_H.M_DOCTOR.NAME_ONLINE))
                                        .ISPANGGIL = False
                                        .SISA = 0
                                    End With

                                    If dsSetPanggilan Is Nothing Then
                                        oSet_panggilan.InsertDataPoli(ds)
                                    Else
                                        oSet_panggilan.UpdateDataPoli(ds)
                                    End If
                                ElseIf dsDaftaranCek.S_PENDAFTARAN_H.KDDAFTAR_L6 = "DAFTAR_L6_0000000003" Then
                                    ' ***** HEADER *****
                                    Dim dsSetPanggilan = oSet_panggilan.GetDataPoli(dsDaftaranCek.S_PENDAFTARAN_H.M_DEPARTMENT.VCLAIM_KODEPOLI)

                                    Dim ds = oSet_panggilan.GetStructureHeaderPoli

                                    With ds
                                        .LOKET = dsDaftaranCek.S_PENDAFTARAN_H.M_DEPARTMENT.VCLAIM_KODEPOLI
                                        .KODE = Microsoft.VisualBasic.Left(dsKodebooking.NOMORANTREAN, 3)
                                        .NOMORANTRIAN = IIf(dsKodebooking.ANGKAANTREAN = 0, Microsoft.VisualBasic.Right(dsKodebooking.NOMORANTREAN, 3), dsKodebooking.ANGKAANTREAN)
                                        .DESCRIPTION = "Panggilan Ulang Antrian " & .KODE & " " & IIf(dsKodebooking.ANGKAANTREAN = 0, Microsoft.VisualBasic.Right(dsKodebooking.NOMORANTREAN, 3), dsKodebooking.ANGKAANTREAN) & " Atas Nama " & System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dsDaftaranCek.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY.ToLower()) & " Menuju " & IIf(dsDaftaranCek.S_PENDAFTARAN_H.M_DOCTOR.NAME_ONLINE = "", "Klinik " & System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase((dsDaftaranCek.S_PENDAFTARAN_H.M_DEPARTMENT.NAME_DISPLAY).Replace("KLINIk", "").ToLower()), " Dokter " & System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dsDaftaranCek.S_PENDAFTARAN_H.M_DOCTOR.NAME_ONLINE))
                                        .ISPANGGIL = False
                                        .SISA = 0
                                    End With

                                    If dsSetPanggilan Is Nothing Then
                                        oSet_panggilan.InsertDataPoli(ds)
                                    Else
                                        oSet_panggilan.UpdateDataPoli(ds)
                                    End If
                                End If

                            Else
                                If dsDaftaranCek.S_PENDAFTARAN_H.KDDAFTAR_L6 = "DAFTAR_L6_0000000002" Then
                                    ' ***** HEADER *****
                                    Dim dsSetPanggilan = oSet_panggilan.GetDataPoli(dsDaftaranCek.S_PENDAFTARAN_H.M_DEPARTMENT.VCLAIM_KODEPOLI)

                                    Dim ds = oSet_panggilan.GetStructureHeaderPoli

                                    With ds
                                        .LOKET = dsDaftaranCek.S_PENDAFTARAN_H.M_DEPARTMENT.VCLAIM_KODEPOLI
                                        .KODE = dsDaftaranCek.M_DOCTOR.MEMO
                                        .NOMORANTRIAN = dsDaftaranCek.S_PENDAFTARAN_H.KDBOOKING
                                        .DESCRIPTION = "ANTRIAN ATAS NAMA " & dsDaftaranCek.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY & " MENUJU " & dsDaftaranCek.S_PENDAFTARAN_H.M_DEPARTMENT.NAME_DISPLAY.ToUpper & IIf(dsDaftaranCek.S_PENDAFTARAN_H.M_DOCTOR.NAME_ONLINE = "", "", " DOKTER " & dsDaftaranCek.S_PENDAFTARAN_H.M_DOCTOR.NAME_ONLINE.ToUpper)
                                        .ISPANGGIL = False
                                        .SISA = 0
                                    End With

                                    If dsSetPanggilan Is Nothing Then
                                        oSet_panggilan.InsertDataPoli(ds)
                                    Else
                                        oSet_panggilan.UpdateDataPoli(ds)
                                    End If
                                ElseIf dsDaftaranCek.S_PENDAFTARAN_H.KDDAFTAR_L6 = "DAFTAR_L6_0000000003" Then
                                    ' ***** HEADER *****
                                    Dim dsSetPanggilan = oSet_panggilan.GetDataPoli(dsDaftaranCek.S_PENDAFTARAN_H.M_DEPARTMENT.VCLAIM_KODEPOLI)

                                    Dim ds = oSet_panggilan.GetStructureHeaderPoli

                                    With ds
                                        .LOKET = dsDaftaranCek.S_PENDAFTARAN_H.M_DEPARTMENT.VCLAIM_KODEPOLI
                                        .KODE = dsDaftaranCek.M_DOCTOR.MEMO
                                        .NOMORANTRIAN = dsDaftaranCek.S_PENDAFTARAN_H.KDBOOKING
                                        .DESCRIPTION = "PANGGILAN ULANG ANTRIAN ATAS NAMA " & dsDaftaranCek.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY & " MENUJU " & dsDaftaranCek.S_PENDAFTARAN_H.M_DEPARTMENT.NAME_DISPLAY.ToUpper & IIf(dsDaftaranCek.S_PENDAFTARAN_H.M_DOCTOR.NAME_ONLINE = "", "", " DOKTER " & dsDaftaranCek.S_PENDAFTARAN_H.M_DOCTOR.NAME_ONLINE.ToUpper)
                                        .ISPANGGIL = False
                                        .SISA = 0
                                    End With

                                    If dsSetPanggilan Is Nothing Then
                                        oSet_panggilan.InsertDataPoli(ds)
                                    Else
                                        oSet_panggilan.UpdateDataPoli(ds)
                                    End If
                                End If
                            End If
                        Catch oErr As Exception
                            MsgBox("Simpan Panggil: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                        End Try
                    End If

                    fn_ChangeFormState()

                    lGROUPLISTPASIEN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    lGROUPFORM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                    If btnType.Text = "Rawat Jalan" Then
                        'Rawat Jalan
                        If chkLISTANTRIAN.Checked = True Then
                            fn_LoadDataList()
                            lLISTANTRIAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        End If

                        Dim dsKoding = oGrouperDataCppt.GetDataByKdKunjunganCPPTDokter(KDKUNJUNGAN_KODE)

                        If dsKoding IsNot Nothing Then
                            sNoidSimpan = dsKoding.KDCPPT
                            oFormModeCPPT = FORM_MODE.FORM_MODE_EDIT

                            fn_LoadData(sNoidSimpan)

                            fn_LoadKDITEMSearch()

                            Dim dsNomorRefrence = oOrder.GetDataNoReference(sNoidSimpan, "ORDER FARMASI")
                            If dsNomorRefrence IsNot Nothing Then
                                Dim dsCekSudahInput = oSalesOrderTransaksi.GetDataByOrderFarmasi(dsNomorRefrence.KDORDER)
                                If dsCekSudahInput IsNot Nothing Then
                                    lblBacaFarmasi.Text = "Sudah Terima Farmasi"
                                    colKDITEM.OptionsColumn.AllowEdit = False
                                    colKDITEM.OptionsColumn.AllowFocus = False
                                    colKDITEM.OptionsColumn.ReadOnly = True
                                    colKDITEM.OptionsColumn.TabStop = False

                                    colQTY.OptionsColumn.AllowEdit = False
                                    colQTY.OptionsColumn.AllowFocus = False
                                    colQTY.OptionsColumn.ReadOnly = True
                                    colQTY.OptionsColumn.TabStop = False

                                    colREMARKS_DOKTER.OptionsColumn.AllowEdit = False
                                    colREMARKS_DOKTER.OptionsColumn.AllowFocus = False
                                    colREMARKS_DOKTER.OptionsColumn.ReadOnly = True
                                    colREMARKS_DOKTER.OptionsColumn.TabStop = False

                                End If
                            End If

                        Else
                            sNoidSimpan = ""

                            grvCPPT_Tindakan.OptionsSelection.MultiSelect = True
                            grvCPPT_Tindakan.SelectAll()
                            grvCPPT_Tindakan.DeleteSelectedRows()
                            grvCPPT_Tindakan.OptionsSelection.MultiSelect = False

                            oFormModeCPPT = FORM_MODE.FORM_MODE_ADD
                            Dim dsReqAwalPemeriksaan = oGrouperDataCppt.GetDataByKodeKunjunganTerakhirTTVPerawat(KDKUNJUNGAN_KODE)

                            If sKDPENDAFTARAN.ToString.Contains("RJ") Then
                                Dim oItem As New Reference.clsItem

                                Dim dsItem = oItem.GetData("10138")
                                If dsItem IsNot Nothing Then
                                    grvCPPT_Tindakan.AddNewRow()
                                    grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN, dsItem.KDITEM)
                                    grvCPPT_Tindakan.UpdateCurrentRow()
                                Else
                                    Dim dsItemCPPTAuto = oItem.GetDataByAutodiCPPT()
                                    If dsItemCPPTAuto IsNot Nothing Then
                                        grvCPPT_Tindakan.AddNewRow()
                                        grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN, dsItemCPPTAuto.KDITEM)
                                        grvCPPT_Tindakan.UpdateCurrentRow()
                                    End If
                                End If
                            End If

                            fn_LoadTindakLanjutPertama(sKDPENDAFTARAN)

                            If dsReqAwalPemeriksaan IsNot Nothing Then
                                chkALERGI_YA.Checked = dsReqAwalPemeriksaan.SUBJEKTIF_ALERGI_YA
                                chkALERGI_TIDAK.Checked = dsReqAwalPemeriksaan.SUBJEKTIF_ALERGI_TIDAK
                                txtALERGIOBAT.Text = dsReqAwalPemeriksaan.SUBJEKTIF_ALERGI_YA_TEXT
                                txtSUBJEKTIF.Text = dsReqAwalPemeriksaan.SUBJEKTIF_KELUHANUTAMA
                                'txtOBJEKTIF_UMUR.Text = dsReqAwalPemeriksaan.
                                txtOBJEKTIF_BERATBADAN.Text = IIf(CDec(dsReqAwalPemeriksaan.OBJEKTIF_BERATBADAN) = 0, "", dsReqAwalPemeriksaan.OBJEKTIF_BERATBADAN)
                                txtOBJEKTIF_TINGGIBADAN.Text = IIf(CDec(dsReqAwalPemeriksaan.OBJEKTIF_TINGGIBADAN) = 0, "", dsReqAwalPemeriksaan.OBJEKTIF_TINGGIBADAN)
                                txtCPPT_Sistole.Text = dsReqAwalPemeriksaan.OBJEKTIF_SISTOLE
                                txtCPPT_Diastole.Text = dsReqAwalPemeriksaan.OBJEKTIF_DIASTOLE
                                txtOBJEKTIF_NADI.Text = dsReqAwalPemeriksaan.OBJEKTIF_HR
                                txtOBJEKTIF_RESPIRASI.Text = dsReqAwalPemeriksaan.OBJEKTIF_RR
                                txtOBJEKTIF_SATURASIOKSIGEN.Text = dsReqAwalPemeriksaan.OBJEKTIF_SPO2
                                txtOBJEKTIF_SUHU.Text = dsReqAwalPemeriksaan.OBJEKTIF_SUHU
                                txtDESKRIPSI.Text = dsReqAwalPemeriksaan.OBJEKTIF_PEMERIKSAAN
                                cboTINGKATKESADARAN.Text = dsReqAwalPemeriksaan.OBJEKTIF_KESADARAN
                                txtOBJEKTIF_IMT.Text = dsReqAwalPemeriksaan.OBJEKTIF_GCS
                                txtOBJEKTIF_BBIDEAL.Text = dsReqAwalPemeriksaan.OBJEKTIF_TAMPAKSAKIT
                                txtOBJEKTIF_BBDITURUNKAN.Text = dsReqAwalPemeriksaan.OBJEKTIF_VISUALANALOGSCORE

                                For Each xloop In oGrouperDataCppt.GetDataDetailTindakan(dsReqAwalPemeriksaan.KDCPPT)
                                    'Cek = True
                                    If xloop.KDITEM <> "10138" Then
                                        grvCPPT_Tindakan.Focus()
                                        grvCPPT_Tindakan.AddNewRow()
                                        grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN, xloop.KDITEM)
                                        grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_ISPERAWAT, True)
                                        grvCPPT_Tindakan.UpdateCurrentRow()

                                    End If
                                Next
                            Else
                                MsgBox("Belum Ada TTV Perawat", MsgBoxStyle.Exclamation, Me.Text)
                            End If

                            Dim dsAlergi = oGrouperDataCppt.GetDataByRmTerakhirTTVAlergi(dsDaftaranCek.S_PENDAFTARAN_H.KDCUSTOMER)
                            If dsAlergi IsNot Nothing Then
                                chkALERGI_YA.Checked = dsAlergi.SUBJEKTIF_ALERGI_YA
                                chkALERGI_TIDAK.Checked = dsAlergi.SUBJEKTIF_ALERGI_TIDAK
                                txtALERGIOBAT.Text = dsAlergi.SUBJEKTIF_ALERGI_YA_TEXT

                                txtALERGIOBAT.BackColor = Color.Red
                            Else
                                txtALERGIOBAT.BackColor = Color.White
                            End If

                            Dim dsTerakhirUser = oGrouperDataCppt.GetDataByRmTerakhirByUserAndDokter(dsDaftaranCek.S_PENDAFTARAN_H.KDCUSTOMER, sUserID)
                            If dsTerakhirUser IsNot Nothing Then
                                For Each xloop In oGrouperDataCppt.GetDataDetailDiagnosa(dsTerakhirUser.KDCPPT)
                                    grvDiagnosaPenyerta.AddNewRow()
                                    grvDiagnosaPenyerta.SetFocusedRowCellValue(colKETERANGAN, xloop.MEMO)
                                    grvDiagnosaPenyerta.SetFocusedRowCellValue(colKDDIAGNOSA, xloop.KDDIAGNOSA)
                                    grvDiagnosaPenyerta.SetFocusedRowCellValue(colKATEGORI, xloop.KATEGORI)
                                    grvDiagnosaPenyerta.UpdateCurrentRow()
                                Next
                            Else
                                Dim dsTerakhirDokter = oGrouperDataCppt.GetDataByRmTerakhirByDokter(dsDaftaranCek.S_PENDAFTARAN_H.KDCUSTOMER, grdDPJP.EditValue)
                                If dsTerakhirDokter IsNot Nothing Then
                                    For Each xloop In oGrouperDataCppt.GetDataDetailDiagnosa(dsTerakhirDokter.KDCPPT)
                                        grvDiagnosaPenyerta.AddNewRow()
                                        grvDiagnosaPenyerta.SetFocusedRowCellValue(colKETERANGAN, xloop.MEMO)
                                        grvDiagnosaPenyerta.SetFocusedRowCellValue(colKDDIAGNOSA, xloop.KDDIAGNOSA)
                                        grvDiagnosaPenyerta.SetFocusedRowCellValue(colKATEGORI, xloop.KATEGORI)
                                        grvDiagnosaPenyerta.UpdateCurrentRow()
                                    Next
                                Else
                                    Dim oDaftar As New Admission.clsPendaftaran
                                    Dim dsDaftar = oDaftar.GetData(sKDPENDAFTARAN)
                                    If dsDaftar IsNot Nothing Then
                                        grvDiagnosaPenyerta.AddNewRow()
                                        grvDiagnosaPenyerta.SetFocusedRowCellValue(colKETERANGAN, dsDaftar.M_DIAGNOSA.MEMO.Replace(dsDaftar.KDDIAGNOSA, "").Replace("-", "").ToString.Trim)
                                        grvDiagnosaPenyerta.SetFocusedRowCellValue(colKDDIAGNOSA, dsDaftar.KDDIAGNOSA)
                                        grvDiagnosaPenyerta.SetFocusedRowCellValue(colKATEGORI, "Primary")
                                        grvDiagnosaPenyerta.UpdateCurrentRow()
                                    End If
                                End If

                            End If

                        End If

                        Calculate()

                        isLoad = True

                        txtSUBJEKTIF.Focus()
                    Else
                        'Rawat Inap

                        grvListantrian.OptionsSelection.MultiSelect = True
                        grvListantrian.SelectAll()
                        grvListantrian.DeleteSelectedRows()
                        grvListantrian.OptionsSelection.MultiSelect = False

                        sNoidSimpan = ""
                        sNoidSimpancppt = ""

                        If MsgBox("Tekan Yes Buat CPPT Baru, No Edit CPPT ", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then
                            Try
                                frmCCPTDokterList.fn_LoadNoRM(txtKDCUSTOMER.Text)
                                frmCCPTDokterList.ShowDialog(Me)
                            Catch ex As Exception
                                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                            Finally
                                If Not frmCCPTDokterList Is Nothing Then frmCCPTDokterList.Dispose()
                                frmCCPTDokterList = Nothing

                                sNoidSimpan = sNoidSimpancppt
                            End Try
                        End If

                        If sNoidSimpan <> "" Then
                            oFormModeCPPT = FORM_MODE.FORM_MODE_EDIT
                            fn_LoadData(sNoidSimpan)
                        Else
                            sNoidSimpan = ""

                            grvCPPT_Tindakan.OptionsSelection.MultiSelect = True
                            grvCPPT_Tindakan.SelectAll()
                            grvCPPT_Tindakan.DeleteSelectedRows()
                            grvCPPT_Tindakan.OptionsSelection.MultiSelect = False

                            oFormModeCPPT = FORM_MODE.FORM_MODE_ADD
                            Dim dsReqAwalPemeriksaan = oGrouperDataCppt.GetDataByKodeKunjunganTerakhirTTVPerawat(txtKDKUNJUNGAN.Text)

                            fn_LoadTindakLanjutPertama(sKDPENDAFTARAN)

                            If dsReqAwalPemeriksaan IsNot Nothing Then
                                chkALERGI_YA.Checked = dsReqAwalPemeriksaan.SUBJEKTIF_ALERGI_YA
                                chkALERGI_TIDAK.Checked = dsReqAwalPemeriksaan.SUBJEKTIF_ALERGI_TIDAK
                                txtALERGIOBAT.Text = dsReqAwalPemeriksaan.SUBJEKTIF_ALERGI_YA_TEXT
                                txtSUBJEKTIF.Text = dsReqAwalPemeriksaan.SUBJEKTIF_KELUHANUTAMA
                                'txtOBJEKTIF_UMUR.Text = dsReqAwalPemeriksaan.
                                txtOBJEKTIF_BERATBADAN.Text = IIf(CDec(dsReqAwalPemeriksaan.OBJEKTIF_BERATBADAN) = 0, "", dsReqAwalPemeriksaan.OBJEKTIF_BERATBADAN)
                                txtOBJEKTIF_TINGGIBADAN.Text = IIf(CDec(dsReqAwalPemeriksaan.OBJEKTIF_TINGGIBADAN) = 0, "", dsReqAwalPemeriksaan.OBJEKTIF_TINGGIBADAN)
                                txtCPPT_Sistole.Text = dsReqAwalPemeriksaan.OBJEKTIF_SISTOLE
                                txtCPPT_Diastole.Text = dsReqAwalPemeriksaan.OBJEKTIF_DIASTOLE
                                txtOBJEKTIF_NADI.Text = dsReqAwalPemeriksaan.OBJEKTIF_HR
                                txtOBJEKTIF_RESPIRASI.Text = dsReqAwalPemeriksaan.OBJEKTIF_RR
                                txtOBJEKTIF_SATURASIOKSIGEN.Text = dsReqAwalPemeriksaan.OBJEKTIF_SPO2
                                txtOBJEKTIF_SUHU.Text = dsReqAwalPemeriksaan.OBJEKTIF_SUHU
                                txtDESKRIPSI.Text = dsReqAwalPemeriksaan.OBJEKTIF_PEMERIKSAAN
                                cboTINGKATKESADARAN.Text = dsReqAwalPemeriksaan.OBJEKTIF_KESADARAN
                                txtOBJEKTIF_IMT.Text = dsReqAwalPemeriksaan.OBJEKTIF_GCS
                                txtOBJEKTIF_BBIDEAL.Text = dsReqAwalPemeriksaan.OBJEKTIF_TAMPAKSAKIT
                                txtOBJEKTIF_BBDITURUNKAN.Text = dsReqAwalPemeriksaan.OBJEKTIF_VISUALANALOGSCORE

                                For Each xloop In oGrouperDataCppt.GetDataDetailTindakan(dsReqAwalPemeriksaan.KDCPPT)
                                    'Cek = True
                                    grvCPPT_Tindakan.Focus()
                                    grvCPPT_Tindakan.AddNewRow()
                                    grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN, xloop.KDITEM)
                                    grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_ISPERAWAT, True)
                                    grvCPPT_Tindakan.UpdateCurrentRow()
                                Next
                            Else
                                Dim dsReqAwalPemeriksaanDokter = oGrouperDataCppt.GetDataByRMUntukDokter(txtKDCUSTOMER.Text)
                                If dsReqAwalPemeriksaanDokter IsNot Nothing Then
                                    chkALERGI_YA.Checked = dsReqAwalPemeriksaanDokter.SUBJEKTIF_ALERGI_YA
                                    chkALERGI_TIDAK.Checked = dsReqAwalPemeriksaanDokter.SUBJEKTIF_ALERGI_TIDAK
                                    txtALERGIOBAT.Text = dsReqAwalPemeriksaanDokter.SUBJEKTIF_ALERGI_YA_TEXT
                                    txtSUBJEKTIF.Text = dsReqAwalPemeriksaanDokter.SUBJEKTIF_KELUHANUTAMA
                                    'txtOBJEKTIF_UMUR.Text = dsReqAwalPemeriksaanDokter.
                                    txtOBJEKTIF_BERATBADAN.Text = IIf(CDec(dsReqAwalPemeriksaanDokter.OBJEKTIF_BERATBADAN) = 0, "", dsReqAwalPemeriksaanDokter.OBJEKTIF_BERATBADAN)
                                    txtOBJEKTIF_TINGGIBADAN.Text = IIf(CDec(dsReqAwalPemeriksaanDokter.OBJEKTIF_TINGGIBADAN) = 0, "", dsReqAwalPemeriksaanDokter.OBJEKTIF_TINGGIBADAN)
                                    txtCPPT_Sistole.Text = dsReqAwalPemeriksaanDokter.OBJEKTIF_SISTOLE
                                    txtCPPT_Diastole.Text = dsReqAwalPemeriksaanDokter.OBJEKTIF_DIASTOLE
                                    txtOBJEKTIF_NADI.Text = dsReqAwalPemeriksaanDokter.OBJEKTIF_HR
                                    txtOBJEKTIF_RESPIRASI.Text = dsReqAwalPemeriksaanDokter.OBJEKTIF_RR
                                    txtOBJEKTIF_SATURASIOKSIGEN.Text = dsReqAwalPemeriksaanDokter.OBJEKTIF_SPO2
                                    txtOBJEKTIF_SUHU.Text = dsReqAwalPemeriksaanDokter.OBJEKTIF_SUHU
                                    txtDESKRIPSI.Text = dsReqAwalPemeriksaanDokter.OBJEKTIF_PEMERIKSAAN
                                    cboTINGKATKESADARAN.Text = dsReqAwalPemeriksaanDokter.OBJEKTIF_KESADARAN
                                    txtOBJEKTIF_IMT.Text = dsReqAwalPemeriksaanDokter.OBJEKTIF_GCS
                                    txtOBJEKTIF_BBIDEAL.Text = dsReqAwalPemeriksaanDokter.OBJEKTIF_TAMPAKSAKIT
                                    txtOBJEKTIF_BBDITURUNKAN.Text = dsReqAwalPemeriksaanDokter.OBJEKTIF_VISUALANALOGSCORE

                                    For Each xloop In oGrouperDataCppt.GetDataDetailTindakan(dsReqAwalPemeriksaanDokter.KDCPPT)
                                        'Cek = True
                                        grvCPPT_Tindakan.Focus()
                                        grvCPPT_Tindakan.AddNewRow()
                                        grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN, xloop.KDITEM)
                                        grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_ISPERAWAT, True)
                                        grvCPPT_Tindakan.UpdateCurrentRow()
                                    Next
                                End If
                            End If

                            If chkALERGI_YA.Checked = False Then
                                Dim dsAlergi = oGrouperDataCppt.GetDataByRmTerakhirTTVAlergi(txtKDCUSTOMER.Text)
                                If dsAlergi IsNot Nothing Then
                                    chkALERGI_YA.Checked = dsAlergi.SUBJEKTIF_ALERGI_YA
                                    chkALERGI_TIDAK.Checked = dsAlergi.SUBJEKTIF_ALERGI_TIDAK
                                    txtALERGIOBAT.Text = dsAlergi.SUBJEKTIF_ALERGI_YA_TEXT

                                    txtALERGIOBAT.BackColor = Color.Red
                                Else
                                    txtALERGIOBAT.BackColor = Color.White
                                End If
                            End If
                        End If

                        txtPenjamin.Focus()

                        isLoad = True
                    End If
                End If
            End If
        Catch oErr As Exception
            MsgBox("Buat CPPT" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub XtraTabControl1_SelectedPageChanged() Handles XtraTabControl1.SelectedPageChanged
        If txtKDKUNJUNGAN.Text = "" Then
            fn_EmptyMe()
            Exit Sub
        End If
        fn_LoadDocument(txtKDKUNJUNGAN.Text, txtKDCUSTOMER.Text)
    End Sub
    Private Sub tabControlList_SelectedPageChanged() Handles tabControlList.SelectedPageChanged
        If txtKDKUNJUNGAN.Text = "" Then
            MsgBox("Data Pasien Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Select Case tabControlList.SelectedTabPage.Name
            Case "tabUtama"

                pnlAsesmenKeperawatan_pic.Dock = DockStyle.Top

                'tabControlDokumen_SelectedPageChanged()
                lAsesmenPerawat_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lAsesmenPerawat_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lAsesmenKeperawatan_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                ClearFormAsesmenNakes()
                fn_EmptyAsesmenMedis()
                XtraTabControl1_SelectedPageChanged()
            Case "tabDokumen"
                tabControlDokumen_SelectedPageChanged()
        End Select
    End Sub
    Private Sub tabControlDokumen_SelectedPageChanged() Handles tabControlDokumen.SelectedPageChanged
        fn_LoadFormulirNakes()
        fn_LoadFormulirMedis()

        Select Case tabControlDokumen.SelectedTabPage.Name
            Case "tabDokumenUpload"
                fn_LoadDataScan()
            Case "tabDokumenAksi"
                tabControlDokumenAksi_SelectedPageChanged()
            Case "tabDokumenAsesmenNakes"
                QueryAsesmenPerawat_LoadHistory(txtKDCUSTOMER.Text)
            Case "tabDokumenAsesmenMedis"
                QueryAsemenMedis_LoadHistory(txtKDCUSTOMER.Text)
        End Select
    End Sub
    Private Sub tabControlDokumenAksi_SelectedPageChanged() Handles tabControlDokumenAksi.SelectedPageChanged
        Select Case tabControlDokumenAksi.SelectedTabPage.Name
            Case "tabDokumenAksiBilling"
                fn_LoadDataAksiBilling()
        End Select
    End Sub
    Private Sub tabControlPlanning_SelectedPageChanged() Handles tabControlPlanning.SelectedPageChanged
        Select Case tabControlPlanning.SelectedTabPage.Name
            Case "tabControltab3"
                If sKDPENDAFTARAN <> "" Then
                    LoadPenunjang(sKDPENDAFTARAN)
                End If
        End Select
    End Sub
    Private Sub grvUpload_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvUpload.FocusedRowChanged
        If grvUpload.GetFocusedRowCellValue("KDSOTRANSAKSI") Is Nothing Then
            PdfViewerHasilPenunjang.CloseDocument()
            Exit Sub
        End If

        If grvUpload.GetFocusedRowCellValue("KATEGORI") = "LABORATORIUM" Then
            xtraReportHasilLaboratorium(grvUpload.GetFocusedRowCellValue("KDSOTRANSAKSI"))
        ElseIf grvUpload.GetFocusedRowCellValue("KATEGORI") = "RADIOLOGI" Then
            xtraReportHasilExpertise(grvUpload.GetFocusedRowCellValue("KDSOTRANSAKSI"), grvUpload.GetFocusedRowCellValue("KDITEM"))
        Else
            xtraReportHasilUpload(grvUpload.GetFocusedRowCellValue("KDITEM"), grvUpload.GetFocusedRowCellValue("KATEGORI"))
        End If
    End Sub
    Private Sub xtraReportHasilLaboratorium(ByVal KDSOTRANSAKSI As String)
        Try
            PdfViewerHasilPenunjang.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Hasil Laboratorium.....")

            Dim oRME As New RME.clsRME

            Dim ds = oSalesOrderTransaksi.GetData(KDSOTRANSAKSI)
            If ds IsNot Nothing Then
                Dim FolderSimpan = "C:\PDF\RME\LABORATORIUM"
                If Not Directory.Exists(FolderSimpan) Then
                    Directory.CreateDirectory(FolderSimpan)
                Else
                    Try
                        DeleteDirectory(FolderSimpan)
                        Directory.CreateDirectory(FolderSimpan)
                    Catch ex As Exception

                    End Try
                End If

                If sHargaApotik = False Then
                    Dim rpt As New xtraHasilLabSementara

                    sSIPNIP = ds.S_SO_TRANSAKSI_D_HASIL_LABORATORIUMs.FirstOrDefault.M_DOCTOR.SIP
                    sUSIA = oRME.GetUmurPasien(ds.DATE, ds.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK
                    rpt.bindingSource.DataSource = ds

                    Dim alamatlab As String = FolderSimpan & "ZLAB" & ds.KDSOTRANSAKSI & Now.ToString("yyyyMMddHHmmss") & ".pdf"

                    rpt.ExportToPdf(alamatlab)

                    If FileIO.FileSystem.FileExists(alamatlab) Then
                        PdfViewerHasilPenunjang.LoadDocument(alamatlab)
                    End If
                Else
                    Dim rpt As New xtraHasilLabSementaraVersi2

                    sSIPNIP = ds.S_SO_TRANSAKSI_D_HASIL_LABORATORIUMs.FirstOrDefault.M_DOCTOR.SIP
                    sUSIA = oRME.GetUmurPasien(ds.DATE, ds.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK
                    rpt.bindingSource.DataSource = ds

                    Dim alamatlab As String = FolderSimpan & "ZLAB" & ds.KDSOTRANSAKSI & Now.ToString("yyyyMMddHHmmss") & ".pdf"

                    rpt.ExportToPdf(alamatlab)

                    If FileIO.FileSystem.FileExists(alamatlab) Then
                        PdfViewerHasilPenunjang.LoadDocument(alamatlab)
                    End If
                End If

            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Hasil Laboratorium" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub xtraReportHasilExpertise(ByVal KDSOTRANSAKSI As String, ByVal SEQ_SO As Integer)
        Try
            Dim oExpertise As New Grouper.clsExpertise
            Dim oRME As New RME.clsRME

            PdfViewerHasilPenunjang.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Hasil Expertise.....")

            Dim dataList As New List(Of Byte())
            Dim ds = oExpertise.GetData(KDSOTRANSAKSI, SEQ_SO)

            If ds IsNot Nothing Then
                Dim FolderSimpan = "C:\PDF\RME\RADIOLOGI"
                If Not Directory.Exists(FolderSimpan) Then
                    Directory.CreateDirectory(FolderSimpan)
                Else
                    Try
                        DeleteDirectory(FolderSimpan)
                        Directory.CreateDirectory(FolderSimpan)
                    Catch ex As Exception

                    End Try
                End If

                Dim Alamat As String = FolderSimpan & KDSOTRANSAKSI & SEQ_SO & Now.ToString("yyyyMMddHHmmss") & ".pdf"

                sUSIA = oRME.GetUmurPasien(ds.DATE, ds.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)

                Dim rpt As New xtraExpertise

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.bindingSource.DataSource = ds
                rpt.ExportToPdf(Alamat)

                If FileIO.FileSystem.FileExists(Alamat) Then
                    PdfViewerHasilPenunjang.LoadDocument(Alamat)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Hasil Expertise" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub xtraReportHasilUpload(ByVal alamat As String, ByVal Type As String)
        Try
            PdfViewerHasilPenunjang.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Hasil Expertise.....")

            If Type.Contains(".pdf") Then
                If IO.File.Exists(alamat) Then
                    PdfViewerHasilPenunjang.LoadDocument(alamat)
                End If
            Else
                Dim FolderSimpan = "C:/SIMRS/UPLOAD/"

                Try
                    If Not Directory.Exists(FolderSimpan) Then
                        Directory.CreateDirectory(FolderSimpan)
                    Else
                        DeleteDirectory(FolderSimpan)
                        Directory.CreateDirectory(FolderSimpan)
                    End If
                Catch ex As Exception

                End Try

                Dim Simpan As String = Now.ToString("yyyyMMddHHmmss") & "Upload.pdf"

                ConvertImageToPDF(alamat, FolderSimpan & Simpan)

                If IO.File.Exists(FolderSimpan & Simpan) Then
                    PdfViewerHasilPenunjang.LoadDocument(FolderSimpan & Simpan)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Hasil Upload" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub ConvertImageToPDF(ByVal alamat As String, ByVal outputPDF As String)
        If Not File.Exists(alamat) Then
            MessageBox.Show("File gambar tidak ditemukan: " & alamat)
            Exit Sub
        End If

        ' Buat dokumen PDF baru
        Dim doc As New Document(PageSize.A4)
        PdfWriter.GetInstance(doc, New FileStream(outputPDF, FileMode.Create))

        doc.Open()

        ' Tambahkan gambar dari file
        Dim img As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(alamat)

        ' Atur ukuran agar pas halaman (opsional)
        img.Alignment = Element.ALIGN_CENTER
        img.ScaleToFit(PageSize.A4.Width - 40, PageSize.A4.Height - 40)

        ' Tambahkan gambar ke halaman
        doc.Add(img)

        doc.Close()
        'MessageBox.Show("Berhasil membuat PDF di: " & outputPDF)
    End Sub
    '    Private Sub grvUpload_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grvUpload.DoubleClick
    '        If grvUpload.GetFocusedRowCellValue("AlamatUpload") Is Nothing Then
    '            Exit Sub
    '        End If

    '        If txtKDCUSTOMER.Text = "" Then
    '            Exit Sub
    '        End If

    '        If grvUpload.GetFocusedRowCellValue("Type") = "RONTGEN" Then
    '            'frmBrowseUpload.fn_LoadMe(2, grvUpload.GetFocusedRowCellValue("Type"), grvUpload.GetFocusedRowCellValue("AlamatUpload"), txtKDCUSTOMER.Text, txtNAMAPASIEN.Text, deDATETANGGALLAHIR.DateTime.ToString("dd-MM-yyyy"), txtOBJEKTIF_JENISKELAMIN.Text, grvUpload.GetFocusedRowCellValue("Kodeupload"))
    '            'frmBrowseUpload.Show()
    '            ''frmBrowseUpload.BringToFront()
    '            'frmBrowseUpload.WindowState = FormWindowState.Normal

    '            Dim frmBrowseUpload As New frmBrowseUpload
    '            Try
    '                frmBrowseUpload.WindowState = FormWindowState.Normal
    '                frmBrowseUpload.fn_LoadMe(2, grvUpload.GetFocusedRowCellValue("Type"), grvUpload.GetFocusedRowCellValue("AlamatUpload"), txtKDCUSTOMER.Text, txtNAMAPASIEN.Text, deDATETANGGALLAHIR.DateTime.ToString("dd-MM-yyyy"), txtOBJEKTIF_JENISKELAMIN.Text, grvUpload.GetFocusedRowCellValue("Kodeupload"))
    '                frmBrowseUpload.ShowDialog(Me)
    '            Catch ex As Exception
    '                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
    '            Finally
    '                If Not frmBrowseUpload Is Nothing Then frmBrowseUploadRadiologi.Dispose()
    '                frmBrowseUpload = Nothing
    '            End Try
    '        ElseIf grvUpload.GetFocusedRowCellValue("Type") = "USG" Then
    '            'frmBrowseUpload.fn_LoadMe(1, grvUpload.GetFocusedRowCellValue("Type"), grvUpload.GetFocusedRowCellValue("AlamatUpload"), txtKDCUSTOMER.Text, txtNAMAPASIEN.Text, deDATETANGGALLAHIR.DateTime.ToString("dd-MM-yyyy"), txtOBJEKTIF_JENISKELAMIN.Text, grvUpload.GetFocusedRowCellValue("Kodeupload"))
    '            'frmBrowseUpload.Show()
    '            ''frmBrowseUpload.BringToFront()
    '            'frmBrowseUpload.WindowState = FormWindowState.Normal

    '            Dim frmBrowseUpload As New frmBrowseUpload
    '            Try
    '                frmBrowseUpload.WindowState = FormWindowState.Normal
    '                frmBrowseUpload.fn_LoadMe(1, grvUpload.GetFocusedRowCellValue("Type"), grvUpload.GetFocusedRowCellValue("AlamatUpload"), txtKDCUSTOMER.Text, txtNAMAPASIEN.Text, deDATETANGGALLAHIR.DateTime.ToString("dd-MM-yyyy"), txtOBJEKTIF_JENISKELAMIN.Text, grvUpload.GetFocusedRowCellValue("Kodeupload"))
    '                frmBrowseUpload.ShowDialog(Me)
    '            Catch ex As Exception
    '                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
    '            Finally
    '                If Not frmBrowseUpload Is Nothing Then frmBrowseUploadRadiologi.Dispose()
    '                frmBrowseUpload = Nothing
    '            End Try
    '        ElseIf grvUpload.GetFocusedRowCellValue("Type") = "APIRADIOLOGI" Then
    '            'frmBrowseUploadRadiologi.fn_LoadMe(txtKDCUSTOMER.Text, txtNAMAPASIEN.Text, deDATETANGGALLAHIR.DateTime.ToString("dd-MM-yyyy"), txtOBJEKTIF_JENISKELAMIN.Text, grvUpload.GetFocusedRowCellValue("AlamatUpload"))
    '            'frmBrowseUploadRadiologi.Show()
    '            'frmBrowseUploadRadiologi.WindowState = FormWindowState.Normal

    '            Dim frmBrowseUploadRadiologi As New frmBrowseUploadRadiologi
    '            Try
    '                frmBrowseUploadRadiologi.WindowState = FormWindowState.Normal
    '                frmBrowseUploadRadiologi.fn_LoadMe(txtKDCUSTOMER.Text, txtNAMAPASIEN.Text, deDATETANGGALLAHIR.DateTime.ToString("dd-MM-yyyy"), txtOBJEKTIF_JENISKELAMIN.Text, grvUpload.GetFocusedRowCellValue("AlamatUpload"), txtKDKUNJUNGAN.Text)
    '                frmBrowseUploadRadiologi.ShowDialog(Me)
    '            Catch ex As Exception
    '                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
    '            Finally
    '                If Not frmBrowseUploadRadiologi Is Nothing Then frmBrowseUploadRadiologi.Dispose()
    '                frmBrowseUploadRadiologi = Nothing

    '            End Try
    '        Else
    '            Dim frmBrowseUpload As New frmBrowseUpload
    '            Try
    '                frmBrowseUpload.WindowState = FormWindowState.Normal
    '                frmBrowseUpload.fn_LoadMe(0, grvUpload.GetFocusedRowCellValue("Type"), grvUpload.GetFocusedRowCellValue("AlamatUpload"), txtKDCUSTOMER.Text, txtNAMAPASIEN.Text, deDATETANGGALLAHIR.DateTime.ToString("dd-MM-yyyy"), txtOBJEKTIF_JENISKELAMIN.Text, grvUpload.GetFocusedRowCellValue("Kodeupload"))
    '                frmBrowseUpload.ShowDialog(Me)
    '            Catch ex As Exception
    '                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
    '            Finally
    '                If Not frmBrowseUpload Is Nothing Then frmBrowseUploadRadiologi.Dispose()
    '                frmBrowseUpload = Nothing
    '            End Try

    '            'frmBrowseUpload.fn_LoadMe(0, grvUpload.GetFocusedRowCellValue("Type"), grvUpload.GetFocusedRowCellValue("AlamatUpload"), txtKDCUSTOMER.Text, txtNAMAPASIEN.Text, deDATETANGGALLAHIR.DateTime.ToString("dd-MM-yyyy"), txtOBJEKTIF_JENISKELAMIN.Text, grvUpload.GetFocusedRowCellValue("Kodeupload"))
    '            'frmBrowseUpload.Show()
    '            ''frmBrowseUpload.BringToFront()
    '            'frmBrowseUpload.WindowState = FormWindowState.Normal
    '        End If
    '    End Sub
    Private Sub grvdetailResep_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grvDetailResep.RowStyle
        If grvDetailResep.IsFilterRow(e.RowHandle) Then Exit Sub
        If grvDetailResep.GetRowCellValue(e.RowHandle, "REMARKS_FARMASI") = "Ya" Then
            e.Appearance.BackColor = Color.Yellow
        End If
    End Sub
    Private Sub grvListPasien_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grvListPasien.RowStyle
        If grvListPasien.IsFilterRow(e.RowHandle) Then Exit Sub

        If grvListPasien.GetRowCellValue(e.RowHandle, "Cek2") = "DAFTAR_L6_0000000004" Then
            e.Appearance.BackColor = Color.Pink
        Else
            If btnType.Text = "Rawat Jalan" Then
                'Rata
                If grvListPasien.GetRowCellValue(e.RowHandle, "Cek") = True Then
                    If grvListPasien.GetRowCellValue(e.RowHandle, "Keterangan").ToString.Contains("PENUNJANG HARI INI") Then
                        e.Appearance.BackColor = Color.Yellow
                    Else
                        e.Appearance.BackColor = Color.LawnGreen
                    End If
                Else
                    If grvListPasien.GetRowCellValue(e.RowHandle, "ISPERAWAT") = True Then
                        e.Appearance.BackColor = Color.Orange
                    End If
                End If
            Else
                If grvListPasien.GetRowCellValue(e.RowHandle, "Keterangan") <> "TERISI" Then

                Else
                    If grvListPasien.GetRowCellValue(e.RowHandle, "CekSudahAdaCPPT") = "DOKTER" & Now.ToString("yyyyMMdd") Then
                        e.Appearance.BackColor = Color.LawnGreen
                    Else
                        If grvListPasien.GetRowCellValue(e.RowHandle, "CekSudahAdaCPPT") = "PERAWAT" & Now.ToString("yyyyMMdd") Then
                            e.Appearance.BackColor = Color.Orange
                        End If
                    End If
                End If

            End If
        End If
    End Sub
    Private Sub grvListantrian_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grvListantrian.RowStyle
        If grvListantrian.IsFilterRow(e.RowHandle) Then Exit Sub
        If btnType.Text = "Rawat Jalan" Then
            If grvListantrian.GetRowCellValue(e.RowHandle, "Cek") = True Then
                If grvListantrian.GetRowCellValue(e.RowHandle, "Keterangan").ToString.Contains("PENUNJANG HARI INI") Then
                    e.Appearance.BackColor = Color.Yellow
                Else
                    e.Appearance.BackColor = Color.LawnGreen
                End If
            Else
                If grvListantrian.GetRowCellValue(e.RowHandle, "ISPERAWAT") = True Then
                    e.Appearance.BackColor = Color.Orange
                End If
            End If
        End If
    End Sub
    '    Private Sub grvLaporanOperasi_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvLaporanOperasi.FocusedRowChanged
    '        If grvLaporanOperasi.GetFocusedRowCellValue("KODE") Is Nothing Then
    '            Exit Sub
    '        End If

    '        Try
    '            PdfViewerLaporanOperasi.CloseDocument()

    '            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

    '            SplashScreenManager.Default.SetWaitFormCaption("Processing data Laporan Operasi.....")


    '            Dim FolderSimpan = "C:/LAPORANOPERASI_LISTERM/"

    '            If Not Directory.Exists(FolderSimpan) Then
    '                Directory.CreateDirectory(FolderSimpan)
    '            Else
    '                DeleteDirectory(FolderSimpan)
    '                Directory.CreateDirectory(FolderSimpan)
    '            End If

    '            Dim oLaporanOperasi As New EMedrek.clsS_DIGITAL_OK_LAPORANOPERASI

    '            If grvLaporanOperasi.GetFocusedRowCellValue("KETERANGAN") = "LAPORAN OPERASI" Then
    '                Dim ds = oLaporanOperasi.GetDataKode(grvLaporanOperasi.GetFocusedRowCellValue("KODE"))
    '                If ds IsNot Nothing Then
    '                    NAMA = txtNAMAPASIEN.Text
    '                    JENISKELAMIN = txtOBJEKTIF_JENISKELAMIN.Text
    '                    TANGGALLAHIR = deDATETANGGALLAHIR.DateTime.ToString("dd-MM-yyyy")
    '                    sKDUSER_TTD = ds.KDUSER
    '                    USIA = frmRawatInapList.GetUmurPasien(ds.DATE, deDATETANGGALLAHIR.DateTime)

    '                    Dim rpt As New xtraReportLAPORANOPERASI

    '                    rpt.ShowPrintMarginsWarning = False
    '                    rpt.Watermark.Text = sWATERMARK

    '                    rpt.BindingSource1.DataSource = ds

    '                    Dim waktu As String = Now.ToString("ddMMyyyyHHmm")
    '                    rpt.ExportToPdf(FolderSimpan & waktu & ds.KDLAPORANOPERASI & ".pdf")
    '                    PdfViewerLaporanOperasi.LoadDocument(FolderSimpan & waktu & ds.KDLAPORANOPERASI & ".pdf")
    '                End If
    '            Else
    '                Dim ds = oLaporanOperasi.GetDataKodeTindakan(grvLaporanOperasi.GetFocusedRowCellValue("KODE"))
    '                If ds IsNot Nothing Then
    '                    NAMA = txtNAMAPASIEN.Text
    '                    JENISKELAMIN = txtOBJEKTIF_JENISKELAMIN.Text
    '                    TANGGALLAHIR = deDATETANGGALLAHIR.DateTime.ToString("dd-MM-yyyy")
    '                    sKDUSER_TTD = ds.KDUSER
    '                    USIA = frmRawatInapList.GetUmurPasien(ds.DATE, deDATETANGGALLAHIR.DateTime)

    '                    Dim rpt As New xtraREPORTLAPORANTINDAKAN

    '                    rpt.ShowPrintMarginsWarning = False
    '                    rpt.Watermark.Text = sWATERMARK

    '                    rpt.BindingSource1.DataSource = ds

    '                    Dim waktu As String = Now.ToString("ddMMyyyyHHmm")
    '                    rpt.ExportToPdf(FolderSimpan & waktu & ds.KDLAPORANTINDAKAN & ".pdf")
    '                    PdfViewerLaporanOperasi.LoadDocument(FolderSimpan & waktu & ds.KDLAPORANTINDAKAN & ".pdf")
    '                End If
    '            End If

    '            SplashScreenManager.CloseForm(False)
    '        Catch oErr As Exception
    '            SplashScreenManager.CloseForm(False)
    '            MsgBox("Load data Laporan Operasi: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '        End Try
    '    End Sub
    Public Sub DeleteDirectory(path As String)
        If Directory.Exists(path) Then
            If Directory.Exists(path) Then
                'Delete all files from the Directory
                For Each filepath As String In Directory.GetFiles(path)
                    File.Delete(filepath)
                Next
                'Delete all child Directories
                For Each dir As String In Directory.GetDirectories(path)
                    DeleteDirectory(dir)
                Next
                'Delete a Directory
                Directory.Delete(path)
            End If

        End If
    End Sub
    Private Sub btnRefreshListPasien_Click(sender As Object, e As EventArgs) Handles btnRefreshListPasien.Click
        fn_LoadSecurity()
    End Sub
    '    Private Function MergeFilesByte(ByVal sourceFiles As List(Of Byte())) As Byte()
    '        Try
    '            Dim mergedPdf As Byte() = Nothing
    '            Using ms As New MemoryStream()
    '                Using document As New Document()
    '                    Using copy As New PdfCopy(document, ms)
    '                        document.Open()
    '                        For i As Integer = 0 To sourceFiles.Count - 1
    '                            Dim reader As New PdfReader(sourceFiles(i))
    '                            ' loop over the pages in that document
    '                            Dim n As Integer = reader.NumberOfPages
    '                            Dim page As Integer = 0
    '                            While page < n
    '                                page = page + 1
    '                                copy.AddPage(copy.GetImportedPage(reader, page))
    '                            End While
    '                        Next
    '                    End Using
    '                End Using

    '                mergedPdf = ms.ToArray()

    '                Return mergedPdf

    '            End Using
    '        Catch ex As Exception
    '            MergeFilesByte = Nothing
    '            MsgBox("Load Merge Data : " & vbCrLf & ex.Message, MsgBoxStyle.Information, Me.Text)
    '        End Try
    '    End Function
    '    'Public Function MergeFilesByte(ByVal sourceFiles As List(Of Byte())) As Byte()
    '    '    Try
    '    '        Dim mergedPdf As Byte() = Nothing
    '    '        Using ms As New MemoryStream()
    '    '            Using document As New Document()
    '    '                Using copy As New PdfCopy(document, ms)
    '    '                    document.Open()
    '    '                    For i As Integer = 0 To sourceFiles.Count - 1
    '    '                        Dim reader As New PdfReader(sourceFiles(i))
    '    '                        ' loop over the pages in that document
    '    '                        Dim n As Integer = reader.NumberOfPages
    '    '                        Dim page As Integer = 0
    '    '                        While page < n
    '    '                            page = page + 1
    '    '                            copy.AddPage(copy.GetImportedPage(reader, page))
    '    '                        End While
    '    '                    Next
    '    '                End Using
    '    '            End Using

    '    '            mergedPdf = ms.ToArray()

    '    '            Return mergedPdf

    '    '        End Using
    '    '    Catch ex As Exception
    '    '        MergeFilesByte = Nothing
    '    '        MsgBox("Load Merge Data : " & vbCrLf & ex.Message, MsgBoxStyle.Information, Me.Text)
    '    '    End Try
    '    'End Function
#End Region
#Region "Form"
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

    End Sub
    Private Sub grdKDDEPARTMENT_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDDEPARTMENT.EditValueChanged
        fn_LoadDokter(grdKDDEPARTMENT.EditValue)

        If isLoad = True Then
            fn_LoadView()
        End If
    End Sub
    Private Sub grdKDDEPARTMENT_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdKDDEPARTMENT.KeyPress
        If Asc(e.KeyChar) = 13 Then
            grdKDDEPARTMENT.ResetText()
        End If
    End Sub
    Private Sub grdDPJP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdDPJP.KeyPress
        If Asc(e.KeyChar) = 13 Then
            grdDPJP.ResetText()
        End If
    End Sub
    Private Sub chkALERGI_TIDAK_CheckedChanged(sender As Object, e As EventArgs) Handles chkALERGI_TIDAK.CheckedChanged
        If isLoad = True Then
            If chkALERGI_TIDAK.Checked = True Then
                txtALERGIOBAT.ResetText()
                chkALERGI_YA.Checked = False
            End If
        End If
    End Sub
    Private Sub chkALERGI_YA_CheckedChanged(sender As Object, e As EventArgs) Handles chkALERGI_YA.CheckedChanged
        If isLoad = True Then
            If chkALERGI_YA.Checked = True Then
                chkALERGI_TIDAK.Checked = False
            End If
        End If
    End Sub
    Private Function Load_SimpanCPPTSelesai(ByVal KDREG As String) As String
        Load_SimpanCPPTSelesai = ""

        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "I_TRACKING_CPPT A "
            SQL &= "WHERE "
            SQL &= "A.TANGGAL = '" & Now.ToString("yyyyMMdd") & "' "
            SQL &= "AND A.KDREG = '" & KDREG & "' "

            If sisDOkter = False Then
                SQL &= "AND A.DESCRIPTION = 'PERAWAT' "
            Else
                SQL &= "AND A.KDDOCTOR = '" & sKDDPJPUSER & "' "
            End If

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "I_TRACKING_CPPT")

            'For iLoop As Integer = 0 To ds.Tables("I_TRACKING_CPPT").Rows.Count - 1
            '    With ds.Tables("I_TRACKING_CPPT")
            '        Load_SimpanCPPTSelesai = "Y"
            '    End With
            'Next

            For xloop As Integer = 0 To ds.Tables("I_TRACKING_CPPT").Rows.Count - 1
                Load_SimpanCPPTSelesai = ds.Tables("I_TRACKING_CPPT").Rows(xloop)("KDCPPT")
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox("I_TRACKING_CPPT : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
#End Region
#Region "Form Medrek Rawat Jalan"
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            Try
                If txtOBJEKTIF_BERATBADAN.Text <> "" Then
                    Dim TES = CDec(txtOBJEKTIF_BERATBADAN.Text)
                End If
            Catch ex As Exception
                MsgBox("Berat Badan Harus Angka" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End Try

            Try
                If txtOBJEKTIF_TINGGIBADAN.Text <> "" Then
                    Dim TES = CDec(txtOBJEKTIF_TINGGIBADAN.Text)
                End If
            Catch ex As Exception
                MsgBox("Tinggi Badan Harus Angka" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End Try

            If txtKDKUNJUNGAN.Text = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                'lblRegister.Focus()
                fn_Validate = False
                Exit Function
            End If

            Dim Diagnosa As Boolean = False

            For i As Integer = 0 To grvDiagnosaPenyerta.RowCount - 2
                Diagnosa = True
            Next
            If Diagnosa = False Then
                MsgBox("Dibutuhkan Diagnosa Primer", MsgBoxStyle.Exclamation, Me.Text)
                'txtDIAGNOOSA.Focus()
                fn_Validate = False
                Exit Function
            End If
            'If txtDIAGNOOSA.Text = "- - -" Then
            '    MsgBox("Dibutuhkan Diagnosa", MsgBoxStyle.Exclamation, Me.Text)
            '    txtDIAGNOOSA.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If
            'If txtDIAGNOOSA.Text = String.Empty Then
            '    MsgBox("Dibutuhkan Diagnosa", MsgBoxStyle.Exclamation, Me.Text)
            '    txtDIAGNOOSA.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If
            If btnType.Text = "Rawat Jalan" Then
                If txtTINDAKLANJUT.Text = String.Empty Then
                    MsgBox("Dibutuhkan Tindak Lanjut", MsgBoxStyle.Exclamation, Me.Text)
                    txtTINDAKLANJUT.Focus()
                    fn_Validate = False
                    Exit Function
                End If
            End If
            If chkALERGI_YA.Checked = True Then
                If txtALERGIOBAT.Text = String.Empty Then
                    MsgBox("Dibutuhkan Keterangan Alergi", MsgBoxStyle.Exclamation, Me.Text)
                    txtALERGIOBAT.Focus()
                    fn_Validate = False
                    Exit Function
                End If
            End If
        Catch oErr As Exception
            MsgBox("Validate Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub btnBATAL_Click(sender As Object, e As EventArgs) Handles btnBATAL.Click
        isLoad = False

        sNoidSimpan = ""
        lGROUPLISTPASIEN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lGROUPFORM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        'lIDENTITASPASIEN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lLISTANTRIAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        'lLISTANTRIANCHEK.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        fn_LoadSecurity()
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If fn_Validate() = False Then
            Exit Sub
        End If

        Dim sDiagnosa As Boolean = False
        Dim sTindakan As Boolean = False
        Dim sResep As Boolean = False
        Dim sLab As Boolean = False
        Dim sRad As Boolean = False

        For i As Integer = 0 To grvDiagnosaPenyerta.RowCount - 2
            sDiagnosa = True
        Next

        For i As Integer = 0 To grvDetailResep.RowCount - 2
            sResep = True
        Next

        For i As Integer = 0 To grvOBATRACIKAN.RowCount - 2
            sResep = True
        Next

        Dim oItem As New Reference.clsItem

        For i As Integer = 0 To grvCPPT_Tindakan.RowCount - 2
            Dim dsItem = oItem.GetData(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_KDITEMTINDAKAN))
            If dsItem IsNot Nothing Then
                If dsItem.M_ITEM_L3.MEMO = "LABORATORIUM" Then
                    sLab = True
                ElseIf dsItem.M_ITEM_L3.MEMO = "BANK DARAH" Then
                    sLab = True
                ElseIf dsItem.M_ITEM_L3.MEMO = "RADIOLOGI" Then
                    sRad = True
                Else
                    sTindakan = True
                End If
            End If
        Next

        Dim frmPilihanMedrek As New frmPilihanMedrek
        frmPilihanMedrek.LoadMe(sDiagnosa, sResep, sTindakan, IIf(txtTINDAKLANJUT.Text = "", False, True), sLab, sRad)
        frmPilihanMedrek.ShowDialog(Me)

        If sSIMPAN = False Then
            Exit Sub
        End If

        If fn_Save(sNoidSimpan) = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        Else
            If txtKODEBOOKING.Text <> "" Then
                frmErmList.fn_TaskID(txtKODEBOOKING.Text, 5)
            End If

            MsgBox("Save " & txtKDKUNJUNGAN.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            'Dim oNameModul As New Setting.clsCounter
            'oNameModul.UpdateDataCekSuara()
        End If

        isLoad = False
        sNoidSimpan = ""
        lGROUPLISTPASIEN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lGROUPFORM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        'lIDENTITASPASIEN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lLISTANTRIAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        fn_EmptyMe()

        fn_LoadSecurity()

    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadTEMPLATE()
        fn_LoadITEM()
        fn_LoadUOM()
        fn_LoadSIGNA()
        fn_LoadCARAPAKAI()
        fn_LoadCPPT_TemplateTindakan()
        fn_LoadKDITEMSearch()
        fn_LoadKDITEMTindakanTerjadwalSearch()
    End Sub
    Private Sub fn_LoadCPPT_TemplateTindakan()
        Dim oGrouperDataCppt_Template As New Template.clsCPPT_TemplateProsedur
        Try
            grdCPPT_TemplateTindakan.Properties.DataSource = oGrouperDataCppt_Template.GetData.Where(Function(x) x.KDUSER = sUserID And x.ISDELETE = True).ToList()
            grdCPPT_TemplateTindakan.Properties.ValueMember = "KDTEMPLATE"
            grdCPPT_TemplateTindakan.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox("Load Template Tindakan" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadTEMPLATE()
        Dim oTemplateNonRacikan As New EMedrek.clsTemplateNonRacikan
        Try
            'grdTEMPLATE.Properties.DataSource = oTemplate.GetDataDetailList().ToList()
            'grdTEMPLATE.Properties.ValueMember = "KDCPPTTEMPLATE"
            'grdTEMPLATE.Properties.DisplayMember = "REMARKS"

            Dim ds = From x In oTemplateNonRacikan.GetDataDetailList()
                     Join y In oTemplateNonRacikan.GetDataDetail()
                     On x.KDCPPTTEMPLATE Equals y.KDCPPTTEMPLATE
                     Where y.KDUSER = sUserID
                     Group y By x.KDCPPTTEMPLATE, x.REMARKS, y.KDUSER Into Total = Sum(y.TOTAL)
                     Select KDCPPTTEMPLATE, REMARKS

            grdTEMPLATE.Properties.DataSource = ds.ToList()
            grdTEMPLATE.Properties.ValueMember = "KDCPPTTEMPLATE"
            grdTEMPLATE.Properties.DisplayMember = "REMARKS"


            Dim dsRacikan = From x In oTemplateNonRacikan.GetDataDetailList()
                            Join y In oTemplateNonRacikan.GetDataDetailRacikan()
                            On x.KDCPPTTEMPLATE Equals y.KDCPPTTEMPLATE
                            Where y.KDUSER = sUserID
                            Group y By x.KDCPPTTEMPLATE, x.REMARKS, y.KDUSER Into Total = Sum(y.TOTAL)
                            Select KDCPPTTEMPLATE, REMARKS

            grdTEMPLATERACIKAN.Properties.DataSource = dsRacikan.ToList()
            grdTEMPLATERACIKAN.Properties.ValueMember = "KDCPPTTEMPLATE"
            grdTEMPLATERACIKAN.Properties.DisplayMember = "REMARKS"

        Catch oErr As Exception
            MsgBox("Load Template Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
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

            If sHargaApotik = False Then
                SQL &= "AND B.MEMO IN ('POLIKINIK', 'NON KATEGORI', 'RAWAT JALAN')  "
            Else
                SQL &= "AND B.MEMO NOT IN ('LABORATORIUM', 'RADIOLOGI', 'BANK DARAH')  "
            End If

            'If sNoidSimpan = "" Then
            '    'SQL &= "AND B.MEMO IN ('LABORATORIUM', 'BANK DARAH', 'RADIOLOGI', 'PENUNJANG', 'POLIKINIK', 'NON KATEGORI')  "
            '    If sHargaApotik = False Then
            '        SQL &= "AND B.MEMO IN ('POLIKINIK', 'NON KATEGORI')  "
            '    Else
            '        SQL &= "AND B.MEMO NOT IN ('LABORATORIUM', 'RADIOLOGI', 'BANK DARAH')  "
            '    End If
            'End If

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ITEM_ALL")

            'grdCPPT_KDITEMTINDAKAN.DataSource = ds.Tables("ITEM_ALL")
            'grdCPPT_KDITEMTINDAKAN.ValueMember = "KDITEM"
            'grdCPPT_KDITEMTINDAKAN.DisplayMember = "NMITEM2"

            grdTindakanHariIni.DataSource = ds.Tables("ITEM_ALL")
            grdTindakanHariIni.ValueMember = "KDITEM"
            grdTindakanHariIni.DisplayMember = "NMITEM2"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox("Load Tindakan" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormulirNakes()
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
            SQL &= "A.* "
            SQL &= "FROM "
            SQL &= "M_FORMULIRNAKES A "
            SQL &= "WHERE A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "M_FORMULIRNAKES")

            grdKDFORMULIRNAKES.Properties.DataSource = ds.Tables("M_FORMULIRNAKES")
            grdKDFORMULIRNAKES.Properties.ValueMember = "KDFORMULIR"
            grdKDFORMULIRNAKES.Properties.DisplayMember = "MEMO"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox("Load Formulir Nakes" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormulirMedisLoad()
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
            SQL &= "A.* "
            SQL &= "FROM "
            SQL &= "M_FORMULIRMEDIS A "
            SQL &= "WHERE A.ISACTIVE = 1 "
            SQL &= "AND MEMO = 'ORDER RESEP' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "M_FORMULIRMEDISCEK")

            'grdKDFORMULIRMEDIS.Properties.DataSource = ds.Tables("M_FORMULIRMEDIS")
            'grdKDFORMULIRMEDIS.Properties.ValueMember = "KDFORMULIR"
            'grdKDFORMULIRMEDIS.Properties.DisplayMember = "MEMO"
            For iLoop As Integer = 0 To ds.Tables("M_FORMULIRMEDISCEK").Rows.Count - 1
                sAutoOrderObat = False
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Load Formulir Medis" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormulirMedis()
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
            SQL &= "A.* "
            SQL &= "FROM "
            SQL &= "M_FORMULIRMEDIS A "
            SQL &= "WHERE A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "M_FORMULIRMEDIS")

            grdKDFORMULIRMEDIS.Properties.DataSource = ds.Tables("M_FORMULIRMEDIS")
            grdKDFORMULIRMEDIS.Properties.ValueMember = "KDFORMULIR"
            grdKDFORMULIRMEDIS.Properties.DisplayMember = "MEMO"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Load Formulir Medis" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDITEMTindakanTerjadwalSearch()
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
            SQL &= ",KELOMPOK = B.MEMO "
            SQL &= "FROM "
            SQL &= "M_ITEM A "
            SQL &= "INNER JOIN M_ITEM_L2 B "
            SQL &= "ON A.KDITEM_L2 = B.KDITEM_L2 "
            SQL &= "WHERE A.ISSTOK = 0 "
            SQL &= "AND A.ISACTIVE = 1 "
            SQL &= "AND B.MEMO IN ('LABORATORIUM', 'BANK DARAH', 'RADIOLOGI', 'PENUNJANG')  "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ITEM_ALLTERJADWAL")

            grdKDITEMTindakanTerjadwal.DataSource = ds.Tables("ITEM_ALLTERJADWAL")
            grdKDITEMTindakanTerjadwal.ValueMember = "KDITEM"
            grdKDITEMTindakanTerjadwal.DisplayMember = "NMITEM2"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox("Load Tindakan" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
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

            grdKDITEM.DataSource = ds.Tables("ITEM")
            grdKDITEM.ValueMember = "KDITEM"
            grdKDITEM.DisplayMember = "NMITEM2"

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
    Private Sub fn_LoadUOM()
        Dim oUOM As New Reference.clsUOM
        Try
            Dim dsList = oUOM.GetData()

            grdUOM.DataSource = dsList.ToList()
            grdUOM.ValueMember = "KDUOM"
            grdUOM.DisplayMember = "MEMO"

            grdKDUOMRACIKAN.DataSource = dsList.ToList()
            grdKDUOMRACIKAN.ValueMember = "KDUOM"
            grdKDUOMRACIKAN.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox("Load Uom" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadSIGNA()
        Dim oSigna As New Reference.clsSigna
        Try
            grdKDSIGNA.DataSource = oSigna.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDSIGNA.ValueMember = "KDSIGNA"
            grdKDSIGNA.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox("Load Signa" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadCARAPAKAI()
        Dim oCaraPakai As New Reference.clsCaraPakai
        Try
            grdCARAPAKAI.DataSource = oCaraPakai.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdCARAPAKAI.ValueMember = "KDCARAPAKAI"
            grdCARAPAKAI.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox("Load Cara Pakai" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData(ByVal sNoid As String)
        Try
            'Dim oReq_Recipe As New Transaksi.clsReq_Recipe
            'Dim oKoding As New Admission.clsKoding
            'Dim oReqAkhirPemeriksaan As New Transaksi.clsReqAkhirPemeriksaan

            ' ***** HEADER *****
            Dim ds = oGrouperDataCppt.GetData(sNoid)

            With ds
                'Dim dsDiagnosaPrimer = oGrouperDataCppt.GetDataDetailDiagnosa(sNoid).Where(Function(x) x.SEQ = 0)

                'If dsDiagnosaPrimer IsNot Nothing Then
                '    txtDIAGNOOSA.Text = dsDiagnosaPrimer.FirstOrDefault().MEMO
                'Else
                '    txtDIAGNOOSA.Text = ""
                'End If

                deDATECPPT.DateTime = .DATE

                txtTINDAKLANJUT.Text = .PLANNING_ALASAN
                'txtGRANDTOTAL.Text = .gr

                chkALERGI_YA.Checked = .SUBJEKTIF_ALERGI_YA
                chkALERGI_TIDAK.Checked = .SUBJEKTIF_ALERGI_TIDAK
                txtALERGIOBAT.Text = .SUBJEKTIF_ALERGI_YA_TEXT
                'txtNAMAPASIEN.Text = .NAMAPASIEN
                'deDATETANGGALLAHIR.DateTime = .TANGGALLAHIR
                'txtKDCUSTOMER.Text = .KDCUSTOMER
                txtSUBJEKTIF.Text = .SUBJEKTIF_KELUHANUTAMA
                'txtOBJEKTIF_JENISKELAMIN.Text = .OBJEKTIF_JENISKELAMIN
                'txtOBJEKTIF_UMUR.Text = .OBJEKTIF_UMUR
                Try
                    txtOBJEKTIF_BERATBADAN.Text = IIf(CDec(.OBJEKTIF_BERATBADAN) = 0, "", .OBJEKTIF_BERATBADAN)
                Catch ex As Exception
                    txtOBJEKTIF_BERATBADAN.Text = ""
                End Try
                Try
                    txtOBJEKTIF_TINGGIBADAN.Text = IIf(CDec(.OBJEKTIF_TINGGIBADAN) = 0, "", .OBJEKTIF_TINGGIBADAN)
                Catch ex As Exception
                    txtOBJEKTIF_TINGGIBADAN.Text = ""
                End Try
                'txtOBJEKTIF_TEKANANDARAH.Text = .OBJEKTIF_TEKANANDARAH
                txtCPPT_Sistole.Text = .OBJEKTIF_SISTOLE
                txtCPPT_Diastole.Text = .OBJEKTIF_DIASTOLE
                txtOBJEKTIF_NADI.Text = .OBJEKTIF_HR
                txtOBJEKTIF_RESPIRASI.Text = .OBJEKTIF_RR
                txtOBJEKTIF_SATURASIOKSIGEN.Text = .OBJEKTIF_SPO2
                txtOBJEKTIF_SUHU.Text = .OBJEKTIF_SUHU
                txtDESKRIPSI.Text = .OBJEKTIF_PEMERIKSAAN
                txtALAMATGAMBAR.Text = .OBJEKTIF_ALAMATGAMBARPEMERIKSAAN
                cboTINGKATKESADARAN.Text = .OBJEKTIF_KESADARAN
                txtOBJEKTIF_IMT.Text = .OBJEKTIF_GCS
                txtOBJEKTIF_BBIDEAL.Text = .OBJEKTIF_TAMPAKSAKIT
                txtOBJEKTIF_BBDITURUNKAN.Text = .OBJEKTIF_VISUALANALOGSCORE

                txtGOALOFTREATMENT.Text = .GOALOFTREATMENT
                txtEDUKASI.Text = .EDUKASI
                txtFREKUENSIKUNJUNGAN.Text = .FREKUENSIKUNJUNGAN

                If txtALAMATGAMBAR.Text <> "" Then
                    If Directory.Exists(sAlamatSimpanFolder) Then
                        Dim img As System.Drawing.Image = System.Drawing.Image.FromFile(txtALAMATGAMBAR.Text)
                        picGAMBAR2.Image = img
                        sPicture = picGAMBAR2.Image
                    Else
                        MsgBox("Folder Simpan Gambar ke File " & sAlamatSimpanFolder & " Tidak dapat diakses")
                    End If
                Else
                    'picGAMBAR2.Image = CType(My.Resources.ResourceManager.GetObject("image1"), Image)
                    picGAMBAR2.Image = My.Resources.ResourceManager.GetObject("image1")
                    sPicture = Nothing
                End If

                BindingSource.DataSource = oGrouperDataCppt.GetDataDetailNonRacikan(sNoid).Where(Function(x) x.KDITEM <> "").OrderBy(Function(x) x.SEQ).ToList()
                grdDetailResep.DataSource = BindingSource

                BindingSource1.DataSource = oGrouperDataCppt.GetDataDetailDiagnosa(sNoid).OrderBy(Function(x) x.SEQ).ToList()
                grdDiagnosaPenyerta.DataSource = BindingSource1

                'BindingSource2.DataSource = oGrouperDataCppt.GetDataDetailTindakan(sNoid).Where(Function(x) x.TINDAKAN <> "").OrderBy(Function(x) x.SEQ).ToList()
                'grdTindakan.DataSource = BindingSource2

                BindingSource3.DataSource = oGrouperDataCppt.GetDataDetailTindakan(sNoid).OrderBy(Function(x) x.SEQ).ToList()
                grdCPPT_Tindakan.DataSource = BindingSource3

                BindingSource4.DataSource = oGrouperDataCppt.GetDataDetailRacikan(sNoid).OrderBy(Function(x) x.SEQ).ToList()
                grdOBATRACIKAN.DataSource = BindingSource4

                BindingSourceOrderTerjadwal.DataSource = oGrouperDataCppt.GetDataDetailTerjadwal(sNoid).OrderBy(Function(x) x.DATECREATED).ToList()
                grdTindakanTerjadwal.DataSource = BindingSourceOrderTerjadwal

                Dim oItem As New Reference.clsItem

                'For i As Integer = 0 To grvCPPT_Tindakan.RowCount - 2
                '    Dim dsItem = oItem.GetData(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_KDITEMTINDAKAN))
                '    If dsItem IsNot Nothing Then
                '        If dsItem.M_ITEM_L3.MEMO = "LABORATORIUM" Then
                '            If grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_ISPERAWAT) = False Then
                '                ListLoadDataLaboratorium.Add(dsItem.NMITEM2 & " " & IIf(String.IsNullOrEmpty(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)), "", grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)))
                '            ElseIf dsItem.M_ITEM_L3.MEMO = "BANK DARAH" Then
                '                ListLoadDataLaboratorium.Add(dsItem.NMITEM2 & " " & IIf(String.IsNullOrEmpty(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)), "", grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)))
                '            End If
                '        End If
                '    End If
                'Next

                'Dim oItem As New Reference.clsItem

                listTindakanLab_LoadData.Clear()
                listTindakanRad_LoadData.Clear()

                For Each xloop In oGrouperDataCppt.GetDataDetailTindakan(ds.KDCPPT)
                    If xloop.ISPERAWAT = False Then
                        Dim dsItem = oItem.GetData(xloop.KDITEM)

                        If dsItem IsNot Nothing Then
                            If dsItem.M_ITEM_L3.MEMO = "LABORATORIUM" Then
                                listTindakanLab_LoadData.Add(dsItem.NMITEM2)
                            ElseIf dsItem.M_ITEM_L3.MEMO = "BANK DARAH" Then
                                listTindakanLab_LoadData.Add(dsItem.NMITEM2)
                            ElseIf dsItem.M_ITEM_L3.MEMO = "RADIOLOGI"
                                listTindakanRad_LoadData.Add(dsItem.NMITEM2)
                            End If
                        End If
                    End If
                Next

                Calculate()
            End With

        Catch oErr As Exception
            MsgBox("Load Data Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Save(ByVal sNoid As String) As Boolean
        Try
            Dim sCategory As Integer = 0
            Dim dsIdentitas = oGrouperDataCppt.GetDataByKdKunjungan(txtKDKUNJUNGAN.Text)
            Dim sKDIDENTITAS As Integer = 0
            If dsIdentitas IsNot Nothing Then
                sKDIDENTITAS = dsIdentitas.KDIDENTITAS
                sCategory = dsIdentitas.CATEGORY
            Else
                MsgBox("Identitas Kosong", MsgBoxStyle.Exclamation, Me.Text)
                Exit Function
            End If

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
                .DATE = deDATECPPT.DateTime
                .KDCPPT = sNoid
                .KDIDENTITAS = sKDIDENTITAS
                .KDPROFESI = "PROFESI_0000000001"

                .SUBJEKTIF_KELUHANUTAMA = txtSUBJEKTIF.Text
                .SUBJEKTIF_ALERGI_TIDAK = chkALERGI_TIDAK.Checked
                .SUBJEKTIF_ALERGI_YA = chkALERGI_YA.Checked
                .SUBJEKTIF_ALERGI_YA_TEXT = txtALERGIOBAT.Text

                Dim listSubjektif As New List(Of String)

                listSubjektif.Add("Keluhan Utama " & .SUBJEKTIF_KELUHANUTAMA)

                Dim Alergi As String = String.Empty

                If .SUBJEKTIF_ALERGI_TIDAK = True Then
                    Alergi = "Alergi Obat: Tidak"
                End If
                If .SUBJEKTIF_ALERGI_YA = True Then
                    Alergi = "Alergi Obat: Ya" & ", " & .SUBJEKTIF_ALERGI_YA_TEXT
                End If

                'listSubjektif.Add((Alergi & " " & .SUBJEKTIF_ALERGI_YA_TEXT).Trim)

                .SUBJEKTIF_TEXT = String.Join(vbCrLf, listSubjektif.ToArray) & vbCrLf & Alergi

                .OBJEKTIF_KESADARAN = cboTINGKATKESADARAN.Text
                .OBJEKTIF_GCS = txtOBJEKTIF_IMT.Text
                .OBJEKTIF_TAMPAKSAKIT = txtOBJEKTIF_BBIDEAL.Text
                .OBJEKTIF_VISUALANALOGSCORE = txtOBJEKTIF_BBDITURUNKAN.Text
                .OBJEKTIF_BERATBADAN = txtOBJEKTIF_BERATBADAN.Text
                .OBJEKTIF_TINGGIBADAN = txtOBJEKTIF_TINGGIBADAN.Text
                .OBJEKTIF_SPO2 = txtOBJEKTIF_SATURASIOKSIGEN.Text
                .OBJEKTIF_SISTOLE = txtCPPT_Sistole.Text
                .OBJEKTIF_DIASTOLE = txtCPPT_Diastole.Text
                .OBJEKTIF_HR = txtOBJEKTIF_NADI.Text
                .OBJEKTIF_RR = txtOBJEKTIF_RESPIRASI.Text
                .OBJEKTIF_SUHU = txtOBJEKTIF_SUHU.Text
                .OBJEKTIF_PEMERIKSAAN = txtDESKRIPSI.Text
                If sPicture Is Nothing Then
                    .OBJEKTIF_ALAMATGAMBARPEMERIKSAAN = txtALAMATGAMBAR.Text
                Else
                    If sAlamatSimpanFolder <> "" Then
                        If Directory.Exists(sAlamatSimpanFolder) Then
                            Dim Alamat As String = sAlamatSimpanFolder & "CPPT_" & Now.ToString("ddMMyyyyHHmm") & "." & .KDIDENTITAS & ".Png"
                            picGAMBAR2.Image.Save(Alamat, System.Drawing.Imaging.ImageFormat.Png)
                            .OBJEKTIF_ALAMATGAMBARPEMERIKSAAN = Alamat
                        Else
                            MsgBox("Folder Simpan Gambar ke File " & sAlamatSimpanFolder & " Tidak dapat diakses")
                            .OBJEKTIF_ALAMATGAMBARPEMERIKSAAN = txtALAMATGAMBAR.Text
                        End If
                    Else
                        MsgBox("Belum Ada Simpan Gambar")
                        .OBJEKTIF_ALAMATGAMBARPEMERIKSAAN = txtALAMATGAMBAR.Text
                    End If
                End If

                Dim listOBjektif As New List(Of String)
                Dim oRME As New RME.clsRME

                If .OBJEKTIF_KESADARAN <> "" Then
                    listOBjektif.Add("Kesadaran : " & .OBJEKTIF_KESADARAN)
                End If

                listOBjektif.Add("Umur : " & oRME.GetUmurPasien(deDATE.DateTime, deDATETANGGALLAHIR.DateTime))

                If .OBJEKTIF_GCS <> "" Then
                    listOBjektif.Add("IMT : " & .OBJEKTIF_GCS)
                End If
                If .OBJEKTIF_TAMPAKSAKIT <> "" Then
                    listOBjektif.Add("BB Ideal : " & .OBJEKTIF_TAMPAKSAKIT)
                End If
                If .OBJEKTIF_VISUALANALOGSCORE <> "" Then
                    listOBjektif.Add("BB Yang diturunkan : " & .OBJEKTIF_VISUALANALOGSCORE)
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

                .ASSEMENT_INDIKASI = ""

                Dim listAsesment As New List(Of String)
                'Dim listDiagnosaPrimer As New List(Of String)
                'Dim listDiagnosaSekunder As New List(Of String)

                'listAsesment.Add(txtDIAGNOOSA.Text)

                For i As Integer = 0 To grvDiagnosaPenyerta.RowCount - 2
                    listAsesment.Add((IIf(String.IsNullOrEmpty(grvDiagnosaPenyerta.GetRowCellValue(i, colKDDIAGNOSA)), "", grvDiagnosaPenyerta.GetRowCellValue(i, colKDDIAGNOSA) & " ")).ToString.Trim & grvDiagnosaPenyerta.GetRowCellValue(i, colKETERANGAN))
                Next

                'listAsesment.Add(String.Join(", ", listDiagnosaPrimer.ToArray))
                'listAsesment.Add(String.Join(", ", listDiagnosaSekunder.ToArray))

                'If .ASSEMENT_INDIKASI <> "" Then
                '    listAsesment.Add("Indikasi : " & .ASSEMENT_INDIKASI)
                'End If

                .ASSEMENT_TEXT = String.Join(vbCrLf, listAsesment.ToArray)

                .PLANNING_ISTINDAKLANJUT_PULANG = False
                .PLANNING_ISTINDAKLANJUT_RAWAT = False
                .PLANNING_ISTINDAKLANJUT_KONSUL = False
                .PLANNING_ISTINDAKLANJUT_RUJUK = False
                .PLANNING_ISTINDAKLANJUT_KONSUL_TEXT = "DAFTAR_L4_0000000001"
                .PLANNING_ISTINDAKLANJUT_RUJUK_TEXT = ""
                .PLANNING_ALASAN = txtTINDAKLANJUT.Text
                .PLANNING_TEXT = ""

                Dim oSkd As New Admission.clsSKD
                Dim oKunjungan As New Admission.clsPendaftaran_Kunjungan

                Dim dsKunjungan = oKunjungan.GetData(txtKDKUNJUNGAN.Text)

                If dsKunjungan IsNot Nothing Then
                    Dim dsSKD = oSkd.GetDataPendaftaran(dsKunjungan.KDPENDAFTARAN)

                    If dsSKD IsNot Nothing Then
                        'txtTINDAKLANJUT.Text = "Kontrol Tanggal : " & dsSKD.DATEKONTROL.ToString("dd-MM-yyyy")
                        'Alasan = dsSKD.DESCRIPTION & " " & dsSKD.ALASAN
                        'KONSUL
                        ds.PLANNING_ISTINDAKLANJUT_KONSUL_TEXT = "DAFTAR_L4_0000000007"
                    End If
                End If

                .CATATAN = ""
                .KDUSER = sUserID
                Try
                    .ISDELETE = oGrouperDataCppt.GetData(sNoid).ISDELETE
                Catch ex As Exception
                    .ISDELETE = False
                End Try
                Try
                    .DATEDELETE = oGrouperDataCppt.GetData(sNoid).DATEDELETE
                Catch ex As Exception
                    .DATEDELETE = ds.DATECREATED
                End Try
                Try
                    .USERDELETE = oGrouperDataCppt.GetData(sNoid).USERDELETE
                Catch ex As Exception
                    .USERDELETE = ""
                End Try

                .GOALOFTREATMENT = txtGOALOFTREATMENT.Text
                .EDUKASI = txtEDUKASI.Text
                .FREKUENSIKUNJUNGAN = txtFREKUENSIKUNJUNGAN.Text
            End With

            ' ***** DETIL *****
            Dim arrDetailDiagnosa = oGrouperDataCppt.GetStructureDetailDiagnosaList

            'Dim dsDetailPrimer = oGrouperDataCppt.GetStructureDetailDiagnosa
            'With dsDetailPrimer
            '    .DATECREATED = ds.DATECREATED
            '    .DATEUPDATED = ds.DATEUPDATED
            '    .KDCPPT = ds.KDCPPT
            '    .SEQ = 0
            '    .KATEGORI = "Primer"
            '    .KDDIAGNOSA = ""
            '    .MEMO = txtDIAGNOOSA.Text
            '    .KDUSER = sUserID
            'End With

            'arrDetailDiagnosa.Add(dsDetailPrimer)

            For i As Integer = 0 To grvDiagnosaPenyerta.RowCount - 2
                Dim dsDetail = oGrouperDataCppt.GetStructureDetailDiagnosa
                With dsDetail
                    .DATECREATED = ds.DATECREATED
                    .DATEUPDATED = ds.DATEUPDATED
                    .KDCPPT = ds.KDCPPT
                    .SEQ = i
                    .KATEGORI = IIf(String.IsNullOrEmpty(grvDiagnosaPenyerta.GetRowCellValue(i, colKATEGORI)), IIf(i = 0, "Primary", "Secondary"), IIf(grvDiagnosaPenyerta.GetRowCellValue(i, colKATEGORI) = "", IIf(i = 0, "Primary", "Secondary"), grvDiagnosaPenyerta.GetRowCellValue(i, colKATEGORI)))
                    .KDDIAGNOSA = IIf(String.IsNullOrEmpty(grvDiagnosaPenyerta.GetRowCellValue(i, colKDDIAGNOSA)), "", grvDiagnosaPenyerta.GetRowCellValue(i, colKDDIAGNOSA))
                    .MEMO = IIf(String.IsNullOrEmpty(grvDiagnosaPenyerta.GetRowCellValue(i, colKETERANGAN)), "", grvDiagnosaPenyerta.GetRowCellValue(i, colKETERANGAN))
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
                    .ISPERAWAT = IIf(String.IsNullOrEmpty(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_ISPERAWAT)), False, grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_ISPERAWAT))
                End With
                arrDetailTindakan.Add(dsDetail)
            Next

            Dim arrDetailTindakanTerjadwal = oGrouperDataCppt.GetStructureDetailTindakanTerjadwalList
            For i As Integer = 0 To grvTindakanTerjadwal.RowCount - 2
                Dim dsDetail = oGrouperDataCppt.GetStructureDetailTindakanTerjadawal
                With dsDetail
                    .DATECREATED = ds.DATECREATED
                    .DATEUPDATED = ds.DATEUPDATED
                    .TANGGALORDER = CDate(grvTindakanTerjadwal.GetRowCellValue(i, colTANGGALORDERTindakanTerjadwal))
                    .JENISORDER = "TERJADWAL"
                    .KDORDER = ds.KDCPPT
                    .KDIDENTITAS = ds.KDIDENTITAS
                    .NOMORREFERENCE = ds.KDCPPT
                    .KDITEM = grvTindakanTerjadwal.GetRowCellValue(i, colKDITEMTindakanTerjadwal)
                    Dim dsItem = oItem.GetData(grvTindakanTerjadwal.GetRowCellValue(i, colKDITEMTindakanTerjadwal))
                    If dsItem IsNot Nothing Then
                        .NMITEM2 = dsItem.NMITEM2
                    Else
                        .NMITEM2 = ""
                    End If
                    .USERORDER = sUserID
                    .STATUS = ""
                    .MEMO = IIf(String.IsNullOrEmpty(grvTindakanTerjadwal.GetRowCellValue(i, colREMARKSTindakanTerjadwal)), "", grvTindakanTerjadwal.GetRowCellValue(i, colREMARKSTindakanTerjadwal))
                End With
                arrDetailTindakanTerjadwal.Add(dsDetail)
            Next

            Dim arrDetail = oGrouperDataCppt.GetStructureDetailNonRacikanList
            For i As Integer = 0 To grvDetailResep.RowCount - 2
                Dim dsDetail = oGrouperDataCppt.GetStructureDetailNonRacikan
                With dsDetail
                    .DATECREATED = ds.DATECREATED
                    .DATEUPDATED = ds.DATEUPDATED
                    .SEQ = i
                    .KDCPPT = ds.KDCPPT
                    Dim dsItem = oItem.GetData(grvDetailResep.GetRowCellValue(i, colKDITEM))
                    If dsItem IsNot Nothing Then
                        .NAMAOBAT = dsItem.NMITEM2
                    Else
                        .NAMAOBAT = ""
                    End If
                    Dim dsUom = oUom.GetData(grvDetailResep.GetRowCellValue(i, colKDUOM))
                    If dsUom IsNot Nothing Then
                        .SATUAN = dsUom.MEMO
                    Else
                        .SATUAN = ""
                    End If
                    Dim dsSigna = oSiga.GetData(grvDetailResep.GetRowCellValue(i, colKDSIGNA))
                    If dsSigna IsNot Nothing Then
                        .SIGNA = dsSigna.MEMO
                    Else
                        .SIGNA = ""
                    End If
                    Dim dsCaraPakai = oCaraPakai.GetData(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI))
                    If dsCaraPakai IsNot Nothing Then
                        .CARAPAKAI = dsCaraPakai.MEMO
                    Else
                        .CARAPAKAI = ""
                    End If
                    .KDCARAPAKAI = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)), oItem.DefaultItem_CaraPakai, grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI))
                    .JUMLAH_PAKET = CDec(0)
                    .JUMLAH_NONPAKET = CDec(0)
                    .JUMLAH = CDec(grvDetailResep.GetRowCellValue(i, colQTY))
                    .HARGA = CDec(grvDetailResep.GetRowCellValue(i, colPRICE))
                    .TOTAL_PAKET = CDec(0)
                    .TOTAL_NONPAKET = CDec(0)
                    .TOTAL = CDec(grvDetailResep.GetRowCellValue(i, colGRANDTOTAL))
                    .ROMAWI = IntegerToRoman(CInt(grvDetailResep.GetFocusedRowCellValue(colQTY)))
                    .REMARKS_DOKTER = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER)), "", grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER))
                    .KDITEM = grvDetailResep.GetRowCellValue(i, colKDITEM)
                    .ISKRONIS = False
                    .ISALKES = False
                    .KDUOM = grvDetailResep.GetRowCellValue(i, colKDUOM)
                    .KDSIGNA = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDSIGNA)), oItem.DefaultItem_Signa, grvDetailResep.GetRowCellValue(i, colKDSIGNA))
                    .QTY_PERUBAHAN = 0
                    .REMARKS_FARMASI = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colREMARKS_FARMASI)), "", grvDetailResep.GetRowCellValue(i, colREMARKS_FARMASI))
                    .KDUSER = sUserID
                    .ISBACA = "0"
                End With

                arrDetail.Add(dsDetail)
            Next

            Dim arrDetailObatRacikan = oGrouperDataCppt.GetStructureDetailRacikanList
            For i As Integer = 0 To grvOBATRACIKAN.RowCount - 2
                Dim dsDetail = oGrouperDataCppt.GetStructureDetailRacikan
                With dsDetail
                    .DATECREATED = ds.DATECREATED
                    .DATEUPDATED = ds.DATEUPDATED
                    .KDCPPT = ds.KDCPPT
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

            'Dim listTindakanLab As New List(Of String)
            'Dim listTindakanRad As New List(Of String)
            Dim listObatNonRacik As New List(Of String)
            Dim listObatRacik As New List(Of String)
            Dim listPlanningTindakan As New List(Of String)
            Dim listPlanningTindakanPenunjang As New List(Of String)
            Dim listPlanningObat As New List(Of String)
            Dim listPlanningTindakanTerjadwal As New List(Of String)

            For i As Integer = 0 To grvCPPT_Tindakan.RowCount - 2
                Dim dsItem = oItem.GetData(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_KDITEMTINDAKAN))
                If dsItem IsNot Nothing Then
                    If dsItem.M_ITEM_L3.MEMO = "LABORATORIUM" Then
                        If grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_ISPERAWAT) = False Then
                            'listTindakanLab.Add(dsItem.NMITEM2 & " " & IIf(String.IsNullOrEmpty(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)), "", grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)))
                        End If
                        listPlanningTindakanPenunjang.Add(dsItem.NMITEM2 & " " & IIf(String.IsNullOrEmpty(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)), "", grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)))
                    ElseIf dsItem.M_ITEM_L3.MEMO = "BANK DARAH" Then
                        If grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_ISPERAWAT) = False Then
                            'listTindakanLab.Add(dsItem.NMITEM2 & " " & IIf(String.IsNullOrEmpty(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)), "", grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)))
                        End If
                        listPlanningTindakanPenunjang.Add(dsItem.NMITEM2 & " " & IIf(String.IsNullOrEmpty(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)), "", grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)))
                    ElseIf dsItem.M_ITEM_L3.MEMO = "RADIOLOGI" Then
                        'listTindakanRad.Add(dsItem.NMITEM2 & " " & IIf(String.IsNullOrEmpty(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)), "", grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)))
                        listPlanningTindakanPenunjang.Add(dsItem.NMITEM2 & " " & IIf(String.IsNullOrEmpty(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)), "", grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)))
                    Else
                        If dsItem.KDITEM_L1 = "ITEM_L1_0000000002" Then
                            listPlanningTindakan.Add(dsItem.NMITEM2 & " " & IIf(String.IsNullOrEmpty(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)), "", grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)))
                        End If
                    End If
                End If
            Next

            For Each xloop In arrDetail
                listObatNonRacik.Add(xloop.NAMAOBAT)
                'listPlanningObat.Add(xloop.NAMAOBAT & " " & xloop.SATUAN & " " & xloop.SIGNA & " " & xloop.CARAPAKAI & " No " & xloop.ROMAWI & " " & xloop.REMARKS_DOKTER)
                listPlanningObat.Add(xloop.NAMAOBAT & " " & xloop.SATUAN & " " & xloop.SIGNA & " " & xloop.CARAPAKAI & " " & xloop.REMARKS_DOKTER)
            Next

            For Each xloop In arrDetailObatRacikan
                listObatRacik.Add(xloop.NAMAOBAT)
                listPlanningObat.Add(xloop.SEQ + 1 & ". " & xloop.NAMAOBAT & " " & xloop.SATUAN & " " & xloop.SIGNA & " " & xloop.PERMINTAAN & " " & xloop.REMARKS)
            Next

            For Each xloop In arrDetailTindakanTerjadwal
                listPlanningTindakanTerjadwal.Add("Tgl " & xloop.TANGGALORDER.ToString("dd-MM-yyy") & " Cek " & xloop.NMITEM2 & IIf(xloop.MEMO = "", "", xloop.MEMO))
            Next

            Dim TindakLanjut As String = IIf(txtTINDAKLANJUT.Text = "", "", "Tindak Lanjut : " & txtTINDAKLANJUT.Text)

            Dim Planning_Text As String = String.Empty
            If listPlanningTindakan.Count > 0 Then
                Planning_Text = Planning_Text & "Tindakan di Poli : " & String.Join(vbCrLf, listPlanningTindakan.ToArray)
            End If
            If listPlanningTindakanPenunjang.Count > 0 Then
                Planning_Text = Planning_Text & vbCrLf & vbCrLf & "Pemeriksaan Penunjang : " & String.Join(vbCrLf, listPlanningTindakanPenunjang.ToArray)
            End If
            sPenunjnag = ""

            If sKDPENDAFTARAN <> "" Then
                LoadPenunjang(sKDPENDAFTARAN)
                If sPenunjnag <> "" Then
                    Planning_Text = Planning_Text & vbCrLf & "Pemeriksaan Penunjang : " & vbCrLf & sPenunjnag
                End If
            End If


            If listPlanningObat.Count > 0 Then
                Planning_Text = Planning_Text & vbCrLf & vbCrLf & "Medikamentosa : " & String.Join(vbCrLf, listPlanningObat.ToArray)
            End If

            If TindakLanjut <> "" Then
                Planning_Text = Planning_Text & vbCrLf & vbCrLf & TindakLanjut
            End If

            If listPlanningTindakanTerjadwal.Count > 0 Then
                'Tindak lanjut Terjadwal
                Planning_Text = Planning_Text & vbCrLf & String.Join(vbCrLf, listPlanningTindakanTerjadwal.ToArray)
            End If

            ds.PLANNING_TEXT = Planning_Text.ToString.Trim

            ds.TINDAKANREHAB = ds.PLANNING_TEXT

            If oFormModeCPPT = FORM_MODE.FORM_MODE_ADD Then
                sNoid = oGrouperDataCppt.InsertData(ds, arrDetailDiagnosa, arrDetailTindakan, arrDetail, arrDetailObatRacikan, arrDetailTindakanTerjadwal)
                If sNoid = "" Then
                    fn_Save = False
                Else
                    fn_Save = True
                End If
            ElseIf oFormModeCPPT = FORM_MODE.FORM_MODE_EDIT Then
                fn_Save = oGrouperDataCppt.UpdateData(ds, arrDetailDiagnosa, arrDetailTindakan, arrDetail, arrDetailObatRacikan, arrDetailTindakanTerjadwal)
            End If

            If fn_Save = True Then
                Dim oAdmision As New Admission.clsPendaftaran_Kunjungan
                Dim dsKunjungan = oAdmision.GetData(dsIdentitas.KDKUNJUNGAN)

                If dsKunjungan IsNot Nothing Then
                    If dsKunjungan.S_PENDAFTARAN_H.CATEGORY = 0 Then
                        fn_SaveTranskasiPoli(ds.KDCPPT, dsKunjungan.S_PENDAFTARAN_H.CATEGORY, dsKunjungan.KDKUNJUNGAN, dsKunjungan.KDDOCTOR, dsKunjungan.KDDEPARTMENT)
                    End If
                End If

                Dim sudah As Boolean = False

                If lblBacaFarmasi.Text = "Belum Terima Farmasi" Then
                    If listObatNonRacik.Count > 0 Then
                        sudah = True
                        Try
                            Dim dsNomorRefrence = oOrder.GetDataNoReference(ds.KDCPPT, "ORDER FARMASI")
                            Dim NoOrder As String = String.Empty
                            Dim StatusOrder As String = String.Empty
                            Dim JenisOrder As String = String.Empty

                            If dsNomorRefrence IsNot Nothing Then
                                NoOrder = dsNomorRefrence.KDORDER
                                If dsNomorRefrence.STATUS = "TAMBAH" Then
                                    StatusOrder = "UPDATE"
                                Else
                                    StatusOrder = "UPDATE"
                                    'StatusOrder = dsNomorRefrence.STATUS
                                End If

                                JenisOrder = dsNomorRefrence.JENISORDER
                            Else
                                Dim AdaObatRacik As Boolean = False

                                If listObatRacik.Count > 0 Then
                                    AdaObatRacik = True
                                End If

                                StatusOrder = "TAMBAH"
                                If sCategory = 0 Then
                                    If AdaObatRacik = False Then
                                        JenisOrder = "FARRJ"
                                    Else
                                        JenisOrder = "RARRJ"
                                    End If
                                Else
                                    If AdaObatRacik = False Then
                                        JenisOrder = "FARRI"
                                    Else
                                        JenisOrder = "RARRI"
                                    End If
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
                End If

                If listObatRacik.Count > 0 Then
                    If sudah = False Then
                        Try
                            Dim dsNomorRefrence = oOrder.GetDataNoReference(ds.KDCPPT, "ORDER FARMASI")
                            Dim NoOrder As String = String.Empty
                            Dim StatusOrder As String = String.Empty
                            Dim JenisOrder As String = String.Empty

                            If dsNomorRefrence IsNot Nothing Then
                                NoOrder = dsNomorRefrence.KDORDER
                                If dsNomorRefrence.STATUS = "TAMBAH" Then
                                    StatusOrder = "UPDATE"
                                Else
                                    StatusOrder = "UPDATE"
                                    'StatusOrder = dsNomorRefrence.STATUS
                                End If

                                JenisOrder = dsNomorRefrence.JENISORDER
                            Else
                                StatusOrder = "TAMBAH"
                                If sCategory = 0 Then
                                    JenisOrder = "RARRJ"
                                Else
                                    JenisOrder = "RARRI"
                                End If
                            End If

                            'If String.Join(", ", listTindakanLab.ToArray) <> String.Join(", ", ListLoadDataLaboratorium.ToArray) Then
                            '    StatusOrder = "UPDATE"
                            'End If

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
                End If

                'Dim oItem As New Reference.clsItem
                'Dim oOrder As New Digital.clsR_Order
                Dim listTindakanLab As New List(Of String)
                Dim listTindakanRad As New List(Of String)

                For i As Integer = 0 To grvCPPT_Tindakan.RowCount - 2
                    If grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_ISPERAWAT) = False Then
                        Dim dsItem = oItem.GetData(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_KDITEMTINDAKAN))
                        If dsItem IsNot Nothing Then
                            If dsItem.M_ITEM_L3.MEMO = "LABORATORIUM" Then
                                listTindakanLab.Add(dsItem.NMITEM2)
                            ElseIf dsItem.M_ITEM_L3.MEMO = "BANK DARAH" Then
                                listTindakanLab.Add(dsItem.NMITEM2)
                            ElseIf dsItem.M_ITEM_L3.MEMO = "RADIOLOGI" Then
                                listTindakanRad.Add(dsItem.NMITEM2)
                            End If
                        End If
                    End If
                Next

                If listTindakanLab.Count > 0 Then
                    Dim UpddateLab As Boolean = False
                    Dim TambahHasil1 = String.Join(", ", listTindakanLab.ToArray)
                    Dim TambahHasil2 = String.Join(", ", listTindakanLab_LoadData.ToArray)

                    For Each yloop In listTindakanLab_LoadData
                        If Not TambahHasil1.Contains(yloop) Then
                            UpddateLab = True
                        End If
                    Next

                    For Each yloop In listTindakanLab
                        If Not TambahHasil2.Contains(yloop) Then
                            UpddateLab = True
                        End If
                    Next

                    Dim dsNomorRefrenceLab = oOrder.GetDataNoReference(ds.KDCPPT, "ORDER LABORATORIUM")
                    If dsNomorRefrenceLab Is Nothing Then
                        'Add

                        Dim dsOrder = oOrder.GetStructureHeader
                        With dsOrder
                            .DATECREATED = ds.DATECREATED
                            .DATEUPDATED = ds.DATEUPDATED
                            .TANGGALORDER = ds.DATE
                            .JENISORDER = IIf(sCategory = 0, "LABRJ", "LABRI")
                            .KDORDER = ""
                            .KDIDENTITAS = ds.KDIDENTITAS
                            .NOMORREFERENCE = ds.KDCPPT
                            .USERORDER = ds.KDUSER
                            .STATUS = "TAMBAH"
                            .MEMO = "ORDER LABORATORIUM"
                        End With

                        oOrder.InsertData(dsOrder)
                    Else
                        'Update

                        If UpddateLab = True Then
                            Dim dsOrder = oOrder.GetStructureHeader
                            With dsOrder
                                .DATECREATED = ds.DATECREATED
                                .DATEUPDATED = ds.DATEUPDATED
                                .TANGGALORDER = ds.DATE
                                .JENISORDER = IIf(sCategory = 0, "LABRJ", "LABRI")
                                .KDORDER = dsNomorRefrenceLab.KDORDER
                                .KDIDENTITAS = ds.KDIDENTITAS
                                .NOMORREFERENCE = ds.KDCPPT
                                .USERORDER = ds.KDUSER
                                .STATUS = "UPDATE"
                                .MEMO = "ORDER LABORATORIUM"
                            End With

                            oOrder.UpdateData(dsOrder)
                        Else
                            If dsNomorRefrenceLab.STATUS = "DELETE" Then
                                Dim dsOrder = oOrder.GetStructureHeader
                                With dsOrder
                                    .DATECREATED = ds.DATECREATED
                                    .DATEUPDATED = ds.DATEUPDATED
                                    .TANGGALORDER = ds.DATE
                                    .JENISORDER = IIf(sCategory = 0, "LABRJ", "LABRI")
                                    .KDORDER = dsNomorRefrenceLab.KDORDER
                                    .KDIDENTITAS = ds.KDIDENTITAS
                                    .NOMORREFERENCE = ds.KDCPPT
                                    .USERORDER = ds.KDUSER
                                    .STATUS = "TAMBAH"
                                    .MEMO = "ORDER LABORATORIUM"
                                End With

                                oOrder.UpdateData(dsOrder)
                            End If
                        End If
                    End If
                Else
                    Dim dsNomorRefrenceLab = oOrder.GetDataNoReference(ds.KDCPPT, "ORDER LABORATORIUM")
                    If dsNomorRefrenceLab IsNot Nothing Then
                        'delete
                        Dim dsOrder = oOrder.GetStructureHeader
                        With dsOrder
                            .DATECREATED = ds.DATECREATED
                            .DATEUPDATED = ds.DATEUPDATED
                            .TANGGALORDER = ds.DATE
                            .JENISORDER = IIf(sCategory = 0, "LABRJ", "LABRI")
                            .KDORDER = dsNomorRefrenceLab.KDORDER
                            .KDIDENTITAS = ds.KDIDENTITAS
                            .NOMORREFERENCE = ds.KDCPPT
                            .USERORDER = ds.KDUSER
                            .STATUS = "DELETE"
                            .MEMO = "ORDER LABORATORIUM"
                        End With

                        oOrder.UpdateData(dsOrder)
                    End If
                End If

                If listTindakanRad.Count > 0 Then
                    Dim UpddateLab As Boolean = False
                    Dim TambahHasil1 = String.Join(", ", listTindakanRad.ToArray)
                    Dim TambahHasil2 = String.Join(", ", listTindakanRad_LoadData.ToArray)

                    For Each yloop In listTindakanRad_LoadData
                        If Not TambahHasil1.Contains(yloop) Then
                            UpddateLab = True
                        End If
                    Next

                    For Each yloop In listTindakanRad
                        If Not TambahHasil2.Contains(yloop) Then
                            UpddateLab = True
                        End If
                    Next

                    Dim dsNomorRefrenceRad = oOrder.GetDataNoReference(ds.KDCPPT, "ORDER RADIOLOGI")
                    If dsNomorRefrenceRad Is Nothing Then
                        'Add

                        Dim dsOrder = oOrder.GetStructureHeader
                        With dsOrder
                            .DATECREATED = ds.DATECREATED
                            .DATEUPDATED = ds.DATEUPDATED
                            .TANGGALORDER = ds.DATE
                            .JENISORDER = IIf(sCategory = 0, "RADRJ", "RADRI")
                            .KDORDER = ""
                            .KDIDENTITAS = ds.KDIDENTITAS
                            .NOMORREFERENCE = ds.KDCPPT
                            .USERORDER = ds.KDUSER
                            .STATUS = "TAMBAH"
                            .MEMO = "ORDER RADIOLOGI"
                        End With

                        oOrder.InsertData(dsOrder)
                    Else
                        'Update

                        If UpddateLab = True Then
                            Dim dsOrder = oOrder.GetStructureHeader
                            With dsOrder
                                .DATECREATED = ds.DATECREATED
                                .DATEUPDATED = ds.DATEUPDATED
                                .TANGGALORDER = ds.DATE
                                .JENISORDER = IIf(sCategory = 0, "RADRJ", "RADRI")
                                .KDORDER = dsNomorRefrenceRad.KDORDER
                                .KDIDENTITAS = ds.KDIDENTITAS
                                .NOMORREFERENCE = ds.KDCPPT
                                .USERORDER = ds.KDUSER
                                .STATUS = "UPDATE"
                                .MEMO = "ORDER RADIOLOGI"
                            End With

                            oOrder.UpdateData(dsOrder)
                        Else
                            If dsNomorRefrenceRad.STATUS = "DELETE" Then
                                Dim dsOrder = oOrder.GetStructureHeader
                                With dsOrder
                                    .DATECREATED = ds.DATECREATED
                                    .DATEUPDATED = ds.DATEUPDATED
                                    .TANGGALORDER = ds.DATE
                                    .JENISORDER = IIf(sCategory = 0, "RADRJ", "RADRI")
                                    .KDORDER = dsNomorRefrenceRad.KDORDER
                                    .KDIDENTITAS = ds.KDIDENTITAS
                                    .NOMORREFERENCE = ds.KDCPPT
                                    .USERORDER = ds.KDUSER
                                    .STATUS = "TAMBAH"
                                    .MEMO = "ORDER RADIOLOGI"
                                End With

                                oOrder.UpdateData(dsOrder)
                            End If
                        End If
                    End If
                Else
                    Dim dsNomorRefrenceRad = oOrder.GetDataNoReference(ds.KDCPPT, "ORDER RADIOLOGI")
                    If dsNomorRefrenceRad IsNot Nothing Then
                        'delete
                        Dim dsOrder = oOrder.GetStructureHeader
                        With dsOrder
                            .DATECREATED = ds.DATECREATED
                            .DATEUPDATED = ds.DATEUPDATED
                            .TANGGALORDER = ds.DATE
                            .JENISORDER = IIf(sCategory = 0, "RADRJ", "RADRI")
                            .KDORDER = dsNomorRefrenceRad.KDORDER
                            .KDIDENTITAS = ds.KDIDENTITAS
                            .NOMORREFERENCE = ds.KDCPPT
                            .USERORDER = ds.KDUSER
                            .STATUS = "DELETE"
                            .MEMO = "ORDER RADIOLOGI"
                        End With

                        oOrder.UpdateData(dsOrder)
                    End If
                End If
            End If

            If fn_Save = True Then
                oGrouperDataCppt.UpdateDaftarL6(sKDPENDAFTARAN, "DAFTAR_L6_0000000003")

                'Dim SuaraFarmasi As Boolean = False

                'If arrDetail.Count > 0 Then
                '    SuaraFarmasi = True
                'End If
                'If arrDetailObatRacikan.Count > 0 Then
                '    SuaraFarmasi = True
                'End If

                'If SuaraFarmasi = True Then
                '    Dim oSuara As New Digital.clsSuara
                '    oSuara.DeleteData(0)

                '    Dim dsOrder = oSuara.GetStructureHeader
                '    With dsOrder
                '        .DATECREATED = ds.DATECREATED
                '        .DATEUPDATED = ds.DATEUPDATED
                '        .KDSUARA = 0
                '        .MEMO = "SUARA FARMASI"
                '        .ISCHEKED = False
                '        .KDUSER = sUserID
                '    End With
                'End If
            End If
        Catch oErr As Exception
            fn_Save = False
            MsgBox("Simpan CPPT" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
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
                .DATE = deDATE.DateTime
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
                    .DATECREATED = deDATE.DateTime
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
                        MsgBox("Simpan Billing" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                Else
                    Try
                        fn_SaveTranskasiPoli = oSalesOrderTransaksi.UpdateData(ds, arrDetail)
                    Catch oErr As Exception
                        MsgBox("Simpan Billing" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                End If
            End If

        Catch oErr As Exception
            MsgBox("Simpan Billing" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveTranskasiPoli = False
        End Try
    End Function
    Private Sub grdCariDiagnosaiDRG_EditValueChanged(sender As Object, e As EventArgs) Handles grdCariDiagnosaiDRG.EditValueChanged
        If grdCariDiagnosaiDRG.Text <> "" Then
            If grvCariDiagnosaiDRG.GetFocusedRowCellValue("ISDEFAULT") Is Nothing Then
                Exit Sub
            End If

            Dim sCek As Integer = 0

            For i As Integer = 0 To grvDiagnosaPenyerta.RowCount - 2
                sCek += 1
            Next

            If sCek = 0 Then
                If CBool(grvCariDiagnosaiDRG.GetFocusedRowCellValue("ISACTIVE")) = False Then
                    MsgBox("Diagnosa " & grdCariDiagnosaiDRG.EditValue & " Tidak Bisa Jadi Primery", MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                End If
            End If

            If CBool(grvCariDiagnosaiDRG.GetFocusedRowCellValue("ISDEFAULT")) = False Then
                MsgBox("Diagnosa " & grdCariDiagnosaiDRG.EditValue & " Tidak Valid Untuk Grouper", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            grvDiagnosaPenyerta.Focus()
            grvDiagnosaPenyerta.AddNewRow()
            grvDiagnosaPenyerta.SetFocusedRowCellValue(colKETERANGAN, grdCariDiagnosaiDRG.Text)
            grvDiagnosaPenyerta.SetFocusedRowCellValue(colKDDIAGNOSA, grdCariDiagnosaiDRG.EditValue)
            grvDiagnosaPenyerta.SetFocusedRowCellValue(colKATEGORI, IIf(sCek = 0, "Primary", "Secondary"))
            grvDiagnosaPenyerta.UpdateCurrentRow()

            '.DATECREATED = ds.DATECREATED
            '.DATEUPDATED = ds.DATEUPDATED
            '.KDCPPT = ds.KDCPPT
            '.SEQ = i
            '.KATEGORI = IIf(i = 0, "Primary", "Secondary")
            '.KDDIAGNOSA = IIf(String.IsNullOrEmpty(grvDiagnosaPenyerta.GetRowCellValue(i, colKDDIAGNOSA)), "", grvDiagnosaPenyerta.GetRowCellValue(i, colKDDIAGNOSA))
            '.MEMO = IIf(String.IsNullOrEmpty(grvDiagnosaPenyerta.GetRowCellValue(i, colKETERANGAN)), "", grvDiagnosaPenyerta.GetRowCellValue(i, colKETERANGAN))
            '.KDUSER = sUserID
        End If
    End Sub
#Region "Grid Method"
    Private Sub OnValueChangedTindakan(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grvCPPT_Tindakan.FocusedRowChanged, grvDetailResep.FocusedRowChanged, grvOBATRACIKAN.FocusedRowChanged
        If isLoad Then
            Calculate()
        End If
    End Sub
    Private Sub Calculate()
        If CDec(lblTarifInacbg.Text) = 0 Then
            lblTarifInacbg.Text = FormatNumber(sInacbgTarifRawatJalanDefault, 0)
        End If

        Dim sSubTotalTindakan As Decimal = 0

        For i As Integer = 0 To grvCPPT_Tindakan.RowCount - 2
            sSubTotalTindakan += CDec(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_TOTALTINDAKAN))
        Next

        Dim sSubTotalNonRacikanPaket As Decimal = 0
        Dim sSubTotalNonRacikanNonPaket As Decimal = 0
        Dim sSubTotalNonRacikanKronis As Decimal = 0

        For i As Integer = 0 To grvDetailResep.RowCount - 2
            sSubTotalNonRacikanNonPaket += CDec(grvDetailResep.GetRowCellValue(i, colGRANDTOTAL))
        Next

        Dim sSubTotalRacikan = 0
        For i As Integer = 0 To grvOBATRACIKAN.RowCount - 2
            'sSubTotalRacikan += CDec(grvOBATRACIKAN.GetRowCellValue(i, colCPPT_TOTALRACIKAN))
        Next

        If txtPenjamin.Text.Contains("BPJS") Then
            lblGRANDTOTAL.Text = FormatNumber(sSubTotalNonRacikanNonPaket)
            lblGRANDTOTAL_TINDAKAN.Text = FormatNumber(sSubTotalTindakan, 0)

            lblTarifRS.Text = FormatNumber(sSubTotalTindakan + sSubTotalNonRacikanNonPaket, 0)

            lblTarifPaket.Text = FormatNumber((sSubTotalTindakan + sSubTotalNonRacikanNonPaket) - sSubTotalNonRacikanKronis, 0)

            lblSelisih.Text = FormatNumber(sInacbgTarifRawatJalanDefault - ((sSubTotalTindakan + sSubTotalNonRacikanNonPaket) - sSubTotalNonRacikanKronis), 0)

            If CDec(sInacbgTarifRawatJalanDefault - ((sSubTotalTindakan + sSubTotalNonRacikanNonPaket) - sSubTotalNonRacikanKronis)) < 0 Then
                lblSelisih.ForeColor = Color.Red
            Else
                lblSelisih.ForeColor = Color.Black
            End If
        Else
            lblGRANDTOTAL.Text = FormatNumber(sSubTotalNonRacikanNonPaket)
            lblGRANDTOTAL_TINDAKAN.Text = FormatNumber(sSubTotalTindakan, 0)

            lblTarifRS.Text = FormatNumber(sSubTotalTindakan + sSubTotalNonRacikanNonPaket, 0)

            lblTarifPaket.Text = 0

            lblSelisih.Text = 0
        End If


    End Sub
    'Private Sub grvCPPT_Diagnosa_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvCPPT_Diagnosa.CellValueChanged
    '    If e.Column.Name = colCPPT_MEMODIAGNOSA.Name Then
    '        If grvCPPT_Diagnosa.GetFocusedRowCellValue(colCPPT_MEMODIAGNOSA) IsNot Nothing Then
    '            Dim sCek As Integer = 0

    '            For i As Integer = 0 To grvCPPT_Diagnosa.RowCount - 2
    '                sCek += 1
    '            Next

    '            grvCPPT_Diagnosa.SetFocusedRowCellValue(colCPPT_KATEGORI, If(sCek = 0, "Primer", "Sekunder"))
    '        End If
    '    End If
    'End Sub
    Private Sub grvCPPT_Tindakan_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvCPPT_Tindakan.CellValueChanged
        Dim oItem As New Reference.clsItem

        If e.Column.Name = colCPPT_KDITEMTINDAKAN.Name Then
            If grvCPPT_Tindakan.GetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN) IsNot Nothing Then
                Dim ds = oItem.GetDataDetail_UOM(grvCPPT_Tindakan.GetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN))

                If ds IsNot Nothing Then
                    Try
                        grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_KDUOMTINDAKAN, ds.FirstOrDefault(Function(x) x.M_UOM.KDKELASRAWAT = sKelas).KDUOM)
                    Catch ex As Exception
                        Try
                            grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_KDUOMTINDAKAN, ds.FirstOrDefault(Function(x) x.RATE = 1).KDUOM)
                        Catch ex2 As Exception

                        End Try
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
                        MsgBox("Load Data Uom", MsgBoxStyle.Exclamation, Me.Text)

                        Dim sItem = grvCPPT_Tindakan.GetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN)
                        grvCPPT_Tindakan.CancelUpdateCurrentRow()

                        grvCPPT_Tindakan.AddNewRow()
                        grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN, sItem)
                    End If
                End If
            Catch oErr As Exception
                MsgBox("Load Data Tindakan" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub grvTindakanTerjadwal_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvTindakanTerjadwal.CellValueChanged
        Dim oItem As New Reference.clsItem

        If e.Column.Name = colKDITEMTindakanTerjadwal.Name Then
            If grvTindakanTerjadwal.GetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN) IsNot Nothing Then
                Dim ds = oItem.GetDataDetail_UOM(grvTindakanTerjadwal.GetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN))

                If ds IsNot Nothing Then
                    Try
                        grvTindakanTerjadwal.SetFocusedRowCellValue(colREMARKSTindakanTerjadwal, "")
                        grvTindakanTerjadwal.SetFocusedRowCellValue(colTANGGALORDERTindakanTerjadwal, Now)
                        'grvTindakanTerjadwal.SetFocusedRowCellValue(COLKDUO, ds.FirstOrDefault(Function(x) x.M_UOM.KDKELASRAWAT = sKelas).KDUOM)
                    Catch ex As Exception
                        'grvTindakanTerjadwal.SetFocusedRowCellValue(colCPPT_KDUOMTINDAKAN, ds.FirstOrDefault(Function(x) x.RATE = 1).KDUOM)
                    End Try
                End If
            End If
            'ElseIf e.Column.Name = colCPPT_KDUOMTINDAKAN.Name Then
            '    Try
            '        If grvTindakanTerjadwal.GetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN) IsNot Nothing And grvTindakanTerjadwal.GetFocusedRowCellValue(colCPPT_KDUOMTINDAKAN) IsNot Nothing Then
            '            Dim ds = oItem.GetDataDetail_UOM(grvTindakanTerjadwal.GetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN), grvTindakanTerjadwal.GetFocusedRowCellValue(colCPPT_KDUOMTINDAKAN))

            '            If ds IsNot Nothing Then
            '                grvTindakanTerjadwal.SetFocusedRowCellValue(colCPPT_JUMLAHTINDAKAN, 1)
            '                grvTindakanTerjadwal.SetFocusedRowCellValue(colCPPT_HARGATINDAKAN, ds.PRICESALESSTANDARD)
            '                grvTindakanTerjadwal.SetFocusedRowCellValue(colCPPT_TOTALTINDAKAN, ds.PRICESALESSTANDARD)
            '            Else
            '                MsgBox("Load Data Uom", MsgBoxStyle.Exclamation, Me.Text)

            '                Dim sItem = grvTindakanTerjadwal.GetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN)
            '                grvTindakanTerjadwal.CancelUpdateCurrentRow()

            '                grvTindakanTerjadwal.AddNewRow()
            '                grvTindakanTerjadwal.SetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN, sItem)
            '            End If
            '        End If
            '    Catch oErr As Exception
            '        MsgBox("Load Data Tindakan" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            '    End Try
        End If
    End Sub
    Private Sub grvDetailResep_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDetailResep.CellValueChanged
        Try
            If e.Column.Name = colKDITEM.Name Then
                If grvDetailResep.GetFocusedRowCellValue(colKDITEM) IsNot Nothing Then
                    Dim oItem As New Reference.clsItem
                    Dim ds = oItem.GetDataDetail_UOM(grvDetailResep.GetFocusedRowCellValue(colKDITEM))

                    If ds IsNot Nothing Then
                        grvDetailResep.SetFocusedRowCellValue(colKDUOM, ds.FirstOrDefault(Function(x) x.RATE = 1).KDUOM)
                        grvDetailResep.SetFocusedRowCellValue(colREMARKS_DOKTER, "")
                        grvDetailResep.SetFocusedRowCellValue(colREMARKS_FARMASI, IIf(ds.FirstOrDefault.M_ITEM.KDITEM_L5.Contains("INTOLERANSI OBAT"), "Ya", "No"))

                        'grvDetailResep.SetFocusedRowCellValue(colCPPT_ISBACANONRACIKAN, "0")
                        'grvDetailResep.SetFocusedRowCellValue(colCPPT_ISALKESNONRACIKAN, IIf(ds.FirstOrDefault.M_ITEM.M_ITEM_L3.MEMO = "ALKES", True, False))
                        'grvDetailResep.SetFocusedRowCellValue(colCPPT_ISALKESNONRACIKAN, IIf(ds.FirstOrDefault.M_ITEM.M_ITEM_L3.MEMO = "OBAT KRONIS", True, False))
                        grvDetailResep.SetFocusedRowCellValue(colQTY, 1)
                    End If
                End If
            ElseIf e.Column.Name = colREMARKS_DOKTER.Name Then
                'grvTindakan.Focus()
                'grvTindakan.AddNewRow()
                'grvTindakan.SetFocusedRowCellValue(colTERAPI, fn_LoadITEM(grvDetailResep.GetFocusedRowCellValue(colKDITEM)) & " " & fn_LoadSIGNA(grvDetailResep.GetFocusedRowCellValue(colKDSIGNA)))
                'grvTindakan.SetFocusedRowCellValue(colTINDAKAN, "")
                'grvTindakan.UpdateCurrentRow()
            ElseIf e.Column.Name = colKDUOM.Name Then
                Dim oItem As New Reference.clsItem
                Dim ds = oItem.GetDataDetail_UOM(grvDetailResep.GetFocusedRowCellValue(colKDITEM))

                'Dim hargajualawal As Decimal = fn_LoadUOMKDUOMHARGA(grvDetailResep.GetFocusedRowCellValue(colKDITEM), grvDetailResep.GetFocusedRowCellValue(colKDUOM))
                'Dim margin As Decimal = fn_LoadUOMKDUOMMARGIN(grvDetailResep.GetFocusedRowCellValue(colKDITEM), grvDetailResep.GetFocusedRowCellValue(colKDUOM))

                'Dim sPriceSales As Decimal = hargajualawal + (hargajualawal * (margin / 100))
                If sHargaApotik = True Then
                    If txtPenjamin.Text <> "BPJS" Then
                        grvDetailResep.SetFocusedRowCellValue(colPRICE, ds.FirstOrDefault.PRICESALESSTANDARD)
                    Else
                        grvDetailResep.SetFocusedRowCellValue(colPRICE, ds.FirstOrDefault.PRICESALESTERMIN)
                    End If
                Else
                    grvDetailResep.SetFocusedRowCellValue(colPRICE, ds.FirstOrDefault.PRICEPURCHASESTANDARD)

                    If CDec(grvDetailResep.GetFocusedRowCellValue(colPRICE)) > 0 Then
                        Dim sPriceSetelahPPN = 0 + CDec(grvDetailResep.GetFocusedRowCellValue(colPRICE))
                        Dim sPriceTermin = buletin(CDec(sPriceSetelahPPN * (IIf(txtPenjamin.Text <> "BPJS", (ds.FirstOrDefault.MARGIN / 100), (ds.FirstOrDefault.PRICESALESTERMIN / 100))) + CDec(sPriceSetelahPPN)), 100)

                        grvDetailResep.SetFocusedRowCellValue(colPRICE, sPriceTermin)
                    Else
                        grvDetailResep.SetFocusedRowCellValue(colPRICE, 0)
                    End If
                End If

                'grvDetailResep.SetFocusedRowCellValue(colPRICE, ds.FirstOrDefault.PRICESALESSTANDARD)
            ElseIf e.Column.Name = colQTY.Name Or e.Column.Name = colPRICE.Name Then
                Dim sSubTotal As Decimal = CDec(grvDetailResep.GetFocusedRowCellValue(colQTY)) * CDec(grvDetailResep.GetFocusedRowCellValue(colPRICE))
                grvDetailResep.SetFocusedRowCellValue(colGRANDTOTAL, sSubTotal)
            End If

        Catch oErr As Exception
            MsgBox("Event : " & vbCrLf & oErr.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub
    Private Function buletin(ByVal Number As Double, Optional ByVal Range As Integer = 10) As Decimal

        buletin = Math.Round(Number / Range, 0) * Range

    End Function
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
    Private Sub grvDiagnosaPenyerta_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDiagnosaPenyerta.CellValueChanged
        If e.Column.Name = colKETERANGAN.Name Then
            If grvDiagnosaPenyerta.GetFocusedRowCellValue(colKETERANGAN) IsNot Nothing Then
                grvDiagnosaPenyerta.SetFocusedRowCellValue(colKDDIAGNOSA, "")

                Dim sCek As Integer = 0
                Dim sCekTex As String = String.Empty

                For i As Integer = 0 To grvDiagnosaPenyerta.RowCount - 2
                    sCekTex = grvDiagnosaPenyerta.GetFocusedRowCellValue(colKATEGORI)
                    sCek += 1
                Next
                If sCekTex = "" Then
                    grvDiagnosaPenyerta.SetFocusedRowCellValue(colKATEGORI, IIf(sCek = 0, "Primary", "Secondary"))
                End If
            End If
        End If
    End Sub
    Private Sub grdDPJP_EditValueChanged(sender As Object, e As EventArgs) Handles grdDPJP.EditValueChanged
        If grdDPJP.Text <> "" Then
            fn_LoadJadwalDokter(grdDPJP.EditValue)
        End If
    End Sub
    '    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs)
    '        'picGAMBAR2.Image = CType(My.Resources.ResourceManager.GetObject("image1"), Image)
    '        sPicture = Nothing
    '    End Sub
    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        Dim frmPopUp_Image As New frmPopUp_img
        frmPopUp_Image.fn_LoadMe(picGAMBAR2.Image)
        frmPopUp_Image.ShowDialog()

        If sPicture IsNot Nothing Then
            picGAMBAR2.Image = sPicture
        End If

        picGAMBAR2.Focus()
    End Sub
    '    Private Sub cboPOLIMATA_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPOLIMATA.SelectedIndexChanged
    '        If isLoad = True Then
    '            grvTindakanPoli.Focus()
    '            grvTindakanPoli.AddNewRow()
    '            grvTindakanPoli.SetFocusedRowCellValue(colKETERANGAN_, cboPOLIMATA.Text)
    '            grvTindakanPoli.UpdateCurrentRow()
    '        End If
    '    End Sub
    Private Sub grdCPPT_TemplateTindakan_EditValueChanged(sender As Object, e As EventArgs)
        If isLoad = True Then
            If grdCPPT_TemplateTindakan.Text <> "" Then
                Dim oGrouperDataCppt_Template As New Template.clsCPPT_TemplateProsedur

                For Each xloop In oGrouperDataCppt_Template.GetDataDetail(grdCPPT_TemplateTindakan.EditValue).OrderBy(Function(x) x.SEQ)
                    grvCPPT_Tindakan.Focus()
                    grvCPPT_Tindakan.AddNewRow()
                    'grvTindakanPoli.SetFocusedRowCellValue(colKETERANGAN_, xloop.KDITEM )
                    grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_MEMOTINDAKAN, xloop.M_ITEM.NMITEM2)
                    grvCPPT_Tindakan.UpdateCurrentRow()
                Next
            End If
        End If
    End Sub
    Private Sub btnCopyResepTerakhir_Click(sender As Object, e As EventArgs) Handles btnCopyResepTerakhir.Click
        'salin resep
        Dim ds = oGrouperDataCppt.GetDataByRmTerakhirByUserAndDokter(txtKDCUSTOMER.Text, sUserID)
        Dim kosong As Boolean = False

        If ds IsNot Nothing Then
            For Each iLoop In oGrouperDataCppt.GetDataDetailNonRacikan(ds.KDCPPT).OrderBy(Function(x) x.SEQ)
                kosong = True
                grvDetailResep.Focus()
                grvDetailResep.AddNewRow()

                grvDetailResep.SetFocusedRowCellValue(colKDITEM, iLoop.KDITEM)
                grvDetailResep.SetFocusedRowCellValue(colQTY, iLoop.JUMLAH)
                grvDetailResep.SetFocusedRowCellValue(colREMARKS_DOKTER, iLoop.REMARKS_DOKTER)
                grvDetailResep.SetFocusedRowCellValue(colKDUOM, iLoop.KDUOM)
                grvDetailResep.SetFocusedRowCellValue(colKDSIGNA, iLoop.KDSIGNA)
                grvDetailResep.SetFocusedRowCellValue(colKDCARAPAKAI, iLoop.KDCARAPAKAI)
                grvDetailResep.SetFocusedRowCellValue(colREMARKS_FARMASI, iLoop.REMARKS_FARMASI)
                grvDetailResep.UpdateCurrentRow()
            Next

            For Each iLoop In oGrouperDataCppt.GetDataDetailRacikan(ds.KDCPPT).OrderBy(Function(x) x.SEQ)
                kosong = True

                grvDetailResep.Focus()
                grvDetailResep.AddNewRow()
                grvOBATRACIKAN.SetFocusedRowCellValue(colKDITEMOBATRACIKAN, iLoop.KDITEM)
                grvOBATRACIKAN.SetFocusedRowCellValue(colQTYOBATRACIKAN, iLoop.JUMLAH)
                grvOBATRACIKAN.SetFocusedRowCellValue(colREMARKSRACIKAN, iLoop.REMARKS)
                grvOBATRACIKAN.SetFocusedRowCellValue(colKDUOMOBATRACIKAN, iLoop.KDUOM)
                grvOBATRACIKAN.SetFocusedRowCellValue(colPERMINTAANOBATRACIKAN, iLoop.PERMINTAAN)
                grvOBATRACIKAN.SetFocusedRowCellValue(colSIGNAOBATRACIKAN, iLoop.SIGNA)
                grvOBATRACIKAN.UpdateCurrentRow()
            Next
        End If

        'If kosong = False Then
        '    Dim NOREF As String = fn_LoadResepTerakhirGosHeader()
        '    If NOREF <> "" Then
        '        fn_LoadResepTerakhirGos(NOREF)
        '    End If
        'End If
    End Sub
    Private Function fn_LoadResepTerakhirGosHeader() As String
        Try
            fn_LoadResepTerakhirGosHeader = ""

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


            MYSQL = "	SELECT	"
            MYSQL &= "	 p.NORM AS 'NORM',	"
            MYSQL &= "	 k.NOMOR AS 'NOMOR KUNJUNGAN',	"
            MYSQL &= "	 res.NOMOR AS 'NOMOR RESEP',	"
            MYSQL &= "	 res.TANGGAL AS 'TANGGAL RESEP',	"
            MYSQL &= "	 master.getNamaLengkapPegawai(mp.NIP)AS 'NAMA DPJP',	"
            MYSQL &= "	 res.DOKTER_DPJP 'KD_DPJP',	"
            MYSQL &= "	 res.DIAGNOSA AS 'DIAGNOSA',	"
            MYSQL &= "	 odr.FARMASI AS 'KD_OBAT',	"
            MYSQL &= "	 b.NAMA AS 'NAMA OBAT',	"
            MYSQL &= "	 odr.JUMLAH AS 'JUMLAH',	"
            MYSQL &= "	 odr.DOSIS AS 'DOSIS',	"
            MYSQL &= "	 odr.FREKUENSI AS 'KODE ATURAN PAKAI',	"
            MYSQL &= "	 (SELECT far.FREKUENSI FROM master.frekuensi_aturan_resep far WHERE far.id = odr.FREKUENSI) AS 'DESKRIPSI ATURAN PAKAI'	"
            MYSQL &= "	FROM layanan.order_detil_resep odr	"
            MYSQL &= "	LEFT JOIN inventory.barang b ON b.ID = odr.FARMASI	"
            MYSQL &= "	JOIN layanan.order_resep res ON res.NOMOR = odr.ORDER_ID	"
            MYSQL &= "	JOIN pendaftaran.kunjungan k ON res.NOMOR = k.REF	"
            MYSQL &= "	LEFT JOIN pendaftaran.pendaftaran p ON p.NOMOR = k.NOPEN  	"
            MYSQL &= "	LEFT JOIN master.dokter md ON res.DOKTER_DPJP=md.ID	"
            MYSQL &= "	LEFT JOIN master.pegawai mp ON md.NIP=mp.NIP	"
            MYSQL &= "	 where b.STATUS = 1 and p.NORM = '" & txtKDCUSTOMER.Text & "'	"
            MYSQL &= "	ORDER BY res.TANGGAL desc LIMIT 1	"


            oCommMySql.Connection = oConnMySql
            oCommMySql.CommandText = MYSQL
            oCommMySql.CommandTimeout = 120
            oCommMySql.CommandType = CommandType.Text

            daMySql = New MySql.Data.MySqlClient.MySqlDataAdapter(oCommMySql)
            daMySql.Fill(dsMySql, "medicalrecordcppt_masterheader")

            If oConnMySql.State = ConnectionState.Open Then
                oConnMySql.Close()
            End If

            For iLoop As Integer = 0 To dsMySql.Tables("medicalrecordcppt_masterheader").Rows.Count - 1
                With dsMySql.Tables("medicalrecordcppt_masterheader")
                    fn_LoadResepTerakhirGosHeader = .Rows(iLoop)("NOMOR RESEP")
                End With
            Next

        Catch oErr As Exception
            fn_LoadResepTerakhirGosHeader = ""
            MsgBox("Load Master Header kunjungan obat" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub fn_LoadResepTerakhirGos(ByVal noref As String)
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

            MYSQL = "	SELECT	"
            MYSQL &= "	 p.NORM AS 'NORM',	"
            MYSQL &= "	 k.NOMOR AS 'NOMOR KUNJUNGAN',	"
            MYSQL &= "	 res.NOMOR AS 'NOMOR RESEP',	"
            MYSQL &= "	 res.TANGGAL AS 'TANGGAL RESEP',	"
            MYSQL &= "	 master.getNamaLengkapPegawai(mp.NIP)AS 'NAMA DPJP',	"
            MYSQL &= "	 res.DOKTER_DPJP 'KD_DPJP',	"
            MYSQL &= "	 res.DIAGNOSA AS 'DIAGNOSA',	"
            MYSQL &= "	 odr.FARMASI AS 'KD_OBAT',	"
            MYSQL &= "	 b.NAMA AS 'NAMA OBAT',	"
            MYSQL &= "	 odr.JUMLAH AS 'JUMLAH',	"
            MYSQL &= "	 odr.DOSIS AS 'DOSIS',	"
            MYSQL &= "	 odr.FREKUENSI AS 'KODE ATURAN PAKAI',	"
            MYSQL &= "	 (SELECT far.FREKUENSI FROM master.frekuensi_aturan_resep far WHERE far.id = odr.FREKUENSI) AS 'DESKRIPSI ATURAN PAKAI'	"
            MYSQL &= "	FROM layanan.order_detil_resep odr	"
            MYSQL &= "	LEFT JOIN inventory.barang b ON b.ID = odr.FARMASI	"
            MYSQL &= "	JOIN layanan.order_resep res ON res.NOMOR = odr.ORDER_ID	"
            MYSQL &= "	JOIN pendaftaran.kunjungan k ON res.NOMOR = k.REF	"
            MYSQL &= "	LEFT JOIN pendaftaran.pendaftaran p ON p.NOMOR = k.NOPEN  	"
            MYSQL &= "	LEFT JOIN master.dokter md ON res.DOKTER_DPJP=md.ID	"
            MYSQL &= "	LEFT JOIN master.pegawai mp ON md.NIP=mp.NIP	"
            MYSQL &= "	 where b.STATUS = 1 and res.NOMOR = '" & noref & "'	"
            MYSQL &= "	ORDER BY res.TANGGAL	"


            oCommMySql.Connection = oConnMySql
            oCommMySql.CommandText = MYSQL
            oCommMySql.CommandTimeout = 120
            oCommMySql.CommandType = CommandType.Text

            daMySql = New MySql.Data.MySqlClient.MySqlDataAdapter(oCommMySql)
            daMySql.Fill(dsMySql, "medicalrecordcppt_obat")

            If oConnMySql.State = ConnectionState.Open Then
                oConnMySql.Close()
            End If

            Dim oItem As New Reference.clsItem

            For iLoop As Integer = 0 To dsMySql.Tables("medicalrecordcppt_obat").Rows.Count - 1
                With dsMySql.Tables("medicalrecordcppt_obat")
                    grvDetailResep.Focus()
                    grvDetailResep.AddNewRow()

                    grvDetailResep.SetFocusedRowCellValue(colKDITEM, .Rows(iLoop)("KD_OBAT").ToString)
                    grvDetailResep.SetFocusedRowCellValue(colQTY, CDec(.Rows(iLoop)("JUMLAH")))
                    grvDetailResep.SetFocusedRowCellValue(colREMARKS_DOKTER, .Rows(iLoop)("DESKRIPSI ATURAN PAKAI").ToString)
                    'grvDetailResep.SetFocusedRowCellValue(colKDUOM, iLoop.KDUOM)
                    'grvDetailResep.SetFocusedRowCellValue(colKDSIGNA, iLoop.KDSIGNA)
                    'grvDetailResep.SetFocusedRowCellValue(colKDCARAPAKAI, iLoop.KDCARAPAKAI)
                    grvDetailResep.UpdateCurrentRow()

                    'Dim ds = oItem.GetData(.Rows(iLoop)("KD_OBAT"))
                    'If ds IsNot Nothing Then
                    '    Dim dsUOM = oItem.GetDataDetail_UOM(ds.KDITEM)

                    '    grvDetailResep.SetFocusedRowCellValue(colKDITEM, .Rows(iLoop)("KD_OBAT"))
                    '    grvDetailResep.SetFocusedRowCellValue(colQTY, .Rows(iLoop)("JUMLAH"))
                    '    grvDetailResep.SetFocusedRowCellValue(colREMARKS_DOKTER, .Rows(iLoop)("DESKRIPSI ATURAN PAKAI"))

                    '    If dsUOM IsNot Nothing Then
                    '        grvDetailResep.SetFocusedRowCellValue(colKDUOM, dsUOM.FirstOrDefault.KDUOM)
                    '    End If
                    '    grvDetailResep.SetFocusedRowCellValue(colKDSIGNA, oItem.DefaultItem_Signa)
                    '    grvDetailResep.SetFocusedRowCellValue(colKDCARAPAKAI, oItem.DefaultItem_CaraPakai)
                    '    grvDetailResep.UpdateCurrentRow()
                    'End If
                End With
            Next
        Catch oErr As Exception
            MsgBox("Koneksi CPPT Aplikasi Lama" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnRiwayatPemberianResep_Click(sender As Object, e As EventArgs) Handles btnRiwayatPemberianResep.Click
        If txtKDCUSTOMER.Text <> "" Then
            Dim frmRiwayatResep As New frmRiwayatResep
            Try
                frmRiwayatResep.LoadMe(txtKDCUSTOMER.Text, 0)
                frmRiwayatResep.ShowDialog(Me)

                If sNoTransaksi <> String.Empty Then
                    Dim oOrderRanapNonRacikan As New Order.clsOrderRanapNonRacikan
                    For Each iLoop In oOrderRanapNonRacikan.GetDataDetail(sNoTransaksi).OrderBy(Function(x) x.SEQ)
                        grvDetailResep.Focus()
                        grvDetailResep.AddNewRow()

                        grvDetailResep.SetFocusedRowCellValue(colKDITEM, iLoop.KDITEM)
                        'grvDetailResep.SetFocusedRowCellValue(colKDUOM, iLoop.M_UOM.MEMO)
                        'grvDetailResep.SetFocusedRowCellValue(colKDSIGNA, iLoop.M_SIGNA.MEMO)
                        'grvDetailResep.SetFocusedRowCellValue(colKDCARAPAKAI, iLoop.M_CARAPAKAI.MEMO)
                        grvDetailResep.SetFocusedRowCellValue(colQTY, iLoop.JUMLAH)
                        grvDetailResep.SetFocusedRowCellValue(colREMARKS_DOKTER, iLoop.REMARKS_DOKTER)

                        'grvDetailResep.SetFocusedRowCellValue(colKDITEM, iLoop.KDITEM)
                        grvDetailResep.SetFocusedRowCellValue(colKDUOM, iLoop.KDUOM)
                        grvDetailResep.SetFocusedRowCellValue(colKDSIGNA, iLoop.KDSIGNA)
                        grvDetailResep.SetFocusedRowCellValue(colKDCARAPAKAI, iLoop.KDCARAPAKAI)
                        grvDetailResep.SetFocusedRowCellValue(colREMARKS_FARMASI, iLoop.REMARKS_FARMASI)
                        'grvDetailResep.SetFocusedRowCellValue(colQTY_PERUBAHAN, iLoop.QTY)
                        'grvDetailResep.SetFocusedRowCellValue(colREMARKS_FARMASI, iLoop.REMARKS_FARMASI)
                        grvDetailResep.UpdateCurrentRow()
                    Next

                    Dim dsTemplate = oGrouperDataCppt.GetDataDetailNonRacikan(sNoTransaksi)

                    For Each iLoop In dsTemplate.OrderBy(Function(x) x.SEQ)
                        grvDetailResep.Focus()
                        grvDetailResep.AddNewRow()

                        grvDetailResep.SetFocusedRowCellValue(colKDITEM, iLoop.KDITEM)
                        'grvDetailResep.SetFocusedRowCellValue(colKDUOM, iLoop.M_UOM.MEMO)
                        'grvDetailResep.SetFocusedRowCellValue(colKDSIGNA, iLoop.M_SIGNA.MEMO)
                        'grvDetailResep.SetFocusedRowCellValue(colKDCARAPAKAI, iLoop.M_CARAPAKAI.MEMO)
                        grvDetailResep.SetFocusedRowCellValue(colQTY, iLoop.JUMLAH)
                        grvDetailResep.SetFocusedRowCellValue(colREMARKS_DOKTER, iLoop.REMARKS_DOKTER)

                        'grvDetailResep.SetFocusedRowCellValue(colKDITEM, iLoop.KDITEM)
                        grvDetailResep.SetFocusedRowCellValue(colKDUOM, iLoop.KDUOM)
                        grvDetailResep.SetFocusedRowCellValue(colKDSIGNA, iLoop.KDSIGNA)
                        grvDetailResep.SetFocusedRowCellValue(colKDCARAPAKAI, iLoop.KDCARAPAKAI)
                        grvDetailResep.SetFocusedRowCellValue(colREMARKS_FARMASI, iLoop.REMARKS_FARMASI)
                        'grvDetailResep.SetFocusedRowCellValue(colQTY_PERUBAHAN, iLoop.QTY)
                        'grvDetailResep.SetFocusedRowCellValue(colREMARKS_FARMASI, iLoop.REMARKS_FARMASI)
                        grvDetailResep.UpdateCurrentRow()
                    Next

                    For Each iLoop In oGrouperDataCppt.GetDataDetailRacikan(sNoTransaksi).OrderBy(Function(x) x.SEQ)
                        grvDetailResep.Focus()
                        grvDetailResep.AddNewRow()
                        grvOBATRACIKAN.SetFocusedRowCellValue(colKDITEMOBATRACIKAN, iLoop.KDITEM)
                        grvOBATRACIKAN.SetFocusedRowCellValue(colQTYOBATRACIKAN, iLoop.JUMLAH)
                        grvOBATRACIKAN.SetFocusedRowCellValue(colREMARKSRACIKAN, iLoop.REMARKS)
                        grvOBATRACIKAN.SetFocusedRowCellValue(colKDUOMOBATRACIKAN, iLoop.KDUOM)
                        grvOBATRACIKAN.SetFocusedRowCellValue(colPERMINTAANOBATRACIKAN, iLoop.PERMINTAAN)
                        grvOBATRACIKAN.SetFocusedRowCellValue(colSIGNAOBATRACIKAN, iLoop.SIGNA)
                        grvOBATRACIKAN.UpdateCurrentRow()
                    Next
                End If

            Catch ex As Exception
                MsgBox("Load Form Riwayat Resep : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Else
            MsgBox("Nomor Rekam Medis Kosong!!!", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub btnRiwayatPemberianObat_Click(sender As Object, e As EventArgs) Handles btnRiwayatPemberianObat.Click
        If txtKDCUSTOMER.Text <> "" Then
            Dim frmRiwayatResep As New frmRiwayatResep
            Try
                frmRiwayatResep.LoadMe(txtKDCUSTOMER.Text, 1)
                frmRiwayatResep.ShowDialog(Me)

                If sNoTransaksi <> String.Empty Then
                    Dim dsTemplate = oSalesOrderTransaksi.GetDataDetail(sNoTransaksi)

                    For Each iLoop In dsTemplate.OrderBy(Function(x) x.SEQ)
                        grvDetailResep.Focus()
                        grvDetailResep.AddNewRow()

                        grvDetailResep.SetFocusedRowCellValue(colKDITEM, iLoop.KDITEM)
                        'grvDetailResep.SetFocusedRowCellValue(colKDUOM, iLoop.M_UOM.MEMO)
                        'grvDetailResep.SetFocusedRowCellValue(colKDSIGNA, iLoop.M_SIGNA.MEMO)
                        'grvDetailResep.SetFocusedRowCellValue(colKDCARAPAKAI, iLoop.M_CARAPAKAI.MEMO)
                        grvDetailResep.SetFocusedRowCellValue(colQTY, iLoop.QTY)
                        grvDetailResep.SetFocusedRowCellValue(colREMARKS_DOKTER, iLoop.REMARKS)

                        'grvDetailResep.SetFocusedRowCellValue(colKDITEM, iLoop.KDITEM)
                        grvDetailResep.SetFocusedRowCellValue(colKDUOM, iLoop.KDUOM)
                        grvDetailResep.SetFocusedRowCellValue(colKDSIGNA, iLoop.KDSIGNA)
                        grvDetailResep.SetFocusedRowCellValue(colKDCARAPAKAI, iLoop.KDCARAPAKAI)
                        'grvDetailResep.SetFocusedRowCellValue(colQTY_PERUBAHAN, iLoop.QTY)
                        'grvDetailResep.SetFocusedRowCellValue(colREMARKS_FARMASI, iLoop.REMARKS_FARMASI)
                        grvDetailResep.UpdateCurrentRow()
                    Next
                End If
            Catch ex As Exception
                MsgBox("Load Form Riwayat Resep : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Else
            MsgBox("Nomor Rekam Medis Kosong!!!", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub grdCPPT_TemplateTindakan_EditValueChanged_1(sender As Object, e As EventArgs) Handles grdCPPT_TemplateTindakan.EditValueChanged
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
    Private Sub grdTEMPLATE_EditValueChanged(sender As Object, e As EventArgs) Handles grdTEMPLATE.EditValueChanged
        If isLoad = True Then
            If grdTEMPLATE.Text <> "" Then
                Dim oTemplate As New EMedrek.clsTemplateNonRacikan

                Dim dsTemplate = oTemplate.GetDataDetail(grdTEMPLATE.EditValue)

                For Each iLoop In dsTemplate.OrderBy(Function(x) x.SEQ)
                    'txtDIAGNOOSA.Text = iLoop.S_REQ_RECIPE_TEMPLATE_H.DESCRIPTION
                    grvDetailResep.Focus()
                    grvDetailResep.AddNewRow()
                    grvDetailResep.SetFocusedRowCellValue(colKDITEM, iLoop.KDITEM)
                    grvDetailResep.SetFocusedRowCellValue(colKDUOM, iLoop.KDUOM)
                    grvDetailResep.SetFocusedRowCellValue(colKDSIGNA, iLoop.KDSIGNA)
                    grvDetailResep.SetFocusedRowCellValue(colKDCARAPAKAI, iLoop.KDCARAPAKAI)
                    grvDetailResep.SetFocusedRowCellValue(colQTY, iLoop.JUMLAH)
                    grvDetailResep.SetFocusedRowCellValue(colREMARKS_DOKTER, iLoop.REMARKS_DOKTER)
                    grvDetailResep.UpdateCurrentRow()
                Next
            End If
        End If
    End Sub
    Private Sub grdTEMPLATERACIKAN_EditValueChanged(sender As Object, e As EventArgs) Handles grdTEMPLATERACIKAN.EditValueChanged
        If isLoad = True Then
            If grdTEMPLATERACIKAN.Text <> "" Then
                Dim oTemplate As New EMedrek.clsTemplateNonRacikan

                Dim dsTemplate = oTemplate.GetDataDetailRacikan1(grdTEMPLATERACIKAN.EditValue)

                For Each iLoop In dsTemplate.OrderBy(Function(x) x.SEQ)
                    grvOBATRACIKAN.Focus()
                    grvOBATRACIKAN.AddNewRow()
                    grvOBATRACIKAN.SetFocusedRowCellValue(colKDITEMOBATRACIKAN, iLoop.KDITEM)
                    grvOBATRACIKAN.SetFocusedRowCellValue(colKDUOMOBATRACIKAN, iLoop.KDUOM)
                    grvOBATRACIKAN.SetFocusedRowCellValue(colSIGNAOBATRACIKAN, iLoop.SIGNA)
                    grvOBATRACIKAN.SetFocusedRowCellValue(colPERMINTAANOBATRACIKAN, iLoop.PERMINTAAN)
                    grvOBATRACIKAN.SetFocusedRowCellValue(colQTYOBATRACIKAN, iLoop.JUMLAH)
                    grvOBATRACIKAN.SetFocusedRowCellValue(colREMARKSRACIKAN, iLoop.REMARKS)
                    grvOBATRACIKAN.UpdateCurrentRow()
                Next
            End If
        End If
    End Sub
    Private Sub btnSKD_Click(sender As Object, e As EventArgs) Handles btnSKD.Click
        If txtKDKUNJUNGAN.Text = "" Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        'Dim oSKD As New Admission.clsSKD
        'Dim ds = oSKD.GetDataPendaftaranalasan(txtKDKUNJUNGAN.Text, "KONTROL")

        'If ds IsNot Nothing Then
        '    Dim frmSKD As New frmSKD
        '    Try
        '        frmSKD.fn_LoadKategori(0)
        '        frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDSKD)
        '        frmSKD.ShowDialog(Me)
        '    Catch ex As Exception
        '        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    Finally
        '        If Not frmSKD Is Nothing Then frmSKD.Dispose()
        '        frmSKD = Nothing
        '    End Try
        'Else
        '    Dim frmSKD As New frmSKD
        '    Try
        '        frmSKD.fn_LoadNoPendaftaran(txtKDKUNJUNGAN.Text, txtKDCUSTOMER.Text, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtNOMORSEP.Text, txtPenjamin.Text, txtNAMAPASIEN.Text)
        '        frmSKD.fn_LoadKategori(0)
        '        frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
        '        frmSKD.ShowDialog(Me)
        '    Catch ex As Exception
        '        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    End Try
        'End If

        'Dim oSKD As New Admission.clsSKD
        'Dim ds = oSKD.GetDataPendaftaranalasan(txtKDKUNJUNGAN.Text, "KONTROL")

        Dim Kode As String = fn_LoadTindakLanjutAlasanCek(sKDPENDAFTARAN, "KONTROL")
        If Kode <> "" Then
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadNoPendaftaranPolidanDokter(0, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtKDCUSTOMER.Text, sKDPENDAFTARAN, "KONTROL")
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, Kode)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSKD Is Nothing Then frmSKD.Dispose()
                frmSKD = Nothing
            End Try
        Else
            Dim frmSKD As New frmSKD
            Try
                'frmSKD.fn_LoadNoPendaftaran(txtKDKUNJUNGAN.Text, txtKDCUSTOMER.Text, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtNOMORSEP.Text, txtPenjamin.Text, txtNAMAPASIEN.Text, txtDIAGNOOSA.Text)
                'frmSKD.fn_LoadKategori(0)
                frmSKD.fn_LoadNoPendaftaranPolidanDokter(0, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtKDCUSTOMER.Text, sKDPENDAFTARAN, "KONTROL")
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If

        fn_LoadTindakLanjutPertama(sKDPENDAFTARAN)

    End Sub
    Private Function fn_LoadTindakLanjutAlasanCek(ByVal KDREG As String, ByVal ALASAN As String) As String
        fn_LoadTindakLanjutAlasanCek = ""

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
            SQL &= "* "
            SQL &= "FROM S_PENDAFTARAN_SKD WHERE KDPENDAFTARAN = '" & KDREG & "' AND ALASAN = '" & ALASAN & "'"

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_PENDAFTARAN_SKD")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            For iLoop As Integer = 0 To ds.Tables("S_PENDAFTARAN_SKD").Rows.Count - 1
                With ds.Tables("S_PENDAFTARAN_SKD")
                    fn_LoadTindakLanjutAlasanCek = .Rows(iLoop)("KDSKD").ToString()
                    sRemarks_Ruangan = .Rows(iLoop)("REQUEST").ToString()
                    sRemarks_RencanaPembedahan = .Rows(iLoop)("RESPONSE").ToString()
                    sRemarks_IntruksiDokter = .Rows(iLoop)("DESCRIPTION").ToString()
                End With
            Next

        Catch ex As Exception
            MsgBox("Tindak Lanjut : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    'Private Function fn_LoadTindakLanjutAlasan(ByVal KDREG As String, ByVal ALASAN As String) As String
    '    fn_LoadTindakLanjutAlasan = ""

    '    Try
    '        Dim oSkd As New Admission.clsSKD

    '        Dim ds = oSkd.GetDataTindakLanjut(KDREG, ALASAN)
    '        If ds IsNot Nothing Then
    '            fn_LoadTindakLanjutAlasan = ds.KDSKD
    '        End If
    '    Catch ex As Exception
    '        MsgBox("Tindak Lanjut : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Function
    Private Sub fn_LoadTindakLanjutPertama(ByVal KDREG As String)
        Dim oSkd As New Admission.clsSKD
        Dim oRujukan As New Admission.clsRujukan

        Dim Paramater As String = txtTINDAKLANJUT.Text
        txtTINDAKLANJUT.ResetText()

        Dim TindakLanjut As String = String.Empty

        For Each xloop In oSkd.GetDataByKDREG(KDREG)

            Dim dsSKD = oSkd.GetDataNotOnline(xloop.KDSKD)
            If dsSKD IsNot Nothing Then
                If dsSKD.ALASAN = "KONTROL" Then
                    Dim sTindakLanjut As String = "Tanggal Kontrol : " & dsSKD.DATEKONTROL.ToString("dd-MM-yyyy") & vbCrLf & "Alasan : " & dsSKD.ALASAN.ToString.Trim & vbCrLf & "Tindak Lanjut : " & dsSKD.TINDAKLANJUT.ToString.Trim & vbCrLf & "Rencana Pemeriksaan Saat Kontrol Selanjutnya : " & dsSKD.DESCRIPTION.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "RUJUKAN EKSTERNAL" Then
                    Dim sTindakLanjut As String = "Tanggal Kontrol : " & dsSKD.DATEKONTROL.ToString("dd-MM-yyyy") & vbCrLf & "Alasan : " & dsSKD.ALASAN.ToString.Trim & vbCrLf & "Alasan di rujuk : " & dsSKD.DESCRIPTION.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "RUJUKAN HABIS" Then
                    Dim sTindakLanjut As String = "Tanggal Kontrol : " & dsSKD.DATEKONTROL.ToString("dd-MM-yyyy") & vbCrLf & "Alasan : " & dsSKD.ALASAN.ToString.Trim & vbCrLf & "Tindak Lanjut : " & dsSKD.TINDAKLANJUT.ToString.Trim & vbCrLf & "Rencana Pemeriksaan : " & dsSKD.DESCRIPTION.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "KONSUL INTERNAL" Then
                    Dim sTindakLanjut As String = "Tanggal Kontrol : " & dsSKD.DATEKONTROL.ToString("dd-MM-yyyy") & vbCrLf & "Alasan : " & dsSKD.ALASAN.ToString.Trim & vbCrLf & "Tujuan Poli : " & dsSKD.M_DEPARTMENT.NAME_DISPLAY.ToString.Trim & vbCrLf & "Dokter yang di tuju: " & dsSKD.M_DOCTOR.NAME_DISPLAY.ToString.Trim & vbCrLf & "Alasan di konsul : " & dsSKD.DESCRIPTION.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "PRB" Then
                    Dim sTindakLanjut As String = "Tanggal Kontrol : " & dsSKD.DATEKONTROL.ToString("dd-MM-yyyy") & vbCrLf & "Alasan : " & dsSKD.ALASAN.ToString.Trim & vbCrLf & "Alasan di rujuk : " & dsSKD.DESCRIPTION.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "RUJUK BALIK" Then
                    Dim sTindakLanjut As String = "Tanggal Kontrol : " & dsSKD.DATEKONTROL.ToString("dd-MM-yyyy") & vbCrLf & "Alasan : " & dsSKD.ALASAN.ToString.Trim & vbCrLf & "Alasan di rujuk : " & dsSKD.DESCRIPTION.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "SELESAI PENGOBATAN" Then
                    Dim sTindakLanjut As String = "Keterangan : " & dsSKD.ALASAN.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "RAWAT INAP" Then
                    Dim sTindakLanjut As String = "Keterangan : " & dsSKD.ALASAN.ToString.Trim & vbCrLf & sRemarks_IntruksiDokter
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "PENJADWALAN OPERASI" Then
                    Dim sTindakLanjut As String = "Keterangan : " & dsSKD.ALASAN.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "PENUNJANG HARI INI" Then
                    Dim sTindakLanjut As String = "Keterangan : " & dsSKD.ALASAN.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "ALIH RAWAT" Then
                    Dim sTindakLanjut As String = "Tanggal Kontrol : " & dsSKD.DATEKONTROL.ToString("dd-MM-yyyy") & vbCrLf & "Alasan : " & dsSKD.ALASAN.ToString.Trim & vbCrLf & "Tujuan Poli : " & dsSKD.M_DEPARTMENT.NAME_DISPLAY.ToString.Trim & vbCrLf & "Dokter yang di tuju: " & dsSKD.M_DOCTOR.NAME_DISPLAY.ToString.Trim & vbCrLf & "Alasan di rujuk : " & dsSKD.DESCRIPTION.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "ITERASI1" Then
                    Dim sTindakLanjut As String = "Tanggal Iterasi Ke 1 : " & dsSKD.DATEKONTROL.ToString("dd-MM-yyyy") & vbCrLf & "Alasan : " & dsSKD.ALASAN.ToString.Trim & vbCrLf & "Asal Poli : " & dsSKD.M_DEPARTMENT.NAME_DISPLAY.ToString.Trim & vbCrLf & "Dokter Pemberi Iterasi: " & dsSKD.M_DOCTOR.NAME_DISPLAY.ToString.Trim & vbCrLf & "Alasan di Iterasi : " & dsSKD.DESCRIPTION.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "ITERASI2" Then
                    Dim sTindakLanjut As String = "Tanggal Iterasi Ke 1 : " & dsSKD.DATEKONTROL.ToString("dd-MM-yyyy") & vbCrLf & "Tanggal Iterasi Ke 2 : " & dsSKD.DATE.AddDays(30).ToString("dd-MM-yyyy") & vbCrLf & "Alasan : " & dsSKD.ALASAN.ToString.Trim & vbCrLf & "Asal Poli : " & dsSKD.M_DEPARTMENT.NAME_DISPLAY.ToString.Trim & vbCrLf & "Dokter Pemberi Iterasi: " & dsSKD.M_DOCTOR.NAME_DISPLAY.ToString.Trim & vbCrLf & "Alasan di Iterasi : " & dsSKD.DESCRIPTION.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
            End If

            TindakLanjut = txtTINDAKLANJUT.Text
        Next

        Dim dsRujukan = oRujukan.GetDataByNoRegister(sKDPENDAFTARAN)
        If dsRujukan IsNot Nothing Then
            TindakLanjut = (TindakLanjut & vbCrLf & vbCrLf & IIf(dsRujukan.TIPERUJUKAN.ToString.Trim = 0, "Rujukan Eksternal Tanggal Rencana Kunjungan : ", IIf(dsRujukan.TIPERUJUKAN.ToString.Trim = 1, "Rujukan Eksternal Tanggal Rencana Kunjungan : ", "Rujuk Balik Tanggal Rencana Kunjungan : ")) & dsRujukan.DATERENCANAKONTROL.ToString("dd-MM-yyyy") & vbCrLf & "Tipe Rujukan : " & IIf(dsRujukan.TIPERUJUKAN.ToString.Trim = 0, "Penuh", IIf(dsRujukan.TIPERUJUKAN.ToString.Trim = 1, "Partial", "Rujuk Balik")) & vbCrLf & "Faskes " & dsRujukan.M_PPK.MEMO & vbCrLf & "Ke Poli " & dsRujukan.POLI_NAME_DISPLAY & vbCrLf & "Catatan di rujuk : " & dsRujukan.CATATAN.ToString.Trim).ToString.Trim
        End If

        Dim oSET_BOOKING_JADWALOPERASI As New WebService.clsSET_BOOKING_JADWALOPERASI
        Dim dsOperasi = oSET_BOOKING_JADWALOPERASI.GetDataByKD(txtKDKUNJUNGAN.Text)
        If dsOperasi IsNot Nothing Then
            TindakLanjut = (TindakLanjut & vbCrLf & vbCrLf & "Jadwal Operasi Tanggal " & dsOperasi.TANGGALOPERASI.ToString("dd-MM-yyyy") & vbCrLf & "Jenis Operasi " & dsOperasi.JENISOPERASI & vbCrLf & "Jenis Tindakan " & dsOperasi.JENISTINDAKAN & vbCrLf & "Catatan " & dsOperasi.REMARKS).ToString.Trim
        End If

        Dim dsKoding = oGrouperDataCppt.GetDataByKdKunjunganCPPTDokter(txtKDKUNJUNGAN.Text)
        If dsKoding Is Nothing Then
            If Paramater = "" Then
                txtTINDAKLANJUT.Text = TindakLanjut
            Else
                txtTINDAKLANJUT.Text = Paramater & vbCrLf & TindakLanjut
            End If
        Else
            txtTINDAKLANJUT.Text = TindakLanjut
        End If
    End Sub
    Private Sub btnEksternal_Click(sender As Object, e As EventArgs) Handles btnEksternal.Click
        If txtKDKUNJUNGAN.Text = "" Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim oRujukan As New Admission.clsRujukan

        Dim ds = oRujukan.GetDataByNoRegister(sKDPENDAFTARAN)

        If ds IsNot Nothing Then
            Dim frmRujukan As New frmRujukan
            Try
                frmRujukan.LoadMeRegister(sKDPENDAFTARAN, 0)
                frmRujukan.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDRUJUKAN)
                frmRujukan.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmRujukan Is Nothing Then frmRujukan.Dispose()
                frmRujukan = Nothing
            End Try
        Else
            Dim frmRujukan As New frmRujukan
            Try
                frmRujukan.LoadMeRegister(sKDPENDAFTARAN, 0)
                frmRujukan.LoadMe(FORM_MODE.FORM_MODE_ADD)
                frmRujukan.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmRujukan Is Nothing Then frmRujukan.Dispose()
                frmRujukan = Nothing
            End Try
        End If

        fn_LoadTindakLanjutPertama(sKDPENDAFTARAN)

    End Sub
    Private Sub btnInternal_Click(sender As Object, e As EventArgs) Handles btnInternal.Click
        If txtKDKUNJUNGAN.Text = "" Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim Kode As String = fn_LoadTindakLanjutAlasanCek(sKDPENDAFTARAN, "KONSUL INTERNAL")
        If Kode <> "" Then
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadNoPendaftaranPolidanDokter(0, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtKDCUSTOMER.Text, sKDPENDAFTARAN, "KONSUL INTERNAL")
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, Kode)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSKD Is Nothing Then frmSKD.Dispose()
                frmSKD = Nothing
            End Try
        Else
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadNoPendaftaranPolidanDokter(0, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtKDCUSTOMER.Text, sKDPENDAFTARAN, "KONSUL INTERNAL")
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If

        fn_LoadTindakLanjutPertama(sKDPENDAFTARAN)

    End Sub
    Private Sub btnAlihRawat_Click(sender As Object, e As EventArgs) Handles btnAlihRawat.Click
        If txtKDKUNJUNGAN.Text = "" Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim Kode As String = fn_LoadTindakLanjutAlasanCek(sKDPENDAFTARAN, "ALIH RAWAT")
        If Kode <> "" Then
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadNoPendaftaranPolidanDokter(0, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtKDCUSTOMER.Text, sKDPENDAFTARAN, "ALIH RAWAT")
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, Kode)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSKD Is Nothing Then frmSKD.Dispose()
                frmSKD = Nothing
            End Try
        Else
            Dim frmSKD As New frmSKD
            Try
                'frmSKD.fn_LoadNoPendaftaran(txtKDKUNJUNGAN.Text, txtKDCUSTOMER.Text, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtNOMORSEP.Text, txtPenjamin.Text, txtNAMAPASIEN.Text, txtDIAGNOOSA.Text)
                'frmSKD.fn_LoadKategori(8)
                frmSKD.fn_LoadNoPendaftaranPolidanDokter(0, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtKDCUSTOMER.Text, sKDPENDAFTARAN, "ALIH RAWAT")
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If

        fn_LoadTindakLanjutPertama(sKDPENDAFTARAN)
    End Sub
    Private Sub btnRujukanHabis_Click(sender As Object, e As EventArgs) Handles btnRujukanHabis.Click
        If txtKDKUNJUNGAN.Text = "" Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        'Dim oSKD As New Admission.clsSKD
        'Dim ds = oSKD.GetDataPendaftaranalasan(txtKDKUNJUNGAN.Text, "RUJUKAN HABIS")

        'If ds IsNot Nothing Then
        '    Dim frmSKD As New frmSKD
        '    Try
        '        frmSKD.fn_LoadKategori(2)
        '        frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDSKD)
        '        frmSKD.ShowDialog(Me)
        '    Catch ex As Exception
        '        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    Finally
        '        If Not frmSKD Is Nothing Then frmSKD.Dispose()
        '        frmSKD = Nothing
        '    End Try
        'Else
        '    Dim frmSKD As New frmSKD
        '    Try
        '        frmSKD.fn_LoadNoPendaftaran(txtKDKUNJUNGAN.Text, txtKDCUSTOMER.Text, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtNOMORSEP.Text, txtPenjamin.Text, txtNAMAPASIEN.Text)
        '        frmSKD.fn_LoadKategori(2)
        '        frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
        '        frmSKD.ShowDialog(Me)
        '    Catch ex As Exception
        '        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    End Try
        'End If

        Dim Kode As String = fn_LoadTindakLanjutAlasanCek(sKDPENDAFTARAN, "RUJUKAN HABIS")
        If Kode <> "" Then
            Dim frmSKD As New frmSKD
            Try
                'frmSKD.fn_LoadNoPendaftaran(txtKDKUNJUNGAN.Text, txtKDCUSTOMER.Text, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtNOMORSEP.Text, txtPenjamin.Text, txtNAMAPASIEN.Text, txtDIAGNOOSA.Text)
                frmSKD.fn_LoadNoPendaftaranPolidanDokter(0, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtKDCUSTOMER.Text, sKDPENDAFTARAN, "RUJUKAN HABIS")
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, Kode)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSKD Is Nothing Then frmSKD.Dispose()
                frmSKD = Nothing
            End Try
        Else
            Dim frmSKD As New frmSKD
            Try
                'frmSKD.fn_LoadNoPendaftaran(txtKDKUNJUNGAN.Text, txtKDCUSTOMER.Text, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtNOMORSEP.Text, txtPenjamin.Text, txtNAMAPASIEN.Text, txtDIAGNOOSA.Text)
                'frmSKD.fn_LoadKategori(2)
                frmSKD.fn_LoadNoPendaftaranPolidanDokter(0, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtKDCUSTOMER.Text, sKDPENDAFTARAN, "RUJUKAN HABIS")
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If

        fn_LoadTindakLanjutPertama(sKDPENDAFTARAN)

    End Sub
    Private Sub btnPRB_Click(sender As Object, e As EventArgs) Handles btnPRB.Click
        If txtKDKUNJUNGAN.Text = "" Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        'Dim oSKD As New Admission.clsSKD
        'Dim ds = oSKD.GetDataPendaftaranalasan(txtKDKUNJUNGAN.Text, "PRB")

        'If ds IsNot Nothing Then
        '    Dim frmSKD As New frmSKD
        '    Try
        '        frmSKD.fn_LoadKategori(4)
        '        frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDSKD)
        '        frmSKD.ShowDialog(Me)
        '    Catch ex As Exception
        '        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    Finally
        '        If Not frmSKD Is Nothing Then frmSKD.Dispose()
        '        frmSKD = Nothing
        '    End Try
        'Else
        '    Dim frmSKD As New frmSKD
        '    Try
        '        frmSKD.fn_LoadNoPendaftaran(txtKDKUNJUNGAN.Text, txtKDCUSTOMER.Text, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtNOMORSEP.Text, txtPenjamin.Text, txtNAMAPASIEN.Text)
        '        frmSKD.fn_LoadKategori(4)
        '        frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
        '        frmSKD.ShowDialog(Me)
        '    Catch ex As Exception
        '        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    End Try
        'End If

        Dim Kode As String = fn_LoadTindakLanjutAlasanCek(sKDPENDAFTARAN, "PRB")
        If Kode <> "" Then
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadNoPendaftaranPolidanDokter(0, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtKDCUSTOMER.Text, sKDPENDAFTARAN, "PRB")
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, Kode)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSKD Is Nothing Then frmSKD.Dispose()
                frmSKD = Nothing
            End Try
        Else
            Dim frmSKD As New frmSKD
            Try
                'frmSKD.fn_LoadNoPendaftaran(txtKDKUNJUNGAN.Text, txtKDCUSTOMER.Text, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtNOMORSEP.Text, txtPenjamin.Text, txtNAMAPASIEN.Text, txtDIAGNOOSA.Text)
                'frmSKD.fn_LoadKategori(4)
                frmSKD.fn_LoadNoPendaftaranPolidanDokter(0, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtKDCUSTOMER.Text, sKDPENDAFTARAN, "PRB")
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If

        fn_LoadTindakLanjutPertama(sKDPENDAFTARAN)
    End Sub
    Private Sub btnITERASI_Click(sender As Object, e As EventArgs) Handles btnITERASI.Click
        MsgBox("Iterasi 1 Belum Tersedia", MsgBoxStyle.Exclamation, Me.Text)

        'If txtKDKUNJUNGAN.Text = "" Then
        '    MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
        '    Exit Sub
        'End If

        'Dim Kode As String = fn_LoadTindakLanjutAlasanCek(txtKDKUNJUNGAN.Text, "ITERASI1")
        'If Kode <> "" Then
        '    Dim frmSKD As New frmSKD
        '    Try
        '        'frmSKD.fn_LoadKategori(9)
        '        frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, Kode)
        '        frmSKD.ShowDialog(Me)
        '    Catch ex As Exception
        '        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    Finally
        '        If Not frmSKD Is Nothing Then frmSKD.Dispose()
        '        frmSKD = Nothing
        '    End Try
        'Else
        '    Dim frmSKD As New frmSKD
        '    Try
        '        'frmSKD.fn_LoadNoPendaftaran(txtKDKUNJUNGAN.Text, txtKDCUSTOMER.Text, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtNOMORSEP.Text, txtPenjamin.Text, txtNAMAPASIEN.Text, txtDIAGNOOSA.Text)
        '        'frmSKD.fn_LoadKategori(9)
        '        frmSKD.fn_LoadNoPendaftaranPolidanDokter(0, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtKDCUSTOMER.Text, sKDPENDAFTARAN, "ITERASI")
        '        frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
        '        frmSKD.ShowDialog(Me)
        '    Catch ex As Exception
        '        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    End Try
        'End If

        'fn_LoadTindakLanjut(sKDPENDAFTARAN)

    End Sub
    Private Sub SimpleButton6_Click(sender As Object, e As EventArgs) Handles SimpleButton6.Click
        MsgBox("Iterasi 2 Belum Tersedia", MsgBoxStyle.Exclamation, Me.Text)

        'If txtKDKUNJUNGAN.Text = "" Then
        '    MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
        '    Exit Sub
        'End If

        'Dim Kode As String = fn_LoadTindakLanjutAlasanCek(txtKDKUNJUNGAN.Text, "ITERASI2")
        'If Kode <> "" Then
        '    Dim frmSKD As New frmSKD
        '    Try
        '        'frmSKD.fn_LoadKategori(9)
        '        frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, Kode)
        '        frmSKD.ShowDialog(Me)
        '    Catch ex As Exception
        '        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    Finally
        '        If Not frmSKD Is Nothing Then frmSKD.Dispose()
        '        frmSKD = Nothing
        '    End Try
        'Else
        '    Dim frmSKD As New frmSKD
        '    Try
        '        'frmSKD.fn_LoadNoPendaftaran(txtKDKUNJUNGAN.Text, txtKDCUSTOMER.Text, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtNOMORSEP.Text, txtPenjamin.Text, txtNAMAPASIEN.Text, txtDIAGNOOSA.Text)
        '        'frmSKD.fn_LoadKategori(10)
        '        frmSKD.fn_LoadNoPendaftaranPolidanDokter(0, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtKDCUSTOMER.Text, sKDPENDAFTARAN, "ITERASI2")
        '        frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
        '        frmSKD.ShowDialog(Me)
        '    Catch ex As Exception
        '        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    End Try
        'End If

        'fn_LoadTindakLanjut(sKDPENDAFTARAN)

    End Sub
    Private Sub btnRujukBalik_Click(sender As Object, e As EventArgs) Handles btnRujukBalik.Click
        If txtKDKUNJUNGAN.Text = "" Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim oRujukan As New Admission.clsRujukan

        Dim ds = oRujukan.GetDataByNoRegister(sKDPENDAFTARAN)

        If ds IsNot Nothing Then
            Dim frmRujukan As New frmRujukan
            Try
                frmRujukan.LoadMeRegister(sKDPENDAFTARAN, 2)
                frmRujukan.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDRUJUKAN)
                frmRujukan.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmRujukan Is Nothing Then frmRujukan.Dispose()
                frmRujukan = Nothing
            End Try
        Else
            Dim frmRujukan As New frmRujukan
            Try
                frmRujukan.LoadMeRegister(sKDPENDAFTARAN, 2)
                frmRujukan.LoadMe(FORM_MODE.FORM_MODE_ADD)
                frmRujukan.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmRujukan Is Nothing Then frmRujukan.Dispose()
                frmRujukan = Nothing
            End Try
        End If

        fn_LoadTindakLanjutPertama(sKDPENDAFTARAN)

        ''Dim oSKD As New Admission.clsSKD
        ''Dim ds = oSKD.GetDataPendaftaranalasan(txtKDKUNJUNGAN.Text, "RUJUK BALIK")

        ''If ds IsNot Nothing Then
        ''    Dim frmSKD As New frmSKD
        ''    Try
        ''        frmSKD.fn_LoadKategori(5)
        ''        frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDSKD)
        ''        frmSKD.ShowDialog(Me)
        ''    Catch ex As Exception
        ''        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        ''    Finally
        ''        If Not frmSKD Is Nothing Then frmSKD.Dispose()
        ''        frmSKD = Nothing
        ''    End Try
        ''Else
        ''    Dim frmSKD As New frmSKD
        ''    Try
        ''        frmSKD.fn_LoadNoPendaftaran(txtKDKUNJUNGAN.Text, txtKDCUSTOMER.Text, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtNOMORSEP.Text, txtPenjamin.Text, txtNAMAPASIEN.Text)
        ''        frmSKD.fn_LoadKategori(5)
        ''        frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
        ''        frmSKD.ShowDialog(Me)
        ''    Catch ex As Exception
        ''        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        ''    End Try
        ''End If

        'Dim Kode As String = fn_LoadTindakLanjutAlasanCek(sKDPENDAFTARAN, "RUJUK BALIK")
        'If Kode <> "" Then
        '    Dim frmSKD As New frmSKD
        '    Try
        '        frmSKD.fn_LoadNoPendaftaranPolidanDokter(0, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtKDCUSTOMER.Text, sKDPENDAFTARAN, "RUJUK BALIK")
        '        frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, Kode)
        '        frmSKD.ShowDialog(Me)
        '    Catch ex As Exception
        '        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    Finally
        '        If Not frmSKD Is Nothing Then frmSKD.Dispose()
        '        frmSKD = Nothing
        '    End Try
        'Else
        '    Dim frmSKD As New frmSKD
        '    Try
        '        'frmSKD.fn_LoadNoPendaftaran(txtKDKUNJUNGAN.Text, txtKDCUSTOMER.Text, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtNOMORSEP.Text, txtPenjamin.Text, txtNAMAPASIEN.Text, txtDIAGNOOSA.Text)
        '        'frmSKD.fn_LoadKategori(5)
        '        frmSKD.fn_LoadNoPendaftaranPolidanDokter(0, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtKDCUSTOMER.Text, sKDPENDAFTARAN, "RUJUK BALIK")
        '        frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
        '        frmSKD.ShowDialog(Me)
        '    Catch ex As Exception
        '        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    End Try
        'End If

        'fn_LoadTindakLanjutPertama(sKDPENDAFTARAN)


    End Sub
    Private Sub btnRawatInap_Click(sender As Object, e As EventArgs) Handles btnRawatInap.Click
        Dim Kode As String = fn_LoadTindakLanjutAlasanCek(sKDPENDAFTARAN, "RAWAT INAP")
        If Kode <> "" Then
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadNoPendaftaranPolidanDokter(1, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtKDCUSTOMER.Text, sKDPENDAFTARAN, "RAWAT INAP")
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, Kode)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSKD Is Nothing Then frmSKD.Dispose()
                frmSKD = Nothing
            End Try
        Else
            Dim frmSKD As New frmSKD
            Try
                'frmSKD.fn_LoadNoPendaftaran(txtKDKUNJUNGAN.Text, txtKDCUSTOMER.Text, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtNOMORSEP.Text, txtPenjamin.Text, txtNAMAPASIEN.Text, txtDIAGNOOSA.Text)
                'frmSKD.fn_LoadKategori(0)
                frmSKD.fn_LoadNoPendaftaranPolidanDokter(1, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtKDCUSTOMER.Text, sKDPENDAFTARAN, "RAWAT INAP")
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If

        fn_LoadTindakLanjutPertama(sKDPENDAFTARAN)

        'MsgBox("Rawat Inap Masih Belum Tersedia", MsgBoxStyle.Exclamation, Me.Text)

        'If txtKDKUNJUNGAN.Text = "" Then
        '    MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
        '    Exit Sub
        'End If

        'If MsgBox("Apakah Rawat Inap?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        ''Dim oSKD As New Admission.clsSKD
        ''Dim ds = oSKD.GetDataPendaftaranalasan(txtKDKUNJUNGAN.Text, "RAWAT INAP")

        ''If ds IsNot Nothing Then
        ''    fn_SaveSKD(False, ds.KDSKD, "RAWAT INAP")
        ''Else
        ''    fn_SaveSKD(True, "", "RAWAT INAP")
        ''End If

        'sRemarks_Ruangan = ""
        'sRemarks_RencanaPembedahan = ""
        'sRemarks_IntruksiDokter = ""

        'Dim Kode As String = fn_LoadTindakLanjutAlasanCek(sKDPENDAFTARAN, "RAWAT INAP")

        'Dim frmRemarksRawatInap As New frmRemarksRawatInap
        'Try
        '    frmRemarksRawatInap.ShowDialog(Me)
        'Catch ex As Exception
        '    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try

        'If Kode <> "" Then
        '    fn_SaveSKD(False, Kode, "RAWAT INAP", 1)
        'Else
        '    fn_SaveSKD(True, "", "RAWAT INAP", 1)
        'End If

        'fn_LoadTindakLanjutPertama(sKDPENDAFTARAN)

        'sRemarks = ""
        'sRemarks_Ruangan = ""
        'sRemarks_RencanaPembedahan = ""
        'sRemarks_IntruksiDokter = ""
    End Sub
    Private Function fn_SaveSKD(ByVal isAdd As Boolean, ByVal NOIID As String, ByVal ALASAN As String, ByVal sISCATEGORY As Integer) As Boolean
        Try
            ' ***** HEADER *****
            Dim oSKD As New Admission.clsSKD

            Dim dsDaftar = oSKD.GetDataPendaftaranByKoderegistrasi(sKDPENDAFTARAN)

            Dim ds = oSKD.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oSKD.GetData(NOIID).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try

                .DATEUPDATED = Now
                .KDSKD = NOIID
                .KDPENDAFTARAN = sKDPENDAFTARAN
                .DATE = deDATE.DateTime
                .ISCATEGORY = sISCATEGORY
                .KDDEPARTMENT = grdKDDEPARTMENT.EditValue
                .KDDOCTOR = grdDPJP.EditValue
                .NOMORRUJUKAN = dsDaftar.NOMORRUJUKAN
                .DESCRIPTION = ""
                Try
                    .ISCHEKED = oSKD.GetData(NOIID).ISCHEKED
                Catch oErr As Exception
                    .ISCHEKED = False
                End Try
                .KDUSER = sUserID
                .DATEKONTROL = deDATE.DateTime
                .ALASAN = ALASAN
                .TINDAKLANJUT = ALASAN
                .TANGGALPERIKSA_TEXT = deDATE.DateTime.ToString("ddMMyyyy")
                .KDJADWALDOKTER = ""
                .SEQ = 0
                .REQUEST = ""
                .RESPONSE = ""
                .NOMORSEP = dsDaftar.NOMORSEP
                .ISONLINE = False
                .ISSKD = IIf(sISCATEGORY = 0, False, True)
            End With

            If isAdd = True Then
                Dim KDSKD As String = oSKD.InsertData(ds, NOIID)

                If KDSKD <> "" Then
                    fn_SaveSKD = True
                Else
                    fn_SaveSKD = False
                End If
            Else
                fn_SaveSKD = oSKD.UpdateData(ds)
            End If
        Catch oErr As Exception
            MsgBox("Save SKD" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveSKD = False
        End Try
    End Function
    Private Sub btnPenjadwalanOperasi_Click(sender As Object, e As EventArgs) Handles btnPenjadwalanOperasi.Click
        If txtKDKUNJUNGAN.Text = "" Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        'If MsgBox("Apakah Penjadwalan Operasi?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        'Dim oSKD As New Admission.clsSKD
        'Dim ds = oSKD.GetDataTindakLanjut(txtKDKUNJUNGAN.Text, "PENJADWALAN OPERASI")

        'If ds IsNot Nothing Then
        '    fn_SaveSKD(False, ds.KDSKD, "PENJADWALAN OPERASI", 0)
        'Else
        '    fn_SaveSKD(True, "", "PENJADWALAN OPERASI", 0)
        'End If

        Dim oSET_BOOKING_JADWALOPERASI As New WebService.clsSET_BOOKING_JADWALOPERASI

        Dim ds = oSET_BOOKING_JADWALOPERASI.GetDataByKD(txtKDKUNJUNGAN.Text)

        If ds Is Nothing Then
            Dim frmSET_BOOKING_JADWALOPERASI As New frmSET_BOOKING_JADWALOPERASI
            Try
                frmSET_BOOKING_JADWALOPERASI.LoadMe(FORM_MODE.FORM_MODE_ADD, txtKDKUNJUNGAN.Text)
                frmSET_BOOKING_JADWALOPERASI.ShowDialog(Me)
            Catch oErr As Exception
                MsgBox("Jadwal Operasi" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Else
            Dim frmSET_BOOKING_JADWALOPERASI As New frmSET_BOOKING_JADWALOPERASI
            Try
                frmSET_BOOKING_JADWALOPERASI.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDKUNJUNGAN, ds.KDBOOKINGJADWALOPERASI)
                frmSET_BOOKING_JADWALOPERASI.ShowDialog(Me)
            Catch oErr As Exception
                MsgBox("Jadwal Operasi" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If

        fn_LoadTindakLanjutPertama(sKDPENDAFTARAN)

    End Sub
    Private Sub btnSelesaiPengobatan_Click(sender As Object, e As EventArgs) Handles btnSelesaiPengobatan.Click
        If txtKDKUNJUNGAN.Text Is Nothing Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If MsgBox("Apakah Selesai Pengobatan?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        Dim Kode As String = fn_LoadTindakLanjutAlasanCek(sKDPENDAFTARAN, "SELESAI PENGOBATAN")

        If Kode <> "" Then
            fn_SaveSKD(False, Kode, "SELESAI PENGOBATAN", 0)
        Else
            fn_SaveSKD(True, "", "SELESAI PENGOBATAN", 0)
        End If

        fn_LoadTindakLanjutPertama(sKDPENDAFTARAN)
    End Sub
    Private Sub btnKembaliKeDokter_Click(sender As Object, e As EventArgs) Handles btnKembaliKeDokter.Click
        If txtKDKUNJUNGAN.Text Is Nothing Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If MsgBox("Apakah Akan dilakukan Penunjang hari Ini?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        'Dim oSKD As New Admission.clsSKD
        'Dim ds = oSKD.GetDataTindakLanjut(txtKDKUNJUNGAN.Text, "PENUNJANG HARI INI")

        Dim Kode As String = fn_LoadTindakLanjutAlasanCek(sKDPENDAFTARAN, "PENUNJANG HARI INI")

        If Kode <> "" Then
            fn_SaveSKD(False, Kode, "PENUNJANG HARI INI", 0)
        Else
            fn_SaveSKD(True, "", "PENUNJANG HARI INI", 0)
        End If

        'txtTINDAKLANJUT.Text = "Penunjang Hari Ini, Kembali Ke Dokter"

        fn_LoadTindakLanjutPertama(sKDPENDAFTARAN)
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        grvDetailResep.DeleteSelectedRows()
    End Sub
    Private Sub DeleteAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteAllToolStripMenuItem.Click
        grvDetailResep.OptionsSelection.MultiSelect = True
        grvDetailResep.SelectAll()
        grvDetailResep.DeleteSelectedRows()
        grvDetailResep.OptionsSelection.MultiSelect = False
    End Sub
    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        grvDiagnosaPenyerta.DeleteSelectedRows()
    End Sub
    Private Sub DeleteAllToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles DeleteAllToolStripMenuItem2.Click
        grvDiagnosaPenyerta.OptionsSelection.MultiSelect = True
        grvDiagnosaPenyerta.SelectAll()
        grvDiagnosaPenyerta.DeleteSelectedRows()
        grvDiagnosaPenyerta.OptionsSelection.MultiSelect = False
    End Sub
    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        'grvTindakan.DeleteSelectedRows()
    End Sub
    Private Sub DeleteAllToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DeleteAllToolStripMenuItem1.Click
        'grvTindakan.OptionsSelection.MultiSelect = True
        'grvTindakan.SelectAll()
        'grvTindakan.DeleteSelectedRows()
        'grvTindakan.OptionsSelection.MultiSelect = False
    End Sub
    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        grvCPPT_Tindakan.DeleteSelectedRows()
    End Sub
    Private Sub ToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem4.Click
        grvCPPT_Tindakan.OptionsSelection.MultiSelect = True
        grvCPPT_Tindakan.SelectAll()
        grvCPPT_Tindakan.DeleteSelectedRows()
        grvCPPT_Tindakan.OptionsSelection.MultiSelect = False
    End Sub
    Private Sub ToolStripMenuItem5_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem5.Click
        grvOBATRACIKAN.DeleteSelectedRows()
    End Sub
    Private Sub ToolStripMenuItem6_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem6.Click
        grvOBATRACIKAN.OptionsSelection.MultiSelect = True
        grvOBATRACIKAN.SelectAll()
        grvOBATRACIKAN.DeleteSelectedRows()
        grvOBATRACIKAN.OptionsSelection.MultiSelect = False
    End Sub
    Private Sub PasteToolStripMenuItem_Click(sender As Object, e As EventArgs)
        For Each iLoop In listCopy
            grvDetailResep.Focus()
            grvDetailResep.AddNewRow()

            grvDetailResep.SetFocusedRowCellValue(colKDITEM, iLoop.KDITEM)
            grvDetailResep.SetFocusedRowCellValue(colKDUOM, iLoop.KDUOM)
            grvDetailResep.SetFocusedRowCellValue(colKDSIGNA, iLoop.KDSIGNA)
            grvDetailResep.SetFocusedRowCellValue(colKDCARAPAKAI, iLoop.KDCARAPAKAI)
            grvDetailResep.SetFocusedRowCellValue(colQTY, iLoop.JUMLAH)
            grvDetailResep.SetFocusedRowCellValue(colREMARKS_DOKTER, iLoop.REMARKS_DOKTER)
            grvDetailResep.UpdateCurrentRow()
        Next
    End Sub
    Private Sub btnSalinDiagnosaByUSER_Click(sender As Object, e As EventArgs) Handles btnSalinDiagnosaByUSER.Click
        Dim dsTerakhirUser = oGrouperDataCppt.GetDataByRmTerakhirByUserAndDokter(txtKDCUSTOMER.Text, sUserID)
        If dsTerakhirUser IsNot Nothing Then
            grvDiagnosaPenyerta.OptionsSelection.MultiSelect = True
            grvDiagnosaPenyerta.SelectAll()
            grvDiagnosaPenyerta.DeleteSelectedRows()
            grvDiagnosaPenyerta.OptionsSelection.MultiSelect = False

            For Each xloop In oGrouperDataCppt.GetDataDetailDiagnosa(dsTerakhirUser.KDCPPT)
                grvDiagnosaPenyerta.AddNewRow()
                grvDiagnosaPenyerta.SetFocusedRowCellValue(colKETERANGAN, xloop.MEMO)
                grvDiagnosaPenyerta.SetFocusedRowCellValue(colKDDIAGNOSA, xloop.KDDIAGNOSA)
                grvDiagnosaPenyerta.SetFocusedRowCellValue(colKATEGORI, xloop.KATEGORI)
                grvDiagnosaPenyerta.UpdateCurrentRow()
            Next
        Else
            MsgBox("Data Tidak ditemukan untuk user " & sUserID & " No Rm " & txtKDCUSTOMER.Text, MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub btnDiagnosaTerakhir_Click(sender As Object, e As EventArgs) Handles btnDiagnosaTerakhir.Click
        Dim oDaftar As New Admission.clsPendaftaran
        Dim dsDaftar = oDaftar.GetData(sKDPENDAFTARAN)
        If dsDaftar IsNot Nothing Then
            grvDiagnosaPenyerta.OptionsSelection.MultiSelect = True
            grvDiagnosaPenyerta.SelectAll()
            grvDiagnosaPenyerta.DeleteSelectedRows()
            grvDiagnosaPenyerta.OptionsSelection.MultiSelect = False

            grvDiagnosaPenyerta.AddNewRow()
            grvDiagnosaPenyerta.SetFocusedRowCellValue(colKETERANGAN, dsDaftar.M_DIAGNOSA.MEMO.Replace(dsDaftar.KDDIAGNOSA, "").Replace("-", "").ToString.Trim)
            grvDiagnosaPenyerta.SetFocusedRowCellValue(colKDDIAGNOSA, dsDaftar.KDDIAGNOSA)
            grvDiagnosaPenyerta.UpdateCurrentRow()
        End If
    End Sub
    Private Sub GERDQToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GERDQToolStripMenuItem.Click
        'If txtKDKUNJUNGAN.Text = "" Then
        '    MsgBox("Silhakan Pilih Pasien", MsgBoxStyle.Information, Me.Text)
        '    fn_EmptyMe()
        '    Exit Sub
        'End If

        'Dim oGerd As New Inventory.clsDigital_IGD_01_GERD
        'Dim dsGerd = oGerd.GetDataByKdreg(txtKDKUNJUNGAN.Text)
        'If dsGerd Is Nothing Then
        '    Dim frmDigital_IGD_01_GERD As New frmDigital_IGD_01_GERD
        '    Try
        '        frmDigital_IGD_01_GERD.LoadMe(FORM_MODE.FORM_MODE_ADD, txtKDKUNJUNGAN.Text, txtNAMAPASIEN.Text, txtKDCUSTOMER.Text)
        '        frmDigital_IGD_01_GERD.ShowDialog(Me)
        '        fn_LoadSecurity()
        '    Catch ex As Exception
        '        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    Finally
        '        If Not frmDigital_IGD_01_GERD Is Nothing Then frmDigital_IGD_01_GERD.Dispose()
        '        frmDigital_IGD_01_GERD = Nothing
        '    End Try
        'Else
        '    Dim frmDigital_IGD_01_GERD As New frmDigital_IGD_01_GERD
        '    Try
        '        frmDigital_IGD_01_GERD.LoadMe(FORM_MODE.FORM_MODE_EDIT, txtKDKUNJUNGAN.Text, txtNAMAPASIEN.Text, txtKDCUSTOMER.Text, dsGerd.KDGERD)
        '        frmDigital_IGD_01_GERD.ShowDialog(Me)
        '        fn_LoadSecurity()
        '    Catch ex As Exception
        '        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    Finally
        '        If Not frmDigital_IGD_01_GERD Is Nothing Then frmDigital_IGD_01_GERD.Dispose()
        '        frmDigital_IGD_01_GERD = Nothing
        '    End Try
        'End If
    End Sub
    Private Sub btnGERDQ_Click(sender As Object, e As EventArgs) Handles btnGERDQ.Click
        If txtKDKUNJUNGAN.Text = "" Then
            MsgBox("Silhakan Pilih Pasien", MsgBoxStyle.Information, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If

        Dim oGerdQ As New Digital.clsGerdQ
        Dim dsGerd = oGerdQ.GetDataByRegister(sKDPENDAFTARAN)
        If dsGerd Is Nothing Then
            Dim frmGerdQ As New frmGerdQ
            Try
                frmGerdQ.LoadMe(FORM_MODE.FORM_MODE_ADD, sKDPENDAFTARAN, txtKDCUSTOMER.Text, txtNAMAPASIEN.Text, grdDPJP.EditValue, "", True)
                frmGerdQ.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmGerdQ Is Nothing Then frmGerdQ.Dispose()
                frmGerdQ = Nothing
            End Try
        Else
            Dim frmGerdQ As New frmGerdQ
            Try
                frmGerdQ.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsGerd.KDPENDAFTARAN, txtNAMAPASIEN.Text, txtKDCUSTOMER.Text, dsGerd.KDDOCTOR, dsGerd.KDGERDQ, True)
                frmGerdQ.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmGerdQ Is Nothing Then frmGerdQ.Dispose()
                frmGerdQ = Nothing
            End Try
        End If
    End Sub
    Private Sub LaporanTindakanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanTindakanToolStripMenuItem.Click
        'If txtKDKUNJUNGAN.Text = "" Then
        '    MsgBox("Silhakan Pilih Pasien", MsgBoxStyle.Information, Me.Text)
        '    fn_EmptyMe()
        '    Exit Sub
        'End If

        'Dim frmLaporanTindakanList As New frmLaporanTindakanList
        'Try
        '    frmLaporanTindakanList.fn_LoadMe(grdDPJP.EditValue, grdDPJP.EditValue, txtKDKUNJUNGAN.Text, txtKDCUSTOMER.Text, txtNAMAPASIEN.Text, txtOBJEKTIF_JENISKELAMIN.Text, deDATETANGGALLAHIR.DateTime)
        '    frmLaporanTindakanList.ShowDialog(Me)
        'Catch ex As Exception
        '    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        'Finally
        '    If Not frmLaporanTindakanList Is Nothing Then frmLaporanTindakanList.Dispose()
        '    frmLaporanTindakanList = Nothing
        'End Try
    End Sub
    Private Sub LaporanOperasiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanOperasiToolStripMenuItem.Click
        'If txtKDKUNJUNGAN.Text = "" Then
        '    MsgBox("Silhakan Pilih Pasien", MsgBoxStyle.Information, Me.Text)
        '    fn_EmptyMe()
        '    Exit Sub
        'End If

        'Dim frmLaporanOperasiList As New frmLaporanOperasiList
        'Try
        '    frmLaporanOperasiList.fn_LoadMe(grdDPJP.EditValue, grdDPJP.EditValue, txtKDKUNJUNGAN.Text, txtKDCUSTOMER.Text, txtNAMAPASIEN.Text, txtOBJEKTIF_JENISKELAMIN.Text, deDATETANGGALLAHIR.DateTime)
        '    frmLaporanOperasiList.ShowDialog(Me)
        'Catch ex As Exception
        '    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        'Finally
        '    If Not frmLaporanOperasiList Is Nothing Then frmLaporanOperasiList.Dispose()
        '    frmLaporanOperasiList = Nothing
        'End Try
    End Sub
    Private Sub btnTutupListLaporanOperasi_Click(sender As Object, e As EventArgs) Handles btnTutupListLaporanOperasi.Click
        If btnTutupListLaporanOperasi.Text = "Tutup List" Then
            btnTutupListLaporanOperasi.Text = "Buka List"
            lGrdLaporanOperasi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            btnTutupListLaporanOperasi.Text = "Tutup List"
            lGrdLaporanOperasi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub
    Private Sub LembarObservasiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LembarObservasiToolStripMenuItem.Click
        'If txtKDKUNJUNGAN.Text = "" Then
        '    MsgBox("Silhakan Pilih Pasien", MsgBoxStyle.Information, Me.Text)
        '    fn_EmptyMe()
        '    Exit Sub
        'End If

        'Dim frmLembarObservasiList As New frmLembarObservasiList
        'Try
        '    frmLembarObservasiList.fn_LoadMe(grdDPJP.EditValue, grdKDDEPARTMENT.Text, "", txtKARTUBPJS.Text, grdDPJP.Text, txtKDKUNJUNGAN.Text, txtKDCUSTOMER.Text, txtNAMAPASIEN.Text, txtOBJEKTIF_JENISKELAMIN.Text, deDATETANGGALLAHIR.DateTime)
        '    frmLembarObservasiList.ShowDialog(Me)
        'Catch ex As Exception
        '    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        'Finally
        '    If Not frmLembarObservasiList Is Nothing Then frmLembarObservasiList.Dispose()
        '    frmLembarObservasiList = Nothing
        'End Try
    End Sub
    Private Sub deDATE_EditValueChanged(sender As Object, e As EventArgs) Handles deDATE.EditValueChanged
        If grdDPJP.Text <> "" Then
            fn_LoadJadwalDokter(grdDPJP.EditValue)
        End If
    End Sub
    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        Dim frmCPPT_TemplateProsedurList As New frmCPPT_TemplateProsedurList
        frmCPPT_TemplateProsedurList.ShowDialog()

        fn_LoadCPPT_TemplateTindakan()
    End Sub
    Private Sub btnResume_BukaTutupList_Click(sender As Object, e As EventArgs) Handles btnResume_BukaTutupList.Click
        If btnResume_BukaTutupList.Text = "Tutup" Then
            btnResume_BukaTutupList.Text = "Buka"
            lgrdResume.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            btnResume_BukaTutupList.Text = "Tutup"
            lgrdResume.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
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
            MsgBox("Load Data Resume" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub xtraReportResumeRawatJalan(ByVal Kode As String)
        Try
            PdfViewerResume.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Resume.....")

            Dim dataList As New List(Of Byte())

            Dim ds = oGrouperDataCppt.GetData(Kode)

            If ds IsNot Nothing Then
                Dim FolderSimpan = "C:\PDF\RME\RESUMERJ"
                If Not Directory.Exists(FolderSimpan) Then
                    Directory.CreateDirectory(FolderSimpan)
                Else
                    Try
                        DeleteDirectory(FolderSimpan)
                        Directory.CreateDirectory(FolderSimpan)
                    Catch ex As Exception

                    End Try
                End If

                Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"

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
            Dim oGrouperDataCppt As New EMedrek.clsRingkasanKeluar
            Dim ds = oGrouperDataCppt.GetData(Kode)

            If ds IsNot Nothing Then
                Dim FolderSimpan = "C:\PDF\RME\RESUMERI"
                If Not Directory.Exists(FolderSimpan) Then
                    Directory.CreateDirectory(FolderSimpan)
                Else
                    Try
                        DeleteDirectory(FolderSimpan)
                        Directory.CreateDirectory(FolderSimpan)
                    Catch ex As Exception

                    End Try
                End If

                Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"

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
            MsgBox("Cetak Resume Inap" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnTemplateNonRacikan_Click(sender As Object, e As EventArgs) Handles btnTemplateNonRacikan.Click
        Dim frmTemplateNonRacikanList As New frmTemplateNonRacikanList
        Try
            frmTemplateNonRacikanList.fn_LoadRacikan(False)
            frmTemplateNonRacikanList.ShowDialog(Me)
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmTemplateNonRacikanList Is Nothing Then frmTemplateNonRacikanList.Dispose()
            frmTemplateNonRacikanList = Nothing

            fn_LoadTEMPLATE()
        End Try
    End Sub
    Private Sub btnTemplateRacikan_Click(sender As Object, e As EventArgs) Handles btnTemplateRacikan.Click
        Dim frmTemplateNonRacikanList As New frmTemplateNonRacikanList
        Try
            frmTemplateNonRacikanList.fn_LoadRacikan(True)
            frmTemplateNonRacikanList.ShowDialog(Me)
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmTemplateNonRacikanList Is Nothing Then frmTemplateNonRacikanList.Dispose()
            frmTemplateNonRacikanList = Nothing

            fn_LoadTEMPLATE()
        End Try
    End Sub
    Private Sub picIcare_Click(sender As Object, e As EventArgs) Handles picIcare.Click
        Dim oAdmisi As New Admission.clsPendaftaran_Kunjungan
        'Dim oSetKoneksi As New Brigging.clsSetKoneksi
        Dim dsCariNosep = oAdmisi.GetData(txtKDKUNJUNGAN.Text)

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
#End Region
#End Region
#Region "Upload Scan"
#Region "Fungction"
    Private Sub fn_LoadDataScan()
        Try
            grvDokumenUpoad.Columns.Clear()
            grdDokumenUpoad.DataSource = Nothing

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
            SQL &= ",TANGGALDAFTAR = B.DATE "
            SQL &= ",TANGGALBUAT = A.DATECREATED "
            SQL &= ",NAMADOKUMEN = ISNULL((SELECT MEMO FROM M_PDF WHERE A.KDPDF = KDPDF), '') "
            SQL &= ",A.TYPEFILE "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_PDF A "
            SQL &= "INNER JOIN S_PENDAFTARAN_H B "
            SQL &= "ON A.KDPENDAFTARAN = B.KDPENDAFTARAN "
            SQL &= "WHERE B.KDCUSTOMER = '" & txtKDCUSTOMER.Text & "' "
            SQL &= "ORDER BY B.DATE DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

            grdDokumenUpoad.MainView = grvDokumenUpoad
            grdDokumenUpoad.DataSource = ds.Tables("ALL")
            grdDokumenUpoad.ForceInitialize()

            fn_LoadFormatDataScan()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Load Data Scan" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatDataScan()
        For iLoop As Integer = 0 To grvDokumenUpoad.Columns.Count - 1
            If grvDokumenUpoad.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvDokumenUpoad.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvDokumenUpoad.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grvDokumenUpoad.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvDokumenUpoad.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvDokumenUpoad.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvDokumenUpoad.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next
    End Sub
#End Region
#Region "Command Button"
    Private Sub grv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grvDokumenUpoad.DoubleClick
        If grvDokumenUpoad.GetFocusedRowCellValue("KDPENDAFTARAN") Is Nothing Then
            Exit Sub
        End If

        Dim frmGrouperPDF As New frmGrouperPDF
        Try
            frmGrouperPDF.LoadMeRegistrasi(FORM_MODE.FORM_MODE_VIEW, grvDokumenUpoad.GetFocusedRowCellValue("KDPENDAFTARAN"))
            frmGrouperPDF.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox("Double Click Upload Scan" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAdd_DokumenUpload_Click() Handles picAdd_DokumenUpload.Click
        If sKDPENDAFTARAN = "" Then
            MsgBox("Kode Pendaftaran Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        Dim ds = oPendaftaranPDF.GetData(sKDPENDAFTARAN)
        If ds Is Nothing Then
            Dim frmGrouperPDF As New frmGrouperPDF
            Try
                frmGrouperPDF.LoadMeRegistrasi(FORM_MODE.FORM_MODE_ADD, sKDPENDAFTARAN)
                frmGrouperPDF.ShowDialog(Me)
                fn_LoadDataScan()
            Catch oErr As Exception
                MsgBox("Add Document" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmGrouperPDF Is Nothing Then frmGrouperPDF.Dispose()
                frmGrouperPDF = Nothing
            End Try
        Else
            Dim frmGrouperPDF As New frmGrouperPDF
            Try
                frmGrouperPDF.LoadMeRegistrasi(FORM_MODE.FORM_MODE_EDIT, ds.KDPENDAFTARAN)
                frmGrouperPDF.ShowDialog(Me)
                fn_LoadDataScan()
            Catch oErr As Exception
                MsgBox("Add Document" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmGrouperPDF Is Nothing Then frmGrouperPDF.Dispose()
                frmGrouperPDF = Nothing
            End Try
        End If
    End Sub
    Private Sub picDelete_DokumenUpload_Click() Handles picDelete_DokumenUpload.Click
        If grvDokumenUpoad.GetFocusedRowCellValue("KDPENDAFTARAN") Is Nothing Then
            Exit Sub
        End If
        Dim ds = oPendaftaranPDF.GetData(grvDokumenUpoad.GetFocusedRowCellValue("KDPENDAFTARAN"))
        If ds Is Nothing Then
            MsgBox("Belum Ada Upload", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If MsgBox("Apakah akan dihapus?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If oPendaftaranPDF.DeleteData(grvDokumenUpoad.GetFocusedRowCellValue("KDPENDAFTARAN")) = False Then
            MsgBox("Hapus Gagal", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox("Hapus Berhasil", MsgBoxStyle.Information, Me.Text)
        fn_LoadDataScan()
    End Sub
    Private Sub picRefresh_DokumenUpload_Click() Handles picRefresh_DokumenUpload.Click
        fn_LoadDataScan()
    End Sub
    Private Sub btnType_Click(sender As Object, e As EventArgs) Handles btnType.Click
        fn_EmptyMe()
        fn_LoadViewType()
    End Sub
    Private Sub fn_LoadViewType()
        grvListPasien.Columns.Clear()
        grdListPasien.DataSource = Nothing
        grvListPasien.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        'grv1.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

        If btnType.Text = "Rawat Jalan" Then
            btnBataldanDeleteSEP.Visible = False
            XtraTabControl1.SelectedTabPageIndex = 1

            'lCPPTIncludePerawat.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            'lCPPTTidakIncludeDataLama.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lLISTANTRIANCHEK.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

            chkIsPulang.Visible = True
            btnPemetaan.Visible = True
            chkPemetaan.Visible = True

            Dim oKelasBed As New Reference.clsKelasAplicareBed

            Dim dsCekBed = oKelasBed.GetDataAda()
            If dsCekBed IsNot Nothing Then
                chkPemetaan.Checked = True
            Else
                chkPemetaan.Checked = False
            End If

            KosongBedToolStripMenuItem.Visible = True
            UpdateBedToolStripMenuItem.Visible = True

            btnType.Text = "Rawat Inap"
            lblPoli.Text = "Rawat Inap"
            btnMutasi.Visible = True
            grdDPJP.Properties.ReadOnly = False
            fn_LoadDEPARTMENT(True)
            lGROUPFORM.Text = "Form E Medrek Rawat Inap"
            lRawatJalan.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lRawatJalan_0.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lRawatJalan_1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lRawatJalan_2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lRawatJalan_3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

            LabelControl22.Visible = True
            LabelControl34.Visible = True
            grdDPJP.Visible = True

            picIcare.Visible = False

            If chkPemetaan.Checked = False Then
                deDATE.Visible = True
                deDateTo.Visible = True

                lblTanggalFrom.Visible = True
                lblTanggalTitikDuaFrom.Visible = True
                lblTanggalTo.Visible = True
                lblTanggalTitikDuaTo.Visible = True
                lblTanggalFrom.Text = "Dari Tgl"
                lblTanggalTo.Text = "Sampai Tgl"
            Else
                deDATE.Visible = False
                deDateTo.Visible = False

                lblTanggalFrom.Visible = False
                lblTanggalTitikDuaFrom.Visible = False
                lblTanggalTo.Visible = False
                lblTanggalTitikDuaTo.Visible = False
                lblTanggalFrom.Text = "Dari Tgl"
                lblTanggalTo.Text = "Sampai Tgl"
            End If

            lblJamHfis.Visible = False
            LabelControl45.Visible = False
            lblJadwalSIP.Visible = False
            LabelControl25.Visible = False
            LabelControl46.Visible = False
            lblSIP.Visible = False

            Dim oUser As New Setting.clsUser
            Dim oDoctor As New Reference.clsDoctor
            Dim dsUser = oUser.GetData(sUserID)
            If dsUser IsNot Nothing Then
                Dim dsDoctor = oDoctor.GetData(dsUser.KDDOCTOR)
                If dsDoctor IsNot Nothing Then
                    grdKDDEPARTMENT.EditValue = dsDoctor.KDDEPARTMENT
                    fn_LoadDokter(dsUser.KDDOCTOR)
                End If
            End If
        Else
            deDATE.DateTime = Now

            chkIsPulang.Visible = False
            btnPemetaan.Visible = False
            chkPemetaan.Visible = False

            If sisDOkter = False Then
                btnBataldanDeleteSEP.Visible = True
            Else
                btnBataldanDeleteSEP.Visible = False
            End If

            XtraTabControl1.SelectedTabPageIndex = 0
            'lCPPTIncludePerawat.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            'lCPPTTidakIncludeDataLama.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lLISTANTRIANCHEK.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            LabelControl22.Visible = True
            LabelControl34.Visible = True
            grdDPJP.Visible = True
            picIcare.Visible = True
            deDATE.Visible = True
            deDateTo.Visible = False

            lblTanggalFrom.Visible = True
            lblTanggalTitikDuaFrom.Visible = True
            lblTanggalTo.Visible = False
            lblTanggalTitikDuaTo.Visible = False
            lblTanggalFrom.Text = "Tanggal"
            lblTanggalTo.Text = "Tanggal"

            lblJamHfis.Visible = True
            LabelControl45.Visible = True
            lblJadwalSIP.Visible = True
            LabelControl25.Visible = True
            LabelControl46.Visible = True
            lblSIP.Visible = True

            KosongBedToolStripMenuItem.Visible = False
            UpdateBedToolStripMenuItem.Visible = False

            btnType.Text = "Rawat Jalan"
            lblPoli.Text = "Rawat Jalan"
            btnMutasi.Visible = False
            grdDPJP.Properties.ReadOnly = False
            fn_LoadDEPARTMENT(False)
            lGROUPFORM.Text = "Form E Medrek Rawat Jalan"
            lRawatJalan.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lRawatJalan_0.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lRawatJalan_1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lRawatJalan_2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lRawatJalan_3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            Dim oUser As New Setting.clsUser
            Dim oDoctor As New Reference.clsDoctor
            Dim dsUser = oUser.GetData(sUserID)
            If dsUser IsNot Nothing Then
                Dim dsDoctor = oDoctor.GetData(dsUser.KDDOCTOR)
                If dsDoctor IsNot Nothing Then
                    grdKDDEPARTMENT.EditValue = dsDoctor.KDDEPARTMENT
                    fn_LoadDokter(dsUser.KDDOCTOR)
                End If
            End If
        End If
    End Sub
    Private Sub btnBataldanDeleteSEP_Click(sender As Object, e As EventArgs) Handles btnBataldanDeleteSEP.Click
        Try
            If sKDPENDAFTARAN = String.Empty Then Exit Sub

            frmLoginDelete.ShowDialog()

            If MsgBox("Apakah Yakin Akan Batal?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

            If oPendaftaran.UpdateDataBatal(sKDPENDAFTARAN, sUserID) = True Then
                MsgBox("Status Daftar diperbaharui", MsgBoxStyle.Information, Me.Text)
                fn_LoadSecurity()
            End If

            'If txtNOMORSEP.Text = "" Or txtNOMORSEP.Text = "<--- AUTO --->" Then
            '    If oPendaftaran.UpdateDataBatal(sKDPENDAFTARAN) = True Then
            '        MsgBox("Status Daftar diperbaharui", MsgBoxStyle.Information, Me.Text)
            '        fn_LoadSecurity()
            '    End If
            'Else
            '    If fn_CariSEP(txtNOMORSEP.Text) = "KOSONG" Then
            '        If fn_DeleteSEP() = True Then
            '            If oPendaftaran.UpdateDataBatal(sKDPENDAFTARAN) = True Then
            '                MsgBox("Status Daftar diperbaharui", MsgBoxStyle.Information, Me.Text)
            '            End If
            '            fn_LoadSecurity()
            '        End If
            '    End If
            'End If
        Catch oErr As Exception
            MsgBox("Batal dan Delete SEP" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_CariSEP(ByVal NomorSep As String) As String
        Try
            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim uTime As Integer = 0

            If sVclaim_ConsId <> "" Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.CariSEP(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, NomorSep)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        fn_CariSEP = "ADA"
                    Else
                        fn_CariSEP = "KOSONG"
                    End If
                Else
                    fn_CariSEP = "KOSONG"
                End If
            Else
                fn_CariSEP = "KOSONG"
            End If
        Catch oErr As Exception
            fn_CariSEP = "KOSONG"
        End Try
    End Function
    Private Function fn_DeleteSEP() As Boolean
        Try
            If txtNOMORSEP.Text = String.Empty Then
                fn_DeleteSEP = True
                Exit Function
            End If

            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim uTime As Integer = 0

            If sVclaim_ConsId <> "" Then
                Dim jsonRequest As String = String.Empty

                jsonRequest = "{" & """request"": {" & """t_sep"": {" & """noSep"": """ & txtNOMORSEP.Text & """," & """user"": """ & sUserID & """" & "}}}"

                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.HapusSEPv2(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, jsonRequest)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        fn_DeleteSEP = True
                    Else
                        fn_DeleteSEP = False
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    fn_DeleteSEP = False
                    MsgBox("Delete SEP Data Gagal/Kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_DeleteSEP = False
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            fn_DeleteSEP = False
            MsgBox("Delete SEP" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
#End Region
#End Region
#Region "Asesmen Keperawatan"
#Region "Function"
    Private Sub QueryAsesmenPerawat_LoadHistory(ByVal NoRM As String)
        Try
            'If sSplashScreen = False Then Exit Sub

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
            SQL &= ",NAMADOKUMEN = 'ASESMEN AWAL KEPERAWATAN IGD' "
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
            SQL &= ",NAMADOKUMEN = 'ASESMEN KEBIDANAN PONEK' "
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
            SQL &= ",NAMADOKUMEN = 'LAPORAN PERSALINAN' "
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
            SQL &= ",NAMADOKUMEN = 'SKRINNING DAN EDUKASI GIZI' "
            SQL &= ",KODE = A.KDASESMEN "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RI_20 A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "
            'SQL &= "AND A.ISDELETE = 0 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "FORMULIR = 'ASESMEN AWAL KEPERAWATAN RAWAT INAP' "
            SQL &= ",NAMADOKUMEN = 'ASESMEN AWAL KEPERAWATAN RAWAT INAP' "
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
            SQL &= ",NAMADOKUMEN = 'ASESMEN AWAL KEPERAWATAN RAWAT JALAN' "
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
            SQL &= ",NAMADOKUMEN = 'PROTOKOL HEMODIALISA' "
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
            SQL &= ",NAMADOKUMEN = 'SURAT KETERANGAN HEMODIALISA' "
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
            SQL &= "FORMULIR = 'ASESMEN KEPERAWATAN PASIEN HEMODIALISA' "
            SQL &= ",NAMADOKUMEN = 'ASESMEN KEPERAWATAN PASIEN HEMODIALISA' "
            SQL &= ",KODE = A.KDKUNJUNGAN "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RJ_38 A "
            SQL &= "WHERE A.KDUSER_SIGNATURE = '" & NoRM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "FORMULIR = 'LEMBAR TRIAGE' "
            SQL &= ",NAMADOKUMEN = 'LEMBAR TRIAGE' "
            SQL &= ",KODE = A.KDKUNJUNGAN "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_TRIAGE A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "FORMULIR = 'GENERAL ICU' "
            SQL &= ",NAMADOKUMEN = 'GENERAL ICU' "
            SQL &= ",KODE = A.KDGENERALICU "
            SQL &= ",TANGGAL = A.TGLMASUKICU "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_GENERALICU_H A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "


            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "FORMULIR = 'FORMULIR DOKUMEN' "
            SQL &= ",A.NAMADOKUMEN "
            SQL &= ",KODE = A.KDDKUMEN "
            SQL &= ",TANGGAL = A.DATECREATED "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "A_DOKUMEN A "
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
        grvAsesmenPerawat_Transaksi.Columns("FORMULIR").Visible = False
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
            SQL &= ",NAMADOKUMEN = 'LAPORAN PERSALINAN' "
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
            SQL &= ",NAMADOKUMEN = 'ASESMEN AWAL KEPERAWATAN IGD' "
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
            SQL &= ",NAMADOKUMEN = 'ASESMEN KEBIDANAN PONEK' "
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
            SQL &= ",NAMADOKUMEN = 'SKRINNING DAN EDUKASI GIZI' "
            SQL &= ",KDFORMULIR = A.KDASESMEN "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RI_20 A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "
            'SQL &= "AND A.ISDELETE = 0 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "DESCRIPTION = 'ASESMEN AWAL KEPERAWATAN RAWAT INAP' "
            SQL &= ",NAMADOKUMEN = 'ASESMEN AWAL KEPERAWATAN RAWAT INAP' "
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
            SQL &= ",NAMADOKUMEN = 'ASESMEN AWAL KEPERAWATAN RAWAT JALAN' "
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
            SQL &= ",NAMADOKUMEN = 'PROTOKOL HEMODIALISA' "
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
            SQL &= ",NAMADOKUMEN = 'SURAT KETERANGAN HEMODIALISA' "
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
            SQL &= "FORMULIR = 'ASESMEN KEPERAWATAN PASIEN HEMODIALISA' "
            SQL &= ",NAMADOKUMEN = 'ASESMEN KEPERAWATAN PASIEN HEMODIALISA' "
            SQL &= ",KDFORMULIR = A.KDKUNJUNGAN "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RJ_38 A "
            SQL &= "WHERE A.KDUSER_SIGNATURE = '" & NoRM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "FORMULIR = 'LEMBAR PROGRAM TERAPI' "
            SQL &= ",NAMADOKUMEN = 'LEMBAR PROGRAM TERAPI' "
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
            SQL &= ",NAMADOKUMEN = 'LEMBAR TRIAGE' "
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
            SQL &= ",NAMADOKUMEN = 'FORMULIR GERD Q' "
            SQL &= ",KDFORMULIR = A.KDGERDQ "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_GERDQ_H A "
            SQL &= "WHERE A.KDCUSTOMER = '" & NoRM & "' "


            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "FORMULIR = 'GENERAL ICU' "
            SQL &= ",NAMADOKUMEN = 'GENERAL ICU' "
            SQL &= ",KDFORMULIR = A.KDGENERALICU "
            SQL &= ",TANGGAL = A.TGLMASUKICU "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_GENERALICU_H A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "FORMULIR = 'FORMULIR DOKUMEN' "
            SQL &= ",A.NAMADOKUMEN "
            SQL &= ",KDFORMULIR = A.KDDKUMEN "
            SQL &= ",TANGGAL = A.DATECREATED "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "A_DOKUMEN A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & NoRM & "' "


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
    Private Sub xtraReportDokumen(ByVal Kode As String, ByVal namadokumen As String)
        Try
            PdfViewerAsesmenNakes.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Awal Nakes.....")

            Dim dataList As New List(Of Byte())
            Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim oDigital As New Digital.clsDigital_A_Dokumen

            Dim ds = oDigital.GetData(Kode)

            If ds IsNot Nothing Then
                If ds.ISCHEKED = True Then
                    Dim rpt As New xtraDokumenFormulir1

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK

                    rpt.bindingSource.DataSource = ds
                    rpt.ExportToPdf(Alamat)

                    If FileIO.FileSystem.FileExists(Alamat) Then
                        PdfViewerAsesmenNakes.LoadDocument(Alamat)
                    End If
                Else
                    Dim rpt As New xtraDokumenFormulir

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK

                    rpt.bindingSource.DataSource = ds
                    rpt.ExportToPdf(Alamat)

                    If FileIO.FileSystem.FileExists(Alamat) Then
                        PdfViewerAsesmenNakes.LoadDocument(Alamat)
                    End If
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
    Private Sub xtraReportAsesmenKeperawatanHemodialisa(ByVal Kode As String)
        Try
            PdfViewerAsesmenNakes.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Awal Nakes.....")

            Dim dataList As New List(Of Byte())
            Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim oDigital As New EMedrek.clsDigital_RJ_38

            Dim ds = oDigital.GetData(Kode)

            If ds IsNot Nothing Then
                Dim rpt As New xtraReportEMedrekRJ_38

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

            Dim ds = oGrouperDataCppt.GetData(Kode)

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
    Private Sub xtraReportLembarTriage(ByVal Kode As String)
        Try
            PdfViewerAsesmenNakes.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Lembar Triage.....")

            Dim dataList As New List(Of Byte())
            Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim oDigital As New Digital.clsDigital_Triage

            Dim ds = oDigital.GetData(Kode)

            If ds IsNot Nothing Then
                Dim rpt As New xtraReportLembarTriage

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
    Private Sub xtraReportGeneralICU(ByVal Kode As String)
        Try
            PdfViewerAsesmenNakes.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Formulir General Icu.....")

            Dim dataList As New List(Of Byte())
            Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim oDigital As New EMedrek.clsGeneralICU

            Dim ds = oDigital.GetData(Kode)

            If ds IsNot Nothing Then
                Dim rpt As New xtraGeneralICU

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

        tabControlDokumen_SelectedPageChanged()
        lAsesmenPerawat_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lAsesmenPerawat_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lAsesmenKeperawatan_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        ClearFormAsesmenNakes()
        XtraTabControl1_SelectedPageChanged()
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
        If txtKDKUNJUNGAN.Text = "" Then
            MsgBox("Silahkan Pilih Dulu Pasien", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        Dim oDataGrouper As New Admission.clsPendaftaran_Kunjungan

        Dim ds = oDataGrouper.GetData(txtKDKUNJUNGAN.Text)

        If ds IsNot Nothing Then
            pnlAsesmenKeperawatan_pic.Dock = DockStyle.None

            lAsesmenPerawat_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lAsesmenPerawat_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lAsesmenKeperawatan_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            If grdKDFORMULIRNAKES.Text = "ASESMEN AWAL KEPERAWATAN IGD" Then
                Try
                    Dim oS_DIGITAL_IGD_02_NEW As New Digital.clsDigital_IGD_02

                    Dim dsDigital = oS_DIGITAL_IGD_02_NEW.GetDataPendaftaran(ds.KDPENDAFTARAN)

                    If dsDigital Is Nothing Then
                        If MsgBox("Tekan Yes Tidak Ke Panel", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then

                            ' Bersihkan konten panel
                            pnlAsesmenPerawat_Transaksi.Controls.Clear()

                            ' Set properti form anak
                            frmEMedrekIDG_02_New.TopLevel = False
                            frmEMedrekIDG_02_New.FormBorderStyle = FormBorderStyle.None
                            frmEMedrekIDG_02_New.Dock = DockStyle.Fill

                            ' Tambahkan form anak ke panel
                            pnlAsesmenPerawat_Transaksi.Controls.Add(frmEMedrekIDG_02_New)

                            ' Tampilkan form anak
                            frmEMedrekIDG_02_New.fn_KeluarAuoto(False)
                            frmEMedrekIDG_02_New.LoadMe(FORM_MODE.FORM_MODE_ADD, ds.KDPENDAFTARAN, ds.S_PENDAFTARAN_H.KDCUSTOMER)
                            frmEMedrekIDG_02_New.Show()
                        Else
                            Dim frmEMedrekIDG_02_New As New frmEMedrekIDG_02_New
                            Try
                                frmEMedrekIDG_02_New.fn_KeluarAuoto(True)
                                frmEMedrekIDG_02_New.LoadMe(FORM_MODE.FORM_MODE_ADD, ds.KDPENDAFTARAN, ds.S_PENDAFTARAN_H.KDCUSTOMER)
                                frmEMedrekIDG_02_New.ShowDialog(Me)
                            Catch oErr As Exception
                                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                            Finally
                                If Not frmEMedrekIDG_02_New Is Nothing Then frmEMedrekIDG_02_New.Dispose()
                                frmEMedrekIDG_02_New = Nothing

                                pnlAsesmenKeperawatan_pic.Dock = DockStyle.Top

                                tabControlDokumen_SelectedPageChanged()
                                lAsesmenPerawat_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                                lAsesmenPerawat_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                                lAsesmenKeperawatan_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                                ClearFormAsesmenNakes()
                            End Try
                        End If
                    Else
                        MsgBox("Formulir Sudah dibuatkan, Silahkan lakukan Edit untuk memperbaiki", MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf grdKDFORMULIRNAKES.Text = "ASESMEN KEBIDANAN PONEK" Then
                Try
                    Dim dsIdentitas = oDataGrouper.GetDatabyKodeKunjungan(ds.KDKUNJUNGAN)

                    If dsIdentitas IsNot Nothing Then
                        Dim oS_DIGITAL_IGD_03 As New Digital.clsDigital_IGD_03

                        Dim dsDigital = oS_DIGITAL_IGD_03.GetDataByKode(dsIdentitas.KDIDENTITAS)
                        If dsDigital Is Nothing Then
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
                        Else
                            MsgBox("Formulir Sudah dibuatkan, Silahkan lakukan Edit untuk memperbaiki", MsgBoxStyle.Exclamation, Me.Text)
                        End If
                    Else
                        MsgBox("Identitas Tidak diTemukan", MsgBoxStyle.Information, Me.Text)
                    End If
                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf grdKDFORMULIRNAKES.Text = "LAPORAN PERSALINAN" Then
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
                    Else
                        MsgBox("Identitas Tidak diTemukan", MsgBoxStyle.Information, Me.Text)
                    End If

                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf grdKDFORMULIRNAKES.Text = "SKRINNING DAN EDUKASI GIZI" Then
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

                        Dim ruangan As String = String.Empty

                        If btnType.Text = "Rawat Inap" Then
                            ruangan = grvListPasien.GetFocusedRowCellValue("WaktuPeriksa")
                        Else
                            ruangan = grdKDDEPARTMENT.Text
                        End If

                        ' Tampilkan form anak
                        frmEMedrekRI_20.LoadMe(FORM_MODE.FORM_MODE_ADD, dsIdentitas.KDIDENTITAS, "", ruangan)
                        frmEMedrekRI_20.Show()
                    Else
                        MsgBox("Identitas Tidak diTemukan", MsgBoxStyle.Information, Me.Text)
                    End If

                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf grdKDFORMULIRNAKES.Text = "ASESMEN AWAL KEPERAWATAN RAWAT INAP" Then
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
                    Else
                        MsgBox("Identitas Tidak diTemukan", MsgBoxStyle.Information, Me.Text)
                    End If

                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf grdKDFORMULIRNAKES.Text = "ASESMEN AWAL KEPERAWATAN RAWAT JALAN" Then
                Try
                    Dim dsIdentitas = oDataGrouper.GetDatabyKodeKunjungan(txtKDKUNJUNGAN.Text)
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
                    Else
                        MsgBox("Identitas Tidak diTemukan", MsgBoxStyle.Information, Me.Text)
                    End If
                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf grdKDFORMULIRNAKES.Text = "PROTOKOL HEMODIALISA" Then
                Try
                    Dim dsIdentitas = oDataGrouper.GetDatabyKodeKunjungan(txtKDKUNJUNGAN.Text)
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
                    Else
                        MsgBox("Identitas Tidak diTemukan", MsgBoxStyle.Information, Me.Text)
                    End If
                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf grdKDFORMULIRNAKES.Text = "ASESMEN KEPERAWATAN PASIEN HEMODIALISA"
                'Dim dsIdentitas = oDataGrouper.GetDatabyKodeKunjungan(txtKDKUNJUNGAN.Text)
                'If dsIdentitas IsNot Nothing Then
                '    Dim oS_DIGITAL_IGD_01 As New EMedrek.clsDigital_RJ_38
                '    Dim dsDisgital = oS_DIGITAL_IGD_01.GetData(dsIdentitas.KDPENDAFTARAN)

                '    If dsDisgital Is Nothing Then
                '        ' Bersihkan konten panel
                '        pnlAsesmenPerawat_Transaksi.Controls.Clear()

                '        ' Set properti form anak
                '        frmEMedrekRJ_38.TopLevel = False
                '        frmEMedrekRJ_38.FormBorderStyle = FormBorderStyle.None
                '        frmEMedrekRJ_38.Dock = DockStyle.Fill

                '        ' Tambahkan form anak ke panel
                '        pnlAsesmenPerawat_Transaksi.Controls.Add(frmEMedrekRJ_38)

                '        ' Tampilkan form anak
                '        frmEMedrekRJ_38.LoadMe(FORM_MODE.FORM_MODE_ADD, dsIdentitas.KDPENDAFTARAN, dsIdentitas.KDIDENTITAS)
                '        frmEMedrekRJ_38.Show()
                '    Else
                '        MsgBox("Formulir Sudah dibuatkan, Silahkan lakukan Edit untuk memperbaiki", MsgBoxStyle.Exclamation, Me.Text)
                '    End If
                'Else
                '    MsgBox("Kunjungan Tidak di Temukan", MsgBoxStyle.Exclamation, Me.Text)
                'End If

                Try
                    Dim dsIdentitas = oDataGrouper.GetDatabyKodeKunjungan(txtKDKUNJUNGAN.Text)

                    If dsIdentitas IsNot Nothing Then
                        Dim oS_DIGITAL_IGD_02_NEW As New EMedrek.clsDigital_RJ_38

                        Dim dsDigital = oS_DIGITAL_IGD_02_NEW.GetData(ds.KDPENDAFTARAN)

                        If dsDigital Is Nothing Then
                            If MsgBox("Tekan Yes Tidak Ke Panel", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then

                                ' Bersihkan konten panel
                                pnlAsesmenPerawat_Transaksi.Controls.Clear()

                                ' Set properti form anak
                                frmEMedrekRJ_38.TopLevel = False
                                frmEMedrekRJ_38.FormBorderStyle = FormBorderStyle.None
                                frmEMedrekRJ_38.Dock = DockStyle.Fill

                                ' Tambahkan form anak ke panel
                                pnlAsesmenPerawat_Transaksi.Controls.Add(frmEMedrekRJ_38)

                                ' Tampilkan form anak
                                frmEMedrekRJ_38.fn_KeluarAuoto(False)
                                frmEMedrekRJ_38.LoadMe(FORM_MODE.FORM_MODE_ADD, dsIdentitas.KDPENDAFTARAN, dsIdentitas.KDIDENTITAS)
                                frmEMedrekRJ_38.Show()
                            Else
                                Dim frmEMedrekRJ_38 As New frmEMedrekRJ_38
                                Try
                                    frmEMedrekRJ_38.fn_KeluarAuoto(True)
                                    frmEMedrekRJ_38.LoadMe(FORM_MODE.FORM_MODE_ADD, dsIdentitas.KDPENDAFTARAN, dsIdentitas.KDIDENTITAS)
                                    frmEMedrekRJ_38.ShowDialog(Me)
                                Catch oErr As Exception
                                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                                Finally
                                    If Not frmEMedrekRJ_38 Is Nothing Then frmEMedrekRJ_38.Dispose()
                                    frmEMedrekRJ_38 = Nothing

                                    pnlAsesmenKeperawatan_pic.Dock = DockStyle.Top

                                    tabControlDokumen_SelectedPageChanged()
                                    lAsesmenPerawat_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                                    lAsesmenPerawat_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                                    lAsesmenKeperawatan_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                                    ClearFormAsesmenNakes()
                                End Try
                            End If
                        Else
                            MsgBox("Formulir Sudah dibuatkan, Silahkan lakukan Edit untuk memperbaiki", MsgBoxStyle.Exclamation, Me.Text)
                        End If
                    Else
                        MsgBox("Kunjungan Tidak di Temukan", MsgBoxStyle.Exclamation, Me.Text)
                    End If


                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf grdKDFORMULIRNAKES.Text = "SURAT KETERANGAN HEMODIALISA" Then
                Try
                    Dim dsIdentitas = oDataGrouper.GetDatabyKodeKunjungan(txtKDKUNJUNGAN.Text)
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
                    Else
                        MsgBox("Identitas Tidak diTemukan", MsgBoxStyle.Information, Me.Text)
                    End If
                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf grdKDFORMULIRNAKES.Text = "LEMBAR TRIAGE" Then
                Try
                    Dim dsIdentitas = oDataGrouper.GetDatabyKodeKunjungan(txtKDKUNJUNGAN.Text)
                    If dsIdentitas IsNot Nothing Then
                        Dim oDigital As New Digital.clsDigital_Triage

                        Dim dsDigital = oDigital.GetData(dsIdentitas.KDKUNJUNGAN)

                        If dsDigital IsNot Nothing Then
                            MsgBox("Formulir Sudah dibuatkan, Silahkan lakukan Edit untuk memperbaiki", MsgBoxStyle.Exclamation, Me.Text)
                            Exit Sub
                        End If

                        '' Bersihkan konten panel
                        'pnlAsesmenPerawat_Transaksi.Controls.Clear()

                        '' Set properti form anak
                        'frmEMedrekTriage.TopLevel = False
                        'frmEMedrekTriage.FormBorderStyle = FormBorderStyle.None
                        'frmEMedrekTriage.Dock = DockStyle.Fill

                        '' Tambahkan form anak ke panel
                        'pnlAsesmenPerawat_Transaksi.Controls.Add(frmEMedrekTriage)

                        '' Tampilkan form anak
                        'frmEMedrekTriage.LoadMe(FORM_MODE.FORM_MODE_ADD, dsIdentitas.KDDOCTOR, dsIdentitas.KDIDENTITAS, dsIdentitas.KDKUNJUNGAN)
                        'frmEMedrekTriage.Show()

                        Dim frmEMedrekTriage As New frmEMedrekTriage
                        Try
                            frmEMedrekTriage.LoadMe(FORM_MODE.FORM_MODE_ADD, dsIdentitas.KDDOCTOR, dsIdentitas.KDIDENTITAS, dsIdentitas.KDKUNJUNGAN, True)
                            frmEMedrekTriage.ShowDialog(Me)
                        Catch oErr As Exception
                            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                        Finally
                            If Not frmEMedrekTriage Is Nothing Then frmEMedrekTriage.Dispose()
                            frmEMedrekTriage = Nothing

                            pnlAsesmenKeperawatan_pic.Dock = DockStyle.Top

                            tabControlDokumen_SelectedPageChanged()
                            lAsesmenPerawat_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                            lAsesmenPerawat_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                            lAsesmenKeperawatan_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                            ClearFormAsesmenNakes()
                        End Try
                    Else
                        MsgBox("Identitas Tidak diTemukan", MsgBoxStyle.Information, Me.Text)
                    End If
                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf grdKDFORMULIRNAKES.Text = "GENERAL ICU" Then
                Dim dsIdentitas = oDataGrouper.GetDatabyKodeKunjungan(txtKDKUNJUNGAN.Text)
                If dsIdentitas IsNot Nothing Then
                    Dim frmGeneralICU As New frmGeneralICU
                    Try
                        frmGeneralICU.fn_loadDataIdentitas(dsIdentitas.KDIDENTITAS)
                        frmGeneralICU.LoadMe(FORM_MODE.FORM_MODE_ADD)
                        frmGeneralICU.ShowDialog(Me)
                    Catch oErr As Exception
                        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    Finally
                        If Not frmGeneralICU Is Nothing Then frmGeneralICU.Dispose()
                        frmGeneralICU = Nothing

                        pnlAsesmenKeperawatan_pic.Dock = DockStyle.Top

                        tabControlDokumen_SelectedPageChanged()
                        lAsesmenPerawat_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        lAsesmenPerawat_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        lAsesmenKeperawatan_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                        ClearFormAsesmenNakes()
                    End Try
                Else
                    MsgBox("Identitas Kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If
            ElseIf grdKDFORMULIRNAKES.Text = "FORMULIR DOKUMEN" Then
                Dim dsIdentitas = oDataGrouper.GetDatabyKodeKunjungan(txtKDKUNJUNGAN.Text)
                If dsIdentitas IsNot Nothing Then
                    'Dim frmDigital_A_Dokumen As New frmDigital_A_Dokumen
                    'Try
                    '    frmDigital_A_Dokumen.LoadMe(FORM_MODE.FORM_MODE_ADD, dsIdentitas.KDIDENTITAS, "")
                    '    frmDigital_A_Dokumen.ShowDialog(Me)
                    'Catch oErr As Exception
                    '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    'Finally
                    '    If Not frmDigital_A_Dokumen Is Nothing Then frmDigital_A_Dokumen.Dispose()
                    '    frmDigital_A_Dokumen = Nothing

                    '    pnlAsesmenKeperawatan_pic.Dock = DockStyle.Top

                    '    tabControlDokumen_SelectedPageChanged()
                    '    lAsesmenPerawat_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    '    lAsesmenPerawat_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    '    lAsesmenKeperawatan_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                    '    ClearFormAsesmenNakes()
                    'End Try

                    ' Bersihkan konten panel
                    pnlAsesmenPerawat_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmDigital_A_Dokumen.TopLevel = False
                    frmDigital_A_Dokumen.FormBorderStyle = FormBorderStyle.None
                    frmDigital_A_Dokumen.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsesmenPerawat_Transaksi.Controls.Add(frmDigital_A_Dokumen)

                    ' Tampilkan form anak
                    frmDigital_A_Dokumen.LoadMe(FORM_MODE.FORM_MODE_ADD, dsIdentitas.KDIDENTITAS, "")
                    frmDigital_A_Dokumen.Show()
                Else
                    MsgBox("Identitas Kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                MsgBox("Dokumen Belum Ada", MsgBoxStyle.Exclamation, Me.Text)

                pnlAsesmenKeperawatan_pic.Dock = DockStyle.Top

                XtraTabControl1_SelectedPageChanged()
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

        'If grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("KDUSER") <> sUserID Then
        '    MsgBox("Maap Tidak dapat Rubah Silahkan Hubungi User " & grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("KDUSER"), MsgBoxStyle.Exclamation, Me.Text)
        '    Exit Sub
        'End If

        pnlAsesmenKeperawatan_pic.Dock = DockStyle.None

        lAsesmenPerawat_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lAsesmenPerawat_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lAsesmenKeperawatan_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

        If grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("FORMULIR") = "ASESMEN AWAL KEPERAWATAN IGD" Then
            Dim oDigital As New Digital.clsDigital_IGD_02

            Dim dsDigital = oDigital.GetData(grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("KODE"))

            If dsDigital IsNot Nothing Then
                If MsgBox("Tekan Yes Tidak Ke Panel", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then
                    Try
                        ' Bersihkan konten panel
                        pnlAsesmenPerawat_Transaksi.Controls.Clear()

                        ' Set properti form anak
                        frmEMedrekIDG_02_New.TopLevel = False
                        frmEMedrekIDG_02_New.FormBorderStyle = FormBorderStyle.None
                        frmEMedrekIDG_02_New.Dock = DockStyle.Fill

                        ' Tambahkan form anak ke panelgrvCPPT_Tindakan
                        pnlAsesmenPerawat_Transaksi.Controls.Add(frmEMedrekIDG_02_New)

                        ' Tampilkan form anak
                        frmEMedrekIDG_02_New.fn_KeluarAuoto(False)
                        frmEMedrekIDG_02_New.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.KDPENDAFTARAN, dsDigital.KDCUSTOMER, dsDigital.KDASESMEN)
                        frmEMedrekIDG_02_New.Show()
                    Catch ex As Exception
                        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                Else
                    Dim frmEMedrekIDG_02_New As New frmEMedrekIDG_02_New
                    Try
                        frmEMedrekIDG_02_New.fn_KeluarAuoto(True)
                        frmEMedrekIDG_02_New.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.KDPENDAFTARAN, dsDigital.KDCUSTOMER, dsDigital.KDASESMEN)
                        frmEMedrekIDG_02_New.ShowDialog(Me)
                    Catch oErr As Exception
                        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    Finally
                        If Not frmEMedrekIDG_02_New Is Nothing Then frmEMedrekIDG_02_New.Dispose()
                        frmEMedrekIDG_02_New = Nothing

                        pnlAsesmenKeperawatan_pic.Dock = DockStyle.Top

                        tabControlDokumen_SelectedPageChanged()
                        lAsesmenPerawat_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        lAsesmenPerawat_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        lAsesmenKeperawatan_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                        ClearFormAsesmenNakes()
                    End Try
                End If

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
        ElseIf grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("FORMULIR") = "ASESMEN KEPERAWATAN PASIEN HEMODIALISA" Then
            Dim oDigital As New EMedrek.clsDigital_RJ_38
            Dim dsDigital = oDigital.GetData(grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("KODE"))

            If dsDigital IsNot Nothing Then
                If MsgBox("Tekan Yes Tidak Ke Panel", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then

                    ' Bersihkan konten panel
                    pnlAsesmenPerawat_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmEMedrekRJ_38.TopLevel = False
                    frmEMedrekRJ_38.FormBorderStyle = FormBorderStyle.None
                    frmEMedrekRJ_38.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsesmenPerawat_Transaksi.Controls.Add(frmEMedrekRJ_38)

                    ' Tampilkan form anak
                    frmEMedrekRJ_38.fn_KeluarAuoto(False)
                    frmEMedrekRJ_38.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.KDKUNJUNGAN, dsDigital.KDIDENTITAS)
                    frmEMedrekRJ_38.Show()
                Else
                    Dim frmEMedrekRJ_38 As New frmEMedrekRJ_38
                    Try
                        frmEMedrekRJ_38.fn_KeluarAuoto(True)
                        frmEMedrekRJ_38.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.KDKUNJUNGAN, dsDigital.KDIDENTITAS)
                        frmEMedrekRJ_38.ShowDialog(Me)
                    Catch oErr As Exception
                        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    Finally
                        If Not frmEMedrekRJ_38 Is Nothing Then frmEMedrekRJ_38.Dispose()
                        frmEMedrekRJ_38 = Nothing

                        pnlAsesmenKeperawatan_pic.Dock = DockStyle.Top

                        tabControlDokumen_SelectedPageChanged()
                        lAsesmenPerawat_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        lAsesmenPerawat_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        lAsesmenKeperawatan_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                        ClearFormAsesmenNakes()
                    End Try
                End If
            Else
                MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
            End If
        ElseIf grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("FORMULIR") = "LEMBAR TRIAGE" Then
            Dim oDigital As New Digital.clsDigital_Triage

            Dim dsDigital = oDigital.GetData(grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("KODE"))

            If dsDigital IsNot Nothing Then
                'Try
                '    ' Bersihkan konten panel
                '    pnlAsesmenPerawat_Transaksi.Controls.Clear()

                '    ' Set properti form anak
                '    frmEMedrekTriage.TopLevel = False
                '    frmEMedrekTriage.FormBorderStyle = FormBorderStyle.None
                '    frmEMedrekTriage.Dock = DockStyle.Fill

                '    ' Tambahkan form anak ke panel
                '    pnlAsesmenPerawat_Transaksi.Controls.Add(frmEMedrekTriage)

                '    ' Tampilkan form anak
                '    frmEMedrekTriage.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.A_IDENTITASPASIEN_LIST.KDDOCTOR, dsDigital.KDIDENTITAS, dsDigital.KDKUNJUNGAN)
                '    frmEMedrekTriage.Show()
                'Catch ex As Exception
                '    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                'End Try
                Dim frmEMedrekTriage As New frmEMedrekTriage
                Try
                    frmEMedrekTriage.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.A_IDENTITASPASIEN_LIST.KDDOCTOR, dsDigital.KDIDENTITAS, dsDigital.KDKUNJUNGAN, True)
                    frmEMedrekTriage.ShowDialog(Me)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                Finally
                    If Not frmEMedrekTriage Is Nothing Then frmEMedrekTriage.Dispose()
                    frmEMedrekTriage = Nothing

                    pnlAsesmenKeperawatan_pic.Dock = DockStyle.Top

                    tabControlDokumen_SelectedPageChanged()
                    lAsesmenPerawat_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    lAsesmenPerawat_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    lAsesmenKeperawatan_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                    ClearFormAsesmenNakes()
                End Try
            Else
                MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
            End If
        ElseIf grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("FORMULIR") = "GENERAL ICU" Then
            Dim oGeneralICU As New EMedrek.clsGeneralICU
            Dim dsGeneralICU = oGeneralICU.GetData(grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("KODE"))
            If dsGeneralICU IsNot Nothing Then
                Dim frmGeneralICU As New frmGeneralICU
                Try
                    frmGeneralICU.fn_loadDataIdentitas(dsGeneralICU.KDIDENTITAS)
                    frmGeneralICU.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsGeneralICU.KDGENERALICU)
                    frmGeneralICU.ShowDialog(Me)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                Finally
                    If Not frmGeneralICU Is Nothing Then frmGeneralICU.Dispose()
                    frmGeneralICU = Nothing

                    pnlAsesmenKeperawatan_pic.Dock = DockStyle.Top

                    tabControlDokumen_SelectedPageChanged()
                    lAsesmenPerawat_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    lAsesmenPerawat_pnl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    lAsesmenKeperawatan_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                    ClearFormAsesmenNakes()
                End Try
            Else
                MsgBox("Identitas Kosong", MsgBoxStyle.Exclamation, Me.Text)
            End If
        ElseIf grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("FORMULIR") = "FORMULIR DOKUMEN" Then
            Try
                Dim oDigital_A_Dokumen As New Digital.clsDigital_A_Dokumen

                Dim dsDokumen = oDigital_A_Dokumen.GetData(grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("KODE"))
                If dsDokumen IsNot Nothing Then
                    ' Bersihkan konten panel
                    pnlAsesmenPerawat_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmDigital_A_Dokumen.TopLevel = False
                    frmDigital_A_Dokumen.FormBorderStyle = FormBorderStyle.None
                    frmDigital_A_Dokumen.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsesmenPerawat_Transaksi.Controls.Add(frmDigital_A_Dokumen)

                    ' Tampilkan form anak
                    frmDigital_A_Dokumen.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDokumen.KDIDENTITAS, dsDokumen.KDDKUMEN)
                    frmDigital_A_Dokumen.Show()
                End If

            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
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
                XtraTabControl1_SelectedPageChanged()
            End If
        ElseIf grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("FORMULIR") = "ASESMEN KEBIDANAN VONEK" Then
            Dim frmPesanDelete As New frmPesanDelete
            frmPesanDelete.ShowDialog(Me)

            If sPesanHapus <> "XXXXXBATALXXXXX" Then
                Dim oDigital As New Digital.clsDigital_IGD_03
                oDigital.UpdateDelete(grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("KODE"), sUserID)
                XtraTabControl1_SelectedPageChanged()
            End If
        ElseIf grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("FORMULIR") = "LAPORAN PERSALINAN" Then
            Dim frmPesanDelete As New frmPesanDelete
            frmPesanDelete.ShowDialog(Me)

            If sPesanHapus <> "XXXXXBATALXXXXX" Then
                Dim oDigital As New EMedrek.clsLaporanPersalinan
                oDigital.UpdateDelete(grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("KODE"), sUserID)
                XtraTabControl1_SelectedPageChanged()
            End If
        ElseIf grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("FORMULIR") = "SKRINNING DAN EDUKASI GIZI" Then
            Dim frmPesanDelete As New frmPesanDelete
            frmPesanDelete.ShowDialog(Me)

            If sPesanHapus <> "XXXXXBATALXXXXX" Then
                Dim oDigital As New Digital.clsDigital_RI_20
                oDigital.DeleteData(grvAsesmenPerawat_Transaksi.GetFocusedRowCellValue("KODE"))
                XtraTabControl1_SelectedPageChanged()
            End If
        End If
    End Sub
    Private Sub picAsesmenPerawat_Refresh_Click(sender As Object, e As EventArgs) Handles picAsesmenPerawat_Refresh.Click
        tabControlDokumen_SelectedPageChanged()
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

        If Not frmGeneralICU Is Nothing Then frmGeneralICU.Dispose()
        frmGeneralICU = Nothing

        If Not frmDigital_A_Dokumen Is Nothing Then frmDigital_A_Dokumen.Dispose()
        frmDigital_A_Dokumen = Nothing

        If Not frmEMedrekRJ_38 Is Nothing Then frmEMedrekRJ_38.Dispose()
        frmEMedrekRJ_38 = Nothing
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
            ElseIf grvAsesmenNakesPDF.GetFocusedRowCellValue("DESCRIPTION") = "ASESMEN KEPERAWATAN PASIEN HEMODIALISA" Then
                xtraReportAsesmenKeperawatanHemodialisa(grvAsesmenNakesPDF.GetFocusedRowCellValue("KDFORMULIR"))
            ElseIf grvAsesmenNakesPDF.GetFocusedRowCellValue("DESCRIPTION") = "LEMBAR PROGRAM TERAPI" Then
                xtraReportLaporanLembarProgramTerapi(grvAsesmenNakesPDF.GetFocusedRowCellValue("KDFORMULIR"))
            ElseIf grvAsesmenNakesPDF.GetFocusedRowCellValue("DESCRIPTION") = "LEMBAR TRIAGE" Then
                xtraReportLembarTriage(grvAsesmenNakesPDF.GetFocusedRowCellValue("KDFORMULIR"))
            ElseIf grvAsesmenNakesPDF.GetFocusedRowCellValue("DESCRIPTION") = "FORMULIR GERD Q" Then
                xtraReportLaporanFormulirGerd(grvAsesmenNakesPDF.GetFocusedRowCellValue("KDFORMULIR"))
            ElseIf grvAsesmenNakesPDF.GetFocusedRowCellValue("DESCRIPTION") = "GENERAL ICU" Then
                xtraReportGeneralICU(grvAsesmenNakesPDF.GetFocusedRowCellValue("KDFORMULIR"))
            ElseIf grvAsesmenNakesPDF.GetFocusedRowCellValue("DESCRIPTION") = "FORMULIR DOKUMEN" Then
                xtraReportDokumen(grvAsesmenNakesPDF.GetFocusedRowCellValue("KDFORMULIR"), grvAsesmenNakesPDF.GetFocusedRowCellValue("NAMADOKUMEN"))
            End If
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
            SQL &= "KDFORMULIR = B.KDREG "
            SQL &= ",DESCRIPTION = 'RESUME RAWAT INAP' "
            SQL &= ",TANGGAL = B.TANGGALMASUK "
            SQL &= ",KDPENDAFTARAN = B.KDREG "
            SQL &= ",TUJUAN = A.KDDEPARTMENT_NAMA "
            SQL &= ",B.KDUSER "
            SQL &= "FROM "
            SQL &= "A_IDENTITASPASIEN_LIST A "
            SQL &= "INNER JOIN S_RINGKASANKELUARRAWATINAP B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE A.KDCUSTOMER = '" & NoRM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDLAPORANOPERASI "
            SQL &= ",DESCRIPTION = 'LAPORAN OPERASI' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",A.KDPENDAFTARAN "
            SQL &= ",TUJUAN = '-' "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_OK_LAPORANOPERASI A "
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
            SQL &= ",DESCRIPTION = 'OK - KARTU ANASTESI A' "
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
            SQL &= ",DESCRIPTION = 'OK - KARTU ANASTESI B' "
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
            SQL &= ",NAMAFORMULIR = 'ASESMEN AWAL MEDIS IGD' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",A.KDPENDAFTARAN "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_IGD_01 A "
            SQL &= "WHERE A.KDCUSTOMER = '" & NoRM & "' "
            SQL &= "AND A.ISDELETE = 0 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDLAPORANOPERASI "
            SQL &= ",DESCRIPTION = 'LAPORAN OPERASI' "
            SQL &= ",NAMAFORMULIR = A.JENIS_OPERASI "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",A.KDPENDAFTARAN "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_OK_LAPORANOPERASI A "
            SQL &= "WHERE A.KDCUSTOMER = '" & NoRM & "' "
            SQL &= "AND A.ISDELETE = 0 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDFORMULIR = A.KDREG "
            SQL &= ",DESCRIPTION = 'CATATAN MEDIS AWAL RAWAT INAP DAN DISCHARGE PLANNING' "
            SQL &= ",NAMAFORMULIR = 'CATATAN MEDIS AWAL RAWAT INAP DAN DISCHARGE PLANNING' "
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
            SQL &= ",DESCRIPTION = 'OK - KARTU ANASTESI A' "
            SQL &= ",NAMAFORMULIR = 'OK - KARTU ANASTESI A' "
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
            SQL &= ",DESCRIPTION = 'OK - KARTU ANASTESI B' "
            SQL &= ",NAMAFORMULIR = 'OK - KARTU ANASTESI B' "
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
            SQL &= ",NAMAFORMULIR = 'ASESMEN AWAL MEDIS PASIEN PENYAKIT DALAM' "
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
            SQL &= ",NAMAFORMULIR = 'ASESMEN AWAL MEDIS PASIEN BEDAH' "
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
            SQL &= ",NAMAFORMULIR = 'ASESMEN AWAL MEDIS PASIEN JANTUNG' "
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
            SQL &= ",NAMAFORMULIR = 'ASESMEN AWAL MEDIS PASIEN GIGI' "
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
            SQL &= ",NAMAFORMULIR = 'ASESMEN AWAL MEDIS PASIEN NEUROLOGI' "
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
            SQL &= ",NAMAFORMULIR = 'ASESMEN AWAL MEDIS OBSTETRI & GYNEKOLOGI' "
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
            SQL &= ",NAMAFORMULIR = 'ASESMEN AWAL MEDIS PASIEN KULIT DAN KELAMIN' "
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
            SQL &= ",NAMAFORMULIR = 'ASESMEN AWAL MEDIS PASIEN MATA' "
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
            SQL &= ",NAMAFORMULIR = 'ASESMEN AWAL MEDIS PASIEN THT' "
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
            SQL &= ",NAMAFORMULIR = 'ASESMEN AWAL MEDIS NEONATUS' "
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
            SQL &= ",NAMAFORMULIR = 'UJI FUNGSI, LAYANAN & PROSEDUR KEDOKTERAN FISIK & REHABILITASI' "
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
            SQL &= ",NAMAFORMULIR = 'FORMULIR CATATAN KLINIS REHABILITASI MEDIK' "
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
            SQL &= ",NAMAFORMULIR = 'LAYANAN KEDOKTERAN FISIK DAN REHABILITASI' "
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
            SQL &= ",NAMAFORMULIR = 'ASESMEN AWAL MEDIS PASIEN PARU' "
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
            SQL &= ",NAMAFORMULIR = 'ASESMEN AWAL MEDIS PASIEN GIZI' "
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
            SQL &= ",NAMAFORMULIR = 'ASESMEN AWAL MEDIS PASIEN PSIKIATRIK' "
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
            SQL &= ",NAMAFORMULIR = 'ASESMEN AWAL MEDIS PASIEN TUMBUH KEMBANG' "
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
            SQL &= ",NAMAFORMULIR = 'ASESMEN AWAL MEDIS PASIEN ANAK' "
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
            SQL &= ",NAMAFORMULIR = 'ASESMEN AWAL MEDIS RAWAT JALAN' "
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

        grvAsesmenMedis_PDF.Columns("DESCRIPTION").Visible = False
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
        Try
            PdfViewerAsesmenMedis.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Lembar Uji Fungsi.....")

            Dim Alamat1 As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & "1.pdf"
            Dim Alamat2 As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & "2.pdf"
            Dim arrfname2 As New List(Of String)()

            Dim oGrouperDataCppt As New Grouper.clsR_CPPT

            Dim ds = oGrouperDataCppt.GetDataByKdKunjunganCPPTDokter(Kode)

            If ds IsNot Nothing Then
                Dim rpt As New xtraLembarUjiFungsiRehabResumeNew

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.bindingSource.DataSource = ds
                rpt.ExportToPdf(Alamat1)

                If FileIO.FileSystem.FileExists(Alamat1) Then
                    arrfname2.Add(Alamat1)
                End If

                Dim rpt1 As New xtraLembarUjiFungsiRehab

                rpt1.ShowPrintMarginsWarning = False
                rpt1.Watermark.Text = sWATERMARK

                rpt1.bindingSource.DataSource = ds
                rpt1.ExportToPdf(Alamat2)

                If FileIO.FileSystem.FileExists(Alamat2) Then
                    arrfname2.Add(Alamat2)
                End If
            End If

            Dim sinputFiles2() As String = {}
            For Each ifname In arrfname2
                sinputFiles2 = AppendArray(sinputFiles2, ifname)
            Next

            Dim AlamatMerge As String = FolderSimpan & Now.ToString("yyyyMMddHHmmss") & "XZHASIL" & ".pdf"
            MergePdfFiles(sinputFiles2, AlamatMerge)

            If System.IO.File.Exists(AlamatMerge) Then
                PdfViewerAsesmenMedis.LoadDocument(AlamatMerge)
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Asesmen Medis" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub xtraReportAsesmenAwalMedisRawatJalan(ByVal Kode As String)
        Try
            PdfViewerAsesmenMedis.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Medis.....")

            Dim dataList As New List(Of Byte())
            Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim oDigital As New EMedrek.clsDigital_AsesmenMedisRawatJalan

            Dim ds = oDigital.GetData(Kode)

            If ds IsNot Nothing Then
                Dim rpt As New xtraReportAsmedRajal

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                'sAttacment_1 = ds.SDIGITALRJ33_15

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
    Private Sub xtraReportLaporanOperasi(ByVal Kode As String)
        Try
            PdfViewerAsesmenMedis.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Laporan Operasi.....")

            Dim dataList As New List(Of Byte())
            Dim Alamat As String = FolderSimpan & Kode & Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim oDigital As New EMedrek.clsS_DIGITAL_OK_LAPORANOPERASI

            Dim ds = oDigital.GetDataKode(Kode)

            Dim Odaftar As New Admission.clsPendaftaran

            If ds IsNot Nothing Then
                If ds.ATTACHMENT_1 Is Nothing Then
                    Dim dsPendaftaran = Odaftar.GetData(ds.KDPENDAFTARAN)

                    If dsPendaftaran IsNot Nothing Then
                        NAMA = dsPendaftaran.M_CUSTOMER.NAME_DISPLAY
                        JENISKELAMIN = IIf(dsPendaftaran.M_CUSTOMER.KDJENISKELAMIN = "1", "L", "P")
                        TANGGALLAHIR = dsPendaftaran.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy")
                        sKDUSER_TTD = ds.KDUSER
                        USIA = frmRawatInapList.GetUmurPasien(ds.DATE, dsPendaftaran.M_CUSTOMER.TANGGALLAHIR)
                    End If

                    Dim rpt As New xtraReportLAPORANOPERASI

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK

                    'sAttacment_1 = ds.SDIGITALRJ33_15

                    rpt.BindingSource1.DataSource = ds
                    rpt.ExportToPdf(Alamat)

                    If FileIO.FileSystem.FileExists(Alamat) Then
                        PdfViewerAsesmenMedis.LoadDocument(Alamat)
                    End If
                Else
                    Dim dsPendaftaran = Odaftar.GetData(ds.KDPENDAFTARAN)

                    If dsPendaftaran IsNot Nothing Then
                        NAMA = dsPendaftaran.M_CUSTOMER.NAME_DISPLAY
                        JENISKELAMIN = IIf(dsPendaftaran.M_CUSTOMER.KDJENISKELAMIN = "1", "L", "P")
                        TANGGALLAHIR = dsPendaftaran.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy")
                        sKDUSER_TTD = ds.KDUSER
                        USIA = frmRawatInapList.GetUmurPasien(ds.DATE, dsPendaftaran.M_CUSTOMER.TANGGALLAHIR)
                    End If

                    Dim rpt As New xtraReportLAPORANOPERASI_Image

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK

                    'sAttacment_1 = ds.SDIGITALRJ33_15

                    rpt.BindingSource1.DataSource = ds
                    rpt.ExportToPdf(Alamat)

                    If FileIO.FileSystem.FileExists(Alamat) Then
                        PdfViewerAsesmenMedis.LoadDocument(Alamat)
                    End If
                End If

            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Asesmen Medis" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function AppendArray(Of T)(ByVal thisArray() As T, ByVal itemToAppend As T) As T()
        If thisArray Is Nothing Then thisArray = New T() {}
        Dim tempList As List(Of T) = thisArray.ToList
        tempList.Add(itemToAppend)
        Return tempList.ToArray
    End Function
    Public Function MergePdfFiles(ByVal pdfFiles() As String, ByVal outputPath As String) As Boolean
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

        tabControlDokumen_SelectedPageChanged()
        lPanelAsesmenMedis_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lPanelAsesmenMedis_Panel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lAsesmenMedis_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If Not frmAsesmenAwalMedisIGD2 Is Nothing Then frmAsesmenAwalMedisIGD2.Dispose()
        frmAsesmenAwalMedisIGD2 = Nothing

        If Not frmAsesmenMedisRawatJalan Is Nothing Then frmAsesmenMedisRawatJalan.Dispose()
        frmAsesmenMedisRawatJalan = Nothing

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

        If Not frmRingkasanKeluar Is Nothing Then frmRingkasanKeluar.Dispose()
        frmRingkasanKeluar = Nothing

        If Not frmLaporanOperasi Is Nothing Then frmLaporanOperasi.Dispose()
        frmLaporanOperasi = Nothing
    End Sub
    Private Sub btnAsesmenMedis_Back_Click(sender As Object, e As EventArgs) Handles btnAsesmenMedis_Back.Click
        fn_EmptyAsesmenMedis()
        XtraTabControl1_SelectedPageChanged()
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
        If txtKDKUNJUNGAN.Text = "" Then
            MsgBox("Silahkan Pilih Dulu Pasien", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grdKDFORMULIRMEDIS.Text = "" Then
            MsgBox("Formulir Belum di Pilih", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        Dim oDataGrouper As New Admission.clsPendaftaran_Kunjungan

        Dim ds = oDataGrouper.GetDatabyKodeKunjungan(txtKDKUNJUNGAN.Text)

        If ds IsNot Nothing Then
            Try
                pnlAsesmenMedis_pic.Dock = DockStyle.None

                lPanelAsesmenMedis_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lPanelAsesmenMedis_Panel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lAsesmenMedis_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                If grdKDFORMULIRMEDIS.Text = "ASESMEN AWAL MEDIS IGD" Then
                    Dim dsKunjungan = oDataGrouper.GetData(ds.KDKUNJUNGAN)
                    If dsKunjungan IsNot Nothing Then
                        Dim oS_DIGITAL_IGD_01 As New Transaksi.clsDigital_IGD_01
                        Dim dsDisgital = oS_DIGITAL_IGD_01.GetDataByPendaftaran(dsKunjungan.KDPENDAFTARAN)

                        If dsDisgital Is Nothing Then
                            ' Bersihkan konten panel
                            pnlAsemenMedis_Transaksi.Controls.Clear()

                            ' Set properti form anak
                            frmAsesmenAwalMedisIGD2.TopLevel = False
                            frmAsesmenAwalMedisIGD2.FormBorderStyle = FormBorderStyle.None
                            frmAsesmenAwalMedisIGD2.Dock = DockStyle.Fill

                            ' Tambahkan form anak ke panel
                            pnlAsemenMedis_Transaksi.Controls.Add(frmAsesmenAwalMedisIGD2)

                            ' Tampilkan form anak
                            frmAsesmenAwalMedisIGD2.LoadMe(sAutoOrderObat, "", ds.KDIDENTITAS, dsKunjungan.KDPENDAFTARAN, ds.KDKUNJUNGAN, ds.KDDOCTOR, ds.KDCUSTOMER, ds.NAMAPASIEN, ds.TANGGALLAHIR)
                            frmAsesmenAwalMedisIGD2.Show()
                        Else
                            MsgBox("Formulir Sudah dibuatkan, Silahkan lakukan Edit untuk memperbaiki", MsgBoxStyle.Exclamation, Me.Text)
                        End If
                    Else
                        MsgBox("Kunjungan Tidak di Temukan", MsgBoxStyle.Exclamation, Me.Text)
                    End If
                ElseIf grdKDFORMULIRMEDIS.Text = "LAPORAN OPERASI"
                    ' Bersihkan konten panel
                    pnlAsemenMedis_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmLaporanOperasi.TopLevel = False
                    frmLaporanOperasi.FormBorderStyle = FormBorderStyle.None
                    frmLaporanOperasi.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsemenMedis_Transaksi.Controls.Add(frmLaporanOperasi)

                    ' Tampilkan form anak
                    frmLaporanOperasi.LoadMe(FORM_MODE.FORM_MODE_ADD, "", grdDPJP.EditValue, ds.KDDOCTOR, ds.KDCUSTOMER, oDataGrouper.GetData(ds.KDKUNJUNGAN).KDPENDAFTARAN)
                    frmLaporanOperasi.Show()
                ElseIf grdKDFORMULIRMEDIS.Text = "RESUME RAWAT INAP"
                    Dim oDigital As New EMedrek.clsRingkasanKeluar

                    Dim dsDigital = oDigital.GetData(grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("Kode"))

                    If dsDigital IsNot Nothing Then
                        MsgBox("Formulir Sudah dibuatkan, Silahkan lakukan Edit untuk memperbaiki", MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                    End If

                    ' Bersihkan konten panel
                    pnlAsemenMedis_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmRingkasanKeluar.TopLevel = False
                    frmRingkasanKeluar.FormBorderStyle = FormBorderStyle.None
                    frmRingkasanKeluar.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsemenMedis_Transaksi.Controls.Add(frmRingkasanKeluar)

                    ' Tampilkan form anak
                    frmRingkasanKeluar.LoadMe(FORM_MODE.FORM_MODE_ADD, oDataGrouper.GetData(ds.KDKUNJUNGAN).KDPENDAFTARAN, ds.KDIDENTITAS)
                    frmRingkasanKeluar.Show()
                ElseIf grdKDFORMULIRMEDIS.Text = "CATATAN MEDIS AWAL RAWAT INAP DAN DISCHARGE PLANNING" Then
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
                ElseIf grdKDFORMULIRMEDIS.Text = "ASESMEN AWAL MEDIS PASIEN PARU" Then
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
                ElseIf grdKDFORMULIRMEDIS.Text = "OK - KARTU ANASTESI A" Then
                    If MsgBox("Tekan Yes Tidak Ke Panel", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then
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
                    Else
                        Dim oDigital As New EMedrek.clsS_DIGITAL_OK_KARTU_ANESTESI_A
                        Dim dsDigital = oDigital.GetDataByKdpendaftaran(ds.KDKUNJUNGAN)

                        If dsDigital IsNot Nothing Then
                            MsgBox("Formulir Sudah dibuatkan, Silahkan lakukan Edit untuk memperbaiki", MsgBoxStyle.Exclamation, Me.Text)
                            Exit Sub
                        End If

                        Dim frmKartuAnastesi_A As New frmKartuAnastesi_A
                        Try
                            'frmKartuAnastesi_A.fn_KeluarAuoto(True)
                            frmKartuAnastesi_A.LoadMe(FORM_MODE.FORM_MODE_ADD, ds.KDKUNJUNGAN)
                            frmKartuAnastesi_A.ShowDialog(Me)
                        Catch oErr As Exception
                            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                        Finally
                            If Not frmKartuAnastesi_A Is Nothing Then frmKartuAnastesi_A.Dispose()
                            frmKartuAnastesi_A = Nothing
                        End Try
                    End If
                ElseIf grdKDFORMULIRMEDIS.Text = "OK - KARTU ANASTESI B" Then
                    If MsgBox("Tekan Yes Tidak Ke Panel", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then
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
                    Else
                        Dim oDigital As New EMedrek.clsS_DIGITAL_OK_KARTU_ANESTESI_B
                        Dim dsDigital = oDigital.GetDataByKdpendaftaran(ds.KDKUNJUNGAN)

                        If dsDigital IsNot Nothing Then
                            MsgBox("Formulir Sudah dibuatkan, Silahkan lakukan Edit untuk memperbaiki", MsgBoxStyle.Exclamation, Me.Text)
                            Exit Sub
                        End If

                        Dim frmKartuAnastesi_B As New frmKartuAnastesi_B
                        Try
                            'frmKartuAnastesi_B.fn_KeluarAuoto(True)
                            frmKartuAnastesi_B.LoadMe(FORM_MODE.FORM_MODE_ADD, ds.KDKUNJUNGAN)
                            frmKartuAnastesi_B.ShowDialog(Me)
                        Catch oErr As Exception
                            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                        Finally
                            If Not frmKartuAnastesi_B Is Nothing Then frmKartuAnastesi_B.Dispose()
                            frmKartuAnastesi_B = Nothing
                        End Try
                    End If
                ElseIf grdKDFORMULIRMEDIS.Text = "ASESMEN AWAL MEDIS PASIEN PENYAKIT DALAM" Then
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
                ElseIf grdKDFORMULIRMEDIS.Text = "ASESMEN AWAL MEDIS PASIEN BEDAH" Then
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
                ElseIf grdKDFORMULIRMEDIS.Text = "ASESMEN AWAL MEDIS PASIEN JANTUNG" Then
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
                ElseIf grdKDFORMULIRMEDIS.Text = "ASESMEN AWAL MEDIS PASIEN NEUROLOGI" Then
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
                ElseIf grdKDFORMULIRMEDIS.Text = "ASESMEN AWAL MEDIS PASIEN GIGI" Then
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
                ElseIf grdKDFORMULIRMEDIS.Text = "ASESMEN AWAL MEDIS PASIEN PSIKIATRIK" Then
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
                ElseIf grdKDFORMULIRMEDIS.Text = "ASESMEN AWAL MEDIS OBSTETRI & GYNEKOLOGI" Then
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
                ElseIf grdKDFORMULIRMEDIS.Text = "ASESMEN AWAL MEDIS PASIEN MATA" Then
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
                ElseIf grdKDFORMULIRMEDIS.Text = "UJI FUNGSI, LAYANAN & PROSEDUR KEDOKTERAN FISIK & REHABILITASI" Then
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
                ElseIf grdKDFORMULIRMEDIS.Text = "FORMULIR CATATAN KLINIS REHABILITASI MEDIK" Then
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
                ElseIf grdKDFORMULIRMEDIS.Text = "LAYANAN KEDOKTERAN FISIK DAN REHABILITASI" Then
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
                ElseIf grdKDFORMULIRMEDIS.Text = "ASESMEN AWAL MEDIS PASIEN GIZI" Then
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
                ElseIf grdKDFORMULIRMEDIS.Text = "ASESMEN AWAL MEDIS PASIEN TUMBUH KEMBANG" Then
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
                ElseIf grdKDFORMULIRMEDIS.Text = "ASESMEN AWAL MEDIS PASIEN ANAK" Then
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
                ElseIf grdKDFORMULIRMEDIS.Text = "ASESMEN AWAL MEDIS PASIEN KULIT DAN KELAMIN" Then
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
                ElseIf grdKDFORMULIRMEDIS.Text = "ASESMEN AWAL MEDIS PASIEN THT" Then
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
                ElseIf grdKDFORMULIRMEDIS.Text = "ASESMEN AWAL MEDIS NEONATUS" Then
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
                ElseIf grdKDFORMULIRMEDIS.Text = "ASESMEN AWAL MEDIS RAWAT JALAN" Then
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
                ElseIf grdKDFORMULIRMEDIS.Text = "ORDER RESEP"
                    Dim frmOrderRanapNonRacikanList As New frmOrderRanapNonRacikanList
                    Try
                        'Dim listdiagnosa As New List(Of String)
                        'For i As Integer = 0 To grvDIAGNOSA_CPPT.RowCount - 2
                        '    listdiagnosa.Add(grvDIAGNOSA_CPPT.GetRowCellValue(i, colNAMADIAGNOSA_CPPT))
                        'Next
                        Dim dsDaftar = oPendaftaran.GetData(ds.KDPENDAFTARAN)
                        If dsDaftar IsNot Nothing Then
                            frmOrderRanapNonRacikanList.fn_LoadData(ds.KDIDENTITAS, ds.KDPENDAFTARAN, dsDaftar.M_DIAGNOSA.MEMO, "", "")
                            frmOrderRanapNonRacikanList.ShowDialog(Me)
                        End If
                    Catch oErr As Exception
                        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    Finally
                        If Not frmOrderRanapNonRacikanList Is Nothing Then frmOrderRanapNonRacikanList.Dispose()
                        frmOrderRanapNonRacikanList = Nothing
                    End Try
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

        'If grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDUSER") <> sUserID Then
        '    MsgBox("Maap Tidak dapat Rubah Silahkan Hubungi User " & grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDUSER"), MsgBoxStyle.Exclamation, Me.Text)
        '    Exit Sub
        'End If

        Dim oDataGrouper As New Admission.clsPendaftaran_Kunjungan

        Dim ds = oDataGrouper.GetDatabyKodeKunjungan(txtKDKUNJUNGAN.Text)

        If ds Is Nothing Then
            MsgBox("Identitas Kosong", MsgBoxStyle.Exclamation, Me.Text)
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
                    frmAsesmenAwalMedisIGD2.LoadMe(sAutoOrderObat, dsDigital.KODE, ds.KDIDENTITAS, dsDigital.KDPENDAFTARAN, txtKDKUNJUNGAN.Text, dsDigital.DOCTOR_KODE, dsDigital.KDCUSTOMER, txtNAMAPASIEN.Text, deDATETANGGALLAHIR.DateTime)
                    frmAsesmenAwalMedisIGD2.Show()
                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
            End If
        ElseIf grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") = "LAPORAN OPERASI"
            Dim oDigital As New EMedrek.clsS_DIGITAL_OK_LAPORANOPERASI

            Dim dsDigital = oDigital.GetDataKode(grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR"))

            If dsDigital IsNot Nothing Then
                Try
                    ' Bersihkan konten panel
                    pnlAsemenMedis_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmLaporanOperasi.TopLevel = False
                    frmLaporanOperasi.FormBorderStyle = FormBorderStyle.None
                    frmLaporanOperasi.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsemenMedis_Transaksi.Controls.Add(frmLaporanOperasi)

                    ' Tampilkan form anak
                    frmLaporanOperasi.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.KDLAPORANOPERASI, grdDPJP.EditValue, dsDigital.KDDOCTOR, dsDigital.KDCUSTOMER, dsDigital.KDPENDAFTARAN)
                    frmLaporanOperasi.Show()
                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
            End If
        ElseIf grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") = "RESUME RAWAT INAP"
            Dim oDigital As New EMedrek.clsRingkasanKeluar

            Dim dsDigital = oDigital.GetData(grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR"))

            If dsDigital IsNot Nothing Then
                Try
                    ' Bersihkan konten panel
                    pnlAsemenMedis_Transaksi.Controls.Clear()

                    ' Set properti form anak
                    frmRingkasanKeluar.TopLevel = False
                    frmRingkasanKeluar.FormBorderStyle = FormBorderStyle.None
                    frmRingkasanKeluar.Dock = DockStyle.Fill

                    ' Tambahkan form anak ke panel
                    pnlAsemenMedis_Transaksi.Controls.Add(frmRingkasanKeluar)

                    ' Tampilkan form anak
                    frmRingkasanKeluar.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.KDREG, dsDigital.KDIDENTITAS)
                    frmRingkasanKeluar.Show()
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
                    frmDischargePlanning.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.ANAMNESIS_23, dsDigital.ANAMNESIS_24, dsDigital.DOCTOR_KODE, dsDigital.KDCUSTOMER, txtNAMAPASIEN.Text, dsDigital.KDREG)
                    frmDischargePlanning.Show()
                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
            End If
        ElseIf grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") = "OK - KARTU ANASTESI A" Then
            If MsgBox("Tekan Yes Tidak Ke Panel", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then
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
            Else
                Dim oDigital As New EMedrek.clsS_DIGITAL_OK_KARTU_ANESTESI_A
                Dim dsDigital = oDigital.GetData(grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR"))

                Dim frmKartuAnastesi_A As New frmKartuAnastesi_A
                Try
                    'frmKartuAnastesi_A.fn_KeluarAuoto(True)
                    frmKartuAnastesi_A.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDKUNJUNGAN, dsDigital.KODE)
                    frmKartuAnastesi_A.ShowDialog(Me)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                Finally
                    If Not frmKartuAnastesi_A Is Nothing Then frmKartuAnastesi_A.Dispose()
                    frmKartuAnastesi_A = Nothing
                End Try
            End If

        ElseIf grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") = "OK - KARTU ANASTESI B" Then
            If MsgBox("Tekan Yes Tidak Ke Panel", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then
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
            Else
                Dim oDigital As New EMedrek.clsS_DIGITAL_OK_KARTU_ANESTESI_B
                Dim dsDigital = oDigital.GetData(grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR"))

                Dim frmKartuAnastesi_B As New frmKartuAnastesi_B
                Try
                    'frmKartuAnastesi_B.fn_KeluarAuoto(True)
                    frmKartuAnastesi_B.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDKUNJUNGAN, dsDigital.KODE)
                    frmKartuAnastesi_B.ShowDialog(Me)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                Finally
                    If Not frmKartuAnastesi_B Is Nothing Then frmKartuAnastesi_B.Dispose()
                    frmKartuAnastesi_B = Nothing
                End Try
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
            'ElseIf grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") = "UJI FUNGSI, LAYANAN & PROSEDUR KEDOKTERAN FISIK & REHABILITASI" Then
            '    Dim oDigital As New EMedrek.clsFisioterafi_1
            '    Dim dsDigital = oDigital.GetData(grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR"))

            '    If dsDigital IsNot Nothing Then
            '        Try
            '            ' Bersihkan konten panel
            '            pnlAsemenMedis_Transaksi.Controls.Clear()

            '            ' Set properti form anak
            '            frmLembarUjiFungsiRehab.TopLevel = False
            '            frmLembarUjiFungsiRehab.FormBorderStyle = FormBorderStyle.None
            '            frmLembarUjiFungsiRehab.Dock = DockStyle.Fill

            '            ' Tambahkan form anak ke panel
            '            pnlAsemenMedis_Transaksi.Controls.Add(frmLembarUjiFungsiRehab)

            '            ' Tampilkan form anak
            '            frmLembarUjiFungsiRehab.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsDigital.KDKUNJUNGAN)
            '            frmLembarUjiFungsiRehab.Show()
            '        Catch ex As Exception
            '            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            '        End Try
            '    Else
            '        MsgBox("Data Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
            '    End If
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

                        Dim oDataGrouper1 As New Admission.clsPendaftaran_Kunjungan
                        Dim sKDDOCTOR As String = String.Empty
                        Dim ds1 = oDataGrouper1.GetDatabyKodeKunjungan(txtKDKUNJUNGAN.Text)
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

        'MsgBox("Maap Belum Tersedia Fungsi Delete", MsgBoxStyle.Exclamation, Me.Text)

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
                    MsgBox("gagal hapus", MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                End If

                Dim oDigital As New Transaksi.clsDigital_IGD_01
                oDigital.DeleteData(grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR"), sPesanHapus)
                XtraTabControl1_SelectedPageChanged()
                tabControlList_SelectedPageChanged()
            ElseIf grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") = "CATATAN MEDIS AWAL RAWAT INAP DAN DISCHARGE PLANNING" Then
                Dim oDelete As New Setting.clsDelete

                If oDelete.InsertData("DELETEASESMENAWALMEDIS", sUserID, grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") & " " & sPesanHapus & " " & Now.ToString("dd-MM-yyyy HH:mm:ss"), grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR")) = False Then
                    MsgBox("gagal hapus", MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                End If

                Dim oDigital As New EMedrek.clsDigital_DischargePlanning

                If oDigital.fn_SaveDelete(grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR")) = True Then
                    oDigital.DeleteData(grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR"), sUserID)
                    XtraTabControl1_SelectedPageChanged()
                    tabControlList_SelectedPageChanged()
                Else
                    MsgBox("gagal hapus", MsgBoxStyle.Exclamation, Me.Text)
                End If

            ElseIf grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") = "OK - KARTU ANASTESI A" Then
                Dim oDelete As New Setting.clsDelete
                Dim oDigital As New EMedrek.clsS_DIGITAL_OK_KARTU_ANESTESI_A

                If oDelete.InsertData("DELETEASESMENAWALMEDIS", sUserID, grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") & " " & sPesanHapus & " " & Now.ToString("dd-MM-yyyy HH:mm:ss"), grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR")) = False Then
                    MsgBox("gagal hapus", MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                End If

                oDigital.DeleteData(grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR"), sUserID)
                XtraTabControl1_SelectedPageChanged()
                tabControlList_SelectedPageChanged()
            ElseIf grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") = "OK - KARTU ANASTESI B" Then
                Dim oDelete As New Setting.clsDelete
                Dim oDigital As New EMedrek.clsS_DIGITAL_OK_KARTU_ANESTESI_B

                If oDelete.InsertData("DELETEASESMENAWALMEDIS", sUserID, grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("DESCRIPTION") & " " & sPesanHapus & " " & Now.ToString("dd-MM-yyyy HH:mm:ss"), grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR")) = False Then
                    MsgBox("gagal hapus", MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                End If

                oDigital.DeleteData(grvAsesmenMedis_Transaksi.GetFocusedRowCellValue("KDFORMULIR"), sUserID)
                XtraTabControl1_SelectedPageChanged()
                tabControlList_SelectedPageChanged()
            Else
                MsgBox("Maap Belum Tersedia Fungsi Delete", MsgBoxStyle.Exclamation, Me.Text)
            End If
        End If
    End Sub
    Private Sub picAsesmenAwalMedis_Refresh_Click(sender As Object, e As EventArgs) Handles picAsesmenAwalMedis_Refresh.Click
        tabControlDokumen_SelectedPageChanged()
        lPanelAsesmenMedis_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lPanelAsesmenMedis_Panel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lAsesmenMedis_btnBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If Not frmAsesmenAwalMedisIGD2 Is Nothing Then frmAsesmenAwalMedisIGD2.Dispose()
        frmAsesmenAwalMedisIGD2 = Nothing

        If Not frmAsesmenMedisRawatJalan Is Nothing Then frmAsesmenMedisRawatJalan.Dispose()
        frmAsesmenMedisRawatJalan = Nothing

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

        If Not frmRingkasanKeluar Is Nothing Then frmRingkasanKeluar.Dispose()
        frmRingkasanKeluar = Nothing

        If Not frmLaporanOperasi Is Nothing Then frmLaporanOperasi.Dispose()
        frmLaporanOperasi = Nothing
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
            ElseIf grvAsesmenMedis_PDF.GetFocusedRowCellValue("DESCRIPTION") = "OK - KARTU ANASTESI A" Then
                xtraReportKartuAnastesiA(grvAsesmenMedis_PDF.GetFocusedRowCellValue("KDFORMULIR"))
            ElseIf grvAsesmenMedis_PDF.GetFocusedRowCellValue("DESCRIPTION") = "OK - KARTU ANASTESI B" Then
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
                xtraReportAsesmenAwalMedisRawatJalan(grvAsesmenMedis_PDF.GetFocusedRowCellValue("KDFORMULIR"))
            ElseIf grvAsesmenMedis_PDF.GetFocusedRowCellValue("DESCRIPTION") = "LAPORAN OPERASI" Then
                xtraReportLaporanOperasi(grvAsesmenMedis_PDF.GetFocusedRowCellValue("KDFORMULIR"))
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
#End Region
#Region "Aksi"
#Region "Function"
    Private Sub fn_LoadDataAksiBilling()
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
            SQL &= "A.CATEGORY "
            SQL &= ",A.KDSOTRANSAKSI "
            SQL &= ",TANGGALTRANSKASI = A.DATE "
            SQL &= ",TINDAKAN = E.NMITEM2 "
            SQL &= ",Total = D.GRANDTOTAL "
            SQL &= ",A.KDUSER "
            SQL &= ",D.SEQ "
            SQL &= ",Bayar = CASE A.GRANDTOTAL WHEN 0 THEN CONVERT(BIT, 0) ELSE CASE WHEN A.PAYAMOUNT = A.GRANDTOTAL THEN CONVERT(BIT, 1) ELSE CONVERT(BIT, 0) END END "
            SQL &= "FROM S_SO_TRANSAKSI_H A "
            SQL &= "INNER JOIN S_PENDAFTARAN_KUNJUNGAN B "
            SQL &= "ON A.KDKUNJUNGAN = B.KDKUNJUNGAN "
            SQL &= "INNER JOIN S_PENDAFTARAN_H C "
            SQL &= "ON B.KDPENDAFTARAN = C.KDPENDAFTARAN "
            SQL &= "INNER JOIN S_SO_TRANSAKSI_D D "
            SQL &= "ON A.KDSOTRANSAKSI = D.KDSOTRANSAKSI "
            SQL &= "INNER JOIN M_ITEM E "
            SQL &= "ON D.KDITEM = E.KDITEM "
            SQL &= "WHERE B.KDPENDAFTARAN = '" & sKDPENDAFTARAN & "' "
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

            fn_LoadFormatDataAksiBilling()

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatDataAksiBilling()
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

        grvBilling.Columns("CATEGORY").VisibleIndex = -1
        grvBilling.Columns("KDSOTRANSAKSI").VisibleIndex = -1
        grvBilling.Columns("Bayar").VisibleIndex = -1
        grvBilling.Columns("SEQ").VisibleIndex = -1
    End Sub
    Private Sub LoadPenunjang(ByVal kdreg As String)
        Try
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
            SQL &= "KODE = B.KDORDER  "
            SQL &= ",TanggalOrder = B.TANGGALORDER "
            SQL &= ",Penunjang = B.MEMO "
            SQL &= ",JenisPemeriksaan = C.NAMATINDAKAN "
            SQL &= ",Dokter = B.USERORDER "
            SQL &= ",Keterangan = B.STATUS "
            SQL &= "FROM "
            SQL &= "A_IDENTITASPASIEN_LIST A "
            SQL &= "INNER JOIN R_ORDER B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "INNER JOIN R_ORDER_RANAPLAB C "
            SQL &= "ON B.KDORDER = C.KDORDER "
            SQL &= "WHERE "
            SQL &= "A.KDPENDAFTARAN = '" & kdreg & "' "
            SQL &= "AND B.MEMO = 'ORDER LABORATORIUM' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KODE = B.KDORDER  "
            SQL &= ",TanggalOrder = B.TANGGALORDER "
            SQL &= ",Penunjang = B.MEMO "
            SQL &= ",JenisPemeriksaan = C.NAMATINDAKAN "
            SQL &= ",Dokter = B.USERORDER "
            SQL &= ",Keterangan = B.STATUS "
            SQL &= "FROM "
            SQL &= "A_IDENTITASPASIEN_LIST A "
            SQL &= "INNER JOIN R_ORDER B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "INNER JOIN R_ORDER_RANAPRAD C "
            SQL &= "ON B.KDORDER = C.KDORDER "
            SQL &= "WHERE "
            SQL &= "A.KDPENDAFTARAN = '" & kdreg & "' "
            SQL &= "AND B.MEMO = 'ORDER RADIOLOGI' "

            'SQL &= "ORDER BY B.TANGGALORDER "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ORDERPENUNJANG")

            grdPenunjang.DataSource = ds.Tables("ORDERPENUNJANG")
            grdPenunjang.ForceInitialize()


            Dim listPlanningTindakanPenunjang As New List(Of String)

            For iLoop As Integer = 0 To ds.Tables("ORDERPENUNJANG").Rows.Count - 1
                With ds.Tables("ORDERPENUNJANG")
                    listPlanningTindakanPenunjang.Add("(" & CDate(.Rows(iLoop)("TanggalOrder").ToString()).ToString("dd-MM-yyyy") & ") " & .Rows(iLoop)("JenisPemeriksaan").ToString())
                End With
            Next

            sPenunjnag = String.Join(vbCrLf, listPlanningTindakanPenunjang.ToArray)

            For iLoop As Integer = 0 To grvPenunjang.Columns.Count - 1
                If grvPenunjang.Columns(iLoop).ColumnType.Name = "Decimal" Then
                    grvPenunjang.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    grvPenunjang.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                    grvPenunjang.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ElseIf grvPenunjang.Columns(iLoop).ColumnType.Name = "DateTime" Then
                    grvPenunjang.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                    grvPenunjang.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm}"
                End If
            Next

            grvPenunjang.Columns("KODE").VisibleIndex = -1

            grvPenunjang.BestFitColumns()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox("Load Penunjang: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnOrderLaboratorium_Click(sender As Object, e As EventArgs) Handles btnOrderLaboratorium.Click
        Dim frmOrderRanapLab As New frmOrderRanapLab

        Try
            If txtOBJEKTIF_BERATBADAN.Text = "" Then
                MsgBox("Dibutuhkan Berat Badan", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            If txtOBJEKTIF_TINGGIBADAN.Text = "" Then
                MsgBox("Dibutuhkan Tinggi Badan", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            Dim listdiagnosa As New List(Of String)

            For i As Integer = 0 To grvDiagnosaPenyerta.RowCount - 2
                listdiagnosa.Add(grvDiagnosaPenyerta.GetRowCellValue(i, colKETERANGAN))
            Next

            'If listdiagnosa.Count <= 0 Then
            '    MsgBox("Dibutuhkan Diagnosa", MsgBoxStyle.Exclamation, Me.Text)
            '    Exit Sub
            'End If

            Dim dsIdentitas = oGrouperDataCppt.GetDataByKdKunjungan(txtKDKUNJUNGAN.Text)
            If dsIdentitas IsNot Nothing Then
                frmOrderRanapLab.LoadMe(FORM_MODE.FORM_MODE_ADD, dsIdentitas.KDIDENTITAS, String.Join(", ", listdiagnosa.ToArray), txtOBJEKTIF_BERATBADAN.Text, txtOBJEKTIF_TINGGIBADAN.Text)
                frmOrderRanapLab.ShowDialog(Me)

                LoadPenunjang(sKDPENDAFTARAN)
            Else
                MsgBox("Identitas Kosong", MsgBoxStyle.Exclamation, Me.Text)
            End If

        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmOrderRanapLab Is Nothing Then frmOrderRanapLab.Dispose()
            frmOrderRanapLab = Nothing
        End Try
    End Sub
    Private Sub btnOrderRadiologi_Click(sender As Object, e As EventArgs) Handles btnOrderRadiologi.Click
        Dim frmOrderRanapRad As New frmOrderRanapRad

        Try
            If txtOBJEKTIF_BERATBADAN.Text = "" Then
                MsgBox("Dibutuhkan Berat Badan", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            If txtOBJEKTIF_TINGGIBADAN.Text = "" Then
                MsgBox("Dibutuhkan Tinggi Badan", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            Dim listdiagnosa As New List(Of String)

            For i As Integer = 0 To grvDiagnosaPenyerta.RowCount - 2
                listdiagnosa.Add(grvDiagnosaPenyerta.GetRowCellValue(i, colKETERANGAN))
            Next

            'If listdiagnosa.Count <= 0 Then
            '    MsgBox("Dibutuhkan Diagnosa", MsgBoxStyle.Exclamation, Me.Text)
            '    Exit Sub
            'End If

            Dim dsIdentitas = oGrouperDataCppt.GetDataByKdKunjungan(txtKDKUNJUNGAN.Text)
            If dsIdentitas IsNot Nothing Then
                frmOrderRanapRad.LoadMe(FORM_MODE.FORM_MODE_ADD, dsIdentitas.KDIDENTITAS, String.Join(", ", listdiagnosa.ToArray), txtOBJEKTIF_BERATBADAN.Text, txtOBJEKTIF_TINGGIBADAN.Text)
                frmOrderRanapRad.ShowDialog(Me)

                LoadPenunjang(sKDPENDAFTARAN)
            Else
                MsgBox("Identitas Kosong", MsgBoxStyle.Exclamation, Me.Text)
            End If

        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmOrderRanapRad Is Nothing Then frmOrderRanapRad.Dispose()
            frmOrderRanapRad = Nothing
        End Try
    End Sub
#End Region

#Region "Command Button"
    Private Sub grvBilling_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grvBilling.DoubleClick
        If grvBilling.GetFocusedRowCellValue("KDSOTRANSAKSI") Is Nothing Then
            Exit Sub
        End If
        If grvBilling.GetFocusedRowCellValue("CATEGORY") = 2 Then
            MsgBox("Tidak dapat di Update Merupakan Penunjang", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grvBilling.GetFocusedRowCellValue("CATEGORY") = 3 Then
            MsgBox("Tidak dapat di Update Merupakan Penunjang", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grvBilling.GetFocusedRowCellValue("CATEGORY") = 4 Then
            MsgBox("Tidak dapat di Update Merupakan Penunjang", MsgBoxStyle.Exclamation, Me.Text)
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
        Dim frmSalesOrderTransaksi As New frmSalesOrderTransaksi
        Try
            Dim oKunjungan As New Admission.clsPendaftaran_Kunjungan
            Dim dsKunjungan = oKunjungan.GetData(txtKDKUNJUNGAN.Text)
            If dsKunjungan IsNot Nothing Then
                frmSalesOrderTransaksi.fn_loadKunjungan(txtKDKUNJUNGAN.Text, dsKunjungan.KDDOCTOR, dsKunjungan.S_PENDAFTARAN_H.CATEGORY)
                frmSalesOrderTransaksi.LoadMe(FORM_MODE.FORM_MODE_ADD, dsKunjungan.S_PENDAFTARAN_H.CATEGORY)
                frmSalesOrderTransaksi.ShowDialog(Me)
            Else
                frmSalesOrderTransaksi.LoadMe(FORM_MODE.FORM_MODE_ADD, sCategoryBilling)
                frmSalesOrderTransaksi.ShowDialog(Me)
                fn_LoadDataAksiBilling()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmSalesOrderTransaksi Is Nothing Then frmSalesOrderTransaksi.Dispose()
            frmSalesOrderTransaksi = Nothing
        End Try
    End Sub
    Private Sub picAksiBilling_Update_Click() Handles picAksiBilling_Update.Click
        If grvBilling.GetFocusedRowCellValue("KDSOTRANSAKSI") Is Nothing Then
            Exit Sub
        End If
        If grvBilling.GetFocusedRowCellValue("CATEGORY") = 2 Then
            MsgBox("Tidak dapat di Update Merupakan Penunjang", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grvBilling.GetFocusedRowCellValue("CATEGORY") = 3 Then
            MsgBox("Tidak dapat di Update Merupakan Penunjang", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grvBilling.GetFocusedRowCellValue("CATEGORY") = 4 Then
            MsgBox("Tidak dapat di Update Merupakan Penunjang", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If CBool(grvBilling.GetFocusedRowCellValue("Bayar")) = True Then
            MsgBox("Sudah Bayar", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        Else
            If sUPDATETRANSAKSI = False Then
                Dim ds = oSalesOrderTransaksi.GetData(grvBilling.GetFocusedRowCellValue("KDSOTRANSAKSI"))
                If ds IsNot Nothing Then
                    If ds.KDUSER <> sUserID Then
                        MsgBox("Tidak dapat Rubah Silahkan hubungi User " & ds.KDUSER, MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                    End If
                End If
            End If

            Dim frmSalesOrderTransaksi As New frmSalesOrderTransaksi
            Try
                frmSalesOrderTransaksi.LoadMe(FORM_MODE.FORM_MODE_EDIT, sCategoryBilling, grvBilling.GetFocusedRowCellValue("KDSOTRANSAKSI"))
                frmSalesOrderTransaksi.ShowDialog(Me)
                fn_LoadDataAksiBilling()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSalesOrderTransaksi Is Nothing Then frmSalesOrderTransaksi.Dispose()
                frmSalesOrderTransaksi = Nothing
            End Try
        End If
    End Sub
    Private Sub picAksiBilling_Delete_Click() Handles picAksiBilling_Delete.Click
        'If grvBilling.GetFocusedRowCellValue("KDSOTRANSAKSI") Is Nothing Then
        '    Exit Sub
        'End If
        'If grvBilling.GetFocusedRowCellValue("CATEGORY") = 2 Then
        '    MsgBox("Tidak dapat di Update Merupakan Penunjang", MsgBoxStyle.Exclamation, Me.Text)
        '    Exit Sub
        'End If
        'If grvBilling.GetFocusedRowCellValue("CATEGORY") = 3 Then
        '    MsgBox("Tidak dapat di Update Merupakan Penunjang", MsgBoxStyle.Exclamation, Me.Text)
        '    Exit Sub
        'End If
        'If grvBilling.GetFocusedRowCellValue("CATEGORY") = 4 Then
        '    MsgBox("Tidak dapat di Update Merupakan Penunjang", MsgBoxStyle.Exclamation, Me.Text)
        '    Exit Sub
        'End If
        'If CBool(grvBilling.GetFocusedRowCellValue("Bayar")) = True Then
        '    MsgBox("Sudah Bayar", MsgBoxStyle.Exclamation, Me.Text)
        '    Exit Sub
        'End If
        'If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        'Dim sKDORDER As String = String.Empty
        'Dim oSalesOrderTransaksi As New Sales.clsSalesOrderTransaksi
        'Dim ds = oSalesOrderTransaksi.GetData(grvBilling.GetFocusedRowCellValue("KDSOTRANSAKSI"))
        'If ds IsNot Nothing Then
        '    sKDORDER = ds.KDORDER
        'End If

        'If sKDORDER <> "" Then
        '    Dim oOrder As New Digital.clsR_Order
        '    Dim dsOrder = oOrder.GetData(sKDORDER)
        '    If dsOrder IsNot Nothing Then
        '        If dsOrder.STATUS = "DITERIMA" Then
        '            Dim oDigitalOrder As New Digital.clsR_Order
        '            oDigitalOrder.UpdateStatus(sKDORDER, "UPDATE")

        '            If fn_DeleteData(grv.GetFocusedRowCellValue("KDSOTRANSAKSI")) = False Then
        '                MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
        '                Exit Sub
        '            End If
        '        ElseIf dsOrder.STATUS = "TAMBAH" Then
        '            If fn_DeleteData(grv.GetFocusedRowCellValue("KDSOTRANSAKSI")) = False Then
        '                MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
        '                Exit Sub
        '            End If
        '        ElseIf dsOrder.STATUS = "UPDATE" Then
        '            If fn_DeleteData(grv.GetFocusedRowCellValue("KDSOTRANSAKSI")) = False Then
        '                MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
        '                Exit Sub
        '            End If
        '        ElseIf dsOrder.STATUS = "HASIL" Then
        '            MsgBox("Hasil Sudah di Input Tidak Dapat di Hapus/Rubah", MsgBoxStyle.Exclamation, Me.Text)
        '        ElseIf dsOrder.STATUS = "KIRIM" Then
        '            MsgBox("Hasil Sudah di Kirim Tidak Dapat di Hapus/Rubah", MsgBoxStyle.Exclamation, Me.Text)
        '        Else
        '            MsgBox("Hasil Sudah di " & dsOrder.STATUS & " Tidak Dapat di Hapus/Rubah", MsgBoxStyle.Exclamation, Me.Text)
        '        End If
        '    Else
        '        If fn_DeleteData(grv.GetFocusedRowCellValue("KDSOTRANSAKSI")) = False Then
        '            MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
        '            Exit Sub
        '        End If
        '    End If
        '    'Dim oDigitalOrder As New Digital.clsR_Order
        '    'oDigitalOrder.UpdateStatus(sFind2, "UPDATE")

        '    'Try
        '    '    If sCategoryBilling = 2 Then
        '    '        frmSalesOrderTransaksi.fn_UpdateOrderByKDCPPTTransaksi(sKDCPPT, "0", "LABORATORIUM")
        '    '    End If
        '    '    If sCategoryBilling = 3 Then
        '    '        frmSalesOrderTransaksi.fn_UpdateOrderByKDCPPTTransaksi(sKDCPPT, "0", "RADIOLOGI")
        '    '    End If
        '    '    If sCategoryBilling = 4 Then
        '    '        frmSalesOrderTransaksi.fn_UpdateOrderByKDCPPTNonRacikan(sKDCPPT, "0")
        '    '        frmSalesOrderTransaksi.fn_UpdateOrderByKDCPPTRacikan(sKDCPPT, "0")
        '    '    End If
        '    'Catch oErr As Exception
        '    '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    'End Try
        'Else
        '    If fn_DeleteData(grv.GetFocusedRowCellValue("KDSOTRANSAKSI")) = False Then
        '        MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
        '        Exit Sub
        '    End If
        'End If

        'MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        'fn_LoadSecurity()
        If grvBilling.GetFocusedRowCellValue("KDSOTRANSAKSI") Is Nothing Then
            Exit Sub
        End If
        If grvBilling.GetFocusedRowCellValue("CATEGORY") = 2 Then
            MsgBox("Tidak dapat di Update Merupakan Penunjang", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grvBilling.GetFocusedRowCellValue("CATEGORY") = 3 Then
            MsgBox("Tidak dapat di Update Merupakan Penunjang", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grvBilling.GetFocusedRowCellValue("CATEGORY") = 4 Then
            MsgBox("Tidak dapat di Update Merupakan Penunjang", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If CBool(grvBilling.GetFocusedRowCellValue("Bayar")) = True Then
            MsgBox("Sudah Bayar silahkan hapus dulu invoice ke bagian pembayaran/kasir", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim oSalesOrderTransaksi As New Sales.clsSalesOrderTransaksi

        'If sUPDATETRANSAKSI = False Then
        Dim ds1 = oSalesOrderTransaksi.GetData(grvBilling.GetFocusedRowCellValue("KDSOTRANSAKSI"))
        If ds1 IsNot Nothing Then
            If ds1.KDUSER <> sUserID Then
                MsgBox("Tidak dapat Rubah Silahkan hubungi User " & ds1.KDUSER, MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
        End If
        'End If

        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        'Dim sKDORDER As String = String.Empty

        'Dim ds = oSalesOrderTransaksi.GetData(grvBilling.GetFocusedRowCellValue("KDSOTRANSAKSI"))
        'If ds IsNot Nothing Then
        '    sKDORDER = ds.KDORDER
        'End If

        If fn_DeleteData(grvBilling.GetFocusedRowCellValue("KDSOTRANSAKSI")) = False Then
            MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        fn_LoadDataAksiBilling()
    End Sub
    Private Function fn_DeleteData(ByVal sKDSOTRANSAKSI As String) As Boolean
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

                fn_DeleteData = True

            Else
                fn_DeleteData = False
            End If

        Catch oErr As Exception
            fn_DeleteData = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub picAksiBilling_Refresh_Click(sender As Object, e As EventArgs) Handles picAksiBilling_Refresh.Click
        fn_LoadDataAksiBilling()
    End Sub
    Private Sub picAksiBilling_PACS_Click(sender As Object, e As EventArgs) Handles picAksiBilling_PACS.Click
        Try
            If grvBilling.GetFocusedRowCellValue("KDSOTRANSAKSI") Is Nothing Then
                Exit Sub
            End If
            If grvBilling.GetFocusedRowCellValue("SEQ") Is Nothing Then
                MsgBox("SEQ Kosong", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            If grvBilling.GetFocusedRowCellValue("CATEGORY") = 2 Then
                MsgBox("Tidak dapat di Update Merupakan Penunjang", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            If grvBilling.GetFocusedRowCellValue("CATEGORY") = 3 Then
                MsgBox("Tidak dapat di Update Merupakan Penunjang", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            If grvBilling.GetFocusedRowCellValue("CATEGORY") = 4 Then
                MsgBox("Tidak dapat di Update Merupakan Penunjang", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            frmReportLoadPasienPACS.fn_LoadRM(txtKDCUSTOMER.Text, grvBilling.GetFocusedRowCellValue("KDSOTRANSAKSI"), grvBilling.GetFocusedRowCellValue("SEQ"))
            frmReportLoadPasienPACS.ShowDialog(Me)
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmReportLoadPasienPACS Is Nothing Then frmReportLoadPasienPACS.Dispose()
            frmReportLoadPasienPACS = Nothing
        End Try
    End Sub
    Private Sub picBHP_Click(sender As Object, e As EventArgs) Handles picBHP.Click
        If sKDPENDAFTARAN <> "" Then
            Dim ds = oGrouperDataCppt.GetDataByKdKunjungan(txtKDKUNJUNGAN.Text)
            If ds IsNot Nothing Then
                Dim frmBHPPasienList As New frmBHPPasienList
                Try
                    frmBHPPasienList.fn_LoadData(ds.KDIDENTITAS, sKDPENDAFTARAN)
                    frmBHPPasienList.ShowDialog(Me)

                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                Finally
                    If Not frmBHPPasienList Is Nothing Then frmBHPPasienList.Dispose()
                    frmBHPPasienList = Nothing

                End Try
            End If

        End If

    End Sub
    Private Sub btnPemetaan_Click(sender As Object, e As EventArgs) Handles btnPemetaan.Click
        Dim oKunjungan As New Admission.clsPendaftaran_Kunjungan

        Dim dsKunjungan = oKunjungan.GetData(txtKDKUNJUNGAN.Text)
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
            MsgBox("Silahakan Pilih salah satu pasien untuk pilih ruangan", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub btnMutasi_Click(sender As Object, e As EventArgs) Handles btnMutasi.Click
        If chkPemetaan.Checked = False Then
            If sKDPENDAFTARAN = "" Then
                MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim frmPendaftaran_KunjunganList As New frmPendaftaran_KunjunganList
            Try
                frmPendaftaran_KunjunganList.fn_LoadRegister(sKDPENDAFTARAN)
                frmPendaftaran_KunjunganList.ShowDialog(Me)

                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmPendaftaran_KunjunganList Is Nothing Then frmPendaftaran_KunjunganList.Dispose()
                frmPendaftaran_KunjunganList = Nothing
            End Try
        Else
            Dim frmPendaftaran_Kunjungan As New frmPendaftaran_Kunjungan
            Try
                frmPendaftaran_Kunjungan.LoadMe(FORM_MODE.FORM_MODE_ADD, "")
                frmPendaftaran_Kunjungan.ShowDialog(Me)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmPendaftaran_Kunjungan Is Nothing Then frmPendaftaran_Kunjungan.Dispose()
                frmPendaftaran_Kunjungan = Nothing
            End Try
        End If

        fn_LoadSecurity()
    End Sub
    Private Sub EditOrderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditOrderToolStripMenuItem.Click
        If grvPenunjang.GetFocusedRowCellValue("KODE") Is Nothing Then
            Exit Sub
        End If

        If grvPenunjang.GetFocusedRowCellValue("Keterangan") <> "TAMBAH" Then
            MsgBox("Status" & vbCrLf & grvPenunjang.GetFocusedRowCellValue("Keterangan"), MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim dsIdentitas = oGrouperDataCppt.GetDataByKdKunjungan(txtKDKUNJUNGAN.Text)
        If dsIdentitas IsNot Nothing Then
            If grvPenunjang.GetFocusedRowCellValue("Penunjang") = "ORDER LABORATORIUM" Then
                Dim frmOrderRanapLab As New frmOrderRanapLab
                Try
                    frmOrderRanapLab.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsIdentitas.KDIDENTITAS, "", "", "", grvPenunjang.GetFocusedRowCellValue("KODE"))
                    frmOrderRanapLab.ShowDialog(Me)
                    LoadPenunjang(sKDPENDAFTARAN)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                Finally
                    If Not frmOrderRanapLab Is Nothing Then frmOrderRanapLab.Dispose()
                    frmOrderRanapLab = Nothing
                End Try
            ElseIf grvPenunjang.GetFocusedRowCellValue("Penunjang") = "ORDER RADIOLOGI"
                Dim frmOrderRanapRad As New frmOrderRanapRad
                Try

                    frmOrderRanapRad.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsIdentitas.KDIDENTITAS, "", "", "", grvPenunjang.GetFocusedRowCellValue("KODE"))
                    frmOrderRanapRad.ShowDialog(Me)
                    LoadPenunjang(sKDPENDAFTARAN)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                Finally
                    If Not frmOrderRanapRad Is Nothing Then frmOrderRanapRad.Dispose()
                    frmOrderRanapRad = Nothing
                End Try
            Else
                MsgBox("Order Tidak di Temukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Else
            MsgBox("Identitas Kosong", MsgBoxStyle.Exclamation, Me.Text)
        End If

    End Sub
    Private Sub ViewOrderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewOrderToolStripMenuItem.Click
        If grvPenunjang.GetFocusedRowCellValue("KODE") Is Nothing Then
            Exit Sub
        End If

        Dim dsIdentitas = oGrouperDataCppt.GetDataByKdKunjungan(txtKDKUNJUNGAN.Text)

        If dsIdentitas IsNot Nothing Then
            If grvPenunjang.GetFocusedRowCellValue("Penunjang") = "ORDER LABORATORIUM" Then
                Dim frmOrderRanapLab As New frmOrderRanapLab
                Try
                    frmOrderRanapLab.LoadMe(FORM_MODE.FORM_MODE_VIEW, dsIdentitas.KDIDENTITAS, "", "", "", grvPenunjang.GetFocusedRowCellValue("KODE"))
                    frmOrderRanapLab.ShowDialog(Me)
                    LoadPenunjang(sKDPENDAFTARAN)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                Finally
                    If Not frmOrderRanapLab Is Nothing Then frmOrderRanapLab.Dispose()
                    frmOrderRanapLab = Nothing
                End Try
            ElseIf grvPenunjang.GetFocusedRowCellValue("Penunjang") = "ORDER RADIOLOGI"
                Dim frmOrderRanapRad As New frmOrderRanapRad
                Try
                    frmOrderRanapRad.LoadMe(FORM_MODE.FORM_MODE_VIEW, dsIdentitas.KDIDENTITAS, "", "", "", grvPenunjang.GetFocusedRowCellValue("KODE"))
                    frmOrderRanapRad.ShowDialog(Me)
                    LoadPenunjang(sKDPENDAFTARAN)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                Finally
                    If Not frmOrderRanapRad Is Nothing Then frmOrderRanapRad.Dispose()
                    frmOrderRanapRad = Nothing
                End Try
            Else
                MsgBox("Order Tidak di Temukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Else
            MsgBox("Identitas Kosong", MsgBoxStyle.Exclamation, Me.Text)
        End If

    End Sub
    Private Sub BatalOrderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalOrderToolStripMenuItem.Click
        If grvPenunjang.GetFocusedRowCellValue("KODE") Is Nothing Then
            Exit Sub
        End If
        If grvPenunjang.GetFocusedRowCellValue("Keterangan") <> "TAMBAH" Then
            MsgBox("Status" & vbCrLf & grvPenunjang.GetFocusedRowCellValue("Keterangan"), MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If grvPenunjang.GetFocusedRowCellValue("Penunjang") = "ORDER LABORATORIUM" Then
            If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
            Dim oOrder As New Order.clsOrderRanapLab

            If oOrder.DeleteData(grvPenunjang.GetFocusedRowCellValue("KODE")) = False Then
                MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
            LoadPenunjang(sKDPENDAFTARAN)

        ElseIf grvPenunjang.GetFocusedRowCellValue("Penunjang") = "ORDER RADIOLOGI"
            If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
            Dim oOrder As New Order.clsOrderRanapRad

            If oOrder.DeleteData(grvPenunjang.GetFocusedRowCellValue("KODE")) = False Then
                MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
            LoadPenunjang(sKDPENDAFTARAN)
        Else
            MsgBox("Order Tidak di Temukan", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub UpdateBedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KosongBedToolStripMenuItem.Click
        If chkPemetaan.Checked = True Then
            If sKDPENDAFTARAN <> "" Then
                Dim oKelasAplicareBed As New Reference.clsKelasAplicareBed

                For Each xloop In oKelasAplicareBed.GetDataDetailByKdPendaftaranKunjungan(sKDPENDAFTARAN)
                    For Each yloop In oKelasAplicareBed.GetDataDetailByKdPendaftaran(xloop.KDKUNJUNGAN)
                        oKelasAplicareBed.UpdateDataPemetaan(yloop.KODEBED, "", "KOSONG", "")
                    Next
                Next
            Else
                MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            End If
        End If
    End Sub
    Private Sub chkPemetaan_EditValueChanged(sender As Object, e As EventArgs) Handles chkPemetaan.EditValueChanged
        If btnType.Text <> "Rawat Jalan" Then
            If chkPemetaan.Checked = False Then
                deDATE.Visible = True
                deDateTo.Visible = True

                lblTanggalFrom.Visible = True
                lblTanggalTitikDuaFrom.Visible = True
                lblTanggalTo.Visible = True
                lblTanggalTitikDuaTo.Visible = True
                lblTanggalFrom.Text = "Dari Tgl"
                lblTanggalTo.Text = "Sampai Tgl"
            Else
                deDATE.DateTime = Now.AddDays((-Now.Day) + 1)
                deDateTo.DateTime = Now
                deDATE.Visible = False
                deDateTo.Visible = False

                lblTanggalFrom.Visible = False
                lblTanggalTitikDuaFrom.Visible = False
                lblTanggalTo.Visible = False
                lblTanggalTitikDuaTo.Visible = False
                lblTanggalFrom.Text = "Dari Tgl"
                lblTanggalTo.Text = "Sampai Tgl"
            End If
        End If
    End Sub

    Private Sub picAksiBilling_Delete_Click(sender As Object, e As EventArgs) Handles picAksiBilling_Delete.Click

    End Sub
    Private Sub UpdateBedToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles UpdateBedToolStripMenuItem.Click
        'If grvListPasien.GetFocusedRowCellValue("KDUPDATE_APLICARE") Is Nothing Then
        '    MsgBox("Silahkan Pilih Template", MsgBoxStyle.Exclamation, Me.Text)
        '    Exit Sub
        'End If

        If grvListPasien.GetFocusedRowCellValue("KDUPDATE_APLICARE") IsNot Nothing Then
            Dim kodeapli As String = grvListPasien.GetFocusedRowCellValue("KDUPDATE_APLICARE")

            Dim oKelasAplicareBed As New Reference.clsKelasAplicareBed

            Dim ds = oKelasAplicareBed.GetDatadefaultKelasAplicare(kodeapli)
            If ds Is Nothing Then
                Dim frmKelasAplicareBed As New frmKelasAplicareBed
                Try
                    frmKelasAplicareBed.LoadMe(FORM_MODE.FORM_MODE_ADD, kodeapli)
                    frmKelasAplicareBed.ShowDialog(Me)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                Finally
                    If Not frmKelasAplicareBed Is Nothing Then frmKelasAplicareBed.Dispose()
                    frmKelasAplicareBed = Nothing
                End Try
            Else
                Dim frmKelasAplicareBed As New frmKelasAplicareBed
                Try
                    frmKelasAplicareBed.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDUPDATE_APLICARE)
                    frmKelasAplicareBed.ShowDialog(Me)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                Finally
                    If Not frmKelasAplicareBed Is Nothing Then frmKelasAplicareBed.Dispose()
                    frmKelasAplicareBed = Nothing
                End Try
            End If
        Else
            MsgBox("Silahkan Pilih Ruangan terlebih dahulu/Bed Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
#End Region
#End Region
End Class

