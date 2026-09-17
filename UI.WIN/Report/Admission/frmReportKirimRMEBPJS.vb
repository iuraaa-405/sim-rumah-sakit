Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports DevExpress.XtraPrinting
Imports System.Data.SqlClient
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Text
Imports System.Text.RegularExpressions
Imports DevExpress.XtraSplashScreen
Imports System
Imports System.Globalization

Public Class frmReportKirimRMEBPJS
    Implements ILanguage
    Private oRIdentitasGrouperData As New Grouper.clsR_Identitas_Grouper_Data
    Private oSSSend As New Reference.clsSS_Send
    Private oPendaftaran_BPJSRME As New Reference.clsPendaftaranBPJSRME
    Private oCustomer_BPJSRME As New Reference.clsCustomerBPJSRME
    Private oDoctor_BPJSRME As New Reference.clsDoctorBPJSRME
    Private oDepartment_BPJSRME As New Reference.clsDepartmentBPJSRME
    Private oPendaftaran_TaskId3 As New SatuSehat.clsECounterRawatJalanTaksId3
    Private oAnamnesis As New SatuSehat.clsAnamnesisSatuSehat
    Private oPendaftaran As New Admission.clsPendaftaran
    Private oCustomerSatuSehat As New Reference.clsCustomerSatuSehat
    Private oDoctorSatuSehat As New Reference.clsDoctorSatuSehat
    'Private oDepartmentSatuSehat As New Reference.clsDepartmentSatuSehat
    Private oDepartmentSatuSehatLokasi As New Reference.clsDepartmentLocationSatuSehat
    Private oSet_Antrian As New SettingAntrian.clsSetAntrian
    Private oGrouperDataCppt As New Grouper.clsR_CPPT
    Private oData As New Grouper.clsR_Identitas_Grouper_Data
    Private sTipe As Integer = 0

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "Laporan Rekap"

        lTYPE.Text = Report.FILTER_TYPE
        lDATEFROM.Text = Report.FILTER_DATEFROM
        lDATETO.Text = Report.FILTER_DATETO

        cboTYPE.Properties.Items.Clear()
        cboTYPE.Properties.Items.Add("Rekap BPJS RME")
        cboTYPE.Properties.Items.Add("Rekap Satu Sehat")

        cboTYPE.SelectedIndex = 0

        deDATEFrom.DateTime = Now.AddDays((-Now.Day) + 1)
        deDATETo.DateTime = Now
    End Sub
    Public Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            Me.Text = "Laporan Rekap"

            lTYPE.Text = Report.FILTER_TYPE
            lDATEFROM.Text = Report.FILTER_DATEFROM
            lDATETO.Text = Report.FILTER_DATETO

            cboTYPE.Properties.Items.Clear()
            cboTYPE.Properties.Items.Add("Rekap BPJS RME")
            cboTYPE.Properties.Items.Add("Rekap Satu Sehat")

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
                      Where x.MODUL = "ADMISSION_KIRIMRME" _
                      And y.KDUSER = sUserID
                      Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                picPrint.Enabled = ds.ISPRINT
                picRefresh.Enabled = ds.ISVIEW
                picKirimBPJS.Enabled = ds.ISADD
                picKirimSatuSehat.Enabled = ds.ISADD

                If ds.ISVIEW = True Then
                    fn_Preview()
                    fn_LoadLanguage()
                End If
            Catch oErr As Exception
                MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)

                picPrint.Enabled = False
                picRefresh.Enabled = False
                picKirimBPJS.Enabled = False
                picKirimSatuSehat.Enabled = False
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
{"", "Laporan Rekap" & vbCrLf & deDATEFrom.DateTime.ToString("dd/MM/yyyy") & " - " & deDATETo.DateTime.ToString("dd/MM/yyyy"), ""})

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
            grv1.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            fn_LoadData(sTipe)

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData(ByVal type As Integer)
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

            If type = 0 Then
                SQL = "EXEC BPJS_KIRIMRME  "
                SQL &= "@DARITANGGAL = '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' "
                SQL &= ",@SAMPAITANGGAL = '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
                SQL &= ",@CATEGORY = " & IIf(chkRawat.Checked = False, 0, 1) & " "
            Else
                SQL = "EXEC SS_KIRIM_RAWATJALAN_TAMPIL "
                SQL &= "@DARITANGGAL = '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' "
                SQL &= ",@SAMPAITANGGAL = '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
                SQL &= ",@CATEGORY = " & IIf(chkRawat.Checked = False, 0, 1) & " "
            End If

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "BPJS_KIRIMRME")

            grd.MainView = grv
            grd.DataSource = ds.Tables("BPJS_KIRIMRME")
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
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next
    End Sub
    Private Function fn_SaveBPJSRME_Customer(ByVal KDCUSTOMER As String, ByVal ID As String) As Boolean
        Try
            ' ***** HEADER *****
            Dim isadd As Boolean = False

            Dim ds = oCustomer_BPJSRME.GetStructureHeader
            With ds
                Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()

                Dim dsCek = oCustomer_BPJSRME.GetData(KDCUSTOMER)
                If dsCek Is Nothing Then
                    isadd = True
                    .DATECREATED = WaktuServer
                Else
                    isadd = False
                    .DATECREATED = dsCek.DATECREATED
                End If

                .DATEUPDATED = WaktuServer
                .KDCUSTOMER = KDCUSTOMER
                .ID = ID
                .REQUEST = ""
                .RESPON = ""
                .ISDEFAULT = False
                .ISACTIVE = False
            End With

            If isadd = True Then
                Try
                    fn_SaveBPJSRME_Customer = oCustomer_BPJSRME.InsertData(ds)
                Catch oErr As Exception
                    'MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                Try
                    fn_SaveBPJSRME_Customer = oCustomer_BPJSRME.UpdateData(ds)
                Catch oErr As Exception
                    'MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

        Catch oErr As Exception
            'MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveBPJSRME_Customer = False
        End Try
    End Function
    Private Function fn_SaveBPJSRME_Doctor(ByVal KDDOCTOR As String, ByVal ID As String, ByVal REQUEST As String, ByVal RESPON As String) As Boolean
        Try
            ' ***** HEADER *****
            Dim isadd As Boolean = False

            Dim ds = oDoctor_BPJSRME.GetStructureHeader
            With ds
                Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()

                Dim dsCek = oDoctor_BPJSRME.GetData(KDDOCTOR)
                If dsCek Is Nothing Then
                    isadd = True
                    .DATECREATED = WaktuServer
                Else
                    isadd = False
                    .DATECREATED = dsCek.DATECREATED
                End If

                .DATEUPDATED = WaktuServer
                .KDDOCTOR = KDDOCTOR
                .ID = ID
                .REQUEST = REQUEST
                .RESPON = RESPON
                .ISDEFAULT = False
                .ISACTIVE = False
            End With

            If isadd = True Then
                Try
                    fn_SaveBPJSRME_Doctor = oDoctor_BPJSRME.InsertData(ds)
                Catch oErr As Exception
                    'MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                Try
                    fn_SaveBPJSRME_Doctor = oDoctor_BPJSRME.UpdateData(ds)
                Catch oErr As Exception
                    'MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

        Catch oErr As Exception
            'MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveBPJSRME_Doctor = False
        End Try
    End Function
    Private Function fn_SaveBPJSRME_Department(ByVal KDDEPARTMENT As String, ByVal ID As String, ByVal REQUEST As String, ByVal RESPON As String) As Boolean
        Try
            ' ***** HEADER *****
            Dim isadd As Boolean = False

            Dim ds = oDepartment_BPJSRME.GetStructureHeader
            With ds
                Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()

                Dim dsCek = oDepartment_BPJSRME.GetData(KDDEPARTMENT)
                If dsCek Is Nothing Then
                    isadd = True
                    .DATECREATED = WaktuServer
                Else
                    isadd = False
                    .DATECREATED = dsCek.DATECREATED
                End If

                .DATEUPDATED = WaktuServer
                .KDDEPARTMENT = KDDEPARTMENT
                .ID = ID
                .REQUEST = REQUEST
                .RESPON = RESPON
                .ISDEFAULT = False
                .ISACTIVE = False
            End With

            If isadd = True Then
                Try
                    fn_SaveBPJSRME_Department = oDepartment_BPJSRME.InsertData(ds)
                Catch oErr As Exception
                    'MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                Try
                    fn_SaveBPJSRME_Department = oDepartment_BPJSRME.UpdateData(ds)
                Catch oErr As Exception
                    'MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

        Catch oErr As Exception
            'MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveBPJSRME_Department = False
        End Try
    End Function
    Private Function fn_SaveBPJSRME(ByVal category As String, ByVal KDPENDAFTARAN As String, ByVal REQUEST As String, ByVal RESPON As String) As Boolean
        Try
            ' ***** HEADER *****
            Dim isadd As Boolean = False

            Dim ds = oPendaftaran_BPJSRME.GetStructureHeader
            With ds
                Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()

                Dim dsCek = oPendaftaran_BPJSRME.GetData(KDPENDAFTARAN)
                If dsCek Is Nothing Then
                    isadd = True
                    .DATECREATED = WaktuServer
                Else
                    isadd = False
                    .DATECREATED = dsCek.DATECREATED
                End If

                .DATEUPDATED = WaktuServer
                .KDPENDAFTARAN = KDPENDAFTARAN
                .ID1 = ""
                .ID2 = ""
                .ID3 = ""
                .ID4 = ""
                .ID5 = ""
                .ID6 = ""
                .ID7 = ""
                .ID8 = ""
                .ID9 = ""
                .ID10 = ""
                .REQUEST = REQUEST
                .RESPON = RESPON
                .CATEGORY = category
                .ISDEFAULT = False
                .ISACTIVE = IIf(RESPON.Contains("Sukses"), True, False)
            End With

            If isadd = True Then
                Try
                    fn_SaveBPJSRME = oPendaftaran_BPJSRME.InsertData(ds)
                Catch oErr As Exception
                    'MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                Try
                    fn_SaveBPJSRME = oPendaftaran_BPJSRME.UpdateData(ds)
                Catch oErr As Exception
                    'MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

        Catch oErr As Exception
            'MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveBPJSRME = False
        End Try
    End Function
    Private Function fn_SaveSatuSehatAdmisi(ByVal KDPENDAFTARAN As String, ByVal IDSATUSEHAT As String, ByVal REQUEST As String, ByVal RESPON As String) As Boolean
        Try
            ' ***** HEADER *****
            Dim isadd As Boolean = False

            Dim ds = oPendaftaran_TaskId3.GetStructureHeader
            With ds
                Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()

                Dim dsCek = oPendaftaran_TaskId3.GetData(KDPENDAFTARAN)
                If dsCek Is Nothing Then
                    isadd = True
                    .DATECREATED = WaktuServer
                    .ISDEFAULT = False
                    .ISACTIVE = False
                    .IDSATUSEHAT = IDSATUSEHAT
                Else
                    isadd = False

                    .DATECREATED = dsCek.DATECREATED
                    .ISDEFAULT = dsCek.ISDEFAULT
                    .ISACTIVE = dsCek.ISACTIVE
                    .IDSATUSEHAT = dsCek.IDSATUSEHAT
                End If

                .DATEUPDATED = WaktuServer
                .KDPENDAFTARAN = KDPENDAFTARAN

                .REQUEST = REQUEST
                .RESPON = RESPON

            End With

            If isadd = True Then
                Try
                    fn_SaveSatuSehatAdmisi = oPendaftaran_TaskId3.InsertData(ds)
                Catch oErr As Exception
                    'MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                Try
                    fn_SaveSatuSehatAdmisi = oPendaftaran_TaskId3.UpdateData(ds)
                Catch oErr As Exception
                    'MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

        Catch oErr As Exception
            'MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveSatuSehatAdmisi = False
        End Try
    End Function
    Private Function fn_SaveSatuSehatAnamnesa(ByVal category As String, ByVal KDPENDAFTARAN As String, ByVal IDSATUSEHAT As String, ByVal REQUEST As String, ByVal RESPON As String) As Boolean
        Try
            ' ***** HEADER *****
            Dim isadd As Boolean = False

            Dim ds = oAnamnesis.GetStructureHeader
            With ds
                Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()

                Dim dsCek = oAnamnesis.GetData(KDPENDAFTARAN, category)
                If dsCek Is Nothing Then
                    isadd = True
                    .DATECREATED = WaktuServer
                    .ISDEFAULT = False
                    .ISACTIVE = False
                    .IDSATUSEHAT = IDSATUSEHAT
                Else
                    isadd = False

                    .DATECREATED = dsCek.DATECREATED
                    .ISDEFAULT = dsCek.ISDEFAULT
                    .ISACTIVE = dsCek.ISACTIVE
                    .IDSATUSEHAT = dsCek.IDSATUSEHAT
                End If

                .DATEUPDATED = WaktuServer
                .KDPENDAFTARAN = KDPENDAFTARAN

                .REQUEST = REQUEST
                .RESPON = RESPON
                .CATEGORY = category
            End With

            If isadd = True Then
                Try
                    fn_SaveSatuSehatAnamnesa = oAnamnesis.InsertData(ds)
                Catch oErr As Exception
                    'MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                Try
                    fn_SaveSatuSehatAnamnesa = oAnamnesis.UpdateData(ds)
                Catch oErr As Exception
                    'MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

        Catch oErr As Exception
            'MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveSatuSehatAnamnesa = False
        End Try
    End Function
    Private Function fn_SaveSatuSehatCustomer(ByVal KDCUSTOMER As String, ByVal IDSATUSEHAT As String, ByVal REQUEST As String, ByVal RESPON As String) As Boolean
        Try
            ' ***** HEADER *****
            Dim isUpdate As Boolean = False

            Dim ds = oCustomerSatuSehat.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oCustomerSatuSehat.GetData(KDCUSTOMER).DATECREATED
                    isUpdate = True
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDCUSTOMER = KDCUSTOMER
                .IDSATUSEHAT = IDSATUSEHAT
                .REQUEST = REQUEST
                .RESPON = RESPON
                .ISDEFAULT = False
                .ISACTIVE = True
            End With

            If isUpdate = False Then
                fn_SaveSatuSehatCustomer = oCustomerSatuSehat.InsertData(ds)
            Else
                fn_SaveSatuSehatCustomer = oCustomerSatuSehat.UpdateData(ds)
            End If

        Catch oErr As Exception
            'MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveSatuSehatCustomer = False
        End Try
    End Function
    Private Function fn_SaveSSSend(ByVal sKDPENDAFTARAN As String, ByVal CATEGORY As String, ByVal REQUEST As String, ByVal sURL As String) As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oSSSend.GetStructureHeader
            With ds
                .DATECREATED = Now
                .DATEUPDATED = Now
                .KODESEND = 0
                .KDPENDAFTARAN = sKDPENDAFTARAN
                .URL = sURL
                .CATEGORY = CATEGORY
                .STATUS = ""
                .REQUEST = REQUEST
                .RESPONS = ""
                .ISSTATUS = False
                .KDUSER = sUserID
            End With

            fn_SaveSSSend = oSSSend.InsertData(ds)

        Catch oErr As Exception
            'MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveSSSend = False
        End Try
    End Function
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
    Private Sub picKirimBPJS_Click() Handles picKirimBPJS.Click
        MsgBox("Silahkan Pilih Modul Kirim BPJS RME", MsgBoxStyle.Exclamation, Me.Text)
        'ContextMenuStrip1.Show(picKirimBPJS.Location.X, picKirimBPJS.Location.Y + 125)
        'KirimBPJS("Bundle")
    End Sub
    Private Sub picKirimSatuSehat_Click() Handles picKirimSatuSehat.Click
        ContextMenuStrip2.Show(picKirimBPJS.Location.X, picKirimBPJS.Location.Y + 125)
        'KirimBPJS("Bundle")
    End Sub
    Private Sub KirimBPJS(ByVal KirimResource As String)
        Try
            If txtJenisKunjungan.Text = "" Then
                MsgBox("Jenis Kunjungan kosong", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            Else
                Try
                    If CInt(txtJenisKunjungan.Text) > 2 Then
                        MsgBox("Jenis Kunjungan Tidak Sesuai", MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                    End If
                Catch ex As Exception
                    MsgBox("Jenis Kunjungan kosong", MsgBoxStyle.Exclamation, Me.Text)
                    txtJenisKunjungan.ResetText()
                    Exit Sub
                End Try
            End If
            If txtBulan.Text = "" Then
                MsgBox("Bulan kosong", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            Else
                Try
                    If CInt(txtBulan.Text) > 12 Then
                        MsgBox("Bulan Tidak Sesuai", MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                    End If
                Catch ex As Exception
                    MsgBox("Bulan kosong", MsgBoxStyle.Exclamation, Me.Text)
                    txtBulan.ResetText()
                    Exit Sub
                End Try

            End If
            If txtTahun.Text = "" Then
                MsgBox("Tahun kosong", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            Else
                Try
                    If CInt(txtTahun.Text).ToString.Count > 4 Then
                        MsgBox("Tahun Tidak Sesuai (Validasi tahun lebih dari 5 digit)", MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                    End If
                Catch ex As Exception
                    MsgBox("Tahun kosong", MsgBoxStyle.Exclamation, Me.Text)
                    txtTahun.ResetText()
                    Exit Sub
                End Try
            End If

            If sRMEBPJS_ConsId = "" Then
                MsgBox("Cons ID masih kosong", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            Dim counter As Integer = 0
            Dim counterAll As Integer = 0
            Dim counterGagal As Integer = 0

            For i As Integer = 0 To grv.RowCount - 1
                counterAll += 1
            Next

            'counterAll = counterAll * 3

            If MsgBox("Apakah Akan Kirim Data Pasien Ke BPJS Sebanyak " & counterAll & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            For i As Integer = 0 To grv.RowCount - 1
                Dim dsDaftarCekBPJSRME = oPendaftaran.GetData(grv.GetRowCellValue(i, "KDPENDAFTARAN"))

                If dsDaftarCekBPJSRME IsNot Nothing Then
                    If dsDaftarCekBPJSRME.NOMORSEP = "" Then
                        MsgBox("Nosep Kosong", MsgBoxStyle.Information, Me.Text)

                        counterGagal += 1
                    Else
                        If dsDaftarCekBPJSRME.NOMORSEP.Count <> "19" Then
                            MsgBox("Nosep <> 19 digit", MsgBoxStyle.Information, Me.Text)

                            counterGagal += 1
                        Else
                            Dim idCsuomer As String = String.Empty
                            Dim idDoctor As String = String.Empty
                            Dim idDepartment As String = String.Empty

                            Dim dsCustomerBPJSRME = oCustomer_BPJSRME.GetData(dsDaftarCekBPJSRME.KDCUSTOMER)
                            If dsCustomerBPJSRME IsNot Nothing Then
                                idCsuomer = dsCustomerBPJSRME.ID
                            Else
                                idCsuomer = Guid.NewGuid().ToString()
                                If fn_SaveBPJSRME_Customer(dsDaftarCekBPJSRME.KDCUSTOMER, idCsuomer) = False Then
                                    idCsuomer = ""
                                End If
                            End If

                            Dim dsDoctorBPJSRME = oDoctor_BPJSRME.GetData(dsDaftarCekBPJSRME.KDDOCTOR)
                            If dsDoctorBPJSRME IsNot Nothing Then
                                idDoctor = dsDoctorBPJSRME.ID
                            Else
                                idDoctor = Guid.NewGuid().ToString()
                                If fn_SaveBPJSRME_Doctor(dsDaftarCekBPJSRME.KDDOCTOR, idDoctor, "", "") = False Then
                                    idDoctor = ""
                                End If
                            End If

                            Dim dsDepartmentBPJSRME = oDepartment_BPJSRME.GetData(dsDaftarCekBPJSRME.KDDEPARTMENT)
                            If dsDepartmentBPJSRME IsNot Nothing Then
                                idDepartment = dsDepartmentBPJSRME.ID
                            Else
                                idDepartment = Guid.NewGuid().ToString()
                                If fn_SaveBPJSRME_Department(dsDaftarCekBPJSRME.KDDEPARTMENT, idDepartment, "", "") = False Then
                                    idDepartment = ""
                                End If
                            End If

                            If idCsuomer <> "" And idDoctor <> "" And idDepartment <> "" Then
                                'Dim dsGrouper = oData.GetDataByRegisterData(dsDaftarCekBPJSRME.KDPENDAFTARAN)
                                Dim dsGrouper = oData.GetDataByRegister(dsDaftarCekBPJSRME.KDPENDAFTARAN)
                                If dsGrouper IsNot Nothing Then
                                    Dim jnsipel As String = IIf(dsDaftarCekBPJSRME.CATEGORY = 0, "R.Jalan", "R.Inap")

                                    If dsGrouper.jnsPelayanan <> jnsipel Then
                                        MsgBox("Jenis Pelayanan Tidak Sama dengan data Klaim" & vbCrLf & "jnsPelayanan Klaim " & dsGrouper.jnsPelayanan & vbCrLf & "jnsPelayanan RME " & jnsipel, MsgBoxStyle.Exclamation, Me.Text)
                                        counterGagal += 1
                                    Else
                                        If dsGrouper.noSep <> dsDaftarCekBPJSRME.NOMORSEP Then
                                            MsgBox("Nosep Tidak Sama dengan data Klaim" & vbCrLf & "Nosep Klaim " & dsGrouper.noSep & vbCrLf & "Nosep RME " & dsDaftarCekBPJSRME.NOMORSEP, MsgBoxStyle.Exclamation, Me.Text)
                                            counterGagal += 1
                                        Else
                                            If dsGrouper.tglSep.ToString("yyyy") <> dsDaftarCekBPJSRME.DATE.ToString("yyyy") Then
                                                MsgBox("Tahun Tidak Sama dengan data Klaim" & vbCrLf & "Tahun Klaim " & dsGrouper.tglSep.ToString("yyyy") & vbCrLf & "Tahun RME " & dsDaftarCekBPJSRME.DATE.ToString("yyyy"), MsgBoxStyle.Exclamation, Me.Text)
                                                counterGagal += 1
                                            Else
                                                If dsGrouper.tglSep.ToString("MM") <> dsDaftarCekBPJSRME.DATE.ToString("MM") Then
                                                    MsgBox("Bulan Tidak Sama dengan data Klaim" & vbCrLf & "Bulan Klaim " & dsGrouper.tglSep.ToString("MM") & vbCrLf & "Bulan RME " & dsDaftarCekBPJSRME.DATE.ToString("MM"), MsgBoxStyle.Exclamation, Me.Text)
                                                    counterGagal += 1
                                                Else
                                                    'KirimResource = "Bundle"
                                                    'KirimResource = "Patient"
                                                    'KirimResource = "Composition"

                                                    If KirimResource <> "" Then
                                                        Try
                                                            Dim oKoneksi As New Brigging.clsSetKoneksi
                                                            Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                                                            Dim dataJson As String = ""

                                                            If KirimResource = "Bundle" Then
                                                                Dim dsCPPT = oGrouperDataCppt.GetDataByKodePendaftaranRawatJalanDokter(dsDaftarCekBPJSRME.KDPENDAFTARAN)
                                                                Dim Subjektif As String = ""
                                                                Dim Plan As String = ""
                                                                Dim Alergi As String = ""
                                                                Dim listDiagnosaDokter As New List(Of String)
                                                                Dim listDiagnosaDokterPrimariCode As New List(Of String)
                                                                Dim listDiagnosaDokterPrimariName As New List(Of String)
                                                                Dim listObatObatan As New List(Of String)
                                                                Dim medicationRefs As New List(Of String)
                                                                Dim KDCPPT As String = String.Empty
                                                                Dim jumlahObat As Integer = 0

                                                                If dsCPPT IsNot Nothing Then
                                                                    KDCPPT = dsCPPT.KDCPPT
                                                                    Subjektif = dsCPPT.SUBJEKTIF_KELUHANUTAMA
                                                                    Plan = dsCPPT.PLANNING_TEXT
                                                                    Alergi = dsCPPT.SUBJEKTIF_ALERGI_YA_TEXT

                                                                    For Each xloop In oGrouperDataCppt.GetDataDetailDiagnosa(dsCPPT.KDCPPT)
                                                                        If xloop.KATEGORI = "Primary" Then
                                                                            listDiagnosaDokterPrimariCode.Add(xloop.KDDIAGNOSA)
                                                                            listDiagnosaDokterPrimariName.Add(xloop.MEMO)
                                                                        End If
                                                                        listDiagnosaDokter.Add(xloop.MEMO)
                                                                    Next
                                                                    For Each xloop In oGrouperDataCppt.GetDataDetailNonRacikan(dsCPPT.KDCPPT)
                                                                        listObatObatan.Add(xloop.NAMAOBAT)
                                                                        medicationRefs.Add("MedicationRequest/" & Guid.NewGuid().ToString())
                                                                        jumlahObat += 1
                                                                    Next
                                                                    For Each xloop In oGrouperDataCppt.GetDataDetailRacikan(dsCPPT.KDCPPT)
                                                                        listObatObatan.Add(xloop.NAMAOBAT)
                                                                        medicationRefs.Add("MedicationRequest/" & Guid.NewGuid().ToString())
                                                                    Next

                                                                    dataJson = BuildBundleWith6Resources(fn_UDD(sPPKPELAYANAN, sRMEBPJS_koderskemenkes, IIf(dsDaftarCekBPJSRME.CATEGORY = 0, "2", "1")), dsDaftarCekBPJSRME.NOMORSEP, idCsuomer, dsDaftarCekBPJSRME.KDCUSTOMER, sUserID, dsDaftarCekBPJSRME.KARTUBPJS, dsDaftarCekBPJSRME.M_CUSTOMER.KTP, dsDaftarCekBPJSRME.M_CUSTOMER.NAME_DISPLAY, dsDaftarCekBPJSRME.M_CUSTOMER.PHONE, IIf(dsDaftarCekBPJSRME.M_CUSTOMER.KDJENISKELAMIN = 0, "female", "male"), dsDaftarCekBPJSRME.M_CUSTOMER.TANGGALLAHIR.ToString("yyyy-MM-dd"), dsDaftarCekBPJSRME.M_CUSTOMER.ALAMAT, dsDaftarCekBPJSRME.M_CUSTOMER.M_KELURAHAN.M_KECAMATAN.M_KABUPATEN.MEMO, dsDaftarCekBPJSRME.M_CUSTOMER.M_KELURAHAN.M_KECAMATAN.MEMO, dsDaftarCekBPJSRME.M_CUSTOMER.NEGARA, dsDaftarCekBPJSRME.M_CUSTOMER.KODEPOS, idDepartment, dsDepartmentBPJSRME.M_DEPARTMENT.NAME_DISPLAY, fn_UDD(sPPKPELAYANAN, sRMEBPJS_koderskemenkes, IIf(dsDaftarCekBPJSRME.CATEGORY = 0, "2", "1")), String.Join(", ", listDiagnosaDokterPrimariCode.ToArray), String.Join(", ", listDiagnosaDokterPrimariName.ToArray), dsDaftarCekBPJSRME.DATE.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), fn_UDD(sPPKPELAYANAN, sRMEBPJS_koderskemenkes, IIf(dsDaftarCekBPJSRME.CATEGORY = 0, "2", "1")), fn_UDD(sPPKPELAYANAN, sRMEBPJS_koderskemenkes, IIf(dsDaftarCekBPJSRME.CATEGORY = 0, "2", "1")), dsDaftarCekBPJSRME.DATE.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), idDoctor, dsDoctorBPJSRME.M_DOCTOR.NAME_DISPLAY, IIf(dsDaftarCekBPJSRME.TUJUANKUNJUNGAN = "", "Normal", dsDaftarCekBPJSRME.TUJUANKUNJUNGAN), Subjektif, dsDaftarCekBPJSRME.M_DIAGNOSA.MEMO, String.Join(", ", listObatObatan.ToArray), Plan, Alergi, KDCPPT, jumlahObat, "")
                                                                    'dataJson = GetFHIRJsonString()
                                                                End If
                                                            ElseIf KirimResource = "Composition"
                                                                Dim dsCek = oPendaftaran_BPJSRME.GetData(dsDaftarCekBPJSRME.KDPENDAFTARAN)
                                                                Dim dsCPPT = oGrouperDataCppt.GetDataByKodePendaftaranRawatJalanDokter(dsDaftarCekBPJSRME.KDPENDAFTARAN)
                                                                Dim Subjektif As String = ""
                                                                Dim Plan As String = ""
                                                                Dim Alergi As String = ""
                                                                Dim listDiagnosaDokter As New List(Of String)
                                                                Dim listObatObatan As New List(Of String)
                                                                Dim medicationRefs As New List(Of String)

                                                                If dsCPPT IsNot Nothing Then
                                                                    Subjektif = dsCPPT.SUBJEKTIF_KELUHANUTAMA
                                                                    Plan = dsCPPT.PLANNING_TEXT
                                                                    Alergi = dsCPPT.SUBJEKTIF_ALERGI_YA_TEXT

                                                                    For Each xloop In oGrouperDataCppt.GetDataDetailDiagnosa(dsCPPT.KDCPPT)
                                                                        listDiagnosaDokter.Add(xloop.MEMO)
                                                                    Next
                                                                    For Each xloop In oGrouperDataCppt.GetDataDetailNonRacikan(dsCPPT.KDCPPT)
                                                                        listObatObatan.Add(xloop.NAMAOBAT)
                                                                        medicationRefs.Add("MedicationRequest/" & Guid.NewGuid().ToString())
                                                                    Next
                                                                    For Each xloop In oGrouperDataCppt.GetDataDetailRacikan(dsCPPT.KDCPPT)
                                                                        listObatObatan.Add(xloop.NAMAOBAT)
                                                                        medicationRefs.Add("MedicationRequest/" & Guid.NewGuid().ToString())
                                                                    Next
                                                                End If

                                                                If dsCek IsNot Nothing Then
                                                                    If SatusehatAuth.GetToken(dsCek.RESPON, "message") = "OK" Then
                                                                        dataJson = ""
                                                                    Else
                                                                        dataJson = BuildBundleWithComposition(fn_UDD(sPPKPELAYANAN, sRMEBPJS_koderskemenkes, IIf(dsDaftarCekBPJSRME.CATEGORY = 0, "2", "1")), dsDaftarCekBPJSRME.NOMORSEP, idCsuomer, dsDaftarCekBPJSRME.M_CUSTOMER.NAME_DISPLAY, "Encounter/" & fn_UDD(sPPKPELAYANAN, sRMEBPJS_koderskemenkes, IIf(dsDaftarCekBPJSRME.CATEGORY = 0, "2", "1")), idDoctor, dsDaftarCekBPJSRME.M_DOCTOR.NAME_DISPLAY, fn_UDD(sPPKPELAYANAN, sRMEBPJS_koderskemenkes, IIf(dsDaftarCekBPJSRME.CATEGORY = 0, "2", "1")), String.Join(", ", listDiagnosaDokter.ToArray), dsDaftarCekBPJSRME.M_DIAGNOSA.MEMO, String.Join(", ", listObatObatan.ToArray), Plan, Alergi, "urn:uuid:" & fn_UDD(sPPKPELAYANAN, sRMEBPJS_koderskemenkes, IIf(dsDaftarCekBPJSRME.CATEGORY = 0, "2", "1")), "", dsDaftarCekBPJSRME.DATE.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), IIf(dsDaftarCekBPJSRME.TUJUANKUNJUNGAN = "", "Normal", dsDaftarCekBPJSRME.TUJUANKUNJUNGAN), Subjektif, medicationRefs)
                                                                    End If
                                                                Else
                                                                    dataJson = BuildBundleWithComposition(fn_UDD(sPPKPELAYANAN, sRMEBPJS_koderskemenkes, IIf(dsDaftarCekBPJSRME.CATEGORY = 0, "2", "1")), dsDaftarCekBPJSRME.NOMORSEP, idCsuomer, dsDaftarCekBPJSRME.M_CUSTOMER.NAME_DISPLAY, "Encounter/" & fn_UDD(sPPKPELAYANAN, sRMEBPJS_koderskemenkes, IIf(dsDaftarCekBPJSRME.CATEGORY = 0, "2", "1")), idDoctor, dsDaftarCekBPJSRME.M_DOCTOR.NAME_DISPLAY, fn_UDD(sPPKPELAYANAN, sRMEBPJS_koderskemenkes, IIf(dsDaftarCekBPJSRME.CATEGORY = 0, "2", "1")), String.Join(", ", listDiagnosaDokter.ToArray), dsDaftarCekBPJSRME.M_DIAGNOSA.MEMO, String.Join(", ", listObatObatan.ToArray), Plan, Alergi, "urn:uuid:" & fn_UDD(sPPKPELAYANAN, sRMEBPJS_koderskemenkes, IIf(dsDaftarCekBPJSRME.CATEGORY = 0, "2", "1")), "", dsDaftarCekBPJSRME.DATE.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), IIf(dsDaftarCekBPJSRME.TUJUANKUNJUNGAN = "", "Normal", dsDaftarCekBPJSRME.TUJUANKUNJUNGAN), Subjektif, medicationRefs)
                                                                End If
                                                            ElseIf KirimResource = "Patient"
                                                                Dim dsCek = oPendaftaran_BPJSRME.GetData(dsDaftarCekBPJSRME.KDPENDAFTARAN)
                                                                If dsCek IsNot Nothing Then
                                                                    If SatusehatAuth.GetToken(dsCek.RESPON, "message") = "OK" Then
                                                                        dataJson = ""
                                                                    Else
                                                                        dataJson = BuildBundleWithPatient(KirimResource, dsDaftarCekBPJSRME.NOMORSEP, idCsuomer, dsDaftarCekBPJSRME.M_CUSTOMER.NAME_DISPLAY, IIf(dsDaftarCekBPJSRME.M_CUSTOMER.KDJENISKELAMIN = 0, "female", "male"), dsDaftarCekBPJSRME.M_CUSTOMER.TANGGALLAHIR.ToString("yyyy-MM-dd"), dsDaftarCekBPJSRME.M_CUSTOMER.KTP, dsDaftarCekBPJSRME.KDCUSTOMER, dsDaftarCekBPJSRME.M_CUSTOMER.KARTUBPJS, dsDaftarCekBPJSRME.M_CUSTOMER.ALAMAT, dsDaftarCekBPJSRME.M_CUSTOMER.PHONE, sRMEBPJS_koderskemenkes, sCompany)
                                                                    End If
                                                                Else
                                                                    dataJson = BuildBundleWithPatient(KirimResource, dsDaftarCekBPJSRME.NOMORSEP, idCsuomer, dsDaftarCekBPJSRME.M_CUSTOMER.NAME_DISPLAY, IIf(dsDaftarCekBPJSRME.M_CUSTOMER.KDJENISKELAMIN = 0, "female", "male"), dsDaftarCekBPJSRME.M_CUSTOMER.TANGGALLAHIR.ToString("yyyy-MM-dd"), dsDaftarCekBPJSRME.M_CUSTOMER.KTP, dsDaftarCekBPJSRME.KDCUSTOMER, dsDaftarCekBPJSRME.M_CUSTOMER.KARTUBPJS, dsDaftarCekBPJSRME.M_CUSTOMER.ALAMAT, dsDaftarCekBPJSRME.M_CUSTOMER.PHONE, sRMEBPJS_koderskemenkes, sCompany)
                                                                End If
                                                            End If

                                                            If dataJson <> "" Then
                                                                Dim compressionResult As CompressionResult = FhirGzipCompressor.CompressWithStats(dataJson)

                                                                Dim dataMR As String = oKoneksi.EncryptBPJS2(sRMEBPJS_ConsId, sRMEBPJS_SecreatKey, sPPKPELAYANAN, compressionResult.CompressedBase64)

                                                                Dim jsonRequest As String = String.Empty

                                                                jsonRequest = "{ "
                                                                jsonRequest &= " ""request"" :   { "
                                                                jsonRequest &= " ""noSep"" :  """ & dsDaftarCekBPJSRME.NOMORSEP & "" & "" & """ , "
                                                                jsonRequest &= " ""jnsPelayanan"" :  """ & "" & IIf(dsDaftarCekBPJSRME.CATEGORY = 0, "2", "1") & "" & """ , "
                                                                jsonRequest &= " ""bulan"" :  """ & "" & CInt(CDate(dsDaftarCekBPJSRME.DATE).ToString("MM")) & "" & """ , "
                                                                jsonRequest &= " ""tahun"" :  """ & "" & CDate(dsDaftarCekBPJSRME.DATE).ToString("yyyy") & "" & """ , "
                                                                jsonRequest &= " ""dataMR"" :  """ & dataMR & """ "
                                                                jsonRequest &= " } "
                                                                jsonRequest &= " } "

                                                                Dim kirim As String = oKoneksi.InsertMedicalRecord(sRMEBPJS_Url, sRMEBPJS_ConsId, sRMEBPJS_SecreatKey, sRMEBPJS_UserKey, uTime, jsonRequest)

                                                                If fn_SaveBPJSRME(KirimResource, dsDaftarCekBPJSRME.KDPENDAFTARAN, dataJson, kirim) = True Then
                                                                    If SatusehatAuth.GetToken(kirim, "message") = "OK" Then
                                                                        'MsgBox(dsDaftarCekBPJSRME.NOMORSEP & " Resource " & vbCrLf & KirimResource & " Berhasil Kirim", MsgBoxStyle.Information, Me.Text)
                                                                        counter += 1
                                                                    Else
                                                                        'MsgBox(dsDaftarCekBPJSRME.NOMORSEP & " Resource " & vbCrLf & KirimResource & " Gagal Kirim" & vbCrLf & kirim, MsgBoxStyle.Information, Me.Text)
                                                                        counterGagal += 1
                                                                    End If
                                                                Else
                                                                    counterGagal += 1
                                                                End If
                                                            Else
                                                                counterGagal += 1
                                                            End If

                                                        Catch oErr As Exception
                                                            counterGagal += 1
                                                            'MsgBox("Looping" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                                                        End Try
                                                    Else
                                                        counterGagal += 1
                                                    End If
                                                    'For x As Integer = 1 To Kirim
                                                    '    KirimResource = ""
                                                    '    'BuildBundleWith6Resources
                                                    '    If x = 1 Then
                                                    '        If Kirim = 1 Then
                                                    '            KirimResource = "Bundle"
                                                    '        Else
                                                    '            'KirimResource = "Composition"
                                                    '        End If
                                                    '    ElseIf x = 2
                                                    '        KirimResource = "Patient"
                                                    '    ElseIf x = 3
                                                    '        KirimResource = "Procedure"
                                                    '    End If



                                                    'Next
                                                End If
                                            End If
                                        End If

                                    End If
                                Else
                                    MsgBox("Data Grrouping Kosong", MsgBoxStyle.Exclamation, Me.Text)
                                    counterGagal += 1
                                End If
                            Else
                                If fn_SaveBPJSRME(KirimResource, dsDaftarCekBPJSRME.KDPENDAFTARAN, "", "Id Pasien : " & idCsuomer & ", Id Dokter : " & idDoctor & ", Id Department : " & idDepartment) = True Then

                                End If

                                counterGagal += 1
                            End If

                        End If

                    End If

                Else
                    counterGagal += 1
                End If
            Next

            SplashScreenManager.CloseForm(False)

            MsgBox("Kirim Data Pasien : " & counterAll & vbCrLf & "*Gagal Resource : " & counterGagal & vbCrLf & "*Berhasil Resource : " & counter, MsgBoxStyle.Information, Me.Text)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Looping" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub KirimSatuSehat(ByVal KirimResource As String)
        Try
            If SatuSehat_Organisasi = "" Then
                MsgBox("Organisasi ID masih kosong", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            Dim counter As Integer = 0
            Dim counterAll As Integer = 0
            Dim counterGagal As Integer = 0

            For i As Integer = 0 To grv.RowCount - 1
                counterAll += 1
            Next

            If MsgBox("Apakah Akan Kirim Data Ke Satu Sehat Sebanyak " & counterAll & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            For i As Integer = 0 To grv.RowCount - 1
                If KirimResource = "Pasien" Then
                    If grv.GetRowCellValue(i, "KTP") <> "" Then
                        If CekNIKPasien(grv.GetRowCellValue(i, "KDCUSTOMER"), grv.GetRowCellValue(i, "KTP"), False) = "" Then
                            counterGagal += 1
                        Else
                            counter += 1
                        End If
                    Else
                        counterGagal += 1
                    End If
                End If
                'If grv.GetRowCellValue(i, "KTP") <> "" Then
                '    Dim idpasien As String = CekNIKPasien(grv.GetRowCellValue(i, "KDCUSTOMER"), grv.GetRowCellValue(i, "KTP"))

                '    If idpasien <> "" Then
                '        If Ecounter_Admisi_TaksID3(grv.GetRowCellValue(i, "KDPENDAFTARAN"), idpasien, grv.GetRowCellValue(i, "KDSOWMED_CT"), grv.GetRowCellValue(i, "KDSOWMED_CT_TEXT")) = True Then
                '            Dim dsCekUlang = oPendaftaran_TaskId3.GetData(grv.GetRowCellValue(i, "KDPENDAFTARAN"))

                '            If dsCekUlang IsNot Nothing Then
                '                If dsCekUlang.ISDEFAULT = False Then
                '                    If dsCekUlang.IDSATUSEHAT <> "" Then
                '                        If Ecounter_Admisi_TaksID4(grv.GetRowCellValue(i, "KDPENDAFTARAN"), idpasien, grv.GetRowCellValue(i, "KDSOWMED_CT"), grv.GetRowCellValue(i, "KDSOWMED_CT_TEXT"), dsCekUlang.IDSATUSEHAT) = True Then
                '                            If oPendaftaran_TaskId3.UpdateMasukRuangan(grv.GetRowCellValue(i, "KDPENDAFTARAN")) = True Then
                '                                If IsianCPPT(grv.GetRowCellValue(i, "KDPENDAFTARAN"), idpasien, grv.GetRowCellValue(i, "KDSOWMED_CT"), grv.GetRowCellValue(i, "KDSOWMED_CT_TEXT"), dsCekUlang.IDSATUSEHAT) = True Then
                '                                    counter += 1
                '                                Else
                '                                    counterGagal += 1
                '                                End If
                '                            Else
                '                                counterGagal += 1
                '                            End If
                '                        Else
                '                            counterGagal += 1
                '                        End If
                '                    Else
                '                        counterGagal += 1
                '                    End If

                '                Else
                '                    If dsCekUlang.IDSATUSEHAT <> "" Then
                '                        If IsianCPPT(grv.GetRowCellValue(i, "KDPENDAFTARAN"), idpasien, grv.GetRowCellValue(i, "KDSOWMED_CT"), grv.GetRowCellValue(i, "KDSOWMED_CT_TEXT"), dsCekUlang.IDSATUSEHAT) = True Then
                '                            counter += 1
                '                        Else
                '                            counterGagal += 1
                '                        End If
                '                    Else
                '                        counterGagal += 1
                '                    End If
                '                End If
                '            Else
                '                counterGagal += 1
                '            End If
                '        Else
                '            counterGagal += 1
                '        End If
                '    Else
                '        counterGagal += 1
                '    End If
                'Else
                '    counterGagal += 1
                'End If
            Next

            SplashScreenManager.CloseForm(False)

            MsgBox("Kirim Data : " & counterAll & vbCrLf & "*Gagal : " & counterGagal & vbCrLf & "*Berhasil : " & counter, MsgBoxStyle.Information, Me.Text)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Looping" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
#Region "RME BPJS bak"
    Public Function BuildBundleWith6Resources(
    bundleId_id As String,
    sepNumber As String,
    patientId As String,
    NoRM As String,
    kduserassigner_display As String,
    nokartu As String,
    nonik As String,
    patientName As String,
    patientPhone As String,
    patientGender As String,
    patientBirthDate As String,
    patientAddress As String,
    city As String,
    district As String,
    state As String,
    postalCode As String,
    organizationId As String,
    organizationName As String,
    idCondition As String,
    icd10_kode_dokter As String,
    icd10_name_dokter As String,
    tanggal_condition As String,
    composition_Id As String,
    composition_encounterId As String,
    composition_tanggal As String,
    practitionerId_dokter As String,
    practitionerId_doktername As String,
    section0 As String,
    section1 As String,
    section2 As String,
    section4 As String,
    section5 As String,
    section7 As String,
    KDCPPT As String,
    jumlahitemobat As Integer,
    idProcedure As String
     ) As String

        Dim sb As New StringBuilder()

        ' =====================================================
        ' BUNDLE AWAL
        ' =====================================================
        sb.AppendLine("{")
        sb.AppendLine("  ""resourceType"": ""Bundle"",")
        sb.AppendLine($"  ""id"": ""{bundleId_id}"",")
        sb.AppendLine("  ""meta"": {")
        sb.AppendLine($"    ""lastUpdated"": ""{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}""")
        sb.AppendLine("  },")
        sb.AppendLine("  ""identifier"": {")
        sb.AppendLine("    ""system"": ""sep"",")
        sb.AppendLine($"    ""value"": ""{sepNumber}""")
        sb.AppendLine("  },")
        sb.AppendLine("  ""type"": ""document"",")
        sb.AppendLine("  ""entry"": [")

        ' =====================================================
        ' 1. PATIENT RESOURCE
        ' =====================================================
        sb.AppendLine("    {")
        sb.AppendLine("      ""resource"": {")
        sb.AppendLine("        ""resourceType"": ""Patient"",")
        sb.AppendLine($"        ""id"": ""{patientId}"",")
        sb.AppendLine("        ""identifier"": [")
        sb.AppendLine("          {")
        sb.AppendLine("            ""use"": ""usual"",")
        sb.AppendLine("            ""type"": {")
        sb.AppendLine("              ""coding"": [{")
        sb.AppendLine("                ""system"": ""http://hl7.org/fhir/v2/0203"",")
        sb.AppendLine("                ""code"": ""MR""")
        sb.AppendLine("              }]")
        sb.AppendLine("            },")
        sb.AppendLine($"            ""value"": ""{EscapeJson(NoRM)}"",")
        sb.AppendLine("            ""assigner"": {")
        sb.AppendLine($"              ""display"": ""{EscapeJson(kduserassigner_display)}""")
        sb.AppendLine("            }")
        sb.AppendLine("          },")
        sb.AppendLine("          {")
        sb.AppendLine("            ""use"": ""official"",")
        sb.AppendLine("            ""type"": {")
        sb.AppendLine("              ""coding"": [{")
        sb.AppendLine("                ""system"": ""http://hl7.org/fhir/v2/0203"",")
        sb.AppendLine("                ""code"": ""MB""")
        sb.AppendLine("              }]")
        sb.AppendLine("            },")
        sb.AppendLine($"            ""value"": ""{EscapeJson(nokartu)}"",")
        sb.AppendLine("            ""assigner"": {")
        sb.AppendLine("              ""display"": ""BPJS KESEHATAN""")
        sb.AppendLine("            }")
        sb.AppendLine("          },")
        sb.AppendLine("          {")
        sb.AppendLine("            ""use"": ""official"",")
        sb.AppendLine("            ""type"": {")
        sb.AppendLine("              ""coding"": [{")
        sb.AppendLine("                ""system"": ""http://hl7.org/fhir/v2/0203"",")
        sb.AppendLine("                ""code"": ""NNIDN""")
        sb.AppendLine("              }]")
        sb.AppendLine("            },")
        sb.AppendLine($"            ""value"": ""{EscapeJson(nonik)}"",")
        sb.AppendLine("            ""assigner"": {")
        sb.AppendLine("              ""display"": ""KEMENDAGRI""")
        sb.AppendLine("            }")
        sb.AppendLine("          }")
        sb.AppendLine("        ],")
        sb.AppendLine("        ""active"": true,")
        sb.AppendLine("        ""name"": [{")
        sb.AppendLine("          ""use"": ""official"",")
        sb.AppendLine($"          ""text"": ""{EscapeJson(patientName)}""")
        sb.AppendLine("        }],")
        sb.AppendLine("        ""maritalStatus"": {")
        sb.AppendLine("          ""coding"": [{")
        sb.AppendLine("            ""system"": ""http://hl7.org/fhir/v3/MaritalStatus"",")
        sb.AppendLine("            ""code"": ""U""")
        sb.AppendLine("          }]")
        sb.AppendLine("        },")
        sb.AppendLine("        ""telecom"": [")
        sb.AppendLine("          {""system"": ""phone"", ""value"": """", ""use"": ""work""},")
        sb.AppendLine($"          {{""system"": ""phone"", ""value"": ""{EscapeJson(patientPhone)}"", ""use"": ""mobile""}},")
        sb.AppendLine("          {""system"": ""phone"", ""value"": ""TDK ADA"", ""use"": ""home""}")
        sb.AppendLine("        ],")
        sb.AppendLine($"        ""gender"": ""{patientGender}"",")
        sb.AppendLine($"        ""birthDate"": ""{patientBirthDate}"",")
        sb.AppendLine("        ""deceasedBoolean"": false,")
        sb.AppendLine("        ""address"": [{")
        sb.AppendLine("          ""line"": [")
        sb.AppendLine($"            ""{EscapeJson(patientAddress)}""")
        sb.AppendLine("          ],")
        sb.AppendLine("          ""city"": """ & city & """,")
        sb.AppendLine("          ""district"": """ & district & """,")
        sb.AppendLine("          ""state"": """ & state & """,")
        sb.AppendLine("          ""postalCode"": """ & postalCode & """,")
        sb.AppendLine($"          ""text"": ""{EscapeJson(patientAddress)}"",")
        sb.AppendLine("          ""use"": ""home"",")
        sb.AppendLine("          ""type"": ""both""")
        sb.AppendLine("        }],")
        sb.AppendLine("        ""managingOrganization"": {")
        sb.AppendLine($"          ""reference"": ""Organization/{organizationId}"",")
        sb.AppendLine($"          ""display"": ""{EscapeJson(organizationName)}""")
        sb.AppendLine("        }")
        sb.AppendLine("      }")
        sb.AppendLine("    },")

        ' =====================================================
        ' 2. CONDITION RESOURCE
        ' =====================================================
        sb.AppendLine("    {")
        sb.AppendLine("      ""resource"": {")
        sb.AppendLine("        ""resourceType"": ""Condition"",")
        sb.AppendLine($"        ""id"": ""{idCondition}"",")
        sb.AppendLine("        ""clinicalStatus"": ""active"",")
        sb.AppendLine("        ""verificationStatus"": ""confirmed"",")
        sb.AppendLine("        ""category"": [{")
        sb.AppendLine("          ""coding"": [{")
        sb.AppendLine("            ""system"": ""http://hl7.org/fhir/condition-category"",")
        sb.AppendLine("            ""code"": ""encounter-diagnosis"",")
        sb.AppendLine("            ""display"": ""Encounter Diagnosis""")
        sb.AppendLine("          }]")
        sb.AppendLine("        }],")
        sb.AppendLine("        ""code"": {")
        sb.AppendLine("          ""coding"": [{")
        sb.AppendLine("            ""system"": ""http://hl7.org/fhir/sid/icd-10"",")
        sb.AppendLine("            ""code"": """ & icd10_kode_dokter & """,")
        sb.AppendLine($"            ""display"": ""{EscapeJson(icd10_name_dokter)}""")
        sb.AppendLine("          }],")
        sb.AppendLine($"          ""text"": ""{EscapeJson(icd10_name_dokter)}""")
        sb.AppendLine("        },")
        sb.AppendLine("        ""subject"": {")
        sb.AppendLine($"          ""reference"": ""Patient/{patientId}""")
        sb.AppendLine("        },")
        sb.AppendLine($"        ""onsetDateTime"": ""{tanggal_condition}""")
        sb.AppendLine("      }")
        sb.AppendLine("    },")

        'If idProcedure <> "" Then
        '    ' =====================================================
        '    ' 3. PROCEDURE RESOURCE
        '    ' =====================================================
        '    sb.AppendLine("    {")
        '    sb.AppendLine("      ""resource"": {")
        '    sb.AppendLine("        ""resourceType"": ""Procedure"",")
        '    sb.AppendLine($"        ""id"": ""{idProcedure}"",")
        '    sb.AppendLine("        ""text"": {")
        '    sb.AppendLine("          ""status"": ""generated"",")
        '    sb.AppendLine("          ""div"": ""<div>Generated Narrative with Details</div>""")
        '    sb.AppendLine("        },")
        '    sb.AppendLine("        ""status"": ""completed"",")
        '    sb.AppendLine("        ""category"": {")
        '    sb.AppendLine("          ""coding"": [{""system"": ""http://snomed.info/sct"", ""code"": ""387713003"", ""display"": ""Surgical procedure""}],")
        '    sb.AppendLine("          ""text"": """"")
        '    sb.AppendLine("        },")
        '    sb.AppendLine("        ""code"": {")
        '    sb.AppendLine("          ""coding"": [{")
        '    sb.AppendLine("            ""system"": ""http://snomed.info/sct"",")
        '    sb.AppendLine($"            ""code"": ""{EscapeJson(procedureCode)}"",")
        '    sb.AppendLine($"            ""display"": ""{EscapeJson(procedureDisplay)}""")
        '    sb.AppendLine("          }]")
        '    sb.AppendLine("        },")
        '    sb.AppendLine("        ""subject"": {")
        '    sb.AppendLine($"          ""reference"": ""Patient/{patientId}"",")
        '    sb.AppendLine($"          ""display"": ""{EscapeJson(patientName)}""")
        '    sb.AppendLine("        },")
        '    sb.AppendLine("        ""context"": {")
        '    sb.AppendLine($"          ""reference"": ""Encounter/{encounterId}"",")
        '    sb.AppendLine($"          ""display"": ""{EscapeJson(patientName)} encounter""")
        '    sb.AppendLine("        },")
        '    sb.AppendLine("        ""performedPeriod"": {")
        '    sb.AppendLine($"          ""start"": ""{procedureDate}"",")
        '    sb.AppendLine($"          ""end"": ""{procedureDate}""")
        '    sb.AppendLine("        },")
        '    sb.AppendLine("        ""performer"": [{")
        '    sb.AppendLine("          ""role"": {")
        '    sb.AppendLine("            ""coding"": [{""system"": ""http://snomed.info/sct"", ""code"": """", ""display"": """"}]")
        '    sb.AppendLine("          },")
        '    sb.AppendLine("          ""actor"": {")
        '    sb.AppendLine($"            ""reference"": ""Practitioner/{practitionerId}"",")
        '    sb.AppendLine("            ""display"": """"")
        '    sb.AppendLine("          }")
        '    sb.AppendLine("        }],")
        '    sb.AppendLine("        ""reasonCode"": [{")
        '    sb.AppendLine($"          ""text"": ""DiagnosticReport/{diagnosticReportId}""")
        '    sb.AppendLine("        }],")
        '    sb.AppendLine("        ""bodySite"": [{")
        '    sb.AppendLine("          ""coding"": [{""system"": ""http://snomed.info/sct"", ""code"": ""818981001"", ""display"": ""Entire head""}]")
        '    sb.AppendLine("        }],")
        '    sb.AppendLine("        ""note"": [{""text"": ""Prosedur berjalan lancar""}]")
        '    sb.AppendLine("      }")
        '    sb.AppendLine("    },")
        'End If

        ' =====================================================
        ' 4. MEDICATION REQUEST RESOURCE
        ' =====================================================

        If jumlahitemobat > 0 Then
            sb.AppendLine("    {")
            sb.AppendLine("  ""resource"": [")

            Dim i As Integer = 0
            Dim oSigna As New Reference.clsSigna

            For Each xloop In oGrouperDataCppt.GetDataDetailNonRacikan(KDCPPT)

                sb.AppendLine("    {")
                sb.AppendLine("      ""resourceType"": ""MedicationRequest"",")
                sb.AppendLine("      ""text"": {")
                sb.AppendLine($"        ""div"": ""{EscapeJson(xloop.NAMAOBAT)}""")
                sb.AppendLine("      },")
                sb.AppendLine("      ""identifier"": {")
                sb.AppendLine("        ""system"": ""id_resep_pulang"",")
                sb.AppendLine($"        ""value"": ""{EscapeJson(fn_UDD(sPPKPELAYANAN, sRMEBPJS_koderskemenkes, IIf(chkRawat.Checked = False, "2", "1")))}""")
                sb.AppendLine("      },")
                sb.AppendLine("      ""subject"": {")
                sb.AppendLine($"        ""display"": ""{EscapeJson(patientName)}"",")
                sb.AppendLine($"        ""reference"": ""Patient/{patientId}""")
                sb.AppendLine("      },")
                sb.AppendLine($"      ""intent"": ""final"",")
                sb.AppendLine("      ""medicationCodeableConcept"": {")
                sb.AppendLine("        ""coding"": [")
                sb.AppendLine("          {")
                sb.AppendLine($"            ""code"": ""DRx0006657"",")
                sb.AppendLine("            ""system"": ""http://rscm.co.id/drug""")
                sb.AppendLine("          }")
                sb.AppendLine("        ],")
                sb.AppendLine($"        ""text"": ""{EscapeJson(xloop.NAMAOBAT)}""")
                sb.AppendLine("      },")
                sb.AppendLine("      ""dosageInstruction"": [")
                sb.AppendLine("        {")
                sb.AppendLine("          ""doseQuantity"": {")
                sb.AppendLine($"            ""code"": ""{EscapeJson(xloop.SATUAN)}"",")
                sb.AppendLine("            ""system"": ""http://unitsofmeasure.org"",")
                sb.AppendLine($"            ""unit"": ""{EscapeJson(xloop.SATUAN)}"",")
                sb.AppendLine($"            ""value"": ""{xloop.JUMLAH.ToString.Replace(",", ".")}""")
                sb.AppendLine("          },")
                sb.AppendLine("          ""route"": {")
                sb.AppendLine("            ""coding"": [")
                sb.AppendLine("              {")
                sb.AppendLine("                ""system"": ""http://snomed.info/sct"",")
                sb.AppendLine($"                ""code"": ""001"",")
                sb.AppendLine($"                ""display"": ""ORAL""")
                sb.AppendLine("              }")
                sb.AppendLine("            ]")
                sb.AppendLine("          },")
                sb.AppendLine("          ""timing"": {")
                sb.AppendLine("            ""repeat"": {")
                sb.AppendLine($"              ""frequency"": {CInt(oSigna.GetData(xloop.KDSIGNA).SIGNA_1)},")
                sb.AppendLine($"              ""period"": {CInt(oSigna.GetData(xloop.KDSIGNA).SIGNA_2)},")
                sb.AppendLine("              ""periodUnit"": ""d""")
                sb.AppendLine("            }")
                sb.AppendLine("          },")
                sb.AppendLine("          ""additionalInstruction"": [")
                sb.AppendLine("            {")
                sb.AppendLine($"              ""text"": ""{EscapeJson(xloop.SIGNA)}""")
                sb.AppendLine("            }")
                sb.AppendLine("          ]")
                sb.AppendLine("        }")
                sb.AppendLine("      ],")
                sb.AppendLine("      ""reasonCode"": [")
                sb.AppendLine("        {")
                sb.AppendLine("          ""coding"": [")
                sb.AppendLine("            {")
                sb.AppendLine("              ""code"": """",")
                sb.AppendLine("              ""display"": """",")
                sb.AppendLine("              ""system"": """"")
                sb.AppendLine("            }")
                sb.AppendLine("          ],")
                sb.AppendLine("          ""text"": """"")
                sb.AppendLine("        }")
                sb.AppendLine("      ],")
                sb.AppendLine("      ""requester"": {")
                sb.AppendLine("        ""agent"": {")
                sb.AppendLine($"          ""display"": ""{EscapeJson(practitionerId_doktername)}"",")
                sb.AppendLine($"          ""reference"": ""Practitioner/{practitionerId_dokter }""")
                sb.AppendLine("        },")
                sb.AppendLine("        ""onBehalfOf"": {")
                sb.AppendLine($"          ""reference"": ""Organization/{organizationId }""")
                sb.AppendLine("        }")
                sb.AppendLine("      },")
                sb.AppendLine("      ""meta"": {")
                sb.AppendLine($"        ""lastUpdated"": ""{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}""")
                sb.AppendLine("      }")
                sb.AppendLine("    }")

                i += 1

                If i < jumlahitemobat Then
                    sb.AppendLine("    ,")
                End If


            Next

            sb.AppendLine("  ]")
            sb.AppendLine("    },")

        End If

        'DiagnosticReport

        sb.AppendLine("{")
        sb.AppendLine("    ""resource"": [")
        sb.AppendLine("        {")
        sb.AppendLine("            ""resourceType"": ""DiagnosticReport"",")
        sb.AppendLine("            ""id"": ""0464R000-1229344-2-acd3b089-2989-4bce-a4e8-fff504997d97"",")
        sb.AppendLine("            ""subject"": {")
        sb.AppendLine($"                ""reference"": ""Patient/{patientId }"",")
        sb.AppendLine($"                ""display"": ""{patientName }"",")
        sb.AppendLine($"                ""noSep"": ""{sepNumber }""")
        sb.AppendLine("            },")
        sb.AppendLine("            ""category"": {")
        sb.AppendLine("                ""coding"": {")
        sb.AppendLine("                    ""system"": ""http://hl7.org/fhir/v2/0074"",")
        sb.AppendLine("                    ""code"": ""RAD"",")
        sb.AppendLine("                    ""display"": ""Radiology""")
        sb.AppendLine("                },")
        sb.AppendLine("            ""status"": ""final"",")
        sb.AppendLine("            ""performer"": [")
        sb.AppendLine("                {")
        sb.AppendLine($"                    ""reference"": ""Organization/{organizationId }"",")
        sb.AppendLine($"                    ""display"": ""{organizationName }""")
        sb.AppendLine("                }")
        sb.AppendLine("            ],")
        sb.AppendLine("            ""result"": [")
        sb.AppendLine("                {")
        sb.AppendLine("                    ""resourceType"": ""Observation"",")
        sb.AppendLine("                    ""id"": ""DX00150004994364"",")
        sb.AppendLine("                    ""status"": ""final"",")
        sb.AppendLine("                    ""text"": {")
        sb.AppendLine("                        ""status"": ""final"",")
        sb.AppendLine("                        ""div"": ""CT scan kepala dengan potongan axial slice interval 5 mm, dimulai didaerah basis sampai vertex.""")
        sb.AppendLine("                    },")


        'tambahan
        sb.AppendLine("            ""valueQuantity"": {")
        sb.AppendLine("            ""value"": 1")
        sb.AppendLine("            },")
        sb.AppendLine("            ""interpretation"": {")
        sb.AppendLine("            ""coding"": {")
        sb.AppendLine("            ""system"": ""http://hl7.org/fhir/observation-interpretation"",")
        sb.AppendLine("            ""code"": ""N"",")
        sb.AppendLine("            ""display"": ""Normal""")
        sb.AppendLine("            }")
        sb.AppendLine("            },")
        sb.AppendLine("            ""referenceRange"": {")
        sb.AppendLine("            ""low"": {")
        sb.AppendLine("            ""value"": 0")
        sb.AppendLine("            },")
        sb.AppendLine("            ""high"": {")
        sb.AppendLine("            ""value"": 1")
        sb.AppendLine("            }")
        sb.AppendLine("            },")
        '''''''''''''''''''

        sb.AppendLine($"                    ""issued"": ""{tanggal_condition}"",")
        sb.AppendLine($"                    ""effectiveDateTime"": ""{tanggal_condition}"",")
        sb.AppendLine("                    ""code"": {")
        sb.AppendLine("                        ""coding"": {")
        sb.AppendLine("                            ""system"": ""http://snomed.info/sct"",")
        sb.AppendLine("                            ""code"": ""PROCx000025499"",")
        sb.AppendLine("                            ""display"": ""THORAX""")
        sb.AppendLine("                        },")
        sb.AppendLine("                        ""text"": ""THORAX""")
        sb.AppendLine("                    },")
        sb.AppendLine("                    ""performer"": {")
        sb.AppendLine($"                        ""reference"": ""Practitioner/{practitionerId_dokter}"",")
        sb.AppendLine($"                        ""display"": ""{practitionerId_doktername}""")
        sb.AppendLine("                    },")
        sb.AppendLine("                    ""image"": [")
        sb.AppendLine("                        {")
        sb.AppendLine("                            ""comment"": """",")
        sb.AppendLine("                            ""link"": {")
        sb.AppendLine("                                ""reference"": """",")
        sb.AppendLine("                                ""display"": """"")
        sb.AppendLine("                            }")
        sb.AppendLine("                        }")
        sb.AppendLine("                    ],")
        sb.AppendLine("                    ""conclusion"": ""Tak tampak kelainan radiologis pada jantung dan paru.(((((""")
        sb.AppendLine("                }")
        sb.AppendLine("            ]")
        sb.AppendLine("        }")
        sb.AppendLine("    ]")
        sb.AppendLine("},")

        ' ==================== OBSERVATION ====================
        sb.AppendLine("{")
        sb.AppendLine("    ""resource"": {")
        sb.AppendLine("        ""resourceType"": ""Observation"",")
        sb.AppendLine("        ""id"": ""DX00150004994364"",")
        sb.AppendLine("        ""status"": ""final"",")
        sb.AppendLine("        ""text"": {")
        sb.AppendLine("            ""status"": ""generated"",")
        sb.AppendLine("            ""div"": ""<div>Teknik: Radiografi toraks dalam proyeksi PA. Jantung tidak membesar, cardiothoracic ratio &lt; 50%. Aorta dan mediastinum superior tidak melebar. Trakea relatif di tengah. Kedua hilus tidak menebal. Corakan vaskular kedua paru masih baik. Tidak tampak infiltrat/nodul. Lengkung diafragma dan sinus kostofrenikus normal. Tulang-tulang yang tervisualisasi optimal kesan intak.</div>""")
        sb.AppendLine("        },")
        sb.AppendLine("        ""issued"": ""2026-04-05T13:03:37+07:00"",")
        sb.AppendLine("        ""effectiveDateTime"": ""2026-04-05T12:34:11+07:00"",")
        sb.AppendLine("        ""code"": {")
        sb.AppendLine("            ""coding"": [")
        sb.AppendLine("                {")
        sb.AppendLine("                    ""system"": ""http://snomed.info/sct"",")
        sb.AppendLine("                    ""code"": ""PROCx000025499"",")
        sb.AppendLine("                    ""display"": ""THORAX""")
        sb.AppendLine("                }")
        sb.AppendLine("            ],")
        sb.AppendLine("            ""text"": ""THORAX""")
        sb.AppendLine("        },")
        sb.AppendLine("        ""subject"": {")
        sb.AppendLine($"            ""reference"": ""Patient/{patientId}"",")
        sb.AppendLine($"            ""display"": ""{patientName}""")
        sb.AppendLine("        },")
        sb.AppendLine("        ""performer"": [")
        sb.AppendLine("            {")
        sb.AppendLine($"                ""reference"": ""Practitioner/{practitionerId_dokter}"",")
        sb.AppendLine($"                ""display"": ""{practitionerId_doktername}""")
        sb.AppendLine("            }")
        sb.AppendLine("        ],")
        sb.AppendLine("        ""conclusion"": ""Tak tampak kelainan radiologis pada jantung dan paru.""")
        sb.AppendLine("    }")
        sb.AppendLine("},")

        ' ==================== OBSERVATION ====================
        sb.AppendLine("{")
        sb.AppendLine("    ""resource"": {")
        sb.AppendLine("        ""resourceType"": ""Observation"",")
        sb.AppendLine("        ""id"": ""DX00150004994364"",")
        sb.AppendLine("        ""status"": ""final"",")
        sb.AppendLine("        ""text"": {")
        sb.AppendLine("            ""status"": ""generated"",")
        sb.AppendLine("            ""div"": ""<div>Teknik: Radiografi toraks dalam proyeksi PA. Jantung tidak membesar, cardiothoracic ratio &lt; 50%. Aorta dan mediastinum superior tidak melebar. Trakea relatif di tengah. Kedua hilus tidak menebal. Corakan vaskular kedua paru masih baik. Tidak tampak infiltrat/nodul. Lengkung diafragma dan sinus kostofrenikus normal. Tulang-tulang yang tervisualisasi optimal kesan intak.</div>""")
        sb.AppendLine("        },")
        sb.AppendLine("        ""issued"": ""2026-04-05T13:03:37+07:00"",")
        sb.AppendLine("        ""effectiveDateTime"": ""2026-04-05T12:34:11+07:00"",")
        sb.AppendLine("        ""code"": {")
        sb.AppendLine("            ""coding"": [")                              ' ← ARRAY
        sb.AppendLine("                {")
        sb.AppendLine("                    ""system"": ""http://snomed.info/sct"",")
        sb.AppendLine("                    ""code"": ""PROCx000025499"",")
        sb.AppendLine("                    ""display"": ""THORAX""")
        sb.AppendLine("                }")
        sb.AppendLine("            ],")
        sb.AppendLine("            ""text"": ""THORAX""")
        sb.AppendLine("        },")
        sb.AppendLine("        ""subject"": {")
        sb.AppendLine($"            ""reference"": ""Patient/{patientId}"",")
        sb.AppendLine($"            ""display"": ""{patientName}""")
        sb.AppendLine("        },")
        sb.AppendLine("        ""performer"": [")                              ' ← ARRAY (performer bisa array)
        sb.AppendLine("            {")
        sb.AppendLine($"                ""reference"": ""Practitioner/{practitionerId_dokter}"",")
        sb.AppendLine($"                ""display"": ""{practitionerId_doktername}""")
        sb.AppendLine("            }")
        sb.AppendLine("        ],")
        sb.AppendLine("        ""conclusion"": ""Tak tampak kelainan radiologis pada jantung dan paru.""")
        sb.AppendLine("    }")
        sb.AppendLine("},")


        ' =====================================================
        ' 7. COMPOSITION RESOURCE (Terakhir)
        ' =====================================================
        sb.AppendLine("    {")
        sb.AppendLine("      ""resource"": {")
        sb.AppendLine("        ""resourceType"": ""Composition"",")
        sb.AppendLine($"        ""id"": ""{composition_Id}"",")
        sb.AppendLine("        ""status"": ""final"",")
        sb.AppendLine("        ""type"": {")
        sb.AppendLine("          ""coding"": [{""system"": ""http://loinc.org"", ""code"": ""81218-0""}],")
        sb.AppendLine("          ""text"": ""Discharge Summary""")
        sb.AppendLine("        },")
        sb.AppendLine("        ""subject"": {")
        sb.AppendLine($"          ""reference"": ""Patient/{patientId}"",")
        sb.AppendLine($"          ""display"": ""{EscapeJson(patientName)}""")
        sb.AppendLine("        },")
        sb.AppendLine("        ""encounter"": {")
        sb.AppendLine($"          ""reference"": ""Encounter/{composition_encounterId}""")
        sb.AppendLine("        },")
        sb.AppendLine($"        ""date"": ""{composition_tanggal}"",")
        sb.AppendLine("        ""author"": [{")
        sb.AppendLine($"          ""reference"": ""Practitioner/{practitionerId_dokter}"",")
        sb.AppendLine($"          ""display"": ""{EscapeJson(practitionerId_doktername)}""")
        sb.AppendLine("        }],")
        sb.AppendLine("        ""title"": ""Discharge Summary"",")
        sb.AppendLine("        ""confidentiality"": ""N"",")
        sb.AppendLine("        ""section"": {")

        ' Section 0 - Reason for admission
        sb.AppendLine("          ""0"": {")
        sb.AppendLine("            ""title"": ""Reason for admission"",")
        sb.AppendLine("            ""code"": {")
        sb.AppendLine("              ""coding"": [{""system"": ""http://loinc.org"", ""code"": ""29299-5"", ""display"": ""Reason for visit Narrative""}]")
        sb.AppendLine("            },")
        sb.AppendLine("            ""text"": {""status"": ""additional"", ""div"": """ & EscapeJson(section0) & """},")
        sb.AppendLine("            ""entry"": []")
        sb.AppendLine("          },")

        ' Section 1 - Chief complaint
        sb.AppendLine("          ""1"": {")
        sb.AppendLine("            ""title"": ""Chief complaint"",")
        sb.AppendLine("            ""code"": {")
        sb.AppendLine("              ""coding"": [{""system"": ""http://loinc.org"", ""code"": ""10154-3"", ""display"": ""Chief complaint Narrative""}]")
        sb.AppendLine("            },")
        sb.AppendLine("            ""text"": {""status"": ""additional"", ""div"": """ & EscapeJson(section1) & """},")
        sb.AppendLine("            ""entry"": []")
        sb.AppendLine("          },")
        '""{EscapeJson(section2)}"""
        ' Section 2 - Admission diagnosis
        sb.AppendLine("          ""2"": {")
        sb.AppendLine("            ""title"": ""Admission diagnosis"",")
        sb.AppendLine("            ""code"": {")
        sb.AppendLine("              ""coding"": [{""system"": ""http://loinc.org"", ""code"": ""42347-5"", ""display"": ""Admission diagnosis Narrative""}]")
        sb.AppendLine("            },")
        sb.AppendLine("            ""text"": {""status"": ""additional"", ""div"": """ & EscapeJson(section2) & """},")
        sb.AppendLine("            ""entry"": []")
        'sb.AppendLine("            ""entry"": [{""reference"": """ & conditionId & """}]")
        sb.AppendLine("          },")

        ' Section 3 - Discharge diagnosis
        sb.AppendLine("          ""3"": {")
        sb.AppendLine("            ""title"": ""Discharge diagnosis"",")
        sb.AppendLine("            ""code"": {")
        sb.AppendLine("              ""coding"": [{""system"": ""http://loinc.org"", ""code"": ""78375-3"", ""display"": ""Discharge diagnosis Narrative""}]")
        sb.AppendLine("            },")
        sb.AppendLine("            ""text"": {""status"": ""additional"", ""div"": ""<div>" & EscapeJson(icd10_kode_dokter) & "</div>""},")
        sb.AppendLine("            ""entry"": [{""reference"": """ & EscapeJson(icd10_name_dokter) & """}]")
        sb.AppendLine("          },")

        ' Section 4 - Medications on Discharge
        sb.AppendLine("          ""4"": {")
        sb.AppendLine("            ""title"": ""Medications on Discharge"",")
        sb.AppendLine("            ""code"": {")
        sb.AppendLine("              ""coding"": [{""system"": ""http://loinc.org"", ""code"": ""75311-1"", ""display"": ""Hospital discharge medications Narrative""}]")
        sb.AppendLine("            },")
        sb.AppendLine("            ""text"": {""status"": ""additional"", ""div"": """ & EscapeJson(section4) & """},")
        sb.AppendLine("            ""mode"": ""working"",")
        'sb.AppendLine("            ""entry"": []")
        sb.AppendLine("            ""entry"": [{""reference"": """ & "TES OBAT" & """}]")
        sb.AppendLine("          },")

        '"entry" [
        '      {
        '        "reference": "MedicationRequest/1002R005-3204086-2-ITM0002571"
        '      },
        '      {
        '        "reference": "MedicationRequest/1002R005-3204086-2-ITM0005231"
        '      }
        '    ]


        ' Section 5 - Plan of care
        sb.AppendLine("          ""5"": {")
        sb.AppendLine("            ""title"": ""Plan of care"",")
        sb.AppendLine("            ""code"": {")
        sb.AppendLine("              ""coding"": [{""system"": ""http://loinc.org"", ""code"": ""18776-5"", ""display"": ""Plan of care""}]")
        sb.AppendLine("            },")
        sb.AppendLine("            ""text"": {""status"": ""additional"", ""div"": """ & EscapeJson(section5) & """},")
        sb.AppendLine("            ""mode"": ""working"",")
        sb.AppendLine("            ""entry"": []")
        sb.AppendLine("          },")

        ' Section 7 - Known allergies
        sb.AppendLine("          ""7"": {")
        sb.AppendLine("            ""title"": ""Known allergies"",")
        sb.AppendLine("            ""code"": {")
        sb.AppendLine("              ""coding"": [{""system"": ""http://loinc.org"", ""code"": ""48765-2"", ""display"": ""Allergies and adverse reactions""}]")
        sb.AppendLine("            },")
        sb.AppendLine("            ""text"": {""status"": ""additional"", ""div"": """ & EscapeJson(section7) & """},")
        sb.AppendLine("            ""entry"": []")
        'sb.AppendLine("            ""entry"": [{""reference"": """ & allergyId & """}]")
        sb.AppendLine("          }")
        sb.AppendLine("        }")
        sb.AppendLine("      }")
        sb.AppendLine("    }")
        sb.AppendLine("  ]")
        sb.AppendLine("}")

        Return sb.ToString()
    End Function
    Public Function BuildBundleWithComposition(
    bundleId As String,
    sepNumber As String,
    patientId As String,
    patientName As String,
    encounterId As String,
    practitionerId As String,
    practitionerName As String,
    compositionId As String,
    diagnosadokter_name As String,
    section2 As String,
    section4 As String,
    section5 As String,
    section7 As String,
    conditionId_section2 As String,
    allergyId As String,
    tanggal As String,
    section0 As String,
    section1 As String,
    section4EntryRefs As List(Of String)) As String

        Dim sb As New StringBuilder()

        ' =====================================================
        ' BUNDLE AWAL
        ' =====================================================
        sb.AppendLine("{")
        sb.AppendLine("  ""resourceType"": ""Bundle"",")
        sb.AppendLine($"  ""id"": ""{bundleId}"",")
        sb.AppendLine("  ""meta"": {")
        sb.AppendLine($"    ""lastUpdated"": ""{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}""")
        sb.AppendLine("  },")
        sb.AppendLine("  ""identifier"": {")
        sb.AppendLine("    ""system"": ""sep"",")
        sb.AppendLine($"    ""value"": ""{sepNumber}""")
        sb.AppendLine("  },")
        sb.AppendLine("  ""type"": ""document"",")
        sb.AppendLine("  ""entry"": [")
        sb.AppendLine("    {")
        sb.AppendLine("      ""resource"": {")

        ' =====================================================
        ' COMPOSITION RESOURCE
        ' =====================================================
        sb.AppendLine("        ""resourceType"": ""Composition"",")
        sb.AppendLine($"        ""id"": ""{compositionId}"",")
        sb.AppendLine("        ""status"": ""final"",")
        sb.AppendLine("        ""type"": {")
        sb.AppendLine("          ""coding"": [")
        sb.AppendLine("            {")
        sb.AppendLine("              ""system"": ""http://loinc.org"",")
        sb.AppendLine("              ""code"": ""81218-0""")
        sb.AppendLine("            }")
        sb.AppendLine("          ],")
        sb.AppendLine("          ""text"": ""Discharge Summary""")
        sb.AppendLine("        },")
        sb.AppendLine("        ""subject"": {")
        sb.AppendLine($"          ""reference"": ""Patient/{patientId}"",")
        sb.AppendLine($"          ""display"": ""{EscapeJson(patientName)}""")
        sb.AppendLine("        },")
        sb.AppendLine("        ""encounter"": {")
        sb.AppendLine($"          ""reference"": ""{encounterId}""")
        sb.AppendLine("        },")
        sb.AppendLine($"        ""date"": ""{tanggal}"",")
        sb.AppendLine("        ""author"": [")
        sb.AppendLine("          {")
        sb.AppendLine($"            ""reference"": ""Practitioner/{practitionerId}"",")
        sb.AppendLine($"            ""display"": ""{EscapeJson(practitionerName)}""")
        sb.AppendLine("          }")
        sb.AppendLine("        ],")
        sb.AppendLine("        ""title"": ""Discharge Summary"",")
        sb.AppendLine("        ""confidentiality"": ""N"",")
        sb.AppendLine("        ""section"": {")

        ' Section 0 - Reason for admission
        sb.AppendLine("          ""0"": {")
        sb.AppendLine("            ""title"": ""Reason for admission"",")
        sb.AppendLine("            ""code"": {")
        sb.AppendLine("              ""coding"": [")
        sb.AppendLine("                {")
        sb.AppendLine("                  ""system"": ""http://loinc.org"",")
        sb.AppendLine("                  ""code"": ""29299-5"",")
        sb.AppendLine("                  ""display"": ""Reason for visit Narrative""")
        sb.AppendLine("                }")
        sb.AppendLine("              ]")
        sb.AppendLine("            },")
        sb.AppendLine("            ""text"": {")
        sb.AppendLine("              ""status"": ""additional"",")
        sb.AppendLine("              ""div"": """ & section0 & """")
        sb.AppendLine("            },")
        sb.AppendLine("            ""entry"": []")
        sb.AppendLine("          },")

        ' Section 1 - Chief complaint
        sb.AppendLine("          ""1"": {")
        sb.AppendLine("            ""title"": ""Chief complaint"",")
        sb.AppendLine("            ""code"": {")
        sb.AppendLine("              ""coding"": [")
        sb.AppendLine("                {")
        sb.AppendLine("                  ""system"": ""http://loinc.org"",")
        sb.AppendLine("                  ""code"": ""10154-3"",")
        sb.AppendLine("                  ""display"": ""Chief complaint Narrative""")
        sb.AppendLine("                }")
        sb.AppendLine("              ]")
        sb.AppendLine("            },")
        sb.AppendLine("            ""text"": {")
        sb.AppendLine("              ""status"": ""additional"",")
        sb.AppendLine("              ""div"": """ & section1 & """")
        sb.AppendLine("            },")
        sb.AppendLine("            ""entry"": []")
        sb.AppendLine("          },")

        ' Section 2 - Admission diagnosis
        sb.AppendLine("          ""2"": {")
        sb.AppendLine("            ""title"": ""Admission diagnosis"",")
        sb.AppendLine("            ""code"": {")
        sb.AppendLine("              ""coding"": [")
        sb.AppendLine("                {")
        sb.AppendLine("                  ""system"": ""http://loinc.org"",")
        sb.AppendLine("                  ""code"": ""42347-5"",")
        sb.AppendLine("                  ""display"": ""Admission diagnosis Narrative""")
        sb.AppendLine("                }")
        sb.AppendLine("              ]")
        sb.AppendLine("            },")
        sb.AppendLine("            ""text"": {")
        sb.AppendLine("              ""status"": ""additional"",")
        sb.AppendLine($"              ""div"": ""{EscapeJson(section2)}""")
        sb.AppendLine("            },")
        sb.AppendLine("            ""entry"": [")
        sb.AppendLine("              {")
        sb.AppendLine($"                ""reference"": ""{conditionId_section2}""")
        sb.AppendLine("              }")
        sb.AppendLine("            ]")
        sb.AppendLine("          },")

        '' Section 3 - Discharge diagnosis
        'sb.AppendLine("          ""3"": {")
        'sb.AppendLine("            ""title"": ""Discharge diagnosis"",")
        'sb.AppendLine("            ""code"": {")
        'sb.AppendLine("              ""coding"": [")
        'sb.AppendLine("                {")
        'sb.AppendLine("                  ""system"": ""http://loinc.org"",")
        'sb.AppendLine("                  ""code"": ""78375-3"",")
        'sb.AppendLine("                  ""display"": ""Discharge diagnosis Narrative""")
        'sb.AppendLine("                }")
        'sb.AppendLine("              ]")
        'sb.AppendLine("            },")
        'sb.AppendLine("            ""text"": {")
        'sb.AppendLine("              ""status"": ""additional"",")
        'sb.AppendLine($"              ""div"": ""<div>{EscapeJson(diagnosadokter_name)}</div>""")
        'sb.AppendLine("            },")
        'sb.AppendLine("            ""entry"": [")
        'sb.AppendLine("              {")
        'sb.AppendLine($"                ""reference"": ""{conditionId_section3}""")
        'sb.AppendLine("              }")
        'sb.AppendLine("            ]")
        'sb.AppendLine("          },")

        ' Section 4 - Medications on Discharge
        sb.AppendLine("          ""4"": {")
        sb.AppendLine("            ""title"": ""Medications on Discharge"",")
        sb.AppendLine("            ""code"": {")
        sb.AppendLine("              ""coding"": [")
        sb.AppendLine("                {")
        sb.AppendLine("                  ""system"": ""http://loinc.org"",")
        sb.AppendLine("                  ""code"": ""75311-1"",")
        sb.AppendLine("                  ""display"": ""Hospital discharge medications Narrative""")
        sb.AppendLine("                }")
        sb.AppendLine("              ]")
        sb.AppendLine("            },")
        sb.AppendLine("            ""text"": {")
        sb.AppendLine("              ""status"": ""additional"",")
        sb.AppendLine($"              ""div"": ""<div>{EscapeJson(section4)}</div>""")
        sb.AppendLine("            },")
        sb.AppendLine("            ""mode"": ""working"",")
        ' ========== MULTIPLE ENTRIES ==========
        If section4EntryRefs IsNot Nothing AndAlso section4EntryRefs.Count > 0 Then
            sb.AppendLine("            ""entry"": [")
            For i As Integer = 0 To section4EntryRefs.Count - 1
                sb.AppendLine("              {")
                sb.AppendLine($"                ""reference"": ""{section4EntryRefs(i)}""")
                If i < section4EntryRefs.Count - 1 Then
                    sb.AppendLine("              },")
                Else
                    sb.AppendLine("              }")
                End If
            Next
            sb.AppendLine("            ]")
        Else
            sb.AppendLine("            ""entry"": []")
        End If
        sb.AppendLine("          },")

        ' Section 5 - Plan of care
        sb.AppendLine("          ""5"": {")
        sb.AppendLine("            ""title"": ""Plan of care"",")
        sb.AppendLine("            ""code"": {")
        sb.AppendLine("              ""coding"": [")
        sb.AppendLine("                {")
        sb.AppendLine("                  ""system"": ""http://loinc.org"",")
        sb.AppendLine("                  ""code"": ""18776-5"",")
        sb.AppendLine("                  ""display"": ""Plan of care""")
        sb.AppendLine("                }")
        sb.AppendLine("              ]")
        sb.AppendLine("            },")
        sb.AppendLine("            ""text"": {")
        sb.AppendLine("              ""status"": ""additional"",")
        sb.AppendLine($"              ""div"": ""{EscapeJson(section5)}""")
        sb.AppendLine("            },")
        sb.AppendLine("            ""mode"": ""working"",")
        sb.AppendLine("            ""entry"": []")
        sb.AppendLine("          },")

        ' Section 7 - Known allergies
        sb.AppendLine("          ""7"": {")
        sb.AppendLine("            ""title"": ""Known allergies"",")
        sb.AppendLine("            ""code"": {")
        sb.AppendLine("              ""coding"": [")
        sb.AppendLine("                {")
        sb.AppendLine("                  ""system"": ""http://loinc.org"",")
        sb.AppendLine("                  ""code"": ""48765-2"",")
        sb.AppendLine("                  ""display"": ""Allergies and adverse reactions""")
        sb.AppendLine("                }")
        sb.AppendLine("              ]")
        sb.AppendLine("            },")
        sb.AppendLine("            ""text"": {")
        sb.AppendLine("              ""status"": ""additional"",")
        sb.AppendLine($"              ""div"": ""{EscapeJson(section7)}""")
        sb.AppendLine("            },")
        sb.AppendLine("            ""entry"": []")
        'sb.AppendLine("            ""entry"": [")
        'sb.AppendLine("              {")
        'sb.AppendLine($"                ""reference"": ""{allergyId}""")
        'sb.AppendLine("              }")
        'sb.AppendLine("            ]")
        sb.AppendLine("          }")
        sb.AppendLine("        }")
        sb.AppendLine("      }")
        sb.AppendLine("    }")
        sb.AppendLine("  ]")
        sb.AppendLine("}")

        Return sb.ToString()
    End Function
    Private Function BuildBundleWithPatient(
    bundleId As String,
    sepNumber As String,
    patientId As String,
    name As String,
    gender As String,
    birthDate As String,
    nik As String,
    mrNumber As String,
    bpjsNumber As String,
    address As String,
    phone As String,
    organizationId As String,
    organizationName As String) As String

        Dim sb As New StringBuilder()

        ' =====================================================
        ' BUNDLE AWAL
        ' =====================================================
        sb.AppendLine("{")
        sb.AppendLine("  ""resourceType"": ""Bundle"",")
        sb.AppendLine($"  ""id"": ""{bundleId}"",")
        sb.AppendLine("  ""meta"": {")
        sb.AppendLine($"    ""lastUpdated"": ""{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}""")
        sb.AppendLine("  },")
        sb.AppendLine("  ""identifier"": {")
        sb.AppendLine("    ""system"": ""sep"",")
        sb.AppendLine($"    ""value"": ""{sepNumber}""")
        sb.AppendLine("  },")
        sb.AppendLine("  ""type"": ""document"",")
        sb.AppendLine("  ""entry"": [")
        sb.AppendLine("    {")
        sb.AppendLine("      ""resource"": {")

        ' =====================================================
        ' PATIENT RESOURCE
        ' =====================================================
        sb.AppendLine("        ""resourceType"": ""Patient"",")
        sb.AppendLine($"        ""id"": ""{patientId}"",")
        sb.AppendLine("        ""identifier"": [")
        sb.AppendLine("          {")
        sb.AppendLine("            ""use"": ""usual"",")
        sb.AppendLine("            ""type"": {")
        sb.AppendLine("              ""coding"": [")
        sb.AppendLine("                {")
        sb.AppendLine("                  ""system"": ""http://hl7.org/fhir/v2/0203"",")
        sb.AppendLine("                  ""code"": ""MR""")
        sb.AppendLine("                }")
        sb.AppendLine("              ]")
        sb.AppendLine("            },")
        sb.AppendLine($"            ""value"": ""{EscapeJson(mrNumber)}"",")
        sb.AppendLine("            ""assigner"": {")
        sb.AppendLine($"              ""display"": ""{EscapeJson(organizationName)}""")
        sb.AppendLine("            }")
        sb.AppendLine("          },")
        sb.AppendLine("          {")
        sb.AppendLine("            ""use"": ""official"",")
        sb.AppendLine("            ""type"": {")
        sb.AppendLine("              ""coding"": [")
        sb.AppendLine("                {")
        sb.AppendLine("                  ""system"": ""http://hl7.org/fhir/v2/0203"",")
        sb.AppendLine("                  ""code"": ""MB""")
        sb.AppendLine("                }")
        sb.AppendLine("              ]")
        sb.AppendLine("            },")
        sb.AppendLine($"            ""value"": ""{EscapeJson(bpjsNumber)}"",")
        sb.AppendLine("            ""assigner"": {")
        sb.AppendLine("              ""display"": ""BPJS KESEHATAN""")
        sb.AppendLine("            }")
        sb.AppendLine("          },")
        sb.AppendLine("          {")
        sb.AppendLine("            ""use"": ""official"",")
        sb.AppendLine("            ""type"": {")
        sb.AppendLine("              ""coding"": [")
        sb.AppendLine("                {")
        sb.AppendLine("                  ""system"": ""http://hl7.org/fhir/v2/0203"",")
        sb.AppendLine("                  ""code"": ""NNIDN""")
        sb.AppendLine("                }")
        sb.AppendLine("              ]")
        sb.AppendLine("            },")
        sb.AppendLine($"            ""value"": ""{EscapeJson(nik)}"",")
        sb.AppendLine("            ""assigner"": {")
        sb.AppendLine("              ""display"": ""KEMENDAGRI""")
        sb.AppendLine("            }")
        sb.AppendLine("          }")
        sb.AppendLine("        ],")
        sb.AppendLine("        ""active"": true,")
        sb.AppendLine("        ""name"": [")
        sb.AppendLine("          {")
        sb.AppendLine("            ""use"": ""official"",")
        sb.AppendLine($"            ""text"": ""{EscapeJson(name)}""")
        sb.AppendLine("          }")
        sb.AppendLine("        ],")
        sb.AppendLine("        ""maritalStatus"": {")
        sb.AppendLine("          ""coding"": [")
        sb.AppendLine("            {")
        sb.AppendLine("              ""system"": ""http://hl7.org/fhir/v3/MaritalStatus"",")
        sb.AppendLine("              ""code"": ""U""")
        sb.AppendLine("            }")
        sb.AppendLine("          ]")
        sb.AppendLine("        },")
        sb.AppendLine("        ""telecom"": [")
        sb.AppendLine("          {")
        sb.AppendLine("            ""system"": ""phone"",")
        sb.AppendLine("            ""value"": """",")
        sb.AppendLine("            ""use"": ""work""")
        sb.AppendLine("          },")
        sb.AppendLine("          {")
        sb.AppendLine("            ""system"": ""phone"",")
        sb.AppendLine($"            ""value"": ""{EscapeJson(phone)}"",")
        sb.AppendLine("            ""use"": ""mobile""")
        sb.AppendLine("          },")
        sb.AppendLine("          {")
        sb.AppendLine("            ""system"": ""phone"",")
        sb.AppendLine("            ""value"": ""TDK ADA"",")
        sb.AppendLine("            ""use"": ""home""")
        sb.AppendLine("          }")
        sb.AppendLine("        ],")
        sb.AppendLine($"        ""gender"": ""{gender}"",")
        sb.AppendLine($"        ""birthDate"": ""{birthDate}"",")
        sb.AppendLine("        ""deceasedBoolean"": false,")
        sb.AppendLine("        ""address"": [")
        sb.AppendLine("          {")
        sb.AppendLine("            ""line"": [")
        sb.AppendLine($"              ""{EscapeJson(address)}""")
        sb.AppendLine("            ],")
        sb.AppendLine("            ""city"": """",")
        sb.AppendLine("            ""district"": """",")
        sb.AppendLine("            ""state"": """",")
        sb.AppendLine("            ""postalCode"": """",")
        sb.AppendLine($"            ""text"": ""{EscapeJson(address)}"",")
        sb.AppendLine("            ""use"": ""home"",")
        sb.AppendLine("            ""type"": ""both""")
        sb.AppendLine("          }")
        sb.AppendLine("        ],")
        sb.AppendLine("        ""managingOrganization"": {")
        sb.AppendLine($"          ""reference"": ""Organization/{organizationId}"",")
        sb.AppendLine($"          ""display"": ""{EscapeJson(organizationName)}""")
        sb.AppendLine("        }")
        sb.AppendLine("      }")
        sb.AppendLine("    }")
        sb.AppendLine("  ]")
        sb.AppendLine("}")

        Return sb.ToString()
    End Function
    Public Function BuildBundleWithProcedure(
    bundleId As String,
    sepNumber As String,
    procedureId As String,
    patientId As String,
    patientName As String,
    encounterId As String,
    encounterName As String,
    practitionerId As String,
    practitionerName As String,
    procedureCode As String,
    procedureDisplay As String,
    procedureDate As String,
    categoryCode As String,
    categoryDisplay As String,
    bodySiteCode As String,
    bodySiteDisplay As String,
    notes As String) As String

        Dim sb As New StringBuilder()

        ' =====================================================
        ' BUNDLE AWAL
        ' =====================================================
        sb.AppendLine("{")
        sb.AppendLine("  ""resourceType"": ""Bundle"",")
        sb.AppendLine($"  ""id"": ""{bundleId}"",")
        sb.AppendLine("  ""meta"": {")
        sb.AppendLine($"    ""lastUpdated"": ""{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}""")
        sb.AppendLine("  },")
        sb.AppendLine("  ""identifier"": {")
        sb.AppendLine("    ""system"": ""sep"",")
        sb.AppendLine($"    ""value"": ""{sepNumber}""")
        sb.AppendLine("  },")
        sb.AppendLine("  ""type"": ""document"",")
        sb.AppendLine("  ""entry"": [")
        sb.AppendLine("    {")
        sb.AppendLine("      ""resource"": {")

        ' =====================================================
        ' PROCEDURE RESOURCE
        ' =====================================================
        sb.AppendLine("        ""resourceType"": ""Procedure"",")
        sb.AppendLine($"        ""id"": ""{procedureId}"",")
        sb.AppendLine("        ""text"": {")
        sb.AppendLine("          ""status"": ""generated"",")
        sb.AppendLine("          ""div"": ""<div>Generated Narrative with Details</div>""")
        sb.AppendLine("        },")
        sb.AppendLine("        ""status"": ""completed"",")
        sb.AppendLine("        ""category"": {")
        sb.AppendLine("          ""coding"": [")
        sb.AppendLine("            {")
        sb.AppendLine("              ""system"": ""http://snomed.info/sct"",")
        sb.AppendLine($"              ""code"": ""{EscapeJson(categoryCode)}"",")
        sb.AppendLine($"              ""display"": ""{EscapeJson(categoryDisplay)}""")
        sb.AppendLine("            }")
        sb.AppendLine("          ],")
        sb.AppendLine("          ""text"": """"")
        sb.AppendLine("        },")
        sb.AppendLine("        ""code"": {")
        sb.AppendLine("          ""coding"": [")
        sb.AppendLine("            {")
        sb.AppendLine("              ""system"": ""http://snomed.info/sct"",")
        sb.AppendLine($"              ""code"": ""{EscapeJson(procedureCode)}"",")
        sb.AppendLine($"              ""display"": ""{EscapeJson(procedureDisplay)}""")
        sb.AppendLine("            }")
        sb.AppendLine("          ]")
        sb.AppendLine("        },")
        sb.AppendLine("        ""subject"": {")
        sb.AppendLine($"          ""reference"": ""Patient/{patientId}"",")
        sb.AppendLine($"          ""display"": ""{EscapeJson(patientName)}""")
        sb.AppendLine("        },")
        sb.AppendLine("        ""context"": {")
        sb.AppendLine($"          ""reference"": ""Encounter/{encounterId}"",")
        sb.AppendLine($"          ""display"": ""{EscapeJson(encounterName)} encounter""")
        sb.AppendLine("        },")
        sb.AppendLine("        ""performedPeriod"": {")
        sb.AppendLine($"          ""start"": ""{procedureDate}"",")
        sb.AppendLine($"          ""end"": ""{procedureDate}""")
        sb.AppendLine("        },")
        sb.AppendLine("        ""performer"": [")
        sb.AppendLine("          {")
        sb.AppendLine("            ""role"": {")
        sb.AppendLine("              ""coding"": [")
        sb.AppendLine("                {")
        sb.AppendLine("                  ""system"": ""http://snomed.info/sct"",")
        sb.AppendLine("                  ""code"": """",")
        sb.AppendLine("                  ""display"": """"")
        sb.AppendLine("                }")
        sb.AppendLine("              ]")
        sb.AppendLine("            },")
        sb.AppendLine("            ""actor"": {")
        sb.AppendLine($"              ""reference"": ""Practitioner/{practitionerId}"",")
        sb.AppendLine($"              ""display"": ""{EscapeJson(practitionerName)}""")
        sb.AppendLine("            }")
        sb.AppendLine("          }")
        sb.AppendLine("        ],")
        sb.AppendLine("        ""reasonCode"": [")
        sb.AppendLine("          {")
        sb.AppendLine("            ""text"": ""DiagnosticReport/c715d143-6695-43d8-8605-46e431182fc1""")
        sb.AppendLine("          }")
        sb.AppendLine("        ],")
        sb.AppendLine("        ""bodySite"": [")
        sb.AppendLine("          {")
        sb.AppendLine("            ""coding"": [")
        sb.AppendLine("              {")
        sb.AppendLine("                ""system"": ""http://snomed.info/sct"",")
        sb.AppendLine($"                ""code"": ""{EscapeJson(bodySiteCode)}"",")
        sb.AppendLine($"                ""display"": ""{EscapeJson(bodySiteDisplay)}""")
        sb.AppendLine("              }")
        sb.AppendLine("            ]")
        sb.AppendLine("          }")
        sb.AppendLine("        ],")
        sb.AppendLine("        ""focalDevice"": [")
        sb.AppendLine("          {")
        sb.AppendLine("            ""action"": {")
        sb.AppendLine("              ""coding"": [")
        sb.AppendLine("                {")
        sb.AppendLine("                  ""system"": ""http://hl7.org/fhir/device-action"",")
        sb.AppendLine("                  ""code"": ""implanted""")
        sb.AppendLine("                }")
        sb.AppendLine("              ]")
        sb.AppendLine("            },")
        sb.AppendLine("            ""manipulated"": {")
        sb.AppendLine("              ""reference"": ""Device/bd9b7f07-602f-416c-ab84-778a9761095b"",")
        sb.AppendLine("              ""display"": """"")
        sb.AppendLine("            }")
        sb.AppendLine("          }")
        sb.AppendLine("        ],")
        sb.AppendLine("        ""note"": [")
        sb.AppendLine("          {")
        sb.AppendLine($"            ""text"": ""{EscapeJson(notes)}""")
        sb.AppendLine("          }")
        sb.AppendLine("        ]")
        sb.AppendLine("      }")
        sb.AppendLine("    }")
        sb.AppendLine("  ]")
        sb.AppendLine("}")

        Return sb.ToString()
    End Function
    Public Function BuildBundleWithOrganization(
    bundleId As String,
    sepNumber As String,
    organizationId As String,
    organizationName As String,
    bpjsCode As String,
    kemkesCode As String,
    phone As String,
    address As String,
    city As String,
    postalCode As String) As String

        Dim sb As New StringBuilder()

        ' =====================================================
        ' BUNDLE AWAL
        ' =====================================================
        sb.AppendLine("{")
        sb.AppendLine("  ""resourceType"": ""Bundle"",")
        sb.AppendLine($"  ""id"": ""{bundleId}"",")
        sb.AppendLine("  ""meta"": {")
        sb.AppendLine($"    ""lastUpdated"": ""{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}""")
        sb.AppendLine("  },")
        sb.AppendLine("  ""identifier"": {")
        sb.AppendLine("    ""system"": ""sep"",")
        sb.AppendLine($"    ""value"": ""{sepNumber}""")
        sb.AppendLine("  },")
        sb.AppendLine("  ""type"": ""document"",")
        sb.AppendLine("  ""entry"": [")
        sb.AppendLine("    {")
        sb.AppendLine("      ""resource"": {")

        ' =====================================================
        ' ORGANIZATION RESOURCE
        ' =====================================================
        sb.AppendLine("        ""resourceType"": ""Organization"",")
        sb.AppendLine($"        ""id"": ""{organizationId}"",")
        sb.AppendLine("        ""identifier"": [")
        sb.AppendLine("          {")
        sb.AppendLine("            ""use"": ""official"",")
        sb.AppendLine("            ""system"": ""urn:oid:bpjs"",")
        sb.AppendLine($"            ""value"": ""{EscapeJson(bpjsCode)}""")
        sb.AppendLine("          },")
        sb.AppendLine("          {")
        sb.AppendLine("            ""use"": ""official"",")
        sb.AppendLine("            ""system"": ""urn:oid:kemkes"",")
        sb.AppendLine($"            ""value"": ""{EscapeJson(kemkesCode)}""")
        sb.AppendLine("          }")
        sb.AppendLine("        ],")
        sb.AppendLine("        ""active"": true,")
        sb.AppendLine("        ""type"": [")
        sb.AppendLine("          {")
        sb.AppendLine("            ""coding"": [")
        sb.AppendLine("              {")
        sb.AppendLine("                ""system"": ""http://hl7.org/fhir/organization-type"",")
        sb.AppendLine("                ""code"": ""prov"",")
        sb.AppendLine("                ""display"": ""Healthcare Provider""")
        sb.AppendLine("              }")
        sb.AppendLine("            ]")
        sb.AppendLine("          }")
        sb.AppendLine("        ],")
        sb.AppendLine($"        ""name"": ""{EscapeJson(organizationName)}"",")
        sb.AppendLine("        ""alias"": [")
        sb.AppendLine($"          ""{EscapeJson(organizationName)}""")
        sb.AppendLine("        ],")
        sb.AppendLine("        ""telecom"": [")
        sb.AppendLine("          {")
        sb.AppendLine("            ""system"": ""phone"",")
        sb.AppendLine($"            ""value"": ""{EscapeJson(phone)}"",")
        sb.AppendLine("            ""use"": ""work""")
        sb.AppendLine("          }")
        sb.AppendLine("        ],")
        sb.AppendLine("        ""address"": [")
        sb.AppendLine("          {")
        sb.AppendLine("            ""use"": ""work"",")
        sb.AppendLine("            ""type"": ""both"",")
        sb.AppendLine("            ""text"": """ & EscapeJson(address) & """,")
        sb.AppendLine("            ""line"": [")
        sb.AppendLine($"              ""{EscapeJson(address)}""")
        sb.AppendLine("            ],")
        sb.AppendLine($"            ""city"": ""{EscapeJson(city)}"",")
        sb.AppendLine("            ""state"": ""DKI Jakarta"",")
        sb.AppendLine($"            ""postalCode"": ""{postalCode}"",")
        sb.AppendLine("            ""country"": ""ID""")
        sb.AppendLine("          }")
        sb.AppendLine("        ],")
        sb.AppendLine("        ""contact"": [")
        sb.AppendLine("          {")
        sb.AppendLine("            ""purpose"": {")
        sb.AppendLine("              ""coding"": [")
        sb.AppendLine("                {")
        sb.AppendLine("                  ""system"": ""http://hl7.org/fhir/contactentity-type"",")
        sb.AppendLine("                  ""code"": ""ADMIN""")
        sb.AppendLine("                }")
        sb.AppendLine("              ]")
        sb.AppendLine("            },")
        sb.AppendLine("            ""name"": {")
        sb.AppendLine("              ""text"": ""Administrator""")
        sb.AppendLine("            },")
        sb.AppendLine("            ""telecom"": [")
        sb.AppendLine("              {")
        sb.AppendLine("                ""system"": ""phone"",")
        sb.AppendLine($"                ""value"": ""{EscapeJson(phone)}"",")
        sb.AppendLine("                ""use"": ""work""")
        sb.AppendLine("              }")
        sb.AppendLine("            ]")
        sb.AppendLine("          }")
        sb.AppendLine("        ]")
        sb.AppendLine("      }")
        sb.AppendLine("    }")
        sb.AppendLine("  ]")
        sb.AppendLine("}")

        Return sb.ToString()
    End Function
    Private Function EscapeJson(text As String) As String
        If String.IsNullOrEmpty(text) Then Return ""

        With text
            Return .Replace("\", "\\") _
               .Replace("""", "\""") _
               .Replace(vbCr, "\r") _
               .Replace(vbLf, "\n") _
               .Replace(vbTab, "\t")
        End With
    End Function
    Private Function fn_UDD(ByVal Parameter1 As String, ByVal Parameter2 As String, ByVal Parameter3 As String) As String
        fn_UDD = Parameter1 & "-" & Parameter2 & "-" & Parameter3 & "-" & Guid.NewGuid().ToString()
    End Function
    Public Function CreateCompositionResource(patientId As String, encounterId As String, practitionerId As String,
                                          compositionId As String, icdCode As String, icdDisplay As String,
                                          admissionDiagnosis As String, dischargeDiagnosis As String,
                                          planOfCare As String, allergies As String) As JObject

        Dim composition As New JObject()
        composition("resourceType") = "Composition"
        composition("id") = compositionId
        composition("status") = "final"

        ' Type
        Dim typeObj As New JObject()
        Dim typeCoding As New JArray()
        Dim typeCodingItem As New JObject()
        typeCodingItem("system") = "http://loinc.org"
        typeCodingItem("code") = "81218-0"
        typeCoding.Add(typeCodingItem)
        typeObj("coding") = typeCoding
        typeObj("text") = "Discharge Summary"
        composition("type") = typeObj

        ' Subject
        Dim subject As New JObject()
        subject("reference") = $"Patient/{patientId}"
        subject("display") = "" ' Isi dengan nama pasien
        composition("subject") = subject

        ' Encounter
        Dim encounterRef As New JObject()
        encounterRef("reference") = $"Encounter/{encounterId}"
        composition("encounter") = encounterRef

        composition("date") = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)

        ' Author
        Dim authorArray As New JArray()
        Dim author As New JObject()
        author("reference") = $"Practitioner/{practitionerId}"
        author("display") = "" ' Isi dengan nama dokter
        authorArray.Add(author)
        composition("author") = authorArray

        composition("title") = "Discharge Summary"
        composition("confidentiality") = "N"

        ' Sections
        Dim sections As New JObject()

        ' Section 0 - Reason for admission
        sections("0") = CreateSection("Reason for admission", "29299-5", "Reason for visit Narrative", "<div></div>", Nothing)

        ' Section 1 - Chief complaint
        sections("1") = CreateSection("Chief complaint", "10154-3", "Chief complaint Narrative", "<div></div>", Nothing)

        ' Section 2 - Admission diagnosis
        Dim conditionId As String = Guid.NewGuid().ToString()
        sections("2") = CreateSection("Admission diagnosis", "42347-5", "Admission diagnosis Narrative", $"<div>{admissionDiagnosis}</div>", conditionId)

        ' Section 3 - Discharge diagnosis (tambahan)
        sections("3") = CreateSection("Discharge diagnosis", "78375-3", "Discharge diagnosis Narrative", $"<div>{dischargeDiagnosis}</div>", conditionId)

        ' Section 4 - Medications on Discharge
        sections("4") = CreateSection("Medications on Discharge", "75311-1", "Hospital discharge medications Narrative", "<div> </div>", Nothing)
        sections("4")("mode") = "working"

        ' Section 5 - Plan of care
        sections("5") = CreateSection("Plan of care", "18776-5", "Plan of care", $"<div>{planOfCare}</div>", Nothing)
        sections("5")("mode") = "working"

        ' Section 6 - Known allergies
        Dim allergyId As String = Guid.NewGuid().ToString()
        sections("6") = CreateSection("Known allergies", "48765-2", "Allergies and adverse reactions", $"<div>{allergies}</div>", allergyId)

        composition("section") = sections

        Return composition
    End Function
    Private Function CreateSection(title As String, loincCode As String, loincDisplay As String,
                               divContent As String, entryRef As String) As JObject
        Dim section As New JObject()
        section("title") = title

        ' Code
        Dim code As New JObject()
        Dim coding As New JArray()
        Dim codingItem As New JObject()
        codingItem("system") = "http://loinc.org"
        codingItem("code") = loincCode
        codingItem("display") = loincDisplay
        coding.Add(codingItem)
        code("coding") = coding
        section("code") = code

        ' Text
        Dim text As New JObject()
        text("status") = "additional"
        text("div") = divContent
        section("text") = text

        ' Entry (Condition reference)
        If Not String.IsNullOrEmpty(entryRef) Then
            Dim entryArray As New JArray()
            Dim entry As New JObject()
            entry("reference") = entryRef
            entryArray.Add(entry)
            section("entry") = entryArray
        Else
            section("entry") = New JArray()
        End If

        Return section
    End Function
    Public Shared Function ExtractValidJson(rawText As String) As String
        ' Cari pola yang dimulai dengan { dan diakhiri } dengan struktur berpasangan
        Dim match As Match = Regex.Match(rawText, "(\{(?:[^{}]|(?<open>\{)|(?<-open>\}))+(?(open)(?!))\})")
        If match.Success Then
            Return match.Value
        End If
        Return rawText
    End Function
    Public Function CreateFhirBundleJsonComposition() As String
        ' Membuat objek Bundle utama
        Dim requestObj As New JObject()

        ' Set properti Bundle
        requestObj("resourceType") = "Bundle"
        requestObj("id") = fn_UDD(sPPKPELAYANAN, sRMEBPJS_koderskemenkes, "2")

        ' Meta object
        requestObj("meta") = New JObject()
        requestObj("meta")("lastUpdated") = "2026-03-24 10:18:25"

        ' Identifier object
        requestObj("identifier") = New JObject()
        requestObj("identifier")("system") = "sep"
        requestObj("identifier")("value") = "0464R0120326V000001"

        requestObj("type") = "document"

        ' Membuat array entry
        Dim entries As New JArray()

        ' ========== ENTRY 1: Composition ==========
        Dim composition As New JObject()
        composition("resourceType") = "Composition"
        composition("id") = fn_UDD(sPPKPELAYANAN, sRMEBPJS_koderskemenkes, "2")
        composition("status") = "final"

        ' Type coding dengan text
        composition("type") = New JObject()
        Dim typeCoding As New JArray()
        Dim typeCodeObj As New JObject()
        typeCodeObj("system") = "http://loinc.org"
        typeCodeObj("code") = "G43"  ' Gunakan kode Discharge Summary
        typeCoding.Add(typeCodeObj)
        composition("type")("coding") = typeCoding
        composition("type")("text") = "G43 - MIGRAINE"  ' ✅ Tambahkan property text

        ' Subject reference
        composition("subject") = New JObject()
        composition("subject")("reference") = "Patient/" & fn_UDD(sPPKPELAYANAN, sRMEBPJS_koderskemenkes, "2")
        composition("subject")("display") = "SOESANTO"

        ' Encounter reference
        composition("encounter") = New JObject()
        composition("encounter")("reference") = "Encounter/" & fn_UDD(sPPKPELAYANAN, sRMEBPJS_koderskemenkes, "2")

        composition("date") = "1950-02-12 00:00:00"  ' Format yang benar

        ' Author array
        Dim authors As New JArray()
        Dim author As New JObject()
        author("reference") = "Practitioner/" & fn_UDD(sPPKPELAYANAN, sRMEBPJS_koderskemenkes, "2")
        author("display") = "TIA TRICIA DEVI, DR"
        authors.Add(author)
        composition("author") = authors

        composition("title") = "Discharge Summary"
        composition("confidentiality") = "N"  ' ✅ Tambahkan confidentiality

        Dim sections As New JObject()

        ' Section "0" - dengan entry kosong
        sections("0") = New JObject() From {
    {"title", "Reason for admission"},
    {"code", New JObject() From {
        {"coding", New JArray() From {
            New JObject() From {{"system", "http://loinc.org"}, {"code", "29299-5"}, {"display", "Reason for visit Narrative"}}
        }}
    }},
    {"text", New JObject() From {{"status", "additional"}, {"div", "sakit 1"}}},
    {"entry", New JArray()}   ' ✅ Entry kosong
}

        ' Section "1" - dengan entry kosong
        sections("1") = New JObject() From {
    {"title", "Chief complaint"},
    {"code", New JObject() From {
        {"coding", New JArray() From {
            New JObject() From {{"system", "http://loinc.org"}, {"code", "10154-3"}, {"display", "Chief complaint Narrative"}}
        }}
    }},
    {"text", New JObject() From {{"status", "additional"}, {"div", "sakit 2"}}},
    {"entry", New JArray()}   ' ✅ Entry kosong
}

        ' Section "2" - dengan entry isi
        sections("2") = New JObject() From {
    {"title", "Admission diagnosis"},
    {"code", New JObject() From {
        {"coding", New JArray() From {
            New JObject() From {{"system", "http://loinc.org"}, {"code", "42347-5"}, {"display", "Admission diagnosis Narrative"}}
        }}
    }},
    {"text", New JObject() From {{"status", "additional"}, {"div", "LUKA BAKAR 52% TBSA, "}}},
    {"entry", New JArray() From {
        New JObject() From {{"reference", "urn:uuid:541a72a8-df75-4484-ac89-ac4923f03b81"}}
    }}
}

        ' Section "4" - dengan entry isi
        sections("4") = New JObject() From {
    {"title", "Medications on Discharge"},
    {"code", New JObject() From {
        {"coding", New JArray() From {
            New JObject() From {{"system", "http://loinc.org"}, {"code", "75311-1"}, {"display", "Hospital discharge medications Narrative"}}
        }}
    }},
    {"text", New JObject() From {{"status", "additional"}, {"div", "(CLINDAMYCIN)CLINDAMYCIN CAPSULE 300 MG 10 CAP, "}}},
    {"mode", "working"},
    {"entry", New JArray() From {
        New JObject() From {{"reference", "MedicationRequest/0901R001-1196708-1-ef852407-45aa-43c7-b5e8-98d63b43c182"}},
        New JObject() From {{"reference", "MedicationRequest/0901R001-1196708-1-ef852407-45aa-43c7-b5e8-98d63b43c182"}}
    }}
}

        ' Section "5" - dengan entry isi
        sections("5") = New JObject() From {
    {"title", "Plan of care"},
    {"code", New JObject() From {
        {"coding", New JArray() From {
            New JObject() From {{"system", "http://loinc.org"}, {"code", "18776-5"}, {"display", "Plan of care"}}
        }}
    }},
    {"text", New JObject() From {{"status", "additional"}, {"div", " "}}},
    {"mode", "working"},
    {"entry", New JArray() From {
        New JObject() From {{"reference", "MedicationRequest/124a6916-5d84-4b8c-b250-10cefb8e6e86"}}
    }}
}

        ' Section "7" - dengan entry isi
        sections("7") = New JObject() From {
    {"title", "Known allergies"},
    {"code", New JObject() From {
        {"coding", New JArray() From {
            New JObject() From {{"system", "http://loinc.org"}, {"code", "48765-2"}, {"display", "Allergies and adverse reactions"}}
        }}
    }},
    {"text", New JObject() From {{"status", "additional"}, {"div", " "}}},
    {"entry", New JArray() From {
        New JObject() From {{"reference", "AllergyIntolerance/47600e0f-b6b5-4308-84b5-5dec157f7637"}}
    }}
}

        composition("section") = sections

        ' ========== ASSIGN SECTIONS KE COMPOSITION ==========
        composition("section") = sections

        ' Tambahkan composition ke entries
        Dim entry1 As New JObject()
        entry1("resource") = composition
        entries.Add(entry1)

        requestObj("entry") = entries

        Return requestObj.ToString(Newtonsoft.Json.Formatting.Indented)
    End Function
    '    Public Function CreateFhirBundleJson() As String
    '        ' Membuat objek Bundle utama
    '        Dim requestObj As New JObject()

    '        ' Set properti Bundle
    '        requestObj("resourceType") = "Bundle"
    '        requestObj("id") = fn_UDD(sPPKPELAYANAN, sRMEBPJS_koderskemenkes, "2")

    '        ' Meta object
    '        requestObj("meta") = New JObject()
    '        requestObj("meta")("lastUpdated") = "2026-03-24 10:18:25"

    '        ' Identifier object
    '        requestObj("identifier") = New JObject()
    '        requestObj("identifier")("system") = "sep"
    '        requestObj("identifier")("value") = "0464R0120326V000001"

    '        requestObj("type") = "document"

    '        ' Membuat array entry
    '        Dim entries As New JArray()

    '        ' ========== ENTRY 1: Composition ==========
    '        Dim composition As New JObject()
    '        composition("resourceType") = "Composition"
    '        composition("id") = fn_UDD(sPPKPELAYANAN, sRMEBPJS_koderskemenkes, "2")
    '        composition("status") = "final"

    '        ' Type coding dengan text
    '        composition("type") = New JObject()
    '        Dim typeCoding As New JArray()
    '        Dim typeCodeObj As New JObject()
    '        typeCodeObj("system") = "http://loinc.org"
    '        typeCodeObj("code") = "81218-0"  ' Gunakan kode Discharge Summary
    '        typeCoding.Add(typeCodeObj)
    '        composition("type")("coding") = typeCoding
    '        composition("type")("text") = "Discharge Summary"  ' ✅ Tambahkan property text

    '        ' Subject reference
    '        composition("subject") = New JObject()
    '        composition("subject")("reference") = "Patient/" & fn_UDD(sPPKPELAYANAN, sRMEBPJS_koderskemenkes, "2")
    '        composition("subject")("display") = "SOESANTO"

    '        ' Encounter reference
    '        composition("encounter") = New JObject()
    '        composition("encounter")("reference") = "Encounter/" & fn_UDD(sPPKPELAYANAN, sRMEBPJS_koderskemenkes, "2")

    '        composition("date") = "1950-02-12 00:00:00"  ' Format yang benar

    '        ' Author array
    '        Dim authors As New JArray()
    '        Dim author As New JObject()
    '        author("reference") = "Practitioner/" & fn_UDD(sPPKPELAYANAN, sRMEBPJS_koderskemenkes, "2")
    '        author("display") = "TIA TRICIA DEVI, DR"
    '        authors.Add(author)
    '        composition("author") = authors

    '        composition("title") = "Discharge Summary"
    '        composition("confidentiality") = "N"  ' ✅ Tambahkan confidentiality

    '        Dim sections As New JObject()

    '        ' Section "0" - dengan entry kosong
    '        sections("0") = New JObject() From {
    '    {"title", "Reason for admission"},
    '    {"code", New JObject() From {
    '        {"coding", New JArray() From {
    '            New JObject() From {{"system", "http://loinc.org"}, {"code", "29299-5"}, {"display", "Reason for visit Narrative"}}
    '        }}
    '    }},
    '    {"text", New JObject() From {{"status", "additional"}, {"div", "sakit 1"}}},
    '    {"entry", New JArray()}   ' ✅ Entry kosong
    '}

    '        ' Section "1" - dengan entry kosong
    '        sections("1") = New JObject() From {
    '    {"title", "Chief complaint"},
    '    {"code", New JObject() From {
    '        {"coding", New JArray() From {
    '            New JObject() From {{"system", "http://loinc.org"}, {"code", "10154-3"}, {"display", "Chief complaint Narrative"}}
    '        }}
    '    }},
    '    {"text", New JObject() From {{"status", "additional"}, {"div", "sakit 2"}}},
    '    {"entry", New JArray()}   ' ✅ Entry kosong
    '}

    '        ' Section "2" - dengan entry isi
    '        sections("2") = New JObject() From {
    '    {"title", "Admission diagnosis"},
    '    {"code", New JObject() From {
    '        {"coding", New JArray() From {
    '            New JObject() From {{"system", "http://loinc.org"}, {"code", "42347-5"}, {"display", "Admission diagnosis Narrative"}}
    '        }}
    '    }},
    '    {"text", New JObject() From {{"status", "additional"}, {"div", "LUKA BAKAR 52% TBSA, "}}},
    '    {"entry", New JArray() From {
    '        New JObject() From {{"reference", "urn:uuid:541a72a8-df75-4484-ac89-ac4923f03b81"}}
    '    }}
    '}

    '        ' Section "4" - dengan entry isi
    '        sections("4") = New JObject() From {
    '    {"title", "Medications on Discharge"},
    '    {"code", New JObject() From {
    '        {"coding", New JArray() From {
    '            New JObject() From {{"system", "http://loinc.org"}, {"code", "75311-1"}, {"display", "Hospital discharge medications Narrative"}}
    '        }}
    '    }},
    '    {"text", New JObject() From {{"status", "additional"}, {"div", "(CLINDAMYCIN)CLINDAMYCIN CAPSULE 300 MG 10 CAP, "}}},
    '    {"mode", "working"},
    '    {"entry", New JArray() From {
    '        New JObject() From {{"reference", "MedicationRequest/0901R001-1196708-1-ef852407-45aa-43c7-b5e8-98d63b43c182"}},
    '        New JObject() From {{"reference", "MedicationRequest/0901R001-1196708-1-ef852407-45aa-43c7-b5e8-98d63b43c182"}}
    '    }}
    '}

    '        ' Section "5" - dengan entry isi
    '        sections("5") = New JObject() From {
    '    {"title", "Plan of care"},
    '    {"code", New JObject() From {
    '        {"coding", New JArray() From {
    '            New JObject() From {{"system", "http://loinc.org"}, {"code", "18776-5"}, {"display", "Plan of care"}}
    '        }}
    '    }},
    '    {"text", New JObject() From {{"status", "additional"}, {"div", " "}}},
    '    {"mode", "working"},
    '    {"entry", New JArray() From {
    '        New JObject() From {{"reference", "MedicationRequest/124a6916-5d84-4b8c-b250-10cefb8e6e86"}}
    '    }}
    '}

    '        ' Section "7" - dengan entry isi
    '        sections("7") = New JObject() From {
    '    {"title", "Known allergies"},
    '    {"code", New JObject() From {
    '        {"coding", New JArray() From {
    '            New JObject() From {{"system", "http://loinc.org"}, {"code", "48765-2"}, {"display", "Allergies and adverse reactions"}}
    '        }}
    '    }},
    '    {"text", New JObject() From {{"status", "additional"}, {"div", " "}}},
    '    {"entry", New JArray() From {
    '        New JObject() From {{"reference", "AllergyIntolerance/47600e0f-b6b5-4308-84b5-5dec157f7637"}}
    '    }}
    '}

    '        composition("section") = sections

    '        ' ========== ASSIGN SECTIONS KE COMPOSITION ==========
    '        composition("section") = sections

    '        ' Tambahkan composition ke entries
    '        Dim entry1 As New JObject()
    '        entry1("resource") = composition
    '        entries.Add(entry1)

    '        '' ========== ENTRY 2: Patient ==========
    '        'Dim patient As New JObject()
    '        'patient("resourceType") = "Patient"
    '        'patient("id") = fn_UDD(sPPKPELAYANAN, sRMEBPJS_koderskemenkes, "2")

    '        '' Patient identifiers
    '        'Dim identifiers As New JArray()

    '        '' Identifier 1 - MR
    '        'Dim id1 As New JObject()
    '        'id1("use") = "usual"
    '        'id1("type") = New JObject()
    '        'Dim id1Coding As New JArray()
    '        'Dim id1Code As New JObject()
    '        'id1Code("system") = "http://hl7.org/fhir/v2/0203"
    '        'id1Code("code") = "MR"
    '        'id1Coding.Add(id1Code)
    '        'id1("type")("coding") = id1Coding
    '        'id1("value") = "000005"
    '        'id1("assigner") = New JObject()
    '        'id1("assigner")("display") = "IGD"
    '        'identifiers.Add(id1)

    '        '' Identifier 2 - BPJS
    '        'Dim id2 As New JObject()
    '        'id2("use") = "official"
    '        'id2("type") = New JObject()
    '        'Dim id2Coding As New JArray()
    '        'Dim id2Code As New JObject()
    '        'id2Code("system") = "http://hl7.org/fhir/v2/0203"
    '        'id2Code("code") = "MB"
    '        'id2Coding.Add(id2Code)
    '        'id2("type")("coding") = id2Coding
    '        'id2("value") = "0002076061241"
    '        'id2("assigner") = New JObject()
    '        'id2("assigner")("display") = "BPJS KESEHATAN"
    '        'identifiers.Add(id2)

    '        '' Identifier 3 - KTP
    '        'Dim id3 As New JObject()
    '        'id3("use") = "official"
    '        'id3("type") = New JObject()
    '        'Dim id3Coding As New JArray()
    '        'Dim id3Code As New JObject()
    '        'id3Code("system") = "http://hl7.org/fhir/v2/0203"
    '        'id3Code("code") = "SOESANTO"
    '        'id3Coding.Add(id3Code)
    '        'id3("type")("coding") = id3Coding
    '        'id3("value") = "3508191202500001"
    '        'id3("assigner") = New JObject()
    '        'id3("assigner")("display") = "KEMENDAGRI"
    '        'identifiers.Add(id3)

    '        'patient("identifier") = identifiers
    '        'patient("active") = True

    '        '' Patient name
    '        'Dim names As New JArray()
    '        'Dim nameObj As New JObject()
    '        'nameObj("use") = "official"
    '        'nameObj("text") = "SOESANTO"
    '        'names.Add(nameObj)
    '        'patient("name") = names

    '        'patient("gender") = "male"
    '        'patient("birthDate") = "1950-02-12"

    '        '' Patient address
    '        'Dim addresses As New JArray()
    '        'Dim addressObj As New JObject()
    '        'Dim addressLines As New JArray()
    '        'addressLines.Add("JL JAYA NO. 119 RT 10000 RW 003")
    '        'addressObj("line") = addressLines
    '        'addressObj("use") = "home"
    '        'addressObj("type") = "both"
    '        'addresses.Add(addressObj)
    '        'patient("address") = addresses

    '        'Dim entry2 As New JObject()
    '        'entry2("resource") = patient
    '        'entries.Add(entry2)

    '        '' ========== ENTRY 3: Encounter ==========
    '        'Dim encounter As New JObject()
    '        'encounter("resourceType") = "Encounter"
    '        'encounter("id") = "Encounter-69c1381f978c0"

    '        'encounter("subject") = New JObject()
    '        'encounter("subject")("reference") = "Patient-69c1381f978bf"
    '        'encounter("subject")("display") = "MITA"
    '        'encounter("subject")("noSep") = "0464R0120326V000001"

    '        'encounter("class") = New JObject()
    '        'encounter("class")("system") = "http://hl7.org/fhir/v3/ActCode"
    '        'encounter("class")("code") = "IMP"
    '        'encounter("class")("display") = "inpatient encounter"

    '        'Dim reasons As New JArray()
    '        'Dim reasonObj As New JObject()
    '        'reasonObj("text") = "Burns involving 40-49% of body surface"
    '        'reasons.Add(reasonObj)
    '        'encounter("reason") = reasons

    '        'encounter("period") = New JObject()
    '        'encounter("period")("start") = "2018-08-15 04:21:36"
    '        'encounter("period")("end") = "2026-03-23 12:54:55"

    '        'encounter("status") = "finished"

    '        'Dim entry3 As New JObject()
    '        'entry3("resource") = encounter
    '        ''entries.Add(entry3)

    '        '' ========== ENTRY 4: Condition ==========
    '        'Dim condition As New JObject()
    '        'condition("resourceType") = "Condition"
    '        'condition("id") = "Condition-69c1381f978c8"
    '        'condition("clinicalStatus") = "active"
    '        'condition("verificationStatus") = "confirmed"

    '        '' Category
    '        'Dim categories As New JArray()
    '        'Dim categoryObj As New JObject()
    '        'Dim categoryCoding As New JArray()
    '        'Dim categoryCode As New JObject()
    '        'categoryCode("system") = "http://hl7.org/fhir/condition-category"
    '        'categoryCode("code") = "encounter-diagnosis"
    '        'categoryCode("display") = "Encounter Diagnosis"
    '        'categoryCoding.Add(categoryCode)
    '        'categoryObj("coding") = categoryCoding
    '        'categories.Add(categoryObj)
    '        'condition("category") = categories

    '        '' Code
    '        'condition("code") = New JObject()
    '        'Dim conditionCodings As New JArray()
    '        'Dim conditionCode As New JObject()
    '        'conditionCode("system") = "http://hl7.org/fhir/sid/icd-10"
    '        'conditionCode("code") = "T31.4"
    '        'conditionCode("display") = "Burns involving 40-49% of body surface"
    '        'conditionCodings.Add(conditionCode)
    '        'condition("code")("coding") = conditionCodings
    '        'condition("code")("text") = "Burns involving 40-49% of body surface"

    '        'condition("subject") = New JObject()
    '        'condition("subject")("reference") = "Patient-69c1381f978bf"
    '        'condition("onsetDateTime") = "2018-08-15 04:21:36"

    '        'Dim entry4 As New JObject()
    '        'entry4("resource") = condition
    '        ''entries.Add(entry4)

    '        '' ========== ENTRY 5: Procedure ==========
    '        'Dim procedure As New JObject()
    '        'procedure("resourceType") = "Procedure"
    '        'procedure("id") = "Procedure-69c1381f978c9"
    '        'procedure("status") = "completed"

    '        'procedure("code") = New JObject()
    '        'Dim procedureCodings As New JArray()
    '        'Dim procedureCode As New JObject()
    '        'procedureCode("display") = "Triage"
    '        'procedureCodings.Add(procedureCode)
    '        'procedure("code")("coding") = procedureCodings

    '        'procedure("subject") = New JObject()
    '        'procedure("subject")("reference") = "Patient-69c1381f978bf"
    '        'procedure("subject")("display") = "MITA"

    '        'Dim performers As New JArray()
    '        'Dim performerObj As New JObject()
    '        'performerObj("actor") = New JObject()
    '        'performerObj("actor")("reference") = "Practitioner-69c1381f978c2"
    '        'performerObj("actor")("display") = "Septi Sari Yanti"
    '        'performers.Add(performerObj)
    '        'procedure("performer") = performers

    '        'Dim entry5 As New JObject()
    '        'entry5("resource") = procedure
    '        ''entries.Add(entry5)

    '        '' ========== ENTRY 6: DiagnosticReport ==========
    '        'Dim diagnosticReport As New JObject()
    '        'diagnosticReport("resourceType") = "DiagnosticReport"
    '        'diagnosticReport("id") = "DiagnosticReport-69c1381f978ca"

    '        'diagnosticReport("subject") = New JObject()
    '        'diagnosticReport("subject")("reference") = "Patient-69c1381f978bf"
    '        'diagnosticReport("subject")("display") = "MITA"

    '        'diagnosticReport("status") = "final"

    '        '' Result observation
    '        'Dim results As New JArray()
    '        'Dim observationObj As New JObject()
    '        'observationObj("resourceType") = "Observation"
    '        'observationObj("id") = "Observation-69c1381f978cb"
    '        'observationObj("status") = "final"
    '        'observationObj("conclusion") = "Tak tampak kelainan radiologis pada jantung dan paru."
    '        'results.Add(observationObj)
    '        'diagnosticReport("result") = results

    '        'Dim entry6 As New JObject()
    '        'entry6("resource") = diagnosticReport
    '        ''entries.Add(entry6)

    '        '' ========== ENTRY 7: Practitioner ==========
    '        'Dim practitioner As New JObject()
    '        'practitioner("resourceType") = "Practitioner"
    '        'practitioner("id") = "Practitioner-69c1381f978c2"

    '        'Dim practitionerIds As New JArray()
    '        'Dim practitionerId As New JObject()
    '        'practitionerId("use") = "official"
    '        'practitionerId("type") = New JObject()
    '        'Dim practitionerIdCoding As New JArray()
    '        'Dim practitionerIdCode As New JObject()
    '        'practitionerIdCode("system") = "http://hl7.org/fhir/v2/0203"
    '        'practitionerIdCode("code") = "NNIDN"
    '        'practitionerIdCoding.Add(practitionerIdCode)
    '        'practitionerId("type")("coding") = practitionerIdCoding
    '        'practitionerId("value") = "3172055103530001"
    '        'practitionerId("assigner") = New JObject()
    '        'practitionerId("assigner")("display") = "KEMDAGRI"
    '        'practitionerIds.Add(practitionerId)
    '        'practitioner("identifier") = practitionerIds

    '        'Dim practitionerNames As New JArray()
    '        'Dim practitionerName As New JObject()
    '        'practitionerName("use") = "official"
    '        'practitionerName("text") = "Septi Sari Yanti"
    '        'practitionerNames.Add(practitionerName)
    '        'practitioner("name") = practitionerNames

    '        'Dim entry7 As New JObject()
    '        'entry7("resource") = practitioner
    '        ''entries.Add(entry7)

    '        '' ========== ENTRY 8: Device ==========
    '        'Dim device As New JObject()
    '        'device("resourceType") = "Device"
    '        'device("id") = "Device-69c1381f978cc"

    '        'Dim deviceIds As New JArray()
    '        'Dim deviceId As New JObject()
    '        'deviceId("system") = "http://acme.com/devices/pacemakers/octane/serial"
    '        'deviceId("value") = "MDVx024590"
    '        'deviceIds.Add(deviceId)
    '        'device("identifier") = deviceIds

    '        'device("type") = New JObject()
    '        'Dim deviceCodings As New JArray()
    '        'Dim deviceCode As New JObject()
    '        'deviceCode("system") = "http://acme.com/devices"
    '        'deviceCode("code") = "MDVx024590"
    '        'deviceCode("display") = "SKINTACT EASYTAB"
    '        'deviceCodings.Add(deviceCode)
    '        'device("type")("coding") = deviceCodings

    '        'device("patient") = New JObject()
    '        'device("patient")("reference") = "Patient-69c1381f978bf"

    '        'Dim entry8 As New JObject()
    '        'entry8("resource") = device
    '        ''entries.Add(entry8)

    '        '' ========== ENTRY 9: Organization ==========
    '        'Dim organization As New JObject()
    '        'organization("resourceType") = "Organization"
    '        'organization("id") = "Organization-69c1381f978c1"
    '        'organization("name") = "IGD RS DUSTIRA"

    '        'Dim entry9 As New JObject()
    '        'entry9("resource") = organization
    '        ''entries.Add(entry9)

    '        '' ========== ENTRY 10: MedicationRequest (Paracetamol) ==========
    '        'Dim medicationRequest1 As New JObject()
    '        'medicationRequest1("resourceType") = "MedicationRequest"
    '        'medicationRequest1("id") = "MedicationRequest-69c1381f978cd"
    '        'medicationRequest1("status") = "active"
    '        'medicationRequest1("intent") = "order"

    '        'medicationRequest1("medicationCodeableConcept") = New JObject()
    '        'Dim medCodings1 As New JArray()
    '        'Dim medCode1 As New JObject()
    '        'medCode1("system") = "http://sys-pharmacy.com/code"
    '        'medCode1("code") = "9300643"
    '        'medCode1("display") = "Paracetamol 500mg"
    '        'medCodings1.Add(medCode1)
    '        'medicationRequest1("medicationCodeableConcept")("coding") = medCodings1
    '        'medicationRequest1("medicationCodeableConcept")("text") = "Paracetamol 500mg"

    '        'medicationRequest1("subject") = New JObject()
    '        'medicationRequest1("subject")("reference") = "Patient-69c1381f978bf"
    '        'medicationRequest1("subject")("display") = "MITA"

    '        'medicationRequest1("encounter") = New JObject()
    '        'medicationRequest1("encounter")("reference") = "Encounter-69c1381f978c0"

    '        'medicationRequest1("authoredOn") = "2026-03-23T12:54:55+00:00"

    '        'medicationRequest1("requester") = New JObject()
    '        'medicationRequest1("requester")("reference") = "Practitioner-69c1381f978c2"
    '        'medicationRequest1("requester")("display") = "Septi Sari Yanti"

    '        'Dim dosages1 As New JArray()
    '        'Dim dosage1 As New JObject()
    '        'dosage1("text") = "3 kali sehari 1 tablet sesudah makan"
    '        'dosages1.Add(dosage1)
    '        'medicationRequest1("dosageInstruction") = dosages1

    '        'medicationRequest1("dispenseRequest") = New JObject()
    '        'medicationRequest1("dispenseRequest")("quantity") = New JObject()
    '        'medicationRequest1("dispenseRequest")("quantity")("value") = 10
    '        'medicationRequest1("dispenseRequest")("quantity")("unit") = "TAB"

    '        'Dim entry10 As New JObject()
    '        'entry10("resource") = medicationRequest1
    '        ''entries.Add(entry10)

    '        '' ========== ENTRY 11: MedicationRequest (Amoxicillin) ==========
    '        'Dim medicationRequest2 As New JObject()
    '        'medicationRequest2("resourceType") = "MedicationRequest"
    '        'medicationRequest2("id") = "MedicationRequest-69c1381f978ce"
    '        'medicationRequest2("status") = "active"
    '        'medicationRequest2("intent") = "order"

    '        'medicationRequest2("medicationCodeableConcept") = New JObject()
    '        'Dim medCodings2 As New JArray()
    '        'Dim medCode2 As New JObject()
    '        'medCode2("system") = "http://sys-pharmacy.com/code"
    '        'medCode2("code") = "1234567"
    '        'medCode2("display") = "Amoxicillin 500mg"
    '        'medCodings2.Add(medCode2)
    '        'medicationRequest2("medicationCodeableConcept")("coding") = medCodings2
    '        'medicationRequest2("medicationCodeableConcept")("text") = "Amoxicillin 500mg"

    '        'medicationRequest2("subject") = New JObject()
    '        'medicationRequest2("subject")("reference") = "Patient-69c1381f978bf"
    '        'medicationRequest2("subject")("display") = "MITA"

    '        'medicationRequest2("encounter") = New JObject()
    '        'medicationRequest2("encounter")("reference") = "Encounter-69c1381f978c0"

    '        'medicationRequest2("authoredOn") = "2026-03-23T12:54:55+00:00"

    '        'medicationRequest2("requester") = New JObject()
    '        'medicationRequest2("requester")("reference") = "Practitioner-69c1381f978c2"
    '        'medicationRequest2("requester")("display") = "Septi Sari Yanti"

    '        'Dim dosages2 As New JArray()
    '        'Dim dosage2 As New JObject()
    '        'dosage2("text") = "3 kali sehari 1 tablet dihabiskan"
    '        'dosages2.Add(dosage2)
    '        'medicationRequest2("dosageInstruction") = dosages2

    '        'medicationRequest2("dispenseRequest") = New JObject()
    '        'medicationRequest2("dispenseRequest")("quantity") = New JObject()
    '        'medicationRequest2("dispenseRequest")("quantity")("value") = 15
    '        'medicationRequest2("dispenseRequest")("quantity")("unit") = "TAB"

    '        'Dim entry11 As New JObject()
    '        'entry11("resource") = medicationRequest2
    '        ''entries.Add(entry11)

    '        ' Assign entries array ke bundle
    '        requestObj("entry") = entries

    '        ' Mengembalikan string JSON dengan format indented
    '        Return requestObj.ToString(Newtonsoft.Json.Formatting.Indented)
    '    End Function
    'Public Function CreateFhirBundleJson2() As String
    '    Dim sb As New StringBuilder()

    '    sb.AppendLine("{")
    '    sb.AppendLine("  ""resourceType"": ""Bundle"",")
    '    sb.AppendLine("  ""id"": ""0464R012-1193709-34-19c94c1a-1b06-4716-a952-6127c45a09a8"",")
    '    sb.AppendLine("  ""meta"": {")
    '    sb.AppendLine("    ""lastUpdated"": ""2026-02-28 10:18:25""")
    '    sb.AppendLine("  },")
    '    sb.AppendLine("  ""identifier"": {")
    '    sb.AppendLine("    ""system"": ""sep"",")
    '    sb.AppendLine("    ""value"": ""0464R0120326V000001""")
    '    sb.AppendLine("  },")
    '    sb.AppendLine("  ""type"": ""document"",")
    '    sb.AppendLine("  ""entry"": [")
    '    sb.AppendLine("    {")
    '    sb.AppendLine("      ""resource"": {")
    '    sb.AppendLine("        ""resourceType"": ""Composition"",")
    '    sb.AppendLine("        ""id"": ""0464R012-1193709-34-16f6a17f-f5c7-4f62-b418-f107dc8a89fb"",")
    '    sb.AppendLine("        ""status"": ""final"",")
    '    sb.AppendLine("        ""type"": {")
    '    sb.AppendLine("          ""coding"": [")
    '    sb.AppendLine("            {")
    '    sb.AppendLine("              ""system"": ""http://loinc.org"",")
    '    sb.AppendLine("              ""code"": ""81218-0""")
    '    sb.AppendLine("            }")
    '    sb.AppendLine("          ],")
    '    sb.AppendLine("          ""text"": ""Discharge Summary""")
    '    sb.AppendLine("        },")
    '    sb.AppendLine("        ""subject"": {")
    '    sb.AppendLine("          ""reference"": ""Patient/0464R012-1193709-34-af125c4b-cd0f-4877-b76a-532a3656da97"",")
    '    sb.AppendLine("          ""display"": ""SOESANTO""")
    '    sb.AppendLine("        },")
    '    sb.AppendLine("        ""encounter"": {")
    '    sb.AppendLine("          ""reference"": ""Encounter/0464R012-1193709-34-8d887b38-feb9-4edf-b818-c49336448c90""")
    '    sb.AppendLine("        },")
    '    sb.AppendLine("        ""date"": ""1950-02-12 00:00:00"",")
    '    sb.AppendLine("        ""author"": [")
    '    sb.AppendLine("          {")
    '    sb.AppendLine("            ""reference"": ""Practitioner/0464R012-1193709-34-6b72e7ad-4be4-419c-9a21-36119e177c47"",")
    '    sb.AppendLine("            ""display"": ""TIA TRICIA DEVI, DR""")
    '    sb.AppendLine("          }")
    '    sb.AppendLine("        ],")
    '    sb.AppendLine("        ""title"": ""Discharge Summary"",")
    '    sb.AppendLine("        ""confidentiality"": ""N"",")
    '    sb.AppendLine("        ""section"": [")
    '    sb.AppendLine("          {")
    '    sb.AppendLine("            ""title"": ""Reason for admission"",")
    '    sb.AppendLine("            ""code"": {")
    '    sb.AppendLine("              ""coding"": [")
    '    sb.AppendLine("                {")
    '    sb.AppendLine("                  ""system"": ""http://loinc.org"",")
    '    sb.AppendLine("                  ""code"": ""29299-5"",")
    '    sb.AppendLine("                  ""display"": ""Reason for visit Narrative""")
    '    sb.AppendLine("                }")
    '    sb.AppendLine("              ]")
    '    sb.AppendLine("            },")
    '    sb.AppendLine("            ""text"": {")
    '    sb.AppendLine("              ""status"": ""additional"",")
    '    sb.AppendLine("              ""div"": ""<div>2</div>""")
    '    sb.AppendLine("            }")
    '    sb.AppendLine("          },")
    '    sb.AppendLine("          {")
    '    'sb.AppendLine("            ""title"": ""Chief complaint"",")
    '    'sb.AppendLine("            ""code"": {")
    '    'sb.AppendLine("              ""coding"": [")
    '    'sb.AppendLine("                {")
    '    'sb.AppendLine("                  ""system"": ""http://loinc.org"",")
    '    'sb.AppendLine("                  ""code"": ""10154-3"",")
    '    'sb.AppendLine("                  ""display"": ""Chief complaint Narrative""")
    '    'sb.AppendLine("                }")
    '    'sb.AppendLine("              ]")
    '    'sb.AppendLine("            },")
    '    'sb.AppendLine("            ""text"": {")
    '    'sb.AppendLine("              ""status"": ""additional"",")
    '    'sb.AppendLine("              ""div"": ""<div>2</div>""")
    '    'sb.AppendLine("            }")
    '    'sb.AppendLine("          },")
    '    'sb.AppendLine("          {")
    '    'sb.AppendLine("            ""title"": ""Admission diagnosis"",")
    '    'sb.AppendLine("            ""code"": {")
    '    'sb.AppendLine("              ""coding"": [")
    '    'sb.AppendLine("                {")
    '    'sb.AppendLine("                  ""system"": ""http://loinc.org"",")
    '    'sb.AppendLine("                  ""code"": ""42347-5"",")
    '    'sb.AppendLine("                  ""display"": ""Admission diagnosis Narrative""")
    '    'sb.AppendLine("                }")
    '    'sb.AppendLine("              ]")
    '    'sb.AppendLine("            },")
    '    'sb.AppendLine("            ""text"": {")
    '    'sb.AppendLine("              ""status"": ""additional"",")
    '    'sb.AppendLine("              ""div"": ""<div>ANEMIA APLASTIK, HEMOROID INTERN</div>""")
    '    'sb.AppendLine("            },")
    '    'sb.AppendLine("            ""entry"": [")
    '    'sb.AppendLine("              {")
    '    'sb.AppendLine("                ""reference"": ""urn:uuid:541a72a8-df75-4484-ac89-ac4923f03b81""")
    '    'sb.AppendLine("              }")
    '    'sb.AppendLine("            ]")
    '    'sb.AppendLine("          },")
    '    'sb.AppendLine("          {")
    '    'sb.AppendLine("            ""title"": ""Discharge diagnosis"",")
    '    'sb.AppendLine("            ""code"": {")
    '    'sb.AppendLine("              ""coding"": [")
    '    'sb.AppendLine("                {")
    '    'sb.AppendLine("                  ""system"": ""http://loinc.org"",")
    '    'sb.AppendLine("                  ""code"": ""78375-3"",")
    '    'sb.AppendLine("                  ""display"": ""Discharge diagnosis Narrative""")
    '    'sb.AppendLine("                }")
    '    'sb.AppendLine("              ]")
    '    'sb.AppendLine("            },")
    '    'sb.AppendLine("            ""text"": {")
    '    'sb.AppendLine("              ""status"": ""additional"",")
    '    'sb.AppendLine("              ""div"": ""<div>Aplastic anaemia, unspecified, ANEMIA APLASTIK, HEMOROID INTERN, Internal haemorrhoids without complication</div>""")
    '    'sb.AppendLine("            },")
    '    'sb.AppendLine("            ""entry"": [")
    '    'sb.AppendLine("              {")
    '    'sb.AppendLine("                ""reference"": ""urn:uuid:541a72a8-df75-4484-ac89-ac4923f03b81""")
    '    'sb.AppendLine("              }")
    '    'sb.AppendLine("            ]")
    '    'sb.AppendLine("          },")
    '    'sb.AppendLine("          {")
    '    'sb.AppendLine("            ""title"": ""Plan of care"",")
    '    'sb.AppendLine("            ""code"": {")
    '    'sb.AppendLine("              ""coding"": [")
    '    'sb.AppendLine("                {")
    '    'sb.AppendLine("                  ""system"": ""http://loinc.org"",")
    '    'sb.AppendLine("                  ""code"": ""18776-5"",")
    '    'sb.AppendLine("                  ""display"": ""Plan of care""")
    '    'sb.AppendLine("                }")
    '    'sb.AppendLine("              ]")
    '    'sb.AppendLine("            },")
    '    'sb.AppendLine("            ""text"": {")
    '    'sb.AppendLine("              ""status"": ""additional"",")
    '    'sb.AppendLine("              ""div"": ""<div>2</div>""")
    '    'sb.AppendLine("            },")
    '    'sb.AppendLine("            ""mode"": ""working"",")
    '    'sb.AppendLine("            ""entry"": [")
    '    'sb.AppendLine("              {")
    '    'sb.AppendLine("                ""reference"": ""MedicationRequest/124a6916-5d84-4b8c-b250-10cefb8e6e86""")
    '    'sb.AppendLine("              }")
    '    'sb.AppendLine("            ]")
    '    'sb.AppendLine("          },")
    '    'sb.AppendLine("          {")
    '    'sb.AppendLine("            ""title"": ""Known allergies"",")
    '    'sb.AppendLine("            ""code"": {")
    '    'sb.AppendLine("              ""coding"": [")
    '    'sb.AppendLine("                {")
    '    'sb.AppendLine("                  ""system"": ""http://loinc.org"",")
    '    'sb.AppendLine("                  ""code"": ""48765-2"",")
    '    'sb.AppendLine("                  ""display"": ""Allergies and adverse reactions""")
    '    'sb.AppendLine("                }")
    '    'sb.AppendLine("              ]")
    '    'sb.AppendLine("            },")
    '    'sb.AppendLine("            ""text"": {")
    '    'sb.AppendLine("              ""status"": ""additional"",")
    '    'sb.AppendLine("              ""div"": ""<div>2</div>""")
    '    'sb.AppendLine("            },")
    '    'sb.AppendLine("            ""entry"": [")
    '    'sb.AppendLine("              {")
    '    'sb.AppendLine("                ""reference"": ""AllergyIntolerance/47600e0f-b6b5-4308-84b5-5dec157f7637""")
    '    'sb.AppendLine("              }")
    '    'sb.AppendLine("            ]")
    '    sb.AppendLine("          }")
    '    sb.AppendLine("        ]")
    '    sb.AppendLine("      }")
    '    sb.AppendLine("    }")
    '    sb.AppendLine("  ]")
    '    sb.AppendLine("}")

    '    Return sb.ToString()
    'End Function
    'Public Function CreateJsonString() As String
    '    ' Membuat objek utama request
    '    Dim requestObj As New JObject()
    '    requestObj("noSep") = "0464R0120326V000001"
    '    requestObj("jnsPelayanan") = "2"
    '    requestObj("bulan") = "03"
    '    requestObj("tahun") = "2026"

    '    ' Membuat objek dataMR (Bundle)
    '    Dim dataMRObj As New JObject()
    '    dataMRObj("resourceType") = "Bundle"
    '    dataMRObj("id") = fn_UDD(sPPKPELAYANAN, sRMEBPJS_koderskemenkes, 2)

    '    ' Meta
    '    Dim metaObj As New JObject()
    '    metaObj("lastUpdated") = Now.ToString("yyyy-MM-dd HH:mm:ss")
    '    dataMRObj("meta") = metaObj

    '    ' Identifier
    '    Dim identifierObj As New JObject()
    '    identifierObj("use") = Nothing
    '    identifierObj("system") = "SEP"
    '    identifierObj("value") = "0464R0120326V000001"

    '    Dim typeObj As New JObject()
    '    typeObj("coding") = New JArray()
    '    typeObj("text") = Nothing
    '    identifierObj("type") = typeObj

    '    Dim assignerObj As New JObject()
    '    assignerObj("display") = Nothing
    '    identifierObj("assigner") = assignerObj

    '    dataMRObj("identifier") = identifierObj
    '    dataMRObj("type") = "document"

    '    ' Membuat array entry
    '    Dim entryArray As New JArray()

    '    ' Entry 1 - Patient
    '    Dim entry1 As New JObject()
    '    Dim patientObj As New JObject()
    '    patientObj("resourceType") = "Patient"
    '    patientObj("id") = fn_UDD(sPPKPELAYANAN, sRMEBPJS_koderskemenkes, 2)
    '    patientObj("active") = True

    '    ' Patient identifiers
    '    Dim identifiersArray As New JArray()

    '    ' Identifier 1
    '    Dim identifier1 As New JObject()
    '    identifier1("use") = "usual"
    '    identifier1("value") = "11111"

    '    Dim type1 As New JObject()
    '    Dim coding1 As New JArray()
    '    Dim coding1Item As New JObject()
    '    coding1Item("system") = "http://hl7.org/fhir/v2/0203"
    '    coding1Item("code") = "MR"
    '    coding1.Add(coding1Item)
    '    type1("coding") = coding1
    '    identifier1("type") = type1

    '    Dim assigner1 As New JObject()
    '    assigner1("display") = "RS DUSTIRA"
    '    identifier1("assigner") = assigner1

    '    identifiersArray.Add(identifier1)

    '    ' Identifier 2
    '    Dim identifier2 As New JObject()
    '    identifier2("use") = "official"
    '    identifier2("value") = "0002066555979"

    '    Dim type2 As New JObject()
    '    Dim coding2 As New JArray()
    '    Dim coding2Item As New JObject()
    '    coding2Item("system") = "http://hl7.org/fhir/v2/0203"
    '    coding2Item("code") = "MB"
    '    coding2.Add(coding2Item)
    '    type2("coding") = coding2
    '    identifier2("type") = type2

    '    Dim assigner2 As New JObject()
    '    assigner2("display") = "BPJS KESEHATAN"
    '    identifier2("assigner") = assigner2

    '    identifiersArray.Add(identifier2)

    '    ' Identifier 3
    '    Dim identifier3 As New JObject()
    '    identifier3("use") = "official"
    '    identifier3("value") = "3273066612970001"

    '    Dim type3 As New JObject()
    '    Dim coding3 As New JArray()
    '    Dim coding3Item As New JObject()
    '    coding3Item("system") = "http://hl7.org/fhir/v2/0203"
    '    coding3Item("code") = "NNIDN"
    '    coding3.Add(coding3Item)
    '    type3("coding") = coding3
    '    identifier3("type") = type3

    '    Dim assigner3 As New JObject()
    '    assigner3("display") = "KEMENDAGRI"
    '    identifier3("assigner") = assigner3

    '    identifiersArray.Add(identifier3)

    '    patientObj("identifier") = identifiersArray

    '    ' Patient name
    '    Dim nameArray As New JArray()
    '    Dim nameObj As New JObject()
    '    nameObj("use") = "official"
    '    nameObj("text") = "MITA"
    '    nameArray.Add(nameObj)
    '    patientObj("name") = nameArray

    '    ' Marital status
    '    Dim maritalStatusObj As New JObject()
    '    Dim maritalCoding As New JArray()
    '    Dim maritalCodingItem As New JObject()
    '    maritalCodingItem("system") = "http://terminology.hl7.org/CodeSystem/v3-MaritalStatus"
    '    maritalCodingItem("code") = "U"
    '    maritalCodingItem("display") = "Unmarried"
    '    maritalCoding.Add(maritalCodingItem)
    '    maritalStatusObj("coding") = maritalCoding
    '    patientObj("maritalStatus") = maritalStatusObj

    '    ' Telecom
    '    Dim telecomArray As New JArray()

    '    Dim telecom1 As New JObject()
    '    telecom1("system") = "phone"
    '    telecom1("value") = ""
    '    telecom1("use") = "work"
    '    telecomArray.Add(telecom1)

    '    Dim telecom2 As New JObject()
    '    telecom2("system") = "phone"
    '    telecom2("value") = "08101010101"
    '    telecom2("use") = "mobile"
    '    telecomArray.Add(telecom2)

    '    Dim telecom3 As New JObject()
    '    telecom3("system") = "phone"
    '    telecom3("value") = "TDK ADA"
    '    telecom3("use") = "home"
    '    telecomArray.Add(telecom3)

    '    patientObj("telecom") = telecomArray
    '    patientObj("gender") = "female"
    '    patientObj("birthDate") = "1997-12-29"
    '    patientObj("deceasedBoolean") = False

    '    ' Address
    '    Dim addressArray As New JArray()
    '    Dim addressObj As New JObject()
    '    Dim lineArray As New JArray()
    '    lineArray.Add("JL JAYA NO. 119 RT 10000 RW 003")
    '    addressObj("line") = lineArray
    '    addressObj("city") = ""
    '    addressObj("district") = "PULO GADING"
    '    addressObj("state") = ""
    '    addressObj("postalCode") = "1311260"
    '    addressObj("text") = "JL JAYA NO. 119 RT 10000 RW 003  PULO GADING  1311260"
    '    addressObj("use") = "home"
    '    addressObj("type") = "both"
    '    addressArray.Add(addressObj)
    '    patientObj("address") = addressArray

    '    ' Managing organization
    '    Dim managingOrg As New JObject()
    '    managingOrg("reference") = "Organization/" & fn_UDD(sPPKPELAYANAN, sRMEBPJS_koderskemenkes, 2)
    '    managingOrg("display") = "RSIA LEMBANG"
    '    patientObj("managingOrganization") = managingOrg

    '    entry1("resource") = patientObj
    '    entryArray.Add(entry1)

    '    ' Entry 2 - Encounter
    '    Dim entry2 As New JObject()
    '    Dim encounterObj As New JObject()
    '    encounterObj("resourceType") = "Encounter"
    '    encounterObj("id") = fn_UDD(sPPKPELAYANAN, sRMEBPJS_koderskemenkes, 2)

    '    ' Encounter identifier
    '    Dim encounterIdentifier As New JArray()
    '    Dim encId As New JObject()
    '    encId("system") = "http://api.bpjs-kesehatan.go.id:8080/Vclaim-rest/SEP/"
    '    encId("value") = "0464R0120326V000001"
    '    encounterIdentifier.Add(encId)
    '    encounterObj("identifier") = encounterIdentifier

    '    ' Subject
    '    Dim subjectObj As New JObject()
    '    subjectObj("reference") = "Patient/" & fn_UDD(sPPKPELAYANAN, sRMEBPJS_koderskemenkes, 2)
    '    subjectObj("display") = "MITA"
    '    subjectObj("noSep") = "0464R0120326V000001"
    '    encounterObj("subject") = subjectObj

    '    ' Class
    '    Dim classObj As New JObject()
    '    classObj("system") = "http://hl7.org/fhir/v3/ActCode"
    '    classObj("code") = "IMP"
    '    classObj("display") = "inpatient encounter"
    '    encounterObj("class") = classObj

    '    ' Incoming referral
    '    Dim incomingReferral As New JArray()
    '    Dim referralObj As New JObject()
    '    Dim referralIdentifier As New JArray()

    '    Dim refId1 As New JObject()
    '    refId1("system") = "nomor_rujukan_bpjs"
    '    refId1("value") = "diehr belum disimpan"
    '    referralIdentifier.Add(refId1)

    '    Dim refId2 As New JObject()
    '    refId2("system") = "nomor_rujukan_internal_rs"
    '    refId2("value") = "belum di buat"
    '    referralIdentifier.Add(refId2)

    '    referralObj("identifier") = referralIdentifier
    '    incomingReferral.Add(referralObj)
    '    encounterObj("incomingReferral") = incomingReferral

    '    ' Reason
    '    Dim reasonArray As New JArray()
    '    Dim reasonObj As New JObject()
    '    Dim reasonCoding As New JArray()
    '    Dim reasonCodingItem As New JObject()
    '    reasonCodingItem("code") = ""
    '    reasonCodingItem("display") = "LUKA BAKAR 52% TBSA"
    '    reasonCodingItem("system") = "http://hl7.org/fhir/sid/icd-10"
    '    reasonCoding.Add(reasonCodingItem)
    '    reasonObj("coding") = reasonCoding
    '    reasonObj("text") = "LUKA BAKAR 52% TBSA"
    '    reasonArray.Add(reasonObj)
    '    encounterObj("reason") = reasonArray

    '    ' Diagnosis
    '    Dim diagnosisArray As New JArray()

    '    ' Diagnosis 1
    '    Dim diag1 As New JObject()
    '    Dim condition1 As New JObject()
    '    condition1("reference") = "Condition/" & fn_UDD(sPPKPELAYANAN, sRMEBPJS_koderskemenkes, 2)

    '    Dim role1 As New JObject()
    '    Dim roleCoding1 As New JArray()
    '    Dim roleCodingItem1 As New JObject()
    '    roleCodingItem1("system") = "http://hl7.org/fhir/diagnosis-role"
    '    roleCodingItem1("code") = "DD"
    '    roleCodingItem1("display") = "Discharge Diagnosis"
    '    roleCoding1.Add(roleCodingItem1)
    '    role1("coding") = roleCoding1
    '    condition1("role") = role1
    '    condition1("rank") = 1
    '    diag1("condition") = condition1
    '    diagnosisArray.Add(diag1)

    '    ' Diagnosis 2
    '    Dim diag2 As New JObject()
    '    Dim condition2 As New JObject()
    '    condition2("reference") = "Condition/" & fn_UDD(sPPKPELAYANAN, sRMEBPJS_koderskemenkes, 2)

    '    Dim role2 As New JObject()
    '    Dim roleCoding2 As New JArray()
    '    Dim roleCodingItem2 As New JObject()
    '    roleCodingItem2("system") = "http://hl7.org/fhir/diagnosis-role"
    '    roleCodingItem2("code") = "DD"
    '    roleCodingItem2("display") = "Discharge Diagnosis"
    '    roleCoding2.Add(roleCodingItem2)
    '    role2("coding") = roleCoding2
    '    condition2("role") = role2
    '    condition2("rank") = 2
    '    diag2("condition") = condition2
    '    diagnosisArray.Add(diag2)

    '    ' Diagnosis 3
    '    Dim diag3 As New JObject()
    '    Dim condition3 As New JObject()
    '    condition3("reference") = "Condition/" & fn_UDD(sPPKPELAYANAN, sRMEBPJS_koderskemenkes, 2)

    '    Dim role3 As New JObject()
    '    Dim roleCoding3 As New JArray()
    '    Dim roleCodingItem3 As New JObject()
    '    roleCodingItem3("system") = "http://hl7.org/fhir/diagnosis-role"
    '    roleCodingItem3("code") = "DD"
    '    roleCodingItem3("display") = "Discharge Diagnosis"
    '    roleCoding3.Add(roleCodingItem3)
    '    role3("coding") = roleCoding3
    '    condition3("role") = role3
    '    condition3("rank") = 3
    '    diag3("condition") = condition3
    '    diagnosisArray.Add(diag3)

    '    ' Diagnosis 4
    '    Dim diag4 As New JObject()
    '    Dim condition4 As New JObject()
    '    condition4("reference") = "Condition/" & fn_UDD(sPPKPELAYANAN, sRMEBPJS_koderskemenkes, 2)

    '    Dim role4 As New JObject()
    '    Dim roleCoding4 As New JArray()
    '    Dim roleCodingItem4 As New JObject()
    '    roleCodingItem4("system") = "http://hl7.org/fhir/diagnosis-role"
    '    roleCodingItem4("code") = "DD"
    '    roleCodingItem4("display") = "Discharge Diagnosis"
    '    roleCoding4.Add(roleCodingItem4)
    '    role4("coding") = roleCoding4
    '    condition4("role") = role4
    '    condition4("rank") = 4
    '    diag4("condition") = condition4
    '    diagnosisArray.Add(diag4)

    '    encounterObj("diagnosis") = diagnosisArray

    '    ' Hospitalization
    '    Dim hospitalizationObj As New JObject()
    '    Dim dischargeDisposition As New JArray()
    '    Dim dispositionObj As New JObject()
    '    Dim dispositionCoding As New JArray()
    '    Dim dispositionCodingItem As New JObject()
    '    dispositionCodingItem("code") = "home"
    '    dispositionCodingItem("display") = "Home"
    '    dispositionCodingItem("system") = "http://hl7.org/fhir/discharge-disposition"
    '    dispositionCoding.Add(dispositionCodingItem)
    '    dispositionObj("coding") = dispositionCoding
    '    dischargeDisposition.Add(dispositionObj)
    '    hospitalizationObj("dischargeDisposition") = dischargeDisposition
    '    encounterObj("hospitalization") = hospitalizationObj

    '    ' Period
    '    Dim periodObj As New JObject()
    '    periodObj("end") = "2018-09-13 18:11:00"
    '    periodObj("start") = "2018-08-15 04:21:36"
    '    encounterObj("period") = periodObj

    '    encounterObj("status") = "finished"

    '    ' Text
    '    Dim textObj As New JObject()
    '    textObj("div") = "Admitted to Instalasi Gawat Darurat,Cipto Mangunkusumo Hospital between 15 Agustus 2018 04:21 and 13 September 2018 18:11"
    '    textObj("status") = "generated"
    '    encounterObj("text") = textObj

    '    entry2("resource") = encounterObj
    '    entryArray.Add(entry2)

    '    ' Entry 3 - Procedure
    '    Dim entry3 As New JObject()
    '    Dim procedureArray As New JArray()
    '    Dim procedureObj As New JObject()
    '    procedureObj("resourceType") = "Procedure"
    '    procedureObj("id") = fn_UDD(sPPKPELAYANAN, sRMEBPJS_koderskemenkes, 2)

    '    ' Procedure text
    '    Dim procText As New JObject()
    '    procText("status") = "generated"
    '    procText("div") = "Generated Narrative with Details"
    '    procedureObj("text") = procText

    '    procedureObj("status") = "completed"

    '    ' Procedure code
    '    Dim procCode As New JObject()
    '    Dim procCoding As New JArray()
    '    Dim procCodingItem As New JObject()
    '    procCodingItem("system") = "http:\\/\\/snomed.info\\/sct"
    '    procCodingItem("code") = "PROCx000032816"
    '    procCodingItem("display") = "Triage"
    '    procCoding.Add(procCodingItem)
    '    procCode("coding") = procCoding
    '    procedureObj("code") = procCode

    '    ' Procedure subject
    '    Dim procSubject As New JObject()
    '    procSubject("reference") = "Patient\\/" & fn_UDD(sPPKPELAYANAN, sRMEBPJS_koderskemenkes, 2)
    '    procSubject("display") = "NURUL"
    '    procedureObj("subject") = procSubject

    '    ' Procedure context
    '    Dim procContext As New JObject()
    '    procContext("reference") = "Encounter\\/" & fn_UDD(sPPKPELAYANAN, sRMEBPJS_koderskemenkes, 2)
    '    procContext("display") = "NURUL encounter on 31 Desember 2018 17:08"
    '    procedureObj("context") = procContext

    '    ' Performed period
    '    Dim performedPeriod As New JObject()
    '    performedPeriod("start") = "2025-09-04 16:00:00"
    '    performedPeriod("end") = "2025-09-04 17:08:00"
    '    procedureObj("performedPeriod") = performedPeriod

    '    ' Performer
    '    Dim performerArray As New JArray()
    '    Dim performerObj As New JObject()

    '    Dim performerRole As New JObject()
    '    Dim performerRoleCoding As New JArray()
    '    Dim performerRoleCodingItem As New JObject()
    '    performerRoleCodingItem("system") = "http:\\/\\/snomed.info\\/sct"
    '    performerRoleCodingItem("code") = "310512001"
    '    performerRoleCodingItem("display") = "Medical oncologist"
    '    performerRoleCoding.Add(performerRoleCodingItem)
    '    performerRole("coding") = performerRoleCoding
    '    performerObj("role") = performerRole

    '    Dim performerActor As New JObject()
    '    performerActor("reference") = "Practitioner\\/" & fn_UDD(sPPKPELAYANAN, sRMEBPJS_koderskemenkes, 2)
    '    performerActor("display") = "Septi Sari Yanti"
    '    performerObj("actor") = performerActor

    '    performerArray.Add(performerObj)
    '    procedureObj("performer") = performerArray

    '    ' Reason code
    '    Dim reasonCodeArray As New JArray()
    '    Dim reasonCodeObj As New JObject()
    '    reasonCodeObj("text") = "DiagnosticReport\\/f201"
    '    reasonCodeArray.Add(reasonCodeObj)
    '    procedureObj("reasonCode") = reasonCodeArray

    '    ' Body site
    '    Dim bodySiteArray As New JArray()
    '    Dim bodySiteObj As New JObject()
    '    Dim bodySiteCoding As New JArray()
    '    Dim bodySiteCodingItem As New JObject()
    '    bodySiteCodingItem("system") = "http:\\/\\/snomed.info\\/sct"
    '    bodySiteCodingItem("code") = "272676008"
    '    bodySiteCodingItem("display") = "Sphenoid bone"
    '    bodySiteCoding.Add(bodySiteCodingItem)
    '    bodySiteObj("coding") = bodySiteCoding
    '    bodySiteArray.Add(bodySiteObj)
    '    procedureObj("bodySite") = bodySiteArray

    '    ' Focal device
    '    Dim focalDeviceArray As New JArray()
    '    Dim focalDeviceObj As New JObject()

    '    Dim deviceAction As New JObject()
    '    Dim actionCoding As New JArray()
    '    Dim actionCodingItem As New JObject()
    '    actionCodingItem("system") = "http:\\/\\/hl7.org\\/fhir\\/device-action"
    '    actionCodingItem("code") = "implanted"
    '    actionCoding.Add(actionCodingItem)
    '    deviceAction("coding") = actionCoding
    '    focalDeviceObj("action") = deviceAction

    '    Dim manipulatedObj As New JObject()
    '    manipulatedObj("reference") = "Device\\/example-pacemaker"
    '    focalDeviceObj("manipulated") = manipulatedObj

    '    focalDeviceArray.Add(focalDeviceObj)
    '    procedureObj("focalDevice") = focalDeviceArray

    '    ' Note
    '    Dim noteArray As New JArray()
    '    Dim noteObj As New JObject()
    '    noteObj("text") = ""
    '    noteArray.Add(noteObj)
    '    procedureObj("note") = noteArray

    '    procedureArray.Add(procedureObj)
    '    entry3("resource") = procedureArray
    '    entryArray.Add(entry3)

    '    ' Entry 4 - Organization
    '    Dim entry4 As New JObject()
    '    Dim orgObj As New JObject()
    '    orgObj("resourceType") = "Organization"
    '    orgObj("id") = fn_UDD(sPPKPELAYANAN, sRMEBPJS_koderskemenkes, 2)

    '    ' Organization identifiers
    '    Dim orgIdentifiers As New JArray()

    '    Dim orgId1 As New JObject()
    '    orgId1("use") = "official"
    '    orgId1("system") = "urn:oid:bpjs"
    '    orgId1("value") = "1002R007"
    '    orgIdentifiers.Add(orgId1)

    '    Dim orgId2 As New JObject()
    '    orgId2("use") = "official"
    '    orgId2("system") = "urn:oid:kemkes"
    '    orgId2("value") = "3173014"
    '    orgIdentifiers.Add(orgId2)

    '    orgObj("identifier") = orgIdentifiers

    '    ' Organization type
    '    Dim orgTypeArray As New JArray()
    '    Dim orgTypeObj As New JObject()
    '    Dim orgTypeCoding As New JArray()
    '    Dim orgTypeCodingItem As New JObject()
    '    orgTypeCodingItem("system") = "http://hl7.org/fhir/organization-type"
    '    orgTypeCodingItem("code") = "prov"
    '    orgTypeCodingItem("display") = "Healthcare Provider"
    '    orgTypeCoding.Add(orgTypeCodingItem)
    '    orgTypeObj("coding") = orgTypeCoding
    '    orgTypeArray.Add(orgTypeObj)
    '    orgObj("type") = orgTypeArray

    '    orgObj("name") = "IGD RS DUSTIRA"

    '    ' Alias
    '    Dim aliasArray As New JArray()
    '    aliasArray.Add("RS DUSTIRA")
    '    orgObj("alias") = aliasArray

    '    ' Telecom
    '    Dim orgTelecomArray As New JArray()
    '    Dim orgTelecomObj As New JObject()
    '    orgTelecomObj("system") = "phone"
    '    orgTelecomObj("value") = "0261-123456"
    '    orgTelecomObj("use") = "work"
    '    orgTelecomArray.Add(orgTelecomObj)
    '    orgObj("telecom") = orgTelecomArray

    '    ' Address
    '    Dim orgAddressArray As New JArray()
    '    Dim orgAddressObj As New JObject()
    '    orgAddressObj("use") = "work"
    '    orgAddressObj("text") = "Jl. Pangeran Diponegoro No. 71, Kenari, Senen, RW. 5, Kenari, RW.5, Kenari, Senen, Jakarta Pusat, Daerah Khusus Ibukota Jakarta, 10430, Indonesia"

    '    Dim orgLineArray As New JArray()
    '    orgLineArray.Add("Jl. Pangeran Diponegoro No. 71, Kenari, Senen, RW. 5, Kenari, RW.5, Kenari, Senen")
    '    orgAddressObj("line") = orgLineArray
    '    orgAddressObj("city") = "Jakarta Pusat"
    '    orgAddressObj("state") = "Daerah Khusus Ibukota Jakarta"
    '    orgAddressObj("postalCode") = "10430"
    '    orgAddressObj("country") = "IDN"
    '    orgAddressArray.Add(orgAddressObj)
    '    orgObj("address") = orgAddressArray

    '    ' Contact
    '    Dim orgContactArray As New JArray()
    '    Dim orgContactObj As New JObject()

    '    Dim contactPurpose As New JObject()
    '    Dim purposeCoding As New JArray()
    '    Dim purposeCodingItem As New JObject()
    '    purposeCodingItem("system") = "http://hl7.org/fhir/contactentity-type"
    '    purposeCodingItem("code") = "PATINF"
    '    purposeCoding.Add(purposeCodingItem)
    '    contactPurpose("coding") = purposeCoding
    '    orgContactObj("purpose") = contactPurpose

    '    Dim contactTelecom As New JArray()
    '    Dim contactTelecomObj As New JObject()
    '    contactTelecomObj("system") = "phone"
    '    contactTelecomObj("value") = "1500-135"
    '    contactTelecom.Add(contactTelecomObj)
    '    orgContactObj("telecom") = contactTelecom

    '    orgContactArray.Add(orgContactObj)
    '    orgObj("contact") = orgContactArray

    '    entry4("resource") = orgObj
    '    entryArray.Add(entry4)

    '    dataMRObj("entry") = entryArray
    '    requestObj("dataMR") = dataMRObj

    '    ' Menggabungkan ke dalam objek utama
    '    Dim wrapperObj As New JObject()
    '    wrapperObj("request") = dataMRObj

    '    ' Mengkonversi ke string JSON
    '    Return wrapperObj.ToString(Formatting.Indented)
    'End Function
#End Region
#Region "RME Satu Sehat"
    Private Function CekNIKPasien(kdcustomer As String, nik As String, ByVal Pesan As Boolean) As String
        Try
            CekNIKPasien = ""

            Dim dsCustomerSatusehatCek = oCustomerSatuSehat.GetData(kdcustomer)

            If dsCustomerSatusehatCek Is Nothing Then
                Dim respon As String = SatusehatAuth.PatientByNIK(SatuSehat_Production, SatuSehat_token, nik)

                If respon.Contains("200") Then
                    CekNIKPasien = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "id")

                    If fn_SaveSatuSehatCustomer(kdcustomer, CekNIKPasien, "", Regex.Replace(respon, "^.{4}", "")) = False Then
                        CekNIKPasien = ""

                        If Pesan = True Then
                            MsgBox("✗ Gagal Simpan ke database", MsgBoxStyle.Exclamation, Me.Text)
                        End If
                    Else
                        If Pesan = True Then
                            MsgBox("Sudah Masuk dengan Id " & CekNIKPasien, MsgBoxStyle.Exclamation, Me.Text)
                        End If
                    End If
                Else
                    Dim cektoken As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "code")
                    If cektoken = "invalid-access-token" Then
                        Dim token As String = SatusehatAuth.GetAccessToken(SatuSehat_Production, SatuSehat_client_id, SatuSehat_client_secret)

                        SatuSehat_token = SatusehatAuth.GetToken(token, "access_token")

                        Dim oToken As New Setting.clsSatuSehatKoneksiToken

                        Dim dsToken = oToken.GetDataSEQ(IIf(SatuSehat_Production = False, "SANDBOX", "PRODUCTION"))

                        If dsToken IsNot Nothing Then
                            If oToken.UpdateToken(dsToken.KDKONEKSI, dsToken.SEQ, Regex.Replace(respon, "^.{4}", ""), SatuSehat_token) = False Then
                                CekNIKPasien = ""
                                SatuSehat_Organisasi = ""
                                SatuSehat_client_id = ""
                                SatuSehat_client_secret = ""

                                MsgBox("✗ Gagal Simpan Token, Silahkan ulangi dengan close form dan masuk kembali!" & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)

                                Exit Function
                            Else
                                Dim respon2 As String = SatusehatAuth.PatientByNIK(SatuSehat_Production, SatuSehat_token, nik)

                                If respon2.Contains("200") Then
                                    CekNIKPasien = SatusehatAuth.GetToken(Regex.Replace(respon2, "^.{4}", ""), "id")

                                    If fn_SaveSatuSehatCustomer(kdcustomer, CekNIKPasien, "", Regex.Replace(respon2, "^.{4}", "")) = False Then
                                        CekNIKPasien = ""

                                        If Pesan = True Then
                                            MsgBox("✗ Gagal Simpan ke database", MsgBoxStyle.Exclamation, Me.Text)
                                        End If
                                    Else
                                        If Pesan = True Then
                                            MsgBox("Sudah Masuk dengan Id " & CekNIKPasien, MsgBoxStyle.Exclamation, Me.Text)
                                        End If
                                    End If
                                Else
                                    CekNIKPasien = ""
                                    If Pesan = True Then
                                        MsgBox("Gagl Cek Nik Pasien" & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)
                                    End If
                                End If
                            End If
                        Else
                            CekNIKPasien = ""
                            If Pesan = True Then
                                MsgBox("Tidak Ada Token di Database!" & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)
                            End If
                        End If
                    Else
                        CekNIKPasien = ""
                        If Pesan = True Then
                            MsgBox("Gagl Cek Nik Pasien" & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)
                        End If
                    End If
                End If
            Else
                CekNIKPasien = dsCustomerSatusehatCek.IDSATUSEHAT
                If Pesan = True Then
                    MsgBox("Sudah Masuk dengan Id " & CekNIKPasien, MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If
        Catch oErr As Exception
            CekNIKPasien = ""
            If Pesan = True Then
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End If
        End Try
    End Function
    Private Function Ecounter_Admisi_TaksID3(register As String, idpasien As String, snowmed As String, snowmedDisplay As String) As Boolean
        Try
            Ecounter_Admisi_TaksID3 = False

            'oSet_Antrian.GetDataBySaveWaktuTunggu(dsSet_Antrian_Simpan.KODEBOOKING, 7)
            Dim dsPendaftaranSatusehat = oPendaftaran_TaskId3.GetData(register)
            If dsPendaftaranSatusehat Is Nothing Then
                Dim dsPendaftaran = oPendaftaran.GetData(register)
                If dsPendaftaran IsNot Nothing Then
                    Dim dsDoctorSatuSehat = oDoctorSatuSehat.GetData(dsPendaftaran.KDDOCTOR)
                    If dsDoctorSatuSehat IsNot Nothing Then
                        Dim dsLocation = oDepartmentSatuSehatLokasi.GetData(dsPendaftaran.KDDEPARTMENT)
                        If dsLocation IsNot Nothing Then
                            Dim jsondata As String = FhirBundleGenerator.JSONEncounterKunjunganBaru(snowmed, snowmedDisplay, SatuSehat_Organisasi, register, idpasien, dsPendaftaran.M_CUSTOMER.NAME_DISPLAY, dsDoctorSatuSehat.IDSATUSEHAT, dsDoctorSatuSehat.M_DOCTOR.NAME_DISPLAY, dsLocation.IDSATUSEHAT, dsLocation.M_DEPARTMENT.NAME_DISPLAY, dsPendaftaran.DATE.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"))

                            Dim respon As String = SatusehatAuth.EncounterKunjunganBaru(SatuSehat_Production, SatuSehat_token, jsondata)

                            If Not String.IsNullOrEmpty(respon) Then
                                If respon.Contains("200") Or respon.Contains("201") Then
                                    Dim ID As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "id")
                                    If ID <> "" Then
                                        If fn_SaveSatuSehatAdmisi(register, ID, jsondata, Regex.Replace(respon, "^.{4}", "")) = True Then
                                            Ecounter_Admisi_TaksID3 = True
                                        End If
                                    Else
                                        Dim cektoken As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "code")
                                        If cektoken = "duplicate" Then
                                            If fn_SaveSatuSehatAdmisi(register, ID, jsondata, Regex.Replace(respon, "^.{4}", "")) = True Then
                                                Ecounter_Admisi_TaksID3 = True
                                            End If
                                        End If
                                    End If
                                Else
                                    Dim cektoken As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "code")
                                    If cektoken = "invalid-access-token" Then
                                        Dim token As String = SatusehatAuth.GetAccessToken(SatuSehat_Production, SatuSehat_client_id, SatuSehat_client_secret)

                                        SatuSehat_token = SatusehatAuth.GetToken(token, "access_token")

                                        Dim oToken As New Setting.clsSatuSehatKoneksiToken

                                        Dim dsToken = oToken.GetDataSEQ(IIf(SatuSehat_Production = False, "SANDBOX", "PRODUCTION"))

                                        If dsToken IsNot Nothing Then
                                            If oToken.UpdateToken(dsToken.KDKONEKSI, dsToken.SEQ, Regex.Replace(respon, "^.{4}", ""), SatuSehat_token) = False Then
                                                SatuSehat_Organisasi = ""
                                                SatuSehat_client_id = ""
                                                SatuSehat_client_secret = ""

                                                MsgBox("✗ Gagal Simpan Token!" & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)

                                                Exit Function
                                            Else
                                                Dim responulang As String = SatusehatAuth.EncounterKunjunganBaru(SatuSehat_Production, SatuSehat_token, jsondata)

                                                Dim ID As String = SatusehatAuth.GetToken(Regex.Replace(responulang, "^.{4}", ""), "id")

                                                If ID <> "" Then
                                                    If fn_SaveSatuSehatAdmisi(register, ID, jsondata, Regex.Replace(responulang, "^.{4}", "")) = True Then
                                                        Ecounter_Admisi_TaksID3 = True
                                                    End If
                                                Else
                                                    Dim cektoken2 As String = SatusehatAuth.GetToken(Regex.Replace(responulang, "^.{4}", ""), "code")
                                                    If cektoken2 = "duplicate" Then
                                                        If fn_SaveSatuSehatAdmisi(register, ID, jsondata, Regex.Replace(responulang, "^.{4}", "")) = True Then
                                                            Ecounter_Admisi_TaksID3 = True
                                                        End If
                                                    End If
                                                End If
                                            End If
                                        Else
                                            SatuSehat_Organisasi = ""
                                            SatuSehat_client_id = ""
                                            SatuSehat_client_secret = ""

                                            MsgBox("✗ Token Database Kosong, Silahkan Add Terlebih Dahulu di menu Setting!" & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)

                                            Exit Function
                                        End If
                                    ElseIf cektoken = "duplicate"
                                        If fn_SaveSatuSehatAdmisi(register, "", jsondata, Regex.Replace(respon, "^.{4}", "")) = True Then
                                            Ecounter_Admisi_TaksID3 = True
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            Else
                Ecounter_Admisi_TaksID3 = True
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function Ecounter_Admisi_TaksID4(register As String, idpasien As String, snowmed As String, snowmedDisplay As String, ByVal idreg As String) As Boolean
        Try
            Ecounter_Admisi_TaksID4 = False

            Dim dsPendaftaran2 = oPendaftaran.GetData(register)
            If dsPendaftaran2 IsNot Nothing Then
                Dim dsDoctorSatuSehat2 = oDoctorSatuSehat.GetData(dsPendaftaran2.KDDOCTOR)
                If dsDoctorSatuSehat2 IsNot Nothing Then
                    Dim dsLocation2 = oDepartmentSatuSehatLokasi.GetData(dsPendaftaran2.KDDEPARTMENT)
                    If dsLocation2 IsNot Nothing Then

                        Dim endwaktu As String = String.Empty

                        If dsPendaftaran2.KODEBOOKING <> "" Then
                            Dim dsTaksId4 = oSet_Antrian.GetDataBySaveWaktuTunggu(dsPendaftaran2.KODEBOOKING, 4)

                            If dsTaksId4 IsNot Nothing Then
                                endwaktu = dsTaksId4.DATE.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00")
                            End If
                        End If

                        If endwaktu = "" Then
                            Dim dsCPPT = oGrouperDataCppt.GetDataByKodePendaftaranRawatJalanDokter(dsPendaftaran2.KDPENDAFTARAN)
                            If dsCPPT IsNot Nothing Then
                                endwaktu = dsCPPT.DATE.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00")
                            End If
                        End If

                        Dim jsondata As String = FhirBundleGenerator.JSONEncounterMasukRuang(idreg, snowmed, snowmedDisplay, SatuSehat_Organisasi, register, idpasien, dsPendaftaran2.M_CUSTOMER.NAME_DISPLAY, dsDoctorSatuSehat2.IDSATUSEHAT, dsDoctorSatuSehat2.M_DOCTOR.NAME_DISPLAY, dsLocation2.IDSATUSEHAT, dsLocation2.M_DEPARTMENT.NAME_DISPLAY, dsPendaftaran2.DATE.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), endwaktu)

                        Dim respon As String = SatusehatAuth.EncounterKunjunganBaruTaksi4(SatuSehat_Production, SatuSehat_token, jsondata, idreg)

                        If Not String.IsNullOrEmpty(respon) Then
                            If respon.Contains("200") Or respon.Contains("201") Then
                                Dim ID As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "id")
                                If ID <> "" Then
                                    If fn_SaveSatuSehatAdmisi(register, ID, jsondata, Regex.Replace(respon, "^.{4}", "")) = True Then
                                        Ecounter_Admisi_TaksID4 = True
                                    End If
                                Else
                                    Dim cektoken As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "code")
                                    If cektoken = "duplicate" Then
                                        If fn_SaveSatuSehatAdmisi(register, ID, jsondata, Regex.Replace(respon, "^.{4}", "")) = True Then
                                            Ecounter_Admisi_TaksID4 = True
                                        End If
                                    End If
                                End If
                            Else
                                Dim cektoken As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "code")
                                If cektoken = "invalid-access-token" Then
                                    Dim token As String = SatusehatAuth.GetAccessToken(SatuSehat_Production, SatuSehat_client_id, SatuSehat_client_secret)

                                    SatuSehat_token = SatusehatAuth.GetToken(token, "access_token")

                                    Dim oToken As New Setting.clsSatuSehatKoneksiToken

                                    Dim dsToken = oToken.GetDataSEQ(IIf(SatuSehat_Production = False, "SANDBOX", "PRODUCTION"))

                                    If dsToken IsNot Nothing Then
                                        If oToken.UpdateToken(dsToken.KDKONEKSI, dsToken.SEQ, Regex.Replace(respon, "^.{4}", ""), SatuSehat_token) = False Then
                                            SatuSehat_Organisasi = ""
                                            SatuSehat_client_id = ""
                                            SatuSehat_client_secret = ""

                                            MsgBox("✗ Gagal Simpan Token!" & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)

                                            Exit Function
                                        Else
                                            Dim responulang As String = SatusehatAuth.EncounterKunjunganBaruTaksi4(SatuSehat_Production, SatuSehat_token, jsondata, idreg)

                                            Dim ID As String = SatusehatAuth.GetToken(Regex.Replace(responulang, "^.{4}", ""), "id")

                                            If ID <> "" Then
                                                If fn_SaveSatuSehatAdmisi(register, ID, jsondata, Regex.Replace(responulang, "^.{4}", "")) = True Then
                                                    Ecounter_Admisi_TaksID4 = True
                                                End If
                                            Else
                                                Dim cektoken2 As String = SatusehatAuth.GetToken(Regex.Replace(responulang, "^.{4}", ""), "code")
                                                If cektoken2 = "duplicate" Then
                                                    If fn_SaveSatuSehatAdmisi(register, ID, jsondata, Regex.Replace(responulang, "^.{4}", "")) = True Then
                                                        Ecounter_Admisi_TaksID4 = True
                                                    End If
                                                End If
                                            End If
                                        End If
                                    Else
                                        SatuSehat_Organisasi = ""
                                        SatuSehat_client_id = ""
                                        SatuSehat_client_secret = ""

                                        MsgBox("✗ Token Database Kosong, Silahkan Add Terlebih Dahulu di menu Setting!" & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)

                                        Exit Function
                                    End If
                                ElseIf cektoken = "duplicate"
                                    If fn_SaveSatuSehatAdmisi(register, "", jsondata, Regex.Replace(respon, "^.{4}", "")) = True Then
                                        Ecounter_Admisi_TaksID4 = True
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function kirimSatuSehat(ByVal category As String, ByVal jsondata As String) As String
        Try
            kirimSatuSehat = ""

            If category = "KELUHAN UTAMA" Then
                kirimSatuSehat = SatusehatAuth.ConditionKeluhanUtama(SatuSehat_Production, SatuSehat_token, jsondata)
            ElseIf category = "KELUHAN PENYERTA"
                kirimSatuSehat = SatusehatAuth.ConditionKeluhanPenyerta(SatuSehat_Production, SatuSehat_token, jsondata)
            ElseIf category = "OBSERVATION - SISTOLIK"
                kirimSatuSehat = SatusehatAuth.Obervasi(SatuSehat_Production, SatuSehat_token, jsondata)
            ElseIf category = "OBSERVATION - DIASTOLIK"
                kirimSatuSehat = SatusehatAuth.Obervasi(SatuSehat_Production, SatuSehat_token, jsondata)
            ElseIf category = "SUHU TUBUH"
                kirimSatuSehat = SatusehatAuth.Obervasi(SatuSehat_Production, SatuSehat_token, jsondata)
            ElseIf category = "DENYUT JANTUNG"
                kirimSatuSehat = SatusehatAuth.Obervasi(SatuSehat_Production, SatuSehat_token, jsondata)
            ElseIf category = "PERNAPASAN"
                kirimSatuSehat = SatusehatAuth.Obervasi(SatuSehat_Production, SatuSehat_token, jsondata)
            ElseIf category = "TINGKAT KESADARAN"
                kirimSatuSehat = SatusehatAuth.Obervasi(SatuSehat_Production, SatuSehat_token, jsondata)
            ElseIf category = "TINGGI BADAN"
                kirimSatuSehat = SatusehatAuth.Obervasi(SatuSehat_Production, SatuSehat_token, jsondata)
            ElseIf category = "BERAT BADAN"
                kirimSatuSehat = SatusehatAuth.Obervasi(SatuSehat_Production, SatuSehat_token, jsondata)
            ElseIf category = "RIWAYAT PERJALANAN PENYAKIT"
                kirimSatuSehat = SatusehatAuth.ClinicalImpression(SatuSehat_Production, SatuSehat_token, jsondata)
                'ElseIf category = "TUJUAN PERAWATAN"
                '    kirimSatuSehat = SatusehatAuth.TUJU(SatuSehat_Production, SatuSehat_token, jsondata)
                'ElseIf category = "RENCANA RAWAT JALAN"
                '    kirimSatuSehat = SatusehatAuth.REN(SatuSehat_Production, SatuSehat_token, jsondata)
            End If
        Catch ex As Exception
            kirimSatuSehat = "Exception: " & ex.Message
        End Try
    End Function
    Private Sub grv_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grv.FocusedRowChanged
        If grv.GetFocusedRowCellValue("KDPENDAFTARAN") Is Nothing Then
            Exit Sub
        End If

        Dim dsCe = oPendaftaran.GetData(grv.GetFocusedRowCellValue("KDPENDAFTARAN"))
        If dsCe IsNot Nothing Then
            txtBulan.Text = CInt(dsCe.DATE.ToString("MM"))
            txtTahun.Text = CInt(dsCe.DATE.ToString("yyyy"))
            txtJenisKunjungan.Text = IIf(dsCe.CATEGORY = 0, "2", "1")
        End If

    End Sub
    Private Sub BundleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BundleToolStripMenuItem.Click
        KirimBPJS("Bundle")
    End Sub
    Private Sub PatientToolStripMenuItem_Click(sender As Object, e As EventArgs)
        KirimBPJS("Patient")
    End Sub
    Private Sub CompotionToolStripMenuItem_Click(sender As Object, e As EventArgs)
        KirimBPJS("Composition")
    End Sub

    Private Sub cboTYPE_SelectedIndexChanged() Handles cboTYPE.SelectedIndexChanged
        If cboTYPE.Text = "Rekap BPJS RME" Then
            sTipe = 0
            lblSatuSehat.Visible = False
            picKirimSatuSehat.Visible = False

            lblBPJS.Visible = True
            picKirimBPJS.Visible = True

            grd.ContextMenuStrip = Nothing

        Else
            sTipe = 1
            lblSatuSehat.Visible = True
            picKirimSatuSehat.Visible = True

            lblBPJS.Visible = False
            picKirimBPJS.Visible = False

            grd.ContextMenuStrip = ContextMenuStrip3

        End If
    End Sub
#End Region
#Region "Satu Sehat"
#Region "Klik"
    'Kirim Looping
    Private Sub PasienToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasienToolStripMenuItem.Click
        Try
            If SatuSehat_Organisasi = "" Then
                MsgBox("Organisasi ID masih kosong", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            Dim counter As Integer = 0
            Dim counterAll As Integer = 0
            Dim counterGagal As Integer = 0
            Dim listNIK As New List(Of String)
            Dim oSalesOrderTransaksi As New Sales.clsSalesOrderTransaksi
            Dim arrDetailCekData = oSalesOrderTransaksi.GetStructureDetailListCek
            Dim idCek As Integer = 0

            For i As Integer = 0 To grv.RowCount - 1
                If grv.GetRowCellValue(i, "IDPASIEN") = "" Then
                    If grv.GetRowCellValue(i, "KTP") <> "" Then
                        If grv.GetRowCellValue(i, "KTP") <> "-" Then
                            Dim dsDetailCekData = oSalesOrderTransaksi.GetStructureDetailCekdata
                            Dim cek As Boolean = False

                            With dsDetailCekData
                                If arrDetailCekData.Count <= 0 Then
                                    .ID = idCek
                                    .KDCUSTOMER = grv.GetRowCellValue(i, "KDCUSTOMER")
                                    .KTP = grv.GetRowCellValue(i, "KTP")
                                    .MEMO = ""
                                    idCek += 1
                                    cek = True
                                Else
                                    Dim RMCARI As String = grv.GetRowCellValue(i, "KDCUSTOMER")
                                    Dim dsCekDouble = arrDetailCekData.Where(Function(x) x.KDCUSTOMER = RMCARI)
                                    If dsCekDouble.Count <= 0 Then
                                        .ID = idCek
                                        .KDCUSTOMER = grv.GetRowCellValue(i, "KDCUSTOMER")
                                        .KTP = grv.GetRowCellValue(i, "KTP")
                                        .MEMO = ""
                                        idCek += 1
                                        cek = True
                                    End If
                                End If

                            End With

                            If cek = True Then
                                arrDetailCekData.Add(dsDetailCekData)
                            End If

                        End If
                    End If
                End If
            Next

            For Each xloop In arrDetailCekData
                counterAll += 1
            Next

            If counterAll <= 0 Then
                SplashScreenManager.CloseForm(False)
                MsgBox("Tidak Ada data yg dikirim", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            If MsgBox("Apakah Akan Kirim Data Ke Satu Sehat Sebanyak " & counterAll & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then
                SplashScreenManager.CloseForm(False)
                Exit Sub
            End If

            For Each xloop In arrDetailCekData
                If CekNIKPasien(xloop.KDCUSTOMER, xloop.KTP, False) = "" Then
                    counterGagal += 1
                Else
                    counter += 1
                End If
            Next

            'For i As Integer = 0 To grv.RowCount - 1
            '    If grv.GetFocusedRowCellValue("IDPASIEN") = "" Then
            '        If grv.GetRowCellValue(i, "KTP") <> "" Then
            '            If grv.GetFocusedRowCellValue("KTP") <> "-" Then
            '                If CekNIKPasien(grv.GetRowCellValue(i, "KDCUSTOMER"), grv.GetRowCellValue(i, "KTP"), False) = "" Then
            '                    counterGagal += 1
            '                Else
            '                    counter += 1
            '                End If
            '            Else
            '                counter += 1
            '            End If
            '        Else
            '            counterGagal += 1
            '        End If
            '    Else
            '        counterGagal += 1
            '    End If

            'Next

            SplashScreenManager.CloseForm(False)

            MsgBox("Kirim Data : " & counterAll & vbCrLf & "*Gagal : " & counterGagal & vbCrLf & "*Berhasil : " & counter, MsgBoxStyle.Information, Me.Text)

            fn_LoadSecurity()

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Looping" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    '---------------------------
    Private Sub CekPasienToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CekPasienToolStripMenuItem.Click
        If SatuSehat_Organisasi = "" Then
            MsgBox("Organisasi ID masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If grv.GetFocusedRowCellValue("KDCUSTOMER") Is Nothing Then
            Exit Sub
        End If

        If grv.GetFocusedRowCellValue("KTP") <> "" Then
            If grv.GetFocusedRowCellValue("IDPASIEN") = "" Then
                CekNIKPasien(grv.GetFocusedRowCellValue("KDCUSTOMER"), grv.GetFocusedRowCellValue("KTP"), True)
            Else
                MsgBox("Id Sudah Ada", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Else
            MsgBox("KTP Kosong", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub UpdateIdToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles UpdateIdToolStripMenuItem1.Click
        If SatuSehat_Organisasi = "" Then
            MsgBox("Organisasi ID masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If grv.GetFocusedRowCellValue("KDCUSTOMER") Is Nothing Then
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("KTP") = "" Then
            MsgBox("KTP KOSONG", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("KTP") = "-" Then
            MsgBox("KTP STRIP", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim dsCustomerSatusehatCek = oCustomerSatuSehat.GetData(grv.GetFocusedRowCellValue("KDCUSTOMER"))

        If dsCustomerSatusehatCek IsNot Nothing Then
            Dim respon As String = SatusehatAuth.PatientByNIK(SatuSehat_Production, SatuSehat_token, grv.GetFocusedRowCellValue("KTP"))

            If fn_SaveSatuSehatCustomer(grv.GetFocusedRowCellValue("KDCUSTOMER"), SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "id"), "", Regex.Replace(respon, "^.{4}", "")) = False Then
                MsgBox("✗ Gagal Simpan ke database", MsgBoxStyle.Exclamation, Me.Text)
            Else
                MsgBox("Sudah Masuk dengan Id " & Regex.Replace(respon, "^.{4}", ""), MsgBoxStyle.Exclamation, Me.Text)
            End If
        Else
            MsgBox("Data Belum Masuk ke database", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub EncounterToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles EncounterToolStripMenuItem1.Click
        If SatuSehat_Organisasi = "" Then
            MsgBox("Organisasi ID masih kosong", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("KDPENDAFTARAN") Is Nothing Then
            MsgBox("Registrasi belum di pilih", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If grv.GetFocusedRowCellValue("IDPASIEN") = "" Then
            MsgBox("Pasien ID masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("IDDOCTOR_SATUSEHAT") = "" Then
            MsgBox("Dokter ID masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("IDORGANIZATION_SATUSEHAT") = "" Then
            MsgBox("Organization ID masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("IDLOCATION_SATUSEHAT") = "" Then
            MsgBox("Location ID masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If Encounter(True, grv.GetFocusedRowCellValue("IDPASIEN"), grv.GetFocusedRowCellValue("NAMAPASIEN"), grv.GetFocusedRowCellValue("IDDOCTOR_SATUSEHAT"), grv.GetFocusedRowCellValue("NAMADOKTER"), CDate(grv.GetFocusedRowCellValue("TANGGALMASUK")), grv.GetFocusedRowCellValue("IDLOCATION_SATUSEHAT"), grv.GetFocusedRowCellValue("NAMALOCATION_SATUSEHAT"), SatuSehat_Organisasi, grv.GetFocusedRowCellValue("KDPENDAFTARAN")) <> "" Then
            MsgBox("Berhasil Kirim", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Gagal Kirim", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub ConditionDiagnosisToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConditionDiagnosisToolStripMenuItem.Click
        If SatuSehat_Organisasi = "" Then
            MsgBox("Organisasi ID masih kosong", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("KDPENDAFTARAN") Is Nothing Then
            MsgBox("Registrasi belum di pilih", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("IDENCOUNTER") = "" Then
            MsgBox("Encounter masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("IDPASIEN") = "" Then
            MsgBox("Pasien ID masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("ICD10_KODE") = "-" Then
            MsgBox("Diagnosa masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("ICD10_KODE") = "" Then
            MsgBox("Diagnosa masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If ConditionDiagnosis(True, grv.GetFocusedRowCellValue("ICD10_KODE"), grv.GetFocusedRowCellValue("ICD10_NAME"), grv.GetFocusedRowCellValue("IDPASIEN"), grv.GetFocusedRowCellValue("NAMAPASIEN"), grv.GetFocusedRowCellValue("KDPENDAFTARAN"), grv.GetFocusedRowCellValue("IDENCOUNTER"), grv.GetFocusedRowCellValue("KETERANGANMASUK")) = True Then
            MsgBox("Berhasil Kirim", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Gagal Kirim", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub ConditionMeninggalkanFaskesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConditionMeninggalkanFaskesToolStripMenuItem.Click
        If SatuSehat_Organisasi = "" Then
            MsgBox("Organisasi ID masih kosong", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("KDPENDAFTARAN") Is Nothing Then
            MsgBox("Registrasi belum di pilih", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("IDENCOUNTER") = "" Then
            MsgBox("Encounter masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("IDPASIEN") = "" Then
            MsgBox("Pasien ID masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("KETERANGANSKD") = "" Then
            MsgBox("SKD ID masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If ConditionMeninggalkanFaskes(True, grv.GetFocusedRowCellValue("IDPASIEN"), grv.GetFocusedRowCellValue("NAMAPASIEN"), grv.GetFocusedRowCellValue("KDPENDAFTARAN"), grv.GetFocusedRowCellValue("IDENCOUNTER"), grv.GetFocusedRowCellValue("KETERANGANMASUK")) = True Then
            MsgBox("Berhasil Kirim", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Gagal Kirim", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub ConditionKeluhanUtamaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConditionKeluhanUtamaToolStripMenuItem.Click
        If SatuSehat_Organisasi = "" Then
            MsgBox("Organisasi ID masih kosong", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("KDPENDAFTARAN") Is Nothing Then
            MsgBox("Registrasi belum di pilih", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("IDENCOUNTER") = "" Then
            MsgBox("Encounter masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("IDPASIEN") = "" Then
            MsgBox("Pasien ID masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("IDDOCTOR_SATUSEHAT") = "" Then
            MsgBox("Dokter ID masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("ICD10_KODE") = "-" Then
            MsgBox("Diagnosa masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim dsCPPT = oGrouperDataCppt.GetDataByKodePendaftaranRawatJalanDokter(grv.GetFocusedRowCellValue("KDPENDAFTARAN"))

        If dsCPPT IsNot Nothing Then
            If ConditionKeluhanUtama(True, grv.GetFocusedRowCellValue("IDPASIEN"), grv.GetFocusedRowCellValue("NAMAPASIEN"), grv.GetFocusedRowCellValue("IDDOCTOR_SATUSEHAT"), grv.GetFocusedRowCellValue("NAMADOKTER"), grv.GetFocusedRowCellValue("ICD10_KODE"), grv.GetFocusedRowCellValue("ICD10_NAME"), grv.GetFocusedRowCellValue("KDPENDAFTARAN"), grv.GetFocusedRowCellValue("IDENCOUNTER"), CDate(grv.GetFocusedRowCellValue("TANGGALMASUK")).ToString("yyyy-MM-ddTHH:mm:ss+00:00", CultureInfo.InvariantCulture), dsCPPT.SUBJEKTIF_KELUHANUTAMA) = True Then
                MsgBox("Berhasil Kirim", MsgBoxStyle.Exclamation, Me.Text)
            Else
                MsgBox("Gagal Kirim", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Else
            MsgBox("CPPT belum di simpan", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub ObservationSistolikToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ObservationSistolikToolStripMenuItem.Click
        If SatuSehat_Organisasi = "" Then
            MsgBox("Organisasi ID masih kosong", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("KDPENDAFTARAN") Is Nothing Then
            MsgBox("Registrasi belum di pilih", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("IDENCOUNTER") = "" Then
            MsgBox("Encounter masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("IDPASIEN") = "" Then
            MsgBox("Pasien ID masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("IDDOCTOR_SATUSEHAT") = "" Then
            MsgBox("Dokter ID masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("ICD10_KODE") = "-" Then
            MsgBox("Diagnosa masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim dsCPPT = oGrouperDataCppt.GetDataByKodePendaftaranRawatJalanDokter(grv.GetFocusedRowCellValue("KDPENDAFTARAN"))

        If dsCPPT IsNot Nothing Then
            If ObservationSistolik(True, grv.GetFocusedRowCellValue("IDPASIEN"), grv.GetFocusedRowCellValue("NAMAPASIEN"), grv.GetFocusedRowCellValue("IDDOCTOR_SATUSEHAT"), grv.GetFocusedRowCellValue("NAMADOKTER"), grv.GetFocusedRowCellValue("ICD10_KODE"), grv.GetFocusedRowCellValue("ICD10_NAME"), grv.GetFocusedRowCellValue("KDPENDAFTARAN"), grv.GetFocusedRowCellValue("IDENCOUNTER"), CDate(grv.GetFocusedRowCellValue("TANGGALMASUK")).ToString("yyyy-MM-ddTHH:mm:ss+00:00", CultureInfo.InvariantCulture), dsCPPT.OBJEKTIF_DIASTOLE) = True Then
                MsgBox("Berhasil Kirim", MsgBoxStyle.Exclamation, Me.Text)
            Else
                MsgBox("Gagal Kirim", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Else
            MsgBox("CPPT belum di simpan", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub ObservationDiastolikToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ObservationDiastolikToolStripMenuItem.Click
        If SatuSehat_Organisasi = "" Then
            MsgBox("Organisasi ID masih kosong", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("KDPENDAFTARAN") Is Nothing Then
            MsgBox("Registrasi belum di pilih", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("IDENCOUNTER") = "" Then
            MsgBox("Encounter masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("IDPASIEN") = "" Then
            MsgBox("Pasien ID masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("IDDOCTOR_SATUSEHAT") = "" Then
            MsgBox("Dokter ID masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("ICD10_KODE") = "-" Then
            MsgBox("Diagnosa masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim dsCPPT = oGrouperDataCppt.GetDataByKodePendaftaranRawatJalanDokter(grv.GetFocusedRowCellValue("KDPENDAFTARAN"))

        If dsCPPT IsNot Nothing Then
            If ObservationDiastolik(True, grv.GetFocusedRowCellValue("IDPASIEN"), grv.GetFocusedRowCellValue("NAMAPASIEN"), grv.GetFocusedRowCellValue("IDDOCTOR_SATUSEHAT"), grv.GetFocusedRowCellValue("NAMADOKTER"), grv.GetFocusedRowCellValue("ICD10_KODE"), grv.GetFocusedRowCellValue("ICD10_NAME"), grv.GetFocusedRowCellValue("KDPENDAFTARAN"), grv.GetFocusedRowCellValue("IDENCOUNTER"), CDate(grv.GetFocusedRowCellValue("TANGGALMASUK")).ToString("yyyy-MM-ddTHH:mm:ss+00:00", CultureInfo.InvariantCulture), dsCPPT.OBJEKTIF_DIASTOLE) = True Then
                MsgBox("Berhasil Kirim", MsgBoxStyle.Exclamation, Me.Text)
            Else
                MsgBox("Gagal Kirim", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Else
            MsgBox("CPPT belum di simpan", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub ObservationSuhuTubuhToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ObservationSuhuTubuhToolStripMenuItem.Click
        If SatuSehat_Organisasi = "" Then
            MsgBox("Organisasi ID masih kosong", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("KDPENDAFTARAN") Is Nothing Then
            MsgBox("Registrasi belum di pilih", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("IDENCOUNTER") = "" Then
            MsgBox("Encounter masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("IDPASIEN") = "" Then
            MsgBox("Pasien ID masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("IDDOCTOR_SATUSEHAT") = "" Then
            MsgBox("Dokter ID masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("ICD10_KODE") = "-" Then
            MsgBox("Diagnosa masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim dsCPPT = oGrouperDataCppt.GetDataByKodePendaftaranRawatJalanDokter(grv.GetFocusedRowCellValue("KDPENDAFTARAN"))

        If dsCPPT IsNot Nothing Then
            If ObservationSuhuTubuh(True, grv.GetFocusedRowCellValue("IDPASIEN"), grv.GetFocusedRowCellValue("NAMAPASIEN"), grv.GetFocusedRowCellValue("IDDOCTOR_SATUSEHAT"), grv.GetFocusedRowCellValue("NAMADOKTER"), grv.GetFocusedRowCellValue("ICD10_KODE"), grv.GetFocusedRowCellValue("ICD10_NAME"), grv.GetFocusedRowCellValue("KDPENDAFTARAN"), grv.GetFocusedRowCellValue("IDENCOUNTER"), CDate(grv.GetFocusedRowCellValue("TANGGALMASUK")).ToString("yyyy-MM-ddTHH:mm:ss+00:00", CultureInfo.InvariantCulture), dsCPPT.OBJEKTIF_SUHU) = True Then
                MsgBox("Berhasil Kirim", MsgBoxStyle.Exclamation, Me.Text)
            Else
                MsgBox("Gagal Kirim", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Else
            MsgBox("CPPT belum di simpan", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub ObservationDenyutJantungToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ObservationDenyutJantungToolStripMenuItem.Click
        If SatuSehat_Organisasi = "" Then
            MsgBox("Organisasi ID masih kosong", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("KDPENDAFTARAN") Is Nothing Then
            MsgBox("Registrasi belum di pilih", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("IDENCOUNTER") = "" Then
            MsgBox("Encounter masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("IDPASIEN") = "" Then
            MsgBox("Pasien ID masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("IDDOCTOR_SATUSEHAT") = "" Then
            MsgBox("Dokter ID masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("ICD10_KODE") = "-" Then
            MsgBox("Diagnosa masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim dsCPPT = oGrouperDataCppt.GetDataByKodePendaftaranRawatJalanDokter(grv.GetFocusedRowCellValue("KDPENDAFTARAN"))

        If dsCPPT IsNot Nothing Then
            If ObservationDenyutJantung(True, grv.GetFocusedRowCellValue("IDPASIEN"), grv.GetFocusedRowCellValue("NAMAPASIEN"), grv.GetFocusedRowCellValue("IDDOCTOR_SATUSEHAT"), grv.GetFocusedRowCellValue("NAMADOKTER"), grv.GetFocusedRowCellValue("ICD10_KODE"), grv.GetFocusedRowCellValue("ICD10_NAME"), grv.GetFocusedRowCellValue("KDPENDAFTARAN"), grv.GetFocusedRowCellValue("IDENCOUNTER"), CDate(grv.GetFocusedRowCellValue("TANGGALMASUK")).ToString("yyyy-MM-ddTHH:mm:ss+00:00", CultureInfo.InvariantCulture), dsCPPT.OBJEKTIF_HR) = True Then
                MsgBox("Berhasil Kirim", MsgBoxStyle.Exclamation, Me.Text)
            Else
                MsgBox("Gagal Kirim", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Else
            MsgBox("CPPT belum di simpan", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub ObservationPernapasanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ObservationPernapasanToolStripMenuItem.Click
        If SatuSehat_Organisasi = "" Then
            MsgBox("Organisasi ID masih kosong", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("KDPENDAFTARAN") Is Nothing Then
            MsgBox("Registrasi belum di pilih", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("IDENCOUNTER") = "" Then
            MsgBox("Encounter masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("IDPASIEN") = "" Then
            MsgBox("Pasien ID masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("IDDOCTOR_SATUSEHAT") = "" Then
            MsgBox("Dokter ID masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("ICD10_KODE") = "-" Then
            MsgBox("Diagnosa masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim dsCPPT = oGrouperDataCppt.GetDataByKodePendaftaranRawatJalanDokter(grv.GetFocusedRowCellValue("KDPENDAFTARAN"))

        If dsCPPT IsNot Nothing Then
            If ObservationPernapasan(True, grv.GetFocusedRowCellValue("IDPASIEN"), grv.GetFocusedRowCellValue("NAMAPASIEN"), grv.GetFocusedRowCellValue("IDDOCTOR_SATUSEHAT"), grv.GetFocusedRowCellValue("NAMADOKTER"), grv.GetFocusedRowCellValue("ICD10_KODE"), grv.GetFocusedRowCellValue("ICD10_NAME"), grv.GetFocusedRowCellValue("KDPENDAFTARAN"), grv.GetFocusedRowCellValue("IDENCOUNTER"), CDate(grv.GetFocusedRowCellValue("TANGGALMASUK")).ToString("yyyy-MM-ddTHH:mm:ss+00:00", CultureInfo.InvariantCulture), dsCPPT.OBJEKTIF_RR) = True Then
                MsgBox("Berhasil Kirim", MsgBoxStyle.Exclamation, Me.Text)
            Else
                MsgBox("Gagal Kirim", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Else
            MsgBox("CPPT belum di simpan", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub ObservationTingkatKesadaranToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ObservationTingkatKesadaranToolStripMenuItem.Click
        If SatuSehat_Organisasi = "" Then
            MsgBox("Organisasi ID masih kosong", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("KDPENDAFTARAN") Is Nothing Then
            MsgBox("Registrasi belum di pilih", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("IDENCOUNTER") = "" Then
            MsgBox("Encounter masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("IDPASIEN") = "" Then
            MsgBox("Pasien ID masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("IDDOCTOR_SATUSEHAT") = "" Then
            MsgBox("Dokter ID masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("ICD10_KODE") = "-" Then
            MsgBox("Diagnosa masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim dsCPPT = oGrouperDataCppt.GetDataByKodePendaftaranRawatJalanDokter(grv.GetFocusedRowCellValue("KDPENDAFTARAN"))

        If dsCPPT IsNot Nothing Then
            Dim kodetingkatkesadaran As String = ""
            Dim kodetingkatkesadaranname As String = ""

            If dsCPPT.OBJEKTIF_KESADARAN.Contains("Compos Mentis") Then
                kodetingkatkesadaran = "248234008"
                kodetingkatkesadaranname = "Mentally alert"
            End If
            If dsCPPT.OBJEKTIF_KESADARAN.Contains("cm") Then
                kodetingkatkesadaran = "248234008"
                kodetingkatkesadaranname = "Mentally alert"
            End If
            If dsCPPT.OBJEKTIF_KESADARAN.Contains("baik") Then
                kodetingkatkesadaran = "248234008"
                kodetingkatkesadaranname = "Mentally alert"
            End If
            If dsCPPT.OBJEKTIF_KESADARAN.Contains("Somnolen") Then
                kodetingkatkesadaran = ""
                kodetingkatkesadaranname = ""
            End If
            If dsCPPT.OBJEKTIF_KESADARAN.Contains("Sopor") Then
                kodetingkatkesadaran = ""
                kodetingkatkesadaranname = ""
            End If
            If dsCPPT.OBJEKTIF_KESADARAN.Contains("Koma") Then
                kodetingkatkesadaran = ""
                kodetingkatkesadaranname = ""
            End If
            If dsCPPT.OBJEKTIF_KESADARAN.Contains("Stupor") Then
                kodetingkatkesadaran = ""
                kodetingkatkesadaranname = ""
            End If
            If dsCPPT.OBJEKTIF_KESADARAN.Contains("CM") Then
                kodetingkatkesadaran = "248234008"
                kodetingkatkesadaranname = "Mentally alert"
            End If

            If kodetingkatkesadaran <> "" Then
                If ObservationTingkatKesadaran(True, grv.GetFocusedRowCellValue("IDPASIEN"), grv.GetFocusedRowCellValue("NAMAPASIEN"), grv.GetFocusedRowCellValue("IDDOCTOR_SATUSEHAT"), grv.GetFocusedRowCellValue("NAMADOKTER"), kodetingkatkesadaran, kodetingkatkesadaranname, grv.GetFocusedRowCellValue("KDPENDAFTARAN"), grv.GetFocusedRowCellValue("IDENCOUNTER"), CDate(grv.GetFocusedRowCellValue("TANGGALMASUK")).ToString("yyyy-MM-ddTHH:mm:ss+00:00", CultureInfo.InvariantCulture), dsCPPT.OBJEKTIF_RR) = True Then
                    MsgBox("Berhasil Kirim", MsgBoxStyle.Exclamation, Me.Text)
                Else
                    MsgBox("Gagal Kirim", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                MsgBox("Tingat Kesadaran Kosong " & dsCPPT.OBJEKTIF_KESADARAN, MsgBoxStyle.Exclamation, Me.Text)
            End If

        Else
            MsgBox("CPPT belum di simpan", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub ObservationTinggiBadanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ObservationTinggiBadanToolStripMenuItem.Click
        If SatuSehat_Organisasi = "" Then
            MsgBox("Organisasi ID masih kosong", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("KDPENDAFTARAN") Is Nothing Then
            MsgBox("Registrasi belum di pilih", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("IDENCOUNTER") = "" Then
            MsgBox("Encounter masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("IDPASIEN") = "" Then
            MsgBox("Pasien ID masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("IDDOCTOR_SATUSEHAT") = "" Then
            MsgBox("Dokter ID masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("ICD10_KODE") = "-" Then
            MsgBox("Diagnosa masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim dsCPPT = oGrouperDataCppt.GetDataByKodePendaftaranRawatJalanDokter(grv.GetFocusedRowCellValue("KDPENDAFTARAN"))

        If dsCPPT IsNot Nothing Then
            Dim kodetingkatkesadaran As String = ""
            Dim kodetingkatkesadaranname As String = ""

            If dsCPPT.OBJEKTIF_KESADARAN.Contains("Compos Mentis") Then
                kodetingkatkesadaran = "248234008"
                kodetingkatkesadaranname = "Mentally alert"
            End If
            If dsCPPT.OBJEKTIF_KESADARAN.Contains("cm") Then
                kodetingkatkesadaran = "248234008"
                kodetingkatkesadaranname = "Mentally alert"
            End If
            If dsCPPT.OBJEKTIF_KESADARAN.Contains("baik") Then
                kodetingkatkesadaran = "248234008"
                kodetingkatkesadaranname = "Mentally alert"
            End If
            If dsCPPT.OBJEKTIF_KESADARAN.Contains("Somnolen") Then
                kodetingkatkesadaran = ""
                kodetingkatkesadaranname = ""
            End If
            If dsCPPT.OBJEKTIF_KESADARAN.Contains("Sopor") Then
                kodetingkatkesadaran = ""
                kodetingkatkesadaranname = ""
            End If
            If dsCPPT.OBJEKTIF_KESADARAN.Contains("Koma") Then
                kodetingkatkesadaran = ""
                kodetingkatkesadaranname = ""
            End If
            If dsCPPT.OBJEKTIF_KESADARAN.Contains("Stupor") Then
                kodetingkatkesadaran = ""
                kodetingkatkesadaranname = ""
            End If

            If ObservationTinggiBadan(True, grv.GetFocusedRowCellValue("IDPASIEN"), grv.GetFocusedRowCellValue("NAMAPASIEN"), grv.GetFocusedRowCellValue("IDDOCTOR_SATUSEHAT"), grv.GetFocusedRowCellValue("NAMADOKTER"), grv.GetFocusedRowCellValue("ICD10_KODE"), grv.GetFocusedRowCellValue("ICD10_NAME"), grv.GetFocusedRowCellValue("KDPENDAFTARAN"), grv.GetFocusedRowCellValue("IDENCOUNTER"), CDate(grv.GetFocusedRowCellValue("TANGGALMASUK")).ToString("yyyy-MM-ddTHH:mm:ss+00:00", CultureInfo.InvariantCulture), CInt(dsCPPT.OBJEKTIF_TINGGIBADAN.Replace(",", "."))) = True Then
                MsgBox("Berhasil Kirim", MsgBoxStyle.Exclamation, Me.Text)
            Else
                MsgBox("Gagal Kirim", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Else
            MsgBox("CPPT belum di simpan", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub ObservationBeratBadanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ObservationBeratBadanToolStripMenuItem.Click
        If SatuSehat_Organisasi = "" Then
            MsgBox("Organisasi ID masih kosong", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("KDPENDAFTARAN") Is Nothing Then
            MsgBox("Registrasi belum di pilih", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("IDENCOUNTER") = "" Then
            MsgBox("Encounter masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("IDPASIEN") = "" Then
            MsgBox("Pasien ID masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("IDDOCTOR_SATUSEHAT") = "" Then
            MsgBox("Dokter ID masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("ICD10_KODE") = "-" Then
            MsgBox("Diagnosa masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim dsCPPT = oGrouperDataCppt.GetDataByKodePendaftaranRawatJalanDokter(grv.GetFocusedRowCellValue("KDPENDAFTARAN"))

        If dsCPPT IsNot Nothing Then
            Dim kodetingkatkesadaran As String = ""
            Dim kodetingkatkesadaranname As String = ""

            If dsCPPT.OBJEKTIF_KESADARAN.Contains("Compos Mentis") Then
                kodetingkatkesadaran = "248234008"
                kodetingkatkesadaranname = "Mentally alert"
            End If
            If dsCPPT.OBJEKTIF_KESADARAN.Contains("cm") Then
                kodetingkatkesadaran = "248234008"
                kodetingkatkesadaranname = "Mentally alert"
            End If
            If dsCPPT.OBJEKTIF_KESADARAN.Contains("baik") Then
                kodetingkatkesadaran = "248234008"
                kodetingkatkesadaranname = "Mentally alert"
            End If
            If dsCPPT.OBJEKTIF_KESADARAN.Contains("Somnolen") Then
                kodetingkatkesadaran = ""
                kodetingkatkesadaranname = ""
            End If
            If dsCPPT.OBJEKTIF_KESADARAN.Contains("Sopor") Then
                kodetingkatkesadaran = ""
                kodetingkatkesadaranname = ""
            End If
            If dsCPPT.OBJEKTIF_KESADARAN.Contains("Koma") Then
                kodetingkatkesadaran = ""
                kodetingkatkesadaranname = ""
            End If
            If dsCPPT.OBJEKTIF_KESADARAN.Contains("Stupor") Then
                kodetingkatkesadaran = ""
                kodetingkatkesadaranname = ""
            End If

            If ObservationBeratBadan(True, grv.GetFocusedRowCellValue("IDPASIEN"), grv.GetFocusedRowCellValue("NAMAPASIEN"), grv.GetFocusedRowCellValue("IDDOCTOR_SATUSEHAT"), grv.GetFocusedRowCellValue("NAMADOKTER"), grv.GetFocusedRowCellValue("ICD10_KODE"), grv.GetFocusedRowCellValue("ICD10_NAME"), grv.GetFocusedRowCellValue("KDPENDAFTARAN"), grv.GetFocusedRowCellValue("IDENCOUNTER"), CDate(grv.GetFocusedRowCellValue("TANGGALMASUK")).ToString("yyyy-MM-ddTHH:mm:ss+00:00", CultureInfo.InvariantCulture), CInt(dsCPPT.OBJEKTIF_BERATBADAN.Replace(",", "."))) = True Then
                MsgBox("Berhasil Kirim", MsgBoxStyle.Exclamation, Me.Text)
            Else
                MsgBox("Gagal Kirim", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Else
            MsgBox("CPPT belum di simpan", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub SendToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SendToolStripMenuItem.Click
        Try
            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Send Satu Sehat Kirim.....")

            Dim counter As Integer = 0
            Dim counterAll As Integer = 0
            Dim counterGagal As Integer = 0

            For i As Integer = 0 To grv.RowCount - 1
                If grv.GetRowCellValue(i, "KDPENDAFTARAN") <> "" Then
                    If grv.GetRowCellValue(i, "IDPASIEN") <> "" Then
                        If grv.GetRowCellValue(i, "IDDOCTOR_SATUSEHAT") <> "" Then
                            If grv.GetRowCellValue(i, "IDLOCATION_SATUSEHAT") <> "" Then
                                If grv.GetRowCellValue(i, "ICD10_KODE") <> "" Then
                                    If grv.GetRowCellValue(i, "ICD10_KODE") <> "-" Then
                                        If grv.GetRowCellValue(i, "ICD10_KODE") <> "-" Then
                                            counterAll += 1
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            Next

            If MsgBox("Apakah Akan Kirim Data Ke Satu Sehat Sebanyak " & counterAll & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then
                Exit Sub
            End If

            For i As Integer = 0 To grv.RowCount - 1
                If grv.GetRowCellValue(i, "KDPENDAFTARAN") <> "" Then
                    If grv.GetRowCellValue(i, "IDPASIEN") <> "" Then
                        If grv.GetRowCellValue(i, "IDDOCTOR_SATUSEHAT") <> "" Then
                            If grv.GetRowCellValue(i, "IDLOCATION_SATUSEHAT") <> "" Then
                                If grv.GetRowCellValue(i, "ICD10_KODE") <> "" Then
                                    If grv.GetRowCellValue(i, "ICD10_KODE") <> "-" Then
                                        If grv.GetRowCellValue(i, "ICD10_KODE") <> "-" Then
                                            Dim encounterData As String = Encounter(False, grv.GetRowCellValue(i, "IDPASIEN"), grv.GetRowCellValue(i, "NAMAPASIEN"), grv.GetRowCellValue(i, "IDDOCTOR_SATUSEHAT"), grv.GetRowCellValue(i, "NAMADOKTER"), CDate(grv.GetRowCellValue(i, "TANGGALMASUK")), grv.GetRowCellValue(i, "IDLOCATION_SATUSEHAT"), grv.GetRowCellValue(i, "NAMALOCATION_SATUSEHAT"), SatuSehat_Organisasi, grv.GetRowCellValue(i, "KDPENDAFTARAN"))
                                            If encounterData <> "" Then
                                                'JsonSS("ConditionDiagnosis", grv.GetRowCellValue(i, "IDPASIEN"), grv.GetRowCellValue(i, "NAMAPASIEN"), grv.GetRowCellValue(i, "IDDOCTOR_SATUSEHAT"), grv.GetRowCellValue(i, "NAMADOKTER"), CDate(grv.GetRowCellValue(i, "TANGGALMASUK")), grv.GetRowCellValue(i, "IDLOCATION_SATUSEHAT"), grv.GetRowCellValue(i, "NAMALOCATION_SATUSEHAT"), SatuSehat_Organisasi, grv.GetRowCellValue(i, "KDPENDAFTARAN"), grv.GetRowCellValue(i, "ICD10_KODE"), grv.GetRowCellValue(i, "ICD10_NAME"), grv.GetRowCellValue(i, "KETERANGANMASUK"), encounterData, "", "", "", "")

                                                If grv.GetRowCellValue(i, "KETERANGANSKD") = "" Then
                                                    JsonSS("ConditionMeninggalkanFaskes", grv.GetRowCellValue(i, "IDPASIEN"), grv.GetRowCellValue(i, "NAMAPASIEN"), grv.GetRowCellValue(i, "IDDOCTOR_SATUSEHAT"), grv.GetRowCellValue(i, "NAMADOKTER"), CDate(grv.GetRowCellValue(i, "TANGGALMASUK")), grv.GetRowCellValue(i, "IDLOCATION_SATUSEHAT"), grv.GetRowCellValue(i, "NAMALOCATION_SATUSEHAT"), SatuSehat_Organisasi, grv.GetRowCellValue(i, "KDPENDAFTARAN"), grv.GetRowCellValue(i, "ICD10_KODE"), grv.GetRowCellValue(i, "ICD10_NAME"), grv.GetRowCellValue(i, "KETERANGANMASUK"), encounterData, "", "", "", "")
                                                End If

                                                Dim dsCPPT = oGrouperDataCppt.GetDataByKodePendaftaranRawatJalanDokter(grv.GetRowCellValue(i, "KDPENDAFTARAN"))

                                                If dsCPPT IsNot Nothing Then
                                                    JsonSS("ConditionKeluhanUtama", grv.GetRowCellValue(i, "IDPASIEN"), grv.GetRowCellValue(i, "NAMAPASIEN"), grv.GetRowCellValue(i, "IDDOCTOR_SATUSEHAT"), grv.GetRowCellValue(i, "NAMADOKTER"), CDate(grv.GetRowCellValue(i, "TANGGALMASUK")), grv.GetRowCellValue(i, "IDLOCATION_SATUSEHAT"), grv.GetRowCellValue(i, "NAMALOCATION_SATUSEHAT"), SatuSehat_Organisasi, grv.GetRowCellValue(i, "KDPENDAFTARAN"), grv.GetRowCellValue(i, "ICD10_KODE"), grv.GetRowCellValue(i, "ICD10_NAME"), dsCPPT.SUBJEKTIF_KELUHANUTAMA, encounterData, "", "", "", "")
                                                    Try
                                                        JsonSS("ObservationSistolik", grv.GetRowCellValue(i, "IDPASIEN"), grv.GetRowCellValue(i, "NAMAPASIEN"), grv.GetRowCellValue(i, "IDDOCTOR_SATUSEHAT"), grv.GetRowCellValue(i, "NAMADOKTER"), CDate(grv.GetRowCellValue(i, "TANGGALMASUK")), grv.GetRowCellValue(i, "IDLOCATION_SATUSEHAT"), grv.GetRowCellValue(i, "NAMALOCATION_SATUSEHAT"), SatuSehat_Organisasi, grv.GetRowCellValue(i, "KDPENDAFTARAN"), grv.GetRowCellValue(i, "ICD10_KODE"), grv.GetRowCellValue(i, "ICD10_NAME"), CInt(dsCPPT.OBJEKTIF_SISTOLE.Replace(",", ".")), encounterData, "", "", "", "")

                                                    Catch ex As Exception
                                                        JsonSS("ObservationSistolik", grv.GetRowCellValue(i, "IDPASIEN"), grv.GetRowCellValue(i, "NAMAPASIEN"), grv.GetRowCellValue(i, "IDDOCTOR_SATUSEHAT"), grv.GetRowCellValue(i, "NAMADOKTER"), CDate(grv.GetRowCellValue(i, "TANGGALMASUK")), grv.GetRowCellValue(i, "IDLOCATION_SATUSEHAT"), grv.GetRowCellValue(i, "NAMALOCATION_SATUSEHAT"), SatuSehat_Organisasi, grv.GetRowCellValue(i, "KDPENDAFTARAN"), grv.GetRowCellValue(i, "ICD10_KODE"), grv.GetRowCellValue(i, "ICD10_NAME"), CInt(0), encounterData, "", "", "", "")

                                                    End Try
                                                    Try
                                                        JsonSS("ObservationDiastolik", grv.GetRowCellValue(i, "IDPASIEN"), grv.GetRowCellValue(i, "NAMAPASIEN"), grv.GetRowCellValue(i, "IDDOCTOR_SATUSEHAT"), grv.GetRowCellValue(i, "NAMADOKTER"), CDate(grv.GetRowCellValue(i, "TANGGALMASUK")), grv.GetRowCellValue(i, "IDLOCATION_SATUSEHAT"), grv.GetRowCellValue(i, "NAMALOCATION_SATUSEHAT"), SatuSehat_Organisasi, grv.GetRowCellValue(i, "KDPENDAFTARAN"), grv.GetRowCellValue(i, "ICD10_KODE"), grv.GetRowCellValue(i, "ICD10_NAME"), CInt(dsCPPT.OBJEKTIF_DIASTOLE.Replace(",", ".")), encounterData, "", "", "", "")

                                                    Catch ex As Exception
                                                        JsonSS("ObservationDiastolik", grv.GetRowCellValue(i, "IDPASIEN"), grv.GetRowCellValue(i, "NAMAPASIEN"), grv.GetRowCellValue(i, "IDDOCTOR_SATUSEHAT"), grv.GetRowCellValue(i, "NAMADOKTER"), CDate(grv.GetRowCellValue(i, "TANGGALMASUK")), grv.GetRowCellValue(i, "IDLOCATION_SATUSEHAT"), grv.GetRowCellValue(i, "NAMALOCATION_SATUSEHAT"), SatuSehat_Organisasi, grv.GetRowCellValue(i, "KDPENDAFTARAN"), grv.GetRowCellValue(i, "ICD10_KODE"), grv.GetRowCellValue(i, "ICD10_NAME"), CInt(0), encounterData, "", "", "", "")

                                                    End Try
                                                    Try
                                                        JsonSS("ObservationSuhuTubuh", grv.GetRowCellValue(i, "IDPASIEN"), grv.GetRowCellValue(i, "NAMAPASIEN"), grv.GetRowCellValue(i, "IDDOCTOR_SATUSEHAT"), grv.GetRowCellValue(i, "NAMADOKTER"), CDate(grv.GetRowCellValue(i, "TANGGALMASUK")), grv.GetRowCellValue(i, "IDLOCATION_SATUSEHAT"), grv.GetRowCellValue(i, "NAMALOCATION_SATUSEHAT"), SatuSehat_Organisasi, grv.GetRowCellValue(i, "KDPENDAFTARAN"), grv.GetRowCellValue(i, "ICD10_KODE"), grv.GetRowCellValue(i, "ICD10_NAME"), CInt(dsCPPT.OBJEKTIF_SUHU.Replace(",", ".")), encounterData, "", "", "", "")

                                                    Catch ex As Exception
                                                        JsonSS("ObservationSuhuTubuh", grv.GetRowCellValue(i, "IDPASIEN"), grv.GetRowCellValue(i, "NAMAPASIEN"), grv.GetRowCellValue(i, "IDDOCTOR_SATUSEHAT"), grv.GetRowCellValue(i, "NAMADOKTER"), CDate(grv.GetRowCellValue(i, "TANGGALMASUK")), grv.GetRowCellValue(i, "IDLOCATION_SATUSEHAT"), grv.GetRowCellValue(i, "NAMALOCATION_SATUSEHAT"), SatuSehat_Organisasi, grv.GetRowCellValue(i, "KDPENDAFTARAN"), grv.GetRowCellValue(i, "ICD10_KODE"), grv.GetRowCellValue(i, "ICD10_NAME"), dsCPPT.OBJEKTIF_SUHU, encounterData, "", "", "", "")

                                                    End Try
                                                    Try
                                                        JsonSS("ObservationDenyutJantung", grv.GetRowCellValue(i, "IDPASIEN"), grv.GetRowCellValue(i, "NAMAPASIEN"), grv.GetRowCellValue(i, "IDDOCTOR_SATUSEHAT"), grv.GetRowCellValue(i, "NAMADOKTER"), CDate(grv.GetRowCellValue(i, "TANGGALMASUK")), grv.GetRowCellValue(i, "IDLOCATION_SATUSEHAT"), grv.GetRowCellValue(i, "NAMALOCATION_SATUSEHAT"), SatuSehat_Organisasi, grv.GetRowCellValue(i, "KDPENDAFTARAN"), grv.GetRowCellValue(i, "ICD10_KODE"), grv.GetRowCellValue(i, "ICD10_NAME"), CInt(dsCPPT.OBJEKTIF_HR.Replace(",", ".")), encounterData, "", "", "", "")

                                                    Catch ex As Exception
                                                        JsonSS("ObservationDenyutJantung", grv.GetRowCellValue(i, "IDPASIEN"), grv.GetRowCellValue(i, "NAMAPASIEN"), grv.GetRowCellValue(i, "IDDOCTOR_SATUSEHAT"), grv.GetRowCellValue(i, "NAMADOKTER"), CDate(grv.GetRowCellValue(i, "TANGGALMASUK")), grv.GetRowCellValue(i, "IDLOCATION_SATUSEHAT"), grv.GetRowCellValue(i, "NAMALOCATION_SATUSEHAT"), SatuSehat_Organisasi, grv.GetRowCellValue(i, "KDPENDAFTARAN"), grv.GetRowCellValue(i, "ICD10_KODE"), grv.GetRowCellValue(i, "ICD10_NAME"), dsCPPT.OBJEKTIF_HR, encounterData, "", "", "", "")

                                                    End Try
                                                    Try
                                                        JsonSS("ObservationPernapasan", grv.GetRowCellValue(i, "IDPASIEN"), grv.GetRowCellValue(i, "NAMAPASIEN"), grv.GetRowCellValue(i, "IDDOCTOR_SATUSEHAT"), grv.GetRowCellValue(i, "NAMADOKTER"), CDate(grv.GetRowCellValue(i, "TANGGALMASUK")), grv.GetRowCellValue(i, "IDLOCATION_SATUSEHAT"), grv.GetRowCellValue(i, "NAMALOCATION_SATUSEHAT"), SatuSehat_Organisasi, grv.GetRowCellValue(i, "KDPENDAFTARAN"), grv.GetRowCellValue(i, "ICD10_KODE"), grv.GetRowCellValue(i, "ICD10_NAME"), CInt(dsCPPT.OBJEKTIF_RR.Replace(",", ".")), encounterData, "", "", "", "")

                                                    Catch ex As Exception
                                                        JsonSS("ObservationPernapasan", grv.GetRowCellValue(i, "IDPASIEN"), grv.GetRowCellValue(i, "NAMAPASIEN"), grv.GetRowCellValue(i, "IDDOCTOR_SATUSEHAT"), grv.GetRowCellValue(i, "NAMADOKTER"), CDate(grv.GetRowCellValue(i, "TANGGALMASUK")), grv.GetRowCellValue(i, "IDLOCATION_SATUSEHAT"), grv.GetRowCellValue(i, "NAMALOCATION_SATUSEHAT"), SatuSehat_Organisasi, grv.GetRowCellValue(i, "KDPENDAFTARAN"), grv.GetRowCellValue(i, "ICD10_KODE"), grv.GetRowCellValue(i, "ICD10_NAME"), dsCPPT.OBJEKTIF_RR, encounterData, "", "", "", "")

                                                    End Try

                                                    Dim kodetingkatkesadaran As String = ""
                                                    Dim kodetingkatkesadaranname As String = ""

                                                    If dsCPPT.OBJEKTIF_KESADARAN.Contains("Compos Mentis") Then
                                                        kodetingkatkesadaran = "248234008"
                                                        kodetingkatkesadaranname = "Mentally alert"
                                                    End If
                                                    If dsCPPT.OBJEKTIF_KESADARAN.Contains("cm") Then
                                                        kodetingkatkesadaran = "248234008"
                                                        kodetingkatkesadaranname = "Mentally alert"
                                                    End If
                                                    If dsCPPT.OBJEKTIF_KESADARAN.Contains("baik") Then
                                                        kodetingkatkesadaran = "248234008"
                                                        kodetingkatkesadaranname = "Mentally alert"
                                                    End If
                                                    If dsCPPT.OBJEKTIF_KESADARAN.Contains("Somnolen") Then
                                                        kodetingkatkesadaran = ""
                                                        kodetingkatkesadaranname = ""
                                                    End If
                                                    If dsCPPT.OBJEKTIF_KESADARAN.Contains("Sopor") Then
                                                        kodetingkatkesadaran = ""
                                                        kodetingkatkesadaranname = ""
                                                    End If
                                                    If dsCPPT.OBJEKTIF_KESADARAN.Contains("Koma") Then
                                                        kodetingkatkesadaran = ""
                                                        kodetingkatkesadaranname = ""
                                                    End If
                                                    If dsCPPT.OBJEKTIF_KESADARAN.Contains("Stupor") Then
                                                        kodetingkatkesadaran = ""
                                                        kodetingkatkesadaranname = ""
                                                    End If
                                                    If dsCPPT.OBJEKTIF_KESADARAN.Contains("CM") Then
                                                        kodetingkatkesadaran = "248234008"
                                                        kodetingkatkesadaranname = "Mentally alert"
                                                    End If
                                                    If kodetingkatkesadaran <> "" Then
                                                        JsonSS("ObservationTingkatKesadaran", grv.GetRowCellValue(i, "IDPASIEN"), grv.GetRowCellValue(i, "NAMAPASIEN"), grv.GetRowCellValue(i, "IDDOCTOR_SATUSEHAT"), grv.GetRowCellValue(i, "NAMADOKTER"), CDate(grv.GetRowCellValue(i, "TANGGALMASUK")), grv.GetRowCellValue(i, "IDLOCATION_SATUSEHAT"), grv.GetRowCellValue(i, "NAMALOCATION_SATUSEHAT"), SatuSehat_Organisasi, grv.GetRowCellValue(i, "KDPENDAFTARAN"), grv.GetRowCellValue(i, "ICD10_KODE"), grv.GetRowCellValue(i, "ICD10_NAME"), dsCPPT.SUBJEKTIF_KELUHANUTAMA, encounterData, kodetingkatkesadaran, kodetingkatkesadaranname, "", "")
                                                    End If

                                                    Try
                                                        JsonSS("ObservationTinggiBadan", grv.GetRowCellValue(i, "IDPASIEN"), grv.GetRowCellValue(i, "NAMAPASIEN"), grv.GetRowCellValue(i, "IDDOCTOR_SATUSEHAT"), grv.GetRowCellValue(i, "NAMADOKTER"), CDate(grv.GetRowCellValue(i, "TANGGALMASUK")), grv.GetRowCellValue(i, "IDLOCATION_SATUSEHAT"), grv.GetRowCellValue(i, "NAMALOCATION_SATUSEHAT"), SatuSehat_Organisasi, grv.GetRowCellValue(i, "KDPENDAFTARAN"), grv.GetRowCellValue(i, "ICD10_KODE"), grv.GetRowCellValue(i, "ICD10_NAME"), CInt(dsCPPT.OBJEKTIF_TINGGIBADAN.Replace(",", ".")), encounterData, kodetingkatkesadaran, kodetingkatkesadaranname, "", "")

                                                    Catch ex As Exception
                                                        JsonSS("ObservationTinggiBadan", grv.GetRowCellValue(i, "IDPASIEN"), grv.GetRowCellValue(i, "NAMAPASIEN"), grv.GetRowCellValue(i, "IDDOCTOR_SATUSEHAT"), grv.GetRowCellValue(i, "NAMADOKTER"), CDate(grv.GetRowCellValue(i, "TANGGALMASUK")), grv.GetRowCellValue(i, "IDLOCATION_SATUSEHAT"), grv.GetRowCellValue(i, "NAMALOCATION_SATUSEHAT"), SatuSehat_Organisasi, grv.GetRowCellValue(i, "KDPENDAFTARAN"), grv.GetRowCellValue(i, "ICD10_KODE"), grv.GetRowCellValue(i, "ICD10_NAME"), CInt(0), encounterData, kodetingkatkesadaran, kodetingkatkesadaranname, "", "")

                                                    End Try
                                                    Try
                                                        JsonSS("ObservationBeratBadan", grv.GetRowCellValue(i, "IDPASIEN"), grv.GetRowCellValue(i, "NAMAPASIEN"), grv.GetRowCellValue(i, "IDDOCTOR_SATUSEHAT"), grv.GetRowCellValue(i, "NAMADOKTER"), CDate(grv.GetRowCellValue(i, "TANGGALMASUK")), grv.GetRowCellValue(i, "IDLOCATION_SATUSEHAT"), grv.GetRowCellValue(i, "NAMALOCATION_SATUSEHAT"), SatuSehat_Organisasi, grv.GetRowCellValue(i, "KDPENDAFTARAN"), grv.GetRowCellValue(i, "ICD10_KODE"), grv.GetRowCellValue(i, "ICD10_NAME"), CInt(dsCPPT.OBJEKTIF_BERATBADAN.Replace(",", ".")), encounterData, kodetingkatkesadaran, kodetingkatkesadaranname, "", "")

                                                    Catch ex As Exception
                                                        JsonSS("ObservationBeratBadan", grv.GetRowCellValue(i, "IDPASIEN"), grv.GetRowCellValue(i, "NAMAPASIEN"), grv.GetRowCellValue(i, "IDDOCTOR_SATUSEHAT"), grv.GetRowCellValue(i, "NAMADOKTER"), CDate(grv.GetRowCellValue(i, "TANGGALMASUK")), grv.GetRowCellValue(i, "IDLOCATION_SATUSEHAT"), grv.GetRowCellValue(i, "NAMALOCATION_SATUSEHAT"), SatuSehat_Organisasi, grv.GetRowCellValue(i, "KDPENDAFTARAN"), grv.GetRowCellValue(i, "ICD10_KODE"), grv.GetRowCellValue(i, "ICD10_NAME"), CInt(0), encounterData, kodetingkatkesadaran, kodetingkatkesadaranname, "", "")

                                                    End Try

                                                    'Dim oDiagnosaSnowmedCT As New Reference.clsDiagnosaSnowmedCT

                                                    'Dim dsDiagnosa = oDiagnosaSnowmedCT.GetData(grv.GetRowCellValue(i, "ICD10_KODE"))
                                                    'If dsDiagnosa IsNot Nothing Then
                                                    '    JsonSS("Procedure", grv.GetRowCellValue(i, "IDPASIEN"), grv.GetRowCellValue(i, "NAMAPASIEN"), grv.GetRowCellValue(i, "IDDOCTOR_SATUSEHAT"), grv.GetRowCellValue(i, "NAMADOKTER"), CDate(grv.GetRowCellValue(i, "TANGGALMASUK")), grv.GetRowCellValue(i, "IDLOCATION_SATUSEHAT"), grv.GetRowCellValue(i, "NAMALOCATION_SATUSEHAT"), SatuSehat_Organisasi, grv.GetRowCellValue(i, "KDPENDAFTARAN"), grv.GetRowCellValue(i, "ICD10_KODE"), grv.GetRowCellValue(i, "ICD10_NAME"), dsCPPT.OBJEKTIF_PEMERIKSAAN, encounterData, kodetingkatkesadaran, kodetingkatkesadaranname, dsDiagnosa.KDSNOMED_CT, dsDiagnosa.MEMO)
                                                    'End If

                                                    counter += 1
                                                    counterGagal += 1
                                                End If
                                            Else
                                                counter += 1
                                                counterGagal += 1
                                            End If

                                            SplashScreenManager.Default.SetWaitFormCaption("Processing data " & counter & " of " & counterAll)

                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            Next

            SplashScreenManager.CloseForm(False)

            'MsgBox("Kirim Data : " & counterAll & vbCrLf & "*Gagal : " & counterGagal & vbCrLf & "*Berhasil : " & counter, MsgBoxStyle.Information, Me.Text)

            fn_LoadSecurity()

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Looping" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub ProcedureToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProcedureToolStripMenuItem.Click
        If SatuSehat_Organisasi = "" Then
            MsgBox("Organisasi ID masih kosong", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("KDPENDAFTARAN") Is Nothing Then
            MsgBox("Registrasi belum di pilih", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("IDENCOUNTER") = "" Then
            MsgBox("Encounter masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("IDPASIEN") = "" Then
            MsgBox("Pasien ID masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("IDDOCTOR_SATUSEHAT") = "" Then
            MsgBox("Dokter ID masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("ICD10_KODE") = "-" Then
            MsgBox("Diagnosa masih kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim oDiagnosaSnowmedCT As New Reference.clsDiagnosaSnowmedCT

        Dim dsDiagnosa = oDiagnosaSnowmedCT.GetData(grv.GetFocusedRowCellValue("ICD10_KODE"))
        If dsDiagnosa IsNot Nothing Then
            Dim dsCPPT = oGrouperDataCppt.GetDataByKodePendaftaranRawatJalanDokter(grv.GetFocusedRowCellValue("KDPENDAFTARAN"))

            If dsCPPT IsNot Nothing Then
                If Procedure(True, grv.GetFocusedRowCellValue("IDDOCTOR_SATUSEHAT"), grv.GetFocusedRowCellValue("NAMADOKTER"), grv.GetFocusedRowCellValue("ICD10_KODE"), grv.GetFocusedRowCellValue("ICD10_NAME"), grv.GetFocusedRowCellValue("KDPENDAFTARAN"), grv.GetFocusedRowCellValue("IDENCOUNTER"), CDate(grv.GetFocusedRowCellValue("TANGGALMASUK")).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), dsDiagnosa.KDSNOMED_CT, dsDiagnosa.MEMO, dsCPPT.OBJEKTIF_PEMERIKSAAN) = True Then
                    MsgBox("Berhasil Kirim", MsgBoxStyle.Exclamation, Me.Text)
                Else
                    MsgBox("Gagal Kirim", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                MsgBox("CPPT belum di simpan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Else
            MsgBox("Kode Snowmed-CT belum di simpan", MsgBoxStyle.Exclamation, Me.Text)

        End If


    End Sub

#End Region
#Region "Fungction Satu Sehat"
    Private Function JsonSS(ByVal Category As String, ByVal idPasien As String, ByVal nmPasien As String, ByVal idDokter As String, ByVal nmDokter As String, ByVal sDate As DateTime, ByVal idLocation As String, ByVal nmLocation As String, ByVal Organization As String, ByVal Register As String, ByVal idIcd10 As String, ByVal nmIcd10 As String, ByVal keterangan As String, ByVal Encounter As String, ByVal kodekesadaran As String, ByVal nmkesadaran As String, ByVal idItemSS As String, ByVal nmItemSS As String) As String
        Try
            JsonSS = ""

            Dim SQL As String = ""
            Dim url As String = String.Empty

            If Category = "Encounter" Then
                SQL = ""
                SQL &= "{"
                SQL &= "  ""resourceType"": ""Encounter"","
                SQL &= "  ""status"": ""arrived"","
                SQL &= "  ""class"": {"
                SQL &= "    ""system"": ""http://terminology.hl7.org/CodeSystem/v3-ActCode"","
                SQL &= "    ""code"": ""AMB"","
                SQL &= "    ""display"": ""ambulatory"""
                SQL &= "  },"
                SQL &= "  ""subject"": {"
                SQL &= "    ""reference"": ""Patient/" & idPasien & ""","
                SQL &= "    ""display"": """ & nmPasien.Replace("'", "") & """"
                SQL &= "  },"
                SQL &= "  ""participant"": ["
                SQL &= "    {"
                SQL &= "      ""type"": ["
                SQL &= "        {"
                SQL &= "          ""coding"": ["
                SQL &= "            {"
                SQL &= "              ""system"": ""http://terminology.hl7.org/CodeSystem/v3-ParticipationType"","
                SQL &= "              ""code"": ""ATND"","
                SQL &= "              ""display"": ""attender"""
                SQL &= "            }"
                SQL &= "          ]"
                SQL &= "        }"
                SQL &= "      ],"
                SQL &= "      ""individual"": {"
                SQL &= "        ""reference"": ""Practitioner/" & idDokter & ""","
                SQL &= "        ""display"": """ & nmDokter & """"
                SQL &= "      }"
                SQL &= "    }"
                SQL &= "  ],"
                SQL &= "  ""period"": {"
                SQL &= "    ""start"": """ & sDate.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00", CultureInfo.InvariantCulture) & """"
                SQL &= "  },"
                SQL &= "  ""location"": ["
                SQL &= "    {"
                SQL &= "      ""location"": {"
                SQL &= "        ""reference"": ""Location/" & idLocation & ""","
                SQL &= "        ""display"": """ & nmLocation & """"
                SQL &= "      }"
                SQL &= "    }"
                SQL &= "  ],"
                SQL &= "  ""statusHistory"": ["
                SQL &= "    {"
                SQL &= "      ""status"": ""arrived"","
                SQL &= "      ""period"": {"
                SQL &= "        ""start"": """ & sDate.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00", CultureInfo.InvariantCulture) & """"
                SQL &= "      }"
                SQL &= "    }"
                SQL &= "  ],"
                SQL &= "  ""serviceProvider"": {"
                SQL &= "    ""reference"": ""Organization/" & Organization & """"
                SQL &= "  },"
                SQL &= "  ""identifier"": ["
                SQL &= "    {"
                SQL &= "      ""system"": ""http://sys-ids.kemkes.go.id/encounter/" & Organization & ""","
                SQL &= "      ""value"": """ & Register & """"
                SQL &= "    }"
                SQL &= "  ]"
                SQL &= "}"
            ElseIf Category = "ConditionDiagnosis"
                If SatuSehat_Production = False Then
                    url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Condition"
                Else
                    url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Condition"
                End If

                SQL = ""
                SQL &= "{"
                SQL &= "  ""resourceType"": ""Condition"","
                SQL &= "  ""clinicalStatus"": {"
                SQL &= "    ""coding"": ["
                SQL &= "      {"
                SQL &= "        ""system"": ""http://terminology.hl7.org/CodeSystem/condition-clinical"","
                SQL &= "        ""code"": ""active"","
                SQL &= "        ""display"": ""Active"""
                SQL &= "      }"
                SQL &= "    ]"
                SQL &= "  },"
                SQL &= "  ""category"": ["
                SQL &= "    {"
                SQL &= "      ""coding"": ["
                SQL &= "        {"
                SQL &= "          ""system"": ""http://terminology.hl7.org/CodeSystem/condition-category"","
                SQL &= "          ""code"": ""encounter-diagnosis"","
                SQL &= "          ""display"": ""Encounter Diagnosis"""
                SQL &= "        }"
                SQL &= "      ]"
                SQL &= "    }"
                SQL &= "  ],"
                SQL &= "  ""code"": {"
                SQL &= "    ""coding"": ["
                SQL &= "      {"
                SQL &= "        ""system"": ""http://hl7.org/fhir/sid/icd-10"","
                SQL &= "        ""code"": """ & idIcd10 & ""","
                SQL &= "        ""display"": """ & nmIcd10 & """"
                SQL &= "      }"
                SQL &= "    ]"
                SQL &= "  },"
                SQL &= "  ""subject"": {"
                SQL &= "    ""reference"": ""Patient/" & idPasien & ""","
                SQL &= "    ""display"": """ & nmPasien.Replace("'", "") & """"
                SQL &= "  },"
                SQL &= "  ""encounter"": {"
                SQL &= "    ""reference"": ""Encounter/" & Encounter & ""","
                SQL &= "    ""display"": """ & keterangan & """"
                SQL &= "  }"
                SQL &= "}"
            ElseIf Category = "ConditionMeninggalkanFaskes"
                If SatuSehat_Production = False Then
                    url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Condition"
                Else
                    url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Condition"
                End If
                SQL = ""
                SQL &= "{"
                SQL &= "  ""resourceType"": ""Condition"","
                SQL &= "  ""clinicalStatus"": {"
                SQL &= "    ""coding"": ["
                SQL &= "      {"
                SQL &= "        ""system"": ""http://terminology.hl7.org/CodeSystem/condition-clinical"","
                SQL &= "        ""code"": ""active"","
                SQL &= "        ""display"": ""Active"""
                SQL &= "      }"
                SQL &= "    ]"
                SQL &= "  },"
                SQL &= "  ""category"": ["
                SQL &= "    {"
                SQL &= "      ""coding"": ["
                SQL &= "        {"
                SQL &= "          ""system"": ""http://terminology.hl7.org/CodeSystem/condition-category"","
                SQL &= "          ""code"": ""encounter-diagnosis"","
                SQL &= "          ""display"": ""Encounter Diagnosis"""
                SQL &= "        }"
                SQL &= "      ]"
                SQL &= "    }"
                SQL &= "  ],"
                SQL &= "  ""code"": {"
                SQL &= "    ""coding"": ["
                SQL &= "      {"
                SQL &= "        ""system"": ""http://snomed.info/sct"","
                SQL &= "        ""code"": ""359746009"","
                SQL &= "        ""display"": ""Patient's condition stable"""
                SQL &= "      }"
                SQL &= "    ]"
                SQL &= "  },"
                SQL &= "  ""subject"": {"
                SQL &= "    ""reference"": ""Patient/" & idPasien & ""","
                SQL &= "    ""display"": """ & nmPasien & """"
                SQL &= "  },"
                SQL &= "  ""encounter"": {"
                SQL &= "    ""reference"": ""Encounter/" & Encounter & ""","
                SQL &= "    ""display"": """ & keterangan & """"
                SQL &= "  }"
                SQL &= "}"
            ElseIf Category = "ConditionKeluhanUtama"
                If SatuSehat_Production = False Then
                    url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Condition"
                Else
                    url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Condition"
                End If
                SQL = FhirBundleGenerator.CreateConditionJsonKeluhanUtama(idPasien, nmPasien, Encounter, idDokter, nmDokter, idIcd10, nmIcd10, sDate.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), sDate.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), keterangan)
            ElseIf Category = "ObservationSistolik"
                If SatuSehat_Production = False Then
                    url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Observation"
                Else
                    url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Observation"
                End If
                SQL = FhirBundleGenerator.CreateObservationJsonSistol(idPasien, nmPasien, Encounter, idDokter, nmDokter, idIcd10, nmIcd10, sDate.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), sDate.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), keterangan)
            ElseIf Category = "ObservationDiastolik"
                If SatuSehat_Production = False Then
                    url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Observation"
                Else
                    url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Observation"
                End If
                SQL = FhirBundleGenerator.CreateObservationJsonDiastol(idPasien, nmPasien, Encounter, idDokter, nmDokter, idIcd10, nmIcd10, sDate.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), sDate.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), keterangan)
            ElseIf Category = "ObservationSuhuTubuh"
                If SatuSehat_Production = False Then
                    url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Observation"
                Else
                    url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Observation"
                End If
                SQL = FhirBundleGenerator.CreateObservationJsonSuhuTubuh(idPasien, nmPasien, Encounter, idDokter, nmDokter, idIcd10, nmIcd10, sDate.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), sDate.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), keterangan)
            ElseIf Category = "ObservationDenyutJantung"
                If SatuSehat_Production = False Then
                    url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Observation"
                Else
                    url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Observation"
                End If
                SQL = FhirBundleGenerator.CreateObservationJsonDenyutJantung(idPasien, nmPasien, Encounter, idDokter, nmDokter, idIcd10, nmIcd10, sDate.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), sDate.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), keterangan)
            ElseIf Category = "ObservationPernapasan"
                If SatuSehat_Production = False Then
                    url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Observation"
                Else
                    url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Observation"
                End If
                SQL = FhirBundleGenerator.CreateObservationJsonPernapasan(idPasien, nmPasien, Encounter, idDokter, nmDokter, idIcd10, nmIcd10, sDate.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), sDate.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), keterangan)
            ElseIf Category = "ObservationPernapasan"
                If SatuSehat_Production = False Then
                    url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Observation"
                Else
                    url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Observation"
                End If
                SQL = FhirBundleGenerator.CreateObservationJsonPernapasan(idPasien, nmPasien, Encounter, idDokter, nmDokter, idIcd10, nmIcd10, sDate.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), sDate.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), keterangan)
            ElseIf Category = "ObservationTingkatKesadaran"
                If SatuSehat_Production = False Then
                    url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Observation"
                Else
                    url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Observation"
                End If
                SQL = FhirBundleGenerator.CreateObservationJsonTingkatKesadaran(idPasien, nmPasien, Encounter, idDokter, nmDokter, kodekesadaran, nmkesadaran, sDate.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), sDate.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), keterangan)
            ElseIf Category = "ObservationTinggiBadan"
                If SatuSehat_Production = False Then
                    url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Observation"
                Else
                    url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Observation"
                End If
                SQL = FhirBundleGenerator.CreateObservationJsonTinggiBadan(idPasien, nmPasien, Encounter, idDokter, nmDokter, idIcd10, nmIcd10, sDate.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), sDate.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), keterangan)
            ElseIf Category = "ObservationBeratBadan"
                If SatuSehat_Production = False Then
                    url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Observation"
                Else
                    url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Observation"
                End If
                SQL = FhirBundleGenerator.CreateObservationJsonBeratBadan(idPasien, nmPasien, Encounter, idDokter, nmDokter, idIcd10, nmIcd10, sDate.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), sDate.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), keterangan)
            ElseIf Category = "Procedure"
                SQL = ""
                SQL &= "{"
                SQL &= "  ""resourceType"": ""Procedure"","
                SQL &= "  ""status"": ""completed"","
                SQL &= "  ""category"": {"
                SQL &= "    ""coding"": ["
                SQL &= "      {"
                SQL &= "        ""system"": ""http://snomed.info/sct"","
                SQL &= "        ""code"": ""103693007"","
                SQL &= "        ""display"": ""Diagnostic procedure"""
                SQL &= "      }"
                SQL &= "    ],"
                SQL &= "    ""text"": ""Diagnostic procedure"""
                SQL &= "  },"
                SQL &= "  ""code"": {"

                Dim ds = oRIdentitasGrouperData.GetDataByRegisterData(Register)

                If ds IsNot Nothing Then
                    BuildCodingArrayFromDataTable(ds.kodegrouper)
                End If

                'SQL &= "    ""coding"": ["
                'SQL &= "      {"
                'SQL &= "        ""system"": ""http://hl7.org/fhir/sid/icd-9-cm"","
                'SQL &= "        ""code"": ""87.44"","
                'SQL &= "        ""display"": ""Routine chest x-ray, so described"""
                'SQL &= "      }"
                'SQL &= "    ]"


                SQL &= "  },"
                SQL &= "  ""subject"": {"
                SQL &= "    ""reference"": ""Patient/100000030009"","
                SQL &= "    ""display"": ""Budi Santoso"""
                SQL &= "  },"
                SQL &= "  ""encounter"": {"
                SQL &= "    ""reference"": ""Encounter/" & Encounter & ""","
                SQL &= "    ""display"": ""Tindakan Rontgen Dada Budi Santoso pada Selasa tanggal 14 Juni 2022"""
                SQL &= "  },"
                SQL &= "  ""performedPeriod"": {"
                SQL &= "    ""start"": """ & sDate.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00") & ""","
                SQL &= "    ""end"": """ & sDate.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00") & """"
                SQL &= "  },"
                SQL &= "  ""performer"": ["
                SQL &= "    {"
                SQL &= "      ""actor"": {"
                SQL &= "        ""reference"": ""Practitioner/" & idDokter & ""","
                SQL &= "        ""display"": """ & nmDokter & """"
                SQL &= "      }"
                SQL &= "    }"
                SQL &= "  ],"
                SQL &= "  ""reasonCode"": ["
                SQL &= "    {"
                SQL &= "      ""coding"": ["
                SQL &= "        {"
                SQL &= "          ""system"": ""http://hl7.org/fhir/sid/icd-10"","
                SQL &= "          ""code"": """ & idIcd10 & ""","
                SQL &= "          ""display"": """ & nmIcd10 & """"
                SQL &= "        }"
                SQL &= "      ]"
                SQL &= "    }"
                SQL &= "  ],"
                SQL &= "  ""bodySite"": ["
                SQL &= "    {"
                SQL &= "      ""coding"": ["
                SQL &= "        {"
                SQL &= "          ""system"": ""http://snomed.info/sct"","
                SQL &= "          ""code"": """ & idItemSS & ""","
                SQL &= "          ""display"": """ & nmItemSS & """"
                SQL &= "        }"
                SQL &= "      ]"
                SQL &= "    }"
                SQL &= "  ],"
                SQL &= "  ""note"": ["
                SQL &= "    {"
                SQL &= "      ""text"": """ & keterangan & """"
                SQL &= "    }"
                SQL &= "  ]"
                SQL &= "}"
                'ElseIf Category = "ClinicalImpression"
                '    SQL = ""
                '    SQL &= "{"
                '    SQL &= "  ""resourceType"": ""ClinicalImpression"","
                '    SQL &= "  ""identifier"": ["
                '    SQL &= "    {"
                '    SQL &= "      ""system"": ""http://sys-ids.kemkes.go.id/clinicalimpression/" & SatuSehat_Organisasi & ""","
                '    SQL &= "      ""use"": ""official"","
                '    SQL &= "      ""value"": ""Prognosis_000123"""
                '    SQL &= "    }"
                '    SQL &= "  ],"
                '    SQL &= "  ""status"": ""completed"","
                '    SQL &= "  ""description"": """ & "" & ""","
                '    SQL &= "  ""subject"": {"
                '    SQL &= "    ""reference"": ""Patient/" & idPasien & ""","
                '    SQL &= "    ""display"": """ & nmPasien & """"
                '    SQL &= "  },"
                '    SQL &= "  ""encounter"": {"
                '    SQL &= "    ""reference"": ""Encounter/" & Encounter  & ""","
                '    SQL &= "    ""display"": """ & keterangan & """"
                '    SQL &= "  },"
                '    SQL &= "  ""effectiveDateTime"": """ & sDate.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00") & ""","
                '    SQL &= "  ""date"": """ & sDate.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00") & ""","
                '    SQL &= "  ""assessor"": {"
                '    SQL &= "    ""reference"": ""Practitioner/" & idDokter & """"
                '    SQL &= "  },"
                '    SQL &= "  ""problem"": ["
                '    SQL &= "    {"
                '    SQL &= "      ""reference"": ""Condition/" & Condition & """"
                '    SQL &= "    }"
                '    SQL &= "  ],"
                '    SQL &= "  ""investigation"": ["
                '    SQL &= "    {"
                '    SQL &= "      ""code"": {"
                '    SQL &= "        ""text"": """ & pemeriksaan & """"
                '    SQL &= "      },"
                '    SQL &= "      ""item"": ["
                '    SQL &= "        {"
                '    SQL &= "          ""reference"": ""DiagnosticReport/a0fa6244-7638-43ba-bbc2-2af954761540"""
                '    SQL &= "        },"
                '    SQL &= "        {"
                '    SQL &= "          ""reference"": ""Observation/" & Observation & """"
                '    SQL &= "        }"
                '    SQL &= "      ]"
                '    SQL &= "    }"
                '    SQL &= "  ],"
                '    SQL &= "  ""summary"": ""Prognosis terhadap gejala klinis dan terkonfirmasi Tuberculosis"","
                '    SQL &= "  ""finding"": ["
                '    SQL &= "    {"
                '    SQL &= "      ""itemCodeableConcept"": {"
                '    SQL &= "        ""coding"": ["
                '    SQL &= "          {"
                '    SQL &= "            ""system"": ""http://hl7.org/fhir/sid/icd-10"","
                '    SQL &= "            ""code"": ""A15.0"","
                '    SQL &= "            ""display"": ""Tuberculosis of lung, confirmed by sputum microscopy with or without culture"""
                '    SQL &= "          }"
                '    SQL &= "        ]"
                '    SQL &= "      },"
                '    SQL &= "      ""itemReference"": {"
                '    SQL &= "        ""reference"": ""Condition/f2bc12fe-0ab2-4e5c-a3cd-32c66150cbe9"""
                '    SQL &= "      }"
                '    SQL &= "    }"
                '    SQL &= "  ],"
                '    SQL &= "  ""prognosisCodeableConcept"": ["
                '    SQL &= "    {"
                '    SQL &= "      ""coding"": ["
                '    SQL &= "        {"
                '    SQL &= "          ""system"": ""http://snomed.info/sct"","
                '    SQL &= "          ""code"": ""170968001"","
                '    SQL &= "          ""display"": ""Prognosis good"""
                '    SQL &= "        }"
                '    SQL &= "      ]"
                '    SQL &= "    }"
                '    SQL &= "  ]"
                '    SQL &= "}"
            End If

            If SQL = "" Then
                JsonSS = ""
            Else
                Dim dsCek = oSSSend.GetDataByRegisterCategory(Register, Category)

                If dsCek Is Nothing Then
                    JsonSS = SQL
                    If fn_SaveSSSend(Register, Category, JsonSS, url) = False Then
                        JsonSS = ""
                    End If
                Else
                    JsonSS = "OK"
                End If
            End If
        Catch oErr As Exception
            JsonSS = ""
        End Try
    End Function
    Private Function Encounter(ByVal isPesan As Boolean, ByVal idPasien As String, ByVal nmPasien As String, ByVal idDokter As String, ByVal nmDokter As String, ByVal sDate As DateTime, ByVal idLocation As String, ByVal nmLocation As String, ByVal Organization As String, ByVal Register As String) As String
        Try
            Encounter = ""

            'SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            'SplashScreenManager.Default.SetWaitFormCaption("Encounter Kirim.....")

            'Dim SQL As String = ""

            'SQL = ""
            'SQL &= "{"
            'SQL &= "  ""resourceType"": ""Encounter"","
            'SQL &= "  ""status"": ""arrived"","
            'SQL &= "  ""class"": {"
            'SQL &= "    ""system"": ""http://terminology.hl7.org/CodeSystem/v3-ActCode"","
            'SQL &= "    ""code"": ""AMB"","
            'SQL &= "    ""display"": ""ambulatory"""
            'SQL &= "  },"
            'SQL &= "  ""subject"": {"
            'SQL &= "    ""reference"": ""Patient/" & idPasien & ""","
            'SQL &= "    ""display"": """ & nmPasien.Replace("'", "") & """"
            'SQL &= "  },"
            'SQL &= "  ""participant"": ["
            'SQL &= "    {"
            'SQL &= "      ""type"": ["
            'SQL &= "        {"
            'SQL &= "          ""coding"": ["
            'SQL &= "            {"
            'SQL &= "              ""system"": ""http://terminology.hl7.org/CodeSystem/v3-ParticipationType"","
            'SQL &= "              ""code"": ""ATND"","
            'SQL &= "              ""display"": ""attender"""
            'SQL &= "            }"
            'SQL &= "          ]"
            'SQL &= "        }"
            'SQL &= "      ],"
            'SQL &= "      ""individual"": {"
            'SQL &= "        ""reference"": ""Practitioner/" & idDokter & ""","
            'SQL &= "        ""display"": """ & nmDokter & """"
            'SQL &= "      }"
            'SQL &= "    }"
            'SQL &= "  ],"
            'SQL &= "  ""period"": {"
            'SQL &= "    ""start"": """ & sDate.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00", CultureInfo.InvariantCulture) & """"
            'SQL &= "  },"
            'SQL &= "  ""location"": ["
            'SQL &= "    {"
            'SQL &= "      ""location"": {"
            'SQL &= "        ""reference"": ""Location/" & idLocation & ""","
            'SQL &= "        ""display"": """ & nmLocation & """"
            'SQL &= "      }"
            'SQL &= "    }"
            'SQL &= "  ],"
            'SQL &= "  ""statusHistory"": ["
            'SQL &= "    {"
            'SQL &= "      ""status"": ""arrived"","
            'SQL &= "      ""period"": {"
            'SQL &= "        ""start"": """ & sDate.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00", CultureInfo.InvariantCulture) & """"
            'SQL &= "      }"
            'SQL &= "    }"
            'SQL &= "  ],"
            'SQL &= "  ""serviceProvider"": {"
            'SQL &= "    ""reference"": ""Organization/" & Organization & """"
            'SQL &= "  },"
            'SQL &= "  ""identifier"": ["
            'SQL &= "    {"
            'SQL &= "      ""system"": ""http://sys-ids.kemkes.go.id/encounter/" & Organization & ""","
            'SQL &= "      ""value"": """ & Register & """"
            'SQL &= "    }"
            'SQL &= "  ]"
            'SQL &= "}"

            Dim dsCek = oPendaftaran_TaskId3.GetData(Register)

            If dsCek Is Nothing Then
                'Dim respon As String = SatusehatAuth.EncounterKunjunganBaru(SatuSehat_Production, SatuSehat_token, SQL)

                'If Not String.IsNullOrEmpty(respon) Then
                '    Dim ID As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "id")
                '    If ID <> "" Then
                '        If fn_SaveSatuSehatAdmisi(Register, ID, SQL, Regex.Replace(respon, "^.{4}", "")) = True Then
                '            Encounter = ID
                '        End If
                '    Else
                '        Dim cektoken As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "code")
                '        If cektoken = "duplicate" Then
                '            If fn_SaveSatuSehatAdmisi(Register, ID, SQL, Regex.Replace(respon, "^.{4}", "")) = True Then
                '                Encounter = ""
                '            End If
                '        ElseIf cektoken = "invalid-access-token"
                '            Dim token As String = SatusehatAuth.GetAccessToken(SatuSehat_Production, SatuSehat_client_id, SatuSehat_client_secret)

                '            SatuSehat_token = SatusehatAuth.GetToken(token, "access_token")

                '            Dim oToken As New Setting.clsSatuSehatKoneksiToken

                '            Dim dsToken = oToken.GetDataSEQ(IIf(SatuSehat_Production = False, "SANDBOX", "PRODUCTION"))
                '            If oToken.UpdateToken(dsToken.KDKONEKSI, dsToken.SEQ, Regex.Replace(respon, "^.{4}", ""), SatuSehat_token) = False Then
                '                SatuSehat_Organisasi = ""
                '                SatuSehat_client_id = ""
                '                SatuSehat_client_secret = ""

                '                'SplashScreenManager.CloseForm(False)

                '                MsgBox("✗ Gagal Simpan Token!" & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)


                '                Exit Function
                '            Else
                '                Dim responulang As String = SatusehatAuth.EncounterKunjunganBaru(SatuSehat_Production, SatuSehat_token, SQL)

                '                Dim IDULANG As String = SatusehatAuth.GetToken(Regex.Replace(responulang, "^.{4}", ""), "id")

                '                If IDULANG <> "" Then
                '                    If fn_SaveSatuSehatAdmisi(Register, IDULANG, SQL, Regex.Replace(responulang, "^.{4}", "")) = True Then
                '                        Encounter = IDULANG
                '                    End If
                '                Else
                '                    Dim cektoken2 As String = SatusehatAuth.GetToken(Regex.Replace(responulang, "^.{4}", ""), "code")
                '                    If cektoken2 = "duplicate" Then
                '                        If fn_SaveSatuSehatAdmisi(Register, IDULANG, SQL, Regex.Replace(responulang, "^.{4}", "")) = True Then
                '                            Encounter = ""
                '                        End If
                '                    End If
                '                End If
                '            End If
                '        Else
                '            If isPesan = True Then
                '                'SplashScreenManager.CloseForm(False)
                '                MsgBox(Statement.ErrorStatement & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)
                '            End If
                '        End If
                '    End If
                'End If
            Else
                Encounter = dsCek.IDSATUSEHAT

                If isPesan = True Then
                    'SplashScreenManager.CloseForm(False)
                    MsgBox("Encounter sudah di simpan", MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If

            'SplashScreenManager.CloseForm(False)

        Catch oErr As Exception
            Encounter = ""
            'SplashScreenManager.CloseForm(False)
            If isPesan = True Then
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End If
        End Try
    End Function
    Private Function ConditionDiagnosis(ByVal isPesan As Boolean, ByVal idIcd10 As String, ByVal nmIcd10 As String, ByVal idPasien As String, ByVal nmPasien As String, ByVal Register As String, ByVal encounter As String, ByVal keterangan As String) As Boolean
        Try
            ConditionDiagnosis = False

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Condition Diagnosis Kirim.....")

            Dim SQL As String = ""

            SQL = ""
            SQL &= "{"
            SQL &= "  ""resourceType"": ""Condition"","
            SQL &= "  ""clinicalStatus"": {"
            SQL &= "    ""coding"": ["
            SQL &= "      {"
            SQL &= "        ""system"": ""http://terminology.hl7.org/CodeSystem/condition-clinical"","
            SQL &= "        ""code"": ""active"","
            SQL &= "        ""display"": ""Active"""
            SQL &= "      }"
            SQL &= "    ]"
            SQL &= "  },"
            SQL &= "  ""category"": ["
            SQL &= "    {"
            SQL &= "      ""coding"": ["
            SQL &= "        {"
            SQL &= "          ""system"": ""http://terminology.hl7.org/CodeSystem/condition-category"","
            SQL &= "          ""code"": ""encounter-diagnosis"","
            SQL &= "          ""display"": ""Encounter Diagnosis"""
            SQL &= "        }"
            SQL &= "      ]"
            SQL &= "    }"
            SQL &= "  ],"
            SQL &= "  ""code"": {"
            SQL &= "    ""coding"": ["
            SQL &= "      {"
            SQL &= "        ""system"": ""http://hl7.org/fhir/sid/icd-10"","
            SQL &= "        ""code"": """ & idIcd10 & ""","
            SQL &= "        ""display"": """ & nmIcd10 & """"
            SQL &= "      }"
            SQL &= "    ]"
            SQL &= "  },"
            SQL &= "  ""subject"": {"
            SQL &= "    ""reference"": ""Patient/" & idPasien & ""","
            SQL &= "    ""display"": """ & nmPasien.Replace("'", "") & """"
            SQL &= "  },"
            SQL &= "  ""encounter"": {"
            SQL &= "    ""reference"": ""Encounter/" & encounter & ""","
            SQL &= "    ""display"": """ & keterangan & """"
            SQL &= "  }"
            SQL &= "}"

            Dim Category As String = "ConditionDiagnosis"
            Dim dsCek = oAnamnesis.GetData(Register, Category)

            If dsCek Is Nothing Then
                Dim url As String = String.Empty

                If SatuSehat_Production = False Then
                    url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Condition"
                Else
                    url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Condition"
                End If

                Dim respon As String = SatusehatAuth.KirimPOST(url, SatuSehat_token, SQL)

                If Not String.IsNullOrEmpty(respon) Then
                    Dim ID As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "id")
                    If ID <> "" Then
                        If fn_SaveSatuSehatAnamnesa(Category, Register, ID, SQL, Regex.Replace(respon, "^.{4}", "")) = True Then
                            ConditionDiagnosis = True
                        End If
                    Else
                        Dim cektoken As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "code")
                        If cektoken = "duplicate" Then
                            If fn_SaveSatuSehatAnamnesa(Category, Register, ID, SQL, Regex.Replace(respon, "^.{4}", "")) = True Then
                                ConditionDiagnosis = True
                            End If
                        ElseIf cektoken = "invalid-access-token"
                            Dim token As String = SatusehatAuth.GetAccessToken(SatuSehat_Production, SatuSehat_client_id, SatuSehat_client_secret)

                            SatuSehat_token = SatusehatAuth.GetToken(token, "access_token")

                            Dim oToken As New Setting.clsSatuSehatKoneksiToken

                            Dim dsToken = oToken.GetDataSEQ(IIf(SatuSehat_Production = False, "SANDBOX", "PRODUCTION"))
                            If oToken.UpdateToken(dsToken.KDKONEKSI, dsToken.SEQ, Regex.Replace(respon, "^.{4}", ""), SatuSehat_token) = False Then
                                SatuSehat_Organisasi = ""
                                SatuSehat_client_id = ""
                                SatuSehat_client_secret = ""

                                SplashScreenManager.CloseForm(False)

                                MsgBox("✗ Gagal Simpan Token!" & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)

                                Exit Function
                            Else
                                Dim responulang As String = SatusehatAuth.EncounterKunjunganBaru(SatuSehat_Production, SatuSehat_token, SQL)

                                Dim IDULANG As String = SatusehatAuth.GetToken(Regex.Replace(responulang, "^.{4}", ""), "id")

                                If IDULANG <> "" Then
                                    If fn_SaveSatuSehatAnamnesa(Category, Register, IDULANG, SQL, Regex.Replace(responulang, "^.{4}", "")) = True Then
                                        ConditionDiagnosis = True
                                    End If
                                Else
                                    Dim cektoken2 As String = SatusehatAuth.GetToken(Regex.Replace(responulang, "^.{4}", ""), "code")
                                    If cektoken2 = "duplicate" Then
                                        If fn_SaveSatuSehatAnamnesa(Category, Register, IDULANG, SQL, Regex.Replace(responulang, "^.{4}", "")) = True Then
                                            ConditionDiagnosis = True
                                        End If
                                    End If
                                End If
                            End If
                        Else
                            If isPesan = True Then
                                SplashScreenManager.CloseForm(False)
                                MsgBox(Statement.ErrorStatement & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)
                            End If
                        End If
                    End If
                End If
            Else
                ConditionDiagnosis = True
                If isPesan = True Then
                    SplashScreenManager.CloseForm(False)
                    MsgBox("ConditionDiagnosis sudah di simpan", MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If

            SplashScreenManager.CloseForm(False)

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            If isPesan = True Then
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End If
        End Try
    End Function
    Private Function ConditionMeninggalkanFaskes(ByVal isPesan As Boolean, ByVal idPasien As String, ByVal nmPasien As String, ByVal Register As String, ByVal encounter As String, ByVal keterangan As String) As Boolean
        Try
            ConditionMeninggalkanFaskes = False

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Condition Diagnosis Kirim.....")

            Dim SQL As String = ""

            SQL = ""
            SQL &= "{"
            SQL &= "  ""resourceType"": ""Condition"","
            SQL &= "  ""clinicalStatus"": {"
            SQL &= "    ""coding"": ["
            SQL &= "      {"
            SQL &= "        ""system"": ""http://terminology.hl7.org/CodeSystem/condition-clinical"","
            SQL &= "        ""code"": ""active"","
            SQL &= "        ""display"": ""Active"""
            SQL &= "      }"
            SQL &= "    ]"
            SQL &= "  },"
            SQL &= "  ""category"": ["
            SQL &= "    {"
            SQL &= "      ""coding"": ["
            SQL &= "        {"
            SQL &= "          ""system"": ""http://terminology.hl7.org/CodeSystem/condition-category"","
            SQL &= "          ""code"": ""encounter-diagnosis"","
            SQL &= "          ""display"": ""Encounter Diagnosis"""
            SQL &= "        }"
            SQL &= "      ]"
            SQL &= "    }"
            SQL &= "  ],"
            SQL &= "  ""code"": {"
            SQL &= "    ""coding"": ["
            SQL &= "      {"
            SQL &= "        ""system"": ""http://snomed.info/sct"","
            SQL &= "        ""code"": ""359746009"","
            SQL &= "        ""display"": ""Patient's condition stable"""
            SQL &= "      }"
            SQL &= "    ]"
            SQL &= "  },"
            SQL &= "  ""subject"": {"
            SQL &= "    ""reference"": ""Patient/" & idPasien & ""","
            SQL &= "    ""display"": """ & nmPasien & """"
            SQL &= "  },"
            SQL &= "  ""encounter"": {"
            SQL &= "    ""reference"": ""Encounter/" & encounter & ""","
            SQL &= "    ""display"": """ & keterangan & """"
            SQL &= "  }"
            SQL &= "}"

            Dim Category As String = "ConditionMeninggalkanFaskes"
            Dim dsCek = oAnamnesis.GetData(Register, Category)

            If dsCek Is Nothing Then
                Dim url As String = String.Empty

                If SatuSehat_Production = False Then
                    url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Condition"
                Else
                    url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Condition"
                End If

                Dim respon As String = SatusehatAuth.KirimPOST(url, SatuSehat_token, SQL)

                If Not String.IsNullOrEmpty(respon) Then
                    Dim ID As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "id")
                    If ID <> "" Then
                        If fn_SaveSatuSehatAnamnesa(Category, Register, ID, SQL, Regex.Replace(respon, "^.{4}", "")) = True Then
                            ConditionMeninggalkanFaskes = True
                        End If
                    Else
                        Dim cektoken As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "code")
                        If cektoken = "duplicate" Then
                            If fn_SaveSatuSehatAnamnesa(Category, Register, ID, SQL, Regex.Replace(respon, "^.{4}", "")) = True Then
                                ConditionMeninggalkanFaskes = True
                            End If
                        ElseIf cektoken = "invalid-access-token"
                            Dim token As String = SatusehatAuth.GetAccessToken(SatuSehat_Production, SatuSehat_client_id, SatuSehat_client_secret)

                            SatuSehat_token = SatusehatAuth.GetToken(token, "access_token")

                            Dim oToken As New Setting.clsSatuSehatKoneksiToken

                            Dim dsToken = oToken.GetDataSEQ(IIf(SatuSehat_Production = False, "SANDBOX", "PRODUCTION"))
                            If oToken.UpdateToken(dsToken.KDKONEKSI, dsToken.SEQ, Regex.Replace(respon, "^.{4}", ""), SatuSehat_token) = False Then
                                SatuSehat_Organisasi = ""
                                SatuSehat_client_id = ""
                                SatuSehat_client_secret = ""

                                SplashScreenManager.CloseForm(False)

                                MsgBox("✗ Gagal Simpan Token!" & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)

                                Exit Function
                            Else
                                Dim responulang As String = SatusehatAuth.EncounterKunjunganBaru(SatuSehat_Production, SatuSehat_token, SQL)

                                Dim IDULANG As String = SatusehatAuth.GetToken(Regex.Replace(responulang, "^.{4}", ""), "id")

                                If IDULANG <> "" Then
                                    If fn_SaveSatuSehatAnamnesa(Category, Register, IDULANG, SQL, Regex.Replace(responulang, "^.{4}", "")) = True Then
                                        ConditionMeninggalkanFaskes = True
                                    End If
                                Else
                                    Dim cektoken2 As String = SatusehatAuth.GetToken(Regex.Replace(responulang, "^.{4}", ""), "code")
                                    If cektoken2 = "duplicate" Then
                                        If fn_SaveSatuSehatAnamnesa(Category, Register, IDULANG, SQL, Regex.Replace(responulang, "^.{4}", "")) = True Then
                                            ConditionMeninggalkanFaskes = True
                                        End If
                                    End If
                                End If
                            End If
                        Else
                            If isPesan = True Then
                                SplashScreenManager.CloseForm(False)
                                MsgBox(Statement.ErrorStatement & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)
                            End If
                        End If
                    End If
                End If
            Else
                ConditionMeninggalkanFaskes = True
                If isPesan = True Then
                    SplashScreenManager.CloseForm(False)
                    MsgBox("ConditionMeninggalkanFaskes sudah di simpan", MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If

            SplashScreenManager.CloseForm(False)

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            If isPesan = True Then
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End If
        End Try
    End Function
    Private Function ConditionKeluhanUtama(ByVal isPesan As Boolean, ByVal idPasien As String, ByVal nmPasien As String, ByVal idDokter As String, ByVal nmDokter As String, ByVal idIcd10 As String, ByVal nmIcd10 As String, ByVal Register As String, ByVal encounter As String, ByVal tgl As String, ByVal keterangan As String) As Boolean
        Try
            ConditionKeluhanUtama = False

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Condition Keluhan Utama Kirim.....")

            Dim SQL As String = FhirBundleGenerator.CreateConditionJsonKeluhanUtama(idPasien, nmPasien, encounter, idDokter, nmDokter, idIcd10, nmIcd10, tgl, tgl, keterangan)

            Dim Category As String = "ConditionKeluhanUtama"
            Dim dsCek = oAnamnesis.GetData(Register, Category)

            If dsCek Is Nothing Then
                Dim url As String = String.Empty

                If SatuSehat_Production = False Then
                    url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Condition"
                Else
                    url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Condition"
                End If

                Dim respon As String = SatusehatAuth.KirimPOST(url, SatuSehat_token, SQL)

                If Not String.IsNullOrEmpty(respon) Then
                    Dim ID As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "id")
                    If ID <> "" Then
                        If fn_SaveSatuSehatAnamnesa(Category, Register, ID, SQL, Regex.Replace(respon, "^.{4}", "")) = True Then
                            ConditionKeluhanUtama = True
                        End If
                    Else
                        Dim cektoken As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "code")
                        If cektoken = "duplicate" Then
                            If fn_SaveSatuSehatAnamnesa(Category, Register, ID, SQL, Regex.Replace(respon, "^.{4}", "")) = True Then
                                ConditionKeluhanUtama = True
                            End If
                        ElseIf cektoken = "invalid-access-token"
                            Dim token As String = SatusehatAuth.GetAccessToken(SatuSehat_Production, SatuSehat_client_id, SatuSehat_client_secret)

                            SatuSehat_token = SatusehatAuth.GetToken(token, "access_token")

                            Dim oToken As New Setting.clsSatuSehatKoneksiToken

                            Dim dsToken = oToken.GetDataSEQ(IIf(SatuSehat_Production = False, "SANDBOX", "PRODUCTION"))
                            If oToken.UpdateToken(dsToken.KDKONEKSI, dsToken.SEQ, Regex.Replace(respon, "^.{4}", ""), SatuSehat_token) = False Then
                                SatuSehat_Organisasi = ""
                                SatuSehat_client_id = ""
                                SatuSehat_client_secret = ""

                                SplashScreenManager.CloseForm(False)

                                MsgBox("✗ Gagal Simpan Token!" & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)

                                Exit Function
                            Else
                                Dim responulang As String = SatusehatAuth.EncounterKunjunganBaru(SatuSehat_Production, SatuSehat_token, SQL)

                                Dim IDULANG As String = SatusehatAuth.GetToken(Regex.Replace(responulang, "^.{4}", ""), "id")

                                If IDULANG <> "" Then
                                    If fn_SaveSatuSehatAnamnesa(Category, Register, IDULANG, SQL, Regex.Replace(responulang, "^.{4}", "")) = True Then
                                        ConditionKeluhanUtama = True
                                    End If
                                Else
                                    Dim cektoken2 As String = SatusehatAuth.GetToken(Regex.Replace(responulang, "^.{4}", ""), "code")
                                    If cektoken2 = "duplicate" Then
                                        If fn_SaveSatuSehatAnamnesa(Category, Register, IDULANG, SQL, Regex.Replace(responulang, "^.{4}", "")) = True Then
                                            ConditionKeluhanUtama = True
                                        End If
                                    End If
                                End If
                            End If
                        Else
                            If isPesan = True Then
                                SplashScreenManager.CloseForm(False)
                                MsgBox(Statement.ErrorStatement & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)
                            End If
                        End If
                    End If
                End If
            Else
                ConditionKeluhanUtama = True
                If isPesan = True Then
                    SplashScreenManager.CloseForm(False)
                    MsgBox("ConditionKeluhanUtama sudah di simpan", MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If

            SplashScreenManager.CloseForm(False)

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            If isPesan = True Then
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End If
        End Try
    End Function
    Private Function ObservationSistolik(ByVal isPesan As Boolean, ByVal idPasien As String, ByVal nmPasien As String, ByVal idDokter As String, ByVal nmDokter As String, ByVal idIcd10 As String, ByVal nmIcd10 As String, ByVal Register As String, ByVal encounter As String, ByVal tgl As String, ByVal keterangan As String) As Boolean
        Try
            ObservationSistolik = False

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Observation Sistolik Kirim.....")

            Dim SQL As String = FhirBundleGenerator.CreateObservationJsonSistol(idPasien, nmPasien, encounter, idDokter, nmDokter, idIcd10, nmIcd10, tgl, tgl, keterangan)

            Dim Category As String = "ObservationSistolik"
            Dim dsCek = oAnamnesis.GetData(Register, Category)

            If dsCek Is Nothing Then
                Dim url As String = String.Empty

                If SatuSehat_Production = False Then
                    url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Observation"
                Else
                    url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Observation"
                End If

                Dim respon As String = SatusehatAuth.KirimPOST(url, SatuSehat_token, SQL)

                If Not String.IsNullOrEmpty(respon) Then
                    Dim ID As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "id")
                    If ID <> "" Then
                        If fn_SaveSatuSehatAnamnesa(Category, Register, ID, SQL, Regex.Replace(respon, "^.{4}", "")) = True Then
                            ObservationSistolik = True
                        End If
                    Else
                        Dim cektoken As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "code")
                        If cektoken = "duplicate" Then
                            If fn_SaveSatuSehatAnamnesa(Category, Register, ID, SQL, Regex.Replace(respon, "^.{4}", "")) = True Then
                                ObservationSistolik = True
                            End If
                        ElseIf cektoken = "invalid-access-token"
                            Dim token As String = SatusehatAuth.GetAccessToken(SatuSehat_Production, SatuSehat_client_id, SatuSehat_client_secret)

                            SatuSehat_token = SatusehatAuth.GetToken(token, "access_token")

                            Dim oToken As New Setting.clsSatuSehatKoneksiToken

                            Dim dsToken = oToken.GetDataSEQ(IIf(SatuSehat_Production = False, "SANDBOX", "PRODUCTION"))
                            If oToken.UpdateToken(dsToken.KDKONEKSI, dsToken.SEQ, Regex.Replace(respon, "^.{4}", ""), SatuSehat_token) = False Then
                                SatuSehat_Organisasi = ""
                                SatuSehat_client_id = ""
                                SatuSehat_client_secret = ""

                                SplashScreenManager.CloseForm(False)

                                MsgBox("✗ Gagal Simpan Token!" & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)

                                Exit Function
                            Else
                                Dim responulang As String = SatusehatAuth.EncounterKunjunganBaru(SatuSehat_Production, SatuSehat_token, SQL)

                                Dim IDULANG As String = SatusehatAuth.GetToken(Regex.Replace(responulang, "^.{4}", ""), "id")

                                If IDULANG <> "" Then
                                    If fn_SaveSatuSehatAnamnesa(Category, Register, IDULANG, SQL, Regex.Replace(responulang, "^.{4}", "")) = True Then
                                        ObservationSistolik = True
                                    End If
                                Else
                                    Dim cektoken2 As String = SatusehatAuth.GetToken(Regex.Replace(responulang, "^.{4}", ""), "code")
                                    If cektoken2 = "duplicate" Then
                                        If fn_SaveSatuSehatAnamnesa(Category, Register, IDULANG, SQL, Regex.Replace(responulang, "^.{4}", "")) = True Then
                                            ObservationSistolik = True
                                        End If
                                    End If
                                End If
                            End If
                        Else
                            If isPesan = True Then
                                SplashScreenManager.CloseForm(False)
                                MsgBox(Statement.ErrorStatement & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)
                            End If
                        End If
                    End If
                End If
            Else
                ObservationSistolik = True
                If isPesan = True Then
                    SplashScreenManager.CloseForm(False)
                    MsgBox("ObservationSistolik sudah di simpan", MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If

            SplashScreenManager.CloseForm(False)

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            If isPesan = True Then
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End If
        End Try
    End Function
    Private Function ObservationDiastolik(ByVal isPesan As Boolean, ByVal idPasien As String, ByVal nmPasien As String, ByVal idDokter As String, ByVal nmDokter As String, ByVal idIcd10 As String, ByVal nmIcd10 As String, ByVal Register As String, ByVal encounter As String, ByVal tgl As String, ByVal keterangan As String) As Boolean
        Try
            ObservationDiastolik = False

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Observation Diastolik Kirim.....")

            Dim SQL As String = FhirBundleGenerator.CreateObservationJsonDiastol(idPasien, nmPasien, encounter, idDokter, nmDokter, idIcd10, nmIcd10, tgl, tgl, keterangan)

            Dim Category As String = "ObservationDiastolik"
            Dim dsCek = oAnamnesis.GetData(Register, Category)

            If dsCek Is Nothing Then
                Dim url As String = String.Empty

                If SatuSehat_Production = False Then
                    url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Observation"
                Else
                    url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Observation"
                End If

                Dim respon As String = SatusehatAuth.KirimPOST(url, SatuSehat_token, SQL)

                If Not String.IsNullOrEmpty(respon) Then
                    Dim ID As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "id")
                    If ID <> "" Then
                        If fn_SaveSatuSehatAnamnesa(Category, Register, ID, SQL, Regex.Replace(respon, "^.{4}", "")) = True Then
                            ObservationDiastolik = True
                        End If
                    Else
                        Dim cektoken As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "code")
                        If cektoken = "duplicate" Then
                            If fn_SaveSatuSehatAnamnesa(Category, Register, ID, SQL, Regex.Replace(respon, "^.{4}", "")) = True Then
                                ObservationDiastolik = True
                            End If
                        ElseIf cektoken = "invalid-access-token"
                            Dim token As String = SatusehatAuth.GetAccessToken(SatuSehat_Production, SatuSehat_client_id, SatuSehat_client_secret)

                            SatuSehat_token = SatusehatAuth.GetToken(token, "access_token")

                            Dim oToken As New Setting.clsSatuSehatKoneksiToken

                            Dim dsToken = oToken.GetDataSEQ(IIf(SatuSehat_Production = False, "SANDBOX", "PRODUCTION"))
                            If oToken.UpdateToken(dsToken.KDKONEKSI, dsToken.SEQ, Regex.Replace(respon, "^.{4}", ""), SatuSehat_token) = False Then
                                SatuSehat_Organisasi = ""
                                SatuSehat_client_id = ""
                                SatuSehat_client_secret = ""

                                SplashScreenManager.CloseForm(False)

                                MsgBox("✗ Gagal Simpan Token!" & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)

                                Exit Function
                            Else
                                Dim responulang As String = SatusehatAuth.EncounterKunjunganBaru(SatuSehat_Production, SatuSehat_token, SQL)

                                Dim IDULANG As String = SatusehatAuth.GetToken(Regex.Replace(responulang, "^.{4}", ""), "id")

                                If IDULANG <> "" Then
                                    If fn_SaveSatuSehatAnamnesa(Category, Register, IDULANG, SQL, Regex.Replace(responulang, "^.{4}", "")) = True Then
                                        ObservationDiastolik = True
                                    End If
                                Else
                                    Dim cektoken2 As String = SatusehatAuth.GetToken(Regex.Replace(responulang, "^.{4}", ""), "code")
                                    If cektoken2 = "duplicate" Then
                                        If fn_SaveSatuSehatAnamnesa(Category, Register, IDULANG, SQL, Regex.Replace(responulang, "^.{4}", "")) = True Then
                                            ObservationDiastolik = True
                                        End If
                                    End If
                                End If
                            End If
                        Else
                            If isPesan = True Then
                                SplashScreenManager.CloseForm(False)
                                MsgBox(Statement.ErrorStatement & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)
                            End If
                        End If
                    End If
                End If
            Else
                ObservationDiastolik = True
                If isPesan = True Then
                    SplashScreenManager.CloseForm(False)
                    MsgBox("ObservationDiastolik sudah di simpan", MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If

            SplashScreenManager.CloseForm(False)

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            If isPesan = True Then
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End If
        End Try
    End Function
    Private Function ObservationSuhuTubuh(ByVal isPesan As Boolean, ByVal idPasien As String, ByVal nmPasien As String, ByVal idDokter As String, ByVal nmDokter As String, ByVal idIcd10 As String, ByVal nmIcd10 As String, ByVal Register As String, ByVal encounter As String, ByVal tgl As String, ByVal keterangan As String) As Boolean
        Try
            ObservationSuhuTubuh = False

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Observation Suhu Tubuh Kirim.....")

            Dim SQL As String = FhirBundleGenerator.CreateObservationJsonSuhuTubuh(idPasien, nmPasien, encounter, idDokter, nmDokter, idIcd10, nmIcd10, tgl, tgl, keterangan)

            Dim Category As String = "ObservationSuhuTubuh"
            Dim dsCek = oAnamnesis.GetData(Register, Category)

            If dsCek Is Nothing Then
                Dim url As String = String.Empty

                If SatuSehat_Production = False Then
                    url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Observation"
                Else
                    url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Observation"
                End If

                Dim respon As String = SatusehatAuth.KirimPOST(url, SatuSehat_token, SQL)

                If Not String.IsNullOrEmpty(respon) Then
                    Dim ID As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "id")
                    If ID <> "" Then
                        If fn_SaveSatuSehatAnamnesa(Category, Register, ID, SQL, Regex.Replace(respon, "^.{4}", "")) = True Then
                            ObservationSuhuTubuh = True
                        End If
                    Else
                        Dim cektoken As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "code")
                        If cektoken = "duplicate" Then
                            If fn_SaveSatuSehatAnamnesa(Category, Register, ID, SQL, Regex.Replace(respon, "^.{4}", "")) = True Then
                                ObservationSuhuTubuh = True
                            End If
                        ElseIf cektoken = "invalid-access-token"
                            Dim token As String = SatusehatAuth.GetAccessToken(SatuSehat_Production, SatuSehat_client_id, SatuSehat_client_secret)

                            SatuSehat_token = SatusehatAuth.GetToken(token, "access_token")

                            Dim oToken As New Setting.clsSatuSehatKoneksiToken

                            Dim dsToken = oToken.GetDataSEQ(IIf(SatuSehat_Production = False, "SANDBOX", "PRODUCTION"))
                            If oToken.UpdateToken(dsToken.KDKONEKSI, dsToken.SEQ, Regex.Replace(respon, "^.{4}", ""), SatuSehat_token) = False Then
                                SatuSehat_Organisasi = ""
                                SatuSehat_client_id = ""
                                SatuSehat_client_secret = ""

                                SplashScreenManager.CloseForm(False)

                                MsgBox("✗ Gagal Simpan Token!" & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)

                                Exit Function
                            Else
                                Dim responulang As String = SatusehatAuth.EncounterKunjunganBaru(SatuSehat_Production, SatuSehat_token, SQL)

                                Dim IDULANG As String = SatusehatAuth.GetToken(Regex.Replace(responulang, "^.{4}", ""), "id")

                                If IDULANG <> "" Then
                                    If fn_SaveSatuSehatAnamnesa(Category, Register, IDULANG, SQL, Regex.Replace(responulang, "^.{4}", "")) = True Then
                                        ObservationSuhuTubuh = True
                                    End If
                                Else
                                    Dim cektoken2 As String = SatusehatAuth.GetToken(Regex.Replace(responulang, "^.{4}", ""), "code")
                                    If cektoken2 = "duplicate" Then
                                        If fn_SaveSatuSehatAnamnesa(Category, Register, IDULANG, SQL, Regex.Replace(responulang, "^.{4}", "")) = True Then
                                            ObservationSuhuTubuh = True
                                        End If
                                    End If
                                End If
                            End If
                        Else
                            If isPesan = True Then
                                SplashScreenManager.CloseForm(False)
                                MsgBox(Statement.ErrorStatement & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)
                            End If
                        End If
                    End If
                End If
            Else
                ObservationSuhuTubuh = True
                If isPesan = True Then
                    SplashScreenManager.CloseForm(False)
                    MsgBox("ObservationSuhuTubuh sudah di simpan", MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If

            SplashScreenManager.CloseForm(False)

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            If isPesan = True Then
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End If
        End Try
    End Function
    Private Function ObservationDenyutJantung(ByVal isPesan As Boolean, ByVal idPasien As String, ByVal nmPasien As String, ByVal idDokter As String, ByVal nmDokter As String, ByVal idIcd10 As String, ByVal nmIcd10 As String, ByVal Register As String, ByVal encounter As String, ByVal tgl As String, ByVal keterangan As String) As Boolean
        Try
            ObservationDenyutJantung = False

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Observation Denyut Jantung Kirim.....")

            Dim SQL As String = FhirBundleGenerator.CreateObservationJsonDenyutJantung(idPasien, nmPasien, encounter, idDokter, nmDokter, idIcd10, nmIcd10, tgl, tgl, keterangan)

            Dim Category As String = "ObservationDenyutJantung"
            Dim dsCek = oAnamnesis.GetData(Register, Category)

            If dsCek Is Nothing Then
                Dim url As String = String.Empty

                If SatuSehat_Production = False Then
                    url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Observation"
                Else
                    url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Observation"
                End If

                Dim respon As String = SatusehatAuth.KirimPOST(url, SatuSehat_token, SQL)

                If Not String.IsNullOrEmpty(respon) Then
                    Dim ID As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "id")
                    If ID <> "" Then
                        If fn_SaveSatuSehatAnamnesa(Category, Register, ID, SQL, Regex.Replace(respon, "^.{4}", "")) = True Then
                            ObservationDenyutJantung = True
                        End If
                    Else
                        Dim cektoken As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "code")
                        If cektoken = "duplicate" Then
                            If fn_SaveSatuSehatAnamnesa(Category, Register, ID, SQL, Regex.Replace(respon, "^.{4}", "")) = True Then
                                ObservationDenyutJantung = True
                            End If
                        ElseIf cektoken = "invalid-access-token"
                            Dim token As String = SatusehatAuth.GetAccessToken(SatuSehat_Production, SatuSehat_client_id, SatuSehat_client_secret)

                            SatuSehat_token = SatusehatAuth.GetToken(token, "access_token")

                            Dim oToken As New Setting.clsSatuSehatKoneksiToken

                            Dim dsToken = oToken.GetDataSEQ(IIf(SatuSehat_Production = False, "SANDBOX", "PRODUCTION"))
                            If oToken.UpdateToken(dsToken.KDKONEKSI, dsToken.SEQ, Regex.Replace(respon, "^.{4}", ""), SatuSehat_token) = False Then
                                SatuSehat_Organisasi = ""
                                SatuSehat_client_id = ""
                                SatuSehat_client_secret = ""

                                SplashScreenManager.CloseForm(False)

                                MsgBox("✗ Gagal Simpan Token!" & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)

                                Exit Function
                            Else
                                Dim responulang As String = SatusehatAuth.EncounterKunjunganBaru(SatuSehat_Production, SatuSehat_token, SQL)

                                Dim IDULANG As String = SatusehatAuth.GetToken(Regex.Replace(responulang, "^.{4}", ""), "id")

                                If IDULANG <> "" Then
                                    If fn_SaveSatuSehatAnamnesa(Category, Register, IDULANG, SQL, Regex.Replace(responulang, "^.{4}", "")) = True Then
                                        ObservationDenyutJantung = True
                                    End If
                                Else
                                    Dim cektoken2 As String = SatusehatAuth.GetToken(Regex.Replace(responulang, "^.{4}", ""), "code")
                                    If cektoken2 = "duplicate" Then
                                        If fn_SaveSatuSehatAnamnesa(Category, Register, IDULANG, SQL, Regex.Replace(responulang, "^.{4}", "")) = True Then
                                            ObservationDenyutJantung = True
                                        End If
                                    End If
                                End If
                            End If
                        Else
                            If isPesan = True Then
                                SplashScreenManager.CloseForm(False)
                                MsgBox(Statement.ErrorStatement & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)
                            End If
                        End If
                    End If
                End If
            Else
                ObservationDenyutJantung = True
                If isPesan = True Then
                    SplashScreenManager.CloseForm(False)
                    MsgBox("ObservationDenyutJantung sudah di simpan", MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If

            SplashScreenManager.CloseForm(False)

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            If isPesan = True Then
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End If
        End Try
    End Function
    Private Function ObservationPernapasan(ByVal isPesan As Boolean, ByVal idPasien As String, ByVal nmPasien As String, ByVal idDokter As String, ByVal nmDokter As String, ByVal idIcd10 As String, ByVal nmIcd10 As String, ByVal Register As String, ByVal encounter As String, ByVal tgl As String, ByVal keterangan As String) As Boolean
        Try
            ObservationPernapasan = False

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Observation Pernapasan Kirim.....")

            Dim SQL As String = FhirBundleGenerator.CreateObservationJsonPernapasan(idPasien, nmPasien, encounter, idDokter, nmDokter, idIcd10, nmIcd10, tgl, tgl, keterangan)

            Dim Category As String = "ObservationPernapasan"
            Dim dsCek = oAnamnesis.GetData(Register, Category)

            If dsCek Is Nothing Then
                Dim url As String = String.Empty

                If SatuSehat_Production = False Then
                    url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Observation"
                Else
                    url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Observation"
                End If

                Dim respon As String = SatusehatAuth.KirimPOST(url, SatuSehat_token, SQL)

                If Not String.IsNullOrEmpty(respon) Then
                    Dim ID As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "id")
                    If ID <> "" Then
                        If fn_SaveSatuSehatAnamnesa(Category, Register, ID, SQL, Regex.Replace(respon, "^.{4}", "")) = True Then
                            ObservationPernapasan = True
                        End If
                    Else
                        Dim cektoken As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "code")
                        If cektoken = "duplicate" Then
                            If fn_SaveSatuSehatAnamnesa(Category, Register, ID, SQL, Regex.Replace(respon, "^.{4}", "")) = True Then
                                ObservationPernapasan = True
                            End If
                        ElseIf cektoken = "invalid-access-token"
                            Dim token As String = SatusehatAuth.GetAccessToken(SatuSehat_Production, SatuSehat_client_id, SatuSehat_client_secret)

                            SatuSehat_token = SatusehatAuth.GetToken(token, "access_token")

                            Dim oToken As New Setting.clsSatuSehatKoneksiToken

                            Dim dsToken = oToken.GetDataSEQ(IIf(SatuSehat_Production = False, "SANDBOX", "PRODUCTION"))
                            If oToken.UpdateToken(dsToken.KDKONEKSI, dsToken.SEQ, Regex.Replace(respon, "^.{4}", ""), SatuSehat_token) = False Then
                                SatuSehat_Organisasi = ""
                                SatuSehat_client_id = ""
                                SatuSehat_client_secret = ""

                                SplashScreenManager.CloseForm(False)

                                MsgBox("✗ Gagal Simpan Token!" & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)

                                Exit Function
                            Else
                                Dim responulang As String = SatusehatAuth.EncounterKunjunganBaru(SatuSehat_Production, SatuSehat_token, SQL)

                                Dim IDULANG As String = SatusehatAuth.GetToken(Regex.Replace(responulang, "^.{4}", ""), "id")

                                If IDULANG <> "" Then
                                    If fn_SaveSatuSehatAnamnesa(Category, Register, IDULANG, SQL, Regex.Replace(responulang, "^.{4}", "")) = True Then
                                        ObservationPernapasan = True
                                    End If
                                Else
                                    Dim cektoken2 As String = SatusehatAuth.GetToken(Regex.Replace(responulang, "^.{4}", ""), "code")
                                    If cektoken2 = "duplicate" Then
                                        If fn_SaveSatuSehatAnamnesa(Category, Register, IDULANG, SQL, Regex.Replace(responulang, "^.{4}", "")) = True Then
                                            ObservationPernapasan = True
                                        End If
                                    End If
                                End If
                            End If
                        Else
                            If isPesan = True Then
                                SplashScreenManager.CloseForm(False)
                                MsgBox(Statement.ErrorStatement & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)
                            End If
                        End If
                    End If
                End If
            Else
                ObservationPernapasan = True
                If isPesan = True Then
                    SplashScreenManager.CloseForm(False)
                    MsgBox("ObservationPernapasan sudah di simpan", MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If

            SplashScreenManager.CloseForm(False)

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            If isPesan = True Then
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End If
        End Try
    End Function
    Private Function ObservationTingkatKesadaran(ByVal isPesan As Boolean, ByVal idPasien As String, ByVal nmPasien As String, ByVal idDokter As String, ByVal nmDokter As String, ByVal kodekesadaran As String, ByVal nmKesadaran As String, ByVal Register As String, ByVal encounter As String, ByVal tgl As String, ByVal keterangan As String) As Boolean
        Try
            ObservationTingkatKesadaran = False

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Observation Tingkat Kesadaran Kirim.....")

            Dim SQL As String = FhirBundleGenerator.CreateObservationJsonTingkatKesadaran(idPasien, nmPasien, encounter, idDokter, nmDokter, kodekesadaran, nmKesadaran, tgl, tgl, keterangan)

            Dim Category As String = "ObservationTingkatKesadaran"
            Dim dsCek = oAnamnesis.GetData(Register, Category)

            If dsCek Is Nothing Then
                Dim url As String = String.Empty

                If SatuSehat_Production = False Then
                    url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Observation"
                Else
                    url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Observation"
                End If

                Dim respon As String = SatusehatAuth.KirimPOST(url, SatuSehat_token, SQL)

                If Not String.IsNullOrEmpty(respon) Then
                    Dim ID As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "id")
                    If ID <> "" Then
                        If fn_SaveSatuSehatAnamnesa(Category, Register, ID, SQL, Regex.Replace(respon, "^.{4}", "")) = True Then
                            ObservationTingkatKesadaran = True
                        End If
                    Else
                        Dim cektoken As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "code")
                        If cektoken = "duplicate" Then
                            If fn_SaveSatuSehatAnamnesa(Category, Register, ID, SQL, Regex.Replace(respon, "^.{4}", "")) = True Then
                                ObservationTingkatKesadaran = True
                            End If
                        ElseIf cektoken = "invalid-access-token"
                            Dim token As String = SatusehatAuth.GetAccessToken(SatuSehat_Production, SatuSehat_client_id, SatuSehat_client_secret)

                            SatuSehat_token = SatusehatAuth.GetToken(token, "access_token")

                            Dim oToken As New Setting.clsSatuSehatKoneksiToken

                            Dim dsToken = oToken.GetDataSEQ(IIf(SatuSehat_Production = False, "SANDBOX", "PRODUCTION"))
                            If oToken.UpdateToken(dsToken.KDKONEKSI, dsToken.SEQ, Regex.Replace(respon, "^.{4}", ""), SatuSehat_token) = False Then
                                SatuSehat_Organisasi = ""
                                SatuSehat_client_id = ""
                                SatuSehat_client_secret = ""

                                SplashScreenManager.CloseForm(False)

                                MsgBox("✗ Gagal Simpan Token!" & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)

                                Exit Function
                            Else
                                Dim responulang As String = SatusehatAuth.EncounterKunjunganBaru(SatuSehat_Production, SatuSehat_token, SQL)

                                Dim IDULANG As String = SatusehatAuth.GetToken(Regex.Replace(responulang, "^.{4}", ""), "id")

                                If IDULANG <> "" Then
                                    If fn_SaveSatuSehatAnamnesa(Category, Register, IDULANG, SQL, Regex.Replace(responulang, "^.{4}", "")) = True Then
                                        ObservationTingkatKesadaran = True
                                    End If
                                Else
                                    Dim cektoken2 As String = SatusehatAuth.GetToken(Regex.Replace(responulang, "^.{4}", ""), "code")
                                    If cektoken2 = "duplicate" Then
                                        If fn_SaveSatuSehatAnamnesa(Category, Register, IDULANG, SQL, Regex.Replace(responulang, "^.{4}", "")) = True Then
                                            ObservationTingkatKesadaran = True
                                        End If
                                    End If
                                End If
                            End If
                        Else
                            If isPesan = True Then
                                SplashScreenManager.CloseForm(False)
                                MsgBox(Statement.ErrorStatement & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)
                            End If
                        End If
                    End If
                End If
            Else
                ObservationTingkatKesadaran = True
                If isPesan = True Then
                    SplashScreenManager.CloseForm(False)
                    MsgBox("ObservationTingkatKesadaran sudah di simpan", MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If

            SplashScreenManager.CloseForm(False)

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            If isPesan = True Then
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End If
        End Try
    End Function
    Private Function ObservationTinggiBadan(ByVal isPesan As Boolean, ByVal idPasien As String, ByVal nmPasien As String, ByVal idDokter As String, ByVal nmDokter As String, ByVal idIcd10 As String, ByVal nmIcd10 As String, ByVal Register As String, ByVal encounter As String, ByVal tgl As String, ByVal keterangan As String) As Boolean
        Try
            ObservationTinggiBadan = False

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Observation Tinggi Badan Kirim.....")

            Dim SQL As String = FhirBundleGenerator.CreateObservationJsonTinggiBadan(idPasien, nmPasien, encounter, idDokter, nmDokter, idIcd10, nmIcd10, tgl, tgl, keterangan)

            Dim Category As String = "ObservationTinggiBadan"
            Dim dsCek = oAnamnesis.GetData(Register, Category)

            If dsCek Is Nothing Then
                Dim url As String = String.Empty

                If SatuSehat_Production = False Then
                    url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Observation"
                Else
                    url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Observation"
                End If

                Dim respon As String = SatusehatAuth.KirimPOST(url, SatuSehat_token, SQL)

                If Not String.IsNullOrEmpty(respon) Then
                    Dim ID As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "id")
                    If ID <> "" Then
                        If fn_SaveSatuSehatAnamnesa(Category, Register, ID, SQL, Regex.Replace(respon, "^.{4}", "")) = True Then
                            ObservationTinggiBadan = True
                        End If
                    Else
                        Dim cektoken As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "code")
                        If cektoken = "duplicate" Then
                            If fn_SaveSatuSehatAnamnesa(Category, Register, ID, SQL, Regex.Replace(respon, "^.{4}", "")) = True Then
                                ObservationTinggiBadan = True
                            End If
                        ElseIf cektoken = "invalid-access-token"
                            Dim token As String = SatusehatAuth.GetAccessToken(SatuSehat_Production, SatuSehat_client_id, SatuSehat_client_secret)

                            SatuSehat_token = SatusehatAuth.GetToken(token, "access_token")

                            Dim oToken As New Setting.clsSatuSehatKoneksiToken

                            Dim dsToken = oToken.GetDataSEQ(IIf(SatuSehat_Production = False, "SANDBOX", "PRODUCTION"))
                            If oToken.UpdateToken(dsToken.KDKONEKSI, dsToken.SEQ, Regex.Replace(respon, "^.{4}", ""), SatuSehat_token) = False Then
                                SatuSehat_Organisasi = ""
                                SatuSehat_client_id = ""
                                SatuSehat_client_secret = ""

                                SplashScreenManager.CloseForm(False)

                                MsgBox("✗ Gagal Simpan Token!" & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)

                                Exit Function
                            Else
                                Dim responulang As String = SatusehatAuth.EncounterKunjunganBaru(SatuSehat_Production, SatuSehat_token, SQL)

                                Dim IDULANG As String = SatusehatAuth.GetToken(Regex.Replace(responulang, "^.{4}", ""), "id")

                                If IDULANG <> "" Then
                                    If fn_SaveSatuSehatAnamnesa(Category, Register, IDULANG, SQL, Regex.Replace(responulang, "^.{4}", "")) = True Then
                                        ObservationTinggiBadan = True
                                    End If
                                Else
                                    Dim cektoken2 As String = SatusehatAuth.GetToken(Regex.Replace(responulang, "^.{4}", ""), "code")
                                    If cektoken2 = "duplicate" Then
                                        If fn_SaveSatuSehatAnamnesa(Category, Register, IDULANG, SQL, Regex.Replace(responulang, "^.{4}", "")) = True Then
                                            ObservationTinggiBadan = True
                                        End If
                                    End If
                                End If
                            End If
                        Else
                            If isPesan = True Then
                                SplashScreenManager.CloseForm(False)
                                MsgBox(Statement.ErrorStatement & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)
                            End If
                        End If
                    End If
                End If
            Else
                ObservationTinggiBadan = True
                If isPesan = True Then
                    SplashScreenManager.CloseForm(False)
                    MsgBox("ObservationTinggiBadan sudah di simpan", MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If

            SplashScreenManager.CloseForm(False)

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            If isPesan = True Then
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End If
        End Try
    End Function
    Private Function ObservationBeratBadan(ByVal isPesan As Boolean, ByVal idPasien As String, ByVal nmPasien As String, ByVal idDokter As String, ByVal nmDokter As String, ByVal idIcd10 As String, ByVal nmIcd10 As String, ByVal Register As String, ByVal encounter As String, ByVal tgl As String, ByVal keterangan As String) As Boolean
        Try
            ObservationBeratBadan = False

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Observation Berat Badan Kirim.....")

            Dim SQL As String = FhirBundleGenerator.CreateObservationJsonBeratBadan(idPasien, nmPasien, encounter, idDokter, nmDokter, idIcd10, nmIcd10, tgl, tgl, keterangan)

            Dim Category As String = "ObservationBeratBadan"
            Dim dsCek = oAnamnesis.GetData(Register, Category)

            If dsCek Is Nothing Then
                Dim url As String = String.Empty

                If SatuSehat_Production = False Then
                    url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Observation"
                Else
                    url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Observation"
                End If

                Dim respon As String = SatusehatAuth.KirimPOST(url, SatuSehat_token, SQL)

                If Not String.IsNullOrEmpty(respon) Then
                    Dim ID As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "id")
                    If ID <> "" Then
                        If fn_SaveSatuSehatAnamnesa(Category, Register, ID, SQL, Regex.Replace(respon, "^.{4}", "")) = True Then
                            ObservationBeratBadan = True
                        End If
                    Else
                        Dim cektoken As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "code")
                        If cektoken = "duplicate" Then
                            If fn_SaveSatuSehatAnamnesa(Category, Register, ID, SQL, Regex.Replace(respon, "^.{4}", "")) = True Then
                                ObservationBeratBadan = True
                            End If
                        ElseIf cektoken = "invalid-access-token"
                            Dim token As String = SatusehatAuth.GetAccessToken(SatuSehat_Production, SatuSehat_client_id, SatuSehat_client_secret)

                            SatuSehat_token = SatusehatAuth.GetToken(token, "access_token")

                            Dim oToken As New Setting.clsSatuSehatKoneksiToken

                            Dim dsToken = oToken.GetDataSEQ(IIf(SatuSehat_Production = False, "SANDBOX", "PRODUCTION"))
                            If oToken.UpdateToken(dsToken.KDKONEKSI, dsToken.SEQ, Regex.Replace(respon, "^.{4}", ""), SatuSehat_token) = False Then
                                SatuSehat_Organisasi = ""
                                SatuSehat_client_id = ""
                                SatuSehat_client_secret = ""

                                SplashScreenManager.CloseForm(False)

                                MsgBox("✗ Gagal Simpan Token!" & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)

                                Exit Function
                            Else
                                Dim responulang As String = SatusehatAuth.EncounterKunjunganBaru(SatuSehat_Production, SatuSehat_token, SQL)

                                Dim IDULANG As String = SatusehatAuth.GetToken(Regex.Replace(responulang, "^.{4}", ""), "id")

                                If IDULANG <> "" Then
                                    If fn_SaveSatuSehatAnamnesa(Category, Register, IDULANG, SQL, Regex.Replace(responulang, "^.{4}", "")) = True Then
                                        ObservationBeratBadan = True
                                    End If
                                Else
                                    Dim cektoken2 As String = SatusehatAuth.GetToken(Regex.Replace(responulang, "^.{4}", ""), "code")
                                    If cektoken2 = "duplicate" Then
                                        If fn_SaveSatuSehatAnamnesa(Category, Register, IDULANG, SQL, Regex.Replace(responulang, "^.{4}", "")) = True Then
                                            ObservationBeratBadan = True
                                        End If
                                    End If
                                End If
                            End If
                        Else
                            If isPesan = True Then
                                SplashScreenManager.CloseForm(False)
                                MsgBox(Statement.ErrorStatement & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)
                            End If
                        End If
                    End If
                End If
            Else
                ObservationBeratBadan = True
                If isPesan = True Then
                    SplashScreenManager.CloseForm(False)
                    MsgBox("ObservationBeratBadan sudah di simpan", MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If

            SplashScreenManager.CloseForm(False)

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            If isPesan = True Then
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End If
        End Try
    End Function
    Private Function Procedure(ByVal isPesan As Boolean,ByVal idDokter As String, ByVal nmDokter As String, ByVal idIcd10 As String, ByVal nmIcd10 As String, ByVal Register As String, ByVal encounter As String, ByVal tgl As String, ByVal idItemSS As String, ByVal nmItemSS As String, ByVal keterangan As String) As Boolean
        Try
            Procedure = False

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Procedure Kirim.....")

            Dim SQL As String = ""

            SQL = ""
            SQL &= "{"
            SQL &= "  ""resourceType"": ""Procedure"","
            SQL &= "  ""status"": ""completed"","
            SQL &= "  ""category"": {"
            SQL &= "    ""coding"": ["
            SQL &= "      {"
            SQL &= "        ""system"": ""http://snomed.info/sct"","
            SQL &= "        ""code"": ""103693007"","
            SQL &= "        ""display"": ""Diagnostic procedure"""
            SQL &= "      }"
            SQL &= "    ],"
            SQL &= "    ""text"": ""Diagnostic procedure"""
            SQL &= "  },"
            SQL &= "  ""code"": {"

            Dim ds = oRIdentitasGrouperData.GetDataByRegisterData(Register)

            If ds IsNot Nothing Then
                BuildCodingArrayFromDataTable(ds.kodegrouper)
            End If

            'SQL &= "    ""coding"": ["
            'SQL &= "      {"
            'SQL &= "        ""system"": ""http://hl7.org/fhir/sid/icd-9-cm"","
            'SQL &= "        ""code"": ""87.44"","
            'SQL &= "        ""display"": ""Routine chest x-ray, so described"""
            'SQL &= "      }"
            'SQL &= "    ]"


            SQL &= "  },"
            SQL &= "  ""subject"": {"
            SQL &= "    ""reference"": ""Patient/100000030009"","
            SQL &= "    ""display"": ""Budi Santoso"""
            SQL &= "  },"
            SQL &= "  ""encounter"": {"
            SQL &= "    ""reference"": ""Encounter/" & encounter & ""","
            SQL &= "    ""display"": ""Tindakan Rontgen Dada Budi Santoso pada Selasa tanggal 14 Juni 2022"""
            SQL &= "  },"
            SQL &= "  ""performedPeriod"": {"
            SQL &= "    ""start"": """ & tgl & ""","
            SQL &= "    ""end"": """ & tgl & """"
            SQL &= "  },"
            SQL &= "  ""performer"": ["
            SQL &= "    {"
            SQL &= "      ""actor"": {"
            SQL &= "        ""reference"": ""Practitioner/" & idDokter & ""","
            SQL &= "        ""display"": """ & nmDokter & """"
            SQL &= "      }"
            SQL &= "    }"
            SQL &= "  ],"
            SQL &= "  ""reasonCode"": ["
            SQL &= "    {"
            SQL &= "      ""coding"": ["
            SQL &= "        {"
            SQL &= "          ""system"": ""http://hl7.org/fhir/sid/icd-10"","
            SQL &= "          ""code"": """ & idIcd10 & ""","
            SQL &= "          ""display"": """ & nmIcd10 & """"
            SQL &= "        }"
            SQL &= "      ]"
            SQL &= "    }"
            SQL &= "  ],"
            SQL &= "  ""bodySite"": ["
            SQL &= "    {"
            SQL &= "      ""coding"": ["
            SQL &= "        {"
            SQL &= "          ""system"": ""http://snomed.info/sct"","
            SQL &= "          ""code"": """ & idItemSS & ""","
            SQL &= "          ""display"": """ & nmItemSS & """"
            SQL &= "        }"
            SQL &= "      ]"
            SQL &= "    }"
            SQL &= "  ],"
            SQL &= "  ""note"": ["
            SQL &= "    {"
            SQL &= "      ""text"": """ & keterangan & """"
            SQL &= "    }"
            SQL &= "  ]"
            SQL &= "}"

            Dim Category As String = "Procedure"
            Dim dsCek = oAnamnesis.GetData(Register, Category)

            If dsCek Is Nothing Then
                Dim url As String = String.Empty

                If SatuSehat_Production = False Then
                    url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1"
                Else
                    url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1"
                End If

                Dim respon As String = SatusehatAuth.KirimPOST(url, SatuSehat_token, SQL)

                If Not String.IsNullOrEmpty(respon) Then
                    Dim ID As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "id")
                    If ID <> "" Then
                        If fn_SaveSatuSehatAnamnesa(Category, Register, ID, SQL, Regex.Replace(respon, "^.{4}", "")) = True Then
                            Procedure = True
                        End If
                    Else
                        Dim cektoken As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "code")
                        If cektoken = "duplicate" Then
                            If fn_SaveSatuSehatAnamnesa(Category, Register, ID, SQL, Regex.Replace(respon, "^.{4}", "")) = True Then
                                Procedure = True
                            End If
                        ElseIf cektoken = "invalid-access-token"
                            Dim token As String = SatusehatAuth.GetAccessToken(SatuSehat_Production, SatuSehat_client_id, SatuSehat_client_secret)

                            SatuSehat_token = SatusehatAuth.GetToken(token, "access_token")

                            Dim oToken As New Setting.clsSatuSehatKoneksiToken

                            Dim dsToken = oToken.GetDataSEQ(IIf(SatuSehat_Production = False, "SANDBOX", "PRODUCTION"))
                            If oToken.UpdateToken(dsToken.KDKONEKSI, dsToken.SEQ, Regex.Replace(respon, "^.{4}", ""), SatuSehat_token) = False Then
                                SatuSehat_Organisasi = ""
                                SatuSehat_client_id = ""
                                SatuSehat_client_secret = ""

                                SplashScreenManager.CloseForm(False)

                                MsgBox("✗ Gagal Simpan Token!" & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)

                                Exit Function
                            Else
                                Dim responulang As String = SatusehatAuth.EncounterKunjunganBaru(SatuSehat_Production, SatuSehat_token, SQL)

                                Dim IDULANG As String = SatusehatAuth.GetToken(Regex.Replace(responulang, "^.{4}", ""), "id")

                                If IDULANG <> "" Then
                                    If fn_SaveSatuSehatAnamnesa(Category, Register, IDULANG, SQL, Regex.Replace(responulang, "^.{4}", "")) = True Then
                                        Procedure = True
                                    End If
                                Else
                                    Dim cektoken2 As String = SatusehatAuth.GetToken(Regex.Replace(responulang, "^.{4}", ""), "code")
                                    If cektoken2 = "duplicate" Then
                                        If fn_SaveSatuSehatAnamnesa(Category, Register, IDULANG, SQL, Regex.Replace(responulang, "^.{4}", "")) = True Then
                                            Procedure = True
                                        End If
                                    End If
                                End If
                            End If
                        Else
                            If isPesan = True Then
                                SplashScreenManager.CloseForm(False)
                                MsgBox(Statement.ErrorStatement & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)
                            End If
                        End If
                    End If
                End If
            Else
                Procedure = True
                If isPesan = True Then
                    SplashScreenManager.CloseForm(False)
                    MsgBox("Procedure sudah di simpan", MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If

            SplashScreenManager.CloseForm(False)

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            If isPesan = True Then
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End If
        End Try
    End Function
    ' Fungsi helper untuk membangun array coding
    Public Function BuildCodingArrayFromDataTable(ByVal kodegrouper As Integer) As String
        Dim oRIdentitasGrouperData As New Grouper.clsR_Identitas_Grouper_Data

        Dim sb As New System.Text.StringBuilder()
        sb.Append("[")

        Dim i As Integer = 0

        For Each xloop In oRIdentitasGrouperData.GetDataDetailProsedur(kodegrouper).OrderBy(Function(x) x.seq)
            If i > 0 Then sb.Append(",")

            sb.Append("{")
            sb.Append("""system"": """ & "http://hl7.org/fhir/sid/icd-9-cm" & """,")
            sb.Append("""code"": """ & xloop.kdprpsedur & """,")
            sb.Append("""display"": """ & xloop.memo & """")
            sb.Append("}")

            i += 1
        Next

        sb.Append("]")
        Return sb.ToString()
    End Function
#End Region
#End Region
#Region "Bak"
    Private Function IsianCPPT(ByVal isPesan As Boolean, Register As String, idpasien As String, snowmed As String, snowmeddisplay As String, idsatusehat As String) As Boolean
        Try
            IsianCPPT = False

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Observation TTV Kirim.....")

            Dim dsCPPT = oGrouperDataCppt.GetDataByKodePendaftaranRawatJalanDokter(Register)

            If dsCPPT IsNot Nothing Then
                Dim dsDaftar = oPendaftaran.GetData(Register)
                If dsDaftar IsNot Nothing Then
                    Dim KeluhanUtama As Boolean = False
                    Dim KeluhanPenyerta As Boolean = False
                    Dim ObservasiSistolik As Boolean = False
                    Dim ObservasiDiastolik As Boolean = False
                    Dim ObservasiSuhuTubuh As Boolean = False
                    Dim ObservasiDenyutJantung As Boolean = False
                    Dim ObservasiPernapasan As Boolean = False
                    Dim ObservasiTingkatKesadaran As Boolean = False
                    Dim ObservasiTinggiBadan As Boolean = False
                    Dim ObservasiBeratBadan As Boolean = False
                    Dim RiwayatPerjalananPenyakit As Boolean = False
                    Dim TujuanPerawatan As Boolean = False
                    Dim RencanaRawatJalan As Boolean = False

                    For Each xloop In oAnamnesis.GetDataByRegister(Register)
                        If xloop.CATEGORY = "KELUHAN UTAMA" Then
                            KeluhanUtama = True
                        End If
                        If xloop.CATEGORY = "KELUHAN PENYERTA" Then
                            KeluhanPenyerta = True
                        End If
                        If xloop.CATEGORY = "OBSERVATION - SISTOLIK" Then
                            ObservasiSistolik = True
                        End If
                        If xloop.CATEGORY = "OBSERVATION - DIASTOLIK" Then
                            ObservasiDiastolik = True
                        End If
                        If xloop.CATEGORY = "SUHU TUBUH" Then
                            ObservasiSuhuTubuh = True
                        End If
                        If xloop.CATEGORY = "DENYUT JANTUNG" Then
                            ObservasiDenyutJantung = True
                        End If
                        If xloop.CATEGORY = "PERNAPASAN" Then
                            ObservasiPernapasan = True
                        End If
                        If xloop.CATEGORY = "TINGKAT KESADARAN" Then
                            ObservasiTingkatKesadaran = True
                        End If
                        If xloop.CATEGORY = "TINGGI BADAN" Then
                            ObservasiTinggiBadan = True
                        End If
                        If xloop.CATEGORY = "BERAT BADAN" Then
                            ObservasiBeratBadan = True
                        End If
                        If xloop.CATEGORY = "RIWAYAT PERJALANAN PENYAKIT" Then
                            RiwayatPerjalananPenyakit = True
                        End If
                        If xloop.CATEGORY = "TUJUAN PERAWATAN" Then
                            TujuanPerawatan = True
                        End If
                        'If xloop.CATEGORY = "RENCANA RAWAT JALAN" Then
                        '    RencanaRawatJalan = True
                        'End If
                    Next

                    If KeluhanUtama = False Then
                        If KirimCPPT("KELUHAN UTAMA", Register, dsDaftar.M_DOCTOR.M_DOCTOR_SATUSEHAT.IDSATUSEHAT, dsDaftar.M_DOCTOR.NAME_DISPLAY, idpasien, snowmed, snowmeddisplay, idsatusehat, dsCPPT.DATE.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), dsCPPT.DATE.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), dsCPPT.SUBJEKTIF_KELUHANUTAMA, dsCPPT.A_IDENTITASPASIEN_LIST.NAMAPASIEN) = True Then
                            IsianCPPT = True
                        Else
                            IsianCPPT = False
                        End If
                    End If

                    If KeluhanPenyerta = False Then
                        For Each xloop In oGrouperDataCppt.GetDataDetailDiagnosa(dsCPPT.KDCPPT)
                            If xloop.KATEGORI <> "Primary" Then
                                If xloop.KDDIAGNOSA <> "" Then
                                    Dim oDiagnosa As New Reference.clsDiagnosa
                                    Dim dsDiagnosa = oDiagnosa.GetData(xloop.KDDIAGNOSA)
                                    If dsDiagnosa IsNot Nothing Then
                                        If KirimCPPT("KELUHAN PENYERTA", Register, dsDaftar.M_DOCTOR.M_DOCTOR_SATUSEHAT.IDSATUSEHAT, dsDaftar.M_DOCTOR.NAME_DISPLAY, idpasien, dsDiagnosa.KDDIAGNOSA, dsDiagnosa.MEMO, idsatusehat, dsCPPT.DATE.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), dsCPPT.DATE.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), dsCPPT.SUBJEKTIF_KELUHANUTAMA, dsCPPT.A_IDENTITASPASIEN_LIST.NAMAPASIEN) = True Then
                                            IsianCPPT = True
                                        Else
                                            IsianCPPT = False
                                        End If
                                    End If
                                End If
                            End If
                        Next
                    End If

                    If ObservasiSistolik = False Then
                        If KirimCPPT("OBSERVATION - SISTOLIK", Register, dsDaftar.M_DOCTOR.M_DOCTOR_SATUSEHAT.IDSATUSEHAT, dsDaftar.M_DOCTOR.NAME_DISPLAY, idpasien, snowmed, snowmeddisplay, idsatusehat, dsCPPT.DATE.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), dsCPPT.DATE.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), dsCPPT.OBJEKTIF_SISTOLE, dsCPPT.A_IDENTITASPASIEN_LIST.NAMAPASIEN) = True Then
                            IsianCPPT = True
                        Else
                            IsianCPPT = False
                        End If
                    End If
                    If ObservasiDiastolik = False Then
                        If KirimCPPT("OBSERVATION - DIASTOLIK", Register, dsDaftar.M_DOCTOR.M_DOCTOR_SATUSEHAT.IDSATUSEHAT, dsDaftar.M_DOCTOR.NAME_DISPLAY, idpasien, snowmed, snowmeddisplay, idsatusehat, dsCPPT.DATE.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), dsCPPT.DATE.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), dsCPPT.OBJEKTIF_DIASTOLE, dsCPPT.A_IDENTITASPASIEN_LIST.NAMAPASIEN) = True Then
                            IsianCPPT = True
                        Else
                            IsianCPPT = False
                        End If
                    End If
                    If ObservasiSuhuTubuh = False Then
                        If KirimCPPT("SUHU TUBUH", Register, dsDaftar.M_DOCTOR.M_DOCTOR_SATUSEHAT.IDSATUSEHAT, dsDaftar.M_DOCTOR.NAME_DISPLAY, idpasien, snowmed, snowmeddisplay, idsatusehat, dsCPPT.DATE.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), dsCPPT.DATE.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), dsCPPT.OBJEKTIF_SUHU, dsCPPT.A_IDENTITASPASIEN_LIST.NAMAPASIEN) = True Then
                            IsianCPPT = True
                        Else
                            IsianCPPT = False
                        End If
                    End If
                    If ObservasiDenyutJantung = False Then
                        If KirimCPPT("DENYUT JANTUNG", Register, dsDaftar.M_DOCTOR.M_DOCTOR_SATUSEHAT.IDSATUSEHAT, dsDaftar.M_DOCTOR.NAME_DISPLAY, idpasien, snowmed, snowmeddisplay, idsatusehat, dsCPPT.DATE.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), dsCPPT.DATE.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), dsCPPT.OBJEKTIF_HR, dsCPPT.A_IDENTITASPASIEN_LIST.NAMAPASIEN) = True Then
                            IsianCPPT = True
                        Else
                            IsianCPPT = False
                        End If
                    End If
                    If ObservasiPernapasan = False Then
                        If KirimCPPT("PERNAPASAN", Register, dsDaftar.M_DOCTOR.M_DOCTOR_SATUSEHAT.IDSATUSEHAT, dsDaftar.M_DOCTOR.NAME_DISPLAY, idpasien, snowmed, snowmeddisplay, idsatusehat, dsCPPT.DATE.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), dsCPPT.DATE.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), dsCPPT.OBJEKTIF_RR, dsCPPT.A_IDENTITASPASIEN_LIST.NAMAPASIEN) = True Then
                            IsianCPPT = True
                        Else
                            IsianCPPT = False
                        End If
                    End If
                    If ObservasiTingkatKesadaran = False Then
                        Dim kodetingkatkesadaran As String = ""
                        Dim kodetingkatkesadaranname As String = ""

                        If dsCPPT.OBJEKTIF_KESADARAN.Contains("Compos Mentis") Then
                            kodetingkatkesadaran = "248234008"
                            kodetingkatkesadaranname = "Mentally alert"
                        End If
                        If dsCPPT.OBJEKTIF_KESADARAN.Contains("cm") Then
                            kodetingkatkesadaran = "248234008"
                            kodetingkatkesadaranname = "Mentally alert"
                        End If
                        If dsCPPT.OBJEKTIF_KESADARAN.Contains("baik") Then
                            kodetingkatkesadaran = "248234008"
                            kodetingkatkesadaranname = "Mentally alert"
                        End If
                        If dsCPPT.OBJEKTIF_KESADARAN.Contains("Somnolen") Then
                            kodetingkatkesadaran = ""
                            kodetingkatkesadaranname = ""
                        End If
                        If dsCPPT.OBJEKTIF_KESADARAN.Contains("Sopor") Then
                            kodetingkatkesadaran = ""
                            kodetingkatkesadaranname = ""
                        End If
                        If dsCPPT.OBJEKTIF_KESADARAN.Contains("Koma") Then
                            kodetingkatkesadaran = ""
                            kodetingkatkesadaranname = ""
                        End If
                        If dsCPPT.OBJEKTIF_KESADARAN.Contains("Stupor") Then
                            kodetingkatkesadaran = ""
                            kodetingkatkesadaranname = ""
                        End If

                        If kodetingkatkesadaran <> "" Then
                            If KirimCPPT("TINGKAT KESADARAN", Register, dsDaftar.M_DOCTOR.M_DOCTOR_SATUSEHAT.IDSATUSEHAT, dsDaftar.M_DOCTOR.NAME_DISPLAY, idpasien, kodetingkatkesadaran, kodetingkatkesadaranname, idsatusehat, dsCPPT.DATE.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), dsCPPT.DATE.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), dsCPPT.OBJEKTIF_RR, dsCPPT.A_IDENTITASPASIEN_LIST.NAMAPASIEN) = True Then
                                IsianCPPT = True
                            Else
                                IsianCPPT = False
                            End If
                        End If
                    End If

                    If ObservasiTinggiBadan = False Then
                        If KirimCPPT("TINGGI BADAN", Register, dsDaftar.M_DOCTOR.M_DOCTOR_SATUSEHAT.IDSATUSEHAT, dsDaftar.M_DOCTOR.NAME_DISPLAY, idpasien, snowmed, snowmeddisplay, idsatusehat, dsCPPT.DATE.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), dsCPPT.DATE.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), dsCPPT.OBJEKTIF_TINGGIBADAN, dsCPPT.A_IDENTITASPASIEN_LIST.NAMAPASIEN) = True Then
                            IsianCPPT = True
                        Else
                            IsianCPPT = False
                        End If
                    End If
                    If ObservasiBeratBadan = False Then
                        If KirimCPPT("BERAT BADAN", Register, dsDaftar.M_DOCTOR.M_DOCTOR_SATUSEHAT.IDSATUSEHAT, dsDaftar.M_DOCTOR.NAME_DISPLAY, idpasien, snowmed, snowmeddisplay, idsatusehat, dsCPPT.DATE.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), dsCPPT.DATE.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), dsCPPT.OBJEKTIF_BERATBADAN, dsCPPT.A_IDENTITASPASIEN_LIST.NAMAPASIEN) = True Then
                            IsianCPPT = True
                        Else
                            IsianCPPT = False
                        End If
                    End If
                    If RiwayatPerjalananPenyakit = False Then
                        If dsCPPT.OBJEKTIF_PEMERIKSAAN <> "" Then
                            If KirimCPPT("RIWAYAT PERJALANAN PENYAKIT", Register, dsDaftar.M_DOCTOR.M_DOCTOR_SATUSEHAT.IDSATUSEHAT, dsDaftar.M_DOCTOR.NAME_DISPLAY, idpasien, snowmed, snowmeddisplay, idsatusehat, dsCPPT.DATE.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), dsCPPT.DATE.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), dsCPPT.OBJEKTIF_PEMERIKSAAN, dsCPPT.A_IDENTITASPASIEN_LIST.NAMAPASIEN) = True Then
                                IsianCPPT = True
                            Else
                                IsianCPPT = False
                            End If
                        End If
                    End If
                    'If TujuanPerawatan = False Then
                    '    If dsCPPT.PLANNING_ALASAN <> "" Then
                    '        If KirimCPPT("TUJUAN PERAWATAN", Register, dsDaftar.M_DOCTOR.M_DOCTOR_SATUSEHAT.IDSATUSEHAT, dsDaftar.M_DOCTOR.NAME_DISPLAY, idpasien, snowmed, snowmeddisplay, idsatusehat, dsCPPT.DATE.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), dsCPPT.DATE.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), dsCPPT.PLANNING_ALASAN, dsCPPT.A_IDENTITASPASIEN_LIST.NAMAPASIEN) = True Then
                    '            IsianCPPT = True
                    '        Else
                    '            IsianCPPT = False
                    '        End If
                    '    End If
                    'End If
                    'If RencanaRawatJalan = False Then
                    '    If dsCPPT.PLANNING_ALASAN <> "" Then
                    '        If KirimCPPT("RENCANA RAWAT JALAN", Register, dsDaftar.M_DOCTOR.M_DOCTOR_SATUSEHAT.IDSATUSEHAT, dsDaftar.M_DOCTOR.NAME_DISPLAY, idpasien, snowmed, snowmeddisplay, idsatusehat, dsCPPT.DATE.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), dsCPPT.DATE.AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss+00:00"), dsCPPT.PLANNING_ALASAN, dsCPPT.A_IDENTITASPASIEN_LIST.NAMAPASIEN) = True Then
                    '            IsianCPPT = True
                    '        Else
                    '            IsianCPPT = False
                    '        End If
                    '    End If
                    'End If
                End If
            Else
                IsianCPPT = False
                If isPesan = True Then
                    SplashScreenManager.CloseForm(False)
                    MsgBox("CPPT Belum dibuat", MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            IsianCPPT = False
            If isPesan = True Then
                SplashScreenManager.CloseForm(False)
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End If
        End Try
    End Function
    Private Function KirimCPPT(category As String, register As String, iddoctor As String, iddoctordisplay As String, idpasien As String, snowmed As String, snowmedDisplay As String, ByVal idreg As String, tanggal1 As String, tanggal2 As String, isian As String, namapasien As String) As Boolean
        Try
            KirimCPPT = False

            Dim jsondata As String = ""
            Dim respon As String = ""

            If category = "KELUHAN UTAMA" Then
                jsondata = FhirBundleGenerator.CreateConditionJsonKeluhanUtama(idpasien, namapasien, idreg, iddoctor, iddoctordisplay, snowmed, snowmedDisplay, tanggal1, tanggal2, isian)
            ElseIf category = "KELUHAN PENYERTA"
                jsondata = FhirBundleGenerator.CreateConditionJsonKeluhanPenyerta(idpasien, namapasien, idreg, iddoctor, iddoctordisplay, snowmed, snowmedDisplay, tanggal1, tanggal2)
            ElseIf category = "OBSERVATION - SISTOLIK"
                jsondata = FhirBundleGenerator.CreateObservationJsonSistol(idpasien, namapasien, idreg, iddoctor, iddoctordisplay, snowmed, snowmedDisplay, tanggal1, tanggal2, CInt(isian))
            ElseIf category = "OBSERVATION - DIASTOLIK"
                jsondata = FhirBundleGenerator.CreateObservationJsonDiastol(idpasien, namapasien, idreg, iddoctor, iddoctordisplay, snowmed, snowmedDisplay, tanggal1, tanggal2, CInt(isian))
            ElseIf category = "SUHU TUBUH"
                jsondata = FhirBundleGenerator.CreateObservationJsonSuhuTubuh(idpasien, namapasien, idreg, iddoctor, iddoctordisplay, snowmed, snowmedDisplay, tanggal1, tanggal2, CInt(isian.Replace(",", ".")))
            ElseIf category = "DENYUT JANTUNG"
                jsondata = FhirBundleGenerator.CreateObservationJsonDenyutJantung(idpasien, namapasien, idreg, iddoctor, iddoctordisplay, snowmed, snowmedDisplay, tanggal1, tanggal2, CInt(isian.Replace(",", ".")))
            ElseIf category = "PERNAPASAN"
                jsondata = FhirBundleGenerator.CreateObservationJsonPernapasan(idpasien, namapasien, idreg, iddoctor, iddoctordisplay, snowmed, snowmedDisplay, tanggal1, tanggal2, CInt(isian.Replace(",", ".")))
            ElseIf category = "TINGKAT KESADARAN"
                jsondata = FhirBundleGenerator.CreateObservationJsonTingkatKesadaran(idpasien, namapasien, idreg, iddoctor, iddoctordisplay, snowmed, snowmedDisplay, tanggal1, tanggal2, CInt(isian.Replace(",", ".")))
            ElseIf category = "TINGGI BADAN"
                jsondata = FhirBundleGenerator.CreateObservationJsonTinggiBadan(idpasien, namapasien, idreg, iddoctor, iddoctordisplay, snowmed, snowmedDisplay, tanggal1, tanggal2, CInt(isian.Replace(",", ".")))
            ElseIf category = "BERAT BADAN"
                jsondata = FhirBundleGenerator.CreateObservationJsonBeratBadan(idpasien, namapasien, idreg, iddoctor, iddoctordisplay, snowmed, snowmedDisplay, tanggal1, tanggal2, CInt(isian.Replace(",", ".")))
            ElseIf category = "RIWAYAT PERJALANAN PENYAKIT"
                jsondata = FhirBundleGenerator.BuildClinicalImpressionJsonAnonymous(idpasien, namapasien, idreg, iddoctor, iddoctordisplay, snowmed, snowmedDisplay, tanggal1, tanggal2, isian)
                'ElseIf category = "TUJUAN PERAWATAN"
                'jsondata = FhirBundleGenerator.D(idpasien, namapasien, idreg, iddoctor, iddoctordisplay, snowmed, snowmedDisplay, tanggal1, tanggal2, isian)
                'ElseIf category = "RENCANA RAWAT JALAN"
                'jsondata = FhirBundleGenerator.D(idpasien, namapasien, idreg, iddoctor, iddoctordisplay, snowmed, snowmedDisplay, tanggal1, tanggal2, isian)
            End If

            If jsondata = "" Then
                Exit Function
            End If

            respon = kirimSatuSehat(category, jsondata)

            If Not String.IsNullOrEmpty(respon) Then
                If Not respon.Contains("Exception") Then
                    If respon.Contains("200") Or respon.Contains("201") Then
                        Dim ID As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "id")
                        If ID <> "" Then
                            If fn_SaveSatuSehatAnamnesa(category, register, ID, jsondata, Regex.Replace(respon, "^.{4}", "")) = True Then
                                KirimCPPT = True
                            End If
                        Else
                            Dim cektoken As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "code")
                            If cektoken = "duplicate" Then
                                If fn_SaveSatuSehatAnamnesa(category, register, ID, jsondata, Regex.Replace(respon, "^.{4}", "")) = True Then
                                    KirimCPPT = True
                                End If
                            End If
                        End If
                    Else
                        Dim cektoken As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "code")
                        If cektoken = "invalid-access-token" Then
                            Dim token As String = SatusehatAuth.GetAccessToken(SatuSehat_Production, SatuSehat_client_id, SatuSehat_client_secret)

                            SatuSehat_token = SatusehatAuth.GetToken(token, "access_token")

                            Dim oToken As New Setting.clsSatuSehatKoneksiToken

                            Dim dsToken = oToken.GetDataSEQ(IIf(SatuSehat_Production = False, "SANDBOX", "PRODUCTION"))

                            If dsToken IsNot Nothing Then
                                If oToken.UpdateToken(dsToken.KDKONEKSI, dsToken.SEQ, Regex.Replace(respon, "^.{4}", ""), SatuSehat_token) = False Then
                                    SatuSehat_Organisasi = ""
                                    SatuSehat_client_id = ""
                                    SatuSehat_client_secret = ""

                                    MsgBox("✗ Gagal Simpan Token!" & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)

                                    Exit Function
                                Else
                                    Dim responulang As String = kirimSatuSehat(category, jsondata)

                                    Dim ID As String = SatusehatAuth.GetToken(Regex.Replace(responulang, "^.{4}", ""), "id")

                                    If ID <> "" Then
                                        If fn_SaveSatuSehatAnamnesa(category, register, ID, jsondata, Regex.Replace(responulang, "^.{4}", "")) = True Then
                                            KirimCPPT = True
                                        End If
                                    Else
                                        Dim cektoken2 As String = SatusehatAuth.GetToken(Regex.Replace(responulang, "^.{4}", ""), "code")
                                        If cektoken2 = "duplicate" Then
                                            If fn_SaveSatuSehatAnamnesa(category, register, ID, jsondata, Regex.Replace(responulang, "^.{4}", "")) = True Then
                                                KirimCPPT = True
                                            End If
                                        End If
                                    End If
                                End If
                            Else
                                SatuSehat_Organisasi = ""
                                SatuSehat_client_id = ""
                                SatuSehat_client_secret = ""

                                MsgBox("✗ Token Database Kosong, Silahkan Add Terlebih Dahulu di menu Setting!" & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)

                                Exit Function
                            End If
                        ElseIf cektoken = "duplicate"
                            If fn_SaveSatuSehatAnamnesa(category, register, "", jsondata, Regex.Replace(respon, "^.{4}", "")) = True Then
                                KirimCPPT = True
                            End If
                        End If
                    End If
                End If
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function

#End Region
End Class