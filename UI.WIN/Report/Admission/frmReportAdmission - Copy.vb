Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports DevExpress.XtraPrinting
Imports System.Data
Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient

Public Class frmReportAdmission
    Implements ILanguage

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "Laporan Admission Rawat Jalan"

        lTYPE.Text = Report.FILTER_TYPE
        lDATEFROM.Text = Report.FILTER_DATEFROM
        lDATETO.Text = Report.FILTER_DATETO

        cboTYPE.Properties.Items.Clear()
        cboTYPE.Properties.Items.Add("Rekap")
        cboTYPE.Properties.Items.Add("Register Laporan Layanan")
        cboTYPE.SelectedIndex = 0

        deDATEFrom.DateTime = Now.AddDays((-Now.Day) + 1)
        deDATETo.DateTime = Now
    End Sub
    Public Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            Me.Text = "Laporan Admission Rawat Jalan"

            lTYPE.Text = Report.FILTER_TYPE
            lDATEFROM.Text = Report.FILTER_DATEFROM
            lDATETO.Text = Report.FILTER_DATETO

            cboTYPE.Properties.Items.Clear()
            cboTYPE.Properties.Items.Add("Rekap")
            cboTYPE.Properties.Items.Add("Register Laporan Layanan")
            'cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_02)
            'cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_03)
            'cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_04)

            'If cboTYPE.SelectedIndex = 3 Then
            '    fn_LoadLanguageAll()
            'Else
            '    fn_LoadLanguageMaster()
            '    fn_LoadLanguageDetail()
            'End If

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
                      Where x.MODUL = "ADMISSION_R" _
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
{"", "Laporan Admission" & vbCrLf & deDATEFrom.DateTime.ToString("dd/MM/yyyy") & " - " & deDATETo.DateTime.ToString("dd/MM/yyyy"), ""})

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

            If cboTYPE.SelectedIndex = 0 Then
                fn_LoadData()
            Else
                fn_LoadDataLayanan()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

