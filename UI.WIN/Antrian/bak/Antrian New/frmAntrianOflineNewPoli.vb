Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq
Imports System.Data.SqlClient

Public Class frmAntrianOflineNewPoli
#Region "Declaration"
    Private oSet_Antrian_Simpan As New SettingAntrian.clsSetAntrian
    Private isLoad As Boolean
    Private sKDDEPARTMENT_SIM As String = String.Empty
    Private sKDDOCTOR_SIM As String = String.Empty
    Private sKDDEPARTMENT_BPJS As String = String.Empty
    Private sKDDOCTOR_BPJS As String = String.Empty
    Private sHARI As String = String.Empty
    Private oCustomer As New Reference.clsCustomer
    Private oDepartment As New Reference.clsDepartment
    Private oDoctor As New Reference.clsDoctor


#End Region
#Region "Function"
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lKDDOCTOR.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lPOLIIRM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        'lKDDEPARTMENT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lNOKUNJUNGAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

        If sPOSTRAWAT = True Then
            fn_LoadPostRawatInap()
        Else
            fn_LoadKartuBPJSMultiRecord()
        End If

        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
        'sCode = txtCODE.Text.Trim.ToUpper
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Function fn_Save() As Boolean
        Try
            Dim dsSudah = oSet_Antrian_Simpan.GetDataByRMTanggal(sKDCUSTOMER_ANTRIAN, Now)

            If dsSudah IsNot Nothing Then
                MsgBox("Anda sudah mencetak antrian dengan kode " & dsSudah.KODEBOOKING, MsgBoxStyle.Exclamation, Me.Text)
                fn_Save = False
                Exit Function
            End If
            ' ***** HEADER *****
            Dim ds = oSet_Antrian_Simpan.GetStructureHeader
            Dim dsJawdwalDokter = oDoctor.GetDataDetailJadwalDokter(sKDDOCTOR_SIM, sHARI)

            If dsJawdwalDokter Is Nothing Then
                fn_Save = False
                Exit Function
            End If

            With ds
                .DATECREATED = Now
                .DATEUPDATED = Now
                .KDPENJAMIN = "PENJAMIN_0000000001"
                .KODEBOOKING = ""
                .JENISPASIEN_RS = sKODEANTRIAN_ANTRIAN
                .JENISPASIEN = "JKN"
                .NOMORKARTU = oCustomer.GetData(sKDCUSTOMER_ANTRIAN).KARTUBPJS
                .NOHP = oCustomer.GetData(sKDCUSTOMER_ANTRIAN).PHONE
                .NIK = oCustomer.GetData(sKDCUSTOMER_ANTRIAN).KTP.ToString.Trim
                .KODEPOLI = sKDDEPARTMENT_BPJS
                .NAMAPOLI = oDepartment.GetData(sKDDEPARTMENT_SIM).NAME_DISPLAY
                .PASIENBARU = 0
                .NORM = sKDCUSTOMER_ANTRIAN
                .TANGGALPERIKSA = Now
                .TANGGALPERIKSA_TEXT = Now.ToString("ddMMyyyy")
                .KODEDOKTER = sKDDOCTOR_BPJS
                .NAMADOKTER = oDoctor.GetData(sKDDOCTOR_SIM).NAME_DISPLAY
                .JAMPRAKTEK = dsJawdwalDokter.BUKA & "-" & dsJawdwalDokter.TUTUP
                .JENISKUNJUNGAN = sJENISKUNJUNGAN_ANTRIAN
                .NOMORREFERENSI = sNOMORREFERENSI_ANTRIAN
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

            Try
                Dim sKDBOOKINGANTREAN As String = ""

                sKDBOOKINGANTREAN = oSet_Antrian_Simpan.InsertData(ds)

                If sKDBOOKINGANTREAN = "" Then
                    fn_Save = False
                Else
                    fn_Save = True

                    Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    Dim jsonRequest As String = fn_RequestTambahAntrean(sKDBOOKINGANTREAN)

                    fn_PrintStruk7(sKDBOOKINGANTREAN)

                    If jsonRequest <> "" Then
                        Dim HASIL = fn_TambahAntrean(jsonRequest, uTime)
                        If HASIL = "200" Then
                            RequestUpdateWaktuAntrean(sKDBOOKINGANTREAN)
                            oSet_Antrian_Simpan.UpdateDataIsKeterangan(sKDBOOKINGANTREAN, jsonRequest)
                        Else
                            oSet_Antrian_Simpan.UpdateDataIsKeterangan(sKDBOOKINGANTREAN, "GAGAL BOOKING BPJS " & HASIL)
                        End If
                    End If
                End If
            Catch ex As Exception
                MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try

        Catch oErr As Exception
            MsgBox("Simpan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
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
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
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

                    'MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)

                    If CodeResponse = "200" Then
                        fn_TambahAntrean = CodeResponse
                    Else
                        fn_TambahAntrean = CodeResponse & " - " & messageResponse
                    End If
                Else
                    fn_TambahAntrean = "Insert Rencana Kontrol Gagal"
                    'MsgBox("Insert Rencana Kontrol Gagal", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_TambahAntrean = "Koneksi Tidak ditemukan"
                'MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            fn_TambahAntrean = oErr.Message
            'MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub RequestUpdateWaktuAntrean(ByVal KDBOOKING As String)
        'Dim oSet_Antrian_Simpan As New SettingAntrian.clsSetAntrian
        'Dim dsSet_Antrian_Simpan = oSet_Antrian_Simpan.GetDataLasAntrianPanggil(Now, cboKode.Text)

        Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
        Dim oUpdateWaktuAntrian As New SettingAntrian_UI.clsSetAntrian_UI
        Dim JsonRequest As String = oUpdateWaktuAntrian.fn_RequestUpdateWaktuAntrean(KDBOOKING, 1, uTime)
        If JsonRequest <> "" Then
            Dim CodeMessage As String = oUpdateWaktuAntrian.fn_UpdateWaktuAntrean(JsonRequest, uTime)
            If CodeMessage <> "200" Then
                MsgBox("Update Waktu Ke BPJS" & vbCrLf & CodeMessage, MsgBoxStyle.Information, Me.Text)
            End If
        End If
    End Sub
    Private Function fn_PrintStruk7(ByVal sCode As String) As Boolean
        Try
            Dim rpt As New xtraAntrianManual
            Dim ds = oSet_Antrian_Simpan.GetData(sCode)
            rpt.BindingSource.DataSource = ds
            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
            printTool.Print()
        Catch oErr As Exception
            MsgBox("Print Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub fn_LoadPostRawatInap()
        Try
            Dim oConn_1 As New SqlConnection
            Dim oComm_1 As New SqlCommand
            Dim da_1 As SqlDataAdapter
            Dim ds_1 As New DataSet
            Dim SQL_1 As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())
            oConn_1 = New SqlConnection(sConn)

            If oConn_1.State = ConnectionState.Closed Then
                oConn_1.Open()
            End If

            SQL_1 = "SELECT "
            SQL_1 &= "noKunjungan = A.NOMORSEP "
            SQL_1 &= ",tglKunjungan = A.DATE "
            SQL_1 &= ",noKartu = A.KDCUSTOMER "
            SQL_1 &= ",provPerujuk = C.MEMO "
            SQL_1 &= ",poliRujukan = B.NAME_DISPLAY "
            SQL_1 &= ",kode = B.VCLAIM_KODEPOLI "
            SQL_1 &= "FROM S_PENDAFTARAN_H A "
            SQL_1 &= "INNER JOIN M_DEPARTMENT B "
            SQL_1 &= "ON A.KDDEPARTMENT = B.KDDEPARTMENT "
            SQL_1 &= "INNER JOIN M_PPK C "
            SQL_1 &= "ON A.KDPPK = C.KDPPK "
            SQL_1 &= "WHERE A.NOPASIEN ='" & sKDCUSTOMER_ANTRIAN & "' "
            SQL_1 &= "AND A.CATEGORY = 1 "
            SQL_1 &= "ORDER BY A.DATE DESC "

            oComm_1.Connection = oConn_1
            oComm_1.CommandText = SQL_1
            oComm_1.CommandTimeout = 120
            oComm_1.CommandType = CommandType.Text

            da_1 = New SqlDataAdapter(oComm_1)
            da_1.Fill(ds_1, "S_PENDAFTARAN_1")

            grdKunjungan.DataSource = ds_1.Tables("S_PENDAFTARAN_1")
            grdKunjungan.ForceInitialize()

            If oConn_1.State = ConnectionState.Open Then
                oConn_1.Close()
            End If

        Catch oErr As Exception
            MsgBox("Preview Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKartuBPJSMultiRecord()
        Try
            Dim dsCustomer = oCustomer.GetData(sKDCUSTOMER_ANTRIAN)
            If dsCustomer IsNot Nothing Then
                If dsCustomer.KARTUBPJS.Count = 13 Then
                    Dim oSetKoneksi As New Brigging.clsSetKoneksi
                    Dim uTime As Integer = 0

                    If sAntrol_ConsId <> "" Then
                        uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                        Dim dsSetKoneksi = oSetKoneksi.CariRujukanKartuMultiRecord(sAntrol_Url, sAntrol_ConsId, sAntrol_SecreatKey, sAntrol_UserKey, uTime, dsCustomer.KARTUBPJS, sASALRUJUKAN_ANTRIAN)

                        If dsSetKoneksi <> "" Then
                            Dim allData = JObject.Parse(dsSetKoneksi)

                            Dim CodeResponse As String = String.Empty
                            Dim messageResponse As String = String.Empty

                            CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                            messageResponse = allData("metaData")("message").ToString

                            If CodeResponse = "200" Then
                                Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), sAntrol_ConsId & sAntrol_SecreatKey & uTime))

                                Dim table As DataTable

                                table = New DataTable("M_RUJUKAN")

                                table.Columns.Add("noKunjungan")
                                table.Columns.Add("tglKunjungan")
                                table.Columns.Add("noKartu")
                                table.Columns.Add("nama")
                                table.Columns.Add("provPerujuk")
                                table.Columns.Add("poliRujukan")
                                table.Columns.Add("kode")

                                For Each item In DataDecrypt("rujukan")
                                    table.Rows.Add(New String() {item("noKunjungan"), item("tglKunjungan"), item("peserta")("noKartu"), item("peserta")("nama"), item("provPerujuk")("nama"), item("poliRujukan")("nama"), item("poliRujukan")("kode")})
                                Next

                                grdKunjungan.DataSource = table
                            End If
                        End If
                    End If
                End If
            Else
                Dim dsKartu = oCustomer.GetDataKARTUBPJS(sKDCUSTOMER_ANTRIAN)
                If dsKartu IsNot Nothing Then
                    sKDCUSTOMER_ANTRIAN = dsKartu.KDCUSTOMER
                    If dsKartu.KARTUBPJS.Count = 13 Then
                        Dim oSetKoneksi As New Brigging.clsSetKoneksi
                        Dim uTime As Integer = 0

                        If sAntrol_ConsId <> "" Then
                            uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                            Dim dsSetKoneksi = oSetKoneksi.CariRujukanKartuMultiRecord(sAntrol_Url, sAntrol_ConsId, sAntrol_SecreatKey, sAntrol_UserKey, uTime, dsKartu.KARTUBPJS, sASALRUJUKAN_ANTRIAN)

                            If dsSetKoneksi <> "" Then
                                Dim allData = JObject.Parse(dsSetKoneksi)

                                Dim CodeResponse As String = String.Empty
                                Dim messageResponse As String = String.Empty

                                CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                                messageResponse = allData("metaData")("message").ToString

                                If CodeResponse = "200" Then
                                    Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), sAntrol_ConsId & sAntrol_SecreatKey & uTime))

                                    Dim table As DataTable

                                    table = New DataTable("M_RUJUKAN")

                                    table.Columns.Add("noKunjungan")
                                    table.Columns.Add("tglKunjungan")
                                    table.Columns.Add("noKartu")
                                    table.Columns.Add("nama")
                                    table.Columns.Add("provPerujuk")
                                    table.Columns.Add("poliRujukan")
                                    table.Columns.Add("kode")

                                    For Each item In DataDecrypt("rujukan")
                                        table.Rows.Add(New String() {item("noKunjungan"), item("tglKunjungan"), item("peserta")("noKartu"), item("peserta")("nama"), item("provPerujuk")("nama"), item("poliRujukan")("nama"), item("poliRujukan")("kode")})
                                    Next

                                    grdKunjungan.DataSource = table
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try

        grvKunjungan.Columns("noKunjungan").Visible = False
    End Sub
    Private Sub fn_LoadDokter(ByVal KDDEPRATMENT As String)
        Try
            grvJadwalDokter.OptionsSelection.MultiSelect = True
            grvJadwalDokter.SelectAll()
            grvJadwalDokter.DeleteSelectedRows()
            grvJadwalDokter.OptionsSelection.MultiSelect = False

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

            Dim dsList = (From x In oDoctor.GetDataDetailJadwalPoli(KDDEPRATMENT, sHARI)
                          Select NAME_DISPLAY = x.M_DOCTOR.NAME_DISPLAY & " ( " & x.BUKA & " s.d " & x.TUTUP & " )" & " Sisa " & IIf(x.KAPASITASPASIEN_JKN - oDoctor.GetDataMonitoringSisaJKN(sKDDEPARTMENT_BPJS, x.M_DOCTOR.VCLAIM_KDDPJP, Now) < 0, 0, x.KAPASITASPASIEN_JKN - oDoctor.GetDataMonitoringSisaJKN(sKDDEPARTMENT_BPJS, x.M_DOCTOR.VCLAIM_KDDPJP, Now)), x.KDDOCTOR, KDDOCTOR_BPJS = x.M_DOCTOR.VCLAIM_KDDPJP, Sisa = IIf(x.KAPASITASPASIEN_JKN - oDoctor.GetDataMonitoringSisaJKN(sKDDEPARTMENT_BPJS, x.M_DOCTOR.VCLAIM_KDDPJP, Now) < 0, 0, x.KAPASITASPASIEN_JKN - oDoctor.GetDataMonitoringSisaJKN(sKDDEPARTMENT_BPJS, x.M_DOCTOR.VCLAIM_KDDPJP, Now))).ToList()

            grdJadwalDokter.DataSource = dsList.Where(Function(x) x.Sisa > 0)

            'Select NAME_DISPLAY = x.M_DOCTOR.NAME_DISPLAY & " ( " & x.BUKA & " s.d " & x.TUTUP & " )" & " Sisa " & IIf(x.KAPASITASPASIEN_JKN - oDoctor.GetDataMonitoringSisaJKN(sKDDEPARTMENT_BPJS, x.M_DOCTOR.VCLAIM_KDDPJP, Now) < 0, 0, x.KAPASITASPASIEN_JKN - oDoctor.GetDataMonitoringSisaJKN(sKDDEPARTMENT_BPJS, x.M_DOCTOR.VCLAIM_KDDPJP, Now)), x.KDDOCTOR, KDDOCTOR_BPJS = x.M_DOCTOR.VCLAIM_KDDPJP).ToList()
        Catch oErr As Exception
            MsgBox("Preview Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub RepositoryItemButtonEdit3_Click(sender As Object, e As EventArgs) Handles btnKunjungan.Click
        If grvKunjungan.GetFocusedRowCellValue("kode") Is Nothing Then
            MsgBox("Poli Tidak di Pilih/Kosong, Silahkan Ulangi", MsgBoxStyle.Exclamation, Me.Text)
            Me.Close()
        End If

        Dim hari As Integer = DateDiff(DateInterval.Day, grvKunjungan.GetFocusedRowCellValue("tglKunjungan"), Today())
        If hari > 90 Then
            MsgBox("Surat Rujukan Nomor : " & grvKunjungan.GetFocusedRowCellValue("noKunjungan") & " masa berlaku Habis, Maksimal 3(tiga) bulan dari tanggal rujukan Silahkan ke Faskes Perujuk untuk Perbaharui Rujukan", MsgBoxStyle.OkOnly, "Perhatian !!!")
            Exit Sub
        End If

        If sJENISKUNJUNGAN_ANTRIAN = "1" Or sJENISKUNJUNGAN_ANTRIAN = "3" Or sJENISKUNJUNGAN_ANTRIAN = "4" Then
            Dim dsDepartment = oDepartment.GetDatakodebpjs(grvKunjungan.GetFocusedRowCellValue("kode"))

            If dsDepartment IsNot Nothing Then
                lKDDOCTOR.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lNOKUNJUNGAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                sKDDEPARTMENT_SIM = dsDepartment.KDDEPARTMENT
                sKDDEPARTMENT_BPJS = dsDepartment.VCLAIM_KODEPOLI
                sNOMORREFERENSI_ANTRIAN = grvKunjungan.GetFocusedRowCellValue("noKunjungan")
                fn_LoadDokter(sKDDEPARTMENT_SIM)
            Else
                MsgBox("Poli Tidak di Pilih/Kosong, Silahkan Ulangi", MsgBoxStyle.Exclamation, Me.Text)
                Me.Close()
            End If
        Else
            sNOMORREFERENSI_ANTRIAN = grvKunjungan.GetFocusedRowCellValue("noKunjungan")

            lPOLIIRM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lNOKUNJUNGAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

            grvKunjungan.OptionsSelection.MultiSelect = True
            grvKunjungan.SelectAll()
            grvKunjungan.DeleteSelectedRows()
            grvKunjungan.OptionsSelection.MultiSelect = False

            Dim oDeparment As New Reference.clsDepartment

            'grdIRM.DataSource = From x In oDeparment.GetData()
            '                    Where x.KDDEPARTMENT_BPJS = "IRM" And x.ISACTIVE = True
            '                    Select x.KDDEPARTMENT, x.NAME_DISPLAY

            grdIRM.DataSource = From x In oDeparment.GetData()
                                Where x.ISACTIVE = True
                                Select x.KDDEPARTMENT, x.NAME_DISPLAY

        End If
    End Sub
    Private Sub RepositoryItemButtonEdit1_Click(sender As Object, e As EventArgs) Handles RepositoryItemButtonEdit1.Click
        If grvIRM.GetFocusedRowCellValue("KDDEPARTMENT") Is Nothing Then
            MsgBox("Poli Tidak di Pilih/Kosong, Silahkan Ulangi", MsgBoxStyle.Exclamation, Me.Text)
            Me.Close()
        End If

        Dim dsDepartment = oDepartment.GetData(grvIRM.GetFocusedRowCellValue("KDDEPARTMENT"))

        If dsDepartment IsNot Nothing Then
            lKDDOCTOR.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lNOKUNJUNGAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lPOLIIRM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            sKDDEPARTMENT_SIM = dsDepartment.KDDEPARTMENT
            sKDDEPARTMENT_BPJS = dsDepartment.VCLAIM_KODEPOLI
            fn_LoadDokter(sKDDEPARTMENT_SIM)
        Else
            MsgBox("Poli Tidak di Pilih/Kosong, Silahkan Ulangi", MsgBoxStyle.Exclamation, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub RepositoryItemButtonEdit2_Click(sender As Object, e As EventArgs) Handles RepositoryItemButtonEdit2.Click
        If grvJadwalDokter.GetFocusedRowCellValue("KDDOCTOR") Is Nothing Then
            MsgBox("Dokter Tidak di Pilih/Kosong, Silahkan Ulangi", MsgBoxStyle.Exclamation, Me.Text)
            Me.Close()
        End If

        sKDDOCTOR_SIM = grvJadwalDokter.GetFocusedRowCellValue("KDDOCTOR")
        sKDDOCTOR_BPJS = grvJadwalDokter.GetFocusedRowCellValue("KDDOCTOR_BPJS")

        If fn_Save() = True Then
            Dim oSisaDokter As New SettingAntrian.clsSet_Sisa_Dokter
            Dim dsSisa = oSisaDokter.GetDataSisa(sKDDOCTOR_SIM, Now.ToString("yyyyMMdd"))

            If dsSisa IsNot Nothing Then
                If dsSisa.SISA_JKN > 0 Then
                    'Update()
                End If
            Else
                'Get jadwal

                'oSisaDokter.InsertData(sKDDOCTOR_SIM, Now.ToString("yyyyMMdd"),)
            End If

            Me.Close()
        Else
            MsgBox("Dokter Tidak di Pilih/Kosong, Silahkan Ulangi", MsgBoxStyle.Exclamation, Me.Text)
            ' Me.Close()
        End If
    End Sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Me.Close()
    End Sub
#End Region
End Class