Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient
Imports Newtonsoft.Json.Linq
Imports System.IO
Imports DevExpress.XtraPdfViewer
Imports DevExpress.XtraPdfViewer.Commands
Imports DevExpress.XtraSplashScreen

Public Class frmSuratKeteranganPerawatan
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private sKdkunjungan As String
    Private skdcustomer As String
    Private isLoad As Boolean = False
    Private oSuratKeteranganPerawatan As New SuratInfokes.clsSuratKeteranganPerawatan
    Private AlamatSimpan = "C:/SIMRS/PRIVIEW/"
    Private sPesanTTE As String = String.Empty

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal kdkunjungan As String, ByVal kdcustomer As String, ByVal NoId As String)
        oFormMode = FormMode
        sNoId = NoId
        sKdkunjungan = kdkunjungan
        skdcustomer = kdcustomer

        Dim oRingkasanKeluar As New Digital.clsResumeRawatInap
        Dim dsRingkasanKeluar = oRingkasanKeluar.GetData(kdkunjungan)

        If dsRingkasanKeluar IsNot Nothing Then
            deDATE_DARI.DateTime = dsRingkasanKeluar.R_IDENTITAS_PASIEN.DATE
            deDATE_SAMPAI.DateTime = dsRingkasanKeluar.DATE
        Else
            MsgBox("Ringkasan Keluar Belum diBuat", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "Surat Keterangan Perawatan"

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtCODE.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDOKUMEN()

        Select Case oFormMode
            Case FORM_MODE.FORM_MODE_VIEW
                fn_ViewMode(True)
                fn_LoadData()
            Case FORM_MODE.FORM_MODE_ADD
                fn_ViewMode(False)
                fn_EmptyMe()
            Case FORM_MODE.FORM_MODE_EDIT
                fn_ViewMode(False)
                fn_LoadData()
            Case Else
                fn_ViewMode(True)
        End Select

        fn_loadKunjungan(txtKDKUNJUNGAN.Text)

        If Not IO.Directory.Exists(AlamatSimpan) Then
            IO.Directory.CreateDirectory(AlamatSimpan)
        Else
            System.IO.Directory.Delete(AlamatSimpan, True)
            IO.Directory.CreateDirectory(AlamatSimpan)
        End If
    End Sub
    Private Sub fn_ViewMode(ByVal Status As Boolean)
        btnSaveNew.Enabled = Not Status
        btnSaveClose.Enabled = Not Status

        deDATE.Properties.ReadOnly = Status
        deDATE_DARI.Properties.ReadOnly = True
        deDATE_SAMPAI.Properties.ReadOnly = True
        txtNOMORSURAT.Properties.ReadOnly = True
        txtNAMAPENDERITA.Properties.ReadOnly = True
        txtUMUR.Properties.ReadOnly = Status
        txtJENISKELAMIN.Properties.ReadOnly = True
        txtPANGKAT.Properties.ReadOnly = True
        txtNRP.Properties.ReadOnly = True
        txtJABATAN.Properties.ReadOnly = Status
        txtKESATUAN.Properties.ReadOnly = True
        txtALAMATKANTOR.Properties.ReadOnly = Status
        txtALAMATKELUARGA.Properties.ReadOnly = Status
        txtBAGIAN.Properties.ReadOnly = Status
        txtRUANGAN.Properties.ReadOnly = True
        grdKDDOKUMEN.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtCODE.Text = "<---AUTO--->"
        txtNOMORSURAT.Text = "<---AUTO--->"
        deDATE.DateTime = Now
        txtKDKUNJUNGAN.ResetText()
        txtNAMAPENDERITA.ResetText()
        txtUMUR.ResetText()
        txtJENISKELAMIN.ResetText()
        txtPANGKAT.ResetText()
        txtNRP.ResetText()
        txtJABATAN.ResetText()
        txtKESATUAN.ResetText()
        txtALAMATKANTOR.ResetText()
        txtALAMATKELUARGA.ResetText()
        txtBAGIAN.ResetText()
        txtRUANGAN.ResetText()
        grdKDDOKUMEN.ResetText()

        txtKDKUNJUNGAN.Text = sKdkunjungan

        Dim oBeritaMasukPerawatan As New SuratInfokes.clsBeritaMasukPerawatan
        Dim dsBeritaMasukPerawatan = oBeritaMasukPerawatan.GetDataKunjunganPasien(txtKDKUNJUNGAN.Text)
        If dsBeritaMasukPerawatan IsNot Nothing Then
            txtALAMATKANTOR.Text = dsBeritaMasukPerawatan.ALAMATKANTOR
            txtALAMATKELUARGA.Text = dsBeritaMasukPerawatan.ALAMATKELUARGA
        End If

        Dim oUser As New Setting.clsUser
        grdKDDOKUMEN.Text = oUser.Daftar_Dokumen_Default()
    End Sub
    Private Sub fn_loadKunjungan(ByVal Parameter As String)
        Dim dsKunjungan = oSuratKeteranganPerawatan.GetDataKunjungan(Parameter)
        If dsKunjungan IsNot Nothing Then
            deDATE.DateTime = dsKunjungan.DATE
            txtKDKUNJUNGAN.Text = dsKunjungan.KDKUNJUNGAN
            txtNAMAPENDERITA.Text = dsKunjungan.NAMAPASIEN
            txtUMUR.Text = dsKunjungan.USIA
            txtJENISKELAMIN.Text = dsKunjungan.JENISKELAMIN
            txtPANGKAT.Text = dsKunjungan.PANGKAT
            txtNRP.Text = dsKunjungan.NRP
            txtKESATUAN.Text = dsKunjungan.KESATUAN
            txtRUANGAN.Text = dsKunjungan.TUJUAN
        End If
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oSuratKeteranganPerawatan.GetData(sNoId)

            With ds
                txtCODE.Text = .KDSURATKETERANGANPERAWATAN
                txtKDKUNJUNGAN.Text = .KDKUNJUNGAN
                txtNOMORSURAT.Text = .NOMORSURAT
                deDATE.DateTime = .DATE
                txtNAMAPENDERITA.Text = .NAMAPENDERITA
                txtUMUR.Text = .UMUR
                txtJENISKELAMIN.Text = .JENISKELAMIN
                txtPANGKAT.Text = .PANGKAT
                txtNRP.Text = .NRP
                txtJABATAN.Text = .JABATAN
                txtKESATUAN.Text = .KESATUAN
                txtALAMATKANTOR.Text = .ALAMATKANTOR
                txtALAMATKELUARGA.Text = .ALAMATKELUARGA
                txtBAGIAN.Text = .BAGIAN
                txtRUANGAN.Text = .RUANGAN
                grdKDDOKUMEN.Text = .KDUSER_KEPALA
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If txtKDKUNJUNGAN.Text = "" Then
                txtKDKUNJUNGAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDKUNJUNGAN.ErrorText = Statement.ErrorRequired

                txtKDKUNJUNGAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtNOMORSURAT.Text = "" Then
                txtNOMORSURAT.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtNOMORSURAT.ErrorText = Statement.ErrorRequired

                txtNOMORSURAT.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDOKUMEN.Text = "" Then
                grdKDDOKUMEN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDOKUMEN.ErrorText = Statement.ErrorRequired

                grdKDDOKUMEN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtJABATAN.Text = "" Then
                txtJABATAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtJABATAN.ErrorText = Statement.ErrorRequired

                txtJABATAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If deDATE_DARI.Text = "" Then
                deDATE_DARI.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                deDATE_DARI.ErrorText = Statement.ErrorRequired

                deDATE_DARI.Focus()
                fn_Validate = False
                Exit Function
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            MsgBox("Informasi : Mulai hari akan diberlakukan TTE BSSN mohon untuk menyiapkan Passphrase, klik OK untuk melanjutkan. ", MsgBoxStyle.Information, Me.Text)

            Dim ds = oSuratKeteranganPerawatan.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oSuratKeteranganPerawatan.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .TANGGALDARI = deDATE_DARI.DateTime
                .TANGGALSAMPAI = deDATE_SAMPAI.DateTime
                .KDSURATKETERANGANPERAWATAN = sNoId
                .KDKUNJUNGAN = txtKDKUNJUNGAN.Text
                .NOMORSURAT = txtNOMORSURAT.Text
                .NAMAPENDERITA = txtNAMAPENDERITA.Text
                .UMUR = txtUMUR.Text
                .JENISKELAMIN = txtJENISKELAMIN.Text
                .PANGKAT = txtPANGKAT.Text
                .NRP = txtNRP.Text
                .JABATAN = txtJABATAN.Text
                .KESATUAN = txtKESATUAN.Text
                .ALAMATKANTOR = txtALAMATKANTOR.Text
                .ALAMATKELUARGA = txtALAMATKELUARGA.Text
                .BAGIAN = txtBAGIAN.Text
                .RUANGAN = txtRUANGAN.Text
                Try
                    .MEMO = oSuratKeteranganPerawatan.GetData(sNoId).MEMO
                Catch ex As Exception
                    .MEMO = ""
                End Try
                .KDUSER = sUserID
                .KDUSER_KEPALA = grdKDDOKUMEN.EditValue
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    txtCODE.Text = oSuratKeteranganPerawatan.InsertData(ds)
                    If txtCODE.Text = "" Then
                        fn_Save = False
                    Else
                        fn_Save = True
                    End If
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oSuratKeteranganPerawatan.UpdateData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

            frmLoginACC.ShowDialog()

            If sCekUserAcc = True Then
                If fn_Save = True Then
                    Dim oDokumen As New Setting.clsUser
                    Dim dsDokumen = oDokumen.GetDataDokumen(grdKDDOKUMEN.EditValue)

                    If dsDokumen IsNot Nothing Then
                        sBase64TTe = ""
                        Dim oBrigging As New Brigging.clsSetKoneksi
                        Dim NamaDokumen As String = "SURAT KETERANGAN PERAWATAN"
                        Dim AlamatSimpan = "C:/SIMRS/EXPORT/"
                        Dim url_stage1 As String = String.Empty
                        Dim url_stage2 As String = String.Empty
                        Dim username_tte As String = String.Empty
                        Dim key_tte As String = String.Empty
                        Dim nip_tte As String = String.Empty
                        Dim title_tte As String = NamaDokumen & "_" & txtNAMAPENDERITA.Text
                        Dim norm_tte As String = ds.R_IDENTITAS_PASIEN.KDCUSTOMER
                        Dim kdpendaftaran_tte As String = ds.KDKUNJUNGAN
                        Dim filename_tte As String = AlamatSimpan & ds.DATE.ToString("ddMMyyyyHHmmss") & ds.KDKUNJUNGAN & ".pdf"
                        sTTEID = ""
                        sTTEPIN = ""
                        If Not IO.Directory.Exists(AlamatSimpan) Then
                            IO.Directory.CreateDirectory(AlamatSimpan)
                        Else
                            System.IO.Directory.Delete(AlamatSimpan, True)
                            IO.Directory.CreateDirectory(AlamatSimpan)
                        End If

                        Dim dsKoneksi = oBrigging.GetDataName_dpslay("TTE")
                        If dsKoneksi IsNot Nothing Then
                            url_stage1 = dsKoneksi.KONEKSI
                            url_stage2 = dsKoneksi.ALAMATWEB
                            username_tte = dsKoneksi.CONSID
                            key_tte = dsKoneksi.SECREATKEY
                            nip_tte = dsDokumen.NIK.ToString.Trim
                        End If

                        Dim DataDecrypt_Satge1 = JObject.Parse(oBrigging.fn_APITTERESUME_STAGE1(url_stage1, username_tte, key_tte, nip_tte, title_tte, filename_tte, norm_tte, kdpendaftaran_tte, NamaDokumen))

                        If DataDecrypt_Satge1.Item("ket").ToString() = "SUCCESS" Then


                            Dim rpt As New xtraSuratKeteranganPerawatan
                            sCODE_DOKUMEN = ds.KDUSER_KEPALA
                            sBase64TTe = DataDecrypt_Satge1.Item("barcode").ToString()
                            sTTEPIN = DataDecrypt_Satge1.Item("pin").ToString()
                            sTTEID = DataDecrypt_Satge1.Item("id").ToString()
                            rpt.bindingSource.DataSource = ds

                            rpt.ExportToPdf(filename_tte)

                            Dim DataDecrypt_Satge2 = JObject.Parse(oBrigging.fn_APITTERESUME_STAGE2(url_stage2, username_tte, key_tte, nip_tte, DataDecrypt_Satge1.Item("id").ToString(), filename_tte, sPassphrase, dsDokumen.NIK))

                            If DataDecrypt_Satge2.Item("ket").ToString() = "SUCCESS" Then
                                UpdatePinIdTTE(DataDecrypt_Satge1.Item("pin").ToString(), DataDecrypt_Satge1.Item("id").ToString(), ds.KDSURATKETERANGANPERAWATAN)
                                If oBrigging.fn_BarcodeTTE(ds.KDSURATKETERANGANPERAWATAN, DataDecrypt_Satge1.Item("barcode").ToString(), ".png") = True Then
                                    oSuratKeteranganPerawatan.UpdateMemo(ds.KDSURATKETERANGANPERAWATAN, DataDecrypt_Satge2.Item("url").ToString())
                                    sBarcodeTTE = ds.KDSURATKETERANGANPERAWATAN & ".png"
                                    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                                    printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
                                Else
                                    oSuratKeteranganPerawatan.UpdateMemo(ds.KDSURATKETERANGANPERAWATAN, DataDecrypt_Satge2.Item("url").ToString())
                                    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                                    printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)

                                    MsgBox("Gagal Save PNG Barcode!!", MsgBoxStyle.Exclamation, Me.Text)
                                End If
                            Else
                                MsgBox("Gagal TTE Stage 2: " & vbCrLf & DataDecrypt_Satge2.Item("ket").ToString(), MsgBoxStyle.Exclamation, Me.Text)
                                fn_Save = False
                            End If

                            sBase64TTe = ""
                        Else
                            MsgBox("Gagal TTE Stage 1: " & vbCrLf & DataDecrypt_Satge1.Item("ket").ToString(), MsgBoxStyle.Exclamation, Me.Text)
                        End If
                    Else
                        MsgBox("Kode Dokumen Tidak ditemukan!!!", MsgBoxStyle.Exclamation, Me.Text)
                    End If
                End If

                ''Tanda Tangan TTE
                'Dim sAalamatUrl As String = String.Empty

                'If fn_Save = True Then
                '    If grdKDDOKUMEN.Text <> "" Then
                '        Dim oBrigging As New Brigging.clsSetKoneksi
                '        Dim oDokumen As New Setting.clsUser
                '        Dim dsDokumen = oDokumen.GetDataDokumen(grdKDDOKUMEN.EditValue)
                '        Dim NamaDokumen As String = "Surat Keterangan Perawatan"
                '        sCODE_DOKUMEN = ""

                '        If dsDokumen IsNot Nothing Then
                '            sTTE = True
                '            Dim rpt As New xtraSuratKeteranganPerawatan
                '            rpt.bindingSource.DataSource = ds
                '            sCODE_DOKUMEN = ds.KDUSER_KEPALA
                '            rpt.ExportToPdf(AlamatSimpan & txtCODE.Text & ".pdf")

                '            Dim JsonRequest As String = String.Empty

                '            JsonRequest = " { "
                '            JsonRequest &= """username"": """ & "emedrek" & ""","
                '            JsonRequest &= """key"": """ & "emedrek2023tte" & ""","
                '            JsonRequest &= """nip"": """ & dsDokumen.IDENTITAS & ""","
                '            JsonRequest &= """seal"": """ & "true" & ""","
                '            JsonRequest &= """title"": """ & NamaDokumen & "_" & txtNAMAPENDERITA.Text & ""","
                '            JsonRequest &= """filename"": """ & AlamatSimpan & txtCODE.Text & ".pdf" & ""","
                '            JsonRequest &= """file"": """ & Convert.ToBase64String(System.IO.File.ReadAllBytes(AlamatSimpan & txtCODE.Text & ".pdf")) & """ "
                '            JsonRequest &= "}  "

                '            Dim DataDecrypt = JObject.Parse(oBrigging.fn_APITTE(JsonRequest, dsDokumen.ALAMAT_URL))
                '            If DataDecrypt.Item("ket").ToString() = "SUCCESS" Then
                '                sPesanTTE = "Tanda Tangan TTE Berhasil"
                '                oSuratKeteranganPerawatan.UpdateMemo(txtCODE.Text, DataDecrypt.Item("url").ToString())
                '                sAalamatUrl = DataDecrypt.Item("url").ToString()
                '                'Return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(DataDecrypt.Item("barcode").ToString()))
                '            Else
                '                sPesanTTE = "Tanda Tangan TTE Tidak Berhasil"
                '            End If
                '        Else
                '            sPesanTTE = "Tanda Tangan TTE Tidak Berhasil"
                '        End If
                '    Else
                '        sPesanTTE = "Tanda Tangan TTE Tidak Berhasil"
                '    End If
                'End If

                'If sAalamatUrl <> "" Then
                '    Dim AlamatSimpanPriview = "C:/SIMRS/KIRIMWA/"

                '    If Not IO.Directory.Exists(AlamatSimpanPriview) Then
                '        IO.Directory.CreateDirectory(AlamatSimpanPriview)
                '    Else
                '        System.IO.Directory.Delete(AlamatSimpanPriview, True)
                '        IO.Directory.CreateDirectory(AlamatSimpanPriview)
                '    End If

                '    My.Computer.Network.DownloadFile(sAalamatUrl, AlamatSimpanPriview & txtCODE.Text & ".pdf")

                '    'Dim oKoneksi As New Brigging.clsSetKoneksi

                '    'frmPopNomorWA.fn_LoadKDCUSTOMER(skdcustomer)
                '    'frmPopNomorWA.ShowDialog(Me)

                '    'If sNomorWA <> "" Then
                '    '    If oKoneksi.fn_SuratInfokes(txtKDKUNJUNGAN.Text, txtCODE.Text, sUserID, "SURAT KETERANGAN PERAWATAN", "SURAT_KETERANGAN_PERAWATAN" & "_" & skdcustomer, Now.ToString("yyyy-MM-dd"), AlamatSimpanPriview & txtCODE.Text & ".pdf", ".pdf", sNomorWA) = True Then
                '    '        MsgBox("Berhasil Kirim Wathsapp ke Nomor " & sNomorWA, MsgBoxStyle.Information, Me.Text)
                '    '    Else
                '    '        MsgBox("Gagal Kirim Wathsapp ke Nomor " & sNomorWA, MsgBoxStyle.Information, Me.Text)
                '    '    End If
                '    'End If
                'End If
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
#End Region
#Region "Grid Method"
#End Region
#Region "Command Button"
    Private Sub frmOrderTindakan_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClose_Click()
            Case Keys.F2
                If btnSaveNew.Enabled = True Then
                    btnSaveNew_Click()
                End If
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
        End Select
    End Sub
    Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox(Statement.SaveSuccess & vbCrLf & sPesanTTE, MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox(Statement.SaveSuccess & vbCrLf & sPesanTTE, MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
    Private Sub btnReloadDataPasien_Click(sender As Object, e As EventArgs)
        fn_loadKunjungan(txtKDKUNJUNGAN.Text)
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadKDOKUMEN()
        Dim oDokumen As New Setting.clsUser

        Try
            grdKDDOKUMEN.Properties.DataSource = oDokumen.GetDataDokumen.Where(Function(x) x.ISACTIVE = True And x.JABATAN = "Kaur Infokes").ToList()
            grdKDDOKUMEN.Properties.ValueMember = "KDDOKUMEN"
            grdKDDOKUMEN.Properties.DisplayMember = "NAMA"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Function UpdatePinIdTTE(ByVal sPINTTE As String, ByVal sIDTTE As String, ByVal sKDSURATKETERANGANPERAWATAN As String) As Boolean
        Try
            UpdatePinIdTTE = False

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\EMEDREK\", "Database", "").ToString())
            oConn = New SqlConnection(sConn)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "UPDATE "
            SQL &= "S_DIGITAL_SURATKETERANGANPERAWATAN "
            SQL &= "SET "
            SQL &= "PINTTE = '" & sPINTTE & "', "
            SQL &= "IDTTE = '" & sIDTTE & "' "
            SQL &= "WHERE "
            SQL &= " KDSURATKETERANGANPERAWATAN = '" & sKDSURATKETERANGANPERAWATAN & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "PINIDTTE")

            UpdatePinIdTTE = True

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            UpdatePinIdTTE = False
            MsgBox("Tidak ada kunjungan ruangan " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation)
        End Try
    End Function
#End Region
End Class