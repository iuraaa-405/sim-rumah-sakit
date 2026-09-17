Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports DevExpress.XtraPrinting
Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.XtraSplashScreen

Public Class frmtEvaluasiKlaim
    Implements ILanguage

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sStok = True

        Me.Text = "Laporan Evaluasi"

        lTYPE.Text = Report.FILTER_TYPE
        lDATEFROM.Text = Report.FILTER_DATEFROM
        lDATETO.Text = Report.FILTER_DATETO

        cboTYPE.Properties.Items.Clear()
        cboTYPE.Properties.Items.Add("Rawat Jalan")
        cboTYPE.Properties.Items.Add("Rawat Inap")
        'cboTYPE.Properties.Items.Add("Semua")
        cboTYPE.SelectedIndex = 0

        deDATEFrom.DateTime = Now.AddDays((-Now.Day) + 1)
        deDATETo.DateTime = Now
    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed

    End Sub
    Public Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            Me.Text = "Laporan Evaluasi"

            lTYPE.Text = Report.FILTER_TYPE
            lDATEFROM.Text = Report.FILTER_DATEFROM
            lDATETO.Text = Report.FILTER_DATETO

            cboTYPE.Properties.Items.Clear()
            cboTYPE.Properties.Items.Add("Rawat Jalan")
            cboTYPE.Properties.Items.Add("Rawat Inap")
            'cboTYPE.Properties.Items.Add("Semua")

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
                      Where x.MODUL = "REKAPEVALUASI" _
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
{"Laporan Evaluasi ", cboTYPE.Text & vbCrLf & deDATEFrom.DateTime.ToString("dd/MM/yyyy") & " - " & deDATETo.DateTime.ToString("dd/MM/yyyy"), ""})

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

            Select Case cboTYPE.SelectedIndex
                Case 0
                    fn_LoadData(0)
                Case 1
                    fn_LoadData(1)
                Case 2
                    fn_LoadData(2)
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

            SQL = "EXEC LAPORANEVALUASI @CATEGORY = '" & Kategori & "', @DARITANGGAL = '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "', @SAMPAITANGGAL = '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "

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

    End Sub
