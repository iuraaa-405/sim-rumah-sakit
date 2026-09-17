Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Imports DevExpress.XtraPrinting

Imports System.Data
Imports System.Data.SqlClient

Public Class frmReportUmpanBalikJasa
    Implements ILanguage
    'Private listHasil_2 As New List(Of DataAccess.R_JASA_RUMUS4)
    'Private listHasil As New List(Of DataAccess.R_DATA_HASIL)
    Private listDataHasil As New List(Of DataAccess.R_DATA_NEW)

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = CashinBPJS.TITLE_REPORT

        cboTYPE.Properties.Items.Clear()
        cboTYPE.Properties.Items.Add("Rekap1")
        cboTYPE.Properties.Items.Add("Rekap2")
        cboTYPE.Properties.Items.Add("Rekap3")
        cboTYPE.SelectedIndex = 0
        fn_LoadKDPAYMENTTYPE()
        fn_LoadKDUOM()

        cboCategory.Visible = False
        cboUom.Visible = False
        grdKDUOM.Visible = False
        cboCategory.SelectedIndex = 0
        deDATEFrom.Visible = False
        deDATETo.Visible = False
        chkGroup.Visible = False

        deDATEFrom.DateTime = Now.AddDays((-Now.Day) + 1)
        deDATETo.DateTime = Now

        chkUmum.Visible = True

    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        'Try
        '    grv.SaveLayoutToRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text & "\")
        'Catch oErr As Exception

        'End Try
    End Sub
    Public Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            Me.Text = CashinBPJS.TITLE_REPORT

            fn_LoadLanguageAll()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadSecurity()
        Try
            Dim oOtority As New Setting.clsOtority
            Dim oUser As New Setting.clsUser

            Dim ds = (From x In oOtority.GetDataDetail
                      Join y In oUser.GetData
                      On x.KDOTORITY Equals y.KDOTORITY
                      Where x.MODUL = "REPORTCASHINBPJS" _
                      And y.KDUSER = sUserID
                      Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                picPrint.Enabled = ds.ISPRINT
                picRefresh.Enabled = ds.ISVIEW

                If ds.ISVIEW = True Then
                    fn_Preview()
                    fn_LoadLanguage()
                End If
            Catch oErr As Exception
                MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)

                picPrint.Enabled = False
                picRefresh.Enabled = False
            End Try
        Catch oErr As Exception
            MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Print()
        Try
            printableComponentLink.Landscape = True
            printableComponentLink.PaperKind = Printing.PaperKind.A4

            Dim phf As PageHeaderFooter =
        TryCast(printableComponentLink.PageHeaderFooter, PageHeaderFooter)
            phf.Header.Content.Clear()
            phf.Header.Font = New Font("Times New Roman", 14, FontStyle.Bold)
            phf.Header.LineAlignment = BrickAlignment.Center
            phf.Footer.Font = New Font("Times New Roman", 9.75)
            phf.Footer.LineAlignment = BrickAlignment.Far
            phf.Footer.Content.AddRange(New String() _
        {"", "", Report.REPORT_PAGE & " : [Page # of Pages #]"})

            phf.Header.Content.AddRange(New String() _
{"", "" & vbCrLf, ""})

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
            grv1.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            If chkUmum.Checked = False Then
                'If grvKDPAYMENTTYPE.GetFocusedRowCellValue("JENISRAWAT") = "Rawat Jalan" Then
                '    fn_LoadDataRawatJalan()
                'Else
                '    fn_LoadDataRawatInap()
                'End If

                fn_LoadDataWaka()

            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

