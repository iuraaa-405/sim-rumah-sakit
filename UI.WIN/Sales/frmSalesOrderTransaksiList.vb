Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient
Imports DevExpress.XtraSplashScreen

Public Class frmSalesOrderTransaksiList
    Implements ILanguage

    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oSalesOrderTransaksi As New Sales.clsSalesOrderTransaksi
    Private sCategoryBilling As Integer = 0
    Private sUPDATETRANSAKSI As Boolean = False
#Region "Function"
    Public Sub LoadMe(ByVal CategoryBilling As Integer)
        sCategoryBilling = CategoryBilling
    End Sub
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = SalesOrderTransaksi.TITLE & " - " & IIf(sCategoryBilling = 0, "Rawat Jalan", IIf(sCategoryBilling = 1, "Rawat Inap", IIf(sCategoryBilling = 2, "Laboratorium", IIf(sCategoryBilling = 3, "Radiologi", IIf(sCategoryBilling = 4, "Farmasi", "BDRS")))))
        deDATEFrom.DateTime = Now
        deDATETo.DateTime = Now

        sFind1 = ""

        fn_LoadSecurity()

        If sCategoryBilling = 4 Then
            picCetakEtiket.Visible = True
            picCetakResep.Visible = True
            picTelaah.Visible = True
            lCetakEtiket.Visible = True
            lblCetakResep.Visible = True
            lblTelaah.Visible = True
        End If
    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed

    End Sub
    Private Sub fn_LoadSecurity()
        Try
            Dim oOtority As New Setting.clsOtority
            Dim oUser As New Setting.clsUser

            Dim ds = (From x In oOtority.GetDataDetail
                      Join y In oUser.GetData
                      On x.KDOTORITY Equals y.KDOTORITY
                      Where IIf(sCategoryBilling = 0, x.MODUL = "SOTRANSAKSI_RJ", IIf(sCategoryBilling = 1, x.MODUL = "SOTRANSAKSI_RI", IIf(sCategoryBilling = 2, x.MODUL = "SOTRANSAKSI_LAB", IIf(sCategoryBilling = 3, x.MODUL = "SOTRANSAKSI_RAD", IIf(sCategoryBilling = 4, x.MODUL = "SOTRANSAKSI_FARMASI", x.MODUL = "SOTRANSAKSI_BDRS"))))) _
                      And y.KDUSER = sUserID
                      Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

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

            Try
                picAdd.Enabled = ds.ISADD
                picDiagnosa.Enabled = ds.ISUPDATE
                picDelete.Enabled = ds.ISDELETE
                picUpdate.Enabled = ds.ISUPDATE
                picPrint.Enabled = ds.ISPRINT
                picRefresh.Enabled = ds.ISVIEW

                If ds.ISVIEW = True Then
                    fn_LoadData()
                    fn_LoadLanguage()
                End If
            Catch oErr As Exception
                MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)

                picAdd.Enabled = False
                picDiagnosa.Enabled = False
                picDelete.Enabled = False
                picUpdate.Enabled = False
                picPrint.Enabled = False
                picRefresh.Enabled = False
            End Try
        Catch oErr As Exception
            MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            Me.Text = SalesOrderTransaksi.TITLE & " - " & IIf(sCategoryBilling = 0, "Rawat Jalan", IIf(sCategoryBilling = 1, "Rawat Inap", IIf(sCategoryBilling = 2, "Laboratorium", IIf(sCategoryBilling = 3, "Radiologi", IIf(sCategoryBilling = 4, "Farmasi", "BDRS")))))

            fn_LoadLanguageMaster()
            'fn_LoadLanguageDetail()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Sub fn_LoadLanguageMaster()
        Try
            grv.Columns("KDSOTRANSAKSI").Caption = SalesOrderTransaksi.KDSOTRANSAKSI
            grv.Columns("TANGGAL").Caption = SalesOrderTransaksi.TANGGAL
            grv.Columns("TANGGALDATANG").Caption = Pendaftaran.TANGGAL
            grv.Columns("KDPENDAFTARAN").Caption = Pendaftaran.KDPENDAFTARAN
            grv.Columns("KDCUSTOMER").Caption = "No Medrek"

            grv.Columns("NAMA").Caption = SalesOrderTransaksi.KDPENDAFTARAN
            grv.Columns("SUBTOTAL").Caption = SalesOrderTransaksi.SUBTOTAL
            grv.Columns("DISCOUNT").Caption = SalesOrderTransaksi.DISCOUNT
            grv.Columns("GRANDTOTAL").Caption = SalesOrderTransaksi.GRANDTOTAL
            grv.Columns("MEMO").Caption = SalesOrderTransaksi.MEMO
            grv.Columns("KDUSER").Caption = Caption.User
            grv.Columns("M_DAFTAR_L1").Caption = sDaftar_L1
            grv.Columns("M_DAFTAR_L4").Caption = sDaftar_L4
        Catch oErr As Exception

        End Try
    End Sub
    Public Sub fn_LoadLanguageDetail()
        Try
            If sCategoryBilling = 4 Then
                grv1.Columns("M_ITEM.NMITEM1").Caption = Item.NMITEM1_2
                grv1.Columns("M_ITEM.NMITEM2").Caption = Item.NMITEM2_2
                grv1.Columns("M_ITEM.NMITEM3").Caption = Item.NMITEM3_2
                grv1.Columns("M_UOM.MEMO").Caption = "Satuan"
            Else
                grv1.Columns("M_ITEM.NMITEM1").Caption = Item.NMITEM1_1
                grv1.Columns("M_ITEM.NMITEM2").Caption = Item.NMITEM2_1
                grv1.Columns("M_ITEM.NMITEM3").Caption = Item.NMITEM3_1
                grv1.Columns("M_UOM.MEMO").Caption = "Kelas"
            End If

            grv1.Columns("QTY").Caption = SalesOrderTransaksi.DETAIL_QTY
            grv1.Columns("PRICE").Caption = SalesOrderTransaksi.DETAIL_PRICE
            grv1.Columns("SUBTOTAL").Caption = SalesOrderTransaksi.DETAIL_SUBTOTAL
            grv1.Columns("DISCOUNT").Caption = SalesOrderTransaksi.DETAIL_DISCOUNT
            grv1.Columns("GRANDTOTAL").Caption = SalesOrderTransaksi.DETAIL_GRANDTOTAL

            grv1.Columns("REMARKS").Caption = SalesOrderTransaksi.DETAIL_REMARKS

            grv1.Columns("M_DOCTOR.NAME_DISPLAY").Caption = Pendaftaran.KDDOCTOR
            grv1.Columns("M_DEPARTMENT.NAME_DISPLAY").Caption = Pendaftaran.KDDEPARTMENT
        Catch oErr As Exception

        End Try
    End Sub
    Private Sub fn_LoadData()
        'Try
        '    Dim ds = From x In oSalesOrderTransaksi.GetDataByCategoryBilling(sCategoryBilling, deDATEFrom.DateTime, deDATETo.DateTime)
        '             Where x.GRANDTOTAL <> 0
        '             Select x.KDSOTRANSAKSI, x.KDKUNJUNGAN, M_DAFTAR_L1 = x.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_DAFTAR_L1.MEMO, M_DAFTAR_L4 = x.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_DAFTAR_L4.MEMO, x.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.KDPENDAFTARAN, TANGGALDATANG = x.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.DATE, Tujuan = x.S_PENDAFTARAN_KUNJUNGAN.M_DEPARTMENT.NAME_DISPLAY, TANGGAL = x.DATE, KDCUSTOMER = x.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.KDCUSTOMER, NAMA = x.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY, x.SUBTOTAL, x.DISCOUNT, x.GRANDTOTAL, x.MEMO, Details = x.S_SO_TRANSAKSI_Ds, Bayar = IIf(x.GRANDTOTAL = 0, CBool(False), IIf(x.PAYAMOUNT = x.GRANDTOTAL, CBool(True), CBool(False))), x.KDUSER

        '    grd.DataSource = ds.ToList

        '    fn_LoadFormatData()
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try

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
            SQL &= ",Bayar = CASE A.GRANDTOTAL WHEN 0 THEN CONVERT(BIT, 0) ELSE CASE WHEN A.PAYAMOUNT = A.GRANDTOTAL THEN CONVERT(BIT, 1) ELSE CONVERT(BIT, 0) END END "
            SQL &= ",KDKUNJUNGAN = A.KDKUNJUNGAN "
            SQL &= ",A.KDUSER "
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
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND A.CATEGORY = " & sCategoryBilling & " "
            SQL &= "ORDER BY A.DATE, A.KDSOTRANSAKSI DESC"

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_SO_TRANSAKSI_H")

            'SQL = "SELECT "
            'SQL &= "B.KDSOTRANSAKSI "
            ''SQL &= ",C.NMITEM1 "
            'SQL &= ",C.NMITEM2 "
            ''SQL &= ",C.NMITEM3 "
            'SQL &= ",B.QTY "
            'SQL &= ",KDUOM = D.MEMO "
            'SQL &= ",B.PRICE "
            'SQL &= ",B.SUBTOTAL "
            'SQL &= ",B.DISCOUNT "
            'SQL &= ",B.GRANDTOTAL "
            'SQL &= ",B.REMARKS "
            'SQL &= "FROM S_SO_TRANSAKSI_H A "
            'SQL &= "INNER JOIN S_SO_TRANSAKSI_D B "
            'SQL &= "ON A.KDSOTRANSAKSI = B.KDSOTRANSAKSI "
            'SQL &= "INNER JOIN M_ITEM C "
            'SQL &= "ON B.KDITEM = C.KDITEM "
            'SQL &= "INNER JOIN M_UOM D "
            'SQL &= "ON B.KDUOM = D.KDUOM "
            'SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            'SQL &= "AND A.CATEGORY = " & sCategoryBilling & " "

            'oComm.Connection = oConn
            'oComm.CommandText = SQL
            'oComm.CommandTimeout = 120
            'oComm.CommandType = CommandType.Text

            'da = New SqlDataAdapter(oComm)
            'da.Fill(ds, "S_SO_TRANSAKSI_D")

            'Dim keyColumn As DataColumn = ds.Tables("S_SO_TRANSAKSI_H").Columns("KDSOTRANSAKSI")
            'Dim foreignKeyColumn As DataColumn = ds.Tables("S_SO_TRANSAKSI_D").Columns("KDSOTRANSAKSI")
            'ds.Relations.Add("FK_RELATION", keyColumn, foreignKeyColumn)

            grd.MainView = grv
            grd.DataSource = ds.Tables("S_SO_TRANSAKSI_H")
            grd.ForceInitialize()

            'grd.LevelTree.Nodes.Add("FK_RELATION", grv1)
            'grv1.ViewCaption = "Details"

            'grv1.PopulateColumns(ds.Tables("S_SO_TRANSAKSI_D"))
            'grv1.Columns("KDSOTRANSAKSI").VisibleIndex = -1

            fn_LoadFormatData()
            fn_LoadFormatDataDetail()

            'If cboTYPE.SelectedIndex = 0 Then
            '    Dim keyColumn As DataColumn = ds.Tables("S_SO_TRANSAKSI_H").Columns("KDSOTRANSAKSI")
            '    Dim foreignKeyColumn As DataColumn = ds.Tables("S_SO_TRANSAKSI_D").Columns("KDSOTRANSAKSI")
            '    ds.Relations.Add("FK_RELATION", keyColumn, foreignKeyColumn)

            '    grd.MainView = grv
            '    grd.DataSource = ds.Tables("S_SO_TRANSAKSI_H")
            '    grd.ForceInitialize()

            '    grd.LevelTree.Nodes.Add("FK_RELATION", grv1)
            '    grv1.ViewCaption = "Details"

            '    grv1.PopulateColumns(ds.Tables("S_SO_TRANSAKSI_D"))
            '    grv1.Columns("KDSOTRANSAKSI").VisibleIndex = -1

            '    fn_LoadFormatData()
            '    fn_LoadFormatDataDetail()
            'ElseIf cboTYPE.SelectedIndex = 1 Then
            '    grd.MainView = grv
            '    grd.DataSource = ds.Tables("S_SO_TRANSAKSI_H")
            '    grd.ForceInitialize()

            '    fn_LoadFormatData()
            'ElseIf cboTYPE.SelectedIndex = 2 Then
            '    grd.MainView = grv1
            '    grd.DataSource = ds.Tables("S_SO_TRANSAKSI_D")
            '    grd.ForceInitialize()

            '    fn_LoadFormatDataDetail()
            'End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try

        'Try
        '    'If sSplashScreen = False Then Exit Sub

        '    grv.Columns.Clear()
        '    grd.DataSource = Nothing
        '    grv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        '    grv1.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

        '    SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
        '    SplashScreenManager.Default.SetWaitFormCaption("Processing data.....")

        '    Dim oConn As New SqlConnection
        '    Dim oComm As New SqlCommand
        '    Dim da As SqlDataAdapter
        '    Dim ds As New DataSet
        '    Dim SQL As String

        '    Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\RME\", "Database", "").ToString())

        '    oConn = New SqlConnection(sConn)
        '    If oConn.State = ConnectionState.Closed Then
        '        oConn.Open()
        '    End If

        '    SQL = "SELECT "
        '    SQL &

        '    oComm.Connection = oConn
        '    oComm.CommandText = SQL
        '    oComm.CommandTimeout = 120
        '    oComm.CommandType = CommandType.Text

        '    da = New SqlDataAdapter(oComm)
        '    da.Fill(ds, "R_IDENTITAS_GROUPER")

        '    If oConn.State = ConnectionState.Open Then
        '        oConn.Close()
        '    End If

        '    grd.MainView = grv
        '    grd.DataSource = ds.Tables("R_IDENTITAS_GROUPER")
        '    grd.ForceInitialize()

        '    SplashScreenManager.CloseForm(False)


        'Catch oErr As Exception
        '    SplashScreenManager.CloseForm(False)
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub
    Private Sub grv_MasterRowExpanded(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventArgs) Handles grv.MasterRowExpanded
        grv1 = TryCast(grv.GetDetailView(e.RowHandle, e.RelationIndex), DevExpress.XtraGrid.Views.Grid.GridView)

        fn_LoadFormatDataDetail()
        fn_LoadLanguageDetail()
    End Sub
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

        grv.Columns("Bayar").VisibleIndex = -1
        grv.Columns("KDKUNJUNGAN").VisibleIndex = -1
    End Sub
    Private Sub fn_LoadFormatDataDetail()
        For iLoop As Integer = 0 To grv1.Columns.Count - 1
            If grv1.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grv1.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv1.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv1.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grv1.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv1.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv1.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

        'grv1.Columns().AddVisible("M_ITEM.NMITEM1")
        'grv1.Columns().AddVisible("M_ITEM.NMITEM2")
        'grv1.Columns().AddVisible("M_ITEM.NMITEM3")
        'grv1.Columns().AddVisible("M_UOM.MEMO")
        'grv1.Columns().AddVisible("M_DOCTOR.NAME_DISPLAY")
        'grv1.Columns().AddVisible("M_DEPARTMENT.NAME_DISPLAY")

        'grv1.Columns("DATECREATED").Visible = False
        'grv1.Columns("DATEUPDATED").Visible = False
        'grv1.Columns("SEQ").Visible = False
        'grv1.Columns("KDSOTRANSAKSI").Visible = False
        'grv1.Columns("M_ITEM").Visible = False
        'grv1.Columns("M_DOCTOR").Visible = False
        'grv1.Columns("M_DEPARTMENT").Visible = False
        'grv1.Columns("M_UOM").Visible = False
        'grv1.Columns("S_SO_TRANSAKSI_H").Visible = False
        'grv1.Columns("KDITEM").Visible = False
        'grv1.Columns("KDUOM").Visible = False
        'grv1.Columns("KDDOCTOR").Visible = False
        'grv1.Columns("KDDEPARTMENT").Visible = False
        'grv1.Columns("ISRACIK").Visible = False
        'grv1.Columns("KDSIGNA").Visible = False
        'grv1.Columns("M_SIGNA").Visible = False

        'grv1.Columns("DATECREATED").OptionsColumn.ShowInCustomizationForm = False
        'grv1.Columns("DATEUPDATED").OptionsColumn.ShowInCustomizationForm = False
        'grv1.Columns("SEQ").OptionsColumn.ShowInCustomizationForm = False
        'grv1.Columns("KDSOTRANSAKSI").OptionsColumn.ShowInCustomizationForm = False
        'grv1.Columns("M_ITEM").OptionsColumn.ShowInCustomizationForm = False
        'grv1.Columns("M_UOM").OptionsColumn.ShowInCustomizationForm = False
        'grv1.Columns("M_DOCTOR").OptionsColumn.ShowInCustomizationForm = False
        'grv1.Columns("M_DEPARTMENT").OptionsColumn.ShowInCustomizationForm = False
        'grv1.Columns("S_SO_TRANSAKSI_H").OptionsColumn.ShowInCustomizationForm = False
        'grv1.Columns("KDITEM").OptionsColumn.ShowInCustomizationForm = False
        'grv1.Columns("KDUOM").OptionsColumn.ShowInCustomizationForm = False
        'grv1.Columns("ISRACIK").OptionsColumn.ShowInCustomizationForm = False
        'grv1.Columns("KDSIGNA").OptionsColumn.ShowInCustomizationForm = False
        'grv1.Columns("M_SIGNA").OptionsColumn.ShowInCustomizationForm = False

        'grv1.Columns("M_ITEM.NMITEM1").VisibleIndex = -1
        'grv1.Columns("M_ITEM.NMITEM3").VisibleIndex = -1

        'grv1.Columns("M_ITEM.NMITEM2").VisibleIndex = 0

    End Sub
    Private Sub MasterColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MasterColumnChooserToolStripMenuItem.Click
        grv.ShowCustomization()
    End Sub
    Private Sub DetailColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DetailColumnChooserToolStripMenuItem.Click
        grv1.ShowCustomization()
    End Sub
    Private Function fn_DeleteData(ByVal sKDSOTRANSAKSI As String) As Boolean
        Try

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
    Private Sub grv_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grv.RowStyle
        If grv.IsFilterRow(e.RowHandle) Then Exit Sub
        If CBool(grv.GetRowCellValue(e.RowHandle, "Bayar")) = False Then
            'e.Appearance.BackColor = Color.Red
        Else
            e.Appearance.BackColor = Color.LightGreen
        End If
    End Sub
    Private Sub MenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picPrint.Click
        If grv.GetFocusedRowCellValue("KDSOTRANSAKSI") Is Nothing Then Exit Sub
        mnuSTRIPCETAK.Show(picPrint.Location.X, picPrint.Location.Y + 125)
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmMember_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.A
                If e.Alt = True And picAdd.Enabled = True Then
                    picAdd_Click()
                End If
            Case Keys.E
                If e.Alt = True And picUpdate.Enabled = True Then
                    picUpdate_Click()
                End If
            Case Keys.D
                If e.Alt = True And picDelete.Enabled = True Then
                    picDelete_Click()
                End If
            Case Keys.R
                If e.Alt = True And picRefresh.Enabled = True Then
                    picRefresh_Click()
                End If
                'Case Keys.F5
                '    If e.Alt = True And picDiagnosa.Enabled = True Then
                '        picDiagnosa_Click()
                '    End If
        End Select
    End Sub
    Private Sub grv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grv.DoubleClick
        If grv.GetFocusedRowCellValue("KDSOTRANSAKSI") Is Nothing Then
            Exit Sub
        End If

        Dim frmSalesOrderTransaksi As New frmSalesOrderTransaksi
        Try
            frmSalesOrderTransaksi.LoadMe(FORM_MODE.FORM_MODE_VIEW, sCategoryBilling, grv.GetFocusedRowCellValue("KDSOTRANSAKSI"))
            frmSalesOrderTransaksi.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picDiagnosa_Click() Handles picDiagnosa.Click
        If grv.GetFocusedRowCellValue("KDSOTRANSAKSI") Is Nothing Then
            Exit Sub
        End If

        Dim dsKunjungan = oSalesOrderTransaksi.GetDataKunjunganByKD(grv.GetFocusedRowCellValue("KDKUNJUNGAN"))

        If dsKunjungan IsNot Nothing Then
            Dim dsMasterDiagnosa = oSalesOrderTransaksi.GetDataMasterDiagnosaByKD(dsKunjungan.KDPENDAFTARAN)
            If dsMasterDiagnosa IsNot Nothing Then
                frmDiagnosaMaster.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsMasterDiagnosa.KDPENDAFTARAN, dsMasterDiagnosa.KDDIAGNOSAMASTER)
                frmDiagnosaMaster.ShowDialog(Me)
            Else
                frmDiagnosaMaster.LoadMe(FORM_MODE.FORM_MODE_ADD, dsKunjungan.KDPENDAFTARAN)
                frmDiagnosaMaster.ShowDialog(Me)
            End If

            fn_LoadSecurity()
        Else
            MsgBox("Pendafatran Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub picAdd_Click() Handles picAdd.Click
        Dim frmSalesOrderTransaksi As New frmSalesOrderTransaksi
        Try
            frmSalesOrderTransaksi.LoadMe(FORM_MODE.FORM_MODE_ADD, sCategoryBilling)
            frmSalesOrderTransaksi.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmSalesOrderTransaksi Is Nothing Then frmSalesOrderTransaksi.Dispose()
            frmSalesOrderTransaksi = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDSOTRANSAKSI"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KDSOTRANSAKSI") Is Nothing Then
            Exit Sub
        End If

        If CBool(grv.GetFocusedRowCellValue("Bayar")) = True Then
            MsgBox("Sudah Bayar silahkan hapus dulu invoice ke bagian pembayaran/kasir", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        Else
            If sUPDATETRANSAKSI = False Then
                Dim ds1 = oSalesOrderTransaksi.GetData(grv.GetFocusedRowCellValue("KDSOTRANSAKSI"))
                If ds1 IsNot Nothing Then
                    If ds1.KDUSER <> sUserID Then
                        MsgBox("Tidak dapat Rubah Silahkan hubungi User " & ds1.KDUSER, MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                    End If
                End If

            End If

            Dim frmSalesOrderTransaksi As New frmSalesOrderTransaksi
            Try
                frmSalesOrderTransaksi.LoadMe(FORM_MODE.FORM_MODE_EDIT, sCategoryBilling, grv.GetFocusedRowCellValue("KDSOTRANSAKSI"))
                frmSalesOrderTransaksi.ShowDialog(Me)
                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSalesOrderTransaksi Is Nothing Then frmSalesOrderTransaksi.Dispose()
                frmSalesOrderTransaksi = Nothing

                Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDSOTRANSAKSI"), sCode)
                If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

                If sStatusSave = "NEW" Then
                    sStatusSave = "NONE"
                    picAdd_Click()
                End If
            End Try
        End If
    End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KDSOTRANSAKSI") Is Nothing Then
            Exit Sub
        End If
        If CBool(grv.GetFocusedRowCellValue("Bayar")) = True Then
            MsgBox("Sudah Bayar silahkan hapus dulu invoice ke bagian pembayaran/kasir", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If sUPDATETRANSAKSI = False Then
            Dim ds1 = oSalesOrderTransaksi.GetData(grv.GetFocusedRowCellValue("KDSOTRANSAKSI"))
            If ds1 IsNot Nothing Then
                If ds1.KDUSER <> sUserID Then
                    MsgBox("Tidak dapat Rubah Silahkan hubungi User " & ds1.KDUSER, MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                End If
            End If
        End If

        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        Dim sKDORDER As String = String.Empty

        Dim ds = oSalesOrderTransaksi.GetData(grv.GetFocusedRowCellValue("KDSOTRANSAKSI"))
        If ds IsNot Nothing Then
            sKDORDER = ds.KDORDER
        End If

        If sKDORDER <> "" Then
            Dim oOrder As New Digital.clsR_Order
            Dim dsOrder = oOrder.GetData(sKDORDER)
            If dsOrder IsNot Nothing Then
                If dsOrder.STATUS = "DITERIMA" Then
                    Dim oDigitalOrder As New Digital.clsR_Order
                    oDigitalOrder.UpdateStatus(sKDORDER, "UPDATE")

                    If fn_DeleteData(grv.GetFocusedRowCellValue("KDSOTRANSAKSI")) = False Then
                        MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                    End If
                ElseIf dsOrder.STATUS = "TAMBAH" Then
                    If fn_DeleteData(grv.GetFocusedRowCellValue("KDSOTRANSAKSI")) = False Then
                        MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                    End If
                ElseIf dsOrder.STATUS = "UPDATE" Then
                    If fn_DeleteData(grv.GetFocusedRowCellValue("KDSOTRANSAKSI")) = False Then
                        MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                    End If
                ElseIf dsOrder.STATUS = "PANGGIL" Then
                    If fn_DeleteData(grv.GetFocusedRowCellValue("KDSOTRANSAKSI")) = False Then
                        MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                    End If
                ElseIf dsOrder.STATUS = "HASIL" Then
                    MsgBox("Hasil Sudah di Input Tidak Dapat di Hapus/Rubah", MsgBoxStyle.Exclamation, Me.Text)
                ElseIf dsOrder.STATUS = "KIRIM" Then
                    MsgBox("Hasil Sudah di Kirim Tidak Dapat di Hapus/Rubah", MsgBoxStyle.Exclamation, Me.Text)
                Else
                    MsgBox("Hasil Sudah di " & dsOrder.STATUS & " Tidak Dapat di Hapus/Rubah", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                If fn_DeleteData(grv.GetFocusedRowCellValue("KDSOTRANSAKSI")) = False Then
                    MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                End If
            End If
            'Dim oDigitalOrder As New Digital.clsR_Order
            'oDigitalOrder.UpdateStatus(sFind2, "UPDATE")

            'Try
            '    If sCategoryBilling = 2 Then
            '        frmSalesOrderTransaksi.fn_UpdateOrderByKDCPPTTransaksi(sKDCPPT, "0", "LABORATORIUM")
            '    End If
            '    If sCategoryBilling = 3 Then
            '        frmSalesOrderTransaksi.fn_UpdateOrderByKDCPPTTransaksi(sKDCPPT, "0", "RADIOLOGI")
            '    End If
            '    If sCategoryBilling = 4 Then
            '        frmSalesOrderTransaksi.fn_UpdateOrderByKDCPPTNonRacikan(sKDCPPT, "0")
            '        frmSalesOrderTransaksi.fn_UpdateOrderByKDCPPTRacikan(sKDCPPT, "0")
            '    End If
            'Catch oErr As Exception
            '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            'End Try
        Else
            If fn_DeleteData(grv.GetFocusedRowCellValue("KDSOTRANSAKSI")) = False Then
                MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
        End If

        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        fn_LoadSecurity()

    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_LoadSecurity()
    End Sub
    Private Sub picCetakEtiket_Click() Handles picCetakEtiket.Click
        Try
            If grv.GetFocusedRowCellValue("KDSOTRANSAKSI") = String.Empty Then Exit Sub

            If sCategoryBilling = 4 Then
                Dim ds = oSalesOrderTransaksi.GetData(grv.GetFocusedRowCellValue("KDSOTRANSAKSI"))
                If ds IsNot Nothing Then
                    Dim oOpname As New Inventory.clsOpname
                    Dim oPI As New Purchasing.clsPurchaseInvoice
                    Dim oRME As New RME.clsRME
                    Dim RI As Integer = 0

                    Dim arrDetailEtiket = oSalesOrderTransaksi.GetStructureDetailEtiketList

                    For Each xloop In oSalesOrderTransaksi.GetDataDetail(grv.GetFocusedRowCellValue("KDSOTRANSAKSI"))
                        If xloop.M_ITEM.M_ITEM_L3.MEMO <> "BHP" Then
                            Dim dsEtiket = oSalesOrderTransaksi.GetStructureHeaderEtiket
                            With dsEtiket
                                RI = xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.CATEGORY

                                If sCetakEtiketFarmasi = 1 Then
                                    .DESCRIPTION = xloop.KDSOTRANSAKSI & "   " & xloop.S_SO_TRANSAKSI_H.DATE.ToString("dd-MM-yyyy")
                                    .DOKTER = xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.M_DEPARTMENT.NAME_DISPLAY & " / " & xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_DAFTAR_L1.MEMO
                                    .EXPIREOBAT = xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY & " / " & xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.KDCUSTOMER
                                    .NAMA = xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy") & " / " & oRME.GetUmurPasien(xloop.S_SO_TRANSAKSI_H.DATE, xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)
                                    .NORESEP = xloop.M_CARAPAKAI.MEMO
                                    .NAMAOBAT = (xloop.M_ITEM.NMITEM2 & " / " & "Jml:" & FormatNumber(xloop.QTY, 0)).ToString.Trim
                                    .RUANGAN = (IIf(xloop.M_SIGNA.MEMO = "-", "", xloop.M_SIGNA.MEMO) & " " & xloop.REMARKS).ToString.Trim

                                    Dim rpt As New xtraEtiket1
                                    rpt.ShowPrintMarginsWarning = False
                                    rpt.Watermark.Text = sWATERMARK
                                    rpt.BindingSource.DataSource = dsEtiket
                                    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                                    printTool.Print()
                                ElseIf sCetakEtiketFarmasi = 2
                                    .DESCRIPTION = xloop.KDSOTRANSAKSI & "   " & xloop.S_SO_TRANSAKSI_H.DATE.ToString("dd-MM-yyyy")
                                    .DOKTER = xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.M_DEPARTMENT.NAME_DISPLAY & " / " & xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_DAFTAR_L1.MEMO
                                    .EXPIREOBAT = xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY & " / " & xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.KDCUSTOMER
                                    .NAMA = xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy") & " / " & oRME.GetUmurPasien(xloop.S_SO_TRANSAKSI_H.DATE, xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)
                                    .NORESEP = xloop.M_CARAPAKAI.MEMO
                                    .NAMAOBAT = (xloop.M_ITEM.NMITEM2 & " / " & "Jml:" & FormatNumber(xloop.QTY, 0)).ToString.Trim
                                    .RUANGAN = (IIf(xloop.M_SIGNA.MEMO = "-", "", xloop.M_SIGNA.MEMO) & " " & xloop.REMARKS).ToString.Trim

                                    Dim rpt As New xtraEtiket2
                                    rpt.ShowPrintMarginsWarning = False
                                    rpt.Watermark.Text = sWATERMARK
                                    rpt.BindingSource.DataSource = dsEtiket
                                    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                                    printTool.Print()
                                Else
                                    .DESCRIPTION = xloop.KDSOTRANSAKSI & "   " & xloop.S_SO_TRANSAKSI_H.DATE.ToString("dd-MM-yyyy")
                                    .DOKTER = xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.M_DEPARTMENT.NAME_DISPLAY & " / " & xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_DAFTAR_L1.MEMO
                                    .EXPIREOBAT = xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY & " / " & xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.KDCUSTOMER
                                    .NAMA = xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy") & " / " & oRME.GetUmurPasien(xloop.S_SO_TRANSAKSI_H.DATE, xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)
                                    .NORESEP = xloop.M_ITEM.M_ITEM_L6.MEMO & "          " & "Sebelum / Sesudah Makan"
                                    .NAMAOBAT = (xloop.M_ITEM.NMITEM2 & " / " & "Jml:" & FormatNumber(xloop.QTY, 0)).ToString.Trim
                                    .RUANGAN = (IIf(xloop.M_SIGNA.MEMO = "-", "", xloop.M_SIGNA.MEMO) & " " & IIf(xloop.M_CARAPAKAI.MEMO = "-", "", xloop.M_CARAPAKAI.MEMO) & " " & xloop.REMARKS).ToString.Trim

                                    Dim rpt As New xtraEtiket
                                    rpt.ShowPrintMarginsWarning = False
                                    rpt.Watermark.Text = sWATERMARK
                                    rpt.BindingSource.DataSource = dsEtiket
                                    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                                    printTool.Print()
                                End If

                            End With
                        Else
                            Dim dsDetailEtiket = oSalesOrderTransaksi.GetStructureDetailEtiket
                            With dsDetailEtiket
                                .DESCRIPTION = xloop.KDSOTRANSAKSI & "   " & xloop.S_SO_TRANSAKSI_H.DATE.ToString("dd-MM-yyyy")
                                .DOKTER = xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.M_DEPARTMENT.NAME_DISPLAY & " / " & xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_DAFTAR_L1.MEMO
                                .EXPIREOBAT = xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY & " / " & xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.KDCUSTOMER
                                .NAMA = xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy") & " / " & oRME.GetUmurPasien(xloop.S_SO_TRANSAKSI_H.DATE, xloop.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)
                                .NORESEP = xloop.M_CARAPAKAI.MEMO
                                .NAMAOBAT = (xloop.M_ITEM.NMITEM2 & " / " & "Jml:" & FormatNumber(xloop.QTY, 0)).ToString.Trim
                                .RUANGAN = (IIf(xloop.M_SIGNA.MEMO = "-", "", xloop.M_SIGNA.MEMO) & " " & xloop.REMARKS).ToString.Trim
                            End With
                            arrDetailEtiket.Add(dsDetailEtiket)
                        End If

                    Next

                    If arrDetailEtiket.Count > 0 Then
                        Dim rptList As New xtraEtiketBHPList
                        rptList.ShowPrintMarginsWarning = False
                        rptList.Watermark.Text = sWATERMARK
                        rptList.BindingSource.DataSource = arrDetailEtiket
                        Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rptList)
                        printTool.Print()
                    End If

                    If RI = 1 Then
                        If MsgBox("Apakah Akan Cetak List Obat?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.Yes Then
                            Dim rpt As New xtraEtiketList
                            rpt.ShowPrintMarginsWarning = False
                            rpt.Watermark.Text = sWATERMARK
                            rpt.BindingSource.DataSource = ds
                            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                            printTool.Print()
                        End If
                    End If
                End If
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picCetakResep_Click() Handles picCetakResep.Click
        Try
            If grv.GetFocusedRowCellValue("KDSOTRANSAKSI") = String.Empty Then Exit Sub

            If sCategoryBilling = 4 Then
                Dim oCPPT As New Grouper.clsR_CPPT

                Dim ds = oSalesOrderTransaksi.GetData(grv.GetFocusedRowCellValue("KDSOTRANSAKSI"))
                If ds IsNot Nothing Then
                    Dim dsResep = oSalesOrderTransaksi.GetStructureHeaderResep
                    Dim DiganosaPasien As String = String.Empty
                    Dim BeratBadan As String = "-"
                    Dim Alergi As String = String.Empty

                    Dim dsCPPTByKunjungan = oCPPT.GetDataByKdKunjungan(ds.KDKUNJUNGAN)
                    If dsCPPTByKunjungan IsNot Nothing Then
                        Dim dsCPPT = oCPPT.GetDataCPPTByIdentitas(dsCPPTByKunjungan.KDIDENTITAS)
                        If dsCPPT IsNot Nothing Then
                            BeratBadan = dsCPPT.OBJEKTIF_BERATBADAN & " Kg"
                            DiganosaPasien = dsCPPT.ASSEMENT_TEXT

                            If dsCPPT.SUBJEKTIF_ALERGI_TIDAK = True Then
                                Alergi = "Alergi Obat: Tidak"
                            End If
                            If dsCPPT.SUBJEKTIF_ALERGI_YA = True Then
                                Alergi = "Alergi Obat: Ya " & dsCPPT.SUBJEKTIF_ALERGI_YA_TEXT
                            End If
                        Else
                            Alergi = "Alergi Obat: -"
                            DiganosaPasien = ds.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_DIAGNOSA.MEMO
                        End If
                    Else
                        Alergi = "Alergi Obat: -"
                    End If

                    With dsResep
                        .NORESEP = ds.KDSOTRANSAKSI
                        .TANGGAL = ds.DATE
                        .KLINIK = ds.S_PENDAFTARAN_KUNJUNGAN.M_DEPARTMENT.NAME_DISPLAY
                        .DOKTER = ds.M_DOCTOR.NAME_DISPLAY
                        .NOMORSIP = ds.M_DOCTOR.SIP
                        .NORM = ds.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.KDCUSTOMER
                        .NAMAPASIEN = ds.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY
                        .TANGGALLAHIR = ds.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR
                        .BERATBADAN = BeratBadan
                        .DIAGNOSA = DiganosaPasien
                        .ALAMAT = ds.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.ALAMAT
                        .ALERGI = Alergi

                        Dim list As New List(Of String)

                        For Each xLoop In oSalesOrderTransaksi.GetDataDetail(ds.KDSOTRANSAKSI)
                            'list.Add("R/ " & xLoop.M_ITEM.NMITEM2 & " No " & IntegerToRoman(xLoop.QTY) & vbCrLf & "   ʃ " & IIf(xLoop.M_SIGNA.CETAKDIETIKET = "", xLoop.M_SIGNA.MEMO, xLoop.M_SIGNA.CETAKDIETIKET) & " " & xLoop.M_CARAPAKAI.MEMO & (" " & IIf(xLoop.REMARKS = "-", "", xLoop.REMARKS)).ToString.Trim)
                            list.Add("R/ " & xLoop.M_ITEM.NMITEM2 & " No " & IntegerToRoman(xLoop.QTY) & vbCrLf & "   ʃ " & IIf(xLoop.M_SIGNA.CETAKDIETIKET = "", "", xLoop.M_SIGNA.CETAKDIETIKET) & IIf(xLoop.M_CARAPAKAI.MEMO = "-", "", " " & xLoop.M_CARAPAKAI.MEMO) & IIf(xLoop.M_SIGNA.MEMO = "-", "", " " & xLoop.M_SIGNA.MEMO) & (" " & xLoop.REMARKS.ToString.Trim).Trim)
                        Next

                        .RESEP = String.Join(vbCrLf & "   --------------------------------------------------------" & vbCrLf, list.ToArray) & vbCrLf & "   --------------------------------------------------------"

                        Dim dsTelaahResep = oSalesOrderTransaksi.GetDataTelaahResep(ds.KDSOTRANSAKSI)
                        If dsTelaahResep IsNot Nothing Then
                            .TELAAH_01_01_01 = dsTelaahResep.TELAAH_01
                            .TELAAH_01_01_02 = dsTelaahResep.TELAAH_02
                            .TELAAH_01_01_03 = dsTelaahResep.TELAAH_03
                            .TELAAH_01_01_04 = dsTelaahResep.TELAAH_04
                            .TELAAH_01_01_05 = dsTelaahResep.TELAAH_05
                            .TELAAH_01_01_06 = dsTelaahResep.TELAAH_06
                            .TELAAH_01_01_07 = dsTelaahResep.TELAAH_07
                            .TELAAH_01_01_08 = dsTelaahResep.TELAAH_08
                            .TELAAH_01_01_09 = dsTelaahResep.TELAAH_09
                            .TELAAH_01_01_10 = dsTelaahResep.TELAAH_10
                            .TELAAH_01_01_11 = dsTelaahResep.TELAAH_11
                            .TELAAH_01_01_12 = dsTelaahResep.TELAAH_12
                            .TELAAH_01_01_13 = dsTelaahResep.TELAAH_13
                            .TELAAH_01_01_14 = dsTelaahResep.TELAAH_14
                            .TELAAH_01_01_15 = dsTelaahResep.TELAAH_15
                            .TELAAH_01_01_16 = dsTelaahResep.TELAAH_16
                            .TELAAH_01_01_17 = dsTelaahResep.TELAAH_17
                            .TELAAH_01_01_18 = dsTelaahResep.TELAAH_18
                            .TELAAH_01_01_19 = dsTelaahResep.TELAAH_19
                            .TELAAH_01_01_20 = dsTelaahResep.TELAAH_20
                            .TELAAH_01_01_21 = dsTelaahResep.TELAAH_21
                            .TELAAH_01_01_22 = dsTelaahResep.TELAAH_22
                            .TELAAH_01_01_23 = dsTelaahResep.TELAAH_23
                            .TELAAH_01_01_24 = dsTelaahResep.TELAAH_24
                            .PETUGASFARMASI = dsTelaahResep.KDUSER
                        Else
                            .TELAAH_01_01_01 = False
                            .TELAAH_01_01_02 = False
                            .TELAAH_01_01_03 = False
                            .TELAAH_01_01_04 = False
                            .TELAAH_01_01_05 = False
                            .TELAAH_01_01_06 = False
                            .TELAAH_01_01_07 = False
                            .TELAAH_01_01_08 = False
                            .TELAAH_01_01_09 = False
                            .TELAAH_01_01_10 = False
                            .TELAAH_01_01_11 = False
                            .TELAAH_01_01_12 = False
                            .TELAAH_01_01_13 = False
                            .TELAAH_01_01_14 = False
                            .TELAAH_01_01_15 = False
                            .TELAAH_01_01_16 = False
                            .TELAAH_01_01_17 = False
                            .TELAAH_01_01_18 = False
                            .TELAAH_01_01_19 = False
                            .TELAAH_01_01_20 = False
                            .TELAAH_01_01_21 = False
                            .TELAAH_01_01_22 = False
                            .TELAAH_01_01_23 = False
                            .TELAAH_01_01_24 = False
                            .PETUGASFARMASI = ds.KDUSER
                        End If

                        Dim dsTelaahObat1 = oSalesOrderTransaksi.GetDataTelaahObat1(ds.KDSOTRANSAKSI)
                        If dsTelaahObat1 IsNot Nothing Then
                            .TELAAH_02_01_01 = dsTelaahObat1.TELAAH_01
                            .TELAAH_02_01_02 = dsTelaahObat1.TELAAH_02
                            .TELAAH_02_01_03 = dsTelaahObat1.TELAAH_03
                            .TELAAH_02_01_04 = dsTelaahObat1.TELAAH_04
                            .TELAAH_02_01_05 = dsTelaahObat1.TELAAH_05
                        Else
                            .TELAAH_02_01_01 = False
                            .TELAAH_02_01_02 = False
                            .TELAAH_02_01_03 = False
                            .TELAAH_02_01_04 = False
                            .TELAAH_02_01_05 = False
                        End If
                        Dim dsTelaahObat2 = oSalesOrderTransaksi.GetDataTelaahObat2(ds.KDSOTRANSAKSI)
                        If dsTelaahObat2 IsNot Nothing Then
                            .TELAAH_03_01_01 = dsTelaahObat2.TELAAH_01
                            .TELAAH_03_01_02 = dsTelaahObat2.TELAAH_02
                            .TELAAH_03_01_03 = dsTelaahObat2.TELAAH_03
                            .TELAAH_03_01_04 = dsTelaahObat2.TELAAH_04
                            .TELAAH_03_01_05 = dsTelaahObat2.TELAAH_05
                        Else
                            .TELAAH_03_01_01 = False
                            .TELAAH_03_01_02 = False
                            .TELAAH_03_01_03 = False
                            .TELAAH_03_01_04 = False
                            .TELAAH_03_01_05 = False
                        End If

                        .PERUBAHANRESEP_01 = ""
                        .PERUBAHANRESEP_02 = ""

                        .KODETTD = ds.M_DOCTOR.KODETTD

                        Dim rpt As New xtraResep
                        rpt.ShowPrintMarginsWarning = False
                        rpt.Watermark.Text = sWATERMARK
                        rpt.bindingSource.DataSource = dsResep

                        Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                        printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
                    End With
                End If
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
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
    Private Sub picTelaah_Click() Handles picTelaah.Click
        Try
            If grv.GetFocusedRowCellValue("KDSOTRANSAKSI") = String.Empty Then Exit Sub

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
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub AddKwitansiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddKwitansiToolStripMenuItem.Click
        'Dim frmCashIn As New frmCashIn
        Try
            If grv.GetFocusedRowCellValue("KDSOTRANSAKSI") = String.Empty Then Exit Sub

            Dim dsSales = oSalesOrderTransaksi.GetData(grv.GetFocusedRowCellValue("KDSOTRANSAKSI"))

            sREKAMMEDIS = dsSales.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.KDCUSTOMER.ToString

            frmCashIn.LoadMe(FORM_MODE.FORM_MODE_ADD)
            frmCashIn.ShowDialog(Me)
            fn_LoadSecurity()

            sREKAMMEDIS = ""

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grd_DoubleClick(sender As Object, e As EventArgs) Handles grd.DoubleClick
        If grv.GetFocusedRowCellValue("KDPENDAFTARAN") = String.Empty Then Exit Sub
        Try
            LoadMetransaksi(grv.GetFocusedRowCellValue("KDPENDAFTARAN"))
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try

    End Sub
    Public Sub LoadMetransaksi(ByVal sKDPENDAFTARAN As String)
        Try
            Dim oPendaftaran As New Admission.clsPendaftaran
            Dim dsPendaftaran = oPendaftaran.GetData(sKDPENDAFTARAN)
            Dim TanggalPulang As DateTime = Now
            Dim TanggalDatang As DateTime = Now
            Dim sKDPEDAFTARAN_H As String = ""

            If dsPendaftaran Is Nothing Then
                Exit Sub
            Else
                Dim oPulang As New Admission.clsUpdate_Tanggal_Pulang
                Dim dsPulang = oPulang.GetDatabyKD(sKDPENDAFTARAN)
                If dsPulang IsNot Nothing Then
                    TanggalPulang = dsPulang.DATE
                End If
                TanggalDatang = dsPendaftaran.DATE
                sKDPEDAFTARAN_H = dsPendaftaran.KDPENDAFTARAN

            End If

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

            SQL = " SELECT  "
            SQL &= " D.KDPENDAFTARAN "
            'SQL &= " ,KELOMPOKPASIEN = (Select BB.MEMO FROM M_CUSTOMER AA INNER JOIN M_PENJAMIN BB On AA.KDPENJAMIN = BB.KDPENJAMIN WHERE D.KDCUSTOMER = AA.KDCUSTOMER) "
            'SQL &= " ,KELOMPOKPASIEN = ISNULL((SELECT MEMO FROM M_DAFTAR_L1 WHERE D.KDDAFTAR_L1 = KDDAFTAR_L1), '') "
            SQL &= " ,TUJUAN = '" & dsPendaftaran.M_DEPARTMENT.NAME_DISPLAY & "' "
            SQL &= " ,DPJP = (SELECT NAME_DISPLAY FROM M_DOCTOR WHERE D.KDDOCTOR = KDDOCTOR) "
            SQL &= " ,PASIEN = (SELECT NAME_DISPLAY FROM M_CUSTOMER WHERE D.KDCUSTOMER = KDCUSTOMER) "
            SQL &= " ,D.KDCUSTOMER "
            SQL &= " ,C.ALAMAT "
            SQL &= " ,KELAS = (SELECT MEMO FROM M_KELASRAWAT WHERE D.KDKELASRAWAT = KDKELASRAWAT) "
            SQL &= " ,TANGGAL_DATANG = D.DATE "
            SQL &= " ,D.CATEGORY "
            SQL &= " ,C.KDKUNJUNGAN "
            SQL &= " ,NAMAKUNJUNGAN = (SELECT NAME_DISPLAY FROM M_DEPARTMENT WHERE C.KDDEPARTMENT = KDDEPARTMENT) "
            SQL &= " ,A.KDSOTRANSAKSI "
            SQL &= " ,TANGGAL_INPUT = A.DATE "
            SQL &= " ,ITEM_GROUP = F.MEMO "
            If dsPendaftaran.M_DAFTAR_L1.MEMO.Contains("BPJS") Then
                SQL &= " ,ITEM = E.NMITEM2 "
            Else
                SQL &= " ,ITEM = E.NMITEM2  + ' (' + (SELECT NAME_DISPLAY FROM M_DOCTOR WHERE B.KDDOCTOR = KDDOCTOR) + ')' "
            End If
            SQL &= " ,QTY = SUM(B.QTY) "
            SQL &= " ,GRANDTOTAL = SUM(B.GRANDTOTAL) "
            SQL &= " FROM "
            SQL &= " S_SO_TRANSAKSI_H A "
            SQL &= " INNER JOIN S_SO_TRANSAKSI_D B "
            SQL &= " ON A.KDSOTRANSAKSI = B.KDSOTRANSAKSI "
            SQL &= " INNER JOIN S_PENDAFTARAN_KUNJUNGAN C "
            SQL &= " ON A.KDKUNJUNGAN = C.KDKUNJUNGAN "
            SQL &= " INNER JOIN S_PENDAFTARAN_H D "
            SQL &= " ON C.KDPENDAFTARAN = D.KDPENDAFTARAN "
            SQL &= " INNER JOIN M_ITEM E "
            SQL &= " ON B.KDITEM = E.KDITEM "
            SQL &= " INNER JOIN M_ITEM_L2 F "
            SQL &= " ON E.KDITEM_L2 = F.KDITEM_L2 "
            SQL &= " WHERE  "
            SQL &= " D.KDPENDAFTARAN = '" & dsPendaftaran.KDPENDAFTARAN & "' "
            SQL &= " OR "
            SQL &= " D.KDPENDAFTARAN = '" & dsPendaftaran.KDPENDAFTARAN_AWAL & "' "
            SQL &= " GROUP BY "
            SQL &= " D.KDPENDAFTARAN "
            SQL &= " ,D.KDDOCTOR "
            SQL &= " ,D.KDCUSTOMER "
            SQL &= " ,C.ALAMAT "
            SQL &= " ,D.KDKELASRAWAT "
            SQL &= " ,D.DATE "
            SQL &= " ,D.CATEGORY "
            SQL &= " ,C.KDKUNJUNGAN "
            SQL &= " ,C.KDDEPARTMENT  "
            SQL &= " ,A.KDSOTRANSAKSI "
            SQL &= " ,F.MEMO "
            SQL &= " ,E.NMITEM2 "
            SQL &= " ,A.DATE "
            SQL &= " ,B.KDDOCTOR "
            SQL &= " ORDER BY A.DATE "

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
                    dsRekap.KDPENDAFTARAN = sKDPEDAFTARAN_H '.Rows(iLoop)("KDPENDAFTARAN")
                    dsRekap.KELOMPOKPASIEN = dsPendaftaran.M_DAFTAR_L1.MEMO
                    dsRekap.DPJP = .Rows(iLoop)("DPJP")
                    dsRekap.PASIEN = .Rows(iLoop)("PASIEN")
                    dsRekap.KDCUSTOMER = .Rows(iLoop)("KDCUSTOMER")
                    dsRekap.ALAMAT = .Rows(iLoop)("ALAMAT")
                    dsRekap.TUJUAN = .Rows(iLoop)("TUJUAN")
                    dsRekap.KELAS = .Rows(iLoop)("KELAS")
                    dsRekap.TANGGAL_DATANG = TanggalDatang
                    dsRekap.TANGGAL_PULANG = TanggalPulang
                    dsRekap.CATEGORY = .Rows(iLoop)("CATEGORY")
                    dsRekap.KDKUNJUNGAN = .Rows(iLoop)("KDKUNJUNGAN")
                    dsRekap.NAMAKUNJUNGAN = .Rows(iLoop)("NAMAKUNJUNGAN")
                    dsRekap.KDSOTRANSAKSI = .Rows(iLoop)("KDSOTRANSAKSI")
                    dsRekap.TANGGAL_INPUT = .Rows(iLoop)("TANGGAL_INPUT")
                    dsRekap.ITEM_GROUP = .Rows(iLoop)("ITEM_GROUP")
                    dsRekap.ITEM = .Rows(iLoop)("ITEM")
                    dsRekap.QTY = .Rows(iLoop)("QTY")
                    dsRekap.GRANDTOTAL = .Rows(iLoop)("GRANDTOTAL")

                    listTranskasi.Add(dsRekap)

                    sPrintGrandTotal = .Rows(iLoop)("GRANDTOTAL")
                End With
            Next

            Dim rpt As New xtraCashInBilling

            rpt.ShowPrintMarginsWarning = False
            rpt.Watermark.Text = sWATERMARK

            rpt.bindingSource.DataSource = listTranskasi
            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
            printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try

    End Sub
    Private Sub AddMutasiPasienToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddMutasiPasienToolStripMenuItem.Click
        If grv.GetFocusedRowCellValue("KDPENDAFTARAN") Is Nothing Then
            Exit Sub
        End If

        Dim frmPendaftaran_Kunjungan As New frmPendaftaran_Kunjungan
        Try
            frmPendaftaran_Kunjungan.LoadMe(FORM_MODE.FORM_MODE_ADD, grv.GetFocusedRowCellValue("KDPENDAFTARAN"))
            frmPendaftaran_Kunjungan.ShowDialog(Me)

            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)

        End Try
    End Sub
    Private Sub PasienPulangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasienPulangToolStripMenuItem.Click
        Dim frmUpdate_Tanggal_Pulang As New frmUpdate_Tanggal_Pulang
        Try
            If grv.GetFocusedRowCellValue("KDSOTRANSAKSI") = String.Empty Then Exit Sub

            Dim dsSales = oSalesOrderTransaksi.GetData(grv.GetFocusedRowCellValue("KDSOTRANSAKSI"))

            sREKAMMEDIS = dsSales.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.KDCUSTOMER.ToString

            frmUpdate_Tanggal_Pulang.LoadMe(FORM_MODE.FORM_MODE_ADD)
            frmUpdate_Tanggal_Pulang.ShowDialog(Me)
            fn_LoadSecurity()

            sREKAMMEDIS = ""
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub CetakForamt1ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakForamt1ToolStripMenuItem.Click
        Try
            If grv.GetFocusedRowCellValue("KDSOTRANSAKSI") = String.Empty Then Exit Sub

            Dim rpt As New xtraSalesOrder

            rpt.ShowPrintMarginsWarning = False
            rpt.Watermark.Text = sWATERMARK
            Dim ds = oSalesOrderTransaksi.GetData(grv.GetFocusedRowCellValue("KDSOTRANSAKSI"))
            rpt.bindingSource.DataSource = ds
            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
            printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub CetakUkuranKecilToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakUkuranKecilToolStripMenuItem.Click
        Try
            If grv.GetFocusedRowCellValue("KDSOTRANSAKSI") = String.Empty Then Exit Sub

            Dim rpt As New xtraSalesOrderKecil

            rpt.ShowPrintMarginsWarning = False
            rpt.Watermark.Text = sWATERMARK
            Dim ds = oSalesOrderTransaksi.GetData(grv.GetFocusedRowCellValue("KDSOTRANSAKSI"))
            rpt.bindingSource.DataSource = ds
            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
            printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub picCetakResep_Click(sender As Object, e As EventArgs) Handles picCetakResep.Click

    End Sub
#End Region
End Class