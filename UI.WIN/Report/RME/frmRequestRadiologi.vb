Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports DevExpress.XtraPrinting
Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.XtraSplashScreen
Imports System.IO

Public Class frmRequestRadiologi
    Implements ILanguage

#Region "Function"
    Private normcek As String = String.Empty

    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "Request Radiologi"

        lTYPE.Text = Report.FILTER_TYPE
        lDATEFROM.Text = Report.FILTER_DATEFROM
        lDATETO.Text = Report.FILTER_DATETO

        cboTYPE.Properties.Items.Clear()
        cboTYPE.Properties.Items.Add("Belum di Terima")
        cboTYPE.Properties.Items.Add("Sudah di Terima")
        cboTYPE.Properties.Items.Add("Semua")
        cboTYPE.Properties.Items.Add("Hasil")
        cboTYPE.SelectedIndex = 0

        deDATEFrom.DateTime = Now.AddDays((-Now.Day) + 1)
        deDATETo.DateTime = Now
        grd.ContextMenuStrip = Nothing
    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        normcek = ""
    End Sub
    Public Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            Me.Text = "Request Radiologi"

            lTYPE.Text = Report.FILTER_TYPE
            lDATEFROM.Text = Report.FILTER_DATEFROM
            lDATETO.Text = Report.FILTER_DATETO

            cboTYPE.Properties.Items.Clear()
            cboTYPE.Properties.Items.Add("Belum di Terima")
            cboTYPE.Properties.Items.Add("Sudah di Terima")
            cboTYPE.Properties.Items.Add("Semua")
            cboTYPE.Properties.Items.Add("Hasil")

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
                      Where x.MODUL = "REQUEST_RADIOLOGI" _
                      And y.KDUSER = sUserID
                      Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                picPrint.Enabled = ds.ISPRINT
                picByhasilPasien.Enabled = ds.ISPRINT
                picRefresh.Enabled = ds.ISVIEW

                If ds.ISVIEW = True Then
                    sFind1 = ""
                    sFind2 = ""
                    fn_Preview()
                    fn_LoadLanguage()
                End If
            Catch oErr As Exception
                MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)

                picPrint.Enabled = False
                picByhasilPasien.Enabled = False
                picRefresh.Enabled = False
            End Try
        Catch oErr As Exception
            MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Print()
        Try
            If cboTYPE.SelectedIndex = 3 Then
                If grv.GetFocusedRowCellValue("KDSOTRANSAKSI") Is Nothing Then
                    Exit Sub
                End If

                If picHasil.Enabled = False Then Exit Sub
                Dim oExpertise As New Grouper.clsExpertise
                Dim oRME As New RME.clsRME

                Dim ds = oExpertise.GetData(grv.GetFocusedRowCellValue("KDSOTRANSAKSI"), CInt(grv.GetFocusedRowCellValue("SEQ_SO")))

                If ds IsNot Nothing Then
                    sUSIA = oRME.GetUmurPasien(ds.DATE, ds.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)

                    Dim rpt As New xtraExpertise

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK
                    rpt.bindingSource.DataSource = ds
                    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                    printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
                Else
                    MsgBox("Expertise Belum di Input", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
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
            {sWATERMARK, "", Report.REPORT_PAGE & " : [Page # of Pages #]"})

                phf.Header.Content.AddRange(New String() _
    {"Request Radiologi ", cboTYPE.Text & vbCrLf & deDATEFrom.DateTime.ToString("dd/MM/yyyy") & " - " & deDATETo.DateTime.ToString("dd/MM/yyyy"), ""})

                printableComponentLink.Component = grd
                printableComponentLink.CreateDocument()
                printableComponentLink.ShowPreviewDialog(Me)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Preview()
        Try
            grv.Columns.Clear()
            grd.DataSource = Nothing
            grv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            Select Case cboTYPE.SelectedIndex
                Case 0
                    fn_LoadData(0)
                Case 1
                    fn_LoadData(1)
                Case 2
                    fn_LoadData(2)
                Case 3
                    fn_LoadData()
            End Select

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#Region "All"
    Private Sub fn_LoadData(ByVal Kategori As Integer)
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

            SQL = "EXEC ORDERRADIOLOGI @CATEGORY = '" & Kategori & "', @DARITANGGAL = '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "', @SAMPAITANGGAL = '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

            grd.MainView = grv
            grd.DataSource = ds.Tables("ALL")
            grd.ForceInitialize()

            fn_LoadFormatDataAll()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grv.BestFitColumns()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
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
            SQL &= "B.KDKUNJUNGAN "
            SQL &= ",D.KDSOTRANSAKSI "
            SQL &= ",D.KDORDER "
            SQL &= ",E.KDITEM "
            SQL &= ",TANGGAL = D.DATE "
            SQL &= ",A.KDCUSTOMER "
            SQL &= ",NAMAPASIEN = C.NAME_DISPLAY "
            SQL &= ",DOKTERPENGIRIM = H.NAME_DISPLAY "
            SQL &= ",DOKTERRADIOLOGI = ISNULL((SELECT BB.NAME_DISPLAY FROM S_SO_TRANSAKSI_D_HASIL_RADIOLOGI_H AA INNER JOIN M_DOCTOR BB ON AA.KDDOCTOR = BB.KDDOCTOR WHERE D.KDSOTRANSAKSI = AA.KDSOTRANSAKSI AND E.SEQ = AA.SEQ) , 'BELUM') "
            SQL &= ",TINDAKAN = F.NMITEM2 "
            SQL &= ",BAYAR = (SELECT CASE D.PAYAMOUNT WHEN 0 THEN CONVERT(BIT, 0) ELSE CONVERT(BIT, 1) END) "
            SQL &= ",CATATANDOKTER = D.MEMO "
            SQL &= ",DIAGNOSAWAL = I.MEMO "
            SQL &= ",D.KDUSER "
            'SQL &= ",KELOMPOK = E.NMITEM1 "
            SQL &= ",SEQ_SO = E.SEQ "
            SQL &= ",E.SEQ "
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
            SQL &= "INNER JOIN M_DIAGNOSA I "
            SQL &= "ON A.KDDIAGNOSA = I.KDDIAGNOSA "
            SQL &= "WHERE CONVERT(VARCHAR(8), D.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), D.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND G.MEMO = 'RADIOLOGI' "
            SQL &= "ORDER BY B.DATE "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "PCR")

            grd.MainView = grv
            grd.DataSource = ds.Tables("PCR")
            grd.ForceInitialize()

            fn_LoadFormatData()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grv.BestFitColumns()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatDataAll()
        For iLoop As Integer = 0 To grv.Columns.Count - 1
            If grv.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm:ss}"
            End If
        Next

        'grv.Columns("KDCPPT").Visible = False
        'grv.Columns("KDCPPT").OptionsColumn.ShowInCustomizationForm = False

        Try
            grv.Columns("KDKUNJUNGAN").Visible = False
            grv.Columns("KDKUNJUNGAN").OptionsColumn.ShowInCustomizationForm = False
        Catch ex As Exception

        End Try
        Try
            grv.Columns("KDORDER").Visible = False
            grv.Columns("KDORDER").OptionsColumn.ShowInCustomizationForm = False
        Catch ex As Exception

        End Try
        Try
            grv.Columns("KDCPPT").Visible = False
            grv.Columns("KDCPPT").OptionsColumn.ShowInCustomizationForm = False
        Catch ex As Exception

        End Try
    End Sub
    Private Sub fn_LoadFormatData()
        For iLoop As Integer = 0 To grv.Columns.Count - 1
            If grv.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm:ss}"
            End If
        Next

        grv.Columns("KDKUNJUNGAN").Visible = False
        grv.Columns("KDKUNJUNGAN").OptionsColumn.ShowInCustomizationForm = False
        grv.Columns("KDSOTRANSAKSI").Visible = False
        grv.Columns("KDSOTRANSAKSI").OptionsColumn.ShowInCustomizationForm = False
        grv.Columns("KDITEM").Visible = False
        grv.Columns("KDITEM").OptionsColumn.ShowInCustomizationForm = False
        grv.Columns("KDDOCTOR").Visible = False
        grv.Columns("KDDOCTOR").OptionsColumn.ShowInCustomizationForm = False
        grv.Columns("SEQ_SO").Visible = False
        grv.Columns("SEQ_SO").OptionsColumn.ShowInCustomizationForm = False
        grv.Columns("SEQ").Visible = False
        grv.Columns("SEQ").OptionsColumn.ShowInCustomizationForm = False
    End Sub
