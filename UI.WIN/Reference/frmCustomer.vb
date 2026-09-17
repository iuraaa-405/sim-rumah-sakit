Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq
Imports System.Text.RegularExpressions

'Imports System.Data.SqlClient
'Imports MySql.Data.MySqlClient

Public Class frmCustomer
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oCustomer As New Reference.clsCustomer
    Private listCustomer As New M_CUSTOMER
    Private oCustomerSatusehat As New Reference.clsCustomerSatuSehat
    Private jsonArray As New JArray()

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal entity As M_CUSTOMER, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId
        Dim dsDatabase = oCustomer.GetDataSetting
        listCustomer = entity
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = Customer.TITLE

            lKESATUAN.Text = Customer.KDKESATUAN & " *"
            lPENJAMIN.Text = Customer.KDPENJAMIN & " *"
            lPERUSAHAAN.Text = Customer.KDPERUSAHAAN & " *"
            lRM.Text = Customer.KDCUSTOMER & " *"
            lKTP.Text = Customer.KTP & " *"
            lNAME_DISPLAY.Text = Customer.NAME_DISPLAY & " *"
            chkISACTIVE.Text = Customer.ISACTIVE

            lPHONE.Text = Customer.PHONE
            lFAX.Text = Customer.FAX
            lMOBILE.Text = Customer.MOBILE
            lOTHER.Text = Customer.OTHER
            lEMAIL.Text = Customer.EMAIL
            lWEBSITE.Text = Customer.WEBSITE

            lBILL_STREET.Text = Customer.BILL_STREET
            lKELURAHAN.Text = Kelurahan.MEMO
            lKECAMATAN.Text = Kecamatan.MEMO
            lKABUPATEN.Text = Kabupaten.MEMO
            lPROPINSI.Text = Propinsi.MEMO
            lBILL_ZIP.Text = Customer.BILL_ZIP
            lBILL_COUNTRY.Text = Customer.BILL_COUNTRY

            lPANGKAT.Text = Customer.KDPANGKAT & " *"
            lGOLONGAN.Text = Customer.KDGOLONGAN & " *"
            lPENDIDIKAN.Text = Customer.KDPENDIDIKAN & " *"
            lPEKERJAAN.Text = Customer.KDPEKERJAAN & " *"
            lAGAMA.Text = Customer.KDAGAMA & " *"
            lJENISKELAMIN.Text = Customer.KDJENISKELAMIN & " *"
            lGOLONGANDARAH.Text = Customer.KDGOLONGANDARAH & " *"
            lSTATUSKAWIN.Text = Customer.KDSTATUSKAWIN & " *"
            lSUKU.Text = Customer.KDSUKU & " *"
            lSTATUSHIDUP.Text = Customer.KDSTATUSHIDUP
            chkSTATUSHIDUP.Text = Customer.KDSTATUSHIDUP
            lNRP.Text = Customer.NRP
            lNAMAKELUARGA.Text = Customer.NAMAKELUARGA
            lKDSTATUSKELUARGA.Text = Customer.KDSTATUSKELUARGA & " *"
            lTEMPATLAHIR.Text = Customer.TEMPATLAHIR & " *"
            lTANGGALLAHIR.Text = Customer.TANGGALLAHIR & " *"
            lWNI.Text = Customer.WNI
            lKARTUBPJS.Text = Customer.KARTUBPJS

            tab1.Text = Customer.TAB_CONTACT
            tab2.Text = Customer.TAB_BILL
            tab3.Text = Customer.TAB_OTHER

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDCUSTOMER.Text.Trim.ToUpper
        sCodeCustomer = txtKDCUSTOMER.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDKESATUAN()
        fn_LoadKDPENJAMIN()
        fn_LoadKDPERUSAHAAN()
        fn_LoadKDPANGKAT()
        fn_LoadKDGOLONGAN()
        fn_LoadKDPENDIDIKAN()
        fn_LoadKDPEKERJAAN()
        fn_LoadKDAGAMA()
        fn_LoadKDSUKU()
        fn_LoadKDSTATUSKELUARGA()
        'fn_LoadKDKELURAHAN()

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

        If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            txtKDCUSTOMER.Properties.ReadOnly = False
        Else
            txtKDCUSTOMER.Properties.ReadOnly = True
        End If

        txtKDCUSTOMERLAMA.Properties.ReadOnly = Status
        grdKDKESATUAN.Properties.ReadOnly = Status
        grdKDPENJAMIN.Properties.ReadOnly = Status
        grdKDPERUSAHAAN.Properties.ReadOnly = Status
        txtKTP.Properties.ReadOnly = Status
        txtNAME_DISPLAY.Properties.ReadOnly = Status
        chkISACTIVE.Properties.ReadOnly = Status

        txtPHONE.Properties.ReadOnly = Status
        txtFAX.Properties.ReadOnly = Status
        txtMOBILE.Properties.ReadOnly = Status
        txtEMAIL.Properties.ReadOnly = Status
        txtOTHER.Properties.ReadOnly = Status
        txtWEBSITE.Properties.ReadOnly = Status

        txtBILL_STREET.Properties.ReadOnly = Status
        'grdKDKELURAHAN.Properties.ReadOnly = Status
        txtKDKELURAHAN.Properties.ReadOnly = True
        txtBILL_ZIP.Properties.ReadOnly = Status
        txtKELURAHAN.Properties.ReadOnly = True
        txtBILL_COUNTRY.Properties.ReadOnly = Status

        txtMEMO.Properties.ReadOnly = True

        grdKDPANGKAT.Properties.ReadOnly = Status
        grdKDGOLONGAN.Properties.ReadOnly = Status
        grdKDPENDIDIKAN.Properties.ReadOnly = Status
        grdKDPEKERJAAN.Properties.ReadOnly = Status
        grdKDAGAMA.Properties.ReadOnly = Status
        cboJENISKELAMIN.Properties.ReadOnly = Status
        cboKDGOLONGANDARAH.Properties.ReadOnly = Status
        cboKDSTATUSKAWIN.Properties.ReadOnly = Status
        grdKDSUKU.Properties.ReadOnly = Status
        chkSTATUSHIDUP.Properties.ReadOnly = Status
        txtNRP.Properties.ReadOnly = Status
        txtNAMAKELUARGA.Properties.ReadOnly = Status
        grdKDSTATUSKELUARGA.Properties.ReadOnly = Status
        txtTEMPATLAHIR.Properties.ReadOnly = Status
        deTANGGLLAHIR.Properties.ReadOnly = Status
        rbWNI.Properties.ReadOnly = Status

        txtPROPINSI.Properties.ReadOnly = True
        txtKABUPATEN.Properties.ReadOnly = True
        txtKECAMATAN.Properties.ReadOnly = True
        txtJENISPESERTA.Properties.ReadOnly = True

        tabControl.SelectedTabPage = tab3
        tabControl.SelectedTabPage = tab2
        tabControl.SelectedTabPage = tab1
    End Sub
    Private Sub fn_EmptyMe()
        If listCustomer IsNot Nothing Then
            txtKDCUSTOMER.Text = sKDCUSTOMERX
            txtKTP.Text = listCustomer.KTP
            txtNAME_DISPLAY.Text = listCustomer.NAME_DISPLAY

            chkISACTIVE.Checked = True
            txtKDCUSTOMERLAMA.Text = listCustomer.KDCUSTOMER_LAMA
            txtPHONE.Text = listCustomer.PHONE
            txtFAX.Text = listCustomer.FAX
            txtMOBILE.Text = listCustomer.MOBILE
            txtEMAIL.Text = listCustomer.EMAIL
            txtOTHER.Text = listCustomer.OTHER
            txtWEBSITE.Text = listCustomer.WEBSITE

            txtBILL_STREET.Text = listCustomer.ALAMAT
            'grdKDKELURAHAN.Text = listCustomer.KDKELURAHAN
            txtKDKELURAHAN.Text = listCustomer.KDKELURAHAN
            txtBILL_ZIP.Text = listCustomer.KODEPOS
            txtBILL_COUNTRY.Text = listCustomer.NEGARA

            txtMEMO.ResetText()

            grdKDKESATUAN.Text = listCustomer.KDKESATUAN
            grdKDPENJAMIN.Text = listCustomer.KDPENJAMIN
            grdKDPERUSAHAAN.Text = listCustomer.KDPERUSAHAAN
            grdKDPANGKAT.Text = listCustomer.KDPANGKAT
            grdKDGOLONGAN.Text = listCustomer.KDGOLONGAN
            grdKDPENDIDIKAN.Text = listCustomer.KDPENDIDIKAN
            grdKDPEKERJAAN.Text = listCustomer.KDPEKERJAAN
            grdKDAGAMA.Text = listCustomer.KDAGAMA
            cboJENISKELAMIN.SelectedIndex = listCustomer.KDJENISKELAMIN
            cboKDGOLONGANDARAH.SelectedIndex = listCustomer.KDGOLONGANDARAH
            cboKDSTATUSKAWIN.SelectedIndex = listCustomer.KDSTATUSKAWIN
            grdKDSUKU.Text = listCustomer.KDSUKU
            chkSTATUSHIDUP.Checked = listCustomer.KDSTATUSHIDUP
            txtNRP.Text = listCustomer.NRP
            txtNAMAKELUARGA.Text = listCustomer.NAMAKELUARGA
            grdKDSTATUSKELUARGA.Text = listCustomer.KDSTATUSKELUARGA
            txtTEMPATLAHIR.Text = listCustomer.TEMPATLAHIR
            deTANGGLLAHIR.DateTime = listCustomer.TANGGALLAHIR
            rbWNI.SelectedIndex = listCustomer.WNI
            txtKARTUBPJS.Text = listCustomer.KARTUBPJS

            fn_LoadCariPropinsi(txtKDKELURAHAN.Text)

            tabControl.SelectedTabPage = tab1

        Else
            txtKDCUSTOMER.Text = "<--- AUTO --->"
            txtKTP.ResetText()
            txtNAME_DISPLAY.ResetText()

            chkISACTIVE.Checked = True
            txtKDCUSTOMERLAMA.ResetText()
            txtPHONE.ResetText()
            txtFAX.ResetText()
            txtMOBILE.ResetText()
            txtEMAIL.ResetText()
            txtOTHER.ResetText()
            txtWEBSITE.ResetText()

            txtBILL_STREET.ResetText()
            'grdKDKELURAHAN.ResetText()
            txtKDKELURAHAN.ResetText()
            txtKELURAHAN.ResetText()
            txtBILL_ZIP.ResetText()
            txtBILL_COUNTRY.Text = "INDONESIA"

            txtMEMO.ResetText()

            grdKDKESATUAN.Text = oCustomer.KesatuanDefault
            grdKDPENJAMIN.Text = oCustomer.PenjaminDefault
            grdKDPERUSAHAAN.Text = oCustomer.PerusahaanDefault
            grdKDPANGKAT.Text = oCustomer.PangkatDefault
            grdKDGOLONGAN.Text = oCustomer.GolonganDefault
            grdKDPENDIDIKAN.Text = oCustomer.PendidikanDefault
            grdKDPEKERJAAN.Text = oCustomer.PekerjaanDefault
            grdKDAGAMA.Text = oCustomer.AgamaDefault
            cboJENISKELAMIN.ResetText()
            cboKDGOLONGANDARAH.SelectedIndex = 0
            cboKDSTATUSKAWIN.SelectedIndex = 0
            grdKDSUKU.Text = oCustomer.SukuDefault
            chkSTATUSHIDUP.Checked = False
            txtNRP.Text = String.Empty
            txtNAMAKELUARGA.Text = String.Empty
            grdKDSTATUSKELUARGA.Text = oCustomer.StatusKeluargaDefault
            txtTEMPATLAHIR.Text = String.Empty
            deTANGGLLAHIR.DateTime = Now
            rbWNI.SelectedIndex = 0
            'txtKARTUBPJS.Text = sKARTUKODEBOKING
            txtKARTUBPJS.Text = ""

            tabControl.SelectedTabPage = tab1
        End If

    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oCustomer.GetData(sNoId)

            With ds

                txtKDCUSTOMER.Text = .KDCUSTOMER
                txtKDCUSTOMERLAMA.Text = .KDCUSTOMER_LAMA
                txtKTP.Text = .KTP
                txtNAME_DISPLAY.Text = .NAME_DISPLAY
                chkISACTIVE.Checked = .ISACTIVE

                txtPHONE.Text = .PHONE
                txtFAX.Text = .FAX
                txtMOBILE.Text = .MOBILE
                txtEMAIL.Text = .EMAIL
                txtOTHER.Text = .OTHER
                txtWEBSITE.Text = .WEBSITE

                txtBILL_STREET.Text = .ALAMAT

                txtKDKELURAHAN.Text = .KDKELURAHAN

                fn_LoadCariPropinsi(txtKDKELURAHAN.Text)

                'txtBILL_STATE.Text = .BILL_STATE
                txtBILL_ZIP.Text = .KODEPOS
                txtBILL_COUNTRY.Text = .NEGARA

                txtMEMO.Text = .MEMO

                grdKDKESATUAN.Text = .KDKESATUAN
                grdKDPENJAMIN.Text = .KDPENJAMIN
                grdKDPERUSAHAAN.Text = .KDPERUSAHAAN
                grdKDPANGKAT.Text = .KDPANGKAT
                grdKDGOLONGAN.Text = .KDGOLONGAN
                grdKDPENDIDIKAN.Text = .KDPENDIDIKAN
                grdKDPEKERJAAN.Text = .KDPEKERJAAN
                grdKDAGAMA.Text = .KDAGAMA
                cboJENISKELAMIN.SelectedIndex = .KDJENISKELAMIN
                cboKDGOLONGANDARAH.SelectedIndex = .KDGOLONGANDARAH
                cboKDSTATUSKAWIN.SelectedIndex = .KDSTATUSKAWIN
                grdKDSUKU.Text = .KDSUKU
                chkSTATUSHIDUP.Checked = .KDSTATUSHIDUP
                txtNRP.Text = .NRP
                txtNAMAKELUARGA.Text = .NAMAKELUARGA
                grdKDSTATUSKELUARGA.Text = .KDSTATUSKELUARGA
                txtTEMPATLAHIR.Text = .TEMPATLAHIR
                deTANGGLLAHIR.DateTime = .TANGGALLAHIR
                rbWNI.SelectedIndex = .WNI
                txtKARTUBPJS.Text = .KARTUBPJS

                tabControl.SelectedTabPage = tab3
                tabControl.SelectedTabPage = tab2
                tabControl.SelectedTabPage = tab1

                If txtKARTUBPJS.Text.Count = 13 Then
                    Try
                        Dim oSetKoneksi As New Brigging.clsSetKoneksi
                        Dim uTime As Integer = 0

                        If sVclaim_ConsId <> "" Then
                            uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                            Dim dsSetKoneksi = oSetKoneksi.GetDataVClaimPesertaNoKartuBPJS(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, txtKARTUBPJS.Text, Now.ToString("yyyy-MM-dd"))

                            If dsSetKoneksi <> "" Then
                                Try
                                    Dim allData = JObject.Parse(dsSetKoneksi)

                                    Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), sVclaim_ConsId & sVclaim_SecreatKey & uTime))

                                    Dim jenisPeserta As String = DataDecrypt("peserta")("jenisPeserta")("keterangan").ToString()
                                    Dim kodejenisPeserta As String = DataDecrypt("peserta")("jenisPeserta")("kode").ToString()

                                    'If jenisPeserta <> "" Then
                                    '    InsertJenisPeserta(kodejenisPeserta, jenisPeserta)
                                    'End If

                                    txtJENISPESERTA.Text = jenisPeserta

                                Catch oErr As Exception
                                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message & vbCrLf & vbCrLf & "Result : " & vbCrLf & dsSetKoneksi, MsgBoxStyle.Exclamation, Me.Text)
                                End Try
                            End If
                        Else
                            MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
                        End If

                        'txtVCLAIM_KDDOCTOR.ResetText()

                    Catch oErr As Exception
                        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                    'ElseIf txtKARTUBPJS.Text.Count > 13 Then
                    '    MsgBox(Statement.ErrorStatement & "No Kartu BPJS > 13 Digit", MsgBoxStyle.Exclamation, Me.Text)
                    'Else
                    '    MsgBox(Statement.ErrorStatement & "Kartu BPJS < 13 Digit", MsgBoxStyle.Exclamation, Me.Text)
                End If
            End With

            Dim dsSatuSehat = oCustomerSatusehat.GetData(txtKDCUSTOMER.Text)
            If dsSatuSehat IsNot Nothing Then
                lblIDSATUSEHAT.Text = dsSatuSehat.IDSATUSEHAT
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If txtKDCUSTOMER.Text = "" Then
                MsgBox("Rekam medis tidak boleh " & txtKDCUSTOMER.Text & " Silahkan perbaiki Nomor Rekam Medis", MsgBoxStyle.Exclamation, Me.Text)

                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            ElseIf txtKDCUSTOMER.Text = "0" Then
                MsgBox("Rekam medis tidak boleh " & txtKDCUSTOMER.Text & " Silahkan perbaiki Nomor Rekam Medis", MsgBoxStyle.Exclamation, Me.Text)

                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            ElseIf txtKDCUSTOMER.Text = "00" Then
                MsgBox("Rekam medis tidak boleh " & txtKDCUSTOMER.Text & " Silahkan perbaiki Nomor Rekam Medis", MsgBoxStyle.Exclamation, Me.Text)

                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            ElseIf txtKDCUSTOMER.Text = "000" Then
                MsgBox("Rekam medis tidak boleh " & txtKDCUSTOMER.Text & " Silahkan perbaiki Nomor Rekam Medis", MsgBoxStyle.Exclamation, Me.Text)

                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            ElseIf txtKDCUSTOMER.Text = "0000" Then
                MsgBox("Rekam medis tidak boleh " & txtKDCUSTOMER.Text & " Silahkan perbaiki Nomor Rekam Medis", MsgBoxStyle.Exclamation, Me.Text)

                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            ElseIf txtKDCUSTOMER.Text = "00000" Then
                MsgBox("Rekam medis tidak boleh " & txtKDCUSTOMER.Text & " Silahkan perbaiki Nomor Rekam Medis", MsgBoxStyle.Exclamation, Me.Text)

                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            ElseIf txtKDCUSTOMER.Text = "000000" Then
                MsgBox("Rekam medis tidak boleh " & txtKDCUSTOMER.Text & " Silahkan perbaiki Nomor Rekam Medis", MsgBoxStyle.Exclamation, Me.Text)

                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            ElseIf txtKDCUSTOMER.Text = "0000000" Then
                MsgBox("Rekam medis tidak boleh " & txtKDCUSTOMER.Text & " Silahkan perbaiki Nomor Rekam Medis", MsgBoxStyle.Exclamation, Me.Text)

                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            ElseIf txtKDCUSTOMER.Text = "00000000" Then
                MsgBox("Rekam medis tidak boleh " & txtKDCUSTOMER.Text & " Silahkan perbaiki Nomor Rekam Medis", MsgBoxStyle.Exclamation, Me.Text)

                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            ElseIf txtKDCUSTOMER.Text = "000000000" Then
                MsgBox("Rekam medis tidak boleh " & txtKDCUSTOMER.Text & " Silahkan perbaiki Nomor Rekam Medis", MsgBoxStyle.Exclamation, Me.Text)

                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            ElseIf txtKDCUSTOMER.Text = "0000000000" Then
                MsgBox("Rekam medis tidak boleh " & txtKDCUSTOMER.Text & " Silahkan perbaiki Nomor Rekam Medis", MsgBoxStyle.Exclamation, Me.Text)

                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            ElseIf txtKDCUSTOMER.Text = "00000000000" Then
                MsgBox("Rekam medis tidak boleh " & txtKDCUSTOMER.Text & " Silahkan perbaiki Nomor Rekam Medis", MsgBoxStyle.Exclamation, Me.Text)

                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtKDCUSTOMER.Text = String.Empty Then
                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtKTP.Text = String.Empty Then
                txtKTP.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKTP.ErrorText = Statement.ErrorRequired

                txtKTP.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDKESATUAN.Text = String.Empty Then
                grdKDKESATUAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDKESATUAN.ErrorText = Statement.ErrorRequired

                grdKDKESATUAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDPENJAMIN.Text = String.Empty Then
                grdKDPENJAMIN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDPENJAMIN.ErrorText = Statement.ErrorRequired

                grdKDPENJAMIN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDPERUSAHAAN.Text = String.Empty Then
                grdKDPERUSAHAAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDPERUSAHAAN.ErrorText = Statement.ErrorRequired

                grdKDPERUSAHAAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtNAME_DISPLAY.Text = String.Empty Then
                txtNAME_DISPLAY.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtNAME_DISPLAY.ErrorText = Statement.ErrorRequired

                txtNAME_DISPLAY.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDPANGKAT.Text = String.Empty Then
                grdKDPANGKAT.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDPANGKAT.ErrorText = Statement.ErrorRequired

                grdKDPANGKAT.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDGOLONGAN.Text = String.Empty Then
                grdKDGOLONGAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDGOLONGAN.ErrorText = Statement.ErrorRequired

                grdKDGOLONGAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDPENDIDIKAN.Text = String.Empty Then
                grdKDPENDIDIKAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDPENDIDIKAN.ErrorText = Statement.ErrorRequired

                grdKDPENDIDIKAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDPEKERJAAN.Text = String.Empty Then
                grdKDPEKERJAAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDPEKERJAAN.ErrorText = Statement.ErrorRequired

                grdKDPEKERJAAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDAGAMA.Text = String.Empty Then
                grdKDAGAMA.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDAGAMA.ErrorText = Statement.ErrorRequired

                grdKDAGAMA.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDAGAMA.Text = String.Empty Then
                grdKDAGAMA.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDAGAMA.ErrorText = Statement.ErrorRequired

                grdKDAGAMA.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDSUKU.Text = String.Empty Then
                grdKDSUKU.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDSUKU.ErrorText = Statement.ErrorRequired

                grdKDSUKU.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDSTATUSKELUARGA.Text = String.Empty Then
                grdKDSTATUSKELUARGA.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDSTATUSKELUARGA.ErrorText = Statement.ErrorRequired

                grdKDSTATUSKELUARGA.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtTEMPATLAHIR.Text = String.Empty Then
                txtTEMPATLAHIR.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtTEMPATLAHIR.ErrorText = Statement.ErrorRequired

                txtTEMPATLAHIR.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtBILL_STREET.Text = String.Empty Then
                tabControl.SelectedTabPage = tab2
                txtBILL_STREET.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtBILL_STREET.ErrorText = Statement.ErrorRequired

                txtBILL_STREET.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtKDKELURAHAN.Text = String.Empty Then
                MsgBox("Di butuhkan Kelurahan/Desa", MsgBoxStyle.Exclamation, Me.Text)
                tabControl.SelectedTabPage = tab2
                txtKDKELURAHAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDKELURAHAN.ErrorText = Statement.ErrorRequired

                txtKDKELURAHAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            'If txtOTHER.Text = String.Empty Then
            '    MsgBox("Di butuhkan Kota", MsgBoxStyle.Exclamation, Me.Text)
            '    tabControl.SelectedTabPage = tab2
            '    txtOTHER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '    txtOTHER.ErrorText = Statement.ErrorRequired

            '    txtOTHER.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If
            'If txtWEBSITE.Text = String.Empty Then
            '    MsgBox("Di butuhkan RT", MsgBoxStyle.Exclamation, Me.Text)
            '    tabControl.SelectedTabPage = tab2
            '    txtWEBSITE.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '    txtWEBSITE.ErrorText = Statement.ErrorRequired

            '    txtWEBSITE.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If
            'If txtFAX.Text = String.Empty Then
            '    MsgBox("Di butuhkan RW", MsgBoxStyle.Exclamation, Me.Text)
            '    tabControl.SelectedTabPage = tab2
            '    txtFAX.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '    txtFAX.ErrorText = Statement.ErrorRequired

            '    txtFAX.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If
            'If txtPHONE.Text = String.Empty Then
            '    MsgBox("Di butuhkan nomor telepon", MsgBoxStyle.Exclamation, Me.Text)
            '    tabControl.SelectedTabPage = tab1
            '    txtPHONE.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '    txtPHONE.ErrorText = Statement.ErrorRequired

            '    txtPHONE.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If

            If cboJENISKELAMIN.Text = "" Then
                cboJENISKELAMIN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                cboJENISKELAMIN.ErrorText = Statement.ErrorRequired

                cboJENISKELAMIN.Focus()
                fn_Validate = False
                Exit Function
            Else
                If cboJENISKELAMIN.SelectedIndex <> 0 Then
                    If cboJENISKELAMIN.SelectedIndex <> 1 Then
                        cboJENISKELAMIN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                        cboJENISKELAMIN.ErrorText = Statement.ErrorRequired

                        cboJENISKELAMIN.Focus()
                        fn_Validate = False
                        Exit Function
                    End If
                End If
            End If
            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Dim dsCekRM = oCustomer.GetData(txtKDCUSTOMER.Text)
                If dsCekRM IsNot Nothing Then
                    MsgBox("Nomor Rekam Medis Sudah ada untuk Pasien " & dsCekRM.NAME_DISPLAY, MsgBoxStyle.Exclamation, Me.Text)
                    txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                    txtKDCUSTOMER.Focus()
                    fn_Validate = False
                    Exit Function
                End If

                If txtKARTUBPJS.Text <> "" Then
                    Dim dsCekkartu = oCustomer.GetDataKARTUBPJS(txtKARTUBPJS.Text)
                    If dsCekkartu IsNot Nothing Then
                        MsgBox("Nomor Kartu BPJS Sudah ada untuk Pasien " & dsCekkartu.NAME_DISPLAY, MsgBoxStyle.Exclamation, Me.Text)
                        txtKARTUBPJS.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                        txtKARTUBPJS.ErrorText = Statement.ErrorRequired

                        txtKARTUBPJS.Focus()
                        fn_Validate = False
                        Exit Function
                    End If
                End If

                Dim dsCekNIK = oCustomer.GetDataKTP(txtKTP.Text)
                If dsCekNIK IsNot Nothing Then
                    MsgBox("Nomor NIK Sudah ada untuk Pasien " & dsCekNIK.NAME_DISPLAY, MsgBoxStyle.Exclamation, Me.Text)
                    txtKTP.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    txtKTP.ErrorText = Statement.ErrorRequired

                    txtKTP.Focus()
                    fn_Validate = False
                    Exit Function
                End If
            End If
            'If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            '    If oCustomer.IsExist(txtNAME_DISPLAY.Text.ToUpper.Trim) = True Then
            '        txtNAME_DISPLAY.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '        txtNAME_DISPLAY.ErrorText = Statement.ErrorRegistered

            '        txtNAME_DISPLAY.Focus()
            '        fn_Validate = False
            '        Exit Function
            '    End If
            'Else
            '    If txtNAME_DISPLAY.Text.Trim.ToUpper <> oCustomer.GetData(sNoId).NAME_DISPLAY Then
            '        If oCustomer.IsExist(txtNAME_DISPLAY.Text.ToUpper.Trim) = True Then
            '            txtNAME_DISPLAY.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '            txtNAME_DISPLAY.ErrorText = Statement.ErrorRegistered

            '            txtNAME_DISPLAY.Focus()
            '            fn_Validate = False
            '            Exit Function
            '        End If
            '    End If
            'End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oCustomer.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oCustomer.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDCUSTOMER = sNoId
                .KDCUSTOMER_LAMA = txtKDCUSTOMERLAMA.Text.ToString.Trim.ToUpper
                .NAME_DISPLAY = txtNAME_DISPLAY.Text.Trim.ToUpper
                .PHONE = txtPHONE.Text.Trim.ToUpper
                .FAX = txtFAX.Text.Trim.ToUpper
                .MOBILE = txtMOBILE.Text.Trim.ToUpper
                .EMAIL = txtEMAIL.Text.Trim.ToUpper
                .OTHER = txtOTHER.Text.Trim.ToUpper
                .WEBSITE = txtWEBSITE.Text.Trim.ToUpper
                .ALAMAT = txtBILL_STREET.Text.Trim.ToUpper
                '.BILL_CITY = txtBILL_CITY.Text.Trim.ToUpper
                '.BILL_STATE = txtBILL_STATE.Text.Trim.ToUpper
                .KTP = txtKTP.Text.ToString.Trim
                .KDKELURAHAN = txtKDKELURAHAN.Text
                .KODEPOS = txtBILL_ZIP.Text.Trim.ToUpper
                .NEGARA = txtBILL_COUNTRY.Text.Trim.ToUpper
                .MEMO = sUserID
                .KDCOA = "1401-001"
                .ISACTIVE = chkISACTIVE.Checked
                .KDPERUSAHAAN = IIf(String.IsNullOrEmpty(grdKDPERUSAHAAN.EditValue), String.Empty, grdKDPERUSAHAAN.EditValue)
                .KDKESATUAN = IIf(String.IsNullOrEmpty(grdKDKESATUAN.EditValue), String.Empty, grdKDKESATUAN.EditValue)
                .KDPENJAMIN = IIf(String.IsNullOrEmpty(grdKDPENJAMIN.EditValue), String.Empty, grdKDPENJAMIN.EditValue)
                .KDPANGKAT = IIf(String.IsNullOrEmpty(grdKDPANGKAT.EditValue), String.Empty, grdKDPANGKAT.EditValue)
                .KDGOLONGAN = IIf(String.IsNullOrEmpty(grdKDGOLONGAN.EditValue), String.Empty, grdKDGOLONGAN.EditValue)
                .KDPENDIDIKAN = IIf(String.IsNullOrEmpty(grdKDPENDIDIKAN.EditValue), String.Empty, grdKDPENDIDIKAN.EditValue)
                .KDPEKERJAAN = IIf(String.IsNullOrEmpty(grdKDPEKERJAAN.EditValue), String.Empty, grdKDPEKERJAAN.EditValue)
                .KDAGAMA = IIf(String.IsNullOrEmpty(grdKDAGAMA.EditValue), String.Empty, grdKDAGAMA.EditValue)
                .KDJENISKELAMIN = cboJENISKELAMIN.SelectedIndex
                .KDGOLONGANDARAH = cboKDGOLONGANDARAH.SelectedIndex
                .KDSTATUSKAWIN = cboKDSTATUSKAWIN.SelectedIndex
                .KDSUKU = IIf(String.IsNullOrEmpty(grdKDSUKU.EditValue), String.Empty, grdKDSUKU.EditValue)
                .KDSTATUSHIDUP = IIf(chkSTATUSHIDUP.Checked = False, 0, 1)
                .NRP = txtNRP.Text.Trim.ToUpper
                .NAMAKELUARGA = txtNAMAKELUARGA.Text.Trim.ToUpper
                .KDSTATUSKELUARGA = IIf(String.IsNullOrEmpty(grdKDSTATUSKELUARGA.EditValue), String.Empty, grdKDSTATUSKELUARGA.EditValue)
                .TEMPATLAHIR = txtTEMPATLAHIR.Text.Trim.ToUpper
                .TANGGALLAHIR = deTANGGLLAHIR.DateTime
                .WNI = rbWNI.SelectedIndex
                .KARTUBPJS = txtKARTUBPJS.Text.Trim.ToUpper
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    'fn_Save = oCustomer.InsertData(ds, "")

                    txtKDCUSTOMER.Text = oCustomer.InsertData(ds, IIf(txtKDCUSTOMER.Text = "<--- AUTO --->", "", txtKDCUSTOMER.Text.ToString.Trim))

                    If txtKDCUSTOMER.Text = "" Then
                        fn_Save = False
                    Else
                        fn_Save = True
                    End If

                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oCustomer.UpdateData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

            'If fn_Save = True Then
            '    Dim dsCustomerSatusehat = oCustomerSatusehat.GetData(txtKDCUSTOMER.Text)
            '    If dsCustomerSatusehat Is Nothing Then
            '        If SatuSehat_Organisasi <> "" Then
            '            Dim respon As String = SatusehatAuth.PatientByNIK(SatuSehat_Production, SatuSehat_token, txtKTP.Text)

            '            If Not String.IsNullOrEmpty(respon) Then
            '                'MsgBox("✓ Respon satu sehat berhasil didapatkan!" & vbCrLf & respon, MsgBoxStyle.Information, Me.Text)

            '                If respon.Contains("Exception") Then
            '                    Dim frmPesanSatuSehat As New frmPesanSatuSehat
            '                    frmPesanSatuSehat.fn_LoadJson("✗ Exception!" & vbCrLf & respon, "Exception")
            '                    frmPesanSatuSehat.ShowDialog(Me)
            '                Else
            '                    If respon.Contains("200") Then
            '                        Dim ID As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "id")

            '                        lblIDSATUSEHAT.Text = ID

            '                        If ID = "" Then
            '                            Dim frmPesanSatuSehat As New frmPesanSatuSehat
            '                            frmPesanSatuSehat.fn_LoadJson(Regex.Replace(respon, "^.{4}", ""), respon.Substring(0, 3))
            '                            frmPesanSatuSehat.ShowDialog(Me)
            '                        Else
            '                            If fn_SaveSatuSehat(True, txtKDCUSTOMER.Text, ID, "", Regex.Replace(respon, "^.{4}", "")) = True Then
            '                                Dim frmPesanSatuSehat As New frmPesanSatuSehat
            '                                frmPesanSatuSehat.fn_LoadJson(Regex.Replace(respon, "^.{4}", ""), respon.Substring(0, 3))
            '                                frmPesanSatuSehat.ShowDialog(Me)
            '                            Else
            '                                MsgBox("✗ Gagal simpan database!" & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)
            '                            End If
            '                        End If
            '                    Else
            '                        Dim cektoken As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "code")
            '                        If cektoken = "invalid-access-token" Then
            '                            Dim token As String = SatusehatAuth.GetAccessToken(SatuSehat_Production, SatuSehat_client_id, SatuSehat_client_secret)

            '                            SatuSehat_token = SatusehatAuth.GetToken(token, "access_token")

            '                            Dim oToken As New Setting.clsSatuSehatKoneksiToken

            '                            Dim dsToken = oToken.GetDataSEQ(IIf(SatuSehat_Production = False, "SANDBOX", "PRODUCTION"))

            '                            If dsToken IsNot Nothing Then
            '                                If oToken.UpdateToken(dsToken.KDKONEKSI, dsToken.SEQ, Regex.Replace(respon, "^.{4}", ""), SatuSehat_token) = False Then
            '                                    SatuSehat_Organisasi = ""
            '                                    SatuSehat_client_id = ""
            '                                    SatuSehat_client_secret = ""

            '                                    MsgBox("✗ Gagal Simpan Token!" & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)
            '                                Else
            '                                    Dim responulang As String = SatusehatAuth.PatientByNIK(SatuSehat_Production, SatuSehat_token, txtKTP.Text)

            '                                    Dim ID As String = SatusehatAuth.GetToken(Regex.Replace(responulang, "^.{4}", ""), "id")

            '                                    lblIDSATUSEHAT.Text = ID

            '                                    If ID = "" Then
            '                                        Dim frmPesanSatuSehat As New frmPesanSatuSehat
            '                                        frmPesanSatuSehat.fn_LoadJson(Regex.Replace(responulang, "^.{4}", ""), responulang.Substring(0, 3))
            '                                        frmPesanSatuSehat.ShowDialog(Me)
            '                                    Else
            '                                        If fn_SaveSatuSehat(True, txtKDCUSTOMER.Text, ID, "", Regex.Replace(responulang, "^.{4}", "")) = True Then
            '                                            Dim frmPesanSatuSehat As New frmPesanSatuSehat
            '                                            frmPesanSatuSehat.fn_LoadJson(Regex.Replace(responulang, "^.{4}", ""), responulang.Substring(0, 3))
            '                                            frmPesanSatuSehat.ShowDialog(Me)
            '                                        Else
            '                                            MsgBox("✗ Gagal simpan database!" & vbCrLf & responulang, MsgBoxStyle.Exclamation, Me.Text)
            '                                        End If
            '                                    End If
            '                                End If
            '                            Else
            '                                SatuSehat_Organisasi = ""
            '                                SatuSehat_client_id = ""
            '                                SatuSehat_client_secret = ""

            '                                MsgBox("✗ Token Database Kosong, Silahkan Add Terlebih Dahulu di menu Setting!" & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)

            '                            End If
            '                        Else
            '                            Dim frmPesanSatuSehat As New frmPesanSatuSehat
            '                            frmPesanSatuSehat.fn_LoadJson(Regex.Replace(respon, "^.{4}", ""), respon.Substring(0, 3))
            '                            frmPesanSatuSehat.ShowDialog(Me)
            '                        End If
            '                    End If
            '                End If

            '            Else
            '                MsgBox("✗ Gagal mendapatkan respon. respon kosong", MsgBoxStyle.Information, Me.Text)
            '            End If
            '        Else
            '            MsgBox("Organisasi ID masih kosong", MsgBoxStyle.Information, Me.Text)
            '        End If
            '    End If
            'End If

            ''If fn_Save = True Then
            ''    If fn_SaveBrigging(ds.KDCUSTOMER) = False Then
            ''        MsgBox("Gagal Brigging", MsgBoxStyle.Exclamation, Me.Text)
            ''    End If
            ''End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Function fn_SaveSatuSehat(ByVal isadd As Boolean, ByVal KDCUSTOMER As String, ByVal IDSATUSEHAT As String, ByVal REQUEST As String, ByVal RESPON As String) As Boolean
        Try
            ' ***** HEADER *****

            Dim ds = oCustomerSatusehat.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oCustomerSatusehat.GetData(KDCUSTOMER).DATECREATED
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

            If isadd = True Then
                Try
                    fn_SaveSatuSehat = oCustomerSatusehat.InsertData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                Try
                    fn_SaveSatuSehat = oCustomerSatusehat.UpdateData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveSatuSehat = False
        End Try
    End Function
    'Private Sub fn_SatuSehat(ByVal tes As Boolean)
    '    Try
    '        If SatuSehat_Organisasi <> "" Then
    '            If tes = True Then
    '                If oFormMode = FORM_MODE.FORM_MODE_ADD Then
    '                    MsgBox("Silahkan Simpan data terlebih dahulu", MsgBoxStyle.Information, Me.Text)
    '                    Exit Sub
    '                End If
    '            End If

    '            Dim ds = oCustomerSatusehat.GetData(txtKDCUSTOMER.Text)

    '            If ds Is Nothing Then
    '                Dim jsonPatientData As String = ""
    '                If cboJENISKELAMIN.Text = "Perempuan" Then

    '                ElseIf cboJENISKELAMIN.Text = "Laki-laki"

    '                Else
    '                    MsgBox("Perbaiki Jenis kelamin", MsgBoxStyle.Exclamation, Me.Text)
    '                    cboJENISKELAMIN.Focus()
    '                    Exit Sub
    '                End If

    '                Dim oKelurahan As New Reference.clsKelurahan

    '                Dim dsKelurahan = oKelurahan.GetData(txtKDKELURAHAN.Text)

    '                If dsKelurahan Is Nothing Then
    '                    MsgBox("Kode kelurahan kosong", MsgBoxStyle.Exclamation, Me.Text)
    '                    Exit Sub
    '                End If

    '                Dim statuskawin_code As String = ""
    '                Dim statuskawin_name As String = ""

    '                If cboKDSTATUSKAWIN.Text = "BELUM MENIKAH" Then
    '                    statuskawin_code = "U"
    '                    statuskawin_name = "unmarried"
    '                ElseIf cboKDSTATUSKAWIN.Text = "MENIKAH"
    '                    statuskawin_code = "M"
    '                    statuskawin_name = "Married"
    '                ElseIf cboKDSTATUSKAWIN.Text = "JANDA"
    '                    statuskawin_code = "W"
    '                    statuskawin_name = "Widowed"
    '                ElseIf cboKDSTATUSKAWIN.Text = "DUDA"
    '                    statuskawin_code = "D"
    '                    statuskawin_name = "Divorced"
    '                Else
    '                    statuskawin_code = "S"
    '                    statuskawin_name = "Never Married"
    '                End If

    '                jsonPatientData = SatusehatAuth.BuildPatientJSON(txtKTP.Text, txtNAME_DISPLAY.Text, IIf(cboJENISKELAMIN.Text = "Perempuan", "female", "male"), deTANGGLLAHIR.DateTime.ToString("yyyy-MM-dd"), txtBILL_STREET.Text, txtOTHER.Text, txtBILL_ZIP.Text, dsKelurahan.M_KECAMATAN.M_KABUPATEN.KDPROPINSI, dsKelurahan.M_KECAMATAN.M_KABUPATEN.KDKABUPATEN, dsKelurahan.M_KECAMATAN.KDKECAMATAN, dsKelurahan.KDKELURAHAN, txtWEBSITE.Text, txtFAX.Text, txtNAME_DISPLAY.Text, txtPHONE.Text, statuskawin_code, statuskawin_name)

    '                Dim respon As String = SatusehatAuth.CreatePatient(SatuSehat_Production, SatuSehat_token, jsonPatientData)

    '                If respon.Contains("Exception") Then
    '                    Dim frmPesanSatuSehat As New frmPesanSatuSehat
    '                    frmPesanSatuSehat.fn_LoadJson("✗ Exception!" & vbCrLf & respon, "Exception")
    '                    frmPesanSatuSehat.ShowDialog(Me)
    '                Else
    '                    If Not String.IsNullOrEmpty(respon) Then

    '                        If respon.Contains("200") Or respon.Contains("400") Then

    '                            Dim ID As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "id")

    '                            lblIDSATUSEHAT.Text = ID

    '                            If ID = "" Then
    '                                Dim frmPesanSatuSehat As New frmPesanSatuSehat
    '                                frmPesanSatuSehat.fn_LoadJson(Regex.Replace(respon, "^.{4}", ""), respon.Substring(0, 3))
    '                                frmPesanSatuSehat.ShowDialog(Me)
    '                                'MsgBox("✗ Gagal simpan database id kosong!" & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)
    '                            Else
    '                                If fn_SaveSatuSehat(True, txtKDCUSTOMER.Text, ID, jsonPatientData, Regex.Replace(respon, "^.{4}", "")) = True Then
    '                                    MsgBox("✓ Berhasil simpan database!" & vbCrLf & respon, MsgBoxStyle.Information, Me.Text)
    '                                Else
    '                                    MsgBox("✗ Gagal simpan database!" & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)
    '                                End If
    '                            End If
    '                        Else
    '                            Dim frmPesanSatuSehat As New frmPesanSatuSehat
    '                            frmPesanSatuSehat.fn_LoadJson(Regex.Replace(respon, "^.{4}", ""), respon.Substring(0, 3))
    '                            frmPesanSatuSehat.ShowDialog(Me)

    '                            'MsgBox("✓ Respon satu sehat berhasil didapatkan!" & vbCrLf & respon, MsgBoxStyle.Information, Me.Text)
    '                        End If
    '                    Else
    '                        MsgBox("✗ Gagal mendapatkan respon. respon kosong", MsgBoxStyle.Exclamation, Me.Text)
    '                    End If
    '                End If

    '            Else
    '                Dim jsonPatientData As String = ""
    '                If cboJENISKELAMIN.Text = "Perempuan" Then

    '                ElseIf cboJENISKELAMIN.Text = "Laki-laki"

    '                Else
    '                    MsgBox("Perbaiki Jenis kelamin", MsgBoxStyle.Exclamation, Me.Text)
    '                    cboJENISKELAMIN.Focus()
    '                    Exit Sub
    '                End If

    '                Dim oKelurahan As New Reference.clsKelurahan

    '                Dim dsKelurahan = oKelurahan.GetData(txtKDKELURAHAN.Text)

    '                If dsKelurahan Is Nothing Then
    '                    MsgBox("Kode kelurahan kosong", MsgBoxStyle.Exclamation, Me.Text)
    '                    Exit Sub
    '                End If

    '                Dim statuskawin_code As String = ""
    '                Dim statuskawin_name As String = ""

    '                If cboKDSTATUSKAWIN.Text = "BELUM MENIKAH" Then
    '                    statuskawin_code = "U"
    '                    statuskawin_name = "unmarried"
    '                ElseIf cboKDSTATUSKAWIN.Text = "MENIKAH"
    '                    statuskawin_code = "M"
    '                    statuskawin_name = "Married"
    '                ElseIf cboKDSTATUSKAWIN.Text = "JANDA"
    '                    statuskawin_code = "W"
    '                    statuskawin_name = "Widowed"
    '                ElseIf cboKDSTATUSKAWIN.Text = "DUDA"
    '                    statuskawin_code = "D"
    '                    statuskawin_name = "Divorced"
    '                Else
    '                    statuskawin_code = "S"
    '                    statuskawin_name = "Never Married"
    '                End If

    '                jsonPatientData = SatusehatAuth.PatchBuildPatientJSON(txtKTP.Text, txtNAME_DISPLAY.Text, IIf(cboJENISKELAMIN.Text = "Perempuan", "female", "male"), deTANGGLLAHIR.DateTime.ToString("yyyy-MM-dd"), txtBILL_STREET.Text, txtOTHER.Text, txtBILL_ZIP.Text, dsKelurahan.M_KECAMATAN.M_KABUPATEN.KDPROPINSI, dsKelurahan.M_KECAMATAN.M_KABUPATEN.KDKABUPATEN, dsKelurahan.M_KECAMATAN.KDKECAMATAN, dsKelurahan.KDKELURAHAN, txtWEBSITE.Text, txtFAX.Text, txtNAME_DISPLAY.Text, txtPHONE.Text, statuskawin_code, statuskawin_name, ds.IDSATUSEHAT)

    '                Dim respon As String = SatusehatAuth.PatchPatient(SatuSehat_Production, SatuSehat_token, ds.IDSATUSEHAT, jsonPatientData)

    '                If respon.Contains("Exception") Then
    '                    Dim frmPesanSatuSehat As New frmPesanSatuSehat
    '                    frmPesanSatuSehat.fn_LoadJson("✗ Exception!" & vbCrLf & respon, "Exeption")
    '                    frmPesanSatuSehat.ShowDialog(Me)
    '                Else
    '                    If Not String.IsNullOrEmpty(respon) Then

    '                        If respon.Contains("200") Or respon.Contains("400") Then

    '                            Dim ID As String = SatusehatAuth.GetToken(Regex.Replace(respon, "^.{4}", ""), "id")

    '                            lblIDSATUSEHAT.Text = ID

    '                            If ID = "" Then
    '                                Dim frmPesanSatuSehat As New frmPesanSatuSehat
    '                                frmPesanSatuSehat.fn_LoadJson(Regex.Replace(respon, "^.{4}", ""), respon.Substring(0, 3))
    '                                frmPesanSatuSehat.ShowDialog(Me)
    '                                'MsgBox("✗ Gagal simpan database id kosong!" & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)
    '                            Else
    '                                If fn_SaveSatuSehat(False, txtKDCUSTOMER.Text, ID, jsonPatientData, Regex.Replace(respon, "^.{4}", "")) = True Then
    '                                    MsgBox("✓ Berhasil simpan database!" & vbCrLf & respon, MsgBoxStyle.Information, Me.Text)
    '                                Else
    '                                    MsgBox("✗ Gagal simpan database!" & vbCrLf & respon, MsgBoxStyle.Exclamation, Me.Text)
    '                                End If
    '                            End If
    '                        Else
    '                            Dim frmPesanSatuSehat As New frmPesanSatuSehat
    '                            frmPesanSatuSehat.fn_LoadJson(Regex.Replace(respon, "^.{4}", ""), respon.Substring(0, 3))
    '                            frmPesanSatuSehat.ShowDialog(Me)

    '                            'MsgBox("✓ Respon satu sehat berhasil didapatkan!" & vbCrLf & respon, MsgBoxStyle.Information, Me.Text)
    '                        End If
    '                    Else
    '                        MsgBox("✗ Gagal mendapatkan respon. respon kosong", MsgBoxStyle.Exclamation, Me.Text)
    '                    End If
    '                End If
    '            End If
    '        Else
    '            If tes = True Then
    '                MsgBox("Organisasi ID masih kosong", MsgBoxStyle.Exclamation, Me.Text)
    '            End If
    '        End If
    '    Catch oErr As Exception
    '        MsgBox("Error Simpan NIK Ke Satu Sehat" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