#Region "All"
    Private Sub fn_LoadDataWaka()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim listDataAwal As New List(Of DataAccess.R_DATA_NEW)

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            Dim oJasa As New Finance.clsJasa
            Dim dsDatabase = oJasa.GetDataSetting

            SQL = "EXEC DATABASE_RSGUNTUR.dbo.TRANSAKSI_BPJS_NEW "
            SQL &= "@CATEGORY = " & cboTYPE.SelectedIndex & ", "
            SQL &= "@CATEGORY_STRING_1 = " & IIf(grdKDPAYMENTTYPE.Text = String.Empty, "''", grdKDPAYMENTTYPE.EditValue) & ", "
            SQL &= "@CATEGORY_STRING_2 = " & IIf(chkUmum.Checked = False, "'BPJS'", "'UMUM'") & ", "
            SQL &= "@CATEGORY_STRING_3 = '', "
            SQL &= "@DATEFROM = '" & deDATEFrom.DateTime & "', "
            SQL &= "@DATETO = '" & deDATETo.DateTime & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

            For iLoop As Integer = 0 To ds.Tables("ALL").Rows.Count - 1
                Dim dsRekap As New DataAccess.R_DATA_NEW
                With ds.Tables("ALL")
                    dsRekap.TRANSAKSI = .Rows(iLoop)("NOSEP")
                    dsRekap.UNIT = .Rows(iLoop)("UNIT")
                    dsRekap.ADMISSION_DATE = .Rows(iLoop)("ADMISSION_DATE")
                    dsRekap.KDCUSTOMER = .Rows(iLoop)("KDCUSTOMER")
                    dsRekap.NAMA = .Rows(iLoop)("NAMA")
                    dsRekap.DOKTER_DPJP = .Rows(iLoop)("DOKTER_DPJP")
                    dsRekap.DELEGASI = .Rows(iLoop)("DELEGASI")
                    dsRekap.TOTAL_TARIF = CDec(.Rows(iLoop)("TOTAL_TARIF"))
                    dsRekap.RS = CDec(.Rows(iLoop)("RS"))
                    dsRekap.BHP = CDec(.Rows(iLoop)("BHP"))
                    dsRekap.JASA = CDec(.Rows(iLoop)("JASA"))
                    dsRekap.FUNGSIONAL = CDec(.Rows(iLoop)("FUNGSIONAL"))
                    dsRekap.DOKTER = CDec(.Rows(iLoop)("DOKTER"))
                    dsRekap.TOTAL_DPJP = CDec(.Rows(iLoop)("TOTAL_DPJP"))
                    dsRekap.TOTAL_DELEGASI = CDec(.Rows(iLoop)("TOTAL_DELEGASI"))
                    dsRekap.PERAWAT = CDec(.Rows(iLoop)("PERAWAT"))
                    dsRekap.NAKESLAIN = CDec(.Rows(iLoop)("NAKESLAIN"))
                    dsRekap.MANAJEMEN = CDec(.Rows(iLoop)("MANAJEMEN"))

                    dsRekap.VISITE = .Rows(iLoop)("VISITE")
                    dsRekap.ISPROSEDUR = .Rows(iLoop)("ISPROSEDUR")

                    listDataAwal.Add(dsRekap)
                End With
            Next


            Dim ListVisite As New List(Of DataAccess.R_PARAMETER_HASIL)

            For Each xloop In listDataAwal.Where(Function(x) x.UNIT = "XX")
                Dim dsRekap As New DataAccess.R_PARAMETER_HASIL
                dsRekap.Transaksi = xloop.TRANSAKSI
                dsRekap.Total = xloop.VISITE
                ListVisite.Add(dsRekap)
            Next

            Dim ListUnit As New List(Of DataAccess.R_PARAMETER_HASIL)

            For Each xloop In listDataAwal.Where(Function(x) x.UNIT = "XXX")
                Dim dsRekap As New DataAccess.R_PARAMETER_HASIL
                dsRekap.Transaksi = xloop.TRANSAKSI
                dsRekap.Total = xloop.VISITE
                ListUnit.Add(dsRekap)
            Next

            Dim ListCount As New List(Of DataAccess.R_PARAMETER_HASIL)

            Dim dsCount = From x In listDataAwal.Where(Function(x) x.UNIT <> "XX" And x.UNIT <> "XXX")
                          Group x By x.TRANSAKSI Into Total = Count(x.TOTAL_TARIF)

            For Each xloop In dsCount
                Dim dsRekap As New DataAccess.R_PARAMETER_HASIL
                dsRekap.Transaksi = xloop.TRANSAKSI
                dsRekap.Total = xloop.Total
                ListCount.Add(dsRekap)
            Next

            listDataHasil.Clear()

            For Each xloop In listDataAwal.Where(Function(x) x.UNIT <> "XX" And x.UNIT <> "XXX")
                Dim dsRekap As New DataAccess.R_DATA_NEW

                dsRekap.TRANSAKSI = xloop.TRANSAKSI
                dsRekap.UNIT = xloop.UNIT
                dsRekap.ADMISSION_DATE = xloop.ADMISSION_DATE
                dsRekap.KDCUSTOMER = xloop.KDCUSTOMER
                dsRekap.NAMA = xloop.NAMA
                dsRekap.DOKTER_DPJP = xloop.DOKTER_DPJP
                dsRekap.DELEGASI = xloop.DELEGASI
                dsRekap.TOTAL_TARIF = CDec(xloop.TOTAL_TARIF)
                dsRekap.RS = CDec(xloop.RS)
                dsRekap.BHP = CDec(xloop.BHP)
                dsRekap.JASA = CDec(xloop.JASA)
                dsRekap.FUNGSIONAL = CDec(xloop.FUNGSIONAL)
                dsRekap.DOKTER = CDec(xloop.DOKTER)
                dsRekap.TOTAL_DPJP = CDec(xloop.TOTAL_DPJP / ListCount.FirstOrDefault(Function(x) x.Transaksi = xloop.TRANSAKSI).Total)
                dsRekap.TOTAL_DELEGASI = CDec(xloop.TOTAL_DELEGASI / ListCount.FirstOrDefault(Function(x) x.Transaksi = xloop.TRANSAKSI).Total)
                dsRekap.PERAWAT = CDec(xloop.PERAWAT / ListCount.FirstOrDefault(Function(x) x.Transaksi = xloop.TRANSAKSI).Total)
                dsRekap.NAKESLAIN = CDec(xloop.NAKESLAIN / ListCount.FirstOrDefault(Function(x) x.Transaksi = xloop.TRANSAKSI).Total)
                dsRekap.MANAJEMEN = CDec(xloop.MANAJEMEN / ListCount.FirstOrDefault(Function(x) x.Transaksi = xloop.TRANSAKSI).Total)

                dsRekap.VISITE = xloop.VISITE
                dsRekap.ISPROSEDUR = xloop.ISPROSEDUR

                listDataHasil.Add(dsRekap)
            Next

            If cboTYPE.SelectedIndex = 0 Then
                grd.MainView = grv
                grd.DataSource = listDataHasil
                grd.ForceInitialize()


            ElseIf cboTYPE.SelectedIndex = 1 Then

                Dim dsDokterDPJP = From x In listDataHasil
                                   Group x By KET = "DOKTER_DPJP", x.TRANSAKSI, NAME_DISPLAY = x.DOKTER_DPJP, x.JASA, x.ADMISSION_DATE, x.KDCUSTOMER, x.NAMA Into TOTAL = Sum(x.TOTAL_DPJP)

                Dim dsDokterDELEGASI = From x In listDataHasil
                                       Group x By KET = "DOKTER_DELEGASI", x.TRANSAKSI, NAME_DISPLAY = x.DELEGASI, x.JASA, x.ADMISSION_DATE, x.KDCUSTOMER, x.NAMA Into TOTAL = Sum(x.TOTAL_DELEGASI)


                grd.MainView = grv
                grd.DataSource = dsDokterDPJP.Union(dsDokterDELEGASI)
                grd.ForceInitialize()


            End If


            fn_LoadFormatDataAll()


        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    'Private Sub fn_LoadDataRawatJalan()
    '    Try
    '        listHasil_2.Clear()

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

    '        Dim oJasa As New Finance.clsJasa
    '        Dim dsDatabase = oJasa.GetDataSetting

    '        SQL = dsDatabase.COA_SALES_DEPOSIT

    '        SQL &= " @CATEGORY = " & IIf(cboTYPE.SelectedIndex = 1, IIf(chkUmum.Checked = False, 8, 9), cboTYPE.SelectedIndex) & ", "
    '        SQL &= " @CATEGORY_STRING_1 = " & IIf(grdKDPAYMENTTYPE.Text = String.Empty, "''", grdKDPAYMENTTYPE.EditValue) & ", "
    '        SQL &= " @CATEGORY_STRING_2 = " & IIf(chkUmum.Checked = False, "'BPJS'", "'UMUM'") & ", "
    '        SQL &= " @CATEGORY_STRING_3 = '', "
    '        'SQL &= " @DATEFROM = '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "', "
    '        'SQL &= " @DATETO = '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
    '        SQL &= " @DATEFROM = '" & deDATEFrom.DateTime & "', "
    '        SQL &= " @DATETO = '" & deDATETo.DateTime & "' "

    '        oComm.Connection = oConn
    '        oComm.CommandText = SQL
    '        oComm.CommandTimeout = 120
    '        oComm.CommandType = CommandType.Text

    '        da = New SqlDataAdapter(oComm)
    '        da.Fill(ds, "ALL")

    '        If cboTYPE.SelectedIndex = 0 Then
    '            grd.MainView = grv
    '            grd.DataSource = ds.Tables("ALL")
    '            grd.ForceInitialize()

    '        ElseIf cboTYPE.SelectedIndex = 1 Then
    '            Dim ListCategoryNonTindakan As New List(Of DataAccess.R_PARAMETER_HASIL)
    '            Dim ListCategoryPerawat As New List(Of DataAccess.R_PARAMETER_HASIL)
    '            Dim ListCategorySemuaPoli As New List(Of DataAccess.R_PARAMETER_HASIL)
    '            Dim ListCategoryLabRad As New List(Of DataAccess.R_PARAMETER_HASIL)
    '            Dim ListCategoryLab As New List(Of DataAccess.R_PARAMETER_HASIL)
    '            Dim ListCategoryRad As New List(Of DataAccess.R_PARAMETER_HASIL)
    '            Dim ListPoli As New List(Of DataAccess.R_PARAMETER_NAME_DISPLAY)
    '            Dim listDataAwal As New List(Of DataAccess.R_DATA_AWAL)
    '            Dim listDataAwal_x As New List(Of DataAccess.R_DATA_AWAL)

    '            For iLoop As Integer = 0 To ds.Tables("ALL").Rows.Count - 1
    '                Dim dsRekap As New DataAccess.R_DATA_AWAL
    '                With ds.Tables("ALL")
    '                    Dim TRANSAKSI As String = ""
    '                    TRANSAKSI = .Rows(iLoop)("TRANSAKSI")

    '                    dsRekap.TRANSAKSI = .Rows(iLoop)("TRANSAKSI")
    '                    dsRekap.TUJUAN = ""
    '                    dsRekap.TIPE_1 = .Rows(iLoop)("TIPE_1")
    '                    dsRekap.TIPE_3 = ""
    '                    dsRekap.TIPE_4 = ""
    '                    dsRekap.TIPE_5 = ""
    '                    dsRekap.NOSEP = .Rows(iLoop)("NOSEP")
    '                    dsRekap.RUGI = .Rows(iLoop)("RUGI")
    '                    dsRekap.SUB_DOKTER_DPJP = .Rows(iLoop)("SUB_DOKTER_DPJP")
    '                    dsRekap.SUB_DOKTER = .Rows(iLoop)("SUB_DOKTER")
    '                    dsRekap.ADMISSION_DATE = .Rows(iLoop)("ADMISSION_DATE")
    '                    dsRekap.DISCHARGE_DATE = .Rows(iLoop)("DISCHARGE_DATE")
    '                    dsRekap.KDCUSTOMER = .Rows(iLoop)("KDCUSTOMER")
    '                    dsRekap.NO_TRANSAKSI = .Rows(iLoop)("NO_TRANSAKSI")
    '                    dsRekap.NAMA = .Rows(iLoop)("NAMA")
    '                    dsRekap.DOKTER_DPJP = .Rows(iLoop)("DOKTER_DPJP")
    '                    dsRekap.DOKTER = .Rows(iLoop)("DOKTER")
    '                    dsRekap.UNIT = .Rows(iLoop)("UNIT")
    '                    dsRekap.GROUPTARIF = .Rows(iLoop)("GROUPTARIF")
    '                    dsRekap.TARIF = .Rows(iLoop)("TARIF")
    '                    dsRekap.HARGTARIF = CDec(.Rows(iLoop)("HARGTARIF"))
    '                    dsRekap.TOTAL_TARIF = CDec(.Rows(iLoop)("TOTAL_TARIF"))
    '                    dsRekap.TARIF_RS = CDec(.Rows(iLoop)("TARIF_RS"))
    '                    dsRekap.DIAGLIST = .Rows(iLoop)("DIAGLIST")
    '                    dsRekap.TINDAKAN = .Rows(iLoop)("TINDAKAN")
    '                    dsRekap.BAGIRS_30 = CDec(.Rows(iLoop)("BAGIRS_30"))
    '                    dsRekap.BAGIBHP = CDec(.Rows(iLoop)("BAGIBHP"))
    '                    dsRekap.BIAYAOBAT = CDec(.Rows(iLoop)("BIAYAOBAT"))
    '                    dsRekap.TOTALRS = CDec(.Rows(iLoop)("TOTALRS"))

    '                    listDataAwal.Add(dsRekap)
    '                End With
    '            Next

    '            Dim dsNonTindakan = From x In listDataAwal
    '                                Where x.TINDAKAN = "TINDAKAN" And x.GROUPTARIF <> "OBAT" And x.GROUPTARIF <> "LABORATORIUM" And x.GROUPTARIF <> "RADIOLOGI"
    '                                Group x By x.TRANSAKSI Into Total = Sum(x.TARIF_RS)
    '                                Select TRANSAKSI, Total

    '            Dim dsSeluruhPoli = From x In listDataAwal
    '                                Where x.GROUPTARIF = "LABORATORIUM" Or x.GROUPTARIF = "RADIOLOGI"
    '                                Group x By x.TRANSAKSI Into Total = Sum(x.TARIF_RS)
    '                                Select TRANSAKSI, Total

    '            Dim dsLab = From x In listDataAwal
    '                        Where x.GROUPTARIF = "LABORATORIUM"
    '                        Group x By x.TRANSAKSI Into Total = Sum(x.TARIF_RS)
    '                        Select TRANSAKSI, Total

    '            Dim dsRad = From x In listDataAwal
    '                        Where x.GROUPTARIF = "RADIOLOGI"
    '                        Group x By x.TRANSAKSI Into Total = Sum(x.TARIF_RS)
    '                        Select TRANSAKSI, Total

    '            Dim dsLabRad = From x In dsLab
    '                           Join y In dsRad
    '                           On x.TRANSAKSI Equals y.TRANSAKSI
    '                           Group x By x.TRANSAKSI Into Total = Sum(x.Total)
    '                           Select TRANSAKSI, Total

    '            Dim dsPoliTujuan = From x In listDataAwal
    '                               Where x.UNIT <> "Laboratorium Klinik" And x.UNIT <> "Apotek" And x.UNIT <> "Radiologi"
    '                               Group x By x.TRANSAKSI, x.UNIT Into Total = Sum(x.TARIF_RS)
    '                               Select TRANSAKSI, Total, UNIT

    '            Dim dsCekPerawat = From x In listDataAwal
    '                               Where x.TARIF <> "Pemeriksaan Dokter Spesialis"
    '                               Group x By x.TRANSAKSI Into Total = Sum(x.TARIF_RS)
    '                               Select TRANSAKSI, Total

    '            For Each xloop In dsCekPerawat
    '                Dim dsRekap As New DataAccess.R_PARAMETER_HASIL
    '                dsRekap.Transaksi = xloop.TRANSAKSI
    '                dsRekap.Total = xloop.Total
    '                ListCategoryPerawat.Add(dsRekap)
    '            Next

    '            For Each xloop In dsNonTindakan
    '                Dim dsRekap As New DataAccess.R_PARAMETER_HASIL
    '                dsRekap.Transaksi = xloop.TRANSAKSI
    '                dsRekap.Total = xloop.Total
    '                ListCategoryNonTindakan.Add(dsRekap)
    '            Next
    '            For Each xloop In dsSeluruhPoli
    '                Dim dsRekap As New DataAccess.R_PARAMETER_HASIL
    '                dsRekap.Transaksi = xloop.TRANSAKSI
    '                dsRekap.Total = xloop.Total
    '                ListCategorySemuaPoli.Add(dsRekap)
    '            Next
    '            For Each xloop In dsLabRad
    '                Dim dsRekap As New DataAccess.R_PARAMETER_HASIL
    '                dsRekap.Transaksi = xloop.TRANSAKSI
    '                dsRekap.Total = xloop.Total
    '                ListCategoryLabRad.Add(dsRekap)
    '            Next
    '            For Each xloop In dsLab
    '                Dim dsRekap As New DataAccess.R_PARAMETER_HASIL
    '                dsRekap.Transaksi = xloop.TRANSAKSI
    '                dsRekap.Total = xloop.Total
    '                ListCategoryLab.Add(dsRekap)
    '            Next
    '            For Each xloop In dsRad
    '                Dim dsRekap As New DataAccess.R_PARAMETER_HASIL
    '                dsRekap.Transaksi = xloop.TRANSAKSI
    '                dsRekap.Total = xloop.Total
    '                ListCategoryRad.Add(dsRekap)
    '            Next

    '            For Each xloop In dsPoliTujuan
    '                Dim dsRekap As New DataAccess.R_PARAMETER_NAME_DISPLAY
    '                dsRekap.Transaksi = xloop.TRANSAKSI
    '                dsRekap.NAME_DISPLAY_1 = xloop.UNIT
    '                dsRekap.NAME_DISPLAY_2 = ""
    '                dsRekap.NAME_DISPLAY_3 = ""
    '                dsRekap.NAME_DISPLAY_4 = ""
    '                dsRekap.Total = xloop.Total
    '                ListPoli.Add(dsRekap)
    '            Next

    '            For Each xloop In listDataAwal
    '                Dim dsRekap As New DataAccess.R_DATA_AWAL
    '                dsRekap.TRANSAKSI = xloop.TRANSAKSI
    '                dsRekap.TUJUAN = ListPoli.FirstOrDefault(Function(x) x.Transaksi = xloop.TRANSAKSI).NAME_DISPLAY_1
    '                dsRekap.RUGI = xloop.RUGI
    '                dsRekap.TIPE_1 = xloop.TIPE_1

    '                Dim dsTindakan = ListCategoryNonTindakan.Where(Function(x) x.Transaksi = xloop.TRANSAKSI)

    '                If dsTindakan.Count > 0 Then
    '                    dsRekap.TIPE_3 = "TINDAKAN"
    '                Else
    '                    dsRekap.TIPE_3 = "NON TINDAKAN"
    '                End If

    '                Dim dsSeluruh = ListCategorySemuaPoli.Where(Function(x) x.Transaksi = xloop.TRANSAKSI)

    '                If dsSeluruh.Count > 0 Then
    '                    dsRekap.TIPE_4 = "NON SEMUA POLI"
    '                Else
    '                    dsRekap.TIPE_4 = "SEMUA POLI"
    '                End If

    '                Dim dsLabRad_ = ListCategoryLabRad.Where(Function(x) x.Transaksi = xloop.TRANSAKSI)

    '                If dsLabRad_.Count > 0 Then
    '                    dsRekap.TIPE_5 = "LAB DAN RADIOLOGI"
    '                Else
    '                    dsRekap.TIPE_5 = "NON LAB DAN RADIOLOGI"
    '                End If

    '                dsRekap.NOSEP = xloop.NOSEP
    '                dsRekap.SUB_DOKTER_DPJP = xloop.SUB_DOKTER_DPJP
    '                dsRekap.SUB_DOKTER = xloop.SUB_DOKTER
    '                dsRekap.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                dsRekap.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                dsRekap.KDCUSTOMER = xloop.KDCUSTOMER
    '                dsRekap.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                dsRekap.NAMA = xloop.NAMA
    '                dsRekap.DOKTER_DPJP = xloop.DOKTER_DPJP
    '                dsRekap.DOKTER = xloop.DOKTER
    '                dsRekap.UNIT = xloop.UNIT
    '                dsRekap.GROUPTARIF = xloop.GROUPTARIF
    '                dsRekap.TARIF = xloop.TARIF
    '                dsRekap.HARGTARIF = xloop.HARGTARIF
    '                dsRekap.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                dsRekap.TARIF_RS = xloop.TARIF_RS
    '                dsRekap.DIAGLIST = xloop.DIAGLIST
    '                dsRekap.TINDAKAN = xloop.TINDAKAN
    '                dsRekap.BAGIRS_30 = xloop.BAGIRS_30
    '                dsRekap.BAGIBHP = xloop.BAGIBHP
    '                dsRekap.BIAYAOBAT = xloop.BIAYAOBAT
    '                dsRekap.TOTALRS = xloop.TOTALRS

    '                listDataAwal_x.Add(dsRekap)
    '            Next

    '            If cboUom.SelectedIndex = 0 Then
    '                If cboCategory.SelectedIndex = 0 Then
    '                    grd.MainView = grv
    '                    grd.DataSource = listDataAwal_x
    '                    grd.ForceInitialize()

    '                ElseIf cboCategory.SelectedIndex = 1 Then
    '                    grd.MainView = grv
    '                    grd.DataSource = listDataAwal_x.Where(Function(x) x.RUGI = False)
    '                    grd.ForceInitialize()

    '                Else
    '                    grd.MainView = grv
    '                    grd.DataSource = listDataAwal_x.Where(Function(x) x.RUGI = True)
    '                    grd.ForceInitialize()

    '                End If

    '            ElseIf cboUom.SelectedIndex = 1 Then
    '                listHasil.Clear()
    '                Dim listRumus As New List(Of DataAccess.R_PARAMETER_NAME_DISPLAY)

    '                Dim dsGroup = From x In listDataAwal_x
    '                              Group x By x.TIPE_1, x.TIPE_3, x.TIPE_4, x.TIPE_5, x.TRANSAKSI, x.TUJUAN, x.NOSEP, x.RUGI, x.ADMISSION_DATE, x.DISCHARGE_DATE, x.KDCUSTOMER, x.NO_TRANSAKSI, x.NAMA, x.DOKTER_DPJP, x.DOKTER, x.DIAGLIST, x.TOTAL_TARIF, x.TARIF_RS Into TOTAL = Sum(x.HARGTARIF), BAGIRS_30 = Sum(x.BAGIRS_30), BAGIBHP = Sum(x.BAGIBHP), BIAYAOBAT = Sum(x.BIAYAOBAT), TOTALRS = Sum(x.TOTALRS)

    '                Dim ListCategoryUSGBIDAN As New List(Of DataAccess.R_PARAMETER_HASIL)

    '                Dim dsusgBIDAN = From x In listDataAwal_x
    '                                 Where x.TUJUAN.Contains("Kandungan")
    '                                 Group x By x.TRANSAKSI Into Total = Sum(x.TARIF_RS)
    '                                 Select TRANSAKSI, Total

    '                For Each xloop In dsusgBIDAN
    '                    Dim dsRekap As New DataAccess.R_PARAMETER_HASIL
    '                    dsRekap.Transaksi = xloop.TRANSAKSI
    '                    dsRekap.Total = xloop.Total
    '                    ListCategoryUSGBIDAN.Add(dsRekap)
    '                Next


    '                For Each xloop In dsGroup
    '                    Dim dsRekap As New DataAccess.R_PARAMETER_NAME_DISPLAY

    '                    dsRekap.Transaksi = xloop.TRANSAKSI
    '                    If xloop.TIPE_3 = "NON TINDAKAN" Then
    '                        Dim dsUSGBIDAN_ = ListCategoryUSGBIDAN.Where(Function(x) x.Transaksi = xloop.TRANSAKSI)

    '                        If dsUSGBIDAN_.Count > 0 Then
    '                            Dim dsRad1 = ListCategoryRad.Where(Function(x) x.Transaksi = xloop.TRANSAKSI)
    '                            If dsRad1.Count > 0 Then
    '                                If Not xloop.DOKTER.Contains("UMUM") Then
    '                                    dsRekap.NAME_DISPLAY_1 = "07"
    '                                Else
    '                                    dsRekap.NAME_DISPLAY_1 = "12"
    '                                End If

    '                            Else
    '                                dsRekap.NAME_DISPLAY_1 = "09"
    '                            End If
    '                        Else
    '                            If xloop.TUJUAN <> "PDP" Then
    '                                If xloop.TIPE_4 = "SEMUA POLI" Then
    '                                    If xloop.TIPE_1 = "SATU DOKTER" Then
    '                                        dsRekap.NAME_DISPLAY_1 = "01"
    '                                    Else
    '                                        If Not xloop.DOKTER.Contains("UMUM") Then
    '                                            dsRekap.NAME_DISPLAY_1 = "02"
    '                                        Else
    '                                            dsRekap.NAME_DISPLAY_1 = "03"
    '                                        End If
    '                                    End If
    '                                Else
    '                                    If xloop.TIPE_5 = "NON LAB DAN RADIOLOGI" Then
    '                                        Dim dsLab1 = ListCategoryLab.Where(Function(x) x.Transaksi = xloop.TRANSAKSI)
    '                                        If dsLab1.Count > 0 Then

    '                                            If Not xloop.DOKTER.Contains("UMUM") Then
    '                                                dsRekap.NAME_DISPLAY_1 = "05"
    '                                            Else
    '                                                dsRekap.NAME_DISPLAY_1 = "10"
    '                                            End If

    '                                        End If
    '                                        Dim dsRad1 = ListCategoryRad.Where(Function(x) x.Transaksi = xloop.TRANSAKSI)
    '                                        If dsRad1.Count > 0 Then
    '                                            If Not xloop.DOKTER.Contains("UMUM") Then
    '                                                dsRekap.NAME_DISPLAY_1 = "06"

    '                                            Else
    '                                                dsRekap.NAME_DISPLAY_1 = "11"

    '                                            End If
    '                                        End If
    '                                    Else
    '                                        If Not xloop.DOKTER.Contains("UMUM") Then
    '                                            dsRekap.NAME_DISPLAY_1 = "08"
    '                                        Else
    '                                            dsRekap.NAME_DISPLAY_1 = "13"
    '                                        End If

    '                                    End If
    '                                End If
    '                            Else
    '                                dsRekap.NAME_DISPLAY_1 = "04"
    '                            End If
    '                        End If

    '                    Else
    '                        If xloop.TUJUAN = "Hemodialisa" Then
    '                            dsRekap.NAME_DISPLAY_1 = "15"
    '                        ElseIf xloop.TUJUAN = "Mata" Then
    '                            dsRekap.NAME_DISPLAY_1 = "01"
    '                        ElseIf xloop.TUJUAN = "Thalassemia" Or xloop.TUJUAN = "Thalassaemia" Then
    '                            dsRekap.NAME_DISPLAY_1 = "16"
    '                        Else
    '                            If xloop.TUJUAN = "Bedah" Then
    '                                Dim dsPerawat = ListCategoryPerawat.Where(Function(x) x.Transaksi = xloop.TRANSAKSI)
    '                                If dsPerawat.Count > 0 Then
    '                                    dsRekap.NAME_DISPLAY_1 = "14"
    '                                Else
    '                                    dsRekap.NAME_DISPLAY_1 = "01"
    '                                End If
    '                            Else
    '                                If Not xloop.DOKTER.Contains("UMUM") Then
    '                                    dsRekap.NAME_DISPLAY_1 = "01"
    '                                Else
    '                                    dsRekap.NAME_DISPLAY_1 = "03"
    '                                End If
    '                            End If
    '                        End If
    '                    End If

    '                    dsRekap.NAME_DISPLAY_2 = ""
    '                    dsRekap.NAME_DISPLAY_3 = ""
    '                    dsRekap.NAME_DISPLAY_4 = ""
    '                    dsRekap.Total = 0

    '                    listRumus.Add(dsRekap)

    '                Next

    '                Dim dsAll = From x In dsGroup
    '                            Join y In listRumus
    '                            On x.TRANSAKSI Equals y.Transaksi
    '                            Select CEK = y.NAME_DISPLAY_1, x.TIPE_1, x.TIPE_3, x.TIPE_4, x.TIPE_5, x.TRANSAKSI, x.TUJUAN, x.NOSEP, x.RUGI, x.ADMISSION_DATE, x.DISCHARGE_DATE, x.KDCUSTOMER, x.NO_TRANSAKSI, x.NAMA, x.DOKTER_DPJP, x.DOKTER, x.DIAGLIST, x.TOTAL_TARIF, x.TARIF_RS, x.TOTAL, x.BAGIRS_30, x.BAGIBHP, x.BIAYAOBAT, x.TOTALRS

    '                For Each xloop In dsAll
    '                    Dim Sisa As Decimal = 0
    '                    Dim SisaPaket As Decimal = 0
    '                    Sisa = xloop.TOTAL_TARIF - xloop.TOTALRS
    '                    SisaPaket = xloop.TOTALRS

    '                    If Sisa <= 0 Then
    '                        Dim dsJasaRS As New DataAccess.R_DATA_HASIL
    '                        Dim dsJasaBHP As New DataAccess.R_DATA_HASIL
    '                        Dim dsJasaBiayaObat As New DataAccess.R_DATA_HASIL

    '                        'RS 30 %
    '                        dsJasaRS.CEK = "00"
    '                        dsJasaRS.TRANSAKSI = xloop.TRANSAKSI
    '                        dsJasaRS.KET = "RS 30 %"
    '                        dsJasaRS.TUJUAN = xloop.TUJUAN
    '                        dsJasaRS.NOSEP = xloop.NOSEP
    '                        dsJasaRS.RUGI = xloop.RUGI
    '                        dsJasaRS.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                        dsJasaRS.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                        dsJasaRS.KDCUSTOMER = xloop.KDCUSTOMER
    '                        dsJasaRS.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                        dsJasaRS.NAMA = xloop.NAMA
    '                        dsJasaRS.NAME_DISPLAY = "RS"
    '                        dsJasaRS.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                        dsJasaRS.TARIF_RS = xloop.TARIF_RS
    '                        dsJasaRS.DIAGLIST = xloop.DIAGLIST
    '                        dsJasaRS.BAGIJASA = xloop.BAGIRS_30
    '                        dsJasaRS.SISAPAKET = 0

    '                        listHasil.Add(dsJasaRS)

    '                        'BHP 35 %
    '                        dsJasaBHP.CEK = "00"
    '                        dsJasaBHP.TRANSAKSI = xloop.TRANSAKSI
    '                        dsJasaBHP.KET = "BHP 35 %"
    '                        dsJasaBHP.TUJUAN = xloop.TUJUAN
    '                        dsJasaBHP.NOSEP = xloop.NOSEP
    '                        dsJasaBHP.RUGI = xloop.RUGI
    '                        dsJasaBHP.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                        dsJasaBHP.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                        dsJasaBHP.KDCUSTOMER = xloop.KDCUSTOMER
    '                        dsJasaBHP.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                        dsJasaBHP.NAMA = xloop.NAMA
    '                        dsJasaBHP.NAME_DISPLAY = "RS"
    '                        dsJasaBHP.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                        dsJasaBHP.TARIF_RS = xloop.TARIF_RS
    '                        dsJasaBHP.DIAGLIST = xloop.DIAGLIST
    '                        dsJasaBHP.BAGIJASA = xloop.BAGIBHP
    '                        dsJasaBHP.SISAPAKET = 0

    '                        listHasil.Add(dsJasaBHP)

    '                        'OBAT 100 %
    '                        dsJasaBiayaObat.CEK = "00"
    '                        dsJasaBiayaObat.TRANSAKSI = xloop.TRANSAKSI
    '                        dsJasaBiayaObat.KET = "BIAYA OBAT 100 %"
    '                        dsJasaBiayaObat.TUJUAN = xloop.TUJUAN
    '                        dsJasaBiayaObat.NOSEP = xloop.NOSEP
    '                        dsJasaBiayaObat.RUGI = xloop.RUGI
    '                        dsJasaBiayaObat.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                        dsJasaBiayaObat.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                        dsJasaBiayaObat.KDCUSTOMER = xloop.KDCUSTOMER
    '                        dsJasaBiayaObat.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                        dsJasaBiayaObat.NAMA = xloop.NAMA
    '                        dsJasaBiayaObat.NAME_DISPLAY = "RS"
    '                        dsJasaBiayaObat.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                        dsJasaBiayaObat.TARIF_RS = xloop.TARIF_RS
    '                        dsJasaBiayaObat.DIAGLIST = xloop.DIAGLIST
    '                        dsJasaBiayaObat.BAGIJASA = xloop.BIAYAOBAT
    '                        dsJasaBiayaObat.SISAPAKET = Sisa

    '                        listHasil.Add(dsJasaBiayaObat)

    '                    Else
    '                        Dim dsJasaRS As New DataAccess.R_DATA_HASIL
    '                        Dim dsJasaBHP As New DataAccess.R_DATA_HASIL
    '                        Dim dsJasaBiayaObat As New DataAccess.R_DATA_HASIL

    '                        'RS 30 %
    '                        dsJasaRS.CEK = xloop.CEK
    '                        dsJasaRS.TRANSAKSI = xloop.TRANSAKSI
    '                        dsJasaRS.KET = "RS 30 %"
    '                        dsJasaRS.TUJUAN = xloop.TUJUAN
    '                        dsJasaRS.NOSEP = xloop.NOSEP
    '                        dsJasaRS.RUGI = xloop.RUGI
    '                        dsJasaRS.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                        dsJasaRS.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                        dsJasaRS.KDCUSTOMER = xloop.KDCUSTOMER
    '                        dsJasaRS.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                        dsJasaRS.NAMA = xloop.NAMA
    '                        dsJasaRS.NAME_DISPLAY = "RS"
    '                        dsJasaRS.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                        dsJasaRS.TARIF_RS = xloop.TARIF_RS
    '                        dsJasaRS.DIAGLIST = xloop.DIAGLIST
    '                        dsJasaRS.BAGIJASA = xloop.BAGIRS_30
    '                        dsJasaRS.SISAPAKET = 0

    '                        listHasil.Add(dsJasaRS)

    '                        'BHP 35 %
    '                        dsJasaBHP.CEK = xloop.CEK
    '                        dsJasaBHP.TRANSAKSI = xloop.TRANSAKSI
    '                        dsJasaBHP.KET = "BHP 35 %"
    '                        dsJasaBHP.TUJUAN = xloop.TUJUAN
    '                        dsJasaBHP.NOSEP = xloop.NOSEP
    '                        dsJasaBHP.RUGI = xloop.RUGI
    '                        dsJasaBHP.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                        dsJasaBHP.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                        dsJasaBHP.KDCUSTOMER = xloop.KDCUSTOMER
    '                        dsJasaBHP.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                        dsJasaBHP.NAMA = xloop.NAMA
    '                        dsJasaBHP.NAME_DISPLAY = "RS"
    '                        dsJasaBHP.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                        dsJasaBHP.TARIF_RS = xloop.TARIF_RS
    '                        dsJasaBHP.DIAGLIST = xloop.DIAGLIST
    '                        dsJasaBHP.BAGIJASA = xloop.BAGIBHP
    '                        dsJasaBHP.SISAPAKET = 0

    '                        listHasil.Add(dsJasaBHP)

    '                        'OBAT 100 %
    '                        dsJasaBiayaObat.CEK = xloop.CEK
    '                        dsJasaBiayaObat.TRANSAKSI = xloop.TRANSAKSI
    '                        dsJasaBiayaObat.KET = "BIAYA OBAT 100 %"
    '                        dsJasaBiayaObat.TUJUAN = xloop.TUJUAN
    '                        dsJasaBiayaObat.NOSEP = xloop.NOSEP
    '                        dsJasaBiayaObat.RUGI = xloop.RUGI
    '                        dsJasaBiayaObat.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                        dsJasaBiayaObat.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                        dsJasaBiayaObat.KDCUSTOMER = xloop.KDCUSTOMER
    '                        dsJasaBiayaObat.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                        dsJasaBiayaObat.NAMA = xloop.NAMA
    '                        dsJasaBiayaObat.NAME_DISPLAY = "RS"
    '                        dsJasaBiayaObat.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                        dsJasaBiayaObat.TARIF_RS = xloop.TARIF_RS
    '                        dsJasaBiayaObat.DIAGLIST = xloop.DIAGLIST
    '                        dsJasaBiayaObat.BAGIJASA = xloop.BIAYAOBAT
    '                        dsJasaBiayaObat.SISAPAKET = 0

    '                        listHasil.Add(dsJasaBiayaObat)

    '                        If xloop.CEK = "01" Then

    '                            Dim dsJasaDokter As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaPerawat As New DataAccess.R_DATA_HASIL

    '                            Dim JasaDPJP As Decimal = 50000
    '                            Dim JasaPerawat As Decimal = 10000

    '                            Dim dsJasaAdministrasi As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaApoteker As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaInputBilling As New DataAccess.R_DATA_HASIL
    '                            Dim JasaAdministrasi As Decimal = 5000
    '                            Dim JasaApoteker As Decimal = 1500
    '                            Dim JasaInputBilling As Decimal = 500

    '                            Dim Total As Decimal = JasaDPJP + JasaPerawat + JasaAdministrasi + JasaApoteker + JasaInputBilling

    '                            'Admisitrasi %
    '                            dsJasaAdministrasi.CEK = xloop.CEK
    '                            dsJasaAdministrasi.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaAdministrasi.KET = "BAGI ADMINISTRASI"
    '                            dsJasaAdministrasi.TUJUAN = xloop.TUJUAN
    '                            dsJasaAdministrasi.NOSEP = xloop.NOSEP
    '                            dsJasaAdministrasi.RUGI = xloop.RUGI
    '                            dsJasaAdministrasi.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaAdministrasi.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaAdministrasi.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaAdministrasi.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaAdministrasi.NAMA = xloop.NAMA
    '                            dsJasaAdministrasi.NAME_DISPLAY = "Administrasi"
    '                            dsJasaAdministrasi.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaAdministrasi.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaAdministrasi.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaAdministrasi.BAGIJASA = Sisa * (JasaAdministrasi / Total)
    '                                dsJasaAdministrasi.SISAPAKET = 0
    '                            Else
    '                                dsJasaAdministrasi.BAGIJASA = JasaAdministrasi
    '                                dsJasaAdministrasi.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaAdministrasi)

    '                            'Apoteker %
    '                            dsJasaApoteker.CEK = xloop.CEK
    '                            dsJasaApoteker.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaApoteker.KET = "BAGI APOTEKER"
    '                            dsJasaApoteker.TUJUAN = xloop.TUJUAN
    '                            dsJasaApoteker.NOSEP = xloop.NOSEP
    '                            dsJasaApoteker.RUGI = xloop.RUGI
    '                            dsJasaApoteker.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaApoteker.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaApoteker.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaApoteker.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaApoteker.NAMA = xloop.NAMA
    '                            dsJasaApoteker.NAME_DISPLAY = "Apoteker"
    '                            dsJasaApoteker.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaApoteker.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaApoteker.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaApoteker.BAGIJASA = Sisa * (JasaApoteker / Total)
    '                                dsJasaApoteker.SISAPAKET = 0
    '                            Else
    '                                dsJasaApoteker.BAGIJASA = JasaApoteker
    '                                dsJasaApoteker.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaApoteker)

    '                            'Input billing %
    '                            dsJasaInputBilling.CEK = xloop.CEK
    '                            dsJasaInputBilling.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaInputBilling.KET = "BAGI INPUT BILLING"
    '                            dsJasaInputBilling.TUJUAN = xloop.TUJUAN
    '                            dsJasaInputBilling.NOSEP = xloop.NOSEP
    '                            dsJasaInputBilling.RUGI = xloop.RUGI
    '                            dsJasaInputBilling.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaInputBilling.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaInputBilling.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaInputBilling.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaInputBilling.NAMA = xloop.NAMA
    '                            dsJasaInputBilling.NAME_DISPLAY = "Input Billing"
    '                            dsJasaInputBilling.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaInputBilling.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaInputBilling.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaInputBilling.BAGIJASA = Sisa * (JasaInputBilling / Total)
    '                                dsJasaInputBilling.SISAPAKET = 0
    '                            Else
    '                                dsJasaInputBilling.BAGIJASA = JasaInputBilling
    '                                dsJasaInputBilling.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaInputBilling)

    '                            'dpjp %
    '                            dsJasaDokter.CEK = xloop.CEK
    '                            dsJasaDokter.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaDokter.KET = "BAGI DPJP"
    '                            dsJasaDokter.TUJUAN = xloop.TUJUAN
    '                            dsJasaDokter.NOSEP = xloop.NOSEP
    '                            dsJasaDokter.RUGI = xloop.RUGI
    '                            dsJasaDokter.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaDokter.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaDokter.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaDokter.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaDokter.NAMA = xloop.NAMA
    '                            dsJasaDokter.NAME_DISPLAY = xloop.DOKTER_DPJP
    '                            dsJasaDokter.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaDokter.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaDokter.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaDokter.BAGIJASA = Sisa * (JasaDPJP / Total)
    '                                dsJasaDokter.SISAPAKET = 0
    '                            Else
    '                                dsJasaDokter.BAGIJASA = JasaDPJP
    '                                dsJasaDokter.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaDokter)

    '                            'perawat %
    '                            dsJasaPerawat.CEK = xloop.CEK
    '                            dsJasaPerawat.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaPerawat.KET = "BAGI PERAWAT"
    '                            dsJasaPerawat.TUJUAN = xloop.TUJUAN
    '                            dsJasaPerawat.NOSEP = xloop.NOSEP
    '                            dsJasaPerawat.RUGI = xloop.RUGI
    '                            dsJasaPerawat.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaPerawat.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaPerawat.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaPerawat.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaPerawat.NAMA = xloop.NAMA
    '                            dsJasaPerawat.NAME_DISPLAY = xloop.TUJUAN
    '                            dsJasaPerawat.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaPerawat.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaPerawat.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaPerawat.BAGIJASA = Sisa * (JasaPerawat / Total)
    '                                dsJasaPerawat.SISAPAKET = 0
    '                            Else
    '                                dsJasaPerawat.BAGIJASA = JasaPerawat
    '                                dsJasaPerawat.SISAPAKET = xloop.TOTAL_TARIF - (SisaPaket + Total)
    '                            End If

    '                            listHasil.Add(dsJasaPerawat)

    '                        ElseIf xloop.CEK = "02" Then
    '                            Dim dsJasaDokterDPJP As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaDokterUMUM As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaPerawat1 As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaPerawat2 As New DataAccess.R_DATA_HASIL

    '                            Dim JasaDOKTERDPJP As Decimal = 25000
    '                            Dim JasaDOKTERUMUM As Decimal = 25000
    '                            Dim JasaPerawat1 As Decimal = 5000
    '                            Dim JasaPerawat2 As Decimal = 5000
    '                            Dim dsJasaAdministrasi As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaApoteker As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaInputBilling As New DataAccess.R_DATA_HASIL
    '                            Dim JasaAdministrasi As Decimal = 5000
    '                            Dim JasaApoteker As Decimal = 1500
    '                            Dim JasaInputBilling As Decimal = 500

    '                            Dim Total As Decimal = JasaDOKTERDPJP + JasaDOKTERUMUM + JasaPerawat1 + JasaPerawat2 + JasaAdministrasi + JasaApoteker + JasaInputBilling

    '                            'Admisitrasi %
    '                            dsJasaAdministrasi.CEK = xloop.CEK
    '                            dsJasaAdministrasi.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaAdministrasi.KET = "BAGI ADMINISTRASI"
    '                            dsJasaAdministrasi.TUJUAN = xloop.TUJUAN
    '                            dsJasaAdministrasi.NOSEP = xloop.NOSEP
    '                            dsJasaAdministrasi.RUGI = xloop.RUGI
    '                            dsJasaAdministrasi.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaAdministrasi.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaAdministrasi.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaAdministrasi.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaAdministrasi.NAMA = xloop.NAMA
    '                            dsJasaAdministrasi.NAME_DISPLAY = "Administrasi"
    '                            dsJasaAdministrasi.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaAdministrasi.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaAdministrasi.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaAdministrasi.BAGIJASA = Sisa * (JasaAdministrasi / Total)
    '                                dsJasaAdministrasi.SISAPAKET = 0
    '                            Else
    '                                dsJasaAdministrasi.BAGIJASA = JasaAdministrasi
    '                                dsJasaAdministrasi.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaAdministrasi)

    '                            'Apoteker %
    '                            dsJasaApoteker.CEK = xloop.CEK
    '                            dsJasaApoteker.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaApoteker.KET = "BAGI APOTEKER"
    '                            dsJasaApoteker.TUJUAN = xloop.TUJUAN
    '                            dsJasaApoteker.NOSEP = xloop.NOSEP
    '                            dsJasaApoteker.RUGI = xloop.RUGI
    '                            dsJasaApoteker.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaApoteker.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaApoteker.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaApoteker.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaApoteker.NAMA = xloop.NAMA
    '                            dsJasaApoteker.NAME_DISPLAY = "Apoteker"
    '                            dsJasaApoteker.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaApoteker.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaApoteker.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaApoteker.BAGIJASA = Sisa * (JasaApoteker / Total)
    '                                dsJasaApoteker.SISAPAKET = 0
    '                            Else
    '                                dsJasaApoteker.BAGIJASA = JasaApoteker
    '                                dsJasaApoteker.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaApoteker)

    '                            'Input billing %
    '                            dsJasaInputBilling.CEK = xloop.CEK
    '                            dsJasaInputBilling.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaInputBilling.KET = "BAGI INPUT BILLING"
    '                            dsJasaInputBilling.TUJUAN = xloop.TUJUAN
    '                            dsJasaInputBilling.NOSEP = xloop.NOSEP
    '                            dsJasaInputBilling.RUGI = xloop.RUGI
    '                            dsJasaInputBilling.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaInputBilling.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaInputBilling.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaInputBilling.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaInputBilling.NAMA = xloop.NAMA
    '                            dsJasaInputBilling.NAME_DISPLAY = "Input Billing"
    '                            dsJasaInputBilling.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaInputBilling.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaInputBilling.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaInputBilling.BAGIJASA = Sisa * (JasaInputBilling / Total)
    '                                dsJasaInputBilling.SISAPAKET = 0
    '                            Else
    '                                dsJasaInputBilling.BAGIJASA = JasaInputBilling
    '                                dsJasaInputBilling.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaInputBilling)

    '                            'dpjp %
    '                            dsJasaDokterDPJP.CEK = xloop.CEK
    '                            dsJasaDokterDPJP.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaDokterDPJP.KET = "BAGI DPJP1"
    '                            dsJasaDokterDPJP.TUJUAN = xloop.TUJUAN
    '                            dsJasaDokterDPJP.NOSEP = xloop.NOSEP
    '                            dsJasaDokterDPJP.RUGI = xloop.RUGI
    '                            dsJasaDokterDPJP.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaDokterDPJP.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaDokterDPJP.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaDokterDPJP.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaDokterDPJP.NAMA = xloop.NAMA
    '                            dsJasaDokterDPJP.NAME_DISPLAY = xloop.DOKTER_DPJP
    '                            dsJasaDokterDPJP.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaDokterDPJP.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaDokterDPJP.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaDokterDPJP.BAGIJASA = Sisa * (JasaDOKTERDPJP / Total)
    '                                dsJasaDokterDPJP.SISAPAKET = 0
    '                            Else
    '                                dsJasaDokterDPJP.BAGIJASA = JasaDOKTERDPJP
    '                                dsJasaDokterDPJP.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaDokterDPJP)

    '                            'UMUM %
    '                            dsJasaDokterUMUM.CEK = xloop.CEK
    '                            dsJasaDokterUMUM.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaDokterUMUM.KET = "BAGI DPJP2"
    '                            dsJasaDokterUMUM.TUJUAN = xloop.TUJUAN
    '                            dsJasaDokterUMUM.NOSEP = xloop.NOSEP
    '                            dsJasaDokterUMUM.RUGI = xloop.RUGI
    '                            dsJasaDokterUMUM.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaDokterUMUM.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaDokterUMUM.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaDokterUMUM.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaDokterUMUM.NAMA = xloop.NAMA
    '                            dsJasaDokterUMUM.NAME_DISPLAY = xloop.DOKTER
    '                            dsJasaDokterUMUM.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaDokterUMUM.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaDokterUMUM.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaDokterUMUM.BAGIJASA = Sisa * (JasaDOKTERUMUM / Total)
    '                                dsJasaDokterUMUM.SISAPAKET = 0
    '                            Else
    '                                dsJasaDokterUMUM.BAGIJASA = JasaDOKTERUMUM
    '                                dsJasaDokterUMUM.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaDokterUMUM)


    '                            'perawat %
    '                            dsJasaPerawat1.CEK = xloop.CEK
    '                            dsJasaPerawat1.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaPerawat1.KET = "BAGI PERAWAT1"
    '                            dsJasaPerawat1.TUJUAN = xloop.TUJUAN
    '                            dsJasaPerawat1.NOSEP = xloop.NOSEP
    '                            dsJasaPerawat1.RUGI = xloop.RUGI
    '                            dsJasaPerawat1.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaPerawat1.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaPerawat1.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaPerawat1.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaPerawat1.NAMA = xloop.NAMA
    '                            dsJasaPerawat1.NAME_DISPLAY = xloop.TUJUAN & "PERUJUK"
    '                            dsJasaPerawat1.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaPerawat1.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaPerawat1.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaPerawat1.BAGIJASA = Sisa * (JasaPerawat1 / Total)
    '                                dsJasaPerawat1.SISAPAKET = 0
    '                            Else
    '                                dsJasaPerawat1.BAGIJASA = JasaPerawat1
    '                                dsJasaPerawat1.SISAPAKET = xloop.TOTAL_TARIF - (SisaPaket + Total)
    '                            End If

    '                            listHasil.Add(dsJasaPerawat1)

    '                            'perawat %
    '                            dsJasaPerawat2.CEK = xloop.CEK
    '                            dsJasaPerawat2.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaPerawat2.KET = "BAGI PERAWAT2"
    '                            dsJasaPerawat2.TUJUAN = xloop.TUJUAN
    '                            dsJasaPerawat2.NOSEP = xloop.NOSEP
    '                            dsJasaPerawat2.RUGI = xloop.RUGI
    '                            dsJasaPerawat2.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaPerawat2.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaPerawat2.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaPerawat2.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaPerawat2.NAMA = xloop.NAMA
    '                            dsJasaPerawat2.NAME_DISPLAY = xloop.TUJUAN & " PELAKSANA"
    '                            dsJasaPerawat2.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaPerawat2.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaPerawat2.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaPerawat2.BAGIJASA = Sisa * (JasaPerawat2 / Total)
    '                                dsJasaPerawat2.SISAPAKET = 0
    '                            Else
    '                                dsJasaPerawat2.BAGIJASA = JasaPerawat2
    '                                dsJasaPerawat2.SISAPAKET = xloop.TOTAL_TARIF - (SisaPaket + Total)
    '                            End If

    '                            listHasil.Add(dsJasaPerawat2)

    '                        ElseIf xloop.CEK = "03" Then
    '                            Dim dsJasaDokterDPJP As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaDokterUMUM As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaPerawat As New DataAccess.R_DATA_HASIL

    '                            Dim JasaDOKTERDPJP As Decimal = 20000
    '                            Dim JasaDOKTERUMUM As Decimal = 20000
    '                            Dim JasaPerawat As Decimal = 10000


    '                            Dim dsJasaAdministrasi As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaApoteker As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaInputBilling As New DataAccess.R_DATA_HASIL
    '                            Dim JasaAdministrasi As Decimal = 5000
    '                            Dim JasaApoteker As Decimal = 1500
    '                            Dim JasaInputBilling As Decimal = 500

    '                            Dim Total As Decimal = JasaDOKTERDPJP + JasaDOKTERUMUM + JasaPerawat + JasaAdministrasi + JasaApoteker + JasaInputBilling

    '                            'Admisitrasi %
    '                            dsJasaAdministrasi.CEK = xloop.CEK
    '                            dsJasaAdministrasi.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaAdministrasi.KET = "BAGI ADMINISTRASI"
    '                            dsJasaAdministrasi.TUJUAN = xloop.TUJUAN
    '                            dsJasaAdministrasi.NOSEP = xloop.NOSEP
    '                            dsJasaAdministrasi.RUGI = xloop.RUGI
    '                            dsJasaAdministrasi.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaAdministrasi.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaAdministrasi.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaAdministrasi.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaAdministrasi.NAMA = xloop.NAMA
    '                            dsJasaAdministrasi.NAME_DISPLAY = "Administrasi"
    '                            dsJasaAdministrasi.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaAdministrasi.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaAdministrasi.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaAdministrasi.BAGIJASA = Sisa * (JasaAdministrasi / Total)
    '                                dsJasaAdministrasi.SISAPAKET = 0
    '                            Else
    '                                dsJasaAdministrasi.BAGIJASA = JasaAdministrasi
    '                                dsJasaAdministrasi.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaAdministrasi)

    '                            'Apoteker %
    '                            dsJasaApoteker.CEK = xloop.CEK
    '                            dsJasaApoteker.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaApoteker.KET = "BAGI APOTEKER"
    '                            dsJasaApoteker.TUJUAN = xloop.TUJUAN
    '                            dsJasaApoteker.NOSEP = xloop.NOSEP
    '                            dsJasaApoteker.RUGI = xloop.RUGI
    '                            dsJasaApoteker.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaApoteker.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaApoteker.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaApoteker.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaApoteker.NAMA = xloop.NAMA
    '                            dsJasaApoteker.NAME_DISPLAY = "Apoteker"
    '                            dsJasaApoteker.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaApoteker.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaApoteker.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaApoteker.BAGIJASA = Sisa * (JasaApoteker / Total)
    '                                dsJasaApoteker.SISAPAKET = 0
    '                            Else
    '                                dsJasaApoteker.BAGIJASA = JasaApoteker
    '                                dsJasaApoteker.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaApoteker)

    '                            'Input billing %
    '                            dsJasaInputBilling.CEK = xloop.CEK
    '                            dsJasaInputBilling.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaInputBilling.KET = "BAGI INPUT BILLING"
    '                            dsJasaInputBilling.TUJUAN = xloop.TUJUAN
    '                            dsJasaInputBilling.NOSEP = xloop.NOSEP
    '                            dsJasaInputBilling.RUGI = xloop.RUGI
    '                            dsJasaInputBilling.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaInputBilling.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaInputBilling.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaInputBilling.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaInputBilling.NAMA = xloop.NAMA
    '                            dsJasaInputBilling.NAME_DISPLAY = "Input Billing"
    '                            dsJasaInputBilling.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaInputBilling.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaInputBilling.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaInputBilling.BAGIJASA = Sisa * (JasaInputBilling / Total)
    '                                dsJasaInputBilling.SISAPAKET = 0
    '                            Else
    '                                dsJasaInputBilling.BAGIJASA = JasaInputBilling
    '                                dsJasaInputBilling.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaInputBilling)

    '                            'dpjp %
    '                            dsJasaDokterDPJP.CEK = xloop.CEK
    '                            dsJasaDokterDPJP.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaDokterDPJP.KET = "BAGI DPJP"
    '                            dsJasaDokterDPJP.TUJUAN = xloop.TUJUAN
    '                            dsJasaDokterDPJP.NOSEP = xloop.NOSEP
    '                            dsJasaDokterDPJP.RUGI = xloop.RUGI
    '                            dsJasaDokterDPJP.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaDokterDPJP.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaDokterDPJP.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaDokterDPJP.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaDokterDPJP.NAMA = xloop.NAMA
    '                            dsJasaDokterDPJP.NAME_DISPLAY = xloop.DOKTER_DPJP
    '                            dsJasaDokterDPJP.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaDokterDPJP.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaDokterDPJP.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaDokterDPJP.BAGIJASA = Sisa * (JasaDOKTERDPJP / Total)
    '                                dsJasaDokterDPJP.SISAPAKET = 0
    '                            Else
    '                                dsJasaDokterDPJP.BAGIJASA = JasaDOKTERDPJP
    '                                dsJasaDokterDPJP.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaDokterDPJP)

    '                            'UMUM
    '                            dsJasaDokterUMUM.CEK = xloop.CEK
    '                            dsJasaDokterUMUM.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaDokterUMUM.KET = "BAGI DELAGASI"
    '                            dsJasaDokterUMUM.TUJUAN = xloop.TUJUAN
    '                            dsJasaDokterUMUM.NOSEP = xloop.NOSEP
    '                            dsJasaDokterUMUM.RUGI = xloop.RUGI
    '                            dsJasaDokterUMUM.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaDokterUMUM.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaDokterUMUM.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaDokterUMUM.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaDokterUMUM.NAMA = xloop.NAMA
    '                            dsJasaDokterUMUM.NAME_DISPLAY = xloop.DOKTER
    '                            dsJasaDokterUMUM.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaDokterUMUM.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaDokterUMUM.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaDokterUMUM.BAGIJASA = Sisa * (JasaDOKTERUMUM / Total)
    '                                dsJasaDokterUMUM.SISAPAKET = 0
    '                            Else
    '                                dsJasaDokterUMUM.BAGIJASA = JasaDOKTERUMUM
    '                                dsJasaDokterUMUM.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaDokterUMUM)

    '                            'perawat %
    '                            dsJasaPerawat.CEK = xloop.CEK
    '                            dsJasaPerawat.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaPerawat.KET = "BAGI PERAWAT"
    '                            dsJasaPerawat.TUJUAN = xloop.TUJUAN
    '                            dsJasaPerawat.NOSEP = xloop.NOSEP
    '                            dsJasaPerawat.RUGI = xloop.RUGI
    '                            dsJasaPerawat.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaPerawat.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaPerawat.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaPerawat.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaPerawat.NAMA = xloop.NAMA
    '                            dsJasaPerawat.NAME_DISPLAY = xloop.TUJUAN
    '                            dsJasaPerawat.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaPerawat.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaPerawat.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaPerawat.BAGIJASA = Sisa * (JasaPerawat / Total)
    '                                dsJasaPerawat.SISAPAKET = 0
    '                            Else
    '                                dsJasaPerawat.BAGIJASA = JasaPerawat
    '                                dsJasaPerawat.SISAPAKET = xloop.TOTAL_TARIF - (SisaPaket + Total)
    '                            End If

    '                            listHasil.Add(dsJasaPerawat)

    '                        ElseIf xloop.CEK = "04" Then
    '                            Dim dsJasaDokterDPJP As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaDokterUMUM As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaKonseling As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaPerawat As New DataAccess.R_DATA_HASIL

    '                            Dim JasaDOKTERDPJP As Decimal = 20000
    '                            Dim JasaDOKTERUMUM As Decimal = 20000
    '                            Dim JasaKonseling As Decimal = 10000
    '                            Dim JasaPerawat As Decimal = 10000

    '                            Dim dsJasaAdministrasi As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaApoteker As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaInputBilling As New DataAccess.R_DATA_HASIL
    '                            Dim JasaAdministrasi As Decimal = 5000
    '                            Dim JasaApoteker As Decimal = 1500
    '                            Dim JasaInputBilling As Decimal = 500

    '                            Dim Total As Decimal = JasaDOKTERDPJP + JasaDOKTERUMUM + JasaPerawat + JasaKonseling + JasaAdministrasi + JasaApoteker + JasaInputBilling

    '                            'Admisitrasi %
    '                            dsJasaAdministrasi.CEK = xloop.CEK
    '                            dsJasaAdministrasi.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaAdministrasi.KET = "BAGI ADMINISTRASI"
    '                            dsJasaAdministrasi.TUJUAN = xloop.TUJUAN
    '                            dsJasaAdministrasi.NOSEP = xloop.NOSEP
    '                            dsJasaAdministrasi.RUGI = xloop.RUGI
    '                            dsJasaAdministrasi.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaAdministrasi.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaAdministrasi.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaAdministrasi.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaAdministrasi.NAMA = xloop.NAMA
    '                            dsJasaAdministrasi.NAME_DISPLAY = "Administrasi"
    '                            dsJasaAdministrasi.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaAdministrasi.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaAdministrasi.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaAdministrasi.BAGIJASA = Sisa * (JasaAdministrasi / Total)
    '                                dsJasaAdministrasi.SISAPAKET = 0
    '                            Else
    '                                dsJasaAdministrasi.BAGIJASA = JasaAdministrasi
    '                                dsJasaAdministrasi.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaAdministrasi)

    '                            'Apoteker %
    '                            dsJasaApoteker.CEK = xloop.CEK
    '                            dsJasaApoteker.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaApoteker.KET = "BAGI APOTEKER"
    '                            dsJasaApoteker.TUJUAN = xloop.TUJUAN
    '                            dsJasaApoteker.NOSEP = xloop.NOSEP
    '                            dsJasaApoteker.RUGI = xloop.RUGI
    '                            dsJasaApoteker.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaApoteker.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaApoteker.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaApoteker.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaApoteker.NAMA = xloop.NAMA
    '                            dsJasaApoteker.NAME_DISPLAY = "Apoteker"
    '                            dsJasaApoteker.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaApoteker.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaApoteker.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaApoteker.BAGIJASA = Sisa * (JasaApoteker / Total)
    '                                dsJasaApoteker.SISAPAKET = 0
    '                            Else
    '                                dsJasaApoteker.BAGIJASA = JasaApoteker
    '                                dsJasaApoteker.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaApoteker)

    '                            'Input billing %
    '                            dsJasaInputBilling.CEK = xloop.CEK
    '                            dsJasaInputBilling.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaInputBilling.KET = "BAGI INPUT BILLING"
    '                            dsJasaInputBilling.TUJUAN = xloop.TUJUAN
    '                            dsJasaInputBilling.NOSEP = xloop.NOSEP
    '                            dsJasaInputBilling.RUGI = xloop.RUGI
    '                            dsJasaInputBilling.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaInputBilling.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaInputBilling.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaInputBilling.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaInputBilling.NAMA = xloop.NAMA
    '                            dsJasaInputBilling.NAME_DISPLAY = "Input Billing"
    '                            dsJasaInputBilling.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaInputBilling.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaInputBilling.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaInputBilling.BAGIJASA = Sisa * (JasaInputBilling / Total)
    '                                dsJasaInputBilling.SISAPAKET = 0
    '                            Else
    '                                dsJasaInputBilling.BAGIJASA = JasaInputBilling
    '                                dsJasaInputBilling.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaInputBilling)


    '                            'dpjp %
    '                            dsJasaDokterDPJP.CEK = xloop.CEK
    '                            dsJasaDokterDPJP.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaDokterDPJP.KET = "BAGI DPJP"
    '                            dsJasaDokterDPJP.TUJUAN = xloop.TUJUAN
    '                            dsJasaDokterDPJP.NOSEP = xloop.NOSEP
    '                            dsJasaDokterDPJP.RUGI = xloop.RUGI
    '                            dsJasaDokterDPJP.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaDokterDPJP.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaDokterDPJP.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaDokterDPJP.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaDokterDPJP.NAMA = xloop.NAMA
    '                            dsJasaDokterDPJP.NAME_DISPLAY = xloop.DOKTER_DPJP
    '                            dsJasaDokterDPJP.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaDokterDPJP.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaDokterDPJP.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaDokterDPJP.BAGIJASA = Sisa * (JasaDOKTERDPJP / Total)
    '                                dsJasaDokterDPJP.SISAPAKET = 0
    '                            Else
    '                                dsJasaDokterDPJP.BAGIJASA = JasaDOKTERDPJP
    '                                dsJasaDokterDPJP.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaDokterDPJP)

    '                            'UMUM
    '                            dsJasaDokterUMUM.CEK = xloop.CEK
    '                            dsJasaDokterUMUM.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaDokterUMUM.KET = "BAGI DELAGASI"
    '                            dsJasaDokterUMUM.TUJUAN = xloop.TUJUAN
    '                            dsJasaDokterUMUM.NOSEP = xloop.NOSEP
    '                            dsJasaDokterUMUM.RUGI = xloop.RUGI
    '                            dsJasaDokterUMUM.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaDokterUMUM.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaDokterUMUM.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaDokterUMUM.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaDokterUMUM.NAMA = xloop.NAMA
    '                            dsJasaDokterUMUM.NAME_DISPLAY = xloop.DOKTER
    '                            dsJasaDokterUMUM.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaDokterUMUM.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaDokterUMUM.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaDokterUMUM.BAGIJASA = Sisa * (JasaDOKTERUMUM / Total)
    '                                dsJasaDokterUMUM.SISAPAKET = 0
    '                            Else
    '                                dsJasaDokterUMUM.BAGIJASA = JasaDOKTERUMUM
    '                                dsJasaDokterUMUM.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaDokterUMUM)

    '                            'KONSELING
    '                            dsJasaKonseling.CEK = xloop.CEK
    '                            dsJasaKonseling.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaKonseling.KET = "BAGI KONSELING"
    '                            dsJasaKonseling.TUJUAN = xloop.TUJUAN
    '                            dsJasaKonseling.NOSEP = xloop.NOSEP
    '                            dsJasaKonseling.RUGI = xloop.RUGI
    '                            dsJasaKonseling.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaKonseling.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaKonseling.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaKonseling.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaKonseling.NAMA = xloop.NAMA
    '                            dsJasaKonseling.NAME_DISPLAY = "KONSELING"
    '                            dsJasaKonseling.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaKonseling.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaKonseling.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaKonseling.BAGIJASA = Sisa * (JasaKonseling / Total)
    '                                dsJasaKonseling.SISAPAKET = 0
    '                            Else
    '                                dsJasaKonseling.BAGIJASA = JasaKonseling
    '                                dsJasaKonseling.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaKonseling)

    '                            'perawat %
    '                            dsJasaPerawat.CEK = xloop.CEK
    '                            dsJasaPerawat.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaPerawat.KET = "BAGI PERAWAT"
    '                            dsJasaPerawat.TUJUAN = xloop.TUJUAN
    '                            dsJasaPerawat.NOSEP = xloop.NOSEP
    '                            dsJasaPerawat.RUGI = xloop.RUGI
    '                            dsJasaPerawat.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaPerawat.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaPerawat.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaPerawat.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaPerawat.NAMA = xloop.NAMA
    '                            dsJasaPerawat.NAME_DISPLAY = xloop.TUJUAN
    '                            dsJasaPerawat.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaPerawat.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaPerawat.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaPerawat.BAGIJASA = Sisa * (JasaPerawat / Total)
    '                                dsJasaPerawat.SISAPAKET = 0
    '                            Else
    '                                dsJasaPerawat.BAGIJASA = JasaPerawat
    '                                dsJasaPerawat.SISAPAKET = xloop.TOTAL_TARIF - (SisaPaket + Total)
    '                            End If

    '                            listHasil.Add(dsJasaPerawat)

    '                        ElseIf xloop.CEK = "05" Then
    '                            Dim dsJasaDokterDPJP As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaDokterLAB As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaAnalis As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaPerawat As New DataAccess.R_DATA_HASIL

    '                            Dim JasaDOKTERDPJP As Decimal = 30000
    '                            Dim JasaDOKTERLAB As Decimal = 10000
    '                            Dim JasaLab As Decimal = 5000
    '                            Dim JasaPerawat As Decimal = 5000

    '                            Dim dsJasaAdministrasi As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaApoteker As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaInputBilling As New DataAccess.R_DATA_HASIL
    '                            Dim JasaAdministrasi As Decimal = 5000
    '                            Dim JasaApoteker As Decimal = 1500
    '                            Dim JasaInputBilling As Decimal = 500

    '                            Dim Total As Decimal = JasaDOKTERDPJP + JasaDOKTERLAB + JasaPerawat + JasaLab + JasaAdministrasi + JasaApoteker + JasaInputBilling

    '                            'Admisitrasi %
    '                            dsJasaAdministrasi.CEK = xloop.CEK
    '                            dsJasaAdministrasi.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaAdministrasi.KET = "BAGI ADMINISTRASI"
    '                            dsJasaAdministrasi.TUJUAN = xloop.TUJUAN
    '                            dsJasaAdministrasi.NOSEP = xloop.NOSEP
    '                            dsJasaAdministrasi.RUGI = xloop.RUGI
    '                            dsJasaAdministrasi.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaAdministrasi.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaAdministrasi.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaAdministrasi.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaAdministrasi.NAMA = xloop.NAMA
    '                            dsJasaAdministrasi.NAME_DISPLAY = "Administrasi"
    '                            dsJasaAdministrasi.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaAdministrasi.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaAdministrasi.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaAdministrasi.BAGIJASA = Sisa * (JasaAdministrasi / Total)
    '                                dsJasaAdministrasi.SISAPAKET = 0
    '                            Else
    '                                dsJasaAdministrasi.BAGIJASA = JasaAdministrasi
    '                                dsJasaAdministrasi.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaAdministrasi)

    '                            'Apoteker %
    '                            dsJasaApoteker.CEK = xloop.CEK
    '                            dsJasaApoteker.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaApoteker.KET = "BAGI APOTEKER"
    '                            dsJasaApoteker.TUJUAN = xloop.TUJUAN
    '                            dsJasaApoteker.NOSEP = xloop.NOSEP
    '                            dsJasaApoteker.RUGI = xloop.RUGI
    '                            dsJasaApoteker.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaApoteker.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaApoteker.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaApoteker.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaApoteker.NAMA = xloop.NAMA
    '                            dsJasaApoteker.NAME_DISPLAY = "Apoteker"
    '                            dsJasaApoteker.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaApoteker.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaApoteker.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaApoteker.BAGIJASA = Sisa * (JasaApoteker / Total)
    '                                dsJasaApoteker.SISAPAKET = 0
    '                            Else
    '                                dsJasaApoteker.BAGIJASA = JasaApoteker
    '                                dsJasaApoteker.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaApoteker)

    '                            'Input billing %
    '                            dsJasaInputBilling.CEK = xloop.CEK
    '                            dsJasaInputBilling.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaInputBilling.KET = "BAGI INPUT BILLING"
    '                            dsJasaInputBilling.TUJUAN = xloop.TUJUAN
    '                            dsJasaInputBilling.NOSEP = xloop.NOSEP
    '                            dsJasaInputBilling.RUGI = xloop.RUGI
    '                            dsJasaInputBilling.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaInputBilling.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaInputBilling.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaInputBilling.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaInputBilling.NAMA = xloop.NAMA
    '                            dsJasaInputBilling.NAME_DISPLAY = "Input Billing"
    '                            dsJasaInputBilling.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaInputBilling.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaInputBilling.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaInputBilling.BAGIJASA = Sisa * (JasaInputBilling / Total)
    '                                dsJasaInputBilling.SISAPAKET = 0
    '                            Else
    '                                dsJasaInputBilling.BAGIJASA = JasaInputBilling
    '                                dsJasaInputBilling.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaInputBilling)

    '                            'dpjp %
    '                            dsJasaDokterDPJP.CEK = xloop.CEK
    '                            dsJasaDokterDPJP.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaDokterDPJP.KET = "BAGI DPJP"
    '                            dsJasaDokterDPJP.TUJUAN = xloop.TUJUAN
    '                            dsJasaDokterDPJP.NOSEP = xloop.NOSEP
    '                            dsJasaDokterDPJP.RUGI = xloop.RUGI
    '                            dsJasaDokterDPJP.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaDokterDPJP.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaDokterDPJP.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaDokterDPJP.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaDokterDPJP.NAMA = xloop.NAMA
    '                            dsJasaDokterDPJP.NAME_DISPLAY = xloop.DOKTER_DPJP
    '                            dsJasaDokterDPJP.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaDokterDPJP.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaDokterDPJP.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaDokterDPJP.BAGIJASA = Sisa * (JasaDOKTERDPJP / Total)
    '                                dsJasaDokterDPJP.SISAPAKET = 0
    '                            Else
    '                                dsJasaDokterDPJP.BAGIJASA = JasaDOKTERDPJP
    '                                dsJasaDokterDPJP.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaDokterDPJP)

    '                            'UMUM
    '                            dsJasaDokterLAB.CEK = xloop.CEK
    '                            dsJasaDokterLAB.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaDokterLAB.KET = "BAGI LABORATORIUM"
    '                            dsJasaDokterLAB.TUJUAN = xloop.TUJUAN
    '                            dsJasaDokterLAB.NOSEP = xloop.NOSEP
    '                            dsJasaDokterLAB.RUGI = xloop.RUGI
    '                            dsJasaDokterLAB.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaDokterLAB.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaDokterLAB.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaDokterLAB.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaDokterLAB.NAMA = xloop.NAMA
    '                            dsJasaDokterLAB.NAME_DISPLAY = "dr. Laboratorium"
    '                            dsJasaDokterLAB.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaDokterLAB.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaDokterLAB.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaDokterLAB.BAGIJASA = Sisa * (JasaDOKTERLAB / Total)
    '                                dsJasaDokterLAB.SISAPAKET = 0
    '                            Else
    '                                dsJasaDokterLAB.BAGIJASA = JasaDOKTERLAB
    '                                dsJasaDokterLAB.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaDokterLAB)

    '                            'LAB
    '                            dsJasaAnalis.CEK = xloop.CEK
    '                            dsJasaAnalis.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaAnalis.KET = "BAGI LABORATORIUM"
    '                            dsJasaAnalis.TUJUAN = xloop.TUJUAN
    '                            dsJasaAnalis.NOSEP = xloop.NOSEP
    '                            dsJasaAnalis.RUGI = xloop.RUGI
    '                            dsJasaAnalis.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaAnalis.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaAnalis.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaAnalis.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaAnalis.NAMA = xloop.NAMA
    '                            dsJasaAnalis.NAME_DISPLAY = "Analis"
    '                            dsJasaAnalis.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaAnalis.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaAnalis.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaAnalis.BAGIJASA = Sisa * (JasaLab / Total)
    '                                dsJasaAnalis.SISAPAKET = 0
    '                            Else
    '                                dsJasaAnalis.BAGIJASA = JasaLab
    '                                dsJasaAnalis.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaAnalis)

    '                            'perawat %
    '                            dsJasaPerawat.CEK = xloop.CEK
    '                            dsJasaPerawat.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaPerawat.KET = "BAGI PERAWAT"
    '                            dsJasaPerawat.TUJUAN = xloop.TUJUAN
    '                            dsJasaPerawat.NOSEP = xloop.NOSEP
    '                            dsJasaPerawat.RUGI = xloop.RUGI
    '                            dsJasaPerawat.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaPerawat.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaPerawat.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaPerawat.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaPerawat.NAMA = xloop.NAMA
    '                            dsJasaPerawat.NAME_DISPLAY = xloop.TUJUAN
    '                            dsJasaPerawat.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaPerawat.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaPerawat.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaPerawat.BAGIJASA = Sisa * (JasaPerawat / Total)
    '                                dsJasaPerawat.SISAPAKET = 0
    '                            Else
    '                                dsJasaPerawat.BAGIJASA = JasaPerawat
    '                                dsJasaPerawat.SISAPAKET = xloop.TOTAL_TARIF - (SisaPaket + Total)
    '                            End If

    '                            listHasil.Add(dsJasaPerawat)

    '                        ElseIf xloop.CEK = "06" Then
    '                            Dim dsJasaDokterDPJP As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaDokterLAB As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaAnalis As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaPerawat As New DataAccess.R_DATA_HASIL

    '                            Dim JasaDOKTERDPJP As Decimal = 30000
    '                            Dim JasaDOKTERLAB As Decimal = 10000
    '                            Dim JasaLab As Decimal = 5000
    '                            Dim JasaPerawat As Decimal = 5000


    '                            Dim dsJasaAdministrasi As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaApoteker As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaInputBilling As New DataAccess.R_DATA_HASIL
    '                            Dim JasaAdministrasi As Decimal = 5000
    '                            Dim JasaApoteker As Decimal = 1500
    '                            Dim JasaInputBilling As Decimal = 500

    '                            Dim Total As Decimal = JasaDOKTERDPJP + JasaDOKTERLAB + JasaPerawat + JasaLab + JasaAdministrasi + JasaApoteker + JasaInputBilling

    '                            'Admisitrasi %
    '                            dsJasaAdministrasi.CEK = xloop.CEK
    '                            dsJasaAdministrasi.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaAdministrasi.KET = "BAGI ADMINISTRASI"
    '                            dsJasaAdministrasi.TUJUAN = xloop.TUJUAN
    '                            dsJasaAdministrasi.NOSEP = xloop.NOSEP
    '                            dsJasaAdministrasi.RUGI = xloop.RUGI
    '                            dsJasaAdministrasi.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaAdministrasi.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaAdministrasi.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaAdministrasi.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaAdministrasi.NAMA = xloop.NAMA
    '                            dsJasaAdministrasi.NAME_DISPLAY = "Administrasi"
    '                            dsJasaAdministrasi.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaAdministrasi.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaAdministrasi.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaAdministrasi.BAGIJASA = Sisa * (JasaAdministrasi / Total)
    '                                dsJasaAdministrasi.SISAPAKET = 0
    '                            Else
    '                                dsJasaAdministrasi.BAGIJASA = JasaAdministrasi
    '                                dsJasaAdministrasi.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaAdministrasi)

    '                            'Apoteker %
    '                            dsJasaApoteker.CEK = xloop.CEK
    '                            dsJasaApoteker.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaApoteker.KET = "BAGI APOTEKER"
    '                            dsJasaApoteker.TUJUAN = xloop.TUJUAN
    '                            dsJasaApoteker.NOSEP = xloop.NOSEP
    '                            dsJasaApoteker.RUGI = xloop.RUGI
    '                            dsJasaApoteker.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaApoteker.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaApoteker.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaApoteker.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaApoteker.NAMA = xloop.NAMA
    '                            dsJasaApoteker.NAME_DISPLAY = "Apoteker"
    '                            dsJasaApoteker.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaApoteker.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaApoteker.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaApoteker.BAGIJASA = Sisa * (JasaApoteker / Total)
    '                                dsJasaApoteker.SISAPAKET = 0
    '                            Else
    '                                dsJasaApoteker.BAGIJASA = JasaApoteker
    '                                dsJasaApoteker.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaApoteker)

    '                            'Input billing %
    '                            dsJasaInputBilling.CEK = xloop.CEK
    '                            dsJasaInputBilling.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaInputBilling.KET = "BAGI INPUT BILLING"
    '                            dsJasaInputBilling.TUJUAN = xloop.TUJUAN
    '                            dsJasaInputBilling.NOSEP = xloop.NOSEP
    '                            dsJasaInputBilling.RUGI = xloop.RUGI
    '                            dsJasaInputBilling.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaInputBilling.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaInputBilling.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaInputBilling.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaInputBilling.NAMA = xloop.NAMA
    '                            dsJasaInputBilling.NAME_DISPLAY = "Input Billing"
    '                            dsJasaInputBilling.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaInputBilling.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaInputBilling.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaInputBilling.BAGIJASA = Sisa * (JasaInputBilling / Total)
    '                                dsJasaInputBilling.SISAPAKET = 0
    '                            Else
    '                                dsJasaInputBilling.BAGIJASA = JasaInputBilling
    '                                dsJasaInputBilling.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaInputBilling)

    '                            'dpjp %
    '                            dsJasaDokterDPJP.CEK = xloop.CEK
    '                            dsJasaDokterDPJP.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaDokterDPJP.KET = "BAGI DPJP"
    '                            dsJasaDokterDPJP.TUJUAN = xloop.TUJUAN
    '                            dsJasaDokterDPJP.NOSEP = xloop.NOSEP
    '                            dsJasaDokterDPJP.RUGI = xloop.RUGI
    '                            dsJasaDokterDPJP.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaDokterDPJP.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaDokterDPJP.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaDokterDPJP.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaDokterDPJP.NAMA = xloop.NAMA
    '                            dsJasaDokterDPJP.NAME_DISPLAY = xloop.DOKTER_DPJP
    '                            dsJasaDokterDPJP.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaDokterDPJP.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaDokterDPJP.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaDokterDPJP.BAGIJASA = Sisa * (JasaDOKTERDPJP / Total)
    '                                dsJasaDokterDPJP.SISAPAKET = 0
    '                            Else
    '                                dsJasaDokterDPJP.BAGIJASA = JasaDOKTERDPJP
    '                                dsJasaDokterDPJP.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaDokterDPJP)

    '                            'UMUM
    '                            dsJasaDokterLAB.CEK = xloop.CEK
    '                            dsJasaDokterLAB.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaDokterLAB.KET = "BAGI RADIOLOGI"
    '                            dsJasaDokterLAB.TUJUAN = xloop.TUJUAN
    '                            dsJasaDokterLAB.NOSEP = xloop.NOSEP
    '                            dsJasaDokterLAB.RUGI = xloop.RUGI
    '                            dsJasaDokterLAB.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaDokterLAB.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaDokterLAB.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaDokterLAB.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaDokterLAB.NAMA = xloop.NAMA
    '                            dsJasaDokterLAB.NAME_DISPLAY = "dr. Radiologi"
    '                            dsJasaDokterLAB.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaDokterLAB.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaDokterLAB.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaDokterLAB.BAGIJASA = Sisa * (JasaDOKTERLAB / Total)
    '                                dsJasaDokterLAB.SISAPAKET = 0
    '                            Else
    '                                dsJasaDokterLAB.BAGIJASA = JasaDOKTERLAB
    '                                dsJasaDokterLAB.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaDokterLAB)

    '                            'LAB
    '                            dsJasaAnalis.CEK = xloop.CEK
    '                            dsJasaAnalis.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaAnalis.KET = "BAGI RADIOLOGI"
    '                            dsJasaAnalis.TUJUAN = xloop.TUJUAN
    '                            dsJasaAnalis.NOSEP = xloop.NOSEP
    '                            dsJasaAnalis.RUGI = xloop.RUGI
    '                            dsJasaAnalis.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaAnalis.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaAnalis.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaAnalis.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaAnalis.NAMA = xloop.NAMA
    '                            dsJasaAnalis.NAME_DISPLAY = "Radiografer"
    '                            dsJasaAnalis.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaAnalis.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaAnalis.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaAnalis.BAGIJASA = Sisa * (JasaLab / Total)
    '                                dsJasaAnalis.SISAPAKET = 0
    '                            Else
    '                                dsJasaAnalis.BAGIJASA = JasaLab
    '                                dsJasaAnalis.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaAnalis)

    '                            'perawat %
    '                            dsJasaPerawat.CEK = xloop.CEK
    '                            dsJasaPerawat.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaPerawat.KET = "BAGI PERAWAT"
    '                            dsJasaPerawat.TUJUAN = xloop.TUJUAN
    '                            dsJasaPerawat.NOSEP = xloop.NOSEP
    '                            dsJasaPerawat.RUGI = xloop.RUGI
    '                            dsJasaPerawat.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaPerawat.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaPerawat.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaPerawat.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaPerawat.NAMA = xloop.NAMA
    '                            dsJasaPerawat.NAME_DISPLAY = xloop.TUJUAN
    '                            dsJasaPerawat.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaPerawat.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaPerawat.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaPerawat.BAGIJASA = Sisa * (JasaPerawat / Total)
    '                                dsJasaPerawat.SISAPAKET = 0
    '                            Else
    '                                dsJasaPerawat.BAGIJASA = JasaPerawat
    '                                dsJasaPerawat.SISAPAKET = xloop.TOTAL_TARIF - (SisaPaket + Total)
    '                            End If

    '                            listHasil.Add(dsJasaPerawat)

    '                        ElseIf xloop.CEK = "07" Then
    '                            Dim dsJasaDokterDPJP As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaDokterLAB As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaAnalis As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaPerawat As New DataAccess.R_DATA_HASIL

    '                            Dim JasaDOKTERDPJP As Decimal = 30000
    '                            Dim JasaDOKTERLAB As Decimal = 10000
    '                            Dim JasaLab As Decimal = 5000
    '                            Dim JasaPerawat As Decimal = 5000

    '                            Dim dsJasaAdministrasi As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaApoteker As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaInputBilling As New DataAccess.R_DATA_HASIL
    '                            Dim JasaAdministrasi As Decimal = 5000
    '                            Dim JasaApoteker As Decimal = 1500
    '                            Dim JasaInputBilling As Decimal = 500

    '                            Dim Total As Decimal = JasaDOKTERDPJP + JasaDOKTERLAB + JasaPerawat + JasaLab + JasaAdministrasi + JasaApoteker + JasaInputBilling

    '                            'Admisitrasi %
    '                            dsJasaAdministrasi.CEK = xloop.CEK
    '                            dsJasaAdministrasi.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaAdministrasi.KET = "BAGI ADMINISTRASI"
    '                            dsJasaAdministrasi.TUJUAN = xloop.TUJUAN
    '                            dsJasaAdministrasi.NOSEP = xloop.NOSEP
    '                            dsJasaAdministrasi.RUGI = xloop.RUGI
    '                            dsJasaAdministrasi.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaAdministrasi.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaAdministrasi.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaAdministrasi.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaAdministrasi.NAMA = xloop.NAMA
    '                            dsJasaAdministrasi.NAME_DISPLAY = "Administrasi"
    '                            dsJasaAdministrasi.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaAdministrasi.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaAdministrasi.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaAdministrasi.BAGIJASA = Sisa * (JasaAdministrasi / Total)
    '                                dsJasaAdministrasi.SISAPAKET = 0
    '                            Else
    '                                dsJasaAdministrasi.BAGIJASA = JasaAdministrasi
    '                                dsJasaAdministrasi.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaAdministrasi)

    '                            'Apoteker %
    '                            dsJasaApoteker.CEK = xloop.CEK
    '                            dsJasaApoteker.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaApoteker.KET = "BAGI APOTEKER"
    '                            dsJasaApoteker.TUJUAN = xloop.TUJUAN
    '                            dsJasaApoteker.NOSEP = xloop.NOSEP
    '                            dsJasaApoteker.RUGI = xloop.RUGI
    '                            dsJasaApoteker.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaApoteker.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaApoteker.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaApoteker.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaApoteker.NAMA = xloop.NAMA
    '                            dsJasaApoteker.NAME_DISPLAY = "Apoteker"
    '                            dsJasaApoteker.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaApoteker.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaApoteker.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaApoteker.BAGIJASA = Sisa * (JasaApoteker / Total)
    '                                dsJasaApoteker.SISAPAKET = 0
    '                            Else
    '                                dsJasaApoteker.BAGIJASA = JasaApoteker
    '                                dsJasaApoteker.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaApoteker)

    '                            'Input billing %
    '                            dsJasaInputBilling.CEK = xloop.CEK
    '                            dsJasaInputBilling.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaInputBilling.KET = "BAGI INPUT BILLING"
    '                            dsJasaInputBilling.TUJUAN = xloop.TUJUAN
    '                            dsJasaInputBilling.NOSEP = xloop.NOSEP
    '                            dsJasaInputBilling.RUGI = xloop.RUGI
    '                            dsJasaInputBilling.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaInputBilling.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaInputBilling.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaInputBilling.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaInputBilling.NAMA = xloop.NAMA
    '                            dsJasaInputBilling.NAME_DISPLAY = "Input Billing"
    '                            dsJasaInputBilling.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaInputBilling.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaInputBilling.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaInputBilling.BAGIJASA = Sisa * (JasaInputBilling / Total)
    '                                dsJasaInputBilling.SISAPAKET = 0
    '                            Else
    '                                dsJasaInputBilling.BAGIJASA = JasaInputBilling
    '                                dsJasaInputBilling.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaInputBilling)

    '                            'dpjp %
    '                            dsJasaDokterDPJP.CEK = xloop.CEK
    '                            dsJasaDokterDPJP.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaDokterDPJP.KET = "BAGI DPJP"
    '                            dsJasaDokterDPJP.TUJUAN = xloop.TUJUAN
    '                            dsJasaDokterDPJP.NOSEP = xloop.NOSEP
    '                            dsJasaDokterDPJP.RUGI = xloop.RUGI
    '                            dsJasaDokterDPJP.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaDokterDPJP.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaDokterDPJP.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaDokterDPJP.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaDokterDPJP.NAMA = xloop.NAMA
    '                            dsJasaDokterDPJP.NAME_DISPLAY = xloop.DOKTER_DPJP
    '                            dsJasaDokterDPJP.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaDokterDPJP.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaDokterDPJP.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaDokterDPJP.BAGIJASA = Sisa * (JasaDOKTERDPJP / Total)
    '                                dsJasaDokterDPJP.SISAPAKET = 0
    '                            Else
    '                                dsJasaDokterDPJP.BAGIJASA = JasaDOKTERDPJP
    '                                dsJasaDokterDPJP.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaDokterDPJP)

    '                            'UMUM
    '                            dsJasaDokterLAB.CEK = xloop.CEK
    '                            dsJasaDokterLAB.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaDokterLAB.KET = "BAGI RADIOLOGI"
    '                            dsJasaDokterLAB.TUJUAN = xloop.TUJUAN
    '                            dsJasaDokterLAB.NOSEP = xloop.NOSEP
    '                            dsJasaDokterLAB.RUGI = xloop.RUGI
    '                            dsJasaDokterLAB.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaDokterLAB.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaDokterLAB.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaDokterLAB.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaDokterLAB.NAMA = xloop.NAMA
    '                            dsJasaDokterLAB.NAME_DISPLAY = "dr. Radiologi"
    '                            dsJasaDokterLAB.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaDokterLAB.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaDokterLAB.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaDokterLAB.BAGIJASA = Sisa * (JasaDOKTERLAB / Total)
    '                                dsJasaDokterLAB.SISAPAKET = 0
    '                            Else
    '                                dsJasaDokterLAB.BAGIJASA = JasaDOKTERLAB
    '                                dsJasaDokterLAB.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaDokterLAB)

    '                            'LAB
    '                            dsJasaAnalis.CEK = xloop.CEK
    '                            dsJasaAnalis.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaAnalis.KET = "BAGI RADIOLOGI"
    '                            dsJasaAnalis.TUJUAN = xloop.TUJUAN
    '                            dsJasaAnalis.NOSEP = xloop.NOSEP
    '                            dsJasaAnalis.RUGI = xloop.RUGI
    '                            dsJasaAnalis.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaAnalis.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaAnalis.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaAnalis.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaAnalis.NAMA = xloop.NAMA
    '                            dsJasaAnalis.NAME_DISPLAY = "Radiografer"
    '                            dsJasaAnalis.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaAnalis.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaAnalis.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaAnalis.BAGIJASA = Sisa * (JasaLab / Total)
    '                                dsJasaAnalis.SISAPAKET = 0
    '                            Else
    '                                dsJasaAnalis.BAGIJASA = JasaLab
    '                                dsJasaAnalis.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaAnalis)

    '                            'perawat %
    '                            dsJasaPerawat.CEK = xloop.CEK
    '                            dsJasaPerawat.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaPerawat.KET = "BAGI PERAWAT"
    '                            dsJasaPerawat.TUJUAN = xloop.TUJUAN
    '                            dsJasaPerawat.NOSEP = xloop.NOSEP
    '                            dsJasaPerawat.RUGI = xloop.RUGI
    '                            dsJasaPerawat.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaPerawat.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaPerawat.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaPerawat.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaPerawat.NAMA = xloop.NAMA
    '                            dsJasaPerawat.NAME_DISPLAY = xloop.TUJUAN
    '                            dsJasaPerawat.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaPerawat.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaPerawat.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaPerawat.BAGIJASA = Sisa * (JasaPerawat / Total)
    '                                dsJasaPerawat.SISAPAKET = 0
    '                            Else
    '                                dsJasaPerawat.BAGIJASA = JasaPerawat
    '                                dsJasaPerawat.SISAPAKET = xloop.TOTAL_TARIF - (SisaPaket + Total)
    '                            End If

    '                            listHasil.Add(dsJasaPerawat)

    '                        ElseIf xloop.CEK = "08" Then
    '                            Dim dsJasaDokterDPJP As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaDokterLAB As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaDokterRAD As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaAnalis As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaRadiografer As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaPerawat As New DataAccess.R_DATA_HASIL

    '                            Dim JasaDOKTERDPJP As Decimal = 30000
    '                            Dim JasaDOKTERLAB As Decimal = 10000
    '                            Dim JasaDOKTERRAD As Decimal = 10000
    '                            Dim JasaLab As Decimal = 3000
    '                            Dim JasaRadiografer As Decimal = 3000
    '                            Dim JasaPerawat As Decimal = 4000


    '                            Dim dsJasaAdministrasi As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaApoteker As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaInputBilling As New DataAccess.R_DATA_HASIL
    '                            Dim JasaAdministrasi As Decimal = 5000
    '                            Dim JasaApoteker As Decimal = 1500
    '                            Dim JasaInputBilling As Decimal = 500

    '                            Dim Total As Decimal = JasaDOKTERDPJP + JasaDOKTERLAB + JasaDOKTERRAD + JasaPerawat + JasaLab + JasaRadiografer + JasaAdministrasi + JasaApoteker + JasaInputBilling

    '                            'Admisitrasi %
    '                            dsJasaAdministrasi.CEK = xloop.CEK
    '                            dsJasaAdministrasi.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaAdministrasi.KET = "BAGI ADMINISTRASI"
    '                            dsJasaAdministrasi.TUJUAN = xloop.TUJUAN
    '                            dsJasaAdministrasi.NOSEP = xloop.NOSEP
    '                            dsJasaAdministrasi.RUGI = xloop.RUGI
    '                            dsJasaAdministrasi.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaAdministrasi.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaAdministrasi.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaAdministrasi.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaAdministrasi.NAMA = xloop.NAMA
    '                            dsJasaAdministrasi.NAME_DISPLAY = "Administrasi"
    '                            dsJasaAdministrasi.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaAdministrasi.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaAdministrasi.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaAdministrasi.BAGIJASA = Sisa * (JasaAdministrasi / Total)
    '                                dsJasaAdministrasi.SISAPAKET = 0
    '                            Else
    '                                dsJasaAdministrasi.BAGIJASA = JasaAdministrasi
    '                                dsJasaAdministrasi.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaAdministrasi)

    '                            'Apoteker %
    '                            dsJasaApoteker.CEK = xloop.CEK
    '                            dsJasaApoteker.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaApoteker.KET = "BAGI APOTEKER"
    '                            dsJasaApoteker.TUJUAN = xloop.TUJUAN
    '                            dsJasaApoteker.NOSEP = xloop.NOSEP
    '                            dsJasaApoteker.RUGI = xloop.RUGI
    '                            dsJasaApoteker.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaApoteker.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaApoteker.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaApoteker.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaApoteker.NAMA = xloop.NAMA
    '                            dsJasaApoteker.NAME_DISPLAY = "Apoteker"
    '                            dsJasaApoteker.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaApoteker.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaApoteker.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaApoteker.BAGIJASA = Sisa * (JasaApoteker / Total)
    '                                dsJasaApoteker.SISAPAKET = 0
    '                            Else
    '                                dsJasaApoteker.BAGIJASA = JasaApoteker
    '                                dsJasaApoteker.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaApoteker)

    '                            'Input billing %
    '                            dsJasaInputBilling.CEK = xloop.CEK
    '                            dsJasaInputBilling.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaInputBilling.KET = "BAGI INPUT BILLING"
    '                            dsJasaInputBilling.TUJUAN = xloop.TUJUAN
    '                            dsJasaInputBilling.NOSEP = xloop.NOSEP
    '                            dsJasaInputBilling.RUGI = xloop.RUGI
    '                            dsJasaInputBilling.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaInputBilling.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaInputBilling.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaInputBilling.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaInputBilling.NAMA = xloop.NAMA
    '                            dsJasaInputBilling.NAME_DISPLAY = "Input Billing"
    '                            dsJasaInputBilling.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaInputBilling.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaInputBilling.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaInputBilling.BAGIJASA = Sisa * (JasaInputBilling / Total)
    '                                dsJasaInputBilling.SISAPAKET = 0
    '                            Else
    '                                dsJasaInputBilling.BAGIJASA = JasaInputBilling
    '                                dsJasaInputBilling.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaInputBilling)

    '                            'dpjp %
    '                            dsJasaDokterDPJP.CEK = xloop.CEK
    '                            dsJasaDokterDPJP.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaDokterDPJP.KET = "BAGI DPJP"
    '                            dsJasaDokterDPJP.TUJUAN = xloop.TUJUAN
    '                            dsJasaDokterDPJP.NOSEP = xloop.NOSEP
    '                            dsJasaDokterDPJP.RUGI = xloop.RUGI
    '                            dsJasaDokterDPJP.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaDokterDPJP.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaDokterDPJP.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaDokterDPJP.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaDokterDPJP.NAMA = xloop.NAMA
    '                            dsJasaDokterDPJP.NAME_DISPLAY = xloop.DOKTER_DPJP
    '                            dsJasaDokterDPJP.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaDokterDPJP.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaDokterDPJP.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaDokterDPJP.BAGIJASA = Sisa * (JasaDOKTERDPJP / Total)
    '                                dsJasaDokterDPJP.SISAPAKET = 0
    '                            Else
    '                                dsJasaDokterDPJP.BAGIJASA = JasaDOKTERDPJP
    '                                dsJasaDokterDPJP.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaDokterDPJP)

    '                            'UMUM
    '                            dsJasaDokterLAB.CEK = xloop.CEK
    '                            dsJasaDokterLAB.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaDokterLAB.KET = "BAGI LABORATORIUM"
    '                            dsJasaDokterLAB.TUJUAN = xloop.TUJUAN
    '                            dsJasaDokterLAB.NOSEP = xloop.NOSEP
    '                            dsJasaDokterLAB.RUGI = xloop.RUGI
    '                            dsJasaDokterLAB.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaDokterLAB.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaDokterLAB.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaDokterLAB.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaDokterLAB.NAMA = xloop.NAMA
    '                            dsJasaDokterLAB.NAME_DISPLAY = "dr. Laboratorium"
    '                            dsJasaDokterLAB.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaDokterLAB.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaDokterLAB.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaDokterLAB.BAGIJASA = Sisa * (JasaDOKTERLAB / Total)
    '                                dsJasaDokterLAB.SISAPAKET = 0
    '                            Else
    '                                dsJasaDokterLAB.BAGIJASA = JasaDOKTERLAB
    '                                dsJasaDokterLAB.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaDokterLAB)

    '                            'UMUM
    '                            dsJasaDokterRAD.CEK = xloop.CEK
    '                            dsJasaDokterRAD.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaDokterRAD.KET = "BAGI RADIOLOGI"
    '                            dsJasaDokterRAD.TUJUAN = xloop.TUJUAN
    '                            dsJasaDokterRAD.NOSEP = xloop.NOSEP
    '                            dsJasaDokterRAD.RUGI = xloop.RUGI
    '                            dsJasaDokterRAD.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaDokterRAD.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaDokterRAD.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaDokterRAD.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaDokterRAD.NAMA = xloop.NAMA
    '                            dsJasaDokterRAD.NAME_DISPLAY = "dr. Radiologi"
    '                            dsJasaDokterRAD.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaDokterRAD.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaDokterRAD.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaDokterRAD.BAGIJASA = Sisa * (JasaDOKTERRAD / Total)
    '                                dsJasaDokterRAD.SISAPAKET = 0
    '                            Else
    '                                dsJasaDokterRAD.BAGIJASA = JasaDOKTERRAD
    '                                dsJasaDokterRAD.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaDokterRAD)

    '                            'LAB
    '                            dsJasaAnalis.CEK = xloop.CEK
    '                            dsJasaAnalis.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaAnalis.KET = "BAGI LABORATORIUM"
    '                            dsJasaAnalis.TUJUAN = xloop.TUJUAN
    '                            dsJasaAnalis.NOSEP = xloop.NOSEP
    '                            dsJasaAnalis.RUGI = xloop.RUGI
    '                            dsJasaAnalis.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaAnalis.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaAnalis.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaAnalis.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaAnalis.NAMA = xloop.NAMA
    '                            dsJasaAnalis.NAME_DISPLAY = "Analis"
    '                            dsJasaAnalis.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaAnalis.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaAnalis.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaAnalis.BAGIJASA = Sisa * (JasaLab / Total)
    '                                dsJasaAnalis.SISAPAKET = 0
    '                            Else
    '                                dsJasaAnalis.BAGIJASA = JasaLab
    '                                dsJasaAnalis.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaAnalis)

    '                            'RAD
    '                            dsJasaRadiografer.CEK = xloop.CEK
    '                            dsJasaRadiografer.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaRadiografer.KET = "BAGI RADIOLOGI"
    '                            dsJasaRadiografer.TUJUAN = xloop.TUJUAN
    '                            dsJasaRadiografer.NOSEP = xloop.NOSEP
    '                            dsJasaRadiografer.RUGI = xloop.RUGI
    '                            dsJasaRadiografer.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaRadiografer.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaRadiografer.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaRadiografer.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaRadiografer.NAMA = xloop.NAMA
    '                            dsJasaRadiografer.NAME_DISPLAY = "Radiografer"
    '                            dsJasaRadiografer.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaRadiografer.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaRadiografer.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaRadiografer.BAGIJASA = Sisa * (JasaRadiografer / Total)
    '                                dsJasaRadiografer.SISAPAKET = 0
    '                            Else
    '                                dsJasaRadiografer.BAGIJASA = JasaRadiografer
    '                                dsJasaRadiografer.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaRadiografer)

    '                            'perawat %
    '                            dsJasaPerawat.CEK = xloop.CEK
    '                            dsJasaPerawat.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaPerawat.KET = "BAGI PERAWAT"
    '                            dsJasaPerawat.TUJUAN = xloop.TUJUAN
    '                            dsJasaPerawat.NOSEP = xloop.NOSEP
    '                            dsJasaPerawat.RUGI = xloop.RUGI
    '                            dsJasaPerawat.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaPerawat.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaPerawat.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaPerawat.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaPerawat.NAMA = xloop.NAMA
    '                            dsJasaPerawat.NAME_DISPLAY = xloop.TUJUAN
    '                            dsJasaPerawat.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaPerawat.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaPerawat.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaPerawat.BAGIJASA = Sisa * (JasaPerawat / Total)
    '                                dsJasaPerawat.SISAPAKET = 0
    '                            Else
    '                                dsJasaPerawat.BAGIJASA = JasaPerawat
    '                                dsJasaPerawat.SISAPAKET = xloop.TOTAL_TARIF - (SisaPaket + Total)
    '                            End If

    '                            listHasil.Add(dsJasaPerawat)

    '                        ElseIf xloop.CEK = "09" Then

    '                            Dim dsJasaDokter As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaPerawat As New DataAccess.R_DATA_HASIL

    '                            Dim JasaDPJP As Decimal = 50000
    '                            Dim JasaPerawat As Decimal = 20000

    '                            Dim dsJasaAdministrasi As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaApoteker As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaInputBilling As New DataAccess.R_DATA_HASIL
    '                            Dim JasaAdministrasi As Decimal = 5000
    '                            Dim JasaApoteker As Decimal = 1500
    '                            Dim JasaInputBilling As Decimal = 500

    '                            Dim Total As Decimal = JasaDPJP + JasaPerawat + JasaAdministrasi + JasaApoteker + JasaInputBilling

    '                            'Admisitrasi %
    '                            dsJasaAdministrasi.CEK = xloop.CEK
    '                            dsJasaAdministrasi.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaAdministrasi.KET = "BAGI ADMINISTRASI"
    '                            dsJasaAdministrasi.TUJUAN = xloop.TUJUAN
    '                            dsJasaAdministrasi.NOSEP = xloop.NOSEP
    '                            dsJasaAdministrasi.RUGI = xloop.RUGI
    '                            dsJasaAdministrasi.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaAdministrasi.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaAdministrasi.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaAdministrasi.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaAdministrasi.NAMA = xloop.NAMA
    '                            dsJasaAdministrasi.NAME_DISPLAY = "Administrasi"
    '                            dsJasaAdministrasi.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaAdministrasi.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaAdministrasi.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaAdministrasi.BAGIJASA = Sisa * (JasaAdministrasi / Total)
    '                                dsJasaAdministrasi.SISAPAKET = 0
    '                            Else
    '                                dsJasaAdministrasi.BAGIJASA = JasaAdministrasi
    '                                dsJasaAdministrasi.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaAdministrasi)

    '                            'Apoteker %
    '                            dsJasaApoteker.CEK = xloop.CEK
    '                            dsJasaApoteker.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaApoteker.KET = "BAGI APOTEKER"
    '                            dsJasaApoteker.TUJUAN = xloop.TUJUAN
    '                            dsJasaApoteker.NOSEP = xloop.NOSEP
    '                            dsJasaApoteker.RUGI = xloop.RUGI
    '                            dsJasaApoteker.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaApoteker.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaApoteker.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaApoteker.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaApoteker.NAMA = xloop.NAMA
    '                            dsJasaApoteker.NAME_DISPLAY = "Apoteker"
    '                            dsJasaApoteker.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaApoteker.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaApoteker.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaApoteker.BAGIJASA = Sisa * (JasaApoteker / Total)
    '                                dsJasaApoteker.SISAPAKET = 0
    '                            Else
    '                                dsJasaApoteker.BAGIJASA = JasaApoteker
    '                                dsJasaApoteker.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaApoteker)

    '                            'Input billing %
    '                            dsJasaInputBilling.CEK = xloop.CEK
    '                            dsJasaInputBilling.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaInputBilling.KET = "BAGI INPUT BILLING"
    '                            dsJasaInputBilling.TUJUAN = xloop.TUJUAN
    '                            dsJasaInputBilling.NOSEP = xloop.NOSEP
    '                            dsJasaInputBilling.RUGI = xloop.RUGI
    '                            dsJasaInputBilling.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaInputBilling.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaInputBilling.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaInputBilling.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaInputBilling.NAMA = xloop.NAMA
    '                            dsJasaInputBilling.NAME_DISPLAY = "Input Billing"
    '                            dsJasaInputBilling.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaInputBilling.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaInputBilling.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaInputBilling.BAGIJASA = Sisa * (JasaInputBilling / Total)
    '                                dsJasaInputBilling.SISAPAKET = 0
    '                            Else
    '                                dsJasaInputBilling.BAGIJASA = JasaInputBilling
    '                                dsJasaInputBilling.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaInputBilling)

    '                            'dpjp %
    '                            dsJasaDokter.CEK = xloop.CEK
    '                            dsJasaDokter.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaDokter.KET = "BAGI DPJP"
    '                            dsJasaDokter.TUJUAN = xloop.TUJUAN
    '                            dsJasaDokter.NOSEP = xloop.NOSEP
    '                            dsJasaDokter.RUGI = xloop.RUGI
    '                            dsJasaDokter.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaDokter.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaDokter.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaDokter.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaDokter.NAMA = xloop.NAMA
    '                            dsJasaDokter.NAME_DISPLAY = xloop.DOKTER_DPJP
    '                            dsJasaDokter.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaDokter.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaDokter.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaDokter.BAGIJASA = Sisa * (JasaDPJP / Total)
    '                                dsJasaDokter.SISAPAKET = 0
    '                            Else
    '                                dsJasaDokter.BAGIJASA = JasaDPJP
    '                                dsJasaDokter.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaDokter)

    '                            'perawat %
    '                            dsJasaPerawat.CEK = xloop.CEK
    '                            dsJasaPerawat.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaPerawat.KET = "BAGI BIDAN POLI"
    '                            dsJasaPerawat.TUJUAN = xloop.TUJUAN
    '                            dsJasaPerawat.NOSEP = xloop.NOSEP
    '                            dsJasaPerawat.RUGI = xloop.RUGI
    '                            dsJasaPerawat.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaPerawat.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaPerawat.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaPerawat.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaPerawat.NAMA = xloop.NAMA
    '                            dsJasaPerawat.NAME_DISPLAY = "Bidan Poli"
    '                            dsJasaPerawat.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaPerawat.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaPerawat.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaPerawat.BAGIJASA = Sisa * (JasaPerawat / Total)
    '                                dsJasaPerawat.SISAPAKET = 0
    '                            Else
    '                                dsJasaPerawat.BAGIJASA = JasaPerawat
    '                                dsJasaPerawat.SISAPAKET = xloop.TOTAL_TARIF - (SisaPaket + Total)
    '                            End If

    '                            listHasil.Add(dsJasaPerawat)

    '                        ElseIf xloop.CEK = "10" Then

    '                            Dim dsJasaDokterSpesialis As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaDokterUmum As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaDokterLab As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaAnalis As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaPerawat As New DataAccess.R_DATA_HASIL

    '                            Dim JasaDokterSpesialis As Decimal = 20000
    '                            Dim JasaDokterUmum As Decimal = 20000
    '                            Dim JasaDokterLab As Decimal = 10000
    '                            Dim JasaAnalis As Decimal = 5000
    '                            Dim JasaPerawat As Decimal = 5000

    '                            Dim dsJasaAdministrasi As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaApoteker As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaInputBilling As New DataAccess.R_DATA_HASIL
    '                            Dim JasaAdministrasi As Decimal = 5000
    '                            Dim JasaApoteker As Decimal = 1500
    '                            Dim JasaInputBilling As Decimal = 500

    '                            Dim Total As Decimal = JasaDokterSpesialis + JasaDokterUmum + JasaDokterLab + JasaAnalis + JasaPerawat + JasaAdministrasi + JasaApoteker + JasaInputBilling

    '                            'Admisitrasi %
    '                            dsJasaAdministrasi.CEK = xloop.CEK
    '                            dsJasaAdministrasi.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaAdministrasi.KET = "BAGI ADMINISTRASI"
    '                            dsJasaAdministrasi.TUJUAN = xloop.TUJUAN
    '                            dsJasaAdministrasi.NOSEP = xloop.NOSEP
    '                            dsJasaAdministrasi.RUGI = xloop.RUGI
    '                            dsJasaAdministrasi.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaAdministrasi.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaAdministrasi.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaAdministrasi.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaAdministrasi.NAMA = xloop.NAMA
    '                            dsJasaAdministrasi.NAME_DISPLAY = "Administrasi"
    '                            dsJasaAdministrasi.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaAdministrasi.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaAdministrasi.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaAdministrasi.BAGIJASA = Sisa * (JasaAdministrasi / Total)
    '                                dsJasaAdministrasi.SISAPAKET = 0
    '                            Else
    '                                dsJasaAdministrasi.BAGIJASA = JasaAdministrasi
    '                                dsJasaAdministrasi.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaAdministrasi)

    '                            'Apoteker %
    '                            dsJasaApoteker.CEK = xloop.CEK
    '                            dsJasaApoteker.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaApoteker.KET = "BAGI APOTEKER"
    '                            dsJasaApoteker.TUJUAN = xloop.TUJUAN
    '                            dsJasaApoteker.NOSEP = xloop.NOSEP
    '                            dsJasaApoteker.RUGI = xloop.RUGI
    '                            dsJasaApoteker.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaApoteker.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaApoteker.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaApoteker.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaApoteker.NAMA = xloop.NAMA
    '                            dsJasaApoteker.NAME_DISPLAY = "Apoteker"
    '                            dsJasaApoteker.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaApoteker.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaApoteker.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaApoteker.BAGIJASA = Sisa * (JasaApoteker / Total)
    '                                dsJasaApoteker.SISAPAKET = 0
    '                            Else
    '                                dsJasaApoteker.BAGIJASA = JasaApoteker
    '                                dsJasaApoteker.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaApoteker)

    '                            'Input billing %
    '                            dsJasaInputBilling.CEK = xloop.CEK
    '                            dsJasaInputBilling.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaInputBilling.KET = "BAGI INPUT BILLING"
    '                            dsJasaInputBilling.TUJUAN = xloop.TUJUAN
    '                            dsJasaInputBilling.NOSEP = xloop.NOSEP
    '                            dsJasaInputBilling.RUGI = xloop.RUGI
    '                            dsJasaInputBilling.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaInputBilling.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaInputBilling.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaInputBilling.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaInputBilling.NAMA = xloop.NAMA
    '                            dsJasaInputBilling.NAME_DISPLAY = "Input Billing"
    '                            dsJasaInputBilling.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaInputBilling.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaInputBilling.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaInputBilling.BAGIJASA = Sisa * (JasaInputBilling / Total)
    '                                dsJasaInputBilling.SISAPAKET = 0
    '                            Else
    '                                dsJasaInputBilling.BAGIJASA = JasaInputBilling
    '                                dsJasaInputBilling.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaInputBilling)

    '                            'dpjp %
    '                            dsJasaDokterSpesialis.CEK = xloop.CEK
    '                            dsJasaDokterSpesialis.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaDokterSpesialis.KET = "BAGI DPJP"
    '                            dsJasaDokterSpesialis.TUJUAN = xloop.TUJUAN
    '                            dsJasaDokterSpesialis.NOSEP = xloop.NOSEP
    '                            dsJasaDokterSpesialis.RUGI = xloop.RUGI
    '                            dsJasaDokterSpesialis.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaDokterSpesialis.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaDokterSpesialis.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaDokterSpesialis.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaDokterSpesialis.NAMA = xloop.NAMA
    '                            dsJasaDokterSpesialis.NAME_DISPLAY = xloop.DOKTER_DPJP
    '                            dsJasaDokterSpesialis.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaDokterSpesialis.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaDokterSpesialis.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaDokterSpesialis.BAGIJASA = Sisa * (JasaDokterSpesialis / Total)
    '                                dsJasaDokterSpesialis.SISAPAKET = 0
    '                            Else
    '                                dsJasaDokterSpesialis.BAGIJASA = JasaDokterSpesialis
    '                                dsJasaDokterSpesialis.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaDokterSpesialis)

    '                            'umum %
    '                            dsJasaDokterUmum.CEK = xloop.CEK
    '                            dsJasaDokterUmum.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaDokterUmum.KET = "BAGI DELAGASI"
    '                            dsJasaDokterUmum.TUJUAN = xloop.TUJUAN
    '                            dsJasaDokterUmum.NOSEP = xloop.NOSEP
    '                            dsJasaDokterUmum.RUGI = xloop.RUGI
    '                            dsJasaDokterUmum.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaDokterUmum.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaDokterUmum.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaDokterUmum.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaDokterUmum.NAMA = xloop.NAMA
    '                            dsJasaDokterUmum.NAME_DISPLAY = xloop.DOKTER
    '                            dsJasaDokterUmum.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaDokterUmum.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaDokterUmum.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaDokterUmum.BAGIJASA = Sisa * (JasaDokterUmum / Total)
    '                                dsJasaDokterUmum.SISAPAKET = 0
    '                            Else
    '                                dsJasaDokterUmum.BAGIJASA = JasaDokterUmum
    '                                dsJasaDokterUmum.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaDokterUmum)

    '                            'umum %
    '                            dsJasaDokterLab.CEK = xloop.CEK
    '                            dsJasaDokterLab.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaDokterLab.KET = "BAGI LABORATORIUM"
    '                            dsJasaDokterLab.TUJUAN = xloop.TUJUAN
    '                            dsJasaDokterLab.NOSEP = xloop.NOSEP
    '                            dsJasaDokterLab.RUGI = xloop.RUGI
    '                            dsJasaDokterLab.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaDokterLab.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaDokterLab.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaDokterLab.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaDokterLab.NAMA = xloop.NAMA
    '                            dsJasaDokterLab.NAME_DISPLAY = "dr. Laboratorium"
    '                            dsJasaDokterLab.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaDokterLab.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaDokterLab.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaDokterLab.BAGIJASA = Sisa * (JasaDokterLab / Total)
    '                                dsJasaDokterLab.SISAPAKET = 0
    '                            Else
    '                                dsJasaDokterLab.BAGIJASA = JasaDokterLab
    '                                dsJasaDokterLab.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaDokterLab)

    '                            'umum %
    '                            dsJasaAnalis.CEK = xloop.CEK
    '                            dsJasaAnalis.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaAnalis.KET = "BAGI LABORATORIUM"
    '                            dsJasaAnalis.TUJUAN = xloop.TUJUAN
    '                            dsJasaAnalis.NOSEP = xloop.NOSEP
    '                            dsJasaAnalis.RUGI = xloop.RUGI
    '                            dsJasaAnalis.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaAnalis.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaAnalis.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaAnalis.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaAnalis.NAMA = xloop.NAMA
    '                            dsJasaAnalis.NAME_DISPLAY = "Analis"
    '                            dsJasaAnalis.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaAnalis.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaAnalis.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaAnalis.BAGIJASA = Sisa * (JasaAnalis / Total)
    '                                dsJasaAnalis.SISAPAKET = 0
    '                            Else
    '                                dsJasaAnalis.BAGIJASA = JasaAnalis
    '                                dsJasaAnalis.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaAnalis)

    '                            'umum %
    '                            dsJasaPerawat.CEK = xloop.CEK
    '                            dsJasaPerawat.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaPerawat.KET = "BAGI PERAWAT"
    '                            dsJasaPerawat.TUJUAN = xloop.TUJUAN
    '                            dsJasaPerawat.NOSEP = xloop.NOSEP
    '                            dsJasaPerawat.RUGI = xloop.RUGI
    '                            dsJasaPerawat.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaPerawat.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaPerawat.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaPerawat.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaPerawat.NAMA = xloop.NAMA
    '                            dsJasaPerawat.NAME_DISPLAY = xloop.TUJUAN
    '                            dsJasaPerawat.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaPerawat.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaPerawat.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaPerawat.BAGIJASA = Sisa * (JasaPerawat / Total)
    '                                dsJasaPerawat.SISAPAKET = 0
    '                            Else
    '                                dsJasaPerawat.BAGIJASA = JasaPerawat
    '                                dsJasaPerawat.SISAPAKET = xloop.TOTAL_TARIF - (SisaPaket + Total)
    '                            End If

    '                            listHasil.Add(dsJasaPerawat)

    '                        ElseIf xloop.CEK = "11" Then

    '                            Dim dsJasaDokterSpesialis As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaDokterUmum As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaDokterLab As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaAnalis As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaPerawat As New DataAccess.R_DATA_HASIL

    '                            Dim JasaDokterSpesialis As Decimal = 20000
    '                            Dim JasaDokterUmum As Decimal = 20000
    '                            Dim JasaDokterLab As Decimal = 10000
    '                            Dim JasaAnalis As Decimal = 5000
    '                            Dim JasaPerawat As Decimal = 5000

    '                            Dim dsJasaAdministrasi As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaApoteker As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaInputBilling As New DataAccess.R_DATA_HASIL
    '                            Dim JasaAdministrasi As Decimal = 5000
    '                            Dim JasaApoteker As Decimal = 1500
    '                            Dim JasaInputBilling As Decimal = 500

    '                            Dim Total As Decimal = JasaDokterSpesialis + JasaDokterUmum + JasaDokterLab + JasaAnalis + JasaPerawat + JasaAdministrasi + JasaApoteker + JasaInputBilling

    '                            'Admisitrasi %
    '                            dsJasaAdministrasi.CEK = xloop.CEK
    '                            dsJasaAdministrasi.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaAdministrasi.KET = "BAGI ADMINISTRASI"
    '                            dsJasaAdministrasi.TUJUAN = xloop.TUJUAN
    '                            dsJasaAdministrasi.NOSEP = xloop.NOSEP
    '                            dsJasaAdministrasi.RUGI = xloop.RUGI
    '                            dsJasaAdministrasi.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaAdministrasi.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaAdministrasi.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaAdministrasi.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaAdministrasi.NAMA = xloop.NAMA
    '                            dsJasaAdministrasi.NAME_DISPLAY = "Administrasi"
    '                            dsJasaAdministrasi.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaAdministrasi.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaAdministrasi.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaAdministrasi.BAGIJASA = Sisa * (JasaAdministrasi / Total)
    '                                dsJasaAdministrasi.SISAPAKET = 0
    '                            Else
    '                                dsJasaAdministrasi.BAGIJASA = JasaAdministrasi
    '                                dsJasaAdministrasi.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaAdministrasi)

    '                            'Apoteker %
    '                            dsJasaApoteker.CEK = xloop.CEK
    '                            dsJasaApoteker.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaApoteker.KET = "BAGI APOTEKER"
    '                            dsJasaApoteker.TUJUAN = xloop.TUJUAN
    '                            dsJasaApoteker.NOSEP = xloop.NOSEP
    '                            dsJasaApoteker.RUGI = xloop.RUGI
    '                            dsJasaApoteker.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaApoteker.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaApoteker.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaApoteker.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaApoteker.NAMA = xloop.NAMA
    '                            dsJasaApoteker.NAME_DISPLAY = "Apoteker"
    '                            dsJasaApoteker.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaApoteker.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaApoteker.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaApoteker.BAGIJASA = Sisa * (JasaApoteker / Total)
    '                                dsJasaApoteker.SISAPAKET = 0
    '                            Else
    '                                dsJasaApoteker.BAGIJASA = JasaApoteker
    '                                dsJasaApoteker.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaApoteker)

    '                            'Input billing %
    '                            dsJasaInputBilling.CEK = xloop.CEK
    '                            dsJasaInputBilling.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaInputBilling.KET = "BAGI INPUT BILLING"
    '                            dsJasaInputBilling.TUJUAN = xloop.TUJUAN
    '                            dsJasaInputBilling.NOSEP = xloop.NOSEP
    '                            dsJasaInputBilling.RUGI = xloop.RUGI
    '                            dsJasaInputBilling.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaInputBilling.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaInputBilling.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaInputBilling.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaInputBilling.NAMA = xloop.NAMA
    '                            dsJasaInputBilling.NAME_DISPLAY = "Input Billing"
    '                            dsJasaInputBilling.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaInputBilling.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaInputBilling.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaInputBilling.BAGIJASA = Sisa * (JasaInputBilling / Total)
    '                                dsJasaInputBilling.SISAPAKET = 0
    '                            Else
    '                                dsJasaInputBilling.BAGIJASA = JasaInputBilling
    '                                dsJasaInputBilling.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaInputBilling)

    '                            'dpjp %
    '                            dsJasaDokterSpesialis.CEK = xloop.CEK
    '                            dsJasaDokterSpesialis.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaDokterSpesialis.KET = "BAGI DPJP"
    '                            dsJasaDokterSpesialis.TUJUAN = xloop.TUJUAN
    '                            dsJasaDokterSpesialis.NOSEP = xloop.NOSEP
    '                            dsJasaDokterSpesialis.RUGI = xloop.RUGI
    '                            dsJasaDokterSpesialis.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaDokterSpesialis.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaDokterSpesialis.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaDokterSpesialis.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaDokterSpesialis.NAMA = xloop.NAMA
    '                            dsJasaDokterSpesialis.NAME_DISPLAY = xloop.DOKTER_DPJP
    '                            dsJasaDokterSpesialis.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaDokterSpesialis.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaDokterSpesialis.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaDokterSpesialis.BAGIJASA = Sisa * (JasaDokterSpesialis / Total)
    '                                dsJasaDokterSpesialis.SISAPAKET = 0
    '                            Else
    '                                dsJasaDokterSpesialis.BAGIJASA = JasaDokterSpesialis
    '                                dsJasaDokterSpesialis.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaDokterSpesialis)

    '                            'umum %
    '                            dsJasaDokterUmum.CEK = xloop.CEK
    '                            dsJasaDokterUmum.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaDokterUmum.KET = "BAGI DELAGASI"
    '                            dsJasaDokterUmum.TUJUAN = xloop.TUJUAN
    '                            dsJasaDokterUmum.NOSEP = xloop.NOSEP
    '                            dsJasaDokterUmum.RUGI = xloop.RUGI
    '                            dsJasaDokterUmum.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaDokterUmum.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaDokterUmum.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaDokterUmum.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaDokterUmum.NAMA = xloop.NAMA
    '                            dsJasaDokterUmum.NAME_DISPLAY = xloop.DOKTER
    '                            dsJasaDokterUmum.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaDokterUmum.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaDokterUmum.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaDokterUmum.BAGIJASA = Sisa * (JasaDokterUmum / Total)
    '                                dsJasaDokterUmum.SISAPAKET = 0
    '                            Else
    '                                dsJasaDokterUmum.BAGIJASA = JasaDokterUmum
    '                                dsJasaDokterUmum.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaDokterUmum)

    '                            'umum %
    '                            dsJasaDokterLab.CEK = xloop.CEK
    '                            dsJasaDokterLab.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaDokterLab.KET = "BAGI RADIOLOGI"
    '                            dsJasaDokterLab.TUJUAN = xloop.TUJUAN
    '                            dsJasaDokterLab.NOSEP = xloop.NOSEP
    '                            dsJasaDokterLab.RUGI = xloop.RUGI
    '                            dsJasaDokterLab.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaDokterLab.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaDokterLab.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaDokterLab.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaDokterLab.NAMA = xloop.NAMA
    '                            dsJasaDokterLab.NAME_DISPLAY = "dr. Radiologi"
    '                            dsJasaDokterLab.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaDokterLab.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaDokterLab.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaDokterLab.BAGIJASA = Sisa * (JasaDokterLab / Total)
    '                                dsJasaDokterLab.SISAPAKET = 0
    '                            Else
    '                                dsJasaDokterLab.BAGIJASA = JasaDokterLab
    '                                dsJasaDokterLab.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaDokterLab)

    '                            'umum %
    '                            dsJasaAnalis.CEK = xloop.CEK
    '                            dsJasaAnalis.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaAnalis.KET = "BAGI RADIOLOGI"
    '                            dsJasaAnalis.TUJUAN = xloop.TUJUAN
    '                            dsJasaAnalis.NOSEP = xloop.NOSEP
    '                            dsJasaAnalis.RUGI = xloop.RUGI
    '                            dsJasaAnalis.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaAnalis.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaAnalis.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaAnalis.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaAnalis.NAMA = xloop.NAMA
    '                            dsJasaAnalis.NAME_DISPLAY = "Radiografer"
    '                            dsJasaAnalis.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaAnalis.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaAnalis.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaAnalis.BAGIJASA = Sisa * (JasaAnalis / Total)
    '                                dsJasaAnalis.SISAPAKET = 0
    '                            Else
    '                                dsJasaAnalis.BAGIJASA = JasaAnalis
    '                                dsJasaAnalis.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaAnalis)

    '                            'umum %
    '                            dsJasaPerawat.CEK = xloop.CEK
    '                            dsJasaPerawat.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaPerawat.KET = "BAGI PERAWAT"
    '                            dsJasaPerawat.TUJUAN = xloop.TUJUAN
    '                            dsJasaPerawat.NOSEP = xloop.NOSEP
    '                            dsJasaPerawat.RUGI = xloop.RUGI
    '                            dsJasaPerawat.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaPerawat.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaPerawat.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaPerawat.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaPerawat.NAMA = xloop.NAMA
    '                            dsJasaPerawat.NAME_DISPLAY = xloop.TUJUAN
    '                            dsJasaPerawat.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaPerawat.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaPerawat.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaPerawat.BAGIJASA = Sisa * (JasaPerawat / Total)
    '                                dsJasaPerawat.SISAPAKET = 0
    '                            Else
    '                                dsJasaPerawat.BAGIJASA = JasaPerawat
    '                                dsJasaPerawat.SISAPAKET = xloop.TOTAL_TARIF - (SisaPaket + Total)
    '                            End If

    '                            listHasil.Add(dsJasaPerawat)

    '                        ElseIf xloop.CEK = "12" Then
    '                            Dim dsJasaDokterSpesialis As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaDokterUmum As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaDokterLab As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaAnalis As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaPerawat As New DataAccess.R_DATA_HASIL

    '                            Dim JasaDokterSpesialis As Decimal = 20000
    '                            Dim JasaDokterUmum As Decimal = 20000
    '                            Dim JasaDokterLab As Decimal = 10000
    '                            Dim JasaAnalis As Decimal = 5000
    '                            Dim JasaPerawat As Decimal = 5000

    '                            Dim dsJasaAdministrasi As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaApoteker As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaInputBilling As New DataAccess.R_DATA_HASIL
    '                            Dim JasaAdministrasi As Decimal = 5000
    '                            Dim JasaApoteker As Decimal = 1500
    '                            Dim JasaInputBilling As Decimal = 500

    '                            Dim Total As Decimal = JasaDokterSpesialis + JasaDokterUmum + JasaDokterLab + JasaAnalis + JasaPerawat + JasaAdministrasi + JasaApoteker + JasaInputBilling

    '                            'Admisitrasi %
    '                            dsJasaAdministrasi.CEK = xloop.CEK
    '                            dsJasaAdministrasi.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaAdministrasi.KET = "BAGI ADMINISTRASI"
    '                            dsJasaAdministrasi.TUJUAN = xloop.TUJUAN
    '                            dsJasaAdministrasi.NOSEP = xloop.NOSEP
    '                            dsJasaAdministrasi.RUGI = xloop.RUGI
    '                            dsJasaAdministrasi.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaAdministrasi.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaAdministrasi.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaAdministrasi.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaAdministrasi.NAMA = xloop.NAMA
    '                            dsJasaAdministrasi.NAME_DISPLAY = "Administrasi"
    '                            dsJasaAdministrasi.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaAdministrasi.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaAdministrasi.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaAdministrasi.BAGIJASA = Sisa * (JasaAdministrasi / Total)
    '                                dsJasaAdministrasi.SISAPAKET = 0
    '                            Else
    '                                dsJasaAdministrasi.BAGIJASA = JasaAdministrasi
    '                                dsJasaAdministrasi.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaAdministrasi)

    '                            'Apoteker %
    '                            dsJasaApoteker.CEK = xloop.CEK
    '                            dsJasaApoteker.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaApoteker.KET = "BAGI APOTEKER"
    '                            dsJasaApoteker.TUJUAN = xloop.TUJUAN
    '                            dsJasaApoteker.NOSEP = xloop.NOSEP
    '                            dsJasaApoteker.RUGI = xloop.RUGI
    '                            dsJasaApoteker.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaApoteker.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaApoteker.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaApoteker.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaApoteker.NAMA = xloop.NAMA
    '                            dsJasaApoteker.NAME_DISPLAY = "Apoteker"
    '                            dsJasaApoteker.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaApoteker.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaApoteker.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaApoteker.BAGIJASA = Sisa * (JasaApoteker / Total)
    '                                dsJasaApoteker.SISAPAKET = 0
    '                            Else
    '                                dsJasaApoteker.BAGIJASA = JasaApoteker
    '                                dsJasaApoteker.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaApoteker)

    '                            'Input billing %
    '                            dsJasaInputBilling.CEK = xloop.CEK
    '                            dsJasaInputBilling.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaInputBilling.KET = "BAGI INPUT BILLING"
    '                            dsJasaInputBilling.TUJUAN = xloop.TUJUAN
    '                            dsJasaInputBilling.NOSEP = xloop.NOSEP
    '                            dsJasaInputBilling.RUGI = xloop.RUGI
    '                            dsJasaInputBilling.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaInputBilling.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaInputBilling.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaInputBilling.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaInputBilling.NAMA = xloop.NAMA
    '                            dsJasaInputBilling.NAME_DISPLAY = "Input Billing"
    '                            dsJasaInputBilling.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaInputBilling.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaInputBilling.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaInputBilling.BAGIJASA = Sisa * (JasaInputBilling / Total)
    '                                dsJasaInputBilling.SISAPAKET = 0
    '                            Else
    '                                dsJasaInputBilling.BAGIJASA = JasaInputBilling
    '                                dsJasaInputBilling.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaInputBilling)

    '                            'dpjp %
    '                            dsJasaDokterSpesialis.CEK = xloop.CEK
    '                            dsJasaDokterSpesialis.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaDokterSpesialis.KET = "BAGI DPJP"
    '                            dsJasaDokterSpesialis.TUJUAN = xloop.TUJUAN
    '                            dsJasaDokterSpesialis.NOSEP = xloop.NOSEP
    '                            dsJasaDokterSpesialis.RUGI = xloop.RUGI
    '                            dsJasaDokterSpesialis.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaDokterSpesialis.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaDokterSpesialis.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaDokterSpesialis.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaDokterSpesialis.NAMA = xloop.NAMA
    '                            dsJasaDokterSpesialis.NAME_DISPLAY = xloop.DOKTER_DPJP
    '                            dsJasaDokterSpesialis.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaDokterSpesialis.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaDokterSpesialis.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaDokterSpesialis.BAGIJASA = Sisa * (JasaDokterSpesialis / Total)
    '                                dsJasaDokterSpesialis.SISAPAKET = 0
    '                            Else
    '                                dsJasaDokterSpesialis.BAGIJASA = JasaDokterSpesialis
    '                                dsJasaDokterSpesialis.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaDokterSpesialis)

    '                            'umum %
    '                            dsJasaDokterUmum.CEK = xloop.CEK
    '                            dsJasaDokterUmum.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaDokterUmum.KET = "BAGI DELAGASI"
    '                            dsJasaDokterUmum.TUJUAN = xloop.TUJUAN
    '                            dsJasaDokterUmum.NOSEP = xloop.NOSEP
    '                            dsJasaDokterUmum.RUGI = xloop.RUGI
    '                            dsJasaDokterUmum.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaDokterUmum.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaDokterUmum.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaDokterUmum.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaDokterUmum.NAMA = xloop.NAMA
    '                            dsJasaDokterUmum.NAME_DISPLAY = xloop.DOKTER
    '                            dsJasaDokterUmum.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaDokterUmum.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaDokterUmum.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaDokterUmum.BAGIJASA = Sisa * (JasaDokterUmum / Total)
    '                                dsJasaDokterUmum.SISAPAKET = 0
    '                            Else
    '                                dsJasaDokterUmum.BAGIJASA = JasaDokterUmum
    '                                dsJasaDokterUmum.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaDokterUmum)

    '                            'umum %
    '                            dsJasaDokterLab.CEK = xloop.CEK
    '                            dsJasaDokterLab.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaDokterLab.KET = "BAGI RADIOLOGI"
    '                            dsJasaDokterLab.TUJUAN = xloop.TUJUAN
    '                            dsJasaDokterLab.NOSEP = xloop.NOSEP
    '                            dsJasaDokterLab.RUGI = xloop.RUGI
    '                            dsJasaDokterLab.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaDokterLab.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaDokterLab.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaDokterLab.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaDokterLab.NAMA = xloop.NAMA
    '                            dsJasaDokterLab.NAME_DISPLAY = "dr. Radiologi"
    '                            dsJasaDokterLab.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaDokterLab.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaDokterLab.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaDokterLab.BAGIJASA = Sisa * (JasaDokterLab / Total)
    '                                dsJasaDokterLab.SISAPAKET = 0
    '                            Else
    '                                dsJasaDokterLab.BAGIJASA = JasaDokterLab
    '                                dsJasaDokterLab.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaDokterLab)

    '                            'umum %
    '                            dsJasaAnalis.CEK = xloop.CEK
    '                            dsJasaAnalis.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaAnalis.KET = "BAGI RADIOLOGI"
    '                            dsJasaAnalis.TUJUAN = xloop.TUJUAN
    '                            dsJasaAnalis.NOSEP = xloop.NOSEP
    '                            dsJasaAnalis.RUGI = xloop.RUGI
    '                            dsJasaAnalis.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaAnalis.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaAnalis.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaAnalis.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaAnalis.NAMA = xloop.NAMA
    '                            dsJasaAnalis.NAME_DISPLAY = "Radiografer"
    '                            dsJasaAnalis.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaAnalis.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaAnalis.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaAnalis.BAGIJASA = Sisa * (JasaAnalis / Total)
    '                                dsJasaAnalis.SISAPAKET = 0
    '                            Else
    '                                dsJasaAnalis.BAGIJASA = JasaAnalis
    '                                dsJasaAnalis.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaAnalis)

    '                            'umum %
    '                            dsJasaPerawat.CEK = xloop.CEK
    '                            dsJasaPerawat.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaPerawat.KET = "BAGI PERAWAT"
    '                            dsJasaPerawat.TUJUAN = xloop.TUJUAN
    '                            dsJasaPerawat.NOSEP = xloop.NOSEP
    '                            dsJasaPerawat.RUGI = xloop.RUGI
    '                            dsJasaPerawat.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaPerawat.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaPerawat.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaPerawat.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaPerawat.NAMA = xloop.NAMA
    '                            dsJasaPerawat.NAME_DISPLAY = xloop.TUJUAN
    '                            dsJasaPerawat.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaPerawat.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaPerawat.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaPerawat.BAGIJASA = Sisa * (JasaPerawat / Total)
    '                                dsJasaPerawat.SISAPAKET = 0
    '                            Else
    '                                dsJasaPerawat.BAGIJASA = JasaPerawat
    '                                dsJasaPerawat.SISAPAKET = xloop.TOTAL_TARIF - (SisaPaket + Total)
    '                            End If

    '                            listHasil.Add(dsJasaPerawat)

    '                        ElseIf xloop.CEK = "13" Then
    '                            Dim dsJasaDokterSpesialis As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaDokterUmum As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaDokterRad As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaDokterLab As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaRadiografer As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaAnalis As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaPerawat As New DataAccess.R_DATA_HASIL

    '                            Dim JasaDokterSpesialis As Decimal = 20000
    '                            Dim JasaDokterUmum As Decimal = 20000
    '                            Dim JasaDokterRad As Decimal = 10000
    '                            Dim JasaDokterLab As Decimal = 10000
    '                            Dim JasaRadiografer As Decimal = 3000
    '                            Dim JasaAnalis As Decimal = 3000
    '                            Dim JasaPerawat As Decimal = 4000

    '                            Dim dsJasaAdministrasi As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaApoteker As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaInputBilling As New DataAccess.R_DATA_HASIL
    '                            Dim JasaAdministrasi As Decimal = 5000
    '                            Dim JasaApoteker As Decimal = 1500
    '                            Dim JasaInputBilling As Decimal = 500

    '                            Dim Total As Decimal = JasaDokterSpesialis + JasaDokterUmum + JasaDokterRad + JasaDokterLab + JasaRadiografer + JasaAnalis + JasaPerawat + JasaAdministrasi + JasaApoteker + JasaInputBilling

    '                            'Admisitrasi %
    '                            dsJasaAdministrasi.CEK = xloop.CEK
    '                            dsJasaAdministrasi.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaAdministrasi.KET = "BAGI ADMINISTRASI"
    '                            dsJasaAdministrasi.TUJUAN = xloop.TUJUAN
    '                            dsJasaAdministrasi.NOSEP = xloop.NOSEP
    '                            dsJasaAdministrasi.RUGI = xloop.RUGI
    '                            dsJasaAdministrasi.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaAdministrasi.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaAdministrasi.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaAdministrasi.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaAdministrasi.NAMA = xloop.NAMA
    '                            dsJasaAdministrasi.NAME_DISPLAY = "Administrasi"
    '                            dsJasaAdministrasi.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaAdministrasi.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaAdministrasi.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaAdministrasi.BAGIJASA = Sisa * (JasaAdministrasi / Total)
    '                                dsJasaAdministrasi.SISAPAKET = 0
    '                            Else
    '                                dsJasaAdministrasi.BAGIJASA = JasaAdministrasi
    '                                dsJasaAdministrasi.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaAdministrasi)

    '                            'Apoteker %
    '                            dsJasaApoteker.CEK = xloop.CEK
    '                            dsJasaApoteker.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaApoteker.KET = "BAGI APOTEKER"
    '                            dsJasaApoteker.TUJUAN = xloop.TUJUAN
    '                            dsJasaApoteker.NOSEP = xloop.NOSEP
    '                            dsJasaApoteker.RUGI = xloop.RUGI
    '                            dsJasaApoteker.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaApoteker.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaApoteker.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaApoteker.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaApoteker.NAMA = xloop.NAMA
    '                            dsJasaApoteker.NAME_DISPLAY = "Apoteker"
    '                            dsJasaApoteker.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaApoteker.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaApoteker.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaApoteker.BAGIJASA = Sisa * (JasaApoteker / Total)
    '                                dsJasaApoteker.SISAPAKET = 0
    '                            Else
    '                                dsJasaApoteker.BAGIJASA = JasaApoteker
    '                                dsJasaApoteker.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaApoteker)

    '                            'Input billing %
    '                            dsJasaInputBilling.CEK = xloop.CEK
    '                            dsJasaInputBilling.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaInputBilling.KET = "BAGI INPUT BILLING"
    '                            dsJasaInputBilling.TUJUAN = xloop.TUJUAN
    '                            dsJasaInputBilling.NOSEP = xloop.NOSEP
    '                            dsJasaInputBilling.RUGI = xloop.RUGI
    '                            dsJasaInputBilling.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaInputBilling.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaInputBilling.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaInputBilling.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaInputBilling.NAMA = xloop.NAMA
    '                            dsJasaInputBilling.NAME_DISPLAY = "Input Billing"
    '                            dsJasaInputBilling.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaInputBilling.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaInputBilling.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaInputBilling.BAGIJASA = Sisa * (JasaInputBilling / Total)
    '                                dsJasaInputBilling.SISAPAKET = 0
    '                            Else
    '                                dsJasaInputBilling.BAGIJASA = JasaInputBilling
    '                                dsJasaInputBilling.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaInputBilling)


    '                            'dpjp %
    '                            dsJasaDokterSpesialis.CEK = xloop.CEK
    '                            dsJasaDokterSpesialis.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaDokterSpesialis.KET = "BAGI DPJP"
    '                            dsJasaDokterSpesialis.TUJUAN = xloop.TUJUAN
    '                            dsJasaDokterSpesialis.NOSEP = xloop.NOSEP
    '                            dsJasaDokterSpesialis.RUGI = xloop.RUGI
    '                            dsJasaDokterSpesialis.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaDokterSpesialis.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaDokterSpesialis.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaDokterSpesialis.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaDokterSpesialis.NAMA = xloop.NAMA
    '                            dsJasaDokterSpesialis.NAME_DISPLAY = xloop.DOKTER_DPJP
    '                            dsJasaDokterSpesialis.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaDokterSpesialis.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaDokterSpesialis.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaDokterSpesialis.BAGIJASA = Sisa * (JasaDokterSpesialis / Total)
    '                                dsJasaDokterSpesialis.SISAPAKET = 0
    '                            Else
    '                                dsJasaDokterSpesialis.BAGIJASA = JasaDokterSpesialis
    '                                dsJasaDokterSpesialis.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaDokterSpesialis)

    '                            'umum %
    '                            dsJasaDokterUmum.CEK = xloop.CEK
    '                            dsJasaDokterUmum.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaDokterUmum.KET = "BAGI DELAGASI"
    '                            dsJasaDokterUmum.TUJUAN = xloop.TUJUAN
    '                            dsJasaDokterUmum.NOSEP = xloop.NOSEP
    '                            dsJasaDokterUmum.RUGI = xloop.RUGI
    '                            dsJasaDokterUmum.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaDokterUmum.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaDokterUmum.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaDokterUmum.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaDokterUmum.NAMA = xloop.NAMA
    '                            dsJasaDokterUmum.NAME_DISPLAY = xloop.DOKTER
    '                            dsJasaDokterUmum.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaDokterUmum.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaDokterUmum.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaDokterUmum.BAGIJASA = Sisa * (JasaDokterUmum / Total)
    '                                dsJasaDokterUmum.SISAPAKET = 0
    '                            Else
    '                                dsJasaDokterUmum.BAGIJASA = JasaDokterUmum
    '                                dsJasaDokterUmum.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaDokterUmum)

    '                            'umum %
    '                            dsJasaDokterRad.CEK = xloop.CEK
    '                            dsJasaDokterRad.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaDokterRad.KET = "BAGI RADIOLOGI"
    '                            dsJasaDokterRad.TUJUAN = xloop.TUJUAN
    '                            dsJasaDokterRad.NOSEP = xloop.NOSEP
    '                            dsJasaDokterRad.RUGI = xloop.RUGI
    '                            dsJasaDokterRad.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaDokterRad.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaDokterRad.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaDokterRad.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaDokterRad.NAMA = xloop.NAMA
    '                            dsJasaDokterRad.NAME_DISPLAY = "dr. Radiologi"
    '                            dsJasaDokterRad.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaDokterRad.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaDokterRad.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaDokterRad.BAGIJASA = Sisa * (JasaDokterRad / Total)
    '                                dsJasaDokterRad.SISAPAKET = 0
    '                            Else
    '                                dsJasaDokterRad.BAGIJASA = JasaDokterRad
    '                                dsJasaDokterRad.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaDokterRad)

    '                            'umum %
    '                            dsJasaDokterLab.CEK = xloop.CEK
    '                            dsJasaDokterLab.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaDokterLab.KET = "BAGI LABORATORIUM"
    '                            dsJasaDokterLab.TUJUAN = xloop.TUJUAN
    '                            dsJasaDokterLab.NOSEP = xloop.NOSEP
    '                            dsJasaDokterLab.RUGI = xloop.RUGI
    '                            dsJasaDokterLab.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaDokterLab.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaDokterLab.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaDokterLab.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaDokterLab.NAMA = xloop.NAMA
    '                            dsJasaDokterLab.NAME_DISPLAY = "dr. Laboratorium"
    '                            dsJasaDokterLab.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaDokterLab.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaDokterLab.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaDokterLab.BAGIJASA = Sisa * (JasaDokterLab / Total)
    '                                dsJasaDokterLab.SISAPAKET = 0
    '                            Else
    '                                dsJasaDokterLab.BAGIJASA = JasaDokterLab
    '                                dsJasaDokterLab.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaDokterLab)


    '                            'umum %
    '                            dsJasaRadiografer.CEK = xloop.CEK
    '                            dsJasaRadiografer.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaRadiografer.KET = "BAGI RADIOLOGI"
    '                            dsJasaRadiografer.TUJUAN = xloop.TUJUAN
    '                            dsJasaRadiografer.NOSEP = xloop.NOSEP
    '                            dsJasaRadiografer.RUGI = xloop.RUGI
    '                            dsJasaRadiografer.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaRadiografer.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaRadiografer.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaRadiografer.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaRadiografer.NAMA = xloop.NAMA
    '                            dsJasaRadiografer.NAME_DISPLAY = "Radiografer"
    '                            dsJasaRadiografer.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaRadiografer.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaRadiografer.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaRadiografer.BAGIJASA = Sisa * (JasaRadiografer / Total)
    '                                dsJasaRadiografer.SISAPAKET = 0
    '                            Else
    '                                dsJasaRadiografer.BAGIJASA = JasaRadiografer
    '                                dsJasaRadiografer.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaRadiografer)

    '                            'umum %
    '                            dsJasaAnalis.CEK = xloop.CEK
    '                            dsJasaAnalis.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaAnalis.KET = "BAGI LABORATORIUM"
    '                            dsJasaAnalis.TUJUAN = xloop.TUJUAN
    '                            dsJasaAnalis.NOSEP = xloop.NOSEP
    '                            dsJasaAnalis.RUGI = xloop.RUGI
    '                            dsJasaAnalis.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaAnalis.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaAnalis.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaAnalis.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaAnalis.NAMA = xloop.NAMA
    '                            dsJasaAnalis.NAME_DISPLAY = "Analis"
    '                            dsJasaAnalis.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaAnalis.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaAnalis.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaAnalis.BAGIJASA = Sisa * (JasaAnalis / Total)
    '                                dsJasaAnalis.SISAPAKET = 0
    '                            Else
    '                                dsJasaAnalis.BAGIJASA = JasaAnalis
    '                                dsJasaAnalis.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaAnalis)

    '                            'umum %
    '                            dsJasaPerawat.CEK = xloop.CEK
    '                            dsJasaPerawat.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaPerawat.KET = "BAGI PERAWAT"
    '                            dsJasaPerawat.TUJUAN = xloop.TUJUAN
    '                            dsJasaPerawat.NOSEP = xloop.NOSEP
    '                            dsJasaPerawat.RUGI = xloop.RUGI
    '                            dsJasaPerawat.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaPerawat.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaPerawat.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaPerawat.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaPerawat.NAMA = xloop.NAMA
    '                            dsJasaPerawat.NAME_DISPLAY = xloop.TUJUAN
    '                            dsJasaPerawat.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaPerawat.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaPerawat.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaPerawat.BAGIJASA = Sisa * (JasaPerawat / Total)
    '                                dsJasaPerawat.SISAPAKET = 0
    '                            Else
    '                                dsJasaPerawat.BAGIJASA = JasaPerawat
    '                                dsJasaPerawat.SISAPAKET = xloop.TOTAL_TARIF - (SisaPaket + Total)
    '                            End If

    '                            listHasil.Add(dsJasaPerawat)

    '                        ElseIf xloop.CEK = "14" Then
    '                            Dim dsJasaDokter As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaPelaksana As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaAdministrasi As New DataAccess.R_DATA_HASIL

    '                            Dim JasaDPJP As Decimal = 20000
    '                            Dim JasaPelaksana As Decimal = 50000
    '                            Dim JasaAdministrasi As Decimal = 5000

    '                            'Dim dsJasaAdministrasi As New DataAccess.R_DATA_HASIL
    '                            'Dim dsJasaApoteker As New DataAccess.R_DATA_HASIL
    '                            'Dim dsJasaInputBilling As New DataAccess.R_DATA_HASIL
    '                            'Dim JasaAdministrasi As Decimal = 5000
    '                            'Dim JasaApoteker As Decimal = 1500
    '                            'Dim JasaInputBilling As Decimal = 500

    '                            Dim Total As Decimal = JasaDPJP + JasaPelaksana + JasaAdministrasi

    '                            'Admisitrasi %
    '                            dsJasaAdministrasi.CEK = xloop.CEK
    '                            dsJasaAdministrasi.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaAdministrasi.KET = "BAGI ADMINISTRASI"
    '                            dsJasaAdministrasi.TUJUAN = xloop.TUJUAN
    '                            dsJasaAdministrasi.NOSEP = xloop.NOSEP
    '                            dsJasaAdministrasi.RUGI = xloop.RUGI
    '                            dsJasaAdministrasi.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaAdministrasi.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaAdministrasi.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaAdministrasi.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaAdministrasi.NAMA = xloop.NAMA
    '                            dsJasaAdministrasi.NAME_DISPLAY = "Administrasi"
    '                            dsJasaAdministrasi.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaAdministrasi.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaAdministrasi.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaAdministrasi.BAGIJASA = Sisa * (JasaAdministrasi / Total)
    '                                dsJasaAdministrasi.SISAPAKET = 0
    '                            Else
    '                                dsJasaAdministrasi.BAGIJASA = JasaAdministrasi
    '                                dsJasaAdministrasi.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaAdministrasi)

    '                            'dpjp %
    '                            dsJasaDokter.CEK = xloop.CEK
    '                            dsJasaDokter.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaDokter.KET = "BAGI DELEGASI"
    '                            dsJasaDokter.TUJUAN = xloop.TUJUAN
    '                            dsJasaDokter.NOSEP = xloop.NOSEP
    '                            dsJasaDokter.RUGI = xloop.RUGI
    '                            dsJasaDokter.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaDokter.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaDokter.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaDokter.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaDokter.NAMA = xloop.NAMA
    '                            dsJasaDokter.NAME_DISPLAY = xloop.DOKTER_DPJP
    '                            dsJasaDokter.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaDokter.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaDokter.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaDokter.BAGIJASA = Sisa * (JasaDPJP / Total)
    '                                dsJasaDokter.SISAPAKET = 0
    '                            Else
    '                                dsJasaDokter.BAGIJASA = JasaDPJP
    '                                dsJasaDokter.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaDokter)

    '                            'perawat %
    '                            dsJasaPelaksana.CEK = xloop.CEK
    '                            dsJasaPelaksana.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaPelaksana.KET = "BAGI PERAWAT"
    '                            dsJasaPelaksana.TUJUAN = xloop.TUJUAN
    '                            dsJasaPelaksana.NOSEP = xloop.NOSEP
    '                            dsJasaPelaksana.RUGI = xloop.RUGI
    '                            dsJasaPelaksana.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaPelaksana.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaPelaksana.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaPelaksana.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaPelaksana.NAMA = xloop.NAMA
    '                            dsJasaPelaksana.NAME_DISPLAY = "Perawat"
    '                            dsJasaPelaksana.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaPelaksana.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaPelaksana.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaPelaksana.BAGIJASA = Sisa * (JasaPelaksana / Total)
    '                                dsJasaPelaksana.SISAPAKET = 0
    '                            Else
    '                                dsJasaPelaksana.BAGIJASA = JasaPelaksana
    '                                dsJasaPelaksana.SISAPAKET = xloop.TOTAL_TARIF - (SisaPaket + Total)
    '                            End If

    '                            listHasil.Add(dsJasaPelaksana)

    '                        ElseIf xloop.CEK = "15" Then
    '                            Dim dsJasaDokterZul As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaDokterJhoni As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaDokterLaila As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaDokterHayat As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaPerawat As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaAdministrasi As New DataAccess.R_DATA_HASIL

    '                            Dim JasaDokterZul As Decimal = 35060
    '                            Dim JasaDokterJhoni As Decimal = 10000
    '                            Dim JasaDokterLaila As Decimal = 23300
    '                            Dim JasaDokterHayat As Decimal = 29640
    '                            Dim JasaPerawat As Decimal = 100000
    '                            Dim JasaAdministrasi As Decimal = 5000

    '                            Dim Total As Decimal = JasaDokterZul + JasaDokterJhoni + JasaDokterLaila + JasaDokterHayat + JasaAdministrasi + JasaPerawat

    '                            'Admisitrasi %
    '                            dsJasaAdministrasi.CEK = xloop.CEK
    '                            dsJasaAdministrasi.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaAdministrasi.KET = "BAGI ADMINISTRASI"
    '                            dsJasaAdministrasi.TUJUAN = xloop.TUJUAN
    '                            dsJasaAdministrasi.NOSEP = xloop.NOSEP
    '                            dsJasaAdministrasi.RUGI = xloop.RUGI
    '                            dsJasaAdministrasi.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaAdministrasi.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaAdministrasi.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaAdministrasi.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaAdministrasi.NAMA = xloop.NAMA
    '                            dsJasaAdministrasi.NAME_DISPLAY = "Administrasi"
    '                            dsJasaAdministrasi.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaAdministrasi.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaAdministrasi.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaAdministrasi.BAGIJASA = Sisa * (JasaAdministrasi / Total)
    '                                dsJasaAdministrasi.SISAPAKET = 0
    '                            Else
    '                                dsJasaAdministrasi.BAGIJASA = JasaAdministrasi
    '                                dsJasaAdministrasi.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaAdministrasi)

    '                            'dpjp %
    '                            dsJasaDokterZul.CEK = xloop.CEK
    '                            dsJasaDokterZul.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaDokterZul.KET = "BAGI DPJP"
    '                            dsJasaDokterZul.TUJUAN = xloop.TUJUAN
    '                            dsJasaDokterZul.NOSEP = xloop.NOSEP
    '                            dsJasaDokterZul.RUGI = xloop.RUGI
    '                            dsJasaDokterZul.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaDokterZul.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaDokterZul.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaDokterZul.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaDokterZul.NAMA = xloop.NAMA
    '                            dsJasaDokterZul.NAME_DISPLAY = "Zulkarnaen, dr. SpPD."
    '                            dsJasaDokterZul.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaDokterZul.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaDokterZul.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaDokterZul.BAGIJASA = Sisa * (JasaDokterZul / Total)
    '                                dsJasaDokterZul.SISAPAKET = 0
    '                            Else
    '                                dsJasaDokterZul.BAGIJASA = JasaDokterZul
    '                                dsJasaDokterZul.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaDokterZul)

    '                            'dpjp %
    '                            dsJasaDokterJhoni.CEK = xloop.CEK
    '                            dsJasaDokterJhoni.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaDokterJhoni.KET = "BAGI SUPERVISI"
    '                            dsJasaDokterJhoni.TUJUAN = xloop.TUJUAN
    '                            dsJasaDokterJhoni.NOSEP = xloop.NOSEP
    '                            dsJasaDokterJhoni.RUGI = xloop.RUGI
    '                            dsJasaDokterJhoni.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaDokterJhoni.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaDokterJhoni.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaDokterJhoni.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaDokterJhoni.NAMA = xloop.NAMA
    '                            dsJasaDokterJhoni.NAME_DISPLAY = "Jhoni dr, Sp.KGH"
    '                            dsJasaDokterJhoni.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaDokterJhoni.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaDokterJhoni.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaDokterJhoni.BAGIJASA = Sisa * (JasaDokterJhoni / Total)
    '                                dsJasaDokterJhoni.SISAPAKET = 0
    '                            Else
    '                                dsJasaDokterJhoni.BAGIJASA = JasaDokterJhoni
    '                                dsJasaDokterJhoni.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaDokterJhoni)

    '                            'dpjp %
    '                            dsJasaDokterLaila.CEK = xloop.CEK
    '                            dsJasaDokterLaila.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaDokterLaila.KET = "BAGI PELAKSANA"
    '                            dsJasaDokterLaila.TUJUAN = xloop.TUJUAN
    '                            dsJasaDokterLaila.NOSEP = xloop.NOSEP
    '                            dsJasaDokterLaila.RUGI = xloop.RUGI
    '                            dsJasaDokterLaila.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaDokterLaila.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaDokterLaila.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaDokterLaila.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaDokterLaila.NAMA = xloop.NAMA
    '                            dsJasaDokterLaila.NAME_DISPLAY = "Lailatul Qadriyah, dr"
    '                            dsJasaDokterLaila.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaDokterLaila.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaDokterLaila.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaDokterLaila.BAGIJASA = Sisa * (JasaDokterLaila / Total)
    '                                dsJasaDokterLaila.SISAPAKET = 0
    '                            Else
    '                                dsJasaDokterLaila.BAGIJASA = JasaDokterLaila
    '                                dsJasaDokterLaila.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaDokterLaila)

    '                            'dpjp %
    '                            dsJasaDokterHayat.CEK = xloop.CEK
    '                            dsJasaDokterHayat.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaDokterHayat.KET = "BAGI KEJIWAAN"
    '                            dsJasaDokterHayat.TUJUAN = xloop.TUJUAN
    '                            dsJasaDokterHayat.NOSEP = xloop.NOSEP
    '                            dsJasaDokterHayat.RUGI = xloop.RUGI
    '                            dsJasaDokterHayat.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaDokterHayat.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaDokterHayat.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaDokterHayat.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaDokterHayat.NAMA = xloop.NAMA
    '                            dsJasaDokterHayat.NAME_DISPLAY = "Hayat Amin, dr., Sp.KJ"
    '                            dsJasaDokterHayat.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaDokterHayat.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaDokterHayat.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaDokterHayat.BAGIJASA = Sisa * (JasaDokterHayat / Total)
    '                                dsJasaDokterHayat.SISAPAKET = 0
    '                            Else
    '                                dsJasaDokterHayat.BAGIJASA = JasaDokterHayat
    '                                dsJasaDokterHayat.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaDokterHayat)

    '                            'dpjp %
    '                            dsJasaPerawat.CEK = xloop.CEK
    '                            dsJasaPerawat.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaPerawat.KET = "BAGI PERAWAT"
    '                            dsJasaPerawat.TUJUAN = xloop.TUJUAN
    '                            dsJasaPerawat.NOSEP = xloop.NOSEP
    '                            dsJasaPerawat.RUGI = xloop.RUGI
    '                            dsJasaPerawat.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaPerawat.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaPerawat.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaPerawat.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaPerawat.NAMA = xloop.NAMA
    '                            dsJasaPerawat.NAME_DISPLAY = "Perawat"
    '                            dsJasaPerawat.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaPerawat.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaPerawat.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaPerawat.BAGIJASA = Sisa * (JasaPerawat / Total)
    '                                dsJasaPerawat.SISAPAKET = 0
    '                            Else
    '                                dsJasaPerawat.BAGIJASA = JasaPerawat
    '                                dsJasaPerawat.SISAPAKET = xloop.TOTAL_TARIF - (SisaPaket + Total)
    '                            End If

    '                            listHasil.Add(dsJasaPerawat)

    '                        ElseIf xloop.CEK = "16" Then
    '                            Dim dsJasaDokterManan1 As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaDokterManan2 As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaDokterHeriyadi As New DataAccess.R_DATA_HASIL
    '                            Dim dsJasaPerawat As New DataAccess.R_DATA_HASIL

    '                            Dim JasaDokterManan1 As Decimal = 54000
    '                            Dim JasaDokterManan2 As Decimal = 6000
    '                            Dim JasaDokterHeriyadi As Decimal = 40000
    '                            Dim JasaPerawat As Decimal = 20000

    '                            Dim Total As Decimal = JasaDokterManan1 + JasaDokterManan2 + JasaDokterHeriyadi + JasaPerawat

    '                            'dpjp %
    '                            dsJasaDokterManan1.CEK = xloop.CEK
    '                            dsJasaDokterManan1.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaDokterManan1.KET = "BAGI DPJP"
    '                            dsJasaDokterManan1.TUJUAN = xloop.TUJUAN
    '                            dsJasaDokterManan1.NOSEP = xloop.NOSEP
    '                            dsJasaDokterManan1.RUGI = xloop.RUGI
    '                            dsJasaDokterManan1.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaDokterManan1.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaDokterManan1.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaDokterManan1.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaDokterManan1.NAMA = xloop.NAMA
    '                            dsJasaDokterManan1.NAME_DISPLAY = "Manan A,dr Spa (1)"
    '                            dsJasaDokterManan1.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaDokterManan1.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaDokterManan1.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaDokterManan1.BAGIJASA = Sisa * (JasaDokterManan1 / Total)
    '                                dsJasaDokterManan1.SISAPAKET = 0
    '                            Else
    '                                dsJasaDokterManan1.BAGIJASA = JasaDokterManan1
    '                                dsJasaDokterManan1.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaDokterManan1)

    '                            'dpjp %
    '                            dsJasaDokterManan2.CEK = xloop.CEK
    '                            dsJasaDokterManan2.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaDokterManan2.KET = "BAGI ASKEP"
    '                            dsJasaDokterManan2.TUJUAN = xloop.TUJUAN
    '                            dsJasaDokterManan2.NOSEP = xloop.NOSEP
    '                            dsJasaDokterManan2.RUGI = xloop.RUGI
    '                            dsJasaDokterManan2.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaDokterManan2.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaDokterManan2.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaDokterManan2.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaDokterManan2.NAMA = xloop.NAMA
    '                            dsJasaDokterManan2.NAME_DISPLAY = "Manan A,dr Spa (2)"
    '                            dsJasaDokterManan2.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaDokterManan2.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaDokterManan2.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaDokterManan2.BAGIJASA = Sisa * (JasaDokterManan2 / Total)
    '                                dsJasaDokterManan2.SISAPAKET = 0
    '                            Else
    '                                dsJasaDokterManan2.BAGIJASA = JasaDokterManan2
    '                                dsJasaDokterManan2.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaDokterManan2)


    '                            'dpjp %
    '                            dsJasaDokterHeriyadi.CEK = xloop.CEK
    '                            dsJasaDokterHeriyadi.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaDokterHeriyadi.KET = "BAGI PELAKSANA"
    '                            dsJasaDokterHeriyadi.TUJUAN = xloop.TUJUAN
    '                            dsJasaDokterHeriyadi.NOSEP = xloop.NOSEP
    '                            dsJasaDokterHeriyadi.RUGI = xloop.RUGI
    '                            dsJasaDokterHeriyadi.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaDokterHeriyadi.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaDokterHeriyadi.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaDokterHeriyadi.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaDokterHeriyadi.NAMA = xloop.NAMA
    '                            dsJasaDokterHeriyadi.NAME_DISPLAY = "Heriyandi Hermawan, dr."
    '                            dsJasaDokterHeriyadi.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaDokterHeriyadi.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaDokterHeriyadi.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaDokterHeriyadi.BAGIJASA = Sisa * (JasaDokterHeriyadi / Total)
    '                                dsJasaDokterHeriyadi.SISAPAKET = 0
    '                            Else
    '                                dsJasaDokterHeriyadi.BAGIJASA = JasaDokterHeriyadi
    '                                dsJasaDokterHeriyadi.SISAPAKET = 0
    '                            End If

    '                            listHasil.Add(dsJasaDokterHeriyadi)

    '                            'dpjp %
    '                            dsJasaPerawat.CEK = xloop.CEK
    '                            dsJasaPerawat.TRANSAKSI = xloop.TRANSAKSI
    '                            dsJasaPerawat.KET = "BAGI PERAWAT"
    '                            dsJasaPerawat.TUJUAN = xloop.TUJUAN
    '                            dsJasaPerawat.NOSEP = xloop.NOSEP
    '                            dsJasaPerawat.RUGI = xloop.RUGI
    '                            dsJasaPerawat.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                            dsJasaPerawat.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                            dsJasaPerawat.KDCUSTOMER = xloop.KDCUSTOMER
    '                            dsJasaPerawat.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                            dsJasaPerawat.NAMA = xloop.NAMA
    '                            dsJasaPerawat.NAME_DISPLAY = "Perawat"
    '                            dsJasaPerawat.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                            dsJasaPerawat.TARIF_RS = xloop.TARIF_RS
    '                            dsJasaPerawat.DIAGLIST = xloop.DIAGLIST

    '                            If Sisa < Total Then
    '                                dsJasaPerawat.BAGIJASA = Sisa * (JasaPerawat / Total)
    '                                dsJasaPerawat.SISAPAKET = 0
    '                            Else
    '                                dsJasaPerawat.BAGIJASA = JasaPerawat
    '                                dsJasaPerawat.SISAPAKET = xloop.TOTAL_TARIF - (SisaPaket + Total)
    '                            End If

    '                            listHasil.Add(dsJasaPerawat)
    '                        End If

    '                    End If
    '                Next

    '                If chkGroup.Checked = False Then
    '                    If cboCategory.SelectedIndex = 0 Then
    '                        grd.MainView = grv
    '                        grd.DataSource = listHasil
    '                        grd.ForceInitialize()

    '                    ElseIf cboCategory.SelectedIndex = 1 Then
    '                        grd.MainView = grv
    '                        grd.DataSource = listHasil.Where(Function(x) x.RUGI = False)
    '                        grd.ForceInitialize()

    '                    Else
    '                        grd.MainView = grv
    '                        grd.DataSource = listHasil.Where(Function(x) x.RUGI = True)
    '                        grd.ForceInitialize()

    '                    End If

    '                Else
    '                    Dim Group = From x In listHasil
    '                                Group x By x.CEK, x.RUGI, x.NOSEP, x.TOTAL_TARIF Into total1 = Sum(x.BAGIJASA), total2 = Sum(x.SISAPAKET)
    '                                Select CEK, NOSEP, RUGI, TOTAL_TARIF, TOTAL = total1 + total2, SEL = TOTAL_TARIF - (total1 + total2)


    '                    If cboCategory.SelectedIndex = 0 Then
    '                        grd.MainView = grv
    '                        grd.DataSource = Group
    '                        grd.ForceInitialize()

    '                    ElseIf cboCategory.SelectedIndex = 1 Then
    '                        grd.MainView = grv
    '                        grd.DataSource = Group.Where(Function(x) x.RUGI = False)
    '                        grd.ForceInitialize()

    '                    Else
    '                        grd.MainView = grv
    '                        grd.DataSource = Group.Where(Function(x) x.RUGI = True)
    '                        grd.ForceInitialize()

    '                    End If

    '                End If

    '            Else
    '                Dim dsGroupList = From x In listHasil
    '                                  Group x By x.KET, x.RUGI, x.TUJUAN, x.NAME_DISPLAY Into TOTAL = Sum(x.BAGIJASA)

    '                If cboCategory.SelectedIndex = 0 Then
    '                    grd.MainView = grv
    '                    grd.DataSource = dsGroupList
    '                    grd.ForceInitialize()

    '                ElseIf cboCategory.SelectedIndex = 1 Then
    '                    grd.MainView = grv
    '                    grd.DataSource = dsGroupList.Where(Function(x) x.RUGI = False)
    '                    grd.ForceInitialize()

    '                Else
    '                    grd.MainView = grv
    '                    grd.DataSource = dsGroupList.Where(Function(x) x.RUGI = True)
    '                    grd.ForceInitialize()

    '                End If

    '            End If

    '            fn_LoadFormatDataAll()

    '        ElseIf cboTYPE.SelectedIndex = 2 Then
    '            'Dim listDetailTransaksiAll_02 As New List(Of DataAccess.R_JASA_RUMUS2)
    '            'Dim listDetailTransaksiNONRS As New List(Of DataAccess.R_PARAMETER_HASIL)
    '            'Dim listDetailTransaksiRS As New List(Of DataAccess.R_PARAMETER_HASIL)

    '            'For iLoop As Integer = 0 To ds.Tables("ALL").Rows.Count - 1
    '            '    Dim dsRekap As New DataAccess.R_PARAMETER_HASIL
    '            '    With ds.Tables("ALL")
    '            '        If .Rows(iLoop)("KDUOM") = "NONRS" Then
    '            '            dsRekap.Transaksi = .Rows(iLoop)("NOSEP")
    '            '            dsRekap.Total = CDec(.Rows(iLoop)("TOTALBAGI"))

    '            '            listDetailTransaksiNONRS.Add(dsRekap)
    '            '        End If
    '            '    End With
    '            'Next

    '            'For iLoop As Integer = 0 To ds.Tables("ALL").Rows.Count - 1
    '            '    Dim dsRekap As New DataAccess.R_PARAMETER_HASIL
    '            '    With ds.Tables("ALL")
    '            '        If .Rows(iLoop)("KDUOM") = "RS" Then
    '            '            dsRekap.Transaksi = .Rows(iLoop)("NOSEP")
    '            '            dsRekap.Total = CDec(.Rows(iLoop)("TOTALBAGI"))

    '            '            listDetailTransaksiRS.Add(dsRekap)
    '            '        End If
    '            '    End With
    '            'Next

    '            'Dim ListGroupNONRS = From x In listDetailTransaksiNONRS
    '            '                     Group x By x.Transaksi Into Total = Sum(x.Total)

    '            'Dim ListGroupRS = From x In listDetailTransaksiRS
    '            '                  Group x By x.Transaksi Into Total = Sum(x.Total)


    '            'grd.MainView = grv
    '            'grd.DataSource = ds.Tables("ALL")
    '            'grd.ForceInitialize()

    '            'For iLoop As Integer = 0 To ds.Tables("ALL").Rows.Count - 1
    '            '    Dim dsRekap As New DataAccess.R_JASA_RUMUS2
    '            '    With ds.Tables("ALL")
    '            '        dsRekap.Transaksi = .Rows(iLoop)("NOSEP")
    '            '        dsRekap.RUGI = .Rows(iLoop)("RUGI")
    '            '        dsRekap.ADMISSION_DATE = .Rows(iLoop)("ADMISSION_DATE")
    '            '        dsRekap.KDCUSTOMER = .Rows(iLoop)("KDCUSTOMER")
    '            '        dsRekap.NO_TRANSAKSI = .Rows(iLoop)("NO_TRANSAKSI")
    '            '        dsRekap.NAMA = .Rows(iLoop)("NAMA")
    '            '        dsRekap.DOKTER_DPJP = .Rows(iLoop)("DOKTER_DPJP")
    '            '        dsRekap.DOKTER = .Rows(iLoop)("DOKTER")
    '            '        dsRekap.GROUPTARIF = .Rows(iLoop)("GROUPTARIF")
    '            '        dsRekap.UNIT = .Rows(iLoop)("UNIT")
    '            '        dsRekap.TARIF = .Rows(iLoop)("TARIF")
    '            '        dsRekap.TARIF_RS = CDec(.Rows(iLoop)("TARIF_RS"))
    '            '        dsRekap.TOTAL_TARIF = CDec(.Rows(iLoop)("TOTAL_TARIF"))
    '            '        dsRekap.HARGTARIF = CDec(.Rows(iLoop)("HARGTARIF"))
    '            '        dsRekap.RATE = CDec(.Rows(iLoop)("RATE"))
    '            '        dsRekap.TARIFBAGI = CDec(.Rows(iLoop)("TARIFBAGI"))

    '            '        If CDec(.Rows(iLoop)("TARIF_RS")) < CDec(.Rows(iLoop)("TOTAL_TARIF")) Then
    '            '            dsRekap.TOTALBAGI = CDec(.Rows(iLoop)("TOTALBAGI"))
    '            '            'If .Rows(iLoop)("KDUOM") = "RS" Then
    '            '            '    dsRekap.TOTALBAGI = CDec(.Rows(iLoop)("TOTALBAGI"))
    '            '            'Else
    '            '            '    Dim Transaksi = .Rows(iLoop)("NOSEP")

    '            '            '    Dim Tes1 = CDec(.Rows(iLoop)("TOTALBAGI")) / ListGroupNONRS.FirstOrDefault(Function(x) x.Transaksi = Transaksi).Total
    '            '            '    Dim Tes2 = Tes1 * (CDec(.Rows(iLoop)("TARIF_RS")) - ListGroupRS.FirstOrDefault(Function(x) x.Transaksi = Transaksi).Total)

    '            '            '    dsRekap.TOTALBAGI = Tes2

    '            '            'End If
    '            '        Else
    '            '            If .Rows(iLoop)("KDUOM") = "RS" Then
    '            '                dsRekap.TOTALBAGI = CDec(.Rows(iLoop)("TOTALBAGI"))
    '            '            Else
    '            '                Dim Transaksi = .Rows(iLoop)("NOSEP")

    '            '                Dim Tes1 = CDec(.Rows(iLoop)("TOTALBAGI")) / ListGroupNONRS.FirstOrDefault(Function(x) x.Transaksi = Transaksi).Total
    '            '                Dim Tes2 = Tes1 * (CDec(.Rows(iLoop)("TOTAL_TARIF")) - ListGroupRS.FirstOrDefault(Function(x) x.Transaksi = Transaksi).Total)

    '            '                dsRekap.TOTALBAGI = Tes2

    '            '            End If

    '            '        End If

    '            '        dsRekap.KDUOM = .Rows(iLoop)("KDUOM")
    '            '        dsRekap.KET = .Rows(iLoop)("KET")
    '            '        dsRekap.TINDAKAN = .Rows(iLoop)("TINDAKAN")

    '            '        listDetailTransaksiAll_02.Add(dsRekap)
    '            '    End With
    '            'Next

    '            'If grdKDUOM.Text = String.Empty Then
    '            '    If cboCategory.SelectedIndex = 0 Then
    '            '        grd.MainView = grv
    '            '        grd.DataSource = listDetailTransaksiAll_02
    '            '        grd.ForceInitialize()
    '            '    ElseIf cboCategory.SelectedIndex = 1 Then
    '            '        grd.MainView = grv
    '            '        grd.DataSource = listDetailTransaksiAll_02.Where(Function(x) x.RUGI = False)
    '            '        grd.ForceInitialize()
    '            '    Else
    '            '        grd.MainView = grv
    '            '        grd.DataSource = listDetailTransaksiAll_02.Where(Function(x) x.RUGI = True)
    '            '        grd.ForceInitialize()
    '            '    End If
    '            'Else
    '            '    If cboCategory.SelectedIndex = 0 Then
    '            '        grd.MainView = grv
    '            '        grd.DataSource = listDetailTransaksiAll_02.Where(Function(x) x.KET = grdKDUOM.Text)
    '            '        grd.ForceInitialize()
    '            '    ElseIf cboCategory.SelectedIndex = 1 Then
    '            '        grd.MainView = grv
    '            '        grd.DataSource = listDetailTransaksiAll_02.Where(Function(x) x.RUGI = False And x.KET = grdKDUOM.Text)
    '            '        grd.ForceInitialize()
    '            '    Else
    '            '        grd.MainView = grv
    '            '        grd.DataSource = listDetailTransaksiAll_02.Where(Function(x) x.RUGI = True And x.KET = grdKDUOM.Text)
    '            '        grd.ForceInitialize()
    '            '    End If
    '            'End If

    '            grd.MainView = grv
    '            grd.DataSource = ds.Tables("ALL")
    '            grd.ForceInitialize()

    '            fn_LoadFormatDataAll()
    '        End If

    '    Catch oErr As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
    'Private Sub fn_LoadDataRawatInap()
    '    Try
    '        Dim oConn As New SqlConnection
    '        Dim oComm As New SqlCommand
    '        Dim da As SqlDataAdapter
    '        Dim ds As New DataSet
    '        Dim SQL As String
    '        Dim listDataAwal As New List(Of DataAccess.R_DATA_AWAL)

    '        Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

    '        oConn = New SqlConnection(sConn)
    '        If oConn.State = ConnectionState.Closed Then
    '            oConn.Open()
    '        End If

    '        Dim oJasa As New Finance.clsJasa
    '        Dim dsDatabase = oJasa.GetDataSetting

    '        SQL = dsDatabase.COA_SALES_DEPOSIT

    '        SQL &= " @CATEGORY = " & IIf(cboTYPE.SelectedIndex = 1, IIf(chkUmum.Checked = False, 8, 9), cboTYPE.SelectedIndex) & ", "
    '        SQL &= " @CATEGORY_STRING_1 = " & IIf(grdKDPAYMENTTYPE.Text = String.Empty, "''", grdKDPAYMENTTYPE.EditValue) & ", "
    '        SQL &= " @CATEGORY_STRING_2 = " & IIf(chkUmum.Checked = False, "'BPJS'", "'UMUM'") & ", "
    '        SQL &= " @CATEGORY_STRING_3 = '', "
    '        SQL &= " @DATEFROM = '" & deDATEFrom.DateTime & "', "
    '        SQL &= " @DATETO = '" & deDATETo.DateTime & "' "

    '        oComm.Connection = oConn
    '        oComm.CommandText = SQL
    '        oComm.CommandTimeout = 120
    '        oComm.CommandType = CommandType.Text

    '        da = New SqlDataAdapter(oComm)
    '        da.Fill(ds, "ALL")

    '        If cboTYPE.SelectedIndex = 0 Then
    '            grd.MainView = grv
    '            grd.DataSource = ds.Tables("ALL")
    '            grd.ForceInitialize()

    '        ElseIf cboTYPE.SelectedIndex = 1 Then
    '            For iLoop As Integer = 0 To ds.Tables("ALL").Rows.Count - 1
    '                Dim dsRekap As New DataAccess.R_DATA_AWAL
    '                With ds.Tables("ALL")
    '                    Dim TRANSAKSI As String = ""
    '                    TRANSAKSI = .Rows(iLoop)("TRANSAKSI")

    '                    dsRekap.TRANSAKSI = .Rows(iLoop)("TRANSAKSI")
    '                    dsRekap.TUJUAN = ""
    '                    dsRekap.TIPE_1 = .Rows(iLoop)("TIPE_1")
    '                    dsRekap.TIPE_3 = ""
    '                    dsRekap.TIPE_4 = ""
    '                    dsRekap.TIPE_5 = ""
    '                    dsRekap.NOSEP = .Rows(iLoop)("NOSEP")
    '                    dsRekap.RUGI = .Rows(iLoop)("RUGI")
    '                    dsRekap.SUB_DOKTER_DPJP = .Rows(iLoop)("SUB_DOKTER_DPJP")
    '                    dsRekap.SUB_DOKTER = .Rows(iLoop)("SUB_DOKTER")
    '                    dsRekap.ADMISSION_DATE = .Rows(iLoop)("ADMISSION_DATE")
    '                    dsRekap.DISCHARGE_DATE = .Rows(iLoop)("DISCHARGE_DATE")
    '                    dsRekap.KDCUSTOMER = .Rows(iLoop)("KDCUSTOMER")
    '                    dsRekap.NO_TRANSAKSI = .Rows(iLoop)("NO_TRANSAKSI")
    '                    dsRekap.NAMA = .Rows(iLoop)("NAMA")
    '                    dsRekap.DOKTER_DPJP = .Rows(iLoop)("DOKTER_DPJP")
    '                    dsRekap.DOKTER = .Rows(iLoop)("DOKTER")
    '                    dsRekap.UNIT = .Rows(iLoop)("UNIT")
    '                    dsRekap.GROUPTARIF = .Rows(iLoop)("GROUPTARIF")
    '                    If .Rows(iLoop)("TARIF") = "Visite Dokter Spesialis" Then
    '                        Dim TES As String = ""
    '                        TES = .Rows(iLoop)("DOKTER")
    '                        If TES.Contains("UMUM") Then
    '                            dsRekap.TARIF = "Visite Dokter Umum"
    '                        Else
    '                            dsRekap.TARIF = .Rows(iLoop)("TARIF")
    '                        End If
    '                    Else
    '                        dsRekap.TARIF = .Rows(iLoop)("TARIF")
    '                    End If
    '                    dsRekap.HARGTARIF = CDec(.Rows(iLoop)("HARGTARIF"))
    '                    dsRekap.TOTAL_TARIF = CDec(.Rows(iLoop)("TOTAL_TARIF"))
    '                    dsRekap.TARIF_RS = CDec(.Rows(iLoop)("TARIF_RS"))
    '                    dsRekap.DIAGLIST = .Rows(iLoop)("DIAGLIST")
    '                    dsRekap.TINDAKAN = .Rows(iLoop)("TINDAKAN")
    '                    dsRekap.BAGIRS_30 = CDec(.Rows(iLoop)("BAGIRS_30"))
    '                    dsRekap.BAGIBHP = CDec(.Rows(iLoop)("BAGIBHP"))
    '                    dsRekap.BIAYAOBAT = CDec(.Rows(iLoop)("BIAYAOBAT"))
    '                    dsRekap.TOTALRS = CDec(.Rows(iLoop)("TOTALRS"))
    '                    dsRekap.ISPROSEDUR = CDec(.Rows(iLoop)("ISPROSEDUR"))

    '                    listDataAwal.Add(dsRekap)
    '                End With
    '            Next

    '            Dim dsGroupAll = From x In listDataAwal
    '                             Group x By x.TRANSAKSI Into Total = Sum(x.HARGTARIF)

    '            Dim dsHitungVisiteDokterS = From x In listDataAwal
    '                                        Group By x.TRANSAKSI, x.TARIF Into QTY = Count(x.HARGTARIF)
    '                                        Where TARIF.Contains("Visite Dokter Spesialis")
    '                                        Select New With {
    '                                        Key .TRANSAKSI = TRANSAKSI,
    '                                        Key .VISITEDPJP = TARIF,
    '                                        Key .QTYDPJP = CDec(QTY),
    '                                        Key .VISITEUMUM = "",
    '                                        Key .QTYUMUM = CDec(0)
    '                                        }

    '            Dim dsHitungVisiteDokterU = From x In listDataAwal
    '                                        Group By x.TRANSAKSI, x.TARIF Into QTY = Count(x.HARGTARIF)
    '                                        Where TARIF.Contains("Visite Dokter Umum")
    '                                        Select New With {
    '                                        Key .TRANSAKSI = TRANSAKSI,
    '                                        Key .VISITEDPJP = "",
    '                                        Key .QTYDPJP = CDec(0),
    '                                        Key .VISITEUMUM = TARIF,
    '                                        Key .QTYUMUM = CDec(QTY)
    '                                        }


    '            Dim dsJoin = From x In dsHitungVisiteDokterS
    '                         Join y In dsHitungVisiteDokterU
    '                         On x.TRANSAKSI Equals y.TRANSAKSI
    '                         Select x.TRANSAKSI, x.VISITEDPJP, y.VISITEUMUM, y.QTYUMUM

    '            'Dim dsUnion = dsHitungVisiteDokterS.Union(dsHitungVisiteDokterU)

    '            Dim list As New List(Of DataAccess.R_PARAMETER_NAME_DISPLAY)

    '            For Each xloop In dsGroupAll
    '                Dim dsRekap As New DataAccess.R_PARAMETER_NAME_DISPLAY
    '                dsRekap.Transaksi = xloop.TRANSAKSI

    '                Dim dsCekSpesialisUmum = dsJoin.Where(Function(x) x.TRANSAKSI = xloop.TRANSAKSI)

    '                If dsCekSpesialisUmum.Count > 0 Then
    '                    dsRekap.NAME_DISPLAY_1 = dsCekSpesialisUmum.FirstOrDefault.VISITEDPJP & " " & dsCekSpesialisUmum.FirstOrDefault.VISITEUMUM
    '                    dsRekap.NAME_DISPLAY_2 = dsCekSpesialisUmum.FirstOrDefault.QTYUMUM + 1
    '                    dsRekap.NAME_DISPLAY_3 = 60
    '                    dsRekap.NAME_DISPLAY_4 = 40
    '                Else
    '                    Dim dsSpesialis = dsHitungVisiteDokterS.Where(Function(x) x.TRANSAKSI = xloop.TRANSAKSI)
    '                    If dsSpesialis.Count > 0 Then
    '                        dsRekap.NAME_DISPLAY_1 = dsSpesialis.FirstOrDefault.VISITEDPJP
    '                        dsRekap.NAME_DISPLAY_2 = 0
    '                        dsRekap.NAME_DISPLAY_3 = 100
    '                        dsRekap.NAME_DISPLAY_4 = 0
    '                    Else
    '                        Dim dsUmum = dsHitungVisiteDokterU.Where(Function(x) x.TRANSAKSI = xloop.TRANSAKSI)
    '                        If dsUmum.Count > 0 Then
    '                            dsRekap.NAME_DISPLAY_1 = dsUmum.FirstOrDefault.VISITEUMUM
    '                            dsRekap.NAME_DISPLAY_2 = dsUmum.FirstOrDefault.QTYUMUM + 1
    '                            dsRekap.NAME_DISPLAY_3 = 80
    '                            dsRekap.NAME_DISPLAY_4 = 20
    '                        Else
    '                            dsRekap.NAME_DISPLAY_1 = "TIDAK ADA TRANSAKSI VISITE"
    '                            dsRekap.NAME_DISPLAY_2 = 0
    '                            dsRekap.NAME_DISPLAY_3 = 0
    '                            dsRekap.NAME_DISPLAY_4 = 0
    '                        End If
    '                    End If
    '                    dsRekap.Total = 150000
    '                End If

    '                list.Add(dsRekap)
    '            Next

    '            If cboUom.SelectedIndex = 0 Then
    '                If chkGroup.Checked = False Then
    '                    Dim dsGroup = From x In listDataAwal
    '                                  Group x By x.TRANSAKSI, x.NOSEP, x.RUGI, x.ADMISSION_DATE, x.DISCHARGE_DATE, x.KDCUSTOMER, x.NO_TRANSAKSI, x.NAMA, x.DOKTER_DPJP, x.DOKTER, x.UNIT, x.DIAGLIST, x.TINDAKAN, x.GROUPTARIF, x.TARIF, x.TOTAL_TARIF, x.TARIF_RS, x.BAGIRS_30, x.BAGIBHP, x.BIAYAOBAT, x.TOTALRS Into HARGATARIF = Sum(x.HARGTARIF)
    '                    grd.MainView = grv
    '                    grd.DataSource = dsGroup
    '                    grd.ForceInitialize()
    '                Else
    '                    grd.MainView = grv
    '                    grd.DataSource = list
    '                    grd.ForceInitialize()
    '                End If
    '            ElseIf cboUom.SelectedIndex = 1 Then
    '                Dim dsGroup = From x In listDataAwal.Where(Function(x) x.ISPROSEDUR = 0)
    '                              Group x By x.TRANSAKSI, x.NOSEP, x.RUGI, x.ADMISSION_DATE, x.DISCHARGE_DATE, x.KDCUSTOMER, x.NO_TRANSAKSI, x.NAMA, x.DOKTER_DPJP, x.DOKTER, x.UNIT, x.TINDAKAN, x.GROUPTARIF, x.TARIF, x.DIAGLIST, x.TOTAL_TARIF, x.TARIF_RS Into HARGTARIF = Sum(x.HARGTARIF), BAGIRS_30 = Sum(x.BAGIRS_30), BAGIBHP = Sum(x.BAGIBHP), BIAYAOBAT = Sum(x.BIAYAOBAT), TOTALRS = Sum(x.TOTALRS)
    '                              Select TRANSAKSI, NOSEP, RUGI, ADMISSION_DATE, DISCHARGE_DATE, KDCUSTOMER, NO_TRANSAKSI, NAMA, DOKTER_DPJP, DOKTER, UNIT, TINDAKAN, GROUPTARIF, TARIF, DIAGLIST, TOTAL_TARIF, TARIF_RS, HARGTARIF = CDec(HARGTARIF), BAGIRS_30, BAGIBHP, BIAYAOBAT, TOTALRS


    '                Dim listAll As New List(Of DataAccess.R_JASA_KELINAP)

    '                For Each xloop In dsGroup.Where(Function(x) x.TARIF.Contains("Visite Dokter Spesialis"))
    '                    Dim dsRekap As New DataAccess.R_JASA_KELINAP

    '                    dsRekap.TRANSAKSI = xloop.TRANSAKSI
    '                    dsRekap.NOSEP = xloop.NOSEP
    '                    dsRekap.RUGI = xloop.RUGI
    '                    dsRekap.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                    dsRekap.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                    dsRekap.HITUNGHARI = CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1)
    '                    dsRekap.KDCUSTOMER = xloop.KDCUSTOMER
    '                    dsRekap.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                    dsRekap.NAMA = xloop.NAMA
    '                    dsRekap.NAME_DISPLAY = xloop.DOKTER_DPJP
    '                    dsRekap.UNIT = xloop.UNIT
    '                    dsRekap.TARIF = xloop.TARIF
    '                    dsRekap.DIAGLIST = xloop.DIAGLIST
    '                    dsRekap.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                    dsRekap.TARIF_RS = xloop.TARIF_RS
    '                    dsRekap.BAGIRS_30 = CDec(xloop.TOTAL_TARIF * 30 / 100)
    '                    dsRekap.BAGIBHP = CDec(xloop.TOTAL_TARIF * 35 / 100)
    '                    dsRekap.SISAPAKET = CDec(xloop.TOTAL_TARIF * 35 / 100)
    '                    dsRekap.JASAVISITESPESIALIS = CDec(CDec(IIf(xloop.TARIF.Contains("Visite Dokter Spesialis"), IIf(list.FirstOrDefault(Function(x) x.Transaksi = xloop.TRANSAKSI).NAME_DISPLAY_1.Contains("Visite Dokter Spesialis Visite Dokter Umum"), 150000 * (60 / 100), IIf(list.FirstOrDefault(Function(x) x.Transaksi = xloop.TRANSAKSI).NAME_DISPLAY_1.Contains("Visite Dokter Umum"), 150000 * (20 / 100), 150000)), 0)))
    '                    dsRekap.JASAVISITESUMUM = CDec(0)
    '                    dsRekap.JASAVISITESUMUMSPESIALIS = CDec(IIf(xloop.TARIF.Contains("Visite Dokter Umum"), IIf(list.FirstOrDefault(Function(x) x.Transaksi = xloop.TRANSAKSI).NAME_DISPLAY_1.Contains("Visite Dokter Spesialis Visite Dokter Umum"), 0, IIf(list.FirstOrDefault(Function(x) x.Transaksi = xloop.TRANSAKSI).NAME_DISPLAY_1.Contains("Visite Dokter Umum"), 150000 * (20 / 100) / list.FirstOrDefault(Function(x) x.Transaksi = xloop.TRANSAKSI).NAME_DISPLAY_2, 150000 * (20 / 100) / list.FirstOrDefault(Function(x) x.Transaksi = xloop.TRANSAKSI).NAME_DISPLAY_2)), 0))
    '                    dsRekap.JASARADIOLOGI = CDec(0)
    '                    dsRekap.JASARADIOGRAFER = CDec(0)
    '                    dsRekap.JASALABORATORIUM = CDec(0)
    '                    dsRekap.JASAANALIS = CDec(0)
    '                    dsRekap.JASAAPOTEKER = CDec(0)
    '                    dsRekap.JASAAHLIGIZI = CDec(0)
    '                    dsRekap.JASAINPUBILLING = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANSPESIALIS = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANUMUM = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANIGD = CDec(0)
    '                    dsRekap.JASATINDAKAN = CDec(0)
    '                    dsRekap.JASAOPERATOR = CDec(0)
    '                    dsRekap.JASAANESTESI = CDec(0)
    '                    dsRekap.JASAASISTEN = CDec(0)
    '                    dsRekap.BIDAN_PERAWAT = CDec(0)

    '                    listAll.Add(dsRekap)
    '                Next


    '                For Each xloop In dsGroup.Where(Function(x) x.TARIF.Contains("Visite Dokter Umum"))
    '                    Dim dsRekap As New DataAccess.R_JASA_KELINAP

    '                    dsRekap.TRANSAKSI = xloop.TRANSAKSI
    '                    dsRekap.NOSEP = xloop.NOSEP
    '                    dsRekap.RUGI = xloop.RUGI
    '                    dsRekap.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                    dsRekap.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                    dsRekap.HITUNGHARI = CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1)
    '                    dsRekap.KDCUSTOMER = xloop.KDCUSTOMER
    '                    dsRekap.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                    dsRekap.NAMA = xloop.NAMA
    '                    dsRekap.NAME_DISPLAY = xloop.DOKTER
    '                    dsRekap.UNIT = xloop.UNIT
    '                    dsRekap.TARIF = xloop.TARIF
    '                    dsRekap.DIAGLIST = xloop.DIAGLIST
    '                    dsRekap.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                    dsRekap.TARIF_RS = xloop.TARIF_RS
    '                    dsRekap.BAGIRS_30 = CDec(xloop.TOTAL_TARIF * 30 / 100)
    '                    dsRekap.BAGIBHP = CDec(xloop.TOTAL_TARIF * 35 / 100)
    '                    dsRekap.SISAPAKET = CDec(xloop.TOTAL_TARIF * 35 / 100)
    '                    dsRekap.JASAVISITESPESIALIS = CDec(0)
    '                    dsRekap.JASAVISITESUMUM = CDec(IIf(xloop.TARIF.Contains("Visite Dokter Umum"), IIf(list.FirstOrDefault(Function(x) x.Transaksi = xloop.TRANSAKSI).NAME_DISPLAY_1.Contains("Visite Dokter Spesialis Visite Dokter Umum"), (150000 * (40 / 100)) / list.FirstOrDefault(Function(x) x.Transaksi = xloop.TRANSAKSI).NAME_DISPLAY_2, IIf(list.FirstOrDefault(Function(x) x.Transaksi = xloop.TRANSAKSI).NAME_DISPLAY_1.Contains("Visite Dokter Umum"), 150000 * (80 / 100) / list.FirstOrDefault(Function(x) x.Transaksi = xloop.TRANSAKSI).NAME_DISPLAY_2, 7777)), 0))
    '                    dsRekap.JASAVISITESUMUMSPESIALIS = CDec(IIf(xloop.TARIF.Contains("Visite Dokter Umum"), IIf(list.FirstOrDefault(Function(x) x.Transaksi = xloop.TRANSAKSI).NAME_DISPLAY_1.Contains("Visite Dokter Spesialis Visite Dokter Umum"), 0, IIf(list.FirstOrDefault(Function(x) x.Transaksi = xloop.TRANSAKSI).NAME_DISPLAY_1.Contains("Visite Dokter Umum"), 150000 * (20 / 100) / list.FirstOrDefault(Function(x) x.Transaksi = xloop.TRANSAKSI).NAME_DISPLAY_2, 150000 * (20 / 100) / list.FirstOrDefault(Function(x) x.Transaksi = xloop.TRANSAKSI).NAME_DISPLAY_2)), 0))
    '                    dsRekap.JASARADIOLOGI = CDec(0)
    '                    dsRekap.JASARADIOGRAFER = CDec(0)
    '                    dsRekap.JASALABORATORIUM = CDec(0)
    '                    dsRekap.JASAANALIS = CDec(0)
    '                    dsRekap.JASAAPOTEKER = CDec(0)
    '                    dsRekap.JASAAHLIGIZI = CDec(0)
    '                    dsRekap.JASAINPUBILLING = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANSPESIALIS = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANUMUM = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANIGD = CDec(0)
    '                    dsRekap.JASATINDAKAN = CDec(0)
    '                    dsRekap.JASAOPERATOR = CDec(0)
    '                    dsRekap.JASAANESTESI = CDec(0)
    '                    dsRekap.JASAASISTEN = CDec(0)
    '                    dsRekap.BIDAN_PERAWAT = CDec(0)

    '                    listAll.Add(dsRekap)
    '                Next


    '                For Each xloop In dsGroup.Where(Function(x) x.GROUPTARIF.Contains("RADIOLOGI"))
    '                    Dim dsRekap As New DataAccess.R_JASA_KELINAP

    '                    dsRekap.TRANSAKSI = xloop.TRANSAKSI
    '                    dsRekap.NOSEP = xloop.NOSEP
    '                    dsRekap.RUGI = xloop.RUGI
    '                    dsRekap.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                    dsRekap.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                    dsRekap.HITUNGHARI = CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1)
    '                    dsRekap.KDCUSTOMER = xloop.KDCUSTOMER
    '                    dsRekap.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                    dsRekap.NAMA = xloop.NAMA
    '                    dsRekap.NAME_DISPLAY = "dr. Radiologi"
    '                    dsRekap.UNIT = xloop.UNIT
    '                    dsRekap.TARIF = xloop.TARIF
    '                    dsRekap.DIAGLIST = xloop.DIAGLIST
    '                    dsRekap.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                    dsRekap.TARIF_RS = xloop.TARIF_RS
    '                    dsRekap.BAGIRS_30 = CDec(xloop.TOTAL_TARIF * 30 / 100)
    '                    dsRekap.BAGIBHP = CDec(xloop.TOTAL_TARIF * 35 / 100)
    '                    dsRekap.SISAPAKET = CDec(xloop.TOTAL_TARIF * 35 / 100)
    '                    dsRekap.JASAVISITESPESIALIS = CDec(0)
    '                    dsRekap.JASAVISITESUMUM = CDec(0)
    '                    dsRekap.JASAVISITESUMUMSPESIALIS = CDec(0)
    '                    dsRekap.JASARADIOLOGI = CDec(IIf(CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1) >= 1 And CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1) < 5, (xloop.TOTAL_TARIF * 35 / 100) * 1.6 / 100, IIf(CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1) >= 5 And CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1) < 9, (xloop.TOTAL_TARIF * 35 / 100) * 1.4 / 100, IIf(CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1) >= 9 And CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1) < 12, (xloop.TOTAL_TARIF * 35 / 100) * 1.2 / 100, IIf(CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1) >= 12 And CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1) < 16, (xloop.TOTAL_TARIF * 35 / 100) * 1 / 100, (xloop.TOTAL_TARIF * 35 / 100) * 0.8 / 100)))))
    '                    dsRekap.JASARADIOGRAFER = CDec(0)
    '                    dsRekap.JASALABORATORIUM = CDec(0)
    '                    dsRekap.JASAANALIS = CDec(0)
    '                    dsRekap.JASAAPOTEKER = CDec(0)
    '                    dsRekap.JASAAHLIGIZI = CDec(0)
    '                    dsRekap.JASAINPUBILLING = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANSPESIALIS = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANUMUM = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANIGD = CDec(0)
    '                    dsRekap.JASATINDAKAN = CDec(0)
    '                    dsRekap.JASAOPERATOR = CDec(0)
    '                    dsRekap.JASAANESTESI = CDec(0)
    '                    dsRekap.JASAASISTEN = CDec(0)
    '                    dsRekap.BIDAN_PERAWAT = CDec(0)

    '                    listAll.Add(dsRekap)
    '                Next


    '                For Each xloop In dsGroup.Where(Function(x) x.GROUPTARIF.Contains("RADIOLOGI"))
    '                    Dim dsRekap As New DataAccess.R_JASA_KELINAP

    '                    dsRekap.TRANSAKSI = xloop.TRANSAKSI
    '                    dsRekap.NOSEP = xloop.NOSEP
    '                    dsRekap.RUGI = xloop.RUGI
    '                    dsRekap.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                    dsRekap.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                    dsRekap.HITUNGHARI = CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1)
    '                    dsRekap.KDCUSTOMER = xloop.KDCUSTOMER
    '                    dsRekap.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                    dsRekap.NAMA = xloop.NAMA
    '                    dsRekap.NAME_DISPLAY = "Radiografer"
    '                    dsRekap.UNIT = xloop.UNIT
    '                    dsRekap.TARIF = xloop.TARIF
    '                    dsRekap.DIAGLIST = xloop.DIAGLIST
    '                    dsRekap.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                    dsRekap.TARIF_RS = xloop.TARIF_RS
    '                    dsRekap.BAGIRS_30 = CDec(xloop.TOTAL_TARIF * 30 / 100)
    '                    dsRekap.BAGIBHP = CDec(xloop.TOTAL_TARIF * 35 / 100)
    '                    dsRekap.SISAPAKET = CDec(xloop.TOTAL_TARIF * 35 / 100)
    '                    dsRekap.JASAVISITESPESIALIS = CDec(0)
    '                    dsRekap.JASAVISITESUMUM = CDec(0)
    '                    dsRekap.JASAVISITESUMUMSPESIALIS = CDec(0)
    '                    dsRekap.JASARADIOLOGI = CDec(0)
    '                    dsRekap.JASARADIOGRAFER = CDec(IIf(CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1) >= 1 And CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1) < 5, (xloop.TOTAL_TARIF * 35 / 100) * 1.2 / 100, IIf(CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1) >= 5 And CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1) < 9, (xloop.TOTAL_TARIF * 35 / 100) * 1 / 100, IIf(CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1) >= 9 And CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1) < 12, (xloop.TOTAL_TARIF * 35 / 100) * 0.8 / 100, IIf(CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1) >= 12 And CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1) < 16, (xloop.TOTAL_TARIF * 35 / 100) * 0.6 / 100, (xloop.TOTAL_TARIF * 35 / 100) * 0.4 / 100)))))
    '                    dsRekap.JASALABORATORIUM = CDec(0)
    '                    dsRekap.JASAANALIS = CDec(0)
    '                    dsRekap.JASAAPOTEKER = CDec(0)
    '                    dsRekap.JASAAHLIGIZI = CDec(0)
    '                    dsRekap.JASAINPUBILLING = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANSPESIALIS = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANUMUM = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANIGD = CDec(0)
    '                    dsRekap.JASATINDAKAN = CDec(0)
    '                    dsRekap.JASAOPERATOR = CDec(0)
    '                    dsRekap.JASAANESTESI = CDec(0)
    '                    dsRekap.JASAASISTEN = CDec(0)
    '                    dsRekap.BIDAN_PERAWAT = CDec(0)

    '                    listAll.Add(dsRekap)
    '                Next

    '                For Each xloop In dsGroup.Where(Function(x) x.GROUPTARIF.Contains("LABORATORIUM"))
    '                    Dim dsRekap As New DataAccess.R_JASA_KELINAP

    '                    dsRekap.TRANSAKSI = xloop.TRANSAKSI
    '                    dsRekap.NOSEP = xloop.NOSEP
    '                    dsRekap.RUGI = xloop.RUGI
    '                    dsRekap.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                    dsRekap.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                    dsRekap.HITUNGHARI = CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1)
    '                    dsRekap.KDCUSTOMER = xloop.KDCUSTOMER
    '                    dsRekap.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                    dsRekap.NAMA = xloop.NAMA
    '                    dsRekap.NAME_DISPLAY = "dr. Laboratorium"
    '                    dsRekap.UNIT = xloop.UNIT
    '                    dsRekap.TARIF = xloop.TARIF
    '                    dsRekap.DIAGLIST = xloop.DIAGLIST
    '                    dsRekap.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                    dsRekap.TARIF_RS = xloop.TARIF_RS
    '                    dsRekap.BAGIRS_30 = CDec(xloop.TOTAL_TARIF * 30 / 100)
    '                    dsRekap.BAGIBHP = CDec(xloop.TOTAL_TARIF * 35 / 100)
    '                    dsRekap.SISAPAKET = CDec(xloop.TOTAL_TARIF * 35 / 100)
    '                    dsRekap.JASAVISITESPESIALIS = CDec(0)
    '                    dsRekap.JASAVISITESUMUM = CDec(0)
    '                    dsRekap.JASAVISITESUMUMSPESIALIS = CDec(0)
    '                    dsRekap.JASARADIOLOGI = CDec(0)
    '                    dsRekap.JASARADIOGRAFER = CDec(0)
    '                    dsRekap.JASALABORATORIUM = CDec(IIf(CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1) >= 1 And CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1) < 5, (xloop.TOTAL_TARIF * 35 / 100) * 1.8 / 100, IIf(CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1) >= 5 And CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1) < 9, (xloop.TOTAL_TARIF * 35 / 100) * 1.6 / 100, IIf(CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1) >= 9 And CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1) < 12, (xloop.TOTAL_TARIF * 35 / 100) * 1.4 / 100, IIf(CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1) >= 12 And CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1) < 16, (xloop.TOTAL_TARIF * 35 / 100) * 1.2 / 100, (xloop.TOTAL_TARIF * 35 / 100) * 1 / 100)))))
    '                    dsRekap.JASAANALIS = CDec(0)
    '                    dsRekap.JASAAPOTEKER = CDec(0)
    '                    dsRekap.JASAAHLIGIZI = CDec(0)
    '                    dsRekap.JASAINPUBILLING = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANSPESIALIS = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANUMUM = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANIGD = CDec(0)
    '                    dsRekap.JASATINDAKAN = CDec(0)
    '                    dsRekap.JASAOPERATOR = CDec(0)
    '                    dsRekap.JASAANESTESI = CDec(0)
    '                    dsRekap.JASAASISTEN = CDec(0)
    '                    dsRekap.BIDAN_PERAWAT = CDec(0)

    '                    listAll.Add(dsRekap)
    '                Next

    '                For Each xloop In dsGroup.Where(Function(x) x.GROUPTARIF.Contains("LABORATORIUM"))
    '                    Dim dsRekap As New DataAccess.R_JASA_KELINAP

    '                    dsRekap.TRANSAKSI = xloop.TRANSAKSI
    '                    dsRekap.NOSEP = xloop.NOSEP
    '                    dsRekap.RUGI = xloop.RUGI
    '                    dsRekap.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                    dsRekap.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                    dsRekap.HITUNGHARI = CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1)
    '                    dsRekap.KDCUSTOMER = xloop.KDCUSTOMER
    '                    dsRekap.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                    dsRekap.NAMA = xloop.NAMA
    '                    dsRekap.NAME_DISPLAY = "Analis"
    '                    dsRekap.UNIT = xloop.UNIT
    '                    dsRekap.TARIF = xloop.TARIF
    '                    dsRekap.DIAGLIST = xloop.DIAGLIST
    '                    dsRekap.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                    dsRekap.TARIF_RS = xloop.TARIF_RS
    '                    dsRekap.BAGIRS_30 = CDec(xloop.TOTAL_TARIF * 30 / 100)
    '                    dsRekap.BAGIBHP = CDec(xloop.TOTAL_TARIF * 35 / 100)
    '                    dsRekap.SISAPAKET = CDec(xloop.TOTAL_TARIF * 35 / 100)
    '                    dsRekap.JASAVISITESPESIALIS = CDec(0)
    '                    dsRekap.JASAVISITESUMUM = CDec(0)
    '                    dsRekap.JASAVISITESUMUMSPESIALIS = CDec(0)
    '                    dsRekap.JASARADIOLOGI = CDec(0)
    '                    dsRekap.JASARADIOGRAFER = CDec(0)
    '                    dsRekap.JASALABORATORIUM = CDec(0)
    '                    dsRekap.JASAANALIS = CDec(IIf(CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1) >= 1 And CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1) < 5, (xloop.TOTAL_TARIF * 35 / 100) * 1.4 / 100, IIf(CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1) >= 5 And CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1) < 9, (xloop.TOTAL_TARIF * 35 / 100) * 1.2 / 100, IIf(CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1) >= 9 And CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1) < 12, (xloop.TOTAL_TARIF * 35 / 100) * 1 / 100, IIf(CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1) >= 12 And CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1) < 16, (xloop.TOTAL_TARIF * 35 / 100) * 0.8 / 100, (xloop.TOTAL_TARIF * 35 / 100) * 0.6 / 100)))))
    '                    dsRekap.JASAAPOTEKER = CDec(0)
    '                    dsRekap.JASAAHLIGIZI = CDec(0)
    '                    dsRekap.JASAINPUBILLING = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANSPESIALIS = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANUMUM = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANIGD = CDec(0)
    '                    dsRekap.JASATINDAKAN = CDec(0)
    '                    dsRekap.JASAOPERATOR = CDec(0)
    '                    dsRekap.JASAANESTESI = CDec(0)
    '                    dsRekap.JASAASISTEN = CDec(0)
    '                    dsRekap.BIDAN_PERAWAT = CDec(0)

    '                    listAll.Add(dsRekap)
    '                Next

    '                For Each xloop In dsGroup.Where(Function(x) x.GROUPTARIF.Contains("OBAT"))
    '                    Dim dsRekap As New DataAccess.R_JASA_KELINAP

    '                    dsRekap.TRANSAKSI = xloop.TRANSAKSI
    '                    dsRekap.NOSEP = xloop.NOSEP
    '                    dsRekap.RUGI = xloop.RUGI
    '                    dsRekap.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                    dsRekap.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                    dsRekap.HITUNGHARI = CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1)
    '                    dsRekap.KDCUSTOMER = xloop.KDCUSTOMER
    '                    dsRekap.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                    dsRekap.NAMA = xloop.NAMA
    '                    dsRekap.NAME_DISPLAY = "Apoteker"
    '                    dsRekap.UNIT = xloop.UNIT
    '                    dsRekap.TARIF = xloop.TARIF
    '                    dsRekap.DIAGLIST = xloop.DIAGLIST
    '                    dsRekap.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                    dsRekap.TARIF_RS = xloop.TARIF_RS
    '                    dsRekap.BAGIRS_30 = CDec(xloop.TOTAL_TARIF * 30 / 100)
    '                    dsRekap.BAGIBHP = CDec(xloop.TOTAL_TARIF * 35 / 100)
    '                    dsRekap.SISAPAKET = CDec(xloop.TOTAL_TARIF * 35 / 100)
    '                    dsRekap.JASAVISITESPESIALIS = CDec(0)
    '                    dsRekap.JASAVISITESUMUM = CDec(0)
    '                    dsRekap.JASAVISITESUMUMSPESIALIS = CDec(0)
    '                    dsRekap.JASARADIOLOGI = CDec(0)
    '                    dsRekap.JASARADIOGRAFER = CDec(0)
    '                    dsRekap.JASALABORATORIUM = CDec(0)
    '                    dsRekap.JASAANALIS = CDec(0)
    '                    dsRekap.JASAAPOTEKER = CDec(10000)
    '                    dsRekap.JASAAHLIGIZI = CDec(0)
    '                    dsRekap.JASAINPUBILLING = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANSPESIALIS = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANUMUM = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANIGD = CDec(0)
    '                    dsRekap.JASATINDAKAN = CDec(0)
    '                    dsRekap.JASAOPERATOR = CDec(0)
    '                    dsRekap.JASAANESTESI = CDec(0)
    '                    dsRekap.JASAASISTEN = CDec(0)
    '                    dsRekap.BIDAN_PERAWAT = CDec(0)

    '                    listAll.Add(dsRekap)
    '                Next

    '                For Each xloop In dsGroup.Where(Function(x) x.TARIF = "Sewa Ruang Perawatan")
    '                    Dim dsRekap As New DataAccess.R_JASA_KELINAP

    '                    dsRekap.TRANSAKSI = xloop.TRANSAKSI
    '                    dsRekap.NOSEP = xloop.NOSEP
    '                    dsRekap.RUGI = xloop.RUGI
    '                    dsRekap.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                    dsRekap.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                    dsRekap.HITUNGHARI = CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1)
    '                    dsRekap.KDCUSTOMER = xloop.KDCUSTOMER
    '                    dsRekap.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                    dsRekap.NAMA = xloop.NAMA
    '                    dsRekap.NAME_DISPLAY = "Ahli Gizi"
    '                    dsRekap.UNIT = xloop.UNIT
    '                    dsRekap.TARIF = xloop.TARIF
    '                    dsRekap.DIAGLIST = xloop.DIAGLIST
    '                    dsRekap.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                    dsRekap.TARIF_RS = xloop.TARIF_RS
    '                    dsRekap.BAGIRS_30 = CDec(xloop.TOTAL_TARIF * 30 / 100)
    '                    dsRekap.BAGIBHP = CDec(xloop.TOTAL_TARIF * 35 / 100)
    '                    dsRekap.SISAPAKET = CDec(xloop.TOTAL_TARIF * 35 / 100)
    '                    dsRekap.JASAVISITESPESIALIS = CDec(0)
    '                    dsRekap.JASAVISITESUMUM = CDec(0)
    '                    dsRekap.JASAVISITESUMUMSPESIALIS = CDec(0)
    '                    dsRekap.JASARADIOLOGI = CDec(0)
    '                    dsRekap.JASARADIOGRAFER = CDec(0)
    '                    dsRekap.JASALABORATORIUM = CDec(0)
    '                    dsRekap.JASAANALIS = CDec(0)
    '                    dsRekap.JASAAPOTEKER = CDec(0)
    '                    dsRekap.JASAAHLIGIZI = CDec(10000)
    '                    dsRekap.JASAINPUBILLING = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANSPESIALIS = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANUMUM = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANIGD = CDec(0)
    '                    dsRekap.JASATINDAKAN = CDec(0)
    '                    dsRekap.JASAOPERATOR = CDec(0)
    '                    dsRekap.JASAANESTESI = CDec(0)
    '                    dsRekap.JASAASISTEN = CDec(0)
    '                    dsRekap.BIDAN_PERAWAT = CDec(0)

    '                    listAll.Add(dsRekap)
    '                Next

    '                For Each xloop In dsGroup.Where(Function(x) x.TARIF = "Sewa Ruang Perawatan")
    '                    Dim dsRekap As New DataAccess.R_JASA_KELINAP

    '                    dsRekap.TRANSAKSI = xloop.TRANSAKSI
    '                    dsRekap.NOSEP = xloop.NOSEP
    '                    dsRekap.RUGI = xloop.RUGI
    '                    dsRekap.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                    dsRekap.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                    dsRekap.HITUNGHARI = CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1)
    '                    dsRekap.KDCUSTOMER = xloop.KDCUSTOMER
    '                    dsRekap.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                    dsRekap.NAMA = xloop.NAMA
    '                    dsRekap.NAME_DISPLAY = "Penata Billing"
    '                    dsRekap.UNIT = xloop.UNIT
    '                    dsRekap.TARIF = xloop.TARIF
    '                    dsRekap.DIAGLIST = xloop.DIAGLIST
    '                    dsRekap.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                    dsRekap.TARIF_RS = xloop.TARIF_RS
    '                    dsRekap.BAGIRS_30 = CDec(xloop.TOTAL_TARIF * 30 / 100)
    '                    dsRekap.BAGIBHP = CDec(xloop.TOTAL_TARIF * 35 / 100)
    '                    dsRekap.SISAPAKET = CDec(xloop.TOTAL_TARIF * 35 / 100)
    '                    dsRekap.JASAVISITESPESIALIS = CDec(0)
    '                    dsRekap.JASAVISITESUMUM = CDec(0)
    '                    dsRekap.JASAVISITESUMUMSPESIALIS = CDec(0)
    '                    dsRekap.JASARADIOLOGI = CDec(0)
    '                    dsRekap.JASARADIOGRAFER = CDec(0)
    '                    dsRekap.JASALABORATORIUM = CDec(0)
    '                    dsRekap.JASAANALIS = CDec(0)
    '                    dsRekap.JASAAPOTEKER = CDec(0)
    '                    dsRekap.JASAAHLIGIZI = CDec(0)
    '                    dsRekap.JASAINPUBILLING = CDec(1500)
    '                    dsRekap.JASAPEMERIKSAANSPESIALIS = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANUMUM = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANIGD = CDec(0)
    '                    dsRekap.JASATINDAKAN = CDec(0)
    '                    dsRekap.JASAOPERATOR = CDec(0)
    '                    dsRekap.JASAANESTESI = CDec(0)
    '                    dsRekap.JASAASISTEN = CDec(0)
    '                    dsRekap.BIDAN_PERAWAT = CDec(0)

    '                    listAll.Add(dsRekap)
    '                Next

    '                For Each xloop In dsGroup.Where(Function(x) x.TARIF.Contains("Pemeriksaan Dokter Spesialis"))
    '                    Dim dsRekap As New DataAccess.R_JASA_KELINAP

    '                    dsRekap.TRANSAKSI = xloop.TRANSAKSI
    '                    dsRekap.NOSEP = xloop.NOSEP
    '                    dsRekap.RUGI = xloop.RUGI
    '                    dsRekap.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                    dsRekap.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                    dsRekap.HITUNGHARI = CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1)
    '                    dsRekap.KDCUSTOMER = xloop.KDCUSTOMER
    '                    dsRekap.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                    dsRekap.NAMA = xloop.NAMA
    '                    dsRekap.NAME_DISPLAY = xloop.DOKTER
    '                    dsRekap.UNIT = xloop.UNIT
    '                    dsRekap.TARIF = xloop.TARIF
    '                    dsRekap.DIAGLIST = xloop.DIAGLIST
    '                    dsRekap.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                    dsRekap.TARIF_RS = xloop.TARIF_RS
    '                    dsRekap.BAGIRS_30 = CDec(xloop.TOTAL_TARIF * 30 / 100)
    '                    dsRekap.BAGIBHP = CDec(xloop.TOTAL_TARIF * 35 / 100)
    '                    dsRekap.SISAPAKET = CDec(xloop.TOTAL_TARIF * 35 / 100)
    '                    dsRekap.JASAVISITESPESIALIS = CDec(0)
    '                    dsRekap.JASAVISITESUMUM = CDec(0)
    '                    dsRekap.JASAVISITESUMUMSPESIALIS = CDec(0)
    '                    dsRekap.JASARADIOLOGI = CDec(0)
    '                    dsRekap.JASARADIOGRAFER = CDec(0)
    '                    dsRekap.JASALABORATORIUM = CDec(0)
    '                    dsRekap.JASAANALIS = CDec(0)
    '                    dsRekap.JASAAPOTEKER = CDec(0)
    '                    dsRekap.JASAAHLIGIZI = CDec(0)
    '                    dsRekap.JASAINPUBILLING = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANSPESIALIS = CDec(xloop.HARGTARIF)
    '                    dsRekap.JASAPEMERIKSAANUMUM = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANIGD = CDec(0)
    '                    dsRekap.JASATINDAKAN = CDec(0)
    '                    dsRekap.JASAOPERATOR = CDec(0)
    '                    dsRekap.JASAANESTESI = CDec(0)
    '                    dsRekap.JASAASISTEN = CDec(0)
    '                    dsRekap.BIDAN_PERAWAT = CDec(0)

    '                    listAll.Add(dsRekap)
    '                Next

    '                For Each xloop In dsGroup.Where(Function(x) x.TARIF.Contains("Pemeriksaan Dokter Umum"))
    '                    Dim dsRekap As New DataAccess.R_JASA_KELINAP

    '                    dsRekap.TRANSAKSI = xloop.TRANSAKSI
    '                    dsRekap.NOSEP = xloop.NOSEP
    '                    dsRekap.RUGI = xloop.RUGI
    '                    dsRekap.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                    dsRekap.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                    dsRekap.HITUNGHARI = CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1)
    '                    dsRekap.KDCUSTOMER = xloop.KDCUSTOMER
    '                    dsRekap.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                    dsRekap.NAMA = xloop.NAMA
    '                    dsRekap.NAME_DISPLAY = xloop.DOKTER
    '                    dsRekap.UNIT = xloop.UNIT
    '                    dsRekap.TARIF = xloop.TARIF
    '                    dsRekap.DIAGLIST = xloop.DIAGLIST
    '                    dsRekap.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                    dsRekap.TARIF_RS = xloop.TARIF_RS
    '                    dsRekap.BAGIRS_30 = CDec(xloop.TOTAL_TARIF * 30 / 100)
    '                    dsRekap.BAGIBHP = CDec(xloop.TOTAL_TARIF * 35 / 100)
    '                    dsRekap.SISAPAKET = CDec(xloop.TOTAL_TARIF * 35 / 100)
    '                    dsRekap.JASAVISITESPESIALIS = CDec(0)
    '                    dsRekap.JASAVISITESUMUM = CDec(0)
    '                    dsRekap.JASAVISITESUMUMSPESIALIS = CDec(0)
    '                    dsRekap.JASARADIOLOGI = CDec(0)
    '                    dsRekap.JASARADIOGRAFER = CDec(0)
    '                    dsRekap.JASALABORATORIUM = CDec(0)
    '                    dsRekap.JASAANALIS = CDec(0)
    '                    dsRekap.JASAAPOTEKER = CDec(0)
    '                    dsRekap.JASAAHLIGIZI = CDec(0)
    '                    dsRekap.JASAINPUBILLING = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANSPESIALIS = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANUMUM = CDec(xloop.HARGTARIF)
    '                    dsRekap.JASAPEMERIKSAANIGD = CDec(0)
    '                    dsRekap.JASATINDAKAN = CDec(0)
    '                    dsRekap.JASAOPERATOR = CDec(0)
    '                    dsRekap.JASAANESTESI = CDec(0)
    '                    dsRekap.JASAASISTEN = CDec(0)
    '                    dsRekap.BIDAN_PERAWAT = CDec(0)

    '                    listAll.Add(dsRekap)
    '                Next

    '                For Each xloop In dsGroup.Where(Function(x) x.TARIF.Contains("Pemeriksaan Dokter IGD"))
    '                    Dim dsRekap As New DataAccess.R_JASA_KELINAP

    '                    dsRekap.TRANSAKSI = xloop.TRANSAKSI
    '                    dsRekap.NOSEP = xloop.NOSEP
    '                    dsRekap.RUGI = xloop.RUGI
    '                    dsRekap.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                    dsRekap.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                    dsRekap.HITUNGHARI = CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1)
    '                    dsRekap.KDCUSTOMER = xloop.KDCUSTOMER
    '                    dsRekap.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                    dsRekap.NAMA = xloop.NAMA
    '                    dsRekap.NAME_DISPLAY = xloop.DOKTER
    '                    dsRekap.UNIT = xloop.UNIT
    '                    dsRekap.TARIF = "Visite Dokter Umum"
    '                    dsRekap.DIAGLIST = xloop.DIAGLIST
    '                    dsRekap.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                    dsRekap.TARIF_RS = xloop.TARIF_RS
    '                    dsRekap.BAGIRS_30 = CDec(xloop.TOTAL_TARIF * 30 / 100)
    '                    dsRekap.BAGIBHP = CDec(xloop.TOTAL_TARIF * 35 / 100)
    '                    dsRekap.SISAPAKET = CDec(xloop.TOTAL_TARIF * 35 / 100)
    '                    dsRekap.JASAVISITESPESIALIS = CDec(0)
    '                    dsRekap.JASAVISITESUMUM = CDec(0)
    '                    dsRekap.JASAVISITESUMUMSPESIALIS = CDec(0)
    '                    dsRekap.JASARADIOLOGI = CDec(0)
    '                    dsRekap.JASARADIOGRAFER = CDec(0)
    '                    dsRekap.JASALABORATORIUM = CDec(0)
    '                    dsRekap.JASAANALIS = CDec(0)
    '                    dsRekap.JASAAPOTEKER = CDec(0)
    '                    dsRekap.JASAAHLIGIZI = CDec(0)
    '                    dsRekap.JASAINPUBILLING = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANSPESIALIS = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANUMUM = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANIGD = CDec(IIf(list.FirstOrDefault(Function(x) x.Transaksi = xloop.TRANSAKSI).NAME_DISPLAY_1.Contains("Visite Dokter Spesialis Visite Dokter Umum"), (150000 * (40 / 100)) / list.FirstOrDefault(Function(x) x.Transaksi = xloop.TRANSAKSI).NAME_DISPLAY_2, IIf(list.FirstOrDefault(Function(x) x.Transaksi = xloop.TRANSAKSI).NAME_DISPLAY_1.Contains("Visite Dokter Umum"), 150000 * (80 / 100) / list.FirstOrDefault(Function(x) x.Transaksi = xloop.TRANSAKSI).NAME_DISPLAY_2, xloop.HARGTARIF)))
    '                    dsRekap.JASATINDAKAN = CDec(0)
    '                    dsRekap.JASAOPERATOR = CDec(0)
    '                    dsRekap.JASAANESTESI = CDec(0)
    '                    dsRekap.JASAASISTEN = CDec(0)
    '                    dsRekap.BIDAN_PERAWAT = CDec(0)

    '                    listAll.Add(dsRekap)
    '                Next

    '                For Each xloop In dsGroup.Where(Function(x) x.TINDAKAN = "TINDAKAN")
    '                    Dim dsRekap As New DataAccess.R_JASA_KELINAP

    '                    dsRekap.TRANSAKSI = xloop.TRANSAKSI
    '                    dsRekap.NOSEP = xloop.NOSEP
    '                    dsRekap.RUGI = xloop.RUGI
    '                    dsRekap.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                    dsRekap.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                    dsRekap.HITUNGHARI = CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1)
    '                    dsRekap.KDCUSTOMER = xloop.KDCUSTOMER
    '                    dsRekap.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                    dsRekap.NAMA = xloop.NAMA
    '                    dsRekap.NAME_DISPLAY = xloop.DOKTER
    '                    dsRekap.UNIT = xloop.UNIT
    '                    dsRekap.TARIF = xloop.TARIF & " Dokter"
    '                    dsRekap.DIAGLIST = xloop.DIAGLIST
    '                    dsRekap.TOTAL_TARIF = CDec(0)
    '                    dsRekap.TARIF_RS = CDec(0)
    '                    dsRekap.BAGIRS_30 = CDec(0)
    '                    dsRekap.BAGIBHP = CDec(0)
    '                    dsRekap.SISAPAKET = CDec(0)
    '                    dsRekap.JASAVISITESPESIALIS = CDec(0)
    '                    dsRekap.JASAVISITESUMUM = CDec(0)
    '                    dsRekap.JASAVISITESUMUMSPESIALIS = CDec(0)
    '                    dsRekap.JASARADIOLOGI = CDec(0)
    '                    dsRekap.JASARADIOGRAFER = CDec(0)
    '                    dsRekap.JASALABORATORIUM = CDec(0)
    '                    dsRekap.JASAANALIS = CDec(0)
    '                    dsRekap.JASAAPOTEKER = CDec(0)
    '                    dsRekap.JASAAHLIGIZI = CDec(0)
    '                    dsRekap.JASAINPUBILLING = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANSPESIALIS = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANUMUM = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANIGD = CDec(0)
    '                    dsRekap.JASATINDAKAN = CDec(xloop.HARGTARIF * 60 / 100)
    '                    dsRekap.JASAOPERATOR = CDec(0)
    '                    dsRekap.JASAANESTESI = CDec(0)
    '                    dsRekap.JASAASISTEN = CDec(0)
    '                    dsRekap.BIDAN_PERAWAT = CDec(0)

    '                    listAll.Add(dsRekap)
    '                Next

    '                For Each xloop In dsGroup.Where(Function(x) x.TINDAKAN = "TINDAKAN")
    '                    Dim dsRekap As New DataAccess.R_JASA_KELINAP

    '                    dsRekap.TRANSAKSI = xloop.TRANSAKSI
    '                    dsRekap.NOSEP = xloop.NOSEP
    '                    dsRekap.RUGI = xloop.RUGI
    '                    dsRekap.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                    dsRekap.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                    dsRekap.HITUNGHARI = CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1)
    '                    dsRekap.KDCUSTOMER = xloop.KDCUSTOMER
    '                    dsRekap.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                    dsRekap.NAMA = xloop.NAMA
    '                    dsRekap.NAME_DISPLAY = xloop.UNIT
    '                    dsRekap.UNIT = xloop.UNIT
    '                    dsRekap.TARIF = xloop.TARIF & " Perawat"
    '                    dsRekap.DIAGLIST = xloop.DIAGLIST
    '                    dsRekap.TOTAL_TARIF = CDec(0)
    '                    dsRekap.TARIF_RS = CDec(0)
    '                    dsRekap.BAGIRS_30 = CDec(0)
    '                    dsRekap.BAGIBHP = CDec(0)
    '                    dsRekap.SISAPAKET = CDec(0)
    '                    dsRekap.JASAVISITESPESIALIS = CDec(0)
    '                    dsRekap.JASAVISITESUMUM = CDec(0)
    '                    dsRekap.JASAVISITESUMUMSPESIALIS = CDec(0)
    '                    dsRekap.JASARADIOLOGI = CDec(0)
    '                    dsRekap.JASARADIOGRAFER = CDec(0)
    '                    dsRekap.JASALABORATORIUM = CDec(0)
    '                    dsRekap.JASAANALIS = CDec(0)
    '                    dsRekap.JASAAPOTEKER = CDec(0)
    '                    dsRekap.JASAAHLIGIZI = CDec(0)
    '                    dsRekap.JASAINPUBILLING = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANSPESIALIS = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANUMUM = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANIGD = CDec(0)
    '                    dsRekap.JASATINDAKAN = CDec(xloop.HARGTARIF * 40 / 100)
    '                    dsRekap.JASAOPERATOR = CDec(0)
    '                    dsRekap.JASAANESTESI = CDec(0)
    '                    dsRekap.JASAASISTEN = CDec(0)
    '                    dsRekap.BIDAN_PERAWAT = CDec(0)

    '                    listAll.Add(dsRekap)
    '                Next

    '                Dim dsGroupProsedur = From x In listDataAwal.Where(Function(x) x.ISPROSEDUR = 1)
    '                                      Group x By x.TRANSAKSI, x.NOSEP, x.RUGI, x.ADMISSION_DATE, x.TARIF_RS, x.DISCHARGE_DATE, x.DIAGLIST, x.KDCUSTOMER, x.NO_TRANSAKSI, x.NAMA, x.DOKTER_DPJP, x.DOKTER, x.UNIT, x.TOTAL_TARIF Into HARGTARIF = Sum(x.HARGTARIF)
    '                                      Select TRANSAKSI, NOSEP, RUGI, ADMISSION_DATE, DISCHARGE_DATE, TARIF_RS, KDCUSTOMER, NO_TRANSAKSI, DIAGLIST, NAMA, DOKTER_DPJP, DOKTER, UNIT, TOTAL_TARIF, HARGTARIF

    '                For Each xloop In dsGroupProsedur
    '                    Dim dsRekap As New DataAccess.R_JASA_KELINAP

    '                    dsRekap.TRANSAKSI = xloop.TRANSAKSI
    '                    dsRekap.NOSEP = xloop.NOSEP
    '                    dsRekap.RUGI = xloop.RUGI
    '                    dsRekap.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                    dsRekap.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                    dsRekap.HITUNGHARI = CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1)
    '                    dsRekap.KDCUSTOMER = xloop.KDCUSTOMER
    '                    dsRekap.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                    dsRekap.NAMA = xloop.NAMA
    '                    dsRekap.NAME_DISPLAY = "OPERATOR BEDAH"
    '                    dsRekap.UNIT = xloop.UNIT
    '                    dsRekap.TARIF = ""
    '                    dsRekap.DIAGLIST = xloop.DIAGLIST
    '                    dsRekap.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                    dsRekap.TARIF_RS = xloop.TARIF_RS
    '                    dsRekap.BAGIRS_30 = CDec(xloop.TOTAL_TARIF * 30 / 100)
    '                    dsRekap.BAGIBHP = CDec(xloop.TOTAL_TARIF * 35 / 100)
    '                    dsRekap.SISAPAKET = CDec(xloop.TOTAL_TARIF * 35 / 100)
    '                    dsRekap.JASAVISITESPESIALIS = CDec(0)
    '                    dsRekap.JASAVISITESUMUM = CDec(0)
    '                    dsRekap.JASAVISITESUMUMSPESIALIS = CDec(0)
    '                    dsRekap.JASARADIOLOGI = CDec(0)
    '                    dsRekap.JASARADIOGRAFER = CDec(0)
    '                    dsRekap.JASALABORATORIUM = CDec(0)
    '                    dsRekap.JASAANALIS = CDec(0)
    '                    dsRekap.JASAAPOTEKER = CDec(0)
    '                    dsRekap.JASAAHLIGIZI = CDec(0)
    '                    dsRekap.JASAINPUBILLING = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANSPESIALIS = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANUMUM = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANIGD = CDec(0)
    '                    dsRekap.JASATINDAKAN = CDec(0)
    '                    dsRekap.JASAOPERATOR = CDec((xloop.TOTAL_TARIF * 35 / 100) * 18 / 100)
    '                    dsRekap.JASAANESTESI = CDec(0)
    '                    dsRekap.JASAASISTEN = CDec(0)
    '                    dsRekap.BIDAN_PERAWAT = CDec(0)

    '                    listAll.Add(dsRekap)
    '                Next

    '                For Each xloop In dsGroupProsedur
    '                    Dim dsRekap As New DataAccess.R_JASA_KELINAP

    '                    dsRekap.TRANSAKSI = xloop.TRANSAKSI
    '                    dsRekap.NOSEP = xloop.NOSEP
    '                    dsRekap.RUGI = xloop.RUGI
    '                    dsRekap.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                    dsRekap.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                    dsRekap.HITUNGHARI = CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1)
    '                    dsRekap.KDCUSTOMER = xloop.KDCUSTOMER
    '                    dsRekap.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                    dsRekap.NAMA = xloop.NAMA
    '                    dsRekap.NAME_DISPLAY = "OPERATOR ANASTESI"
    '                    dsRekap.UNIT = xloop.UNIT
    '                    dsRekap.TARIF = ""
    '                    dsRekap.DIAGLIST = xloop.DIAGLIST
    '                    dsRekap.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                    dsRekap.TARIF_RS = xloop.TARIF_RS
    '                    dsRekap.BAGIRS_30 = CDec(xloop.TOTAL_TARIF * 30 / 100)
    '                    dsRekap.BAGIBHP = CDec(xloop.TOTAL_TARIF * 35 / 100)
    '                    dsRekap.SISAPAKET = CDec(xloop.TOTAL_TARIF * 35 / 100)
    '                    dsRekap.JASAVISITESPESIALIS = CDec(0)
    '                    dsRekap.JASAVISITESUMUM = CDec(0)
    '                    dsRekap.JASAVISITESUMUMSPESIALIS = CDec(0)
    '                    dsRekap.JASARADIOLOGI = CDec(0)
    '                    dsRekap.JASARADIOGRAFER = CDec(0)
    '                    dsRekap.JASALABORATORIUM = CDec(0)
    '                    dsRekap.JASAANALIS = CDec(0)
    '                    dsRekap.JASAAPOTEKER = CDec(0)
    '                    dsRekap.JASAAHLIGIZI = CDec(0)
    '                    dsRekap.JASAINPUBILLING = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANSPESIALIS = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANUMUM = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANIGD = CDec(0)
    '                    dsRekap.JASATINDAKAN = CDec(0)
    '                    dsRekap.JASAOPERATOR = CDec(0)
    '                    dsRekap.JASAANESTESI = CDec((xloop.TOTAL_TARIF * 35 / 100) * 8 / 100)
    '                    dsRekap.JASAASISTEN = CDec(0)
    '                    dsRekap.BIDAN_PERAWAT = CDec(0)

    '                    listAll.Add(dsRekap)
    '                Next

    '                For Each xloop In dsGroupProsedur
    '                    Dim dsRekap As New DataAccess.R_JASA_KELINAP

    '                    dsRekap.TRANSAKSI = xloop.TRANSAKSI
    '                    dsRekap.NOSEP = xloop.NOSEP
    '                    dsRekap.RUGI = xloop.RUGI
    '                    dsRekap.ADMISSION_DATE = xloop.ADMISSION_DATE
    '                    dsRekap.DISCHARGE_DATE = xloop.DISCHARGE_DATE
    '                    dsRekap.HITUNGHARI = CInt(DateDiff(DateInterval.Day, xloop.ADMISSION_DATE, xloop.DISCHARGE_DATE) + 1)
    '                    dsRekap.KDCUSTOMER = xloop.KDCUSTOMER
    '                    dsRekap.NO_TRANSAKSI = xloop.NO_TRANSAKSI
    '                    dsRekap.NAMA = xloop.NAMA
    '                    dsRekap.NAME_DISPLAY = "OPERATOR ANASTESI"
    '                    dsRekap.UNIT = xloop.UNIT
    '                    dsRekap.TARIF = ""
    '                    dsRekap.DIAGLIST = xloop.DIAGLIST
    '                    dsRekap.TOTAL_TARIF = xloop.TOTAL_TARIF
    '                    dsRekap.TARIF_RS = xloop.TARIF_RS
    '                    dsRekap.BAGIRS_30 = CDec(xloop.TOTAL_TARIF * 30 / 100)
    '                    dsRekap.BAGIBHP = CDec(xloop.TOTAL_TARIF * 35 / 100)
    '                    dsRekap.SISAPAKET = CDec(xloop.TOTAL_TARIF * 35 / 100)
    '                    dsRekap.JASAVISITESPESIALIS = CDec(0)
    '                    dsRekap.JASAVISITESUMUM = CDec(0)
    '                    dsRekap.JASAVISITESUMUMSPESIALIS = CDec(0)
    '                    dsRekap.JASARADIOLOGI = CDec(0)
    '                    dsRekap.JASARADIOGRAFER = CDec(0)
    '                    dsRekap.JASALABORATORIUM = CDec(0)
    '                    dsRekap.JASAANALIS = CDec(0)
    '                    dsRekap.JASAAPOTEKER = CDec(0)
    '                    dsRekap.JASAAHLIGIZI = CDec(0)
    '                    dsRekap.JASAINPUBILLING = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANSPESIALIS = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANUMUM = CDec(0)
    '                    dsRekap.JASAPEMERIKSAANIGD = CDec(0)
    '                    dsRekap.JASATINDAKAN = CDec(0)
    '                    dsRekap.JASAOPERATOR = CDec(0)
    '                    dsRekap.JASAANESTESI = CDec((xloop.TOTAL_TARIF * 35 / 100) * 8 / 100)
    '                    dsRekap.JASAASISTEN = CDec(0)
    '                    dsRekap.BIDAN_PERAWAT = CDec(0)

    '                    listAll.Add(dsRekap)
    '                Next

    '                'Dim dsUnion = dsHasilGroupS.Union(dsHasilGroupU).Union(dsHasilGroupF).Union(dsHasilGroupG).Union(dsHasilGroupBilling).Union(dsHasilGroupRadiologi).Union(dsHasilGroupRadiografer).Union(dsHasilGroupLaboratorium).Union(dsHasilGroupAnalis).Union(dsHasilPemeriksaanDokterIGD).Union(dsHasilPemeriksaanDokterUmum).Union(dsHasilPemeriksaanDokterSpesialis)
    '                'Dim dsHasil = From x In dsHasilGroupS.Union(dsHasilGroupU).Union(dsHasilGroupF).Union(dsHasilGroupG).Union(dsHasilGroupBilling).Union(dsHasilGroupRadiologi).Union(dsHasilGroupRadiografer).Union(dsHasilGroupLaboratorium).Union(dsHasilGroupAnalis).Union(dsHasilPemeriksaanDokterIGD).Union(dsHasilPemeriksaanDokterUmum).Union(dsHasilPemeriksaanDokterSpesialis)
    '                '              Select x.TRANSAKSI, x.NOSEP, x.RUGI, x.ADMISSION_DATE, x.DISCHARGE_DATE, x.HITUNGHARI, x.KDCUSTOMER _
    '                '              , x.NO_TRANSAKSI, x.NAMA, x.NAME_DISPLAY, x.UNIT, x.TARIF, x.DIAGLIST, x.TOTAL_TARIF, x.TARIF_RS, x.BAGIRS_30, x.BAGIBHP, x.SISAPAKET _
    '                '              , BAGI = x.JASAVISITESPESIALIS + x.JASAVISITESUMUM + x.JASAVISITESUMUMSPESIALIS + x.JASARADIOLOGI + x.JASARADIOGRAFER + x.JASALABORATORIUM + x.JASAANALIS + x.JASAAPOTEKER + x.JASAAHLIGIZI _
    '                '              + x.JASAINPUBILLING + x.JASAPEMERIKSAANSPESIALIS + x.JASAPEMERIKSAANUMUM + x.JASAPEMERIKSAANIGD + x.JASATINDAKAN



    '                grd.MainView = grv
    '                grd.DataSource = IIf(chkGroup.Checked = False, listAll, listAll)
    '                grd.ForceInitialize()

    '            End If

    '            fn_LoadFormatDataAll()

    '        ElseIf cboTYPE.SelectedIndex = 2 Then
    '            Dim listDetailTransaksiAll_02 As New List(Of DataAccess.R_JASA_RUMUS2)
    '            Dim listDetailTransaksiNONRS As New List(Of DataAccess.R_PARAMETER_HASIL)
    '            Dim listDetailTransaksiRS As New List(Of DataAccess.R_PARAMETER_HASIL)

    '            For iLoop As Integer = 0 To ds.Tables("ALL").Rows.Count - 1
    '                Dim dsRekap As New DataAccess.R_PARAMETER_HASIL
    '                With ds.Tables("ALL")
    '                    If .Rows(iLoop)("KDUOM") = "NONRS" Then
    '                        dsRekap.Transaksi = .Rows(iLoop)("NOSEP")
    '                        dsRekap.Total = CDec(.Rows(iLoop)("TOTALBAGI"))

    '                        listDetailTransaksiNONRS.Add(dsRekap)
    '                    End If
    '                End With
    '            Next

    '            For iLoop As Integer = 0 To ds.Tables("ALL").Rows.Count - 1
    '                Dim dsRekap As New DataAccess.R_PARAMETER_HASIL
    '                With ds.Tables("ALL")
    '                    If .Rows(iLoop)("KDUOM") = "RS" Then
    '                        dsRekap.Transaksi = .Rows(iLoop)("NOSEP")
    '                        dsRekap.Total = CDec(.Rows(iLoop)("TOTALBAGI"))

    '                        listDetailTransaksiRS.Add(dsRekap)
    '                    End If
    '                End With
    '            Next

    '            Dim ListGroupNONRS = From x In listDetailTransaksiNONRS
    '                                 Group x By x.Transaksi Into Total = Sum(x.Total)

    '            Dim ListGroupRS = From x In listDetailTransaksiRS
    '                              Group x By x.Transaksi Into Total = Sum(x.Total)


    '            grd.MainView = grv
    '            grd.DataSource = ds.Tables("ALL")
    '            grd.ForceInitialize()

    '            For iLoop As Integer = 0 To ds.Tables("ALL").Rows.Count - 1
    '                Dim dsRekap As New DataAccess.R_JASA_RUMUS2
    '                With ds.Tables("ALL")
    '                    dsRekap.Transaksi = .Rows(iLoop)("NOSEP")
    '                    dsRekap.RUGI = .Rows(iLoop)("RUGI")
    '                    dsRekap.ADMISSION_DATE = .Rows(iLoop)("ADMISSION_DATE")
    '                    dsRekap.KDCUSTOMER = .Rows(iLoop)("KDCUSTOMER")
    '                    dsRekap.NO_TRANSAKSI = .Rows(iLoop)("NO_TRANSAKSI")
    '                    dsRekap.NAMA = .Rows(iLoop)("NAMA")
    '                    dsRekap.DOKTER_DPJP = .Rows(iLoop)("DOKTER_DPJP")
    '                    dsRekap.DOKTER = .Rows(iLoop)("DOKTER")
    '                    dsRekap.GROUPTARIF = .Rows(iLoop)("GROUPTARIF")
    '                    dsRekap.UNIT = .Rows(iLoop)("UNIT")
    '                    dsRekap.TARIF = .Rows(iLoop)("TARIF")
    '                    dsRekap.TARIF_RS = CDec(.Rows(iLoop)("TARIF_RS"))
    '                    dsRekap.TOTAL_TARIF = CDec(.Rows(iLoop)("TOTAL_TARIF"))
    '                    dsRekap.HARGTARIF = CDec(.Rows(iLoop)("HARGTARIF"))
    '                    dsRekap.RATE = CDec(.Rows(iLoop)("RATE"))
    '                    dsRekap.TARIFBAGI = CDec(.Rows(iLoop)("TARIFBAGI"))

    '                    If CDec(.Rows(iLoop)("TARIF_RS")) < CDec(.Rows(iLoop)("TOTAL_TARIF")) Then
    '                        dsRekap.TOTALBAGI = CDec(.Rows(iLoop)("TOTALBAGI"))
    '                        'If .Rows(iLoop)("KDUOM") = "RS" Then
    '                        '    dsRekap.TOTALBAGI = CDec(.Rows(iLoop)("TOTALBAGI"))
    '                        'Else
    '                        '    Dim Transaksi = .Rows(iLoop)("NOSEP")

    '                        '    Dim Tes1 = CDec(.Rows(iLoop)("TOTALBAGI")) / ListGroupNONRS.FirstOrDefault(Function(x) x.Transaksi = Transaksi).Total
    '                        '    Dim Tes2 = Tes1 * (CDec(.Rows(iLoop)("TARIF_RS")) - ListGroupRS.FirstOrDefault(Function(x) x.Transaksi = Transaksi).Total)

    '                        '    dsRekap.TOTALBAGI = Tes2

    '                        'End If
    '                    Else
    '                        If .Rows(iLoop)("KDUOM") = "RS" Then
    '                            dsRekap.TOTALBAGI = CDec(.Rows(iLoop)("TOTALBAGI"))
    '                        Else
    '                            Dim Transaksi = .Rows(iLoop)("NOSEP")

    '                            Dim Tes1 = CDec(.Rows(iLoop)("TOTALBAGI")) / ListGroupNONRS.FirstOrDefault(Function(x) x.Transaksi = Transaksi).Total
    '                            Dim Tes2 = Tes1 * (CDec(.Rows(iLoop)("TOTAL_TARIF")) - ListGroupRS.FirstOrDefault(Function(x) x.Transaksi = Transaksi).Total)

    '                            dsRekap.TOTALBAGI = Tes2

    '                        End If

    '                    End If

    '                    dsRekap.KDUOM = .Rows(iLoop)("KDUOM")
    '                    dsRekap.KET = .Rows(iLoop)("KET")
    '                    dsRekap.TINDAKAN = .Rows(iLoop)("TINDAKAN")

    '                    listDetailTransaksiAll_02.Add(dsRekap)
    '                End With
    '            Next

    '            If grdKDUOM.Text = String.Empty Then
    '                If cboCategory.SelectedIndex = 0 Then
    '                    grd.MainView = grv
    '                    grd.DataSource = listDetailTransaksiAll_02
    '                    grd.ForceInitialize()
    '                ElseIf cboCategory.SelectedIndex = 1 Then
    '                    grd.MainView = grv
    '                    grd.DataSource = listDetailTransaksiAll_02.Where(Function(x) x.RUGI = False)
    '                    grd.ForceInitialize()
    '                Else
    '                    grd.MainView = grv
    '                    grd.DataSource = listDetailTransaksiAll_02.Where(Function(x) x.RUGI = True)
    '                    grd.ForceInitialize()
    '                End If
    '            Else
    '                If cboCategory.SelectedIndex = 0 Then
    '                    grd.MainView = grv
    '                    grd.DataSource = listDetailTransaksiAll_02.Where(Function(x) x.KET = grdKDUOM.Text)
    '                    grd.ForceInitialize()
    '                ElseIf cboCategory.SelectedIndex = 1 Then
    '                    grd.MainView = grv
    '                    grd.DataSource = listDetailTransaksiAll_02.Where(Function(x) x.RUGI = False And x.KET = grdKDUOM.Text)
    '                    grd.ForceInitialize()
    '                Else
    '                    grd.MainView = grv
    '                    grd.DataSource = listDetailTransaksiAll_02.Where(Function(x) x.RUGI = True And x.KET = grdKDUOM.Text)
    '                    grd.ForceInitialize()
    '                End If
    '            End If

    '            fn_LoadFormatDataAll()
    '        End If

    '    Catch oErr As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub

    Private Sub fn_LoadFormatDataAll()
        For iLoop As Integer = 0 To grv.Columns.Count - 1
            If grv.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

                grv.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, grv.Columns(iLoop).FieldName, grv.Columns(iLoop), "{0:n0}")
                grv.Columns(iLoop).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                grv.Columns(iLoop).SummaryItem.DisplayFormat = "{0:n0}"

            ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next
    End Sub
    Public Sub fn_LoadLanguageAll()
        Try
            grv.Columns("KDCASH").Caption = Cash.KDCASH
            grv.Columns("DATE").Caption = Cash.TANGGAL
            grv.Columns("KDPAYMENTTYPE").Caption = Cash.KDPAYMENTTYPE
            grv.Columns("KDCOA").Caption = Cash.KDCOA
            grv.Columns("MEMO").Caption = Cash.MEMO
            grv.Columns("TOTAL").Caption = Cash.TOTAL
            grv.Columns("KDUSER").Caption = Caption.User

            MasterColumnChooserToolStripMenuItem.Text = Caption.ColumnChooserMaster
        Catch oErr As Exception

        End Try
    End Sub
