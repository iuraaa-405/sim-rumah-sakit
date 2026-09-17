Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Imports DevExpress.XtraPrinting
Imports DevExpress.XtraSplashScreen
Imports System.Data
Imports System.Data.SqlClient


Public Class frmReportCashinBPJS
    Implements ILanguage
    Private sCategory As Integer = 0
    Private pilih As Integer = 0

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "Laporan Jasa"

        lTYPE.Text = Report.FILTER_TYPE

        cboTYPE.Properties.Items.Clear()
        cboTYPE.Properties.Items.Add("Rekap")
        cboTYPE.Properties.Items.Add("Rincian")
        cboTYPE.Properties.Items.Add("Poliklinik")
        cboTYPE.SelectedIndex = 0
    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        'Try
        '    grv.SaveLayoutToRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text & "\" & cboTYPE.Text)
        'Catch oErr As Exception

        'End Try
    End Sub
    Public Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            Me.Text = "Laporan Jasa"

            lTYPE.Text = Report.FILTER_TYPE

            cboTYPE.Properties.Items.Clear()
            cboTYPE.Properties.Items.Add("Rekap")
            cboTYPE.Properties.Items.Add("Rincian")
            cboTYPE.Properties.Items.Add("Poliklinik")
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
                      Where x.MODUL = "JASA_R_TUTUP" _
                      And y.KDUSER = sUserID
                      Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                picPrint.Enabled = ds.ISPRINT
                picRefresh.Enabled = ds.ISVIEW
                picConfirm.Enabled = ds.ISADD
                picRefresh2.Enabled = ds.ISADD
                picSelesai.Enabled = ds.ISADD

                If ds.ISVIEW = True Then
                    fn_LoadKDPAYMENTTYPE()
                    fn_Preview()
                    fn_LoadLanguage()
                End If
            Catch oErr As Exception
                MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)

                picPrint.Enabled = False
                picRefresh.Enabled = False
                picConfirm.Enabled = False
                picRefresh2.Enabled = False
                picSelesai.Enabled = False
            End Try
        Catch oErr As Exception
            MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Print()
        Try
            PrintableComponentLink.Landscape = True
            PrintableComponentLink.PaperKind = Printing.PaperKind.A4

            Dim phf As PageHeaderFooter =
        TryCast(PrintableComponentLink.PageHeaderFooter, PageHeaderFooter)
            phf.Header.Content.Clear()
            phf.Header.Font = New Font("Times New Roman", 14, FontStyle.Bold)
            phf.Header.LineAlignment = BrickAlignment.Center
            phf.Footer.Font = New Font("Times New Roman", 9.75)
            phf.Footer.LineAlignment = BrickAlignment.Far
            phf.Footer.Content.AddRange(New String() _
        {sWATERMARK, "", Report.REPORT_PAGE & " : [Page # of Pages #]"})

            phf.Header.Content.AddRange(New String() _
{"", "LAPORAN JASA " & grdKDJUDULJASA.Text, "-" & cboTYPE.Text})

            printableComponentLink.Component = grd
            PrintableComponentLink.CreateDocument()
            PrintableComponentLink.ShowPreviewDialog(Me)
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

            Select Case cboTYPE.SelectedIndex
                Case 0
                    fn_LoadData(pilih)
                Case 1
                    fn_LoadDataDetail()
                Case 2
                    fn_LoadDataRumusPoliklinik()
            End Select

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#Region "Get Data"
    Private Function fn_NilaiPenunjang(ByVal KDPENDAFTARAN As String) As Decimal
        Try
            fn_NilaiPenunjang = 0

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
            SQL &= "NILAI = SUM(B.GRANDTOTAL) "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_KUNJUNGAN A "
            SQL &= "INNER JOIN S_SO_TRANSAKSI_H B "
            SQL &= "ON A.KDKUNJUNGAN = B.KDKUNJUNGAN "
            SQL &= "WHERE "
            SQL &= "A.KDPENDAFTARAN = '" & KDPENDAFTARAN & "' AND B.CATEGORY = 2 "
            SQL &= "OR "
            SQL &= "A.KDPENDAFTARAN = '" & KDPENDAFTARAN & "' AND B.CATEGORY = 3 "
            SQL &= "OR "
            SQL &= "A.KDPENDAFTARAN = '" & KDPENDAFTARAN & "' AND B.CATEGORY = 4 "
            SQL &= "GROUP BY A.KDPENDAFTARAN "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "F_LAB_H")

            If ds.Tables("F_LAB_H").Rows.Count > 0 Then
                With ds.Tables("F_LAB_H")
                    fn_NilaiPenunjang = .Rows(0)("NILAI")
                End With
            End If


        Catch oErr As Exception
            fn_NilaiPenunjang = 0
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_TindakanLain(ByVal KDPENDAFTARAN As String) As Decimal
        Try
            fn_TindakanLain = 0

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
            SQL &= "NILAI = SUM(B.GRANDTOTAL) "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_KUNJUNGAN A "
            SQL &= "INNER JOIN S_SO_TRANSAKSI_H B "
            SQL &= "ON A.KDKUNJUNGAN = B.KDKUNJUNGAN "
            SQL &= "INNER JOIN S_SO_TRANSAKSI_D C "
            SQL &= "ON B.KDSOTRANSAKSI = C.KDSOTRANSAKSI "
            SQL &= "INNER JOIN M_ITEM D "
            SQL &= "ON C.KDITEM = D.KDITEM "
            SQL &= "WHERE "
            SQL &= "A.KDPENDAFTARAN = '" & KDPENDAFTARAN & "' AND B.CATEGORY = 0 AND D.KDITEM_L5 = 'ITEM_L5_0000000002' "
            SQL &= "OR "
            SQL &= "A.KDPENDAFTARAN = '" & KDPENDAFTARAN & "' AND B.CATEGORY = 1 AND D.KDITEM_L5 = 'ITEM_L5_0000000002' "
            SQL &= "GROUP BY A.KDPENDAFTARAN "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "F_LAB_H")

            If ds.Tables("F_LAB_H").Rows.Count > 0 Then
                With ds.Tables("F_LAB_H")
                    fn_TindakanLain = .Rows(0)("NILAI")
                End With
            End If

        Catch oErr As Exception
            fn_TindakanLain = 0
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
#End Region
#Region "Master Detail"
    Private Sub fn_LoadData(ByVal category As Integer)
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
            SQL &= "A.KDCASHINBPJS "
            SQL &= ",B.NAME_DPJP "
            SQL &= ",B.NOSEP "
            SQL &= ",B.ADMISSION_DATE "
            SQL &= ",B.DISCHARGE_DATE "
            SQL &= ",B.DIAGLIST "
            SQL &= ",B.PROCLIST "
            SQL &= ",B.MRN "
            SQL &= ",B.TOTAL_TARIF "
            SQL &= ",B.TARIF_RS "
            SQL &= ",SELISIH = B.TOTAL_TARIF - B.TARIF_RS "
            SQL &= ",B.JASA_RS "
            SQL &= ",B.JASA_PENUNJANG "
            SQL &= ",B.JASA_TINDAKANLAIN "
            SQL &= ",B.JASA_SISA "
            SQL &= ",B.JASA_MEDIS "
            SQL &= ",B.JASA_PARAMEDIS "
            'SQL &= ",RS_40 = B.TOTAL_TARIF * 0.4 "
            'SQL &= ",BHP_20 = B.TOTAL_TARIF * 0.2 "
            'SQL &= ",JASA_40 = B.TOTAL_TARIF * 0.4 "
            'SQL &= ",JASA_MEDIS = (B.TOTAL_TARIF * 0.4) * 0.6 "
            'SQL &= ",JASA_PARAMEDIS = (B.TOTAL_TARIF * 0.4) * 0.4 "

            If category = 0 Then
                SQL &= ",B.KDPENDAFTARAN "
            ElseIf category = 1 Then
                SQL &= ",KDPENDAFTARAN = ISNULL((SELECT KDPENDAFTARAN FROM S_PENDAFTARAN_H WHERE B.NOSEP = NOMORSEP), '') "
            ElseIf category = 2 Then
                SQL &= ",KDPENDAFTARAN = ISNULL((SELECT KDPENDAFTARAN FROM S_PENDAFTARAN_H WHERE B.NOSEP = NOMORSEP), ISNULL((SELECT TOP 1 KDPENDAFTARAN FROM S_PENDAFTARAN_H WHERE REPLACE(STR(KDCUSTOMER, 6), SPACE(1), '0') = B.MRN AND CATEGORY = " & sCategory & " AND CONVERT(VARCHAR(8), DATE, 112) = CONVERT(VARCHAR(8), B.ADMISSION_DATE, 112)), '')) "
            End If

            SQL &= "FROM "
            SQL &= "F_CASHINBPJS_H A "
            SQL &= "INNER JOIN F_CASHINBPJS_D B "
            SQL &= "ON A.KDCASHINBPJS = B.KDCASHINBPJS "
            SQL &= "INNER JOIN M_JUDULJASA C "
            SQL &= "ON A.KDJUDULJASA = C.KDJUDULJASA "
            SQL &= "WHERE "
            SQL &= "A.KDJUDULJASA = '" & grdKDJUDULJASA.EditValue & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "F_CASHINBPJS_H")

            grd.MainView = grv
            grd.DataSource = ds.Tables("F_CASHINBPJS_H")
            grd.ForceInitialize()

            fn_LoadFormatData()

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDataDetail()
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
            SQL &= ",A.KDPENDAFTARAN_AWAL "
            SQL &= ",D.KDSOTRANSAKSI "
            SQL &= ",K.NOSEP "
            SQL &= ",TANGGALMASUK = K.ADMISSION_DATE "
            SQL &= ",TANGGALPULANG = K.DISCHARGE_DATE "
            SQL &= ",PASIEN = B.NAME_DISPLAY "
            SQL &= ",K.TOTAL_TARIF "
            SQL &= ",K.TARIF_RS "
            SQL &= ",SELISIH = K.TOTAL_TARIF - K.TARIF_RS "
            SQL &= ",JENISTARIF = H.MEMO "
            SQL &= ",TARIF = G.NMITEM2 "
            SQL &= ",JUMLAH = E.QTY "
            SQL &= ",HARGA = E.PRICE "
            SQL &= ",GRANDTOTAL = E.GRANDTOTAL "
            SQL &= ",SPESIALIS = (SELECT CASE I.SUBSPESIALIS WHEN 1 THEN CONVERT(BIT, 0) ELSE CONVERT(BIT, 1) END) "
            SQL &= ",K.NAME_DPJP "
            SQL &= ",DOKTER_TINDAK = I.NAME_DISPLAY "
            SQL &= ",TUJUAN = F.NAME_DISPLAY "
            SQL &= ",UNIT_TINDAK = J.NAME_DISPLAY "
            SQL &= ",RUJUKAN = ISNULL((SELECT CONVERT(BIT, 1) FROM S_PENDAFTARAN_RUJUKAN WHERE A.KDPENDAFTARAN = KDPENDAFTARAN), CONVERT(BIT, 0)) "
            'SQL &= ",K.JASA_PENUNJANG "
            'SQL &= ",K.JASA_TINDAKANLAIN "
            'SQL &= ",K.JASA_MEDIS "
            'SQL &= ",K.JASA_PARAMEDIS "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_H A "
            SQL &= "INNER JOIN M_CUSTOMER B "
            SQL &= "ON A.KDCUSTOMER = B.KDCUSTOMER "
            SQL &= "INNER JOIN S_PENDAFTARAN_KUNJUNGAN C "
            If sCategory = 0 Then
                SQL &= "ON A.KDPENDAFTARAN = C.KDPENDAFTARAN "
            Else
                SQL &= "ON A.KDPENDAFTARAN = C.KDPENDAFTARAN OR A.KDPENDAFTARAN_AWAL = C.KDPENDAFTARAN "
            End If
            SQL &= "INNER JOIN S_SO_TRANSAKSI_H D "
            SQL &= "ON C.KDKUNJUNGAN = D.KDKUNJUNGAN "
            SQL &= "INNER JOIN S_SO_TRANSAKSI_D E "
            SQL &= "ON D.KDSOTRANSAKSI = E.KDSOTRANSAKSI "
            SQL &= "INNER JOIN M_DEPARTMENT F "
            SQL &= "ON A.KDDEPARTMENT = F.KDDEPARTMENT "
            SQL &= "INNER JOIN M_ITEM G "
            SQL &= "ON E.KDITEM = G.KDITEM "
            SQL &= "INNER JOIN M_ITEM_L1 H "
            SQL &= "ON G.KDITEM_L1 = H.KDITEM_L1 "
            SQL &= "INNER JOIN M_DOCTOR I "
            SQL &= "ON E.KDDOCTOR = I.KDDOCTOR "
            SQL &= "INNER JOIN M_DEPARTMENT J "
            SQL &= "ON E.KDDEPARTMENT = J.KDDEPARTMENT "
            SQL &= "INNER JOIN F_CASHINBPJS_D K "
            SQL &= "ON A.KDPENDAFTARAN = K.KDPENDAFTARAN "
            SQL &= "INNER JOIN F_CASHINBPJS_H L "
            SQL &= "ON K.KDCASHINBPJS = L.KDCASHINBPJS "
            SQL &= "WHERE L.KDJUDULJASA = '" & grdKDJUDULJASA.EditValue & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "F_CASHINBPJS_H")

            grd.MainView = grv
            grd.DataSource = ds.Tables("F_CASHINBPJS_H")
            grd.ForceInitialize()

            fn_LoadFormatData()

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDataRumusPoliklinik()
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
            'SQL &= ",COUNT_TINDAKAN = COUNT(Z.DOKTER_TINDAK) "
            SQL &= ",SISAJASA = Z.SISA_JASAMEDIS / COUNT(Z.DOKTER_TINDAK) "
            SQL &= "FROM "
            SQL &= "( "
            SQL &= "SELECT "
            SQL &= "A.KDPENDAFTARAN "
            SQL &= ",K.NOSEP "
            SQL &= ",TANGGALMASUK = K.ADMISSION_DATE "
            SQL &= ",PASIEN = B.NAME_DISPLAY "
            SQL &= ",K.TOTAL_TARIF "
            SQL &= ",RS_40 = K.TOTAL_TARIF * 0.4 "
            SQL &= ",BHP_20 = K.TOTAL_TARIF * 0.2 "
            SQL &= ",JASA_40 = K.TOTAL_TARIF * 0.40 "
            SQL &= ",JASA_MEDIS = (K.TOTAL_TARIF * 0.4) * 0.6 "
            SQL &= ",JASA_PARAMEDIS = (K.TOTAL_TARIF * 0.4) * 0.4 "
            SQL &= ",LABORATORIUM = K.JASA_LAB "
            SQL &= ",RADIOLOGI = K.JASA_RAD "
            SQL &= ",USG = K.JASA_USG "
            SQL &= ",K.SISA_JASAMEDIS "
            'SQL &= ",JASA_DPJP = (SELECT CASE I.SUBSPESIALIS WHEN 1 THEN K.SISA_JASAMEDIS * 0.5 ELSE K.SISA_JASAMEDIS END) "
            'SQL &= ",JASA_DELEGASI = (SELECT CASE I.SUBSPESIALIS WHEN 1 THEN K.SISA_JASAMEDIS * 0.5 ELSE 0 END) "
            SQL &= ",SPESIALIS = (SELECT CASE I.SUBSPESIALIS WHEN 1 THEN CONVERT(BIT, 0) ELSE CONVERT(BIT, 1) END) "
            SQL &= ",K.NAME_DPJP "
            SQL &= ",DOKTER_TINDAK = I.NAME_DISPLAY "
            SQL &= ",TUJUAN = F.NAME_DISPLAY "
            SQL &= ",UNIT_TINDAK = J.NAME_DISPLAY "
            SQL &= ",RUJUKAN = ISNULL((SELECT CONVERT(BIT, 1) FROM S_PENDAFTARAN_RUJUKAN WHERE A.KDPENDAFTARAN = KDPENDAFTARAN), CONVERT(BIT, 0)) "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_H A "
            SQL &= "INNER JOIN M_CUSTOMER B "
            SQL &= "ON A.KDCUSTOMER = B.KDCUSTOMER "
            SQL &= "INNER JOIN S_PENDAFTARAN_KUNJUNGAN C "
            If sCategory = 0 Then
                SQL &= "ON A.KDPENDAFTARAN = C.KDPENDAFTARAN "
            Else
                SQL &= "ON A.KDPENDAFTARAN = C.KDPENDAFTARAN OR A.KDPENDAFTARAN_AWAL = C.KDPENDAFTARAN "
            End If
            SQL &= "INNER JOIN S_SO_TRANSAKSI_H D "
            SQL &= "ON C.KDKUNJUNGAN = D.KDKUNJUNGAN "
            SQL &= "INNER JOIN S_SO_TRANSAKSI_D E "
            SQL &= "ON D.KDSOTRANSAKSI = E.KDSOTRANSAKSI "
            SQL &= "INNER JOIN M_DEPARTMENT F "
            SQL &= "ON A.KDDEPARTMENT = F.KDDEPARTMENT "
            SQL &= "INNER JOIN M_ITEM G "
            SQL &= "ON E.KDITEM = G.KDITEM "
            SQL &= "INNER JOIN M_ITEM_L1 H "
            SQL &= "ON G.KDITEM_L1 = H.KDITEM_L1 "
            SQL &= "INNER JOIN M_DOCTOR I "
            SQL &= "ON E.KDDOCTOR = I.KDDOCTOR "
            SQL &= "INNER JOIN M_DEPARTMENT J "
            SQL &= "ON E.KDDEPARTMENT = J.KDDEPARTMENT "
            SQL &= "INNER JOIN F_CASHINBPJS_D K "
            SQL &= "ON A.KDPENDAFTARAN = K.KDPENDAFTARAN "
            SQL &= "INNER JOIN F_CASHINBPJS_H L "
            SQL &= "ON K.KDCASHINBPJS = L.KDCASHINBPJS "
            SQL &= "WHERE L.KDJUDULJASA = '" & grdKDJUDULJASA.EditValue & "' "
            SQL &= "AND H.MEMO <> 'BHP' "
            SQL &= "AND H.MEMO <> 'ADMINISTRASI' "
            SQL &= "AND J.NAME_DISPLAY NOT LIKE '%Laboratorium%' "
            SQL &= "AND J.NAME_DISPLAY NOT LIKE '%Radiologi%' "
            SQL &= "AND G.NMITEM2 NOT LIKE '%USG%' "

            SQL &= ") Z "
            SQL &= "WHERE "
            SQL &= "Z.TUJUAN NOT LIKE '%Hemodialisa%' "
            SQL &= "GROUP BY "
            SQL &= "Z.KDPENDAFTARAN "
            SQL &= ",Z.NOSEP "
            SQL &= ",Z.TANGGALMASUK "
            SQL &= ",Z.PASIEN "
            SQL &= ",Z.TOTAL_TARIF "
            SQL &= ",Z.RS_40 "
            SQL &= ",Z.BHP_20 "
            SQL &= ",Z.JASA_40 "
            SQL &= ",Z.JASA_MEDIS "
            SQL &= ",Z.JASA_PARAMEDIS "
            SQL &= ",Z.LABORATORIUM "
            SQL &= ",Z.RADIOLOGI "
            SQL &= ",Z.USG  "
            SQL &= ",Z.SISA_JASAMEDIS "
            SQL &= ",Z.SPESIALIS "
            SQL &= ",Z.NAME_DPJP "
            SQL &= ",Z.DOKTER_TINDAK "
            SQL &= ",Z.TUJUAN "
            SQL &= ",Z.UNIT_TINDAK "
            SQL &= ",Z.RUJUKAN "


            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "F_CASHINBPJS_H")

            grd.MainView = grv
            grd.DataSource = ds.Tables("F_CASHINBPJS_H")
            grd.ForceInitialize()

            fn_LoadFormatData()

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatData()
        For iLoop As Integer = 0 To grv.Columns.Count - 1
            If grv.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n0}"
                grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

        grv.BestFitColumns()

    End Sub
    Private Sub fn_LoadDouble()
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
            SQL &= " * "
            SQL &= " FROM ( "
            SQL &= " SELECT "
            SQL &= " X.KDCUSTOMER "
            SQL &= " ,X.TANGGAL "
            SQL &= " ,X.CARI "
            SQL &= " ,COUNT_DOUBLE = COUNT(X.CARI) "
            SQL &= " FROM ( "
            SQL &= " SELECT  "
            SQL &= " C.KDCUSTOMER "
            SQL &= " ,TANGGAL = CONVERT(VARCHAR(8), C.DATE, 112) "
            SQL &= " ,CARI = C.KDCUSTOMER + CONVERT(VARCHAR(8), C.DATE, 112) "
            SQL &= " FROM F_CASHINBPJS_H A  "
            SQL &= " INNER JOIN F_CASHINBPJS_D  B ON A.KDCASHINBPJS = B.KDCASHINBPJS "
            SQL &= " INNER JOIN S_PENDAFTARAN_H  C ON REPLACE(STR(KDCUSTOMER, 6), SPACE(1), '0') = B.MRN AND CONVERT(VARCHAR(8), C.DATE, 112) = CONVERT(VARCHAR(8), B.ADMISSION_DATE, 112) "
            SQL &= " INNER JOIN M_JUDULJASA D ON A.KDJUDULJASA = D.KDJUDULJASA "
            SQL &= " WHERE A.KDJUDULJASA = '" & grdKDJUDULJASA.EditValue & "' "
            SQL &= " AND C.CATEGORY = " & sCategory & " "
            SQL &= " ) X "
            SQL &= " GROUP BY X.CARI, X.KDCUSTOMER, X.TANGGAL "
            SQL &= " ) Z "
            SQL &= " WHERE  "
            SQL &= " Z.COUNT_DOUBLE > 1 "


            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "F_CASHINBPJS_H")

            grd.MainView = grv
            grd.DataSource = ds.Tables("F_CASHINBPJS_H")
            grd.ForceInitialize()

            fn_LoadFormatData()

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDPAYMENTTYPE()
        Dim oPAYMENTTYPE As New Reference.clsJudulJasa
        Try
            Dim ds = From x In oPAYMENTTYPE.GetData
                     Where x.ISACTIVE = True
                     Select x.KDJUDULJASA, x.MEMO, KATEGORI = IIf(x.CATEGORY = 0, "Rawat Jalan", "Rawat Inap")

            grdKDJUDULJASA.Properties.DataSource = ds.ToList()
            grdKDJUDULJASA.Properties.ValueMember = "KDJUDULJASA"
            grdKDJUDULJASA.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDPAYMENTTYPE_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdKDJUDULJASA.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdKDJUDULJASA.ResetText()
        End If
    End Sub
