<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmCPPT
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCPPT))
        Me.panelCPPT = New System.Windows.Forms.Panel()
        Me.GroupControl8 = New DevExpress.XtraEditors.GroupControl()
        Me.txtCPPT_CATATAN = New DevExpress.XtraEditors.MemoEdit()
        Me.grdKDDAFTAR_L4 = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn29 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtCPPT_Alasan = New DevExpress.XtraEditors.MemoEdit()
        Me.LabelControl28 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl27 = New DevExpress.XtraEditors.LabelControl()
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.grdSemuaTindakan = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.barManager = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar3 = New DevExpress.XtraBars.Bar()
        Me.btnSaveClosee = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.btnSaveNew = New DevExpress.XtraBars.BarButtonItem()
        Me.btnClose = New DevExpress.XtraBars.BarButtonItem()
        Me.btnSaveClose = New DevExpress.XtraBars.BarButtonItem()
        Me.btnClosee = New DevExpress.XtraBars.BarButtonItem()
        Me.progressBarSave = New DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar()
        Me.progressSave = New DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn21 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.btnObatTerakhir = New DevExpress.XtraEditors.SimpleButton()
        Me.btnTindakanTerakhir = New DevExpress.XtraEditors.SimpleButton()
        Me.grdKDITEMALL = New DevExpress.XtraGrid.GridControl()
        Me.grvKDITEMALL = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn30 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colKELOMPOK = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colNMITEM2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPilih = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.chkPilih = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.grdITEM_L2 = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView5 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn28 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtCPPT_TotalTindakan = New DevExpress.XtraEditors.TextEdit()
        Me.grdCPPT_Tindakan = New DevExpress.XtraGrid.GridControl()
        Me.ContextMenuStrip2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DeleteToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.BindingSourceCPPT_Tindakan = New System.Windows.Forms.BindingSource(Me.components)
        Me.grvCPPT_Tindakan = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colCPPT_KDITEMTINDAKAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdCPPT_KDITEMTINDAKAN = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.grvCPPT_KDITEMTINDAKAN = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCPPT_MEMOTINDAKAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemMemoExEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit()
        Me.colCPPT_KDUOMTINDAKAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdCPPT_KDUOMTINDAKAN = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.grvCPPT_KDUOMTINDAKAN = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCPPT_JUMLAHTINDAKAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCPPT_HARGATINDAKAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCPPT_TOTALTINDAKAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCPPT_ISBACA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdCPPT_TemplateTindakan = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.grvCPPT_TemplateTindakan = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem13 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem14 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem4 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem12 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lKDITEM_L2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.XtraTabControlCPPT_Resep = New DevExpress.XtraTab.XtraTabControl()
        Me.tabCPPT_NonRacikan = New DevExpress.XtraTab.XtraTabPage()
        Me.LayoutControl11 = New DevExpress.XtraLayout.LayoutControl()
        Me.txtCPPT_TotalNonRacikanNonPaket = New DevExpress.XtraEditors.TextEdit()
        Me.grdCPPT_ResepNonRacikan = New DevExpress.XtraGrid.GridControl()
        Me.ContextMenuStrip3 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DeleteToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.BindingSourceCPPT_NonRacikan = New System.Windows.Forms.BindingSource(Me.components)
        Me.grvCPPT_ResepNonRacikan = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colCPPT_KDITEMNONRACIKAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdCPPT_KDITEMNONRACIKAN = New DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit()
        Me.grvCPPT_KDITEMNONRACIKAN = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCPPT_ISKRONISNONRACIKAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCPPT_KDUOMNONRACIKAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdCPPT_KDUOMNONRACIKAN = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.grvCPPT_KDUOMNONRACIKAN = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCPPT_KDSIGNANONRACIKAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdCPPT_KDSIGNANONRACIKAN = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.grvCPPT_KDSIGNANONRACIKAN = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn15 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCPPT_KDCARAPAKAINONRACIKAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdCPPT_KDCARAPAKAINONRACIKAN = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.grvCPPT_KDCARAPAKAINONRACIKAN = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn16 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCPPT_JUMLAHNONRACIKAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCPPT_JUMLAH_PAKETNONRACIKAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCPPT_JUMLAH_NONPAKETNONRACIKAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCPPT_HARGANONRACIKAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCPPT_TOTAL_PAKETNONRACIKAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCPPT_TOTAL_NONPAKETNONRACIKAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCPPT_TOTALNONRACIKAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCPPT_REMARKS_DOKTERNONRACIKAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCPPT_ISALKESNONRACIKAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCPPT_ISBACANONRACIKAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.btnRiwayatPemberianObat = New DevExpress.XtraEditors.SimpleButton()
        Me.grdTEMPLATE = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.grvWAREHOUSE = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtCPPT_TotalNonRacikanPaket = New DevExpress.XtraEditors.TextEdit()
        Me.btnRiwayatPemberianResep = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControlGroup11 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem24 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem66 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem20 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.tabCPPT_Racikan = New DevExpress.XtraTab.XtraTabPage()
        Me.LayoutControl4 = New DevExpress.XtraLayout.LayoutControl()
        Me.txtCPPT_TotalRacikanNonPaket = New DevExpress.XtraEditors.TextEdit()
        Me.grdCPPT_ResepRacikan = New DevExpress.XtraGrid.GridControl()
        Me.ContextMenuStrip4 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DeleteToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.BindingSourceCPPT_Racikan = New System.Windows.Forms.BindingSource(Me.components)
        Me.grvCPPT_ResepRacikan = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colCPPT_KDITEMRACIKAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdCPPT_KDITEMRACIKAN = New DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit()
        Me.grvCPPT_KDITEMRACIKAN = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn17 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn18 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn19 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn20 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn24 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCPPT_KDUOMRACIKAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdCPPT_KDUOMRACIKAN = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.grvCPPT_KDUOMRACIKAN = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn25 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCPPT_SIGNARACIKAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCPPT_PERMINTAANRACIKAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCPPT_JUMLAHRACIKAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCPPT_HARGARACIKAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCPPT_TOTALRACIKAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCPPT_REMARKSRACIKAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCPPT_ISBACARACIKAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtCPPT_TotalRacikanPaket = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup4 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem19 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem18 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem21 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.GroupControl7 = New DevExpress.XtraEditors.GroupControl()
        Me.btnCPPT_CariICD10 = New DevExpress.XtraEditors.SimpleButton()
        Me.txtCPPT_CARIDIAGNOSA = New DevExpress.XtraEditors.TextEdit()
        Me.grdCariDiagnosa = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.grvCariDiagnosa = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LabelControl33 = New DevExpress.XtraEditors.LabelControl()
        Me.txtCPPT_Indikasi = New DevExpress.XtraEditors.MemoEdit()
        Me.grdCPPT_Diagnosa = New DevExpress.XtraGrid.GridControl()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BindingSourceCPPT_Diagnosa = New System.Windows.Forms.BindingSource(Me.components)
        Me.grvCPPT_Diagnosa = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colCPPT_KATEGORI = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCPPT_MEMODIAGNOSA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCPPT_KDDIAGNOSA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.btnbtnBuatTemplateTindakan = New DevExpress.XtraEditors.SimpleButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtKDIDENTITAS = New DevExpress.XtraEditors.TextEdit()
        Me.txtCPPT_Kode = New DevExpress.XtraEditors.TextEdit()
        Me.GroupControl4 = New DevExpress.XtraEditors.GroupControl()
        Me.txtCPPT_OBJEKTIF_ALAMATGAMBARPEMERIKSAAN = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl39 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl38 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl37 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl36 = New DevExpress.XtraEditors.LabelControl()
        Me.txtCPPT_VisualAnalogScore = New DevExpress.XtraEditors.TextEdit()
        Me.txtCPPT_GCS = New DevExpress.XtraEditors.TextEdit()
        Me.txtCPPT_TampakSakit = New DevExpress.XtraEditors.TextEdit()
        Me.txtCPPT_Kesadaran = New DevExpress.XtraEditors.TextEdit()
        Me.picCPPT_Gambar = New System.Windows.Forms.PictureBox()
        Me.txtCPPT_Pemeriksaan = New DevExpress.XtraEditors.MemoEdit()
        Me.btnResetGambar = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCPPT_AmbilGambar = New DevExpress.XtraEditors.SimpleButton()
        Me.txtCPPT_Diastole = New DevExpress.XtraEditors.TextEdit()
        Me.txtCPPT_Sistole = New DevExpress.XtraEditors.TextEdit()
        Me.txtCPPT_RR = New DevExpress.XtraEditors.TextEdit()
        Me.txtCPPT_BeratBadan = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl14 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl13 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl30 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl19 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.txtCPPT_Suhu = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl20 = New DevExpress.XtraEditors.LabelControl()
        Me.txtCPPT_SpO2 = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl11 = New DevExpress.XtraEditors.LabelControl()
        Me.txtCPPT_HR = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl22 = New DevExpress.XtraEditors.LabelControl()
        Me.txtCPPT_TinggiBadan = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl24 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl10 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl21 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl18 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl12 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl15 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl23 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl25 = New DevExpress.XtraEditors.LabelControl()
        Me.GroupControl5 = New DevExpress.XtraEditors.GroupControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.grdCPPT_Profesi = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.grvCPPT_Profesi = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.deCPPT_Tanggal = New DevExpress.XtraEditors.DateEdit()
        Me.grdCPPT_KDDOCTOR = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.grvDPJP = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn22 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LabelControl29 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.txtRUANGAN = New DevExpress.XtraEditors.TextEdit()
        Me.txtCPPT_KeluhanUtama = New DevExpress.XtraEditors.MemoEdit()
        Me.LabelControl50 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl16 = New DevExpress.XtraEditors.LabelControl()
        Me.chkCPPT_AlergiTidak = New DevExpress.XtraEditors.CheckEdit()
        Me.chkCPPT_AlergiYa = New DevExpress.XtraEditors.CheckEdit()
        Me.txtCPPT_AlergiYa = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.panelCPPT.SuspendLayout()
        CType(Me.GroupControl8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl8.SuspendLayout()
        CType(Me.txtCPPT_CATATAN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDDAFTAR_L4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCPPT_Alasan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl2.SuspendLayout()
        CType(Me.grdSemuaTindakan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDITEMALL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvKDITEMALL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPilih, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdITEM_L2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCPPT_TotalTindakan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCPPT_Tindakan, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip2.SuspendLayout()
        CType(Me.BindingSourceCPPT_Tindakan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvCPPT_Tindakan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCPPT_KDITEMTINDAKAN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvCPPT_KDITEMTINDAKAN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemMemoExEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCPPT_KDUOMTINDAKAN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvCPPT_KDUOMTINDAKAN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCPPT_TemplateTindakan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvCPPT_TemplateTindakan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKDITEM_L2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XtraTabControlCPPT_Resep, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControlCPPT_Resep.SuspendLayout()
        Me.tabCPPT_NonRacikan.SuspendLayout()
        CType(Me.LayoutControl11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl11.SuspendLayout()
        CType(Me.txtCPPT_TotalNonRacikanNonPaket.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCPPT_ResepNonRacikan, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip3.SuspendLayout()
        CType(Me.BindingSourceCPPT_NonRacikan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvCPPT_ResepNonRacikan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCPPT_KDITEMNONRACIKAN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvCPPT_KDITEMNONRACIKAN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCPPT_KDUOMNONRACIKAN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvCPPT_KDUOMNONRACIKAN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCPPT_KDSIGNANONRACIKAN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvCPPT_KDSIGNANONRACIKAN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCPPT_KDCARAPAKAINONRACIKAN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvCPPT_KDCARAPAKAINONRACIKAN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdTEMPLATE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvWAREHOUSE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCPPT_TotalNonRacikanPaket.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem66, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabCPPT_Racikan.SuspendLayout()
        CType(Me.LayoutControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl4.SuspendLayout()
        CType(Me.txtCPPT_TotalRacikanNonPaket.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCPPT_ResepRacikan, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip4.SuspendLayout()
        CType(Me.BindingSourceCPPT_Racikan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvCPPT_ResepRacikan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCPPT_KDITEMRACIKAN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvCPPT_KDITEMRACIKAN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCPPT_KDUOMRACIKAN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvCPPT_KDUOMRACIKAN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCPPT_TotalRacikanPaket.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl7.SuspendLayout()
        CType(Me.txtCPPT_CARIDIAGNOSA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCariDiagnosa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvCariDiagnosa, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCPPT_Indikasi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCPPT_Diagnosa, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.BindingSourceCPPT_Diagnosa, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvCPPT_Diagnosa, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.txtKDIDENTITAS.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCPPT_Kode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl4.SuspendLayout()
        CType(Me.txtCPPT_OBJEKTIF_ALAMATGAMBARPEMERIKSAAN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCPPT_VisualAnalogScore.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCPPT_GCS.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCPPT_TampakSakit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCPPT_Kesadaran.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picCPPT_Gambar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCPPT_Pemeriksaan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCPPT_Diastole.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCPPT_Sistole.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCPPT_RR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCPPT_BeratBadan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCPPT_Suhu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCPPT_SpO2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCPPT_HR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCPPT_TinggiBadan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl5.SuspendLayout()
        CType(Me.grdCPPT_Profesi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvCPPT_Profesi, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deCPPT_Tanggal.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deCPPT_Tanggal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCPPT_KDDOCTOR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvDPJP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRUANGAN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCPPT_KeluhanUtama.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCPPT_AlergiTidak.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCPPT_AlergiYa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCPPT_AlergiYa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'panelCPPT
        '
        Me.panelCPPT.AutoScroll = True
        Me.panelCPPT.Controls.Add(Me.GroupControl8)
        Me.panelCPPT.Controls.Add(Me.GroupControl7)
        Me.panelCPPT.Controls.Add(Me.PanelControl2)
        Me.panelCPPT.Controls.Add(Me.GroupControl4)
        Me.panelCPPT.Controls.Add(Me.GroupControl5)
        Me.panelCPPT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelCPPT.Location = New System.Drawing.Point(0, 0)
        Me.panelCPPT.Name = "panelCPPT"
        Me.panelCPPT.Size = New System.Drawing.Size(809, 551)
        Me.panelCPPT.TabIndex = 6
        '
        'GroupControl8
        '
        Me.GroupControl8.AppearanceCaption.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GroupControl8.AppearanceCaption.Options.UseFont = True
        Me.GroupControl8.Controls.Add(Me.txtCPPT_CATATAN)
        Me.GroupControl8.Controls.Add(Me.grdKDDAFTAR_L4)
        Me.GroupControl8.Controls.Add(Me.txtCPPT_Alasan)
        Me.GroupControl8.Controls.Add(Me.LabelControl28)
        Me.GroupControl8.Controls.Add(Me.LabelControl27)
        Me.GroupControl8.Controls.Add(Me.LayoutControl2)
        Me.GroupControl8.Controls.Add(Me.XtraTabControlCPPT_Resep)
        Me.GroupControl8.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl8.Location = New System.Drawing.Point(0, 1027)
        Me.GroupControl8.Name = "GroupControl8"
        Me.GroupControl8.Size = New System.Drawing.Size(792, 844)
        Me.GroupControl8.TabIndex = 6
        Me.GroupControl8.Text = "Planning"
        '
        'txtCPPT_CATATAN
        '
        Me.txtCPPT_CATATAN.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCPPT_CATATAN.Location = New System.Drawing.Point(13, 650)
        Me.txtCPPT_CATATAN.Name = "txtCPPT_CATATAN"
        Me.txtCPPT_CATATAN.Size = New System.Drawing.Size(774, 82)
        Me.txtCPPT_CATATAN.TabIndex = 96
        '
        'grdKDDAFTAR_L4
        '
        Me.grdKDDAFTAR_L4.EnterMoveNextControl = True
        Me.grdKDDAFTAR_L4.Location = New System.Drawing.Point(13, 624)
        Me.grdKDDAFTAR_L4.Name = "grdKDDAFTAR_L4"
        Me.grdKDDAFTAR_L4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDDAFTAR_L4.Properties.NullText = ""
        Me.grdKDDAFTAR_L4.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdKDDAFTAR_L4.Properties.View = Me.GridView1
        Me.grdKDDAFTAR_L4.Size = New System.Drawing.Size(271, 20)
        Me.grdKDDAFTAR_L4.TabIndex = 54
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn29})
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'GridColumn29
        '
        Me.GridColumn29.Caption = "Name Display"
        Me.GridColumn29.FieldName = "MEMO"
        Me.GridColumn29.Name = "GridColumn29"
        Me.GridColumn29.Visible = True
        Me.GridColumn29.VisibleIndex = 0
        '
        'txtCPPT_Alasan
        '
        Me.txtCPPT_Alasan.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCPPT_Alasan.Location = New System.Drawing.Point(13, 763)
        Me.txtCPPT_Alasan.Name = "txtCPPT_Alasan"
        Me.txtCPPT_Alasan.Size = New System.Drawing.Size(772, 75)
        Me.txtCPPT_Alasan.TabIndex = 39
        '
        'LabelControl28
        '
        Me.LabelControl28.Location = New System.Drawing.Point(15, 744)
        Me.LabelControl28.Name = "LabelControl28"
        Me.LabelControl28.Size = New System.Drawing.Size(39, 13)
        Me.LabelControl28.TabIndex = 95
        Me.LabelControl28.Text = "Alasan :"
        '
        'LabelControl27
        '
        Me.LabelControl27.Location = New System.Drawing.Point(18, 605)
        Me.LabelControl27.Name = "LabelControl27"
        Me.LabelControl27.Size = New System.Drawing.Size(71, 13)
        Me.LabelControl27.TabIndex = 95
        Me.LabelControl27.Text = "Tindak Lanjut :"
        '
        'LayoutControl2
        '
        Me.LayoutControl2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LayoutControl2.Controls.Add(Me.grdSemuaTindakan)
        Me.LayoutControl2.Controls.Add(Me.btnObatTerakhir)
        Me.LayoutControl2.Controls.Add(Me.btnTindakanTerakhir)
        Me.LayoutControl2.Controls.Add(Me.grdKDITEMALL)
        Me.LayoutControl2.Controls.Add(Me.grdITEM_L2)
        Me.LayoutControl2.Controls.Add(Me.txtCPPT_TotalTindakan)
        Me.LayoutControl2.Controls.Add(Me.grdCPPT_Tindakan)
        Me.LayoutControl2.Controls.Add(Me.grdCPPT_TemplateTindakan)
        Me.LayoutControl2.Location = New System.Drawing.Point(6, 22)
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(1137, 253, 250, 350)
        Me.LayoutControl2.Root = Me.LayoutControlGroup2
        Me.LayoutControl2.Size = New System.Drawing.Size(783, 258)
        Me.LayoutControl2.TabIndex = 73
        Me.LayoutControl2.Text = "LayoutControl2"
        '
        'grdSemuaTindakan
        '
        Me.grdSemuaTindakan.EditValue = ""
        Me.grdSemuaTindakan.Location = New System.Drawing.Point(137, 52)
        Me.grdSemuaTindakan.MenuManager = Me.barManager
        Me.grdSemuaTindakan.Name = "grdSemuaTindakan"
        Me.grdSemuaTindakan.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdSemuaTindakan.Properties.NullText = ""
        Me.grdSemuaTindakan.Properties.View = Me.SearchLookUpEdit1View
        Me.grdSemuaTindakan.Size = New System.Drawing.Size(366, 20)
        Me.grdSemuaTindakan.StyleController = Me.LayoutControl2
        Me.grdSemuaTindakan.TabIndex = 55
        '
        'barManager
        '
        Me.barManager.AllowQuickCustomization = False
        Me.barManager.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar3})
        Me.barManager.DockControls.Add(Me.barDockControlTop)
        Me.barManager.DockControls.Add(Me.barDockControlBottom)
        Me.barManager.DockControls.Add(Me.barDockControlLeft)
        Me.barManager.DockControls.Add(Me.barDockControlRight)
        Me.barManager.Form = Me
        Me.barManager.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.btnSaveNew, Me.btnClose, Me.btnSaveClose, Me.btnSaveClosee, Me.btnClosee})
        Me.barManager.MainMenu = Me.Bar3
        Me.barManager.MaxItemId = 10
        Me.barManager.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.progressBarSave, Me.progressSave})
        '
        'Bar3
        '
        Me.Bar3.BarName = "Custom 2"
        Me.Bar3.DockCol = 0
        Me.Bar3.DockRow = 0
        Me.Bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom
        Me.Bar3.FloatLocation = New System.Drawing.Point(46, 709)
        Me.Bar3.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.btnSaveClosee)})
        Me.Bar3.OptionsBar.MultiLine = True
        Me.Bar3.OptionsBar.UseWholeRow = True
        Me.Bar3.Text = "Custom 2"
        '
        'btnSaveClosee
        '
        Me.btnSaveClosee.Border = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.btnSaveClosee.Caption = "SIMPAN CPPT"
        Me.btnSaveClosee.Glyph = CType(resources.GetObject("btnSaveClosee.Glyph"), System.Drawing.Image)
        Me.btnSaveClosee.Id = 8
        Me.btnSaveClosee.ItemAppearance.Normal.Font = New System.Drawing.Font("Tahoma", 50.25!)
        Me.btnSaveClosee.ItemAppearance.Normal.FontSizeDelta = 3
        Me.btnSaveClosee.ItemAppearance.Normal.Options.UseFont = True
        Me.btnSaveClosee.Name = "btnSaveClosee"
        Me.btnSaveClosee.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(809, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 551)
        Me.barDockControlBottom.Size = New System.Drawing.Size(809, 90)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 551)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(809, 0)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 551)
        '
        'btnSaveNew
        '
        Me.btnSaveNew.Caption = "F2 - Save && New"
        Me.btnSaveNew.Id = 2
        Me.btnSaveNew.Name = "btnSaveNew"
        '
        'btnClose
        '
        Me.btnClose.Caption = "F12 - Close"
        Me.btnClose.Id = 3
        Me.btnClose.Name = "btnClose"
        '
        'btnSaveClose
        '
        Me.btnSaveClose.Caption = "F3 - Save && Close"
        Me.btnSaveClose.Id = 5
        Me.btnSaveClose.Name = "btnSaveClose"
        '
        'btnClosee
        '
        Me.btnClosee.Caption = "F12 - Close"
        Me.btnClosee.Id = 9
        Me.btnClosee.Name = "btnClosee"
        '
        'progressBarSave
        '
        Me.progressBarSave.Name = "progressBarSave"
        Me.progressBarSave.Stopped = True
        '
        'progressSave
        '
        Me.progressSave.Name = "progressSave"
        Me.progressSave.Paused = True
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn7, Me.GridColumn21})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "GridColumn7"
        Me.GridColumn7.FieldName = "KELOMPOK"
        Me.GridColumn7.Name = "GridColumn7"
        '
        'GridColumn21
        '
        Me.GridColumn21.Caption = "Tindakan"
        Me.GridColumn21.FieldName = "NMITEM2"
        Me.GridColumn21.Name = "GridColumn21"
        Me.GridColumn21.Visible = True
        Me.GridColumn21.VisibleIndex = 0
        '
        'btnObatTerakhir
        '
        Me.btnObatTerakhir.Location = New System.Drawing.Point(2, 234)
        Me.btnObatTerakhir.Name = "btnObatTerakhir"
        Me.btnObatTerakhir.Size = New System.Drawing.Size(140, 22)
        Me.btnObatTerakhir.StyleController = Me.LayoutControl2
        Me.btnObatTerakhir.TabIndex = 54
        Me.btnObatTerakhir.Text = "Reload Obat Terakhir"
        '
        'btnTindakanTerakhir
        '
        Me.btnTindakanTerakhir.Location = New System.Drawing.Point(2, 2)
        Me.btnTindakanTerakhir.Name = "btnTindakanTerakhir"
        Me.btnTindakanTerakhir.Size = New System.Drawing.Size(135, 22)
        Me.btnTindakanTerakhir.StyleController = Me.LayoutControl2
        Me.btnTindakanTerakhir.TabIndex = 53
        Me.btnTindakanTerakhir.Text = "Reload Tindakan Terkahir"
        '
        'grdKDITEMALL
        '
        Me.grdKDITEMALL.Location = New System.Drawing.Point(507, 76)
        Me.grdKDITEMALL.MainView = Me.grvKDITEMALL
        Me.grdKDITEMALL.Name = "grdKDITEMALL"
        Me.grdKDITEMALL.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.chkPilih})
        Me.grdKDITEMALL.Size = New System.Drawing.Size(274, 154)
        Me.grdKDITEMALL.TabIndex = 31
        Me.grdKDITEMALL.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvKDITEMALL})
        '
        'grvKDITEMALL
        '
        Me.grvKDITEMALL.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn30, Me.colKELOMPOK, Me.colNMITEM2, Me.colPilih})
        Me.grvKDITEMALL.GridControl = Me.grdKDITEMALL
        Me.grvKDITEMALL.Name = "grvKDITEMALL"
        Me.grvKDITEMALL.OptionsView.ShowAutoFilterRow = True
        Me.grvKDITEMALL.OptionsView.ShowGroupPanel = False
        '
        'GridColumn30
        '
        Me.GridColumn30.Caption = "GridColumn30"
        Me.GridColumn30.FieldName = "KDITEM"
        Me.GridColumn30.Name = "GridColumn30"
        '
        'colKELOMPOK
        '
        Me.colKELOMPOK.Caption = "Kelompok"
        Me.colKELOMPOK.FieldName = "KELOMPOK"
        Me.colKELOMPOK.Name = "colKELOMPOK"
        '
        'colNMITEM2
        '
        Me.colNMITEM2.Caption = "Tindakan"
        Me.colNMITEM2.FieldName = "NMITEM2"
        Me.colNMITEM2.Name = "colNMITEM2"
        Me.colNMITEM2.Visible = True
        Me.colNMITEM2.VisibleIndex = 0
        Me.colNMITEM2.Width = 401
        '
        'colPilih
        '
        Me.colPilih.Caption = "Pilih"
        Me.colPilih.ColumnEdit = Me.chkPilih
        Me.colPilih.FieldName = "KDPILIH"
        Me.colPilih.Name = "colPilih"
        Me.colPilih.Width = 108
        '
        'chkPilih
        '
        Me.chkPilih.AutoHeight = False
        Me.chkPilih.Name = "chkPilih"
        Me.chkPilih.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'grdITEM_L2
        '
        Me.grdITEM_L2.Location = New System.Drawing.Point(612, 28)
        Me.grdITEM_L2.Name = "grdITEM_L2"
        Me.grdITEM_L2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdITEM_L2.Properties.NullText = ""
        Me.grdITEM_L2.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdITEM_L2.Properties.View = Me.GridView5
        Me.grdITEM_L2.Size = New System.Drawing.Size(169, 20)
        Me.grdITEM_L2.StyleController = Me.LayoutControl2
        Me.grdITEM_L2.TabIndex = 29
        '
        'GridView5
        '
        Me.GridView5.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn28})
        Me.GridView5.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView5.Name = "GridView5"
        Me.GridView5.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView5.OptionsView.ShowAutoFilterRow = True
        Me.GridView5.OptionsView.ShowGroupPanel = False
        '
        'GridColumn28
        '
        Me.GridColumn28.Caption = "Keterangan"
        Me.GridColumn28.FieldName = "MEMO"
        Me.GridColumn28.Name = "GridColumn28"
        Me.GridColumn28.Visible = True
        Me.GridColumn28.VisibleIndex = 0
        '
        'txtCPPT_TotalTindakan
        '
        Me.txtCPPT_TotalTindakan.EditValue = "0"
        Me.txtCPPT_TotalTindakan.Location = New System.Drawing.Point(304, 210)
        Me.txtCPPT_TotalTindakan.Name = "txtCPPT_TotalTindakan"
        Me.txtCPPT_TotalTindakan.Properties.Appearance.Options.UseTextOptions = True
        Me.txtCPPT_TotalTindakan.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtCPPT_TotalTindakan.Properties.Mask.EditMask = "n0"
        Me.txtCPPT_TotalTindakan.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtCPPT_TotalTindakan.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtCPPT_TotalTindakan.Properties.ReadOnly = True
        Me.txtCPPT_TotalTindakan.Size = New System.Drawing.Size(199, 20)
        Me.txtCPPT_TotalTindakan.StyleController = Me.LayoutControl2
        Me.txtCPPT_TotalTindakan.TabIndex = 52
        '
        'grdCPPT_Tindakan
        '
        Me.grdCPPT_Tindakan.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdCPPT_Tindakan.ContextMenuStrip = Me.ContextMenuStrip2
        Me.grdCPPT_Tindakan.DataSource = Me.BindingSourceCPPT_Tindakan
        Me.grdCPPT_Tindakan.Location = New System.Drawing.Point(2, 76)
        Me.grdCPPT_Tindakan.MainView = Me.grvCPPT_Tindakan
        Me.grdCPPT_Tindakan.Name = "grdCPPT_Tindakan"
        Me.grdCPPT_Tindakan.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.grdCPPT_KDITEMTINDAKAN, Me.grdCPPT_KDUOMTINDAKAN, Me.RepositoryItemMemoExEdit1})
        Me.grdCPPT_Tindakan.Size = New System.Drawing.Size(501, 130)
        Me.grdCPPT_Tindakan.TabIndex = 30
        Me.grdCPPT_Tindakan.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvCPPT_Tindakan})
        '
        'ContextMenuStrip2
        '
        Me.ContextMenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DeleteToolStripMenuItem1})
        Me.ContextMenuStrip2.Name = "ContextMenuStrip2"
        Me.ContextMenuStrip2.Size = New System.Drawing.Size(108, 26)
        '
        'DeleteToolStripMenuItem1
        '
        Me.DeleteToolStripMenuItem1.Name = "DeleteToolStripMenuItem1"
        Me.DeleteToolStripMenuItem1.Size = New System.Drawing.Size(107, 22)
        Me.DeleteToolStripMenuItem1.Text = "Delete"
        '
        'BindingSourceCPPT_Tindakan
        '
        Me.BindingSourceCPPT_Tindakan.DataSource = GetType(DataAccess.R_CPPT_PROSEDUR)
        '
        'grvCPPT_Tindakan
        '
        Me.grvCPPT_Tindakan.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colCPPT_KDITEMTINDAKAN, Me.colCPPT_MEMOTINDAKAN, Me.colCPPT_KDUOMTINDAKAN, Me.colCPPT_JUMLAHTINDAKAN, Me.colCPPT_HARGATINDAKAN, Me.colCPPT_TOTALTINDAKAN, Me.colCPPT_ISBACA})
        Me.grvCPPT_Tindakan.GridControl = Me.grdCPPT_Tindakan
        Me.grvCPPT_Tindakan.Name = "grvCPPT_Tindakan"
        Me.grvCPPT_Tindakan.OptionsCustomization.AllowColumnMoving = False
        Me.grvCPPT_Tindakan.OptionsCustomization.AllowFilter = False
        Me.grvCPPT_Tindakan.OptionsCustomization.AllowGroup = False
        Me.grvCPPT_Tindakan.OptionsCustomization.AllowQuickHideColumns = False
        Me.grvCPPT_Tindakan.OptionsCustomization.AllowSort = False
        Me.grvCPPT_Tindakan.OptionsDetail.EnableMasterViewMode = False
        Me.grvCPPT_Tindakan.OptionsFind.AllowFindPanel = False
        Me.grvCPPT_Tindakan.OptionsLayout.StoreAllOptions = True
        Me.grvCPPT_Tindakan.OptionsLayout.StoreAppearance = True
        Me.grvCPPT_Tindakan.OptionsMenu.EnableColumnMenu = False
        Me.grvCPPT_Tindakan.OptionsNavigation.AutoFocusNewRow = True
        Me.grvCPPT_Tindakan.OptionsNavigation.EnterMoveNextColumn = True
        Me.grvCPPT_Tindakan.OptionsView.EnableAppearanceEvenRow = True
        Me.grvCPPT_Tindakan.OptionsView.EnableAppearanceOddRow = True
        Me.grvCPPT_Tindakan.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.grvCPPT_Tindakan.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.grvCPPT_Tindakan.OptionsView.ShowGroupPanel = False
        '
        'colCPPT_KDITEMTINDAKAN
        '
        Me.colCPPT_KDITEMTINDAKAN.Caption = "Tindakan Hari Ini"
        Me.colCPPT_KDITEMTINDAKAN.ColumnEdit = Me.grdCPPT_KDITEMTINDAKAN
        Me.colCPPT_KDITEMTINDAKAN.FieldName = "KDITEM"
        Me.colCPPT_KDITEMTINDAKAN.Name = "colCPPT_KDITEMTINDAKAN"
        Me.colCPPT_KDITEMTINDAKAN.OptionsColumn.AllowEdit = False
        Me.colCPPT_KDITEMTINDAKAN.OptionsColumn.AllowFocus = False
        Me.colCPPT_KDITEMTINDAKAN.OptionsColumn.ReadOnly = True
        Me.colCPPT_KDITEMTINDAKAN.OptionsColumn.TabStop = False
        Me.colCPPT_KDITEMTINDAKAN.Visible = True
        Me.colCPPT_KDITEMTINDAKAN.VisibleIndex = 0
        Me.colCPPT_KDITEMTINDAKAN.Width = 585
        '
        'grdCPPT_KDITEMTINDAKAN
        '
        Me.grdCPPT_KDITEMTINDAKAN.AutoHeight = False
        Me.grdCPPT_KDITEMTINDAKAN.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdCPPT_KDITEMTINDAKAN.Name = "grdCPPT_KDITEMTINDAKAN"
        Me.grdCPPT_KDITEMTINDAKAN.NullText = ""
        Me.grdCPPT_KDITEMTINDAKAN.View = Me.grvCPPT_KDITEMTINDAKAN
        '
        'grvCPPT_KDITEMTINDAKAN
        '
        Me.grvCPPT_KDITEMTINDAKAN.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn8})
        Me.grvCPPT_KDITEMTINDAKAN.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvCPPT_KDITEMTINDAKAN.Name = "grvCPPT_KDITEMTINDAKAN"
        Me.grvCPPT_KDITEMTINDAKAN.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvCPPT_KDITEMTINDAKAN.OptionsView.ShowGroupPanel = False
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Tampilan Nama"
        Me.GridColumn8.FieldName = "NMITEM2"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 0
        '
        'colCPPT_MEMOTINDAKAN
        '
        Me.colCPPT_MEMOTINDAKAN.Caption = "Catatan"
        Me.colCPPT_MEMOTINDAKAN.ColumnEdit = Me.RepositoryItemMemoExEdit1
        Me.colCPPT_MEMOTINDAKAN.FieldName = "MEMO"
        Me.colCPPT_MEMOTINDAKAN.Name = "colCPPT_MEMOTINDAKAN"
        Me.colCPPT_MEMOTINDAKAN.Visible = True
        Me.colCPPT_MEMOTINDAKAN.VisibleIndex = 1
        Me.colCPPT_MEMOTINDAKAN.Width = 191
        '
        'RepositoryItemMemoExEdit1
        '
        Me.RepositoryItemMemoExEdit1.AutoHeight = False
        Me.RepositoryItemMemoExEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemMemoExEdit1.Name = "RepositoryItemMemoExEdit1"
        '
        'colCPPT_KDUOMTINDAKAN
        '
        Me.colCPPT_KDUOMTINDAKAN.Caption = "Satuan"
        Me.colCPPT_KDUOMTINDAKAN.ColumnEdit = Me.grdCPPT_KDUOMTINDAKAN
        Me.colCPPT_KDUOMTINDAKAN.FieldName = "KDUOM"
        Me.colCPPT_KDUOMTINDAKAN.Name = "colCPPT_KDUOMTINDAKAN"
        '
        'grdCPPT_KDUOMTINDAKAN
        '
        Me.grdCPPT_KDUOMTINDAKAN.AutoHeight = False
        Me.grdCPPT_KDUOMTINDAKAN.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdCPPT_KDUOMTINDAKAN.Name = "grdCPPT_KDUOMTINDAKAN"
        Me.grdCPPT_KDUOMTINDAKAN.NullText = ""
        Me.grdCPPT_KDUOMTINDAKAN.View = Me.grvCPPT_KDUOMTINDAKAN
        '
        'grvCPPT_KDUOMTINDAKAN
        '
        Me.grvCPPT_KDUOMTINDAKAN.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2})
        Me.grvCPPT_KDUOMTINDAKAN.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvCPPT_KDUOMTINDAKAN.Name = "grvCPPT_KDUOMTINDAKAN"
        Me.grvCPPT_KDUOMTINDAKAN.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvCPPT_KDUOMTINDAKAN.OptionsView.ShowGroupPanel = False
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Tampilan Nama"
        Me.GridColumn2.FieldName = "MEMO"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        '
        'colCPPT_JUMLAHTINDAKAN
        '
        Me.colCPPT_JUMLAHTINDAKAN.Caption = "Jumlah"
        Me.colCPPT_JUMLAHTINDAKAN.FieldName = "JUMLAH"
        Me.colCPPT_JUMLAHTINDAKAN.Name = "colCPPT_JUMLAHTINDAKAN"
        '
        'colCPPT_HARGATINDAKAN
        '
        Me.colCPPT_HARGATINDAKAN.Caption = "Harga"
        Me.colCPPT_HARGATINDAKAN.FieldName = "HARGA"
        Me.colCPPT_HARGATINDAKAN.Name = "colCPPT_HARGATINDAKAN"
        '
        'colCPPT_TOTALTINDAKAN
        '
        Me.colCPPT_TOTALTINDAKAN.Caption = "Total"
        Me.colCPPT_TOTALTINDAKAN.FieldName = "TOTAL"
        Me.colCPPT_TOTALTINDAKAN.Name = "colCPPT_TOTALTINDAKAN"
        '
        'colCPPT_ISBACA
        '
        Me.colCPPT_ISBACA.Caption = "Baca?"
        Me.colCPPT_ISBACA.FieldName = "ISBACA"
        Me.colCPPT_ISBACA.Name = "colCPPT_ISBACA"
        '
        'grdCPPT_TemplateTindakan
        '
        Me.grdCPPT_TemplateTindakan.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdCPPT_TemplateTindakan.EditValue = ""
        Me.grdCPPT_TemplateTindakan.Location = New System.Drawing.Point(137, 28)
        Me.grdCPPT_TemplateTindakan.Name = "grdCPPT_TemplateTindakan"
        Me.grdCPPT_TemplateTindakan.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdCPPT_TemplateTindakan.Properties.NullText = ""
        Me.grdCPPT_TemplateTindakan.Properties.View = Me.grvCPPT_TemplateTindakan
        Me.grdCPPT_TemplateTindakan.Size = New System.Drawing.Size(366, 20)
        Me.grdCPPT_TemplateTindakan.StyleController = Me.LayoutControl2
        Me.grdCPPT_TemplateTindakan.TabIndex = 27
        '
        'grvCPPT_TemplateTindakan
        '
        Me.grvCPPT_TemplateTindakan.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn13})
        Me.grvCPPT_TemplateTindakan.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvCPPT_TemplateTindakan.Name = "grvCPPT_TemplateTindakan"
        Me.grvCPPT_TemplateTindakan.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvCPPT_TemplateTindakan.OptionsView.ShowGroupPanel = False
        '
        'GridColumn13
        '
        Me.GridColumn13.Caption = "Judul"
        Me.GridColumn13.FieldName = "MEMO"
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.Visible = True
        Me.GridColumn13.VisibleIndex = 0
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup2.GroupBordersVisible = False
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem13, Me.LayoutControlItem14, Me.EmptySpaceItem4, Me.LayoutControlItem12, Me.LayoutControlItem10, Me.lKDITEM_L2, Me.LayoutControlItem1, Me.EmptySpaceItem1, Me.LayoutControlItem2, Me.EmptySpaceItem2, Me.LayoutControlItem3})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "Root"
        Me.LayoutControlGroup2.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(783, 258)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'LayoutControlItem13
        '
        Me.LayoutControlItem13.Control = Me.grdCPPT_Tindakan
        Me.LayoutControlItem13.Location = New System.Drawing.Point(0, 74)
        Me.LayoutControlItem13.Name = "LayoutControlItem13"
        Me.LayoutControlItem13.Size = New System.Drawing.Size(505, 134)
        Me.LayoutControlItem13.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem13.TextVisible = False
        '
        'LayoutControlItem14
        '
        Me.LayoutControlItem14.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem14.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem14.Control = Me.txtCPPT_TotalTindakan
        Me.LayoutControlItem14.Location = New System.Drawing.Point(197, 208)
        Me.LayoutControlItem14.Name = "LayoutControlItem14"
        Me.LayoutControlItem14.Size = New System.Drawing.Size(308, 24)
        Me.LayoutControlItem14.Text = "Total :"
        Me.LayoutControlItem14.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem14.TextSize = New System.Drawing.Size(100, 20)
        Me.LayoutControlItem14.TextToControlDistance = 5
        '
        'EmptySpaceItem4
        '
        Me.EmptySpaceItem4.AllowHotTrack = False
        Me.EmptySpaceItem4.Location = New System.Drawing.Point(0, 208)
        Me.EmptySpaceItem4.Name = "EmptySpaceItem4"
        Me.EmptySpaceItem4.Size = New System.Drawing.Size(197, 24)
        Me.EmptySpaceItem4.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem12
        '
        Me.LayoutControlItem12.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem12.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem12.Control = Me.grdCPPT_TemplateTindakan
        Me.LayoutControlItem12.Location = New System.Drawing.Point(0, 26)
        Me.LayoutControlItem12.Name = "LayoutControlItem12"
        Me.LayoutControlItem12.Size = New System.Drawing.Size(505, 24)
        Me.LayoutControlItem12.Text = "Template Tindakan :"
        Me.LayoutControlItem12.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem12.TextSize = New System.Drawing.Size(130, 20)
        Me.LayoutControlItem12.TextToControlDistance = 5
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.Control = Me.grdKDITEMALL
        Me.LayoutControlItem10.Location = New System.Drawing.Point(505, 74)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Size = New System.Drawing.Size(278, 158)
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem10.TextVisible = False
        '
        'lKDITEM_L2
        '
        Me.lKDITEM_L2.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKDITEM_L2.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKDITEM_L2.Control = Me.grdITEM_L2
        Me.lKDITEM_L2.Location = New System.Drawing.Point(505, 26)
        Me.lKDITEM_L2.Name = "lKDITEM_L2"
        Me.lKDITEM_L2.Size = New System.Drawing.Size(278, 48)
        Me.lKDITEM_L2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKDITEM_L2.TextSize = New System.Drawing.Size(100, 20)
        Me.lKDITEM_L2.TextToControlDistance = 5
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.btnTindakanTerakhir
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(139, 26)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(139, 0)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(644, 26)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.btnObatTerakhir
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 232)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(144, 26)
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.AllowHotTrack = False
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(144, 232)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(639, 26)
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem3.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem3.Control = Me.grdSemuaTindakan
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 50)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(505, 24)
        Me.LayoutControlItem3.Text = "Semua Tindakan :"
        Me.LayoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(130, 20)
        Me.LayoutControlItem3.TextToControlDistance = 5
        '
        'XtraTabControlCPPT_Resep
        '
        Me.XtraTabControlCPPT_Resep.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XtraTabControlCPPT_Resep.Location = New System.Drawing.Point(9, 287)
        Me.XtraTabControlCPPT_Resep.Name = "XtraTabControlCPPT_Resep"
        Me.XtraTabControlCPPT_Resep.SelectedTabPage = Me.tabCPPT_NonRacikan
        Me.XtraTabControlCPPT_Resep.Size = New System.Drawing.Size(780, 314)
        Me.XtraTabControlCPPT_Resep.TabIndex = 72
        Me.XtraTabControlCPPT_Resep.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tabCPPT_NonRacikan, Me.tabCPPT_Racikan})
        '
        'tabCPPT_NonRacikan
        '
        Me.tabCPPT_NonRacikan.Controls.Add(Me.LayoutControl11)
        Me.tabCPPT_NonRacikan.Name = "tabCPPT_NonRacikan"
        Me.tabCPPT_NonRacikan.Size = New System.Drawing.Size(774, 286)
        Me.tabCPPT_NonRacikan.Text = "Non Racikan"
        '
        'LayoutControl11
        '
        Me.LayoutControl11.Controls.Add(Me.txtCPPT_TotalNonRacikanNonPaket)
        Me.LayoutControl11.Controls.Add(Me.grdCPPT_ResepNonRacikan)
        Me.LayoutControl11.Controls.Add(Me.btnRiwayatPemberianObat)
        Me.LayoutControl11.Controls.Add(Me.grdTEMPLATE)
        Me.LayoutControl11.Controls.Add(Me.txtCPPT_TotalNonRacikanPaket)
        Me.LayoutControl11.Controls.Add(Me.btnRiwayatPemberianResep)
        Me.LayoutControl11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl11.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl11.Name = "LayoutControl11"
        Me.LayoutControl11.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(973, 141, 250, 350)
        Me.LayoutControl11.Root = Me.LayoutControlGroup11
        Me.LayoutControl11.Size = New System.Drawing.Size(774, 286)
        Me.LayoutControl11.TabIndex = 0
        Me.LayoutControl11.Text = "LayoutControl11"
        '
        'txtCPPT_TotalNonRacikanNonPaket
        '
        Me.txtCPPT_TotalNonRacikanNonPaket.EditValue = "0"
        Me.txtCPPT_TotalNonRacikanNonPaket.Location = New System.Drawing.Point(107, 264)
        Me.txtCPPT_TotalNonRacikanNonPaket.Name = "txtCPPT_TotalNonRacikanNonPaket"
        Me.txtCPPT_TotalNonRacikanNonPaket.Properties.Appearance.Options.UseTextOptions = True
        Me.txtCPPT_TotalNonRacikanNonPaket.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtCPPT_TotalNonRacikanNonPaket.Properties.Mask.EditMask = "n0"
        Me.txtCPPT_TotalNonRacikanNonPaket.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtCPPT_TotalNonRacikanNonPaket.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtCPPT_TotalNonRacikanNonPaket.Properties.ReadOnly = True
        Me.txtCPPT_TotalNonRacikanNonPaket.Size = New System.Drawing.Size(278, 20)
        Me.txtCPPT_TotalNonRacikanNonPaket.StyleController = Me.LayoutControl11
        Me.txtCPPT_TotalNonRacikanNonPaket.TabIndex = 52
        '
        'grdCPPT_ResepNonRacikan
        '
        Me.grdCPPT_ResepNonRacikan.ContextMenuStrip = Me.ContextMenuStrip3
        Me.grdCPPT_ResepNonRacikan.DataSource = Me.BindingSourceCPPT_NonRacikan
        Me.grdCPPT_ResepNonRacikan.Location = New System.Drawing.Point(2, 28)
        Me.grdCPPT_ResepNonRacikan.MainView = Me.grvCPPT_ResepNonRacikan
        Me.grdCPPT_ResepNonRacikan.Name = "grdCPPT_ResepNonRacikan"
        Me.grdCPPT_ResepNonRacikan.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.grdCPPT_KDITEMNONRACIKAN, Me.grdCPPT_KDUOMNONRACIKAN, Me.grdCPPT_KDSIGNANONRACIKAN, Me.grdCPPT_KDCARAPAKAINONRACIKAN})
        Me.grdCPPT_ResepNonRacikan.Size = New System.Drawing.Size(770, 232)
        Me.grdCPPT_ResepNonRacikan.TabIndex = 19
        Me.grdCPPT_ResepNonRacikan.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvCPPT_ResepNonRacikan})
        '
        'ContextMenuStrip3
        '
        Me.ContextMenuStrip3.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DeleteToolStripMenuItem2})
        Me.ContextMenuStrip3.Name = "ContextMenuStrip3"
        Me.ContextMenuStrip3.Size = New System.Drawing.Size(108, 26)
        '
        'DeleteToolStripMenuItem2
        '
        Me.DeleteToolStripMenuItem2.Name = "DeleteToolStripMenuItem2"
        Me.DeleteToolStripMenuItem2.Size = New System.Drawing.Size(107, 22)
        Me.DeleteToolStripMenuItem2.Text = "Delete"
        '
        'BindingSourceCPPT_NonRacikan
        '
        Me.BindingSourceCPPT_NonRacikan.DataSource = GetType(DataAccess.R_CPPT_NONRACIKAN)
        '
        'grvCPPT_ResepNonRacikan
        '
        Me.grvCPPT_ResepNonRacikan.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colCPPT_KDITEMNONRACIKAN, Me.colCPPT_ISKRONISNONRACIKAN, Me.colCPPT_KDUOMNONRACIKAN, Me.colCPPT_KDSIGNANONRACIKAN, Me.colCPPT_KDCARAPAKAINONRACIKAN, Me.colCPPT_JUMLAHNONRACIKAN, Me.colCPPT_JUMLAH_PAKETNONRACIKAN, Me.colCPPT_JUMLAH_NONPAKETNONRACIKAN, Me.colCPPT_HARGANONRACIKAN, Me.colCPPT_TOTAL_PAKETNONRACIKAN, Me.colCPPT_TOTAL_NONPAKETNONRACIKAN, Me.colCPPT_TOTALNONRACIKAN, Me.colCPPT_REMARKS_DOKTERNONRACIKAN, Me.colCPPT_ISALKESNONRACIKAN, Me.colCPPT_ISBACANONRACIKAN})
        Me.grvCPPT_ResepNonRacikan.GridControl = Me.grdCPPT_ResepNonRacikan
        Me.grvCPPT_ResepNonRacikan.Name = "grvCPPT_ResepNonRacikan"
        Me.grvCPPT_ResepNonRacikan.OptionsCustomization.AllowColumnMoving = False
        Me.grvCPPT_ResepNonRacikan.OptionsCustomization.AllowFilter = False
        Me.grvCPPT_ResepNonRacikan.OptionsCustomization.AllowGroup = False
        Me.grvCPPT_ResepNonRacikan.OptionsCustomization.AllowQuickHideColumns = False
        Me.grvCPPT_ResepNonRacikan.OptionsCustomization.AllowSort = False
        Me.grvCPPT_ResepNonRacikan.OptionsDetail.EnableMasterViewMode = False
        Me.grvCPPT_ResepNonRacikan.OptionsFind.AllowFindPanel = False
        Me.grvCPPT_ResepNonRacikan.OptionsLayout.StoreAllOptions = True
        Me.grvCPPT_ResepNonRacikan.OptionsLayout.StoreAppearance = True
        Me.grvCPPT_ResepNonRacikan.OptionsMenu.EnableColumnMenu = False
        Me.grvCPPT_ResepNonRacikan.OptionsNavigation.AutoFocusNewRow = True
        Me.grvCPPT_ResepNonRacikan.OptionsNavigation.EnterMoveNextColumn = True
        Me.grvCPPT_ResepNonRacikan.OptionsView.EnableAppearanceEvenRow = True
        Me.grvCPPT_ResepNonRacikan.OptionsView.EnableAppearanceOddRow = True
        Me.grvCPPT_ResepNonRacikan.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.grvCPPT_ResepNonRacikan.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.grvCPPT_ResepNonRacikan.OptionsView.ShowGroupPanel = False
        '
        'colCPPT_KDITEMNONRACIKAN
        '
        Me.colCPPT_KDITEMNONRACIKAN.Caption = "Obat"
        Me.colCPPT_KDITEMNONRACIKAN.ColumnEdit = Me.grdCPPT_KDITEMNONRACIKAN
        Me.colCPPT_KDITEMNONRACIKAN.FieldName = "KDITEM"
        Me.colCPPT_KDITEMNONRACIKAN.Name = "colCPPT_KDITEMNONRACIKAN"
        Me.colCPPT_KDITEMNONRACIKAN.Visible = True
        Me.colCPPT_KDITEMNONRACIKAN.VisibleIndex = 0
        Me.colCPPT_KDITEMNONRACIKAN.Width = 182
        '
        'grdCPPT_KDITEMNONRACIKAN
        '
        Me.grdCPPT_KDITEMNONRACIKAN.AutoHeight = False
        Me.grdCPPT_KDITEMNONRACIKAN.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdCPPT_KDITEMNONRACIKAN.Name = "grdCPPT_KDITEMNONRACIKAN"
        Me.grdCPPT_KDITEMNONRACIKAN.NullText = ""
        Me.grdCPPT_KDITEMNONRACIKAN.PopupFormMinSize = New System.Drawing.Size(950, 500)
        Me.grdCPPT_KDITEMNONRACIKAN.View = Me.grvCPPT_KDITEMNONRACIKAN
        '
        'grvCPPT_KDITEMNONRACIKAN
        '
        Me.grvCPPT_KDITEMNONRACIKAN.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn5, Me.GridColumn9, Me.GridColumn10, Me.GridColumn11, Me.GridColumn12})
        Me.grvCPPT_KDITEMNONRACIKAN.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvCPPT_KDITEMNONRACIKAN.Name = "grvCPPT_KDITEMNONRACIKAN"
        Me.grvCPPT_KDITEMNONRACIKAN.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvCPPT_KDITEMNONRACIKAN.OptionsView.ShowGroupPanel = False
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Generik"
        Me.GridColumn5.FieldName = "NMITEM1"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Width = 242
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Paten"
        Me.GridColumn9.FieldName = "NMITEM2"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 0
        Me.GridColumn9.Width = 239
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Harga Beli"
        Me.GridColumn10.DisplayFormat.FormatString = "{0:n0}"
        Me.GridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn10.FieldName = "HARGA"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.Width = 89
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Satuan"
        Me.GridColumn11.FieldName = "SATUAN"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 1
        Me.GridColumn11.Width = 89
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "Stok"
        Me.GridColumn12.DisplayFormat.FormatString = "{0:n0}"
        Me.GridColumn12.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn12.FieldName = "STOK"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 2
        Me.GridColumn12.Width = 95
        '
        'colCPPT_ISKRONISNONRACIKAN
        '
        Me.colCPPT_ISKRONISNONRACIKAN.Caption = "Kronis?"
        Me.colCPPT_ISKRONISNONRACIKAN.FieldName = "ISKRONIS"
        Me.colCPPT_ISKRONISNONRACIKAN.Name = "colCPPT_ISKRONISNONRACIKAN"
        '
        'colCPPT_KDUOMNONRACIKAN
        '
        Me.colCPPT_KDUOMNONRACIKAN.Caption = "Satuan"
        Me.colCPPT_KDUOMNONRACIKAN.ColumnEdit = Me.grdCPPT_KDUOMNONRACIKAN
        Me.colCPPT_KDUOMNONRACIKAN.FieldName = "KDUOM"
        Me.colCPPT_KDUOMNONRACIKAN.Name = "colCPPT_KDUOMNONRACIKAN"
        Me.colCPPT_KDUOMNONRACIKAN.OptionsColumn.AllowEdit = False
        Me.colCPPT_KDUOMNONRACIKAN.OptionsColumn.AllowFocus = False
        Me.colCPPT_KDUOMNONRACIKAN.OptionsColumn.ReadOnly = True
        Me.colCPPT_KDUOMNONRACIKAN.OptionsColumn.TabStop = False
        Me.colCPPT_KDUOMNONRACIKAN.Visible = True
        Me.colCPPT_KDUOMNONRACIKAN.VisibleIndex = 1
        Me.colCPPT_KDUOMNONRACIKAN.Width = 88
        '
        'grdCPPT_KDUOMNONRACIKAN
        '
        Me.grdCPPT_KDUOMNONRACIKAN.AutoHeight = False
        Me.grdCPPT_KDUOMNONRACIKAN.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdCPPT_KDUOMNONRACIKAN.Name = "grdCPPT_KDUOMNONRACIKAN"
        Me.grdCPPT_KDUOMNONRACIKAN.NullText = ""
        Me.grdCPPT_KDUOMNONRACIKAN.View = Me.grvCPPT_KDUOMNONRACIKAN
        '
        'grvCPPT_KDUOMNONRACIKAN
        '
        Me.grvCPPT_KDUOMNONRACIKAN.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn14})
        Me.grvCPPT_KDUOMNONRACIKAN.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvCPPT_KDUOMNONRACIKAN.Name = "grvCPPT_KDUOMNONRACIKAN"
        Me.grvCPPT_KDUOMNONRACIKAN.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvCPPT_KDUOMNONRACIKAN.OptionsView.ShowGroupPanel = False
        '
        'GridColumn14
        '
        Me.GridColumn14.Caption = "Tampilan Nama"
        Me.GridColumn14.FieldName = "MEMO"
        Me.GridColumn14.Name = "GridColumn14"
        Me.GridColumn14.Visible = True
        Me.GridColumn14.VisibleIndex = 0
        '
        'colCPPT_KDSIGNANONRACIKAN
        '
        Me.colCPPT_KDSIGNANONRACIKAN.Caption = "Signa"
        Me.colCPPT_KDSIGNANONRACIKAN.ColumnEdit = Me.grdCPPT_KDSIGNANONRACIKAN
        Me.colCPPT_KDSIGNANONRACIKAN.FieldName = "KDSIGNA"
        Me.colCPPT_KDSIGNANONRACIKAN.Name = "colCPPT_KDSIGNANONRACIKAN"
        Me.colCPPT_KDSIGNANONRACIKAN.Visible = True
        Me.colCPPT_KDSIGNANONRACIKAN.VisibleIndex = 2
        Me.colCPPT_KDSIGNANONRACIKAN.Width = 88
        '
        'grdCPPT_KDSIGNANONRACIKAN
        '
        Me.grdCPPT_KDSIGNANONRACIKAN.AutoHeight = False
        Me.grdCPPT_KDSIGNANONRACIKAN.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdCPPT_KDSIGNANONRACIKAN.Name = "grdCPPT_KDSIGNANONRACIKAN"
        Me.grdCPPT_KDSIGNANONRACIKAN.NullText = ""
        Me.grdCPPT_KDSIGNANONRACIKAN.View = Me.grvCPPT_KDSIGNANONRACIKAN
        '
        'grvCPPT_KDSIGNANONRACIKAN
        '
        Me.grvCPPT_KDSIGNANONRACIKAN.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn15})
        Me.grvCPPT_KDSIGNANONRACIKAN.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvCPPT_KDSIGNANONRACIKAN.Name = "grvCPPT_KDSIGNANONRACIKAN"
        Me.grvCPPT_KDSIGNANONRACIKAN.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvCPPT_KDSIGNANONRACIKAN.OptionsView.ShowGroupPanel = False
        '
        'GridColumn15
        '
        Me.GridColumn15.Caption = "Tampilan Nama"
        Me.GridColumn15.FieldName = "MEMO"
        Me.GridColumn15.Name = "GridColumn15"
        Me.GridColumn15.Visible = True
        Me.GridColumn15.VisibleIndex = 0
        '
        'colCPPT_KDCARAPAKAINONRACIKAN
        '
        Me.colCPPT_KDCARAPAKAINONRACIKAN.Caption = "Cara Pakai"
        Me.colCPPT_KDCARAPAKAINONRACIKAN.ColumnEdit = Me.grdCPPT_KDCARAPAKAINONRACIKAN
        Me.colCPPT_KDCARAPAKAINONRACIKAN.FieldName = "KDCARAPAKAI"
        Me.colCPPT_KDCARAPAKAINONRACIKAN.Name = "colCPPT_KDCARAPAKAINONRACIKAN"
        Me.colCPPT_KDCARAPAKAINONRACIKAN.Visible = True
        Me.colCPPT_KDCARAPAKAINONRACIKAN.VisibleIndex = 3
        Me.colCPPT_KDCARAPAKAINONRACIKAN.Width = 88
        '
        'grdCPPT_KDCARAPAKAINONRACIKAN
        '
        Me.grdCPPT_KDCARAPAKAINONRACIKAN.AutoHeight = False
        Me.grdCPPT_KDCARAPAKAINONRACIKAN.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdCPPT_KDCARAPAKAINONRACIKAN.Name = "grdCPPT_KDCARAPAKAINONRACIKAN"
        Me.grdCPPT_KDCARAPAKAINONRACIKAN.NullText = ""
        Me.grdCPPT_KDCARAPAKAINONRACIKAN.View = Me.grvCPPT_KDCARAPAKAINONRACIKAN
        '
        'grvCPPT_KDCARAPAKAINONRACIKAN
        '
        Me.grvCPPT_KDCARAPAKAINONRACIKAN.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn16})
        Me.grvCPPT_KDCARAPAKAINONRACIKAN.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvCPPT_KDCARAPAKAINONRACIKAN.Name = "grvCPPT_KDCARAPAKAINONRACIKAN"
        Me.grvCPPT_KDCARAPAKAINONRACIKAN.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvCPPT_KDCARAPAKAINONRACIKAN.OptionsView.ShowGroupPanel = False
        '
        'GridColumn16
        '
        Me.GridColumn16.Caption = "Tampilan Nama"
        Me.GridColumn16.FieldName = "MEMO"
        Me.GridColumn16.Name = "GridColumn16"
        Me.GridColumn16.Visible = True
        Me.GridColumn16.VisibleIndex = 0
        '
        'colCPPT_JUMLAHNONRACIKAN
        '
        Me.colCPPT_JUMLAHNONRACIKAN.Caption = "Jumlah"
        Me.colCPPT_JUMLAHNONRACIKAN.DisplayFormat.FormatString = "{0:n0}"
        Me.colCPPT_JUMLAHNONRACIKAN.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colCPPT_JUMLAHNONRACIKAN.FieldName = "JUMLAH"
        Me.colCPPT_JUMLAHNONRACIKAN.Name = "colCPPT_JUMLAHNONRACIKAN"
        Me.colCPPT_JUMLAHNONRACIKAN.Visible = True
        Me.colCPPT_JUMLAHNONRACIKAN.VisibleIndex = 4
        Me.colCPPT_JUMLAHNONRACIKAN.Width = 88
        '
        'colCPPT_JUMLAH_PAKETNONRACIKAN
        '
        Me.colCPPT_JUMLAH_PAKETNONRACIKAN.Caption = "Paket"
        Me.colCPPT_JUMLAH_PAKETNONRACIKAN.DisplayFormat.FormatString = "{0:n0}"
        Me.colCPPT_JUMLAH_PAKETNONRACIKAN.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colCPPT_JUMLAH_PAKETNONRACIKAN.FieldName = "JUMLAH_PAKET"
        Me.colCPPT_JUMLAH_PAKETNONRACIKAN.Name = "colCPPT_JUMLAH_PAKETNONRACIKAN"
        Me.colCPPT_JUMLAH_PAKETNONRACIKAN.Visible = True
        Me.colCPPT_JUMLAH_PAKETNONRACIKAN.VisibleIndex = 5
        '
        'colCPPT_JUMLAH_NONPAKETNONRACIKAN
        '
        Me.colCPPT_JUMLAH_NONPAKETNONRACIKAN.Caption = "Non Paket"
        Me.colCPPT_JUMLAH_NONPAKETNONRACIKAN.DisplayFormat.FormatString = "{0:n0}"
        Me.colCPPT_JUMLAH_NONPAKETNONRACIKAN.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colCPPT_JUMLAH_NONPAKETNONRACIKAN.FieldName = "JUMLAH_NONPAKET"
        Me.colCPPT_JUMLAH_NONPAKETNONRACIKAN.Name = "colCPPT_JUMLAH_NONPAKETNONRACIKAN"
        Me.colCPPT_JUMLAH_NONPAKETNONRACIKAN.Visible = True
        Me.colCPPT_JUMLAH_NONPAKETNONRACIKAN.VisibleIndex = 6
        '
        'colCPPT_HARGANONRACIKAN
        '
        Me.colCPPT_HARGANONRACIKAN.Caption = "Harga"
        Me.colCPPT_HARGANONRACIKAN.DisplayFormat.FormatString = "{0:n0}"
        Me.colCPPT_HARGANONRACIKAN.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colCPPT_HARGANONRACIKAN.FieldName = "HARGA"
        Me.colCPPT_HARGANONRACIKAN.Name = "colCPPT_HARGANONRACIKAN"
        Me.colCPPT_HARGANONRACIKAN.Visible = True
        Me.colCPPT_HARGANONRACIKAN.VisibleIndex = 10
        '
        'colCPPT_TOTAL_PAKETNONRACIKAN
        '
        Me.colCPPT_TOTAL_PAKETNONRACIKAN.Caption = "Total Paket"
        Me.colCPPT_TOTAL_PAKETNONRACIKAN.FieldName = "TOTAL_PAKET"
        Me.colCPPT_TOTAL_PAKETNONRACIKAN.Name = "colCPPT_TOTAL_PAKETNONRACIKAN"
        Me.colCPPT_TOTAL_PAKETNONRACIKAN.Visible = True
        Me.colCPPT_TOTAL_PAKETNONRACIKAN.VisibleIndex = 9
        '
        'colCPPT_TOTAL_NONPAKETNONRACIKAN
        '
        Me.colCPPT_TOTAL_NONPAKETNONRACIKAN.Caption = "Total Non Paket"
        Me.colCPPT_TOTAL_NONPAKETNONRACIKAN.FieldName = "TOTAL_NONPAKET"
        Me.colCPPT_TOTAL_NONPAKETNONRACIKAN.Name = "colCPPT_TOTAL_NONPAKETNONRACIKAN"
        Me.colCPPT_TOTAL_NONPAKETNONRACIKAN.Visible = True
        Me.colCPPT_TOTAL_NONPAKETNONRACIKAN.VisibleIndex = 8
        '
        'colCPPT_TOTALNONRACIKAN
        '
        Me.colCPPT_TOTALNONRACIKAN.Caption = "Total"
        Me.colCPPT_TOTALNONRACIKAN.DisplayFormat.FormatString = "{0:n0}"
        Me.colCPPT_TOTALNONRACIKAN.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colCPPT_TOTALNONRACIKAN.FieldName = "TOTAL"
        Me.colCPPT_TOTALNONRACIKAN.Name = "colCPPT_TOTALNONRACIKAN"
        Me.colCPPT_TOTALNONRACIKAN.Visible = True
        Me.colCPPT_TOTALNONRACIKAN.VisibleIndex = 11
        '
        'colCPPT_REMARKS_DOKTERNONRACIKAN
        '
        Me.colCPPT_REMARKS_DOKTERNONRACIKAN.Caption = "Catatan"
        Me.colCPPT_REMARKS_DOKTERNONRACIKAN.FieldName = "REMARKS_DOKTER"
        Me.colCPPT_REMARKS_DOKTERNONRACIKAN.Name = "colCPPT_REMARKS_DOKTERNONRACIKAN"
        Me.colCPPT_REMARKS_DOKTERNONRACIKAN.Visible = True
        Me.colCPPT_REMARKS_DOKTERNONRACIKAN.VisibleIndex = 7
        Me.colCPPT_REMARKS_DOKTERNONRACIKAN.Width = 95
        '
        'colCPPT_ISALKESNONRACIKAN
        '
        Me.colCPPT_ISALKESNONRACIKAN.Caption = "Alkes?"
        Me.colCPPT_ISALKESNONRACIKAN.FieldName = "ISALKES"
        Me.colCPPT_ISALKESNONRACIKAN.Name = "colCPPT_ISALKESNONRACIKAN"
        '
        'colCPPT_ISBACANONRACIKAN
        '
        Me.colCPPT_ISBACANONRACIKAN.Caption = "Baca"
        Me.colCPPT_ISBACANONRACIKAN.FieldName = "ISBACA"
        Me.colCPPT_ISBACANONRACIKAN.Name = "colCPPT_ISBACANONRACIKAN"
        '
        'btnRiwayatPemberianObat
        '
        Me.btnRiwayatPemberianObat.Appearance.BorderColor = System.Drawing.Color.Blue
        Me.btnRiwayatPemberianObat.Appearance.ForeColor = System.Drawing.Color.Black
        Me.btnRiwayatPemberianObat.Appearance.Options.UseBorderColor = True
        Me.btnRiwayatPemberianObat.Appearance.Options.UseForeColor = True
        Me.btnRiwayatPemberianObat.Location = New System.Drawing.Point(169, 2)
        Me.btnRiwayatPemberianObat.Name = "btnRiwayatPemberianObat"
        Me.btnRiwayatPemberianObat.Size = New System.Drawing.Size(156, 22)
        Me.btnRiwayatPemberianObat.StyleController = Me.LayoutControl11
        Me.btnRiwayatPemberianObat.TabIndex = 70
        Me.btnRiwayatPemberianObat.Text = "Riwayat Pemberian Obat"
        '
        'grdTEMPLATE
        '
        Me.grdTEMPLATE.EnterMoveNextControl = True
        Me.grdTEMPLATE.Location = New System.Drawing.Point(434, 2)
        Me.grdTEMPLATE.Name = "grdTEMPLATE"
        Me.grdTEMPLATE.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdTEMPLATE.Properties.NullText = ""
        Me.grdTEMPLATE.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdTEMPLATE.Properties.View = Me.grvWAREHOUSE
        Me.grdTEMPLATE.Size = New System.Drawing.Size(338, 20)
        Me.grdTEMPLATE.StyleController = Me.LayoutControl11
        Me.grdTEMPLATE.TabIndex = 35
        '
        'grvWAREHOUSE
        '
        Me.grvWAREHOUSE.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn6})
        Me.grvWAREHOUSE.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvWAREHOUSE.Name = "grvWAREHOUSE"
        Me.grvWAREHOUSE.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvWAREHOUSE.OptionsView.ShowAutoFilterRow = True
        Me.grvWAREHOUSE.OptionsView.ShowGroupPanel = False
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Tampilan Nama"
        Me.GridColumn6.FieldName = "DESCRIPTION"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 0
        '
        'txtCPPT_TotalNonRacikanPaket
        '
        Me.txtCPPT_TotalNonRacikanPaket.EditValue = "0"
        Me.txtCPPT_TotalNonRacikanPaket.Location = New System.Drawing.Point(494, 264)
        Me.txtCPPT_TotalNonRacikanPaket.Name = "txtCPPT_TotalNonRacikanPaket"
        Me.txtCPPT_TotalNonRacikanPaket.Properties.Appearance.Options.UseTextOptions = True
        Me.txtCPPT_TotalNonRacikanPaket.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtCPPT_TotalNonRacikanPaket.Properties.Mask.EditMask = "n0"
        Me.txtCPPT_TotalNonRacikanPaket.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtCPPT_TotalNonRacikanPaket.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtCPPT_TotalNonRacikanPaket.Properties.ReadOnly = True
        Me.txtCPPT_TotalNonRacikanPaket.Size = New System.Drawing.Size(278, 20)
        Me.txtCPPT_TotalNonRacikanPaket.StyleController = Me.LayoutControl11
        Me.txtCPPT_TotalNonRacikanPaket.TabIndex = 51
        '
        'btnRiwayatPemberianResep
        '
        Me.btnRiwayatPemberianResep.Appearance.BorderColor = System.Drawing.Color.Blue
        Me.btnRiwayatPemberianResep.Appearance.ForeColor = System.Drawing.Color.Black
        Me.btnRiwayatPemberianResep.Appearance.Options.UseBorderColor = True
        Me.btnRiwayatPemberianResep.Appearance.Options.UseForeColor = True
        Me.btnRiwayatPemberianResep.Location = New System.Drawing.Point(2, 2)
        Me.btnRiwayatPemberianResep.Name = "btnRiwayatPemberianResep"
        Me.btnRiwayatPemberianResep.Size = New System.Drawing.Size(163, 22)
        Me.btnRiwayatPemberianResep.StyleController = Me.LayoutControl11
        Me.btnRiwayatPemberianResep.TabIndex = 69
        Me.btnRiwayatPemberianResep.Text = "Riwayat Pemberian Resep"
        '
        'LayoutControlGroup11
        '
        Me.LayoutControlGroup11.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup11.GroupBordersVisible = False
        Me.LayoutControlGroup11.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem24, Me.LayoutControlItem9, Me.LayoutControlItem66, Me.LayoutControlItem7, Me.LayoutControlItem20, Me.LayoutControlItem8})
        Me.LayoutControlGroup11.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup11.Name = "Root"
        Me.LayoutControlGroup11.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup11.Size = New System.Drawing.Size(774, 286)
        Me.LayoutControlGroup11.TextVisible = False
        '
        'LayoutControlItem24
        '
        Me.LayoutControlItem24.Control = Me.btnRiwayatPemberianResep
        Me.LayoutControlItem24.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem24.Name = "LayoutControlItem24"
        Me.LayoutControlItem24.Size = New System.Drawing.Size(167, 26)
        Me.LayoutControlItem24.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem24.TextVisible = False
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem9.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem9.Control = Me.grdTEMPLATE
        Me.LayoutControlItem9.Location = New System.Drawing.Point(327, 0)
        Me.LayoutControlItem9.Name = "LayoutControlItem2"
        Me.LayoutControlItem9.Size = New System.Drawing.Size(447, 26)
        Me.LayoutControlItem9.Text = "Template Resep :"
        Me.LayoutControlItem9.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(100, 20)
        Me.LayoutControlItem9.TextToControlDistance = 5
        '
        'LayoutControlItem66
        '
        Me.LayoutControlItem66.Control = Me.btnRiwayatPemberianObat
        Me.LayoutControlItem66.Location = New System.Drawing.Point(167, 0)
        Me.LayoutControlItem66.Name = "LayoutControlItem66"
        Me.LayoutControlItem66.Size = New System.Drawing.Size(160, 26)
        Me.LayoutControlItem66.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem66.TextVisible = False
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.grdCPPT_ResepNonRacikan
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 26)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(774, 236)
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem7.TextVisible = False
        '
        'LayoutControlItem20
        '
        Me.LayoutControlItem20.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem20.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem20.Control = Me.txtCPPT_TotalNonRacikanNonPaket
        Me.LayoutControlItem20.Location = New System.Drawing.Point(0, 262)
        Me.LayoutControlItem20.Name = "LayoutControlItem20"
        Me.LayoutControlItem20.Size = New System.Drawing.Size(387, 24)
        Me.LayoutControlItem20.Text = "Total Non Paket :"
        Me.LayoutControlItem20.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem20.TextSize = New System.Drawing.Size(100, 20)
        Me.LayoutControlItem20.TextToControlDistance = 5
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem8.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem8.Control = Me.txtCPPT_TotalNonRacikanPaket
        Me.LayoutControlItem8.Location = New System.Drawing.Point(387, 262)
        Me.LayoutControlItem8.Name = "LayoutControlItem3"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(387, 24)
        Me.LayoutControlItem8.Text = "Total Paket :"
        Me.LayoutControlItem8.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(100, 20)
        Me.LayoutControlItem8.TextToControlDistance = 5
        '
        'tabCPPT_Racikan
        '
        Me.tabCPPT_Racikan.Controls.Add(Me.LayoutControl4)
        Me.tabCPPT_Racikan.Name = "tabCPPT_Racikan"
        Me.tabCPPT_Racikan.Size = New System.Drawing.Size(774, 286)
        Me.tabCPPT_Racikan.Text = "Racikan"
        '
        'LayoutControl4
        '
        Me.LayoutControl4.Controls.Add(Me.txtCPPT_TotalRacikanNonPaket)
        Me.LayoutControl4.Controls.Add(Me.grdCPPT_ResepRacikan)
        Me.LayoutControl4.Controls.Add(Me.txtCPPT_TotalRacikanPaket)
        Me.LayoutControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl4.Name = "LayoutControl4"
        Me.LayoutControl4.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(878, 428, 250, 350)
        Me.LayoutControl4.Root = Me.LayoutControlGroup4
        Me.LayoutControl4.Size = New System.Drawing.Size(774, 286)
        Me.LayoutControl4.TabIndex = 2
        Me.LayoutControl4.Text = "LayoutControl4"
        '
        'txtCPPT_TotalRacikanNonPaket
        '
        Me.txtCPPT_TotalRacikanNonPaket.EditValue = "0"
        Me.txtCPPT_TotalRacikanNonPaket.Location = New System.Drawing.Point(497, 264)
        Me.txtCPPT_TotalRacikanNonPaket.Name = "txtCPPT_TotalRacikanNonPaket"
        Me.txtCPPT_TotalRacikanNonPaket.Properties.Appearance.Options.UseTextOptions = True
        Me.txtCPPT_TotalRacikanNonPaket.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtCPPT_TotalRacikanNonPaket.Properties.Mask.EditMask = "n0"
        Me.txtCPPT_TotalRacikanNonPaket.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtCPPT_TotalRacikanNonPaket.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtCPPT_TotalRacikanNonPaket.Properties.ReadOnly = True
        Me.txtCPPT_TotalRacikanNonPaket.Size = New System.Drawing.Size(275, 20)
        Me.txtCPPT_TotalRacikanNonPaket.StyleController = Me.LayoutControl4
        Me.txtCPPT_TotalRacikanNonPaket.TabIndex = 97
        '
        'grdCPPT_ResepRacikan
        '
        Me.grdCPPT_ResepRacikan.ContextMenuStrip = Me.ContextMenuStrip4
        Me.grdCPPT_ResepRacikan.DataSource = Me.BindingSourceCPPT_Racikan
        Me.grdCPPT_ResepRacikan.Location = New System.Drawing.Point(2, 2)
        Me.grdCPPT_ResepRacikan.MainView = Me.grvCPPT_ResepRacikan
        Me.grdCPPT_ResepRacikan.Name = "grdCPPT_ResepRacikan"
        Me.grdCPPT_ResepRacikan.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.grdCPPT_KDITEMRACIKAN, Me.grdCPPT_KDUOMRACIKAN})
        Me.grdCPPT_ResepRacikan.Size = New System.Drawing.Size(770, 258)
        Me.grdCPPT_ResepRacikan.TabIndex = 20
        Me.grdCPPT_ResepRacikan.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvCPPT_ResepRacikan})
        '
        'ContextMenuStrip4
        '
        Me.ContextMenuStrip4.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DeleteToolStripMenuItem3})
        Me.ContextMenuStrip4.Name = "ContextMenuStrip4"
        Me.ContextMenuStrip4.Size = New System.Drawing.Size(108, 26)
        '
        'DeleteToolStripMenuItem3
        '
        Me.DeleteToolStripMenuItem3.Name = "DeleteToolStripMenuItem3"
        Me.DeleteToolStripMenuItem3.Size = New System.Drawing.Size(107, 22)
        Me.DeleteToolStripMenuItem3.Text = "Delete"
        '
        'BindingSourceCPPT_Racikan
        '
        Me.BindingSourceCPPT_Racikan.DataSource = GetType(DataAccess.R_CPPT_RACIKAN)
        '
        'grvCPPT_ResepRacikan
        '
        Me.grvCPPT_ResepRacikan.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colCPPT_KDITEMRACIKAN, Me.colCPPT_KDUOMRACIKAN, Me.colCPPT_SIGNARACIKAN, Me.colCPPT_PERMINTAANRACIKAN, Me.colCPPT_JUMLAHRACIKAN, Me.colCPPT_HARGARACIKAN, Me.colCPPT_TOTALRACIKAN, Me.colCPPT_REMARKSRACIKAN, Me.colCPPT_ISBACARACIKAN})
        Me.grvCPPT_ResepRacikan.GridControl = Me.grdCPPT_ResepRacikan
        Me.grvCPPT_ResepRacikan.Name = "grvCPPT_ResepRacikan"
        Me.grvCPPT_ResepRacikan.OptionsCustomization.AllowColumnMoving = False
        Me.grvCPPT_ResepRacikan.OptionsCustomization.AllowFilter = False
        Me.grvCPPT_ResepRacikan.OptionsCustomization.AllowGroup = False
        Me.grvCPPT_ResepRacikan.OptionsCustomization.AllowQuickHideColumns = False
        Me.grvCPPT_ResepRacikan.OptionsCustomization.AllowSort = False
        Me.grvCPPT_ResepRacikan.OptionsDetail.EnableMasterViewMode = False
        Me.grvCPPT_ResepRacikan.OptionsFind.AllowFindPanel = False
        Me.grvCPPT_ResepRacikan.OptionsLayout.StoreAllOptions = True
        Me.grvCPPT_ResepRacikan.OptionsLayout.StoreAppearance = True
        Me.grvCPPT_ResepRacikan.OptionsMenu.EnableColumnMenu = False
        Me.grvCPPT_ResepRacikan.OptionsNavigation.AutoFocusNewRow = True
        Me.grvCPPT_ResepRacikan.OptionsNavigation.EnterMoveNextColumn = True
        Me.grvCPPT_ResepRacikan.OptionsView.EnableAppearanceEvenRow = True
        Me.grvCPPT_ResepRacikan.OptionsView.EnableAppearanceOddRow = True
        Me.grvCPPT_ResepRacikan.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.grvCPPT_ResepRacikan.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.grvCPPT_ResepRacikan.OptionsView.ShowGroupPanel = False
        '
        'colCPPT_KDITEMRACIKAN
        '
        Me.colCPPT_KDITEMRACIKAN.Caption = "Obat"
        Me.colCPPT_KDITEMRACIKAN.ColumnEdit = Me.grdCPPT_KDITEMRACIKAN
        Me.colCPPT_KDITEMRACIKAN.FieldName = "KDITEM"
        Me.colCPPT_KDITEMRACIKAN.Name = "colCPPT_KDITEMRACIKAN"
        Me.colCPPT_KDITEMRACIKAN.Visible = True
        Me.colCPPT_KDITEMRACIKAN.VisibleIndex = 0
        Me.colCPPT_KDITEMRACIKAN.Width = 223
        '
        'grdCPPT_KDITEMRACIKAN
        '
        Me.grdCPPT_KDITEMRACIKAN.AutoHeight = False
        Me.grdCPPT_KDITEMRACIKAN.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdCPPT_KDITEMRACIKAN.Name = "grdCPPT_KDITEMRACIKAN"
        Me.grdCPPT_KDITEMRACIKAN.NullText = ""
        Me.grdCPPT_KDITEMRACIKAN.PopupFormSize = New System.Drawing.Size(950, 500)
        Me.grdCPPT_KDITEMRACIKAN.View = Me.grvCPPT_KDITEMRACIKAN
        '
        'grvCPPT_KDITEMRACIKAN
        '
        Me.grvCPPT_KDITEMRACIKAN.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn17, Me.GridColumn18, Me.GridColumn19, Me.GridColumn20, Me.GridColumn24})
        Me.grvCPPT_KDITEMRACIKAN.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvCPPT_KDITEMRACIKAN.Name = "grvCPPT_KDITEMRACIKAN"
        Me.grvCPPT_KDITEMRACIKAN.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvCPPT_KDITEMRACIKAN.OptionsView.ShowGroupPanel = False
        '
        'GridColumn17
        '
        Me.GridColumn17.Caption = "Generik"
        Me.GridColumn17.FieldName = "NMITEM1"
        Me.GridColumn17.Name = "GridColumn17"
        Me.GridColumn17.Visible = True
        Me.GridColumn17.VisibleIndex = 0
        Me.GridColumn17.Width = 284
        '
        'GridColumn18
        '
        Me.GridColumn18.Caption = "Paten"
        Me.GridColumn18.FieldName = "NMITEM2"
        Me.GridColumn18.Name = "GridColumn18"
        Me.GridColumn18.Visible = True
        Me.GridColumn18.VisibleIndex = 1
        Me.GridColumn18.Width = 205
        '
        'GridColumn19
        '
        Me.GridColumn19.Caption = "Harga Beli"
        Me.GridColumn19.DisplayFormat.FormatString = "{0:n0}"
        Me.GridColumn19.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn19.FieldName = "HARGA"
        Me.GridColumn19.Name = "GridColumn19"
        Me.GridColumn19.Visible = True
        Me.GridColumn19.VisibleIndex = 2
        Me.GridColumn19.Width = 84
        '
        'GridColumn20
        '
        Me.GridColumn20.Caption = "Satuan"
        Me.GridColumn20.FieldName = "SATUAN"
        Me.GridColumn20.Name = "GridColumn20"
        Me.GridColumn20.Visible = True
        Me.GridColumn20.VisibleIndex = 3
        Me.GridColumn20.Width = 84
        '
        'GridColumn24
        '
        Me.GridColumn24.Caption = "Stok"
        Me.GridColumn24.DisplayFormat.FormatString = "{0:n0}"
        Me.GridColumn24.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn24.FieldName = "STOK"
        Me.GridColumn24.Name = "GridColumn24"
        Me.GridColumn24.Visible = True
        Me.GridColumn24.VisibleIndex = 4
        Me.GridColumn24.Width = 97
        '
        'colCPPT_KDUOMRACIKAN
        '
        Me.colCPPT_KDUOMRACIKAN.Caption = "Satuan"
        Me.colCPPT_KDUOMRACIKAN.ColumnEdit = Me.grdCPPT_KDUOMRACIKAN
        Me.colCPPT_KDUOMRACIKAN.FieldName = "KDUOM"
        Me.colCPPT_KDUOMRACIKAN.Name = "colCPPT_KDUOMRACIKAN"
        Me.colCPPT_KDUOMRACIKAN.OptionsColumn.AllowEdit = False
        Me.colCPPT_KDUOMRACIKAN.OptionsColumn.AllowFocus = False
        Me.colCPPT_KDUOMRACIKAN.OptionsColumn.ReadOnly = True
        Me.colCPPT_KDUOMRACIKAN.OptionsColumn.TabStop = False
        Me.colCPPT_KDUOMRACIKAN.Visible = True
        Me.colCPPT_KDUOMRACIKAN.VisibleIndex = 1
        Me.colCPPT_KDUOMRACIKAN.Width = 74
        '
        'grdCPPT_KDUOMRACIKAN
        '
        Me.grdCPPT_KDUOMRACIKAN.AutoHeight = False
        Me.grdCPPT_KDUOMRACIKAN.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdCPPT_KDUOMRACIKAN.Name = "grdCPPT_KDUOMRACIKAN"
        Me.grdCPPT_KDUOMRACIKAN.NullText = ""
        Me.grdCPPT_KDUOMRACIKAN.View = Me.grvCPPT_KDUOMRACIKAN
        '
        'grvCPPT_KDUOMRACIKAN
        '
        Me.grvCPPT_KDUOMRACIKAN.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn25})
        Me.grvCPPT_KDUOMRACIKAN.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvCPPT_KDUOMRACIKAN.Name = "grvCPPT_KDUOMRACIKAN"
        Me.grvCPPT_KDUOMRACIKAN.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvCPPT_KDUOMRACIKAN.OptionsView.ShowGroupPanel = False
        '
        'GridColumn25
        '
        Me.GridColumn25.Caption = "Tampilan Nama"
        Me.GridColumn25.FieldName = "MEMO"
        Me.GridColumn25.Name = "GridColumn25"
        Me.GridColumn25.Visible = True
        Me.GridColumn25.VisibleIndex = 0
        '
        'colCPPT_SIGNARACIKAN
        '
        Me.colCPPT_SIGNARACIKAN.Caption = "Signa"
        Me.colCPPT_SIGNARACIKAN.FieldName = "SIGNA"
        Me.colCPPT_SIGNARACIKAN.Name = "colCPPT_SIGNARACIKAN"
        Me.colCPPT_SIGNARACIKAN.Visible = True
        Me.colCPPT_SIGNARACIKAN.VisibleIndex = 2
        Me.colCPPT_SIGNARACIKAN.Width = 74
        '
        'colCPPT_PERMINTAANRACIKAN
        '
        Me.colCPPT_PERMINTAANRACIKAN.Caption = "Permintaan"
        Me.colCPPT_PERMINTAANRACIKAN.FieldName = "PERMINTAAN"
        Me.colCPPT_PERMINTAANRACIKAN.Name = "colCPPT_PERMINTAANRACIKAN"
        Me.colCPPT_PERMINTAANRACIKAN.Visible = True
        Me.colCPPT_PERMINTAANRACIKAN.VisibleIndex = 3
        Me.colCPPT_PERMINTAANRACIKAN.Width = 74
        '
        'colCPPT_JUMLAHRACIKAN
        '
        Me.colCPPT_JUMLAHRACIKAN.Caption = "Jumlah"
        Me.colCPPT_JUMLAHRACIKAN.DisplayFormat.FormatString = "{0:n0}"
        Me.colCPPT_JUMLAHRACIKAN.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colCPPT_JUMLAHRACIKAN.FieldName = "JUMLAH"
        Me.colCPPT_JUMLAHRACIKAN.Name = "colCPPT_JUMLAHRACIKAN"
        Me.colCPPT_JUMLAHRACIKAN.Visible = True
        Me.colCPPT_JUMLAHRACIKAN.VisibleIndex = 4
        Me.colCPPT_JUMLAHRACIKAN.Width = 74
        '
        'colCPPT_HARGARACIKAN
        '
        Me.colCPPT_HARGARACIKAN.Caption = "Harga"
        Me.colCPPT_HARGARACIKAN.DisplayFormat.FormatString = "{0:n0}"
        Me.colCPPT_HARGARACIKAN.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colCPPT_HARGARACIKAN.FieldName = "HARGA"
        Me.colCPPT_HARGARACIKAN.Name = "colCPPT_HARGARACIKAN"
        Me.colCPPT_HARGARACIKAN.Visible = True
        Me.colCPPT_HARGARACIKAN.VisibleIndex = 5
        Me.colCPPT_HARGARACIKAN.Width = 74
        '
        'colCPPT_TOTALRACIKAN
        '
        Me.colCPPT_TOTALRACIKAN.Caption = "Total"
        Me.colCPPT_TOTALRACIKAN.DisplayFormat.FormatString = "{0:n0}"
        Me.colCPPT_TOTALRACIKAN.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colCPPT_TOTALRACIKAN.FieldName = "TOTAL"
        Me.colCPPT_TOTALRACIKAN.Name = "colCPPT_TOTALRACIKAN"
        Me.colCPPT_TOTALRACIKAN.Visible = True
        Me.colCPPT_TOTALRACIKAN.VisibleIndex = 6
        Me.colCPPT_TOTALRACIKAN.Width = 74
        '
        'colCPPT_REMARKSRACIKAN
        '
        Me.colCPPT_REMARKSRACIKAN.Caption = "Catatan"
        Me.colCPPT_REMARKSRACIKAN.FieldName = "REMARKS"
        Me.colCPPT_REMARKSRACIKAN.Name = "colCPPT_REMARKSRACIKAN"
        Me.colCPPT_REMARKSRACIKAN.Visible = True
        Me.colCPPT_REMARKSRACIKAN.VisibleIndex = 7
        Me.colCPPT_REMARKSRACIKAN.Width = 87
        '
        'colCPPT_ISBACARACIKAN
        '
        Me.colCPPT_ISBACARACIKAN.Caption = "Baca"
        Me.colCPPT_ISBACARACIKAN.FieldName = "ISBACA"
        Me.colCPPT_ISBACARACIKAN.Name = "colCPPT_ISBACARACIKAN"
        '
        'txtCPPT_TotalRacikanPaket
        '
        Me.txtCPPT_TotalRacikanPaket.EditValue = "0"
        Me.txtCPPT_TotalRacikanPaket.Location = New System.Drawing.Point(107, 264)
        Me.txtCPPT_TotalRacikanPaket.Name = "txtCPPT_TotalRacikanPaket"
        Me.txtCPPT_TotalRacikanPaket.Properties.Appearance.Options.UseTextOptions = True
        Me.txtCPPT_TotalRacikanPaket.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtCPPT_TotalRacikanPaket.Properties.Mask.EditMask = "n0"
        Me.txtCPPT_TotalRacikanPaket.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtCPPT_TotalRacikanPaket.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtCPPT_TotalRacikanPaket.Properties.ReadOnly = True
        Me.txtCPPT_TotalRacikanPaket.Size = New System.Drawing.Size(281, 20)
        Me.txtCPPT_TotalRacikanPaket.StyleController = Me.LayoutControl4
        Me.txtCPPT_TotalRacikanPaket.TabIndex = 96
        '
        'LayoutControlGroup4
        '
        Me.LayoutControlGroup4.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup4.GroupBordersVisible = False
        Me.LayoutControlGroup4.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem19, Me.LayoutControlItem18, Me.LayoutControlItem21})
        Me.LayoutControlGroup4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup4.Name = "Root"
        Me.LayoutControlGroup4.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup4.Size = New System.Drawing.Size(774, 286)
        Me.LayoutControlGroup4.TextVisible = False
        '
        'LayoutControlItem19
        '
        Me.LayoutControlItem19.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem19.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem19.Control = Me.txtCPPT_TotalRacikanPaket
        Me.LayoutControlItem19.Location = New System.Drawing.Point(0, 262)
        Me.LayoutControlItem19.Name = "LayoutControlItem19"
        Me.LayoutControlItem19.Size = New System.Drawing.Size(390, 24)
        Me.LayoutControlItem19.Text = "Total Paket :"
        Me.LayoutControlItem19.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem19.TextSize = New System.Drawing.Size(100, 20)
        Me.LayoutControlItem19.TextToControlDistance = 5
        '
        'LayoutControlItem18
        '
        Me.LayoutControlItem18.Control = Me.grdCPPT_ResepRacikan
        Me.LayoutControlItem18.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem18.Name = "LayoutControlItem18"
        Me.LayoutControlItem18.Size = New System.Drawing.Size(774, 262)
        Me.LayoutControlItem18.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem18.TextVisible = False
        '
        'LayoutControlItem21
        '
        Me.LayoutControlItem21.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem21.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem21.Control = Me.txtCPPT_TotalRacikanNonPaket
        Me.LayoutControlItem21.Location = New System.Drawing.Point(390, 262)
        Me.LayoutControlItem21.Name = "LayoutControlItem21"
        Me.LayoutControlItem21.Size = New System.Drawing.Size(384, 24)
        Me.LayoutControlItem21.Text = "Total Non Paket :"
        Me.LayoutControlItem21.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem21.TextSize = New System.Drawing.Size(100, 20)
        Me.LayoutControlItem21.TextToControlDistance = 5
        '
        'GroupControl7
        '
        Me.GroupControl7.AppearanceCaption.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GroupControl7.AppearanceCaption.Options.UseFont = True
        Me.GroupControl7.Controls.Add(Me.btnCPPT_CariICD10)
        Me.GroupControl7.Controls.Add(Me.txtCPPT_CARIDIAGNOSA)
        Me.GroupControl7.Controls.Add(Me.grdCariDiagnosa)
        Me.GroupControl7.Controls.Add(Me.LabelControl33)
        Me.GroupControl7.Controls.Add(Me.txtCPPT_Indikasi)
        Me.GroupControl7.Controls.Add(Me.grdCPPT_Diagnosa)
        Me.GroupControl7.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl7.Location = New System.Drawing.Point(0, 766)
        Me.GroupControl7.Name = "GroupControl7"
        Me.GroupControl7.Size = New System.Drawing.Size(792, 261)
        Me.GroupControl7.TabIndex = 5
        Me.GroupControl7.Text = "Assesment"
        '
        'btnCPPT_CariICD10
        '
        Me.btnCPPT_CariICD10.Location = New System.Drawing.Point(179, 24)
        Me.btnCPPT_CariICD10.Name = "btnCPPT_CariICD10"
        Me.btnCPPT_CariICD10.Size = New System.Drawing.Size(136, 23)
        Me.btnCPPT_CariICD10.TabIndex = 100
        Me.btnCPPT_CariICD10.Text = "Cari Diagnosa ICD (10) :"
        '
        'txtCPPT_CARIDIAGNOSA
        '
        Me.txtCPPT_CARIDIAGNOSA.Location = New System.Drawing.Point(12, 25)
        Me.txtCPPT_CARIDIAGNOSA.MenuManager = Me.barManager
        Me.txtCPPT_CARIDIAGNOSA.Name = "txtCPPT_CARIDIAGNOSA"
        Me.txtCPPT_CARIDIAGNOSA.Size = New System.Drawing.Size(160, 20)
        Me.txtCPPT_CARIDIAGNOSA.TabIndex = 99
        '
        'grdCariDiagnosa
        '
        Me.grdCariDiagnosa.EditValue = ""
        Me.grdCariDiagnosa.Location = New System.Drawing.Point(321, 26)
        Me.grdCariDiagnosa.MenuManager = Me.barManager
        Me.grdCariDiagnosa.Name = "grdCariDiagnosa"
        Me.grdCariDiagnosa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdCariDiagnosa.Properties.NullText = ""
        Me.grdCariDiagnosa.Properties.View = Me.grvCariDiagnosa
        Me.grdCariDiagnosa.Size = New System.Drawing.Size(241, 20)
        Me.grdCariDiagnosa.TabIndex = 97
        '
        'grvCariDiagnosa
        '
        Me.grvCariDiagnosa.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn4})
        Me.grvCariDiagnosa.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvCariDiagnosa.Name = "grvCariDiagnosa"
        Me.grvCariDiagnosa.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvCariDiagnosa.OptionsView.ShowGroupPanel = False
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Kode Diagnosa"
        Me.GridColumn3.FieldName = "kode"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        Me.GridColumn3.Width = 126
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Nama Diagnosa"
        Me.GridColumn4.FieldName = "nama"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 1
        Me.GridColumn4.Width = 521
        '
        'LabelControl33
        '
        Me.LabelControl33.Location = New System.Drawing.Point(12, 197)
        Me.LabelControl33.Name = "LabelControl33"
        Me.LabelControl33.Size = New System.Drawing.Size(43, 13)
        Me.LabelControl33.TabIndex = 96
        Me.LabelControl33.Text = "Indikasi :"
        '
        'txtCPPT_Indikasi
        '
        Me.txtCPPT_Indikasi.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCPPT_Indikasi.Location = New System.Drawing.Point(12, 216)
        Me.txtCPPT_Indikasi.Name = "txtCPPT_Indikasi"
        Me.txtCPPT_Indikasi.Size = New System.Drawing.Size(775, 39)
        Me.txtCPPT_Indikasi.TabIndex = 26
        '
        'grdCPPT_Diagnosa
        '
        Me.grdCPPT_Diagnosa.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdCPPT_Diagnosa.ContextMenuStrip = Me.ContextMenuStrip1
        Me.grdCPPT_Diagnosa.DataSource = Me.BindingSourceCPPT_Diagnosa
        Me.grdCPPT_Diagnosa.Location = New System.Drawing.Point(12, 53)
        Me.grdCPPT_Diagnosa.MainView = Me.grvCPPT_Diagnosa
        Me.grdCPPT_Diagnosa.Name = "grdCPPT_Diagnosa"
        Me.grdCPPT_Diagnosa.Size = New System.Drawing.Size(774, 138)
        Me.grdCPPT_Diagnosa.TabIndex = 25
        Me.grdCPPT_Diagnosa.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvCPPT_Diagnosa})
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DeleteToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(108, 26)
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(107, 22)
        Me.DeleteToolStripMenuItem.Text = "Delete"
        '
        'BindingSourceCPPT_Diagnosa
        '
        Me.BindingSourceCPPT_Diagnosa.DataSource = GetType(DataAccess.R_CPPT_DIAGNOSA)
        '
        'grvCPPT_Diagnosa
        '
        Me.grvCPPT_Diagnosa.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colCPPT_KATEGORI, Me.colCPPT_MEMODIAGNOSA, Me.colCPPT_KDDIAGNOSA})
        Me.grvCPPT_Diagnosa.GridControl = Me.grdCPPT_Diagnosa
        Me.grvCPPT_Diagnosa.Name = "grvCPPT_Diagnosa"
        Me.grvCPPT_Diagnosa.OptionsCustomization.AllowColumnMoving = False
        Me.grvCPPT_Diagnosa.OptionsCustomization.AllowFilter = False
        Me.grvCPPT_Diagnosa.OptionsCustomization.AllowGroup = False
        Me.grvCPPT_Diagnosa.OptionsCustomization.AllowQuickHideColumns = False
        Me.grvCPPT_Diagnosa.OptionsCustomization.AllowSort = False
        Me.grvCPPT_Diagnosa.OptionsDetail.EnableMasterViewMode = False
        Me.grvCPPT_Diagnosa.OptionsFind.AllowFindPanel = False
        Me.grvCPPT_Diagnosa.OptionsLayout.StoreAllOptions = True
        Me.grvCPPT_Diagnosa.OptionsLayout.StoreAppearance = True
        Me.grvCPPT_Diagnosa.OptionsMenu.EnableColumnMenu = False
        Me.grvCPPT_Diagnosa.OptionsNavigation.AutoFocusNewRow = True
        Me.grvCPPT_Diagnosa.OptionsNavigation.EnterMoveNextColumn = True
        Me.grvCPPT_Diagnosa.OptionsView.EnableAppearanceEvenRow = True
        Me.grvCPPT_Diagnosa.OptionsView.EnableAppearanceOddRow = True
        Me.grvCPPT_Diagnosa.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.grvCPPT_Diagnosa.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.grvCPPT_Diagnosa.OptionsView.ShowGroupPanel = False
        '
        'colCPPT_KATEGORI
        '
        Me.colCPPT_KATEGORI.Caption = "Kategori"
        Me.colCPPT_KATEGORI.FieldName = "KATEGORI"
        Me.colCPPT_KATEGORI.Name = "colCPPT_KATEGORI"
        Me.colCPPT_KATEGORI.Width = 137
        '
        'colCPPT_MEMODIAGNOSA
        '
        Me.colCPPT_MEMODIAGNOSA.Caption = "Diagnosa"
        Me.colCPPT_MEMODIAGNOSA.FieldName = "MEMO"
        Me.colCPPT_MEMODIAGNOSA.Name = "colCPPT_MEMODIAGNOSA"
        Me.colCPPT_MEMODIAGNOSA.Visible = True
        Me.colCPPT_MEMODIAGNOSA.VisibleIndex = 0
        Me.colCPPT_MEMODIAGNOSA.Width = 483
        '
        'colCPPT_KDDIAGNOSA
        '
        Me.colCPPT_KDDIAGNOSA.Caption = "Kode"
        Me.colCPPT_KDDIAGNOSA.FieldName = "KDDIAGNOSA"
        Me.colCPPT_KDDIAGNOSA.Name = "colCPPT_KDDIAGNOSA"
        Me.colCPPT_KDDIAGNOSA.Visible = True
        Me.colCPPT_KDDIAGNOSA.VisibleIndex = 1
        Me.colCPPT_KDDIAGNOSA.Width = 156
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.SimpleButton1)
        Me.PanelControl2.Controls.Add(Me.btnbtnBuatTemplateTindakan)
        Me.PanelControl2.Controls.Add(Me.Label2)
        Me.PanelControl2.Controls.Add(Me.Label1)
        Me.PanelControl2.Controls.Add(Me.txtKDIDENTITAS)
        Me.PanelControl2.Controls.Add(Me.txtCPPT_Kode)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl2.Location = New System.Drawing.Point(0, 1871)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(792, 66)
        Me.PanelControl2.TabIndex = 42
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Location = New System.Drawing.Point(333, 34)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(192, 23)
        Me.SimpleButton1.TabIndex = 4
        Me.SimpleButton1.Text = "Buat Template Resep Non Racikan"
        '
        'btnbtnBuatTemplateTindakan
        '
        Me.btnbtnBuatTemplateTindakan.Location = New System.Drawing.Point(333, 8)
        Me.btnbtnBuatTemplateTindakan.Name = "btnbtnBuatTemplateTindakan"
        Me.btnbtnBuatTemplateTindakan.Size = New System.Drawing.Size(192, 23)
        Me.btnbtnBuatTemplateTindakan.TabIndex = 3
        Me.btnbtnBuatTemplateTindakan.Text = "Buat Template Tindakan"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 39)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(112, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Kode Identitas CPPT :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(64, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Kode CPPT :"
        '
        'txtKDIDENTITAS
        '
        Me.txtKDIDENTITAS.Location = New System.Drawing.Point(143, 36)
        Me.txtKDIDENTITAS.Name = "txtKDIDENTITAS"
        Me.txtKDIDENTITAS.Properties.ReadOnly = True
        Me.txtKDIDENTITAS.Size = New System.Drawing.Size(171, 20)
        Me.txtKDIDENTITAS.TabIndex = 1
        '
        'txtCPPT_Kode
        '
        Me.txtCPPT_Kode.Location = New System.Drawing.Point(143, 10)
        Me.txtCPPT_Kode.Name = "txtCPPT_Kode"
        Me.txtCPPT_Kode.Properties.ReadOnly = True
        Me.txtCPPT_Kode.Size = New System.Drawing.Size(171, 20)
        Me.txtCPPT_Kode.TabIndex = 0
        '
        'GroupControl4
        '
        Me.GroupControl4.AppearanceCaption.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GroupControl4.AppearanceCaption.Options.UseFont = True
        Me.GroupControl4.Controls.Add(Me.txtCPPT_OBJEKTIF_ALAMATGAMBARPEMERIKSAAN)
        Me.GroupControl4.Controls.Add(Me.LabelControl39)
        Me.GroupControl4.Controls.Add(Me.LabelControl38)
        Me.GroupControl4.Controls.Add(Me.LabelControl37)
        Me.GroupControl4.Controls.Add(Me.LabelControl36)
        Me.GroupControl4.Controls.Add(Me.txtCPPT_VisualAnalogScore)
        Me.GroupControl4.Controls.Add(Me.txtCPPT_GCS)
        Me.GroupControl4.Controls.Add(Me.txtCPPT_TampakSakit)
        Me.GroupControl4.Controls.Add(Me.txtCPPT_Kesadaran)
        Me.GroupControl4.Controls.Add(Me.picCPPT_Gambar)
        Me.GroupControl4.Controls.Add(Me.txtCPPT_Pemeriksaan)
        Me.GroupControl4.Controls.Add(Me.btnResetGambar)
        Me.GroupControl4.Controls.Add(Me.btnCPPT_AmbilGambar)
        Me.GroupControl4.Controls.Add(Me.txtCPPT_Diastole)
        Me.GroupControl4.Controls.Add(Me.txtCPPT_Sistole)
        Me.GroupControl4.Controls.Add(Me.txtCPPT_RR)
        Me.GroupControl4.Controls.Add(Me.txtCPPT_BeratBadan)
        Me.GroupControl4.Controls.Add(Me.LabelControl14)
        Me.GroupControl4.Controls.Add(Me.LabelControl13)
        Me.GroupControl4.Controls.Add(Me.LabelControl30)
        Me.GroupControl4.Controls.Add(Me.LabelControl19)
        Me.GroupControl4.Controls.Add(Me.LabelControl9)
        Me.GroupControl4.Controls.Add(Me.txtCPPT_Suhu)
        Me.GroupControl4.Controls.Add(Me.LabelControl20)
        Me.GroupControl4.Controls.Add(Me.txtCPPT_SpO2)
        Me.GroupControl4.Controls.Add(Me.LabelControl11)
        Me.GroupControl4.Controls.Add(Me.txtCPPT_HR)
        Me.GroupControl4.Controls.Add(Me.LabelControl22)
        Me.GroupControl4.Controls.Add(Me.txtCPPT_TinggiBadan)
        Me.GroupControl4.Controls.Add(Me.LabelControl24)
        Me.GroupControl4.Controls.Add(Me.LabelControl10)
        Me.GroupControl4.Controls.Add(Me.LabelControl21)
        Me.GroupControl4.Controls.Add(Me.LabelControl18)
        Me.GroupControl4.Controls.Add(Me.LabelControl12)
        Me.GroupControl4.Controls.Add(Me.LabelControl15)
        Me.GroupControl4.Controls.Add(Me.LabelControl23)
        Me.GroupControl4.Controls.Add(Me.LabelControl25)
        Me.GroupControl4.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl4.Location = New System.Drawing.Point(0, 289)
        Me.GroupControl4.Name = "GroupControl4"
        Me.GroupControl4.Size = New System.Drawing.Size(792, 477)
        Me.GroupControl4.TabIndex = 2
        Me.GroupControl4.Text = "Objektif"
        '
        'txtCPPT_OBJEKTIF_ALAMATGAMBARPEMERIKSAAN
        '
        Me.txtCPPT_OBJEKTIF_ALAMATGAMBARPEMERIKSAAN.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCPPT_OBJEKTIF_ALAMATGAMBARPEMERIKSAAN.Location = New System.Drawing.Point(422, 173)
        Me.txtCPPT_OBJEKTIF_ALAMATGAMBARPEMERIKSAAN.Name = "txtCPPT_OBJEKTIF_ALAMATGAMBARPEMERIKSAAN"
        Me.txtCPPT_OBJEKTIF_ALAMATGAMBARPEMERIKSAAN.Properties.ReadOnly = True
        Me.txtCPPT_OBJEKTIF_ALAMATGAMBARPEMERIKSAAN.Size = New System.Drawing.Size(282, 20)
        Me.txtCPPT_OBJEKTIF_ALAMATGAMBARPEMERIKSAAN.TabIndex = 24
        '
        'LabelControl39
        '
        Me.LabelControl39.Location = New System.Drawing.Point(316, 52)
        Me.LabelControl39.Name = "LabelControl39"
        Me.LabelControl39.Size = New System.Drawing.Size(100, 13)
        Me.LabelControl39.TabIndex = 16
        Me.LabelControl39.Text = "Visual Analog Score :"
        '
        'LabelControl38
        '
        Me.LabelControl38.Location = New System.Drawing.Point(389, 26)
        Me.LabelControl38.Name = "LabelControl38"
        Me.LabelControl38.Size = New System.Drawing.Size(27, 13)
        Me.LabelControl38.TabIndex = 16
        Me.LabelControl38.Text = "GCS :"
        '
        'LabelControl37
        '
        Me.LabelControl37.Location = New System.Drawing.Point(20, 52)
        Me.LabelControl37.Name = "LabelControl37"
        Me.LabelControl37.Size = New System.Drawing.Size(70, 13)
        Me.LabelControl37.TabIndex = 16
        Me.LabelControl37.Text = "Tampak Sakit :"
        '
        'LabelControl36
        '
        Me.LabelControl36.Location = New System.Drawing.Point(32, 26)
        Me.LabelControl36.Name = "LabelControl36"
        Me.LabelControl36.Size = New System.Drawing.Size(58, 13)
        Me.LabelControl36.TabIndex = 16
        Me.LabelControl36.Text = "Kesadaran :"
        '
        'txtCPPT_VisualAnalogScore
        '
        Me.txtCPPT_VisualAnalogScore.Location = New System.Drawing.Point(444, 49)
        Me.txtCPPT_VisualAnalogScore.Name = "txtCPPT_VisualAnalogScore"
        Me.txtCPPT_VisualAnalogScore.Size = New System.Drawing.Size(158, 20)
        Me.txtCPPT_VisualAnalogScore.TabIndex = 14
        '
        'txtCPPT_GCS
        '
        Me.txtCPPT_GCS.Location = New System.Drawing.Point(444, 23)
        Me.txtCPPT_GCS.Name = "txtCPPT_GCS"
        Me.txtCPPT_GCS.Size = New System.Drawing.Size(158, 20)
        Me.txtCPPT_GCS.TabIndex = 12
        '
        'txtCPPT_TampakSakit
        '
        Me.txtCPPT_TampakSakit.Location = New System.Drawing.Point(105, 49)
        Me.txtCPPT_TampakSakit.Name = "txtCPPT_TampakSakit"
        Me.txtCPPT_TampakSakit.Size = New System.Drawing.Size(158, 20)
        Me.txtCPPT_TampakSakit.TabIndex = 13
        '
        'txtCPPT_Kesadaran
        '
        Me.txtCPPT_Kesadaran.Location = New System.Drawing.Point(105, 23)
        Me.txtCPPT_Kesadaran.Name = "txtCPPT_Kesadaran"
        Me.txtCPPT_Kesadaran.Size = New System.Drawing.Size(158, 20)
        Me.txtCPPT_Kesadaran.TabIndex = 11
        '
        'picCPPT_Gambar
        '
        Me.picCPPT_Gambar.Image = Global.UI.WIN.MAIN.My.Resources.Resources.image1
        Me.picCPPT_Gambar.Location = New System.Drawing.Point(422, 228)
        Me.picCPPT_Gambar.Name = "picCPPT_Gambar"
        Me.picCPPT_Gambar.Size = New System.Drawing.Size(277, 240)
        Me.picCPPT_Gambar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picCPPT_Gambar.TabIndex = 15
        Me.picCPPT_Gambar.TabStop = False
        '
        'txtCPPT_Pemeriksaan
        '
        Me.txtCPPT_Pemeriksaan.Location = New System.Drawing.Point(12, 203)
        Me.txtCPPT_Pemeriksaan.Name = "txtCPPT_Pemeriksaan"
        Me.txtCPPT_Pemeriksaan.Size = New System.Drawing.Size(404, 265)
        Me.txtCPPT_Pemeriksaan.TabIndex = 23
        '
        'btnResetGambar
        '
        Me.btnResetGambar.Location = New System.Drawing.Point(422, 199)
        Me.btnResetGambar.Name = "btnResetGambar"
        Me.btnResetGambar.Size = New System.Drawing.Size(92, 23)
        Me.btnResetGambar.TabIndex = 23
        Me.btnResetGambar.Text = "Reset"
        '
        'btnCPPT_AmbilGambar
        '
        Me.btnCPPT_AmbilGambar.Location = New System.Drawing.Point(520, 199)
        Me.btnCPPT_AmbilGambar.Name = "btnCPPT_AmbilGambar"
        Me.btnCPPT_AmbilGambar.Size = New System.Drawing.Size(92, 23)
        Me.btnCPPT_AmbilGambar.TabIndex = 23
        Me.btnCPPT_AmbilGambar.Text = "Status Lokasi"
        '
        'txtCPPT_Diastole
        '
        Me.txtCPPT_Diastole.EditValue = "0"
        Me.txtCPPT_Diastole.Location = New System.Drawing.Point(553, 101)
        Me.txtCPPT_Diastole.Name = "txtCPPT_Diastole"
        Me.txtCPPT_Diastole.Properties.DisplayFormat.FormatString = "n0"
        Me.txtCPPT_Diastole.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtCPPT_Diastole.Properties.EditFormat.FormatString = "n0"
        Me.txtCPPT_Diastole.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtCPPT_Diastole.Properties.Mask.EditMask = "n0"
        Me.txtCPPT_Diastole.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtCPPT_Diastole.Size = New System.Drawing.Size(50, 20)
        Me.txtCPPT_Diastole.TabIndex = 19
        '
        'txtCPPT_Sistole
        '
        Me.txtCPPT_Sistole.EditValue = "0"
        Me.txtCPPT_Sistole.Location = New System.Drawing.Point(445, 100)
        Me.txtCPPT_Sistole.Name = "txtCPPT_Sistole"
        Me.txtCPPT_Sistole.Properties.DisplayFormat.FormatString = "n0"
        Me.txtCPPT_Sistole.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtCPPT_Sistole.Properties.EditFormat.FormatString = "n0"
        Me.txtCPPT_Sistole.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtCPPT_Sistole.Properties.Mask.EditMask = "n0"
        Me.txtCPPT_Sistole.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtCPPT_Sistole.Size = New System.Drawing.Size(50, 20)
        Me.txtCPPT_Sistole.TabIndex = 18
        '
        'txtCPPT_RR
        '
        Me.txtCPPT_RR.Location = New System.Drawing.Point(445, 127)
        Me.txtCPPT_RR.Name = "txtCPPT_RR"
        Me.txtCPPT_RR.Size = New System.Drawing.Size(158, 20)
        Me.txtCPPT_RR.TabIndex = 21
        '
        'txtCPPT_BeratBadan
        '
        Me.txtCPPT_BeratBadan.Location = New System.Drawing.Point(105, 75)
        Me.txtCPPT_BeratBadan.Name = "txtCPPT_BeratBadan"
        Me.txtCPPT_BeratBadan.Size = New System.Drawing.Size(158, 20)
        Me.txtCPPT_BeratBadan.TabIndex = 15
        '
        'LabelControl14
        '
        Me.LabelControl14.Location = New System.Drawing.Point(501, 104)
        Me.LabelControl14.Name = "LabelControl14"
        Me.LabelControl14.Size = New System.Drawing.Size(45, 13)
        Me.LabelControl14.TabIndex = 0
        Me.LabelControl14.Text = "Diastole :"
        '
        'LabelControl13
        '
        Me.LabelControl13.Location = New System.Drawing.Point(378, 103)
        Me.LabelControl13.Name = "LabelControl13"
        Me.LabelControl13.Size = New System.Drawing.Size(38, 13)
        Me.LabelControl13.TabIndex = 0
        Me.LabelControl13.Text = "Sistole :"
        '
        'LabelControl30
        '
        Me.LabelControl30.Location = New System.Drawing.Point(23, 181)
        Me.LabelControl30.Name = "LabelControl30"
        Me.LabelControl30.Size = New System.Drawing.Size(67, 13)
        Me.LabelControl30.TabIndex = 0
        Me.LabelControl30.Text = "Pemeriksaan :"
        '
        'LabelControl19
        '
        Me.LabelControl19.Location = New System.Drawing.Point(395, 130)
        Me.LabelControl19.Name = "LabelControl19"
        Me.LabelControl19.Size = New System.Drawing.Size(21, 13)
        Me.LabelControl19.TabIndex = 0
        Me.LabelControl19.Text = "RR :"
        '
        'LabelControl9
        '
        Me.LabelControl9.Location = New System.Drawing.Point(348, 78)
        Me.LabelControl9.Name = "LabelControl9"
        Me.LabelControl9.Size = New System.Drawing.Size(68, 13)
        Me.LabelControl9.TabIndex = 0
        Me.LabelControl9.Text = "Tinggi Badan :"
        '
        'txtCPPT_Suhu
        '
        Me.txtCPPT_Suhu.Location = New System.Drawing.Point(105, 153)
        Me.txtCPPT_Suhu.Name = "txtCPPT_Suhu"
        Me.txtCPPT_Suhu.Size = New System.Drawing.Size(158, 20)
        Me.txtCPPT_Suhu.TabIndex = 22
        '
        'LabelControl20
        '
        Me.LabelControl20.Location = New System.Drawing.Point(57, 104)
        Me.LabelControl20.Name = "LabelControl20"
        Me.LabelControl20.Size = New System.Drawing.Size(33, 13)
        Me.LabelControl20.TabIndex = 0
        Me.LabelControl20.Text = "SpO2 :"
        '
        'txtCPPT_SpO2
        '
        Me.txtCPPT_SpO2.Location = New System.Drawing.Point(105, 101)
        Me.txtCPPT_SpO2.Name = "txtCPPT_SpO2"
        Me.txtCPPT_SpO2.Size = New System.Drawing.Size(158, 20)
        Me.txtCPPT_SpO2.TabIndex = 17
        '
        'LabelControl11
        '
        Me.LabelControl11.Location = New System.Drawing.Point(609, 78)
        Me.LabelControl11.Name = "LabelControl11"
        Me.LabelControl11.Size = New System.Drawing.Size(15, 13)
        Me.LabelControl11.TabIndex = 0
        Me.LabelControl11.Text = "Cm"
        '
        'txtCPPT_HR
        '
        Me.txtCPPT_HR.Location = New System.Drawing.Point(105, 127)
        Me.txtCPPT_HR.Name = "txtCPPT_HR"
        Me.txtCPPT_HR.Size = New System.Drawing.Size(158, 20)
        Me.txtCPPT_HR.TabIndex = 20
        '
        'LabelControl22
        '
        Me.LabelControl22.Location = New System.Drawing.Point(69, 130)
        Me.LabelControl22.Name = "LabelControl22"
        Me.LabelControl22.Size = New System.Drawing.Size(21, 13)
        Me.LabelControl22.TabIndex = 0
        Me.LabelControl22.Text = "HR :"
        '
        'txtCPPT_TinggiBadan
        '
        Me.txtCPPT_TinggiBadan.Location = New System.Drawing.Point(445, 75)
        Me.txtCPPT_TinggiBadan.Name = "txtCPPT_TinggiBadan"
        Me.txtCPPT_TinggiBadan.Size = New System.Drawing.Size(158, 20)
        Me.txtCPPT_TinggiBadan.TabIndex = 16
        '
        'LabelControl24
        '
        Me.LabelControl24.Location = New System.Drawing.Point(59, 156)
        Me.LabelControl24.Name = "LabelControl24"
        Me.LabelControl24.Size = New System.Drawing.Size(31, 13)
        Me.LabelControl24.TabIndex = 0
        Me.LabelControl24.Text = "Suhu :"
        '
        'LabelControl10
        '
        Me.LabelControl10.Location = New System.Drawing.Point(24, 79)
        Me.LabelControl10.Name = "LabelControl10"
        Me.LabelControl10.Size = New System.Drawing.Size(66, 13)
        Me.LabelControl10.TabIndex = 0
        Me.LabelControl10.Text = "Berat Badan :"
        '
        'LabelControl21
        '
        Me.LabelControl21.Location = New System.Drawing.Point(270, 130)
        Me.LabelControl21.Name = "LabelControl21"
        Me.LabelControl21.Size = New System.Drawing.Size(36, 13)
        Me.LabelControl21.TabIndex = 0
        Me.LabelControl21.Text = "x/menit"
        '
        'LabelControl18
        '
        Me.LabelControl18.Location = New System.Drawing.Point(609, 129)
        Me.LabelControl18.Name = "LabelControl18"
        Me.LabelControl18.Size = New System.Drawing.Size(36, 13)
        Me.LabelControl18.TabIndex = 0
        Me.LabelControl18.Text = "x/menit"
        '
        'LabelControl12
        '
        Me.LabelControl12.Location = New System.Drawing.Point(269, 78)
        Me.LabelControl12.Name = "LabelControl12"
        Me.LabelControl12.Size = New System.Drawing.Size(12, 13)
        Me.LabelControl12.TabIndex = 0
        Me.LabelControl12.Text = "Kg"
        '
        'LabelControl15
        '
        Me.LabelControl15.Location = New System.Drawing.Point(609, 104)
        Me.LabelControl15.Name = "LabelControl15"
        Me.LabelControl15.Size = New System.Drawing.Size(29, 13)
        Me.LabelControl15.TabIndex = 0
        Me.LabelControl15.Text = "mmHg"
        '
        'LabelControl23
        '
        Me.LabelControl23.Location = New System.Drawing.Point(270, 104)
        Me.LabelControl23.Name = "LabelControl23"
        Me.LabelControl23.Size = New System.Drawing.Size(11, 13)
        Me.LabelControl23.TabIndex = 0
        Me.LabelControl23.Text = "%"
        '
        'LabelControl25
        '
        Me.LabelControl25.Location = New System.Drawing.Point(270, 156)
        Me.LabelControl25.Name = "LabelControl25"
        Me.LabelControl25.Size = New System.Drawing.Size(13, 13)
        Me.LabelControl25.TabIndex = 0
        Me.LabelControl25.Text = "oC"
        '
        'GroupControl5
        '
        Me.GroupControl5.AppearanceCaption.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GroupControl5.AppearanceCaption.Options.UseFont = True
        Me.GroupControl5.Controls.Add(Me.LabelControl2)
        Me.GroupControl5.Controls.Add(Me.grdCPPT_Profesi)
        Me.GroupControl5.Controls.Add(Me.LabelControl4)
        Me.GroupControl5.Controls.Add(Me.deCPPT_Tanggal)
        Me.GroupControl5.Controls.Add(Me.grdCPPT_KDDOCTOR)
        Me.GroupControl5.Controls.Add(Me.LabelControl29)
        Me.GroupControl5.Controls.Add(Me.LabelControl1)
        Me.GroupControl5.Controls.Add(Me.txtRUANGAN)
        Me.GroupControl5.Controls.Add(Me.txtCPPT_KeluhanUtama)
        Me.GroupControl5.Controls.Add(Me.LabelControl50)
        Me.GroupControl5.Controls.Add(Me.LabelControl16)
        Me.GroupControl5.Controls.Add(Me.chkCPPT_AlergiTidak)
        Me.GroupControl5.Controls.Add(Me.chkCPPT_AlergiYa)
        Me.GroupControl5.Controls.Add(Me.txtCPPT_AlergiYa)
        Me.GroupControl5.Controls.Add(Me.LabelControl8)
        Me.GroupControl5.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl5.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl5.Name = "GroupControl5"
        Me.GroupControl5.Size = New System.Drawing.Size(792, 289)
        Me.GroupControl5.TabIndex = 3
        Me.GroupControl5.Text = "Subjektif"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(61, 105)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(49, 13)
        Me.LabelControl2.TabIndex = 47
        Me.LabelControl2.Text = "Profesi * :"
        '
        'grdCPPT_Profesi
        '
        Me.grdCPPT_Profesi.EnterMoveNextControl = True
        Me.grdCPPT_Profesi.Location = New System.Drawing.Point(123, 102)
        Me.grdCPPT_Profesi.Name = "grdCPPT_Profesi"
        Me.grdCPPT_Profesi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdCPPT_Profesi.Properties.NullText = ""
        Me.grdCPPT_Profesi.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdCPPT_Profesi.Properties.View = Me.grvCPPT_Profesi
        Me.grdCPPT_Profesi.Size = New System.Drawing.Size(312, 20)
        Me.grdCPPT_Profesi.TabIndex = 46
        '
        'grvCPPT_Profesi
        '
        Me.grvCPPT_Profesi.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1})
        Me.grvCPPT_Profesi.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvCPPT_Profesi.Name = "grvCPPT_Profesi"
        Me.grvCPPT_Profesi.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvCPPT_Profesi.OptionsView.ShowAutoFilterRow = True
        Me.grvCPPT_Profesi.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Name Display"
        Me.GridColumn1.FieldName = "MEMO"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(57, 77)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(54, 13)
        Me.LabelControl4.TabIndex = 45
        Me.LabelControl4.Text = "Tanggal * :"
        '
        'deCPPT_Tanggal
        '
        Me.deCPPT_Tanggal.EditValue = Nothing
        Me.deCPPT_Tanggal.EnterMoveNextControl = True
        Me.deCPPT_Tanggal.Location = New System.Drawing.Point(123, 76)
        Me.deCPPT_Tanggal.Name = "deCPPT_Tanggal"
        Me.deCPPT_Tanggal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deCPPT_Tanggal.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.deCPPT_Tanggal.Properties.Mask.EditMask = "dd/MM/yyyy HH:mm"
        Me.deCPPT_Tanggal.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.deCPPT_Tanggal.Size = New System.Drawing.Size(312, 20)
        Me.deCPPT_Tanggal.TabIndex = 3
        '
        'grdCPPT_KDDOCTOR
        '
        Me.grdCPPT_KDDOCTOR.EnterMoveNextControl = True
        Me.grdCPPT_KDDOCTOR.Location = New System.Drawing.Point(123, 52)
        Me.grdCPPT_KDDOCTOR.Name = "grdCPPT_KDDOCTOR"
        Me.grdCPPT_KDDOCTOR.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdCPPT_KDDOCTOR.Properties.NullText = ""
        Me.grdCPPT_KDDOCTOR.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdCPPT_KDDOCTOR.Properties.View = Me.grvDPJP
        Me.grdCPPT_KDDOCTOR.Size = New System.Drawing.Size(312, 20)
        Me.grdCPPT_KDDOCTOR.TabIndex = 2
        '
        'grvDPJP
        '
        Me.grvDPJP.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn22})
        Me.grvDPJP.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvDPJP.Name = "grvDPJP"
        Me.grvDPJP.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvDPJP.OptionsView.ShowAutoFilterRow = True
        Me.grvDPJP.OptionsView.ShowGroupPanel = False
        '
        'GridColumn22
        '
        Me.GridColumn22.Caption = "Name Display"
        Me.GridColumn22.FieldName = "NAME_DISPLAY"
        Me.GridColumn22.Name = "GridColumn22"
        Me.GridColumn22.Visible = True
        Me.GridColumn22.VisibleIndex = 0
        '
        'LabelControl29
        '
        Me.LabelControl29.Location = New System.Drawing.Point(36, 55)
        Me.LabelControl29.Name = "LabelControl29"
        Me.LabelControl29.Size = New System.Drawing.Size(75, 13)
        Me.LabelControl29.TabIndex = 42
        Me.LabelControl29.Text = "Dokter DPJP * :"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(32, 32)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(79, 13)
        Me.LabelControl1.TabIndex = 12
        Me.LabelControl1.Text = "Poli/Ruangan * :"
        '
        'txtRUANGAN
        '
        Me.txtRUANGAN.Location = New System.Drawing.Point(123, 29)
        Me.txtRUANGAN.MenuManager = Me.barManager
        Me.txtRUANGAN.Name = "txtRUANGAN"
        Me.txtRUANGAN.Properties.ReadOnly = True
        Me.txtRUANGAN.Size = New System.Drawing.Size(312, 20)
        Me.txtRUANGAN.TabIndex = 1
        '
        'txtCPPT_KeluhanUtama
        '
        Me.txtCPPT_KeluhanUtama.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCPPT_KeluhanUtama.Location = New System.Drawing.Point(12, 143)
        Me.txtCPPT_KeluhanUtama.Name = "txtCPPT_KeluhanUtama"
        Me.txtCPPT_KeluhanUtama.Size = New System.Drawing.Size(775, 63)
        Me.txtCPPT_KeluhanUtama.TabIndex = 7
        '
        'LabelControl50
        '
        Me.LabelControl50.Location = New System.Drawing.Point(12, 212)
        Me.LabelControl50.Name = "LabelControl50"
        Me.LabelControl50.Size = New System.Drawing.Size(34, 13)
        Me.LabelControl50.TabIndex = 0
        Me.LabelControl50.Text = "Alergi :"
        '
        'LabelControl16
        '
        Me.LabelControl16.Location = New System.Drawing.Point(14, 123)
        Me.LabelControl16.Name = "LabelControl16"
        Me.LabelControl16.Size = New System.Drawing.Size(79, 13)
        Me.LabelControl16.TabIndex = 0
        Me.LabelControl16.Text = "Keluhan Utama :"
        '
        'chkCPPT_AlergiTidak
        '
        Me.chkCPPT_AlergiTidak.Location = New System.Drawing.Point(13, 233)
        Me.chkCPPT_AlergiTidak.Name = "chkCPPT_AlergiTidak"
        Me.chkCPPT_AlergiTidak.Properties.Caption = "Tidak"
        Me.chkCPPT_AlergiTidak.Size = New System.Drawing.Size(50, 19)
        Me.chkCPPT_AlergiTidak.TabIndex = 8
        '
        'chkCPPT_AlergiYa
        '
        Me.chkCPPT_AlergiYa.Location = New System.Drawing.Point(13, 258)
        Me.chkCPPT_AlergiYa.Name = "chkCPPT_AlergiYa"
        Me.chkCPPT_AlergiYa.Properties.Caption = "Ya"
        Me.chkCPPT_AlergiYa.Size = New System.Drawing.Size(34, 19)
        Me.chkCPPT_AlergiYa.TabIndex = 9
        '
        'txtCPPT_AlergiYa
        '
        Me.txtCPPT_AlergiYa.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCPPT_AlergiYa.Location = New System.Drawing.Point(54, 257)
        Me.txtCPPT_AlergiYa.Name = "txtCPPT_AlergiYa"
        Me.txtCPPT_AlergiYa.Size = New System.Drawing.Size(732, 20)
        Me.txtCPPT_AlergiYa.TabIndex = 10
        '
        'LabelControl8
        '
        Me.LabelControl8.Location = New System.Drawing.Point(14, 193)
        Me.LabelControl8.Name = "LabelControl8"
        Me.LabelControl8.Size = New System.Drawing.Size(34, 13)
        Me.LabelControl8.TabIndex = 1
        Me.LabelControl8.Text = "Alergi :"
        '
        'frmCPPT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(809, 641)
        Me.Controls.Add(Me.panelCPPT)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "frmCPPT"
        Me.Text = "frmCPPT"
        Me.panelCPPT.ResumeLayout(False)
        CType(Me.GroupControl8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl8.ResumeLayout(False)
        Me.GroupControl8.PerformLayout()
        CType(Me.txtCPPT_CATATAN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDDAFTAR_L4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCPPT_Alasan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl2.ResumeLayout(False)
        CType(Me.grdSemuaTindakan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDITEMALL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvKDITEMALL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPilih, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdITEM_L2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCPPT_TotalTindakan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCPPT_Tindakan, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip2.ResumeLayout(False)
        CType(Me.BindingSourceCPPT_Tindakan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvCPPT_Tindakan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCPPT_KDITEMTINDAKAN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvCPPT_KDITEMTINDAKAN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemMemoExEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCPPT_KDUOMTINDAKAN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvCPPT_KDUOMTINDAKAN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCPPT_TemplateTindakan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvCPPT_TemplateTindakan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKDITEM_L2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XtraTabControlCPPT_Resep, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabControlCPPT_Resep.ResumeLayout(False)
        Me.tabCPPT_NonRacikan.ResumeLayout(False)
        CType(Me.LayoutControl11, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl11.ResumeLayout(False)
        CType(Me.txtCPPT_TotalNonRacikanNonPaket.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCPPT_ResepNonRacikan, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip3.ResumeLayout(False)
        CType(Me.BindingSourceCPPT_NonRacikan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvCPPT_ResepNonRacikan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCPPT_KDITEMNONRACIKAN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvCPPT_KDITEMNONRACIKAN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCPPT_KDUOMNONRACIKAN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvCPPT_KDUOMNONRACIKAN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCPPT_KDSIGNANONRACIKAN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvCPPT_KDSIGNANONRACIKAN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCPPT_KDCARAPAKAINONRACIKAN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvCPPT_KDCARAPAKAINONRACIKAN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdTEMPLATE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvWAREHOUSE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCPPT_TotalNonRacikanPaket.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem66, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabCPPT_Racikan.ResumeLayout(False)
        CType(Me.LayoutControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl4.ResumeLayout(False)
        CType(Me.txtCPPT_TotalRacikanNonPaket.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCPPT_ResepRacikan, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip4.ResumeLayout(False)
        CType(Me.BindingSourceCPPT_Racikan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvCPPT_ResepRacikan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCPPT_KDITEMRACIKAN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvCPPT_KDITEMRACIKAN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCPPT_KDUOMRACIKAN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvCPPT_KDUOMRACIKAN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCPPT_TotalRacikanPaket.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl7.ResumeLayout(False)
        Me.GroupControl7.PerformLayout()
        CType(Me.txtCPPT_CARIDIAGNOSA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCariDiagnosa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvCariDiagnosa, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCPPT_Indikasi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCPPT_Diagnosa, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.BindingSourceCPPT_Diagnosa, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvCPPT_Diagnosa, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        Me.PanelControl2.PerformLayout()
        CType(Me.txtKDIDENTITAS.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCPPT_Kode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl4.ResumeLayout(False)
        Me.GroupControl4.PerformLayout()
        CType(Me.txtCPPT_OBJEKTIF_ALAMATGAMBARPEMERIKSAAN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCPPT_VisualAnalogScore.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCPPT_GCS.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCPPT_TampakSakit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCPPT_Kesadaran.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picCPPT_Gambar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCPPT_Pemeriksaan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCPPT_Diastole.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCPPT_Sistole.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCPPT_RR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCPPT_BeratBadan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCPPT_Suhu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCPPT_SpO2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCPPT_HR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCPPT_TinggiBadan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl5.ResumeLayout(False)
        Me.GroupControl5.PerformLayout()
        CType(Me.grdCPPT_Profesi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvCPPT_Profesi, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deCPPT_Tanggal.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deCPPT_Tanggal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCPPT_KDDOCTOR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvDPJP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRUANGAN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCPPT_KeluhanUtama.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCPPT_AlergiTidak.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCPPT_AlergiYa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCPPT_AlergiYa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents panelCPPT As Panel
    Friend WithEvents GroupControl8 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtCPPT_CATATAN As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents grdKDDAFTAR_L4 As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn29 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents txtCPPT_Alasan As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LabelControl28 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl27 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents grdKDITEMALL As DevExpress.XtraGrid.GridControl
    Friend WithEvents grvKDITEMALL As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn30 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colKELOMPOK As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colNMITEM2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPilih As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents chkPilih As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents grdITEM_L2 As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView5 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn28 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents txtCPPT_TotalTindakan As DevExpress.XtraEditors.TextEdit
    Friend WithEvents grdCPPT_Tindakan As DevExpress.XtraGrid.GridControl
    Friend WithEvents grvCPPT_Tindakan As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colCPPT_KDITEMTINDAKAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdCPPT_KDITEMTINDAKAN As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents grvCPPT_KDITEMTINDAKAN As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCPPT_MEMOTINDAKAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCPPT_KDUOMTINDAKAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdCPPT_KDUOMTINDAKAN As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents grvCPPT_KDUOMTINDAKAN As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCPPT_JUMLAHTINDAKAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCPPT_HARGATINDAKAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCPPT_TOTALTINDAKAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCPPT_ISBACA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdCPPT_TemplateTindakan As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents grvCPPT_TemplateTindakan As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem13 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem14 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem4 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlItem12 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lKDITEM_L2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents XtraTabControlCPPT_Resep As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents tabCPPT_NonRacikan As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents LayoutControl11 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents txtCPPT_TotalNonRacikanNonPaket As DevExpress.XtraEditors.TextEdit
    Friend WithEvents grdCPPT_ResepNonRacikan As DevExpress.XtraGrid.GridControl
    Friend WithEvents grvCPPT_ResepNonRacikan As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colCPPT_KDITEMNONRACIKAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdCPPT_KDITEMNONRACIKAN As DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit
    Friend WithEvents grvCPPT_KDITEMNONRACIKAN As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCPPT_ISKRONISNONRACIKAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCPPT_KDUOMNONRACIKAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdCPPT_KDUOMNONRACIKAN As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents grvCPPT_KDUOMNONRACIKAN As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCPPT_KDSIGNANONRACIKAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdCPPT_KDSIGNANONRACIKAN As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents grvCPPT_KDSIGNANONRACIKAN As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCPPT_KDCARAPAKAINONRACIKAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdCPPT_KDCARAPAKAINONRACIKAN As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents grvCPPT_KDCARAPAKAINONRACIKAN As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCPPT_JUMLAHNONRACIKAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCPPT_JUMLAH_PAKETNONRACIKAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCPPT_JUMLAH_NONPAKETNONRACIKAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCPPT_HARGANONRACIKAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCPPT_TOTAL_PAKETNONRACIKAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCPPT_TOTAL_NONPAKETNONRACIKAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCPPT_TOTALNONRACIKAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCPPT_REMARKS_DOKTERNONRACIKAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCPPT_ISALKESNONRACIKAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCPPT_ISBACANONRACIKAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btnRiwayatPemberianObat As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents grdTEMPLATE As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents grvWAREHOUSE As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents txtCPPT_TotalNonRacikanPaket As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnRiwayatPemberianResep As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlGroup11 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem24 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem66 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem20 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents tabCPPT_Racikan As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents LayoutControl4 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents txtCPPT_TotalRacikanNonPaket As DevExpress.XtraEditors.TextEdit
    Friend WithEvents grdCPPT_ResepRacikan As DevExpress.XtraGrid.GridControl
    Friend WithEvents grvCPPT_ResepRacikan As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colCPPT_KDITEMRACIKAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdCPPT_KDITEMRACIKAN As DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit
    Friend WithEvents grvCPPT_KDITEMRACIKAN As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn17 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn18 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn19 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn20 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn24 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCPPT_KDUOMRACIKAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdCPPT_KDUOMRACIKAN As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents grvCPPT_KDUOMRACIKAN As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn25 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCPPT_SIGNARACIKAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCPPT_PERMINTAANRACIKAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCPPT_JUMLAHRACIKAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCPPT_HARGARACIKAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCPPT_TOTALRACIKAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCPPT_REMARKSRACIKAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCPPT_ISBACARACIKAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents txtCPPT_TotalRacikanPaket As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlGroup4 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem19 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem18 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem21 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GroupControl7 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents LabelControl33 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtCPPT_Indikasi As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents grdCPPT_Diagnosa As DevExpress.XtraGrid.GridControl
    Friend WithEvents grvCPPT_Diagnosa As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colCPPT_KATEGORI As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCPPT_MEMODIAGNOSA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCPPT_KDDIAGNOSA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents GroupControl4 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtCPPT_OBJEKTIF_ALAMATGAMBARPEMERIKSAAN As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl39 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl38 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl37 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl36 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtCPPT_VisualAnalogScore As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCPPT_GCS As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCPPT_TampakSakit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCPPT_Kesadaran As DevExpress.XtraEditors.TextEdit
    Friend WithEvents picCPPT_Gambar As PictureBox
    Friend WithEvents txtCPPT_Pemeriksaan As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents btnResetGambar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCPPT_AmbilGambar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtCPPT_Diastole As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCPPT_Sistole As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCPPT_RR As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCPPT_BeratBadan As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl14 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl13 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl30 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl19 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtCPPT_Suhu As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl20 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtCPPT_SpO2 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl11 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtCPPT_HR As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl22 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtCPPT_TinggiBadan As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl24 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl10 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl21 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl18 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl12 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl15 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl23 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl25 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GroupControl5 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtCPPT_KeluhanUtama As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LabelControl50 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl16 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents chkCPPT_AlergiTidak As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkCPPT_AlergiYa As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtCPPT_AlergiYa As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtCPPT_Kode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtKDIDENTITAS As DevExpress.XtraEditors.TextEdit
    Friend WithEvents barManager As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar3 As DevExpress.XtraBars.Bar
    Friend WithEvents btnSaveClosee As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents btnSaveNew As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnClose As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnSaveClose As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnClosee As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents progressBarSave As DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar
    Friend WithEvents progressSave As DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtRUANGAN As DevExpress.XtraEditors.TextEdit
    Friend WithEvents grdCPPT_KDDOCTOR As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents grvDPJP As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn22 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LabelControl29 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents deCPPT_Tanggal As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents grdCPPT_Profesi As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents grvCPPT_Profesi As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BindingSourceCPPT_Diagnosa As BindingSource
    Friend WithEvents BindingSourceCPPT_Tindakan As BindingSource
    Friend WithEvents BindingSourceCPPT_NonRacikan As BindingSource
    Friend WithEvents BindingSourceCPPT_Racikan As BindingSource
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents ContextMenuStrip2 As ContextMenuStrip
    Friend WithEvents DeleteToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ContextMenuStrip3 As ContextMenuStrip
    Friend WithEvents DeleteToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents ContextMenuStrip4 As ContextMenuStrip
    Friend WithEvents DeleteToolStripMenuItem3 As ToolStripMenuItem
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents DeleteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents btnbtnBuatTemplateTindakan As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents RepositoryItemMemoExEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents grdCariDiagnosa As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents grvCariDiagnosa As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents txtCPPT_CARIDIAGNOSA As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnCPPT_CariICD10 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btnObatTerakhir As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnTindakanTerakhir As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents grdSemuaTindakan As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn21 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
End Class