#Region "Master Detail"
    Private Sub fn_LoadData()
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
            SQL &= "NoPendaftaran = A.KDPENDAFTARAN "
            SQL &= ",NomorSEP = A.NOMORSEP "
            SQL &= ",TindakLanjut = E.MEMO "
            SQL &= ",Nama = B.NAME_DISPLAY "
            SQL &= ",NoRM = A.KDCUSTOMER "
            SQL &= ",Alamat = (SELECT TOP 1 ALAMAT FROM S_PENDAFTARAN_KUNJUNGAN WHERE A.KDPENDAFTARAN = KDPENDAFTARAN) "
            SQL &= ",Jk = (SELECT CASE B.KDJENISKELAMIN WHEN 0 THEN 'Perempuan' WHEN 1 THEN 'Laki-Laki' ELSE '-' END) "
            SQL &= ",UmurHari = DATEDIFF(DD, B.TANGGALLAHIR, A.DATE) "
            SQL &= ",UmurBulan = DATEDIFF(MM, B.TANGGALLAHIR, A.DATE) "
            SQL &= ",UmurTahun =  DATEDIFF(YYYY, B.TANGGALLAHIR, A.DATE) "
            SQL &= ",NamaKeluarga = B.NAMAKELUARGA "
            SQL &= ",NoTelepon = B.PHONE "
            SQL &= ",PangkatGol = (SELECT TOP 1 BB.MEMO FROM S_PENDAFTARAN_KUNJUNGAN AA INNER JOIN M_PANGKAT BB ON AA.KDPANGKAT = BB.KDPANGKAT WHERE A.KDPENDAFTARAN = AA.KDPENDAFTARAN) "
            SQL &= ",B.NRP "
            SQL &= ",Kesatuan = (SELECT TOP 1 BB.MEMO FROM S_PENDAFTARAN_KUNJUNGAN AA INNER JOIN M_KESATUAN BB ON AA.KDKESATUAN = BB.KDKESATUAN WHERE A.KDPENDAFTARAN = AA.KDPENDAFTARAN) "
            SQL &= ",CaraBayar = (SELECT MEMO FROM M_DAFTAR_L1 WHERE A.KDDAFTAR_L1 = KDDAFTAR_L1) "
            SQL &= ",Tanggalmasuk = A.DATE "
            SQL &= ",DiganosaAwal = (SELECT MEMO FROM M_DIAGNOSA WHERE A.KDDIAGNOSA = KDDIAGNOSA) "
            SQL &= ",Diagnosa_1 = ISNULL((SELECT (SELECT CC.KDDIAGNOSA + '-' + CC.MEMO FROM M_DIAGNOSA CC WHERE AA.KDDIAGNOSA1 = CC.KDDIAGNOSA) FROM S_PENDAFTARAN_DIAGNOSA_MASTER AA WHERE A.KDPENDAFTARAN = AA.KDPENDAFTARAN GROUP BY AA.KDDIAGNOSA1) , '-') "
            SQL &= ",Diagnosa_2 = ISNULL((SELECT (SELECT CC.KDDIAGNOSA + '-' + CC.MEMO FROM M_DIAGNOSA CC WHERE AA.KDDIAGNOSA2 = CC.KDDIAGNOSA) FROM S_PENDAFTARAN_DIAGNOSA_MASTER AA WHERE A.KDPENDAFTARAN = AA.KDPENDAFTARAN GROUP BY AA.KDDIAGNOSA2) , '-') "
            SQL &= ",Diagnosa_3 = ISNULL((SELECT (SELECT CC.KDDIAGNOSA + '-' + CC.MEMO FROM M_DIAGNOSA CC WHERE AA.KDDIAGNOSA3 = CC.KDDIAGNOSA) FROM S_PENDAFTARAN_DIAGNOSA_MASTER AA WHERE A.KDPENDAFTARAN = AA.KDPENDAFTARAN GROUP BY AA.KDDIAGNOSA3) , '-') "
            SQL &= ",Diagnosa_4 = ISNULL((SELECT (SELECT CC.KDDIAGNOSA + '-' + CC.MEMO FROM M_DIAGNOSA CC WHERE AA.KDDIAGNOSA4 = CC.KDDIAGNOSA) FROM S_PENDAFTARAN_DIAGNOSA_MASTER AA WHERE A.KDPENDAFTARAN = AA.KDPENDAFTARAN GROUP BY AA.KDDIAGNOSA4) , '-') "
            SQL &= ",Diagnosa_5 = ISNULL((SELECT (SELECT CC.KDDIAGNOSA + '-' + CC.MEMO FROM M_DIAGNOSA CC WHERE AA.KDDIAGNOSA5 = CC.KDDIAGNOSA) FROM S_PENDAFTARAN_DIAGNOSA_MASTER AA WHERE A.KDPENDAFTARAN = AA.KDPENDAFTARAN GROUP BY AA.KDDIAGNOSA5) , '-') "
            SQL &= ",Diagnosa_6 = ISNULL((SELECT (SELECT CC.KDDIAGNOSA + '-' + CC.MEMO FROM M_DIAGNOSA CC WHERE AA.KDDIAGNOSA6 = CC.KDDIAGNOSA) FROM S_PENDAFTARAN_DIAGNOSA_MASTER AA WHERE A.KDPENDAFTARAN = AA.KDPENDAFTARAN GROUP BY AA.KDDIAGNOSA6) , '-') "
            SQL &= ",DokterDPJP = D.NAME_DISPLAY "
            SQL &= ",Poli = C.NAME_DISPLAY "
            SQL &= ",[User] = A.KDUSER "
            SQL &= ",JenisPasien = (SELECT CASE WHEN CONVERT(VARCHAR(8), A.DATE, 112) = CONVERT(VARCHAR(8), B.DATECREATED, 112) THEN 'PASIEN BARU' ELSE 'PASIEN LAMA' END) "
            SQL &= "FROM S_PENDAFTARAN_H A "
            SQL &= "INNER JOIN M_CUSTOMER B "
            SQL &= "On A.KDCUSTOMER = B.KDCUSTOMER "
            SQL &= "INNER JOIN M_DEPARTMENT C "
            SQL &= "On A.KDDEPARTMENT = C.KDDEPARTMENT "
            SQL &= "INNER JOIN M_DOCTOR D "
            SQL &= "On A.KDDOCTOR = D.KDDOCTOR "
            SQL &= "INNER JOIN M_DAFTAR_L4 E "
            SQL &= "On A.KDDAFTAR_L4 = E.KDDAFTAR_L4 "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.Date, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND A.CATEGORY = 0 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "I_MUTATION_D")

            grd.MainView = grv
            grd.DataSource = ds.Tables("I_MUTATION_D")
            grd.ForceInitialize()

            fn_LoadFormatData()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDataLayanan()
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

            SQL = "EXEC R_LAPORAN_BULANAN_RAWATJALAN @DATEFROM = '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "', @DATETO = '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "R_LAPORAN_BULANAN_RAWATJALAN")

            grd.MainView = grv
            grd.DataSource = ds.Tables("R_LAPORAN_BULANAN_RAWATJALAN")
            grd.ForceInitialize()

            fn_LoadFormatData()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    'Private Sub fn_LoadDataOnline()
    '    Try
    '        Dim oConnMySql As New MySql.Data.MySqlClient.MySqlConnection
    '        Dim oCommMySql As New MySql.Data.MySqlClient.MySqlCommand
    '        Dim daMySql As MySql.Data.MySqlClient.MySqlDataAdapter
    '        Dim dsMySql As New DataSet
    '        Dim MYSQL As String

    '        dsMySql = New DataSet

    '        oConnMySql = New MySqlConnection(sConnMySql)

    '        If oConnMySql.State = ConnectionState.Closed Then
    '            oConnMySql.Open()
    '        End If

    '        MYSQL = "SELECT  "
    '        MYSQL &= "* "
    '        MYSQL &= "FROM daftar_online "

    '        oCommMySql.Connection = oConnMySql
    '        oCommMySql.CommandText = MYSQL
    '        oCommMySql.CommandTimeout = 120
    '        oCommMySql.CommandType = CommandType.Text

    '        daMySql = New MySql.Data.MySqlClient.MySqlDataAdapter(oCommMySql)
    '        daMySql.Fill(dsMySql, "R_HISTORYRI")

    '        If oConnMySql.State = ConnectionState.Open Then
    '            oConnMySql.Close()
    '        End If

    '        'If dsMySql.Tables("R_HISTORYRI").Rows.Count < 1 Then

    '        'End If

    '        grd.MainView = grv
    '        grd.DataSource = dsMySql.Tables("R_HISTORYRI")
    '        grd.ForceInitialize()

    '        fn_LoadFormatData()
    '    Catch oErr As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
    Private Sub fn_LoadFormatData()
        For iLoop As Integer = 0 To grv.Columns.Count - 1
            If grv.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next
    End Sub
#End Region
    Private Sub MasterColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MasterColumnChooserToolStripMenuItem.Click
        grv.ShowCustomization()
    End Sub
    Private Sub DetailColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DetailColumnChooserToolStripMenuItem.Click
        grv1.ShowCustomization()
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
    End Sub
#End Region
End Class