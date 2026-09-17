Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports DevExpress.XtraPrinting
Imports System.Data
Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports DevExpress.XtraSplashScreen
Imports System.Globalization
Imports System.Text.RegularExpressions

Public Class frmReportAntrianOnlineJKN
    Implements ILanguage

#Region "Function"
    Private oSet_Antrian As New SettingAntrian.clsSetAntrian
    Private oDaftar As New Admission.clsPendaftaran
    Private oBilling As New Sales.clsSalesOrderTransaksi
    Private oCPPT As New Grouper.clsR_CPPT

    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "Antrian Online JKN"
        sCOPYKODDEBOOKING = String.Empty
        deDATEFrom.DateTime = Now
        deDATETo.DateTime = Now
        fn_LoadSecurity()
    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        'Try
        '    grv.SaveLayoutToRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text & "\")
        'Catch oErr As Exception

        'End Try
    End Sub
    Public Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            Me.Text = "Laporan Antrian"

            'fn_LoadLanguageAll()

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
                      Where x.MODUL = "ANTRIAN" _
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
{"", "ANTRIAN DARI TANGGAL " & deDATEFrom.DateTime.ToString("dd-MM-yyyy") & " SAMPAI " & deDATETo.DateTime.ToString("dd-MM-yyyy"), ""})

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

            If cboTYPE.SelectedIndex = 0 Then
                fn_LoadData()
                'ElseIf cboTYPE.SelectedIndex = 1 Then
                '    MsgBox("Tidak dapat digunakan", MsgBoxStyle.Exclamation, Me.Text)
                '    'fn_LoadDataOnline()
            ElseIf cboTYPE.SelectedIndex = 1 Then
                fn_LoadData06()
            ElseIf cboTYPE.SelectedIndex = 2 Then
                fn_LoadAntrianUmum()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Create_KodeBooking(ByVal sKODEBOOKING As String, ByVal sNOMORKARTU As String, ByVal sNOHP As String, ByVal sNIK As String, ByVal sKODEPOLI As String, ByVal sPASIENBARU As String, ByVal sNORM As String, ByVal sKODEDOKTER As String, ByVal sJAMPRAKTEK As String, ByVal sJENISKUNJUNGAN As String, ByVal sNOMORREFERENSI As String, ByVal sNOMORANTREAN As String, ByVal sANGKAANTREAN As String, ByVal sESTIMASIDILAYANI As DateTime, ByVal sStatus As String)
        Try
            Dim dsSet_Antrian_Simpan = oSet_Antrian.GetData(sKODEBOOKING)

            If dsSet_Antrian_Simpan Is Nothing Then
                ' ***** HEADER *****
                Dim oDepartment As New Reference.clsDepartment
                Dim oDoctor As New Reference.clsDoctor

                Dim ds = oSet_Antrian.GetStructureHeader
                With ds
                    .DATECREATED = Now
                    .DATEUPDATED = Now
                    .KDPENJAMIN = IIf(sNOMORKARTU <> "", "PENJAMIN_0000000004", "PENJAMIN_0000000001")
                    .KODEBOOKING = sKODEBOOKING
                    .JENISPASIEN_RS = "AUT"
                    .JENISPASIEN = IIf(sNOMORKARTU <> "", "JKN", "NON JKN")
                    .NOMORKARTU = sNOMORKARTU
                    .NOHP = sNOHP
                    .NIK = sNIK
                    Dim dsDepartment = oDepartment.GetDataByKodeVclaim(sKODEPOLI)
                    If dsDepartment Is Nothing Then
                        .KODEPOLI = sKODEPOLI
                        .NAMAPOLI = "-"
                    Else
                        .KODEPOLI = sKODEPOLI
                        .NAMAPOLI = dsDepartment.NAME_DISPLAY
                    End If
                    .PASIENBARU = sPASIENBARU
                    .NORM = sNORM
                    .TANGGALPERIKSA = deDATEFrom.DateTime
                    .TANGGALPERIKSA_TEXT = deDATEFrom.DateTime.ToString("ddMMyyyy")
                    Dim dsDoctor = oDoctor.GetDataByKodeVclaim(sKODEDOKTER)
                    If dsDoctor Is Nothing Then
                        .KODEDOKTER = sKODEDOKTER
                        .NAMADOKTER = "-"
                    Else
                        .KODEDOKTER = sKODEDOKTER
                        .NAMADOKTER = dsDoctor.NAME_DISPLAY
                    End If
                    .JAMPRAKTEK = sJAMPRAKTEK
                    .JENISKUNJUNGAN = sJENISKUNJUNGAN
                    .NOMORREFERENSI = sNOMORREFERENSI
                    .NOMORANTREAN = sNOMORANTREAN
                    .ANGKAANTREAN = sANGKAANTREAN
                    .ESTIMASIDILAYANI = sESTIMASIDILAYANI.ToString("yyyy-MM-dd") & "00:00"
                    .SISAKUOTAJKN = 0
                    .KUOTAJKN = 0
                    .SISAKUOTANONJKN = 0
                    .SISAKUOTANONJKN = 0
                    .ISPANGGIL = 0
                    .ISONLINE = IIf(sStatus = "Mobile JKN", True, False)
                    .KETERANGAN = ""
                    .KDSKD = ""
                End With

                oSet_Antrian.InsertDataBookingNew(ds)
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data Kode Booking: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    'Private Function fn_UpdateWaktu(ByVal KODEBOOKING As String, ByVal sTaskId As Integer, ByVal isPesan As Boolean, ByVal waktulayanestimasi As DateTime) As Boolean
    '    Try
    '        fn_UpdateWaktu = False

    '        Dim dsSet_Antrian_Simpan = oSet_Antrian.GetData(KODEBOOKING)
    '        Dim Waktu As DateTime
    '        Dim sLanjut As Boolean = False
    '        Dim sPasienLama As Boolean = False

    '        If dsSet_Antrian_Simpan IsNot Nothing Then
    '            If dsSet_Antrian_Simpan.ISPANGGIL < sTaskId Then
    '                oSet_Antrian.UpdateDataIsCheked(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, "")
    '            End If

    '            If sTaskId = 1 Then
    '                If dsSet_Antrian_Simpan.PASIENBARU = "1" Then
    '                    Dim dsWaktuPanggil = oSet_Antrian.GetDataBySaveWaktuTunggu(dsSet_Antrian_Simpan.KODEBOOKING, 1)
    '                    If dsWaktuPanggil IsNot Nothing Then
    '                        If Not dsWaktuPanggil.REMARKS.Contains("200-Ok") Then
    '                            sLanjut = True
    '                            Waktu = dsWaktuPanggil.DATE
    '                        End If
    '                    Else
    '                        Dim Rnd As New Random()

    '                        Waktu = waktulayanestimasi.AddMinutes(-Rnd.Next(10, 15))

    '                        frmErmList.fn_TaskIDyangLalu(dsSet_Antrian_Simpan.KODEBOOKING, 1, Waktu)

    '                        If isPesan = True Then
    '                            MsgBox("belum dipanggil", MsgBoxStyle.Exclamation, Me.Text)
    '                        Else
    '                            oSet_Antrian.UpdateDataIsChekedNewKeterangan(dsSet_Antrian_Simpan.KODEBOOKING, "belum dipanggil (1 Rnd)")
    '                        End If
    '                    End If
    '                Else
    '                    sPasienLama = True

    '                    If isPesan = True Then
    '                        MsgBox("bukan merupakan pasien baru", MsgBoxStyle.Exclamation, Me.Text)
    '                    End If
    '                End If
    '            ElseIf sTaskId = 2 Then
    '                If dsSet_Antrian_Simpan.PASIENBARU = "1" Then
    '                    Dim dsWaktuPanggil = oSet_Antrian.GetDataBySaveWaktuTunggu(dsSet_Antrian_Simpan.KODEBOOKING, 2)
    '                    If dsWaktuPanggil IsNot Nothing Then
    '                        If Not dsWaktuPanggil.REMARKS.Contains("200-Ok") Then
    '                            sLanjut = True
    '                            Waktu = dsWaktuPanggil.DATE
    '                        End If
    '                    Else
    '                        Dim dsWaktuPanggil1 = oSet_Antrian.GetDataBySaveWaktuTunggu(dsSet_Antrian_Simpan.KODEBOOKING, 1)
    '                        If dsWaktuPanggil1 IsNot Nothing Then
    '                            Dim Rnd As New Random()

    '                            Waktu = dsWaktuPanggil1.DATE.AddMinutes(Rnd.Next(1, 3))

    '                            If frmErmList.fn_TaskIDyangLalu(dsSet_Antrian_Simpan.KODEBOOKING, 2, Waktu) = True Then
    '                                sLanjut = True
    '                            End If

    '                            If isPesan = True Then
    '                                MsgBox("belum dipanggil", MsgBoxStyle.Exclamation, Me.Text)
    '                            Else
    '                                oSet_Antrian.UpdateDataIsChekedNewKeterangan(dsSet_Antrian_Simpan.KODEBOOKING, "belum dipanggil (2 Rnd)")
    '                            End If
    '                        End If
    '                    End If
    '                Else
    '                    sPasienLama = True

    '                    If isPesan = True Then
    '                        MsgBox("bukan merupakan pasien baru", MsgBoxStyle.Exclamation, Me.Text)
    '                    End If
    '                End If
    '            ElseIf sTaskId = 3 Then
    '                Dim dsWaktuPanggil = oSet_Antrian.GetDataBySaveWaktuTunggu(dsSet_Antrian_Simpan.KODEBOOKING, 3)
    '                If dsWaktuPanggil IsNot Nothing Then
    '                    If Not dsWaktuPanggil.REMARKS.Contains("200-Ok") Then
    '                        sLanjut = True
    '                        Waktu = dsWaktuPanggil.DATE
    '                    Else
    '                        sPasienLama = True
    '                    End If
    '                Else
    '                    Dim dsKodeBooking = oDaftar.GetDataPendaftaranByKodeBooking(dsSet_Antrian_Simpan.KODEBOOKING)
    '                    If dsKodeBooking IsNot Nothing Then
    '                        If frmErmList.fn_TaskIDyangLalu(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, dsKodeBooking.DATECREATED) = True Then
    '                            sLanjut = True
    '                            Waktu = dsKodeBooking.DATECREATED
    '                        Else
    '                            If isPesan = True Then
    '                                MsgBox("Data Gagal Simpan untuk TaskId " & sTaskId, MsgBoxStyle.Exclamation, Me.Text)
    '                            End If
    '                        End If
    '                    Else
    '                        Dim dsWaktuPanggil2 = oSet_Antrian.GetDataBySaveWaktuTunggu(dsSet_Antrian_Simpan.KODEBOOKING, 2)

    '                        If dsWaktuPanggil2 IsNot Nothing Then
    '                            Dim Rnd As New Random()

    '                            Waktu = dsWaktuPanggil2.DATE.AddMinutes(Rnd.Next(5, 10))

    '                            If frmErmList.fn_TaskIDyangLalu(dsSet_Antrian_Simpan.KODEBOOKING, 3, Waktu) = True Then
    '                                sLanjut = True
    '                            Else
    '                                If isPesan = True Then
    '                                    MsgBox("Data Gagal Simpan untuk TaskId " & sTaskId, MsgBoxStyle.Exclamation, Me.Text)
    '                                End If
    '                            End If

    '                            If isPesan = True Then
    '                                MsgBox("Tidak ada data untuk pendaftaran", MsgBoxStyle.Exclamation, Me.Text)
    '                            Else
    '                                oSet_Antrian.UpdateDataIsChekedNewKeterangan(dsSet_Antrian_Simpan.KODEBOOKING, "Tidak ada data untuk pendaftaran (3 Rnd)")
    '                            End If
    '                        Else
    '                            Dim Rnd As New Random()

    '                            Waktu = waktulayanestimasi.AddMinutes(Rnd.Next(5, 10))

    '                            If frmErmList.fn_TaskIDyangLalu(dsSet_Antrian_Simpan.KODEBOOKING, 3, Waktu) = True Then
    '                                sLanjut = True
    '                            Else
    '                                If isPesan = True Then
    '                                    MsgBox("Data Gagal Simpan untuk TaskId " & sTaskId, MsgBoxStyle.Exclamation, Me.Text)
    '                                End If
    '                            End If

    '                            If isPesan = True Then
    '                                MsgBox("Tidak ada data untuk pendaftaran dan Task Id 2", MsgBoxStyle.Exclamation, Me.Text)
    '                            Else
    '                                oSet_Antrian.UpdateDataIsChekedNewKeterangan(dsSet_Antrian_Simpan.KODEBOOKING, "Tidak ada data untuk pendaftaran dan Task Id 2 (3 Rnd)")
    '                            End If
    '                        End If
    '                    End If
    '                End If
    '            ElseIf sTaskId = 4 Then
    '                Dim dsWaktuPanggil = oSet_Antrian.GetDataBySaveWaktuTunggu(dsSet_Antrian_Simpan.KODEBOOKING, 4)
    '                If dsWaktuPanggil IsNot Nothing Then
    '                    If Not dsWaktuPanggil.REMARKS.Contains("200-Ok") Then
    '                        sLanjut = True
    '                        Waktu = dsWaktuPanggil.DATE
    '                    Else
    '                        sPasienLama = True
    '                    End If
    '                Else
    '                    Dim dsWaktuPanggil3 = oSet_Antrian.GetDataBySaveWaktuTunggu(dsSet_Antrian_Simpan.KODEBOOKING, 3)
    '                    If dsWaktuPanggil3 IsNot Nothing Then
    '                        Dim Rnd As New Random()

    '                        Waktu = dsWaktuPanggil3.DATE.AddMinutes(Rnd.Next(5, 10))

    '                        If frmErmList.fn_TaskIDyangLalu(dsSet_Antrian_Simpan.KODEBOOKING, 4, Waktu) = True Then
    '                            sLanjut = True
    '                        Else
    '                            If isPesan = True Then
    '                                MsgBox("Data Gagal Simpan untuk TaskId " & sTaskId, MsgBoxStyle.Exclamation, Me.Text)
    '                            End If
    '                        End If

    '                        If isPesan = True Then
    '                            MsgBox("Dokter Belum Klik CPPT", MsgBoxStyle.Exclamation, Me.Text)
    '                        Else
    '                            oSet_Antrian.UpdateDataIsChekedNewKeterangan(dsSet_Antrian_Simpan.KODEBOOKING, "Simpan Task Id 4 (4 Rnd)")
    '                        End If
    '                    End If
    '                End If
    '            ElseIf sTaskId = 5 Then
    '                Dim dsWaktuPanggil = oSet_Antrian.GetDataBySaveWaktuTunggu(dsSet_Antrian_Simpan.KODEBOOKING, 5)
    '                If dsWaktuPanggil IsNot Nothing Then
    '                    If Not dsWaktuPanggil.REMARKS.Contains("200-Ok") Then
    '                        sLanjut = True
    '                        Waktu = dsWaktuPanggil.DATE
    '                    Else
    '                        sPasienLama = True
    '                    End If
    '                Else
    '                    Dim dsWaktuPanggil4 = oSet_Antrian.GetDataBySaveWaktuTunggu(dsSet_Antrian_Simpan.KODEBOOKING, 4)
    '                    If dsWaktuPanggil4 IsNot Nothing Then
    '                        Dim Rnd As New Random()

    '                        Waktu = dsWaktuPanggil4.DATE.AddMinutes(Rnd.Next(15, 20))

    '                        If frmErmList.fn_TaskIDyangLalu(dsSet_Antrian_Simpan.KODEBOOKING, 5, Waktu) = True Then
    '                            sLanjut = True
    '                        Else
    '                            If isPesan = True Then
    '                                MsgBox("Data Gagal Simpan untuk TaskId " & sTaskId, MsgBoxStyle.Exclamation, Me.Text)
    '                            End If
    '                        End If

    '                        If isPesan = True Then
    '                            MsgBox("Dokter Belum Simpan CPPT", MsgBoxStyle.Exclamation, Me.Text)
    '                        Else
    '                            oSet_Antrian.UpdateDataIsChekedNewKeterangan(dsSet_Antrian_Simpan.KODEBOOKING, "Simpan Task Id 5 (5 Rnd)")
    '                        End If
    '                    End If
    '                End If
    '            ElseIf sTaskId = 6 Then
    '                Dim dsWaktuPanggil = oSet_Antrian.GetDataBySaveWaktuTunggu(dsSet_Antrian_Simpan.KODEBOOKING, 6)
    '                If dsWaktuPanggil IsNot Nothing Then
    '                    If Not dsWaktuPanggil.REMARKS.Contains("200-Ok") Then
    '                        sLanjut = True
    '                        Waktu = dsWaktuPanggil.DATE
    '                    Else
    '                        sPasienLama = True
    '                    End If
    '                Else
    '                    If isPesan = True Then
    '                        MsgBox("Farmasi Belum Terima Resep", MsgBoxStyle.Exclamation, Me.Text)
    '                    End If
    '                End If
    '            ElseIf sTaskId = 7 Then
    '                Dim dsWaktuPanggil = oSet_Antrian.GetDataBySaveWaktuTunggu(dsSet_Antrian_Simpan.KODEBOOKING, 7)
    '                If dsWaktuPanggil IsNot Nothing Then
    '                    If Not dsWaktuPanggil.REMARKS.Contains("200-Ok") Then
    '                        sLanjut = True
    '                        Waktu = dsWaktuPanggil.DATE
    '                    Else
    '                        sPasienLama = True
    '                    End If
    '                Else
    '                    Dim dsWaktuPanggil6 = oSet_Antrian.GetDataBySaveWaktuTunggu(dsSet_Antrian_Simpan.KODEBOOKING, 6)

    '                    If dsWaktuPanggil6 IsNot Nothing Then
    '                        Dim Rnd As New Random()

    '                        Waktu = dsWaktuPanggil6.DATE.AddMinutes(Rnd.Next(20, 30))

    '                        If frmErmList.fn_TaskIDyangLalu(dsSet_Antrian_Simpan.KODEBOOKING, 7, Waktu) = True Then
    '                            sLanjut = True
    '                        Else
    '                            If isPesan = True Then
    '                                MsgBox("Data Gagal Simpan untuk TaskId " & sTaskId, MsgBoxStyle.Exclamation, Me.Text)
    '                            End If
    '                        End If

    '                        If isPesan = True Then
    '                            MsgBox("Farmasi Belum Panggil", MsgBoxStyle.Exclamation, Me.Text)
    '                        Else
    '                            oSet_Antrian.UpdateDataIsChekedNewKeterangan(dsSet_Antrian_Simpan.KODEBOOKING, "Simpan Task Id 7 (7 Rnd)")
    '                        End If
    '                    End If
    '                End If
    '            End If

    '            If sLanjut = True Then
    '                Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
    '                Dim oUpdateWaktuAntrian As New SettingAntrian_UI.clsSetAntrian_UI
    '                Dim JsonRequest As String = oUpdateWaktuAntrian.fn_RequestUpdateWaktuAntreanLooping(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, Waktu)
    '                If JsonRequest <> "" Then
    '                    Dim CodeMessage As String = oUpdateWaktuAntrian.fn_UpdateWaktuAntrean(JsonRequest, uTime)
    '                    If CodeMessage <> "200" Then
    '                        If isPesan = True Then
    '                            MsgBox("Update Waktu Ke BPJS" & vbCrLf & CodeMessage, MsgBoxStyle.Information, Me.Text)
    '                        Else
    '                            If CodeMessage = "208-TaskId=" & sTaskId & " sudah ada" Then
    '                                fn_UpdateWaktu = sLanjut
    '                                oSet_Antrian.UpdateDataIsChekedNew(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, "200-Ok")
    '                                oSet_Antrian.UpdateDataIsChekedNewKeteranganTaksId(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, "200-Ok")
    '                            ElseIf CodeMessage.Contains("lebih besar dari pada") Then
    '                                Try
    '                                    Dim teks As String = CodeMessage

    '                                    ' Ambil tanggal dan waktu (format: 2025-10-17 10:07:27 WIB)
    '                                    Dim match As Match = Regex.Match(teks, "\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2} WIB")

    '                                    If match.Success Then
    '                                        Dim waktuPertama As String = match.Value.Replace(" WIB", "")
    '                                        Dim Rnd As New Random()

    '                                        Waktu = CDate(waktuPertama).AddMinutes(Rnd.Next(15, 20))

    '                                        If oSet_Antrian.UpdateDataIsChekedNewKeteranganTaksIdWaktu(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, CDate(Waktu)) = True Then
    '                                            Dim JsonRequest2 As String = oUpdateWaktuAntrian.fn_RequestUpdateWaktuAntreanLooping(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, CDate(Waktu))
    '                                            Dim uTime2 As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

    '                                            If JsonRequest2 <> "" Then
    '                                                Dim CodeMessage2 As String = oUpdateWaktuAntrian.fn_UpdateWaktuAntrean(JsonRequest2, uTime2)
    '                                                If CodeMessage2 <> "200" Then
    '                                                    If CodeMessage2 = "208-TaskId=" & sTaskId & " sudah ada" Then
    '                                                        fn_UpdateWaktu = True
    '                                                        oSet_Antrian.UpdateDataIsChekedNew(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, "200-Ok")
    '                                                        oSet_Antrian.UpdateDataIsChekedNewKeteranganTaksId(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, "200-Ok")
    '                                                    Else
    '                                                        oSet_Antrian.UpdateDataIsChekedNew(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, CodeMessage2)
    '                                                        oSet_Antrian.UpdateDataIsChekedNewKeteranganTaksId(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, CodeMessage2)
    '                                                    End If
    '                                                Else
    '                                                    fn_UpdateWaktu = True
    '                                                    oSet_Antrian.UpdateDataIsChekedNew(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, CodeMessage2 & "-" & "Ok")
    '                                                    oSet_Antrian.UpdateDataIsChekedNewKeteranganTaksId(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, CodeMessage2 & "-" & "Ok")
    '                                                End If
    '                                            Else
    '                                                oSet_Antrian.UpdateDataIsChekedNew(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, CodeMessage)
    '                                                oSet_Antrian.UpdateDataIsChekedNewKeteranganTaksId(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, CodeMessage)
    '                                            End If
    '                                        Else
    '                                            oSet_Antrian.UpdateDataIsChekedNew(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, CodeMessage)
    '                                            oSet_Antrian.UpdateDataIsChekedNewKeteranganTaksId(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, CodeMessage)
    '                                        End If
    '                                    Else
    '                                        oSet_Antrian.UpdateDataIsChekedNew(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, CodeMessage)
    '                                        oSet_Antrian.UpdateDataIsChekedNewKeteranganTaksId(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, CodeMessage)
    '                                    End If
    '                                Catch ex As Exception
    '                                    oSet_Antrian.UpdateDataIsChekedNew(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, CodeMessage)
    '                                    oSet_Antrian.UpdateDataIsChekedNewKeteranganTaksId(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, CodeMessage)
    '                                End Try
    '                            ElseIf CodeMessage.Contains("tidak boleh kurang atau sama dengan waktu sebelumnya") Then
    '                                Try
    '                                    Dim teks As String = CodeMessage

    '                                    ' Ambil tanggal dan waktu (format: 2025-10-17 10:07:27 WIB)
    '                                    Dim match As Match = Regex.Match(teks, "\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2} WIB")

    '                                    If match.Success Then
    '                                        Dim waktuPertama As String = match.Value.Replace(" WIB", "")
    '                                        Dim Rnd As New Random()

    '                                        Waktu = CDate(waktuPertama).AddMinutes(Rnd.Next(15, 20))

    '                                        If oSet_Antrian.UpdateDataIsChekedNewKeteranganTaksIdWaktu(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, CDate(Waktu)) = True Then
    '                                            Dim JsonRequest2 As String = oUpdateWaktuAntrian.fn_RequestUpdateWaktuAntreanLooping(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, CDate(Waktu))
    '                                            Dim uTime2 As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

    '                                            If JsonRequest2 <> "" Then
    '                                                Dim CodeMessage2 As String = oUpdateWaktuAntrian.fn_UpdateWaktuAntrean(JsonRequest2, uTime2)
    '                                                If CodeMessage2 <> "200" Then
    '                                                    If CodeMessage2 = "208-TaskId=" & sTaskId & " sudah ada" Then
    '                                                        fn_UpdateWaktu = True
    '                                                        oSet_Antrian.UpdateDataIsChekedNew(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, "200-Ok")
    '                                                        oSet_Antrian.UpdateDataIsChekedNewKeteranganTaksId(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, "200-Ok")
    '                                                    Else
    '                                                        oSet_Antrian.UpdateDataIsChekedNew(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, CodeMessage2)
    '                                                        oSet_Antrian.UpdateDataIsChekedNewKeteranganTaksId(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, CodeMessage2)
    '                                                    End If
    '                                                Else
    '                                                    fn_UpdateWaktu = True
    '                                                    oSet_Antrian.UpdateDataIsChekedNew(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, CodeMessage2 & "-" & "Ok")
    '                                                    oSet_Antrian.UpdateDataIsChekedNewKeteranganTaksId(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, CodeMessage2 & "-" & "Ok")
    '                                                End If
    '                                            Else
    '                                                oSet_Antrian.UpdateDataIsChekedNew(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, CodeMessage)
    '                                                oSet_Antrian.UpdateDataIsChekedNewKeteranganTaksId(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, CodeMessage)
    '                                            End If
    '                                        Else
    '                                            oSet_Antrian.UpdateDataIsChekedNew(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, CodeMessage)
    '                                            oSet_Antrian.UpdateDataIsChekedNewKeteranganTaksId(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, CodeMessage)
    '                                        End If
    '                                    Else
    '                                        oSet_Antrian.UpdateDataIsChekedNew(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, CodeMessage)
    '                                        oSet_Antrian.UpdateDataIsChekedNewKeteranganTaksId(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, CodeMessage)
    '                                    End If
    '                                Catch ex As Exception
    '                                    oSet_Antrian.UpdateDataIsChekedNew(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, CodeMessage)
    '                                    oSet_Antrian.UpdateDataIsChekedNewKeteranganTaksId(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, CodeMessage)
    '                                End Try
    '                            Else
    '                                oSet_Antrian.UpdateDataIsChekedNew(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, CodeMessage)
    '                                oSet_Antrian.UpdateDataIsChekedNewKeteranganTaksId(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, CodeMessage)
    '                            End If
    '                        End If
    '                    Else
    '                        fn_UpdateWaktu = sLanjut
    '                        oSet_Antrian.UpdateDataIsChekedNew(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, CodeMessage & "-" & "Ok")
    '                        oSet_Antrian.UpdateDataIsChekedNewKeteranganTaksId(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, CodeMessage & "-" & "Ok")
    '                    End If
    '                End If
    '            Else
    '                fn_UpdateWaktu = sPasienLama
    '            End If
    '        Else
    '            If isPesan = True Then
    '                MsgBox("data tidak ada di database", MsgBoxStyle.Exclamation, Me.Text)
    '            End If
    '        End If
    '    Catch oErr As Exception
    '        fn_UpdateWaktu = False
    '        If isPesan = True Then
    '            MsgBox("Kode Booking " & KODEBOOKING & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '        End If
    '    End Try
    'End Function