#End Region

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
    Private Sub picUploadTXT_Click() Handles picUploadTXT.Click
        Dim fBrowse As New OpenFileDialog
        With fBrowse
            .Filter = "Txt files(*.txt)|*.txt|All files (*.*)|*.*"
            .FilterIndex = 1
            .Title = "Import data from Txt file"
        End With

        If fBrowse.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim sProcess As Integer = 0
            Dim sTotal As Integer = 0

            Dim oUmpanBalik As New Sales.clsUmpanBalikBPJS
            Dim reader As New System.IO.StreamReader(fBrowse.FileName)
            Dim allLines As List(Of String) = New List(Of String)
            Dim RecordLine As List(Of String) = New List(Of String)

            Do While Not reader.EndOfStream
                allLines.Add(reader.ReadLine())
                sTotal += 1
            Loop

            If MsgBox("Apa anda yakin akan mengimport " & sTotal - 1 & " Baris data ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

            Dim list_UmpanBalik As New List(Of DataAccess.S_UMPANBALIK)
            Dim Tes As String = String.Empty

            Try
                SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

                For Each xLoop In allLines

                    RecordLine.Add(xLoop)

                    Dim Record As List(Of String) = New List(Of String)

                    Dim KODE_RS As String = String.Empty
                    Dim KELAS_RS As String = String.Empty
                    Dim KELAS_RAWAT As String = String.Empty
                    Dim KODE_TARIF As String = String.Empty
                    Dim PTD As String = String.Empty
                    Dim ADMISSION_DATE As String = String.Empty
                    Dim DISCHARGE_DATE As String = String.Empty
                    Dim BIRTH_DATE As String = String.Empty
                    Dim BIRTH_WEIGHT As String = String.Empty
                    Dim SEX As String = String.Empty
                    Dim DISCHARGE_STATUS As String = String.Empty
                    Dim DIAGLIST As String = String.Empty
                    Dim PROCLIST As String = String.Empty
                    Dim ADL1 As String = String.Empty
                    Dim ADL2 As String = String.Empty
                    Dim IN_SP As String = String.Empty
                    Dim IN_SR As String = String.Empty
                    Dim IN_SI As String = String.Empty
                    Dim IN_SD As String = String.Empty
                    Dim INACBG As String = String.Empty
                    Dim SUBACUTE As String = String.Empty
                    Dim CHRONIC As String = String.Empty
                    Dim SP As String = String.Empty
                    Dim SR As String = String.Empty
                    Dim SI As String = String.Empty
                    Dim SD As String = String.Empty
                    Dim DESKRIPSI_INACBG As String = String.Empty
                    Dim TARIF_INACBG As String = String.Empty
                    Dim TARIF_SUBACUTE As String = String.Empty
                    Dim TARIF_CHRONIC As String = String.Empty
                    Dim DESKRIPSI_SP As String = String.Empty
                    Dim TARIF_SP As String = String.Empty
                    Dim DESKRIPSI_SR As String = String.Empty
                    Dim TARIF_SR As String = String.Empty
                    Dim DESKRIPSI_SI As String = String.Empty
                    Dim TARIF_SI As String = String.Empty
                    Dim DESKRIPSI_SD As String = String.Empty
                    Dim TARIF_SD As String = String.Empty
                    Dim TOTAL_TARIF As String = String.Empty
                    Dim TARIF_RS As String = String.Empty
                    Dim TARIF_POLI_EKS As String = String.Empty
                    Dim LOS As String = String.Empty
                    Dim ICU_INDIKATOR As String = String.Empty
                    Dim ICU_LOS As String = String.Empty
                    Dim VENT_HOUR As String = String.Empty
                    Dim NAMA_PASIEN As String = String.Empty
                    Dim MRN As String = String.Empty
                    Dim UMUR_TAHUN As String = String.Empty
                    Dim UMUR_HARI As String = String.Empty
                    Dim DPJP As String = String.Empty
                    Dim SEP As String = String.Empty
                    Dim NOKARTU As String = String.Empty
                    Dim PAYOR_ID As String = String.Empty
                    Dim CODER_ID As String = String.Empty
                    Dim VERSI_INACBG As String = String.Empty
                    Dim VERSI_GROUPER As String = String.Empty
                    Dim C1 As String = String.Empty
                    Dim C2 As String = String.Empty
                    Dim C3 As String = String.Empty
                    Dim C4 As String = String.Empty
                    Dim PROSEDUR_NON_BEDAH As String = String.Empty
                    Dim PROSEDUR_BEDAH As String = String.Empty
                    Dim KONSULTASI As String = String.Empty
                    Dim TENAGA_AHLI As String = String.Empty
                    Dim KEPERAWATAN As String = String.Empty
                    Dim PENUNJANG As String = String.Empty
                    Dim RADIOLOGI As String = String.Empty
                    Dim LABORATORIUM As String = String.Empty
                    Dim PELAYANAN_DARAH As String = String.Empty
                    Dim REHABILITASI As String = String.Empty
                    Dim KAMAR_AKOMODASI As String = String.Empty
                    Dim RAWAT_INTENSIF As String = String.Empty
                    Dim OBAT As String = String.Empty
                    Dim ALKES As String = String.Empty
                    Dim BMHP As String = String.Empty
                    Dim SEWA_ALAT As String = String.Empty
                    Dim OBAT_KRONIS As String = String.Empty
                    Dim OBAT_KEMO As String = String.Empty

                    If RecordLine.Count > 1 Then

                        For Each field As String In xLoop.Split(New String() {ControlChars.Tab}, StringSplitOptions.None)
                            Record.Add(field)

                            If Record.Count = 1 Then
                                KODE_RS = field
                            End If
                            If Record.Count = 2 Then
                                KELAS_RS = field
                            End If
                            If Record.Count = 3 Then
                                KELAS_RAWAT = field
                            End If
                            If Record.Count = 4 Then
                                KODE_TARIF = field
                            End If
                            If Record.Count = 5 Then
                                PTD = field
                            End If
                            If Record.Count = 6 Then
                                ADMISSION_DATE = field
                            End If
                            If Record.Count = 7 Then
                                DISCHARGE_DATE = field
                            End If
                            If Record.Count = 8 Then
                                BIRTH_DATE = field
                            End If
                            If Record.Count = 9 Then
                                BIRTH_WEIGHT = field
                            End If
                            If Record.Count = 10 Then
                                SEX = field
                            End If
                            If Record.Count = 11 Then
                                DISCHARGE_STATUS = field
                            End If
                            If Record.Count = 12 Then
                                DIAGLIST = field
                            End If
                            If Record.Count = 13 Then
                                PROCLIST = field
                            End If
                            If Record.Count = 14 Then
                                ADL1 = field
                            End If
                            If Record.Count = 15 Then
                                ADL2 = field
                            End If
                            If Record.Count = 16 Then
                                IN_SP = field
                            End If
                            If Record.Count = 17 Then
                                IN_SR = field
                            End If
                            If Record.Count = 18 Then
                                IN_SI = field
                            End If
                            If Record.Count = 19 Then
                                IN_SD = field
                            End If
                            If Record.Count = 20 Then
                                INACBG = field
                            End If
                            If Record.Count = 21 Then
                                SUBACUTE = field
                            End If
                            If Record.Count = 22 Then
                                CHRONIC = field
                            End If
                            If Record.Count = 23 Then
                                SP = field
                            End If
                            If Record.Count = 24 Then
                                SR = field
                            End If
                            If Record.Count = 25 Then
                                SI = field
                            End If
                            If Record.Count = 26 Then
                                SD = field
                            End If
                            If Record.Count = 27 Then
                                DESKRIPSI_INACBG = field
                            End If
                            If Record.Count = 28 Then
                                TARIF_INACBG = field
                            End If
                            If Record.Count = 29 Then
                                TARIF_SUBACUTE = field
                            End If
                            If Record.Count = 30 Then
                                TARIF_CHRONIC = field
                            End If
                            If Record.Count = 31 Then
                                DESKRIPSI_SP = field
                            End If
                            If Record.Count = 32 Then
                                TARIF_SP = field
                            End If
                            If Record.Count = 33 Then
                                DESKRIPSI_SR = field
                            End If
                            If Record.Count = 34 Then
                                TARIF_SR = field
                            End If
                            If Record.Count = 35 Then
                                DESKRIPSI_SI = field
                            End If
                            If Record.Count = 36 Then
                                TARIF_SI = field
                            End If
                            If Record.Count = 37 Then
                                DESKRIPSI_SD = field
                            End If
                            If Record.Count = 38 Then
                                TARIF_SD = field
                            End If
                            If Record.Count = 39 Then
                                TOTAL_TARIF = field
                            End If
                            If Record.Count = 40 Then
                                TARIF_RS = field
                            End If
                            If Record.Count = 41 Then
                                TARIF_POLI_EKS = field
                            End If
                            If Record.Count = 42 Then
                                LOS = field
                            End If
                            If Record.Count = 43 Then
                                ICU_INDIKATOR = field
                            End If
                            If Record.Count = 44 Then
                                ICU_LOS = field
                            End If
                            If Record.Count = 45 Then
                                VENT_HOUR = field
                            End If
                            If Record.Count = 46 Then
                                NAMA_PASIEN = field
                            End If
                            If Record.Count = 47 Then
                                MRN = field
                            End If
                            If Record.Count = 48 Then
                                UMUR_TAHUN = field
                            End If
                            If Record.Count = 49 Then
                                UMUR_HARI = field
                            End If
                            If Record.Count = 50 Then
                                DPJP = field
                            End If
                            If Record.Count = 51 Then
                                SEP = field
                            End If
                            If Record.Count = 52 Then
                                NOKARTU = field
                            End If
                            If Record.Count = 53 Then
                                PAYOR_ID = field
                            End If
                            If Record.Count = 54 Then
                                CODER_ID = field
                            End If
                            If Record.Count = 55 Then
                                VERSI_INACBG = field
                            End If
                            If Record.Count = 56 Then
                                VERSI_GROUPER = field
                            End If
                            If Record.Count = 57 Then
                                C1 = field
                            End If
                            If Record.Count = 58 Then
                                C2 = field
                            End If
                            If Record.Count = 59 Then
                                C3 = field
                            End If
                            If Record.Count = 60 Then
                                C4 = field
                            End If
                            If Record.Count = 61 Then
                                PROSEDUR_NON_BEDAH = field
                            End If
                            If Record.Count = 62 Then
                                PROSEDUR_BEDAH = field
                            End If
                            If Record.Count = 63 Then
                                KONSULTASI = field
                            End If
                            If Record.Count = 64 Then
                                TENAGA_AHLI = field
                            End If
                            If Record.Count = 65 Then
                                KEPERAWATAN = field
                            End If
                            If Record.Count = 66 Then
                                PENUNJANG = field
                            End If
                            If Record.Count = 67 Then
                                RADIOLOGI = field
                            End If
                            If Record.Count = 68 Then
                                LABORATORIUM = field
                            End If
                            If Record.Count = 69 Then
                                PELAYANAN_DARAH = field
                            End If
                            If Record.Count = 70 Then
                                REHABILITASI = field
                            End If
                            If Record.Count = 71 Then
                                KAMAR_AKOMODASI = field
                            End If
                            If Record.Count = 72 Then
                                RAWAT_INTENSIF = field
                            End If
                            If Record.Count = 73 Then
                                OBAT = field
                            End If
                            If Record.Count = 74 Then
                                ALKES = field
                            End If
                            If Record.Count = 75 Then
                                BMHP = field
                            End If
                            If Record.Count = 76 Then
                                SEWA_ALAT = field
                            End If
                            If Record.Count = 77 Then
                                OBAT_KRONIS = field
                            End If
                            If Record.Count = 78 Then
                                OBAT_KEMO = field
                            End If

                        Next

                    End If

                    If SEP <> "" Then
                        Dim dsDetail = oUmpanBalik.GetStructureDetail

                        With dsDetail
                            Dim dsDateCreated = oUmpanBalik.GetData(SEP)
                            If dsDateCreated IsNot Nothing Then
                                .DATECREATED = dsDateCreated.DATECREATED
                            Else
                                .DATECREATED = Now
                            End If
                            .DATEUPDATED = Now

                            .KODE_RS = KODE_RS
                            .KELAS_RS = KELAS_RS
                            .KELAS_RAWAT = KELAS_RAWAT
                            .KODE_TARIF = KODE_TARIF
                            .PTD = PTD
                            .ADMISSION_DATE = ADMISSION_DATE
                            .DISCHARGE_DATE = DISCHARGE_DATE
                            .BIRTH_DATE = BIRTH_DATE
                            .BIRTH_WEIGHT = BIRTH_WEIGHT
                            .SEX = SEX
                            .DISCHARGE_STATUS = DISCHARGE_STATUS
                            .DIAGLIST = DIAGLIST
                            .PROCLIST = PROCLIST
                            .ADL1 = ADL1
                            .ADL2 = ADL2
                            .IN_SP = IN_SP
                            .IN_SR = IN_SR
                            .IN_SI = IN_SI
                            .IN_SD = IN_SD
                            .INACBG = INACBG
                            .SUBACUTE = SUBACUTE
                            .CHRONIC = CHRONIC
                            .SP = SP
                            .SR = SR
                            .SI = SI
                            .SD = SD
                            .DESKRIPSI_INACBG = DESKRIPSI_INACBG
                            .TARIF_INACBG = TARIF_INACBG
                            .TARIF_SUBACUTE = TARIF_SUBACUTE
                            .TARIF_CHRONIC = TARIF_CHRONIC
                            .DESKRIPSI_SP = DESKRIPSI_SP
                            .TARIF_SP = TARIF_SP
                            .DESKRIPSI_SR = DESKRIPSI_SR
                            .TARIF_SR = TARIF_SR
                            .DESKRIPSI_SI = DESKRIPSI_SI
                            .TARIF_SI = TARIF_SI
                            .DESKRIPSI_SD = DESKRIPSI_SD
                            .TARIF_SD = TARIF_SD
                            .TOTAL_TARIF = TOTAL_TARIF
                            .TARIF_RS = TARIF_RS
                            .TARIF_POLI_EKS = TARIF_POLI_EKS
                            .LOS = LOS
                            .ICU_INDIKATOR = ICU_INDIKATOR
                            .ICU_LOS = ICU_LOS
                            .VENT_HOUR = VENT_HOUR
                            .NAMA_PASIEN = NAMA_PASIEN
                            .MRN = MRN
                            .UMUR_TAHUN = UMUR_TAHUN
                            .UMUR_HARI = UMUR_HARI
                            .DPJP = DPJP
                            .SEP = SEP
                            .NOKARTU = NOKARTU
                            .PAYOR_ID = PAYOR_ID
                            .CODER_ID = CODER_ID
                            .VERSI_INACBG = VERSI_INACBG
                            .VERSI_GROUPER = VERSI_GROUPER
                            .C1 = C1
                            .C2 = C2
                            .C3 = C3
                            .C4 = C4
                            .PROSEDUR_NON_BEDAH = PROSEDUR_NON_BEDAH
                            .PROSEDUR_BEDAH = PROSEDUR_BEDAH
                            .KONSULTASI = KONSULTASI
                            .TENAGA_AHLI = TENAGA_AHLI
                            .KEPERAWATAN = KEPERAWATAN
                            .PENUNJANG = PENUNJANG
                            .RADIOLOGI = RADIOLOGI
                            .LABORATORIUM = LABORATORIUM
                            .PELAYANAN_DARAH = PELAYANAN_DARAH
                            .REHABILITASI = REHABILITASI
                            .KAMAR_AKOMODASI = KAMAR_AKOMODASI
                            .RAWAT_INTENSIF = RAWAT_INTENSIF
                            .OBAT = OBAT
                            .ALKES = ALKES
                            .BMHP = BMHP
                            .SEWA_ALAT = SEWA_ALAT
                            .OBAT_KRONIS = OBAT_KRONIS
                            .OBAT_KEMO = OBAT_KEMO

                            .KDUSER = sUserID

                            If oUmpanBalik.IsExist(SEP) = False Then
                                oUmpanBalik.InsertData(dsDetail)
                            Else
                                oUmpanBalik.UpdateData(dsDetail)
                            End If

                        End With

                    End If


                    SplashScreenManager.Default.SetWaitFormCaption("Processing data " & sProcess & " of " & sTotal - 1 & "")

                    sProcess += 1

                Next

            Catch ex As Exception
                MsgBox("Load Data " & Tes & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                SplashScreenManager.CloseForm(False)
                MsgBox("Export Selesai", MsgBoxStyle.Information, Me.Text)
            End Try

        End If
    End Sub
#End Region
End Class