#End Region
    Private Sub MasterColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MasterColumnChooserToolStripMenuItem.Click
        grv.ShowCustomization()
    End Sub
    Private Sub DetailColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DetailColumnChooserToolStripMenuItem.Click
        grv1.ShowCustomization()
    End Sub
    Private Sub cboTYPE_SelectedIndexChanged() Handles cboTYPE.SelectedIndexChanged
        fn_LoadSecurity()

        Try
            grv.RestoreLayoutFromRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text & "\" & cboTYPE.Text)
        Catch ex As Exception

        End Try
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
            'Case Keys.R
            '    If e.Alt = True And picRefresh.Enabled = True Then
            '        picRefresh_Click()
            '    End If
            Case Keys.C
                If e.Alt = True And picConfirm.Enabled = True Then
                    If pilih <> 0 Then
                        picConfirm_Click()
                    End If
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
        pilih = 1

        fn_LoadSecurity()

        'Try
        '    grv.RestoreLayoutFromRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text & "\" & cboTYPE.Text)
        'Catch ex As Exception

        'End Try
    End Sub
    Private Sub picRefresh2_Click() Handles picRefresh2.Click
        pilih = 2

        fn_LoadSecurity()
    End Sub
    Private Sub picSelesai_Click() Handles picSelesai.Click
        pilih = 0

        fn_LoadSecurity()
    End Sub
    Private Sub picConfirm_Click() Handles picConfirm.Click
        Dim sResult = MessageBox.Show("Apakah yakin akan diperbaharui??", Me.Text, MessageBoxButtons.YesNo)

        If sResult = Windows.Forms.DialogResult.Yes Then
            Dim oCashinBPJS As New Finance.clsCashinBPJS
            Dim sProcess As Integer = 0
            Dim sTotal As Integer = 0

            If cboTYPE.SelectedIndex = 0 Then
                Try
                    Dim dsCahsin = oCashinBPJS.GetDataJudul(grdKDJUDULJASA.EditValue)

                    For Each xloop In dsCahsin
                        oCashinBPJS.UpdateNomorSEPKDREG(xloop.KDCASHINBPJS, sUserID)
                    Next

                    For iLoop As Integer = 0 To grv.RowCount - 1
                        sTotal += 1
                    Next

                    SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

                    For iLoop As Integer = 0 To grv.RowCount - 1
                        If grv.GetRowCellValue(iLoop, "KDPENDAFTARAN") <> "" Then
                            Dim sNilaiTarif As Decimal = grv.GetRowCellValue(iLoop, "TOTAL_TARIF")
                            Dim sNilaiPenunjang As Decimal = fn_NilaiPenunjang(grv.GetRowCellValue(iLoop, "KDPENDAFTARAN"))
                            Dim sNilaiTindakanLain As Decimal = fn_TindakanLain(grv.GetRowCellValue(iLoop, "KDPENDAFTARAN"))

                            Dim sNilai_SisaJasa As Decimal = sNilaiTarif - sNilaiPenunjang - sNilaiTindakanLain

                            oCashinBPJS.UpdateNomorSEPKDREG(grv.GetRowCellValue(iLoop, "KDCASHINBPJS"), grv.GetRowCellValue(iLoop, "NOSEP"), grv.GetRowCellValue(iLoop, "KDPENDAFTARAN"), sNilaiPenunjang, sNilaiTindakanLain, sNilai_SisaJasa, sNilai_SisaJasa * (60 / 100), sNilai_SisaJasa * (40 / 100))
                        End If

                        SplashScreenManager.Default.SetWaitFormCaption("Processing data " & sProcess & " of " & sTotal - 1 & "")

                        sProcess += 1
                    Next

                Catch ex As Exception
                    MsgBox("Load Data" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                Finally
                    SplashScreenManager.CloseForm(False)
                    MsgBox("Export Selesai", MsgBoxStyle.Information, Me.Text)
                End Try

            End If
        End If
    End Sub
    Private Sub grdKDJUDULJASA_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDJUDULJASA.EditValueChanged
        If grdKDJUDULJASA.Text <> "" Then
            Dim oJUDULJASA As New Reference.clsJudulJasa
            Dim dsJUDULJSASA = oJUDULJASA.GetData(grdKDJUDULJASA.EditValue)
            If dsJUDULJSASA IsNot Nothing Then
                sCategory = dsJUDULJSASA.CATEGORY
            End If
        End If
    End Sub
#End Region
End Class