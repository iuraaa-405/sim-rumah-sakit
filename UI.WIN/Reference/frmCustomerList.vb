Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient
Imports DevExpress.XtraSplashScreen

Public Class frmCustomerList
    Implements ILanguage

    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oCustomer As New Reference.clsCustomer

#Region "Function"
    Private Sub Me_Load() Handles Me.Load
        Me.Text = Customer.TITLE

        chkTop1000.Checked = True
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
                      Where x.MODUL = "CUSTOMER" _
                     And y.KDUSER = sUserID
                      Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Dim dsCPPT = (From x In oOtority.GetDataDetail
                          Join y In oUser.GetData
                        On x.KDOTORITY Equals y.KDOTORITY
                          Where x.MODUL = "MEDREK_RJ" _
                            And y.KDUSER = sUserID
                          Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                picCPPTRJ.Enabled = dsCPPT.ISVIEW
                picCPPTRI.Enabled = dsCPPT.ISVIEW
            Catch ex As Exception
                picCPPTRJ.Enabled = False
                picCPPTRI.Enabled = False
            End Try
            Try
                picAdd.Enabled = ds.ISADD
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
            Me.Text = Customer.TITLE

            grv.Columns("KDCUSTOMER").Caption = Customer.KDCUSTOMER
            grv.Columns("NAME_DISPLAY").Caption = Customer.NAME_DISPLAY
            grv.Columns("KDCOA").Caption = Customer.KDCOA
            grv.Columns("EMAIL").Caption = Customer.EMAIL
            grv.Columns("PHONE").Caption = Customer.PHONE
            grv.Columns("MOBILE").Caption = Customer.MOBILE
            grv.Columns("FAX").Caption = Customer.FAX
            grv.Columns("OTHER").Caption = Customer.OTHER
            grv.Columns("WEBSITE").Caption = Customer.WEBSITE
            'grv.Columns("BILL_STREET").Caption = Customer.BILL_STREET
            'grv.Columns("BILL_CITY").Caption = Customer.BILL_CITY
            'grv.Columns("BILL_STATE").Caption = Customer.BILL_STATE
            'grv.Columns("BILL_ZIP").Caption = Customer.BILL_ZIP
            'grv.Columns("BILL_COUNTRY").Caption = Customer.BILL_COUNTRY
            grv.Columns("MEMO").Caption = Customer.MEMO
            grv.Columns("ISACTIVE").Caption = Customer.ISACTIVE

            ColumnChooserToolStripMenuItem.Text = Caption.ColumnChooser
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData()
        'Try
        '    Dim ds = From x In oCustomer.GetData
        '             Select x.KDCUSTOMER, x.NAME_DISPLAY, x.KDCOA, x.EMAIL, x.PHONE, x.MOBILE, x.FAX, x.OTHER, x.WEBSITE, x.MEMO, x.ISACTIVE

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
            If chkTop1000.Checked = True Then
                SQL &= "TOP 1000 "
            End If

            SQL &= "A.KDCUSTOMER "
            SQL &= ",NORMRLAMA = A.KDCUSTOMER_LAMA "
            SQL &= ",IHS = ISNULL((SELECT IDSATUSEHAT FROM M_CUSTOMER_SATUSEHAT WHERE A.KDCUSTOMER = KDCUSTOMER), '') "
            SQL &= ",NONIK = A.KTP "
            SQL &= ",NOKARTUBPJS = A.KARTUBPJS "
            SQL &= ",A.TANGGALLAHIR "
            SQL &= ",A.NAME_DISPLAY "
            SQL &= ",A.ALAMAT "
            SQL &= ",A.KDCOA "
            SQL &= ",A.EMAIL  "
            SQL &= ",A.PHONE "
            SQL &= ",A.MOBILE "
            SQL &= ",A.FAX "
            SQL &= ",A.OTHER "
            SQL &= ",A.WEBSITE "
            SQL &= ",A.MEMO "
            SQL &= ",A.ISACTIVE "
            SQL &= "FROM "
            SQL &= "M_CUSTOMER A "
            SQL &= "ORDER BY A.DATECREATED DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "HISTORY")

            grd.MainView = grv
            grd.DataSource = ds.Tables("HISTORY")
            grd.ForceInitialize()

            fn_LoadFormatData()

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
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

        'grv.Columns("KDCUSTOMER").Visible = False
        grv.Columns("KDCOA").Visible = False
        grv.Columns("EMAIL").Visible = False
        'grv.Columns("PHONE").Visible = False
        grv.Columns("MOBILE").Visible = False
        grv.Columns("FAX").Visible = False
        grv.Columns("OTHER").Visible = False
        grv.Columns("WEBSITE").Visible = False
        'grv.Columns("BILL_STREET").Visible = False
        'grv.Columns("BILL_CITY").Visible = False
        'grv.Columns("BILL_STATE").Visible = False
        'grv.Columns("BILL_ZIP").Visible = False
        'grv.Columns("BILL_COUNTRY").Visible = False
        grv.Columns("MEMO").Visible = False
        'grv.Columns("ISACTIVE").Visible = False

        ' grv.Columns("KDCUSTOMER").OptionsColumn.ShowInCustomizationForm = False
    End Sub
    Private Sub ColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ColumnChooserToolStripMenuItem.Click
        grv.ShowCustomization()
    End Sub
    Private Function fn_DeleteData(ByVal sKDCUSTOMER As String) As Boolean
        Try
            oCustomer.DeleteData(sKDCUSTOMER)
            fn_DeleteData = True
        Catch oErr As Exception
            fn_DeleteData = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
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
    Private Sub grv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grv.DoubleClick
        If grv.GetFocusedRowCellValue("KDCUSTOMER") Is Nothing Then
            Exit Sub
        End If

        Dim frmCustomer As New frmCustomer
        Try
            frmCustomer.LoadMe(FORM_MODE.FORM_MODE_VIEW, Nothing, grv.GetFocusedRowCellValue("KDCUSTOMER"))
            frmCustomer.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picCPPTRJ_Click() Handles picCPPTRJ.Click
        If grv.GetFocusedRowCellValue("KDCUSTOMER") Is Nothing Then
            Exit Sub
        End If

        fn_LoadHistoryPasienCPPT(grv.GetFocusedRowCellValue("KDCUSTOMER"))
    End Sub
    Private Sub fn_LoadHistoryPasienCPPT(ByVal sKDCUSTOMER As String)
        Try
            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data CPPT.....")

            Dim dsCustomer = oCustomer.GetData(sKDCUSTOMER)
            If dsCustomer IsNot Nothing Then
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
                ''SQL &= "AND A.CATEGORY = " & Category & " "
                'If chkCPPTDokter.Checked = False Then
                '    SQL &= "AND ISNULL((SELECT MEMO FROM R_CPPT_M_PROFESI WHERE B.KDPROFESI = KDPROFESI), '') LIKE '%DOKTER%' "
                'End If

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
                    sFind1_cppt = dsCustomer.KDCUSTOMER
                    sFind2_cppt = dsCustomer.NAME_DISPLAY
                    sFind3_cppt = dsCustomer.TANGGALLAHIR.ToString("dd-MM-yyyy")

                    Dim FolderSimpan = "C:/SIMRS/CPPT/"

                    If Not IO.Directory.Exists(FolderSimpan) Then
                        IO.Directory.CreateDirectory(FolderSimpan)
                    Else
                        DeleteDirectory(FolderSimpan)
                        IO.Directory.CreateDirectory(FolderSimpan)
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
                    Else
                        SplashScreenManager.CloseForm(False)
                        MsgBox("CPPT Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                    End If

                End If

                sFind1_cppt = String.Empty
                sFind2_cppt = String.Empty
                sFind3_cppt = String.Empty

            End If

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

            Dim FolderSimpan = "C:/CPPTRANAP/"

            If Not IO.Directory.Exists(FolderSimpan) Then
                IO.Directory.CreateDirectory(FolderSimpan)
            Else
                DeleteDirectory(FolderSimpan)
                IO.Directory.CreateDirectory(FolderSimpan)
            End If

            Dim oCPPT As New Transaksi.clsCPPT
            Dim dataList As New List(Of Byte())
            'Dim dsLoad() As Byte
            Dim ds = oCPPT.GetDataByRMRANAP(sKDCUSTOMER)

            If ds.Count > 0 Then
                Dim rpt As New xtraDigital_CPPT_01_RawatInap

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.bindingSource.DataSource = ds
                rpt.ExportToPdf(FolderSimpan & "HASIL" & ".pdf")
                'dataList.Add(IO.File.ReadAllBytes(FolderSimpan & "HASIL" & ".pdf"))

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
                Else
                    SplashScreenManager.CloseForm(False)
                    MsgBox("CPPT Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak CPPT Ranap" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Sub DeleteDirectory(path As String)
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
    Private Sub picAdd_Click() Handles picAdd.Click
        Dim frmCustomer As New frmCustomer
        Try
            frmCustomer.LoadMe(FORM_MODE.FORM_MODE_ADD, Nothing)
            frmCustomer.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmCustomer Is Nothing Then frmCustomer.Dispose()
            frmCustomer = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("NAME_DISPLAY"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KDCUSTOMER") Is Nothing Then
            Exit Sub
        End If
        Dim frmCustomer As New frmCustomer
        Try
            frmCustomer.LoadMe(FORM_MODE.FORM_MODE_EDIT, Nothing, grv.GetFocusedRowCellValue("KDCUSTOMER"))
            frmCustomer.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmCustomer Is Nothing Then frmCustomer.Dispose()
            frmCustomer = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("NAME_DISPLAY"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KDCUSTOMER") Is Nothing Then
            Exit Sub
        End If
        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_DeleteData(grv.GetFocusedRowCellValue("KDCUSTOMER")) = False Then
            MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        fn_LoadSecurity()
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        Try
            Dim xtraCustomer As New xtraCustomer

            Dim sKDCUSTOMER As New List(Of String)
            For i As Integer = 0 To grv.RowCount - 1
                sKDCUSTOMER.Add(grv.GetRowCellValue(i, "KDCUSTOMER"))
            Next

            Dim ds = oCustomer.GetData.Where(Function(x) sKDCUSTOMER.Contains(x.KDCUSTOMER)).ToList

            xtraCustomer.bindingSource.DataSource = ds
            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(xtraCustomer)
            printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_LoadSecurity()
    End Sub

    Private Sub picRefresh_Click(sender As Object, e As EventArgs) Handles picRefresh.Click

    End Sub

    Private Sub picCPPT_Click(sender As Object, e As EventArgs) Handles picCPPTRI.Click, picCPPTRJ.Click

    End Sub
#End Region
End Class