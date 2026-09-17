Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq
Imports MySql.Data.MySqlClient

Public Class frmSKD
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oSKD As New Admission.clsSKD
    Private PopUP As Boolean = False
    Private sKDPENDAFTARAN As String = String.Empty
    Private sSaveAuto As Boolean = False
    Private sAlasan As String = String.Empty
    Private sType As Integer = 0
    Private oPendaftaran As New Admission.clsPendaftaran

#End Region
#Region "Function"
    Public Sub fn_LoadNoPendaftaranPolidanDokter(ByVal Type As Integer, ByVal Parameter1 As String, ByVal Parameter2 As String, ByVal Parameter3 As String, ByVal KDPENDAFTARAN As String, ByVal alasan As String)
        sType = Type
        sPoli = String.Empty
        grdKDDEPARTMENT.Text = Parameter1
        grdKDDOCTOR.Text = Parameter2
        txtCARI.Text = Parameter3
        sKDPENDAFTARAN = KDPENDAFTARAN
        sAlasan = alasan
    End Sub
    Public Sub LoadMe(ByVal FormMode As Integer, Optional ByVal NoId As String = "")
        sNomorSKDPspri = ""
        sNomorSKDPspri_SEP = ""
        sNomorSEPKartu = ""
        sNomorSKDPspri_TanggalSKD = String.Empty
        sNomorSKDPspri_NamaDokter = String.Empty
        sNomorSKDPspri_Poli = String.Empty
        sCodeSKD = String.Empty
        oFormMode = FormMode
        sNoId = NoId
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
        If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            cboSKDSPRI.SelectedIndex = sType
        End If
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            lKDSKD.Text = SKD.KDSKD
            lKDPENDAFATRAN.Text = SKD.KDPENDAFTARAN & " *"
            lDATE.Text = SKD.TANGGAL
            lDATE_KONTROL.Text = SKD.TANGGAL_KONTROL
            lKDDEPARTMENT.Text = SKD.KDDEPARTMENT
            'lKDDOCTOR.Text = SKD.KDDOCTOR
            lNOMORRUJUKAN.Text = SKD.NOMORRUJUKAN
            'lDESCRIPTION.Text = SKD.DESCRIPTION
            'lALASAN.Text = SKD.ALASAN
            'lTINDAKLANJUT.Text = SKD.TINDAKLANJUT

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtCODE.Text.Trim.ToUpper
        sCodeSKD = txtCODE.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDDEPARTMENT()

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

    End Sub
    Private Sub fn_ViewMode(ByVal Status As Boolean)
        btnSaveNew.Enabled = Not Status
        btnSaveClose.Enabled = Not Status
        btnCari.Enabled = Not Status

        grdKDPENDAFTARAN.Properties.ReadOnly = Status
        deDATE.Properties.ReadOnly = True
        deDATEKONTROL.Properties.ReadOnly = Status
        grdKDDEPARTMENT.Properties.ReadOnly = Status
        grdKDDOCTOR.Properties.ReadOnly = Status
        txtNOMORRUJKAN.Properties.ReadOnly = Status
        txtDESCRIPTION.Properties.ReadOnly = Status
        'txtALASAN.Properties.ReadOnly = Status
        txtTINDAKLANJUT.Properties.ReadOnly = Status
        txtNomorSepaAtauKartu.Properties.ReadOnly = Status
        If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            cboSKDSPRI.Properties.ReadOnly = False
            txtCODE.Properties.ReadOnly = False
        Else
            txtCODE.Properties.ReadOnly = True
            cboSKDSPRI.Properties.ReadOnly = True
        End If

        lDATE.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

        If sAlasan = "" Then
            Me.Text = SKD.TITLE

            XtraTabPage2.PageVisible = True
            'txtALASAN.Properties.ReadOnly = Status
            cboSKDSPRI.Properties.ReadOnly = Status

            lDESCRIPTION.Text = SKD.DESCRIPTION
            'lALASAN.Text = SKD.ALASAN
            lTINDAKLANJUT.Text = SKD.TINDAKLANJUT
            lKDDOCTOR.Text = SKD.KDDOCTOR

            lcategory.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            Me.Text = SKD.TITLE
            XtraTabPage2.PageVisible = False
            cboSKDSPRI.Properties.ReadOnly = True
            lcategory.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lDESCRIPTION.Text = SKD.DESCRIPTION
            'lALASAN.Text = SKD.ALASAN
            lTINDAKLANJUT.Text = SKD.TINDAKLANJUT
            lKDDOCTOR.Text = SKD.KDDOCTOR

            'txtALASAN.Properties.ReadOnly = True

            If sAlasan = "KONTROL" Then
                Me.Text = "Rencana Kontrol Selanjutnya - Edit Form"
                lcategory.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lTINDAKLANJUT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lDESCRIPTION.Text = "Rencana Pemeriksaan Saat Kontrol Selanjutnya"
                lDESCRIPTION.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            ElseIf sAlasan = "KONSUL INTERNAL" Then
                Me.Text = "Konsul Internal - Edit Form"
                lDESCRIPTION.Text = "Alasan di Konsul"
                lKDDOCTOR.Text = "Dokter Yang di Tuju"
                lTINDAKLANJUT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            ElseIf sAlasan = "ALIH RAWAT" Then
                Me.Text = "Rujuk Internal Alih Rawat - Edit Form"
                lTINDAKLANJUT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lDESCRIPTION.Text = "Alasan di Rujuk"
                lKDDOCTOR.Text = "Dokter Yang di Tuju"
            ElseIf sAlasan = "RUJUKAN HABIS" Then
                Me.Text = "Rujukan Habis - Edit Form"
                lTINDAKLANJUT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lDESCRIPTION.Text = "Rencana Pemeriksaan Saat Kontrol Selanjutnya"
            ElseIf sAlasan = "PRB" Then
                lDATE_KONTROL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lDATE.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            ElseIf sAlasan = "RAWAT INAP"
                Me.Text = SKD.TITLE

                XtraTabPage2.PageVisible = True
                'txtALASAN.Properties.ReadOnly = Status
                cboSKDSPRI.Properties.ReadOnly = Status

                lDESCRIPTION.Text = SKD.DESCRIPTION
                'lALASAN.Text = SKD.ALASAN
                lTINDAKLANJUT.Text = SKD.TINDAKLANJUT
                lKDDOCTOR.Text = SKD.KDDOCTOR

                lcategory.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
        End If
    End Sub
    Private Sub fn_EmptyMe()
        txtCODE.Text = "<--- AUTO --->"

        grdKDPENDAFTARAN.ResetText()
        deDATE.DateTime = Now
        deDATEKONTROL.DateTime = Now
        txtDESCRIPTION.ResetText()
        txtTINDAKLANJUT.Text = "-"
        If sAlasan = "" Then
            sAlasan = "KONTROL"
        End If

        txtNomorSepaAtauKartu.ResetText()

        If sKDPENDAFTARAN <> "" Then
            cboCARI.SelectedIndex = 2
            fn_LoadKDPENDAFTARAN(sKDPENDAFTARAN)
            grdKDPENDAFTARAN.Text = sKDPENDAFTARAN

            Dim dsPendaftaran = oPendaftaran.GetData(grdKDPENDAFTARAN.EditValue)
            If dsPendaftaran IsNot Nothing Then
                deDATE.DateTime = dsPendaftaran.DATE
                txtNOMORRUJKAN.Text = dsPendaftaran.NOMORRUJUKAN
                grdKDDEPARTMENT.Text = dsPendaftaran.KDDEPARTMENT

                If cboSKDSPRI.SelectedIndex <= 0 Then
                    lDATE_KONTROL.Text = "Tgl. Kontrol"
                    lSEPKARTU.Text = "No. SEP "
                    txtNomorSepaAtauKartu.Text = dsPendaftaran.NOMORSEP
                Else
                    lDATE_KONTROL.Text = "Tgl. SPRI"
                    lSEPKARTU.Text = "No. Kartu "
                    txtNomorSepaAtauKartu.Text = dsPendaftaran.KARTUBPJS
                End If
            End If
        End If

        fn_LoadKDDOCTOR(grdKDDEPARTMENT.EditValue)


    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oSKD.GetDataOfline(sNoId)

            With ds
                txtCODE.Text = sNoId
                cboCARI.SelectedIndex = 2
                fn_LoadKDPENDAFTARAN(.KDPENDAFTARAN)
                grdKDPENDAFTARAN.Text = .KDPENDAFTARAN
                cboSKDSPRI.SelectedIndex = .ISCATEGORY
                txtDESCRIPTION.Text = .DESCRIPTION
                deDATE.DateTime = .DATE
                deDATEKONTROL.DateTime = .DATEKONTROL
                grdKDDEPARTMENT.Text = .KDDEPARTMENT
                fn_LoadKDDOCTOR(.KDDEPARTMENT)
                grdKDDOCTOR.Text = .KDDOCTOR
                txtNOMORRUJKAN.Text = .NOMORRUJUKAN
                sAlasan = .ALASAN
                If sAlasan = "" Then
                    sAlasan = "KONTROL"
                End If
                txtTINDAKLANJUT.Text = .TINDAKLANJUT
                txtNomorSepaAtauKartu.Text = .NOMORSEP
                cboSKDSPRI.SelectedIndex = IIf(.ISSKD = False, 0, 1)
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If cboSKDSPRI.Text = String.Empty Then
                cboSKDSPRI.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                cboSKDSPRI.ErrorText = Statement.ErrorRequired

                cboSKDSPRI.Focus()
                fn_Validate = False
                Exit Function
            End If
            If sAlasan = String.Empty Then
                MsgBox("Alasan Kosong", MsgBoxStyle.Exclamation, Me.Text)
                'txtALASAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                'txtALASAN.ErrorText = Statement.ErrorRequired

                'txtALASAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtTINDAKLANJUT.Text = String.Empty Then
                txtTINDAKLANJUT.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtTINDAKLANJUT.ErrorText = Statement.ErrorRequired

                txtTINDAKLANJUT.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDPENDAFTARAN.Text = String.Empty Then
                grdKDPENDAFTARAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDPENDAFTARAN.ErrorText = Statement.ErrorRequired

                grdKDPENDAFTARAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDEPARTMENT.Text = String.Empty Then
                grdKDDEPARTMENT.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDEPARTMENT.ErrorText = Statement.ErrorRequired

                grdKDDEPARTMENT.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDOCTOR.Text = String.Empty Then
                grdKDDOCTOR.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDOCTOR.ErrorText = Statement.ErrorRequired

                grdKDDOCTOR.Focus()
                fn_Validate = False
                Exit Function
            End If

            If cboSKDSPRI.SelectedIndex <= 0 Then
                If sAlasan = "KONSUL INTERNAL" Then
                    Dim dsPendaftran = oPendaftaran.GetData(grdKDPENDAFTARAN.Text)
                    If dsPendaftran IsNot Nothing Then
                        If dsPendaftran.KDDEPARTMENT = grdKDDEPARTMENT.EditValue Then
                            MsgBox("Poli Daftar Sama dengan yg di tuju", MsgBoxStyle.Exclamation, Me.Text)
                            grdKDDEPARTMENT.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                            grdKDDEPARTMENT.ErrorText = Statement.ErrorRequired
                            grdKDDEPARTMENT.Focus()
                            fn_Validate = False
                            Exit Function
                        End If
                    Else
                        MsgBox("Pendaftaran Kosong", MsgBoxStyle.Exclamation, Me.Text)
                        grdKDPENDAFTARAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                        grdKDPENDAFTARAN.ErrorText = Statement.ErrorRequired
                        grdKDPENDAFTARAN.Focus()
                        fn_Validate = False
                        Exit Function
                    End If
                Else
                    Dim dsPendaftran = oPendaftaran.GetData(grdKDPENDAFTARAN.Text)
                    If dsPendaftran IsNot Nothing Then
                        If dsPendaftran.DATE.ToString("yyyyMMdd") = deDATEKONTROL.DateTime.ToString("yyyyMMdd") Then
                            MsgBox("Tanggal Kontrol Sama dengan Tanggal Datang", MsgBoxStyle.Exclamation, Me.Text)
                            deDATEKONTROL.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                            deDATEKONTROL.ErrorText = Statement.ErrorRequired
                            deDATEKONTROL.Focus()
                            fn_Validate = False
                            Exit Function
                        End If
                    Else
                        MsgBox("Pendaftaran Kosong", MsgBoxStyle.Exclamation, Me.Text)
                        grdKDPENDAFTARAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                        grdKDPENDAFTARAN.ErrorText = Statement.ErrorRequired
                        grdKDPENDAFTARAN.Focus()
                        fn_Validate = False
                        Exit Function
                    End If
                End If
            End If

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                If sAlasan = "KONSUL INTERNAL" Then
                    Dim oDoctor As New Reference.clsDoctor

                    Dim sHARI As String = String.Empty

                    Select Case Weekday(deDATEKONTROL.DateTime)
                        Case 1
                            sHARI = "Minggu"
                        Case 2
                            sHARI = "Senin"
                        Case 3
                            sHARI = "Selasa"
                        Case 4
                            sHARI = "Rabu"
                        Case 5
                            sHARI = "Kamis"
                        Case 6
                            sHARI = "Jumat"
                        Case 7
                            sHARI = "Sabtu"
                    End Select

                    Dim dsJawdwalDokter = oDoctor.GetDataDetailJadwalDokter(grdKDDOCTOR.EditValue, sHARI)

                    If dsJawdwalDokter Is Nothing Then
                        MsgBox("Jadwal Dokter belum ada Untuk Dokter " & grdKDDOCTOR.Text & " di Tanggal Kontrol " & deDATEKONTROL.DateTime.ToString("dd-MM-yyyy") & " Silahkan Cari Tanggal Kontrol Lain", MsgBoxStyle.Exclamation, Me.Text)

                        deDATEKONTROL.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                        deDATEKONTROL.ErrorText = Statement.ErrorRequired
                        deDATEKONTROL.Focus()
                        fn_Validate = False
                        Exit Function
                    End If
                End If
            End If

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                If sAlasan = "KONTROL" Then
                    Dim dsDaftar = oPendaftaran.GetData(grdKDPENDAFTARAN.EditValue)

                    If dsDaftar IsNot Nothing Then
                        If cboSKDSPRI.Text = "SKD (Surat Kontrol Dokter)" Then
                            If dsDaftar.M_DAFTAR_L1.MEMO.Contains("BPJS") Then
                                If dsDaftar.NOMORSEP = "" Then
                                    MsgBox("Nomor SEP Kosong Penjammin BPJS", MsgBoxStyle.Exclamation, Me.Text)

                                    grdKDPENDAFTARAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                                    grdKDPENDAFTARAN.ErrorText = Statement.ErrorRequired
                                    grdKDPENDAFTARAN.Focus()
                                    fn_Validate = False
                                    Exit Function
                                End If
                            End If
                        End If
                    Else
                        MsgBox("Register Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)

                        grdKDPENDAFTARAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                        grdKDPENDAFTARAN.ErrorText = Statement.ErrorRequired
                        grdKDPENDAFTARAN.Focus()
                        fn_Validate = False
                        Exit Function
                    End If
                End If
            End If
            'If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            '    If sPenjaminBPJS = True Then
            '        If txtNomorSepaAtauKartu.Text = String.Empty Then
            '            MsgBox("Nomor SEP / Kartu Kosong", MsgBoxStyle.Exclamation, Me.Text)
            '            txtNomorSepaAtauKartu.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '            txtNomorSepaAtauKartu.ErrorText = Statement.ErrorRequired
            '            txtNomorSepaAtauKartu.Focus()
            '            fn_Validate = False
            '            Exit Function
            '        End If
            '    End If
            'End If


            'Dim ds = oSKD.GetDataPendaftaran(grdKDPENDAFTARAN.EditValue)
            'If ds IsNot Nothing Then
            '    If Not ds.KDSKD.Contains("Auto") Then
            '        grdKDPENDAFTARAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '        grdKDPENDAFTARAN.ErrorText = Statement.ErrorRequired
            '        MsgBox("Sudah dibuatkan SKD Silahkan, edit kembali", MsgBoxStyle.Exclamation, Me.Text)
            '        txtNomorSepaAtauKartu.Focus()
            '        fn_Validate = False
            '        Exit Function
            '    End If
            'End If

            '
            'sNomorSKDPspri_TanggalSKD = grv.GetFocusedRowCellValue("tglRencanaKontrol")
            'sNomorSKDPspri_NamaDokter = grv.GetFocusedRowCellValue("kodeDokter")
            'sNomorSKDPspri_Poli = grv.GetFocusedRowCellValue("poliTujuan")

            If sNomorSKDPspri <> "" Then
                If txtCODE.Text <> "<--- AUTO --->" Then
                    If txtCODE.Text <> sNomorSKDPspri Then
                        MsgBox("Nomor Surat Kotnrol Tidak Sama dengan yg dipilih", MsgBoxStyle.Exclamation, Me.Text)
                        txtCODE.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                        txtCODE.ErrorText = Statement.ErrorRequired
                        txtCODE.Focus()
                        fn_Validate = False
                        Exit Function
                    End If
                    If deDATEKONTROL.DateTime.ToString("yyyy-MM-dd") <> sNomorSKDPspri_TanggalSKD Then
                        MsgBox("Tanggal Kontrol yg ada di BPJS adalah " & sNomorSKDPspri_TanggalSKD & " Silahkan Pilih tanggal yg seusai dengan bpjs terlebih dahulu", MsgBoxStyle.Exclamation, Me.Text)
                        deDATEKONTROL.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                        deDATEKONTROL.ErrorText = Statement.ErrorRequired
                        deDATEKONTROL.Focus()
                        fn_Validate = False
                        Exit Function
                    End If
                    If txtNomorSepaAtauKartu.Text <> sNomorSKDPspri_SEP Then
                        MsgBox("Nomor SEP yg dipilih " & sNomorSKDPspri_SEP & " Silahkan Sesuaikan dengan Nomor SEP tersebut", MsgBoxStyle.Exclamation, Me.Text)
                        txtNomorSepaAtauKartu.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                        txtNomorSepaAtauKartu.ErrorText = Statement.ErrorRequired
                        txtNomorSepaAtauKartu.Focus()
                        fn_Validate = False
                        Exit Function
                    End If

                    Dim oDoctor As New Reference.clsDoctor
                    Dim dsDoctor = oDoctor.GetData(grdKDDOCTOR.EditValue)

                    If dsDoctor IsNot Nothing Then
                        If dsDoctor.VCLAIM_KDDPJP <> sNomorSKDPspri_NamaDokter Then
                            MsgBox("Kode Dokter yg dipilih tidak sama dengan yg ada di BPJS " & sNomorSKDPspri_NamaDokter & " Silahkan Pilih Dkter yg seusai dengan bpjs terlebih dahulu", MsgBoxStyle.Exclamation, Me.Text)
                            grdKDDOCTOR.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                            grdKDDOCTOR.ErrorText = Statement.ErrorRequired
                            grdKDDOCTOR.Focus()
                            fn_Validate = False
                            Exit Function
                        End If
                    Else
                        grdKDDOCTOR.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                        grdKDDOCTOR.ErrorText = Statement.ErrorRequired

                        grdKDDOCTOR.Focus()
                        fn_Validate = False
                        Exit Function
                    End If

                    Dim oDepartment As New Reference.clsDepartment
                    Dim dsDepartment = oDepartment.GetData(grdKDDEPARTMENT.EditValue)

                    If dsDepartment IsNot Nothing Then
                        If dsDepartment.VCLAIM_KODEPOLI <> sNomorSKDPspri_Poli Then
                            MsgBox("Kode Poli yg dituju tidak sama dengan yg ada di BPJS " & sNomorSKDPspri_Poli & " Silahkan Pilih Poli yg seusai dengan bpjs terlebih dahulu", MsgBoxStyle.Exclamation, Me.Text)
                            grdKDDEPARTMENT.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                            grdKDDEPARTMENT.ErrorText = Statement.ErrorRequired
                            grdKDDEPARTMENT.Focus()
                            fn_Validate = False
                            Exit Function
                        End If
                    Else
                        grdKDDEPARTMENT.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                        grdKDDEPARTMENT.ErrorText = Statement.ErrorRequired

                        grdKDDEPARTMENT.Focus()
                        fn_Validate = False
                        Exit Function
                    End If

                    Dim dsSKD = oPendaftaran.GetDataBySuratKontrol(txtCODE.Text)
                    If dsSKD IsNot Nothing Then
                        MsgBox("Nomor SKD Sudah Terinput di registriasi " & dsSKD.KDPENDAFTARAN & " Atas Nama " & dsSKD.M_CUSTOMER.NAME_DISPLAY, MsgBoxStyle.Exclamation, Me.Text)
                        txtCODE.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                        txtCODE.ErrorText = Statement.ErrorRequired
                        txtCODE.Focus()
                        fn_Validate = False
                        Exit Function
                    End If
                End If
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_RequestInsertRencanaKontrol(ByVal sNoSEP As String) As String
        Try
            Dim oDoctor As New Reference.clsDoctor
            Dim jsonRequest As String = String.Empty
            Dim oDepartment As New Reference.clsDepartment

            jsonRequest = "{ "
            jsonRequest &= " ""request"" :   { "
            jsonRequest &= " ""noSEP"" :  """ & sNoSEP & """ , "
            jsonRequest &= " ""kodeDokter"" :  """ & oDoctor.GetData(grdKDDOCTOR.EditValue).VCLAIM_KDDPJP & """ , "
            jsonRequest &= " ""poliKontrol"" :  """ & oDepartment.GetData(grdKDDEPARTMENT.EditValue).VCLAIM_KODEPOLI & """ , "
            jsonRequest &= " ""tglRencanaKontrol"" :  """ & deDATEKONTROL.DateTime.ToString("yyyy-MM-dd") & """ , "
            jsonRequest &= " ""user"" :  """ & sUserID & """ "
            jsonRequest &= " } "
            jsonRequest &= " } "

            fn_RequestInsertRencanaKontrol = jsonRequest

        Catch oErr As Exception
            fn_RequestInsertRencanaKontrol = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_InsertRencanaKontrol(ByVal jsonRequest As String, ByVal uTime As Integer) As String
        Try
            Dim oSetKoneksi As New Brigging.clsSetKoneksi

            If sVclaim_ConsId <> "" Then
                Dim dsSetKoneksi = oSetKoneksi.InsertRencanaKontrol(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, jsonRequest)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        fn_InsertRencanaKontrol = allData("response")
                    Else
                        fn_InsertRencanaKontrol = ""
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Critical, Me.Text)
                        Exit Function
                    End If
                Else
                    fn_InsertRencanaKontrol = ""
                    MsgBox("Insert Rencana Kontrol Gagal", MsgBoxStyle.Critical, Me.Text)
                End If
            Else
                fn_InsertRencanaKontrol = ""
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Critical, Me.Text)
            End If
        Catch oErr As Exception
            fn_InsertRencanaKontrol = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_RequestUpdateRencanaKontrol(ByVal Kode As String, ByVal sNoSEP As String) As String
        Try
            Dim oDoctor As New Reference.clsDoctor
            Dim jsonRequest As String = String.Empty
            Dim oDepartment As New Reference.clsDepartment

            jsonRequest = "{ "
            jsonRequest &= " ""request"" :   { "
            jsonRequest &= " ""noSuratKontrol"" :  """ & Kode & """ , "
            jsonRequest &= " ""noSEP"" :  """ & sNoSEP & """ , "
            jsonRequest &= " ""kodeDokter"" :  """ & oDoctor.GetData(grdKDDOCTOR.EditValue).VCLAIM_KDDPJP & """ , "
            jsonRequest &= " ""poliKontrol"" :  """ & oDepartment.GetData(grdKDDEPARTMENT.EditValue).VCLAIM_KODEPOLI & """ , "
            jsonRequest &= " ""tglRencanaKontrol"" :  """ & deDATEKONTROL.DateTime.ToString("yyyy-MM-dd") & """ , "
            jsonRequest &= " ""user"" :  """ & sUserID & """ "
            jsonRequest &= " } "
            jsonRequest &= " } "

            fn_RequestUpdateRencanaKontrol = jsonRequest

        Catch oErr As Exception
            fn_RequestUpdateRencanaKontrol = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_UpdateRencanaKontrol(ByVal jsonRequest As String, ByVal uTime As Integer) As String
        Try
            Dim oSetKoneksi As New Brigging.clsSetKoneksi

            If sVclaim_ConsId <> "" Then
                Dim dsSetKoneksi = oSetKoneksi.UpdateRencanaKontrol(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, jsonRequest)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        fn_UpdateRencanaKontrol = allData("response")
                    Else
                        fn_UpdateRencanaKontrol = ""
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Critical, Me.Text)
                    End If
                Else
                    fn_UpdateRencanaKontrol = ""
                    MsgBox("Update Rencana Kontrol Gagal", MsgBoxStyle.Critical, Me.Text)
                End If
            Else
                fn_UpdateRencanaKontrol = ""
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Critical, Me.Text)
            End If
        Catch oErr As Exception
            fn_UpdateRencanaKontrol = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_RequestInsertSPRI(ByVal sKartu As String) As String
        Try
            Dim oDoctor As New Reference.clsDoctor
            Dim jsonRequest As String = String.Empty
            Dim oDepartment As New Reference.clsDepartment

            jsonRequest = "{ "
            jsonRequest &= " ""request"" :   { "
            jsonRequest &= " ""noKartu"" :  """ & sKartu & """ , "
            jsonRequest &= " ""kodeDokter"" :  """ & oDoctor.GetData(grdKDDOCTOR.EditValue).VCLAIM_KDDPJP & """ , "
            jsonRequest &= " ""poliKontrol"" :  """ & oDepartment.GetData(grdKDDEPARTMENT.EditValue).VCLAIM_KODEPOLI & """ , "
            jsonRequest &= " ""tglRencanaKontrol"" :  """ & deDATEKONTROL.DateTime.ToString("yyyy-MM-dd") & """ , "
            jsonRequest &= " ""user"" :  """ & sUserID & """ "
            jsonRequest &= " } "
            jsonRequest &= " } "

            fn_RequestInsertSPRI = jsonRequest

        Catch oErr As Exception
            fn_RequestInsertSPRI = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_InsertSPRI(ByVal jsonRequest As String, ByVal uTime As Integer) As String
        Try
            Dim oSetKoneksi As New Brigging.clsSetKoneksi

            If sVclaim_ConsId <> "" Then
                Dim dsSetKoneksi = oSetKoneksi.InsertSPRI(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, jsonRequest)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        fn_InsertSPRI = allData("response")
                    Else
                        fn_InsertSPRI = ""
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                        Exit Function
                    End If
                Else
                    fn_InsertSPRI = ""
                    MsgBox("Insert Rencana Kontrol Gagal", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_InsertSPRI = ""
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            fn_InsertSPRI = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_RequestUpdateSPRI(ByVal Kode As String) As String
        Try
            Dim oDoctor As New Reference.clsDoctor
            Dim jsonRequest As String = String.Empty
            Dim oDepartment As New Reference.clsDepartment

            jsonRequest = "{ "
            jsonRequest &= " ""request"" :   { "
            jsonRequest &= " ""noSPRI"" :  """ & Kode & """ , "
            jsonRequest &= " ""kodeDokter"" :  """ & oDoctor.GetData(grdKDDOCTOR.EditValue).VCLAIM_KDDPJP & """ , "
            jsonRequest &= " ""poliKontrol"" :  """ & oDepartment.GetData(grdKDDEPARTMENT.EditValue).VCLAIM_KODEPOLI & """ , "
            jsonRequest &= " ""tglRencanaKontrol"" :  """ & deDATEKONTROL.DateTime.ToString("yyyy-MM-dd") & """ , "
            jsonRequest &= " ""user"" :  """ & sUserID & """ "
            jsonRequest &= " } "
            jsonRequest &= " } "

            fn_RequestUpdateSPRI = jsonRequest

        Catch oErr As Exception
            fn_RequestUpdateSPRI = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_UpdateSPRI(ByVal jsonRequest As String, ByVal uTime As Integer) As Boolean
        Try
            Dim oSetKoneksi As New Brigging.clsSetKoneksi

            If sVclaim_ConsId <> "" Then
                Dim dsSetKoneksi = oSetKoneksi.UpdateSPRI(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, jsonRequest)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        fn_UpdateSPRI = allData("response")
                    Else
                        fn_UpdateSPRI = ""
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    fn_UpdateSPRI = ""
                    MsgBox("Insert Rencana Kontrol Gagal", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_UpdateSPRI = ""
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            fn_UpdateSPRI = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ******** Insert BPJS
            Dim jsonRequest As String = String.Empty
            Dim jsonResponse As String = String.Empty
            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim sPenjaminBPJS As Boolean = False
            Dim dsPendaftaran = oPendaftaran.GetData(grdKDPENDAFTARAN.EditValue)
            If dsPendaftaran IsNot Nothing Then
                sPenjaminBPJS = IIf(dsPendaftaran.M_DAFTAR_L1.MEMO.ToString.Contains("BPJS"), True, False)
            Else
                MsgBox("Silahkan Pilih Registrasi Terlebih dahulu", MsgBoxStyle.Exclamation, Me.Text)
                fn_Save = False
                Exit Function
            End If

            If sAlasan <> "" Then
                If sAlasan <> "KONTROL" Then
                    sPenjaminBPJS = False
                End If
            End If

            If sPenjaminBPJS = True Then
                If sSaveAuto = False Then
                    Try
                        If cboSKDSPRI.SelectedIndex = 0 Then
                            Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                            If txtCODE.Text = "<--- AUTO --->" Then
                                jsonRequest = fn_RequestInsertRencanaKontrol(txtNomorSepaAtauKartu.Text)

                                If jsonRequest <> "" Then
                                    jsonResponse = fn_InsertRencanaKontrol(jsonRequest, uTime)
                                    If jsonResponse <> "" Then
                                        Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(jsonResponse, sVclaim_ConsId & sVclaim_SecreatKey & uTime))
                                        txtCODE.Text = DataDecrypt.Item("noSuratKontrol").ToString()
                                    Else
                                        fn_Save = False
                                        Exit Function
                                    End If
                                Else
                                    fn_Save = False
                                    Exit Function
                                End If
                            Else
                                jsonRequest = fn_RequestUpdateRencanaKontrol(txtCODE.Text, txtNomorSepaAtauKartu.Text)
                                If jsonRequest <> "" Then
                                    jsonResponse = fn_UpdateRencanaKontrol(jsonRequest, uTime)
                                    If jsonResponse <> "" Then
                                        Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(jsonResponse, sVclaim_ConsId & sVclaim_SecreatKey & uTime))

                                        MsgBox(DataDecrypt.Item("noSuratKontrol").ToString(), MsgBoxStyle.Information, Me.Text)
                                    Else
                                        fn_Save = False
                                        Exit Function
                                    End If
                                Else
                                    fn_Save = False
                                    Exit Function
                                End If
                            End If
                        Else
                            Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                            If txtCODE.Text = "<--- AUTO --->" Then
                                jsonRequest = fn_RequestInsertSPRI(txtNomorSepaAtauKartu.Text)

                                If jsonRequest <> "" Then
                                    jsonResponse = fn_InsertSPRI(jsonRequest, uTime)
                                    If jsonResponse <> "" Then
                                        Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(jsonResponse, sVclaim_ConsId & sVclaim_SecreatKey & uTime))
                                        txtCODE.Text = DataDecrypt.Item("noSPRI").ToString()
                                    Else
                                        fn_Save = False
                                        Exit Function
                                    End If
                                Else
                                    fn_Save = False
                                    Exit Function
                                End If
                            Else
                                jsonRequest = fn_RequestUpdateSPRI(txtCODE.Text)
                                If jsonRequest <> "" Then
                                    jsonResponse = fn_UpdateSPRI(jsonRequest, uTime)
                                    If jsonResponse <> "" Then
                                        Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(jsonResponse, sVclaim_ConsId & sVclaim_SecreatKey & uTime))

                                        MsgBox(DataDecrypt.Item("noSPRI").ToString(), MsgBoxStyle.Information, Me.Text)

                                    Else
                                        fn_Save = False
                                        Exit Function
                                    End If
                                Else
                                    fn_Save = False
                                    Exit Function
                                End If
                            End If
                        End If
                    Catch oErr As Exception
                        MsgBox("Brigging SKD Ke BPJS" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                        fn_Save = False
                        Exit Function
                    End Try
                Else
                    sSaveAuto = False
                End If
            Else
                If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                    txtCODE.ResetText()
                End If
            End If

            ' ***** HEADER *****
            Dim ds = oSKD.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oSKD.GetData(txtCODE.Text).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try

                .DATEUPDATED = Now
                .KDSKD = txtCODE.Text
                .KDPENDAFTARAN = grdKDPENDAFTARAN.EditValue
                .DATE = deDATE.DateTime
                .ISCATEGORY = cboSKDSPRI.SelectedIndex
                .KDDEPARTMENT = grdKDDEPARTMENT.EditValue
                .KDDOCTOR = grdKDDOCTOR.EditValue
                .NOMORRUJUKAN = txtNOMORRUJKAN.Text.Trim
                .DESCRIPTION = txtDESCRIPTION.Text.Trim
                Try
                    .ISCHEKED = oSKD.GetData(txtCODE.Text).ISCHEKED
                Catch oErr As Exception
                    .ISCHEKED = False
                End Try
                .KDUSER = sUserID
                .DATEKONTROL = deDATEKONTROL.DateTime
                .ALASAN = sAlasan
                .TINDAKLANJUT = txtTINDAKLANJUT.Text.Trim
                .TANGGALPERIKSA_TEXT = deDATEKONTROL.DateTime.ToString("ddMMyyyy")
                .KDJADWALDOKTER = ""
                .SEQ = 0
                .REQUEST = jsonRequest
                .RESPONSE = jsonResponse
                .NOMORSEP = txtNomorSepaAtauKartu.Text
                If txtCODE.Text <> "" Then
                    Try
                        .ISONLINE = oSKD.GetData(txtCODE.Text).ISONLINE
                    Catch oErr As Exception
                        .ISONLINE = True
                    End Try
                Else
                    Try
                        .ISONLINE = oSKD.GetData(txtCODE.Text).ISONLINE
                    Catch oErr As Exception
                        .ISONLINE = False
                    End Try
                End If
                .ISSKD = IIf(cboSKDSPRI.SelectedIndex = 0, False, True)
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    Dim KDSKD As String = oSKD.InsertData(ds, txtCODE.Text)

                    If KDSKD <> "" Then
                        txtCODE.Text = KDSKD
                        fn_Save = True
                    Else
                        fn_Save = False
                    End If
                Catch oErr As Exception
                    fn_Save = False
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oSKD.UpdateData(ds)
                Catch oErr As Exception
                    fn_Save = False
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

            If sAlasan = "" Then
                If fn_Save = True Then
                    Try
                        If txtCODE.Text = String.Empty Then Exit Function

                        If Not txtCODE.Text.Contains("SKD") Then
                            Dim rpt As New xtraRencanaKontrol

                            rpt.ShowPrintMarginsWarning = False
                            rpt.Watermark.Text = sWATERMARK

                            rpt.bindingSource.DataSource = ds
                            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                            printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
                        End If
                    Catch oErr As Exception
                        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                End If
            End If

            sPoli = grdKDDEPARTMENT.Text

            'Try
            '    If txtCODE.Text <> "" Then
            '        fn_SaveOnline(txtCODE.Text)
            '    End If
            'Catch ex As Exception

            'End Try

            If fn_Save = True Then
                If sAlasan = "KONSUL INTERNAL" Then
                    fn_SaveKunjungan()
                End If
            End If
        Catch oErr As Exception
            fn_Save = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_SaveKunjungan() As Boolean
        Try
            Dim oPendaftaran_Kunjungan As New Admission.clsPendaftaran_Kunjungan

            Dim dsKunjunganCek = oPendaftaran_Kunjungan.GetDatabyRegisterdanDeparment(grdKDPENDAFTARAN.EditValue, grdKDDEPARTMENT.EditValue)
            ' ***** HEADER *****
            Dim ds = oPendaftaran_Kunjungan.GetStructureHeader
            With ds
                Try
                    .DATECREATED = dsKunjunganCek.DATECREATED
                    .KDKUNJUNGAN = dsKunjunganCek.KDKUNJUNGAN
                Catch oErr As Exception
                    .DATECREATED = Now
                    .KDKUNJUNGAN = ""
                End Try
                .DATEUPDATED = Now
                .KDPENDAFTARAN = grdKDPENDAFTARAN.EditValue
                .DATE = deDATEKONTROL.DateTime
                .KDDOCTOR = grdKDDOCTOR.EditValue
                .KDDEPARTMENT = grdKDDEPARTMENT.EditValue
                Dim dsDaftar = oPendaftaran_Kunjungan.GetDatabykd(grdKDPENDAFTARAN.EditValue)
                .ALAMAT = dsDaftar.ALAMAT
                .KDPENJAMIN = dsDaftar.KDPENJAMIN
                .KDKESATUAN = dsDaftar.KDKESATUAN
                .KDPANGKAT = dsDaftar.KDPANGKAT
                .KDGOLONGAN = dsDaftar.KDGOLONGAN
                .KDPENDIDIKAN = dsDaftar.KDPENDIDIKAN
                .KDPEKERJAAN = dsDaftar.KDPEKERJAAN
                .KDPERUSAHAAN = dsDaftar.KDPERUSAHAAN
                .KDSTATUSKAWIN = dsDaftar.KDSTATUSKAWIN
                .NAMAKELUARGA = dsDaftar.NAMAKELUARGA
                .KDSTATUSKELUARGA = dsDaftar.KDSTATUSKELUARGA
                .TERSEDIA = 0
                .KDUPDATE_APLICARE = ""
                .KDUSER = sUserID
            End With


            If dsKunjunganCek Is Nothing Then
                fn_SaveKunjungan = oPendaftaran_Kunjungan.InsertData(ds)
            Else
                fn_SaveKunjungan = oPendaftaran_Kunjungan.UpdateData(ds)
            End If

            If fn_SaveKunjungan = True Then
                Try
                    Dim dsKunjungan = oPendaftaran.GetDataKunjungan(ds.KDKUNJUNGAN)

                    Dim dsIdentitas = oPendaftaran.GetStructureHeader_Identitas

                    With dsIdentitas
                        .DATECREATED = dsKunjungan.DATECREATED
                        .DATEUPDATED = dsKunjungan.DATEUPDATED
                        .DATE = dsKunjungan.DATE
                        .CATEGORY = dsKunjungan.S_PENDAFTARAN_H.CATEGORY
                        .KDKUNJUNGAN = dsKunjungan.KDKUNJUNGAN
                        .KDPENDAFTARAN = dsKunjungan.KDPENDAFTARAN
                        .PENJAMIN = dsKunjungan.S_PENDAFTARAN_H.M_DAFTAR_L1.MEMO
                        .KDCUSTOMER = dsKunjungan.S_PENDAFTARAN_H.KDCUSTOMER
                        .NAMAPASIEN = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY
                        .ALAMAT = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.ALAMAT
                        .DOKTER = grdKDDOCTOR.Text
                        .TUJUAN = grdKDDEPARTMENT.Text
                        .KDDOKTER = grdKDDOCTOR.EditValue
                        .KDTUJUAN = grdKDDEPARTMENT.EditValue
                        .TANGGALLAHIR = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR
                        .JENISKELAMIN = IIf(dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.KDJENISKELAMIN = 0, "P", "L")
                        .NIK = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.KTP
                        .TEMPATLAHIR = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.TEMPATLAHIR
                        .AGAMA = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.M_AGAMA.MEMO
                        .PANGKAT = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.M_PANGKAT.MEMO
                        .NRP = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.NRP
                        .KESATUAN = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.M_KESATUAN.MEMO
                        .NOMORTELEPON = dsKunjungan.S_PENDAFTARAN_H.NOMORTELEPON
                        .PENDIDIKAN = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.KDPENDIDIKAN
                        .SUKU = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.M_SUKU.MEMO
                        .USIA = oPendaftaran.GetUmurPasien(deDATE.DateTime, dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)
                        .NOMORSEP = dsKunjungan.S_PENDAFTARAN_H.NOMORSEP
                        .KELASPELAYANAN = dsKunjungan.S_PENDAFTARAN_H.KDKELASRAWAT
                        .KARTUBPJS = dsKunjungan.S_PENDAFTARAN_H.KARTUBPJS
                        .STATUS_KAWIN = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.KDSTATUSKAWIN
                        .HUBUNGAN = ""
                        .HUBUNGAN_NAMA = ""
                        .HUBUNGAN_PENDIDIKAN = ""
                        .HUBUNGAN_PEKERJAAN = ""
                        .NOMORASURANSILAIN = ""
                        .KDGOLONGANDARAH = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.KDGOLONGANDARAH
                        .KDDIAGNOSA = dsKunjungan.S_PENDAFTARAN_H.KDDIAGNOSA
                        .KDPENJAMIN = ""
                        .KDPERUSAHAAN = ""
                        .DIAGNOSA = dsKunjungan.S_PENDAFTARAN_H.M_DIAGNOSA.MEMO
                        .KDUSER = sUserID
                        .HAKKELAS = ""
                        .URL_SIGNATURE = ""
                        .NAMA_TANDATANGAN = ""
                        .FASKES = dsKunjungan.S_PENDAFTARAN_H.M_PPK.MEMO
                    End With

                    Dim dsIdentitasCek = oPendaftaran.GetDataIdentitas(ds.KDKUNJUNGAN)
                    If dsIdentitasCek Is Nothing Then
                        oPendaftaran.InsertDataR_identitas(dsIdentitas)
                    Else
                        oPendaftaran.UpdateDataR_Identitas(dsIdentitas)
                    End If
                Catch ex As Exception
                    MsgBox("R_Identitas Eror" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveKunjungan = False
        End Try
    End Function
#End Region
#Region "Command Button"
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClose_Click()
            'Case Keys.F2
            '    If btnSaveNew.Enabled = True Then
            '        btnSaveNew_Click()
            '    End If
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
            Case Keys.F10
                If btnCari.Enabled = True Then
                    btnCari_Click()
                End If
        End Select
    End Sub
    Private Sub btnCari_Click() Handles btnCari.ItemClick
        Try
            sSaveAuto = False

            frmReportRencaKontrol.ShowDialog(Me)

            If sNomorSKDPspri <> "" Then
                If XtraTabControl1.SelectedTabPageIndex = 0 Then
                    If txtNomorSepaAtauKartu.Text = "" Then
                        MsgBox("Nomor SEP Kosong, Silahkan Kordinasikan dengan Bagian Admisi", MsgBoxStyle.Exclamation, Me.Text)
                    Else
                        If txtNomorSepaAtauKartu.Text <> sNomorSKDPspri_SEP Then
                            MsgBox("Nomor Surat Kontrol yang dipilih tidak sama dengan Nomor Registrasi yg sudah tersimpan, Silahkan Pilih Surat Kontrol Sesuai Nomor SEP yg sudah teregistrasi dengan Benar", MsgBoxStyle.Exclamation, Me.Text)
                        Else
                            sSaveAuto = True

                            txtCODE.Text = sNomorSKDPspri
                            txtNomorSepaAtauKartu.Text = sNomorSKDPspri_SEP
                            deDATEKONTROL.DateTime = CDate(sNomorSKDPspri_TanggalSKD)
                            Dim oDepartment As New Reference.clsDepartment
                            Dim oDoctor As New Reference.clsDoctor

                            Dim dsDepartment = oDepartment.GetDatakodebpjs(sNomorSKDPspri_Poli)
                            If dsDepartment IsNot Nothing Then
                                grdKDDEPARTMENT.Text = dsDepartment.KDDEPARTMENT
                                fn_LoadKDDOCTOR(grdKDDEPARTMENT.EditValue)
                                Dim dsDoctor = oDoctor.GetDataByKodeVclaim(sNomorSKDPspri_NamaDokter)
                                If dsDoctor IsNot Nothing Then
                                    grdKDDOCTOR.Text = dsDoctor.KDDOCTOR
                                End If
                            End If
                        End If
                    End If
                Else
                    txtKode2.Text = sNomorSKDPspri
                    txtNoSEP2.Text = sNomorSKDPspri_SEP
                    txtKartu2.Text = sNomorSEPKartu
                End If
            End If
        Catch oErr As Exception
            MsgBox("Pencarian SKD" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    'Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
    '    If fn_Validate() = False Then Exit Sub
    '    If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
    '    If fn_Save() = False Then
    '        MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
    '    Else
    '        MsgBox("Nomor SKD " & txtCODE.Text, MsgBoxStyle.Information, Me.Text)
    '        sStatusSave = "NEW"
    '        Me.Close()
    '    End If
    'End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Nomor SKD " & txtCODE.Text, MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If

    End Sub
    'Private Sub btnTanpaResgistrasie_Click() Handles btnTanpaResgistrasi.ItemClick
    '    Try
    '        If grdKDPENDAFTARAN.Text = "" Then
    '            If grdKDDEPARTMENT.Text <> "" Then
    '                If grdKDDOCTOR.Text <> "" Then
    '                    If cboSKDSPRI.SelectedIndex = 0 Then
    '                        If lblKODESKD.Text = "-" Then
    '                            Dim jsonResponse As String = String.Empty
    '                            Dim jsonRequest As String = fn_RequestInsertRencanaKontrol(txtNomorSepaAtauKartu.Text)
    '                            Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
    '                            Dim oSetKoneksi As New Brigging.clsSetKoneksi

    '                            If jsonRequest <> "" Then
    '                                jsonResponse = fn_InsertRencanaKontrol(jsonRequest, uTime)
    '                                If jsonResponse <> "" Then
    '                                    Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM2")
    '                                    Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(jsonResponse, dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime))

    '                                    lblKODESKD.Text = DataDecrypt.Item("noSuratKontrol").ToString()

    '                                    Dim CodeResponse As String = String.Empty
    '                                    Dim messageResponse As String = String.Empty

    '                                    CodeResponse = IIf(IsDBNull(DataDecrypt.Item("metaData").Item("code")) = True, "", DataDecrypt.Item("metaData").Item("code"))
    '                                    messageResponse = DataDecrypt("metaData")("message").ToString

    '                                    MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
    '                                    'Me.Close()
    '                                End If
    '                            End If
    '                        Else
    '                            Dim jsonResponse As String = String.Empty
    '                            Dim jsonRequest As String = fn_RequestUpdateRencanaKontrol(lblKODESKD.Text, lblNoSEP.Text)
    '                            Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
    '                            Dim oSetKoneksi As New Brigging.clsSetKoneksi

    '                            If jsonRequest <> "" Then
    '                                jsonResponse = fn_UpdateRencanaKontrol(jsonRequest, uTime)
    '                                If jsonResponse <> "" Then
    '                                    Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM2")
    '                                    Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(jsonResponse, dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime))

    '                                    Dim CodeResponse As String = String.Empty
    '                                    Dim messageResponse As String = String.Empty

    '                                    CodeResponse = IIf(IsDBNull(DataDecrypt.Item("metaData").Item("code")) = True, "", DataDecrypt.Item("metaData").Item("code"))
    '                                    messageResponse = DataDecrypt("metaData")("message").ToString

    '                                    MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)

    '                                End If
    '                            End If
    '                        End If
    '                    Else
    '                        If lblKODESKD.Text = "-" Then
    '                            Dim jsonResponse As String = String.Empty
    '                            Dim jsonRequest As String = fn_RequestInsertSPRI(txtNomorSepaAtauKartu.Text)
    '                            Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
    '                            Dim oSetKoneksi As New Brigging.clsSetKoneksi

    '                            If jsonRequest <> "" Then
    '                                jsonResponse = fn_InsertSPRI(jsonRequest, uTime)
    '                                If jsonResponse <> "" Then
    '                                    Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM2")
    '                                    Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(jsonResponse, dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime))
    '                                    'txtCODE.Text = DataDecrypt.Item("noSuratKontrol").ToString()
    '                                    'Me.Close()
    '                                End If
    '                            End If
    '                        Else
    '                            Dim jsonResponse As String = String.Empty
    '                            Dim jsonRequest As String = fn_RequestUpdateSPRI(lblKODESKD.Text)
    '                            Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
    '                            Dim oSetKoneksi As New Brigging.clsSetKoneksi

    '                            If jsonRequest <> "" Then
    '                                jsonResponse = fn_UpdateSPRI(jsonRequest, uTime)
    '                                If jsonResponse <> "" Then
    '                                    Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM2")
    '                                    Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(jsonResponse, dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime))

    '                                    Dim CodeResponse As String = String.Empty
    '                                    Dim messageResponse As String = String.Empty

    '                                    CodeResponse = IIf(IsDBNull(DataDecrypt.Item("metaData").Item("code")) = True, "", DataDecrypt.Item("metaData").Item("code"))
    '                                    messageResponse = DataDecrypt("metaData")("message").ToString

    '                                    MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)

    '                                End If
    '                            End If
    '                        End If
    '                    End If
    '                Else
    '                    grdKDDOCTOR.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
    '                    grdKDDOCTOR.ErrorText = Statement.ErrorRequired
    '                    grdKDDOCTOR.Focus()
    '                End If
    '            Else
    '                grdKDDEPARTMENT.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
    '                grdKDDEPARTMENT.ErrorText = Statement.ErrorRequired
    '                grdKDDEPARTMENT.Focus()
    '            End If
    '        Else
    '            MsgBox("No Register Ada Silahkan Kosong Kan Terlebih dahulu", MsgBoxStyle.Exclamation, Me.Text)
    '        End If
    '    Catch oErr As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try

    'End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadKDDEPARTMENT()
        Dim oDEPARTMENT As New Reference.clsDepartment
        Try
            grdKDDEPARTMENT.Properties.DataSource = oDEPARTMENT.GetData.Where(Function(x) x.ISACTIVE = True And x.VCLAIM_KODEPOLI <> "").ToList()
            grdKDDEPARTMENT.Properties.ValueMember = "KDDEPARTMENT"
            grdKDDEPARTMENT.Properties.DisplayMember = "NAME_DISPLAY"

            grdKDDEPARTMENT2.Properties.DataSource = oDEPARTMENT.GetData.Where(Function(x) x.ISACTIVE = True And x.VCLAIM_KODEPOLI <> "").ToList()
            grdKDDEPARTMENT2.Properties.ValueMember = "KDDEPARTMENT"
            grdKDDEPARTMENT2.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDDOCTOR(ByVal Parameter As String)
        Try
            If cboSKDSPRI.SelectedIndex = 0 Then
                Dim Contoh As Date = deDATEKONTROL.DateTime
                Dim Hasil As String = ""

                Select Case Weekday(Contoh)
                    Case 1
                        Hasil = "Minggu"
                    Case 2
                        Hasil = "Senin"
                    Case 3
                        Hasil = "Selasa"
                    Case 4
                        Hasil = "Rabu"
                    Case 5
                        Hasil = "Kamis"
                    Case 6
                        Hasil = "Jumat"
                    Case 7
                        Hasil = "Sabtu"
                End Select

                Dim oDoctor As New Reference.clsDoctor

                'Dim oJadwal As New Inventory.clsJadwalDokter

                Dim ds = From x In oDoctor.GetDataDetailbyHariDepatment(Parameter, Hasil)
                         Select x.KDDOCTOR, NAME_DISPLAY = x.M_DOCTOR.NAME_DISPLAY
                         Order By NAME_DISPLAY Ascending

                grdKDDOCTOR.Properties.DataSource = ds.ToList()
                grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
                grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

                grdKDDOCTOR2.Properties.DataSource = oDoctor.GetData.Where(Function(x) x.ISACTIVE = True And x.VCLAIM_KDDPJP <> "").ToList()
                grdKDDOCTOR2.Properties.ValueMember = "KDDOCTOR"
                grdKDDOCTOR2.Properties.DisplayMember = "NAME_DISPLAY"
            Else
                Dim oDoctor As New Reference.clsDoctor

                grdKDDOCTOR.Properties.DataSource = oDoctor.GetData.Where(Function(x) x.ISACTIVE = True And x.KDDEPARTMENT = Parameter And x.VCLAIM_KDDPJP <> "").ToList()
                grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
                grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

            End If

        Catch oErr As Exception
            MsgBox("Preview Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    'Private Sub fn_LoadKDDOCTOR()
    '    Dim oDOCTOR As New Reference.clsDoctor

    '    If rbCATEGORY.SelectedIndex = 0 Then
    '        Try
    '            Dim dsDoctor = From x In oDOCTOR.GetDataDetail_DEPARMENT
    '                           Where x.KDDEPARTMENT = grdKDDEPARTMENT.EditValue And x.M_DOCTOR.VCLAIM_KDDPJP <> "" And x.M_DEPARTMENT.ISRUANGRAWAT = False
    '                           Select x.KDDOCTOR, x.M_DOCTOR.NAME_DISPLAY

    '            grdKDDOCTOR.Properties.DataSource = dsDoctor.ToList()
    '            grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
    '            grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"
    '        Catch oErr As Exception
    '            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '        End Try
    '    Else
    '        Try
    '            Dim dsDoctor = From x In oDOCTOR.GetDataDetail_DEPARMENT
    '                           Where x.KDDEPARTMENT = grdKDDEPARTMENT.EditValue And x.M_DEPARTMENT.ISRUANGRAWAT = True
    '                           Select x.KDDOCTOR, x.M_DOCTOR.NAME_DISPLAY

    '            grdKDDOCTOR.Properties.DataSource = dsDoctor.ToList()
    '            grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
    '            grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"
    '        Catch oErr As Exception
    '            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '        End Try
    '    End If
    'End Sub
    Private Sub txtCARI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCARI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            PopUP = True
            fn_LoadKDPENDAFTARAN(txtCARI.Text)
            txtCARI.ResetText()
        End If
    End Sub
    Private Sub fn_LoadKDPENDAFTARAN(ByVal Parameter As String)
        Try
            Dim dsPendaftaran = (From x In oPendaftaran.GetDataBySKD(Parameter, cboCARI.SelectedIndex)
                                 Select x.KDPENDAFTARAN, x.NOMORSEP, x.CATEGORY, x.KDCUSTOMER, x.M_CUSTOMER.NAME_DISPLAY, TUJUAN = x.M_DEPARTMENT.NAME_DISPLAY, DOKTER = x.M_DOCTOR.NAME_DISPLAY, x.DATE).OrderByDescending(Function(x) x.DATE)

            grdKDPENDAFTARAN.Properties.DataSource = dsPendaftaran.ToList()
            grdKDPENDAFTARAN.Properties.ValueMember = "KDPENDAFTARAN"
            grdKDPENDAFTARAN.Properties.DisplayMember = "KDPENDAFTARAN"

            If PopUP = True Then
                grdKDPENDAFTARAN.ShowPopup()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDDEPARTMENT_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdKDDEPARTMENT.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If grdKDDEPARTMENT.Text = String.Empty Then Exit Sub
            fn_LoadKDDOCTOR(grdKDDEPARTMENT.EditValue)
            grdKDDOCTOR.ShowPopup()
        End If
    End Sub
    Private Sub grdKDPENDAFTARAN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdKDPENDAFTARAN.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Dim dsPendaftaran = oPendaftaran.GetData(grdKDPENDAFTARAN.EditValue)
            If dsPendaftaran IsNot Nothing Then
                If cboSKDSPRI.Text <> "" Then
                    deDATE.DateTime = dsPendaftaran.DATE
                    txtNOMORRUJKAN.Text = dsPendaftaran.NOMORRUJUKAN
                    grdKDDEPARTMENT.Text = dsPendaftaran.KDDEPARTMENT

                    If cboSKDSPRI.SelectedIndex <= 0 Then
                        txtNomorSepaAtauKartu.Text = dsPendaftaran.NOMORSEP
                    Else
                        txtNomorSepaAtauKartu.Text = dsPendaftaran.KARTUBPJS
                    End If
                    fn_LoadKDDOCTOR(grdKDDEPARTMENT.EditValue)
                Else
                    MsgBox("Silahkan Pilih Type", MsgBoxStyle.Exclamation, Me.Text)
                    cboSKDSPRI.Focus()
                End If
            End If
        End If
    End Sub
    Private Sub btnKunjungan_Click(sender As Object, e As EventArgs) Handles btnKunjungan.Click
        Dim dsPendaftaran = oPendaftaran.GetData(grdKDPENDAFTARAN.EditValue)
        If dsPendaftaran IsNot Nothing Then
            If dsPendaftaran.KARTUBPJS <> "" Then
                frmMonitoringBPJS.fn_LoadDataBPJS(dsPendaftaran.KARTUBPJS)
                frmMonitoringBPJS.ShowDialog(Me)
                If sCopySEPdiSKD <> "" Then
                    txtNomorSepaAtauKartu.Text = sCopySEPdiSKD
                End If
            End If
        Else
            MsgBox("Silahkan Pilih Registrasi Terlebih dahulu", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub cboSKDSPRI_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSKDSPRI.SelectedIndexChanged
        If isLoad = True Then
            If cboSKDSPRI.SelectedIndex <= 0 Then
                lDATE_KONTROL.Text = "Tgl. Kontrol"
                lSEPKARTU.Text = "No. SEP "
            Else
                lDATE_KONTROL.Text = "Tgl. SPRI"
                lSEPKARTU.Text = "No. Kartu "
            End If
        End If
    End Sub
    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        Try
            If cboType2.Text = "" Then
                MsgBox("Silahkan Pilih Type", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            If grdKDDEPARTMENT2.Text <> "" Then
                If grdKDDOCTOR2.Text <> "" Then
                    If cboType2.SelectedIndex = 0 Then
                        Dim jsonResponse As String = String.Empty
                        Dim jsonRequest As String = ""
                        Dim oDoctor As New Reference.clsDoctor
                        Dim oDepartment As New Reference.clsDepartment

                        jsonRequest = "{ "
                        jsonRequest &= " ""request"" :   { "
                        jsonRequest &= " ""noSEP"" :  """ & txtNoSEP2.Text & """ , "
                        jsonRequest &= " ""kodeDokter"" :  """ & oDoctor.GetData(grdKDDOCTOR2.EditValue).VCLAIM_KDDPJP & """ , "
                        jsonRequest &= " ""poliKontrol"" :  """ & oDepartment.GetData(grdKDDEPARTMENT2.EditValue).VCLAIM_KODEPOLI & """ , "
                        jsonRequest &= " ""tglRencanaKontrol"" :  """ & deKontrol2.DateTime.ToString("yyyy-MM-dd") & """ , "
                        jsonRequest &= " ""user"" :  """ & sUserID & """ "
                        jsonRequest &= " } "
                        jsonRequest &= " } "

                        Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                        Dim oSetKoneksi As New Brigging.clsSetKoneksi

                        If jsonRequest <> "" Then
                            jsonResponse = fn_InsertRencanaKontrol(jsonRequest, uTime)
                            If jsonResponse <> "" Then
                                Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(jsonResponse, sVclaim_ConsId & sVclaim_SecreatKey & uTime))

                                Dim CodeResponse As String = String.Empty
                                Dim messageResponse As String = String.Empty

                                txtKode2.Text = DataDecrypt.Item("noSuratKontrol").ToString()

                                'Me.Close()
                            End If
                        End If

                    Else
                        Dim jsonResponse As String = String.Empty
                        Dim jsonRequest As String = ""
                        Dim oDoctor As New Reference.clsDoctor
                        Dim oDepartment As New Reference.clsDepartment

                        jsonRequest = "{ "
                        jsonRequest &= " ""request"" :   { "
                        jsonRequest &= " ""noKartu"" :  """ & txtNoSEP2.Text & """ , "
                        jsonRequest &= " ""kodeDokter"" :  """ & oDoctor.GetData(grdKDDOCTOR2.EditValue).VCLAIM_KDDPJP & """ , "
                        jsonRequest &= " ""poliKontrol"" :  """ & oDepartment.GetData(grdKDDEPARTMENT2.EditValue).VCLAIM_KODEPOLI & """ , "
                        jsonRequest &= " ""tglRencanaKontrol"" :  """ & deKontrol2.DateTime.ToString("yyyy-MM-dd") & """ , "
                        jsonRequest &= " ""user"" :  """ & sUserID & """ "
                        jsonRequest &= " } "
                        jsonRequest &= " } "

                        Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                        Dim oSetKoneksi As New Brigging.clsSetKoneksi

                        If jsonRequest <> "" Then
                            jsonResponse = fn_InsertSPRI(jsonRequest, uTime)
                            If jsonResponse <> "" Then
                                Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(jsonResponse, sVclaim_ConsId & sVclaim_SecreatKey & uTime))

                                txtKode2.Text = DataDecrypt.Item("noSPRI").ToString()

                            End If
                        End If

                    End If
                Else
                    grdKDDOCTOR2.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    grdKDDOCTOR2.ErrorText = Statement.ErrorRequired
                    grdKDDOCTOR2.Focus()
                End If
            Else
                grdKDDEPARTMENT2.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDEPARTMENT2.ErrorText = Statement.ErrorRequired
                grdKDDEPARTMENT2.Focus()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            If cboType2.Text = "" Then
                MsgBox("Silahkan Pilih Type", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            If grdKDDEPARTMENT2.Text <> "" Then
                If grdKDDOCTOR2.Text <> "" Then
                    If cboType2.SelectedIndex = 0 Then
                        Dim jsonResponse As String = String.Empty
                        Dim jsonRequest As String = ""
                        Dim oDoctor As New Reference.clsDoctor
                        Dim oDepartment As New Reference.clsDepartment

                        jsonRequest = "{ "
                        jsonRequest &= " ""request"" :   { "
                        jsonRequest &= " ""noSuratKontrol"" :  """ & txtKode2.Text & """ , "
                        jsonRequest &= " ""noSEP"" :  """ & txtNoSEP2.Text & """ , "
                        jsonRequest &= " ""kodeDokter"" :  """ & oDoctor.GetData(grdKDDOCTOR2.EditValue).VCLAIM_KDDPJP & """ , "
                        jsonRequest &= " ""poliKontrol"" :  """ & oDepartment.GetData(grdKDDEPARTMENT2.EditValue).VCLAIM_KODEPOLI & """ , "
                        jsonRequest &= " ""tglRencanaKontrol"" :  """ & deKontrol2.DateTime.ToString("yyyy-MM-dd") & """ , "
                        jsonRequest &= " ""user"" :  """ & sUserID & """ "
                        jsonRequest &= " } "
                        jsonRequest &= " } "

                        Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                        Dim oSetKoneksi As New Brigging.clsSetKoneksi

                        If jsonRequest <> "" Then
                            jsonResponse = fn_UpdateRencanaKontrol(jsonRequest, uTime)
                            If jsonResponse <> "" Then
                                Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(jsonResponse, sVclaim_ConsId & sVclaim_SecreatKey & uTime))

                                MsgBox(DataDecrypt.Item("noSuratKontrol").ToString(), MsgBoxStyle.Information, Me.Text)
                                'Dim CodeResponse As String = String.Empty
                                'Dim messageResponse As String = String.Empty

                                'CodeResponse = IIf(IsDBNull(DataDecrypt.Item("metaData").Item("code")) = True, "", DataDecrypt.Item("metaData").Item("code"))
                                'messageResponse = DataDecrypt("metaData")("message").ToString
                            End If
                        End If
                    Else
                        Dim jsonResponse As String = String.Empty
                        Dim jsonRequest As String = ""
                        Dim oDoctor As New Reference.clsDoctor
                        Dim oDepartment As New Reference.clsDepartment

                        jsonRequest = "{ "
                        jsonRequest &= " ""request"" :   { "
                        jsonRequest &= " ""noSPRI"" :  """ & txtKode2.Text & """ , "
                        jsonRequest &= " ""kodeDokter"" :  """ & oDoctor.GetData(grdKDDOCTOR2.EditValue).VCLAIM_KDDPJP & """ , "
                        jsonRequest &= " ""poliKontrol"" :  """ & oDepartment.GetData(grdKDDEPARTMENT2.EditValue).VCLAIM_KODEPOLI & """ , "
                        jsonRequest &= " ""tglRencanaKontrol"" :  """ & deKontrol2.DateTime.ToString("yyyy-MM-dd") & """ , "
                        jsonRequest &= " ""user"" :  """ & sUserID & """ "
                        jsonRequest &= " } "
                        jsonRequest &= " } "

                        Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                        Dim oSetKoneksi As New Brigging.clsSetKoneksi

                        If jsonRequest <> "" Then
                            jsonResponse = fn_UpdateSPRI(jsonRequest, uTime)
                            If jsonResponse <> "" Then
                                Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(jsonResponse, sVclaim_ConsId & sVclaim_SecreatKey & uTime))

                                MsgBox(DataDecrypt.Item("noSPRI").ToString(), MsgBoxStyle.Information, Me.Text)

                                'Dim CodeResponse As String = String.Empty
                                'Dim messageResponse As String = String.Empty

                                'CodeResponse = IIf(IsDBNull(DataDecrypt.Item("metaData").Item("code")) = True, "", DataDecrypt.Item("metaData").Item("code"))
                                'messageResponse = DataDecrypt("metaData")("message").ToString

                                'MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                            End If
                        End If
                    End If
                Else
                    grdKDDOCTOR2.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    grdKDDOCTOR2.ErrorText = Statement.ErrorRequired
                    grdKDDOCTOR2.Focus()
                End If
            Else
                grdKDDEPARTMENT2.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDEPARTMENT2.ErrorText = Statement.ErrorRequired
                grdKDDEPARTMENT2.Focus()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        frmMonitoringBPJS.fn_LoadDataBPJS(txtKartu2.Text)
        frmMonitoringBPJS.ShowDialog(Me)
        If sCopySEPdiSKD <> "" Then
            txtNoSEP2.Text = sCopySEPdiSKD
        End If
    End Sub
#End Region
End Class