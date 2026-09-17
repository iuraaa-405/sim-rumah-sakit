Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports DevExpress.XtraPrinting
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization

Public Class frmRequestResep
    Implements ILanguage

#Region "Function"
    Private TextOrder As String = "BELUM ADA ORDER"
    Private oOrder As New Digital.clsR_Order
    Private oBilling As New Sales.clsSalesOrderTransaksi
    Private oSuara As New Digital.clsSuaraKeFarmasi

    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sStok = True

        Me.Text = "Request Farmasi"

        lTYPE.Text = Report.FILTER_TYPE
        lDATEFROM.Text = Report.FILTER_DATEFROM
        lDATETO.Text = Report.FILTER_DATETO

        cboTYPE.Properties.Items.Clear()
        cboTYPE.Properties.Items.Add("Belum di Baca")
        cboTYPE.Properties.Items.Add("Sudah di Baca")
        cboTYPE.Properties.Items.Add("Semua")
        cboTYPE.Properties.Items.Add("Hasil")
        'cboTYPE.Properties.Items.Add("Semua Lama")
        cboTYPE.SelectedIndex = 0

        'deDATEFrom.DateTime = Now.AddDays((-Now.Day) + 1)
        deDATEFrom.DateTime = Now
        deDATETo.DateTime = Now

    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        sFind1 = ""
        sFind2 = ""
    End Sub
    Public Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            Me.Text = "Request Farmasi"

            lTYPE.Text = Report.FILTER_TYPE
            lDATEFROM.Text = Report.FILTER_DATEFROM
            lDATETO.Text = Report.FILTER_DATETO

            cboTYPE.Properties.Items.Clear()
            cboTYPE.Properties.Items.Add("Belum di Baca")
            cboTYPE.Properties.Items.Add("Sudah di Baca")
            cboTYPE.Properties.Items.Add("Semua")
            cboTYPE.Properties.Items.Add("Hasil")
            'cboTYPE.Properties.Items.Add("Semua Lama")
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
                      Where x.MODUL = "REQUEST_RESEP" _
                      And y.KDUSER = sUserID
                      Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                picPrint.Enabled = ds.ISPRINT
                picRefresh.Enabled = ds.ISVIEW
                picApotekOnline.Enabled = ds.ISADD

                If ds.ISVIEW = True Then
                    sFind1 = ""
                    sFind2 = ""
                    fn_Preview()
                    fn_LoadLanguage()
                End If
            Catch oErr As Exception
                MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)

                picPrint.Enabled = False
                picRefresh.Enabled = False
                picApotekOnline.Enabled = False
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
{"Request Farmasi ", cboTYPE.Text & vbCrLf & deDATEFrom.DateTime.ToString("dd/MM/yyyy") & " - " & deDATETo.DateTime.ToString("dd/MM/yyyy"), ""})

            PrintableComponentLink.Component = grd
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

            Select Case cboTYPE.SelectedIndex
                Case 0
                    fn_LoadData(0)
                Case 1
                    fn_LoadData(1)
                Case 2
                    fn_LoadData(2)
                Case 3
                    fn_LoadData()
                    'fn_LoadData(3)
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

            SQL = "EXEC ORDERFARMASI @CATEGORY = '" & Kategori & "', @DARITANGGAL = '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "', @SAMPAITANGGAL = '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

            grd.MainView = grv
            grd.DataSource = ds.Tables("ALL")
            grd.ForceInitialize()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            fn_LoadFormatDataAll()
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
    End Sub