#End Region

    Private Sub MasterColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MasterColumnChooserToolStripMenuItem.Click
        grv.ShowCustomization()
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmMember_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                Me.Close()
            Case Keys.P
                If e.Alt = True And picPrint.Enabled = True Then
                    picPrint_Click()
                End If
            Case Keys.R
                If e.Alt = True And picRefresh.Enabled = True Then
                    picRefresh_Click()
                End If
        End Select
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        Try
            fn_Print()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_LoadSecurity()

        'Try
        '    grv.RestoreLayoutFromRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text & "\")
        'Catch ex As Exception

        'End Try
    End Sub
    Private Sub fn_LoadKDPAYMENTTYPE()
        Dim oPAYMENTTYPE As New Reference.clsJudulJasa
        Dim oCashinJasa As New Finance.clsCashinBPJS

        Try
            Dim dsPayment = From x In oCashinJasa.GetData()
                            Join y In oPAYMENTTYPE.GetData()
                            On x.KDJUDULJASA Equals y.KDJUDULJASA
                            Select x.KDJUDULJASA, y.MEMO, JENISRAWAT = IIf(x.CATEGORY = 0, "Rawat Jalan", "Rawat Inap")

            grdKDPAYMENTTYPE.Properties.DataSource = dsPayment.ToList()
            grdKDPAYMENTTYPE.Properties.ValueMember = "KDJUDULJASA"
            grdKDPAYMENTTYPE.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDUOM()
        Dim oUOM As New Reference.clsUOM

        Try
            grdKDUOM.Properties.DataSource = oUOM.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDUOM.Properties.ValueMember = "KDUOM"
            grdKDUOM.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDUOM_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdKDUOM.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdKDUOM.ResetText()
        End If
    End Sub
    Private Sub cboTYPE_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTYPE.SelectedIndexChanged
        'If cboTYPE.SelectedIndex = 0 Then
        '    cboCategory.Visible = False
        '    grdKDUOM.Visible = False
        '    cboUom.Visible = False
        '    chkGroup.Visible = False
        'ElseIf cboTYPE.SelectedIndex = 1 Then
        '    cboCategory.Visible = True
        '    grdKDUOM.Visible = False
        '    cboUom.Visible = True
        '    chkGroup.Visible = True
        'Else
        '    cboCategory.Visible = True
        '    grdKDUOM.Visible = True
        '    cboUom.Visible = False
        '    chkGroup.Enabled = False
        'End If
    End Sub

    Private Sub chkBPJS_CheckedChanged(sender As Object, e As EventArgs) Handles chkUmum.CheckedChanged
        'If chkUmum.Checked = False Then
        '    deDATEFrom.Visible = False
        '    deDATETo.Visible = False
        '    chkGroup.Visible = False
        '    grdKDPAYMENTTYPE.Visible = True
        'Else
        '    deDATEFrom.Visible = True
        '    deDATETo.Visible = True
        '    chkGroup.Enabled = True
        '    grdKDPAYMENTTYPE.Visible = False
        'End If
    End Sub
#End Region
End Class