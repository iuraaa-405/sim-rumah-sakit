Imports DataAccess
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmRiwayatResep
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sIdentitas As String = String.Empty
    Private sNoRekamMedis As String = String.Empty
    Private sKategori As Integer = 0

#Region "Function"
    Public Sub LoadMe(ByVal NoRekamMedis As String, ByVal Kategori As Integer)
        sNoTransaksi = String.Empty
        sNoRekamMedis = NoRekamMedis
        sKategori = Kategori
        Dim oCustomer As New Reference.clsCustomer
        Dim dsCustomer = oCustomer.GetData(NoRekamMedis)
        If dsCustomer IsNot Nothing Then
            sIdentitas = "Riwayat Resep " & dsCustomer.KDCUSTOMER & " - " & dsCustomer.NAME_DISPLAY
        End If
    End Sub
    Private Sub fn_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = sIdentitas
        fn_LoadSecurity()
    End Sub
    Private Sub fn_LoadSecurity()
        Try
            Dim oOtority As New Setting.clsOtority
            Dim oUser As New Setting.clsUser

            Dim ds = (From x In oOtority.GetDataDetail
                      Join y In oUser.GetData
                      On x.KDOTORITY Equals y.KDOTORITY
                      Where x.MODUL = "REQ_RECIPE" _
                      And y.KDUSER = sUserID
                      Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try

                picRefresh.Enabled = ds.ISVIEW

                If ds.ISVIEW = True Then
                    fn_Preview()
                End If
            Catch ex As Exception
                MsgBox("Keamanan belum dipasang, tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)

                picRefresh.Enabled = False
            End Try
        Catch ex As Exception
            MsgBox("Load Security : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        fn_Validate = True

    End Function
    Private Sub fn_Preview()
        If fn_Validate() Then
            Try
                grv.Columns.Clear()
                grd.DataSource = Nothing
                grv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

                If sKategori = 0 Then
                    fn_LoadDataRiwayatResep()
                ElseIf sKategori = 1
                    fn_LoadDataRiwayatObat()
                ElseIf sKategori = 2
                    fn_LoadDataBhp()
                End If

            Catch oErr As Exception
                MsgBox("Preview Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub fn_LoadDataRiwayatResep()
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
        SQL &= "Kategori = 'CPPT' "
        SQL &= ",NoTransaksi = A.KDCPPT "
        SQL &= ",Penjamin = B.KDDAFTAR_L1_NAMA "
        SQL &= ",NoRegister = B.KDKUNJUNGAN "
        SQL &= ",TanggalResep = A.DATE "
        SQL &= ",NoRM = B.KDCUSTOMER "
        SQL &= ",Pasien = B.NAMAPASIEN "
        SQL &= ",Poli = B.KDDEPARTMENT_NAMA "
        SQL &= ",Dokter = B.KDDOCTOR_NAMA "
        SQL &= ",BukanRacikan = ISNULL((SELECT TOP 1 'Ya' FROM R_CPPT_NONRACIKAN WHERE A.KDCPPT = KDCPPT), '') "
        SQL &= ",Racikan = ISNULL((SELECT TOP 1 'Ya' FROM R_CPPT_RACIKAN WHERE A.KDCPPT = KDCPPT), '') "
        SQL &= "FROM R_CPPT AS A "
        SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
        SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
        SQL &= "WHERE B.KDCUSTOMER = '" & sNoRekamMedis & "' "

        SQL &= "UNION "

        SQL &= "SELECT "
        SQL &= "Kategori = 'ORDER' "
        SQL &= ",NoTransaksi = A.KDORDER "
        SQL &= ",Penjamin = B.KDDAFTAR_L1_NAMA "
        SQL &= ",NoRegister = B.KDKUNJUNGAN "
        SQL &= ",TanggalResep = A.TANGGALORDER "
        SQL &= ",NoRM = B.KDCUSTOMER "
        SQL &= ",Pasien = B.NAMAPASIEN "
        SQL &= ",Poli = B.KDDEPARTMENT_NAMA "
        SQL &= ",Dokter = B.KDDOCTOR_NAMA "
        SQL &= ",BukanNoRacikan = ISNULL((SELECT TOP 1 'Ya' FROM R_ORDER_NONRACIKAN WHERE A.KDORDER = KDORDER), '') "
        SQL &= ",Racikan = '' "
        SQL &= "FROM R_ORDER AS A "
        SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
        SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
        SQL &= "WHERE B.KDCUSTOMER = '" & sNoRekamMedis & "' "
        SQL &= "AND A.MEMO = 'ORDER FARMASI' "


        SQL &= ") X "
        SQL &= "WHERE "
        SQL &= "X.BukanRacikan <> '' "
        SQL &= "OR X.Racikan <> '' "
        SQL &= "ORDER BY X.TanggalResep DESC "

        oComm.Connection = oConn
        oComm.CommandText = SQL
        oComm.CommandTimeout = 120
        oComm.CommandType = CommandType.Text

        da = New SqlDataAdapter(oComm)

        da.Fill(ds, "S_LIS_H")

        If ds.Tables("S_LIS_H").Rows.Count < 1 Then Exit Sub

        SQL = "SELECT "
        SQL &= "NoTransaksi = A.KDCPPT "
        SQL &= ",Obat = B.NAMAOBAT "
        SQL &= ",Satuan = B.SATUAN "
        SQL &= ",Signa = B.SIGNA "
        SQL &= ",CaraPakai = B.CARAPAKAI "
        SQL &= ",Jumlah = B.JUMLAH "
        SQL &= ",Catatan = B.REMARKS_DOKTER "
        SQL &= "FROM R_CPPT AS A "
        SQL &= "INNER JOIN R_CPPT_NONRACIKAN AS B "
        SQL &= "ON A.KDCPPT = B.KDCPPT "
        SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST C "
        SQL &= "ON A.KDIDENTITAS = C.KDIDENTITAS "
        SQL &= "WHERE C.KDCUSTOMER = '" & sNoRekamMedis & "' "


        SQL &= "UNION "

        SQL &= "SELECT "
        SQL &= "NoTransaksi = A.KDORDER "
        SQL &= ",Obat = B.NAMAOBAT "
        SQL &= ",Satuan = B.SATUAN "
        SQL &= ",Signa = B.SIGNA "
        SQL &= ",CaraPakai = B.CARAPAKAI "
        SQL &= ",Jumlah = B.JUMLAH "
        SQL &= ",Catatan = B.REMARKS_DOKTER "
        SQL &= "FROM R_ORDER AS A "
        SQL &= "INNER JOIN R_ORDER_NONRACIKAN AS B "
        SQL &= "ON A.KDORDER = B.KDORDER "
        SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST C "
        SQL &= "ON A.KDIDENTITAS = C.KDIDENTITAS "
        SQL &= "WHERE C.KDCUSTOMER = '" & sNoRekamMedis & "' "


        oComm.Connection = oConn
        oComm.CommandText = SQL
        oComm.CommandTimeout = 120
        oComm.CommandType = CommandType.Text

        da = New SqlDataAdapter(oComm)
        da.Fill(ds, "S_LIS_D")

        Dim keyColumn As DataColumn = ds.Tables("S_LIS_H").Columns("NoTransaksi")
        Dim foreignKeyColumn As DataColumn = ds.Tables("S_LIS_D").Columns("NoTransaksi")
        ds.Relations.Add("FK_S_LIS_H_S_LIS_D", keyColumn, foreignKeyColumn)

        grd.DataSource = ds.Tables("S_LIS_H")
        grd.ForceInitialize()

        grd.LevelTree.Nodes.Add("FK_S_LIS_H_S_LIS_D", grv1)
        grv1.ViewCaption = "Details"

        grv1.PopulateColumns(ds.Tables("S_LIS_D"))
        grv1.Columns("NoTransaksi").VisibleIndex = -1

        fn_SetFormat()

        If oConn.State = ConnectionState.Open Then
            oConn.Close()
        End If
    End Sub
    Private Sub fn_LoadDataRiwayatObat()
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
        SQL &= "JenisRawat = CASE B.CATEGORY WHEN '1' THEN 'RJ' ELSE 'RI' END  "
        SQL &= ",NoTransaksi = A.KDSOTRANSAKSI "
        SQL &= ",NoRegister = A.KDKUNJUNGAN "
        SQL &= ",TanggalResep = A.DATE "
        SQL &= ",NoRM = B.KDCUSTOMER "
        SQL &= ",Pasien = C.NAME_DISPLAY "
        SQL &= ",Tujuan = D.NAME_DISPLAY "
        SQL &= ",DPJP = E.NAME_DISPLAY "
        SQL &= "FROM S_SO_TRANSAKSI_H AS A "
        SQL &= "INNER JOIN S_PENDAFTARAN_KUNJUNGAN AS AB "
        SQL &= "ON A.KDKUNJUNGAN = AB.KDKUNJUNGAN "
        SQL &= "INNER JOIN S_PENDAFTARAN_H AS B "
        SQL &= "ON AB.KDPENDAFTARAN = B.KDPENDAFTARAN "
        SQL &= "INNER JOIN M_CUSTOMER AS C "
        SQL &= "ON B.KDCUSTOMER = C.KDCUSTOMER "
        SQL &= "INNER JOIN M_DEPARTMENT AS D "
        SQL &= "ON AB.KDDEPARTMENT = D.KDDEPARTMENT "
        SQL &= "INNER JOIN M_DOCTOR AS E "
        SQL &= "ON AB.KDDOCTOR = E.KDDOCTOR "
        SQL &= "WHERE B.KDCUSTOMER = '" & sNoRekamMedis & "' "
        SQL &= "AND A.CATEGORY = 4 "
        SQL &= "ORDER BY A.DATE DESC "

        oComm.Connection = oConn
        oComm.CommandText = SQL
        oComm.CommandTimeout = 120
        oComm.CommandType = CommandType.Text

        da = New SqlDataAdapter(oComm)

        da.Fill(ds, "S_LIS_H")

        If ds.Tables("S_LIS_H").Rows.Count < 1 Then Exit Sub

        SQL = "SELECT "
        SQL &= "NoTransaksi = A.KDSOTRANSAKSI "
        SQL &= ",NamaObat = D.NMITEM2 "
        SQL &= ",Signa = E.MEMO "
        SQL &= ",CaraPakai = F.MEMO "
        SQL &= ",Jumlah = SUM(C.QTY) "
        SQL &= ",Catatan = C.REMARKS "
        SQL &= "FROM S_SO_TRANSAKSI_H AS A "
        SQL &= "INNER JOIN S_PENDAFTARAN_KUNJUNGAN AS AB "
        SQL &= "ON A.KDKUNJUNGAN = AB.KDKUNJUNGAN "
        SQL &= "INNER JOIN S_PENDAFTARAN_H AS B "
        SQL &= "ON AB.KDPENDAFTARAN = B.KDPENDAFTARAN "
        SQL &= "INNER JOIN S_SO_TRANSAKSI_D C "
        SQL &= "ON A.KDSOTRANSAKSI = C.KDSOTRANSAKSI "
        SQL &= "INNER JOIN M_ITEM D "
        SQL &= "ON C.KDITEM = D.KDITEM "
        SQL &= "INNER JOIN M_SIGNA E "
        SQL &= "ON C.KDSIGNA = E.KDSIGNA "
        SQL &= "INNER JOIN M_CARAPAKAI F "
        SQL &= "ON C.KDCARAPAKAI = F.KDCARAPAKAI "
        SQL &= "WHERE B.KDCUSTOMER = '" & sNoRekamMedis & "' "
        SQL &= "AND A.CATEGORY = 4 "
        SQL &= "GROUP BY "
        SQL &= "A.KDSOTRANSAKSI "
        SQL &= ",D.NMITEM2 "
        SQL &= ",E.MEMO "
        SQL &= ",F.MEMO "
        SQL &= ",C.REMARKS "
        SQL &= ",C.SEQ "
        SQL &= "ORDER BY C.SEQ "

        oComm.Connection = oConn
        oComm.CommandText = SQL
        oComm.CommandTimeout = 120
        oComm.CommandType = CommandType.Text

        da = New SqlDataAdapter(oComm)
        da.Fill(ds, "S_LIS_D")

        Dim keyColumn As DataColumn = ds.Tables("S_LIS_H").Columns("NoTransaksi")
        Dim foreignKeyColumn As DataColumn = ds.Tables("S_LIS_D").Columns("NoTransaksi")
        ds.Relations.Add("FK_S_LIS_H_S_LIS_D", keyColumn, foreignKeyColumn)

        grd.DataSource = ds.Tables("S_LIS_H")
        grd.ForceInitialize()

        grd.LevelTree.Nodes.Add("FK_S_LIS_H_S_LIS_D", grv1)
        grv1.ViewCaption = "Details"

        grv1.PopulateColumns(ds.Tables("S_LIS_D"))
        grv1.Columns("NoTransaksi").VisibleIndex = -1

        fn_SetFormat()

        If oConn.State = ConnectionState.Open Then
            oConn.Close()
        End If
    End Sub
    Private Sub fn_LoadDataBhp()
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
        SQL &= "NoTransaksi = A.KDBHPPASIEN  "
        SQL &= ",NoRegister = B.KDPENDAFTARAN "
        SQL &= ",Tanggal = A.DATE "
        SQL &= ",BHPRuang = C.NAME_DISPLAY "
        SQL &= ",Catatan = A.MEMO "
        SQL &= "FROM "
        SQL &= "DATABASERS..I_BHPPASIEN_H A "
        SQL &= "INNER JOIN DATABASERS..S_PENDAFTARAN_KUNJUNGAN B "
        SQL &= "ON A.KDKUNJUNGAN = B.KDKUNJUNGAN "
        SQL &= "INNER JOIN DATABASERS..M_WAREHOUSE C "
        SQL &= "ON A.KDWAREHOUSE = C.KDWAREHOUSE "
        SQL &= "INNER JOIN DATABASERS..S_PENDAFTARAN_H D "
        SQL &= "ON B.KDPENDAFTARAN = D.KDPENDAFTARAN "
        SQL &= "WHERE "
        SQL &= "D.KDCUSTOMER = '" & sNoRekamMedis & "' "
        SQL &= "ORDER BY A.DATE "

        oComm.Connection = oConn
        oComm.CommandText = SQL
        oComm.CommandTimeout = 120
        oComm.CommandType = CommandType.Text

        da = New SqlDataAdapter(oComm)

        da.Fill(ds, "S_LIS_H")

        If ds.Tables("S_LIS_H").Rows.Count < 1 Then Exit Sub

        'SQL = "SELECT "
        'SQL &= "NoTransaksi = A.KDSOTRANSAKSI "
        'SQL &= ",NamaObat = D.NMITEM2 "
        'SQL &= ",Signa = E.MEMO "
        'SQL &= ",CaraPakai = F.MEMO "
        'SQL &= ",Jumlah = SUM(C.QTY) "
        'SQL &= ",Catatan = C.REMARKS "
        'SQL &= "FROM S_SO_TRANSAKSI_H AS A "
        'SQL &= "INNER JOIN S_PENDAFTARAN_KUNJUNGAN AS AB "
        'SQL &= "ON A.KDKUNJUNGAN = AB.KDKUNJUNGAN "
        'SQL &= "INNER JOIN S_PENDAFTARAN_H AS B "
        'SQL &= "ON AB.KDPENDAFTARAN = B.KDPENDAFTARAN "
        'SQL &= "INNER JOIN S_SO_TRANSAKSI_D C "
        'SQL &= "ON A.KDSOTRANSAKSI = C.KDSOTRANSAKSI "
        'SQL &= "INNER JOIN M_ITEM D "
        'SQL &= "ON C.KDITEM = D.KDITEM "
        'SQL &= "INNER JOIN M_SIGNA E "
        'SQL &= "ON C.KDSIGNA = E.KDSIGNA "
        'SQL &= "INNER JOIN M_CARAPAKAI F "
        'SQL &= "ON C.KDCARAPAKAI = F.KDCARAPAKAI "
        'SQL &= "WHERE B.KDCUSTOMER = '" & sNoRekamMedis & "' "
        'SQL &= "AND A.CATEGORY = 4 "
        'SQL &= "GROUP BY "
        'SQL &= "A.KDSOTRANSAKSI "
        'SQL &= ",D.NMITEM2 "
        'SQL &= ",E.MEMO "
        'SQL &= ",F.MEMO "
        'SQL &= ",C.REMARKS "
        'SQL &= ",C.SEQ "
        'SQL &= "ORDER BY C.SEQ "

        oComm.Connection = oConn
        oComm.CommandText = SQL
        oComm.CommandTimeout = 120
        oComm.CommandType = CommandType.Text

        da = New SqlDataAdapter(oComm)
        da.Fill(ds, "S_LIS_D")

        Dim keyColumn As DataColumn = ds.Tables("S_LIS_H").Columns("NoTransaksi")
        Dim foreignKeyColumn As DataColumn = ds.Tables("S_LIS_D").Columns("NoTransaksi")
        ds.Relations.Add("FK_S_LIS_H_S_LIS_D", keyColumn, foreignKeyColumn)

        grd.DataSource = ds.Tables("S_LIS_H")
        grd.ForceInitialize()

        grd.LevelTree.Nodes.Add("FK_S_LIS_H_S_LIS_D", grv1)
        grv1.ViewCaption = "Details"

        grv1.PopulateColumns(ds.Tables("S_LIS_D"))
        grv1.Columns("NoTransaksi").VisibleIndex = -1

        fn_SetFormat()

        If oConn.State = ConnectionState.Open Then
            oConn.Close()
        End If
    End Sub
    Private Sub fn_SetFormat()
        For iLoop As Integer = 0 To grv.Columns.Count - 1
            If grv.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                grv.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, grv.Columns(iLoop).FieldName, grv.Columns(iLoop),
                                     "{0:n2}")
                grv.Columns(iLoop).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                grv.Columns(iLoop).SummaryItem.DisplayFormat = "{0:n2}"
            ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm:ss}"
            End If
        Next

        Try
            grv.Columns("NoTransaksi").VisibleIndex = -1
            grv.Columns("NoRegister").VisibleIndex = -1
            grv.Columns("NoRM").VisibleIndex = -1
            grv.Columns("Pasien").VisibleIndex = -1
        Catch ex As Exception

        End Try

    End Sub
#End Region
#Region "Command Button"
    Private Sub frmMember_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                Me.Close()
            Case Keys.R
                If e.Alt = True And picRefresh.Enabled = True Then
                    picRefresh_Click()
                End If
        End Select
    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_Preview()
    End Sub
    Private Sub AmbilTransaksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AmbilTransaksiToolStripMenuItem.Click
        If grv.GetFocusedRowCellValue("NoTransaksi") Is Nothing Then
            sNoTransaksi = String.Empty
            Exit Sub
        End If

        sNoTransaksi = grv.GetFocusedRowCellValue("NoTransaksi")
        Me.Close()
    End Sub
#End Region
End Class