#End Region
    Private Sub cboTYPE_SelectedIndexChanged() Handles cboTYPE.SelectedIndexChanged
        If cboTYPE.SelectedIndex = 0 Then
            btnTerima.Visible = False
            lpicPanggilDokterTaksId6.Text = "Terima Resep" & vbCrLf & "Taks Id 6 RJ"
            'picPanggilDokterTaksId6.Visible = True
            'lpicPanggilDokterTaksId6.Visible = True
            btnTelaah2.Visible = False

            If chkAuto.Checked = False Then
                Timer1.Stop()
            Else
                lblCekOrder.Left = Panel1.Width
                Timer1.Start()
            End If
        Else
            Timer1.Stop()
            btnTerima.Visible = True
            lpicPanggilDokterTaksId6.Text = "Panggil Resep" & vbCrLf & "Taks Id 7 RJ"
            btnTelaah2.Visible = True
            'picPanggilDokterTaksId6.Visible = False
            'lpicPanggilDokterTaksId6.Visible = False
        End If
        fn_LoadSecurity()
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
            'SQL &= ",DOKTERRADIOLOGI = ISNULL((SELECT BB.NAME_DISPLAY FROM S_SO_TRANSAKSI_D_HASIL_RADIOLOGI_H AA INNER JOIN M_DOCTOR BB ON AA.KDDOCTOR = BB.KDDOCTOR WHERE D.KDSOTRANSAKSI = AA.KDSOTRANSAKSI AND E.SEQ = AA.SEQ) , 'BELUM') "
            SQL &= ",TINDAKAN = F.NMITEM2 "
            SQL &= ",BAYAR = (SELECT CASE D.PAYAMOUNT WHEN 0 THEN CONVERT(BIT, 0) ELSE CONVERT(BIT, 1) END) "
            SQL &= ",CATATANDOKTER = D.MEMO "
            SQL &= ",DIAGNOSAWAL = I.MEMO "
            SQL &= ",D.KDUSER "
            'SQL &= ",KELOMPOK = E.NMITEM1 "
            SQL &= ",SEQ_SO = E.SEQ "
            SQL &= ",E.KDDOCTOR "
            SQL &= ",RESEPPULANG = '-' "
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
            SQL &= "AND F.ISSTOK = 1 "
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

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_LoadDataSuara(ByVal MEMO1 As String, ByVal MEMO2 As String) As Integer
        Try
            fn_LoadDataSuara = 0

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
            SQL &= "FROM "
            SQL &= "DATABASERME..A_SUARA A "
            If MEMO1 = "" And MEMO2 = "" Then

            Else
                If MEMO2 = "" Then
                    SQL &= "WHERE A.MEMO LIKE '" & MEMO1 & "' "
                Else
                    SQL &= "WHERE A.MEMO LIKE '" & MEMO1 & "' OR A.MEMO LIKE '" & MEMO2 & "' "
                End If
            End If

            SQL &= "ORDER BY "
            SQL &= "A.KDSUARA ASC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "A_SUARA")

            With ds.Tables("A_SUARA")
                If .Rows.Count > 0 Then
                    For iLoop As Integer = 0 To .Rows.Count - 1
                        fn_LoadDataSuara = .Rows(iLoop)("KDSUARA").ToString()
                    Next
                End If
            End With

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            Timer1.Stop()
            chkAuto.Checked = False
            'MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
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

        'grv.Columns("KDSOTRANSAKSI").Visible = False
        'grv.Columns("KDSOTRANSAKSI").OptionsColumn.ShowInCustomizationForm = False
        grv.Columns("KDITEM").Visible = False
        grv.Columns("KDITEM").OptionsColumn.ShowInCustomizationForm = False
        grv.Columns("KDDOCTOR").Visible = False
        grv.Columns("KDDOCTOR").OptionsColumn.ShowInCustomizationForm = False
        grv.Columns("SEQ_SO").Visible = False
        grv.Columns("SEQ_SO").OptionsColumn.ShowInCustomizationForm = False
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
        '    grv.RestoreLayoutFromRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text & "\" & cboTYPE.Text)
        'Catch ex As Exception

        'End Try
    End Sub
    Private Sub picRiwayat_Click() Handles picRiwayat.Click
        If grv.GetFocusedRowCellValue("REKAMMEDIS") Is Nothing Then
            MsgBox("Nomor Rekam Medis Kosong!", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim frmRiwayatResep As New frmRiwayatResep
        Try
            frmRiwayatResep.LoadMe(grv.GetFocusedRowCellValue("REKAMMEDIS"), 1)
            frmRiwayatResep.ShowDialog(Me)

        Catch ex As Exception
            MsgBox("Load Form Riwayat Resep : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picApotekOnline_Click() Handles picApotekOnline.Click
        Dim kartuBPSJS As String = String.Empty
        If grv.GetFocusedRowCellValue("KARTUBPJS") Is Nothing Then

        Else
            kartuBPSJS = grv.GetFocusedRowCellValue("KARTUBPJS")
        End If

        Dim frmReferensiApotekOnline As New frmReferensiApotekOnline

        Try
            frmReferensiApotekOnline.fn_LoadKategori(8, kartuBPSJS)
            frmReferensiApotekOnline.ShowDialog(Me)
            frmReferensiApotekOnline.WindowState = FormWindowState.Normal
        Catch ex As Exception
            MsgBox("Load Apotek Online : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picPanggilDokterTaksId6_Click(sender As Object, e As EventArgs) Handles picPanggilDokterTaksId6.Click
        If grv.GetFocusedRowCellValue("NOMORREFERENCE") Is Nothing Then
            Exit Sub
        End If

        Timer1.Stop()

        If cboTYPE.SelectedIndex = 0 Then
            Dim kodekunjungan As String = grv.GetFocusedRowCellValue("KDKUNJUNGAN")

            Dim frmSalesOrderTransaksi As New frmSalesOrderTransaksi
            Try
                sFind1 = grv.GetFocusedRowCellValue("NOMORREFERENCE")
                sFind2 = grv.GetFocusedRowCellValue("KDORDER")

                Dim dsOrder = oOrder.GetDataAntrianFarmasi(sFind2)

                If dsOrder Is Nothing Then
                    ' ***** HEADER *****
                    Dim ds = oOrder.GetStructureHeaderFarmasi

                    With ds
                        .DATECREATED = Now
                        .DATEUPDATED = Now
                        .KDORDER = sFind2
                        .NOMORANTRIAN = ""
                        .ANGKAANTRIAN = 0
                        .MEMO = sUserID
                    End With

                    Dim KodeNomor As String = String.Empty

                    If sFind2.Contains("FARRJ") Then
                        KodeNomor = "FARRJ-K"
                    ElseIf sFind2.Contains("RARRJ") Then
                        KodeNomor = "RARRJ-K"
                    ElseIf sFind2.Contains("FARRI") Then
                        KodeNomor = "FARRI-K"
                    Else
                        KodeNomor = "RARRI-K"
                    End If

                    oOrder.InsertDataFarmasi(KodeNomor, ds)

                End If

                Dim oAdmisi As New Admission.clsPendaftaran_Kunjungan
                Dim dsAdmisi = oAdmisi.GetData(kodekunjungan)

                If dsAdmisi IsNot Nothing Then
                    If dsAdmisi.S_PENDAFTARAN_H.KODEBOOKING <> "" Then
                        Dim dsBilling = oBilling.GetDataByKDkunjunganFarmasi(kodekunjungan)
                        If dsBilling Is Nothing Then
                            frmErmList.fn_TaskID(dsAdmisi.S_PENDAFTARAN_H.KODEBOOKING, 6)
                        Else
                            frmErmList.fn_TaskIDyangLalu(dsAdmisi.S_PENDAFTARAN_H.KODEBOOKING, 6, dsBilling.DATECREATED)
                        End If
                    End If
                End If

                Dim oSalesOrderTransaksi As New Sales.clsSalesOrderTransaksi

                Dim dsCek = oSalesOrderTransaksi.GetDataByOrderFarmasi(sFind2)

                If dsCek Is Nothing Then
                    frmSalesOrderTransaksi.LoadMe(FORM_MODE.FORM_MODE_ADD, 4)
                    frmSalesOrderTransaksi.ShowDialog(Me)
                Else
                    frmSalesOrderTransaksi.LoadMe(FORM_MODE.FORM_MODE_EDIT, 4, dsCek.KDSOTRANSAKSI)
                    frmSalesOrderTransaksi.ShowDialog(Me)
                End If

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

                If chkAuto.Checked = False Then
                    Timer1.Stop()
                Else
                    lblCekOrder.Left = Panel1.Width
                    Timer1.Start()
                End If
            End Try
        Else
            Try
                Dim oSet_panggilan As New AntrianRS.clsSetPanggilSET_PANGGIL_ANTRIAN
                Dim kodekunjungan As String = grv.GetFocusedRowCellValue("KDKUNJUNGAN")
                Dim KDORDER As String = grv.GetFocusedRowCellValue("KDORDER")

                Dim oAdmisi As New Admission.clsPendaftaran_Kunjungan
                Dim dsAdmisi = oAdmisi.GetData(kodekunjungan)

                If dsAdmisi IsNot Nothing Then
                    Try
                        Dim dsOrder = oOrder.GetDataAntrianFarmasi(KDORDER)
                        If dsOrder IsNot Nothing Then
                            ' ***** HEADER *****
                            Dim dsSetPanggilan = oSet_panggilan.GetDataFarmasi(dsAdmisi.S_PENDAFTARAN_H.M_DEPARTMENT.VCLAIM_KODEPOLI)

                            Dim ds = oSet_panggilan.GetStructureHeaderFarmasi

                            With ds
                                .LOKET = dsAdmisi.S_PENDAFTARAN_H.M_DEPARTMENT.VCLAIM_KODEPOLI
                                .KODE = dsOrder.R_ORDER.JENISORDER
                                .NOMORANTRIAN = CInt(Microsoft.VisualBasic.Right(dsOrder.NOMORANTRIAN, 3))
                                '.DESCRIPTION = "ANTRIAN FARMASI ATAS NAMA " & dsAdmisi.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY
                                .DESCRIPTION = "Antrian Farmasi Nomor " & CInt(Microsoft.VisualBasic.Right(dsOrder.NOMORANTRIAN, 3)) & " atas nama " & CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dsAdmisi.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY.ToLower())
                                .ISPANGGIL = False
                                .SISA = 0
                            End With

                            If dsSetPanggilan Is Nothing Then
                                oSet_panggilan.InsertDataFarmasi(ds)
                            Else
                                oSet_panggilan.UpdateDataFarmasi(ds)
                            End If

                            Dim oDigitalOrder As New Digital.clsR_Order
                            oDigitalOrder.UpdateStatus(KDORDER, "PANGGIL")

                            MsgBox("Berhasil Panggil", MsgBoxStyle.Exclamation, Me.Text)

                        Else
                            MsgBox("Pasien Belum Ambil Antrian", MsgBoxStyle.Exclamation, Me.Text)
                        End If
                    Catch oErr As Exception
                        MsgBox("Simpan Panggilan" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try

                    If dsAdmisi.S_PENDAFTARAN_H.KODEBOOKING <> "" Then
                        Dim dsBilling = oBilling.GetDataByKDkunjunganFarmasi(kodekunjungan)

                        If dsBilling Is Nothing Then
                            frmErmList.fn_TaskID(dsAdmisi.S_PENDAFTARAN_H.KODEBOOKING, 7)
                        Else
                            Dim Rnd As New Random()
                            frmErmList.fn_TaskIDyangLalu(dsAdmisi.S_PENDAFTARAN_H.KODEBOOKING, 7, dsBilling.DATECREATED.AddMinutes(Rnd.Next(20, 30)))
                        End If
                    End If
                End If

                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            If cboMEMO.Text = "IGD" Then
                Dim kdsuara As Integer = fn_LoadDataSuara("IGD", "")
                If kdsuara <> 0 Then
                    '' Beep standar
                    'Beep()

                    '' Beep dengan frekuensi dan durasi (Windows Forms)
                    'Console.Beep(1000, 500)  ' 1000 Hz selama 500 ms

                    My.Computer.Audio.Play("C:\notif.wav")
                    oSuara.HapusSudahPanggil(kdsuara)
                End If
            ElseIf cboMEMO.Text = "Rajal"
                Dim kdsuara As Integer = fn_LoadDataSuara("FARRJ", "")
                If kdsuara <> 0 Then
                    ' Beep standar
                    'Beep()

                    '' Beep dengan frekuensi dan durasi (Windows Forms)
                    'Console.Beep(1000, 500)  ' 1000 Hz selama 500 ms

                    My.Computer.Audio.Play("C:\notif.wav")
                    oSuara.HapusSudahPanggil(kdsuara)
                End If
            ElseIf cboMEMO.Text = "Rajal dan IGD"
                Dim kdsuara As Integer = fn_LoadDataSuara("FARRJ", "IGD")
                If kdsuara <> 0 Then
                    '' Beep standar
                    'Beep()

                    '' Beep dengan frekuensi dan durasi (Windows Forms)
                    'Console.Beep(1000, 500)  ' 1000 Hz selama 500 ms

                    My.Computer.Audio.Play("C:\notif.wav")
                    oSuara.HapusSudahPanggil(kdsuara)
                End If
            ElseIf cboMEMO.Text = "Ranap"
                Dim kdsuara As Integer = fn_LoadDataSuara("FARRI", "")
                If kdsuara <> 0 Then
                    '' Beep standar
                    'Beep()

                    '' Beep dengan frekuensi dan durasi (Windows Forms)
                    'Console.Beep(1000, 500)  ' 1000 Hz selama 500 ms

                    My.Computer.Audio.Play("C:\notif.wav")
                    oSuara.HapusSudahPanggil(kdsuara)
                End If
            ElseIf cboMEMO.Text = "Ranap dan IGD"
                Dim kdsuara As Integer = fn_LoadDataSuara("FARRI", "IGD")
                If kdsuara <> 0 Then
                    '' Beep standar
                    'Beep()

                    '' Beep dengan frekuensi dan durasi (Windows Forms)
                    'Console.Beep(1000, 500)  ' 1000 Hz selama 500 ms

                    My.Computer.Audio.Play("C:\notif.wav")
                    oSuara.HapusSudahPanggil(kdsuara)
                End If
            Else
                Dim kdsuara As Integer = fn_LoadDataSuara("", "")
                If kdsuara <> 0 Then
                    '' Beep standar
                    'Beep()

                    '' Beep dengan frekuensi dan durasi (Windows Forms)
                    'Console.Beep(1000, 500)  ' 1000 Hz selama 500 ms

                    My.Computer.Audio.Play("C:\notif.wav")
                    oSuara.HapusSudahPanggil(kdsuara)
                End If
            End If
        Catch ex As Exception
            Timer1.Stop()
            chkAuto.Checked = False
        End Try
    End Sub
    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Dim dsOrder = oOrder.GetDataTerakhir()
        If dsOrder IsNot Nothing Then
            lblCekOrder.Text = dsOrder.A_IDENTITASPASIEN_LIST.KDDEPARTMENT_NAMA & " ORDER"
        End If

        If lblCekOrder.ForeColor = Color.Red Then
            lblCekOrder.ForeColor = Color.Black
        Else
            lblCekOrder.ForeColor = Color.Red
        End If

        ' geser label ke kiri
        lblCekOrder.Left += -5

        ' kalau sudah habis ke kiri, kembalikan ke kanan panel
        If lblCekOrder.Right < 0 Then
            lblCekOrder.Left = Panel1.Width
        End If
    End Sub
    Private Sub chkAuto_CheckedChanged(sender As Object, e As EventArgs) Handles chkAuto.CheckedChanged
        If chkAuto.Checked = False Then
            Timer1.Stop()
            Timer2.Stop()
        Else
            lblCekOrder.Left = Panel1.Width
            Timer1.Start()
            Timer2.Start()
        End If
    End Sub
    Private Sub btnTelaah2_Click(sender As Object, e As EventArgs) Handles btnTelaah2.Click
        If grv.GetFocusedRowCellValue("KDSOTRANSAKSI") Is Nothing Then
            MsgBox("Kode Transaksi Kosong!", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim KDORDER As String = grv.GetFocusedRowCellValue("KDORDER")

        Dim oDigitalOrder As New Digital.clsR_Order
        oDigitalOrder.UpdateStatus(KDORDER, "TELAAH2")

        Dim oSalesOrderTransaksi As New Sales.clsSalesOrderTransaksi

        Dim dsCekTelaahResep = oSalesOrderTransaksi.GetDataTelaahResep(grv.GetFocusedRowCellValue("KDSOTRANSAKSI"))
        If dsCekTelaahResep Is Nothing Then
            Dim frmTelaahResep As New frmTelaahResep
            Try
                frmTelaahResep.LoadMe(FORM_MODE.FORM_MODE_ADD, grv.GetFocusedRowCellValue("KDSOTRANSAKSI"))
                frmTelaahResep.ShowDialog(Me)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmTelaahResep Is Nothing Then frmTelaahResep.Dispose()
                frmTelaahResep = Nothing
            End Try
        Else
            Dim frmTelaahResep As New frmTelaahResep
            Try
                frmTelaahResep.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsCekTelaahResep.KDSOTRANSAKSI)
                frmTelaahResep.ShowDialog(Me)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmTelaahResep Is Nothing Then frmTelaahResep.Dispose()
                frmTelaahResep = Nothing
            End Try
        End If

        fn_LoadSecurity()
    End Sub
    Private Sub grv_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grv.RowStyle
        If grv.IsFilterRow(e.RowHandle) Then Exit Sub

        If grv.GetRowCellValue(e.RowHandle, "RESEPPULANG") = "RESEP PULANG" Then
            e.Appearance.BackColor = Color.Red
        Else
            If grv.GetRowCellValue(e.RowHandle, "STATUS") = "TELAAH2" Then
                e.Appearance.BackColor = Color.Yellow
            ElseIf grv.GetRowCellValue(e.RowHandle, "STATUS") = "PANGGIL" Then
                e.Appearance.BackColor = Color.LightPink
            End If
        End If

    End Sub
    Private Sub btnTerima_Click(sender As Object, e As EventArgs) Handles btnTerima.Click
        If grv.GetFocusedRowCellValue("KDORDER") <> "" Then

            Dim kodekunjungan As String = grv.GetFocusedRowCellValue("KDKUNJUNGAN")

            Dim frmSalesOrderTransaksi As New frmSalesOrderTransaksi
            Try
                sFind1 = grv.GetFocusedRowCellValue("NOMORREFERENCE")
                sFind2 = grv.GetFocusedRowCellValue("KDORDER")

                Dim dsOrder = oOrder.GetDataAntrianFarmasi(sFind2)

                If dsOrder Is Nothing Then
                    ' ***** HEADER *****
                    Dim ds = oOrder.GetStructureHeaderFarmasi

                    With ds
                        .DATECREATED = Now
                        .DATEUPDATED = Now
                        .KDORDER = sFind2
                        .NOMORANTRIAN = ""
                        .ANGKAANTRIAN = 0
                        .MEMO = sUserID
                    End With

                    Dim KodeNomor As String = String.Empty

                    If sFind2.Contains("FARRJ") Then
                        KodeNomor = "FARRJ-K"
                    ElseIf sFind2.Contains("RARRJ") Then
                        KodeNomor = "RARRJ-K"
                    ElseIf sFind2.Contains("FARRI") Then
                        KodeNomor = "FARRI-K"
                    Else
                        KodeNomor = "RARRI-K"
                    End If

                    oOrder.InsertDataFarmasi(KodeNomor, ds)

                End If

                Dim oAdmisi As New Admission.clsPendaftaran_Kunjungan
                Dim dsAdmisi = oAdmisi.GetData(kodekunjungan)

                If dsAdmisi IsNot Nothing Then
                    If dsAdmisi.S_PENDAFTARAN_H.KODEBOOKING <> "" Then
                        Dim dsBilling = oBilling.GetDataByKDkunjunganFarmasi(kodekunjungan)
                        If dsBilling Is Nothing Then
                            frmErmList.fn_TaskID(dsAdmisi.S_PENDAFTARAN_H.KODEBOOKING, 6)
                        Else
                            frmErmList.fn_TaskIDyangLalu(dsAdmisi.S_PENDAFTARAN_H.KODEBOOKING, 6, dsBilling.DATECREATED)
                        End If
                    End If
                End If

                frmSalesOrderTransaksi.LoadMe(FORM_MODE.FORM_MODE_ADD, 4)
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
        End If

    End Sub
    Private Sub AmbilAntrianToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AmbilAntrianToolStripMenuItem.Click
        Try
            If grv.GetFocusedRowCellValue("KDORDER") Is Nothing Then
                MsgBox("Kode Order Kosong!", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim Nomor As String = grv.GetFocusedRowCellValue("KDORDER")
            Dim dsOrder = oOrder.GetDataAntrianFarmasi(Nomor)

            If dsOrder Is Nothing Then
                ' ***** HEADER *****
                Dim ds = oOrder.GetStructureHeaderFarmasi

                With ds
                    .DATECREATED = Now
                    .DATEUPDATED = Now
                    .KDORDER = Nomor
                    .NOMORANTRIAN = ""
                    .ANGKAANTRIAN = 0
                    .MEMO = sUserID
                End With

                Dim KodeNomor As String = String.Empty

                If Nomor.Contains("FARRJ") Then
                    KodeNomor = "FARRJ-K"
                ElseIf Nomor.Contains("RARRJ") Then
                    KodeNomor = "RARRJ-K"
                ElseIf Nomor.Contains("FARRI") Then
                    KodeNomor = "FARRI-K"
                Else
                    KodeNomor = "RARRI-K"
                End If

                oOrder.InsertDataFarmasi(KodeNomor, ds)

                Try
                    If sCetakEtiketFarmasi = 1 Then
                        Dim dsOrderCetak = oOrder.GetDataAntrianFarmasi(Nomor)

                        Dim rpt As New xtraAntrianFarmasi1
                        rpt.bindingSource.DataSource = dsOrderCetak
                        Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                        printTool.PrintDialog()
                    Else
                        Dim dsOrderCetak = oOrder.GetDataAntrianFarmasi(Nomor)

                        Dim rpt As New xtraAntrianFarmasi
                        rpt.bindingSource.DataSource = dsOrderCetak
                        Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                        printTool.PrintDialog()
                    End If

                Catch oErr As Exception
                    MsgBox("Print Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try

            Else
                MsgBox("Pasien Sudah Ambil Antrian dengan Nomor Antrian " & dsOrder.NOMORANTRIAN, MsgBoxStyle.Exclamation, Me.Text)

                If sCetakEtiketFarmasi = 1 Then
                    Dim dsOrderCetak = oOrder.GetDataAntrianFarmasi(Nomor)

                    Dim rpt As New xtraAntrianFarmasi1
                    rpt.bindingSource.DataSource = dsOrderCetak
                    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                    printTool.PrintDialog()
                Else
                    Dim dsOrderCetak = oOrder.GetDataAntrianFarmasi(Nomor)

                    Dim rpt As New xtraAntrianFarmasi
                    rpt.bindingSource.DataSource = dsOrderCetak
                    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                    printTool.PrintDialog()
                End If
            End If

            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox("Simpan Nomor Farmasi" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub KirimApotekOnlineToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KirimApotekOnlineToolStripMenuItem.Click
        If grv.GetFocusedRowCellValue("KDSOTRANSAKSI") Is Nothing Then
            MsgBox("Kode Transaksi Kosong!", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("KDSOTRANSAKSI") = "" Then
            MsgBox("Kode Transaksi Kosong!", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim KDSOTRANSAKSI As String = grv.GetFocusedRowCellValue("KDSOTRANSAKSI")

        Dim oSalesOrderTransaksi As New Sales.clsSalesKatalog

        Dim dsCekTelaahResep = oSalesOrderTransaksi.GetData(grv.GetFocusedRowCellValue("KDSOTRANSAKSI"))
        If dsCekTelaahResep Is Nothing Then
            Dim frmSalesKatalog As New frmSalesKatalog
            Try
                frmSalesKatalog.LoadMe(FORM_MODE.FORM_MODE_ADD, grv.GetFocusedRowCellValue("KDSOTRANSAKSI"))
                frmSalesKatalog.ShowDialog(Me)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSalesKatalog Is Nothing Then frmSalesKatalog.Dispose()
                frmSalesKatalog = Nothing
            End Try
        Else
            Dim frmSalesKatalog As New frmSalesKatalog
            Try
                frmSalesKatalog.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsCekTelaahResep.KDSOTRANSAKSI)
                frmSalesKatalog.ShowDialog(Me)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSalesKatalog Is Nothing Then frmSalesKatalog.Dispose()
                frmSalesKatalog = Nothing
            End Try
        End If

        fn_LoadSecurity()
    End Sub
    Private Sub ProgramRujukBalikPRBToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProgramRujukBalikPRBToolStripMenuItem.Click
        If grv.GetFocusedRowCellValue("REKAMMEDIS") Is Nothing Then
            MsgBox("Nomor Rekam Medis Kosong!", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim frmPendaftaran_PRB As New frmPendaftaran_PRB
        Try
            frmPendaftaran_PRB.fn_loadRekamMedis(grv.GetFocusedRowCellValue("REKAMMEDIS"))
            frmPendaftaran_PRB.LoadMe(FORM_MODE.FORM_MODE_ADD)
            frmPendaftaran_PRB.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmPendaftaran_PRB Is Nothing Then frmPendaftaran_PRB.Dispose()
            frmPendaftaran_PRB = Nothing

        End Try
    End Sub

    Private Sub picRefresh_Click(sender As Object, e As EventArgs) Handles picRefresh.Click

    End Sub


#End Region
End Class