#End Region
#Region "Grid Method"

#End Region
#Region "Command Button"
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
            sStatusSave = "NEW"
            Me.Close()
        End If
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnReporCustomer_Click() Handles btnReporCustomer.ItemClick
        Dim frmReportCustomer As New frmReportCustomer
        Try
            frmReportCustomer.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmReportCustomer Is Nothing Then frmReportCustomer.Dispose()
            frmReportCustomer = Nothing
        End Try
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
    Private Sub btnAddPekerjaan_Click(sender As Object, e As EventArgs) Handles btnAddPekerjaan.Click
        frmPekerjaan.LoadMe(FORM_MODE.FORM_MODE_ADD)
        frmPekerjaan.ShowDialog(Me)
        fn_LoadKDPEKERJAAN()

        Dim oPekerjaan As New Reference.clsPekerjaan

        If sCode = String.Empty Then Exit Sub

        grdKDPEKERJAAN.Text = oPekerjaan.GetData.FirstOrDefault(Function(x) x.MEMO = sCode).KDPEKERJAAN

    End Sub
    Private Sub btnAddKesatuan_Click(sender As Object, e As EventArgs) Handles btnAddKesatuan.Click
        frmKesatuan.LoadMe(FORM_MODE.FORM_MODE_ADD)
        frmKesatuan.ShowDialog(Me)
        fn_LoadKDKESATUAN()

        Dim oKesatuan As New Reference.clsKesatuan

        If sCode = String.Empty Then Exit Sub

        grdKDKESATUAN.Text = oKesatuan.GetData.FirstOrDefault(Function(x) x.MEMO = sCode).KDKESATUAN

    End Sub
    Private Sub btnAddPenjamin_Click(sender As Object, e As EventArgs) Handles btnAddPenjamin.Click
        frmPenjamin.LoadMe(FORM_MODE.FORM_MODE_ADD)
        frmPenjamin.ShowDialog(Me)
        fn_LoadKDPENJAMIN()

        Dim oPenjamin As New Reference.clsPenjamin

        If sCode = String.Empty Then Exit Sub

        grdKDPENJAMIN.Text = oPenjamin.GetData.FirstOrDefault(Function(x) x.MEMO = sCode).KDPENJAMIN

    End Sub
    Private Sub btnAddPerusahaan_Click(sender As Object, e As EventArgs) Handles btnAddPerusahaan.Click
        frmPerusahaan.LoadMe(FORM_MODE.FORM_MODE_ADD)
        frmPerusahaan.ShowDialog(Me)
        fn_LoadKDPERUSAHAAN()

        Dim oPerusahaan As New Reference.clsPerusahaan

        If sCode = String.Empty Then Exit Sub

        grdKDPERUSAHAAN.Text = oPerusahaan.GetData.FirstOrDefault(Function(x) x.MEMO = sCode).KDPERUSAHAAN

    End Sub
    Private Sub btnAddStatusKeluarga_Click(sender As Object, e As EventArgs) Handles btnAddStatusKeluarga.Click
        frmStatusKeluarga.LoadMe(FORM_MODE.FORM_MODE_ADD)
        frmStatusKeluarga.ShowDialog(Me)
        fn_LoadKDSTATUSKELUARGA()

        Dim oStatusKeluarga As New Reference.clsStatusKeluarga

        If sCode = String.Empty Then Exit Sub

        grdKDSTATUSKELUARGA.Text = oStatusKeluarga.GetData.FirstOrDefault(Function(x) x.MEMO = sCode).KDSTATUSKELUARGA

    End Sub
    Private Sub btnAddSuku_Click(sender As Object, e As EventArgs) Handles btnAddSuku.Click
        frmSuku.LoadMe(FORM_MODE.FORM_MODE_ADD)
        frmSuku.ShowDialog(Me)
        fn_LoadKDSUKU()

        Dim oSuku As New Reference.clsSuku

        If sCode = String.Empty Then Exit Sub

        grdKDSUKU.Text = oSuku.GetData.FirstOrDefault(Function(x) x.MEMO = sCode).KDSUKU

    End Sub
    Private Sub btnAddPangkat_Click(sender As Object, e As EventArgs) Handles btnAddPangkat.Click
        frmPangkat.LoadMe(FORM_MODE.FORM_MODE_ADD)
        frmPangkat.ShowDialog(Me)
        fn_LoadKDPANGKAT()

        Dim oPangkat As New Reference.clsPangkat

        If sCode = String.Empty Then Exit Sub

        grdKDPANGKAT.Text = oPangkat.GetData.FirstOrDefault(Function(x) x.MEMO = sCode).KDPANGKAT

    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadKDKESATUAN()
        Dim oKesatuan As New Reference.clsKesatuan
        Try
            grdKDKESATUAN.Properties.DataSource = oKesatuan.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDKESATUAN.Properties.ValueMember = "KDKESATUAN"
            grdKDKESATUAN.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDKESATUAN_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdKDKESATUAN.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdKDKESATUAN.ResetText()
        End If
    End Sub
    Private Sub fn_LoadKDPANGKAT()
        Dim oPangkat As New Reference.clsPangkat
        Try
            grdKDPANGKAT.Properties.DataSource = oPangkat.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDPANGKAT.Properties.ValueMember = "KDPANGKAT"
            grdKDPANGKAT.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDPANGKAT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdKDPANGKAT.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdKDPANGKAT.ResetText()
        End If
    End Sub
    Private Sub fn_LoadKDPERUSAHAAN()
        Dim oPerusahaan As New Reference.clsPerusahaan
        Try
            grdKDPERUSAHAAN.Properties.DataSource = oPerusahaan.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDPERUSAHAAN.Properties.ValueMember = "KDPERUSAHAAN"
            grdKDPERUSAHAAN.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDPERUSAHAAN_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdKDPERUSAHAAN.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdKDPERUSAHAAN.ResetText()
        End If
    End Sub
    Private Sub fn_LoadKDGOLONGAN()
        Dim oGolongan As New Reference.clsGolongan
        Try
            grdKDGOLONGAN.Properties.DataSource = oGolongan.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDGOLONGAN.Properties.ValueMember = "KDGOLONGAN"
            grdKDGOLONGAN.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDGOLONGAN_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdKDGOLONGAN.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdKDGOLONGAN.ResetText()
        End If
    End Sub
    Private Sub fn_LoadKDPENDIDIKAN()
        Dim oPendidikan As New Reference.clsPendidikan
        Try
            grdKDPENDIDIKAN.Properties.DataSource = oPendidikan.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDPENDIDIKAN.Properties.ValueMember = "KDPENDIDIKAN"
            grdKDPENDIDIKAN.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDPENDIDIKAN_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdKDPENDIDIKAN.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdKDPENDIDIKAN.ResetText()
        End If
    End Sub
    Private Sub fn_LoadKDPEKERJAAN()
        Dim oPekerjaan As New Reference.clsPekerjaan
        Try
            grdKDPEKERJAAN.Properties.DataSource = oPekerjaan.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDPEKERJAAN.Properties.ValueMember = "KDPEKERJAAN"
            grdKDPEKERJAAN.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDPEKERJAAN_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdKDPEKERJAAN.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdKDPEKERJAAN.ResetText()
        End If
    End Sub
    Private Sub fn_LoadKDPENJAMIN()
        Dim oPenjamin As New Reference.clsPenjamin
        Try
            grdKDPENJAMIN.Properties.DataSource = oPenjamin.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDPENJAMIN.Properties.ValueMember = "KDPENJAMIN"
            grdKDPENJAMIN.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDPENJAMIN_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdKDPENJAMIN.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdKDPENJAMIN.ResetText()
        End If
    End Sub
    Private Sub fn_LoadKDAGAMA()
        Dim oAgama As New Reference.clsAgama
        Try
            grdKDAGAMA.Properties.DataSource = oAgama.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDAGAMA.Properties.ValueMember = "KDAGAMA"
            grdKDAGAMA.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDAGAMA_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdKDAGAMA.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdKDAGAMA.ResetText()
        End If
    End Sub
    Private Sub fn_LoadKDSUKU()
        Dim oSuku As New Reference.clsSuku
        Try
            grdKDSUKU.Properties.DataSource = oSuku.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDSUKU.Properties.ValueMember = "KDSUKU"
            grdKDSUKU.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDSUKU_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdKDSUKU.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdKDSUKU.ResetText()
        End If
    End Sub
    Private Sub fn_LoadKDSTATUSKELUARGA()
        Dim oStatusKeluarga As New Reference.clsStatusKeluarga
        Try
            grdKDSTATUSKELUARGA.Properties.DataSource = oStatusKeluarga.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDSTATUSKELUARGA.Properties.ValueMember = "KDSTATUSKELUARGA"
            grdKDSTATUSKELUARGA.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDSTATUSKELUARGA_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdKDSTATUSKELUARGA.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdKDSTATUSKELUARGA.ResetText()
        End If
    End Sub
    'Private Sub fn_LoadKDKELURAHAN()
    '    Dim oKelurahan As New Reference.clsKelurahan

    '    Dim dsKelurahan = From a In oKelurahan.GetData()
    '                      Select a.KDKELURAHAN, KELURAHAN = a.MEMO, KECAMATAN = a.M_KECAMATAN.MEMO, KABUPATEN = a.M_KECAMATAN.M_KABUPATEN.MEMO, PROPINSI = a.M_KECAMATAN.M_KABUPATEN.M_PROPINSI.MEMO, a.ISACTIVE
    '                      Where ISACTIVE = True
    '    Try
    '        grdKDKELURAHAN.Properties.DataSource = dsKelurahan.ToList()
    '        grdKDKELURAHAN.Properties.ValueMember = "KDKELURAHAN"
    '        grdKDKELURAHAN.Properties.DisplayMember = "KELURAHAN"
    '    Catch oErr As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
    'Private Sub grdKDKELURAHAN_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs)
    '    If e.KeyCode = Keys.Delete Then
    '        grdKDSTATUSKELUARGA.ResetText()
    '    End If
    'End Sub
    Private Sub fn_LoadCariPropinsi(ByVal kdkulrahan As String)
        Dim oKelurahan As New Reference.clsKelurahan

        Dim dsKelurahan = oKelurahan.GetData(kdkulrahan)

        If dsKelurahan IsNot Nothing Then
            txtKDKELURAHAN.Text = dsKelurahan.KDKELURAHAN
            txtKELURAHAN.Text = dsKelurahan.MEMO
            txtKECAMATAN.Text = dsKelurahan.M_KECAMATAN.MEMO
            txtKABUPATEN.Text = dsKelurahan.M_KECAMATAN.M_KABUPATEN.MEMO
            txtPROPINSI.Text = dsKelurahan.M_KECAMATAN.M_KABUPATEN.M_PROPINSI.MEMO
            txtBILL_ZIP.Text = dsKelurahan.KODEPOS
        Else
            txtKDKELURAHAN.ResetText()
            txtKELURAHAN.ResetText()
            txtKECAMATAN.ResetText()
            txtKABUPATEN.ResetText()
            txtPROPINSI.ResetText()
            txtBILL_ZIP.ResetText()
        End If
    End Sub
    'Private Sub grdKDKELURAHAN_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs)
    '    'If grdKDKELURAHAN.Text = String.Empty Then Exit Sub
    '    fn_LoadCariPropinsi(grdKDKELURAHAN.EditValue)
    'End Sub
    Private Sub rbWNI_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rbWNI.SelectedIndexChanged
        If rbWNI.SelectedIndex = 0 Then
            txtBILL_COUNTRY.Text = "INDONESIA"
        Else
            txtBILL_COUNTRY.Text = ""
        End If
    End Sub
    Private Sub txtKARTUBPJS_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtKARTUBPJS.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If txtKARTUBPJS.Text = String.Empty Then
                Exit Sub
            End If

            If txtKARTUBPJS.Text.Count = 13 Then
                Try
                    Dim oSetKoneksi As New Brigging.clsSetKoneksi
                    Dim uTime As Integer = 0

                    If sVclaim_ConsId <> "" Then
                        uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                        Dim dsSetKoneksi = oSetKoneksi.GetDataVClaimPesertaNoKartuBPJS(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, txtKARTUBPJS.Text, Now.ToString("yyyy-MM-dd"))

                        If dsSetKoneksi <> "" Then
                            Try
                                Dim allData = JObject.Parse(dsSetKoneksi)

                                Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), sVclaim_ConsId & sVclaim_SecreatKey & uTime))

                                MsgBox("Jenis Peserta : " & DataDecrypt("peserta")("jenisPeserta")("keterangan").ToString() _
                                                  & vbCrLf _
                                                  & "No RM : " & DataDecrypt("peserta")("mr")("noMR").ToString() _
                                                  & vbCrLf _
                                                  & "No Telepon : " & DataDecrypt("peserta")("mr")("noTelepon").ToString() _
                                                  & vbCrLf _
                                                  & "Nama : " & DataDecrypt("peserta")("nama").ToString() _
                                                  & vbCrLf _
                                                  & "Tgl Cetak Kartu : " & DataDecrypt("peserta")("tglCetakKartu").ToString() _
                                                  & vbCrLf _
                                                  & "Umur Sekarang : " & DataDecrypt("peserta")("umur")("umurSekarang").ToString() _
                                                  & vbCrLf _
                                                  & "Tgl TAT : " & DataDecrypt("peserta")("tglTAT").ToString() _
                                                  & vbCrLf _
                                                  & "Tgl TMT : " & DataDecrypt("peserta")("tglTMT").ToString() _
                                                  & vbCrLf _
                                                  & "Status Peserta : " & DataDecrypt("peserta")("statusPeserta")("keterangan").ToString() _
                                                  & vbCrLf _
                                                  & vbCrLf _
                                                  & "Jenis Pasien : " & IIf(DataDecrypt("peserta")("mr")("noMR").ToString.Trim.ToUpper() = "", "PASIEN BARU", "PASIEN LAMA") _
                                                  , MsgBoxStyle.Information, Me.Text)

                                If DataDecrypt("peserta")("mr")("noMR").ToString.Trim.ToUpper() <> "" Then
                                    If txtKDCUSTOMER.Text = "" Or txtKDCUSTOMER.Text = "<--- AUTO --->" Then
                                        txtKDCUSTOMER.Text = DataDecrypt("peserta")("mr")("noMR").ToString.Trim.ToUpper()
                                    End If
                                End If

                                Dim jenisPeserta As String = DataDecrypt("peserta")("jenisPeserta")("keterangan").ToString()
                                Dim kodejenisPeserta As String = DataDecrypt("peserta")("jenisPeserta")("kode").ToString()

                                'If jenisPeserta <> "" Then
                                '    InsertJenisPeserta(kodejenisPeserta, jenisPeserta)
                                'End If

                                txtJENISPESERTA.Text = jenisPeserta
                                txtNAME_DISPLAY.Text = DataDecrypt("peserta")("nama").ToString.Trim.ToUpper()
                                txtPHONE.Text = DataDecrypt("peserta")("mr")("noTelepon").ToString.Trim.ToUpper()
                                txtKTP.Text = DataDecrypt("peserta")("nik").ToString()
                                deTANGGLLAHIR.DateTime = DataDecrypt("peserta")("tglLahir").ToString()
                                cboJENISKELAMIN.SelectedIndex = IIf(DataDecrypt("peserta")("sex").ToString() = "L", 1, 0)

                            Catch oErr As Exception
                                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message & vbCrLf & vbCrLf & "Result : " & vbCrLf & dsSetKoneksi, MsgBoxStyle.Exclamation, Me.Text)
                            End Try
                        End If
                    Else
                        MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
                    End If

                    'txtVCLAIM_KDDOCTOR.ResetText()

                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf txtKARTUBPJS.Text.Count > 13 Then
                MsgBox(Statement.ErrorStatement & "No Kartu BPJS > 13 Digit", MsgBoxStyle.Exclamation, Me.Text)
            Else
                MsgBox(Statement.ErrorStatement & "Kartu BPJS < 13 Digit", MsgBoxStyle.Exclamation, Me.Text)
            End If
        End If
    End Sub
    Private Sub txtKTP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtKTP.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If txtKTP.Text = String.Empty Then
                Exit Sub
            End If

            If txtKTP.Text.Count = 16 Then
                Try
                    Dim oSetKoneksi As New Brigging.clsSetKoneksi
                    Dim uTime As Integer = 0

                    If sVclaim_ConsId <> "" Then
                        uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                        Dim dsSetKoneksi = oSetKoneksi.GetDataVClaimPesertaNIK(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, txtKTP.Text, Now.ToString("yyyy-MM-dd"))

                        If dsSetKoneksi <> "" Then
                            Try
                                Dim allData = JObject.Parse(dsSetKoneksi)

                                Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), sVclaim_ConsId & sVclaim_SecreatKey & uTime))

                                MsgBox("Jenis Peserta : " & DataDecrypt("peserta")("jenisPeserta")("keterangan").ToString() _
                                                  & vbCrLf _
                                                  & "No RM : " & DataDecrypt("peserta")("mr")("noMR").ToString() _
                                                  & vbCrLf _
                                                  & "No Telepon : " & DataDecrypt("peserta")("mr")("noTelepon").ToString() _
                                                  & vbCrLf _
                                                  & "Nama : " & DataDecrypt("peserta")("nama").ToString() _
                                                  & vbCrLf _
                                                  & "Tgl Cetak Kartu : " & DataDecrypt("peserta")("tglCetakKartu").ToString() _
                                                  & vbCrLf _
                                                  & "Umur Sekarang : " & DataDecrypt("peserta")("umur")("umurSekarang").ToString() _
                                                  & vbCrLf _
                                                  & "Tgl TAT : " & DataDecrypt("peserta")("tglTAT").ToString() _
                                                  & vbCrLf _
                                                  & "Tgl TMT : " & DataDecrypt("peserta")("tglTMT").ToString() _
                                                  & vbCrLf _
                                                  & "Status Peserta : " & DataDecrypt("peserta")("statusPeserta")("keterangan").ToString() _
                                                  & vbCrLf _
                                                  & vbCrLf _
                                                  & "Jenis Pasien : " & IIf(DataDecrypt("peserta")("mr")("noMR").ToString.Trim.ToUpper() = "", "PASIEN BARU", "PASIEN LAMA") _
                                                  , MsgBoxStyle.Information, Me.Text)

                                If DataDecrypt("peserta")("mr")("noMR").ToString.Trim.ToUpper() <> "" Then
                                    If txtKDCUSTOMER.Text = "" Or txtKDCUSTOMER.Text = "<--- AUTO --->" Then
                                        txtKDCUSTOMER.Text = DataDecrypt("peserta")("mr")("noMR").ToString.Trim.ToUpper()
                                    End If
                                End If

                                Dim jenisPeserta As String = DataDecrypt("peserta")("jenisPeserta")("keterangan").ToString()
                                Dim kodejenisPeserta As String = DataDecrypt("peserta")("jenisPeserta")("kode").ToString()

                                'If jenisPeserta <> "" Then
                                '    InsertJenisPeserta(kodejenisPeserta, jenisPeserta)
                                'End If

                                txtJENISPESERTA.Text = jenisPeserta
                                txtNAME_DISPLAY.Text = DataDecrypt("peserta")("nama").ToString.Trim.ToUpper()
                                txtPHONE.Text = DataDecrypt("peserta")("mr")("noTelepon").ToString.Trim.ToUpper()
                                txtKARTUBPJS.Text = DataDecrypt("peserta")("noKartu").ToString()
                                deTANGGLLAHIR.DateTime = DataDecrypt("peserta")("tglLahir").ToString()
                                cboJENISKELAMIN.SelectedIndex = IIf(DataDecrypt("peserta")("sex").ToString() = "L", 1, 0)

                            Catch oErr As Exception
                                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message & vbCrLf & vbCrLf & "Result : " & vbCrLf & dsSetKoneksi, MsgBoxStyle.Exclamation, Me.Text)
                            End Try
                        End If
                    Else
                        MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
                    End If

                    'txtVCLAIM_KDDOCTOR.ResetText()

                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf txtKARTUBPJS.Text.Count > 16 Then
                MsgBox(Statement.ErrorStatement & "No NIK > 16 Digit", MsgBoxStyle.Exclamation, Me.Text)
            Else
                MsgBox(Statement.ErrorStatement & "No NIK < 16 Digit", MsgBoxStyle.Exclamation, Me.Text)
            End If
        End If
    End Sub
    Private Sub btnCariWilayah_Click(sender As Object, e As EventArgs) Handles btnCariWilayah.Click
        Dim frmBrowseWilayah As New frmBrowseWilayah
        Try
            frmBrowseWilayah.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmBrowseWilayah Is Nothing Then frmBrowseWilayah.Dispose()
            frmBrowseWilayah = Nothing

            If sFind1 <> "" Then
                fn_LoadCariPropinsi(sFind1)
            End If

        End Try
    End Sub
    Private Sub btnPatientByID_ItemClick() Handles btnPatientByID.ItemClick
        Try
            If lblIDSATUSEHAT.Text = "-" Or lblIDSATUSEHAT.Text = "" Then
                Exit Sub
            End If

            If SatuSehat_Organisasi <> "" Then
                Dim respon As String = SatusehatAuth.PatientByID(SatuSehat_Production, SatuSehat_token, lblIDSATUSEHAT.Text)

                If Not String.IsNullOrEmpty(respon) Then
                    'MsgBox("✓ Respon satu sehat berhasil didapatkan!" & vbCrLf & respon, MsgBoxStyle.Information, Me.Text)

                    If respon.Contains("Exception") Then
                        Dim frmPesanSatuSehat As New frmPesanSatuSehat
                        frmPesanSatuSehat.fn_LoadJson("✗ Exception!" & vbCrLf & respon, "Exception")
                        frmPesanSatuSehat.ShowDialog(Me)
                    Else
                        Dim frmPesanSatuSehat As New frmPesanSatuSehat
                        frmPesanSatuSehat.fn_LoadJson(Regex.Replace(respon, "^.{4}", ""), respon.Substring(0, 3))
                        frmPesanSatuSehat.ShowDialog(Me)
                    End If
                Else
                    MsgBox("✗ Gagal mendapatkan respon. respon kosong", MsgBoxStyle.Information, Me.Text)
                End If
            Else
                MsgBox("Organisasi ID masih kosong", MsgBoxStyle.Information, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnPatientByNIK_ItemClick() Handles btnPatientByNIK.ItemClick
        Try
            If txtKTP.Text = "" Then
                Exit Sub
            End If

            If SatuSehat_Organisasi <> "" Then
                Dim respon As String = SatusehatAuth.PatientByNIK(SatuSehat_Production, SatuSehat_token, txtKTP.Text)

                If Not String.IsNullOrEmpty(respon) Then
                    'MsgBox("✓ Respon satu sehat berhasil didapatkan!" & vbCrLf & respon, MsgBoxStyle.Information, Me.Text)

                    If respon.Contains("Exception") Then
                        Dim frmPesanSatuSehat As New frmPesanSatuSehat
                        frmPesanSatuSehat.fn_LoadJson("✗ Exception!" & vbCrLf & respon, "Exception")
                        frmPesanSatuSehat.ShowDialog(Me)
                    Else
                        Dim frmPesanSatuSehat As New frmPesanSatuSehat
                        frmPesanSatuSehat.fn_LoadJson(Regex.Replace(respon, "^.{4}", ""), respon.Substring(0, 3))
                        frmPesanSatuSehat.ShowDialog(Me)
                    End If

                Else
                    MsgBox("✗ Gagal mendapatkan respon. respon kosong", MsgBoxStyle.Information, Me.Text)
                End If
            Else
                MsgBox("Organisasi ID masih kosong", MsgBoxStyle.Information, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnPatientSearchNameBirthdateGender_ItemClick() Handles btnPatientSearchNameBirthdateGender.ItemClick
        Try
            If txtKTP.Text = "" Then
                Exit Sub
            End If

            If SatuSehat_Organisasi <> "" Then
                Dim respon As String = SatusehatAuth.PatientSearchNameirthdateGender(SatuSehat_Production, SatuSehat_token, txtNAME_DISPLAY.Text, deTANGGLLAHIR.DateTime.ToString("yyyy-MM-dd"), IIf(cboJENISKELAMIN.Text = "Perempuan", "female", "male"))

                If Not String.IsNullOrEmpty(respon) Then
                    'MsgBox("✓ Respon satu sehat berhasil didapatkan!" & vbCrLf & respon, MsgBoxStyle.Information, Me.Text)

                    If respon.Contains("Exception") Then
                        Dim frmPesanSatuSehat As New frmPesanSatuSehat
                        frmPesanSatuSehat.fn_LoadJson("✗ Exception!" & vbCrLf & respon, "Exception")
                        frmPesanSatuSehat.ShowDialog(Me)
                    Else
                        Dim frmPesanSatuSehat As New frmPesanSatuSehat
                        frmPesanSatuSehat.fn_LoadJson(Regex.Replace(respon, "^.{4}", ""), respon.Substring(0, 3))
                        frmPesanSatuSehat.ShowDialog(Me)
                    End If
                Else
                    MsgBox("✗ Gagal mendapatkan respon. respon kosong", MsgBoxStyle.Information, Me.Text)
                End If
            Else
                MsgBox("Organisasi ID masih kosong", MsgBoxStyle.Information, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region

End Class