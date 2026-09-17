Imports DataAccess
Imports System.Linq
Imports Newtonsoft.Json.Linq

Public Class frmAntrianSKDOnline
    'Private sKODEBOOKING As String = String.Empty
    Private sKDPENJAMIN As String = String.Empty

    Public Sub fn_LoadMeAuto(ByVal Kategori As Boolean)
        If Kategori = False Then
            cboASALRUJUKAN.Properties.ReadOnly = Kategori
        Else
            cboASALRUJUKAN.Properties.ReadOnly = Kategori
            cboASALRUJUKAN.SelectedIndex = 2
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_LoadPOLI()
        fn_LoadDOKTER()
        txtSEARCH.Focus()
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub txtSEARCH_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSEARCH.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If fn_CariBookingAntrian(txtSEARCH.Text.ToString.Trim) = False Then
                txtSEARCH.ResetText()
            End If
        End If
    End Sub
    Private Sub btnEnter_Click(sender As Object, e As EventArgs) Handles btnEnter.Click
        If fn_CariBookingAntrian(txtSEARCH.Text.ToString.Trim) = False Then
            txtSEARCH.ResetText()
        End If
    End Sub
    Private Function fn_CariBookingAntrian(ByVal Parameter As String) As Boolean
        Try
            txtKODEBOOKING.ResetText()

            Dim oAntrian As New SettingAntrian.clsSetAntrian
            Dim dsAntrian = oAntrian.GetData(Parameter)
            If dsAntrian IsNot Nothing Then

                txtKODEBOOKING.Text = dsAntrian.KODEBOOKING

                'sKODEBOOKING = dsAntrian.KODEBOOKING
                fn_CariBookingAntrian = True
                Dim oCustomer As New Reference.clsCustomer
                txtJENISPASIEN.Text = dsAntrian.JENISPASIEN
                txtKARTUBPJS.Text = dsAntrian.NOMORKARTU
                txtNIK.Text = dsAntrian.NIK
                txtKDCUSTOMER.Text = dsAntrian.NORM
                Dim dsCustomer = oCustomer.GetData(dsAntrian.NORM.ToString.PadLeft(9, "0"))
                If dsCustomer IsNot Nothing Then
                    txtNAMAPASIEN.Text = dsCustomer.NAME_DISPLAY
                End If
                txtNOTELEPON.Text = dsAntrian.NOHP
                txtNOMORREFERNSI.Text = dsAntrian.NOMORREFERENSI
                Dim oPoli As New Reference.clsDepartment
                Dim oDokter As New Reference.clsDoctor
                Dim dsPoli = oPoli.GetDataByKodeVclaim(dsAntrian.KODEPOLI)
                If dsPoli IsNot Nothing Then
                    grdKDDEPARTMENT.Text = dsPoli.KDDEPARTMENT
                End If
                Dim dsDokter = oDokter.GetDataByKodeVclaim(dsAntrian.KODEDOKTER)
                If dsDokter IsNot Nothing Then
                    grdKDDOCTOR.Text = dsDokter.KDDOCTOR
                End If

                'If dsAntrian.TANGGALPERIKSA_TEXT = Now.ToString("ddMMyyyy") Then
                '    'sKODEBOOKING = dsAntrian.KODEBOOKING
                '    fn_CariBookingAntrian = True
                '    Dim oCustomer As New Reference.clsCustomer
                '    txtJENISPASIEN.Text = dsAntrian.JENISPASIEN
                '    txtKARTUBPJS.Text = dsAntrian.NOMORKARTU
                '    txtNIK.Text = dsAntrian.NIK
                '    txtKDCUSTOMER.Text = dsAntrian.NORM
                '    Dim dsCustomer = oCustomer.GetData(dsAntrian.NORM.ToString.PadLeft(9, "0"))
                '    If dsCustomer IsNot Nothing Then
                '        txtNAMAPASIEN.Text = dsCustomer.NAME_DISPLAY
                '    End If
                '    txtNOTELEPON.Text = dsAntrian.NOHP
                '    txtNOMORREFERNSI.Text = dsAntrian.NOMORREFERENSI
                '    Dim oPoli As New Reference.clsDepartment
                '    Dim oDokter As New Reference.clsDoctor
                '    Dim dsPoli = oPoli.GetDataByKodeVclaim(dsAntrian.KODEPOLI)
                '    If dsPoli IsNot Nothing Then
                '        grdKDDEPARTMENT.Text = dsPoli.KDDEPARTMENT
                '    End If
                '    Dim dsDokter = oDokter.GetDataByKodeVclaim(dsAntrian.KODEDOKTER)
                '    If dsDokter IsNot Nothing Then
                '        grdKDDOCTOR.Text = dsDokter.KDDOCTOR
                '    End If
                'Else
                '    txtKODEBOOKING.ResetText()
                '    fn_CariBookingAntrian = False
                '    MsgBox("Tanggal Tidak Sesuai Kunjungan, Silahkan Antri Manual " & dsAntrian.TANGGALPERIKSA.ToString("dd-MM-yyyy"), MsgBoxStyle.Exclamation, Me.Text)
                'End If
            Else
                Dim dsAntrianKartuBPJS = oAntrian.GetDataByKartuBPJSTanggal(Parameter, Now)

                If dsAntrianKartuBPJS IsNot Nothing Then
                    txtKODEBOOKING.Text = dsAntrianKartuBPJS.KODEBOOKING

                    fn_CariBookingAntrian = True
                    Dim oCustomer As New Reference.clsCustomer
                    txtJENISPASIEN.Text = dsAntrianKartuBPJS.JENISPASIEN
                    txtKARTUBPJS.Text = dsAntrianKartuBPJS.NOMORKARTU
                    txtNIK.Text = dsAntrianKartuBPJS.NIK
                    txtKDCUSTOMER.Text = dsAntrianKartuBPJS.NORM
                    Dim dsCustomer = oCustomer.GetData(dsAntrianKartuBPJS.NORM.ToString.PadLeft(9, "0"))
                    If dsCustomer IsNot Nothing Then
                        txtNAMAPASIEN.Text = dsCustomer.NAME_DISPLAY
                    End If
                    txtNOTELEPON.Text = dsAntrianKartuBPJS.NOHP
                    txtNOMORREFERNSI.Text = dsAntrianKartuBPJS.NOMORREFERENSI
                    Dim oPoli As New Reference.clsDepartment
                    Dim oDokter As New Reference.clsDoctor
                    Dim dsPoli = oPoli.GetDataByKodeVclaim(dsAntrianKartuBPJS.KODEPOLI)
                    If dsPoli IsNot Nothing Then
                        grdKDDEPARTMENT.Text = dsPoli.KDDEPARTMENT
                    End If
                    Dim dsDokter = oDokter.GetDataByKodeVclaim(dsAntrianKartuBPJS.KODEDOKTER)
                    If dsDokter IsNot Nothing Then
                        grdKDDOCTOR.Text = dsDokter.KDDOCTOR
                    End If
                Else
                    Dim dsAntrianNoRM = oAntrian.GetDataByRMTanggal(Parameter.PadLeft(9, "0"), Now)
                    If dsAntrianNoRM IsNot Nothing Then
                        txtKODEBOOKING.Text = dsAntrianNoRM.KODEBOOKING

                        fn_CariBookingAntrian = True
                        Dim oCustomer As New Reference.clsCustomer
                        txtJENISPASIEN.Text = dsAntrianNoRM.JENISPASIEN
                        txtKARTUBPJS.Text = dsAntrianNoRM.NOMORKARTU
                        txtNIK.Text = dsAntrianNoRM.NIK
                        txtKDCUSTOMER.Text = dsAntrianNoRM.NORM
                        Dim dsCustomer = oCustomer.GetData(dsAntrianNoRM.NORM.ToString.PadLeft(9, "0"))
                        If dsCustomer IsNot Nothing Then
                            txtNAMAPASIEN.Text = dsCustomer.NAME_DISPLAY
                        End If
                        txtNOTELEPON.Text = dsAntrianNoRM.NOHP
                        txtNOMORREFERNSI.Text = dsAntrianNoRM.NOMORREFERENSI
                        Dim oPoli As New Reference.clsDepartment
                        Dim oDokter As New Reference.clsDoctor
                        Dim dsPoli = oPoli.GetDataByKodeVclaim(dsAntrianNoRM.KODEPOLI)
                        If dsPoli IsNot Nothing Then
                            grdKDDEPARTMENT.Text = dsPoli.KDDEPARTMENT
                        End If
                        Dim dsDokter = oDokter.GetDataByKodeVclaim(dsAntrianNoRM.KODEDOKTER)
                        If dsDokter IsNot Nothing Then
                            grdKDDOCTOR.Text = dsDokter.KDDOCTOR
                        End If
                    Else
                        fn_CariBookingAntrian = False
                        fn_CariSKD(txtSEARCH.Text.ToString.Trim)
                    End If
                End If
            End If
        Catch oErr As Exception
            fn_CariBookingAntrian = False
            MsgBox("Cari Booking Antrian" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_CariSKD(ByVal Parameter As String) As Boolean
        Try
            fn_CariSKD = True

            Dim oSKD As New Admission.clsSKD

            Dim dsAntrianKartuBPJS = oSKD.GetDataByKartuBPJSTanggalKonsul(Parameter, Now)

            If dsAntrianKartuBPJS IsNot Nothing Then
                'sKODEBOOKING = ""

                txtJENISPASIEN.Text = IIf(dsAntrianKartuBPJS.S_PENDAFTARAN_H.M_DAFTAR_L1.MEMO = "BPJS", "JKN", "NON JKN")
                txtKARTUBPJS.Text = dsAntrianKartuBPJS.S_PENDAFTARAN_H.KARTUBPJS
                txtNIK.Text = dsAntrianKartuBPJS.S_PENDAFTARAN_H.M_CUSTOMER.KTP
                txtKDCUSTOMER.Text = dsAntrianKartuBPJS.S_PENDAFTARAN_H.KDCUSTOMER
                txtNAMAPASIEN.Text = dsAntrianKartuBPJS.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY
                txtNOTELEPON.Text = dsAntrianKartuBPJS.S_PENDAFTARAN_H.NOMORTELEPON
                txtNOMORREFERNSI.Text = dsAntrianKartuBPJS.NOMORRUJUKAN
                grdKDDEPARTMENT.Text = dsAntrianKartuBPJS.KDDEPARTMENT
                grdKDDOCTOR.Text = dsAntrianKartuBPJS.KDDOCTOR
                sKDPENJAMIN = dsAntrianKartuBPJS.S_PENDAFTARAN_H.M_CUSTOMER.KDPENJAMIN
            Else
                Dim dsAntrianNoRM = oSKD.GetDataByRMTanggalKonsul(Parameter, Now)
                If dsAntrianNoRM IsNot Nothing Then
                    'sKODEBOOKING = ""

                    txtJENISPASIEN.Text = IIf(dsAntrianNoRM.S_PENDAFTARAN_H.M_DAFTAR_L1.MEMO = "BPJS", "JKN", "NON JKN")
                    txtKARTUBPJS.Text = dsAntrianNoRM.S_PENDAFTARAN_H.KARTUBPJS
                    txtNIK.Text = dsAntrianNoRM.S_PENDAFTARAN_H.M_CUSTOMER.KTP
                    txtKDCUSTOMER.Text = dsAntrianNoRM.S_PENDAFTARAN_H.KDCUSTOMER
                    txtNAMAPASIEN.Text = dsAntrianNoRM.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY
                    txtNOTELEPON.Text = dsAntrianNoRM.S_PENDAFTARAN_H.NOMORTELEPON
                    txtNOMORREFERNSI.Text = dsAntrianNoRM.NOMORRUJUKAN
                    grdKDDEPARTMENT.Text = dsAntrianNoRM.KDDEPARTMENT
                    grdKDDOCTOR.Text = dsAntrianNoRM.KDDOCTOR
                    sKDPENJAMIN = dsAntrianNoRM.S_PENDAFTARAN_H.M_CUSTOMER.KDPENJAMIN
                Else
                    fn_CariSKD = False
                    MsgBox("Data Tidak ditemukan !!!" & vbCrLf & Parameter, MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If
        Catch oErr As Exception
            fn_CariSKD = False
            MsgBox("Cari SKD" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtJENISPASIEN.Text = String.Empty Then
                fn_Validate = False
                MsgBox("Data Belum Lengkap !!!", MsgBoxStyle.Exclamation, Me.Text)
                Exit Function
            End If
            If txtNAMAPASIEN.Text = String.Empty Then
                fn_Validate = False
                MsgBox("Data Belum Lengkap !!!", MsgBoxStyle.Exclamation, Me.Text)
                Exit Function
            End If
            If grdKDDEPARTMENT.Text = String.Empty Then
                fn_Validate = False
                MsgBox("Data Belum Lengkap !!!", MsgBoxStyle.Exclamation, Me.Text)
                Exit Function
            End If
            If grdKDDOCTOR.Text = String.Empty Then
                fn_Validate = False
                MsgBox("Data Belum Lengkap !!!", MsgBoxStyle.Exclamation, Me.Text)
                Exit Function
            End If
        Catch oErr As Exception
            MsgBox("Validate" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub fn_UpdateWaktuAntrean(ByVal KODEBOOKING As String, ByVal taskid As Integer, ByVal KETERANGAN As String)
        Dim oSet_Antrian_Simpan As New SettingAntrian.clsSetAntrian
        oSet_Antrian_Simpan.UpdateDataIsCheked(KODEBOOKING, taskid, KETERANGAN)
        Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
        Dim oUpdateWaktuAntrian As New SettingAntrian_UI.clsSetAntrian_UI
        Dim JsonRequest As String = oUpdateWaktuAntrian.fn_RequestUpdateWaktuAntrean(KODEBOOKING, taskid, uTime)
        If JsonRequest <> "" Then
            MsgBox("Update Waktu Ke BPJS" & vbCrLf & oUpdateWaktuAntrian.fn_UpdateWaktuAntrean(JsonRequest, uTime), MsgBoxStyle.Information, Me.Text)
        End If
    End Sub
    Private Function fn_Save() As String
        Try
            Dim oSet_Antrian_Simpan As New SettingAntrian.clsSetAntrian
            Dim oDoctor As New Reference.clsDoctor
            Dim sHARI As String = String.Empty
            Dim sKDDOCTOR_BPJS As String = String.Empty
            Dim sKDDEPARTMENT_BPJS As String = String.Empty

            Select Case Weekday(Now)
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

            ' ***** HEADER *****
            Dim ds = oSet_Antrian_Simpan.GetStructureHeader

            Dim dsJawdwalDokter = oDoctor.GetDataDetailJadwalDokter(grdKDDOCTOR.EditValue, sHARI)

            If dsJawdwalDokter Is Nothing Then
                MsgBox("Jadwal Dokter di SIMRS belum ada Untuk Dokter " & grdKDDOCTOR.Text & " di Hari " & sHARI & " Silahkan Lengkapi Jadwal Dokter di Referensi Dokter", MsgBoxStyle.Information, Me.Text)
                fn_Save = ""
                Exit Function
            Else
                sKDDOCTOR_BPJS = dsJawdwalDokter.M_DOCTOR.VCLAIM_KDDPJP
            End If

            Dim oDepartmen As New Reference.clsDepartment

            Dim dsDepartment = oDepartmen.GetData(grdKDDEPARTMENT.EditValue)
            If dsDepartment IsNot Nothing Then
                sKDDEPARTMENT_BPJS = dsDepartment.VCLAIM_KODEPOLI
            End If

            With ds
                .DATECREATED = Now
                .DATEUPDATED = Now
                .KDPENJAMIN = sKDPENJAMIN
                .KODEBOOKING = ""
                .JENISPASIEN_RS = "U"
                .JENISPASIEN = txtJENISPASIEN.Text
                .NOMORKARTU = txtKARTUBPJS.Text
                .NOHP = txtNOTELEPON.Text.ToString.Trim
                .NIK = txtNIK.Text.ToString.Trim
                .KODEPOLI = sKDDEPARTMENT_BPJS
                .NAMAPOLI = grdKDDEPARTMENT.Text
                .PASIENBARU = 0
                .NORM = txtKDCUSTOMER.Text
                .TANGGALPERIKSA = Now
                .TANGGALPERIKSA_TEXT = Now.ToString("ddMMyyyy")
                .KODEDOKTER = sKDDOCTOR_BPJS
                .NAMADOKTER = grdKDDOCTOR.Text
                .JAMPRAKTEK = dsJawdwalDokter.BUKA & "-" & dsJawdwalDokter.TUTUP
                .JENISKUNJUNGAN = 3
                .NOMORREFERENSI = txtNOMORREFERNSI.Text
                .NOMORANTREAN = ""
                .ANGKAANTREAN = 0
                Dim estimasi As DateTime = DateTime.Parse(Now.ToString("yyyy-MM-dd HH:mm"))
                .ESTIMASIDILAYANI = (estimasi.AddMinutes(60 * oDoctor.GetDataMonitoringTotal(sKDDEPARTMENT_BPJS, sKDDOCTOR_BPJS, Now)).ToString("yyyy-MM-dd HH:mm"))
                .SISAKUOTAJKN = IIf(dsJawdwalDokter.KAPASITASPASIEN_JKN - oDoctor.GetDataMonitoringSisaJKN(sKDDEPARTMENT_BPJS, sKDDOCTOR_BPJS, Now) < 0, 0, dsJawdwalDokter.KAPASITASPASIEN_JKN - oDoctor.GetDataMonitoringSisaJKN(sKDDEPARTMENT_BPJS, sKDDOCTOR_BPJS, Now))
                .KUOTAJKN = dsJawdwalDokter.KAPASITASPASIEN_JKN
                .SISAKUOTANONJKN = IIf(dsJawdwalDokter.KAPASITASPASIEN_NONJKN - oDoctor.GetDataMonitoringSisaNONJKN(sKDDEPARTMENT_BPJS, sKDDOCTOR_BPJS, Now) < 0, 0, dsJawdwalDokter.KAPASITASPASIEN_NONJKN - oDoctor.GetDataMonitoringSisaNONJKN(sKDDEPARTMENT_BPJS, sKDDOCTOR_BPJS, Now))
                .KUOTANONJKN = dsJawdwalDokter.KAPASITASPASIEN_NONJKN
                .ISPANGGIL = 1
                .ISONLINE = False
                .KETERANGAN = ""
                .KDSKD = ""
            End With

            If txtKODEBOOKING.Text = "" Then
                fn_Save = oSet_Antrian_Simpan.InsertData(ds)
            Else
                fn_Save = True
                fn_PrintStruk7(txtKODEBOOKING.Text)
            End If

        Catch oErr As Exception
            MsgBox("Simpan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = ""
        End Try
    End Function
    Private Function fn_RequestTambahAntrean(ByVal KODEBOOKING As String) As String
        Try
            Dim oAntrian As New SettingAntrian.clsSetAntrian
            Dim jsonRequest As String = String.Empty

            Dim dsAntrian = oAntrian.GetData(KODEBOOKING)
            If dsAntrian IsNot Nothing Then
                Dim TanggalEstimasi As DateTime = Now
                TanggalEstimasi = dsAntrian.ESTIMASIDILAYANI & ":00"

                Dim uTime As String = (TanggalEstimasi.Subtract(New DateTime(1970, 1, 1))).TotalMilliseconds

                jsonRequest = " { "
                jsonRequest &= """kodebooking"": """ & dsAntrian.KODEBOOKING & ""","
                jsonRequest &= """jenispasien"": """ & dsAntrian.JENISPASIEN & """, "
                jsonRequest &= """nomorkartu"": """ & dsAntrian.NOMORKARTU & """, "
                jsonRequest &= """nik"": """ & dsAntrian.NIK.ToString & """, "
                jsonRequest &= """nohp"": """ & dsAntrian.NOHP.ToString.Trim & """, "
                jsonRequest &= """kodepoli"": """ & dsAntrian.KODEPOLI & """, "
                jsonRequest &= """namapoli"": """ & dsAntrian.NAMAPOLI & """, "
                jsonRequest &= """pasienbaru"": """ & dsAntrian.PASIENBARU.ToString.Trim & """, "
                jsonRequest &= """norm"": """ & dsAntrian.NORM & """, "
                jsonRequest &= """tanggalperiksa"": """ & dsAntrian.TANGGALPERIKSA.ToString("yyyy-MM-dd") & """, "
                jsonRequest &= """kodedokter"": """ & dsAntrian.KODEDOKTER & """, "
                jsonRequest &= """namadokter"": """ & dsAntrian.NAMADOKTER & """, "
                jsonRequest &= """jampraktek"": """ & dsAntrian.JAMPRAKTEK & """, "
                jsonRequest &= """jeniskunjungan"": """ & dsAntrian.JENISKUNJUNGAN & """, "
                jsonRequest &= """nomorreferensi"": """ & dsAntrian.NOMORREFERENSI & """, "
                jsonRequest &= """nomorantrean"": """ & dsAntrian.NOMORANTREAN & """, "
                jsonRequest &= """angkaantrean"": """ & dsAntrian.ANGKAANTREAN & """, "
                jsonRequest &= """estimasidilayani"": """ & uTime & """, "
                jsonRequest &= """sisakuotajkn"": """ & dsAntrian.SISAKUOTAJKN & """, "
                jsonRequest &= """kuotajkn"": """ & dsAntrian.KUOTAJKN & """, "
                jsonRequest &= """sisakuotanonjkn"": """ & dsAntrian.SISAKUOTANONJKN & """, "
                jsonRequest &= """kuotanonjkn"": """ & dsAntrian.KUOTANONJKN & """, "
                jsonRequest &= """keterangan"": """ & dsAntrian.KETERANGAN & """ "
                jsonRequest &= "}  "

                fn_RequestTambahAntrean = jsonRequest
            Else
                fn_RequestTambahAntrean = ""
            End If
        Catch oErr As Exception
            fn_RequestTambahAntrean = ""
            MsgBox("Requset Tambah Antrean" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_TambahAntrean(ByVal jsonRequest As String, ByVal uTime As Integer) As String
        Try
            Dim oSetKoneksi As New Brigging.clsSetKoneksi

            If sAntrol_ConsId <> "" Then
                Dim dsSetKoneksi = oSetKoneksi.TambahAntrean(sAntrol_Url, sAntrol_ConsId, sAntrol_SecreatKey, sAntrol_UserKey, uTime, jsonRequest)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metadata").Item("code")) = True, "", allData.Item("metadata").Item("code"))
                    messageResponse = allData("metadata")("message").ToString

                    If CodeResponse = "200" Then
                        fn_TambahAntrean = CodeResponse
                    Else
                        fn_TambahAntrean = CodeResponse & " - " & messageResponse
                    End If
                Else
                    fn_TambahAntrean = "Tambah Antrean Gagal"
                End If
            Else
                fn_TambahAntrean = "Koneksi Tidak ditemukan"
            End If
        Catch oErr As Exception
            fn_TambahAntrean = "Tambah Antrean" & vbCrLf & oErr.Message
        End Try
    End Function
    Private Function fn_PrintStruk7(ByVal sCode As String) As Boolean
        Try
            Dim oSet_Antrian_Simpan As New SettingAntrian.clsSetAntrian
            Dim rpt As New xtraAntrianManual
            Dim ds = oSet_Antrian_Simpan.GetData(sCode)
            rpt.BindingSource.DataSource = ds
            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
            printTool.Print()
        Catch oErr As Exception
            MsgBox("Print Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
#Region "ButtonNumber"
    Private Sub fn_LoadPOLI()
        Dim oPOLI As New Reference.clsDepartment
        Try
            grdKDDEPARTMENT.Properties.DataSource = oPOLI.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDEPARTMENT.Properties.ValueMember = "KDDEPARTMENT"
            grdKDDEPARTMENT.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox("Load Poli" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDOKTER()
        Dim oDOKTER As New Reference.clsDoctor
        Try
            grdKDDOCTOR.Properties.DataSource = oDOKTER.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox("Load Dokter" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub SimpleButton1_Click_1(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        txtSEARCH.Text = txtSEARCH.Text + "1"
    End Sub
    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        txtSEARCH.Text = txtSEARCH.Text + "2"
    End Sub
    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        txtSEARCH.Text = txtSEARCH.Text + "3"
    End Sub
    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        txtSEARCH.Text = txtSEARCH.Text + "4"
    End Sub
    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        txtSEARCH.Text = txtSEARCH.Text + "5"
    End Sub
    Private Sub SimpleButton6_Click(sender As Object, e As EventArgs) Handles SimpleButton6.Click
        txtSEARCH.Text = txtSEARCH.Text + "6"
    End Sub
    Private Sub SimpleButton7_Click(sender As Object, e As EventArgs) Handles SimpleButton7.Click
        txtSEARCH.Text = txtSEARCH.Text + "7"
    End Sub
    Private Sub SimpleButton8_Click(sender As Object, e As EventArgs) Handles SimpleButton8.Click
        txtSEARCH.Text = txtSEARCH.Text + "8"
    End Sub
    Private Sub SimpleButton9_Click(sender As Object, e As EventArgs) Handles SimpleButton9.Click
        txtSEARCH.Text = txtSEARCH.Text + "9"
    End Sub
    Private Sub SimpleButton0_Click(sender As Object, e As EventArgs) Handles SimpleButton0.Click
        txtSEARCH.Text = txtSEARCH.Text + "0"
    End Sub
    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        txtSEARCH.Text = ""
        txtSEARCH.Focus()
    End Sub
    Private Sub SimpleButton11_Click(sender As Object, e As EventArgs) Handles SimpleButton11.Click
        If txtSEARCH.Text < " " Then
            txtSEARCH.Text = Mid(txtSEARCH.Text, 1, Len(txtSEARCH.Text) - 1 + 1)
        Else
            txtSEARCH.Text = Mid(txtSEARCH.Text, 1, Len(txtSEARCH.Text) - 1)
        End If
    End Sub
    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        Me.Close()
    End Sub
    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If fn_Validate() = False Then Exit Sub
        If txtKODEBOOKING.Text <> "" Then
            fn_UpdateWaktuAntrean(txtKODEBOOKING.Text, cboASALRUJUKAN.SelectedIndex + 1, "")
        Else
            If cboASALRUJUKAN.SelectedIndex = 1 Or cboASALRUJUKAN.SelectedIndex = 2 Then
                txtKODEBOOKING.Text = fn_Save()
                If txtKODEBOOKING.Text <> "" Then
                    Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                    Dim jsonRequest As String = fn_RequestTambahAntrean(txtKODEBOOKING.Text)

                    If jsonRequest <> "" Then
                        Dim HASIL = fn_TambahAntrean(jsonRequest, uTime)
                        If HASIL = "200" Then
                            fn_UpdateWaktuAntrean(txtKODEBOOKING.Text, cboASALRUJUKAN.SelectedIndex + 1, jsonRequest)
                        Else
                            Dim oSet_Antrian_Simpan As New SettingAntrian.clsSetAntrian
                            oSet_Antrian_Simpan.UpdateDataIsKeterangan(txtKODEBOOKING.Text, "GAGAL TAMBAH ANTREAN KE BPJS " & HASIL)
                        End If
                    End If
                Else
                    MsgBox("Save Antrian Gagal", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                MsgBox("Kode Booking Belum Ada, Silahkan Pilih Update 1 (mulai waktu tunggu admisi) atau 3 (akhir waktu layan admisi/mulai waktu tunggu poli) terlebih dahulu", MsgBoxStyle.Exclamation, Me.Text)
            End If
        End If

        Me.Close()

    End Sub
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If txtKODEBOOKING.Text <> "" Then
            Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
            Dim jsonRequest As String = fn_RequestTambahAntrean(txtKODEBOOKING.Text)

            If jsonRequest <> "" Then
                Dim HASIL = fn_TambahAntrean(jsonRequest, uTime)
                If HASIL = "200" Then
                    fn_UpdateWaktuAntrean(txtKODEBOOKING.Text, cboASALRUJUKAN.SelectedIndex + 1, jsonRequest)
                Else
                    Dim oSet_Antrian_Simpan As New SettingAntrian.clsSetAntrian
                    oSet_Antrian_Simpan.UpdateDataIsKeterangan(txtKODEBOOKING.Text, "GAGAL TAMBAH ANTREAN KE BPJS " & HASIL)
                End If
            End If
        Else
            MsgBox("Save Antrian Gagal", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
#End Region
End Class