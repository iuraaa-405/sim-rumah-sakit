Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient
Imports Newtonsoft.Json.Linq
Imports DevExpress.XtraPrinting
Imports MySql.Data.MySqlClient

Public Class frmPendaftaranList
    Implements ILanguage

    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oPendaftaran As New Admission.clsPendaftaran

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = Pendaftaran.TITLE

        deDATEFrom.DateTime = Now
        deDATETo.DateTime = Now
        deDATE.DateTime = Now

        fn_LoadSecurity()

        'Try
        '    grv.RestoreLayoutFromRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text)
        'Catch ex As Exception

        'End Try
    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        'Try
        '    grv.SaveLayoutToRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text)
        'Catch ex As Exception

        'End Try
    End Sub
    Private Sub fn_LoadSecurity()
        Try
            Dim oOtority As New Setting.clsOtority
            Dim oUser As New Setting.clsUser

            Dim ds = (From x In oOtority.GetDataDetail
                      Join y In oUser.GetData
                      On x.KDOTORITY Equals y.KDOTORITY
                      Where x.MODUL = "PENDAFTARAN" _
                      And y.KDUSER = sUserID
                      Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                picAdd.Enabled = ds.ISADD
                picDelete.Enabled = ds.ISDELETE
                picBatal.Enabled = ds.ISDELETE
                picUpdate.Enabled = ds.ISUPDATE
                picPrint.Enabled = ds.ISPRINT
                picRefresh.Enabled = ds.ISVIEW
                picOperasi.Enabled = ds.ISADD
                picPanggil.Enabled = ds.ISADD
                picReset.Enabled = ds.ISADD

                If ds.ISVIEW = True Then
                    fn_LoadData()
                    fn_LoadLanguage()
                    fn_LoadDepartment()
                    If chkBukaAntrianSisa.Checked = True Then
                        fn_LoadSisa()
                    End If
                    'Try
                    '    grv.RestoreLayoutFromRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text)
                    'Catch ex As Exception

                    'End Try
                End If
            Catch oErr As Exception
                MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)

                picAdd.Enabled = False
                picDelete.Enabled = False
                picBatal.Enabled = False
                picUpdate.Enabled = False
                picPrint.Enabled = False
                picRefresh.Enabled = False
                picOperasi.Enabled = False
                picPanggil.Enabled = False
                picReset.Enabled = False
            End Try
        Catch oErr As Exception
            MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            Me.Text = Pendaftaran.TITLE

            fn_LoadLanguageMaster()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Sub fn_LoadLanguageMaster()
        Try
            grv.Columns("KDPENDAFTARAN").Caption = Pendaftaran.KDPENDAFTARAN
            grv.Columns("TANGGAL").Caption = Pendaftaran.TANGGAL
            grv.Columns("TUJUAN").Caption = Pendaftaran.KDDEPARTMENT
            grv.Columns("DOKTER").Caption = Pendaftaran.KDDOCTOR
            grv.Columns("KDCUSTOMER").Caption = "Rekam Medis"
            grv.Columns("PASIEN").Caption = Customer.NAME_DISPLAY
            grv.Columns("KDUSER").Caption = Caption.User
            grv.Columns("STATUSDAFTAR").Caption = Pendaftaran.STATUSDAFTAR
            grv.Columns("JENISPASIEN").Caption = "Jenis Pasien"

            grv.Columns("DAFTAR_L1").Caption = sDaftar_L1
            grv.Columns("DAFTAR_L2").Caption = sDaftar_L2
            grv.Columns("DAFTAR_L3").Caption = sDaftar_L3
            grv.Columns("DAFTAR_L4").Caption = sDaftar_L4
            grv.Columns("DAFTAR_L5").Caption = sDaftar_L5
            grv.Columns("DAFTAR_L6").Caption = sDaftar_L6

        Catch oErr As Exception

        End Try
    End Sub
    Private Sub fn_LoadHistoryPasien(ByVal sKDCUSTOMER As String)
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
            SQL &= "NomorPendaftaran = A.KDPENDAFTARAN "
            SQL &= ",NomorPemetaan = B.KDUPDATE_APLICARE "
            SQL &= ",Tanggal = B.DATE "
            SQL &= ",Tujuan = C.NAME_DISPLAY  "
            SQL &= ",Dokter = D.NAME_DISPLAY "
            SQL &= ",B.KDKUNJUNGAN "
            SQL &= ",CekPemetaan = (SELECT CASE A.CATEGORY WHEN 0 THEN CONVERT(BIT, 0) ELSE ISNULL((SELECT CASE WHEN A.KDUPDATE_APLICARE = B.KDUPDATE_APLICARE THEN (SELECT CASE A.KDUPDATE_APLICARE WHEN '' THEN CONVERT(BIT, 0) ELSE CONVERT(BIT, 1) END) ELSE CONVERT(BIT, 0) END), CONVERT(BIT, 0)) END) "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_H A "
            SQL &= "INNER JOIN S_PENDAFTARAN_KUNJUNGAN B "
            SQL &= "ON A.KDPENDAFTARAN = B.KDPENDAFTARAN "
            SQL &= "INNER JOIN M_DEPARTMENT C "
            SQL &= "ON B.KDDEPARTMENT = C.KDDEPARTMENT "
            SQL &= "INNER JOIN M_DOCTOR D "
            SQL &= "ON B.KDDOCTOR = D.KDDOCTOR "
            SQL &= "WHERE A.KDCUSTOMER = '" & sKDCUSTOMER & "' "
            SQL &= "ORDER BY B.DATE DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "HISTORY")

            grdHistoryPasien.MainView = grvHistoryPasien
            grdHistoryPasien.DataSource = ds.Tables("HISTORY")
            grdHistoryPasien.ForceInitialize()

            fn_LoadFormatData()

            grvHistoryPasien.Columns("NomorPemetaan").VisibleIndex = -1

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData()
        'Try
        '    Dim ds = From x In oPendaftaran.GetData
        '             Where x.DATE.ToString("yyyyMMdd") >= deDATEFrom.DateTime.ToString("yyyyMMdd") And x.DATE.ToString("yyyyMMdd") <= deDATETo.DateTime.ToString("yyyyMMdd")
        '             Select x.KDPENDAFTARAN, TANGGAL = x.DATE, DAFTAR_L1 = x.M_DAFTAR_L1.MEMO, DAFTAR_L2 = x.M_DAFTAR_L2.MEMO, DAFTAR_L3 = x.M_DAFTAR_L3.MEMO, DAFTAR_L4 = x.M_DAFTAR_L4.MEMO, DAFTAR_L5 = x.M_DAFTAR_L5.MEMO, DAFTAR_L6 = x.M_DAFTAR_L6.MEMO, x.KDCUSTOMER, PASIEN = x.M_CUSTOMER.NAME_DISPLAY, x.KDUSER, STATUSDAFTAR = IIf(x.STATUSDAFTAR = 0, "DAFTAR", "BATAL")
        '    grd.DataSource = ds.ToList

        '    fn_LoadFormatData()
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try

        grvHistoryPasien.OptionsSelection.MultiSelect = True
        grvHistoryPasien.SelectAll()
        grvHistoryPasien.DeleteSelectedRows()
        grvHistoryPasien.OptionsSelection.MultiSelect = False

        Try
            grv.Columns.Clear()
            grd.DataSource = Nothing

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
            'SQL &= "No = ROW_NUMBER() OVER (ORDER BY A.KDPENDAFTARAN) "
            SQL &= "A.KDPENDAFTARAN "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",DAFTAR_L1 = E.MEMO "
            SQL &= ",DAFTAR_L2 = F.MEMO "
            SQL &= ",DAFTAR_L3 = G.MEMO "
            SQL &= ",DAFTAR_L4 = H.MEMO "
            SQL &= ",DAFTAR_L5 = I.MEMO "
            SQL &= ",DAFTAR_L6 = J.MEMO "
            SQL &= ",A.KDCUSTOMER "
            SQL &= ",PASIEN = D.NAME_DISPLAY "
            SQL &= ",TUJUAN = B.NAME_DISPLAY  "
            SQL &= ",DOKTER = C.NAME_DISPLAY "
            SQL &= ",NoSEP = A.NOMORSEP "
            SQL &= ",TanggalLahir = D.TANGGALLAHIR "
            SQL &= ",JenisKelamin = CASE D.KDJENISKELAMIN WHEN 0 THEN 'P' ELSE 'L' END "
            SQL &= ",KodeBooking = A.KODEBOOKING "
            SQL &= ",NoSKD = A.NOMORSKDP "
            SQL &= ",A.KDDIAGNOSA "
            SQL &= ",TujuanKunjungan = A.TUJUANKUNJUNGAN "
            SQL &= ",A.KDUSER "
            SQL &= ",STATUSDAFTAR = (SELECT CASE A.STATUSDAFTAR WHEN 0 THEN 'DAFTAR' WHEN 1 THEN 'BATAL' ELSE '' END) "
            SQL &= ",JENISPASIEN = (SELECT CASE D.KDCUSTOMER_LAMA WHEN '' THEN (SELECT CASE WHEN CONVERT(VARCHAR(8), A.DATE, 112) = CONVERT(VARCHAR(8), D.DATECREATED, 112) THEN 'PASIEN BARU' ELSE 'PASIEN LAMA' END) ELSE 'PASIEN LAMA' END) "
            SQL &= ",NaikRanap = (SELECT CASE A.CATEGORY WHEN 0 THEN (SELECT CASE A.KDPENDAFTARAN_AWAL WHEN '' THEN CONVERT(BIT, 0) ELSE CONVERT(BIT, 1) END) ELSE CONVERT(BIT, 0) END) "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_H A "
            SQL &= "INNER JOIN M_DEPARTMENT B "
            SQL &= "ON A.KDDEPARTMENT = B.KDDEPARTMENT "
            SQL &= "INNER JOIN M_DOCTOR C "
            SQL &= "ON A.KDDOCTOR = C.KDDOCTOR "
            SQL &= "INNER JOIN M_CUSTOMER D "
            SQL &= "ON A.KDCUSTOMER = D.KDCUSTOMER "
            SQL &= "INNER JOIN M_DAFTAR_L1 E "
            SQL &= "ON A.KDDAFTAR_L1 = E.KDDAFTAR_L1 "
            SQL &= "INNER JOIN M_DAFTAR_L2 F "
            SQL &= "ON A.KDDAFTAR_L2 = F.KDDAFTAR_L2 "
            SQL &= "INNER JOIN M_DAFTAR_L3 G "
            SQL &= "ON A.KDDAFTAR_L3 = G.KDDAFTAR_L3 "
            SQL &= "INNER JOIN M_DAFTAR_L4 H "
            SQL &= "ON A.KDDAFTAR_L4 = H.KDDAFTAR_L4 "
            SQL &= "INNER JOIN M_DAFTAR_L5 I "
            SQL &= "ON A.KDDAFTAR_L5 = I.KDDAFTAR_L5 "
            SQL &= "INNER JOIN M_DAFTAR_L6 J "
            SQL &= "ON A.KDDAFTAR_L6 = J.KDDAFTAR_L6 "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "ORDER BY A.DATE DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

            grd.MainView = grv
            grd.DataSource = ds.Tables("ALL")
            grd.ForceInitialize()

            fn_LoadFormatData()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadSisa()
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

            SQL = "SELECT A.KODE, A.SISA FROM SET_PANGGIL_SISA A "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "SET_PANGGIL_SISA")

            grdSISA.MainView = grvSISA
            grdSISA.DataSource = ds.Tables("SET_PANGGIL_SISA")
            grdSISA.ForceInitialize()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
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
        For iLoop As Integer = 0 To grvHistoryPasien.Columns.Count - 1
            If grvHistoryPasien.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvHistoryPasien.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvHistoryPasien.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grvHistoryPasien.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvHistoryPasien.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvHistoryPasien.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvHistoryPasien.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm:ss}"
            End If
        Next
        grv.BestFitColumns()
        grvHistoryPasien.BestFitColumns()

    End Sub
    'Private Sub MasterColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MasterColumnChooserToolStripMenuItem.Click
    '    grv.ShowCustomization()
    'End Sub
    Private Function fn_DeleteData(ByVal sKDPENDAFTARAN As String, ByVal sNoIdKunjungan As String, ByVal sNoIdSKD As String) As Boolean
        Try
            If oPendaftaran.DeleteData(sKDPENDAFTARAN, sNoIdKunjungan, sNoIdSKD, sKDPENDAFTARAN) = False Then
                'jika gagal
                oPendaftaran.UpdateDataBatal(sKDPENDAFTARAN, sUserID)
            End If

            fn_DeleteData = True

        Catch oErr As Exception
            fn_DeleteData = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
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
            If grv.GetFocusedRowCellValue("NoSEP") = String.Empty Then
                fn_DeleteSEP = True
                Exit Function
            End If

            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim uTime As Integer = 0

            If sVclaim_ConsId <> "" Then
                Dim jsonRequest As String = String.Empty

                jsonRequest = "{" & """request"": {" & """t_sep"": {" & """noSep"": """ & grv.GetFocusedRowCellValue("NoSEP") & """," & """user"": """ & sUserID & """" & "}}}"

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
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub grv_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs)
        If grv.IsFilterRow(e.RowHandle) Then Exit Sub
        If grv.GetRowCellValue(e.RowHandle, "STATUSDAFTAR") = "BATAL" Then
            e.Appearance.BackColor = Color.LightYellow
        End If
    End Sub
    'Private Sub picPending_Click(sender As Object, e As EventArgs)
    '    ContextMenuStrip1.Show(picPending.Location.X, picPending.Location.Y + 125)
    'End Sub
    'Private Sub mnuPending_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picPending.Click
    '    ContextMenuStrip1.Show(picPending.Location.X, picPending.Location.Y + 125)
    'End Sub
    Private Sub grv_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grv.FocusedRowChanged
        If grv.GetFocusedRowCellValue("KDCUSTOMER") Is Nothing Then
            'fn_EmptyM()
            Exit Sub
        End If

        fn_LoadHistoryPasien(grv.GetFocusedRowCellValue("KDCUSTOMER"))
        grvHistoryPasien.Columns("KDKUNJUNGAN").VisibleIndex = -1
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmMember_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.A
                If e.Alt = True And picAdd.Enabled = True Then
                    picAdd_Click()
                End If
            Case Keys.B
                If e.Alt = True And picBatal.Enabled = True Then
                    picBatal_Click()
                End If
            Case Keys.E
                If e.Alt = True And picUpdate.Enabled = True Then
                    picUpdate_Click()
                End If
            Case Keys.D
                If e.Alt = True And picDelete.Enabled = True Then
                    picDelete_Click()
                End If
            Case Keys.P
                If e.Alt = True And picPrint.Enabled = True Then
                    picPrint_Click()
                End If
            Case Keys.R
                If e.Alt = True And picRefresh.Enabled = True Then
                    picRefresh_Click()
                End If
            Case Keys.O
                If e.Alt = True And picOperasi.Enabled = True Then
                    picOperasi_Click()
                End If
        End Select
    End Sub
    Private Sub picReset_Click() Handles picReset.Click
        If MsgBox("Apakah yakin Layar Antrian di Reset?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        Dim oAntrian As New SettingAntrian.clsSetAntrian
        If oAntrian.DeletePanggilAntrian() = True Then
            oAntrian.DeletePanggilAntrianSisa()
            MsgBox("Berhasil direset", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Function fn_SaveSetPanggil(ByVal NomorAntrian As Integer, ByVal kode As String, ByVal isPanggilUlang As Boolean) As Boolean
        Try
            Dim oAntrian As New SettingAntrian.clsSetAntrian
            Dim oSet_panggilan As New AntrianRS.clsSetPanggilSET_PANGGIL_ANTRIAN
            fn_SaveSetPanggil = True

            Dim dsSetPanggilan = oSet_panggilan.GetData(cboLoket.Text)

            ' ***** HEADER *****

            Dim ds = oSet_panggilan.GetStructureHeader

            With ds
                .LOKET = cboLoket.Text
                .KODE = kode
                .NOMORANTRIAN = NomorAntrian
                If isPanggilUlang = False Then
                    .DESCRIPTION = "NOMOR ANTRIAN " & .KODE & " " & .NOMORANTRIAN & " KE LOKET " & .LOKET
                Else
                    .DESCRIPTION = "PANGGILAN ULANG NOMOR ANTRIAN " & .KODE & " " & .NOMORANTRIAN & " KE LOKET " & .LOKET
                End If
                .ISPANGGIL = False
                .SISA = 0
            End With

            If dsSetPanggilan Is Nothing Then
                fn_SaveSetPanggil = oSet_panggilan.InsertData(ds)
            Else
                fn_SaveSetPanggil = oSet_panggilan.UpdateData(ds)
            End If

        Catch oErr As Exception
            MsgBox("Simpan Panggil: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveSetPanggil = False
        End Try
    End Function
    Private Sub fn_Panggil(ByVal Kode As String)
        Try
            Dim oAntrian As New SettingAntrian.clsSetAntrian
            Dim dsAntrianBelumPanggil = oAntrian.GetDataLasAntrianPanggil(deDATE.DateTime, Kode, False)

            If dsAntrianBelumPanggil IsNot Nothing Then
                txtKODEBOOKING.Text = dsAntrianBelumPanggil.KODEBOOKING
                MsgBox("Nomor Antrian ditemukan " & dsAntrianBelumPanggil.KODEBOOKING, MsgBoxStyle.Exclamation, Me.Text)
                fn_SaveSetPanggil(Microsoft.VisualBasic.Right(dsAntrianBelumPanggil.KODEBOOKING, 3), dsAntrianBelumPanggil.JENISPASIEN_RS, False)

                oAntrian.UpdateDataIsCheked(dsAntrianBelumPanggil.KODEBOOKING, 1, "")
                oAntrian.UpdateDataIsCheked(dsAntrianBelumPanggil.KODEBOOKING, 2, "PANGGIL")

                frmErmList.fn_TaskID(dsAntrianBelumPanggil.KODEBOOKING, 2)

                picAddBooking_Click()
            Else
                MsgBox("Nomor Antrian Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox("Panggil: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picPanggil_Click() Handles picPanggil.Click
        If grdKDDEPARTMENT.Text <> "" Then
            MsgBox("Silahkan Tipe", MsgBoxStyle.Exclamation, Me.Text)
            grdKDDEPARTMENT.Focus()

            Dim oDepartment As New Reference.clsDepartment
            fn_Panggil(oDepartment.GetData(grdKDDEPARTMENT.EditValue).ANTRIAN)
        Else
            fn_Panggil("B")
        End If
    End Sub
    Private Sub picPanggilAbtn_Click() Handles picPanggilAbtn.Click
        If grdKDDEPARTMENT.Text <> "" Then
            MsgBox("Silahkan Tipe", MsgBoxStyle.Exclamation, Me.Text)
            grdKDDEPARTMENT.Focus()

            Dim oDepartment As New Reference.clsDepartment
            fn_Panggil(oDepartment.GetData(grdKDDEPARTMENT.EditValue).ANTRIAN)
        Else
            fn_Panggil("A")
        End If
    End Sub
    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        'Lansia
        fn_Panggil("A")
    End Sub
    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        'Umum
        fn_Panggil("C")
    End Sub
    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        'Pasien Baru
        fn_Panggil("D")
    End Sub
    Private Sub btnPanggiOnline_Click(sender As Object, e As EventArgs) Handles btnPanggiOnline.Click
        Try
            frmPanggilOnlineJKN.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmPanggilOnlineJKN Is Nothing Then frmPanggilOnlineJKN.Dispose()
            frmPanggilOnlineJKN = Nothing

            If sKODEBOOKINGONLINEPANGGIL <> "" Then
                Try
                    txtKODEBOOKING.Text = sKODEBOOKINGONLINEPANGGIL

                    Dim oAntrian As New SettingAntrian.clsSetAntrian
                    Dim dsAntrianBelumPanggil = oAntrian.GetData(txtKODEBOOKING.Text)

                    If dsAntrianBelumPanggil IsNot Nothing Then
                        If dsAntrianBelumPanggil.ISONLINE = True Then
                            MsgBox("Nomor Antrian ditemukan " & dsAntrianBelumPanggil.KODEBOOKING, MsgBoxStyle.Exclamation, Me.Text)

                            fn_SaveSetPanggil(dsAntrianBelumPanggil.ANGKAANTREAN, Microsoft.VisualBasic.Left(dsAntrianBelumPanggil.NOMORANTREAN, 3), False)

                            If dsAntrianBelumPanggil.ISPANGGIL = 0 Then
                                oAntrian.UpdateDataIsCheked(dsAntrianBelumPanggil.KODEBOOKING, 1, "")
                            End If

                            oAntrian.UpdateDataIsCheked(dsAntrianBelumPanggil.KODEBOOKING, 2, "PANGGIL")
                            frmErmList.fn_TaskID(dsAntrianBelumPanggil.KODEBOOKING, 2)

                            picAddBooking_Click()
                        Else
                            MsgBox("Pasien Onsite Tidak bisa panggil", MsgBoxStyle.Exclamation, Me.Text)
                        End If

                    Else
                        MsgBox("Nomor Antrian Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Catch oErr As Exception
                    MsgBox("Panggil: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        End Try
    End Sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        If txtKode.Text <> "" Then

        Else
            MsgBox("Masukkan Kode", MsgBoxStyle.Exclamation, Me.Text)
            txtKode.Focus()
            Exit Sub
        End If
        Try
            fn_SaveSetPanggil(CInt(txtNomor.Text), txtKode.Text, False)
        Catch ex As Exception
            MsgBox("Masukkan Nomor Harus angka", MsgBoxStyle.Exclamation, Me.Text)
            txtNomor.Focus()
            Exit Sub
        End Try
    End Sub
    Public Sub fn_SaveBrigging()
        Try
            Dim oConnMySql As New MySql.Data.MySqlClient.MySqlConnection
            Dim oCommMySql As New MySql.Data.MySqlClient.MySqlCommand
            Dim daMySql As MySql.Data.MySqlClient.MySqlDataAdapter
            Dim dsMySql As New DataSet
            Dim MYSQL As String

            dsMySql = New DataSet

            oConnMySql = New MySqlConnection(sConnMySqlAntrianOnline)

            If oConnMySql.State = ConnectionState.Closed Then
                oConnMySql.Open()
            End If

            MYSQL = "select * from booking_registrasi where tanggal_periksa BETWEEN '" & Now.ToString("yyyy-MM-dd") & "' AND '" & Now.ToString("yyyy-MM-dd") & "'  and status = 'Belum' ORDER BY no_reg "

            oCommMySql.Connection = oConnMySql
            oCommMySql.CommandText = MYSQL
            oCommMySql.CommandTimeout = 120
            oCommMySql.CommandType = CommandType.Text

            daMySql = New MySql.Data.MySqlClient.MySqlDataAdapter(oCommMySql)
            daMySql.Fill(dsMySql, "booking_registrasi")

            If oConnMySql.State = ConnectionState.Open Then
                oConnMySql.Close()
            End If

            Dim oDokter As New Reference.clsDoctor
            Dim oDepartment As New Reference.clsDepartment
            Dim oSet_Antrian_Simpan As New SettingAntrian.clsSetAntrian

            For iLoop As Integer = 0 To dsMySql.Tables("booking_registrasi").Rows.Count - 1
                With dsMySql.Tables("booking_registrasi")
                    Dim rekammedis As String = .Rows(iLoop)("no_rkm_medis")
                    Dim tanggalperiksa As DateTime = CDate(.Rows(iLoop)("tanggal_periksa"))
                    Dim dsDokter = oDokter.GetDataByKodeVclaim(.Rows(iLoop)("kd_dokter"))
                    If dsDokter IsNot Nothing Then
                        Dim dsdepartment = oDepartment.GetDataByKodeVclaim(.Rows(iLoop)("kd_poli"))
                        If dsdepartment IsNot Nothing Then
                            ' ***** HEADER *****
                            Dim ds = oSet_Antrian_Simpan.GetStructureHeader
                            With ds
                                .DATECREATED = Now
                                .DATEUPDATED = Now
                                .KDPENJAMIN = "PENJAMIN_0000000001"
                                .KODEBOOKING = ""
                                .JENISPASIEN_RS = "C"
                                .JENISPASIEN = "NON JKN"
                                .NOMORKARTU = ""
                                .NOHP = ""
                                .NIK = ""
                                '.KODEPOLI = dsdepartment.VCLAIM_KODEPOLI
                                '.NAMAPOLI = dsdepartment.NAME_DISPLAY
                                .KODEPOLI = dsdepartment.VCLAIM_KODEPOLI
                                .NAMAPOLI = dsdepartment.NAME_DISPLAY
                                .PASIENBARU = 0
                                .NORM = rekammedis
                                .TANGGALPERIKSA = tanggalperiksa
                                .TANGGALPERIKSA_TEXT = tanggalperiksa.ToString("ddMMyyyy")
                                .KODEDOKTER = dsDokter.VCLAIM_KDDPJP
                                .NAMADOKTER = dsDokter.NAME_DISPLAY
                                .JAMPRAKTEK = ""
                                .JENISKUNJUNGAN = 10
                                .NOMORREFERENSI = ""
                                .NOMORANTREAN = ""
                                .ANGKAANTREAN = 0
                                .ESTIMASIDILAYANI = tanggalperiksa.ToString("yyyy-MM-dd") & "00:00"
                                .SISAKUOTAJKN = 0
                                .KUOTAJKN = 0
                                .SISAKUOTANONJKN = 0
                                .SISAKUOTANONJKN = 0
                                .ISPANGGIL = 0
                                .ISONLINE = False
                                .KETERANGAN = "OFLINE"
                                .KDSKD = ""
                            End With

                            Dim sKDBOOKINGANTREAN As String = oSet_Antrian_Simpan.InsertData(ds)

                            UpdateDaftar(rekammedis, tanggalperiksa.ToString("yyyy-MM-dd"))

                            Dim dsSisaCek = oSet_Antrian_Simpan.GetDataSisaByKode(ds.JENISPASIEN_RS)

                            If dsSisaCek Is Nothing Then
                                Dim dsSisa = oSet_Antrian_Simpan.GetStructureHeaderSisa
                                With dsSisa
                                    .KODE = ds.JENISPASIEN_RS
                                    .SISA = 1
                                    .DESCRIPTION = sKDBOOKINGANTREAN
                                    sBelumPanggil = .SISA
                                End With

                                oSet_Antrian_Simpan.InsertDataSisa(dsSisa)

                            Else
                                Dim dsSisa = oSet_Antrian_Simpan.GetStructureHeaderSisa
                                With dsSisa
                                    .KODE = ds.JENISPASIEN_RS
                                    .SISA = oSet_Antrian_Simpan.GetDataBelumPanggil(Now, ds.JENISPASIEN_RS)
                                    .DESCRIPTION = sKDBOOKINGANTREAN
                                    sBelumPanggil = .SISA
                                End With

                                oSet_Antrian_Simpan.UpdateDataSisa(dsSisa)

                            End If
                        Else
                            MsgBox("poli Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
                        End If
                    End If
                End With
            Next

        Catch oErr As Exception
            MsgBox("Save booking registrasi Online Umum Gagal" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub UpdateDaftar(ByVal kdcustomer As String, ByVal tanggalperiksa As String)
        Try
            Dim oConnMySql As New MySql.Data.MySqlClient.MySqlConnection
            Dim oCommMySql As New MySql.Data.MySqlClient.MySqlCommand
            Dim daMySql As MySql.Data.MySqlClient.MySqlDataAdapter
            Dim dsMySql As New DataSet
            Dim MYSQL As String

            dsMySql = New DataSet

            oConnMySql = New MySqlConnection(sConnMySqlAntrianOnline)

            If oConnMySql.State = ConnectionState.Closed Then
                oConnMySql.Open()
            End If

            MYSQL = "update booking_registrasi set status = 'Terdaftar' where no_rkm_medis = '" & kdcustomer & "' AND tanggal_periksa = '" & tanggalperiksa & "' "

            oCommMySql.Connection = oConnMySql
            oCommMySql.CommandText = MYSQL
            oCommMySql.CommandTimeout = 120
            oCommMySql.CommandType = CommandType.Text

            daMySql = New MySql.Data.MySqlClient.MySqlDataAdapter(oCommMySql)
            daMySql.Fill(dsMySql, "updatebooking_registrasi")

            If oConnMySql.State = ConnectionState.Open Then
                oConnMySql.Close()
            End If

        Catch oErr As Exception
            MsgBox("Save Update Status Booking Umum Gagal" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picUlang_Click() Handles picUlang.Click
        If txtKODEBOOKING.Text <> "-" Then
            Dim oAntrian As New SettingAntrian.clsSetAntrian
            Dim dsAntrianBelumPanggil = oAntrian.GetData(txtKODEBOOKING.Text)
            If dsAntrianBelumPanggil IsNot Nothing Then
                txtKODEBOOKING.Text = dsAntrianBelumPanggil.KODEBOOKING
                'MsgBox("Nomor Antrian ditemukan " & dsAntrianBelumPanggil.KODEBOOKING, MsgBoxStyle.Exclamation, Me.Text)
                fn_SaveSetPanggil(Microsoft.VisualBasic.Right(dsAntrianBelumPanggil.KODEBOOKING, 3), dsAntrianBelumPanggil.JENISPASIEN_RS, True)
                picAddBooking_Click()
            Else
                MsgBox("Nomor Antrian Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        End If
    End Sub
    Private Sub picOperasi_Click() Handles picOperasi.Click
        If grvHistoryPasien.GetFocusedRowCellValue("KDKUNJUNGAN") Is Nothing Then
            Exit Sub
        End If

        Dim oBooking As New WebService.clsSET_BOOKING_JADWALOPERASI
        Dim dsBooking = oBooking.GetDataByKD(grvHistoryPasien.GetFocusedRowCellValue("KDKUNJUNGAN"))

        If dsBooking IsNot Nothing Then
            Dim frmSET_BOOKING_JADWALOPERASI As New frmSET_BOOKING_JADWALOPERASI
            Try
                frmSET_BOOKING_JADWALOPERASI.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsBooking.KDKUNJUNGAN, dsBooking.KDBOOKINGJADWALOPERASI)
                frmSET_BOOKING_JADWALOPERASI.ShowDialog(Me)
                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSET_BOOKING_JADWALOPERASI Is Nothing Then frmSET_BOOKING_JADWALOPERASI.Dispose()
                frmSET_BOOKING_JADWALOPERASI = Nothing
            End Try
        Else
            Dim frmSET_BOOKING_JADWALOPERASI As New frmSET_BOOKING_JADWALOPERASI
            Try
                frmSET_BOOKING_JADWALOPERASI.LoadMe(FORM_MODE.FORM_MODE_ADD, grvHistoryPasien.GetFocusedRowCellValue("KDKUNJUNGAN"))
                frmSET_BOOKING_JADWALOPERASI.ShowDialog(Me)

                If sStatusSave <> "NEW" Then
                    fn_LoadSecurity()
                End If

            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSET_BOOKING_JADWALOPERASI Is Nothing Then frmSET_BOOKING_JADWALOPERASI.Dispose()
                frmSET_BOOKING_JADWALOPERASI = Nothing
            End Try
        End If

    End Sub
    Private Sub grv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grv.DoubleClick
        If grv.GetFocusedRowCellValue("KDPENDAFTARAN") Is Nothing Then
            Exit Sub
        End If

        Dim frmPENDAFTARAN As New frmPendaftaran
        Try
            frmPENDAFTARAN.LoadMe(FORM_MODE.FORM_MODE_VIEW, "", grv.GetFocusedRowCellValue("KDPENDAFTARAN"))
            frmPENDAFTARAN.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAddBooking_Click()
        Dim frmPENDAFTARAN As New frmPendaftaran
        Try
            frmPENDAFTARAN.LoadMe(FORM_MODE.FORM_MODE_ADD, txtKODEBOOKING.Text)
            frmPENDAFTARAN.ShowDialog(Me)

            If sStatusSave <> "NEW" Then
                fn_LoadSecurity()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmPENDAFTARAN Is Nothing Then frmPENDAFTARAN.Dispose()
            frmPENDAFTARAN = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDPENDAFTARAN"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAddBooking_Click()
            End If
        End Try
    End Sub
    Private Sub picAdd_Click() Handles picAdd.Click
        Dim frmPENDAFTARAN As New frmPendaftaran
        Try
            frmPENDAFTARAN.LoadMe(FORM_MODE.FORM_MODE_ADD, "")
            frmPENDAFTARAN.ShowDialog(Me)

            If sStatusSave <> "NEW" Then
                fn_LoadSecurity()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmPENDAFTARAN Is Nothing Then frmPENDAFTARAN.Dispose()
            frmPENDAFTARAN = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDPENDAFTARAN"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KDPENDAFTARAN") Is Nothing Then
            Exit Sub
        End If
        Dim frmPENDAFTARAN As New frmPendaftaran
        Try
            frmPENDAFTARAN.LoadMe(FORM_MODE.FORM_MODE_EDIT, "", grv.GetFocusedRowCellValue("KDPENDAFTARAN"))
            frmPENDAFTARAN.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmPENDAFTARAN Is Nothing Then frmPENDAFTARAN.Dispose()
            frmPENDAFTARAN = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDPENDAFTARAN"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KDPENDAFTARAN") Is Nothing Then
            Exit Sub
        End If

        Dim Kode As String = grv.GetFocusedRowCellValue("KDPENDAFTARAN")
        Dim nosep = grv.GetFocusedRowCellValue("NoSEP")

        frmLoginDelete.ShowDialog()

        'If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        Dim dsKunjungan = oPendaftaran.GetDataKunjunganByPendaftaran(Kode)
        Dim sNoIdKunjungan As String

        If dsKunjungan IsNot Nothing Then
            sNoIdKunjungan = dsKunjungan.KDKUNJUNGAN
        Else
            sNoIdKunjungan = ""
        End If

        Dim dsSKD = oPendaftaran.GetDataKunjunganBySKD(Kode)
        Dim sNoIdSKD As String

        If dsSKD IsNot Nothing Then
            sNoIdSKD = dsSKD.KDSKD
        Else
            sNoIdSKD = ""
        End If

        Dim frmPesanDelete As New frmPesanDelete
        frmPesanDelete.ShowDialog(Me)

        If sPesanHapus <> "XXXXXBATALXXXXX" Then
            Dim oDelete As New Setting.clsDelete

            If oDelete.InsertData("PENDAFTARAN", sUserID, sPesanHapus & " : " & Kode & " " & sNoIdKunjungan & " " & sNoIdSKD & " " & nosep, Kode) = False Then
                MsgBox("Gagal Simpan ke tabel delete", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If
        Else
            MsgBox("Alasan Kosong", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If

        If nosep = "" Then
            If fn_DeleteData(Kode, sNoIdKunjungan, sNoIdSKD) = False Then
                MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
            fn_LoadSecurity()
        ElseIf nosep = "<--- AUTO --->"
            If fn_DeleteData(Kode, sNoIdKunjungan, sNoIdSKD) = False Then
                MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
            fn_LoadSecurity()
        Else
            If fn_DeleteSEP() = True Then
                If fn_CariSEP(nosep) = "KOSONG" Then
                    If fn_DeleteData(Kode, sNoIdKunjungan, sNoIdSKD) = False Then
                        MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                    End If
                    MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
                    fn_LoadSecurity()
                Else
                    MsgBox("Nomor SEP di BPJS tidak ditemukan gagal hapus di aplikasi", MsgBoxStyle.Information, Me.Text)
                    Exit Sub
                End If
            End If
        End If
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        Try
            If grv.GetFocusedRowCellValue("KDPENDAFTARAN") = String.Empty Then Exit Sub
            If MsgBox("apakah akan cetak ulang Sep / Registrasi ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

            Dim ds = oPendaftaran.GetData(grv.GetFocusedRowCellValue("KDPENDAFTARAN"))

            sCetakSEP = False

            If ds.NOMORSEP <> "" Then
                If fn_CariSEP(ds.NOMORSEP) = "KOSONG" Then
                    MsgBox("SEP Tidak di Temukan dengan Nomor SEP " & ds.NOMORSEP, MsgBoxStyle.Information, Me.Text)
                    Exit Sub
                End If
                If ds.M_DEPARTMENT.OTHER = "ANTRIAN" Then
                    Dim a, b, c As String
                    a = Year(ds.DATE)
                    b = Year(ds.M_CUSTOMER.TANGGALLAHIR)
                    c = a - b
                    sUmur = c & " tahun"

                    Dim rpt As New xtraSEPNomorAntrian
                    rpt.bindingSource.DataSource = ds
                    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                    printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
                Else
                    Dim a, b, c As String
                    a = Year(ds.DATE)
                    b = Year(ds.M_CUSTOMER.TANGGALLAHIR)
                    c = a - b
                    sUmur = c & " tahun"

                    Dim rpt As New xtraSEPNomorAntrian
                    rpt.bindingSource.DataSource = ds
                    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                    printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
                End If
                'Dim rpt As New xtraSEP
                'rpt.bindingSource.DataSource = ds
            Else
                If ds.M_DAFTAR_L1.MEMO <> "BPJS" Then
                    Dim rpt As New xtraUmum
                    Dim a, b, c As String
                    a = Year(ds.DATE)
                    b = Year(ds.M_CUSTOMER.TANGGALLAHIR)
                    c = a - b

                    sUmur = c & " tahun"
                    rpt.bindingSource.DataSource = ds
                    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                    printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)

                End If
            End If

            If sCetakSEP = True Then
                oPendaftaran.UpdateCetak(ds.KDPENDAFTARAN)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picBatal_Click() Handles picBatal.Click
        Try
            If grv.GetFocusedRowCellValue("KDPENDAFTARAN") = String.Empty Then Exit Sub

            If oPendaftaran.UpdateDataBatal(grv.GetFocusedRowCellValue("KDPENDAFTARAN"), sUserID) = True Then
                MsgBox("Status Daftar diperbaharui", MsgBoxStyle.Information, Me.Text)
            End If

            fn_LoadSecurity()

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_LoadSecurity()
    End Sub
    Private Sub MutasiPasienToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MutasiPasienToolStripMenuItem.Click
        If grvHistoryPasien.GetFocusedRowCellValue("NomorPendaftaran") Is Nothing Then
            Exit Sub
        End If

        Dim frmPendaftaran_Kunjungan As New frmPendaftaran_Kunjungan
        Try
            frmPendaftaran_Kunjungan.LoadMe(FORM_MODE.FORM_MODE_ADD, grvHistoryPasien.GetFocusedRowCellValue("NomorPendaftaran"))
            frmPendaftaran_Kunjungan.ShowDialog(Me)

            fn_LoadHistoryPasien(oPendaftaran.GetData(grv.GetFocusedRowCellValue("KDPENDAFTARAN")).KDCUSTOMER)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)

        End Try
    End Sub
    Private Sub EditMutasiPasienToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditMutasiPasienToolStripMenuItem.Click
        If grvHistoryPasien.GetFocusedRowCellValue("KDKUNJUNGAN") Is Nothing Then
            Exit Sub
        End If
        Dim frmPendaftaran_Kunjungan As New frmPendaftaran_Kunjungan
        Try
            frmPendaftaran_Kunjungan.LoadMe(FORM_MODE.FORM_MODE_EDIT, "", grvHistoryPasien.GetFocusedRowCellValue("KDKUNJUNGAN"))
            frmPendaftaran_Kunjungan.ShowDialog(Me)
            fn_LoadHistoryPasien(oPendaftaran.GetData(grv.GetFocusedRowCellValue("KDPENDAFTARAN")).KDCUSTOMER)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub CetakKartuStatusToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakKartuStatusToolStripMenuItem.Click
        Try
            If grvHistoryPasien.GetFocusedRowCellValue("KDKUNJUNGAN") = String.Empty Then Exit Sub
            Dim oKunjungan As New Admission.clsPendaftaran_Kunjungan
            Dim ds = oKunjungan.GetData(grvHistoryPasien.GetFocusedRowCellValue("KDKUNJUNGAN"))

            Dim listTranskasi As New List(Of R_KARTU_STATUS)

            Dim dsRekap As New DataAccess.R_KARTU_STATUS

            With ds
                sJenisDaftar = ds.S_PENDAFTARAN_H.M_DAFTAR_L1.MEMO

                dsRekap.KDCUSTOMER = ds.S_PENDAFTARAN_H.KDCUSTOMER
                dsRekap.NIK = ds.S_PENDAFTARAN_H.KTP
                dsRekap.NAMAUNIT = ds.M_DEPARTMENT.NAME_DISPLAY
                dsRekap.TANGGAL_MASUK = ds.DATE
                dsRekap.PASIEN = ds.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY
                dsRekap.PANGKAT = ds.M_PANGKAT.MEMO
                dsRekap.NRP = ds.S_PENDAFTARAN_H.M_CUSTOMER.NRP
                Try
                    dsRekap.NAMAKELUARGA = ds.S_PENDAFTARAN_H.S_PENDAFTARAN_PENANGGUNGJAWAB.NAMA
                Catch ex As Exception
                    dsRekap.NAMAKELUARGA = ""
                End Try

                dsRekap.TEMPAT = ds.S_PENDAFTARAN_H.M_CUSTOMER.TEMPATLAHIR
                dsRekap.TANGGAL_LAHIR = ds.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR

                Dim a, b, c As String
                a = Year(ds.S_PENDAFTARAN_H.DATE)
                b = Year(ds.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)
                c = a - b
                dsRekap.UMUR = c & " tahun"
                dsRekap.KELAMIN = IIf(ds.S_PENDAFTARAN_H.M_CUSTOMER.KDJENISKELAMIN = 0, "Perempuan", "Laki-laki")
                dsRekap.DARAH = IIf(ds.S_PENDAFTARAN_H.M_CUSTOMER.KDGOLONGANDARAH = 0, "A", IIf(ds.S_PENDAFTARAN_H.M_CUSTOMER.KDGOLONGANDARAH = 1, "B", IIf(ds.S_PENDAFTARAN_H.M_CUSTOMER.KDGOLONGANDARAH = 2, "AB", IIf(ds.S_PENDAFTARAN_H.M_CUSTOMER.KDGOLONGANDARAH = 3, "O", "-"))))
                dsRekap.STATUS = IIf(ds.S_PENDAFTARAN_H.M_CUSTOMER.KDSTATUSKAWIN = 0, "BELUM MENIKAH", IIf(ds.S_PENDAFTARAN_H.M_CUSTOMER.KDSTATUSKAWIN = 1, "MENIKAH", IIf(ds.S_PENDAFTARAN_H.M_CUSTOMER.KDSTATUSKAWIN = 2, "JANDA", "DUDA")))
                dsRekap.AGAMA = ds.S_PENDAFTARAN_H.M_CUSTOMER.M_AGAMA.MEMO
                dsRekap.PEKERJAAN = ds.S_PENDAFTARAN_H.M_CUSTOMER.M_PEKERJAAN.MEMO
                dsRekap.PENDIDIKAN = ds.S_PENDAFTARAN_H.M_CUSTOMER.M_PENDIDIKAN.MEMO
                dsRekap.ALAMAT = ds.ALAMAT
                dsRekap.KESATUAN = ds.S_PENDAFTARAN_H.M_CUSTOMER.M_KESATUAN.MEMO
                dsRekap.TELEPON = ds.S_PENDAFTARAN_H.M_CUSTOMER.PHONE
                dsRekap.KELOMPOKPASIEN = ds.S_PENDAFTARAN_H.M_DAFTAR_L2.MEMO
                dsRekap.PEMEGANGASURANSI = ds.S_PENDAFTARAN_H.M_CUSTOMER.NAMAKELUARGA
                dsRekap.RUJUKAN = ds.S_PENDAFTARAN_H.M_PPK.MEMO
                dsRekap.ALAMAT_PERUBAHAN = ""
                dsRekap.DOKTER = ds.M_DOCTOR.NAME_DISPLAY
                dsRekap.NOASURANSI = ds.S_PENDAFTARAN_H.KARTUBPJS

                listTranskasi.Add(dsRekap)
            End With



            Dim rpt As New xtraKartuStatus
            rpt.bindingSource.DataSource = listTranskasi
            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
            printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub CetakKartuStatusIGDToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakKartuStatusIGDToolStripMenuItem.Click
        Try
            If grvHistoryPasien.GetFocusedRowCellValue("KDKUNJUNGAN") = String.Empty Then Exit Sub
            Dim oKunjungan As New Admission.clsPendaftaran_Kunjungan
            Dim ds = oKunjungan.GetData(grvHistoryPasien.GetFocusedRowCellValue("KDKUNJUNGAN"))

            Dim listTranskasi As New List(Of R_KARTU_STATUS)

            Dim dsRekap As New DataAccess.R_KARTU_STATUS

            With ds
                sJenisDaftar = ds.S_PENDAFTARAN_H.M_DAFTAR_L1.MEMO & " (" & ds.S_PENDAFTARAN_H.M_KELASRAWAT.KELOMPOKKELAS & ")"
                dsRekap.KDCUSTOMER = ds.S_PENDAFTARAN_H.KDCUSTOMER
                dsRekap.NIK = ds.S_PENDAFTARAN_H.KTP
                dsRekap.NAMAUNIT = ds.M_DEPARTMENT.NAME_DISPLAY
                dsRekap.TANGGAL_MASUK = ds.DATE
                dsRekap.PASIEN = ds.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY
                dsRekap.PANGKAT = ds.M_PANGKAT.MEMO
                dsRekap.NRP = ds.S_PENDAFTARAN_H.M_CUSTOMER.NRP
                Try
                    dsRekap.NAMAKELUARGA = ds.S_PENDAFTARAN_H.S_PENDAFTARAN_PENANGGUNGJAWAB.NAMA
                Catch ex As Exception
                    dsRekap.NAMAKELUARGA = ""
                End Try

                dsRekap.TEMPAT = ds.S_PENDAFTARAN_H.M_CUSTOMER.TEMPATLAHIR
                dsRekap.TANGGAL_LAHIR = ds.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR

                Dim a, b, c As String
                a = Year(ds.S_PENDAFTARAN_H.DATE)
                b = Year(ds.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)
                c = a - b
                dsRekap.UMUR = c & " tahun"
                dsRekap.KELAMIN = IIf(ds.S_PENDAFTARAN_H.M_CUSTOMER.KDJENISKELAMIN = 0, "Perempuan", "Laki-laki")
                dsRekap.DARAH = IIf(ds.S_PENDAFTARAN_H.M_CUSTOMER.KDGOLONGANDARAH = 0, "A", IIf(ds.S_PENDAFTARAN_H.M_CUSTOMER.KDGOLONGANDARAH = 1, "B", IIf(ds.S_PENDAFTARAN_H.M_CUSTOMER.KDGOLONGANDARAH = 2, "AB", IIf(ds.S_PENDAFTARAN_H.M_CUSTOMER.KDGOLONGANDARAH = 3, "O", "-"))))
                dsRekap.STATUS = IIf(ds.S_PENDAFTARAN_H.M_CUSTOMER.KDSTATUSKAWIN = 0, "BELUM MENIKAH", IIf(ds.S_PENDAFTARAN_H.M_CUSTOMER.KDSTATUSKAWIN = 1, "MENIKAH", IIf(ds.S_PENDAFTARAN_H.M_CUSTOMER.KDSTATUSKAWIN = 2, "JANDA", "DUDA")))
                dsRekap.AGAMA = ds.S_PENDAFTARAN_H.M_CUSTOMER.M_AGAMA.MEMO
                dsRekap.PEKERJAAN = ds.S_PENDAFTARAN_H.M_CUSTOMER.M_PEKERJAAN.MEMO
                dsRekap.PENDIDIKAN = ds.S_PENDAFTARAN_H.M_CUSTOMER.M_PENDIDIKAN.MEMO
                dsRekap.ALAMAT = ds.ALAMAT
                dsRekap.KESATUAN = ds.S_PENDAFTARAN_H.M_CUSTOMER.M_KESATUAN.MEMO
                dsRekap.TELEPON = ds.S_PENDAFTARAN_H.M_CUSTOMER.PHONE
                dsRekap.KELOMPOKPASIEN = ds.S_PENDAFTARAN_H.M_DAFTAR_L2.MEMO
                dsRekap.PEMEGANGASURANSI = ds.S_PENDAFTARAN_H.M_CUSTOMER.NAMAKELUARGA
                dsRekap.RUJUKAN = ds.S_PENDAFTARAN_H.M_PPK.MEMO
                dsRekap.ALAMAT_PERUBAHAN = ""
                dsRekap.DOKTER = ds.M_DOCTOR.NAME_DISPLAY
                dsRekap.NOASURANSI = ds.S_PENDAFTARAN_H.KARTUBPJS

                listTranskasi.Add(dsRekap)
            End With

            Dim rpt As New xtraKartuStatusIgd
            rpt.bindingSource.DataSource = listTranskasi
            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
            printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub CetakListToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakListToolStripMenuItem.Click
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
{"", "LAPORAN PENDAFTARAN" & vbCrLf & deDATEFrom.DateTime.ToString("dd/MM/yyyy") & " - " & deDATETo.DateTime.ToString("dd/MM/yyyy"), ""})

            PrintableComponentLink.Component = grd
            PrintableComponentLink.CreateDocument()
            PrintableComponentLink.ShowPreviewDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub CetakKartuBerobatToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakKartuBerobatToolStripMenuItem.Click
        Try
            If grv.GetFocusedRowCellValue("KDCUSTOMER") = String.Empty Then Exit Sub

            Dim rpt As New xtraKartuBerobatKosong
            Dim oCustomer As New Reference.clsCustomer

            rpt.ShowPrintMarginsWarning = False
            rpt.Watermark.Text = sWATERMARK
            Dim ds = oCustomer.GetData(grv.GetFocusedRowCellValue("KDCUSTOMER"))
            rpt.bindingSource.DataSource = ds
            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
            printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub CetakLabelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakLabelToolStripMenuItem.Click
        Try
            If grv.GetFocusedRowCellValue("KDPENDAFTARAN") = String.Empty Then Exit Sub

            Dim rpt As New xtraLabel

            rpt.ShowPrintMarginsWarning = False
            rpt.Watermark.Text = sWATERMARK
            Dim ds = oPendaftaran.GetData(grv.GetFocusedRowCellValue("KDPENDAFTARAN"))
            rpt.bindingSource.DataSource = ds
            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
            printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub cboLoket_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboLoket.SelectedIndexChanged
        'If cboLoket.Text = "4" Then
        '    cboType.Text = "O"
        '    cboType.Properties.ReadOnly = True
        '    cboLoket.Properties.ReadOnly = False
        'Else
        '    cboType.Properties.ReadOnly = False
        '    cboType.Text = "A"
        'End If
    End Sub
    Private Sub cboType_SelectedIndexChanged(sender As Object, e As EventArgs)
        'If cboType.Text = "O" Then
        '    cboLoket.Text = "4"
        '    cboLoket.Properties.ReadOnly = True
        '    cboType.Properties.ReadOnly = False
        'Else
        '    cboLoket.Properties.ReadOnly = False
        '    cboLoket.Text = "1"
        'End If
    End Sub
    Private Sub KartuIdentitasPasienToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KartuIdentitasPasienToolStripMenuItem.Click
        Try
            If grv.GetFocusedRowCellValue("KDPENDAFTARAN") = String.Empty Then Exit Sub

            Dim rpt As New xtraKartuIdentitasPasien
            Dim oCustomer As New Reference.clsCustomer

            rpt.ShowPrintMarginsWarning = False
            rpt.Watermark.Text = sWATERMARK
            Dim ds = oPendaftaran.GetData(grv.GetFocusedRowCellValue("KDPENDAFTARAN"))
            rpt.bindingSource.DataSource = ds
            sUMURPASIEN = oPendaftaran.GetUmurPasien(ds.DATE, ds.M_CUSTOMER.TANGGALLAHIR)
            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
            printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub CetakGelangPasienToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakGelangPasienToolStripMenuItem.Click
        Try
            If grv.GetFocusedRowCellValue("KDPENDAFTARAN") = String.Empty Then Exit Sub

            Dim rpt As New xtraGelangPink
            Dim oCustomer As New Reference.clsCustomer

            rpt.ShowPrintMarginsWarning = False
            rpt.Watermark.Text = sWATERMARK
            Dim ds = oPendaftaran.GetData(grv.GetFocusedRowCellValue("KDPENDAFTARAN"))
            rpt.BindingSource.DataSource = ds
            sUMURPASIEN = oPendaftaran.GetUmurPasien(ds.DATE, ds.M_CUSTOMER.TANGGALLAHIR)
            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
            printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub CetakRegistrasiLamaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakRegistrasiLamaToolStripMenuItem.Click
        Try
            If grv.GetFocusedRowCellValue("KDPENDAFTARAN") = String.Empty Then Exit Sub
            Dim ds = oPendaftaran.GetData(grv.GetFocusedRowCellValue("KDPENDAFTARAN"))

            sCetakSEP = False

            If ds.NOMORSEP <> "" Then
                If ds.M_DEPARTMENT.OTHER = "ANTRIAN" Then
                    Dim a, b, c As String
                    a = Year(ds.DATE)
                    b = Year(ds.M_CUSTOMER.TANGGALLAHIR)
                    c = a - b
                    sUmur = c & " tahun"

                    Dim rpt As New xtraSEPNomorAntrian
                    rpt.bindingSource.DataSource = ds
                    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                    printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
                Else
                    Dim a, b, c As String
                    a = Year(ds.DATE)
                    b = Year(ds.M_CUSTOMER.TANGGALLAHIR)
                    c = a - b
                    sUmur = c & " tahun"

                    Dim rpt As New xtraSEP
                    rpt.bindingSource.DataSource = ds
                    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                    printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
                End If
                'Dim rpt As New xtraSEP
                'rpt.bindingSource.DataSource = ds
            Else
                If ds.M_DAFTAR_L1.MEMO <> "BPJS" Then
                    Dim rpt As New xtraUmum
                    Dim a, b, c As String
                    a = Year(ds.DATE)
                    b = Year(ds.M_CUSTOMER.TANGGALLAHIR)
                    c = a - b

                    sUmur = c & " tahun"
                    rpt.bindingSource.DataSource = ds
                    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                    printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
                Else
                    MsgBox("Nomor SEP Kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If

            If sCetakSEP = True Then
                oPendaftaran.UpdateCetak(ds.KDPENDAFTARAN)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub DeleteDirectory(path As String)
        If IO.Directory.Exists(path) Then
            If IO.Directory.Exists(path) Then
                'Delete all files from the Directory
                For Each filepath As String In IO.Directory.GetFiles(path)
                    IO.File.Delete(filepath)
                Next
                'Delete all child Directories
                For Each dir As String In IO.Directory.GetDirectories(path)
                    DeleteDirectory(dir)
                Next
                'Delete a Directory
                IO.Directory.Delete(path)
            End If

        End If
    End Sub
    Private Sub CetakRegistrasiBaruToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakRegistrasiBaruToolStripMenuItem.Click
        Try
            If grv.GetFocusedRowCellValue("KDPENDAFTARAN") = String.Empty Then Exit Sub
            Dim ds = oPendaftaran.GetData(grv.GetFocusedRowCellValue("KDPENDAFTARAN"))

            sCetakSEP = False

            'Dim a, b, c As String
            'a = Year(ds.DATE)
            'b = Year(ds.M_CUSTOMER.TANGGALLAHIR)
            'c = a - b
            'sUmur = c & " tahun"

            sUmur = oPendaftaran.GetUmurPasien(ds.DATE, ds.M_CUSTOMER.TANGGALLAHIR)

            Dim rpt As New xtraAntrianPendaftaran_88_Kecil
            rpt.BindingSource.DataSource = ds
            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
            printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)

            If sCetakSEP = True Then
                oPendaftaran.UpdateCetak(ds.KDPENDAFTARAN)
            End If

            If ds.NOMORSEP <> "" Then
                Dim FolderSimpan = "C:/Source/CetakSEP/"

                If Not IO.Directory.Exists(FolderSimpan) Then
                    IO.Directory.CreateDirectory(FolderSimpan)
                Else
                    DeleteDirectory(FolderSimpan)
                    IO.Directory.CreateDirectory(FolderSimpan)
                End If

                Dim rpt1 As New xtraSEPNomorAntrian
                rpt1.bindingSource.DataSource = ds
                Dim printTool1 As New DevExpress.XtraReports.UI.ReportPrintTool(rpt1)

                Dim Alamat As String = "C:/Source/CetakSEP/" & ds.KDPENDAFTARAN & Now.ToString("ddMMyyy HHmmss") & ".pdf"
                rpt1.ExportToPdf(Alamat)

                If FileIO.FileSystem.FileExists(Alamat) Then
                    Dim frmPopPDF As New frmPopPDF
                    Try
                        frmPopPDF.fn_LoadMe(Alamat)
                        frmPopPDF.ShowDialog(Me)
                    Catch oErr As Exception
                        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    Finally
                        If Not frmPopPDF Is Nothing Then frmPopPDF.Dispose()
                        frmPopPDF = Nothing
                    End Try
                End If
            Else
                MsgBox("Nomor SEP Kosong", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDDEPARTMENT_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdKDDEPARTMENT.KeyPress
        If Asc(e.KeyChar) = 13 Then
            grdKDDEPARTMENT.ResetText()
        End If
    End Sub
    Private Sub fn_LoadDepartment()
        Dim oDepartment As New Reference.clsDepartment
        Try
            grdKDDEPARTMENT.Properties.DataSource = oDepartment.GetData.Where(Function(x) x.NOMOR > 0 And x.ISACTIVE = True And x.ANTRIAN <> "" And x.VCLAIM_KODEPOLI <> "").ToList()
            grdKDDEPARTMENT.Properties.ValueMember = "KDDEPARTMENT"
            grdKDDEPARTMENT.Properties.DisplayMember = "ANTRIAN"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub chkBukaAntrianSisa_CheckedChanged(sender As Object, e As EventArgs) Handles chkBukaAntrianSisa.CheckedChanged
        If chkBukaAntrianSisa.Checked = False Then
            lANTRIAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            lANTRIAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            fn_LoadSisa()
        End If
    End Sub
    Private Sub CetakGelangPasien2ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakGelangPasien2ToolStripMenuItem.Click
        Try
            If grv.GetFocusedRowCellValue("KDPENDAFTARAN") = String.Empty Then Exit Sub

            Dim rpt As New xtraGelangCetak
            Dim oCustomer As New Reference.clsCustomer

            rpt.ShowPrintMarginsWarning = False
            rpt.Watermark.Text = sWATERMARK
            Dim ds = oPendaftaran.GetData(grv.GetFocusedRowCellValue("KDPENDAFTARAN"))
            rpt.BindingSource.DataSource = ds
            sUMURPASIEN = oPendaftaran.GetUmurPasien(ds.DATE, ds.M_CUSTOMER.TANGGALLAHIR)
            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
            printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub CetakKartuBerobat2ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakKartuBerobat2ToolStripMenuItem.Click
        Try
            If grv.GetFocusedRowCellValue("KDPENDAFTARAN") = String.Empty Then Exit Sub

            Dim rpt As New xtraKartuBerobatKosong2

            rpt.ShowPrintMarginsWarning = False
            rpt.Watermark.Text = sWATERMARK
            Dim ds = oPendaftaran.GetData(grv.GetFocusedRowCellValue("KDPENDAFTARAN"))
            rpt.bindingSource.DataSource = ds
            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
            printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub CetakGelangPasien3ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakGelangPasien3ToolStripMenuItem.Click
        Try
            If grv.GetFocusedRowCellValue("KDPENDAFTARAN") = String.Empty Then Exit Sub

            Dim rpt As New xtraGelangCetak2
            Dim oCustomer As New Reference.clsCustomer

            rpt.ShowPrintMarginsWarning = False
            rpt.Watermark.Text = sWATERMARK
            Dim ds = oPendaftaran.GetData(grv.GetFocusedRowCellValue("KDPENDAFTARAN"))
            rpt.BindingSource.DataSource = ds
            sUMURPASIEN = oPendaftaran.GetUmurPasien(ds.DATE, ds.M_CUSTOMER.TANGGALLAHIR)
            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
            printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class