#End Region
    Private Sub cboTYPE_SelectedIndexChanged() Handles cboTYPE.SelectedIndexChanged
        grd.ContextMenuStrip = Nothing

        If cboTYPE.SelectedIndex = 0 Then
            picPanggilDokterTaksId6.Enabled = True
            picHasil.Enabled = False
        ElseIf cboTYPE.SelectedIndex = 1 Then
            picPanggilDokterTaksId6.Enabled = False
            picHasil.Enabled = False
        ElseIf cboTYPE.SelectedIndex = 2 Then
            picPanggilDokterTaksId6.Enabled = False
            picHasil.Enabled = False
        Else
            grd.ContextMenuStrip = ContextMenuStrip1
            picPanggilDokterTaksId6.Enabled = False
            picHasil.Enabled = True
        End If
        fn_LoadSecurity()
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
    Private Sub picCPPT_Click() Handles picCPPT.Click
        normcek = ""

        If grv.GetFocusedRowCellValue("NOMORREKAMMEDIS") Is Nothing Then

        Else
            normcek = grv.GetFocusedRowCellValue("NOMORREKAMMEDIS")
        End If

        If grv.GetFocusedRowCellValue("KDCUSTOMER") Is Nothing Then

        Else
            normcek = grv.GetFocusedRowCellValue("KDCUSTOMER")
        End If

        If grv.GetFocusedRowCellValue("REKAMMEDIS") Is Nothing Then

        Else
            normcek = grv.GetFocusedRowCellValue("REKAMMEDIS")
        End If

        If normcek <> "" Then
            ContextMenuStrip2.Show(picPrint.Location.X, picPrint.Location.Y + 125)

        Else
            MsgBox("rekam medis kosong", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub picUpload_Click() Handles picUpload.Click
        If grv.GetFocusedRowCellValue("KDKUNJUNGAN") Is Nothing Then
            Exit Sub
        End If

        Dim oPendaftaranPDF As New Admission.clsPendaftaranPDF

        Dim dsKunjungan = oPendaftaranPDF.GetDataPendaftaranBykdKunjungan(grv.GetFocusedRowCellValue("KDKUNJUNGAN"))

        If dsKunjungan IsNot Nothing Then
            Dim ds = oPendaftaranPDF.GetData(dsKunjungan.KDPENDAFTARAN)
            If ds Is Nothing Then
                Dim frmGrouperPDF As New frmGrouperPDF
                Try
                    frmGrouperPDF.LoadMeRegistrasi(FORM_MODE.FORM_MODE_ADD, dsKunjungan.KDPENDAFTARAN)
                    frmGrouperPDF.ShowDialog(Me)
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
                Catch oErr As Exception
                    MsgBox("Add Document" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                Finally
                    If Not frmGrouperPDF Is Nothing Then frmGrouperPDF.Dispose()
                    frmGrouperPDF = Nothing
                End Try
            End If
        Else
            MsgBox("Kunjungan Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        Try
            fn_Print()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picByhasilPasien_Click() Handles picByhasilPasien.Click
        If picByhasilPasien.Enabled = False Then Exit Sub

        Dim NOrm As String = ""

        If grv.GetFocusedRowCellValue("NOMORREKAMMEDIS") Is Nothing Then

        Else
            NOrm = grv.GetFocusedRowCellValue("NOMORREKAMMEDIS")
        End If

        If grv.GetFocusedRowCellValue("KDCUSTOMER") Is Nothing Then

        Else
            NOrm = grv.GetFocusedRowCellValue("KDCUSTOMER")
        End If

        If grv.GetFocusedRowCellValue("REKAMMEDIS") Is Nothing Then

        Else
            NOrm = grv.GetFocusedRowCellValue("REKAMMEDIS")
        End If

        Dim FolderSimpan = "C:/Source/RMEHASILRAD/"
        Dim oRME As New RME.clsRME
        Dim oSalesOrder As New Sales.clsSalesOrderTransaksi

        If Not IO.Directory.Exists(FolderSimpan) Then
            IO.Directory.CreateDirectory(FolderSimpan)
        Else
            oRME.DeleteDirectory(FolderSimpan)
            IO.Directory.CreateDirectory(FolderSimpan)
        End If

        Dim oExpertise As New Grouper.clsExpertise
        'Dim oRME As New RME.clsRME
        Dim arrfname2 As New List(Of String)()

        For Each xloop In oExpertise.GetDataList(NOrm)
            Dim ds = oExpertise.GetData(xloop.KDSOTRANSAKSI, xloop.SEQ)
            Dim Alamat2 As String = FolderSimpan & xloop.SEQ & xloop.KDSOTRANSAKSI & Now.ToString("yyyyMMddHHmmss") & ".pdf"

            If ds IsNot Nothing Then
                sUSIA = oRME.GetUmurPasien(ds.DATE, ds.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)

                Dim rpt As New xtraExpertise

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK
                rpt.bindingSource.DataSource = ds


                rpt.ExportToPdf(Alamat2)

                If FileIO.FileSystem.FileExists(Alamat2) Then
                    arrfname2.Add(Alamat2)
                End If
                'Else
                '    MsgBox("Expertise Belum di Input", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Next

        Dim sinputFiles2() As String = {}
        For Each ifname In arrfname2
            sinputFiles2 = AppendArray(sinputFiles2, ifname)
        Next

        Dim AlamatMerge As String = FolderSimpan & Now.ToString("yyyyMMddHHmmss") & "XZHASIL" & ".pdf"

        frmMedrekRawatNew2JalanList.MergePdfFiles(sinputFiles2, AlamatMerge)

        If FileIO.FileSystem.FileExists(AlamatMerge) Then
            'If System.IO.File.Exists(AlamatMerge) Then
            '    PdfViewerAsesmenMedis.LoadDocument(AlamatMerge)
            'End If
            Dim frmPopPDF As New frmPopPDF
            Try
                frmPopPDF.fn_LoadMe(AlamatMerge)
                frmPopPDF.ShowDialog(Me)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmPopPDF Is Nothing Then frmCustomer.Dispose()
                frmPopPDF = Nothing
            End Try
        Else
            MsgBox("Belum Ada Hasil untuk Pasien ini", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Function AppendArray(Of T)(ByVal thisArray() As T, ByVal itemToAppend As T) As T()
        If thisArray Is Nothing Then thisArray = New T() {}
        Dim tempList As List(Of T) = thisArray.ToList
        tempList.Add(itemToAppend)
        Return tempList.ToArray
    End Function
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_LoadSecurity()

        'Try
        '    grv.RestoreLayoutFromRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text & "\" & cboTYPE.Text)
        'Catch ex As Exception

        'End Try
    End Sub
    Private Sub picAksiBilling_PACS_Click(sender As Object, e As EventArgs) Handles picAksiBilling_PACS.Click
        Try
            If grv.GetFocusedRowCellValue("KDSOTRANSAKSI") Is Nothing Then
                Exit Sub
            End If
            If grv.GetFocusedRowCellValue("SEQ") Is Nothing Then
                MsgBox("SEQ Kosong", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            If grv.GetFocusedRowCellValue("KDCUSTOMER") Is Nothing Then
                MsgBox("RM Kosong", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            If grv.GetFocusedRowCellValue("CATEGORY") = 2 Then
                MsgBox("Tidak dapat di Update Merupakan Penunjang", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            If grv.GetFocusedRowCellValue("CATEGORY") = 3 Then
                MsgBox("Tidak dapat di Update Merupakan Penunjang", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            If grv.GetFocusedRowCellValue("CATEGORY") = 4 Then
                MsgBox("Tidak dapat di Update Merupakan Penunjang", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            frmReportLoadPasienPACS.fn_LoadRM(grv.GetFocusedRowCellValue("KDCUSTOMER"), grv.GetFocusedRowCellValue("KDSOTRANSAKSI"), grv.GetFocusedRowCellValue("SEQ"))
            frmReportLoadPasienPACS.ShowDialog(Me)
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmReportLoadPasienPACS Is Nothing Then frmReportLoadPasienPACS.Dispose()
            frmReportLoadPasienPACS = Nothing
        End Try
    End Sub
    Private Sub picPanggilDokterTaksId6_Click(sender As Object, e As EventArgs) Handles picPanggilDokterTaksId6.Click
        If grv.GetFocusedRowCellValue("KDCPPT") Is Nothing Then
            Exit Sub
        End If

        'If Not grv.GetFocusedRowCellValue("KDORDER").ToString.Contains("RAD") Then
        '    MsgBox("Kode Order Bukan Radiologi", MsgBoxStyle.Exclamation, Me.Text)
        '    Exit Sub
        'End If

        If picPanggilDokterTaksId6.Enabled = False Then Exit Sub

        Dim frmSalesOrderTransaksi As New frmSalesOrderTransaksi
        Try
            sFind1 = grv.GetFocusedRowCellValue("KDCPPT")
            sFind2 = grv.GetFocusedRowCellValue("KDORDER")

            frmSalesOrderTransaksi.LoadMe(FORM_MODE.FORM_MODE_ADD, 3)
            frmSalesOrderTransaksi.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            sFind1 = ""
            sFind2 = ""

            If Not frmSalesOrderTransaksi Is Nothing Then frmSalesOrderTransaksi.Dispose()
            frmSalesOrderTransaksi = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDSOTRANSAKSI"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
            End If
        End Try
    End Sub
    Private Sub picHasil_Click(sender As Object, e As EventArgs) Handles picHasil.Click
        If grv.GetFocusedRowCellValue("KDSOTRANSAKSI") Is Nothing Then
            Exit Sub
        End If

        If picHasil.Enabled = False Then Exit Sub
        Dim oExpertise As New Grouper.clsExpertise

        Dim ds = oExpertise.GetData(grv.GetFocusedRowCellValue("KDSOTRANSAKSI"), CInt(grv.GetFocusedRowCellValue("SEQ_SO")))

        sFind2 = grv.GetFocusedRowCellValue("KDORDER")

        If ds IsNot Nothing Then
            Dim frmExpertise As New frmExpertise
            Try
                frmExpertise.LoadMe(FORM_MODE.FORM_MODE_EDIT, grv.GetFocusedRowCellValue("KDSOTRANSAKSI"), CInt(grv.GetFocusedRowCellValue("SEQ_SO")), grv.GetFocusedRowCellValue("KDDOCTOR"), grv.GetFocusedRowCellValue("KDITEM"))
                frmExpertise.ShowDialog(Me)
                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmExpertise Is Nothing Then frmExpertise.Dispose()
                frmExpertise = Nothing

                Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDSOTRANSAKSI"), sCode)
                If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

                If sStatusSave = "NEW" Then
                    sStatusSave = "NONE"
                End If
            End Try
        Else
            Dim frmExpertise As New frmExpertise
            Try
                frmExpertise.LoadMe(FORM_MODE.FORM_MODE_ADD, grv.GetFocusedRowCellValue("KDSOTRANSAKSI"), grv.GetFocusedRowCellValue("SEQ_SO"), grv.GetFocusedRowCellValue("KDDOCTOR"), grv.GetFocusedRowCellValue("KDITEM"))
                frmExpertise.ShowDialog(Me)
                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmExpertise Is Nothing Then frmExpertise.Dispose()
                frmExpertise = Nothing

                Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDSOTRANSAKSI"), sCode)
                If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

                If sStatusSave = "NEW" Then
                    sStatusSave = "NONE"
                End If
            End Try
        End If

        sFind2 = ""

    End Sub
    Private Sub DeleteHasilToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteHasilToolStripMenuItem.Click
        If grv.GetFocusedRowCellValue("KDSOTRANSAKSI") Is Nothing Then
            Exit Sub
        End If

        If grv.GetFocusedRowCellValue("DOKTERRADIOLOGI") = "BELUM" Then Exit Sub

        If MsgBox("Apakah yakin akan hapus expertise?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        Dim oDelete As New Setting.clsDelete
        Dim frmPesanDelete As New frmPesanDelete
        frmPesanDelete.ShowDialog(Me)

        If sPesanHapus <> "XXXXXBATALXXXXX" Then
            If oDelete.InsertData("EXPERTISE", sUserID, sPesanHapus, grv.GetFocusedRowCellValue("KDSOTRANSAKSI")) = True Then
                Dim oExpertise As New Grouper.clsExpertise
                If oExpertise.DeleteData(grv.GetFocusedRowCellValue("KDSOTRANSAKSI"), grv.GetFocusedRowCellValue("SEQ_SO")) = False Then
                    MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                End If
                MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
                fn_LoadSecurity()
            Else
                MsgBox(Statement.ErrorStatement & vbCrLf & "Gagal Simpan Delete", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Else
            MsgBox(Statement.ErrorStatement & vbCrLf & "Batal dihapus", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    'Private Sub grv_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grv.RowStyle
    '    If grv.IsFilterRow(e.RowHandle) Then Exit Sub
    '    If grv.GetFocusedRowCellValue("DOKTERRADIOLOGI") = "BELUM" Then
    '        e.Appearance.BackColor = Color.LightGreen
    '    End If
    'End Sub
    Private Sub fn_LoadHistoryPasienCPPT(ByVal sKDCUSTOMER As String)
        Try
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
            SQL &= "AND ISNULL((SELECT MEMO FROM R_CPPT_M_PROFESI WHERE B.KDPROFESI = KDPROFESI), '') LIKE '%DOKTER%' "

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

            sFind1_cppt = String.Empty
            sFind2_cppt = String.Empty
            sFind3_cppt = String.Empty

            If listCPPT.Count > 0 Then
                Dim oCustomer As New Reference.clsCustomer
                Dim dsCustomer = oCustomer.GetData(sKDCUSTOMER)
                If dsCustomer IsNot Nothing Then
                    sFind1_cppt = dsCustomer.KDCUSTOMER
                    sFind2_cppt = dsCustomer.NAME_DISPLAY
                    sFind3_cppt = dsCustomer.TANGGALLAHIR.ToString("dd-MM-yyyy")
                End If


                Dim FolderSimpan = "C:/SIMRS/CPPTDIRADIOLOGI/"

                If Not Directory.Exists(FolderSimpan) Then
                    Directory.CreateDirectory(FolderSimpan)
                Else
                    Dim oRME As New RME.clsRME
                    oRME.DeleteDirectory(FolderSimpan)
                    Directory.CreateDirectory(FolderSimpan)
                End If

                Dim AlamatCPPT As String = FolderSimpan & sKDCUSTOMER & Now.ToString("yyyyMMddHHmmss") & ".pdf"

                Dim rpt As New xtraDigital_CPPT_01_QR

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.bindingSource.DataSource = listCPPT.OrderByDescending(Function(x) x.DATE)
                rpt.ExportToPdf(AlamatCPPT)

                If FileIO.FileSystem.FileExists(AlamatCPPT) Then
                    Dim frmPopPDF As New frmPopPDF
                    Try
                        frmPopPDF.fn_LoadMe(AlamatCPPT)
                        frmPopPDF.ShowDialog(Me)
                    Catch oErr As Exception
                        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    Finally
                        If Not frmPopPDF Is Nothing Then frmCustomer.Dispose()
                        frmPopPDF = Nothing
                    End Try
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
    Private Sub fn_LoadPdfViewerCPPTRANANP(ByVal sKDCUSTOMER As String)
        Try
            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data CPPT Rawat Inap.....")

            Dim FolderSimpan = "C:/CPPTRANAPDIRADIOLOGI/"

            If Not Directory.Exists(FolderSimpan) Then
                Directory.CreateDirectory(FolderSimpan)
            Else
                Dim oRME As New RME.clsRME
                oRME.DeleteDirectory(FolderSimpan)
                Directory.CreateDirectory(FolderSimpan)
            End If

            Dim oCPPT As New Transaksi.clsCPPT
            Dim ds = oCPPT.GetDataByRMRANAP(sKDCUSTOMER)

            If ds.Count > 0 Then
                Dim rpt As New xtraDigital_CPPT_01_RawatInap

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.bindingSource.DataSource = ds
                rpt.ExportToPdf(FolderSimpan & "HASIL" & ".pdf")
                'dataList.Add(File.ReadAllBytes(FolderSimpan & "HASIL" & ".pdf"))

                If FileIO.FileSystem.FileExists(FolderSimpan & "HASIL" & ".pdf") Then
                    Dim frmPopPDF As New frmPopPDF
                    Try
                        frmPopPDF.fn_LoadMe(FolderSimpan & "HASIL" & ".pdf")
                        frmPopPDF.ShowDialog(Me)
                    Catch oErr As Exception
                        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    Finally
                        If Not frmPopPDF Is Nothing Then frmCustomer.Dispose()
                        frmPopPDF = Nothing
                    End Try
                End If

            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak CPPT Ranap" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub RawatJalanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RawatJalanToolStripMenuItem.Click
        fn_LoadHistoryPasienCPPT(normcek)
    End Sub
    Private Sub RawatInapToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RawatInapToolStripMenuItem.Click
        fn_LoadPdfViewerCPPTRANANP(normcek)
    End Sub
#End Region
End Class