#Region "Master Detail"
    Private Sub fn_LoadData()
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

            'SQL = "SELECT "
            'SQL &= "A.KODEBOOKING "
            'SQL &= ",A.JENISPASIEN_RS "
            'SQL &= ",A.JENISPASIEN "
            'SQL &= ",A.NOMORKARTU "
            'SQL &= ",A.NOHP "
            'SQL &= ",A.NIK "
            'SQL &= ",POLI = C.NAME_DISPLAY "
            'SQL &= ",DOKTER = B.NAME_DISPLAY "
            'SQL &= ",A.NORM "
            'SQL &= ",A.NOMORANTREAN "
            'SQL &= ",A.ISPANGGIL "
            'SQL &= ",A.ISONLINE "
            'SQL &= ",A.KDSKD "
            'SQL &= "FROM "
            'SQL &= "ANTRIAN A "
            'SQL &= "INNER JOIN M_DOCTOR B "
            'SQL &= "ON A.KDDOCTOR = B.KDDOCTOR "
            'SQL &= "INNER JOIN M_DEPARTMENT C "
            'SQL &= "ON A.KDDEPARTMENT = C.KDDEPARTMENT "
            'SQL &= "WHERE CONVERT(VARCHAR(8), A.TANGGALPERIKSA, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.TANGGALPERIKSA, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "


            SQL = "SELECT "
            'SQL &= "PENJAMIN = B.MEMO "
            SQL &= "A.KODEBOOKING "
            'SQL &= ",REGISTER = ISNULL((SELECT KDPENDAFTARAN FROM S_PENDAFTARAN_H WHERE A.KODEBOOKING = KODEBOOKING), '') "
            'SQL &= ",JENISPESERTA = ISNULL((SELECT BB.MEMO FROM S_PENDAFTARAN_H AA INNER JOIN M_DAFTAR_L1 BB ON AA.KDDAFTAR_L1 = BB.KDDAFTAR_L1 WHERE A.KODEBOOKING = AA.KODEBOOKING), '-') "
            SQL &= ",A.NORM "
            SQL &= ",NAMA = ISNULL((SELECT NAME_DISPLAY FROM M_CUSTOMER WHERE KDCUSTOMER = NORM), '') "
            SQL &= ",A.PASIENBARU "
            SQL &= ",A.JENISPASIEN_RS "
            SQL &= ",A.NOMORKARTU "
            SQL &= ",A.NIK "
            SQL &= ",A.NOHP "
            SQL &= ",A.NAMAPOLI "
            SQL &= ",A.NAMADOKTER "
            SQL &= ",A.NOMORREFERENSI "
            SQL &= ",A.NOMORANTREAN "
            SQL &= ",STATUSPANGGIL = (SELECT CASE A.ISPANGGIL WHEN 0 THEN 'BELUM DI PANGGIL' WHEN 1 THEN '1 mulai waktu tunggu admisi' WHEN 2 THEN '2 akhir waktu tunggu admisi/mulai waktu layan admisi' WHEN 3 THEN '3 akhir waktu layan admisi/mulai waktu tunggu poli' WHEN 4 THEN '4 akhir waktu tunggu poli/mulai waktu layan poli' WHEN 5 THEN '5 akhir waktu layan poli/mulai waktu tunggu farmasi' WHEN 6 THEN '6 akhir waktu tunggu farmasi/mulai waktu layan farmasi membuat obat' WHEN 7 THEN '7 akhir waktu obat selesai dibuat' WHEN 99 THEN '99 tidak hadir/batal' ELSE '-' END) "
            SQL &= ",A.KETERANGAN "
            SQL &= "FROM "
            SQL &= "SET_BOOKING_ANTRIAN A "
            'SQL &= "INNER JOIN M_PENJAMIN B "
            'SQL &= "ON A.KDPENJAMIN = B.KDPENJAMIN "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.TANGGALPERIKSA, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.TANGGALPERIKSA, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "ORDER BY A.KODEBOOKING "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "R_ANTRIAN_JKN")

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grd.MainView = grv
            grd.DataSource = ds.Tables("R_ANTRIAN_JKN")
            grd.ForceInitialize()
            fn_LoadFormatData()

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    'Private Sub fn_LoadDataOnline()
    '    Try
    '        Dim oConnMySql As New MySql.Data.MySqlClient.MySqlConnection
    '        Dim oCommMySql As New MySql.Data.MySqlClient.MySqlCommand
    '        Dim daMySql As MySql.Data.MySqlClient.MySqlDataAdapter
    '        Dim dsMySql As New DataSet
    '        Dim MYSQL As String

    '        dsMySql = New DataSet

    '        oConnMySql = New MySqlConnection(sConnMySql)

    '        If oConnMySql.State = ConnectionState.Closed Then
    '            oConnMySql.Open()
    '        End If

    '        MYSQL = "SELECT  "
    '        MYSQL &= "id "
    '        MYSQL &= ",nama "
    '        MYSQL &= ",dokter "
    '        MYSQL &= ",Spesialis "
    '        MYSQL &= ",date "
    '        MYSQL &= ",day "
    '        MYSQL &= ",status "
    '        MYSQL &= ",rm "
    '        MYSQL &= ",no_antrian "
    '        MYSQL &= ",lama "
    '        MYSQL &= ",jaminan "
    '        MYSQL &= ",no_ass "
    '        MYSQL &= ",rujukan "
    '        MYSQL &= ",skd "
    '        MYSQL &= ",fam_id "
    '        MYSQL &= "FROM daftar_online "
    '        MYSQL &= "WHERE date BETWEEN '" & deDATEFrom.DateTime.ToString("yyyy-MM-dd") & " 00:00:00' AND '" & deDATETo.DateTime.ToString("yyyy-MM-dd") & " 23:59:59' "

    '        oCommMySql.Connection = oConnMySql
    '        oCommMySql.CommandText = MYSQL
    '        oCommMySql.CommandTimeout = 120
    '        oCommMySql.CommandType = CommandType.Text

    '        daMySql = New MySql.Data.MySqlClient.MySqlDataAdapter(oCommMySql)
    '        daMySql.Fill(dsMySql, "R_HISTORYRI")

    '        If oConnMySql.State = ConnectionState.Open Then
    '            oConnMySql.Close()
    '        End If

    '        'If dsMySql.Tables("R_HISTORYRI").Rows.Count < 1 Then

    '        'End If

    '        grd.MainView = grv
    '        grd.DataSource = dsMySql.Tables("R_HISTORYRI")
    '        grd.ForceInitialize()

    '        fn_LoadFormatData()
    '    Catch oErr As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
    Private Sub fn_LoadData06()
        Try
            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim oDepartment As New Reference.clsDepartment
            Dim uTime As Integer = 0

            If sAntrol_ConsId <> "" Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                Dim dsSetKoneksi = oSetKoneksi.AntreanPerTanggal(sAntrol_Url, sAntrol_ConsId, sAntrol_SecreatKey, uTime, sAntrol_UserKey, deDATEFrom.DateTime.ToString("yyyy-MM-dd"))

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metadata").Item("code")) = True, "", allData.Item("metadata").Item("code"))
                    messageResponse = allData("metadata")("message").ToString

                    If CodeResponse = "200" Then
                        ' Dim DataDecrypt = oSetKoneksi.Decrypt(allData("response"), sAntrol_ConsId & sAntrol_SecreatKey & uTime)

                        Dim allDataZ = JsonConvert.DeserializeObject(oSetKoneksi.Decrypt(allData("response"), sAntrol_ConsId & sAntrol_SecreatKey & uTime))

                        Dim table As DataTable

                        table = New DataTable("M_RUJUKAN")

                        table.Columns.Add("jeniskunjungan")
                        table.Columns.Add("nomorreferensi")
                        table.Columns.Add("createdtime")
                        table.Columns.Add("KODEBOOKING")
                        table.Columns.Add("norekammedis")
                        table.Columns.Add("nik")
                        table.Columns.Add("nokapst")
                        table.Columns.Add("noantrean")
                        table.Columns.Add("kodepoli")
                        table.Columns.Add("sumberdata")
                        table.Columns.Add("estimasidilayani")
                        table.Columns.Add("kodedokter")
                        table.Columns.Add("jampraktek")
                        table.Columns.Add("nohp")
                        table.Columns.Add("tanggal")
                        table.Columns.Add("ispeserta")
                        table.Columns.Add("status")


                        For Each item In allDataZ
                            Dim jeniskunjungan As String = ""
                            Dim nomorreferensi As String = ""
                            Dim createdtime As String = ""
                            Dim kodebooking As String = ""
                            Dim norekammedis As String = ""
                            Dim nik As String = ""
                            Dim nokapst As String = ""
                            Dim noantrean As String = ""
                            Dim kodepoli As String = ""
                            Dim sumberdata As String = ""
                            Dim estimasidilayani As String = ""
                            Dim kodedokter As String = ""
                            Dim jampraktek As String = ""
                            Dim nohp As String = ""
                            Dim tanggal As String = ""
                            Dim ispeserta As String = ""
                            Dim status As String = ""

                            Try
                                jeniskunjungan = item("jeniskunjungan")
                                nomorreferensi = item("nomorreferensi")
                                createdtime = New DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(CLng(item("createdtime")) / 1000)
                                kodebooking = item("kodebooking")
                                norekammedis = item("norekammedis")
                                nik = item("nik")
                                nokapst = item("nokapst")
                                noantrean = item("noantrean")
                                kodepoli = item("kodepoli")
                                sumberdata = item("sumberdata")
                                estimasidilayani = New DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(CLng(item("estimasidilayani")) / 1000)
                                kodedokter = item("kodedokter")
                                jampraktek = item("jampraktek")
                                nohp = item("nohp")
                                tanggal = item("tanggal")
                                ispeserta = item("ispeserta")
                                status = item("status")
                            Catch oErr As Exception

                            End Try
                            Try
                                jeniskunjungan = item("jeniskunjungan")
                            Catch oErr As Exception

                            End Try
                            Try
                                nomorreferensi = item("nomorreferensi")
                            Catch oErr As Exception

                            End Try
                            Try
                                createdtime = New DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(CLng(item("createdtime")) / 1000)
                            Catch oErr As Exception

                            End Try
                            Try
                                kodebooking = item("kodebooking")
                            Catch oErr As Exception

                            End Try
                            Try
                                norekammedis = item("norekammedis")
                            Catch oErr As Exception

                            End Try
                            Try
                                nik = item("nik")
                            Catch oErr As Exception

                            End Try
                            Try
                                nokapst = item("nokapst")
                            Catch oErr As Exception

                            End Try
                            Try
                                noantrean = item("noantrean")
                            Catch oErr As Exception

                            End Try
                            Try
                                kodepoli = item("kodepoli")
                            Catch oErr As Exception

                            End Try
                            Try
                                sumberdata = item("sumberdata")
                            Catch oErr As Exception

                            End Try
                            Try
                                estimasidilayani = New DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(CLng(item("estimasidilayani")) / 1000)
                            Catch oErr As Exception

                            End Try
                            Try
                                kodedokter = item("kodedokter")
                            Catch oErr As Exception

                            End Try
                            Try
                                jampraktek = item("jampraktek")
                            Catch oErr As Exception

                            End Try
                            Try
                                nohp = item("nohp")
                            Catch oErr As Exception

                            End Try
                            Try
                                tanggal = item("tanggal")
                            Catch oErr As Exception

                            End Try
                            Try
                                ispeserta = item("ispeserta")
                            Catch oErr As Exception

                            End Try
                            Try
                                status = item("status")
                            Catch oErr As Exception

                            End Try

                            table.Rows.Add(New String() {jeniskunjungan, nomorreferensi, createdtime, kodebooking, norekammedis, nik, nokapst, noantrean, kodepoli, sumberdata, estimasidilayani, kodedokter, jampraktek, nohp, tanggal, ispeserta, status})

                        Next

                        grd.MainView = grv
                        grd.DataSource = table
                        grd.ForceInitialize()
                        fn_LoadFormatData()
                    End If
                Else
                    MsgBox("Referensi Jadwal Dokter", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadAntrianUmum()
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

            MYSQL = "select * from booking_registrasi where tanggal_periksa BETWEEN '" & deDATEFrom.DateTime.ToString("yyyy-MM-dd") & "' AND '" & deDATETo.DateTime.ToString("yyyy-MM-dd") & "' "

            oCommMySql.Connection = oConnMySql
            oCommMySql.CommandText = MYSQL
            oCommMySql.CommandTimeout = 120
            oCommMySql.CommandType = CommandType.Text

            daMySql = New MySql.Data.MySqlClient.MySqlDataAdapter(oCommMySql)
            daMySql.Fill(dsMySql, "updatebooking_registrasi")

            If oConnMySql.State = ConnectionState.Open Then
                oConnMySql.Close()
            End If

            grd.MainView = grv
            grd.DataSource = dsMySql.Tables("updatebooking_registrasi")
            grd.ForceInitialize()

            fn_LoadFormatData()

        Catch oErr As Exception
            MsgBox("Load Status Booking Umum Gagal" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
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

        grv.Columns("KODEBOOKING").Caption = "Nomor Booking"
    End Sub
    Private Sub grv_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grv.RowStyle
        If grv.IsFilterRow(e.RowHandle) Then Exit Sub
        If CDec(grv.GetRowCellValue(e.RowHandle, "TERSEDIA")) = 0 Then
            'e.Appearance.BackColor = Color.YellowGreen
        Else
            e.Appearance.BackColor = Color.LightGreen
        End If
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
        '    grv.RestoreLayoutFromRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text)
        'Catch ex As Exception

        'End Try
    End Sub
    Private Sub DatangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DatangToolStripMenuItem.Click
        If grv.GetFocusedRowCellValue("KODEBOOKING") Is Nothing Then
            Exit Sub
        End If

        Dim dsSet_Antrian_Simpan = oSet_Antrian.GetData(grv.GetFocusedRowCellValue("KODEBOOKING"))

        If dsSet_Antrian_Simpan IsNot Nothing Then
            If dsSet_Antrian_Simpan.PASIENBARU = "1" Then
                Dim dsWaktuTungguTaskId = oSet_Antrian.GetDataBySaveWaktuTunggu(dsSet_Antrian_Simpan.KODEBOOKING, 1)
                If dsWaktuTungguTaskId IsNot Nothing Then
                    If Not dsWaktuTungguTaskId.REMARKS.Contains("200-Ok") Then
                        fn_UpdateWaktuNew(dsSet_Antrian_Simpan.KODEBOOKING, 1, True)
                    Else
                        Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                        Dim oUpdateWaktuAntrian As New SettingAntrian_UI.clsSetAntrian_UI

                        Dim CodeMessage As String = oUpdateWaktuAntrian.fn_UpdateWaktuAntrean(oUpdateWaktuAntrian.fn_RequestUpdateWaktuAntreanLooping(dsSet_Antrian_Simpan.KODEBOOKING, 1, dsWaktuTungguTaskId.DATE), uTime)

                        MsgBox(CodeMessage, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    fn_UpdateWaktuNew(dsSet_Antrian_Simpan.KODEBOOKING, 1, True)
                End If
            Else
                MsgBox("Merupakan Pasien Lama SIlhkan di mulai dari Task Id 3", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Else
            MsgBox("Kode Booking Tidak di Temukan di Database SIMRS", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub SelesaiAdmisiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelesaiAdmisiToolStripMenuItem.Click
        If grv.GetFocusedRowCellValue("KODEBOOKING") Is Nothing Then
            Exit Sub
        End If

        Dim dsSet_Antrian_Simpan = oSet_Antrian.GetData(grv.GetFocusedRowCellValue("KODEBOOKING"))

        If dsSet_Antrian_Simpan IsNot Nothing Then
            If dsSet_Antrian_Simpan.PASIENBARU = "1" Then
                Dim dsWaktuTungguTaskId = oSet_Antrian.GetDataBySaveWaktuTunggu(dsSet_Antrian_Simpan.KODEBOOKING, 2)
                If dsWaktuTungguTaskId IsNot Nothing Then
                    If Not dsWaktuTungguTaskId.REMARKS.Contains("200-Ok") Then
                        fn_UpdateWaktuNew(dsSet_Antrian_Simpan.KODEBOOKING, 2, True)
                    Else
                        Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                        Dim oUpdateWaktuAntrian As New SettingAntrian_UI.clsSetAntrian_UI

                        Dim CodeMessage As String = oUpdateWaktuAntrian.fn_UpdateWaktuAntrean(oUpdateWaktuAntrian.fn_RequestUpdateWaktuAntreanLooping(dsSet_Antrian_Simpan.KODEBOOKING, 2, dsWaktuTungguTaskId.DATE), uTime)

                        MsgBox(CodeMessage, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    fn_UpdateWaktuNew(dsSet_Antrian_Simpan.KODEBOOKING, 2, True)
                End If
            Else
                MsgBox("Merupakan Pasien Lama SIlhkan di mulai dari Task Id 3", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Else
            MsgBox("Kode Booking Tidak di Temukan di Database SIMRS", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub PanggilPoliToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PanggilPoliToolStripMenuItem.Click
        If grv.GetFocusedRowCellValue("KODEBOOKING") Is Nothing Then
            Exit Sub
        End If

        Dim dsSet_Antrian_Simpan = oSet_Antrian.GetData(grv.GetFocusedRowCellValue("KODEBOOKING"))

        If dsSet_Antrian_Simpan IsNot Nothing Then
            Dim dsWaktuTungguTaskId = oSet_Antrian.GetDataBySaveWaktuTunggu(dsSet_Antrian_Simpan.KODEBOOKING, 3)
            If dsWaktuTungguTaskId IsNot Nothing Then
                If Not dsWaktuTungguTaskId.REMARKS.Contains("200-Ok") Then
                    fn_UpdateWaktuNew(dsSet_Antrian_Simpan.KODEBOOKING, 3, True)
                Else
                    Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                    Dim oUpdateWaktuAntrian As New SettingAntrian_UI.clsSetAntrian_UI

                    Dim CodeMessage As String = oUpdateWaktuAntrian.fn_UpdateWaktuAntrean(oUpdateWaktuAntrian.fn_RequestUpdateWaktuAntreanLooping(dsSet_Antrian_Simpan.KODEBOOKING, 3, dsWaktuTungguTaskId.DATE), uTime)

                    MsgBox(CodeMessage, MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_UpdateWaktuNew(dsSet_Antrian_Simpan.KODEBOOKING, 3, True)
            End If
        Else
            MsgBox("Kode Booking Tidak di Temukan di Database SIMRS", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub KeluarPoliToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KeluarPoliToolStripMenuItem.Click
        If grv.GetFocusedRowCellValue("KODEBOOKING") Is Nothing Then
            Exit Sub
        End If

        Dim dsSet_Antrian_Simpan = oSet_Antrian.GetData(grv.GetFocusedRowCellValue("KODEBOOKING"))

        If dsSet_Antrian_Simpan IsNot Nothing Then
            Dim dsWaktuTungguTaskId = oSet_Antrian.GetDataBySaveWaktuTunggu(dsSet_Antrian_Simpan.KODEBOOKING, 4)
            If dsWaktuTungguTaskId IsNot Nothing Then
                If Not dsWaktuTungguTaskId.REMARKS.Contains("200-Ok") Then
                    fn_UpdateWaktuNew(dsSet_Antrian_Simpan.KODEBOOKING, 4, True)
                Else
                    Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                    Dim oUpdateWaktuAntrian As New SettingAntrian_UI.clsSetAntrian_UI

                    Dim CodeMessage As String = oUpdateWaktuAntrian.fn_UpdateWaktuAntrean(oUpdateWaktuAntrian.fn_RequestUpdateWaktuAntreanLooping(dsSet_Antrian_Simpan.KODEBOOKING, 4, dsWaktuTungguTaskId.DATE), uTime)

                    MsgBox(CodeMessage, MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_UpdateWaktuNew(dsSet_Antrian_Simpan.KODEBOOKING, 4, True)
            End If
        Else
            MsgBox("Kode Booking Tidak di Temukan di Database SIMRS", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub DatangFarmasiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DatangFarmasiToolStripMenuItem.Click
        If grv.GetFocusedRowCellValue("KODEBOOKING") Is Nothing Then
            Exit Sub
        End If

        Dim dsSet_Antrian_Simpan = oSet_Antrian.GetData(grv.GetFocusedRowCellValue("KODEBOOKING"))

        If dsSet_Antrian_Simpan IsNot Nothing Then
            Dim dsWaktuTungguTaskId = oSet_Antrian.GetDataBySaveWaktuTunggu(dsSet_Antrian_Simpan.KODEBOOKING, 5)
            If dsWaktuTungguTaskId IsNot Nothing Then
                If Not dsWaktuTungguTaskId.REMARKS.Contains("200-Ok") Then
                    fn_UpdateWaktuNew(dsSet_Antrian_Simpan.KODEBOOKING, 5, True)
                Else
                    Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                    Dim oUpdateWaktuAntrian As New SettingAntrian_UI.clsSetAntrian_UI

                    Dim CodeMessage As String = oUpdateWaktuAntrian.fn_UpdateWaktuAntrean(oUpdateWaktuAntrian.fn_RequestUpdateWaktuAntreanLooping(dsSet_Antrian_Simpan.KODEBOOKING, 5, dsWaktuTungguTaskId.DATE), uTime)

                    MsgBox(CodeMessage, MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_UpdateWaktuNew(dsSet_Antrian_Simpan.KODEBOOKING, 5, True)
            End If
        Else
            MsgBox("Kode Booking Tidak di Temukan di Database SIMRS", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub PanggilFarmasiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PanggilFarmasiToolStripMenuItem.Click
        If grv.GetFocusedRowCellValue("KODEBOOKING") Is Nothing Then
            Exit Sub
        End If

        Dim dsSet_Antrian_Simpan = oSet_Antrian.GetData(grv.GetFocusedRowCellValue("KODEBOOKING"))

        If dsSet_Antrian_Simpan IsNot Nothing Then
            Dim dsWaktuTungguTaskId = oSet_Antrian.GetDataBySaveWaktuTunggu(dsSet_Antrian_Simpan.KODEBOOKING, 6)
            If dsWaktuTungguTaskId IsNot Nothing Then
                If Not dsWaktuTungguTaskId.REMARKS.Contains("200-Ok") Then
                    fn_UpdateWaktuNew(dsSet_Antrian_Simpan.KODEBOOKING, 6, True)
                Else
                    Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                    Dim oUpdateWaktuAntrian As New SettingAntrian_UI.clsSetAntrian_UI

                    Dim CodeMessage As String = oUpdateWaktuAntrian.fn_UpdateWaktuAntrean(oUpdateWaktuAntrian.fn_RequestUpdateWaktuAntreanLooping(dsSet_Antrian_Simpan.KODEBOOKING, 6, dsWaktuTungguTaskId.DATE), uTime)

                    MsgBox(CodeMessage, MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_UpdateWaktuNew(dsSet_Antrian_Simpan.KODEBOOKING, 6, True)
            End If
        Else
            MsgBox("Kode Booking Tidak di Temukan di Database SIMRS", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub DatangDanPanggilFarmasiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DatangDanPanggilFarmasiToolStripMenuItem.Click
        If grv.GetFocusedRowCellValue("KODEBOOKING") Is Nothing Then
            Exit Sub
        End If

        Dim dsSet_Antrian_Simpan = oSet_Antrian.GetData(grv.GetFocusedRowCellValue("KODEBOOKING"))

        If dsSet_Antrian_Simpan IsNot Nothing Then
            Dim dsWaktuTungguTaskId = oSet_Antrian.GetDataBySaveWaktuTunggu(dsSet_Antrian_Simpan.KODEBOOKING, 7)
            If dsWaktuTungguTaskId IsNot Nothing Then
                If Not dsWaktuTungguTaskId.REMARKS.Contains("200-Ok") Then
                    fn_UpdateWaktuNew(dsSet_Antrian_Simpan.KODEBOOKING, 7, True)
                Else
                    Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                    Dim oUpdateWaktuAntrian As New SettingAntrian_UI.clsSetAntrian_UI

                    Dim CodeMessage As String = oUpdateWaktuAntrian.fn_UpdateWaktuAntrean(oUpdateWaktuAntrian.fn_RequestUpdateWaktuAntreanLooping(dsSet_Antrian_Simpan.KODEBOOKING, 7, dsWaktuTungguTaskId.DATE), uTime)

                    MsgBox(CodeMessage, MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_UpdateWaktuNew(dsSet_Antrian_Simpan.KODEBOOKING, 7, True)
            End If
        Else
            MsgBox("Kode Booking Tidak di Temukan di Database SIMRS", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub TidakHadirBatalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TidakHadirBatalToolStripMenuItem.Click
        If grv.GetFocusedRowCellValue("KODEBOOKING") Is Nothing Then
            Exit Sub
        End If

        Dim oSet_Antrian_Simpan As New SettingAntrian.clsSetAntrian

        Dim dsSet_Antrian_Simpan = oSet_Antrian_Simpan.GetData(grv.GetFocusedRowCellValue("KODEBOOKING"))

        If dsSet_Antrian_Simpan IsNot Nothing Then
            oSet_Antrian_Simpan.UpdateDataIsCheked(dsSet_Antrian_Simpan.KODEBOOKING, 99, "")

            Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
            Dim oUpdateWaktuAntrian As New SettingAntrian_UI.clsSetAntrian_UI
            Dim JsonRequest As String = oUpdateWaktuAntrian.fn_RequestUpdateWaktuAntrean(dsSet_Antrian_Simpan.KODEBOOKING, 99, uTime)
            If JsonRequest <> "" Then
                Dim CodeMessage As String = oUpdateWaktuAntrian.fn_UpdateWaktuAntrean(JsonRequest, uTime)
                If CodeMessage <> "200" Then
                    MsgBox("Update Waktu Ke BPJS" & vbCrLf & CodeMessage, MsgBoxStyle.Information, Me.Text)
                End If
            End If
        End If
    End Sub
    Private Sub TidakHadirbataLoopingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TidakHadirbataLoopingToolStripMenuItem.Click
        If grv.GetFocusedRowCellValue("KODEBOOKING") Is Nothing Then
            Exit Sub
        End If

        If txtKETERANGAN.Text = "" Then
            MsgBox("Keterangan Kosong", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If

        Dim oSet_Antrian_Simpan As New SettingAntrian.clsSetAntrian

        Dim dsSet_Antrian_Simpan = oSet_Antrian_Simpan.GetData(grv.GetFocusedRowCellValue("KODEBOOKING"))

        If dsSet_Antrian_Simpan IsNot Nothing Then
            Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
            Dim oUpdateWaktuAntrian As New SettingAntrian_UI.clsSetAntrian_UI
            Dim JsonRequest As String = oUpdateWaktuAntrian.fn_RequestBatalAntrean(dsSet_Antrian_Simpan.KODEBOOKING, txtKETERANGAN.Text)
            If JsonRequest <> "" Then
                Dim CodeMessage As String = oUpdateWaktuAntrian.fn_BatalAntrean(JsonRequest, uTime)
                If CodeMessage <> "200" Then
                    MsgBox("Update Waktu Ke BPJS" & vbCrLf & CodeMessage, MsgBoxStyle.Information, Me.Text)
                Else
                    oSet_Antrian_Simpan.UpdateDataIsChekedNew(dsSet_Antrian_Simpan.KODEBOOKING, 99, txtKETERANGAN.Text)
                End If
            End If
        End If
    End Sub
    Private Sub BatalAntrianToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Try
            If grv.GetFocusedRowCellValue("KODEBOOKING") Is Nothing Then
                Exit Sub
            End If

            If txtKETERANGAN.Text = "" Then
                MsgBox("Keterangan Kosong", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            frmLoginDelete.ShowDialog()

            Dim Cek As Integer = 0
            Dim Hasil As Integer = 0

            For i As Integer = 0 To grv.RowCount - 1
                Cek += 1
                'sRetur = CDec(grvDetail_R.GetRowCellValue(i, colAMOUNTPAYMENT_R))
            Next

            If MsgBox("Apakah Yakin akan di Batal kan Antrian Sebanyak " & Cek & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

            Dim oSet_Antrian_Simpan As New SettingAntrian.clsSetAntrian

            For i As Integer = 0 To grv.RowCount - 1

                Dim dsSet_Antrian_Simpan = oSet_Antrian_Simpan.GetData(grv.GetRowCellValue(i, "KODEBOOKING"))

                If dsSet_Antrian_Simpan IsNot Nothing Then
                    Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                    Dim oUpdateWaktuAntrian As New SettingAntrian_UI.clsSetAntrian_UI
                    Dim JsonRequest As String = oUpdateWaktuAntrian.fn_RequestBatalAntrean(dsSet_Antrian_Simpan.KODEBOOKING, txtKETERANGAN.Text)
                    If JsonRequest <> "" Then
                        Dim CodeMessage As String = oUpdateWaktuAntrian.fn_BatalAntrean(JsonRequest, uTime)

                        If CodeMessage = "200" Then
                            Hasil = +1
                        End If

                        oSet_Antrian_Simpan.UpdateDataIsChekedNew(dsSet_Antrian_Simpan.KODEBOOKING, 99, CodeMessage)
                    Else
                        oSet_Antrian_Simpan.UpdateDataIsChekedGagal(dsSet_Antrian_Simpan.KODEBOOKING, "Gagal Json Req Kosong")
                    End If
                Else
                    oSet_Antrian_Simpan.UpdateDataIsChekedGagal(grv.GetRowCellValue(i, "KODEBOOKING"), "Gagal Kode Booking Kosong")
                End If
            Next

            MsgBox("Berhasil Looping " & Hasil & " dari " & Cek, MsgBoxStyle.Information, Me.Text)
        Catch ex As Exception
            MsgBox("batal antrian Looping" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub KirimJknMobileOnsiteToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Try
            If grv.GetFocusedRowCellValue("KODEBOOKING") Is Nothing Then
                Exit Sub
            End If

            If grv.GetFocusedRowCellValue("STATUSPANGGIL") <> "tidak hadir/batal" Then
                MsgBox("Status Bukan Batal Antrian", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            frmLoginDelete.ShowDialog()

            If MsgBox("Apakah Yakin akan di Tambah Antrian Onsite?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

            Dim sKdBooking As String = grv.GetFocusedRowCellValue("KODEBOOKING")
            Dim sKdBookingNew As String = "X" & sKdBooking

            Dim oSet_Antrian_Simpan As New SettingAntrian.clsSetAntrian

            Dim sPesan As String = fn_UpdateBookingAntrian(sKdBooking, sKdBookingNew)
            If sPesan <> "Berhasil" Then
                oSet_Antrian_Simpan.UpdateDataIsChekedNew(sKdBookingNew, 99, sPesan)
                MsgBox(sPesan, MsgBoxStyle.Exclamation, Me.Text)
            Else
                Dim HASIL = fn_AntrianOnsiteBPJS("X" & sKdBooking)

                If Not HASIL.Contains("200") Then
                    If HASIL.Contains("208 - Terdapat duplikasi Kode Booking") Then
                        oSet_Antrian_Simpan.UpdateDataIsChekedNew(sKdBookingNew, 0, "MOBILE JKN ONSITE DUPLIKASI")
                        MsgBox("Berhasil 208", MsgBoxStyle.Information, Me.Text)
                    Else
                        oSet_Antrian_Simpan.UpdateDataIsChekedNew(sKdBookingNew, 99, HASIL)
                        MsgBox("Kode Booking Gagal" & vbCrLf & HASIL, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    oSet_Antrian_Simpan.UpdateDataIsChekedNew(sKdBookingNew, 0, "MOBILE JKN ONSITE")
                    MsgBox("Berhasil 200", MsgBoxStyle.Information, Me.Text)
                End If
            End If
        Catch ex As Exception
            MsgBox("batal antrian Looping" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub KirimJknMobileOnsiteToolStripMenuItem1_Click(sender As Object, e As EventArgs)
        Try
            If grv.GetFocusedRowCellValue("KODEBOOKING") Is Nothing Then
                Exit Sub
            End If

            Dim CekNumber As Integer = 0
            Dim HasilNumber As Integer = 0

            For i As Integer = 0 To grv.RowCount - 1
                If grv.GetRowCellValue(i, "STATUSPANGGIL") = "tidak hadir/batal" Then
                    CekNumber += 1
                End If
                'sRetur = CDec(grvDetail_R.GetRowCellValue(i, colAMOUNTPAYMENT_R))
            Next

            If CekNumber < 1 Then
                MsgBox("Tidak Ada Status Batal Antrian", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            frmLoginDelete.ShowDialog()

            If MsgBox("Apakah Yakin akan di Tambah Antrian Onsite Sebanyak " & CekNumber & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

            Dim oSet_Antrian_Simpan As New SettingAntrian.clsSetAntrian

            For i As Integer = 0 To grv.RowCount - 1
                If grv.GetRowCellValue(i, "STATUSPANGGIL") = "tidak hadir/batal" Then
                    Dim sKdBooking As String = grv.GetRowCellValue(i, "KODEBOOKING")
                    Dim sKdBookingNew As String = "X" & sKdBooking

                    Dim sPesan As String = fn_UpdateBookingAntrian(sKdBooking, sKdBookingNew)

                    If sPesan <> "Berhasil" Then
                        oSet_Antrian_Simpan.UpdateDataIsChekedNew(sKdBookingNew, 99, sPesan)
                        'MsgBox(sPesan, MsgBoxStyle.Exclamation, Me.Text)
                    Else
                        Dim HASIL = fn_AntrianOnsiteBPJS("X" & sKdBooking)

                        If Not HASIL.Contains("200") Then
                            If HASIL.Contains("208 - Terdapat duplikasi Kode Booking") Then
                                oSet_Antrian_Simpan.UpdateDataIsChekedNew(sKdBookingNew, 0, "MOBILE JKN ONSITE DUPLIKASI")
                                'MsgBox("Berhasil 208", MsgBoxStyle.Information, Me.Text)
                            Else
                                oSet_Antrian_Simpan.UpdateDataIsChekedNew(sKdBookingNew, 99, sPesan)
                                'MsgBox("Kode Booking Gagal" & vbCrLf & HASIL, MsgBoxStyle.Exclamation, Me.Text)
                            End If
                        Else
                            oSet_Antrian_Simpan.UpdateDataIsChekedNew(sKdBookingNew, 0, "MOBILE JKN ONSITE")
                            'MsgBox("Berhasil 200", MsgBoxStyle.Information, Me.Text)
                        End If
                    End If
                End If
            Next

            fn_LoadSecurity()

        Catch ex As Exception
            MsgBox("batal antrian Looping" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_UpdateBookingAntrian(ByVal KODEBOOKING As String, ByVal KODEBOOKING_NEW As String) As String
        Try
            Dim oSet_Antrian_Simpan As New SettingAntrian.clsSetAntrian
            Dim oDoctor As New Reference.clsDoctor
            Dim sHARI As String = String.Empty

            Dim dsBooking = oSet_Antrian_Simpan.GetData(KODEBOOKING)
            If dsBooking IsNot Nothing Then
                Select Case Weekday(dsBooking.TANGGALPERIKSA)
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

                Dim dskddoctor = oDoctor.GetDataByKodeVclaim(dsBooking.KODEDOKTER)
                If dskddoctor Is Nothing Then
                    fn_UpdateBookingAntrian = "Kode Dokter Tidak Ada"
                    Exit Function
                End If

                Dim dsJawdwalDokter = oDoctor.GetDataDetailJadwalDokter(dskddoctor.KDDOCTOR, sHARI)

                If dsJawdwalDokter Is Nothing Then
                    fn_UpdateBookingAntrian = "Jadwal Dokter di SIMRS belum ada Untuk Dokter " & oDoctor.GetData(dsBooking.KODEDOKTER).NAME_DISPLAY & " di Hari " & sHARI & " Silahkan Lengkapi Jadwal Dokter di Referensi Dokter"
                    Exit Function
                End If

                ' ***** HEADER *****
                Dim ds = oSet_Antrian_Simpan.GetStructureHeader

                With ds
                    .DATECREATED = dsBooking.DATECREATED
                    .DATEUPDATED = Now
                    .KDPENJAMIN = dsBooking.KDPENJAMIN
                    .KODEBOOKING = KODEBOOKING_NEW
                    .JENISPASIEN_RS = dsBooking.JENISPASIEN_RS
                    .JENISPASIEN = dsBooking.JENISPASIEN
                    .NOMORKARTU = dsBooking.NOMORKARTU
                    .NOHP = dsBooking.NOHP
                    .NIK = dsBooking.NIK
                    .KODEPOLI = dsBooking.KODEPOLI
                    .NAMAPOLI = dsBooking.NAMAPOLI
                    .PASIENBARU = dsBooking.PASIENBARU
                    .NORM = dsBooking.NORM
                    .TANGGALPERIKSA = dsBooking.TANGGALPERIKSA
                    .TANGGALPERIKSA_TEXT = dsBooking.TANGGALPERIKSA_TEXT
                    .KODEDOKTER = dsBooking.KODEDOKTER
                    .NAMADOKTER = dsBooking.NAMADOKTER
                    .JAMPRAKTEK = dsJawdwalDokter.BUKA & "-" & dsJawdwalDokter.TUTUP
                    .JENISKUNJUNGAN = dsBooking.JENISKUNJUNGAN
                    .NOMORREFERENSI = dsBooking.NOMORREFERENSI
                    .NOMORANTREAN = dsBooking.NOMORANTREAN
                    .ANGKAANTREAN = dsBooking.ANGKAANTREAN

                    Dim estimasi As DateTime = DateTime.Parse(Now.ToString("yyyy-MM-dd") & " " & dsJawdwalDokter.BUKA)
                    .ESTIMASIDILAYANI = (estimasi.AddMinutes(dskddoctor.ESTIMASI_MENIT * oDoctor.GetDataMonitoringTotal(dsBooking.KODEPOLI, dsBooking.KODEDOKTER, dsBooking.TANGGALPERIKSA)).ToString("yyyy-MM-dd HH:mm"))
                    .SISAKUOTAJKN = IIf(dsJawdwalDokter.KAPASITASPASIEN_JKN - oDoctor.GetDataMonitoringSisaJKN(dsBooking.KODEPOLI, dsBooking.KODEDOKTER, dsBooking.TANGGALPERIKSA) < 0, 0, dsJawdwalDokter.KAPASITASPASIEN_JKN - oDoctor.GetDataMonitoringSisaJKN(dsBooking.KODEPOLI, dsBooking.KODEDOKTER, dsBooking.TANGGALPERIKSA))
                    .KUOTAJKN = dsJawdwalDokter.KAPASITASPASIEN_JKN
                    .SISAKUOTANONJKN = IIf(dsJawdwalDokter.KAPASITASPASIEN_NONJKN - oDoctor.GetDataMonitoringSisaNONJKN(dsBooking.KODEPOLI, dsBooking.KODEDOKTER, dsBooking.TANGGALPERIKSA) < 0, 0, dsJawdwalDokter.KAPASITASPASIEN_NONJKN - oDoctor.GetDataMonitoringSisaNONJKN(dsBooking.KODEPOLI, dsBooking.KODEDOKTER, dsBooking.TANGGALPERIKSA))
                    .KUOTANONJKN = dsJawdwalDokter.KAPASITASPASIEN_NONJKN

                    .ISPANGGIL = dsBooking.ISPANGGIL
                    .ISONLINE = dsBooking.ISONLINE
                    .KETERANGAN = dsBooking.KETERANGAN
                    .KDSKD = dsBooking.KDSKD
                End With

                If oSet_Antrian_Simpan.InsertDataBookingNew(ds) = True Then
                    fn_UpdateBookingAntrian = "Berhasil"
                Else
                    fn_UpdateBookingAntrian = "Gagal Update"
                End If
            Else
                fn_UpdateBookingAntrian = "Kode Booking Kosong"
            End If
        Catch oErr As Exception
            fn_UpdateBookingAntrian = oErr.Message
        End Try
    End Function
    Private Function fn_AntrianOnsiteBPJS(ByVal KODEBOOKING As String) As String
        fn_AntrianOnsiteBPJS = ""

        Try
            Dim jsonRequest As String = fn_RequestTambahAntrean(KODEBOOKING)

            Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

            If jsonRequest <> "" Then
                Dim HASIL = fn_TambahAntrean(jsonRequest, uTime)

                fn_AntrianOnsiteBPJS = HASIL

                'If Not HASIL.Contains("200") Then
                '    If HASIL.Contains("208 - Terdapat duplikasi Kode Booking") Then

                '    Else
                '        MsgBox("Antrian BPJS" & vbCrLf & HASIL, MsgBoxStyle.Information, Me.Text)
                '    End If
                'End If
            Else
                fn_AntrianOnsiteBPJS = "json tambah antrian kosong/IGD"
            End If
        Catch oErr As Exception
            MsgBox("Antrian Onsite : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_RequestTambahAntrean(ByVal KODEBOOKING As String) As String
        Try
            Dim oAntrian As New SettingAntrian.clsSetAntrian
            Dim jsonRequest As String = String.Empty

            Dim dsAntrian = oAntrian.GetData(KODEBOOKING)
            If dsAntrian IsNot Nothing Then
                If dsAntrian.KODEPOLI = "IGD" Then
                    fn_RequestTambahAntrean = ""
                    Exit Function
                End If

                Dim TanggalEstimasi As DateTime = Now
                TanggalEstimasi = dsAntrian.ESTIMASIDILAYANI & ":00"

                Dim uTime As String = (TanggalEstimasi.Subtract(New DateTime(1970, 1, 1))).TotalMilliseconds

                jsonRequest = " { "
                jsonRequest &= """kodebooking"": """ & dsAntrian.KODEBOOKING & ""","
                jsonRequest &= """jenispasien"": """ & dsAntrian.JENISPASIEN & """, "
                jsonRequest &= """nomorkartu"": """ & dsAntrian.NOMORKARTU & """, "
                jsonRequest &= """nik"": """ & dsAntrian.NIK.ToString & """, "
                jsonRequest &= """nohp"": """ & dsAntrian.NOHP.ToString.Trim & """, "
                'jsonRequest &= """kodepoli"": """ & dsAntrian.KODEPOLI & """, "
                jsonRequest &= """kodepoli"": """ & IIf(dsAntrian.KODEPOLI = "HDL", "INT", dsAntrian.KODEPOLI) & """, "
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
    Private Sub ListToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ListToolStripMenuItem.Click
        If grv.GetFocusedRowCellValue("KODEBOOKING") Is Nothing Then
            Exit Sub
        End If

        Dim oSet_Antrian_Simpan As New SettingAntrian.clsSetAntrian

        Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
        Dim oUpdateWaktuAntrian As New SettingAntrian_UI.clsSetAntrian_UI
        Dim JsonRequest As String = oUpdateWaktuAntrian.fn_RequestListWaktuTaskId(grv.GetFocusedRowCellValue("KODEBOOKING"))
        If JsonRequest <> "" Then
            'Dim DataDecrypt = oSetKoneksi.Decrypt(allData("response"), dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime)

            Dim CodeMessage As String = oUpdateWaktuAntrian.fn_ListWaktuTaskId(JsonRequest, uTime)
            MsgBox(CodeMessage, MsgBoxStyle.Information, Me.Text)
        End If

        'Dim dsSet_Antrian_Simpan = oSet_Antrian_Simpan.GetData(grv.GetFocusedRowCellValue("KODEBOOKING"))

        'If dsSet_Antrian_Simpan IsNot Nothing Then
        '    Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
        '    Dim oUpdateWaktuAntrian As New SettingAntrian_UI.clsSetAntrian_UI
        '    Dim JsonRequest As String = oUpdateWaktuAntrian.fn_RequestListWaktuTaskId(dsSet_Antrian_Simpan.KODEBOOKING)
        '    If JsonRequest <> "" Then
        '        'Dim DataDecrypt = oSetKoneksi.Decrypt(allData("response"), dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime)

        '        Dim CodeMessage As String = oUpdateWaktuAntrian.fn_ListWaktuTaskId(JsonRequest, uTime)
        '        MsgBox(CodeMessage, MsgBoxStyle.Information, Me.Text)
        '    End If
        'End If
    End Sub
    'Private Sub CekDataPasienToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CekDataPasienToolStripMenuItem.Click
    '    If cboTYPE.SelectedIndex = 1 Then
    '        If grv.GetFocusedRowCellValue("fam_id") Is Nothing Then
    '            Exit Sub
    '        End If

    '        frmReportKeluarga.fn_Loadme(grv.GetFocusedRowCellValue("fam_id"))
    '        frmReportKeluarga.ShowDialog(Me)
    '    Else
    '        MsgBox("Silahkan Pilih Type Online", MsgBoxStyle.Information, Me.Text)
    '    End If
    'End Sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        frmReportWebAntrian.ShowDialog(Me)
    End Sub
    Private Sub cboTYPE_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTYPE.SelectedIndexChanged
        'LoopingToolStripMenuItem.Visible = False
        'TidakHadirbataLoopingToolStripMenuItem.Visible = False
        'KirimJknMobileOnsiteToolStripMenuItem.Visible = False
        btnLoopingAntrian.Visible = False
        btnLengkapiData.Visible = False
        SimpleButton2.Visible = False

        If cboTYPE.SelectedIndex = 1 Then
            'LabelControl1.Text = "Tanggal"
            LabelControl2.Visible = False
            LabelControl8.Visible = False
            deDATETo.Visible = False
            SimpleButton1.Visible = False
            txtKETERANGAN.Visible = True
            btnLoopingAntrian.Visible = True
            btnLengkapiData.Visible = True
            SimpleButton2.Visible = True
        Else
            'LabelControl1.Text = "Dari Tanggal"
            LabelControl2.Visible = True
            LabelControl8.Visible = True
            deDATETo.Visible = True
            SimpleButton1.Visible = True
            txtKETERANGAN.Visible = True
        End If

        If cboTYPE.SelectedIndex = 0 Then
            'LoopingToolStripMenuItem.Visible = True
            'TidakHadirbataLoopingToolStripMenuItem.Visible = True
            'KirimJknMobileOnsiteToolStripMenuItem.Visible = True
        End If
    End Sub
    Private Sub grv_DoubleClick(sender As Object, e As EventArgs) Handles grv.DoubleClick
        If cboTYPE.SelectedIndex = 0 Then
            If grv.GetFocusedRowCellValue("KODEBOOKING") Is Nothing Then
                Exit Sub
            End If

            sCOPYKODDEBOOKING = grv.GetFocusedRowCellValue("KODEBOOKING")
            Me.Close()
        End If
    End Sub
    Private Sub btnLengkapiData_Click(sender As Object, e As EventArgs) Handles btnLengkapiData.Click
        If cboTYPE.SelectedIndex = 1 Then
            Try
                Dim counter As Integer = 0
                Dim counterAll As Integer = 0

                For i As Integer = 0 To grv.RowCount - 1
                    If grv.GetRowCellValue(i, "status") <> "Batal" Then
                        counterAll += 1
                    End If
                Next

                If MsgBox("Apakah Yakin akan di Lengkapi Data Antrian Sebanyak " & counterAll & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

                SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

                For i As Integer = 0 To grv.RowCount - 1
                    If grv.GetRowCellValue(i, "status") <> "Batal" Then
                        Create_KodeBooking(grv.GetRowCellValue(i, "KODEBOOKING"), grv.GetRowCellValue(i, "nokapst"), grv.GetRowCellValue(i, "nohp"), grv.GetRowCellValue(i, "nik"), grv.GetRowCellValue(i, "kodepoli"), 0, grv.GetRowCellValue(i, "norekammedis"), grv.GetRowCellValue(i, "kodedokter"), grv.GetRowCellValue(i, "jampraktek"), grv.GetRowCellValue(i, "jeniskunjungan"), grv.GetRowCellValue(i, "nomorreferensi"), grv.GetRowCellValue(i, "noantrean"), 0, CDate(grv.GetRowCellValue(i, "estimasidilayani")), grv.GetRowCellValue(i, "sumberdata"))
                        SplashScreenManager.Default.SetWaitFormCaption("Processing data " & counter & " of " & counterAll & "")
                        counter += 1
                    End If
                Next
            Catch oErr As Exception
                MsgBox("Looping" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                SplashScreenManager.CloseForm(False)
            End Try
        Else
            MsgBox("Silahkan Pilih Type ke 2", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub btnLoopingAntrian_Click(sender As Object, e As EventArgs) Handles btnLoopingAntrian.Click
        Try
            Dim counter As Integer = 0
            Dim counterAll As Integer = 0

            For i As Integer = 0 To grv.RowCount - 1
                If grv.GetRowCellValue(i, "status") <> "Batal" Then
                    counterAll += 1
                End If
            Next

            If MsgBox("Apakah Yakin akan di Kirim kan Antrian Sebanyak " & counterAll & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            For i As Integer = 0 To grv.RowCount - 1
                If grv.GetRowCellValue(i, "status") <> "Batal" Then
                    Dim dsSet_Antrian_Simpan = oSet_Antrian.GetData(grv.GetRowCellValue(i, "KODEBOOKING"))
                    If dsSet_Antrian_Simpan IsNot Nothing Then
                        If dsSet_Antrian_Simpan.PASIENBARU = "1" Then
                            Dim dsLastKirimBPJS = oSet_Antrian.GetDataBySaveWaktuTungguLastKirimBPJS(dsSet_Antrian_Simpan.KODEBOOKING)

                            If dsLastKirimBPJS IsNot Nothing Then
                                For y As Integer = dsLastKirimBPJS.ISPANGGIL + 1 To 7
                                    If fn_UpdateWaktuNew(dsSet_Antrian_Simpan.KODEBOOKING, y, False) = False Then
                                        Exit For
                                    End If
                                Next
                            Else
                                For y As Integer = 1 To 7
                                    If fn_UpdateWaktuNew(dsSet_Antrian_Simpan.KODEBOOKING, y, False) = False Then
                                        Exit For
                                    End If
                                Next
                            End If
                        Else
                            Dim dsLastKirimBPJS = oSet_Antrian.GetDataBySaveWaktuTungguLastKirimBPJSPasienLama(dsSet_Antrian_Simpan.KODEBOOKING)

                            If dsLastKirimBPJS IsNot Nothing Then
                                For y As Integer = dsLastKirimBPJS.ISPANGGIL + 1 To 7
                                    If fn_UpdateWaktuNew(dsSet_Antrian_Simpan.KODEBOOKING, y, False) = False Then
                                        Exit For
                                    End If
                                Next
                            Else
                                For y As Integer = 3 To 7
                                    If fn_UpdateWaktuNew(dsSet_Antrian_Simpan.KODEBOOKING, y, False) = False Then
                                        Exit For
                                    End If
                                Next
                            End If
                        End If
                    End If

                    SplashScreenManager.Default.SetWaitFormCaption("Processing data " & counter & " of " & counterAll & "")
                    counter += 1
                End If
            Next
        Catch oErr As Exception
            MsgBox("Looping" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try
    End Sub
    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        Try
            Dim counter As Integer = 0
            Dim counterAll As Integer = 0

            For i As Integer = 0 To grv.RowCount - 1
                If grv.GetRowCellValue(i, "status") <> "Batal" Then
                    counterAll += 1
                End If
            Next

            If MsgBox("Apakah Yakin akan di Kirim kan Antrian Sebanyak " & counterAll & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            For i As Integer = 0 To grv.RowCount - 1
                If grv.GetRowCellValue(i, "status") <> "Batal" Then
                    Dim dsSet_Antrian_Simpan = oSet_Antrian.GetData(grv.GetRowCellValue(i, "KODEBOOKING"))
                    If dsSet_Antrian_Simpan IsNot Nothing Then
                        If dsSet_Antrian_Simpan.PASIENBARU = "1" Then
                            For y As Integer = 1 To 7
                                If fn_UpdateWaktuNew(dsSet_Antrian_Simpan.KODEBOOKING, y, False) = False Then
                                    Exit For
                                End If
                            Next
                        Else
                            For y As Integer = 3 To 7
                                If fn_UpdateWaktuNew(dsSet_Antrian_Simpan.KODEBOOKING, y, False) = False Then
                                    Exit For
                                End If
                            Next
                        End If
                    End If

                    SplashScreenManager.Default.SetWaitFormCaption("Processing data " & counter & " of " & counterAll & "")
                    counter += 1
                End If
            Next
        Catch oErr As Exception
            MsgBox("Looping" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try
    End Sub
    Private Function fn_UpdateWaktuNew(ByVal KODEBOOKING As String, ByVal sTaskId As Integer, ByVal isPesan As Boolean) As Boolean
        Try
            fn_UpdateWaktuNew = False

            Dim dsSet_Antrian_Simpan = oSet_Antrian.GetData(KODEBOOKING)

            Dim dsWaktuTungguTaskId = oSet_Antrian.GetDataBySaveWaktuTunggu(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId)

            If dsWaktuTungguTaskId IsNot Nothing Then
                If Not dsWaktuTungguTaskId.REMARKS.Contains("200-Ok") Then
                    Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                    Dim oUpdateWaktuAntrian As New SettingAntrian_UI.clsSetAntrian_UI

                    Dim CodeMessage As String = oUpdateWaktuAntrian.fn_UpdateWaktuAntrean(oUpdateWaktuAntrian.fn_RequestUpdateWaktuAntreanLooping(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, dsWaktuTungguTaskId.DATE), uTime)
                    If CodeMessage <> "200" Then
                        If CodeMessage = "208-TaskId=" & sTaskId & " sudah ada" Then
                            oSet_Antrian.UpdateDataIsChekedNew(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, CodeMessage & "-" & "Ok")
                            oSet_Antrian.UpdateDataIsChekedNewKeteranganTaksId(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, CodeMessage & "-" & "Ok")
                        Else
                            oSet_Antrian.UpdateDataIsChekedNew(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, CodeMessage)
                            oSet_Antrian.UpdateDataIsChekedNewKeteranganTaksId(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, CodeMessage)
                        End If
                    Else
                        oSet_Antrian.UpdateDataIsChekedNew(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, CodeMessage & "-" & "Ok")
                        oSet_Antrian.UpdateDataIsChekedNewKeteranganTaksId(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, CodeMessage & "-" & "Ok")
                        fn_UpdateWaktuNew = True
                    End If

                    If isPesan = True Then
                        MsgBox(CodeMessage, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                    Dim oUpdateWaktuAntrian As New SettingAntrian_UI.clsSetAntrian_UI

                    Dim CodeMessage As String = oUpdateWaktuAntrian.fn_UpdateWaktuAntrean(oUpdateWaktuAntrian.fn_RequestUpdateWaktuAntreanLooping(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, dsWaktuTungguTaskId.DATE), uTime)

                    If CodeMessage <> "200" Then
                        If CodeMessage = "208-TaskId=" & sTaskId & " sudah ada" Then
                            fn_UpdateWaktuNew = True
                        End If
                    Else
                        fn_UpdateWaktuNew = True
                    End If
                End If
            Else
                If sTaskId = 1 Then
                    Dim dsWaktuTungguTaskId3 = oSet_Antrian.GetDataBySaveWaktuTunggu(dsSet_Antrian_Simpan.KODEBOOKING, 3)

                    If dsWaktuTungguTaskId3 IsNot Nothing Then
                        Try
                            ' ***** HEADER *****
                            Dim ds = oSet_Antrian.GetStructureHeaderWaktuTunggu
                            With ds
                                Dim oData As New Grouper.clsR_Identitas_Grouper_Data

                                Dim WaktuServer As DateTime = dsWaktuTungguTaskId3.DATE

                                WaktuServer = WaktuServer.AddMinutes(-20)

                                .DATECREATED = WaktuServer
                                .DATEUPDATED = WaktuServer
                                .KODEBOOKING = dsSet_Antrian_Simpan.KODEBOOKING
                                .ISPANGGIL = 1
                                .DATE = WaktuServer
                                .DATE_TEXT = WaktuServer.ToString("ddMMyyyy")
                                .KDCUSTOMER = ""
                                .REMARKS = "INSERT"
                            End With

                            Dim kode As String = oSet_Antrian.InsertDataWaktuTungguSemuaKesini(ds)
                            If kode <> "" Then
                                fn_UpdateWaktuNew = True

                                Dim ds2 = oSet_Antrian.GetStructureHeaderWaktuTunggu
                                With ds2
                                    Dim oData As New Grouper.clsR_Identitas_Grouper_Data

                                    Dim WaktuServer As DateTime = dsWaktuTungguTaskId3.DATE

                                    WaktuServer = WaktuServer.AddMinutes(-10)

                                    .DATECREATED = WaktuServer
                                    .DATEUPDATED = WaktuServer
                                    .KODEBOOKING = dsSet_Antrian_Simpan.KODEBOOKING
                                    .ISPANGGIL = 2
                                    .DATE = WaktuServer
                                    .DATE_TEXT = WaktuServer.ToString("ddMMyyyy")
                                    .KDCUSTOMER = ""
                                    .REMARKS = "INSERT"
                                End With

                                Dim descek = oSet_Antrian.GetDataBySaveWaktuTunggu(dsSet_Antrian_Simpan.KODEBOOKING, 2)
                                If descek Is Nothing Then
                                    Dim kode2 As String = oSet_Antrian.InsertDataWaktuTungguSemuaKesini(ds2)
                                    If kode2 <> "" Then
                                        fn_UpdateWaktuNew = True
                                    Else
                                        fn_UpdateWaktuNew = False
                                        oSet_Antrian.UpdateDataIsChekedNew(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, "Waktu Tunggu Antrian Tidak diTemukan untuk Task Id " & sTaskId & " Silahkan Minimal TaskId 3 Ada")

                                        If isPesan = True Then
                                            MsgBox("Waktu Tunggu Antrian Tidak diTemukan untuk Task Id " & sTaskId, MsgBoxStyle.Exclamation, Me.Text)
                                        End If
                                    End If
                                End If
                            Else
                                fn_UpdateWaktuNew = False
                                oSet_Antrian.UpdateDataIsChekedNew(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, "Waktu Tunggu Antrian Tidak diTemukan untuk Task Id " & sTaskId & " Silahkan Minimal TaskId 3 Ada")

                                If isPesan = True Then
                                    MsgBox("Waktu Tunggu Antrian Tidak diTemukan untuk Task Id " & sTaskId, MsgBoxStyle.Exclamation, Me.Text)
                                End If
                            End If


                        Catch oErr As Exception
                             fn_UpdateWaktuNew = False
                            If isPesan = True Then
                                MsgBox("Kode Booking " & KODEBOOKING & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                            End If
                        End Try

                        If fn_UpdateWaktuNew = True Then
                            Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                            Dim oUpdateWaktuAntrian As New SettingAntrian_UI.clsSetAntrian_UI

                            Dim CodeMessage As String = oUpdateWaktuAntrian.fn_UpdateWaktuAntrean(oUpdateWaktuAntrian.fn_RequestUpdateWaktuAntreanLooping(dsSet_Antrian_Simpan.KODEBOOKING, 1, dsWaktuTungguTaskId3.DATE.AddMinutes(-20)), uTime)
                            If CodeMessage <> "200" Then
                                If CodeMessage = "208-TaskId=" & 1 & " sudah ada" Then
                                    oSet_Antrian.UpdateDataIsChekedNew(dsSet_Antrian_Simpan.KODEBOOKING, 1, CodeMessage & "-" & "Ok")
                                    oSet_Antrian.UpdateDataIsChekedNewKeteranganTaksId(dsSet_Antrian_Simpan.KODEBOOKING, 1, CodeMessage & "-" & "Ok")
                                Else
                                    oSet_Antrian.UpdateDataIsChekedNew(dsSet_Antrian_Simpan.KODEBOOKING, 1, CodeMessage)
                                    oSet_Antrian.UpdateDataIsChekedNewKeteranganTaksId(dsSet_Antrian_Simpan.KODEBOOKING, 1, CodeMessage)
                                End If
                            Else
                                oSet_Antrian.UpdateDataIsChekedNew(dsSet_Antrian_Simpan.KODEBOOKING, 1, CodeMessage & "-" & "Ok")
                                oSet_Antrian.UpdateDataIsChekedNewKeteranganTaksId(dsSet_Antrian_Simpan.KODEBOOKING, 1, CodeMessage & "-" & "Ok")
                                fn_UpdateWaktuNew = True
                            End If

                            If isPesan = True Then
                                MsgBox(CodeMessage, MsgBoxStyle.Exclamation, Me.Text)
                            End If
                        End If

                    Else
                        oSet_Antrian.UpdateDataIsChekedNew(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, "Waktu Tunggu Antrian Tidak diTemukan untuk Task Id " & sTaskId & " Silahkan Minimal TaskId 3 Ada")

                        If isPesan = True Then
                            MsgBox("Waktu Tunggu Antrian Tidak diTemukan untuk Task Id " & sTaskId, MsgBoxStyle.Exclamation, Me.Text)
                        End If
                    End If
                Else
                    oSet_Antrian.UpdateDataIsChekedNew(dsSet_Antrian_Simpan.KODEBOOKING, sTaskId, "Waktu Tunggu Antrian Tidak diTemukan untuk Task Id " & sTaskId)

                    If isPesan = True Then
                        MsgBox("Waktu Tunggu Antrian Tidak diTemukan untuk Task Id " & sTaskId, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                End If
            End If
        Catch oErr As Exception
            If isPesan = True Then
                MsgBox("Kode Booking " & KODEBOOKING & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End If
        End Try
    End Function
    Private Sub btnCekData_Click(sender As Object, e As EventArgs) Handles btnCekData.Click
        If btnCekData.Text = "Lihat Data" Then
            btnCekData.Text = "Tutup Data"
            lDETAIL_HEADER.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lDETAIL_HEADER_label.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            btnCekData.Text = "Lihat Data"
            lDETAIL_HEADER.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lDETAIL_HEADER_label.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub
    Private Sub grv_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grv.FocusedRowChanged
        If grv.GetFocusedRowCellValue("KODEBOOKING") Is Nothing Then
            grvDetail.Columns.Clear()
            grdDetail.DataSource = Nothing
            grvDetail.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            grvDetailBPJS.Columns.Clear()
            grdDetailBPJS.DataSource = Nothing
            grvDetailBPJS.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            Exit Sub
        End If


        If btnCekData.Text <> "Lihat Data" Then

            Dim dsSetBooking = oSet_Antrian.GetData(grv.GetFocusedRowCellValue("KODEBOOKING"))
            If dsSetBooking IsNot Nothing Then
                lblSetBooking.Text = dsSetBooking.KETERANGAN
            End If

            fn_GetDataWaktuTunggu(grv.GetFocusedRowCellValue("KODEBOOKING"))
            fn_ListBPJS(grv.GetFocusedRowCellValue("KODEBOOKING"), False)
        End If
        'If chkTANPATASKID.Checked = True Then

        'Else
        '    'grvDetailBPJS.OptionsSelection.MultiSelect = True
        '    'grvDetailBPJS.SelectAll()
        '    'grvDetailBPJS.DeleteSelectedRows()
        '    'grvDetailBPJS.OptionsSelection.MultiSelect = False
        '    grvDetailBPJS.Columns.Clear()
        '    grdDetailBPJS.DataSource = Nothing
        '    grvDetailBPJS.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        'End If
    End Sub
    Private Sub fn_GetDataWaktuTunggu(ByVal KODEBOOKING As String)
        If KODEBOOKING = "" Then Exit Sub

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
            SQL &= "A.KODEBOOKING "
            SQL &= ",WAKTU = A.DATE "
            SQL &= ",A.ISPANGGIL "
            SQL &= ",A.REMARKS "
            SQL &= "FROM "
            SQL &= "SET_WAKTUTUNGGU A "
            SQL &= "WHERE "
            SQL &= "A.KODEBOOKING = '" & KODEBOOKING & "' "
            SQL &= "ORDER BY "
            SQL &= "A.ISPANGGIL "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "GETDATAWAKTUTUNGGU")

            grdDetail.MainView = grvDetail
            grdDetail.DataSource = ds.Tables("GETDATAWAKTUTUNGGU")
            grdDetail.ForceInitialize()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            fn_LoadFormatDataWaktuTunggu()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_ListBPJS(ByVal KODEBOOKING As String, ByVal lengkapidata As Boolean)
        Try
            If KODEBOOKING = "" Then Exit Sub

            grvDetailBPJS.Columns.Clear()
            grdDetailBPJS.DataSource = Nothing

            Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
            Dim oUpdateWaktuAntrian As New SettingAntrian_UI.clsSetAntrian_UI
            Dim JsonRequest As String = oUpdateWaktuAntrian.fn_RequestListWaktuTaskId(KODEBOOKING)

            If JsonRequest <> "" Then
                Dim Hasil As String = oUpdateWaktuAntrian.fn_ListWaktuTaskId(JsonRequest, uTime)
                Try
                    Dim allDataZ = JsonConvert.DeserializeObject(Hasil)

                    Dim table As DataTable

                    table = New DataTable("M_RUJUKAN")

                    table.Columns.Add("kodebooking")
                    table.Columns.Add("wakturs")
                    table.Columns.Add("taskid")
                    table.Columns.Add("taskname")

                    For Each item In allDataZ
                        table.Rows.Add(New String() {item("kodebooking"), item("wakturs"), item("taskid"), item("taskname")})

                        If lengkapidata = True Then
                            Dim dsSet_Antrian_Simpan = oSet_Antrian.GetData(KODEBOOKING)

                            If dsSet_Antrian_Simpan IsNot Nothing Then
                                'TaksId 3
                                Dim sKDPENDAFTARAN As String = String.Empty

                                Dim dsKodeBookingPendaftaran = oDaftar.GetDataPendaftaranByKodeBooking(dsSet_Antrian_Simpan.KODEBOOKING)
                                Dim WaktuId3 As DateTime = Now

                                If dsKodeBookingPendaftaran Is Nothing Then
                                    WaktuId3 = CDate(item("wakturs"))
                                Else
                                    sKDPENDAFTARAN = dsKodeBookingPendaftaran.KDPENDAFTARAN

                                    Dim dsWaktuPanggil3 = oSet_Antrian.GetDataBySaveWaktuTunggu(dsSet_Antrian_Simpan.KODEBOOKING, 3)

                                    If dsWaktuPanggil3 Is Nothing Then
                                        WaktuId3 = CDate(item("wakturs"))
                                    Else
                                        WaktuId3 = dsWaktuPanggil3.DATE
                                    End If
                                End If

                                frmErmList.fn_TaskIDyangLalu(dsSet_Antrian_Simpan.KODEBOOKING, 3, WaktuId3)

                                Dim dsWaktuPanggil4 = oSet_Antrian.GetDataBySaveWaktuTunggu(dsSet_Antrian_Simpan.KODEBOOKING, 4)
                                Dim WaktuId4 As DateTime = WaktuId3

                                Dim Rnd As New Random()
                                WaktuId4 = WaktuId3.AddMinutes(Rnd.Next(15, 20))

                                If dsWaktuPanggil4 Is Nothing Then
                                    Dim dsKunjungan = oDaftar.GetDataKunjunganByKodepndafatran(sKDPENDAFTARAN)

                                    If dsKunjungan IsNot Nothing Then
                                        Dim dsCPPT = oCPPT.GetDataByKdKunjunganCPPT(dsKunjungan.KDKUNJUNGAN)
                                        If dsCPPT IsNot Nothing Then
                                            frmErmList.fn_TaskIDyangLalu(dsSet_Antrian_Simpan.KODEBOOKING, 4, dsCPPT.DATE)
                                        Else
                                            frmErmList.fn_TaskIDyangLalu(dsSet_Antrian_Simpan.KODEBOOKING, 4, WaktuId4)
                                        End If
                                    Else
                                        frmErmList.fn_TaskIDyangLalu(dsSet_Antrian_Simpan.KODEBOOKING, 4, WaktuId4)
                                    End If
                                Else
                                    frmErmList.fn_TaskIDyangLalu(dsSet_Antrian_Simpan.KODEBOOKING, 4, WaktuId4)
                                End If

                                frmErmList.fn_TaskIDyangLalu(dsSet_Antrian_Simpan.KODEBOOKING, 5, WaktuId4.AddMinutes(Rnd.Next(10, 20)))

                            End If
                        End If
                    Next

                    grdDetailBPJS.MainView = grvDetailBPJS
                    grdDetailBPJS.DataSource = table
                    grdDetailBPJS.ForceInitialize()

                    grvDetailBPJS.Columns("kodebooking").VisibleIndex = -1

                    fn_LoadFormatDataWaktuTunggu()
                Catch ex As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & Hasil, MsgBoxStyle.Exclamation, Me.Text)
                End Try

            End If
        Catch ex As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatDataWaktuTunggu()
        For iLoop As Integer = 0 To grvDetail.Columns.Count - 1
            If grvDetail.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvDetail.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvDetail.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grvDetail.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvDetail.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvDetail.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvDetail.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm:ss}"
            End If
        Next
        For iLoop As Integer = 0 To grvDetailBPJS.Columns.Count - 1
            If grvDetailBPJS.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvDetailBPJS.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvDetailBPJS.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grvDetailBPJS.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvDetailBPJS.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvDetailBPJS.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvDetailBPJS.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm}"
            End If
        Next

        If grvDetail.Columns("KODEBOOKING") IsNot Nothing Then
            grvDetail.Columns("KODEBOOKING").VisibleIndex = -1
            grvDetail.Columns("WAKTU").Caption = "wakturs"
            grvDetail.Columns("ISPANGGIL").Caption = "taskid"
            'grvDetail.Columns("KETERANGAN").Caption = "taskname"
        End If
    End Sub
    Private Sub WaktuTungguToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WaktuTungguToolStripMenuItem.Click
        If grv.GetFocusedRowCellValue("KODEBOOKING") Is Nothing Then
            Exit Sub
        End If

        Dim dsSet_Antrian_Simpan = oSet_Antrian.GetData(grv.GetFocusedRowCellValue("KODEBOOKING"))

        If dsSet_Antrian_Simpan IsNot Nothing Then
            Dim frmWaktuTunggu As New frmWaktuTunggu
            Try
                frmWaktuTunggu.LoadMe(grv.GetFocusedRowCellValue("KODEBOOKING"))
                frmWaktuTunggu.ShowDialog(Me)
                fn_GetDataWaktuTunggu(grv.GetFocusedRowCellValue("KODEBOOKING"))
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmWaktuTunggu Is Nothing Then frmWaktuTunggu.Dispose()
                frmWaktuTunggu = Nothing
            End Try
        Else
            MsgBox("Kode Booking Tidak di Temukan di Database SIMRS", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub


#End Region
End Class